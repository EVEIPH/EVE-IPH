
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Net
Imports System.IO
Imports System.Security.Cryptography

' Place to store all public variables and functions
Public Module Public_Variables
    ' DB name and version
    Public Const SDEVersion As String = "March 15, 2023 Release"
    Public Const VersionNumber As String = "5.0.*"

    Public TestingVersion As Boolean ' This flag will test the test downloads from the server for an update
    Public Developer As Boolean ' This is if I'm developing something and only want me to see it instead of public release

    Public LocalCulture As CultureInfo

    Public EVEDB As DBConnection
    Public DBCommand As SQLiteCommand
    ' For checking the DB to see if it's ok to write
    Public DBLock As New Object

    Public SelectedCharacter As New Character
    Public SelectedBlueprint As Blueprint

    Public Const DummyClient As String = ""
    Public Const DefaultCharacterCode As Integer = -1 ' To indicate the default character
    Public Const DummyCharacterID As Long = -1
    Public Const DummyCorporationID As Long = -1

    Public Const CommonSavedFacilitiesID As Integer = -2
    Public Const CommonLoadBPsID As Integer = -2

    ' Variable to hold error tracking data when the error is hard to find - used for debugging only but mostly this is set to empty string
    Public ErrorTracker As String
    Public ESIErrorHandler As ESIErrorProcessor

    Public DefaultCharSelected As Boolean
    Public FirstLoad As Boolean ' If the program just opened
    Public SkillsUpdated As Boolean ' To track if skills where updated in the skill override screen
    Public ManufacturingTabColumnsChanged As Boolean ' To track if thy changed columns

    ' File Paths
    Public DynamicFilePath As String = "" ' Where the update and settings files are stored that we can write, create, delete, etc.
    Public DBFilePath As String = "" ' Where the DB is stored for updates

    Public Const PatchNotesURL = "https://raw.githubusercontent.com/EVEIPH/LatestFiles/master/Patch%20Notes.txt"
    Public Const XMLUpdateFileURL = "https://raw.githubusercontent.com/EVEIPH/LatestFiles/master/LatestVersionIPH.xml"
    Public Const XMLUpdateTestFileURL = "https://github.com/EVEIPH/LatestFiles/raw/master/LatestVersionIPH_Test.xml"

    Public Const DynamicAppDataPath As String = "EVE IPH"
    Public Const UserImagePath As String = "EVEIPH Images"
    Public Const UpdatePath As String = "EVE IPH Updates"
    Public Const SettingsFolder As String = "Settings" ' For saving all settings

    Public Const SQLiteDBFileName As String = "EVEIPH DB.sqlite"

    Public ReactionTypes As New List(Of String)(New String() {"Composite", "Intermediate Materials", "Hybrid Polymers"})

    ' For updates
    Public Const UpdaterFileName As String = "EVEIPH Updater.exe"
    Public Const SQLiteDLLUpdater As String = "EVEIPH SQLite DLL Updater.exe"
    Public Const XMLUpdaterFileName As String = "EVEIPH_Updater.exe" ' For use in the XML files to remove spaces from row names
    Public Const XMLLatestVersionFileName As String = "LatestVersionIPH.xml"
    Public Const XMLLatestVersionTest As String = "LatestVersionIPH Test.xml"

    ' Only request ESI scopes I need - if I add a scope, the user will need to re-authorize for the new scopes.
    Public ESIScopesString As String = ""

    Public Const TheForgeTypeID As Long = 10000002
    Public Const JitaPerimeter As String = "Jita/Perimeter"

    Public Const USER_BLUEPRINTS As String = "(SELECT ALL_BLUEPRINTS.BLUEPRINT_ID AS BP_ID, ALL_BLUEPRINTS.BLUEPRINT_GROUP, ALL_BLUEPRINTS.BLUEPRINT_NAME, " _
                                            & "ITEM_GROUP_ID, ITEM_GROUP, ITEM_CATEGORY_ID, CASE WHEN ITEM_GROUP LIKE 'RIG%' THEN 'RIG' ELSE ITEM_CATEGORY END AS ITEM_CATEGORY, " _
                                            & "ALL_BLUEPRINTS.ITEM_ID, ITEM_NAME," _
                                            & "CASE WHEN OBP.ME IS NOT NULL THEN OBP.ME ELSE 0 END AS ME," _
                                            & "CASE WHEN OBP.TE IS NOT NULL THEN OBP.TE ELSE 0 END AS TE," _
                                            & "CASE WHEN USER_ID IS NOT NULL THEN USER_ID ELSE 0 END AS USER_ID, ITEM_TYPE," _
                                            & "CASE WHEN ALL_BLUEPRINTS.RACE_ID IS NOT NULL THEN ALL_BLUEPRINTS.RACE_ID ELSE 0 END AS RACE_ID," _
                                            & "CASE WHEN OBP.OWNED IS NOT NULL THEN OBP.OWNED ELSE 0 END AS OWNED," _
                                            & "CASE WHEN OBP.SCANNED IS NOT NULL THEN OBP.SCANNED ELSE 0 END AS SCANNED," _
                                            & "CASE WHEN OBP.BP_TYPE IS NOT NULL THEN OBP.BP_TYPE ELSE 0 END AS BP_TYPE," _
                                            & "CASE WHEN OBP.ITEM_ID IS NOT NULL THEN OBP.ITEM_ID ELSE 0 END AS UNIQUE_BP_ITEM_ID, " _
                                            & "CASE WHEN OBP.FAVORITE IS NOT NULL THEN OBP.FAVORITE " _
                                            & "ELSE CASE WHEN ALL_BLUEPRINTS.FAVORITE IS NOT NULL THEN ALL_BLUEPRINTS.FAVORITE ELSE 0 END END AS FAVORITE, " _
                                            & "IT.VOLUME, IT.MARKETGROUPID, " _
                                            & "CASE WHEN OBP.ADDITIONAL_COSTS IS NOT NULL THEN OBP.ADDITIONAL_COSTS ELSE 0 END AS ADDITIONAL_COSTS, " _
                                            & "CASE WHEN OBP.LOCATION_ID IS NOT NULL THEN OBP.LOCATION_ID ELSE 0 END AS LOCATION_ID, " _
                                            & "CASE WHEN OBP.QUANTITY IS NOT NULL THEN OBP.QUANTITY ELSE 0 END AS QUANTITY, " _
                                            & "CASE WHEN OBP.FLAG_ID IS NOT NULL THEN OBP.FLAG_ID ELSE 0 END AS FLAG_ID, " _
                                            & "CASE WHEN OBP.RUNS IS NOT NULL THEN OBP.RUNS ELSE 0 END AS RUNS, " _
                                            & "IGNORE, ALL_BLUEPRINTS.TECH_LEVEL, SIZE_GROUP, " _
                                            & "CASE WHEN IT2.MARKETGROUPID IS NULL THEN 0 ELSE 1 END AS NPC_BPO " _
                                            & "FROM ALL_BLUEPRINTS LEFT OUTER JOIN " _
                                            & "(SELECT * FROM OWNED_BLUEPRINTS) AS OBP " _
                                            & "ON ALL_BLUEPRINTS.BLUEPRINT_ID = OBP.BLUEPRINT_ID " _
                                            & "AND (OBP.USER_ID = @USERBP_USERID OR OBP.USER_ID = @USERBP_CORPID), " _
                                            & "INVENTORY_TYPES AS IT, INVENTORY_TYPES AS IT2 " _
                                            & "WHERE ALL_BLUEPRINTS.ITEM_ID = IT.TYPEID AND ALL_BLUEPRINTS.BLUEPRINT_ID = IT2.TYPEID) AS X "


    ' Shopping List
    Public TotalShoppingList As New ShoppingList

    ' For a new shopping list, so we can upate it when it's open
    Public frmShop As frmShoppingList = New frmShoppingList
    Public frmConversionOptions As frmConversiontoOreSettings
    Public CopyPasteRefineryMaterialText As String

    ' Same with assets
    Public frmDefaultAssets As frmAssetsViewer
    Public frmShoppingAssets As frmAssetsViewer
    Public frmRefineryAssets As frmAssetsViewer
    Public frmViewStructures As frmViewSavedStructures = New frmViewSavedStructures
    Public frmRepoPlant As frmReprocessingPlant

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
    Public CancelThreading As Boolean

    ' Column processing
    Public Const NumManufacturingTabColumns As Integer = 110
    Public Const NumIndustryJobColumns As Integer = 21

    Public Const BaseRefineRate As Double = 0.5

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

    ' For industry tab loading
    Public Const BPTab As String = "BP"
    Public Const CalcTab As String = "Calc"

    ' For update prices
    Public Const DefaultSystemPriceCombo As String = "Select System"
    Public Const DefaultRegionPriceCombo As String = "Select Region"

    Public Const AllSystems As String = "All Systems"

    ' For unhandled exceptions
    Public frmErrorText As String = ""
    Public ErrorTest As String = ""

    Public PriceQueryCount As Integer ' This will track the number of times the user queries EVE Central - used to warn them for over pinging
    Public CalcHistoryRegionLoaded As Boolean
    Public ShownPriceUpdateError As Boolean ' Only want to show them the error once

    Public Const BPO As String = "BPO"
    Public Const BPC As String = "BPC"
    Public Const InventedBPC As String = "Invented BPC"
    Public Const UnownedBP As String = "Unowned"

    Public Const Yes As String = "Yes"
    Public Const No As String = "No"
    Public Const Unknown As String = "Unknown"
    Public Const Unlimited As String = "Unlimited"
    Public Const Male As String = "male"

    Public NoFacility As New IndustryFacility

    Public Const None As String = "None" ' For decryptors and facilities

    Public MiningUpgradesCollection As New List(Of String)

    Public CancelESISSOLogin As Boolean

    Public NoPOSCategoryIDs As List(Of Long) ' For facilities

    Public Const DefaultStructureTaxRate = 0.0 ' 0% to start for structures
    Public Const DefaultStationTaxRate = 0.0025 ' 0.25% for all stations

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
    Public Const Gnosis As String = "Gnosis"
    Public Const Rokh As String = "Rokh"

    ' For exporting Data
    Public Const DefaultTextDataExport As String = "Default"
    Public Const CSVDataExport As String = "CSV"
    Public Const SSVDataExport As String = "SSV"
    Public Const MultiBuyDataExport As String = "Multibuy"

    Public SetTaxFeeChecks As Boolean
    Public LocationIDs As New List(Of Long)

    Public MaxStationID As Long = 67000000
    Public MinStationID As Long = 60000000

    Public BaseSalesTaxRate As Double = 8 ' Sales tax base is 8 and during holidays they may change to 50% or 4 to make it work
    Public BaseBrokerFeeRate As Double = 3
    Public SCCBrokerFeeSurcharge As Double = 0.005 ' Fixed rate of 0.5%
    Public SCCIndustryFeeSurcharge As Double = 0.015 ' Fixed rate of 1.5% on 9/12/2023

    Public Const AlphaAccountTaxRate As Double = 0.0025 ' fixed to 0.25%

    ' Opened forms from menu
    Public ReprocessingPlantOpen As Boolean
    Public OreBeltFlipOpen As Boolean
    Public IceBeltFlipOpen As Boolean

    ' Limits for Market History endpoint
    Public Const MaxMarketHistoryCallsPerMinute As Integer = 300
    Public MarketHistoryCallsPerMinute As Integer = 0
    Public LastMarketHistoryUpdate As Date = NoDate

    Public PriceUpdateDown As Boolean = False

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

    Public Enum BPTechLevel
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

    Public Enum SentFromLocation
        None = 0
        BlueprintTab = 1
        ManufacturingTab = 2
        History = 3
        ShoppingList = 4
    End Enum

    Public Enum CopyPasteWindowType
        Materials = 1
        Blueprints = 2
    End Enum

    Public Enum CopyPasteWindowLocation
        Assets = 1
        RefineMaterials = 2
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

    Public Structure BrokerFeeInfo
        Dim IncludeFee As BrokerFeeType
        Dim FixedRate As Double
    End Structure

    Public Enum BrokerFeeType
        NoFee = 0
        Fee = 1
        SpecialFee = 2
    End Enum

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

        'Invoke the splach screen's SetProgress method on the thread that owns it.
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

    Public Structure BuildBuyItem
        Dim ItemID As Long
        Dim BuildItem As Boolean ' True, we build it regardless, False we do not regardless. If not in list, we don't do anything differently
    End Structure

    ' Returns the ReactionGroupID number if sent is a reaction, else -1
    Public Function ReactionGroupID(CheckID As Integer) As Integer
        Select Case CheckID
            Case ItemIDs.ReactionBiochemicalsGroupID, ItemIDs.ReactionCompositesGroupID, ItemIDs.ReactionPolymersGroupID, ItemIDs.ReactionsIntermediateGroupID, ItemIDs.ReactionMolecularForgedGroupID
                Return CheckID
            Case Else
                Return -1
        End Select
    End Function

    Public Function IsReaction(ByVal ItemGroupID As Integer) As Boolean
        If ReactionGroupID(ItemGroupID) <> -1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Enum ItemIDs
        None = 0

        ' These are all the capital ships that use capital parts
        CapitalIndustrialShipGroupID = 883
        CarrierGroupID = 547
        DreadnoughtGroupID = 485
        FreighterGroupID = 513
        IndustrialCommandShipGroupID = 941
        JumpFreighterGroupID = 902
        SupercarrierGroupID = 659
        FAXGroupID = 1538
        TitanGroupID = 30
        BoosterGroupID = 303
        BoosterCategoryID = 20

        ' T3 items
        StrategicCruiserGroupID = 963
        TacticalDestroyerGroupID = 1305
        SubsystemCategoryID = 32

        ' T3 Bps for facility updates
        StrategicCruiserBPGroupID = 996
        TacticalDestroyerBPGroupID = 1309
        SubsystemBPGroupID = 973

        ShipCategoryID = 6 ' for loading invention and copying and basic t1
        FrigateGroupID = 25

        ' Reactions
        ReactionsIntermediateGroupID = 428
        ReactionCompositesGroupID = 429
        ReactionPolymersGroupID = 974
        ReactionBiochemicalsGroupID = 712
        ReactionMolecularForgedGroupID = 4096

        ConstructionComponentsGroupID = 334 ' Use this for all non-capital components
        ComponentCategoryID = 17
        CapitalComponentGroupID = 873
        AdvCapitalComponentGroupID = 913
        ProtectiveComponentGroupID = -3 ' My manual group ID until they fix it in the SDE

        BlueprintCategoryID = 9
        FrigateBlueprintGroupID = 105

        AsteroidsCategoryID = 25 ' category for asteroids,ice,moons = 25
        ' Ice group id = 465
        IceGroupID = 465

        ' Groupids for moon ores - 1884, 1920, 1921, 1922, 1923
        CommonMoonAsteroids = 1920
        ExceptionalMoonAsteroids = 1923
        RareMoonAsteroids = 1922
        UbiquitousMoonAsteroids = 1884
        UncommonMoonAsteroids = 1921

        ' Groupid's for asteroid types - regular ore
        Arkonor = 450
        Bistot = 451
        Crokite = 452
        DarkOchre = 453
        Gneiss = 467
        Hedbergite = 454
        Hemorphite = 455
        Jaspet = 456
        Kernite = 457
        Mercoxit = 468
        Omber = 469
        Plagioclase = 458
        Pyroxeres = 459
        Scordite = 460
        Spodumain = 461
        Veldspar = 462

    End Enum

