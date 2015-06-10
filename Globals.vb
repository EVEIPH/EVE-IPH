
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Net
Imports System.IO
Imports System.Xml

' Place to store all public variables and functions
Public Module Public_Variables
    ' DB name and version
    Public Const DataDumpVersion As String = "Mosaic_1.2_112318"
    Public Const VersionNumber As String = "3.1.*"

    Public TestingVersion As Boolean ' This flag will test the test downloads from the server for an update
    Public Developer As Boolean ' This is if I'm developing something and only want me to see it instead of public release

    Public LocalCulture As CultureInfo

    Public DB As New SQLiteConnection
    Public DBCommand As SQLiteCommand

    Public SelectedCharacter As New Character
    Public SelectedBlueprint As Blueprint

    ' Variable to hold error tracking data when the error is hard to find - used for debugging only but mostly this is set to empty string
    Public ErrorTracker As String

    Public DefaultCharSelected As Boolean
    Public CharactersLoaded As Boolean
    Public FirstLoad As Boolean ' If the program just opened
    Public SkillsUpdated As Boolean ' To track if skills where updated in the skill override screen
    Public ManufacturingTabColumnsChanged As Boolean ' To track if thy changed columns

    ' File Paths
    Public UpdaterFilePath As String = "" ' Where the update files are stored
    Public UserWorkingFolder As String = "" ' Where the DB and updater and anything that changes files will be
    Public UserImagePath As String = "" ' Where the images are kept

    Public Const XMLUpdateServerURL = "http://sourceforge.net/projects/eveiph/files/Latest%20Files/LatestVersionIPH.xml/download"
    Public Const XMLUpdateTestServerURL = "http://sourceforge.net/projects/eveiph/files/Testing/LatestVersionIPH%20Test.xml/download"
    Public Const PatchNotesURL = "http://sourceforge.net/projects/eveiph/files/README.txt/download"

    Public Const AppDataPath As String = "EVEIPH\"
    Public Const BPImageFilePath As String = "EVEIPH Images\"
    Public Const UpdatePath As String = "Updates\"

    Public Const SQLiteDBFileName As String = "EVEIPH DB.s3db"
    Public Const UpdaterFileName As String = "EVEIPH Updater.exe"
    Public Const XMLLatestVersionFileName As String = "LatestVersionIPH.xml"
    Public Const XMLLatestVersionTest As String = "LatestVersionIPH Test.xml"

    ' For API loading
    Public Const NoSkillsLoaded As String = "No Skills Loaded"
    Public Const NoStandingsLoaded As String = "No Standings Loaded"
    Public Const NoSkillsStandingsLoaded As String = "No Skills or Standings Loaded"
    Public Const NoIndyJobsLoaded As String = "No Industry Jobs Loaded"
    Public Const NoResearchLoaded As String = "No Research Data Loaded"

    ' Just because
    Public Const TheForgeTypeID As Long = 10000002

    Public Const USER_BLUEPRINTS As String = "(SELECT ALL_BLUEPRINTS.BLUEPRINT_ID AS BP_ID, ALL_BLUEPRINTS.BLUEPRINT_GROUP, ALL_BLUEPRINTS.BLUEPRINT_NAME, " _
                                            & "ITEM_GROUP_ID, ITEM_GROUP, ITEM_CATEGORY_ID, ITEM_CATEGORY, ALL_BLUEPRINTS.ITEM_ID, ITEM_NAME," _
                                            & "CASE WHEN OBP.ME IS NOT NULL THEN OBP.ME ELSE 0 END AS ME," _
                                            & "CASE WHEN OBP.TE IS NOT NULL THEN OBP.TE ELSE 0 END AS TE," _
                                            & "CASE WHEN USER_ID IS NOT NULL THEN USER_ID ELSE 0 END AS USER_ID, ITEM_TYPE," _
                                            & "CASE WHEN ALL_BLUEPRINTS.RACE_ID IS NOT NULL THEN ALL_BLUEPRINTS.RACE_ID ELSE 0 END AS RACE_ID," _
                                            & "CASE WHEN OBP.OWNED IS NOT NULL THEN OBP.OWNED ELSE 0 END AS OWNED," _
                                            & "CASE WHEN OBP.SCANNED IS NOT NULL THEN OBP.SCANNED ELSE 0 END AS SCANNED," _
                                            & "CASE WHEN OBP.BP_TYPE IS NOT NULL THEN OBP.BP_TYPE ELSE 0 END AS BP_TYPE," _
                                            & "CASE WHEN OBP.ITEM_ID IS NOT NULL THEN OBP.ITEM_ID ELSE 0 END AS UNIQUE_BP_ITEM_ID, " _
                                            & "CASE WHEN OBP.FAVORITE IS NOT NULL THEN OBP.FAVORITE ELSE 0 END AS FAVORITE, INVENTORY_TYPES.volume, INVENTORY_TYPES.marketGroupID, " _
                                            & "CASE WHEN OBP.ADDITIONAL_COSTS IS NOT NULL THEN OBP.ADDITIONAL_COSTS ELSE 0 END AS ADDITIONAL_COSTS, " _
                                            & "CASE WHEN OBP.LOCATION_ID IS NOT NULL THEN OBP.LOCATION_ID ELSE 0 END AS LOCATION_ID, " _
                                            & "CASE WHEN OBP.QUANTITY IS NOT NULL THEN OBP.QUANTITY ELSE 0 END AS QUANTITY, " _
                                            & "CASE WHEN OBP.FLAG_ID IS NOT NULL THEN OBP.FLAG_ID ELSE 0 END AS FLAG_ID, " _
                                            & "CASE WHEN OBP.RUNS IS NOT NULL THEN OBP.RUNS ELSE 0 END AS RUNS, " _
                                            & "IGNORE, ALL_BLUEPRINTS.TECH_LEVEL, SIZE_GROUP " _
                                            & "FROM ALL_BLUEPRINTS LEFT OUTER JOIN " _
                                            & "(SELECT * FROM OWNED_BLUEPRINTS WHERE OWNED = 1 OR (OWNED = 0 AND BP_TYPE <> -2)) AS OBP " _
                                            & "ON ALL_BLUEPRINTS.BLUEPRINT_ID=OBP.BLUEPRINT_ID AND OBP.USER_ID =  @USERBP_USERID, " _
                                            & "INVENTORY_TYPES WHERE ALL_BLUEPRINTS.ITEM_ID = INVENTORY_TYPES.typeID) AS X "

    ' This will include copies too
    Public Const ALL_USER_BLUEPRINTS As String = "(SELECT ALL_BLUEPRINTS.BLUEPRINT_ID AS BP_ID, ALL_BLUEPRINTS.BLUEPRINT_GROUP AS BLUEPRINT_GROUP, ALL_BLUEPRINTS.BLUEPRINT_NAME AS BLUEPRINT_NAME, " _
                                            & "ITEM_GROUP_ID, ITEM_GROUP, ITEM_CATEGORY_ID, ITEM_CATEGORY, ALL_BLUEPRINTS.ITEM_ID, ITEM_NAME," _
                                            & "CASE WHEN OBP.ME IS NOT NULL THEN OBP.ME ELSE 0 END AS ME," _
                                            & "CASE WHEN OBP.TE IS NOT NULL THEN OBP.TE ELSE 0 END AS TE," _
                                            & "CASE WHEN USER_ID IS NOT NULL THEN USER_ID ELSE 0 END AS USER_ID, ITEM_TYPE," _
                                            & "CASE WHEN ALL_BLUEPRINTS.RACE_ID IS NOT NULL THEN ALL_BLUEPRINTS.RACE_ID ELSE 0 END AS RACE_ID," _
                                            & "CASE WHEN OBP.OWNED IS NOT NULL THEN OBP.OWNED ELSE 0 END AS OWNED," _
                                            & "CASE WHEN OBP.SCANNED IS NOT NULL THEN OBP.SCANNED ELSE 0 END AS SCANNED," _
                                            & "CASE WHEN OBP.BP_TYPE IS NOT NULL THEN OBP.BP_TYPE ELSE 0 END AS BP_TYPE," _
                                            & "CASE WHEN OBP.ITEM_ID IS NOT NULL THEN OBP.ITEM_ID ELSE 0 END AS UNIQUE_BP_ITEM_ID, " _
                                            & "CASE WHEN OBP.FAVORITE IS NOT NULL THEN OBP.FAVORITE ELSE 0 END AS FAVORITE, INVENTORY_TYPES.volume, INVENTORY_TYPES.marketGroupID, " _
                                            & "CASE WHEN OBP.ADDITIONAL_COSTS IS NOT NULL THEN OBP.ADDITIONAL_COSTS ELSE 0 END AS ADDITIONAL_COSTS, " _
                                            & "CASE WHEN OBP.LOCATION_ID IS NOT NULL THEN OBP.LOCATION_ID ELSE 0 END AS LOCATION_ID, " _
                                            & "CASE WHEN OBP.QUANTITY IS NOT NULL THEN OBP.QUANTITY ELSE 0 END AS QUANTITY, " _
                                            & "CASE WHEN OBP.FLAG_ID IS NOT NULL THEN OBP.FLAG_ID ELSE 0 END AS FLAG_ID, " _
                                            & "CASE WHEN OBP.RUNS IS NOT NULL THEN OBP.RUNS ELSE 0 END AS RUNS, " _
                                            & "IGNORE, ALL_BLUEPRINTS.TECH_LEVEL, SIZE_GROUP " _
                                            & "FROM ALL_BLUEPRINTS LEFT OUTER JOIN " _
                                            & "(SELECT * FROM OWNED_BLUEPRINTS) AS OBP ON ALL_BLUEPRINTS.BLUEPRINT_ID=OBP.BLUEPRINT_ID " _
                                            & "AND OBP.USER_ID = @ALLUSERBP_USERID, INVENTORY_TYPES WHERE ALL_BLUEPRINTS.ITEM_ID = INVENTORY_TYPES.typeID ) AS X "

    ' Shopping List
    Public TotalShoppingList As New ShoppingList

    ' For a new shopping list, so we can upate it when it's open
    Public frmShop As frmShoppingList = New frmShoppingList
    ' Same with assets
    Public frmDefaultAssets As frmAssetsViewer
    Public frmShoppingAssets As frmAssetsViewer

    ' The only allowed characters for text entry
    Public Const allowedPriceChars As String = "0123456789.,"
    Public Const allowedMETEChars As String = "0123456789."
    Public Const allowedRunschars As String = "0123456789"
    Public Const allowedDecimalChars As String = "0123456789.-"
    Public Const allowedPercentChars As String = "0123456789.%"

    Public Const SQLiteDateFormat As String = "yyyy-MM-dd HH:mm:ss"

    Public Const IndustryNoCompletedDate As DateTime = #1/1/2001#

    Public Const DataCoreRedeemCost As Double = 10000.0

    ' Column processing
    Public Const NumManufacturingTabColumns As Integer = 68
    Public Const NumIndustryJobColumns As Integer = 20

    Public Const NoDate As Date = #1/1/1900#
    Public Const NoExpiry As Date = #1/1/2200#

    ' For T3 Relics
    Public Const IntactRelic As String = "Intact"
    Public Const MalfunctioningRelic As String = "Malfunctioning"
    Public Const WreckedRelic As String = "Wrecked"

    Public Const MineralGroupID As Integer = 18

    ' T2 BPC base ME/TE
    Public Const BaseT2T3ME As Integer = 2
    Public Const BaseT2T3TE As Integer = 4

    ' For team and industry tab loading
    Public Const BPTab As String = "BP"
    Public Const CalcTab As String = "Calc"

    ' For update prices
    Public Const DefaultSystemPriceCombo As String = "Select System"

    ' For getting mining ammount attribute
    Public Const MiningAmountBonus As String = "miningAmountBonus"

    ' For unhandled exceptions
    Public frmErrorText As String = ""
    Public ErrorTest As String = ""

    Public PriceQueryCount As Integer ' This will track the number of times the user queries EVE Central - used to warn them for over pinging

    Public ShownPriceUpdateError As Boolean ' Only want to show them the error once

    Public Const ActivityManufacturing As String = "Manufacturing"
    Public Const ActivityComponentManufacturing As String = "Component Manufacturing"
    Public Const ActivityCapComponentManufacturing As String = "Cap Component Manufacturing"
    Public Const ActivityCopying As String = "Copying"
    Public Const ActivityInvention As String = "Invention"

    ' Use these to determine the facility types for these cases
    ' These are all the capital ships that use capital parts
    Public Const CapitalIndustrialShipGroupID As Integer = 883
    Public Const CarrierGroupID As Integer = 547
    Public Const DreadnoughtGroupID As Integer = 485
    Public Const FreighterGroupID As Integer = 513
    Public Const IndustrialCommandShipGroupID As Integer = 941
    Public Const JumpFreighterGroupID As Integer = 902
    Public Const SupercarrierGroupID As Integer = 659
    Public Const TitanGroupID As Integer = 30
    Public Const BoosterGroupID As Integer = 303

    Public Const StrategicCruiserGroupID As Integer = 963
    Public Const TacticalDestroyerGroupID As Integer = 1305
    Public Const SubsystemCategoryID As Integer = 32

    Public Const ShipCategoryID As Integer = 6 ' for loading invention and copying 

    ' T3 Bps for facility updates
    Public Const StrategicCruiserBPGroupID As Integer = 996
    Public Const TacticalDestroyerBPGroupID As Integer = 1309
    Public Const SubsystemBPGroupID As Integer = 973

    Public Const ConstructionComponentsGroupID As Integer = 334 ' Use this for all non-capital components
    Public Const ComponentCategoryID As Integer = 17
    Public Const CapitalComponentGroupID As Integer = 873
    Public Const AdvCapitalComponentGroupID As Integer = 913

    ' Only one Decryptor Group with Pheobe
    Public Const DecryptorGroup As Long = 1304

    ' Categories (has multiple groups)
    Public Const StationEggGroupID As Integer = 307 ' This is for loading No POS build items
    Public Const SovStructureCategoryID As Integer = 40
    Public Const StationPartsGroupID As Integer = 536

    Public Const POSFacility As String = "POS"
    Public Const StationFacility As String = "Station"
    Public Const OutpostFacility As String = "Outpost"

    Public Const BPO As String = "BPO"
    Public Const BPC As String = "BPC"
    Public Const InventedBPC As String = "Invented BPC"
    Public Const UnownedBP As String = "Unowned"

    Public Const Yes As String = "Yes"
    Public Const No As String = "No"

    Public NoFacility As New IndustryFacility

    Public Const None As String = "None" ' For decryptors, facilities and teams

    Public APIAdded As Boolean ' To flag if a new api was added, then we can use to reload apis if needed in other areas (eg industry jobs)

    Public MiningUpgradesCollection As New List(Of String)

    Public NoPOSCategoryIDs As List(Of Long) ' For facilities

    Public Enum StationType
        Station = 0
        Outpost = 1
    End Enum

    Public Const POSTaxRate = 0.0 ' was 10% tax on pos usage now it's 0

    ' Mining Ship Name constants
    Public Const Procurer As String = "Procurer"
    Public Const Retriever As String = "Retriever"
    Public Const Covetor As String = "Covetor"
    Public Const Skiff As String = "Skiff"
    Public Const Mackinaw As String = "Mackinaw"
    Public Const Hulk As String = "Hulk"
    Public Const Venture As String = "Venture"
    Public Const Prospect As String = "Prospect"
    Public Const Rorqual As String = "Rorqual"
    Public Const Orca As String = "Orca"
    Public Const Drake As String = "Drake"
    Public Const Rokh As String = "Rokh"

    Public Const CorporationAPITypeName = "Corporation"

    Public Const ExpiredKey = "EXPIRED!"

    ' For exporting Data
    Public Const DefaultTextDataExport As String = "Default"
    Public Const CSVDataExport As String = "CSV"
    Public Const SSVDataExport As String = "SSV"

    ' For scanning assets
    Public Enum ScanType
        Personal = 0
        Corporation = 1
    End Enum

    ' BP Types: -1 is original, -2 is copy from API, others are built for IPH
    Public Enum BPType
        NotOwned = 0
        ' These are assumed owned, since they are marked or loaded from API
        Original = -1
        Copy = -2
        InventedBPC = -3
    End Enum

    ' Types of Asset windows
    Public Enum AssetWindow
        ProgramDefault = 0
        ShoppingList = 1
        RefiningOre = 2
        RefiningItems = 3
    End Enum

    ' For scanning assets
    Public Enum SkillType
        BPReqSkills = 1
        BPComponentSkills = 2
        REReqSkills = 3
        InventionReqSkills = 4
    End Enum

    Public Enum MaterialType
        BaseMats = 0
        ExtraMats = 1
    End Enum

    Public Enum BlueprintTechLevel
        T1 = 1
        T2 = 2
        T3 = 3
    End Enum

    Public Enum BeltType
        Small = 1
        Moderate = 2
        Large = 3
        ExtraLarge = 4
        Giant = 5
    End Enum

    Public Enum ItemSize
        Small = 1
        Medium = 2
        Large = 3
        ExtraLarge = 4
    End Enum

    Public Enum TeamBonusType
        TEBonus = 0
        MEBonus = 1
    End Enum

    Public Enum CopyPasteWindowType
        Materials = 1
        Blueprints = 2
    End Enum

    ' To play ding sound without box
    Public Sub PlayNotifySound()
        If Not UserApplicationSettings.DisableSound Then
            My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
        End If
    End Sub

    Public Class LocationInfo
        Public AccountID As Long
        Public LocationID As Long
        Public FlagID As Integer ' ID in the INVENTORY_FLAGS Table
    End Class

    ' For error processing
    Public Structure MyError
        Dim Description As String
        Dim Number As Integer
    End Structure

    ' For importing blueprints via cut/paste
    Public Structure CopyPasteBlueprint
        Dim Name As String
        Dim Quantity As Long
        Dim Group As String
        Dim Category As String
        Dim Copy As Boolean
        Dim ML As Integer
        Dim PL As Integer
        Dim Runs As Integer
    End Structure

    ' For shopping list
    Public Structure ItemBuyType
        Dim ItemName As String
        Dim BuyType As String
    End Structure

    ' For updating the splash screen with what is going on
    Private Delegate Sub ProgressSetter(ByVal progress As String)

    Public Sub SetProgress(ByVal progress As String)
        'Get a reference to the application's splash screen.
        Dim splash As SplashScreen = DirectCast(My.Application.SplashScreen, SplashScreen)

        'Invoke the spalsh screen's SetProgress method on the thread that owns it.
        splash.Invoke(New ProgressSetter(AddressOf splash.SetProgress), progress)
    End Sub

