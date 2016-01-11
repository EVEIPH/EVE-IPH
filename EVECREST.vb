
Imports System.IO
Imports System.Data.SQLite
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

' Class for all CREST function calls, which will update tables in the DB if past cache time
Public Class EVECREST
    'Private Const CRESTRootServerURL = "https://public-crest-sisi.testeveonline.com"
    Private Const CRESTRootServerURL = "https://public-crest.eveonline.com"

    ' URL for each file
    Private Const CRESTIndustryTeamSpecialties = "/industry/specialities/"
    Private Const CRESTIndustryTeams = "/industry/teams/"
    Private Const CRESTIndustryTeamAuctions = "/industry/teams/auction/"
    Private Const CRESTIndustrySystems = "/industry/systems/"
    Private Const CRESTIndustryFacilities = "/industry/facilities/"
    Private Const CRESTMarketPrices = "/market/prices/"

    ' Cache field names and times
    Private Const IndustryTeamSpecialtiesCacheDateField As String = "CREST_INDUSTRY_SPECIALIZATIONS_CACHED_UNTIL"
    Private Const IndustryTeamSpecalitiesCacheDateLength As Integer = 1
    Private Const IndustryTeamsCacheDateField As String = "CREST_INDUSTRY_TEAMS_CACHED_UNTIL"
    Private Const IndustryTeamsCacheDateLength As Integer = 1
    Private Const IndustryTeamAuctionsField As String = "CREST_INDUSTRY_TEAM_AUCTIONS_CACHED_UNTIL"
    Private Const IndustryTeamAuctionsLength As Integer = 1
    Private Const IndustrySystemsField As String = "CREST_INDUSTRY_SYSTEMS_CACHED_UNTIL"
    Private Const IndustrySystemsLength As Integer = 1
    Private Const IndustryFacilitiesField As String = "CREST_INDUSTRY_FACILITIES_CACHED_UNTIL"
    Private Const IndustryFacilitiesLength As Integer = 1
    Private Const MarketPricesField As String = "CREST_MARKET_PRICES_CACHED_UNTIL"
    Private Const MarketPricesLength As Integer = 23

    ' For looking up market order data
    Public StationsData As StationLocation

    ' Rate limits
    'For your requests, this means you can send an occasional burst of 400 requests all at once. 
    'If you do, you'll hit the rate limit once you try to send your 401st request unless you wait.

    'Your bucket refills at a rate of 1 per 1/150th of a second. If you send 400 requests at once, 
    'you need to wait 2.67 seconds before you can send another 400 requests (1/150 * 400), if you 
    'only wait 1.33 seconds you can send another 200, and so on. Altrnatively, you can send a constant 150 requests every 1 second. 
    Private Const CRESTRatePerSecond As Integer = 150 ' max requests per second
    Private Const CRESTBurstSize As Integer = 400 ' max burst of requests, which need 2.46 seconds to refill before re-bursting
    Private Const CRESTMaximumConnections As Integer = 20

    Public Sub New(Optional LoadStationData As Boolean = False)
        If LoadStationData Then
            StationsData = New StationLocation
        End If
    End Sub

    Public Function GetRatePerSecond() As Integer
        Return CRESTRatePerSecond
    End Function

    Public Function GetBurstSize() As Integer
        Return CRESTBurstSize
    End Function

    Public Function GetMaximumConnections() As Integer
        Return CRESTMaximumConnections
    End Function

    ' Implements limiting on the calls to CREST - Total records is the total you are going to update, call count is number already called, returns boolean if slept
    Public Function LimitCRESTCalls(RequestStart As Date, TotalRecordCount As Integer, CallCount As Integer) As Boolean

        ' If we aren't as big as the burst size, then we don't need to limit our calls
        If TotalRecordCount <= CRESTBurstSize Then
            Return False
        End If

        ' Check to see if we hit the maximum calls per second, if so, check for time to sleep
        If CallCount = CRESTRatePerSecond Then
            ' Need to see if we are over the time limit and sleep
            ' Figure out the difference between the max time for max requests and our requests (in milliseconds)
            Dim Difference As Integer = CInt((1000 - ((Now - RequestStart).TotalMilliseconds)))
            If Difference > 0 Then
                Threading.Thread.Sleep(Difference)
            End If
            Return True
        Else
            Return False
        End If

    End Function

    ' Uses the type from a crest call - replace buy-sell with buy or sell
    ' market/{region_id}/orders/buy-sell/?type=https://public-crest.eveonline.com/types/{type_id}/ (cache: x hours)
    'https://public-crest.eveonline.com/market/10000002/orders/sell/?type=https://public-crest.eveonline.com/types/34/
    ' Provides the current buy or sell orders on the market region for the type id sent - same as in game view

    ' Gets the CREST file from CCP for the current Market orders (buy and sell) for the region_id and type_id sent
    ' Open transaction will open an SQL transaction here instead of the calling function
    ' Returns boolean if the history was updated or not
    Public Function UpdateMarketOrders(ByRef MHDB As DBConnection, ByVal TypeID As Long, ByVal RegionID As Long, _
                                    Optional OpenTransaction As Boolean = True, _
                                    Optional IgnoreCacheLookup As Boolean = False) As Boolean
        Dim MarketOrdersOutput As MarketOrders
        Dim SQL As String
        Dim rsCache As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader
        Dim CacheDate As Date = NoDate
        Dim ReturnCacheDate As Date = NoDate
        Dim OrderType As String = ""

        If Not IgnoreCacheLookup Then
            ' First look up the cache date to see if it's time to run the update
            SQL = "SELECT CACHE_DATE FROM MARKET_ORDERS_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
            DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
            rsCache = DBCommand.ExecuteReader

            CacheDate = ProcessCacheDate(rsCache)

            rsCache.Close()
            rsCache = Nothing
            DBCommand = Nothing
        Else
            CacheDate = NoDate
        End If

        ' If it's later than now, update
        If CacheDate <= Now Then
            ' Always open here incase we update below
            If OpenTransaction Then
                Call MHDB.BeginSQLiteTransaction()
            End If

            ' Delete any records for this type and region since we have a fresh set to load
            Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_ORDERS WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))

            ' Do two loops, one for buy, one for sell
            For counter = 0 To 1
                Application.DoEvents()

                If counter = 0 Then
                    OrderType = "buy"
                Else
                    OrderType = "sell"
                End If

                ' Dump the file into the Specializations object
                MarketOrdersOutput = JsonConvert.DeserializeObject(Of MarketOrders) _
                    (GetJSONFile(CRESTRootServerURL & "/market/" & CStr(RegionID) & "/orders/" & OrderType & "/?type=https://public-crest.eveonline.com/types/" & CStr(TypeID) & "/", ReturnCacheDate, "Market History", True))

                ' Read in the data
                If Not IsNothing(MarketOrdersOutput) Then
                    ' Parse the data
                    If MarketOrdersOutput.items.Count > 0 Then
                        Application.DoEvents()

                        ' Now read through all the output items that are not in the table insert them in MARKET_ORDERS
                        For i = 0 To MarketOrdersOutput.totalCount - 1
                            With MarketOrdersOutput.items(i)
                                Dim StationLocation As SystemRegion
                                Dim OrderDownloadType As String = ""

                                StationLocation = StationsData.FindStationInfo(.location.id)

                                If .buy Then
                                    OrderDownloadType = "'BUY'"
                                Else
                                    OrderDownloadType = "'SELL'"
                                End If

                                ' Insert all the new records
                                SQL = "INSERT INTO MARKET_ORDERS VALUES (" & CStr(TypeID) & "," & CStr(StationLocation.RegionID) & ","
                                SQL = SQL & CStr(StationLocation.SystemID) & ",'" & .issued.Replace("T", " ") & "',"
                                SQL = SQL & .duration_str & "," & OrderDownloadType & "," & CStr(.price) & "," & .volumeEntered_str & ","
                                SQL = SQL & .minVolume_str & "," & .volume_str & ")"
                                Call MHDB.ExecuteNonQuerySQL(SQL)

                            End With

                            Application.DoEvents()
                        Next

                    End If

                Else
                    ' Json file didn't download
                    Return False
                End If
            Next

            ' Set the Cache Date for everything queried 
            Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_ORDERS_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))
            Call MHDB.ExecuteNonQuerySQL("INSERT INTO MARKET_ORDERS_UPDATE_CACHE VALUES (" & CStr(TypeID) & "," & CStr(RegionID) & "," & "'" & Format(ReturnCacheDate, SQLiteDateFormat) & "')")

            ' Done updating
            If OpenTransaction Then
                Call MHDB.CommitSQLiteTransaction()
            End If

            rsCache = Nothing
            rsCheck = Nothing
            DBCommand = Nothing

            Return True

        Else
            Return False
        End If

        Return True

    End Function

    ' For MarketOrders
    Private Class MarketOrders
        '{"totalCount_str": "182", "items": [], "pageCount": 1, "pageCount_str": "1", "totalCount": 182}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As MarketOrder
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Market Orders
    Private Class MarketOrder
        '{"volume_str": "838037", "buy": false, "issued": "2015-12-28T15:25:24", "price": 6.3, "volumeEntered": 1700062, "minVolume": 1, "volume": 838037, "range": "region", 
        '"href": "https://public-crest.eveonline.com/market/10000002/orders/4379176393/", "duration_str": "7", "location": {}, 
        '"duration": 7, "minVolume_str": "1", "volumeEntered_str": "1700062", "type": {}, "id": 4379176393, "id_str": "4379176393"}

        <JsonProperty("volume_str")> Public volume_str As String
        <JsonProperty("buy")> Public buy As Boolean
        <JsonProperty("issued")> Public issued As String ' date
        <JsonProperty("price")> Public price As Double
        <JsonProperty("volumeEntered")> Public volumeEntered As Long
        <JsonProperty("minVolume")> Public minVolume As Integer
        <JsonProperty("volume")> Public volume As Long
        <JsonProperty("range")> Public range As String
        <JsonProperty("href")> Public href As String
        <JsonProperty("duration_str")> Public duration_str As String
        <JsonProperty("location")> Public location As MarketLocation
        <JsonProperty("duration")> Public duration As Integer
        <JsonProperty("minVolume_str")> Public minVolume_str As String
        <JsonProperty("volumeEntered_str")> Public volumeEntered_str As String
        <JsonProperty("type")> Public type As MarketPriceType
        <JsonProperty("id")> Public id As Long
        <JsonProperty("id_str")> Public id_str As String
    End Class

    ' For Market Orders
    Private Class MarketLocation
        '"location": {"id_str": "60005596", "href": "https://public-crest.eveonline.com/universe/locations/60005596/", "id": 60005596, "name": "Itamo VIII - Moon 13 - Core Complexion Inc. Factory"},
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("href")> Public href As String
        <JsonProperty("id")> Public id As Long
        <JsonProperty("name")> Public name As String
    End Class

    ' market/{region_id}/types/{type_id}/history/ (cache: 23 hours)
    'https://public-crest.eveonline.com/market/10000002/types/34/history/
    ' Provides per day summary of market activity for 13 months for the region_id and type_id sent.

    ' Gets the CREST file from CCP for current Market History and updates the EVEIPH DB with the values
    ' Open transaction will open an SQL transaction here instead of the calling function
    ' Returns boolean if the history was updated or not
    Public Function UpdateMarketHistory(ByRef MHDB As DBConnection, ByVal TypeID As Long, ByVal RegionID As Long, _
                                        Optional ByRef IgnoreCacheLookup As Boolean = False, Optional OpenTransaction As Boolean = False) As Boolean
        Dim MarketPricesOutput As MarketHistory
        Dim SQL As String
        Dim rsCache As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader
        Dim CacheDate As Date = NoDate
        Dim MaxRecordDate As Date = NoDate
        Dim ReturnCacheDate As Date = NoDate

        If Not IgnoreCacheLookup Then
            ' First look up the cache date to see if it's time to run the update
            SQL = "SELECT CACHE_DATE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
            DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
            rsCache = DBCommand.ExecuteReader

            CacheDate = ProcessCacheDate(rsCache)

            rsCache.Close()
            rsCache = Nothing
            DBCommand = Nothing
        Else
            CacheDate = NoDate
        End If

        ' If it's later than now, update
        If CacheDate <= Now Then
            ' Always open here incase we update below
            If OpenTransaction Then
                Call MHDB.BeginSQLiteTransaction()
            End If

            Application.DoEvents()
            ' Dump the file into the Specializations object
            MarketPricesOutput = JsonConvert.DeserializeObject(Of MarketHistory) _
                (GetJSONFile(CRESTRootServerURL & "/market/" & CStr(RegionID) & "/types/" & CStr(TypeID) & "/history/", ReturnCacheDate, "Market History", True))

            ' Read in the data
            If Not IsNothing(MarketPricesOutput) Then

                If MarketPricesOutput.items.Count > 0 Then
                    ' See what the last cache date we have on the records first - any records after or equal to this date we want to update
                    If CacheDate = NoDate Then ' only run this if we don't already have the max date for this typeid
                        SQL = "SELECT CACHE_DATE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
                        DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
                        rsCheck = DBCommand.ExecuteReader

                        If rsCheck.Read And Not IsDBNull(rsCheck.GetValue(0)) Then
                            ' The cache date is the date when we run the next update
                            MaxRecordDate = CDate(rsCheck.GetString(0))
                        Else
                            MaxRecordDate = NoDate
                        End If
                        rsCheck.Close()
                    Else
                        MaxRecordDate = CacheDate
                    End If

                    Application.DoEvents()
                    Dim i As Integer

                    ' Now read through all the output items that are not in the table insert them in MARKET_HISTORY
                    For i = 0 To MarketPricesOutput.totalCount - 1
                        With MarketPricesOutput.items(i)
                            ' only insert the records that are larger than the max date (with no time or 0:00:00 in GMT when records are updated)
                            If CDate(.date_str.Replace("T", " ")).Date > MaxRecordDate.Date Then
                                SQL = "INSERT INTO MARKET_HISTORY VALUES (" & CStr(TypeID) & "," & CStr(RegionID) & ",'" & .date_str.Replace("T", " ") & "',"
                                SQL = SQL & CStr(.lowPrice) & "," & CStr(.highPrice) & "," & CStr(.avgPrice) & "," & .orderCount_str & "," & .volume_str & ")"
                                Call MHDB.ExecuteNonQuerySQL(SQL)
                            End If
                        End With

                        Application.DoEvents()
                    Next
                End If

                ' Set the Cache Date for everything queried 
                Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))
                Call MHDB.ExecuteNonQuerySQL("INSERT INTO MARKET_HISTORY_UPDATE_CACHE VALUES (" & CStr(TypeID) & "," & CStr(RegionID) & "," & "'" & Format(ReturnCacheDate, SQLiteDateFormat) & "')")

                ' Done updating
                If OpenTransaction Then
                    Call MHDB.CommitSQLiteTransaction()
                End If

                Return True

            End If
            ' Json file didn't download
            Return False
        Else
            Return False
        End If

        Return True

    End Function

    ' For Market History
    Private Class MarketHistory
        '{"totalCount_str": "414", "items": [], "pageCount": 1, "pageCount_str": "1", "totalCount": 414}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As MarketPriceItems
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Market History
    Private Class MarketPriceItems
        '{"volume_str": "28662910175", "orderCount": 4312, "lowPrice": 4.98, "highPrice": 5.04, "avgPrice": 5.0, "volume": 28662910175, "orderCount_str": "4312", "date": "2014-02-01T00:00:00"}
        <JsonProperty("volume_str")> Public volume_str As String
        <JsonProperty("orderCount")> Public orderCount As Long
        <JsonProperty("lowPrice")> Public lowPrice As Double
        <JsonProperty("highPrice")> Public highPrice As Double
        <JsonProperty("avgPrice")> Public avgPrice As Double
        <JsonProperty("volume")> Public volume As Long
        <JsonProperty("orderCount_str")> Public orderCount_str As String
        <JsonProperty("date")> Public date_str As String
    End Class

    ' Current endpoints as of July 22, 2014

    ' /industry/specialities/ (cache: 1 hour)
    ' vnd.ccp.eve.IndustrySpecialityCollection-v1
    ' Lists all specialties that can be associated with teams and what groups those specialties modify.

    ' Gets the CREST file from CCP for current Industry Specialties and updates the EVEIPH DB with the values
    Public Function UpdateIndustrySpecialties(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim TeamSpecialtiesOutput As TeamSpecializations
        Dim SQL As String

        ' Data Variables
        Dim SpecializationName As String
        Dim SpecializationID As Integer
        Dim GroupID As Integer
        Dim CacheDate As Date

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' First look up the cache date to see if it's time to run the update
        CacheDate = GetCRESTCacheDate(IndustryTeamSpecialtiesCacheDateField)

        ' If it's later than now, update
        If CacheDate <= Now Then

            TempLabel.Text = "Downloading Team Specialty Data..."
            Application.DoEvents()

            ' Dump the file into the Specializations object
            TeamSpecialtiesOutput = JsonConvert.DeserializeObject(Of TeamSpecializations) _
                (GetJSONFile(CRESTRootServerURL & CRESTIndustryTeamSpecialties, CacheDate, "Industry Specialities"))

            ' Read in the data
            If Not IsNothing(TeamSpecialtiesOutput) Then
                If TeamSpecialtiesOutput.items.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Delete the old records first
                    Call evedb.ExecuteNonQuerySQL("DELETE FROM INDUSTRY_GROUP_SPECIALTIES")
                    Call evedb.ExecuteNonQuerySQL("DELETE FROM INDUSTRY_CATEGORY_SPECIALTIES")

                    ' Now read through all the output items and input them into the DB
                    For i = 0 To TeamSpecialtiesOutput.totalCount - 1

                        TempLabel.Text = "Saving Team Specialty Data..."
                        TempPB.Minimum = 0
                        TempPB.Value = 0
                        TempPB.Maximum = TeamSpecialtiesOutput.totalCount - 1
                        TempPB.Visible = True
                        Application.DoEvents()

                        SpecializationName = TeamSpecialtiesOutput.items(i).name
                        SpecializationID = TeamSpecialtiesOutput.items(i).id

                        ' Now loop through all the items and get the group IDs that this specialization uses. Group ID is the invGroups ID for linking to items
                        If TeamSpecialtiesOutput.items(i).groups.Count <> 0 Then
                            For j = 0 To TeamSpecialtiesOutput.items(i).groups.Count - 1
                                GroupID = TeamSpecialtiesOutput.items(i).groups(j).id

                                SQL = "INSERT INTO INDUSTRY_GROUP_SPECIALTIES VALUES(" & GroupID & "," & SpecializationID & ",'" & SpecializationName & "')"
                                Call evedb.ExecuteNonQuerySQL(SQL)

                            Next
                        Else
                            ' Insert the record with no groups - these are the category of the team, which then can do groups of items in that category
                            SQL = "INSERT INTO INDUSTRY_CATEGORY_SPECIALTIES VALUES(" & SpecializationID & ",'" & SpecializationName & "')"
                            Call evedb.ExecuteNonQuerySQL(SQL)
                        End If

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()

                    Next

                    ' Rebuild indexes
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_CAT_ID")

                    ' Set the Cache Date to now plus the length since it's not sent in the file
                    Call SetCRESTCacheDate(IndustryTeamSpecialtiesCacheDateField, CacheDate)
                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()

                    Return True

                End If
            End If
            ' Json file didn't download
            Return False
        End If

        Return True

    End Function

    ' For Team Specalizations
    Private Class TeamSpecializations

        '"{"totalCount_str": "141", "items": [], "pageCount": 1, "pageCount_str": "1", "totalCount": 141}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As TeamSpecializationsItem
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer

    End Class

    ' For Team Specalizations
    Private Class TeamSpecializationsItem
        '{"id_str": "0", "id": 0, "groups": [], "name": "Specializations"}
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("id")> Public id As Integer
        <JsonProperty("groups")> Public groups() As TeamSpecializationsGroupIDs
        <JsonProperty("name")> Public name As String
    End Class

    ' For Team Specalizations
    Private Class TeamSpecializationsGroupIDs
        '{"id": 838, "id_str": "838"}, 
        <JsonProperty("id")> Public id As Integer
        <JsonProperty("id_str")> Public id_str As String
    End Class

    ' /industry/teams/ (cache: 1 hour)
    ' vnd.ccp.eve.IndustryTeamCollection-v1
    ' Returns a list of all active teams, excluding the teams in auction. This does not include wormhole space.

    ' Gets the CREST file from CCP for current Industry Teams and updates the EVEIPH DB with the values
    Public Function UpdateIndustryTeams(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim IndustryTeamsOutput As IndustryTeams
        Dim SQL As String

        ' Data Variables
        Dim CacheDate As Date
        ' Industry Teams
        Dim TeamID As String
        Dim TeamName As String
        Dim ActivityID As String
        Dim CreationTime As DateTime
        Dim ExpiryTime As DateTime
        Dim CostModifier As Double
        Dim SolarSystemID As Long
        Dim SolarSystemName As String
        Dim CategorySpecializationID As Integer

        ' Industry Teams Bonuses
        Dim BonusID As Integer
        Dim BonusType As String
        Dim BonusValue As Double
        Dim GroupSpecializationID As Integer

        Dim rsCheck As SQLiteDataReader
        Dim rsCheck2 As SQLiteDataReader
        Dim Sequence As Integer = 0

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' First look up the cache date to see if it's time to run the update
        CacheDate = GetCRESTCacheDate(IndustryTeamsCacheDateField)

        ' If it's later than now, update
        If CacheDate <= Now Then

            TempLabel.Text = "Downloading Team Data..."
            Application.DoEvents()

            ' Dump the file into the Specializations object
            IndustryTeamsOutput = JsonConvert.DeserializeObject(Of IndustryTeams)(GetJSONFile(CRESTRootServerURL & CRESTIndustryTeams, CacheDate, "Industry Teams"))

            ' Read in the data
            If Not IsNothing(IndustryTeamsOutput) Then
                If IndustryTeamsOutput.items.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Delete the old records first
                    Call evedb.ExecuteNonQuerySQL("DELETE FROM INDUSTRY_TEAMS")
                    Call evedb.ExecuteNonQuerySQL("DELETE FROM INDUSTRY_TEAMS_BONUSES")

                    ' Now read through all the output items and input them into the DB
                    For i = 0 To IndustryTeamsOutput.totalCount - 1

                        TempLabel.Text = "Saving Team Data..."
                        TempPB.Minimum = 0
                        TempPB.Value = 0
                        TempPB.Maximum = IndustryTeamsOutput.totalCount - 1
                        TempPB.Visible = True
                        Application.DoEvents()

                        With IndustryTeamsOutput.items(i)
                            TeamID = .id
                            TeamName = FormatDBString(Replace(.name, "<br>", " ")) ' Replace the break with a space
                            ActivityID = .activity
                            CreationTime = CDate(Replace(.creationTime, "T", " "))
                            ExpiryTime = CDate(Replace(.expiryTime, "T", " "))
                            CostModifier = .costModifier
                            SolarSystemID = .solarSystem.id
                            SolarSystemName = .solarSystem.name
                            CategorySpecializationID = .specialization.id
                        End With

                        ' Insert this team record
                        SQL = "INSERT INTO INDUSTRY_TEAMS VALUES(" & TeamID & ",'" & TeamName & "'," & ActivityID & ","
                        SQL = SQL & CStr(SolarSystemID) & ",'" & SolarSystemName & "',"
                        SQL = SQL & CStr(CostModifier) & ",'"
                        SQL = SQL & Format(CreationTime, SQLiteDateFormat) & "','"
                        SQL = SQL & Format(ExpiryTime, SQLiteDateFormat) & "',"
                        SQL = SQL & CStr(CategorySpecializationID) & ")"

                        Call evedb.ExecuteNonQuerySQL(SQL)

                        ' Now loop through all the workers and input the bonus data
                        For j = 0 To IndustryTeamsOutput.items(i).workers.Count - 1

                            With IndustryTeamsOutput.items(i).workers(j)
                                BonusID = .bonus.id
                                BonusType = .bonus.bonusType
                                BonusValue = .bonus.value
                                GroupSpecializationID = .specialization.id
                            End With

                            SQL = "INSERT INTO INDUSTRY_TEAMS_BONUSES VALUES (" & TeamID & ",'" & TeamName & "'," & CStr(BonusID) & ",'"
                            SQL = SQL & BonusType & "'," & CStr(-1 * BonusValue) & "," & CStr(GroupSpecializationID) & ")" ' make bonus value positive

                            Call evedb.ExecuteNonQuerySQL(SQL)

                        Next

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()
                    Next

                    ' Set the Cache Date to now plus the length since it's not sent in the file
                    Call SetCRESTCacheDate(IndustryTeamsCacheDateField, CacheDate)

                    TempLabel.Text = "Finalizing Team Data..."
                    TempPB.Visible = False
                    Application.DoEvents()

                    ' Finally, update the team names that are duplicates - short term fix for duplicates?
                    SQL = "SELECT TEAM_NAME, COUNT(*) FROM INDUSTRY_TEAMS GROUP BY TEAM_NAME HAVING COUNT(*) > 1"
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsCheck = DBCommand.ExecuteReader

                    While rsCheck.Read
                        ' Update each team name to make it unique
                        SQL = "SELECT TEAM_NAME, TEAM_ID FROM INDUSTRY_TEAMS WHERE TEAM_NAME = '" & rsCheck.GetString(0) & "' ORDER BY TEAM_ID"
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsCheck2 = DBCommand.ExecuteReader

                        While rsCheck2.Read
                            ' Update Teams first
                            SQL = "UPDATE INDUSTRY_TEAMS SET TEAM_NAME = '" & FormatDBString(rsCheck2.GetString(0) & " " & CStr(Sequence + 1)) & "' "
                            SQL = SQL & "WHERE TEAM_NAME = '" & rsCheck2.GetString(0) & "' AND TEAM_ID = " & rsCheck2.GetInt64(1)
                            Call evedb.ExecuteNonQuerySQL(SQL)

                            ' Update Team Bonuses 
                            SQL = "UPDATE INDUSTRY_TEAMS_BONUSES SET TEAM_NAME = '" & FormatDBString(rsCheck2.GetString(0) & " " & CStr(Sequence + 1)) & "' "
                            SQL = SQL & "WHERE TEAM_NAME = '" & rsCheck2.GetString(0) & "' AND TEAM_ID = " & rsCheck2.GetInt64(1)
                            Call evedb.ExecuteNonQuerySQL(SQL)

                            ' Increment
                            Sequence += 1
                        End While

                        rsCheck2.Close()
                        Sequence = 0 ' Reset for next group

                    End While

                    ' Rebuild indexes on teams and bonuses
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_TEAMS_TEAM_NAME")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_TEAMS_ACTIVITY_ID")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_TEAMS_TEAM_ID")

                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_BONUSES_ID_GID")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_BONUSES_NAME_GID")

                    rsCheck.Close()
                    DBCommand = Nothing
                    rsCheck = Nothing
                    rsCheck2 = Nothing

                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()

                    Return True

                End If
            End If
            ' Json file didn't download
            Return False
        End If

        Return True

    End Function

    ' For Industry Teams
    Private Class IndustryTeams
        '{"totalCount_str": "56", "items": [], "pageCount": 1, "pageCount_str": "1", "totalCount": 56}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As IndustryTeamItems
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Industry Teams
    Private Class IndustryTeamItems
        '{"solarSystem": {}, "name": "Roden Shipyards<br>Team MPV00", "creationTime": "2014-07-11T22:56:52",  "workers": [],
        ' "expiryTime": "2014-08-15T22:56:52", "costModifier": 5.0, "id_str": "22", "activity": 1, "activity_str": "1",
        ' "id": 22},  "specialization": {} "
        <JsonProperty("solarSystem")> Public solarSystem As SolarSystem
        <JsonProperty("name")> Public name As String
        <JsonProperty("creationTime")> Public creationTime As String
        <JsonProperty("workers")> Public workers() As IndustryTeamsWorkers
        <JsonProperty("expiryTime")> Public expiryTime As String
        <JsonProperty("costModifier")> Public costModifier As Double
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("activity")> Public activity As String
        <JsonProperty("activity_str")> Public activity_str As String
        <JsonProperty("id")> Public id As String
        <JsonProperty("specialization")> Public specialization As IndustryTeamsSpecialization
    End Class

    ' For Industry Teams
    Private Class IndustryTeamsWorkers
        '"workers": [{bonus},{specialization}]
        <JsonProperty("bonus")> Public bonus As IndustryTeamsBonus
        <JsonProperty("specialization")> Public specialization As IndustryTeamsSpecialization
    End Class

    ' For Industry Teams
    Private Class IndustryTeamsBonus
        '{"bonus": {"bonusType": "TE", "id": 0, "value": -1.0, "id_str": "0"}, 
        <JsonProperty("bonusType")> Public bonusType As String
        <JsonProperty("id")> Public id As Integer
        <JsonProperty("value")> Public value As Double
        <JsonProperty("id_str")> Public id_str As String
    End Class

    ' For Industry Teams
    Private Class IndustryTeamsSpecialization
        '"specialization": {"href": "https://public-crest-sisi.testeveonline.com/industry/specialities/6/", "id": 6, "id_str": "6"},
        <JsonProperty("href")> Public href As String
        <JsonProperty("id")> Public id As Integer
        <JsonProperty("id_str")> Public id_str As String
    End Class

    ' /industry/teams/auction/ (cache: 1 hour)
    ' vnd.ccp.eve.IndustryTeamCollection-v1
    ' Returns a list of all the teams currently up for auction.

    ' Gets the CREST file from CCP for current Industry Team Auctions and updates the EVEIPH DB with the values
    Public Function UpdateIndustryTeamAuctions(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim IndustryTeamAuctionsOutput As IndustryTeamAuctions
        Dim SQL As String
        Dim CacheDate As Date
        Dim AuctionID As String
        Dim CreationTime As DateTime
        Dim ExpiryTime As DateTime
        Dim TeamName As String
        Dim TeamID As Long

        Dim rsCheck As SQLiteDataReader
        Dim rsCheck2 As SQLiteDataReader
        Dim Sequence As Integer = 0

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' First look up the cache date to see if it's time to run the update
        CacheDate = GetCRESTCacheDate(IndustryTeamAuctionsField)

        ' If it's later than now, update
        If CacheDate <= Now Then

            TempLabel.Text = "Downloading Team Auction Data..."
            Application.DoEvents()

            ' Dump the file into the Specializations object
            IndustryTeamAuctionsOutput = JsonConvert.DeserializeObject(Of IndustryTeamAuctions)(GetJSONFile(CRESTRootServerURL & CRESTIndustryTeamAuctions, CacheDate, "Team Auctions"))

            ' Read in the data
            If Not IsNothing(IndustryTeamAuctionsOutput) Then
                If IndustryTeamAuctionsOutput.items.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    TempLabel.Text = "Saving Team Auction Data..."
                    TempPB.Minimum = 0
                    TempPB.Value = 0
                    TempPB.Maximum = IndustryTeamAuctionsOutput.totalCount - 1
                    TempPB.Visible = True
                    Application.DoEvents()

                    ' Delete the old records first
                    Call evedb.ExecuteNonQuerySQL("DELETE FROM INDUSTRY_TEAMS_AUCTIONS")

                    ' Now read through all the output items and input them into the DB
                    For i = 0 To IndustryTeamAuctionsOutput.totalCount - 1

                        With IndustryTeamAuctionsOutput.items(i)
                            AuctionID = CStr(.id)
                            CreationTime = CDate(Replace(.creationTime, "T", " "))
                            ExpiryTime = CDate(Replace(.expiryTime, "T", " "))
                            TeamName = FormatDBString(Replace(.name, "<br>", " "))
                            TeamID = CLng(.id)

                            ' Insert the base auction data 
                            SQL = "INSERT INTO INDUSTRY_TEAMS_AUCTIONS VALUES(" & TeamID & ",'" & TeamName & "'," & CStr(.activity) & ","
                            SQL = SQL & CStr(.solarSystem.id) & ",'" & .solarSystem.name & "'," & CStr(.costModifier) & ",'"
                            SQL = SQL & Format(CreationTime, SQLiteDateFormat) & "','" & Format(ExpiryTime, SQLiteDateFormat) & "',"
                            SQL = SQL & CStr(.specialization.id) & "," & AuctionID & ")"

                            Call evedb.ExecuteNonQuerySQL(SQL)

                            For j = 0 To .workers.Count - 1
                                ' Insert the workers for this team and link by team name (should be unique) - make bonus value negative
                                SQL = "INSERT INTO INDUSTRY_TEAMS_BONUSES VALUES (" & TeamID & ",'" & TeamName & "'," & CStr(.workers(j).bonus.id) & ",'"
                                SQL = SQL & .workers(j).bonus.bonusType & "'," & CStr(-1 * .workers(j).bonus.value) & "," & CStr(.workers(j).specialization.id) & ")"

                                Call evedb.ExecuteNonQuerySQL(SQL)
                            Next

                            ' Insert the Bid data
                            For j = 0 To .solarSystemBids.Count - 1
                                ' For each bid in the array of solar systems, you can have multiple bids, and each bid can have multiple characters
                                For k = 0 To .solarSystemBids(j).characterBids.Count - 1

                                    SQL = "INSERT INTO INDUSTRY_TEAMS_AUCTIONS_BIDS VALUES(" & AuctionID & ","
                                    SQL = SQL & CStr(.solarSystemBids(j).solarSystem.id) & ",'"
                                    SQL = SQL & .solarSystemBids(j).solarSystem.name & "',"
                                    SQL = SQL & CStr(.solarSystemBids(j).bidAmount) & ","
                                    SQL = SQL & CStr(.solarSystemBids(j).characterBids(k).character.CharacterID) & ",'"
                                    SQL = SQL & FormatDBString(.solarSystemBids(j).characterBids(k).character.name) & "',"
                                    SQL = SQL & CStr(CInt(.solarSystemBids(j).characterBids(k).character.isNPC)) & ","
                                    SQL = SQL & CStr(.solarSystemBids(j).characterBids(k).bidAmount) & ")"

                                    Call evedb.ExecuteNonQuerySQL(SQL)

                                Next
                            Next
                        End With

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()
                    Next

                    ' Set the Cache Date to now plus the length since it's not sent in the file
                    Call SetCRESTCacheDate(IndustryTeamAuctionsField, CacheDate)

                    TempLabel.Text = "Finalizing Team Auction Data..."
                    TempPB.Visible = False
                    Application.DoEvents()

                    ' Finally, update the team names that are duplicates - short term fix for duplicates?
                    SQL = "SELECT TEAM_NAME, COUNT(*) FROM INDUSTRY_TEAMS_AUCTIONS GROUP BY TEAM_NAME HAVING COUNT(*) >1"
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsCheck = DBCommand.ExecuteReader

                    While rsCheck.Read
                        ' Update each team name to make it unique
                        SQL = "SELECT TEAM_NAME, TEAM_ID FROM INDUSTRY_TEAMS_AUCTIONS WHERE TEAM_NAME = '" & rsCheck.GetString(0) & "' ORDER BY TEAM_ID"
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsCheck2 = DBCommand.ExecuteReader

                        While rsCheck2.Read
                            ' Update Teams first
                            SQL = "UPDATE INDUSTRY_TEAMS_AUCTIONS SET TEAM_NAME = '" & FormatDBString(rsCheck2.GetString(0) & " " & CStr(Sequence + 1)) & "' "
                            SQL = SQL & "WHERE TEAM_NAME = '" & rsCheck2.GetString(0) & "' AND TEAM_ID = " & rsCheck2.GetInt64(1)
                            Call evedb.ExecuteNonQuerySQL(SQL)

                            ' Update Team Bonuses 
                            SQL = "UPDATE INDUSTRY_TEAMS_BONUSES SET TEAM_NAME = '" & FormatDBString(rsCheck2.GetString(0) & " " & CStr(Sequence + 1)) & "' "
                            SQL = SQL & "WHERE TEAM_NAME = '" & rsCheck2.GetString(0) & "' AND TEAM_ID = " & rsCheck2.GetInt64(1)
                            Call evedb.ExecuteNonQuerySQL(SQL)
                            ' Increment
                            Sequence += 1
                        End While

                        rsCheck2.Close()
                        Sequence = 0 ' Reset for next group

                    End While

                    ' Rebuild indexes on auctions and bonuses
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_AUCTIONS_TEAM_ID")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_AUCTIONS_CTIVITY_ID")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_AUCTIONS_TEAM_NAME")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_AUCTIONS_AUCTION_ID")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_BIDS_AUCTION_ID")

                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_BONUSES_ID_GID")
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_BONUSES_NAME_GID")

                    rsCheck.Close()
                    DBCommand = Nothing
                    rsCheck = Nothing
                    rsCheck2 = Nothing

                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()

                    Return True

                End If
            End If
            ' Json file didn't download
            Return False
        End If

        Return True

    End Function

    ' For Industry Team Auctions
    Private Class IndustryTeamAuctions
        '{"totalCount_str": "1005", "items":[], "pageCount": 1, "pageCount_str": "1", "totalCount": 1005}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As IndustryTeamAuction
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Industry Team Auctions
    Private Class IndustryTeamAuction
        '{"solarSystem": {}, "specialization": {}, "creationTime": "2014-07-13T14:23:38", "workers": [], 
        '"expiryTime": "2014-08-17T14:23:38", "solarSystemBids": [], "auctionExpiryTime": "2014-07-20T14:23:38", 
        '"id_str": "258", "activity": 1, "costModifier": 4.0, "id": 1281, "name": "Guristas Production<br>Team MPV00"},
        <JsonProperty("solarSystem")> Public solarSystem As SolarSystem
        <JsonProperty("specialization")> Public specialization As IndustryTeamsSpecialization
        <JsonProperty("creationTime")> Public creationTime As String
        <JsonProperty("workers")> Public workers() As IndustryTeamsWorkers
        <JsonProperty("expiryTime")> Public expiryTime As String
        <JsonProperty("solarSystemBids")> Public solarSystemBids() As IndustryTeamAuctionSolarSystemBids
        <JsonProperty("auctionExpiryTime")> Public auctionExpiryTime As String
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("activity")> Public activity As String
        <JsonProperty("costModifier")> Public costModifier As Double
        <JsonProperty("id")> Public id As Integer
        <JsonProperty("name")> Public name As String

    End Class

    ' For Industry Team Auctions
    Private Class IndustryTeamAuctionSolarSystemBids
        '"solarSystemBids": [{"characterBids": [], "solarSystem": {}, "bidAmount": 400000.0}], 
        <JsonProperty("characterBids")> Public characterBids() As IndustryTeamAuctionCharacterBid
        <JsonProperty("solarSystem")> Public solarSystem As SolarSystem
        <JsonProperty("bidAmount")> Public bidAmount As Double
    End Class

    ' For Industry Team Auctions
    Private Class IndustryTeamAuctionCharacterBid
        '"characterBids": [{"character": {}, "bidAmount": 400000.0}], 
        <JsonProperty("character")> Public character As IndustryTeamAuctionCharacter
        <JsonProperty("bidAmount")> Public bidAmount As Double
    End Class

    ' For Industry Team Auctions
    Private Class IndustryTeamAuctionCharacter
        ' "character": {"isNPC": false, "id": 94733327, "href": "https://public-crest-sisi.testeveonline.com/characters/94733327/", "name": "Aniv Omegadeleiv", "capsuleer": {"href": ""}
        <JsonProperty("isNPC")> Public isNPC As Boolean
        <JsonProperty("id")> Public CharacterID As Long
        <JsonProperty("href")> Public href As String
        <JsonProperty("name")> Public name As String
        <JsonProperty("capsuleer")> Public capsuleer As hrefKey
    End Class

    ' /industry/facilities/ (cache: 1 hour)
    ' vnd.ccp.eve.IndustryFacilityCollection-v1
    ' This returns a list of all publicly accessible facilities, including player built outposts in nullsec.

    ' Gets the CREST file from CCP for current Industry Facilities and updates the EVEIPH DB with the values
    Public Function UpdateIndustryFacilties(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing, _
                                            Optional SplashVisible As Boolean = False) As Boolean
        Dim IndustryFacilitiesOutput As IndustryFacilities
        Dim SQL As String
        Dim CacheDate As Date
        Dim rsLookup As SQLiteDataReader

        Dim StartTime As DateTime
        Dim TimeCounter As Integer
        Dim StatusText As String = ""

        Dim SystemIndiciesUpdated As Boolean

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        Dim FacilityName As String

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' Before doing anything, update the system indicies
        Call UpdateIndustrySystemsCostIndex(SystemIndiciesUpdated, UpdateLabel, PB)

        ' Now look up the cache date to see if it's time to run the update
        CacheDate = GetCRESTCacheDate(IndustryFacilitiesField)

        ' If it's later than now, update
        If CacheDate <= Now Then

            StatusText = "Updating Facility Data..."
            If SplashVisible Then
                Call SetProgress(StatusText)
            Else
                TempLabel.Text = StatusText
            End If

            Application.DoEvents()

            ' Dump the file into the Specializations object
            IndustryFacilitiesOutput = JsonConvert.DeserializeObject(Of IndustryFacilities) _
                (GetJSONFile(CRESTRootServerURL & CRESTIndustryFacilities, CacheDate, "Industry Facilities"))

            ' Read in the data
            If Not IsNothing(IndustryFacilitiesOutput) Then
                If IndustryFacilitiesOutput.items.Count > 0 Then

                    Call EVEDB.BeginSQLiteTransaction()

                    StatusText = "Saving Industry Facilities Data..."
                    If SplashVisible Then
                        Call SetProgress(StatusText)
                    Else
                        TempLabel.Text = StatusText
                    End If
                    TempPB.Minimum = 0
                    TempPB.Value = 0
                    TempPB.Maximum = IndustryFacilitiesOutput.totalCount - 1
                    TempPB.Visible = True
                    Application.DoEvents()

                    ' Now read through all the output items and input them into the DB
                    For i = 0 To IndustryFacilitiesOutput.totalCount - 1
                        With IndustryFacilitiesOutput.items(i)
                            ' See if this is an outpost or not and add the tag for type to the name
                            Select Case .type.id
                                ' FACILITY_TYPE_ID	FACILITY_TYPE
                                ' 21644	Amarr Factory Outpost
                                ' 21645	Gallente Administrative Outpost
                                ' 21646	Minmatar Service Outpost
                                ' 21642	Caldari Research Outpost
                                Case 21644
                                    FacilityName = Format(.name) & " (A)"
                                Case 21645
                                    FacilityName = Format(.name) & " (G)"
                                Case 21646
                                    FacilityName = Format(.name) & " (M)"
                                Case 21642
                                    FacilityName = Format(.name) & " (C)"
                                Case Else
                                    FacilityName = Format(.name)
                            End Select

                            ' Look up each facility and if found, update it. If not, insert - this way if the CREST is having issues, we won't delete all the station data (which doesn't change much)
                            SQL = "SELECT 'X' FROM INDUSTRY_FACILITIES WHERE FACILITY_ID = " & CStr(.facilityID)

                            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                            rsLookup = DBCommand.ExecuteReader

                            If rsLookup.Read() Then
                                SQL = "UPDATE INDUSTRY_FACILITIES "
                                SQL = SQL & "SET FACILITY_NAME = '" & FormatDBString(FacilityName) & "',"
                                SQL = SQL & "FACILITY_TYPE_ID = " & CStr(.type.id) & ","
                                SQL = SQL & "FACILITY_TAX = " & CStr(.tax) & ","
                                SQL = SQL & "SOLAR_SYSTEM_ID = " & CStr(.solarSystem.id) & ","
                                SQL = SQL & "REGION_ID = " & CStr(.region.id) & ","
                                SQL = SQL & "OWNER_ID = " & CStr(.owner.id) & " "
                                SQL = SQL & "WHERE FACILITY_ID = " & CStr(.facilityID)
                                ErrorTracker = SQL
                            Else ' New record, insert
                                SQL = "INSERT INTO INDUSTRY_FACILITIES VALUES ("
                                SQL = SQL & CStr(.facilityID) & ",'"
                                SQL = SQL & FormatDBString(FacilityName) & "',"
                                SQL = SQL & CStr(.type.id) & ","
                                SQL = SQL & CStr(.tax) & ","
                                SQL = SQL & CStr(.solarSystem.id) & ","
                                SQL = SQL & CStr(.region.id) & ","
                                SQL = SQL & CStr(.owner.id) & ")"
                                ErrorTracker = SQL
                            End If

                            Call EVEDB.ExecuteNonQuerySQL(SQL)

                            rsLookup.Close()
                            DBCommand = Nothing

                        End With

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()
                    Next

                    ' Now that everything is inserted update the master station table that we can query for anything
                    StatusText = "Updating Stations Data..."
                    If SplashVisible Then
                        Call SetProgress(StatusText)
                    Else
                        TempLabel.Text = StatusText
                    End If
                    StartTime = Now
                    TimeCounter = 0
                    Application.DoEvents()

                    ' Find all facilities not already in the stations table and loop through to add them
                    SQL = "SELECT DISTINCT FACILITY_ID FROM INDUSTRY_FACILITIES WHERE FACILITY_ID NOT IN (SELECT DISTINCT FACILITY_ID FROM STATION_FACILITIES) "
                    SQL = SQL & "AND (FACILITY_ID IN (SELECT stationID FROM RAM_ASSEMBLY_LINE_STATIONS) " ' Stations with assembly lines
                    SQL = SQL & "OR FACILITY_TYPE_ID IN (21642,21644,21645,21646)) " ' Outpost types

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLookup = DBCommand.ExecuteReader

                    While rsLookup.Read

                        Call SetStationFacilityData(rsLookup.GetInt64(0))

                        ' Add some updates to the splash screen if it takes longer than 30 seconds to update
                        ' After the first time, this all should be relatively fast to update
                        If DateDiff("s", StartTime, Now) >= 30 Then
                            StartTime = Now ' reset the time for another 30 seconds
                            Select Case TimeCounter
                                Case 0 ' 30 seconds
                                    StatusText = "Still Updating Stations Data..."
                                Case 1 ' 60 seconds
                                    StatusText = "Still working..."
                                Case 2 ' 1 min 30 seconds
                                    StatusText = "Don't leave, almost done..."
                                Case 3 ' 2 min 
                                    StatusText = "I promise..."
                                Case 4 ' 2 min 30 seconds
                                    StatusText = "OK, I also hope it finishes soon..."
                                Case 5 ' 3 min
                                    StatusText = "Hearding Llamas?"
                                Case Else
                                    StatusText = "Yeah, this is taking too long - email Zifrian..."
                            End Select
                            ' Update the window
                            If SplashVisible Then
                                Call SetProgress(StatusText)
                            Else
                                TempLabel.Text = StatusText
                            End If
                            TimeCounter += 1 ' increment
                            Application.DoEvents()
                        End If
                    End While

                    rsLookup.Close()
                    DBCommand = Nothing

                    '' Update Tax rates - ignore this until they actually could change, NPC is set by CCP and outposts don't get set through CREST
                    'SQL = "SELECT DISTINCT FACILITY_ID, FACILITY_TAX FROM STATION_FACILITIES WHERE OUTPOST = 0
                    'DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    'rsLookup = DBCommand.ExecuteReader

                    'While rsLookup.Read
                    '    SQL = "UPDATE STATION_FACILITIES SET FACILITY_TAX = " & CStr(rsLookup.GetDouble(1)) & " WHERE FACILITY_ID = " & CStr(rsLookup.GetInt64(0))
                    '    Call evedb.ExecuteNonQuerySQL(SQL)
                    'End While

                    'rsLookup.Close()
                    'DBCommand = Nothing

                    StatusText = "Refreshing Station Data..."
                    If SplashVisible Then
                        Call SetProgress(StatusText)
                    Else
                        TempLabel.Text = StatusText
                    End If

                    ' Update the outposts names, which can change and do
                    SQL = "SELECT DISTINCT FACILITY_NAME, FACILITY_ID FROM INDUSTRY_FACILITIES "
                    SQL = SQL & "WHERE FACILITY_TYPE_ID IN (21642,21644,21645,21646) " ' Outpost types
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLookup = DBCommand.ExecuteReader

                    While rsLookup.Read
                        SQL = "UPDATE STATION_FACILITIES SET FACILITY_NAME = '" & FormatDBString(rsLookup.GetString(0)) & "' WHERE FACILITY_ID = " & CStr(rsLookup.GetInt64(1))
                        Call EVEDB.ExecuteNonQuerySQL(SQL)
                    End While

                    rsLookup.Close()
                    DBCommand = Nothing

                    ' Finally, update the stations table for easy look ups in assets - most should be there from the SDE
                    ' note some stations may not be in the CREST update since those are just industry facilities but contains all outposts, which we want
                    SQL = "SELECT FACILITY_ID, FACILITY_NAME, FACILITY_TYPE_ID, SOLAR_SYSTEM_ID, SOLAR_SYSTEM_SECURITY, REGION_ID "
                    SQL = SQL & "FROM STATION_FACILITIES WHERE FACILITY_ID NOT IN (SELECT STATION_ID AS FACILITY_ID FROM STATIONS) "
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLookup = DBCommand.ExecuteReader

                    ' Insert the new data
                    While rsLookup.Read()
                        SQL = "INSERT INTO STATIONS VALUES (" & CStr(rsLookup.GetInt64(0)) & ","
                        SQL = SQL & "'" & FormatDBString(rsLookup.GetString(1)) & "',"
                        SQL = SQL & CStr(rsLookup.GetInt64(2)) & ","
                        SQL = SQL & CStr(rsLookup.GetInt64(3)) & ","
                        SQL = SQL & CStr(rsLookup.GetFloat(4)) & ","
                        SQL = SQL & CStr(rsLookup.GetInt64(5)) & ",0,0)" ' If we don't know the refinery data then it wasn't in the SDE, so set to zero
                        ErrorTracker = SQL
                        Call EVEDB.ExecuteNonQuerySQL(SQL)
                    End While

                    ' Set the Cache Date to now plus the length since it's not sent in the file 
                    ' for industry facilities though, this only needs to be run once a day - after downtime
                    Call SetCRESTCacheDate(IndustryFacilitiesField, CacheDate)

                    Call EVEDB.CommitSQLiteTransaction()
                    ErrorTracker = ""

                    ' Finally, Update the cost indicies for the solar system of the stations every time we update the system indicies (above)
                    If SystemIndiciesUpdated Then
                        Call EVEDB.BeginSQLiteTransaction()
                        SQL = "SELECT DISTINCT SOLAR_SYSTEM_ID, ACTIVITY_ID, COST_INDEX FROM INDUSTRY_SYSTEMS_COST_INDICIES"
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsLookup = DBCommand.ExecuteReader

                        While rsLookup.Read
                            SQL = "UPDATE STATION_FACILITIES SET COST_INDEX = " & CStr(rsLookup.GetDouble(2)) & " "
                            SQL = SQL & " WHERE SOLAR_SYSTEM_ID = " & CStr(rsLookup.GetInt64(0)) & " AND ACTIVITY_ID = " & CStr(rsLookup.GetInt32(1))
                            Call EVEDB.ExecuteNonQuerySQL(SQL)
                        End While

                        rsLookup.Close()
                        DBCommand = Nothing

                        Call EVEDB.CommitSQLiteTransaction()
                    End If

                    Return True

                End If
            End If
            ' Json file didn't download
            Return False
        End If

        Return True

    End Function

    Private Sub SetStationFacilityData(ByVal FacilityID As Long)
        Dim SQL As String
        Dim rsFacility As SQLiteDataReader

        ' Set the query first
        SQL = "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL = SQL & "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL = SQL & "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL = SQL & "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.timeMultiplier AS TIME_MULTIPLIER, "
        SQL = SQL & "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.costMultiplier AS COST_MULTIPLIER, "
        SQL = SQL & "INVENTORY_GROUPS.groupID AS GROUP_ID, 0 AS CATEGORY_ID, INDUSTRY_SYSTEMS_COST_INDICIES.COST_INDEX, 0 AS OUTPOST "
        SQL = SQL & "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_ASSEMBLY_LINE_STATIONS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL = SQL & "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP, INVENTORY_GROUPS "
        SQL = SQL & "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.assemblyLineTypeID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.groupID = INVENTORY_GROUPS.groupID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_ID = RAM_ASSEMBLY_LINE_STATIONS.stationID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_STATIONS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "
        SQL = SQL & "UNION "
        SQL = SQL & "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL = SQL & "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL = SQL & "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL = SQL & "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.timeMultiplier AS TIME_MULTIPLIER, "
        SQL = SQL & "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.costMultiplier AS COST_MULTIPLIER, "
        SQL = SQL & "0 AS GROUP_ID, INVENTORY_CATEGORIES.categoryID AS CATEGORY_ID, COST_INDEX, 0 AS OUTPOST "
        SQL = SQL & "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_ASSEMBLY_LINE_STATIONS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL = SQL & "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY, INVENTORY_CATEGORIES "
        SQL = SQL & "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.assemblyLineTypeID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.categoryID = INVENTORY_CATEGORIES.categoryID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_ID = RAM_ASSEMBLY_LINE_STATIONS.stationID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_STATIONS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "
        SQL = SQL & "UNION "
        SQL = SQL & "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL = SQL & "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL = SQL & "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL = SQL & "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.timeMultiplier AS TIME_MULTIPLIER, "
        SQL = SQL & "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.costMultiplier AS COST_MULTIPLIER, "
        SQL = SQL & "INVENTORY_GROUPS.groupID AS GROUP_ID, 0 AS CATEGORY_ID, COST_INDEX, 1 AS OUTPOST "
        SQL = SQL & "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_INSTALLATION_TYPE_CONTENTS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL = SQL & "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP, INVENTORY_GROUPS "
        SQL = SQL & "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL = SQL & "AND FACILITY_TYPE_ID IN (21642,21644,21645,21646) "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = RAM_INSTALLATION_TYPE_CONTENTS.installationTypeID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.assemblyLineTypeID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.groupID = INVENTORY_GROUPS.groupID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL = SQL & "AND RAM_INSTALLATION_TYPE_CONTENTS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "
        SQL = SQL & "UNION "
        SQL = SQL & "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL = SQL & "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL = SQL & "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL = SQL & "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL = SQL & "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.timeMultiplier AS TIME_MULTIPLIER, "
        SQL = SQL & "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.costMultiplier AS COST_MULTIPLIER, "
        SQL = SQL & "0 AS GROUP_ID, INVENTORY_CATEGORIES.categoryID AS CATEGORY_ID, COST_INDEX, 1 AS OUTPOST "
        SQL = SQL & "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_INSTALLATION_TYPE_CONTENTS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL = SQL & "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY, INVENTORY_CATEGORIES "
        SQL = SQL & "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL = SQL & "AND FACILITY_TYPE_ID IN (21642,21644,21645,21646) "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = RAM_INSTALLATION_TYPE_CONTENTS.installationTypeID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL = SQL & "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.assemblyLineTypeID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.categoryID = INVENTORY_CATEGORIES.categoryID "
        SQL = SQL & "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL = SQL & "AND RAM_INSTALLATION_TYPE_CONTENTS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsFacility = DBCommand.ExecuteReader

        While rsFacility.Read
            SQL = "INSERT INTO STATION_FACILITIES VALUES ("
            SQL = SQL & CStr(rsFacility.GetInt64(0)) & ", " ' Facility ID
            SQL = SQL & "'" & FormatDBString(rsFacility.GetString(1)) & "', " ' Facility Name
            SQL = SQL & CStr(rsFacility.GetInt64(2)) & ", " ' Solar System ID
            SQL = SQL & "'" & FormatDBString(rsFacility.GetString(3)) & "', " ' Solar System Name
            SQL = SQL & CStr(rsFacility.GetDouble(4)) & ", " ' Solar System Security
            SQL = SQL & CStr(rsFacility.GetInt64(5)) & ", " ' Region ID
            SQL = SQL & "'" & FormatDBString(rsFacility.GetString(6)) & "', " ' Region Name
            SQL = SQL & CStr(rsFacility.GetInt64(7)) & ", " ' Facility Type ID
            SQL = SQL & "'" & FormatDBString(rsFacility.GetString(8)) & "', " ' Facility Type
            SQL = SQL & CStr(rsFacility.GetInt64(9)) & ", " ' Activity ID
            SQL = SQL & CStr(rsFacility.GetDouble(10)) & ", " ' Facility Tax
            SQL = SQL & CStr(rsFacility.GetDouble(11)) & ", " ' Material Multiplier
            SQL = SQL & CStr(rsFacility.GetDouble(12)) & ", " ' Time Multiplier
            SQL = SQL & CStr(rsFacility.GetDouble(13)) & ", " ' Cost Multiplier
            SQL = SQL & CStr(rsFacility.GetInt64(14)) & ", " ' Group ID
            SQL = SQL & CStr(rsFacility.GetInt64(15)) & ", " ' Category ID
            SQL = SQL & CStr(rsFacility.GetDouble(16)) & ", " ' Cost Index
            SQL = SQL & CStr(rsFacility.GetInt32(17)) & ")" ' Outpost 

            Call evedb.ExecuteNonQuerySQL(SQL)
            Application.DoEvents()
        End While

    End Sub

    ' For Industry Facilities
    Private Class IndustryFacilities
        '{"totalCount_str": "1005", "items":[], "pageCount": 1, "pageCount_str": "1", "totalCount": 1005}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As IndustryFacility
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Industry Facilities
    Private Class IndustryFacility
        '{"facilityID": 60012160, "solarSystem": {}, "name": "xxx", "region": {}, "tax": 0.1, "facilityID_str": "60012160", "owner": {}, "type": {} }, 
        <JsonProperty("facilityID")> Public facilityID As Long
        <JsonProperty("solarSystem")> Public solarSystem As IndustryFacilitySolarSystem
        <JsonProperty("name")> Public name As String
        <JsonProperty("region")> Public region As IndustryFacilityRegion
        <JsonProperty("tax")> Public tax As Double
        <JsonProperty("facilityID_str")> Public facilityID_str As String
        <JsonProperty("owner")> Public owner As IndustryFacilitiesOwner
        <JsonProperty("type")> Public type As IndustryActivities
    End Class

    ' For Industry Facilities
    Private Class IndustryFacilitiesOwner
        ' "owner": {"id": 1000123, "id_str": "1000123"},  
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("id")> Public id As Integer
    End Class

    ' For Industry Facilities
    Private Class IndustryActivities
        ' "type": {"id": 2500, "id_str": "2500"}
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("id")> Public id As Integer
    End Class

    ' For Industry Facilities
    Private Class IndustryFacilitySolarSystem
        ' "solarSystem": {"id": 30000049, "id_str": "30000049"}, 
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("id")> Public id As Long
    End Class

    ' For Industry Facilities
    Private Class IndustryFacilityRegion
        ' "region": {"id": 10000001, "id_str": "10000001"}, 
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("id")> Public id As Long
    End Class

    ' /industry/systems/ (cache: 1 hour)
    ' vnd.ccp.eve.IndustrySystemCollection-v1
    ' Lists the cost index for installing industry jobs per type of activity. This does not include wormhole space.

    ' Gets the CREST file from CCP for current Cost Indexes of Industry Systems and updates the EVEIPH DB with the values

    ' Make private so we can only run with the update industry facilities function
    Private Function UpdateIndustrySystemsCostIndex(ByRef IndiciesUpdated As Boolean, Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim IndustrySystemsIndex As IndustrySystemCostIndicies
        Dim SQL As String
        Dim CacheDate As Date
        Dim rsLookup As SQLiteDataReader

        Dim SolarSystemID As Long
        Dim SolarSystemName As String

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' First look up the cache date to see if it's time to run the update
        CacheDate = GetCRESTCacheDate(IndustrySystemsField)

        ' If it's later than now, update
        If CacheDate <= Now Then
            IndiciesUpdated = True
            TempLabel.Text = "Downloading System Index Data..."
            Application.DoEvents()

            ' Dump the file into the Specializations object
            IndustrySystemsIndex = JsonConvert.DeserializeObject(Of IndustrySystemCostIndicies) _
                (GetJSONFile(CRESTRootServerURL & CRESTIndustrySystems, CacheDate, "Industry System Indexes"))

            ' Read in the data
            If Not IsNothing(IndustrySystemsIndex) Then
                If IndustrySystemsIndex.items.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    TempLabel.Text = "Saving System Index Data..."
                    TempPB.Minimum = 0
                    TempPB.Value = 0
                    TempPB.Maximum = IndustrySystemsIndex.totalCount - 1
                    TempPB.Visible = True
                    Application.DoEvents()

                    ' Now read through all the output items and input them into the DB
                    For i = 0 To IndustrySystemsIndex.totalCount - 1

                        SolarSystemID = IndustrySystemsIndex.items(i).solarSystem.id
                        SolarSystemName = IndustrySystemsIndex.items(i).solarSystem.name

                        For j = 0 To IndustrySystemsIndex.items(i).systemCostIndices.Count - 1
                            With IndustrySystemsIndex.items(i).systemCostIndices(j)
                                ' Look up each facility and if found, update it. If not, insert - this way if the CREST is having issues, we won't delete all the station data (which doesn't change much)
                                SQL = "SELECT 'X' FROM INDUSTRY_SYSTEMS_COST_INDICIES WHERE SOLAR_SYSTEM_ID = " & CStr(SolarSystemID) & " AND ACTIVITY_ID = " & CStr(.activityID)

                                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                                rsLookup = DBCommand.ExecuteReader

                                If rsLookup.Read Then
                                    ' Update the old
                                    SQL = "UPDATE INDUSTRY_SYSTEMS_COST_INDICIES "
                                    SQL = SQL & "SET SOLAR_SYSTEM_NAME = '" & FormatDBString(SolarSystemName) & "',"
                                    SQL = SQL & "ACTIVITY_ID = " & CStr(.activityID) & ","
                                    SQL = SQL & "ACTIVITY_NAME = '" & FormatDBString(.activityName) & "',"
                                    SQL = SQL & "COST_INDEX = " & CStr(.costIndex) & " "
                                    SQL = SQL & "WHERE SOLAR_SYSTEM_ID = " & CStr(SolarSystemID) & " AND ACTIVITY_ID = " & CStr(.activityID)
                                Else
                                    ' Insert the new record
                                    SQL = "INSERT INTO INDUSTRY_SYSTEMS_COST_INDICIES VALUES(" & CStr(SolarSystemID) & ",'" & FormatDBString(SolarSystemName) & "',"
                                    SQL = SQL & CStr(.activityID) & ",'" & FormatDBString(.activityName) & "'," & CStr(.costIndex) & ")"
                                End If

                                Call evedb.ExecuteNonQuerySQL(SQL)
                            End With
                        Next

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()
                    Next

                    TempPB.Visible = False

                    ' Rebuild indexes
                    Call evedb.ExecuteNonQuerySQL("REINDEX IDX_ISCI_SSID_AID")

                    ' Set the Cache Date to now plus the length since it's not sent in the file
                    Call SetCRESTCacheDate(IndustrySystemsField, CacheDate)
                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()

                    Return True

                End If
            End If
            ' Json file didn't download
            Return False
        Else
            IndiciesUpdated = False
        End If

        Return True

    End Function

    ' For Industry System Cost Indicies
    Private Class IndustrySystemCostIndicies
        '{"totalCount_str": "1005", "items":[], "pageCount": 1, "pageCount_str": "1", "totalCount": 1005}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As systemCostIndices
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Industry System Cost Indicies
    Private Class systemCostIndices
        '{"systemCostIndices": [], "solarSystem": {}}
        <JsonProperty("systemCostIndices")> Public systemCostIndices() As costIndex
        <JsonProperty("solarSystem")> Public solarSystem As SolarSystem
    End Class

    ' For Industry System Cost Indicies
    Private Class costIndex
        ''{"costIndex": 0.02832645090482675, "activityID": 1, "activityID_str": "1", "activityName": "Manufacturing"}
        <JsonProperty("costIndex")> Public costIndex As Double
        <JsonProperty("activityID")> Public activityID As Integer
        <JsonProperty("activityID_str")> Public activityID_str As String
        <JsonProperty("activityName")> Public activityName As String
    End Class

    ' /market/prices/ (cache: 23 hours)
    ' vnd.ccp.eve.MarketTypePriceCollection-v1
    ' Returns the list of trade-able types and their average market price, as shown in the inventory UI in the EVE client. 
    ' Also includes an adjusted market price which is used in industry calculations.

    ' Gets the CREST file from CCP for current Market Prices and updates the EVEIPH DB with the values
    Public Function UpdateAdjAvgMarketPrices(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim MarketPricesOutput As MarketPrices
        Dim SQL As String
        Dim CacheDate As Date
        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' First look up the cache date to see if it's time to run the update
        CacheDate = GetCRESTCacheDate(MarketPricesField)

        ' If it's later than now, update
        If CacheDate <= Now Then

            TempLabel.Text = "Downloading Adjusted Market Price Data..."
            Application.DoEvents()

            ' Dump the file into the Specializations object
            MarketPricesOutput = JsonConvert.DeserializeObject(Of MarketPrices) _
                (GetJSONFile(CRESTRootServerURL & CRESTMarketPrices, CacheDate, "Market Prices"))

            ' Read in the data
            If Not IsNothing(MarketPricesOutput) Then
                If MarketPricesOutput.items.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Clear the old records first
                    Call evedb.ExecuteNonQuerySQL("UPDATE ITEM_PRICES SET ADJUSTED_PRICE = 0, AVERAGE_PRICE = 0")

                    TempLabel.Text = "Saving Adjusted Market Price Data..."
                    TempPB.Minimum = 0
                    TempPB.Value = 0
                    TempPB.Maximum = MarketPricesOutput.totalCount - 1
                    TempPB.Visible = True
                    Application.DoEvents()

                    ' Now read through all the output items and update them in ITEM_PRICES
                    For i = 0 To MarketPricesOutput.totalCount - 1
                        With MarketPricesOutput.items(i)
                            SQL = "UPDATE ITEM_PRICES SET ADJUSTED_PRICE = " & CStr(.adjustedPrice) & ", AVERAGE_PRICE = " & CStr(.averagePrice)
                            SQL = SQL & " WHERE ITEM_ID = " & CStr(.type.id)
                            Call evedb.ExecuteNonQuerySQL(SQL)
                        End With

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()

                    Next

                    ' Set the Cache Date to now plus the length since it's not sent in the file
                    Call SetCRESTCacheDate(MarketPricesField, CacheDate)
                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()
                    Return True
                End If
            End If
            ' Json file didn't download
            Return False
        End If

        Return True

    End Function

    ' For Market Prices
    Private Class MarketPrices
        '{"totalCount_str": "1005", "items":[], "pageCount": 1, "pageCount_str": "1", "totalCount": 1005}
        <JsonProperty("totalCount_str")> Public totalCount_str As String
        <JsonProperty("items")> Public items() As MarketAdjustedPrice
        <JsonProperty("pageCount")> Public pageCount As Integer
        <JsonProperty("pageCount_str")> Public pageCount_str As String
        <JsonProperty("totalCount")> Public totalCount As Integer
    End Class

    ' For Market Prices
    Private Class MarketAdjustedPrice
        '{"adjustedPrice": 567464.0783, "averagePrice": 565523.7836, "type": {} }
        <JsonProperty("adjustedPrice")> Public adjustedPrice As Double
        <JsonProperty("averagePrice")> Public averagePrice As Double
        <JsonProperty("type")> Public type As MarketPriceType
    End Class

    ' For Market Prices
    Private Class MarketPriceType
        '"type": {"id_str": "32772", "href": "https://public-crest-sisi.testeveonline.com/types/32772/", "id": 32772, "name": "Medium Ancillary Shield Booster"}
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("href")> Public href As String
        <JsonProperty("id")> Public id As Long
        <JsonProperty("name")> Public name As String
    End Class

    ' Stuff for all functions

    ' For CREST Solar Systems
    Private Class SolarSystem
        '{"id_str": "30001743", "href": "https://public-crest-sisi.testeveonline.com/solarsystems/30001743/", "id": 30001743, "name": "JUE-DX"}, 
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("href")> Public href As String
        <JsonProperty("id")> Public id As Long
        <JsonProperty("name")> Public name As String
    End Class

    ' For CREST Regions
    Private Class Region
        '{"id_str": "30001743", "href": "https://public-crest-sisi.testeveonline.com/solarsystems/30001743/", "id": 30001743, "name": "The Forge"}, 
        <JsonProperty("id_str")> Public id_str As String
        <JsonProperty("href")> Public href As String
        <JsonProperty("id")> Public id As Long
        <JsonProperty("name")> Public name As String
    End Class

    ' For all CREST where key is just a web link
    Private Class hrefKey
        '"capsuleer": {"href": "https://public-crest-sisi.testeveonline.com/characters/1047420507/capsuleer/"}
        <JsonProperty("href")> Public href As String
    End Class

    ' Downloads the JSON file sent and saves it to the location, then imports it into a string to return
    ' Note Cache Date is returned in local time, not GMT
    Private Function GetJSONFile(ByVal URL As String, ByRef CacheDate As Date, ByVal UpdateType As String, _
                                 Optional ByVal IgnoreExceptions As Boolean = False, Optional RecursiveCalls As Integer = 0) As String
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim MaxAge As String
        Dim CallStatus As String

        Dim Output As String = ""

        Try

            Dim myUri As New Uri(URL)

            ' Create the web request  
            request = DirectCast(WebRequest.Create(myUri), HttpWebRequest)
            ' Settings for speed
            request.Method = "GET"
            request.Proxy = Nothing
            request.PreAuthenticate = True
            request.UnsafeAuthenticatedConnectionSharing = True

            ' Get response  
            response = DirectCast(request.GetResponse(), HttpWebResponse)

            ' Use later to see what calls hit or fail
            CallStatus = response.Headers.Get("X-Cache-Status")

            ' Get the max-age from cache control
            MaxAge = response.Headers.Get("Cache-Control")
            ' Parse max age for seconds
            Dim Seconds As Integer = CInt(MaxAge.Substring(MaxAge.IndexOf("=") + 1))

            ' Get the response stream into a reader  
            reader = New StreamReader(response.GetResponseStream())

            ' Set the cache date by ref for market history and industry facilities
            ' These only change once per day. So for these cases set the cache date to take into account
            ' the next downtime or midnight. There could be a case where the servers are down for awhile and the CREST server is
            ' not updated, but it's past the cache time - in that case the users can reset the cache
            Dim TempDate As Date = DateValue(response.Headers.Get("Date").Replace("GMT", "")) ' remove GMT or it will automatically switch to local
            If URL.Contains("/history/") Then
                ' Set this to midnight tomorrow GMT when the prices are updated in history - strip the date received from ccp and add midnight gmt, reparse to get local time
                CacheDate = CDate(CStr(DateAdd(DateInterval.Day, 1, DateTime.Parse(CStr(TempDate) & " 00:00:00 GMT"))))
            ElseIf URL.Contains("/market/prices/") Then
                ' The header isn't correct and the cache of these prices is every 23 hours, so just adjust for this (the time sent is the current time)
                CacheDate = DateAdd(DateInterval.Hour, 23, CDate(response.Headers.Get("Date")))
            ElseIf URL.Contains("/industry/facilities/") Then
                ' Industry changes can only occur once per day after downtime (new outposts) so at the morning restart
                CacheDate = CDate(CStr(DateAdd(DateInterval.Day, 1, DateTime.Parse(CStr(TempDate) & " 12:00:00 GMT"))))
            Else
                CacheDate = DateAdd(DateInterval.Second, Seconds, CDate(response.Headers.Get("Date")))
            End If

            ' Read the data
            Output = reader.ReadToEnd

            ' See if it downloaded a full file
            If Output.Substring(Len(Output) - 1, 1) <> "}" Then
                Application.DoEvents()
                ' Re-run this function - limit to 10 calls
                If RecursiveCalls <= 10 Then
                    Dim NumCalls As Integer = RecursiveCalls + 1
                    Output = GetJSONFile(URL, CacheDate, UpdateType, IgnoreExceptions, NumCalls)
                End If
            End If

            reader.Close()
            response.Close()
            request = Nothing

        Catch ex As Exception
            If Not IgnoreExceptions Then
                MsgBox("Unable to download CREST data for " & UpdateType & vbCrLf & "Error: " & ex.Message, vbInformation, Application.ProductName)
                Output = ""
            End If

            If ex.Message.Contains("An established connection was aborted by the software in your host machine") _
                Or ex.Message.Contains("An existing connection was forcibly closed by the remote host.") Then
                ' Re-run this function - limit to 10 calls
                If RecursiveCalls <= 10 Then
                    Dim NumCalls As Integer = RecursiveCalls + 1
                    Output = GetJSONFile(URL, CacheDate, UpdateType, IgnoreExceptions, NumCalls)
                End If
            End If

        Finally
            If Not response Is Nothing Then response.Close()
        End Try

        Return Output

    End Function

    ' Gets the CREST Cache Date for the field name sent in the CREST_CACHE_DATES table
    Private Function GetCRESTCacheDate(UpdateField As String) As Date
        Dim SQL As String
        Dim readerData As SQLiteDataReader
        Dim RefreshDate As Date

        SQL = "SELECT " & UpdateField & " FROM CREST_CACHE_DATES"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerData = DBCommand.ExecuteReader

        If readerData.Read Then
            If Not IsDBNull(readerData.GetValue(0)) Then
                If readerData.GetString(0) = "" Then
                    RefreshDate = NoDate
                Else
                    RefreshDate = CDate(readerData.GetString(0))
                End If
            Else
                RefreshDate = NoDate
            End If
        Else
            RefreshDate = NoDate
        End If

        readerData.Close()
        readerData = Nothing
        DBCommand = Nothing

        Return RefreshDate

    End Function

    ' Sets the CREST Cache Date for the field name sent in the CREST_CACHE_DATES table
    Private Sub SetCRESTCacheDate(UpdateField As String, CacheDate As Date)
        Dim SQL As String
        Dim readerData As SQLiteDataReader

        ' Update the cache for this CREST file
        SQL = "SELECT " & UpdateField & " FROM CREST_CACHE_DATES"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerData = DBCommand.ExecuteReader

        If readerData.Read Then
            SQL = "UPDATE CREST_CACHE_DATES SET " & UpdateField & " = '" & Format(CacheDate, SQLiteDateFormat) & "'"
            evedb.ExecuteNonQuerySQL(SQL)
        Else
            SQL = "INSERT INTO CREST_CACHE_DATES (" & UpdateField & ") VALUES ('" & Format(CacheDate, SQLiteDateFormat) & "')"
            evedb.ExecuteNonQuerySQL(SQL)
        End If
    End Sub

End Class

