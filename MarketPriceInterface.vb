Imports System.Data.SQLite
Imports System.Threading

Public Class MarketPriceInterface

    Private TypeIDToFind As Long ' For searching a price list
    Private PriceHistoryUpdateCount As Integer ' For counting price history updates
    Private PriceOrdersUpdateCount As Integer ' for counting price updates
    Private RefProgressBar As ToolStripProgressBar

    Private TrackingRecords As Boolean

    ' Keeps an array of threads if we need to abort update
    Private ThreadsArray As List(Of Thread) = New List(Of Thread)

    Public Sub New(ByRef SentPG As ToolStripProgressBar)
        RefProgressBar = SentPG
        PriceHistoryUpdateCount = 0
        TrackingRecords = False
    End Sub

    ' For updating market prices and history
    Private Structure ItemRegionPairs
        Dim ItemID As Long
        Dim RegionID As Long
    End Structure

    ' Updates all the price history from ESI
    Public Function UpdateESIPriceHistory(SentTypeIDs As List(Of Long), UpdateRegionID As Long) As Boolean
        Dim Pairs As New List(Of ItemRegionPairs)
        Dim ReturnValue As Boolean = True

        ' Build the pairs
        For i = 0 To SentTypeIDs.Count - 1
            ' Only add data we want to query
            Dim Temp As New TypeIDRegion
            Temp.TypeIDs.Add(CStr(SentTypeIDs(i)))
            Temp.RegionString = CStr(UpdateRegionID)
            If UpdatableMarketData(Temp, "History") Then
                Dim TempPair As ItemRegionPairs
                TempPair.ItemID = SentTypeIDs(i)
                TempPair.RegionID = UpdateRegionID
                Pairs.Add(TempPair)
            End If
        Next

        Try
            If Pairs.Count > 0 Then
                Dim ESIData As New ESI
                Dim NumberofThreads As Integer = 0
                ' How many records per thread do we need?
                Dim Splits As Integer = CInt(Math.Ceiling(Pairs.Count / ESIData.GetMaximumConnections))
                If Splits = 1 Then
                    ' If the return is 1, then we have less than the max connections
                    ' so just set up enough pairs for 1 run each
                    NumberofThreads = Pairs.Count
                Else
                    NumberofThreads = ESIData.GetMaximumConnections
                End If

                ' For processing
                Dim ThreadPairs As New List(Of List(Of ItemRegionPairs))
                Dim TempPairs As New List(Of ItemRegionPairs)
                Dim PairMarker As Integer = 0
                Dim j As Integer = 0

                ' Cut up the pairs into chunks of split count and put them into the threadpairs list for threading later
                For i = 0 To NumberofThreads - 1
                    For j = PairMarker To Pairs.Count - 1
                        If j < Splits * (i + 1) Then
                            TempPairs.Add(Pairs(j))
                        Else
                            Exit For
                        End If
                    Next
                    ThreadPairs.Add(TempPairs)
                    TempPairs = New List(Of ItemRegionPairs)
                    PairMarker = j
                    If j = Pairs.Count Then
                        Exit For
                    End If
                Next

                ' Start a transaction here to speed up processing in the updates
                Call EVEDB.BeginSQLiteTransaction()

                ThreadsArray = New List(Of Thread)

                ' Reset the value of the progress bar
                If Not IsNothing(RefProgressBar) Then
                    RefProgressBar.Visible = True
                    RefProgressBar.Value = 0
                    PriceHistoryUpdateCount = 0
                    RefProgressBar.Maximum = Pairs.Count
                End If
                Application.DoEvents()

                ' Call this manually if it's just one item to update
                If ThreadPairs.Count = 1 Then
                    Call UpdateMarketHistory(ThreadPairs(0))
                Else
                    ' Call each thread for the pairs
                    For i = 0 To ThreadPairs.Count - 1
                        Dim UPHThread As New Thread(AddressOf UpdateMarketHistory)
                        UPHThread.Start(ThreadPairs(i))
                        ' Save the thread if we need to kill it
                        ThreadsArray.Add(UPHThread)
                    Next

                    ' Now loop until all the threads are done
                    ReturnValue = WaitforUpdatetoComplete(CancelManufacturingTabCalc, PriceHistoryUpdateCount, Pairs.Count)

                End If

                ' Make sure all threads are not running
                Call KillThreads()

                ' Finish updating the DB
                Call EVEDB.CommitSQLiteTransaction()
                If Not IsNothing(RefProgressBar) Then
                    RefProgressBar.Visible = False
                End If
                Application.DoEvents()
            End If

        Catch ex As Exception
            Application.DoEvents()
        End Try

        Return ReturnValue

    End Function

    ' For use with threading to speed up the ESI calls
    Private Sub UpdateMarketHistory(ByVal PairsList As Object)
        Dim BatchStart As DateTime = Now
        Dim BatchCounter As Integer = 0
        Dim PricesUpdated As Boolean
        Dim TotalTimes As New List(Of Double)
        Dim DownloadTime As New List(Of Double)
        Dim ProcessingTime As New List(Of Double)
        Dim ESIHistory As New ESI
        Dim ESIData As New ESI
        Dim MaxRequestsperSecond As Integer = ESIHistory.GetRatePerSecond

        Dim Pairs As List(Of ItemRegionPairs) = CType(PairsList, List(Of ItemRegionPairs))

        Try
            For i = 0 To Pairs.Count - 1

                ' Update the prices then check limiting if needed - Note, the internets suggests opening new threads with new db connections but I can't do transactions in each thread, which slows it down and this seems to work fine.
                PricesUpdated = ESIData.UpdateMarketHistory(EVEDB, Pairs(i).ItemID, Pairs(i).RegionID, True)

                ' Only do limiting if we actually update something 
                If PricesUpdated Then
                    BatchCounter += 1

                    If ESIHistory.LimitESICalls(BatchStart, Pairs.Count, BatchCounter) Then
                        ' Reset
                        BatchCounter = 0
                        BatchStart = Now
                    End If
                End If

                ' Increment the count for reach record
                PriceHistoryUpdateCount += 1
            Next
        Catch ex As Exception
            Application.DoEvents()
        End Try

    End Sub

    ' Uses ESI to update market prices from CCP
    Public Function UpdateMarketOrders(ByVal CacheItems As List(Of TypeIDRegion)) As Boolean
        Dim ReturnValue As Boolean = True
        Dim Pairs As New List(Of ItemRegionPairs)

        ' Build the pairs
        For i = 0 To CacheItems.Count - 1
            ' Make sure they are ready to update by cache
            If UpdatableMarketData(CacheItems(i), "Orders") Then
                Dim Temp As New ItemRegionPairs
                Temp.ItemID = CLng(CacheItems(i).TypeIDs(0))
                Temp.RegionID = CLng(CacheItems(i).RegionString)
                Pairs.Add(Temp)
            End If
        Next

        If Pairs.Count > 0 Then
            Dim ESIData As New ESI
            Dim NumberofThreads As Integer = 0
            ' How many records per thread do we need?
            Dim Splits As Integer = CInt(Math.Ceiling(Pairs.Count / ESIData.GetMaximumConnections))
            If Splits = 1 Then
                ' If the return is 1, then we have less than the max connections
                ' so just set up enough pairs for 1 run each
                NumberofThreads = Pairs.Count
            Else
                NumberofThreads = ESIData.GetMaximumConnections
            End If

            ' For processing
            Dim ThreadPairs As New List(Of List(Of ItemRegionPairs))
            Dim TempPairs As New List(Of ItemRegionPairs)
            Dim PairMarker As Integer = 0
            Dim j As Integer = 0

            ' Cut up the pairs into chunks of split count and put them into the threadpairs list for threading later
            For i = 0 To NumberofThreads - 1
                For j = PairMarker To Pairs.Count - 1
                    If j < Splits * (i + 1) Then
                        TempPairs.Add(Pairs(j))
                    Else
                        Exit For
                    End If
                Next
                ThreadPairs.Add(TempPairs)
                TempPairs = New List(Of ItemRegionPairs)
                PairMarker = j
                If j = Pairs.Count Then
                    Exit For
                End If
            Next

            ' Start a transaction here to speed up processing in the updates
            Call EVEDB.BeginSQLiteTransaction()

            ' Reset the value of the progress bar
            If Not IsNothing(RefProgressBar) Then
                RefProgressBar.Visible = True
                RefProgressBar.Value = 0
                PriceHistoryUpdateCount = 0
                RefProgressBar.Maximum = Pairs.Count
            End If
            Application.DoEvents()

            ' Call this manually if it's just one item to update
            If ThreadPairs.Count = 1 Then
                Call UpdateMarketOrders(ThreadPairs(0))
            Else
                ' Call each thread for the pairs
                For i = 0 To ThreadPairs.Count - 1
                    Dim UPHThread As New Thread(AddressOf UpdateMarketOrders)
                    UPHThread.Start(ThreadPairs(i))
                    ' Save the thread if we need to kill it
                    ThreadsArray.Add(UPHThread)
                Next

                ' Now loop until all the threads are done
                ReturnValue = WaitforUpdatetoComplete(CancelUpdatePrices, PriceOrdersUpdateCount, Pairs.Count)

            End If

            ' Make sure all threads are not running
            Call KillThreads()

            ' Finish updating the DB
            Call EVEDB.CommitSQLiteTransaction()
            If Not IsNothing(RefProgressBar) Then
                RefProgressBar.Visible = False
            End If
            Application.DoEvents()
        End If

        CancelUpdatePrices = False

        Return ReturnValue

    End Function

    Private Function WaitforUpdatetoComplete(ByRef CancelUpdate As Boolean, ByRef Counter As Integer, ByVal MaxValue As Integer) As Boolean
        Dim StartTime As DateTime = NoDate
        Dim Counting As Boolean = False
        Dim StillWorking As Boolean = False

        While Counter < MaxValue
            ' Now loop until all the threads are done
            For Each T In ThreadsArray
                If T.ThreadState = ThreadState.Running Then
                    ' Still working on at least 1 thread, so exit
                    StillWorking = True
                    Exit For
                Else
                    StillWorking = False
                End If
            Next

            ' Update the progress bar with data from each thread
            Call IncrementToolStripProgressBar(Counter)
            Application.DoEvents()

            If Not StillWorking Then
                Exit While
            End If

            ' If we are at the last 20 records, start a timer for finishing in case it hangs
            If MaxValue - Counter <= 20 And Not Counting Then
                StartTime = Now
                Counting = True
                TrackingRecords = True
            End If

            ' Check if we need to leave
            If CancelUpdate Or (StartTime <> NoDate And DateDiff(DateInterval.Second, StartTime, Now) >= 30) Then
                Call KillThreads()
                If CancelUpdate Then
                    Return True ' They wanted this so don't error
                Else
                    Return False
                End If
            End If
        End While

        Return True

    End Function

    ' For use with threading to speed up the ESI calls for market orders
    Private Sub UpdateMarketOrders(PairsList As Object)
        Dim BatchStart As DateTime = Now
        Dim BatchCounter As Integer = 0
        Dim PricesUpdated As Boolean
        Dim TotalTimes As New List(Of Double)
        Dim DownloadTime As New List(Of Double)
        Dim ProcessingTime As New List(Of Double)
        Dim ESIData As New ESI
        Dim MaxRequestsperSecond As Integer = ESIData.GetRatePerSecond

        Dim Pairs As List(Of ItemRegionPairs) = CType(PairsList, List(Of ItemRegionPairs))

        Try
            For i = 0 To Pairs.Count - 1

                ' Update the prices then check limiting if needed 
                PricesUpdated = ESIData.UpdateMarketOrders(EVEDB, Pairs(i).ItemID, Pairs(i).RegionID, False, True)

                ' Only do limiting if we actually update something 
                If PricesUpdated Then
                    BatchCounter += 1

                    If ESIData.LimitESICalls(BatchStart, Pairs.Count, BatchCounter) Then
                        ' Reset
                        BatchCounter = 0
                        BatchStart = Now
                    End If
                End If

                ' Now that we updated it, for each record, update the total record count for the progressbar on frmMain
                PriceOrdersUpdateCount += 1
            Next
        Catch ex As Exception
            Application.DoEvents()
        End Try

    End Sub

    ' Aborts all the threads in threads array
    Private Sub KillThreads()
        ' Kill all the threads
        For i = 0 To ThreadsArray.Count - 1
            If ThreadsArray(i).IsAlive Then
                ThreadsArray(i).Abort()
            End If
        Next
    End Sub

    ' Sees if the typid and region sent is ready to be updated by a cache date look up
    Public Function UpdatableMarketData(ByVal Item As TypeIDRegion, ByVal CacheType As String) As Boolean
        Dim SQL As String
        Dim TableName As String
        Dim rsCache As SQLiteDataReader
        Dim Cachedate As Date

        ' First look up the cache date to see if it's time to run the update
        If CacheType = "History" Then
            TableName = "MARKET_HISTORY_UPDATE_CACHE"
        ElseIf CacheType = "Orders" Then
            TableName = "MARKET_ORDERS_UPDATE_CACHE"
        Else
            Return False
        End If

        SQL = "SELECT CACHE_DATE FROM " & TableName & " WHERE TYPE_ID = " & CStr(Item.TypeIDs(0)) & " AND REGION_ID = " & CStr(Item.RegionString)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCache = DBCommand.ExecuteReader

        Cachedate = ProcessCacheDate(rsCache)

        rsCache.Close()
        rsCache = Nothing
        DBCommand = Nothing

        If Cachedate <= Date.UtcNow Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Updates the class referenced toolbar 
    Private Sub IncrementToolStripProgressBar(inValue As Integer)

        If IsNothing(RefProgressBar) Then
            Exit Sub
        End If

        ' Updates the value in the progressbar for a smooth progress (slows procesing a little)
        If inValue <= RefProgressBar.Maximum - 1 And inValue <> 0 Then
            RefProgressBar.Value = inValue
            RefProgressBar.Value = inValue - 1
            RefProgressBar.Value = inValue
        Else
            RefProgressBar.Value = inValue
        End If

    End Sub

End Class
