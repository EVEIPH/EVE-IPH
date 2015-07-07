﻿
Imports System.Xml
Imports System.IO
Imports System.Xml.XPath
Imports System.Text.RegularExpressions
Imports System.Data.SQLite

Public Module SettingsVariables

    ' All settings
    Public AllSettings As New ProgramSettings
    ' User Settings
    Public UserApplicationSettings As ApplicationSettings
    ' Tower Cost settings
    Public SelectedTower As PlayerOwnedStationSettings
    ' BP Tab Settings
    Public UserBPTabSettings As BPTabSettings
    ' Manufacturing
    Public UserManufacturingTabSettings As ManufacturingTabSettings
    ' Datacores
    Public UserDCTabSettings As DataCoreTabSettings
    ' Reactions Tab
    Public UserReactionTabSettings As ReactionsTabSettings
    ' Update Prices Tab Settings
    Public UserUpdatePricesTabSettings As UpdatePriceTabSettings
    ' Mining Tab Settings
    Public UserMiningTabSettings As MiningTabSettings
    ' Industry Job Column Settings
    Public UserIndustryJobsColumnSettings As IndustryJobsColumnSettings
    ' Manufacturing Tab Column Settings
    Public UserManufacturingTabColumnSettings As ManufacturingTabColumnSettings
    ' Shopping List settings
    Public UserShoppingListSettings As ShoppingListSettings
    ' Industry Flip Belt Settings
    Public UserIndustryFlipBeltSettings As IndustryFlipBeltSettings
    ' and the five belts
    Public UserIndustryFlipBeltOreCheckSettings1 As IndustryBeltOreChecks
    Public UserIndustryFlipBeltOreCheckSettings2 As IndustryBeltOreChecks
    Public UserIndustryFlipBeltOreCheckSettings3 As IndustryBeltOreChecks
    Public UserIndustryFlipBeltOreCheckSettings4 As IndustryBeltOreChecks
    Public UserIndustryFlipBeltOreCheckSettings5 As IndustryBeltOreChecks
    ' Asset windows - multiple
    Public UserAssetWindowDefaultSettings As AssetWindowSettings
    Public UserAssetWindowShoppingListSettings As AssetWindowSettings

End Module

Public Class ProgramSettings

    ' Default Tower Settings
    Public Const DefaultTowerName As String = None
    Public Const DefaultTowerRaceID As Integer = 0
    Public Const DefaultCostperHour As Integer = 0
    Public Const DefaultMECostperSecond As Integer = 0
    Public Const DefaultTECostperSecond As Integer = 0
    Public Const DefaultInventionCostperSecond As Integer = 0
    Public Const DefaultCopyCostperSecond As Integer = 0
    Public Const DefaultTowerType As String = "Standard"
    Public Const DefaultTowerSize As String = "Large"
    Public Const DefaultFuelBlockBuild As Boolean = False
    Public Const DefaultNumAdvLabs As Integer = 0
    Public Const DefaultNumMobileLabs As Integer = 0
    Public Const DefaultNumHyasyodaLabs As Integer = 0
    Public Const DefaultCharterCost As Double = 2500.0

    ' Application Setting Defaults
    Public MBeanCounterName As String = "Zainou 'Beancounter' Industry BX-80" ' Manufacturing time
    Public RBeanCounterName As String = "Zainou 'Beancounter' Reprocessing RX-80" ' Refining waste
    Public CBeanCounterName As String = "Zainou 'Beancounter' Science SC-80" ' Copy time

    Public DefaultCheckUpdatesOnStart As Boolean = True
    Public DefaultAllowSkillOverride As Boolean = False
    Public DefaultDataExportFormat As String = "Default"
    Public DefaultShowToolTips As Boolean = True
    Public DefaultLoadAssetsonStartup As Boolean = True
    Public DefaultLoadBPsonStartup As Boolean = True
    Public DefaultRefreshTeamCRESTDataonStartup As Boolean = True
    Public DefaultRefreshMarketCRESTDataonStartup As Boolean = True
    Public DefaultRefreshFacilityCRESTDataonStartup As Boolean = True
    Public DefaultDisableSound As Boolean = False
    Public DefaultDNMarkInlineasOwned As Boolean = False

    Public DefaultBuildBaseInstall As Double = 1000
    Public DefaultBuildBaseHourly As Double = 333
    Public DefaultBuildStandingDiscount As Double = 0.015
    Public DefaultBuildStandingSurcharge As Double = 0.005

    Public DefaultInventBaseInstall As Double = 10000
    Public DefaultInventBaseHourly As Double = 416.67
    Public DefaultInventStandingDiscount As Double = 0.015
    Public DefaultInventStandingSurcharge As Double = 0.005

    Public DefaultBuildCorpStanding As Double = 5.0 ' Corp standing of where this blueprint will be made
    Public DefaultInventCorpStanding As Double = 5.0 ' Corp standing of where this blueprint will be invented
    Public DefaultBrokerCorpStanding As Double = 5.0 ' Corp standing of where this blueprint will be sold
    Public DefaultBrokerFactionStanding As Double = 5.0 ' Faction standing of where this blueprint will be sold (for Broker calc)
    Public DefaultRefineCorpStanding As Double = 6.67 ' Corp standing for use of refining

    Public DefaultIncludeCopyTimes As Boolean = False ' If we include copy times in IPH calcs for invention
    Public DefaultIncludeInventionTimes As Boolean = False ' If we include invention times in IPH calcs for invention
    Public DefaultIncludeRETimes As Boolean = False ' If we include RE times in IPH calcs for RE

    Public DefaultEstimateCopyCost As Boolean = False ' Estimate copy costs for invention BPC's
    Public DefaultCopySlotModifier As String = "1.0" ' The default copy slot modifier for T1 BPC copies to use in invention
    Public DefaultInventionSlotModifier As String = "1.0" ' Default invention time
    Public DefaultBuildSlotModifier As String = "1.0" ' Default build time for production
    Public DefaultRefiningEfficency As Double = 0.5 ' Default refining equipment

    Public DefaultRefineTax As Double = 0.05 ' Default tax rate

    Public DefaultCheckBuildBuy As Boolean = False
    Public DefaultIgnoreRareandShipSkinBPs As Boolean = True
    Public DefaultSaveBPRelicsDecryptors As Boolean = False

    Public DefaultSettingME As Integer = 0
    Public DefaultSettingTE As Integer = 0

    Public DefaultDisableSVR As Boolean = False
    Public DefaultSuggestBuildBPNotOwned As Boolean = True ' If the bp is not owned, default to suggesting they build the item anyway

    ' For shopping list
    Public DefaultShopListIncludeInventMats As Boolean = True
    Public DefaultShopListIncludeCopyMats As Boolean = True

    ' If the user has no implants
    Public DefaultImplantValues As Double = 0

    ' No team
    Public DefaultTeamID As Long = 0

    ' Default Facilities - all Jita 4-4 except for RE, which will be a POS in Jita 
    Public DefaultManufacturingFacilityID As Long = 60003760
    Public DefaultManufacturingFacility As String = "Jita IV - Moon 4 - Caldari Navy Assembly Plant"
    Public DefaultManufacturingFacilityType As String = StationFacility
    Public DefaultComponentManufacturingFacilityID As Long = 60003760
    Public DefaultComponentManufacturingFacility As String = "Jita IV - Moon 4 - Caldari Navy Assembly Plant"
    Public DefaultComponentManufacturingFacilityType As String = StationFacility
    Public DefaultCapitalComponentManufacturingFacilityID As Long = 60003760
    Public DefaultCapitalComponentManufacturingFacility As String = "Jita IV - Moon 4 - Caldari Navy Assembly Plant"
    Public DefaultCapitalComponentManufacturingFacilityType As String = StationFacility
    Public DefaultCapitalManufacturingFacilityID As Long = 60003043
    Public DefaultCapitalManufacturingFacility As String = "Akora VI - Moon 7 - Expert Housing Production Plant"
    Public DefaultCapitalManufacturingFacilityType As String = StationFacility
    Public DefaultCopyFacilityID As Long = 60001786 ' Wous
    Public DefaultCopyFacility As String = "Wuos VI - Zainou Biotech Research Center"
    Public DefaultCopyFacilityType As String = StationFacility
    Public DefaultInventionFacilityID As Long = 60001786 ' Wous
    Public DefaultInventionFacility As String = "Wuos VI - Zainou Biotech Research Center"
    Public DefaultInventionFacilityType As String = StationFacility
    Public DefaultT3InventionFacilityID As Long = 24567
    Public DefaultT3InventionFacility As String = "Experimental Laboratory"
    Public DefaultT3InventionFacilityType As String = POSFacility
    Public DefaultT3CruiserManufacturingFacilityID As Long = 30389 ' If we are manufacturing a T3 item, then default to the subsystem array in a pos
    Public DefaultT3CruiserManufacturingFacility As String = "Subsystem Assembly Array"
    Public DefaultT3CruiserManufacturingFacilityType As String = POSFacility
    Public DefaultT3DestroyerManufacturingFacilityID As Long = 24653 ' If we are manufacturing a T3 item, then default to the subsystem array in a pos
    Public DefaultT3DestroyerManufacturingFacility As String = "Advanced Small Ship Assembly Array"
    Public DefaultT3DestroyerManufacturingFacilityType As String = POSFacility
    Public DefaultSubsystemManufacturingFacilityID As Long = 30389 ' If we are manufacturing a T3 item, then default to the subsystem array in a pos
    Public DefaultSubsystemManufacturingFacility As String = "Subsystem Assembly Array"
    Public DefaultSubsystemManufacturingFacilityType As String = POSFacility
    Public DefaultSuperManufacturingFacilityID As Long = 24575 ' If we are manufacturing a super, then default to the supercaptial array in a pos
    Public DefaultSuperManufacturingFacility As String = "Supercapital Ship Assembly Array"
    Public DefaultSuperManufacturingFacilityType As String = POSFacility
    Public DefaultBoosterManufacturingFacilityID As Long = 25305 ' Drug lab in a pos
    Public DefaultBoosterManufacturingFacility As String = "Drug Lab"
    Public DefaultBoosterManufacturingFacilityType As String = POSFacility

    Public DefaultPOSFuelBlockManufacturingFacilityID As Long = 24660
    Public DefaultPOSFuelBlockManufacturingFacility As String = "Component Assembly Array" ' Component array in a pos for fuel blocks
    Public DefaultPOSFuelBlockManufacturingFacilityType As String = POSFacility

    Public DefaultPOSLargeShipManufacturingFacilityID As Long = 29613
    Public DefaultPOSLargeShipManufacturingFacility As String = "Large Ship Assembly Array" ' Large Ship assembly array in a pos for Large Ships
    Public DefaultPOSLargeShipManufacturingFacilityType As String = POSFacility

    Public DefaultPOSModuleManufacturingFacilityID As Long = 13780
    Public DefaultPOSModuleManufacturingFacility As String = "Equipment Assembly Array" ' Equipment assembly array in a pos for all modules
    Public DefaultPOSModuleManufacturingFacilityType As String = POSFacility

    Public FacilityDefaultMM As Double = 1
    Public FacilityDefaultTM As Double = 1
    Public DefalutFacilityCM As Double = 1
    Public FacilityDefaultTax As Double = 0.1 ' Only for processing
    Public OutpostDefaultTax As Double = 0 ' If we are saving the settings, then the only time would be for outposts

    ' For POS data (T3 and general pos)
    Public FacilityDefaultSolarSystemID As Long = 30000142
    Public FacilityDefaultSolarSystem As String = "Jita"
    Public FacilityDefaultRegionID As Long = 10000002
    Public FacilityDefaultRegion As String = "The Forge"

    ' For Booster and super pos production
    Public DefaultNullFacilitySolarSystemID As Long = 30003713
    Public DefaultNullFacilitySolarSystem As String = "G7AQ-7"
    Public DefaultNullFacilityRegionID As Long = 10000047
    Public DefaultNullFacilityRegion As String = "Providence"

    Public FacilityDefaultActivityCostperSecond As Double = 0
    Public FacilityDefaultIncludeUsage As Boolean = True
    Public FacilityDefaultIncludeCost As Boolean = False ' Only for Invention, Copy, and RE so let this get set 
    Public FacilityDefaultIncludeTime As Boolean = False ' Only for Invention, Copy, and RE so let this get set 

    ' Set here, but use in Update Prices - 6 hours to refresh prices
    Public DefaultEVECentralRefreshInterval As Integer = 6

    ' BP Tab Default settings
    Public DefaultBPTechChecks As Boolean = True
    Public DefaultSizeChecks As Boolean = False
    Public DefaultBPSelectionType As String = "All"
    Public DefaultBPIncludeFees As Boolean = True
    Public DefaultBPIncludeTaxes As Boolean = True
    Public DefaultBPIncludeUsage As Boolean = True
    Public DefaultBPIgnoreChecks As Boolean = False
    Public DefaultBPPricePerUnit As Boolean = False
    Public DefaultBPIncludeInventionTime As Boolean = False
    Public DefaultBPIncludeInventionCost As Boolean = True
    Public DefaultBPIncludeCopyTime As Boolean = False
    Public DefaultBPIncludecopyCost As Boolean = True
    Public DefaultBPIncludeT3Cost As Boolean = False
    Public DefaultBPIncludeT3Time As Boolean = False
    Public DefaultBPProductionLines As Integer = 1
    Public DefaultBPLaboratoryLines As Integer = 1
    Public DefaultBPRELines As Integer = 1
    Public DefaultBPRelicType As String = "" ' If they want to save and auto load relic types
    Public DefaultBPT3DecryptorType As String = "" ' if they want to save and auto load decryptors
    Public DefaultBPT2DecryptorType As String = "" ' if they want to save and auto load decryptors
    Public DefaultBPIgnoreInvention As Boolean = False
    Public DefaultBPIgnoreMinerals As Boolean = False
    Public DefaultBPIgnoreT1Item As Boolean = False
    Public DefaultBPIncludeIgnoredBPs As Boolean = False

    ' Update Prices Default Settings
    Public DefaultPriceChecks As Boolean = False
    Public DefaultPriceImportPriceType As String = "Minimum Sell"
    Public DefaultPriceSystem As String = "Jita"
    Public DefaultPriceRegion As String = ""
    Public DefaultPriceRawMatsCombo As String = "Max Buy"
    Public DefaultPriceItemsCombo As String = "Min Sell"
    Public DefaultPriceCRESTHistory As Boolean = False

    ' Default Manufacturing Tab
    Public DefaultBlueprintType As String = "All Blueprints"
    Public DefaultCheckTech1 As Boolean = True
    Public DefaultCheckTech2 As Boolean = True
    Public DefaultCheckTech3 As Boolean = True
    Public DefaultCheckTechStoryline As Boolean = True
    Public DefaultCheckTechNavy As Boolean = True
    Public DefaultCheckTechPirate As Boolean = True
    Public DefaultItemTypeFilter As String = "All Types"
    Public DefaultTextItemFilter As String = ""
    Public DefaultCheckBPTypeShips As Boolean = True
    Public DefaultCheckBPTypeDrones As Boolean = True
    Public DefaultCheckBPTypeComponents As Boolean = True
    Public DefaultCheckBPTypeStructures As Boolean = True
    Public DefaultCheckBPTypeTools As Boolean = True
    Public DefaultCheckBPTypeModules As Boolean = True
    Public DefaultCheckBPTypeAmmoCharges As Boolean = True
    Public DefaultCheckBPTypeRigs As Boolean = True
    Public DefaultCheckBPTypeSubsystems As Boolean = True
    Public DefaultCheckBPTypeBoosters As Boolean = True
    Public DefaultCheckBPTypeDeployables As Boolean = True
    Public DefaultCheckBPTypeCelestials As Boolean = True
    Public DefaultCheckBPTypeStationParts As Boolean = True
    Public DefaultAveragePriceDuration As String = "7"
    Public DefaultCheckDecryptorNone As Boolean = True
    Public DefaultCheckDecryptor06 As Boolean = False
    Public DefaultCheckDecryptor09 As Boolean = False
    Public DefaultCheckDecryptor10 As Boolean = False
    Public DefaultCheckDecryptor11 As Boolean = False
    Public DefaultCheckDecryptor12 As Boolean = False
    Public DefaultCheckDecryptor15 As Boolean = False
    Public DefaultCheckDecryptor18 As Boolean = False
    Public DefaultCheckDecryptor19 As Boolean = False
    Public DefaultCheckDecryptorUseforT2 As Boolean = True
    Public DefaultCheckDecryptorUseforT3 As Boolean = True
    Public DefaultCheckIgnoreInvention As Boolean = False
    Public DefaultCheckRelicWrecked As Boolean = True
    Public DefaultCheckRelicIntact As Boolean = False
    Public DefaultCheckRelicMalfunction As Boolean = False
    Public DefaultCheckOnlyBuild As Boolean = False
    Public DefaultCheckOnlyInvent As Boolean = False
    Public DefaultCheckOnlyRE As Boolean = False
    Public DefaultCheckIncludeTaxes As Boolean = True
    Public DefaultIncludeBrokersFees As Boolean = True
    Public DefaultCheckIncludeUsage As Boolean = True
    Public DefaultCheckRaceAmarr As Boolean = True
    Public DefaultCheckRaceCaldari As Boolean = True
    Public DefaultCheckRaceGallente As Boolean = True
    Public DefaultCheckRaceMinmatar As Boolean = True
    Public DefaultCheckRacePirate As Boolean = True
    Public DefaultCheckRaceOther As Boolean = True
    Public DefaultSortBy As String = "IPH"
    Public DefaultPriceCompare As String = "Compare All"
    Public DefaultCheckIncludeT2Owned As Boolean = True
    Public DefaultCheckIncludeT3Owned As Boolean = True
    Public DefaultIgnoreSVRThreshold As Double = 0.0
    Public DefaultCheckSVRIncludeNull As Boolean = True
    Public DefaultSVRRegion As String = "The Forge"
    Public DefaultCalcProductionLines As Integer = 1
    Public DefaultCalcLaboratoryLines As Integer = 1
    Public DefaultCalcRuns As Integer = 1
    Public DefaultCalcBPRuns As Integer = 1
    Public DefaultCheckAutoCalcNumBPs As Boolean = True
    Public DefaultCalcSizeChecks As Boolean = False
    Public DefaultCheckT3Destroyers As Boolean = False
    Public DefaultCheckCapComponents As Boolean = False
    Public DefaultCalcIgnoreInvention As Boolean = False
    Public DefaultCalcIgnoreMinerals As Boolean = False
    Public DefaultCalcIgnoreT1Item As Boolean = False

    ' Datacore Default Settings
    Public DefaultDCPricesFrom As String = "Updated Prices"
    Public DefaultDCCheckHighSec As Boolean = True
    Public DefaultDCCheckLowNullSec As Boolean = False
    Public DefaultDCIncludeAgentsCantUse As Boolean = False
    Public DefaultDCAgentsInRegion As String = "All Regions"
    Public DefaultDCSovCheck As Boolean = True

    ' Datacores For these, use the users settings
    Public DefaultConnections As Integer = -1
    Public DefaultNegotiation As Integer = -1
    Public DefaultResearchProjMgt As Integer = -1
    Public DefaultCorpStanding As Integer = -1
    Public DefaultCorpStandingChecked As Integer = -1
    Public DefaultSkillLevel As Integer = -1
    Public DefaultSkillLevelChecked As Integer = -1

    ' Datacore setting array sizes
    Public NumberofDCSettingsSkillRecords As Integer = 16
    Public NumberofDCSettingsCorpRecords As Integer = 12

    ' Reactions Default Settings
    Public DefaultReactPOSFuelCost As Double = 500000.0
    Public DefaultReactCheckTaxes As Boolean = True
    Public DefaultReactCheckFees As Boolean = True
    Public DefaultReactItemChecks As Boolean = False
    Public DefaultReactNumPOS As Integer = 1

    ' Mining Default Settings
    Public DefaultMiningOreType As String = "Ore"
    Public DefaultMiningCheckHighYieldOres As Boolean = False
    Public DefaultMiningCheckHighSecOres As Boolean = True
    Public DefaultMiningCheckLowSecOres As Boolean = False
    Public DefaultMiningCheckNullSecOres As Boolean = False
    Public DefaultMiningCheckSovAmarr As Boolean = True
    Public DefaultMiningCheckSovCaldari As Boolean = True
    Public DefaultMiningCheckSovGallente As Boolean = True
    Public DefaultMiningCheckSovMinmatar As Boolean = True
    Public DefaultMiningCheckSovWormhole As Boolean = True
    Public DefaultMiningCheckSovC1 As Boolean = True
    Public DefaultMiningCheckSovC2 As Boolean = True
    Public DefaultMiningCheckSovC3 As Boolean = True
    Public DefaultMiningCheckSovC4 As Boolean = True
    Public DefaultMiningCheckSovC5 As Boolean = True
    Public DefaultMiningCheckSovC6 As Boolean = True
    Public DefaultMiningCheckIncludeFees As Boolean = True
    Public DefaultMiningCheckIncludeTaxes As Boolean = True
    Public DefaultMiningCheckIncludeJumpFuelCosts As Boolean = False
    Public DefaultMiningTotalJumpFuelCost As Integer = 0
    Public DefaultMiningTotalJumpFuelM3 As Integer = 1
    Public DefaultMiningJumpCompressedOre As Boolean = True
    Public DefaultMiningJumpMinerals As Boolean = False
    Public DefaultMiningMiningShip As String = "" ' Keep this blank so that it will default to a ship for them, if they have the skills
    Public DefaultMiningIceMiningShip As String = "" ' Keep this blank so that it will default to a ship for them, if they have the skills
    Public DefaultMiningGasMiningShip As String = ""
    Public DefaultMiningOreStrip As String = "" ' Keep blank to set max possible strip/miner they can use
    Public DefaultMiningIceStrip As String = "" ' Keep blank so they can set the max possible ice strip
    Public DefaultMiningGasHarvester As String = ""
    Public DefaultMiningNumOreMiners As Integer = 0
    Public DefaultMiningNumIceMiners As Integer = 0
    Public DefaultMiningNumGasHarvesters As Integer = 0
    Public DefaultMiningOreUpgrade As String = None
    Public DefaultMiningIceUpgrade As String = None
    Public DefaultMiningGasUpgrade As String = None
    Public DefaultMiningNumOreUpgrades As Integer = 0
    Public DefaultMiningNumIceUpgrades As Integer = 0
    Public DefaultMiningNumGasUpgrades As Integer = 0
    Public DefaultMiningMichiiImplant As Boolean = False
    Public DefaultMiningT2Crystals As Boolean = False
    Public DefaultMiningOreImplant As String = None
    Public DefaultMiningIceImplant As String = None
    Public DefaultMiningGasImplant As String = None
    Public DefaultMiningCheckUseHauler As Boolean = True
    Public DefaultMiningRoundTripMin As Integer = 1
    Public DefaultMiningRoundTripSec As Integer = 0
    Public DefaultMiningHaulerm3 As Integer = 0
    Public DefaultMiningCheckUseFleetBooster As Boolean = False
    Public DefaultMiningBoosterShip As String = "Other"
    Public DefaultMiningBoosterShipSkill As Integer = 0
    Public DefaultMiningMiningFormanSkill As Integer = 0
    Public DefaultMiningMiningDirectorSkill As Integer = 0
    Public DefaultMiningWarfareLinkSpecSkill As Integer = 0
    Public DefaultMiningCheckMineForemanLaserOpBoost As Integer = 0
    Public DefaultMiningCheckMiningForemanMindLink As Boolean = False
    Public DefaultMiningRefineCorpTax As Double = 0.05
    Public DefaultMiningRorqDeployed As Boolean = True
    Public DefaultMiningDroneM3perHour As Double = 0.0
    Public DefaultMiningRefinedOre As Boolean = True
    Public DefaultMiningCompressedOre As Boolean = False
    Public DefaultMiningUnrefinedOre As Boolean = False
    Public DefaultMiningIndustrialReconfig As Integer = 0
    Public DefaultMiningRig As Boolean = False
    Public DefaultMiningNumberofMiners As Integer = 1

    ' Industry Jobs column settings
    Public DefaultJobState As Integer = 1
    Public DefaultInstallerName As Integer = 2
    Public DefaultTimeToComplete As Integer = 4
    Public DefaultActivity As Integer = 3
    Public DefaultStatus As Integer = 0
    Public DefaultStartTime As Integer = 0
    Public DefaultEndTime As Integer = 0
    Public DefaultCompletionTime As Integer = 0
    Public DefaultBlueprint As Integer = 5
    Public DefaultOutputItem As Integer = 6
    Public DefaultOutputItemType As Integer = 0
    Public DefaultInstallSolarSystem As Integer = 7
    Public DefaultInstallRegion As Integer = 8
    Public DefaultLicensedRuns As Integer = 0
    Public DefaultRuns As Integer = 0
    Public DefaultSuccessfulRuns As Integer = 0
    Public DefaultBlueprintLocation As Integer = 9
    Public DefaultOutputLocation As Integer = 10
    Public DefaultJobType As Integer = 11
    Public DefaultOrderType As String = "Ascending"
    Public DefaultViewJobType As String = "Personal"
    Public DefaultJobTimes As String = "Current Jobs"
    Public DefaultSelectedCharacterIDs As String = ""
    Public DefaultIndustryColumnWidth As Integer = 100
    Public DefaultOrderByColumn As Integer = 3

    ' Column Names for industry jobs viewer
    Public Const JobStateColumn As String = "Job State"
    Public Const InstallerNameColumn As String = "Installer"
    Public Const TimetoCompleteColumn As String = "Time to Complete"
    Public Const ActivityColumn As String = "Activity"
    Public Const StatusColumn As String = "Status"
    Public Const StartTimeColumn As String = "Start Time"
    Public Const EndTimeColumn As String = "End Time"
    Public Const CompletionTimeColumn As String = "Completed Time"
    Public Const BlueprintColumn As String = "Blueprint"
    Public Const OutputItemColumn As String = "Output Item"
    Public Const OutputItemTypeColumn As String = "Output Item Type"
    Public Const InstallSolarSystemColumn As String = "Install System"
    Public Const InstallRegionColumn As String = "Install Region"
    Public Const LicensedRunsColumn As String = "Licensed Runs"
    Public Const RunsColumn As String = "Runs"
    Public Const SuccessfulRunsColumn As String = "Successful Runs"
    Public Const BlueprintLocationColumn As String = "Blueprint Location"
    Public Const OutputLocationColumn As String = "Output Location"
    Public Const JobTypeColumn As String = "Job Type"

    ' Manufacturing Tab column settings - index 0 is for hidden id column
    Dim DefaultMTItemCategory As Integer = 1
    Dim DefaultMTItemGroup As Integer = 0
    Dim DefaultMTItemName As Integer = 2
    Dim DefaultMTOwned As Integer = 3
    Dim DefaultMTTech As Integer = 4
    Dim DefaultMTBPME As Integer = 5
    Dim DefaultMTBPTE As Integer = 6
    Dim DefaultMTInputs As Integer = 7
    Dim DefaultMTCompared As Integer = 8
    Dim DefaultMTTotalRuns As Integer = 0
    Dim DefaultMTSingleInventedBPCRuns As Integer = 0
    Dim DefaultMTProductionLines As Integer = 0
    Dim DefaultMTLaboratoryLines As Integer = 0
    Dim DefaultMTTotalInventionCost As Integer = 0
    Dim DefaultMTTotalCopyCost As Integer = 0
    Dim DefaultMTTaxes As Integer = 0
    Dim DefaultMTBrokerFees As Integer = 0
    Dim DefaultMTBPProductionTime As Integer = 0
    Dim DefaultMTTotalProductionTime As Integer = 0
    Dim DefaultMTCopyTime As Integer = 0
    Dim DefaultMTInventionTime As Integer = 0
    Dim DefaultMTItemMarketPrice As Integer = 0
    Dim DefaultMTProfit As Integer = 9
    Dim DefaultMTProfitPercentage As Integer = 0
    Dim DefaultMTIskperHour As Integer = 10
    Dim DefaultMTSVR As Integer = 11
    Dim DefaultMTSVRxIPH As Integer = 0
    Dim DefaultMTTotalCost As Integer = 12
    Dim DefaultMTBaseJobCost As Integer = 0
    Dim DefaultMTNumBPs As Integer = 0
    Dim DefaultMTInventionChance As Integer = 0
    Dim DefaultMTBPType As Integer = 0
    Dim DefaultMTRace As Integer = 0
    Dim DefaultMTVolumeperItem As Integer = 0
    Dim DefaultMTTotalVolume As Integer = 0
    Dim DefaultMTManufacturingJobFee As Integer = 0
    Dim DefaultMTManufacturingFacilityName As Integer = 0
    Dim DefaultMTManufacturingFacilitySystem As Integer = 0
    Dim DefaultMTManufacturingFacilityRegion As Integer = 0
    Dim DefaultMTManufacturingFacilitySystemIndex As Integer = 0
    Dim DefaultMTManufacturingFacilityTax As Integer = 0
    Dim DefaultMTManufacturingFacilityMEBonus As Integer = 0
    Dim DefaultMTManufacturingFacilityTEBonus As Integer = 0
    Dim DefaultMTManufacturingFacilityUsage As Integer = 0
    Dim DefaultMTComponentFacilityName As Integer = 0
    Dim DefaultMTComponentFacilitySystem As Integer = 0
    Dim DefaultMTComponentFacilityRegion As Integer = 0
    Dim DefaultMTComponentFacilitySystemIndex As Integer = 0
    Dim DefaultMTComponentFacilityTax As Integer = 0
    Dim DefaultMTComponentFacilityMEBonus As Integer = 0
    Dim DefaultMTComponentFacilityTEBonus As Integer = 0
    Dim DefaultMTComponentFacilityUsage As Integer = 0
    Dim DefaultMTCopyingFacilityName As Integer = 0
    Dim DefaultMTCopyingFacilitySystem As Integer = 0
    Dim DefaultMTCopyingFacilityRegion As Integer = 0
    Dim DefaultMTCopyingFacilitySystemIndex As Integer = 0
    Dim DefaultMTCopyingFacilityTax As Integer = 0
    Dim DefaultMTCopyingFacilityMEBonus As Integer = 0
    Dim DefaultMTCopyingFacilityTEBonus As Integer = 0
    Dim DefaultMTCopyingFacilityUsage As Integer = 0
    Dim DefaultMTInventionFacilityName As Integer = 0
    Dim DefaultMTInventionFacilitySystem As Integer = 0
    Dim DefaultMTInventionFacilityRegion As Integer = 0
    Dim DefaultMTInventionFacilitySystemIndex As Integer = 0
    Dim DefaultMTInventionFacilityTax As Integer = 0
    Dim DefaultMTInventionFacilityMEBonus As Integer = 0
    Dim DefaultMTInventionFacilityTEBonus As Integer = 0
    Dim DefaultMTInventionFacilityUsage As Integer = 0

    Dim DefaultMTItemCategoryWidth As Integer = 100
    Dim DefaultMTItemGroupWidth As Integer = 100
    Dim DefaultMTItemNameWidth As Integer = 225
    Dim DefaultMTOwnedWidth As Integer = 50
    Dim DefaultMTTechWidth As Integer = 37
    Dim DefaultMTBPMEWidth As Integer = 28
    Dim DefaultMTBPTEWidth As Integer = 28
    Dim DefaultMTInputsWidth As Integer = 150
    Dim DefaultMTComparedWidth As Integer = 80
    Dim DefaultMTTotalRunsWidth As Integer = 64
    Dim DefaultMTSingleInventedBPCRunsWidth As Integer = 138
    Dim DefaultMTProductionLinesWidth As Integer = 92
    Dim DefaultMTLaboratoryLinesWidth As Integer = 92
    Dim DefaultMTTotalInventionCostWidth As Integer = 107
    Dim DefaultMTTotalCopyCostWidth As Integer = 88
    Dim DefaultMTTaxesWidth As Integer = 91
    Dim DefaultMTBrokerFeesWidth As Integer = 100
    Dim DefaultMTBPProductionTimeWidth As Integer = 106
    Dim DefaultMTTotalProductionTimeWidth As Integer = 116
    Dim DefaultMTCopyTimeWidth As Integer = 100
    Dim DefaultMTInventionTimeWidth As Integer = 100
    Dim DefaultMTItemMarketPriceWidth As Integer = 100
    Dim DefaultMTProfitWidth As Integer = 100
    Dim DefaultMTProfitPercentageWidth As Integer = 100
    Dim DefaultMTIskperHourWidth As Integer = 100
    Dim DefaultMTSVRWidth As Integer = 100
    Dim DefaultMTSVRxIPHWidth As Integer = 100
    Dim DefaultMTTotalCostWidth As Integer = 102
    Dim DefaultMTBaseJobCostWidth As Integer = 100
    Dim DefaultMTNumBPsWidth As Integer = 57
    Dim DefaultMTInventionChanceWidth As Integer = 100
    Dim DefaultMTBPTypeWidth As Integer = 54
    Dim DefaultMTRaceWidth As Integer = 77
    Dim DefaultMTVolumeperItemWidth As Integer = 89
    Dim DefaultMTTotalVolumeWidth As Integer = 75
    Dim DefaultMTManufacturingJobFeeWidth As Integer = 122
    Dim DefaultMTManufacturingFacilityNameWidth As Integer = 150
    Dim DefaultMTManufacturingFacilitySystemWidth As Integer = 152
    Dim DefaultMTManufacturingFacilityRegionWidth As Integer = 154
    Dim DefaultMTManufacturingFacilitySystemIndexWidth As Integer = 184
    Dim DefaultMTManufacturingFacilityTaxWidth As Integer = 138
    Dim DefaultMTManufacturingFacilityMEBonusWidth As Integer = 169
    Dim DefaultMTManufacturingFacilityTEBonusWidth As Integer = 166
    Dim DefaultMTManufacturingFacilityUsageWidth As Integer = 149
    Dim DefaultMTComponentFacilityNameWidth As Integer = 145
    Dim DefaultMTComponentFacilitySystemWidth As Integer = 140
    Dim DefaultMTComponentFacilityRegionWidth As Integer = 138
    Dim DefaultMTComponentFacilitySystemIndexWidth As Integer = 168
    Dim DefaultMTComponentFacilityTaxWidth As Integer = 122
    Dim DefaultMTComponentFacilityMEBonusWidth As Integer = 153
    Dim DefaultMTComponentFacilityTEBonusWidth As Integer = 153
    Dim DefaultMTComponentFacilityUsageWidth As Integer = 136
    Dim DefaultMTCopyingFacilityNameWidth As Integer = 116
    Dim DefaultMTCopyingFacilitySystemWidth As Integer = 122
    Dim DefaultMTCopyingFacilityRegionWidth As Integer = 122
    Dim DefaultMTCopyingFacilitySystemIndexWidth As Integer = 153
    Dim DefaultMTCopyingFacilityTaxWidth As Integer = 107
    Dim DefaultMTCopyingFacilityMEBonusWidth As Integer = 137
    Dim DefaultMTCopyingFacilityTEBonusWidth As Integer = 135
    Dim DefaultMTCopyingFacilityUsageWidth As Integer = 121
    Dim DefaultMTInventionFacilityNameWidth As Integer = 122
    Dim DefaultMTInventionFacilitySystemWidth As Integer = 130
    Dim DefaultMTInventionFacilityRegionWidth As Integer = 129
    Dim DefaultMTInventionFacilitySystemIndexWidth As Integer = 156
    Dim DefaultMTInventionFacilityTaxWidth As Integer = 112
    Dim DefaultMTInventionFacilityMEBonusWidth As Integer = 144
    Dim DefaultMTInventionFacilityTEBonusWidth As Integer = 141
    Dim DefaultMTInventionFacilityUsageWidth As Integer = 127

    Public DefaultMTOrderType As String = "Ascending"
    Public DefaultMTOrderByColumn As Integer = 3

    ' Column Names for manufacturing tab
    Public Const ItemCategoryColumnName As String = "Item Category"
    Public Const ItemGroupColumnName As String = "Item Group"
    Public Const ItemNameColumnName As String = "Item Name"
    Public Const OwnedColumnName As String = "Owned"
    Public Const TechColumnName As String = "Tech"
    Public Const BPMEColumnName As String = "ME"
    Public Const BPTEColumnName As String = "TE"
    Public Const InputsColumnName As String = "Inputs"
    Public Const ComparedColumnName As String = "Compared"
    Public Const TotalRunsColumnName As String = "Total Runs"
    Public Const SingleInventedBPCRunsColumnName As String = "Single Invented BPC Runs"
    Public Const ProductionLinesColumnName As String = "Production Lines"
    Public Const LaboratoryLinesColumnName As String = "Laboratory Lines"
    Public Const TotalInventionCostColumnName As String = "Total Invention Cost"
    Public Const TotalCopyCostColumnName As String = "Total Copy Cost"
    Public Const SVRxIPHColumnName As String = "SVR * IPH"
    Public Const TaxesColumnName As String = "Taxes"
    Public Const BrokerFeesColumnName As String = "Broker Fees"
    Public Const BPProductionTimeColumnName As String = "BP Production Time"
    Public Const TotalProductionTimeColumnName As String = "Total Production Time"
    Public Const CopyTimeColumnName As String = "Copy Time"
    Public Const InventionTimeColumnName As String = "Invention Time"
    Public Const ItemMarketPriceColumnName As String = "Item Market Price"
    Public Const ProfitColumnName As String = "Profit"
    Public Const ProfitPercentageColumnName As String = "Profit Percentage"
    Public Const IskperHourColumnName As String = "Isk per Hour"
    Public Const SVRColumnName As String = "SVR"
    Public Const TotalCostColumnName As String = "Total Cost"
    Public Const BaseJobCostColumnName As String = "Base Job Cost"
    Public Const NumBPsColumnName As String = "Num BPs"
    Public Const InventionChanceColumnName As String = "Invention Chance"
    Public Const BPTypeColumnName As String = "BP Type"
    Public Const RaceColumnName As String = "Race"
    Public Const VolumeperItemColumnName As String = "Volume per Item"
    Public Const TotalVolumeColumnName As String = "Total Volume"
    Public Const ManufacturingJobFeeColumnName As String = "Manufacturing Job Fee"
    Public Const ManufacturingFacilityNameColumnName As String = "Manufacturing Facility Name"
    Public Const ManufacturingFacilitySystemColumnName As String = "Manufacturing Facility System"
    Public Const ManufacturingFacilityRegionColumnName As String = "Manufacturing Facility Region"
    Public Const ManufacturingFacilitySystemIndexColumnName As String = "Manufacturing Facility System Index"
    Public Const ManufacturingFacilityTaxColumnName As String = "Manufacturing Facility Tax"
    Public Const ManufacturingFacilityMEBonusColumnName As String = "Manufacturing Facility ME Bonus"
    Public Const ManufacturingFacilityTEBonusColumnName As String = "Manufacturing Facility TE Bonus"
    Public Const ManufacturingFacilityUsageColumnName As String = "Manufacturing Facility Usage"
    Public Const ComponentFacilityNameColumnName As String = "Component Facility Name"
    Public Const ComponentFacilitySystemColumnName As String = "Component Facility System"
    Public Const ComponentFacilityRegionColumnName As String = "Component Facility Region"
    Public Const ComponentFacilitySystemIndexColumnName As String = "Component Facility System Index"
    Public Const ComponentFacilityTaxColumnName As String = "Component Facility Tax"
    Public Const ComponentFacilityMEBonusColumnName As String = "Component Facility ME Bonus"
    Public Const ComponentFacilityTEBonusColumnName As String = "Component Facility TE Bonus"
    Public Const ComponentFacilityUsageColumnName As String = "Component Facility Usage"
    Public Const CopyingFacilityNameColumnName As String = "Copying Facility Name"
    Public Const CopyingFacilitySystemColumnName As String = "Copying Facility System"
    Public Const CopyingFacilityRegionColumnName As String = "Copying Facility Region"
    Public Const CopyingFacilitySystemIndexColumnName As String = "Copying Facility System Index"
    Public Const CopyingFacilityTaxColumnName As String = "Copying Facility Tax"
    Public Const CopyingFacilityMEBonusColumnName As String = "Copying Facility ME Bonus"
    Public Const CopyingFacilityTEBonusColumnName As String = "Copying Facility TE Bonus"
    Public Const CopyingFacilityUsageColumnName As String = "Copying Facility Usage"
    Public Const InventionFacilityNameColumnName As String = "Invention Facility Name"
    Public Const InventionFacilitySystemColumnName As String = "Invention Facility System"
    Public Const InventionFacilityRegionColumnName As String = "Invention Facility Region"
    Public Const InventionFacilitySystemIndexColumnName As String = "Invention Facility SystemIndex"
    Public Const InventionFacilityTaxColumnName As String = "Invention Facility Tax"
    Public Const InventionFacilityMEBonusColumnName As String = "Invention Facility ME Bonus"
    Public Const InventionFacilityTEBonusColumnName As String = "Invention Facility TE Bonus"
    Public Const InventionFacilityUsageColumnName As String = "Invention Facility Usage"

    ' Industry Flip Belt settings
    Private DefaultCycleTime As Double = 180
    Private Defaultm3perCycle As Double = 3000
    Private DefaultNumMiners As Integer = 1
    Private DefaultCompressOre As Boolean = False
    Private DefaultIPHperMiner As Boolean = False
    Private DefaultIncludeBrokerFees As Boolean = True
    Private DefaultIncludeTaxes As Boolean = True
    Private DefaultTruesec As String = ""

    ' Industry flip belt defaults
    Private DefaultPlagioclase As Boolean = True
    Private DefaultSpodumain As Boolean = True
    Private DefaultKernite As Boolean = True
    Private DefaultHedbergite As Boolean = True
    Private DefaultArkonor As Boolean = True
    Private DefaultBistot As Boolean = True
    Private DefaultPyroxeres As Boolean = True
    Private DefaultCrokite As Boolean = True
    Private DefaultJaspet As Boolean = True
    Private DefaultOmber As Boolean = True
    Private DefaultScordite As Boolean = True
    Private DefaultGneiss As Boolean = True
    Private DefaultVeldspar As Boolean = True
    Private DefaultHemorphite As Boolean = True
    Private DefaultDarkOchre As Boolean = True
    Private DefaultMercoxit As Boolean = True
    Private DefaultCrimsonArkonor As Boolean = True
    Private DefaultPrimeArkonor As Boolean = True
    Private DefaultTriclinicBistot As Boolean = True
    Private DefaultMonoclinicBistot As Boolean = True
    Private DefaultSharpCrokite As Boolean = True
    Private DefaultCrystallineCrokite As Boolean = True
    Private DefaultOnyxOchre As Boolean = True
    Private DefaultObsidianOchre As Boolean = True
    Private DefaultVitricHedbergite As Boolean = True
    Private DefaultGlazedHedbergite As Boolean = True
    Private DefaultVividHemorphite As Boolean = True
    Private DefaultRadiantHemorphite As Boolean = True
    Private DefaultPureJaspet As Boolean = True
    Private DefaultPristineJaspet As Boolean = True
    Private DefaultLuminousKernite As Boolean = True
    Private DefaultFieryKernite As Boolean = True
    Private DefaultAzurePlagioclase As Boolean = True
    Private DefaultRichPlagioclase As Boolean = True
    Private DefaultSolidPyroxeres As Boolean = True
    Private DefaultViscousPyroxeres As Boolean = True
    Private DefaultCondensedScordite As Boolean = True
    Private DefaultMassiveScordite As Boolean = True
    Private DefaultBrightSpodumain As Boolean = True
    Private DefaultGleamingSpodumain As Boolean = True
    Private DefaultConcentratedVeldspar As Boolean = True
    Private DefaultDenseVeldspar As Boolean = True
    Private DefaultIridescentGneiss As Boolean = True
    Private DefaultPrismaticGneiss As Boolean = True
    Private DefaultSilveryOmber As Boolean = True
    Private DefaultGoldenOmber As Boolean = True
    Private DefaultMagmaMercoxit As Boolean = True
    Private DefaultVitreousMercoxit As Boolean = True

    ' Default Shopping List Settings
    Private DefaultAlwaysonTop As Boolean = False
    Private DefaultUpdateAssetsWhenUsed As Boolean = False
    Private DefaultFees As Boolean = True
    Private DefaultCalcBuyBuyOrder As Boolean = True
    Private DefaultUsage As Boolean = True
    Private DefaultTotalItemTax As Boolean = True
    Private DefaultTotalItemBrokerFees As Boolean = True

    ' Assets - Item Checks
    Private DefaultAssetItemChecks As Boolean = True
    Private DefaultAssetItemTextFilter As String = ""
    Private DefaultAllItems As Boolean = True
    ' Assets - Main window 
    Private DefaultAssetType As String = "Both"
    Private DefaultAssetSortbyName As Boolean = True

    ' Local versions of settings
    Private ApplicationSettings As ApplicationSettings
    Private POSSettings As PlayerOwnedStationSettings
    Private BPSettings As BPTabSettings
    Private ManufacturingSettings As ManufacturingTabSettings
    Private DatacoreSettings As DataCoreTabSettings
    Private ReactionSettings As ReactionsTabSettings
    Private MiningSettings As MiningTabSettings
    Private UpdatePricesSettings As UpdatePriceTabSettings
    Private IndustryJobsColumnSettings As IndustryJobsColumnSettings
    Private ManufacturingTabColumnSettings As ManufacturingTabColumnSettings
    Private IndustryFlipBeltsSettings As IndustryFlipBeltSettings
    Private ShoppingListTabSettings As ShoppingListSettings
    Private IndustryTeamSettings As TeamSettings

    ' Facilities
    Private ManufacturingFacilitySettings As FacilitySettings
    Private ComponentsManufacturingFacilitySettings As FacilitySettings
    Private CapitalComponentsManufacturingFacilitySettings As FacilitySettings
    Private CapitalManufacturingFacilitySettings As FacilitySettings
    Private SuperManufacturingFacilitySettings As FacilitySettings
    Private T3CruiserManufacturingFacilitySettings As FacilitySettings
    Private T3DestroyerManufacturingFacilitySettings As FacilitySettings
    Private SubsystemManufacturingFacilitySettings As FacilitySettings
    Private BoosterManufacturingFacilitySettings As FacilitySettings
    Private CopyFacilitySettings As FacilitySettings
    Private InventionFacilitySettings As FacilitySettings
    Private T3InventionFacilitySettings As FacilitySettings
    Private NoPOSFacilitySettings As FacilitySettings
    Private POSFuelBlockFacilitySettings As FacilitySettings
    Private POSModuleFacilitySettings As FacilitySettings
    Private POSLargeShipFacilitySettings As FacilitySettings

    ' Teams
    Private BPManufacturingTeamSettings As TeamSettings
    Private BPComponentManufacturingTeamSettings As TeamSettings
    Private BPCopyTeamSettings As TeamSettings
    Private BPInventionTeamSettings As TeamSettings

    Private CalcManufacturingTeamSettings As TeamSettings
    Private CalcComponentManufacturingTeamSettings As TeamSettings
    Private CalcCopyTeamSettings As TeamSettings
    Private CalcInventionTeamSettings As TeamSettings

    ' Multiple versions of Asset windows
    Private AssetWindowSettingsDefault As AssetWindowSettings
    Private AssetWindowSettingsShoppingList As AssetWindowSettings

    ' 5 belt types
    Private IndustryBeltOreChecksSettings1 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings2 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings3 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings4 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings5 As IndustryBeltOreChecks

    Private Const AppSettingsFileName As String = "ApplicationSettings"
    Private Const POSSettingsFileName As String = "POSSettings"
    Private Const BPSettingsFileName As String = "BPTabSettings"
    Private Const ManufacturingSettingsFileName As String = "ManufacturingTabSettings"
    Private Const UpdatePricesFileName As String = "UpdatePricesSettings"
    Private Const DatacoreSettingsFileName As String = "DatacoreSettings"
    Private Const ReactionSettingsFileName As String = "ReactionTabSettings"
    Private Const MiningSettingsFileName As String = "MiningTabSettings"
    Private Const IndustryJobsColumnSettingsFileName As String = "IndustryJobsColumnSettings"
    Private Const ManufacturingTabColumnSettingsFileName As String = "ManufacturingTabColumnSettings"
    Private Const IndustryFlipBeltSettingsFileName As String = "IndustryFlipBeltSettings"
    Private Const ShoppingListSettingsFileName As String = "ShoppingListSettings"

    Private Const ManufacturingFacilitySettingsFileName As String = "ManufacturingFacilitySettings"
    Private Const ComponentsManufacturingFacilitySettingsFileName As String = "ComponentsManufacturingFacilitySettings"
    Private Const CapitalComponentsManufacturingFacilitySettingsFileName As String = "CapitalComponentsManufacturingFacilitySettings"
    Private Const CapitalManufacturingFacilitySettingsFileName As String = "CapitalManufacturingFacilitySettings"
    Private Const SuperCapitalManufacturingFacilitySettingsFileName As String = "SuperCapitalManufacturingFacilitySettings"
    Private Const T3CruiserManufacturingFacilitySettingsFileName As String = "T3CruiserManufacturingFacilitySettings"
    Private Const T3DestroyerManufacturingFacilitySettingsFileName As String = "T3DestroyerManufacturingFacilitySettings"
    Private Const SubsystemManufacturingFacilitySettingsFileName As String = "SubsystemManufacturingFacilitySettings"
    Private Const BoosterManufacturingFacilitySettingsFileName As String = "BoosterManufacturingFacilitySettings"
    Private Const CopyFacilitySettingsFileName As String = "CopyFacilitySettings"
    Private Const InventionFacilitySettingsFileName As String = "InventionFacilitySettings"
    Private Const T3InventionFacilitySettingsFileName As String = "T3InventionFacilitySettings"
    Private Const NoPoSFacilitySettingsFileName As String = "NoPOSFacilitySettings"

    Private Const POSFuelBlockFacilitySettingsFileName As String = "POSFuelBlockFacilitySettings"
    Private Const POSLargeShipFacilitySettingsFileName As String = "POSLargeShipFacilitySettings"
    Private Const POSModuleFacilitySettingsFileName As String = "POSModuleFacilitySettings"

    Private Const ManufacturingTeamSettingsFileName As String = "ManufacturingTeamSettings"
    Private Const ComponentManufacturingTeamSettingsFileName As String = "ComponentsManufacturingTeamSettings"
    Private Const CopyTeamSettingsFileName As String = "CopyTeamSettings"
    Private Const InventionTeamSettingsFileName As String = "InventionTeamSettings"

    ' 5 belts
    Private IndustryBeltOreChecksFileName As String = IndustryBeltOreChecksFileName
    Private Const IndustryBeltOreChecksFileName1 As String = "1"
    Private Const IndustryBeltOreChecksFileName2 As String = "2"
    Private Const IndustryBeltOreChecksFileName3 As String = "3"
    Private Const IndustryBeltOreChecksFileName4 As String = "4"
    Private Const IndustryBeltOreChecksFileName5 As String = "5"

    ' Multiple asset windows
    Private Const AssetWindowFileNameDefault As String = "AssetWindowSettingsDefault"
    Private Const AssetWindowFileNameShoppingList As String = "AssetWindowSettingsShoppingList"

    Public Const SettingsFolder As String = "Settings/"
    Private Const XMLfileType As String = ".xml"

    Public Sub New()
        ApplicationSettings = Nothing
        MiningSettings = Nothing
        POSSettings = Nothing
        BPSettings = Nothing
        ManufacturingSettings = Nothing
        DatacoreSettings = Nothing
        ReactionSettings = Nothing
        MiningSettings = Nothing
        UpdatePricesSettings = Nothing
        IndustryJobsColumnSettings = Nothing
    End Sub

    ' Writes the sent settings to the sent file name
    Private Sub WriteSettingsToFile(FileName As String, Settings As Setting(), RootName As String)
        Dim i As Integer

        ' Create XmlWriterSettings.
        Dim XMLSettings As XmlWriterSettings = New XmlWriterSettings()
        XMLSettings.Indent = True

        If Not Directory.Exists(SettingsFolder) Then
            ' Create the settings folder
            Directory.CreateDirectory(SettingsFolder)
        End If

        ' Delete and make a fresh copy
        If File.Exists(SettingsFolder & FileName & XMLfileType) Then
            File.Delete(SettingsFolder & FileName & XMLfileType)
        End If

        ' Loop through the settings sent and output each name and value
        Using writer As XmlWriter = XmlWriter.Create(SettingsFolder & FileName & XMLfileType, XMLSettings)
            writer.WriteStartDocument()
            writer.WriteStartElement(RootName) ' Root.

            ' Main loop
            For i = 0 To Settings.Count - 1
                writer.WriteElementString(Settings(i).Name, Settings(i).Value)
            Next

            ' End document.
            writer.WriteEndDocument()
        End Using

    End Sub

    ' Gets a value from a referenced XML file by searching for it
    Private Function GetSettingValue(ByRef FileName As String, ObjectType As SettingTypes, RootElement As String, ElementString As String, DefaultValue As Object) As Object
        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList

        Dim TempValue As String

        'Load the Xml file
        m_xmld.Load(SettingsFolder & FileName & XMLfileType)

        'Get the settings

        ' Get the cache update
        m_nodelist = m_xmld.SelectNodes("/" & RootElement & "/" & ElementString)

        If Not IsNothing(m_nodelist.Item(0)) Then
            ' Should only be one
            TempValue = m_nodelist.Item(0).InnerText

            ' If blank, then return default
            If TempValue = "" Then
                Return DefaultValue
            End If

            ' Found it, return the cast
            Select Case ObjectType
                Case SettingTypes.TypeBoolean
                    Return CBool(TempValue)
                Case SettingTypes.TypeDouble
                    Return CDbl(TempValue)
                Case SettingTypes.TypeInteger
                    Return CInt(TempValue)
                Case SettingTypes.TypeString
                    Return CStr(TempValue)
                Case SettingTypes.TypeLong
                    Return CLng(TempValue)
            End Select

        Else
            ' Doesn't exist, use default
            Return DefaultValue
        End If

        Return Nothing

    End Function

    ' Just checks if the file exists or not so we don't have to mess with file names
    Private Function FileExists(FileName As String) As Boolean

        If File.Exists(SettingsFolder & FileName & XMLfileType) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Structure Setting
        Dim Name As String
        Dim Value As String

        Public Sub New(inName As String, inValue As String)
            Name = inName
            Value = inValue
        End Sub

    End Structure

    Private Enum SettingTypes
        TypeInteger = 1
        TypeDouble = 2
        TypeString = 3
        TypeBoolean = 4
        TypeLong = 5
    End Enum

