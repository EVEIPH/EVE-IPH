
Imports System.Data.SQLite

Public Class Character
    Public CachedUntil As Date
    Public KeyID As Long
    Public APIKey As String
    Public AccessMask As Long ' This number will define what access I have to the character's API
    Public ID As Long ' PK
    Public Name As String
    Public OverrideSkills As Boolean
    Public IsDefault As Boolean
    Public APIExpiration As Date ' Date when the API key will expire

    Private CombinedKeyData As APIKeyData

    ' For each Access Mask, we can have different access, which are stored here
    Private CharacterSkillsAccess As Boolean
    Private CharacterStandingsAccess As Boolean

    Private CharacterAssetsAccess As Boolean
    Private IndustryJobsAccess As Boolean
    Private ResearchAgentAccess As Boolean

    ' Skill Tree
    Public Skills As EVESkillList
    ' Standings
    Public Standings As NPCStandings ' Base Standings
    ' Industry jobs
    Private Jobs As EVEIndustryJobs
    ' Research agents
    Private DatacoreAgents As ResearchAgents
    ' Assets
    Private Assets As EVEAssets
    ' Blueprints
    Private Blueprints As EVEBlueprints

    ' All corporation data stored here (assets, jobs, etc)
    Public CharacterCorporation As Corporation

    ' If we load new data or not through the API
    Private UpdatedNewData As Boolean

    Public Sub New()
        KeyID = 0
        APIKey = ""
        ID = 0 ' PK
        Name = ""
        CachedUntil = NoDate
        IsDefault = False
        AccessMask = 0
        APIExpiration = NoDate ' Date when this API key will expire

        ' For each Access Mask, we can have different access
        SkillsAccess = False
        StandingsAccess = False
        AssetsAccess = False
        IndustryJobsAccess = False
        ResearchAccess = False

        CombinedKeyData = New APIKeyData

        Skills = New EVESkillList
        Standings = New NPCStandings
        Jobs = New EVEIndustryJobs
        DatacoreAgents = New ResearchAgents
        Assets = New EVEAssets

        ' Corporation Data for this character
        CharacterCorporation = New Corporation

    End Sub

    ' Sets the Access variables for the return character data sent
    Public Sub SetAPIAccess()
        Dim BitString As String
        Dim BitLen As Integer

        ' Access mask is a bitmask 
        BitString = GetBits(AccessMask)

        BitLen = Len(BitString)

        ' Just do a bool cast on the bits for any API access stuff we want
        If BitLen >= AccessMaskBitLocs.CharacterSheet Then
            SkillsAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.CharacterSheet, 1))
        Else
            SkillsAccess = False
        End If
        If BitLen >= AccessMaskBitLocs.Standings Then
            StandingsAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.Standings, 1))
        Else
            StandingsAccess = False
        End If
        If BitLen >= AccessMaskBitLocs.AssetList Then
            AssetsAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.AssetList, 1))
        Else
            AssetsAccess = False
        End If
        If BitLen >= AccessMaskBitLocs.IndustryJobs Then
            IndustryJobsAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.IndustryJobs, 1))
        Else
            IndustryJobsAccess = False
        End If
        If BitLen >= AccessMaskBitLocs.Research Then
            ResearchAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.Research, 1))
        Else
            ResearchAccess = False
        End If

        BitLen = 0

    End Sub

    ' Saves the dummy character for the program
    Public Sub LoadDummyCharacter()
        Dim SQL As String
        Dim rsCheck As SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT 'X' FROM API WHERE CHARACTER_ID = 0", DB)
        rsCheck = DBCommand.ExecuteReader

        ' Double check to make sure the record doesn't already exist - user could update skills, etc for a dummy and don't want to overwrite
        If Not rsCheck.HasRows Then
            ' Now insert this data in the DB for using all the time and set to default"yyyy-MM-dd HH:mm:ss" since it doesn't exist
            CachedUntil = NoExpiry
            ID = 0
            KeyID = 0
            APIKey = "No Key"
            AccessMask = 0
            Name = None
            IsDefault = True
            OverrideSkills = False

            APIExpiration = NoExpiry

            ' Default corp
            CharacterCorporation = New Corporation()

            SQL = "INSERT INTO API VALUES ('" & Format(CachedUntil, SQLiteDateFormat) & "'," & CStr(KeyID) & ",'" & APIKey & "',"
            SQL = SQL & "'" & "Character" & "'," & AccessMask & "," & CStr(ID) & ",'" & Name & "'," & CStr(CharacterCorporation.CorporationID) & ","
            SQL = SQL & "'" & CharacterCorporation.CorporationName & "'," & CInt(OverrideSkills) & "," & CInt(IsDefault) & ","
            SQL = SQL & "'" & Format(APIExpiration, SQLiteDateFormat) & "',"
            SQL = SQL & "'" & Format(Assets.CachedUntil, SQLiteDateFormat) & "',"
            SQL = SQL & "'" & Format(Jobs.CachedUntil, SQLiteDateFormat) & "',"
            SQL = SQL & "'" & Format(DatacoreAgents.CachedUntil, SQLiteDateFormat) & "',"
            SQL = SQL & "'" & Format(APIExpiration, SQLiteDateFormat) & "',"
            SQL = SQL & "'" & Format(APIExpiration, SQLiteDateFormat) & "')"

            Call ExecuteNonQuerySQL(SQL)

            ' Load the dummy skills
            Skills.LoadDummySkills()
            SkillsAccess = True ' Set this so it won't throw errors but there is no access to skills through API

            ' No standings
            Standings = New NPCStandings

            ' No agents
            DatacoreAgents = New ResearchAgents

            ' No Assets
            Assets = New EVEAssets

            ' No Jobs
            Jobs = New EVEIndustryJobs

        Else ' There is a dummy already in the DB, so just set it to default and load like a normal char
            SQL = "UPDATE API SET IS_DEFAULT = -1 WHERE CHARACTER_ID = 0"
            Call ExecuteNonQuerySQL(SQL)

            Call LoadDefaultCharacter()

        End If

    End Sub

    ' Sets the default character for the program if no character ID sent, else it returns the character ID. If we can't find it in DB, then return false
    Public Function LoadDefaultCharacter(Optional ReloadAPIData As Boolean = False, Optional RefreshAssets As Boolean = False, Optional RefreshBlueprints As Boolean = False, _
                                         Optional ByVal CharacterID As Long = 0, Optional ByVal OverrideCacheDate As Boolean = False) As Boolean
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String
        Dim RefreshDate As Date ' To check for update of the API. 

        Dim NumAPIs As Integer = 0
        Dim NumNewAPIs As Integer = 0

        SQL = "SELECT COUNT(*) FROM API WHERE API_TYPE <> 'Old Key'"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerCharacter = DBCommand.ExecuteReader

        If readerCharacter.Read() Then
            NumNewAPIs = readerCharacter.GetInt32(0)
        End If

        SQL = "SELECT COUNT(*) FROM API WHERE CHARACTER_NAME <> 'None'"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerCharacter = DBCommand.ExecuteReader

        If readerCharacter.Read() Then
            NumAPIs = readerCharacter.GetInt32(0)
        End If

        If NumAPIs <> 0 And NumNewAPIs = 0 Then
            ' Only show message if they have old API's stored
            MsgBox("You do not have a valid API key entered. Please update your character data with customizable API keys.", vbInformation, Application.ProductName)
        End If

        ' Get base data and cache date
        SQL = "SELECT CACHED_UNTIL, KEY_ID, API_KEY, CHARACTER_ID, API_TYPE FROM API WHERE IS_DEFAULT <> 0 AND API_TYPE NOT IN ('Old Key','Corporation')"
        DBCommand = New SQLiteCommand(SQL, DB)
        readerCharacter = DBCommand.ExecuteReader

        If readerCharacter.Read() Then
            ' See if we refresh the data or just pull from DB by checking the last updated
            ' RefreshDate is in UTC time
            If readerCharacter.GetString(0) <> "" Then
                RefreshDate = CDate(readerCharacter.GetString(0))
            Else
                RefreshDate = NoDate
            End If

            ' See if we want to refresh the data from API
            If RefreshDate < DateTime.UtcNow Or OverrideCacheDate Then
                ' Refresh data, update loadnewdata if we error - used later on in updates
                UpdatedNewData = UpdateAccountAPIData(readerCharacter.GetInt64(1), readerCharacter.GetString(2), readerCharacter.GetString(4), readerCharacter.GetInt64(3), True)
            Else
                ' Not time to update the API data so don't reload all the rest of the data later either unless we set it in call
                If ReloadAPIData Then
                    UpdatedNewData = True
                Else
                    UpdatedNewData = False
                End If
            End If

            ' Get the latest data for this character
            Call LoadCharacterData(RefreshAssets, RefreshBlueprints, CharacterID)

            readerCharacter.Close()
            Return True

        Else ' No record in DB
            readerCharacter.Close()
            Return False
        End If

    End Function

    ' Load the latest data for the character sent or the default if no character sent from the DB
    Private Sub LoadCharacterData(ByVal RefreshAssets As Boolean, ByVal RefreshBlueprints As Boolean, Optional ByVal CharacterID As Long = 0)
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String

        SQL = "SELECT KEY_ID, API_KEY, CHARACTER_ID, CHARACTER_NAME, "
        SQL = SQL & "CORPORATION_ID, CORPORATION_NAME, CACHED_UNTIL, IS_DEFAULT, "
        SQL = SQL & "OVERRIDE_SKILLS, ACCESS_MASK, KEY_EXPIRATION_DATE "
        SQL = SQL & "FROM API "
        If CharacterID = 0 Then
            SQL = SQL & "WHERE IS_DEFAULT <> 0 "
        Else
            SQL = SQL & "WHERE CHARACTER_ID = " & CStr(CharacterID) & " "
        End If
        SQL = SQL & "AND API_TYPE NOT IN ('Old Key','Corporation')"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerCharacter = DBCommand.ExecuteReader

        If readerCharacter.Read Then
            ' Query the character data and store
            KeyID = readerCharacter.GetInt64(0)
            APIKey = readerCharacter.GetString(1)
            ID = readerCharacter.GetInt64(2)
            Name = readerCharacter.GetString(3)
            CharacterCorporation = New Corporation(readerCharacter.GetInt64(4), readerCharacter.GetString(5), ID)
            CharacterCorporation.LoadCorpAPIData(RefreshAssets, RefreshBlueprints)
            CachedUntil = CDate(readerCharacter.GetString(6))
            IsDefault = CBool(readerCharacter.GetInt32(7))

            UserApplicationSettings.AllowSkillOverride = CBool(readerCharacter.GetInt32(8))
            AccessMask = readerCharacter.GetInt64(9)
            If readerCharacter.GetString(10) <> ExpiredKey Then
                APIExpiration = CDate(readerCharacter.GetString(10))
            Else
                APIExpiration = NoDate
            End If

            Call SetAPIAccess()

            readerCharacter.Close()

            ' Set the key data
            CombinedKeyData.KeyID = KeyID
            CombinedKeyData.APIKey = APIKey
            CombinedKeyData.ID = ID

            ' Load the skills from DB for this character locally
            Call LoadSkills(False, UpdatedNewData)

            ' Load the standings for this character
            Call LoadCharacterStandings(UpdatedNewData)

            ' Load the character's research agents
            CombinedKeyData.Access = ResearchAccess
            DatacoreAgents = New ResearchAgents(CombinedKeyData)
            Call DatacoreAgents.LoadResearchAgents(UpdatedNewData)

            '*** Jobs and assets basically work the same, we load them locally but don't really use them locally
            ' but instead query directly from the db as it's easier to work with

            ' Update the character's industry jobs
            CombinedKeyData.Access = IndustryJobsAccess
            Jobs = New EVEIndustryJobs(CombinedKeyData, 0)
            Call Jobs.LoadIndustryJobs(ScanType.Personal, UpdatedNewData)

            ' Load in the assets, but we won't update them due to long API cache times
            CombinedKeyData.Access = AssetsAccess
            Assets = New EVEAssets(CombinedKeyData, 0)
            Call Assets.LoadAssets(ScanType.Personal, RefreshAssets)

            ' Load the Blueprints but don't update due to long api cache times
            CombinedKeyData.Access = AssetsAccess
            Blueprints = New EVEBlueprints(CombinedKeyData, 0)
            Call Blueprints.LoadBlueprints(ScanType.Personal, RefreshBlueprints)

            readerCharacter.Close()

        End If

    End Sub

    Public Function GetIndustryJobs() As EVEIndustryJobs
        Return Jobs
    End Function

    Public Function GetResearchAgents() As ResearchAgents
        Return DatacoreAgents
    End Function

    Public Function GetAssets() As EVEAssets
        Return Assets
    End Function

    Public Function GetBlueprints() As EVEBlueprints
        Return Blueprints
    End Function

    ' Gets the user's saved location IDs from the table
    Public Function GetAssetLocationIDs(Location As AssetWindow) As List(Of LocationInfo)
        Dim TempLocation As LocationInfo
        Dim ReturnLocations As New List(Of LocationInfo)
        Dim SQL As String
        Dim readerLocations As SQLiteDataReader

        ' Since a lot of locations will bog down the settings loading, load locations from a table
        SQL = "SELECT ID, LocationID, FlagID FROM ASSET_LOCATIONS WHERE EnumAssetType = " & CStr(Location)
        SQL = SQL & " AND ID IN (" & CStr(ID) & "," & CStr(CharacterCorporation.CorporationID) & ")"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerLocations = DBCommand.ExecuteReader

        While readerLocations.Read
            TempLocation = New LocationInfo
            TempLocation.AccountID = readerLocations.GetInt64(0)
            TempLocation.LocationID = readerLocations.GetInt64(1)
            TempLocation.FlagID = readerLocations.GetInt32(2)

            ReturnLocations.Add(TempLocation)
        End While

        Return ReturnLocations

    End Function