#Region "DataBase"

    Public Sub InitDB()

        DB.ConnectionString = "Data Source=" & SQLiteDBFileName & ";Version=3;"
        DB.Open()
        Call ExecuteNonQuerySQL("PRAGMA synchronous = NORMAL; PRAGMA locking_mode = NORMAL; PRAGMA cache_size = 10000; PRAGMA page_size = 4096; PRAGMA temp_store = DEFAULT; PRAGMA journal_mode = TRUNCATE; PRAGMA count_changes = OFF")
        Call ExecuteNonQuerySQL("PRAGMA auto_vacuum = FULL;") ' Keep the DB small

    End Sub

    Public Sub CloseDB()
        DB.Close()
    End Sub

    Public Sub ExecuteNonQuerySQL(ByVal SQL As String)
        Dim DBExecuteCmd As SQLiteCommand

        ErrorTracker = SQL

        DBExecuteCmd = DB.CreateCommand
        DBExecuteCmd.CommandText = SQL
        DBExecuteCmd.ExecuteNonQuery()

        DBExecuteCmd.Dispose()

        ErrorTracker = ""

    End Sub

    Public Sub BeginSQLiteTransaction()
        Call ExecuteNonQuerySQL("BEGIN;")
    End Sub

    Public Sub CommitSQLiteTransaction()
        Call ExecuteNonQuerySQL("END;")
    End Sub

    Public Sub RollbackSQLiteTransaction()
        'DBTransaction.Rollback()
        Call ExecuteNonQuerySQL("ROLLBACK;")
    End Sub

