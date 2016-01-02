
Imports System.Data.SQLite

Public Class EVEIndustryJobs

    Private JobList As List(Of IndustryJob)
    Private KeyData As APIKeyData
    Private CorpID As Long

    Private CacheDate As Date

    Public Sub New(Optional ByVal Key As APIKeyData = Nothing, Optional ByVal CorporationID As Long = 0)

        If IsNothing(Key) Then
            KeyData = New APIKeyData
        Else
            KeyData = Key
        End If

        JobList = New List(Of IndustryJob)

        ' Set for corp industry jobs
        CorpID = CorporationID

        ' Default to this until we set it
        CacheDate = NoExpiry

    End Sub

    ' Loads all the Industry Assets for the character from the DB
    Public Sub LoadIndustryJobs(ByVal JobType As ScanType, ByRef UpdatefromAPI As Boolean)
        Dim SQL As String
        Dim readerJobs As SQLiteDataReader
        Dim TempJob As IndustryJob
        Dim Jobs As New List(Of IndustryJob)

        If Not KeyData.Access Then
            ' They don't have access to the api so leave
            Exit Sub
        End If

        ' Update Industry jobs first
        Call UpdateIndustryJobs(JobType, UpdatefromAPI)

        ' Load the standings
        SQL = "SELECT * FROM INDUSTRY_JOBS WHERE installerID = " & KeyData.ID
        SQL = SQL & " AND JobType = " & CStr(JobType)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerJobs = DBCommand.ExecuteReader

        While readerJobs.Read
            TempJob.jobID = readerJobs.GetInt64(0)
            TempJob.installerID = readerJobs.GetInt64(1)
            TempJob.installerName = readerJobs.GetString(2)
            TempJob.facilityID = readerJobs.GetInt64(3)
            TempJob.solarSystemID = readerJobs.GetInt64(4)
            TempJob.solarSystemName = readerJobs.GetString(5)
            TempJob.stationID = readerJobs.GetInt64(6)
            TempJob.activityID = readerJobs.GetInt32(7)
            TempJob.licensedRuns = readerJobs.GetInt64(8)
            TempJob.probability = readerJobs.GetDouble(9)
            TempJob.productTypeID = readerJobs.GetInt64(10)
            TempJob.productTypeName = readerJobs.GetString(11)
            TempJob.status = readerJobs.GetInt64(12)
            TempJob.timeInSeconds = readerJobs.GetInt64(13)

            TempJob.startDate = DateTime.ParseExact(readerJobs.GetString(14), SQLiteDateFormat, LocalCulture)
            TempJob.endDate = DateTime.ParseExact(readerJobs.GetString(15), SQLiteDateFormat, LocalCulture)
            TempJob.pauseDate = DateTime.ParseExact(readerJobs.GetString(16), SQLiteDateFormat, LocalCulture)
            TempJob.completedDate = DateTime.ParseExact(readerJobs.GetString(17), SQLiteDateFormat, LocalCulture)

            TempJob.completedCharacterID = readerJobs.GetInt64(18)
            TempJob.blueprintID = readerJobs.GetDouble(19)
            TempJob.blueprintTypeID = readerJobs.GetInt64(20)
            TempJob.blueprintTypeName = readerJobs.GetString(21)
            TempJob.blueprintLocationID = readerJobs.GetInt64(22)
            TempJob.outputLocationID = readerJobs.GetInt64(23)
            TempJob.runs = readerJobs.GetInt64(24)
            TempJob.successfulRuns = readerJobs.GetInt64(25)
            TempJob.cost = readerJobs.GetDouble(26)
            TempJob.teamID = readerJobs.GetInt64(27)

            TempJob.JobType = readerJobs.GetInt32(28)

            ' Insert standing
            Jobs.Add(TempJob)

        End While

        readerJobs.Close()
        DBCommand = Nothing
        readerJobs = Nothing

        JobList = Jobs

    End Sub

    ' Updates the Industry Jobs from API for the character/corp and inserts them into the Database for later queries
    Private Sub UpdateIndustryJobs(ByVal JobType As ScanType, ByRef UpdateAPI As Boolean)
        Dim readerJobs As SQLiteDataReader
        Dim SQL As String
        Dim RefreshDate As Date ' To check the update of the API.
        Dim API As New EVEAPI
        Dim IndyJobs As New List(Of IndustryJob)

        ' See if we are doing an API update 
        If Not UpdateAPI Then
            Exit Sub
        End If

        ' Look up the industry jobs cache date first, if past the date, update the database
        SQL = "SELECT INDUSTRY_JOBS_CACHED_UNTIL FROM API "
        If JobType = ScanType.Personal Then
            SQL = SQL & "WHERE CHARACTER_ID = " & KeyData.ID
            SQL = SQL & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
        Else
            SQL = SQL & "WHERE CORPORATION_ID = " & CorpID
            SQL = SQL & " AND API_TYPE = 'Corporation'"
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerJobs = DBCommand.ExecuteReader

        If readerJobs.Read Then
            If Not IsDBNull(readerJobs.GetValue(0)) Then
                If readerJobs.GetString(0) = "" Then
                    RefreshDate = NoDate
                Else
                    RefreshDate = CDate(readerJobs.GetString(0))
                End If
            Else
                RefreshDate = NoDate
            End If
        Else
            RefreshDate = NoDate
        End If

        ' See if we refresh the data 
        If RefreshDate <= DateTime.UtcNow Then

            IndyJobs = API.GetIndustryJobs(KeyData, JobType, CacheDate)

            If Not NoAPIError(API.GetErrorText, "Character") Then
                ' Errored, exit
                Exit Sub
            End If

            ' Begin session
            Call EVEDB.BeginSQLiteTransaction()

            ' Update the cache date
            SQL = "UPDATE API SET INDUSTRY_JOBS_CACHED_UNTIL = '" & Format(CacheDate, SQLiteDateFormat)

            If JobType = ScanType.Personal Then
                SQL = SQL & "' WHERE CHARACTER_ID = " & KeyData.ID
                SQL = SQL & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            Else
                SQL = SQL & "' WHERE CORPORATION_ID = " & CorpID
                SQL = SQL & " AND API_TYPE = 'Corporation'"
            End If

            Call evedb.ExecuteNonQuerySQL(SQL)

            ' Clear out all the industry jobs for the user - 90 days from the API should be enough
            SQL = "DELETE FROM INDUSTRY_JOBS WHERE installerID = " & KeyData.ID
            SQL = SQL & " AND JobType = " & CStr(JobType)

            Call evedb.ExecuteNonQuerySQL(SQL)

            If Not IsNothing(IndyJobs) Then

                ' Insert industry data
                For i = 0 To IndyJobs.Count - 1
                    ' First make sure it's not already in there
                    With IndyJobs(i)

                        SQL = "SELECT 'X' FROM INDUSTRY_JOBS WHERE jobID = " & .jobID
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        readerJobs = DBCommand.ExecuteReader

                        If Not readerJobs.Read Then

                            ' Insert it
                            SQL = "INSERT INTO INDUSTRY_JOBS (jobID, installerID, installerName, facilityID, solarSystemID, solarSystemName, "
                            SQL = SQL & "stationID, activityID, blueprintID, blueprintTypeID, blueprintTypeName, blueprintLocationID, "
                            SQL = SQL & "outputLocationID, runs, successfulRuns, cost, teamID, licensedRuns, probability, productTypeID, productTypeName, "
                            SQL = SQL & "status, timeInSeconds, startDate, endDate, pauseDate, completedDate, completedCharacterID, JobType) "
                            SQL = SQL & "VALUES (" & .jobID & "," & .installerID & ",'" & FormatDBString(.installerName) & "',"
                            SQL = SQL & .facilityID & "," & .solarSystemID & ",'" & FormatDBString(.solarSystemName) & "'," & .stationID & "," & .activityID & ","
                            SQL = SQL & .blueprintID & "," & .blueprintTypeID & ",'" & FormatDBString(.blueprintTypeName) & "',"
                            SQL = SQL & .blueprintLocationID & "," & .outputLocationID & "," & .runs & "," & .successfulRuns & "," & .cost & "," & .teamID & ","
                            SQL = SQL & .licensedRuns & "," & .probability & "," & .productTypeID & ",'" & FormatDBString(.productTypeName) & "'," & .status & ","
                            SQL = SQL & .timeInSeconds & ",'"
                            SQL = SQL & Format(.startDate, SQLiteDateFormat) & "','"
                            SQL = SQL & Format(.endDate, SQLiteDateFormat) & "','"
                            SQL = SQL & Format(.pauseDate, SQLiteDateFormat) & "','"
                            SQL = SQL & Format(.completedDate, SQLiteDateFormat) & "',"
                            SQL = SQL & .completedCharacterID & ","
                            SQL = SQL & CStr(JobType) & ")"

                            Call evedb.ExecuteNonQuerySQL(SQL)

                        End If
                        readerJobs.Close()
                        readerJobs = Nothing

                    End With
                Next

                DBCommand = Nothing

            End If

            Call EVEDB.CommitSQLiteTransaction()

        End If

    End Sub

    ReadOnly Property CachedUntil() As Date
        Get
            Return CacheDate
        End Get
    End Property