#Region "Taxes/Fees"

    Public Function AdjustPriceforTaxesandFees(ByVal OriginalPrice As Double, ByVal SetTax As Boolean, ByVal BrokerFeeData As BrokerFeeInfo) As Double
        If OriginalPrice <= 0 Then
            Return 0
        End If

        Dim NewPrice As Double = 0

        ' Apply taxes and fees
        If SetTax Then
            NewPrice = OriginalPrice - GetSalesTax(OriginalPrice) - GetSalesBrokerFee(OriginalPrice, BrokerFeeData)
        Else
            NewPrice = OriginalPrice - GetSalesBrokerFee(OriginalPrice, BrokerFeeData)
        End If

        Return NewPrice

    End Function

    ' Returns the tax on an item price only
    Public Function GetSalesTax(ByVal ItemMarketCost As Double) As Double
        Dim Accounting As Integer = SelectedCharacter.Skills.GetSkillLevel(16622)
        ' Each level of accounting reduces tax by 11%, Max/Base Sales Tax: 8%, Min Sales Tax: 3.6%
        ' Latest info: https://www.eveonline.com/news/view/restructuring-taxes-after-relief
        Return (BaseSalesTaxRate - (Accounting * 0.11 * BaseSalesTaxRate)) / 100 * ItemMarketCost
    End Function

    ' Returns the tax on setting up a sell order for an item price only
    Public Function GetSalesBrokerFee(ByVal ItemMarketCost As Double, BrokerFee As BrokerFeeInfo) As Double
        Dim BrokerRelations As Integer = SelectedCharacter.Skills.GetSkillLevel(3446)
        Dim TempFee As Double

        If BrokerFee.IncludeFee = BrokerFeeType.Fee Then
            ' 3%-(0.3%*BrokerRelationsLevel)-(0.03%*FactionStanding)-(0.02%*CorpStanding) - uses unmodified standings
            ' Base broker fee = 3%, Min broker fees: 1.0%
            ' Latest info: https://www.eveonline.com/news/view/restructuring-taxes-after-relief
            ' and https://www.eveonline.com/de/news/view/viridian-expansion-notes
            Dim BrokerTax = BaseBrokerFeeRate - (0.3 * BrokerRelations) - (0.03 * UserApplicationSettings.BrokerFactionStanding) - (0.02 * UserApplicationSettings.BrokerCorpStanding)
            TempFee = (BrokerTax / 100) * ItemMarketCost
        ElseIf BrokerFee.IncludeFee = BrokerFeeType.SpecialFee Then
            ' use a flat rate to set the fee - Since they are setting this, assume they are in an Upwell and add in the SCC fixed rate fee added in Viridian
            TempFee = (BrokerFee.FixedRate * ItemMarketCost) + (SCCBrokerFeeSurcharge * ItemMarketCost)
        Else
            Return 0
        End If

        If TempFee < 100 Then
            Return 100
        Else
            Return TempFee
        End If

    End Function

    ' Get Broker Fee data
    Public Function GetBrokerFeeData(ByVal BrokerCheck As CheckBox, SpecialRateBox As TextBox) As BrokerFeeInfo
        Dim TempBF As BrokerFeeInfo
        If BrokerCheck.CheckState = CheckState.Indeterminate Then
            ' Get the special fee
            TempBF.FixedRate = FormatManualPercentEntry(SpecialRateBox.Text)
            TempBF.IncludeFee = BrokerFeeType.SpecialFee
        ElseIf BrokerCheck.CheckState = CheckState.Checked Then
            TempBF.IncludeFee = BrokerFeeType.Fee
        Else
            TempBF.IncludeFee = BrokerFeeType.NoFee
        End If

        Return TempBF

    End Function

#End Region