#End Region

#Region "Taxes/Fees"

    ' Returns the tax on an item price only
    Public Function GetTaxes(ByVal ItemMarketCost As Double) As Double
        Dim Accounting As Integer = SelectedCharacter.Skills.GetSkillLevel(16622)
        ' Each level of accounting reduces tax by 10% - Starting level with Accounting 0 is 1.5% tax 
        Return (1.5 - (Accounting * 0.1 * 1.5)) / 100 * ItemMarketCost
    End Function

    ' Returns the tax on setting up a sell order for an item price only
    Public Function GetBrokerFee(ByVal ItemMarketCost As Double) As Double
        Dim BrokerRelations As Integer = SelectedCharacter.Skills.GetSkillLevel(3446)

        Dim TempFee As Double
        ' Old BrokerFee % = (1.000 % – 0.050 % × BrokerRelationsSkillLevel) / e ^ (0.1000 × FactionStanding + 0.04000 × CorporationStanding)
        ' BrokerFee % = (1.000 % – 0.050 % × BrokerRelationsSkillLevel) / 2 ^ (0.1400 × FactionStanding + 0.06000 × CorporationStanding) 
        'TempFee = ((1 - 0.05 * BrokerRelations) / Math.Exp(0.1 * UserApplicationSettings.BrokerFactionStanding + 0.04 * UserApplicationSettings.BrokerCorpStanding)) / 100 * ItemMarketCost
        TempFee = ((1 - 0.05 * BrokerRelations) / (2 ^ (0.14 * UserApplicationSettings.BrokerFactionStanding + 0.06 * UserApplicationSettings.BrokerCorpStanding))) / 100 * ItemMarketCost

        If TempFee < 100 Then
            Return 100
        Else
            Return TempFee
        End If

    End Function

#End Region

#Region "API Stuff"

    ' Loads the character for the program
    Public Sub LoadCharacter(RefreshAssets As Boolean, RefreshBPs As Boolean)

        ' Try to load the character
        If Not SelectedCharacter.LoadDefaultCharacter(False, RefreshAssets, RefreshBPs) Then

            ' Didn't find a default character. Either we don't have one selected or there are no characters in the DB yet
            ' Check for chars (corp chars do not count)
            Dim CMDCount As New SQLiteCommand("SELECT COUNT(*) FROM API WHERE API_TYPE IN ('Account','Character') AND CHARACTER_ID <> 0", DB)

            If CInt(CMDCount.ExecuteScalar()) = 0 Then
                ' No Characters selected, open the add char form
                Dim f1 = New frmLoadCharacterAPI
                f1.ShowDialog()
                If SelectedCharacter.Name <> None Then
                    ' Need to set a default, open that form
                    Dim f2 = New frmSetCharacterDefault
                    f2.ShowDialog()
                End If
            Else
                ' Have a set of chars, need to set a default, open that form
                Dim f2 = New frmSetCharacterDefault
                f2.ShowDialog()
            End If

            ' Now load the default character if they didn't X out. Don't refresh data, we just loaded it
            If DefaultCharSelected Then
                Call SelectedCharacter.LoadDefaultCharacter(False, RefreshAssets, RefreshBPs)
            End If

        End If

    End Sub

    ' Saves the sent data to the DB for later queries
    Public Sub SaveAccountAPIData(ByVal InsertData As Character, ByVal KeyType As String)
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String

        ' Character Data
        With InsertData
            ' Find out of the data exists, and insert if not
            If KeyType <> CorporationAPITypeName Then
                SQL = "SELECT * FROM API WHERE CHARACTER_ID = " & InsertData.ID
                SQL = SQL & " AND API_TYPE NOT IN ('Old Key', 'Corporation')"
            Else ' Corp Key
                SQL = "SELECT * FROM API WHERE CORPORATION_ID = " & InsertData.CharacterCorporation.CorporationID
                SQL = SQL & " AND API_TYPE = 'Corporation'"
            End If

            DBCommand = New SQLiteCommand(SQL, DB)
            readerCharacter = DBCommand.ExecuteReader

            If Not readerCharacter.Read Then
                ' Insert character data
                SQL = "INSERT INTO API (KEY_ID, API_KEY, CHARACTER_ID, CHARACTER_NAME, CORPORATION_ID, "
                SQL = SQL & "CORPORATION_NAME, CACHED_UNTIL, IS_DEFAULT, OVERRIDE_SKILLS, ACCESS_MASK, KEY_EXPIRATION_DATE, API_TYPE) "
                SQL = SQL & "VALUES (" & .KeyID & ",'" & .APIKey & "'," & .ID & ",'" & FormatDBString(.Name) & "',"
                SQL = SQL & .CharacterCorporation.CorporationID & ",'" & FormatDBString(.CharacterCorporation.CorporationName)
                SQL = SQL & "','" & Format(.CachedUntil, SQLiteDateFormat) & "'," & CInt(InsertData.IsDefault) & ",0,"
                SQL = SQL & .AccessMask & ",'" & Format(.APIExpiration, SQLiteDateFormat) & "','" & KeyType & "')"

                ' Inserting a new char won't have skills overrideen
                ' Since we have the data, save the override skill flag
                UserApplicationSettings.AllowSkillOverride = False

            Else
                ' Update character data
                SQL = "UPDATE API SET "
                SQL = SQL & "KEY_ID = " & .KeyID & ", API_KEY = '" & .APIKey & "', "
                SQL = SQL & "CHARACTER_NAME = '" & FormatDBString(.Name) & "', CORPORATION_ID = " & .CharacterCorporation.CorporationID & ", "
                SQL = SQL & "CORPORATION_NAME = '" & FormatDBString(.CharacterCorporation.CorporationName) & "', IS_DEFAULT = " & CInt(InsertData.IsDefault) & ", "
                SQL = SQL & "CACHED_UNTIL = '" & Format(.CachedUntil, SQLiteDateFormat) & "', " ' Cached assets set when they are accessed
                SQL = SQL & "ACCESS_MASK = " & .AccessMask & ", KEY_EXPIRATION_DATE = '" & Format(.APIExpiration, SQLiteDateFormat) & "', "
                SQL = SQL & "API_TYPE = '" & KeyType & "' "
                If KeyType <> CorporationAPITypeName Then
                    ' Update character
                    SQL = SQL & "WHERE CHARACTER_ID = " & InsertData.ID & " "
                    SQL = SQL & "AND API_TYPE NOT IN ('Old Key', 'Corporation')"
                Else ' Corp Key
                    SQL = SQL & "WHERE CORPORATION_ID = " & InsertData.CharacterCorporation.CorporationID & " "
                    SQL = SQL & "AND API_TYPE = 'Corporation'"
                End If

                ' Since we have the data, save the override skill flag
                UserApplicationSettings.AllowSkillOverride = CBool(If(readerCharacter.IsDBNull(9), 0, readerCharacter.GetInt32(9)))

            End If

        End With

        Call ExecuteNonQuerySQL(SQL)

        ' Update the Access mask of all keys to prevent duplicates
        SQL = "UPDATE API SET ACCESS_MASK = " & InsertData.AccessMask & " WHERE KEY_ID = " & InsertData.KeyID
        Call ExecuteNonQuerySQL(SQL)

        readerCharacter.Close()
        readerCharacter = Nothing
        DBCommand = Nothing

    End Sub

    ' Updates the key in the DB with data sent
    Public Function UpdateAccountAPIData(ByVal KeyID As Long, ByVal AccountAPIKey As String, ByVal APIType As String, Optional ByVal ID As Long = 0, Optional IsDefault As Boolean = False) As Boolean
        Dim TempCharacter() As Character ' char or corp
        Dim API As New EVEAPI
        Dim TempErrorText As String = ""
        Dim TempKeyType As String = ""

        ' Get the data for this character
        TempCharacter = API.GetAccountCharacters(KeyID, AccountAPIKey, TempKeyType, ID)
        TempErrorText = API.GetErrorText

        If TempKeyType = "" Then
            TempKeyType = APIType
        End If

        ' Errorcheck api
        If NoAPIError(TempErrorText, TempKeyType) Then
            If IsDefault Then
                TempCharacter(0).IsDefault = True
            Else
                TempCharacter(0).IsDefault = False
            End If

            ' Save the new data for the character
            Call SaveAccountAPIData(TempCharacter(0), TempKeyType)
            Return True
        Else
            ' If it's an expired key, update the data in the API including a cache date that is set by CCP (I think is to not allow expired keys to keep banging the api)
            Call UpdateIfExpiredKeyData(API.GetAPIErrorData, KeyID, AccountAPIKey)
            Return False
        End If

    End Function

    ' Updates the DB with a cachedate and key expiration if the key is expired
    Public Sub UpdateIfExpiredKeyData(APIError As EVEAPI.ErrorData, KeyID As Long, APIKey As String)
        If APIError.ErrorText = "Key has expired. Contact key owner for access renewal." Then
            Dim SQL As String

            SQL = "UPDATE API SET KEY_EXPIRATION_DATE = '" & ExpiredKey & "' "
            If APIError.CacheDate <> NoDate Then
                SQL = SQL & ", CACHED_UNTIL = '" & Format(APIError.CacheDate, SQLiteDateFormat) & "' "
            End If
            SQL = SQL & "WHERE KEY_ID = " & CStr(KeyID) & " AND API_KEY = '" & APIKey & "' "

            Call ExecuteNonQuerySQL(SQL)

        End If
    End Sub

