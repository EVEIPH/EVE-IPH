
Imports System.Xml
Imports System.IO

Public Module SettingsVariables

    ' All settings
    Public AllSettings As New ProgramSettings
    ' User Settings
    Public UserApplicationSettings As ApplicationSettings
    ' BP Tab Settings
    Public UserBPTabSettings As BPTabSettings
    ' Market History viewer
    Public UserMHViewerSettings As MarketHistoryViewerSettings
    ' Manufacturing
    Public UserManufacturingTabSettings As ManufacturingTabSettings
    ' Datacores
    Public UserDCTabSettings As DataCoreTabSettings
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
    Public UserAssetWindowManufacturingTabSettings As AssetWindowSettings
    Public UserAssetWindowShoppingListSettings As AssetWindowSettings
    Public UserAssetWindowRefinerySettings As AssetWindowSettings
    ' For the Blueprint List Viewer
    Public UserBPViewerSettings As BPViewerSettings
    ' For Upwell Structure viewer
    Public UserUpwellStructureSettings As UpwellStructureSettings
    ' For bonus popout on structure viewer
    Public StructureBonusPopoutViewerSettings As StructureBonusPopoutSettings

    Public UserIceBeltFlipSettings As IceBeltFlipSettings
    Public UserIceBeltCheckSettings As IceBeltCheckSettings

    Public UserConversiontoOreSettings As ConversionToOreSettings

End Module

Public Class ProgramSettings

    ' Default Tower Settings
    Public Const DefaultTowerName As String = None
    Public Const DefaultTowerRaceID As Integer = 0
    Public Const DefaultCostperHour As Integer = 0
    Public Const DefaultTowerType As String = "Standard"
    Public Const DefaultTowerSize As String = "Large"
    Public Const DefaultFuelBlockBuild As Boolean = False
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
    Public DefaultRefreshMarketESIDataonStartup As Boolean = True
    Public DefaultRefreshFacilityESIDataonStartup As Boolean = True
    Public DefaultRefreshPublicStructureDataonStartup As Boolean = True
    Public DefaultSupressESIStatusMessages As Boolean = False
    Public DefaultDisableSound As Boolean = False
    Public DefaultDNMarkInlineasOwned As Boolean = False
    Public DefaultSaveFacilitiesbyChar As Boolean = True
    Public DefaultLoadBPsbyChar As Boolean = True
    Public DefaultBuildWhenNotEnoughItemsonMarket As Boolean = False
    Public DefaultManualPriceOverride As Boolean = False

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

    Public DefaultBaseSalesTaxRate As Double = 4.5 ' Sales tax base is 4.5% and during holidays they may change it
    Public DefaultBaseBrokerFeeRate As Double = 3 ' 3%
    Public DefaultSCCBrokerFeeSurcharge As Double = 0.005 ' Fixed rate of 0.5%
    Public DefaultSCCIndustryFeeSurcharge As Double = 0.04 ' Fixed rate of 4% on 2/1/2024
    Public DefaultAlphaAccountTaxRate As Double = 0.0025 ' fixed to 0.25%
    Public DefaultStructureTaxRate As Double = 0.0 ' 0% to start for structures
    Public DefaultStationTaxRate As Double = 0.0025 ' 0.25% for all stations

    Public DefaultIncludeCopyTimes As Boolean = False ' If we include copy times in IPH calcs for invention
    Public DefaultIncludeInventionTimes As Boolean = False ' If we include invention times in IPH calcs for invention
    Public DefaultIncludeRETimes As Boolean = False ' If we include RE times in IPH calcs for RE

    Public DefaultEstimateCopyCost As Boolean = False ' Estimate copy costs for invention BPC's
    Public DefaultCopySlotModifier As String = "1.0" ' The default copy slot modifier for T1 BPC copies to use in invention
    Public DefaultInventionSlotModifier As String = "1.0" ' Default invention time
    Public DefaultBuildSlotModifier As String = "1.0" ' Default build time for production

    Public DefaultCheckBuildBuy As Boolean = False
    Public DefaultIgnoreRareandShipSkinBPs As Boolean = True
    Public DefaultSaveBPRelicsDecryptors As Boolean = False
    Public DefaultRefineDrillDown As Boolean = False

    Public DefaultAlwaysBuyFuelBlocks As Boolean = False
    Public DefaultAlwaysBuyRAMs As Boolean = False
    Public DefaultSaveBPCCostperBP As Boolean = True

    Public DefaultSettingME As Integer = 0
    Public DefaultSettingTE As Integer = 0

    Public DefaultDisableSVR As Boolean = False
    Public DefaultDisableGATracking As Boolean = False
    Public DefaultShareSavedFacilities As Boolean = True
    Public DefaultSuggestBuildBPNotOwned As Boolean = True ' If the bp is not owned, default to suggesting they build the item anyway

    Public DefaultAlphaAccount As Boolean = False
    Public DefaultUseActiveSkills As Boolean = False
    Public DefaultLoadMaxAlphaSkills As Boolean = False

    ' SVR Stuff
    Public DefaultIgnoreSVRThresholdValue As Double = 0.0
    Public DefaultSVRAveragePriceRegion As String = "The Forge"
    Public DefaultSVRAveragePriceDuration As String = "7"
    Public DefaultAutoUpdateSVRonBPTab As Boolean = True

    Public DefaultIncludeInGameLinksinCopyText As Boolean = False

    ' Proxy
    Public DefaultProxyAddress As String = ""
    Public DefaultProxyPort As Integer = 0

    ' For shopping list
    Public DefaultShopListIncludeInventMats As Boolean = True
    Public DefaultShopListIncludeCopyMats As Boolean = True

    ' If the user has no implants
    Public DefaultImplantValues As Double = 0

    Public FacilityDefaultMM As Double = 1
    Public FacilityDefaultTM As Double = 1
    Public FacilityDefaultCM As Double = 1
    Public FacilityDefaultTax As Double = 0.1 ' Only for processing
    Public OutpostDefaultTax As Double = 0 ' If we are saving the settings, then the only time would be for outposts

    Public FacilityDefaultActivityCostperSecond As Double = 0
    Public FacilityDefaultIncludeUsage As Boolean = True
    Public FacilityDefaultIncludeCost As Boolean = False ' Only for Invention, Copy, and RE so let this get set 
    Public FacilityDefaultIncludeTime As Boolean = False ' Only for Invention, Copy, and RE so let this get set 

    ' Set here, but use in Update Prices - 10 minutes
    Public DefaultUpdatePricesRefreshInterval As Integer = 10

    Public DefaultBuiltMatsType As Integer = 1 ' use enum BuildMatType - both BP and Manufacturing tabs

    ' BP Tab Default settings
    Public DefaultBPTechChecks As Boolean = True
    Public DefaultSizeChecks As Boolean = False
    Public DefaultBPSelectionType As String = "All"
    Public DefaultBPIncludeFees As Integer = 0
    Public DefaultBPBrokerFeeRate As Double = 0.05
    Public DefaultBPIncludeTaxes As Boolean = True
    Public DefaultBPIncludeUsage As Boolean = True
    Public DefaultBPIgnoreChecks As Boolean = False
    Public DefaultBPPricePerUnit As Boolean = False
    Public DefaultBPOptimalDecryptor As Integer = 0
    Public DefaultBPIncludeInventionTime As Boolean = False
    Public DefaultBPIncludeInventionCost As Boolean = True
    Public DefaultBPIncludeCopyTime As Boolean = False
    Public DefaultBPIncludecopyCost As Boolean = True
    Public DefaultBPIncludeT3Cost As Boolean = False
    Public DefaultBPIncludeT3Time As Boolean = False
    Public DefaultBPSimpleCopyCheck As Boolean = False
    Public DefaultBPNPCBPOs As Boolean = False
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
    Public DefaultBPSellExcessItems As Boolean = False
    Public DefaultBPShoppingListExportType As String = "Components"
    Public DefaultBPCompColumnSort As Integer = 1
    Public DefaultBPCompColumnSortType As String = "Decending"
    Public DefaultBPRawColumnSort As Integer = 1
    Public DefaultBPRawColumnSortType As String = "Decending"
    Public DefaultBPRawProfitType As String = "Profit"
    Public DefaultBPCompProfitType As String = "Profit"
    Public DefaultBPCompressedOre As Boolean = False

    ' Update Prices Default Settings
    Public DefaultPriceChecks As Boolean = True
    Public DefaultPriceSystem As String = "Jita"
    Public DefaultPriceRegion As String = "The Forge"
    Public DefaultPriceRawMatsCombo As String = "Min Sell"
    Public DefaultPriceItemsCombo As String = "Min Sell"
    Public DefaultUPColumnSort As Integer = 1
    Public DefaultUPColumnSortType As String = "Ascending"
    Public DefaultRawPriceModifier As Double = 0
    Public DefaultItemsPriceModifier As Double = 0
    Public DefaultUseESIData As Integer = 0
    Public DefaultUsePriceProfile As Boolean = False
    Public DefaultPPRawPriceType As String = "Max Buy"
    Public DefaultPPRawRegion As String = "The Forge"
    Public DefaultPPRawSystem As String = "Jita"
    Public DefaultPPRawPriceMod As Double = 0
    Public DefaultPPItemsPriceType As String = "Min Sell"
    Public DefaultPPItemsRegion As String = "The Forge"
    Public DefaultPPItemsSystem As String = "Jita"
    Public DefaultPPItemsPriceMod As Double = 0

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
    Public DefaultCheckBPTypeNPCBPOs As Boolean = False
    Public DefaultCheckBPTypeAmmoCharges As Boolean = True
    Public DefaultCheckBPTypeRigs As Boolean = True
    Public DefaultCheckBPTypeSubsystems As Boolean = True
    Public DefaultCheckBPTypeBoosters As Boolean = True
    Public DefaultCheckBPTypeDeployables As Boolean = True
    Public DefaultCheckBPTypeCelestials As Boolean = True
    Public DefaultCheckBPTypeReactions As Boolean = True
    Public DefaultCheckBPTypeStructureModules As Boolean = True
    Public DefaultCheckBPTypeStationParts As Boolean = True
    Public DefaultCheckDecryptorNone As Boolean = True
    Public DefaultCheckDecryptorOptimal As Integer = 0
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
    Public DefaultCheckIncludeTaxes As Boolean = True
    Public DefaultIncludeBrokersFees As Integer = 0
    Public DefaultCalcBrokerFeeRate As Double = 0.05
    Public DefaultCheckIncludeUsage As Boolean = True
    Public DefaultCheckRaceAmarr As Boolean = True
    Public DefaultCheckRaceCaldari As Boolean = True
    Public DefaultCheckRaceGallente As Boolean = True
    Public DefaultCheckRaceMinmatar As Boolean = True
    Public DefaultCheckRacePirate As Boolean = True
    Public DefaultCheckRaceOther As Boolean = True
    Public DefaultPriceCompare As String = "Compare All"
    Public DefaultCheckIncludeT2Owned As Boolean = True
    Public DefaultCheckIncludeT3Owned As Boolean = True
    Public DefaultCheckSVRIncludeNull As Boolean = True
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
    Public DefaultCalcPPU As Boolean = False
    Public DefaultCalcManufacturingFWLevel As String = "0"
    Public DefaultCalcCopyingFWLevel As String = "0"
    Public DefaultCalcInventionFWLevel As String = "0"
    Public DefaultCalcColumnSort As Integer = 10 ' Default is sorting descending by IPH
    Public DefaultCalcColumnType As String = "Decending"
    Public DefaultCalcPriceTrend As String = "All"
    Public DefaultCalcMinBuildTime As String = "0 Days 00:00:00"
    Public DefaultCalcMinBuildTimeCheck As Boolean = False
    Public DefaultCalcMaxBuildTime As String = "1 Days 00:00:00"
    Public DefaultCalcMaxBuildTimeCheck As Boolean = False
    Public DefaultCalcIPHThreshold As Double = 0
    Public DefaultCalcIPHThresholdCheck As Boolean = False
    Public DefaultCalcProfitThreshold As Double = 0
    Public DefaultCalcProfitThresholdCheck As Integer = 0
    Public DefaultCalcVolumeThreshold As Double = 0
    Public DefaultCalcVolumeThresholdCheck As Boolean = False
    Public DefaultCalcSellExcessItems As Boolean = True

    ' Datacore Default Settings
    Public DefaultDCPricesFrom As String = "Updated Prices"
    Public DefaultDCCheckHighSec As Boolean = True
    Public DefaultDCCheckLowNullSec As Boolean = False
    Public DefaultDCIncludeAgentsCantUse As Boolean = False
    Public DefaultDCAgentsInRegion As String = "All Regions"
    Public DefaultDCSovCheck As Boolean = True
    Public DefaultDCColumnSort As Integer = 10
    Public DefaultDCColumnSortType As String = "Decending"

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
    Public DefaultReactColumnSort As Integer = 5
    Public DefaultReactColumnSortType As String = "Decending"

    ' Mining Default Settings
    Public DefaultMiningOreType As String = "Ore"
    Public DefaultMiningCheckHighYieldOres As Boolean = False
    Public DefaultMiningCheckHighSecOres As Boolean = True
    Public DefaultMiningCheckLowSecOres As Boolean = False
    Public DefaultMiningCheckNullSecOres As Boolean = False
    Public DefaultMiningCheckA0Ores As Boolean = False
    Public DefaultMiningCheckSovAmarr As Boolean = True
    Public DefaultMiningCheckSovCaldari As Boolean = True
    Public DefaultMiningCheckSovGallente As Boolean = True
    Public DefaultMiningCheckSovMinmatar As Boolean = True
    Public DefaultMiningCheckSovTriglavian As Boolean = True
    Public DefaultMiningCheckEDENCOM As Boolean = False
    Public DefaultMiningCheckSovWormhole As Boolean = True
    Public DefaultMiningCheckSovMoon As Boolean = True
    Public DefaultMiningCheckSovC1 As Boolean = True
    Public DefaultMiningCheckSovC2 As Boolean = True
    Public DefaultMiningCheckSovC3 As Boolean = True
    Public DefaultMiningCheckSovC4 As Boolean = True
    Public DefaultMiningCheckSovC5 As Boolean = True
    Public DefaultMiningCheckSovC6 As Boolean = True
    Public DefaultMiningCheckIncludeFees As Boolean = True
    Public DefaultMiningBrokerFeeRate As Double = 0.05
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
    Public DefaultMiningCrystals As Boolean = False
    Public DefaultMiningCrystalType As Boolean = False
    Public DefaultMiningOreImplant As String = None
    Public DefaultMiningIceImplant As String = None
    Public DefaultMiningGasImplant As String = None
    Public DefaultBeancounterImplant As String = None
    Public DefaultMiningRig As String = None
    Public DefaultMiningCheckUseHauler As Boolean = True
    Public DefaultMiningRoundTripMin As Integer = 1
    Public DefaultMiningRoundTripSec As Integer = 0
    Public DefaultMiningHaulerm3 As Integer = 0
    Public DefaultMiningCheckUseFleetBooster As Boolean = False
    Public DefaultMiningOverrideCheck As Boolean = False
    Public DefaultMiningOverrideCycleTime As Double = 1
    Public DefaultMiningOverrideLaserRange As Double = 1
    Public DefaultMiningBoosterShip As String = "Other"
    Public DefaultMiningBoosterShipSkill As Integer = 0
    Public DefaultMiningMiningFormanSkill As Integer = 0
    Public DefaultMiningMiningDirectorSkill As Integer = 0
    Public DefaultMiningWarfareLinkSpecSkill As Integer = 0
    Public DefaultMiningCheckMineForemanLaserOpBoost As Integer = 0
    Public DefaultMiningCheckMiningForemanMindLink As Boolean = False
    Public DefaultMiningRefineCorpTax As Double = 0.05
    Public DefaultMiningRorqDeployed As Integer = 0
    Public DefaultMiningDroneM3perHour As Double = 0.0
    Public DefaultMiningRefinedOre As Boolean = True
    Public DefaultMiningCompressedOre As Boolean = False
    Public DefaultMiningUnrefinedOre As Boolean = False
    Public DefaultMiningIndustrialReconfig As Integer = 0
    Public DefaultMiningNumberofMiners As Integer = 1
    Public DefaultMiningColumnSort As Integer = 9
    Public DefaultMiningColumnSortType As String = "Decending"
    Public DefaultMiningDrone As String = "None"
    Public DefaultNumMiningDrone As String = "0"
    Public DefaultIceMiningDrone As String = "None"
    Public DefaultNumIceMiningdrone As String = "0"
    Public DefaultDroneSkills As String = "-1"
    Public DefaultDroneRigs As String = None
    Public DefaultBoosterDroneRigs As Integer = 0
    Public DefaultBoosterUseDrones As Boolean = False

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
    Public DefaultLocalCompletionDateTime As Integer = 0
    Public DefaultViewJobType As String = "Personal"
    Public DefaultJobTimes As String = "Current Jobs"
    Public DefaultSelectedCharacterIDs As String = ""
    Public DefaultIndustryColumnWidth As Integer = 100
    Public DefaultOrderByColumn As Integer = 3
    Public DefaultOrderType As String = "Ascending"
    Public DefaultAutoUpdateJobs As Boolean = True

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
    Public Const LocalCompletionDateTimeColumn As String = "Local Completion Time"

    ' Manufacturing Tab column settings - index 0 is for hidden id column
    Dim DefaultMTItemCategory As Integer = 3
    Dim DefaultMTItemGroup As Integer = 0
    Dim DefaultMTItemName As Integer = 4
    Dim DefaultMTOwned As Integer = 5
    Dim DefaultMTTech As Integer = 6
    Dim DefaultMTBPME As Integer = 7
    Dim DefaultMTBPTE As Integer = 8
    Dim DefaultMTInputs As Integer = 9
    Dim DefaultMTCompared As Integer = 10
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
    Dim DefaultMTProfit As Integer = 11
    Dim DefaultMTProfitPercentage As Integer = 0
    Dim DefaultMTIskperHour As Integer = 12
    Dim DefaultMTSVR As Integer = 13
    Dim DefaultMTSVRxIPH As Integer = 0
    Dim DefaultMTPriceTrend As Integer = 0
    Dim DefaultMTTotalItemsSold As Integer = 0
    Dim DefaultMTTotalOrdersFilled As Integer = 0
    Dim DefaultMTAvgItemsperOrder As Integer = 0
    Dim DefaultMTCurrentSellOrders As Integer = 0
    Dim DefaultMTCurrentBuyOrders As Integer = 0
    Dim DefaultMTItemsinProduction As Integer = 0
    Dim DefaultMTItemsinStock As Integer = 0
    Dim DefaultMTMaterialCost As Integer = 0
    Dim DefaultMTTotalCost As Integer = 14
    Dim DefaultMTBaseJobCost As Integer = 0
    Dim DefaultMTNumBPs As Integer = 0
    Dim DefaultMTInventionChance As Integer = 0
    Dim DefaultMTBPType As Integer = 0
    Dim DefaultMTRace As Integer = 0
    Dim DefaultMTVolumeperItem As Integer = 0
    Dim DefaultMTTotalVolume As Integer = 0
    Dim DefaultMTSellExcess As Integer = 0
    Dim DefaultMTROI As Integer = 0
    Dim DefaultMTPortionSize As Integer = 0
    Dim DefaultMTManufacturingJobFee As Integer = 0
    Dim DefaultMTManufacturingFacilityName As Integer = 0
    Dim DefaultMTManufacturingFacilitySystem As Integer = 0
    Dim DefaultMTManufacturingFacilityRegion As Integer = 0
    Dim DefaultMTManufacturingFacilitySystemIndex As Integer = 0
    Dim DefaultMTManufacturingFacilityTax As Integer = 0
    Dim DefaultMTManufacturingFacilityMEBonus As Integer = 0
    Dim DefaultMTManufacturingFacilityTEBonus As Integer = 0
    Dim DefaultMTManufacturingFacilityUsage As Integer = 0
    Dim DefaultMTManufacturingFacilityFWSystemLevel As Integer = 0
    Dim DefaultMTComponentFacilityName As Integer = 0
    Dim DefaultMTComponentFacilitySystem As Integer = 0
    Dim DefaultMTComponentFacilityRegion As Integer = 0
    Dim DefaultMTComponentFacilitySystemIndex As Integer = 0
    Dim DefaultMTComponentFacilityTax As Integer = 0
    Dim DefaultMTComponentFacilityMEBonus As Integer = 0
    Dim DefaultMTComponentFacilityTEBonus As Integer = 0
    Dim DefaultMTComponentFacilityUsage As Integer = 0
    Dim DefaultMTComponentFacilityFWSystemLevel As Integer = 0
    Dim DefaultMTCapComponentFacilityName As Integer = 0
    Dim DefaultMTCapComponentFacilitySystem As Integer = 0
    Dim DefaultMTCapComponentFacilityRegion As Integer = 0
    Dim DefaultMTCapComponentFacilitySystemIndex As Integer = 0
    Dim DefaultMTCapComponentFacilityTax As Integer = 0
    Dim DefaultMTCapComponentFacilityMEBonus As Integer = 0
    Dim DefaultMTCapComponentFacilityTEBonus As Integer = 0
    Dim DefaultMTCapComponentFacilityUsage As Integer = 0
    Dim DefaultMTCapComponentFacilityFWSystemLevel As Integer = 0
    Dim DefaultMTCopyingFacilityName As Integer = 0
    Dim DefaultMTCopyingFacilitySystem As Integer = 0
    Dim DefaultMTCopyingFacilityRegion As Integer = 0
    Dim DefaultMTCopyingFacilitySystemIndex As Integer = 0
    Dim DefaultMTCopyingFacilityTax As Integer = 0
    Dim DefaultMTCopyingFacilityMEBonus As Integer = 0
    Dim DefaultMTCopyingFacilityTEBonus As Integer = 0
    Dim DefaultMTCopyingFacilityUsage As Integer = 0
    Dim DefaultMTCopyingFacilityFWSystemLevel As Integer = 0
    Dim DefaultMTInventionFacilityName As Integer = 0
    Dim DefaultMTInventionFacilitySystem As Integer = 0
    Dim DefaultMTInventionFacilityRegion As Integer = 0
    Dim DefaultMTInventionFacilitySystemIndex As Integer = 0
    Dim DefaultMTInventionFacilityTax As Integer = 0
    Dim DefaultMTInventionFacilityMEBonus As Integer = 0
    Dim DefaultMTInventionFacilityTEBonus As Integer = 0
    Dim DefaultMTInventionFacilityUsage As Integer = 0
    Dim DefaultMTInventionFacilityFWSystemLevel As Integer = 0
    Dim DefaultMTReactionFacilityName As Integer = 0
    Dim DefaultMTReactionFacilitySystem As Integer = 0
    Dim DefaultMTReactionFacilityRegion As Integer = 0
    Dim DefaultMTReactionFacilitySystemIndex As Integer = 0
    Dim DefaultMTReactionFacilityTax As Integer = 0
    Dim DefaultMTReactionFacilityMEBonus As Integer = 0
    Dim DefaultMTReactionFacilityTEBonus As Integer = 0
    Dim DefaultMTReactionFacilityUsage As Integer = 0
    Dim DefaultMTReactionFacilityFWSystemLevel As Integer = 0
    Dim DefaultMTReprocessingFacilityName As Integer = 0
    Dim DefaultMTReprocessingFacilitySystem As Integer = 0
    Dim DefaultMTReprocessingFacilityRegion As Integer = 0
    Dim DefaultMTReprocessingFacilityTax As Integer = 0
    Dim DefaultMTReprocessingFacilityUsage As Integer = 0
    Dim DefaultMTReprocessingFacilityOreRefineRate As Integer = 0
    Dim DefaultMTReprocessingFacilityIceRefineRate As Integer = 0
    Dim DefaultMTReprocessingFacilityMoonRefineRate As Integer = 0

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
    Dim DefaultMTPriceTrendWidth As Integer = 100
    Dim DefaultMTTotalItemsSoldWidth As Integer = 100
    Dim DefaultMTTotalOrdersFilledWidth As Integer = 100
    Dim DefaultMTAvgItemsperOrderWidth As Integer = 100
    Dim DefaultMTCurrentSellOrdersWidth As Integer = 100
    Dim DefaultMTCurrentBuyOrdersWidth As Integer = 100
    Dim DefaultMTItemsinProductionWidth As Integer = 100
    Dim DefaultMTItemsinStockWidth As Integer = 100
    Dim DefaultMTMaterialCostWidth As Integer = 100
    Dim DefaultMTTotalCostWidth As Integer = 100
    Dim DefaultMTBaseJobCostWidth As Integer = 100
    Dim DefaultMTNumBPsWidth As Integer = 57
    Dim DefaultMTInventionChanceWidth As Integer = 100
    Dim DefaultMTBPTypeWidth As Integer = 54
    Dim DefaultMTRaceWidth As Integer = 77
    Dim DefaultMTVolumeperItemWidth As Integer = 89
    Dim DefaultMTTotalVolumeWidth As Integer = 75
    Dim DefaultMTSellExcessWidth As Integer = 100
    Dim DefaultMTROIWidth As Integer = 100
    Dim DefaultMTPortionSizeWidth As Integer = 75
    Dim DefaultMTManufacturingJobFeeWidth As Integer = 122
    Dim DefaultMTManufacturingFacilityNameWidth As Integer = 150
    Dim DefaultMTManufacturingFacilitySystemWidth As Integer = 152
    Dim DefaultMTManufacturingFacilityRegionWidth As Integer = 154
    Dim DefaultMTManufacturingFacilitySystemIndexWidth As Integer = 184
    Dim DefaultMTManufacturingFacilityTaxWidth As Integer = 138
    Dim DefaultMTManufacturingFacilityMEBonusWidth As Integer = 169
    Dim DefaultMTManufacturingFacilityTEBonusWidth As Integer = 166
    Dim DefaultMTManufacturingFacilityUsageWidth As Integer = 149
    Dim DefaultMTManufacturingFacilityFWSystemLevelWidth As Integer = 150
    Dim DefaultMTComponentFacilityNameWidth As Integer = 145
    Dim DefaultMTComponentFacilitySystemWidth As Integer = 140
    Dim DefaultMTComponentFacilityRegionWidth As Integer = 138
    Dim DefaultMTComponentFacilitySystemIndexWidth As Integer = 168
    Dim DefaultMTComponentFacilityTaxWidth As Integer = 122
    Dim DefaultMTComponentFacilityMEBonusWidth As Integer = 153
    Dim DefaultMTComponentFacilityTEBonusWidth As Integer = 153
    Dim DefaultMTComponentFacilityUsageWidth As Integer = 136
    Dim DefaultMTComponentFacilityFWSystemLevelWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityNameWidth As Integer = 150
    Dim DefaultMTCapComponentFacilitySystemWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityRegionWidth As Integer = 150
    Dim DefaultMTCapComponentFacilitySystemIndexWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityTaxWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityMEBonusWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityTEBonusWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityUsageWidth As Integer = 150
    Dim DefaultMTCapComponentFacilityFWSystemLevelWidth As Integer = 150
    Dim DefaultMTCopyingFacilityNameWidth As Integer = 116
    Dim DefaultMTCopyingFacilitySystemWidth As Integer = 122
    Dim DefaultMTCopyingFacilityRegionWidth As Integer = 122
    Dim DefaultMTCopyingFacilitySystemIndexWidth As Integer = 153
    Dim DefaultMTCopyingFacilityTaxWidth As Integer = 107
    Dim DefaultMTCopyingFacilityMEBonusWidth As Integer = 137
    Dim DefaultMTCopyingFacilityTEBonusWidth As Integer = 135
    Dim DefaultMTCopyingFacilityUsageWidth As Integer = 121
    Dim DefaultMTCopyingFacilityFWSystemLevelWidth As Integer = 150
    Dim DefaultMTInventionFacilityNameWidth As Integer = 122
    Dim DefaultMTInventionFacilitySystemWidth As Integer = 130
    Dim DefaultMTInventionFacilityRegionWidth As Integer = 129
    Dim DefaultMTInventionFacilitySystemIndexWidth As Integer = 156
    Dim DefaultMTInventionFacilityTaxWidth As Integer = 112
    Dim DefaultMTInventionFacilityMEBonusWidth As Integer = 144
    Dim DefaultMTInventionFacilityTEBonusWidth As Integer = 141
    Dim DefaultMTInventionFacilityUsageWidth As Integer = 127
    Dim DefaultMTInventionFacilityFWSystemLevelWidth As Integer = 150
    Dim DefaultMTReactionFacilityNameWidth As Integer = 150
    Dim DefaultMTReactionFacilitySystemWidth As Integer = 150
    Dim DefaultMTReactionFacilityRegionWidth As Integer = 150
    Dim DefaultMTReactionFacilitySystemIndexWidth As Integer = 150
    Dim DefaultMTReactionFacilityTaxWidth As Integer = 150
    Dim DefaultMTReactionFacilityMEBonusWidth As Integer = 150
    Dim DefaultMTReactionFacilityTEBonusWidth As Integer = 150
    Dim DefaultMTReactionFacilityUsageWidth As Integer = 150
    Dim DefaultMTReactionFacilityFWSystemLevelWidth As Integer = 122
    Dim DefaultMTReprocessingFacilityNameWidth As Integer = 122
    Dim DefaultMTReprocessingFacilitySystemWidth As Integer = 130
    Dim DefaultMTReprocessingFacilityRegionWidth As Integer = 129
    Dim DefaultMTReprocessingFacilityTaxWidth As Integer = 112
    Dim DefaultMTReprocessingFacilityUsageWidth As Integer = 127
    Dim DefaultMTReprocessingFacilityOreRefineRateWidth As Integer = 144
    Dim DefaultMTReprocessingFacilityIceRefineRateWidth As Integer = 144
    Dim DefaultMTReprocessingFacilityMoonRefineRateWidth As Integer = 144

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
    Public Const SVRxIPHColumnName As String = "SVR * IPH"
    Public Const PriceTrendColumnName As String = "Price Trend"
    Public Const TotalItemsSoldColumnName As String = "Total Items Sold"
    Public Const TotalOrdersFilledColumnName As String = "Total Orders Filled"
    Public Const AvgItemsperOrderColumnName As String = "Average Items Per Order"
    Public Const CurrentSellOrdersColumnName As String = "Current Sell Orders"
    Public Const CurrentBuyOrdersColumnName As String = "Current Buy Orders"
    Public Const ItemsinProductionColumnName As String = "Items in Production"
    Public Const ItemsinStockColumnName As String = "Items in Stock"
    Public Const MaterialCostColumnName As String = "Material Cost"
    Public Const TotalCostColumnName As String = "Total Cost"
    Public Const BaseJobCostColumnName As String = "Base Job Cost"
    Public Const NumBPsColumnName As String = "Num BPs"
    Public Const InventionChanceColumnName As String = "Invention Chance"
    Public Const BPTypeColumnName As String = "BP Type"
    Public Const RaceColumnName As String = "Race"
    Public Const VolumeperItemColumnName As String = "Volume per Item"
    Public Const TotalVolumeColumnName As String = "Total Volume"
    Public Const SellExcessColumnName As String = "Sell Excess Amount"
    Public Const ROIColumnName As String = "Return on Investment"
    Public Const PortionSizeColumnName As String = "Portion Size"
    Public Const ManufacturingJobFeeColumnName As String = "Manufacturing Job Fee"
    Public Const ManufacturingFacilityNameColumnName As String = "Manufacturing Facility Name"
    Public Const ManufacturingFacilitySystemColumnName As String = "Manufacturing Facility System"
    Public Const ManufacturingFacilityRegionColumnName As String = "Manufacturing Facility Region"
    Public Const ManufacturingFacilitySystemIndexColumnName As String = "Manufacturing Facility System Index"
    Public Const ManufacturingFacilityTaxColumnName As String = "Manufacturing Facility Tax"
    Public Const ManufacturingFacilityMEBonusColumnName As String = "Manufacturing Facility ME Bonus"
    Public Const ManufacturingFacilityTEBonusColumnName As String = "Manufacturing Facility TE Bonus"
    Public Const ManufacturingFacilityUsageColumnName As String = "Manufacturing Facility Usage"
    Public Const ManufacturingFacilityFWSystemLevelColumnName As String = "Manufacturing Facility FW System Level"
    Public Const ComponentFacilityNameColumnName As String = "Component Facility Name"
    Public Const ComponentFacilitySystemColumnName As String = "Component Facility System"
    Public Const ComponentFacilityRegionColumnName As String = "Component Facility Region"
    Public Const ComponentFacilitySystemIndexColumnName As String = "Component Facility System Index"
    Public Const ComponentFacilityTaxColumnName As String = "Component Facility Tax"
    Public Const ComponentFacilityMEBonusColumnName As String = "Component Facility ME Bonus"
    Public Const ComponentFacilityTEBonusColumnName As String = "Component Facility TE Bonus"
    Public Const ComponentFacilityUsageColumnName As String = "Component Facility Usage"
    Public Const ComponentFacilityFWSystemLevelColumnName As String = "Component Facility FW System Level"
    Public Const CapComponentFacilityNameColumnName As String = "Capital Component Facility Name"
    Public Const CapComponentFacilitySystemColumnName As String = "Capital Component Facility System"
    Public Const CapComponentFacilityRegionColumnName As String = "Capital Component Facility Region"
    Public Const CapComponentFacilitySystemIndexColumnName As String = "Capital Component Facility SystemIndex"
    Public Const CapComponentFacilityTaxColumnName As String = "Capital Component Facility Tax"
    Public Const CapComponentFacilityMEBonusColumnName As String = "Capital Component Facility ME Bonus"
    Public Const CapComponentFacilityTEBonusColumnName As String = "Capital Component Facility TE Bonus"
    Public Const CapComponentFacilityUsageColumnName As String = "Capital Component Facility Usage"
    Public Const CapComponentFacilityFWSystemLevelColumnName As String = "Capital Component Facility FW System Level"
    Public Const CopyingFacilityNameColumnName As String = "Copying Facility Name"
    Public Const CopyingFacilitySystemColumnName As String = "Copying Facility System"
    Public Const CopyingFacilityRegionColumnName As String = "Copying Facility Region"
    Public Const CopyingFacilitySystemIndexColumnName As String = "Copying Facility System Index"
    Public Const CopyingFacilityTaxColumnName As String = "Copying Facility Tax"
    Public Const CopyingFacilityMEBonusColumnName As String = "Copying Facility ME Bonus"
    Public Const CopyingFacilityTEBonusColumnName As String = "Copying Facility TE Bonus"
    Public Const CopyingFacilityUsageColumnName As String = "Copying Facility Usage"
    Public Const CopyingFacilityFWSystemLevelColumnName As String = "Copying Facility FW System Level"
    Public Const InventionFacilityNameColumnName As String = "Invention Facility Name"
    Public Const InventionFacilitySystemColumnName As String = "Invention Facility System"
    Public Const InventionFacilityRegionColumnName As String = "Invention Facility Region"
    Public Const InventionFacilitySystemIndexColumnName As String = "Invention Facility SystemIndex"
    Public Const InventionFacilityTaxColumnName As String = "Invention Facility Tax"
    Public Const InventionFacilityMEBonusColumnName As String = "Invention Facility ME Bonus"
    Public Const InventionFacilityTEBonusColumnName As String = "Invention Facility TE Bonus"
    Public Const InventionFacilityUsageColumnName As String = "Invention Facility Usage"
    Public Const InventionFacilityFWSystemLevelColumnName As String = "Invention Facility FW System Level"
    Public Const ReactionFacilityNameColumnName As String = "Reaction Facility Name"
    Public Const ReactionFacilitySystemColumnName As String = "Reaction Facility System"
    Public Const ReactionFacilityRegionColumnName As String = "Reaction Facility Region"
    Public Const ReactionFacilitySystemIndexColumnName As String = "Reaction Facility SystemIndex"
    Public Const ReactionFacilityTaxColumnName As String = "Reaction Facility Tax"
    Public Const ReactionFacilityMEBonusColumnName As String = "Reaction Facility ME Bonus"
    Public Const ReactionFacilityTEBonusColumnName As String = "Reaction Facility TE Bonus"
    Public Const ReactionFacilityUsageColumnName As String = "Reaction Facility Usage"
    Public Const ReactionFacilityFWSystemLevelColumnName As String = "Reaction Facility FW System Level"
    Public Const ReprocessingFacilityNameColumnName As String = "Reprocessing Facility Name"
    Public Const ReprocessingFacilitySystemColumnName As String = "Reprocessing Facility System"
    Public Const ReprocessingFacilityRegionColumnName As String = "Reprocessing Facility Region"
    Public Const ReprocessingFacilityTaxColumnName As String = "Reprocessing Facility Tax"
    Public Const ReprocessingFacilityUsageColumnName As String = "Reprocessing Facility Usage"
    Public Const ReprocessingFacilityOreRefineRateColumnName As String = "Reprocessing Facility Ore Efficiency Rate"
    Public Const ReprocessingFacilityIceRefineRateColumnName As String = "Reprocessing Facility Ice Efficiency Rate"
    Public Const ReprocessingFacilityMoonRefineRateColumnName As String = "Reprocessing Facility Moon Efficiency Rate"

    ' Industry Ore/Ice Flip Belt settings
    Private DefaultCycleTime As Double = 180
    Private Defaultm3perCycle As Double = 3000
    Private DefaultNumMiners As Integer = 1
    Private DefaultCompressOre As Boolean = False
    Private DefaultIPHperMiner As Boolean = False
    Private DefaultIncludeBrokerFees As Integer '0,1,2 - Tri-check
    Private DefaultBFBrokerFeeRate As Double = 0.05
    Private DefaultIncludeTaxes As Boolean = True
    Private DefaultTruesec As String = ""
    Private DefaultSpace As String = ""

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

    ' Ice Belt Flip - checks
    Private DefaultBlueIce As Boolean = True
    Private DefaultClearIcicle As Boolean = True
    Private DefaultDarkGlitter As Boolean = True
    Private DefaultEnrichedClearIcicle As Boolean = True
    Private DefaultGelidus As Boolean = True
    Private DefaultGlacialMass As Boolean = True
    Private DefaultGlareCrust As Boolean = True
    Private DefaultKrystallos As Boolean = True
    Private DefaultPristineWhiteGlaze As Boolean = True
    Private DefaultSmoothGlacialMass As Boolean = True
    Private DefaultThickBlueIce As Boolean = True
    Private DefaultWhiteGlaze As Boolean = True
    Private DefaultCompressedBlueIce As Boolean = True
    Private DefaultCompressedClearIcicle As Boolean = True
    Private DefaultCompressedDarkGlitter As Boolean = True
    Private DefaultCompressedEnrichedClearIcicle As Boolean = True
    Private DefaultCompressedGelidus As Boolean = True
    Private DefaultCompressedGlacialMass As Boolean = True
    Private DefaultCompressedGlareCrust As Boolean = True
    Private DefaultCompressedKrystallos As Boolean = True
    Private DefaultCompressedPristineWhiteGlaze As Boolean = True
    Private DefaultCompressedSmoothGlacialMass As Boolean = True
    Private DefaultCompressedThickBlueIce As Boolean = True
    Private DefaultCompressedWhiteGlaze As Boolean = True

    ' ConvertToOre
    Private DefaultConversionType As String = None
    Private DefaultMinimizeOn As String = "Refine Price"
    Private DefaultCompressedOre As Boolean = True
    Private DefaultCompressedIce As Boolean = True
    Private DefaultHighSec As Boolean = True
    Private DefaultLowSec As Boolean = True
    Private DefaultNullSec As Boolean = True
    Private DefaultOreVariant0 As Boolean = True
    Private DefaultOreVariant5 As Boolean = True
    Private DefaultOreVariant10 As Boolean = True
    Private DefaultAmarr As Boolean = True
    Private DefaultCaldari As Boolean = True
    Private DefaultGallente As Boolean = True
    Private DefaultMinmatar As Boolean = True
    Private DefaultWormhole As Boolean = False
    Private DefaultTriglavian As Boolean = False
    Private DefaultC1 As Boolean = True
    Private DefaultC2 As Boolean = True
    Private DefaultC3 As Boolean = True
    Private DefaultC4 As Boolean = True
    Private DefaultC5 As Boolean = True
    Private DefaultC6 As Boolean = True
    Public DefaultOverrideValue As Integer = 1 ' 1 is not overridden, 0 is false, -1 true for override value
    Public DefaultIgnoreValue As Integer = 0

    ' Default Shopping List Settings
    Private DefaultAlwaysonTop As Boolean = False
    Private DefaultUpdateAssetsWhenUsed As Boolean = False
    Private DefaultFees As Boolean = True
    Private DefaultCalcBuyBuyOrder As Integer = 1
    Private DefaultUsage As Boolean = True
    Private DefaultReloadBPsFromFile As Boolean = True

    ' Default Market History Viewer Settings
    Private DefaultMHDatePreference As String = "By Days"
    Private DefaultMHVolume As Boolean = False
    Private DefaultMHMinMaxDayPrice As Boolean = False
    Private DefaultMHLinearTrend As Boolean = False
    Private DefaultMHDochianChannel As Boolean = False
    Private DefaultMHFiveDayAvg As Boolean = False
    Private DefaultMHTwentyDayAvg As Boolean = False

    ' Assets - Item Checks
    Private DefaultAssetItemChecks As Boolean = True
    Private DefaultAssetItemTextFilter As String = ""
    Private DefaultAllItems As Boolean = True
    ' Assets - Main window 
    Private DefaultAssetType As String = "Both"
    Private DefaultAssetSortbyName As Boolean = True
    ' Account type
    Private DefaultSelectedAccount As Boolean = True
    Private DefaultCharacterList As String = ""

    ' Default LP Store
    Private DefaultLPRewardType As String = "All"
    Private DefaultLPCorpFilter As String = "Use Standings"
    Private DefaultLPCheckAgentLevel1 As Boolean = True
    Private DefaultLPCheckAgentLevel2 As Boolean = True
    Private DefaultLPCheckAgentLevel3 As Boolean = True
    Private DefaultLPCheckAgentLevel4 As Boolean = True
    Private DefaultLPCheckAgentLevel5 As Boolean = True
    Private DefaultLPTextItemSearch As String = ""
    Private DefaultLPTextReqItemSearch As String = ""
    Private DefaultLPLPCostLessThan As String = "0.00"
    Private DefaultLPLPCostGreaterThan As String = "0.00"
    Private DefaultLPISKCostLessThan As String = "0.00"
    Private DefaultLPISKCostGreaterThan As String = "0.00"
    Private DefaultLPStandingLessThan As String = "0.00"
    Private DefaultLPStandingGreaterThan As String = "0.00"
    Private DefaultLPSearchOption As String = "All Corporations"
    Private DefaultLPSortByOption As String = "ISK/LP"
    Private DefaultLPHighlightCheck As Boolean = True
    Private DefaultLPSelectedCorporations As String = ""

    ' Upwell Structures Viewer
    Private DefaultHighSlotsCheck As Boolean = False
    Private DefaultMediumSlotsCheck As Boolean = False
    Private DefaultLowSlotsCheck As Boolean = False
    Private DefaultServicesCheck As Boolean = False
    Private DefaultReprocessingRigsCheck As Boolean = False
    Private DefaultEngineeringRigsCheck As Boolean = False
    Private DefaultCombatRigsCheck As Boolean = False
    Private DefaultIncludeFuelCostsCheck As Boolean = False
    Private DefaultFuelBlockType As String = "Helium Fuel Block"
    Private DefaultBuyBuildBlockOption As String = "Buy Blocks"
    Private DefaultAutoUpdateFuelBlockPricesCheck As Boolean = False
    Private DefaultSearchFilterText As String = ""
    Private DefaultSelectedStructureName As String = "Raitaru"
    Private DefaultReactionsRigsCheck As Boolean = False
    Private DefaultDrillingRigsCheck As Boolean = False
    Private DefaultIconListView As Boolean = True

    ' Bonus Popout Viewer Settings for facilities
    Private DefaultSBPVFormWidth As Integer = 750
    Private DefaultSBPVFormHeight As Integer = 275
    Private DefaultSBPVBonusAppliesColumnWidth As Integer = 150
    Private DefaultSBPVActivityColumnWidth As Integer = 125
    Private DefaultSBPVBonusesColumnWidth As Integer = 250
    Private DefaultSBPVBonusSourceColumnWidth As Integer = 165

    ' Local versions of settings
    Private ApplicationSettings As ApplicationSettings
    Private BPSettings As BPTabSettings
    Private ManufacturingSettings As ManufacturingTabSettings
    Private DatacoreSettings As DataCoreTabSettings
    Private MiningSettings As MiningTabSettings
    Private UpdatePricesSettings As UpdatePriceTabSettings
    Private IndustryJobsColumnSettings As IndustryJobsColumnSettings
    Private ManufacturingTabColumnSettings As ManufacturingTabColumnSettings
    Private IndustryFlipBeltsSettings As IndustryFlipBeltSettings
    Private ShoppingListTabSettings As ShoppingListSettings
    Private MarketHistoryViewSettings As MarketHistoryViewerSettings
    Private UpwellStructureViewerSettings As UpwellStructureSettings
    Private BPViewSettings As BPViewerSettings
    Private IceBeltFlipSetting As IceBeltFlipSettings
    Private IceBeltCheckSetting As IceBeltCheckSettings
    Private ConversionToOreSetting As ConversionToOreSettings

    ' Multiple versions of Asset windows
    Private AssetWindowSettingsManufacturingTab As AssetWindowSettings
    Private AssetWindowSettingsShoppingList As AssetWindowSettings
    Private AssetWindowsettingsReprocessing As AssetWindowSettings

    ' 5 belt types
    Private IndustryBeltOreChecksSettings1 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings2 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings3 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings4 As IndustryBeltOreChecks
    Private IndustryBeltOreChecksSettings5 As IndustryBeltOreChecks

    Private Const AppSettingsFileName As String = "ApplicationSettings"
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
    Private Const BPViewerSettingsFileName As String = "BPViewerSettings"

    Private Const LPStoreSettingsFileName As String = "LPStoreSettings"
    Private Const MarketHistoryViewerSettingsFileName As String = "MarketHistoryViewerSettings"
    Private Const UpwellStructureViewerSettingsFileName As String = "UpwellStructureViewerSettings"
    Private Const StructureBonusPopoutViewerSettingsFileName As String = "StructureBonusPopoutViewerSettings"

    Private Const IceBeltFlipSettingsFileName As String = "IceBeltFlipSettings"
    Private Const IceBeltFlipCheckSettingsFileName As String = "IceBeltFlipCheckSettings"

    Private Const ConvertToOreSettingsFileName As String = "ConvertToOreSettings"

    ' For BP List Viewer
    Public DefaultBPViewerTechChecks As Boolean = True
    Public DefaultBPViewerSizeChecks As Boolean = False
    Public DefaultBPViewerIgnoreBPsCheck As Boolean = False
    Public DefaultBPNPCBPOsCheck As Boolean = False
    Public DefaultBPViewerSelectionType As String = "All"

    ' 5 belts
    Private IndustryBeltOreChecksBaseFileName As String = "IndustryBeltOreChecks"
    Private IndustryBeltOreChecksFileName As String = ""
    Private Const IndustryBeltOreChecksFileName1 As String = "1"
    Private Const IndustryBeltOreChecksFileName2 As String = "2"
    Private Const IndustryBeltOreChecksFileName3 As String = "3"
    Private Const IndustryBeltOreChecksFileName4 As String = "4"
    Private Const IndustryBeltOreChecksFileName5 As String = "5"

    ' Multiple asset windows
    Private Const AssetWindowFileNameDefault As String = "AssetWindowSettingsDefault"
    Private Const AssetWindowFileNameManufacturingTab As String = "AssetWindowSettingsManufacturingTab"
    Private Const AssetWindowFileNameShoppingList As String = "AssetWindowSettingsShoppingList"
    Private Const AssetWindowFileNameRefinery As String = "AssetWindowFileNameRefinery"

    Private Const XMLfileType As String = ".xml"

    Public Sub New()
        ApplicationSettings = Nothing
        MiningSettings = Nothing
        BPSettings = Nothing
        ManufacturingSettings = Nothing
        DatacoreSettings = Nothing
        MiningSettings = Nothing
        UpdatePricesSettings = Nothing
        IndustryJobsColumnSettings = Nothing
        ManufacturingTabColumnSettings = Nothing
        IndustryFlipBeltsSettings = Nothing
        IceBeltFlipSetting = Nothing
        IceBeltCheckSetting = Nothing
        ShoppingListTabSettings = Nothing
        MarketHistoryViewSettings = Nothing
        UpwellStructureViewerSettings = Nothing
        BPViewSettings = Nothing
        ConversionToOreSetting = Nothing

        ReDim ConversionToOreSetting.OverrideChecks(35)

    End Sub

    ' Writes the sent settings to the sent file name
    Private Sub WriteSettingsToFile(FileFolder As String, FileName As String, Settings As Setting(), RootName As String)
        Dim i As Integer

        ' Create XmlWriterSettings.
        Dim XMLSettings As XmlWriterSettings = New XmlWriterSettings()
        Dim FilePath As String = Path.Combine(DynamicFilePath, FileFolder)
        XMLSettings.Indent = True

        If FileFolder <> "" Then
            If Not Directory.Exists(FilePath) Then
                ' Create the settings folder
                Directory.CreateDirectory(FilePath)
            End If
        End If

        ' Delete and make a fresh copy
        If File.Exists(Path.ChangeExtension(Path.Combine(FilePath, FileName), XMLfileType)) Then
            File.Delete(Path.ChangeExtension(Path.Combine(FilePath, FileName), XMLfileType))
        End If

        ' Loop through the settings sent and output each name and value
        Using writer As XmlWriter = XmlWriter.Create(Path.ChangeExtension(Path.Combine(FilePath, FileName), XMLfileType), XMLSettings)
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
    Private Function GetSettingValue(FileFolder As String, ByRef FileName As String, ObjectType As SettingTypes,
                                     RootElement As String, ElementString As String,
                                     DefaultValue As Object) As Object
        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim FilePath As String = Path.ChangeExtension(Path.Combine(DynamicFilePath, FileFolder, FileName), XMLfileType)
        Dim TempValue As String

        Try


            'Load the Xml file
            m_xmld.Load(FilePath)

            'Get the settings
            m_nodelist = m_xmld.SelectNodes("/" & RootElement & "/" & ElementString)

            If Not IsNothing(m_nodelist.Item(0)) Then
                ' Should only be one
                TempValue = m_nodelist.Item(0).InnerText

                ' If blank, then return default
                If TempValue = "" Then
                    Return DefaultValue
                End If

                If TempValue = "False" Or TempValue = "True" Then
                    ' Change to type boolean
                    ObjectType = SettingTypes.TypeBoolean
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

        Catch ex As Exception
            ' Threw an error, so return the default value
            Return DefaultValue
        End Try

        Return Nothing

    End Function

    ' Just checks if the file exists or not so we don't have to mess with file names
    Private Function FileExists(FileFolder As String, FileName As String) As Boolean

        If File.Exists(Path.ChangeExtension(Path.Combine(DynamicFilePath, FileFolder, FileName), XMLfileType)) Then
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
            If FileExists(SettingsFolder, AppSettingsFileName) Then

                'Get the settings
                With TempSettings
                    .CheckforUpdatesonStart = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "CheckforUpdatesonStart", DefaultCheckUpdatesOnStart))
                    .LoadAssetsonStartup = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadAssetsonStartup", DefaultLoadAssetsonStartup))
                    .LoadBPsonStartup = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadbpsonStartup", DefaultLoadBPsonStartup))
                    .LoadESIMarketDataonStartup = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadESIMarketDataonStartup", DefaultRefreshMarketESIDataonStartup))
                    .LoadESISystemCostIndiciesDataonStartup = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadESISystemCostIndiciesDataonStartup", DefaultRefreshFacilityESIDataonStartup))
                    .LoadESIPublicStructuresonStartup = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadESISystemCostIndiciesDataonStartup", DefaultRefreshPublicStructureDataonStartup))
                    .SupressESIStatusMessages = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SupressESIStatusMessages", DefaultSupressESIStatusMessages))
                    .DataExportFormat = CStr(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeString, AppSettingsFileName, "DataExportFormat", DefaultDataExportFormat))
                    .AllowSkillOverride = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "AllowSkillOverride", DefaultAllowSkillOverride))
                    .ShowToolTips = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShowToolTips", DefaultShowToolTips))
                    .RefiningImplantValue = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "RefiningImplantValue", DefaultImplantValues))
                    .ManufacturingImplantValue = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "ManufacturingImplantValue", DefaultImplantValues))
                    .CopyImplantValue = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "CopyImplantValue", DefaultImplantValues))
                    .BrokerCorpStanding = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "BrokerCorpStanding", DefaultBrokerCorpStanding))
                    .IncludeInGameLinksinCopyText = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "IncludeInGameLinksinCopyText", DefaultIncludeInGameLinksinCopyText))
                    .BrokerFactionStanding = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "BrokerFactionStanding", DefaultBrokerFactionStanding))
                    .DefaultBPME = CInt(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "DefaultBPME", DefaultSettingME))
                    .DefaultBPTE = CInt(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "DefaultBPTE", DefaultSettingTE))
                    .CheckBuildBuy = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "CheckBuildBuy", DefaultCheckBuildBuy))
                    .DisableSVR = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "DisableSVR", DefaultDisableSVR))
                    .DisableGATracking = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "DisableGATracking", DefaultDisableGATracking))
                    .ShopListIncludeInventMats = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShopListIncludeInventMats", DefaultShopListIncludeInventMats))
                    .ShopListIncludeCopyMats = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShopListIncludeCopyMats", DefaultShopListIncludeCopyMats))
                    .SuggestBuildBPNotOwned = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SuggestBuildBPNotOwned", DefaultSuggestBuildBPNotOwned))
                    .UpdatePricesRefreshInterval = CInt(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "UpdatePricesRefreshInterval", DefaultUpdatePricesRefreshInterval))
                    .DisableSound = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "DisableSound", DefaultDisableSound))
                    .SaveBPRelicsDecryptors = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SaveBPRelicsDecryptors", DefaultSaveBPRelicsDecryptors))
                    .AlwaysBuyFuelBlocks = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "AlwaysBuyFuelBlocks", DefaultAlwaysBuyFuelBlocks))
                    .AlwaysBuyRAMs = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "AlwaysBuyRAMs", DefaultAlwaysBuyRAMs))
                    .SaveBPCCostperBP = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SaveBPCCostperBP", DefaultSaveBPCCostperBP))
                    .IgnoreSVRThresholdValue = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "IgnoreSVRThresholdValue", DefaultIgnoreSVRThresholdValue))
                    .SVRAveragePriceRegion = CStr(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeString, AppSettingsFileName, "SVRAveragePriceRegion", DefaultSVRAveragePriceRegion))
                    .SVRAveragePriceDuration = CStr(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeString, AppSettingsFileName, "SVRAveragePriceDuration", DefaultSVRAveragePriceDuration))
                    .AutoUpdateSVRonBPTab = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "AutoUpdateSVRonBPTab", DefaultAutoUpdateSVRonBPTab))
                    .ProxyAddress = CStr(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeString, AppSettingsFileName, "ProxyAddress", DefaultProxyAddress))
                    .ProxyPort = CInt(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeInteger, AppSettingsFileName, "ProxyPort", DefaultProxyPort))
                    .SaveFacilitiesbyChar = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "SaveFacilitiesbyChar", DefaultSaveFacilitiesbyChar))
                    .LoadBPsbyChar = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadBPsbyChar", DefaultLoadBPsbyChar))
                    .AlphaAccount = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "AlphaAccount", DefaultAlphaAccount))
                    .UseActiveSkillLevels = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "UseActiveSkillLevels", DefaultUseActiveSkills))
                    .LoadMaxAlphaSkills = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "LoadMaxAlphaSkills", DefaultLoadMaxAlphaSkills))
                    .ShareSavedFacilities = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "ShareSavedFacilities", DefaultDisableGATracking))
                    .RefineDrillDown = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "RefineDrillDown", DefaultRefineDrillDown))
                    .BaseSalesTaxRate = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "BaseSalesTaxRate", DefaultBaseSalesTaxRate))
                    .BaseBrokerFeeRate = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "BaseBrokerFeeRate", DefaultBaseBrokerFeeRate))
                    .SCCBrokerFeeSurcharge = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "SCCBrokerFeeSurcharge", DefaultSCCBrokerFeeSurcharge))
                    .SCCIndustryFeeSurcharge = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "SCCIndustryFeeSurcharge", DefaultSCCIndustryFeeSurcharge))
                    .AlphaAccountTaxRate = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "AlphaAccountTaxRate", DefaultAlphaAccountTaxRate))
                    .StructureTaxRate = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "StructureTaxRate", DefaultStructureTaxRate))
                    .StationTaxRate = CDbl(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeDouble, AppSettingsFileName, "StationTaxRate", DefaultStationTaxRate))
                    .BuildWhenNotEnoughItemsonMarket = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "BuildWhenNotEnoughItemsonMarket", DefaultBuildWhenNotEnoughItemsonMarket))
                    .ManualPriceOverride = CBool(GetSettingValue(SettingsFolder, AppSettingsFileName, SettingTypes.TypeBoolean, AppSettingsFileName, "BuildWhenNotEnoughItemsonMarket", DefaultManualPriceOverride))
                End With

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

        With TempSettings
            ' Load default settings
            .CheckforUpdatesonStart = DefaultCheckUpdatesOnStart
            .DataExportFormat = DefaultDataExportFormat
            .ShowToolTips = DefaultShowToolTips
            .LoadAssetsonStartup = DefaultLoadAssetsonStartup
            .LoadBPsonStartup = DefaultLoadBPsonStartup
            .LoadESIMarketDataonStartup = DefaultRefreshMarketESIDataonStartup
            .SupressESIStatusMessages = DefaultSupressESIStatusMessages
            .LoadESISystemCostIndiciesDataonStartup = DefaultRefreshFacilityESIDataonStartup
            .LoadESIPublicStructuresonStartup = DefaultRefreshPublicStructureDataonStartup
            .DisableSound = DefaultDisableSound
            .ManufacturingImplantValue = DefaultImplantValues
            .RefiningImplantValue = DefaultImplantValues
            .CopyImplantValue = DefaultImplantValues

            ' Station Standings for building and selling
            .BrokerCorpStanding = DefaultBrokerCorpStanding
            .BrokerFactionStanding = DefaultBrokerFactionStanding

            .CheckBuildBuy = DefaultCheckBuildBuy

            .DefaultBPME = DefaultSettingME
            .DefaultBPTE = DefaultSettingTE

            .AlphaAccount = DefaultAlphaAccount
            .UseActiveSkillLevels = DefaultUseActiveSkills
            .LoadMaxAlphaSkills = DefaultLoadMaxAlphaSkills

            .DisableSVR = DefaultDisableSVR
            .DisableGATracking = DefaultDisableGATracking
            .ShareSavedFacilities = DefaultShareSavedFacilities
            .SuggestBuildBPNotOwned = DefaultSuggestBuildBPNotOwned
            .SaveBPRelicsDecryptors = DefaultSaveBPRelicsDecryptors

            .AlwaysBuyFuelBlocks = DefaultAlwaysBuyFuelBlocks
            .AlwaysBuyRAMs = DefaultAlwaysBuyRAMs
            .SaveBPCCostperBP = DefaultSaveBPCCostperBP

            .ShopListIncludeInventMats = DefaultShopListIncludeInventMats
            .ShopListIncludeCopyMats = DefaultShopListIncludeCopyMats

            .UpdatePricesRefreshInterval = DefaultUpdatePricesRefreshInterval

            .IgnoreSVRThresholdValue = DefaultIgnoreSVRThresholdValue
            .SVRAveragePriceRegion = DefaultSVRAveragePriceRegion
            .SVRAveragePriceDuration = DefaultSVRAveragePriceDuration
            .AutoUpdateSVRonBPTab = DefaultAutoUpdateSVRonBPTab

            .ProxyAddress = DefaultProxyAddress
            .ProxyPort = DefaultProxyPort

            .LoadBPsbyChar = DefaultLoadBPsbyChar
            .SaveFacilitiesbyChar = DefaultSaveFacilitiesbyChar

            .RefineDrillDown = DefaultRefineDrillDown

            .BaseSalesTaxRate = DefaultBaseSalesTaxRate
            .BaseBrokerFeeRate = DefaultBaseBrokerFeeRate
            .SCCBrokerFeeSurcharge = DefaultSCCBrokerFeeSurcharge
            .SCCIndustryFeeSurcharge = DefaultSCCIndustryFeeSurcharge
            .AlphaAccountTaxRate = DefaultAlphaAccountTaxRate
            .StructureTaxRate = DefaultStructureTaxRate
            .StationTaxRate = DefaultStationTaxRate

            .BuildWhenNotEnoughItemsonMarket = DefaultBuildWhenNotEnoughItemsonMarket
            .ManualPriceOverride = DefaultManualPriceOverride
        End With

        ' Save locally
        ApplicationSettings = TempSettings
        Return TempSettings

    End Function

    ' Saves the application settings to XML
    Public Sub SaveApplicationSettings(SentSettings As ApplicationSettings)
        Dim ApplicationSettingsList(50) As Setting

        Try
            ApplicationSettingsList(0) = New Setting("CheckforUpdatesonStart", CStr(SentSettings.CheckforUpdatesonStart))
            ApplicationSettingsList(1) = New Setting("DataExportFormat", CStr(SentSettings.DataExportFormat))
            ApplicationSettingsList(2) = New Setting("AllowSkillOverride", CStr(SentSettings.AllowSkillOverride))
            ApplicationSettingsList(3) = New Setting("ShowToolTips", CStr(SentSettings.ShowToolTips))
            ApplicationSettingsList(4) = New Setting("RefiningImplantValue", CStr(SentSettings.RefiningImplantValue))
            ApplicationSettingsList(5) = New Setting("ManufacturingImplantValue", CStr(SentSettings.ManufacturingImplantValue))
            ApplicationSettingsList(6) = New Setting("CopyImplantValue", CStr(SentSettings.CopyImplantValue))
            ApplicationSettingsList(7) = New Setting("BrokerCorpStanding", CStr(SentSettings.BrokerCorpStanding))
            ApplicationSettingsList(8) = New Setting("BrokerFactionStanding", CStr(SentSettings.BrokerFactionStanding))
            ApplicationSettingsList(9) = New Setting("DefaultBPME", CStr(SentSettings.DefaultBPME))
            ApplicationSettingsList(10) = New Setting("DefaultBPTE", CStr(SentSettings.DefaultBPTE))
            ApplicationSettingsList(11) = New Setting("CheckBuildBuy", CStr(SentSettings.CheckBuildBuy))
            ApplicationSettingsList(12) = New Setting("IncludeInGameLinksinCopyText", CStr(SentSettings.IncludeInGameLinksinCopyText))
            ApplicationSettingsList(13) = New Setting("ShopListIncludeInventMats", CStr(SentSettings.ShopListIncludeInventMats))
            ApplicationSettingsList(14) = New Setting("ShopListIncludeCopyMats", CStr(SentSettings.ShopListIncludeCopyMats))
            ApplicationSettingsList(15) = New Setting("SuggestBuildBPNotOwned", CStr(SentSettings.SuggestBuildBPNotOwned))
            ApplicationSettingsList(16) = New Setting("UpdatePricesRefreshInterval", CStr(SentSettings.UpdatePricesRefreshInterval))
            ApplicationSettingsList(17) = New Setting("LoadAssetsonStartup", CStr(SentSettings.LoadAssetsonStartup))
            ApplicationSettingsList(18) = New Setting("DisableSound", CStr(SentSettings.DisableSound))
            ApplicationSettingsList(19) = New Setting("LoadbpsonStartup", CStr(SentSettings.LoadBPsonStartup))
            ApplicationSettingsList(20) = New Setting("LoadESISystemCostIndiciesDataonStartup", CStr(SentSettings.LoadESISystemCostIndiciesDataonStartup))
            ApplicationSettingsList(21) = New Setting("LoadESIMarketDataonStartup", CStr(SentSettings.LoadESIMarketDataonStartup))
            ApplicationSettingsList(22) = New Setting("SaveBPRelicsDecryptors", CStr(SentSettings.SaveBPRelicsDecryptors))
            ApplicationSettingsList(23) = New Setting("IgnoreSVRThresholdValue", CStr(SentSettings.IgnoreSVRThresholdValue))
            ApplicationSettingsList(24) = New Setting("SVRAveragePriceRegion", CStr(SentSettings.SVRAveragePriceRegion))
            ApplicationSettingsList(25) = New Setting("SVRAveragePriceDuration", CStr(SentSettings.SVRAveragePriceDuration))
            ApplicationSettingsList(26) = New Setting("AutoUpdateSVRonBPTab", CStr(SentSettings.AutoUpdateSVRonBPTab))
            ApplicationSettingsList(27) = New Setting("ProxyAddress", CStr(SentSettings.ProxyAddress))
            ApplicationSettingsList(28) = New Setting("ProxyPort", CStr(SentSettings.ProxyPort))
            ApplicationSettingsList(29) = New Setting("SaveFacilitiesbyChar", CStr(SentSettings.SaveFacilitiesbyChar))
            ApplicationSettingsList(30) = New Setting("LoadBPsbyChar", CStr(SentSettings.LoadBPsbyChar))
            ApplicationSettingsList(31) = New Setting("LoadESIPublicStructuresonStartup", CStr(SentSettings.LoadESIPublicStructuresonStartup))
            ApplicationSettingsList(32) = New Setting("DisableGATracking", CStr(SentSettings.DisableGATracking))
            ApplicationSettingsList(33) = New Setting("AlphaAccount", CStr(SentSettings.AlphaAccount))
            ApplicationSettingsList(34) = New Setting("UseActiveSkillLevels", CStr(SentSettings.UseActiveSkillLevels))
            ApplicationSettingsList(35) = New Setting("SupressESIStatusMessages", CStr(SentSettings.SupressESIStatusMessages))
            ApplicationSettingsList(36) = New Setting("LoadMaxAlphaSkills", CStr(SentSettings.LoadMaxAlphaSkills))
            ApplicationSettingsList(37) = New Setting("ShareSavedFacilities", CStr(SentSettings.ShareSavedFacilities))
            ApplicationSettingsList(38) = New Setting("RefineDrillDown", CStr(SentSettings.RefineDrillDown))
            ApplicationSettingsList(39) = New Setting("AlwaysBuyFuelBlocks", CStr(SentSettings.AlwaysBuyFuelBlocks))
            ApplicationSettingsList(40) = New Setting("AlwaysBuyRAMs", CStr(SentSettings.AlwaysBuyRAMs))
            ApplicationSettingsList(41) = New Setting("BaseSalesTaxRate", CStr(SentSettings.BaseSalesTaxRate))
            ApplicationSettingsList(42) = New Setting("BaseBrokerFeeRate", CStr(SentSettings.BaseBrokerFeeRate))
            ApplicationSettingsList(43) = New Setting("SCCBrokerFeeSurcharge", CStr(SentSettings.SCCBrokerFeeSurcharge))
            ApplicationSettingsList(44) = New Setting("SCCIndustryFeeSurcharge", CStr(SentSettings.SCCIndustryFeeSurcharge))
            ApplicationSettingsList(45) = New Setting("AlphaAccountTaxRate", CStr(SentSettings.AlphaAccountTaxRate))
            ApplicationSettingsList(46) = New Setting("StationTaxRate", CStr(SentSettings.StationTaxRate))
            ApplicationSettingsList(47) = New Setting("StructureTaxRate", CStr(SentSettings.StructureTaxRate))
            ApplicationSettingsList(48) = New Setting("BuildWhenNotEnoughItemsonMarket", CStr(SentSettings.BuildWhenNotEnoughItemsonMarket))
            ApplicationSettingsList(49) = New Setting("ManualPriceOverride", CStr(SentSettings.ManualPriceOverride))
            ApplicationSettingsList(50) = New Setting("SaveBPCCostperBP", CStr(SentSettings.SaveBPCCostperBP))

            Call WriteSettingsToFile(SettingsFolder, AppSettingsFileName, ApplicationSettingsList, AppSettingsFileName)

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

    ' Loads the shnopping list settings from XML setting file
    Public Function LoadShoppingListSettings() As ShoppingListSettings
        Dim TempSettings As ShoppingListSettings = Nothing

        Try
            If FileExists(SettingsFolder, ShoppingListSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .DataExportFormat = CStr(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeString, ShoppingListSettingsFileName, "DataExportFormat", DefaultDataExportFormat))
                    .AlwaysonTop = CBool(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "AlwaysonTop", DefaultAlwaysonTop))
                    .UpdateAssetsWhenUsed = CBool(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "UpdateAssetsWhenUsed", DefaultUpdateAssetsWhenUsed))
                    .Fees = CBool(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "Fees", DefaultFees))
                    .CalcBuyBuyOrder = CInt(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeInteger, ShoppingListSettingsFileName, "CalcBuyBuyOrder", DefaultCalcBuyBuyOrder))
                    .Usage = CBool(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "Usage", DefaultUsage))
                    .ReloadBPsFromFile = CBool(GetSettingValue(SettingsFolder, ShoppingListSettingsFileName, SettingTypes.TypeBoolean, ShoppingListSettingsFileName, "ReloadBPsFromFile", DefaultReloadBPsFromFile))
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
        TempSettings.ReloadBPsFromFile = DefaultReloadBPsFromFile

        ShoppingListTabSettings = TempSettings

        Return TempSettings

    End Function

    ' Saves the Shopping List Settings to XML
    Public Sub SaveShoppingListSettings(SentSettings As ShoppingListSettings)
        Dim ShoppingListSettingsList(6) As Setting

        Try
            ShoppingListSettingsList(0) = New Setting("DataExportFormat", CStr(SentSettings.DataExportFormat))
            ShoppingListSettingsList(1) = New Setting("AlwaysonTop", CStr(SentSettings.AlwaysonTop))
            ShoppingListSettingsList(2) = New Setting("UpdateAssetsWhenUsed", CStr(SentSettings.UpdateAssetsWhenUsed))
            ShoppingListSettingsList(3) = New Setting("Fees", CStr(SentSettings.Fees))
            ShoppingListSettingsList(4) = New Setting("CalcBuyBuyOrder", CStr(SentSettings.CalcBuyBuyOrder))
            ShoppingListSettingsList(5) = New Setting("Usage", CStr(SentSettings.Usage))
            ShoppingListSettingsList(6) = New Setting("ReloadBPsFromFile", CStr(SentSettings.ReloadBPsFromFile))

            Call WriteSettingsToFile(SettingsFolder, ShoppingListSettingsFileName, ShoppingListSettingsList, ShoppingListSettingsFileName)

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
            If FileExists(SettingsFolder, BPSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .BlueprintTypeSelection = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "BlueprintTypeSelection", DefaultBPSelectionType))
                    .Tech1Check = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "Tech1Check", DefaultBPTechChecks))
                    .Tech2Check = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "Tech2Check", DefaultBPTechChecks))
                    .Tech3Check = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "Tech3Check", DefaultBPTechChecks))
                    .TechStorylineCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "TechStorylineCheck", DefaultBPTechChecks))
                    .TechFactionCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "TechFactionCheck", DefaultBPTechChecks))
                    .TechPirateCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "TechPirateCheck", DefaultBPTechChecks))
                    .IncludeUsage = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeUsage", DefaultBPIncludeUsage))
                    .IncludeTaxes = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeTaxes", DefaultBPIncludeTaxes))
                    .PricePerUnit = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "PricePerUnit", DefaultBPPricePerUnit))
                    .IncludeInventionCost = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeInventionCost", DefaultBPIncludeInventionCost))
                    .IncludeInventionTime = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeInventionTime", DefaultBPIncludeInventionTime))
                    .IncludeCopyCost = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeCopyCost", DefaultBPIncludecopyCost))
                    .IncludeCopyTime = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeCopyTime", DefaultBPIncludeCopyTime))
                    .IncludeT3Cost = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeT3Cost", DefaultBPIncludeT3Cost))
                    .IncludeT3Time = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeT3Time", DefaultBPIncludeT3Time))
                    .ProductionLines = CInt(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "ProductionLines", DefaultBPProductionLines))
                    .LaboratoryLines = CInt(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "LaboratoryLines", DefaultBPLaboratoryLines))
                    .T3Lines = CInt(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "RELines", DefaultBPRELines))
                    .SmallCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SmallCheck", DefaultSizeChecks))
                    .MediumCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "MediumCheck", DefaultSizeChecks))
                    .LargeCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "LargeCheck", DefaultSizeChecks))
                    .XLCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "XLCheck", DefaultSizeChecks))
                    .IncludeFees = CInt(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "IncludeFees", DefaultBPIncludeFees))
                    .BrokerFeeRate = CDbl(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeDouble, BPSettingsFileName, "BrokerFeeRate", DefaultBPBrokerFeeRate))
                    .RelicType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "RelicType", DefaultBPRelicType))
                    .T2DecryptorType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "T2DecryptorType", DefaultBPT2DecryptorType))
                    .T3DecryptorType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "T3DecryptorType", DefaultBPT3DecryptorType))
                    .IgnoreInvention = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IgnoreInvention", DefaultBPIgnoreInvention))
                    .IgnoreMinerals = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IgnoreMinerals", DefaultBPIgnoreMinerals))
                    .IgnoreT1Item = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IgnoreT1Item", DefaultBPIgnoreT1Item))
                    .IncludeIgnoredBPs = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "IncludeIgnoredBPs", DefaultBPIncludeIgnoredBPs))
                    .ExporttoShoppingListType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "ExporttoShoppingListType", DefaultBPShoppingListExportType))
                    .RawColumnSort = CInt(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "RawColumnSort", DefaultBPRawColumnSort))
                    .RawColumnSortType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "RawColumnSortType", DefaultBPRawColumnSortType))
                    .CompColumnSort = CInt(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeInteger, BPSettingsFileName, "CompColumnSort", DefaultBPCompColumnSort))
                    .CompColumnSortType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "CompColumnSortType", DefaultBPCompColumnSortType))
                    .RawProfitType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "RawProfitType", DefaultBPRawProfitType))
                    .CompProfitType = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "CompProfitType", DefaultBPCompProfitType))
                    .CompressedOre = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "CompressedOre", DefaultBPCompressedOre))
                    .SimpleCopyCheck = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SimpleCopyCheck", DefaultBPSimpleCopyCheck))
                    .NPCBPOs = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "NPCBPOs", DefaultBPNPCBPOs))
                    .SellExcessBuildItems = CBool(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeBoolean, BPSettingsFileName, "SellExcessBuildItems", DefaultBPSellExcessItems))
                    .BuildT2T3Materials = CType(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "BuildT2T3Materials", DefaultBuiltMatsType), BuildMatType)
                    .HistoryRegion = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "HistoryRegion", DefaultSVRAveragePriceRegion))
                    .HistoryAvgDays = CStr(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeString, BPSettingsFileName, "HistoryAvgDays", DefaultSVRAveragePriceDuration))
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
        Dim BPSettingsList(47) As Setting

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
            BPSettingsList(31) = New Setting("ExporttoShoppingListType", CStr(SentSettings.ExporttoShoppingListType))

            BPSettingsList(32) = New Setting("RawColumnSort", CStr(SentSettings.RawColumnSort))
            BPSettingsList(33) = New Setting("RawColumnSortType", CStr(SentSettings.RawColumnSortType))
            BPSettingsList(34) = New Setting("CompColumnSort", CStr(SentSettings.CompColumnSort))
            BPSettingsList(35) = New Setting("CompColumnSortType", CStr(SentSettings.CompColumnSortType))

            BPSettingsList(36) = New Setting("RawProfitType", CStr(SentSettings.RawProfitType))
            BPSettingsList(37) = New Setting("CompProfitType", CStr(SentSettings.CompProfitType))
            BPSettingsList(38) = New Setting("CompressedOre", CStr(SentSettings.CompressedOre))

            BPSettingsList(39) = New Setting("SimpleCopyCheck", CStr(SentSettings.SimpleCopyCheck))

            BPSettingsList(40) = New Setting("NPCBPOs", CStr(SentSettings.NPCBPOs))
            BPSettingsList(41) = New Setting("SellExcessBuildItems", CStr(SentSettings.SellExcessBuildItems))
            BPSettingsList(42) = New Setting("BrokerFeeRate", CStr(SentSettings.BrokerFeeRate))

            BPSettingsList(43) = New Setting("BuildT2T3Materials", CStr(SentSettings.BuildT2T3Materials))

            BPSettingsList(44) = New Setting("OptimalT2Decryptor", CStr(SentSettings.OptimalT2Decryptor))
            BPSettingsList(45) = New Setting("OptimalT3Decryptor", CStr(SentSettings.OptimalT3Decryptor))

            BPSettingsList(46) = New Setting("HistoryRegion", CStr(SentSettings.HistoryRegion))
            BPSettingsList(47) = New Setting("HistoryAvgDays", CStr(SentSettings.HistoryAvgDays))

            Call WriteSettingsToFile(SettingsFolder, BPSettingsFileName, BPSettingsList, BPSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving BP Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Loads the defaults
    Public Function SetDefaultBPSettings() As BPTabSettings
        Dim LocalSettings As BPTabSettings

        With LocalSettings
            .BlueprintTypeSelection = DefaultBPSelectionType
            .Tech1Check = DefaultBPTechChecks
            .Tech2Check = DefaultBPTechChecks
            .Tech3Check = DefaultBPTechChecks
            .TechStorylineCheck = DefaultBPTechChecks
            .TechFactionCheck = DefaultBPTechChecks
            .TechPirateCheck = DefaultBPTechChecks
            .IncludeUsage = DefaultBPIncludeUsage
            .IncludeTaxes = DefaultBPIncludeTaxes
            .IncludeFees = DefaultIncludeBrokerFees
            .BrokerFeeRate = DefaultBPBrokerFeeRate
            .PricePerUnit = DefaultBPPricePerUnit
            .ProductionLines = DefaultBPProductionLines
            .LaboratoryLines = DefaultBPLaboratoryLines
            .T3Lines = DefaultBPRELines
            .SmallCheck = DefaultSizeChecks
            .MediumCheck = DefaultSizeChecks
            .LargeCheck = DefaultSizeChecks
            .XLCheck = DefaultSizeChecks
            .SimpleCopyCheck = DefaultBPSimpleCopyCheck
            .NPCBPOs = DefaultBPNPCBPOs
            .SellExcessBuildItems = DefaultBPSellExcessItems

            .OptimalT2Decryptor = DefaultBPOptimalDecryptor
            .OptimalT3Decryptor = DefaultBPOptimalDecryptor

            .IncludeInventionCost = DefaultBPIncludeInventionCost
            .IncludeInventionTime = DefaultBPIncludeInventionTime
            .IncludeCopyCost = DefaultBPIncludecopyCost
            .IncludeCopyTime = DefaultBPIncludeCopyTime
            .IncludeT3Cost = DefaultBPIncludeT3Cost
            .IncludeT3Time = DefaultBPIncludeT3Time

            .RelicType = DefaultBPRelicType
            .T2DecryptorType = DefaultBPT2DecryptorType
            .T3DecryptorType = DefaultBPT3DecryptorType

            .IgnoreInvention = DefaultBPIgnoreInvention
            .IgnoreMinerals = DefaultBPIgnoreMinerals
            .IgnoreT1Item = DefaultBPIgnoreT1Item

            .IncludeIgnoredBPs = DefaultBPIncludeIgnoredBPs

            .HistoryAvgDays = DefaultSVRAveragePriceDuration
            .HistoryRegion = DefaultSVRAveragePriceRegion

            .ExporttoShoppingListType = DefaultBPShoppingListExportType

            .CompColumnSort = DefaultBPCompColumnSort
            .CompColumnSortType = DefaultBPCompColumnSortType
            .RawColumnSort = DefaultBPRawColumnSort
            .RawColumnSortType = DefaultBPRawColumnSortType

            .RawProfitType = DefaultBPRawProfitType
            .CompProfitType = DefaultBPCompProfitType

            .CompressedOre = DefaultBPCompressedOre
            .SellExcessBuildItems = DefaultBPSellExcessItems
            .BuildT2T3Materials = CType(DefaultBuiltMatsType, BuildMatType)
        End With

        ' Save locally
        BPSettings = LocalSettings

        Return LocalSettings

    End Function

    ' Returns the tab settings
    Public Function GetBPSettings() As BPTabSettings
        Return BPSettings
    End Function

#End Region

#Region "Update Price Tab Settings"

    ' Loads the tab settings
    Public Function LoadUpdatePricesSettings() As UpdatePriceTabSettings
        Dim TempSettings As UpdatePriceTabSettings = Nothing

        Try
            If FileExists(SettingsFolder, UpdatePricesFileName) Then

                'Get the settings
                With TempSettings
                    .AllRawMats = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AllRawMats", DefaultPriceChecks))
                    .AdvancedProtectiveTechnology = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AdvancedProtectiveTechnology", DefaultPriceChecks))
                    .Gas = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Gas", DefaultPriceChecks))
                    .IceProducts = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "IceProducts", DefaultPriceChecks))
                    .MolecularForgingTools = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "MolecularForgingTools", DefaultPriceChecks))
                    .FactionMaterials = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "FactionMaterials", DefaultPriceChecks))
                    .NamedComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "NamedComponents", DefaultPriceChecks))
                    .Minerals = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Minerals", DefaultPriceChecks))
                    .Planetary = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Planetary", DefaultPriceChecks))
                    .RawMaterials = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "RawMaterials", DefaultPriceChecks))
                    .Salvage = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Salvage", DefaultPriceChecks))
                    .Misc = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Misc", DefaultPriceChecks))
                    .BPCs = CInt(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeInteger, UpdatePricesFileName, "BPCs", DefaultPriceChecks))

                    .AdvancedMoonMats = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AdvancedMoonMats", DefaultPriceChecks))
                    .BoosterMats = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "BoosterMats", DefaultPriceChecks))
                    .MolecularForgedMats = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "MolecularForgedMats", DefaultPriceChecks))
                    .Polymers = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Polymers", DefaultPriceChecks))
                    .ProcessedMoonMats = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "ProcessedMoonMats", DefaultPriceChecks))
                    .RawMoonMats = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "RawMoonMats", DefaultPriceChecks))

                    .AncientRelics = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AncientRelics", DefaultPriceChecks))
                    .Datacores = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Datacores", DefaultPriceChecks))
                    .Decryptors = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Decryptors", DefaultPriceChecks))
                    .RDB = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "RDB", DefaultPriceChecks))

                    .AllManufacturedItems = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AllManufacturedItems", DefaultPriceChecks))

                    .Ships = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Ships", DefaultPriceChecks))
                    .Charges = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Charges", DefaultPriceChecks))
                    .Modules = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Modules", DefaultPriceChecks))
                    .Drones = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Drones", DefaultPriceChecks))
                    .Rigs = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Rigs", DefaultPriceChecks))
                    .Subsystems = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Subsystems", DefaultPriceChecks))
                    .Deployables = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Deployables", DefaultPriceChecks))
                    .Boosters = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Boosters", DefaultPriceChecks))
                    .Structures = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Structures", DefaultPriceChecks))
                    .StructureRigs = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "StructureRigs", DefaultPriceChecks))
                    .Celestials = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Celestials", DefaultPriceChecks))
                    .StructureModules = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "StructureModules", DefaultPriceChecks))
                    .Implants = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Implants", DefaultPriceChecks))

                    .AdvancedCapComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AdvancedCapComponents", DefaultPriceChecks))
                    .AdvancedComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "AdvancedComponents", DefaultPriceChecks))
                    .FuelBlocks = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "FuelBlocks", DefaultPriceChecks))
                    .ProtectiveComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "ProtectiveComponents", DefaultPriceChecks))
                    .RAM = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "RAM", DefaultPriceChecks))
                    .NoBuildItems = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "NoBuildItems", False)) ' Default always false on this
                    .CapitalShipComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "CapitalShipComponents", DefaultPriceChecks))
                    .StructureComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "StructureComponents", DefaultPriceChecks))
                    .SubsystemComponents = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "SubsystemComponents", DefaultPriceChecks))

                    .T1 = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "T1", DefaultPriceChecks))
                    .T2 = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "T2", DefaultPriceChecks))
                    .T3 = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "T3", DefaultPriceChecks))
                    .Faction = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Faction", DefaultPriceChecks))
                    .Pirate = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Pirate", DefaultPriceChecks))
                    .Storyline = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "Storyline", DefaultPriceChecks))

                    .SelectedRegion = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "SelectedRegion", DefaultPriceRegion))
                    .SelectedSystem = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "SelectedSystem", DefaultPriceSystem))
                    .ItemsCombo = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "ItemsCombo", DefaultPriceItemsCombo))
                    .RawMatsCombo = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "RawMatsCombo", DefaultPriceRawMatsCombo))

                    .RawPriceModifier = CDbl(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeDouble, UpdatePricesFileName, "RawPriceModifier", DefaultRawPriceModifier))
                    .ItemsPriceModifier = CDbl(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeDouble, UpdatePricesFileName, "ItemsPriceModifier", DefaultItemsPriceModifier))

                    .PriceDataSource = CType(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeInteger, UpdatePricesFileName, "PriceDataSource", DefaultUseESIData), DataSource)
                    .UsePriceProfile = CBool(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeBoolean, UpdatePricesFileName, "UsePriceProfile", DefaultUsePriceProfile))

                    .ColumnSort = CInt(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeInteger, UpdatePricesFileName, "ColumnSort", DefaultUPColumnSort))
                    .ColumnSortType = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "ColumnSortType", DefaultUPColumnSortType))

                    .PPRawPriceType = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PPRawPriceType", DefaultPPRawPriceType))
                    .PPRawRegion = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PPRawRegion", DefaultPPRawRegion))
                    .PPRawSystem = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PPRawSystem", DefaultPPRawSystem))
                    .PPRawPriceMod = CDbl(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeDouble, UpdatePricesFileName, "PPRawPriceMod", DefaultPPRawPriceMod))

                    .PPItemsPriceType = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PPItemsPriceType", DefaultPPItemsPriceType))
                    .PPItemsRegion = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PPItemsRegion", DefaultPPItemsRegion))
                    .PPItemsSystem = CStr(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeString, UpdatePricesFileName, "PPItemsSystem", DefaultPPItemsSystem))
                    .PPItemsPriceMod = CDbl(GetSettingValue(SettingsFolder, UpdatePricesFileName, SettingTypes.TypeDouble, UpdatePricesFileName, "PPItemsPriceMod", DefaultPPItemsPriceMod))

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
        Dim UpdatePricesSettingsList(69) As Setting

        Try
            UpdatePricesSettingsList(0) = New Setting("AllRawMats", CStr(PriceSettings.AllRawMats))

            UpdatePricesSettingsList(1) = New Setting("AdvancedProtectiveTechnology", CStr(PriceSettings.AdvancedProtectiveTechnology))
            UpdatePricesSettingsList(2) = New Setting("Gas", CStr(PriceSettings.Gas))
            UpdatePricesSettingsList(3) = New Setting("IceProducts", CStr(PriceSettings.IceProducts))
            UpdatePricesSettingsList(4) = New Setting("MolecularForgingTools", CStr(PriceSettings.MolecularForgingTools))
            UpdatePricesSettingsList(5) = New Setting("FactionMaterials", CStr(PriceSettings.FactionMaterials))
            UpdatePricesSettingsList(6) = New Setting("NamedComponents", CStr(PriceSettings.NamedComponents))
            UpdatePricesSettingsList(7) = New Setting("Minerals", CStr(PriceSettings.Minerals))
            UpdatePricesSettingsList(8) = New Setting("Planetary", CStr(PriceSettings.Planetary))
            UpdatePricesSettingsList(9) = New Setting("RawMaterials", CStr(PriceSettings.RawMaterials))
            UpdatePricesSettingsList(10) = New Setting("Salvage", CStr(PriceSettings.Salvage))
            UpdatePricesSettingsList(11) = New Setting("Misc", CStr(PriceSettings.Misc))
            UpdatePricesSettingsList(12) = New Setting("BPCs", CStr(PriceSettings.BPCs))

            UpdatePricesSettingsList(13) = New Setting("AdvancedMoonMats", CStr(PriceSettings.AdvancedMoonMats))
            UpdatePricesSettingsList(14) = New Setting("BoosterMats", CStr(PriceSettings.BoosterMats))
            UpdatePricesSettingsList(15) = New Setting("MolecularForgedMats", CStr(PriceSettings.MolecularForgedMats))
            UpdatePricesSettingsList(16) = New Setting("Polymers", CStr(PriceSettings.Polymers))
            UpdatePricesSettingsList(17) = New Setting("ProcessedMoonMats", CStr(PriceSettings.ProcessedMoonMats))
            UpdatePricesSettingsList(18) = New Setting("RawMoonMats", CStr(PriceSettings.RawMoonMats))

            UpdatePricesSettingsList(19) = New Setting("AncientRelics", CStr(PriceSettings.AncientRelics))
            UpdatePricesSettingsList(20) = New Setting("Datacores", CStr(PriceSettings.Datacores))
            UpdatePricesSettingsList(21) = New Setting("Decryptors", CStr(PriceSettings.Decryptors))
            UpdatePricesSettingsList(22) = New Setting("RDB", CStr(PriceSettings.RDB))

            UpdatePricesSettingsList(23) = New Setting("AllManufacturedItems", CStr(PriceSettings.AllManufacturedItems))

            UpdatePricesSettingsList(24) = New Setting("Ships", CStr(PriceSettings.Ships))
            UpdatePricesSettingsList(25) = New Setting("Charges", CStr(PriceSettings.Charges))
            UpdatePricesSettingsList(26) = New Setting("Modules", CStr(PriceSettings.Modules))
            UpdatePricesSettingsList(27) = New Setting("Drones", CStr(PriceSettings.Drones))
            UpdatePricesSettingsList(28) = New Setting("Rigs", CStr(PriceSettings.Rigs))
            UpdatePricesSettingsList(29) = New Setting("Subsystems", CStr(PriceSettings.Subsystems))
            UpdatePricesSettingsList(30) = New Setting("Deployables", CStr(PriceSettings.Deployables))
            UpdatePricesSettingsList(31) = New Setting("Boosters", CStr(PriceSettings.Boosters))
            UpdatePricesSettingsList(32) = New Setting("Structures", CStr(PriceSettings.Structures))
            UpdatePricesSettingsList(33) = New Setting("StructureRigs", CStr(PriceSettings.StructureRigs))
            UpdatePricesSettingsList(34) = New Setting("Celestials", CStr(PriceSettings.Celestials))
            UpdatePricesSettingsList(35) = New Setting("StructureModules", CStr(PriceSettings.StructureModules))
            UpdatePricesSettingsList(36) = New Setting("Implants", CStr(PriceSettings.Implants))

            UpdatePricesSettingsList(37) = New Setting("AdvancedCapComponents", CStr(PriceSettings.AdvancedCapComponents))
            UpdatePricesSettingsList(38) = New Setting("AdvancedComponents", CStr(PriceSettings.AdvancedComponents))
            UpdatePricesSettingsList(39) = New Setting("FuelBlocks", CStr(PriceSettings.FuelBlocks))
            UpdatePricesSettingsList(40) = New Setting("ProtectiveComponents", CStr(PriceSettings.ProtectiveComponents))
            UpdatePricesSettingsList(41) = New Setting("RAM", CStr(PriceSettings.RAM))
            UpdatePricesSettingsList(42) = New Setting("CapitalShipComponents", CStr(PriceSettings.CapitalShipComponents))
            UpdatePricesSettingsList(43) = New Setting("StructureComponents", CStr(PriceSettings.StructureComponents))
            UpdatePricesSettingsList(44) = New Setting("SubsystemComponents", CStr(PriceSettings.SubsystemComponents))

            UpdatePricesSettingsList(45) = New Setting("T1", CStr(PriceSettings.T1))
            UpdatePricesSettingsList(46) = New Setting("T2", CStr(PriceSettings.T2))
            UpdatePricesSettingsList(47) = New Setting("T3", CStr(PriceSettings.T3))
            UpdatePricesSettingsList(48) = New Setting("Faction", CStr(PriceSettings.Faction))
            UpdatePricesSettingsList(49) = New Setting("Pirate", CStr(PriceSettings.Pirate))
            UpdatePricesSettingsList(50) = New Setting("Storyline", CStr(PriceSettings.Storyline))
            UpdatePricesSettingsList(51) = New Setting("SelectedRegion", PriceSettings.SelectedRegion)
            UpdatePricesSettingsList(52) = New Setting("SelectedSystem", CStr(PriceSettings.SelectedSystem))
            UpdatePricesSettingsList(53) = New Setting("ItemsCombo", CStr(PriceSettings.ItemsCombo))
            UpdatePricesSettingsList(54) = New Setting("RawMatsCombo", CStr(PriceSettings.RawMatsCombo))

            UpdatePricesSettingsList(55) = New Setting("ColumnSort", CStr(PriceSettings.ColumnSort))
            UpdatePricesSettingsList(56) = New Setting("ColumnSortType", CStr(PriceSettings.ColumnSortType))

            UpdatePricesSettingsList(57) = New Setting("RawPriceModifier", CStr(PriceSettings.RawPriceModifier))
            UpdatePricesSettingsList(58) = New Setting("ItemsPriceModifier", CStr(PriceSettings.ItemsPriceModifier))
            UpdatePricesSettingsList(59) = New Setting("PriceDataSource", CStr(PriceSettings.PriceDataSource))
            UpdatePricesSettingsList(60) = New Setting("UsePriceProfile", CStr(PriceSettings.UsePriceProfile))

            UpdatePricesSettingsList(61) = New Setting("PPRawPriceType", CStr(PriceSettings.PPRawPriceType))
            UpdatePricesSettingsList(62) = New Setting("PPRawRegion", CStr(PriceSettings.PPRawRegion))
            UpdatePricesSettingsList(63) = New Setting("PPRawSystem", CStr(PriceSettings.PPRawSystem))
            UpdatePricesSettingsList(64) = New Setting("PPRawPriceMod", CStr(PriceSettings.PPRawPriceMod))

            UpdatePricesSettingsList(65) = New Setting("PPItemsPriceType", CStr(PriceSettings.PPItemsPriceType))
            UpdatePricesSettingsList(66) = New Setting("PPItemsRegion", CStr(PriceSettings.PPItemsRegion))
            UpdatePricesSettingsList(67) = New Setting("PPItemsSystem", CStr(PriceSettings.PPItemsSystem))
            UpdatePricesSettingsList(68) = New Setting("PPItemsPriceMod", CStr(PriceSettings.PPItemsPriceMod))

            UpdatePricesSettingsList(69) = New Setting("NoBuildItems", CStr(PriceSettings.NoBuildItems))

            Call WriteSettingsToFile(SettingsFolder, UpdatePricesFileName, UpdatePricesSettingsList, UpdatePricesFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Update Prices Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    Public Function SetDefaultUpdatePriceSettings() As UpdatePriceTabSettings
        Dim LocalSettings As UpdatePriceTabSettings

        With LocalSettings
            .AllRawMats = DefaultPriceChecks
            .AdvancedProtectiveTechnology = DefaultPriceChecks
            .Gas = DefaultPriceChecks
            .IceProducts = DefaultPriceChecks
            .MolecularForgingTools = DefaultPriceChecks
            .FactionMaterials = DefaultPriceChecks
            .NamedComponents = DefaultPriceChecks
            .Minerals = DefaultPriceChecks
            .Planetary = DefaultPriceChecks
            .RawMaterials = DefaultPriceChecks
            .Salvage = DefaultPriceChecks
            .Misc = DefaultPriceChecks
            .BPCs = CInt(DefaultPriceChecks)

            .AdvancedMoonMats = DefaultPriceChecks
            .BoosterMats = DefaultPriceChecks
            .MolecularForgedMats = DefaultPriceChecks
            .Polymers = DefaultPriceChecks
            .ProcessedMoonMats = DefaultPriceChecks
            .RawMoonMats = DefaultPriceChecks

            .AncientRelics = DefaultPriceChecks
            .Datacores = DefaultPriceChecks
            .Decryptors = DefaultPriceChecks
            .RDB = DefaultPriceChecks

            .AllManufacturedItems = DefaultPriceChecks

            .Ships = DefaultPriceChecks
            .Charges = DefaultPriceChecks
            .Modules = DefaultPriceChecks
            .Drones = DefaultPriceChecks
            .Rigs = DefaultPriceChecks
            .Subsystems = DefaultPriceChecks
            .Deployables = DefaultPriceChecks
            .Boosters = DefaultPriceChecks
            .Structures = DefaultPriceChecks
            .StructureRigs = DefaultPriceChecks
            .Celestials = DefaultPriceChecks
            .StructureModules = DefaultPriceChecks
            .Implants = DefaultPriceChecks

            .AdvancedCapComponents = DefaultPriceChecks
            .AdvancedComponents = DefaultPriceChecks
            .FuelBlocks = DefaultPriceChecks
            .ProtectiveComponents = DefaultPriceChecks
            .RAM = DefaultPriceChecks
            .NoBuildItems = False ' Always false
            .CapitalShipComponents = DefaultPriceChecks
            .StructureComponents = DefaultPriceChecks
            .SubsystemComponents = DefaultPriceChecks

            .T1 = DefaultPriceChecks
            .T2 = DefaultPriceChecks
            .T3 = DefaultPriceChecks
            .Faction = DefaultPriceChecks
            .Pirate = DefaultPriceChecks
            .Storyline = DefaultPriceChecks
            .SelectedRegion = DefaultPriceRegion
            .SelectedSystem = DefaultPriceSystem
            .ItemsCombo = DefaultPriceItemsCombo
            .RawMatsCombo = DefaultPriceRawMatsCombo
            .ColumnSort = DefaultUPColumnSort
            .ColumnSortType = DefaultUPColumnSortType
            .RawPriceModifier = DefaultRawPriceModifier
            .ItemsPriceModifier = DefaultItemsPriceModifier
            .PriceDataSource = CType(DefaultUseESIData, DataSource)
            .UsePriceProfile = DefaultUsePriceProfile
            .StructureModules = DefaultPriceChecks

            .PPItemsPriceType = DefaultPPItemsPriceType
            .PPItemsRegion = DefaultPPItemsRegion
            .PPItemsSystem = DefaultPPItemsSystem
            .PPItemsPriceMod = DefaultPPItemsPriceMod
            .PPRawPriceType = DefaultPPRawPriceType
            .PPRawRegion = DefaultPPRawRegion
            .PPRawSystem = DefaultPPRawSystem
            .PPRawPriceMod = DefaultPPRawPriceMod

        End With

        ' Save locally
        UpdatePricesSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Returns the tab settings
    Public Function GetUpdatePricesSettings() As UpdatePriceTabSettings
        Return UpdatePricesSettings
    End Function

#End Region

#Region "Manufacturing Tab Settings"

    ' Loads the tab settings
    Public Function LoadManufacturingSettings() As ManufacturingTabSettings
        Dim TempSettings As ManufacturingTabSettings = Nothing

        Try
            If FileExists(SettingsFolder, ManufacturingSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .BlueprintType = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "BlueprintType", DefaultBlueprintType))
                    .CheckTech1 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTech1", DefaultCheckTech1))
                    .CheckTech2 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTech2", DefaultCheckTech2))
                    .CheckTech3 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTech3", DefaultCheckTech3))
                    .CheckTechStoryline = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTechStoryline", DefaultCheckTechStoryline))
                    .CheckTechNavy = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTechNavy", DefaultCheckTechNavy))
                    .CheckTechPirate = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckTechPirate", DefaultCheckTechPirate))
                    .ItemTypeFilter = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "ItemTypeFilter", DefaultItemTypeFilter))
                    .TextItemFilter = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "TextItemFilter", DefaultTextItemFilter))
                    .CheckBPTypeShips = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeShips", DefaultCheckBPTypeShips))
                    .CheckBPTypeDrones = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeDrones", DefaultCheckBPTypeDrones))
                    .CheckBPTypeComponents = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeComponents", DefaultCheckBPTypeComponents))
                    .CheckBPTypeStructures = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeStructures", DefaultCheckBPTypeStructures))
                    .CheckBPTypeMisc = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeMisc", DefaultCheckBPTypeTools))
                    .CheckBPTypeNPCBPOs = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeNPCBPOs", DefaultCheckBPTypeNPCBPOs))
                    .CheckBPTypeModules = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeModules", DefaultCheckBPTypeModules))
                    .CheckBPTypeAmmoCharges = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeAmmoCharges", DefaultCheckBPTypeAmmoCharges))
                    .CheckBPTypeRigs = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeRigs", DefaultCheckBPTypeRigs))
                    .CheckBPTypeSubsystems = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeSubsystems", DefaultCheckBPTypeSubsystems))
                    .CheckBPTypeBoosters = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeBoosters", DefaultCheckBPTypeBoosters))
                    .CheckBPTypeDeployables = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeDeployables", DefaultCheckBPTypeDeployables))
                    .CheckBPTypeCelestials = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeCelestials", DefaultCheckBPTypeCelestials))
                    .CheckBPTypeReactions = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeReactions", DefaultCheckBPTypeReactions))
                    .CheckBPTypeStructureModules = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeStructureModules", DefaultCheckBPTypeStructureModules))
                    .CheckBPTypeStationParts = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckBPTypeStationParts", DefaultCheckBPTypeStationParts))
                    .CheckDecryptorNone = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptorNone", DefaultCheckDecryptorNone))
                    .CheckDecryptorOptimal = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "CheckDecryptorOptimal", DefaultCheckDecryptorOptimal))
                    .CheckDecryptor06 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor06", DefaultCheckDecryptor06))
                    .CheckDecryptor09 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor09", DefaultCheckDecryptor09))
                    .CheckDecryptor10 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor10", DefaultCheckDecryptor10))
                    .CheckDecryptor11 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor11", DefaultCheckDecryptor11))
                    .CheckDecryptor12 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor12", DefaultCheckDecryptor12))
                    .CheckDecryptor15 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor15", DefaultCheckDecryptor15))
                    .CheckDecryptor18 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor18", DefaultCheckDecryptor18))
                    .CheckDecryptor19 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptor19", DefaultCheckDecryptor19))
                    .CheckDecryptorUseforT2 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptorUseforT2", DefaultCheckDecryptorUseforT2))
                    .CheckDecryptorUseforT3 = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckDecryptorUseforT3", DefaultCheckDecryptorUseforT3))
                    .CheckIgnoreInvention = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIgnoreInvention", DefaultCheckIgnoreInvention))
                    .CheckRelicWrecked = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRelicWrecked", DefaultCheckRelicWrecked))
                    .CheckRelicIntact = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRelicIntact", DefaultCheckRelicIntact))
                    .CheckRelicMalfunction = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRelicMalfunction", DefaultCheckRelicMalfunction))
                    .CheckOnlyBuild = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckOnlyBuild", DefaultCheckOnlyBuild))
                    .CheckOnlyInvent = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckOnlyInvent", DefaultCheckOnlyInvent))
                    .CheckIncludeTaxes = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeTaxes", DefaultCheckIncludeTaxes))
                    .CheckIncludeBrokersFees = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "CheckIncludeBrokersFees", DefaultIncludeBrokersFees))
                    .CalcBrokerFeeRate = CDbl(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeDouble, ManufacturingSettingsFileName, "CalcBrokerFeeRate", DefaultCalcBrokerFeeRate))
                    .CheckIncludeUsage = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeUsage", DefaultCheckIncludeUsage))
                    .CheckRaceAmarr = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceAmarr", DefaultCheckRaceAmarr))
                    .CheckRaceCaldari = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceCaldari", DefaultCheckRaceCaldari))
                    .CheckRaceGallente = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceGallente", DefaultCheckRaceGallente))
                    .CheckRaceMinmatar = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceMinmatar", DefaultCheckRacePirate))
                    .CheckRacePirate = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRacePirate", DefaultCheckRacePirate))
                    .CheckRaceOther = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckRaceOther", DefaultCheckRaceOther))
                    .PriceCompare = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "PriceCompare", DefaultPriceCompare))
                    .CheckIncludeT2Owned = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeT2Owned", DefaultCheckIncludeT2Owned))
                    .CheckIncludeT3Owned = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckIncludeT3Owned", DefaultCheckIncludeT3Owned))
                    .CheckSVRIncludeNull = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckSVRIncludeNull", DefaultCheckSVRIncludeNull))
                    .ProductionLines = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "ProductionLines", DefaultCalcProductionLines))
                    .LaboratoryLines = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "LaboratoryLines", DefaultCalcLaboratoryLines))
                    .Runs = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "Runs", DefaultCalcRuns))
                    .BPRuns = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "BPRuns", DefaultCalcBPRuns))
                    .CheckSmall = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckSmall", DefaultCalcSizeChecks))
                    .CheckMedium = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckMedium", DefaultCalcSizeChecks))
                    .CheckLarge = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckLarge", DefaultCalcSizeChecks))
                    .CheckXL = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckXL", DefaultCalcSizeChecks))
                    .CheckCapitalComponentsFacility = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckCapitalComponentsFacility", DefaultCheckT3Destroyers))
                    .CheckT3DestroyerFacility = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckT3DestroyerFacility", DefaultCheckCapComponents))
                    .CheckAutoCalcNumBPs = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckAutoCalcNumBPs", DefaultCheckAutoCalcNumBPs))
                    .IgnoreInvention = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IgnoreInvention", DefaultCalcIgnoreInvention))
                    .IgnoreMinerals = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IgnoreMinerals", DefaultCalcIgnoreMinerals))
                    .IgnoreT1Item = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IgnoreT1Item", DefaultCalcIgnoreT1Item))
                    .CalcPPU = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CalcPPU", DefaultCalcPPU))
                    .ManufacturingFWUpgradeLevel = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "ManufacturingFWUpgradeLevel", DefaultCalcManufacturingFWLevel))
                    .CopyingFWUpgradeLevel = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "CopyingFWUpgradeLevel", DefaultCalcCopyingFWLevel))
                    .InventionFWUpgradeLevel = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "InventionFWUpgradeLevel", DefaultCalcInventionFWLevel))
                    .ColumnSort = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "ColumnSort", DefaultCalcColumnSort))
                    .ColumnSortType = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "ColumnSortType", DefaultCalcColumnType))
                    .PriceTrend = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "PriceTrend", DefaultCalcPriceTrend))
                    .MinBuildTime = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "MinBuildTime", DefaultCalcMinBuildTime))
                    .MinBuildTimeCheck = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "MinBuildTimeCheck", DefaultCalcMinBuildTimeCheck))
                    .MaxBuildTime = CStr(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "MaxBuildTime", DefaultCalcMaxBuildTime))
                    .MaxBuildTimeCheck = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "MaxBuildTimeCheck", DefaultCalcMaxBuildTimeCheck))
                    .IPHThreshold = CDbl(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeDouble, ManufacturingSettingsFileName, "IPHThreshold", DefaultCalcIPHThreshold))
                    .IPHThresholdCheck = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "IPHThresholdCheck", DefaultCalcMinBuildTimeCheck))
                    .ProfitThreshold = CDbl(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeDouble, ManufacturingSettingsFileName, "ProfitThreshold", DefaultCalcProfitThreshold))
                    .ProfitThresholdCheck = CInt(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeInteger, ManufacturingSettingsFileName, "ProfitThresholdCheck", DefaultCalcProfitThresholdCheck))
                    .VolumeThreshold = CDbl(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeDouble, ManufacturingSettingsFileName, "VolumeThreshold", DefaultCalcVolumeThreshold))
                    .VolumeThresholdCheck = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "VolumeThresholdCheck", DefaultCalcVolumeThresholdCheck))
                    .CheckSellExcessItems = CBool(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeBoolean, ManufacturingSettingsFileName, "CheckSellExcessItems", DefaultCalcSellExcessItems))
                    .BuildT2T3Materials = CType(GetSettingValue(SettingsFolder, ManufacturingSettingsFileName, SettingTypes.TypeString, ManufacturingSettingsFileName, "BuildT2T3Materials", DefaultBuiltMatsType), BuildMatType)
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
            .CheckBPTypeNPCBPOs = DefaultCheckBPTypeNPCBPOs
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
            .CheckBPTypeStructureModules = DefaultCheckBPTypeStructureModules
            .CheckBPTypeStationParts = DefaultCheckBPTypeStationParts
            .CheckBPTypeDeployables = DefaultCheckBPTypeDeployables
            .CheckBPTypeReactions = DefaultCheckBPTypeReactions
            .CheckDecryptorNone = DefaultCheckDecryptorNone
            .CheckDecryptorOptimal = DefaultCheckDecryptorOptimal
            .CheckDecryptor06 = DefaultCheckDecryptor06
            .CheckDecryptor09 = DefaultCheckDecryptor09
            .CheckDecryptor10 = DefaultCheckDecryptor10
            .CheckDecryptor11 = DefaultCheckDecryptor11
            .CheckDecryptor12 = DefaultCheckDecryptor12
            .CheckDecryptor15 = DefaultCheckDecryptor15
            .CheckDecryptor18 = DefaultCheckDecryptor18
            .CheckDecryptor19 = DefaultCheckDecryptor19
            .CheckDecryptorUseforT2 = DefaultCheckDecryptorUseforT2
            .CheckDecryptorUseforT3 = DefaultCheckDecryptorUseforT3
            .CheckIgnoreInvention = DefaultCheckIgnoreInvention
            .CheckRelicWrecked = DefaultCheckRelicWrecked
            .CheckRelicIntact = DefaultCheckRelicIntact
            .CheckRelicMalfunction = DefaultCheckRelicMalfunction
            .CheckOnlyBuild = DefaultCheckOnlyBuild
            .CheckOnlyInvent = DefaultCheckOnlyInvent
            .CheckIncludeTaxes = DefaultCheckIncludeTaxes
            .CheckIncludeBrokersFees = DefaultIncludeBrokersFees
            .CalcBrokerFeeRate = DefaultCalcBrokerFeeRate
            .CheckIncludeUsage = DefaultCheckIncludeUsage
            .CheckRaceAmarr = DefaultCheckRaceAmarr
            .CheckRaceCaldari = DefaultCheckRaceCaldari
            .CheckRaceGallente = DefaultCheckRaceGallente
            .CheckRaceMinmatar = DefaultCheckRaceMinmatar
            .CheckRacePirate = DefaultCheckRacePirate
            .CheckRaceOther = DefaultCheckRaceOther
            .PriceCompare = DefaultPriceCompare
            .CheckIncludeT2Owned = DefaultCheckIncludeT2Owned
            .CheckIncludeT3Owned = DefaultCheckIncludeT3Owned
            .CheckSVRIncludeNull = DefaultCheckSVRIncludeNull
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
            .CalcPPU = DefaultCalcPPU
            .CheckSellExcessItems = DefaultBPSellExcessItems
            .ManufacturingFWUpgradeLevel = DefaultCalcManufacturingFWLevel
            .CopyingFWUpgradeLevel = DefaultCalcCopyingFWLevel
            .InventionFWUpgradeLevel = DefaultCalcInventionFWLevel
            .ColumnSort = DefaultCalcColumnSort
            .ColumnSortType = DefaultCalcColumnType
            .PriceTrend = DefaultCalcPriceTrend
            .MinBuildTime = DefaultCalcMinBuildTime
            .MinBuildTimeCheck = DefaultCalcMinBuildTimeCheck
            .MaxBuildTime = DefaultCalcMaxBuildTime
            .MaxBuildTimeCheck = DefaultCalcMaxBuildTimeCheck
            .IPHThreshold = DefaultCalcIPHThreshold
            .IPHThresholdCheck = DefaultCalcIPHThresholdCheck
            .ProfitThreshold = DefaultCalcProfitThreshold
            .ProfitThresholdCheck = DefaultCalcProfitThresholdCheck
            .VolumeThreshold = DefaultCalcVolumeThreshold
            .VolumeThresholdCheck = DefaultCalcVolumeThresholdCheck
            .BuildT2T3Materials = CType(DefaultBuiltMatsType, BuildMatType)
        End With

        ' Save locally
        ManufacturingSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveManufacturingSettings(SentSettings As ManufacturingTabSettings)
        Dim ManufacturingSettingsList(88) As Setting

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
            ManufacturingSettingsList(19) = New Setting("CheckDecryptorNone", CStr(SentSettings.CheckDecryptorNone))
            ManufacturingSettingsList(20) = New Setting("CheckDecryptor06", CStr(SentSettings.CheckDecryptor06))
            ManufacturingSettingsList(21) = New Setting("CheckDecryptor10", CStr(SentSettings.CheckDecryptor10))
            ManufacturingSettingsList(22) = New Setting("CheckDecryptor11", CStr(SentSettings.CheckDecryptor11))
            ManufacturingSettingsList(23) = New Setting("CheckDecryptor12", CStr(SentSettings.CheckDecryptor12))
            ManufacturingSettingsList(24) = New Setting("CheckDecryptor18", CStr(SentSettings.CheckDecryptor18))
            ManufacturingSettingsList(25) = New Setting("CheckIgnoreInvention", CStr(SentSettings.CheckIgnoreInvention))
            ManufacturingSettingsList(26) = New Setting("CheckRelicWrecked", CStr(SentSettings.CheckRelicWrecked))
            ManufacturingSettingsList(27) = New Setting("CheckRelicIntact", CStr(SentSettings.CheckRelicIntact))
            ManufacturingSettingsList(28) = New Setting("CheckRelicMalfunction", CStr(SentSettings.CheckRelicMalfunction))
            ManufacturingSettingsList(29) = New Setting("CheckOnlyBuild", CStr(SentSettings.CheckOnlyBuild))
            ManufacturingSettingsList(30) = New Setting("CheckOnlyInvent", CStr(SentSettings.CheckOnlyInvent))
            ManufacturingSettingsList(31) = New Setting("CheckIncludeTaxes", CStr(SentSettings.CheckIncludeTaxes))
            ManufacturingSettingsList(32) = New Setting("CheckIncludeUsage", CStr(SentSettings.CheckIncludeUsage))
            ManufacturingSettingsList(33) = New Setting("CheckRaceAmarr", CStr(SentSettings.CheckRaceAmarr))
            ManufacturingSettingsList(34) = New Setting("CheckRaceCaldari", CStr(SentSettings.CheckRaceCaldari))
            ManufacturingSettingsList(35) = New Setting("CheckRaceGallente", CStr(SentSettings.CheckRaceGallente))
            ManufacturingSettingsList(36) = New Setting("CheckRaceMinmatar", CStr(SentSettings.CheckRaceMinmatar))
            ManufacturingSettingsList(37) = New Setting("CheckRacePirate", CStr(SentSettings.CheckRacePirate))
            ManufacturingSettingsList(38) = New Setting("CheckRaceOther", CStr(SentSettings.CheckRaceOther))
            ManufacturingSettingsList(39) = New Setting("PriceCompare", CStr(SentSettings.PriceCompare))
            ManufacturingSettingsList(40) = New Setting("CheckIncludeT2Owned", CStr(SentSettings.CheckIncludeT2Owned))
            ManufacturingSettingsList(41) = New Setting("CheckIncludeT3Owned", CStr(SentSettings.CheckIncludeT3Owned))
            ManufacturingSettingsList(42) = New Setting("CheckSVRIncludeNull", CStr(SentSettings.CheckSVRIncludeNull))
            ManufacturingSettingsList(43) = New Setting("ProductionLines", CStr(SentSettings.ProductionLines))
            ManufacturingSettingsList(44) = New Setting("LaboratoryLines", CStr(SentSettings.LaboratoryLines))
            ManufacturingSettingsList(45) = New Setting("CheckDecryptor09", CStr(SentSettings.CheckDecryptor09))
            ManufacturingSettingsList(46) = New Setting("CheckDecryptor15", CStr(SentSettings.CheckDecryptor15))
            ManufacturingSettingsList(47) = New Setting("CheckDecryptor19", CStr(SentSettings.CheckDecryptor19))
            ManufacturingSettingsList(48) = New Setting("Runs", CStr(SentSettings.Runs))
            ManufacturingSettingsList(49) = New Setting("CheckBPTypeCelestials", CStr(SentSettings.CheckBPTypeCelestials))
            ManufacturingSettingsList(50) = New Setting("CheckBPTypeDeployables", CStr(SentSettings.CheckBPTypeDeployables))
            ManufacturingSettingsList(51) = New Setting("CheckSmall", CStr(SentSettings.CheckSmall))
            ManufacturingSettingsList(52) = New Setting("CheckMedium", CStr(SentSettings.CheckMedium))
            ManufacturingSettingsList(53) = New Setting("CheckLarge", CStr(SentSettings.CheckLarge))
            ManufacturingSettingsList(54) = New Setting("CheckXL", CStr(SentSettings.CheckXL))
            ManufacturingSettingsList(55) = New Setting("CheckBPTypeStationParts", CStr(SentSettings.CheckBPTypeStationParts))
            ManufacturingSettingsList(56) = New Setting("CheckIncludeBrokersFees", CStr(SentSettings.CheckIncludeBrokersFees))
            ManufacturingSettingsList(57) = New Setting("CheckDecryptorUseforT2", CStr(SentSettings.CheckDecryptorUseforT2))
            ManufacturingSettingsList(58) = New Setting("CheckDecryptorUseforT3", CStr(SentSettings.CheckDecryptorUseforT3))
            ManufacturingSettingsList(59) = New Setting("CheckCapitalComponentsFacility", CStr(SentSettings.CheckCapitalComponentsFacility))
            ManufacturingSettingsList(60) = New Setting("CheckT3DestroyerFacility", CStr(SentSettings.CheckT3DestroyerFacility))
            ManufacturingSettingsList(61) = New Setting("BPRuns", CStr(SentSettings.BPRuns))
            ManufacturingSettingsList(62) = New Setting("CheckAutoCalcNumBPs", CStr(SentSettings.CheckAutoCalcNumBPs))
            ManufacturingSettingsList(63) = New Setting("IgnoreInvention", CStr(SentSettings.IgnoreInvention))
            ManufacturingSettingsList(64) = New Setting("IgnoreMinerals", CStr(SentSettings.IgnoreMinerals))
            ManufacturingSettingsList(65) = New Setting("IgnoreT1Item", CStr(SentSettings.IgnoreT1Item))
            ManufacturingSettingsList(66) = New Setting("CalcPPU", CStr(SentSettings.CalcPPU))
            ManufacturingSettingsList(67) = New Setting("ColumnSort", CStr(SentSettings.ColumnSort))
            ManufacturingSettingsList(68) = New Setting("ColumnSortType", CStr(SentSettings.ColumnSortType))
            ManufacturingSettingsList(69) = New Setting("ManufacturingFWUpgradeLevel", CStr(SentSettings.ManufacturingFWUpgradeLevel))
            ManufacturingSettingsList(70) = New Setting("CopyingFWUpgradeLevel", CStr(SentSettings.CopyingFWUpgradeLevel))
            ManufacturingSettingsList(71) = New Setting("PriceTrend", CStr(SentSettings.PriceTrend))
            ManufacturingSettingsList(72) = New Setting("MinBuildTime", CStr(SentSettings.MinBuildTime))
            ManufacturingSettingsList(73) = New Setting("MinBuildTimeCheck", CStr(SentSettings.MinBuildTimeCheck))
            ManufacturingSettingsList(74) = New Setting("MaxBuildTime", CStr(SentSettings.MaxBuildTime))
            ManufacturingSettingsList(75) = New Setting("MaxBuildTimeCheck", CStr(SentSettings.MaxBuildTimeCheck))
            ManufacturingSettingsList(76) = New Setting("IPHThreshold", CStr(SentSettings.IPHThreshold))
            ManufacturingSettingsList(77) = New Setting("IPHThresholdCheck", CStr(SentSettings.IPHThresholdCheck))
            ManufacturingSettingsList(78) = New Setting("ProfitThreshold", CStr(SentSettings.ProfitThreshold))
            ManufacturingSettingsList(79) = New Setting("ProfitThresholdCheck", CStr(SentSettings.ProfitThresholdCheck))
            ManufacturingSettingsList(80) = New Setting("VolumeThreshold", CStr(SentSettings.VolumeThreshold))
            ManufacturingSettingsList(81) = New Setting("VolumeThresholdCheck", CStr(SentSettings.VolumeThresholdCheck))
            ManufacturingSettingsList(82) = New Setting("CheckDecryptorOptimal", CStr(SentSettings.CheckDecryptorOptimal))
            ManufacturingSettingsList(83) = New Setting("CheckBPTypeStructureModules", CStr(SentSettings.CheckBPTypeStructureModules))
            ManufacturingSettingsList(84) = New Setting("CheckBPTypeReactions", CStr(SentSettings.CheckBPTypeReactions))
            ManufacturingSettingsList(85) = New Setting("CheckBPTypeNPCBPOs", CStr(SentSettings.CheckBPTypeNPCBPOs))
            ManufacturingSettingsList(86) = New Setting("CheckSellExcessItems", CStr(SentSettings.CheckSellExcessItems))
            ManufacturingSettingsList(87) = New Setting("CalcBrokerFeeRate", CStr(SentSettings.CalcBrokerFeeRate))
            ManufacturingSettingsList(88) = New Setting("BuildT2T3Materials", CStr(SentSettings.BuildT2T3Materials))

            Call WriteSettingsToFile(SettingsFolder, ManufacturingSettingsFileName, ManufacturingSettingsList, ManufacturingSettingsFileName)

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

            If FileExists(SettingsFolder, DatacoreSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .PricesFrom = CStr(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeString, DatacoreSettingsFileName, "PricesFrom", DefaultReactPOSFuelCost))
                    .CheckHighSecAgents = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckHighSecAgents", DefaultReactCheckTaxes))
                    .CheckLowNullSecAgents = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckLowNullSecAgents", DefaultReactCheckFees))
                    .CheckIncludeAgentsCannotAccess = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckIncludeAgentsCannotAccess", DefaultReactItemChecks))
                    .AgentsInRegion = CStr(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeString, DatacoreSettingsFileName, "AgentsInRegion", DefaultReactItemChecks))
                    .CheckSovAmarr = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovAmarr", DefaultReactItemChecks))
                    .CheckSovAmmatar = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovAmmatar", DefaultReactItemChecks))
                    .CheckSovGallente = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovGallente", DefaultReactItemChecks))
                    .CheckSovSyndicate = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovSyndicate", DefaultReactItemChecks))
                    .CheckSovKhanid = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovKhanid", DefaultReactItemChecks))
                    .CheckSovThukker = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovThukker", DefaultReactItemChecks))
                    .CheckSovCaldari = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovCaldari", DefaultReactItemChecks))
                    .CheckSovMinmatar = CBool(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeBoolean, DatacoreSettingsFileName, "CheckSovMinmatar", DefaultReactItemChecks))

                    For i = 1 To 17
                        .SkillsChecked(i - 1) = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Skill" & CStr(i) & "Checked", DefaultSkillLevelChecked))
                        .SkillsLevel(i - 1) = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Skill" & CStr(i) & "Level ", DefaultSkillLevel))
                    Next

                    For i = 1 To 13
                        .CorpsChecked(i - 1) = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Corp" & CStr(i) & "Checked", DefaultSkillLevelChecked))
                        .CorpsStanding(i - 1) = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Corp" & CStr(i) & "Standing ", DefaultSkillLevel))
                    Next

                    .Negotiation = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Negotiation", DefaultNegotiation))
                    .Connections = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "Connections", DefaultConnections))
                    .ResearchProjectMgt = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "ResearchProjectMgt", DefaultResearchProjMgt))

                    .ColumnSort = CInt(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeInteger, DatacoreSettingsFileName, "ColumnSort", DefaultDCColumnSort))
                    .ColumnSortType = CStr(GetSettingValue(SettingsFolder, DatacoreSettingsFileName, SettingTypes.TypeString, DatacoreSettingsFileName, "ColumnSortType", DefaultDCColumnSortType))

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

            .ColumnSort = DefaultDCColumnSort
            .ColumnSortType = DefaultDCColumnSortType

        End With
        ' Save locally
        DatacoreSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveDatacoreSettings(SentSettings As DataCoreTabSettings)
        Dim DatacoreSettingsList(77) As Setting
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

            DatacoreSettingsList(76) = New Setting("ColumnSort", CStr(SentSettings.ColumnSort))
            DatacoreSettingsList(77) = New Setting("ColumnSortType", CStr(SentSettings.ColumnSortType))

            Call WriteSettingsToFile(SettingsFolder, DatacoreSettingsFileName, DatacoreSettingsList, DatacoreSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Datacore Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetDatacoreSettings() As DataCoreTabSettings
        Return DatacoreSettings
    End Function

#End Region

#Region "Mining Tab Settings"

    ' Loads the tab settings
    Public Function LoadMiningSettings() As MiningTabSettings
        Dim TempSettings As MiningTabSettings = Nothing

        Try
            If FileExists(SettingsFolder, MiningSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .OreType = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreType", DefaultMiningOreType))
                    .CheckHighYieldOres = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckHighYieldOres", DefaultMiningCheckHighYieldOres))
                    .CheckHighSecOres = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckHighSecOres", DefaultMiningCheckHighSecOres))
                    .CheckLowSecOres = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckLowSecOres", DefaultMiningCheckLowSecOres))
                    .CheckNullSecOres = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckNullSecOres", DefaultMiningCheckNullSecOres))
                    .CheckA0Ores = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckA0Ores", DefaultMiningCheckA0Ores))
                    .CheckSovAmarr = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovAmarr", DefaultMiningCheckSovAmarr))
                    .CheckSovCaldari = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovCaldari", DefaultMiningCheckSovCaldari))
                    .CheckSovGallente = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovGallente", DefaultMiningCheckSovGallente))
                    .CheckSovMinmatar = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovMinmatar", DefaultMiningCheckSovMinmatar))
                    .CheckSovTriglavian = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovTriglavian", DefaultMiningCheckSovTriglavian))
                    .CheckEDENCOM = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckEDENCOM", DefaultMiningCheckEDENCOM))
                    .CheckIncludeFees = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckIncludeFees", DefaultMiningCheckIncludeFees))
                    .BrokerFeeRate = CDbl(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeDouble, MiningSettingsFileName, "BrokerFeeRate", DefaultMiningBrokerFeeRate))
                    .CheckIncludeTaxes = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckIncludeTaxes", DefaultMiningCheckIncludeTaxes))
                    .OreMiningShip = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreMiningShip", DefaultMiningMiningShip))
                    .IceMiningShip = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceMiningShip", DefaultMiningIceMiningShip))
                    .GasMiningShip = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasMiningShip", DefaultMiningGasMiningShip))
                    .OreStrip = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreStrip", DefaultMiningOreStrip))
                    .IceStrip = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceStrip", DefaultMiningIceStrip))
                    .GasHarvester = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasHarvester", DefaultMiningGasHarvester))
                    .NumOreMiners = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumOreMiners", DefaultMiningNumOreMiners))
                    .NumIceMiners = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumIceMiners", DefaultMiningNumIceMiners))
                    .NumGasHarvesters = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumGasHarvesters", DefaultMiningNumGasHarvesters))
                    .OreUpgrade = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreUpgrade", DefaultMiningOreUpgrade))
                    .IceUpgrade = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceUpgrade", DefaultMiningIceUpgrade))
                    .GasUpgrade = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasUpgrade", DefaultMiningGasUpgrade))
                    .NumOreUpgrades = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumOreUpgrades", DefaultMiningNumOreUpgrades))
                    .NumIceUpgrades = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumIceUpgrades", DefaultMiningNumIceUpgrades))
                    .NumGasUpgrades = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumGasUpgrades", DefaultMiningNumGasUpgrades))
                    .MichiiImplant = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "MichiiImplant", DefaultMiningMichiiImplant))
                    .T1Crystals = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "T1Crystals", DefaultMiningCrystals))
                    .T2Crystals = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "T2Crystals", DefaultMiningCrystals))
                    .OreImplant = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "OreImplant", DefaultMiningOreImplant))
                    .IceImplant = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceImplant", DefaultMiningIceImplant))
                    .GasImplant = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "GasImplant", DefaultMiningGasImplant))
                    .CheckUseHauler = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckUseHauler", DefaultMiningCheckUseHauler))
                    .RoundTripMin = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "RoundTripMin", DefaultMiningRoundTripMin))
                    .RoundTripSec = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "RoundTripSec", DefaultMiningRoundTripSec))
                    .Haulerm3 = CDbl(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "Haulerm3", DefaultMiningHaulerm3))
                    .CheckUseFleetBooster = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckUseFleetBooster", DefaultMiningCheckUseFleetBooster))
                    .BoosterShip = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterShip", DefaultMiningBoosterShip))
                    .BoosterShipSkill = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterShipSkill", DefaultMiningBoosterShipSkill))
                    .MiningFormanSkill = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "MiningFormanSkill", DefaultMiningMiningFormanSkill))
                    .MiningDirectorSkill = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "MiningDirectorSkill", DefaultMiningMiningDirectorSkill))
                    .CheckMineForemanLaserOpBoost = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "CheckMineForemanLaserOpBoost", DefaultMiningCheckMineForemanLaserOpBoost))
                    .CheckMineForemanLaserRangeBoost = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "CheckMineForemanLaserRangeBoost", DefaultMiningCheckMineForemanLaserOpBoost))
                    .CheckMiningForemanMindLink = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckMiningForemanMindLink", DefaultMiningCheckMiningForemanMindLink))
                    .CheckRorqDeployed = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "CheckRorqDeployed", DefaultMiningRorqDeployed))
                    .OverrideCheck = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "OverrideCheck", DefaultMiningOverrideCheck))
                    .OverrideCycleTime = CDbl(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "OverrideCycleTime", DefaultMiningOverrideCycleTime))
                    .OverrideLaserRange = CDbl(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "OverrideLaserRange", DefaultMiningOverrideLaserRange))
                    .RefinedOre = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "RefinedOre", DefaultMiningRefinedOre))
                    .UnrefinedOre = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "UnrefinedOre", DefaultMiningUnrefinedOre))
                    .CompressedOre = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CompressedOre", DefaultMiningCompressedOre))
                    .IndustrialReconfig = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "IndustrialReconfig", DefaultMiningIndustrialReconfig))
                    .CheckSovWormhole = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovWormhole", DefaultMiningCheckSovWormhole))
                    .CheckSovMoon = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovMoon", DefaultMiningCheckSovMoon))
                    .CheckSovC1 = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC1", DefaultMiningCheckSovC1))
                    .CheckSovC2 = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC2", DefaultMiningCheckSovC2))
                    .CheckSovC3 = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC3", DefaultMiningCheckSovC3))
                    .CheckSovC4 = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC4", DefaultMiningCheckSovC4))
                    .CheckSovC5 = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC5", DefaultMiningCheckSovC5))
                    .CheckSovC6 = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CheckSovC6", DefaultMiningCheckSovC6))
                    .NumberofMiners = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "NumberofMiners", DefaultMiningNumberofMiners))
                    .ColumnSort = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "ColumnSort", DefaultMiningColumnSort))
                    .ColumnSortType = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ColumnSortType", DefaultMiningColumnSortType))
                    .MiningDrone = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "MiningDrone", DefaultMiningDrone))
                    .IceMiningDrone = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceMiningDrone", DefaultIceMiningDrone))
                    .NumMiningDrones = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "NumMiningDrones", DefaultNumMiningDrone))
                    .NumIceMiningDrones = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "NumIceMiningDrones", DefaultNumIceMiningdrone))
                    .DroneOpSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "DroneOpSkill", DefaultDroneSkills))
                    .DroneSpecSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "DroneSpecSkill", DefaultDroneSkills))
                    .DroneInterfaceSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "DroneInterfaceSkill", DefaultDroneSkills))
                    .IceDroneOpSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceDroneOpSkill", DefaultDroneSkills))
                    .IceDroneSpecSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceDroneSpecSkill", DefaultDroneSkills))
                    .IceDroneInterfaceSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "IceDroneInterfaceSkill", DefaultDroneSkills))
                    .BoosterMiningDrone = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterMiningDrone", DefaultMiningDrone))
                    .BoosterIceMiningDrone = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterIceMiningDrone", DefaultIceMiningDrone))
                    .BoosterNumMiningDrones = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterNumMiningDrones", DefaultNumMiningDrone))
                    .BoosterNumIceMiningDrones = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterNumIceMiningDrones", DefaultNumIceMiningdrone))
                    .BoosterDroneOpSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterDroneOpSkill", DefaultDroneSkills))
                    .BoosterDroneSpecSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterDroneSpecSkill", DefaultDroneSkills))
                    .BoosterDroneInterfaceSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterDroneInterfaceSkill", DefaultDroneSkills))
                    .BoosterIceDroneOpSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterIceDroneOpSkill", DefaultDroneSkills))
                    .BoosterIceDroneSpecSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterIceDroneSpecSkill", DefaultDroneSkills))
                    .BoosterIceDroneInterfaceSkill = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "BoosterIceDroneInterfaceSkill", DefaultDroneSkills))
                    .BoosterUseDrones = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "BoosterUseDrones", DefaultBoosterUseDrones))
                    .ShipDroneRig1 = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ShipDroneRig1", DefaultDroneRigs))
                    .ShipDroneRig2 = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ShipDroneRig2", DefaultDroneRigs))
                    .ShipDroneRig3 = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ShipDroneRig3", DefaultDroneRigs))
                    .ShipIceDroneRig1 = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ShipIceDroneRig1", DefaultDroneRigs))
                    .ShipIceDroneRig2 = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ShipIceDroneRig2", DefaultDroneRigs))
                    .ShipIceDroneRig3 = CStr(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeString, MiningSettingsFileName, "ShipIceDroneRig3", DefaultDroneRigs))
                    .BoosterDroneRig1 = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterDroneRig1", DefaultBoosterDroneRigs))
                    .BoosterDroneRig2 = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterDroneRig2", DefaultBoosterDroneRigs))
                    .BoosterDroneRig3 = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterDroneRig3", DefaultBoosterDroneRigs))
                    .BoosterIceDroneRig1 = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterIceDroneRig1", DefaultBoosterDroneRigs))
                    .BoosterIceDroneRig2 = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterIceDroneRig2", DefaultBoosterDroneRigs))
                    .BoosterIceDroneRig3 = CInt(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeInteger, MiningSettingsFileName, "BoosterIceDroneRig3", DefaultBoosterDroneRigs))
                    .CrystalTypeA = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CrystalTypeA", DefaultMiningCrystalType))
                    .CrystalTypeB = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CrystalTypeB", DefaultMiningCrystalType))
                    .CrystalTypeC = CBool(GetSettingValue(SettingsFolder, MiningSettingsFileName, SettingTypes.TypeBoolean, MiningSettingsFileName, "CrystalTypeC", DefaultMiningCrystalType))
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
            .CheckA0Ores = DefaultMiningCheckA0Ores
            .CheckSovAmarr = DefaultMiningCheckSovAmarr
            .CheckSovCaldari = DefaultMiningCheckSovCaldari
            .CheckSovGallente = DefaultMiningCheckSovGallente
            .CheckSovMinmatar = DefaultMiningCheckSovMinmatar
            .CheckSovTriglavian = DefaultMiningCheckSovTriglavian
            .CheckEDENCOM = DefaultMiningCheckEDENCOM
            .CheckSovWormhole = DefaultMiningCheckSovWormhole
            .CheckSovMoon = DefaultMiningCheckSovMoon
            .CheckSovC1 = DefaultMiningCheckSovC1
            .CheckSovC2 = DefaultMiningCheckSovC2
            .CheckSovC3 = DefaultMiningCheckSovC3
            .CheckSovC4 = DefaultMiningCheckSovC4
            .CheckSovC5 = DefaultMiningCheckSovC5
            .CheckSovC6 = DefaultMiningCheckSovC6
            .CheckIncludeFees = DefaultMiningCheckIncludeFees
            .BrokerFeeRate = DefaultMiningBrokerFeeRate
            .CheckIncludeTaxes = DefaultMiningCheckIncludeTaxes
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
            .T1Crystals = DefaultMiningCrystals
            .T2Crystals = DefaultMiningCrystals
            .CrystalTypeA = DefaultMiningCrystalType
            .CrystalTypeB = DefaultMiningCrystalType
            .CrystalTypeC = DefaultMiningCrystalType
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
            .CheckMineForemanLaserOpBoost = DefaultMiningCheckMineForemanLaserOpBoost
            .CheckMineForemanLaserRangeBoost = DefaultMiningCheckMineForemanLaserOpBoost
            .CheckMiningForemanMindLink = DefaultMiningCheckMiningForemanMindLink
            .CheckRorqDeployed = DefaultMiningRorqDeployed
            .OverrideCheck = DefaultMiningOverrideCheck
            .OverrideCycleTime = DefaultMiningOverrideCycleTime
            .OverrideLaserRange = DefaultMiningOverrideLaserRange
            .RefinedOre = DefaultMiningRefinedOre
            .UnrefinedOre = DefaultMiningUnrefinedOre
            .CompressedOre = DefaultMiningCompressedOre
            .IndustrialReconfig = DefaultMiningIndustrialReconfig
            .NumberofMiners = DefaultNumMiners
            .ColumnSort = DefaultMiningColumnSort
            .ColumnSortType = DefaultMiningColumnSortType

            .MiningDrone = DefaultMiningDrone
            .NumMiningDrones = DefaultNumMiningDrone
            .IceMiningDrone = DefaultIceMiningDrone
            .NumIceMiningDrones = DefaultNumIceMiningdrone
            .DroneOpSkill = DefaultDroneSkills
            .DroneSpecSkill = DefaultDroneSkills
            .DroneInterfaceSkill = DefaultDroneSkills
            .IceDroneOpSkill = DefaultDroneSkills
            .IceDroneSpecSkill = DefaultDroneSkills
            .IceDroneInterfaceSkill = DefaultDroneSkills

            .BoosterMiningDrone = DefaultMiningDrone
            .BoosterNumMiningDrones = DefaultNumMiningDrone
            .BoosterIceMiningDrone = DefaultIceMiningDrone
            .BoosterNumIceMiningDrones = DefaultNumIceMiningdrone
            .BoosterDroneOpSkill = DefaultDroneSkills
            .BoosterDroneSpecSkill = DefaultDroneSkills
            .BoosterDroneInterfaceSkill = DefaultDroneSkills
            .BoosterIceDroneOpSkill = DefaultDroneSkills
            .BoosterIceDroneSpecSkill = DefaultDroneSkills
            .BoosterIceDroneInterfaceSkill = DefaultDroneSkills

            .BoosterUseDrones = DefaultBoosterUseDrones
            .ShipDroneRig1 = DefaultDroneRigs
            .ShipDroneRig2 = DefaultDroneRigs
            .ShipDroneRig3 = DefaultDroneRigs
            .BoosterDroneRig1 = DefaultBoosterDroneRigs
            .BoosterDroneRig2 = DefaultBoosterDroneRigs
            .BoosterDroneRig3 = DefaultBoosterDroneRigs
            .ShipIceDroneRig1 = DefaultDroneRigs
            .ShipIceDroneRig2 = DefaultDroneRigs
            .ShipIceDroneRig3 = DefaultDroneRigs
            .BoosterIceDroneRig1 = DefaultBoosterDroneRigs
            .BoosterIceDroneRig2 = DefaultBoosterDroneRigs
            .BoosterIceDroneRig3 = DefaultBoosterDroneRigs

        End With

        ' Save locally
        MiningSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveMiningSettings(SentSettings As MiningTabSettings)
        Dim MiningSettingsList(102) As Setting

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
            MiningSettingsList(11) = New Setting("OreMiningShip", CStr(SentSettings.OreMiningShip))
            MiningSettingsList(12) = New Setting("IceMiningShip", CStr(SentSettings.IceMiningShip))
            MiningSettingsList(13) = New Setting("OreStrip", CStr(SentSettings.OreStrip))
            MiningSettingsList(14) = New Setting("IceStrip", CStr(SentSettings.IceStrip))
            MiningSettingsList(15) = New Setting("NumOreMiners", CStr(SentSettings.NumOreMiners))
            MiningSettingsList(16) = New Setting("NumIceMiners", CStr(SentSettings.NumIceMiners))
            MiningSettingsList(17) = New Setting("OreUpgrade", CStr(SentSettings.OreUpgrade))
            MiningSettingsList(18) = New Setting("IceUpgrade", CStr(SentSettings.IceUpgrade))
            MiningSettingsList(19) = New Setting("NumOreUpgrades", CStr(SentSettings.NumOreUpgrades))
            MiningSettingsList(20) = New Setting("NumIceUpgrades", CStr(SentSettings.NumIceUpgrades))
            MiningSettingsList(21) = New Setting("MichiiImplant", CStr(SentSettings.MichiiImplant))
            MiningSettingsList(22) = New Setting("T2Crystals", CStr(SentSettings.T2Crystals))
            MiningSettingsList(23) = New Setting("OreImplant", CStr(SentSettings.OreImplant))
            MiningSettingsList(24) = New Setting("IceImplant", CStr(SentSettings.IceImplant))
            MiningSettingsList(25) = New Setting("CheckUseHauler", CStr(SentSettings.CheckUseHauler))
            MiningSettingsList(26) = New Setting("RoundTripMin", CStr(SentSettings.RoundTripMin))
            MiningSettingsList(27) = New Setting("RoundTripSec", CStr(SentSettings.RoundTripSec))
            MiningSettingsList(28) = New Setting("Haulerm3", CStr(SentSettings.Haulerm3))
            MiningSettingsList(29) = New Setting("CheckUseFleetBooster", CStr(SentSettings.CheckUseFleetBooster))
            MiningSettingsList(30) = New Setting("BoosterShip", CStr(SentSettings.BoosterShip))
            MiningSettingsList(31) = New Setting("BoosterShipSkill", CStr(SentSettings.BoosterShipSkill))
            MiningSettingsList(32) = New Setting("MiningFormanSkill", CStr(SentSettings.MiningFormanSkill))
            MiningSettingsList(33) = New Setting("MiningDirectorSkill", CStr(SentSettings.MiningDirectorSkill))
            MiningSettingsList(34) = New Setting("CheckMineForemanLaserOpBoost", CStr(SentSettings.CheckMineForemanLaserOpBoost))
            MiningSettingsList(35) = New Setting("CheckMiningForemanMindLink", CStr(SentSettings.CheckMiningForemanMindLink))
            MiningSettingsList(36) = New Setting("CheckRorqDeployed", CStr(SentSettings.CheckRorqDeployed))
            MiningSettingsList(37) = New Setting("RefinedOre", CStr(SentSettings.RefinedOre))
            MiningSettingsList(38) = New Setting("IndustrialReconfig", CStr(SentSettings.IndustrialReconfig))
            MiningSettingsList(39) = New Setting("CheckMineForemanLaserRangeBoost", CStr(SentSettings.CheckMineForemanLaserRangeBoost))
            MiningSettingsList(40) = New Setting("GasMiningShip", CStr(SentSettings.GasMiningShip))
            MiningSettingsList(41) = New Setting("GasHarvester", CStr(SentSettings.GasHarvester))
            MiningSettingsList(42) = New Setting("NumGasHarvesters", CStr(SentSettings.NumGasHarvesters))
            MiningSettingsList(43) = New Setting("GasUpgrade", CStr(SentSettings.GasUpgrade))
            MiningSettingsList(44) = New Setting("NumGasUpgrades", CStr(SentSettings.NumGasUpgrades))
            MiningSettingsList(45) = New Setting("GasImplant", CStr(SentSettings.GasImplant))
            MiningSettingsList(46) = New Setting("CheckSovWormhole", CStr(SentSettings.CheckSovWormhole))
            MiningSettingsList(47) = New Setting("CheckSovC1", CStr(SentSettings.CheckSovC1))
            MiningSettingsList(48) = New Setting("CheckSovC2", CStr(SentSettings.CheckSovC2))
            MiningSettingsList(49) = New Setting("CheckSovC3", CStr(SentSettings.CheckSovC3))
            MiningSettingsList(50) = New Setting("CheckSovC4", CStr(SentSettings.CheckSovC4))
            MiningSettingsList(51) = New Setting("CheckSovC5", CStr(SentSettings.CheckSovC5))
            MiningSettingsList(52) = New Setting("CheckSovC6", CStr(SentSettings.CheckSovC6))
            MiningSettingsList(53) = New Setting("CompressedOre", CStr(SentSettings.CompressedOre))
            MiningSettingsList(54) = New Setting("UnrefinedOre", CStr(SentSettings.UnrefinedOre))
            MiningSettingsList(55) = New Setting("NumberofMiners", CStr(SentSettings.NumberofMiners))

            MiningSettingsList(56) = New Setting("ColumnSort", CStr(SentSettings.ColumnSort))
            MiningSettingsList(57) = New Setting("ColumnSortType", CStr(SentSettings.ColumnSortType))

            MiningSettingsList(58) = New Setting("CheckSovMoon", CStr(SentSettings.CheckSovMoon))
            MiningSettingsList(59) = New Setting("BrokerFeeRate", CStr(SentSettings.BrokerFeeRate))
            MiningSettingsList(60) = New Setting("CheckSovTriglavian", CStr(SentSettings.CheckSovTriglavian))

            MiningSettingsList(61) = New Setting("MiningDrone", CStr(SentSettings.MiningDrone))
            MiningSettingsList(62) = New Setting("NumMiningDrones", CStr(SentSettings.NumMiningDrones))
            MiningSettingsList(63) = New Setting("IceMiningDrone", CStr(SentSettings.IceMiningDrone))
            MiningSettingsList(64) = New Setting("NumIceMiningDrones", CStr(SentSettings.NumIceMiningDrones))
            MiningSettingsList(65) = New Setting("DroneOpSkill", CStr(SentSettings.DroneOpSkill))
            MiningSettingsList(66) = New Setting("DroneSpecSkill", CStr(SentSettings.DroneSpecSkill))
            MiningSettingsList(67) = New Setting("DroneInterfaceSkill", CStr(SentSettings.DroneInterfaceSkill))

            MiningSettingsList(68) = New Setting("BoosterMiningDrone", CStr(SentSettings.BoosterMiningDrone))
            MiningSettingsList(69) = New Setting("BoosterNumMiningDrones", CStr(SentSettings.BoosterNumMiningDrones))
            MiningSettingsList(70) = New Setting("BoosterIceMiningDrone", CStr(SentSettings.BoosterIceMiningDrone))
            MiningSettingsList(71) = New Setting("BoosterNumIceMiningDrones", CStr(SentSettings.BoosterNumIceMiningDrones))
            MiningSettingsList(72) = New Setting("BoosterDroneOpSkill", CStr(SentSettings.BoosterDroneOpSkill))
            MiningSettingsList(73) = New Setting("BoosterDroneSpecSkill", CStr(SentSettings.BoosterDroneSpecSkill))
            MiningSettingsList(74) = New Setting("BoosterDroneInterfaceSkill", CStr(SentSettings.BoosterDroneInterfaceSkill))

            MiningSettingsList(75) = New Setting("BoosterUseDrones", CStr(SentSettings.BoosterUseDrones))
            MiningSettingsList(76) = New Setting("ShipDroneRig1", CStr(SentSettings.ShipDroneRig1))
            MiningSettingsList(77) = New Setting("ShipDroneRig2", CStr(SentSettings.ShipDroneRig2))
            MiningSettingsList(78) = New Setting("ShipDroneRig3", CStr(SentSettings.ShipDroneRig3))
            MiningSettingsList(79) = New Setting("BoosterDroneRig1", CStr(SentSettings.BoosterDroneRig1))
            MiningSettingsList(80) = New Setting("BoosterDroneRig2", CStr(SentSettings.BoosterDroneRig2))
            MiningSettingsList(81) = New Setting("BoosterDroneRig3", CStr(SentSettings.BoosterDroneRig3))

            MiningSettingsList(82) = New Setting("ShipIceDroneRig1", CStr(SentSettings.ShipIceDroneRig1))
            MiningSettingsList(83) = New Setting("ShipIceDroneRig2", CStr(SentSettings.ShipIceDroneRig2))
            MiningSettingsList(84) = New Setting("ShipIceDroneRig3", CStr(SentSettings.ShipIceDroneRig3))
            MiningSettingsList(85) = New Setting("BoosterIceDroneRig1", CStr(SentSettings.BoosterIceDroneRig1))
            MiningSettingsList(86) = New Setting("BoosterIceDroneRig2", CStr(SentSettings.BoosterIceDroneRig2))
            MiningSettingsList(87) = New Setting("BoosterIceDroneRig3", CStr(SentSettings.BoosterIceDroneRig3))

            MiningSettingsList(88) = New Setting("IceDroneOpSkill", CStr(SentSettings.IceDroneOpSkill))
            MiningSettingsList(89) = New Setting("IceDroneSpecSkill", CStr(SentSettings.IceDroneSpecSkill))
            MiningSettingsList(90) = New Setting("IceDroneInterfaceSkill", CStr(SentSettings.IceDroneInterfaceSkill))
            MiningSettingsList(91) = New Setting("BoosterIceDroneOpSkill", CStr(SentSettings.BoosterIceDroneOpSkill))
            MiningSettingsList(92) = New Setting("BoosterIceDroneSpecSkill", CStr(SentSettings.BoosterIceDroneSpecSkill))
            MiningSettingsList(93) = New Setting("BoosterIceDroneInterfaceSkill", CStr(SentSettings.BoosterIceDroneInterfaceSkill))

            MiningSettingsList(94) = New Setting("T1Crystals", CStr(SentSettings.T1Crystals))

            MiningSettingsList(95) = New Setting("CrystalTypeA", CStr(SentSettings.CrystalTypeA))
            MiningSettingsList(96) = New Setting("CrystalTypeB", CStr(SentSettings.CrystalTypeB))
            MiningSettingsList(97) = New Setting("CrystalTypeC", CStr(SentSettings.CrystalTypeC))

            MiningSettingsList(98) = New Setting("CheckEDENCOM", CStr(SentSettings.CheckEDENCOM))
            MiningSettingsList(99) = New Setting("CheckA0Ores", CStr(SentSettings.CheckA0Ores))

            MiningSettingsList(100) = New Setting("OverrideCheck", CStr(SentSettings.OverrideCheck))
            MiningSettingsList(101) = New Setting("OverrideCycleTime", CStr(SentSettings.OverrideCycleTime))
            MiningSettingsList(102) = New Setting("OverrideLaserRange", CStr(SentSettings.OverrideLaserRange))

            Call WriteSettingsToFile(SettingsFolder, MiningSettingsFileName, MiningSettingsList, MiningSettingsFileName)

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
            If FileExists(SettingsFolder, IndustryJobsColumnSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .JobState = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobState", DefaultJobState))
                    .InstallerName = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallerName", DefaultInstallerName))
                    .TimeToComplete = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "TimeToComplete", DefaultTimeToComplete))
                    .Activity = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Activity", DefaultActivity))
                    .Status = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Status", DefaultStatus))
                    .StartTime = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "StartTime", DefaultStartTime))
                    .EndTime = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "EndTime", DefaultEndTime))
                    .CompletionTime = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "CompletionTime", DefaultCompletionTime))
                    .Blueprint = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Blueprint", DefaultBlueprint))
                    .OutputItem = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItem", DefaultOutputItem))
                    .OutputItemType = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItemType", DefaultOutputItemType))
                    .InstallSystem = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallSystem", DefaultInstallSolarSystem))
                    .InstallRegion = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallRegion", DefaultInstallRegion))
                    .LicensedRuns = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "LicensedRuns", DefaultLicensedRuns))
                    .Runs = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "Runs", DefaultRuns))
                    .SuccessfulRuns = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "SuccessfulRuns", DefaultSuccessfulRuns))
                    .BlueprintLocation = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "BlueprintLocation", DefaultBlueprintLocation))
                    .OutputLocation = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputLocation", DefaultOutputLocation))
                    .JobType = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobType", DefaultJobType))
                    .LocalCompletionDateTime = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "LocalCompletionDateTime", DefaultLocalCompletionDateTime))

                    .JobStateWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobStateWidth", DefaultIndustryColumnWidth))
                    .InstallerNameWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallerNameWidth", DefaultIndustryColumnWidth))
                    .TimeToCompleteWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "TimeToCompleteWidth", DefaultIndustryColumnWidth))
                    .ActivityWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "ActivityWidth", DefaultIndustryColumnWidth))
                    .StatusWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "StatusWidth", DefaultIndustryColumnWidth))
                    .StartTimeWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "StartTimewidth", DefaultIndustryColumnWidth))
                    .EndTimeWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "EndTimeWidth", DefaultIndustryColumnWidth))
                    .CompletionTimeWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "CompletionTimeWidth", DefaultIndustryColumnWidth))
                    .BlueprintWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "BlueprintWidth", DefaultIndustryColumnWidth))
                    .OutputItemWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItemWidth", DefaultIndustryColumnWidth))
                    .OutputItemTypeWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputItemTypeWidth", DefaultIndustryColumnWidth))
                    .InstallSystemWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallSystemWidth", DefaultIndustryColumnWidth))
                    .InstallRegionWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "InstallRegionWidth", DefaultIndustryColumnWidth))
                    .LicensedRunsWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "LiscencedRunsWidth", DefaultIndustryColumnWidth))
                    .RunsWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "RunsWidth", DefaultIndustryColumnWidth))
                    .SuccessfulRunsWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "SuccessfulRunsWidth", DefaultIndustryColumnWidth))
                    .BlueprintLocationWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "BlueprintLocationWidth", DefaultIndustryColumnWidth))
                    .OutputLocationWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OutputLocationWidth", DefaultIndustryColumnWidth))
                    .JobTypeWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "JobTypeWidth", DefaultIndustryColumnWidth))
                    .LocalCompletionDateTimeWidth = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "LocalCompletionDateTimeWidth", DefaultIndustryColumnWidth))

                    .OrderByColumn = CInt(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeInteger, IndustryJobsColumnSettingsFileName, "OrderByColumn", DefaultOrderByColumn))
                    .ViewJobType = CStr(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "ViewJobType", DefaultViewJobType))
                    .OrderType = CStr(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "OrderType", DefaultOrderType))
                    .JobTimes = CStr(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "JobTimes", DefaultJobTimes))
                    .SelectedCharacterIDs = CStr(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeString, IndustryJobsColumnSettingsFileName, "SelectedCharacterIDs", DefaultSelectedCharacterIDs))
                    .AutoUpdateJobs = CBool(GetSettingValue(SettingsFolder, IndustryJobsColumnSettingsFileName, SettingTypes.TypeBoolean, IndustryJobsColumnSettingsFileName, "AutoUpdateJobs", DefaultAutoUpdateJobs))

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
            .LocalCompletionDateTime = DefaultLocalCompletionDateTime

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
            .LocalCompletionDateTimeWidth = DefaultIndustryColumnWidth

            .OrderByColumn = DefaultOrderByColumn
            .OrderType = DefaultOrderType
            .ViewJobType = DefaultViewJobType
            .JobTimes = DefaultJobTimes

            .SelectedCharacterIDs = DefaultSelectedCharacterIDs

            .AutoUpdateJobs = DefaultAutoUpdateJobs

        End With

        ' Save locally
        IndustryJobsColumnSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIndustryJobsColumnSettings(SentSettings As IndustryJobsColumnSettings)
        Dim IndustryJobsColumnSettingsList(45) As Setting

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

            IndustryJobsColumnSettingsList(43) = New Setting("AutoUpdateJobs", CStr(SentSettings.AutoUpdateJobs))

            IndustryJobsColumnSettingsList(44) = New Setting("LocalCompletionDateTime", CStr(SentSettings.LocalCompletionDateTime))
            IndustryJobsColumnSettingsList(45) = New Setting("LocalCompletionDateTimeWidth", CStr(SentSettings.LocalCompletionDateTimeWidth))

            Call WriteSettingsToFile(SettingsFolder, IndustryJobsColumnSettingsFileName, IndustryJobsColumnSettingsList, IndustryJobsColumnSettingsFileName)

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
            If FileExists(SettingsFolder, ManufacturingTabColumnSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .ItemCategory = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemCategory", DefaultMTItemCategory))
                    .ItemGroup = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemGroup", DefaultMTItemGroup))
                    .ItemName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemName", DefaultMTItemName))
                    .Owned = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Owned", DefaultMTOwned))
                    .Tech = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Tech", DefaultMTTech))
                    .BPME = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPME", DefaultMTBPME))
                    .BPTE = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPTE", DefaultMTBPTE))
                    .Inputs = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Inputs", DefaultMTInputs))
                    .Compared = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Compared", DefaultMTCompared))
                    .TotalRuns = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalRuns", DefaultMTTotalRuns))
                    .SingleInventedBPCRuns = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SingleInventedBPCRuns", DefaultMTSingleInventedBPCRuns))
                    .ProductionLines = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ProductionLines", DefaultMTProductionLines))
                    .LaboratoryLines = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "LaboratoryLines", DefaultMTLaboratoryLines))
                    .TotalInventionCost = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalInventionCost", DefaultMTTotalInventionCost))
                    .TotalCopyCost = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalCopyCost", DefaultMTTotalCopyCost))
                    .Taxes = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Taxes", DefaultMTTaxes))
                    .BrokerFees = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BrokerFees", DefaultMTBrokerFees))
                    .BPProductionTime = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPProductionTime", DefaultMTBPProductionTime))
                    .TotalProductionTime = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalProductionTime", DefaultMTTotalProductionTime))
                    .CopyTime = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyTime", DefaultMTCopyTime))
                    .InventionTime = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionTime", DefaultMTInventionTime))
                    .ItemMarketPrice = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemMarketPrice", DefaultMTItemMarketPrice))
                    .Profit = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Profit", DefaultMTProfit))
                    .ProfitPercentage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ProfitPercentage", DefaultMTProfitPercentage))
                    .IskperHour = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "IskperHour", DefaultMTIskperHour))
                    .SVR = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SVR", DefaultMTSVR))
                    .SVRxIPH = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SVRxIPH", DefaultMTSVRxIPH))
                    .PriceTrend = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "PriceTrend", DefaultMTPriceTrend))
                    .TotalItemsSold = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalItemsSold", DefaultMTTotalItemsSold))
                    .TotalOrdersFilled = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalOrdersFilled", DefaultMTTotalOrdersFilled))
                    .AvgItemsperOrder = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "AvgItemsperOrder", DefaultMTAvgItemsperOrder))
                    .CurrentSellOrders = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CurrentSellOrders", DefaultMTCurrentSellOrders))
                    .CurrentBuyOrders = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CurrentBuyOrders", DefaultMTCurrentBuyOrders))
                    .ItemsinProduction = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemsinProduction", DefaultMTItemsinProduction))
                    .ItemsinStock = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemsinStock", DefaultMTItemsinStock))
                    .MaterialCost = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "MaterialCost", DefaultMTMaterialCost))
                    .TotalCost = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalCost", DefaultMTTotalCost))
                    .BaseJobCost = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BaseJobCost", DefaultMTBaseJobCost))
                    .NumBPs = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "NumBPs", DefaultMTNumBPs))
                    .InventionChance = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionChance", DefaultMTInventionChance))
                    .BPType = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPType", DefaultMTBPType))
                    .Race = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "Race", DefaultMTRace))
                    .VolumeperItem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "VolumeperItem", DefaultMTVolumeperItem))
                    .TotalVolume = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalVolume", DefaultMTTotalVolume))
                    .SellExcess = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SellExcess", DefaultMTSellExcess))
                    .ROI = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ROI", DefaultMTROI))
                    .PortionSize = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "PortionSize", DefaultMTPortionSize))
                    .ManufacturingJobFee = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingJobFee", DefaultMTManufacturingJobFee))
                    .ManufacturingFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityName", DefaultMTManufacturingFacilityName))
                    .ManufacturingFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilitySystem", DefaultMTManufacturingFacilitySystem))
                    .ManufacturingFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityRegion", DefaultMTManufacturingFacilityRegion))
                    .ManufacturingFacilitySystemIndex = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilitySystemIndex", DefaultMTManufacturingFacilitySystemIndex))
                    .ManufacturingFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityTax", DefaultMTManufacturingFacilityTax))
                    .ManufacturingFacilityMEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityMEBonus", DefaultMTManufacturingFacilityMEBonus))
                    .ManufacturingFacilityTEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityTEBonus", DefaultMTManufacturingFacilityTEBonus))
                    .ManufacturingFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityUsage", DefaultMTManufacturingFacilityUsage))
                    .ManufacturingFacilityFWSystemLevel = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityFWSystemLevel", DefaultMTManufacturingFacilityFWSystemLevel))
                    .ComponentFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityName", DefaultMTComponentFacilityName))
                    .ComponentFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilitySystem", DefaultMTComponentFacilitySystem))
                    .ComponentFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityRegion", DefaultMTComponentFacilityRegion))
                    .ComponentFacilitySystemIndex = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilitySystemIndex", DefaultMTComponentFacilitySystemIndex))
                    .ComponentFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityTax", DefaultMTComponentFacilityTax))
                    .ComponentFacilityMEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityMEBonus", DefaultMTComponentFacilityMEBonus))
                    .ComponentFacilityTEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityTEBonus", DefaultMTComponentFacilityTEBonus))
                    .ComponentFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityUsage", DefaultMTComponentFacilityUsage))
                    .ComponentFacilityFWSystemLevel = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityFWSystemLevel", DefaultMTComponentFacilityFWSystemLevel))
                    .CapComponentFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityName", DefaultMTCapComponentFacilityName))
                    .CapComponentFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilitySystem", DefaultMTCapComponentFacilitySystem))
                    .CapComponentFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityRegion", DefaultMTCapComponentFacilityRegion))
                    .CapComponentFacilitySystemIndex = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilitySystemIndex", DefaultMTCapComponentFacilitySystemIndex))
                    .CapComponentFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityTax", DefaultMTCapComponentFacilityTax))
                    .CapComponentFacilityMEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityMEBonus", DefaultMTCapComponentFacilityMEBonus))
                    .CapComponentFacilityTEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityTEBonus", DefaultMTCapComponentFacilityTEBonus))
                    .CapComponentFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityUsage", DefaultMTCapComponentFacilityUsage))
                    .CapComponentFacilityFWSystemLevel = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityFWSystemLevel", DefaultMTCapComponentFacilityFWSystemLevel))
                    .CopyingFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityName", DefaultMTCopyingFacilityName))
                    .CopyingFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilitySystem", DefaultMTCopyingFacilitySystem))
                    .CopyingFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityRegion", DefaultMTCopyingFacilityRegion))
                    .CopyingFacilitySystemIndex = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilitySystemIndex", DefaultMTCopyingFacilitySystemIndex))
                    .CopyingFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityTax", DefaultMTCopyingFacilityTax))
                    .CopyingFacilityMEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityMEBonus", DefaultMTCopyingFacilityMEBonus))
                    .CopyingFacilityTEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityTEBonus", DefaultMTCopyingFacilityTEBonus))
                    .CopyingFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityUsage", DefaultMTCopyingFacilityUsage))
                    .CopyingFacilityFWSystemLevel = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityFWSystemLevel", DefaultMTCopyingFacilityFWSystemLevel))
                    .InventionFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityName", DefaultMTInventionFacilityName))
                    .InventionFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilitySystem", DefaultMTInventionFacilitySystem))
                    .InventionFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityRegion", DefaultMTInventionFacilityRegion))
                    .InventionFacilitySystemIndex = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilitySystemIndex", DefaultMTInventionFacilitySystemIndex))
                    .InventionFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityTax", DefaultMTInventionFacilityTax))
                    .InventionFacilityMEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityMEBonus", DefaultMTInventionFacilityMEBonus))
                    .InventionFacilityTEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityTEBonus", DefaultMTInventionFacilityTEBonus))
                    .InventionFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityUsage", DefaultMTInventionFacilityUsage))
                    .InventionFacilityFWSystemLevel = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityFWSystemLevel", DefaultMTInventionFacilityFWSystemLevel))
                    .ReactionFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityName", DefaultMTReactionFacilityName))
                    .ReactionFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilitySystem", DefaultMTReactionFacilitySystem))
                    .ReactionFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityRegion", DefaultMTReactionFacilityRegion))
                    .ReactionFacilitySystemIndex = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilitySystemIndex", DefaultMTReactionFacilitySystemIndex))
                    .ReactionFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityTax", DefaultMTReactionFacilityTax))
                    .ReactionFacilityMEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityMEBonus", DefaultMTReactionFacilityMEBonus))
                    .ReactionFacilityTEBonus = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityTEBonus", DefaultMTReactionFacilityTEBonus))
                    .ReactionFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityUsage", DefaultMTReactionFacilityUsage))
                    .ReactionFacilityFWSystemLevel = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityFWSystemLevel", DefaultMTReactionFacilityFWSystemLevel))
                    .ReprocessingFacilityName = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityName", DefaultMTReprocessingFacilityName))
                    .ReprocessingFacilitySystem = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilitySystem", DefaultMTReprocessingFacilitySystem))
                    .ReprocessingFacilityRegion = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityRegion", DefaultMTReprocessingFacilityRegion))
                    .ReprocessingFacilityTax = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityTax", DefaultMTReprocessingFacilityTax))
                    .ReprocessingFacilityUsage = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityUsage", DefaultMTReprocessingFacilityUsage))
                    .ReprocessingFacilityOreRefineRate = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityOreRefineRate", DefaultMTReprocessingFacilityOreRefineRate))
                    .ReprocessingFacilityIceRefineRate = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityIceRefineRate", DefaultMTReprocessingFacilityIceRefineRate))
                    .ReprocessingFacilityMoonRefineRate = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityMoonRefineRate", DefaultMTReprocessingFacilityMoonRefineRate))

                    .ItemCategoryWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemCategoryWidth", DefaultMTItemCategoryWidth))
                    .ItemGroupWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemGroupWidth", DefaultMTItemGroupWidth))
                    .ItemNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemNameWidth", DefaultMTItemNameWidth))
                    .OwnedWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "OwnedWidth", DefaultMTOwnedWidth))
                    .TechWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TechWidth", DefaultMTTechWidth))
                    .BPMEWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPMEWidth", DefaultMTBPMEWidth))
                    .BPTEWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPTEWidth", DefaultMTBPTEWidth))
                    .InputsWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InputsWidth", DefaultMTInputsWidth))
                    .ComparedWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComparedWidth", DefaultMTComparedWidth))
                    .TotalRunsWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalRunsWidth", DefaultMTTotalRunsWidth))
                    .SingleInventedBPCRunsWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SingleInventedBPCRunsWidth", DefaultMTSingleInventedBPCRunsWidth))
                    .ProductionLinesWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ProductionLinesWidth", DefaultMTProductionLinesWidth))
                    .LaboratoryLinesWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "LaboratoryLinesWidth", DefaultMTLaboratoryLinesWidth))
                    .TotalInventionCostWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalInventionCostWidth", DefaultMTTotalInventionCostWidth))
                    .TotalCopyCostWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalCopyCostWidth", DefaultMTTotalCopyCostWidth))
                    .TaxesWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TaxesWidth", DefaultMTTaxesWidth))
                    .BrokerFeesWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BrokerFeesWidth", DefaultMTBrokerFeesWidth))
                    .BPProductionTimeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPProductionTimeWidth", DefaultMTBPProductionTimeWidth))
                    .TotalProductionTimeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalProductionTimeWidth", DefaultMTTotalProductionTimeWidth))
                    .CopyTimeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyTimeWidth", DefaultMTCopyTimeWidth))
                    .InventionTimeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionTimeWidth", DefaultMTInventionTimeWidth))
                    .ItemMarketPriceWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemMarketPriceWidth", DefaultMTItemMarketPriceWidth))
                    .ProfitWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ProfitWidth", DefaultMTProfitWidth))
                    .ProfitPercentageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ProfitPercentageWidth", DefaultMTProfitPercentageWidth))
                    .IskperHourWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "IskperHourWidth", DefaultMTIskperHourWidth))
                    .SVRWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SVRWidth", DefaultMTSVRWidth))
                    .SVRxIPHWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SVRxIPHWidth", DefaultMTSVRxIPHWidth))
                    .PriceTrendWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "PriceTrendWidth", DefaultMTPriceTrendWidth))
                    .TotalItemsSoldWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalItemsSoldWidth", DefaultMTTotalItemsSoldWidth))
                    .TotalOrdersFilledWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalOrdersFilledWidth", DefaultMTTotalOrdersFilledWidth))
                    .AvgItemsperOrderWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "AvgItemsperOrderWidth", DefaultMTAvgItemsperOrderWidth))
                    .CurrentSellOrdersWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CurrentSellOrdersWidth", DefaultMTCurrentSellOrdersWidth))
                    .CurrentBuyOrdersWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CurrentBuyOrdersWidth", DefaultMTCurrentBuyOrdersWidth))
                    .ItemsinProductionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemsinProductionWidth", DefaultMTItemsinProductionWidth))
                    .ItemsinStockWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ItemsinStockWidth", DefaultMTItemsinStockWidth))
                    .MaterialCostWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "MaterialCostWidth", DefaultMTMaterialCostWidth))
                    .TotalCostWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalCostWidth", DefaultMTTotalCostWidth))
                    .BaseJobCostWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BaseJobCostWidth", DefaultMTBaseJobCostWidth))
                    .NumBPsWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "NumBPsWidth", DefaultMTNumBPsWidth))
                    .InventionChanceWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionChanceWidth", DefaultMTInventionChanceWidth))
                    .BPTypeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "BPTypeWidth", DefaultMTBPTypeWidth))
                    .RaceWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "RaceWidth", DefaultMTRaceWidth))
                    .VolumeperItemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "VolumeperItemWidth", DefaultMTVolumeperItemWidth))
                    .TotalVolumeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "TotalVolumeWidth", DefaultMTTotalVolumeWidth))
                    .SellExcessWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "SellExcessWidth", DefaultMTSellExcessWidth))
                    .ROIWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ROIWidth", DefaultMTROIWidth))
                    .PortionSizeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "PortionSizeWidth", DefaultMTPortionSizeWidth))
                    .ManufacturingJobFeeWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingJobFeeWidth", DefaultMTManufacturingJobFeeWidth))
                    .ManufacturingFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityNameWidth", DefaultMTManufacturingFacilityNameWidth))
                    .ManufacturingFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilitySystemWidth", DefaultMTManufacturingFacilitySystemWidth))
                    .ManufacturingFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityRegionWidth", DefaultMTManufacturingFacilityRegionWidth))
                    .ManufacturingFacilitySystemIndexWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilitySystemIndexWidth", DefaultMTManufacturingFacilitySystemIndexWidth))
                    .ManufacturingFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityTaxWidth", DefaultMTManufacturingFacilityTaxWidth))
                    .ManufacturingFacilityMEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityMEBonusWidth", DefaultMTManufacturingFacilityMEBonusWidth))
                    .ManufacturingFacilityTEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityTEBonusWidth", DefaultMTManufacturingFacilityTEBonusWidth))
                    .ManufacturingFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityUsageWidth", DefaultMTManufacturingFacilityUsageWidth))
                    .ManufacturingFacilityFWSystemLevelWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ManufacturingFacilityFWSystemLevelWidth", DefaultMTManufacturingFacilityFWSystemLevelWidth))
                    .ComponentFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityNameWidth", DefaultMTComponentFacilityNameWidth))
                    .ComponentFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilitySystemWidth", DefaultMTComponentFacilitySystemWidth))
                    .ComponentFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityRegionWidth", DefaultMTComponentFacilityRegionWidth))
                    .ComponentFacilitySystemIndexWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilitySystemIndexWidth", DefaultMTComponentFacilitySystemIndexWidth))
                    .ComponentFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityTaxWidth", DefaultMTComponentFacilityTaxWidth))
                    .ComponentFacilityMEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityMEBonusWidth", DefaultMTComponentFacilityMEBonusWidth))
                    .ComponentFacilityTEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityTEBonusWidth", DefaultMTComponentFacilityTEBonusWidth))
                    .ComponentFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityUsageWidth", DefaultMTComponentFacilityUsageWidth))
                    .ComponentFacilityFWSystemLevelWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ComponentFacilityFWSystemLevelWidth", DefaultMTComponentFacilityFWSystemLevelWidth))
                    .CapComponentFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityNameWidth", DefaultMTCapComponentFacilityNameWidth))
                    .CapComponentFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilitySystemWidth", DefaultMTCapComponentFacilitySystemWidth))
                    .CapComponentFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityRegionWidth", DefaultMTCapComponentFacilityRegionWidth))
                    .CapComponentFacilitySystemIndexWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilitySystemIndexWidth", DefaultMTCapComponentFacilitySystemIndexWidth))
                    .CapComponentFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityTaxWidth", DefaultMTCapComponentFacilityTaxWidth))
                    .CapComponentFacilityMEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityMEBonusWidth", DefaultMTCapComponentFacilityMEBonusWidth))
                    .CapComponentFacilityTEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityTEBonusWidth", DefaultMTCapComponentFacilityTEBonusWidth))
                    .CapComponentFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityUsageWidth", DefaultMTCapComponentFacilityUsageWidth))
                    .CapComponentFacilityFWSystemLevelWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CapComponentFacilityFWSystemLevelWidth", DefaultMTCapComponentFacilityFWSystemLevelWidth))
                    .CopyingFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityNameWidth", DefaultMTCopyingFacilityNameWidth))
                    .CopyingFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilitySystemWidth", DefaultMTCopyingFacilitySystemWidth))
                    .CopyingFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityRegionWidth", DefaultMTCopyingFacilityRegionWidth))
                    .CopyingFacilitySystemIndexWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilitySystemIndexWidth", DefaultMTCopyingFacilitySystemIndexWidth))
                    .CopyingFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityTaxWidth", DefaultMTCopyingFacilityTaxWidth))
                    .CopyingFacilityMEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityMEBonusWidth", DefaultMTCopyingFacilityMEBonusWidth))
                    .CopyingFacilityTEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityTEBonusWidth", DefaultMTCopyingFacilityTEBonusWidth))
                    .CopyingFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityUsageWidth", DefaultMTCopyingFacilityUsageWidth))
                    .CopyingFacilityFWSystemLevelWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "CopyingFacilityFWSystemLevelWidth", DefaultMTCopyingFacilityFWSystemLevelWidth))
                    .InventionFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityNameWidth", DefaultMTInventionFacilityNameWidth))
                    .InventionFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilitySystemWidth", DefaultMTInventionFacilitySystemWidth))
                    .InventionFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityRegionWidth", DefaultMTInventionFacilityRegionWidth))
                    .InventionFacilitySystemIndexWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilitySystemIndexWidth", DefaultMTInventionFacilitySystemIndexWidth))
                    .InventionFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityTaxWidth", DefaultMTInventionFacilityTaxWidth))
                    .InventionFacilityMEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityMEBonusWidth", DefaultMTInventionFacilityMEBonusWidth))
                    .InventionFacilityTEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityTEBonusWidth", DefaultMTInventionFacilityTEBonusWidth))
                    .InventionFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityUsageWidth", DefaultMTInventionFacilityUsageWidth))
                    .InventionFacilityFWSystemLevelWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "InventionFacilityFWSystemLevelWidth", DefaultMTInventionFacilityFWSystemLevelWidth))
                    .ReactionFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityNameWidth", DefaultMTReactionFacilityNameWidth))
                    .ReactionFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilitySystemWidth", DefaultMTReactionFacilitySystemWidth))
                    .ReactionFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityRegionWidth", DefaultMTReactionFacilityRegionWidth))
                    .ReactionFacilitySystemIndexWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilitySystemIndexWidth", DefaultMTReactionFacilitySystemIndexWidth))
                    .ReactionFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityTaxWidth", DefaultMTReactionFacilityTaxWidth))
                    .ReactionFacilityMEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityMEBonusWidth", DefaultMTReactionFacilityMEBonusWidth))
                    .ReactionFacilityTEBonusWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityTEBonusWidth", DefaultMTReactionFacilityTEBonusWidth))
                    .ReactionFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityUsageWidth", DefaultMTReactionFacilityUsageWidth))
                    .ReactionFacilityFWSystemLevelWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReactionFacilityFWSystemLevelWidth", DefaultMTReactionFacilityFWSystemLevelWidth))
                    .ReprocessingFacilityNameWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityNameWidth", DefaultMTReprocessingFacilityNameWidth))
                    .ReprocessingFacilitySystemWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilitySystemWidth", DefaultMTReprocessingFacilitySystemWidth))
                    .ReprocessingFacilityRegionWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityRegionWidth", DefaultMTReprocessingFacilityRegionWidth))
                    .ReprocessingFacilityTaxWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityTaxWidth", DefaultMTReprocessingFacilityTaxWidth))
                    .ReprocessingFacilityUsageWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityUsageWidth", DefaultMTReprocessingFacilityUsageWidth))
                    .ReprocessingFacilityOreRefineRateWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityOreRefineRateWidth", DefaultMTReprocessingFacilityOreRefineRateWidth))
                    .ReprocessingFacilityIceRefineRateWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityIceRefineRateWidth", DefaultMTReprocessingFacilityIceRefineRateWidth))
                    .ReprocessingFacilityMoonRefineRateWidth = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, "ManufacturingTabColumnSettings", "ReprocessingFacilityMoonRefineRateWidth", DefaultMTReprocessingFacilityMoonRefineRateWidth))

                    .OrderByColumn = CInt(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeInteger, ManufacturingTabColumnSettingsFileName, "OrderByColumn", DefaultMTOrderByColumn))
                    .OrderType = CStr(GetSettingValue(SettingsFolder, ManufacturingTabColumnSettingsFileName, SettingTypes.TypeString, ManufacturingTabColumnSettingsFileName, "OrderType", DefaultMTOrderType))

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
            .PriceTrend = DefaultMTPriceTrend
            .TotalItemsSold = DefaultMTTotalItemsSold
            .TotalOrdersFilled = DefaultMTTotalOrdersFilled
            .AvgItemsperOrder = DefaultMTAvgItemsperOrder
            .CurrentSellOrders = DefaultMTCurrentSellOrders
            .CurrentBuyOrders = DefaultMTCurrentBuyOrders
            .ItemsinProduction = DefaultMTItemsinProduction
            .ItemsinStock = DefaultMTItemsinStock
            .MaterialCost = DefaultMTMaterialCost
            .TotalCost = DefaultMTTotalCost
            .BaseJobCost = DefaultMTBaseJobCost
            .NumBPs = DefaultMTNumBPs
            .InventionChance = DefaultMTInventionChance
            .BPType = DefaultMTBPType
            .Race = DefaultMTRace
            .VolumeperItem = DefaultMTVolumeperItem
            .TotalVolume = DefaultMTTotalVolume
            .SellExcess = DefaultMTSellExcess
            .ROI = DefaultMTROI
            .PortionSize = DefaultMTPortionSize
            .ManufacturingJobFee = DefaultMTManufacturingJobFee
            .ManufacturingFacilityName = DefaultMTManufacturingFacilityName
            .ManufacturingFacilitySystem = DefaultMTManufacturingFacilitySystem
            .ManufacturingFacilityRegion = DefaultMTManufacturingFacilityRegion
            .ManufacturingFacilitySystemIndex = DefaultMTManufacturingFacilitySystemIndex
            .ManufacturingFacilityTax = DefaultMTManufacturingFacilityTax
            .ManufacturingFacilityMEBonus = DefaultMTManufacturingFacilityMEBonus
            .ManufacturingFacilityTEBonus = DefaultMTManufacturingFacilityTEBonus
            .ManufacturingFacilityUsage = DefaultMTManufacturingFacilityUsage
            .ManufacturingFacilityFWSystemLevel = DefaultMTManufacturingFacilityFWSystemLevel
            .ComponentFacilityName = DefaultMTComponentFacilityName
            .ComponentFacilitySystem = DefaultMTComponentFacilitySystem
            .ComponentFacilityRegion = DefaultMTComponentFacilityRegion
            .ComponentFacilitySystemIndex = DefaultMTComponentFacilitySystemIndex
            .ComponentFacilityTax = DefaultMTComponentFacilityTax
            .ComponentFacilityMEBonus = DefaultMTComponentFacilityMEBonus
            .ComponentFacilityTEBonus = DefaultMTComponentFacilityTEBonus
            .ComponentFacilityUsage = DefaultMTComponentFacilityUsage
            .ComponentFacilityFWSystemLevel = DefaultMTComponentFacilityFWSystemLevel
            .CapComponentFacilityName = DefaultMTCapComponentFacilityName
            .CapComponentFacilitySystem = DefaultMTCapComponentFacilitySystem
            .CapComponentFacilityRegion = DefaultMTCapComponentFacilityRegion
            .CapComponentFacilitySystemIndex = DefaultMTCapComponentFacilitySystemIndex
            .CapComponentFacilityTax = DefaultMTCapComponentFacilityTax
            .CapComponentFacilityMEBonus = DefaultMTCapComponentFacilityMEBonus
            .CapComponentFacilityTEBonus = DefaultMTCapComponentFacilityTEBonus
            .CapComponentFacilityUsage = DefaultMTCapComponentFacilityUsage
            .CapComponentFacilityFWSystemLevel = DefaultMTCapComponentFacilityFWSystemLevel
            .CopyingFacilityName = DefaultMTCopyingFacilityName
            .CopyingFacilitySystem = DefaultMTCopyingFacilitySystem
            .CopyingFacilityRegion = DefaultMTCopyingFacilityRegion
            .CopyingFacilitySystemIndex = DefaultMTCopyingFacilitySystemIndex
            .CopyingFacilityTax = DefaultMTCopyingFacilityTax
            .CopyingFacilityMEBonus = DefaultMTCopyingFacilityMEBonus
            .CopyingFacilityTEBonus = DefaultMTCopyingFacilityTEBonus
            .CopyingFacilityUsage = DefaultMTCopyingFacilityUsage
            .CopyingFacilityFWSystemLevel = DefaultMTCopyingFacilityFWSystemLevel
            .InventionFacilityName = DefaultMTInventionFacilityName
            .InventionFacilitySystem = DefaultMTInventionFacilitySystem
            .InventionFacilityRegion = DefaultMTInventionFacilityRegion
            .InventionFacilitySystemIndex = DefaultMTInventionFacilitySystemIndex
            .InventionFacilityTax = DefaultMTInventionFacilityTax
            .InventionFacilityMEBonus = DefaultMTInventionFacilityMEBonus
            .InventionFacilityTEBonus = DefaultMTInventionFacilityTEBonus
            .InventionFacilityUsage = DefaultMTInventionFacilityUsage
            .InventionFacilityFWSystemLevel = DefaultMTInventionFacilityFWSystemLevel
            .ReactionFacilityName = DefaultMTReactionFacilityName
            .ReactionFacilitySystem = DefaultMTReactionFacilitySystem
            .ReactionFacilityRegion = DefaultMTReactionFacilityRegion
            .ReactionFacilitySystemIndex = DefaultMTReactionFacilitySystemIndex
            .ReactionFacilityTax = DefaultMTReactionFacilityTax
            .ReactionFacilityMEBonus = DefaultMTReactionFacilityMEBonus
            .ReactionFacilityTEBonus = DefaultMTReactionFacilityTEBonus
            .ReactionFacilityUsage = DefaultMTReactionFacilityUsage
            .ReactionFacilityFWSystemLevel = DefaultMTReactionFacilityFWSystemLevel
            .ReprocessingFacilityName = DefaultMTReprocessingFacilityName
            .ReprocessingFacilitySystem = DefaultMTReprocessingFacilitySystem
            .ReprocessingFacilityRegion = DefaultMTReprocessingFacilityRegion
            .ReprocessingFacilityTax = DefaultMTReprocessingFacilityTax
            .ReprocessingFacilityUsage = DefaultMTReprocessingFacilityUsage
            .ReprocessingFacilityOreRefineRate = DefaultMTReprocessingFacilityOreRefineRate
            .ReprocessingFacilityIceRefineRate = DefaultMTReprocessingFacilityIceRefineRate
            .ReprocessingFacilityMoonRefineRate = DefaultMTReprocessingFacilityMoonRefineRate

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
            .PriceTrendWidth = DefaultMTPriceTrendWidth
            .TotalItemsSoldWidth = DefaultMTTotalItemsSoldWidth
            .TotalOrdersFilledWidth = DefaultMTTotalOrdersFilledWidth
            .AvgItemsperOrderWidth = DefaultMTAvgItemsperOrderWidth
            .CurrentSellOrdersWidth = DefaultMTCurrentSellOrdersWidth
            .CurrentBuyOrdersWidth = DefaultMTCurrentBuyOrdersWidth
            .ItemsinProductionWidth = DefaultMTItemsinProductionWidth
            .ItemsinStockWidth = DefaultMTItemsinStockWidth
            .MaterialCostWidth = DefaultMTMaterialCostWidth
            .TotalCostWidth = DefaultMTTotalCostWidth
            .BaseJobCostWidth = DefaultMTBaseJobCostWidth
            .NumBPsWidth = DefaultMTNumBPsWidth
            .InventionChanceWidth = DefaultMTInventionChanceWidth
            .BPTypeWidth = DefaultMTBPTypeWidth
            .RaceWidth = DefaultMTRaceWidth
            .VolumeperItemWidth = DefaultMTVolumeperItemWidth
            .TotalVolumeWidth = DefaultMTTotalVolumeWidth
            .SellExcessWidth = DefaultMTSellExcessWidth
            .ROIWidth = DefaultMTROIWidth
            .PortionSizeWidth = DefaultMTPortionSizeWidth
            .ManufacturingJobFeeWidth = DefaultMTManufacturingJobFeeWidth
            .ManufacturingFacilityNameWidth = DefaultMTManufacturingFacilityNameWidth
            .ManufacturingFacilitySystemWidth = DefaultMTManufacturingFacilitySystemWidth
            .ManufacturingFacilityRegionWidth = DefaultMTManufacturingFacilityRegionWidth
            .ManufacturingFacilitySystemIndexWidth = DefaultMTManufacturingFacilitySystemIndexWidth
            .ManufacturingFacilityTaxWidth = DefaultMTManufacturingFacilityTaxWidth
            .ManufacturingFacilityMEBonusWidth = DefaultMTManufacturingFacilityMEBonusWidth
            .ManufacturingFacilityTEBonusWidth = DefaultMTManufacturingFacilityTEBonusWidth
            .ManufacturingFacilityUsageWidth = DefaultMTManufacturingFacilityUsageWidth
            .ManufacturingFacilityFWSystemLevelWidth = DefaultMTManufacturingFacilityFWSystemLevelWidth
            .ComponentFacilityNameWidth = DefaultMTComponentFacilityNameWidth
            .ComponentFacilitySystemWidth = DefaultMTComponentFacilitySystemWidth
            .ComponentFacilityRegionWidth = DefaultMTComponentFacilityRegionWidth
            .ComponentFacilitySystemIndexWidth = DefaultMTComponentFacilitySystemIndexWidth
            .ComponentFacilityTaxWidth = DefaultMTComponentFacilityTaxWidth
            .ComponentFacilityMEBonusWidth = DefaultMTComponentFacilityMEBonusWidth
            .ComponentFacilityTEBonusWidth = DefaultMTComponentFacilityTEBonusWidth
            .ComponentFacilityUsageWidth = DefaultMTComponentFacilityUsageWidth
            .ComponentFacilityFWSystemLevelWidth = DefaultMTComponentFacilityFWSystemLevelWidth
            .CapComponentFacilityNameWidth = DefaultMTCapComponentFacilityNameWidth
            .CapComponentFacilitySystemWidth = DefaultMTCapComponentFacilitySystemWidth
            .CapComponentFacilityRegionWidth = DefaultMTCapComponentFacilityRegionWidth
            .CapComponentFacilitySystemIndexWidth = DefaultMTCapComponentFacilitySystemIndexWidth
            .CapComponentFacilityTaxWidth = DefaultMTCapComponentFacilityTaxWidth
            .CapComponentFacilityMEBonusWidth = DefaultMTCapComponentFacilityMEBonusWidth
            .CapComponentFacilityTEBonusWidth = DefaultMTCapComponentFacilityTEBonusWidth
            .CapComponentFacilityUsageWidth = DefaultMTCapComponentFacilityUsageWidth
            .CapComponentFacilityFWSystemLevelWidth = DefaultMTCapComponentFacilityFWSystemLevelWidth
            .CopyingFacilityNameWidth = DefaultMTCopyingFacilityNameWidth
            .CopyingFacilitySystemWidth = DefaultMTCopyingFacilitySystemWidth
            .CopyingFacilityRegionWidth = DefaultMTCopyingFacilityRegionWidth
            .CopyingFacilitySystemIndexWidth = DefaultMTCopyingFacilitySystemIndexWidth
            .CopyingFacilityTaxWidth = DefaultMTCopyingFacilityTaxWidth
            .CopyingFacilityMEBonusWidth = DefaultMTCopyingFacilityMEBonusWidth
            .CopyingFacilityTEBonusWidth = DefaultMTCopyingFacilityTEBonusWidth
            .CopyingFacilityUsageWidth = DefaultMTCopyingFacilityUsageWidth
            .CopyingFacilityFWSystemLevelWidth = DefaultMTCopyingFacilityFWSystemLevelWidth
            .InventionFacilityNameWidth = DefaultMTInventionFacilityNameWidth
            .InventionFacilitySystemWidth = DefaultMTInventionFacilitySystemWidth
            .InventionFacilityRegionWidth = DefaultMTInventionFacilityRegionWidth
            .InventionFacilitySystemIndexWidth = DefaultMTInventionFacilitySystemIndexWidth
            .InventionFacilityTaxWidth = DefaultMTInventionFacilityTaxWidth
            .InventionFacilityMEBonusWidth = DefaultMTInventionFacilityMEBonusWidth
            .InventionFacilityTEBonusWidth = DefaultMTInventionFacilityTEBonusWidth
            .InventionFacilityUsageWidth = DefaultMTInventionFacilityUsageWidth
            .InventionFacilityFWSystemLevelWidth = DefaultMTInventionFacilityFWSystemLevelWidth
            .ReactionFacilityNameWidth = DefaultMTReactionFacilityNameWidth
            .ReactionFacilitySystemWidth = DefaultMTReactionFacilitySystemWidth
            .ReactionFacilityRegionWidth = DefaultMTReactionFacilityRegionWidth
            .ReactionFacilitySystemIndexWidth = DefaultMTReactionFacilitySystemIndexWidth
            .ReactionFacilityTaxWidth = DefaultMTReactionFacilityTaxWidth
            .ReactionFacilityMEBonusWidth = DefaultMTReactionFacilityMEBonusWidth
            .ReactionFacilityTEBonusWidth = DefaultMTReactionFacilityTEBonusWidth
            .ReactionFacilityUsageWidth = DefaultMTReactionFacilityUsageWidth
            .ReactionFacilityFWSystemLevelWidth = DefaultMTReactionFacilityFWSystemLevelWidth
            .ReprocessingFacilityNameWidth = DefaultMTReprocessingFacilityNameWidth
            .ReprocessingFacilitySystemWidth = DefaultMTReprocessingFacilitySystemWidth
            .ReprocessingFacilityRegionWidth = DefaultMTReprocessingFacilityRegionWidth
            .ReprocessingFacilityTaxWidth = DefaultMTReprocessingFacilityTaxWidth
            .ReprocessingFacilityUsageWidth = DefaultMTReprocessingFacilityUsageWidth
            .ReprocessingFacilityOreRefineRateWidth = DefaultMTReprocessingFacilityOreRefineRateWidth
            .ReprocessingFacilityIceRefineRateWidth = DefaultMTReprocessingFacilityIceRefineRateWidth
            .ReprocessingFacilityMoonRefineRateWidth = DefaultMTReprocessingFacilityMoonRefineRateWidth

            .OrderByColumn = DefaultMTOrderByColumn
            .OrderType = DefaultMTOrderType

        End With

        ' save locally
        ManufacturingTabColumnSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveManufacturingTabColumnSettings(SentSettings As ManufacturingTabColumnSettings)
        Dim ManufacturingTabColumnSettingsList(221) As Setting

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
            ManufacturingTabColumnSettingsList(15) = New Setting("Taxes", CStr(SentSettings.Taxes))
            ManufacturingTabColumnSettingsList(16) = New Setting("BrokerFees", CStr(SentSettings.BrokerFees))
            ManufacturingTabColumnSettingsList(17) = New Setting("BPProductionTime", CStr(SentSettings.BPProductionTime))
            ManufacturingTabColumnSettingsList(18) = New Setting("TotalProductionTime", CStr(SentSettings.TotalProductionTime))
            ManufacturingTabColumnSettingsList(19) = New Setting("CopyTime", CStr(SentSettings.CopyTime))
            ManufacturingTabColumnSettingsList(20) = New Setting("InventionTime", CStr(SentSettings.InventionTime))
            ManufacturingTabColumnSettingsList(21) = New Setting("ItemMarketPrice", CStr(SentSettings.ItemMarketPrice))
            ManufacturingTabColumnSettingsList(22) = New Setting("Profit", CStr(SentSettings.Profit))
            ManufacturingTabColumnSettingsList(23) = New Setting("ProfitPercentage", CStr(SentSettings.ProfitPercentage))
            ManufacturingTabColumnSettingsList(24) = New Setting("IskperHour", CStr(SentSettings.IskperHour))
            ManufacturingTabColumnSettingsList(25) = New Setting("SVR", CStr(SentSettings.SVR))
            ManufacturingTabColumnSettingsList(26) = New Setting("SVRxIPH", CStr(SentSettings.SVRxIPH))
            ManufacturingTabColumnSettingsList(27) = New Setting("PriceTrend", CStr(SentSettings.PriceTrend))
            ManufacturingTabColumnSettingsList(28) = New Setting("TotalItemsSold", CStr(SentSettings.TotalItemsSold))
            ManufacturingTabColumnSettingsList(29) = New Setting("TotalOrdersFilled", CStr(SentSettings.TotalOrdersFilled))
            ManufacturingTabColumnSettingsList(30) = New Setting("AvgItemsperOrder", CStr(SentSettings.AvgItemsperOrder))
            ManufacturingTabColumnSettingsList(31) = New Setting("CurrentSellOrders", CStr(SentSettings.CurrentSellOrders))
            ManufacturingTabColumnSettingsList(32) = New Setting("CurrentBuyOrders", CStr(SentSettings.CurrentBuyOrders))
            ManufacturingTabColumnSettingsList(33) = New Setting("ItemsinProduction", CStr(SentSettings.ItemsinProduction))
            ManufacturingTabColumnSettingsList(34) = New Setting("ItemsinStock", CStr(SentSettings.ItemsinStock))
            ManufacturingTabColumnSettingsList(35) = New Setting("MaterialCost", CStr(SentSettings.MaterialCost))
            ManufacturingTabColumnSettingsList(36) = New Setting("TotalCost", CStr(SentSettings.TotalCost))
            ManufacturingTabColumnSettingsList(37) = New Setting("BaseJobCost", CStr(SentSettings.BaseJobCost))
            ManufacturingTabColumnSettingsList(38) = New Setting("NumBPs", CStr(SentSettings.NumBPs))
            ManufacturingTabColumnSettingsList(39) = New Setting("InventionChance", CStr(SentSettings.InventionChance))
            ManufacturingTabColumnSettingsList(40) = New Setting("BPType", CStr(SentSettings.BPType))
            ManufacturingTabColumnSettingsList(41) = New Setting("Race", CStr(SentSettings.Race))
            ManufacturingTabColumnSettingsList(42) = New Setting("VolumeperItem", CStr(SentSettings.VolumeperItem))
            ManufacturingTabColumnSettingsList(43) = New Setting("TotalVolume", CStr(SentSettings.TotalVolume))
            ManufacturingTabColumnSettingsList(44) = New Setting("SellExcess", CStr(SentSettings.SellExcess))
            ManufacturingTabColumnSettingsList(45) = New Setting("ROI", CStr(SentSettings.ROI))
            ManufacturingTabColumnSettingsList(46) = New Setting("PortionSize", CStr(SentSettings.PortionSize))
            ManufacturingTabColumnSettingsList(47) = New Setting("ManufacturingJobFee", CStr(SentSettings.ManufacturingJobFee))
            ManufacturingTabColumnSettingsList(48) = New Setting("ManufacturingFacilityName", CStr(SentSettings.ManufacturingFacilityName))
            ManufacturingTabColumnSettingsList(49) = New Setting("ManufacturingFacilitySystem", CStr(SentSettings.ManufacturingFacilitySystem))
            ManufacturingTabColumnSettingsList(50) = New Setting("ManufacturingFacilityRegion", CStr(SentSettings.ManufacturingFacilityRegion))
            ManufacturingTabColumnSettingsList(51) = New Setting("ManufacturingFacilitySystemIndex", CStr(SentSettings.ManufacturingFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(52) = New Setting("ManufacturingFacilityTax", CStr(SentSettings.ManufacturingFacilityTax))
            ManufacturingTabColumnSettingsList(53) = New Setting("ManufacturingFacilityMEBonus", CStr(SentSettings.ManufacturingFacilityMEBonus))
            ManufacturingTabColumnSettingsList(54) = New Setting("ManufacturingFacilityTEBonus", CStr(SentSettings.ManufacturingFacilityTEBonus))
            ManufacturingTabColumnSettingsList(55) = New Setting("ManufacturingFacilityUsage", CStr(SentSettings.ManufacturingFacilityUsage))
            ManufacturingTabColumnSettingsList(56) = New Setting("ManufacturingFacilityFWSystemLevel", CStr(SentSettings.ManufacturingFacilityFWSystemLevel))
            ManufacturingTabColumnSettingsList(57) = New Setting("ComponentFacilityName", CStr(SentSettings.ComponentFacilityName))
            ManufacturingTabColumnSettingsList(58) = New Setting("ComponentFacilitySystem", CStr(SentSettings.ComponentFacilitySystem))
            ManufacturingTabColumnSettingsList(59) = New Setting("ComponentFacilityRegion", CStr(SentSettings.ComponentFacilityRegion))
            ManufacturingTabColumnSettingsList(60) = New Setting("ComponentFacilitySystemIndex", CStr(SentSettings.ComponentFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(61) = New Setting("ComponentFacilityTax", CStr(SentSettings.ComponentFacilityTax))
            ManufacturingTabColumnSettingsList(62) = New Setting("ComponentFacilityMEBonus", CStr(SentSettings.ComponentFacilityMEBonus))
            ManufacturingTabColumnSettingsList(63) = New Setting("ComponentFacilityTEBonus", CStr(SentSettings.ComponentFacilityTEBonus))
            ManufacturingTabColumnSettingsList(64) = New Setting("ComponentFacilityUsage", CStr(SentSettings.ComponentFacilityUsage))
            ManufacturingTabColumnSettingsList(65) = New Setting("ComponentFacilityFWSystemLevel", CStr(SentSettings.ComponentFacilityFWSystemLevel))
            ManufacturingTabColumnSettingsList(66) = New Setting("CapComponentFacilityName", CStr(SentSettings.CapComponentFacilityName))
            ManufacturingTabColumnSettingsList(67) = New Setting("CapComponentFacilitySystem", CStr(SentSettings.CapComponentFacilitySystem))
            ManufacturingTabColumnSettingsList(68) = New Setting("CapComponentFacilityRegion", CStr(SentSettings.CapComponentFacilityRegion))
            ManufacturingTabColumnSettingsList(69) = New Setting("CapComponentFacilitySystemIndex", CStr(SentSettings.CapComponentFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(70) = New Setting("CapComponentFacilityTax", CStr(SentSettings.CapComponentFacilityTax))
            ManufacturingTabColumnSettingsList(71) = New Setting("CapComponentFacilityMEBonus", CStr(SentSettings.CapComponentFacilityMEBonus))
            ManufacturingTabColumnSettingsList(72) = New Setting("CapComponentFacilityTEBonus", CStr(SentSettings.CapComponentFacilityTEBonus))
            ManufacturingTabColumnSettingsList(73) = New Setting("CapComponentFacilityUsage", CStr(SentSettings.CapComponentFacilityUsage))
            ManufacturingTabColumnSettingsList(74) = New Setting("CapComponentFacilityFWSystemLevel", CStr(SentSettings.CapComponentFacilityFWSystemLevel))
            ManufacturingTabColumnSettingsList(75) = New Setting("CopyingFacilityName", CStr(SentSettings.CopyingFacilityName))
            ManufacturingTabColumnSettingsList(76) = New Setting("CopyingFacilitySystem", CStr(SentSettings.CopyingFacilitySystem))
            ManufacturingTabColumnSettingsList(77) = New Setting("CopyingFacilityRegion", CStr(SentSettings.CopyingFacilityRegion))
            ManufacturingTabColumnSettingsList(78) = New Setting("CopyingFacilitySystemIndex", CStr(SentSettings.CopyingFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(79) = New Setting("CopyingFacilityTax", CStr(SentSettings.CopyingFacilityTax))
            ManufacturingTabColumnSettingsList(80) = New Setting("CopyingFacilityMEBonus", CStr(SentSettings.CopyingFacilityMEBonus))
            ManufacturingTabColumnSettingsList(81) = New Setting("CopyingFacilityTEBonus", CStr(SentSettings.CopyingFacilityTEBonus))
            ManufacturingTabColumnSettingsList(82) = New Setting("CopyingFacilityUsage", CStr(SentSettings.CopyingFacilityUsage))
            ManufacturingTabColumnSettingsList(83) = New Setting("CopyingFacilityFWSystemLevel", CStr(SentSettings.CopyingFacilityFWSystemLevel))
            ManufacturingTabColumnSettingsList(84) = New Setting("InventionFacilityName", CStr(SentSettings.InventionFacilityName))
            ManufacturingTabColumnSettingsList(85) = New Setting("InventionFacilitySystem", CStr(SentSettings.InventionFacilitySystem))
            ManufacturingTabColumnSettingsList(86) = New Setting("InventionFacilityRegion", CStr(SentSettings.InventionFacilityRegion))
            ManufacturingTabColumnSettingsList(87) = New Setting("InventionFacilitySystemIndex", CStr(SentSettings.InventionFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(88) = New Setting("InventionFacilityTax", CStr(SentSettings.InventionFacilityTax))
            ManufacturingTabColumnSettingsList(89) = New Setting("InventionFacilityMEBonus", CStr(SentSettings.InventionFacilityMEBonus))
            ManufacturingTabColumnSettingsList(90) = New Setting("InventionFacilityTEBonus", CStr(SentSettings.InventionFacilityTEBonus))
            ManufacturingTabColumnSettingsList(91) = New Setting("InventionFacilityUsage", CStr(SentSettings.InventionFacilityUsage))
            ManufacturingTabColumnSettingsList(92) = New Setting("InventionFacilityFWSystemLevel", CStr(SentSettings.InventionFacilityFWSystemLevel))
            ManufacturingTabColumnSettingsList(93) = New Setting("ReactionFacilityName", CStr(SentSettings.ReactionFacilityName))
            ManufacturingTabColumnSettingsList(94) = New Setting("ReactionFacilitySystem", CStr(SentSettings.ReactionFacilitySystem))
            ManufacturingTabColumnSettingsList(95) = New Setting("ReactionFacilityRegion", CStr(SentSettings.ReactionFacilityRegion))
            ManufacturingTabColumnSettingsList(96) = New Setting("ReactionFacilitySystemIndex", CStr(SentSettings.ReactionFacilitySystemIndex))
            ManufacturingTabColumnSettingsList(97) = New Setting("ReactionFacilityTax", CStr(SentSettings.ReactionFacilityTax))
            ManufacturingTabColumnSettingsList(98) = New Setting("ReactionFacilityMEBonus", CStr(SentSettings.ReactionFacilityMEBonus))
            ManufacturingTabColumnSettingsList(99) = New Setting("ReactionFacilityTEBonus", CStr(SentSettings.ReactionFacilityTEBonus))
            ManufacturingTabColumnSettingsList(100) = New Setting("ReactionFacilityUsage", CStr(SentSettings.ReactionFacilityUsage))
            ManufacturingTabColumnSettingsList(101) = New Setting("ReactionFacilityFWSystemLevel", CStr(SentSettings.ReactionFacilityFWSystemLevel))
            ManufacturingTabColumnSettingsList(102) = New Setting("ReprocessingFacilityName", CStr(SentSettings.ReprocessingFacilityName))
            ManufacturingTabColumnSettingsList(103) = New Setting("ReprocessingFacilitySystem", CStr(SentSettings.ReprocessingFacilitySystem))
            ManufacturingTabColumnSettingsList(104) = New Setting("ReprocessingFacilityRegion", CStr(SentSettings.ReprocessingFacilityRegion))
            ManufacturingTabColumnSettingsList(105) = New Setting("ReprocessingFacilityTax", CStr(SentSettings.ReprocessingFacilityTax))
            ManufacturingTabColumnSettingsList(106) = New Setting("ReprocessingFacilityUsage", CStr(SentSettings.ReprocessingFacilityUsage))
            ManufacturingTabColumnSettingsList(107) = New Setting("ReprocessingFacilityOreRefineRate", CStr(SentSettings.ReprocessingFacilityOreRefineRate))
            ManufacturingTabColumnSettingsList(108) = New Setting("ReprocessingFacilityIceRefineRate", CStr(SentSettings.ReprocessingFacilityIceRefineRate))
            ManufacturingTabColumnSettingsList(109) = New Setting("ReprocessingFacilityMoonRefineRate", CStr(SentSettings.ReprocessingFacilityMoonRefineRate))

            ManufacturingTabColumnSettingsList(110) = New Setting("ItemCategoryWidth", CStr(SentSettings.ItemCategoryWidth))
            ManufacturingTabColumnSettingsList(111) = New Setting("ItemGroupWidth", CStr(SentSettings.ItemGroupWidth))
            ManufacturingTabColumnSettingsList(112) = New Setting("ItemNameWidth", CStr(SentSettings.ItemNameWidth))
            ManufacturingTabColumnSettingsList(113) = New Setting("OwnedWidth", CStr(SentSettings.OwnedWidth))
            ManufacturingTabColumnSettingsList(114) = New Setting("TechWidth", CStr(SentSettings.TechWidth))
            ManufacturingTabColumnSettingsList(115) = New Setting("BPMEWidth", CStr(SentSettings.BPMEWidth))
            ManufacturingTabColumnSettingsList(116) = New Setting("BPTEWidth", CStr(SentSettings.BPTEWidth))
            ManufacturingTabColumnSettingsList(117) = New Setting("InputsWidth", CStr(SentSettings.InputsWidth))
            ManufacturingTabColumnSettingsList(118) = New Setting("ComparedWidth", CStr(SentSettings.ComparedWidth))
            ManufacturingTabColumnSettingsList(119) = New Setting("TotalRunsWidth", CStr(SentSettings.TotalRunsWidth))
            ManufacturingTabColumnSettingsList(120) = New Setting("SingleInventedBPCRunsWidth", CStr(SentSettings.SingleInventedBPCRunsWidth))
            ManufacturingTabColumnSettingsList(121) = New Setting("ProductionLinesWidth", CStr(SentSettings.ProductionLinesWidth))
            ManufacturingTabColumnSettingsList(122) = New Setting("LaboratoryLinesWidth", CStr(SentSettings.LaboratoryLinesWidth))
            ManufacturingTabColumnSettingsList(123) = New Setting("TotalInventionCostWidth", CStr(SentSettings.TotalInventionCostWidth))
            ManufacturingTabColumnSettingsList(124) = New Setting("TotalCopyCostWidth", CStr(SentSettings.TotalCopyCostWidth))
            ManufacturingTabColumnSettingsList(125) = New Setting("TaxesWidth", CStr(SentSettings.TaxesWidth))
            ManufacturingTabColumnSettingsList(126) = New Setting("BrokerFeesWidth", CStr(SentSettings.BrokerFeesWidth))
            ManufacturingTabColumnSettingsList(127) = New Setting("BPProductionTimeWidth", CStr(SentSettings.BPProductionTimeWidth))
            ManufacturingTabColumnSettingsList(128) = New Setting("TotalProductionTimeWidth", CStr(SentSettings.TotalProductionTimeWidth))
            ManufacturingTabColumnSettingsList(129) = New Setting("CopyTimeWidth", CStr(SentSettings.CopyTimeWidth))
            ManufacturingTabColumnSettingsList(130) = New Setting("InventionTimeWidth", CStr(SentSettings.InventionTimeWidth))
            ManufacturingTabColumnSettingsList(131) = New Setting("ItemMarketPriceWidth", CStr(SentSettings.ItemMarketPriceWidth))
            ManufacturingTabColumnSettingsList(132) = New Setting("ProfitWidth", CStr(SentSettings.ProfitWidth))
            ManufacturingTabColumnSettingsList(133) = New Setting("ProfitPercentageWidth", CStr(SentSettings.ProfitPercentageWidth))
            ManufacturingTabColumnSettingsList(134) = New Setting("IskperHourWidth", CStr(SentSettings.IskperHourWidth))
            ManufacturingTabColumnSettingsList(135) = New Setting("SVRWidth", CStr(SentSettings.SVRWidth))
            ManufacturingTabColumnSettingsList(136) = New Setting("SVRxIPHWidth", CStr(SentSettings.SVRxIPHWidth))
            ManufacturingTabColumnSettingsList(137) = New Setting("PriceTrendWidth", CStr(SentSettings.PriceTrendWidth))
            ManufacturingTabColumnSettingsList(138) = New Setting("TotalItemsSoldWidth", CStr(SentSettings.TotalItemsSoldWidth))
            ManufacturingTabColumnSettingsList(139) = New Setting("TotalOrdersFilledWidth", CStr(SentSettings.TotalOrdersFilledWidth))
            ManufacturingTabColumnSettingsList(140) = New Setting("AvgItemsperOrderWidth", CStr(SentSettings.AvgItemsperOrderWidth))
            ManufacturingTabColumnSettingsList(141) = New Setting("CurrentSellOrdersWidth", CStr(SentSettings.CurrentSellOrdersWidth))
            ManufacturingTabColumnSettingsList(142) = New Setting("CurrentBuyOrdersWidth", CStr(SentSettings.CurrentBuyOrdersWidth))
            ManufacturingTabColumnSettingsList(143) = New Setting("ItemsinProductionWidth", CStr(SentSettings.ItemsinProductionWidth))
            ManufacturingTabColumnSettingsList(144) = New Setting("ItemsinStockWidth", CStr(SentSettings.ItemsinStockWidth))
            ManufacturingTabColumnSettingsList(145) = New Setting("MaterialCostWidth", CStr(SentSettings.MaterialCostWidth))
            ManufacturingTabColumnSettingsList(146) = New Setting("TotalCostWidth", CStr(SentSettings.TotalCostWidth))
            ManufacturingTabColumnSettingsList(147) = New Setting("BaseJobCostWidth", CStr(SentSettings.BaseJobCostWidth))
            ManufacturingTabColumnSettingsList(148) = New Setting("NumBPsWidth", CStr(SentSettings.NumBPsWidth))
            ManufacturingTabColumnSettingsList(149) = New Setting("InventionChanceWidth", CStr(SentSettings.InventionChanceWidth))
            ManufacturingTabColumnSettingsList(150) = New Setting("BPTypeWidth", CStr(SentSettings.BPTypeWidth))
            ManufacturingTabColumnSettingsList(151) = New Setting("RaceWidth", CStr(SentSettings.RaceWidth))
            ManufacturingTabColumnSettingsList(152) = New Setting("VolumeperItemWidth", CStr(SentSettings.VolumeperItemWidth))
            ManufacturingTabColumnSettingsList(153) = New Setting("TotalVolumeWidth", CStr(SentSettings.TotalVolumeWidth))
            ManufacturingTabColumnSettingsList(154) = New Setting("SellExcessWidth", CStr(SentSettings.SellExcessWidth))
            ManufacturingTabColumnSettingsList(155) = New Setting("ROIWidth", CStr(SentSettings.ROIWidth))
            ManufacturingTabColumnSettingsList(156) = New Setting("PortionSizeWidth", CStr(SentSettings.PortionSizeWidth))
            ManufacturingTabColumnSettingsList(157) = New Setting("ManufacturingJobFeeWidth", CStr(SentSettings.ManufacturingJobFeeWidth))
            ManufacturingTabColumnSettingsList(158) = New Setting("ManufacturingFacilityNameWidth", CStr(SentSettings.ManufacturingFacilityNameWidth))
            ManufacturingTabColumnSettingsList(159) = New Setting("ManufacturingFacilitySystemWidth", CStr(SentSettings.ManufacturingFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(160) = New Setting("ManufacturingFacilityRegionWidth", CStr(SentSettings.ManufacturingFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(161) = New Setting("ManufacturingFacilitySystemIndexWidth", CStr(SentSettings.ManufacturingFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(162) = New Setting("ManufacturingFacilityTaxWidth", CStr(SentSettings.ManufacturingFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(163) = New Setting("ManufacturingFacilityMEBonusWidth", CStr(SentSettings.ManufacturingFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(164) = New Setting("ManufacturingFacilityTEBonusWidth", CStr(SentSettings.ManufacturingFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(165) = New Setting("ManufacturingFacilityUsageWidth", CStr(SentSettings.ManufacturingFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(166) = New Setting("ManufacturingFacilityFWSystemLevelWidth", CStr(SentSettings.ManufacturingFacilityFWSystemLevelWidth))
            ManufacturingTabColumnSettingsList(167) = New Setting("ComponentFacilityNameWidth", CStr(SentSettings.ComponentFacilityNameWidth))
            ManufacturingTabColumnSettingsList(168) = New Setting("ComponentFacilitySystemWidth", CStr(SentSettings.ComponentFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(169) = New Setting("ComponentFacilityRegionWidth", CStr(SentSettings.ComponentFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(170) = New Setting("ComponentFacilitySystemIndexWidth", CStr(SentSettings.ComponentFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(171) = New Setting("ComponentFacilityTaxWidth", CStr(SentSettings.ComponentFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(172) = New Setting("ComponentFacilityMEBonusWidth", CStr(SentSettings.ComponentFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(173) = New Setting("ComponentFacilityTEBonusWidth", CStr(SentSettings.ComponentFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(174) = New Setting("ComponentFacilityUsageWidth", CStr(SentSettings.ComponentFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(175) = New Setting("ComponentFacilityFWSystemLevelWidth", CStr(SentSettings.ComponentFacilityFWSystemLevelWidth))
            ManufacturingTabColumnSettingsList(176) = New Setting("CapComponentFacilityNameWidth", CStr(SentSettings.CapComponentFacilityNameWidth))
            ManufacturingTabColumnSettingsList(177) = New Setting("CapComponentFacilitySystemWidth", CStr(SentSettings.CapComponentFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(178) = New Setting("CapComponentFacilityRegionWidth", CStr(SentSettings.CapComponentFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(179) = New Setting("CapComponentFacilitySystemIndexWidth", CStr(SentSettings.CapComponentFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(180) = New Setting("CapComponentFacilityTaxWidth", CStr(SentSettings.CapComponentFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(181) = New Setting("CapComponentFacilityMEBonusWidth", CStr(SentSettings.CapComponentFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(182) = New Setting("CapComponentFacilityTEBonusWidth", CStr(SentSettings.CapComponentFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(183) = New Setting("CapComponentFacilityUsageWidth", CStr(SentSettings.CapComponentFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(184) = New Setting("CapComponentFacilityFWSystemLevelWidth", CStr(SentSettings.CapComponentFacilityFWSystemLevelWidth))
            ManufacturingTabColumnSettingsList(185) = New Setting("CopyingFacilityNameWidth", CStr(SentSettings.CopyingFacilityNameWidth))
            ManufacturingTabColumnSettingsList(186) = New Setting("CopyingFacilitySystemWidth", CStr(SentSettings.CopyingFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(187) = New Setting("CopyingFacilityRegionWidth", CStr(SentSettings.CopyingFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(188) = New Setting("CopyingFacilitySystemIndexWidth", CStr(SentSettings.CopyingFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(189) = New Setting("CopyingFacilityTaxWidth", CStr(SentSettings.CopyingFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(190) = New Setting("CopyingFacilityMEBonusWidth", CStr(SentSettings.CopyingFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(191) = New Setting("CopyingFacilityTEBonusWidth", CStr(SentSettings.CopyingFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(192) = New Setting("CopyingFacilityUsageWidth", CStr(SentSettings.CopyingFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(193) = New Setting("CopyingFacilityFWSystemLevelWidth", CStr(SentSettings.CopyingFacilityFWSystemLevelWidth))
            ManufacturingTabColumnSettingsList(194) = New Setting("InventionFacilityNameWidth", CStr(SentSettings.InventionFacilityNameWidth))
            ManufacturingTabColumnSettingsList(195) = New Setting("InventionFacilitySystemWidth", CStr(SentSettings.InventionFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(196) = New Setting("InventionFacilityRegionWidth", CStr(SentSettings.InventionFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(197) = New Setting("InventionFacilitySystemIndexWidth", CStr(SentSettings.InventionFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(198) = New Setting("InventionFacilityTaxWidth", CStr(SentSettings.InventionFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(199) = New Setting("InventionFacilityMEBonusWidth", CStr(SentSettings.InventionFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(200) = New Setting("InventionFacilityTEBonusWidth", CStr(SentSettings.InventionFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(201) = New Setting("InventionFacilityUsageWidth", CStr(SentSettings.InventionFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(202) = New Setting("InventionFacilityFWSystemLevelWidth", CStr(SentSettings.InventionFacilityFWSystemLevelWidth))
            ManufacturingTabColumnSettingsList(203) = New Setting("ReactionFacilityNameWidth", CStr(SentSettings.ReactionFacilityNameWidth))
            ManufacturingTabColumnSettingsList(204) = New Setting("ReactionFacilitySystemWidth", CStr(SentSettings.ReactionFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(205) = New Setting("ReactionFacilityRegionWidth", CStr(SentSettings.ReactionFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(206) = New Setting("ReactionFacilitySystemIndexWidth", CStr(SentSettings.ReactionFacilitySystemIndexWidth))
            ManufacturingTabColumnSettingsList(207) = New Setting("ReactionFacilityTaxWidth", CStr(SentSettings.ReactionFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(208) = New Setting("ReactionFacilityMEBonusWidth", CStr(SentSettings.ReactionFacilityMEBonusWidth))
            ManufacturingTabColumnSettingsList(209) = New Setting("ReactionFacilityTEBonusWidth", CStr(SentSettings.ReactionFacilityTEBonusWidth))
            ManufacturingTabColumnSettingsList(210) = New Setting("ReactionFacilityUsageWidth", CStr(SentSettings.ReactionFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(211) = New Setting("ReactionFacilityFWSystemLevelWidth", CStr(SentSettings.ReactionFacilityFWSystemLevelWidth))
            ManufacturingTabColumnSettingsList(212) = New Setting("ReprocessingFacilityNameWidth", CStr(SentSettings.ReprocessingFacilityNameWidth))
            ManufacturingTabColumnSettingsList(213) = New Setting("ReprocessingFacilitySystemWidth", CStr(SentSettings.ReprocessingFacilitySystemWidth))
            ManufacturingTabColumnSettingsList(214) = New Setting("ReprocessingFacilityRegionWidth", CStr(SentSettings.ReprocessingFacilityRegionWidth))
            ManufacturingTabColumnSettingsList(215) = New Setting("ReprocessingFacilityTaxWidth", CStr(SentSettings.ReprocessingFacilityTaxWidth))
            ManufacturingTabColumnSettingsList(216) = New Setting("ReprocessingFacilityUsageWidth", CStr(SentSettings.ReprocessingFacilityUsageWidth))
            ManufacturingTabColumnSettingsList(217) = New Setting("ReprocessingFacilityOreRefineRateWidth", CStr(SentSettings.ReprocessingFacilityOreRefineRateWidth))
            ManufacturingTabColumnSettingsList(218) = New Setting("ReprocessingFacilityIceRefineRateWidth", CStr(SentSettings.ReprocessingFacilityIceRefineRateWidth))
            ManufacturingTabColumnSettingsList(219) = New Setting("ReprocessingFacilityMoonRefineRateWidth", CStr(SentSettings.ReprocessingFacilityMoonRefineRateWidth))

            ManufacturingTabColumnSettingsList(220) = New Setting("OrderMTByColumn", CStr(SentSettings.OrderByColumn))
            ManufacturingTabColumnSettingsList(221) = New Setting("OrderMTType", CStr(SentSettings.OrderType))

            Call WriteSettingsToFile(SettingsFolder, ManufacturingTabColumnSettingsFileName, ManufacturingTabColumnSettingsList, ManufacturingTabColumnSettingsFileName)

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
            If FileExists(SettingsFolder, IndustryFlipBeltSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .CycleTime = CDbl(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeDouble, IndustryFlipBeltSettingsFileName, "CycleTime", DefaultCycleTime))
                    .m3perCycle = CDbl(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeDouble, IndustryFlipBeltSettingsFileName, "m3perCycle", Defaultm3perCycle))
                    .NumMiners = CInt(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeInteger, IndustryFlipBeltSettingsFileName, "NumMiners", DefaultNumMiners))
                    .CompressOre = CBool(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "CompressOre", DefaultCompressOre))
                    .IPHperMiner = CBool(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "IPHperMiner", DefaultIPHperMiner))
                    .IncludeBrokerFees = CInt(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeInteger, IndustryFlipBeltSettingsFileName, "IncludeBrokerFees", DefaultIncludeBrokerFees))
                    .IncludeTaxes = CBool(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeBoolean, IndustryFlipBeltSettingsFileName, "IncludeTaxes", DefaultIncludeTaxes))
                    .TrueSec = CStr(GetSettingValue(SettingsFolder, IndustryFlipBeltSettingsFileName, SettingTypes.TypeString, IndustryFlipBeltSettingsFileName, "TrueSec", DefaultTruesec))
                    .BrokerFeeRate = CDbl(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeDouble, BPSettingsFileName, "BrokerFeeRate", DefaultBPBrokerFeeRate))
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
            .BrokerFeeRate = DefaultBFBrokerFeeRate
            .IncludeTaxes = DefaultIncludeTaxes
            .TrueSec = DefaultTruesec
        End With

        ' Save locally
        IndustryFlipBeltsSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIndustryFlipBeltSettings(SentSettings As IndustryFlipBeltSettings)
        Dim IndustryFlipBeltSettingsList(8) As Setting

        Try
            IndustryFlipBeltSettingsList(0) = New Setting("CycleTime", CStr(SentSettings.CycleTime))
            IndustryFlipBeltSettingsList(1) = New Setting("m3perCycle", CStr(SentSettings.m3perCycle))
            IndustryFlipBeltSettingsList(2) = New Setting("NumMiners", CStr(SentSettings.NumMiners))
            IndustryFlipBeltSettingsList(3) = New Setting("CompressedOre", CStr(SentSettings.CompressOre))
            IndustryFlipBeltSettingsList(4) = New Setting("IPHperMiner", CStr(SentSettings.IPHperMiner))
            IndustryFlipBeltSettingsList(5) = New Setting("IncludeBrokerFees", CStr(SentSettings.IncludeBrokerFees))
            IndustryFlipBeltSettingsList(6) = New Setting("IncludeTaxes", CStr(SentSettings.IncludeTaxes))
            IndustryFlipBeltSettingsList(7) = New Setting("TrueSec", CStr(SentSettings.TrueSec))
            IndustryFlipBeltSettingsList(8) = New Setting("BrokerFeeRate", CStr(SentSettings.BrokerFeeRate))

            Call WriteSettingsToFile(SettingsFolder, IndustryFlipBeltSettingsFileName, IndustryFlipBeltSettingsList, IndustryFlipBeltSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Industry Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIndustryFlipBeltSettings() As IndustryFlipBeltSettings
        Return IndustryFlipBeltsSettings
    End Function

#End Region

#Region "Ice Belt Flip"

    ' Loads the tab settings
    Public Function LoadIceFlipBeltColumnSettings() As IceBeltFlipSettings
        Dim TempSettings As IceBeltFlipSettings = Nothing

        Try
            If FileExists(SettingsFolder, IceBeltFlipSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .CycleTime = CDbl(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeDouble, IceBeltFlipSettingsFileName, "CycleTime", DefaultCycleTime))
                    .m3perCycle = CDbl(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeDouble, IceBeltFlipSettingsFileName, "m3perCycle", Defaultm3perCycle))
                    .NumMiners = CInt(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeInteger, IceBeltFlipSettingsFileName, "NumMiners", DefaultNumMiners))
                    .CompressOre = CBool(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipSettingsFileName, "CompressOre", DefaultCompressOre))
                    .IPHperMiner = CBool(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipSettingsFileName, "IPHperMiner", DefaultIPHperMiner))
                    .IncludeBrokerFees = CInt(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeInteger, IceBeltFlipSettingsFileName, "IncludeBrokerFees", DefaultIncludeBrokerFees))
                    .IncludeTaxes = CBool(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipSettingsFileName, "IncludeTaxes", DefaultIncludeTaxes))
                    .SystemSecurity = CStr(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeString, IceBeltFlipSettingsFileName, "SystemSecurity", DefaultTruesec))
                    .BrokerFeeRate = CDbl(GetSettingValue(SettingsFolder, BPSettingsFileName, SettingTypes.TypeDouble, BPSettingsFileName, "BrokerFeeRate", DefaultBPBrokerFeeRate))
                    .Space = CStr(GetSettingValue(SettingsFolder, IceBeltFlipSettingsFileName, SettingTypes.TypeString, IceBeltFlipSettingsFileName, "Space", DefaultSpace))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultIceBeltFlipSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Ice Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultIceBeltFlipSettings()
        End Try

        ' Save them locally and then export
        IceBeltFlipSetting = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultIceBeltFlipSettings() As IceBeltFlipSettings
        Dim LocalSettings As IceBeltFlipSettings

        With LocalSettings
            .CycleTime = DefaultCycleTime
            .m3perCycle = Defaultm3perCycle
            .NumMiners = DefaultNumMiners
            .CompressOre = DefaultCompressOre
            .IPHperMiner = DefaultIPHperMiner
            .IncludeBrokerFees = DefaultIncludeBrokerFees
            .BrokerFeeRate = DefaultBFBrokerFeeRate
            .IncludeTaxes = DefaultIncludeTaxes
            .SystemSecurity = DefaultTruesec
            .Space = DefaultSpace
        End With

        ' Save locally
        IceBeltFlipSetting = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIceBeltFlipSettings(SentSettings As IceBeltFlipSettings)
        Dim IceBeltFlipSettingsList(8) As Setting

        Try
            IceBeltFlipSettingsList(0) = New Setting("CycleTime", CStr(SentSettings.CycleTime))
            IceBeltFlipSettingsList(1) = New Setting("m3perCycle", CStr(SentSettings.m3perCycle))
            IceBeltFlipSettingsList(2) = New Setting("NumMiners", CStr(SentSettings.NumMiners))
            IceBeltFlipSettingsList(3) = New Setting("CompressedOre", CStr(SentSettings.CompressOre))
            IceBeltFlipSettingsList(4) = New Setting("IPHperMiner", CStr(SentSettings.IPHperMiner))
            IceBeltFlipSettingsList(5) = New Setting("IncludeBrokerFees", CStr(SentSettings.IncludeBrokerFees))
            IceBeltFlipSettingsList(6) = New Setting("IncludeTaxes", CStr(SentSettings.IncludeTaxes))
            IceBeltFlipSettingsList(7) = New Setting("SystemSecurity", CStr(SentSettings.SystemSecurity))
            IceBeltFlipSettingsList(8) = New Setting("BrokerFeeRate", CStr(SentSettings.BrokerFeeRate))

            Call WriteSettingsToFile(SettingsFolder, IceBeltFlipSettingsFileName, IceBeltFlipSettingsList, IceBeltFlipSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Ice Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIceBeltFlipSettings() As IceBeltFlipSettings
        Return IceBeltFlipSetting
    End Function

#End Region

#Region "Industry Belt Ore Checks"

    ' Loads the tab settings
    Public Function LoadIndustryBeltOreChecksSettings(Belt As BeltType) As IndustryBeltOreChecks
        Dim TempSettings As IndustryBeltOreChecks = Nothing

        Select Case Belt
            Case BeltType.Small
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName1
            Case BeltType.Medium
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName2
            Case BeltType.Large
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName3
            Case BeltType.Enormous
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName4
            Case BeltType.Colossal
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName5
        End Select

        Try
            If FileExists(SettingsFolder, IndustryBeltOreChecksFileName) Then
                'Get the settings
                With TempSettings
                    .Plagioclase = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Plagioclase", DefaultPlagioclase))
                    .Spodumain = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Spodumain", DefaultSpodumain))
                    .Kernite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Kernite", DefaultKernite))
                    .Hedbergite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Hedbergite", DefaultHedbergite))
                    .Arkonor = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Arkonor", DefaultArkonor))
                    .Bistot = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Bistot", DefaultBistot))
                    .Pyroxeres = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Pyroxeres", DefaultPyroxeres))
                    .Crokite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Crokite", DefaultCrokite))
                    .Jaspet = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Jaspet", DefaultJaspet))
                    .Omber = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Omber", DefaultOmber))
                    .Scordite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Scordite", DefaultScordite))
                    .Gneiss = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Gneiss", DefaultGneiss))
                    .Veldspar = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Veldspar", DefaultVeldspar))
                    .Hemorphite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Hemorphite", DefaultHemorphite))
                    .DarkOchre = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "DarkOchre", DefaultDarkOchre))
                    .Mercoxit = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "Mercoxit", DefaultMercoxit))
                    .CrimsonArkonor = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "CrimsonArkonor", DefaultCrimsonArkonor))
                    .PrimeArkonor = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PrimeArkonor", DefaultPrimeArkonor))
                    .TriclinicBistot = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "TriclinicBistot", DefaultTriclinicBistot))
                    .MonoclinicBistot = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "MonoclinicBistot", DefaultMonoclinicBistot))
                    .SharpCrokite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "SharpCrokite", DefaultSharpCrokite))
                    .CrystallineCrokite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "CrystallineCrokite", DefaultCrystallineCrokite))
                    .OnyxOchre = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "OnyxOchre", DefaultOnyxOchre))
                    .ObsidianOchre = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "ObsidianOchre", DefaultObsidianOchre))
                    .VitricHedbergite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "VitricHedbergite", DefaultVitricHedbergite))
                    .GlazedHedbergite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "GlazedHedbergite", DefaultGlazedHedbergite))
                    .VividHemorphite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "VividHemorphite", DefaultVividHemorphite))
                    .RadiantHemorphite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "RadiantHemorphite", DefaultRadiantHemorphite))
                    .PureJaspet = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PureJaspet", DefaultPureJaspet))
                    .PristineJaspet = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PristineJaspet", DefaultPristineJaspet))
                    .LuminousKernite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "LuminousKernite", DefaultLuminousKernite))
                    .FieryKernite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "FieryKernite", DefaultFieryKernite))
                    .AzurePlagioclase = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "AzurePlagioclase", DefaultAzurePlagioclase))
                    .RichPlagioclase = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "RichPlagioclase", DefaultRichPlagioclase))
                    .SolidPyroxeres = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "SolidPyroxeres", DefaultSolidPyroxeres))
                    .ViscousPyroxeres = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "ViscousPyroxeres", DefaultViscousPyroxeres))
                    .CondensedScordite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "CondensedScordite", DefaultCondensedScordite))
                    .MassiveScordite = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "MassiveScordite", DefaultMassiveScordite))
                    .BrightSpodumain = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "BrightSpodumain", DefaultBrightSpodumain))
                    .GleamingSpodumain = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "GleamingSpodumain", DefaultGleamingSpodumain))
                    .ConcentratedVeldspar = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "ConcentratedVeldspar", DefaultConcentratedVeldspar))
                    .DenseVeldspar = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "DenseVeldspar", DefaultDenseVeldspar))
                    .IridescentGneiss = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "IridescentGneiss", DefaultIridescentGneiss))
                    .PrismaticGneiss = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "PrismaticGneiss", DefaultPrismaticGneiss))
                    .SilveryOmber = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "SilveryOmber", DefaultSilveryOmber))
                    .GoldenOmber = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "GoldenOmber", DefaultGoldenOmber))
                    .MagmaMercoxit = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "MagmaMercoxit", DefaultMagmaMercoxit))
                    .VitreousMercoxit = CBool(GetSettingValue(SettingsFolder, IndustryBeltOreChecksFileName, SettingTypes.TypeBoolean, IndustryBeltOreChecksFileName, "VitreousMercoxit", DefaultVitreousMercoxit))
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
            Case BeltType.Medium
                IndustryBeltOreChecksSettings2 = TempSettings
            Case BeltType.Large
                IndustryBeltOreChecksSettings3 = TempSettings
            Case BeltType.Enormous
                IndustryBeltOreChecksSettings4 = TempSettings
            Case BeltType.Colossal
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
            Case BeltType.Medium
                IndustryBeltOreChecksSettings2 = LocalSettings
            Case BeltType.Large
                IndustryBeltOreChecksSettings3 = LocalSettings
            Case BeltType.Enormous
                IndustryBeltOreChecksSettings4 = LocalSettings
            Case BeltType.Colossal
                IndustryBeltOreChecksSettings5 = LocalSettings
        End Select

        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIndustryBeltOreChecksSettings(SentSettings As IndustryBeltOreChecks, Belt As BeltType)
        Dim IndustryBeltOreChecksList(47) As Setting

        Select Case Belt
            Case BeltType.Small
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName1
            Case BeltType.Medium
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName2
            Case BeltType.Large
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName3
            Case BeltType.Enormous
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName4
            Case BeltType.Colossal
                IndustryBeltOreChecksFileName = IndustryBeltOreChecksBaseFileName & IndustryBeltOreChecksFileName5
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

            Call WriteSettingsToFile(SettingsFolder, IndustryBeltOreChecksFileName, IndustryBeltOreChecksList, IndustryBeltOreChecksFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Industry Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIndustryBeltOreChecksSettings(Belt As BeltType) As IndustryBeltOreChecks
        Select Case Belt
            Case BeltType.Small
                Return IndustryBeltOreChecksSettings1
            Case BeltType.Medium
                Return IndustryBeltOreChecksSettings2
            Case BeltType.Large
                Return IndustryBeltOreChecksSettings3
            Case BeltType.Enormous
                Return IndustryBeltOreChecksSettings4
            Case BeltType.Colossal
                Return IndustryBeltOreChecksSettings5
        End Select
    End Function

#End Region

#Region "Ice Belt Ore Checks"

    ' Loads the tab settings
    Public Function LoadIceBeltOreChecksSettings() As IceBeltCheckSettings
        Dim TempSettings As IceBeltCheckSettings = Nothing

        Try
            If FileExists(SettingsFolder, IceBeltFlipCheckSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .BlueIce = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "BlueIce", DefaultBlueIce))
                    .ClearIcicle = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "ClearIcicle", DefaultClearIcicle))
                    .DarkGlitter = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "DarkGlitter", DefaultDarkGlitter))
                    .EnrichedClearIcicle = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "EnrichedClearIcicle", DefaultEnrichedClearIcicle))
                    .Gelidus = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "Gelidus", DefaultGelidus))
                    .GlacialMass = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "GlacialMass", DefaultGlacialMass))
                    .GlareCrust = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "GlareCrust", DefaultGlareCrust))
                    .Krystallos = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "Krystallos", DefaultKrystallos))
                    .PristineWhiteGlaze = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "PristineWhiteGlaze", DefaultPristineWhiteGlaze))
                    .SmoothGlacialMass = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "SmoothGlacialMass", DefaultSmoothGlacialMass))
                    .ThickBlueIce = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "ThickBlueIce", DefaultThickBlueIce))
                    .WhiteGlaze = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "WhiteGlaze", DefaultWhiteGlaze))

                    .CompressedBlueIce = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedBlueIce", DefaultCompressedBlueIce))
                    .CompressedClearIcicle = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedClearIcicle", DefaultCompressedClearIcicle))
                    .CompressedDarkGlitter = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedDarkGlitter", DefaultCompressedDarkGlitter))
                    .CompressedEnrichedClearIcicle = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedEnrichedClearIcicle", DefaultCompressedEnrichedClearIcicle))
                    .CompressedGelidus = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedGelidus", DefaultCompressedGelidus))
                    .CompressedGlacialMass = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedGlacialMass", DefaultCompressedGlacialMass))
                    .CompressedGlareCrust = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedGlareCrust", DefaultCompressedGlareCrust))
                    .CompressedKrystallos = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedKrystallos", DefaultCompressedKrystallos))
                    .CompressedPristineWhiteGlaze = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedPristineWhiteGlaze", DefaultCompressedPristineWhiteGlaze))
                    .CompressedSmoothGlacialMass = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedSmoothGlacialMass", DefaultCompressedSmoothGlacialMass))
                    .CompressedThickBlueIce = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedThickBlueIce", DefaultCompressedThickBlueIce))
                    .CompressedWhiteGlaze = CBool(GetSettingValue(SettingsFolder, IceBeltFlipCheckSettingsFileName, SettingTypes.TypeBoolean, IceBeltFlipCheckSettingsFileName, "CompressedWhiteGlaze", DefaultCompressedWhiteGlaze))

                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultIceBeltChecksSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Ice Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultIceBeltChecksSettings()
        End Try

        ' Save them locally and then export
        IceBeltCheckSetting = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultIceBeltChecksSettings() As IceBeltCheckSettings
        Dim LocalSettings As IceBeltCheckSettings

        With LocalSettings
            .BlueIce = DefaultBlueIce
            .ClearIcicle = DefaultClearIcicle
            .DarkGlitter = DefaultDarkGlitter
            .EnrichedClearIcicle = DefaultEnrichedClearIcicle
            .Gelidus = DefaultGelidus
            .GlacialMass = DefaultGlacialMass
            .GlareCrust = DefaultGlareCrust
            .Krystallos = DefaultKrystallos
            .PristineWhiteGlaze = DefaultPristineWhiteGlaze
            .SmoothGlacialMass = DefaultSmoothGlacialMass
            .ThickBlueIce = DefaultThickBlueIce
            .WhiteGlaze = DefaultWhiteGlaze
            .CompressedBlueIce = DefaultCompressedBlueIce
            .CompressedClearIcicle = DefaultCompressedClearIcicle
            .CompressedDarkGlitter = DefaultCompressedDarkGlitter
            .CompressedEnrichedClearIcicle = DefaultCompressedEnrichedClearIcicle
            .CompressedGelidus = DefaultCompressedGelidus
            .CompressedGlacialMass = DefaultCompressedGlacialMass
            .CompressedGlareCrust = DefaultCompressedGlareCrust
            .CompressedKrystallos = DefaultCompressedKrystallos
            .CompressedPristineWhiteGlaze = DefaultCompressedPristineWhiteGlaze
            .CompressedSmoothGlacialMass = DefaultCompressedSmoothGlacialMass
            .CompressedThickBlueIce = DefaultCompressedThickBlueIce
            .CompressedWhiteGlaze = DefaultCompressedWhiteGlaze
        End With

        ' Save Locally
        IceBeltCheckSetting = LocalSettings

        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveIceBeltChecksSettings(SentSettings As IceBeltCheckSettings)
        Dim IceBeltOreChecksList(23) As Setting

        Try
            IceBeltOreChecksList(0) = New Setting("BlueIce", CStr(SentSettings.BlueIce))
            IceBeltOreChecksList(1) = New Setting("ClearIcicle", CStr(SentSettings.ClearIcicle))
            IceBeltOreChecksList(2) = New Setting("DarkGlitter", CStr(SentSettings.DarkGlitter))
            IceBeltOreChecksList(3) = New Setting("EnrichedClearIcicle", CStr(SentSettings.EnrichedClearIcicle))
            IceBeltOreChecksList(4) = New Setting("Gelidus", CStr(SentSettings.Gelidus))
            IceBeltOreChecksList(5) = New Setting("GlacialMass", CStr(SentSettings.GlacialMass))
            IceBeltOreChecksList(6) = New Setting("GlareCrust", CStr(SentSettings.GlareCrust))
            IceBeltOreChecksList(7) = New Setting("Krystallos", CStr(SentSettings.Krystallos))
            IceBeltOreChecksList(8) = New Setting("PristineWhiteGlaze", CStr(SentSettings.PristineWhiteGlaze))
            IceBeltOreChecksList(9) = New Setting("SmoothGlacialMass", CStr(SentSettings.SmoothGlacialMass))
            IceBeltOreChecksList(10) = New Setting("ThickBlueIce", CStr(SentSettings.ThickBlueIce))
            IceBeltOreChecksList(11) = New Setting("WhiteGlaze", CStr(SentSettings.WhiteGlaze))

            IceBeltOreChecksList(12) = New Setting("CompressedBlueIce", CStr(SentSettings.CompressedBlueIce))
            IceBeltOreChecksList(13) = New Setting("CompressedClearIcicle", CStr(SentSettings.CompressedClearIcicle))
            IceBeltOreChecksList(14) = New Setting("CompressedDarkGlitter", CStr(SentSettings.CompressedDarkGlitter))
            IceBeltOreChecksList(15) = New Setting("CompressedEnrichedClearIcicle", CStr(SentSettings.CompressedEnrichedClearIcicle))
            IceBeltOreChecksList(16) = New Setting("CompressedGelidus", CStr(SentSettings.CompressedGelidus))
            IceBeltOreChecksList(17) = New Setting("CompressedGlacialMass", CStr(SentSettings.CompressedGlacialMass))
            IceBeltOreChecksList(18) = New Setting("CompressedGlareCrust", CStr(SentSettings.CompressedGlareCrust))
            IceBeltOreChecksList(19) = New Setting("CompressedKrystallos", CStr(SentSettings.CompressedKrystallos))
            IceBeltOreChecksList(20) = New Setting("CompressedPristineWhiteGlaze", CStr(SentSettings.CompressedPristineWhiteGlaze))
            IceBeltOreChecksList(21) = New Setting("CompressedSmoothGlacialMass", CStr(SentSettings.CompressedSmoothGlacialMass))
            IceBeltOreChecksList(22) = New Setting("CompressedThickBlueIce", CStr(SentSettings.CompressedThickBlueIce))
            IceBeltOreChecksList(23) = New Setting("CompressedWhiteGlaze", CStr(SentSettings.CompressedWhiteGlaze))

            Call WriteSettingsToFile(SettingsFolder, IceBeltFlipCheckSettingsFileName, IceBeltOreChecksList, IceBeltFlipCheckSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Ice Flip Belt Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetIceBeltOreChecksSettings(Belt As BeltType) As IceBeltCheckSettings
        Return IceBeltCheckSetting
    End Function

#End Region

#Region "Conversion to Ore"

    ' Loads the tab settings
    Public Function LoadConversiontoOreSettings() As ConversionToOreSettings
        Dim TempSettings As ConversionToOreSettings = Nothing

        Try
            If FileExists(SettingsFolder, ConvertToOreSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .ConversionType = CStr(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeString, ConvertToOreSettingsFileName, "ConversionType", DefaultConversionType))
                    .MinimizeOn = CStr(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeString, ConvertToOreSettingsFileName, "MinimizeOn", DefaultMinimizeOn))

                    .CompressedIce = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "CompressedIce", DefaultCompressedIce))
                    .CompressedOre = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "CompressedOre", DefaultCompressedOre))
                    .HighSec = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "HighSec", DefaultHighSec))
                    .LowSec = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "LowSec", DefaultLowSec))
                    .NullSec = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "NullSec", DefaultNullSec))

                    .OreVariant0 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "OreVariant0", DefaultOreVariant0))
                    .OreVariant5 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "OreVariant5", DefaultOreVariant5))
                    .OreVariant10 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "OreVariant10", DefaultOreVariant10))
                    .Amarr = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "Amarr", DefaultAmarr))
                    .Caldari = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "Caldari", DefaultCaldari))
                    .Gallente = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "Gallente", DefaultGallente))
                    .Minmatar = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "Minmatar", DefaultMinmatar))
                    .Wormhole = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "Wormhole", DefaultWormhole))
                    .Triglavian = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "Triglavian", DefaultTriglavian))

                    .C1 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "C1", DefaultC1))
                    .C2 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "C2", DefaultC2))
                    .C3 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "C3", DefaultC3))
                    .C4 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "C4", DefaultC4))
                    .C5 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "C5", DefaultC5))
                    .C6 = CBool(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeBoolean, ConvertToOreSettingsFileName, "C6", DefaultC6))

                    Dim OverrideString As String = CStr(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeString, ConvertToOreSettingsFileName, "OverrideChecks", ""))
                    ReDim .OverrideChecks(35)

                    If OverrideString = "" Then
                        .OverrideChecks = GetDefaultOverrideChecks()
                    Else
                        ' Parse it out and Save values
                        Dim Items As String() = OverrideString.Split(New Char() {","c})

                        For i = 0 To 35
                            .OverrideChecks(i) = CInt(Items(i))
                        Next
                    End If

                    .SelectedOres = New List(Of OreType)
                    .IgnoreItems = New List(Of String)
                    ReDim .IgnoreRefinedItems(14)
                    Dim IgnoreRefinedItemsString As String = CStr(GetSettingValue(SettingsFolder, ConvertToOreSettingsFileName, SettingTypes.TypeString, ConvertToOreSettingsFileName, "IgnoreRefinedItems", ""))

                    If IgnoreRefinedItemsString <> "" Then
                        Dim RefinedItems As String() = IgnoreRefinedItemsString.Split(New Char() {","c})
                        For i = 0 To 14
                            .IgnoreRefinedItems(i) = CInt(RefinedItems(i))
                        Next
                    End If
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultConversionToOreSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading Conversion to Ore Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultConversionToOreSettings()
        End Try

        ' Save them locally and then export
        ConversionToOreSetting = TempSettings

        Return TempSettings

    End Function

    ' Loads the Defaults for the tab
    Public Function SetDefaultConversionToOreSettings() As ConversionToOreSettings
        Dim LocalSettings As ConversionToOreSettings

        With LocalSettings
            .ConversionType = DefaultConversionType
            .MinimizeOn = DefaultMinimizeOn
            .CompressedOre = DefaultCompressedOre
            .CompressedIce = DefaultCompressedIce
            .HighSec = DefaultHighSec
            .LowSec = DefaultLowSec
            .NullSec = DefaultNullSec
            .OreVariant0 = DefaultOreVariant0
            .OreVariant5 = DefaultOreVariant5
            .OreVariant10 = DefaultOreVariant10
            .Amarr = DefaultAmarr
            .Caldari = DefaultCaldari
            .Gallente = DefaultGallente
            .Minmatar = DefaultMinmatar
            .Wormhole = DefaultWormhole
            .Triglavian = DefaultTriglavian
            .C1 = DefaultC1
            .C2 = DefaultC2
            .C3 = DefaultC3
            .C4 = DefaultC4
            .C5 = DefaultC5
            .C6 = DefaultC6

            .OverrideChecks = GetDefaultOverrideChecks()
            .SelectedOres = New List(Of OreType)
            .IgnoreRefinedItems = GetDefaultIgnoreChecks()
            .IgnoreItems = New List(Of String)
        End With

        ' Save Locally
        ConversionToOreSetting = LocalSettings

        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveConversionToOreSettings(SentSettings As ConversionToOreSettings)
        Dim ConvertSetting(24) As Setting

        Try
            ConvertSetting(0) = New Setting("MinimizeOn", CStr(SentSettings.MinimizeOn))
            ConvertSetting(1) = New Setting("ConvertOre", CStr(SentSettings.ConvertOre))
            ConvertSetting(2) = New Setting("ConvertIce", CStr(SentSettings.ConvertIce))
            ConvertSetting(3) = New Setting("ConvertMoonOre", CStr(SentSettings.ConvertMoonOre))
            ConvertSetting(4) = New Setting("ConvertGas", CStr(SentSettings.ConvertGas))
            ConvertSetting(5) = New Setting("HighSec", CStr(SentSettings.HighSec))
            ConvertSetting(6) = New Setting("LowSec", CStr(SentSettings.LowSec))
            ConvertSetting(7) = New Setting("NullSec", CStr(SentSettings.NullSec))
            ConvertSetting(8) = New Setting("OreVariant0", CStr(SentSettings.OreVariant0))
            ConvertSetting(9) = New Setting("OreVariant5", CStr(SentSettings.OreVariant5))
            ConvertSetting(10) = New Setting("OreVariant10", CStr(SentSettings.OreVariant10))
            ConvertSetting(11) = New Setting("Amarr", CStr(SentSettings.Amarr))
            ConvertSetting(12) = New Setting("Caldari", CStr(SentSettings.Caldari))
            ConvertSetting(13) = New Setting("Gallente", CStr(SentSettings.Gallente))
            ConvertSetting(14) = New Setting("Minmatar", CStr(SentSettings.Minmatar))
            ConvertSetting(15) = New Setting("Wormhole", CStr(SentSettings.Wormhole))
            ConvertSetting(16) = New Setting("Triglavian", CStr(SentSettings.Triglavian))
            ConvertSetting(17) = New Setting("C1", CStr(SentSettings.C1))
            ConvertSetting(18) = New Setting("C2", CStr(SentSettings.C2))
            ConvertSetting(19) = New Setting("C3", CStr(SentSettings.C3))
            ConvertSetting(20) = New Setting("C4", CStr(SentSettings.C4))
            ConvertSetting(21) = New Setting("C5", CStr(SentSettings.C5))
            ConvertSetting(22) = New Setting("C6", CStr(SentSettings.C6))

            ' For overridechecks, just make one long string with the value for each index in order
            Dim OverrideList As String = ""

            For Each CheckValue In SentSettings.OverrideChecks
                OverrideList &= CheckValue & ","
            Next
            OverrideList = OverrideList.Substring(0, Len(OverrideList) - 1)
            ConvertSetting(23) = New Setting("OverrideChecks", OverrideList)

            Dim IgnoreItemsList As String = ""
            For Each item In SentSettings.IgnoreRefinedItems
                IgnoreItemsList &= item & ","
            Next
            IgnoreItemsList = IgnoreItemsList.Substring(0, Len(IgnoreItemsList) - 1)
            ConvertSetting(24) = New Setting("IgnoreRefinedItems", IgnoreItemsList)

            Call WriteSettingsToFile(SettingsFolder, ConvertToOreSettingsFileName, ConvertSetting, ConvertToOreSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Conversion to Ore Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetConversiontoOreSettings(Belt As BeltType) As ConversionToOreSettings
        Return ConversionToOreSetting
    End Function

    Private Function GetDefaultOverrideChecks() As Integer()
        Dim ReturnList(35) As Integer
        For i = 0 To 35
            ReturnList(i) = DefaultOverrideValue
        Next

        Return ReturnList

    End Function

    Private Function GetDefaultIgnoreChecks() As Integer()
        Dim ReturnList(14) As Integer
        For i = 0 To 14
            ReturnList(i) = DefaultIgnoreValue
        Next

        Return ReturnList

    End Function

#End Region

#Region "Asset Window Settings"

    ' Loads the tab settings
    Public Function LoadAssetWindowSettings(Location As AssetWindow) As AssetWindowSettings
        Dim TempSettings As AssetWindowSettings = Nothing
        Dim AssetWindowFileName As String = ""

        Select Case Location
            Case AssetWindow.DefaultView
                AssetWindowFileName = AssetWindowFileNameDefault
            Case AssetWindow.ManufacturingTab
                AssetWindowFileName = AssetWindowFileNameManufacturingTab
            Case AssetWindow.ShoppingList
                AssetWindowFileName = AssetWindowFileNameShoppingList
            Case AssetWindow.ReprocessingPlant
                AssetWindowFileName = AssetWindowFileNameRefinery
        End Select

        Try
            If FileExists(SettingsFolder, AssetWindowFileName) Then

                'Get the settings
                With TempSettings
                    .SelectedCharacterIDs = CStr(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeString, AssetWindowFileName, "SelectedCharacterIDs", DefaultSelectedCharacterIDs))

                    ' Main window
                    .AssetType = CStr(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeString, AssetWindowFileName, "AssetType", DefaultAssetType))
                    .SortbyName = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "SortbyName", DefaultAssetSortbyName))

                    ' Search Settings
                    .ItemFilterText = CStr(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeString, AssetWindowFileName, "ItemFilterText", DefaultAssetItemTextFilter))
                    .AllItems = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AllItems", DefaultAllItems))
                    .AllRawMats = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AllRawMats", DefaultAssetItemChecks))

                    .AdvancedProtectiveTechnology = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AdvancedProtectiveTechnology", DefaultAssetItemChecks))
                    .Gas = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Gas", DefaultAssetItemChecks))
                    .IceProducts = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "IceProducts", DefaultAssetItemChecks))
                    .MolecularForgingTools = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "MolecularForgingTools", DefaultAssetItemChecks))
                    .FactionMaterials = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "FactionMaterials", DefaultAssetItemChecks))
                    .NamedComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "NamedComponents", DefaultAssetItemChecks))
                    .Minerals = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Minerals", DefaultAssetItemChecks))
                    .Planetary = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Planetary", DefaultAssetItemChecks))
                    .RawMaterials = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "RawMaterials", DefaultAssetItemChecks))
                    .Salvage = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Salvage", DefaultAssetItemChecks))
                    .Misc = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Misc", DefaultAssetItemChecks))
                    .BPCs = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "BPCs", False))

                    .AdvancedMoonMats = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AdvancedMoonMats", DefaultAssetItemChecks))
                    .BoosterMats = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "BoosterMats", DefaultAssetItemChecks))
                    .MolecularForgedMats = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "MolecularForgedMats", DefaultAssetItemChecks))
                    .Polymers = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Polymers", DefaultAssetItemChecks))
                    .ProcessedMoonMats = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "ProcessedMoonMats", DefaultAssetItemChecks))
                    .RawMoonMats = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "RawMoonMats", DefaultAssetItemChecks))

                    .AncientRelics = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AncientRelics", DefaultAssetItemChecks))
                    .Datacores = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Datacores", DefaultAssetItemChecks))
                    .Decryptors = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Decryptors", DefaultAssetItemChecks))
                    .RDB = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "RDB", DefaultAssetItemChecks))

                    .AllManufacturedItems = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AllManufacturedItems", DefaultAssetItemChecks))

                    .Ships = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Ships", DefaultAssetItemChecks))
                    .Charges = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Charges", DefaultAssetItemChecks))
                    .Modules = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Modules", DefaultAssetItemChecks))
                    .Drones = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Drones", DefaultAssetItemChecks))
                    .Rigs = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Rigs", DefaultAssetItemChecks))
                    .Subsystems = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Subsystems", DefaultAssetItemChecks))
                    .Deployables = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Deployables", DefaultAssetItemChecks))
                    .Boosters = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Boosters", DefaultAssetItemChecks))
                    .Structures = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Structures", DefaultAssetItemChecks))
                    .StructureRigs = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "StructureRigs", DefaultAssetItemChecks))
                    .Celestials = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Celestials", DefaultAssetItemChecks))
                    .StructureModules = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "StructureModules", DefaultAssetItemChecks))
                    .Implants = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Implants", DefaultAssetItemChecks))

                    .AdvancedCapComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AdvancedCapComponents", DefaultAssetItemChecks))
                    .AdvancedComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "AdvancedComponents", DefaultAssetItemChecks))
                    .FuelBlocks = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "FuelBlocks", DefaultAssetItemChecks))
                    .ProtectiveComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "ProtectiveComponents", DefaultAssetItemChecks))
                    .RAM = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "RAM", DefaultAssetItemChecks))
                    .CapitalShipComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "CapitalShipComponents", DefaultAssetItemChecks))
                    .StructureComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Structure Components", DefaultAssetItemChecks))
                    .SubsystemComponents = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "SubsystemComponents", DefaultAssetItemChecks))
                    .NoBuildItems = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "NoBuildItems", False))

                    .T1 = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "T1", DefaultAssetItemChecks))
                    .T2 = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "T2", DefaultAssetItemChecks))
                    .T3 = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "T3", DefaultAssetItemChecks))
                    .Faction = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Faction", DefaultAssetItemChecks))
                    .Pirate = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Pirate", DefaultAssetItemChecks))
                    .Storyline = CBool(GetSettingValue(SettingsFolder, AssetWindowFileName, SettingTypes.TypeBoolean, AssetWindowFileName, "Storyline", DefaultAssetItemChecks))
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
            Case AssetWindow.ManufacturingTab
                AssetWindowSettingsManufacturingTab = TempSettings
            Case AssetWindow.ShoppingList
                AssetWindowSettingsShoppingList = TempSettings
            Case AssetWindow.ReprocessingPlant
                AssetWindowsettingsReprocessing = TempSettings
        End Select

        Return TempSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveAssetWindowSettings(ItemsSelected As AssetWindowSettings, Location As AssetWindow)
        Dim AssetWindowSettingsList(57) As Setting
        Dim AssetWindowFileName As String = ""

        Select Case Location
            Case AssetWindow.DefaultView
                AssetWindowFileName = AssetWindowFileNameDefault
            Case AssetWindow.ManufacturingTab
                AssetWindowFileName = AssetWindowFileNameManufacturingTab
            Case AssetWindow.ShoppingList
                AssetWindowFileName = AssetWindowFileNameShoppingList
            Case AssetWindow.ReprocessingPlant
                AssetWindowFileName = AssetWindowFileNameRefinery
        End Select

        Try
            ' Main window
            AssetWindowSettingsList(0) = New Setting("AssetType", CStr(ItemsSelected.AssetType))
            AssetWindowSettingsList(1) = New Setting("SortbyName", CStr(ItemsSelected.SortbyName))
            AssetWindowSettingsList(2) = New Setting("ItemFilterText", CStr(ItemsSelected.ItemFilterText))
            AssetWindowSettingsList(3) = New Setting("AllItems", CStr(ItemsSelected.AllItems))

            AssetWindowSettingsList(4) = New Setting("AllRawMats", CStr(ItemsSelected.AllRawMats))
            AssetWindowSettingsList(5) = New Setting("AdvancedProtectiveTechnology", CStr(ItemsSelected.AdvancedProtectiveTechnology))
            AssetWindowSettingsList(6) = New Setting("Gas", CStr(ItemsSelected.Gas))
            AssetWindowSettingsList(7) = New Setting("IceProducts", CStr(ItemsSelected.IceProducts))
            AssetWindowSettingsList(8) = New Setting("MolecularForgingTools", CStr(ItemsSelected.MolecularForgingTools))
            AssetWindowSettingsList(9) = New Setting("FactionMaterials", CStr(ItemsSelected.FactionMaterials))
            AssetWindowSettingsList(10) = New Setting("NamedComponents", CStr(ItemsSelected.NamedComponents))
            AssetWindowSettingsList(11) = New Setting("Minerals", CStr(ItemsSelected.Minerals))
            AssetWindowSettingsList(12) = New Setting("Planetary", CStr(ItemsSelected.Planetary))
            AssetWindowSettingsList(13) = New Setting("RawMaterials", CStr(ItemsSelected.RawMaterials))
            AssetWindowSettingsList(14) = New Setting("Salvage", CStr(ItemsSelected.Salvage))
            AssetWindowSettingsList(15) = New Setting("Misc", CStr(ItemsSelected.Misc))
            AssetWindowSettingsList(16) = New Setting("BPCs", CStr(ItemsSelected.BPCs))
            AssetWindowSettingsList(17) = New Setting("AdvancedMoonMats", CStr(ItemsSelected.AdvancedMoonMats))
            AssetWindowSettingsList(18) = New Setting("BoosterMats", CStr(ItemsSelected.BoosterMats))
            AssetWindowSettingsList(19) = New Setting("MolecularForgedMats", CStr(ItemsSelected.MolecularForgedMats))
            AssetWindowSettingsList(20) = New Setting("Polymers", CStr(ItemsSelected.Polymers))
            AssetWindowSettingsList(21) = New Setting("ProcessedMoonMats", CStr(ItemsSelected.ProcessedMoonMats))
            AssetWindowSettingsList(22) = New Setting("RawMoonMats", CStr(ItemsSelected.RawMoonMats))
            AssetWindowSettingsList(23) = New Setting("AncientRelics", CStr(ItemsSelected.AncientRelics))
            AssetWindowSettingsList(24) = New Setting("Datacores", CStr(ItemsSelected.Datacores))
            AssetWindowSettingsList(25) = New Setting("Decryptors", CStr(ItemsSelected.Decryptors))
            AssetWindowSettingsList(26) = New Setting("RDB", CStr(ItemsSelected.RDB))
            AssetWindowSettingsList(27) = New Setting("AllManufacturedItems", CStr(ItemsSelected.AllManufacturedItems))
            AssetWindowSettingsList(28) = New Setting("Ships", CStr(ItemsSelected.Ships))
            AssetWindowSettingsList(29) = New Setting("Charges", CStr(ItemsSelected.Charges))
            AssetWindowSettingsList(30) = New Setting("Modules", CStr(ItemsSelected.Modules))
            AssetWindowSettingsList(31) = New Setting("Drones", CStr(ItemsSelected.Drones))
            AssetWindowSettingsList(32) = New Setting("Rigs", CStr(ItemsSelected.Rigs))
            AssetWindowSettingsList(33) = New Setting("Subsystems", CStr(ItemsSelected.Subsystems))
            AssetWindowSettingsList(34) = New Setting("Deployables", CStr(ItemsSelected.Deployables))
            AssetWindowSettingsList(35) = New Setting("Boosters", CStr(ItemsSelected.Boosters))
            AssetWindowSettingsList(36) = New Setting("Structures", CStr(ItemsSelected.Structures))
            AssetWindowSettingsList(37) = New Setting("StructureRigs", CStr(ItemsSelected.StructureRigs))
            AssetWindowSettingsList(38) = New Setting("Celestials", CStr(ItemsSelected.Celestials))
            AssetWindowSettingsList(39) = New Setting("StructureModules", CStr(ItemsSelected.StructureModules))
            AssetWindowSettingsList(40) = New Setting("Implants", CStr(ItemsSelected.Implants))
            AssetWindowSettingsList(41) = New Setting("AdvancedCapComponents", CStr(ItemsSelected.AdvancedCapComponents))
            AssetWindowSettingsList(42) = New Setting("AdvancedComponents", CStr(ItemsSelected.AdvancedComponents))
            AssetWindowSettingsList(43) = New Setting("FuelBlocks", CStr(ItemsSelected.FuelBlocks))
            AssetWindowSettingsList(44) = New Setting("ProtectiveComponents", CStr(ItemsSelected.ProtectiveComponents))
            AssetWindowSettingsList(45) = New Setting("RAM", CStr(ItemsSelected.RAM))
            AssetWindowSettingsList(46) = New Setting("CapitalShipComponents", CStr(ItemsSelected.CapitalShipComponents))
            AssetWindowSettingsList(47) = New Setting("StructureComponents", CStr(ItemsSelected.StructureComponents))
            AssetWindowSettingsList(48) = New Setting("SubsystemComponents", CStr(ItemsSelected.SubsystemComponents))
            AssetWindowSettingsList(49) = New Setting("T1", CStr(ItemsSelected.T1))
            AssetWindowSettingsList(50) = New Setting("T2", CStr(ItemsSelected.T2))
            AssetWindowSettingsList(51) = New Setting("T3", CStr(ItemsSelected.T3))
            AssetWindowSettingsList(52) = New Setting("Faction", CStr(ItemsSelected.Faction))
            AssetWindowSettingsList(53) = New Setting("Pirate", CStr(ItemsSelected.Pirate))
            AssetWindowSettingsList(54) = New Setting("Storyline", CStr(ItemsSelected.Storyline))
            AssetWindowSettingsList(55) = New Setting("NoBuildItems", CStr(ItemsSelected.NoBuildItems))
            AssetWindowSettingsList(56) = New Setting("SelectedAccount", CStr(ItemsSelected.SelectedAccount))
            AssetWindowSettingsList(57) = New Setting("SelectedCharacterIDs", CStr(ItemsSelected.SelectedCharacterIDs))

            Call WriteSettingsToFile(SettingsFolder, AssetWindowFileName, AssetWindowSettingsList, AssetWindowFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Asset Window Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetAssetWindowSettings(Location As AssetWindow) As AssetWindowSettings

        Select Case Location
            Case AssetWindow.ManufacturingTab
                Return AssetWindowSettingsManufacturingTab
            Case AssetWindow.ShoppingList
                Return AssetWindowSettingsShoppingList
            Case AssetWindow.ReprocessingPlant
                Return AssetWindowsettingsReprocessing
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
            .AdvancedProtectiveTechnology = DefaultAssetItemChecks
            .Gas = DefaultAssetItemChecks
            .IceProducts = DefaultAssetItemChecks
            .MolecularForgingTools = DefaultAssetItemChecks
            .FactionMaterials = DefaultAssetItemChecks
            .NamedComponents = DefaultAssetItemChecks
            .Minerals = DefaultAssetItemChecks
            .Planetary = DefaultAssetItemChecks
            .RawMaterials = DefaultAssetItemChecks
            .Salvage = DefaultAssetItemChecks
            .Misc = DefaultAssetItemChecks
            .BPCs = DefaultAssetItemChecks
            .AdvancedMoonMats = DefaultAssetItemChecks
            .BoosterMats = DefaultAssetItemChecks
            .MolecularForgedMats = DefaultAssetItemChecks
            .Polymers = DefaultAssetItemChecks
            .ProcessedMoonMats = DefaultAssetItemChecks
            .RawMoonMats = DefaultAssetItemChecks
            .AncientRelics = DefaultAssetItemChecks
            .Datacores = DefaultAssetItemChecks
            .Decryptors = DefaultAssetItemChecks
            .RDB = DefaultAssetItemChecks
            .AllManufacturedItems = DefaultAssetItemChecks
            .Ships = DefaultAssetItemChecks
            .Charges = DefaultAssetItemChecks
            .Modules = DefaultAssetItemChecks
            .Drones = DefaultAssetItemChecks
            .Rigs = DefaultAssetItemChecks
            .Subsystems = DefaultAssetItemChecks
            .Deployables = DefaultAssetItemChecks
            .Boosters = DefaultAssetItemChecks
            .Structures = DefaultAssetItemChecks
            .StructureRigs = DefaultAssetItemChecks
            .Celestials = DefaultAssetItemChecks
            .StructureModules = DefaultAssetItemChecks
            .Implants = DefaultAssetItemChecks
            .AdvancedCapComponents = DefaultAssetItemChecks
            .AdvancedComponents = DefaultAssetItemChecks
            .FuelBlocks = DefaultAssetItemChecks
            .ProtectiveComponents = DefaultAssetItemChecks
            .RAM = DefaultAssetItemChecks
            .NoBuildItems = False
            .CapitalShipComponents = DefaultAssetItemChecks
            .StructureComponents = DefaultAssetItemChecks
            .SubsystemComponents = DefaultAssetItemChecks
            .T1 = DefaultAssetItemChecks
            .T2 = DefaultAssetItemChecks
            .T3 = DefaultAssetItemChecks
            .Faction = DefaultAssetItemChecks
            .Pirate = DefaultAssetItemChecks
            .Storyline = DefaultAssetItemChecks

        End With

        ' Save locally - Will have more than one
        Select Case Location
            Case AssetWindow.ManufacturingTab
                AssetWindowSettingsManufacturingTab = LocalSettings
            Case AssetWindow.ShoppingList
                AssetWindowSettingsShoppingList = LocalSettings
            Case AssetWindow.ReprocessingPlant
                AssetWindowsettingsReprocessing = LocalSettings
        End Select

        Return LocalSettings

    End Function

#End Region

#Region "Market History Viewer Settings"

    ' Loads the tab settings
    Public Function LoadMarketHistoryViewerSettingsSettings() As MarketHistoryViewerSettings
        Dim TempSettings As MarketHistoryViewerSettings = Nothing

        Try

            If FileExists(SettingsFolder, MarketHistoryViewerSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .DatePreference = CStr(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeString, MarketHistoryViewerSettingsFileName, "DatePreference", DefaultMHDatePreference))
                    .Volume = CBool(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeBoolean, MarketHistoryViewerSettingsFileName, "Volume", DefaultMHVolume))
                    .MinMaxDayPrice = CBool(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeBoolean, MarketHistoryViewerSettingsFileName, "MinMaxDayPrice", DefaultMHMinMaxDayPrice))
                    .LinearTrend = CBool(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeBoolean, MarketHistoryViewerSettingsFileName, "LinearTrend", DefaultMHLinearTrend))
                    .DochianChannel = CBool(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeBoolean, MarketHistoryViewerSettingsFileName, "DochianChannel", DefaultMHDochianChannel))
                    .FiveDayAvg = CBool(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeBoolean, MarketHistoryViewerSettingsFileName, "FiveDayAvg", DefaultMHFiveDayAvg))
                    .TwentyDayAvg = CBool(GetSettingValue(SettingsFolder, MarketHistoryViewerSettingsFileName, SettingTypes.TypeBoolean, MarketHistoryViewerSettingsFileName, "TwentyDayAvg", DefaultMHTwentyDayAvg))
                End With
            Else
                ' Load defaults 
                TempSettings = SetDefaultMarketHistoryViewerSettingsSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading MarketHistoryViewerSettings Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultMarketHistoryViewerSettingsSettings()
        End Try

        ' Save them locally and then export
        MarketHistoryViewSettings = TempSettings

        Return TempSettings

    End Function

    Public Function SetDefaultMarketHistoryViewerSettingsSettings() As MarketHistoryViewerSettings
        Dim LocalSettings As MarketHistoryViewerSettings

        With LocalSettings
            .DatePreference = DefaultMHDatePreference
            .Volume = DefaultMHVolume
            .MinMaxDayPrice = DefaultMHMinMaxDayPrice
            .LinearTrend = DefaultMHLinearTrend
            .DochianChannel = DefaultMHDochianChannel
            .FiveDayAvg = DefaultMHFiveDayAvg
            .TwentyDayAvg = DefaultMHTwentyDayAvg
        End With

        ' Save locally
        MarketHistoryViewSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveMarketHistoryViewerSettingsSettings(SentSettings As MarketHistoryViewerSettings)
        Dim MarketHistoryViewerSettingsSettingsList(6) As Setting

        Try
            MarketHistoryViewerSettingsSettingsList(0) = New Setting("DatePreference", CStr(SentSettings.DatePreference))
            MarketHistoryViewerSettingsSettingsList(1) = New Setting("MinMaxDayPrice", CStr(SentSettings.MinMaxDayPrice))
            MarketHistoryViewerSettingsSettingsList(2) = New Setting("Volume", CStr(SentSettings.Volume))
            MarketHistoryViewerSettingsSettingsList(3) = New Setting("LinearTrend", CStr(SentSettings.LinearTrend))
            MarketHistoryViewerSettingsSettingsList(4) = New Setting("DochianChannel", CStr(SentSettings.DochianChannel))
            MarketHistoryViewerSettingsSettingsList(5) = New Setting("FiveDayAvg", CStr(SentSettings.FiveDayAvg))
            MarketHistoryViewerSettingsSettingsList(6) = New Setting("TwentyDayAvg", CStr(SentSettings.TwentyDayAvg))

            Call WriteSettingsToFile(SettingsFolder, MarketHistoryViewerSettingsFileName, MarketHistoryViewerSettingsSettingsList, MarketHistoryViewerSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving LP Store Tab Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetMarketHistoryViewerSettingsSettings() As MarketHistoryViewerSettings
        Return MarketHistoryViewSettings
    End Function

#End Region

#Region "BP Viewer Settings"

    ' Loads the tab settings
    Public Function LoadBPViewerSettings() As BPViewerSettings
        Dim TempSettings As BPViewerSettings = Nothing

        Try
            If FileExists(SettingsFolder, BPViewerSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .BlueprintTypeSelection = CStr(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeString, BPViewerSettingsFileName, "BlueprintTypeSelection", DefaultBPViewerSelectionType))
                    .Tech1Check = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "Tech1Check", DefaultBPViewerTechChecks))
                    .Tech2Check = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "Tech2Check", DefaultBPViewerTechChecks))
                    .Tech3Check = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "Tech3Check", DefaultBPViewerTechChecks))
                    .TechStorylineCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "TechStorylineCheck", DefaultBPViewerTechChecks))
                    .TechFactionCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "TechFactionCheck", DefaultBPViewerTechChecks))
                    .TechPirateCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "TechPirateCheck", DefaultBPViewerTechChecks))
                    .SmallCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "SmallCheck", DefaultBPViewerSizeChecks))
                    .MediumCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "SmallCheck", DefaultBPViewerSizeChecks))
                    .LargeCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "SmallCheck", DefaultBPViewerSizeChecks))
                    .XLCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "SmallCheck", DefaultBPViewerSizeChecks))
                    .IncludeIgnoredBPs = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "IncludeIgnoredBPs", DefaultBPViewerIgnoreBPsCheck))
                    .BPNPCBPOsCheck = CBool(GetSettingValue(SettingsFolder, BPViewerSettingsFileName, SettingTypes.TypeBoolean, BPViewerSettingsFileName, "BPNPCBPOsCheck", DefaultBPNPCBPOsCheck))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultBPViewerSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading BP Viewer Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultBPViewerSettings()
        End Try

        ' Save them locally and then export
        BPViewSettings = TempSettings

        Return TempSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveBPViewerSettings(SentSettings As BPViewerSettings)
        Dim BPSettingsList(12) As Setting

        Try
            BPSettingsList(0) = New Setting("BlueprintTypeSelection", CStr(SentSettings.BlueprintTypeSelection))
            BPSettingsList(1) = New Setting("Tech1Check", CStr(SentSettings.Tech1Check))
            BPSettingsList(2) = New Setting("Tech2Check", CStr(SentSettings.Tech2Check))
            BPSettingsList(3) = New Setting("Tech3Check", CStr(SentSettings.Tech3Check))
            BPSettingsList(4) = New Setting("TechStorylineCheck", CStr(SentSettings.TechStorylineCheck))
            BPSettingsList(5) = New Setting("TechFactionCheck", CStr(SentSettings.TechFactionCheck))
            BPSettingsList(6) = New Setting("TechPirateCheck", CStr(SentSettings.TechPirateCheck))
            BPSettingsList(7) = New Setting("SmallCheck", CStr(SentSettings.SmallCheck))
            BPSettingsList(8) = New Setting("MediumCheck", CStr(SentSettings.MediumCheck))
            BPSettingsList(9) = New Setting("LargeCheck", CStr(SentSettings.LargeCheck))
            BPSettingsList(10) = New Setting("XLCheck", CStr(SentSettings.XLCheck))
            BPSettingsList(11) = New Setting("IncludeIgnoredBPs", CStr(SentSettings.IncludeIgnoredBPs))
            BPSettingsList(12) = New Setting("BPNPCBPOsCheck", CStr(SentSettings.BPNPCBPOsCheck))

            Call WriteSettingsToFile(SettingsFolder, BPSettingsFileName, BPSettingsList, BPSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving BP Viewer Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Loads the defaults
    Public Function SetDefaultBPViewerSettings() As BPViewerSettings
        Dim LocalSettings As BPViewerSettings

        With LocalSettings
            .BPNPCBPOsCheck = DefaultBPNPCBPOsCheck
            .BlueprintTypeSelection = DefaultBPViewerSelectionType
            .Tech1Check = DefaultBPViewerTechChecks
            .Tech2Check = DefaultBPViewerTechChecks
            .Tech3Check = DefaultBPViewerTechChecks
            .TechStorylineCheck = DefaultBPViewerTechChecks
            .TechFactionCheck = DefaultBPViewerTechChecks
            .TechPirateCheck = DefaultBPViewerTechChecks
            .SmallCheck = DefaultBPViewerSizeChecks
            .MediumCheck = DefaultBPViewerSizeChecks
            .LargeCheck = DefaultBPViewerSizeChecks
            .XLCheck = DefaultBPViewerSizeChecks
            .IncludeIgnoredBPs = DefaultBPViewerIgnoreBPsCheck
        End With

        ' Save locally
        BPViewSettings = LocalSettings

        Return LocalSettings

    End Function

    ' Returns the tab settings
    Public Function GetBPViewerSettings() As BPViewerSettings
        Return BPViewSettings
    End Function

#End Region

#Region "Upwell Structures Viewer Settings"

    ' Loads the tab settings
    Public Function LoadUpwellStructureViewerSettings() As UpwellStructureSettings
        Dim TempSettings As UpwellStructureSettings = Nothing

        Try

            If FileExists(SettingsFolder, UpwellStructureViewerSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .HighSlotsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "HighSlotsCheck", DefaultHighSlotsCheck))
                    .MediumSlotsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "MediumSlotsCheck", DefaultMediumSlotsCheck))
                    .LowSlotsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "LowSlotsCheck", DefaultLowSlotsCheck))
                    .ServicesCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "ServicesCheck", DefaultServicesCheck))
                    .ReprocessingRigsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "ReprocessingRigsCheck", DefaultReprocessingRigsCheck))
                    .EngineeringRigsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "EngineeringRigsCheck", DefaultEngineeringRigsCheck))
                    .CombatRigsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "CombatRigsCheck", DefaultCombatRigsCheck))
                    .IncludeFuelCostsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "IncludeFuelCostsCheck", DefaultIncludeFuelCostsCheck))
                    .FuelBlockType = CStr(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeString, UpwellStructureViewerSettingsFileName, "FuelBlockType", DefaultFuelBlockType))
                    .BuyBuildBlockOption = CStr(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeString, UpwellStructureViewerSettingsFileName, "BuyBuildBlockOption", DefaultBuyBuildBlockOption))
                    .AutoUpdateFuelBlockPricesCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "AutoUpdateFuelBlockPricesCheck", DefaultAutoUpdateFuelBlockPricesCheck))
                    .SearchFilterText = CStr(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeString, UpwellStructureViewerSettingsFileName, "SearchFilterText", DefaultSearchFilterText))
                    .SelectedStructureName = CStr(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeString, UpwellStructureViewerSettingsFileName, "SelectedStructureName", DefaultSelectedStructureName))
                    .ReactionsRigsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "ReactionsRigsCheck", DefaultReactionsRigsCheck))
                    .DrillingRigsCheck = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "DrillingRigsCheck", DefaultDrillingRigsCheck))
                    .IconListView = CBool(GetSettingValue(SettingsFolder, UpwellStructureViewerSettingsFileName, SettingTypes.TypeBoolean, UpwellStructureViewerSettingsFileName, "IconListView", DefaultIconListView))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultUpwellStructureViewerSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading UpwellStructureViewer Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultUpwellStructureViewerSettings()
        End Try

        ' Save them locally and then export
        UpwellStructureViewerSettings = TempSettings

        Return TempSettings

    End Function

    Public Function SetDefaultUpwellStructureViewerSettings() As UpwellStructureSettings
        Dim LocalSettings As UpwellStructureSettings

        With LocalSettings
            .HighSlotsCheck = DefaultHighSlotsCheck
            .MediumSlotsCheck = DefaultMediumSlotsCheck
            .LowSlotsCheck = DefaultLowSlotsCheck
            .ServicesCheck = DefaultServicesCheck
            .ReprocessingRigsCheck = DefaultReprocessingRigsCheck
            .EngineeringRigsCheck = DefaultEngineeringRigsCheck
            .CombatRigsCheck = DefaultCombatRigsCheck
            .IncludeFuelCostsCheck = DefaultIncludeFuelCostsCheck
            .FuelBlockType = DefaultFuelBlockType
            .BuyBuildBlockOption = DefaultBuyBuildBlockOption
            .AutoUpdateFuelBlockPricesCheck = DefaultAutoUpdateFuelBlockPricesCheck
            .SearchFilterText = DefaultSearchFilterText
            .SelectedStructureName = DefaultSelectedStructureName
            .ReactionsRigsCheck = DefaultReactionsRigsCheck
            .DrillingRigsCheck = DefaultDrillingRigsCheck
            .IconListView = DefaultIconListView
        End With

        ' Save locally
        UpwellStructureViewerSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveUpwellStructureViewerSettings(SentSettings As UpwellStructureSettings)
        Dim UpwellStructureViewerSettingsList(15) As Setting

        Try
            UpwellStructureViewerSettingsList(0) = New Setting("HighSlotsCheck", CStr(SentSettings.HighSlotsCheck))
            UpwellStructureViewerSettingsList(1) = New Setting("MediumSlotsCheck", CStr(SentSettings.MediumSlotsCheck))
            UpwellStructureViewerSettingsList(2) = New Setting("LowSlotsCheck", CStr(SentSettings.LowSlotsCheck))
            UpwellStructureViewerSettingsList(3) = New Setting("ServicesCheck", CStr(SentSettings.ServicesCheck))
            UpwellStructureViewerSettingsList(4) = New Setting("ReprocessingRigsCheck", CStr(SentSettings.ReprocessingRigsCheck))
            UpwellStructureViewerSettingsList(5) = New Setting("EngineeringRigsCheck", CStr(SentSettings.EngineeringRigsCheck))
            UpwellStructureViewerSettingsList(6) = New Setting("CombatRigsCheck", CStr(SentSettings.CombatRigsCheck))
            UpwellStructureViewerSettingsList(7) = New Setting("IncludeFuelCostsCheck", CStr(SentSettings.IncludeFuelCostsCheck))
            UpwellStructureViewerSettingsList(8) = New Setting("FuelBlockType", CStr(SentSettings.FuelBlockType))
            UpwellStructureViewerSettingsList(9) = New Setting("BuyBuildBlockOption", CStr(SentSettings.BuyBuildBlockOption))
            UpwellStructureViewerSettingsList(10) = New Setting("AutoUpdateFuelBlockPricesCheck", CStr(SentSettings.AutoUpdateFuelBlockPricesCheck))
            UpwellStructureViewerSettingsList(11) = New Setting("SearchFilterText", CStr(SentSettings.SearchFilterText))
            UpwellStructureViewerSettingsList(12) = New Setting("SelectedStructureName", CStr(SentSettings.SelectedStructureName))
            UpwellStructureViewerSettingsList(13) = New Setting("ReactionsRigsCheck", CStr(SentSettings.ReactionsRigsCheck))
            UpwellStructureViewerSettingsList(14) = New Setting("DrillingRigsCheck", CStr(SentSettings.DrillingRigsCheck))
            UpwellStructureViewerSettingsList(15) = New Setting("IconListView", CStr(SentSettings.IconListView))

            Call WriteSettingsToFile(SettingsFolder, UpwellStructureViewerSettingsFileName, UpwellStructureViewerSettingsList, UpwellStructureViewerSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Upwell Structures Viewer Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetUpwellStructureViewerSettings() As UpwellStructureSettings
        Return UpwellStructureViewerSettings
    End Function

#End Region

#Region "Bonus Popup Settings"

    ' Loads the tab settings
    Public Function LoadStructureBonusPopoutViewerSettings() As StructureBonusPopoutSettings
        Dim TempSettings As StructureBonusPopoutSettings = Nothing

        Try

            If FileExists(SettingsFolder, StructureBonusPopoutViewerSettingsFileName) Then
                'Get the settings
                With TempSettings
                    .FormHeight = CInt(GetSettingValue(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, SettingTypes.TypeInteger, StructureBonusPopoutViewerSettingsFileName, "FormHeight", DefaultSBPVFormHeight))
                    .FormWidth = CInt(GetSettingValue(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, SettingTypes.TypeInteger, StructureBonusPopoutViewerSettingsFileName, "FormWidth", DefaultSBPVFormWidth))
                    .ActivityColumnWidth = CInt(GetSettingValue(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, SettingTypes.TypeInteger, StructureBonusPopoutViewerSettingsFileName, "ActivityColumnWidth", DefaultSBPVActivityColumnWidth))
                    .BonusAppliesColumnWidth = CInt(GetSettingValue(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, SettingTypes.TypeInteger, StructureBonusPopoutViewerSettingsFileName, "BonusAppliesColumnWidth", DefaultSBPVBonusAppliesColumnWidth))
                    .BonusesColumnWidth = CInt(GetSettingValue(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, SettingTypes.TypeInteger, StructureBonusPopoutViewerSettingsFileName, "BonusesColumnWidth", DefaultSBPVBonusesColumnWidth))
                    .BonusSourceColumnWidth = CInt(GetSettingValue(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, SettingTypes.TypeInteger, StructureBonusPopoutViewerSettingsFileName, "BonusSourceColumnWidth", DefaultSBPVBonusSourceColumnWidth))
                End With

            Else
                ' Load defaults 
                TempSettings = SetDefaultStructureBonusPopoutViewerSettings()
            End If

        Catch ex As Exception
            MsgBox("An error occured when loading UpwellStructureViewer Settings. Error: " & Err.Description & vbCrLf & "Default settings were loaded.", vbExclamation, Application.ProductName)
            ' Load defaults 
            TempSettings = SetDefaultStructureBonusPopoutViewerSettings()
        End Try

        ' Save them locally and then export
        StructureBonusPopoutViewerSettings = TempSettings

        Return TempSettings

    End Function

    Public Function SetDefaultStructureBonusPopoutViewerSettings() As StructureBonusPopoutSettings
        Dim LocalSettings As StructureBonusPopoutSettings

        With LocalSettings
            .FormHeight = DefaultSBPVFormHeight
            .FormWidth = DefaultSBPVFormWidth
            .ActivityColumnWidth = DefaultSBPVActivityColumnWidth
            .BonusAppliesColumnWidth = DefaultSBPVBonusAppliesColumnWidth
            .BonusesColumnWidth = DefaultSBPVBonusesColumnWidth
            .BonusSourceColumnWidth = DefaultSBPVBonusSourceColumnWidth
        End With

        ' Save locally
        StructureBonusPopoutViewerSettings = LocalSettings
        Return LocalSettings

    End Function

    ' Saves the tab settings to XML
    Public Sub SaveStructureBonusPopoutViewerSettings(SentSettings As StructureBonusPopoutSettings)
        Dim StructureBonusPopoutViewerSettingsList(5) As Setting

        Try
            StructureBonusPopoutViewerSettingsList(0) = New Setting("FormHeight", CStr(SentSettings.FormHeight))
            StructureBonusPopoutViewerSettingsList(1) = New Setting("FormWidth", CStr(SentSettings.FormWidth))
            StructureBonusPopoutViewerSettingsList(2) = New Setting("ActivityColumnWidth", CStr(SentSettings.ActivityColumnWidth))
            StructureBonusPopoutViewerSettingsList(3) = New Setting("BonusAppliesColumnWidth", CStr(SentSettings.BonusAppliesColumnWidth))
            StructureBonusPopoutViewerSettingsList(4) = New Setting("BonusesColumnWidth", CStr(SentSettings.BonusesColumnWidth))
            StructureBonusPopoutViewerSettingsList(5) = New Setting("BonusSourceColumnWidth", CStr(SentSettings.BonusSourceColumnWidth))

            Call WriteSettingsToFile(SettingsFolder, StructureBonusPopoutViewerSettingsFileName, StructureBonusPopoutViewerSettingsList, StructureBonusPopoutViewerSettingsFileName)

        Catch ex As Exception
            MsgBox("An error occured when saving Upwell Structures Viewer Settings. Error: " & Err.Description & vbCrLf & "Settings not saved.", vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Returns the tab settings
    Public Function GetStructureBonusPopoutViewerSettings() As StructureBonusPopoutSettings
        Return StructureBonusPopoutViewerSettings
    End Function

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
    Dim LoadESIMarketDataonStartup As Boolean
    Dim LoadESISystemCostIndiciesDataonStartup As Boolean
    Dim LoadESIPublicStructuresonStartup As Boolean
    Dim SupressESIStatusMessages As Boolean
    Dim DisableSound As Boolean
    Dim IncludeInGameLinksinCopyText As Boolean

    Dim SaveFacilitiesbyChar As Boolean
    Dim LoadBPsbyChar As Boolean

    ' Station Standings for building and selling
    Dim BrokerCorpStanding As Double
    Dim BrokerFactionStanding As Double

    ' For setting default rates
    Dim BaseSalesTaxRate As Double
    Dim BaseBrokerFeeRate As Double
    Dim SCCBrokerFeeSurcharge As Double
    Dim SCCIndustryFeeSurcharge As Double
    Dim AlphaAccountTaxRate As Double
    Dim StructureTaxRate As Double
    Dim StationTaxRate As Double

    ' ME/TE for BP's we don't own or haven't entered info for
    Dim DefaultBPME As Integer
    Dim DefaultBPTE As Integer

    ' For Build/Buy 
    Dim CheckBuildBuy As Boolean ' Default for setting the check box for build/buy on the blueprints screen
    Dim SuggestBuildBPNotOwned As Boolean ' For Build/Buy suggestions
    Dim BuildWhenNotEnoughItemsonMarket As Boolean
    Dim ManualPriceOverride As Boolean
    Dim SaveBPRelicsDecryptors As Boolean ' For auto-loading relics and decryptor types
    Dim AlwaysBuyFuelBlocks As Boolean ' Forces build/buy to always buy fuel blocks instead of making a decision
    Dim AlwaysBuyRAMs As Boolean ' Forces build/buy to always buy RAMs instead of making a decision
    Dim SaveBPCCostperBP As Boolean ' Allows saving the inclusion of BPC cost in the production cost for the selected BPC

    Dim DisableSVR As Boolean ' For disabling SVR updates
    Dim DisableGATracking As Boolean ' for disabling tracking app usage through Google Analytics

    Dim ShareSavedFacilities As Boolean ' to use the same facility everywhere

    Dim RefineDrillDown As Boolean ' This is only on the refinery but since it's the only setting there, I'll just save it with application settings

    ' Character options
    Dim AlphaAccount As Boolean ' Check to determine if they are using an alpha account or not
    Dim UseActiveSkillLevels As Boolean ' Use active skill levels instead of trained - useful for omega on alpha currently
    Dim LoadMaxAlphaSkills As Boolean ' Load the max alpha skills for dummy accounts

    ' For shopping list
    Dim ShopListIncludeInventMats As Boolean
    Dim ShopListIncludeCopyMats As Boolean

    ' The interval for allowing refresh of prices 
    Dim UpdatePricesRefreshInterval As Integer

    ' Filter variables for svr
    Dim IgnoreSVRThresholdValue As Double
    Dim SVRAveragePriceRegion As String
    Dim SVRAveragePriceDuration As String
    Dim AutoUpdateSVRonBPTab As Boolean

    Dim ProxyAddress As String
    Dim ProxyPort As Integer

End Structure

Public Enum BuildMatType
    AdvMaterials = 1
    ProcessedMaterials = 2
    RawMaterials = 3
End Enum

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

    Dim IncludeFees As Integer ' 0,1,2 - Tri check
    Dim BrokerFeeRate As Double
    Dim IncludeUsage As Boolean
    Dim IncludeTaxes As Boolean

    Dim IncludeInventionCost As Boolean
    Dim IncludeInventionTime As Boolean
    Dim IncludeCopyCost As Boolean
    Dim IncludeCopyTime As Boolean
    Dim IncludeT3Cost As Boolean
    Dim IncludeT3Time As Boolean

    Dim OptimalT2Decryptor As Integer
    Dim OptimalT3Decryptor As Integer

    Dim PricePerUnit As Boolean

    Dim SimpleCopyCheck As Boolean
    Dim NPCBPOs As Boolean

    Dim ProductionLines As Integer
    Dim LaboratoryLines As Integer
    Dim T3Lines As Integer

    Dim T2DecryptorType As String
    Dim RelicType As String
    Dim T3DecryptorType As String

    Dim IgnoreInvention As Boolean
    Dim IgnoreMinerals As Boolean
    Dim IgnoreT1Item As Boolean

    Dim ExporttoShoppingListType As String

    Dim RawColumnSort As Integer
    Dim RawColumnSortType As String
    Dim CompColumnSort As Integer
    Dim CompColumnSortType As String

    Dim RawProfitType As String
    Dim CompProfitType As String

    Dim CompressedOre As Boolean

    Dim SellExcessBuildItems As Boolean

    Dim HistoryRegion As String
    Dim HistoryAvgDays As String

    Dim BuildT2T3Materials As BuildMatType ' How they want to build T2/T3 items (BuildMatType) - BP Tab

End Structure

' For the Market Viewer
Public Structure MarketHistoryViewerSettings
    Dim DatePreference As String

    Dim Volume As Boolean
    Dim MinMaxDayPrice As Boolean

    Dim LinearTrend As Boolean
    Dim DochianChannel As Boolean
    Dim FiveDayAvg As Boolean
    Dim TwentyDayAvg As Boolean

End Structure

' For Update Price Settings
Public Structure UpdatePriceTabSettings
    Dim AllRawMats As Boolean

    Dim AdvancedProtectiveTechnology As Boolean
    Dim Gas As Boolean
    Dim IceProducts As Boolean
    Dim MolecularForgingTools As Boolean
    Dim FactionMaterials As Boolean
    Dim NamedComponents As Boolean
    Dim Minerals As Boolean
    Dim Planetary As Boolean
    Dim RawMaterials As Boolean
    Dim Salvage As Boolean
    Dim Misc As Boolean
    Dim BPCs As Integer ' Tri-check

    Dim AdvancedMoonMats As Boolean
    Dim BoosterMats As Boolean
    Dim MolecularForgedMats As Boolean
    Dim Polymers As Boolean
    Dim ProcessedMoonMats As Boolean
    Dim RawMoonMats As Boolean

    Dim AncientRelics As Boolean
    Dim Datacores As Boolean
    Dim Decryptors As Boolean
    Dim RDB As Boolean

    Dim AllManufacturedItems As Boolean

    Dim Ships As Boolean
    Dim Charges As Boolean
    Dim Modules As Boolean
    Dim Drones As Boolean
    Dim Rigs As Boolean
    Dim Subsystems As Boolean
    Dim Deployables As Boolean
    Dim Boosters As Boolean
    Dim Structures As Boolean
    Dim StructureRigs As Boolean
    Dim Celestials As Boolean
    Dim StructureModules As Boolean
    Dim Implants As Boolean

    Dim AdvancedCapComponents As Boolean
    Dim AdvancedComponents As Boolean
    Dim FuelBlocks As Boolean
    Dim ProtectiveComponents As Boolean
    Dim RAM As Boolean
    Dim NoBuildItems As Boolean
    Dim CapitalShipComponents As Boolean
    Dim StructureComponents As Boolean
    Dim SubsystemComponents As Boolean

    Dim T1 As Boolean
    Dim T2 As Boolean
    Dim T3 As Boolean
    Dim Faction As Boolean
    Dim Pirate As Boolean
    Dim Storyline As Boolean

    Dim SelectedRegion As String
    Dim SelectedSystem As String

    ' The default price profile settings
    Dim PPRawPriceType As String
    Dim PPRawRegion As String
    Dim PPRawSystem As String
    Dim PPRawPriceMod As Double
    Dim PPItemsPriceType As String
    Dim PPItemsRegion As String
    Dim PPItemsSystem As String
    Dim PPItemsPriceMod As Double

    ' For two price types
    Dim ItemsCombo As String
    Dim RawMatsCombo As String

    Dim ItemsPriceModifier As Double
    Dim RawPriceModifier As Double

    Dim PriceDataSource As DataSource
    Dim UsePriceProfile As Boolean

    Dim ColumnSort As Integer
    Dim ColumnSortType As String

End Structure

Public Enum DataSource
    CCP = 0
    EVEMarketer = 1
    Fuzzworks = 2
End Enum

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
    Dim CheckBPTypeNPCBPOs As Boolean
    Dim CheckBPTypeModules As Boolean
    Dim CheckBPTypeAmmoCharges As Boolean
    Dim CheckBPTypeRigs As Boolean
    Dim CheckBPTypeSubsystems As Boolean
    Dim CheckBPTypeBoosters As Boolean
    Dim CheckBPTypeDeployables As Boolean
    Dim CheckBPTypeCelestials As Boolean
    Dim CheckBPTypeStructureModules As Boolean
    Dim CheckBPTypeStationParts As Boolean
    Dim CheckBPTypeReactions As Boolean

    Dim CheckCapitalComponentsFacility As Boolean
    Dim CheckT3DestroyerFacility As Boolean

    Dim CheckAutoCalcNumBPs As Boolean

    Dim CheckDecryptorNone As Boolean
    Dim CheckDecryptorOptimal As Integer ' Check State
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

    Dim CheckIncludeTaxes As Boolean
    Dim CheckIncludeBrokersFees As Integer ' Tri check
    Dim CalcBrokerFeeRate As Double
    Dim CheckIncludeUsage As Boolean

    Dim CheckRaceAmarr As Boolean
    Dim CheckRaceCaldari As Boolean
    Dim CheckRaceGallente As Boolean
    Dim CheckRaceMinmatar As Boolean
    Dim CheckRacePirate As Boolean
    Dim CheckRaceOther As Boolean

    Dim PriceCompare As String

    Dim CheckIncludeT2Owned As Boolean
    Dim CheckIncludeT3Owned As Boolean

    ' Filter variables
    Dim CheckSVRIncludeNull As Boolean
    Dim PriceTrend As String
    Dim MinBuildTime As String
    Dim MinBuildTimeCheck As Boolean
    Dim MaxBuildTime As String
    Dim MaxBuildTimeCheck As Boolean
    Dim IPHThreshold As Double
    Dim IPHThresholdCheck As Boolean
    Dim ProfitThreshold As Double
    Dim ProfitThresholdCheck As Integer
    Dim VolumeThreshold As Double
    Dim VolumeThresholdCheck As Boolean

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

    Dim CalcPPU As Boolean
    Dim CheckSellExcessItems As Boolean

    Dim ManufacturingFWUpgradeLevel As String
    Dim CopyingFWUpgradeLevel As String
    Dim InventionFWUpgradeLevel As String

    Dim ColumnSort As Integer
    Dim ColumnSortType As String

    Dim BuildT2T3Materials As BuildMatType ' How they want to build T2/T3 items (BuildMatType) - BP Tab

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

    Dim ColumnSort As Integer
    Dim ColumnSortType As String

End Structure

' For Mining Settings
Public Structure MiningTabSettings
    Dim OreType As String ' Ore or Ice

    Dim CheckHighYieldOres As Boolean
    Dim CheckHighSecOres As Boolean
    Dim CheckLowSecOres As Boolean
    Dim CheckNullSecOres As Boolean
    Dim CheckA0Ores As Boolean

    Dim CheckSovAmarr As Boolean
    Dim CheckSovCaldari As Boolean
    Dim CheckSovGallente As Boolean
    Dim CheckSovMinmatar As Boolean
    Dim CheckSovTriglavian As Boolean
    Dim CheckEDENCOM As Boolean
    Dim CheckSovWormhole As Boolean
    Dim CheckSovMoon As Boolean
    Dim CheckSovC1 As Boolean
    Dim CheckSovC2 As Boolean
    Dim CheckSovC3 As Boolean
    Dim CheckSovC4 As Boolean
    Dim CheckSovC5 As Boolean
    Dim CheckSovC6 As Boolean

    Dim CheckIncludeFees As Boolean
    Dim CheckIncludeTaxes As Boolean
    Dim BrokerFeeRate As Double

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
    Dim ShipDroneRig1 As String
    Dim ShipDroneRig2 As String
    Dim ShipDroneRig3 As String
    Dim ShipIceDroneRig1 As String
    Dim ShipIceDroneRig2 As String
    Dim ShipIceDroneRig3 As String

    Dim MiningDrone As String
    Dim NumMiningDrones As String
    Dim IceMiningDrone As String
    Dim NumIceMiningDrones As String
    Dim DroneOpSkill As String
    Dim DroneSpecSkill As String
    Dim DroneInterfaceSkill As String
    Dim IceDroneOpSkill As String
    Dim IceDroneSpecSkill As String
    Dim IceDroneInterfaceSkill As String

    Dim BoosterMiningDrone As String
    Dim BoosterNumMiningDrones As String
    Dim BoosterIceMiningDrone As String
    Dim BoosterNumIceMiningDrones As String
    Dim BoosterDroneOpSkill As String
    Dim BoosterDroneSpecSkill As String
    Dim BoosterDroneInterfaceSkill As String
    Dim BoosterIceDroneOpSkill As String
    Dim BoosterIceDroneSpecSkill As String
    Dim BoosterIceDroneInterfaceSkill As String

    Dim MichiiImplant As Boolean
    Dim T1Crystals As Boolean
    Dim T2Crystals As Boolean

    Dim CrystalTypeA As Boolean
    Dim CrystalTypeB As Boolean
    Dim CrystalTypeC As Boolean

    Dim CheckUseHauler As Boolean
    Dim RoundTripMin As Integer
    Dim RoundTripSec As Integer
    Dim Haulerm3 As Double

    Dim CheckUseFleetBooster As Boolean
    Dim BoosterShip As String
    Dim BoosterShipSkill As Integer
    Dim MiningFormanSkill As Integer
    Dim MiningDirectorSkill As Integer
    Dim CheckMineForemanLaserOpBoost As Integer ' 0,1,2
    Dim CheckMineForemanLaserRangeBoost As Integer '0,1,2
    Dim CheckMiningForemanMindLink As Boolean
    Dim BoosterUseDrones As Boolean
    Dim BoosterDroneRig1 As Integer
    Dim BoosterDroneRig2 As Integer
    Dim BoosterDroneRig3 As Integer
    Dim BoosterIceDroneRig1 As Integer
    Dim BoosterIceDroneRig2 As Integer
    Dim BoosterIceDroneRig3 As Integer

    Dim CheckRorqDeployed As Integer  '0,1,2
    Dim IndustrialReconfig As Integer

    Dim NumberofMiners As Integer

    Dim RefinedOre As Boolean
    Dim UnrefinedOre As Boolean
    Dim CompressedOre As Boolean

    Dim ColumnSort As Integer
    Dim ColumnSortType As String

    Dim OverrideCheck As Boolean
    Dim OverrideCycleTime As Double
    Dim OverrideLaserRange As Double

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
    Dim LocalCompletionDateTime As Integer

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
    Dim LocalCompletionDateTimeWidth As Integer

    Dim OrderByColumn As Integer ' What column index the jobs are sorted
    Dim OrderType As String ' Ascending or Descending

    Dim ViewJobType As String ' Personal, Corp, or Both

    Dim JobTimes As String ' Current or History

    ' List of selected characters, comma separated - default is going to be empty but will automatically choose the selected character
    Dim SelectedCharacterIDs As String

    ' Whether we automatically update jobs every time they open the window - if not checked, they need to hit 'Update Jobs'
    Dim AutoUpdateJobs As Boolean

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
    Dim PriceTrend As Integer
    Dim TotalItemsSold As Integer
    Dim TotalOrdersFilled As Integer
    Dim AvgItemsperOrder As Integer
    Dim CurrentSellOrders As Integer
    Dim CurrentBuyOrders As Integer
    Dim ItemsinProduction As Integer
    Dim ItemsinStock As Integer
    Dim MaterialCost As Integer
    Dim TotalCost As Integer
    Dim BaseJobCost As Integer
    Dim NumBPs As Integer
    Dim InventionChance As Integer
    Dim BPType As Integer
    Dim Race As Integer
    Dim VolumeperItem As Integer
    Dim TotalVolume As Integer
    Dim SellExcess As Integer
    Dim ROI As Integer
    Dim PortionSize As Integer
    Dim ManufacturingJobFee As Integer
    Dim ManufacturingFacilityName As Integer
    Dim ManufacturingFacilitySystem As Integer
    Dim ManufacturingFacilityRegion As Integer
    Dim ManufacturingFacilitySystemIndex As Integer
    Dim ManufacturingFacilityTax As Integer
    Dim ManufacturingFacilityMEBonus As Integer
    Dim ManufacturingFacilityTEBonus As Integer
    Dim ManufacturingFacilityUsage As Integer
    Dim ManufacturingFacilityFWSystemLevel As Integer
    Dim ComponentFacilityName As Integer
    Dim ComponentFacilitySystem As Integer
    Dim ComponentFacilityRegion As Integer
    Dim ComponentFacilitySystemIndex As Integer
    Dim ComponentFacilityTax As Integer
    Dim ComponentFacilityMEBonus As Integer
    Dim ComponentFacilityTEBonus As Integer
    Dim ComponentFacilityUsage As Integer
    Dim ComponentFacilityFWSystemLevel As Integer
    Dim CapComponentFacilityName As Integer
    Dim CapComponentFacilitySystem As Integer
    Dim CapComponentFacilityRegion As Integer
    Dim CapComponentFacilitySystemIndex As Integer
    Dim CapComponentFacilityTax As Integer
    Dim CapComponentFacilityMEBonus As Integer
    Dim CapComponentFacilityTEBonus As Integer
    Dim CapComponentFacilityUsage As Integer
    Dim CapComponentFacilityFWSystemLevel As Integer
    Dim CopyingFacilityName As Integer
    Dim CopyingFacilitySystem As Integer
    Dim CopyingFacilityRegion As Integer
    Dim CopyingFacilitySystemIndex As Integer
    Dim CopyingFacilityTax As Integer
    Dim CopyingFacilityMEBonus As Integer
    Dim CopyingFacilityTEBonus As Integer
    Dim CopyingFacilityUsage As Integer
    Dim CopyingFacilityFWSystemLevel As Integer
    Dim InventionFacilityName As Integer
    Dim InventionFacilitySystem As Integer
    Dim InventionFacilityRegion As Integer
    Dim InventionFacilitySystemIndex As Integer
    Dim InventionFacilityTax As Integer
    Dim InventionFacilityMEBonus As Integer
    Dim InventionFacilityTEBonus As Integer
    Dim InventionFacilityUsage As Integer
    Dim InventionFacilityFWSystemLevel As Integer
    Dim ReactionFacilityName As Integer
    Dim ReactionFacilitySystem As Integer
    Dim ReactionFacilityRegion As Integer
    Dim ReactionFacilitySystemIndex As Integer
    Dim ReactionFacilityTax As Integer
    Dim ReactionFacilityMEBonus As Integer
    Dim ReactionFacilityTEBonus As Integer
    Dim ReactionFacilityUsage As Integer
    Dim ReactionFacilityFWSystemLevel As Integer
    Dim ReprocessingFacilityName As Integer
    Dim ReprocessingFacilitySystem As Integer
    Dim ReprocessingFacilityRegion As Integer
    Dim ReprocessingFacilityTax As Integer
    Dim ReprocessingFacilityUsage As Integer
    Dim ReprocessingFacilityOreRefineRate As Integer
    Dim ReprocessingFacilityIceRefineRate As Integer
    Dim ReprocessingFacilityMoonRefineRate As Integer

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
    Dim PriceTrendWidth As Integer
    Dim TotalItemsSoldWidth As Integer
    Dim TotalOrdersFilledWidth As Integer
    Dim AvgItemsperOrderWidth As Integer
    Dim CurrentSellOrdersWidth As Integer
    Dim CurrentBuyOrdersWidth As Integer
    Dim ItemsinProductionWidth As Integer
    Dim ItemsinStockWidth As Integer
    Dim MaterialCostWidth As Integer
    Dim TotalCostWidth As Integer
    Dim BaseJobCostWidth As Integer
    Dim NumBPsWidth As Integer
    Dim InventionChanceWidth As Integer
    Dim BPTypeWidth As Integer
    Dim RaceWidth As Integer
    Dim VolumeperItemWidth As Integer
    Dim TotalVolumeWidth As Integer
    Dim SellExcessWidth As Integer
    Dim ROIWidth As Integer
    Dim PortionSizeWidth As Integer
    Dim ManufacturingJobFeeWidth As Integer
    Dim ManufacturingFacilityNameWidth As Integer
    Dim ManufacturingFacilitySystemWidth As Integer
    Dim ManufacturingFacilityRegionWidth As Integer
    Dim ManufacturingFacilitySystemIndexWidth As Integer
    Dim ManufacturingFacilityTaxWidth As Integer
    Dim ManufacturingFacilityMEBonusWidth As Integer
    Dim ManufacturingFacilityTEBonusWidth As Integer
    Dim ManufacturingFacilityUsageWidth As Integer
    Dim ManufacturingFacilityFWSystemLevelWidth As Integer
    Dim ComponentFacilityNameWidth As Integer
    Dim ComponentFacilitySystemWidth As Integer
    Dim ComponentFacilityRegionWidth As Integer
    Dim ComponentFacilitySystemIndexWidth As Integer
    Dim ComponentFacilityTaxWidth As Integer
    Dim ComponentFacilityMEBonusWidth As Integer
    Dim ComponentFacilityTEBonusWidth As Integer
    Dim ComponentFacilityUsageWidth As Integer
    Dim ComponentFacilityFWSystemLevelWidth As Integer
    Dim CapComponentFacilityNameWidth As Integer
    Dim CapComponentFacilitySystemWidth As Integer
    Dim CapComponentFacilityRegionWidth As Integer
    Dim CapComponentFacilitySystemIndexWidth As Integer
    Dim CapComponentFacilityTaxWidth As Integer
    Dim CapComponentFacilityMEBonusWidth As Integer
    Dim CapComponentFacilityTEBonusWidth As Integer
    Dim CapComponentFacilityUsageWidth As Integer
    Dim CapComponentFacilityFWSystemLevelWidth As Integer
    Dim CopyingFacilityNameWidth As Integer
    Dim CopyingFacilitySystemWidth As Integer
    Dim CopyingFacilityRegionWidth As Integer
    Dim CopyingFacilitySystemIndexWidth As Integer
    Dim CopyingFacilityTaxWidth As Integer
    Dim CopyingFacilityMEBonusWidth As Integer
    Dim CopyingFacilityTEBonusWidth As Integer
    Dim CopyingFacilityUsageWidth As Integer
    Dim CopyingFacilityFWSystemLevelWidth As Integer
    Dim InventionFacilityNameWidth As Integer
    Dim InventionFacilitySystemWidth As Integer
    Dim InventionFacilityRegionWidth As Integer
    Dim InventionFacilitySystemIndexWidth As Integer
    Dim InventionFacilityTaxWidth As Integer
    Dim InventionFacilityMEBonusWidth As Integer
    Dim InventionFacilityTEBonusWidth As Integer
    Dim InventionFacilityUsageWidth As Integer
    Dim InventionFacilityFWSystemLevelWidth As Integer
    Dim ReactionFacilityNameWidth As Integer
    Dim ReactionFacilitySystemWidth As Integer
    Dim ReactionFacilityRegionWidth As Integer
    Dim ReactionFacilitySystemIndexWidth As Integer
    Dim ReactionFacilityTaxWidth As Integer
    Dim ReactionFacilityMEBonusWidth As Integer
    Dim ReactionFacilityTEBonusWidth As Integer
    Dim ReactionFacilityUsageWidth As Integer
    Dim ReactionFacilityFWSystemLevelWidth As Integer
    Dim ReprocessingFacilityNameWidth As Integer
    Dim ReprocessingFacilitySystemWidth As Integer
    Dim ReprocessingFacilityRegionWidth As Integer
    Dim ReprocessingFacilityTaxWidth As Integer
    Dim ReprocessingFacilityUsageWidth As Integer
    Dim ReprocessingFacilityOreRefineRateWidth As Integer
    Dim ReprocessingFacilityIceRefineRateWidth As Integer
    Dim ReprocessingFacilityMoonRefineRateWidth As Integer

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
    Dim IncludeBrokerFees As Integer
    Dim IncludeTaxes As Boolean
    Dim BrokerFeeRate As Double
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

' For Ice Flip Belt Settings
Public Structure IceBeltFlipSettings
    Dim CycleTime As Double
    Dim m3perCycle As Double
    Dim NumMiners As Integer
    Dim CompressOre As Boolean
    Dim IPHperMiner As Boolean
    Dim IncludeTaxes As Boolean
    Dim IncludeBrokerFees As Integer
    Dim BrokerFeeRate As Double
    Dim SystemSecurity As String
    Dim Space As String
End Structure

' For the checked ore on each mining tab
Public Structure IceBeltCheckSettings
    Dim BlueIce As Boolean
    Dim ClearIcicle As Boolean
    Dim DarkGlitter As Boolean
    Dim EnrichedClearIcicle As Boolean
    Dim Gelidus As Boolean
    Dim GlacialMass As Boolean
    Dim GlareCrust As Boolean
    Dim Krystallos As Boolean
    Dim PristineWhiteGlaze As Boolean
    Dim SmoothGlacialMass As Boolean
    Dim ThickBlueIce As Boolean
    Dim WhiteGlaze As Boolean

    Dim CompressedBlueIce As Boolean
    Dim CompressedClearIcicle As Boolean
    Dim CompressedDarkGlitter As Boolean
    Dim CompressedEnrichedClearIcicle As Boolean
    Dim CompressedGelidus As Boolean
    Dim CompressedGlacialMass As Boolean
    Dim CompressedGlareCrust As Boolean
    Dim CompressedKrystallos As Boolean
    Dim CompressedPristineWhiteGlaze As Boolean
    Dim CompressedSmoothGlacialMass As Boolean
    Dim CompressedThickBlueIce As Boolean
    Dim CompressedWhiteGlaze As Boolean

End Structure

Public Structure ConversionToOreSettings
    Dim ConversionType As String
    Dim MinimizeOn As String
    Dim CompressedOre As Boolean
    Dim CompressedIce As Boolean
    Dim HighSec As Boolean
    Dim LowSec As Boolean
    Dim NullSec As Boolean
    Dim OreVariant0 As Boolean
    Dim OreVariant5 As Boolean
    Dim OreVariant10 As Boolean
    ' Tri-checks
    Dim ConvertOre As Integer
    Dim ConvertIce As Integer
    Dim ConvertMoonOre As Integer
    Dim ConvertGas As Integer
    Dim Amarr As Boolean
    Dim Caldari As Boolean
    Dim Gallente As Boolean
    Dim Minmatar As Boolean
    Dim Wormhole As Boolean
    Dim Triglavian As Boolean
    Dim C1 As Boolean
    Dim C2 As Boolean
    Dim C3 As Boolean
    Dim C4 As Boolean
    Dim C5 As Boolean
    Dim C6 As Boolean
    ' List of 35 check boxes cooresponds to the checks on settings for override savings
    Dim OverrideChecks() As Integer
    Dim SelectedOres As List(Of OreType)
    ' Names of all the item checks that they want to ignore in minerals/ice products to ores (meaning don't consider them in the conversion)
    Dim IgnoreRefinedItems() As Integer
    Dim IgnoreItems As List(Of String)

End Structure

Public Structure OreType
    Dim OreName As String
    Dim OreGroup As String ' Ice or Ore
End Structure

' For Assets Selected Item Settings
Public Structure AssetWindowSettings

    ' Main window
    Dim AssetType As String
    Dim SortbyName As Boolean

    ' Accounts
    Dim SelectedAccount As Boolean ' False is multi account
    ' List of selected characters, comma separated - default is going to be empty but will automatically choose the selected character
    Dim SelectedCharacterIDs As String

    ' Selected Items
    Dim ItemFilterText As String
    Dim AllItems As Boolean

    Dim AllRawMats As Boolean

    Dim AdvancedProtectiveTechnology As Boolean
    Dim Gas As Boolean
    Dim IceProducts As Boolean
    Dim MolecularForgingTools As Boolean
    Dim FactionMaterials As Boolean
    Dim NamedComponents As Boolean
    Dim Minerals As Boolean
    Dim Planetary As Boolean
    Dim RawMaterials As Boolean
    Dim Salvage As Boolean
    Dim Misc As Boolean
    Dim BPCs As Boolean

    Dim AdvancedMoonMats As Boolean
    Dim BoosterMats As Boolean
    Dim MolecularForgedMats As Boolean
    Dim Polymers As Boolean
    Dim ProcessedMoonMats As Boolean
    Dim RawMoonMats As Boolean

    Dim AncientRelics As Boolean
    Dim Datacores As Boolean
    Dim Decryptors As Boolean
    Dim RDB As Boolean

    Dim AllManufacturedItems As Boolean

    Dim Ships As Boolean
    Dim Charges As Boolean
    Dim Modules As Boolean
    Dim Drones As Boolean
    Dim Rigs As Boolean
    Dim Subsystems As Boolean
    Dim Deployables As Boolean
    Dim Boosters As Boolean
    Dim Structures As Boolean
    Dim StructureRigs As Boolean
    Dim Celestials As Boolean
    Dim StructureModules As Boolean
    Dim Implants As Boolean

    Dim AdvancedCapComponents As Boolean
    Dim AdvancedComponents As Boolean
    Dim FuelBlocks As Boolean
    Dim ProtectiveComponents As Boolean
    Dim RAM As Boolean
    Dim NoBuildItems As Boolean
    Dim CapitalShipComponents As Boolean
    Dim StructureComponents As Boolean
    Dim SubsystemComponents As Boolean

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
    Dim CalcBuyBuyOrder As Integer
    Dim Usage As Boolean
    Dim ReloadBPsFromFile As Boolean
End Structure

' For the BP Viewer
Public Structure BPViewerSettings
    Dim BlueprintTypeSelection As String ' Saves the name of the radio button used

    Dim BPNPCBPOsCheck As Boolean

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
End Structure

' For Upwell Structures fitting window
Public Structure UpwellStructureSettings
    Dim HighSlotsCheck As Boolean
    Dim MediumSlotsCheck As Boolean
    Dim LowSlotsCheck As Boolean
    Dim ServicesCheck As Boolean
    Dim ReprocessingRigsCheck As Boolean
    Dim EngineeringRigsCheck As Boolean
    Dim CombatRigsCheck As Boolean
    Dim ReactionsRigsCheck As Boolean
    Dim DrillingRigsCheck As Boolean

    Dim IncludeFuelCostsCheck As Boolean
    Dim FuelBlockType As String
    Dim BuyBuildBlockOption As String
    Dim AutoUpdateFuelBlockPricesCheck As Boolean
    Dim SearchFilterText As String
    Dim SelectedStructureName As String

    Dim IconListView As Boolean

End Structure

' For structure bonus viewing
Public Structure StructureBonusPopoutSettings
    Dim FormWidth As Integer
    Dim FormHeight As Integer
    Dim BonusAppliesColumnWidth As Integer
    Dim ActivityColumnWidth As Integer
    Dim BonusesColumnWidth As Integer
    Dim BonusSourceColumnWidth As Integer
End Structure