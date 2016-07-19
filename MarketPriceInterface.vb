Imports System.Data.SQLite
Imports System.Threading

Public Class MarketPriceInterface

    Private TypeIDToFind As Long ' For searching a price list
    Private PriceHistoryUpdateCount As Integer ' For counting price history updates
    Private PriceOrdersUpdateCount As Integer ' for counting price updates
    Private RefProgressBar As ToolStripProgressBar

    Private TrackingRecords As Boolean

    ' Keeps an array of threads if we need to abort update
    Private ThreadsArray As List(Of Threading.Thread) = New List(Of Threading.Thread)

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

    ' EVE MarketData - Updates the item price averages for the region, days, and typeid sent in the cache database - update only each day
    Public Sub UpdateEMDPriceHistory(SentTypeIDs As List(Of Long), UpdateRegionID As Long, UpdateDays As Integer)
        Dim SQL As String
        Dim i, j As Integer
        Dim readerAvgPrices As SQLiteDataReader
        Dim UpdateIDList As New List(Of Long)
        Dim CleanupIDs As New List(Of Long)
        Dim UniqueSentIDs As New List(Of Long)
        Dim UniqueUpdatedPrices As New List(Of Long)
        Dim CurrentDateTime As Date = Now

        Dim API As New EVEMarketDataAPI
        Dim UpdatedAvgPrices As New List(Of EVEMarketDataPriceAverage)
        Dim SQLTypeIDList As String = ""

        ' Clean up sent ID's and make sure we have a set of unique ids
        For i = 0 To SentTypeIDs.Count - 1
            If Not UniqueSentIDs.Contains(SentTypeIDs(i)) Then
                UniqueSentIDs.Add(SentTypeIDs(i))
            End If
        Next

        ' Check the list of Type ID's and see if we ran an update for the ID and days entered in the past day
        For i = 0 To UniqueSentIDs.Count - 1
            SQL = "SELECT 'X' FROM EMD_UPDATE_HISTORY WHERE TYPE_ID =" & CStr(UniqueSentIDs(i)) & " AND REGION_ID = " & CStr(UpdateRegionID)
            SQL = SQL & " AND DateTime(UPDATE_LAST_RAN) >= DateTime('" & Format(DateAdd(DateInterval.Day, -1, CurrentDateTime), SQLiteDateFormat) & "')"
            SQL = SQL & " AND DAYS = " & UpdateDays

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerAvgPrices = DBCommand.ExecuteReader

            If Not readerAvgPrices.Read Then
                ' Then the record needs to be updated, so insert it to the list
                UpdateIDList.Add(UniqueSentIDs(i))
            End If
        Next

        ' Convert list to array of longs
        If UpdateIDList.Count = 0 Then
            ' No updates required
            Exit Sub
        End If

        ' TypeID list for the records
        For i = 0 To UpdateIDList.Count - 1
            SQLTypeIDList = SQLTypeIDList & CStr(UpdateIDList(i)) & ","
        Next

        ' Now that we have a list of ids, get them updated - note return data might not include all typeids
        UpdatedAvgPrices = API.GetPriceAverages(UpdateIDList, UpdateRegionID, UpdateDays)

        ' If this errored, then don't update and notify user it's not working
        If API.GetErrorCode <> 0 Then
            If Not ShownPriceUpdateError Then
                MsgBox("Unable to update all Price volume data at this time.", vbInformation, Application.ProductName)
                ShownPriceUpdateError = True
            End If
        End If

        ' Get a unique list of typeID's that were updated to check later
        ' Clean up sent ID's and make sure we have a set of unique ids
        For i = 0 To UpdatedAvgPrices.Count - 1
            If Not UniqueUpdatedPrices.Contains(UpdatedAvgPrices(i).typeID) Then
                UniqueUpdatedPrices.Add(UpdatedAvgPrices(i).typeID)
            End If
        Next

        ' Even if we errored, update any of the data we did get
        If UpdatedAvgPrices.Count <> 0 Then
            Call EVEDB.BeginSQLiteTransaction()

            ' First delete any records older than 90 days for these typeIDs
            SQLTypeIDList = " (" & SQLTypeIDList.Substring(0, Len(SQLTypeIDList) - 1) & ") "
            SQL = "DELETE FROM EMD_ITEM_PRICE_HISTORY WHERE TYPE_ID IN " & SQLTypeIDList
            SQL = SQL & "AND REGION_ID = " & CStr(UpdateRegionID)
            SQL = SQL & " AND DATETIME(PRICE_HISTORY_DATE) <= DateTime('" & Format(DateAdd(DateInterval.Day, -90, CurrentDateTime), SQLiteDateFormat) & "')"
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            ' Insert these records to the database 
            For i = 0 To UpdatedAvgPrices.Count - 1

                ' Delete the record and insert a fresh one if in there
                SQL = "DELETE FROM EMD_ITEM_PRICE_HISTORY WHERE TYPE_ID = " & CStr(UpdatedAvgPrices(i).typeID)
                SQL = SQL & " AND REGION_ID = " & CStr(UpdatedAvgPrices(i).regionID)
                SQL = SQL & " AND PRICE_HISTORY_DATE = '" & Format(UpdatedAvgPrices(i).pricedate, SQLiteDateFormat) & "'"
                Call EVEDB.ExecuteNonQuerySQL(SQL)

                ' Insert new record - this will make sure any null added records that get updated are current
                SQL = "INSERT INTO EMD_ITEM_PRICE_HISTORY VALUES ("
                SQL = SQL & CStr(UpdatedAvgPrices(i).typeID) & ","
                SQL = SQL & CStr(UpdatedAvgPrices(i).regionID) & ","
                SQL = SQL & "'" & Format(UpdatedAvgPrices(i).pricedate, SQLiteDateFormat) & "',"
                SQL = SQL & CStr(UpdatedAvgPrices(i).lowPrice) & ","
                SQL = SQL & CStr(UpdatedAvgPrices(i).highPrice) & ","
                SQL = SQL & CStr(UpdatedAvgPrices(i).avgPrice) & ","
                SQL = SQL & CStr(UpdatedAvgPrices(i).orders) & ","
                SQL = SQL & CStr(UpdatedAvgPrices(i).volume) & ")"
                Call EVEDB.ExecuteNonQuerySQL(SQL)

            Next

            ' We just did a set of updates, so update the update history for the unique typeIDs
            For i = 0 To UniqueUpdatedPrices.Count - 1
                ' Delete any records there
                SQL = "DELETE FROM EMD_UPDATE_HISTORY WHERE TYPE_ID = " & CStr(UniqueUpdatedPrices(i))
                SQL = SQL & " AND REGION_ID = " & CStr(UpdateRegionID)
                SQL = SQL & " AND DAYS = " & UpdateDays
                Call EVEDB.ExecuteNonQuerySQL(SQL)

                ' Insert the new record
                SQL = "INSERT INTO EMD_UPDATE_HISTORY VALUES (" & CStr(UniqueUpdatedPrices(i)) & ","
                SQL = SQL & UpdateDays & ","
                SQL = SQL & CStr(UpdateRegionID) & ","
                SQL = SQL & "'" & Format(UpdatedAvgPrices(i).UpdateRan, SQLiteDateFormat) & "')"
                Call EVEDB.ExecuteNonQuerySQL(SQL)

            Next

            Call EVEDB.CommitSQLiteTransaction()

        End If

        ' If there are any ID's that we sent and got nothing back from, 
        ' then insert update the update history table and try again tomorrow
        If UniqueUpdatedPrices.Count <> UpdateIDList.Count Then
            Call EVEDB.BeginSQLiteTransaction()

            ' Save the missed ID's here
            j = 0
            SQLTypeIDList = ""

            ' Build the Type ID list
            For i = 0 To UpdateIDList.Count - 1
                TypeIDToFind = UpdateIDList(i)
                If Not UpdatedAvgPrices.Exists(AddressOf FindItem) Then
                    SQLTypeIDList = SQLTypeIDList & CStr(UpdateIDList(i)) & ","
                    CleanupIDs.Add(UpdateIDList(i))
                    j += 1
                End If
            Next

            ' If we have items in the list, process them
            If SQLTypeIDList <> "" Then

                ' Process the missed records one by one
                For i = 0 To CleanupIDs.Count - 1
                    ' If the record is not there, then insert
                    SQL = "SELECT * FROM EMD_UPDATE_HISTORY WHERE TYPE_ID = " & CStr(CleanupIDs(i))
                    SQL = SQL & " AND REGION_ID = " & CStr(UpdateRegionID)
                    SQL = SQL & " AND DAYS = " & CStr(UpdateDays)

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    readerAvgPrices = DBCommand.ExecuteReader

                    If Not readerAvgPrices.HasRows Then
                        ' Insert the record showing we ran an update for this ID
                        SQL = "INSERT INTO EMD_UPDATE_HISTORY VALUES (" & CStr(CleanupIDs(i)) & ","
                        SQL = SQL & CStr(UpdateDays) & ","
                        SQL = SQL & CStr(UpdateRegionID) & ","
                        SQL = SQL & "'" & Format(CurrentDateTime, SQLiteDateFormat) & "')"
                        Call EVEDB.ExecuteNonQuerySQL(SQL)
                    End If
                Next
            End If

            Call EVEDB.CommitSQLiteTransaction()

        End If

    End Sub

    ' Updates all the price history from CREST
    Public Function UpdateCRESTPriceHistory(SentTypeIDs As List(Of Long), UpdateRegionID As Long) As Boolean
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
                Dim CREST As New EVECREST
                Dim NumberofThreads As Integer = 0
                ' How many records per thread do we need?
                Dim Splits As Integer = CInt(Math.Ceiling(Pairs.Count / CREST.GetMaximumConnections))
                If Splits = 1 Then
                    ' If the return is 1, then we have less than the max connections
                    ' so just set up enough pairs for 1 run each
                    NumberofThreads = Pairs.Count
                Else
                    NumberofThreads = CREST.GetMaximumConnections
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

    ' For use with threading to speed up the CREST calls
    Private Sub UpdateMarketHistory(ByVal PairsList As Object)
        Dim BatchStart As DateTime = Now
        Dim BatchCounter As Integer = 0
        Dim PricesUpdated As Boolean
        Dim TotalTimes As New List(Of Double)
        Dim DownloadTime As New List(Of Double)
        Dim ProcessingTime As New List(Of Double)
        Dim CRESTHistory As New EVECREST
        Dim MaxRequestsperSecond As Integer = CRESTHistory.GetRatePerSecond

        Dim Pairs As List(Of ItemRegionPairs) = CType(PairsList, List(Of ItemRegionPairs))

        Try
            For i = 0 To Pairs.Count - 1

                ' Update the prices then check limiting if needed - Note, the internets suggests opening new threads with new db connections but I can't do transactions in each thread, which slows it down and this seems to work fine.
                PricesUpdated = CRESTHistory.UpdateMarketHistory(EVEDB, Pairs(i).ItemID, Pairs(i).RegionID, True)

                ' Only do limiting if we actually update something 
                If PricesUpdated Then
                    BatchCounter += 1

                    If CRESTHistory.LimitCRESTCalls(BatchStart, Pairs.Count, BatchCounter) Then
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

    ' Uses CREST to update market prices
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
            Dim CREST As New EVECREST
            Dim NumberofThreads As Integer = 0
            ' How many records per thread do we need?
            Dim Splits As Integer = CInt(Math.Ceiling(Pairs.Count / CREST.GetMaximumConnections))
            If Splits = 1 Then
                ' If the return is 1, then we have less than the max connections
                ' so just set up enough pairs for 1 run each
                NumberofThreads = Pairs.Count
            Else
                NumberofThreads = CREST.GetMaximumConnections
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

        ' Now loop until all the threads are done
        While Counter < MaxValue
            ' Update the progress bar with data from each thread
            Call IncrementToolStripProgressBar(Counter)

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

            Application.DoEvents()
        End While

        Return True

    End Function

    ' For use with threading to speed up the CREST calls for market orders
    Private Sub UpdateMarketOrders(PairsList As Object)
        Dim BatchStart As DateTime = Now
        Dim BatchCounter As Integer = 0
        Dim PricesUpdated As Boolean
        Dim TotalTimes As New List(Of Double)
        Dim DownloadTime As New List(Of Double)
        Dim ProcessingTime As New List(Of Double)
        Dim CREST As New EVECREST(True)
        Dim MaxRequestsperSecond As Integer = CREST.GetRatePerSecond

        Dim Pairs As List(Of ItemRegionPairs) = CType(PairsList, List(Of ItemRegionPairs))

        Try
            For i = 0 To Pairs.Count - 1

                ' Update the prices then check limiting if needed - ignore cache lookup, the list we get will be updateable
                PricesUpdated = CREST.UpdateMarketOrders(EVEDB, Pairs(i).ItemID, Pairs(i).RegionID, False, True)

                ' Only do limiting if we actually update something 
                If PricesUpdated Then
                    BatchCounter += 1

                    If CREST.LimitCRESTCalls(BatchStart, Pairs.Count, BatchCounter) Then
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
    Private Function UpdatableMarketData(ByVal Item As TypeIDRegion, ByVal CacheType As String) As Boolean
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

        If Cachedate <= Now Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Updates the class referenced toolbar 
    Private Sub IncrementToolStripProgressBar(inValue As integer)

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

    ' Predicate for finding an item in a list EVE Market Data of items
    Private Function FindItem(ByVal Item As EVEMarketDataPriceAverage) As Boolean
        If Item.typeID = TypeIDToFind Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