#End Region

#Region "Facility Functions"

    ' Returns the type of production done for the activity and bp data sent
    Public Function GetProductionType(Activity As String, ItemGroupID As Long, ItemCategoryID As Long, FacilityType As String) As IndustryType
        Dim SelectedIndyType As IndustryType

        ' Determine if it's a fuel block, module, or big ship that can use a multi-use array
        If FacilityType = POSFacility And (ItemGroupID = 1136 _
                                           Or ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
                                           Or ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 _
                                           Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649 _
                                           ) And Activity = ActivityManufacturing Then
            If ItemGroupID = 1136 Then
                SelectedIndyType = IndustryType.POSFuelBlockManufacturing
            ElseIf ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 Then
                SelectedIndyType = IndustryType.POSLargeShipManufacturing
            ElseIf ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
                Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649 Then
                SelectedIndyType = IndustryType.POSModuleManufacturing
            End If
        Else
            Select Case Activity
                Case ActivityManufacturing
                    ' Need to load selected manufacturing facility
                    Select Case ItemGroupID
                        Case SupercarrierGroupID, TitanGroupID
                            SelectedIndyType = IndustryType.SuperManufacturing
                        Case BoosterGroupID
                            SelectedIndyType = IndustryType.BoosterManufacturing
                        Case CarrierGroupID, DreadnoughtGroupID, CapitalIndustrialShipGroupID
                            SelectedIndyType = IndustryType.CapitalManufacturing
                        Case StrategicCruiserGroupID
                            SelectedIndyType = IndustryType.T3CruiserManufacturing
                        Case TacticalDestroyerGroupID
                            SelectedIndyType = IndustryType.T3DestroyerManufacturing
                        Case Else
                            SelectedIndyType = IndustryType.Manufacturing

                            If ItemCategoryID = SubsystemCategoryID Then
                                SelectedIndyType = IndustryType.SubsystemManufacturing
                            ElseIf ItemCategoryID = ComponentCategoryID And ItemGroupID <> StationPartsGroupID Then
                                ' Add category for component
                                If ItemGroupID = CapitalComponentGroupID Or ItemGroupID = AdvCapitalComponentGroupID Then
                                    SelectedIndyType = IndustryType.CapitalComponentManufacturing ' These all use cap components
                                Else
                                    SelectedIndyType = IndustryType.ComponentManufacturing
                                End If
                                SelectedIndyType = IndustryType.ComponentManufacturing
                            ElseIf NoPOSCategoryIDs.Contains(ItemCategoryID) Or ItemGroupID = StationEggGroupID Or ItemGroupID = StationPartsGroupID Then
                                SelectedIndyType = IndustryType.NoPOSManufacturing
                            End If
                    End Select
                Case ActivityComponentManufacturing
                    SelectedIndyType = IndustryType.ComponentManufacturing
                Case ActivityCapComponentManufacturing
                    SelectedIndyType = IndustryType.CapitalComponentManufacturing
                Case ActivityCopying
                    SelectedIndyType = IndustryType.Copying
                Case ActivityInvention
                    If ItemCategoryID = SubsystemCategoryID Or ItemGroupID = StrategicCruiserGroupID Then
                        ' Need to invent this at a station or pos
                        SelectedIndyType = IndustryType.T3Invention
                    Else
                        SelectedIndyType = IndustryType.Invention
                    End If
            End Select
        End If

        Return SelectedIndyType

    End Function

    ' Returns the facility with sent indytype of facilities
    Public Function GetManufacturingFacility(IndyType As IndustryType, Tab As String, Optional Clone As Boolean = True) As IndustryFacility
        Dim FacilityReference As IndustryFacility

        If Tab = BPTab Then
            Select Case IndyType
                Case IndustryType.Manufacturing
                    FacilityReference = SelectedBPManufacturingFacility
                Case IndustryType.BoosterManufacturing
                    FacilityReference = SelectedBPBoosterManufacturingFacility
                Case IndustryType.CapitalManufacturing
                    FacilityReference = SelectedBPCapitalManufacturingFacility
                Case IndustryType.CapitalComponentManufacturing
                    FacilityReference = SelectedBPComponentManufacturingFacility
                Case IndustryType.ComponentManufacturing
                    FacilityReference = SelectedBPComponentManufacturingFacility
                Case IndustryType.SubsystemManufacturing
                    FacilityReference = SelectedBPSubsystemManufacturingFacility
                Case IndustryType.SuperManufacturing
                    FacilityReference = SelectedBPSuperManufacturingFacility
                Case IndustryType.T3CruiserManufacturing
                    FacilityReference = SelectedBPT3CruiserManufacturingFacility
                Case IndustryType.T3DestroyerManufacturing
                    FacilityReference = SelectedBPT3DestroyerManufacturingFacility
                Case IndustryType.POSFuelBlockManufacturing
                    FacilityReference = SelectedBPPOSFuelBlockFacility
                Case IndustryType.POSLargeShipManufacturing
                    FacilityReference = SelectedBPPOSLargeShipFacility
                Case IndustryType.POSModuleManufacturing
                    FacilityReference = SelectedBPPOSModuleFacility
                Case IndustryType.NoPOSManufacturing
                    FacilityReference = SelectedBPNoPOSFacility
                Case IndustryType.Invention
                    FacilityReference = SelectedBPInventionFacility
                Case IndustryType.T3Invention
                    FacilityReference = SelectedBPT3InventionFacility
                Case IndustryType.Copying
                    FacilityReference = SelectedBPCopyFacility
                Case Else
                    FacilityReference = SelectedBPManufacturingFacility
            End Select
        Else
            Select Case IndyType
                Case IndustryType.Manufacturing
                    FacilityReference = SelectedCalcBaseManufacturingFacility
                Case IndustryType.BoosterManufacturing
                    FacilityReference = SelectedCalcBoosterManufacturingFacility
                Case IndustryType.CapitalManufacturing
                    FacilityReference = SelectedCalcCapitalManufacturingFacility
                Case IndustryType.ComponentManufacturing
                    FacilityReference = SelectedCalcComponentManufacturingFacility
                Case IndustryType.CapitalComponentManufacturing
                    FacilityReference = SelectedCalcComponentManufacturingFacility
                Case IndustryType.SubsystemManufacturing
                    FacilityReference = SelectedCalcSubsystemManufacturingFacility
                Case IndustryType.SuperManufacturing
                    FacilityReference = SelectedCalcSuperManufacturingFacility
                Case IndustryType.T3DestroyerManufacturing
                    FacilityReference = SelectedCalcT3DestroyerManufacturingFacility
                Case IndustryType.T3CruiserManufacturing
                    FacilityReference = SelectedCalcT3CruiserManufacturingFacility
                Case IndustryType.POSFuelBlockManufacturing
                    FacilityReference = SelectedCalcPOSFuelBlockFacility
                Case IndustryType.POSLargeShipManufacturing
                    FacilityReference = SelectedCalcPOSLargeShipFacility
                Case IndustryType.POSModuleManufacturing
                    FacilityReference = SelectedCalcPOSModuleFacility
                Case IndustryType.NoPOSManufacturing
                    FacilityReference = SelectedCalcNoPOSFacility
                Case IndustryType.Invention
                    FacilityReference = SelectedCalcInventionFacility
                Case IndustryType.T3Invention
                    FacilityReference = SelectedCalcT3InventionFacility
                Case IndustryType.Copying
                    FacilityReference = SelectedCalcCopyFacility
                Case Else
                    FacilityReference = SelectedCalcBaseManufacturingFacility
            End Select
        End If

        If Clone Then
            Return CType(FacilityReference.Clone(), IndustryFacility)
        Else
            Return FacilityReference ' Only return the reference
        End If

    End Function