#Region "Standings Processing"

    ' Updates and Loads the character's standings from DB
    Private Sub LoadCharacterStandings(UpdateAPIData As Boolean)
        Dim SQL As String
        Dim readerStandings As SQLiteDataReader
        Dim Tempstandings As New NPCStandings

        ' Don't try to update/load dummy standings
        If ID = 0 Then
            Exit Sub
        End If

        ' First update the standings
        Call UpdateCharacterStandings(UpdateAPIData)

        ' Load the standings
        SQL = "SELECT NPC_TYPE_ID, NPC_TYPE, NPC_NAME, STANDING FROM CHARACTER_STANDINGS WHERE CHARACTER_ID=" & ID

        DBCommand = New SQLiteCommand(SQL, DB)
        readerStandings = DBCommand.ExecuteReader

        While readerStandings.Read
            ' Insert standing
            Tempstandings.InsertStanding(readerStandings.GetInt64(0), readerStandings.GetString(1), readerStandings.GetString(2), readerStandings.GetDouble(3))
        End While

        readerStandings.Close()
        DBCommand = Nothing
        readerStandings = Nothing

        Standings = Tempstandings

    End Sub

    ' Updates the Character Standings from API for the sent character and inserts them into the Database for later queries
    Private Sub UpdateCharacterStandings(UpdateAPIData As Boolean)
        Dim API As New EVEAPI
        Dim SQL As String
        Dim i As Integer
        Dim TempStandings As NPCStandings

        ' See if we are doing an API update 
        If Not UpdateAPIData Then
            Exit Sub
        End If

        CombinedKeyData.Access = SkillsAccess

        ' Get standings
        TempStandings = API.GetCharacterStandings(CombinedKeyData)

        If Not NoAPIError(API.GetErrorText, "Character") Then
            ' Errored, exit
            Exit Sub
        End If

        Call BeginSQLiteTransaction()

        ' Delete the old standings data
        SQL = "DELETE FROM CHARACTER_STANDINGS WHERE CHARACTER_ID = " & ID
        Call ExecuteNonQuerySQL(SQL)

        If Not IsNothing(TempStandings) Then

            ' Insert skill data
            For i = 0 To TempStandings.NumStandings - 1
                SQL = "INSERT INTO CHARACTER_STANDINGS (CHARACTER_ID, NPC_TYPE_ID, NPC_TYPE, NPC_NAME, STANDING) "
                SQL = SQL & " VALUES (" & ID & "," & TempStandings.GetStandingsList(i).NPCID
                SQL = SQL & ",'" & TempStandings.GetStandingsList(i).NPCType
                SQL = SQL & "','" & FormatDBString(TempStandings.GetStandingsList(i).NPCName)
                SQL = SQL & "'," & TempStandings.GetStandingsList(i).Standing & ")"
                Call ExecuteNonQuerySQL(SQL)
            Next

            DBCommand = Nothing
        End If

        Call CommitSQLiteTransaction()

    End Sub