#Region "Application Settings"

    ' Loads the settings for the user from the DB (for now) for the whole program
    Public Function LoadApplicationSettings() As ApplicationSettings
        Dim TempSettings As ApplicationSettings = Nothing

        Try
            If FileExists(AppSettingsFileName) Then

                'Get the settings
                With TempSettings
                    .CheckforUpdatesonStart = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "CheckforUpdatesonStart", DefaultCheckUpdatesOnStart))
                    .LoadAssetsonStartup = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadAssetsonStartup", DefaultLoadAssetsonStartup))
                    .LoadBPsonStartup = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadbpsonStartup", DefaultLoadBPsonStartup))
                    .LoadCRESTTeamDataonStartup = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadCRESTTeamDataonStartup", DefaultRefreshTeamCRESTDataonStartup))
                    .LoadCRESTMarketDataonStartup = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadCRESTMarketDataonStartup", DefaultRefreshMarketCRESTDataonStartup))
                    .LoadCRESTFacilityDataonStartup = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadCRESTFacilityDataonStartup", DefaultRefreshFacilityCRESTDataonStartup))
                    .DataExportFormat = CStr(GetSettingValue(AppSettingsFileName, SettingTypes.TypeString, AppSettingsFileName, "DataExportFormat", DefaultDataExportFormat))
                    .AllowSkillOverride = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "AllowSkillOverride", DefaultAllowSkillOverride))
                    .ShowToolTips = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShowToolTips", DefaultShowToolTips))
                    .RefiningImplantValue = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "RefiningImplantValue", DefaultImplantValues))
                    .ManufacturingImplantValue = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "ManufacturingImplantValue", DefaultImplantValues))
                    .CopyImplantValue = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "CopyImplantValue", DefaultImplantValues))
                    .BrokerCorpStanding = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "BrokerCorpStanding", DefaultBrokerCorpStanding))
                    .RefineCorpStanding = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "RefineCorpStanding", DefaultRefineCorpStanding))
                    .BrokerFactionStanding = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "BrokerFactionStanding", DefaultBrokerFactionStanding))
                    .DefaultBPME = CInt(GetSettingValue(AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "DefaultBPME", DefaultSettingME))
                    .DefaultBPTE = CInt(GetSettingValue(AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "DefaultBPTE", DefaultSettingTE))
                    .CheckBuildBuy = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "CheckBuildBuy", DefaultCheckBuildBuy))
                    .DisableSVR = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "DisableSVR", DefaultDisableSVR))
                    .RefiningEfficiency = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "RefiningEfficiency", DefaultRefiningEfficency))
                    .RefiningTax = CDbl(GetSettingValue(AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "RefiningTax", DefaultRefineTax))
                    .ShopListIncludeInventMats = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShopListIncludeInventMats", DefaultShopListIncludeInventMats))
                    .ShopListIncludeCopyMats = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShopListIncludeCopyMats", DefaultShopListIncludeCopyMats))
                    .SuggestBuildBPNotOwned = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SuggestBuildBPNotOwned", DefaultSuggestBuildBPNotOwned))
                    .EVECentralRefreshInterval = CInt(GetSettingValue(AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "EVECentralRefreshInterval", DefaultEVECentralRefreshInterval))
                    .DisableSound = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "DisableSound", DefaultDisableSound))
                    .SaveBPRelicsDecryptors = CBool(GetSettingValue(AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SaveBPRelicsDecryptors", DefaultSaveBPRelicsDecryptors))
                End With

                Select Case TempSettings.RefiningEfficiency
                    Case 0.5, 0.52, 0.53, 0.54, 0.57, 0.6
                        ' Do nothing
                    Case Else
                        ' Set to the default
                        TempSettings.RefiningEfficiency = DefaultRefiningEfficency
                End Select

            Else
                ' Load defaults 
                TempSettings = SetDefaultApplicationSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Application Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Some other error occured Load defaults 
            TempSettings = SetDefaultApplicationSettings()
        End Try

        ' Save them locally and then export
        ApplicationSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the defaults
    Public Function SetDefaultApplicationSettings() As ApplicationSettings
        Dim TempSettings As ApplicationSettings

        ' Load default settings
        TempSettings.CheckforUpdatesonStart = DefaultCheckUpdatesOnStart
        TempSettings.DataExportFormat = DefaultDataExportFormat
        TempSettings.ShowToolTips = DefaultShowToolTips
        TempSettings.LoadAssetsonStartup = DefaultLoadAssetsonStartup
        TempSettings.LoadBPsonStartup = DefaultLoadBPsonStartup
        TempSettings.LoadCRESTTeamDataonStartup = DefaultRefreshTeamCRESTDataonStartup
        TempSettings.LoadCRESTMarketDataonStartup = DefaultRefreshMarketCRESTDataonStartup
        TempSettings.LoadCRESTFacilityDataonStartup = DefaultRefreshFacilityCRESTDataonStartup
        TempSettings.DisableSound = DefaultDisableSound
        TempSettings.ManufacturingImplantValue = DefaultImplantValues
        TempSettings.RefiningImplantValue = DefaultImplantValues
        TempSettings.CopyImplantValue = DefaultImplantValues

        ' Station Standings for building and selling
        TempSettings.BrokerCorpStanding = DefaultBrokerCorpStanding
        TempSettings.BrokerFactionStanding = DefaultBrokerFactionStanding
        TempSettings.RefineCorpStanding = DefaultRefineCorpStanding

        TempSettings.CheckBuildBuy = DefaultCheckBuildBuy

        TempSettings.DefaultBPME = DefaultSettingME
        TempSettings.DefaultBPTE = DefaultSettingTE

        TempSettings.RefiningEfficiency = DefaultRefiningEfficency

        TempSettings.RefiningTax = DefaultRefineTax

        TempSettings.DisableSVR = DefaultDisableSVR
        TempSettings.SuggestBuildBPNotOwned = DefaultSuggestBuildBPNotOwned
        TempSettings.SaveBPRelicsDecryptors = DefaultSaveBPRelicsDecryptors

        TempSettings.ShopListIncludeInventMats = DefaultShopListIncludeInventMats
        TempSettings.ShopListIncludeCopyMats = DefaultShopListIncludeCopyMats

        TempSettings.EVECentralRefreshInterval = DefaultEVECentralRefreshInterval

        ' Save locally
        ApplicationSettings = TempSettings
        Return TempSettings

    End Function

    ' Saves the application settings to XML
    Public Sub SaveApplicationSettings(SentSettings As ApplicationSettings)
        Dim ApplicationSettingsList(25) As Setting

        Try
            ApplicationSettingsList(0) = New Setting("CheckforUpdatesonStart", CStr(SentSettings.CheckforUpdatesonStart))
            ApplicationSettingsList(1) = New Setting("DataExportFormat", CStr(SentSettings.DataExportFormat))
            ApplicationSettingsList(2) = New Setting("AllowSkillOverride", CStr(SentSettings.AllowSkillOverride))
            ApplicationSettingsList(3) = New Setting("ShowToolTips", CStr(SentSettings.ShowToolTips))
            ApplicationSettingsList(4) = New Setting("RefiningImplantValue", CStr(SentSettings.RefiningImplantValue))
            ApplicationSettingsList(5) = New Setting("ManufacturingImplantValue", CStr(SentSettings.ManufacturingImplantValue))
            ApplicationSettingsList(6) = New Setting("CopyImplantValue", CStr(SentSettings.CopyImplantValue))
            ApplicationSettingsList(7) = New Setting("BrokerCorpStanding", CStr(SentSettings.BrokerCorpStanding))
            ApplicationSettingsList(8) = New Setting("RefineCorpStanding", CStr(SentSettings.RefineCorpStanding))
            ApplicationSettingsList(9) = New Setting("BrokerFactionStanding", CStr(SentSettings.BrokerFactionStanding))
            ApplicationSettingsList(10) = New Setting("DefaultBPME", CStr(SentSettings.DefaultBPME))
            ApplicationSettingsList(11) = New Setting("DefaultBPTE", CStr(SentSettings.DefaultBPTE))
            ApplicationSettingsList(12) = New Setting("CheckBuildBuy", CStr(SentSettings.CheckBuildBuy))
            ApplicationSettingsList(13) = New Setting("RefiningEfficiency", CStr(SentSettings.RefiningEfficiency))
            ApplicationSettingsList(14) = New Setting("RefiningTax", CStr(SentSettings.RefiningTax))
            ApplicationSettingsList(15) = New Setting("ShopListIncludeInventMats", CStr(SentSettings.ShopListIncludeInventMats))
            ApplicationSettingsList(16) = New Setting("ShopListIncludeCopyMats", CStr(SentSettings.ShopListIncludeCopyMats))
            ApplicationSettingsList(17) = New Setting("SuggestBuildBPNotOwned", CStr(SentSettings.SuggestBuildBPNotOwned))
            ApplicationSettingsList(18) = New Setting("EVECentralRefreshInterval", CStr(SentSettings.EVECentralRefreshInterval))
            ApplicationSettingsList(19) = New Setting("LoadAssetsonStartup", CStr(SentSettings.LoadAssetsonStartup))
            ApplicationSettingsList(20) = New Setting("DisableSound", CStr(SentSettings.DisableSound))
            ApplicationSettingsList(21) = New Setting("LoadbpsonStartup", CStr(SentSettings.LoadBPsonStartup))
            ApplicationSettingsList(22) = New Setting("LoadCRESTTeamDataonStartup", CStr(SentSettings.LoadCRESTTeamDataonStartup))
            ApplicationSettingsList(23) = New Setting("LoadCRESTFacilityDataonStartup", CStr(SentSettings.LoadCRESTFacilityDataonStartup))
            ApplicationSettingsList(24) = New Setting("LoadCRESTMarketDataonStartup", CStr(SentSettings.LoadCRESTMarketDataonStartup))
            ApplicationSettingsList(25) = New Setting("SaveBPRelicsDecryptors", CStr(SentSettings.SaveBPRelicsDecryptors))

            Call WriteSettingsToFile(AppSettingsFileName, ApplicationSettingsList, AppSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Application Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the application settings
    Public Function GetApplicationSettings() As ApplicationSettings
        Return ApplicationSettings
    End Function

#End Region

#Region "Shopping List Settings"

    ' Loads the POS tower settings from XML setting file
    Public Function LoadShoppingListSettings() As ShoppingListSettings
        Dim TempSettings As ShoppingListSettings = Nothing

        Try
            If FileExists(ShoppingListSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .DataExportFormat = CStr(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeString, ShoppingListSettingsFileName, "DataExportFormat", DefaultDataExportFormat))
                    .AlwaysonTop = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "AlwaysonTop", DefaultAlwaysonTop))
                    .UpdateAssetsWhenUsed = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "UpdateAssetsWhenUsed", DefaultUpdateAssetsWhenUsed))
                    .Fees = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "Fees", DefaultFees))
                    .CalcBuyBuyOrder = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "CalcBuyBuyOrder", DefaultCalcBuyBuyOrder))
                    .Usage = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "Usage", DefaultUsage))
                    .TotalItemTax = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "TotalItemTax", DefaultTotalItemTax))
                    .TotalItemBrokerFees = CBool(GetSettingValue(ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "TotalItemBrokerFees", DefaultTotalItemBrokerFees))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultShopingListSettings()
            End If
        Catch ex As Exception
            MsgBox("An error occured when loading Shopping List Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultShopingListSettings()
        End Try

        ' Save them locally and then export
        ShoppingListTabSettings = TempSettings

        Return TempSettings

    End Function

    ' Load defaults 
    Public Function SetDefaultShopingListSettings() As ShoppingListSettings
        Dim TempSettings As ShoppingListSettings = Nothing

        ' Load defaults 
        TempSettings.DataExportFormat = DefaultDataExportFormat
        TempSettings.AlwaysonTop = DefaultAlwaysonTop
        TempSettings.UpdateAssetsWhenUsed = DefaultUpdateAssetsWhenUsed
        TempSettings.UpdateAssetsWhenUsed = DefaultUpdateAssetsWhenUsed
        TempSettings.Fees = DefaultFees
        TempSettings.CalcBuyBuyOrder = DefaultCalcBuyBuyOrder
        TempSettings.Usage = DefaultUsage
        TempSettings.TotalItemTax = DefaultTotalItemTax
        TempSettings.TotalItemBrokerFees = DefaultTotalItemBrokerFees

        ShoppingListTabSettings = TempSettings

        Return TempSettings

    End Function

    ' Saves the Shopping List Settings to XML
    Public Sub SaveShoppingListSettings(SentSettings As ShoppingListSettings)
        Dim ShoppingListSettingsList(7) As Setting

        Try
            ShoppingListSettingsList(0) = New Setting("DataExportFormat", CStr(SentSettings.DataExportFormat))
            ShoppingListSettingsList(1) = New Setting("AlwaysonTop", CStr(SentSettings.AlwaysonTop))
            ShoppingListSettingsList(2) = New Setting("UpdateAssetsWhenUsed", CStr(SentSettings.UpdateAssetsWhenUsed))
            ShoppingListSettingsList(3) = New Setting("Fees", CStr(SentSettings.Fees))
            ShoppingListSettingsList(4) = New Setting("CalcBuyBuyOrder", CStr(SentSettings.CalcBuyBuyOrder))
            ShoppingListSettingsList(5) = New Setting("Usage", CStr(SentSettings.Usage))
            ShoppingListSettingsList(6) = New Setting("TotalItemTax", CStr(SentSettings.TotalItemTax))
            ShoppingListSettingsList(7) = New Setting("TotalItemBrokerFees", CStr(SentSettings.TotalItemBrokerFees))

            Call WriteSettingsToFile(ShoppingListSettingsFileName, ShoppingListSettingsList, ShoppingListSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Shopping List Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the Shopping List Settings
    Public Function GetShoppingListSettings() As ShoppingListSettings
        Return ShoppingListTabSettings
    End Function

#End Region

#Region "BP Tab Settings"

    ' Loads the tab settings
    Public Function LoadBPSettings() As BPTabSettings
        Dim TempSettings As BPTabSettings = Nothing

        Try
            If FileExists(BPSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .BlueprintTypeSelection = CStr(GetSettingValue(BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "BlueprintTypeSelection", DefaultBPSelectionType))
                    .Tech1Check = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "Tech1Check", DefaultBPTechChecks))
                    .Tech2Check = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "Tech2Check", DefaultBPTechChecks))
                    .Tech3Check = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "Tech3Check", DefaultBPTechChecks))
                    .TechStorylineCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "TechStorylineCheck", DefaultBPTechChecks))
                    .TechFactionCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "TechFactionCheck", DefaultBPTechChecks))
                    .TechPirateCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "TechPirateCheck", DefaultBPTechChecks))
                    .IncludeUsage = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeUsage", DefaultBPIncludeUsage))
                    .IncludeTaxes = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeTaxes", DefaultBPIncludeTaxes))
                    .PricePerUnit = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "PricePerUnit", DefaultBPPricePerUnit))
                    .IncludeInventionCost = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeInventionCost", DefaultBPIncludeInventionCost))
                    .IncludeInventionTime = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeInventionTime", DefaultBPIncludeInventionTime))
                    .IncludeCopyCost = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeCopyCost", DefaultBPIncludecopyCost))
                    .IncludeCopyTime = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeCopyTime", DefaultBPIncludeCopyTime))
                    .IncludeT3Cost = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeT3Cost", DefaultBPIncludeT3Cost))
                    .IncludeT3Time = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeT3Time", DefaultBPIncludeT3Time))
                    .ProductionLines = CInt(GetSettingValue(BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "ProductionLines", DefaultBPProductionLines))
                    .LaboratoryLines = CInt(GetSettingValue(BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "LaboratoryLines", DefaultBPLaboratoryLines))
                    .T3Lines = CInt(GetSettingValue(BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "RELines", DefaultBPRELines))
                    .SmallCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SmallCheck", DefaultSizeChecks))
                    .MediumCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SmallCheck", DefaultSizeChecks))
                    .LargeCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SmallCheck", DefaultSizeChecks))
                    .XLCheck = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SmallCheck", DefaultSizeChecks))
                    .IncludeFees = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeFees", DefaultBPIncludeFees))
                    .RelicType = CStr(GetSettingValue(BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "RelicType", DefaultBPRelicType))
                    .T2DecryptorType = CStr(GetSettingValue(BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "T2DecryptorType", DefaultBPT2DecryptorType))
                    .T3DecryptorType = CStr(GetSettingValue(BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "T3DecryptorType", DefaultBPT3DecryptorType))
                    .IgnoreInvention = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IgnoreInvention", DefaultBPIgnoreInvention))
                    .IgnoreMinerals = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IgnoreMinerals", DefaultBPIgnoreMinerals))
                    .IgnoreT1Item = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IgnoreT1Item", DefaultBPIgnoreT1Item))
                    .IncludeIgnoredBPs = CBool(GetSettingValue(BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeIgnoredBPs", DefaultBPIgnoreT1Item))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultBPSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading BP Tab Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultBPSettings()
        End Try

        ' Save them locally and then export
        BPSettings = TempSettings

        Return TempSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveBPSettings(SentSettings As BPTabSettings)
        Dim BPSettingsList(30) As Setting

        Try
            BPSettingsList(0) = New Setting("BlueprintTypeSelection", CStr(SentSettings.BlueprintTypeSelection))
            BPSettingsList(1) = New Setting("Tech1Check", CStr(SentSettings.Tech1Check))
            BPSettingsList(2) = New Setting("Tech2Check", CStr(SentSettings.Tech2Check))
            BPSettingsList(3) = New Setting("Tech3Check", CStr(SentSettings.Tech3Check))
            BPSettingsList(4) = New Setting("TechStorylineCheck", CStr(SentSettings.TechStorylineCheck))
            BPSettingsList(5) = New Setting("TechFactionCheck", CStr(SentSettings.TechFactionCheck))
            BPSettingsList(6) = New Setting("TechPirateCheck", CStr(SentSettings.TechPirateCheck))
            BPSettingsList(7) = New Setting("IncludeUsage", CStr(SentSettings.IncludeUsage))
            BPSettingsList(8) = New Setting("IncludeTaxes", CStr(SentSettings.IncludeTaxes))
            BPSettingsList(9) = New Setting("PricePerUnit", CStr(SentSettings.PricePerUnit))
            BPSettingsList(10) = New Setting("ProductionLines", CStr(SentSettings.ProductionLines))
            BPSettingsList(11) = New Setting("LaboratoryLines", CStr(SentSettings.LaboratoryLines))
            BPSettingsList(12) = New Setting("RELines", CStr(SentSettings.T3Lines))
            BPSettingsList(13) = New Setting("SmallCheck", CStr(SentSettings.SmallCheck))
            BPSettingsList(14) = New Setting("MediumCheck", CStr(SentSettings.MediumCheck))
            BPSettingsList(15) = New Setting("LargeCheck", CStr(SentSettings.LargeCheck))
            BPSettingsList(16) = New Setting("XLCheck", CStr(SentSettings.XLCheck))
            BPSettingsList(17) = New Setting("IncludeFees", CStr(SentSettings.IncludeFees))

            BPSettingsList(18) = New Setting("IncludeInventionCost", CStr(SentSettings.IncludeInventionCost))
            BPSettingsList(19) = New Setting("IncludeInventionTime", CStr(SentSettings.IncludeInventionTime))
            BPSettingsList(20) = New Setting("IncludeCopyCost", CStr(SentSettings.IncludeCopyCost))
            BPSettingsList(21) = New Setting("IncludeCopyTime", CStr(SentSettings.IncludeCopyTime))
            BPSettingsList(22) = New Setting("IncludeT3Cost", CStr(SentSettings.IncludeT3Cost))
            BPSettingsList(23) = New Setting("IncludeT3Time", CStr(SentSettings.IncludeT3Time))
            BPSettingsList(24) = New Setting("RelicType", CStr(SentSettings.RelicType))
            BPSettingsList(25) = New Setting("T2DecryptorType", CStr(SentSettings.T2DecryptorType))

            BPSettingsList(26) = New Setting("IgnoreInvention", CStr(SentSettings.IgnoreInvention))
            BPSettingsList(27) = New Setting("IgnoreMinerals", CStr(SentSettings.IgnoreMinerals))
            BPSettingsList(28) = New Setting("IgnoreT1Item", CStr(SentSettings.IgnoreT1Item))

            BPSettingsList(29) = New Setting("IncludeIgnoredBPs", CStr(SentSettings.IncludeIgnoredBPs))
            BPSettingsList(30) = New Setting("T3DecryptorType", CStr(SentSettings.T3DecryptorType))

            Call WriteSettingsToFile(BPSettingsFileName, BPSettingsList, BPSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving BP Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetBPSettings() As BPTabSettings
        Return BPSettings
    End Function

    ' Loads the defaults
    Public Function SetDefaultBPSettings() As BPTabSettings
        Dim LocalSettings As BPTabSettings

        LocalSettings.BlueprintTypeSelection = DefaultBPSelectionType
        LocalSettings.Tech1Check = DefaultBPTechChecks
        LocalSettings.Tech2Check = DefaultBPTechChecks
        LocalSettings.Tech3Check = DefaultBPTechChecks
        LocalSettings.TechStorylineCheck = DefaultBPTechChecks
        LocalSettings.TechFactionCheck = DefaultBPTechChecks
        LocalSettings.TechPirateCheck = DefaultBPTechChecks
        LocalSettings.IncludeUsage = DefaultBPIncludeFees
        LocalSettings.IncludeTaxes = DefaultBPIncludeTaxes
        LocalSettings.IncludeFees = DefaultIncludeBrokerFees
        LocalSettings.PricePerUnit = DefaultBPPricePerUnit
        LocalSettings.ProductionLines = DefaultBPProductionLines
        LocalSettings.LaboratoryLines = DefaultBPLaboratoryLines
        LocalSettings.T3Lines = DefaultBPRELines
        LocalSettings.SmallCheck = DefaultSizeChecks
        LocalSettings.MediumCheck = DefaultSizeChecks
        LocalSettings.LargeCheck = DefaultSizeChecks
        LocalSettings.XLCheck = DefaultSizeChecks

        LocalSettings.IncludeInventionCost = DefaultBPIncludeInventionCost
        LocalSettings.IncludeInventionTime = DefaultBPIncludeInventionTime
        LocalSettings.IncludeCopyCost = DefaultBPIncludecopyCost
        LocalSettings.IncludeCopyTime = DefaultBPIncludeCopyTime
        LocalSettings.IncludeT3Cost = DefaultBPIncludeT3Cost
        LocalSettings.IncludeT3Time = DefaultBPIncludeT3Time

        LocalSettings.RelicType = DefaultBPRelicType
        LocalSettings.T2DecryptorType = DefaultBPT2DecryptorType
        LocalSettings.T3DecryptorType = DefaultBPT3DecryptorType

        LocalSettings.IgnoreInvention = DefaultBPIgnoreInvention
        LocalSettings.IgnoreMinerals = DefaultBPIgnoreMinerals
        LocalSettings.IgnoreT1Item = DefaultBPIgnoreT1Item

        LocalSettings.IncludeIgnoredBPs = DefaultBPIncludeIgnoredBPs

        ' Save locally
        BPSettings = LocalSettings

        Return LocalSettings

    End Function

#End Region

#Region "Update Price Tab Settings"

    ' Loads the tab settings
    Public Function LoadUpdatePricesSettings() As UpdatePriceTabSettings
        Dim TempSettings As UpdatePriceTabSettings = Nothing

        Try
            If FileExists(UpdatePricesFileName) Then

                'Get the settings
                With TempSettings
                    .AllRawMats = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AllRawMats", DefaultPriceChecks))
                    .Minerals = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Minerals", DefaultPriceChecks))
                    .IceProducts = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "IceProducts", DefaultPriceChecks))
                    .Gas = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Gas", DefaultPriceChecks))
                    .Misc = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Misc", DefaultPriceChecks))
                    .AncientRelics = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AncientRelics", DefaultPriceChecks))
                    .AncientSalvage = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AncientSalvage", DefaultPriceChecks))
                    .Salvage = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Salvage", DefaultPriceChecks))
                    .StationComponents = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "StationComponents", DefaultPriceChecks))
                    .Planetary = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Planetary", DefaultPriceChecks))
                    .Datacores = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Datacores", DefaultPriceChecks))
                    .Decryptors = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Decryptors", DefaultPriceChecks))
                    .Deployables = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Deployables", DefaultPriceChecks))
                    .Celestials = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Celestials", DefaultPriceChecks))
                    .Deployables = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Deployables", DefaultPriceChecks))
                    .Implants = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Implants", DefaultPriceChecks))
                    .RawMats = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "RawMats", DefaultPriceChecks))
                    .ProcessedMats = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "ProcessedMats", DefaultPriceChecks))
                    .AdvancedMats = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AdvancedMats", DefaultPriceChecks))
                    .MatsandCompounds = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "MatsandCompounds", DefaultPriceChecks))
                    .DroneComponents = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "DroneComponents", DefaultPriceChecks))
                    .BoosterMats = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "BoosterMats", DefaultPriceChecks))
                    .Polymers = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Polymers", DefaultPriceChecks))
                    .Asteroids = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Asteroids", DefaultPriceChecks))
                    .AllManufacturedItems = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AllManufacturedItems", DefaultPriceChecks))
                    .Ships = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Ships", DefaultPriceChecks))
                    .Modules = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Modules", DefaultPriceChecks))
                    .Drones = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Drones", DefaultPriceChecks))
                    .Boosters = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Boosters", DefaultPriceChecks))
                    .Rigs = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Rigs", DefaultPriceChecks))
                    .Charges = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Charges", DefaultPriceChecks))
                    .Subsystems = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Subsystems", DefaultPriceChecks))
                    .Structures = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Structures", DefaultPriceChecks))
                    .Tools = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Tools", DefaultPriceChecks))
                    .CapT2Components = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "CapT2Components", DefaultPriceChecks))
                    .CapitalComponents = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "CapitalComponents", DefaultPriceChecks))
                    .Components = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Components", DefaultPriceChecks))
                    .Hybrid = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Hybrid", DefaultPriceChecks))
                    .FuelBlocks = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "FuelBlocks", DefaultPriceChecks))
                    .T1 = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "T1", DefaultPriceChecks))
                    .T2 = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "T2", DefaultPriceChecks))
                    .T3 = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "T3", DefaultPriceChecks))
                    .Faction = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Faction", DefaultPriceChecks))
                    .Pirate = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Pirate", DefaultPriceChecks))
                    .Storyline = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Storyline", DefaultPriceChecks))

                    Dim TempRegions As String = CStr(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "SelectedRegions", DefaultPriceRegion))
                    Dim RegionList As New List(Of String)
                    Dim RegionCount As Integer

                    If TempRegions <> "0" Then
                        RegionCount = System.Text.RegularExpressions.Regex.Matches(TempRegions, Regex.Escape(",")).Count + 1 ' Add one for last item + 1 ' Add one for last item
                    End If

                    Dim ReaderStartPosition As Integer = 0
                    Dim CommaLoc As Integer

                    For i = 0 To RegionCount - 1
                        CommaLoc = InStr(TempRegions.Substring(ReaderStartPosition), ",")
                        If CommaLoc <> 0 Then
                            RegionList.Add(TempRegions.Substring(ReaderStartPosition, CommaLoc - 1))
                        Else ' At the end
                            RegionList.Add(TempRegions.Substring(ReaderStartPosition))
                        End If
                        ReaderStartPosition = ReaderStartPosition + CommaLoc
                    Next

                    .SelectedRegions = RegionList
                    .SelectedSystem = CStr(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "SelectedSystem", DefaultPriceSystem))
                    .PriceImportType = CStr(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PriceImportType", DefaultPriceImportPriceType))
                    .ItemsCombo = CStr(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "ItemsCombo", DefaultPriceItemsCombo))
                    .RawMatsCombo = CStr(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "RawMatsCombo", DefaultPriceRawMatsCombo))
                    .UpdatePriceHistory = CBool(GetSettingValue(UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "UpdatePriceHistory", DefaultPriceCRESTHistory))

                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultUpdatePriceSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Update Prices Tab Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultUpdatePriceSettings()
        End Try

        ' Save them locally and then export
        UpdatePricesSettings = TempSettings

        Return TempSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveUpdatePricesSettings(PriceSettings As UpdatePriceTabSettings)
        Dim UpdatePricesSettingsList(49) As Setting

        Try
            UpdatePricesSettingsList(0) = New Setting("AllRawMats", CStr(PriceSettings.AllRawMats))
            UpdatePricesSettingsList(1) = New Setting("Minerals", CStr(PriceSettings.Minerals))
            UpdatePricesSettingsList(2) = New Setting("IceProducts", CStr(PriceSettings.IceProducts))
            UpdatePricesSettingsList(3) = New Setting("Gas", CStr(PriceSettings.Gas))
            UpdatePricesSettingsList(4) = New Setting("AncientRelics", CStr(PriceSettings.AncientRelics))
            UpdatePricesSettingsList(5) = New Setting("AncientSalvage", CStr(PriceSettings.AncientSalvage))
            UpdatePricesSettingsList(6) = New Setting("Salvage", CStr(PriceSettings.Salvage))
            UpdatePricesSettingsList(7) = New Setting("StationComponents", CStr(PriceSettings.StationComponents))
            UpdatePricesSettingsList(8) = New Setting("Planetary", CStr(PriceSettings.Planetary))
            UpdatePricesSettingsList(9) = New Setting("Datacores", CStr(PriceSettings.Datacores))
            UpdatePricesSettingsList(10) = New Setting("Decryptors", CStr(PriceSettings.Decryptors))
            UpdatePricesSettingsList(11) = New Setting("RawMats", CStr(PriceSettings.RawMats))
            UpdatePricesSettingsList(12) = New Setting("ProcessedMats", CStr(PriceSettings.ProcessedMats))
            UpdatePricesSettingsList(13) = New Setting("AdvancedMats", CStr(PriceSettings.AdvancedMats))
            UpdatePricesSettingsList(14) = New Setting("MatsandCompounds", CStr(PriceSettings.MatsandCompounds))
            UpdatePricesSettingsList(15) = New Setting("DroneComponents", CStr(PriceSettings.DroneComponents))
            UpdatePricesSettingsList(16) = New Setting("BoosterMats", CStr(PriceSettings.BoosterMats))
            UpdatePricesSettingsList(17) = New Setting("Polymers", CStr(PriceSettings.Polymers))
            UpdatePricesSettingsList(18) = New Setting("AllManufacturedItems", CStr(PriceSettings.AllManufacturedItems))
            UpdatePricesSettingsList(19) = New Setting("Ships", CStr(PriceSettings.Ships))
            UpdatePricesSettingsList(20) = New Setting("Modules", CStr(PriceSettings.Modules))
            UpdatePricesSettingsList(21) = New Setting("Drones", CStr(PriceSettings.Drones))
            UpdatePricesSettingsList(22) = New Setting("Boosters", CStr(PriceSettings.Boosters))
            UpdatePricesSettingsList(23) = New Setting("Rigs", CStr(PriceSettings.Rigs))
            UpdatePricesSettingsList(24) = New Setting("Charges", CStr(PriceSettings.Charges))
            UpdatePricesSettingsList(25) = New Setting("Subsystems", CStr(PriceSettings.Subsystems))
            UpdatePricesSettingsList(26) = New Setting("Structures", CStr(PriceSettings.Structures))
            UpdatePricesSettingsList(27) = New Setting("Tools", CStr(PriceSettings.Tools))
            UpdatePricesSettingsList(28) = New Setting("CapT2Components", CStr(PriceSettings.CapT2Components))
            UpdatePricesSettingsList(29) = New Setting("CapitalComponents", CStr(PriceSettings.CapitalComponents))
            UpdatePricesSettingsList(30) = New Setting("Components", CStr(PriceSettings.Components))
            UpdatePricesSettingsList(31) = New Setting("Hybrid", CStr(PriceSettings.Hybrid))
            UpdatePricesSettingsList(32) = New Setting("FuelBlocks", CStr(PriceSettings.FuelBlocks))
            UpdatePricesSettingsList(33) = New Setting("T1", CStr(PriceSettings.T1))
            UpdatePricesSettingsList(34) = New Setting("T2", CStr(PriceSettings.T2))
            UpdatePricesSettingsList(35) = New Setting("T3", CStr(PriceSettings.T3))
            UpdatePricesSettingsList(36) = New Setting("Faction", CStr(PriceSettings.Faction))
            UpdatePricesSettingsList(37) = New Setting("Pirate", CStr(PriceSettings.Pirate))
            UpdatePricesSettingsList(38) = New Setting("Storyline", CStr(PriceSettings.Storyline))
            Dim RegionList As String = ""
            If Not IsNothing(PriceSettings.SelectedRegions) Then
                For i = 0 To PriceSettings.SelectedRegions.Count - 1
                    RegionList = RegionList & PriceSettings.SelectedRegions(i) & ","
                Next
                If RegionList <> "" Then
                    ' Strip last comma
                    RegionList = RegionList.Substring(0, Len(RegionList) - 1)
                End If
            Else
                RegionList = "0"
            End If
            UpdatePricesSettingsList(39) = New Setting("SelectedRegions", RegionList)
            UpdatePricesSettingsList(40) = New Setting("SelectedSystem", CStr(PriceSettings.SelectedSystem))
            UpdatePricesSettingsList(41) = New Setting("PriceImportType", CStr(PriceSettings.PriceImportType))
            UpdatePricesSettingsList(42) = New Setting("ItemsCombo", CStr(PriceSettings.ItemsCombo))
            UpdatePricesSettingsList(43) = New Setting("RawMatsCombo", CStr(PriceSettings.RawMatsCombo))

            UpdatePricesSettingsList(44) = New Setting("Asteroids", CStr(PriceSettings.Asteroids))
            UpdatePricesSettingsList(45) = New Setting("Misc", CStr(PriceSettings.Misc))

            UpdatePricesSettingsList(46) = New Setting("Deployables", CStr(PriceSettings.Deployables))
            UpdatePricesSettingsList(47) = New Setting("Celestials", CStr(PriceSettings.Celestials))
            UpdatePricesSettingsList(48) = New Setting("Implants", CStr(PriceSettings.Implants))

            UpdatePricesSettingsList(49) = New Setting("UpdatePriceHistory", CStr(PriceSettings.UpdatePriceHistory))

            Call WriteSettingsToFile(UpdatePricesFileName, UpdatePricesSettingsList, UpdatePricesFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Update Prices Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetUpdatePricesSettings() As UpdatePriceTabSettings
        Return UpdatePricesSettings
    End Function

    Public Function SetDefaultUpdatePriceSettings() As UpdatePriceTabSettings
        Dim LocalSettings As UpdatePriceTabSettings

        With LocalSettings
            .AllRawMats = DefaultPriceChecks
            .Minerals = DefaultPriceChecks
            .IceProducts = DefaultPriceChecks
            .Gas = DefaultPriceChecks
            .Misc = DefaultPriceChecks
            .AncientRelics = DefaultPriceChecks
            .AncientSalvage = DefaultPriceChecks
            .Salvage = DefaultPriceChecks
            .StationComponents = DefaultPriceChecks
            .Planetary = DefaultPriceChecks
            .Datacores = DefaultPriceChecks
            .Decryptors = DefaultPriceChecks
            .RawMats = DefaultPriceChecks
            .ProcessedMats = DefaultPriceChecks
            .AdvancedMats = DefaultPriceChecks
            .MatsandCompounds = DefaultPriceChecks
            .DroneComponents = DefaultPriceChecks
            .BoosterMats = DefaultPriceChecks
            .Polymers = DefaultPriceChecks
            .Asteroids = DefaultPriceChecks
            .AllManufacturedItems = DefaultPriceChecks
            .Ships = DefaultPriceChecks
            .Modules = DefaultPriceChecks
            .Drones = DefaultPriceChecks
            .Boosters = DefaultPriceChecks
            .Rigs = DefaultPriceChecks
            .Charges = DefaultPriceChecks
            .Subsystems = DefaultPriceChecks
            .Structures = DefaultPriceChecks
            .Tools = DefaultPriceChecks
            .CapT2Components = DefaultPriceChecks
            .CapitalComponents = DefaultPriceChecks
            .Components = DefaultPriceChecks
            .Hybrid = DefaultPriceChecks
            .FuelBlocks = DefaultPriceChecks
            .Implants = DefaultPriceChecks
            .Celestials = DefaultPriceChecks
            .Deployables = DefaultPriceChecks
            .T1 = DefaultPriceChecks
            .T2 = DefaultPriceChecks
            .T3 = DefaultPriceChecks
            .Faction = DefaultPriceChecks
            .Pirate = DefaultPriceChecks
            .Storyline = DefaultPriceChecks
            .SelectedRegions = Nothing
            .SelectedSystem = DefaultPriceSystem
            .PriceImportType = DefaultPriceImportPriceType
            .ItemsCombo = DefaultPriceItemsCombo
            .RawMatsCombo = DefaultPriceRawMatsCombo
            .UpdatePriceHistory = DefaultPriceCRESTHistory
        End With

        ' Save locally
        UpdatePricesSettings = LocalSettings
        Return LocalSettings

    End Function

#End Region

#Region "Manufacturing Tab Settings"

    ' Loads the tab settings
    Public Function LoadManufacturingSettings() As ManufacturingTabSettings
        Dim TempSettings As ManufacturingTabSettings = Nothing

        Try
            If FileExists(ManufacturingSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .BlueprintType = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "BlueprintType", DefaultBlueprintType))
                    .CheckTech1 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTech1", DefaultCheckTech1))
                    .CheckTech2 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTech2", DefaultCheckTech2))
                    .CheckTech3 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTech3", DefaultCheckTech3))
                    .CheckTechStoryline = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTechStoryline", DefaultCheckTechStoryline))
                    .CheckTechNavy = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTechNavy", DefaultCheckTechNavy))
                    .CheckTechPirate = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTechPirate", DefaultCheckTechPirate))
                    .ItemTypeFilter = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "ItemTypeFilter", DefaultItemTypeFilter))
                    .TextItemFilter = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "TextItemFilter", DefaultTextItemFilter))
                    .CheckBPTypeShips = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeShips", DefaultCheckBPTypeShips))
                    .CheckBPTypeDrones = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeDrones", DefaultCheckBPTypeDrones))
                    .CheckBPTypeComponents = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeComponents", DefaultCheckBPTypeComponents))
                    .CheckBPTypeStructures = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeStructures", DefaultCheckBPTypeStructures))
                    .CheckBPTypeMisc = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeMisc", DefaultCheckBPTypeTools))
                    .CheckBPTypeModules = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeModules", DefaultCheckBPTypeModules))
                    .CheckBPTypeAmmoCharges = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeAmmoCharges", DefaultCheckBPTypeAmmoCharges))
                    .CheckBPTypeRigs = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeRigs", DefaultCheckBPTypeRigs))
                    .CheckBPTypeSubsystems = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeSubsystems", DefaultCheckBPTypeSubsystems))
                    .CheckBPTypeBoosters = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeBoosters", DefaultCheckBPTypeBoosters))
                    .CheckBPTypeDeployables = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeDeployables", DefaultCheckBPTypeDeployables))
                    .CheckBPTypeCelestials = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeCelestials", DefaultCheckBPTypeCelestials))
                    .CheckBPTypeStationParts = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeStationParts", DefaultCheckBPTypeStationParts))
                    .AveragePriceDuration = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "AveragePriceDuration", DefaultAveragePriceDuration))
                    .CheckDecryptorNone = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptorNone", DefaultCheckDecryptorNone))
                    .CheckDecryptor06 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor06", DefaultCheckDecryptor06))
                    .CheckDecryptor09 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor09", DefaultCheckDecryptor09))
                    .CheckDecryptor10 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor10", DefaultCheckDecryptor10))
                    .CheckDecryptor11 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor11", DefaultCheckDecryptor11))
                    .CheckDecryptor12 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor12", DefaultCheckDecryptor12))
                    .CheckDecryptor15 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor15", DefaultCheckDecryptor15))
                    .CheckDecryptor18 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor18", DefaultCheckDecryptor18))
                    .CheckDecryptor19 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor19", DefaultCheckDecryptor19))
                    .CheckDecryptorUseforT2 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptorUseforT2", DefaultCheckDecryptorUseforT2))
                    .CheckDecryptorUseforT3 = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptorUseforT3", DefaultCheckDecryptorUseforT3))
                    .CheckIgnoreInvention = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIgnoreInvention", DefaultCheckIgnoreInvention))
                    .CheckRelicWrecked = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRelicWrecked", DefaultCheckRelicWrecked))
                    .CheckRelicIntact = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRelicIntact", DefaultCheckRelicIntact))
                    .CheckRelicMalfunction = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRelicMalfunction", DefaultCheckRelicMalfunction))
                    .CheckOnlyBuild = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckOnlyBuild", DefaultCheckOnlyBuild))
                    .CheckOnlyInvent = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckOnlyInvent", DefaultCheckOnlyInvent))
                    .CheckOnlyRE = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckOnlyRE", DefaultCheckOnlyRE))
                    .CheckIncludeTaxes = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeTaxes", DefaultCheckIncludeTaxes))
                    .CheckIncludeBrokersFees = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeBrokersFees", DefaultIncludeBrokersFees))
                    .CheckIncludeUsage = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeUsage", DefaultCheckIncludeUsage))
                    .CheckRaceAmarr = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceAmarr", DefaultCheckRaceAmarr))
                    .CheckRaceCaldari = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceCaldari", DefaultCheckRaceCaldari))
                    .CheckRaceGallente = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceGallente", DefaultCheckRaceGallente))
                    .CheckRaceMinmatar = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceMinmatar", DefaultCheckRaceMinmatar))
                    .CheckRacePirate = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRacePirate", DefaultCheckRacePirate))
                    .CheckRaceOther = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceOther", DefaultCheckRaceOther))
                    .SortBy = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "SortBy", DefaultSortBy))
                    .PriceCompare = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "PriceCompare", DefaultPriceCompare))
                    .CheckIncludeT2Owned = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeT2Owned", DefaultCheckIncludeT2Owned))
                    .CheckIncludeT3Owned = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeT3Owned", DefaultCheckIncludeT3Owned))
                    .IgnoreSVRThreshold = CDbl(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeDouble, ManufacturingSettingsFileName, "IgnoreLowSVRThreshold", DefaultIgnoreSVRThreshold))
                    .CheckSVRIncludeNull = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckSVRIncludeNull", DefaultCheckSVRIncludeNull))
                    .AveragePriceRegion = CStr(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "AveragePriceRegion", DefaultSVRRegion))
                    .ProductionLines = CInt(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "ProductionLines", DefaultCalcProductionLines))
                    .LaboratoryLines = CInt(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "LaboratoryLines", DefaultCalcLaboratoryLines))
                    .Runs = CInt(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "Runs", DefaultCalcRuns))
                    .BPRuns = CInt(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "BPRuns", DefaultCalcBPRuns))
                    .CheckSmall = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckSmall", DefaultCalcSizeChecks))
                    .CheckMedium = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckMedium", DefaultCalcSizeChecks))
                    .CheckLarge = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckLarge", DefaultCalcSizeChecks))
                    .CheckXL = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckXL", DefaultCalcSizeChecks))
                    .CheckCapitalComponentsFacility = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckCapitalComponentsFacility", DefaultCheckT3Destroyers))
                    .CheckT3DestroyerFacility = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckT3DestroyerFacility", DefaultCheckCapComponents))
                    .CheckAutoCalcNumBPs = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckAutoCalcNumBPs", DefaultCheckAutoCalcNumBPs))
                    .IgnoreInvention = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IgnoreInvention", DefaultCalcIgnoreInvention))
                    .IgnoreMinerals = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IgnoreMinerals", DefaultCalcIgnoreMinerals))
                    .IgnoreT1Item = CBool(GetSettingValue(ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IgnoreT1Item", DefaultCalcIgnoreT1Item))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultManufacturingSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Manufacturing Tab Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultManufacturingSettings()
        End Try

        ' Save them locally and then export
        ManufacturingSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultManufacturingSettings() As ManufacturingTabSettings
        Dim LocalSettings As ManufacturingTabSettings

        With LocalSettings
            .BlueprintType = DefaultBlueprintType
            .CheckTech1 = DefaultCheckTech1
            .CheckTech2 = DefaultCheckTech2
            .CheckTech3 = DefaultCheckTech3
            .CheckTechStoryline = DefaultCheckTechStoryline
            .CheckTechNavy = DefaultCheckTechNavy
            .CheckTechPirate = DefaultCheckTechPirate
            .ItemTypeFilter = DefaultItemTypeFilter
            .TextItemFilter = DefaultTextItemFilter
            .CheckBPTypeShips = DefaultCheckBPTypeShips
            .CheckBPTypeDrones = DefaultCheckBPTypeDrones
            .CheckBPTypeComponents = DefaultCheckBPTypeComponents
            .CheckBPTypeStructures = DefaultCheckBPTypeStructures
            .CheckBPTypeMisc = DefaultCheckBPTypeTools
            .CheckBPTypeModules = DefaultCheckBPTypeModules
            .CheckBPTypeAmmoCharges = DefaultCheckBPTypeAmmoCharges
            .CheckBPTypeRigs = DefaultCheckBPTypeRigs
            .CheckBPTypeSubsystems = DefaultCheckBPTypeSubsystems
            .CheckBPTypeBoosters = DefaultCheckBPTypeBoosters
            .CheckBPTypeCelestials = DefaultCheckBPTypeCelestials
            .CheckBPTypeStationParts = DefaultCheckBPTypeStationParts
            .CheckBPTypeDeployables = DefaultCheckBPTypeDeployables
            .AveragePriceDuration = DefaultAveragePriceDuration
            .CheckDecryptorNone = DefaultCheckDecryptorNone
            .CheckDecryptor06 = DefaultCheckDecryptor06
            .CheckDecryptor09 = DefaultCheckDecryptor09
            .CheckDecryptor10 = DefaultCheckDecryptor10
            .CheckDecryptor11 = DefaultCheckDecryptor11
            .CheckDecryptor12 = DefaultCheckDecryptor12
            .CheckDecryptor15 = DefaultCheckDecryptor15
            .CheckDecryptor18 = DefaultCheckDecryptor18
            .CheckDecryptor19 = DefaultCheckDecryptor19
            .CheckDecryptorUseforT2 = DefaultCheckDecryptorUseforT2
            .CheckDecryptorUseforT3 = defaultCheckDecryptorUseforT3
            .CheckIgnoreInvention = DefaultCheckIgnoreInvention
            .CheckRelicWrecked = DefaultCheckRelicWrecked
            .CheckRelicIntact = DefaultCheckRelicIntact
            .CheckRelicMalfunction = DefaultCheckRelicMalfunction
            .CheckOnlyBuild = DefaultCheckOnlyBuild
            .CheckOnlyInvent = DefaultCheckOnlyInvent
            .CheckOnlyRE = DefaultCheckOnlyRE
            .CheckIncludeTaxes = DefaultCheckIncludeTaxes
            .CheckIncludeBrokersFees = DefaultIncludeBrokersFees
            .CheckIncludeUsage = DefaultCheckIncludeUsage
            .CheckRaceAmarr = DefaultCheckRaceAmarr
            .CheckRaceCaldari = DefaultCheckRaceCaldari
            .CheckRaceGallente = DefaultCheckRaceGallente
            .CheckRaceMinmatar = DefaultCheckRaceMinmatar
            .CheckRacePirate = DefaultCheckRacePirate
            .CheckRaceOther = DefaultCheckRaceOther
            .SortBy = DefaultSortBy
            .PriceCompare = DefaultPriceCompare
            .CheckIncludeT2Owned = DefaultCheckIncludeT2Owned
            .CheckIncludeT3Owned = DefaultCheckIncludeT3Owned
            .IgnoreSVRThreshold = DefaultIgnoreSVRThreshold
            .CheckSVRIncludeNull = DefaultCheckSVRIncludeNull
            .AveragePriceRegion = DefaultSVRRegion
            .ProductionLines = DefaultCalcProductionLines
            .LaboratoryLines = DefaultCalcLaboratoryLines
            .Runs = DefaultCalcRuns
            .BPRuns = DefaultCalcBPRuns
            .CheckSmall = DefaultCalcSizeChecks
            .CheckMedium = DefaultCalcSizeChecks
            .CheckLarge = DefaultCalcSizeChecks
            .CheckXL = DefaultCalcSizeChecks
            .CheckT3DestroyerFacility = DefaultCheckT3Destroyers
            .CheckCapitalComponentsFacility = DefaultCheckCapComponents
            .CheckAutoCalcNumBPs = DefaultCheckAutoCalcNumBPs
            .IgnoreInvention = DefaultCalcIgnoreInvention
            .IgnoreMinerals = DefaultCalcIgnoreMinerals
            .IgnoreT1Item = DefaultCalcIgnoreT1Item
        End With

        ' Save locally
        ManufacturingSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveManufacturingSettings(SentSettings As ManufacturingTabSettings)
        Dim ManufacturingSettingsList(70) As Setting

        Try
            ManufacturingSettingsList(0) = New Setting("BlueprintType", CStr(SentSettings.BlueprintType))
            ManufacturingSettingsList(1) = New Setting("CheckTech1", CStr(SentSettings.CheckTech1))
            ManufacturingSettingsList(2) = New Setting("CheckTech2", CStr(SentSettings.CheckTech2))
            ManufacturingSettingsList(3) = New Setting("CheckTech3", CStr(SentSettings.CheckTech3))
            ManufacturingSettingsList(4) = New Setting("CheckTechStoryline", CStr(SentSettings.CheckTechStoryline))
            ManufacturingSettingsList(5) = New Setting("CheckTechNavy", CStr(SentSettings.CheckTechNavy))
            ManufacturingSettingsList(6) = New Setting("CheckTechPirate", CStr(SentSettings.CheckTechPirate))
            ManufacturingSettingsList(7) = New Setting("ItemTypeFilter", CStr(SentSettings.ItemTypeFilter))
            ManufacturingSettingsList(8) = New Setting("TextItemFilter", CStr(SentSettings.TextItemFilter))
            ManufacturingSettingsList(9) = New Setting("CheckBPTypeShips", CStr(SentSettings.CheckBPTypeShips))
            ManufacturingSettingsList(10) = New Setting("CheckBPTypeDrones", CStr(SentSettings.CheckBPTypeDrones))
            ManufacturingSettingsList(11) = New Setting("CheckBPTypeComponents", CStr(SentSettings.CheckBPTypeComponents))
            ManufacturingSettingsList(12) = New Setting("CheckBPTypeStructures", CStr(SentSettings.CheckBPTypeStructures))
            ManufacturingSettingsList(13) = New Setting("CheckBPTypeMisc", CStr(SentSettings.CheckBPTypeMisc))
            ManufacturingSettingsList(14) = New Setting("CheckBPTypeModules", CStr(SentSettings.CheckBPTypeModules))
            ManufacturingSettingsList(15) = New Setting("CheckBPTypeAmmoCharges", CStr(SentSettings.CheckBPTypeAmmoCharges))
            ManufacturingSettingsList(16) = New Setting("CheckBPTypeRigs", CStr(SentSettings.CheckBPTypeRigs))
            ManufacturingSettingsList(17) = New Setting("CheckBPTypeSubsystems", CStr(SentSettings.CheckBPTypeSubsystems))
            ManufacturingSettingsList(18) = New Setting("CheckBPTypeBoosters", CStr(SentSettings.CheckBPTypeBoosters))
            ManufacturingSettingsList(19) = New Setting("AveragePriceDuration", CStr(SentSettings.AveragePriceDuration))
            ManufacturingSettingsList(20) = New Setting("CheckDecryptorNone", CStr(SentSettings.CheckDecryptorNone))
            ManufacturingSettingsList(21) = New Setting("CheckDecryptor06", CStr(SentSettings.CheckDecryptor06))
            ManufacturingSettingsList(22) = New Setting("CheckDecryptor10", CStr(SentSettings.CheckDecryptor10))
            ManufacturingSettingsList(23) = New Setting("CheckDecryptor11", CStr(SentSettings.CheckDecryptor11))
            ManufacturingSettingsList(24) = New Setting("CheckDecryptor12", CStr(SentSettings.CheckDecryptor12))
            ManufacturingSettingsList(25) = New Setting("CheckDecryptor18", CStr(SentSettings.CheckDecryptor18))
            ManufacturingSettingsList(26) = New Setting("CheckIgnoreInvention", CStr(SentSettings.CheckIgnoreInvention))
            ManufacturingSettingsList(27) = New Setting("CheckRelicWrecked", CStr(SentSettings.CheckRelicWrecked))
            ManufacturingSettingsList(28) = New Setting("CheckRelicIntact", CStr(SentSettings.CheckRelicIntact))
            ManufacturingSettingsList(29) = New Setting("CheckRelicMalfunction", CStr(SentSettings.CheckRelicMalfunction))
            ManufacturingSettingsList(30) = New Setting("CheckOnlyBuild", CStr(SentSettings.CheckOnlyBuild))
            ManufacturingSettingsList(31) = New Setting("CheckOnlyInvent", CStr(SentSettings.CheckOnlyInvent))
            ManufacturingSettingsList(32) = New Setting("CheckOnlyRE", CStr(SentSettings.CheckOnlyRE))
            ManufacturingSettingsList(33) = New Setting("CheckIncludeTaxes", CStr(SentSettings.CheckIncludeTaxes))
            ManufacturingSettingsList(34) = New Setting("CheckIncludeUsage", CStr(SentSettings.CheckIncludeUsage))
            ManufacturingSettingsList(35) = New Setting("CheckRaceAmarr", CStr(SentSettings.CheckRaceAmarr))
            ManufacturingSettingsList(36) = New Setting("CheckRaceCaldari", CStr(SentSettings.CheckRaceCaldari))
            ManufacturingSettingsList(37) = New Setting("CheckRaceGallente", CStr(SentSettings.CheckRaceGallente))
            ManufacturingSettingsList(38) = New Setting("CheckRaceMinmatar", CStr(SentSettings.CheckRaceMinmatar))
            ManufacturingSettingsList(39) = New Setting("CheckRacePirate", CStr(SentSettings.CheckRacePirate))
            ManufacturingSettingsList(40) = New Setting("CheckRaceOther", CStr(SentSettings.CheckRaceOther))
            ManufacturingSettingsList(41) = New Setting("SortBy", CStr(SentSettings.SortBy))
            ManufacturingSettingsList(42) = New Setting("PriceCompare", CStr(SentSettings.PriceCompare))
            ManufacturingSettingsList(43) = New Setting("CheckIncludeT2Owned", CStr(SentSettings.CheckIncludeT2Owned))
            ManufacturingSettingsList(44) = New Setting("CheckIncludeT3Owned", CStr(SentSettings.CheckIncludeT3Owned))
            ManufacturingSettingsList(45) = New Setting("IgnoreLowSVRThreshold", CStr(SentSettings.IgnoreSVRThreshold))
            ManufacturingSettingsList(46) = New Setting("CheckSVRIncludeNull", CStr(SentSettings.CheckSVRIncludeNull))
            ManufacturingSettingsList(47) = New Setting("AveragePriceRegion", CStr(SentSettings.AveragePriceRegion))
            ManufacturingSettingsList(48) = New Setting("ProductionLines", CStr(SentSettings.ProductionLines))
            ManufacturingSettingsList(49) = New Setting("LaboratoryLines", CStr(SentSettings.LaboratoryLines))
            ManufacturingSettingsList(50) = New Setting("CheckDecryptor09", CStr(SentSettings.CheckDecryptor09))
            ManufacturingSettingsList(51) = New Setting("CheckDecryptor15", CStr(SentSettings.CheckDecryptor15))
            ManufacturingSettingsList(52) = New Setting("CheckDecryptor19", CStr(SentSettings.CheckDecryptor19))
            ManufacturingSettingsList(53) = New Setting("Runs", CStr(SentSettings.Runs))
            ManufacturingSettingsList(54) = New Setting("CheckBPTypeCelestials", CStr(SentSettings.CheckBPTypeCelestials))
            ManufacturingSettingsList(55) = New Setting("CheckBPTypeDeployables", CStr(SentSettings.CheckBPTypeDeployables))
            ManufacturingSettingsList(56) = New Setting("CheckSmall", CStr(SentSettings.CheckSmall))
            ManufacturingSettingsList(57) = New Setting("CheckMedium", CStr(SentSettings.CheckMedium))
            ManufacturingSettingsList(58) = New Setting("CheckLarge", CStr(SentSettings.CheckLarge))
            ManufacturingSettingsList(59) = New Setting("CheckXL", CStr(SentSettings.CheckXL))
            ManufacturingSettingsList(60) = New Setting("CheckBPTypeStationParts", CStr(SentSettings.CheckBPTypeStationParts))
            ManufacturingSettingsList(61) = New Setting("CheckIncludeBrokersFees", CStr(SentSettings.CheckIncludeBrokersFees))
            ManufacturingSettingsList(62) = New Setting("CheckDecryptorUseforT2", CStr(SentSettings.CheckDecryptorUseforT2))
            ManufacturingSettingsList(63) = New Setting("CheckDecryptorUseforT3", CStr(SentSettings.CheckDecryptorUseforT3))
            ManufacturingSettingsList(64) = New Setting("CheckCapitalComponentsFacility", CStr(SentSettings.CheckCapitalComponentsFacility))
            ManufacturingSettingsList(65) = New Setting("CheckT3DestroyerFacility", CStr(SentSettings.CheckT3DestroyerFacility))
            ManufacturingSettingsList(66) = New Setting("BPRuns", CStr(SentSettings.BPRuns))
            ManufacturingSettingsList(67) = New Setting("CheckAutoCalcNumBPs", CStr(SentSettings.CheckAutoCalcNumBPs))
            ManufacturingSettingsList(68) = New Setting("IgnoreInvention", CStr(SentSettings.IgnoreInvention))
            ManufacturingSettingsList(69) = New Setting("IgnoreMinerals", CStr(SentSettings.IgnoreMinerals))
            ManufacturingSettingsList(70) = New Setting("IgnoreT1Item", CStr(SentSettings.IgnoreT1Item))

            Call WriteSettingsToFile(ManufacturingSettingsFileName, ManufacturingSettingsList, ManufacturingSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Manufacturing Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetManufacturingSettings() As ManufacturingTabSettings
        Return ManufacturingSettings
    End Function

#End Region

#Region "Datacore Tab Settings"

    ' Loads the tab settings
    Public Function LoadDatacoreSettings() As DataCoreTabSettings
        Dim TempSettings As DataCoreTabSettings = Nothing

        Try

            ' Dim the settings
            ReDim TempSettings.SkillsLevel(NumberofDCSettingsSkillRecords)
            ReDim TempSettings.SkillsChecked(NumberofDCSettingsSkillRecords)
            ReDim TempSettings.CorpsStanding(NumberofDCSettingsCorpRecords)
            ReDim TempSettings.CorpsChecked(NumberofDCSettingsCorpRecords)

            If FileExists(DatacoreSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .PricesFrom = CStr(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeString, DatacoreSettingsFileName, "PricesFrom", DefaultReactPOSFuelCost))
                    .CheckHighSecAgents = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckHighSecAgents", DefaultReactCheckTaxes))
                    .CheckLowNullSecAgents = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckLowNullSecAgents", DefaultReactCheckFees))
                    .CheckIncludeAgentsCannotAccess = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckIncludeAgentsCannotAccess", DefaultReactItemChecks))
                    .AgentsInRegion = CStr(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeString, DatacoreSettingsFileName, "AgentsInRegion", DefaultReactItemChecks))
                    .CheckSovAmarr = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovAmarr", DefaultReactItemChecks))
                    .CheckSovAmmatar = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovAmmatar", DefaultReactItemChecks))
                    .CheckSovGallente = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovGallente", DefaultReactItemChecks))
                    .CheckSovSyndicate = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovSyndicate", DefaultReactItemChecks))
                    .CheckSovKhanid = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovKhanid", DefaultReactItemChecks))
                    .CheckSovThukker = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovThukker", DefaultReactItemChecks))
                    .CheckSovCaldari = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovCaldari", DefaultReactItemChecks))
                    .CheckSovMinmatar = CBool(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovMinmatar", DefaultReactItemChecks))

                    For i = 1 To 17
                        .SkillsChecked(i - 1) = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Skill" & CStr(i) & "Checked", DefaultSkillLevelChecked))
                        .SkillsLevel(i - 1) = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Skill" & CStr(i) & "Level ", DefaultSkillLevel))
                    Next

                    For i = 1 To 13
                        .CorpsChecked(i - 1) = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Corp" & CStr(i) & "Checked", DefaultSkillLevelChecked))
                        .CorpsStanding(i - 1) = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Corp" & CStr(i) & "Standing ", DefaultSkillLevel))
                    Next

                    .Negotiation = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Negotiation", DefaultNegotiation))
                    .Connections = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Connections", DefaultConnections))
                    .ResearchProjectMgt = CInt(GetSettingValue(DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "ResearchProjectMgt", DefaultResearchProjMgt))

                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultDatacoreSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Datacore Tab Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultDatacoreSettings()
        End Try

        ' Save them locally and then export
        DatacoreSettings = TempSettings

        Return TempSettings

    End Function

    Public Function SetDefaultDatacoreSettings() As DataCoreTabSettings
        Dim LocalSettings As DataCoreTabSettings

        ReDim LocalSettings.SkillsChecked(NumberofDCSettingsSkillRecords)
        ReDim LocalSettings.SkillsLevel(NumberofDCSettingsSkillRecords)

        ReDim LocalSettings.CorpsChecked(NumberofDCSettingsCorpRecords)
        ReDim LocalSettings.CorpsStanding(NumberofDCSettingsCorpRecords)

        With LocalSettings
            .PricesFrom = DefaultDCPricesFrom
            .CheckHighSecAgents = DefaultDCCheckHighSec
            .CheckLowNullSecAgents = DefaultDCCheckLowNullSec
            .CheckIncludeAgentsCannotAccess = DefaultDCIncludeAgentsCantUse
            .AgentsInRegion = DefaultDCAgentsInRegion
            .CheckSovAmarr = DefaultDCSovCheck
            .CheckSovAmmatar = DefaultDCSovCheck
            .CheckSovGallente = DefaultDCSovCheck
            .CheckSovSyndicate = DefaultDCSovCheck
            .CheckSovKhanid = DefaultDCSovCheck
            .CheckSovThukker = DefaultDCSovCheck
            .CheckSovCaldari = DefaultDCSovCheck
            .CheckSovMinmatar = DefaultDCSovCheck

            For i = 0 To .SkillsChecked.Count - 1
                .SkillsChecked(i) = DefaultSkillLevelChecked
                .SkillsLevel(i) = DefaultSkillLevel
            Next

            For i = 0 To .CorpsChecked.Count - 1
                .CorpsChecked(i) = DefaultCorpStandingChecked
                .CorpsStanding(i) = DefaultCorpStanding
            Next

            .Negotiation = DefaultNegotiation
            .Connections = DefaultConnections
            .ResearchProjectMgt = DefaultResearchProjMgt

        End With
        ' Save locally
        DatacoreSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveDatacoreSettings(SentSettings As DataCoreTabSettings)
        Dim DatacoreSettingsList(75) As Setting
        Dim j As Integer

        Try
            DatacoreSettingsList(0) = New Setting("PricesFrom", CStr(SentSettings.PricesFrom))
            DatacoreSettingsList(1) = New Setting("CheckHighSecAgents", CStr(SentSettings.CheckHighSecAgents))
            DatacoreSettingsList(2) = New Setting("CheckLowNullSecAgents", CStr(SentSettings.CheckLowNullSecAgents))
            DatacoreSettingsList(3) = New Setting("CheckIncludeAgentsCannotAccess", CStr(SentSettings.CheckIncludeAgentsCannotAccess))
            DatacoreSettingsList(4) = New Setting("AgentsInRegion", CStr(SentSettings.AgentsInRegion))
            DatacoreSettingsList(5) = New Setting("CheckSovAmarr", CStr(SentSettings.CheckSovAmarr))
            DatacoreSettingsList(6) = New Setting("CheckSovAmmatar", CStr(SentSettings.CheckSovAmmatar))
            DatacoreSettingsList(7) = New Setting("CheckSovGallente", CStr(SentSettings.CheckSovGallente))
            DatacoreSettingsList(8) = New Setting("CheckSovSyndicate", CStr(SentSettings.CheckSovSyndicate))
            DatacoreSettingsList(9) = New Setting("CheckSovKhanid", CStr(SentSettings.CheckSovKhanid))
            DatacoreSettingsList(10) = New Setting("CheckSovThukker", CStr(SentSettings.CheckSovThukker))
            DatacoreSettingsList(11) = New Setting("CheckSovCaldari", CStr(SentSettings.CheckSovCaldari))
            DatacoreSettingsList(12) = New Setting("CheckSovMinmatar", CStr(SentSettings.CheckSovMinmatar))

            ' Skills
            j = 0
            For i = 13 To 29
                j += 1
                DatacoreSettingsList(i) = New Setting("Skill" & CStr(j) & "Level", CStr(SentSettings.SkillsLevel(j - 1)))
            Next

            j = 0
            For i = 30 To 46
                j += 1
                DatacoreSettingsList(i) = New Setting("Skill" & CStr(j) & "Checked", CStr(SentSettings.SkillsChecked(j - 1)))
            Next

            ' Corp Standings
            j = 0
            For i = 47 To 59
                j += 1
                DatacoreSettingsList(i) = New Setting("Corp" & CStr(j) & "Standing", CStr(SentSettings.CorpsStanding(j - 1)))
            Next

            j = 0
            For i = 60 To 72
                j += 1
                DatacoreSettingsList(i) = New Setting("Corp" & CStr(j) & "Checked", CStr(SentSettings.CorpsChecked(j - 1)))
            Next

            DatacoreSettingsList(73) = New Setting("Negotiation", CStr(SentSettings.Negotiation))
            DatacoreSettingsList(74) = New Setting("Connections", CStr(SentSettings.Connections))
            DatacoreSettingsList(75) = New Setting("ResearchProjectMgt", CStr(SentSettings.ResearchProjectMgt))

            Call WriteSettingsToFile(DatacoreSettingsFileName, DatacoreSettingsList, DatacoreSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Datacore Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetDatacoreSettings() As DataCoreTabSettings
        Return DatacoreSettings
    End Function

#End Region

#Region "Reactions Tab Settings"

    ' Loads the tab settings
    Public Function LoadReactionSettings() As ReactionsTabSettings
        Dim TempSettings As ReactionsTabSettings = Nothing

        Try
            If FileExists(ReactionSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .POSFuelCost = CDbl(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeDouble, ReactionSettingsFileName, "POSFuelCost", DefaultReactPOSFuelCost))
                    .CheckTaxes = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckTaxes", DefaultReactCheckTaxes))
                    .CheckFees = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckFees", DefaultReactCheckFees))
                    .CheckAdvMoonMats = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckAdvMoonMats", DefaultReactItemChecks))
                    .CheckProcessedMoonMats = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckProcessedMoonMats", DefaultReactItemChecks))
                    .CheckHybrid = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckHybrid", DefaultReactItemChecks))
                    .CheckComplexBio = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckComplexBio", DefaultReactItemChecks))
                    .CheckSimpleBio = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckSimpleBio", DefaultReactItemChecks))
                    .CheckBuildBasic = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckBuildBasic", DefaultReactItemChecks))
                    .CheckIgnoreMarket = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckIgnoreMarket", DefaultReactItemChecks))
                    .CheckRefine = CBool(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeBoolean, ReactionSettingsFileName, "CheckRefine", DefaultReactItemChecks))
                    .NumberofPOS = CInt(GetSettingValue(ReactionSettingsFileName, SettingTypes.TypeInteger, ReactionSettingsFileName, "NumberofPOS", DefaultReactNumPOS))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultReactionSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Reaction Tab Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultReactionSettings()
        End Try

        ' Save them locally and then export
        ReactionSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultReactionSettings() As ReactionsTabSettings
        Dim LocalSettings As ReactionsTabSettings

        LocalSettings.POSFuelCost = DefaultReactPOSFuelCost
        LocalSettings.CheckTaxes = DefaultReactCheckTaxes
        LocalSettings.CheckFees = DefaultReactCheckFees
        LocalSettings.CheckAdvMoonMats = DefaultReactItemChecks
        LocalSettings.CheckProcessedMoonMats = DefaultReactItemChecks
        LocalSettings.CheckHybrid = DefaultReactItemChecks
        LocalSettings.CheckComplexBio = DefaultReactItemChecks
        LocalSettings.CheckSimpleBio = DefaultReactItemChecks
        LocalSettings.CheckBuildBasic = DefaultReactItemChecks
        LocalSettings.CheckIgnoreMarket = DefaultReactItemChecks
        LocalSettings.CheckRefine = DefaultReactItemChecks
        LocalSettings.NumberofPOS = DefaultReactNumPOS

        ' Save locally
        ReactionSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveReactionSettings(SentSettings As ReactionsTabSettings)
        Dim ReactionSettingsList(11) As Setting

        Try
            ReactionSettingsList(0) = New Setting("POSFuelCost", CStr(SentSettings.POSFuelCost))
            ReactionSettingsList(1) = New Setting("CheckTaxes", CStr(SentSettings.CheckTaxes))
            ReactionSettingsList(2) = New Setting("CheckFees", CStr(SentSettings.CheckFees))
            ReactionSettingsList(3) = New Setting("CheckAdvMoonMats", CStr(SentSettings.CheckAdvMoonMats))
            ReactionSettingsList(4) = New Setting("CheckProcessedMoonMats", CStr(SentSettings.CheckProcessedMoonMats))
            ReactionSettingsList(5) = New Setting("CheckHybrid", CStr(SentSettings.CheckHybrid))
            ReactionSettingsList(6) = New Setting("CheckComplexBio", CStr(SentSettings.CheckComplexBio))
            ReactionSettingsList(7) = New Setting("CheckSimpleBio", CStr(SentSettings.CheckSimpleBio))
            ReactionSettingsList(8) = New Setting("CheckBuildBasic", CStr(SentSettings.CheckBuildBasic))
            ReactionSettingsList(9) = New Setting("CheckIgnoreMarket", CStr(SentSettings.CheckIgnoreMarket))
            ReactionSettingsList(10) = New Setting("CheckRefine", CStr(SentSettings.CheckRefine))
            ReactionSettingsList(11) = New Setting("NumberofPOS", CStr(SentSettings.NumberofPOS))

            Call WriteSettingsToFile(ReactionSettingsFileName, ReactionSettingsList, ReactionSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Reaction Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetReactionSettings() As ReactionsTabSettings
        Return ReactionSettings
    End Function

#End Region

#Region "Mining Tab Settings"

    ' Loads the tab settings
    Public Function LoadMiningSettings() As MiningTabSettings
        Dim TempSettings As MiningTabSettings = Nothing

        Try
            If FileExists(MiningSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .OreType = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreType", DefaultMiningOreType))
                    .CheckHighYieldOres = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckHighYieldOres", DefaultMiningCheckHighYieldOres))
                    .CheckHighSecOres = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckHighSecOres", DefaultMiningCheckHighSecOres))
                    .CheckLowSecOres = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckLowSecOres", DefaultMiningCheckLowSecOres))
                    .CheckNullSecOres = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckNullSecOres", DefaultMiningCheckNullSecOres))
                    .CheckSovAmarr = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovAmarr", DefaultMiningCheckSovAmarr))
                    .CheckSovCaldari = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovCaldari", DefaultMiningCheckSovCaldari))
                    .CheckSovGallente = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovGallente", DefaultMiningCheckSovGallente))
                    .CheckSovMinmatar = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovMinmatar", DefaultMiningCheckSovMinmatar))
                    .CheckIncludeFees = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckIncludeFees", DefaultMiningCheckIncludeFees))
                    .CheckIncludeTaxes = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckIncludeTaxes", DefaultMiningCheckIncludeTaxes))
                    .CheckIncludeJumpFuelCosts = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckIncludeJumpFuelCosts", DefaultMiningCheckIncludeJumpFuelCosts))
                    .TotalJumpFuelCost = CDbl(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeDouble, MiningSettingsFileName, "TotalJumpFuelCost", DefaultMiningTotalJumpFuelCost))
                    .TotalJumpFuelM3 = CDbl(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeDouble, MiningSettingsFileName, "TotalJumpFuelM3", DefaultMiningTotalJumpFuelM3))
                    .JumpCompressedOre = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "JumpCompressedOre", DefaultMiningJumpCompressedOre))
                    .JumpMinerals = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "JumpMinerals", DefaultMiningJumpMinerals))
                    .OreMiningShip = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreMiningShip", DefaultMiningMiningShip))
                    .IceMiningShip = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceMiningShip", DefaultMiningIceMiningShip))
                    .GasMiningShip = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasMiningShip", DefaultMiningGasMiningShip))
                    .OreStrip = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreStrip", DefaultMiningOreStrip))
                    .IceStrip = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceStrip", DefaultMiningIceStrip))
                    .GasHarvester = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasHarvester", DefaultMiningGasHarvester))
                    .NumOreMiners = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumOreMiners", DefaultMiningNumOreMiners))
                    .NumIceMiners = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumIceMiners", DefaultMiningNumIceMiners))
                    .NumGasHarvesters = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumGasHarvesters", DefaultMiningNumGasHarvesters))
                    .OreUpgrade = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreUpgrade", DefaultMiningOreUpgrade))
                    ' Changed the text in the box on 4/11/2015
                    If Not MiningUpgradesCollection.Contains(.OreUpgrade) Then
                        ' Set to default
                        .OreUpgrade = DefaultMiningOreUpgrade
                    End If
                    .IceUpgrade = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceUpgrade", DefaultMiningIceUpgrade))
                    ' Changed the text in the box on 4/11/2015
                    If Not MiningUpgradesCollection.Contains(.IceUpgrade) Then
                        ' Set to default
                        .IceUpgrade = DefaultMiningOreUpgrade
                    End If
                    .GasUpgrade = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasUpgrade", DefaultMiningGasUpgrade))
                    .NumOreUpgrades = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumOreUpgrades", DefaultMiningNumOreUpgrades))
                    .NumIceUpgrades = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumIceUpgrades", DefaultMiningNumIceUpgrades))
                    .NumGasUpgrades = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumGasUpgrades", DefaultMiningNumGasUpgrades))
                    .MichiiImplant = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "MichiiImplant", DefaultMiningMichiiImplant))
                    .T2Crystals = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "T2Crystals", DefaultMiningT2Crystals))
                    .OreImplant = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreImplant", DefaultMiningOreImplant))
                    .IceImplant = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceImplant", DefaultMiningIceImplant))
                    .GasImplant = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasImplant", DefaultMiningGasImplant))
                    .CheckUseHauler = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckUseHauler", DefaultMiningCheckUseHauler))
                    .RoundTripMin = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "RoundTripMin", DefaultMiningRoundTripMin))
                    .RoundTripSec = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "RoundTripSec", DefaultMiningRoundTripSec))
                    .Haulerm3 = CDbl(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "Haulerm3", DefaultMiningHaulerm3))
                    .CheckUseFleetBooster = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckUseFleetBooster", DefaultMiningCheckUseFleetBooster))
                    .BoosterShip = CStr(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterShip", DefaultMiningBoosterShip))
                    .BoosterShipSkill = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterShipSkill", DefaultMiningBoosterShipSkill))
                    .MiningFormanSkill = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "MiningFormanSkill", DefaultMiningMiningFormanSkill))
                    .MiningDirectorSkill = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "MiningDirectorSkill", DefaultMiningMiningDirectorSkill))
                    .WarfareLinkSpecSkill = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "WarfareLinkSpecSkill", DefaultMiningWarfareLinkSpecSkill))
                    .CheckMineForemanLaserOpBoost = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "CheckMineForemanLaserOpBoost", DefaultMiningCheckMineForemanLaserOpBoost))
                    .CheckMineForemanLaserRangeBoost = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "CheckMineForemanLaserRangeBoost", DefaultMiningCheckMineForemanLaserOpBoost))
                    .CheckMiningForemanMindLink = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckMiningForemanMindLink", DefaultMiningCheckMiningForemanMindLink))
                    .CheckRorqDeployed = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckRorqDeployed", DefaultMiningRorqDeployed))
                    .MiningDroneM3perHour = CDbl(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeDouble, MiningSettingsFileName, "MiningDroneM3perHour", DefaultMiningDroneM3perHour))
                    .RefinedOre = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "RefinedOre", DefaultMiningRefinedOre))
                    .UnrefinedOre = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "UnrefinedOre", DefaultMiningUnrefinedOre))
                    .CompressedOre = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CompressedOre", DefaultMiningCompressedOre))
                    .IndustrialReconfig = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "IndustrialReconfig", DefaultMiningIndustrialReconfig))
                    .MercoxitMiningRig = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "MercoxitMiningRig", DefaultMiningRig))
                    .IceMiningRig = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "IceMiningRig", DefaultMiningRig))
                    .CheckSovWormhole = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovWormhole", DefaultMiningCheckSovWormhole))
                    .CheckSovC1 = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC1", DefaultMiningCheckSovC1))
                    .CheckSovC2 = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC2", DefaultMiningCheckSovC2))
                    .CheckSovC3 = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC3", DefaultMiningCheckSovC3))
                    .CheckSovC4 = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC4", DefaultMiningCheckSovC4))
                    .CheckSovC5 = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC5", DefaultMiningCheckSovC5))
                    .CheckSovC6 = CBool(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC6", DefaultMiningCheckSovC6))
                    .NumberofMiners = CInt(GetSettingValue(MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumberofMiners", DefaultMiningNumberofMiners))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultMiningSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Mining Tab Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultMiningSettings()
        End Try

        ' Save them locally and then export
        MiningSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultMiningSettings() As MiningTabSettings
        Dim LocalSettings As MiningTabSettings

        With LocalSettings
            .OreType = DefaultMiningOreType
            .CheckHighYieldOres = DefaultMiningCheckHighYieldOres
            .CheckHighSecOres = DefaultMiningCheckHighSecOres
            .CheckLowSecOres = DefaultMiningCheckLowSecOres
            .CheckNullSecOres = DefaultMiningCheckNullSecOres
            .CheckSovAmarr = DefaultMiningCheckSovAmarr
            .CheckSovCaldari = DefaultMiningCheckSovCaldari
            .CheckSovGallente = DefaultMiningCheckSovGallente
            .CheckSovMinmatar = DefaultMiningCheckSovMinmatar
            .CheckSovWormhole = DefaultMiningCheckSovWormhole
            .CheckSovC1 = DefaultMiningCheckSovC1
            .CheckSovC2 = DefaultMiningCheckSovC2
            .CheckSovC3 = DefaultMiningCheckSovC3
            .CheckSovC4 = DefaultMiningCheckSovC4
            .CheckSovC5 = DefaultMiningCheckSovC5
            .CheckSovC6 = DefaultMiningCheckSovC6
            .CheckIncludeFees = DefaultMiningCheckIncludeFees
            .CheckIncludeTaxes = DefaultMiningCheckIncludeTaxes
            .CheckIncludeJumpFuelCosts = DefaultMiningCheckIncludeJumpFuelCosts
            .TotalJumpFuelCost = DefaultMiningTotalJumpFuelCost
            .TotalJumpFuelM3 = DefaultMiningTotalJumpFuelM3
            .JumpCompressedOre = DefaultMiningJumpCompressedOre
            .JumpMinerals = DefaultMiningJumpMinerals
            .OreMiningShip = DefaultMiningMiningShip
            .IceMiningShip = DefaultMiningIceMiningShip
            .GasMiningShip = DefaultMiningGasMiningShip
            .OreStrip = DefaultMiningOreStrip
            .IceStrip = DefaultMiningIceStrip
            .GasHarvester = DefaultMiningGasHarvester
            .NumOreMiners = DefaultMiningNumOreMiners
            .NumIceMiners = DefaultMiningNumIceMiners
            .NumGasHarvesters = DefaultMiningNumGasHarvesters
            .OreUpgrade = DefaultMiningOreUpgrade
            .IceUpgrade = DefaultMiningIceUpgrade
            .GasUpgrade = DefaultMiningGasUpgrade
            .NumOreUpgrades = DefaultMiningNumOreUpgrades
            .NumIceUpgrades = DefaultMiningNumIceUpgrades
            .NumGasUpgrades = DefaultMiningNumGasUpgrades
            .MichiiImplant = DefaultMiningMichiiImplant
            .T2Crystals = DefaultMiningT2Crystals
            .OreImplant = DefaultMiningOreImplant
            .IceImplant = DefaultMiningIceImplant
            .GasImplant = DefaultMiningGasImplant
            .CheckUseHauler = DefaultMiningCheckUseHauler
            .RoundTripMin = DefaultMiningRoundTripMin
            .RoundTripSec = DefaultMiningRoundTripSec
            .Haulerm3 = DefaultMiningHaulerm3
            .CheckUseFleetBooster = DefaultMiningCheckUseFleetBooster
            .BoosterShip = DefaultMiningBoosterShip
            .BoosterShipSkill = DefaultMiningBoosterShipSkill
            .MiningFormanSkill = DefaultMiningMiningFormanSkill
            .MiningDirectorSkill = DefaultMiningMiningDirectorSkill
            .WarfareLinkSpecSkill = DefaultMiningWarfareLinkSpecSkill
            .CheckMineForemanLaserOpBoost = DefaultMiningCheckMineForemanLaserOpBoost
            .CheckMineForemanLaserRangeBoost = DefaultMiningCheckMineForemanLaserOpBoost
            .CheckMiningForemanMindLink = DefaultMiningCheckMiningForemanMindLink
            .CheckRorqDeployed = DefaultMiningRorqDeployed
            .MiningDroneM3perHour = DefaultMiningDroneM3perHour
            .RefinedOre = DefaultMiningRefinedOre
            .UnrefinedOre = DefaultMiningUnrefinedOre
            .CompressedOre = DefaultMiningCompressedOre
            .IndustrialReconfig = DefaultMiningIndustrialReconfig
            .MercoxitMiningRig = DefaultMiningRig
            .IceMiningRig = DefaultMiningRig
            .NumberofMiners = DefaultNumMiners
        End With

        ' Save locally
        MiningSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveMiningSettings(SentSettings As MiningTabSettings)
        Dim MiningSettingsList(64) As Setting

        Try
            MiningSettingsList(0) = New Setting("OreType", CStr(SentSettings.OreType))
            MiningSettingsList(1) = New Setting("CheckHighYieldOres", CStr(SentSettings.CheckHighYieldOres))
            MiningSettingsList(2) = New Setting("CheckHighSecOres", CStr(SentSettings.CheckHighSecOres))
            MiningSettingsList(3) = New Setting("CheckLowSecOres", CStr(SentSettings.CheckLowSecOres))
            MiningSettingsList(4) = New Setting("CheckNullSecOres", CStr(SentSettings.CheckNullSecOres))
            MiningSettingsList(5) = New Setting("CheckSovAmarr", CStr(SentSettings.CheckSovAmarr))
            MiningSettingsList(6) = New Setting("CheckSovCaldari", CStr(SentSettings.CheckSovCaldari))
            MiningSettingsList(7) = New Setting("CheckSovGallente", CStr(SentSettings.CheckSovGallente))
            MiningSettingsList(8) = New Setting("CheckSovMinmatar", CStr(SentSettings.CheckSovMinmatar))
            MiningSettingsList(9) = New Setting("CheckIncludeFees", CStr(SentSettings.CheckIncludeFees))
            MiningSettingsList(10) = New Setting("CheckIncludeTaxes", CStr(SentSettings.CheckIncludeTaxes))
            MiningSettingsList(11) = New Setting("CheckIncludeJumpFuelCosts", CStr(SentSettings.CheckIncludeJumpFuelCosts))
            MiningSettingsList(12) = New Setting("TotalJumpFuelCost", CStr(SentSettings.TotalJumpFuelCost))
            MiningSettingsList(13) = New Setting("TotalJumpFuelM3", CStr(SentSettings.TotalJumpFuelM3))
            MiningSettingsList(14) = New Setting("JumpCompressedOre", CStr(SentSettings.JumpCompressedOre))
            MiningSettingsList(15) = New Setting("JumpMinerals", CStr(SentSettings.JumpMinerals))
            MiningSettingsList(16) = New Setting("OreMiningShip", CStr(SentSettings.OreMiningShip))
            MiningSettingsList(17) = New Setting("IceMiningShip", CStr(SentSettings.IceMiningShip))
            MiningSettingsList(18) = New Setting("OreStrip", CStr(SentSettings.OreStrip))
            MiningSettingsList(19) = New Setting("IceStrip", CStr(SentSettings.IceStrip))
            MiningSettingsList(20) = New Setting("NumOreMiners", CStr(SentSettings.NumOreMiners))
            MiningSettingsList(21) = New Setting("NumIceMiners", CStr(SentSettings.NumIceMiners))
            MiningSettingsList(22) = New Setting("OreUpgrade", CStr(SentSettings.OreUpgrade))
            MiningSettingsList(23) = New Setting("IceUpgrade", CStr(SentSettings.IceUpgrade))
            MiningSettingsList(24) = New Setting("NumOreUpgrades", CStr(SentSettings.NumOreUpgrades))
            MiningSettingsList(25) = New Setting("NumIceUpgrades", CStr(SentSettings.NumIceUpgrades))
            MiningSettingsList(26) = New Setting("MichiiImplant", CStr(SentSettings.MichiiImplant))
            MiningSettingsList(27) = New Setting("T2Crystals", CStr(SentSettings.T2Crystals))
            MiningSettingsList(28) = New Setting("OreImplant", CStr(SentSettings.OreImplant))
            MiningSettingsList(29) = New Setting("IceImplant", CStr(SentSettings.IceImplant))
            MiningSettingsList(30) = New Setting("CheckUseHauler", CStr(SentSettings.CheckUseHauler))
            MiningSettingsList(31) = New Setting("RoundTripMin", CStr(SentSettings.RoundTripMin))
            MiningSettingsList(32) = New Setting("RoundTripSec", CStr(SentSettings.RoundTripSec))
            MiningSettingsList(33) = New Setting("Haulerm3", CStr(SentSettings.Haulerm3))
            MiningSettingsList(34) = New Setting("CheckUseFleetBooster", CStr(SentSettings.CheckUseFleetBooster))
            MiningSettingsList(35) = New Setting("BoosterShip", CStr(SentSettings.BoosterShip))
            MiningSettingsList(36) = New Setting("BoosterShipSkill", CStr(SentSettings.BoosterShipSkill))
            MiningSettingsList(37) = New Setting("MiningFormanSkill", CStr(SentSettings.MiningFormanSkill))
            MiningSettingsList(38) = New Setting("MiningDirectorSkill", CStr(SentSettings.MiningDirectorSkill))
            MiningSettingsList(39) = New Setting("WarfareLinkSpecSkill", CStr(SentSettings.WarfareLinkSpecSkill))
            MiningSettingsList(40) = New Setting("CheckMineForemanLaserOpBoost", CStr(SentSettings.CheckMineForemanLaserOpBoost))
            MiningSettingsList(41) = New Setting("CheckMiningForemanMindLink", CStr(SentSettings.CheckMiningForemanMindLink))
            MiningSettingsList(42) = New Setting("CheckRorqDeployed", CStr(SentSettings.CheckRorqDeployed))
            MiningSettingsList(43) = New Setting("MiningDroneM3perHour", CStr(SentSettings.MiningDroneM3perHour))
            MiningSettingsList(44) = New Setting("RefinedOre", CStr(SentSettings.RefinedOre))
            MiningSettingsList(45) = New Setting("IndustrialReconfig", CStr(SentSettings.IndustrialReconfig))
            MiningSettingsList(46) = New Setting("MercoxitMiningRig", CStr(SentSettings.MercoxitMiningRig))
            MiningSettingsList(47) = New Setting("IceMiningRig", CStr(SentSettings.IceMiningRig))
            MiningSettingsList(48) = New Setting("CheckMineForemanLaserRangeBoost", CStr(SentSettings.CheckMineForemanLaserRangeBoost))
            MiningSettingsList(49) = New Setting("GasMiningShip", CStr(SentSettings.GasMiningShip))
            MiningSettingsList(50) = New Setting("GasHarvester", CStr(SentSettings.GasHarvester))
            MiningSettingsList(51) = New Setting("NumGasHarvesters", CStr(SentSettings.NumGasHarvesters))
            MiningSettingsList(52) = New Setting("GasUpgrade", CStr(SentSettings.GasUpgrade))
            MiningSettingsList(53) = New Setting("NumGasUpgrades", CStr(SentSettings.NumGasUpgrades))
            MiningSettingsList(54) = New Setting("GasImplant", CStr(SentSettings.GasImplant))
            MiningSettingsList(55) = New Setting("CheckSovWormhole", CStr(SentSettings.CheckSovWormhole))
            MiningSettingsList(56) = New Setting("CheckSovC1", CStr(SentSettings.CheckSovC1))
            MiningSettingsList(57) = New Setting("CheckSovC2", CStr(SentSettings.CheckSovC2))
            MiningSettingsList(58) = New Setting("CheckSovC3", CStr(SentSettings.CheckSovC3))
            MiningSettingsList(59) = New Setting("CheckSovC4", CStr(SentSettings.CheckSovC4))
            MiningSettingsList(60) = New Setting("CheckSovC5", CStr(SentSettings.CheckSovC5))
            MiningSettingsList(61) = New Setting("CheckSovC6", CStr(SentSettings.CheckSovC6))
            MiningSettingsList(62) = New Setting("CompressedOre", CStr(SentSettings.CompressedOre))
            MiningSettingsList(63) = New Setting("UnrefinedOre", CStr(SentSettings.UnrefinedOre))
            MiningSettingsList(64) = New Setting("NumberofMiners", CStr(SentSettings.NumberofMiners))

            Call WriteSettingsToFile(MiningSettingsFileName, MiningSettingsList, MiningSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Mining Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetMiningSettings() As MiningTabSettings
        Return MiningSettings
    End Function

#End Region

#Region "Industry Jobs Column Settings"

    ' Loads the tab settings
    Public Function LoadIndustryJobsColumnSettings() As IndustryJobsColumnSettings
        Dim TempSettings As IndustryJobsColumnSettings = Nothing

        Try
            If FileExists(IndustryJobsColumnSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .JobState = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobState", DefaultJobState))
                    .InstallerName = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallerName", DefaultInstallerName))
                    .TimeToComplete = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "TimeToComplete", DefaultTimeToComplete))
                    .Activity = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Activity", DefaultActivity))
                    .Status = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Status", DefaultStatus))
                    .StartTime = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "StartTime", DefaultStartTime))
                    .EndTime = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "EndTime", DefaultEndTime))
                    .CompletionTime = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "CompletionTime", DefaultCompletionTime))
                    .Blueprint = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Blueprint", DefaultBlueprint))
                    .OutputItem = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItem", DefaultOutputItem))
                    .OutputItemType = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItemType", DefaultOutputItemType))
                    .InstallSystem = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallSystem", DefaultInstallSolarSystem))
                    .InstallRegion = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallRegion", DefaultInstallRegion))
                    .LicensedRuns = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "LicensedRuns", DefaultLicensedRuns))
                    .Runs = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Runs", DefaultRuns))
                    .SuccessfulRuns = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "SuccessfulRuns", DefaultSuccessfulRuns))
                    .BlueprintLocation = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "BlueprintLocation", DefaultBlueprintLocation))
                    .OutputLocation = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputLocation", DefaultOutputLocation))
                    .JobType = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobType", DefaultJobType))

                    .JobStateWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobStateWidth", DefaultIndustryColumnWidth))
                    .InstallerNameWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallerNameWidth", DefaultIndustryColumnWidth))
                    .TimeToCompleteWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "TimeToCompleteWidth", DefaultIndustryColumnWidth))
                    .ActivityWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "ActivityWidth", DefaultIndustryColumnWidth))
                    .StatusWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "StatusWidth", DefaultIndustryColumnWidth))
                    .StartTimeWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "StartTimewidth", DefaultIndustryColumnWidth))
                    .EndTimeWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "EndTimeWidth", DefaultIndustryColumnWidth))
                    .CompletionTimeWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "CompletionTimeWidth", DefaultIndustryColumnWidth))
                    .BlueprintWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "BlueprintWidth", DefaultIndustryColumnWidth))
                    .OutputItemWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItemWidth", DefaultIndustryColumnWidth))
                    .OutputItemTypeWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItemTypeWidth", DefaultIndustryColumnWidth))
                    .InstallSystemWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallSystemWidth", DefaultIndustryColumnWidth))
                    .InstallRegionWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallRegionWidth", DefaultIndustryColumnWidth))
                    .LicensedRunsWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "LiscencedRunsWidth", DefaultIndustryColumnWidth))
                    .RunsWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "RunsWidth", DefaultIndustryColumnWidth))
                    .SuccessfulRunsWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "SuccessfulRunsWidth", DefaultIndustryColumnWidth))
                    .BlueprintLocationWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "BlueprintLocationWidth", DefaultIndustryColumnWidth))
                    .OutputLocationWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputLocationWidth", DefaultIndustryColumnWidth))
                    .JobTypeWidth = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobTypeWidth", DefaultIndustryColumnWidth))

                    .OrderByColumn = CInt(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OrderByColumn", DefaultOrderByColumn))
                    .ViewJobType = CStr(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "ViewJobType", DefaultViewJobType))
                    .OrderType = CStr(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "OrderType", DefaultOrderType))
                    .JobTimes = CStr(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "JobTimes", DefaultJobTimes))
                    .SelectedCharacterIDs = CStr(GetSettingValue(IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "SelectedCharacterIDs", DefaultSelectedCharacterIDs))

                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultIndustryJobsColumnSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Industry Jobs Column Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultIndustryJobsColumnSettings()
        End Try

        ' Save them locally and then export
        IndustryJobsColumnSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultIndustryJobsColumnSettings() As IndustryJobsColumnSettings
        Dim LocalSettings As IndustryJobsColumnSettings

        With LocalSettings
            .JobState = DefaultJobState
            .InstallerName = DefaultInstallerName
            .TimeToComplete = DefaultTimeToComplete
            .Activity = DefaultActivity
            .Status = DefaultStatus
            .StartTime = DefaultStartTime
            .EndTime = DefaultEndTime
            .CompletionTime = DefaultCompletionTime
            .Blueprint = DefaultBlueprint
            .OutputItem = DefaultOutputItem
            .OutputItemType = DefaultOutputItemType
            .InstallSystem = DefaultInstallSolarSystem
            .InstallRegion = DefaultInstallRegion
            .LicensedRuns = DefaultLicensedRuns
            .Runs = DefaultRuns
            .BlueprintLocation = DefaultBlueprintLocation
            .SuccessfulRuns = DefaultSuccessfulRuns
            .OutputLocation = DefaultOutputLocation
            .JobType = DefaultJobType

            .JobStateWidth = DefaultIndustryColumnWidth
            .InstallerNameWidth = DefaultIndustryColumnWidth
            .TimeToCompleteWidth = DefaultIndustryColumnWidth
            .ActivityWidth = DefaultIndustryColumnWidth
            .StatusWidth = DefaultIndustryColumnWidth
            .StartTimeWidth = DefaultIndustryColumnWidth
            .EndTimeWidth = DefaultIndustryColumnWidth
            .CompletionTimeWidth = DefaultIndustryColumnWidth
            .BlueprintWidth = DefaultIndustryColumnWidth
            .OutputItemWidth = DefaultIndustryColumnWidth
            .OutputItemTypeWidth = DefaultIndustryColumnWidth
            .InstallSystemWidth = DefaultIndustryColumnWidth
            .InstallRegionWidth = DefaultIndustryColumnWidth
            .LicensedRunsWidth = DefaultIndustryColumnWidth
            .RunsWidth = DefaultIndustryColumnWidth
            .SuccessfulRunsWidth = DefaultIndustryColumnWidth
            .BlueprintLocationWidth = DefaultIndustryColumnWidth
            .OutputLocationWidth = DefaultIndustryColumnWidth
            .JobTypeWidth = DefaultIndustryColumnWidth

            .OrderByColumn = DefaultOrderByColumn
            .OrderType = DefaultOrderType
            .ViewJobType = DefaultViewJobType
            .JobTimes = DefaultJobTimes

            .SelectedCharacterIDs = DefaultSelectedCharacterIDs

        End With

        ' Save locally
        IndustryJobsColumnSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIndustryJobsColumnSettings(SentSettings As IndustryJobsColumnSettings)
        Dim IndustryJobsColumnSettingsList(42) As Setting

        Try
            IndustryJobsColumnSettingsList(0) = New Setting("JobState", CStr(SentSettings.JobState))
            IndustryJobsColumnSettingsList(1) = New Setting("TimeToComplete", CStr(SentSettings.TimeToComplete))
            IndustryJobsColumnSettingsList(2) = New Setting("Activity", CStr(SentSettings.Activity))
            IndustryJobsColumnSettingsList(3) = New Setting("Status", CStr(SentSettings.Status))
            IndustryJobsColumnSettingsList(4) = New Setting("StartTime", CStr(SentSettings.StartTime))
            IndustryJobsColumnSettingsList(5) = New Setting("EndTime", CStr(SentSettings.EndTime))
            IndustryJobsColumnSettingsList(6) = New Setting("CompletionTime", CStr(SentSettings.CompletionTime))
            IndustryJobsColumnSettingsList(7) = New Setting("Blueprint", CStr(SentSettings.Blueprint))
            IndustryJobsColumnSettingsList(8) = New Setting("OutputItem", CStr(SentSettings.OutputItem))
            IndustryJobsColumnSettingsList(9) = New Setting("OutputItemType", CStr(SentSettings.OutputItemType))
            IndustryJobsColumnSettingsList(10) = New Setting("InstallSystem", CStr(SentSettings.InstallSystem))
            IndustryJobsColumnSettingsList(11) = New Setting("InstallRegion", CStr(SentSettings.InstallRegion))
            IndustryJobsColumnSettingsList(12) = New Setting("LicensedRuns", CStr(SentSettings.LicensedRuns))
            IndustryJobsColumnSettingsList(13) = New Setting("Runs", CStr(SentSettings.Runs))
            IndustryJobsColumnSettingsList(14) = New Setting("SuccessfulRuns", CStr(SentSettings.SuccessfulRuns))
            IndustryJobsColumnSettingsList(15) = New Setting("BlueprintLocation", CStr(SentSettings.BlueprintLocation))
            IndustryJobsColumnSettingsList(16) = New Setting("OutputLocation", CStr(SentSettings.OutputLocation))

            IndustryJobsColumnSettingsList(17) = New Setting("JobStateWidth", CStr(SentSettings.JobStateWidth))
            IndustryJobsColumnSettingsList(18) = New Setting("TimeToCompleteWidth", CStr(SentSettings.TimeToCompleteWidth))
            IndustryJobsColumnSettingsList(19) = New Setting("ActivityWidth", CStr(SentSettings.ActivityWidth))
            IndustryJobsColumnSettingsList(20) = New Setting("StatusWidth", CStr(SentSettings.StatusWidth))
            IndustryJobsColumnSettingsList(21) = New Setting("StartTimeWidth", CStr(SentSettings.StartTimeWidth))
            IndustryJobsColumnSettingsList(22) = New Setting("EndTimeWidth", CStr(SentSettings.EndTimeWidth))
            IndustryJobsColumnSettingsList(23) = New Setting("CompletionTimeWidth", CStr(SentSettings.CompletionTimeWidth))
            IndustryJobsColumnSettingsList(24) = New Setting("BlueprintWidth", CStr(SentSettings.BlueprintWidth))
            IndustryJobsColumnSettingsList(25) = New Setting("OutputItemWidth", CStr(SentSettings.OutputItemWidth))
            IndustryJobsColumnSettingsList(26) = New Setting("OutputItemTypeWidth", CStr(SentSettings.OutputItemTypeWidth))
            IndustryJobsColumnSettingsList(27) = New Setting("InstallSystemWidth", CStr(SentSettings.InstallSystemWidth))
            IndustryJobsColumnSettingsList(28) = New Setting("InstallRegionWidth", CStr(SentSettings.InstallRegionWidth))
            IndustryJobsColumnSettingsList(29) = New Setting("LicensedRunsWidth", CStr(SentSettings.LicensedRunsWidth))
            IndustryJobsColumnSettingsList(30) = New Setting("RunsWidth", CStr(SentSettings.RunsWidth))
            IndustryJobsColumnSettingsList(31) = New Setting("SuccessfulRunsWidth", CStr(SentSettings.SuccessfulRunsWidth))
            IndustryJobsColumnSettingsList(32) = New Setting("BlueprintLocationWidth", CStr(SentSettings.BlueprintLocationWidth))
            IndustryJobsColumnSettingsList(33) = New Setting("OutputLocationWidth", CStr(SentSettings.OutputLocationWidth))

            IndustryJobsColumnSettingsList(34) = New Setting("OrderByColumn", CStr(SentSettings.OrderByColumn))
            IndustryJobsColumnSettingsList(35) = New Setting("ViewJobType", CStr(SentSettings.ViewJobType))
            IndustryJobsColumnSettingsList(36) = New Setting("OrderType", CStr(SentSettings.OrderType))
            IndustryJobsColumnSettingsList(37) = New Setting("JobTimes", CStr(SentSettings.JobTimes))
            IndustryJobsColumnSettingsList(38) = New Setting("SelectedCharacterIDs", CStr(SentSettings.SelectedCharacterIDs))

            IndustryJobsColumnSettingsList(39) = New Setting("InstallerName", CStr(SentSettings.InstallerName))
            IndustryJobsColumnSettingsList(40) = New Setting("InstallerNameWidth", CStr(SentSettings.InstallerNameWidth))

            IndustryJobsColumnSettingsList(41) = New Setting("JobType", CStr(SentSettings.JobType))
            IndustryJobsColumnSettingsList(42) = New Setting("JobTypeWidth", CStr(SentSettings.JobTypeWidth))

            Call WriteSettingsToFile(IndustryJobsColumnSettingsFileName, IndustryJobsColumnSettingsList, IndustryJobsColumnSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Industry Jobs Column Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIndustryJobsColumnSettings() As IndustryJobsColumnSettings
        Return IndustryJobsColumnSettings
    End Function

#End Region

#Region "Manufacturing Tab Column Settings"

    ' Loads the tab settings
    Public Function LoadManufacturingTabColumnSettings() As ManufacturingTabColumnSettings
        Dim TempSettings As ManufacturingTabColumnSettings = Nothing

        Try
            If FileExists(ManufacturingTabColumnSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .ItemCategory = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemCategory", DefaultMTItemCategory))
                    .ItemGroup = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemGroup", DefaultMTItemGroup))
                    .ItemName = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemName", DefaultMTItemName))
                    .Owned = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Owned", DefaultMTOwned))
                    .Tech = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Tech", DefaultMTTech))
                    .BPME = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPME", DefaultMTBPME))
                    .BPTE = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPTE", DefaultMTBPTE))
                    .Inputs = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Inputs", DefaultMTInputs))
                    .Compared = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Compared", DefaultMTCompared))
                    .TotalRuns = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalRuns", DefaultMTTotalRuns))
                    .SingleInventedBPCRuns = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "SingleInventedBPCRuns", DefaultMTSingleInventedBPCRuns))
                    .ProductionLines = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ProductionLines", DefaultMTProductionLines))
                    .LaboratoryLines = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "LaboratoryLines", DefaultMTLaboratoryLines))
                    .TotalInventionCost = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalInventionCost", DefaultMTTotalInventionCost))
                    .TotalCopyCost = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalCopyCost", DefaultMTTotalCopyCost))
                    .Taxes = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Taxes", DefaultMTTaxes))
                    .BrokerFees = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BrokerFees", DefaultMTBrokerFees))
                    .BPProductionTime = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPProductionTime", DefaultMTBPProductionTime))
                    .TotalProductionTime = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalProductionTime", DefaultMTTotalProductionTime))
                    .CopyTime = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyTime", DefaultMTCopyTime))
                    .InventionTime = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionTime", DefaultMTInventionTime))
                    .ItemMarketPrice = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemMarketPrice", DefaultMTItemMarketPrice))
                    .Profit = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Profit", DefaultMTProfit))
                    .ProfitPercentage = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ProfitPercentage", DefaultMTProfitPercentage))
                    .IskperHour = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "IskperHour", DefaultMTIskperHour))
                    .SVR = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "SVR", DefaultMTSVR))
                    .SVRxIPH = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "SVRxIPH", DefaultMTSVRxIPH))
                    .TotalCost = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalCost", DefaultMTTotalCost))
                    .BaseJobCost = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BaseJobCost", DefaultMTBaseJobCost))
                    .NumBPs = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "NumBPs", DefaultMTNumBPs))
                    .InventionChance = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionChance", DefaultMTInventionChance))
                    .BPType = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPType", DefaultMTBPType))
                    .Race = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "Race", DefaultMTRace))
                    .VolumeperItem = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "VolumeperItem", DefaultMTVolumeperItem))
                    .TotalVolume = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalVolume", DefaultMTTotalVolume))
                    .ManufacturingJobFee = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingJobFee", DefaultMTManufacturingJobFee))
                    .ManufacturingFacilityName = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityName", DefaultMTManufacturingFacilityName))
                    .ManufacturingFacilitySystem = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilitySystem", DefaultMTManufacturingFacilitySystem))
                    .ManufacturingFacilityRegion = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityRegion", DefaultMTManufacturingFacilityRegion))
                    .ManufacturingFacilitySystemIndex = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilitySystemIndex", DefaultMTManufacturingFacilitySystemIndex))
                    .ManufacturingFacilityTax = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityTax", DefaultMTManufacturingFacilityTax))
                    .ManufacturingFacilityMEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityMEBonus", DefaultMTManufacturingFacilityMEBonus))
                    .ManufacturingFacilityTEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityTEBonus", DefaultMTManufacturingFacilityTEBonus))
                    .ManufacturingFacilityUsage = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityUsage", DefaultMTManufacturingFacilityUsage))
                    .ComponentFacilityName = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityName", DefaultMTComponentFacilityName))
                    .ComponentFacilitySystem = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilitySystem", DefaultMTComponentFacilitySystem))
                    .ComponentFacilityRegion = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityRegion", DefaultMTComponentFacilityRegion))
                    .ComponentFacilitySystemIndex = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilitySystemIndex", DefaultMTComponentFacilitySystemIndex))
                    .ComponentFacilityTax = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityTax", DefaultMTComponentFacilityTax))
                    .ComponentFacilityMEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityMEBonus", DefaultMTComponentFacilityMEBonus))
                    .ComponentFacilityTEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityTEBonus", DefaultMTComponentFacilityTEBonus))
                    .ComponentFacilityUsage = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityUsage", DefaultMTComponentFacilityUsage))
                    .CopyingFacilityName = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityName", DefaultMTCopyingFacilityName))
                    .CopyingFacilitySystem = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilitySystem", DefaultMTCopyingFacilitySystem))
                    .CopyingFacilityRegion = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityRegion", DefaultMTCopyingFacilityRegion))
                    .CopyingFacilitySystemIndex = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilitySystemIndex", DefaultMTCopyingFacilitySystemIndex))
                    .CopyingFacilityTax = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityTax", DefaultMTCopyingFacilityTax))
                    .CopyingFacilityMEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityMEBonus", DefaultMTCopyingFacilityMEBonus))
                    .CopyingFacilityTEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityTEBonus", DefaultMTCopyingFacilityTEBonus))
                    .CopyingFacilityUsage = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityUsage", DefaultMTCopyingFacilityUsage))
                    .InventionFacilityName = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityName", DefaultMTInventionFacilityName))
                    .InventionFacilitySystem = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilitySystem", DefaultMTInventionFacilitySystem))
                    .InventionFacilityRegion = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityRegion", DefaultMTInventionFacilityRegion))
                    .InventionFacilitySystemIndex = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilitySystemIndex", DefaultMTInventionFacilitySystemIndex))
                    .InventionFacilityTax = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityTax", DefaultMTInventionFacilityTax))
                    .InventionFacilityMEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityMEBonus", DefaultMTInventionFacilityMEBonus))
                    .InventionFacilityTEBonus = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityTEBonus", DefaultMTInventionFacilityTEBonus))
                    .InventionFacilityUsage = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityUsage", DefaultMTInventionFacilityUsage))

                    .ItemCategoryWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemCategoryWidth", DefaultMTItemCategoryWidth))
                    .ItemGroupWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemGroupWidth", DefaultMTItemGroupWidth))
                    .ItemNameWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemNameWidth", DefaultMTItemNameWidth))
                    .OwnedWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "OwnedWidth", DefaultMTOwnedWidth))
                    .TechWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TechWidth", DefaultMTTechWidth))
                    .BPMEWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPMEWidth", DefaultMTBPMEWidth))
                    .BPTEWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPTEWidth", DefaultMTBPTEWidth))
                    .InputsWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InputsWidth", DefaultMTInputsWidth))
                    .ComparedWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComparedWidth", DefaultMTComparedWidth))
                    .TotalRunsWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalRunsWidth", DefaultMTTotalRunsWidth))
                    .SingleInventedBPCRunsWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "SingleInventedBPCRunsWidth", DefaultMTSingleInventedBPCRunsWidth))
                    .ProductionLinesWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ProductionLinesWidth", DefaultMTProductionLinesWidth))
                    .LaboratoryLinesWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "LaboratoryLinesWidth", DefaultMTLaboratoryLinesWidth))
                    .TotalInventionCostWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalInventionCostWidth", DefaultMTTotalInventionCostWidth))
                    .TotalCopyCostWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalCopyCostWidth", DefaultMTTotalCopyCostWidth))
                    .TaxesWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TaxesWidth", DefaultMTTaxesWidth))
                    .BrokerFeesWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BrokerFeesWidth", DefaultMTBrokerFeesWidth))
                    .BPProductionTimeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPProductionTimeWidth", DefaultMTBPProductionTimeWidth))
                    .TotalProductionTimeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalProductionTimeWidth", DefaultMTTotalProductionTimeWidth))
                    .CopyTimeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyTimeWidth", DefaultMTCopyTimeWidth))
                    .InventionTimeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionTimeWidth", DefaultMTInventionTimeWidth))
                    .ItemMarketPriceWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ItemMarketPriceWidth", DefaultMTItemMarketPriceWidth))
                    .ProfitWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ProfitWidth", DefaultMTProfitWidth))
                    .ProfitPercentageWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ProfitPercentageWidth", DefaultMTProfitPercentageWidth))
                    .IskperHourWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "IskperHourWidth", DefaultMTIskperHourWidth))
                    .SVRWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "SVRWidth", DefaultMTSVRWidth))
                    .SVRxIPHWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "SVRxIPHWidth", DefaultMTSVRxIPHWidth))
                    .TotalCostWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalCostWidth", DefaultMTTotalCostWidth))
                    .BaseJobCostWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BaseJobCostWidth", DefaultMTBaseJobCostWidth))
                    .NumBPsWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "NumBPsWidth", DefaultMTNumBPsWidth))
                    .InventionChanceWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionChanceWidth", DefaultMTInventionChanceWidth))
                    .BPTypeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "BPTypeWidth", DefaultMTBPTypeWidth))
                    .RaceWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "RaceWidth", DefaultMTRaceWidth))
                    .VolumeperItemWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "VolumeperItemWidth", DefaultMTVolumeperItemWidth))
                    .TotalVolumeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "TotalVolumeWidth", DefaultMTTotalVolumeWidth))
                    .ManufacturingJobFeeWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingJobFeeWidth", DefaultMTManufacturingJobFeeWidth))
                    .ManufacturingFacilityNameWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityNameWidth", DefaultMTManufacturingFacilityNameWidth))
                    .ManufacturingFacilitySystemWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilitySystemWidth", DefaultMTManufacturingFacilitySystemWidth))
                    .ManufacturingFacilityRegionWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityRegionWidth", DefaultMTManufacturingFacilityRegionWidth))
                    .ManufacturingFacilitySystemIndexWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilitySystemIndexWidth", DefaultMTManufacturingFacilitySystemIndexWidth))
                    .ManufacturingFacilityTaxWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityTaxWidth", DefaultMTManufacturingFacilityTaxWidth))
                    .ManufacturingFacilityMEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityMEBonusWidth", DefaultMTManufacturingFacilityMEBonusWidth))
                    .ManufacturingFacilityTEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityTEBonusWidth", DefaultMTManufacturingFacilityTEBonusWidth))
                    .ManufacturingFacilityUsageWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ManufacturingFacilityUsageWidth", DefaultMTManufacturingFacilityUsageWidth))
                    .ComponentFacilityNameWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityNameWidth", DefaultMTComponentFacilityNameWidth))
                    .ComponentFacilitySystemWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilitySystemWidth", DefaultMTComponentFacilitySystemWidth))
                    .ComponentFacilityRegionWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityRegionWidth", DefaultMTComponentFacilityRegionWidth))
                    .ComponentFacilitySystemIndexWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilitySystemIndexWidth", DefaultMTComponentFacilitySystemIndexWidth))
                    .ComponentFacilityTaxWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityTaxWidth", DefaultMTComponentFacilityTaxWidth))
                    .ComponentFacilityMEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityMEBonusWidth", DefaultMTComponentFacilityMEBonusWidth))
                    .ComponentFacilityTEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityTEBonusWidth", DefaultMTComponentFacilityTEBonusWidth))
                    .ComponentFacilityUsageWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "ComponentFacilityUsageWidth", DefaultMTComponentFacilityUsageWidth))
                    .CopyingFacilityNameWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityNameWidth", DefaultMTCopyingFacilityNameWidth))
                    .CopyingFacilitySystemWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilitySystemWidth", DefaultMTCopyingFacilitySystemWidth))
                    .CopyingFacilityRegionWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityRegionWidth", DefaultMTCopyingFacilityRegionWidth))
                    .CopyingFacilitySystemIndexWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilitySystemIndexWidth", DefaultMTCopyingFacilitySystemIndexWidth))
                    .CopyingFacilityTaxWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityTaxWidth", DefaultMTCopyingFacilityTaxWidth))
                    .CopyingFacilityMEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityMEBonusWidth", DefaultMTCopyingFacilityMEBonusWidth))
                    .CopyingFacilityTEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityTEBonusWidth", DefaultMTCopyingFacilityTEBonusWidth))
                    .CopyingFacilityUsageWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "CopyingFacilityUsageWidth", DefaultMTCopyingFacilityUsageWidth))
                    .InventionFacilityNameWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityNameWidth", DefaultMTInventionFacilityNameWidth))
                    .InventionFacilitySystemWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilitySystemWidth", DefaultMTInventionFacilitySystemWidth))
                    .InventionFacilityRegionWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityRegionWidth", DefaultMTInventionFacilityRegionWidth))
                    .InventionFacilitySystemIndexWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilitySystemIndexWidth", DefaultMTInventionFacilitySystemIndexWidth))
                    .InventionFacilityTaxWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityTaxWidth", DefaultMTInventionFacilityTaxWidth))
                    .InventionFacilityMEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityMEBonusWidth", DefaultMTInventionFacilityMEBonusWidth))
                    .InventionFacilityTEBonusWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityTEBonusWidth", DefaultMTInventionFacilityTEBonusWidth))
                    .InventionFacilityUsageWidth = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "InventionFacilityUsageWidth", DefaultMTInventionFacilityUsageWidth))

                    .OrderByColumn = CInt(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "OrderByColumn", DefaultMTOrderByColumn))
                    .OrderType = CStr(GetSettingValue(ManufacturingTabColumnSettingsFileName, SettingTypes.TypeString, ManufacturingTabColumnSettingsFileName, "OrderType", DefaultMTOrderType))

                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultManufacturingTabColumnSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Industry Jobs Column Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultManufacturingTabColumnSettings()
        End Try

        ' Save them locally and then export
        ManufacturingTabColumnSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultManufacturingTabColumnSettings() As ManufacturingTabColumnSettings
        Dim LocalSettings As ManufacturingTabColumnSettings

        With LocalSettings
            .ItemCategory = DefaultMTItemCategory
            .ItemGroup = DefaultMTItemGroup
            .ItemName = DefaultMTItemName
            .Owned = DefaultMTOwned
            .Tech = DefaultMTTech
            .BPME = DefaultMTBPME
            .BPTE = DefaultMTBPTE
            .Inputs = DefaultMTInputs
            .Compared = DefaultMTCompared
            .TotalRuns = DefaultMTTotalRuns
            .SingleInventedBPCRuns = DefaultMTSingleInventedBPCRuns
            .ProductionLines = DefaultMTProductionLines
            .LaboratoryLines = DefaultMTLaboratoryLines
            .TotalInventionCost = DefaultMTTotalInventionCost
            .TotalCopyCost = DefaultMTTotalCopyCost
            .Taxes = DefaultMTTaxes
            .BrokerFees = DefaultMTBrokerFees
            .BPProductionTime = DefaultMTBPProductionTime
            .TotalProductionTime = DefaultMTTotalProductionTime
            .CopyTime = DefaultMTCopyTime
            .InventionTime = DefaultMTInventionTime
            .ItemMarketPrice = DefaultMTItemMarketPrice
            .Profit = DefaultMTProfit
            .ProfitPercentage = DefaultMTProfitPercentage
            .IskperHour = DefaultMTIskperHour
            .SVR = DefaultMTSVR
            .SVRxIPH = DefaultMTSVRxIPH
            .TotalCost = DefaultMTTotalCost
            .BaseJobCost = DefaultMTBaseJobCost
            .NumBPs = DefaultMTNumBPs
            .InventionChance = DefaultMTInventionChance
            .BPType = DefaultMTBPType
            .Race = DefaultMTRace
            .VolumeperItem = DefaultMTVolumeperItem
            .TotalVolume = DefaultMTTotalVolume
            .ManufacturingJobFee = DefaultMTManufacturingJobFee
            .ManufacturingFacilityName = DefaultMTManufacturingFacilityName
            .ManufacturingFacilitySystem = DefaultMTManufacturingFacilitySystem
            .ManufacturingFacilityRegion = DefaultMTManufacturingFacilityRegion
            .ManufacturingFacilitySystemIndex = DefaultMTManufacturingFacilitySystemIndex
            .ManufacturingFacilityTax = DefaultMTManufacturingFacilityTax
            .ManufacturingFacilityMEBonus = DefaultMTManufacturingFacilityMEBonus
            .ManufacturingFacilityTEBonus = DefaultMTManufacturingFacilityTEBonus
            .ManufacturingFacilityUsage = DefaultMTManufacturingFacilityUsage
            .ComponentFacilityName = DefaultMTComponentFacilityName
            .ComponentFacilitySystem = DefaultMTComponentFacilitySystem
            .ComponentFacilityRegion = DefaultMTComponentFacilityRegion
            .ComponentFacilitySystemIndex = DefaultMTComponentFacilitySystemIndex
            .ComponentFacilityTax = DefaultMTComponentFacilityTax
            .ComponentFacilityMEBonus = DefaultMTComponentFacilityMEBonus
            .ComponentFacilityTEBonus = DefaultMTComponentFacilityTEBonus
            .ComponentFacilityUsage = DefaultMTComponentFacilityUsage
            .CopyingFacilityName = DefaultMTCopyingFacilityName
            .CopyingFacilitySystem = DefaultMTCopyingFacilitySystem
            .CopyingFacilityRegion = DefaultMTCopyingFacilityRegion
            .CopyingFacilitySystemIndex = DefaultMTCopyingFacilitySystemIndex
            .CopyingFacilityTax = DefaultMTCopyingFacilityTax
            .CopyingFacilityMEBonus = DefaultMTCopyingFacilityMEBonus
            .CopyingFacilityTEBonus = DefaultMTCopyingFacilityTEBonus
            .CopyingFacilityUsage = DefaultMTCopyingFacilityUsage
            .InventionFacilityName = DefaultMTInventionFacilityName
            .InventionFacilitySystem = DefaultMTInventionFacilitySystem
            .InventionFacilityRegion = DefaultMTInventionFacilityRegion
            .InventionFacilitySystemIndex = DefaultMTInventionFacilitySystemIndex
            .InventionFacilityTax = DefaultMTInventionFacilityTax
            .InventionFacilityMEBonus = DefaultMTInventionFacilityMEBonus
            .InventionFacilityTEBonus = DefaultMTInventionFacilityTEBonus
            .InventionFacilityUsage = DefaultMTInventionFacilityUsage

            .ItemCategoryWidth = DefaultMTItemCategoryWidth
            .ItemGroupWidth = DefaultMTItemGroupWidth
            .ItemNameWidth = DefaultMTItemNameWidth
            .OwnedWidth = DefaultMTOwnedWidth
            .TechWidth = DefaultMTTechWidth
            .BPMEWidth = DefaultMTBPMEWidth
            .BPTEWidth = DefaultMTBPTEWidth
            .InputsWidth = DefaultMTInputsWidth
            .ComparedWidth = DefaultMTComparedWidth
            .TotalRunsWidth = DefaultMTTotalRunsWidth
            .SingleInventedBPCRunsWidth = DefaultMTSingleInventedBPCRunsWidth
            .ProductionLinesWidth = DefaultMTProductionLinesWidth
            .LaboratoryLinesWidth = DefaultMTLaboratoryLinesWidth
            .TotalInventionCostWidth = DefaultMTTotalInventionCostWidth
            .TotalCopyCostWidth = DefaultMTTotalCopyCostWidth
            .TaxesWidth = DefaultMTTaxesWidth
            .BrokerFeesWidth = DefaultMTBrokerFeesWidth
            .BPProductionTimeWidth = DefaultMTBPProductionTimeWidth
            .TotalProductionTimeWidth = DefaultMTTotalProductionTimeWidth
            .CopyTimeWidth = DefaultMTCopyTimeWidth
            .InventionTimeWidth = DefaultMTInventionTimeWidth
            .ItemMarketPriceWidth = DefaultMTItemMarketPriceWidth
            .ProfitWidth = DefaultMTProfitWidth
            .ProfitPercentageWidth = DefaultMTProfitPercentageWidth
            .IskperHourWidth = DefaultMTIskperHourWidth
            .SVRWidth = DefaultMTSVRWidth
            .SVRxIPHWidth = DefaultMTSVRxIPHWidth
            .TotalCostWidth = DefaultMTTotalCostWidth
            .BaseJobCostWidth = DefaultMTBaseJobCostWidth
            .NumBPsWidth = DefaultMTNumBPsWidth
            .InventionChanceWidth = DefaultMTInventionChanceWidth
            .BPTypeWidth = DefaultMTBPTypeWidth
            .RaceWidth = DefaultMTRaceWidth
            .VolumeperItemWidth = DefaultMTVolumeperItemWidth
            .TotalVolumeWidth = DefaultMTTotalVolumeWidth
            .ManufacturingJobFeeWidth = DefaultMTManufacturingJobFeeWidth
            .ManufacturingFacilityNameWidth = DefaultMTManufacturingFacilityNameWidth
            .ManufacturingFacilitySystemWidth = DefaultMTManufacturingFacilitySystemWidth
            .ManufacturingFacilityRegionWidth = DefaultMTManufacturingFacilityRegionWidth
            .ManufacturingFacilitySystemIndexWidth = DefaultMTManufacturingFacilitySystemIndexWidth
            .ManufacturingFacilityTaxWidth = DefaultMTManufacturingFacilityTaxWidth
            .ManufacturingFacilityMEBonusWidth = DefaultMTManufacturingFacilityMEBonusWidth
            .ManufacturingFacilityTEBonusWidth = DefaultMTManufacturingFacilityTEBonusWidth
            .ManufacturingFacilityUsageWidth = DefaultMTManufacturingFacilityUsageWidth
            .ComponentFacilityNameWidth = DefaultMTComponentFacilityNameWidth
            .ComponentFacilitySystemWidth = DefaultMTComponentFacilitySystemWidth
            .ComponentFacilityRegionWidth = DefaultMTComponentFacilityRegionWidth
            .ComponentFacilitySystemIndexWidth = DefaultMTComponentFacilitySystemIndexWidth
            .ComponentFacilityTaxWidth = DefaultMTComponentFacilityTaxWidth
            .ComponentFacilityMEBonusWidth = DefaultMTComponentFacilityMEBonusWidth
            .ComponentFacilityTEBonusWidth = DefaultMTComponentFacilityTEBonusWidth
            .ComponentFacilityUsageWidth = DefaultMTComponentFacilityUsageWidth
            .CopyingFacilityNameWidth = DefaultMTCopyingFacilityNameWidth
            .CopyingFacilitySystemWidth = DefaultMTCopyingFacilitySystemWidth
            .CopyingFacilityRegionWidth = DefaultMTCopyingFacilityRegionWidth
            .CopyingFacilitySystemIndexWidth = DefaultMTCopyingFacilitySystemIndexWidth
            .CopyingFacilityTaxWidth = DefaultMTCopyingFacilityTaxWidth
            .CopyingFacilityMEBonusWidth = DefaultMTCopyingFacilityMEBonusWidth
            .CopyingFacilityTEBonusWidth = DefaultMTCopyingFacilityTEBonusWidth
            .CopyingFacilityUsageWidth = DefaultMTCopyingFacilityUsageWidth
            .InventionFacilityNameWidth = DefaultMTInventionFacilityNameWidth
            .InventionFacilitySystemWidth = DefaultMTInventionFacilitySystemWidth
            .InventionFacilityRegionWidth = DefaultMTInventionFacilityRegionWidth
            .InventionFacilitySystemIndexWidth = DefaultMTInventionFacilitySystemIndexWidth
            .InventionFacilityTaxWidth = DefaultMTInventionFacilityTaxWidth
            .InventionFacilityMEBonusWidth = DefaultMTInventionFacilityMEBonusWidth
            .InventionFacilityTEBonusWidth = DefaultMTInventionFacilityTEBonusWidth
            .InventionFacilityUsageWidth = DefaultMTInventionFacilityUsageWidth

            .OrderByColumn = DefaultMTOrderByColumn
            .OrderType = DefaultMTOrderType

        End With

        ' save locally
        ManufacturingTabColumnSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveManufacturingTabColumnSettings(SentSettings As ManufacturingTabColumnSettings)
        Dim ManufacturingTabColumnSettingsList(137) As Setting

        Try
            ManufacturingTabColumnSettingsList(0) = New Setting("ItemCategory", CStr(SentSettings.ItemCategory))
            ManufacturingTabColumnSettingsList(1) = New Setting("ItemGroup", CStr(SentSettings.ItemGroup))
            ManufacturingTabColumnSettingsList(2) = New Setting("ItemName", CStr(SentSettings.ItemName))
            ManufacturingTabColumnSettingsList(3) = New Setting("Owned", CStr(SentSettings.Owned))
            ManufacturingTabColumnSettingsList(4) = New Setting("Tech", CStr(SentSettings.Tech))
            ManufacturingTabColumnSettingsList(5) = New Setting("BPME", CStr(SentSettings.BPME))
            ManufacturingTabColumnSettingsList(6) = New Setting("BPTE", CStr(SentSettings.BPTE))
            ManufacturingTabColumnSettingsList(7) = New Setting("Inputs", CStr(SentSettings.Inputs))
            ManufacturingTabColumnSettingsList(8) = New Setting("Compared", CStr(SentSettings.Compared))
            ManufacturingTabColumnSettingsList(9) = New Setting("TotalRuns", CStr(SentSettings.TotalRuns))
            ManufacturingTabColumnSettingsList(10) = New Setting("SingleInventedBPCRuns", CStr(SentSettings.SingleInventedBPCRuns))
            ManufacturingTabColumnSettingsList(11) = New Setting("ProductionLines", CStr(SentSettings.ProductionLines))
            ManufacturingTabColumnSettingsList(12) = New Setting("LaboratoryLines", CStr(SentSettings.LaboratoryLines))
            ManufacturingTabColumnSettingsList(13) = New Setting("TotalInventionCost", CStr(SentSettings.TotalInventionCost))
            ManufacturingTabColumnSettingsList(14) = New Setting("TotalCopyCost", CStr(SentSettings.TotalCopyCost))
            ManufacturingTabColumnSettingsList(15) = New Setting("SVRxIPH", CStr(SentSettings.SVRxIPH))
            ManufacturingTabColumnSettingsList(16) = New Setting("Taxes", CStr(SentSettings.Taxes))
            ManufacturingTabColumnSettingsList(17) = New Setting("BrokerFees", CStr(SentSettings.BrokerFees))
            ManufacturingTabColumnSettingsList(18) = New Setting("BPProductionTime", CStr(SentSettings.BPProductionTime))
            ManufacturingTabColumnSettingsList(19) = New Setting("TotalProductionTime", CStr(SentSettings.TotalProductionTime))
            ManufacturingTabColumnSettingsList(20) = New Setting("CopyTime", CStr(SentSettings.CopyTime))
            ManufacturingTabColumnSettingsList(21) = New Setting("InventionTime", CStr(SentSettings.InventionTime))
            ManufacturingTabColumnSettingsList(22) = New Setting("ItemMarketPrice", CStr(SentSettings.ItemMarketPrice))
            ManufacturingTabColumnSettingsList(23) = New Setting("Profit", CStr(SentSettings.Profit))
            ManufacturingTabColumnSettingsList(24) = New Setting("ProfitPercentage", CStr(SentSettings.ProfitPercentage))
            ManufacturingTabColumnSettingsList(25) = New Setting("IskperHour", CStr(SentSettings.IskperHour))
            ManufacturingTabColumnSettingsList(26) = New Setting("SVR", CStr(SentSettings.SVR))
            ManufacturingTabColumnSettingsList(27) = New Setting("TotalCost", CStr(SentSettings.TotalCost))
            ManufacturingTabColumnSettingsList(28) = New Setting("BaseJobCost", CStr(SentSettings.BaseJobCost))
            ManufacturingTabColumnSettingsList(29) = New Setting("NumBPs", CStr(SentSettings.NumBPs))
            ManufacturingTabColumnSettingsList(30) = New Setting("InventionChance", CStr(SentSettings.InventionChance))
            ManufacturingTabColumnSettingsList(31) = New Setting("BPType", CStr(SentSettings.BPType))
            ManufacturingTabColumnSettingsList(32) = New Setting("Race", CStr(SentSettings.Race))
            ManufacturingTabColumnSettingsList(33) = New Setting("VolumeperItem", CStr(SentSettings.VolumeperItem))
            ManufacturingTabColumnSettingsList(34) = New Setting("TotalVolume", CStr(SentSettings.TotalVolume))
            ManufacturingTabColumnSettingsList(35) = New Setting("ManufacturingJobFee", CStr(SentSettings.ManufacturingJobFee))
            ManufacturingTabColumnSettingsList(36) = New Setting("ManufacturingFacilityName", CStr(SentSettings.ManufacturingFacilityName))
            ManufacturingTabColumnSettingsList(37) = New Setting("ManufacturingFacilitySystem", CStr(SentSettings.ManufacturingFacilitySystem))
            ManufacturingTabColumnSettingsList(38) = New Setting("ManufacturingFacilityRegion", CStr(SentSettings.ManufacturingFacilityRegion))
            ManufacturingTabColumnSettingsList(39) = New Setting("ManufacturingFacilitySystemIndex", CStr(SentSettings.ManufacturingFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(40) = New Setting("ManufacturingFacilityTax", CStr(SentSettings.ManufacturingFacilityTax))
            ManufacturingTabColumnSettingsList(41) = New Setting("ManufacturingFacilityMEBonus", CStr(SentSettings.ManufacturingFacilityMEBonus))
            ManufacturingTabColumnSettingsList(42) = New Setting("ManufacturingFacilityTEBonus", CStr(SentSettings.ManufacturingFacilityTEBonus))
            ManufacturingTabColumnSettingsList(43) = New Setting("ManufacturingFacilityUsage", CStr(SentSettings.ManufacturingFacilityUsage))
            ManufacturingTabColumnSettingsList(44) = New Setting("ComponentFacilityName", CStr(SentSettings.ComponentFacilityName))
            ManufacturingTabColumnSettingsList(45) = New Setting("ComponentFacilitySystem", CStr(SentSettings.ComponentFacilitySystem))
            ManufacturingTabColumnSettingsList(48) = New Setting("ComponentFacilityRegion", CStr(SentSettings.ComponentFacilityRegion))
            ManufacturingTabColumnSettingsList(46) = New Setting("ComponentFacilitySystemIndex", CStr(SentSettings.ComponentFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(47) = New Setting("ComponentFacilityTax", CStr(SentSettings.ComponentFacilityTax))
            ManufacturingTabColumnSettingsList(49) = New Setting("ComponentFacilityMEBonus", CStr(SentSettings.ComponentFacilityMEBonus))
            ManufacturingTabColumnSettingsList(50) = New Setting("ComponentFacilityTEBonus", CStr(SentSettings.ComponentFacilityTEBonus))
            ManufacturingTabColumnSettingsList(51) = New Setting("ComponentFacilityUsage", CStr(SentSettings.ComponentFacilityUsage))
            ManufacturingTabColumnSettingsList(52) = New Setting("CopyingFacilityName", CStr(SentSettings.CopyingFacilityName))
            ManufacturingTabColumnSettingsList(53) = New Setting("CopyingFacilitySystem", CStr(SentSettings.CopyingFacilitySystem))
            ManufacturingTabColumnSettingsList(54) = New Setting("CopyingFacilityRegion", CStr(SentSettings.CopyingFacilityRegion))
            ManufacturingTabColumnSettingsList(55) = New Setting("CopyingFacilitySystemIndex", CStr(SentSettings.CopyingFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(56) = New Setting("CopyingFacilityTax", CStr(SentSettings.CopyingFacilityTax))
            ManufacturingTabColumnSettingsList(57) = New Setting("CopyingFacilityMEBonus", CStr(SentSettings.CopyingFacilityMEBonus))
            ManufacturingTabColumnSettingsList(58) = New Setting("CopyingFacilityTEBonus", CStr(SentSettings.CopyingFacilityTEBonus))
            ManufacturingTabColumnSettingsList(59) = New Setting("CopyingFacilityUsage", CStr(SentSettings.CopyingFacilityUsage))
            ManufacturingTabColumnSettingsList(60) = New Setting("InventionFacilityName", CStr(SentSettings.InventionFacilityName))
            ManufacturingTabColumnSettingsList(61) = New Setting("InventionFacilitySystem", CStr(SentSettings.InventionFacilitySystem))
            ManufacturingTabColumnSettingsList(62) = New Setting("InventionFacilityRegion", CStr(SentSettings.InventionFacilityRegion))
            ManufacturingTabColumnSettingsList(63) = New Setting("InventionFacilitySystemIndex", CStr(SentSettings.InventionFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(64) = New Setting("InventionFacilityTax", CStr(SentSettings.InventionFacilityTax))
            ManufacturingTabColumnSettingsList(65) = New Setting("InventionFacilityMEBonus", CStr(SentSettings.InventionFacilityMEBonus))
            ManufacturingTabColumnSettingsList(66) = New Setting("InventionFacilityTEBonus", CStr(SentSettings.InventionFacilityTEBonus))
            ManufacturingTabColumnSettingsList(67) = New Setting("InventionFacilityUsage", CStr(SentSettings.InventionFacilityUsage))

            ManufacturingTabColumnSettingsList(68) = New Setting("ItemCategoryWidth", CStr(SentSettings.ItemCategoryWidth))
            ManufacturingTabColumnSettingsList(69) = New Setting("ItemGroupWidth", CStr(SentSettings.ItemGroupWidth))
            ManufacturingTabColumnSettingsList(70) = New Setting("ItemNameWidth", CStr(SentSettings.ItemNameWidth))
            ManufacturingTabColumnSettingsList(71) = New Setting("OwnedWidth", CStr(SentSettings.OwnedWidth))
            ManufacturingTabColumnSettingsList(72) = New Setting("TechWidth", CStr(SentSettings.TechWidth))
            ManufacturingTabColumnSettingsList(73) = New Setting("BPMEWidth", CStr(SentSettings.BPMEWidth))
            ManufacturingTabColumnSettingsList(74) = New Setting("BPTEWidth", CStr(SentSettings.BPTEWidth))
            ManufacturingTabColumnSettingsList(75) = New Setting("InputsWidth", CStr(SentSettings.InputsWidth))
            ManufacturingTabColumnSettingsList(76) = New Setting("ComparedWidth", CStr(SentSettings.ComparedWidth))
            ManufacturingTabColumnSettingsList(77) = New Setting("TotalRunsWidth", CStr(SentSettings.TotalRunsWidth))
            ManufacturingTabColumnSettingsList(78) = New Setting("SingleInventedBPCRunsWidth", CStr(SentSettings.SingleInventedBPCRunsWidth))
            ManufacturingTabColumnSettingsList(79) = New Setting("ProductionLinesWidth", CStr(SentSettings.ProductionLinesWidth))
            ManufacturingTabColumnSettingsList(80) = New Setting("LaboratoryLinesWidth", CStr(SentSettings.LaboratoryLinesWidth))
            ManufacturingTabColumnSettingsList(81) = New Setting("TotalInventionCostWidth", CStr(SentSettings.TotalInventionCostWidth))
            ManufacturingTabColumnSettingsList(82) = New Setting("TotalCopyCostWidth", CStr(SentSettings.TotalCopyCostWidth))
            ManufacturingTabColumnSettingsList(83) = New Setting("TotalManufacturingCostWidth", CStr(SentSettings.TotalManufacturingCostWidth))
            ManufacturingTabColumnSettingsList(84) = New Setting("TaxesWidth", CStr(SentSettings.TaxesWidth))
            ManufacturingTabColumnSettingsList(85) = New Setting("BrokerFeesWidth", CStr(SentSettings.BrokerFeesWidth))
            ManufacturingTabColumnSettingsList(86) = New Setting("BPProductionTimeWidth", CStr(SentSettings.BPProductionTimeWidth))
            ManufacturingTabColumnSettingsList(87) = New Setting("TotalProductionTimeWidth", CStr(SentSettings.TotalProductionTimeWidth))
            ManufacturingTabColumnSettingsList(88) = New Setting("CopyTimeWidth", CStr(SentSettings.CopyTimeWidth))
            ManufacturingTabColumnSettingsList(89) = New Setting("InventionTimeWidth", CStr(SentSettings.InventionTimeWidth))
            ManufacturingTabColumnSettingsList(90) = New Setting("ItemMarketPriceWidth", CStr(SentSettings.ItemMarketPriceWidth))
            ManufacturingTabColumnSettingsList(91) = New Setting("ProfitWidth", CStr(SentSettings.ProfitWidth))
            ManufacturingTabColumnSettingsList(92) = New Setting("ProfitPercentageWidth", CStr(SentSettings.ProfitPercentageWidth))
            ManufacturingTabColumnSettingsList(93) = New Setting("IskperHourWidth", CStr(SentSettings.IskperHourWidth))
            ManufacturingTabColumnSettingsList(94) = New Setting("SVRWidth", CStr(SentSettings.SVRWidth))
            ManufacturingTabColumnSettingsList(95) = New Setting("TotalCostWidth", CStr(SentSettings.TotalCostWidth))
            ManufacturingTabColumnSettingsList(96) = New Setting("BaseJobCostWidth", CStr(SentSettings.BaseJobCostWidth))
            ManufacturingTabColumnSettingsList(97) = New Setting("NumBPsWidth", CStr(SentSettings.NumBPsWidth))
            ManufacturingTabColumnSettingsList(98) = New Setting("InventionChanceWidth", CStr(SentSettings.InventionChanceWidth))
            ManufacturingTabColumnSettingsList(99) = New Setting("BPTypeWidth", CStr(SentSettings.BPTypeWidth))
            ManufacturingTabColumnSettingsList(100) = New Setting("RaceWidth", CStr(SentSettings.RaceWidth))
            ManufacturingTabColumnSettingsList(101) = New Setting("VolumeperItemWidth", CStr(SentSettings.VolumeperItemWidth))
            ManufacturingTabColumnSettingsList(102) = New Setting("TotalVolumeWidth", CStr(SentSettings.TotalVolumeWidth))
            ManufacturingTabColumnSettingsList(103) = New Setting("ManufacturingJobFeeWidth", CStr(SentSettings.ManufacturingJobFeeWidth))
            ManufacturingTabColumnSettingsList(104) = New Setting("ManufacturingFacilityNameWidth", CStr(SentSettings.ManufacturingFacilityNameWidth))
            ManufacturingTabColumnSettingsList(105) = New Setting("ManufacturingFacilitySystemWidth", CStr(SentSettings.ManufacturingFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(106) = New Setting("ManufacturingFacilityRegionWidth", CStr(SentSettings.ManufacturingFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(107) = New Setting("ManufacturingFacilitySystemIndexWidth", CStr(SentSettings.ManufacturingFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(108) = New Setting("ManufacturingFacilityTaxWidth", CStr(SentSettings.ManufacturingFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(109) = New Setting("ManufacturingFacilityMEBonusWidth", CStr(SentSettings.ManufacturingFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(110) = New Setting("ManufacturingFacilityTEBonusWidth", CStr(SentSettings.ManufacturingFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(111) = New Setting("ManufacturingFacilityUsageWidth", CStr(SentSettings.ManufacturingFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(112) = New Setting("ComponentFacilityNameWidth", CStr(SentSettings.ComponentFacilityNameWidth))
            ManufacturingTabColumnSettingsList(113) = New Setting("ComponentFacilitySystemWidth", CStr(SentSettings.ComponentFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(114) = New Setting("ComponentFacilityRegionWidth", CStr(SentSettings.ComponentFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(115) = New Setting("ComponentFacilitySystemIndexWidth", CStr(SentSettings.ComponentFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(116) = New Setting("ComponentFacilityTaxWidth", CStr(SentSettings.ComponentFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(117) = New Setting("ComponentFacilityMEBonusWidth", CStr(SentSettings.ComponentFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(118) = New Setting("ComponentFacilityTEBonusWidth", CStr(SentSettings.ComponentFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(119) = New Setting("ComponentFacilityUsageWidth", CStr(SentSettings.ComponentFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(120) = New Setting("CopyingFacilityNameWidth", CStr(SentSettings.CopyingFacilityNameWidth))
            ManufacturingTabColumnSettingsList(121) = New Setting("CopyingFacilitySystemWidth", CStr(SentSettings.CopyingFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(122) = New Setting("CopyingFacilityRegionWidth", CStr(SentSettings.CopyingFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(123) = New Setting("CopyingFacilitySystemIndexWidth", CStr(SentSettings.CopyingFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(124) = New Setting("CopyingFacilityTaxWidth", CStr(SentSettings.CopyingFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(125) = New Setting("CopyingFacilityMEBonusWidth", CStr(SentSettings.CopyingFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(126) = New Setting("CopyingFacilityTEBonusWidth", CStr(SentSettings.CopyingFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(127) = New Setting("CopyingFacilityUsageWidth", CStr(SentSettings.CopyingFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(128) = New Setting("InventionFacilityNameWidth", CStr(SentSettings.InventionFacilityNameWidth))
            ManufacturingTabColumnSettingsList(129) = New Setting("InventionFacilitySystemWidth", CStr(SentSettings.InventionFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(130) = New Setting("InventionFacilityRegionWidth", CStr(SentSettings.InventionFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(131) = New Setting("InventionFacilitySystemIndexWidth", CStr(SentSettings.InventionFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(132) = New Setting("InventionFacilityTaxWidth", CStr(SentSettings.InventionFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(133) = New Setting("InventionFacilityMEBonusWidth", CStr(SentSettings.InventionFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(134) = New Setting("InventionFacilityTEBonusWidth", CStr(SentSettings.InventionFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(135) = New Setting("InventionFacilityUsageWidth", CStr(SentSettings.InventionFacilityUsageWidth))

            ManufacturingTabColumnSettingsList(136) = New Setting("OrderMTByColumn", CStr(SentSettings.OrderByColumn))
            ManufacturingTabColumnSettingsList(137) = New Setting("OrderMTType", CStr(SentSettings.OrderType))

            Call WriteSettingsToFile(ManufacturingTabColumnSettingsFileName, ManufacturingTabColumnSettingsList, ManufacturingTabColumnSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Industry Jobs Column Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetManufacturingTabColumnSettings() As ManufacturingTabColumnSettings
        Return ManufacturingTabColumnSettings
    End Function

#End Region

#Region "Industry Belt Flip"

    ' Loads the tab settings
    Public Function LoadIndustryFlipBeltColumnSettings() As IndustryFlipBeltSettings
        Dim TempSettings As IndustryFlipBeltSettings = Nothing

        Try
            If FileExists(IndustryFlipBeltSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .CycleTime = CDbl(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeDouble, IndustryFlipBeltSettingsFileName, "CycleTime", DefaultCycleTime))
                    .m3perCycle = CDbl(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeDouble, IndustryFlipBeltSettingsFileName, "m3perCycle", Defaultm3perCycle))
                    .NumMiners = CInt(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeInteger, IndustryFlipBeltSettingsFileName, "NumMiners", DefaultNumMiners))
                    .CompressOre = CBool(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "CompressOre", DefaultCompressOre))
                    .IPHperMiner = CBool(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "IPHperMiner", DefaultIPHperMiner))
                    .IncludeBrokerFees = CBool(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "IncludeBrokerFees", DefaultIncludeBrokerFees))
                    .IncludeTaxes = CBool(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "IncludeTaxes", DefaultIncludeTaxes))
                    .TrueSec = CStr(GetSettingValue(IndustryFlipBeltSettingsFileName, SettingTypes.TypeString, IndustryFlipBeltSettingsFileName, "TrueSec", DefaultTruesec))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultIndustryFlipBeltSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Industry Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultIndustryFlipBeltSettings()
        End Try

        ' Save them locally and then export
        IndustryFlipBeltsSettings = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultIndustryFlipBeltSettings() As IndustryFlipBeltSettings
        Dim LocalSettings As IndustryFlipBeltSettings

        With LocalSettings
            .CycleTime = DefaultCycleTime
            .m3perCycle = Defaultm3perCycle
            .NumMiners = DefaultNumMiners
            .CompressOre = DefaultCompressOre
            .IPHperMiner = DefaultIPHperMiner
            .IncludeBrokerFees = DefaultIncludeBrokerFees
            .IncludeTaxes = DefaultIncludeTaxes
            .TrueSec = DefaultTruesec
        End With

        ' Save locally
        IndustryFlipBeltsSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIndustryFlipBeltSettings(SentSettings As IndustryFlipBeltSettings)
        Dim IndustryFlipBeltSettingsList(7) As Setting

        Try
            IndustryFlipBeltSettingsList(0) = New Setting("CycleTime", CStr(SentSettings.CycleTime))
            IndustryFlipBeltSettingsList(1) = New Setting("m3perCycle", CStr(SentSettings.m3perCycle))
            IndustryFlipBeltSettingsList(2) = New Setting("NumMiners", CStr(SentSettings.NumMiners))
            IndustryFlipBeltSettingsList(3) = New Setting("CompressedOre", CStr(SentSettings.CompressOre))
            IndustryFlipBeltSettingsList(4) = New Setting("IPHperMiner", CStr(SentSettings.IPHperMiner))
            IndustryFlipBeltSettingsList(5) = New Setting("IncludeBrokerFees", CStr(SentSettings.IncludeBrokerFees))
            IndustryFlipBeltSettingsList(6) = New Setting("IncludeTaxes", CStr(SentSettings.IncludeTaxes))
            IndustryFlipBeltSettingsList(7) = New Setting("TrueSec", CStr(SentSettings.TrueSec))

            Call WriteSettingsToFile(IndustryFlipBeltSettingsFileName, IndustryFlipBeltSettingsList, IndustryFlipBeltSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Industry Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIndustryFlipBeltSettings() As IndustryFlipBeltSettings
        Return IndustryFlipBeltsSettings
    End Function

#End Region

#Region "Industry Belt Ore Checks"

    ' Loads the tab settings
    Public Function LoadIndustryBeltOreChecksSettings(Belt As BeltType) As IndustryBeltOreChecks
        Dim TempSettings As IndustryBeltOreChecks = Nothing

        Select Case Belt
            Case BeltType.Small
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName1
            Case BeltType.Moderate
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName2
            Case BeltType.Large
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName3
            Case BeltType.ExtraLarge
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName4
            Case BeltType.Giant
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName5
        End Select

        Try
            If FileExists(IndustryBeltOreChecksFileName) Then
                'Get the settings
                With TempSettings
                    .Plagioclase = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Plagioclase", DefaultPlagioclase))
                    .Spodumain = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Spodumain", DefaultSpodumain))
                    .Kernite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Kernite", DefaultKernite))
                    .Hedbergite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Hedbergite", DefaultHedbergite))
                    .Arkonor = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Arkonor", DefaultArkonor))
                    .Bistot = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Bistot", DefaultBistot))
                    .Pyroxeres = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Pyroxeres", DefaultPyroxeres))
                    .Crokite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Crokite", DefaultCrokite))
                    .Jaspet = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Jaspet", DefaultJaspet))
                    .Omber = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Omber", DefaultOmber))
                    .Scordite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Scordite", DefaultScordite))
                    .Gneiss = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Gneiss", DefaultGneiss))
                    .Veldspar = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Veldspar", DefaultVeldspar))
                    .Hemorphite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Hemorphite", DefaultHemorphite))
                    .DarkOchre = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "DarkOchre", DefaultDarkOchre))
                    .Mercoxit = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Mercoxit", DefaultMercoxit))
                    .CrimsonArkonor = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "CrimsonArkonor", DefaultCrimsonArkonor))
                    .PrimeArkonor = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PrimeArkonor", DefaultPrimeArkonor))
                    .TriclinicBistot = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "TriclinicBistot", DefaultTriclinicBistot))
                    .MonoclinicBistot = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "MonoclinicBistot", DefaultMonoclinicBistot))
                    .SharpCrokite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "SharpCrokite", DefaultSharpCrokite))
                    .CrystallineCrokite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "CrystallineCrokite", DefaultCrystallineCrokite))
                    .OnyxOchre = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "OnyxOchre", DefaultOnyxOchre))
                    .ObsidianOchre = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "ObsidianOchre", DefaultObsidianOchre))
                    .VitricHedbergite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "VitricHedbergite", DefaultVitricHedbergite))
                    .GlazedHedbergite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "GlazedHedbergite", DefaultGlazedHedbergite))
                    .VividHemorphite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "VividHemorphite", DefaultVividHemorphite))
                    .RadiantHemorphite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "RadiantHemorphite", DefaultRadiantHemorphite))
                    .PureJaspet = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PureJaspet", DefaultPureJaspet))
                    .PristineJaspet = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PristineJaspet", DefaultPristineJaspet))
                    .LuminousKernite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "LuminousKernite", DefaultLuminousKernite))
                    .FieryKernite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "FieryKernite", DefaultFieryKernite))
                    .AzurePlagioclase = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "AzurePlagioclase", DefaultAzurePlagioclase))
                    .RichPlagioclase = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "RichPlagioclase", DefaultRichPlagioclase))
                    .SolidPyroxeres = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "SolidPyroxeres", DefaultSolidPyroxeres))
                    .ViscousPyroxeres = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "ViscousPyroxeres", DefaultViscousPyroxeres))
                    .CondensedScordite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "CondensedScordite", DefaultCondensedScordite))
                    .MassiveScordite = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "MassiveScordite", DefaultMassiveScordite))
                    .BrightSpodumain = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "BrightSpodumain", DefaultBrightSpodumain))
                    .GleamingSpodumain = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "GleamingSpodumain", DefaultGleamingSpodumain))
                    .ConcentratedVeldspar = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "ConcentratedVeldspar", DefaultConcentratedVeldspar))
                    .DenseVeldspar = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "DenseVeldspar", DefaultDenseVeldspar))
                    .IridescentGneiss = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "IridescentGneiss", DefaultIridescentGneiss))
                    .PrismaticGneiss = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PrismaticGneiss", DefaultPrismaticGneiss))
                    .SilveryOmber = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "SilveryOmber", DefaultSilveryOmber))
                    .GoldenOmber = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "GoldenOmber", DefaultGoldenOmber))
                    .MagmaMercoxit = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "MagmaMercoxit", DefaultMagmaMercoxit))
                    .VitreousMercoxit = CBool(GetSettingValue(IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "VitreousMercoxit", DefaultVitreousMercoxit))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultIndustryBeltOreChecksSettings(Belt)
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Industry Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultIndustryBeltOreChecksSettings(Belt)
        End Try

        ' Save them locally and then export
        Select Case Belt
            Case BeltType.Small
                IndustryBeltOreChecksSettings1 = TempSettings
            Case BeltType.Moderate
                IndustryBeltOreChecksSettings2 = TempSettings
            Case BeltType.Large
                IndustryBeltOreChecksSettings3 = TempSettings
            Case BeltType.ExtraLarge
                IndustryBeltOreChecksSettings4 = TempSettings
            Case BeltType.Giant
                IndustryBeltOreChecksSettings5 = TempSettings
        End Select

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultIndustryBeltOreChecksSettings(Belt As BeltType) As IndustryBeltOreChecks
        Dim LocalSettings As IndustryBeltOreChecks

        With LocalSettings
            .Plagioclase = DefaultPlagioclase
            .Spodumain = DefaultSpodumain
            .Kernite = DefaultKernite
            .Hedbergite = DefaultHedbergite
            .Arkonor = DefaultArkonor
            .Bistot = DefaultBistot
            .Pyroxeres = DefaultPyroxeres
            .Crokite = DefaultCrokite
            .Jaspet = DefaultJaspet
            .Omber = DefaultOmber
            .Scordite = DefaultScordite
            .Gneiss = DefaultGneiss
            .Veldspar = DefaultVeldspar
            .Hemorphite = DefaultHemorphite
            .DarkOchre = DefaultDarkOchre
            .Mercoxit = DefaultMercoxit
            .CrimsonArkonor = DefaultCrimsonArkonor
            .PrimeArkonor = DefaultPrimeArkonor
            .TriclinicBistot = DefaultTriclinicBistot
            .MonoclinicBistot = DefaultMonoclinicBistot
            .SharpCrokite = DefaultSharpCrokite
            .CrystallineCrokite = DefaultCrystallineCrokite
            .OnyxOchre = DefaultOnyxOchre
            .ObsidianOchre = DefaultObsidianOchre
            .VitricHedbergite = DefaultVitricHedbergite
            .GlazedHedbergite = DefaultGlazedHedbergite
            .VividHemorphite = DefaultVividHemorphite
            .RadiantHemorphite = DefaultRadiantHemorphite
            .PureJaspet = DefaultPureJaspet
            .PristineJaspet = DefaultPristineJaspet
            .LuminousKernite = DefaultLuminousKernite
            .FieryKernite = DefaultFieryKernite
            .AzurePlagioclase = DefaultAzurePlagioclase
            .RichPlagioclase = DefaultRichPlagioclase
            .SolidPyroxeres = DefaultSolidPyroxeres
            .ViscousPyroxeres = DefaultViscousPyroxeres
            .CondensedScordite = DefaultCondensedScordite
            .MassiveScordite = DefaultMassiveScordite
            .BrightSpodumain = DefaultBrightSpodumain
            .GleamingSpodumain = DefaultGleamingSpodumain
            .ConcentratedVeldspar = DefaultConcentratedVeldspar
            .DenseVeldspar = DefaultDenseVeldspar
            .IridescentGneiss = DefaultIridescentGneiss
            .PrismaticGneiss = DefaultPrismaticGneiss
            .SilveryOmber = DefaultSilveryOmber
            .GoldenOmber = DefaultGoldenOmber
            .MagmaMercoxit = DefaultMagmaMercoxit
            .VitreousMercoxit = DefaultVitreousMercoxit
        End With

        ' Save locally
        ' Save them locally and then export
        Select Case Belt
            Case BeltType.Small
                IndustryBeltOreChecksSettings1 = LocalSettings
            Case BeltType.Moderate
                IndustryBeltOreChecksSettings2 = LocalSettings
            Case BeltType.Large
                IndustryBeltOreChecksSettings3 = LocalSettings
            Case BeltType.ExtraLarge
                IndustryBeltOreChecksSettings4 = LocalSettings
            Case BeltType.Giant
                IndustryBeltOreChecksSettings5 = LocalSettings
        End Select

        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIndustryBeltOreChecksSettings(SentSettings As IndustryBeltOreChecks, Belt As BeltType)
        Dim IndustryBeltOreChecksList(47) As Setting

        Select Case Belt
            Case BeltType.Small
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName1
            Case BeltType.Moderate
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName2
            Case BeltType.Large
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName3
            Case BeltType.ExtraLarge
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName4
            Case BeltType.Giant
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksFileName & IndustryBeltOreChecksFileName5
        End Select

        Try
            IndustryBeltOreChecksList(0) = New Setting("Plagioclase", CStr(SentSettings.Plagioclase))
            IndustryBeltOreChecksList(1) = New Setting("Spodumain", CStr(SentSettings.Spodumain))
            IndustryBeltOreChecksList(2) = New Setting("Kernite", CStr(SentSettings.Kernite))
            IndustryBeltOreChecksList(3) = New Setting("Hedbergite", CStr(SentSettings.Hedbergite))
            IndustryBeltOreChecksList(4) = New Setting("Arkonor", CStr(SentSettings.Arkonor))
            IndustryBeltOreChecksList(5) = New Setting("Bistot", CStr(SentSettings.Bistot))
            IndustryBeltOreChecksList(6) = New Setting("Pyroxeres", CStr(SentSettings.Pyroxeres))
            IndustryBeltOreChecksList(7) = New Setting("Crokite", CStr(SentSettings.Crokite))
            IndustryBeltOreChecksList(8) = New Setting("Jaspet", CStr(SentSettings.Jaspet))
            IndustryBeltOreChecksList(9) = New Setting("Omber", CStr(SentSettings.Omber))
            IndustryBeltOreChecksList(10) = New Setting("Scordite", CStr(SentSettings.Scordite))
            IndustryBeltOreChecksList(11) = New Setting("Gneiss", CStr(SentSettings.Gneiss))
            IndustryBeltOreChecksList(12) = New Setting("Veldspar", CStr(SentSettings.Veldspar))
            IndustryBeltOreChecksList(13) = New Setting("Hemorphite", CStr(SentSettings.Hemorphite))
            IndustryBeltOreChecksList(14) = New Setting("DarkOchre", CStr(SentSettings.DarkOchre))
            IndustryBeltOreChecksList(15) = New Setting("Mercoxit", CStr(SentSettings.Mercoxit))
            IndustryBeltOreChecksList(16) = New Setting("CrimsonArkonor", CStr(SentSettings.CrimsonArkonor))
            IndustryBeltOreChecksList(17) = New Setting("PrimeArkonor", CStr(SentSettings.PrimeArkonor))
            IndustryBeltOreChecksList(18) = New Setting("TriclinicBistot", CStr(SentSettings.TriclinicBistot))
            IndustryBeltOreChecksList(19) = New Setting("MonoclinicBistot", CStr(SentSettings.MonoclinicBistot))
            IndustryBeltOreChecksList(20) = New Setting("SharpCrokite", CStr(SentSettings.SharpCrokite))
            IndustryBeltOreChecksList(21) = New Setting("CrystallineCrokite", CStr(SentSettings.CrystallineCrokite))
            IndustryBeltOreChecksList(22) = New Setting("OnyxOchre", CStr(SentSettings.OnyxOchre))
            IndustryBeltOreChecksList(23) = New Setting("ObsidianOchre", CStr(SentSettings.ObsidianOchre))
            IndustryBeltOreChecksList(24) = New Setting("VitricHedbergite", CStr(SentSettings.VitricHedbergite))
            IndustryBeltOreChecksList(25) = New Setting("GlazedHedbergite", CStr(SentSettings.GlazedHedbergite))
            IndustryBeltOreChecksList(26) = New Setting("VividHemorphite", CStr(SentSettings.VividHemorphite))
            IndustryBeltOreChecksList(27) = New Setting("RadiantHemorphite", CStr(SentSettings.RadiantHemorphite))
            IndustryBeltOreChecksList(28) = New Setting("PureJaspet", CStr(SentSettings.PureJaspet))
            IndustryBeltOreChecksList(29) = New Setting("PristineJaspet", CStr(SentSettings.PristineJaspet))
            IndustryBeltOreChecksList(30) = New Setting("LuminousKernite", CStr(SentSettings.LuminousKernite))
            IndustryBeltOreChecksList(31) = New Setting("FieryKernite", CStr(SentSettings.FieryKernite))
            IndustryBeltOreChecksList(32) = New Setting("AzurePlagioclase", CStr(SentSettings.AzurePlagioclase))
            IndustryBeltOreChecksList(33) = New Setting("RichPlagioclase", CStr(SentSettings.RichPlagioclase))
            IndustryBeltOreChecksList(34) = New Setting("SolidPyroxeres", CStr(SentSettings.SolidPyroxeres))
            IndustryBeltOreChecksList(35) = New Setting("ViscousPyroxeres", CStr(SentSettings.ViscousPyroxeres))
            IndustryBeltOreChecksList(36) = New Setting("CondensedScordite", CStr(SentSettings.CondensedScordite))
            IndustryBeltOreChecksList(37) = New Setting("MassiveScordite", CStr(SentSettings.MassiveScordite))
            IndustryBeltOreChecksList(38) = New Setting("BrightSpodumain", CStr(SentSettings.BrightSpodumain))
            IndustryBeltOreChecksList(39) = New Setting("GleamingSpodumain", CStr(SentSettings.GleamingSpodumain))
            IndustryBeltOreChecksList(40) = New Setting("ConcentratedVeldspar", CStr(SentSettings.ConcentratedVeldspar))
            IndustryBeltOreChecksList(41) = New Setting("DenseVeldspar", CStr(SentSettings.DenseVeldspar))
            IndustryBeltOreChecksList(42) = New Setting("IridescentGneiss", CStr(SentSettings.IridescentGneiss))
            IndustryBeltOreChecksList(43) = New Setting("PrismaticGneiss", CStr(SentSettings.PrismaticGneiss))
            IndustryBeltOreChecksList(44) = New Setting("SilveryOmber", CStr(SentSettings.SilveryOmber))
            IndustryBeltOreChecksList(45) = New Setting("GoldenOmber", CStr(SentSettings.GoldenOmber))
            IndustryBeltOreChecksList(46) = New Setting("MagmaMercoxit", CStr(SentSettings.MagmaMercoxit))
            IndustryBeltOreChecksList(47) = New Setting("VitreousMercoxit", CStr(SentSettings.VitreousMercoxit))

            Call WriteSettingsToFile(IndustryBeltOreChecksFileName, IndustryBeltOreChecksList, IndustryBeltOreChecksFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Industry Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIndustryBeltOreChecksSettings(Belt As BeltType) As IndustryBeltOreChecks
        Select Case Belt
            Case BeltType.Small
                Return IndustryBeltOreChecksSettings1
            Case BeltType.Moderate
                Return IndustryBeltOreChecksSettings2
            Case BeltType.Large
                Return IndustryBeltOreChecksSettings3
            Case BeltType.ExtraLarge
                Return IndustryBeltOreChecksSettings4
            Case BeltType.Giant
                Return IndustryBeltOreChecksSettings5
        End Select
    End Function

#End Region

#Region "Asset Window Settings"

    ' Loads the tab settings
    Public Function LoadAssetWindowSettings(Location As AssetWindow) As AssetWindowSettings
        Dim TempSettings As AssetWindowSettings = Nothing
        Dim AssetWindowFileName As String = ""

        Select Case Location
            Case AssetWindow.ProgramDefault
                AssetWindowFileName = AssetWindowFileNameDefault
            Case AssetWindow.ShoppingList
                AssetWindowFileName = AssetWindowFileNameShoppingList
        End Select

        Try
            If FileExists(AssetWindowFileName) Then

                'Get the settings
                With TempSettings
                    ' Main window
                    .AssetType = CStr(GetSettingValue(AssetWindowFileName, SettingTypes.TypeString, AssetWindowFileName, "AssetType", DefaultAssetType))
                    .SortbyName = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "SortbyName", DefaultAssetSortbyName))

                    ' Search Settings
                    .ItemFilterText = CStr(GetSettingValue(AssetWindowFileName, SettingTypes.TypeString, AssetWindowFileName, "ItemFilterText", DefaultAssetItemTextFilter))
                    .AllItems = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AllItems", DefaultAllItems))
                    .AllRawMats = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AllRawMats", DefaultAssetItemChecks))
                    .Minerals = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Minerals", DefaultAssetItemChecks))
                    .IceProducts = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "IceProducts", DefaultAssetItemChecks))
                    .Gas = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Gas", DefaultAssetItemChecks))
                    .Misc = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Misc", DefaultAssetItemChecks))
                    .BPCs = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "BPCs", DefaultAssetItemChecks))
                    .AncientRelics = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AncientRelics", DefaultAssetItemChecks))
                    .AncientSalvage = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AncientSalvage", DefaultAssetItemChecks))
                    .Salvage = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Salvage", DefaultAssetItemChecks))
                    .StationComponents = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "StationComponents", DefaultAssetItemChecks))
                    .Planetary = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Planetary", DefaultAssetItemChecks))
                    .Datacores = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Datacores", DefaultAssetItemChecks))
                    .Decryptors = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Decryptors", DefaultAssetItemChecks))
                    .RawMats = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "RawMats", DefaultAssetItemChecks))
                    .ProcessedMats = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "ProcessedMats", DefaultAssetItemChecks))
                    .AdvancedMats = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AdvancedMats", DefaultAssetItemChecks))
                    .MatsandCompounds = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "MatsandCompounds", DefaultAssetItemChecks))
                    .DroneComponents = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "DroneComponents", DefaultAssetItemChecks))
                    .BoosterMats = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "BoosterMats", DefaultAssetItemChecks))
                    .Polymers = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Polymers", DefaultAssetItemChecks))
                    .Asteroids = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Asteroids", DefaultAssetItemChecks))
                    .AllManufacturedItems = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AllManufacturedItems", DefaultAssetItemChecks))
                    .Ships = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Ships", DefaultAssetItemChecks))
                    .Modules = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Modules", DefaultAssetItemChecks))
                    .Drones = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Drones", DefaultAssetItemChecks))
                    .Boosters = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Boosters", DefaultAssetItemChecks))
                    .Rigs = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Rigs", DefaultAssetItemChecks))
                    .Charges = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Charges", DefaultAssetItemChecks))
                    .Subsystems = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Subsystems", DefaultAssetItemChecks))
                    .Structures = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Structures", DefaultAssetItemChecks))
                    .Tools = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Tools", DefaultAssetItemChecks))
                    .DataInterfaces = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "DataInterfaces", DefaultAssetItemChecks))
                    .CapT2Components = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "CapT2Components", DefaultAssetItemChecks))
                    .CapitalComponents = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "CapitalComponents", DefaultAssetItemChecks))
                    .Components = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Components", DefaultAssetItemChecks))
                    .Hybrid = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Hybrid", DefaultAssetItemChecks))
                    .FuelBlocks = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "FuelBlocks", DefaultAssetItemChecks))
                    .T1 = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "T1", DefaultAssetItemChecks))
                    .T2 = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "T2", DefaultAssetItemChecks))
                    .T3 = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "T3", DefaultAssetItemChecks))
                    .Faction = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Faction", DefaultAssetItemChecks))
                    .Pirate = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Pirate", DefaultAssetItemChecks))
                    .Storyline = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Storyline", DefaultAssetItemChecks))

                    .Celestials = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Celestials", DefaultAssetItemChecks))
                    .Deployables = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Deployables", DefaultAssetItemChecks))
                    .Implants = CBool(GetSettingValue(AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Implants", DefaultAssetItemChecks))

                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultAssetWindowSettings(Location)

            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Asset Window Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults                            
            TempSettings = SetDefaultAssetWindowSettings(Location)
        End Try

        ' Save them locally and then export
        Select Case Location
            Case AssetWindow.ProgramDefault
                AssetWindowSettingsDefault = TempSettings
            Case AssetWindow.ShoppingList
                AssetWindowSettingsShoppingList = TempSettings
        End Select

        Return TempSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveAssetWindowSettings(ItemsSelected As AssetWindowSettings, Location As AssetWindow)
        Dim AssetWindowSettingsList(49) As Setting
        Dim AssetWindowFileName As String = ""

        Select Case Location
            Case AssetWindow.ProgramDefault
                AssetWindowFileName = AssetWindowFileNameDefault
            Case AssetWindow.ShoppingList
                AssetWindowFileName = AssetWindowFileNameShoppingList
        End Select

        Try
            AssetWindowSettingsList(0) = New Setting("AllRawMats", CStr(ItemsSelected.AllRawMats))
            AssetWindowSettingsList(1) = New Setting("Minerals", CStr(ItemsSelected.Minerals))
            AssetWindowSettingsList(2) = New Setting("IceProducts", CStr(ItemsSelected.IceProducts))
            AssetWindowSettingsList(3) = New Setting("Gas", CStr(ItemsSelected.Gas))
            AssetWindowSettingsList(4) = New Setting("AncientRelics", CStr(ItemsSelected.AncientRelics))
            AssetWindowSettingsList(5) = New Setting("AncientSalvage", CStr(ItemsSelected.AncientSalvage))
            AssetWindowSettingsList(6) = New Setting("Salvage", CStr(ItemsSelected.Salvage))
            AssetWindowSettingsList(7) = New Setting("StationComponents", CStr(ItemsSelected.StationComponents))
            AssetWindowSettingsList(8) = New Setting("Planetary", CStr(ItemsSelected.Planetary))
            AssetWindowSettingsList(9) = New Setting("Datacores", CStr(ItemsSelected.Datacores))
            AssetWindowSettingsList(10) = New Setting("Decryptors", CStr(ItemsSelected.Decryptors))
            AssetWindowSettingsList(11) = New Setting("RawMats", CStr(ItemsSelected.RawMats))
            AssetWindowSettingsList(12) = New Setting("ProcessedMats", CStr(ItemsSelected.ProcessedMats))
            AssetWindowSettingsList(13) = New Setting("AdvancedMats", CStr(ItemsSelected.AdvancedMats))
            AssetWindowSettingsList(14) = New Setting("MatsandCompounds", CStr(ItemsSelected.MatsandCompounds))
            AssetWindowSettingsList(15) = New Setting("DroneComponents", CStr(ItemsSelected.DroneComponents))
            AssetWindowSettingsList(16) = New Setting("BoosterMats", CStr(ItemsSelected.BoosterMats))
            AssetWindowSettingsList(17) = New Setting("Polymers", CStr(ItemsSelected.Polymers))
            AssetWindowSettingsList(18) = New Setting("AllManufacturedItems", CStr(ItemsSelected.AllManufacturedItems))
            AssetWindowSettingsList(19) = New Setting("Ships", CStr(ItemsSelected.Ships))
            AssetWindowSettingsList(20) = New Setting("Modules", CStr(ItemsSelected.Modules))
            AssetWindowSettingsList(21) = New Setting("Drones", CStr(ItemsSelected.Drones))
            AssetWindowSettingsList(22) = New Setting("Boosters", CStr(ItemsSelected.Boosters))
            AssetWindowSettingsList(23) = New Setting("Rigs", CStr(ItemsSelected.Rigs))
            AssetWindowSettingsList(24) = New Setting("Charges", CStr(ItemsSelected.Charges))
            AssetWindowSettingsList(25) = New Setting("Subsystems", CStr(ItemsSelected.Subsystems))
            AssetWindowSettingsList(26) = New Setting("Structures", CStr(ItemsSelected.Structures))
            AssetWindowSettingsList(27) = New Setting("Tools", CStr(ItemsSelected.Tools))
            AssetWindowSettingsList(28) = New Setting("DataInterfaces", CStr(ItemsSelected.DataInterfaces))
            AssetWindowSettingsList(29) = New Setting("CapT2Components", CStr(ItemsSelected.CapT2Components))
            AssetWindowSettingsList(30) = New Setting("CapitalComponents", CStr(ItemsSelected.CapitalComponents))
            AssetWindowSettingsList(31) = New Setting("Components", CStr(ItemsSelected.Components))
            AssetWindowSettingsList(32) = New Setting("Hybrid", CStr(ItemsSelected.Hybrid))
            AssetWindowSettingsList(33) = New Setting("FuelBlocks", CStr(ItemsSelected.FuelBlocks))
            AssetWindowSettingsList(34) = New Setting("T1", CStr(ItemsSelected.T1))
            AssetWindowSettingsList(35) = New Setting("T2", CStr(ItemsSelected.T2))
            AssetWindowSettingsList(36) = New Setting("T3", CStr(ItemsSelected.T3))
            AssetWindowSettingsList(37) = New Setting("Faction", CStr(ItemsSelected.Faction))
            AssetWindowSettingsList(38) = New Setting("Pirate", CStr(ItemsSelected.Pirate))
            AssetWindowSettingsList(39) = New Setting("Storyline", CStr(ItemsSelected.Storyline))
            AssetWindowSettingsList(40) = New Setting("Asteroids", CStr(ItemsSelected.Asteroids))
            AssetWindowSettingsList(41) = New Setting("Misc", CStr(ItemsSelected.Misc))
            AssetWindowSettingsList(42) = New Setting("ItemFilterText", CStr(ItemsSelected.ItemFilterText))
            AssetWindowSettingsList(43) = New Setting("AllItems", CStr(ItemsSelected.AllItems))

            ' Main window
            AssetWindowSettingsList(44) = New Setting("AssetType", CStr(ItemsSelected.AssetType))
            AssetWindowSettingsList(45) = New Setting("SortbyName", CStr(ItemsSelected.SortbyName))

            AssetWindowSettingsList(46) = New Setting("Celestials", CStr(ItemsSelected.Celestials))
            AssetWindowSettingsList(47) = New Setting("Deployables", CStr(ItemsSelected.Deployables))
            AssetWindowSettingsList(48) = New Setting("Implants", CStr(ItemsSelected.Implants))
            AssetWindowSettingsList(49) = New Setting("BPCs", CStr(ItemsSelected.BPCs))

            Call WriteSettingsToFile(AssetWindowFileName, AssetWindowSettingsList, AssetWindowFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Asset Window Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetAssetWindowSettings(Location As AssetWindow) As AssetWindowSettings

        Select Case Location
            Case AssetWindow.ProgramDefault
                Return AssetWindowSettingsDefault
            Case AssetWindow.ShoppingList
                Return AssetWindowSettingsShoppingList
            Case Else
                Return Nothing
        End Select

    End Function

    Public Function SetDefaultAssetWindowSettings(Location As AssetWindow) As AssetWindowSettings
        Dim LocalSettings As AssetWindowSettings = Nothing

        With LocalSettings
            .AssetType = DefaultAssetType
            .SortbyName = DefaultAssetSortbyName

            .ItemFilterText = DefaultAssetItemTextFilter
            .AllItems = DefaultAllItems
            .AllRawMats = DefaultAssetItemChecks
            .Minerals = DefaultAssetItemChecks
            .IceProducts = DefaultAssetItemChecks
            .Gas = DefaultAssetItemChecks
            .Misc = DefaultAssetItemChecks
            .BPCs = DefaultAssetItemChecks
            .AncientRelics = DefaultAssetItemChecks
            .AncientSalvage = DefaultAssetItemChecks
            .Salvage = DefaultAssetItemChecks
            .StationComponents = DefaultAssetItemChecks
            .Planetary = DefaultAssetItemChecks
            .Datacores = DefaultAssetItemChecks
            .Decryptors = DefaultAssetItemChecks
            .RawMats = DefaultAssetItemChecks
            .ProcessedMats = DefaultAssetItemChecks
            .AdvancedMats = DefaultAssetItemChecks
            .MatsandCompounds = DefaultAssetItemChecks
            .DroneComponents = DefaultAssetItemChecks
            .BoosterMats = DefaultAssetItemChecks
            .Polymers = DefaultAssetItemChecks
            .Asteroids = DefaultAssetItemChecks
            .AllManufacturedItems = DefaultAssetItemChecks
            .Ships = DefaultAssetItemChecks
            .Modules = DefaultAssetItemChecks
            .Drones = DefaultAssetItemChecks
            .Boosters = DefaultAssetItemChecks
            .Rigs = DefaultAssetItemChecks
            .Charges = DefaultAssetItemChecks
            .Subsystems = DefaultAssetItemChecks
            .Structures = DefaultAssetItemChecks
            .Tools = DefaultAssetItemChecks
            .DataInterfaces = DefaultAssetItemChecks
            .CapT2Components = DefaultAssetItemChecks
            .CapitalComponents = DefaultAssetItemChecks
            .Components = DefaultAssetItemChecks
            .Hybrid = DefaultAssetItemChecks
            .FuelBlocks = DefaultAssetItemChecks
            .T1 = DefaultAssetItemChecks
            .T2 = DefaultAssetItemChecks
            .T3 = DefaultAssetItemChecks
            .Faction = DefaultAssetItemChecks
            .Pirate = DefaultAssetItemChecks
            .Storyline = DefaultAssetItemChecks
            .Celestials = DefaultAssetItemChecks
            .Deployables = DefaultAssetItemChecks
            .Implants = DefaultAssetItemChecks
        End With

        ' Save locally - Will have more than one
        Select Case Location
            Case AssetWindow.ProgramDefault
                AssetWindowSettingsDefault = LocalSettings
            Case AssetWindow.ShoppingList
                AssetWindowSettingsShoppingList = LocalSettings
        End Select

        Return LocalSettings

    End Function

#End Region

#Region "POS Settings"

    ' Loads the POS tower settings from XML setting file
    Public Function LoadPOSSettings() As PlayerOwnedStationSettings
        Dim TempSettings As PlayerOwnedStationSettings = Nothing

        Try
            If FileExists(POSSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .TowerRaceID = CInt(GetSettingValue(POSSettingsFileName, SettingTypes.TypeInteger, POSSettingsFileName, "TowerRaceID", DefaultTowerRaceID))
                    .TowerName = CStr(GetSettingValue(POSSettingsFileName, SettingTypes.TypeString, POSSettingsFileName, "TowerName", DefaultTowerName))
                    .CostperHour = CDbl(GetSettingValue(POSSettingsFileName, SettingTypes.TypeDouble, POSSettingsFileName, "CostperHour", DefaultCostperHour))
                    .MECostperSecond = CDbl(GetSettingValue(POSSettingsFileName, SettingTypes.TypeDouble, POSSettingsFileName, "MECostperSecond", DefaultMECostperSecond))
                    .TECostperSecond = CDbl(GetSettingValue(POSSettingsFileName, SettingTypes.TypeDouble, POSSettingsFileName, "TECostperSecond", DefaultTECostperSecond))
                    .InventionCostperSecond = CDbl(GetSettingValue(POSSettingsFileName, SettingTypes.TypeDouble, POSSettingsFileName, "InventionCostperSecond", DefaultInventionCostperSecond))
                    .CopyCostperSecond = CDbl(GetSettingValue(POSSettingsFileName, SettingTypes.TypeDouble, POSSettingsFileName, "CopyCostperSecond", DefaultCopyCostperSecond))
                    .TowerType = CStr(GetSettingValue(POSSettingsFileName, SettingTypes.TypeString, POSSettingsFileName, "TowerType", DefaultTowerType))
                    .TowerSize = CStr(GetSettingValue(POSSettingsFileName, SettingTypes.TypeString, POSSettingsFileName, "TowerSize", DefaultTowerSize))
                    .FuelBlockBuild = CBool(GetSettingValue(POSSettingsFileName, SettingTypes.TypeBoolean, POSSettingsFileName, "FuelBlockBuild", DefaultFuelBlockBuild))
                    .NumAdvLabs = CInt(GetSettingValue(POSSettingsFileName, SettingTypes.TypeInteger, POSSettingsFileName, "NumAdvLabs", DefaultNumAdvLabs))
                    .NumMobileLabs = CInt(GetSettingValue(POSSettingsFileName, SettingTypes.TypeInteger, POSSettingsFileName, "NumMobileLabs", DefaultNumMobileLabs))
                    .NumHyasyodaLabs = CInt(GetSettingValue(POSSettingsFileName, SettingTypes.TypeInteger, POSSettingsFileName, "NumHyasyodaLabs", DefaultNumHyasyodaLabs))
                    .CharterCost = CDbl(GetSettingValue(POSSettingsFileName, SettingTypes.TypeInteger, POSSettingsFileName, "CharterCost", DefaultCharterCost))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultPOSSettings()
            End If
        Catch ex As Exception
            MsgBox("An error occured when loading POS Tower Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultPOSSettings()
        End Try

        ' Save them locally and then export
        POSSettings = TempSettings

        Return TempSettings

    End Function

    ' Load defaults 
    Public Function SetDefaultPOSSettings() As PlayerOwnedStationSettings
        Dim TempSettings As PlayerOwnedStationSettings = Nothing

        ' Load defaults 
        TempSettings.TowerRaceID = DefaultTowerRaceID
        TempSettings.TowerName = DefaultTowerName
        TempSettings.CostperHour = DefaultCostperHour
        TempSettings.MECostperSecond = DefaultMECostperSecond
        TempSettings.TECostperSecond = DefaultTECostperSecond
        TempSettings.InventionCostperSecond = DefaultInventionCostperSecond
        TempSettings.CopyCostperSecond = DefaultCopyCostperSecond
        TempSettings.TowerType = DefaultTowerType
        TempSettings.TowerSize = DefaultTowerSize
        TempSettings.FuelBlockBuild = DefaultFuelBlockBuild
        TempSettings.NumAdvLabs = DefaultNumAdvLabs
        TempSettings.NumMobileLabs = DefaultNumMobileLabs
        TempSettings.NumHyasyodaLabs = DefaultNumHyasyodaLabs
        TempSettings.CharterCost = DefaultCharterCost

        POSSettings = TempSettings
        Return TempSettings

    End Function

    ' Saves the POS Settings to XML
    Public Sub SavePOSSettings(SentSettings As PlayerOwnedStationSettings)
        Dim POSSettingsList(13) As Setting

        Try
            POSSettingsList(0) = New Setting("TowerRaceID", CStr(SentSettings.TowerRaceID))
            POSSettingsList(1) = New Setting("TowerName", CStr(SentSettings.TowerName))
            POSSettingsList(2) = New Setting("CostperHour", CStr(SentSettings.CostperHour))
            POSSettingsList(3) = New Setting("MECostperSecond", CStr(SentSettings.MECostperSecond))
            POSSettingsList(4) = New Setting("TECostperSecond", CStr(SentSettings.TECostperSecond))
            POSSettingsList(5) = New Setting("InventionCostperSecond", CStr(SentSettings.InventionCostperSecond))
            POSSettingsList(6) = New Setting("CopyCostperSecond", CStr(SentSettings.CopyCostperSecond))
            POSSettingsList(7) = New Setting("TowerType", CStr(SentSettings.TowerType))
            POSSettingsList(8) = New Setting("TowerSize", CStr(SentSettings.TowerSize))
            POSSettingsList(9) = New Setting("FuelBlockBuild", CStr(SentSettings.FuelBlockBuild))
            POSSettingsList(10) = New Setting("NumAdvLabs", CStr(SentSettings.NumAdvLabs))
            POSSettingsList(11) = New Setting("NumMobileLabs", CStr(SentSettings.NumMobileLabs))
            POSSettingsList(12) = New Setting("NumHyasyodaLabs", CStr(SentSettings.NumHyasyodaLabs))
            POSSettingsList(13) = New Setting("CharterCost", CStr(SentSettings.CharterCost))

            Call WriteSettingsToFile(POSSettingsFileName, POSSettingsList, POSSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving POS Tower Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the POS Tower Settings
    Public Function GetPOSSettings() As PlayerOwnedStationSettings
        Return POSSettings
    End Function

#End Region

#Region "Team Settings"

    ' Loads the Team settings from XML setting file
    Public Function LoadTeamSettings(IndustryTeamType As TeamType, Tab As String) As TeamSettings
        Dim TempSettings As TeamSettings = Nothing
        Dim TeamFileName As String = ""

        Select Case IndustryTeamType
            Case TeamType.Manufacturing
                TeamFileName = Tab & ManufacturingTeamSettingsFileName
            Case TeamType.ComponentManufacturing
                TeamFileName = Tab & ComponentManufacturingTeamSettingsFileName
            Case TeamType.Copy
                TeamFileName = Tab & CopyTeamSettingsFileName
            Case TeamType.Invention
                TeamFileName = Tab & InventionTeamSettingsFileName
        End Select

        TeamFileName = TeamFileName

        Try
            If FileExists(TeamFileName) Then
                'Get the settings
                With TempSettings
                    Select Case IndustryTeamType
                        Case TeamType.Manufacturing
                            .TeamID = CInt(GetSettingValue(TeamFileName, SettingTypes.TypeInteger, Tab & ManufacturingTeamSettingsFileName, "TeamID", DefaultTeamID))
                        Case TeamType.ComponentManufacturing
                            .TeamID = CInt(GetSettingValue(TeamFileName, SettingTypes.TypeInteger, Tab & ComponentManufacturingTeamSettingsFileName, "TeamID", DefaultTeamID))
                        Case TeamType.Copy
                            .TeamID = CInt(GetSettingValue(TeamFileName, SettingTypes.TypeInteger, Tab & CopyTeamSettingsFileName, "TeamID", DefaultTeamID))
                        Case TeamType.Invention
                            .TeamID = CInt(GetSettingValue(TeamFileName, SettingTypes.TypeInteger, Tab & InventionTeamSettingsFileName, "TeamID", DefaultTeamID))
                    End Select
                End With
            Else
                ' Load defaults 
                TempSettings = SetDefaultTeamSettings(IndustryTeamType, Tab)
            End If
        Catch ex As Exception
            MsgBox("An error occured when loading Team Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultTeamSettings(IndustryTeamType, Tab)
        End Try

        TempSettings.TeamTab = Tab

        ' Save them locally and then export
        Select Case IndustryTeamType
            Case TeamType.Manufacturing
                BPManufacturingTeamSettings = TempSettings
            Case TeamType.ComponentManufacturing
                BPComponentManufacturingTeamSettings = TempSettings
            Case TeamType.Copy
                BPCopyTeamSettings = TempSettings
            Case TeamType.Invention
                BPInventionTeamSettings = TempSettings
        End Select

        Return TempSettings

    End Function

    ' Saves the Team Settings to XML
    Public Sub SaveTeamSettings(SentSettings As TeamSettings, IndustryTeamType As TeamType)
        Dim TeamSettingsList(0) As Setting

        Try
            TeamSettingsList(0) = New Setting("TeamID", CStr(SentSettings.TeamID))

            Select Case IndustryTeamType
                Case TeamType.Manufacturing
                    Call WriteSettingsToFile(SentSettings.TeamTab & ManufacturingTeamSettingsFileName, TeamSettingsList, SentSettings.TeamTab & ManufacturingTeamSettingsFileName)
                Case TeamType.ComponentManufacturing
                    Call WriteSettingsToFile(SentSettings.TeamTab & ComponentManufacturingTeamSettingsFileName, TeamSettingsList, SentSettings.TeamTab & ComponentManufacturingTeamSettingsFileName)
                Case TeamType.Copy
                    Call WriteSettingsToFile(SentSettings.TeamTab & CopyTeamSettingsFileName, TeamSettingsList, SentSettings.TeamTab & CopyTeamSettingsFileName)
                Case TeamType.Invention
                    Call WriteSettingsToFile(SentSettings.TeamTab & InventionTeamSettingsFileName, TeamSettingsList, SentSettings.TeamTab & InventionTeamSettingsFileName)
            End Select

        Catch ex As Exception
            MsgBox("An error occured when saving Team Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the Team Settings
    Public Function GetTeamSettings(IndustryTeamType As TeamType, Tab As String) As TeamSettings

        If Tab = BPTab Then
            Select Case IndustryTeamType
                Case TeamType.Manufacturing
                    Return BPManufacturingTeamSettings
                Case TeamType.ComponentManufacturing
                    Return BPComponentManufacturingTeamSettings
                Case TeamType.Copy
                    Return BPCopyTeamSettings
                Case TeamType.Invention
                    Return BPInventionTeamSettings
            End Select
        Else
            Select Case IndustryTeamType
                Case TeamType.Manufacturing
                    Return CalcManufacturingTeamSettings
                Case TeamType.ComponentManufacturing
                    Return CalcComponentManufacturingTeamSettings
                Case TeamType.Copy
                    Return CalcCopyTeamSettings
                Case TeamType.Invention
                    Return CalcInventionTeamSettings
            End Select
        End If

        Return Nothing

    End Function

    ' Load defaults 
    Public Function SetDefaultTeamSettings(IndustryTeamType As TeamType, Tab As String) As TeamSettings
        Dim TempSettings As TeamSettings = Nothing

        ' Load defaults 
        TempSettings.TeamID = DefaultTeamID

        If Tab = BPTab Then
            Select Case IndustryTeamType
                Case TeamType.Manufacturing
                    BPManufacturingTeamSettings = TempSettings
                Case TeamType.ComponentManufacturing
                    BPComponentManufacturingTeamSettings = TempSettings
                Case TeamType.Copy
                    BPCopyTeamSettings = TempSettings
                Case TeamType.Invention
                    BPInventionTeamSettings = TempSettings
            End Select
        Else
            Select Case IndustryTeamType
                Case TeamType.Manufacturing
                    CalcManufacturingTeamSettings = TempSettings
                Case TeamType.ComponentManufacturing
                    CalcComponentManufacturingTeamSettings = TempSettings
                Case TeamType.Copy
                    CalcCopyTeamSettings = TempSettings
                Case TeamType.Invention
                    CalcInventionTeamSettings = TempSettings
            End Select
        End If

        TempSettings.TeamTab = Tab

        Return TempSettings

    End Function

#End Region

#Region "Facility Settings"

    ' Loads the Facility settings from XML setting file
    Public Function LoadFacilitySettings(ProductionType As IndustryType, Tab As String) As FacilitySettings
        Dim TempSettings As FacilitySettings = Nothing
        Dim FacilityFileName As String = ""

        Select Case ProductionType
            Case IndustryType.Manufacturing
                FacilityFileName = ManufacturingFacilitySettingsFileName
            Case IndustryType.ComponentManufacturing
                FacilityFileName = ComponentsManufacturingFacilitySettingsFileName
            Case IndustryType.CapitalComponentManufacturing
                FacilityFileName = CapitalComponentsManufacturingFacilitySettingsFileName
            Case IndustryType.CapitalManufacturing
                FacilityFileName = CapitalManufacturingFacilitySettingsFileName
            Case IndustryType.SuperManufacturing
                FacilityFileName = SuperCapitalManufacturingFacilitySettingsFileName
            Case IndustryType.BoosterManufacturing
                FacilityFileName = BoosterManufacturingFacilitySettingsFileName
            Case IndustryType.T3CruiserManufacturing
                FacilityFileName = T3CruiserManufacturingFacilitySettingsFileName
            Case IndustryType.T3DestroyerManufacturing
                FacilityFileName = T3DestroyerManufacturingFacilitySettingsFileName
            Case IndustryType.SubsystemManufacturing
                FacilityFileName = SubsystemManufacturingFacilitySettingsFileName
            Case IndustryType.Copying
                FacilityFileName = CopyFacilitySettingsFileName
            Case IndustryType.Invention
                FacilityFileName = InventionFacilitySettingsFileName
            Case IndustryType.T3Invention
                FacilityFileName = T3InventionFacilitySettingsFileName
            Case IndustryType.NoPOSManufacturing
                FacilityFileName = NoPoSFacilitySettingsFileName
            Case IndustryType.POSFuelBlockManufacturing
                FacilityFileName = POSFuelBlockFacilitySettingsFileName
            Case IndustryType.POSLargeShipManufacturing
                FacilityFileName = POSLargeShipFacilitySettingsFileName
            Case IndustryType.POSModuleManufacturing
                FacilityFileName = POSModuleFacilitySettingsFileName
        End Select

        FacilityFileName = Tab & FacilityFileName

        Try
            If FileExists(FacilityFileName) Then
                'Get the settings
                With TempSettings
                    Select Case ProductionType
                        Case IndustryType.Manufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ManufacturingFacilitySettingsFileName, "Facility", DefaultBPManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ManufacturingFacilitySettingsFileName, "FacilityType", DefaultManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & ManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & ManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & ManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & ManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & ManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & ManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & ManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.ComponentManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ComponentsManufacturingFacilitySettingsFileName, "Facility", DefaultComponentManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ComponentsManufacturingFacilitySettingsFileName, "FacilityType", DefaultComponentManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & ComponentsManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & ComponentsManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ComponentsManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ComponentsManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ComponentsManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & ComponentsManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ComponentsManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & ComponentsManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & ComponentsManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & ComponentsManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & ComponentsManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & ComponentsManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & ComponentsManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.CapitalComponentManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "Facility", DefaultCapitalComponentManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "FacilityType", DefaultCapitalComponentManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CapitalComponentsManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.CapitalManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalManufacturingFacilitySettingsFileName, "Facility", DefaultCapitalManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalManufacturingFacilitySettingsFileName, "FacilityType", DefaultCapitalManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & CapitalManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & CapitalManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & CapitalManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & CapitalManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CapitalManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CapitalManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CapitalManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CapitalManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CapitalManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.SuperManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "Facility", DefaultSuperManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "FacilityType", DefaultSuperManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & SuperCapitalManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.BoosterManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & BoosterManufacturingFacilitySettingsFileName, "Facility", DefaultBoosterManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & BoosterManufacturingFacilitySettingsFileName, "FacilityType", DefaultBoosterManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & BoosterManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & BoosterManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & BoosterManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & BoosterManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & BoosterManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & BoosterManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & BoosterManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & BoosterManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & BoosterManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & BoosterManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & BoosterManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & BoosterManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & BoosterManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.T3CruiserManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3CruiserManufacturingFacilitySettingsFileName, "Facility", DefaultT3CruiserManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3CruiserManufacturingFacilitySettingsFileName, "FacilityType", DefaultT3CruiserManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & T3CruiserManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & T3CruiserManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3CruiserManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3CruiserManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3CruiserManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & T3CruiserManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3CruiserManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & T3CruiserManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3CruiserManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3CruiserManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3CruiserManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3CruiserManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3CruiserManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.T3DestroyerManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "Facility", DefaultT3DestroyerManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "FacilityType", DefaultT3DestroyerManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3DestroyerManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.SubsystemManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SubsystemManufacturingFacilitySettingsFileName, "Facility", DefaultSubsystemManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SubsystemManufacturingFacilitySettingsFileName, "FacilityType", DefaultSubsystemManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & SubsystemManufacturingFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & SubsystemManufacturingFacilitySettingsFileName, "ProductionType", IndustryType.Manufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SubsystemManufacturingFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SubsystemManufacturingFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SubsystemManufacturingFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & SubsystemManufacturingFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SubsystemManufacturingFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & SubsystemManufacturingFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & SubsystemManufacturingFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & SubsystemManufacturingFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & SubsystemManufacturingFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & SubsystemManufacturingFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & SubsystemManufacturingFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.Copying
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CopyFacilitySettingsFileName, "Facility", DefaultCopyFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CopyFacilitySettingsFileName, "FacilityType", DefaultCopyFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & CopyFacilitySettingsFileName, "ActivityID", IndustryActivities.Copying))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & CopyFacilitySettingsFileName, "ProductionType", IndustryType.Copying), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CopyFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CopyFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CopyFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & CopyFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CopyFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & CopyFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & CopyFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & CopyFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CopyFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CopyFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & CopyFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.Invention
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & InventionFacilitySettingsFileName, "Facility", DefaultInventionFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & InventionFacilitySettingsFileName, "FacilityType", DefaultInventionFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & InventionFacilitySettingsFileName, "ActivityID", IndustryActivities.Invention))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & InventionFacilitySettingsFileName, "ProductionType", IndustryType.Invention), IndustryType)
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & InventionFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & InventionFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & InventionFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & InventionFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & InventionFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & InventionFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & InventionFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & InventionFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & InventionFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & InventionFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & InventionFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.T3Invention
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3InventionFacilitySettingsFileName, "Facility", DefaultT3InventionFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3InventionFacilitySettingsFileName, "FacilityType", DefaultT3InventionFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & T3InventionFacilitySettingsFileName, "ActivityID", IndustryActivities.Invention))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & T3InventionFacilitySettingsFileName, "ProductionType", IndustryType.Invention), IndustryType)
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3InventionFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3InventionFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3InventionFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & T3InventionFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3InventionFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & T3InventionFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & T3InventionFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & T3InventionFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3InventionFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3InventionFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & T3InventionFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.NoPOSManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & NoPoSFacilitySettingsFileName, "Facility", DefaultManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & NoPoSFacilitySettingsFileName, "FacilityType", DefaultManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & NoPoSFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & NoPoSFacilitySettingsFileName, "ProductionType", IndustryType.NoPOSManufacturing), IndustryType)
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & NoPoSFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & NoPoSFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & NoPoSFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & NoPoSFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & NoPoSFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & NoPoSFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & NoPoSFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & NoPoSFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & NoPoSFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & NoPoSFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & NoPoSFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.POSFuelBlockManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSFuelBlockFacilitySettingsFileName, "Facility", DefaultPOSFuelBlockManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSFuelBlockFacilitySettingsFileName, "FacilityType", DefaultPOSFuelBlockManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & POSFuelBlockFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & POSFuelBlockFacilitySettingsFileName, "ProductionType", IndustryType.POSFuelBlockManufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSFuelBlockFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSFuelBlockFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSFuelBlockFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & POSFuelBlockFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSFuelBlockFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & POSFuelBlockFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSFuelBlockFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSFuelBlockFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSFuelBlockFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSFuelBlockFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSFuelBlockFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.POSModuleManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSModuleFacilitySettingsFileName, "Facility", DefaultPOSModuleManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSModuleFacilitySettingsFileName, "FacilityType", DefaultPOSModuleManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & POSModuleFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & POSModuleFacilitySettingsFileName, "ProductionType", IndustryType.POSModuleManufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSModuleFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSModuleFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSModuleFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & POSModuleFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSModuleFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & POSModuleFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSModuleFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSModuleFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSModuleFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSModuleFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSModuleFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                        Case IndustryType.POSLargeShipManufacturing
                            .Facility = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSLargeShipFacilitySettingsFileName, "Facility", DefaultPOSLargeShipManufacturingFacility))
                            .FacilityType = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSLargeShipFacilitySettingsFileName, "FacilityType", DefaultPOSLargeShipManufacturingFacilityType))
                            .ActivityID = CInt(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & POSLargeShipFacilitySettingsFileName, "ActivityID", IndustryActivities.Manufacturing))
                            .ProductionType = CType(GetSettingValue(FacilityFileName, SettingTypes.TypeInteger, Tab & POSLargeShipFacilitySettingsFileName, "ProductionType", IndustryType.POSLargeShipManufacturing), IndustryType)
                            .MaterialMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSLargeShipFacilitySettingsFileName, "MaterialMultiplier", FacilityDefaultMM))
                            .TimeMultiplier = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSLargeShipFacilitySettingsFileName, "TimeMultiplier", FacilityDefaultTM))
                            .TaxRate = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSLargeShipFacilitySettingsFileName, "TaxRate", OutpostDefaultTax))
                            .SolarSystemID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & POSLargeShipFacilitySettingsFileName, "SolarSystemID", FacilityDefaultSolarSystemID))
                            .SolarSystemName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSLargeShipFacilitySettingsFileName, "SolarSystemName", FacilityDefaultSolarSystem))
                            .RegionID = CLng(GetSettingValue(FacilityFileName, SettingTypes.TypeLong, Tab & POSLargeShipFacilitySettingsFileName, "RegionID", FacilityDefaultRegionID))
                            .RegionName = CStr(GetSettingValue(FacilityFileName, SettingTypes.TypeString, Tab & POSLargeShipFacilitySettingsFileName, "RegionName", FacilityDefaultRegion))
                            .ActivityCostperSecond = CDbl(GetSettingValue(FacilityFileName, SettingTypes.TypeDouble, Tab & POSLargeShipFacilitySettingsFileName, "ActivityCostperSecond", FacilityDefaultActivityCostperSecond))
                            .IncludeActivityUsage = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSLargeShipFacilitySettingsFileName, "IncludeActivityUsage", FacilityDefaultIncludeUsage))
                            .IncludeActivityCost = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSLargeShipFacilitySettingsFileName, "IncludeActivityCost", FacilityDefaultIncludeCost))
                            .IncludeActivityTime = CBool(GetSettingValue(FacilityFileName, SettingTypes.TypeBoolean, Tab & POSLargeShipFacilitySettingsFileName, "IncludeActivityTime", FacilityDefaultIncludeTime))
                    End Select
                End With
            Else
                ' Load defaults 
                TempSettings = SetFacilityDefaultSettings(ProductionType)
            End If
        Catch ex As Exception
            MsgBox("An error occured when loading Facility Tower Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetFacilityDefaultSettings(ProductionType)
        End Try

        ' Save them locally and then export
        Select Case ProductionType
            Case IndustryType.Manufacturing
                ManufacturingFacilitySettings = TempSettings
            Case IndustryType.ComponentManufacturing
                ComponentsManufacturingFacilitySettings = TempSettings
            Case IndustryType.CapitalComponentManufacturing
                CapitalComponentsManufacturingFacilitySettings = TempSettings
            Case IndustryType.CapitalManufacturing
                CapitalManufacturingFacilitySettings = TempSettings
            Case IndustryType.SuperManufacturing
                SuperManufacturingFacilitySettings = TempSettings
            Case IndustryType.BoosterManufacturing
                BoosterManufacturingFacilitySettings = TempSettings
            Case IndustryType.T3CruiserManufacturing
                T3CruiserManufacturingFacilitySettings = TempSettings
            Case IndustryType.T3DestroyerManufacturing
                T3DestroyerManufacturingFacilitySettings = TempSettings
            Case IndustryType.SubsystemManufacturing
                SubsystemManufacturingFacilitySettings = TempSettings
            Case IndustryType.Copying
                CopyFacilitySettings = TempSettings
            Case IndustryType.Invention
                InventionFacilitySettings = TempSettings
            Case IndustryType.T3Invention
                T3InventionFacilitySettings = TempSettings
            Case IndustryType.NoPOSManufacturing
                NoPoSFacilitySettings = TempSettings
            Case IndustryType.POSFuelBlockManufacturing
                POSFuelBlockFacilitySettings = TempSettings
            Case IndustryType.POSLargeShipManufacturing
                POSLargeShipFacilitySettings = TempSettings
            Case IndustryType.POSModuleManufacturing
                POSModuleFacilitySettings = TempSettings
        End Select

        Return TempSettings

    End Function

    ' Saves the Facility Settings to XML
    Public Sub FacilitySaveSettings(SentSettings As FacilitySettings, ProductionType As IndustryType, Tab As String)
        Dim FacilitySettingsList(14) As Setting

        Try
            FacilitySettingsList(0) = New Setting("Facility", CStr(SentSettings.Facility))
            FacilitySettingsList(1) = New Setting("FacilityType", CStr(SentSettings.FacilityType))
            FacilitySettingsList(2) = New Setting("ActivityID", CStr(SentSettings.ActivityID))
            FacilitySettingsList(3) = New Setting("ProductionType", CStr(SentSettings.ProductionType))
            FacilitySettingsList(4) = New Setting("MaterialMultiplier", CStr(SentSettings.MaterialMultiplier))
            FacilitySettingsList(5) = New Setting("TimeMultiplier", CStr(SentSettings.TimeMultiplier))
            FacilitySettingsList(6) = New Setting("TaxRate", CStr(SentSettings.TaxRate))
            FacilitySettingsList(7) = New Setting("ActivityCostperSecond", CStr(SentSettings.ActivityCostperSecond))
            FacilitySettingsList(8) = New Setting("SolarSystemID", CStr(SentSettings.SolarSystemID))
            FacilitySettingsList(9) = New Setting("SolarSystemName", CStr(SentSettings.SolarSystemName))
            FacilitySettingsList(10) = New Setting("RegionID", CStr(SentSettings.RegionID))
            FacilitySettingsList(11) = New Setting("RegionName", CStr(SentSettings.RegionName))
            FacilitySettingsList(12) = New Setting("IncludeActivityUsage", CStr(SentSettings.IncludeActivityUsage))
            FacilitySettingsList(13) = New Setting("IncludeActivityCost", CStr(SentSettings.IncludeActivityCost))
            FacilitySettingsList(14) = New Setting("IncludeActivityTime", CStr(SentSettings.IncludeActivityTime))

            Select Case ProductionType
                Case IndustryType.Manufacturing
                    Call WriteSettingsToFile(Tab & ManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & ManufacturingFacilitySettingsFileName)
                Case IndustryType.ComponentManufacturing
                    Call WriteSettingsToFile(Tab & ComponentsManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & ComponentsManufacturingFacilitySettingsFileName)
                Case IndustryType.CapitalComponentManufacturing
                    Call WriteSettingsToFile(Tab & CapitalComponentsManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & CapitalComponentsManufacturingFacilitySettingsFileName)
                Case IndustryType.SubsystemManufacturing
                    Call WriteSettingsToFile(Tab & SubsystemManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & SubsystemManufacturingFacilitySettingsFileName)
                Case IndustryType.SuperManufacturing
                    Call WriteSettingsToFile(Tab & SuperCapitalManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & SuperCapitalManufacturingFacilitySettingsFileName)
                Case IndustryType.T3CruiserManufacturing
                    Call WriteSettingsToFile(Tab & T3CruiserManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & T3CruiserManufacturingFacilitySettingsFileName)
                Case IndustryType.T3DestroyerManufacturing
                    Call WriteSettingsToFile(Tab & T3DestroyerManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & T3DestroyerManufacturingFacilitySettingsFileName)
                Case IndustryType.BoosterManufacturing
                    Call WriteSettingsToFile(Tab & BoosterManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & BoosterManufacturingFacilitySettingsFileName)
                Case IndustryType.CapitalManufacturing
                    Call WriteSettingsToFile(Tab & CapitalManufacturingFacilitySettingsFileName, FacilitySettingsList, Tab & CapitalManufacturingFacilitySettingsFileName)
                Case IndustryType.Copying
                    Call WriteSettingsToFile(Tab & CopyFacilitySettingsFileName, FacilitySettingsList, Tab & CopyFacilitySettingsFileName)
                Case IndustryType.Invention
                    Call WriteSettingsToFile(Tab & InventionFacilitySettingsFileName, FacilitySettingsList, Tab & InventionFacilitySettingsFileName)
                Case IndustryType.T3Invention
                    Call WriteSettingsToFile(Tab & T3InventionFacilitySettingsFileName, FacilitySettingsList, Tab & T3InventionFacilitySettingsFileName)
                Case IndustryType.NoPOSManufacturing
                    Call WriteSettingsToFile(Tab & NoPoSFacilitySettingsFileName, FacilitySettingsList, Tab & NoPoSFacilitySettingsFileName)
                Case IndustryType.POSFuelBlockManufacturing
                    Call WriteSettingsToFile(Tab & POSFuelBlockFacilitySettingsFileName, FacilitySettingsList, Tab & POSFuelBlockFacilitySettingsFileName)
                Case IndustryType.POSLargeShipManufacturing
                    Call WriteSettingsToFile(Tab & POSLargeShipFacilitySettingsFileName, FacilitySettingsList, Tab & POSLargeShipFacilitySettingsFileName)
                Case IndustryType.POSModuleManufacturing
                    Call WriteSettingsToFile(Tab & POSModuleFacilitySettingsFileName, FacilitySettingsList, Tab & POSModuleFacilitySettingsFileName)
            End Select

        Catch ex As Exception
            MsgBox("An error occured when saving Manufacturing Facility Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the Facility Settings
    Public Function GetFacilitySettings(ProductionType As IndustryType) As FacilitySettings

        Select Case ProductionType
            Case IndustryType.Manufacturing
                Return ManufacturingFacilitySettings
            Case IndustryType.BoosterManufacturing
                Return BoosterManufacturingFacilitySettings
            Case IndustryType.CapitalManufacturing
                Return CapitalManufacturingFacilitySettings
            Case IndustryType.SubsystemManufacturing
                Return SubsystemManufacturingFacilitySettings
            Case IndustryType.SuperManufacturing
                Return SuperManufacturingFacilitySettings
            Case IndustryType.T3CruiserManufacturing
                Return T3CruiserManufacturingFacilitySettings
            Case IndustryType.T3DestroyerManufacturing
                Return T3DestroyerManufacturingFacilitySettings
            Case IndustryType.ComponentManufacturing
                Return ComponentsManufacturingFacilitySettings
            Case IndustryType.CapitalComponentManufacturing
                Return CapitalComponentsManufacturingFacilitySettings
            Case IndustryType.Copying
                Return CopyFacilitySettings
            Case IndustryType.Invention
                Return InventionFacilitySettings
            Case IndustryType.T3Invention
                Return T3InventionFacilitySettings
            Case IndustryType.NoPOSManufacturing
                Return NoPOSFacilitySettings
            Case IndustryType.POSFuelBlockManufacturing
                Return POSFuelBlockFacilitySettings
            Case IndustryType.POSLargeShipManufacturing
                Return POSLargeShipFacilitySettings
            Case IndustryType.POSModuleManufacturing
                Return POSModuleFacilitySettings
        End Select

        Return Nothing

    End Function

    ' Load defaults 
    Public Function SetFacilityDefaultSettings(ProductionType As IndustryType) As FacilitySettings
        Dim TempSettings As FacilitySettings = Nothing

        ' These are all the same regardless
        TempSettings.MaterialMultiplier = FacilityDefaultMM
        TempSettings.TimeMultiplier = FacilityDefaultTM
        TempSettings.TaxRate = FacilityDefaultTax
        TempSettings.ActivityCostperSecond = FacilityDefaultActivityCostperSecond
        TempSettings.IncludeActivityUsage = FacilityDefaultIncludeUsage
        TempSettings.IncludeActivityCost = FacilityDefaultIncludeCost
        TempSettings.IncludeActivityTime = FacilityDefaultIncludeTime

        ' For POS settings, use a null setting for boosters and supers, else use highsec (Jita)
        Select Case ProductionType
            Case IndustryType.SuperManufacturing, IndustryType.BoosterManufacturing
                TempSettings.SolarSystemID = DefaultNullFacilitySolarSystemID
                TempSettings.SolarSystemName = DefaultNullFacilitySolarSystem
                TempSettings.RegionID = DefaultNullFacilityRegionID
                TempSettings.RegionName = DefaultNullFacilityRegion
            Case Else
                TempSettings.SolarSystemID = FacilityDefaultSolarSystemID
                TempSettings.SolarSystemName = FacilityDefaultSolarSystem
                TempSettings.RegionID = FacilityDefaultRegionID
                TempSettings.RegionName = FacilityDefaultRegion
        End Select

        ' Load defaults 
        Select Case ProductionType
            Case IndustryType.Manufacturing
                TempSettings.Facility = DefaultManufacturingFacility
                TempSettings.FacilityType = DefaultManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.Manufacturing
                ManufacturingFacilitySettings = TempSettings
            Case IndustryType.SuperManufacturing
                TempSettings.Facility = DefaultSuperManufacturingFacility
                TempSettings.FacilityType = DefaultSuperManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.SuperManufacturing
                SuperManufacturingFacilitySettings = TempSettings
            Case IndustryType.BoosterManufacturing
                TempSettings.Facility = DefaultBoosterManufacturingFacility
                TempSettings.FacilityType = DefaultBoosterManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.BoosterManufacturing
                BoosterManufacturingFacilitySettings = TempSettings
            Case IndustryType.SubsystemManufacturing
                TempSettings.Facility = DefaultSubsystemManufacturingFacility
                TempSettings.FacilityType = DefaultSubsystemManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.SubsystemManufacturing
                SubsystemManufacturingFacilitySettings = TempSettings
            Case IndustryType.T3CruiserManufacturing
                TempSettings.Facility = DefaultT3CruiserManufacturingFacility
                TempSettings.FacilityType = DefaultT3CruiserManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.T3CruiserManufacturing
                T3CruiserManufacturingFacilitySettings = TempSettings
            Case IndustryType.T3DestroyerManufacturing
                TempSettings.Facility = DefaultT3DestroyerManufacturingFacility
                TempSettings.FacilityType = DefaultT3DestroyerManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.T3DestroyerManufacturing
                T3DestroyerManufacturingFacilitySettings = TempSettings
            Case IndustryType.CapitalManufacturing
                TempSettings.Facility = DefaultCapitalManufacturingFacility
                TempSettings.FacilityType = DefaultCapitalManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.CapitalManufacturing
                CapitalManufacturingFacilitySettings = TempSettings
            Case IndustryType.ComponentManufacturing
                TempSettings.Facility = DefaultComponentManufacturingFacility
                TempSettings.FacilityType = DefaultComponentManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.ComponentManufacturing
                ComponentsManufacturingFacilitySettings = TempSettings
            Case IndustryType.CapitalComponentManufacturing
                TempSettings.Facility = DefaultCapitalComponentManufacturingFacility
                TempSettings.FacilityType = DefaultCapitalComponentManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.CapitalComponentManufacturing
                CapitalComponentsManufacturingFacilitySettings = TempSettings
            Case IndustryType.Copying
                TempSettings.Facility = DefaultCopyFacility
                TempSettings.FacilityType = DefaultCopyFacilityType
                TempSettings.ActivityID = IndustryActivities.Copying
                TempSettings.ProductionType = IndustryType.Copying
                CopyFacilitySettings = TempSettings
            Case IndustryType.Invention
                TempSettings.Facility = DefaultInventionFacility
                TempSettings.FacilityType = DefaultInventionFacilityType
                TempSettings.ActivityID = IndustryActivities.Invention
                TempSettings.ProductionType = IndustryType.Invention
                InventionFacilitySettings = TempSettings
            Case IndustryType.T3Invention
                TempSettings.Facility = DefaultT3InventionFacility
                TempSettings.FacilityType = DefaultT3InventionFacilityType
                TempSettings.ActivityID = IndustryActivities.Invention
                TempSettings.ProductionType = IndustryType.T3Invention
                InventionFacilitySettings = TempSettings
            Case IndustryType.NoPOSManufacturing
                TempSettings.Facility = DefaultManufacturingFacility
                TempSettings.FacilityType = DefaultManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.NoPOSManufacturing
                NoPOSFacilitySettings = TempSettings
            Case IndustryType.POSFuelBlockManufacturing
                TempSettings.Facility = DefaultPOSFuelBlockManufacturingFacility
                TempSettings.FacilityType = DefaultPOSFuelBlockManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.POSFuelBlockManufacturing
                POSFuelBlockFacilitySettings = TempSettings
            Case IndustryType.POSLargeShipManufacturing
                TempSettings.Facility = DefaultPOSLargeShipManufacturingFacility
                TempSettings.FacilityType = DefaultPOSLargeShipManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.POSLargeShipManufacturing
                POSLargeShipFacilitySettings = TempSettings
            Case IndustryType.POSModuleManufacturing
                TempSettings.Facility = DefaultPOSModuleManufacturingFacility
                TempSettings.FacilityType = DefaultPOSModuleManufacturingFacilityType
                TempSettings.ActivityID = IndustryActivities.Manufacturing
                TempSettings.ProductionType = IndustryType.POSModuleManufacturing
                POSModuleFacilitySettings = TempSettings
        End Select

        Return TempSettings

    End Function

    ' Deletes all the facility files for the tab sent
    Public Sub DeleteAllFacilitySettingsFiles(Tab As String)

        File.Delete(SettingsFolder & Tab & ManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & ComponentsManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & CapitalComponentsManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & CapitalManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & SuperCapitalManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & T3CruiserManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & T3DestroyerManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & SubsystemManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & BoosterManufacturingFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & CopyFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & InventionFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & T3InventionFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & NoPoSFacilitySettingsFileName)

        File.Delete(SettingsFolder & Tab & POSFuelBlockFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & POSLargeShipFacilitySettingsFileName)
        File.Delete(SettingsFolder & Tab & POSModuleFacilitySettingsFileName)

    End Sub

#End Region

End Class

' For general program settings
Public Structure ApplicationSettings
    Dim CheckforUpdatesonStart As Boolean
    Dim DataExportFormat As String
    Dim AllowSkillOverride As Boolean
    Dim ShowToolTips As Boolean

    Dim RefiningImplantValue As Double
    Dim ManufacturingImplantValue As Double
    Dim CopyImplantValue As Double

    Dim LoadAssetsonStartup As Boolean
    Dim LoadBPsonStartup As Boolean
    Dim LoadCRESTTeamDataonStartup As Boolean
    Dim LoadCRESTMarketDataonStartup As Boolean
    Dim LoadCRESTFacilityDataonStartup As Boolean
    Dim DisableSound As Boolean

    ' Station Standings for building and selling
    Dim BrokerCorpStanding As Double
    Dim BrokerFactionStanding As Double
    Dim RefineCorpStanding As Double
    Dim RefiningEfficiency As Double ' The default base equipment refining

    Dim RefiningTax As Double ' Tax on refining in stations

    ' ME/TE for BP's we don't own or haven't entered info for
    Dim DefaultBPME As Integer
    Dim DefaultBPTE As Integer

    ' For Build/Buy 
    Dim CheckBuildBuy As Boolean ' Default for setting the check box for build/buy on the blueprints screen
    Dim SuggestBuildBPNotOwned As Boolean ' For Build/Buy suggestions
    Dim SaveBPRelicsDecryptors As Boolean ' For auto-loading relics and decryptor types

    Dim DisableSVR As Boolean ' For disabling SVR updates

    ' For shopping list
    Dim ShopListIncludeInventMats As Boolean
    Dim ShopListIncludeCopyMats As Boolean

    ' The interval for allowing refresh of prices from EVE Central - no less than 1 hour
    Dim EVECentralRefreshInterval As Integer

End Structure

' For BP Tab Settings
Public Structure BPTabSettings
    ' Form stuff
    Dim BlueprintTypeSelection As String
    Dim Tech1Check As Boolean
    Dim Tech2Check As Boolean
    Dim Tech3Check As Boolean
    Dim TechStorylineCheck As Boolean
    Dim TechFactionCheck As Boolean
    Dim TechPirateCheck As Boolean
    Dim IncludeIgnoredBPs As Boolean

    Dim SmallCheck As Boolean
    Dim MediumCheck As Boolean
    Dim LargeCheck As Boolean
    Dim XLCheck As Boolean

    Dim IncludeFees As Boolean
    Dim IncludeUsage As Boolean
    Dim IncludeTaxes As Boolean

    Dim IncludeInventionCost As Boolean
    Dim IncludeInventionTime As Boolean
    Dim IncludeCopyCost As Boolean
    Dim IncludeCopyTime As Boolean
    Dim IncludeT3Cost As Boolean
    Dim IncludeT3Time As Boolean

    Dim PricePerUnit As Boolean

    Dim ProductionLines As Integer
    Dim LaboratoryLines As Integer
    Dim T3Lines As Integer

    Dim T2DecryptorType As String
    Dim RelicType As String
    Dim T3DecryptorType As String

    Dim IgnoreInvention As Boolean
    Dim IgnoreMinerals As Boolean
    Dim IgnoreT1Item As Boolean

End Structure

' For Update Price Settings
Public Structure UpdatePriceTabSettings
    Dim AllRawMats As Boolean
    Dim Minerals As Boolean
    Dim IceProducts As Boolean
    Dim Gas As Boolean
    Dim Misc As Boolean
    Dim AncientRelics As Boolean
    Dim AncientSalvage As Boolean
    Dim Salvage As Boolean
    Dim Planetary As Boolean
    Dim Datacores As Boolean
    Dim Decryptors As Boolean
    Dim RawMats As Boolean
    Dim ProcessedMats As Boolean
    Dim AdvancedMats As Boolean
    Dim MatsandCompounds As Boolean
    Dim DroneComponents As Boolean
    Dim BoosterMats As Boolean
    Dim Polymers As Boolean
    Dim Asteroids As Boolean

    Dim AllManufacturedItems As Boolean
    Dim Ships As Boolean
    Dim Charges As Boolean

    Dim Modules As Boolean
    Dim Drones As Boolean
    Dim Rigs As Boolean

    Dim Deployables As Boolean
    Dim Subsystems As Boolean
    Dim Boosters As Boolean

    Dim Structures As Boolean
    Dim Celestials As Boolean
    Dim StationComponents As Boolean

    Dim Tools As Boolean
    Dim FuelBlocks As Boolean
    Dim Implants As Boolean

    Dim CapT2Components As Boolean
    Dim CapitalComponents As Boolean
    Dim Components As Boolean
    Dim Hybrid As Boolean

    Dim T1 As Boolean
    Dim T2 As Boolean
    Dim T3 As Boolean
    Dim Faction As Boolean
    Dim Pirate As Boolean
    Dim Storyline As Boolean

    Dim SelectedRegions As List(Of String) ' Could have several
    Dim SelectedSystem As String

    Dim PriceImportType As String
    Dim ItemsCombo As String
    Dim RawMatsCombo As String

    Dim UpdatePriceHistory As Boolean

End Structure

' For Manufacturing Tab Settings
Public Structure ManufacturingTabSettings
    Dim BlueprintType As String

    Dim CheckTech1 As Boolean
    Dim CheckTech2 As Boolean
    Dim CheckTech3 As Boolean
    Dim CheckTechStoryline As Boolean
    Dim CheckTechNavy As Boolean
    Dim CheckTechPirate As Boolean

    Dim ItemTypeFilter As String
    Dim TextItemFilter As String

    Dim CheckBPTypeShips As Boolean
    Dim CheckBPTypeDrones As Boolean
    Dim CheckBPTypeComponents As Boolean
    Dim CheckBPTypeStructures As Boolean
    Dim CheckBPTypeMisc As Boolean
    Dim CheckBPTypeModules As Boolean
    Dim CheckBPTypeAmmoCharges As Boolean
    Dim CheckBPTypeRigs As Boolean
    Dim CheckBPTypeSubsystems As Boolean
    Dim CheckBPTypeBoosters As Boolean
    Dim CheckBPTypeDeployables As Boolean
    Dim CheckBPTypeCelestials As Boolean
    Dim CheckBPTypeStationParts As Boolean

    Dim CheckCapitalComponentsFacility As Boolean
    Dim CheckT3DestroyerFacility As Boolean

    Dim CheckAutoCalcNumBPs As Boolean

    Dim AveragePriceDuration As String

    Dim CheckDecryptorNone As Boolean
    Dim CheckDecryptor06 As Boolean
    Dim CheckDecryptor09 As Boolean
    Dim CheckDecryptor10 As Boolean
    Dim CheckDecryptor11 As Boolean
    Dim CheckDecryptor12 As Boolean
    Dim CheckDecryptor15 As Boolean
    Dim CheckDecryptor18 As Boolean
    Dim CheckDecryptor19 As Boolean

    Dim CheckDecryptorUseforT2 As Boolean
    Dim CheckDecryptorUseforT3 As Boolean

    Dim CheckIgnoreInvention As Boolean

    Dim CheckRelicWrecked As Boolean
    Dim CheckRelicIntact As Boolean
    Dim CheckRelicMalfunction As Boolean

    Dim CheckOnlyBuild As Boolean
    Dim CheckOnlyInvent As Boolean
    Dim CheckOnlyRE As Boolean

    Dim CheckIncludeTaxes As Boolean
    Dim CheckIncludeBrokersFees As Boolean
    Dim CheckIncludeUsage As Boolean

    Dim CheckRaceAmarr As Boolean
    Dim CheckRaceCaldari As Boolean
    Dim CheckRaceGallente As Boolean
    Dim CheckRaceMinmatar As Boolean
    Dim CheckRacePirate As Boolean
    Dim CheckRaceOther As Boolean

    Dim SortBy As String

    Dim PriceCompare As String

    Dim CheckIncludeT2Owned As Boolean
    Dim CheckIncludeT3Owned As Boolean

    Dim IgnoreSVRThreshold As Double
    Dim CheckSVRIncludeNull As Boolean

    Dim AveragePriceRegion As String

    Dim ProductionLines As Integer
    Dim LaboratoryLines As Integer
    Dim Runs As Integer
    Dim BPRuns As Integer

    Dim CheckSmall As Boolean
    Dim CheckMedium As Boolean
    Dim CheckLarge As Boolean
    Dim CheckXL As Boolean

    Dim IgnoreInvention As Boolean
    Dim IgnoreMinerals As Boolean
    Dim IgnoreT1Item As Boolean

End Structure

' For Datacore Tab Settings
Public Structure DataCoreTabSettings
    Dim PricesFrom As String

    Dim CheckHighSecAgents As Boolean
    Dim CheckLowNullSecAgents As Boolean
    Dim CheckIncludeAgentsCannotAccess As Boolean

    Dim AgentsInRegion As String

    Dim CheckSovAmarr As Boolean
    Dim CheckSovAmmatar As Boolean
    Dim CheckSovGallente As Boolean
    Dim CheckSovSyndicate As Boolean
    Dim CheckSovKhanid As Boolean
    Dim CheckSovThukker As Boolean
    Dim CheckSovCaldari As Boolean
    Dim CheckSovMinmatar As Boolean

    Dim SkillsChecked() As Integer
    Dim SkillsLevel() As Integer

    Dim CorpsChecked() As Integer
    Dim CorpsStanding() As Double

    Dim Connections As Integer
    Dim Negotiation As Integer
    Dim ResearchProjectMgt As Integer

End Structure

' For Reaction Tab Settings
Public Structure ReactionsTabSettings
    Dim POSFuelCost As Double
    Dim NumberofPOS As Integer

    Dim CheckTaxes As Boolean
    Dim CheckFees As Boolean

    Dim CheckAdvMoonMats As Boolean
    Dim CheckProcessedMoonMats As Boolean
    Dim CheckHybrid As Boolean
    Dim CheckComplexBio As Boolean
    Dim CheckSimpleBio As Boolean

    Dim CheckBuildBasic As Boolean
    Dim CheckIgnoreMarket As Boolean
    Dim CheckRefine As Boolean

End Structure

' For Mining Settings
Public Structure MiningTabSettings
    Dim OreType As String ' Ore or Ice

    Dim CheckHighYieldOres As Boolean
    Dim CheckHighSecOres As Boolean
    Dim CheckLowSecOres As Boolean
    Dim CheckNullSecOres As Boolean

    Dim CheckSovAmarr As Boolean
    Dim CheckSovCaldari As Boolean
    Dim CheckSovGallente As Boolean
    Dim CheckSovMinmatar As Boolean
    Dim CheckSovWormhole As Boolean
    Dim CheckSovC1 As Boolean
    Dim CheckSovC2 As Boolean
    Dim CheckSovC3 As Boolean
    Dim CheckSovC4 As Boolean
    Dim CheckSovC5 As Boolean
    Dim CheckSovC6 As Boolean

    Dim CheckIncludeFees As Boolean
    Dim CheckIncludeTaxes As Boolean

    Dim CheckIncludeJumpFuelCosts As Boolean
    Dim TotalJumpFuelCost As Double
    Dim TotalJumpFuelM3 As Double
    Dim JumpCompressedOre As Boolean
    Dim JumpMinerals As Boolean

    Dim OreMiningShip As String
    Dim IceMiningShip As String
    Dim GasMiningShip As String
    Dim OreStrip As String
    Dim IceStrip As String
    Dim GasHarvester As String
    Dim NumOreMiners As Integer
    Dim NumIceMiners As Integer
    Dim NumGasHarvesters As Integer
    Dim OreUpgrade As String
    Dim IceUpgrade As String
    Dim GasUpgrade As String
    Dim NumOreUpgrades As Integer
    Dim NumIceUpgrades As Integer
    Dim NumGasUpgrades As Integer
    Dim OreImplant As String
    Dim IceImplant As String
    Dim GasImplant As String

    Dim MichiiImplant As Boolean
    Dim T2Crystals As Boolean

    Dim CheckUseHauler As Boolean
    Dim RoundTripMin As Integer
    Dim RoundTripSec As Integer
    Dim Haulerm3 As Double

    Dim CheckUseFleetBooster As Boolean
    Dim BoosterShip As String
    Dim BoosterShipSkill As Integer
    Dim MiningFormanSkill As Integer
    Dim MiningDirectorSkill As Integer
    Dim WarfareLinkSpecSkill As Integer
    Dim CheckMineForemanLaserOpBoost As Integer ' 0,1,2
    Dim CheckMineForemanLaserRangeBoost As Integer '0,1,2
    Dim CheckMiningForemanMindLink As Boolean

    Dim CheckRorqDeployed As Boolean
    Dim IndustrialReconfig As Integer

    Dim MiningDroneM3perHour As Double
    Dim NumberofMiners As Integer

    Dim RefinedOre As Boolean
    Dim UnrefinedOre As Boolean
    Dim CompressedOre As Boolean

    Dim MercoxitMiningRig As Boolean
    Dim IceMiningRig As Boolean

End Structure

' For POS Tower Settings
Public Structure PlayerOwnedStationSettings
    ' Form stuff
    Dim TowerRaceID As Integer
    Dim TowerName As String
    Dim CostperHour As Double
    Dim TowerType As String
    Dim TowerSize As String

    Dim FuelBlockBuild As Boolean

    ' Lab numbers
    Dim NumAdvLabs As Integer
    Dim NumMobileLabs As Integer
    Dim NumHyasyodaLabs As Integer

    ' Lab stuff
    Dim MECostperSecond As Double
    Dim TECostperSecond As Double
    Dim InventionCostperSecond As Double
    Dim CopyCostperSecond As Double

    Dim CharterCost As Double

End Structure

' If we show these columns or not
Public Structure IndustryJobsColumnSettings

    ' These are the column orders and shown/not shown. 0 is not shown, else the order number
    Dim JobState As Integer
    Dim InstallerName As Integer
    Dim TimeToComplete As Integer
    Dim Activity As Integer
    Dim Status As Integer
    Dim StartTime As Integer
    Dim EndTime As Integer
    Dim CompletionTime As Integer
    Dim Blueprint As Integer
    Dim OutputItem As Integer
    Dim OutputItemType As Integer
    Dim InstallSystem As Integer
    Dim InstallRegion As Integer
    Dim LicensedRuns As Integer
    Dim Runs As Integer
    Dim SuccessfulRuns As Integer
    Dim BlueprintLocation As Integer
    Dim OutputLocation As Integer
    Dim JobType As Integer ' Personal or Corp

    Dim JobStateWidth As Integer
    Dim InstallerNameWidth As Integer
    Dim TimeToCompleteWidth As Integer
    Dim ActivityWidth As Integer
    Dim StatusWidth As Integer
    Dim StartTimeWidth As Integer
    Dim EndTimeWidth As Integer
    Dim CompletionTimeWidth As Integer
    Dim BlueprintWidth As Integer
    Dim OutputItemWidth As Integer
    Dim OutputItemTypeWidth As Integer
    Dim InstallSystemWidth As Integer
    Dim InstallRegionWidth As Integer
    Dim LicensedRunsWidth As Integer
    Dim RunsWidth As Integer
    Dim SuccessfulRunsWidth As Integer
    Dim BlueprintLocationWidth As Integer
    Dim OutputLocationWidth As Integer
    Dim JobTypeWidth As Integer ' Personal or Corp

    Dim OrderByColumn As Integer ' What column index the jobs are sorted
    Dim OrderType As String ' Ascending or Descending

    Dim ViewJobType As String ' Personal, Corp, or Both

    Dim JobTimes As String ' Current or History

    ' List of selected characters, comma separated - default is going to be empty but will automatically choose the selected character
    Dim SelectedCharacterIDs As String

End Structure

' If we show these columns or not
Public Structure ManufacturingTabColumnSettings

    ' These are the column orders and shown/not shown. 0 is not shown so it can be used for the order number
    Dim ItemCategory As Integer
    Dim ItemGroup As Integer
    Dim ItemName As Integer
    Dim Owned As Integer
    Dim Tech As Integer
    Dim BPME As Integer
    Dim BPTE As Integer
    Dim Inputs As Integer
    Dim Compared As Integer
    Dim TotalRuns As Integer
    Dim SingleInventedBPCRuns As Integer
    Dim ProductionLines As Integer
    Dim LaboratoryLines As Integer
    Dim TotalInventionCost As Integer
    Dim TotalCopyCost As Integer
    Dim Taxes As Integer
    Dim BrokerFees As Integer
    Dim BPProductionTime As Integer
    Dim TotalProductionTime As Integer
    Dim CopyTime As Integer
    Dim InventionTime As Integer
    Dim ItemMarketPrice As Integer
    Dim Profit As Integer
    Dim ProfitPercentage As Integer
    Dim IskperHour As Integer
    Dim SVR As Integer
    Dim SVRxIPH As Integer
    Dim TotalCost As Integer
    Dim BaseJobCost As Integer
    Dim NumBPs As Integer
    Dim InventionChance As Integer
    Dim BPType As Integer
    Dim Race As Integer
    Dim VolumeperItem As Integer
    Dim TotalVolume As Integer
    Dim ManufacturingJobFee As Integer
    Dim ManufacturingFacilityName As Integer
    Dim ManufacturingFacilitySystem As Integer
    Dim ManufacturingFacilityRegion As Integer
    Dim ManufacturingFacilitySystemIndex As Integer
    Dim ManufacturingFacilityTax As Integer
    Dim ManufacturingFacilityMEBonus As Integer
    Dim ManufacturingFacilityTEBonus As Integer
    Dim ManufacturingFacilityUsage As Integer
    Dim ComponentFacilityName As Integer
    Dim ComponentFacilitySystem As Integer
    Dim ComponentFacilityRegion As Integer
    Dim ComponentFacilitySystemIndex As Integer
    Dim ComponentFacilityTax As Integer
    Dim ComponentFacilityMEBonus As Integer
    Dim ComponentFacilityTEBonus As Integer
    Dim ComponentFacilityUsage As Integer
    Dim CopyingFacilityName As Integer
    Dim CopyingFacilitySystem As Integer
    Dim CopyingFacilityRegion As Integer
    Dim CopyingFacilitySystemIndex As Integer
    Dim CopyingFacilityTax As Integer
    Dim CopyingFacilityMEBonus As Integer
    Dim CopyingFacilityTEBonus As Integer
    Dim CopyingFacilityUsage As Integer
    Dim InventionFacilityName As Integer
    Dim InventionFacilitySystem As Integer
    Dim InventionFacilityRegion As Integer
    Dim InventionFacilitySystemIndex As Integer
    Dim InventionFacilityTax As Integer
    Dim InventionFacilityMEBonus As Integer
    Dim InventionFacilityTEBonus As Integer
    Dim InventionFacilityUsage As Integer

    Dim ItemCategoryWidth As Integer
    Dim ItemGroupWidth As Integer
    Dim ItemNameWidth As Integer
    Dim OwnedWidth As Integer
    Dim TechWidth As Integer
    Dim BPMEWidth As Integer
    Dim BPTEWidth As Integer
    Dim InputsWidth As Integer
    Dim ComparedWidth As Integer
    Dim TotalRunsWidth As Integer
    Dim SingleInventedBPCRunsWidth As Integer
    Dim ProductionLinesWidth As Integer
    Dim LaboratoryLinesWidth As Integer
    Dim TotalInventionCostWidth As Integer
    Dim TotalCopyCostWidth As Integer
    Dim TotalManufacturingCostWidth As Integer
    Dim TaxesWidth As Integer
    Dim BrokerFeesWidth As Integer
    Dim BPProductionTimeWidth As Integer
    Dim TotalProductionTimeWidth As Integer
    Dim CopyTimeWidth As Integer
    Dim InventionTimeWidth As Integer
    Dim ItemMarketPriceWidth As Integer
    Dim ProfitWidth As Integer
    Dim ProfitPercentageWidth As Integer
    Dim IskperHourWidth As Integer
    Dim SVRWidth As Integer
    Dim SVRxIPHWidth As Integer
    Dim TotalCostWidth As Integer
    Dim BaseJobCostWidth As Integer
    Dim NumBPsWidth As Integer
    Dim InventionChanceWidth As Integer
    Dim BPTypeWidth As Integer
    Dim RaceWidth As Integer
    Dim VolumeperItemWidth As Integer
    Dim TotalVolumeWidth As Integer
    Dim ManufacturingJobFeeWidth As Integer
    Dim ManufacturingFacilityNameWidth As Integer
    Dim ManufacturingFacilitySystemWidth As Integer
    Dim ManufacturingFacilityRegionWidth As Integer
    Dim ManufacturingFacilitySystemIndexWidth As Integer
    Dim ManufacturingFacilityTaxWidth As Integer
    Dim ManufacturingFacilityMEBonusWidth As Integer
    Dim ManufacturingFacilityTEBonusWidth As Integer
    Dim ManufacturingFacilityUsageWidth As Integer
    Dim ComponentFacilityNameWidth As Integer
    Dim ComponentFacilitySystemWidth As Integer
    Dim ComponentFacilityRegionWidth As Integer
    Dim ComponentFacilitySystemIndexWidth As Integer
    Dim ComponentFacilityTaxWidth As Integer
    Dim ComponentFacilityMEBonusWidth As Integer
    Dim ComponentFacilityTEBonusWidth As Integer
    Dim ComponentFacilityUsageWidth As Integer
    Dim CopyingFacilityNameWidth As Integer
    Dim CopyingFacilitySystemWidth As Integer
    Dim CopyingFacilityRegionWidth As Integer
    Dim CopyingFacilitySystemIndexWidth As Integer
    Dim CopyingFacilityTaxWidth As Integer
    Dim CopyingFacilityMEBonusWidth As Integer
    Dim CopyingFacilityTEBonusWidth As Integer
    Dim CopyingFacilityUsageWidth As Integer
    Dim InventionFacilityNameWidth As Integer
    Dim InventionFacilitySystemWidth As Integer
    Dim InventionFacilityRegionWidth As Integer
    Dim InventionFacilitySystemIndexWidth As Integer
    Dim InventionFacilityTaxWidth As Integer
    Dim InventionFacilityMEBonusWidth As Integer
    Dim InventionFacilityTEBonusWidth As Integer
    Dim InventionFacilityUsageWidth As Integer

    Dim OrderByColumn As Integer ' What column index the jobs are sorted
    Dim OrderType As String ' Ascending or Descending

End Structure

' For Main Industry Flip Belt Settings
Public Structure IndustryFlipBeltSettings
    Dim CycleTime As Double
    Dim m3perCycle As Double
    Dim NumMiners As Integer
    Dim CompressOre As Boolean
    Dim IPHperMiner As Boolean
    Dim IncludeBrokerFees As Boolean
    Dim IncludeTaxes As Boolean
    Dim TrueSec As String
End Structure

' For the checked ore on each mining tab
Public Structure IndustryBeltOreChecks
    Dim Plagioclase As Boolean
    Dim Spodumain As Boolean
    Dim Kernite As Boolean
    Dim Hedbergite As Boolean
    Dim Arkonor As Boolean
    Dim Bistot As Boolean
    Dim Pyroxeres As Boolean
    Dim Crokite As Boolean
    Dim Jaspet As Boolean
    Dim Omber As Boolean
    Dim Scordite As Boolean
    Dim Gneiss As Boolean
    Dim Veldspar As Boolean
    Dim Hemorphite As Boolean
    Dim DarkOchre As Boolean
    Dim Mercoxit As Boolean
    Dim CrimsonArkonor As Boolean
    Dim PrimeArkonor As Boolean
    Dim TriclinicBistot As Boolean
    Dim MonoclinicBistot As Boolean
    Dim SharpCrokite As Boolean
    Dim CrystallineCrokite As Boolean
    Dim OnyxOchre As Boolean
    Dim ObsidianOchre As Boolean
    Dim VitricHedbergite As Boolean
    Dim GlazedHedbergite As Boolean
    Dim VividHemorphite As Boolean
    Dim RadiantHemorphite As Boolean
    Dim PureJaspet As Boolean
    Dim PristineJaspet As Boolean
    Dim LuminousKernite As Boolean
    Dim FieryKernite As Boolean
    Dim AzurePlagioclase As Boolean
    Dim RichPlagioclase As Boolean
    Dim SolidPyroxeres As Boolean
    Dim ViscousPyroxeres As Boolean
    Dim CondensedScordite As Boolean
    Dim MassiveScordite As Boolean
    Dim BrightSpodumain As Boolean
    Dim GleamingSpodumain As Boolean
    Dim ConcentratedVeldspar As Boolean
    Dim DenseVeldspar As Boolean
    Dim IridescentGneiss As Boolean
    Dim PrismaticGneiss As Boolean
    Dim SilveryOmber As Boolean
    Dim GoldenOmber As Boolean
    Dim MagmaMercoxit As Boolean
    Dim VitreousMercoxit As Boolean
End Structure

' For Assets Selected Item Settings
Public Structure AssetWindowSettings

    ' Main window
    Dim AssetType As String
    Dim SortbyName As Boolean

    ' Selected Items
    Dim ItemFilterText As String
    Dim AllItems As Boolean

    Dim AllRawMats As Boolean
    Dim Minerals As Boolean
    Dim IceProducts As Boolean
    Dim Gas As Boolean
    Dim Misc As Boolean
    Dim BPCs As Boolean
    Dim AncientRelics As Boolean
    Dim AncientSalvage As Boolean
    Dim Salvage As Boolean
    Dim Planetary As Boolean
    Dim Datacores As Boolean
    Dim Decryptors As Boolean
    Dim RawMats As Boolean
    Dim ProcessedMats As Boolean
    Dim AdvancedMats As Boolean
    Dim MatsandCompounds As Boolean
    Dim DroneComponents As Boolean
    Dim BoosterMats As Boolean
    Dim Polymers As Boolean
    Dim Asteroids As Boolean

    Dim AllManufacturedItems As Boolean
    Dim Ships As Boolean
    Dim Modules As Boolean
    Dim Drones As Boolean
    Dim Boosters As Boolean
    Dim Rigs As Boolean
    Dim Charges As Boolean
    Dim Subsystems As Boolean
    Dim Structures As Boolean
    Dim Tools As Boolean
    Dim DataInterfaces As Boolean
    Dim CapT2Components As Boolean
    Dim CapitalComponents As Boolean
    Dim Components As Boolean
    Dim Hybrid As Boolean
    Dim FuelBlocks As Boolean
    Dim StationComponents As Boolean
    Dim Celestials As Boolean
    Dim Deployables As Boolean
    Dim Implants As Boolean

    Dim T1 As Boolean
    Dim T2 As Boolean
    Dim T3 As Boolean
    Dim Faction As Boolean
    Dim Pirate As Boolean
    Dim Storyline As Boolean

End Structure

' For the Shopping List
Public Structure ShoppingListSettings
    Dim DataExportFormat As String
    Dim AlwaysonTop As Boolean
    Dim UpdateAssetsWhenUsed As Boolean
    Dim Fees As Boolean
    Dim CalcBuyBuyOrder As Boolean
    Dim Usage As Boolean
    Dim TotalItemTax As Boolean
    Dim TotalItemBrokerFees As Boolean
End Structure

' For all types of facilities
Public Structure FacilitySettings
    Dim Facility As String ' Will be a station/outpost or a pos module
    Dim FacilityType As String ' Type of facility (station, outpost, or pos)
    Dim ActivityID As Integer
    Dim ProductionType As IndustryType ' What will this facility be used for?
    Dim MaterialMultiplier As Double ' Allows them to set the ME/TE/Tax for the facility (like in Outposts) when I can't get the info
    Dim TimeMultiplier As Double
    Dim TaxRate As Double

    ' For POS, save the location
    Dim SolarSystemID As Long
    Dim SolarSystemName As String
    Dim RegionID As Long
    Dim RegionName As String
    Dim ActivityCostperSecond As Double ' For pos costs
    Dim IncludeActivityCost As Boolean
    Dim IncludeActivityTime As Boolean
    Dim IncludeActivityUsage As Boolean

End Structure

' For saving all 4 possible teams used for jobs
Public Structure TeamSettings
    Dim TeamID As Long
    Dim TeamTab As String ' BP or Calc
End Structure