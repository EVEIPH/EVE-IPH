
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Net
Imports System.IO

' Place to store all public variables and functions
Public Module Public_Variables
    ' DB name and version
    Public Const SDEVersion As String = "YC 119.2"
    Public Const VersionNumber As String = "3.3.*"

    Public TestingVersion As Boolean ' This flag will test the test downloads from the server for an update
    Public Developer As Boolean ' This is if I'm developing something and only want me to see it instead of public release

    Public LocalCulture As CultureInfo

    Public EVEDB As DBConnection
    Public DBCommand As SQLiteCommand
    ' For checking the DB to see if it's ok to write
    Public DBLock As New Object

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

    'Public Const PatchNotesURL = "http://www.mediafire.com/download/a6dc16n5ndqi2ki/README.txt"
    'Public Const XMLUpdateServerURL = "http://www.mediafire.com/download/zazw6acanj1m43x/LatestVersionIPH.xml"
    'Public Const XMLUpdateTestServerURL = "http://www.mediafire.com/download/zlkpaw8qck4qryw/LatestVersionIPH_Test.xml"
    Public Const PatchNotesURL = "https://raw.githubusercontent.com/EVEIPH/LatestFiles/master/Patch%20Notes.txt"
    Public Const XMLUpdateFileURL = "https://raw.githubusercontent.com/EVEIPH/LatestFiles/master/LatestVersionIPH.xml"
    Public Const XMLUpdateTestFileURL = "https://github.com/EVEIPH/LatestFiles/raw/master/LatestVersionIPH_Test.xml"

    Public Const AppDataPath As String = "EVEIPH\"
    Public Const BPImageFilePath As String = "EVEIPH Images\"
    Public Const UpdatePath As String = "Updates\"

    Public Const SQLiteDBFileName As String = "EVEIPH DB.s3db"

    ' For updates
    Public Const UpdaterFileName As String = "EVEIPH Updater.exe"
    Public Const XMLUpdaterFileName As String = "EVEIPH_Updater.exe" ' For use in the XML files to remove spaces from row names
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
                                            & "ITEM_GROUP_ID, ITEM_GROUP, ITEM_CATEGORY_ID, CASE WHEN ITEM_GROUP LIKE 'Rig%' THEN 'Rig' ELSE ITEM_CATEGORY END AS ITEM_CATEGORY, " _
                                            & "ALL_BLUEPRINTS.ITEM_ID, ITEM_NAME," _
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
                                            & "(SELECT * FROM OWNED_BLUEPRINTS) AS OBP " _
                                            & "ON ALL_BLUEPRINTS.BLUEPRINT_ID = OBP.BLUEPRINT_ID " _
                                            & "AND (OBP.USER_ID = @USERBP_USERID OR OBP.USER_ID = @USERBP_CORPID), " _
                                            & "INVENTORY_TYPES WHERE ALL_BLUEPRINTS.ITEM_ID = INVENTORY_TYPES.typeID) AS X "

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
    Public Const allowedNegativePercentChars As String = "0123456789.%-"

    Public Const SQLiteDateFormat As String = "yyyy-MM-dd HH:mm:ss"

    Public Const IndustryNoCompletedDate As DateTime = #1/1/2001#

    Public Const DataCoreRedeemCost As Double = 10000.0

    Public FirstIndustryJobsViewerLoad As Boolean

    Public Const SpaceFlagCode As Integer = 500

    ' For update prices, to cancel update
    Public CancelUpdatePrices As Boolean
    Public CancelManufacturingTabCalc As Boolean

    ' Column processing
    Public Const NumManufacturingTabColumns As Integer = 90
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

    Public Const AllSystems As String = "All Systems"

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
    Public Const FAXGroupID As Integer = 1538
    Public Const TitanGroupID As Integer = 30
    Public Const BoosterGroupID As Integer = 303

    Public Const StrategicCruiserGroupID As Integer = 963
    Public Const TacticalDestroyerGroupID As Integer = 1305
    Public Const SubsystemCategoryID As Integer = 32

    ' For looking up pos stuff in facilities
    Public Const FuelBlockGroupID As Integer = 1136
    Public Const BattleshipGroupID As Integer = 27
    Public Const ModuleCategoryID As Integer = 7

    Public Const ShipCategoryID As Integer = 6 ' for loading invention and copying 

    ' T3 Bps for facility updates
    Public Const StrategicCruiserBPGroupID As Integer = 996
    Public Const TacticalDestroyerBPGroupID As Integer = 1309
    Public Const SubsystemBPGroupID As Integer = 973

    Public Const ConstructionComponentsGroupID As Integer = 334 ' Use this for all non-capital components
    Public Const ComponentCategoryID As Integer = 17
    Public Const CapitalComponentGroupID As Integer = 873
    Public Const AdvCapitalComponentGroupID As Integer = 913

    ' Categories (has multiple groups)
    Public Const StationEggGroupID As Integer = 307 ' This is for loading No POS build items
    Public Const SovStructureCategoryID As Integer = 3 ' For stations - I don't think this is used anymore (everything can be built at a pos?)
    Public Const StationPartsGroupID As Integer = 536

    Public Const POSFacility As String = "POS"
    Public Const StationFacility As String = "Station"
    Public Const OutpostFacility As String = "Outpost"
    Public Const CitadelFacility As String = "Citadel"

    Public Const BPO As String = "BPO"
    Public Const BPC As String = "BPC"
    Public Const InventedBPC As String = "Invented BPC"
    Public Const UnownedBP As String = "Unowned"

    Public Const Yes As String = "Yes"
    Public Const No As String = "No"
    Public Const Unknown As String = "Unknown"
    Public Const Unlimited As String = "Unlimited"

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
    Public Const Endurance As String = "Endurance"
    Public Const Rorqual As String = "Rorqual"
    Public Const Porpoise As String = "Porpoise"
    Public Const Orca As String = "Orca"
    Public Const Drake As String = "Drake"
    Public Const Rokh As String = "Rokh"

    Public Const CorporationAPITypeName = "Corporation"

    Public Const ExpiredKey = "EXPIRED!"

    ' For exporting Data
    Public Const DefaultTextDataExport As String = "Default"
    Public Const CSVDataExport As String = "CSV"
    Public Const SSVDataExport As String = "SSV"

    ' Team combos
    Public BPTeamComboLoaded As Boolean
    Public CalcManufacturingTeamComboLoaded As Boolean
    Public CalcComponentManufacturingTeamComboLoaded As Boolean
    Public CalcInventionTeamComboLoaded As Boolean
    Public CalcCopyTeamComboLoaded As Boolean
    Public LoadTeambyCombo As Boolean
    Public PreviousTeamActivity As String

    ' Facility combos
    Public PreviousIndustryType As IndustryType
    Public PreviousFacilityType As String
    Public PreviousFacilityRegion As String
    Public PreviousFacilitySystem As String
    Public PreviousFacilityEquipment As String
    Public PreviousActivity As String
    Public CurrentIndustryType As IndustryType
    Public CurrentBPGroupID As Integer
    Public CurrentBPCategoryID As Integer

    Public PreviousCalcBaseIndustryType As IndustryType
    Public PreviousCalcBaseFacilityType As String
    Public PreviousCalcBaseFacilityRegion As String
    Public PreviousCalcBaseFacilitySystem As String
    Public PreviousCalcBaseFacilityEquipment As String

    Public PreviousCalcComponentIndustryType As IndustryType
    Public PreviousCalcComponentFacilityType As String
    Public PreviousCalcComponentFacilityRegion As String
    Public PreviousCalcComponentFacilitySystem As String
    Public PreviousCalcComponentFacilityEquipment As String

    Public PreviousCalcSuperIndustryType As IndustryType
    Public PreviousCalcSuperFacilityType As String
    Public PreviousCalcSuperFacilityRegion As String
    Public PreviousCalcSuperFacilitySystem As String
    Public PreviousCalcSuperFacilityEquipment As String

    Public PreviousCalcCapitalIndustryType As IndustryType
    Public PreviousCalcCapitalFacilityType As String
    Public PreviousCalcCapitalFacilityRegion As String
    Public PreviousCalcCapitalFacilitySystem As String
    Public PreviousCalcCapitalFacilityEquipment As String

    Public PreviousCalcT3IndustryType As IndustryType
    Public PreviousCalcT3FacilityType As String
    Public PreviousCalcT3FacilityRegion As String
    Public PreviousCalcT3FacilitySystem As String
    Public PreviousCalcT3FacilityEquipment As String

    Public PreviousCalcSubsystemIndustryType As IndustryType
    Public PreviousCalcSubsystemFacilityType As String
    Public PreviousCalcSubsystemFacilityRegion As String
    Public PreviousCalcSubsystemFacilitySystem As String
    Public PreviousCalcSubsystemFacilityEquipment As String

    Public PreviousCalcBoosterIndustryType As IndustryType
    Public PreviousCalcBoosterFacilityType As String
    Public PreviousCalcBoosterFacilityRegion As String
    Public PreviousCalcBoosterFacilitySystem As String
    Public PreviousCalcBoosterFacilityEquipment As String

    Public PreviousCalcInventionIndustryType As IndustryType
    Public PreviousCalcInventionFacilityType As String
    Public PreviousCalcInventionFacilityRegion As String
    Public PreviousCalcInventionFacilitySystem As String
    Public PreviousCalcInventionFacilityEquipment As String

    Public PreviousCalcT3InventionIndustryType As IndustryType
    Public PreviousCalcT3InventionFacilityType As String
    Public PreviousCalcT3InventionFacilityRegion As String
    Public PreviousCalcT3InventionFacilitySystem As String
    Public PreviousCalcT3InventionFacilityEquipment As String

    Public PreviousCalcCopyIndustryType As IndustryType
    Public PreviousCalcCopyFacilityType As String
    Public PreviousCalcCopyFacilityRegion As String
    Public PreviousCalcCopyFacilitySystem As String
    Public PreviousCalcCopyFacilityEquipment As String

    Public PreviousCalcREIndustryType As IndustryType
    Public PreviousCalcNoPOSFacilityType As String
    Public PreviousCalcNoPOSFacilityRegion As String
    Public PreviousCalcNoPOSFacilitySystem As String
    Public PreviousCalcNoPOSFacilityEquipment As String

    Public BPFacilityRegionsLoaded As Boolean
    Public BPFacilitySystemsLoaded As Boolean
    Public BPFacilitiesLoaded As Boolean

    Public CalcBaseFacilityRegionsLoaded As Boolean
    Public CalcBaseFacilitySystemsLoaded As Boolean
    Public CalcBaseFacilitiesLoaded As Boolean
    Public CalcComponentFacilityRegionsLoaded As Boolean
    Public CalcComponentFacilitySystemsLoaded As Boolean
    Public CalcComponentFacilitiesLoaded As Boolean
    Public CalcInventionFacilityRegionsLoaded As Boolean
    Public CalcInventionFacilitySystemsLoaded As Boolean
    Public CalcInventionFacilitiesLoaded As Boolean
    Public CalcT3InventionFacilityRegionsLoaded As Boolean
    Public CalcT3InventionFacilitySystemsLoaded As Boolean
    Public CalcT3InventionFacilitiesLoaded As Boolean
    Public CalcCopyFacilityRegionsLoaded As Boolean
    Public CalcCopyFacilitySystemsLoaded As Boolean
    Public CalcCopyFacilitiesLoaded As Boolean
    Public CalcNoPOSFacilityRegionsLoaded As Boolean
    Public CalcNoPOSFacilitySystemsLoaded As Boolean
    Public CalcNoPOSFacilitiesLoaded As Boolean
    Public CalcCapitalFacilityRegionsLoaded As Boolean
    Public CalcCapitalFacilitySystemsLoaded As Boolean
    Public CalcCapitalFacilitiesLoaded As Boolean
    Public CalcSuperFacilityRegionsLoaded As Boolean
    Public CalcSuperFacilitySystemsLoaded As Boolean
    Public CalcSuperFacilitiesLoaded As Boolean
    Public CalcT3FacilityRegionsLoaded As Boolean
    Public CalcT3FacilitySystemsLoaded As Boolean
    Public CalcT3FacilitiesLoaded As Boolean
    Public CalcSubsystemFacilityRegionsLoaded As Boolean
    Public CalcSubsystemFacilitySystemsLoaded As Boolean
    Public CalcSubsystemFacilitiesLoaded As Boolean
    Public CalcBoosterFacilityRegionsLoaded As Boolean
    Public CalcBoosterFacilitySystemsLoaded As Boolean
    Public CalcBoosterFacilitiesLoaded As Boolean

    Public LoadingFacilityActivities As Boolean
    Public LoadingFacilityTypes As Boolean
    Public LoadingFacilityRegions As Boolean
    Public LoadingFacilitySystems As Boolean
    Public LoadingFacilities As Boolean
    Public ChangingUsageChecks As Boolean

    ' For making sure they have a bp facility loaded
    Public FullyLoadedBPFacility As Boolean
    Public SetTaxFeeChecks As Boolean

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
        DefaultView = 0
        ManufacturingTab = 1
        ShoppingList = 2
        RefiningOre = 3
        RefiningItems = 4
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
        Medium = 2
        Large = 3
        Enormous = 4
        Colossal = 5
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

    ' Takes a string value percent and returns a double
    Public Function CpctD(ByVal PercentValue As String) As Double
        Dim PercentNumber As String = PercentValue.Replace("%", "")

        If IsNumeric(PercentNumber) And Not PercentNumber.Contains("E") Then
            Return CDbl(PercentNumber) / 100
        Else
            Return 0
        End If

    End Function

#Region "Taxes/Fees"

    ' Returns the tax on an item price only
    Public Function GetSalesTax(ByVal ItemMarketCost As Double) As Double
        Dim Accounting As Integer = SelectedCharacter.Skills.GetSkillLevel(16622)
        ' Each level of accounting reduces tax by 10% - Starting level with Accounting 0 is 1.5% tax 
        Return (2.0 - (Accounting * 0.1 * 2.0)) / 100 * ItemMarketCost
    End Function

    ' Returns the tax on setting up a sell order for an item price only
    Public Function GetSalesBrokerFee(ByVal ItemMarketCost As Double) As Double
        Dim BrokerRelations As Integer = SelectedCharacter.Skills.GetSkillLevel(3446)

        Dim TempFee As Double
        ' Old BrokerFee % = (1.000 %  0.050 % × BrokerRelationsSkillLevel) / e ^ (0.1000 × FactionStanding + 0.04000 × CorporationStanding)
        ' BrokerFee % = (1.000 %  0.050 % × BrokerRelationsSkillLevel) / 2 ^ (0.1400 × FactionStanding + 0.06000 × CorporationStanding) 
        'TempFee = ((1 - 0.05 * BrokerRelations) / Math.Exp(0.1 * UserApplicationSettings.BrokerFactionStanding + 0.04 * UserApplicationSettings.BrokerCorpStanding)) / 100 * ItemMarketCost
        'TempFee = ((1 - 0.05 * BrokerRelations) / (2 ^ (0.14 * UserApplicationSettings.BrokerFactionStanding + 0.06 * UserApplicationSettings.BrokerCorpStanding))) / 100 * ItemMarketCost

        Dim BrokerTax = 3.0 - (0.1 * BrokerRelations) - (0.03 * UserApplicationSettings.BrokerFactionStanding) - (0.02 * UserApplicationSettings.BrokerCorpStanding)
        TempFee = (BrokerTax / 100) * ItemMarketCost

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
            Dim CMDCount As New SQLiteCommand("SELECT COUNT(*) FROM API WHERE API_TYPE IN ('Account','Character') AND CHARACTER_ID <> 0", EVEDB.DBREf)

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

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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

        Call evedb.ExecuteNonQuerySQL(SQL)

        ' Update the Access mask of all keys to prevent duplicates
        SQL = "UPDATE API SET ACCESS_MASK = " & InsertData.AccessMask & " WHERE KEY_ID = " & InsertData.KeyID
        Call evedb.ExecuteNonQuerySQL(SQL)

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

            Call evedb.ExecuteNonQuerySQL(SQL)

        End If
    End Sub

#End Region

#Region "Teams"

    ' Returns a list of group ID's for the selected BP for use in teams
    Public Function GetTeamGroupIDList(ByRef SentBlueprint As Blueprint, ByRef TeamActivitiesCombo As ComboBox) As List(Of Long)
        Dim TempList As New List(Of Long)
        Dim rsLookup As SQLiteDataReader
        Dim SQL As String

        If Not IsNothing(SentBlueprint) Then
            ' Set the groupID's that we want to limit the team search to
            If SentBlueprint.HasComponents And TeamActivitiesCombo.Text = ActivityComponentManufacturing Then
                ' Load up all the items that build this blueprint
                For i = 0 To SentBlueprint.GetComponentMaterials.GetMaterialList.Count - 1
                    With SentBlueprint.GetComponentMaterials.GetMaterialList(i)
                        SQL = "SELECT INVENTORY_GROUPS.groupID, categoryID FROM INVENTORY_TYPES, INVENTORY_GROUPS "
                        SQL = SQL & "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
                        SQL = SQL & "AND typeID = " & .GetMaterialTypeID
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsLookup = DBCommand.ExecuteReader
                        rsLookup.Read()
                        If rsLookup.GetInt64(1) = ComponentCategoryID Then
                            ' Only add this if it's a component - ingore all T1 and other items
                            TempList.Add(rsLookup.GetInt64(0))
                        End If
                        rsLookup.Close()
                    End With
                Next
            ElseIf TeamActivitiesCombo.Text <> ActivityComponentManufacturing Then
                ' Just add the groupID
                TempList.Add(SentBlueprint.GetItemGroupID)
            Else
                ' Send nothing
                TempList = Nothing
            End If
        Else
            ' Send nothing
            TempList = Nothing
        End If

        Return TempList

    End Function

    ' Looks up and loads all the team data for the selected team in combo and activity
    Public Sub LoadTeam(TeamName As String, Activity As String, ByRef TeamBonusLabel As TextBox, ByRef TeamDefaultLabel As Label, ByRef SaveButton As Button, Tab As String, GroupIDList As List(Of Long))
        If LoadTeambyCombo Then
            Dim SQL As String = ""
            Dim rsLoader As SQLiteDataReader
            Dim TeamBonusFound As Boolean = False
            Dim FoundTeam As New IndustryTeam
            Dim FoundBonus As New IndustryTeamBonus
            Dim FoundTeamBonuses As New List(Of IndustryTeamBonus)

            If TeamName.Contains(NoTeam.TeamName) Then
                GoTo NoBonus
            End If

            Dim TempTeamText As String = Replace(Mid(TeamName, 1, InStr(TeamName, " -") - 1), " (A) ", "")

            SQL = "SELECT BONUS_ID, BONUS_TYPE, BONUS_VALUE, SPECIALTY_GROUP_NAME, INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID "
            SQL = SQL & "FROM INDUSTRY_TEAMS_BONUSES, INDUSTRY_GROUP_SPECIALTIES "
            SQL = SQL & "WHERE TEAM_NAME = '" & TempTeamText & "' "
            SQL = SQL & "AND INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID = INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID "
            SQL = SQL & "GROUP BY BONUS_ID, BONUS_TYPE, BONUS_VALUE, SPECIALTY_GROUP_NAME, INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader

            While rsLoader.Read()
                ' Get all bonuses and save
                FoundBonus = New IndustryTeamBonus
                With FoundBonus
                    .BonusID = rsLoader.GetInt32(0)
                    .BonusType = rsLoader.GetString(1)
                    .BonusValue = rsLoader.GetDouble(2)
                    .BonusSpecialtyGroupName = rsLoader.GetString(3)
                    .BonusSpecialtyGroupID = rsLoader.GetInt32(4)
                End With

                ' Add to list
                FoundTeamBonuses.Add(FoundBonus)

                TeamBonusFound = True
            End While

            rsLoader.Close()
            rsLoader = Nothing
            DBCommand = Nothing

            If TeamBonusFound Then
                ' We have a team we can use a bonus(s) on, Get and save the team information for the selected activity
                FoundTeam.Bonuses = FoundTeamBonuses

                ' Look up team info
                SQL = "SELECT TEAM_ID, TEAM_NAME, TEAM_ACTIVITY_ID, SOLAR_SYSTEM_ID, SOLAR_SYSTEM_NAME, COST_MODIFIER, CREATION_TIME, EXPIRY_TIME, "
                SQL = SQL & "INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID, INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_NAME "
                SQL = SQL & "FROM INDUSTRY_TEAMS, INDUSTRY_CATEGORY_SPECIALTIES "
                SQL = SQL & "WHERE TEAM_NAME = '" & TempTeamText & "' "
                SQL = SQL & "AND INDUSTRY_TEAMS.SPECIALTY_CATEGORY_ID = INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID "
                SQL = SQL & "UNION "
                SQL = SQL & "SELECT TEAM_ID, TEAM_NAME, TEAM_ACTIVITY_ID, SOLAR_SYSTEM_ID, SOLAR_SYSTEM_NAME, COST_MODIFIER, CREATION_TIME, EXPIRY_TIME, "
                SQL = SQL & "INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID, INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_NAME "
                SQL = SQL & "FROM INDUSTRY_TEAMS_AUCTIONS, INDUSTRY_CATEGORY_SPECIALTIES "
                SQL = SQL & "WHERE TEAM_NAME = '" & TempTeamText & "'"
                SQL = SQL & "AND INDUSTRY_TEAMS_AUCTIONS.SPECIALTY_CATEGORY_ID = INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID "

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                With FoundTeam
                    .TeamID = rsLoader.GetInt64(0)
                    .TeamName = rsLoader.GetString(1)
                    .ActivityID = rsLoader.GetInt32(2)
                    .SolarSystemID = rsLoader.GetInt64(3)
                    .SolarSystemName = rsLoader.GetString(4)
                    .CostModifier = rsLoader.GetDouble(5) / 100
                    .CreationTime = CDate(rsLoader.GetString(6))
                    .ExpiryTime = CDate(rsLoader.GetString(7))
                    .SpecializationCategoryID = rsLoader.GetInt32(8)
                    .SpecializationCategory = rsLoader.GetString(9)
                End With

                rsLoader.Close()
                rsLoader = Nothing
                DBCommand = Nothing

            Else