#Region "Character Loading"

    ' Loads the character for the program
    Public Sub LoadCharacter(RefreshAssets As Boolean, RefreshBPs As Boolean)
        On Error GoTo 0

        ' Try to load the character
        If Not SelectedCharacter.LoadDefaultCharacter(RefreshBPs, RefreshAssets) Then

            ' Didn't find a default character. Either we don't have one selected or there are no characters in the DB yet
            Dim CMDCount As New SQLiteCommand("SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> " & CStr(DummyCharacterID), EVEDB.DBREf)

            If CInt(CMDCount.ExecuteScalar()) = 0 Then
                ' No characters loaded yet so load dummy for all
                Call SelectedCharacter.LoadDummyCharacter(True)
            Else
                ' Have a set of chars, need to set a default, open that form
                Dim f2 = New frmSetCharacterDefault
                f2.ShowDialog()
            End If
        End If

    End Sub

    ' Loads a default character from name sent
    Public Sub LoadCharacter(CharacterName As String, Optional PlaySound As Boolean = True)

        ' Load only if a new character
        If SelectedCharacter.Name <> CharacterName Then
            ' Update them all to 0 first
            Call EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = 0")
            Call EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = " & CStr(DefaultCharacterCode) & " WHERE CHARACTER_NAME = '" & FormatDBString(CharacterName) & "'")

            ' Load the character as default for program and reload additional API data
            Call SelectedCharacter.LoadDefaultCharacter(UserApplicationSettings.LoadAssetsonStartup, UserApplicationSettings.LoadBPsonStartup)
            If PlaySound Then
                Call PlayNotifySound()
            End If
        End If

    End Sub

#End Region

#Region "Time Functions"

    ' Converts a time in d h m s to a long of seconds - 3d 12h 2m 33s or 1 Day 12:23:33
    Public Function ConvertDHMSTimetoSeconds(ByVal SentTime As String) As Long
        Dim Days As Long = 0
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

    ' Checks entry of percentage chars in keypress
    Public Function CheckPercentCharEntry(ke As KeyPressEventArgs, box As TextBox) As Boolean
        Dim Istr As String = box.Text

        ' Only allow numbers or backspace
        If ke.KeyChar <> ControlChars.Back Then
            If allowedNegativePercentChars.IndexOf(ke.KeyChar) = -1 Then
                ' Invalid Character
                Return True
            ElseIf (Asc(ke.KeyChar) = 45 And Istr.Contains("-")) Or (Asc(ke.KeyChar) = 37 And Istr.Contains("%")) Or (Asc(ke.KeyChar) = 46 And Istr.Contains(".")) Then ' If the dash, percent, or period is pressed again, don't accept
                Return True
            End If
        End If

        Return False

    End Function

    ' Formats the manual entry of a percent in a string
    Public Function FormatManualPercentEntry(Entry As String) As Double
        Dim EntryText As String

        If Entry.Contains("%") Then
            ' Strip the percent first
            EntryText = Entry.Substring(0, Len(Entry) - 1)
        Else
            EntryText = Entry
        End If

        If IsNumeric(EntryText) Then
            Return CDbl(EntryText) / 100
        Else
            Return 0
        End If

    End Function

    ' Returns the sent text box text as a percent in string format
    Public Function GetFormattedPercentEntry(RefTextbox As TextBox) As String
        Return FormatPercent(FormatManualPercentEntry(RefTextbox.Text), 1)
    End Function

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

    ' Returns the price of the typeID sent in item_prices
    Public Function GetItemPrice(ByVal TypeID As Long) As Double
        Dim readerCost As SQLiteDataReader
        Dim SQL As String
        Dim ItemPrice As Double = 0

        ' Look up the cost for the material
        SQL = "SELECT PRICE FROM ITEM_PRICES WHERE ITEM_ID =" & TypeID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerCost = DBCommand.ExecuteReader

        If readerCost.Read Then
            ItemPrice = readerCost.GetDouble(0)
        End If

        readerCost.Close()

        Return ItemPrice

    End Function

    ' Sorts the reference listview and column
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ColumnIndex"></param>
    ''' <param name="RefListView"></param>
    ''' <param name="ListPrevColumnClicked"></param>
    ''' <param name="ListPrevColumnSortOrder"></param>
    ''' <param name="UseSentSortType"></param>
    Public Sub ListViewColumnSorter(ByVal ColumnIndex As Integer, ByRef RefListView As ListView, ByRef ListPrevColumnClicked As Integer, ByRef ListPrevColumnSortOrder As SortOrder,
                                    Optional UseSentSortType As Boolean = False)
        Dim SortType As SortOrder

        Application.UseWaitCursor = True
        Application.DoEvents()

        ' Figure out sort order
        If Not UseSentSortType Then
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
        Else
            SortType = ListPrevColumnSortOrder
        End If

        ' Perform the sort with these new sort options.
        If ColumnIndex > RefListView.Columns.Count - 1 Then
            ColumnIndex = 0
        End If

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

        Dim TempString As String = ""
        Dim TempNumber As String = ""

        ' Format of imported text for items will always be: Name, Quantity, Group, Category, Size, Slot, Volume, Meta Level, Tech Level, Est. Price
        ' Users can remove columns but the general rule is Name and quantity first, they can separate lines by three ways
        If SentText.Contains(vbCrLf) Then
            ItemLines = SentText.Split(New [Char]() {CChar(vbCrLf)}, StringSplitOptions.RemoveEmptyEntries) ' Get all the item lines
        ElseIf SentText.Contains(vbCr) Then
            ItemLines = SentText.Split(New [Char]() {CChar(vbCr)}, StringSplitOptions.RemoveEmptyEntries) ' Get all the item lines
        ElseIf SentText.Contains(vbLf) Then
            ItemLines = SentText.Split(New [Char]() {CChar(vbLf)}, StringSplitOptions.RemoveEmptyEntries) ' Get all the item lines
        Else
            ' Add a vbcrlf to the end and then split - only one line
            SentText &= vbCrLf
            ItemLines = SentText.Split(New [Char]() {CChar(vbCrLf)}, StringSplitOptions.RemoveEmptyEntries)
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
                    TempString = ""
                    TempNumber = ""

                    For j = 0 To ItemColumns.Count - 1
                        If Not IsNumeric(ItemColumns(j)) Then
                            TempString = TempString & ItemColumns(j) & " "
                        Else
                            TempNumber = TempNumber & ItemColumns(j)
                        End If
                    Next

                    'Now reset the Item columns
                    ReDim ItemColumns(1)
                    ItemColumns(0) = Trim(TempString)
                    ItemColumns(1) = Trim(TempNumber)

                ElseIf ItemLines(i).Contains(",") Then ' Keep this to final case
                    ItemColumns = ItemLines(i).Split(New [Char]() {","c})
                Else
                    GoTo SkipItem ' Don't process
                    'Dim itemcolumns As String() = ItemLines(i).Split(New String() {"   "}, StringSplitOptions.RemoveEmptyEntries)
                End If

                ' If the item has a comma after it, strip it off
                If ItemColumns(0).Substring(Len(ItemColumns(0)) - 1, 1) = "," Then
                    ItemColumns(0) = ItemColumns(0).Substring(0, Len(ItemColumns(0)) - 1)
                End If

                SQL = "SELECT typeID FROM INVENTORY_TYPES WHERE typeName = '" & FormatDBString(ItemColumns(0)) & "'"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerItem = DBCommand.ExecuteReader
                readerItem.Read()

                If readerItem.HasRows Then
                    ' If the itemcolumns doesn't have a number, add it
                    If ItemColumns.Count = 1 Then
                        TempString = ItemColumns(0)
                        ReDim ItemColumns(1)
                        ItemColumns(0) = TempString
                        ItemColumns(1) = ""
                    End If

                    ItemColumns(0) = Trim(ItemColumns(0))
                    ItemColumns(1) = Trim(ItemColumns(1))

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
                readerItem.Close()