End Class

Public Structure IndustryJob
    ' NEW 
    'columns="jobID,installerID,installerName,facilityID,solarSystemID,solarSystemName,stationID,activityID,
    'blueprintID,blueprintTypeID,blueprintTypeName,blueprintLocationID,outputLocationID,runs,cost,teamID,
    'licensedRuns,probability,productTypeID,productTypeName,status,timeInSeconds,startDate,endDate,pauseDate,completedDate,completedCharacterID"/>

    'Dim jobID As Long 'Unique ID for this job. Subject to the same renumbering as journal entries.
    'Dim assemblyLineID As Long 'ID of the assembly line this job is installed in. IDs for lines in stations don't change, but repackaged assembly arrays, repackaged mobile labs, and redeployed Rorquals will get new assemblyLineIDs.
    'Dim containerID As Long 'If the container is a station (see containerTypeID, below), this is the stationID in the staStations table. For a POS module, this is its itemID (see also the Corporation Asset List API page).
    'Dim installedItemID As Long 'Blueprint itemID. See asset list.
    'Dim installedItemLocationID As Long 'ID for the location from which the blueprint was installed. Office or POS module. See asset list.
    'Dim installedItemQuantity As Long 'Number of blueprints entered (so always 1)
    'Dim installedItemTimeEfficiency As Long 'Starting TE of blueprint.
    'Dim installedItemMaterialEfficiency As Long 'Starting ME of blueprint.
    'Dim installedItemLicensedProductionRunsRemaining As Long 'Starting number of runs remaining. (-1 for a BPO)
    'Dim outputLocationID As Long 'Destination hanger for product (built item when manufacturing, BPC when copying or inventing).
    'Dim installerID As Long 'ID of character who started this job.
    'Dim runs As Long 'Number of runs for this job (when making copies, number of BPCs to make).
    'Dim licensedProductionRuns As Long 'Number of runs on output BPCs for copying and inventing.
    'Dim installedInSolarSystemID As Long 'ID for the solar system this job was installed in. See mapSolarSystems table.
    'Dim containerLocationID As Long 'Container of the container. Seems to generally be the solar system ID.
    'Dim materialMultiplier As Double 'Modifier for amount of materials required over standard BPO/C listing, as effected by installation location (i.e. Rapid Assembly Arrays have a modifier of 1.2, resulting in 20% extra material usage)
    'Dim charMaterialMultiplier As Double 'Effect character's skills & implants have
    'Dim timeMultiplier As Double 'Effect of installation - ie, an advanced mobile lab as a timeMultiplier of 0.65 when copying.
    'Dim charTimeMultiplier As Double 'Speed of research/invention/production, as reduced by individual character skills.
    'Dim installedItemTypeID As Long 'TypeID of blueprint. See tables INVENTORY_TYPES and invBlueprintTypes|invBlueprintTypes.
    'Dim outputTypeID As Long 'TypeID of product. This refers to what's been built, what's being copied, or what's being invented. See INVENTORY_TYPES table.
    'Dim containerTypeID As Long 'TypeID of container, such as station, mobile lab, or assembly array. Again, see INVENTORY_TYPES table.
    'Dim installedItemCopy As Long '0 if the blueprint is an original, 1 if it is a copy.
    'Dim completed As Long '1 if the job has been delivered, 0 if not.
    'Dim completedSuccessfully As Long 'Always 0?
    'Dim installedItemFlag As Long 'See APIv2 Inventory Flags?
    'Dim outputFlag As Long 'See APIv2 Inventory Flags?
    'Dim activityID As Long 'Activity ID of this job. See TL2MaterialsActivity.
    'Dim completedStatus As Long 'See Invention/completedStatus below.
    'Dim installTime As Date 'When this job was installed.
    'Dim beginProductionTime As Date 'When this job was started (after waiting in line or rounding to the next minute, for example).
    'Dim endProductionTime As Date 'When this job will finish or was finished. (Not when it was delivered.)
    'Dim pauseProductionTime As Date 'Normally "0001-01-01 00:00:00". If the job was installed into a POS module and that module went offline, this is when that module went offline. The S&I window in-game calculates the difference between this and endProductionTime to show time remaining (in red, and not counting down). When the module is back up, this is reset to "0001-01-01 00:00:00" and the endProductionTime is updated to reflect the delay.
    'Dim JobType As Integer ' Type of job - corp or personal

    Dim jobID As Long
    Dim installerID As Long
    Dim installerName As String
    Dim facilityID As Long
    Dim solarSystemID As Long
    Dim solarSystemName As String
    Dim stationID As Long
    Dim activityID As Integer
    Dim blueprintID As Double
    Dim blueprintTypeID As Long
    Dim blueprintTypeName As String
    Dim blueprintLocationID As Long
    Dim outputLocationID As Long
    Dim runs As Long
    Dim cost As Double
    Dim teamID As Long
    Dim licensedRuns As Long
    Dim probability As Double
    Dim productTypeID As Long
    Dim productTypeName As String
    Dim status As Long
    Dim timeInSeconds As Long
    Dim startDate As DateTime
    Dim endDate As DateTime
    Dim pauseDate As DateTime
    Dim completedDate As DateTime
    Dim completedCharacterID As Long
    Dim successfulRuns As Long
    Dim JobType As Integer ' Type of job - corp or personal

End Structure