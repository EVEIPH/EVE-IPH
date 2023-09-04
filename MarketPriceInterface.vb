Imports System.Data.SQLite
Imports System.Threading

Public Class MarketPriceInterface

    Private TypeIDToFind As Long ' For searching a price list
    Private PriceHistoryUpdateCount As Integer ' For counting price history updates
    Private PriceOrdersUpdateCount As Integer ' for counting price updates
    Private RefProgressBar As ToolStripProgressBar

    Private TrackingRecords As Boolean

    Public Sub New(ByRef SentPG As ToolStripProgressBar)
        RefProgressBar = SentPG
        PriceHistoryUpdateCount = 0
        TrackingRecords = False
    End Sub

    ' For updating market prices and history
    Public Structure ItemRegionPairs
        Dim ItemID As Long
        Dim RegionID As Long
    End Structure

    Public Enum MarketPriceCacheType
        History = 0
        Orders = 1
    End Enum

    ' Updates all the price history from ESI
    Public Function UpdateESIPriceHistory(SentTypeIDs As List(Of Long), UpdateRegionID As Long) As Boolean
        Dim Pairs As New List(Of ItemRegionPairs)
        Dim ReturnValue As Boolean = True
        Dim Threads As New ThreadingArray

        ' If the last time we called for history update was one minute, then reset the last date called and calls per minute counter
        If DateDiff(DateInterval.Second, LastMarketHistoryUpdate, Now) > 60 Then
            MarketHistoryCallsPerMinute = 0
            LastMarketHistoryUpdate = Now
        End If

        ' Build the pairs
        For i = 0 To SentTypeIDs.Count - 1
            ' Only add data we want to query
            Dim TempPair As New ItemRegionPairs
            TempPair.ItemID = SentTypeIDs(i)
            TempPair.RegionID = UpdateRegionID
            If UpdatableMarketData(TempPair, MarketPriceCacheType.History) Then
                ' Check if we reached the max per calls and then don't add anything else
                If MarketHistoryCallsPerMinute < MaxMarketHIstoryCallsPerMinute Then
                    Pairs.Add(TempPair)
                    MarketHistoryCallsPerMinute += 1
                Else
                    ' Can't do more than max calls
                    Exit For
                End If
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
                        Threads.AddThread(UPHThread)
                    Next

                    ' Now loop until all the threads are done
                    ReturnValue = WaitforUpdatetoComplete(Threads, CancelManufacturingTabCalc, PriceHistoryUpdateCount, Pairs.Count)

                End If

                ' Make sure all threads are not running
                Call Threads.StopAllThreads()
                ' Reset the error handler
                ESIErrorHandler = New ESIErrorProcessor

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
        Dim PricesUpdated As Boolean
        Dim TotalTimes As New List(Of Double)
        Dim DownloadTime As New List(Of Double)
        Dim ProcessingTime As New List(Of Double)
        Dim ESIHistory As New ESI
        Dim ESIData As New ESI

        Dim Pairs As List(Of ItemRegionPairs) = CType(PairsList, List(Of ItemRegionPairs))

        Try
            For i = 0 To Pairs.Count - 1

                ' Update the prices then check limiting if needed - Note, the internets suggests opening new threads with new db connections but I can't do transactions in each thread, which slows it down and this seems to work fine.
                PricesUpdated = ESIData.UpdateMarketHistory(EVEDB, Pairs(i).ItemID, Pairs(i).RegionID)

                ' Increment the count for reach record
                PriceHistoryUpdateCount += 1
            Next
        Catch ex As Exception
            Application.DoEvents()
        End Try

    End Sub

    ' Uses ESI to update market prices from CCP
    Public Function UpdateESIMarketOrders(ByVal CacheItems As List(Of TypeIDRegion)) As Boolean
        Dim ReturnValue As Boolean = True
        Dim Pairs As New List(Of ItemRegionPairs)
        Dim Threads As New ThreadingArray
        Dim RegionIDList As New List(Of String)

        CancelUpdatePrices = False

        ' Build the pairs
        For i = 0 To CacheItems.Count - 1
            Dim TempPair As New ItemRegionPairs
            TempPair.ItemID = CLng(CacheItems(i).TypeIDs(0))
            TempPair.RegionID = CLng(CacheItems(i).RegionString)
            ' Make sure they are ready to update by cache
            If UpdatableMarketData(TempPair, MarketPriceCacheType.Orders) Then
                ' Save all the region IDs and check for one remaining at end for unknown stations
                If Not RegionIDList.Contains(CStr(TempPair.RegionID)) Then
                    RegionIDList.Add(CStr(TempPair.RegionID))
                End If
                Pairs.Add(TempPair)
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
                    Threads.AddThread(UPHThread)
                Next

                ' Now loop until all the threads are done
                ReturnValue = WaitforUpdatetoComplete(Threads, CancelUpdatePrices, PriceOrdersUpdateCount, Pairs.Count)

            End If

            ' Make sure all threads are not running
            Call Threads.StopAllThreads()
            ' Reset the error handler
            ESIErrorHandler = New ESIErrorProcessor

            ' Finally, update any location data on structures we imported without region or system ID
            Dim UpdateIDs As New List(Of Long)
            Dim rsUpdate As SQLiteDataReader
            DBCommand = New SQLiteCommand("SELECT DISTINCT LOCATION_ID FROM MARKET_ORDERS WHERE REGION_ID = 0", EVEDB.DBREf)
            rsUpdate = DBCommand.ExecuteReader

            While rsUpdate.Read
                UpdateIDs.Add(rsUpdate.GetInt64(0))
            End While

            rsUpdate.Close()

            ' Now with this list, run the structures update
            Dim SP As New StructureProcessor
            Dim TempRegionID As String = ""
            Call SP.UpdateStructuresData(UpdateIDs, SelectedCharacter.CharacterTokenData, False, RefProgressBar)

            For Each ID In UpdateIDs
                DBCommand = New SQLiteCommand("SELECT REGION_ID, SOLAR_SYSTEM_ID FROM STATIONS WHERE STATION_ID =" & CStr(ID), EVEDB.DBREf)
                rsUpdate = DBCommand.ExecuteReader

                If rsUpdate.Read Then
                    TempRegionID = CStr(rsUpdate.GetInt64(0))
                    If TempRegionID = "0" And RegionIDList.Count = 1 Then
                        ' They only wanted prices from one region and we have an unknown structure, so at least save the region ID we set the query up for
                        TempRegionID = RegionIDList(0)
                    End If
                    Call EVEDB.ExecuteNonQuerySQL(String.Format("UPDATE MARKET_ORDERS SET REGION_ID = {0}, SOLAR_SYSTEM_ID = {1} WHERE LOCATION_ID = {2}", TempRegionID, rsUpdate.GetInt64(1), ID))
                End If

                rsUpdate.Close()
            Next

            ' Finish updating the DB
            Call EVEDB.CommitSQLiteTransaction()

            If Not IsNothing(RefProgressBar) Then
                RefProgressBar.Visible = False
            End If
            Application.DoEvents()
        End If

        Return ReturnValue

    End Function

    Private Function WaitforUpdatetoComplete(ByRef ThreadsArray As ThreadingArray, ByRef CancelUpdate As Boolean, ByRef Counter As Integer, ByVal MaxValue As Integer) As Boolean
        Dim StartTime As DateTime = NoDate
        Dim Counting As Boolean = False
        Dim StillWorking As Boolean = False

        While Counter < MaxValue
            ' Now loop until all the threads are done
            StillWorking = Not ThreadsArray.Complete

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
                Call ThreadsArray.StopAllThreads()
                ' Reset the error handler
                ESIErrorHandler = New ESIErrorProcessor
                If CancelUpdate Then
                    Return True ' They wanted this so don't error
                Else
                    Return False
                End If
            End If
        End While

        Return True

    End Function

    ' Threading call to speed up the ESI calls for market orders
    Private Sub UpdateMarketOrders(PairsList As Object)
        Dim PricesUpdated As Boolean
        Dim TotalTimes As New List(Of Double)
        Dim DownloadTime As New List(Of Double)
        Dim ProcessingTime As New List(Of Double)
        Dim ESIData As New ESI

        Dim Pairs As List(Of ItemRegionPairs) = CType(PairsList, List(Of ItemRegionPairs))

        Try
            For i = 0 To Pairs.Count - 1

                ' Update the prices then check limiting if needed 
                PricesUpdated = ESIData.UpdateMarketOrders(EVEDB, Pairs(i).ItemID, Pairs(i).RegionID, False, True)

                ' Now that we updated it, for each record, update the total record count for the progressbar on frmMain
                PriceOrdersUpdateCount += 1
            Next
        Catch ex As Exception
            Application.DoEvents()
        End Try

    End Sub

    ' Sees if the typid and region sent is ready to be updated by a cache date look up
    Public Function UpdatableMarketData(ByVal Item As ItemRegionPairs, ByVal CacheType As MarketPriceCacheType) As Boolean
        Dim SQL As String
        Dim TableName As String
        Dim rsCache As SQLiteDataReader
        Dim Cachedate As Date

        'Temp fix for ESI issue on market/region/history endpoint - don't allow updates
        If CacheType = MarketPriceCacheType.History Then
            'Return False
        End If

        ' First look up the cache date to see if it's time to run the update
        If CacheType = MarketPriceCacheType.History Then
            TableName = "MARKET_HISTORY_UPDATE_CACHE"
        ElseIf CacheType = MarketPriceCacheType.Orders Then
            TableName = "MARKET_ORDERS_UPDATE_CACHE"
        Else
            Return False
        End If

        SQL = "SELECT CACHE_DATE FROM " & TableName & " WHERE TYPE_ID = " & CStr(Item.ItemID) & " AND REGION_ID = " & CStr(Item.RegionID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCache = DBCommand.ExecuteReader

        Cachedate = ProcessCacheDate(rsCache)

        rsCache.Close()

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
