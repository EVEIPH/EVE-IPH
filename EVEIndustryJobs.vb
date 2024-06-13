
Imports System.Data.SQLite

Public Class EVEIndustryJobs

    Private JobList As List(Of IndustryJob)
    Private JobType As ScanType

    Public Sub New(Optional ByVal InitalJobType As ScanType = ScanType.Personal)

        JobList = New List(Of IndustryJob)
        JobType = InitalJobType
    End Sub

    ' Loads all the Industry Assets from the DB for the ID sent - I'm not using this locally so don't load anything
    Public Sub LoadIndustryJobs(ByVal ID As Long, ByVal TokenData As SavedTokenData)
        'Dim SQL As String
        'Dim readerJobs As SQLiteDataReader
        'Dim TempJob As IndustryJob
        'Dim Jobs As New List(Of IndustryJob)

        ' Update Industry jobs first
        Call UpdateIndustryJobs(ID, TokenData, JobType)

        'DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        'readerJobs = DBCommand.ExecuteReader

        'While readerJobs.Read

        'End While

        'readerJobs.Close()
        'DBCommand = Nothing
        'readerJobs = Nothing

        ' JobList = Jobs

    End Sub

    ' Updates the Industry Jobs from ESI for the character/corp and inserts them into the Database for later queries
    Public Sub UpdateIndustryJobs(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData, ByVal JobType As ScanType)
        Dim SQL As String
        Dim IndyJobs As New List(Of ESIIndustryJob)
        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date
        Dim LocationID As Long = 0
        Dim TempDate As Date
        Dim CorpCharIDs As New List(Of Long)

        Dim CDType As CacheDateType

        If JobType = ScanType.Personal Then
            CDType = CacheDateType.PersonalIndyJobs
        Else
            CDType = CacheDateType.CorporateIndyJobs
        End If

        ' Look up the industry Blueprints cache date first      
        If CB.DataUpdateable(CDType, ID) Then
            IndyJobs = ESIData.GetIndustryJobs(ID, CharacterTokenData, JobType, CacheDate)

            If Not IsNothing(IndyJobs) Then
                If IndyJobs.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Clear out all the industry jobs for the user if not a corp lookup
                    If JobType = ScanType.Personal Then
                        SQL = "DELETE FROM INDUSTRY_JOBS WHERE InstallerID = " & CharacterTokenData.CharacterID & " AND JobType = " & CStr(JobType)
                    Else
                        ' Delete all jobs and reload for corp
                        SQL = "DELETE FROM INDUSTRY_JOBS WHERE JobType = " & CStr(JobType)

                        ' Also, get the list of character IDs stored in the DB with this corporation and only load those jobs
                        Dim rsIDs As SQLiteDataReader
                        DBCommand = New SQLiteCommand("SELECT CHARACTER_ID FROM ESI_CHARACTER_DATA WHERE CORPORATION_ID = " & CStr(ID), EVEDB.DBREf)
                        rsIDs = DBCommand.ExecuteReader

                        While rsIDs.Read
                            CorpCharIDs.Add(rsIDs.GetInt64(0))
                        End While

                        rsIDs.Close()

                    End If

                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    ' Insert industry data
                    For i = 0 To IndyJobs.Count - 1
                        ' First make sure it's not already in there
                        With IndyJobs(i)
                            ' Insert it
                            If .location_id = 0 Then
                                LocationID = .station_id
                            Else
                                LocationID = .location_id
                            End If

                            If JobType = ScanType.Personal Or (CorpCharIDs.Contains(.installer_id) And JobType = ScanType.Corporation) Then ' update fields
                                SQL = "INSERT INTO INDUSTRY_JOBS (jobID, installerID, facilityID, locationID, activityID, "
                                SQL &= "blueprintID, blueprintTypeID, blueprintLocationID, outputLocationID, "
                                SQL &= "runs, cost, licensedRuns, probability, productTypeID, status, duration, "
                                SQL &= "startDate, endDate, pauseDate, completedDate, completedCharacterID, successfulRuns, JobType) VALUES ("
                                SQL &= .job_id & "," & .installer_id & "," & .facility_id & "," & LocationID & ","
                                ' Bug fix until a decision is made to update SDE or ESI
                                If .activity_id = 9 Then
                                    .activity_id = 11
                                End If
                                SQL &= .activity_id & "," & .blueprint_id & "," & .blueprint_type_id & "," & .blueprint_location_id & "," & .output_location_id & ","
                                SQL &= .runs & "," & .cost & "," & .licensed_runs & "," & .probability & "," & .product_type_id & ",'" & .status & "'," & .duration & ","
                                TempDate = ESIData.FormatESIDate(.start_date)
                                If TempDate <> NoDate Then
                                    SQL &= "'" & Format(TempDate, SQLiteDateFormat) & "',"
                                Else
                                    SQL &= "NULL,"
                                End If
                                TempDate = ESIData.FormatESIDate(.end_date)
                                If TempDate <> NoDate Then
                                    SQL &= "'" & Format(TempDate, SQLiteDateFormat) & "',"
                                Else
                                    SQL &= "NULL,"
                                End If
                                TempDate = ESIData.FormatESIDate(.pause_date)
                                If TempDate <> NoDate Then
                                    SQL &= "'" & Format(TempDate, SQLiteDateFormat) & "',"
                                Else
                                    SQL &= "NULL,"
                                End If
                                TempDate = ESIData.FormatESIDate(.completed_date)
                                If TempDate <> NoDate Then
                                    SQL &= "'" & Format(TempDate, SQLiteDateFormat) & "',"
                                Else
                                    SQL &= "NULL,"
                                End If

                                SQL &= .completed_character_id & "," & .successful_runs & "," & CStr(JobType) & ")"

                                Call EVEDB.ExecuteNonQuerySQL(SQL)

                            End If
                        End With
                    Next

                    ' Now look up distinct location ids to find any public upwell structures to update
                    Dim rsStructure As SQLiteDataReader
                    Dim StructureIDList As New List(Of Long)

                    ' Select facilties only for this character, since others may not have the same rights to this token
                    SQL = "SELECT DISTINCT facilityID FROM INDUSTRY_JOBS WHERE installerID = " & CStr(ID)
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsStructure = DBCommand.ExecuteReader

                    While rsStructure.Read
                        StructureIDList.Add(rsStructure.GetInt64(0))
                    End While

                    rsStructure.Close()

                    ' Update all the structures we don't have names for
                    ' Add the data
                    Dim SP As New StructureProcessor
                    For Each StructureID In StructureIDList
                        Call SP.UpdateStructureData(StructureID, SelectedCharacter.CharacterTokenData, False, True)
                    Next

                    Call EVEDB.CommitSQLiteTransaction()

                End If

                ' Update cache date now that it's all set
                Call CB.UpdateCacheDate(CDType, CacheDate, ID)
            End If
        End If

    End Sub

End Class

Public Structure IndustryJob
    Dim JobID As Integer
    Dim InstallerID As Integer
    Dim FacilityID As Integer
    Dim LocationID As Integer
    Dim ActivityID As Integer
    Dim BlueprintID As Integer
    Dim BlueprintTypeID As Integer
    Dim BlueprintLocationID As Integer
    Dim OutputlocationID As Integer
    Dim Runs As Integer
    Dim Cost As Double
    Dim Licensedruns As Integer
    Dim Probability As Double
    Dim ProductTypeID As Integer
    Dim Status As Integer
    Dim Duration As Integer
    Dim StartDate As Date
    Dim EndDate As Date
    Dim PauseDate As Date
    Dim CompletedDate As Date
    Dim CompletedCharacterID As Integer
    Dim SuccessfulRuns As Integer
    Dim JobType As Integer
End Structure