#End Region

    Public Function GetBPType(BPTypeValue As Object) As BPType

        If IsNothing(BPTypeValue) Then
            Return BPType.NotOwned
        End If

        If IsDBNull(BPTypeValue) Then
            Return BPType.NotOwned
        End If

        Select Case CInt(BPTypeValue)
            Case BPType.Original
                Return BPType.Original
            Case BPType.Copy
                Return BPType.Copy
            Case BPType.InventedBPC
                Return BPType.InventedBPC
            Case BPType.NotOwned
                Return BPType.NotOwned
        End Select

        Return BPType.NotOwned

    End Function

    Public Function GetBPTypeString(BPTypeValue As Object) As String

        If IsNothing(BPTypeValue) Then
            Return UnownedBP
        End If

        If IsDBNull(BPTypeValue) Then
            Return UnownedBP
        End If

        Select Case CInt(BPTypeValue)
            Case BPType.Original
                Return BPO
            Case BPType.Copy
                Return BPC
            Case BPType.InventedBPC
                Return InventedBPC
            Case BPType.NotOwned
                Return UnownedBP
        End Select

        Return UnownedBP

    End Function

    ' Function takes a recordset reference and processes it to return the cache date from the query
    ' Assumes the first field is the cache date
    Public Function ProcessCacheDate(ByRef rsCache As SQLiteDataReader) As Date
        Dim CacheDate As Date

        Try
            If rsCache.Read Then
                If Not IsDBNull(rsCache.GetValue(0)) Then
                    If rsCache.GetString(0) = "" Then
                        CacheDate = NoDate
                    Else
                        CacheDate = CDate(rsCache.GetString(0))
                    End If
                Else
                    CacheDate = NoDate
                End If
            Else
                CacheDate = NoDate
            End If
        Catch ex As Exception
            CacheDate = NoDate
        End Try

        Return CacheDate

    End Function

    ' Converts a time in d h m s to a long of seconds
    Public Function ConvertDHMSTimetoSeconds(ByVal SentTime As String) As Long
        Dim Days As Integer = 0
        Dim Hours As Integer = 0
        Dim Minutes As Integer = 0
        Dim Seconds As Integer = 0

        SentTime = Trim(SentTime)

        If SentTime.Contains("d ") Then
            ' Get the days
            Days = CInt(SentTime.Substring(0, SentTime.IndexOf("d")))
            ' Reset the string
            SentTime = Trim(SentTime.Substring(SentTime.IndexOf("d") + 1))
        End If

        If SentTime.Contains("h ") Then
            ' Get the days
            Hours = CInt(SentTime.Substring(0, SentTime.IndexOf("h")))
            ' Reset the string
            SentTime = Trim(SentTime.Substring(SentTime.IndexOf("h") + 1))
        End If

        If SentTime.Contains("m ") Then
            ' Get the days
            Minutes = CInt(SentTime.Substring(0, SentTime.IndexOf("m")))
            ' Reset the string
            SentTime = Trim(SentTime.Substring(SentTime.IndexOf("m") + 1))
        End If

        If SentTime.Contains("s") Then
            ' Get the days
            Seconds = CInt(SentTime.Substring(0, SentTime.IndexOf("s")))
        End If

        Return (Days * 24 * 60 * 60) + (Hours * 60 * 60) + (Minutes * 60) + Seconds

    End Function

    ' Takes text from copy and paste from game and parses it, returns nothing if not, list of parsed materials if successful
    Public Function ImportCopyPasteText(SentText As String) As Materials
        Dim SQL As String
        Dim readerItem As SQLiteDataReader
        Dim TempMaterial As Material
        Dim CopyPasteMaterials As New Materials
        Dim TempQuantity As Long
        Dim ItemLines As String() = Nothing
        Dim ItemColumns As String() = Nothing

        ' Format of imported text for items will always be: Name, Quantity, Group, Category, Size, Slot, Volume, Tech Level
        ' Users can remove columns but the general rule is Name and quantity first, they can separate lines by three ways
        If SentText.Contains(vbCrLf) Then
            ItemLines = SentText.Split(New [Char]() {CChar(vbCrLf)}, StringSplitOptions.RemoveEmptyEntries) ' Get all the item lines
        ElseIf SentText.Contains(vbCr) Then
            ItemLines = SentText.Split(New [Char]() {CChar(vbCr)}, StringSplitOptions.RemoveEmptyEntries) ' Get all the item lines
        ElseIf SentText.Contains(vbLf) Then
            ItemLines = SentText.Split(New [Char]() {CChar(vbLf)}, StringSplitOptions.RemoveEmptyEntries) ' Get all the item lines
        End If

        ' Loop through the lines
        If Not IsNothing(ItemLines) Then
            For i = 0 To ItemLines.Count - 1

                ' Clean up the item line if it has spare lf's or cr's
                ItemLines(i) = ItemLines(i).Replace(vbCr, "")
                ItemLines(i) = ItemLines(i).Replace(vbLf, "")
                ItemLines(i) = ItemLines(i).Replace(vbCrLf, "")

                ' How they split out the columns can be done with Tab, Semicolon, Space or in a final case, comma (because numbers will likely have commas)
                If ItemLines(i).Contains(vbTab) Then
                    ItemColumns = ItemLines(i).Split(New [Char]() {CChar(vbTab)}, StringSplitOptions.RemoveEmptyEntries)
                ElseIf ItemLines(i).Contains(";") Then
                    ItemColumns = ItemLines(i).Split(New [Char]() {";"c}, StringSplitOptions.RemoveEmptyEntries)
                ElseIf ItemLines(i).Contains(" ") Then
                    ItemColumns = ItemLines(i).Split(New [Char]() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                    ' After importing a space, make sure we have a full name and then a number before processing. 
                    ' For example, Capital Armor Plates 33 would be a 4 index array but we want to combine the first 3
                    Dim TempString As String = ""
                    Dim TempNumber As String = ""

                    For j = 0 To ItemColumns.Count - 1
                        If Not IsNumeric(ItemColumns(j)) Then
                            TempString = TempString & ItemColumns(j) & " "
                        Else
                            TempNumber = ItemColumns(j)
                        End If
                    Next

                    'Now reset the Item columns
                    ReDim ItemColumns(1)
                    ItemColumns(0) = Trim(TempString)
                    ItemColumns(1) = Trim(TempNumber)

                ElseIf ItemLines(i).Contains(",") Then
                    ItemColumns = ItemLines(i).Split(New [Char]() {","c})
                Else
                    Exit For ' Don't process
                    'Dim itemcolumns As String() = ItemLines(i).Split(New String() {"   "}, StringSplitOptions.RemoveEmptyEntries)
                End If

                SQL = "SELECT typeID FROM INVENTORY_TYPES WHERE typeName = '" & FormatDBString(ItemColumns(0)) & "'"

                DBCommand = New SQLiteCommand(SQL, DB)
                readerItem = DBCommand.ExecuteReader
                readerItem.Read()

                If readerItem.HasRows Then
                    ' The next item in the list will be the quantity, if not it might be a can or something, so skip it
                    If IsNumeric(ItemColumns(1)) Or ItemColumns(1) = "" Then ' Unpackaged items are 1
                        If ItemColumns(1) = "" Then
                            TempQuantity = 1
                        Else
                            TempQuantity = CLng(ItemColumns(1))
                        End If
                        TempMaterial = New Material(CLng(readerItem.GetValue(0)), ItemColumns(0), "", TempQuantity, 0, 0, "")
                        Call CopyPasteMaterials.InsertMaterial(TempMaterial)
                    End If
                End If

                readerItem = Nothing

            Next
        End If

        Return CopyPasteMaterials

    End Function

    ' Imports sent blueprint to shopping list
    Public Sub AddToShoppingList(SentBlueprint As Blueprint, BuildBuy As Boolean, RawMatsCopy As Boolean, _
                                 ComponentCopy As Boolean, _
                                 FacilityMEModifier As Double, _
                                 BuiltInPOS As Boolean, _
                                 IgnoreInvention As Boolean, _
                                 IgnoreMinerals As Boolean, _
                                 IgnoreT1ITem As Boolean, _
                                 Optional IgnoreRefresh As Boolean = False, _
                                 Optional CopyInventionMatsOnly As Boolean = False)
        Dim TempMats As New Materials
        Dim ShoppingItem As New ShoppingListItem
        Dim ShoppingBuildList As New BuiltItemList
        Dim ShoppingBuyList As New Materials
        Dim InventionCopyUsage As Double = 0

        With ShoppingItem
            If RawMatsCopy Or BuildBuy = True Then ' Either just raw or build buy selected
                ' Add the item and the materials for the item
                If Not IsNothing(SentBlueprint.GetRawMaterials) Then
                    .TypeID = SentBlueprint.GetItemID
                    .Name = SentBlueprint.GetBPItemData.GetMaterialName
                    .Quantity = SentBlueprint.GetBPItemData.GetQuantity

                    .ItemMarketCost = SentBlueprint.GetItemMarketPrice
                    .ItemBuildTime = SentBlueprint.GetTotalProductionTime
                    .ItemME = SentBlueprint.GetME
                    .TotalMarketCost = SentBlueprint.GetItemMarketPrice
                    .TotalBuildTime = SentBlueprint.GetTotalProductionTime

                    .FacilityMEModifier = FacilityMEModifier ' For full item, components will be saved in blueprint class for ComponentList
                    .BuiltInPOS = BuiltInPOS

                    ' Ignore flags
                    .IgnoredInvention = IgnoreInvention
                    .IgnoredMinerals = IgnoreMinerals
                    .IgnoredT1BaseItem = IgnoreT1ITem

                    If BuildBuy Then
                        .BuildType = "Build/Buy"
                    Else
                        ' Just insert the materials in components since we are building all
                        .BuildType = "Raw Mats"
                    End If

                    .NumBPs = SentBlueprint.GetUsedNumBPs

                    If Not CopyInventionMatsOnly Then
                        ShoppingBuyList = CType(SentBlueprint.GetRawMaterials.Clone, Materials) ' Need a deep copy because we might insert later
                        ShoppingBuildList = CType(SentBlueprint.GetBPComponentsList.Clone, BuiltItemList)
                    End If

                End If

            ElseIf ComponentCopy Then
                ' Add the component items and mats to the list and that's it. They are building the end item, nothing else
                If Not IsNothing(SentBlueprint.GetComponentMaterials) Then
                    .TypeID = SentBlueprint.GetItemID
                    .Name = SentBlueprint.GetBPItemData.GetMaterialName
                    .Quantity = SentBlueprint.GetBPItemData.GetQuantity

                    .ItemMarketCost = SentBlueprint.GetItemMarketPrice
                    .ItemBuildTime = SentBlueprint.GetProductionTime
                    .ItemME = SentBlueprint.GetME
                    .TotalMarketCost = SentBlueprint.GetItemMarketPrice
                    .TotalBuildTime = SentBlueprint.GetProductionTime

                    .FacilityMEModifier = FacilityMEModifier ' For full item, components will be saved in blueprint class for ComponentList
                    .BuiltInPOS = BuiltInPOS

                    ' Ignore flags
                    .IgnoredInvention = IgnoreInvention
                    .IgnoredMinerals = IgnoreMinerals
                    .IgnoredT1BaseItem = IgnoreT1ITem

                    .NumBPs = SentBlueprint.GetUsedNumBPs

                    .BuildType = "Components"

                    If Not CopyInventionMatsOnly Then
                        ShoppingBuyList = CType(SentBlueprint.GetComponentMaterials.Clone, Materials) ' Need a deep copy because we might insert later
                        ShoppingBuildList = Nothing
                    End If

                End If
            End If

            If SentBlueprint.GetTechLevel = BlueprintTechLevel.T2 Or SentBlueprint.GetTechLevel = BlueprintTechLevel.T3 Then
                If UserApplicationSettings.ShopListIncludeInventMats = True Then
                    ' Save the list of invention materials
                    .InventionMaterials = CType(SentBlueprint.GetInventionMaterials.Clone, Materials)

                    ' Now insert into main buy List 
                    ShoppingBuyList.InsertMaterialList(.InventionMaterials.GetMaterialList)

                    ' Remove the data interface though, we will assume they don't want to buy this but this will get listed in the copy output (sent above)
                    ShoppingBuyList.RemoveMaterial(SentBlueprint.GetInventionMaterials.SearchListbyName("Data Interface"))

                    If Not IsNothing(ShoppingBuyList.SearchListbyName("Invention Usage")) Then
                        InventionCopyUsage = InventionCopyUsage + ShoppingBuyList.SearchListbyName("Invention Usage").GetCostPerItem
                    End If

                    If Not IsNothing(ShoppingBuyList.SearchListbyName("Copy Usage")) Then
                        InventionCopyUsage = InventionCopyUsage + ShoppingBuyList.SearchListbyName("Copy Usage").GetCostPerItem
                    End If

                    ' Remove the usage as well
                    ShoppingBuyList.RemoveMaterial(SentBlueprint.GetInventionMaterials.SearchListbyName("Invention Usage"))
                    ShoppingBuyList.RemoveMaterial(SentBlueprint.GetInventionMaterials.SearchListbyName("Copy Usage"))
                End If

                ' How many runs do we need to invent this?
                .AvgInvRunsforSuccess = 1 / SentBlueprint.GetInventionChance
                .InventedRuns = SentBlueprint.GetSingleInventedBPCRuns
                .InventionJobs = SentBlueprint.GetInventionJobs

                ' Decryptor if used
                .Decryptor = SentBlueprint.GetDecryptor.Name
                .Relic = SentBlueprint.GetRelic

                ' Add the cost
                .InventionCost = SentBlueprint.GetBPInventionCost
                .InventionUsage = SentBlueprint.GetBPInventionUsage

            End If

            ' Taxes and usage
            .ItemTaxes = SentBlueprint.GetBPTaxes
            .ItemBrokerFees = SentBlueprint.GetBPBrokerFees
            .ItemUsage = SentBlueprint.GetManufacturingFacilityUsage + InventionCopyUsage

            ' Volume of the item(s)
            .BuildVolume = SentBlueprint.GetTotalItemVolume

            ' Finally set techlevel
            .TechLevel = SentBlueprint.GetTechLevel

            ' All blueprint build types we want to save the base materials to build the bp
            ShoppingItem.BPMaterialList = CType(SentBlueprint.GetComponentMaterials.Clone, Materials)

        End With

        ' Add the final item and mark as items in list
        TotalShoppingList.InsertShoppingItem(ShoppingItem, ShoppingBuildList, ShoppingBuyList)

        ' Refresh the data if it's open
        If frmShop.Visible And Not IgnoreRefresh Then
            Call frmShop.RefreshLists()
        End If

    End Sub

    ' Enables Cut, Copy, Paste, and Select all from shortcut key entry for the sent text box
    Public Function ProcessCutCopyPasteSelect(SentBox As TextBox, e As System.Windows.Forms.KeyEventArgs) As Boolean

        If e.KeyCode = Keys.A AndAlso e.Control = True Then ' Select All
            SentBox.SelectAll()
        ElseIf e.KeyCode = Keys.X AndAlso e.Control = True Then ' Cut
            SentBox.Cut()
        ElseIf e.KeyCode = Keys.C AndAlso e.Control = True Then ' Copy
            SentBox.Copy()
        ElseIf e.KeyCode = Keys.V AndAlso e.Control = True Then ' Paste
            SentBox.Paste()
            Return True
        End If

        Return False

    End Function

    ' After a price update in any location that updates prices, we want to refresh all the prices and grids on every tab 
    Public Sub UpdateProgramPrices()

        ' Update the Update Prices tab
        Call frmMain.UpdatePriceList()

        ' Update the shopping list
        Call UpdateShoppingListPrices()

        ' Reset manufacturing calc button
        Call frmMain.ResetRefresh()

        ' Refresh the BP Tab if there is a blueprint selected
        If Not IsNothing(SelectedBlueprint) Then
            Call frmMain.RefreshBP(True)
        End If

        ' Refresh the Mining Tab
        If frmMain.lstMineGrid.Items.Count > 0 Then
            Call frmMain.LoadMiningGrid()
        End If

        ' Refresh the Reactions Tab
        If frmMain.lstReactions.Items.Count > 0 Then
            Call frmMain.UpdateReactionsGrid()
        End If

        ' Refresh the prices in manual update for minerals
        Call frmManualPriceUpdate.LoadMineralPrices()

        ' Manual update of moon materials
        Call frmManualPriceUpdate.LoadMoonPrices()

        ' Refill the search grid on manual updates
        If Trim(frmManualPriceUpdate.lblLSelectedItem.Text) <> "" Then
            Call frmManualPriceUpdate.FillSearchGrid(frmManualPriceUpdate.lblSelectedItem.Text)
        End If

    End Sub

    ' Limits the referenced text box between 0 and 10/20 on text
    Public Sub VerifyMETEEntry(ByRef METETextBox As TextBox, Type As String)
        If Trim(METETextBox.Text) <> "" Then
            If Type = "ME" Then
                If Not IsNumeric(METETextBox) Then
                    If CInt(METETextBox.Text) < 0 Then
                        METETextBox.Text = "0"
                    ElseIf CInt(METETextBox.Text) > 10 Then
                        METETextBox.Text = "10"
                    End If
                Else
                    METETextBox.Text = ""
                End If
            Else
                If Not IsNumeric(METETextBox) Then
                    If CInt(METETextBox.Text) < 0 Then
                        METETextBox.Text = "0"
                    ElseIf CInt(METETextBox.Text) > 20 Then
                        METETextBox.Text = "20"
                    End If
                Else
                    METETextBox.Text = ""
                End If
            End If
        End If
    End Sub

    ' Formats seconds into a time for display with days, hours, min, sec
    Public Function FormatTimeToComplete(TimeinSeconds As Long) As String
        Dim FinalTime As String = ""
        Dim Days As Long
        Dim Hours As Long
        Dim Minutes As Long
        Dim Seconds As Long

        Seconds = TimeinSeconds
        Days = CLng(Math.Floor(Seconds / (24 * 60 * 60)))
        Seconds = Seconds - (Days * 24 * 60 * 60)
        Hours = CLng(Math.Floor(Seconds / (60 * 60)))
        Seconds = Seconds - (Hours * 60 * 60)
        Minutes = CLng(Math.Floor(Seconds / 60))
        Seconds = Seconds - (Minutes * 60)

        If Days <> 0 Then
            FinalTime = CStr(Days) & "d " & CStr(Hours) & "h " & CStr(Minutes) & "m " & CStr(Seconds) & "s"
        ElseIf Days = 0 And Hours <> 0 Then
            FinalTime = CStr(Hours) & "h " & CStr(Minutes) & "m " & CStr(Seconds) & "s"
        ElseIf Days = 0 And Hours = 0 And Minutes <> 0 Then
            FinalTime = CStr(Minutes) & "m " & CStr(Seconds) & "s"
        ElseIf Days = 0 And Hours = 0 And Minutes = 0 And Seconds <> 0 Then
            FinalTime = CStr(Seconds) & "s"
        End If

        Return FinalTime

    End Function

    ' Checks the error sent for EVE API data and shows forms etc based on error
    Public Function NoAPIError(ByVal ErrorText As String, ByVal KeyType As String) As Boolean
        Dim fAccessError As New frmAPIError

        If (ErrorText = NoSkillsLoaded Or ErrorText = NoStandingsLoaded Or ErrorText = NoSkillsStandingsLoaded Or ErrorText = "") And KeyType <> "Corporation" And KeyType <> "" Then
            If ErrorText = NoSkillsStandingsLoaded Then
                fAccessError.ErrorText = "This API did not allow skills or standings to be loaded for associated characters." & _
                    Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'Standings' under the 'Public Information' section and 'CharacterSheet' under the 'Private Information' section to include skills and standings and then reload the API."
                fAccessError.Text = "API: No Skills or Standings Loaded"
            ElseIf ErrorText = NoStandingsLoaded Then
                fAccessError.ErrorText = "This API did not allow standings to be loaded for associated characters." & _
                    Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'Standings' under the 'Public Information' section to include standings and then reload the API."
                fAccessError.Text = "API: No Standings Loaded"
            ElseIf ErrorText = NoSkillsLoaded Then
                fAccessError.ErrorText = "This API did not allow skills to be loaded for associated characters." & _
                    Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'CharacterSheet' under the 'Private Information' section to include skills and then reload the API."
                fAccessError.Text = "API: No Skills Loaded"
            End If

            If ErrorText <> "" Then
                fAccessError.ErrorLink = "http://support.eveonline.com/api/Key/CreatePredefined/589962/"
                fAccessError.ShowDialog()
            End If

        ElseIf ErrorText <> "" Then
            ' Error returned
            Call TopMostMessageBox.Show("Unable to update " & KeyType & " API data. Error Text: " & ErrorText & Environment.NewLine _
                                        & Environment.NewLine & "If one of your APIs changed recently, please update it through the Manage Accounts Menu.")
            Return False

        End If

        Return True

    End Function

    ' Returns a bit string for the number sent
    Public Function GetBits(ByVal inNumber As Long) As String
        Dim BS As String = ""

        While inNumber > 0
            BS = inNumber Mod 2 & BS
            inNumber = inNumber \ 2
        End While

        Return BS

    End Function

    ' Sets column sort order for the sorter and args sent
    Public Sub SetLstVwColumnSortOrder(ByVal EventArgs As System.Windows.Forms.ColumnClickEventArgs, ByRef SentColumnSorter As ListViewColumnSorter)
        ' Determine if the clicked column is already the column that is 
        ' being sorted.
        If Not IsNothing(SentColumnSorter) Then
            If (EventArgs.Column = SentColumnSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (SentColumnSorter.Order = SortOrder.Ascending) Then
                    SentColumnSorter.Order = SortOrder.Descending
                Else
                    SentColumnSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                SentColumnSorter.SortColumn = EventArgs.Column
                If SentColumnSorter.Order = SortOrder.None Then
                    SentColumnSorter.Order = SortOrder.Ascending
                End If
            End If
        End If
    End Sub

    ' Takes a time in seconds and converts it to a string display of Days HH:MM:SS
    Public Function FormatIPHTime(ByVal SentTimeString As Double) As String
        Dim Seconds As Long
        Dim Minutes As Integer
        Dim Hours As Integer
        Dim Days As Integer
        Dim TimeString As String = ""

        Seconds = CLng(SentTimeString)

        ' Calcuate Days
        Days = CInt(Seconds \ 86400)
        Seconds = Seconds Mod 86400
        'Calculate Hours and remaining Seconds
        Hours = CInt(Seconds \ 3600)
        Seconds = Seconds Mod 3600
        'Calculate Minutes and remaining Seconds
        Minutes = CInt(Seconds \ 60)
        Seconds = Seconds Mod 60

        ' Add Days on if needed
        If Days <> 0 Then
            TimeString = CStr(Days) & " Days "
        End If

        Return (TimeString & Format(Hours, "00") & ":" & Format(Minutes, "00") & ":" & Format(Seconds, "00"))

    End Function

    ' Takes a date/time like "1d 22h 38m 46s" and converts it to seconds
    Public Function FormatStringdate(ByVal SentTimeString As String) As Long
        Dim Days As Integer
        Dim Hours As Integer
        Dim Minutes As Integer
        Dim Seconds As Integer

        Dim strArr() As String
        Dim count As Integer

        On Error GoTo InvalidDate

        If SentTimeString = "" Then
            GoTo InvalidDate
        End If

        ' Break up the string sections
        strArr = SentTimeString.Split(New Char() {" "c})

        For count = strArr.Count - 1 To 0 Step -1
            ' Loop from seconds to the days
            If strArr(count).Substring(strArr(count).Length - 1) = "s" Then
                If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "s") - 1)) Then
                    GoTo InvalidDate
                Else
                    Seconds = CInt(strArr(count).Substring(0, InStr(strArr(count), "s") - 1))
                End If
            End If

            If strArr(count).Substring(strArr(count).Length - 1) = "m" Then
                If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "m") - 1)) Then
                    GoTo InvalidDate
                Else
                    Seconds = CInt(strArr(count).Substring(0, InStr(strArr(count), "m") - 1))
                End If
            End If

            If strArr(count).Substring(strArr(count).Length - 1) = "h" Then
                If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "h") - 1)) Then
                    GoTo InvalidDate
                Else
                    Seconds = CInt(strArr(count).Substring(0, InStr(strArr(count), "h") - 1))
                End If
            End If

            If strArr(count).Substring(strArr(count).Length - 1) = "d" Then
                If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "d") - 1)) Then
                    GoTo InvalidDate
                Else
                    Seconds = CInt(strArr(count).Substring(0, InStr(strArr(count), "d") - 1))
                End If
            End If

        Next

        Return CInt(Seconds) + (60 * CInt(Minutes)) + (360 * CInt(Hours)) + (360 * 24 * CInt(Days))

