
Imports System.Data.SQLite

Public Class EVEIndustryJobs

    Private JobList As List(Of IndustryJob)

    Public Sub New()

        JobList = New List(Of IndustryJob)

    End Sub

    ' Loads all the Industry Assets from the DB for the ID sent - I'm not using this locally so don't load anything
    Public Sub LoadIndustryJobs(ByVal ID As Long, ByVal TokenData As SavedTokenData, ByVal JobType As ScanType)
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
                Call EVEDB.BeginSQLiteTransaction()

                ' Clear out all the industry jobs for the user
                SQL = "DELETE FROM INDUSTRY_JOBS WHERE InstallerID = " & CharacterTokenData.CharacterID & " AND JobType = " & CStr(JobType)

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

                        If .installer_id = CharacterTokenData.CharacterID Then ' update fields
                            SQL = "INSERT INTO INDUSTRY_JOBS (jobID, installerID, facilityID, locationID, activityID, "
                            SQL &= "blueprintID, blueprintTypeID, blueprintLocationID, outputLocationID, "
                            SQL &= "runs, cost, licensedRuns, probability, productTypeID, status, duration, "
                            SQL &= "startDate, endDate, pauseDate, completedDate, completedCharacterID, successfulRuns, JobType) VALUES ("
                            SQL &= .job_id & "," & .installer_id & "," & .facility_id & "," & LocationID & "," & .activity_id & ","
                            SQL &= .blueprint_id & "," & .blueprint_type_id & "," & .blueprint_location_id & "," & .output_location_id & ","
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

                DBCommand = Nothing

                ' Update cache date now that it's all set
                Call CB.UpdateCacheDate(CDType, CacheDate, ID)

                Call EVEDB.CommitSQLiteTransaction()

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
