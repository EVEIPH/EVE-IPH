
Imports System.Data.SQLite

Public Class Corporation

    Private Name As String 'Corp name
    Private ID As Long ' Corp ID
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
            'FactionID = FormatNullInteger(rsData.GetValue(4))
            AllianceID = FormatNullInteger(rsData.GetValue(4))
            CEOID = rsData.GetInt32(5)
            CreatorID = rsData.GetInt32(6)
            'HomeStationID = FormatNullInteger(rsData.GetValue(8))
            'Shares = FormatNullLong(rsData.GetValue(9))
            TaxRate = rsData.GetDouble(7)
            Description = FormatNullString(rsData.GetValue(8))
            DateFounded = FormatNullDate(rsData.GetValue(9))
            URL = FormatNullString(rsData.GetValue(10))
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

                ' See if we insert or update
                Dim rsCheck As SQLiteDataReader
                ' Load up all the data for the corporation
                SQL = "SELECT * FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & CorporationID

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    ' Found a record, so update the data
                    With CorpData
                        SQL = "UPDATE ESI_CORPORATION_DATA SET "
                        SQL &= "CORPORATION_NAME = " & BuildInsertFieldString(.name) & ","
                        SQL &= "TICKER = " & BuildInsertFieldString(.ticker) & ","
                        SQL &= "MEMBER_COUNT = " & BuildInsertFieldString(.member_count) & ","
                        'SQL &= "FACTION_ID = " & BuildInsertFieldString(.faction_id) & ","
                        SQL &= "ALLIANCE_ID = " & BuildInsertFieldString(.alliance_id) & ","
                        SQL &= "CEO_ID = " & BuildInsertFieldString(.ceo_id) & ","
                        SQL &= "CREATOR_ID = " & BuildInsertFieldString(.creator_id) & ","
                        'SQL &= "HOME_STATION_ID = " & BuildInsertFieldString(.home_station_id) & ","
                        'SQL &= "SHARES = " & BuildInsertFieldString(.shares) & ","
                        SQL &= "TAX_RATE = " & BuildInsertFieldString(.tax_rate) & ","
                        SQL &= "DESCRIPTION = " & BuildInsertFieldString(.description) & ","
                        SQL &= "DATE_FOUNDED = " & BuildInsertFieldString(.date_founded) & ","
                        SQL &= "URL = " & BuildInsertFieldString(.date_founded) & " "
                        SQL &= "WHERE CORPORATION_ID = " & CStr(CorporationID)
                    End With
                Else
                    ' New record
                    With CorpData
                        SQL = "INSERT INTO ESI_CORPORATION_DATA VALUES ("
                        SQL &= BuildInsertFieldString(CorporationID) & ","
                        SQL &= BuildInsertFieldString(.name) & ","
                        SQL &= BuildInsertFieldString(.ticker) & ","
                        SQL &= BuildInsertFieldString(.member_count) & ","
                        'SQL &= BuildInsertFieldString(.faction_id) & ","
                        SQL &= BuildInsertFieldString(.alliance_id) & ","
                        SQL &= BuildInsertFieldString(.ceo_id) & ","
                        SQL &= BuildInsertFieldString(.creator_id) & ","
                        'SQL &= BuildInsertFieldString(.home_station_id) & ","
                        ' SQL &= BuildInsertFieldString(.shares) & ","
                        SQL &= BuildInsertFieldString(.tax_rate) & ","
                        SQL &= BuildInsertFieldString(.description) & ","
                        SQL &= BuildInsertFieldString(.date_founded) & ","
                        SQL &= BuildInsertFieldString(.url) & ","
                        SQL &= "NULL,NULL,NULL,NULL)"
                    End With

                End If

                Call EVEDB.ExecuteNonQuerySQL(SQL)

                ' Update after we update/insert the record
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
