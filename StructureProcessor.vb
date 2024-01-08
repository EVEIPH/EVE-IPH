Imports System.Data.SQLite
Imports System.Threading

Public Class StructureProcessor

    Private StructureCount As Integer

    Public Structure StructureStationInformation
        Dim ID As Long
        Dim Name As String
        Dim RegionID As Long
        Dim SystemID As Long
    End Structure

    Public Structure StructureIDTokenEntryFlag
        Dim StructureID As Long
        Dim TokenData As SavedTokenData
        Dim ManualEntry As Boolean
        Dim RefPG As ToolStripProgressBar
    End Structure

    ' Updates the stations table with upwell structure data for the list of IDs sent and returns a set of name/ID pairs using threading
    Public Sub UpdateStructuresData(StructureIDs As List(Of Long), CharacterTokenData As SavedTokenData, refPG As ToolStripProgressBar)

        Try
            Dim Threads As New ThreadingArray
            Dim ESIData As New ESI

            ' For processing
            Dim ThreadData As New List(Of StructureIDTokenEntryFlag)
            Dim QueryData As New StructureIDTokenEntryFlag
            Dim PairMarker As Integer = 0
            Dim SP As New StructureProcessor

            ' Load all the query data
            For Each SID In StructureIDs
                QueryData.StructureID = SID
                QueryData.TokenData = CharacterTokenData
                QueryData.ManualEntry = False
                QueryData.RefPG = refPG
                ThreadData.Add(QueryData)
                Application.DoEvents()
            Next

            ' Call this manually if it's just one item to update
            If ThreadData.Count = 1 Then
                Dim TempPair As New StructureIDTokenEntryFlag
                TempPair = ThreadData(0)
                ThreadData = New List(Of StructureIDTokenEntryFlag)
                ThreadData.Add(TempPair)
                Call UpdateStructureData(ThreadData(0))
            Else
                ' Reset the value of the progress bar for counting structures
                If Not IsNothing(refPG) Then
                    refPG.Visible = True
                    refPG.Value = 0
                    StructureCount = 0
                    refPG.Maximum = StructureIDs.Count
                End If

                ' Call each thread for the pairs
                For i = 0 To ThreadData.Count - 1
                    Dim UPHThread As New Thread(AddressOf UpdateStructureData)
                    UPHThread.Start(ThreadData(i))
                    ' Save the thread if we need to kill it
                    Threads.AddThread(UPHThread)
                Next

                Dim Stillworking As Boolean = True
                Dim PrevCount As Integer = 0
                Dim StartTime As DateTime = Now

                While Not Threads.Complete
                    ' Update the progress bar with current count every time we check (only if we finished at least one run)
                    If StructureCount > PrevCount Then
                        Call IncrementToolStripProgressBar(StructureCount, refPG)
                    End If
                    PrevCount = StructureCount
                    Application.DoEvents()

                    ' Check if we need to leave - cancel pressed or 2 minutes passed
                    If CancelUpdatePrices Or (StartTime <> NoDate And DateDiff(DateInterval.Second, StartTime, Now) >= 120) Then
                        Call Threads.StopAllThreads()
                        ' Reset the error handler
                        ESIErrorHandler = New ESIErrorProcessor
                        If CancelUpdatePrices Then
                            Exit Sub
                        End If
                    End If
                End While

                ' Make sure all threads are not running
                Call Threads.StopAllThreads()
                ' Reset the error handler
                ESIErrorHandler = New ESIErrorProcessor

            End If

        Catch ex As Exception
            Call ESIErrorHandler.ProcessException(ex, ESIErrorProcessor.ESIErrorLocation.PrivateAuthData, False)
        End Try

    End Sub

    ' For use with threading to update structure data
    Public Sub UpdateStructureData(Data As Object)
        Dim StructureInfo As StructureIDTokenEntryFlag

        StructureInfo = CType(Data, StructureIDTokenEntryFlag)

        Call UpdateStructureData(StructureInfo.StructureID, StructureInfo.TokenData, StructureInfo.ManualEntry)

    End Sub

    ' To update data for one structure
    Public Sub UpdateStructureData(StructureID As Long, TokenData As SavedTokenData, ManualEntry As Boolean,
                                   Optional SupressError As Boolean = True, Optional IgnoreCacheDate As Boolean = False)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader
        Dim API As New ESI()
        Dim EVEStructure As New ESIUniverseStructure
        Dim CacheDate As Date
        Dim ManuallyAddedCode As Integer

        If Not IgnoreCacheDate Then
            ' Get the cache date of the facility ID
            SQL = "SELECT CACHE_DATE, STATION_NAME FROM STATIONS WHERE STATION_ID = " & CStr(StructureID)
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsData = DBCommand.ExecuteReader

            If rsData.Read Then
                ' See if we update it
                If Not IsDBNull(rsData.GetValue(0)) Then
                    If DateValue(rsData.GetString(0)) > DateTime.UtcNow Then
                        ' Doesn't need update
                        Exit Sub
                    End If
                End If
            End If

            rsData.Close()

        End If

        If ManualEntry Then
            ManuallyAddedCode = -1
        Else
            ManuallyAddedCode = 0
        End If

        Try
            ' Look up the facility and save it in the STATIONS table
            EVEStructure = API.GetStructureData(StructureID, TokenData, CacheDate, SupressError)

            StructureCount += 1

            ' Look up the manual saved code and save it if we update the data
            SQL = "SELECT MANUAL_ENTRY FROM STATIONS WHERE STATION_ID = " & CStr(StructureID)
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsData = DBCommand.ExecuteReader

            ' Reset the data if it's in the table and we aren't setting to true in this call
            If rsData.Read And ManuallyAddedCode = 0 Then
                ManuallyAddedCode = rsData.GetInt32(0)
            End If

            rsData.Close()

            ' Delete the record, if there, then add new data
            EVEDB.ExecuteNonQuerySQL("DELETE FROM STATIONS WHERE STATION_ID = " & CStr(StructureID))

            If Not IsNothing(EVEStructure) Then
                ' Lookup the data for the upwell structure from static tables
                SQL = "SELECT solarSystemID, security, regionID FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(EVEStructure.solar_system_id)
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsData = DBCommand.ExecuteReader
                rsData.Read()

                If rsData.HasRows Then
                    SQL = "INSERT INTO STATIONS VALUES ({0},'{1}',{2},{3},{4},{5},{6},0,0,'{7}',{8})"
                    With EVEStructure
                        EVEDB.ExecuteNonQuerySQL(String.Format(SQL, StructureID, FormatDBString(.name), .type_id, rsData.GetInt32(0), rsData.GetDouble(1),
                                                        rsData.GetInt32(2), .owner_id, Format(CacheDate, SQLiteDateFormat), ManuallyAddedCode))
                    End With

                End If
                rsData.Close()

            Else
                ' Insert it as unknown so we don't look it up again for a day
                SQL = "INSERT INTO STATIONS VALUES ({0},'{1}',{2},{3},{4},{5},{6},0,0,'{7}',{8})"
                With EVEStructure
                    ' Check the structure each day - set cache to now + 1
                    EVEDB.ExecuteNonQuerySQL(String.Format(SQL, StructureID, "Unknown Structure", 0, 0, 0, 0, 0, Format(DateAdd(DateInterval.Day,
                                                    1, Date.UtcNow), SQLiteDateFormat), ManuallyAddedCode))
                End With
            End If

        Catch X As ThreadAbortException
            ' Just continue as normal
            Application.DoEvents()
        Catch ex As Exception
            MsgBox("An error occured when importing structure data: " & ex.Message, vbInformation, Application.ProductName)
        End Try

    End Sub

    ' Returns the ID, Name, region and system IDs for a structure or station ID sent. If refresh true, then refresh the data 
    Public Function GetStationInformation(StructureID As Long, CharacterTokenData As SavedTokenData, RefreshData As Boolean) As StructureStationInformation
        Dim ReturnData As New StructureStationInformation

        ReturnData.ID = StructureID

        ' Update the data if it's a structure
        If StructureID > MaxStationID And RefreshData Then
            Call UpdateStructureData(StructureID, CharacterTokenData, False)
        End If

        Dim SQL As String
        Dim rsStations As SQLiteDataReader

        ' Get the region and system id from the location of the station or structure
        SQL = "SELECT STATION_ID, STATION_NAME, SOLAR_SYSTEM_ID, REGION_ID FROM STATIONS WHERE STATION_ID = " & CStr(StructureID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsStations = DBCommand.ExecuteReader

        While rsStations.Read()
            ReturnData.ID = rsStations.GetInt64(0)
            ReturnData.Name = rsStations.GetString(1)
            ReturnData.SystemID = rsStations.GetInt64(2)
            ReturnData.RegionID = rsStations.GetInt64(3)
        End While

        rsStations.Close()

        Return ReturnData

    End Function

    ' Updates the class referenced toolbar 
    Private Sub IncrementToolStripProgressBar(inValue As Integer, ByRef PG As ToolStripProgressBar)

        If IsNothing(PG) Then
            Exit Sub
        End If

        ' Updates the value in the progressbar for a smooth progress (slows procesing a little)
        If inValue <= PG.Maximum - 1 And inValue <> 0 Then
            PG.Value = inValue
            PG.Value = inValue - 1
            PG.Value = inValue
        Else
            PG.Value = inValue
        End If

    End Sub

End Class