NoBonus:
                ' Set to no team
                FoundTeam = NoTeam

            End If

            ' Save the team locally
            If Tab = BPTab Then
                Select Case Activity
                    Case ActivityManufacturing
                        SelectedBPManufacturingTeam = FoundTeam
                        ' Set the default flag
                        If SelectedBPManufacturingTeam.TeamID = DefaultBPManufacturingTeam.TeamID Then
                            SelectedBPManufacturingTeam.IsDefault = True
                        End If
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SelectedBPComponentManufacturingTeam = FoundTeam
                        ' Set the default flag
                        If SelectedBPComponentManufacturingTeam.TeamID = DefaultBPComponentManufacturingTeam.TeamID Then
                            SelectedBPComponentManufacturingTeam.IsDefault = True
                        End If
                    Case ActivityCopying
                        SelectedBPCopyTeam = FoundTeam
                        ' Set the default flag
                        If SelectedBPCopyTeam.TeamID = DefaultBPCopyTeam.TeamID Then
                            SelectedBPCopyTeam.IsDefault = True
                        End If
                    Case ActivityInvention
                        SelectedBPInventionTeam = FoundTeam
                        ' Set the default flag
                        If SelectedBPInventionTeam.TeamID = DefaultBPInventionTeam.TeamID Then
                            SelectedBPInventionTeam.IsDefault = True
                        End If
                End Select
            Else
                Select Case Activity
                    Case ActivityManufacturing
                        SelectedCalcManufacturingTeam = FoundTeam
                        ' Set the default flag
                        If SelectedCalcManufacturingTeam.TeamID = DefaultCalcManufacturingTeam.TeamID Then
                            SelectedCalcManufacturingTeam.IsDefault = True
                        End If
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SelectedCalcComponentManufacturingTeam = FoundTeam
                        ' Set the default flag
                        If SelectedCalcComponentManufacturingTeam.TeamID = DefaultCalcComponentManufacturingTeam.TeamID Then
                            SelectedCalcComponentManufacturingTeam.IsDefault = True
                        End If
                    Case ActivityCopying
                        SelectedCalcCopyTeam = FoundTeam
                        ' Set the default flag
                        If SelectedCalcCopyTeam.TeamID = DefaultCalcCopyTeam.TeamID Then
                            SelectedCalcCopyTeam.IsDefault = True
                        End If
                    Case ActivityInvention
                        SelectedCalcInventionTeam = FoundTeam
                        ' Set the default flag
                        If SelectedCalcInventionTeam.TeamID = DefaultCalcInventionTeam.TeamID Then
                            SelectedCalcInventionTeam.IsDefault = True
                        End If
                End Select
            End If

            ' Finally update the labels for this team
            Call DisplayTeamBonus(GroupIDList, FoundTeam, TeamBonusLabel, TeamDefaultLabel, SaveButton, Tab)

            ' Reset these so combo works
            frmMain.MouseWheelSelection = False
            frmMain.ComboBoxArrowKeys = False
        End If

    End Sub

    ' Loads the default team for the activity on the sent tab into the sent combo
    Public Sub LoadDefaultTeam(SetDefaultText As Boolean, ByRef TeamActivityCombo As ComboBox, IgnoreDisplay As Boolean,
                               ByRef TeamCombo As ComboBox, ByRef TeamBonusLabel As TextBox,
                               ByRef TeamDefaultLabel As Label, ByRef TeamSaveButton As Button, ByRef Tab As String,
                               Optional ByRef RefBlueprint As Blueprint = Nothing)
        Dim SelectedTeam As New IndustryTeam

        ' Don't trigger a reload of the combos yet
        BPTeamComboLoaded = True

        Select Case TeamActivityCombo.Text
            Case ActivityManufacturing
                If Tab = BPTab Then
                    SelectedTeam = DefaultBPManufacturingTeam
                Else
                    SelectedTeam = DefaultCalcManufacturingTeam
                End If
            Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                If Tab = BPTab Then
                    SelectedTeam = DefaultBPComponentManufacturingTeam
                Else
                    SelectedTeam = DefaultCalcComponentManufacturingTeam
                End If
            Case ActivityInvention
                If Tab = BPTab Then
                    SelectedTeam = DefaultBPInventionTeam
                Else
                    SelectedTeam = DefaultCalcInventionTeam
                End If
            Case ActivityCopying
                If Tab = BPTab Then
                    SelectedTeam = DefaultBPCopyTeam
                Else
                    SelectedTeam = DefaultCalcCopyTeam
                End If
        End Select

        LoadTeambyCombo = False ' Don't run the saving of this team's info since we already have it

        ' First, add 'No Team' to the combo if not already loaded and the selected team is not no-team
        If Not TeamCombo.Items.Contains(NoTeam.TeamName & " - " & FormatPercent(NoTeam.CostModifier / 100, 0)) And SelectedTeam.TeamID <> NoTeam.TeamID Then
            TeamCombo.Items.Add(NoTeam.TeamName & " - " & FormatPercent(NoTeam.CostModifier / 100, 0))
        End If

        ' Add the team if not in the combo and not no team
        If SelectedTeam.TeamID <> NoTeam.TeamID Or Not TeamCombo.Items.Contains(SelectedTeam.TeamName & " - " & FormatPercent(SelectedTeam.CostModifier / 100, 0)) Then
            ' Also, need to add the selected team to the combo since it might not be selected in the query
            TeamCombo.Items.Add(SelectedTeam.TeamName & " - " & FormatPercent(SelectedTeam.CostModifier / 100, 0))
        End If

        LoadTeambyCombo = True

        If SetDefaultText Then
            ' Set the team name to the selected team even before loading
            LoadTeambyCombo = False ' Don't run the saving of this team's info since we already have it
            TeamCombo.Text = SelectedTeam.TeamName & " - " & FormatPercent(SelectedTeam.CostModifier / 100, 0)
            LoadTeambyCombo = True
        End If

        Call DisplayTeamBonus(GetTeamGroupIDList(RefBlueprint, TeamActivityCombo), SelectedTeam, TeamBonusLabel, TeamDefaultLabel, TeamSaveButton, Tab)

        BPTeamComboLoaded = False

    End Sub

    ' Loads the team combo with teams for the activity selected
    Public Sub LoadTeamCombo(SetDefaultTeamText As Boolean, ByRef TeamCombo As ComboBox, TeamActivityCombo As ComboBox, ByRef TeamBonusLabel As TextBox, ByRef TeamDefaultLabel As Label, ByRef TeamSaveButton As Button, Tab As String, Optional BPItemGroupIDList As List(Of Long) = Nothing)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim Activity As Integer
        Dim GroupIDList As String = ""

        TeamCombo.Enabled = True
        TeamCombo.Items.Clear()

        ' Load the default team even if it's not part of the list so they can see it doesn't apply or does
        Call LoadDefaultTeam(SetDefaultTeamText, TeamActivityCombo, True, TeamCombo, TeamBonusLabel, TeamDefaultLabel, TeamSaveButton, Tab)

        Select Case TeamActivityCombo.Text
            Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                Activity = 1
            Case ActivityInvention
                Activity = 8
            Case ActivityCopying
                Activity = 5
        End Select

        If TeamActivityCombo.Text = ActivityComponentManufacturing Or TeamActivityCombo.Text = ActivityCapComponentManufacturing Then
            ' Reset the groupID list to just categories with components
            DBCommand = New SQLiteCommand("SELECT groupID FROM INVENTORY_GROUPS WHERE categoryID = " & ComponentCategoryID, EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            BPItemGroupIDList = New List(Of Long)

            While rsLoader.Read
                BPItemGroupIDList.Add(rsLoader.GetInt64(0))
            End While

            rsLoader.Close()
        End If

        If Not IsNothing(BPItemGroupIDList) Then
            GroupIDList = "("
            For i = 0 To BPItemGroupIDList.Count - 1
                GroupIDList = GroupIDList & CStr(BPItemGroupIDList(i)) & ","
            Next
            GroupIDList = Mid(GroupIDList, 1, Len(GroupIDList) - 1) & ")"
        End If

        ' Only load teams that can work with this BP
        SQL = "SELECT INDUSTRY_TEAMS.TEAM_NAME, COST_MODIFIER "
        SQL = SQL & "FROM INDUSTRY_TEAMS, INDUSTRY_GROUP_SPECIALTIES, INDUSTRY_TEAMS_BONUSES "
        SQL = SQL & "WHERE INDUSTRY_TEAMS.TEAM_ID = INDUSTRY_TEAMS_BONUSES.TEAM_ID "
        SQL = SQL & "AND INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID = INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID "
        If GroupIDList <> "" Then
            SQL = SQL & "AND INDUSTRY_GROUP_SPECIALTIES.GROUP_ID IN " & GroupIDList & " "
        End If
        SQL = SQL & "AND TEAM_ACTIVITY_ID = " & CStr(Activity) & " "
        If Tab = BPTab And frmMain.cmbBPFacilitySystem.Text <> "" Then
            ' Link the query of teams to only that system
            SQL = SQL & "AND INDUSTRY_TEAMS.SOLAR_SYSTEM_NAME = '" & frmMain.cmbBPFacilitySystem.Text & "' "
        End If
        SQL = SQL & "UNION "
        SQL = SQL & "SELECT INDUSTRY_TEAMS_AUCTIONS.TEAM_NAME || ' (A) ', COST_MODIFIER "
        SQL = SQL & "FROM INDUSTRY_TEAMS_AUCTIONS, INDUSTRY_GROUP_SPECIALTIES, INDUSTRY_TEAMS_BONUSES "
        SQL = SQL & "WHERE INDUSTRY_TEAMS_AUCTIONS.TEAM_ID = INDUSTRY_TEAMS_BONUSES.TEAM_ID "
        SQL = SQL & "AND INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID = INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID "
        If GroupIDList <> "" Then
            SQL = SQL & "AND INDUSTRY_GROUP_SPECIALTIES.GROUP_ID IN " & GroupIDList & " "
        End If
        SQL = SQL & "AND TEAM_ACTIVITY_ID = " & CStr(Activity) & " "
        If Tab = BPTab And frmMain.cmbBPFacilitySystem.Text <> "" Then
            ' Link the query of teams to only that system
            SQL = SQL & "AND INDUSTRY_TEAMS_AUCTIONS.SOLAR_SYSTEM_NAME = '" & frmMain.cmbBPFacilitySystem.Text & "' "
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read()
            Application.DoEvents()
            ' Add the cost modifier for now and don't add if already there from default
            If Not TeamCombo.Items.Contains(rsLoader.GetString(0) & " - " & FormatPercent(rsLoader.GetDouble(1) / 100, 0)) Then
                TeamCombo.Items.Add(rsLoader.GetString(0) & " - " & FormatPercent(rsLoader.GetDouble(1) / 100, 0))
            End If
        End While

    End Sub

    ' Displays the bonus for the selected team as it applies to the item
    Public Sub DisplayTeamBonus(SentGroupIDList As List(Of Long), DisplayTeam As IndustryTeam, ByRef TeamBonusLabel As TextBox, ByRef TeamDefaultLabel As Label, ByRef TeamSaveButton As Button, Tab As String)
        Dim BonusLabel As String = ""
        Dim ToolTipLabel As String = ""
        Dim SQL As String = ""
        Dim rsSearch As SQLiteDataReader
        Dim rsLookUp As SQLiteDataReader

        If DisplayTeam.TeamName <> "" And ((Not IsNothing(SelectedBlueprint) And Tab = BPTab) Or Tab = CalcTab) Then

            ' Look up the bonuses
            SQL = "SELECT BONUS_TYPE, BONUS_VALUE, INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID, SPECIALTY_GROUP_NAME "
            SQL = SQL & "FROM INDUSTRY_TEAMS_BONUSES, INDUSTRY_GROUP_SPECIALTIES "
            SQL = SQL & "WHERE TEAM_NAME = '" & DisplayTeam.TeamName & "' "
            SQL = SQL & "AND INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID = INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID "
            SQL = SQL & "GROUP BY BONUS_TYPE, BONUS_VALUE, INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID, SPECIALTY_GROUP_NAME "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsLookUp = DBCommand.ExecuteReader

            ' Loop through the bonuses and display the bonuses that apply to the label, and all for tool tip
            While rsLookUp.Read()

                ' Look up the groupIDs in the table with this team specialty id bonus if bp tab, else look them all up
                If Tab = BPTab Then
                    SQL = "SELECT SPECIALTY_GROUP_NAME, GROUP_ID FROM INDUSTRY_GROUP_SPECIALTIES "
                    SQL = SQL & "WHERE SPECIALTY_GROUP_ID = " & rsLookUp.GetInt32(2)

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsSearch = DBCommand.ExecuteReader

                    While rsSearch.Read
                        If Not IsNothing(SentGroupIDList) Then
                            If SentGroupIDList.Contains(rsSearch.GetInt32(1)) Then
                                ' Save the bonus to the bonus label
                                BonusLabel = BonusLabel & rsSearch.GetString(0) & " " & FormatPercent(rsLookUp.GetDouble(1) / 100, 1) & " " & rsLookUp.GetString(0) & vbCrLf
                            End If
                        End If
                    End While

                    rsSearch.Close()
                    rsSearch = Nothing
                    DBCommand = Nothing
                Else
                    ' Save all the bonuses to the bonus label for manufacturing tab 
                    BonusLabel = BonusLabel & rsLookUp.GetString(3) & " - " & FormatPercent(rsLookUp.GetDouble(1) / 100, 1) & " " & rsLookUp.GetString(0) & vbCrLf
                End If

                ' Always add all bonuses to the tool tip
                If Tab <> CalcTab Then
                    ToolTipLabel = ToolTipLabel & rsLookUp.GetString(3) & " - " & FormatPercent(rsLookUp.GetDouble(1) / 100, 1) & " " & rsLookUp.GetString(0) & vbCrLf
                End If

            End While

            ' Set the labels
            If BonusLabel <> "" Then
                TeamBonusLabel.Text = BonusLabel.Substring(0, Len(BonusLabel) - 2)
            Else
                TeamBonusLabel.Text = "No Team Bonus"
            End If

            ' Set the tool tip regardless
            If UserApplicationSettings.ShowToolTips And ToolTipLabel <> "" Then
                frmMain.ttBP.SetToolTip(TeamBonusLabel, ToolTipLabel.Substring(0, Len(ToolTipLabel) - 2))
            End If

        Else
            TeamBonusLabel.Text = ""
        End If

        If DisplayTeam.IsDefault Then
            TeamDefaultLabel.Visible = True
            TeamSaveButton.Enabled = False
        Else
            TeamDefaultLabel.Visible = False
            TeamSaveButton.Enabled = True
        End If

    End Sub

#End Region

#Region "Facilities"

    ' Returns the type of production done for the activity and bp data sent
    Public Function GetProductionType(Activity As String, ItemGroupID As Long, ItemCategoryID As Long, FacilityType As String) As IndustryType
        Dim SelectedIndyType As IndustryType

        ' Determine if it's a fuel block, module, or big ship that can use a multi-use array
        If FacilityType = POSFacility And (ItemGroupID = 1136 _
                                           Or ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
                                           Or ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 _
                                           Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649
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
                        Case CarrierGroupID, DreadnoughtGroupID, CapitalIndustrialShipGroupID, FAXGroupID
                            SelectedIndyType = IndustryType.CapitalManufacturing
                        Case StrategicCruiserGroupID
                            SelectedIndyType = IndustryType.T3CruiserManufacturing
                        Case TacticalDestroyerGroupID
                            SelectedIndyType = IndustryType.T3DestroyerManufacturing
                        Case Else
                            SelectedIndyType = IndustryType.Manufacturing

                            If ItemCategoryID = SubsystemCategoryID Then
                                SelectedIndyType = IndustryType.SubsystemManufacturing
                            ElseIf ItemCategoryID = ComponentCategoryID Then
                                ' Add category for component
                                If ItemGroupID = CapitalComponentGroupID Or ItemGroupID = AdvCapitalComponentGroupID Then
                                    SelectedIndyType = IndustryType.CapitalComponentManufacturing ' These all use cap components
                                Else
                                    SelectedIndyType = IndustryType.ComponentManufacturing
                                End If
                            ElseIf NoPOSCategoryIDs.Contains(ItemCategoryID) Or ItemGroupID = StationEggGroupID Then
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
                    FacilityReference = SelectedBPCapitalComponentManufacturingFacility
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
                    FacilityReference = SelectedCalcCapitalComponentManufacturingFacility
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

    ' Resets all combo boxes that might need to be updated 
    Public Sub ResetComboLoadVariables(Tab As String, ProductionType As IndustryType, RegionsValue As Boolean, SystemsValue As Boolean,
                                       FacilitiesValue As Boolean, ManualIndexUpdate As Boolean, ByRef ManualSystemIndexGroupBox As GroupBox)

        If Tab = BPTab Then
            BPFacilityRegionsLoaded = RegionsValue
            BPFacilitySystemsLoaded = SystemsValue
            BPFacilitiesLoaded = FacilitiesValue
            ManualSystemIndexGroupBox.Enabled = ManualIndexUpdate
        Else
            Select Case ProductionType
                Case IndustryType.Manufacturing
                    CalcBaseFacilitiesLoaded = FacilitiesValue
                    CalcBaseFacilitySystemsLoaded = SystemsValue
                    CalcBaseFacilityRegionsLoaded = RegionsValue
                Case IndustryType.SuperManufacturing
                    CalcSuperFacilitiesLoaded = FacilitiesValue
                    CalcSuperFacilitySystemsLoaded = SystemsValue
                    CalcSuperFacilityRegionsLoaded = RegionsValue
                Case IndustryType.CapitalManufacturing
                    CalcCapitalFacilitiesLoaded = FacilitiesValue
                    CalcCapitalFacilitySystemsLoaded = SystemsValue
                    CalcCapitalFacilityRegionsLoaded = RegionsValue
                Case IndustryType.BoosterManufacturing
                    CalcBoosterFacilitiesLoaded = FacilitiesValue
                    CalcBoosterFacilitySystemsLoaded = SystemsValue
                    CalcBoosterFacilityRegionsLoaded = RegionsValue
                Case IndustryType.T3CruiserManufacturing, IndustryType.T3DestroyerManufacturing
                    CalcT3FacilitiesLoaded = FacilitiesValue
                    CalcT3FacilitySystemsLoaded = SystemsValue
                    CalcT3FacilityRegionsLoaded = RegionsValue
                Case IndustryType.SubsystemManufacturing
                    CalcSubsystemFacilitiesLoaded = FacilitiesValue
                    CalcSubsystemFacilitySystemsLoaded = SystemsValue
                    CalcSubsystemFacilityRegionsLoaded = RegionsValue
                Case IndustryType.Invention
                    CalcInventionFacilitiesLoaded = FacilitiesValue
                    CalcInventionFacilitySystemsLoaded = SystemsValue
                    CalcInventionFacilityRegionsLoaded = RegionsValue
                Case IndustryType.T3Invention
                    CalcT3InventionFacilitiesLoaded = FacilitiesValue
                    CalcT3InventionFacilitySystemsLoaded = SystemsValue
                    CalcT3InventionFacilityRegionsLoaded = RegionsValue
                Case IndustryType.Copying
                    CalcCopyFacilitiesLoaded = FacilitiesValue
                    CalcCopyFacilitySystemsLoaded = SystemsValue
                    CalcCopyFacilityRegionsLoaded = RegionsValue
                Case IndustryType.NoPOSManufacturing
                    CalcNoPOSFacilitiesLoaded = FacilitiesValue
                    CalcNoPOSFacilitySystemsLoaded = SystemsValue
                    CalcNoPOSFacilityRegionsLoaded = RegionsValue
                Case IndustryType.ComponentManufacturing, IndustryType.CapitalComponentManufacturing
                    CalcComponentFacilitiesLoaded = FacilitiesValue
                    CalcComponentFacilitySystemsLoaded = SystemsValue
                    CalcComponentFacilityRegionsLoaded = RegionsValue
            End Select
        End If
    End Sub

    ' Loads the default facility for activity sent unless specified
    Public Sub LoadFacility(ProductionType As IndustryType, IsDefault As Boolean, NewBP As Boolean,
                             FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox, ByRef FacilityRegionCombo As ComboBox,
                             ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
                             ByRef FacilityBonusLabel As Label, ByRef FacilityDefaultLabel As Label,
                             ByRef FacilityManualMELabel As Label, ByRef FacilityManualMETextBox As TextBox,
                             ByRef FacilityManualTELabel As Label, ByRef FacilityManualTETextBox As TextBox,
                             ByRef FacilityManualTaxLabel As Label, ByRef FacilityManualTaxTextBox As TextBox,
                             ByRef FacilitySaveButton As Button, ByRef FacilityTaxRateLabel As Label, Tab As String,
                             ByRef FacilityUsageCheck As CheckBox, ByRef FacilityIncludeLabel As Label,
                             ByRef FacilityActivityCostCheck As CheckBox, ByRef FacilityActivityTimeCheck As CheckBox,
                             ByRef FacilityLoaded As Boolean,
                             Optional ByRef FacilityActivityCombo As ComboBox = Nothing, Optional BPTech As Integer = 1,
                             Optional ItemGroupID As Integer = 0, Optional ItemCategoryID As Integer = 0,
                             Optional LoadActivites As Boolean = True, Optional RefreshBP As Boolean = True,
                             Optional ByRef FacilityUsageLabel As Label = Nothing,
                             Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing, Optional ByRef ToolTipRef As ToolTip = Nothing,
                             Optional ByRef FWUpgradeLabel As Label = Nothing, Optional ByRef FWUpgradeCombo As ComboBox = Nothing)

        Dim SelectedFacility As New IndustryFacility
        Dim SelectedActivity As String = ActivityManufacturing
        Dim FacilityName As String = ""

        If Tab = BPTab Then
            If IsDefault Then
                Select Case ProductionType
                    Case IndustryType.Manufacturing
                        SelectedFacility = CType(DefaultBPManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SuperManufacturing
                        SelectedFacility = CType(DefaultBPSuperManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.CapitalManufacturing
                        SelectedFacility = CType(DefaultBPCapitalManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.BoosterManufacturing
                        SelectedFacility = CType(DefaultBPBoosterManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3CruiserManufacturing
                        SelectedFacility = CType(DefaultBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3DestroyerManufacturing
                        SelectedFacility = CType(DefaultBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SubsystemManufacturing
                        SelectedFacility = CType(DefaultBPSubsystemManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.Invention
                        SelectedFacility = CType(DefaultBPInventionFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityInvention
                    Case IndustryType.T3Invention
                        SelectedFacility = CType(DefaultBPT3InventionFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityInvention
                    Case IndustryType.Copying
                        SelectedActivity = ActivityCopying
                        SelectedFacility = CType(DefaultBPCopyFacility.Clone, IndustryFacility)
                    Case IndustryType.NoPOSManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultBPNoPOSFacility.Clone, IndustryFacility)
                    Case IndustryType.ComponentManufacturing
                        SelectedActivity = ActivityComponentManufacturing
                        SelectedFacility = CType(DefaultBPComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.CapitalComponentManufacturing
                        SelectedActivity = ActivityCapComponentManufacturing
                        SelectedFacility = CType(DefaultBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.POSFuelBlockManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultBPPOSFuelBlockFacility.Clone, IndustryFacility)
                    Case IndustryType.POSLargeShipManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultBPPOSLargeShipFacility.Clone, IndustryFacility)
                    Case IndustryType.POSModuleManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultBPPOSModuleFacility.Clone, IndustryFacility)
                End Select

            Else
                Select Case ProductionType
                    Case IndustryType.Manufacturing
                        SelectedFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SuperManufacturing
                        SelectedFacility = CType(SelectedBPSuperManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.CapitalManufacturing
                        SelectedFacility = CType(SelectedBPCapitalManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.BoosterManufacturing
                        SelectedFacility = CType(SelectedBPBoosterManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3CruiserManufacturing
                        SelectedFacility = CType(SelectedBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3DestroyerManufacturing
                        SelectedFacility = CType(SelectedBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SubsystemManufacturing
                        SelectedFacility = CType(SelectedBPSubsystemManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.Invention
                        SelectedActivity = ActivityInvention
                        SelectedFacility = CType(SelectedBPInventionFacility.Clone, IndustryFacility)
                    Case IndustryType.T3Invention
                        SelectedActivity = ActivityInvention
                        SelectedFacility = CType(SelectedBPT3InventionFacility.Clone, IndustryFacility)
                    Case IndustryType.Copying
                        SelectedActivity = ActivityCopying
                        SelectedFacility = CType(SelectedBPCopyFacility.Clone, IndustryFacility)
                    Case IndustryType.NoPOSManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedBPNoPOSFacility.Clone, IndustryFacility)
                    Case IndustryType.ComponentManufacturing
                        SelectedActivity = ActivityComponentManufacturing
                        SelectedFacility = CType(SelectedBPComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.CapitalComponentManufacturing
                        SelectedActivity = ActivityCapComponentManufacturing
                        SelectedFacility = CType(SelectedBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.POSFuelBlockManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedBPPOSFuelBlockFacility.Clone, IndustryFacility)
                    Case IndustryType.POSLargeShipManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedBPPOSLargeShipFacility.Clone, IndustryFacility)
                    Case IndustryType.POSModuleManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedBPPOSModuleFacility.Clone, IndustryFacility)
                End Select
            End If
        Else
            If IsDefault Then
                Select Case ProductionType
                    Case IndustryType.Manufacturing
                        SelectedFacility = CType(DefaultCalcBaseManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SuperManufacturing
                        SelectedFacility = CType(DefaultCalcSuperManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.CapitalManufacturing
                        SelectedFacility = CType(DefaultCalcCapitalManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.BoosterManufacturing
                        SelectedFacility = CType(DefaultCalcBoosterManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3CruiserManufacturing
                        SelectedFacility = CType(DefaultCalcT3CruiserManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3DestroyerManufacturing
                        SelectedFacility = CType(DefaultCalcT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SubsystemManufacturing
                        SelectedFacility = CType(DefaultCalcSubsystemManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.Invention
                        SelectedActivity = ActivityInvention
                        SelectedFacility = CType(DefaultCalcInventionFacility.Clone, IndustryFacility)
                    Case IndustryType.T3Invention
                        SelectedActivity = ActivityInvention
                        SelectedFacility = CType(DefaultCalcT3InventionFacility.Clone, IndustryFacility)
                    Case IndustryType.Copying
                        SelectedActivity = ActivityCopying
                        SelectedFacility = CType(DefaultCalcCopyFacility.Clone, IndustryFacility)
                    Case IndustryType.NoPOSManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultCalcNoPOSFacility.Clone, IndustryFacility)
                    Case IndustryType.ComponentManufacturing
                        SelectedActivity = ActivityComponentManufacturing
                        SelectedFacility = CType(DefaultCalcComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.CapitalComponentManufacturing
                        SelectedActivity = ActivityCapComponentManufacturing
                        SelectedFacility = CType(DefaultCalcCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.POSFuelBlockManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultCalcPOSFuelBlockFacility.Clone, IndustryFacility)
                    Case IndustryType.POSLargeShipManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultCalcPOSLargeShipFacility.Clone, IndustryFacility)
                    Case IndustryType.POSModuleManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(DefaultCalcPOSModuleFacility.Clone, IndustryFacility)
                End Select

            Else
                Select Case ProductionType
                    Case IndustryType.Manufacturing
                        SelectedFacility = CType(SelectedCalcBaseManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SuperManufacturing
                        SelectedFacility = CType(SelectedCalcSuperManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.CapitalManufacturing
                        SelectedFacility = CType(SelectedCalcCapitalManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.BoosterManufacturing
                        SelectedFacility = CType(SelectedCalcBoosterManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3CruiserManufacturing
                        SelectedFacility = CType(SelectedCalcT3CruiserManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.T3DestroyerManufacturing
                        SelectedFacility = CType(SelectedCalcT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.SubsystemManufacturing
                        SelectedFacility = CType(SelectedCalcSubsystemManufacturingFacility.Clone, IndustryFacility)
                        SelectedActivity = ActivityManufacturing
                    Case IndustryType.Invention
                        SelectedActivity = ActivityInvention
                        SelectedFacility = CType(SelectedCalcInventionFacility.Clone, IndustryFacility)
                    Case IndustryType.T3Invention
                        SelectedActivity = ActivityInvention
                        SelectedFacility = CType(SelectedCalcT3InventionFacility.Clone, IndustryFacility)
                    Case IndustryType.Copying
                        SelectedActivity = ActivityCopying
                        SelectedFacility = CType(SelectedCalcCopyFacility.Clone, IndustryFacility)
                    Case IndustryType.NoPOSManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedCalcNoPOSFacility.Clone, IndustryFacility)
                    Case IndustryType.ComponentManufacturing
                        SelectedActivity = ActivityComponentManufacturing
                        SelectedFacility = CType(SelectedCalcComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.CapitalComponentManufacturing
                        SelectedActivity = ActivityCapComponentManufacturing
                        SelectedFacility = CType(SelectedCalcCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                    Case IndustryType.POSFuelBlockManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedCalcPOSFuelBlockFacility.Clone, IndustryFacility)
                    Case IndustryType.POSLargeShipManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedCalcPOSLargeShipFacility.Clone, IndustryFacility)
                    Case IndustryType.POSModuleManufacturing
                        SelectedActivity = ActivityManufacturing
                        SelectedFacility = CType(SelectedCalcPOSModuleFacility.Clone, IndustryFacility)
                End Select
            End If
        End If

        If LoadActivites And Not IsNothing(FacilityActivityCombo) Then
            Call LoadFacilityActivities(BPTech, NewBP, FacilityActivityCombo, ItemGroupID, ItemCategoryID)
        End If

        ' Activity combo is loaded so set the activity Text
        LoadingFacilityActivities = True
        If Not IsNothing(FacilityActivityCombo) Then
            FacilityActivityCombo.Text = SelectedActivity
        End If
        PreviousIndustryType = ProductionType
        PreviousActivity = SelectedActivity
        LoadingFacilityActivities = False

        ' Facility Type combo
        ' Load the combo if they want to change
        Call LoadFacilityTypeCombo(ProductionType, FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo,
                                   FacilityCombo, FacilityBonusLabel, FacilityDefaultLabel, FacilityManualMELabel, FacilityManualMETextBox,
                                   FacilityManualTELabel, FacilityManualMETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox,
                                   FacilitySaveButton, FacilityTaxRateLabel, Tab, FacilityUsageLabel, FacilityUsageCheck, ManualSystemIndexGroupBox)

        ' Enable the type of facility and set
        LoadingFacilityTypes = True
        FacilityTypeCombo.Enabled = True
        FacilityTypeCombo.Text = SelectedFacility.FacilityType
        LoadingFacilityTypes = False

        If SelectedFacility.FacilityType = None Then
            ' Just hide the boxes and exit
            Call HideFacilityBonusBoxes(FacilityBonusLabel, FacilityTaxRateLabel, FacilityManualMELabel, FacilityManualTELabel,
                                        FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox, FacilityUsageLabel)
            Call SetNoFacility(FacilityRegionCombo, FacilitySystemCombo, FacilityCombo, FacilityUsageCheck,
                               FacilityActivityCostCheck, FacilityActivityTimeCheck, FacilityIncludeLabel)
            FacilityLoaded = True ' Even with none, it's loaded
            Exit Sub
        End If

        ' Region name Combo
        LoadingFacilityRegions = True
        FacilityRegionCombo.Enabled = True
        FacilityRegionCombo.Text = SelectedFacility.RegionName
        LoadingFacilityRegions = False

        ' Systems combo
        LoadingFacilitySystems = True
        FacilitySystemCombo.Enabled = True
        FacilitySystemCombo.Text = SelectedFacility.SolarSystemName
        LoadingFacilitySystems = False

        ' Facility/Array combo
        LoadingFacilities = True
        FacilityCombo.Enabled = True
        Dim AutoLoad As Boolean = False
        'If it's a pos, need to auto-load the facility for that item selected
        If FacilityTypeCombo.Text = POSFacility And Tab = BPTab Then
            Call LoadFacilities(ItemGroupID, ItemCategoryID, False,
                                FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
                                FacilityBonusLabel, FacilityDefaultLabel, FacilityManualMELabel, FacilityManualMETextBox,
                                FacilityManualTELabel, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox,
                                FacilitySaveButton, FacilityTaxRateLabel, Tab,
                                FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, AutoLoad,
                                SelectedFacility.IncludeActivityUsage, SelectedFacility.FacilityName, FacilityUsageLabel, ManualSystemIndexGroupBox, ToolTipRef, FWUpgradeLabel, FWUpgradeCombo)
        ElseIf Tab = CalcTab Then
            ' Load all facilities for each calc tab facility
            Call LoadFacilities(ItemGroupID, ItemCategoryID, False,
                    FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
                    FacilityBonusLabel, FacilityDefaultLabel, FacilityManualMELabel, FacilityManualMETextBox,
                    FacilityManualTELabel, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox,
                    FacilitySaveButton, FacilityTaxRateLabel, Tab,
                    FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, AutoLoad,
                    SelectedFacility.IncludeActivityUsage, SelectedFacility.FacilityName, FacilityUsageLabel, ManualSystemIndexGroupBox, ToolTipRef)
        End If
        LoadingFacilities = False

        ' Usage checks
        ChangingUsageChecks = True
        FacilityUsageCheck.Checked = SelectedFacility.IncludeActivityUsage

        If Not IsNothing(FacilityActivityCostCheck) Then
            FacilityActivityCostCheck.Checked = SelectedFacility.IncludeActivityCost
        End If

        If Not IsNothing(FacilityActivityTimeCheck) Then
            FacilityActivityTimeCheck.Checked = SelectedFacility.IncludeActivityTime
        End If
        ChangingUsageChecks = False

        ' Set her in case we load the bonuses
        Call SetFWUpgradeControls(Tab, FWUpgradeLabel, FWUpgradeCombo, SelectedFacility.SolarSystemName)

        ' Finally show the results and save the facility locally
        If Not AutoLoad Then
            LoadingFacilities = True
            FacilityCombo.Text = SelectedFacility.FacilityName
            Call DisplayFacilityBonus(SelectedFacility.ProductionType, SelectedFacility.MaterialMultiplier, SelectedFacility.TimeMultiplier, SelectedFacility.TaxRate,
                                      ItemGroupID, ItemCategoryID,
                                      FacilityActivity, FacilityTypeCombo.Text, FacilityCombo.Text,
                                      FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
                                      FacilityBonusLabel, FacilityDefaultLabel,
                                      FacilityManualMELabel, FacilityManualMETextBox,
                                      FacilityManualTELabel, FacilityManualTETextBox,
                                      FacilityManualTaxLabel, FacilityManualTaxTextBox,
                                      FacilitySaveButton, FacilityTaxRateLabel,
                                      FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, Tab, FacilityLoaded,
                                      SelectedFacility.IncludeActivityUsage, ToolTipRef, GetFWUpgradeLevel(FWUpgradeCombo, FacilitySystemCombo.Text))
            LoadingFacilities = False
        End If

        ' If this is the BP tab, then refresh the BP prices
        If Not FirstLoad And SetTaxFeeChecks And Tab = BPTab And RefreshBP Then
            If Not IsNothing(SelectedBlueprint) Then
                Call SelectedBlueprint.SetPriceData(frmMain.chkBPTaxes.Checked, frmMain.chkBPBrokerFees.Checked)
                Call frmMain.UpdateBPPriceLabels()
            End If
        End If

        Call ResetComboLoadVariables(Tab, ProductionType, False, False, False, True, ManualSystemIndexGroupBox)

        ' All facilities loaded
        FacilityLoaded = True

        If Tab = CalcTab And Not FirstLoad Then
            Call frmMain.ResetRefresh()
        End If

    End Sub

    ' Loads the bp facility activity combo
    Public Sub LoadFacilityActivities(BPTech As Integer, NewBP As Boolean, ByRef FacilityActivitiesCombo As ComboBox, BPGroupID As Long, BPCategoryID As Long)

        LoadingFacilityActivities = True
        FacilityActivitiesCombo.BeginUpdate()

        Select Case BPTech
            Case BlueprintTechLevel.T1
                ' Just manufacturing (add components later if there are any)
                FacilityActivitiesCombo.Items.Clear()
                FacilityActivitiesCombo.Items.Add(ActivityManufacturing)

            Case BlueprintTechLevel.T2
                ' Add only T2 activities to equipment
                FacilityActivitiesCombo.Items.Clear()
                FacilityActivitiesCombo.Items.Add(ActivityManufacturing)
                FacilityActivitiesCombo.Items.Add(ActivityCopying)
                FacilityActivitiesCombo.Items.Add(ActivityInvention)

            Case BlueprintTechLevel.T3
                ' Add only T3 activities to eqipment
                FacilityActivitiesCombo.Items.Clear()
                FacilityActivitiesCombo.Items.Add(ActivityManufacturing)
                FacilityActivitiesCombo.Items.Add(ActivityInvention)

        End Select

        ' Add components as a manufacturing facility option if this bp has any
        If Not IsNothing(SelectedBlueprint) And Not NewBP Then
            If SelectedBlueprint.HasComponents Then
                Select Case BPGroupID
                    Case TitanGroupID, DreadnoughtGroupID, CarrierGroupID, SupercarrierGroupID, CapitalIndustrialShipGroupID,
                         IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID, FAXGroupID
                        FacilityActivitiesCombo.Items.Add(ActivityCapComponentManufacturing)
                        If BPGroupID = JumpFreighterGroupID Then
                            ' Need to add both cap and components
                            FacilityActivitiesCombo.Items.Add(ActivityComponentManufacturing)
                        End If
                    Case Else
                        FacilityActivitiesCombo.Items.Add(ActivityComponentManufacturing)
                End Select
            End If
        End If

        ' Only the BP tab will call this
        BPFacilitiesLoaded = False
        BPFacilityRegionsLoaded = False
        BPFacilitySystemsLoaded = False

        LoadingFacilityActivities = False
        FacilityActivitiesCombo.EndUpdate()

    End Sub

    ' Loads the facility types in the sent combo
    Public Sub LoadFacilityTypeCombo(ProductionType As IndustryType,
                             ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
                             ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
                             ByRef FacilityBonusLabel As Label, ByRef FacilityDefaultLabel As Label,
                             ByRef FacilityManualMELabel As Label, ByRef FacilityManualMETextBox As TextBox,
                             ByRef FacilityManualTELabel As Label, ByRef FacilityManualTETextBox As TextBox,
                             ByRef FacilityManualTaxLabel As Label, ByRef FacilityManualTaxTextBox As TextBox,
                             ByRef FacilitySaveButton As Button, ByRef FacilityTaxRateLabel As Label, Tab As String,
                             ByRef FacilityUsageLabel As Label, ByRef FacilityUsageCheck As CheckBox, ByRef ManualSystemIndexGroupBox As GroupBox)

        LoadingFacilityTypes = True
        LoadingFacilityRegions = True
        LoadingFacilitySystems = True
        LoadingFacilities = True

        ' Clear the types each time for a fresh set of options
        FacilityTypeCombo.Items.Clear()

        ' Load the facility type options
        Select Case FacilityActivity
            ' Load up None for Invention/RE, Copy - they could buy the BP or T2 BPO
            Case ActivityCopying, ActivityInvention
                Select Case ProductionType
                    Case IndustryType.T3Invention
                        ' Can be invented in outposts and POS
                        FacilityTypeCombo.Items.Add(OutpostFacility)
                        FacilityTypeCombo.Items.Add(POSFacility)
                        'FacilityTypeCombo.Items.Add(CitadelFacility)
                        FacilityTypeCombo.Items.Add(None)
                    Case Else
                        FacilityTypeCombo.Items.Add(StationFacility)
                        FacilityTypeCombo.Items.Add(OutpostFacility)
                        'FacilityTypeCombo.Items.Add(CitadelFacility)
                        FacilityTypeCombo.Items.Add(POSFacility)
                        FacilityTypeCombo.Items.Add(None)
                End Select
            Case ActivityManufacturing
                Select Case ProductionType
                    Case IndustryType.SuperManufacturing
                        ' Check types, supers can only be built in a pos
                        FacilityTypeCombo.Items.Add(POSFacility)
                        'FacilityTypeCombo.Items.Add(CitadelFacility)
                    Case IndustryType.BoosterManufacturing, IndustryType.SubsystemManufacturing, IndustryType.T3CruiserManufacturing, IndustryType.T3DestroyerManufacturing
                        ' Can be built in outposts and POS
                        FacilityTypeCombo.Items.Add(OutpostFacility)
                        FacilityTypeCombo.Items.Add(POSFacility)
                        'FacilityTypeCombo.Items.Add(CitadelFacility)
                    Case IndustryType.NoPOSManufacturing
                        ' No POS for stuff like infrastructure hubs
                        FacilityTypeCombo.Items.Add(StationFacility)
                        FacilityTypeCombo.Items.Add(OutpostFacility)
                        'FacilityTypeCombo.Items.Add(CitadelFacility)
                    Case Else
                        ' Add all
                        FacilityTypeCombo.Items.Add(StationFacility)
                        FacilityTypeCombo.Items.Add(OutpostFacility)
                        FacilityTypeCombo.Items.Add(POSFacility)
                        'FacilityTypeCombo.Items.Add(CitadelFacility)
                End Select
            Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                ' Can do these anywhere
                FacilityTypeCombo.Items.Add(StationFacility)
                FacilityTypeCombo.Items.Add(OutpostFacility)
                FacilityTypeCombo.Items.Add(POSFacility)
                'FacilityTypeCombo.Items.Add(CitadelFacility)
        End Select

        ' Only reset if they changed it
        If ProductionType <> PreviousIndustryType Or FacilityActivity <> PreviousActivity Then
            ' Reset all other dropdowns
            FacilityTypeCombo.Text = "Select Type"
            FacilityRegionCombo.Items.Clear()
            FacilityRegionCombo.Text = "Select Region"
            FacilityRegionCombo.Enabled = False
            FacilitySystemCombo.Items.Clear()
            FacilitySystemCombo.Text = "Select System"
            FacilitySystemCombo.Enabled = False
            FacilityCombo.Items.Clear()
            FacilityCombo.Text = "Select Facility / Array"
            FacilityCombo.Enabled = False
            FacilityUsageCheck.Enabled = False
            PreviousIndustryType = ProductionType
            PreviousActivity = FacilityActivity
            Call HideFacilityBonusBoxes(FacilityBonusLabel, FacilityTaxRateLabel, FacilityManualMELabel, FacilityManualTELabel,
                                        FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox, FacilityUsageLabel)


        End If

        ' Double check the text selected and reset 
        If Not FacilityTypeCombo.Items.Contains(FacilityTypeCombo.Text) Then
            FacilityTypeCombo.Text = POSFacility ' can build almost everything (if not all) in a pos
        End If

        ' Enable the facility type combo
        FacilityTypeCombo.Enabled = True

        ' Make sure default is not shown yet
        'FacilityDefaultLabel.Visible = False
        FacilitySaveButton.Enabled = False

        LoadingFacilityTypes = False
        LoadingFacilityRegions = False
        LoadingFacilitySystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(Tab, ProductionType, False, False, False, False, ManualSystemIndexGroupBox)

    End Sub

    ' Based on the selections, load the region combo
    Public Sub LoadFacilityRegions(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean,
                                    ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
                                    ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
                                    ByRef FacilityBonusLabel As Label, ByRef FacilityDefaultLabel As Label,
                                    ByRef FacilityManualMELabel As Label, ByRef FacilityManualMETextBox As TextBox,
                                    ByRef FacilityManualTELabel As Label, ByRef FacilityManualTETextBox As TextBox,
                                    ByRef FacilityManualTaxLabel As Label, ByRef FacilityManualTaxTextBox As TextBox,
                                    ByRef FacilitySaveButton As Button, ByRef FacilityTaxRateLabel As Label, Tab As String,
                                    ByRef FacilityUsageCheck As CheckBox,
                                    Optional ByRef FacilityUsageLabel As Label = Nothing,
                                    Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        LoadingFacilityRegions = True
        LoadingFacilitySystems = True
        LoadingFacilities = True

        FacilityRegionCombo.Items.Clear()

        ' Load regions from the facilities table - only load regions for our activity type and item group/category
        Select Case FacilityTypeCombo.Text

            Case OutpostFacility, StationFacility

                SQL = "SELECT DISTINCT REGION_NAME FROM STATION_FACILITIES WHERE OUTPOST "

                ' Set flag for outpost just to delineate
                If FacilityTypeCombo.Text = StationFacility Then
                    SQL = SQL & " = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                End If

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add only regions with stations that can make what we sent
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

            Case POSFacility
                ' For a POS, load all regions as options, but adding only one wormhole region option and don't show Jove regions
                SQL = "SELECT DISTINCT CASE WHEN (REGIONS.regionID >=11000000 and REGIONS.regionid <=11000030) THEN 'Wormhole Space' ELSE regionName END AS REGION_NAME "
                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "
                SQL = SQL & "AND (factionID <> 500005 OR factionID IS NULL) "

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
                    ' For caps, only show low sec
                    SQL = SQL & " AND security < .45 "
                End If

        End Select

        SQL = SQL & "GROUP BY REGION_NAME "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            FacilityRegionCombo.Items.Add(rsLoader.GetString(0))
        End While

        ' Enable the region combo
        FacilityRegionCombo.Enabled = True

        ' Only turn off everything if it's set to select region
        If NewFacility Then
            FacilitySystemCombo.Items.Clear()
            FacilitySystemCombo.Text = "Select System"
            FacilitySystemCombo.Enabled = False
            FacilityCombo.Items.Clear()
            FacilityCombo.Text = "Select Facility / Array"
            FacilityCombo.Enabled = False
            ' Make sure default is not checked yet
            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
            FacilitySaveButton.Enabled = False
            FacilityUsageCheck.Enabled = False
            Call HideFacilityBonusBoxes(FacilityBonusLabel, FacilityTaxRateLabel, FacilityManualMELabel, FacilityManualTELabel,
                                        FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox, FacilityUsageLabel)
        End If

        ' Only reset the region if the current selected region is not in list, also if it is in list, enable solarsystem
        If Not FacilityRegionCombo.Items.Contains(FacilityRegionCombo.Text) Then
            FacilityRegionCombo.Text = "Select Region"
        Else
            FacilitySystemCombo.Enabled = True
        End If

        LoadingFacilityRegions = False
        LoadingFacilitySystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(Tab, GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text), True, False, False, False, ManualSystemIndexGroupBox)

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub

    ' Based on the selections, load the systems combo
    Public Sub LoadFacilitySystems(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean,
                               ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
                               ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
                               ByRef FacilityBonusLabel As Label, ByRef FacilityTaxRateLabel As Label,
                               ByRef FacilityManualMELabel As Label, ByRef FacilityManualMETextBox As TextBox,
                               ByRef FacilityManualTELabel As Label, ByRef FacilityManualTETextBox As TextBox,
                               ByRef FacilityManualTaxLabel As Label, ByRef FacilityManualTaxTextBox As TextBox,
                               ByRef FacilityDefaultLabel As Label, ByRef FacilitySaveButton As Button, Tab As String,
                               ByRef FacilityUsageCheck As CheckBox,
                               Optional ByRef FacilityUsageLabel As Label = Nothing,
                               Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing)

        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        LoadingFacilitySystems = True
        LoadingFacilities = True

        FacilitySystemCombo.Items.Clear()

        Select Case FacilityTypeCombo.Text

            Case OutpostFacility, StationFacility

                SQL = "SELECT DISTINCT SOLAR_SYSTEM_NAME, COST_INDEX FROM STATION_FACILITIES WHERE OUTPOST "

                ' Set flag for outpost just to delineate
                If FacilityTypeCombo.Text = StationFacility Then
                    SQL = SQL & " = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                End If

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(FacilityRegionCombo.Text) & "'"

            Case POSFacility
                ' For a POS, load all systems, if wormhole 'region' selected, then load jspace systems
                SQL = "SELECT DISTINCT solarSystemName AS SOLAR_SYSTEM_NAME, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS COST_INDEX "
                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
                SQL = SQL & "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON solarSystemID = SOLAR_SYSTEM_ID "

                Select Case FacilityActivity
                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                End Select

                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "

                If FacilityRegionCombo.Text = "Wormhole Space" Then
                    SQL = SQL & "AND SOLAR_SYSTEMS.regionID >=11000000 and SOLAR_SYSTEMS.regionid <=11000030 "
                Else
                    ' For a POS, load all systems that have records linked
                    SQL = SQL & "AND regionName = '" & FormatDBString(FacilityRegionCombo.Text) & "'"
                End If

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
                    ' For caps, only show low sec
                    SQL = SQL & " AND security < .45 "
                End If

        End Select

        SQL = SQL & " GROUP BY SOLAR_SYSTEM_NAME, COST_INDEX"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            FacilitySystemCombo.Items.Add(rsLoader.GetString(0) & " (" & FormatNumber(rsLoader.GetDouble(1), 3) & ")")
        End While

        ' Enable the system combo
        FacilitySystemCombo.Enabled = True

        ' Only turn off everything if it's set to select a system
        If NewFacility Then
            FacilityCombo.Items.Clear()
            If FacilityTypeCombo.Text = POSFacility Then
                FacilityCombo.Text = "Select Array"
            Else
                FacilityCombo.Text = "Select Facility"
            End If
            FacilityCombo.Enabled = False
            ' Make sure default is not checked yet
            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
            FacilitySaveButton.Enabled = False
            FacilityUsageCheck.Enabled = False
            Call HideFacilityBonusBoxes(FacilityBonusLabel, FacilityTaxRateLabel, FacilityManualMELabel, FacilityManualTELabel,
                                        FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox, FacilityUsageLabel)
        End If

        ' Only reset the system if the current selected system is not in list, also if it is in list, enable facilty
        If Not FacilitySystemCombo.Items.Contains(FacilitySystemCombo.Text) Then
            FacilitySystemCombo.Text = "Select System"
        Else
            FacilityCombo.Enabled = True
        End If

        LoadingFacilitySystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(Tab, GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text), False, True, False, False, ManualSystemIndexGroupBox)

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub

    ' Based on the selections, load the facilities/arrays combo - an itemcategory or itemgroup id of -1 means to ignore it when filling arrays
    Public Sub LoadFacilities(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean,
                               ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
                               ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
                               ByRef FacilityBonusLabel As Label, ByRef FacilityDefaultLabel As Label,
                               ByRef FacilityManualMELabel As Label, ByRef FacilityManualMETextBox As TextBox,
                               ByRef FacilityManualTELabel As Label, ByRef FacilityManualTETextBox As TextBox,
                               ByRef FacilityManualTaxLabel As Label, ByRef FacilityManualTaxTextBox As TextBox,
                               ByRef FacilitySaveButton As Button, ByRef FacilityTaxRateLabel As Label,
                               ByVal Tab As String, ByRef FacilityUsageCheck As CheckBox,
                               ByRef FacilityIncludeActivityCostsCheck As CheckBox, ByRef FacilityIncludeActivityTimeCheck As CheckBox,
                               ByRef AutoLoadFacility As Boolean, ByVal FacilityUsageCheckValue As Boolean,
                               Optional OverrideFacilityName As String = "", Optional ByRef FacilityUsageLabel As Label = Nothing,
                               Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing, Optional ByRef ToolTipRef As ToolTip = Nothing,
                               Optional ByRef FWUpgradeLabel As Label = Nothing, Optional ByRef FWUpgradeCombo As ComboBox = Nothing, Optional ByVal SolarSystemName As String = "")
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        LoadingFacilities = True

        Select Case FacilityTypeCombo.Text

            Case StationFacility, OutpostFacility
                ' Load the Stations in system for the activity we are doing
                SQL = "SELECT DISTINCT FACILITY_NAME FROM STATION_FACILITIES WHERE OUTPOST "

                ' Set flag for outpost just to delineate
                If FacilityTypeCombo.Text = StationFacility Then
                    SQL = SQL & " = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                End If

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Check groups and categories
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(FacilityRegionCombo.Text) & "' "
                Dim SystemName As String = FacilitySystemCombo.Text.Substring(0, InStr(FacilitySystemCombo.Text, "(") - 2)
                SQL = SQL & "AND SOLAR_SYSTEM_NAME = '" & FormatDBString(SystemName) & "' "

            Case POSFacility

                ' Load all the array types up into the combo for a POS
                SQL = "SELECT DISTINCT ARRAY_NAME AS FACILITY_NAME FROM ASSEMBLY_ARRAYS "
                SQL = SQL & "WHERE ACTIVITY_ID = "

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Check groups and categories
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for component
                        Select Case ItemGroupID
                            Case TitanGroupID, SupercarrierGroupID, DreadnoughtGroupID, CarrierGroupID,
                                CapitalIndustrialShipGroupID, IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID,
                                AdvCapitalComponentGroupID, CapitalComponentGroupID, FAXGroupID
                                SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
                            Case Else
                                SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
                        End Select
                    Case ActivityCopying
                        SQL = SQL & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                    Case ActivityInvention
                        ' POS invention you can only do T3 in certain arrays
                        SQL = SQL & CStr(IndustryActivities.Invention) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

        End Select

        ' This is helpful if we auto-load (Capital array before super capital, equipment array before rapid equipment) to choose the one more likely
        SQL = SQL & " ORDER BY FACILITY_NAME"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        FacilityCombo.Enabled = True
        FacilityCombo.Items.Clear()

        Dim AutoLoadName As String = ""
        Dim i As Integer = 0

        While rsLoader.Read
            If rsLoader.GetString(0).Contains("Thukker") And FacilityTypeCombo.Text = POSFacility Then
                ' Need to make sure it's a low sec system selected
                Dim rsCheck As SQLiteDataReader
                SQL = "SELECT SECURITY FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(FacilitySystemCombo.Text.Substring(0, InStr(FacilitySystemCombo.Text, "(") - 2)) & "'"
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    If rsCheck.GetDouble(0) < 0.45 Then
                        ' Thukker is only low sec - no easy way to weed this out
                        FacilityCombo.Items.Add(rsLoader.GetString(0))
                    End If
                Else
                    ' Allow it
                    FacilityCombo.Items.Add(rsLoader.GetString(0))
                End If
            Else
                FacilityCombo.Items.Add(rsLoader.GetString(0))
            End If

            i += 1 ' get the count
            ' Load the first one - auto choose subsystem array over advanced medium array unless already selected
            If AutoLoadName = "" Or (rsLoader.GetString(0) = "Subsystem Assembly Array" And OverrideFacilityName = "") Then
                AutoLoadName = rsLoader.GetString(0)
            End If
        End While

        ' Always load the facility if there is only one and we have a reference to auto load or we are loading a specific facility
        If (i = 1 And Not IsNothing(AutoLoadFacility)) Or FacilityCombo.Items.Contains(OverrideFacilityName) _
            Or FacilityCombo.Items.Contains(FacilityCombo.Text) Or OverrideFacilityName = "CalcBase" Then
            ' Check the override, if they want to use a rapid assembly it will override here, otherwise the other facility types should handle it (e.g. super, cap, etc)
            If OverrideFacilityName <> "" And FacilityCombo.Items.Contains(OverrideFacilityName) Then
                FacilityCombo.Text = OverrideFacilityName
            Else
                FacilityCombo.Text = AutoLoadName
            End If

            AutoLoadFacility = True
            ' Display bonuses - Need to load everything since the array won't change to cause it to reload
            Dim Defaults As New ProgramSettings

            ' Set the FW controls before loading bonuses
            Call SetFWUpgradeControls(Tab, FWUpgradeLabel, FWUpgradeCombo, FacilitySystemCombo.Text)

            ' For a pos, need to display the results and reload the bp
            Call DisplayFacilityBonus(GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text),
                          Defaults.FacilityDefaultMM, Defaults.FacilityDefaultTM, Defaults.FacilityDefaultTax, ItemGroupID, ItemCategoryID,
                          FacilityActivity, FacilityTypeCombo.Text, FacilityCombo.Text,
                          FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
                          FacilityBonusLabel, FacilityDefaultLabel,
                          FacilityManualMELabel, FacilityManualMETextBox,
                          FacilityManualTELabel, FacilityManualTETextBox,
                          FacilityManualTaxLabel, FacilityManualTaxTextBox,
                          FacilitySaveButton, FacilityTaxRateLabel,
                          FacilityUsageCheck, FacilityIncludeActivityCostsCheck, FacilityIncludeActivityTimeCheck,
                          Tab, FullyLoadedBPFacility, FacilityUsageCheckValue, ToolTipRef, GetFWUpgradeLevel(FWUpgradeCombo, FacilitySystemCombo.Text))

        Else
            If Not FacilityCombo.Items.Contains(FacilityCombo.Text) Then
                ' Only load if the item isn't in the combo
                Select Case FacilityTypeCombo.Text
                    Case OutpostFacility, StationFacility
                        FacilityCombo.Text = "Select Facility"
                    Case POSFacility
                        FacilityCombo.Text = "Select Array"
                End Select

                ' Make sure default is turned off since we still have to load the array
                FacilitySaveButton.Enabled = False
                FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
                FacilityUsageCheck.Enabled = False ' Don't enable the usage either
            Else
                ' Since this is a different system but facility is loaded, enable save
                FacilitySaveButton.Enabled = True
                FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
                FacilityUsageCheck.Enabled = True
            End If

            AutoLoadFacility = False

        End If

        If NewFacility Then
            ' Make sure default is not checked yet
            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
            FacilitySaveButton.Enabled = False
            Call HideFacilityBonusBoxes(FacilityBonusLabel, FacilityTaxRateLabel, FacilityManualMELabel, FacilityManualTELabel,
                                        FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxLabel, FacilityManualTaxTextBox, FacilityUsageLabel)
        End If

        ' Users might select the facility drop down first, so reload all others
        Call ResetComboLoadVariables(Tab, GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text), False, False, True, True, ManualSystemIndexGroupBox)

        LoadingFacilities = False

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub

    ' Displays the bonus for the facility selected in the facility or array combo
    Public Sub DisplayFacilityBonus(ProductionType As IndustryType, SentMM As Double, SentTM As Double, SentTax As Double,
                                     ItemGroupID As Integer, ItemCategoryID As Integer,
                                     Activity As String, FacilityType As String, FacilityName As String,
                                     ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
                                     ByRef FacilityBonusLabel As Label, ByRef FacilityDefaultLabel As Label,
                                     ByRef FacilityManualMELabel As Label, ByRef FacilityManualMEText As TextBox,
                                     ByRef FacilityManualTELabel As Label, ByRef FacilityManualTEText As TextBox,
                                     ByRef FacilityManualTaxLabel As Label, ByRef FacilityManualTaxText As TextBox,
                                     ByRef FacilitySaveButton As Button, ByRef FacilityTaxRateLabel As Label,
                                     ByRef FacilityUsageCheck As CheckBox,
                                     ByRef ActivityCostCheck As CheckBox, ByRef ActivityTimeCheck As CheckBox,
                                     ByRef Tab As String, ByRef FacilityLoaded As Boolean, ByRef FacilityUsageCheckValue As Boolean,
                                     ByRef ToolTipRef As ToolTip, ByVal FWUpgradeLevel As Integer)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        Dim FacilityID As Long
        Dim FacilityTypeID As Long
        Dim MaterialMultiplier As Double
        Dim TimeMultiplier As Double
        Dim CostMultiplier As Double
        Dim Tax As Double

        Dim Defaults As New ProgramSettings
        Dim TempDefaultFacility As New IndustryFacility

        Dim SelectedFacility As New IndustryFacility
        Dim CompareCostCheck As Boolean = False
        Dim CompareTimeCheck As Boolean = False

        If FacilityType <> None Then
            Select Case FacilityType

                Case OutpostFacility, StationFacility

                    ' Load the Stations in system for the activity we are doing
                    SQL = "SELECT FACILITY_ID, FACILITY_TYPE_ID, MATERIAL_MULTIPLIER, "
                    SQL = SQL & "TIME_MULTIPLIER, COST_MULTIPLIER, "
                    SQL = SQL & "FACILITY_TAX FROM STATION_FACILITIES WHERE OUTPOST  "

                    ' Set flag for outpost just to delineate
                    If FacilityType = StationFacility Then
                        SQL = SQL & " = " & CStr(StationType.Station) & " "
                    Else
                        SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                    End If
                    SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString(FacilityName) & "' "

                Case POSFacility

                    SQL = "SELECT 0 AS FACILITY_ID, ARRAY_TYPE_ID, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, 1 AS COST_MULTIPLIER, " & CStr(POSTaxRate) & " as TAX "
                    SQL = SQL & "FROM ASSEMBLY_ARRAYS "
                    SQL = SQL & "WHERE ARRAY_NAME = '" & FormatDBString(FacilityName) & "' "

            End Select

            Select Case Activity
                Case ActivityManufacturing
                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                    ' Add category for component
                    Select Case ItemGroupID
                        Case TitanGroupID, SupercarrierGroupID, DreadnoughtGroupID, CarrierGroupID,
                            CapitalIndustrialShipGroupID, IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID,
                                AdvCapitalComponentGroupID, CapitalComponentGroupID, FAXGroupID
                            SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
                        Case Else
                            SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
                    End Select
                Case ActivityCopying
                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                Case ActivityInvention
                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
            End Select

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader

            If rsLoader.Read Then
                ' If we have values that are not the defaults, then they sent in something else saved (outposts) so set them here
                If FacilityType = OutpostFacility Then
                    If SentMM <> Defaults.FacilityDefaultMM Then
                        MaterialMultiplier = SentMM
                    Else
                        MaterialMultiplier = rsLoader.GetDouble(2)
                    End If

                    If SentTM <> Defaults.FacilityDefaultTM Then
                        TimeMultiplier = SentTM
                    Else
                        TimeMultiplier = rsLoader.GetDouble(3)
                    End If

                    If SentTax <> Defaults.FacilityDefaultTax Then
                        Tax = SentTax
                    Else
                        Tax = rsLoader.GetDouble(5)
                    End If

                Else ' For POS and Stations, this is already set
                    MaterialMultiplier = rsLoader.GetDouble(2)
                    TimeMultiplier = rsLoader.GetDouble(3)
                    Tax = rsLoader.GetDouble(5)
                End If

                CostMultiplier = rsLoader.GetDouble(4)

                FacilityID = rsLoader.GetInt64(0)
                FacilityTypeID = rsLoader.GetInt64(1)

                rsLoader.Close()
            Else
                ' Set the facility to none if not found
                FacilityType = None
            End If

        End If

        If FacilityType = None Then
            ' None selected or not found
            FacilityName = None
            FacilityID = 0
            FacilityTypeID = 0
            MaterialMultiplier = Defaults.FacilityDefaultMM
            TimeMultiplier = Defaults.FacilityDefaultTM
            CostMultiplier = 1
            Tax = Defaults.FacilityDefaultTax
        End If

        Dim MMText As String = FormatPercent(1 - MaterialMultiplier, 1)
        Dim TMText As String = FormatPercent(1 - TimeMultiplier, 1)
        Dim TaxText As String = FormatPercent(Tax, 1)

        ' Show boxes for the user to enter for outposts since I can't get the upgrades or taxes from CREST
        If FacilityType = OutpostFacility Then

            FacilityManualMELabel.Visible = True
            FacilityManualTELabel.Visible = True
            FacilityManualMEText.Visible = True
            FacilityManualTEText.Visible = True
            FacilityManualMEText.Text = MMText
            FacilityManualTEText.Text = TMText

            FacilityBonusLabel.Visible = False
            FacilityManualTaxLabel.Visible = True
            FacilityManualTaxText.Visible = True
            FacilityManualTaxText.Text = TaxText
            FacilityTaxRateLabel.Text = ""
            FacilityTaxRateLabel.Visible = False
        Else

            FacilityBonusLabel.Visible = True
            FacilityManualMELabel.Visible = False
            FacilityManualTELabel.Visible = False
            FacilityManualTaxLabel.Visible = False
            FacilityManualMEText.Visible = False
            FacilityManualTEText.Visible = False
            FacilityManualTaxText.Visible = False

            FacilityBonusLabel.Text = "ME: " & MMText & " TE: " & TMText
            FacilityTaxRateLabel.Text = "Tax: " & TaxText
            FacilityTaxRateLabel.Visible = True
        End If

        ' Now that we have everything, load the full facility into the appropriate selected facility to use later
        With SelectedFacility
            .FacilityName = FacilityName
            Select Case Activity
                Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                    .ActivityID = IndustryActivities.Manufacturing
                Case ActivityCopying
                    .ActivityID = IndustryActivities.Copying
                Case ActivityInvention
                    .ActivityID = IndustryActivities.Invention
            End Select

            .ActivityCostPerSecond = 0
            .FacilityType = FacilityType
            .MaterialMultiplier = MaterialMultiplier
            .TimeMultiplier = TimeMultiplier
            .RegionName = FacilityRegionCombo.Text
            .SolarSystemName = FacilitySystemCombo.Text
            .ProductionType = ProductionType
            ChangingUsageChecks = True
            .IncludeActivityUsage = FacilityUsageCheckValue ' Use this value when loading from Load Facility (using the selected facility) or from the form dropdown (use the checkbox)
            ChangingUsageChecks = False
            .TaxRate = Tax
            .FWUpgradeLevel = FWUpgradeLevel

            If Not IsNothing(ActivityCostCheck) Then
                .IncludeActivityCost = ActivityCostCheck.Checked
            Else
                .IncludeActivityTime = False
            End If

            If Not IsNothing(ActivityTimeCheck) Then
                .IncludeActivityTime = ActivityTimeCheck.Checked
            Else
                .IncludeActivityTime = False
            End If

            If FacilityType <> None And .SolarSystemID = 0 Then
                ' Quick look up for the solarsystemid and region id, Strip off the system index first
                Dim SystemName As String = .SolarSystemName.Substring(0, InStr(.SolarSystemName, "(") - 2)
                SQL = "SELECT solarSystemID, regionID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SystemName) & "'"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                .SolarSystemID = rsLoader.GetInt64(0)
                .RegionID = rsLoader.GetInt64(1)
                rsLoader.Close()

                ' Now look up the cost index 
                If FacilityType <> POSFacility Then
                    SQL = "SELECT COST_INDEX FROM STATION_FACILITIES WHERE FACILITY_NAME = '" & FormatDBString(FacilityName) & "'"
                    SQL = SQL & "AND ACTIVITY_ID = " & .ActivityID & " "
                Else
                    SQL = "SELECT COST_INDEX FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES "
                    SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
                    SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
                    SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = " & .ActivityID & " "
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader

                If rsLoader.Read() Then
                    .CostIndex = rsLoader.GetDouble(0)
                Else
                    .CostIndex = 0
                End If

                rsLoader.Close()
            Else
                .SolarSystemID = 0
                .RegionID = 0
                .CostIndex = 0
            End If
        End With

        Call SetFacilityandDefault(SelectedFacility, ProductionType, Tab, FacilityType, FacilityCombo,
                                   FacilityDefaultLabel, FacilitySaveButton, CompareCostCheck, CompareTimeCheck, ToolTipRef)

        ' Make sure the usage check is now enabled
        If FacilityType <> None Then
            FacilityUsageCheck.Enabled = True
        End If

        FacilityLoaded = True

        If Tab = CalcTab And Not FirstLoad Then
            Call frmMain.ResetRefresh()
        End If

        Application.DoEvents()

    End Sub

    Public Function GetFWUpgradeLevel(SolarSystemName As String) As Integer
        Dim rsFW As SQLiteDataReader

        ' Format system name
        If SolarSystemName.Contains("(") Then
            SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
        End If

        Dim SQL As String = "SELECT UPGRADE_LEVEL FROM SOLAR_SYSTEMS, FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = solarSystemID AND factionWarzone <> 0 AND solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsFW = DBCommand.ExecuteReader

        If rsFW.Read Then
            Return rsFW.GetInt32(0)
        Else
            Return 0
        End If

    End Function

    Public Function GetFWUpgradeLevel(FWUpgradeCombo As ComboBox, SolarSystemName As String) As Integer
        Dim FWUpgradeLevel As Integer

        Dim rsFW As SQLiteDataReader
        Dim SSID As Long

        ' Format system name
        If SolarSystemName.Contains("(") Then
            SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
        End If

        Dim SQL As String = "SELECT factionWarzone, solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsFW = DBCommand.ExecuteReader

        Dim Warzone As Boolean
        If rsFW.Read Then
            Warzone = CBool(rsFW.GetInt32(0))
            SSID = rsFW.GetInt64(1)
        Else
            Warzone = False
        End If

        If Warzone Then
            If Not IsNothing(FWUpgradeCombo) Then
                Select Case FWUpgradeCombo.Text
                    Case "Level 1"
                        FWUpgradeLevel = 1
                    Case "Level 2"
                        FWUpgradeLevel = 2
                    Case "Level 3"
                        FWUpgradeLevel = 3
                    Case "Level 4"
                        FWUpgradeLevel = 4
                    Case "Level 5"
                        FWUpgradeLevel = 5
                    Case Else
                        FWUpgradeLevel = 0
                End Select
            Else
                FWUpgradeLevel = 0
            End If
        Else
            FWUpgradeLevel = 0
        End If

        Return FWUpgradeLevel

    End Function

    ' enables the controls for fw settings on the bp tab
    Public Sub SetFWUpgradeControls(Tab As String, ByRef FWUpgradeLabel As Label, ByRef FWUpgradeCombo As ComboBox, ByVal SolarSystemName As String)
        ' Load the faction warfare upgrade if set for the BP tab
        If Tab = BPTab Then
            Dim rsFW As SQLiteDataReader
            Dim SSID As Long

            ' Format system name
            If SolarSystemName.Contains("(") Then
                SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
            End If

            Dim SQL As String = "SELECT factionWarzone, solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsFW = DBCommand.ExecuteReader

            Dim Warzone As Boolean
            If rsFW.Read Then
                Warzone = CBool(rsFW.GetInt32(0))
                SSID = rsFW.GetInt64(1)
            Else
                Warzone = False
            End If

            If Warzone Then
                FWUpgradeLabel.Enabled = True
                FWUpgradeCombo.Enabled = True
                ' look up level
                Dim rsFWLevel As SQLiteDataReader
                SQL = "SELECT UPGRADE_LEVEL FROM FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = " & CStr(SSID)
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsFWLevel = DBCommand.ExecuteReader
                rsFWLevel.Read()

                If rsFWLevel.HasRows Then
                    If rsFWLevel.GetInt32(0) = 0 Then
                        FWUpgradeCombo.Text = None
                    Else
                        FWUpgradeCombo.Text = "Level " & CStr(rsFWLevel.GetInt32(0))
                    End If
                Else
                    FWUpgradeCombo.Text = None
                End If
            Else
                FWUpgradeLabel.Enabled = False
                FWUpgradeCombo.Enabled = False
                FWUpgradeCombo.Text = None
            End If
            rsFW.Close()
        End If
    End Sub

    ' Sets the sent facility to the one we are selecting and sets the default 
    Public Sub SetFacilityandDefault(ByVal SelectedFacility As IndustryFacility, ProductionType As IndustryType, Tab As String,
                                      ByRef FacilityType As String, ByRef FacilityCombo As ComboBox,
                                      ByRef FacilityDefaultLabel As Label, ByRef FacilitySaveButton As Button,
                                      ByVal CompareIncludeCostCheck As Boolean,
                                      ByVal CompareIncludeTimeCheck As Boolean, ByRef ToolTipRef As ToolTip)
        ' For checking change from stations to pos on bp tab
        Dim PreviousFacility As New IndustryFacility

        ' Based on the type of activity, set the selected facility for that type
        If Tab = BPTab Then
            Select Case ProductionType
                Case IndustryType.Manufacturing
                    PreviousFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    ' Set the other three types for pos too
                    If SelectedFacility.FacilityType = POSFacility Then
                        SelectedFacility.FacilityName = SelectedBPPOSFuelBlockFacility.FacilityName
                        SelectedFacility.FacilityType = SelectedBPPOSFuelBlockFacility.FacilityType
                        SelectedBPPOSFuelBlockFacility = CType(SelectedFacility.Clone, IndustryFacility)

                        SelectedFacility.FacilityName = SelectedBPPOSLargeShipFacility.FacilityName
                        SelectedFacility.FacilityType = SelectedBPPOSLargeShipFacility.FacilityType
                        SelectedBPPOSLargeShipFacility = CType(SelectedFacility.Clone, IndustryFacility)

                        SelectedFacility.FacilityName = SelectedBPPOSModuleFacility.FacilityName
                        SelectedFacility.FacilityType = SelectedBPPOSModuleFacility.FacilityType
                        SelectedBPPOSModuleFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    End If
                    If SelectedBPManufacturingFacility.IsEqual(DefaultBPManufacturingFacility) Then
                        SelectedBPManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.POSFuelBlockManufacturing
                    PreviousFacility = CType(SelectedBPPOSFuelBlockFacility.Clone, IndustryFacility)
                    SelectedBPPOSFuelBlockFacility = SelectedFacility
                    SelectedBPManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
                    If SelectedBPPOSFuelBlockFacility.IsEqual(DefaultBPPOSFuelBlockFacility) Then
                        SelectedBPPOSFuelBlockFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPPOSFuelBlockFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.POSLargeShipManufacturing
                    PreviousFacility = CType(SelectedBPPOSLargeShipFacility.Clone, IndustryFacility)
                    SelectedBPPOSLargeShipFacility = SelectedFacility
                    SelectedBPManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
                    If SelectedBPPOSLargeShipFacility.IsEqual(DefaultBPPOSLargeShipFacility) Then
                        SelectedBPPOSLargeShipFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPPOSLargeShipFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.POSModuleManufacturing
                    PreviousFacility = CType(SelectedBPPOSModuleFacility.Clone, IndustryFacility)
                    SelectedBPPOSModuleFacility = SelectedFacility
                    SelectedBPManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
                    If SelectedBPPOSModuleFacility.IsEqual(DefaultBPPOSModuleFacility) Then
                        SelectedBPPOSModuleFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPPOSModuleFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.BoosterManufacturing
                    PreviousFacility = CType(SelectedBPBoosterManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPBoosterManufacturingFacility = SelectedFacility
                    If SelectedBPBoosterManufacturingFacility.IsEqual(DefaultBPBoosterManufacturingFacility) Then
                        SelectedBPBoosterManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPBoosterManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.CapitalManufacturing
                    PreviousFacility = CType(SelectedBPCapitalManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPCapitalManufacturingFacility = SelectedFacility
                    If SelectedBPCapitalManufacturingFacility.IsEqual(DefaultBPCapitalManufacturingFacility) Then
                        SelectedBPCapitalManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPCapitalManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.SuperManufacturing
                    PreviousFacility = CType(SelectedBPSuperManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPSuperManufacturingFacility = SelectedFacility
                    If SelectedBPSuperManufacturingFacility.IsEqual(DefaultBPSuperManufacturingFacility) Then
                        SelectedBPSuperManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPSuperManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.T3CruiserManufacturing
                    PreviousFacility = CType(SelectedBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPT3CruiserManufacturingFacility = SelectedFacility
                    If SelectedBPT3CruiserManufacturingFacility.IsEqual(DefaultBPT3CruiserManufacturingFacility) Then
                        SelectedBPT3CruiserManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPT3CruiserManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.T3DestroyerManufacturing
                    PreviousFacility = CType(SelectedBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPT3DestroyerManufacturingFacility = SelectedFacility
                    If SelectedBPT3DestroyerManufacturingFacility.IsEqual(DefaultBPT3DestroyerManufacturingFacility) Then
                        SelectedBPT3DestroyerManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPT3DestroyerManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.SubsystemManufacturing
                    PreviousFacility = CType(SelectedBPSubsystemManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPSubsystemManufacturingFacility = SelectedFacility
                    If SelectedBPSubsystemManufacturingFacility.IsEqual(DefaultBPSubsystemManufacturingFacility) Then
                        SelectedBPSubsystemManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPSubsystemManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.ComponentManufacturing
                    PreviousFacility = CType(SelectedBPComponentManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPComponentManufacturingFacility = SelectedFacility
                    If SelectedBPComponentManufacturingFacility.IsEqual(DefaultBPComponentManufacturingFacility) Then
                        SelectedBPComponentManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPComponentManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.CapitalComponentManufacturing
                    PreviousFacility = CType(SelectedBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPCapitalComponentManufacturingFacility = SelectedFacility
                    If SelectedBPCapitalComponentManufacturingFacility.IsEqual(DefaultBPCapitalComponentManufacturingFacility) Then
                        SelectedBPCapitalComponentManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPCapitalComponentManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.Invention
                    PreviousFacility = CType(SelectedBPInventionFacility.Clone, IndustryFacility)
                    SelectedBPInventionFacility = SelectedFacility
                    If SelectedBPInventionFacility.IsEqual(DefaultBPInventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                        SelectedBPInventionFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPInventionFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.T3Invention
                    PreviousFacility = CType(SelectedBPT3InventionFacility.Clone, IndustryFacility)
                    SelectedBPT3InventionFacility = SelectedFacility
                    If SelectedBPT3InventionFacility.IsEqual(DefaultBPT3InventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                        SelectedBPT3InventionFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPT3InventionFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.Copying
                    PreviousFacility = CType(SelectedBPCopyFacility.Clone, IndustryFacility)
                    SelectedBPCopyFacility = SelectedFacility
                    If SelectedBPCopyFacility.IsEqual(DefaultBPCopyFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                        SelectedBPCopyFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPCopyFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.NoPOSManufacturing
                    PreviousFacility = CType(SelectedBPNoPOSFacility.Clone, IndustryFacility)
                    SelectedBPNoPOSFacility = SelectedFacility
                    If SelectedBPNoPOSFacility.IsEqual(DefaultBPNoPOSFacility) Then
                        SelectedBPNoPOSFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPNoPOSFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case Else
                    PreviousFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
                    SelectedBPManufacturingFacility = SelectedFacility
                    If SelectedBPManufacturingFacility.IsEqual(DefaultBPManufacturingFacility) Then
                        SelectedBPManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedBPManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
            End Select
        Else
            Select Case ProductionType
                Case IndustryType.Manufacturing
                    SelectedCalcBaseManufacturingFacility = SelectedFacility
                    If SelectedCalcBaseManufacturingFacility.IsEqual(DefaultCalcBaseManufacturingFacility) Then
                        SelectedCalcBaseManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcBaseManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.POSFuelBlockManufacturing
                    SelectedCalcPOSFuelBlockFacility = SelectedFacility
                    If SelectedCalcPOSFuelBlockFacility.IsEqual(DefaultCalcPOSFuelBlockFacility) And DefaultCalcBaseManufacturingFacility.FacilityType = POSFacility Then
                        SelectedCalcPOSFuelBlockFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcPOSFuelBlockFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.POSLargeShipManufacturing
                    SelectedCalcPOSLargeShipFacility = SelectedFacility
                    If SelectedCalcPOSLargeShipFacility.IsEqual(DefaultCalcPOSLargeShipFacility) And DefaultCalcBaseManufacturingFacility.FacilityType = POSFacility Then
                        SelectedCalcPOSLargeShipFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcPOSLargeShipFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.POSModuleManufacturing
                    SelectedCalcPOSModuleFacility = SelectedFacility
                    If SelectedCalcPOSModuleFacility.IsEqual(DefaultCalcPOSModuleFacility) And DefaultCalcBaseManufacturingFacility.FacilityType = POSFacility Then
                        SelectedCalcPOSModuleFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcPOSModuleFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.BoosterManufacturing
                    SelectedCalcBoosterManufacturingFacility = SelectedFacility
                    If SelectedCalcBoosterManufacturingFacility.IsEqual(DefaultCalcBoosterManufacturingFacility) Then
                        SelectedCalcBoosterManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcBoosterManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.CapitalManufacturing
                    SelectedCalcCapitalManufacturingFacility = SelectedFacility
                    If SelectedCalcCapitalManufacturingFacility.IsEqual(DefaultCalcCapitalManufacturingFacility) Then
                        SelectedCalcCapitalManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcCapitalManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.SuperManufacturing
                    SelectedCalcSuperManufacturingFacility = SelectedFacility
                    If SelectedCalcSuperManufacturingFacility.IsEqual(DefaultCalcSuperManufacturingFacility) Then
                        SelectedCalcSuperManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcSuperManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.T3CruiserManufacturing
                    SelectedCalcT3CruiserManufacturingFacility = SelectedFacility
                    If SelectedCalcT3CruiserManufacturingFacility.IsEqual(DefaultCalcT3CruiserManufacturingFacility) Then
                        SelectedCalcT3CruiserManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcT3CruiserManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.T3DestroyerManufacturing
                    SelectedCalcT3DestroyerManufacturingFacility = SelectedFacility
                    If SelectedCalcT3DestroyerManufacturingFacility.IsEqual(DefaultCalcT3DestroyerManufacturingFacility) Then
                        SelectedCalcT3DestroyerManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcT3DestroyerManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.SubsystemManufacturing
                    SelectedCalcSubsystemManufacturingFacility = SelectedFacility
                    If SelectedCalcSubsystemManufacturingFacility.IsEqual(DefaultCalcSubsystemManufacturingFacility) Then
                        SelectedCalcSubsystemManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcSubsystemManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.ComponentManufacturing
                    SelectedCalcComponentManufacturingFacility = SelectedFacility
                    If SelectedCalcComponentManufacturingFacility.IsEqual(DefaultCalcComponentManufacturingFacility) Then
                        SelectedCalcComponentManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcComponentManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.CapitalComponentManufacturing
                    SelectedCalcCapitalComponentManufacturingFacility = SelectedFacility
                    If SelectedCalcCapitalComponentManufacturingFacility.IsEqual(DefaultCalcCapitalComponentManufacturingFacility) Then
                        SelectedCalcCapitalComponentManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcCapitalComponentManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.Invention
                    SelectedCalcInventionFacility = SelectedFacility
                    If SelectedCalcInventionFacility.IsEqual(DefaultCalcInventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                        SelectedCalcInventionFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcInventionFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.T3Invention
                    SelectedCalcT3InventionFacility = SelectedFacility
                    If SelectedCalcT3InventionFacility.IsEqual(DefaultCalcT3InventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                        SelectedCalcT3InventionFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcT3InventionFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.Copying
                    SelectedCalcCopyFacility = SelectedFacility
                    If SelectedCalcCopyFacility.IsEqual(DefaultCalcCopyFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                        SelectedCalcCopyFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcCopyFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case IndustryType.NoPOSManufacturing
                    SelectedCalcNoPOSFacility = SelectedFacility
                    If SelectedCalcNoPOSFacility.IsEqual(DefaultCalcNoPOSFacility) Then
                        SelectedCalcNoPOSFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcNoPOSFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
                Case Else
                    SelectedCalcBaseManufacturingFacility = SelectedFacility
                    If SelectedCalcBaseManufacturingFacility.IsEqual(DefaultCalcBaseManufacturingFacility) Then
                        SelectedCalcBaseManufacturingFacility.IsDefault = True
                        SelectedFacility.IsDefault = True
                    Else
                        SelectedCalcBaseManufacturingFacility.IsDefault = False
                        SelectedFacility.IsDefault = False
                    End If
            End Select
        End If

        ' Set the default 
        If SelectedFacility.IsDefault = True Then 'Or (FacilityType = POSFacility And FacilityCombo.Items.Count = 1 And Tab = BPTab _
            'And PreviousFacility.FacilityType = SelectedFacility.FacilityType _
            'And PreviousFacility.SolarSystemName = SelectedFacility.SolarSystemName _
            'And PreviousFacility.RegionName = SelectedFacility.RegionName _
            'And PreviousFacility.IncludeActivityUsage = SelectedFacility.IncludeActivityUsage) Then
            FacilityDefaultLabel.ForeColor = SystemColors.Highlight
            Call ResetToolTipforDefaultFacilityLabel(FacilityDefaultLabel, False, ToolTipRef)
            FacilitySaveButton.Enabled = False ' don't enable since it's already the default, it's pointless to save it
        Else
            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
            Call ResetToolTipforDefaultFacilityLabel(FacilityDefaultLabel, True, ToolTipRef)
            FacilitySaveButton.Enabled = True
        End If

    End Sub

    ' Returns the SQL string for querying by category or group id's 
    Public Function GetFacilityCatGroupIDSQL(ByVal CategoryID As Integer, ByVal GroupID As Integer, ByVal Activity As IndustryActivities) As String
        Dim SQL As String = ""
        Dim TempGroupID As Integer
        Dim TempCategoryID As Integer

        ' If the categoryID or groupID is for T3 invention, then switch the item ID's to the blueprint groupID for that item to match CCP's logic in table
        If Activity = IndustryActivities.Invention Then
            If CategoryID = SubsystemCategoryID Then
                TempGroupID = SubsystemBPGroupID
                TempCategoryID = 0
            ElseIf GroupID = StrategicCruiserGroupID Then
                TempGroupID = StrategicCruiserBPGroupID
                TempCategoryID = 0
            ElseIf GroupID = TacticalDestroyerGroupID Then
                TempGroupID = TacticalDestroyerBPGroupID
                TempCategoryID = 0
            Else
                TempGroupID = GroupID
                TempCategoryID = CategoryID
            End If
        Else
            TempGroupID = GroupID
            TempCategoryID = CategoryID
        End If

        SQL = "AND (GROUP_ID = " & CStr(TempGroupID) & " OR (GROUP_ID = 0 AND CATEGORY_ID = " & CStr(TempCategoryID) & ")) "

        Return SQL

    End Function

    ' Hides all the facility bonus boxes and such
    Public Sub HideFacilityBonusBoxes(ByRef LabelBonus As Label, LabelTaxRate As Label, ByRef LabelME As Label,
                                       ByRef LabelTE As Label, ByRef TextME As TextBox, ByRef TextTE As TextBox,
                                       ByRef LabelTax As Label, ByRef TextTax As TextBox,
                                       Optional ByRef UsageLabel As Label = Nothing)
        LabelBonus.Visible = False
        LabelTaxRate.Visible = False
        LabelME.Visible = False
        LabelTE.Visible = False
        TextME.Visible = False
        TextTE.Visible = False
        LabelTax.Visible = False
        TextTax.Visible = False
        ' Clear the usage until these are set
        If Not IsNothing(UsageLabel) Then
            UsageLabel.Text = ""
        End If

    End Sub

    ' Sets all the combos to unenabled and base text to show no facility for stuff like Invention, Copy and RE where they might buy the item
    Public Sub SetNoFacility(ByRef RegionCombo As ComboBox, ByRef SystemCombo As ComboBox, ByRef FacilityorArray As ComboBox,
                              ByRef CheckUsage As CheckBox, Optional IncludeCostCheck As CheckBox = Nothing,
                              Optional IncludeTimeCheck As CheckBox = Nothing, Optional IncludeLabel As Label = Nothing)
        RegionCombo.Items.Clear()
        RegionCombo.Text = "Select Region"
        RegionCombo.Enabled = False
        SystemCombo.Items.Clear()
        SystemCombo.Text = "Select System"
        SystemCombo.Enabled = False
        FacilityorArray.Items.Clear()
        FacilityorArray.Text = "Select Facility / Array"
        CheckUsage.Enabled = False

        If Not IsNothing(IncludeCostCheck) Then
            IncludeCostCheck.Enabled = False
        End If
        If Not IsNothing(IncludeTimeCheck) Then
            IncludeTimeCheck.Enabled = False
        End If
        FacilityorArray.Enabled = False
        If Not IsNothing(IncludeLabel) Then
            IncludeLabel.Enabled = False
        End If
    End Sub

    ' Sets the default based on the cost check change
    Public Sub SetDefaultFacilitybyCheck(ProductionType As IndustryType, IncludeUsageCheck As CheckBox, Tab As String,
                                          FacilityType As String, FacilityArrayCombo As ComboBox, FacilityDefaultLabel As Label,
                                          FacilitySaveButton As Button, Optional IncludeCostCheck As CheckBox = Nothing,
                                          Optional IncludeTimeCheck As CheckBox = Nothing, Optional ToolTipRef As ToolTip = Nothing)
        Dim SelectedFacility As IndustryFacility
        Dim CompareTime As Boolean = False
        Dim CompareCost As Boolean = False

        If Tab = BPTab Then
            Select Case ProductionType
                Case IndustryType.Manufacturing
                    SelectedFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.POSFuelBlockManufacturing
                    SelectedFacility = CType(SelectedBPPOSFuelBlockFacility, IndustryFacility)
                Case IndustryType.POSLargeShipManufacturing
                    SelectedFacility = CType(SelectedBPPOSLargeShipFacility, IndustryFacility)
                Case IndustryType.POSModuleManufacturing
                    SelectedFacility = CType(SelectedBPPOSModuleFacility, IndustryFacility)
                Case IndustryType.BoosterManufacturing
                    SelectedFacility = CType(SelectedBPBoosterManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.CapitalManufacturing
                    SelectedFacility = CType(SelectedBPCapitalManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.SuperManufacturing
                    SelectedFacility = CType(SelectedBPSuperManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.T3CruiserManufacturing
                    SelectedFacility = CType(SelectedBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.T3DestroyerManufacturing
                    SelectedFacility = CType(SelectedBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.SubsystemManufacturing
                    SelectedFacility = CType(SelectedBPSubsystemManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.ComponentManufacturing
                    SelectedFacility = CType(SelectedBPComponentManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.CapitalComponentManufacturing
                    SelectedFacility = CType(SelectedBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.Invention
                    SelectedFacility = CType(SelectedBPInventionFacility, IndustryFacility)
                Case IndustryType.T3Invention
                    SelectedFacility = CType(SelectedBPT3InventionFacility, IndustryFacility)
                Case IndustryType.Copying
                    SelectedFacility = CType(SelectedBPCopyFacility, IndustryFacility)
                Case IndustryType.NoPOSManufacturing
                    SelectedFacility = CType(SelectedBPNoPOSFacility, IndustryFacility)
                Case Else
                    SelectedFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
            End Select
        Else
            Select Case ProductionType
                Case IndustryType.Manufacturing
                    SelectedFacility = CType(SelectedCalcBaseManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.POSFuelBlockManufacturing
                    SelectedFacility = CType(SelectedCalcPOSFuelBlockFacility, IndustryFacility)
                Case IndustryType.POSLargeShipManufacturing
                    SelectedFacility = CType(SelectedCalcPOSLargeShipFacility, IndustryFacility)
                Case IndustryType.POSModuleManufacturing
                    SelectedFacility = CType(SelectedCalcPOSModuleFacility, IndustryFacility)
                Case IndustryType.BoosterManufacturing
                    SelectedFacility = CType(SelectedCalcBoosterManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.CapitalManufacturing
                    SelectedFacility = CType(SelectedCalcCapitalManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.SuperManufacturing
                    SelectedFacility = CType(SelectedCalcSuperManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.T3CruiserManufacturing
                    SelectedFacility = CType(SelectedCalcT3CruiserManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.T3DestroyerManufacturing
                    SelectedFacility = CType(SelectedCalcT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.SubsystemManufacturing
                    SelectedFacility = CType(SelectedCalcSubsystemManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.CapitalComponentManufacturing
                    SelectedFacility = CType(SelectedCalcCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.ComponentManufacturing
                    SelectedFacility = CType(SelectedCalcComponentManufacturingFacility.Clone, IndustryFacility)
                Case IndustryType.Invention
                    SelectedFacility = CType(SelectedCalcInventionFacility, IndustryFacility)
                Case IndustryType.T3Invention
                    SelectedFacility = CType(SelectedCalcT3InventionFacility, IndustryFacility)
                Case IndustryType.Copying
                    SelectedFacility = CType(SelectedCalcCopyFacility, IndustryFacility)
                Case IndustryType.NoPOSManufacturing
                    SelectedFacility = CType(SelectedCalcNoPOSFacility, IndustryFacility)
                Case Else
                    SelectedFacility = CType(SelectedCalcBaseManufacturingFacility.Clone, IndustryFacility)
            End Select
        End If

        SelectedFacility.IncludeActivityUsage = IncludeUsageCheck.Checked

        If Not IsNothing(IncludeCostCheck) Then
            SelectedFacility.IncludeActivityCost = IncludeCostCheck.Checked
            CompareCost = True
        Else
            CompareCost = False
        End If

        If Not IsNothing(IncludeTimeCheck) Then
            SelectedFacility.IncludeActivityTime = IncludeTimeCheck.Checked
            CompareTime = True
        Else
            ' Don't compare this value
            CompareTime = False
        End If

        ' Set the default based on the checkbox 
        Call SetFacilityandDefault(SelectedFacility, ProductionType, Tab, FacilityType, FacilityArrayCombo,
                                   FacilityDefaultLabel, FacilitySaveButton, CompareCost, CompareTime, ToolTipRef)

    End Sub

    ' Sets the tool tip text for default facility labels if they can double click to reload
    Public Sub ResetToolTipforDefaultFacilityLabel(ByRef FacilityDefaultLabel As Label, ByVal ShowTip As Boolean, ByRef ToolTipRef As ToolTip)
        If Not IsNothing(ToolTipRef) Then
            If ShowTip And UserApplicationSettings.ShowToolTips Then
                ToolTipRef.SetToolTip(FacilityDefaultLabel, "Double-Click to reload default facility")
            Else
                ToolTipRef.SetToolTip(FacilityDefaultLabel, "")
            End If
        End If
    End Sub

#End Region

#Region "Time Functions"

    ' Converts a time in d h m s to a long of seconds - 3d 12h 2m 33s or 1 Day 12:23:33
    Public Function ConvertDHMSTimetoSeconds(ByVal SentTime As String) As Long
        Dim Days As Integer = 0
        Dim Hours As Integer = 0
        Dim Minutes As Integer = 0
        Dim Seconds As Integer = 0

        Dim StringMarker As String = ""

        SentTime = Trim(SentTime)

        If SentTime.Contains("Day ") Or SentTime.Contains("Days ") Or SentTime.Contains(":") Then
            ' Time in 2 Days 12:23:05 format
            If SentTime.Contains("Days") Then
                StringMarker = "Days "
            ElseIf SentTime.Contains("Day") Then
                StringMarker = "Day "
            Else
                StringMarker = ""
            End If

            If StringMarker <> "" Then
                ' Get the days
                Days = CInt(SentTime.Substring(0, SentTime.IndexOf(StringMarker)))
                ' Reset the string
                SentTime = Trim(SentTime.Substring(SentTime.IndexOf(StringMarker) + Len(StringMarker)))
            End If

            'Now parse the times
            Hours = CInt(SentTime.Substring(0, SentTime.IndexOf(":")))
            SentTime = Trim(SentTime.Substring(SentTime.IndexOf(":") + 1))
            Minutes = CInt(SentTime.Substring(0, SentTime.IndexOf(":")))
            SentTime = Trim(SentTime.Substring(SentTime.IndexOf(":") + 1))
            Seconds = CInt(SentTime)
        Else

            If SentTime.Contains("d ") Then
                StringMarker = "d "

                ' Get the days
                Days = CInt(SentTime.Substring(0, SentTime.IndexOf(StringMarker)))
                ' Reset the string
                SentTime = Trim(SentTime.Substring(SentTime.IndexOf(StringMarker) + Len(StringMarker)))
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
        End If

        Return (Days * 24 * 60 * 60) + (Hours * 60 * 60) + (Minutes * 60) + Seconds

    End Function

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

        ' Make sure the sent string has no extra spaces that create a blank array entry
        SentTimeString = Trim(SentTimeString)

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

#End Region

    Public Function GetBPType(BPTypeValue As Object) As BPType

        If IsNothing(BPTypeValue) Then
            Return BPType.NotOwned
        End If

        If IsDBNull(BPTypeValue) Then
            Return BPType.NotOwned
        End If

        If BPTypeValue.GetType.Name = "String" Then
            Select Case CStr(BPTypeValue)
                Case BPO
                    Return BPType.Original
                Case BPC
                    Return BPType.Copy
                Case InventedBPC
                    Return BPType.InventedBPC
                Case UnownedBP
                    Return BPType.NotOwned
            End Select
        Else
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
        End If

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

    '' Single function to build the where clause of a options selected for displaying BPs
    'Public Function BuildBPWhereClause() As String



    'End Function

    ' Sorts the reference listview and column
    Public Sub ListViewColumnSorter(ByVal ColumnIndex As Integer, ByRef RefListView As ListView, ByRef ListPrevColumnClicked As Integer, ByRef ListPrevColumnSortOrder As SortOrder)
        Dim SortType As SortOrder

        Application.UseWaitCursor = True
        Application.DoEvents()

        ' Figure out sort order
        If ColumnIndex = ListPrevColumnClicked Then
            If ListPrevColumnSortOrder = SortOrder.Ascending Then
                SortType = SortOrder.Descending
            Else
                SortType = SortOrder.Ascending
            End If
        Else
            If ListPrevColumnSortOrder <> SortOrder.None Then
                ' Swap sort type
                If ListPrevColumnSortOrder = SortOrder.Ascending Then
                    SortType = SortOrder.Descending
                Else
                    SortType = SortOrder.Ascending
                End If
            Else
                SortType = SortOrder.Ascending
            End If
        End If

        ' Perform the sort with these new sort options.
        RefListView.ListViewItemSorter = New ListViewItemComparer(ColumnIndex, SortType)
        RefListView.Sort()

        ' Save the values for next check
        ListPrevColumnClicked = ColumnIndex
        ListPrevColumnSortOrder = SortType

        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

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

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerItem = DBCommand.ExecuteReader
                readerItem.Read()

                If ItemColumns(0).Contains("Tripped") Then
                    Application.DoEvents()
                End If

                If readerItem.HasRows Then
                    ' Format number first if needed
                    If ItemColumns(1).Contains(".") Then
                        ' EU number format - quantity is always an integer (27.070)
                        ItemColumns(1) = ItemColumns(1).Replace(".", ",")
                    End If

                    ' The next item in the list will be the quantity, if not it might be a can or something, so skip it
                    If IsNumeric(ItemColumns(1)) Or ItemColumns(1) = "" Then ' Unpackaged items are 1
                        If ItemColumns(1) = "" Then
                            TempQuantity = 1
                        Else
                            TempQuantity = CLng(ItemColumns(1))
                        End If
                        TempMaterial = New Material(CLng(readerItem.GetValue(0)), ItemColumns(0), "", TempQuantity, 0, 0, "", "")
                        Call CopyPasteMaterials.InsertMaterial(TempMaterial)
                    End If
                End If

                readerItem = Nothing

            Next
        End If

        Return CopyPasteMaterials

    End Function

    ' Imports sent blueprint to shopping list
    Public Sub AddToShoppingList(SentBlueprint As Blueprint, BuildBuy As Boolean, CopyRawMats As Boolean,
                                 ManufacturingFacilityMEModifier As Double, ManufacturingFacilityType As String,
                                 IgnoreInvention As Boolean, IgnoreMinerals As Boolean, IgnoreT1ITem As Boolean,
                                 IncludeActivityCost As Boolean, IncludeActivityTime As Boolean, IncludeActivityUsage As Boolean,
                                 Optional CopyInventionMatsOnly As Boolean = False)
        Dim TempMats As New Materials
        Dim ShoppingItem As New ShoppingListItem
        Dim ShoppingBuildList As New BuiltItemList
        Dim ShoppingBuyList As New Materials

        With ShoppingItem
            If CopyRawMats Or BuildBuy = True Then ' Either just raw or build buy selected
                ' Add the item and the materials for the item
                If Not IsNothing(SentBlueprint.GetRawMaterials) Then
                    .BlueprintTypeID = SentBlueprint.GetTypeID
                    .TypeID = SentBlueprint.GetItemID
                    .Name = SentBlueprint.GetItemData.GetMaterialName
                    .Quantity = SentBlueprint.GetItemData.GetQuantity
                    .ItemME = SentBlueprint.GetME
                    .ItemTE = SentBlueprint.GetTE
                    .ManufacturingFacilityMEModifier = ManufacturingFacilityMEModifier ' For full item, components will be saved in blueprint class for ComponentList
                    .ManufacturingFacilityType = ManufacturingFacilityType
                    .ManufacturingFacilityLocation = SentBlueprint.GetManufacturingFacility.FacilityName

                    ' See if we need to add the system on to the end of the build location for POS
                    If ManufacturingFacilityType = POSFacility Then
                        .ManufacturingFacilityLocation = .ManufacturingFacilityLocation & " (" & SentBlueprint.GetManufacturingFacility.SolarSystemName & ")"
                    End If

                    If BuildBuy Then
                        .BuildType = "Build/Buy"
                    Else
                        ' Just insert the materials in components since we are building all
                        .BuildType = "Raw Mats"
                    End If

                    If Not CopyInventionMatsOnly Then
                        ShoppingBuyList = CType(SentBlueprint.GetRawMaterials.Clone, Materials) ' Need a deep copy because we might insert later
                        ShoppingBuildList = CType(SentBlueprint.GetComponentsList.Clone, BuiltItemList)
                    End If

                    ' Total up all usage
                    .TotalUsage = SentBlueprint.GetManufacturingFacilityUsage + SentBlueprint.GetComponentFacilityUsage + SentBlueprint.GetCapComponentFacilityUsage _
                        + SentBlueprint.GetInventionUsage + SentBlueprint.GetCopyUsage

                    ' Get the build time
                    .TotalBuildTime = SentBlueprint.GetTotalProductionTime

                    ' All blueprint build types we want to save the base materials to build the bp
                    ShoppingItem.BPMaterialList = CType(SentBlueprint.GetComponentMaterials.Clone, Materials)
                    ShoppingItem.BPBuiltItems = CType(SentBlueprint.GetComponentsList.Clone, BuiltItemList)

                End If

            Else
                ' Add the component items and mats to the list and that's it. They are building the end item, nothing else
                If Not IsNothing(SentBlueprint.GetComponentMaterials) Then
                    .BlueprintTypeID = SentBlueprint.GetTypeID
                    .TypeID = SentBlueprint.GetItemID
                    .Name = SentBlueprint.GetItemData.GetMaterialName
                    .Quantity = SentBlueprint.GetItemData.GetQuantity
                    .ItemME = SentBlueprint.GetME
                    .ItemTE = SentBlueprint.GetTE
                    .ManufacturingFacilityMEModifier = ManufacturingFacilityMEModifier ' For full item, components will be saved in blueprint class for ComponentList
                    .ManufacturingFacilityType = ManufacturingFacilityType
                    .ManufacturingFacilityLocation = SentBlueprint.GetManufacturingFacility.FacilityName

                    ' See if we need to add the system on to the end of the build location for POS
                    If ManufacturingFacilityType = POSFacility Then
                        .ManufacturingFacilityLocation = .ManufacturingFacilityLocation & " (" & SentBlueprint.GetManufacturingFacility.SolarSystemName & ")"
                    End If

                    .BuildType = "Components"

                    If Not CopyInventionMatsOnly Then
                        ShoppingBuyList = CType(SentBlueprint.GetComponentMaterials.Clone, Materials) ' Need a deep copy because we might insert later
                        ShoppingBuildList = Nothing
                    End If

                    ' Total up all usage but not component usage
                    .TotalUsage = SentBlueprint.GetManufacturingFacilityUsage + SentBlueprint.GetInventionUsage + SentBlueprint.GetCopyUsage

                    ' Get the build time
                    .TotalBuildTime = SentBlueprint.GetProductionTime

                    ' Make sure all items in the buy list are not set to build
                    For i = 0 To ShoppingBuyList.GetMaterialList.Count - 1
                        ShoppingBuyList.GetMaterialList(i).SetBuildItem(False)
                    Next

                    ' All blueprint build types we want to save the base materials to build it, here we want just what's in the buy list since we aren't building
                    ShoppingItem.BPMaterialList = CType(ShoppingBuyList.Clone, Materials)
                    ShoppingItem.BPBuiltItems = Nothing ' no building here

                End If
            End If

            If SentBlueprint.GetTechLevel = BlueprintTechLevel.T2 Or SentBlueprint.GetTechLevel = BlueprintTechLevel.T3 Then
                If UserApplicationSettings.ShopListIncludeInventMats = True Or CopyInventionMatsOnly Then
                    ' Save the list of invention materials
                    .InventionMaterials = CType(SentBlueprint.GetInventionMaterials.Clone, Materials)

                    ' Now insert into main buy List 
                    ShoppingBuyList.InsertMaterialList(.InventionMaterials.GetMaterialList)

                    ' Remove the data interface though, we will assume they don't want to buy this but this will get listed in the copy output (sent above)
                    ShoppingBuyList.RemoveMaterial(SentBlueprint.GetInventionMaterials.SearchListbyName("Data Interface"))

                    ' Remove the usage as well
                    ShoppingBuyList.RemoveMaterial(SentBlueprint.GetInventionMaterials.SearchListbyName("Invention Usage"))

                End If

                If UserApplicationSettings.ShopListIncludeCopyMats = True Or CopyInventionMatsOnly Then
                    ' Save the list of copy materials
                    .CopyMaterials = CType(SentBlueprint.GetCopyMaterials.Clone, Materials)

                    ' Now insert these into main buy list
                    ShoppingBuyList.InsertMaterialList(.CopyMaterials.GetMaterialList)

                    ' Remove Usage
                    ShoppingBuyList.RemoveMaterial(SentBlueprint.GetInventionMaterials.SearchListbyName("Copy Usage"))
                End If

                ' How many runs do we need to invent this?
                .AvgInvRunsforSuccess = 1 / SentBlueprint.GetInventionChance
                .InventedRunsPerBP = SentBlueprint.GetSingleInventedBPCRuns
                .InventionJobs = SentBlueprint.GetInventionJobs

                ' Decryptor if used
                .Decryptor = SentBlueprint.GetDecryptor.Name
                .Relic = SentBlueprint.GetRelic

            End If

            ' Volume of the item(s)
            .BuildVolume = SentBlueprint.GetTotalItemVolume

            ' Ignore flags
            .IgnoredInvention = IgnoreInvention
            .IgnoredMinerals = IgnoreMinerals
            .IgnoredT1BaseItem = IgnoreT1ITem

            ' Number of bps used
            .NumBPs = SentBlueprint.GetUsedNumBPs

            ' Finally set techlevel
            .TechLevel = SentBlueprint.GetTechLevel

        End With

        ' Add the market cost
        ShoppingItem.TotalItemMarketCost = SentBlueprint.GetItemMarketPrice

        ' Add the final item and mark as items in list
        TotalShoppingList.InsertShoppingItem(ShoppingItem, ShoppingBuildList, ShoppingBuyList)

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

    ' Function to get the regionID from the name sent
    Public Function GetRegionID(ByVal RegionName As String) As Long
        Dim readerRegion As SQLiteDataReader
        Dim ReturnID As Long

        ' Get the region ID
        DBCommand = New SQLiteCommand("SELECT regionID FROM REGIONS WHERE regionName ='" & FormatDBString(RegionName) & "'", EVEDB.DBREf)
        readerRegion = DBCommand.ExecuteReader

        If readerRegion.Read Then
            ReturnID = readerRegion.GetInt64(0)
        Else
            ReturnID = 0
        End If

        readerRegion.Close()
        DBCommand = Nothing

        Return ReturnID

    End Function

    Public Function GetSolarSystemID(ByVal SystemName As String) As Long
        ' Look up Solar System ID
        Dim rsSystem As SQLiteDataReader
        Dim SSID As Long

        DBCommand = New SQLiteCommand("SELECT solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & SystemName & "'", EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            SSID = rsSystem.GetInt64(0)
        Else
            SSID = 0
        End If

        rsSystem.Close()
        DBCommand = Nothing

        Return SSID

    End Function

    ' Loads a referenced combobox with regions
    Public Sub LoadRegionCombo(ByRef RegionCombo As ComboBox, ByVal DefaultRegionName As String)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader

        Sql = "SELECT regionName FROM REGIONS WHERE regionName NOT LIKE '%-R%' OR regionName = 'G-R00031' GROUP BY regionName "
        DBCommand = New SQLiteCommand(Sql, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader
        RegionCombo.BeginUpdate()
        RegionCombo.Items.Clear()
        While rsData.Read
            RegionCombo.Items.Add(rsData.GetString(0))
        End While
        RegionCombo.Text = DefaultRegionName
        RegionCombo.EndUpdate()
        rsData.Close()

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

    ' Updates the value in the progressbar for a smooth progress - total hack from this: http://stackoverflow.com/questions/977278/how-can-i-make-the-progress-bar-update-fast-enough/1214147#1214147
    Public Sub IncrementToolStripProgressBar(ByRef PG As ToolStripProgressBar)
        If PG.Value <= PG.Maximum - 1 Then
            PG.Value = PG.Value + 1
            PG.Value = PG.Value - 1
            PG.Value = PG.Value + 1
        End If
    End Sub

    ' Updates the value in the progressbar for a smooth progress - total hack from this: http://stackoverflow.com/questions/977278/how-can-i-make-the-progress-bar-update-fast-enough/1214147#1214147
    Public Sub IncrementProgressBar(ByRef PG As ProgressBar)
        If PG.Value <= PG.Maximum - 1 Then
            PG.Value = PG.Value + 1
            PG.Value = PG.Value - 1
            PG.Value = PG.Value + 1
        End If
    End Sub

    ' Checks the error sent for EVE API data and shows forms etc based on error
    Public Function NoAPIError(ByVal ErrorText As String, ByVal KeyType As String) As Boolean
        Dim fAccessError As New frmAPIError

        If (ErrorText = NoSkillsLoaded Or ErrorText = NoStandingsLoaded Or ErrorText = NoSkillsStandingsLoaded Or ErrorText = "") And KeyType <> "Corporation" And KeyType <> "" Then
            If ErrorText = NoSkillsStandingsLoaded Then
                fAccessError.ErrorText = "This API did not allow skills or standings to be loaded for associated characters." &
                    Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'Standings' under the 'Public Information' section and 'CharacterSheet' under the 'Private Information' section to include skills and standings and then reload the API."
                fAccessError.Text = "API: No Skills or Standings Loaded"
            ElseIf ErrorText = NoStandingsLoaded Then
                fAccessError.ErrorText = "This API did not allow standings to be loaded for associated characters." &
                    Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'Standings' under the 'Public Information' section to include standings and then reload the API."
                fAccessError.Text = "API: No Standings Loaded"
            ElseIf ErrorText = NoSkillsLoaded Then
                fAccessError.ErrorText = "This API did not allow skills to be loaded for associated characters." &
                    Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'CharacterSheet' under the 'Private Information' section to include skills and then reload the API."
                fAccessError.Text = "API: No Skills Loaded"
            End If

            If ErrorText <> "" Then
                fAccessError.ErrorLink = "https://community.eveonline.com/support/api-key/CreatePredefined?accessMask=589962"
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

    ' Checks for program updates
    Public Sub CheckForUpdates(ByVal ShowFinalMessage As Boolean, ByVal ProgramIcon As Icon)
        Dim Response As DialogResult
        ' Program Updater
        Dim Updater As New ProgramUpdater
        Dim UpdateCode As UpdateCheckResult

        ' 1 = Update Available, 0 No Update Available, -1 an error occured and msg box already shown
        UpdateCode = Updater.IsProgramUpdatable

        Select Case UpdateCode
            Case UpdateCheckResult.UpdateAvailable

                Response = TopMostMessageBox.Show("Update Available - Do you want to update now?", Application.ProductName, MessageBoxButtons.YesNo, ProgramIcon)

                If Response = DialogResult.Yes Then
                    ' Run the updater
                    Call Updater.RunUpdate()
                End If
            Case UpdateCheckResult.UpToDate
                If ShowFinalMessage Then
                    MsgBox("No updates available.", vbInformation, Application.ProductName)
                End If
            Case UpdateCheckResult.UpdateError
                MsgBox("Unable to run update at this time. Please try again later.", vbInformation, Application.ProductName)
        End Select

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

        ' Last update, re-set the names for R.A.M.s and R.Dbs back
        TempString = TempString.Replace("R,A,M,", "R.A.M.")
        TempString = TempString.Replace("R,Db", "R.Db")

        Return TempString

    End Function

    Public Function ConvertEUDecimaltoUSDecimal(ByVal EUFormatedValue As String) As String
        Dim TempString As String = ""

        If EUFormatedValue.Contains(",") Then
            ' This is the EU decimal so change to us
            TempString = EUFormatedValue.Replace(",", ".")
        Else
            TempString = EUFormatedValue
        End If

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

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader

        If readerLookup.Read() Then
            T1BPID = CLng(readerLookup.GetInt64(0))
            readerLookup.Close()

            ' Select all materials now
            SQL = "SELECT ITEM_ID, ITEM_NAME, ITEM_GROUP FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID =" & T1BPID

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerLookup = DBCommand.ExecuteReader

            readerLookup.Read()
            TempMat = New Material(readerLookup.GetInt64(0), readerLookup.GetString(1), readerLookup.GetString(2), 1, 1, 0, "0", "0")
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
        SQL = SQL & "AND typeID = blueprintTypeID AND activityID = 8"

        If RelicName <> "" Then
            ' Need to add the relic variant to the query for just one item
            SQL = SQL & " AND typeName LIKE '%" & RelicName & "%'"
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCheck = DBCommand.ExecuteReader

        If rsCheck.Read Then
            InventItemTypeID = rsCheck.GetInt64(0)
        End If

        Return InventItemTypeID

    End Function

    ' Returns the text race for the ID sent
    Public Function GetRace(ByVal RaceID As Integer) As String
        Dim rsLookup As SQLite.SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT RACE FROM RACE_IDS WHERE ID = " & CStr(RaceID), EVEDB.DBREf)
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

        SQL = "SELECT COALESCE(valuefloat, valueint), ATTRIBUTE_TYPES.displayName "
        SQL = SQL & "FROM INVENTORY_TYPES, TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
        SQL = SQL & "WHERE INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.attributeID = ATTRIBUTE_TYPES.attributeID "
        SQL = SQL & "AND INVENTORY_TYPES.typeName ='" & FormatDBString(TypeName) & "' "
        SQL = SQL & "AND (ATTRIBUTE_TYPES.displayName= '" & AttributeName & "' OR ATTRIBUTE_TYPES.attributeName = '" & AttributeName & "')"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
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

        Call EVEDB.BeginSQLiteTransaction()

        SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID IN (" & UserID & ",0)"
        evedb.ExecuteNonQuerySQL(SQL)

        SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID IN (" & CorpID & ",0)"
        evedb.ExecuteNonQuerySQL(SQL)

        SQL = "UPDATE API SET BLUEPRINTS_CACHED_UNTIL = '1900-01-01 00:00:00' WHERE CHARACTER_ID = " & UserID
        evedb.ExecuteNonQuerySQL(SQL)

        SQL = "UPDATE API SET BLUEPRINTS_CACHED_UNTIL = '1900-01-01 00:00:00' WHERE API_TYPE = 'Corporation' "
        SQL = SQL & "AND CORPORATION_ID = " & CorpID
        evedb.ExecuteNonQuerySQL(SQL)

        Call EVEDB.CommitSQLiteTransaction()

        MsgBox("Blueprints reset", vbInformation, Application.ProductName)

    End Sub

    ' Sets an existing bp in the DB to the ME/TE or adds it if not in DB as a new owned blueprint - this is always due to user input, not API
    Public Function UpdateBPinDB(ByVal BPID As Long, ByVal BPName As String, ByVal bpME As Integer, ByVal bpTE As Integer, ByVal SentBPType As BPType,
                            ByVal OriginalME As Integer, ByVal OriginalTE As Integer, ByRef UserRuns As Integer,
                            Optional Favorite As Boolean = False, Optional Ignore As Boolean = False, Optional AdditionalCosts As Double = 0,
                            Optional RemoveAll As Boolean = False) As BPType
        Dim SQL As String
        Dim readerBP As SQLiteDataReader
        Dim rsMaxRuns As SQLiteDataReader
        Dim TempFavorite As String
        Dim TempIgnore As String
        Dim TempOwned As String
        Dim UpdatedBPType As BPType

        If SentBPType = BPType.NotOwned And (bpME <> OriginalME Or bpTE <> OriginalTE) Then
            ' Can't update the ME/TE and not saved as owned
            UpdatedBPType = BPType.Copy ' save all as copy
        Else
            UpdatedBPType = SentBPType
        End If

        If UpdatedBPType = BPType.Original Then
            UserRuns = -1
        Else
            DBCommand = New SQLiteCommand("SELECT MAX_PRODUCTION_LIMIT FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID = " & CStr(BPID), EVEDB.DBREf)
            rsMaxRuns = DBCommand.ExecuteReader
            If rsMaxRuns.Read() Then
                UserRuns = rsMaxRuns.GetInt32(0)
            Else
                UserRuns = 0
            End If
        End If

        EVEDB.BeginSQLiteTransaction()

        ' If they are setting to not owned, not updating the ME/TE and not saving favorite or ignore, then remove the bp
        If (UpdatedBPType = BPType.NotOwned And Favorite = False And Ignore = False) Or RemoveAll Then

            ' Look up the BP first to see if it is scanned
            SQL = "SELECT 'X' FROM OWNED_BLUEPRINTS "
            SQL = SQL & "WHERE (USER_ID =" & CStr(SelectedCharacter.ID) & " Or USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
            SQL = SQL & "AND BLUEPRINT_ID =" & CStr(BPID) & " AND SCANNED <> 0"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader
            readerBP.Read()

            ' If Found then update then just reset the owned flag - might be scanned
            If readerBP.HasRows Then
                ' Update it
                SQL = "UPDATE OWNED_BLUEPRINTS Set OWNED = 0, Me = 0, TE = 0, FAVORITE = 0, BP_TYPE = 0 "
                SQL = SQL & "WHERE (USER_ID =" & CStr(SelectedCharacter.ID) & " Or USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
                SQL = SQL & "And BLUEPRINT_ID =" & CStr(BPID)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            Else
                ' Just delete the record since it's not scanned
                SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID=" & SelectedCharacter.ID & " And BLUEPRINT_ID=" & BPID
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            End If

            ' Update the bp ignore flag (note for all accounts on this pc)
            SQL = "UPDATE ALL_BLUEPRINTS Set IGNORE = 0 WHERE BLUEPRINT_ID = " & CStr(BPID)
            Call EVEDB.ExecuteNonQuerySQL(SQL)

        Else

            ' Set the flags
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
            If UpdatedBPType = BPType.NotOwned Then
                TempOwned = "0" ' User updated, not owned
            Else
                TempOwned = "-1" ' User updated, user owned (not API)
            End If

            ' See if the BP is in the DB
            SQL = "SELECT TE FROM OWNED_BLUEPRINTS WHERE (USER_ID =" & CStr(SelectedCharacter.ID) & " OR USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
            SQL = SQL & "AND BLUEPRINT_ID =" & CStr(BPID)

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader
            readerBP.Read()

            If Not readerBP.HasRows Then
                ' No record, So add it and mark as owned (code 2) - save the scanned data if it was scanned - no item id or location id (from API), so set to 0 on manual saves
                SQL = "INSERT INTO OWNED_BLUEPRINTS (USER_ID, ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, QUANTITY, FLAG_ID, "
                SQL = SQL & "ME, TE, RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS) "
                SQL = SQL & "VALUES (" & SelectedCharacter.ID & ",0,0," & BPID & ",'" & FormatDBString(BPName) & "',1,0,"
                SQL = SQL & CStr(bpME) & "," & CStr(bpTE) & "," & CStr(UserRuns) & "," & CStr(UpdatedBPType) & "," & TempOwned & ",0," & TempFavorite & "," & CStr(AdditionalCosts) & ")"
                Call EVEDB.ExecuteNonQuerySQL(SQL)

            Else
                ' Update it
                SQL = "UPDATE OWNED_BLUEPRINTS SET ME = " & CStr(bpME) & ", TE = " & CStr(bpTE) & ", OWNED = " & TempOwned & ", FAVORITE = " & TempFavorite
                SQL = SQL & ", ADDITIONAL_COSTS = " & CStr(AdditionalCosts) & ", BP_TYPE = " & CStr(UpdatedBPType) & ", RUNS = " & CStr(UserRuns) & " "
                SQL = SQL & "WHERE (USER_ID =" & CStr(SelectedCharacter.ID) & " OR USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
                SQL = SQL & "AND BLUEPRINT_ID =" & CStr(BPID)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            End If

            ' Update the bp ignore flag (note for all accounts on this pc)
            SQL = "UPDATE ALL_BLUEPRINTS SET IGNORE = " & TempIgnore & " WHERE BLUEPRINT_ID = " & CStr(BPID)
            Call EVEDB.ExecuteNonQuerySQL(SQL)

        End If

        readerBP.Close()
        readerBP = Nothing
        DBCommand = Nothing

        EVEDB.CommitSQLiteTransaction()

        Return UpdatedBPType

    End Function

    ' Downloads the sent file from server and saves it to the root directory as the sent file name
    Public Function DownloadFileFromServer(ByVal DownloadURL As String, ByVal FileName As String) As String
        ' Creating the request And getting the response
        Dim Response As HttpWebResponse
        Dim Request As HttpWebRequest

        ' For reading in chunks of data
        Dim readBytes(4095) As Byte
        ' Save in root directory
        Dim writeStream As New FileStream(FileName, FileMode.Create)
        Dim bytesread As Integer

        Try 'Checks if the file exist
            Request = DirectCast(HttpWebRequest.Create(DownloadURL), HttpWebRequest)
            'Request.Proxy = GetProxyData()
            Request.Credentials = CredentialCache.DefaultCredentials ' Added 9/27 to attempt to fix error: (407) Proxy Authentication Required.
            Request.Timeout = 50000
            Response = CType(Request.GetResponse, HttpWebResponse)
        Catch ex As Exception
            ' Show error and exit
            'Close the streams
            writeStream.Close()
            MsgBox("An error occurred while downloading update file: " & ex.Message, vbCritical, Application.ProductName)
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

        ' Finally, check if the file is xml or text and adjust the lf to crlf (git saves as unix or lf only)
        If FileName.Contains(".txt") Then 'Or FileName.Contains(".xml") Then
            Dim FileText As String = File.ReadAllText(FileName)
            FileText = FileText.Replace(Chr(10), vbCrLf)
            ' Write the file back out if it's been updated
            File.WriteAllText(FileName, FileText)
        End If

        Return FileName

    End Function

    Public Function GetProxyData() As WebProxy
        Dim ReturnProxy As WebProxy

        If UserApplicationSettings.ProxyAddress <> "" Then
            If UserApplicationSettings.ProxyPort <> 0 Then
                ReturnProxy = New WebProxy(UserApplicationSettings.ProxyAddress, UserApplicationSettings.ProxyPort)
            Else
                ReturnProxy = New WebProxy(UserApplicationSettings.ProxyAddress)
            End If

            ReturnProxy = New WebProxy(UserApplicationSettings.ProxyAddress, UserApplicationSettings.ProxyPort)
            ReturnProxy.Credentials = CredentialCache.DefaultCredentials

            Return ReturnProxy
        Else
            Return Nothing
        End If

    End Function

    ' Looks up the relic based on the decryptor used and the runs sent on the bp the relic created
    Public Function GetRelicfromInputs(ByVal DecryptorUsed As Decryptor, BPID As Long, BPRuns As Integer) As String

        Dim BaseRuns As Integer = BPRuns - DecryptorUsed.RunMod ' Adjust runs for look up
        Dim SQL As String
        Dim readerBP As SQLiteDataReader
        Dim ReturnString As String

        SQL = "SELECT typeName, quantity FROM INVENTORY_TYPES, INDUSTRY_ACTIVITY_PRODUCTS "
        SQL = SQL & "WHERE typeID = blueprintTypeID AND activityID = 8 AND productTypeID = " & CStr(BPID) & " AND quantity <= " & BaseRuns

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader()

        If readerBP.Read Then
            ReturnString = readerBP.GetString(0)
        Else
            ReturnString = ""
        End If

        readerBP.Close()
        readerBP = Nothing

        Return ReturnString

    End Function

    ' Parses the data and builds an AND qualifier for searching text data - adds data for two fields sent
    Public Function GetSearchText(SearchText As String, Field1 As String, Optional Field2 As String = "") As String
        Dim ReturnString As String = ""
        Dim LikePhrase As String = " LIKE "
        Dim NOTLikePhrase As String = " NOT LIKE "

        Dim SplitPhrase As Boolean = False ' If they have something like 'Oxygen NOT Isotopes'
        Dim SearchItems() As String = Nothing
        Dim NotSearchItems() As String = Nothing
        Dim SearchItemList As String = ""
        Dim NotSearchItemList As String = ""

        Dim SearchClause As String = ""
        Dim NotSearchClause As String = ""

        If Trim(SearchText) = "" Or Field1 = "" Then
            Return ""
        End If

        ' Options
        ' 1 - 'mining crystal not rig, jaspet' - want mining crystals but not the mercoxit mining crystal rig or jaspet mining crystals
        ' 2 - 'Hulk, Mackinaw, Skiff' - want only these three
        ' 3 - 'NOT Hulk, Mackinaw, Skiff' - don't want these three but all others

        ' See if it has not and if larger than three characters, allow it to be set - test 'bob NOT '
        If UCase(SearchText).Contains("NOT ") And Trim(SearchText).Length > 4 Then
            ' Find where the NOT is in the string
            Dim NotLocation As Integer = InStr(UCase(SearchText), "NOT ")

            ' If it's at the beginning, then the rest is a not phrase
            If NotLocation = 0 Then
                ' Strip off the not and add
                SearchText = Trim(SearchText.Substring(4))
                NotSearchItemList = Trim(SearchText)
            Else
                ' split and Strip off the NOT at the beginning 
                SearchItemList = FormatDBString(SearchText.Substring(0, NotLocation - 1))
                NotSearchItemList = FormatDBString(SearchText.Substring(NotLocation + 3))
            End If
        Else
            ' Just search for the terms
            SearchItemList = FormatDBString(SearchText)
            SplitPhrase = False
        End If

        ' Parse by comma then loop through items to build clauses
        If SearchItemList <> "" Then
            SearchItems = SearchItemList.Split(New [Char]() {CChar(",")}, StringSplitOptions.RemoveEmptyEntries)
        End If

        If NotSearchItemList <> "" Then
            NotSearchItems = NotSearchItemList.Split(New [Char]() {CChar(",")}, StringSplitOptions.RemoveEmptyEntries)
        End If

        ' Build the like search items
        If Not IsNothing(SearchItems) Then
            For i = 0 To SearchItems.Count - 1
                SearchClause = SearchClause & "(" & Field1 & LikePhrase & "'%" & Trim(SearchItems(i)) & "%' "
                If Field2 <> "" Then
                    SearchClause = SearchClause & "OR " & Field2 & LikePhrase & "'%" & Trim(SearchItems(i)) & "%') OR "
                Else
                    SearchClause = SearchClause & ") OR "
                End If
            Next

            ' Clean up the clause
            SearchClause = "(" & SearchClause.Substring(0, SearchClause.Length - 4) & ")" ' Strip off the and

        End If

        ' Do the other phrase if needed
        If Not IsNothing(NotSearchItems) Then
            For i = 0 To NotSearchItems.Count - 1
                NotSearchClause = NotSearchClause & "(" & Field1 & " " & NOTLikePhrase & "'%" & Trim(NotSearchItems(i)) & "%' "
                If Field2 <> "" Then
                    NotSearchClause = NotSearchClause & "AND " & Field2 & " " & NOTLikePhrase & "'%" & Trim(NotSearchItems(i)) & "%') AND "
                Else
                    NotSearchClause = NotSearchClause & ") AND "
                End If
            Next

            ' Clean up clause
            NotSearchClause = "(" & NotSearchClause.Substring(0, NotSearchClause.Length - 5) & ")" ' Strip off the and

        End If

        If SearchClause <> "" And NotSearchClause <> "" Then
            ReturnString = "(" & SearchClause & " AND " & NotSearchClause & ")"
        ElseIf SearchClause <> "" Then
            ReturnString = SearchClause
        Else
            ReturnString = NotSearchClause
        End If

        Return ReturnString

    End Function

    ' Gets the typeName from inventory types for the typeID sent
    Public Function GetTypeName(ByVal TypeID As Integer) As String
        Dim SQL As String
        Dim readerIT As SQLiteDataReader
        Dim ReturnString As String

        SQL = "SELECT typeName FROM INVENTORY_TYPES WHERE typeID = " & CStr(TypeID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerIT = DBCommand.ExecuteReader()

        If readerIT.Read Then
            ReturnString = readerIT.GetString(0)
        Else
            ReturnString = ""
        End If

        readerIT.Close()
        readerIT = Nothing

        Return ReturnString

    End Function

    ' Looks up the typeid for the mining bonus for different attributes related to mining and returns the value for everything except ships (those are invTraits)
    Public Function GetMiningBonus(ByVal TypeID As Integer) As Double
        Dim SQL As String
        Dim readerBonus As SQLiteDataReader
        Dim ReturnBonus As Double

        ' It's a module - compressionQuantityNeeded and mining amounts
        SQL = "SELECT TYPE_ATTRIBUTES.attributeID, COALESCE(valueInt, valueFloat) AS Bonus "
        SQL &= "FROM TYPE_ATTRIBUTES, INVENTORY_TYPES, INVENTORY_GROUPS, ATTRIBUTE_TYPES "
        SQL &= "WHERE TYPE_ATTRIBUTES.typeid = INVENTORY_TYPES.typeid "
        SQL &= "AND INVENTORY_TYPES.groupid = INVENTORY_GROUPS.groupid "
        SQL &= "AND TYPE_ATTRIBUTES.attributeID = ATTRIBUTE_TYPES.attributeID "
        SQL &= "AND TYPE_ATTRIBUTES.attributeid IN (434,77,1938,207,2458,428,1941,379,780,885) "
        SQL &= "AND INVENTORY_TYPES.groupID Not In (1,1218) And categoryID <> 6 "
        SQL &= "AND TYPE_ATTRIBUTES.typeID = " & CStr(TypeID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBonus = DBCommand.ExecuteReader()

        If readerBonus.Read Then
            ReturnBonus = readerBonus.GetDouble(0)
        Else
            ReturnBonus = 0
        End If

        readerBonus.Close()
        readerBonus = Nothing

        Return ReturnBonus

    End Function

End Module
