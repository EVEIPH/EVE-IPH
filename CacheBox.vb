Imports System.Data.SQLite

Public Class CacheBox

    Private Structure CacheData
        Dim FieldName As String
        Dim TableName As String
        Dim IDName As String
    End Structure

    ' Updates the database with the cache date for the type sent
    Public Sub UpdateCacheDate(ByVal UpdateCacheType As CacheDateType, ByVal SentDate As Date, Optional ByVal CharacterID As Long = 0)
        Dim SQL As String
        Dim UpdateInfo As CacheData

        UpdateInfo = GetCacheUpdateInfo(UpdateCacheType)

        ' If we don't have a record in the public cache dates, add one
        If UpdateInfo.TableName = "ESI_PUBLIC_CACHE_DATES" Then
            Dim rsCheck As SQLiteDataReader
            DBCommand = New SQLiteCommand("SELECT * FROM ESI_PUBLIC_CACHE_DATES", EVEDB.DBREf)
            rsCheck = DBCommand.ExecuteReader

            If Not rsCheck.HasRows Then
                ' Insert a record
                EVEDB.ExecuteNonQuerySQL("INSERT INTO ESI_PUBLIC_CACHE_DATES VALUES (NULL,NULL,NULL)")
            End If
            rsCheck.Close()
        End If

        If UpdateInfo.FieldName <> "" Then
            ' Update the cache date
            SQL = String.Format("UPDATE {0} SET {1} = '" & Format(SentDate, SQLiteDateFormat) & "'", UpdateInfo.TableName, UpdateInfo.FieldName)
            If UpdateInfo.IDName <> "" Then
                SQL = String.Format(SQL & " WHERE {0} = {1}", UpdateInfo.IDName, CharacterID)
            End If

            Call EVEDB.ExecuteNonQuerySQL(SQL)

        End If

    End Sub

    Public Function DataUpdateable(ByVal UpdateCacheType As CacheDateType, Optional ByVal CharacterID As Long = 0) As Boolean
        Dim SQL As String
        Dim rsDate As SQLiteDataReader
        Dim UpdateInfo As CacheData

        UpdateInfo = GetCacheUpdateInfo(UpdateCacheType)

        If UpdateInfo.FieldName <> "" Then
            SQL = String.Format("SELECT {0} FROM {1}", UpdateInfo.FieldName, UpdateInfo.TableName)

            If UpdateInfo.IDName <> "" Then
                SQL = String.Format(SQL & " WHERE {0} = {1}", UpdateInfo.IDName, CharacterID)
            End If

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsDate = DBCommand.ExecuteReader

            If rsDate.Read Then
                If Not IsDBNull(rsDate.GetValue(0)) Then
                    If CDate(rsDate.GetString(0)) <= DateTime.UtcNow Then
                        Return True ' Time to update
                    Else
                        Return False ' Don't update yet
                    End If
                End If
            End If
        End If

        Return True ' Always update if we don't have a date

    End Function

    Private Function GetCacheUpdateInfo(ByVal CacheType As CacheDateType) As CacheData
        Dim ReturnData As CacheData

        With ReturnData
            Select Case CacheType
                Case CacheDateType.Skills
                    .FieldName = "SKILLS_CACHE_DATE"
                Case CacheDateType.Standings
                    .FieldName = "STANDINGS_CACHE_DATE"
                Case CacheDateType.ResearchAgents
                    .FieldName = "RESEARCH_AGENTS_CACHE_DATE"
                Case CacheDateType.PersonalBlueprints
                    .FieldName = "BLUEPRINTS_CACHE_DATE"
                Case CacheDateType.PersonalAssets
                    .FieldName = "ASSETS_CACHE_DATE"
                Case CacheDateType.PersonalIndyJobs
                    .FieldName = "INDUSTRY_JOBS_CACHE_DATE"
                Case CacheDateType.CorporateBlueprints
                    .FieldName = "BLUEPRINTS_CACHE_DATE"
                Case CacheDateType.CorporateAssets
                    .FieldName = "ASSETS_CACHE_DATE"
                Case CacheDateType.CorporateIndyJobs
                    .FieldName = "INDUSTRY_JOBS_CACHE_DATE"
                Case CacheDateType.PublicCharacterData
                    .FieldName = "PUBLIC_DATA_CACHE_DATE"
                Case CacheDateType.PublicCorporationData
                    .FieldName = "PUBLIC_DATA_CACHE_DATE"
                Case CacheDateType.IndustrySystems
                    .FieldName = "INDUSTRY_SYSTEMS_CACHED_UNTIL"
                Case CacheDateType.MarketPrices
                    .FieldName = "MARKET_PRICES_CACHED_UNTIL"
                Case CacheDateType.PublicStructures
                    .FieldName = "PUBLIC_STRUCTURES_CACHED_UNTIL"
                Case CacheDateType.CorporateRoles
                    .FieldName = "CORP_ROLES_CACHE_DATE"
                Case Else
                    .FieldName = ""
            End Select
        End With

        Select Case CacheType
            Case CacheDateType.CorporateAssets, CacheDateType.CorporateBlueprints, CacheDateType.CorporateIndyJobs, CacheDateType.PublicCorporationData, CacheDateType.CorporateRoles
                ReturnData.TableName = "ESI_CORPORATION_DATA"
                ReturnData.IDName = "CORPORATION_ID"
            Case CacheDateType.IndustryFacilities, CacheDateType.IndustrySystems, CacheDateType.MarketPrices, CacheDateType.PublicStructures
                ReturnData.TableName = "ESI_PUBLIC_CACHE_DATES"
                ReturnData.IDName = ""
            Case Else
                ReturnData.TableName = "ESI_CHARACTER_DATA"
                ReturnData.IDName = "CHARACTER_ID"
        End Select

        Return ReturnData

    End Function

End Class

Public Enum CacheDateType
    Skills = 0
    Standings = 1
    ResearchAgents = 2
    PersonalBlueprints = 3
    PersonalAssets = 4
    PersonalIndyJobs = 5

    CorporateRoles = 14
    CorporateBlueprints = 6
    CorporateAssets = 7
    CorporateIndyJobs = 8

    PublicCharacterData = 9
    PublicCorporationData = 10

    ' Public Cache dates
    IndustrySystems = 11
    IndustryFacilities = 12
    MarketPrices = 13
    PublicStructures = 15

End Enum