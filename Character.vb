
Imports System.Data.SQLite

Public Class Character
    Public CachedUntil As Date
    Public KeyID As Long
    Public APIKey As String
    Public AccessMask As Long ' This number will define what access I have to the character's API

    Public ID As Long ' PK
    Public Name As String
    Public HomeStationID As Long
    Public DOB As Date
    Public Race As String
    Public BloodLineID As Integer
    Public BloodLine As String
    Public AncestryLineID As Integer
    Public AncestryLine As String
    Public Gender As String
    Public AllianceName As String
    Public AllianceID As Long
    Public FactionName As String
    Public FactionID As Integer
    Public FreeSkillPoints As Integer
    Public FreeRespecs As Integer
    Public CloneJumpDate As Date
    Public LastRespecDate As Date
    Public LastTimedRespec As Date
    Public RemoteStationDate As Date
    Public JumpActivation As Date
    Public JumpFatigue As Date
    Public JumpLastUpdate As Date
    Public Balance As Double

    Public AttributeMemory As Integer
    Public AttributeIntelligence As Integer
    Public AttributeWillpower As Integer
    Public AttributePerception As Integer
    Public AttributeCharisma As Integer

    Public JumpClones As List(Of JumpClone)
    Public Implants As List(Of Implant)
    Public CorpRoles As List(Of CorporationRole)
    Public CorpTitles As List(Of CorporationTitle)

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
        Gender = ""
        CachedUntil = NoDate
        IsDefault = False
        AccessMask = 0
        APIExpiration = NoDate ' Date when this API key will expire

        HomeStationID = 0
        DOB = NoDate
        Race = ""
        BloodLineID = 0
        BloodLine = ""
        AncestryLineID = 0
        AncestryLine = ""
        Gender = ""
        AllianceName = ""
        AllianceID = 0
        FactionName = ""
        FactionID = 0
        FreeSkillPoints = 0
        FreeRespecs = 0
        CloneJumpDate = NoDate
        LastRespecDate = NoDate
        LastTimedRespec = NoDate
        RemoteStationDate = NoDate
        JumpActivation = NoDate
        JumpFatigue = NoDate
        JumpLastUpdate = NoDate
        Balance = 0

        AttributeMemory = 0
        AttributeIntelligence = 0
        AttributeWillpower = 0
        AttributePerception = 0
        AttributeCharisma = 0

        JumpClones = New List(Of JumpClone)
        Implants = New List(Of Implant)
        CorpRoles = New List(Of CorporationRole)
        CorpTitles = New List(Of CorporationTitle)

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

        DBCommand = New SQLiteCommand("SELECT 'X' FROM API WHERE CHARACTER_ID = 0", EVEDB.DBREf)
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
            Gender = ""
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

            Call evedb.ExecuteNonQuerySQL(SQL)

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
            Call evedb.ExecuteNonQuerySQL(SQL)

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

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerCharacter = DBCommand.ExecuteReader

        If readerCharacter.Read() Then
            NumNewAPIs = readerCharacter.GetInt32(0)
        End If

        SQL = "SELECT COUNT(*) FROM API WHERE CHARACTER_NAME <> 'None'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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
            Call LoadCharacterSheet(False, UpdatedNewData)

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
            Blueprints = New EVEBlueprints(CombinedKeyData, CharacterCorporation.CorporationID)
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

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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

    ' Updates and Loads the character sheet and skills from DB - Should be public for processing skills in the skill window
    Public Sub LoadCharacterSheet(Optional ByVal LoadAllSkillsforOverride As Boolean = False, Optional UpdateAPIData As Boolean = True, Optional SkillNameFilter As String = "")
        Dim rsData As SQLiteDataReader
        Dim SQL As String

        ' First Update the data from API
        Call UpdateCharacterSheet(UpdateAPIData)

        ' Start with character info
        SQL = "SELECT HOME_STATION_ID, DOB, RACE, BLOOD_LINE_ID, BLOOD_LINE, ANCESTRY_LINE_ID, ANCESTRY_LINE, GENDER, ALLIANCE_NAME, ALLIANCE_ID, "
        SQL = SQL & " FACTION_NAME, FACTION_ID, FREE_SKILL_POINTS, FREE_RESPECS, CLONE_JUMP_DATE, LAST_RESPEC_DATE, "
        SQL = SQL & "LAST_TIMED_RESPEC, REMOTE_STATION_DATE, JUMP_ACTIVATION, JUMP_FATIGUE, JUMP_LAST_UPDATE, BALANCE, "
        SQL = SQL & "INTELLIGENCE, MEMORY, WILLPOWER, PERCEPTION, CHARISMA FROM CHARACTER_SHEET WHERE CHARACTER_ID = " & CStr(ID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        ErrorTracker = SQL

        While rsData.Read
            With rsData
                HomeStationID = .GetInt64(0)
                DOB = CDate(.GetString(1))
                Race = .GetString(2)
                BloodLineID = .GetInt32(3)
                BloodLine = .GetString(4)
                AncestryLineID = .GetInt32(5)
                AncestryLine = .GetString(6)
                Gender = .GetString(7)
                AllianceName = .GetString(8)
                AllianceID = .GetInt64(9)
                FactionName = .GetString(10)
                FactionID = .GetInt32(11)
                FreeSkillPoints = .GetInt32(12)
                FreeRespecs = .GetInt32(13)
                CloneJumpDate = CDate(If(.GetValue(14), NoDate))
                LastRespecDate = CDate(If(.GetValue(15), NoDate))
                LastTimedRespec = CDate(If(.GetValue(16), NoDate))
                RemoteStationDate = CDate(If(.GetValue(17), NoDate))
                JumpActivation = CDate(If(.GetValue(18), NoDate))
                JumpFatigue = CDate(If(.GetValue(19), NoDate))
                JumpLastUpdate = CDate(If(.GetValue(20), NoDate))
                Balance = .GetDouble(21)

                AttributeMemory = .GetInt32(22)
                AttributeIntelligence = .GetInt32(23)
                AttributeWillpower = .GetInt32(24)
                AttributePerception = .GetInt32(25)
                AttributeCharisma = .GetInt32(26)
            End With
        End While

        rsData.Close()

        ' Add jump clones
        SQL = "SELECT JUMP_CLONE_ID, LOCATION_ID, TYPE_ID, CLONE_NAME "
        SQL = SQL & "FROM CHARACTER_JUMP_CLONES WHERE CHARACTER_ID = " & CStr(ID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        ErrorTracker = SQL

        While rsData.Read
            With rsData
                Dim TempJC As New JumpClone
                TempJC.JumpCloneID = rsData.GetInt64(0)
                TempJC.LocationID = rsData.GetInt64(1)
                TempJC.TypeID = rsData.GetInt64(2)
                TempJC.CloneName = rsData.GetString(3)

                JumpClones.Add(TempJC)
            End With
        End While

        rsData.Close()

        ' Now implants
        SQL = "SELECT JUMP_CLONE_ID, IMPLANT_ID, IMPLANT_NAME "
        SQL = SQL & "FROM CHARACTER_IMPLANTS WHERE CHARACTER_ID = " & CStr(ID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        ErrorTracker = SQL

        While rsData.Read
            With rsData
                Dim TempImplant As New Implant
                TempImplant.JumpCloneID = rsData.GetInt64(0)
                TempImplant.ImplantID = rsData.GetInt64(1)
                TempImplant.ImplantName = rsData.GetString(2)

                Implants.Add(TempImplant)
            End With
        End While

        rsData.Close()

        ' Corp Roles
        SQL = "SELECT ROLE_TYPE, ROLE_ID, ROLE_NAME "
        SQL = SQL & "FROM CHARACTER_CORP_ROLES WHERE CHARACTER_ID = " & CStr(ID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        ErrorTracker = SQL

        While rsData.Read
            With rsData
                Dim TempRole As New CorporationRole
                TempRole.RoleType = rsData.GetString(0)
                TempRole.RoleID = rsData.GetInt64(1)
                TempRole.RoleName = rsData.GetString(2)

                CorpRoles.Add(TempRole)
            End With
        End While

        rsData.Close()

        ' Corp Titles
        SQL = "SELECT Title_ID, Title_NAME "
        SQL = SQL & "FROM CHARACTER_CORP_TitleS WHERE CHARACTER_ID = " & CStr(ID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        ErrorTracker = SQL

        While rsData.Read
            With rsData
                Dim TempTitle As New CorporationTitle
                TempTitle.TitleID = rsData.GetInt32(0)
                TempTitle.TitleName = rsData.GetString(1)

                CorpTitles.Add(TempTitle)
            End With
        End While

        rsData.Close()

        ' Reset Local Skills Variable
        Skills = New EVESkillList

        ' Get all skills and set skill to 0 if they don't have it
        SQL = "SELECT SKILLS.SKILL_TYPE_ID,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.SKILL_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.SKILL_LEVEL END AS SKILL_LEVEL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.SKILL_POINTS IS NULL THEN 0 ELSE CHAR_SKILLS.SKILL_POINTS END AS SKILL_POINTS,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.OVERRIDE_SKILL IS NULL THEN 0 ELSE CHAR_SKILLS.OVERRIDE_SKILL END AS OVERRIDE_SKILL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.OVERRIDE_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.OVERRIDE_LEVEL END AS OVERRIDE_LEVEL "
        SQL = SQL & "FROM SKILLS LEFT OUTER JOIN "
        SQL = SQL & "(SELECT * FROM CHARACTER_SKILLS WHERE CHARACTER_SKILLS.CHARACTER_ID=" & ID & ") AS CHAR_SKILLS "
        SQL = SQL & "ON (SKILLS.SKILL_TYPE_ID = CHAR_SKILLS.SKILL_TYPE_ID) "
        If SkillNameFilter <> "" Then
            SQL = SQL & " WHERE SKILLS.SKILL_TYPE_ID IN (SELECT SKILL_TYPE_ID FROM SKILLS WHERE SKILL_NAME LIKE '%" & FormatDBString(SkillNameFilter) & "%') "
        End If
        SQL = SQL & "ORDER BY SKILLS.SKILL_GROUP, SKILLS.SKILL_NAME "

        ErrorTracker = SQL

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        While rsData.Read
            ' Insert skill
            If UserApplicationSettings.AllowSkillOverride And CBool(rsData.GetInt32(3)) And LoadAllSkillsforOverride Then
                ' Use the override skill if set, save the old skill level in the override so we can reference it later if needed
                Skills.InsertSkill(rsData.GetInt64(0), rsData.GetInt32(4), rsData.GetInt64(2), CBool(rsData.GetInt32(3)), rsData.GetInt32(1))
            Else ' Just normal skills
                Skills.InsertSkill(rsData.GetInt64(0), rsData.GetInt32(1), rsData.GetInt64(2), CBool(rsData.GetInt32(3)), rsData.GetInt32(4))
            End If

        End While

        rsData.Close()
        rsData = Nothing
        DBCommand = Nothing
        ErrorTracker = ""

    End Sub

    ' Gets the Character Skills from API for this character and inserts them into the Database for later queries
    Private Sub UpdateCharacterSheet(UpdateAPIData As Boolean)
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String
        Dim API As New EVEAPI
        Dim i As Integer
        Dim TempSheet As New CharacterSheet
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
        TempSheet = API.GetCharacterSheet(CombinedKeyData)

        If Not NoAPIError(API.GetErrorText, "Character") Then
            ' Errored, exit
            Exit Sub
        End If

        ' Update the Character sheet first
        ' Delete since this is refreshed each time
        SQL = "DELETE FROM CHARACTER_SHEET WHERE CHARACTER_ID = " & CStr(ID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        With TempSheet
            SQL = "INSERT INTO CHARACTER_SHEET (CHARACTER_ID, CHARACTER_NAME, HOME_STATION_ID, DOB, RACE, BLOOD_LINE_ID, BLOOD_LINE, "
            SQL = SQL & "ANCESTRY_LINE_ID, ANCESTRY_LINE, GENDER, CORPORATION_NAME, CORPORATION_ID, ALLIANCE_NAME, ALLIANCE_ID, FACTION_NAME, "
            SQL = SQL & "FACTION_ID, FREE_SKILL_POINTS, FREE_RESPECS, CLONE_JUMP_DATE, LAST_RESPEC_DATE, LAST_TIMED_RESPEC, REMOTE_STATION_DATE,"
            SQL = SQL & "JUMP_ACTIVATION, JUMP_FATIGUE, JUMP_LAST_UPDATE, BALANCE, INTELLIGENCE, MEMORY, WILLPOWER, PERCEPTION, CHARISMA) VALUES ("
            SQL = SQL & CStr(.CharacterID) & ",'" & FormatDBString(.CharacterName) & "'," & CStr(.HomeStationID) & ",'" & Format(.DOB, SQLiteDateFormat) & "','"
            SQL = SQL & FormatDBString(.Race) & "'," & CStr(.BloodLineID) & ",'" & .BloodLine & "'," & CStr(.AncestryLineID) & ",'" & FormatDBString(.AncestryLine) & "','" & .Gender & "','"
            SQL = SQL & FormatDBString(.CorporationName) & "'," & CStr(.CorporationID) & ",'" & FormatDBString(.AllianceName) & "'," & CStr(.AllianceID) & ",'" & FormatDBString(.FactionName) & "',"
            SQL = SQL & CStr(.FactionID) & "," & CStr(.FreeSkillPoints) & "," & CStr(.FreeRespecs) & ",'" & Format(.CloneJumpDate, SQLiteDateFormat) & "','"
            SQL = SQL & Format(.LastRespecDate, SQLiteDateFormat) & "','" & Format(.LastTimedRespec, SQLiteDateFormat) & "','" & Format(.RemoteStationDate, SQLiteDateFormat) & "','"
            SQL = SQL & Format(.JumpActivation, SQLiteDateFormat) & "','" & Format(.JumpFatigue, SQLiteDateFormat) & "','" & Format(.JumpLastUpdate, SQLiteDateFormat) & "',"
            SQL = SQL & CStr(.Balance) & "," & CStr(.AttributeIntelligence) & "," & CStr(.AttributeMemory) & ","
            SQL = SQL & CStr(.AttributeWillpower) & "," & CStr(.AttributePerception) & "," & CStr(.AttributeCharisma) & ")"
        End With

        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Implants
        SQL = "DELETE FROM CHARACTER_IMPLANTS WHERE CHARACTER_ID = " & CStr(ID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        With TempSheet
            For i = 0 To .Implants.Count - 1
                SQL = "INSERT INTO CHARACTER_IMPLANTS (CHARACTER_ID, JUMP_CLONE_ID, IMPLANT_ID, IMPLANT_NAME) "
                SQL = SQL & " VALUES (" & CStr(.CharacterID) & "," & CStr(.Implants(i).JumpCloneID) & "," & CStr(.Implants(i).ImplantID) & ",'" & FormatDBString(.Implants(i).ImplantName) & "')"
            Next
        End With

        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Jump Clones
        SQL = "DELETE FROM CHARACTER_JUMP_CLONES WHERE CHARACTER_ID = " & CStr(ID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        With TempSheet
            For i = 0 To .JumpClones.Count - 1
                SQL = "INSERT INTO CHARACTER_JUMP_CLONES (CHARACTER_ID, JUMP_CLONE_ID, LOCATION_ID, TYPE_ID, CLONE_NAME) "
                SQL = SQL & " VALUES (" & CStr(.CharacterID) & "," & CStr(.JumpClones(i).JumpCloneID) & "," & CStr(.JumpClones(i).LocationID) & "," & CStr(.JumpClones(i).TypeID) & ",'" & FormatDBString(.JumpClones(i).CloneName) & "')"
            Next
        End With

        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Corp Roles
        SQL = "DELETE FROM CHARACTER_CORP_ROLES WHERE CHARACTER_ID = " & CStr(ID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        With TempSheet
            For i = 0 To .CorporationRoles.Count - 1
                SQL = "INSERT INTO CHARACTER_CORP_ROLES (CHARACTER_ID, ROLE_TYPE, ROLE_ID, ROLE_NAME) "
                SQL = SQL & " VALUES (" & CStr(.CharacterID) & ",'" & .CorporationRoles(i).RoleType & "'," & CStr(.CorporationRoles(i).RoleID) & ",'" & FormatDBString(.CorporationRoles(i).RoleName) & "')"
            Next
        End With

        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Corp Titles
        SQL = "DELETE FROM CHARACTER_CORP_TITLES WHERE CHARACTER_ID = " & CStr(ID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        With TempSheet
            For i = 0 To .CorporationTitles.Count - 1
                SQL = "INSERT INTO CHARACTER_CORP_TITLES (CHARACTER_ID, TITLE_ID, TITLE_NAME) "
                SQL = SQL & " VALUES (" & CStr(.CharacterID) & "," & CStr(.CorporationTitles(i).TitleID) & ",'" & FormatDBString(.CorporationTitles(i).TitleName) & "')"
            Next
        End With

        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Now skills
        ' Clean out any skills not in the temp skills, make this first. This will ignore any skills the person may have over-ridden and added
        For i = 0 To TempSheet.CharacterSkills.GetSkillList.Count - 1
            SkillList = SkillList & TempSheet.CharacterSkills.GetSkillList(i).TypeID & ","
        Next

        ' Strip comma
        SkillList = SkillList.Substring(0, Len(SkillList) - 1)

        ' Delete the temp skills but not any that are overridden
        SQL = "DELETE FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID IN (" & SkillList & ") AND CHARACTER_ID =" & CStr(ID)
        SQL = SQL & " AND OVERRIDE_SKILL <> -1"
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        Call EVEDB.BeginSQLiteTransaction()

        ' Insert skill data
        For i = 0 To TempSheet.CharacterSkills.GetSkillList.Count - 1

            ' Check for skill and update if there
            SQL = "SELECT 'X' FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & TempSheet.CharacterSkills.GetSkillList(i).TypeID & " AND CHARACTER_ID =" & CStr(ID)

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerCharacter = DBCommand.ExecuteReader

            If Not readerCharacter.HasRows Then
                ' Insert skill data
                SQL = "INSERT INTO CHARACTER_SKILLS (CHARACTER_ID, SKILL_TYPE_ID, SKILL_NAME, SKILL_POINTS, SKILL_LEVEL, OVERRIDE_SKILL, OVERRIDE_LEVEL) "
                SQL = SQL & " VALUES (" & ID & "," & TempSheet.CharacterSkills.GetSkillList(i).TypeID & ",'" & TempSheet.CharacterSkills.GetSkillList(i).Name & "',"
                SQL = SQL & TempSheet.CharacterSkills.GetSkillList(i).SkillPoints & "," & TempSheet.CharacterSkills.GetSkillList(i).Level & ",0,0)"
            Else
                ' Update skill data
                SQL = "UPDATE CHARACTER_SKILLS SET "
                SQL = SQL & "SKILL_TYPE_ID = " & TempSheet.CharacterSkills.GetSkillList(i).TypeID & ", SKILL_NAME = '" & TempSheet.CharacterSkills.GetSkillList(i).Name & "',"
                SQL = SQL & "SKILL_POINTS = " & TempSheet.CharacterSkills.GetSkillList(i).SkillPoints & ", SKILL_LEVEL = " & TempSheet.CharacterSkills.GetSkillList(i).Level & " "
                SQL = SQL & "WHERE CHARACTER_ID = " & ID & " AND SKILL_TYPE_ID = " & TempSheet.CharacterSkills.GetSkillList(i).TypeID
            End If

            readerCharacter.Close()
            readerCharacter = Nothing

            Call EVEDB.ExecuteNonQuerySQL(SQL)

        Next

        Call EVEDB.CommitSQLiteTransaction()

    End Sub

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

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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

        Call EVEDB.BeginSQLiteTransaction()

        ' Delete the old standings data
        SQL = "DELETE FROM CHARACTER_STANDINGS WHERE CHARACTER_ID = " & ID
        Call evedb.ExecuteNonQuerySQL(SQL)

        If Not IsNothing(TempStandings) Then

            ' Insert skill data
            For i = 0 To TempStandings.NumStandings - 1
                SQL = "INSERT INTO CHARACTER_STANDINGS (CHARACTER_ID, NPC_TYPE_ID, NPC_TYPE, NPC_NAME, STANDING) "
                SQL = SQL & " VALUES (" & ID & "," & TempStandings.GetStandingsList(i).NPCID
                SQL = SQL & ",'" & TempStandings.GetStandingsList(i).NPCType
                SQL = SQL & "','" & FormatDBString(TempStandings.GetStandingsList(i).NPCName)
                SQL = SQL & "'," & TempStandings.GetStandingsList(i).Standing & ")"
                Call evedb.ExecuteNonQuerySQL(SQL)
            Next

            DBCommand = Nothing
        End If

        Call EVEDB.CommitSQLiteTransaction()

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