
Imports System.Data.SQLite

Public Class Corporation

    Private Name As String 'Corp name
    Private ID As Long ' Corp ID
    Public Ticker As String
    Public MemberCount As Integer
    Public CEOID As Long
    Public CreatorID As Long
    Public AllianceID As Integer
    Public Description As String
    Public TaxRate As Double
    Public DateFounded As Date
    Public URL As String

    Private Assets As EVEAssets
    Public AssetAccess As Boolean
    Private Blueprints As EVEBlueprints
    Public BlueprintAccess As Boolean
    Private Jobs As EVEIndustryJobs
    Public JobsAccess As Boolean

    Public Sub New()

        AssetAccess = False
        BlueprintAccess = False
        JobsAccess = False

    End Sub

    ' Loads the corporation data from token information sent
    Public Sub LoadCorporationData(ByVal CorporationID As Long, ByVal CharacterID As Long, ByVal CharacterTokenData As SavedTokenData,
                                   RefreshAssets As Boolean, RefreshBlueprints As Boolean)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader

        ' Get the public data about corporation and update it
        Call UpdateCorporationData(CorporationID, CharacterTokenData)

        ' Load up all the data for the corporation
        SQL = "SELECT * FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & CorporationID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        While rsData.Read
            ' Save data
            ID = CorporationID
            Name = rsData.GetString(1)
            Ticker = rsData.GetString(2)
            MemberCount = rsData.GetInt32(3)
            If Not IsDBNull(rsData.GetValue(4)) Then
                AllianceID = rsData.GetInt32(4)
            Else
                AllianceID = 0
            End If
            CEOID = rsData.GetInt32(5)
            CreatorID = rsData.GetInt32(6)
            TaxRate = rsData.GetDouble(7)
            Description = rsData.GetString(8)
            DateFounded = CDate(rsData.GetString(9))
            URL = rsData.GetString(10)
        End While

        rsData.Close()
        DBCommand = Nothing
        rsData = Nothing

        ' Industry Jobs
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationIndustryJobsScope) Then
            JobsAccess = True
            Jobs = New EVEIndustryJobs()
            Call Jobs.UpdateIndustryJobs(CorporationID, CharacterTokenData, ScanType.Corporation) ' use character ID because only characters can install jobs
        End If

        ' Blueprints
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationBlueprintsScope) Then
            BlueprintAccess = True
            Blueprints = New EVEBlueprints()
            Call Blueprints.LoadBlueprints(CorporationID, CharacterTokenData, ScanType.Corporation, RefreshBlueprints)
        End If

        ' Assets
        If CharacterTokenData.Scopes.Contains(ESI.ESICorporationAssetScope) Then
            AssetAccess = True
            Assets = New EVEAssets(ScanType.Corporation)
            Call Assets.LoadAssets(CorporationID, CharacterTokenData, RefreshAssets)
        End If

    End Sub

    ' Updates the public informaton about the corporation in DB
    Private Sub UpdateCorporationData(ByVal CorporationID As Long, ByVal CharacterTokenData As SavedTokenData)
        Dim SQL As String
        Dim CorpData As ESICorporation = Nothing

        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Update the corp data if we can
        If CB.DataUpdateable(CacheDateType.PublicCorporationData, CorporationID) Then
            CorpData = ESIData.GetCorporationData(CorporationID, CacheDate)

            If Not IsNothing(CorpData) Then
                Call EVEDB.BeginSQLiteTransaction()

                ' Delete the old standings data
                SQL = "DELETE FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & CorporationID
                Call EVEDB.ExecuteNonQuerySQL(SQL)

                ' Insert new standings data
                With CorpData
                    SQL = "INSERT INTO ESI_CORPORATION_DATA (CORPORATION_ID, CORPORATION_NAME, TICKER,  "
                    SQL &= "MEMBER_COUNT, CEO_ID, CREATOR_ID, TAX_RATE, DESCRIPTION, DATE_FOUNDED, URL)"
                    SQL &= " VALUES (" & CorporationID & ",'" & FormatDBString(.name) & "','" & .ticker & "'," & .member_count & "," & .ceo_id & ","
                    SQL &= .creator_id & "," & .tax_rate & ",'" & FormatDBString(.description) & "','" & .date_founded & "','" & .url & "')"
                    Call EVEDB.ExecuteNonQuerySQL(SQL)
                End With

                ' Update after we insert the record
                Call CB.UpdateCacheDate(CacheDateType.PublicCorporationData, CacheDate, CorporationID)

                Call EVEDB.CommitSQLiteTransaction()

                DBCommand = Nothing

            End If
        End If
    End Sub

    Public Function GetIndustryJobs() As EVEIndustryJobs
        Return Jobs
    End Function

    Public Function GetAssets() As EVEAssets
        Return Assets
    End Function

    Public Function GetBlueprints() As EVEBlueprints
        Return Blueprints
    End Function

    ReadOnly Property CorporationID() As Long
        Get
            Return ID
        End Get
    End Property

    ReadOnly Property CorporationName() As String
        Get
            Return Name
        End Get
    End Property

End Class
