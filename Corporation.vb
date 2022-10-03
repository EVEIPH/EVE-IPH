
Imports System.Data.SQLite

Public Class Corporation

    Public Name As String 'Corp name
    Public CorporationID As Long ' Corp ID
    Public Ticker As String
    Public FactionID As Integer
    Public AllianceID As Integer
    Public CEOID As Long
    Public CreatorID As Long
    Public HomeStationID As Integer
    Public Shares As Long
    Public MemberCount As Integer
    Public Description As String
    Public TaxRate As Double
    Public DateFounded As Date
    Public URL As String

    Private Assets As EVEAssets
    Public AssetAccess As Boolean
    Private Blueprints As EVEBlueprints
    Public BlueprintsAccess As Boolean
    Private Jobs As EVEIndustryJobs
    Public JobsAccess As Boolean

    Public Sub New()

        AssetAccess = False
        BlueprintsAccess = False
        JobsAccess = False

        Assets = New EVEAssets
        Blueprints = New EVEBlueprints
        Jobs = New EVEIndustryJobs

    End Sub

    ' Loads the corporation data from token information sent
    Public Sub LoadCorporationData(ByVal CorpID As Long, ByVal CharacterID As Long, ByVal CharacterTokenData As SavedTokenData,
                                   RefreshAssets As Boolean, RefreshBlueprints As Boolean, Optional ResetData As Boolean = True)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader
        Dim CorpRoles As New List(Of String)

        ' Get the public data about corporation and update it
        Call UpdateCorporationData(CorpID)

        ' Load up all the data for the corporation
        SQL = "SELECT * FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & CorpID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        While rsData.Read
            ' Save data
            CorporationID = CorpID
            Name = rsData.GetString(1)
            Ticker = rsData.GetString(2)
            MemberCount = rsData.GetInt32(3)
            FactionID = FormatNullInteger(rsData.GetValue(4))
            AllianceID = FormatNullInteger(rsData.GetValue(5))
            CEOID = rsData.GetInt32(6)
            CreatorID = rsData.GetInt32(7)
            HomeStationID = FormatNullInteger(rsData.GetValue(8))
            Shares = FormatNullLong(rsData.GetValue(9))
            TaxRate = rsData.GetDouble(10)
            Description = FormatNullString(rsData.GetValue(11))
            DateFounded = FormatNullDate(rsData.GetValue(12))
            URL = FormatNullString(rsData.GetValue(13))
        End While

        rsData.Close()

        Dim FactoryManager As Boolean = False
        Dim Director As Boolean = False

        ' See what permissions we have access to in the corporation for this character and only query those we can see
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationMembership) Then
            Dim CharacterCorpRoles As List(Of String) = GetCorporationRoles(CharacterID, CorporationID, CharacterTokenData)

            For Each Role In CharacterCorpRoles
                Select Case Role
                    Case "Factory_Manager"
                        FactoryManager = True
                    Case "Director"
                        Director = True
                End Select
            Next
        End If

        ' Industry Jobs - needs FactoryManager role
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationIndustryJobsScope) And FactoryManager Then
            JobsAccess = True
            If ResetData Then
                Jobs = New EVEIndustryJobs()
                Call Jobs.LoadIndustryJobs(CorporationID, CharacterTokenData, ScanType.Corporation) ' use character ID because only characters can install jobs
            End If
        End If

        ' Blueprints - needs Director role
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationBlueprintsScope) And Director Then
            BlueprintsAccess = True
            If ResetData Then
                Blueprints = New EVEBlueprints()
                Call Blueprints.LoadBlueprints(CorporationID, CharacterTokenData, ScanType.Corporation, RefreshBlueprints)
            End If
        End If

        ' Assets - needs Director role
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationAssetScope) And Director Then
            AssetAccess = True
            If ResetData Then
                Assets = New EVEAssets(ScanType.Corporation)
                Call Assets.LoadAssets(CorporationID, CharacterTokenData, RefreshAssets)
            End If
        End If

    End Sub

    ' Updates the public informaton about the corporation in DB. 
    Private Sub UpdateCorporationData(ByVal CorporationID As Long)
        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Update the corp data if we can
        If CB.DataUpdateable(CacheDateType.PublicCorporationData, CorporationID) Then
            Call ESIData.SetCorporationData(CorporationID, CacheDate)
            ' Update after we update/insert the record
            Call CB.UpdateCacheDate(CacheDateType.PublicCorporationData, CacheDate, CorporationID)
        End If

    End Sub

    ' Loads the dummy corporation into the dummy character
    Public Sub LoadDummyCorporationData()
        Dim SQL As String = ""

        ' Delete the dummy information if in there
        Call EVEDB.ExecuteNonQuerySQL(String.Format("DELETE FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = {0}", DummyCorporationID))

        ' Load the variables
        CorporationID = DummyCorporationID
        Name = None
        Ticker = None
        MemberCount = 1
        FactionID = 0
        AllianceID = 0
        CEOID = 0
        CreatorID = 0
        HomeStationID = 0
        Shares = 0
        TaxRate = 0
        Description = ""
        DateFounded = NoDate
        URL = ""

        SQL = "INSERT INTO ESI_CORPORATION_DATA VALUES ("
        SQL &= BuildInsertFieldString(CorporationID) & ","
        SQL &= BuildInsertFieldString(Name) & ","
        SQL &= BuildInsertFieldString(Ticker) & ","
        SQL &= BuildInsertFieldString(MemberCount) & ","
        SQL &= BuildInsertFieldString(FactionID) & ","
        SQL &= BuildInsertFieldString(AllianceID) & ","
        SQL &= BuildInsertFieldString(CEOID) & ","
        SQL &= BuildInsertFieldString(CreatorID) & ","
        SQL &= BuildInsertFieldString(HomeStationID) & ","
        SQL &= BuildInsertFieldString(Shares) & ","
        SQL &= BuildInsertFieldString(TaxRate) & ","
        SQL &= BuildInsertFieldString(Description) & ","
        SQL &= BuildInsertFieldString(NoDate) & ","
        SQL &= BuildInsertFieldString(URL) & ","
        SQL &= BuildInsertFieldString(NoExpiry) & "," ' Set this here too to stop calls to update dummy corp through ESI
        SQL &= BuildInsertFieldString(NoExpiry) & ","
        SQL &= BuildInsertFieldString(NoExpiry) & ","
        SQL &= BuildInsertFieldString(NoExpiry) & ","
        SQL &= BuildInsertFieldString(NoExpiry) & ")"
        SQL &= BuildInsertFieldString(NoExpiry) & ")"

        ' Insert the dummy corp
        Call EVEDB.ExecuteNonQuerySQL(SQL)

    End Sub

    Private Function GetCorporationRoles(CharacterID As Long, CorporationID As Long, TokenData As SavedTokenData) As List(Of String)
        Dim RoleESI As New ESI
        Dim RoleData As List(Of ESICorporationRoles)
        Dim ReturnRoles As New List(Of String)
        Dim CB As New CacheBox
        Dim CacheDate As Date

        Dim rsRoles As SQLiteDataReader
        Dim SQL As String = ""

        If CB.DataUpdateable(CacheDateType.CorporateRoles, CorporationID) Then
            ' Get all the roles for all characters in corporation. Note, the roles can only be pulled for a character with personnel manager
            RoleData = RoleESI.GetCorporationRoles(CharacterID, CorporationID, TokenData, CacheDate)

            ' Delete current role data and update
            Call EVEDB.ExecuteNonQuerySQL(String.Format("DELETE FROM ESI_CORPORATION_ROLES WHERE CORPORATION_ID = {0} AND CHARACTER_ID = {1}", CorporationID, CharacterID))

            If Not IsNothing(RoleData) Then
                Call EVEDB.BeginSQLiteTransaction()
                ' Check roles - for places they can carry out the role
                ' Grantable is that they can give the role to others
                For Each Character In RoleData
                    ' Insert only role data (read access) for later checks
                    For Each Role In Character.roles
                        Call EVEDB.ExecuteNonQuerySQL(String.Format("INSERT INTO ESI_CORPORATION_ROLES VALUES ({0}, {1},'{2}','Roles')", CorporationID, Character.character_id, FormatDBString(Role)))
                    Next
                Next
                Call EVEDB.CommitSQLiteTransaction()
            End If
            Call CB.UpdateCacheDate(CacheDateType.CorporateRoles, CacheDate, CorporationID)
        End If

        ' Look up roles for the character sent in DB
        SQL = "SELECT ROLE FROM ESI_CORPORATION_ROLES WHERE CORPORATION_ID = {0} AND CHARACTER_ID = {1}"
        DBCommand = New SQLiteCommand(String.Format(SQL, CorporationID, CharacterID), EVEDB.DBREf)
        rsRoles = DBCommand.ExecuteReader

        While rsRoles.Read
            ReturnRoles.Add(rsRoles.GetString(0))
        End While

        rsRoles.Close()

        Return ReturnRoles

    End Function

    Public Function GetIndustryJobs() As EVEIndustryJobs
        Return Jobs
    End Function

    Public Function GetAssets() As EVEAssets
        Return Assets
    End Function

    Public Function GetBlueprints() As EVEBlueprints
        Return Blueprints
    End Function

End Class