InvalidDate:

        On Error Resume Next
        Return -1

    End Function

    ' Takes a date/time like "1d 22h 38m 6s" and sees if it is a date/time
    Public Function IsStringdate(ByVal SentTimeString As String) As Boolean
        Dim strArr() As String
        Dim count As Integer

        If SentTimeString = "" Then
            Return False
        End If

        ' Break up the string sections
        strArr = SentTimeString.Split(New Char() {" "c})

        For count = strArr.Count - 1 To 0 Step -1
            ' Loop from seconds to the days
            Select Case strArr(count).Substring(strArr(count).Length - 1)
                Case "s"
                    If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "s") - 1)) Then
                        Return False
                    End If
                Case "m"
                    If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "m") - 1)) Then
                        Return False
                    End If
                Case "h"
                    If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "h") - 1)) Then
                        Return False
                    End If
                Case "d"
                    If Not IsNumeric(strArr(count).Substring(0, InStr(strArr(count), "d") - 1)) Then
                        Return False
                    End If
                Case Else
                    Return False
            End Select

        Next

        Return True

    End Function

    ' Checks for program updates
    Public Sub CheckForUpdates(ByVal ShowFinalMessage As Boolean, ByVal ProgramIcon As Icon)
        Dim Response As DialogResult
        ' Program Updater
        Dim Updater As New ProgramUpdater
        Dim UpdateCode As Integer

        ' 1 = Update Available, 0 No Update Available, -1 an error occured and msg box already shown
        UpdateCode = Updater.IsProgramUpdatable

        If UpdateCode = 1 Then

            Response = TopMostMessageBox.Show("Update Available - Do you want to update now?", Application.ProductName, MessageBoxButtons.YesNo, ProgramIcon)

            If Response = DialogResult.Yes Then
                ' Run the updater
                Call Updater.RunUpdate()
            End If
        ElseIf ShowFinalMessage And UpdateCode = 0 Then
            MsgBox("No updates available.", vbInformation, Application.ProductName)
        End If

        ' Clean up files used to check
        Call Updater.CleanUpFiles()

    End Sub

    ' Converts a US Decimal to a EU Decimal
    Public Function ConvertUStoEUDecimal(ByVal USFormattedValue As String) As String
        Dim TempString As String

        TempString = USFormattedValue

        ' First replace any periods with pipes
        TempString = TempString.Replace(".", "|")

        ' Now change the commas to periods
        TempString = TempString.Replace(",", ".")

        ' Now change the pipes to commas
        TempString = TempString.Replace("|", ",")

        Return TempString

    End Function

    ' MD5 Hash - specify the path to a file and this routine will calculate your hash
    Public Function MD5CalcFile(ByVal filepath As String) As String

        ' Open file (as read-only) - If it's not there, return ""
        If IO.File.Exists(filepath) Then
            Using reader As New System.IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read)
                Using md5 As New System.Security.Cryptography.MD5CryptoServiceProvider

                    ' hash contents of this stream
                    Dim hash() As Byte = md5.ComputeHash(reader)

                    ' return formatted hash
                    Return ByteArrayToString(hash)

                End Using
            End Using
        End If

        ' Something went wrong
        Return ""

    End Function

    ' MD5 Hash - utility function to convert a byte array into a hex string
    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String

        Dim sb As New System.Text.StringBuilder(arrInput.Length * 2)

        For i As Integer = 0 To arrInput.Length - 1
            sb.Append(arrInput(i).ToString("X2"))
        Next

        Return sb.ToString().ToLower

    End Function

    ' Writes a sent message to a log file
    Public Sub WriteMsgToLog(ByVal ErrorMsg As String)
        Dim FilePath As String = UserWorkingFolder & "EVEIPH.log"
        Dim AllText() As String

        If Not IO.File.Exists(FilePath) Then
            Dim sw As IO.StreamWriter = IO.File.CreateText(FilePath)
            sw.Close()
        End If

        ' This is an easier way to get all of the strings in the file.
        AllText = IO.File.ReadAllLines(FilePath)
        ' This will append the string to the end of the file.
        My.Computer.FileSystem.WriteAllText(FilePath, CStr(Now) & ", " & ErrorMsg & Environment.NewLine, True)


    End Sub

    ' Function will take a string and return it in a DB friendly format - ie if it has single quotes in the string
    Public Function FormatDBString(ByVal inStrVar As String) As String
        ' Anything with quote mark in name it won't correctly load - need to replace with double quotes
        If InStr(inStrVar, "'") <> 0 Then
            inStrVar = Replace(inStrVar, "'", "''")
        End If
        Return inStrVar
    End Function

    ' Finds the T1 material for a T2 blueprint
    Public Function GetT1Material(ByVal BlueprintID As Long) As Material
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader
        Dim TempMat As Material
        Dim T1BPID As Long = 0

        ' Look up the blueprint we used to invent from the sent blueprint ID
        SQL = "SELECT blueprintTypeID from INDUSTRY_ACTIVITY_PRODUCTS WHERE productTypeID = " & BlueprintID & " AND activityID = 8"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerLookup = DBCommand.ExecuteReader

        If readerLookup.Read() Then
            T1BPID = CLng(readerLookup.GetInt64(0))
            readerLookup.Close()

            ' Select all materials now
            SQL = "SELECT ITEM_ID, ITEM_NAME, ITEM_GROUP FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID =" & T1BPID

            DBCommand = New SQLiteCommand(SQL, DB)
            readerLookup = DBCommand.ExecuteReader

            readerLookup.Read()
            TempMat = New Material(readerLookup.GetInt64(0), readerLookup.GetString(1), readerLookup.GetString(2), 1, 1, 0, "0")
            readerLookup.Close()

        Else
            Return Nothing
        End If

        readerLookup.Close()

        Return TempMat

    End Function

    ' Takes the BP ID and relic name (if sent) and returns the TypeID for the item to invent that BP
    Public Function GetInventItemTypeID(ByVal BlueprintTypeID As Long, ByVal RelicName As String) As Long
        Dim SQL As String
        Dim rsCheck As SQLite.SQLiteDataReader
        Dim InventItemTypeID As Long = 0

        ' What is the item we are using to invent?
        SQL = "SELECT blueprintTypeID from INDUSTRY_ACTIVITY_PRODUCTS, INVENTORY_TYPES WHERE productTypeID = " & BlueprintTypeID & " "
        SQL = SQL & "AND typeID = blueprintTypeID "

        If RelicName <> "" Then
            ' Need to add the relic variant to the query for just one item
            SQL = SQL & " AND typeName LIKE '%" & RelicName & "%'"
        End If

        DBCommand = New SQLiteCommand(SQL, DB)
        rsCheck = DBCommand.ExecuteReader

        If rsCheck.Read Then
            InventItemTypeID = rsCheck.GetInt64(0)
        End If

        Return InventItemTypeID

    End Function

    ' Returns the text race for the ID sent
    Public Function GetRace(ByVal RaceID As Integer) As String
        Dim rsLookup As SQLite.SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT RACE FROM RACE_IDS WHERE ID = " & CStr(RaceID), DB)
        rsLookup = DBCommand.ExecuteReader

        If rsLookup.Read Then
            Return rsLookup.GetString(0)
        Else
            Return ""
        End If

    End Function

    ' Gets the attribute by the name sent for the typeid or typename sent, if typeid sent, will use that first
    Public Function GetAttribute(ByVal AttributeName As String, ByVal TypeName As String) As Double
        Dim SQL As String
        Dim readerAttribute As SQLiteDataReader

        SQL = "SELECT CASE WHEN valueInt IS NULL THEN valueFloat ELSE valueInt END, ATTRIBUTE_TYPES.displayName "
        SQL = SQL & "FROM INVENTORY_TYPES, TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
        SQL = SQL & "WHERE INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.attributeID = ATTRIBUTE_TYPES.attributeID "
        SQL = SQL & "AND INVENTORY_TYPES.typeName ='" & FormatDBString(TypeName) & "' "
        SQL = SQL & "AND (ATTRIBUTE_TYPES.displayName= '" & AttributeName & "' OR ATTRIBUTE_TYPES.attributeName = '" & AttributeName & "')"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerAttribute = DBCommand.ExecuteReader

        If readerAttribute.Read Then
            Return readerAttribute.GetDouble(0)
        Else
            Return Nothing
        End If

    End Function

    ' Updates the shopping list with new prices
    Public Sub UpdateShoppingListPrices()
        ' Update the shopping list if there is anything there
        If TotalShoppingList.GetNumShoppingItems <> 0 Then
            Call TotalShoppingList.UpdateListPrices()
            ' Refresh the lists
            Call frmShop.RefreshLists()
        End If

    End Sub

    ' Try to catch exceptions when the clipboard errors for whatever reason: 
    Public Sub CopyTextToClipboard(TextToCopy As String)
        Dim ClipboardData = New DataObject

        Try ' Try to catch exceptions when the clipboard errors for whatever reason: 
            ClipboardData.SetData(DataFormats.Text, TextToCopy)
            Clipboard.SetDataObject(ClipboardData, True)
        Catch ex As Exception
            ' One error could be: Requested Clipboard operation did not succeed.
            MsgBox("Copy to Clipboard Failed: " & ex.Message & " Source: " & ex.Source, vbCritical, Application.ProductName)
        End Try

    End Sub

    ' Deletes all data related to blueprints for the selected character and corporation they are part
    Public Sub ResetAllBPData()
        Dim SQL As String
        Dim UserID As String = CStr(SelectedCharacter.ID)
        Dim CorpID As String = CStr(SelectedCharacter.CharacterCorporation.CorporationID)

        Call BeginSQLiteTransaction()

        SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID IN (" & UserID & ",0)"
        ExecuteNonQuerySQL(SQL)

        SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID IN (" & CorpID & ",0)"
        ExecuteNonQuerySQL(SQL)

        SQL = "UPDATE API SET BLUEPRINTS_CACHED_UNTIL = '1900-01-01 00:00:00' WHERE CHARACTER_ID = " & UserID
        ExecuteNonQuerySQL(SQL)

        SQL = "UPDATE API SET BLUEPRINTS_CACHED_UNTIL = '1900-01-01 00:00:00' WHERE API_TYPE = 'Corporation' "
        SQL = SQL & "AND CORPORATION_ID = " & CorpID
        ExecuteNonQuerySQL(SQL)

        Call CommitSQLiteTransaction()

        MsgBox("Blueprints reset", vbInformation, Application.ProductName)

    End Sub

    ' Sets an existing bp in the DB to the ME/TE or adds it if not in DB as a new owned blueprint - this is always due to user input, not API
    Public Sub UpdateBPinDB(ByVal BPID As Long, ByVal BPName As String, ByVal bpME As Integer, ByVal bpTE As Integer, ByVal SentBPType As BPType, _
                            Optional Favorite As Boolean = False, Optional Ignore As Boolean = False, Optional AdditionalCosts As Double = 0)
        Dim SQL As String
        Dim readerBP As SQLiteDataReader
        Dim TempFavorite As String
        Dim TempIgnore As String
        Dim TempOwned As String

        ' If not sent, it's not a favorite
        If Not Favorite Then
            TempFavorite = "0"
        Else
            TempFavorite = "1"
        End If

        If Not Ignore Then
            TempIgnore = "0"
        Else
            TempIgnore = "1"
        End If

        ' Set the owned flag, only mark this BP as owned if it's not the unowned type
        If SentBPType = BPType.NotOwned Then
            TempOwned = "0" ' User updated, not owned
        Else
            TempOwned = "-1" ' User updated, user owned (not API)
        End If

        ' See if the BP is in the DB
        SQL = "SELECT 'X' FROM OWNED_BLUEPRINTS WHERE BLUEPRINT_ID = " & CStr(BPID) & " AND USER_ID = " & CStr(SelectedCharacter.ID)

        DBCommand = New SQLiteCommand(SQL, DB)
        readerBP = DBCommand.ExecuteReader

        If Not readerBP.HasRows Then
            ' No record, So add it and mark as owned (code 2) - save the scanned data if it was scanned - no item id or location id (from API), so set to 0 on manual saves
            SQL = "INSERT INTO OWNED_BLUEPRINTS (USER_ID, ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, QUANTITY, FLAG_ID, "
            SQL = SQL & "ME, TE, RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS) "
            SQL = SQL & "VALUES (" & SelectedCharacter.ID & ",0,0," & BPID & ",'" & FormatDBString(BPName) & "',1,0,"
            SQL = SQL & CStr(bpME) & "," & CStr(bpTE) & ",1," & CStr(SentBPType) & "," & TempOwned & ",0," & TempFavorite & "," & CStr(AdditionalCosts) & ")"
            Call ExecuteNonQuerySQL(SQL)

        Else
            ' Update it 
            SQL = "UPDATE OWNED_BLUEPRINTS SET ME = " & CStr(bpME) & ", TE = " & CStr(bpTE) & ", OWNED = " & TempOwned & ", FAVORITE = " & TempFavorite
            SQL = SQL & ", ADDITIONAL_COSTS = " & CStr(AdditionalCosts) & ", BP_TYPE = " & CStr(SentBPType) & " "
            SQL = SQL & "WHERE USER_ID =" & CStr(SelectedCharacter.ID) & " AND BLUEPRINT_ID =" & CStr(BPID)
            Call ExecuteNonQuerySQL(SQL)
        End If

        ' Update the bp ignore flag (note for all accounts on this pc)
        SQL = "UPDATE ALL_BLUEPRINTS SET IGNORE = " & TempIgnore & " WHERE BLUEPRINT_ID = " & CStr(BPID)
        Call ExecuteNonQuerySQL(SQL)

        readerBP.Close()
        readerBP = Nothing
        DBCommand = Nothing

    End Sub

    ' Downloads the sent file from server and saves it to the root directory as the sent file name
    Public Function DownloadFileFromServer(ByVal DownloadURL As String, ByVal FileName As String) As String
        'Creating the request and getting the response
        Dim Response As HttpWebResponse
        Dim Request As HttpWebRequest

        ' For reading in chunks of data
        Dim readBytes(4095) As Byte
        ' Save in root directory
        Dim writeStream As New FileStream(FileName, FileMode.Create)
        Dim bytesread As Integer

        Try 'Checks if the file exist
            Request = DirectCast(HttpWebRequest.Create(DownloadURL), HttpWebRequest)
            Request.Credentials = CredentialCache.DefaultCredentials ' Added 9/27 to attempt to fix error: (407) Proxy Authentication Required.
            Response = CType(Request.GetResponse, HttpWebResponse)
        Catch ex As Exception
            ' Show error and exit
            'Close the streams
            writeStream.Close()
            MsgBox("An error occurred while downloading update file.", vbCritical, Application.ProductName)
            Return ""
        End Try

        ' Loop through and get the file in chunks, save out
        Do
            bytesread = Response.GetResponseStream.Read(readBytes, 0, 4096)

            ' No more bytes to read
            If bytesread = 0 Then Exit Do

            writeStream.Write(readBytes, 0, bytesread)
        Loop

        'Close the streams
        Response.GetResponseStream.Close()
        writeStream.Close()

        Return FileName

    End Function

End Module