SkipItem:
                readerItem = Nothing

            Next
        End If

        Return CopyPasteMaterials

    End Function

    Public Function UpdateItemNamewithRuns(ByVal ItemName As String, ByVal Runs As Long) As String
        ' Update built item name with runs we did to get this quantity
        ' Reset item name for found item
        If ItemName.Contains("(") Then
            ItemName = Trim(ItemName.Substring(0, InStr(ItemName, "(") - 1))
        End If

        Return ItemName & " (Runs: " & FormatNumber(Runs, 0) & ")"

    End Function

    ' Strips off the Runs if it is on the name
    Public Function RemoveItemNameRuns(ByVal ItemName As String) As String
        If ItemName.Contains("(Runs:") Then
            Return Trim(ItemName.Substring(0, InStr(ItemName, "(") - 2))
        Else
            Return ItemName
        End If
    End Function

    Public Function GetBPUserID(SentUserID As Long) As Long
        Dim BPUserID As Long

        If UserApplicationSettings.LoadBPsbyChar Then
            ' Use the ID sent
            BPUserID = SentUserID
        Else
            BPUserID = CommonLoadBPsID
        End If

        Return BPUserID

    End Function

    ' Imports sent blueprint to shopping list
    Public Sub AddToShoppingList(SentBlueprint As Blueprint, BuildBuy As Boolean, CopyRawMats As Boolean, SLFacility As IndustryFacility,
                                 IgnoreInvention As Boolean, IgnoreMinerals As Boolean, IgnoreT1ITem As Boolean,
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
                    .Runs = SentBlueprint.GetItemData.GetQuantity
                    .ItemME = SentBlueprint.GetME
                    .ItemTE = SentBlueprint.GetTE
                    .PortionSize = SentBlueprint.GetPortionSize

                    .ManufacturingFacility = SentBlueprint.GetManufacturingFacility
                    .ComponentManufacturingFacility = SentBlueprint.GetComponentManufacturingFacility
                    .ReactionFacility = SentBlueprint.GetReactionFacility

                    If BuildBuy Then
                        .BuildType = "Build/Buy"
                    Else
                        ' Just insert the materials in components since we are building all
                        .BuildType = "Raw Mats"
                    End If

                    If Not CopyInventionMatsOnly Then
                        ShoppingBuyList = CType(SentBlueprint.GetRawMaterials.Clone, Materials) ' Need a deep copy because we might insert later
                        ShoppingBuildList = CType(SentBlueprint.BuiltComponentList.Clone, BuiltItemList)
                    End If

                    ' Total up all usage
                    .TotalUsage = SentBlueprint.GetManufacturingFacilityUsage + SentBlueprint.GetComponentFacilityUsage + SentBlueprint.GetCapComponentFacilityUsage _
                        + SentBlueprint.GetInventionUsage + SentBlueprint.GetCopyUsage + SentBlueprint.GetReactionFacilityUsage

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
                    .Runs = SentBlueprint.GetItemData.GetQuantity
                    .ItemME = SentBlueprint.GetME
                    .ItemTE = SentBlueprint.GetTE
                    .ManufacturingFacility = SentBlueprint.GetManufacturingFacility
                    .ComponentManufacturingFacility = SentBlueprint.GetComponentManufacturingFacility
                    .ReactionFacility = SentBlueprint.GetReactionFacility
                    .PortionSize = SentBlueprint.GetPortionSize
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

            If SentBlueprint.GetTechLevel = BPTechLevel.T2 Or SentBlueprint.GetTechLevel = BPTechLevel.T3 Then
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

    Public Function BPHasProcRawMats(BPID As Integer, MatType As BuildMatType) As Boolean
        Dim SQL As String
        Dim readerBP As SQLiteDataReader

        If MatType = BuildMatType.AdvMaterials Then
            ' Don't process for advanced since they don't want to drill down for reactions, etc.
            Return False
        End If

        SQL = "SELECT 'X' FROM ALL_BLUEPRINT_MATERIALS_FACT WHERE PRODUCT_ID IN "
        SQL &= "(SELECT ITEM_ID FROM ALL_BLUEPRINTS_FACT WHERE ITEM_ID IN "
        SQL &= "(SELECT MATERIAL_ID FROM ALL_BLUEPRINT_MATERIALS_FACT WHERE BLUEPRINT_ID = {0})) "

        If MatType = BuildMatType.ProcessedMaterials Then
            SQL &= "AND MATERIAL_GROUP_ID IN (429,712)"
        ElseIf MatType = BuildMatType.RawMaterials Then
            SQL &= "AND MATERIAL_GROUP_ID IN (428,429,711,712,974)"
        End If

        Try
            DBCommand = New SQLiteCommand(String.Format(SQL, BPID), EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader

            If readerBP.Read Then
                readerBP.Close()
                Return True
            End If

            readerBP.Close()
            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function

    ' After a price update in any location that updates prices, we want to refresh all the prices and grids on every tab 
    Public Sub UpdateProgramPrices(Optional ByVal RefreshUpdatePriceList As Boolean = True)

        ' Update the Update Prices tab
        If RefreshUpdatePriceList Then
            Call frmMain.UpdatePriceList()
        End If

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

        ' Refresh the prices in manual update for minerals
        Call frmManualPriceUpdate.LoadMineralPrices()

        ' Manual update of moon materials
        Call frmManualPriceUpdate.LoadMoonPrices()

        ' Reload the prices on the reprocessing plant if open
        If Application.OpenForms().OfType(Of frmReprocessingPlant).Any Then
            frmRepoPlant.RefreshMaterialList()
        End If

        ' Refill the search grid on manual updates
        If Trim(frmManualPriceUpdate.lblLSelectedItem.Text) <> "" Then
            Call frmManualPriceUpdate.FillSearchGrid(frmManualPriceUpdate.lblSelectedItem.Text)
        End If

    End Sub

    ' Function to get the regionID from the name sent
    Public Function GetRegionID(ByVal RegionName As String) As Integer
        Dim readerRegion As SQLiteDataReader
        Dim ReturnID As Integer

        ' Get the region ID
        DBCommand = New SQLiteCommand("SELECT regionID FROM REGIONS WHERE regionName ='" & FormatDBString(RegionName) & "'", EVEDB.DBREf)
        readerRegion = DBCommand.ExecuteReader

        If readerRegion.Read Then
            ReturnID = readerRegion.GetInt32(0)
        Else
            ReturnID = 0
        End If

        readerRegion.Close()

        Return ReturnID

    End Function

    ' Function to get the regionID from systemid sent
    Public Function GetRegionID(ByVal solarSystemID As Integer) As Integer
        Dim readerRegion As SQLiteDataReader
        Dim ReturnID As Integer

        ' Get the region ID
        DBCommand = New SQLiteCommand("SELECT regionID FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(solarSystemID), EVEDB.DBREf)
        readerRegion = DBCommand.ExecuteReader

        If readerRegion.Read Then
            ReturnID = readerRegion.GetInt32(0)
        Else
            ReturnID = 0
        End If

        readerRegion.Close()

        Return ReturnID

    End Function

    ' Sets the default character to the character name sent
    Public Sub SetDefaultCharacter(ByVal CharacterName As String)
        ' If we get here, just clear out the old default and set the new one
        Call LoadCharacter(CharacterName, False)
        ' Refresh all screens
        If Application.OpenForms().OfType(Of frmMain).Any Then
            Call frmMain.ResetTabs()
        End If

        ' Reset any of the characterids
        Call frmMain.ResetCharacterIDonFacilties()

        DefaultCharSelected = True
        MsgBox(CharacterName & " selected as Default Character", vbInformation, Application.ProductName)

    End Sub

    ' Function to get the regionID from the name sent
    Public Function GetRegionName(ByVal RegionID As Integer) As String
        Dim readerRegion As SQLiteDataReader
        Dim ReturnName As String

        ' Get the region ID
        DBCommand = New SQLiteCommand("SELECT regionName FROM REGIONS WHERE regionID = " & CStr(RegionID), EVEDB.DBREf)
        readerRegion = DBCommand.ExecuteReader

        If readerRegion.Read Then
            ReturnName = readerRegion.GetString(0)
        Else
            ReturnName = ""
        End If

        readerRegion.Close()

        Return ReturnName

    End Function

    ' Returns the SQL for getting item price typeids = and empty string if nothing selected
    Public Function GetItemPriceGroupListSQL(AdvancedComponents As CheckBox, AdvancedMats As CheckBox, AdvancedProtectiveTechnology As CheckBox, AncientRelics As CheckBox,
                                             BoosterMats As CheckBox, Boosters As CheckBox, BPCs As CheckBox, CapitalShipComponents As CheckBox,
                                             CapT2ShipComponents As CheckBox, Celestials As CheckBox, Charges As CheckBox,
                                             Datacores As CheckBox, Decryptors As CheckBox, Deployables As CheckBox, Drones As CheckBox,
                                             FactionMaterials As CheckBox, FuelBlocks As CheckBox, Gas As CheckBox, IceProducts As CheckBox,
                                             Implants As CheckBox, Minerals As CheckBox, Misc As CheckBox, Modules As CheckBox, MolecularForgedMaterials As CheckBox,
                                             MolecularForgingTools As CheckBox, NamedComponents As CheckBox, Planetary As CheckBox,
                                             Polymers As CheckBox, ProcessedMats As CheckBox, ProtectiveComponents As CheckBox, RAM As CheckBox,
                                             RawMaterials As CheckBox, RawMoonMats As CheckBox, RDb As CheckBox, Rigs As CheckBox, Salvage As CheckBox,
                                             Ships As CheckBox, StructureComponents As CheckBox, StructureModules As CheckBox, StructureRigs As CheckBox,
                                             Structures As CheckBox, SubsystemComponents As CheckBox, Subsystems As CheckBox,
                                             ChargeTypes As ComboBox, ShipTypes As ComboBox,
                                             PricesT1 As CheckBox, PriceCheckT1Enabled As Boolean,
                                             PricesT2 As CheckBox, PriceCheckT2Enabled As Boolean,
                                             PricesT3 As CheckBox, PriceCheckT3Enabled As Boolean,
                                             PricesT4 As CheckBox, PriceCheckT4Enabled As Boolean,
                                             PricesT5 As CheckBox, PriceCheckT5Enabled As Boolean,
                                             PricesT6 As CheckBox, PriceCheckT6Enabled As Boolean, NoBuildItems As CheckBox) As String

        Dim SQL As String = ""
        Dim TechSQL As String = ""
        Dim ItemChecked As Boolean = False
        Dim TechChecked As Boolean = False

        ' Materials & Research Equipment Grid
        ' Materials First
        If AdvancedProtectiveTechnology.Checked Then
            SQL &= "ITEM_GROUP = 'Advanced Protective Technology' OR "
            ItemChecked = True
        End If
        If FactionMaterials.Checked Then
            SQL &= "(ITEM_GROUP IN ('Materials and Compounds','Artifacts and Prototypes','Rogue Drone Components') OR ITEM_GROUP LIKE 'Decryptors -%') OR "
            ItemChecked = True
        End If
        If Gas.Checked Then
            SQL &= "ITEM_GROUP IN ('Harvestable Cloud','Compressed Gas') OR "
            ItemChecked = True
        End If
        If IceProducts.Checked Then
            SQL &= "ITEM_GROUP = 'Ice Product' OR "
            ItemChecked = True
        End If
        If Minerals.Checked Then
            SQL &= "ITEM_GROUP = 'Mineral' OR "
            ItemChecked = True
        End If
        If MolecularForgingTools.Checked Then
            SQL &= "ITEM_GROUP = 'Molecular-Forging Tools' OR "
            ItemChecked = True
        End If
        If NamedComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Named Components' OR "
            ItemChecked = True
        End If
        If Planetary.Checked Then
            SQL &= "ITEM_CATEGORY LIKE 'Planetary%' OR "
            ItemChecked = True
        End If

        ' Raw Materials (Ores)
        If RawMaterials.Checked Then
            SQL &= "(ITEM_CATEGORY = 'Asteroid' OR ITEM_GROUP = 'Abyssal Materials') OR "
            ItemChecked = True
        End If

        ' Reaction Materials
        If AdvancedMats.Checked Then
            SQL &= "ITEM_GROUP = 'Composite' OR "
            ItemChecked = True
        End If
        If BoosterMats.Checked Then
            SQL &= "ITEM_GROUP = 'Biochemical Material' OR "
            ItemChecked = True
        End If
        If MolecularForgedMaterials.Checked Then
            SQL &= "ITEM_GROUP = 'Molecular-Forged Materials' OR "
            ItemChecked = True
        End If
        If Polymers.Checked Then
            SQL &= "ITEM_GROUP = 'Hybrid Polymers' OR "
            ItemChecked = True
        End If
        If ProcessedMats.Checked Then
            SQL &= "ITEM_GROUP = 'Intermediate Materials' OR "
            ItemChecked = True
        End If
        If RawMoonMats.Checked Then
            SQL &= "ITEM_GROUP = 'Moon Materials' OR "
            ItemChecked = True
        End If

        If Salvage.Checked Then
            SQL &= "ITEM_GROUP IN ('Salvaged Materials','Ancient Salvage') OR "
            ItemChecked = True
        End If

        ' Research Equipment
        If AncientRelics.Checked Then
            SQL &= "ITEM_CATEGORY = 'Ancient Relics' OR "
            ItemChecked = True
        End If
        If Datacores.Checked Then
            SQL &= "ITEM_GROUP = 'Datacores' OR "
            ItemChecked = True
        End If
        If Decryptors.Checked Then
            SQL &= "ITEM_CATEGORY = 'Decryptors' OR "
            ItemChecked = True
        End If
        If RDb.Checked Then
            SQL &= "ITEM_NAME LIKE 'R.Db%' OR "
            ItemChecked = True
        End If

        ' Misc and Blueprints
        If BPCs.Checked Then
            SQL &= "ITEM_CATEGORY = 'Blueprint' OR "
            ItemChecked = True
        End If
        If Misc.Checked Then ' Commodities = Shattered Villard Wheel
            SQL &= "ITEM_GROUP IN ('General','Livestock','Radioactive','Biohazard','Commodities','Empire Insignia Drops','Criminal Tags','Miscellaneous','Unknown Components','Lease') OR "
            ItemChecked = True
        End If

        ' Other Manufacturables
        If CapT2ShipComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Advanced Capital Construction Components' OR "
            ItemChecked = True
        End If
        If AdvancedComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Construction Components' OR "
            ItemChecked = True
        End If
        If FuelBlocks.Checked Then
            SQL &= "ITEM_GROUP = 'Fuel Block' OR "
            ItemChecked = True
        End If
        If ProtectiveComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Protective Components' OR "
            ItemChecked = True
        End If
        If RAM.Checked Then
            SQL &= "ITEM_NAME LIKE 'R.A.M.%' OR "
            ItemChecked = True
        End If
        If NoBuildItems.Checked Then
            SQL &= "MANUFACTURE = -1 OR "
            ItemChecked = True
        End If
        If CapitalShipComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Capital Construction Components' OR "
            ItemChecked = True
        End If
        If StructureComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Structure Components' OR "
            ItemChecked = True
        End If
        If SubsystemComponents.Checked Then
            SQL &= "ITEM_GROUP = 'Hybrid Tech Components' OR "
            ItemChecked = True
        End If
        If Boosters.Checked Then
            SQL &= "ITEM_GROUP = 'Booster' OR "
            ItemChecked = True
        End If

        ' All other manufactured items
        If Implants.Checked Then
            SQL &= "(ITEM_GROUP = 'Cyberimplant' OR (ITEM_CATEGORY = 'Implant' AND ITEM_GROUP <> 'Booster')) OR "
            ItemChecked = True
        End If
        If Deployables.Checked Then
            SQL &= "ITEM_CATEGORY = 'Deployable' OR "
            ItemChecked = True
        End If
        If StructureModules.Checked Then
            SQL &= "(ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP NOT LIKE '%Rig%') OR "
            ItemChecked = True
        End If
        If Celestials.Checked Then
            SQL &= "(ITEM_CATEGORY IN ('Celestial','Orbitals','Sovereignty Structures','Station','Accessories','Infrastructure Upgrades')  AND ITEM_GROUP NOT IN ('Harvestable Cloud','Compressed Gas')) OR "
            ItemChecked = True
        End If

        ' Manufactured Items
        If Ships.Checked Or Modules.Checked Or Drones.Checked Or Rigs.Checked Or Subsystems.Checked Or Structures.Checked Or Charges.Checked Or StructureRigs.Checked Then

            ' If they choose a tech level, then build this part of the SQL query
            If PriceCheckT1Enabled Then
                If PricesT1.Checked Then
                    ' Add to SQL query for tech level
                    TechSQL = TechSQL & "ITEM_TYPE = 1 OR "
                    TechChecked = True
                End If
            End If

            If PriceCheckT2Enabled Then
                If PricesT2.Checked Then
                    ' Add to SQL query for tech level
                    TechSQL = TechSQL & "ITEM_TYPE = 2 OR "
                    TechChecked = True
                End If
            End If

            If PriceCheckT3Enabled Then
                If PricesT3.Checked Then
                    ' Add to SQL query for tech level
                    TechSQL = TechSQL & "ITEM_TYPE = 14 OR "
                    TechChecked = True
                End If
            End If

            ' Add the Pirate, Storyline, Navy search string
            ' Storyline
            If PriceCheckT4Enabled Then
                If PricesT4.Checked Then
                    ' Add to SQL query for tech level
                    TechSQL = TechSQL & "ITEM_TYPE = 3 OR "
                    TechChecked = True
                End If
            End If

            ' Navy
            If PriceCheckT5Enabled Then
                If PricesT5.Checked Then
                    ' Add to SQL query for tech level
                    TechSQL = TechSQL & "ITEM_TYPE = 16 OR "
                    TechChecked = True
                End If
            End If

            ' Pirate
            If PriceCheckT6Enabled Then
                If PricesT6.Checked Then
                    ' Add to SQL query for tech level
                    TechSQL = TechSQL & "ITEM_TYPE = 15 OR "
                    TechChecked = True
                End If
            End If

            If Not TechChecked And Not ItemChecked Then
                ' There isn't an item checked before this and these items all require tech, so exit
                Return ""
            End If

            ' Format TechSQL - Add on Meta codes - 21,22,23,24 are T3
            If TechSQL <> "" Then
                TechSQL = "(" & TechSQL.Substring(0, TechSQL.Length - 3) & "OR ITEM_TYPE IN (21,22,23,24)) "
            End If

            ' Build Tech 1,2,3 Manufactured Items
            If Charges.Checked Then
                SQL &= "(ITEM_CATEGORY = 'Charge' AND " & TechSQL
                If ChargeTypes.Text <> "All Charge Types" Then
                    SQL &= " AND ITEM_GROUP = '" & ChargeTypes.Text & "'"
                End If
                SQL &= ") OR "
            End If
            If Drones.Checked Then
                SQL &= "(ITEM_CATEGORY IN ('Drone', 'Fighter') AND " & TechSQL & ") OR "
            End If
            If Modules.Checked Then ' Not rigs but Modules
                SQL &= "(ITEM_CATEGORY = 'Module' AND ITEM_GROUP NOT LIKE 'Rig%' AND " & TechSQL & ") OR "
            End If
            If Ships.Checked Then
                SQL &= "(ITEM_CATEGORY = 'Ship' AND " & TechSQL
                If ShipTypes.Text <> "All Ship Types" Then
                    SQL &= " AND ITEM_GROUP = '" & ShipTypes.Text & "'"
                End If
                SQL &= ") OR "
            End If
            If Subsystems.Checked Then
                SQL &= "(ITEM_CATEGORY = 'Subsystem' AND " & TechSQL & ") OR "
            End If
            If StructureRigs.Checked Then
                SQL &= "(ITEM_CATEGORY = 'Structure Rigs' AND " & TechSQL & ") OR "
            End If
            If Rigs.Checked Then ' Rigs
                SQL &= "((ITEM_CATEGORY = 'Module' AND ITEM_GROUP LIKE 'Rig%' AND " & TechSQL & ") OR (ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP LIKE '%Rig%')) OR "
            End If
            If Structures.Checked Then
                SQL &= "((ITEM_CATEGORY IN ('Starbase','Structure') AND " & TechSQL & ") OR ITEM_GROUP = 'Station Components') OR "
            End If
        End If

        ' Take off last OR and add the final )
        SQL = SQL.Substring(0, SQL.Length - 4)

        Return SQL

    End Function

    Public Function GetSolarSystemID(ByVal SystemName As String) As Integer
        ' Look up Solar System ID
        Dim rsSystem As SQLiteDataReader
        Dim SSID As Integer

        DBCommand = New SQLiteCommand("SELECT solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & SystemName & "'", EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            SSID = rsSystem.GetInt32(0)
        Else
            SSID = 0
        End If

        rsSystem.Close()

        Return SSID

    End Function

    Public Function GetSolarSystemName(ByVal SystemID As Long) As String
        ' Look up Solar System Name
        Dim rsSystem As SQLiteDataReader
        Dim SSName As String

        DBCommand = New SQLiteCommand("SELECT solarSystemName FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(SystemID), EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            SSName = rsSystem.GetString(0)
        Else
            SSName = ""
        End If

        rsSystem.Close()

        Return SSName

    End Function

    Public Function GetSolarSystemSecurityLevel(ByVal SystemName As String) As Double
        ' Look up Solar System ID
        Dim rsSystem As SQLiteDataReader
        Dim security As Double

        DBCommand = New SQLiteCommand("SELECT SECURITY FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SystemName) & "'", EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            security = rsSystem.GetDouble(0)
        Else
            security = Nothing
        End If

        rsSystem.Close()

        Return security

    End Function

    Public Function GetSolarSystemSecurityLevel(ByVal SystemID As Long) As Double
        ' Look up Solar System ID
        Dim rsSystem As SQLiteDataReader
        Dim security As Double

        DBCommand = New SQLiteCommand("SELECT SECURITY FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(SystemID), EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            security = rsSystem.GetDouble(0)
        Else
            security = Nothing
        End If

        rsSystem.Close()

        Return security

    End Function

    Public Function GetActivityID(ByVal ActivityName As String) As Integer
        ' Look up the Activity ID for the ID sent
        Dim rsSystem As SQLiteDataReader
        Dim AID As Integer

        DBCommand = New SQLiteCommand("SELECT activityID FROM INDUSTRY_ACTIVITIES WHERE UPPER(activityName) = '" & UCase(ActivityName) & "'", EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            AID = rsSystem.GetInt32(0)
        Else
            AID = 0
        End If

        rsSystem.Close()

        Return AID

    End Function

    Public Function GetActivityName(ByVal ActivityID As Integer) As String
        ' Look up the Activity Name for the ID sent
        Dim rsSystem As SQLiteDataReader
        Dim AName As String

        DBCommand = New SQLiteCommand("SELECT activityName FROM INDUSTRY_ACTIVITIES WHERE activityID = " & CStr(ActivityID), EVEDB.DBREf)
        rsSystem = DBCommand.ExecuteReader

        If rsSystem.Read() Then
            AName = rsSystem.GetString(0)
        Else
            AName = ""
        End If

        rsSystem.Close()

        Return AName

    End Function

    ' Loads a referenced combobox with regions
    Public Sub LoadRegionCombo(ByRef RegionCombo As ComboBox, ByVal DefaultRegionName As String)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader

        SQL = "SELECT regionName FROM REGIONS WHERE (regionName NOT LIKE '%-R%' OR regionName = 'G-R00031') "
        SQL &= "AND regionName NOT IN ('A821-A','J7HZ-F','PR-01','UUA-F4') AND regionName NOT LIKE 'ADR%' GROUP BY regionName "
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader
        RegionCombo.BeginUpdate()
        RegionCombo.Items.Clear()
        While rsData.Read
            RegionCombo.Items.Add(rsData.GetString(0))
        End While
        RegionCombo.EndUpdate()
        RegionCombo.Text = DefaultRegionName

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

    ' Check for Fulcrum bonus - if it's an angel or gurista's subcap and they are using Fulcrum station, return true
    Public Function GetFulcrumBonusFlagforItem(ByVal FacilityID As Long, ByVal BlueprintID As Long) As Boolean

        If FacilityID = 60015187 Then
            Dim rsShip As SQLiteDataReader
            Dim SQL As String

            ' Get the ItemID from the BP ID
            Dim ItemID As Long = 0
            DBCommand = New SQLiteCommand("SELECT ITEM_ID FROM ALL_BLUEPRINTS_FACT WHERE BLUEPRINT_ID = " & CStr(BlueprintID), EVEDB.DBREf)
            rsShip = DBCommand.ExecuteReader
            If rsShip.Read Then
                ItemID = rsShip.GetInt64(0)
            End If

            SQL = "SELECT 'X' FROM INVENTORY_GROUPS, INVENTORY_TYPES WHERE typeID = {0} AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
            SQL &= "AND categoryID = 6 AND factionID IN (500010,500011) AND INVENTORY_GROUPS.groupID NOT IN (883, 547, 485, 1538, 513, 902, 30) " ' No caps

            DBCommand = New SQLiteCommand(String.Format(SQL, ItemID), EVEDB.DBREf)
            rsShip = DBCommand.ExecuteReader

            If rsShip.HasRows Then
                rsShip.Close()
                Return True
            End If
            rsShip.Close()
        End If

        Return False

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
        Dim TempValue As String = EUFormatedValue

        ' EU string can be 1.000.000,00 or 1000000,00
        If EUFormatedValue.Contains(",") Then
            ' This is the EU decimal so change to US version
            TempValue = EUFormatedValue.Replace(",", ".")
        End If

        Return TempValue

    End Function

    Public Function ConvertPriceHistoryEUDecimal(ByVal HistoryValue As String) As String
        Dim TempValue As String = HistoryValue

        If Len(HistoryValue) > 2 Then
            If HistoryValue.Substring(Len(HistoryValue) - 3, 1) = "," Then
                ' EU value, so convert remove the decimals
                TempValue = HistoryValue.Replace(".", "")
                TempValue = TempValue.Replace(",", ".")
            End If
            ' Both formats need commas removed if there are any
            TempValue = TempValue.Replace(",", "")
        End If

        Return TempValue

    End Function

    ' MD5 Hash - specify the path to a file and this routine will calculate your hash
    Public Function MD5CalcFile(ByVal filepath As String) As String

        ' Open file (as read-only) - If it's not there, return ""
        If File.Exists(filepath) Then
            Using reader As New FileStream(filepath, FileMode.Open, FileAccess.Read)
                Using md5 As New MD5CryptoServiceProvider

                    ' hash contents of this stream
                    Dim hash() As Byte = md5.ComputeHash(reader)
                    Dim sb As New Text.StringBuilder(hash.Length * 2)

                    For i As Integer = 0 To hash.Length - 1
                        sb.Append(hash(i).ToString("X2"))
                    Next

                    Return sb.ToString().ToLower

                End Using
            End Using
        End If

        ' Something went wrong
        Return ""

    End Function

    ' SHA Hash
    Public Function HashSHA(InputString As String) As String
        Try
            Dim sha512 As SHA512 = SHA512Managed.Create()
            Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(InputString)
            Dim hash As Byte() = sha512.ComputeHash(bytes)
            Dim stringBuilder As New Text.StringBuilder()

            For i As Integer = 0 To hash.Length - 1
                stringBuilder.Append(hash(i).ToString("X2"))
            Next

            Return stringBuilder.ToString()
        Catch ex As Exception
            Return ""
        End Try

    End Function

    ' Writes a sent message to a log file
    Public Sub WriteMsgToLog(ByVal ErrorMsg As String)
        Dim FilePath As String = Path.Combine(DynamicFilePath, "EVEIPH.log")
        Dim AllText() As String

        If Not File.Exists(FilePath) Then
            Dim sw As StreamWriter = File.CreateText(FilePath)
            sw.Close()
        End If

        ' This is an easier way to get all of the strings in the file.
        AllText = File.ReadAllLines(FilePath)
        ' This will append the string to the end of the file.
        My.Computer.FileSystem.WriteAllText(FilePath, CStr(Now) & ", " & ErrorMsg & Environment.NewLine & Environment.NewLine & "---" & Environment.NewLine, True)

    End Sub

    ' Function will take a string and return it in a DB friendly format - ie if it has single quotes in the string
    Public Function FormatDBString(ByVal inStrVar As String) As String
        ' Anything with quote mark in name it won't correctly load - need to replace with double quotes
        If InStr(inStrVar, "'") <> 0 Then
            inStrVar = Replace(inStrVar, "'", "''")
        End If
        Return inStrVar
    End Function

    ' Formats the value sent to what we want to insert into the table field
    Public Function BuildInsertFieldString(ByVal inValue As Object) As String
        Dim CheckNullValue As Object
        Dim OutputString As String

        ' See if it is null first
        CheckNullValue = CheckNull(inValue)

        If CStr(CheckNullValue) <> "null" Then
            ' Not null, so format
            If inValue.GetType.Name = "DateTime" Then
                OutputString = "'" & Format(inValue, SQLiteDateFormat) & "'"
            ElseIf inValue.GetType.Name <> "String" Then
                ' Just a value, so no quotes needed
                OutputString = CStr(inValue)
            Else
                ' String, so check for appostrophes and add quotes
                OutputString = "'" & FormatDBString(CStr(inValue)) & "'"
            End If
        Else
            OutputString = "NULL"
        End If

        Return OutputString

    End Function

    ' Returns the ore processing skill level from the objects on the tab for the ore name sent 
    Public Function GetFormOreProcessingSkill(ByVal OreName As String, Labels As Label(), Combos As ComboBox()) As Integer
        Dim i As Integer
        Dim CurrentProcessingLabel As String

        For i = 1 To Combos.Count - 1
            CurrentProcessingLabel = Labels(i).Text

            If Combos(i).Enabled = True And CBool(InStr(GetOreProcessingSkillName(OreName), CurrentProcessingLabel)) Then
                ' Found it, return value
                Return CInt(Combos(i).Text)
            End If
        Next

        Return 0

    End Function

    Public Function GetOreProcessingSkillName(OreName As String) As String
        Dim rsCheck As SQLiteDataReader
        Dim SQL As String
        Dim FoundOreName As String = ""

        SQL = "SELECT value FROM TYPE_ATTRIBUTES AS TA, INVENTORY_TYPES AS IT WHERE TA.typeID = IT.typeID AND attributeID = " & CStr(ItemAttributes.reprocessingSkillType)
        SQL &= " AND typeName = '" & OreName & "'"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCheck = DBCommand.ExecuteReader

        If rsCheck.Read Then
            FoundOreName = GetTypeName(CInt(rsCheck.GetDouble(0)))
        End If

        rsCheck.Close()

        Return FoundOreName

    End Function

    Private Function CheckNull(ByVal inVariable As Object) As Object
        If IsNothing(inVariable) Then
            Return "null"
        ElseIf DBNull.Value.Equals(inVariable) Then
            Return "null"
        Else
            Return inVariable
        End If
    End Function

    Public Function FormatNullInteger(ByVal inVariable As Object) As Integer
        If CStr(CheckNull(inVariable)) = "null" Then
            Return 0
        Else
            Return CInt(inVariable)
        End If
    End Function

    Public Function FormatNullLong(ByVal inVariable As Object) As Long
        If CStr(CheckNull(inVariable)) = "null" Then
            Return 0
        Else
            Return CLng(inVariable)
        End If
    End Function

    Public Function FormatNullDouble(ByVal inVariable As Object) As Double
        If CStr(CheckNull(inVariable)) = "null" Then
            Return 0
        Else
            Return CDbl(inVariable)
        End If
    End Function

    Public Function FormatNullDate(ByVal inVariable As Object) As Date
        If CStr(CheckNull(inVariable)) = "null" Then
            Return NoDate
        Else
            Return CDate(inVariable)
        End If
    End Function

    Public Function FormatNullString(ByVal inVariable As Object) As String
        If CStr(CheckNull(inVariable)) = "null" Then
            Return ""
        Else
            Return CStr(inVariable)
        End If
    End Function

    ' Finds the T1 material for a T2 blueprint
    Public Function GetT1Material(ByVal BlueprintID As Long) As Material
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader
        Dim TempMat As Material
        Dim T1BPID As Long = 0

        ' Look up the blueprint we used to invent from the sent blueprint ID
        SQL = "Select blueprintTypeID from INDUSTRY_ACTIVITY_PRODUCTS WHERE productTypeID = " & BlueprintID & " And activityID = 8"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader

        If readerLookup.Read() Then
            T1BPID = CLng(readerLookup.GetInt64(0))
            readerLookup.Close()

            ' Select all materials now
            SQL = "Select ITEM_ID, ITEM_NAME, ITEM_GROUP FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID =" & T1BPID

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerLookup = DBCommand.ExecuteReader

            readerLookup.Read()
            TempMat = New Material(readerLookup.GetInt64(0), readerLookup.GetString(1), readerLookup.GetString(2), 1, 1, 0, "0", "0")
            readerLookup.Close()

        Else
            readerLookup.Close()
            Return Nothing
        End If

        Return TempMat

    End Function

    ' Takes the BP ID and relic name (if sent) and returns the TypeID for the item to invent that BP
    Public Function GetInventItemTypeID(ByVal BlueprintTypeID As Long, ByVal RelicName As String) As Long
        Dim SQL As String
        Dim rsCheck As SQLiteDataReader
        Dim InventItemTypeID As Long = 0

        ' What is the item we are using to invent?
        SQL = "SELECT blueprintTypeID from INDUSTRY_ACTIVITY_PRODUCTS, INVENTORY_TYPES WHERE productTypeID = " & BlueprintTypeID & " "
        SQL &= "AND typeID = blueprintTypeID And activityID = 8"

        If RelicName <> "" Then
            ' Need to add the relic variant to the query for just one item
            SQL &= " And typeName Like '%" & RelicName & "%'"
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCheck = DBCommand.ExecuteReader

        If rsCheck.Read Then
            InventItemTypeID = rsCheck.GetInt64(0)
        End If

        rsCheck.Close()

        Return InventItemTypeID

    End Function

    ' Returns the text race for the ID sent
    Public Function GetRace(ByVal RaceID As Integer) As String
        Dim rsLookup As SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT RACE FROM RACE_IDS WHERE ID = " & CStr(RaceID), EVEDB.DBREf)
        rsLookup = DBCommand.ExecuteReader

        If rsLookup.Read Then
            Return rsLookup.GetString(0)
        Else
            Return ""
        End If

        rsLookup.Close()

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

    ' Deletes all the public structures from the stations table
    Public Sub ResetPublicStructureData()
        Dim SQL As String = "DELETE FROM STATIONS WHERE STATION_ID > 70000000 AND MANUAL_ENTRY = 0"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
    End Sub

    ' Try to catch exceptions when the clipboard errors for whatever reason
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

    Public Function GetBlueprintSQLWhereQuery(AmmoCharges As Boolean, Drones As Boolean, Modules As Boolean, Ships As Boolean,
                                              Subsystems As Boolean, Boosters As Boolean, Components As Boolean, Misc As Boolean,
                                              Deployables As Boolean, Celestials As Boolean, Structures As Boolean, StructureRigs As Boolean,
                                              StructureModules As Boolean, Reactions As Boolean, Rigs As Boolean) As String

        Dim ReturnClause As String = ""

        If AmmoCharges Then
            ReturnClause &= "ITEM_CATEGORY = 'Charge'"
        ElseIf Drones Then
            ReturnClause &= "ITEM_CATEGORY in ('Drone', 'Fighter')"
        ElseIf Modules Then
            ReturnClause &= "(ITEM_CATEGORY ='Module' AND ITEM_GROUP NOT LIKE 'Rig%')"
        ElseIf Ships Then
            ReturnClause &= "ITEM_CATEGORY = 'Ship'"
        ElseIf Subsystems Then
            ReturnClause &= "ITEM_CATEGORY = 'Subsystem'"
        ElseIf Boosters Then
            ReturnClause &= "ITEM_CATEGORY = 'Implant'"
        ElseIf Components Then
            ReturnClause &= "(ITEM_GROUP LIKE '%Components%' AND ITEM_GROUP <> 'Station Components')"
        ElseIf Misc Then
            ReturnClause &= "ITEM_GROUP IN ('Tool','Data Interfaces','Cyberimplant','Fuel Block')"
        ElseIf Deployables Then
            ReturnClause &= "ITEM_CATEGORY = 'Deployable'"
        ElseIf Celestials Then
            ReturnClause &= "ITEM_CATEGORY IN ('Celestial','Orbitals','Sovereignty Structures', 'Station', 'Accessories', 'Infrastructure Upgrades') "
        ElseIf Structures Then
            ReturnClause &= "(ITEM_CATEGORY IN ('Starbase','Structure') OR ITEM_GROUP = 'Station Components')"
        ElseIf StructureRigs Then
            ReturnClause &= "ITEM_CATEGORY = 'Structure Rigs' "
        ElseIf StructureModules Then
            ReturnClause &= "(ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP NOT LIKE '%Rig%')"
        ElseIf Reactions Then
            ReturnClause &= "BLUEPRINT_GROUP LIKE '%Reaction Formulas'"
        ElseIf Rigs Then
            ReturnClause &= "(BLUEPRINT_GROUP = 'Rig Blueprint' OR (ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP LIKE '%Rig%'))"
        End If

        Return ReturnClause

    End Function

    ' Deletes all data related to blueprints for the selected character and corporation they are part
    Public Sub ResetAllBPData()
        Dim SQL As String

        Dim CorpID As String = CStr(SelectedCharacter.CharacterCorporation.CorporationID)
        Dim IDList As String = "(" & CorpID & CStr(SelectedCharacter.ID) & "," & "," & CStr(CommonLoadBPsID) & ",0)"

        Call EVEDB.BeginSQLiteTransaction()

        SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID IN (" & CStr(SelectedCharacter.ID) & "," & CorpID & "," & CStr(CommonLoadBPsID) & ",0)"
        EVEDB.ExecuteNonQuerySQL(SQL)

        SQL = "UPDATE ESI_CHARACTER_DATA SET BLUEPRINTS_CACHE_DATE = '1900-01-01 00:00:00' WHERE CHARACTER_ID IN (" & CStr(SelectedCharacter.ID) & "," & CStr(CommonLoadBPsID) & ",0)"
        EVEDB.ExecuteNonQuerySQL(SQL)

        SQL = "UPDATE ESI_CORPORATION_DATA SET BLUEPRINTS_CACHE_DATE = '1900-01-01 00:00:00' WHERE CORPORATION_ID = " & CorpID
        EVEDB.ExecuteNonQuerySQL(SQL)

        Call EVEDB.CommitSQLiteTransaction()

        MsgBox("Blueprints reset", vbInformation, Application.ProductName)

    End Sub

    ' Sets an existing bp in the DB to the ME/TE or adds it if not in DB as a new owned blueprint - this is always due to user input, not API
    Public Function UpdateBPinDB(ByVal BPID As Long, ByVal bpME As Integer, ByVal bpTE As Integer, ByVal SentBPType As BPType,
                                ByVal OriginalME As Integer, ByVal OriginalTE As Integer,
                                Optional Favorite As Boolean = False,
                                Optional Ignore As Boolean = False,
                                Optional AdditionalCosts As Double = 0,
                                Optional RemoveAll As Boolean = False) As BPType
        Dim SQL As String
        Dim readerBP As SQLiteDataReader
        Dim rsMaxRuns As SQLiteDataReader
        Dim TempFavorite As String
        Dim TempIgnore As String
        Dim TempOwned As String
        Dim UpdatedBPType As BPType
        Dim BPName As String = ""
        Dim BPGroup As Integer = 0
        Dim UserRuns As Integer

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
            rsMaxRuns.Close()
        End If

        ' Look BP Name and group up
        DBCommand = New SQLiteCommand("SELECT typeName, groupID FROM INVENTORY_TYPES WHERE typeID = " & CStr(BPID), EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader
        If readerBP.Read() Then
            BPName = readerBP.GetString(0)
            BPGroup = readerBP.GetInt32(1)
        End If
        readerBP.Close()

        ' See what ID we use for character bps
        Dim CharID As Long = 0
        If UserApplicationSettings.LoadBPsbyChar Then
            ' Use the ID sent
            CharID = SelectedCharacter.ID
        Else
            CharID = CommonLoadBPsID
        End If

        EVEDB.BeginSQLiteTransaction()

        ' If they are setting to not owned, not updating the ME/TE and not saving favorite or ignore, then remove the bp
        If (UpdatedBPType = BPType.NotOwned And Favorite = False And Ignore = False) Or RemoveAll Then

            ' Look up the BP first to see if it is scanned
            SQL = "SELECT 'X' FROM OWNED_BLUEPRINTS "
            SQL &= "WHERE (USER_ID =" & CStr(CharID) & " Or USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
            SQL &= "AND BLUEPRINT_ID =" & CStr(BPID) & " AND SCANNED <> 0"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader
            readerBP.Read()

            ' If Found then update then just reset the owned flag - might be scanned
            If readerBP.HasRows Then
                ' Update it
                SQL = "UPDATE OWNED_BLUEPRINTS Set OWNED = 0, ME = 0, TE = 0, FAVORITE = 0, BP_TYPE = 0 "
                SQL &= "WHERE (USER_ID =" & CStr(CharID) & " Or USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
                SQL &= "And BLUEPRINT_ID =" & CStr(BPID)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            Else
                ' Just delete the record since it's not scanned
                SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID=" & CharID & " And BLUEPRINT_ID=" & BPID
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            End If

            ' Update the bp ignore flag (note for all accounts on this pc)
            SQL = "UPDATE ALL_BLUEPRINTS_FACT SET IGNORE = 0 WHERE BLUEPRINT_ID = " & CStr(BPID)
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

            ' For reactions, always set bpME and bpTE to zero because they can't be researched
            If BPGroup = 1888 Or BPGroup = 1889 Or BPGroup = 1890 Or BPGroup = 4097 Then
                bpME = 0
                bpTE = 0
            End If

            ' See if the BP is in the DB
            SQL = "SELECT TE FROM OWNED_BLUEPRINTS WHERE (USER_ID =" & CStr(CharID) & " OR USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
            SQL &= "AND BLUEPRINT_ID =" & CStr(BPID)

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader
            readerBP.Read()

            If Not readerBP.HasRows Then
                ' No record, So add it and mark as owned (code 2) - save the scanned data if it was scanned - no item id or location id (from API), so set to 0 on manual saves
                SQL = "INSERT INTO OWNED_BLUEPRINTS (USER_ID, ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, QUANTITY, FLAG_ID, "
                SQL &= "ME, TE, RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS) "
                SQL &= "VALUES (" & CharID & ",0,0," & BPID & ",'" & FormatDBString(BPName) & "',1,0,"
                SQL &= CStr(bpME) & "," & CStr(bpTE) & "," & CStr(UserRuns) & "," & CStr(UpdatedBPType) & "," & TempOwned & ",0," & TempFavorite & "," & CStr(AdditionalCosts) & ")"
                Call EVEDB.ExecuteNonQuerySQL(SQL)

            Else
                ' Update it
                SQL = "UPDATE OWNED_BLUEPRINTS SET ME = " & CStr(bpME) & ", TE = " & CStr(bpTE) & ", OWNED = " & TempOwned & ", FAVORITE = " & TempFavorite
                SQL &= ", ADDITIONAL_COSTS = " & CStr(AdditionalCosts) & ", BP_TYPE = " & CStr(UpdatedBPType) & ", RUNS = " & CStr(UserRuns) & " "
                SQL &= "WHERE (USER_ID =" & CStr(CharID) & " OR USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & ") "
                SQL &= "AND BLUEPRINT_ID =" & CStr(BPID)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            End If

            ' Update the bp ignore flag (note for all accounts on this pc)
            SQL = "UPDATE ALL_BLUEPRINTS_FACT SET IGNORE = " & TempIgnore & " WHERE BLUEPRINT_ID = " & CStr(BPID)
            Call EVEDB.ExecuteNonQuerySQL(SQL)

        End If

        readerBP.Close()

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
        ' Create directory if it doesn't exist already
        If Not Directory.Exists(Path.GetDirectoryName(FileName)) Then
            Directory.CreateDirectory(Path.GetDirectoryName(FileName))
        End If
        Dim writeStream As New FileStream(FileName, FileMode.Create)
        Dim bytesread As Integer

        Try 'Checks if the file exist
            Request = DirectCast(HttpWebRequest.Create(DownloadURL), HttpWebRequest)
            Request.Proxy = GetProxyData()
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
        SQL &= "WHERE typeID = blueprintTypeID AND activityID = 8 AND productTypeID = " & CStr(BPID) & " AND quantity <= " & BaseRuns

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader()

        If readerBP.Read Then
            ReturnString = readerBP.GetString(0)
        Else
            ReturnString = ""
        End If

        readerBP.Close()

        Return ReturnString

    End Function

    ' Parses the data and builds an AND qualifier for searching text data - adds data for two fields sent
    Public Function GetSearchText(SearchText As String, Field1 As String, Optional Field2 As String = "") As String
        Dim ReturnString As String = ""
        Dim LikePhrase As String = " LIKE "
        Dim NOTLikePhrase As String = " NOT LIKE "

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

        Return ReturnString

    End Function

    ' Gets the groupName from inventory groups for the groupID sent
    Public Function GetGroupName(ByVal groupID As Integer) As String
        Dim SQL As String
        Dim readerIT As SQLiteDataReader
        Dim ReturnString As String

        SQL = "SELECT groupName FROM INVENTORY_CATEGORIES WHERE groupID = " & CStr(groupID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerIT = DBCommand.ExecuteReader()

        If readerIT.Read Then
            ReturnString = readerIT.GetString(0)
        Else
            ReturnString = ""
        End If

        readerIT.Close()

        Return ReturnString

    End Function

    ' Gets the categoryName from inventory categories for the categoryID sent
    Public Function GetCategoryName(ByVal categoryID As Integer) As String
        Dim SQL As String
        Dim readerIT As SQLiteDataReader
        Dim ReturnString As String

        SQL = "SELECT categoryName FROM INVENTORY_CATEGORIES WHERE categoryID = " & CStr(categoryID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerIT = DBCommand.ExecuteReader()

        If readerIT.Read Then
            ReturnString = readerIT.GetString(0)
        Else
            ReturnString = ""
        End If

        readerIT.Close()

        Return ReturnString

    End Function

    ' Gets the typeid from inventory types for the typename sent
    Public Function GetTypeID(ByVal TypeName As String) As Long
        Dim SQL As String
        Dim readerIT As SQLiteDataReader
        Dim ReturnID As Long

        SQL = "SELECT typeID FROM INVENTORY_TYPES WHERE typeName = '" & FormatDBString(TypeName) & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerIT = DBCommand.ExecuteReader()

        If readerIT.Read Then
            ReturnID = readerIT.GetInt64(0)
        Else
            ReturnID = 0
        End If

        readerIT.Close()

        Return ReturnID

    End Function

    ' Looks up the typeid for the mining bonus for different attributes related to mining and returns the value for everything except ships (those are invTraits)
    Public Function GetMiningBonus(ByVal TypeID As Integer) As Double
        Dim SQL As String
        Dim readerBonus As SQLiteDataReader
        Dim ReturnBonus As Double

        ' It's a module - compressionQuantityNeeded and mining amounts
        SQL = "SELECT TYPE_ATTRIBUTES.attributeID, value AS Bonus "
        SQL &= "FROM TYPE_ATTRIBUTES, INVENTORY_TYPES, INVENTORY_GROUPS "
        SQL &= "WHERE TYPE_ATTRIBUTES.typeid = INVENTORY_TYPES.typeid "
        SQL &= "AND INVENTORY_TYPES.groupid = INVENTORY_GROUPS.groupid "
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

        Return ReturnBonus

    End Function

    ' Downloads the JSON file sent and saves it to the location, then imports it into a string to return
    Public Function GetJSONFile(ByVal URL As String, ByVal UpdateType As String, Optional ByVal IgnoreExceptions As Boolean = False, Optional RecursiveCalls As Integer = 0) As String
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader

        Dim Output As String = ""

        If CancelUpdatePrices Then
            Return ""
        End If

        Try
            Dim Start As DateTime = Now
            Dim myUri As New Uri(URL)
            '/market/<regionID:integerType>/history/
            ' Create the web request  
            request = DirectCast(WebRequest.Create(myUri), HttpWebRequest)
            ' Settings for speed
            request.Method = "GET"
            request.Proxy = GetProxyData()
            request.PreAuthenticate = True
            request.Timeout = 10000
            request.UnsafeAuthenticatedConnectionSharing = True

            ' Get response  
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            Dim ContentLength As Long = CLng(request.GetResponse.Headers(HttpResponseHeader.ContentLength))

            ' Get the response stream into a reader  
            reader = New StreamReader(response.GetResponseStream())

            ' Read the data
            Output = Trim(reader.ReadToEnd)
            If Output.Substring(Len(Output) - 1, 1) = vbLf Then
                Output = Output.Substring(0, Len(Output) - 1)
            End If

            ' See if it downloaded a full file
            If (Len(Output) <> ContentLength And ContentLength <> 0) Or (Output.Substring(Len(Output) - 1, 1) <> "]" And ContentLength = 0) Then
                Application.DoEvents()
                ' Re-run this function - limit to 5 calls
                If RecursiveCalls <= 5 Then
                    Dim NumCalls As Integer = RecursiveCalls + 1
                    Output = GetJSONFile(URL, UpdateType, IgnoreExceptions, NumCalls)
                End If
            End If

            reader.Close()
            response.Close()
            request = Nothing

        Catch ex As Exception
            If Not IgnoreExceptions Then
                MsgBox("Unable to download data for " & UpdateType & vbCrLf & "Error: " & ex.Message, vbInformation, Application.ProductName)
                If UpdateType = "Fuzzwork Market Prices" Or UpdateType = "EVE Marketer Prices" Then
                    ' Don't error again
                    PriceUpdateDown = True
                End If
                Output = ""
            End If

            If ex.Message.Contains("An established connection was aborted by the software in your host machine") _
                Or ex.Message.Contains("An existing connection was forcibly closed by the remote host.") _
                Or ex.Message.Contains("503") And Not IgnoreExceptions Then 'Or ex.Message.Contains("The operation has timed out")
                ' Re-run this function - limit to 10 calls if not part of the first load of the program
                If RecursiveCalls <= 10 And Not FirstLoad Then
                    Dim NumCalls As Integer = RecursiveCalls + 1
                    Output = GetJSONFile(URL, UpdateType, IgnoreExceptions, NumCalls)
                End If
            End If
        Finally
            If response IsNot Nothing Then response.Close()
        End Try

        Return Output

    End Function

    Public Class TypeIDRegion
        Public RegionString As String
        Public TypeIDs As List(Of String)

        Public Sub New()
            RegionString = ""
            TypeIDs = New List(Of String)
        End Sub
    End Class

    Public Structure SystemRegion
        Dim SystemID As String ' could be empty string
        Dim RegionID As String
    End Structure

End Module