#End Region

#Region "Skills Processing"

    ' Updates and Loads the character's skills from DB - Should be public for processing skills in the skill window
    Public Sub LoadSkills(Optional ByVal LoadAllSkillsforOverride As Boolean = False, Optional UpdateAPIData As Boolean = True, Optional SkillNameFilter As String = "")
        Dim readerSkills As SQLiteDataReader
        Dim SQL As String

        ' First Update the data from API
        Call UpdateCharacterSkills(UpdateAPIData)

        ' Reset Local Variable
        Skills = New EVESkillList

        ' Get all skills and set skill to 0 if they don't have it
        SQL = "SELECT SKILLS.SKILL_TYPE_ID,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.SKILL_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.SKILL_LEVEL END AS SKILL_LEVEL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.SKILL_POINTS IS NULL THEN 0 ELSE CHAR_SKILLS.SKILL_POINTS END AS SKILL_POINTS,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.OVERRIDE_SKILL IS NULL THEN 0 ELSE CHAR_SKILLS.OVERRIDE_SKILL END AS OVERRIDE_SKILL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.OVERRIDE_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.OVERRIDE_LEVEL END AS OVERRIDE_LEVEL "
        SQL = SQL & "FROM SKILLS LEFT OUTER JOIN "
        SQL = SQL & "(SELECT * FROM CHARACTER_SKILLS WHERE CHARACTER_SKILLS.CHARACTER_ID=" & SelectedCharacter.ID & ") AS CHAR_SKILLS "
        SQL = SQL & "ON (SKILLS.SKILL_TYPE_ID = CHAR_SKILLS.SKILL_TYPE_ID) "
        If SkillNameFilter <> "" Then
            SQL = SQL & " WHERE SKILLS.SKILL_TYPE_ID IN (SELECT SKILL_TYPE_ID FROM SKILLS WHERE SKILL_NAME LIKE '%" & FormatDBString(SkillNameFilter) & "%') "
        End If
        SQL = SQL & "ORDER BY SKILLS.SKILL_GROUP, SKILLS.SKILL_NAME "

        DBCommand = New SQLiteCommand(SQL, DB)
        readerSkills = DBCommand.ExecuteReader

        While readerSkills.Read
            ' Insert skill
            If UserApplicationSettings.AllowSkillOverride And CBool(readerSkills.GetInt32(3)) And LoadAllSkillsforOverride Then
                ' Use the override skill if set, save the old skill level in the override so we can reference it later if needed
                Skills.InsertSkill(readerSkills.GetInt64(0), readerSkills.GetInt32(4), readerSkills.GetInt64(2), CBool(readerSkills.GetInt32(3)), readerSkills.GetInt32(1))
            Else ' Just normal skills
                Skills.InsertSkill(readerSkills.GetInt64(0), readerSkills.GetInt32(1), readerSkills.GetInt64(2), CBool(readerSkills.GetInt32(3)), readerSkills.GetInt32(4))
            End If

        End While

        readerSkills.Close()
        readerSkills = Nothing
        DBCommand = Nothing

    End Sub

    ' Gets the Character Skills from API for this character and inserts them into the Database for later queries
    Private Sub UpdateCharacterSkills(UpdateAPIData As Boolean)
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String
        Dim API As New EVEAPI
        Dim i As Integer
        Dim TempSkills As New EVESkillList
        Dim SingleSkill As New EVESkillList
        Dim SkillList As String = ""

        ' See if we are doing an API update 
        If Not UpdateAPIData Then
            Exit Sub
        End If

        If ID = 0 Then
            ' Don't run update for dummy
            Exit Sub
        End If

        CombinedKeyData.Access = SkillsAccess

        ' Get skill data from API
        TempSkills = API.GetCharacterSkills(CombinedKeyData)

        If Not NoAPIError(API.GetErrorText, "Character") Then
            ' Errored, exit
            Exit Sub
        End If

        ' Clean out any skills not in the temp skills, make this first. This will ignore any skills the person may have over-ridden and added
        For i = 0 To TempSkills.GetSkillList.Count - 1
            SkillList = SkillList & TempSkills.GetSkillList(i).TypeID & ","
        Next

        ' Strip comma
        SkillList = Left(SkillList, Len(SkillList) - 1)

        ' Delete the temp skills but not any that are overridden
        SQL = "DELETE FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID IN (" & SkillList & ") AND CHARACTER_ID =" & ID
        SQL = SQL & " AND OVERRIDE_SKILL <> -1"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerCharacter = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction()

        ' Insert skill data
        For i = 0 To TempSkills.GetSkillList.Count - 1

            ' Check for skill and update if there
            SQL = "SELECT 'X' FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & TempSkills.GetSkillList(i).TypeID & " AND CHARACTER_ID =" & ID

            DBCommand = New SQLiteCommand(SQL, DB)
            readerCharacter = DBCommand.ExecuteReader

            If Not readerCharacter.HasRows Then
                ' Insert skill data
                SQL = "INSERT INTO CHARACTER_SKILLS (CHARACTER_ID, SKILL_TYPE_ID, SKILL_NAME, SKILL_POINTS, SKILL_LEVEL, OVERRIDE_SKILL, OVERRIDE_LEVEL) "
                SQL = SQL & " VALUES (" & ID & "," & TempSkills.GetSkillList(i).TypeID & ",'" & TempSkills.GetSkillList(i).Name & "',"
                SQL = SQL & TempSkills.GetSkillList(i).SkillPoints & "," & TempSkills.GetSkillList(i).Level & ",0,0)"
            Else
                ' Update skill data
                SQL = "UPDATE CHARACTER_SKILLS SET "
                SQL = SQL & "SKILL_TYPE_ID = " & TempSkills.GetSkillList(i).TypeID & ", SKILL_NAME = '" & TempSkills.GetSkillList(i).Name & "',"
                SQL = SQL & "SKILL_POINTS = " & TempSkills.GetSkillList(i).SkillPoints & ", SKILL_LEVEL = " & TempSkills.GetSkillList(i).Level & " "
                SQL = SQL & "WHERE CHARACTER_ID = " & ID & " AND SKILL_TYPE_ID = " & TempSkills.GetSkillList(i).TypeID
            End If

            readerCharacter.Close()
            readerCharacter = Nothing

            Call ExecuteNonQuerySQL(SQL)

        Next

        Call CommitSQLiteTransaction()

    End Sub

#End Region

#Region "Properties for Access Variables"

    Property JobsAccess() As Boolean
        Get
            Return IndustryJobsAccess
        End Get

        Set(ByVal AccessValue As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            IndustryJobsAccess = AccessValue
        End Set
    End Property

    Property AssetsAccess() As Boolean
        Get
            Return CharacterAssetsAccess
        End Get

        Set(ByVal AccessValue As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            CharacterAssetsAccess = AccessValue
        End Set
    End Property

    Property ResearchAccess() As Boolean
        Get
            Return ResearchAgentAccess
        End Get

        Set(ByVal AccessValue As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            ResearchAgentAccess = AccessValue
        End Set
    End Property

    Property SkillsAccess() As Boolean
        Get
            Return CharacterSkillsAccess
        End Get

        Set(ByVal AccessValue As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            CharacterSkillsAccess = AccessValue
        End Set
    End Property

    Property StandingsAccess() As Boolean
        Get
            Return CharacterStandingsAccess
        End Get

        Set(ByVal AccessValue As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            CharacterStandingsAccess = AccessValue
        End Set
    End Property

#End Region

End Class