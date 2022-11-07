<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mnuStripMain = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSelectionAddChar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSelectionManageCharacters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewErrorLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSelectionExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItemUpdatePrices = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetPOSDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuManageBlueprintsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearBPHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateIndustryFacilities = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateESIMarketPrices = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateESIPublicStructures = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChangeDummyCharacterName = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuResetData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetBlueprintData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetIgnoredBPs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetPriceData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetAgents = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetIndustryJobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetAssets = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetMarketHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetMarketOrders = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetESIPublicStructures = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetSavedFacilities = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetESIIndustryFacilities = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetESIMarketPrices = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetESIDates = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuResetAllData = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewAssets = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSelectionShoppingList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCharacterSkills = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCharacterStandings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCurrentResearchAgents = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCurrentIndustryJobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuViewESIStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMETECalculator = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReprocessingPlant = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOreFlips = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAnomalyOreBelts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIceBelts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUserSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSelectDefaultChar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestoreDefaultTabSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestoreDefaultBP = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestoreDefaultUpdatePrices = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestoreDefaultManufacturing = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestoreDefaultDatacores = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestoreDefaultMining = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetBuildBuyManualSelections = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatchNotes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCheckforUpdates = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSelectionAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlMain = New System.Windows.Forms.StatusStrip()
        Me.mnuCharacter = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsCharacter1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter13 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter14 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter16 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter17 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter18 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCharacter20 = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlSkills = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlShoppingList = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.ttBP = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.gbSystems = New System.Windows.Forms.GroupBox()
        Me.ListOptionsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewMarketHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddToShoppingListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreBlueprintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FavoriteBlueprintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ttUpdatePrices = New System.Windows.Forms.ToolTip(Me.components)
        Me.rbtnPriceSourceFW = New System.Windows.Forms.RadioButton()
        Me.ttManufacturing = New System.Windows.Forms.ToolTip(Me.components)
        Me.ttDatacores = New System.Windows.Forms.ToolTip(Me.components)
        Me.ttReactions = New System.Windows.Forms.ToolTip(Me.components)
        Me.ttMining = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkMineEDENCOM = New System.Windows.Forms.CheckBox()
        Me.ttPI = New System.Windows.Forms.ToolTip(Me.components)
        Me.CalcImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.tabPI = New System.Windows.Forms.TabPage()
        Me.btnPISaveSettings = New System.Windows.Forms.Button()
        Me.gbPIPlanets = New System.Windows.Forms.GroupBox()
        Me.chkPILava = New System.Windows.Forms.CheckBox()
        Me.chkPIPlasma = New System.Windows.Forms.CheckBox()
        Me.chkPIIce = New System.Windows.Forms.CheckBox()
        Me.chkPIGas = New System.Windows.Forms.CheckBox()
        Me.chkPIOcean = New System.Windows.Forms.CheckBox()
        Me.chkPIBarren = New System.Windows.Forms.CheckBox()
        Me.chkPIStorm = New System.Windows.Forms.CheckBox()
        Me.chkPITemperate = New System.Windows.Forms.CheckBox()
        Me.btnPIReset = New System.Windows.Forms.Button()
        Me.tabMining = New System.Windows.Forms.TabPage()
        Me.gbMineCrystalType = New System.Windows.Forms.GroupBox()
        Me.chkMineTypeC = New System.Windows.Forms.CheckBox()
        Me.chkMineTypeB = New System.Windows.Forms.CheckBox()
        Me.chkMineTypeA = New System.Windows.Forms.CheckBox()
        Me.tabMiningDrones = New System.Windows.Forms.TabControl()
        Me.tabShipDrones = New System.Windows.Forms.TabPage()
        Me.lblMineDroneIdealRange = New System.Windows.Forms.Label()
        Me.cmbMineDroneName = New System.Windows.Forms.ComboBox()
        Me.lblMineMiningDroneYield = New System.Windows.Forms.Label()
        Me.cmbMineDroneOpSkill = New System.Windows.Forms.ComboBox()
        Me.lblMineMiningDroneM3 = New System.Windows.Forms.Label()
        Me.lblMineDroneOpSkill = New System.Windows.Forms.Label()
        Me.lblMineNumMiningDrones = New System.Windows.Forms.Label()
        Me.cmbMineDroneSpecSkill = New System.Windows.Forms.ComboBox()
        Me.cmbMineNumMiningDrones = New System.Windows.Forms.ComboBox()
        Me.lblMineDroneSpecSkill = New System.Windows.Forms.Label()
        Me.lblMineDroneInterfacingSkill = New System.Windows.Forms.Label()
        Me.lblMineDroneName = New System.Windows.Forms.Label()
        Me.cmbMineDroneInterfacingSkill = New System.Windows.Forms.ComboBox()
        Me.tabBoosterDrones = New System.Windows.Forms.TabPage()
        Me.lblMineBoosterDroneIdealRange = New System.Windows.Forms.Label()
        Me.cmbMineBoosterDroneName = New System.Windows.Forms.ComboBox()
        Me.lblMineBoosterMiningDroneYield = New System.Windows.Forms.Label()
        Me.cmbMineBoosterDroneOpSkill = New System.Windows.Forms.ComboBox()
        Me.lblMineBoosterMiningDroneM3 = New System.Windows.Forms.Label()
        Me.lblMineBoosterDroneOpSkill = New System.Windows.Forms.Label()
        Me.lblMineBoosterNumMiningDrones = New System.Windows.Forms.Label()
        Me.cmbMineBoosterDroneSpecSkill = New System.Windows.Forms.ComboBox()
        Me.cmbMineBoosterNumMiningDrones = New System.Windows.Forms.ComboBox()
        Me.lblMineBoosterDroneSpecSkill = New System.Windows.Forms.Label()
        Me.lblMineBoosterDroneInterfacingSkill = New System.Windows.Forms.Label()
        Me.lblMineBoosterDroneName = New System.Windows.Forms.Label()
        Me.cmbMineBoosterDroneInterfacingSkill = New System.Windows.Forms.ComboBox()
        Me.gbMineCrystals = New System.Windows.Forms.GroupBox()
        Me.chkMineT2Crystals = New System.Windows.Forms.CheckBox()
        Me.chkMineT1Crystals = New System.Windows.Forms.CheckBox()
        Me.gbMineNumberMiners = New System.Windows.Forms.GroupBox()
        Me.txtMineNumberMiners = New System.Windows.Forms.TextBox()
        Me.lblMineNumberMiners = New System.Windows.Forms.Label()
        Me.gbMineOreProcessingType = New System.Windows.Forms.GroupBox()
        Me.chkMineUnrefinedOre = New System.Windows.Forms.CheckBox()
        Me.chkMineRefinedOre = New System.Windows.Forms.CheckBox()
        Me.chkMineCompressedOre = New System.Windows.Forms.CheckBox()
        Me.btnMineSaveAllSettings = New System.Windows.Forms.Button()
        Me.gbMineTaxBroker = New System.Windows.Forms.GroupBox()
        Me.txtMineBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.chkMineIncludeBrokerFees = New System.Windows.Forms.CheckBox()
        Me.chkMineIncludeTaxes = New System.Windows.Forms.CheckBox()
        Me.gbMineStripStats = New System.Windows.Forms.GroupBox()
        Me.lblMineRange = New System.Windows.Forms.Label()
        Me.lblMineCycleTime1 = New System.Windows.Forms.Label()
        Me.lblMineRange1 = New System.Windows.Forms.Label()
        Me.lblMineCycleTime = New System.Windows.Forms.Label()
        Me.chkMineUseFleetBooster = New System.Windows.Forms.CheckBox()
        Me.btnMineReset = New System.Windows.Forms.Button()
        Me.gbMineHauling = New System.Windows.Forms.GroupBox()
        Me.txtMineHaulerM3 = New System.Windows.Forms.TextBox()
        Me.lblMineHaulerM3 = New System.Windows.Forms.Label()
        Me.lblMineRTSec = New System.Windows.Forms.Label()
        Me.chkMineUseHauler = New System.Windows.Forms.CheckBox()
        Me.lblMineRTMin = New System.Windows.Forms.Label()
        Me.txtMineRTMin = New System.Windows.Forms.TextBox()
        Me.txtMineRTSec = New System.Windows.Forms.TextBox()
        Me.lblMineRoundTripTime = New System.Windows.Forms.Label()
        Me.btnMineRefresh = New System.Windows.Forms.Button()
        Me.gbMineBooster = New System.Windows.Forms.GroupBox()
        Me.chkMineBoosterDroneRig3 = New System.Windows.Forms.CheckBox()
        Me.pictMineLaserOptmize = New System.Windows.Forms.PictureBox()
        Me.pictMineRangeLink = New System.Windows.Forms.PictureBox()
        Me.chkMineBoosterDroneRig2 = New System.Windows.Forms.CheckBox()
        Me.chkMineBoosterDroneRig1 = New System.Windows.Forms.CheckBox()
        Me.chkMineBoosterUseDrones = New System.Windows.Forms.CheckBox()
        Me.pictMineFleetBoostShip = New System.Windows.Forms.PictureBox()
        Me.chkMineForemanLaserRangeBoost = New System.Windows.Forms.CheckBox()
        Me.chkMineIndyCoreDeployedMode = New System.Windows.Forms.CheckBox()
        Me.cmbMineBoosterShipSkill = New System.Windows.Forms.ComboBox()
        Me.chkMineForemanMindlink = New System.Windows.Forms.CheckBox()
        Me.cmbMineBoosterShipName = New System.Windows.Forms.ComboBox()
        Me.cmbMineMiningDirector = New System.Windows.Forms.ComboBox()
        Me.chkMineForemanLaserOpBoost = New System.Windows.Forms.CheckBox()
        Me.lblMineMiningDirector = New System.Windows.Forms.Label()
        Me.cmbMineMiningForeman = New System.Windows.Forms.ComboBox()
        Me.lblMineFleetBoosterShip = New System.Windows.Forms.Label()
        Me.lblMineMiningForeman = New System.Windows.Forms.Label()
        Me.lblMineBoosterShipSkill = New System.Windows.Forms.Label()
        Me.cmbMineIndustReconfig = New System.Windows.Forms.ComboBox()
        Me.lblMineIndustrialReconfig = New System.Windows.Forms.Label()
        Me.gbMineRefining = New System.Windows.Forms.GroupBox()
        Me.gbMineOreStuctureRates = New System.Windows.Forms.GroupBox()
        Me.lblMineFacilityOreRate = New System.Windows.Forms.Label()
        Me.lblMineFacilityMoonOreRate = New System.Windows.Forms.Label()
        Me.lblMineFacilityOreRate1 = New System.Windows.Forms.Label()
        Me.lblMineFacilityMoonOreRate1 = New System.Windows.Forms.Label()
        Me.cmbMineRefining = New System.Windows.Forms.ComboBox()
        Me.lblMineRefining = New System.Windows.Forms.Label()
        Me.cmbMineRefineryEff = New System.Windows.Forms.ComboBox()
        Me.lblMineRefineryEfficiency = New System.Windows.Forms.Label()
        Me.MineRefineFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabMiningProcessingSkills = New System.Windows.Forms.TabControl()
        Me.tabPageOres = New System.Windows.Forms.TabPage()
        Me.chkOreProcessing1 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing2 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing2 = New System.Windows.Forms.Label()
        Me.chkOreProcessing3 = New System.Windows.Forms.CheckBox()
        Me.chkOreProcessing2 = New System.Windows.Forms.CheckBox()
        Me.chkOreProcessing6 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing1 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing1 = New System.Windows.Forms.Label()
        Me.lblOreProcessing6 = New System.Windows.Forms.Label()
        Me.chkOreProcessing5 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing6 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing3 = New System.Windows.Forms.Label()
        Me.lblOreProcessing5 = New System.Windows.Forms.Label()
        Me.cmbOreProcessing4 = New System.Windows.Forms.ComboBox()
        Me.cmbOreProcessing3 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing4 = New System.Windows.Forms.CheckBox()
        Me.lblOreProcessing4 = New System.Windows.Forms.Label()
        Me.cmbOreProcessing5 = New System.Windows.Forms.ComboBox()
        Me.tabPageMoonOres = New System.Windows.Forms.TabPage()
        Me.lblOreProcessing7 = New System.Windows.Forms.Label()
        Me.lblOreProcessing8 = New System.Windows.Forms.Label()
        Me.lblOreProcessing10 = New System.Windows.Forms.Label()
        Me.lblOreProcessing11 = New System.Windows.Forms.Label()
        Me.cmbOreProcessing11 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing9 = New System.Windows.Forms.CheckBox()
        Me.lblOreProcessing9 = New System.Windows.Forms.Label()
        Me.chkOreProcessing8 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing10 = New System.Windows.Forms.ComboBox()
        Me.cmbOreProcessing7 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing10 = New System.Windows.Forms.CheckBox()
        Me.chkOreProcessing7 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing9 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing11 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing8 = New System.Windows.Forms.ComboBox()
        Me.tabPageIce = New System.Windows.Forms.TabPage()
        Me.cmbOreProcessing12 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing12 = New System.Windows.Forms.Label()
        Me.chkOreProcessing12 = New System.Windows.Forms.CheckBox()
        Me.gbMineShipSetup = New System.Windows.Forms.GroupBox()
        Me.gbMineSelectShip = New System.Windows.Forms.GroupBox()
        Me.pictMineSelectedShip = New System.Windows.Forms.PictureBox()
        Me.lblMineSelectShip = New System.Windows.Forms.Label()
        Me.cmbMineBaseShipSkill = New System.Windows.Forms.ComboBox()
        Me.cmbMineAdvShipSkill = New System.Windows.Forms.ComboBox()
        Me.cmbMineShipName = New System.Windows.Forms.ComboBox()
        Me.lblMineBaseShipSkill = New System.Windows.Forms.Label()
        Me.lblMineExhumers = New System.Windows.Forms.Label()
        Me.gbMineShipEquipment = New System.Windows.Forms.GroupBox()
        Me.gbMiningRigs = New System.Windows.Forms.GroupBox()
        Me.cmbMineMiningRig3 = New System.Windows.Forms.ComboBox()
        Me.cmbMineMiningRig1 = New System.Windows.Forms.ComboBox()
        Me.cmbMineMiningRig2 = New System.Windows.Forms.ComboBox()
        Me.cmbMineMiningLaser = New System.Windows.Forms.ComboBox()
        Me.cmbMineNumMiningUpgrades = New System.Windows.Forms.ComboBox()
        Me.cmbMineNumLasers = New System.Windows.Forms.ComboBox()
        Me.cmbMineMiningUpgrade = New System.Windows.Forms.ComboBox()
        Me.cmbMineHighwallImplant = New System.Windows.Forms.ComboBox()
        Me.chkMineMichiImplant = New System.Windows.Forms.CheckBox()
        Me.lblMineImplants = New System.Windows.Forms.Label()
        Me.lblMineLaserNumber = New System.Windows.Forms.Label()
        Me.lblMineNumMiningUpgrades = New System.Windows.Forms.Label()
        Me.lblMineMinerTurret = New System.Windows.Forms.Label()
        Me.lblMineMiningUpgrade = New System.Windows.Forms.Label()
        Me.gbMineSkills = New System.Windows.Forms.GroupBox()
        Me.cmbMineGasIceHarvesting = New System.Windows.Forms.ComboBox()
        Me.lblMineGasIceHarvesting = New System.Windows.Forms.Label()
        Me.cmbMineDeepCore = New System.Windows.Forms.ComboBox()
        Me.lblMineAstrogeology = New System.Windows.Forms.Label()
        Me.cmbMineSkill = New System.Windows.Forms.ComboBox()
        Me.lblMineSkill = New System.Windows.Forms.Label()
        Me.cmbMineAstrogeology = New System.Windows.Forms.ComboBox()
        Me.lblMineDeepCore = New System.Windows.Forms.Label()
        Me.gbMineMain = New System.Windows.Forms.GroupBox()
        Me.gbMineIncludeOres = New System.Windows.Forms.GroupBox()
        Me.chkMineIncludeHighSec = New System.Windows.Forms.CheckBox()
        Me.chkMineIncludeNullSec = New System.Windows.Forms.CheckBox()
        Me.chkMineIncludeLowSec = New System.Windows.Forms.CheckBox()
        Me.chkMineIncludeHighYieldOre = New System.Windows.Forms.CheckBox()
        Me.cmbMineOreType = New System.Windows.Forms.ComboBox()
        Me.gbMineOreLocSov = New System.Windows.Forms.GroupBox()
        Me.chkMineMoonMining = New System.Windows.Forms.CheckBox()
        Me.chkMineTriglavian = New System.Windows.Forms.CheckBox()
        Me.chkMineWH = New System.Windows.Forms.CheckBox()
        Me.gbMineWHSpace = New System.Windows.Forms.GroupBox()
        Me.chkMineC6 = New System.Windows.Forms.CheckBox()
        Me.chkMineC5 = New System.Windows.Forms.CheckBox()
        Me.chkMineC4 = New System.Windows.Forms.CheckBox()
        Me.chkMineC3 = New System.Windows.Forms.CheckBox()
        Me.chkMineC2 = New System.Windows.Forms.CheckBox()
        Me.chkMineC1 = New System.Windows.Forms.CheckBox()
        Me.chkMineCaldari = New System.Windows.Forms.CheckBox()
        Me.chkMineMinmatar = New System.Windows.Forms.CheckBox()
        Me.chkMineGallente = New System.Windows.Forms.CheckBox()
        Me.chkMineAmarr = New System.Windows.Forms.CheckBox()
        Me.lblMineType = New System.Windows.Forms.Label()
        Me.lstMineGrid = New System.Windows.Forms.ListView()
        Me.tabDatacores = New System.Windows.Forms.TabPage()
        Me.lstDC = New System.Windows.Forms.ListView()
        Me.gbDCOptions = New System.Windows.Forms.GroupBox()
        Me.btnDCSaveSettings = New System.Windows.Forms.Button()
        Me.gbDCAgentLocSov = New System.Windows.Forms.GroupBox()
        Me.chkDCThukkerSov = New System.Windows.Forms.CheckBox()
        Me.chkDCKhanidSov = New System.Windows.Forms.CheckBox()
        Me.chkDCMinmatarSov = New System.Windows.Forms.CheckBox()
        Me.chkDCSyndicateSov = New System.Windows.Forms.CheckBox()
        Me.chkDCGallenteSov = New System.Windows.Forms.CheckBox()
        Me.chkDCAmarrSov = New System.Windows.Forms.CheckBox()
        Me.chkDCAmmatarSov = New System.Windows.Forms.CheckBox()
        Me.chkDCCaldariSov = New System.Windows.Forms.CheckBox()
        Me.gbDCTotalIPH = New System.Windows.Forms.GroupBox()
        Me.lblDCTotalOptIPH = New System.Windows.Forms.Label()
        Me.lblDCTotalSelectedIPH = New System.Windows.Forms.Label()
        Me.txtDCTotalSelectedIPH = New System.Windows.Forms.TextBox()
        Me.txtDCTotalOptIPH = New System.Windows.Forms.TextBox()
        Me.gbDCPrices = New System.Windows.Forms.GroupBox()
        Me.rbtnDCSystemPrices = New System.Windows.Forms.RadioButton()
        Me.rbtnDCRegionPrices = New System.Windows.Forms.RadioButton()
        Me.rbtnDCUpdatedPrices = New System.Windows.Forms.RadioButton()
        Me.gbDCAgentTypes = New System.Windows.Forms.GroupBox()
        Me.cmbDCRegions = New System.Windows.Forms.ComboBox()
        Me.lblDCRegion = New System.Windows.Forms.Label()
        Me.chkDCLowSecAgents = New System.Windows.Forms.CheckBox()
        Me.chkDCHighSecAgents = New System.Windows.Forms.CheckBox()
        Me.chkDCIncludeAllAgents = New System.Windows.Forms.CheckBox()
        Me.gbDCBaseSkills = New System.Windows.Forms.GroupBox()
        Me.cmbDCResearchMgmt = New System.Windows.Forms.ComboBox()
        Me.lblDCResearchManagement = New System.Windows.Forms.Label()
        Me.lblDCNegotiation = New System.Windows.Forms.Label()
        Me.cmbDCConnections = New System.Windows.Forms.ComboBox()
        Me.lblDCConnections = New System.Windows.Forms.Label()
        Me.cmbDCNegotiation = New System.Windows.Forms.ComboBox()
        Me.gbDCDatacores = New System.Windows.Forms.GroupBox()
        Me.cmbDCSkillLevel17 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel16 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel15 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel14 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel13 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel12 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel11 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel10 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel9 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel8 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel7 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel6 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel5 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel4 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel3 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel2 = New System.Windows.Forms.ComboBox()
        Me.cmbDCSkillLevel1 = New System.Windows.Forms.ComboBox()
        Me.chkDC17 = New System.Windows.Forms.CheckBox()
        Me.chkDC4 = New System.Windows.Forms.CheckBox()
        Me.chkDC16 = New System.Windows.Forms.CheckBox()
        Me.chkDC3 = New System.Windows.Forms.CheckBox()
        Me.lblDatacore17 = New System.Windows.Forms.Label()
        Me.chkDC15 = New System.Windows.Forms.CheckBox()
        Me.chkDC2 = New System.Windows.Forms.CheckBox()
        Me.chkDC14 = New System.Windows.Forms.CheckBox()
        Me.chkDC1 = New System.Windows.Forms.CheckBox()
        Me.chkDC13 = New System.Windows.Forms.CheckBox()
        Me.chkDC12 = New System.Windows.Forms.CheckBox()
        Me.chkDC11 = New System.Windows.Forms.CheckBox()
        Me.chkDC10 = New System.Windows.Forms.CheckBox()
        Me.lblDatacore16 = New System.Windows.Forms.Label()
        Me.lblDatacore4 = New System.Windows.Forms.Label()
        Me.lblDatacore15 = New System.Windows.Forms.Label()
        Me.chkDC9 = New System.Windows.Forms.CheckBox()
        Me.lblDatacore14 = New System.Windows.Forms.Label()
        Me.lblDatacore3 = New System.Windows.Forms.Label()
        Me.chkDC8 = New System.Windows.Forms.CheckBox()
        Me.lblDatacore13 = New System.Windows.Forms.Label()
        Me.lblDatacore2 = New System.Windows.Forms.Label()
        Me.chkDC7 = New System.Windows.Forms.CheckBox()
        Me.chkDC6 = New System.Windows.Forms.CheckBox()
        Me.lblDatacore1 = New System.Windows.Forms.Label()
        Me.chkDC5 = New System.Windows.Forms.CheckBox()
        Me.lblDatacore5 = New System.Windows.Forms.Label()
        Me.lblDatacore6 = New System.Windows.Forms.Label()
        Me.lblDatacore7 = New System.Windows.Forms.Label()
        Me.lblDatacore8 = New System.Windows.Forms.Label()
        Me.lblDatacore12 = New System.Windows.Forms.Label()
        Me.lblDatacore11 = New System.Windows.Forms.Label()
        Me.lblDatacore10 = New System.Windows.Forms.Label()
        Me.lblDatacore9 = New System.Windows.Forms.Label()
        Me.gbDCCodes = New System.Windows.Forms.GroupBox()
        Me.lblDCColors = New System.Windows.Forms.Label()
        Me.lblDCRedText = New System.Windows.Forms.Label()
        Me.lblDCOrangeText = New System.Windows.Forms.Label()
        Me.lblDCGrayText = New System.Windows.Forms.Label()
        Me.lblDCBlueText = New System.Windows.Forms.Label()
        Me.lblDCGreenBackColor = New System.Windows.Forms.Label()
        Me.gbDCCorpMinmatar = New System.Windows.Forms.GroupBox()
        Me.lblDCCorp13 = New System.Windows.Forms.Label()
        Me.chkDCCorp13 = New System.Windows.Forms.CheckBox()
        Me.txtDCStanding13 = New System.Windows.Forms.TextBox()
        Me.lblDCCorpLabel4 = New System.Windows.Forms.Label()
        Me.lblDCCorp10 = New System.Windows.Forms.Label()
        Me.lblDCCorp11 = New System.Windows.Forms.Label()
        Me.lblDCCorp12 = New System.Windows.Forms.Label()
        Me.chkDCCorp10 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp11 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp12 = New System.Windows.Forms.CheckBox()
        Me.txtDCStanding10 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding11 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding12 = New System.Windows.Forms.TextBox()
        Me.lblDCStanding4 = New System.Windows.Forms.Label()
        Me.btnDCExporttoClip = New System.Windows.Forms.Button()
        Me.gbDCCorpAmarr = New System.Windows.Forms.GroupBox()
        Me.lblDCCorpLabel1 = New System.Windows.Forms.Label()
        Me.lblDCCorp1 = New System.Windows.Forms.Label()
        Me.lblDCCorp2 = New System.Windows.Forms.Label()
        Me.lblDCCorp3 = New System.Windows.Forms.Label()
        Me.chkDCCorp1 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp2 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp3 = New System.Windows.Forms.CheckBox()
        Me.txtDCStanding1 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding2 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding3 = New System.Windows.Forms.TextBox()
        Me.lblDCStanding1 = New System.Windows.Forms.Label()
        Me.btnDCReset = New System.Windows.Forms.Button()
        Me.gbDCCorpsCaldari = New System.Windows.Forms.GroupBox()
        Me.lblDCCorpLabel2 = New System.Windows.Forms.Label()
        Me.lblDCCorp4 = New System.Windows.Forms.Label()
        Me.lblDCCorp5 = New System.Windows.Forms.Label()
        Me.lblDCCorp6 = New System.Windows.Forms.Label()
        Me.chkDCCorp4 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp5 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp6 = New System.Windows.Forms.CheckBox()
        Me.txtDCStanding4 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding5 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding6 = New System.Windows.Forms.TextBox()
        Me.lblDCStanding2 = New System.Windows.Forms.Label()
        Me.gbDCCorpsGallente = New System.Windows.Forms.GroupBox()
        Me.lblDCCorpLabel3 = New System.Windows.Forms.Label()
        Me.lblDCCorp7 = New System.Windows.Forms.Label()
        Me.lblDCCorp8 = New System.Windows.Forms.Label()
        Me.lblDCCorp9 = New System.Windows.Forms.Label()
        Me.chkDCCorp7 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp8 = New System.Windows.Forms.CheckBox()
        Me.chkDCCorp9 = New System.Windows.Forms.CheckBox()
        Me.txtDCStanding7 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding8 = New System.Windows.Forms.TextBox()
        Me.txtDCStanding9 = New System.Windows.Forms.TextBox()
        Me.lblDCStanding3 = New System.Windows.Forms.Label()
        Me.btnDCRefresh = New System.Windows.Forms.Button()
        Me.tabManufacturing = New System.Windows.Forms.TabPage()
        Me.lstManufacturing = New EVE_Isk_per_Hour.ManufacturingListView()
        Me.gbCalcBPSelectOptions = New System.Windows.Forms.GroupBox()
        Me.gbCalcIgnoreinCalcs = New System.Windows.Forms.GroupBox()
        Me.chkCalcIgnoreMinerals = New System.Windows.Forms.CheckBox()
        Me.chkCalcIgnoreT1Item = New System.Windows.Forms.CheckBox()
        Me.chkCalcIgnoreInvention = New System.Windows.Forms.CheckBox()
        Me.gbIncludeTaxesFees = New System.Windows.Forms.GroupBox()
        Me.txtCalcBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.chkCalcFees = New System.Windows.Forms.CheckBox()
        Me.chkCalcTaxes = New System.Windows.Forms.CheckBox()
        Me.gbCalcSellExessItems = New System.Windows.Forms.GroupBox()
        Me.rbtnCalcAdvT2MatType = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcProcT2MatType = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcRawT2MatType = New System.Windows.Forms.RadioButton()
        Me.chkCalcSellExessItems = New System.Windows.Forms.CheckBox()
        Me.chkCalcNPCBPOs = New System.Windows.Forms.CheckBox()
        Me.btnCalcShowAssets = New System.Windows.Forms.Button()
        Me.gbCalcIncludeItems = New System.Windows.Forms.GroupBox()
        Me.chkCalcCanInvent = New System.Windows.Forms.CheckBox()
        Me.chkCalcCanBuild = New System.Windows.Forms.CheckBox()
        Me.gbCalcMarketFilters = New System.Windows.Forms.GroupBox()
        Me.txtCalcProfitThreshold = New System.Windows.Forms.TextBox()
        Me.tpMaxBuildTimeFilter = New EVE_Isk_per_Hour.TimePicker()
        Me.txtCalcSVRThreshold = New System.Windows.Forms.TextBox()
        Me.tpMinBuildTimeFilter = New EVE_Isk_per_Hour.TimePicker()
        Me.chkCalcMaxBuildTimeFilter = New System.Windows.Forms.CheckBox()
        Me.chkCalcMinBuildTimeFilter = New System.Windows.Forms.CheckBox()
        Me.cmbCalcPriceTrend = New System.Windows.Forms.ComboBox()
        Me.cmbCalcAvgPriceDuration = New System.Windows.Forms.ComboBox()
        Me.lblCalcPriceTrend = New System.Windows.Forms.Label()
        Me.txtCalcVolumeThreshold = New System.Windows.Forms.TextBox()
        Me.cmbCalcHistoryRegion = New System.Windows.Forms.ComboBox()
        Me.lblCalcHistoryRegion = New System.Windows.Forms.Label()
        Me.lblCalcSVRThreshold = New System.Windows.Forms.Label()
        Me.lblCalcAvgPrice = New System.Windows.Forms.Label()
        Me.txtCalcIPHThreshold = New System.Windows.Forms.TextBox()
        Me.chkCalcProfitThreshold = New System.Windows.Forms.CheckBox()
        Me.chkCalcVolumeThreshold = New System.Windows.Forms.CheckBox()
        Me.chkCalcIPHThreshold = New System.Windows.Forms.CheckBox()
        Me.chkCalcSVRIncludeNull = New System.Windows.Forms.CheckBox()
        Me.btnCalcCalculate = New System.Windows.Forms.Button()
        Me.btnCalcSelectColumns = New System.Windows.Forms.Button()
        Me.gbCalcSizeLimit = New System.Windows.Forms.GroupBox()
        Me.chkCalcXL = New System.Windows.Forms.CheckBox()
        Me.chkCalcLarge = New System.Windows.Forms.CheckBox()
        Me.chkCalcMedium = New System.Windows.Forms.CheckBox()
        Me.chkCalcSmall = New System.Windows.Forms.CheckBox()
        Me.gbCalcProdLines = New System.Windows.Forms.GroupBox()
        Me.chkCalcAutoCalcT2NumBPs = New System.Windows.Forms.CheckBox()
        Me.lblCalcBPs = New System.Windows.Forms.Label()
        Me.txtCalcNumBPs = New System.Windows.Forms.TextBox()
        Me.txtCalcRuns = New System.Windows.Forms.TextBox()
        Me.txtCalcLabLines = New System.Windows.Forms.TextBox()
        Me.lblCalcRuns = New System.Windows.Forms.Label()
        Me.lblCalcLabLines1 = New System.Windows.Forms.Label()
        Me.lblCalcProdLines1 = New System.Windows.Forms.Label()
        Me.txtCalcProdLines = New System.Windows.Forms.TextBox()
        Me.gbCalcCompareType = New System.Windows.Forms.GroupBox()
        Me.chkCalcPPU = New System.Windows.Forms.CheckBox()
        Me.rbtnCalcCompareBuildBuy = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcCompareRawMats = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcCompareComponents = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcCompareAll = New System.Windows.Forms.RadioButton()
        Me.gbCalcTextColors = New System.Windows.Forms.GroupBox()
        Me.lblCalcColorCode6 = New System.Windows.Forms.Label()
        Me.lblCalcText = New System.Windows.Forms.Label()
        Me.lblCalcColorCode3 = New System.Windows.Forms.Label()
        Me.lblCalcColorCode4 = New System.Windows.Forms.Label()
        Me.lblCalcColorCode5 = New System.Windows.Forms.Label()
        Me.lblCalcColorCode2 = New System.Windows.Forms.Label()
        Me.lblCalcColorCode1 = New System.Windows.Forms.Label()
        Me.gbCalcInvention = New System.Windows.Forms.GroupBox()
        Me.chkCalcDecryptorforT3 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptorforT2 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor0 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor9 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor8 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor7 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor6 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor5 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor4 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor3 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor2 = New System.Windows.Forms.CheckBox()
        Me.chkCalcDecryptor1 = New System.Windows.Forms.CheckBox()
        Me.lblCalcDecryptorUse = New System.Windows.Forms.Label()
        Me.gbCalcBPRace = New System.Windows.Forms.GroupBox()
        Me.chkCalcRaceOther = New System.Windows.Forms.CheckBox()
        Me.chkCalcRacePirate = New System.Windows.Forms.CheckBox()
        Me.chkCalcRaceMinmatar = New System.Windows.Forms.CheckBox()
        Me.chkCalcRaceGallente = New System.Windows.Forms.CheckBox()
        Me.chkCalcRaceCaldari = New System.Windows.Forms.CheckBox()
        Me.chkCalcRaceAmarr = New System.Windows.Forms.CheckBox()
        Me.gbTempMEPE = New System.Windows.Forms.GroupBox()
        Me.txtCalcTempTE = New System.Windows.Forms.TextBox()
        Me.lblTempPE = New System.Windows.Forms.Label()
        Me.txtCalcTempME = New System.Windows.Forms.TextBox()
        Me.lblTempME = New System.Windows.Forms.Label()
        Me.tabCalcFacilities = New System.Windows.Forms.TabControl()
        Me.tabCalcFacilityBase = New System.Windows.Forms.TabPage()
        Me.CalcBaseFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityComponents = New System.Windows.Forms.TabPage()
        Me.CalcComponentsFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityCopy = New System.Windows.Forms.TabPage()
        Me.CalcCopyFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityT2Invention = New System.Windows.Forms.TabPage()
        Me.CalcInventionFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityT3Invention = New System.Windows.Forms.TabPage()
        Me.CalcT3InventionFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilitySupers = New System.Windows.Forms.TabPage()
        Me.CalcSupersFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityCapitals = New System.Windows.Forms.TabPage()
        Me.CalcCapitalsFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityT3Ships = New System.Windows.Forms.TabPage()
        Me.CalcT3ShipsFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilitySubsystems = New System.Windows.Forms.TabPage()
        Me.CalcSubsystemsFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityBoosters = New System.Windows.Forms.TabPage()
        Me.CalcBoostersFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityReactions = New System.Windows.Forms.TabPage()
        Me.CalcReactionsFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.gbCalcFilter = New System.Windows.Forms.GroupBox()
        Me.cmbCalcBPTypeFilter = New System.Windows.Forms.ComboBox()
        Me.gbCalcBPTech = New System.Windows.Forms.GroupBox()
        Me.chkCalcPirateFaction = New System.Windows.Forms.CheckBox()
        Me.chkCalcStoryline = New System.Windows.Forms.CheckBox()
        Me.chkCalcNavyFaction = New System.Windows.Forms.CheckBox()
        Me.chkCalcT3 = New System.Windows.Forms.CheckBox()
        Me.chkCalcT2 = New System.Windows.Forms.CheckBox()
        Me.chkCalcT1 = New System.Windows.Forms.CheckBox()
        Me.gbCalcIncludeOwned = New System.Windows.Forms.GroupBox()
        Me.chkCalcIncludeT3Owned = New System.Windows.Forms.CheckBox()
        Me.chkCalcIncludeT2Owned = New System.Windows.Forms.CheckBox()
        Me.btnCalcSaveSettings = New System.Windows.Forms.Button()
        Me.btnCalcExportList = New System.Windows.Forms.Button()
        Me.btnCalcPreview = New System.Windows.Forms.Button()
        Me.btnCalcReset = New System.Windows.Forms.Button()
        Me.gbCalcTextFilter = New System.Windows.Forms.GroupBox()
        Me.btnCalcResetTextSearch = New System.Windows.Forms.Button()
        Me.txtCalcItemFilter = New System.Windows.Forms.TextBox()
        Me.gbCalcBPType = New System.Windows.Forms.GroupBox()
        Me.chkCalcReactions = New System.Windows.Forms.CheckBox()
        Me.chkCalcCelestials = New System.Windows.Forms.CheckBox()
        Me.chkCalcMisc = New System.Windows.Forms.CheckBox()
        Me.chkCalcSubsystems = New System.Windows.Forms.CheckBox()
        Me.chkCalcDeployables = New System.Windows.Forms.CheckBox()
        Me.chkCalcStructures = New System.Windows.Forms.CheckBox()
        Me.chkCalcStructureRigs = New System.Windows.Forms.CheckBox()
        Me.chkCalcBoosters = New System.Windows.Forms.CheckBox()
        Me.chkCalcRigs = New System.Windows.Forms.CheckBox()
        Me.chkCalcComponents = New System.Windows.Forms.CheckBox()
        Me.chkCalcAmmo = New System.Windows.Forms.CheckBox()
        Me.chkCalcDrones = New System.Windows.Forms.CheckBox()
        Me.chkCalcModules = New System.Windows.Forms.CheckBox()
        Me.chkCalcShips = New System.Windows.Forms.CheckBox()
        Me.chkCalcStructureModules = New System.Windows.Forms.CheckBox()
        Me.gbCalcBPSelect = New System.Windows.Forms.GroupBox()
        Me.rbtnCalcBPFavorites = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcAllBPs = New System.Windows.Forms.RadioButton()
        Me.rbtnCalcBPOwned = New System.Windows.Forms.RadioButton()
        Me.gbCalcRelics = New System.Windows.Forms.GroupBox()
        Me.chkCalcRERelic2 = New System.Windows.Forms.CheckBox()
        Me.chkCalcRERelic3 = New System.Windows.Forms.CheckBox()
        Me.chkCalcRERelic1 = New System.Windows.Forms.CheckBox()
        Me.tabUpdatePrices = New System.Windows.Forms.TabPage()
        Me.gbRawMaterials = New System.Windows.Forms.GroupBox()
        Me.gbReactionMaterials = New System.Windows.Forms.GroupBox()
        Me.chkAdvancedMats = New System.Windows.Forms.CheckBox()
        Me.chkBoosterMats = New System.Windows.Forms.CheckBox()
        Me.chkMolecularForgedMaterials = New System.Windows.Forms.CheckBox()
        Me.chkPolymers = New System.Windows.Forms.CheckBox()
        Me.chkProcessedMats = New System.Windows.Forms.CheckBox()
        Me.chkRawMoonMats = New System.Windows.Forms.CheckBox()
        Me.gbResearchEquipment = New System.Windows.Forms.GroupBox()
        Me.chkRDb = New System.Windows.Forms.CheckBox()
        Me.chkAncientRelics = New System.Windows.Forms.CheckBox()
        Me.chkDecryptors = New System.Windows.Forms.CheckBox()
        Me.chkDatacores = New System.Windows.Forms.CheckBox()
        Me.chkMinerals = New System.Windows.Forms.CheckBox()
        Me.chkFactionMaterials = New System.Windows.Forms.CheckBox()
        Me.chkNamedComponents = New System.Windows.Forms.CheckBox()
        Me.chkMisc = New System.Windows.Forms.CheckBox()
        Me.chkMolecularForgingTools = New System.Windows.Forms.CheckBox()
        Me.chkAdvancedProtectiveTechnology = New System.Windows.Forms.CheckBox()
        Me.chkBPCs = New System.Windows.Forms.CheckBox()
        Me.chkRawMaterials = New System.Windows.Forms.CheckBox()
        Me.chkPriceMaterialResearchEqPrices = New System.Windows.Forms.CheckBox()
        Me.chkPlanetary = New System.Windows.Forms.CheckBox()
        Me.chkGas = New System.Windows.Forms.CheckBox()
        Me.chkSalvage = New System.Windows.Forms.CheckBox()
        Me.chkIceProducts = New System.Windows.Forms.CheckBox()
        Me.gbSingleSource = New System.Windows.Forms.GroupBox()
        Me.gbMarketStructures = New System.Windows.Forms.GroupBox()
        Me.btnAddStructureIDs = New System.Windows.Forms.Button()
        Me.btnViewSavedStructures = New System.Windows.Forms.Button()
        Me.gbRegionSystemPrice = New System.Windows.Forms.GroupBox()
        Me.cmbPriceRegions = New System.Windows.Forms.ComboBox()
        Me.cmbPriceSystems = New System.Windows.Forms.ComboBox()
        Me.gbTradeHubSystems = New System.Windows.Forms.GroupBox()
        Me.chkSystems6 = New System.Windows.Forms.CheckBox()
        Me.chkSystems5 = New System.Windows.Forms.CheckBox()
        Me.chkSystems4 = New System.Windows.Forms.CheckBox()
        Me.chkSystems3 = New System.Windows.Forms.CheckBox()
        Me.chkSystems2 = New System.Windows.Forms.CheckBox()
        Me.chkSystems1 = New System.Windows.Forms.CheckBox()
        Me.gbPriceProfile = New System.Windows.Forms.GroupBox()
        Me.tabPriceProfile = New System.Windows.Forms.TabControl()
        Me.tabPriceProfileRaw = New System.Windows.Forms.TabPage()
        Me.lstRawPriceProfile = New EVE_Isk_per_Hour.MyListView()
        Me.tabPriceProfileManufactured = New System.Windows.Forms.TabPage()
        Me.lstManufacturedPriceProfile = New EVE_Isk_per_Hour.MyListView()
        Me.gbPPDefaultSettings = New System.Windows.Forms.GroupBox()
        Me.btnPPUpdateDefaults = New System.Windows.Forms.Button()
        Me.cmbPPDefaultsPriceType = New System.Windows.Forms.ComboBox()
        Me.lblPPDefaultsSystem = New System.Windows.Forms.Label()
        Me.lblPPDefaultsPriceType = New System.Windows.Forms.Label()
        Me.cmbPPDefaultsSystem = New System.Windows.Forms.ComboBox()
        Me.cmbPPDefaultsRegion = New System.Windows.Forms.ComboBox()
        Me.lblPPDefaultsRegion = New System.Windows.Forms.Label()
        Me.txtPPDefaultsPriceMod = New System.Windows.Forms.TextBox()
        Me.lblPPDefaultsPriceMod = New System.Windows.Forms.Label()
        Me.btnLoadPricesfromFile = New System.Windows.Forms.Button()
        Me.btnSavePricestoFile = New System.Windows.Forms.Button()
        Me.lstPricesView = New EVE_Isk_per_Hour.MyListView()
        Me.txtPriceItemFilter = New System.Windows.Forms.TextBox()
        Me.gbPriceOptions = New System.Windows.Forms.GroupBox()
        Me.txtItemsPriceModifier = New System.Windows.Forms.TextBox()
        Me.txtRawPriceModifier = New System.Windows.Forms.TextBox()
        Me.gbPriceTypes = New System.Windows.Forms.GroupBox()
        Me.rbtnPriceSettingPriceProfile = New System.Windows.Forms.RadioButton()
        Me.rbtnPriceSettingSingleSelect = New System.Windows.Forms.RadioButton()
        Me.lblItemsPriceModifier = New System.Windows.Forms.Label()
        Me.lblRawPriceModifier = New System.Windows.Forms.Label()
        Me.gbDataSource = New System.Windows.Forms.GroupBox()
        Me.rbtnPriceSourceCCPData = New System.Windows.Forms.RadioButton()
        Me.rbtnPriceSourceEM = New System.Windows.Forms.RadioButton()
        Me.cmbItemsSplitPrices = New System.Windows.Forms.ComboBox()
        Me.cmbRawMatsSplitPrices = New System.Windows.Forms.ComboBox()
        Me.lblItemsSplitPrices = New System.Windows.Forms.Label()
        Me.lblRawMatsSplitPrices = New System.Windows.Forms.Label()
        Me.btnSaveUpdatePrices = New System.Windows.Forms.Button()
        Me.btnCancelUpdate = New System.Windows.Forms.Button()
        Me.btnClearItemFilter = New System.Windows.Forms.Button()
        Me.btnToggleAllPriceItems = New System.Windows.Forms.Button()
        Me.btnDownloadPrices = New System.Windows.Forms.Button()
        Me.lblItemFilter = New System.Windows.Forms.Label()
        Me.gbManufacturedItems = New System.Windows.Forms.GroupBox()
        Me.chkPriceManufacturedPrices = New System.Windows.Forms.CheckBox()
        Me.gbComponents = New System.Windows.Forms.GroupBox()
        Me.gbReprocessables = New System.Windows.Forms.GroupBox()
        Me.chkNoBuildItems = New System.Windows.Forms.CheckBox()
        Me.chkFuelBlocks = New System.Windows.Forms.CheckBox()
        Me.chkRAM = New System.Windows.Forms.CheckBox()
        Me.chkProtectiveComponents = New System.Windows.Forms.CheckBox()
        Me.chkSubsystemComponents = New System.Windows.Forms.CheckBox()
        Me.chkStructureComponents = New System.Windows.Forms.CheckBox()
        Me.chkComponents = New System.Windows.Forms.CheckBox()
        Me.chkCapitalShipComponents = New System.Windows.Forms.CheckBox()
        Me.chkCapT2Components = New System.Windows.Forms.CheckBox()
        Me.gbItems = New System.Windows.Forms.GroupBox()
        Me.chkCelestials = New System.Windows.Forms.CheckBox()
        Me.chkDeployables = New System.Windows.Forms.CheckBox()
        Me.cmbPriceChargeTypes = New System.Windows.Forms.ComboBox()
        Me.chkStructures = New System.Windows.Forms.CheckBox()
        Me.chkStructureRigs = New System.Windows.Forms.CheckBox()
        Me.chkCharges = New System.Windows.Forms.CheckBox()
        Me.chkBoosters = New System.Windows.Forms.CheckBox()
        Me.cmbPriceShipTypes = New System.Windows.Forms.ComboBox()
        Me.gbPricesTech = New System.Windows.Forms.GroupBox()
        Me.chkPricesT4 = New System.Windows.Forms.CheckBox()
        Me.chkPricesT6 = New System.Windows.Forms.CheckBox()
        Me.chkPricesT5 = New System.Windows.Forms.CheckBox()
        Me.chkPricesT3 = New System.Windows.Forms.CheckBox()
        Me.chkPricesT2 = New System.Windows.Forms.CheckBox()
        Me.chkPricesT1 = New System.Windows.Forms.CheckBox()
        Me.chkSubsystems = New System.Windows.Forms.CheckBox()
        Me.chkShips = New System.Windows.Forms.CheckBox()
        Me.chkModules = New System.Windows.Forms.CheckBox()
        Me.chkRigs = New System.Windows.Forms.CheckBox()
        Me.chkDrones = New System.Windows.Forms.CheckBox()
        Me.chkUpdatePricesNoPrice = New System.Windows.Forms.CheckBox()
        Me.chkImplants = New System.Windows.Forms.CheckBox()
        Me.chkStructureModules = New System.Windows.Forms.CheckBox()
        Me.btnOpenMarketBrowser = New System.Windows.Forms.Button()
        Me.tabBlueprints = New System.Windows.Forms.TabPage()
        Me.pbReactions = New System.Windows.Forms.PictureBox()
        Me.gbBPMEPEImage = New System.Windows.Forms.GroupBox()
        Me.gbBPSellExcess = New System.Windows.Forms.GroupBox()
        Me.btnBPListMats = New System.Windows.Forms.Button()
        Me.chkBPSellExcessItems = New System.Windows.Forms.CheckBox()
        Me.btnBPSaveBP = New System.Windows.Forms.Button()
        Me.tabBPInventionEquip = New System.Windows.Forms.TabControl()
        Me.tabFacility = New System.Windows.Forms.TabPage()
        Me.BPTabFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabT3Calcs = New System.Windows.Forms.TabPage()
        Me.chkBPOptimalT3Decryptor = New System.Windows.Forms.CheckBox()
        Me.lblBPT3Decryptor = New System.Windows.Forms.Label()
        Me.cmbBPT3Decryptor = New System.Windows.Forms.ComboBox()
        Me.lblBPT3Stats = New System.Windows.Forms.Label()
        Me.lblBPRelic = New System.Windows.Forms.Label()
        Me.txtBPRelicLines = New System.Windows.Forms.TextBox()
        Me.lblBPRelicLines = New System.Windows.Forms.Label()
        Me.lblBPRETime = New System.Windows.Forms.Label()
        Me.cmbBPRelic = New System.Windows.Forms.ComboBox()
        Me.lblBPRECost = New System.Windows.Forms.Label()
        Me.lblBPT3InventionChance = New System.Windows.Forms.Label()
        Me.lblBPT3InventionChance1 = New System.Windows.Forms.Label()
        Me.lblT3InventStatus = New System.Windows.Forms.Label()
        Me.chkBPIncludeT3Time = New System.Windows.Forms.CheckBox()
        Me.chkBPIncludeT3Costs = New System.Windows.Forms.CheckBox()
        Me.tabInventionCalcs = New System.Windows.Forms.TabPage()
        Me.chkBPOptimalT2Decryptor = New System.Windows.Forms.CheckBox()
        Me.lblBPCopyTime = New System.Windows.Forms.Label()
        Me.lblBPT2InventStatus = New System.Windows.Forms.Label()
        Me.lblBPCopyCosts = New System.Windows.Forms.Label()
        Me.txtBPInventionLines = New System.Windows.Forms.TextBox()
        Me.lblBPInventionLines = New System.Windows.Forms.Label()
        Me.lblInventionChance1 = New System.Windows.Forms.Label()
        Me.lblBPDecryptor = New System.Windows.Forms.Label()
        Me.lblBPInventionTime = New System.Windows.Forms.Label()
        Me.lblBPDecryptorStats = New System.Windows.Forms.Label()
        Me.lblBPInventionCost = New System.Windows.Forms.Label()
        Me.cmbBPInventionDecryptor = New System.Windows.Forms.ComboBox()
        Me.lblBPInventionChance = New System.Windows.Forms.Label()
        Me.chkBPIncludeInventionTime = New System.Windows.Forms.CheckBox()
        Me.chkBPIncludeCopyTime = New System.Windows.Forms.CheckBox()
        Me.chkBPIncludeCopyCosts = New System.Windows.Forms.CheckBox()
        Me.chkBPIncludeInventionCosts = New System.Windows.Forms.CheckBox()
        Me.btnBPSaveSettings = New System.Windows.Forms.Button()
        Me.txtBPLines = New System.Windows.Forms.TextBox()
        Me.pictBP = New System.Windows.Forms.PictureBox()
        Me.gbBPManualSystemCostIndex = New System.Windows.Forms.GroupBox()
        Me.btnBPUpdateCostIndex = New System.Windows.Forms.Button()
        Me.lblBPSystemCostIndexManual = New System.Windows.Forms.Label()
        Me.txtBPUpdateCostIndex = New System.Windows.Forms.TextBox()
        Me.txtBPNumBPs = New System.Windows.Forms.TextBox()
        Me.btnBPRefreshBP = New System.Windows.Forms.Button()
        Me.lblBPLines = New System.Windows.Forms.Label()
        Me.txtBPME = New System.Windows.Forms.TextBox()
        Me.lblBPRuns = New System.Windows.Forms.Label()
        Me.chkBPBuildBuy = New System.Windows.Forms.CheckBox()
        Me.txtBPRuns = New System.Windows.Forms.TextBox()
        Me.txtBPAddlCosts = New System.Windows.Forms.TextBox()
        Me.lblBPAddlCosts = New System.Windows.Forms.Label()
        Me.lblBPME = New System.Windows.Forms.Label()
        Me.txtBPTE = New System.Windows.Forms.TextBox()
        Me.lblBPPE = New System.Windows.Forms.Label()
        Me.lblBPNumBPs = New System.Windows.Forms.Label()
        Me.gbBPIgnoreinCalcs = New System.Windows.Forms.GroupBox()
        Me.chkBPIgnoreMinerals = New System.Windows.Forms.CheckBox()
        Me.chkBPIgnoreT1Item = New System.Windows.Forms.CheckBox()
        Me.chkBPIgnoreInvention = New System.Windows.Forms.CheckBox()
        Me.btnBPBuiltComponents = New System.Windows.Forms.Button()
        Me.btnBPComponents = New System.Windows.Forms.Button()
        Me.rbtnBPRawT2MatType = New System.Windows.Forms.RadioButton()
        Me.rbtnBPProcT2MatType = New System.Windows.Forms.RadioButton()
        Me.rbtnBPAdvT2MatType = New System.Windows.Forms.RadioButton()
        Me.lblBPT2MatTypeSelector = New System.Windows.Forms.Label()
        Me.lstBPList = New System.Windows.Forms.ListBox()
        Me.gbBPBlueprintType = New System.Windows.Forms.GroupBox()
        Me.chkBPNPCBPOs = New System.Windows.Forms.CheckBox()
        Me.rbtnBPReactionsBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPStructureModulesBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPCelestialsBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPMiscBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPStructureBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPFavoriteBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPStructureRigsBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPOwnedBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPRigBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPBoosterBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPSubsystemBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPModuleBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPAmmoChargeBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPDroneBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPComponentBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPAllBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPShipBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPDeployableBlueprints = New System.Windows.Forms.RadioButton()
        Me.gbBPBlueprintTech = New System.Windows.Forms.GroupBox()
        Me.chkBPPirateFaction = New System.Windows.Forms.CheckBox()
        Me.chkBPStoryline = New System.Windows.Forms.CheckBox()
        Me.chkBPNavyFaction = New System.Windows.Forms.CheckBox()
        Me.chkBPT3 = New System.Windows.Forms.CheckBox()
        Me.chkBPT2 = New System.Windows.Forms.CheckBox()
        Me.chkBPT1 = New System.Windows.Forms.CheckBox()
        Me.gbFilters = New System.Windows.Forms.GroupBox()
        Me.chkBPXL = New System.Windows.Forms.CheckBox()
        Me.chkBPLarge = New System.Windows.Forms.CheckBox()
        Me.chkBPMedium = New System.Windows.Forms.CheckBox()
        Me.chkBPSmall = New System.Windows.Forms.CheckBox()
        Me.cmbBPBlueprintSelection = New System.Windows.Forms.ComboBox()
        Me.btnBPListView = New System.Windows.Forms.Button()
        Me.btnBPForward = New System.Windows.Forms.Button()
        Me.btnBPBack = New System.Windows.Forms.Button()
        Me.lblBPSelectBlueprint = New System.Windows.Forms.Label()
        Me.gbBPInventionStats = New System.Windows.Forms.GroupBox()
        Me.txtBPBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.txtBPMarketPriceEdit = New System.Windows.Forms.TextBox()
        Me.lblBPProductionTime = New System.Windows.Forms.Label()
        Me.lblBPTotalUnits = New System.Windows.Forms.Label()
        Me.lblBPTaxes = New System.Windows.Forms.Label()
        Me.lblBPTotalUnits1 = New System.Windows.Forms.Label()
        Me.lblBPBrokerFees = New System.Windows.Forms.Label()
        Me.lblBPPT = New System.Windows.Forms.Label()
        Me.chkBPTaxes = New System.Windows.Forms.CheckBox()
        Me.lblBPMarketCost = New System.Windows.Forms.Label()
        Me.lblBPMarketCost1 = New System.Windows.Forms.Label()
        Me.lblBPRawTotalCost = New System.Windows.Forms.Label()
        Me.lblBPCompProfit = New System.Windows.Forms.Label()
        Me.lblBPRawTotalCost1 = New System.Windows.Forms.Label()
        Me.chkBPBrokerFees = New System.Windows.Forms.CheckBox()
        Me.lblBPCompIPH = New System.Windows.Forms.Label()
        Me.lblBPRawIPH = New System.Windows.Forms.Label()
        Me.lblBPTotalCompCost1 = New System.Windows.Forms.Label()
        Me.lblBPCompIPH1 = New System.Windows.Forms.Label()
        Me.lblBPTotalItemPT = New System.Windows.Forms.Label()
        Me.lblBPTotalCompCost = New System.Windows.Forms.Label()
        Me.lblBPCPTPT = New System.Windows.Forms.Label()
        Me.lblBPRawSVR = New System.Windows.Forms.Label()
        Me.lblBPRawIPH1 = New System.Windows.Forms.Label()
        Me.lblBPRawProfit = New System.Windows.Forms.Label()
        Me.lblBPBPSVR = New System.Windows.Forms.Label()
        Me.lblBPCompProfit1 = New System.Windows.Forms.Label()
        Me.lblBPRawProfit1 = New System.Windows.Forms.Label()
        Me.lblBPBPSVR1 = New System.Windows.Forms.Label()
        Me.lblBPRawSVR1 = New System.Windows.Forms.Label()
        Me.chkBPPricePerUnit = New System.Windows.Forms.CheckBox()
        Me.lblBPBuyColor = New System.Windows.Forms.Label()
        Me.lblBPBuildColor = New System.Windows.Forms.Label()
        Me.gbBPShopandCopy = New System.Windows.Forms.GroupBox()
        Me.chkBPSimpleCopy = New System.Windows.Forms.CheckBox()
        Me.rbtnBPCopyInvREMats = New System.Windows.Forms.RadioButton()
        Me.rbtnBPComponentCopy = New System.Windows.Forms.RadioButton()
        Me.rbtnBPRawmatCopy = New System.Windows.Forms.RadioButton()
        Me.btnBPCopyMatstoClip = New System.Windows.Forms.Button()
        Me.btnBPAddBPMatstoShoppingList = New System.Windows.Forms.Button()
        Me.lblBPSimpleCopy = New System.Windows.Forms.Label()
        Me.lblBPCanMakeBPAll = New System.Windows.Forms.Label()
        Me.lblBPRawMatCost = New System.Windows.Forms.Label()
        Me.lblBPRawMatCost1 = New System.Windows.Forms.Label()
        Me.lblBPCanMakeBP = New System.Windows.Forms.Label()
        Me.lblBPRawMats = New System.Windows.Forms.Label()
        Me.lblBPComponentMatCost = New System.Windows.Forms.Label()
        Me.lblBPComponentMats = New System.Windows.Forms.Label()
        Me.lblBPComponentMatCost1 = New System.Windows.Forms.Label()
        Me.lstBPComponentMats = New EVE_Isk_per_Hour.MyListView()
        Me.lstBPRawMats = New EVE_Isk_per_Hour.MyListView()
        Me.lstBPBuiltComponents = New EVE_Isk_per_Hour.MyListView()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.mnuStripMain.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.ListOptionsMenu.SuspendLayout()
        Me.tabPI.SuspendLayout()
        Me.gbPIPlanets.SuspendLayout()
        Me.tabMining.SuspendLayout()
        Me.gbMineCrystalType.SuspendLayout()
        Me.tabMiningDrones.SuspendLayout()
        Me.tabShipDrones.SuspendLayout()
        Me.tabBoosterDrones.SuspendLayout()
        Me.gbMineCrystals.SuspendLayout()
        Me.gbMineNumberMiners.SuspendLayout()
        Me.gbMineOreProcessingType.SuspendLayout()
        Me.gbMineTaxBroker.SuspendLayout()
        Me.gbMineStripStats.SuspendLayout()
        Me.gbMineHauling.SuspendLayout()
        Me.gbMineBooster.SuspendLayout()
        CType(Me.pictMineLaserOptmize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictMineRangeLink, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictMineFleetBoostShip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMineRefining.SuspendLayout()
        Me.gbMineOreStuctureRates.SuspendLayout()
        Me.tabMiningProcessingSkills.SuspendLayout()
        Me.tabPageOres.SuspendLayout()
        Me.tabPageMoonOres.SuspendLayout()
        Me.tabPageIce.SuspendLayout()
        Me.gbMineShipSetup.SuspendLayout()
        Me.gbMineSelectShip.SuspendLayout()
        CType(Me.pictMineSelectedShip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMineShipEquipment.SuspendLayout()
        Me.gbMiningRigs.SuspendLayout()
        Me.gbMineSkills.SuspendLayout()
        Me.gbMineMain.SuspendLayout()
        Me.gbMineIncludeOres.SuspendLayout()
        Me.gbMineOreLocSov.SuspendLayout()
        Me.gbMineWHSpace.SuspendLayout()
        Me.tabDatacores.SuspendLayout()
        Me.gbDCOptions.SuspendLayout()
        Me.gbDCAgentLocSov.SuspendLayout()
        Me.gbDCTotalIPH.SuspendLayout()
        Me.gbDCPrices.SuspendLayout()
        Me.gbDCAgentTypes.SuspendLayout()
        Me.gbDCBaseSkills.SuspendLayout()
        Me.gbDCDatacores.SuspendLayout()
        Me.gbDCCodes.SuspendLayout()
        Me.gbDCCorpMinmatar.SuspendLayout()
        Me.gbDCCorpAmarr.SuspendLayout()
        Me.gbDCCorpsCaldari.SuspendLayout()
        Me.gbDCCorpsGallente.SuspendLayout()
        Me.tabManufacturing.SuspendLayout()
        Me.gbCalcBPSelectOptions.SuspendLayout()
        Me.gbCalcIgnoreinCalcs.SuspendLayout()
        Me.gbIncludeTaxesFees.SuspendLayout()
        Me.gbCalcSellExessItems.SuspendLayout()
        Me.gbCalcIncludeItems.SuspendLayout()
        Me.gbCalcMarketFilters.SuspendLayout()
        Me.gbCalcSizeLimit.SuspendLayout()
        Me.gbCalcProdLines.SuspendLayout()
        Me.gbCalcCompareType.SuspendLayout()
        Me.gbCalcTextColors.SuspendLayout()
        Me.gbCalcInvention.SuspendLayout()
        Me.gbCalcBPRace.SuspendLayout()
        Me.gbTempMEPE.SuspendLayout()
        Me.tabCalcFacilities.SuspendLayout()
        Me.tabCalcFacilityBase.SuspendLayout()
        Me.tabCalcFacilityComponents.SuspendLayout()
        Me.tabCalcFacilityCopy.SuspendLayout()
        Me.tabCalcFacilityT2Invention.SuspendLayout()
        Me.tabCalcFacilityT3Invention.SuspendLayout()
        Me.tabCalcFacilitySupers.SuspendLayout()
        Me.tabCalcFacilityCapitals.SuspendLayout()
        Me.tabCalcFacilityT3Ships.SuspendLayout()
        Me.tabCalcFacilitySubsystems.SuspendLayout()
        Me.tabCalcFacilityBoosters.SuspendLayout()
        Me.tabCalcFacilityReactions.SuspendLayout()
        Me.gbCalcFilter.SuspendLayout()
        Me.gbCalcBPTech.SuspendLayout()
        Me.gbCalcIncludeOwned.SuspendLayout()
        Me.gbCalcTextFilter.SuspendLayout()
        Me.gbCalcBPType.SuspendLayout()
        Me.gbCalcBPSelect.SuspendLayout()
        Me.gbCalcRelics.SuspendLayout()
        Me.tabUpdatePrices.SuspendLayout()
        Me.gbRawMaterials.SuspendLayout()
        Me.gbReactionMaterials.SuspendLayout()
        Me.gbResearchEquipment.SuspendLayout()
        Me.gbSingleSource.SuspendLayout()
        Me.gbMarketStructures.SuspendLayout()
        Me.gbRegionSystemPrice.SuspendLayout()
        Me.gbTradeHubSystems.SuspendLayout()
        Me.gbPriceProfile.SuspendLayout()
        Me.tabPriceProfile.SuspendLayout()
        Me.tabPriceProfileRaw.SuspendLayout()
        Me.tabPriceProfileManufactured.SuspendLayout()
        Me.gbPPDefaultSettings.SuspendLayout()
        Me.gbPriceOptions.SuspendLayout()
        Me.gbPriceTypes.SuspendLayout()
        Me.gbDataSource.SuspendLayout()
        Me.gbManufacturedItems.SuspendLayout()
        Me.gbComponents.SuspendLayout()
        Me.gbReprocessables.SuspendLayout()
        Me.gbItems.SuspendLayout()
        Me.gbPricesTech.SuspendLayout()
        Me.tabBlueprints.SuspendLayout()
        CType(Me.pbReactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbBPMEPEImage.SuspendLayout()
        Me.gbBPSellExcess.SuspendLayout()
        Me.tabBPInventionEquip.SuspendLayout()
        Me.tabFacility.SuspendLayout()
        Me.tabT3Calcs.SuspendLayout()
        Me.tabInventionCalcs.SuspendLayout()
        CType(Me.pictBP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbBPManualSystemCostIndex.SuspendLayout()
        Me.gbBPIgnoreinCalcs.SuspendLayout()
        Me.gbBPBlueprintType.SuspendLayout()
        Me.gbBPBlueprintTech.SuspendLayout()
        Me.gbFilters.SuspendLayout()
        Me.gbBPInventionStats.SuspendLayout()
        Me.gbBPShopandCopy.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuStripMain
        '
        Me.mnuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuUpdateData, Me.ViewToolStripMenuItem, Me.mnuTools, Me.mnuSettings, Me.mnuAbout})
        Me.mnuStripMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuStripMain.Name = "mnuStripMain"
        Me.mnuStripMain.Size = New System.Drawing.Size(1149, 24)
        Me.mnuStripMain.TabIndex = 0
        Me.mnuStripMain.Text = "MainMenu"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSelectionAddChar, Me.mnuSelectionManageCharacters, Me.mnuViewErrorLog, Me.ToolStripSeparator1, Me.mnuSelectionExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuSelectionAddChar
        '
        Me.mnuSelectionAddChar.Name = "mnuSelectionAddChar"
        Me.mnuSelectionAddChar.Size = New System.Drawing.Size(170, 22)
        Me.mnuSelectionAddChar.Text = "Add Character"
        '
        'mnuSelectionManageCharacters
        '
        Me.mnuSelectionManageCharacters.Name = "mnuSelectionManageCharacters"
        Me.mnuSelectionManageCharacters.Size = New System.Drawing.Size(170, 22)
        Me.mnuSelectionManageCharacters.Text = "Manage Accounts"
        '
        'mnuViewErrorLog
        '
        Me.mnuViewErrorLog.Name = "mnuViewErrorLog"
        Me.mnuViewErrorLog.Size = New System.Drawing.Size(170, 22)
        Me.mnuViewErrorLog.Text = "View Error Log"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(167, 6)
        '
        'mnuSelectionExit
        '
        Me.mnuSelectionExit.Name = "mnuSelectionExit"
        Me.mnuSelectionExit.Size = New System.Drawing.Size(170, 22)
        Me.mnuSelectionExit.Text = "Exit"
        '
        'mnuEdit
        '
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItemUpdatePrices, Me.SetPOSDataToolStripMenuItem, Me.mnuManageBlueprintsToolStripMenuItem, Me.mnuClearBPHistory})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuEdit.Text = "Edit"
        '
        'mnuItemUpdatePrices
        '
        Me.mnuItemUpdatePrices.Name = "mnuItemUpdatePrices"
        Me.mnuItemUpdatePrices.Size = New System.Drawing.Size(173, 22)
        Me.mnuItemUpdatePrices.Text = "Prices"
        Me.mnuItemUpdatePrices.Visible = False
        '
        'SetPOSDataToolStripMenuItem
        '
        Me.SetPOSDataToolStripMenuItem.Name = "SetPOSDataToolStripMenuItem"
        Me.SetPOSDataToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.SetPOSDataToolStripMenuItem.Text = "POS Data"
        Me.SetPOSDataToolStripMenuItem.Visible = False
        '
        'mnuManageBlueprintsToolStripMenuItem
        '
        Me.mnuManageBlueprintsToolStripMenuItem.Name = "mnuManageBlueprintsToolStripMenuItem"
        Me.mnuManageBlueprintsToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.mnuManageBlueprintsToolStripMenuItem.Text = "Manage Blueprints"
        '
        'mnuClearBPHistory
        '
        Me.mnuClearBPHistory.Name = "mnuClearBPHistory"
        Me.mnuClearBPHistory.Size = New System.Drawing.Size(173, 22)
        Me.mnuClearBPHistory.Text = "Clear BP History"
        '
        'mnuUpdateData
        '
        Me.mnuUpdateData.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUpdateIndustryFacilities, Me.mnuUpdateESIMarketPrices, Me.mnuUpdateESIPublicStructures, Me.mnuChangeDummyCharacterName, Me.ToolStripSeparator6, Me.mnuResetData})
        Me.mnuUpdateData.Name = "mnuUpdateData"
        Me.mnuUpdateData.Size = New System.Drawing.Size(43, 20)
        Me.mnuUpdateData.Text = "Data"
        '
        'mnuUpdateIndustryFacilities
        '
        Me.mnuUpdateIndustryFacilities.Name = "mnuUpdateIndustryFacilities"
        Me.mnuUpdateIndustryFacilities.Size = New System.Drawing.Size(250, 22)
        Me.mnuUpdateIndustryFacilities.Text = "Update Industry Facilities"
        '
        'mnuUpdateESIMarketPrices
        '
        Me.mnuUpdateESIMarketPrices.Name = "mnuUpdateESIMarketPrices"
        Me.mnuUpdateESIMarketPrices.Size = New System.Drawing.Size(250, 22)
        Me.mnuUpdateESIMarketPrices.Text = "Update Adjusted Market Prices"
        '
        'mnuUpdateESIPublicStructures
        '
        Me.mnuUpdateESIPublicStructures.Name = "mnuUpdateESIPublicStructures"
        Me.mnuUpdateESIPublicStructures.Size = New System.Drawing.Size(250, 22)
        Me.mnuUpdateESIPublicStructures.Text = "Update Public Structures"
        '
        'mnuChangeDummyCharacterName
        '
        Me.mnuChangeDummyCharacterName.Name = "mnuChangeDummyCharacterName"
        Me.mnuChangeDummyCharacterName.Size = New System.Drawing.Size(250, 22)
        Me.mnuChangeDummyCharacterName.Text = "Change Dummy Character Name"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(247, 6)
        '
        'mnuResetData
        '
        Me.mnuResetData.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuResetBlueprintData, Me.mnuResetIgnoredBPs, Me.mnuResetPriceData, Me.mnuResetAgents, Me.mnuResetIndustryJobs, Me.mnuResetAssets, Me.mnuResetMarketHistory, Me.mnuResetMarketOrders, Me.mnuResetESIPublicStructures, Me.mnuResetSavedFacilities, Me.mnuResetESIIndustryFacilities, Me.mnuResetESIMarketPrices, Me.mnuResetESIDates, Me.ToolStripSeparator4, Me.mnuResetAllData})
        Me.mnuResetData.Name = "mnuResetData"
        Me.mnuResetData.Size = New System.Drawing.Size(250, 22)
        Me.mnuResetData.Text = "Reset Data"
        '
        'mnuResetBlueprintData
        '
        Me.mnuResetBlueprintData.Name = "mnuResetBlueprintData"
        Me.mnuResetBlueprintData.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetBlueprintData.Text = "Reset Blueprint Data"
        '
        'mnuResetIgnoredBPs
        '
        Me.mnuResetIgnoredBPs.Name = "mnuResetIgnoredBPs"
        Me.mnuResetIgnoredBPs.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetIgnoredBPs.Text = "Reset All Ignored BPs"
        '
        'mnuResetPriceData
        '
        Me.mnuResetPriceData.Name = "mnuResetPriceData"
        Me.mnuResetPriceData.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetPriceData.Text = "Reset Price Data"
        '
        'mnuResetAgents
        '
        Me.mnuResetAgents.Name = "mnuResetAgents"
        Me.mnuResetAgents.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetAgents.Text = "Reset Research Agents"
        '
        'mnuResetIndustryJobs
        '
        Me.mnuResetIndustryJobs.Name = "mnuResetIndustryJobs"
        Me.mnuResetIndustryJobs.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetIndustryJobs.Text = "Reset Industry Jobs"
        '
        'mnuResetAssets
        '
        Me.mnuResetAssets.Name = "mnuResetAssets"
        Me.mnuResetAssets.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetAssets.Text = "Reset Assets"
        '
        'mnuResetMarketHistory
        '
        Me.mnuResetMarketHistory.Name = "mnuResetMarketHistory"
        Me.mnuResetMarketHistory.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetMarketHistory.Text = "Reset Market History"
        '
        'mnuResetMarketOrders
        '
        Me.mnuResetMarketOrders.Name = "mnuResetMarketOrders"
        Me.mnuResetMarketOrders.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetMarketOrders.Text = "Reset Market Orders"
        '
        'mnuResetESIPublicStructures
        '
        Me.mnuResetESIPublicStructures.Name = "mnuResetESIPublicStructures"
        Me.mnuResetESIPublicStructures.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetESIPublicStructures.Text = "Reset Public Structures"
        '
        'mnuResetSavedFacilities
        '
        Me.mnuResetSavedFacilities.Name = "mnuResetSavedFacilities"
        Me.mnuResetSavedFacilities.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetSavedFacilities.Text = "Reset Saved Facilities"
        '
        'mnuResetESIIndustryFacilities
        '
        Me.mnuResetESIIndustryFacilities.Name = "mnuResetESIIndustryFacilities"
        Me.mnuResetESIIndustryFacilities.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetESIIndustryFacilities.Text = "Reset Industry System Indicies (ESI)"
        '
        'mnuResetESIMarketPrices
        '
        Me.mnuResetESIMarketPrices.Name = "mnuResetESIMarketPrices"
        Me.mnuResetESIMarketPrices.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetESIMarketPrices.Text = "Reset Adjusted Market Prices (ESI)"
        '
        'mnuResetESIDates
        '
        Me.mnuResetESIDates.Name = "mnuResetESIDates"
        Me.mnuResetESIDates.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetESIDates.Text = "Reset All ESI Cache Dates"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(255, 6)
        '
        'mnuResetAllData
        '
        Me.mnuResetAllData.Name = "mnuResetAllData"
        Me.mnuResetAllData.Size = New System.Drawing.Size(258, 22)
        Me.mnuResetAllData.Text = "Reset All Data"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuViewAssets, Me.mnuSelectionShoppingList, Me.mnuCharacterSkills, Me.mnuCharacterStandings, Me.ToolStripSeparator5, Me.mnuCurrentResearchAgents, Me.mnuCurrentIndustryJobs, Me.ToolStripSeparator3, Me.mnuViewESIStatus})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'mnuViewAssets
        '
        Me.mnuViewAssets.Name = "mnuViewAssets"
        Me.mnuViewAssets.Size = New System.Drawing.Size(204, 22)
        Me.mnuViewAssets.Text = "Assets"
        '
        'mnuSelectionShoppingList
        '
        Me.mnuSelectionShoppingList.Name = "mnuSelectionShoppingList"
        Me.mnuSelectionShoppingList.Size = New System.Drawing.Size(204, 22)
        Me.mnuSelectionShoppingList.Text = "Shopping List"
        '
        'mnuCharacterSkills
        '
        Me.mnuCharacterSkills.Name = "mnuCharacterSkills"
        Me.mnuCharacterSkills.Size = New System.Drawing.Size(204, 22)
        Me.mnuCharacterSkills.Text = "Character Skills"
        '
        'mnuCharacterStandings
        '
        Me.mnuCharacterStandings.Name = "mnuCharacterStandings"
        Me.mnuCharacterStandings.Size = New System.Drawing.Size(204, 22)
        Me.mnuCharacterStandings.Text = "Character Standings"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(201, 6)
        '
        'mnuCurrentResearchAgents
        '
        Me.mnuCurrentResearchAgents.Name = "mnuCurrentResearchAgents"
        Me.mnuCurrentResearchAgents.Size = New System.Drawing.Size(204, 22)
        Me.mnuCurrentResearchAgents.Text = "Current Research Agents"
        '
        'mnuCurrentIndustryJobs
        '
        Me.mnuCurrentIndustryJobs.Name = "mnuCurrentIndustryJobs"
        Me.mnuCurrentIndustryJobs.Size = New System.Drawing.Size(204, 22)
        Me.mnuCurrentIndustryJobs.Text = "Current Industry Jobs"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(201, 6)
        '
        'mnuViewESIStatus
        '
        Me.mnuViewESIStatus.Name = "mnuViewESIStatus"
        Me.mnuViewESIStatus.Size = New System.Drawing.Size(204, 22)
        Me.mnuViewESIStatus.Text = "View ESI Status"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMETECalculator, Me.mnuReprocessingPlant, Me.mnuOreFlips})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(46, 20)
        Me.mnuTools.Text = "Tools"
        '
        'mnuMETECalculator
        '
        Me.mnuMETECalculator.Name = "mnuMETECalculator"
        Me.mnuMETECalculator.Size = New System.Drawing.Size(219, 22)
        Me.mnuMETECalculator.Text = "ME/TE Calculator"
        Me.mnuMETECalculator.Visible = False
        '
        'mnuReprocessingPlant
        '
        Me.mnuReprocessingPlant.Name = "mnuReprocessingPlant"
        Me.mnuReprocessingPlant.Size = New System.Drawing.Size(219, 22)
        Me.mnuReprocessingPlant.Text = "Reprocessing Plant"
        '
        'mnuOreFlips
        '
        Me.mnuOreFlips.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAnomalyOreBelts, Me.mnuIceBelts})
        Me.mnuOreFlips.Name = "mnuOreFlips"
        Me.mnuOreFlips.Size = New System.Drawing.Size(219, 22)
        Me.mnuOreFlips.Text = "Mining Belt Flip Calculators"
        Me.mnuOreFlips.Visible = False
        '
        'mnuAnomalyOreBelts
        '
        Me.mnuAnomalyOreBelts.Name = "mnuAnomalyOreBelts"
        Me.mnuAnomalyOreBelts.Size = New System.Drawing.Size(180, 22)
        Me.mnuAnomalyOreBelts.Text = "Ore Soverignty Belts"
        '
        'mnuIceBelts
        '
        Me.mnuIceBelts.Name = "mnuIceBelts"
        Me.mnuIceBelts.Size = New System.Drawing.Size(180, 22)
        Me.mnuIceBelts.Text = "Ice Belts"
        '
        'mnuSettings
        '
        Me.mnuSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUserSettings, Me.mnuSelectDefaultChar, Me.mnuRestoreDefaultTabSettings, Me.mnuResetBuildBuyManualSelections})
        Me.mnuSettings.Name = "mnuSettings"
        Me.mnuSettings.Size = New System.Drawing.Size(61, 20)
        Me.mnuSettings.Text = "Settings"
        '
        'mnuUserSettings
        '
        Me.mnuUserSettings.Name = "mnuUserSettings"
        Me.mnuUserSettings.Size = New System.Drawing.Size(256, 22)
        Me.mnuUserSettings.Text = "Select Application Settings"
        '
        'mnuSelectDefaultChar
        '
        Me.mnuSelectDefaultChar.Name = "mnuSelectDefaultChar"
        Me.mnuSelectDefaultChar.Size = New System.Drawing.Size(256, 22)
        Me.mnuSelectDefaultChar.Text = "Select Default Character"
        '
        'mnuRestoreDefaultTabSettings
        '
        Me.mnuRestoreDefaultTabSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRestoreDefaultBP, Me.mnuRestoreDefaultUpdatePrices, Me.mnuRestoreDefaultManufacturing, Me.mnuRestoreDefaultDatacores, Me.mnuRestoreDefaultMining})
        Me.mnuRestoreDefaultTabSettings.Name = "mnuRestoreDefaultTabSettings"
        Me.mnuRestoreDefaultTabSettings.Size = New System.Drawing.Size(256, 22)
        Me.mnuRestoreDefaultTabSettings.Text = "Restore Default Tab Settings"
        '
        'mnuRestoreDefaultBP
        '
        Me.mnuRestoreDefaultBP.Name = "mnuRestoreDefaultBP"
        Me.mnuRestoreDefaultBP.Size = New System.Drawing.Size(153, 22)
        Me.mnuRestoreDefaultBP.Text = "Blueprints"
        '
        'mnuRestoreDefaultUpdatePrices
        '
        Me.mnuRestoreDefaultUpdatePrices.Name = "mnuRestoreDefaultUpdatePrices"
        Me.mnuRestoreDefaultUpdatePrices.Size = New System.Drawing.Size(153, 22)
        Me.mnuRestoreDefaultUpdatePrices.Text = "Update Prices"
        '
        'mnuRestoreDefaultManufacturing
        '
        Me.mnuRestoreDefaultManufacturing.Name = "mnuRestoreDefaultManufacturing"
        Me.mnuRestoreDefaultManufacturing.Size = New System.Drawing.Size(153, 22)
        Me.mnuRestoreDefaultManufacturing.Text = "Manufacturing"
        '
        'mnuRestoreDefaultDatacores
        '
        Me.mnuRestoreDefaultDatacores.Name = "mnuRestoreDefaultDatacores"
        Me.mnuRestoreDefaultDatacores.Size = New System.Drawing.Size(153, 22)
        Me.mnuRestoreDefaultDatacores.Text = "Datacores"
        '
        'mnuRestoreDefaultMining
        '
        Me.mnuRestoreDefaultMining.Name = "mnuRestoreDefaultMining"
        Me.mnuRestoreDefaultMining.Size = New System.Drawing.Size(153, 22)
        Me.mnuRestoreDefaultMining.Text = "Mining"
        '
        'mnuResetBuildBuyManualSelections
        '
        Me.mnuResetBuildBuyManualSelections.Name = "mnuResetBuildBuyManualSelections"
        Me.mnuResetBuildBuyManualSelections.Size = New System.Drawing.Size(256, 22)
        Me.mnuResetBuildBuyManualSelections.Text = "Reset Build/Buy Manual Selections"
        '
        'mnuAbout
        '
        Me.mnuAbout.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPatchNotes, Me.mnuCheckforUpdates, Me.ToolStripSeparator2, Me.mnuSelectionAbout})
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(52, 20)
        Me.mnuAbout.Text = "About"
        '
        'mnuPatchNotes
        '
        Me.mnuPatchNotes.Name = "mnuPatchNotes"
        Me.mnuPatchNotes.Size = New System.Drawing.Size(171, 22)
        Me.mnuPatchNotes.Text = "View Patch Notes"
        '
        'mnuCheckforUpdates
        '
        Me.mnuCheckforUpdates.Name = "mnuCheckforUpdates"
        Me.mnuCheckforUpdates.Size = New System.Drawing.Size(171, 22)
        Me.mnuCheckforUpdates.Text = "Check for Updates"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(168, 6)
        '
        'mnuSelectionAbout
        '
        Me.mnuSelectionAbout.Name = "mnuSelectionAbout"
        Me.mnuSelectionAbout.Size = New System.Drawing.Size(171, 22)
        Me.mnuSelectionAbout.Text = "About IPH"
        '
        'pnlMain
        '
        Me.pnlMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCharacter, Me.pnlSkills, Me.pnlShoppingList, Me.pnlStatus, Me.pnlProgressBar})
        Me.pnlMain.Location = New System.Drawing.Point(0, 669)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1149, 22)
        Me.pnlMain.SizingGrip = False
        Me.pnlMain.TabIndex = 1
        Me.pnlMain.Text = "pnlMain"
        '
        'mnuCharacter
        '
        Me.mnuCharacter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.mnuCharacter.AutoSize = False
        Me.mnuCharacter.AutoToolTip = False
        Me.mnuCharacter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuCharacter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsCharacter1, Me.tsCharacter2, Me.tsCharacter3, Me.tsCharacter4, Me.tsCharacter5, Me.tsCharacter6, Me.tsCharacter7, Me.tsCharacter8, Me.tsCharacter9, Me.tsCharacter10, Me.tsCharacter11, Me.tsCharacter12, Me.tsCharacter13, Me.tsCharacter14, Me.tsCharacter15, Me.tsCharacter16, Me.tsCharacter17, Me.tsCharacter18, Me.tsCharacter19, Me.tsCharacter20})
        Me.mnuCharacter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.mnuCharacter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuCharacter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuCharacter.Name = "mnuCharacter"
        Me.mnuCharacter.Size = New System.Drawing.Size(250, 20)
        Me.mnuCharacter.Text = "Character Loaded:"
        Me.mnuCharacter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.mnuCharacter.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'tsCharacter1
        '
        Me.tsCharacter1.Name = "tsCharacter1"
        Me.tsCharacter1.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter1.Text = "tsCharacter1"
        '
        'tsCharacter2
        '
        Me.tsCharacter2.Name = "tsCharacter2"
        Me.tsCharacter2.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter2.Text = "tsCharacter2"
        '
        'tsCharacter3
        '
        Me.tsCharacter3.Name = "tsCharacter3"
        Me.tsCharacter3.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter3.Text = "tsCharacter3"
        '
        'tsCharacter4
        '
        Me.tsCharacter4.Name = "tsCharacter4"
        Me.tsCharacter4.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter4.Text = "tsCharacter4"
        '
        'tsCharacter5
        '
        Me.tsCharacter5.ImageTransparentColor = System.Drawing.Color.White
        Me.tsCharacter5.Name = "tsCharacter5"
        Me.tsCharacter5.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter5.Text = "tsCharacter5"
        '
        'tsCharacter6
        '
        Me.tsCharacter6.Name = "tsCharacter6"
        Me.tsCharacter6.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter6.Text = "tsCharacter6"
        '
        'tsCharacter7
        '
        Me.tsCharacter7.Name = "tsCharacter7"
        Me.tsCharacter7.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter7.Text = "tsCharacter7"
        '
        'tsCharacter8
        '
        Me.tsCharacter8.Name = "tsCharacter8"
        Me.tsCharacter8.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter8.Text = "tsCharacter8"
        '
        'tsCharacter9
        '
        Me.tsCharacter9.Name = "tsCharacter9"
        Me.tsCharacter9.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter9.Text = "tsCharacter9"
        '
        'tsCharacter10
        '
        Me.tsCharacter10.Name = "tsCharacter10"
        Me.tsCharacter10.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter10.Text = "tsCharacter10"
        '
        'tsCharacter11
        '
        Me.tsCharacter11.Name = "tsCharacter11"
        Me.tsCharacter11.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter11.Text = "tsCharacter11"
        '
        'tsCharacter12
        '
        Me.tsCharacter12.Name = "tsCharacter12"
        Me.tsCharacter12.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter12.Text = "tsCharacter12"
        '
        'tsCharacter13
        '
        Me.tsCharacter13.Name = "tsCharacter13"
        Me.tsCharacter13.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter13.Text = "tsCharacter13"
        '
        'tsCharacter14
        '
        Me.tsCharacter14.Name = "tsCharacter14"
        Me.tsCharacter14.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter14.Text = "tsCharacter14"
        '
        'tsCharacter15
        '
        Me.tsCharacter15.Name = "tsCharacter15"
        Me.tsCharacter15.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter15.Text = "tsCharacter15"
        '
        'tsCharacter16
        '
        Me.tsCharacter16.Name = "tsCharacter16"
        Me.tsCharacter16.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter16.Text = "tsCharacter16"
        '
        'tsCharacter17
        '
        Me.tsCharacter17.Name = "tsCharacter17"
        Me.tsCharacter17.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter17.Text = "tsCharacter17"
        '
        'tsCharacter18
        '
        Me.tsCharacter18.Name = "tsCharacter18"
        Me.tsCharacter18.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter18.Text = "tsCharacter18"
        '
        'tsCharacter19
        '
        Me.tsCharacter19.Name = "tsCharacter19"
        Me.tsCharacter19.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter19.Text = "tsCharacter19"
        '
        'tsCharacter20
        '
        Me.tsCharacter20.Name = "tsCharacter20"
        Me.tsCharacter20.Size = New System.Drawing.Size(146, 22)
        Me.tsCharacter20.Text = "tsCharacter20"
        '
        'pnlSkills
        '
        Me.pnlSkills.AutoSize = False
        Me.pnlSkills.Name = "pnlSkills"
        Me.pnlSkills.Size = New System.Drawing.Size(153, 17)
        Me.pnlSkills.Text = "Skills Overidden"
        '
        'pnlShoppingList
        '
        Me.pnlShoppingList.AutoSize = False
        Me.pnlShoppingList.Name = "pnlShoppingList"
        Me.pnlShoppingList.Size = New System.Drawing.Size(200, 17)
        Me.pnlShoppingList.ToolTipText = "Click to Open Shopping List"
        '
        'pnlStatus
        '
        Me.pnlStatus.AutoSize = False
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(215, 17)
        Me.pnlStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlProgressBar
        '
        Me.pnlProgressBar.AutoSize = False
        Me.pnlProgressBar.Name = "pnlProgressBar"
        Me.pnlProgressBar.Size = New System.Drawing.Size(300, 16)
        Me.pnlProgressBar.Step = 1
        Me.pnlProgressBar.Visible = False
        '
        'ttBP
        '
        Me.ttBP.IsBalloon = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'gbSystems
        '
        Me.gbSystems.Location = New System.Drawing.Point(0, 0)
        Me.gbSystems.Name = "gbSystems"
        Me.gbSystems.Size = New System.Drawing.Size(200, 100)
        Me.gbSystems.TabIndex = 0
        Me.gbSystems.TabStop = False
        '
        'ListOptionsMenu
        '
        Me.ListOptionsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewMarketHistoryToolStripMenuItem, Me.AddToShoppingListToolStripMenuItem, Me.IgnoreBlueprintToolStripMenuItem, Me.FavoriteBlueprintToolStripMenuItem})
        Me.ListOptionsMenu.Name = "ListOptionsMenu"
        Me.ListOptionsMenu.Size = New System.Drawing.Size(186, 92)
        '
        'ViewMarketHistoryToolStripMenuItem
        '
        Me.ViewMarketHistoryToolStripMenuItem.Name = "ViewMarketHistoryToolStripMenuItem"
        Me.ViewMarketHistoryToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ViewMarketHistoryToolStripMenuItem.Text = "View Market History"
        '
        'AddToShoppingListToolStripMenuItem
        '
        Me.AddToShoppingListToolStripMenuItem.Name = "AddToShoppingListToolStripMenuItem"
        Me.AddToShoppingListToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AddToShoppingListToolStripMenuItem.Text = "Add to Shopping List"
        '
        'IgnoreBlueprintToolStripMenuItem
        '
        Me.IgnoreBlueprintToolStripMenuItem.Name = "IgnoreBlueprintToolStripMenuItem"
        Me.IgnoreBlueprintToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.IgnoreBlueprintToolStripMenuItem.Text = "Ignore Blueprint"
        '
        'FavoriteBlueprintToolStripMenuItem
        '
        Me.FavoriteBlueprintToolStripMenuItem.Name = "FavoriteBlueprintToolStripMenuItem"
        Me.FavoriteBlueprintToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.FavoriteBlueprintToolStripMenuItem.Text = "Favorite Blueprint"
        '
        'rbtnPriceSourceFW
        '
        Me.rbtnPriceSourceFW.AutoSize = True
        Me.rbtnPriceSourceFW.Location = New System.Drawing.Point(130, 19)
        Me.rbtnPriceSourceFW.Name = "rbtnPriceSourceFW"
        Me.rbtnPriceSourceFW.Size = New System.Drawing.Size(42, 17)
        Me.rbtnPriceSourceFW.TabIndex = 49
        Me.rbtnPriceSourceFW.Text = "FW"
        Me.ttUpdatePrices.SetToolTip(Me.rbtnPriceSourceFW, "Fuzzworks")
        Me.rbtnPriceSourceFW.UseVisualStyleBackColor = True
        '
        'chkMineEDENCOM
        '
        Me.chkMineEDENCOM.AutoSize = True
        Me.chkMineEDENCOM.Location = New System.Drawing.Point(119, 75)
        Me.chkMineEDENCOM.Name = "chkMineEDENCOM"
        Me.chkMineEDENCOM.Size = New System.Drawing.Size(117, 17)
        Me.chkMineEDENCOM.TabIndex = 0
        Me.chkMineEDENCOM.Text = "EDENCOM System"
        '
        'CalcImageList
        '
        Me.CalcImageList.ImageStream = CType(resources.GetObject("CalcImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.CalcImageList.TransparentColor = System.Drawing.Color.White
        Me.CalcImageList.Images.SetKeyName(0, "GreenUP.bmp")
        Me.CalcImageList.Images.SetKeyName(1, "RedDown.bmp")
        Me.CalcImageList.Images.SetKeyName(2, "T2.bmp")
        Me.CalcImageList.Images.SetKeyName(3, "T3.bmp")
        Me.CalcImageList.Images.SetKeyName(4, "Storyline.bmp")
        Me.CalcImageList.Images.SetKeyName(5, "Faction.bmp")
        Me.CalcImageList.Images.SetKeyName(6, "Blank.bmp")
        Me.CalcImageList.Images.SetKeyName(7, "Green Up Arrow.bmp")
        Me.CalcImageList.Images.SetKeyName(8, "Red Down Arrow.bmp")
        '
        'tabPI
        '
        Me.tabPI.Controls.Add(Me.btnPISaveSettings)
        Me.tabPI.Controls.Add(Me.gbPIPlanets)
        Me.tabPI.Controls.Add(Me.btnPIReset)
        Me.tabPI.Location = New System.Drawing.Point(4, 22)
        Me.tabPI.Name = "tabPI"
        Me.tabPI.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPI.Size = New System.Drawing.Size(1137, 615)
        Me.tabPI.TabIndex = 6
        Me.tabPI.Text = "Planetary Interaction"
        Me.tabPI.UseVisualStyleBackColor = True
        '
        'btnPISaveSettings
        '
        Me.btnPISaveSettings.Location = New System.Drawing.Point(315, 47)
        Me.btnPISaveSettings.Name = "btnPISaveSettings"
        Me.btnPISaveSettings.Size = New System.Drawing.Size(92, 23)
        Me.btnPISaveSettings.TabIndex = 74
        Me.btnPISaveSettings.Text = "Save Settings"
        Me.btnPISaveSettings.UseVisualStyleBackColor = True
        '
        'gbPIPlanets
        '
        Me.gbPIPlanets.Controls.Add(Me.chkPILava)
        Me.gbPIPlanets.Controls.Add(Me.chkPIPlasma)
        Me.gbPIPlanets.Controls.Add(Me.chkPIIce)
        Me.gbPIPlanets.Controls.Add(Me.chkPIGas)
        Me.gbPIPlanets.Controls.Add(Me.chkPIOcean)
        Me.gbPIPlanets.Controls.Add(Me.chkPIBarren)
        Me.gbPIPlanets.Controls.Add(Me.chkPIStorm)
        Me.gbPIPlanets.Controls.Add(Me.chkPITemperate)
        Me.gbPIPlanets.Location = New System.Drawing.Point(9, 9)
        Me.gbPIPlanets.Name = "gbPIPlanets"
        Me.gbPIPlanets.Size = New System.Drawing.Size(299, 67)
        Me.gbPIPlanets.TabIndex = 41
        Me.gbPIPlanets.TabStop = False
        Me.gbPIPlanets.Text = "Planets"
        '
        'chkPILava
        '
        Me.chkPILava.AutoSize = True
        Me.chkPILava.Location = New System.Drawing.Point(216, 19)
        Me.chkPILava.Name = "chkPILava"
        Me.chkPILava.Size = New System.Drawing.Size(50, 17)
        Me.chkPILava.TabIndex = 25
        Me.chkPILava.Text = "Lava"
        Me.chkPILava.UseVisualStyleBackColor = True
        '
        'chkPIPlasma
        '
        Me.chkPIPlasma.AutoSize = True
        Me.chkPIPlasma.Location = New System.Drawing.Point(83, 42)
        Me.chkPIPlasma.Name = "chkPIPlasma"
        Me.chkPIPlasma.Size = New System.Drawing.Size(60, 17)
        Me.chkPIPlasma.TabIndex = 27
        Me.chkPIPlasma.Text = "Plasma"
        Me.chkPIPlasma.UseVisualStyleBackColor = True
        '
        'chkPIIce
        '
        Me.chkPIIce.AutoSize = True
        Me.chkPIIce.Location = New System.Drawing.Point(153, 19)
        Me.chkPIIce.Name = "chkPIIce"
        Me.chkPIIce.Size = New System.Drawing.Size(41, 17)
        Me.chkPIIce.TabIndex = 23
        Me.chkPIIce.Text = "Ice"
        Me.chkPIIce.UseVisualStyleBackColor = True
        '
        'chkPIGas
        '
        Me.chkPIGas.AutoSize = True
        Me.chkPIGas.Location = New System.Drawing.Point(83, 19)
        Me.chkPIGas.Name = "chkPIGas"
        Me.chkPIGas.Size = New System.Drawing.Size(45, 17)
        Me.chkPIGas.TabIndex = 24
        Me.chkPIGas.Text = "Gas"
        Me.chkPIGas.UseVisualStyleBackColor = True
        '
        'chkPIOcean
        '
        Me.chkPIOcean.AutoSize = True
        Me.chkPIOcean.Location = New System.Drawing.Point(15, 42)
        Me.chkPIOcean.Name = "chkPIOcean"
        Me.chkPIOcean.Size = New System.Drawing.Size(58, 17)
        Me.chkPIOcean.TabIndex = 26
        Me.chkPIOcean.Text = "Ocean"
        Me.chkPIOcean.UseVisualStyleBackColor = True
        '
        'chkPIBarren
        '
        Me.chkPIBarren.AutoSize = True
        Me.chkPIBarren.Location = New System.Drawing.Point(15, 19)
        Me.chkPIBarren.Name = "chkPIBarren"
        Me.chkPIBarren.Size = New System.Drawing.Size(57, 17)
        Me.chkPIBarren.TabIndex = 22
        Me.chkPIBarren.Text = "Barren"
        Me.chkPIBarren.UseVisualStyleBackColor = True
        '
        'chkPIStorm
        '
        Me.chkPIStorm.AutoSize = True
        Me.chkPIStorm.Location = New System.Drawing.Point(153, 42)
        Me.chkPIStorm.Name = "chkPIStorm"
        Me.chkPIStorm.Size = New System.Drawing.Size(53, 17)
        Me.chkPIStorm.TabIndex = 28
        Me.chkPIStorm.Text = "Storm"
        Me.chkPIStorm.UseVisualStyleBackColor = True
        '
        'chkPITemperate
        '
        Me.chkPITemperate.AutoSize = True
        Me.chkPITemperate.Location = New System.Drawing.Point(216, 42)
        Me.chkPITemperate.Name = "chkPITemperate"
        Me.chkPITemperate.Size = New System.Drawing.Size(77, 17)
        Me.chkPITemperate.TabIndex = 29
        Me.chkPITemperate.Text = "Temperate"
        Me.chkPITemperate.UseVisualStyleBackColor = True
        '
        'btnPIReset
        '
        Me.btnPIReset.Location = New System.Drawing.Point(315, 14)
        Me.btnPIReset.Name = "btnPIReset"
        Me.btnPIReset.Size = New System.Drawing.Size(92, 25)
        Me.btnPIReset.TabIndex = 73
        Me.btnPIReset.Text = "Reset"
        Me.btnPIReset.UseVisualStyleBackColor = True
        '
        'tabMining
        '
        Me.tabMining.Controls.Add(Me.gbMineCrystalType)
        Me.tabMining.Controls.Add(Me.tabMiningDrones)
        Me.tabMining.Controls.Add(Me.gbMineCrystals)
        Me.tabMining.Controls.Add(Me.gbMineNumberMiners)
        Me.tabMining.Controls.Add(Me.gbMineOreProcessingType)
        Me.tabMining.Controls.Add(Me.btnMineSaveAllSettings)
        Me.tabMining.Controls.Add(Me.gbMineTaxBroker)
        Me.tabMining.Controls.Add(Me.gbMineStripStats)
        Me.tabMining.Controls.Add(Me.chkMineUseFleetBooster)
        Me.tabMining.Controls.Add(Me.btnMineReset)
        Me.tabMining.Controls.Add(Me.gbMineHauling)
        Me.tabMining.Controls.Add(Me.btnMineRefresh)
        Me.tabMining.Controls.Add(Me.gbMineBooster)
        Me.tabMining.Controls.Add(Me.gbMineRefining)
        Me.tabMining.Controls.Add(Me.gbMineShipSetup)
        Me.tabMining.Controls.Add(Me.gbMineMain)
        Me.tabMining.Controls.Add(Me.lstMineGrid)
        Me.tabMining.Location = New System.Drawing.Point(4, 22)
        Me.tabMining.Name = "tabMining"
        Me.tabMining.Size = New System.Drawing.Size(1137, 615)
        Me.tabMining.TabIndex = 5
        Me.tabMining.Text = "Mining"
        Me.tabMining.UseVisualStyleBackColor = True
        '
        'gbMineCrystalType
        '
        Me.gbMineCrystalType.Controls.Add(Me.chkMineTypeC)
        Me.gbMineCrystalType.Controls.Add(Me.chkMineTypeB)
        Me.gbMineCrystalType.Controls.Add(Me.chkMineTypeA)
        Me.gbMineCrystalType.Location = New System.Drawing.Point(1040, 514)
        Me.gbMineCrystalType.Name = "gbMineCrystalType"
        Me.gbMineCrystalType.Size = New System.Drawing.Size(83, 85)
        Me.gbMineCrystalType.TabIndex = 8
        Me.gbMineCrystalType.TabStop = False
        Me.gbMineCrystalType.Text = "Crystal Type:"
        '
        'chkMineTypeC
        '
        Me.chkMineTypeC.AutoSize = True
        Me.chkMineTypeC.Checked = True
        Me.chkMineTypeC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineTypeC.Location = New System.Drawing.Point(7, 59)
        Me.chkMineTypeC.Name = "chkMineTypeC"
        Me.chkMineTypeC.Size = New System.Drawing.Size(60, 17)
        Me.chkMineTypeC.TabIndex = 2
        Me.chkMineTypeC.Text = "Type C"
        Me.chkMineTypeC.UseVisualStyleBackColor = True
        '
        'chkMineTypeB
        '
        Me.chkMineTypeB.AutoSize = True
        Me.chkMineTypeB.Checked = True
        Me.chkMineTypeB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineTypeB.Location = New System.Drawing.Point(7, 39)
        Me.chkMineTypeB.Name = "chkMineTypeB"
        Me.chkMineTypeB.Size = New System.Drawing.Size(60, 17)
        Me.chkMineTypeB.TabIndex = 0
        Me.chkMineTypeB.Text = "Type B"
        Me.chkMineTypeB.UseVisualStyleBackColor = True
        '
        'chkMineTypeA
        '
        Me.chkMineTypeA.AutoSize = True
        Me.chkMineTypeA.Checked = True
        Me.chkMineTypeA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineTypeA.Location = New System.Drawing.Point(7, 19)
        Me.chkMineTypeA.Name = "chkMineTypeA"
        Me.chkMineTypeA.Size = New System.Drawing.Size(60, 17)
        Me.chkMineTypeA.TabIndex = 1
        Me.chkMineTypeA.Text = "Type A"
        Me.chkMineTypeA.UseVisualStyleBackColor = True
        '
        'tabMiningDrones
        '
        Me.tabMiningDrones.Controls.Add(Me.tabShipDrones)
        Me.tabMiningDrones.Controls.Add(Me.tabBoosterDrones)
        Me.tabMiningDrones.Location = New System.Drawing.Point(374, 14)
        Me.tabMiningDrones.Name = "tabMiningDrones"
        Me.tabMiningDrones.SelectedIndex = 0
        Me.tabMiningDrones.Size = New System.Drawing.Size(261, 138)
        Me.tabMiningDrones.TabIndex = 120
        '
        'tabShipDrones
        '
        Me.tabShipDrones.Controls.Add(Me.lblMineDroneIdealRange)
        Me.tabShipDrones.Controls.Add(Me.cmbMineDroneName)
        Me.tabShipDrones.Controls.Add(Me.lblMineMiningDroneYield)
        Me.tabShipDrones.Controls.Add(Me.cmbMineDroneOpSkill)
        Me.tabShipDrones.Controls.Add(Me.lblMineMiningDroneM3)
        Me.tabShipDrones.Controls.Add(Me.lblMineDroneOpSkill)
        Me.tabShipDrones.Controls.Add(Me.lblMineNumMiningDrones)
        Me.tabShipDrones.Controls.Add(Me.cmbMineDroneSpecSkill)
        Me.tabShipDrones.Controls.Add(Me.cmbMineNumMiningDrones)
        Me.tabShipDrones.Controls.Add(Me.lblMineDroneSpecSkill)
        Me.tabShipDrones.Controls.Add(Me.lblMineDroneInterfacingSkill)
        Me.tabShipDrones.Controls.Add(Me.lblMineDroneName)
        Me.tabShipDrones.Controls.Add(Me.cmbMineDroneInterfacingSkill)
        Me.tabShipDrones.Location = New System.Drawing.Point(4, 22)
        Me.tabShipDrones.Name = "tabShipDrones"
        Me.tabShipDrones.Padding = New System.Windows.Forms.Padding(3)
        Me.tabShipDrones.Size = New System.Drawing.Size(253, 112)
        Me.tabShipDrones.TabIndex = 0
        Me.tabShipDrones.Text = "Ship Mining Drones"
        Me.tabShipDrones.UseVisualStyleBackColor = True
        '
        'lblMineDroneIdealRange
        '
        Me.lblMineDroneIdealRange.Location = New System.Drawing.Point(112, 87)
        Me.lblMineDroneIdealRange.Name = "lblMineDroneIdealRange"
        Me.lblMineDroneIdealRange.Size = New System.Drawing.Size(134, 16)
        Me.lblMineDroneIdealRange.TabIndex = 137
        Me.lblMineDroneIdealRange.Text = "Ideal Range:"
        Me.lblMineDroneIdealRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMineDroneName
        '
        Me.cmbMineDroneName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineDroneName.FormattingEnabled = True
        Me.cmbMineDroneName.Location = New System.Drawing.Point(74, 6)
        Me.cmbMineDroneName.Name = "cmbMineDroneName"
        Me.cmbMineDroneName.Size = New System.Drawing.Size(172, 21)
        Me.cmbMineDroneName.TabIndex = 132
        '
        'lblMineMiningDroneYield
        '
        Me.lblMineMiningDroneYield.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMineMiningDroneYield.Location = New System.Drawing.Point(167, 56)
        Me.lblMineMiningDroneYield.Name = "lblMineMiningDroneYield"
        Me.lblMineMiningDroneYield.Size = New System.Drawing.Size(79, 23)
        Me.lblMineMiningDroneYield.TabIndex = 136
        Me.lblMineMiningDroneYield.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMineDroneOpSkill
        '
        Me.cmbMineDroneOpSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineDroneOpSkill.FormattingEnabled = True
        Me.cmbMineDroneOpSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineDroneOpSkill.Location = New System.Drawing.Point(74, 32)
        Me.cmbMineDroneOpSkill.Name = "cmbMineDroneOpSkill"
        Me.cmbMineDroneOpSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineDroneOpSkill.TabIndex = 110
        '
        'lblMineMiningDroneM3
        '
        Me.lblMineMiningDroneM3.Location = New System.Drawing.Point(112, 53)
        Me.lblMineMiningDroneM3.Name = "lblMineMiningDroneM3"
        Me.lblMineMiningDroneM3.Size = New System.Drawing.Size(53, 30)
        Me.lblMineMiningDroneM3.TabIndex = 9
        Me.lblMineMiningDroneM3.Text = "Yield (m3/Hr):"
        Me.lblMineMiningDroneM3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineDroneOpSkill
        '
        Me.lblMineDroneOpSkill.AutoSize = True
        Me.lblMineDroneOpSkill.Location = New System.Drawing.Point(27, 36)
        Me.lblMineDroneOpSkill.Name = "lblMineDroneOpSkill"
        Me.lblMineDroneOpSkill.Size = New System.Drawing.Size(46, 13)
        Me.lblMineDroneOpSkill.TabIndex = 111
        Me.lblMineDroneOpSkill.Text = "Op Skill:"
        Me.lblMineDroneOpSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineNumMiningDrones
        '
        Me.lblMineNumMiningDrones.AutoSize = True
        Me.lblMineNumMiningDrones.Location = New System.Drawing.Point(113, 36)
        Me.lblMineNumMiningDrones.Name = "lblMineNumMiningDrones"
        Me.lblMineNumMiningDrones.Size = New System.Drawing.Size(96, 13)
        Me.lblMineNumMiningDrones.TabIndex = 135
        Me.lblMineNumMiningDrones.Text = "Number of Drones:"
        '
        'cmbMineDroneSpecSkill
        '
        Me.cmbMineDroneSpecSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineDroneSpecSkill.FormattingEnabled = True
        Me.cmbMineDroneSpecSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineDroneSpecSkill.Location = New System.Drawing.Point(74, 58)
        Me.cmbMineDroneSpecSkill.Name = "cmbMineDroneSpecSkill"
        Me.cmbMineDroneSpecSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineDroneSpecSkill.TabIndex = 112
        '
        'cmbMineNumMiningDrones
        '
        Me.cmbMineNumMiningDrones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineNumMiningDrones.FormattingEnabled = True
        Me.cmbMineNumMiningDrones.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineNumMiningDrones.Location = New System.Drawing.Point(210, 32)
        Me.cmbMineNumMiningDrones.Name = "cmbMineNumMiningDrones"
        Me.cmbMineNumMiningDrones.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineNumMiningDrones.TabIndex = 134
        '
        'lblMineDroneSpecSkill
        '
        Me.lblMineDroneSpecSkill.AutoSize = True
        Me.lblMineDroneSpecSkill.Location = New System.Drawing.Point(16, 60)
        Me.lblMineDroneSpecSkill.Name = "lblMineDroneSpecSkill"
        Me.lblMineDroneSpecSkill.Size = New System.Drawing.Size(57, 13)
        Me.lblMineDroneSpecSkill.TabIndex = 113
        Me.lblMineDroneSpecSkill.Text = "Spec Skill:"
        Me.lblMineDroneSpecSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineDroneInterfacingSkill
        '
        Me.lblMineDroneInterfacingSkill.Location = New System.Drawing.Point(12, 78)
        Me.lblMineDroneInterfacingSkill.Name = "lblMineDroneInterfacingSkill"
        Me.lblMineDroneInterfacingSkill.Size = New System.Drawing.Size(61, 29)
        Me.lblMineDroneInterfacingSkill.TabIndex = 115
        Me.lblMineDroneInterfacingSkill.Text = "Drone Interfacing:"
        Me.lblMineDroneInterfacingSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineDroneName
        '
        Me.lblMineDroneName.AutoSize = True
        Me.lblMineDroneName.Location = New System.Drawing.Point(3, 9)
        Me.lblMineDroneName.Name = "lblMineDroneName"
        Me.lblMineDroneName.Size = New System.Drawing.Size(70, 13)
        Me.lblMineDroneName.TabIndex = 133
        Me.lblMineDroneName.Text = "Drone Name:"
        Me.lblMineDroneName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMineDroneInterfacingSkill
        '
        Me.cmbMineDroneInterfacingSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineDroneInterfacingSkill.FormattingEnabled = True
        Me.cmbMineDroneInterfacingSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineDroneInterfacingSkill.Location = New System.Drawing.Point(74, 84)
        Me.cmbMineDroneInterfacingSkill.Name = "cmbMineDroneInterfacingSkill"
        Me.cmbMineDroneInterfacingSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineDroneInterfacingSkill.TabIndex = 114
        '
        'tabBoosterDrones
        '
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterDroneIdealRange)
        Me.tabBoosterDrones.Controls.Add(Me.cmbMineBoosterDroneName)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterMiningDroneYield)
        Me.tabBoosterDrones.Controls.Add(Me.cmbMineBoosterDroneOpSkill)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterMiningDroneM3)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterDroneOpSkill)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterNumMiningDrones)
        Me.tabBoosterDrones.Controls.Add(Me.cmbMineBoosterDroneSpecSkill)
        Me.tabBoosterDrones.Controls.Add(Me.cmbMineBoosterNumMiningDrones)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterDroneSpecSkill)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterDroneInterfacingSkill)
        Me.tabBoosterDrones.Controls.Add(Me.lblMineBoosterDroneName)
        Me.tabBoosterDrones.Controls.Add(Me.cmbMineBoosterDroneInterfacingSkill)
        Me.tabBoosterDrones.Location = New System.Drawing.Point(4, 22)
        Me.tabBoosterDrones.Name = "tabBoosterDrones"
        Me.tabBoosterDrones.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBoosterDrones.Size = New System.Drawing.Size(253, 112)
        Me.tabBoosterDrones.TabIndex = 1
        Me.tabBoosterDrones.Text = "Booster Mining Drones"
        Me.tabBoosterDrones.UseVisualStyleBackColor = True
        '
        'lblMineBoosterDroneIdealRange
        '
        Me.lblMineBoosterDroneIdealRange.Location = New System.Drawing.Point(112, 87)
        Me.lblMineBoosterDroneIdealRange.Name = "lblMineBoosterDroneIdealRange"
        Me.lblMineBoosterDroneIdealRange.Size = New System.Drawing.Size(134, 16)
        Me.lblMineBoosterDroneIdealRange.TabIndex = 150
        Me.lblMineBoosterDroneIdealRange.Text = "Ideal Range:"
        Me.lblMineBoosterDroneIdealRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMineBoosterDroneName
        '
        Me.cmbMineBoosterDroneName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterDroneName.FormattingEnabled = True
        Me.cmbMineBoosterDroneName.Location = New System.Drawing.Point(74, 6)
        Me.cmbMineBoosterDroneName.Name = "cmbMineBoosterDroneName"
        Me.cmbMineBoosterDroneName.Size = New System.Drawing.Size(172, 21)
        Me.cmbMineBoosterDroneName.TabIndex = 145
        '
        'lblMineBoosterMiningDroneYield
        '
        Me.lblMineBoosterMiningDroneYield.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMineBoosterMiningDroneYield.Location = New System.Drawing.Point(167, 56)
        Me.lblMineBoosterMiningDroneYield.Name = "lblMineBoosterMiningDroneYield"
        Me.lblMineBoosterMiningDroneYield.Size = New System.Drawing.Size(79, 23)
        Me.lblMineBoosterMiningDroneYield.TabIndex = 149
        Me.lblMineBoosterMiningDroneYield.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMineBoosterDroneOpSkill
        '
        Me.cmbMineBoosterDroneOpSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterDroneOpSkill.FormattingEnabled = True
        Me.cmbMineBoosterDroneOpSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineBoosterDroneOpSkill.Location = New System.Drawing.Point(74, 32)
        Me.cmbMineBoosterDroneOpSkill.Name = "cmbMineBoosterDroneOpSkill"
        Me.cmbMineBoosterDroneOpSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineBoosterDroneOpSkill.TabIndex = 139
        '
        'lblMineBoosterMiningDroneM3
        '
        Me.lblMineBoosterMiningDroneM3.Location = New System.Drawing.Point(112, 53)
        Me.lblMineBoosterMiningDroneM3.Name = "lblMineBoosterMiningDroneM3"
        Me.lblMineBoosterMiningDroneM3.Size = New System.Drawing.Size(53, 30)
        Me.lblMineBoosterMiningDroneM3.TabIndex = 138
        Me.lblMineBoosterMiningDroneM3.Text = "Yield (m3/Hr):"
        Me.lblMineBoosterMiningDroneM3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineBoosterDroneOpSkill
        '
        Me.lblMineBoosterDroneOpSkill.AutoSize = True
        Me.lblMineBoosterDroneOpSkill.Location = New System.Drawing.Point(27, 36)
        Me.lblMineBoosterDroneOpSkill.Name = "lblMineBoosterDroneOpSkill"
        Me.lblMineBoosterDroneOpSkill.Size = New System.Drawing.Size(46, 13)
        Me.lblMineBoosterDroneOpSkill.TabIndex = 140
        Me.lblMineBoosterDroneOpSkill.Text = "Op Skill:"
        Me.lblMineBoosterDroneOpSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineBoosterNumMiningDrones
        '
        Me.lblMineBoosterNumMiningDrones.AutoSize = True
        Me.lblMineBoosterNumMiningDrones.Location = New System.Drawing.Point(113, 36)
        Me.lblMineBoosterNumMiningDrones.Name = "lblMineBoosterNumMiningDrones"
        Me.lblMineBoosterNumMiningDrones.Size = New System.Drawing.Size(96, 13)
        Me.lblMineBoosterNumMiningDrones.TabIndex = 148
        Me.lblMineBoosterNumMiningDrones.Text = "Number of Drones:"
        '
        'cmbMineBoosterDroneSpecSkill
        '
        Me.cmbMineBoosterDroneSpecSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterDroneSpecSkill.FormattingEnabled = True
        Me.cmbMineBoosterDroneSpecSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineBoosterDroneSpecSkill.Location = New System.Drawing.Point(74, 58)
        Me.cmbMineBoosterDroneSpecSkill.Name = "cmbMineBoosterDroneSpecSkill"
        Me.cmbMineBoosterDroneSpecSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineBoosterDroneSpecSkill.TabIndex = 141
        '
        'cmbMineBoosterNumMiningDrones
        '
        Me.cmbMineBoosterNumMiningDrones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterNumMiningDrones.FormattingEnabled = True
        Me.cmbMineBoosterNumMiningDrones.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineBoosterNumMiningDrones.Location = New System.Drawing.Point(210, 32)
        Me.cmbMineBoosterNumMiningDrones.Name = "cmbMineBoosterNumMiningDrones"
        Me.cmbMineBoosterNumMiningDrones.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineBoosterNumMiningDrones.TabIndex = 147
        '
        'lblMineBoosterDroneSpecSkill
        '
        Me.lblMineBoosterDroneSpecSkill.AutoSize = True
        Me.lblMineBoosterDroneSpecSkill.Location = New System.Drawing.Point(16, 60)
        Me.lblMineBoosterDroneSpecSkill.Name = "lblMineBoosterDroneSpecSkill"
        Me.lblMineBoosterDroneSpecSkill.Size = New System.Drawing.Size(57, 13)
        Me.lblMineBoosterDroneSpecSkill.TabIndex = 142
        Me.lblMineBoosterDroneSpecSkill.Text = "Spec Skill:"
        Me.lblMineBoosterDroneSpecSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineBoosterDroneInterfacingSkill
        '
        Me.lblMineBoosterDroneInterfacingSkill.Location = New System.Drawing.Point(12, 78)
        Me.lblMineBoosterDroneInterfacingSkill.Name = "lblMineBoosterDroneInterfacingSkill"
        Me.lblMineBoosterDroneInterfacingSkill.Size = New System.Drawing.Size(61, 29)
        Me.lblMineBoosterDroneInterfacingSkill.TabIndex = 144
        Me.lblMineBoosterDroneInterfacingSkill.Text = "Drone Interfacing:"
        Me.lblMineBoosterDroneInterfacingSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineBoosterDroneName
        '
        Me.lblMineBoosterDroneName.AutoSize = True
        Me.lblMineBoosterDroneName.Location = New System.Drawing.Point(3, 9)
        Me.lblMineBoosterDroneName.Name = "lblMineBoosterDroneName"
        Me.lblMineBoosterDroneName.Size = New System.Drawing.Size(70, 13)
        Me.lblMineBoosterDroneName.TabIndex = 146
        Me.lblMineBoosterDroneName.Text = "Drone Name:"
        Me.lblMineBoosterDroneName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMineBoosterDroneInterfacingSkill
        '
        Me.cmbMineBoosterDroneInterfacingSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterDroneInterfacingSkill.FormattingEnabled = True
        Me.cmbMineBoosterDroneInterfacingSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineBoosterDroneInterfacingSkill.Location = New System.Drawing.Point(74, 84)
        Me.cmbMineBoosterDroneInterfacingSkill.Name = "cmbMineBoosterDroneInterfacingSkill"
        Me.cmbMineBoosterDroneInterfacingSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineBoosterDroneInterfacingSkill.TabIndex = 143
        '
        'gbMineCrystals
        '
        Me.gbMineCrystals.Controls.Add(Me.chkMineT2Crystals)
        Me.gbMineCrystals.Controls.Add(Me.chkMineT1Crystals)
        Me.gbMineCrystals.Location = New System.Drawing.Point(1040, 448)
        Me.gbMineCrystals.Name = "gbMineCrystals"
        Me.gbMineCrystals.Size = New System.Drawing.Size(83, 60)
        Me.gbMineCrystals.TabIndex = 4
        Me.gbMineCrystals.TabStop = False
        Me.gbMineCrystals.Text = "Crystal Tech:"
        '
        'chkMineT2Crystals
        '
        Me.chkMineT2Crystals.AutoSize = True
        Me.chkMineT2Crystals.Checked = True
        Me.chkMineT2Crystals.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineT2Crystals.Location = New System.Drawing.Point(7, 35)
        Me.chkMineT2Crystals.Name = "chkMineT2Crystals"
        Me.chkMineT2Crystals.Size = New System.Drawing.Size(78, 17)
        Me.chkMineT2Crystals.TabIndex = 9
        Me.chkMineT2Crystals.Text = "T2 Crystals"
        Me.chkMineT2Crystals.UseVisualStyleBackColor = True
        '
        'chkMineT1Crystals
        '
        Me.chkMineT1Crystals.AutoSize = True
        Me.chkMineT1Crystals.Checked = True
        Me.chkMineT1Crystals.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineT1Crystals.Location = New System.Drawing.Point(7, 18)
        Me.chkMineT1Crystals.Name = "chkMineT1Crystals"
        Me.chkMineT1Crystals.Size = New System.Drawing.Size(78, 17)
        Me.chkMineT1Crystals.TabIndex = 10
        Me.chkMineT1Crystals.Text = "T1 Crystals"
        Me.chkMineT1Crystals.UseVisualStyleBackColor = True
        '
        'gbMineNumberMiners
        '
        Me.gbMineNumberMiners.Controls.Add(Me.txtMineNumberMiners)
        Me.gbMineNumberMiners.Controls.Add(Me.lblMineNumberMiners)
        Me.gbMineNumberMiners.Location = New System.Drawing.Point(580, 156)
        Me.gbMineNumberMiners.Name = "gbMineNumberMiners"
        Me.gbMineNumberMiners.Size = New System.Drawing.Size(125, 44)
        Me.gbMineNumberMiners.TabIndex = 110
        Me.gbMineNumberMiners.TabStop = False
        Me.gbMineNumberMiners.Text = "Multi-box mining:"
        '
        'txtMineNumberMiners
        '
        Me.txtMineNumberMiners.Location = New System.Drawing.Point(92, 17)
        Me.txtMineNumberMiners.MaxLength = 5
        Me.txtMineNumberMiners.Name = "txtMineNumberMiners"
        Me.txtMineNumberMiners.Size = New System.Drawing.Size(27, 20)
        Me.txtMineNumberMiners.TabIndex = 4
        Me.txtMineNumberMiners.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblMineNumberMiners
        '
        Me.lblMineNumberMiners.AutoSize = True
        Me.lblMineNumberMiners.Location = New System.Drawing.Point(0, 20)
        Me.lblMineNumberMiners.Name = "lblMineNumberMiners"
        Me.lblMineNumberMiners.Size = New System.Drawing.Size(93, 13)
        Me.lblMineNumberMiners.TabIndex = 133
        Me.lblMineNumberMiners.Text = "Number of Miners:"
        '
        'gbMineOreProcessingType
        '
        Me.gbMineOreProcessingType.Controls.Add(Me.chkMineUnrefinedOre)
        Me.gbMineOreProcessingType.Controls.Add(Me.chkMineRefinedOre)
        Me.gbMineOreProcessingType.Controls.Add(Me.chkMineCompressedOre)
        Me.gbMineOreProcessingType.Location = New System.Drawing.Point(580, 200)
        Me.gbMineOreProcessingType.Name = "gbMineOreProcessingType"
        Me.gbMineOreProcessingType.Size = New System.Drawing.Size(125, 73)
        Me.gbMineOreProcessingType.TabIndex = 118
        Me.gbMineOreProcessingType.TabStop = False
        Me.gbMineOreProcessingType.Text = "Processing Types"
        '
        'chkMineUnrefinedOre
        '
        Me.chkMineUnrefinedOre.AutoSize = True
        Me.chkMineUnrefinedOre.BackColor = System.Drawing.Color.Transparent
        Me.chkMineUnrefinedOre.Location = New System.Drawing.Point(13, 35)
        Me.chkMineUnrefinedOre.Name = "chkMineUnrefinedOre"
        Me.chkMineUnrefinedOre.Size = New System.Drawing.Size(92, 17)
        Me.chkMineUnrefinedOre.TabIndex = 118
        Me.chkMineUnrefinedOre.Text = "Unrefined Ore"
        Me.chkMineUnrefinedOre.UseVisualStyleBackColor = False
        '
        'chkMineRefinedOre
        '
        Me.chkMineRefinedOre.AutoSize = True
        Me.chkMineRefinedOre.BackColor = System.Drawing.Color.Transparent
        Me.chkMineRefinedOre.Location = New System.Drawing.Point(13, 18)
        Me.chkMineRefinedOre.Name = "chkMineRefinedOre"
        Me.chkMineRefinedOre.Size = New System.Drawing.Size(83, 17)
        Me.chkMineRefinedOre.TabIndex = 19
        Me.chkMineRefinedOre.Text = "Refined Ore"
        Me.chkMineRefinedOre.UseVisualStyleBackColor = False
        '
        'chkMineCompressedOre
        '
        Me.chkMineCompressedOre.AutoSize = True
        Me.chkMineCompressedOre.BackColor = System.Drawing.Color.Transparent
        Me.chkMineCompressedOre.Location = New System.Drawing.Point(13, 52)
        Me.chkMineCompressedOre.Name = "chkMineCompressedOre"
        Me.chkMineCompressedOre.Size = New System.Drawing.Size(104, 17)
        Me.chkMineCompressedOre.TabIndex = 117
        Me.chkMineCompressedOre.Text = "Compressed Ore"
        Me.chkMineCompressedOre.UseVisualStyleBackColor = False
        '
        'btnMineSaveAllSettings
        '
        Me.btnMineSaveAllSettings.Location = New System.Drawing.Point(637, 114)
        Me.btnMineSaveAllSettings.Name = "btnMineSaveAllSettings"
        Me.btnMineSaveAllSettings.Size = New System.Drawing.Size(68, 34)
        Me.btnMineSaveAllSettings.TabIndex = 3
        Me.btnMineSaveAllSettings.Text = "Save Settings"
        Me.btnMineSaveAllSettings.UseVisualStyleBackColor = True
        '
        'gbMineTaxBroker
        '
        Me.gbMineTaxBroker.Controls.Add(Me.txtMineBrokerFeeRate)
        Me.gbMineTaxBroker.Controls.Add(Me.chkMineIncludeBrokerFees)
        Me.gbMineTaxBroker.Controls.Add(Me.chkMineIncludeTaxes)
        Me.gbMineTaxBroker.Location = New System.Drawing.Point(1057, 241)
        Me.gbMineTaxBroker.Name = "gbMineTaxBroker"
        Me.gbMineTaxBroker.Size = New System.Drawing.Size(72, 69)
        Me.gbMineTaxBroker.TabIndex = 7
        Me.gbMineTaxBroker.TabStop = False
        Me.gbMineTaxBroker.Text = "Include:"
        '
        'txtMineBrokerFeeRate
        '
        Me.txtMineBrokerFeeRate.Location = New System.Drawing.Point(6, 45)
        Me.txtMineBrokerFeeRate.Name = "txtMineBrokerFeeRate"
        Me.txtMineBrokerFeeRate.Size = New System.Drawing.Size(60, 20)
        Me.txtMineBrokerFeeRate.TabIndex = 8
        Me.txtMineBrokerFeeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtMineBrokerFeeRate.Visible = False
        '
        'chkMineIncludeBrokerFees
        '
        Me.chkMineIncludeBrokerFees.AutoSize = True
        Me.chkMineIncludeBrokerFees.Checked = True
        Me.chkMineIncludeBrokerFees.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineIncludeBrokerFees.Location = New System.Drawing.Point(6, 29)
        Me.chkMineIncludeBrokerFees.Name = "chkMineIncludeBrokerFees"
        Me.chkMineIncludeBrokerFees.Size = New System.Drawing.Size(49, 17)
        Me.chkMineIncludeBrokerFees.TabIndex = 0
        Me.chkMineIncludeBrokerFees.Text = "Fees"
        Me.chkMineIncludeBrokerFees.ThreeState = True
        Me.chkMineIncludeBrokerFees.UseVisualStyleBackColor = True
        '
        'chkMineIncludeTaxes
        '
        Me.chkMineIncludeTaxes.AutoSize = True
        Me.chkMineIncludeTaxes.Checked = True
        Me.chkMineIncludeTaxes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMineIncludeTaxes.Location = New System.Drawing.Point(6, 14)
        Me.chkMineIncludeTaxes.Name = "chkMineIncludeTaxes"
        Me.chkMineIncludeTaxes.Size = New System.Drawing.Size(55, 17)
        Me.chkMineIncludeTaxes.TabIndex = 1
        Me.chkMineIncludeTaxes.Text = "Taxes"
        Me.chkMineIncludeTaxes.UseVisualStyleBackColor = True
        '
        'gbMineStripStats
        '
        Me.gbMineStripStats.Controls.Add(Me.lblMineRange)
        Me.gbMineStripStats.Controls.Add(Me.lblMineCycleTime1)
        Me.gbMineStripStats.Controls.Add(Me.lblMineRange1)
        Me.gbMineStripStats.Controls.Add(Me.lblMineCycleTime)
        Me.gbMineStripStats.Location = New System.Drawing.Point(711, 241)
        Me.gbMineStripStats.Name = "gbMineStripStats"
        Me.gbMineStripStats.Size = New System.Drawing.Size(140, 69)
        Me.gbMineStripStats.TabIndex = 5
        Me.gbMineStripStats.TabStop = False
        Me.gbMineStripStats.Text = "Miner Stats:"
        '
        'lblMineRange
        '
        Me.lblMineRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMineRange.Location = New System.Drawing.Point(79, 42)
        Me.lblMineRange.Name = "lblMineRange"
        Me.lblMineRange.Size = New System.Drawing.Size(53, 18)
        Me.lblMineRange.TabIndex = 135
        Me.lblMineRange.Text = "99.99 km"
        Me.lblMineRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMineCycleTime1
        '
        Me.lblMineCycleTime1.AutoSize = True
        Me.lblMineCycleTime1.Location = New System.Drawing.Point(9, 21)
        Me.lblMineCycleTime1.Name = "lblMineCycleTime1"
        Me.lblMineCycleTime1.Size = New System.Drawing.Size(62, 13)
        Me.lblMineCycleTime1.TabIndex = 132
        Me.lblMineCycleTime1.Text = "Cycle Time:"
        '
        'lblMineRange1
        '
        Me.lblMineRange1.AutoSize = True
        Me.lblMineRange1.Location = New System.Drawing.Point(9, 45)
        Me.lblMineRange1.Name = "lblMineRange1"
        Me.lblMineRange1.Size = New System.Drawing.Size(71, 13)
        Me.lblMineRange1.TabIndex = 134
        Me.lblMineRange1.Text = "Laser Range:"
        '
        'lblMineCycleTime
        '
        Me.lblMineCycleTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMineCycleTime.Location = New System.Drawing.Point(79, 18)
        Me.lblMineCycleTime.Name = "lblMineCycleTime"
        Me.lblMineCycleTime.Size = New System.Drawing.Size(53, 18)
        Me.lblMineCycleTime.TabIndex = 133
        Me.lblMineCycleTime.Text = "999.99 s"
        Me.lblMineCycleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkMineUseFleetBooster
        '
        Me.chkMineUseFleetBooster.AutoSize = True
        Me.chkMineUseFleetBooster.BackColor = System.Drawing.Color.Transparent
        Me.chkMineUseFleetBooster.Location = New System.Drawing.Point(14, 155)
        Me.chkMineUseFleetBooster.Name = "chkMineUseFleetBooster"
        Me.chkMineUseFleetBooster.Size = New System.Drawing.Size(113, 17)
        Me.chkMineUseFleetBooster.TabIndex = 0
        Me.chkMineUseFleetBooster.Text = "Use Fleet Booster:"
        Me.chkMineUseFleetBooster.UseVisualStyleBackColor = False
        '
        'btnMineReset
        '
        Me.btnMineReset.Location = New System.Drawing.Point(637, 76)
        Me.btnMineReset.Name = "btnMineReset"
        Me.btnMineReset.Size = New System.Drawing.Size(68, 34)
        Me.btnMineReset.TabIndex = 2
        Me.btnMineReset.Text = "Reset"
        Me.btnMineReset.UseVisualStyleBackColor = True
        '
        'gbMineHauling
        '
        Me.gbMineHauling.Controls.Add(Me.txtMineHaulerM3)
        Me.gbMineHauling.Controls.Add(Me.lblMineHaulerM3)
        Me.gbMineHauling.Controls.Add(Me.lblMineRTSec)
        Me.gbMineHauling.Controls.Add(Me.chkMineUseHauler)
        Me.gbMineHauling.Controls.Add(Me.lblMineRTMin)
        Me.gbMineHauling.Controls.Add(Me.txtMineRTMin)
        Me.gbMineHauling.Controls.Add(Me.txtMineRTSec)
        Me.gbMineHauling.Controls.Add(Me.lblMineRoundTripTime)
        Me.gbMineHauling.Location = New System.Drawing.Point(856, 241)
        Me.gbMineHauling.Name = "gbMineHauling"
        Me.gbMineHauling.Size = New System.Drawing.Size(195, 69)
        Me.gbMineHauling.TabIndex = 6
        Me.gbMineHauling.TabStop = False
        Me.gbMineHauling.Text = "Hauling:"
        '
        'txtMineHaulerM3
        '
        Me.txtMineHaulerM3.Location = New System.Drawing.Point(25, 40)
        Me.txtMineHaulerM3.Name = "txtMineHaulerM3"
        Me.txtMineHaulerM3.Size = New System.Drawing.Size(65, 20)
        Me.txtMineHaulerM3.TabIndex = 1
        Me.txtMineHaulerM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMineHaulerM3
        '
        Me.lblMineHaulerM3.AutoSize = True
        Me.lblMineHaulerM3.Location = New System.Drawing.Point(3, 43)
        Me.lblMineHaulerM3.Name = "lblMineHaulerM3"
        Me.lblMineHaulerM3.Size = New System.Drawing.Size(24, 13)
        Me.lblMineHaulerM3.TabIndex = 7
        Me.lblMineHaulerM3.Text = "m3:"
        '
        'lblMineRTSec
        '
        Me.lblMineRTSec.AutoSize = True
        Me.lblMineRTSec.Location = New System.Drawing.Point(151, 27)
        Me.lblMineRTSec.Name = "lblMineRTSec"
        Me.lblMineRTSec.Size = New System.Drawing.Size(29, 13)
        Me.lblMineRTSec.TabIndex = 5
        Me.lblMineRTSec.Text = "Sec:"
        '
        'chkMineUseHauler
        '
        Me.chkMineUseHauler.AutoSize = True
        Me.chkMineUseHauler.Location = New System.Drawing.Point(6, 21)
        Me.chkMineUseHauler.Name = "chkMineUseHauler"
        Me.chkMineUseHauler.Size = New System.Drawing.Size(79, 17)
        Me.chkMineUseHauler.TabIndex = 0
        Me.chkMineUseHauler.Text = "Use Hauler"
        Me.chkMineUseHauler.UseVisualStyleBackColor = True
        '
        'lblMineRTMin
        '
        Me.lblMineRTMin.AutoSize = True
        Me.lblMineRTMin.Location = New System.Drawing.Point(103, 27)
        Me.lblMineRTMin.Name = "lblMineRTMin"
        Me.lblMineRTMin.Size = New System.Drawing.Size(27, 13)
        Me.lblMineRTMin.TabIndex = 4
        Me.lblMineRTMin.Text = "Min:"
        '
        'txtMineRTMin
        '
        Me.txtMineRTMin.Location = New System.Drawing.Point(95, 40)
        Me.txtMineRTMin.Name = "txtMineRTMin"
        Me.txtMineRTMin.Size = New System.Drawing.Size(43, 20)
        Me.txtMineRTMin.TabIndex = 2
        Me.txtMineRTMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMineRTSec
        '
        Me.txtMineRTSec.Location = New System.Drawing.Point(143, 40)
        Me.txtMineRTSec.Name = "txtMineRTSec"
        Me.txtMineRTSec.Size = New System.Drawing.Size(43, 20)
        Me.txtMineRTSec.TabIndex = 3
        Me.txtMineRTSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblMineRoundTripTime
        '
        Me.lblMineRoundTripTime.AutoSize = True
        Me.lblMineRoundTripTime.Location = New System.Drawing.Point(80, 11)
        Me.lblMineRoundTripTime.Name = "lblMineRoundTripTime"
        Me.lblMineRoundTripTime.Size = New System.Drawing.Size(111, 13)
        Me.lblMineRoundTripTime.TabIndex = 1
        Me.lblMineRoundTripTime.Text = "Round Trip to Station:"
        Me.lblMineRoundTripTime.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnMineRefresh
        '
        Me.btnMineRefresh.Location = New System.Drawing.Point(637, 38)
        Me.btnMineRefresh.Name = "btnMineRefresh"
        Me.btnMineRefresh.Size = New System.Drawing.Size(68, 34)
        Me.btnMineRefresh.TabIndex = 1
        Me.btnMineRefresh.Text = "Calculate"
        Me.btnMineRefresh.UseVisualStyleBackColor = True
        '
        'gbMineBooster
        '
        Me.gbMineBooster.Controls.Add(Me.chkMineBoosterDroneRig3)
        Me.gbMineBooster.Controls.Add(Me.pictMineLaserOptmize)
        Me.gbMineBooster.Controls.Add(Me.pictMineRangeLink)
        Me.gbMineBooster.Controls.Add(Me.chkMineBoosterDroneRig2)
        Me.gbMineBooster.Controls.Add(Me.chkMineBoosterDroneRig1)
        Me.gbMineBooster.Controls.Add(Me.chkMineBoosterUseDrones)
        Me.gbMineBooster.Controls.Add(Me.pictMineFleetBoostShip)
        Me.gbMineBooster.Controls.Add(Me.chkMineForemanLaserRangeBoost)
        Me.gbMineBooster.Controls.Add(Me.chkMineIndyCoreDeployedMode)
        Me.gbMineBooster.Controls.Add(Me.cmbMineBoosterShipSkill)
        Me.gbMineBooster.Controls.Add(Me.chkMineForemanMindlink)
        Me.gbMineBooster.Controls.Add(Me.cmbMineBoosterShipName)
        Me.gbMineBooster.Controls.Add(Me.cmbMineMiningDirector)
        Me.gbMineBooster.Controls.Add(Me.chkMineForemanLaserOpBoost)
        Me.gbMineBooster.Controls.Add(Me.lblMineMiningDirector)
        Me.gbMineBooster.Controls.Add(Me.cmbMineMiningForeman)
        Me.gbMineBooster.Controls.Add(Me.lblMineFleetBoosterShip)
        Me.gbMineBooster.Controls.Add(Me.lblMineMiningForeman)
        Me.gbMineBooster.Controls.Add(Me.lblMineBoosterShipSkill)
        Me.gbMineBooster.Controls.Add(Me.cmbMineIndustReconfig)
        Me.gbMineBooster.Controls.Add(Me.lblMineIndustrialReconfig)
        Me.gbMineBooster.Location = New System.Drawing.Point(6, 156)
        Me.gbMineBooster.Name = "gbMineBooster"
        Me.gbMineBooster.Size = New System.Drawing.Size(568, 117)
        Me.gbMineBooster.TabIndex = 3
        Me.gbMineBooster.TabStop = False
        '
        'chkMineBoosterDroneRig3
        '
        Me.chkMineBoosterDroneRig3.AutoSize = True
        Me.chkMineBoosterDroneRig3.Location = New System.Drawing.Point(326, 95)
        Me.chkMineBoosterDroneRig3.Name = "chkMineBoosterDroneRig3"
        Me.chkMineBoosterDroneRig3.Size = New System.Drawing.Size(90, 17)
        Me.chkMineBoosterDroneRig3.TabIndex = 146
        Me.chkMineBoosterDroneRig3.Text = "T1 Drone Rig"
        Me.chkMineBoosterDroneRig3.ThreeState = True
        Me.chkMineBoosterDroneRig3.UseVisualStyleBackColor = True
        '
        'pictMineLaserOptmize
        '
        Me.pictMineLaserOptmize.Location = New System.Drawing.Point(528, 64)
        Me.pictMineLaserOptmize.Name = "pictMineLaserOptmize"
        Me.pictMineLaserOptmize.Size = New System.Drawing.Size(32, 32)
        Me.pictMineLaserOptmize.TabIndex = 139
        Me.pictMineLaserOptmize.TabStop = False
        '
        'pictMineRangeLink
        '
        Me.pictMineRangeLink.Location = New System.Drawing.Point(528, 24)
        Me.pictMineRangeLink.Name = "pictMineRangeLink"
        Me.pictMineRangeLink.Size = New System.Drawing.Size(32, 32)
        Me.pictMineRangeLink.TabIndex = 138
        Me.pictMineRangeLink.TabStop = False
        '
        'chkMineBoosterDroneRig2
        '
        Me.chkMineBoosterDroneRig2.AutoSize = True
        Me.chkMineBoosterDroneRig2.Location = New System.Drawing.Point(236, 95)
        Me.chkMineBoosterDroneRig2.Name = "chkMineBoosterDroneRig2"
        Me.chkMineBoosterDroneRig2.Size = New System.Drawing.Size(90, 17)
        Me.chkMineBoosterDroneRig2.TabIndex = 145
        Me.chkMineBoosterDroneRig2.Text = "T1 Drone Rig"
        Me.chkMineBoosterDroneRig2.ThreeState = True
        Me.chkMineBoosterDroneRig2.UseVisualStyleBackColor = True
        '
        'chkMineBoosterDroneRig1
        '
        Me.chkMineBoosterDroneRig1.AutoSize = True
        Me.chkMineBoosterDroneRig1.Location = New System.Drawing.Point(146, 95)
        Me.chkMineBoosterDroneRig1.Name = "chkMineBoosterDroneRig1"
        Me.chkMineBoosterDroneRig1.Size = New System.Drawing.Size(90, 17)
        Me.chkMineBoosterDroneRig1.TabIndex = 144
        Me.chkMineBoosterDroneRig1.Text = "T1 Drone Rig"
        Me.chkMineBoosterDroneRig1.ThreeState = True
        Me.chkMineBoosterDroneRig1.UseVisualStyleBackColor = True
        '
        'chkMineBoosterUseDrones
        '
        Me.chkMineBoosterUseDrones.AutoSize = True
        Me.chkMineBoosterUseDrones.Location = New System.Drawing.Point(18, 96)
        Me.chkMineBoosterUseDrones.Name = "chkMineBoosterUseDrones"
        Me.chkMineBoosterUseDrones.Size = New System.Drawing.Size(116, 17)
        Me.chkMineBoosterUseDrones.TabIndex = 141
        Me.chkMineBoosterUseDrones.Text = "Use Mining Drones"
        Me.chkMineBoosterUseDrones.UseVisualStyleBackColor = True
        '
        'pictMineFleetBoostShip
        '
        Me.pictMineFleetBoostShip.BackColor = System.Drawing.Color.White
        Me.pictMineFleetBoostShip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictMineFleetBoostShip.Location = New System.Drawing.Point(170, 17)
        Me.pictMineFleetBoostShip.Name = "pictMineFleetBoostShip"
        Me.pictMineFleetBoostShip.Size = New System.Drawing.Size(68, 69)
        Me.pictMineFleetBoostShip.TabIndex = 137
        Me.pictMineFleetBoostShip.TabStop = False
        '
        'chkMineForemanLaserRangeBoost
        '
        Me.chkMineForemanLaserRangeBoost.Location = New System.Drawing.Point(370, 18)
        Me.chkMineForemanLaserRangeBoost.Name = "chkMineForemanLaserRangeBoost"
        Me.chkMineForemanLaserRangeBoost.Size = New System.Drawing.Size(152, 45)
        Me.chkMineForemanLaserRangeBoost.TabIndex = 9
        Me.chkMineForemanLaserRangeBoost.Text = "Mining Foreman Link - Mining Laser Field Enhancement Charge"
        Me.chkMineForemanLaserRangeBoost.ThreeState = True
        Me.chkMineForemanLaserRangeBoost.UseVisualStyleBackColor = True
        '
        'chkMineIndyCoreDeployedMode
        '
        Me.chkMineIndyCoreDeployedMode.AutoSize = True
        Me.chkMineIndyCoreDeployedMode.Location = New System.Drawing.Point(18, 80)
        Me.chkMineIndyCoreDeployedMode.Name = "chkMineIndyCoreDeployedMode"
        Me.chkMineIndyCoreDeployedMode.Size = New System.Drawing.Size(134, 17)
        Me.chkMineIndyCoreDeployedMode.TabIndex = 4
        Me.chkMineIndyCoreDeployedMode.Text = "Industrial Core Inactive"
        Me.chkMineIndyCoreDeployedMode.ThreeState = True
        Me.chkMineIndyCoreDeployedMode.UseVisualStyleBackColor = True
        '
        'cmbMineBoosterShipSkill
        '
        Me.cmbMineBoosterShipSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterShipSkill.FormattingEnabled = True
        Me.cmbMineBoosterShipSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineBoosterShipSkill.Location = New System.Drawing.Point(77, 41)
        Me.cmbMineBoosterShipSkill.Name = "cmbMineBoosterShipSkill"
        Me.cmbMineBoosterShipSkill.Size = New System.Drawing.Size(46, 21)
        Me.cmbMineBoosterShipSkill.TabIndex = 2
        '
        'chkMineForemanMindlink
        '
        Me.chkMineForemanMindlink.AutoSize = True
        Me.chkMineForemanMindlink.Location = New System.Drawing.Point(18, 64)
        Me.chkMineForemanMindlink.Name = "chkMineForemanMindlink"
        Me.chkMineForemanMindlink.Size = New System.Drawing.Size(143, 17)
        Me.chkMineForemanMindlink.TabIndex = 3
        Me.chkMineForemanMindlink.Text = "Mining Foreman Mindlink"
        Me.chkMineForemanMindlink.UseVisualStyleBackColor = True
        '
        'cmbMineBoosterShipName
        '
        Me.cmbMineBoosterShipName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBoosterShipName.FormattingEnabled = True
        Me.cmbMineBoosterShipName.Items.AddRange(New Object() {"Orca", "Rorqual", "Battlecruiser", "Other"})
        Me.cmbMineBoosterShipName.Location = New System.Drawing.Point(77, 17)
        Me.cmbMineBoosterShipName.Name = "cmbMineBoosterShipName"
        Me.cmbMineBoosterShipName.Size = New System.Drawing.Size(85, 21)
        Me.cmbMineBoosterShipName.TabIndex = 1
        '
        'cmbMineMiningDirector
        '
        Me.cmbMineMiningDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningDirector.FormattingEnabled = True
        Me.cmbMineMiningDirector.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineMiningDirector.Location = New System.Drawing.Point(326, 41)
        Me.cmbMineMiningDirector.Name = "cmbMineMiningDirector"
        Me.cmbMineMiningDirector.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineMiningDirector.TabIndex = 6
        '
        'chkMineForemanLaserOpBoost
        '
        Me.chkMineForemanLaserOpBoost.Location = New System.Drawing.Point(370, 60)
        Me.chkMineForemanLaserOpBoost.Name = "chkMineForemanLaserOpBoost"
        Me.chkMineForemanLaserOpBoost.Size = New System.Drawing.Size(152, 40)
        Me.chkMineForemanLaserOpBoost.TabIndex = 10
        Me.chkMineForemanLaserOpBoost.Text = "Mining Foreman Link - Laser Optimization Charge"
        Me.chkMineForemanLaserOpBoost.ThreeState = True
        Me.chkMineForemanLaserOpBoost.UseVisualStyleBackColor = True
        '
        'lblMineMiningDirector
        '
        Me.lblMineMiningDirector.Location = New System.Drawing.Point(239, 44)
        Me.lblMineMiningDirector.Name = "lblMineMiningDirector"
        Me.lblMineMiningDirector.Size = New System.Drawing.Size(86, 13)
        Me.lblMineMiningDirector.TabIndex = 115
        Me.lblMineMiningDirector.Text = "Mining Director:"
        Me.lblMineMiningDirector.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMineMiningForeman
        '
        Me.cmbMineMiningForeman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningForeman.FormattingEnabled = True
        Me.cmbMineMiningForeman.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineMiningForeman.Location = New System.Drawing.Point(326, 18)
        Me.cmbMineMiningForeman.Name = "cmbMineMiningForeman"
        Me.cmbMineMiningForeman.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineMiningForeman.TabIndex = 5
        '
        'lblMineFleetBoosterShip
        '
        Me.lblMineFleetBoosterShip.AutoSize = True
        Me.lblMineFleetBoosterShip.Location = New System.Drawing.Point(15, 21)
        Me.lblMineFleetBoosterShip.Name = "lblMineFleetBoosterShip"
        Me.lblMineFleetBoosterShip.Size = New System.Drawing.Size(64, 13)
        Me.lblMineFleetBoosterShip.TabIndex = 119
        Me.lblMineFleetBoosterShip.Text = "Select Ship:"
        '
        'lblMineMiningForeman
        '
        Me.lblMineMiningForeman.Location = New System.Drawing.Point(239, 21)
        Me.lblMineMiningForeman.Name = "lblMineMiningForeman"
        Me.lblMineMiningForeman.Size = New System.Drawing.Size(86, 13)
        Me.lblMineMiningForeman.TabIndex = 113
        Me.lblMineMiningForeman.Text = "Mining Foreman:"
        Me.lblMineMiningForeman.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMineBoosterShipSkill
        '
        Me.lblMineBoosterShipSkill.AutoSize = True
        Me.lblMineBoosterShipSkill.Location = New System.Drawing.Point(16, 45)
        Me.lblMineBoosterShipSkill.Name = "lblMineBoosterShipSkill"
        Me.lblMineBoosterShipSkill.Size = New System.Drawing.Size(53, 13)
        Me.lblMineBoosterShipSkill.TabIndex = 131
        Me.lblMineBoosterShipSkill.Text = "Ship Skill:"
        '
        'cmbMineIndustReconfig
        '
        Me.cmbMineIndustReconfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineIndustReconfig.FormattingEnabled = True
        Me.cmbMineIndustReconfig.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineIndustReconfig.Location = New System.Drawing.Point(326, 64)
        Me.cmbMineIndustReconfig.Name = "cmbMineIndustReconfig"
        Me.cmbMineIndustReconfig.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineIndustReconfig.TabIndex = 8
        '
        'lblMineIndustrialReconfig
        '
        Me.lblMineIndustrialReconfig.Location = New System.Drawing.Point(232, 55)
        Me.lblMineIndustrialReconfig.Name = "lblMineIndustrialReconfig"
        Me.lblMineIndustrialReconfig.Size = New System.Drawing.Size(93, 39)
        Me.lblMineIndustrialReconfig.TabIndex = 135
        Me.lblMineIndustrialReconfig.Text = "Industrial Reconfiguration:"
        Me.lblMineIndustrialReconfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbMineRefining
        '
        Me.gbMineRefining.Controls.Add(Me.gbMineOreStuctureRates)
        Me.gbMineRefining.Controls.Add(Me.cmbMineRefining)
        Me.gbMineRefining.Controls.Add(Me.lblMineRefining)
        Me.gbMineRefining.Controls.Add(Me.cmbMineRefineryEff)
        Me.gbMineRefining.Controls.Add(Me.lblMineRefineryEfficiency)
        Me.gbMineRefining.Controls.Add(Me.MineRefineFacility)
        Me.gbMineRefining.Controls.Add(Me.tabMiningProcessingSkills)
        Me.gbMineRefining.Location = New System.Drawing.Point(711, 310)
        Me.gbMineRefining.Name = "gbMineRefining"
        Me.gbMineRefining.Size = New System.Drawing.Size(417, 299)
        Me.gbMineRefining.TabIndex = 8
        Me.gbMineRefining.TabStop = False
        Me.gbMineRefining.Text = "Refining Settings:"
        '
        'gbMineOreStuctureRates
        '
        Me.gbMineOreStuctureRates.Controls.Add(Me.lblMineFacilityOreRate)
        Me.gbMineOreStuctureRates.Controls.Add(Me.lblMineFacilityMoonOreRate)
        Me.gbMineOreStuctureRates.Controls.Add(Me.lblMineFacilityOreRate1)
        Me.gbMineOreStuctureRates.Controls.Add(Me.lblMineFacilityMoonOreRate1)
        Me.gbMineOreStuctureRates.Location = New System.Drawing.Point(312, 62)
        Me.gbMineOreStuctureRates.Name = "gbMineOreStuctureRates"
        Me.gbMineOreStuctureRates.Size = New System.Drawing.Size(101, 60)
        Me.gbMineOreStuctureRates.TabIndex = 121
        Me.gbMineOreStuctureRates.TabStop = False
        Me.gbMineOreStuctureRates.Text = "Refine Yields:"
        '
        'lblMineFacilityOreRate
        '
        Me.lblMineFacilityOreRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMineFacilityOreRate.Location = New System.Drawing.Point(47, 16)
        Me.lblMineFacilityOreRate.Name = "lblMineFacilityOreRate"
        Me.lblMineFacilityOreRate.Size = New System.Drawing.Size(50, 18)
        Me.lblMineFacilityOreRate.TabIndex = 136
        Me.lblMineFacilityOreRate.Text = "100.00%"
        Me.lblMineFacilityOreRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMineFacilityMoonOreRate
        '
        Me.lblMineFacilityMoonOreRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMineFacilityMoonOreRate.Location = New System.Drawing.Point(47, 37)
        Me.lblMineFacilityMoonOreRate.Name = "lblMineFacilityMoonOreRate"
        Me.lblMineFacilityMoonOreRate.Size = New System.Drawing.Size(50, 18)
        Me.lblMineFacilityMoonOreRate.TabIndex = 137
        Me.lblMineFacilityMoonOreRate.Text = "100.00%"
        Me.lblMineFacilityMoonOreRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMineFacilityOreRate1
        '
        Me.lblMineFacilityOreRate1.AutoSize = True
        Me.lblMineFacilityOreRate1.Location = New System.Drawing.Point(19, 19)
        Me.lblMineFacilityOreRate1.Name = "lblMineFacilityOreRate1"
        Me.lblMineFacilityOreRate1.Size = New System.Drawing.Size(27, 13)
        Me.lblMineFacilityOreRate1.TabIndex = 123
        Me.lblMineFacilityOreRate1.Text = "Ore:"
        '
        'lblMineFacilityMoonOreRate1
        '
        Me.lblMineFacilityMoonOreRate1.AutoSize = True
        Me.lblMineFacilityMoonOreRate1.Location = New System.Drawing.Point(9, 40)
        Me.lblMineFacilityMoonOreRate1.Name = "lblMineFacilityMoonOreRate1"
        Me.lblMineFacilityMoonOreRate1.Size = New System.Drawing.Size(37, 13)
        Me.lblMineFacilityMoonOreRate1.TabIndex = 125
        Me.lblMineFacilityMoonOreRate1.Text = "Moon:"
        '
        'cmbMineRefining
        '
        Me.cmbMineRefining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineRefining.FormattingEnabled = True
        Me.cmbMineRefining.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineRefining.Location = New System.Drawing.Point(382, 13)
        Me.cmbMineRefining.Name = "cmbMineRefining"
        Me.cmbMineRefining.Size = New System.Drawing.Size(30, 21)
        Me.cmbMineRefining.TabIndex = 0
        '
        'lblMineRefining
        '
        Me.lblMineRefining.AutoSize = True
        Me.lblMineRefining.Location = New System.Drawing.Point(309, 16)
        Me.lblMineRefining.Name = "lblMineRefining"
        Me.lblMineRefining.Size = New System.Drawing.Size(75, 13)
        Me.lblMineRefining.TabIndex = 108
        Me.lblMineRefining.Text = "Reprocessing:"
        '
        'cmbMineRefineryEff
        '
        Me.cmbMineRefineryEff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineRefineryEff.FormattingEnabled = True
        Me.cmbMineRefineryEff.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineRefineryEff.Location = New System.Drawing.Point(382, 36)
        Me.cmbMineRefineryEff.Name = "cmbMineRefineryEff"
        Me.cmbMineRefineryEff.Size = New System.Drawing.Size(30, 21)
        Me.cmbMineRefineryEff.TabIndex = 1
        '
        'lblMineRefineryEfficiency
        '
        Me.lblMineRefineryEfficiency.Location = New System.Drawing.Point(309, 33)
        Me.lblMineRefineryEfficiency.Name = "lblMineRefineryEfficiency"
        Me.lblMineRefineryEfficiency.Size = New System.Drawing.Size(77, 27)
        Me.lblMineRefineryEfficiency.TabIndex = 109
        Me.lblMineRefineryEfficiency.Text = "Reprocessing Efficiency:"
        '
        'MineRefineFacility
        '
        Me.MineRefineFacility.BackColor = System.Drawing.Color.Transparent
        Me.MineRefineFacility.Location = New System.Drawing.Point(7, 14)
        Me.MineRefineFacility.Name = "MineRefineFacility"
        Me.MineRefineFacility.Size = New System.Drawing.Size(303, 108)
        Me.MineRefineFacility.TabIndex = 122
        '
        'tabMiningProcessingSkills
        '
        Me.tabMiningProcessingSkills.Controls.Add(Me.tabPageOres)
        Me.tabMiningProcessingSkills.Controls.Add(Me.tabPageMoonOres)
        Me.tabMiningProcessingSkills.Controls.Add(Me.tabPageIce)
        Me.tabMiningProcessingSkills.Location = New System.Drawing.Point(7, 123)
        Me.tabMiningProcessingSkills.Name = "tabMiningProcessingSkills"
        Me.tabMiningProcessingSkills.SelectedIndex = 0
        Me.tabMiningProcessingSkills.Size = New System.Drawing.Size(318, 170)
        Me.tabMiningProcessingSkills.TabIndex = 121
        '
        'tabPageOres
        '
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing1)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing2)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing2)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing3)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing2)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing6)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing1)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing1)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing6)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing5)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing6)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing3)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing5)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing4)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing3)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing4)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing4)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing5)
        Me.tabPageOres.Location = New System.Drawing.Point(4, 22)
        Me.tabPageOres.Name = "tabPageOres"
        Me.tabPageOres.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageOres.Size = New System.Drawing.Size(310, 144)
        Me.tabPageOres.TabIndex = 0
        Me.tabPageOres.Text = "Ore Processing"
        Me.tabPageOres.UseVisualStyleBackColor = True
        '
        'chkOreProcessing1
        '
        Me.chkOreProcessing1.AutoSize = True
        Me.chkOreProcessing1.Location = New System.Drawing.Point(10, 10)
        Me.chkOreProcessing1.Name = "chkOreProcessing1"
        Me.chkOreProcessing1.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing1.TabIndex = 95
        Me.chkOreProcessing1.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing2
        '
        Me.cmbOreProcessing2.FormattingEnabled = True
        Me.cmbOreProcessing2.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing2.Location = New System.Drawing.Point(168, 29)
        Me.cmbOreProcessing2.Name = "cmbOreProcessing2"
        Me.cmbOreProcessing2.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing2.TabIndex = 102
        '
        'lblOreProcessing2
        '
        Me.lblOreProcessing2.Location = New System.Drawing.Point(32, 32)
        Me.lblOreProcessing2.Name = "lblOreProcessing2"
        Me.lblOreProcessing2.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing2.TabIndex = 133
        Me.lblOreProcessing2.Text = "Coherent Ore Processing"
        '
        'chkOreProcessing3
        '
        Me.chkOreProcessing3.AutoSize = True
        Me.chkOreProcessing3.Location = New System.Drawing.Point(10, 54)
        Me.chkOreProcessing3.Name = "chkOreProcessing3"
        Me.chkOreProcessing3.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing3.TabIndex = 107
        Me.chkOreProcessing3.UseVisualStyleBackColor = True
        '
        'chkOreProcessing2
        '
        Me.chkOreProcessing2.AutoSize = True
        Me.chkOreProcessing2.Location = New System.Drawing.Point(10, 32)
        Me.chkOreProcessing2.Name = "chkOreProcessing2"
        Me.chkOreProcessing2.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing2.TabIndex = 101
        Me.chkOreProcessing2.UseVisualStyleBackColor = True
        '
        'chkOreProcessing6
        '
        Me.chkOreProcessing6.AutoSize = True
        Me.chkOreProcessing6.Location = New System.Drawing.Point(10, 120)
        Me.chkOreProcessing6.Name = "chkOreProcessing6"
        Me.chkOreProcessing6.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing6.TabIndex = 125
        Me.chkOreProcessing6.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing1
        '
        Me.cmbOreProcessing1.FormattingEnabled = True
        Me.cmbOreProcessing1.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing1.Location = New System.Drawing.Point(168, 7)
        Me.cmbOreProcessing1.Name = "cmbOreProcessing1"
        Me.cmbOreProcessing1.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing1.TabIndex = 96
        '
        'lblOreProcessing1
        '
        Me.lblOreProcessing1.Location = New System.Drawing.Point(32, 10)
        Me.lblOreProcessing1.Name = "lblOreProcessing1"
        Me.lblOreProcessing1.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing1.TabIndex = 127
        Me.lblOreProcessing1.Text = "Simple Ore Processing"
        '
        'lblOreProcessing6
        '
        Me.lblOreProcessing6.Location = New System.Drawing.Point(32, 120)
        Me.lblOreProcessing6.Name = "lblOreProcessing6"
        Me.lblOreProcessing6.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing6.TabIndex = 142
        Me.lblOreProcessing6.Text = "Mercoxit Ore Processing"
        '
        'chkOreProcessing5
        '
        Me.chkOreProcessing5.AutoSize = True
        Me.chkOreProcessing5.Location = New System.Drawing.Point(10, 98)
        Me.chkOreProcessing5.Name = "chkOreProcessing5"
        Me.chkOreProcessing5.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing5.TabIndex = 121
        Me.chkOreProcessing5.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing6
        '
        Me.cmbOreProcessing6.FormattingEnabled = True
        Me.cmbOreProcessing6.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing6.Location = New System.Drawing.Point(168, 117)
        Me.cmbOreProcessing6.Name = "cmbOreProcessing6"
        Me.cmbOreProcessing6.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing6.TabIndex = 126
        '
        'lblOreProcessing3
        '
        Me.lblOreProcessing3.Location = New System.Drawing.Point(32, 54)
        Me.lblOreProcessing3.Name = "lblOreProcessing3"
        Me.lblOreProcessing3.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing3.TabIndex = 130
        Me.lblOreProcessing3.Text = "Variegated Ore Processing"
        '
        'lblOreProcessing5
        '
        Me.lblOreProcessing5.Location = New System.Drawing.Point(32, 98)
        Me.lblOreProcessing5.Name = "lblOreProcessing5"
        Me.lblOreProcessing5.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing5.TabIndex = 141
        Me.lblOreProcessing5.Text = "Abyssal Ore Processing"
        '
        'cmbOreProcessing4
        '
        Me.cmbOreProcessing4.FormattingEnabled = True
        Me.cmbOreProcessing4.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing4.Location = New System.Drawing.Point(168, 73)
        Me.cmbOreProcessing4.Name = "cmbOreProcessing4"
        Me.cmbOreProcessing4.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing4.TabIndex = 114
        '
        'cmbOreProcessing3
        '
        Me.cmbOreProcessing3.FormattingEnabled = True
        Me.cmbOreProcessing3.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing3.Location = New System.Drawing.Point(168, 51)
        Me.cmbOreProcessing3.Name = "cmbOreProcessing3"
        Me.cmbOreProcessing3.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing3.TabIndex = 108
        '
        'chkOreProcessing4
        '
        Me.chkOreProcessing4.AutoSize = True
        Me.chkOreProcessing4.Location = New System.Drawing.Point(10, 76)
        Me.chkOreProcessing4.Name = "chkOreProcessing4"
        Me.chkOreProcessing4.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing4.TabIndex = 113
        Me.chkOreProcessing4.UseVisualStyleBackColor = True
        '
        'lblOreProcessing4
        '
        Me.lblOreProcessing4.Location = New System.Drawing.Point(32, 76)
        Me.lblOreProcessing4.Name = "lblOreProcessing4"
        Me.lblOreProcessing4.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing4.TabIndex = 139
        Me.lblOreProcessing4.Text = "Complex Ore Processing"
        '
        'cmbOreProcessing5
        '
        Me.cmbOreProcessing5.FormattingEnabled = True
        Me.cmbOreProcessing5.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing5.Location = New System.Drawing.Point(168, 95)
        Me.cmbOreProcessing5.Name = "cmbOreProcessing5"
        Me.cmbOreProcessing5.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing5.TabIndex = 122
        '
        'tabPageMoonOres
        '
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing7)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing8)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing10)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing11)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing11)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing9)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing9)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing8)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing10)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing7)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing10)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing7)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing9)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing11)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing8)
        Me.tabPageMoonOres.Location = New System.Drawing.Point(4, 22)
        Me.tabPageMoonOres.Name = "tabPageMoonOres"
        Me.tabPageMoonOres.Size = New System.Drawing.Size(310, 144)
        Me.tabPageMoonOres.TabIndex = 2
        Me.tabPageMoonOres.Text = "Moon Ore Processing"
        Me.tabPageMoonOres.UseVisualStyleBackColor = True
        '
        'lblOreProcessing7
        '
        Me.lblOreProcessing7.Location = New System.Drawing.Point(31, 10)
        Me.lblOreProcessing7.Name = "lblOreProcessing7"
        Me.lblOreProcessing7.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing7.TabIndex = 148
        Me.lblOreProcessing7.Text = "Ubiquitous Moon Ore Processing"
        '
        'lblOreProcessing8
        '
        Me.lblOreProcessing8.Location = New System.Drawing.Point(31, 32)
        Me.lblOreProcessing8.Name = "lblOreProcessing8"
        Me.lblOreProcessing8.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing8.TabIndex = 149
        Me.lblOreProcessing8.Text = "Uncommon Moon Ore Processing"
        '
        'lblOreProcessing10
        '
        Me.lblOreProcessing10.Location = New System.Drawing.Point(31, 76)
        Me.lblOreProcessing10.Name = "lblOreProcessing10"
        Me.lblOreProcessing10.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing10.TabIndex = 145
        Me.lblOreProcessing10.Text = "Common Moon Ore Processing"
        '
        'lblOreProcessing11
        '
        Me.lblOreProcessing11.Location = New System.Drawing.Point(31, 98)
        Me.lblOreProcessing11.Name = "lblOreProcessing11"
        Me.lblOreProcessing11.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing11.TabIndex = 147
        Me.lblOreProcessing11.Text = "Rare Moon Ore Processing"
        '
        'cmbOreProcessing11
        '
        Me.cmbOreProcessing11.FormattingEnabled = True
        Me.cmbOreProcessing11.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing11.Location = New System.Drawing.Point(202, 95)
        Me.cmbOreProcessing11.Name = "cmbOreProcessing11"
        Me.cmbOreProcessing11.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing11.TabIndex = 140
        '
        'chkOreProcessing9
        '
        Me.chkOreProcessing9.AutoSize = True
        Me.chkOreProcessing9.Location = New System.Drawing.Point(10, 54)
        Me.chkOreProcessing9.Name = "chkOreProcessing9"
        Me.chkOreProcessing9.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing9.TabIndex = 137
        Me.chkOreProcessing9.UseVisualStyleBackColor = True
        '
        'lblOreProcessing9
        '
        Me.lblOreProcessing9.Location = New System.Drawing.Point(31, 54)
        Me.lblOreProcessing9.Name = "lblOreProcessing9"
        Me.lblOreProcessing9.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing9.TabIndex = 146
        Me.lblOreProcessing9.Text = "Exceptional Moon Ore Processing"
        '
        'chkOreProcessing8
        '
        Me.chkOreProcessing8.AutoSize = True
        Me.chkOreProcessing8.Location = New System.Drawing.Point(10, 32)
        Me.chkOreProcessing8.Name = "chkOreProcessing8"
        Me.chkOreProcessing8.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing8.TabIndex = 143
        Me.chkOreProcessing8.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing10
        '
        Me.cmbOreProcessing10.FormattingEnabled = True
        Me.cmbOreProcessing10.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing10.Location = New System.Drawing.Point(202, 73)
        Me.cmbOreProcessing10.Name = "cmbOreProcessing10"
        Me.cmbOreProcessing10.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing10.TabIndex = 136
        '
        'cmbOreProcessing7
        '
        Me.cmbOreProcessing7.FormattingEnabled = True
        Me.cmbOreProcessing7.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing7.Location = New System.Drawing.Point(202, 7)
        Me.cmbOreProcessing7.Name = "cmbOreProcessing7"
        Me.cmbOreProcessing7.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing7.TabIndex = 142
        '
        'chkOreProcessing10
        '
        Me.chkOreProcessing10.AutoSize = True
        Me.chkOreProcessing10.Location = New System.Drawing.Point(10, 76)
        Me.chkOreProcessing10.Name = "chkOreProcessing10"
        Me.chkOreProcessing10.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing10.TabIndex = 135
        Me.chkOreProcessing10.UseVisualStyleBackColor = True
        '
        'chkOreProcessing7
        '
        Me.chkOreProcessing7.AutoSize = True
        Me.chkOreProcessing7.Location = New System.Drawing.Point(10, 10)
        Me.chkOreProcessing7.Name = "chkOreProcessing7"
        Me.chkOreProcessing7.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing7.TabIndex = 141
        Me.chkOreProcessing7.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing9
        '
        Me.cmbOreProcessing9.FormattingEnabled = True
        Me.cmbOreProcessing9.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing9.Location = New System.Drawing.Point(202, 51)
        Me.cmbOreProcessing9.Name = "cmbOreProcessing9"
        Me.cmbOreProcessing9.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing9.TabIndex = 138
        '
        'chkOreProcessing11
        '
        Me.chkOreProcessing11.AutoSize = True
        Me.chkOreProcessing11.Location = New System.Drawing.Point(10, 98)
        Me.chkOreProcessing11.Name = "chkOreProcessing11"
        Me.chkOreProcessing11.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing11.TabIndex = 139
        Me.chkOreProcessing11.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing8
        '
        Me.cmbOreProcessing8.FormattingEnabled = True
        Me.cmbOreProcessing8.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing8.Location = New System.Drawing.Point(202, 29)
        Me.cmbOreProcessing8.Name = "cmbOreProcessing8"
        Me.cmbOreProcessing8.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing8.TabIndex = 144
        '
        'tabPageIce
        '
        Me.tabPageIce.Controls.Add(Me.cmbOreProcessing12)
        Me.tabPageIce.Controls.Add(Me.lblOreProcessing12)
        Me.tabPageIce.Controls.Add(Me.chkOreProcessing12)
        Me.tabPageIce.Location = New System.Drawing.Point(4, 22)
        Me.tabPageIce.Name = "tabPageIce"
        Me.tabPageIce.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageIce.Size = New System.Drawing.Size(310, 144)
        Me.tabPageIce.TabIndex = 3
        Me.tabPageIce.Text = "Ice Processing"
        Me.tabPageIce.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing12
        '
        Me.cmbOreProcessing12.FormattingEnabled = True
        Me.cmbOreProcessing12.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing12.Location = New System.Drawing.Point(168, 7)
        Me.cmbOreProcessing12.Name = "cmbOreProcessing12"
        Me.cmbOreProcessing12.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing12.TabIndex = 147
        '
        'lblOreProcessing12
        '
        Me.lblOreProcessing12.Location = New System.Drawing.Point(29, 11)
        Me.lblOreProcessing12.Name = "lblOreProcessing12"
        Me.lblOreProcessing12.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing12.TabIndex = 148
        Me.lblOreProcessing12.Text = "Ice Processing"
        '
        'chkOreProcessing12
        '
        Me.chkOreProcessing12.AutoSize = True
        Me.chkOreProcessing12.Location = New System.Drawing.Point(10, 10)
        Me.chkOreProcessing12.Name = "chkOreProcessing12"
        Me.chkOreProcessing12.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing12.TabIndex = 146
        Me.chkOreProcessing12.UseVisualStyleBackColor = True
        '
        'gbMineShipSetup
        '
        Me.gbMineShipSetup.Controls.Add(Me.gbMineSelectShip)
        Me.gbMineShipSetup.Controls.Add(Me.gbMineShipEquipment)
        Me.gbMineShipSetup.Controls.Add(Me.gbMineSkills)
        Me.gbMineShipSetup.Location = New System.Drawing.Point(711, 8)
        Me.gbMineShipSetup.Name = "gbMineShipSetup"
        Me.gbMineShipSetup.Size = New System.Drawing.Size(418, 233)
        Me.gbMineShipSetup.TabIndex = 4
        Me.gbMineShipSetup.TabStop = False
        Me.gbMineShipSetup.Text = "Mining Skills/Ship Setup:"
        '
        'gbMineSelectShip
        '
        Me.gbMineSelectShip.Controls.Add(Me.pictMineSelectedShip)
        Me.gbMineSelectShip.Controls.Add(Me.lblMineSelectShip)
        Me.gbMineSelectShip.Controls.Add(Me.cmbMineBaseShipSkill)
        Me.gbMineSelectShip.Controls.Add(Me.cmbMineAdvShipSkill)
        Me.gbMineSelectShip.Controls.Add(Me.cmbMineShipName)
        Me.gbMineSelectShip.Controls.Add(Me.lblMineBaseShipSkill)
        Me.gbMineSelectShip.Controls.Add(Me.lblMineExhumers)
        Me.gbMineSelectShip.Location = New System.Drawing.Point(6, 13)
        Me.gbMineSelectShip.Name = "gbMineSelectShip"
        Me.gbMineSelectShip.Size = New System.Drawing.Size(111, 174)
        Me.gbMineSelectShip.TabIndex = 0
        Me.gbMineSelectShip.TabStop = False
        Me.gbMineSelectShip.Text = "Select Ship:"
        '
        'pictMineSelectedShip
        '
        Me.pictMineSelectedShip.BackColor = System.Drawing.Color.White
        Me.pictMineSelectedShip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictMineSelectedShip.Location = New System.Drawing.Point(21, 95)
        Me.pictMineSelectedShip.Name = "pictMineSelectedShip"
        Me.pictMineSelectedShip.Size = New System.Drawing.Size(68, 69)
        Me.pictMineSelectedShip.TabIndex = 138
        Me.pictMineSelectedShip.TabStop = False
        '
        'lblMineSelectShip
        '
        Me.lblMineSelectShip.AutoSize = True
        Me.lblMineSelectShip.Location = New System.Drawing.Point(3, 15)
        Me.lblMineSelectShip.Name = "lblMineSelectShip"
        Me.lblMineSelectShip.Size = New System.Drawing.Size(62, 13)
        Me.lblMineSelectShip.TabIndex = 0
        Me.lblMineSelectShip.Text = "Ship Name:"
        '
        'cmbMineBaseShipSkill
        '
        Me.cmbMineBaseShipSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineBaseShipSkill.FormattingEnabled = True
        Me.cmbMineBaseShipSkill.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cmbMineBaseShipSkill.Location = New System.Drawing.Point(6, 70)
        Me.cmbMineBaseShipSkill.Name = "cmbMineBaseShipSkill"
        Me.cmbMineBaseShipSkill.Size = New System.Drawing.Size(48, 21)
        Me.cmbMineBaseShipSkill.TabIndex = 1
        '
        'cmbMineAdvShipSkill
        '
        Me.cmbMineAdvShipSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineAdvShipSkill.FormattingEnabled = True
        Me.cmbMineAdvShipSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineAdvShipSkill.Location = New System.Drawing.Point(57, 70)
        Me.cmbMineAdvShipSkill.Name = "cmbMineAdvShipSkill"
        Me.cmbMineAdvShipSkill.Size = New System.Drawing.Size(48, 21)
        Me.cmbMineAdvShipSkill.TabIndex = 2
        '
        'cmbMineShipName
        '
        Me.cmbMineShipName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineShipName.FormattingEnabled = True
        Me.cmbMineShipName.Location = New System.Drawing.Point(6, 30)
        Me.cmbMineShipName.Name = "cmbMineShipName"
        Me.cmbMineShipName.Size = New System.Drawing.Size(99, 21)
        Me.cmbMineShipName.TabIndex = 0
        '
        'lblMineBaseShipSkill
        '
        Me.lblMineBaseShipSkill.AutoSize = True
        Me.lblMineBaseShipSkill.Location = New System.Drawing.Point(3, 54)
        Me.lblMineBaseShipSkill.Name = "lblMineBaseShipSkill"
        Me.lblMineBaseShipSkill.Size = New System.Drawing.Size(53, 13)
        Me.lblMineBaseShipSkill.TabIndex = 130
        Me.lblMineBaseShipSkill.Text = "Ship Skill:"
        '
        'lblMineExhumers
        '
        Me.lblMineExhumers.AutoSize = True
        Me.lblMineExhumers.Location = New System.Drawing.Point(54, 54)
        Me.lblMineExhumers.Name = "lblMineExhumers"
        Me.lblMineExhumers.Size = New System.Drawing.Size(57, 13)
        Me.lblMineExhumers.TabIndex = 128
        Me.lblMineExhumers.Text = "Spec Skill:"
        '
        'gbMineShipEquipment
        '
        Me.gbMineShipEquipment.Controls.Add(Me.gbMiningRigs)
        Me.gbMineShipEquipment.Controls.Add(Me.cmbMineMiningLaser)
        Me.gbMineShipEquipment.Controls.Add(Me.cmbMineNumMiningUpgrades)
        Me.gbMineShipEquipment.Controls.Add(Me.cmbMineNumLasers)
        Me.gbMineShipEquipment.Controls.Add(Me.cmbMineMiningUpgrade)
        Me.gbMineShipEquipment.Controls.Add(Me.cmbMineHighwallImplant)
        Me.gbMineShipEquipment.Controls.Add(Me.chkMineMichiImplant)
        Me.gbMineShipEquipment.Controls.Add(Me.lblMineImplants)
        Me.gbMineShipEquipment.Controls.Add(Me.lblMineLaserNumber)
        Me.gbMineShipEquipment.Controls.Add(Me.lblMineNumMiningUpgrades)
        Me.gbMineShipEquipment.Controls.Add(Me.lblMineMinerTurret)
        Me.gbMineShipEquipment.Controls.Add(Me.lblMineMiningUpgrade)
        Me.gbMineShipEquipment.Location = New System.Drawing.Point(122, 13)
        Me.gbMineShipEquipment.Name = "gbMineShipEquipment"
        Me.gbMineShipEquipment.Size = New System.Drawing.Size(290, 174)
        Me.gbMineShipEquipment.TabIndex = 1
        Me.gbMineShipEquipment.TabStop = False
        Me.gbMineShipEquipment.Text = "Select Ship Equipment:"
        '
        'gbMiningRigs
        '
        Me.gbMiningRigs.Controls.Add(Me.cmbMineMiningRig3)
        Me.gbMiningRigs.Controls.Add(Me.cmbMineMiningRig1)
        Me.gbMiningRigs.Controls.Add(Me.cmbMineMiningRig2)
        Me.gbMiningRigs.Location = New System.Drawing.Point(6, 127)
        Me.gbMiningRigs.Name = "gbMiningRigs"
        Me.gbMiningRigs.Size = New System.Drawing.Size(279, 41)
        Me.gbMiningRigs.TabIndex = 136
        Me.gbMiningRigs.TabStop = False
        Me.gbMiningRigs.Text = "Mining Rigs:"
        '
        'cmbMineMiningRig3
        '
        Me.cmbMineMiningRig3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningRig3.FormattingEnabled = True
        Me.cmbMineMiningRig3.Items.AddRange(New Object() {"None", "T1 Drone Rig", "T2 Drone Rig"})
        Me.cmbMineMiningRig3.Location = New System.Drawing.Point(186, 15)
        Me.cmbMineMiningRig3.Name = "cmbMineMiningRig3"
        Me.cmbMineMiningRig3.Size = New System.Drawing.Size(89, 21)
        Me.cmbMineMiningRig3.TabIndex = 146
        '
        'cmbMineMiningRig1
        '
        Me.cmbMineMiningRig1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningRig1.FormattingEnabled = True
        Me.cmbMineMiningRig1.Items.AddRange(New Object() {"None", "T1 Drone Rig", "T2 Drone Rig"})
        Me.cmbMineMiningRig1.Location = New System.Drawing.Point(4, 15)
        Me.cmbMineMiningRig1.Name = "cmbMineMiningRig1"
        Me.cmbMineMiningRig1.Size = New System.Drawing.Size(89, 21)
        Me.cmbMineMiningRig1.TabIndex = 137
        '
        'cmbMineMiningRig2
        '
        Me.cmbMineMiningRig2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningRig2.FormattingEnabled = True
        Me.cmbMineMiningRig2.Items.AddRange(New Object() {"None", "T1 Drone Rig", "T2 Drone Rig"})
        Me.cmbMineMiningRig2.Location = New System.Drawing.Point(95, 15)
        Me.cmbMineMiningRig2.Name = "cmbMineMiningRig2"
        Me.cmbMineMiningRig2.Size = New System.Drawing.Size(89, 21)
        Me.cmbMineMiningRig2.TabIndex = 145
        '
        'cmbMineMiningLaser
        '
        Me.cmbMineMiningLaser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningLaser.FormattingEnabled = True
        Me.cmbMineMiningLaser.Location = New System.Drawing.Point(67, 17)
        Me.cmbMineMiningLaser.Name = "cmbMineMiningLaser"
        Me.cmbMineMiningLaser.Size = New System.Drawing.Size(218, 21)
        Me.cmbMineMiningLaser.TabIndex = 0
        '
        'cmbMineNumMiningUpgrades
        '
        Me.cmbMineNumMiningUpgrades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineNumMiningUpgrades.FormattingEnabled = True
        Me.cmbMineNumMiningUpgrades.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineNumMiningUpgrades.Location = New System.Drawing.Point(249, 69)
        Me.cmbMineNumMiningUpgrades.Name = "cmbMineNumMiningUpgrades"
        Me.cmbMineNumMiningUpgrades.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineNumMiningUpgrades.TabIndex = 3
        '
        'cmbMineNumLasers
        '
        Me.cmbMineNumLasers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineNumLasers.FormattingEnabled = True
        Me.cmbMineNumLasers.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineNumLasers.Location = New System.Drawing.Point(101, 69)
        Me.cmbMineNumLasers.Name = "cmbMineNumLasers"
        Me.cmbMineNumLasers.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineNumLasers.TabIndex = 2
        '
        'cmbMineMiningUpgrade
        '
        Me.cmbMineMiningUpgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineMiningUpgrade.FormattingEnabled = True
        Me.cmbMineMiningUpgrade.Items.AddRange(New Object() {"None", "5% (T1)", "8% (M1)", "9% (T2)", "9% (M6)", "10% (M6)"})
        Me.cmbMineMiningUpgrade.Location = New System.Drawing.Point(55, 42)
        Me.cmbMineMiningUpgrade.Name = "cmbMineMiningUpgrade"
        Me.cmbMineMiningUpgrade.Size = New System.Drawing.Size(230, 21)
        Me.cmbMineMiningUpgrade.TabIndex = 1
        '
        'cmbMineHighwallImplant
        '
        Me.cmbMineHighwallImplant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineHighwallImplant.FormattingEnabled = True
        Me.cmbMineHighwallImplant.Location = New System.Drawing.Point(45, 101)
        Me.cmbMineHighwallImplant.Name = "cmbMineHighwallImplant"
        Me.cmbMineHighwallImplant.Size = New System.Drawing.Size(120, 21)
        Me.cmbMineHighwallImplant.TabIndex = 5
        '
        'chkMineMichiImplant
        '
        Me.chkMineMichiImplant.Location = New System.Drawing.Point(172, 94)
        Me.chkMineMichiImplant.Name = "chkMineMichiImplant"
        Me.chkMineMichiImplant.Size = New System.Drawing.Size(124, 34)
        Me.chkMineMichiImplant.TabIndex = 6
        Me.chkMineMichiImplant.Text = "Michi's Excavation Augmentor"
        Me.chkMineMichiImplant.UseVisualStyleBackColor = True
        '
        'lblMineImplants
        '
        Me.lblMineImplants.Location = New System.Drawing.Point(1, 97)
        Me.lblMineImplants.Name = "lblMineImplants"
        Me.lblMineImplants.Size = New System.Drawing.Size(60, 29)
        Me.lblMineImplants.TabIndex = 23
        Me.lblMineImplants.Text = "Mining Implant:"
        '
        'lblMineLaserNumber
        '
        Me.lblMineLaserNumber.AutoSize = True
        Me.lblMineLaserNumber.Location = New System.Drawing.Point(1, 73)
        Me.lblMineLaserNumber.Name = "lblMineLaserNumber"
        Me.lblMineLaserNumber.Size = New System.Drawing.Size(94, 13)
        Me.lblMineLaserNumber.TabIndex = 125
        Me.lblMineLaserNumber.Text = "# Mining Modules:"
        '
        'lblMineNumMiningUpgrades
        '
        Me.lblMineNumMiningUpgrades.AutoSize = True
        Me.lblMineNumMiningUpgrades.Location = New System.Drawing.Point(143, 73)
        Me.lblMineNumMiningUpgrades.Name = "lblMineNumMiningUpgrades"
        Me.lblMineNumMiningUpgrades.Size = New System.Drawing.Size(100, 13)
        Me.lblMineNumMiningUpgrades.TabIndex = 129
        Me.lblMineNumMiningUpgrades.Text = "# Mining Upgrades:"
        '
        'lblMineMinerTurret
        '
        Me.lblMineMinerTurret.AutoSize = True
        Me.lblMineMinerTurret.Location = New System.Drawing.Point(1, 20)
        Me.lblMineMinerTurret.Name = "lblMineMinerTurret"
        Me.lblMineMinerTurret.Size = New System.Drawing.Size(67, 13)
        Me.lblMineMinerTurret.TabIndex = 131
        Me.lblMineMinerTurret.Text = "Miner Name:"
        '
        'lblMineMiningUpgrade
        '
        Me.lblMineMiningUpgrade.AutoSize = True
        Me.lblMineMiningUpgrade.Location = New System.Drawing.Point(1, 47)
        Me.lblMineMiningUpgrade.Name = "lblMineMiningUpgrade"
        Me.lblMineMiningUpgrade.Size = New System.Drawing.Size(51, 13)
        Me.lblMineMiningUpgrade.TabIndex = 132
        Me.lblMineMiningUpgrade.Text = "Upgrade:"
        '
        'gbMineSkills
        '
        Me.gbMineSkills.Controls.Add(Me.cmbMineGasIceHarvesting)
        Me.gbMineSkills.Controls.Add(Me.lblMineGasIceHarvesting)
        Me.gbMineSkills.Controls.Add(Me.cmbMineDeepCore)
        Me.gbMineSkills.Controls.Add(Me.lblMineAstrogeology)
        Me.gbMineSkills.Controls.Add(Me.cmbMineSkill)
        Me.gbMineSkills.Controls.Add(Me.lblMineSkill)
        Me.gbMineSkills.Controls.Add(Me.cmbMineAstrogeology)
        Me.gbMineSkills.Controls.Add(Me.lblMineDeepCore)
        Me.gbMineSkills.Location = New System.Drawing.Point(6, 187)
        Me.gbMineSkills.Name = "gbMineSkills"
        Me.gbMineSkills.Size = New System.Drawing.Size(406, 41)
        Me.gbMineSkills.TabIndex = 2
        Me.gbMineSkills.TabStop = False
        Me.gbMineSkills.Text = "Skills:"
        '
        'cmbMineGasIceHarvesting
        '
        Me.cmbMineGasIceHarvesting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineGasIceHarvesting.FormattingEnabled = True
        Me.cmbMineGasIceHarvesting.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineGasIceHarvesting.Location = New System.Drawing.Point(141, 13)
        Me.cmbMineGasIceHarvesting.Name = "cmbMineGasIceHarvesting"
        Me.cmbMineGasIceHarvesting.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineGasIceHarvesting.TabIndex = 1
        '
        'lblMineGasIceHarvesting
        '
        Me.lblMineGasIceHarvesting.AutoSize = True
        Me.lblMineGasIceHarvesting.Location = New System.Drawing.Point(89, 17)
        Me.lblMineGasIceHarvesting.Name = "lblMineGasIceHarvesting"
        Me.lblMineGasIceHarvesting.Size = New System.Drawing.Size(51, 13)
        Me.lblMineGasIceHarvesting.TabIndex = 117
        Me.lblMineGasIceHarvesting.Text = "Ice Harv:"
        '
        'cmbMineDeepCore
        '
        Me.cmbMineDeepCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineDeepCore.FormattingEnabled = True
        Me.cmbMineDeepCore.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineDeepCore.Location = New System.Drawing.Point(356, 13)
        Me.cmbMineDeepCore.Name = "cmbMineDeepCore"
        Me.cmbMineDeepCore.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineDeepCore.TabIndex = 3
        '
        'lblMineAstrogeology
        '
        Me.lblMineAstrogeology.AutoSize = True
        Me.lblMineAstrogeology.Location = New System.Drawing.Point(183, 17)
        Me.lblMineAstrogeology.Name = "lblMineAstrogeology"
        Me.lblMineAstrogeology.Size = New System.Drawing.Size(71, 13)
        Me.lblMineAstrogeology.TabIndex = 109
        Me.lblMineAstrogeology.Text = "Astrogeology:"
        '
        'cmbMineSkill
        '
        Me.cmbMineSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineSkill.FormattingEnabled = True
        Me.cmbMineSkill.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineSkill.Location = New System.Drawing.Point(48, 13)
        Me.cmbMineSkill.Name = "cmbMineSkill"
        Me.cmbMineSkill.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineSkill.TabIndex = 0
        '
        'lblMineSkill
        '
        Me.lblMineSkill.AutoSize = True
        Me.lblMineSkill.Location = New System.Drawing.Point(6, 17)
        Me.lblMineSkill.Name = "lblMineSkill"
        Me.lblMineSkill.Size = New System.Drawing.Size(41, 13)
        Me.lblMineSkill.TabIndex = 108
        Me.lblMineSkill.Text = "Mining:"
        '
        'cmbMineAstrogeology
        '
        Me.cmbMineAstrogeology.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMineAstrogeology.FormattingEnabled = True
        Me.cmbMineAstrogeology.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbMineAstrogeology.Location = New System.Drawing.Point(255, 13)
        Me.cmbMineAstrogeology.Name = "cmbMineAstrogeology"
        Me.cmbMineAstrogeology.Size = New System.Drawing.Size(36, 21)
        Me.cmbMineAstrogeology.TabIndex = 2
        '
        'lblMineDeepCore
        '
        Me.lblMineDeepCore.Location = New System.Drawing.Point(297, 10)
        Me.lblMineDeepCore.Name = "lblMineDeepCore"
        Me.lblMineDeepCore.Size = New System.Drawing.Size(60, 27)
        Me.lblMineDeepCore.TabIndex = 114
        Me.lblMineDeepCore.Text = "Deep Core Mining:"
        '
        'gbMineMain
        '
        Me.gbMineMain.Controls.Add(Me.gbMineIncludeOres)
        Me.gbMineMain.Controls.Add(Me.cmbMineOreType)
        Me.gbMineMain.Controls.Add(Me.gbMineOreLocSov)
        Me.gbMineMain.Controls.Add(Me.lblMineType)
        Me.gbMineMain.Location = New System.Drawing.Point(6, 8)
        Me.gbMineMain.Name = "gbMineMain"
        Me.gbMineMain.Size = New System.Drawing.Size(362, 144)
        Me.gbMineMain.TabIndex = 0
        Me.gbMineMain.TabStop = False
        Me.gbMineMain.Text = "Options:"
        '
        'gbMineIncludeOres
        '
        Me.gbMineIncludeOres.Controls.Add(Me.chkMineIncludeHighSec)
        Me.gbMineIncludeOres.Controls.Add(Me.chkMineIncludeNullSec)
        Me.gbMineIncludeOres.Controls.Add(Me.chkMineIncludeLowSec)
        Me.gbMineIncludeOres.Controls.Add(Me.chkMineIncludeHighYieldOre)
        Me.gbMineIncludeOres.Location = New System.Drawing.Point(6, 38)
        Me.gbMineIncludeOres.Name = "gbMineIncludeOres"
        Me.gbMineIncludeOres.Size = New System.Drawing.Size(105, 100)
        Me.gbMineIncludeOres.TabIndex = 4
        Me.gbMineIncludeOres.TabStop = False
        Me.gbMineIncludeOres.Text = "Include:"
        '
        'chkMineIncludeHighSec
        '
        Me.chkMineIncludeHighSec.AutoSize = True
        Me.chkMineIncludeHighSec.Location = New System.Drawing.Point(9, 18)
        Me.chkMineIncludeHighSec.Name = "chkMineIncludeHighSec"
        Me.chkMineIncludeHighSec.Size = New System.Drawing.Size(95, 17)
        Me.chkMineIncludeHighSec.TabIndex = 0
        Me.chkMineIncludeHighSec.Text = "High Sec Ores"
        Me.chkMineIncludeHighSec.UseVisualStyleBackColor = True
        '
        'chkMineIncludeNullSec
        '
        Me.chkMineIncludeNullSec.AutoSize = True
        Me.chkMineIncludeNullSec.Location = New System.Drawing.Point(9, 52)
        Me.chkMineIncludeNullSec.Name = "chkMineIncludeNullSec"
        Me.chkMineIncludeNullSec.Size = New System.Drawing.Size(91, 17)
        Me.chkMineIncludeNullSec.TabIndex = 2
        Me.chkMineIncludeNullSec.Text = "Null Sec Ores"
        Me.chkMineIncludeNullSec.UseVisualStyleBackColor = True
        '
        'chkMineIncludeLowSec
        '
        Me.chkMineIncludeLowSec.AutoSize = True
        Me.chkMineIncludeLowSec.Location = New System.Drawing.Point(9, 35)
        Me.chkMineIncludeLowSec.Name = "chkMineIncludeLowSec"
        Me.chkMineIncludeLowSec.Size = New System.Drawing.Size(93, 17)
        Me.chkMineIncludeLowSec.TabIndex = 1
        Me.chkMineIncludeLowSec.Text = "Low Sec Ores"
        Me.chkMineIncludeLowSec.UseVisualStyleBackColor = True
        '
        'chkMineIncludeHighYieldOre
        '
        Me.chkMineIncludeHighYieldOre.AutoSize = True
        Me.chkMineIncludeHighYieldOre.Location = New System.Drawing.Point(9, 74)
        Me.chkMineIncludeHighYieldOre.Name = "chkMineIncludeHighYieldOre"
        Me.chkMineIncludeHighYieldOre.Size = New System.Drawing.Size(99, 17)
        Me.chkMineIncludeHighYieldOre.TabIndex = 3
        Me.chkMineIncludeHighYieldOre.Text = "High Yield Ores"
        Me.chkMineIncludeHighYieldOre.UseVisualStyleBackColor = True
        '
        'cmbMineOreType
        '
        Me.cmbMineOreType.FormattingEnabled = True
        Me.cmbMineOreType.Items.AddRange(New Object() {"Ore", "Ice", "Gas"})
        Me.cmbMineOreType.Location = New System.Drawing.Point(67, 15)
        Me.cmbMineOreType.Name = "cmbMineOreType"
        Me.cmbMineOreType.Size = New System.Drawing.Size(44, 21)
        Me.cmbMineOreType.TabIndex = 0
        '
        'gbMineOreLocSov
        '
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineEDENCOM)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineMoonMining)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineTriglavian)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineWH)
        Me.gbMineOreLocSov.Controls.Add(Me.gbMineWHSpace)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineCaldari)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineMinmatar)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineGallente)
        Me.gbMineOreLocSov.Controls.Add(Me.chkMineAmarr)
        Me.gbMineOreLocSov.Location = New System.Drawing.Point(114, 9)
        Me.gbMineOreLocSov.Name = "gbMineOreLocSov"
        Me.gbMineOreLocSov.Size = New System.Drawing.Size(243, 129)
        Me.gbMineOreLocSov.TabIndex = 5
        Me.gbMineOreLocSov.TabStop = False
        Me.gbMineOreLocSov.Text = "Ore Location:"
        '
        'chkMineMoonMining
        '
        Me.chkMineMoonMining.AutoSize = True
        Me.chkMineMoonMining.Location = New System.Drawing.Point(11, 75)
        Me.chkMineMoonMining.Name = "chkMineMoonMining"
        Me.chkMineMoonMining.Size = New System.Drawing.Size(87, 17)
        Me.chkMineMoonMining.TabIndex = 6
        Me.chkMineMoonMining.Text = "Moon Mining"
        Me.chkMineMoonMining.UseVisualStyleBackColor = True
        '
        'chkMineTriglavian
        '
        Me.chkMineTriglavian.AutoSize = True
        Me.chkMineTriglavian.Location = New System.Drawing.Point(119, 56)
        Me.chkMineTriglavian.Name = "chkMineTriglavian"
        Me.chkMineTriglavian.Size = New System.Drawing.Size(106, 17)
        Me.chkMineTriglavian.TabIndex = 6
        Me.chkMineTriglavian.Text = "Triglavian Space"
        Me.chkMineTriglavian.UseVisualStyleBackColor = True
        '
        'chkMineWH
        '
        Me.chkMineWH.AutoSize = True
        Me.chkMineWH.Location = New System.Drawing.Point(11, 56)
        Me.chkMineWH.Name = "chkMineWH"
        Me.chkMineWH.Size = New System.Drawing.Size(108, 17)
        Me.chkMineWH.TabIndex = 4
        Me.chkMineWH.Text = "Wormhole Space"
        Me.chkMineWH.UseVisualStyleBackColor = True
        '
        'gbMineWHSpace
        '
        Me.gbMineWHSpace.Controls.Add(Me.chkMineC6)
        Me.gbMineWHSpace.Controls.Add(Me.chkMineC5)
        Me.gbMineWHSpace.Controls.Add(Me.chkMineC4)
        Me.gbMineWHSpace.Controls.Add(Me.chkMineC3)
        Me.gbMineWHSpace.Controls.Add(Me.chkMineC2)
        Me.gbMineWHSpace.Controls.Add(Me.chkMineC1)
        Me.gbMineWHSpace.Location = New System.Drawing.Point(6, 87)
        Me.gbMineWHSpace.Name = "gbMineWHSpace"
        Me.gbMineWHSpace.Size = New System.Drawing.Size(230, 37)
        Me.gbMineWHSpace.TabIndex = 5
        Me.gbMineWHSpace.TabStop = False
        '
        'chkMineC6
        '
        Me.chkMineC6.AutoSize = True
        Me.chkMineC6.Location = New System.Drawing.Point(190, 13)
        Me.chkMineC6.Name = "chkMineC6"
        Me.chkMineC6.Size = New System.Drawing.Size(39, 17)
        Me.chkMineC6.TabIndex = 10
        Me.chkMineC6.Text = "C6"
        Me.chkMineC6.UseVisualStyleBackColor = True
        '
        'chkMineC5
        '
        Me.chkMineC5.AutoSize = True
        Me.chkMineC5.Location = New System.Drawing.Point(153, 13)
        Me.chkMineC5.Name = "chkMineC5"
        Me.chkMineC5.Size = New System.Drawing.Size(39, 17)
        Me.chkMineC5.TabIndex = 9
        Me.chkMineC5.Text = "C5"
        Me.chkMineC5.UseVisualStyleBackColor = True
        '
        'chkMineC4
        '
        Me.chkMineC4.AutoSize = True
        Me.chkMineC4.Location = New System.Drawing.Point(116, 13)
        Me.chkMineC4.Name = "chkMineC4"
        Me.chkMineC4.Size = New System.Drawing.Size(39, 17)
        Me.chkMineC4.TabIndex = 8
        Me.chkMineC4.Text = "C4"
        Me.chkMineC4.UseVisualStyleBackColor = True
        '
        'chkMineC3
        '
        Me.chkMineC3.AutoSize = True
        Me.chkMineC3.Location = New System.Drawing.Point(79, 13)
        Me.chkMineC3.Name = "chkMineC3"
        Me.chkMineC3.Size = New System.Drawing.Size(39, 17)
        Me.chkMineC3.TabIndex = 7
        Me.chkMineC3.Text = "C3"
        Me.chkMineC3.UseVisualStyleBackColor = True
        '
        'chkMineC2
        '
        Me.chkMineC2.AutoSize = True
        Me.chkMineC2.Location = New System.Drawing.Point(42, 13)
        Me.chkMineC2.Name = "chkMineC2"
        Me.chkMineC2.Size = New System.Drawing.Size(39, 17)
        Me.chkMineC2.TabIndex = 6
        Me.chkMineC2.Text = "C2"
        Me.chkMineC2.UseVisualStyleBackColor = True
        '
        'chkMineC1
        '
        Me.chkMineC1.AutoSize = True
        Me.chkMineC1.Location = New System.Drawing.Point(5, 13)
        Me.chkMineC1.Name = "chkMineC1"
        Me.chkMineC1.Size = New System.Drawing.Size(39, 17)
        Me.chkMineC1.TabIndex = 5
        Me.chkMineC1.Text = "C1"
        Me.chkMineC1.UseVisualStyleBackColor = True
        '
        'chkMineCaldari
        '
        Me.chkMineCaldari.AutoSize = True
        Me.chkMineCaldari.Location = New System.Drawing.Point(119, 18)
        Me.chkMineCaldari.Name = "chkMineCaldari"
        Me.chkMineCaldari.Size = New System.Drawing.Size(92, 17)
        Me.chkMineCaldari.TabIndex = 1
        Me.chkMineCaldari.Text = "Caldari Space"
        Me.chkMineCaldari.UseVisualStyleBackColor = True
        '
        'chkMineMinmatar
        '
        Me.chkMineMinmatar.AutoSize = True
        Me.chkMineMinmatar.Location = New System.Drawing.Point(119, 37)
        Me.chkMineMinmatar.Name = "chkMineMinmatar"
        Me.chkMineMinmatar.Size = New System.Drawing.Size(103, 17)
        Me.chkMineMinmatar.TabIndex = 3
        Me.chkMineMinmatar.Text = "Minmatar Space"
        Me.chkMineMinmatar.UseVisualStyleBackColor = True
        '
        'chkMineGallente
        '
        Me.chkMineGallente.AutoSize = True
        Me.chkMineGallente.Location = New System.Drawing.Point(11, 37)
        Me.chkMineGallente.Name = "chkMineGallente"
        Me.chkMineGallente.Size = New System.Drawing.Size(99, 17)
        Me.chkMineGallente.TabIndex = 2
        Me.chkMineGallente.Text = "Gallente Space"
        Me.chkMineGallente.UseVisualStyleBackColor = True
        '
        'chkMineAmarr
        '
        Me.chkMineAmarr.AutoSize = True
        Me.chkMineAmarr.Location = New System.Drawing.Point(11, 18)
        Me.chkMineAmarr.Name = "chkMineAmarr"
        Me.chkMineAmarr.Size = New System.Drawing.Size(87, 17)
        Me.chkMineAmarr.TabIndex = 0
        Me.chkMineAmarr.Text = "Amarr Space"
        Me.chkMineAmarr.UseVisualStyleBackColor = True
        '
        'lblMineType
        '
        Me.lblMineType.AutoSize = True
        Me.lblMineType.Location = New System.Drawing.Point(2, 18)
        Me.lblMineType.Name = "lblMineType"
        Me.lblMineType.Size = New System.Drawing.Size(67, 13)
        Me.lblMineType.TabIndex = 60
        Me.lblMineType.Text = "Select Type:"
        '
        'lstMineGrid
        '
        Me.lstMineGrid.FullRowSelect = True
        Me.lstMineGrid.GridLines = True
        Me.lstMineGrid.HideSelection = False
        Me.lstMineGrid.Location = New System.Drawing.Point(5, 276)
        Me.lstMineGrid.MultiSelect = False
        Me.lstMineGrid.Name = "lstMineGrid"
        Me.lstMineGrid.Size = New System.Drawing.Size(700, 332)
        Me.lstMineGrid.TabIndex = 8
        Me.lstMineGrid.UseCompatibleStateImageBehavior = False
        Me.lstMineGrid.View = System.Windows.Forms.View.Details
        '
        'tabDatacores
        '
        Me.tabDatacores.Controls.Add(Me.lstDC)
        Me.tabDatacores.Controls.Add(Me.gbDCOptions)
        Me.tabDatacores.Location = New System.Drawing.Point(4, 22)
        Me.tabDatacores.Name = "tabDatacores"
        Me.tabDatacores.Size = New System.Drawing.Size(1137, 615)
        Me.tabDatacores.TabIndex = 3
        Me.tabDatacores.Text = "Datacores"
        Me.tabDatacores.UseVisualStyleBackColor = True
        '
        'lstDC
        '
        Me.lstDC.CheckBoxes = True
        Me.lstDC.FullRowSelect = True
        Me.lstDC.GridLines = True
        Me.lstDC.HideSelection = False
        Me.lstDC.Location = New System.Drawing.Point(5, 8)
        Me.lstDC.Name = "lstDC"
        Me.lstDC.Size = New System.Drawing.Size(1124, 282)
        Me.lstDC.TabIndex = 10
        Me.lstDC.UseCompatibleStateImageBehavior = False
        Me.lstDC.View = System.Windows.Forms.View.Details
        '
        'gbDCOptions
        '
        Me.gbDCOptions.Controls.Add(Me.btnDCSaveSettings)
        Me.gbDCOptions.Controls.Add(Me.gbDCAgentLocSov)
        Me.gbDCOptions.Controls.Add(Me.gbDCTotalIPH)
        Me.gbDCOptions.Controls.Add(Me.gbDCPrices)
        Me.gbDCOptions.Controls.Add(Me.gbDCAgentTypes)
        Me.gbDCOptions.Controls.Add(Me.gbDCBaseSkills)
        Me.gbDCOptions.Controls.Add(Me.gbDCDatacores)
        Me.gbDCOptions.Controls.Add(Me.gbDCCorpMinmatar)
        Me.gbDCOptions.Controls.Add(Me.btnDCExporttoClip)
        Me.gbDCOptions.Controls.Add(Me.gbDCCorpAmarr)
        Me.gbDCOptions.Controls.Add(Me.btnDCReset)
        Me.gbDCOptions.Controls.Add(Me.gbDCCorpsCaldari)
        Me.gbDCOptions.Controls.Add(Me.gbDCCorpsGallente)
        Me.gbDCOptions.Controls.Add(Me.btnDCRefresh)
        Me.gbDCOptions.Location = New System.Drawing.Point(5, 286)
        Me.gbDCOptions.Name = "gbDCOptions"
        Me.gbDCOptions.Size = New System.Drawing.Size(1124, 323)
        Me.gbDCOptions.TabIndex = 9
        Me.gbDCOptions.TabStop = False
        '
        'btnDCSaveSettings
        '
        Me.btnDCSaveSettings.Location = New System.Drawing.Point(980, 176)
        Me.btnDCSaveSettings.Name = "btnDCSaveSettings"
        Me.btnDCSaveSettings.Size = New System.Drawing.Size(138, 30)
        Me.btnDCSaveSettings.TabIndex = 71
        Me.btnDCSaveSettings.Text = "Save Settings"
        Me.btnDCSaveSettings.UseVisualStyleBackColor = True
        '
        'gbDCAgentLocSov
        '
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCThukkerSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCKhanidSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCMinmatarSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCSyndicateSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCGallenteSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCAmarrSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCAmmatarSov)
        Me.gbDCAgentLocSov.Controls.Add(Me.chkDCCaldariSov)
        Me.gbDCAgentLocSov.Location = New System.Drawing.Point(746, 241)
        Me.gbDCAgentLocSov.Name = "gbDCAgentLocSov"
        Me.gbDCAgentLocSov.Size = New System.Drawing.Size(372, 76)
        Me.gbDCAgentLocSov.TabIndex = 63
        Me.gbDCAgentLocSov.TabStop = False
        Me.gbDCAgentLocSov.Text = "Agent Location Sovergnity:"
        '
        'chkDCThukkerSov
        '
        Me.chkDCThukkerSov.AutoSize = True
        Me.chkDCThukkerSov.Location = New System.Drawing.Point(272, 36)
        Me.chkDCThukkerSov.Name = "chkDCThukkerSov"
        Me.chkDCThukkerSov.Size = New System.Drawing.Size(93, 17)
        Me.chkDCThukkerSov.TabIndex = 34
        Me.chkDCThukkerSov.Text = "Thukker Tribe"
        Me.chkDCThukkerSov.UseVisualStyleBackColor = True
        '
        'chkDCKhanidSov
        '
        Me.chkDCKhanidSov.AutoSize = True
        Me.chkDCKhanidSov.Location = New System.Drawing.Point(127, 36)
        Me.chkDCKhanidSov.Name = "chkDCKhanidSov"
        Me.chkDCKhanidSov.Size = New System.Drawing.Size(103, 17)
        Me.chkDCKhanidSov.TabIndex = 31
        Me.chkDCKhanidSov.Text = "Khanid Kingdom"
        Me.chkDCKhanidSov.UseVisualStyleBackColor = True
        '
        'chkDCMinmatarSov
        '
        Me.chkDCMinmatarSov.AutoSize = True
        Me.chkDCMinmatarSov.Location = New System.Drawing.Point(127, 53)
        Me.chkDCMinmatarSov.Name = "chkDCMinmatarSov"
        Me.chkDCMinmatarSov.Size = New System.Drawing.Size(114, 17)
        Me.chkDCMinmatarSov.TabIndex = 32
        Me.chkDCMinmatarSov.Text = "Minmatar Republic"
        Me.chkDCMinmatarSov.UseVisualStyleBackColor = True
        '
        'chkDCSyndicateSov
        '
        Me.chkDCSyndicateSov.AutoSize = True
        Me.chkDCSyndicateSov.Location = New System.Drawing.Point(272, 19)
        Me.chkDCSyndicateSov.Name = "chkDCSyndicateSov"
        Me.chkDCSyndicateSov.Size = New System.Drawing.Size(95, 17)
        Me.chkDCSyndicateSov.TabIndex = 33
        Me.chkDCSyndicateSov.Text = "The Syndicate"
        Me.chkDCSyndicateSov.UseVisualStyleBackColor = True
        '
        'chkDCGallenteSov
        '
        Me.chkDCGallenteSov.AutoSize = True
        Me.chkDCGallenteSov.Location = New System.Drawing.Point(127, 19)
        Me.chkDCGallenteSov.Name = "chkDCGallenteSov"
        Me.chkDCGallenteSov.Size = New System.Drawing.Size(118, 17)
        Me.chkDCGallenteSov.TabIndex = 30
        Me.chkDCGallenteSov.Text = "Gallente Federation"
        Me.chkDCGallenteSov.UseVisualStyleBackColor = True
        '
        'chkDCAmarrSov
        '
        Me.chkDCAmarrSov.AutoSize = True
        Me.chkDCAmarrSov.Location = New System.Drawing.Point(12, 19)
        Me.chkDCAmarrSov.Name = "chkDCAmarrSov"
        Me.chkDCAmarrSov.Size = New System.Drawing.Size(88, 17)
        Me.chkDCAmarrSov.TabIndex = 27
        Me.chkDCAmarrSov.Text = "Amarr Empire"
        Me.chkDCAmarrSov.UseVisualStyleBackColor = True
        '
        'chkDCAmmatarSov
        '
        Me.chkDCAmmatarSov.AutoSize = True
        Me.chkDCAmmatarSov.Location = New System.Drawing.Point(12, 36)
        Me.chkDCAmmatarSov.Name = "chkDCAmmatarSov"
        Me.chkDCAmmatarSov.Size = New System.Drawing.Size(112, 17)
        Me.chkDCAmmatarSov.TabIndex = 28
        Me.chkDCAmmatarSov.Text = "Ammatar Mandate"
        Me.chkDCAmmatarSov.UseVisualStyleBackColor = True
        '
        'chkDCCaldariSov
        '
        Me.chkDCCaldariSov.AutoSize = True
        Me.chkDCCaldariSov.Location = New System.Drawing.Point(12, 53)
        Me.chkDCCaldariSov.Name = "chkDCCaldariSov"
        Me.chkDCCaldariSov.Size = New System.Drawing.Size(86, 17)
        Me.chkDCCaldariSov.TabIndex = 29
        Me.chkDCCaldariSov.Text = "Caldari State"
        Me.chkDCCaldariSov.UseVisualStyleBackColor = True
        '
        'gbDCTotalIPH
        '
        Me.gbDCTotalIPH.Controls.Add(Me.lblDCTotalOptIPH)
        Me.gbDCTotalIPH.Controls.Add(Me.lblDCTotalSelectedIPH)
        Me.gbDCTotalIPH.Controls.Add(Me.txtDCTotalSelectedIPH)
        Me.gbDCTotalIPH.Controls.Add(Me.txtDCTotalOptIPH)
        Me.gbDCTotalIPH.Location = New System.Drawing.Point(978, 11)
        Me.gbDCTotalIPH.Name = "gbDCTotalIPH"
        Me.gbDCTotalIPH.Size = New System.Drawing.Size(140, 101)
        Me.gbDCTotalIPH.TabIndex = 70
        Me.gbDCTotalIPH.TabStop = False
        Me.gbDCTotalIPH.Text = "Total Isk per Hour:"
        '
        'lblDCTotalOptIPH
        '
        Me.lblDCTotalOptIPH.Location = New System.Drawing.Point(5, 56)
        Me.lblDCTotalOptIPH.Name = "lblDCTotalOptIPH"
        Me.lblDCTotalOptIPH.Size = New System.Drawing.Size(81, 13)
        Me.lblDCTotalOptIPH.TabIndex = 47
        Me.lblDCTotalOptIPH.Text = "Total Optimal:"
        '
        'lblDCTotalSelectedIPH
        '
        Me.lblDCTotalSelectedIPH.Location = New System.Drawing.Point(5, 19)
        Me.lblDCTotalSelectedIPH.Name = "lblDCTotalSelectedIPH"
        Me.lblDCTotalSelectedIPH.Size = New System.Drawing.Size(81, 13)
        Me.lblDCTotalSelectedIPH.TabIndex = 46
        Me.lblDCTotalSelectedIPH.Text = "Total Selected:"
        '
        'txtDCTotalSelectedIPH
        '
        Me.txtDCTotalSelectedIPH.Location = New System.Drawing.Point(5, 33)
        Me.txtDCTotalSelectedIPH.Name = "txtDCTotalSelectedIPH"
        Me.txtDCTotalSelectedIPH.Size = New System.Drawing.Size(129, 20)
        Me.txtDCTotalSelectedIPH.TabIndex = 41
        Me.txtDCTotalSelectedIPH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDCTotalOptIPH
        '
        Me.txtDCTotalOptIPH.Location = New System.Drawing.Point(5, 71)
        Me.txtDCTotalOptIPH.Name = "txtDCTotalOptIPH"
        Me.txtDCTotalOptIPH.Size = New System.Drawing.Size(129, 20)
        Me.txtDCTotalOptIPH.TabIndex = 40
        Me.txtDCTotalOptIPH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'gbDCPrices
        '
        Me.gbDCPrices.Controls.Add(Me.rbtnDCSystemPrices)
        Me.gbDCPrices.Controls.Add(Me.rbtnDCRegionPrices)
        Me.gbDCPrices.Controls.Add(Me.rbtnDCUpdatedPrices)
        Me.gbDCPrices.Location = New System.Drawing.Point(6, 277)
        Me.gbDCPrices.Name = "gbDCPrices"
        Me.gbDCPrices.Size = New System.Drawing.Size(506, 40)
        Me.gbDCPrices.TabIndex = 7
        Me.gbDCPrices.TabStop = False
        Me.gbDCPrices.Text = "Use Prices From:"
        '
        'rbtnDCSystemPrices
        '
        Me.rbtnDCSystemPrices.AutoSize = True
        Me.rbtnDCSystemPrices.Location = New System.Drawing.Point(313, 16)
        Me.rbtnDCSystemPrices.Name = "rbtnDCSystemPrices"
        Me.rbtnDCSystemPrices.Size = New System.Drawing.Size(90, 17)
        Me.rbtnDCSystemPrices.TabIndex = 2
        Me.rbtnDCSystemPrices.TabStop = True
        Me.rbtnDCSystemPrices.Text = "Agent System"
        Me.rbtnDCSystemPrices.UseVisualStyleBackColor = True
        '
        'rbtnDCRegionPrices
        '
        Me.rbtnDCRegionPrices.AutoSize = True
        Me.rbtnDCRegionPrices.Location = New System.Drawing.Point(164, 16)
        Me.rbtnDCRegionPrices.Name = "rbtnDCRegionPrices"
        Me.rbtnDCRegionPrices.Size = New System.Drawing.Size(90, 17)
        Me.rbtnDCRegionPrices.TabIndex = 1
        Me.rbtnDCRegionPrices.TabStop = True
        Me.rbtnDCRegionPrices.Text = "Agent Region"
        Me.rbtnDCRegionPrices.UseVisualStyleBackColor = True
        '
        'rbtnDCUpdatedPrices
        '
        Me.rbtnDCUpdatedPrices.AutoSize = True
        Me.rbtnDCUpdatedPrices.Location = New System.Drawing.Point(7, 16)
        Me.rbtnDCUpdatedPrices.Name = "rbtnDCUpdatedPrices"
        Me.rbtnDCUpdatedPrices.Size = New System.Drawing.Size(98, 17)
        Me.rbtnDCUpdatedPrices.TabIndex = 0
        Me.rbtnDCUpdatedPrices.TabStop = True
        Me.rbtnDCUpdatedPrices.Text = "Updated Prices"
        Me.rbtnDCUpdatedPrices.UseVisualStyleBackColor = True
        '
        'gbDCAgentTypes
        '
        Me.gbDCAgentTypes.Controls.Add(Me.cmbDCRegions)
        Me.gbDCAgentTypes.Controls.Add(Me.lblDCRegion)
        Me.gbDCAgentTypes.Controls.Add(Me.chkDCLowSecAgents)
        Me.gbDCAgentTypes.Controls.Add(Me.chkDCHighSecAgents)
        Me.gbDCAgentTypes.Controls.Add(Me.chkDCIncludeAllAgents)
        Me.gbDCAgentTypes.Location = New System.Drawing.Point(518, 214)
        Me.gbDCAgentTypes.Name = "gbDCAgentTypes"
        Me.gbDCAgentTypes.Size = New System.Drawing.Size(222, 104)
        Me.gbDCAgentTypes.TabIndex = 69
        Me.gbDCAgentTypes.TabStop = False
        Me.gbDCAgentTypes.Text = "Agents:"
        '
        'cmbDCRegions
        '
        Me.cmbDCRegions.FormattingEnabled = True
        Me.cmbDCRegions.Location = New System.Drawing.Point(65, 70)
        Me.cmbDCRegions.Name = "cmbDCRegions"
        Me.cmbDCRegions.Size = New System.Drawing.Size(144, 21)
        Me.cmbDCRegions.TabIndex = 6
        Me.cmbDCRegions.Text = "All Regions"
        '
        'lblDCRegion
        '
        Me.lblDCRegion.Location = New System.Drawing.Point(9, 73)
        Me.lblDCRegion.Name = "lblDCRegion"
        Me.lblDCRegion.Size = New System.Drawing.Size(59, 13)
        Me.lblDCRegion.TabIndex = 7
        Me.lblDCRegion.Text = "In Region:"
        '
        'chkDCLowSecAgents
        '
        Me.chkDCLowSecAgents.AutoSize = True
        Me.chkDCLowSecAgents.Location = New System.Drawing.Point(97, 21)
        Me.chkDCLowSecAgents.Name = "chkDCLowSecAgents"
        Me.chkDCLowSecAgents.Size = New System.Drawing.Size(91, 17)
        Me.chkDCLowSecAgents.TabIndex = 1
        Me.chkDCLowSecAgents.Text = "Low/Null Sec"
        Me.chkDCLowSecAgents.UseVisualStyleBackColor = True
        '
        'chkDCHighSecAgents
        '
        Me.chkDCHighSecAgents.AutoSize = True
        Me.chkDCHighSecAgents.Location = New System.Drawing.Point(12, 21)
        Me.chkDCHighSecAgents.Name = "chkDCHighSecAgents"
        Me.chkDCHighSecAgents.Size = New System.Drawing.Size(70, 17)
        Me.chkDCHighSecAgents.TabIndex = 0
        Me.chkDCHighSecAgents.Text = "High Sec"
        Me.chkDCHighSecAgents.UseVisualStyleBackColor = True
        '
        'chkDCIncludeAllAgents
        '
        Me.chkDCIncludeAllAgents.AutoSize = True
        Me.chkDCIncludeAllAgents.Location = New System.Drawing.Point(12, 44)
        Me.chkDCIncludeAllAgents.Name = "chkDCIncludeAllAgents"
        Me.chkDCIncludeAllAgents.Size = New System.Drawing.Size(178, 17)
        Me.chkDCIncludeAllAgents.TabIndex = 0
        Me.chkDCIncludeAllAgents.Text = "Include Agents I Cannot Access"
        Me.chkDCIncludeAllAgents.UseVisualStyleBackColor = True
        '
        'gbDCBaseSkills
        '
        Me.gbDCBaseSkills.Controls.Add(Me.cmbDCResearchMgmt)
        Me.gbDCBaseSkills.Controls.Add(Me.lblDCResearchManagement)
        Me.gbDCBaseSkills.Controls.Add(Me.lblDCNegotiation)
        Me.gbDCBaseSkills.Controls.Add(Me.cmbDCConnections)
        Me.gbDCBaseSkills.Controls.Add(Me.lblDCConnections)
        Me.gbDCBaseSkills.Controls.Add(Me.cmbDCNegotiation)
        Me.gbDCBaseSkills.Location = New System.Drawing.Point(6, 236)
        Me.gbDCBaseSkills.Name = "gbDCBaseSkills"
        Me.gbDCBaseSkills.Size = New System.Drawing.Size(506, 40)
        Me.gbDCBaseSkills.TabIndex = 9
        Me.gbDCBaseSkills.TabStop = False
        Me.gbDCBaseSkills.Text = "Base Skills:"
        '
        'cmbDCResearchMgmt
        '
        Me.cmbDCResearchMgmt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCResearchMgmt.FormattingEnabled = True
        Me.cmbDCResearchMgmt.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCResearchMgmt.Location = New System.Drawing.Point(396, 13)
        Me.cmbDCResearchMgmt.Name = "cmbDCResearchMgmt"
        Me.cmbDCResearchMgmt.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCResearchMgmt.TabIndex = 60
        '
        'lblDCResearchManagement
        '
        Me.lblDCResearchManagement.AutoSize = True
        Me.lblDCResearchManagement.Location = New System.Drawing.Point(269, 17)
        Me.lblDCResearchManagement.Name = "lblDCResearchManagement"
        Me.lblDCResearchManagement.Size = New System.Drawing.Size(121, 13)
        Me.lblDCResearchManagement.TabIndex = 59
        Me.lblDCResearchManagement.Text = "Research Project Mgmt:"
        '
        'lblDCNegotiation
        '
        Me.lblDCNegotiation.AutoSize = True
        Me.lblDCNegotiation.Location = New System.Drawing.Point(6, 17)
        Me.lblDCNegotiation.Name = "lblDCNegotiation"
        Me.lblDCNegotiation.Size = New System.Drawing.Size(64, 13)
        Me.lblDCNegotiation.TabIndex = 58
        Me.lblDCNegotiation.Text = "Negotiation:"
        '
        'cmbDCConnections
        '
        Me.cmbDCConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCConnections.FormattingEnabled = True
        Me.cmbDCConnections.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCConnections.Location = New System.Drawing.Point(218, 13)
        Me.cmbDCConnections.Name = "cmbDCConnections"
        Me.cmbDCConnections.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCConnections.TabIndex = 55
        '
        'lblDCConnections
        '
        Me.lblDCConnections.AutoSize = True
        Me.lblDCConnections.Location = New System.Drawing.Point(143, 17)
        Me.lblDCConnections.Name = "lblDCConnections"
        Me.lblDCConnections.Size = New System.Drawing.Size(69, 13)
        Me.lblDCConnections.TabIndex = 57
        Me.lblDCConnections.Text = "Connections:"
        '
        'cmbDCNegotiation
        '
        Me.cmbDCNegotiation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCNegotiation.FormattingEnabled = True
        Me.cmbDCNegotiation.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCNegotiation.Location = New System.Drawing.Point(76, 13)
        Me.cmbDCNegotiation.Name = "cmbDCNegotiation"
        Me.cmbDCNegotiation.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCNegotiation.TabIndex = 56
        '
        'gbDCDatacores
        '
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel17)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel16)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel15)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel14)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel13)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel12)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel11)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel10)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel9)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel8)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel7)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel6)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel5)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel4)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel3)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel2)
        Me.gbDCDatacores.Controls.Add(Me.cmbDCSkillLevel1)
        Me.gbDCDatacores.Controls.Add(Me.chkDC17)
        Me.gbDCDatacores.Controls.Add(Me.chkDC4)
        Me.gbDCDatacores.Controls.Add(Me.chkDC16)
        Me.gbDCDatacores.Controls.Add(Me.chkDC3)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore17)
        Me.gbDCDatacores.Controls.Add(Me.chkDC15)
        Me.gbDCDatacores.Controls.Add(Me.chkDC2)
        Me.gbDCDatacores.Controls.Add(Me.chkDC14)
        Me.gbDCDatacores.Controls.Add(Me.chkDC1)
        Me.gbDCDatacores.Controls.Add(Me.chkDC13)
        Me.gbDCDatacores.Controls.Add(Me.chkDC12)
        Me.gbDCDatacores.Controls.Add(Me.chkDC11)
        Me.gbDCDatacores.Controls.Add(Me.chkDC10)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore16)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore4)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore15)
        Me.gbDCDatacores.Controls.Add(Me.chkDC9)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore14)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore3)
        Me.gbDCDatacores.Controls.Add(Me.chkDC8)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore13)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore2)
        Me.gbDCDatacores.Controls.Add(Me.chkDC7)
        Me.gbDCDatacores.Controls.Add(Me.chkDC6)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore1)
        Me.gbDCDatacores.Controls.Add(Me.chkDC5)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore5)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore6)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore7)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore8)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore12)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore11)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore10)
        Me.gbDCDatacores.Controls.Add(Me.lblDatacore9)
        Me.gbDCDatacores.Controls.Add(Me.gbDCCodes)
        Me.gbDCDatacores.Location = New System.Drawing.Point(6, 11)
        Me.gbDCDatacores.Name = "gbDCDatacores"
        Me.gbDCDatacores.Size = New System.Drawing.Size(506, 224)
        Me.gbDCDatacores.TabIndex = 3
        Me.gbDCDatacores.TabStop = False
        Me.gbDCDatacores.Text = "Datacore Skills:"
        '
        'cmbDCSkillLevel17
        '
        Me.cmbDCSkillLevel17.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel17.FormattingEnabled = True
        Me.cmbDCSkillLevel17.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel17.Location = New System.Drawing.Point(465, 167)
        Me.cmbDCSkillLevel17.Name = "cmbDCSkillLevel17"
        Me.cmbDCSkillLevel17.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel17.TabIndex = 55
        '
        'cmbDCSkillLevel16
        '
        Me.cmbDCSkillLevel16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel16.FormattingEnabled = True
        Me.cmbDCSkillLevel16.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel16.Location = New System.Drawing.Point(465, 145)
        Me.cmbDCSkillLevel16.Name = "cmbDCSkillLevel16"
        Me.cmbDCSkillLevel16.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel16.TabIndex = 54
        '
        'cmbDCSkillLevel15
        '
        Me.cmbDCSkillLevel15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel15.FormattingEnabled = True
        Me.cmbDCSkillLevel15.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel15.Location = New System.Drawing.Point(465, 122)
        Me.cmbDCSkillLevel15.Name = "cmbDCSkillLevel15"
        Me.cmbDCSkillLevel15.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel15.TabIndex = 53
        '
        'cmbDCSkillLevel14
        '
        Me.cmbDCSkillLevel14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel14.FormattingEnabled = True
        Me.cmbDCSkillLevel14.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel14.Location = New System.Drawing.Point(465, 100)
        Me.cmbDCSkillLevel14.Name = "cmbDCSkillLevel14"
        Me.cmbDCSkillLevel14.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel14.TabIndex = 52
        '
        'cmbDCSkillLevel13
        '
        Me.cmbDCSkillLevel13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel13.FormattingEnabled = True
        Me.cmbDCSkillLevel13.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel13.Location = New System.Drawing.Point(465, 78)
        Me.cmbDCSkillLevel13.Name = "cmbDCSkillLevel13"
        Me.cmbDCSkillLevel13.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel13.TabIndex = 51
        '
        'cmbDCSkillLevel12
        '
        Me.cmbDCSkillLevel12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel12.FormattingEnabled = True
        Me.cmbDCSkillLevel12.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel12.Location = New System.Drawing.Point(465, 55)
        Me.cmbDCSkillLevel12.Name = "cmbDCSkillLevel12"
        Me.cmbDCSkillLevel12.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel12.TabIndex = 50
        '
        'cmbDCSkillLevel11
        '
        Me.cmbDCSkillLevel11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel11.FormattingEnabled = True
        Me.cmbDCSkillLevel11.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel11.Location = New System.Drawing.Point(465, 33)
        Me.cmbDCSkillLevel11.Name = "cmbDCSkillLevel11"
        Me.cmbDCSkillLevel11.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel11.TabIndex = 49
        '
        'cmbDCSkillLevel10
        '
        Me.cmbDCSkillLevel10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel10.FormattingEnabled = True
        Me.cmbDCSkillLevel10.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel10.Location = New System.Drawing.Point(218, 191)
        Me.cmbDCSkillLevel10.Name = "cmbDCSkillLevel10"
        Me.cmbDCSkillLevel10.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel10.TabIndex = 48
        '
        'cmbDCSkillLevel9
        '
        Me.cmbDCSkillLevel9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel9.FormattingEnabled = True
        Me.cmbDCSkillLevel9.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel9.Location = New System.Drawing.Point(465, 11)
        Me.cmbDCSkillLevel9.Name = "cmbDCSkillLevel9"
        Me.cmbDCSkillLevel9.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel9.TabIndex = 47
        '
        'cmbDCSkillLevel8
        '
        Me.cmbDCSkillLevel8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel8.FormattingEnabled = True
        Me.cmbDCSkillLevel8.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel8.Location = New System.Drawing.Point(218, 169)
        Me.cmbDCSkillLevel8.Name = "cmbDCSkillLevel8"
        Me.cmbDCSkillLevel8.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel8.TabIndex = 46
        '
        'cmbDCSkillLevel7
        '
        Me.cmbDCSkillLevel7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel7.FormattingEnabled = True
        Me.cmbDCSkillLevel7.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel7.Location = New System.Drawing.Point(218, 147)
        Me.cmbDCSkillLevel7.Name = "cmbDCSkillLevel7"
        Me.cmbDCSkillLevel7.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel7.TabIndex = 45
        '
        'cmbDCSkillLevel6
        '
        Me.cmbDCSkillLevel6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel6.FormattingEnabled = True
        Me.cmbDCSkillLevel6.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel6.Location = New System.Drawing.Point(218, 124)
        Me.cmbDCSkillLevel6.Name = "cmbDCSkillLevel6"
        Me.cmbDCSkillLevel6.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel6.TabIndex = 44
        '
        'cmbDCSkillLevel5
        '
        Me.cmbDCSkillLevel5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel5.FormattingEnabled = True
        Me.cmbDCSkillLevel5.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel5.Location = New System.Drawing.Point(218, 102)
        Me.cmbDCSkillLevel5.Name = "cmbDCSkillLevel5"
        Me.cmbDCSkillLevel5.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel5.TabIndex = 43
        '
        'cmbDCSkillLevel4
        '
        Me.cmbDCSkillLevel4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel4.FormattingEnabled = True
        Me.cmbDCSkillLevel4.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel4.Location = New System.Drawing.Point(218, 80)
        Me.cmbDCSkillLevel4.Name = "cmbDCSkillLevel4"
        Me.cmbDCSkillLevel4.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel4.TabIndex = 42
        '
        'cmbDCSkillLevel3
        '
        Me.cmbDCSkillLevel3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel3.FormattingEnabled = True
        Me.cmbDCSkillLevel3.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel3.Location = New System.Drawing.Point(218, 57)
        Me.cmbDCSkillLevel3.Name = "cmbDCSkillLevel3"
        Me.cmbDCSkillLevel3.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel3.TabIndex = 41
        '
        'cmbDCSkillLevel2
        '
        Me.cmbDCSkillLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel2.FormattingEnabled = True
        Me.cmbDCSkillLevel2.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel2.Location = New System.Drawing.Point(218, 35)
        Me.cmbDCSkillLevel2.Name = "cmbDCSkillLevel2"
        Me.cmbDCSkillLevel2.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel2.TabIndex = 40
        '
        'cmbDCSkillLevel1
        '
        Me.cmbDCSkillLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDCSkillLevel1.FormattingEnabled = True
        Me.cmbDCSkillLevel1.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbDCSkillLevel1.Location = New System.Drawing.Point(218, 13)
        Me.cmbDCSkillLevel1.Name = "cmbDCSkillLevel1"
        Me.cmbDCSkillLevel1.Size = New System.Drawing.Size(36, 21)
        Me.cmbDCSkillLevel1.TabIndex = 39
        '
        'chkDC17
        '
        Me.chkDC17.AutoSize = True
        Me.chkDC17.Location = New System.Drawing.Point(277, 172)
        Me.chkDC17.Name = "chkDC17"
        Me.chkDC17.Size = New System.Drawing.Size(15, 14)
        Me.chkDC17.TabIndex = 38
        Me.chkDC17.UseVisualStyleBackColor = True
        '
        'chkDC4
        '
        Me.chkDC4.AutoSize = True
        Me.chkDC4.Location = New System.Drawing.Point(7, 83)
        Me.chkDC4.Name = "chkDC4"
        Me.chkDC4.Size = New System.Drawing.Size(15, 14)
        Me.chkDC4.TabIndex = 30
        Me.chkDC4.UseVisualStyleBackColor = True
        '
        'chkDC16
        '
        Me.chkDC16.AutoSize = True
        Me.chkDC16.Location = New System.Drawing.Point(277, 150)
        Me.chkDC16.Name = "chkDC16"
        Me.chkDC16.Size = New System.Drawing.Size(15, 14)
        Me.chkDC16.TabIndex = 37
        Me.chkDC16.UseVisualStyleBackColor = True
        '
        'chkDC3
        '
        Me.chkDC3.AutoSize = True
        Me.chkDC3.Location = New System.Drawing.Point(7, 60)
        Me.chkDC3.Name = "chkDC3"
        Me.chkDC3.Size = New System.Drawing.Size(15, 14)
        Me.chkDC3.TabIndex = 29
        Me.chkDC3.UseVisualStyleBackColor = True
        '
        'lblDatacore17
        '
        Me.lblDatacore17.Location = New System.Drawing.Point(298, 173)
        Me.lblDatacore17.Name = "lblDatacore17"
        Me.lblDatacore17.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore17.TabIndex = 24
        Me.lblDatacore17.Text = "Rocket Science"
        '
        'chkDC15
        '
        Me.chkDC15.AutoSize = True
        Me.chkDC15.Location = New System.Drawing.Point(277, 127)
        Me.chkDC15.Name = "chkDC15"
        Me.chkDC15.Size = New System.Drawing.Size(15, 14)
        Me.chkDC15.TabIndex = 36
        Me.chkDC15.UseVisualStyleBackColor = True
        '
        'chkDC2
        '
        Me.chkDC2.AutoSize = True
        Me.chkDC2.Location = New System.Drawing.Point(7, 38)
        Me.chkDC2.Name = "chkDC2"
        Me.chkDC2.Size = New System.Drawing.Size(15, 14)
        Me.chkDC2.TabIndex = 28
        Me.chkDC2.UseVisualStyleBackColor = True
        '
        'chkDC14
        '
        Me.chkDC14.AutoSize = True
        Me.chkDC14.Location = New System.Drawing.Point(277, 105)
        Me.chkDC14.Name = "chkDC14"
        Me.chkDC14.Size = New System.Drawing.Size(15, 14)
        Me.chkDC14.TabIndex = 35
        Me.chkDC14.UseVisualStyleBackColor = True
        '
        'chkDC1
        '
        Me.chkDC1.AutoSize = True
        Me.chkDC1.Location = New System.Drawing.Point(7, 16)
        Me.chkDC1.Name = "chkDC1"
        Me.chkDC1.Size = New System.Drawing.Size(15, 14)
        Me.chkDC1.TabIndex = 27
        Me.chkDC1.UseVisualStyleBackColor = True
        '
        'chkDC13
        '
        Me.chkDC13.AutoSize = True
        Me.chkDC13.Location = New System.Drawing.Point(277, 83)
        Me.chkDC13.Name = "chkDC13"
        Me.chkDC13.Size = New System.Drawing.Size(15, 14)
        Me.chkDC13.TabIndex = 34
        Me.chkDC13.UseVisualStyleBackColor = True
        '
        'chkDC12
        '
        Me.chkDC12.AutoSize = True
        Me.chkDC12.Location = New System.Drawing.Point(277, 60)
        Me.chkDC12.Name = "chkDC12"
        Me.chkDC12.Size = New System.Drawing.Size(15, 14)
        Me.chkDC12.TabIndex = 33
        Me.chkDC12.UseVisualStyleBackColor = True
        '
        'chkDC11
        '
        Me.chkDC11.AutoSize = True
        Me.chkDC11.Location = New System.Drawing.Point(277, 38)
        Me.chkDC11.Name = "chkDC11"
        Me.chkDC11.Size = New System.Drawing.Size(15, 14)
        Me.chkDC11.TabIndex = 32
        Me.chkDC11.UseVisualStyleBackColor = True
        '
        'chkDC10
        '
        Me.chkDC10.AutoSize = True
        Me.chkDC10.Location = New System.Drawing.Point(7, 194)
        Me.chkDC10.Name = "chkDC10"
        Me.chkDC10.Size = New System.Drawing.Size(15, 14)
        Me.chkDC10.TabIndex = 31
        Me.chkDC10.UseVisualStyleBackColor = True
        '
        'lblDatacore16
        '
        Me.lblDatacore16.Location = New System.Drawing.Point(298, 151)
        Me.lblDatacore16.Name = "lblDatacore16"
        Me.lblDatacore16.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore16.TabIndex = 20
        Me.lblDatacore16.Text = "Quantum Physics"
        '
        'lblDatacore4
        '
        Me.lblDatacore4.Location = New System.Drawing.Point(28, 84)
        Me.lblDatacore4.Name = "lblDatacore4"
        Me.lblDatacore4.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore4.TabIndex = 4
        Me.lblDatacore4.Text = "Minmatar Starship Engineering"
        '
        'lblDatacore15
        '
        Me.lblDatacore15.Location = New System.Drawing.Point(298, 128)
        Me.lblDatacore15.Name = "lblDatacore15"
        Me.lblDatacore15.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore15.TabIndex = 19
        Me.lblDatacore15.Text = "Plasma Physics"
        '
        'chkDC9
        '
        Me.chkDC9.AutoSize = True
        Me.chkDC9.Location = New System.Drawing.Point(277, 16)
        Me.chkDC9.Name = "chkDC9"
        Me.chkDC9.Size = New System.Drawing.Size(15, 14)
        Me.chkDC9.TabIndex = 30
        Me.chkDC9.UseVisualStyleBackColor = True
        '
        'lblDatacore14
        '
        Me.lblDatacore14.Location = New System.Drawing.Point(298, 106)
        Me.lblDatacore14.Name = "lblDatacore14"
        Me.lblDatacore14.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore14.TabIndex = 18
        Me.lblDatacore14.Text = "Nuclear Physics"
        '
        'lblDatacore3
        '
        Me.lblDatacore3.Location = New System.Drawing.Point(28, 61)
        Me.lblDatacore3.Name = "lblDatacore3"
        Me.lblDatacore3.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore3.TabIndex = 3
        Me.lblDatacore3.Text = "Gallente Starship Engineering"
        '
        'chkDC8
        '
        Me.chkDC8.AutoSize = True
        Me.chkDC8.Location = New System.Drawing.Point(7, 172)
        Me.chkDC8.Name = "chkDC8"
        Me.chkDC8.Size = New System.Drawing.Size(15, 14)
        Me.chkDC8.TabIndex = 29
        Me.chkDC8.UseVisualStyleBackColor = True
        '
        'lblDatacore13
        '
        Me.lblDatacore13.Location = New System.Drawing.Point(298, 84)
        Me.lblDatacore13.Name = "lblDatacore13"
        Me.lblDatacore13.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore13.TabIndex = 17
        Me.lblDatacore13.Text = "Nanite Engineering"
        '
        'lblDatacore2
        '
        Me.lblDatacore2.Location = New System.Drawing.Point(28, 39)
        Me.lblDatacore2.Name = "lblDatacore2"
        Me.lblDatacore2.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore2.TabIndex = 2
        Me.lblDatacore2.Text = "Caldari Starship Engineering"
        '
        'chkDC7
        '
        Me.chkDC7.AutoSize = True
        Me.chkDC7.Location = New System.Drawing.Point(7, 150)
        Me.chkDC7.Name = "chkDC7"
        Me.chkDC7.Size = New System.Drawing.Size(15, 14)
        Me.chkDC7.TabIndex = 28
        Me.chkDC7.UseVisualStyleBackColor = True
        '
        'chkDC6
        '
        Me.chkDC6.AutoSize = True
        Me.chkDC6.Location = New System.Drawing.Point(7, 127)
        Me.chkDC6.Name = "chkDC6"
        Me.chkDC6.Size = New System.Drawing.Size(15, 14)
        Me.chkDC6.TabIndex = 27
        Me.chkDC6.UseVisualStyleBackColor = True
        '
        'lblDatacore1
        '
        Me.lblDatacore1.Location = New System.Drawing.Point(28, 17)
        Me.lblDatacore1.Name = "lblDatacore1"
        Me.lblDatacore1.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore1.TabIndex = 1
        Me.lblDatacore1.Text = "Amarr Starship Engineering"
        '
        'chkDC5
        '
        Me.chkDC5.AutoSize = True
        Me.chkDC5.Location = New System.Drawing.Point(7, 105)
        Me.chkDC5.Name = "chkDC5"
        Me.chkDC5.Size = New System.Drawing.Size(15, 14)
        Me.chkDC5.TabIndex = 26
        Me.chkDC5.UseVisualStyleBackColor = True
        '
        'lblDatacore5
        '
        Me.lblDatacore5.Location = New System.Drawing.Point(28, 106)
        Me.lblDatacore5.Name = "lblDatacore5"
        Me.lblDatacore5.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore5.TabIndex = 1
        Me.lblDatacore5.Text = "Electromagnetic Physics"
        '
        'lblDatacore6
        '
        Me.lblDatacore6.Location = New System.Drawing.Point(28, 128)
        Me.lblDatacore6.Name = "lblDatacore6"
        Me.lblDatacore6.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore6.TabIndex = 2
        Me.lblDatacore6.Text = "Electronic Engineering"
        '
        'lblDatacore7
        '
        Me.lblDatacore7.Location = New System.Drawing.Point(28, 151)
        Me.lblDatacore7.Name = "lblDatacore7"
        Me.lblDatacore7.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore7.TabIndex = 3
        Me.lblDatacore7.Text = "Graviton Physics"
        '
        'lblDatacore8
        '
        Me.lblDatacore8.Location = New System.Drawing.Point(28, 173)
        Me.lblDatacore8.Name = "lblDatacore8"
        Me.lblDatacore8.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore8.TabIndex = 4
        Me.lblDatacore8.Text = "High Energy Physics"
        '
        'lblDatacore12
        '
        Me.lblDatacore12.Location = New System.Drawing.Point(298, 61)
        Me.lblDatacore12.Name = "lblDatacore12"
        Me.lblDatacore12.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore12.TabIndex = 12
        Me.lblDatacore12.Text = "Molecular Engineering"
        '
        'lblDatacore11
        '
        Me.lblDatacore11.Location = New System.Drawing.Point(298, 39)
        Me.lblDatacore11.Name = "lblDatacore11"
        Me.lblDatacore11.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore11.TabIndex = 11
        Me.lblDatacore11.Text = "Mechanical Engineering"
        '
        'lblDatacore10
        '
        Me.lblDatacore10.Location = New System.Drawing.Point(28, 195)
        Me.lblDatacore10.Name = "lblDatacore10"
        Me.lblDatacore10.Size = New System.Drawing.Size(158, 13)
        Me.lblDatacore10.TabIndex = 10
        Me.lblDatacore10.Text = "Laser Physics"
        '
        'lblDatacore9
        '
        Me.lblDatacore9.Location = New System.Drawing.Point(298, 17)
        Me.lblDatacore9.Name = "lblDatacore9"
        Me.lblDatacore9.Size = New System.Drawing.Size(122, 13)
        Me.lblDatacore9.TabIndex = 9
        Me.lblDatacore9.Text = "Hydromagnetic Physics"
        '
        'gbDCCodes
        '
        Me.gbDCCodes.Controls.Add(Me.lblDCColors)
        Me.gbDCCodes.Controls.Add(Me.lblDCRedText)
        Me.gbDCCodes.Controls.Add(Me.lblDCOrangeText)
        Me.gbDCCodes.Controls.Add(Me.lblDCGrayText)
        Me.gbDCCodes.Controls.Add(Me.lblDCBlueText)
        Me.gbDCCodes.Controls.Add(Me.lblDCGreenBackColor)
        Me.gbDCCodes.Location = New System.Drawing.Point(277, 187)
        Me.gbDCCodes.Name = "gbDCCodes"
        Me.gbDCCodes.Size = New System.Drawing.Size(224, 30)
        Me.gbDCCodes.TabIndex = 56
        Me.gbDCCodes.TabStop = False
        '
        'lblDCColors
        '
        Me.lblDCColors.AutoSize = True
        Me.lblDCColors.Location = New System.Drawing.Point(8, 11)
        Me.lblDCColors.Name = "lblDCColors"
        Me.lblDCColors.Size = New System.Drawing.Size(63, 13)
        Me.lblDCColors.TabIndex = 5
        Me.lblDCColors.Text = "Text Colors:"
        Me.lblDCColors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDCRedText
        '
        Me.lblDCRedText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDCRedText.ForeColor = System.Drawing.Color.Red
        Me.lblDCRedText.Location = New System.Drawing.Point(200, 10)
        Me.lblDCRedText.Name = "lblDCRedText"
        Me.lblDCRedText.Size = New System.Drawing.Size(15, 15)
        Me.lblDCRedText.TabIndex = 4
        Me.lblDCRedText.Text = "T"
        Me.lblDCRedText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDCOrangeText
        '
        Me.lblDCOrangeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDCOrangeText.ForeColor = System.Drawing.Color.Orange
        Me.lblDCOrangeText.Location = New System.Drawing.Point(170, 10)
        Me.lblDCOrangeText.Name = "lblDCOrangeText"
        Me.lblDCOrangeText.Size = New System.Drawing.Size(15, 15)
        Me.lblDCOrangeText.TabIndex = 3
        Me.lblDCOrangeText.Text = "T"
        Me.lblDCOrangeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDCGrayText
        '
        Me.lblDCGrayText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDCGrayText.ForeColor = System.Drawing.Color.Gray
        Me.lblDCGrayText.Location = New System.Drawing.Point(140, 10)
        Me.lblDCGrayText.Name = "lblDCGrayText"
        Me.lblDCGrayText.Size = New System.Drawing.Size(15, 15)
        Me.lblDCGrayText.TabIndex = 2
        Me.lblDCGrayText.Text = "T"
        Me.lblDCGrayText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDCBlueText
        '
        Me.lblDCBlueText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDCBlueText.ForeColor = System.Drawing.Color.Blue
        Me.lblDCBlueText.Location = New System.Drawing.Point(110, 10)
        Me.lblDCBlueText.Name = "lblDCBlueText"
        Me.lblDCBlueText.Size = New System.Drawing.Size(15, 15)
        Me.lblDCBlueText.TabIndex = 1
        Me.lblDCBlueText.Text = "T"
        Me.lblDCBlueText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDCGreenBackColor
        '
        Me.lblDCGreenBackColor.BackColor = System.Drawing.Color.LightGreen
        Me.lblDCGreenBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDCGreenBackColor.Location = New System.Drawing.Point(80, 10)
        Me.lblDCGreenBackColor.Name = "lblDCGreenBackColor"
        Me.lblDCGreenBackColor.Size = New System.Drawing.Size(15, 15)
        Me.lblDCGreenBackColor.TabIndex = 0
        Me.lblDCGreenBackColor.Text = "T"
        Me.lblDCGreenBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbDCCorpMinmatar
        '
        Me.gbDCCorpMinmatar.Controls.Add(Me.lblDCCorp13)
        Me.gbDCCorpMinmatar.Controls.Add(Me.chkDCCorp13)
        Me.gbDCCorpMinmatar.Controls.Add(Me.txtDCStanding13)
        Me.gbDCCorpMinmatar.Controls.Add(Me.lblDCCorpLabel4)
        Me.gbDCCorpMinmatar.Controls.Add(Me.lblDCCorp10)
        Me.gbDCCorpMinmatar.Controls.Add(Me.lblDCCorp11)
        Me.gbDCCorpMinmatar.Controls.Add(Me.lblDCCorp12)
        Me.gbDCCorpMinmatar.Controls.Add(Me.chkDCCorp10)
        Me.gbDCCorpMinmatar.Controls.Add(Me.chkDCCorp11)
        Me.gbDCCorpMinmatar.Controls.Add(Me.chkDCCorp12)
        Me.gbDCCorpMinmatar.Controls.Add(Me.txtDCStanding10)
        Me.gbDCCorpMinmatar.Controls.Add(Me.txtDCStanding11)
        Me.gbDCCorpMinmatar.Controls.Add(Me.txtDCStanding12)
        Me.gbDCCorpMinmatar.Controls.Add(Me.lblDCStanding4)
        Me.gbDCCorpMinmatar.Location = New System.Drawing.Point(746, 119)
        Me.gbDCCorpMinmatar.Name = "gbDCCorpMinmatar"
        Me.gbDCCorpMinmatar.Size = New System.Drawing.Size(228, 122)
        Me.gbDCCorpMinmatar.TabIndex = 64
        Me.gbDCCorpMinmatar.TabStop = False
        Me.gbDCCorpMinmatar.Text = "Minmatar Republic/Thukker Tribe/Khanid"
        '
        'lblDCCorp13
        '
        Me.lblDCCorp13.Location = New System.Drawing.Point(33, 101)
        Me.lblDCCorp13.Name = "lblDCCorp13"
        Me.lblDCCorp13.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp13.TabIndex = 46
        Me.lblDCCorp13.Text = "Thukker Mix"
        '
        'chkDCCorp13
        '
        Me.chkDCCorp13.AutoSize = True
        Me.chkDCCorp13.Location = New System.Drawing.Point(12, 100)
        Me.chkDCCorp13.Name = "chkDCCorp13"
        Me.chkDCCorp13.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp13.TabIndex = 47
        Me.chkDCCorp13.UseVisualStyleBackColor = True
        '
        'txtDCStanding13
        '
        Me.txtDCStanding13.Location = New System.Drawing.Point(176, 97)
        Me.txtDCStanding13.Name = "txtDCStanding13"
        Me.txtDCStanding13.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding13.TabIndex = 48
        Me.txtDCStanding13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDCCorpLabel4
        '
        Me.lblDCCorpLabel4.AutoSize = True
        Me.lblDCCorpLabel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCCorpLabel4.Location = New System.Drawing.Point(33, 16)
        Me.lblDCCorpLabel4.Name = "lblDCCorpLabel4"
        Me.lblDCCorpLabel4.Size = New System.Drawing.Size(72, 13)
        Me.lblDCCorpLabel4.TabIndex = 45
        Me.lblDCCorpLabel4.Text = "Corporation"
        Me.lblDCCorpLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDCCorp10
        '
        Me.lblDCCorp10.Location = New System.Drawing.Point(33, 35)
        Me.lblDCCorp10.Name = "lblDCCorp10"
        Me.lblDCCorp10.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp10.TabIndex = 1
        Me.lblDCCorp10.Text = "Boundless Creation"
        '
        'lblDCCorp11
        '
        Me.lblDCCorp11.Location = New System.Drawing.Point(33, 57)
        Me.lblDCCorp11.Name = "lblDCCorp11"
        Me.lblDCCorp11.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp11.TabIndex = 2
        Me.lblDCCorp11.Text = "Core Complexion Inc."
        '
        'lblDCCorp12
        '
        Me.lblDCCorp12.Location = New System.Drawing.Point(33, 79)
        Me.lblDCCorp12.Name = "lblDCCorp12"
        Me.lblDCCorp12.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp12.TabIndex = 3
        Me.lblDCCorp12.Text = "Khanid Innovation"
        '
        'chkDCCorp10
        '
        Me.chkDCCorp10.AutoSize = True
        Me.chkDCCorp10.Location = New System.Drawing.Point(12, 34)
        Me.chkDCCorp10.Name = "chkDCCorp10"
        Me.chkDCCorp10.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp10.TabIndex = 27
        Me.chkDCCorp10.UseVisualStyleBackColor = True
        '
        'chkDCCorp11
        '
        Me.chkDCCorp11.AutoSize = True
        Me.chkDCCorp11.Location = New System.Drawing.Point(12, 56)
        Me.chkDCCorp11.Name = "chkDCCorp11"
        Me.chkDCCorp11.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp11.TabIndex = 28
        Me.chkDCCorp11.UseVisualStyleBackColor = True
        '
        'chkDCCorp12
        '
        Me.chkDCCorp12.AutoSize = True
        Me.chkDCCorp12.Location = New System.Drawing.Point(12, 78)
        Me.chkDCCorp12.Name = "chkDCCorp12"
        Me.chkDCCorp12.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp12.TabIndex = 29
        Me.chkDCCorp12.UseVisualStyleBackColor = True
        '
        'txtDCStanding10
        '
        Me.txtDCStanding10.Location = New System.Drawing.Point(176, 31)
        Me.txtDCStanding10.Name = "txtDCStanding10"
        Me.txtDCStanding10.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding10.TabIndex = 39
        Me.txtDCStanding10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding11
        '
        Me.txtDCStanding11.Location = New System.Drawing.Point(176, 53)
        Me.txtDCStanding11.Name = "txtDCStanding11"
        Me.txtDCStanding11.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding11.TabIndex = 40
        Me.txtDCStanding11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding12
        '
        Me.txtDCStanding12.Location = New System.Drawing.Point(176, 75)
        Me.txtDCStanding12.Name = "txtDCStanding12"
        Me.txtDCStanding12.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding12.TabIndex = 41
        Me.txtDCStanding12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDCStanding4
        '
        Me.lblDCStanding4.AutoSize = True
        Me.lblDCStanding4.Location = New System.Drawing.Point(169, 16)
        Me.lblDCStanding4.Name = "lblDCStanding4"
        Me.lblDCStanding4.Size = New System.Drawing.Size(49, 13)
        Me.lblDCStanding4.TabIndex = 44
        Me.lblDCStanding4.Text = "Standing"
        Me.lblDCStanding4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnDCExporttoClip
        '
        Me.btnDCExporttoClip.Location = New System.Drawing.Point(980, 206)
        Me.btnDCExporttoClip.Name = "btnDCExporttoClip"
        Me.btnDCExporttoClip.Size = New System.Drawing.Size(138, 35)
        Me.btnDCExporttoClip.TabIndex = 68
        Me.btnDCExporttoClip.Text = "Copy Selected to Clipboard"
        Me.btnDCExporttoClip.UseVisualStyleBackColor = True
        '
        'gbDCCorpAmarr
        '
        Me.gbDCCorpAmarr.Controls.Add(Me.lblDCCorpLabel1)
        Me.gbDCCorpAmarr.Controls.Add(Me.lblDCCorp1)
        Me.gbDCCorpAmarr.Controls.Add(Me.lblDCCorp2)
        Me.gbDCCorpAmarr.Controls.Add(Me.lblDCCorp3)
        Me.gbDCCorpAmarr.Controls.Add(Me.chkDCCorp1)
        Me.gbDCCorpAmarr.Controls.Add(Me.chkDCCorp2)
        Me.gbDCCorpAmarr.Controls.Add(Me.chkDCCorp3)
        Me.gbDCCorpAmarr.Controls.Add(Me.txtDCStanding1)
        Me.gbDCCorpAmarr.Controls.Add(Me.txtDCStanding2)
        Me.gbDCCorpAmarr.Controls.Add(Me.txtDCStanding3)
        Me.gbDCCorpAmarr.Controls.Add(Me.lblDCStanding1)
        Me.gbDCCorpAmarr.Location = New System.Drawing.Point(518, 12)
        Me.gbDCCorpAmarr.Name = "gbDCCorpAmarr"
        Me.gbDCCorpAmarr.Size = New System.Drawing.Size(222, 100)
        Me.gbDCCorpAmarr.TabIndex = 63
        Me.gbDCCorpAmarr.TabStop = False
        Me.gbDCCorpAmarr.Text = "Amarr Empire/Ammatar Mandate"
        '
        'lblDCCorpLabel1
        '
        Me.lblDCCorpLabel1.AutoSize = True
        Me.lblDCCorpLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCCorpLabel1.Location = New System.Drawing.Point(33, 15)
        Me.lblDCCorpLabel1.Name = "lblDCCorpLabel1"
        Me.lblDCCorpLabel1.Size = New System.Drawing.Size(72, 13)
        Me.lblDCCorpLabel1.TabIndex = 45
        Me.lblDCCorpLabel1.Text = "Corporation"
        Me.lblDCCorpLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDCCorp1
        '
        Me.lblDCCorp1.Location = New System.Drawing.Point(33, 34)
        Me.lblDCCorp1.Name = "lblDCCorp1"
        Me.lblDCCorp1.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp1.TabIndex = 1
        Me.lblDCCorp1.Text = "Carthum Conglomerate"
        '
        'lblDCCorp2
        '
        Me.lblDCCorp2.Location = New System.Drawing.Point(33, 56)
        Me.lblDCCorp2.Name = "lblDCCorp2"
        Me.lblDCCorp2.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp2.TabIndex = 2
        Me.lblDCCorp2.Text = "Viziam"
        '
        'lblDCCorp3
        '
        Me.lblDCCorp3.Location = New System.Drawing.Point(33, 78)
        Me.lblDCCorp3.Name = "lblDCCorp3"
        Me.lblDCCorp3.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp3.TabIndex = 3
        Me.lblDCCorp3.Text = "Nefantar Miner Association"
        '
        'chkDCCorp1
        '
        Me.chkDCCorp1.AutoSize = True
        Me.chkDCCorp1.Location = New System.Drawing.Point(12, 33)
        Me.chkDCCorp1.Name = "chkDCCorp1"
        Me.chkDCCorp1.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp1.TabIndex = 27
        Me.chkDCCorp1.UseVisualStyleBackColor = True
        '
        'chkDCCorp2
        '
        Me.chkDCCorp2.AutoSize = True
        Me.chkDCCorp2.Location = New System.Drawing.Point(12, 55)
        Me.chkDCCorp2.Name = "chkDCCorp2"
        Me.chkDCCorp2.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp2.TabIndex = 28
        Me.chkDCCorp2.UseVisualStyleBackColor = True
        '
        'chkDCCorp3
        '
        Me.chkDCCorp3.AutoSize = True
        Me.chkDCCorp3.Location = New System.Drawing.Point(12, 77)
        Me.chkDCCorp3.Name = "chkDCCorp3"
        Me.chkDCCorp3.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp3.TabIndex = 29
        Me.chkDCCorp3.UseVisualStyleBackColor = True
        '
        'txtDCStanding1
        '
        Me.txtDCStanding1.Location = New System.Drawing.Point(174, 30)
        Me.txtDCStanding1.Name = "txtDCStanding1"
        Me.txtDCStanding1.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding1.TabIndex = 39
        Me.txtDCStanding1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding2
        '
        Me.txtDCStanding2.Location = New System.Drawing.Point(174, 52)
        Me.txtDCStanding2.Name = "txtDCStanding2"
        Me.txtDCStanding2.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding2.TabIndex = 40
        Me.txtDCStanding2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding3
        '
        Me.txtDCStanding3.Location = New System.Drawing.Point(174, 74)
        Me.txtDCStanding3.Name = "txtDCStanding3"
        Me.txtDCStanding3.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding3.TabIndex = 41
        Me.txtDCStanding3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDCStanding1
        '
        Me.lblDCStanding1.AutoSize = True
        Me.lblDCStanding1.Location = New System.Drawing.Point(165, 14)
        Me.lblDCStanding1.Name = "lblDCStanding1"
        Me.lblDCStanding1.Size = New System.Drawing.Size(49, 13)
        Me.lblDCStanding1.TabIndex = 44
        Me.lblDCStanding1.Text = "Standing"
        Me.lblDCStanding1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnDCReset
        '
        Me.btnDCReset.Location = New System.Drawing.Point(980, 146)
        Me.btnDCReset.Name = "btnDCReset"
        Me.btnDCReset.Size = New System.Drawing.Size(138, 30)
        Me.btnDCReset.TabIndex = 67
        Me.btnDCReset.Text = "Reset"
        Me.btnDCReset.UseVisualStyleBackColor = True
        '
        'gbDCCorpsCaldari
        '
        Me.gbDCCorpsCaldari.Controls.Add(Me.lblDCCorpLabel2)
        Me.gbDCCorpsCaldari.Controls.Add(Me.lblDCCorp4)
        Me.gbDCCorpsCaldari.Controls.Add(Me.lblDCCorp5)
        Me.gbDCCorpsCaldari.Controls.Add(Me.lblDCCorp6)
        Me.gbDCCorpsCaldari.Controls.Add(Me.chkDCCorp4)
        Me.gbDCCorpsCaldari.Controls.Add(Me.chkDCCorp5)
        Me.gbDCCorpsCaldari.Controls.Add(Me.chkDCCorp6)
        Me.gbDCCorpsCaldari.Controls.Add(Me.txtDCStanding4)
        Me.gbDCCorpsCaldari.Controls.Add(Me.txtDCStanding5)
        Me.gbDCCorpsCaldari.Controls.Add(Me.txtDCStanding6)
        Me.gbDCCorpsCaldari.Controls.Add(Me.lblDCStanding2)
        Me.gbDCCorpsCaldari.Location = New System.Drawing.Point(518, 114)
        Me.gbDCCorpsCaldari.Name = "gbDCCorpsCaldari"
        Me.gbDCCorpsCaldari.Size = New System.Drawing.Size(222, 100)
        Me.gbDCCorpsCaldari.TabIndex = 62
        Me.gbDCCorpsCaldari.TabStop = False
        Me.gbDCCorpsCaldari.Text = "Caldari State"
        '
        'lblDCCorpLabel2
        '
        Me.lblDCCorpLabel2.AutoSize = True
        Me.lblDCCorpLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCCorpLabel2.Location = New System.Drawing.Point(33, 16)
        Me.lblDCCorpLabel2.Name = "lblDCCorpLabel2"
        Me.lblDCCorpLabel2.Size = New System.Drawing.Size(72, 13)
        Me.lblDCCorpLabel2.TabIndex = 45
        Me.lblDCCorpLabel2.Text = "Corporation"
        Me.lblDCCorpLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDCCorp4
        '
        Me.lblDCCorp4.Location = New System.Drawing.Point(33, 34)
        Me.lblDCCorp4.Name = "lblDCCorp4"
        Me.lblDCCorp4.Size = New System.Drawing.Size(131, 13)
        Me.lblDCCorp4.TabIndex = 1
        Me.lblDCCorp4.Text = "Ishukone Corporation"
        '
        'lblDCCorp5
        '
        Me.lblDCCorp5.Location = New System.Drawing.Point(33, 56)
        Me.lblDCCorp5.Name = "lblDCCorp5"
        Me.lblDCCorp5.Size = New System.Drawing.Size(131, 13)
        Me.lblDCCorp5.TabIndex = 2
        Me.lblDCCorp5.Text = "Kaalakiota Corporation"
        '
        'lblDCCorp6
        '
        Me.lblDCCorp6.Location = New System.Drawing.Point(33, 78)
        Me.lblDCCorp6.Name = "lblDCCorp6"
        Me.lblDCCorp6.Size = New System.Drawing.Size(131, 13)
        Me.lblDCCorp6.TabIndex = 3
        Me.lblDCCorp6.Text = "Lai Dai Corporation"
        '
        'chkDCCorp4
        '
        Me.chkDCCorp4.AutoSize = True
        Me.chkDCCorp4.Location = New System.Drawing.Point(10, 33)
        Me.chkDCCorp4.Name = "chkDCCorp4"
        Me.chkDCCorp4.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp4.TabIndex = 27
        Me.chkDCCorp4.UseVisualStyleBackColor = True
        '
        'chkDCCorp5
        '
        Me.chkDCCorp5.AutoSize = True
        Me.chkDCCorp5.Location = New System.Drawing.Point(10, 55)
        Me.chkDCCorp5.Name = "chkDCCorp5"
        Me.chkDCCorp5.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp5.TabIndex = 28
        Me.chkDCCorp5.UseVisualStyleBackColor = True
        '
        'chkDCCorp6
        '
        Me.chkDCCorp6.AutoSize = True
        Me.chkDCCorp6.Location = New System.Drawing.Point(10, 77)
        Me.chkDCCorp6.Name = "chkDCCorp6"
        Me.chkDCCorp6.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp6.TabIndex = 29
        Me.chkDCCorp6.UseVisualStyleBackColor = True
        '
        'txtDCStanding4
        '
        Me.txtDCStanding4.Location = New System.Drawing.Point(174, 32)
        Me.txtDCStanding4.Name = "txtDCStanding4"
        Me.txtDCStanding4.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding4.TabIndex = 39
        Me.txtDCStanding4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding5
        '
        Me.txtDCStanding5.Location = New System.Drawing.Point(174, 54)
        Me.txtDCStanding5.Name = "txtDCStanding5"
        Me.txtDCStanding5.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding5.TabIndex = 40
        Me.txtDCStanding5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding6
        '
        Me.txtDCStanding6.Location = New System.Drawing.Point(174, 76)
        Me.txtDCStanding6.Name = "txtDCStanding6"
        Me.txtDCStanding6.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding6.TabIndex = 41
        Me.txtDCStanding6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDCStanding2
        '
        Me.lblDCStanding2.AutoSize = True
        Me.lblDCStanding2.Location = New System.Drawing.Point(163, 16)
        Me.lblDCStanding2.Name = "lblDCStanding2"
        Me.lblDCStanding2.Size = New System.Drawing.Size(49, 13)
        Me.lblDCStanding2.TabIndex = 44
        Me.lblDCStanding2.Text = "Standing"
        Me.lblDCStanding2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbDCCorpsGallente
        '
        Me.gbDCCorpsGallente.Controls.Add(Me.lblDCCorpLabel3)
        Me.gbDCCorpsGallente.Controls.Add(Me.lblDCCorp7)
        Me.gbDCCorpsGallente.Controls.Add(Me.lblDCCorp8)
        Me.gbDCCorpsGallente.Controls.Add(Me.lblDCCorp9)
        Me.gbDCCorpsGallente.Controls.Add(Me.chkDCCorp7)
        Me.gbDCCorpsGallente.Controls.Add(Me.chkDCCorp8)
        Me.gbDCCorpsGallente.Controls.Add(Me.chkDCCorp9)
        Me.gbDCCorpsGallente.Controls.Add(Me.txtDCStanding7)
        Me.gbDCCorpsGallente.Controls.Add(Me.txtDCStanding8)
        Me.gbDCCorpsGallente.Controls.Add(Me.txtDCStanding9)
        Me.gbDCCorpsGallente.Controls.Add(Me.lblDCStanding3)
        Me.gbDCCorpsGallente.Location = New System.Drawing.Point(746, 12)
        Me.gbDCCorpsGallente.Name = "gbDCCorpsGallente"
        Me.gbDCCorpsGallente.Size = New System.Drawing.Size(226, 100)
        Me.gbDCCorpsGallente.TabIndex = 61
        Me.gbDCCorpsGallente.TabStop = False
        Me.gbDCCorpsGallente.Text = "Gallente Federation"
        '
        'lblDCCorpLabel3
        '
        Me.lblDCCorpLabel3.AutoSize = True
        Me.lblDCCorpLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCCorpLabel3.Location = New System.Drawing.Point(33, 16)
        Me.lblDCCorpLabel3.Name = "lblDCCorpLabel3"
        Me.lblDCCorpLabel3.Size = New System.Drawing.Size(72, 13)
        Me.lblDCCorpLabel3.TabIndex = 45
        Me.lblDCCorpLabel3.Text = "Corporation"
        Me.lblDCCorpLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDCCorp7
        '
        Me.lblDCCorp7.Location = New System.Drawing.Point(33, 35)
        Me.lblDCCorp7.Name = "lblDCCorp7"
        Me.lblDCCorp7.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp7.TabIndex = 1
        Me.lblDCCorp7.Text = "CreoDron"
        '
        'lblDCCorp8
        '
        Me.lblDCCorp8.Location = New System.Drawing.Point(33, 57)
        Me.lblDCCorp8.Name = "lblDCCorp8"
        Me.lblDCCorp8.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp8.TabIndex = 2
        Me.lblDCCorp8.Text = "Duvolle Laboratories"
        '
        'lblDCCorp9
        '
        Me.lblDCCorp9.Location = New System.Drawing.Point(33, 79)
        Me.lblDCCorp9.Name = "lblDCCorp9"
        Me.lblDCCorp9.Size = New System.Drawing.Size(139, 13)
        Me.lblDCCorp9.TabIndex = 3
        Me.lblDCCorp9.Text = "Roden Shipyards"
        '
        'chkDCCorp7
        '
        Me.chkDCCorp7.AutoSize = True
        Me.chkDCCorp7.Location = New System.Drawing.Point(12, 34)
        Me.chkDCCorp7.Name = "chkDCCorp7"
        Me.chkDCCorp7.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp7.TabIndex = 27
        Me.chkDCCorp7.UseVisualStyleBackColor = True
        '
        'chkDCCorp8
        '
        Me.chkDCCorp8.AutoSize = True
        Me.chkDCCorp8.Location = New System.Drawing.Point(12, 56)
        Me.chkDCCorp8.Name = "chkDCCorp8"
        Me.chkDCCorp8.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp8.TabIndex = 28
        Me.chkDCCorp8.UseVisualStyleBackColor = True
        '
        'chkDCCorp9
        '
        Me.chkDCCorp9.AutoSize = True
        Me.chkDCCorp9.Location = New System.Drawing.Point(12, 78)
        Me.chkDCCorp9.Name = "chkDCCorp9"
        Me.chkDCCorp9.Size = New System.Drawing.Size(15, 14)
        Me.chkDCCorp9.TabIndex = 29
        Me.chkDCCorp9.UseVisualStyleBackColor = True
        '
        'txtDCStanding7
        '
        Me.txtDCStanding7.Location = New System.Drawing.Point(178, 31)
        Me.txtDCStanding7.Name = "txtDCStanding7"
        Me.txtDCStanding7.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding7.TabIndex = 39
        Me.txtDCStanding7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding8
        '
        Me.txtDCStanding8.Location = New System.Drawing.Point(178, 53)
        Me.txtDCStanding8.Name = "txtDCStanding8"
        Me.txtDCStanding8.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding8.TabIndex = 40
        Me.txtDCStanding8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDCStanding9
        '
        Me.txtDCStanding9.Location = New System.Drawing.Point(178, 75)
        Me.txtDCStanding9.Name = "txtDCStanding9"
        Me.txtDCStanding9.Size = New System.Drawing.Size(35, 20)
        Me.txtDCStanding9.TabIndex = 41
        Me.txtDCStanding9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDCStanding3
        '
        Me.lblDCStanding3.AutoSize = True
        Me.lblDCStanding3.Location = New System.Drawing.Point(169, 15)
        Me.lblDCStanding3.Name = "lblDCStanding3"
        Me.lblDCStanding3.Size = New System.Drawing.Size(49, 13)
        Me.lblDCStanding3.TabIndex = 44
        Me.lblDCStanding3.Text = "Standing"
        Me.lblDCStanding3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnDCRefresh
        '
        Me.btnDCRefresh.Location = New System.Drawing.Point(980, 116)
        Me.btnDCRefresh.Name = "btnDCRefresh"
        Me.btnDCRefresh.Size = New System.Drawing.Size(138, 30)
        Me.btnDCRefresh.TabIndex = 66
        Me.btnDCRefresh.Text = "Refresh"
        Me.btnDCRefresh.UseVisualStyleBackColor = True
        '
        'tabManufacturing
        '
        Me.tabManufacturing.Controls.Add(Me.lstManufacturing)
        Me.tabManufacturing.Controls.Add(Me.gbCalcBPSelectOptions)
        Me.tabManufacturing.Location = New System.Drawing.Point(4, 22)
        Me.tabManufacturing.Name = "tabManufacturing"
        Me.tabManufacturing.Size = New System.Drawing.Size(1137, 615)
        Me.tabManufacturing.TabIndex = 2
        Me.tabManufacturing.Text = "Manufacturing List"
        Me.tabManufacturing.UseVisualStyleBackColor = True
        '
        'lstManufacturing
        '
        Me.lstManufacturing.AllowColumnReorder = True
        Me.lstManufacturing.ContextMenuStrip = Me.ListOptionsMenu
        Me.lstManufacturing.FullRowSelect = True
        Me.lstManufacturing.GridLines = True
        Me.lstManufacturing.Location = New System.Drawing.Point(8, 7)
        Me.lstManufacturing.Name = "lstManufacturing"
        Me.lstManufacturing.OwnerDraw = True
        Me.lstManufacturing.Size = New System.Drawing.Size(1121, 300)
        Me.lstManufacturing.TabIndex = 1
        Me.lstManufacturing.UseCompatibleStateImageBehavior = False
        Me.lstManufacturing.View = System.Windows.Forms.View.Details
        '
        'gbCalcBPSelectOptions
        '
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcIgnoreinCalcs)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbIncludeTaxesFees)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcSellExessItems)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.chkCalcNPCBPOs)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcShowAssets)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcIncludeItems)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcMarketFilters)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcCalculate)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcSelectColumns)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcSizeLimit)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcProdLines)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcCompareType)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcTextColors)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcInvention)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcBPRace)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbTempMEPE)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.tabCalcFacilities)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcFilter)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcBPTech)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcIncludeOwned)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcSaveSettings)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcExportList)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcPreview)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.btnCalcReset)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcTextFilter)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcBPType)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcBPSelect)
        Me.gbCalcBPSelectOptions.Controls.Add(Me.gbCalcRelics)
        Me.gbCalcBPSelectOptions.Location = New System.Drawing.Point(8, 308)
        Me.gbCalcBPSelectOptions.Name = "gbCalcBPSelectOptions"
        Me.gbCalcBPSelectOptions.Size = New System.Drawing.Size(1121, 300)
        Me.gbCalcBPSelectOptions.TabIndex = 0
        Me.gbCalcBPSelectOptions.TabStop = False
        Me.gbCalcBPSelectOptions.Text = "Blueprint Filters:"
        '
        'gbCalcIgnoreinCalcs
        '
        Me.gbCalcIgnoreinCalcs.Controls.Add(Me.chkCalcIgnoreMinerals)
        Me.gbCalcIgnoreinCalcs.Controls.Add(Me.chkCalcIgnoreT1Item)
        Me.gbCalcIgnoreinCalcs.Controls.Add(Me.chkCalcIgnoreInvention)
        Me.gbCalcIgnoreinCalcs.Location = New System.Drawing.Point(859, 199)
        Me.gbCalcIgnoreinCalcs.Name = "gbCalcIgnoreinCalcs"
        Me.gbCalcIgnoreinCalcs.Size = New System.Drawing.Size(156, 56)
        Me.gbCalcIgnoreinCalcs.TabIndex = 19
        Me.gbCalcIgnoreinCalcs.TabStop = False
        Me.gbCalcIgnoreinCalcs.Text = "Ignore in Calculations:"
        '
        'chkCalcIgnoreMinerals
        '
        Me.chkCalcIgnoreMinerals.AutoSize = True
        Me.chkCalcIgnoreMinerals.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkCalcIgnoreMinerals.Location = New System.Drawing.Point(86, 16)
        Me.chkCalcIgnoreMinerals.Name = "chkCalcIgnoreMinerals"
        Me.chkCalcIgnoreMinerals.Size = New System.Drawing.Size(65, 17)
        Me.chkCalcIgnoreMinerals.TabIndex = 1
        Me.chkCalcIgnoreMinerals.Text = "Minerals"
        Me.chkCalcIgnoreMinerals.UseVisualStyleBackColor = True
        '
        'chkCalcIgnoreT1Item
        '
        Me.chkCalcIgnoreT1Item.AutoSize = True
        Me.chkCalcIgnoreT1Item.Location = New System.Drawing.Point(9, 34)
        Me.chkCalcIgnoreT1Item.Name = "chkCalcIgnoreT1Item"
        Me.chkCalcIgnoreT1Item.Size = New System.Drawing.Size(89, 17)
        Me.chkCalcIgnoreT1Item.TabIndex = 2
        Me.chkCalcIgnoreT1Item.Text = "T1 Base Item"
        Me.chkCalcIgnoreT1Item.UseVisualStyleBackColor = True
        '
        'chkCalcIgnoreInvention
        '
        Me.chkCalcIgnoreInvention.AutoSize = True
        Me.chkCalcIgnoreInvention.Location = New System.Drawing.Point(9, 16)
        Me.chkCalcIgnoreInvention.Name = "chkCalcIgnoreInvention"
        Me.chkCalcIgnoreInvention.Size = New System.Drawing.Size(70, 17)
        Me.chkCalcIgnoreInvention.TabIndex = 0
        Me.chkCalcIgnoreInvention.Text = "Invention"
        Me.chkCalcIgnoreInvention.UseVisualStyleBackColor = True
        '
        'gbIncludeTaxesFees
        '
        Me.gbIncludeTaxesFees.Controls.Add(Me.txtCalcBrokerFeeRate)
        Me.gbIncludeTaxesFees.Controls.Add(Me.chkCalcFees)
        Me.gbIncludeTaxesFees.Controls.Add(Me.chkCalcTaxes)
        Me.gbIncludeTaxesFees.Location = New System.Drawing.Point(859, 255)
        Me.gbIncludeTaxesFees.Name = "gbIncludeTaxesFees"
        Me.gbIncludeTaxesFees.Size = New System.Drawing.Size(156, 39)
        Me.gbIncludeTaxesFees.TabIndex = 17
        Me.gbIncludeTaxesFees.TabStop = False
        Me.gbIncludeTaxesFees.Text = "Include:"
        '
        'txtCalcBrokerFeeRate
        '
        Me.txtCalcBrokerFeeRate.Location = New System.Drawing.Point(114, 14)
        Me.txtCalcBrokerFeeRate.Name = "txtCalcBrokerFeeRate"
        Me.txtCalcBrokerFeeRate.Size = New System.Drawing.Size(37, 20)
        Me.txtCalcBrokerFeeRate.TabIndex = 62
        Me.txtCalcBrokerFeeRate.TabStop = False
        Me.txtCalcBrokerFeeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCalcBrokerFeeRate.Visible = False
        '
        'chkCalcFees
        '
        Me.chkCalcFees.AutoSize = True
        Me.chkCalcFees.Location = New System.Drawing.Point(65, 17)
        Me.chkCalcFees.Name = "chkCalcFees"
        Me.chkCalcFees.Size = New System.Drawing.Size(49, 17)
        Me.chkCalcFees.TabIndex = 1
        Me.chkCalcFees.Text = "Fees"
        Me.chkCalcFees.ThreeState = True
        Me.chkCalcFees.UseVisualStyleBackColor = True
        '
        'chkCalcTaxes
        '
        Me.chkCalcTaxes.AutoSize = True
        Me.chkCalcTaxes.Location = New System.Drawing.Point(9, 17)
        Me.chkCalcTaxes.Name = "chkCalcTaxes"
        Me.chkCalcTaxes.Size = New System.Drawing.Size(55, 17)
        Me.chkCalcTaxes.TabIndex = 0
        Me.chkCalcTaxes.Text = "Taxes"
        Me.chkCalcTaxes.UseVisualStyleBackColor = True
        '
        'gbCalcSellExessItems
        '
        Me.gbCalcSellExessItems.Controls.Add(Me.rbtnCalcAdvT2MatType)
        Me.gbCalcSellExessItems.Controls.Add(Me.rbtnCalcProcT2MatType)
        Me.gbCalcSellExessItems.Controls.Add(Me.rbtnCalcRawT2MatType)
        Me.gbCalcSellExessItems.Controls.Add(Me.chkCalcSellExessItems)
        Me.gbCalcSellExessItems.Location = New System.Drawing.Point(1019, 197)
        Me.gbCalcSellExessItems.Name = "gbCalcSellExessItems"
        Me.gbCalcSellExessItems.Size = New System.Drawing.Size(96, 97)
        Me.gbCalcSellExessItems.TabIndex = 29
        Me.gbCalcSellExessItems.TabStop = False
        Me.gbCalcSellExessItems.Text = "T2/3 Mat Type"
        '
        'rbtnCalcAdvT2MatType
        '
        Me.rbtnCalcAdvT2MatType.AutoSize = True
        Me.rbtnCalcAdvT2MatType.BackColor = System.Drawing.Color.Transparent
        Me.rbtnCalcAdvT2MatType.Location = New System.Drawing.Point(7, 17)
        Me.rbtnCalcAdvT2MatType.Name = "rbtnCalcAdvT2MatType"
        Me.rbtnCalcAdvT2MatType.Size = New System.Drawing.Size(74, 17)
        Me.rbtnCalcAdvT2MatType.TabIndex = 76
        Me.rbtnCalcAdvT2MatType.TabStop = True
        Me.rbtnCalcAdvT2MatType.Text = "Advanced"
        Me.rbtnCalcAdvT2MatType.UseVisualStyleBackColor = False
        '
        'rbtnCalcProcT2MatType
        '
        Me.rbtnCalcProcT2MatType.AutoSize = True
        Me.rbtnCalcProcT2MatType.BackColor = System.Drawing.Color.Transparent
        Me.rbtnCalcProcT2MatType.Location = New System.Drawing.Point(7, 33)
        Me.rbtnCalcProcT2MatType.Name = "rbtnCalcProcT2MatType"
        Me.rbtnCalcProcT2MatType.Size = New System.Drawing.Size(75, 17)
        Me.rbtnCalcProcT2MatType.TabIndex = 77
        Me.rbtnCalcProcT2MatType.TabStop = True
        Me.rbtnCalcProcT2MatType.Text = "Processed"
        Me.rbtnCalcProcT2MatType.UseVisualStyleBackColor = False
        '
        'rbtnCalcRawT2MatType
        '
        Me.rbtnCalcRawT2MatType.AutoSize = True
        Me.rbtnCalcRawT2MatType.BackColor = System.Drawing.Color.Transparent
        Me.rbtnCalcRawT2MatType.Location = New System.Drawing.Point(7, 49)
        Me.rbtnCalcRawT2MatType.Name = "rbtnCalcRawT2MatType"
        Me.rbtnCalcRawT2MatType.Size = New System.Drawing.Size(47, 17)
        Me.rbtnCalcRawT2MatType.TabIndex = 78
        Me.rbtnCalcRawT2MatType.TabStop = True
        Me.rbtnCalcRawT2MatType.Text = "Raw"
        Me.rbtnCalcRawT2MatType.UseVisualStyleBackColor = False
        '
        'chkCalcSellExessItems
        '
        Me.chkCalcSellExessItems.Location = New System.Drawing.Point(7, 65)
        Me.chkCalcSellExessItems.Name = "chkCalcSellExessItems"
        Me.chkCalcSellExessItems.Size = New System.Drawing.Size(83, 32)
        Me.chkCalcSellExessItems.TabIndex = 1
        Me.chkCalcSellExessItems.Text = "Sell excess build items"
        Me.chkCalcSellExessItems.UseVisualStyleBackColor = True
        '
        'chkCalcNPCBPOs
        '
        Me.chkCalcNPCBPOs.AutoSize = True
        Me.chkCalcNPCBPOs.Location = New System.Drawing.Point(443, 13)
        Me.chkCalcNPCBPOs.Name = "chkCalcNPCBPOs"
        Me.chkCalcNPCBPOs.Size = New System.Drawing.Size(78, 17)
        Me.chkCalcNPCBPOs.TabIndex = 15
        Me.chkCalcNPCBPOs.Text = "NPC BPOs"
        Me.chkCalcNPCBPOs.UseVisualStyleBackColor = True
        '
        'btnCalcShowAssets
        '
        Me.btnCalcShowAssets.Image = CType(resources.GetObject("btnCalcShowAssets.Image"), System.Drawing.Image)
        Me.btnCalcShowAssets.Location = New System.Drawing.Point(219, 205)
        Me.btnCalcShowAssets.Name = "btnCalcShowAssets"
        Me.btnCalcShowAssets.Size = New System.Drawing.Size(48, 48)
        Me.btnCalcShowAssets.TabIndex = 28
        Me.btnCalcShowAssets.UseVisualStyleBackColor = True
        '
        'gbCalcIncludeItems
        '
        Me.gbCalcIncludeItems.Controls.Add(Me.chkCalcCanInvent)
        Me.gbCalcIncludeItems.Controls.Add(Me.chkCalcCanBuild)
        Me.gbCalcIncludeItems.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.gbCalcIncludeItems.Location = New System.Drawing.Point(409, 147)
        Me.gbCalcIncludeItems.Name = "gbCalcIncludeItems"
        Me.gbCalcIncludeItems.Size = New System.Drawing.Size(134, 42)
        Me.gbCalcIncludeItems.TabIndex = 12
        Me.gbCalcIncludeItems.TabStop = False
        Me.gbCalcIncludeItems.Text = "Only Items I Can:"
        '
        'chkCalcCanInvent
        '
        Me.chkCalcCanInvent.AutoSize = True
        Me.chkCalcCanInvent.Location = New System.Drawing.Point(66, 19)
        Me.chkCalcCanInvent.Name = "chkCalcCanInvent"
        Me.chkCalcCanInvent.Size = New System.Drawing.Size(56, 17)
        Me.chkCalcCanInvent.TabIndex = 1
        Me.chkCalcCanInvent.Text = "Invent"
        Me.chkCalcCanInvent.UseVisualStyleBackColor = True
        '
        'chkCalcCanBuild
        '
        Me.chkCalcCanBuild.AutoSize = True
        Me.chkCalcCanBuild.Location = New System.Drawing.Point(9, 19)
        Me.chkCalcCanBuild.Name = "chkCalcCanBuild"
        Me.chkCalcCanBuild.Size = New System.Drawing.Size(49, 17)
        Me.chkCalcCanBuild.TabIndex = 0
        Me.chkCalcCanBuild.Text = "Build"
        Me.chkCalcCanBuild.UseVisualStyleBackColor = True
        '
        'gbCalcMarketFilters
        '
        Me.gbCalcMarketFilters.Controls.Add(Me.txtCalcProfitThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.tpMaxBuildTimeFilter)
        Me.gbCalcMarketFilters.Controls.Add(Me.txtCalcSVRThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.tpMinBuildTimeFilter)
        Me.gbCalcMarketFilters.Controls.Add(Me.chkCalcMaxBuildTimeFilter)
        Me.gbCalcMarketFilters.Controls.Add(Me.chkCalcMinBuildTimeFilter)
        Me.gbCalcMarketFilters.Controls.Add(Me.cmbCalcPriceTrend)
        Me.gbCalcMarketFilters.Controls.Add(Me.cmbCalcAvgPriceDuration)
        Me.gbCalcMarketFilters.Controls.Add(Me.lblCalcPriceTrend)
        Me.gbCalcMarketFilters.Controls.Add(Me.txtCalcVolumeThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.cmbCalcHistoryRegion)
        Me.gbCalcMarketFilters.Controls.Add(Me.lblCalcHistoryRegion)
        Me.gbCalcMarketFilters.Controls.Add(Me.lblCalcSVRThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.lblCalcAvgPrice)
        Me.gbCalcMarketFilters.Controls.Add(Me.txtCalcIPHThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.chkCalcProfitThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.chkCalcVolumeThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.chkCalcIPHThreshold)
        Me.gbCalcMarketFilters.Controls.Add(Me.chkCalcSVRIncludeNull)
        Me.gbCalcMarketFilters.Location = New System.Drawing.Point(409, 188)
        Me.gbCalcMarketFilters.Name = "gbCalcMarketFilters"
        Me.gbCalcMarketFilters.Size = New System.Drawing.Size(447, 106)
        Me.gbCalcMarketFilters.TabIndex = 27
        Me.gbCalcMarketFilters.TabStop = False
        Me.gbCalcMarketFilters.Text = "Market Filters:"
        '
        'txtCalcProfitThreshold
        '
        Me.txtCalcProfitThreshold.Enabled = False
        Me.txtCalcProfitThreshold.Location = New System.Drawing.Point(333, 79)
        Me.txtCalcProfitThreshold.Name = "txtCalcProfitThreshold"
        Me.txtCalcProfitThreshold.Size = New System.Drawing.Size(108, 20)
        Me.txtCalcProfitThreshold.TabIndex = 62
        Me.txtCalcProfitThreshold.TabStop = False
        Me.txtCalcProfitThreshold.Text = "0.00"
        Me.txtCalcProfitThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpMaxBuildTimeFilter
        '
        Me.tpMaxBuildTimeFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpMaxBuildTimeFilter.Enabled = False
        Me.tpMaxBuildTimeFilter.Location = New System.Drawing.Point(328, 39)
        Me.tpMaxBuildTimeFilter.Name = "tpMaxBuildTimeFilter"
        Me.tpMaxBuildTimeFilter.Size = New System.Drawing.Size(113, 22)
        Me.tpMaxBuildTimeFilter.TabIndex = 70
        '
        'txtCalcSVRThreshold
        '
        Me.txtCalcSVRThreshold.Location = New System.Drawing.Point(65, 40)
        Me.txtCalcSVRThreshold.MaxLength = 10
        Me.txtCalcSVRThreshold.Name = "txtCalcSVRThreshold"
        Me.txtCalcSVRThreshold.Size = New System.Drawing.Size(60, 20)
        Me.txtCalcSVRThreshold.TabIndex = 1
        Me.txtCalcSVRThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpMinBuildTimeFilter
        '
        Me.tpMinBuildTimeFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpMinBuildTimeFilter.Enabled = False
        Me.tpMinBuildTimeFilter.Location = New System.Drawing.Point(328, 14)
        Me.tpMinBuildTimeFilter.Name = "tpMinBuildTimeFilter"
        Me.tpMinBuildTimeFilter.Size = New System.Drawing.Size(113, 22)
        Me.tpMinBuildTimeFilter.TabIndex = 69
        '
        'chkCalcMaxBuildTimeFilter
        '
        Me.chkCalcMaxBuildTimeFilter.AutoSize = True
        Me.chkCalcMaxBuildTimeFilter.Location = New System.Drawing.Point(227, 42)
        Me.chkCalcMaxBuildTimeFilter.Name = "chkCalcMaxBuildTimeFilter"
        Me.chkCalcMaxBuildTimeFilter.Size = New System.Drawing.Size(101, 17)
        Me.chkCalcMaxBuildTimeFilter.TabIndex = 71
        Me.chkCalcMaxBuildTimeFilter.Text = "Max Build Time:"
        Me.chkCalcMaxBuildTimeFilter.UseVisualStyleBackColor = True
        '
        'chkCalcMinBuildTimeFilter
        '
        Me.chkCalcMinBuildTimeFilter.AutoSize = True
        Me.chkCalcMinBuildTimeFilter.Location = New System.Drawing.Point(227, 17)
        Me.chkCalcMinBuildTimeFilter.Name = "chkCalcMinBuildTimeFilter"
        Me.chkCalcMinBuildTimeFilter.Size = New System.Drawing.Size(98, 17)
        Me.chkCalcMinBuildTimeFilter.TabIndex = 70
        Me.chkCalcMinBuildTimeFilter.Text = "Min Build Time:"
        Me.chkCalcMinBuildTimeFilter.UseVisualStyleBackColor = True
        '
        'cmbCalcPriceTrend
        '
        Me.cmbCalcPriceTrend.FormattingEnabled = True
        Me.cmbCalcPriceTrend.Items.AddRange(New Object() {"All", "Up", "Down"})
        Me.cmbCalcPriceTrend.Location = New System.Drawing.Point(75, 78)
        Me.cmbCalcPriceTrend.MaxLength = 3
        Me.cmbCalcPriceTrend.Name = "cmbCalcPriceTrend"
        Me.cmbCalcPriceTrend.Size = New System.Drawing.Size(48, 21)
        Me.cmbCalcPriceTrend.TabIndex = 7
        '
        'cmbCalcAvgPriceDuration
        '
        Me.cmbCalcAvgPriceDuration.FormattingEnabled = True
        Me.cmbCalcAvgPriceDuration.Items.AddRange(New Object() {"7", "15", "30", "60", "90"})
        Me.cmbCalcAvgPriceDuration.Location = New System.Drawing.Point(164, 39)
        Me.cmbCalcAvgPriceDuration.MaxLength = 3
        Me.cmbCalcAvgPriceDuration.Name = "cmbCalcAvgPriceDuration"
        Me.cmbCalcAvgPriceDuration.Size = New System.Drawing.Size(41, 21)
        Me.cmbCalcAvgPriceDuration.TabIndex = 3
        '
        'lblCalcPriceTrend
        '
        Me.lblCalcPriceTrend.AutoSize = True
        Me.lblCalcPriceTrend.Location = New System.Drawing.Point(6, 82)
        Me.lblCalcPriceTrend.Name = "lblCalcPriceTrend"
        Me.lblCalcPriceTrend.Size = New System.Drawing.Size(65, 13)
        Me.lblCalcPriceTrend.TabIndex = 6
        Me.lblCalcPriceTrend.Text = "Price Trend:"
        Me.lblCalcPriceTrend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCalcVolumeThreshold
        '
        Me.txtCalcVolumeThreshold.Enabled = False
        Me.txtCalcVolumeThreshold.Location = New System.Drawing.Point(239, 79)
        Me.txtCalcVolumeThreshold.Name = "txtCalcVolumeThreshold"
        Me.txtCalcVolumeThreshold.Size = New System.Drawing.Size(90, 20)
        Me.txtCalcVolumeThreshold.TabIndex = 63
        Me.txtCalcVolumeThreshold.TabStop = False
        Me.txtCalcVolumeThreshold.Text = "0.00"
        Me.txtCalcVolumeThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbCalcHistoryRegion
        '
        Me.cmbCalcHistoryRegion.FormattingEnabled = True
        Me.cmbCalcHistoryRegion.Location = New System.Drawing.Point(56, 15)
        Me.cmbCalcHistoryRegion.Name = "cmbCalcHistoryRegion"
        Me.cmbCalcHistoryRegion.Size = New System.Drawing.Size(149, 21)
        Me.cmbCalcHistoryRegion.TabIndex = 5
        '
        'lblCalcHistoryRegion
        '
        Me.lblCalcHistoryRegion.AutoSize = True
        Me.lblCalcHistoryRegion.Location = New System.Drawing.Point(6, 18)
        Me.lblCalcHistoryRegion.Name = "lblCalcHistoryRegion"
        Me.lblCalcHistoryRegion.Size = New System.Drawing.Size(44, 13)
        Me.lblCalcHistoryRegion.TabIndex = 4
        Me.lblCalcHistoryRegion.Text = "Region:"
        '
        'lblCalcSVRThreshold
        '
        Me.lblCalcSVRThreshold.Location = New System.Drawing.Point(6, 35)
        Me.lblCalcSVRThreshold.Name = "lblCalcSVRThreshold"
        Me.lblCalcSVRThreshold.Size = New System.Drawing.Size(65, 27)
        Me.lblCalcSVRThreshold.TabIndex = 0
        Me.lblCalcSVRThreshold.Text = "SVR Threshold:"
        '
        'lblCalcAvgPrice
        '
        Me.lblCalcAvgPrice.Location = New System.Drawing.Point(131, 35)
        Me.lblCalcAvgPrice.Name = "lblCalcAvgPrice"
        Me.lblCalcAvgPrice.Size = New System.Drawing.Size(41, 27)
        Me.lblCalcAvgPrice.TabIndex = 2
        Me.lblCalcAvgPrice.Text = "Avg Days:"
        Me.lblCalcAvgPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCalcIPHThreshold
        '
        Me.txtCalcIPHThreshold.Enabled = False
        Me.txtCalcIPHThreshold.Location = New System.Drawing.Point(145, 79)
        Me.txtCalcIPHThreshold.Name = "txtCalcIPHThreshold"
        Me.txtCalcIPHThreshold.Size = New System.Drawing.Size(90, 20)
        Me.txtCalcIPHThreshold.TabIndex = 72
        Me.txtCalcIPHThreshold.TabStop = False
        Me.txtCalcIPHThreshold.Text = "0.00"
        Me.txtCalcIPHThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkCalcProfitThreshold
        '
        Me.chkCalcProfitThreshold.AutoSize = True
        Me.chkCalcProfitThreshold.Location = New System.Drawing.Point(333, 63)
        Me.chkCalcProfitThreshold.Name = "chkCalcProfitThreshold"
        Me.chkCalcProfitThreshold.Size = New System.Drawing.Size(103, 17)
        Me.chkCalcProfitThreshold.TabIndex = 76
        Me.chkCalcProfitThreshold.Text = "Profit Threshold:"
        Me.chkCalcProfitThreshold.ThreeState = True
        Me.chkCalcProfitThreshold.UseVisualStyleBackColor = True
        '
        'chkCalcVolumeThreshold
        '
        Me.chkCalcVolumeThreshold.AutoSize = True
        Me.chkCalcVolumeThreshold.Location = New System.Drawing.Point(239, 63)
        Me.chkCalcVolumeThreshold.Name = "chkCalcVolumeThreshold"
        Me.chkCalcVolumeThreshold.Size = New System.Drawing.Size(97, 17)
        Me.chkCalcVolumeThreshold.TabIndex = 77
        Me.chkCalcVolumeThreshold.Text = "Vol. Threshold:"
        Me.chkCalcVolumeThreshold.UseVisualStyleBackColor = True
        '
        'chkCalcIPHThreshold
        '
        Me.chkCalcIPHThreshold.AutoSize = True
        Me.chkCalcIPHThreshold.Location = New System.Drawing.Point(145, 63)
        Me.chkCalcIPHThreshold.Name = "chkCalcIPHThreshold"
        Me.chkCalcIPHThreshold.Size = New System.Drawing.Size(97, 17)
        Me.chkCalcIPHThreshold.TabIndex = 75
        Me.chkCalcIPHThreshold.Text = "IPH Threshold:"
        Me.chkCalcIPHThreshold.UseVisualStyleBackColor = True
        '
        'chkCalcSVRIncludeNull
        '
        Me.chkCalcSVRIncludeNull.AutoSize = True
        Me.chkCalcSVRIncludeNull.Location = New System.Drawing.Point(9, 63)
        Me.chkCalcSVRIncludeNull.Name = "chkCalcSVRIncludeNull"
        Me.chkCalcSVRIncludeNull.Size = New System.Drawing.Size(136, 17)
        Me.chkCalcSVRIncludeNull.TabIndex = 6
        Me.chkCalcSVRIncludeNull.Text = "Include Items w/o SVR"
        Me.chkCalcSVRIncludeNull.UseVisualStyleBackColor = True
        '
        'btnCalcCalculate
        '
        Me.btnCalcCalculate.Location = New System.Drawing.Point(1018, 49)
        Me.btnCalcCalculate.Name = "btnCalcCalculate"
        Me.btnCalcCalculate.Size = New System.Drawing.Size(98, 29)
        Me.btnCalcCalculate.TabIndex = 21
        Me.btnCalcCalculate.Text = "Calculate"
        Me.btnCalcCalculate.UseVisualStyleBackColor = True
        '
        'btnCalcSelectColumns
        '
        Me.btnCalcSelectColumns.Location = New System.Drawing.Point(1018, 107)
        Me.btnCalcSelectColumns.Name = "btnCalcSelectColumns"
        Me.btnCalcSelectColumns.Size = New System.Drawing.Size(98, 29)
        Me.btnCalcSelectColumns.TabIndex = 23
        Me.btnCalcSelectColumns.Text = "Select Columns"
        Me.btnCalcSelectColumns.UseVisualStyleBackColor = True
        '
        'gbCalcSizeLimit
        '
        Me.gbCalcSizeLimit.Controls.Add(Me.chkCalcXL)
        Me.gbCalcSizeLimit.Controls.Add(Me.chkCalcLarge)
        Me.gbCalcSizeLimit.Controls.Add(Me.chkCalcMedium)
        Me.gbCalcSizeLimit.Controls.Add(Me.chkCalcSmall)
        Me.gbCalcSizeLimit.Location = New System.Drawing.Point(6, 81)
        Me.gbCalcSizeLimit.Name = "gbCalcSizeLimit"
        Me.gbCalcSizeLimit.Size = New System.Drawing.Size(148, 38)
        Me.gbCalcSizeLimit.TabIndex = 3
        Me.gbCalcSizeLimit.TabStop = False
        Me.gbCalcSizeLimit.Text = "Size Limit"
        '
        'chkCalcXL
        '
        Me.chkCalcXL.AutoSize = True
        Me.chkCalcXL.Location = New System.Drawing.Point(109, 17)
        Me.chkCalcXL.Name = "chkCalcXL"
        Me.chkCalcXL.Size = New System.Drawing.Size(39, 17)
        Me.chkCalcXL.TabIndex = 3
        Me.chkCalcXL.Text = "XL"
        Me.chkCalcXL.UseVisualStyleBackColor = True
        '
        'chkCalcLarge
        '
        Me.chkCalcLarge.AutoSize = True
        Me.chkCalcLarge.Location = New System.Drawing.Point(77, 17)
        Me.chkCalcLarge.Name = "chkCalcLarge"
        Me.chkCalcLarge.Size = New System.Drawing.Size(32, 17)
        Me.chkCalcLarge.TabIndex = 2
        Me.chkCalcLarge.Text = "L"
        Me.chkCalcLarge.UseVisualStyleBackColor = True
        '
        'chkCalcMedium
        '
        Me.chkCalcMedium.AutoSize = True
        Me.chkCalcMedium.Location = New System.Drawing.Point(42, 17)
        Me.chkCalcMedium.Name = "chkCalcMedium"
        Me.chkCalcMedium.Size = New System.Drawing.Size(35, 17)
        Me.chkCalcMedium.TabIndex = 1
        Me.chkCalcMedium.Text = "M"
        Me.chkCalcMedium.UseVisualStyleBackColor = True
        '
        'chkCalcSmall
        '
        Me.chkCalcSmall.AutoSize = True
        Me.chkCalcSmall.Location = New System.Drawing.Point(9, 17)
        Me.chkCalcSmall.Name = "chkCalcSmall"
        Me.chkCalcSmall.Size = New System.Drawing.Size(33, 17)
        Me.chkCalcSmall.TabIndex = 0
        Me.chkCalcSmall.Text = "S"
        Me.chkCalcSmall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkCalcSmall.UseVisualStyleBackColor = True
        '
        'gbCalcProdLines
        '
        Me.gbCalcProdLines.Controls.Add(Me.chkCalcAutoCalcT2NumBPs)
        Me.gbCalcProdLines.Controls.Add(Me.lblCalcBPs)
        Me.gbCalcProdLines.Controls.Add(Me.txtCalcNumBPs)
        Me.gbCalcProdLines.Controls.Add(Me.txtCalcRuns)
        Me.gbCalcProdLines.Controls.Add(Me.txtCalcLabLines)
        Me.gbCalcProdLines.Controls.Add(Me.lblCalcRuns)
        Me.gbCalcProdLines.Controls.Add(Me.lblCalcLabLines1)
        Me.gbCalcProdLines.Controls.Add(Me.lblCalcProdLines1)
        Me.gbCalcProdLines.Controls.Add(Me.txtCalcProdLines)
        Me.gbCalcProdLines.Location = New System.Drawing.Point(859, 15)
        Me.gbCalcProdLines.Name = "gbCalcProdLines"
        Me.gbCalcProdLines.Size = New System.Drawing.Size(156, 72)
        Me.gbCalcProdLines.TabIndex = 15
        Me.gbCalcProdLines.TabStop = False
        Me.gbCalcProdLines.Text = "Runs / Lines:"
        '
        'chkCalcAutoCalcT2NumBPs
        '
        Me.chkCalcAutoCalcT2NumBPs.AutoSize = True
        Me.chkCalcAutoCalcT2NumBPs.Location = New System.Drawing.Point(12, 52)
        Me.chkCalcAutoCalcT2NumBPs.Name = "chkCalcAutoCalcT2NumBPs"
        Me.chkCalcAutoCalcT2NumBPs.Size = New System.Drawing.Size(135, 17)
        Me.chkCalcAutoCalcT2NumBPs.TabIndex = 8
        Me.chkCalcAutoCalcT2NumBPs.Text = "Auto Calc T2 Num BPs"
        Me.chkCalcAutoCalcT2NumBPs.UseVisualStyleBackColor = True
        '
        'lblCalcBPs
        '
        Me.lblCalcBPs.AutoSize = True
        Me.lblCalcBPs.Location = New System.Drawing.Point(44, 13)
        Me.lblCalcBPs.Name = "lblCalcBPs"
        Me.lblCalcBPs.Size = New System.Drawing.Size(29, 13)
        Me.lblCalcBPs.TabIndex = 2
        Me.lblCalcBPs.Text = "BPs:"
        '
        'txtCalcNumBPs
        '
        Me.txtCalcNumBPs.Location = New System.Drawing.Point(45, 28)
        Me.txtCalcNumBPs.MaxLength = 3
        Me.txtCalcNumBPs.Name = "txtCalcNumBPs"
        Me.txtCalcNumBPs.Size = New System.Drawing.Size(32, 20)
        Me.txtCalcNumBPs.TabIndex = 3
        Me.txtCalcNumBPs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCalcRuns
        '
        Me.txtCalcRuns.Location = New System.Drawing.Point(9, 28)
        Me.txtCalcRuns.MaxLength = 4
        Me.txtCalcRuns.Name = "txtCalcRuns"
        Me.txtCalcRuns.Size = New System.Drawing.Size(32, 20)
        Me.txtCalcRuns.TabIndex = 1
        Me.txtCalcRuns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCalcLabLines
        '
        Me.txtCalcLabLines.Location = New System.Drawing.Point(115, 28)
        Me.txtCalcLabLines.MaxLength = 3
        Me.txtCalcLabLines.Name = "txtCalcLabLines"
        Me.txtCalcLabLines.Size = New System.Drawing.Size(32, 20)
        Me.txtCalcLabLines.TabIndex = 7
        Me.txtCalcLabLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCalcRuns
        '
        Me.lblCalcRuns.AutoSize = True
        Me.lblCalcRuns.Location = New System.Drawing.Point(8, 13)
        Me.lblCalcRuns.Name = "lblCalcRuns"
        Me.lblCalcRuns.Size = New System.Drawing.Size(35, 13)
        Me.lblCalcRuns.TabIndex = 0
        Me.lblCalcRuns.Text = "Runs:"
        '
        'lblCalcLabLines1
        '
        Me.lblCalcLabLines1.AutoSize = True
        Me.lblCalcLabLines1.Location = New System.Drawing.Point(113, 13)
        Me.lblCalcLabLines1.Name = "lblCalcLabLines1"
        Me.lblCalcLabLines1.Size = New System.Drawing.Size(33, 13)
        Me.lblCalcLabLines1.TabIndex = 6
        Me.lblCalcLabLines1.Text = "Labs:"
        '
        'lblCalcProdLines1
        '
        Me.lblCalcProdLines1.AutoSize = True
        Me.lblCalcProdLines1.Location = New System.Drawing.Point(79, 13)
        Me.lblCalcProdLines1.Name = "lblCalcProdLines1"
        Me.lblCalcProdLines1.Size = New System.Drawing.Size(32, 13)
        Me.lblCalcProdLines1.TabIndex = 4
        Me.lblCalcProdLines1.Text = "Prod:"
        '
        'txtCalcProdLines
        '
        Me.txtCalcProdLines.Location = New System.Drawing.Point(80, 28)
        Me.txtCalcProdLines.MaxLength = 3
        Me.txtCalcProdLines.Name = "txtCalcProdLines"
        Me.txtCalcProdLines.Size = New System.Drawing.Size(32, 20)
        Me.txtCalcProdLines.TabIndex = 5
        Me.txtCalcProdLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'gbCalcCompareType
        '
        Me.gbCalcCompareType.Controls.Add(Me.chkCalcPPU)
        Me.gbCalcCompareType.Controls.Add(Me.rbtnCalcCompareBuildBuy)
        Me.gbCalcCompareType.Controls.Add(Me.rbtnCalcCompareRawMats)
        Me.gbCalcCompareType.Controls.Add(Me.rbtnCalcCompareComponents)
        Me.gbCalcCompareType.Controls.Add(Me.rbtnCalcCompareAll)
        Me.gbCalcCompareType.Location = New System.Drawing.Point(859, 89)
        Me.gbCalcCompareType.Name = "gbCalcCompareType"
        Me.gbCalcCompareType.Size = New System.Drawing.Size(156, 110)
        Me.gbCalcCompareType.TabIndex = 16
        Me.gbCalcCompareType.TabStop = False
        Me.gbCalcCompareType.Text = "Price Comparison:"
        '
        'chkCalcPPU
        '
        Me.chkCalcPPU.AutoSize = True
        Me.chkCalcPPU.Location = New System.Drawing.Point(9, 88)
        Me.chkCalcPPU.Name = "chkCalcPPU"
        Me.chkCalcPPU.Size = New System.Drawing.Size(137, 17)
        Me.chkCalcPPU.TabIndex = 9
        Me.chkCalcPPU.Text = "Calculate Price per Unit"
        Me.chkCalcPPU.UseVisualStyleBackColor = True
        '
        'rbtnCalcCompareBuildBuy
        '
        Me.rbtnCalcCompareBuildBuy.AutoSize = True
        Me.rbtnCalcCompareBuildBuy.Location = New System.Drawing.Point(9, 32)
        Me.rbtnCalcCompareBuildBuy.Name = "rbtnCalcCompareBuildBuy"
        Me.rbtnCalcCompareBuildBuy.Size = New System.Drawing.Size(116, 17)
        Me.rbtnCalcCompareBuildBuy.TabIndex = 1
        Me.rbtnCalcCompareBuildBuy.Text = "Compare Build/Buy"
        Me.rbtnCalcCompareBuildBuy.UseVisualStyleBackColor = True
        '
        'rbtnCalcCompareRawMats
        '
        Me.rbtnCalcCompareRawMats.AutoSize = True
        Me.rbtnCalcCompareRawMats.Location = New System.Drawing.Point(9, 49)
        Me.rbtnCalcCompareRawMats.Name = "rbtnCalcCompareRawMats"
        Me.rbtnCalcCompareRawMats.Size = New System.Drawing.Size(137, 17)
        Me.rbtnCalcCompareRawMats.TabIndex = 2
        Me.rbtnCalcCompareRawMats.Text = "Compare Raw Materials"
        Me.rbtnCalcCompareRawMats.UseVisualStyleBackColor = True
        '
        'rbtnCalcCompareComponents
        '
        Me.rbtnCalcCompareComponents.AutoSize = True
        Me.rbtnCalcCompareComponents.Location = New System.Drawing.Point(9, 66)
        Me.rbtnCalcCompareComponents.Name = "rbtnCalcCompareComponents"
        Me.rbtnCalcCompareComponents.Size = New System.Drawing.Size(129, 17)
        Me.rbtnCalcCompareComponents.TabIndex = 3
        Me.rbtnCalcCompareComponents.Text = "Compare Components"
        Me.rbtnCalcCompareComponents.UseVisualStyleBackColor = True
        '
        'rbtnCalcCompareAll
        '
        Me.rbtnCalcCompareAll.AutoSize = True
        Me.rbtnCalcCompareAll.Location = New System.Drawing.Point(9, 15)
        Me.rbtnCalcCompareAll.Name = "rbtnCalcCompareAll"
        Me.rbtnCalcCompareAll.Size = New System.Drawing.Size(81, 17)
        Me.rbtnCalcCompareAll.TabIndex = 0
        Me.rbtnCalcCompareAll.Text = "Compare All"
        Me.rbtnCalcCompareAll.UseVisualStyleBackColor = True
        '
        'gbCalcTextColors
        '
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcColorCode6)
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcText)
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcColorCode3)
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcColorCode4)
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcColorCode5)
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcColorCode2)
        Me.gbCalcTextColors.Controls.Add(Me.lblCalcColorCode1)
        Me.gbCalcTextColors.Location = New System.Drawing.Point(6, 116)
        Me.gbCalcTextColors.Name = "gbCalcTextColors"
        Me.gbCalcTextColors.Size = New System.Drawing.Size(148, 30)
        Me.gbCalcTextColors.TabIndex = 5
        Me.gbCalcTextColors.TabStop = False
        '
        'lblCalcColorCode6
        '
        Me.lblCalcColorCode6.BackColor = System.Drawing.Color.LightGreen
        Me.lblCalcColorCode6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcColorCode6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCalcColorCode6.Location = New System.Drawing.Point(55, 10)
        Me.lblCalcColorCode6.Name = "lblCalcColorCode6"
        Me.lblCalcColorCode6.Size = New System.Drawing.Size(15, 15)
        Me.lblCalcColorCode6.TabIndex = 5
        Me.lblCalcColorCode6.Text = "T"
        Me.lblCalcColorCode6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalcText
        '
        Me.lblCalcText.AutoSize = True
        Me.lblCalcText.Location = New System.Drawing.Point(6, 11)
        Me.lblCalcText.Name = "lblCalcText"
        Me.lblCalcText.Size = New System.Drawing.Size(31, 13)
        Me.lblCalcText.TabIndex = 0
        Me.lblCalcText.Text = "Text:"
        '
        'lblCalcColorCode3
        '
        Me.lblCalcColorCode3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcColorCode3.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblCalcColorCode3.Location = New System.Drawing.Point(127, 10)
        Me.lblCalcColorCode3.Name = "lblCalcColorCode3"
        Me.lblCalcColorCode3.Size = New System.Drawing.Size(15, 15)
        Me.lblCalcColorCode3.TabIndex = 2
        Me.lblCalcColorCode3.Text = "T"
        Me.lblCalcColorCode3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalcColorCode4
        '
        Me.lblCalcColorCode4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcColorCode4.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblCalcColorCode4.Location = New System.Drawing.Point(109, 10)
        Me.lblCalcColorCode4.Name = "lblCalcColorCode4"
        Me.lblCalcColorCode4.Size = New System.Drawing.Size(15, 15)
        Me.lblCalcColorCode4.TabIndex = 3
        Me.lblCalcColorCode4.Text = "T"
        Me.lblCalcColorCode4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalcColorCode5
        '
        Me.lblCalcColorCode5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcColorCode5.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCalcColorCode5.Location = New System.Drawing.Point(91, 10)
        Me.lblCalcColorCode5.Name = "lblCalcColorCode5"
        Me.lblCalcColorCode5.Size = New System.Drawing.Size(15, 15)
        Me.lblCalcColorCode5.TabIndex = 4
        Me.lblCalcColorCode5.Text = "T"
        Me.lblCalcColorCode5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalcColorCode2
        '
        Me.lblCalcColorCode2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblCalcColorCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcColorCode2.ForeColor = System.Drawing.Color.Black
        Me.lblCalcColorCode2.Location = New System.Drawing.Point(73, 10)
        Me.lblCalcColorCode2.Name = "lblCalcColorCode2"
        Me.lblCalcColorCode2.Size = New System.Drawing.Size(15, 15)
        Me.lblCalcColorCode2.TabIndex = 1
        Me.lblCalcColorCode2.Text = "T"
        Me.lblCalcColorCode2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalcColorCode1
        '
        Me.lblCalcColorCode1.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.lblCalcColorCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcColorCode1.Location = New System.Drawing.Point(37, 10)
        Me.lblCalcColorCode1.Name = "lblCalcColorCode1"
        Me.lblCalcColorCode1.Size = New System.Drawing.Size(15, 15)
        Me.lblCalcColorCode1.TabIndex = 0
        Me.lblCalcColorCode1.Text = "T"
        Me.lblCalcColorCode1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbCalcInvention
        '
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptorforT3)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptorforT2)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor0)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor9)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor8)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor7)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor6)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor5)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor4)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor3)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor2)
        Me.gbCalcInvention.Controls.Add(Me.chkCalcDecryptor1)
        Me.gbCalcInvention.Controls.Add(Me.lblCalcDecryptorUse)
        Me.gbCalcInvention.Location = New System.Drawing.Point(6, 147)
        Me.gbCalcInvention.Name = "gbCalcInvention"
        Me.gbCalcInvention.Size = New System.Drawing.Size(400, 52)
        Me.gbCalcInvention.TabIndex = 6
        Me.gbCalcInvention.TabStop = False
        Me.gbCalcInvention.Text = "Invention Decryptors (Probability Multiplier):"
        '
        'chkCalcDecryptorforT3
        '
        Me.chkCalcDecryptorforT3.AutoSize = True
        Me.chkCalcDecryptorforT3.Location = New System.Drawing.Point(354, 30)
        Me.chkCalcDecryptorforT3.Name = "chkCalcDecryptorforT3"
        Me.chkCalcDecryptorforT3.Size = New System.Drawing.Size(39, 17)
        Me.chkCalcDecryptorforT3.TabIndex = 12
        Me.chkCalcDecryptorforT3.Text = "T3"
        Me.chkCalcDecryptorforT3.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptorforT2
        '
        Me.chkCalcDecryptorforT2.AutoSize = True
        Me.chkCalcDecryptorforT2.Location = New System.Drawing.Point(354, 14)
        Me.chkCalcDecryptorforT2.Name = "chkCalcDecryptorforT2"
        Me.chkCalcDecryptorforT2.Size = New System.Drawing.Size(39, 17)
        Me.chkCalcDecryptorforT2.TabIndex = 11
        Me.chkCalcDecryptorforT2.Text = "T2"
        Me.chkCalcDecryptorforT2.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor0
        '
        Me.chkCalcDecryptor0.AutoSize = True
        Me.chkCalcDecryptor0.Location = New System.Drawing.Point(9, 30)
        Me.chkCalcDecryptor0.Name = "chkCalcDecryptor0"
        Me.chkCalcDecryptor0.Size = New System.Drawing.Size(61, 17)
        Me.chkCalcDecryptor0.TabIndex = 1
        Me.chkCalcDecryptor0.Text = "Optimal"
        Me.chkCalcDecryptor0.ThreeState = True
        Me.chkCalcDecryptor0.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor9
        '
        Me.chkCalcDecryptor9.AutoSize = True
        Me.chkCalcDecryptor9.Location = New System.Drawing.Point(267, 30)
        Me.chkCalcDecryptor9.Name = "chkCalcDecryptor9"
        Me.chkCalcDecryptor9.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor9.TabIndex = 9
        Me.chkCalcDecryptor9.Text = "1.9x"
        Me.chkCalcDecryptor9.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor8
        '
        Me.chkCalcDecryptor8.AutoSize = True
        Me.chkCalcDecryptor8.Location = New System.Drawing.Point(211, 29)
        Me.chkCalcDecryptor8.Name = "chkCalcDecryptor8"
        Me.chkCalcDecryptor8.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor8.TabIndex = 7
        Me.chkCalcDecryptor8.Text = "1.8x"
        Me.chkCalcDecryptor8.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor7
        '
        Me.chkCalcDecryptor7.AutoSize = True
        Me.chkCalcDecryptor7.Location = New System.Drawing.Point(155, 30)
        Me.chkCalcDecryptor7.Name = "chkCalcDecryptor7"
        Me.chkCalcDecryptor7.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor7.TabIndex = 5
        Me.chkCalcDecryptor7.Text = "1.5x"
        Me.chkCalcDecryptor7.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor6
        '
        Me.chkCalcDecryptor6.AutoSize = True
        Me.chkCalcDecryptor6.Location = New System.Drawing.Point(99, 30)
        Me.chkCalcDecryptor6.Name = "chkCalcDecryptor6"
        Me.chkCalcDecryptor6.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor6.TabIndex = 3
        Me.chkCalcDecryptor6.Text = "1.2x"
        Me.chkCalcDecryptor6.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor5
        '
        Me.chkCalcDecryptor5.AutoSize = True
        Me.chkCalcDecryptor5.Location = New System.Drawing.Point(267, 14)
        Me.chkCalcDecryptor5.Name = "chkCalcDecryptor5"
        Me.chkCalcDecryptor5.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor5.TabIndex = 8
        Me.chkCalcDecryptor5.Text = "1.1x"
        Me.chkCalcDecryptor5.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor4
        '
        Me.chkCalcDecryptor4.AutoSize = True
        Me.chkCalcDecryptor4.Location = New System.Drawing.Point(211, 13)
        Me.chkCalcDecryptor4.Name = "chkCalcDecryptor4"
        Me.chkCalcDecryptor4.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor4.TabIndex = 6
        Me.chkCalcDecryptor4.Text = "1.0x"
        Me.chkCalcDecryptor4.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor3
        '
        Me.chkCalcDecryptor3.AutoSize = True
        Me.chkCalcDecryptor3.Location = New System.Drawing.Point(155, 14)
        Me.chkCalcDecryptor3.Name = "chkCalcDecryptor3"
        Me.chkCalcDecryptor3.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor3.TabIndex = 4
        Me.chkCalcDecryptor3.Text = "0.9x"
        Me.chkCalcDecryptor3.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor2
        '
        Me.chkCalcDecryptor2.AutoSize = True
        Me.chkCalcDecryptor2.Location = New System.Drawing.Point(99, 14)
        Me.chkCalcDecryptor2.Name = "chkCalcDecryptor2"
        Me.chkCalcDecryptor2.Size = New System.Drawing.Size(46, 17)
        Me.chkCalcDecryptor2.TabIndex = 2
        Me.chkCalcDecryptor2.Text = "0.6x"
        Me.chkCalcDecryptor2.UseVisualStyleBackColor = True
        '
        'chkCalcDecryptor1
        '
        Me.chkCalcDecryptor1.AutoSize = True
        Me.chkCalcDecryptor1.Location = New System.Drawing.Point(9, 14)
        Me.chkCalcDecryptor1.Name = "chkCalcDecryptor1"
        Me.chkCalcDecryptor1.Size = New System.Drawing.Size(52, 17)
        Me.chkCalcDecryptor1.TabIndex = 0
        Me.chkCalcDecryptor1.Text = "None"
        Me.chkCalcDecryptor1.UseVisualStyleBackColor = True
        '
        'lblCalcDecryptorUse
        '
        Me.lblCalcDecryptorUse.Location = New System.Drawing.Point(319, 13)
        Me.lblCalcDecryptorUse.Name = "lblCalcDecryptorUse"
        Me.lblCalcDecryptorUse.Size = New System.Drawing.Size(36, 33)
        Me.lblCalcDecryptorUse.TabIndex = 10
        Me.lblCalcDecryptorUse.Text = "Use For:"
        Me.lblCalcDecryptorUse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbCalcBPRace
        '
        Me.gbCalcBPRace.Controls.Add(Me.chkCalcRaceOther)
        Me.gbCalcBPRace.Controls.Add(Me.chkCalcRacePirate)
        Me.gbCalcBPRace.Controls.Add(Me.chkCalcRaceMinmatar)
        Me.gbCalcBPRace.Controls.Add(Me.chkCalcRaceGallente)
        Me.gbCalcBPRace.Controls.Add(Me.chkCalcRaceCaldari)
        Me.gbCalcBPRace.Controls.Add(Me.chkCalcRaceAmarr)
        Me.gbCalcBPRace.Location = New System.Drawing.Point(159, 81)
        Me.gbCalcBPRace.Name = "gbCalcBPRace"
        Me.gbCalcBPRace.Size = New System.Drawing.Size(192, 65)
        Me.gbCalcBPRace.TabIndex = 4
        Me.gbCalcBPRace.TabStop = False
        Me.gbCalcBPRace.Text = "BP Race"
        '
        'chkCalcRaceOther
        '
        Me.chkCalcRaceOther.AutoSize = True
        Me.chkCalcRaceOther.Location = New System.Drawing.Point(136, 40)
        Me.chkCalcRaceOther.Name = "chkCalcRaceOther"
        Me.chkCalcRaceOther.Size = New System.Drawing.Size(52, 17)
        Me.chkCalcRaceOther.TabIndex = 5
        Me.chkCalcRaceOther.Text = "Other"
        Me.chkCalcRaceOther.UseVisualStyleBackColor = True
        '
        'chkCalcRacePirate
        '
        Me.chkCalcRacePirate.AutoSize = True
        Me.chkCalcRacePirate.Location = New System.Drawing.Point(136, 17)
        Me.chkCalcRacePirate.Name = "chkCalcRacePirate"
        Me.chkCalcRacePirate.Size = New System.Drawing.Size(53, 17)
        Me.chkCalcRacePirate.TabIndex = 2
        Me.chkCalcRacePirate.Text = "Pirate"
        Me.chkCalcRacePirate.UseVisualStyleBackColor = True
        '
        'chkCalcRaceMinmatar
        '
        Me.chkCalcRaceMinmatar.AutoSize = True
        Me.chkCalcRaceMinmatar.Location = New System.Drawing.Point(69, 40)
        Me.chkCalcRaceMinmatar.Name = "chkCalcRaceMinmatar"
        Me.chkCalcRaceMinmatar.Size = New System.Drawing.Size(69, 17)
        Me.chkCalcRaceMinmatar.TabIndex = 4
        Me.chkCalcRaceMinmatar.Text = "Minmatar"
        Me.chkCalcRaceMinmatar.UseVisualStyleBackColor = True
        '
        'chkCalcRaceGallente
        '
        Me.chkCalcRaceGallente.AutoSize = True
        Me.chkCalcRaceGallente.Location = New System.Drawing.Point(6, 40)
        Me.chkCalcRaceGallente.Name = "chkCalcRaceGallente"
        Me.chkCalcRaceGallente.Size = New System.Drawing.Size(65, 17)
        Me.chkCalcRaceGallente.TabIndex = 3
        Me.chkCalcRaceGallente.Text = "Gallente"
        Me.chkCalcRaceGallente.UseVisualStyleBackColor = True
        '
        'chkCalcRaceCaldari
        '
        Me.chkCalcRaceCaldari.AutoSize = True
        Me.chkCalcRaceCaldari.Location = New System.Drawing.Point(69, 17)
        Me.chkCalcRaceCaldari.Name = "chkCalcRaceCaldari"
        Me.chkCalcRaceCaldari.Size = New System.Drawing.Size(58, 17)
        Me.chkCalcRaceCaldari.TabIndex = 1
        Me.chkCalcRaceCaldari.Text = "Caldari"
        Me.chkCalcRaceCaldari.UseVisualStyleBackColor = True
        '
        'chkCalcRaceAmarr
        '
        Me.chkCalcRaceAmarr.AutoSize = True
        Me.chkCalcRaceAmarr.Location = New System.Drawing.Point(6, 17)
        Me.chkCalcRaceAmarr.Name = "chkCalcRaceAmarr"
        Me.chkCalcRaceAmarr.Size = New System.Drawing.Size(53, 17)
        Me.chkCalcRaceAmarr.TabIndex = 0
        Me.chkCalcRaceAmarr.Text = "Amarr"
        Me.chkCalcRaceAmarr.UseVisualStyleBackColor = True
        '
        'gbTempMEPE
        '
        Me.gbTempMEPE.Controls.Add(Me.txtCalcTempTE)
        Me.gbTempMEPE.Controls.Add(Me.lblTempPE)
        Me.gbTempMEPE.Controls.Add(Me.txtCalcTempME)
        Me.gbTempMEPE.Controls.Add(Me.lblTempME)
        Me.gbTempMEPE.Location = New System.Drawing.Point(273, 254)
        Me.gbTempMEPE.Name = "gbTempMEPE"
        Me.gbTempMEPE.Size = New System.Drawing.Size(133, 40)
        Me.gbTempMEPE.TabIndex = 11
        Me.gbTempMEPE.TabStop = False
        Me.gbTempMEPE.Text = "Unowned BPs:"
        '
        'txtCalcTempTE
        '
        Me.txtCalcTempTE.Location = New System.Drawing.Point(97, 13)
        Me.txtCalcTempTE.Name = "txtCalcTempTE"
        Me.txtCalcTempTE.Size = New System.Drawing.Size(29, 20)
        Me.txtCalcTempTE.TabIndex = 3
        '
        'lblTempPE
        '
        Me.lblTempPE.AutoSize = True
        Me.lblTempPE.Location = New System.Drawing.Point(70, 17)
        Me.lblTempPE.Name = "lblTempPE"
        Me.lblTempPE.Size = New System.Drawing.Size(24, 13)
        Me.lblTempPE.TabIndex = 2
        Me.lblTempPE.Text = "TE:"
        '
        'txtCalcTempME
        '
        Me.txtCalcTempME.Location = New System.Drawing.Point(37, 13)
        Me.txtCalcTempME.Name = "txtCalcTempME"
        Me.txtCalcTempME.Size = New System.Drawing.Size(29, 20)
        Me.txtCalcTempME.TabIndex = 1
        '
        'lblTempME
        '
        Me.lblTempME.AutoSize = True
        Me.lblTempME.Location = New System.Drawing.Point(13, 17)
        Me.lblTempME.Name = "lblTempME"
        Me.lblTempME.Size = New System.Drawing.Size(26, 13)
        Me.lblTempME.TabIndex = 0
        Me.lblTempME.Text = "ME:"
        '
        'tabCalcFacilities
        '
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityBase)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityComponents)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityCopy)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityT2Invention)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityT3Invention)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilitySupers)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityCapitals)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityT3Ships)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilitySubsystems)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityBoosters)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityReactions)
        Me.tabCalcFacilities.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tabCalcFacilities.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.tabCalcFacilities.HotTrack = True
        Me.tabCalcFacilities.ItemSize = New System.Drawing.Size(49, 20)
        Me.tabCalcFacilities.Location = New System.Drawing.Point(546, 13)
        Me.tabCalcFacilities.Multiline = True
        Me.tabCalcFacilities.Name = "tabCalcFacilities"
        Me.tabCalcFacilities.Padding = New System.Drawing.Point(0, 0)
        Me.tabCalcFacilities.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tabCalcFacilities.SelectedIndex = 0
        Me.tabCalcFacilities.Size = New System.Drawing.Size(310, 176)
        Me.tabCalcFacilities.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tabCalcFacilities.TabIndex = 13
        '
        'tabCalcFacilityBase
        '
        Me.tabCalcFacilityBase.Controls.Add(Me.CalcBaseFacility)
        Me.tabCalcFacilityBase.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityBase.Margin = New System.Windows.Forms.Padding(0)
        Me.tabCalcFacilityBase.Name = "tabCalcFacilityBase"
        Me.tabCalcFacilityBase.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCalcFacilityBase.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityBase.TabIndex = 1
        Me.tabCalcFacilityBase.Text = "Base"
        Me.tabCalcFacilityBase.UseVisualStyleBackColor = True
        '
        'CalcBaseFacility
        '
        Me.CalcBaseFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcBaseFacility.Name = "CalcBaseFacility"
        Me.CalcBaseFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcBaseFacility.TabIndex = 0
        '
        'tabCalcFacilityComponents
        '
        Me.tabCalcFacilityComponents.Controls.Add(Me.CalcComponentsFacility)
        Me.tabCalcFacilityComponents.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityComponents.Name = "tabCalcFacilityComponents"
        Me.tabCalcFacilityComponents.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityComponents.TabIndex = 10
        Me.tabCalcFacilityComponents.Text = "Components"
        Me.tabCalcFacilityComponents.UseVisualStyleBackColor = True
        '
        'CalcComponentsFacility
        '
        Me.CalcComponentsFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcComponentsFacility.Name = "CalcComponentsFacility"
        Me.CalcComponentsFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcComponentsFacility.TabIndex = 1
        '
        'tabCalcFacilityCopy
        '
        Me.tabCalcFacilityCopy.Controls.Add(Me.CalcCopyFacility)
        Me.tabCalcFacilityCopy.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityCopy.Name = "tabCalcFacilityCopy"
        Me.tabCalcFacilityCopy.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityCopy.TabIndex = 3
        Me.tabCalcFacilityCopy.Text = "Copy"
        Me.tabCalcFacilityCopy.UseVisualStyleBackColor = True
        '
        'CalcCopyFacility
        '
        Me.CalcCopyFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcCopyFacility.Name = "CalcCopyFacility"
        Me.CalcCopyFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcCopyFacility.TabIndex = 1
        '
        'tabCalcFacilityT2Invention
        '
        Me.tabCalcFacilityT2Invention.Controls.Add(Me.CalcInventionFacility)
        Me.tabCalcFacilityT2Invention.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT2Invention.Name = "tabCalcFacilityT2Invention"
        Me.tabCalcFacilityT2Invention.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT2Invention.TabIndex = 2
        Me.tabCalcFacilityT2Invention.Text = "T2 Inv"
        Me.tabCalcFacilityT2Invention.UseVisualStyleBackColor = True
        '
        'CalcInventionFacility
        '
        Me.CalcInventionFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcInventionFacility.Name = "CalcInventionFacility"
        Me.CalcInventionFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcInventionFacility.TabIndex = 1
        '
        'tabCalcFacilityT3Invention
        '
        Me.tabCalcFacilityT3Invention.Controls.Add(Me.CalcT3InventionFacility)
        Me.tabCalcFacilityT3Invention.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT3Invention.Name = "tabCalcFacilityT3Invention"
        Me.tabCalcFacilityT3Invention.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT3Invention.TabIndex = 11
        Me.tabCalcFacilityT3Invention.Text = "T3 Inv"
        Me.tabCalcFacilityT3Invention.UseVisualStyleBackColor = True
        '
        'CalcT3InventionFacility
        '
        Me.CalcT3InventionFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcT3InventionFacility.Name = "CalcT3InventionFacility"
        Me.CalcT3InventionFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcT3InventionFacility.TabIndex = 1
        '
        'tabCalcFacilitySupers
        '
        Me.tabCalcFacilitySupers.Controls.Add(Me.CalcSupersFacility)
        Me.tabCalcFacilitySupers.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilitySupers.Name = "tabCalcFacilitySupers"
        Me.tabCalcFacilitySupers.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilitySupers.TabIndex = 6
        Me.tabCalcFacilitySupers.Text = "Supers"
        Me.tabCalcFacilitySupers.UseVisualStyleBackColor = True
        '
        'CalcSupersFacility
        '
        Me.CalcSupersFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcSupersFacility.Name = "CalcSupersFacility"
        Me.CalcSupersFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcSupersFacility.TabIndex = 1
        '
        'tabCalcFacilityCapitals
        '
        Me.tabCalcFacilityCapitals.Controls.Add(Me.CalcCapitalsFacility)
        Me.tabCalcFacilityCapitals.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityCapitals.Name = "tabCalcFacilityCapitals"
        Me.tabCalcFacilityCapitals.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityCapitals.TabIndex = 5
        Me.tabCalcFacilityCapitals.Text = "Capitals"
        Me.tabCalcFacilityCapitals.UseVisualStyleBackColor = True
        '
        'CalcCapitalsFacility
        '
        Me.CalcCapitalsFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcCapitalsFacility.Name = "CalcCapitalsFacility"
        Me.CalcCapitalsFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcCapitalsFacility.TabIndex = 1
        '
        'tabCalcFacilityT3Ships
        '
        Me.tabCalcFacilityT3Ships.Controls.Add(Me.CalcT3ShipsFacility)
        Me.tabCalcFacilityT3Ships.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT3Ships.Name = "tabCalcFacilityT3Ships"
        Me.tabCalcFacilityT3Ships.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT3Ships.TabIndex = 9
        Me.tabCalcFacilityT3Ships.Text = "T3 Ships"
        Me.tabCalcFacilityT3Ships.UseVisualStyleBackColor = True
        '
        'CalcT3ShipsFacility
        '
        Me.CalcT3ShipsFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcT3ShipsFacility.Name = "CalcT3ShipsFacility"
        Me.CalcT3ShipsFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcT3ShipsFacility.TabIndex = 1
        '
        'tabCalcFacilitySubsystems
        '
        Me.tabCalcFacilitySubsystems.Controls.Add(Me.CalcSubsystemsFacility)
        Me.tabCalcFacilitySubsystems.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilitySubsystems.Name = "tabCalcFacilitySubsystems"
        Me.tabCalcFacilitySubsystems.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilitySubsystems.TabIndex = 8
        Me.tabCalcFacilitySubsystems.Text = "Subsystems"
        Me.tabCalcFacilitySubsystems.UseVisualStyleBackColor = True
        '
        'CalcSubsystemsFacility
        '
        Me.CalcSubsystemsFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcSubsystemsFacility.Name = "CalcSubsystemsFacility"
        Me.CalcSubsystemsFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcSubsystemsFacility.TabIndex = 1
        '
        'tabCalcFacilityBoosters
        '
        Me.tabCalcFacilityBoosters.Controls.Add(Me.CalcBoostersFacility)
        Me.tabCalcFacilityBoosters.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityBoosters.Name = "tabCalcFacilityBoosters"
        Me.tabCalcFacilityBoosters.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityBoosters.TabIndex = 7
        Me.tabCalcFacilityBoosters.Text = "Boosters"
        Me.tabCalcFacilityBoosters.UseVisualStyleBackColor = True
        '
        'CalcBoostersFacility
        '
        Me.CalcBoostersFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcBoostersFacility.Name = "CalcBoostersFacility"
        Me.CalcBoostersFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcBoostersFacility.TabIndex = 1
        '
        'tabCalcFacilityReactions
        '
        Me.tabCalcFacilityReactions.Controls.Add(Me.CalcReactionsFacility)
        Me.tabCalcFacilityReactions.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityReactions.Name = "tabCalcFacilityReactions"
        Me.tabCalcFacilityReactions.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityReactions.TabIndex = 4
        Me.tabCalcFacilityReactions.Text = "Reactions"
        Me.tabCalcFacilityReactions.UseVisualStyleBackColor = True
        '
        'CalcReactionsFacility
        '
        Me.CalcReactionsFacility.Location = New System.Drawing.Point(0, 0)
        Me.CalcReactionsFacility.Name = "CalcReactionsFacility"
        Me.CalcReactionsFacility.Size = New System.Drawing.Size(303, 128)
        Me.CalcReactionsFacility.TabIndex = 1
        '
        'gbCalcFilter
        '
        Me.gbCalcFilter.Controls.Add(Me.cmbCalcBPTypeFilter)
        Me.gbCalcFilter.Location = New System.Drawing.Point(6, 201)
        Me.gbCalcFilter.Name = "gbCalcFilter"
        Me.gbCalcFilter.Size = New System.Drawing.Size(210, 49)
        Me.gbCalcFilter.TabIndex = 8
        Me.gbCalcFilter.TabStop = False
        Me.gbCalcFilter.Text = "Item Type Filter:"
        '
        'cmbCalcBPTypeFilter
        '
        Me.cmbCalcBPTypeFilter.FormattingEnabled = True
        Me.cmbCalcBPTypeFilter.Location = New System.Drawing.Point(9, 18)
        Me.cmbCalcBPTypeFilter.Name = "cmbCalcBPTypeFilter"
        Me.cmbCalcBPTypeFilter.Size = New System.Drawing.Size(195, 21)
        Me.cmbCalcBPTypeFilter.TabIndex = 0
        Me.cmbCalcBPTypeFilter.Text = "All Types"
        '
        'gbCalcBPTech
        '
        Me.gbCalcBPTech.Controls.Add(Me.chkCalcPirateFaction)
        Me.gbCalcBPTech.Controls.Add(Me.chkCalcStoryline)
        Me.gbCalcBPTech.Controls.Add(Me.chkCalcNavyFaction)
        Me.gbCalcBPTech.Controls.Add(Me.chkCalcT3)
        Me.gbCalcBPTech.Controls.Add(Me.chkCalcT2)
        Me.gbCalcBPTech.Controls.Add(Me.chkCalcT1)
        Me.gbCalcBPTech.Location = New System.Drawing.Point(209, 15)
        Me.gbCalcBPTech.Name = "gbCalcBPTech"
        Me.gbCalcBPTech.Size = New System.Drawing.Size(142, 65)
        Me.gbCalcBPTech.TabIndex = 2
        Me.gbCalcBPTech.TabStop = False
        Me.gbCalcBPTech.Text = "Tech"
        '
        'chkCalcPirateFaction
        '
        Me.chkCalcPirateFaction.AutoSize = True
        Me.chkCalcPirateFaction.Location = New System.Drawing.Point(76, 45)
        Me.chkCalcPirateFaction.Name = "chkCalcPirateFaction"
        Me.chkCalcPirateFaction.Size = New System.Drawing.Size(53, 17)
        Me.chkCalcPirateFaction.TabIndex = 5
        Me.chkCalcPirateFaction.Text = "Pirate"
        Me.chkCalcPirateFaction.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCalcPirateFaction.UseVisualStyleBackColor = True
        '
        'chkCalcStoryline
        '
        Me.chkCalcStoryline.AutoSize = True
        Me.chkCalcStoryline.Location = New System.Drawing.Point(76, 13)
        Me.chkCalcStoryline.Name = "chkCalcStoryline"
        Me.chkCalcStoryline.Size = New System.Drawing.Size(66, 17)
        Me.chkCalcStoryline.TabIndex = 3
        Me.chkCalcStoryline.Text = "Storyline"
        Me.chkCalcStoryline.UseVisualStyleBackColor = True
        '
        'chkCalcNavyFaction
        '
        Me.chkCalcNavyFaction.AutoSize = True
        Me.chkCalcNavyFaction.Location = New System.Drawing.Point(76, 29)
        Me.chkCalcNavyFaction.Name = "chkCalcNavyFaction"
        Me.chkCalcNavyFaction.Size = New System.Drawing.Size(51, 17)
        Me.chkCalcNavyFaction.TabIndex = 4
        Me.chkCalcNavyFaction.Text = "Navy"
        Me.chkCalcNavyFaction.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCalcNavyFaction.UseVisualStyleBackColor = True
        '
        'chkCalcT3
        '
        Me.chkCalcT3.AutoSize = True
        Me.chkCalcT3.Location = New System.Drawing.Point(14, 46)
        Me.chkCalcT3.Name = "chkCalcT3"
        Me.chkCalcT3.Size = New System.Drawing.Size(60, 17)
        Me.chkCalcT3.TabIndex = 2
        Me.chkCalcT3.Text = "Tech 3"
        Me.chkCalcT3.UseVisualStyleBackColor = True
        '
        'chkCalcT2
        '
        Me.chkCalcT2.AutoSize = True
        Me.chkCalcT2.Location = New System.Drawing.Point(14, 29)
        Me.chkCalcT2.Name = "chkCalcT2"
        Me.chkCalcT2.Size = New System.Drawing.Size(60, 17)
        Me.chkCalcT2.TabIndex = 1
        Me.chkCalcT2.Text = "Tech 2"
        Me.chkCalcT2.UseVisualStyleBackColor = True
        '
        'chkCalcT1
        '
        Me.chkCalcT1.AutoSize = True
        Me.chkCalcT1.Location = New System.Drawing.Point(14, 14)
        Me.chkCalcT1.Name = "chkCalcT1"
        Me.chkCalcT1.Size = New System.Drawing.Size(60, 17)
        Me.chkCalcT1.TabIndex = 0
        Me.chkCalcT1.Text = "Tech 1"
        Me.chkCalcT1.UseVisualStyleBackColor = True
        '
        'gbCalcIncludeOwned
        '
        Me.gbCalcIncludeOwned.Controls.Add(Me.chkCalcIncludeT3Owned)
        Me.gbCalcIncludeOwned.Controls.Add(Me.chkCalcIncludeT2Owned)
        Me.gbCalcIncludeOwned.Location = New System.Drawing.Point(104, 15)
        Me.gbCalcIncludeOwned.Name = "gbCalcIncludeOwned"
        Me.gbCalcIncludeOwned.Size = New System.Drawing.Size(102, 65)
        Me.gbCalcIncludeOwned.TabIndex = 1
        Me.gbCalcIncludeOwned.TabStop = False
        Me.gbCalcIncludeOwned.Text = "Include Owned"
        '
        'chkCalcIncludeT3Owned
        '
        Me.chkCalcIncludeT3Owned.AutoSize = True
        Me.chkCalcIncludeT3Owned.Location = New System.Drawing.Point(8, 44)
        Me.chkCalcIncludeT3Owned.Name = "chkCalcIncludeT3Owned"
        Me.chkCalcIncludeT3Owned.Size = New System.Drawing.Size(84, 17)
        Me.chkCalcIncludeT3Owned.TabIndex = 1
        Me.chkCalcIncludeT3Owned.Text = "T3 Invented"
        Me.chkCalcIncludeT3Owned.UseVisualStyleBackColor = True
        '
        'chkCalcIncludeT2Owned
        '
        Me.chkCalcIncludeT2Owned.AutoSize = True
        Me.chkCalcIncludeT2Owned.Location = New System.Drawing.Point(8, 21)
        Me.chkCalcIncludeT2Owned.Name = "chkCalcIncludeT2Owned"
        Me.chkCalcIncludeT2Owned.Size = New System.Drawing.Size(84, 17)
        Me.chkCalcIncludeT2Owned.TabIndex = 0
        Me.chkCalcIncludeT2Owned.Text = "T2 Invented"
        Me.chkCalcIncludeT2Owned.UseVisualStyleBackColor = True
        '
        'btnCalcSaveSettings
        '
        Me.btnCalcSaveSettings.Location = New System.Drawing.Point(1018, 136)
        Me.btnCalcSaveSettings.Name = "btnCalcSaveSettings"
        Me.btnCalcSaveSettings.Size = New System.Drawing.Size(98, 29)
        Me.btnCalcSaveSettings.TabIndex = 24
        Me.btnCalcSaveSettings.Text = "Save Settings"
        Me.btnCalcSaveSettings.UseVisualStyleBackColor = True
        '
        'btnCalcExportList
        '
        Me.btnCalcExportList.Location = New System.Drawing.Point(1018, 165)
        Me.btnCalcExportList.Name = "btnCalcExportList"
        Me.btnCalcExportList.Size = New System.Drawing.Size(98, 29)
        Me.btnCalcExportList.TabIndex = 25
        Me.btnCalcExportList.Text = "Export Table"
        Me.btnCalcExportList.UseVisualStyleBackColor = True
        '
        'btnCalcPreview
        '
        Me.btnCalcPreview.Location = New System.Drawing.Point(1018, 20)
        Me.btnCalcPreview.Name = "btnCalcPreview"
        Me.btnCalcPreview.Size = New System.Drawing.Size(98, 29)
        Me.btnCalcPreview.TabIndex = 20
        Me.btnCalcPreview.Text = "Preview Item List"
        Me.btnCalcPreview.UseVisualStyleBackColor = True
        '
        'btnCalcReset
        '
        Me.btnCalcReset.Location = New System.Drawing.Point(1018, 78)
        Me.btnCalcReset.Name = "btnCalcReset"
        Me.btnCalcReset.Size = New System.Drawing.Size(98, 29)
        Me.btnCalcReset.TabIndex = 22
        Me.btnCalcReset.Text = "Reset"
        Me.btnCalcReset.UseVisualStyleBackColor = True
        '
        'gbCalcTextFilter
        '
        Me.gbCalcTextFilter.Controls.Add(Me.btnCalcResetTextSearch)
        Me.gbCalcTextFilter.Controls.Add(Me.txtCalcItemFilter)
        Me.gbCalcTextFilter.Location = New System.Drawing.Point(6, 251)
        Me.gbCalcTextFilter.Name = "gbCalcTextFilter"
        Me.gbCalcTextFilter.Size = New System.Drawing.Size(261, 43)
        Me.gbCalcTextFilter.TabIndex = 9
        Me.gbCalcTextFilter.TabStop = False
        Me.gbCalcTextFilter.Text = "Text Item Filter:"
        '
        'btnCalcResetTextSearch
        '
        Me.btnCalcResetTextSearch.Location = New System.Drawing.Point(216, 15)
        Me.btnCalcResetTextSearch.Name = "btnCalcResetTextSearch"
        Me.btnCalcResetTextSearch.Size = New System.Drawing.Size(39, 21)
        Me.btnCalcResetTextSearch.TabIndex = 1
        Me.btnCalcResetTextSearch.Text = "Clear"
        Me.btnCalcResetTextSearch.UseVisualStyleBackColor = True
        '
        'txtCalcItemFilter
        '
        Me.txtCalcItemFilter.Location = New System.Drawing.Point(9, 16)
        Me.txtCalcItemFilter.Name = "txtCalcItemFilter"
        Me.txtCalcItemFilter.Size = New System.Drawing.Size(201, 20)
        Me.txtCalcItemFilter.TabIndex = 0
        '
        'gbCalcBPType
        '
        Me.gbCalcBPType.Controls.Add(Me.chkCalcReactions)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcCelestials)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcMisc)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcSubsystems)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcDeployables)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcStructures)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcStructureRigs)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcBoosters)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcRigs)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcComponents)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcAmmo)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcDrones)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcModules)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcShips)
        Me.gbCalcBPType.Controls.Add(Me.chkCalcStructureModules)
        Me.gbCalcBPType.Location = New System.Drawing.Point(355, 15)
        Me.gbCalcBPType.Name = "gbCalcBPType"
        Me.gbCalcBPType.Size = New System.Drawing.Size(188, 132)
        Me.gbCalcBPType.TabIndex = 5
        Me.gbCalcBPType.TabStop = False
        Me.gbCalcBPType.Text = "Blueprint Type:"
        '
        'chkCalcReactions
        '
        Me.chkCalcReactions.AutoSize = True
        Me.chkCalcReactions.Location = New System.Drawing.Point(111, 111)
        Me.chkCalcReactions.Name = "chkCalcReactions"
        Me.chkCalcReactions.Size = New System.Drawing.Size(74, 17)
        Me.chkCalcReactions.TabIndex = 14
        Me.chkCalcReactions.Text = "Reactions"
        Me.chkCalcReactions.UseVisualStyleBackColor = True
        '
        'chkCalcCelestials
        '
        Me.chkCalcCelestials.AutoSize = True
        Me.chkCalcCelestials.Location = New System.Drawing.Point(111, 95)
        Me.chkCalcCelestials.Name = "chkCalcCelestials"
        Me.chkCalcCelestials.Size = New System.Drawing.Size(70, 17)
        Me.chkCalcCelestials.TabIndex = 10
        Me.chkCalcCelestials.Text = "Celestials"
        Me.chkCalcCelestials.UseVisualStyleBackColor = True
        '
        'chkCalcMisc
        '
        Me.chkCalcMisc.AutoSize = True
        Me.chkCalcMisc.Location = New System.Drawing.Point(130, 47)
        Me.chkCalcMisc.Name = "chkCalcMisc"
        Me.chkCalcMisc.Size = New System.Drawing.Size(51, 17)
        Me.chkCalcMisc.TabIndex = 12
        Me.chkCalcMisc.Text = "Misc."
        Me.chkCalcMisc.UseVisualStyleBackColor = True
        '
        'chkCalcSubsystems
        '
        Me.chkCalcSubsystems.AutoSize = True
        Me.chkCalcSubsystems.Location = New System.Drawing.Point(84, 63)
        Me.chkCalcSubsystems.Name = "chkCalcSubsystems"
        Me.chkCalcSubsystems.Size = New System.Drawing.Size(82, 17)
        Me.chkCalcSubsystems.TabIndex = 7
        Me.chkCalcSubsystems.Text = "Subsystems"
        Me.chkCalcSubsystems.UseVisualStyleBackColor = True
        '
        'chkCalcDeployables
        '
        Me.chkCalcDeployables.AutoSize = True
        Me.chkCalcDeployables.Location = New System.Drawing.Point(5, 63)
        Me.chkCalcDeployables.Name = "chkCalcDeployables"
        Me.chkCalcDeployables.Size = New System.Drawing.Size(84, 17)
        Me.chkCalcDeployables.TabIndex = 6
        Me.chkCalcDeployables.Text = "Deployables"
        Me.chkCalcDeployables.UseVisualStyleBackColor = True
        '
        'chkCalcStructures
        '
        Me.chkCalcStructures.AutoSize = True
        Me.chkCalcStructures.Location = New System.Drawing.Point(5, 79)
        Me.chkCalcStructures.Name = "chkCalcStructures"
        Me.chkCalcStructures.Size = New System.Drawing.Size(74, 17)
        Me.chkCalcStructures.TabIndex = 8
        Me.chkCalcStructures.Text = "Structures"
        Me.chkCalcStructures.UseVisualStyleBackColor = True
        '
        'chkCalcStructureRigs
        '
        Me.chkCalcStructureRigs.AutoSize = True
        Me.chkCalcStructureRigs.Location = New System.Drawing.Point(5, 95)
        Me.chkCalcStructureRigs.Name = "chkCalcStructureRigs"
        Me.chkCalcStructureRigs.Size = New System.Drawing.Size(93, 17)
        Me.chkCalcStructureRigs.TabIndex = 11
        Me.chkCalcStructureRigs.Text = "Structure Rigs"
        Me.chkCalcStructureRigs.UseVisualStyleBackColor = True
        '
        'chkCalcBoosters
        '
        Me.chkCalcBoosters.AutoSize = True
        Me.chkCalcBoosters.Location = New System.Drawing.Point(84, 79)
        Me.chkCalcBoosters.Name = "chkCalcBoosters"
        Me.chkCalcBoosters.Size = New System.Drawing.Size(67, 17)
        Me.chkCalcBoosters.TabIndex = 9
        Me.chkCalcBoosters.Text = "Boosters"
        Me.chkCalcBoosters.UseVisualStyleBackColor = True
        '
        'chkCalcRigs
        '
        Me.chkCalcRigs.AutoSize = True
        Me.chkCalcRigs.Location = New System.Drawing.Point(84, 47)
        Me.chkCalcRigs.Name = "chkCalcRigs"
        Me.chkCalcRigs.Size = New System.Drawing.Size(47, 17)
        Me.chkCalcRigs.TabIndex = 5
        Me.chkCalcRigs.Text = "Rigs"
        Me.chkCalcRigs.UseVisualStyleBackColor = True
        '
        'chkCalcComponents
        '
        Me.chkCalcComponents.AutoSize = True
        Me.chkCalcComponents.Location = New System.Drawing.Point(5, 47)
        Me.chkCalcComponents.Name = "chkCalcComponents"
        Me.chkCalcComponents.Size = New System.Drawing.Size(85, 17)
        Me.chkCalcComponents.TabIndex = 4
        Me.chkCalcComponents.Text = "Components"
        Me.chkCalcComponents.UseVisualStyleBackColor = True
        '
        'chkCalcAmmo
        '
        Me.chkCalcAmmo.AutoSize = True
        Me.chkCalcAmmo.Location = New System.Drawing.Point(84, 31)
        Me.chkCalcAmmo.Name = "chkCalcAmmo"
        Me.chkCalcAmmo.Size = New System.Drawing.Size(99, 17)
        Me.chkCalcAmmo.TabIndex = 3
        Me.chkCalcAmmo.Text = "Ammo/Charges"
        Me.chkCalcAmmo.UseVisualStyleBackColor = True
        '
        'chkCalcDrones
        '
        Me.chkCalcDrones.AutoSize = True
        Me.chkCalcDrones.Location = New System.Drawing.Point(5, 31)
        Me.chkCalcDrones.Name = "chkCalcDrones"
        Me.chkCalcDrones.Size = New System.Drawing.Size(60, 17)
        Me.chkCalcDrones.TabIndex = 2
        Me.chkCalcDrones.Text = "Drones"
        Me.chkCalcDrones.UseVisualStyleBackColor = True
        '
        'chkCalcModules
        '
        Me.chkCalcModules.AutoSize = True
        Me.chkCalcModules.Location = New System.Drawing.Point(84, 15)
        Me.chkCalcModules.Name = "chkCalcModules"
        Me.chkCalcModules.Size = New System.Drawing.Size(66, 17)
        Me.chkCalcModules.TabIndex = 1
        Me.chkCalcModules.Text = "Modules"
        Me.chkCalcModules.UseVisualStyleBackColor = True
        '
        'chkCalcShips
        '
        Me.chkCalcShips.AutoSize = True
        Me.chkCalcShips.Location = New System.Drawing.Point(5, 15)
        Me.chkCalcShips.Name = "chkCalcShips"
        Me.chkCalcShips.Size = New System.Drawing.Size(52, 17)
        Me.chkCalcShips.TabIndex = 0
        Me.chkCalcShips.Text = "Ships"
        Me.chkCalcShips.UseVisualStyleBackColor = True
        '
        'chkCalcStructureModules
        '
        Me.chkCalcStructureModules.AutoSize = True
        Me.chkCalcStructureModules.Location = New System.Drawing.Point(5, 111)
        Me.chkCalcStructureModules.Name = "chkCalcStructureModules"
        Me.chkCalcStructureModules.Size = New System.Drawing.Size(112, 17)
        Me.chkCalcStructureModules.TabIndex = 13
        Me.chkCalcStructureModules.Text = "Structure Modules"
        Me.chkCalcStructureModules.UseVisualStyleBackColor = True
        '
        'gbCalcBPSelect
        '
        Me.gbCalcBPSelect.Controls.Add(Me.rbtnCalcBPFavorites)
        Me.gbCalcBPSelect.Controls.Add(Me.rbtnCalcAllBPs)
        Me.gbCalcBPSelect.Controls.Add(Me.rbtnCalcBPOwned)
        Me.gbCalcBPSelect.Location = New System.Drawing.Point(6, 15)
        Me.gbCalcBPSelect.Name = "gbCalcBPSelect"
        Me.gbCalcBPSelect.Size = New System.Drawing.Size(95, 65)
        Me.gbCalcBPSelect.TabIndex = 0
        Me.gbCalcBPSelect.TabStop = False
        Me.gbCalcBPSelect.Text = "Load:"
        '
        'rbtnCalcBPFavorites
        '
        Me.rbtnCalcBPFavorites.AutoSize = True
        Me.rbtnCalcBPFavorites.Location = New System.Drawing.Point(8, 44)
        Me.rbtnCalcBPFavorites.Name = "rbtnCalcBPFavorites"
        Me.rbtnCalcBPFavorites.Size = New System.Drawing.Size(68, 17)
        Me.rbtnCalcBPFavorites.TabIndex = 2
        Me.rbtnCalcBPFavorites.Text = "Favorites"
        Me.rbtnCalcBPFavorites.UseVisualStyleBackColor = True
        '
        'rbtnCalcAllBPs
        '
        Me.rbtnCalcAllBPs.AutoSize = True
        Me.rbtnCalcAllBPs.Location = New System.Drawing.Point(8, 14)
        Me.rbtnCalcAllBPs.Name = "rbtnCalcAllBPs"
        Me.rbtnCalcAllBPs.Size = New System.Drawing.Size(85, 17)
        Me.rbtnCalcAllBPs.TabIndex = 0
        Me.rbtnCalcAllBPs.Text = "All Blueprints"
        Me.rbtnCalcAllBPs.UseVisualStyleBackColor = True
        '
        'rbtnCalcBPOwned
        '
        Me.rbtnCalcBPOwned.AutoSize = True
        Me.rbtnCalcBPOwned.Location = New System.Drawing.Point(8, 29)
        Me.rbtnCalcBPOwned.Name = "rbtnCalcBPOwned"
        Me.rbtnCalcBPOwned.Size = New System.Drawing.Size(81, 17)
        Me.rbtnCalcBPOwned.TabIndex = 1
        Me.rbtnCalcBPOwned.Text = "Owned BPs"
        Me.rbtnCalcBPOwned.UseVisualStyleBackColor = True
        '
        'gbCalcRelics
        '
        Me.gbCalcRelics.Controls.Add(Me.chkCalcRERelic2)
        Me.gbCalcRelics.Controls.Add(Me.chkCalcRERelic3)
        Me.gbCalcRelics.Controls.Add(Me.chkCalcRERelic1)
        Me.gbCalcRelics.Location = New System.Drawing.Point(273, 201)
        Me.gbCalcRelics.Name = "gbCalcRelics"
        Me.gbCalcRelics.Size = New System.Drawing.Size(133, 52)
        Me.gbCalcRelics.TabIndex = 7
        Me.gbCalcRelics.TabStop = False
        Me.gbCalcRelics.Text = "Invention Relics:"
        '
        'chkCalcRERelic2
        '
        Me.chkCalcRERelic2.Location = New System.Drawing.Point(9, 30)
        Me.chkCalcRERelic2.Name = "chkCalcRERelic2"
        Me.chkCalcRERelic2.Size = New System.Drawing.Size(95, 17)
        Me.chkCalcRERelic2.TabIndex = 1
        Me.chkCalcRERelic2.Text = "Malfunctioning"
        Me.chkCalcRERelic2.UseVisualStyleBackColor = True
        '
        'chkCalcRERelic3
        '
        Me.chkCalcRERelic3.AutoSize = True
        Me.chkCalcRERelic3.Location = New System.Drawing.Point(78, 14)
        Me.chkCalcRERelic3.Name = "chkCalcRERelic3"
        Me.chkCalcRERelic3.Size = New System.Drawing.Size(53, 17)
        Me.chkCalcRERelic3.TabIndex = 2
        Me.chkCalcRERelic3.Text = "Intact"
        Me.chkCalcRERelic3.UseVisualStyleBackColor = True
        '
        'chkCalcRERelic1
        '
        Me.chkCalcRERelic1.AutoSize = True
        Me.chkCalcRERelic1.Location = New System.Drawing.Point(9, 14)
        Me.chkCalcRERelic1.Name = "chkCalcRERelic1"
        Me.chkCalcRERelic1.Size = New System.Drawing.Size(70, 17)
        Me.chkCalcRERelic1.TabIndex = 0
        Me.chkCalcRERelic1.Text = "Wrecked"
        Me.chkCalcRERelic1.UseVisualStyleBackColor = True
        '
        'tabUpdatePrices
        '
        Me.tabUpdatePrices.Controls.Add(Me.gbRawMaterials)
        Me.tabUpdatePrices.Controls.Add(Me.gbSingleSource)
        Me.tabUpdatePrices.Controls.Add(Me.gbPriceProfile)
        Me.tabUpdatePrices.Controls.Add(Me.btnLoadPricesfromFile)
        Me.tabUpdatePrices.Controls.Add(Me.btnSavePricestoFile)
        Me.tabUpdatePrices.Controls.Add(Me.lstPricesView)
        Me.tabUpdatePrices.Controls.Add(Me.txtPriceItemFilter)
        Me.tabUpdatePrices.Controls.Add(Me.gbPriceOptions)
        Me.tabUpdatePrices.Controls.Add(Me.btnSaveUpdatePrices)
        Me.tabUpdatePrices.Controls.Add(Me.btnCancelUpdate)
        Me.tabUpdatePrices.Controls.Add(Me.btnClearItemFilter)
        Me.tabUpdatePrices.Controls.Add(Me.btnToggleAllPriceItems)
        Me.tabUpdatePrices.Controls.Add(Me.btnDownloadPrices)
        Me.tabUpdatePrices.Controls.Add(Me.lblItemFilter)
        Me.tabUpdatePrices.Controls.Add(Me.gbManufacturedItems)
        Me.tabUpdatePrices.Controls.Add(Me.btnOpenMarketBrowser)
        Me.tabUpdatePrices.Location = New System.Drawing.Point(4, 22)
        Me.tabUpdatePrices.Name = "tabUpdatePrices"
        Me.tabUpdatePrices.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUpdatePrices.Size = New System.Drawing.Size(1137, 615)
        Me.tabUpdatePrices.TabIndex = 1
        Me.tabUpdatePrices.Text = "Update Prices"
        Me.tabUpdatePrices.UseVisualStyleBackColor = True
        '
        'gbRawMaterials
        '
        Me.gbRawMaterials.Controls.Add(Me.gbReactionMaterials)
        Me.gbRawMaterials.Controls.Add(Me.gbResearchEquipment)
        Me.gbRawMaterials.Controls.Add(Me.chkMinerals)
        Me.gbRawMaterials.Controls.Add(Me.chkFactionMaterials)
        Me.gbRawMaterials.Controls.Add(Me.chkNamedComponents)
        Me.gbRawMaterials.Controls.Add(Me.chkMisc)
        Me.gbRawMaterials.Controls.Add(Me.chkMolecularForgingTools)
        Me.gbRawMaterials.Controls.Add(Me.chkAdvancedProtectiveTechnology)
        Me.gbRawMaterials.Controls.Add(Me.chkBPCs)
        Me.gbRawMaterials.Controls.Add(Me.chkRawMaterials)
        Me.gbRawMaterials.Controls.Add(Me.chkPriceMaterialResearchEqPrices)
        Me.gbRawMaterials.Controls.Add(Me.chkPlanetary)
        Me.gbRawMaterials.Controls.Add(Me.chkGas)
        Me.gbRawMaterials.Controls.Add(Me.chkSalvage)
        Me.gbRawMaterials.Controls.Add(Me.chkIceProducts)
        Me.gbRawMaterials.Location = New System.Drawing.Point(8, 337)
        Me.gbRawMaterials.Name = "gbRawMaterials"
        Me.gbRawMaterials.Size = New System.Drawing.Size(257, 238)
        Me.gbRawMaterials.TabIndex = 1
        Me.gbRawMaterials.TabStop = False
        '
        'gbReactionMaterials
        '
        Me.gbReactionMaterials.Controls.Add(Me.chkAdvancedMats)
        Me.gbReactionMaterials.Controls.Add(Me.chkBoosterMats)
        Me.gbReactionMaterials.Controls.Add(Me.chkMolecularForgedMaterials)
        Me.gbReactionMaterials.Controls.Add(Me.chkPolymers)
        Me.gbReactionMaterials.Controls.Add(Me.chkProcessedMats)
        Me.gbReactionMaterials.Controls.Add(Me.chkRawMoonMats)
        Me.gbReactionMaterials.Location = New System.Drawing.Point(4, 123)
        Me.gbReactionMaterials.Name = "gbReactionMaterials"
        Me.gbReactionMaterials.Size = New System.Drawing.Size(249, 70)
        Me.gbReactionMaterials.TabIndex = 27
        Me.gbReactionMaterials.TabStop = False
        Me.gbReactionMaterials.Text = "Reaction Materials"
        '
        'chkAdvancedMats
        '
        Me.chkAdvancedMats.AutoSize = True
        Me.chkAdvancedMats.Location = New System.Drawing.Point(5, 16)
        Me.chkAdvancedMats.Name = "chkAdvancedMats"
        Me.chkAdvancedMats.Size = New System.Drawing.Size(105, 17)
        Me.chkAdvancedMats.TabIndex = 15
        Me.chkAdvancedMats.Text = "Advanced Moon"
        Me.chkAdvancedMats.UseVisualStyleBackColor = True
        '
        'chkBoosterMats
        '
        Me.chkBoosterMats.AutoSize = True
        Me.chkBoosterMats.Location = New System.Drawing.Point(125, 16)
        Me.chkBoosterMats.Name = "chkBoosterMats"
        Me.chkBoosterMats.Size = New System.Drawing.Size(107, 17)
        Me.chkBoosterMats.TabIndex = 18
        Me.chkBoosterMats.Text = "Booster Materials"
        Me.chkBoosterMats.UseVisualStyleBackColor = True
        '
        'chkMolecularForgedMaterials
        '
        Me.chkMolecularForgedMaterials.AutoSize = True
        Me.chkMolecularForgedMaterials.Location = New System.Drawing.Point(5, 33)
        Me.chkMolecularForgedMaterials.Name = "chkMolecularForgedMaterials"
        Me.chkMolecularForgedMaterials.Size = New System.Drawing.Size(108, 17)
        Me.chkMolecularForgedMaterials.TabIndex = 21
        Me.chkMolecularForgedMaterials.Text = "Molecular-Forged"
        Me.chkMolecularForgedMaterials.UseVisualStyleBackColor = True
        '
        'chkPolymers
        '
        Me.chkPolymers.AutoSize = True
        Me.chkPolymers.Location = New System.Drawing.Point(125, 33)
        Me.chkPolymers.Name = "chkPolymers"
        Me.chkPolymers.Size = New System.Drawing.Size(108, 17)
        Me.chkPolymers.TabIndex = 10
        Me.chkPolymers.Text = "Polymer Materials"
        Me.chkPolymers.UseVisualStyleBackColor = True
        '
        'chkProcessedMats
        '
        Me.chkProcessedMats.AutoSize = True
        Me.chkProcessedMats.Location = New System.Drawing.Point(5, 50)
        Me.chkProcessedMats.Name = "chkProcessedMats"
        Me.chkProcessedMats.Size = New System.Drawing.Size(106, 17)
        Me.chkProcessedMats.TabIndex = 14
        Me.chkProcessedMats.Text = "Processed Moon"
        Me.chkProcessedMats.UseVisualStyleBackColor = True
        '
        'chkRawMoonMats
        '
        Me.chkRawMoonMats.AutoSize = True
        Me.chkRawMoonMats.Location = New System.Drawing.Point(125, 50)
        Me.chkRawMoonMats.Name = "chkRawMoonMats"
        Me.chkRawMoonMats.Size = New System.Drawing.Size(123, 17)
        Me.chkRawMoonMats.TabIndex = 13
        Me.chkRawMoonMats.Text = "Raw Moon Materials"
        Me.chkRawMoonMats.UseVisualStyleBackColor = True
        '
        'gbResearchEquipment
        '
        Me.gbResearchEquipment.Controls.Add(Me.chkRDb)
        Me.gbResearchEquipment.Controls.Add(Me.chkAncientRelics)
        Me.gbResearchEquipment.Controls.Add(Me.chkDecryptors)
        Me.gbResearchEquipment.Controls.Add(Me.chkDatacores)
        Me.gbResearchEquipment.Location = New System.Drawing.Point(4, 195)
        Me.gbResearchEquipment.Name = "gbResearchEquipment"
        Me.gbResearchEquipment.Size = New System.Drawing.Size(249, 37)
        Me.gbResearchEquipment.TabIndex = 26
        Me.gbResearchEquipment.TabStop = False
        Me.gbResearchEquipment.Text = "Research Equipment"
        '
        'chkRDb
        '
        Me.chkRDb.AutoSize = True
        Me.chkRDb.Location = New System.Drawing.Point(199, 16)
        Me.chkRDb.Name = "chkRDb"
        Me.chkRDb.Size = New System.Drawing.Size(51, 17)
        Me.chkRDb.TabIndex = 25
        Me.chkRDb.Text = "R.Db"
        Me.chkRDb.UseVisualStyleBackColor = True
        '
        'chkAncientRelics
        '
        Me.chkAncientRelics.AutoSize = True
        Me.chkAncientRelics.Location = New System.Drawing.Point(148, 16)
        Me.chkAncientRelics.Name = "chkAncientRelics"
        Me.chkAncientRelics.Size = New System.Drawing.Size(55, 17)
        Me.chkAncientRelics.TabIndex = 9
        Me.chkAncientRelics.Text = "Relics"
        Me.chkAncientRelics.UseVisualStyleBackColor = True
        '
        'chkDecryptors
        '
        Me.chkDecryptors.AutoSize = True
        Me.chkDecryptors.Location = New System.Drawing.Point(76, 16)
        Me.chkDecryptors.Name = "chkDecryptors"
        Me.chkDecryptors.Size = New System.Drawing.Size(77, 17)
        Me.chkDecryptors.TabIndex = 4
        Me.chkDecryptors.Text = "Decryptors"
        Me.chkDecryptors.UseVisualStyleBackColor = True
        '
        'chkDatacores
        '
        Me.chkDatacores.AutoSize = True
        Me.chkDatacores.Location = New System.Drawing.Point(5, 16)
        Me.chkDatacores.Name = "chkDatacores"
        Me.chkDatacores.Size = New System.Drawing.Size(75, 17)
        Me.chkDatacores.TabIndex = 2
        Me.chkDatacores.Text = "Datacores"
        Me.chkDatacores.UseVisualStyleBackColor = True
        '
        'chkMinerals
        '
        Me.chkMinerals.AutoSize = True
        Me.chkMinerals.Location = New System.Drawing.Point(147, 70)
        Me.chkMinerals.Name = "chkMinerals"
        Me.chkMinerals.Size = New System.Drawing.Size(65, 17)
        Me.chkMinerals.TabIndex = 0
        Me.chkMinerals.Text = "Minerals"
        Me.chkMinerals.UseVisualStyleBackColor = True
        '
        'chkFactionMaterials
        '
        Me.chkFactionMaterials.AutoSize = True
        Me.chkFactionMaterials.Location = New System.Drawing.Point(147, 53)
        Me.chkFactionMaterials.Name = "chkFactionMaterials"
        Me.chkFactionMaterials.Size = New System.Drawing.Size(106, 17)
        Me.chkFactionMaterials.TabIndex = 16
        Me.chkFactionMaterials.Text = "Faction Materials"
        Me.chkFactionMaterials.UseVisualStyleBackColor = True
        '
        'chkNamedComponents
        '
        Me.chkNamedComponents.AutoSize = True
        Me.chkNamedComponents.Location = New System.Drawing.Point(9, 70)
        Me.chkNamedComponents.Name = "chkNamedComponents"
        Me.chkNamedComponents.Size = New System.Drawing.Size(122, 17)
        Me.chkNamedComponents.TabIndex = 24
        Me.chkNamedComponents.Text = "Named Components"
        Me.chkNamedComponents.UseVisualStyleBackColor = True
        '
        'chkMisc
        '
        Me.chkMisc.AutoSize = True
        Me.chkMisc.Location = New System.Drawing.Point(80, 104)
        Me.chkMisc.Name = "chkMisc"
        Me.chkMisc.Size = New System.Drawing.Size(51, 17)
        Me.chkMisc.TabIndex = 12
        Me.chkMisc.Text = "Misc."
        Me.chkMisc.UseVisualStyleBackColor = True
        '
        'chkMolecularForgingTools
        '
        Me.chkMolecularForgingTools.AutoSize = True
        Me.chkMolecularForgingTools.Location = New System.Drawing.Point(9, 53)
        Me.chkMolecularForgingTools.Name = "chkMolecularForgingTools"
        Me.chkMolecularForgingTools.Size = New System.Drawing.Size(139, 17)
        Me.chkMolecularForgingTools.TabIndex = 23
        Me.chkMolecularForgingTools.Text = "Molecular-Forging Tools"
        Me.chkMolecularForgingTools.UseVisualStyleBackColor = True
        '
        'chkAdvancedProtectiveTechnology
        '
        Me.chkAdvancedProtectiveTechnology.AutoSize = True
        Me.chkAdvancedProtectiveTechnology.Location = New System.Drawing.Point(9, 19)
        Me.chkAdvancedProtectiveTechnology.Name = "chkAdvancedProtectiveTechnology"
        Me.chkAdvancedProtectiveTechnology.Size = New System.Drawing.Size(185, 17)
        Me.chkAdvancedProtectiveTechnology.TabIndex = 22
        Me.chkAdvancedProtectiveTechnology.Text = "Advanced Protective Technology"
        Me.chkAdvancedProtectiveTechnology.UseVisualStyleBackColor = True
        '
        'chkBPCs
        '
        Me.chkBPCs.AutoSize = True
        Me.chkBPCs.Location = New System.Drawing.Point(147, 104)
        Me.chkBPCs.Name = "chkBPCs"
        Me.chkBPCs.Size = New System.Drawing.Size(102, 17)
        Me.chkBPCs.TabIndex = 19
        Me.chkBPCs.Text = "Blueprint Copies"
        Me.chkBPCs.ThreeState = True
        Me.chkBPCs.UseVisualStyleBackColor = True
        '
        'chkRawMaterials
        '
        Me.chkRawMaterials.AutoSize = True
        Me.chkRawMaterials.Location = New System.Drawing.Point(147, 87)
        Me.chkRawMaterials.Name = "chkRawMaterials"
        Me.chkRawMaterials.Size = New System.Drawing.Size(93, 17)
        Me.chkRawMaterials.TabIndex = 6
        Me.chkRawMaterials.Text = "Raw Materials"
        Me.chkRawMaterials.UseVisualStyleBackColor = True
        '
        'chkPriceMaterialResearchEqPrices
        '
        Me.chkPriceMaterialResearchEqPrices.AutoSize = True
        Me.chkPriceMaterialResearchEqPrices.BackColor = System.Drawing.Color.White
        Me.chkPriceMaterialResearchEqPrices.Location = New System.Drawing.Point(6, 1)
        Me.chkPriceMaterialResearchEqPrices.Name = "chkPriceMaterialResearchEqPrices"
        Me.chkPriceMaterialResearchEqPrices.Size = New System.Drawing.Size(179, 17)
        Me.chkPriceMaterialResearchEqPrices.TabIndex = 0
        Me.chkPriceMaterialResearchEqPrices.Text = "Materials && Research Equipment"
        Me.chkPriceMaterialResearchEqPrices.UseVisualStyleBackColor = False
        '
        'chkPlanetary
        '
        Me.chkPlanetary.AutoSize = True
        Me.chkPlanetary.Location = New System.Drawing.Point(9, 87)
        Me.chkPlanetary.Name = "chkPlanetary"
        Me.chkPlanetary.Size = New System.Drawing.Size(115, 17)
        Me.chkPlanetary.TabIndex = 5
        Me.chkPlanetary.Text = "Planetary Materials"
        Me.chkPlanetary.UseVisualStyleBackColor = True
        '
        'chkGas
        '
        Me.chkGas.AutoSize = True
        Me.chkGas.Location = New System.Drawing.Point(9, 36)
        Me.chkGas.Name = "chkGas"
        Me.chkGas.Size = New System.Drawing.Size(120, 17)
        Me.chkGas.TabIndex = 11
        Me.chkGas.Text = "Gas Cloud Materials"
        Me.chkGas.UseVisualStyleBackColor = True
        '
        'chkSalvage
        '
        Me.chkSalvage.AutoSize = True
        Me.chkSalvage.Location = New System.Drawing.Point(9, 104)
        Me.chkSalvage.Name = "chkSalvage"
        Me.chkSalvage.Size = New System.Drawing.Size(65, 17)
        Me.chkSalvage.TabIndex = 7
        Me.chkSalvage.Text = "Salvage"
        Me.chkSalvage.UseVisualStyleBackColor = True
        '
        'chkIceProducts
        '
        Me.chkIceProducts.AutoSize = True
        Me.chkIceProducts.Location = New System.Drawing.Point(147, 36)
        Me.chkIceProducts.Name = "chkIceProducts"
        Me.chkIceProducts.Size = New System.Drawing.Size(86, 17)
        Me.chkIceProducts.TabIndex = 1
        Me.chkIceProducts.Text = "Ice Products"
        Me.chkIceProducts.UseVisualStyleBackColor = True
        '
        'gbSingleSource
        '
        Me.gbSingleSource.Controls.Add(Me.gbMarketStructures)
        Me.gbSingleSource.Controls.Add(Me.gbRegionSystemPrice)
        Me.gbSingleSource.Controls.Add(Me.gbTradeHubSystems)
        Me.gbSingleSource.Location = New System.Drawing.Point(675, 381)
        Me.gbSingleSource.Name = "gbSingleSource"
        Me.gbSingleSource.Size = New System.Drawing.Size(457, 102)
        Me.gbSingleSource.TabIndex = 126
        Me.gbSingleSource.TabStop = False
        Me.gbSingleSource.Text = "Single Source "
        '
        'gbMarketStructures
        '
        Me.gbMarketStructures.Controls.Add(Me.btnAddStructureIDs)
        Me.gbMarketStructures.Controls.Add(Me.btnViewSavedStructures)
        Me.gbMarketStructures.Location = New System.Drawing.Point(323, 18)
        Me.gbMarketStructures.Name = "gbMarketStructures"
        Me.gbMarketStructures.Size = New System.Drawing.Size(127, 77)
        Me.gbMarketStructures.TabIndex = 126
        Me.gbMarketStructures.TabStop = False
        Me.gbMarketStructures.Text = "Market Structures:"
        '
        'btnAddStructureIDs
        '
        Me.btnAddStructureIDs.Location = New System.Drawing.Point(13, 16)
        Me.btnAddStructureIDs.Name = "btnAddStructureIDs"
        Me.btnAddStructureIDs.Size = New System.Drawing.Size(100, 25)
        Me.btnAddStructureIDs.TabIndex = 71
        Me.btnAddStructureIDs.Text = "Add Structure IDs"
        Me.btnAddStructureIDs.UseVisualStyleBackColor = True
        '
        'btnViewSavedStructures
        '
        Me.btnViewSavedStructures.Location = New System.Drawing.Point(13, 46)
        Me.btnViewSavedStructures.Name = "btnViewSavedStructures"
        Me.btnViewSavedStructures.Size = New System.Drawing.Size(100, 25)
        Me.btnViewSavedStructures.TabIndex = 122
        Me.btnViewSavedStructures.Text = "View Saved SIDs"
        Me.btnViewSavedStructures.UseVisualStyleBackColor = True
        '
        'gbRegionSystemPrice
        '
        Me.gbRegionSystemPrice.Controls.Add(Me.cmbPriceRegions)
        Me.gbRegionSystemPrice.Controls.Add(Me.cmbPriceSystems)
        Me.gbRegionSystemPrice.Location = New System.Drawing.Point(184, 18)
        Me.gbRegionSystemPrice.Name = "gbRegionSystemPrice"
        Me.gbRegionSystemPrice.Size = New System.Drawing.Size(133, 77)
        Me.gbRegionSystemPrice.TabIndex = 125
        Me.gbRegionSystemPrice.TabStop = False
        Me.gbRegionSystemPrice.Text = "Region or System:"
        '
        'cmbPriceRegions
        '
        Me.cmbPriceRegions.FormattingEnabled = True
        Me.cmbPriceRegions.Location = New System.Drawing.Point(9, 18)
        Me.cmbPriceRegions.Name = "cmbPriceRegions"
        Me.cmbPriceRegions.Size = New System.Drawing.Size(115, 21)
        Me.cmbPriceRegions.TabIndex = 124
        Me.cmbPriceRegions.Text = "Select Region"
        '
        'cmbPriceSystems
        '
        Me.cmbPriceSystems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPriceSystems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPriceSystems.FormattingEnabled = True
        Me.cmbPriceSystems.Location = New System.Drawing.Point(9, 48)
        Me.cmbPriceSystems.Name = "cmbPriceSystems"
        Me.cmbPriceSystems.Size = New System.Drawing.Size(115, 21)
        Me.cmbPriceSystems.TabIndex = 5
        Me.cmbPriceSystems.Text = "Select System"
        '
        'gbTradeHubSystems
        '
        Me.gbTradeHubSystems.Controls.Add(Me.chkSystems6)
        Me.gbTradeHubSystems.Controls.Add(Me.chkSystems5)
        Me.gbTradeHubSystems.Controls.Add(Me.chkSystems4)
        Me.gbTradeHubSystems.Controls.Add(Me.chkSystems3)
        Me.gbTradeHubSystems.Controls.Add(Me.chkSystems2)
        Me.gbTradeHubSystems.Controls.Add(Me.chkSystems1)
        Me.gbTradeHubSystems.Location = New System.Drawing.Point(8, 18)
        Me.gbTradeHubSystems.Name = "gbTradeHubSystems"
        Me.gbTradeHubSystems.Size = New System.Drawing.Size(170, 77)
        Me.gbTradeHubSystems.TabIndex = 8
        Me.gbTradeHubSystems.TabStop = False
        Me.gbTradeHubSystems.Text = "Trade Hub System:"
        '
        'chkSystems6
        '
        Me.chkSystems6.AutoSize = True
        Me.chkSystems6.Location = New System.Drawing.Point(89, 19)
        Me.chkSystems6.Name = "chkSystems6"
        Me.chkSystems6.Size = New System.Drawing.Size(70, 17)
        Me.chkSystems6.TabIndex = 7
        Me.chkSystems6.Text = "Perimeter"
        Me.chkSystems6.UseVisualStyleBackColor = True
        '
        'chkSystems5
        '
        Me.chkSystems5.AutoSize = True
        Me.chkSystems5.Location = New System.Drawing.Point(89, 53)
        Me.chkSystems5.Name = "chkSystems5"
        Me.chkSystems5.Size = New System.Drawing.Size(46, 17)
        Me.chkSystems5.TabIndex = 4
        Me.chkSystems5.Text = "Hek"
        Me.chkSystems5.UseVisualStyleBackColor = True
        '
        'chkSystems4
        '
        Me.chkSystems4.AutoSize = True
        Me.chkSystems4.Location = New System.Drawing.Point(14, 53)
        Me.chkSystems4.Name = "chkSystems4"
        Me.chkSystems4.Size = New System.Drawing.Size(51, 17)
        Me.chkSystems4.TabIndex = 3
        Me.chkSystems4.Text = "Rens"
        Me.chkSystems4.UseVisualStyleBackColor = True
        '
        'chkSystems3
        '
        Me.chkSystems3.AutoSize = True
        Me.chkSystems3.Location = New System.Drawing.Point(89, 36)
        Me.chkSystems3.Name = "chkSystems3"
        Me.chkSystems3.Size = New System.Drawing.Size(61, 17)
        Me.chkSystems3.TabIndex = 2
        Me.chkSystems3.Text = "Dodixie"
        Me.chkSystems3.UseVisualStyleBackColor = True
        '
        'chkSystems2
        '
        Me.chkSystems2.AutoSize = True
        Me.chkSystems2.Location = New System.Drawing.Point(14, 36)
        Me.chkSystems2.Name = "chkSystems2"
        Me.chkSystems2.Size = New System.Drawing.Size(53, 17)
        Me.chkSystems2.TabIndex = 1
        Me.chkSystems2.Text = "Amarr"
        Me.chkSystems2.UseVisualStyleBackColor = True
        '
        'chkSystems1
        '
        Me.chkSystems1.AutoSize = True
        Me.chkSystems1.Location = New System.Drawing.Point(14, 19)
        Me.chkSystems1.Name = "chkSystems1"
        Me.chkSystems1.Size = New System.Drawing.Size(42, 17)
        Me.chkSystems1.TabIndex = 0
        Me.chkSystems1.Text = "Jita"
        Me.chkSystems1.UseVisualStyleBackColor = True
        '
        'gbPriceProfile
        '
        Me.gbPriceProfile.Controls.Add(Me.tabPriceProfile)
        Me.gbPriceProfile.Controls.Add(Me.gbPPDefaultSettings)
        Me.gbPriceProfile.Location = New System.Drawing.Point(674, 4)
        Me.gbPriceProfile.Name = "gbPriceProfile"
        Me.gbPriceProfile.Size = New System.Drawing.Size(457, 371)
        Me.gbPriceProfile.TabIndex = 125
        Me.gbPriceProfile.TabStop = False
        Me.gbPriceProfile.Text = "Set Price Profile"
        '
        'tabPriceProfile
        '
        Me.tabPriceProfile.Controls.Add(Me.tabPriceProfileRaw)
        Me.tabPriceProfile.Controls.Add(Me.tabPriceProfileManufactured)
        Me.tabPriceProfile.Location = New System.Drawing.Point(4, 15)
        Me.tabPriceProfile.Name = "tabPriceProfile"
        Me.tabPriceProfile.SelectedIndex = 0
        Me.tabPriceProfile.Size = New System.Drawing.Size(449, 280)
        Me.tabPriceProfile.TabIndex = 9
        '
        'tabPriceProfileRaw
        '
        Me.tabPriceProfileRaw.Controls.Add(Me.lstRawPriceProfile)
        Me.tabPriceProfileRaw.Location = New System.Drawing.Point(4, 22)
        Me.tabPriceProfileRaw.Name = "tabPriceProfileRaw"
        Me.tabPriceProfileRaw.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPriceProfileRaw.Size = New System.Drawing.Size(441, 254)
        Me.tabPriceProfileRaw.TabIndex = 0
        Me.tabPriceProfileRaw.Text = "Materials && Research Equipment"
        Me.tabPriceProfileRaw.UseVisualStyleBackColor = True
        '
        'lstRawPriceProfile
        '
        Me.lstRawPriceProfile.FullRowSelect = True
        Me.lstRawPriceProfile.GridLines = True
        Me.lstRawPriceProfile.HideSelection = False
        Me.lstRawPriceProfile.Location = New System.Drawing.Point(2, 3)
        Me.lstRawPriceProfile.MultiSelect = False
        Me.lstRawPriceProfile.Name = "lstRawPriceProfile"
        Me.lstRawPriceProfile.Size = New System.Drawing.Size(437, 248)
        Me.lstRawPriceProfile.TabIndex = 1
        Me.lstRawPriceProfile.UseCompatibleStateImageBehavior = False
        Me.lstRawPriceProfile.View = System.Windows.Forms.View.Details
        '
        'tabPriceProfileManufactured
        '
        Me.tabPriceProfileManufactured.Controls.Add(Me.lstManufacturedPriceProfile)
        Me.tabPriceProfileManufactured.Location = New System.Drawing.Point(4, 22)
        Me.tabPriceProfileManufactured.Name = "tabPriceProfileManufactured"
        Me.tabPriceProfileManufactured.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPriceProfileManufactured.Size = New System.Drawing.Size(441, 254)
        Me.tabPriceProfileManufactured.TabIndex = 1
        Me.tabPriceProfileManufactured.Text = "Manufactured Items"
        Me.tabPriceProfileManufactured.UseVisualStyleBackColor = True
        '
        'lstManufacturedPriceProfile
        '
        Me.lstManufacturedPriceProfile.FullRowSelect = True
        Me.lstManufacturedPriceProfile.GridLines = True
        Me.lstManufacturedPriceProfile.HideSelection = False
        Me.lstManufacturedPriceProfile.Location = New System.Drawing.Point(2, 3)
        Me.lstManufacturedPriceProfile.MultiSelect = False
        Me.lstManufacturedPriceProfile.Name = "lstManufacturedPriceProfile"
        Me.lstManufacturedPriceProfile.Size = New System.Drawing.Size(437, 248)
        Me.lstManufacturedPriceProfile.TabIndex = 2
        Me.lstManufacturedPriceProfile.UseCompatibleStateImageBehavior = False
        Me.lstManufacturedPriceProfile.View = System.Windows.Forms.View.Details
        '
        'gbPPDefaultSettings
        '
        Me.gbPPDefaultSettings.Controls.Add(Me.btnPPUpdateDefaults)
        Me.gbPPDefaultSettings.Controls.Add(Me.cmbPPDefaultsPriceType)
        Me.gbPPDefaultSettings.Controls.Add(Me.lblPPDefaultsSystem)
        Me.gbPPDefaultSettings.Controls.Add(Me.lblPPDefaultsPriceType)
        Me.gbPPDefaultSettings.Controls.Add(Me.cmbPPDefaultsSystem)
        Me.gbPPDefaultSettings.Controls.Add(Me.cmbPPDefaultsRegion)
        Me.gbPPDefaultSettings.Controls.Add(Me.lblPPDefaultsRegion)
        Me.gbPPDefaultSettings.Controls.Add(Me.txtPPDefaultsPriceMod)
        Me.gbPPDefaultSettings.Controls.Add(Me.lblPPDefaultsPriceMod)
        Me.gbPPDefaultSettings.Location = New System.Drawing.Point(8, 299)
        Me.gbPPDefaultSettings.Name = "gbPPDefaultSettings"
        Me.gbPPDefaultSettings.Size = New System.Drawing.Size(443, 64)
        Me.gbPPDefaultSettings.TabIndex = 53
        Me.gbPPDefaultSettings.TabStop = False
        Me.gbPPDefaultSettings.Text = "Set Defaults:"
        '
        'btnPPUpdateDefaults
        '
        Me.btnPPUpdateDefaults.Location = New System.Drawing.Point(385, 30)
        Me.btnPPUpdateDefaults.Name = "btnPPUpdateDefaults"
        Me.btnPPUpdateDefaults.Size = New System.Drawing.Size(51, 23)
        Me.btnPPUpdateDefaults.TabIndex = 4
        Me.btnPPUpdateDefaults.Text = "Update"
        Me.btnPPUpdateDefaults.UseVisualStyleBackColor = True
        '
        'cmbPPDefaultsPriceType
        '
        Me.cmbPPDefaultsPriceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPPDefaultsPriceType.FormattingEnabled = True
        Me.cmbPPDefaultsPriceType.Items.AddRange(New Object() {"Min Sell", "Max Sell", "Avg Sell", "Median Sell", "Percentile Sell", "Min Buy", "Max Buy", "Avg Buy", "Median Buy", "Percentile Buy", "Split Price"})
        Me.cmbPPDefaultsPriceType.Location = New System.Drawing.Point(6, 31)
        Me.cmbPPDefaultsPriceType.Name = "cmbPPDefaultsPriceType"
        Me.cmbPPDefaultsPriceType.Size = New System.Drawing.Size(91, 21)
        Me.cmbPPDefaultsPriceType.TabIndex = 0
        '
        'lblPPDefaultsSystem
        '
        Me.lblPPDefaultsSystem.AutoSize = True
        Me.lblPPDefaultsSystem.Location = New System.Drawing.Point(215, 17)
        Me.lblPPDefaultsSystem.Name = "lblPPDefaultsSystem"
        Me.lblPPDefaultsSystem.Size = New System.Drawing.Size(44, 13)
        Me.lblPPDefaultsSystem.TabIndex = 51
        Me.lblPPDefaultsSystem.Text = "System:"
        '
        'lblPPDefaultsPriceType
        '
        Me.lblPPDefaultsPriceType.AutoSize = True
        Me.lblPPDefaultsPriceType.Location = New System.Drawing.Point(3, 17)
        Me.lblPPDefaultsPriceType.Name = "lblPPDefaultsPriceType"
        Me.lblPPDefaultsPriceType.Size = New System.Drawing.Size(61, 13)
        Me.lblPPDefaultsPriceType.TabIndex = 47
        Me.lblPPDefaultsPriceType.Text = "Price Type:"
        '
        'cmbPPDefaultsSystem
        '
        Me.cmbPPDefaultsSystem.FormattingEnabled = True
        Me.cmbPPDefaultsSystem.Location = New System.Drawing.Point(218, 31)
        Me.cmbPPDefaultsSystem.Name = "cmbPPDefaultsSystem"
        Me.cmbPPDefaultsSystem.Size = New System.Drawing.Size(115, 21)
        Me.cmbPPDefaultsSystem.TabIndex = 2
        Me.cmbPPDefaultsSystem.Text = "Select System"
        '
        'cmbPPDefaultsRegion
        '
        Me.cmbPPDefaultsRegion.FormattingEnabled = True
        Me.cmbPPDefaultsRegion.Location = New System.Drawing.Point(100, 31)
        Me.cmbPPDefaultsRegion.Name = "cmbPPDefaultsRegion"
        Me.cmbPPDefaultsRegion.Size = New System.Drawing.Size(115, 21)
        Me.cmbPPDefaultsRegion.TabIndex = 1
        Me.cmbPPDefaultsRegion.Text = "Select Region"
        '
        'lblPPDefaultsRegion
        '
        Me.lblPPDefaultsRegion.AutoSize = True
        Me.lblPPDefaultsRegion.Location = New System.Drawing.Point(97, 17)
        Me.lblPPDefaultsRegion.Name = "lblPPDefaultsRegion"
        Me.lblPPDefaultsRegion.Size = New System.Drawing.Size(44, 13)
        Me.lblPPDefaultsRegion.TabIndex = 50
        Me.lblPPDefaultsRegion.Text = "Region:"
        '
        'txtPPDefaultsPriceMod
        '
        Me.txtPPDefaultsPriceMod.Location = New System.Drawing.Point(336, 32)
        Me.txtPPDefaultsPriceMod.Name = "txtPPDefaultsPriceMod"
        Me.txtPPDefaultsPriceMod.Size = New System.Drawing.Size(46, 20)
        Me.txtPPDefaultsPriceMod.TabIndex = 3
        Me.txtPPDefaultsPriceMod.Text = "0.0%"
        Me.txtPPDefaultsPriceMod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPPDefaultsPriceMod
        '
        Me.lblPPDefaultsPriceMod.AutoSize = True
        Me.lblPPDefaultsPriceMod.Location = New System.Drawing.Point(333, 17)
        Me.lblPPDefaultsPriceMod.Name = "lblPPDefaultsPriceMod"
        Me.lblPPDefaultsPriceMod.Size = New System.Drawing.Size(47, 13)
        Me.lblPPDefaultsPriceMod.TabIndex = 44
        Me.lblPPDefaultsPriceMod.Text = "Modifier:"
        '
        'btnLoadPricesfromFile
        '
        Me.btnLoadPricesfromFile.Location = New System.Drawing.Point(588, 578)
        Me.btnLoadPricesfromFile.Name = "btnLoadPricesfromFile"
        Me.btnLoadPricesfromFile.Size = New System.Drawing.Size(80, 32)
        Me.btnLoadPricesfromFile.TabIndex = 42
        Me.btnLoadPricesfromFile.Text = "Load Prices"
        Me.btnLoadPricesfromFile.UseVisualStyleBackColor = True
        '
        'btnSavePricestoFile
        '
        Me.btnSavePricestoFile.Location = New System.Drawing.Point(502, 578)
        Me.btnSavePricestoFile.Name = "btnSavePricestoFile"
        Me.btnSavePricestoFile.Size = New System.Drawing.Size(80, 32)
        Me.btnSavePricestoFile.TabIndex = 41
        Me.btnSavePricestoFile.Text = "Save Prices"
        Me.btnSavePricestoFile.UseVisualStyleBackColor = True
        '
        'lstPricesView
        '
        Me.lstPricesView.FullRowSelect = True
        Me.lstPricesView.GridLines = True
        Me.lstPricesView.HideSelection = False
        Me.lstPricesView.Location = New System.Drawing.Point(8, 10)
        Me.lstPricesView.MultiSelect = False
        Me.lstPricesView.Name = "lstPricesView"
        Me.lstPricesView.Size = New System.Drawing.Size(660, 321)
        Me.lstPricesView.TabIndex = 0
        Me.lstPricesView.UseCompatibleStateImageBehavior = False
        Me.lstPricesView.View = System.Windows.Forms.View.Details
        '
        'txtPriceItemFilter
        '
        Me.txtPriceItemFilter.Location = New System.Drawing.Point(80, 584)
        Me.txtPriceItemFilter.Name = "txtPriceItemFilter"
        Me.txtPriceItemFilter.Size = New System.Drawing.Size(226, 20)
        Me.txtPriceItemFilter.TabIndex = 3
        '
        'gbPriceOptions
        '
        Me.gbPriceOptions.Controls.Add(Me.txtItemsPriceModifier)
        Me.gbPriceOptions.Controls.Add(Me.txtRawPriceModifier)
        Me.gbPriceOptions.Controls.Add(Me.gbPriceTypes)
        Me.gbPriceOptions.Controls.Add(Me.lblItemsPriceModifier)
        Me.gbPriceOptions.Controls.Add(Me.lblRawPriceModifier)
        Me.gbPriceOptions.Controls.Add(Me.gbDataSource)
        Me.gbPriceOptions.Controls.Add(Me.cmbItemsSplitPrices)
        Me.gbPriceOptions.Controls.Add(Me.cmbRawMatsSplitPrices)
        Me.gbPriceOptions.Controls.Add(Me.lblItemsSplitPrices)
        Me.gbPriceOptions.Controls.Add(Me.lblRawMatsSplitPrices)
        Me.gbPriceOptions.Location = New System.Drawing.Point(674, 482)
        Me.gbPriceOptions.Name = "gbPriceOptions"
        Me.gbPriceOptions.Size = New System.Drawing.Size(457, 93)
        Me.gbPriceOptions.TabIndex = 10
        Me.gbPriceOptions.TabStop = False
        '
        'txtItemsPriceModifier
        '
        Me.txtItemsPriceModifier.Location = New System.Drawing.Point(219, 64)
        Me.txtItemsPriceModifier.Name = "txtItemsPriceModifier"
        Me.txtItemsPriceModifier.Size = New System.Drawing.Size(46, 20)
        Me.txtItemsPriceModifier.TabIndex = 4
        Me.txtItemsPriceModifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRawPriceModifier
        '
        Me.txtRawPriceModifier.Location = New System.Drawing.Point(219, 25)
        Me.txtRawPriceModifier.Name = "txtRawPriceModifier"
        Me.txtRawPriceModifier.Size = New System.Drawing.Size(46, 20)
        Me.txtRawPriceModifier.TabIndex = 3
        Me.txtRawPriceModifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'gbPriceTypes
        '
        Me.gbPriceTypes.Controls.Add(Me.rbtnPriceSettingPriceProfile)
        Me.gbPriceTypes.Controls.Add(Me.rbtnPriceSettingSingleSelect)
        Me.gbPriceTypes.Location = New System.Drawing.Point(271, 49)
        Me.gbPriceTypes.Name = "gbPriceTypes"
        Me.gbPriceTypes.Size = New System.Drawing.Size(180, 38)
        Me.gbPriceTypes.TabIndex = 19
        Me.gbPriceTypes.TabStop = False
        Me.gbPriceTypes.Text = "Price Settings:"
        '
        'rbtnPriceSettingPriceProfile
        '
        Me.rbtnPriceSettingPriceProfile.AutoSize = True
        Me.rbtnPriceSettingPriceProfile.Location = New System.Drawing.Point(99, 15)
        Me.rbtnPriceSettingPriceProfile.Name = "rbtnPriceSettingPriceProfile"
        Me.rbtnPriceSettingPriceProfile.Size = New System.Drawing.Size(81, 17)
        Me.rbtnPriceSettingPriceProfile.TabIndex = 8
        Me.rbtnPriceSettingPriceProfile.Text = "Price Profile"
        Me.rbtnPriceSettingPriceProfile.UseVisualStyleBackColor = True
        '
        'rbtnPriceSettingSingleSelect
        '
        Me.rbtnPriceSettingSingleSelect.AutoSize = True
        Me.rbtnPriceSettingSingleSelect.Location = New System.Drawing.Point(5, 15)
        Me.rbtnPriceSettingSingleSelect.Name = "rbtnPriceSettingSingleSelect"
        Me.rbtnPriceSettingSingleSelect.Size = New System.Drawing.Size(91, 17)
        Me.rbtnPriceSettingSingleSelect.TabIndex = 7
        Me.rbtnPriceSettingSingleSelect.Text = "Single Source"
        Me.rbtnPriceSettingSingleSelect.UseVisualStyleBackColor = True
        '
        'lblItemsPriceModifier
        '
        Me.lblItemsPriceModifier.Location = New System.Drawing.Point(145, 60)
        Me.lblItemsPriceModifier.Name = "lblItemsPriceModifier"
        Me.lblItemsPriceModifier.Size = New System.Drawing.Size(81, 29)
        Me.lblItemsPriceModifier.TabIndex = 48
        Me.lblItemsPriceModifier.Text = "Manufactured Price Modifier:"
        '
        'lblRawPriceModifier
        '
        Me.lblRawPriceModifier.Location = New System.Drawing.Point(145, 21)
        Me.lblRawPriceModifier.Name = "lblRawPriceModifier"
        Me.lblRawPriceModifier.Size = New System.Drawing.Size(81, 29)
        Me.lblRawPriceModifier.TabIndex = 20
        Me.lblRawPriceModifier.Text = "Raw Price Modifier:"
        '
        'gbDataSource
        '
        Me.gbDataSource.Controls.Add(Me.rbtnPriceSourceCCPData)
        Me.gbDataSource.Controls.Add(Me.rbtnPriceSourceFW)
        Me.gbDataSource.Controls.Add(Me.rbtnPriceSourceEM)
        Me.gbDataSource.Location = New System.Drawing.Point(271, 7)
        Me.gbDataSource.Name = "gbDataSource"
        Me.gbDataSource.Size = New System.Drawing.Size(180, 41)
        Me.gbDataSource.TabIndex = 18
        Me.gbDataSource.TabStop = False
        Me.gbDataSource.Text = "Data Source:"
        '
        'rbtnPriceSourceCCPData
        '
        Me.rbtnPriceSourceCCPData.AutoSize = True
        Me.rbtnPriceSourceCCPData.Location = New System.Drawing.Point(5, 19)
        Me.rbtnPriceSourceCCPData.Name = "rbtnPriceSourceCCPData"
        Me.rbtnPriceSourceCCPData.Size = New System.Drawing.Size(46, 17)
        Me.rbtnPriceSourceCCPData.TabIndex = 6
        Me.rbtnPriceSourceCCPData.Text = "CCP"
        Me.rbtnPriceSourceCCPData.UseVisualStyleBackColor = True
        '
        'rbtnPriceSourceEM
        '
        Me.rbtnPriceSourceEM.AutoSize = True
        Me.rbtnPriceSourceEM.Location = New System.Drawing.Point(57, 19)
        Me.rbtnPriceSourceEM.Name = "rbtnPriceSourceEM"
        Me.rbtnPriceSourceEM.Size = New System.Drawing.Size(67, 17)
        Me.rbtnPriceSourceEM.TabIndex = 5
        Me.rbtnPriceSourceEM.Text = "Marketer"
        Me.rbtnPriceSourceEM.UseVisualStyleBackColor = True
        '
        'cmbItemsSplitPrices
        '
        Me.cmbItemsSplitPrices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemsSplitPrices.FormattingEnabled = True
        Me.cmbItemsSplitPrices.Items.AddRange(New Object() {"Min Sell", "Max Sell", "Avg Sell", "Median Sell", "Percentile Sell", "Min Buy", "Max Buy", "Avg Buy", "Median Buy", "Percentile Buy", "Split Price"})
        Me.cmbItemsSplitPrices.Location = New System.Drawing.Point(8, 64)
        Me.cmbItemsSplitPrices.Name = "cmbItemsSplitPrices"
        Me.cmbItemsSplitPrices.Size = New System.Drawing.Size(131, 21)
        Me.cmbItemsSplitPrices.TabIndex = 2
        '
        'cmbRawMatsSplitPrices
        '
        Me.cmbRawMatsSplitPrices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRawMatsSplitPrices.FormattingEnabled = True
        Me.cmbRawMatsSplitPrices.Items.AddRange(New Object() {"Min Sell", "Max Sell", "Avg Sell", "Median Sell", "Percentile Sell", "Min Buy", "Max Buy", "Avg Buy", "Median Buy", "Percentile Buy", "Split Price"})
        Me.cmbRawMatsSplitPrices.Location = New System.Drawing.Point(8, 25)
        Me.cmbRawMatsSplitPrices.Name = "cmbRawMatsSplitPrices"
        Me.cmbRawMatsSplitPrices.Size = New System.Drawing.Size(131, 21)
        Me.cmbRawMatsSplitPrices.TabIndex = 1
        '
        'lblItemsSplitPrices
        '
        Me.lblItemsSplitPrices.AutoSize = True
        Me.lblItemsSplitPrices.Location = New System.Drawing.Point(5, 50)
        Me.lblItemsSplitPrices.Name = "lblItemsSplitPrices"
        Me.lblItemsSplitPrices.Size = New System.Drawing.Size(104, 13)
        Me.lblItemsSplitPrices.TabIndex = 6
        Me.lblItemsSplitPrices.Text = "Manufactured Items:"
        '
        'lblRawMatsSplitPrices
        '
        Me.lblRawMatsSplitPrices.AutoSize = True
        Me.lblRawMatsSplitPrices.Location = New System.Drawing.Point(5, 11)
        Me.lblRawMatsSplitPrices.Name = "lblRawMatsSplitPrices"
        Me.lblRawMatsSplitPrices.Size = New System.Drawing.Size(77, 13)
        Me.lblRawMatsSplitPrices.TabIndex = 7
        Me.lblRawMatsSplitPrices.Text = "Raw Materials:"
        '
        'btnSaveUpdatePrices
        '
        Me.btnSaveUpdatePrices.Location = New System.Drawing.Point(903, 578)
        Me.btnSaveUpdatePrices.Name = "btnSaveUpdatePrices"
        Me.btnSaveUpdatePrices.Size = New System.Drawing.Size(113, 32)
        Me.btnSaveUpdatePrices.TabIndex = 14
        Me.btnSaveUpdatePrices.Text = "Save Settings"
        Me.btnSaveUpdatePrices.UseVisualStyleBackColor = True
        '
        'btnCancelUpdate
        '
        Me.btnCancelUpdate.Location = New System.Drawing.Point(1017, 578)
        Me.btnCancelUpdate.Name = "btnCancelUpdate"
        Me.btnCancelUpdate.Size = New System.Drawing.Size(113, 32)
        Me.btnCancelUpdate.TabIndex = 13
        Me.btnCancelUpdate.Text = "Cancel Update"
        Me.btnCancelUpdate.UseVisualStyleBackColor = True
        '
        'btnClearItemFilter
        '
        Me.btnClearItemFilter.Location = New System.Drawing.Point(312, 583)
        Me.btnClearItemFilter.Name = "btnClearItemFilter"
        Me.btnClearItemFilter.Size = New System.Drawing.Size(59, 21)
        Me.btnClearItemFilter.TabIndex = 4
        Me.btnClearItemFilter.Text = "Clear"
        Me.btnClearItemFilter.UseVisualStyleBackColor = True
        '
        'btnToggleAllPriceItems
        '
        Me.btnToggleAllPriceItems.Location = New System.Drawing.Point(675, 578)
        Me.btnToggleAllPriceItems.Name = "btnToggleAllPriceItems"
        Me.btnToggleAllPriceItems.Size = New System.Drawing.Size(113, 32)
        Me.btnToggleAllPriceItems.TabIndex = 11
        Me.btnToggleAllPriceItems.Text = "Select All Items"
        Me.btnToggleAllPriceItems.UseVisualStyleBackColor = True
        '
        'btnDownloadPrices
        '
        Me.btnDownloadPrices.Location = New System.Drawing.Point(789, 578)
        Me.btnDownloadPrices.Name = "btnDownloadPrices"
        Me.btnDownloadPrices.Size = New System.Drawing.Size(113, 32)
        Me.btnDownloadPrices.TabIndex = 12
        Me.btnDownloadPrices.Text = "Download Prices"
        Me.btnDownloadPrices.UseVisualStyleBackColor = True
        '
        'lblItemFilter
        '
        Me.lblItemFilter.AutoSize = True
        Me.lblItemFilter.Location = New System.Drawing.Point(15, 588)
        Me.lblItemFilter.Name = "lblItemFilter"
        Me.lblItemFilter.Size = New System.Drawing.Size(55, 13)
        Me.lblItemFilter.TabIndex = 5
        Me.lblItemFilter.Text = "Item Filter:"
        '
        'gbManufacturedItems
        '
        Me.gbManufacturedItems.Controls.Add(Me.chkPriceManufacturedPrices)
        Me.gbManufacturedItems.Controls.Add(Me.gbComponents)
        Me.gbManufacturedItems.Controls.Add(Me.gbItems)
        Me.gbManufacturedItems.Location = New System.Drawing.Point(269, 337)
        Me.gbManufacturedItems.Name = "gbManufacturedItems"
        Me.gbManufacturedItems.Size = New System.Drawing.Size(399, 238)
        Me.gbManufacturedItems.TabIndex = 2
        Me.gbManufacturedItems.TabStop = False
        '
        'chkPriceManufacturedPrices
        '
        Me.chkPriceManufacturedPrices.AutoSize = True
        Me.chkPriceManufacturedPrices.BackColor = System.Drawing.Color.White
        Me.chkPriceManufacturedPrices.Location = New System.Drawing.Point(6, 1)
        Me.chkPriceManufacturedPrices.Name = "chkPriceManufacturedPrices"
        Me.chkPriceManufacturedPrices.Size = New System.Drawing.Size(120, 17)
        Me.chkPriceManufacturedPrices.TabIndex = 0
        Me.chkPriceManufacturedPrices.Text = "Manufactured Items"
        Me.chkPriceManufacturedPrices.UseVisualStyleBackColor = False
        '
        'gbComponents
        '
        Me.gbComponents.Controls.Add(Me.gbReprocessables)
        Me.gbComponents.Controls.Add(Me.chkFuelBlocks)
        Me.gbComponents.Controls.Add(Me.chkRAM)
        Me.gbComponents.Controls.Add(Me.chkProtectiveComponents)
        Me.gbComponents.Controls.Add(Me.chkSubsystemComponents)
        Me.gbComponents.Controls.Add(Me.chkStructureComponents)
        Me.gbComponents.Controls.Add(Me.chkComponents)
        Me.gbComponents.Controls.Add(Me.chkCapitalShipComponents)
        Me.gbComponents.Controls.Add(Me.chkCapT2Components)
        Me.gbComponents.Location = New System.Drawing.Point(5, 145)
        Me.gbComponents.Name = "gbComponents"
        Me.gbComponents.Size = New System.Drawing.Size(388, 88)
        Me.gbComponents.TabIndex = 2
        Me.gbComponents.TabStop = False
        Me.gbComponents.Text = "Components"
        '
        'gbReprocessables
        '
        Me.gbReprocessables.Controls.Add(Me.chkNoBuildItems)
        Me.gbReprocessables.Location = New System.Drawing.Point(283, 52)
        Me.gbReprocessables.Name = "gbReprocessables"
        Me.gbReprocessables.Size = New System.Drawing.Size(101, 32)
        Me.gbReprocessables.TabIndex = 19
        Me.gbReprocessables.TabStop = False
        '
        'chkNoBuildItems
        '
        Me.chkNoBuildItems.AutoSize = True
        Me.chkNoBuildItems.Location = New System.Drawing.Point(6, 11)
        Me.chkNoBuildItems.Name = "chkNoBuildItems"
        Me.chkNoBuildItems.Size = New System.Drawing.Size(94, 17)
        Me.chkNoBuildItems.TabIndex = 20
        Me.chkNoBuildItems.Text = "No Build Items"
        Me.chkNoBuildItems.UseVisualStyleBackColor = True
        '
        'chkFuelBlocks
        '
        Me.chkFuelBlocks.AutoSize = True
        Me.chkFuelBlocks.Location = New System.Drawing.Point(196, 49)
        Me.chkFuelBlocks.Name = "chkFuelBlocks"
        Me.chkFuelBlocks.Size = New System.Drawing.Size(81, 17)
        Me.chkFuelBlocks.TabIndex = 3
        Me.chkFuelBlocks.Text = "Fuel Blocks"
        Me.chkFuelBlocks.UseVisualStyleBackColor = True
        '
        'chkRAM
        '
        Me.chkRAM.AutoSize = True
        Me.chkRAM.Location = New System.Drawing.Point(196, 66)
        Me.chkRAM.Name = "chkRAM"
        Me.chkRAM.Size = New System.Drawing.Size(59, 17)
        Me.chkRAM.TabIndex = 1
        Me.chkRAM.Text = "R.A.M."
        Me.chkRAM.UseVisualStyleBackColor = True
        '
        'chkProtectiveComponents
        '
        Me.chkProtectiveComponents.AutoSize = True
        Me.chkProtectiveComponents.Location = New System.Drawing.Point(196, 32)
        Me.chkProtectiveComponents.Name = "chkProtectiveComponents"
        Me.chkProtectiveComponents.Size = New System.Drawing.Size(136, 17)
        Me.chkProtectiveComponents.TabIndex = 18
        Me.chkProtectiveComponents.Text = "Protective Components"
        Me.chkProtectiveComponents.UseVisualStyleBackColor = True
        '
        'chkSubsystemComponents
        '
        Me.chkSubsystemComponents.AutoSize = True
        Me.chkSubsystemComponents.Location = New System.Drawing.Point(9, 49)
        Me.chkSubsystemComponents.Name = "chkSubsystemComponents"
        Me.chkSubsystemComponents.Size = New System.Drawing.Size(139, 17)
        Me.chkSubsystemComponents.TabIndex = 5
        Me.chkSubsystemComponents.Text = "Subsystem Components"
        Me.chkSubsystemComponents.UseVisualStyleBackColor = True
        '
        'chkStructureComponents
        '
        Me.chkStructureComponents.AutoSize = True
        Me.chkStructureComponents.Location = New System.Drawing.Point(9, 66)
        Me.chkStructureComponents.Name = "chkStructureComponents"
        Me.chkStructureComponents.Size = New System.Drawing.Size(131, 17)
        Me.chkStructureComponents.TabIndex = 17
        Me.chkStructureComponents.Text = "Structure Components"
        Me.chkStructureComponents.UseVisualStyleBackColor = True
        '
        'chkComponents
        '
        Me.chkComponents.AutoSize = True
        Me.chkComponents.Location = New System.Drawing.Point(9, 32)
        Me.chkComponents.Name = "chkComponents"
        Me.chkComponents.Size = New System.Drawing.Size(137, 17)
        Me.chkComponents.TabIndex = 4
        Me.chkComponents.Text = "Advanced Components"
        Me.chkComponents.UseVisualStyleBackColor = True
        '
        'chkCapitalShipComponents
        '
        Me.chkCapitalShipComponents.AutoSize = True
        Me.chkCapitalShipComponents.Location = New System.Drawing.Point(196, 15)
        Me.chkCapitalShipComponents.Name = "chkCapitalShipComponents"
        Me.chkCapitalShipComponents.Size = New System.Drawing.Size(190, 17)
        Me.chkCapitalShipComponents.TabIndex = 3
        Me.chkCapitalShipComponents.Text = "Standard Capital Ship Components"
        Me.chkCapitalShipComponents.UseVisualStyleBackColor = True
        '
        'chkCapT2Components
        '
        Me.chkCapT2Components.AutoSize = True
        Me.chkCapT2Components.Location = New System.Drawing.Point(9, 15)
        Me.chkCapT2Components.Name = "chkCapT2Components"
        Me.chkCapT2Components.Size = New System.Drawing.Size(172, 17)
        Me.chkCapT2Components.TabIndex = 2
        Me.chkCapT2Components.Text = "Advanced Capital Components"
        Me.chkCapT2Components.UseVisualStyleBackColor = True
        '
        'gbItems
        '
        Me.gbItems.Controls.Add(Me.chkCelestials)
        Me.gbItems.Controls.Add(Me.chkDeployables)
        Me.gbItems.Controls.Add(Me.cmbPriceChargeTypes)
        Me.gbItems.Controls.Add(Me.chkStructures)
        Me.gbItems.Controls.Add(Me.chkStructureRigs)
        Me.gbItems.Controls.Add(Me.chkCharges)
        Me.gbItems.Controls.Add(Me.chkBoosters)
        Me.gbItems.Controls.Add(Me.cmbPriceShipTypes)
        Me.gbItems.Controls.Add(Me.gbPricesTech)
        Me.gbItems.Controls.Add(Me.chkSubsystems)
        Me.gbItems.Controls.Add(Me.chkShips)
        Me.gbItems.Controls.Add(Me.chkModules)
        Me.gbItems.Controls.Add(Me.chkRigs)
        Me.gbItems.Controls.Add(Me.chkDrones)
        Me.gbItems.Controls.Add(Me.chkUpdatePricesNoPrice)
        Me.gbItems.Controls.Add(Me.chkImplants)
        Me.gbItems.Controls.Add(Me.chkStructureModules)
        Me.gbItems.Location = New System.Drawing.Point(5, 16)
        Me.gbItems.Name = "gbItems"
        Me.gbItems.Size = New System.Drawing.Size(388, 128)
        Me.gbItems.TabIndex = 1
        Me.gbItems.TabStop = False
        Me.gbItems.Text = "Items"
        '
        'chkCelestials
        '
        Me.chkCelestials.AutoSize = True
        Me.chkCelestials.Location = New System.Drawing.Point(196, 92)
        Me.chkCelestials.Name = "chkCelestials"
        Me.chkCelestials.Size = New System.Drawing.Size(70, 17)
        Me.chkCelestials.TabIndex = 13
        Me.chkCelestials.Text = "Celestials"
        Me.chkCelestials.UseVisualStyleBackColor = True
        '
        'chkDeployables
        '
        Me.chkDeployables.AutoSize = True
        Me.chkDeployables.Location = New System.Drawing.Point(94, 75)
        Me.chkDeployables.Name = "chkDeployables"
        Me.chkDeployables.Size = New System.Drawing.Size(84, 17)
        Me.chkDeployables.TabIndex = 9
        Me.chkDeployables.Text = "Deployables"
        Me.chkDeployables.UseVisualStyleBackColor = True
        '
        'cmbPriceChargeTypes
        '
        Me.cmbPriceChargeTypes.FormattingEnabled = True
        Me.cmbPriceChargeTypes.Location = New System.Drawing.Point(71, 34)
        Me.cmbPriceChargeTypes.Name = "cmbPriceChargeTypes"
        Me.cmbPriceChargeTypes.Size = New System.Drawing.Size(211, 21)
        Me.cmbPriceChargeTypes.TabIndex = 5
        Me.cmbPriceChargeTypes.Text = "All Charge Types"
        '
        'chkStructures
        '
        Me.chkStructures.AutoSize = True
        Me.chkStructures.Location = New System.Drawing.Point(9, 92)
        Me.chkStructures.Name = "chkStructures"
        Me.chkStructures.Size = New System.Drawing.Size(74, 17)
        Me.chkStructures.TabIndex = 12
        Me.chkStructures.Text = "Structures"
        Me.chkStructures.UseVisualStyleBackColor = True
        '
        'chkStructureRigs
        '
        Me.chkStructureRigs.AutoSize = True
        Me.chkStructureRigs.Location = New System.Drawing.Point(94, 92)
        Me.chkStructureRigs.Name = "chkStructureRigs"
        Me.chkStructureRigs.Size = New System.Drawing.Size(93, 17)
        Me.chkStructureRigs.TabIndex = 14
        Me.chkStructureRigs.Text = "Structure Rigs"
        Me.chkStructureRigs.UseVisualStyleBackColor = True
        '
        'chkCharges
        '
        Me.chkCharges.AutoSize = True
        Me.chkCharges.Location = New System.Drawing.Point(9, 36)
        Me.chkCharges.Name = "chkCharges"
        Me.chkCharges.Size = New System.Drawing.Size(65, 17)
        Me.chkCharges.TabIndex = 4
        Me.chkCharges.Text = "Charges"
        Me.chkCharges.UseVisualStyleBackColor = True
        '
        'chkBoosters
        '
        Me.chkBoosters.AutoSize = True
        Me.chkBoosters.Location = New System.Drawing.Point(196, 75)
        Me.chkBoosters.Name = "chkBoosters"
        Me.chkBoosters.Size = New System.Drawing.Size(67, 17)
        Me.chkBoosters.TabIndex = 11
        Me.chkBoosters.Text = "Boosters"
        Me.chkBoosters.UseVisualStyleBackColor = True
        '
        'cmbPriceShipTypes
        '
        Me.cmbPriceShipTypes.FormattingEnabled = True
        Me.cmbPriceShipTypes.Location = New System.Drawing.Point(71, 12)
        Me.cmbPriceShipTypes.Name = "cmbPriceShipTypes"
        Me.cmbPriceShipTypes.Size = New System.Drawing.Size(211, 21)
        Me.cmbPriceShipTypes.TabIndex = 3
        Me.cmbPriceShipTypes.Text = "All Ship Types"
        '
        'gbPricesTech
        '
        Me.gbPricesTech.Controls.Add(Me.chkPricesT4)
        Me.gbPricesTech.Controls.Add(Me.chkPricesT6)
        Me.gbPricesTech.Controls.Add(Me.chkPricesT5)
        Me.gbPricesTech.Controls.Add(Me.chkPricesT3)
        Me.gbPricesTech.Controls.Add(Me.chkPricesT2)
        Me.gbPricesTech.Controls.Add(Me.chkPricesT1)
        Me.gbPricesTech.Location = New System.Drawing.Point(288, 7)
        Me.gbPricesTech.Name = "gbPricesTech"
        Me.gbPricesTech.Size = New System.Drawing.Size(94, 114)
        Me.gbPricesTech.TabIndex = 15
        Me.gbPricesTech.TabStop = False
        '
        'chkPricesT4
        '
        Me.chkPricesT4.AutoSize = True
        Me.chkPricesT4.Enabled = False
        Me.chkPricesT4.Location = New System.Drawing.Point(6, 60)
        Me.chkPricesT4.Name = "chkPricesT4"
        Me.chkPricesT4.Size = New System.Drawing.Size(66, 17)
        Me.chkPricesT4.TabIndex = 3
        Me.chkPricesT4.Text = "Storyline"
        Me.chkPricesT4.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkPricesT4.UseVisualStyleBackColor = True
        '
        'chkPricesT6
        '
        Me.chkPricesT6.AutoSize = True
        Me.chkPricesT6.Enabled = False
        Me.chkPricesT6.Location = New System.Drawing.Point(6, 94)
        Me.chkPricesT6.Name = "chkPricesT6"
        Me.chkPricesT6.Size = New System.Drawing.Size(91, 17)
        Me.chkPricesT6.TabIndex = 9
        Me.chkPricesT6.Text = "Pirate Faction"
        Me.chkPricesT6.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkPricesT6.UseVisualStyleBackColor = True
        '
        'chkPricesT5
        '
        Me.chkPricesT5.AutoSize = True
        Me.chkPricesT5.Enabled = False
        Me.chkPricesT5.Location = New System.Drawing.Point(6, 77)
        Me.chkPricesT5.Name = "chkPricesT5"
        Me.chkPricesT5.Size = New System.Drawing.Size(89, 17)
        Me.chkPricesT5.TabIndex = 8
        Me.chkPricesT5.Text = "Navy Faction"
        Me.chkPricesT5.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkPricesT5.UseVisualStyleBackColor = True
        '
        'chkPricesT3
        '
        Me.chkPricesT3.AutoSize = True
        Me.chkPricesT3.Enabled = False
        Me.chkPricesT3.Location = New System.Drawing.Point(6, 43)
        Me.chkPricesT3.Name = "chkPricesT3"
        Me.chkPricesT3.Size = New System.Drawing.Size(60, 17)
        Me.chkPricesT3.TabIndex = 2
        Me.chkPricesT3.Text = "Tech 3"
        Me.chkPricesT3.UseVisualStyleBackColor = True
        '
        'chkPricesT2
        '
        Me.chkPricesT2.AutoSize = True
        Me.chkPricesT2.Enabled = False
        Me.chkPricesT2.Location = New System.Drawing.Point(6, 26)
        Me.chkPricesT2.Name = "chkPricesT2"
        Me.chkPricesT2.Size = New System.Drawing.Size(60, 17)
        Me.chkPricesT2.TabIndex = 1
        Me.chkPricesT2.Text = "Tech 2"
        Me.chkPricesT2.UseVisualStyleBackColor = True
        '
        'chkPricesT1
        '
        Me.chkPricesT1.AutoSize = True
        Me.chkPricesT1.Enabled = False
        Me.chkPricesT1.Location = New System.Drawing.Point(6, 9)
        Me.chkPricesT1.Name = "chkPricesT1"
        Me.chkPricesT1.Size = New System.Drawing.Size(60, 17)
        Me.chkPricesT1.TabIndex = 0
        Me.chkPricesT1.Text = "Tech 1"
        Me.chkPricesT1.UseVisualStyleBackColor = True
        '
        'chkSubsystems
        '
        Me.chkSubsystems.AutoSize = True
        Me.chkSubsystems.Location = New System.Drawing.Point(9, 75)
        Me.chkSubsystems.Name = "chkSubsystems"
        Me.chkSubsystems.Size = New System.Drawing.Size(82, 17)
        Me.chkSubsystems.TabIndex = 10
        Me.chkSubsystems.Text = "Subsystems"
        Me.chkSubsystems.UseVisualStyleBackColor = True
        '
        'chkShips
        '
        Me.chkShips.AutoSize = True
        Me.chkShips.Location = New System.Drawing.Point(9, 14)
        Me.chkShips.Name = "chkShips"
        Me.chkShips.Size = New System.Drawing.Size(52, 17)
        Me.chkShips.TabIndex = 2
        Me.chkShips.Text = "Ships"
        Me.chkShips.UseVisualStyleBackColor = True
        '
        'chkModules
        '
        Me.chkModules.AutoSize = True
        Me.chkModules.Location = New System.Drawing.Point(9, 58)
        Me.chkModules.Name = "chkModules"
        Me.chkModules.Size = New System.Drawing.Size(66, 17)
        Me.chkModules.TabIndex = 6
        Me.chkModules.Text = "Modules"
        Me.chkModules.UseVisualStyleBackColor = True
        '
        'chkRigs
        '
        Me.chkRigs.AutoSize = True
        Me.chkRigs.Location = New System.Drawing.Point(196, 58)
        Me.chkRigs.Name = "chkRigs"
        Me.chkRigs.Size = New System.Drawing.Size(47, 17)
        Me.chkRigs.TabIndex = 8
        Me.chkRigs.Text = "Rigs"
        Me.chkRigs.UseVisualStyleBackColor = True
        '
        'chkDrones
        '
        Me.chkDrones.AutoSize = True
        Me.chkDrones.Location = New System.Drawing.Point(94, 58)
        Me.chkDrones.Name = "chkDrones"
        Me.chkDrones.Size = New System.Drawing.Size(60, 17)
        Me.chkDrones.TabIndex = 7
        Me.chkDrones.Text = "Drones"
        Me.chkDrones.UseVisualStyleBackColor = True
        '
        'chkUpdatePricesNoPrice
        '
        Me.chkUpdatePricesNoPrice.AutoSize = True
        Me.chkUpdatePricesNoPrice.Location = New System.Drawing.Point(182, 109)
        Me.chkUpdatePricesNoPrice.Name = "chkUpdatePricesNoPrice"
        Me.chkUpdatePricesNoPrice.Size = New System.Drawing.Size(108, 17)
        Me.chkUpdatePricesNoPrice.TabIndex = 6
        Me.chkUpdatePricesNoPrice.Text = "Items w/No Price"
        Me.chkUpdatePricesNoPrice.UseVisualStyleBackColor = True
        '
        'chkImplants
        '
        Me.chkImplants.AutoSize = True
        Me.chkImplants.Location = New System.Drawing.Point(119, 109)
        Me.chkImplants.Name = "chkImplants"
        Me.chkImplants.Size = New System.Drawing.Size(65, 17)
        Me.chkImplants.TabIndex = 4
        Me.chkImplants.Text = "Implants"
        Me.chkImplants.UseVisualStyleBackColor = True
        '
        'chkStructureModules
        '
        Me.chkStructureModules.AutoSize = True
        Me.chkStructureModules.Location = New System.Drawing.Point(9, 109)
        Me.chkStructureModules.Name = "chkStructureModules"
        Me.chkStructureModules.Size = New System.Drawing.Size(112, 17)
        Me.chkStructureModules.TabIndex = 16
        Me.chkStructureModules.Text = "Structure Modules"
        Me.chkStructureModules.UseVisualStyleBackColor = True
        '
        'btnOpenMarketBrowser
        '
        Me.btnOpenMarketBrowser.Location = New System.Drawing.Point(377, 578)
        Me.btnOpenMarketBrowser.Name = "btnOpenMarketBrowser"
        Me.btnOpenMarketBrowser.Size = New System.Drawing.Size(119, 32)
        Me.btnOpenMarketBrowser.TabIndex = 127
        Me.btnOpenMarketBrowser.Text = "Open Market Browser"
        Me.btnOpenMarketBrowser.UseVisualStyleBackColor = True
        '
        'tabBlueprints
        '
        Me.tabBlueprints.BackColor = System.Drawing.Color.Transparent
        Me.tabBlueprints.Controls.Add(Me.pbReactions)
        Me.tabBlueprints.Controls.Add(Me.gbBPMEPEImage)
        Me.tabBlueprints.Controls.Add(Me.btnBPBuiltComponents)
        Me.tabBlueprints.Controls.Add(Me.btnBPComponents)
        Me.tabBlueprints.Controls.Add(Me.rbtnBPRawT2MatType)
        Me.tabBlueprints.Controls.Add(Me.rbtnBPProcT2MatType)
        Me.tabBlueprints.Controls.Add(Me.rbtnBPAdvT2MatType)
        Me.tabBlueprints.Controls.Add(Me.lblBPT2MatTypeSelector)
        Me.tabBlueprints.Controls.Add(Me.lstBPList)
        Me.tabBlueprints.Controls.Add(Me.gbBPBlueprintType)
        Me.tabBlueprints.Controls.Add(Me.gbBPBlueprintTech)
        Me.tabBlueprints.Controls.Add(Me.gbFilters)
        Me.tabBlueprints.Controls.Add(Me.cmbBPBlueprintSelection)
        Me.tabBlueprints.Controls.Add(Me.btnBPListView)
        Me.tabBlueprints.Controls.Add(Me.btnBPForward)
        Me.tabBlueprints.Controls.Add(Me.btnBPBack)
        Me.tabBlueprints.Controls.Add(Me.lblBPSelectBlueprint)
        Me.tabBlueprints.Controls.Add(Me.gbBPInventionStats)
        Me.tabBlueprints.Controls.Add(Me.lblBPBuyColor)
        Me.tabBlueprints.Controls.Add(Me.lblBPBuildColor)
        Me.tabBlueprints.Controls.Add(Me.gbBPShopandCopy)
        Me.tabBlueprints.Controls.Add(Me.lblBPCanMakeBPAll)
        Me.tabBlueprints.Controls.Add(Me.lblBPRawMatCost)
        Me.tabBlueprints.Controls.Add(Me.lblBPRawMatCost1)
        Me.tabBlueprints.Controls.Add(Me.lblBPCanMakeBP)
        Me.tabBlueprints.Controls.Add(Me.lblBPRawMats)
        Me.tabBlueprints.Controls.Add(Me.lblBPComponentMatCost)
        Me.tabBlueprints.Controls.Add(Me.lblBPComponentMats)
        Me.tabBlueprints.Controls.Add(Me.lblBPComponentMatCost1)
        Me.tabBlueprints.Controls.Add(Me.lstBPComponentMats)
        Me.tabBlueprints.Controls.Add(Me.lstBPRawMats)
        Me.tabBlueprints.Controls.Add(Me.lstBPBuiltComponents)
        Me.tabBlueprints.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tabBlueprints.Location = New System.Drawing.Point(4, 22)
        Me.tabBlueprints.Name = "tabBlueprints"
        Me.tabBlueprints.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBlueprints.Size = New System.Drawing.Size(1137, 615)
        Me.tabBlueprints.TabIndex = 0
        Me.tabBlueprints.Text = "Blueprints"
        Me.tabBlueprints.UseVisualStyleBackColor = True
        '
        'pbReactions
        '
        Me.pbReactions.Image = Global.EVE_Isk_per_Hour.My.Resources.Resources._16654_32
        Me.pbReactions.Location = New System.Drawing.Point(1055, 235)
        Me.pbReactions.Name = "pbReactions"
        Me.pbReactions.Size = New System.Drawing.Size(32, 32)
        Me.pbReactions.TabIndex = 139
        Me.pbReactions.TabStop = False
        Me.pbReactions.Visible = False
        '
        'gbBPMEPEImage
        '
        Me.gbBPMEPEImage.BackColor = System.Drawing.Color.Transparent
        Me.gbBPMEPEImage.Controls.Add(Me.gbBPSellExcess)
        Me.gbBPMEPEImage.Controls.Add(Me.btnBPSaveBP)
        Me.gbBPMEPEImage.Controls.Add(Me.tabBPInventionEquip)
        Me.gbBPMEPEImage.Controls.Add(Me.btnBPSaveSettings)
        Me.gbBPMEPEImage.Controls.Add(Me.txtBPLines)
        Me.gbBPMEPEImage.Controls.Add(Me.pictBP)
        Me.gbBPMEPEImage.Controls.Add(Me.gbBPManualSystemCostIndex)
        Me.gbBPMEPEImage.Controls.Add(Me.txtBPNumBPs)
        Me.gbBPMEPEImage.Controls.Add(Me.btnBPRefreshBP)
        Me.gbBPMEPEImage.Controls.Add(Me.lblBPLines)
        Me.gbBPMEPEImage.Controls.Add(Me.txtBPME)
        Me.gbBPMEPEImage.Controls.Add(Me.lblBPRuns)
        Me.gbBPMEPEImage.Controls.Add(Me.chkBPBuildBuy)
        Me.gbBPMEPEImage.Controls.Add(Me.txtBPRuns)
        Me.gbBPMEPEImage.Controls.Add(Me.txtBPAddlCosts)
        Me.gbBPMEPEImage.Controls.Add(Me.lblBPAddlCosts)
        Me.gbBPMEPEImage.Controls.Add(Me.lblBPME)
        Me.gbBPMEPEImage.Controls.Add(Me.txtBPTE)
        Me.gbBPMEPEImage.Controls.Add(Me.lblBPPE)
        Me.gbBPMEPEImage.Controls.Add(Me.lblBPNumBPs)
        Me.gbBPMEPEImage.Controls.Add(Me.gbBPIgnoreinCalcs)
        Me.gbBPMEPEImage.Location = New System.Drawing.Point(392, 8)
        Me.gbBPMEPEImage.Name = "gbBPMEPEImage"
        Me.gbBPMEPEImage.Size = New System.Drawing.Size(455, 224)
        Me.gbBPMEPEImage.TabIndex = 6
        Me.gbBPMEPEImage.TabStop = False
        '
        'gbBPSellExcess
        '
        Me.gbBPSellExcess.Controls.Add(Me.btnBPListMats)
        Me.gbBPSellExcess.Controls.Add(Me.chkBPSellExcessItems)
        Me.gbBPSellExcess.Location = New System.Drawing.Point(238, 7)
        Me.gbBPSellExcess.Name = "gbBPSellExcess"
        Me.gbBPSellExcess.Size = New System.Drawing.Size(82, 60)
        Me.gbBPSellExcess.TabIndex = 21
        Me.gbBPSellExcess.TabStop = False
        '
        'btnBPListMats
        '
        Me.btnBPListMats.Location = New System.Drawing.Point(6, 35)
        Me.btnBPListMats.Name = "btnBPListMats"
        Me.btnBPListMats.Size = New System.Drawing.Size(72, 22)
        Me.btnBPListMats.TabIndex = 24
        Me.btnBPListMats.Text = "List Mats"
        Me.btnBPListMats.UseVisualStyleBackColor = True
        '
        'chkBPSellExcessItems
        '
        Me.chkBPSellExcessItems.Location = New System.Drawing.Point(3, 7)
        Me.chkBPSellExcessItems.Name = "chkBPSellExcessItems"
        Me.chkBPSellExcessItems.Size = New System.Drawing.Size(79, 32)
        Me.chkBPSellExcessItems.TabIndex = 1
        Me.chkBPSellExcessItems.Text = "Sell excess build items"
        Me.chkBPSellExcessItems.UseVisualStyleBackColor = True
        '
        'btnBPSaveBP
        '
        Me.btnBPSaveBP.Location = New System.Drawing.Point(6, 154)
        Me.btnBPSaveBP.Name = "btnBPSaveBP"
        Me.btnBPSaveBP.Size = New System.Drawing.Size(45, 34)
        Me.btnBPSaveBP.TabIndex = 17
        Me.btnBPSaveBP.Text = "Save BP"
        Me.btnBPSaveBP.UseVisualStyleBackColor = True
        '
        'tabBPInventionEquip
        '
        Me.tabBPInventionEquip.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.tabBPInventionEquip.Controls.Add(Me.tabFacility)
        Me.tabBPInventionEquip.Controls.Add(Me.tabT3Calcs)
        Me.tabBPInventionEquip.Controls.Add(Me.tabInventionCalcs)
        Me.tabBPInventionEquip.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tabBPInventionEquip.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.tabBPInventionEquip.ItemSize = New System.Drawing.Size(49, 20)
        Me.tabBPInventionEquip.Location = New System.Drawing.Point(141, 68)
        Me.tabBPInventionEquip.Multiline = True
        Me.tabBPInventionEquip.Name = "tabBPInventionEquip"
        Me.tabBPInventionEquip.Padding = New System.Drawing.Point(0, 0)
        Me.tabBPInventionEquip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tabBPInventionEquip.SelectedIndex = 0
        Me.tabBPInventionEquip.Size = New System.Drawing.Size(309, 150)
        Me.tabBPInventionEquip.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tabBPInventionEquip.TabIndex = 16
        '
        'tabFacility
        '
        Me.tabFacility.Controls.Add(Me.BPTabFacility)
        Me.tabFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabFacility.Location = New System.Drawing.Point(4, 4)
        Me.tabFacility.Margin = New System.Windows.Forms.Padding(0)
        Me.tabFacility.Name = "tabFacility"
        Me.tabFacility.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFacility.Size = New System.Drawing.Size(261, 142)
        Me.tabFacility.TabIndex = 1
        Me.tabFacility.Text = "Facility"
        Me.tabFacility.UseVisualStyleBackColor = True
        '
        'BPTabFacility
        '
        Me.BPTabFacility.Location = New System.Drawing.Point(0, 0)
        Me.BPTabFacility.Name = "BPTabFacility"
        Me.BPTabFacility.Size = New System.Drawing.Size(280, 142)
        Me.BPTabFacility.TabIndex = 0
        '
        'tabT3Calcs
        '
        Me.tabT3Calcs.Controls.Add(Me.chkBPOptimalT3Decryptor)
        Me.tabT3Calcs.Controls.Add(Me.lblBPT3Decryptor)
        Me.tabT3Calcs.Controls.Add(Me.cmbBPT3Decryptor)
        Me.tabT3Calcs.Controls.Add(Me.lblBPT3Stats)
        Me.tabT3Calcs.Controls.Add(Me.lblBPRelic)
        Me.tabT3Calcs.Controls.Add(Me.txtBPRelicLines)
        Me.tabT3Calcs.Controls.Add(Me.lblBPRelicLines)
        Me.tabT3Calcs.Controls.Add(Me.lblBPRETime)
        Me.tabT3Calcs.Controls.Add(Me.cmbBPRelic)
        Me.tabT3Calcs.Controls.Add(Me.lblBPRECost)
        Me.tabT3Calcs.Controls.Add(Me.lblBPT3InventionChance)
        Me.tabT3Calcs.Controls.Add(Me.lblBPT3InventionChance1)
        Me.tabT3Calcs.Controls.Add(Me.lblT3InventStatus)
        Me.tabT3Calcs.Controls.Add(Me.chkBPIncludeT3Time)
        Me.tabT3Calcs.Controls.Add(Me.chkBPIncludeT3Costs)
        Me.tabT3Calcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabT3Calcs.Location = New System.Drawing.Point(4, 4)
        Me.tabT3Calcs.Name = "tabT3Calcs"
        Me.tabT3Calcs.Size = New System.Drawing.Size(261, 142)
        Me.tabT3Calcs.TabIndex = 2
        Me.tabT3Calcs.Text = "Invention"
        Me.tabT3Calcs.UseVisualStyleBackColor = True
        '
        'chkBPOptimalT3Decryptor
        '
        Me.chkBPOptimalT3Decryptor.Location = New System.Drawing.Point(6, 94)
        Me.chkBPOptimalT3Decryptor.Name = "chkBPOptimalT3Decryptor"
        Me.chkBPOptimalT3Decryptor.Size = New System.Drawing.Size(61, 45)
        Me.chkBPOptimalT3Decryptor.TabIndex = 55
        Me.chkBPOptimalT3Decryptor.Text = "Optimal"
        Me.chkBPOptimalT3Decryptor.ThreeState = True
        Me.chkBPOptimalT3Decryptor.UseVisualStyleBackColor = True
        '
        'lblBPT3Decryptor
        '
        Me.lblBPT3Decryptor.AutoSize = True
        Me.lblBPT3Decryptor.Location = New System.Drawing.Point(69, 94)
        Me.lblBPT3Decryptor.Name = "lblBPT3Decryptor"
        Me.lblBPT3Decryptor.Size = New System.Drawing.Size(89, 13)
        Me.lblBPT3Decryptor.TabIndex = 53
        Me.lblBPT3Decryptor.Text = "Select Decryptor:"
        '
        'cmbBPT3Decryptor
        '
        Me.cmbBPT3Decryptor.FormattingEnabled = True
        Me.cmbBPT3Decryptor.ItemHeight = 13
        Me.cmbBPT3Decryptor.Location = New System.Drawing.Point(71, 108)
        Me.cmbBPT3Decryptor.Name = "cmbBPT3Decryptor"
        Me.cmbBPT3Decryptor.Size = New System.Drawing.Size(206, 21)
        Me.cmbBPT3Decryptor.TabIndex = 54
        '
        'lblBPT3Stats
        '
        Me.lblBPT3Stats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPT3Stats.Location = New System.Drawing.Point(199, 2)
        Me.lblBPT3Stats.Name = "lblBPT3Stats"
        Me.lblBPT3Stats.Size = New System.Drawing.Size(78, 30)
        Me.lblBPT3Stats.TabIndex = 52
        Me.lblBPT3Stats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRelic
        '
        Me.lblBPRelic.AutoSize = True
        Me.lblBPRelic.Location = New System.Drawing.Point(38, 22)
        Me.lblBPRelic.Name = "lblBPRelic"
        Me.lblBPRelic.Size = New System.Drawing.Size(67, 13)
        Me.lblBPRelic.TabIndex = 41
        Me.lblBPRelic.Text = "Select Relic:"
        '
        'txtBPRelicLines
        '
        Me.txtBPRelicLines.Location = New System.Drawing.Point(4, 36)
        Me.txtBPRelicLines.MaxLength = 4
        Me.txtBPRelicLines.Name = "txtBPRelicLines"
        Me.txtBPRelicLines.Size = New System.Drawing.Size(31, 20)
        Me.txtBPRelicLines.TabIndex = 42
        '
        'lblBPRelicLines
        '
        Me.lblBPRelicLines.AutoSize = True
        Me.lblBPRelicLines.Location = New System.Drawing.Point(2, 22)
        Me.lblBPRelicLines.Name = "lblBPRelicLines"
        Me.lblBPRelicLines.Size = New System.Drawing.Size(35, 13)
        Me.lblBPRelicLines.TabIndex = 40
        Me.lblBPRelicLines.Text = "Lines:"
        '
        'lblBPRETime
        '
        Me.lblBPRETime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRETime.Location = New System.Drawing.Point(179, 75)
        Me.lblBPRETime.Name = "lblBPRETime"
        Me.lblBPRETime.Size = New System.Drawing.Size(98, 17)
        Me.lblBPRETime.TabIndex = 50
        Me.lblBPRETime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbBPRelic
        '
        Me.cmbBPRelic.FormattingEnabled = True
        Me.cmbBPRelic.ItemHeight = 13
        Me.cmbBPRelic.Items.AddRange(New Object() {"Wrecked", "Malfunctioning", "Intact"})
        Me.cmbBPRelic.Location = New System.Drawing.Point(41, 36)
        Me.cmbBPRelic.Name = "cmbBPRelic"
        Me.cmbBPRelic.Size = New System.Drawing.Size(236, 21)
        Me.cmbBPRelic.TabIndex = 43
        '
        'lblBPRECost
        '
        Me.lblBPRECost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRECost.Location = New System.Drawing.Point(71, 75)
        Me.lblBPRECost.Name = "lblBPRECost"
        Me.lblBPRECost.Size = New System.Drawing.Size(105, 17)
        Me.lblBPRECost.TabIndex = 48
        Me.lblBPRECost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPT3InventionChance
        '
        Me.lblBPT3InventionChance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPT3InventionChance.Location = New System.Drawing.Point(4, 75)
        Me.lblBPT3InventionChance.Name = "lblBPT3InventionChance"
        Me.lblBPT3InventionChance.Size = New System.Drawing.Size(63, 17)
        Me.lblBPT3InventionChance.TabIndex = 46
        Me.lblBPT3InventionChance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPT3InventionChance1
        '
        Me.lblBPT3InventionChance1.AutoSize = True
        Me.lblBPT3InventionChance1.Location = New System.Drawing.Point(2, 60)
        Me.lblBPT3InventionChance1.Name = "lblBPT3InventionChance1"
        Me.lblBPT3InventionChance1.Size = New System.Drawing.Size(47, 13)
        Me.lblBPT3InventionChance1.TabIndex = 45
        Me.lblBPT3InventionChance1.Text = "Chance:"
        '
        'lblT3InventStatus
        '
        Me.lblT3InventStatus.ForeColor = System.Drawing.Color.Black
        Me.lblT3InventStatus.Location = New System.Drawing.Point(1, 4)
        Me.lblT3InventStatus.Name = "lblT3InventStatus"
        Me.lblT3InventStatus.Size = New System.Drawing.Size(185, 13)
        Me.lblT3InventStatus.TabIndex = 37
        Me.lblT3InventStatus.Text = "T3 Invention Calculations:"
        '
        'chkBPIncludeT3Time
        '
        Me.chkBPIncludeT3Time.AutoSize = True
        Me.chkBPIncludeT3Time.Location = New System.Drawing.Point(179, 59)
        Me.chkBPIncludeT3Time.Name = "chkBPIncludeT3Time"
        Me.chkBPIncludeT3Time.Size = New System.Drawing.Size(99, 17)
        Me.chkBPIncludeT3Time.TabIndex = 49
        Me.chkBPIncludeT3Time.Text = "Invention Time:"
        Me.chkBPIncludeT3Time.UseVisualStyleBackColor = True
        '
        'chkBPIncludeT3Costs
        '
        Me.chkBPIncludeT3Costs.AutoSize = True
        Me.chkBPIncludeT3Costs.Location = New System.Drawing.Point(71, 59)
        Me.chkBPIncludeT3Costs.Name = "chkBPIncludeT3Costs"
        Me.chkBPIncludeT3Costs.Size = New System.Drawing.Size(102, 17)
        Me.chkBPIncludeT3Costs.TabIndex = 47
        Me.chkBPIncludeT3Costs.Text = "Invention Costs:"
        Me.chkBPIncludeT3Costs.UseVisualStyleBackColor = True
        '
        'tabInventionCalcs
        '
        Me.tabInventionCalcs.Controls.Add(Me.chkBPOptimalT2Decryptor)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPCopyTime)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPT2InventStatus)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPCopyCosts)
        Me.tabInventionCalcs.Controls.Add(Me.txtBPInventionLines)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPInventionLines)
        Me.tabInventionCalcs.Controls.Add(Me.lblInventionChance1)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPDecryptor)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPInventionTime)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPDecryptorStats)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPInventionCost)
        Me.tabInventionCalcs.Controls.Add(Me.cmbBPInventionDecryptor)
        Me.tabInventionCalcs.Controls.Add(Me.lblBPInventionChance)
        Me.tabInventionCalcs.Controls.Add(Me.chkBPIncludeInventionTime)
        Me.tabInventionCalcs.Controls.Add(Me.chkBPIncludeCopyTime)
        Me.tabInventionCalcs.Controls.Add(Me.chkBPIncludeCopyCosts)
        Me.tabInventionCalcs.Controls.Add(Me.chkBPIncludeInventionCosts)
        Me.tabInventionCalcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabInventionCalcs.Location = New System.Drawing.Point(4, 4)
        Me.tabInventionCalcs.Margin = New System.Windows.Forms.Padding(0)
        Me.tabInventionCalcs.Name = "tabInventionCalcs"
        Me.tabInventionCalcs.Padding = New System.Windows.Forms.Padding(3)
        Me.tabInventionCalcs.Size = New System.Drawing.Size(261, 142)
        Me.tabInventionCalcs.TabIndex = 0
        Me.tabInventionCalcs.Text = "Invention"
        Me.tabInventionCalcs.UseVisualStyleBackColor = True
        '
        'chkBPOptimalT2Decryptor
        '
        Me.chkBPOptimalT2Decryptor.Location = New System.Drawing.Point(6, 94)
        Me.chkBPOptimalT2Decryptor.Name = "chkBPOptimalT2Decryptor"
        Me.chkBPOptimalT2Decryptor.Size = New System.Drawing.Size(61, 45)
        Me.chkBPOptimalT2Decryptor.TabIndex = 37
        Me.chkBPOptimalT2Decryptor.Text = "Optimal"
        Me.chkBPOptimalT2Decryptor.ThreeState = True
        Me.chkBPOptimalT2Decryptor.UseVisualStyleBackColor = True
        '
        'lblBPCopyTime
        '
        Me.lblBPCopyTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPCopyTime.Location = New System.Drawing.Point(179, 111)
        Me.lblBPCopyTime.Name = "lblBPCopyTime"
        Me.lblBPCopyTime.Size = New System.Drawing.Size(99, 17)
        Me.lblBPCopyTime.TabIndex = 36
        Me.lblBPCopyTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPT2InventStatus
        '
        Me.lblBPT2InventStatus.AutoSize = True
        Me.lblBPT2InventStatus.ForeColor = System.Drawing.Color.Black
        Me.lblBPT2InventStatus.Location = New System.Drawing.Point(1, 4)
        Me.lblBPT2InventStatus.Name = "lblBPT2InventStatus"
        Me.lblBPT2InventStatus.Size = New System.Drawing.Size(114, 13)
        Me.lblBPT2InventStatus.TabIndex = 19
        Me.lblBPT2InventStatus.Text = "Invention Calculations:"
        '
        'lblBPCopyCosts
        '
        Me.lblBPCopyCosts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPCopyCosts.Location = New System.Drawing.Point(71, 111)
        Me.lblBPCopyCosts.Name = "lblBPCopyCosts"
        Me.lblBPCopyCosts.Size = New System.Drawing.Size(105, 17)
        Me.lblBPCopyCosts.TabIndex = 34
        Me.lblBPCopyCosts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBPInventionLines
        '
        Me.txtBPInventionLines.Location = New System.Drawing.Point(4, 36)
        Me.txtBPInventionLines.MaxLength = 4
        Me.txtBPInventionLines.Name = "txtBPInventionLines"
        Me.txtBPInventionLines.Size = New System.Drawing.Size(31, 20)
        Me.txtBPInventionLines.TabIndex = 24
        '
        'lblBPInventionLines
        '
        Me.lblBPInventionLines.AutoSize = True
        Me.lblBPInventionLines.Location = New System.Drawing.Point(3, 22)
        Me.lblBPInventionLines.Name = "lblBPInventionLines"
        Me.lblBPInventionLines.Size = New System.Drawing.Size(35, 13)
        Me.lblBPInventionLines.TabIndex = 23
        Me.lblBPInventionLines.Text = "Lines:"
        '
        'lblInventionChance1
        '
        Me.lblInventionChance1.AutoSize = True
        Me.lblInventionChance1.Location = New System.Drawing.Point(2, 60)
        Me.lblInventionChance1.Name = "lblInventionChance1"
        Me.lblInventionChance1.Size = New System.Drawing.Size(47, 13)
        Me.lblInventionChance1.TabIndex = 27
        Me.lblInventionChance1.Text = "Chance:"
        '
        'lblBPDecryptor
        '
        Me.lblBPDecryptor.AutoSize = True
        Me.lblBPDecryptor.Location = New System.Drawing.Point(38, 22)
        Me.lblBPDecryptor.Name = "lblBPDecryptor"
        Me.lblBPDecryptor.Size = New System.Drawing.Size(89, 13)
        Me.lblBPDecryptor.TabIndex = 25
        Me.lblBPDecryptor.Text = "Select Decryptor:"
        '
        'lblBPInventionTime
        '
        Me.lblBPInventionTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPInventionTime.Location = New System.Drawing.Point(179, 75)
        Me.lblBPInventionTime.Name = "lblBPInventionTime"
        Me.lblBPInventionTime.Size = New System.Drawing.Size(98, 17)
        Me.lblBPInventionTime.TabIndex = 32
        Me.lblBPInventionTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPDecryptorStats
        '
        Me.lblBPDecryptorStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPDecryptorStats.Location = New System.Drawing.Point(179, 2)
        Me.lblBPDecryptorStats.Name = "lblBPDecryptorStats"
        Me.lblBPDecryptorStats.Size = New System.Drawing.Size(98, 30)
        Me.lblBPDecryptorStats.TabIndex = 20
        Me.lblBPDecryptorStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPInventionCost
        '
        Me.lblBPInventionCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPInventionCost.Location = New System.Drawing.Point(71, 75)
        Me.lblBPInventionCost.Name = "lblBPInventionCost"
        Me.lblBPInventionCost.Size = New System.Drawing.Size(105, 17)
        Me.lblBPInventionCost.TabIndex = 30
        Me.lblBPInventionCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbBPInventionDecryptor
        '
        Me.cmbBPInventionDecryptor.FormattingEnabled = True
        Me.cmbBPInventionDecryptor.ItemHeight = 13
        Me.cmbBPInventionDecryptor.Location = New System.Drawing.Point(41, 36)
        Me.cmbBPInventionDecryptor.Name = "cmbBPInventionDecryptor"
        Me.cmbBPInventionDecryptor.Size = New System.Drawing.Size(236, 21)
        Me.cmbBPInventionDecryptor.TabIndex = 26
        '
        'lblBPInventionChance
        '
        Me.lblBPInventionChance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPInventionChance.Location = New System.Drawing.Point(4, 75)
        Me.lblBPInventionChance.Name = "lblBPInventionChance"
        Me.lblBPInventionChance.Size = New System.Drawing.Size(63, 17)
        Me.lblBPInventionChance.TabIndex = 28
        Me.lblBPInventionChance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkBPIncludeInventionTime
        '
        Me.chkBPIncludeInventionTime.AutoSize = True
        Me.chkBPIncludeInventionTime.Location = New System.Drawing.Point(179, 59)
        Me.chkBPIncludeInventionTime.Name = "chkBPIncludeInventionTime"
        Me.chkBPIncludeInventionTime.Size = New System.Drawing.Size(99, 17)
        Me.chkBPIncludeInventionTime.TabIndex = 31
        Me.chkBPIncludeInventionTime.Text = "Invention Time:"
        Me.chkBPIncludeInventionTime.UseVisualStyleBackColor = True
        '
        'chkBPIncludeCopyTime
        '
        Me.chkBPIncludeCopyTime.AutoSize = True
        Me.chkBPIncludeCopyTime.Location = New System.Drawing.Point(179, 95)
        Me.chkBPIncludeCopyTime.Name = "chkBPIncludeCopyTime"
        Me.chkBPIncludeCopyTime.Size = New System.Drawing.Size(79, 17)
        Me.chkBPIncludeCopyTime.TabIndex = 35
        Me.chkBPIncludeCopyTime.Text = "Copy Time:"
        Me.chkBPIncludeCopyTime.UseVisualStyleBackColor = True
        '
        'chkBPIncludeCopyCosts
        '
        Me.chkBPIncludeCopyCosts.AutoSize = True
        Me.chkBPIncludeCopyCosts.Location = New System.Drawing.Point(71, 95)
        Me.chkBPIncludeCopyCosts.Name = "chkBPIncludeCopyCosts"
        Me.chkBPIncludeCopyCosts.Size = New System.Drawing.Size(82, 17)
        Me.chkBPIncludeCopyCosts.TabIndex = 33
        Me.chkBPIncludeCopyCosts.Text = "Copy Costs:"
        Me.chkBPIncludeCopyCosts.UseVisualStyleBackColor = True
        '
        'chkBPIncludeInventionCosts
        '
        Me.chkBPIncludeInventionCosts.AutoSize = True
        Me.chkBPIncludeInventionCosts.Location = New System.Drawing.Point(71, 59)
        Me.chkBPIncludeInventionCosts.Name = "chkBPIncludeInventionCosts"
        Me.chkBPIncludeInventionCosts.Size = New System.Drawing.Size(102, 17)
        Me.chkBPIncludeInventionCosts.TabIndex = 29
        Me.chkBPIncludeInventionCosts.Text = "Invention Costs:"
        Me.chkBPIncludeInventionCosts.UseVisualStyleBackColor = True
        '
        'btnBPSaveSettings
        '
        Me.btnBPSaveSettings.Location = New System.Drawing.Point(54, 154)
        Me.btnBPSaveSettings.Name = "btnBPSaveSettings"
        Me.btnBPSaveSettings.Size = New System.Drawing.Size(82, 34)
        Me.btnBPSaveSettings.TabIndex = 14
        Me.btnBPSaveSettings.Text = "Save Settings"
        Me.btnBPSaveSettings.UseVisualStyleBackColor = True
        '
        'txtBPLines
        '
        Me.txtBPLines.Location = New System.Drawing.Point(38, 105)
        Me.txtBPLines.MaxLength = 3
        Me.txtBPLines.Name = "txtBPLines"
        Me.txtBPLines.Size = New System.Drawing.Size(32, 20)
        Me.txtBPLines.TabIndex = 8
        '
        'pictBP
        '
        Me.pictBP.BackColor = System.Drawing.Color.White
        Me.pictBP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictBP.Location = New System.Drawing.Point(6, 12)
        Me.pictBP.Name = "pictBP"
        Me.pictBP.Size = New System.Drawing.Size(68, 69)
        Me.pictBP.TabIndex = 0
        Me.pictBP.TabStop = False
        '
        'gbBPManualSystemCostIndex
        '
        Me.gbBPManualSystemCostIndex.Controls.Add(Me.btnBPUpdateCostIndex)
        Me.gbBPManualSystemCostIndex.Controls.Add(Me.lblBPSystemCostIndexManual)
        Me.gbBPManualSystemCostIndex.Controls.Add(Me.txtBPUpdateCostIndex)
        Me.gbBPManualSystemCostIndex.Location = New System.Drawing.Point(323, 7)
        Me.gbBPManualSystemCostIndex.Name = "gbBPManualSystemCostIndex"
        Me.gbBPManualSystemCostIndex.Size = New System.Drawing.Size(127, 60)
        Me.gbBPManualSystemCostIndex.TabIndex = 23
        Me.gbBPManualSystemCostIndex.TabStop = False
        Me.gbBPManualSystemCostIndex.Text = "Update System Data:"
        '
        'btnBPUpdateCostIndex
        '
        Me.btnBPUpdateCostIndex.Enabled = False
        Me.btnBPUpdateCostIndex.Location = New System.Drawing.Point(76, 13)
        Me.btnBPUpdateCostIndex.Name = "btnBPUpdateCostIndex"
        Me.btnBPUpdateCostIndex.Size = New System.Drawing.Size(50, 45)
        Me.btnBPUpdateCostIndex.TabIndex = 21
        Me.btnBPUpdateCostIndex.Text = "Update System"
        Me.btnBPUpdateCostIndex.UseVisualStyleBackColor = True
        '
        'lblBPSystemCostIndexManual
        '
        Me.lblBPSystemCostIndexManual.AutoSize = True
        Me.lblBPSystemCostIndexManual.Location = New System.Drawing.Point(3, 20)
        Me.lblBPSystemCostIndexManual.Name = "lblBPSystemCostIndexManual"
        Me.lblBPSystemCostIndexManual.Size = New System.Drawing.Size(60, 13)
        Me.lblBPSystemCostIndexManual.TabIndex = 26
        Me.lblBPSystemCostIndexManual.Text = "Cost Index:"
        '
        'txtBPUpdateCostIndex
        '
        Me.txtBPUpdateCostIndex.Location = New System.Drawing.Point(5, 37)
        Me.txtBPUpdateCostIndex.MaxLength = 7
        Me.txtBPUpdateCostIndex.Name = "txtBPUpdateCostIndex"
        Me.txtBPUpdateCostIndex.Size = New System.Drawing.Size(66, 20)
        Me.txtBPUpdateCostIndex.TabIndex = 22
        Me.txtBPUpdateCostIndex.Text = "0.00 %"
        Me.txtBPUpdateCostIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPNumBPs
        '
        Me.txtBPNumBPs.Location = New System.Drawing.Point(104, 105)
        Me.txtBPNumBPs.Name = "txtBPNumBPs"
        Me.txtBPNumBPs.Size = New System.Drawing.Size(32, 20)
        Me.txtBPNumBPs.TabIndex = 10
        '
        'btnBPRefreshBP
        '
        Me.btnBPRefreshBP.Location = New System.Drawing.Point(6, 189)
        Me.btnBPRefreshBP.Name = "btnBPRefreshBP"
        Me.btnBPRefreshBP.Size = New System.Drawing.Size(130, 30)
        Me.btnBPRefreshBP.TabIndex = 13
        Me.btnBPRefreshBP.Text = "Refresh"
        Me.btnBPRefreshBP.UseVisualStyleBackColor = True
        '
        'lblBPLines
        '
        Me.lblBPLines.AutoSize = True
        Me.lblBPLines.Location = New System.Drawing.Point(4, 109)
        Me.lblBPLines.Name = "lblBPLines"
        Me.lblBPLines.Size = New System.Drawing.Size(35, 13)
        Me.lblBPLines.TabIndex = 7
        Me.lblBPLines.Text = "Lines:"
        '
        'txtBPME
        '
        Me.txtBPME.Location = New System.Drawing.Point(76, 60)
        Me.txtBPME.MaxLength = 2
        Me.txtBPME.Name = "txtBPME"
        Me.txtBPME.Size = New System.Drawing.Size(29, 20)
        Me.txtBPME.TabIndex = 4
        Me.txtBPME.Text = "0"
        Me.txtBPME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPRuns
        '
        Me.lblBPRuns.AutoSize = True
        Me.lblBPRuns.Location = New System.Drawing.Point(74, 11)
        Me.lblBPRuns.Name = "lblBPRuns"
        Me.lblBPRuns.Size = New System.Drawing.Size(62, 13)
        Me.lblBPRuns.TabIndex = 0
        Me.lblBPRuns.Text = "Total Runs:"
        '
        'chkBPBuildBuy
        '
        Me.chkBPBuildBuy.AutoSize = True
        Me.chkBPBuildBuy.Location = New System.Drawing.Point(12, 86)
        Me.chkBPBuildBuy.Name = "chkBPBuildBuy"
        Me.chkBPBuildBuy.Size = New System.Drawing.Size(119, 17)
        Me.chkBPBuildBuy.TabIndex = 6
        Me.chkBPBuildBuy.Text = "Calculate Build/Buy"
        Me.chkBPBuildBuy.UseVisualStyleBackColor = True
        '
        'txtBPRuns
        '
        Me.txtBPRuns.Location = New System.Drawing.Point(76, 25)
        Me.txtBPRuns.MaxLength = 6
        Me.txtBPRuns.Name = "txtBPRuns"
        Me.txtBPRuns.Size = New System.Drawing.Size(60, 20)
        Me.txtBPRuns.TabIndex = 1
        Me.txtBPRuns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPAddlCosts
        '
        Me.txtBPAddlCosts.Location = New System.Drawing.Point(38, 130)
        Me.txtBPAddlCosts.MaxLength = 15
        Me.txtBPAddlCosts.Name = "txtBPAddlCosts"
        Me.txtBPAddlCosts.Size = New System.Drawing.Size(98, 20)
        Me.txtBPAddlCosts.TabIndex = 12
        Me.txtBPAddlCosts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPAddlCosts
        '
        Me.lblBPAddlCosts.Location = New System.Drawing.Point(4, 126)
        Me.lblBPAddlCosts.Name = "lblBPAddlCosts"
        Me.lblBPAddlCosts.Size = New System.Drawing.Size(39, 28)
        Me.lblBPAddlCosts.TabIndex = 11
        Me.lblBPAddlCosts.Text = "Add'l Costs:"
        '
        'lblBPME
        '
        Me.lblBPME.AutoSize = True
        Me.lblBPME.Location = New System.Drawing.Point(77, 46)
        Me.lblBPME.Name = "lblBPME"
        Me.lblBPME.Size = New System.Drawing.Size(26, 13)
        Me.lblBPME.TabIndex = 2
        Me.lblBPME.Text = "ME:"
        '
        'txtBPTE
        '
        Me.txtBPTE.Location = New System.Drawing.Point(107, 60)
        Me.txtBPTE.MaxLength = 2
        Me.txtBPTE.Name = "txtBPTE"
        Me.txtBPTE.Size = New System.Drawing.Size(29, 20)
        Me.txtBPTE.TabIndex = 5
        Me.txtBPTE.Text = "0"
        Me.txtBPTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPPE
        '
        Me.lblBPPE.AutoSize = True
        Me.lblBPPE.Location = New System.Drawing.Point(109, 46)
        Me.lblBPPE.Name = "lblBPPE"
        Me.lblBPPE.Size = New System.Drawing.Size(24, 13)
        Me.lblBPPE.TabIndex = 3
        Me.lblBPPE.Text = "TE:"
        '
        'lblBPNumBPs
        '
        Me.lblBPNumBPs.AutoSize = True
        Me.lblBPNumBPs.Location = New System.Drawing.Point(75, 109)
        Me.lblBPNumBPs.Name = "lblBPNumBPs"
        Me.lblBPNumBPs.Size = New System.Drawing.Size(29, 13)
        Me.lblBPNumBPs.TabIndex = 9
        Me.lblBPNumBPs.Text = "BPs:"
        '
        'gbBPIgnoreinCalcs
        '
        Me.gbBPIgnoreinCalcs.Controls.Add(Me.chkBPIgnoreMinerals)
        Me.gbBPIgnoreinCalcs.Controls.Add(Me.chkBPIgnoreT1Item)
        Me.gbBPIgnoreinCalcs.Controls.Add(Me.chkBPIgnoreInvention)
        Me.gbBPIgnoreinCalcs.Location = New System.Drawing.Point(141, 7)
        Me.gbBPIgnoreinCalcs.Name = "gbBPIgnoreinCalcs"
        Me.gbBPIgnoreinCalcs.Size = New System.Drawing.Size(94, 60)
        Me.gbBPIgnoreinCalcs.TabIndex = 20
        Me.gbBPIgnoreinCalcs.TabStop = False
        Me.gbBPIgnoreinCalcs.Text = "Ignore in Calcs:"
        '
        'chkBPIgnoreMinerals
        '
        Me.chkBPIgnoreMinerals.AutoSize = True
        Me.chkBPIgnoreMinerals.Location = New System.Drawing.Point(5, 41)
        Me.chkBPIgnoreMinerals.Name = "chkBPIgnoreMinerals"
        Me.chkBPIgnoreMinerals.Size = New System.Drawing.Size(65, 17)
        Me.chkBPIgnoreMinerals.TabIndex = 1
        Me.chkBPIgnoreMinerals.Text = "Minerals"
        Me.chkBPIgnoreMinerals.UseVisualStyleBackColor = True
        '
        'chkBPIgnoreT1Item
        '
        Me.chkBPIgnoreT1Item.AutoSize = True
        Me.chkBPIgnoreT1Item.Location = New System.Drawing.Point(5, 27)
        Me.chkBPIgnoreT1Item.Name = "chkBPIgnoreT1Item"
        Me.chkBPIgnoreT1Item.Size = New System.Drawing.Size(62, 17)
        Me.chkBPIgnoreT1Item.TabIndex = 2
        Me.chkBPIgnoreT1Item.Text = "T1 Item"
        Me.chkBPIgnoreT1Item.UseVisualStyleBackColor = True
        '
        'chkBPIgnoreInvention
        '
        Me.chkBPIgnoreInvention.AutoSize = True
        Me.chkBPIgnoreInvention.Location = New System.Drawing.Point(5, 13)
        Me.chkBPIgnoreInvention.Name = "chkBPIgnoreInvention"
        Me.chkBPIgnoreInvention.Size = New System.Drawing.Size(70, 17)
        Me.chkBPIgnoreInvention.TabIndex = 0
        Me.chkBPIgnoreInvention.Text = "Invention"
        Me.chkBPIgnoreInvention.UseVisualStyleBackColor = True
        '
        'btnBPBuiltComponents
        '
        Me.btnBPBuiltComponents.AutoSize = True
        Me.btnBPBuiltComponents.Location = New System.Drawing.Point(84, 588)
        Me.btnBPBuiltComponents.Name = "btnBPBuiltComponents"
        Me.btnBPBuiltComponents.Size = New System.Drawing.Size(99, 23)
        Me.btnBPBuiltComponents.TabIndex = 79
        Me.btnBPBuiltComponents.Text = "Built Components"
        Me.btnBPBuiltComponents.UseVisualStyleBackColor = True
        '
        'btnBPComponents
        '
        Me.btnBPComponents.AutoSize = True
        Me.btnBPComponents.Location = New System.Drawing.Point(6, 588)
        Me.btnBPComponents.Name = "btnBPComponents"
        Me.btnBPComponents.Size = New System.Drawing.Size(76, 23)
        Me.btnBPComponents.TabIndex = 78
        Me.btnBPComponents.Text = "Components"
        Me.btnBPComponents.UseVisualStyleBackColor = True
        '
        'rbtnBPRawT2MatType
        '
        Me.rbtnBPRawT2MatType.AutoSize = True
        Me.rbtnBPRawT2MatType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.rbtnBPRawT2MatType.Location = New System.Drawing.Point(668, 236)
        Me.rbtnBPRawT2MatType.Name = "rbtnBPRawT2MatType"
        Me.rbtnBPRawT2MatType.Size = New System.Drawing.Size(47, 17)
        Me.rbtnBPRawT2MatType.TabIndex = 75
        Me.rbtnBPRawT2MatType.TabStop = True
        Me.rbtnBPRawT2MatType.Text = "Raw"
        Me.rbtnBPRawT2MatType.UseVisualStyleBackColor = False
        '
        'rbtnBPProcT2MatType
        '
        Me.rbtnBPProcT2MatType.AutoSize = True
        Me.rbtnBPProcT2MatType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.rbtnBPProcT2MatType.Location = New System.Drawing.Point(594, 236)
        Me.rbtnBPProcT2MatType.Name = "rbtnBPProcT2MatType"
        Me.rbtnBPProcT2MatType.Size = New System.Drawing.Size(75, 17)
        Me.rbtnBPProcT2MatType.TabIndex = 74
        Me.rbtnBPProcT2MatType.TabStop = True
        Me.rbtnBPProcT2MatType.Text = "Processed"
        Me.rbtnBPProcT2MatType.UseVisualStyleBackColor = False
        '
        'rbtnBPAdvT2MatType
        '
        Me.rbtnBPAdvT2MatType.AutoSize = True
        Me.rbtnBPAdvT2MatType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.rbtnBPAdvT2MatType.Location = New System.Drawing.Point(522, 236)
        Me.rbtnBPAdvT2MatType.Name = "rbtnBPAdvT2MatType"
        Me.rbtnBPAdvT2MatType.Size = New System.Drawing.Size(74, 17)
        Me.rbtnBPAdvT2MatType.TabIndex = 19
        Me.rbtnBPAdvT2MatType.TabStop = True
        Me.rbtnBPAdvT2MatType.Text = "Advanced"
        Me.rbtnBPAdvT2MatType.UseVisualStyleBackColor = False
        '
        'lblBPT2MatTypeSelector
        '
        Me.lblBPT2MatTypeSelector.BackColor = System.Drawing.SystemColors.Window
        Me.lblBPT2MatTypeSelector.Location = New System.Drawing.Point(414, 234)
        Me.lblBPT2MatTypeSelector.Name = "lblBPT2MatTypeSelector"
        Me.lblBPT2MatTypeSelector.Size = New System.Drawing.Size(308, 20)
        Me.lblBPT2MatTypeSelector.TabIndex = 76
        Me.lblBPT2MatTypeSelector.Text = "T2/T3 Material Type:"
        Me.lblBPT2MatTypeSelector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstBPList
        '
        Me.lstBPList.BackColor = System.Drawing.SystemColors.Window
        Me.lstBPList.FormattingEnabled = True
        Me.lstBPList.Location = New System.Drawing.Point(4, 47)
        Me.lstBPList.Name = "lstBPList"
        Me.lstBPList.Size = New System.Drawing.Size(322, 134)
        Me.lstBPList.TabIndex = 64
        Me.lstBPList.Visible = False
        '
        'gbBPBlueprintType
        '
        Me.gbBPBlueprintType.Controls.Add(Me.chkBPNPCBPOs)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPReactionsBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPStructureModulesBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPCelestialsBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPMiscBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPStructureBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPFavoriteBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPStructureRigsBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPOwnedBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPRigBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPBoosterBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPSubsystemBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPModuleBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPAmmoChargeBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPDroneBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPComponentBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPAllBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPShipBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPDeployableBlueprints)
        Me.gbBPBlueprintType.Location = New System.Drawing.Point(4, 51)
        Me.gbBPBlueprintType.Name = "gbBPBlueprintType"
        Me.gbBPBlueprintType.Size = New System.Drawing.Size(294, 125)
        Me.gbBPBlueprintType.TabIndex = 71
        Me.gbBPBlueprintType.TabStop = False
        Me.gbBPBlueprintType.Text = "Blueprint Type"
        '
        'chkBPNPCBPOs
        '
        Me.chkBPNPCBPOs.AutoSize = True
        Me.chkBPNPCBPOs.Location = New System.Drawing.Point(208, 103)
        Me.chkBPNPCBPOs.Name = "chkBPNPCBPOs"
        Me.chkBPNPCBPOs.Size = New System.Drawing.Size(78, 17)
        Me.chkBPNPCBPOs.TabIndex = 6
        Me.chkBPNPCBPOs.Text = "NPC BPOs"
        Me.chkBPNPCBPOs.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBPNPCBPOs.UseVisualStyleBackColor = True
        '
        'rbtnBPReactionsBlueprints
        '
        Me.rbtnBPReactionsBlueprints.AutoSize = True
        Me.rbtnBPReactionsBlueprints.Location = New System.Drawing.Point(9, 102)
        Me.rbtnBPReactionsBlueprints.Name = "rbtnBPReactionsBlueprints"
        Me.rbtnBPReactionsBlueprints.Size = New System.Drawing.Size(73, 17)
        Me.rbtnBPReactionsBlueprints.TabIndex = 66
        Me.rbtnBPReactionsBlueprints.TabStop = True
        Me.rbtnBPReactionsBlueprints.Text = "Reactions"
        Me.rbtnBPReactionsBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPStructureModulesBlueprints
        '
        Me.rbtnBPStructureModulesBlueprints.AutoSize = True
        Me.rbtnBPStructureModulesBlueprints.Location = New System.Drawing.Point(97, 102)
        Me.rbtnBPStructureModulesBlueprints.Name = "rbtnBPStructureModulesBlueprints"
        Me.rbtnBPStructureModulesBlueprints.Size = New System.Drawing.Size(111, 17)
        Me.rbtnBPStructureModulesBlueprints.TabIndex = 65
        Me.rbtnBPStructureModulesBlueprints.TabStop = True
        Me.rbtnBPStructureModulesBlueprints.Text = "Structure Modules"
        Me.rbtnBPStructureModulesBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPCelestialsBlueprints
        '
        Me.rbtnBPCelestialsBlueprints.AutoSize = True
        Me.rbtnBPCelestialsBlueprints.Location = New System.Drawing.Point(208, 85)
        Me.rbtnBPCelestialsBlueprints.Name = "rbtnBPCelestialsBlueprints"
        Me.rbtnBPCelestialsBlueprints.Size = New System.Drawing.Size(69, 17)
        Me.rbtnBPCelestialsBlueprints.TabIndex = 14
        Me.rbtnBPCelestialsBlueprints.TabStop = True
        Me.rbtnBPCelestialsBlueprints.Text = "Celestials"
        Me.rbtnBPCelestialsBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPMiscBlueprints
        '
        Me.rbtnBPMiscBlueprints.AutoSize = True
        Me.rbtnBPMiscBlueprints.Location = New System.Drawing.Point(149, 51)
        Me.rbtnBPMiscBlueprints.Name = "rbtnBPMiscBlueprints"
        Me.rbtnBPMiscBlueprints.Size = New System.Drawing.Size(50, 17)
        Me.rbtnBPMiscBlueprints.TabIndex = 15
        Me.rbtnBPMiscBlueprints.TabStop = True
        Me.rbtnBPMiscBlueprints.Text = "Misc."
        Me.rbtnBPMiscBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPStructureBlueprints
        '
        Me.rbtnBPStructureBlueprints.AutoSize = True
        Me.rbtnBPStructureBlueprints.Location = New System.Drawing.Point(9, 85)
        Me.rbtnBPStructureBlueprints.Name = "rbtnBPStructureBlueprints"
        Me.rbtnBPStructureBlueprints.Size = New System.Drawing.Size(73, 17)
        Me.rbtnBPStructureBlueprints.TabIndex = 12
        Me.rbtnBPStructureBlueprints.TabStop = True
        Me.rbtnBPStructureBlueprints.Text = "Structures"
        Me.rbtnBPStructureBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPFavoriteBlueprints
        '
        Me.rbtnBPFavoriteBlueprints.AutoSize = True
        Me.rbtnBPFavoriteBlueprints.Location = New System.Drawing.Point(208, 17)
        Me.rbtnBPFavoriteBlueprints.Name = "rbtnBPFavoriteBlueprints"
        Me.rbtnBPFavoriteBlueprints.Size = New System.Drawing.Size(68, 17)
        Me.rbtnBPFavoriteBlueprints.TabIndex = 2
        Me.rbtnBPFavoriteBlueprints.TabStop = True
        Me.rbtnBPFavoriteBlueprints.Text = "Favorites"
        Me.rbtnBPFavoriteBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPStructureRigsBlueprints
        '
        Me.rbtnBPStructureRigsBlueprints.AutoSize = True
        Me.rbtnBPStructureRigsBlueprints.Location = New System.Drawing.Point(97, 85)
        Me.rbtnBPStructureRigsBlueprints.Name = "rbtnBPStructureRigsBlueprints"
        Me.rbtnBPStructureRigsBlueprints.Size = New System.Drawing.Size(92, 17)
        Me.rbtnBPStructureRigsBlueprints.TabIndex = 13
        Me.rbtnBPStructureRigsBlueprints.TabStop = True
        Me.rbtnBPStructureRigsBlueprints.Text = "Structure Rigs"
        Me.rbtnBPStructureRigsBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPOwnedBlueprints
        '
        Me.rbtnBPOwnedBlueprints.AutoSize = True
        Me.rbtnBPOwnedBlueprints.Location = New System.Drawing.Point(97, 17)
        Me.rbtnBPOwnedBlueprints.Name = "rbtnBPOwnedBlueprints"
        Me.rbtnBPOwnedBlueprints.Size = New System.Drawing.Size(59, 17)
        Me.rbtnBPOwnedBlueprints.TabIndex = 1
        Me.rbtnBPOwnedBlueprints.TabStop = True
        Me.rbtnBPOwnedBlueprints.Text = "Owned"
        Me.rbtnBPOwnedBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPRigBlueprints
        '
        Me.rbtnBPRigBlueprints.AutoSize = True
        Me.rbtnBPRigBlueprints.Location = New System.Drawing.Point(97, 51)
        Me.rbtnBPRigBlueprints.Name = "rbtnBPRigBlueprints"
        Me.rbtnBPRigBlueprints.Size = New System.Drawing.Size(46, 17)
        Me.rbtnBPRigBlueprints.TabIndex = 7
        Me.rbtnBPRigBlueprints.TabStop = True
        Me.rbtnBPRigBlueprints.Text = "Rigs"
        Me.rbtnBPRigBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPBoosterBlueprints
        '
        Me.rbtnBPBoosterBlueprints.AutoSize = True
        Me.rbtnBPBoosterBlueprints.Location = New System.Drawing.Point(208, 68)
        Me.rbtnBPBoosterBlueprints.Name = "rbtnBPBoosterBlueprints"
        Me.rbtnBPBoosterBlueprints.Size = New System.Drawing.Size(66, 17)
        Me.rbtnBPBoosterBlueprints.TabIndex = 11
        Me.rbtnBPBoosterBlueprints.TabStop = True
        Me.rbtnBPBoosterBlueprints.Text = "Boosters"
        Me.rbtnBPBoosterBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPSubsystemBlueprints
        '
        Me.rbtnBPSubsystemBlueprints.AutoSize = True
        Me.rbtnBPSubsystemBlueprints.Location = New System.Drawing.Point(208, 51)
        Me.rbtnBPSubsystemBlueprints.Name = "rbtnBPSubsystemBlueprints"
        Me.rbtnBPSubsystemBlueprints.Size = New System.Drawing.Size(81, 17)
        Me.rbtnBPSubsystemBlueprints.TabIndex = 8
        Me.rbtnBPSubsystemBlueprints.TabStop = True
        Me.rbtnBPSubsystemBlueprints.Text = "Subsystems"
        Me.rbtnBPSubsystemBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPModuleBlueprints
        '
        Me.rbtnBPModuleBlueprints.AutoSize = True
        Me.rbtnBPModuleBlueprints.Location = New System.Drawing.Point(208, 34)
        Me.rbtnBPModuleBlueprints.Name = "rbtnBPModuleBlueprints"
        Me.rbtnBPModuleBlueprints.Size = New System.Drawing.Size(65, 17)
        Me.rbtnBPModuleBlueprints.TabIndex = 4
        Me.rbtnBPModuleBlueprints.TabStop = True
        Me.rbtnBPModuleBlueprints.Text = "Modules"
        Me.rbtnBPModuleBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPAmmoChargeBlueprints
        '
        Me.rbtnBPAmmoChargeBlueprints.AutoSize = True
        Me.rbtnBPAmmoChargeBlueprints.Location = New System.Drawing.Point(97, 34)
        Me.rbtnBPAmmoChargeBlueprints.Name = "rbtnBPAmmoChargeBlueprints"
        Me.rbtnBPAmmoChargeBlueprints.Size = New System.Drawing.Size(98, 17)
        Me.rbtnBPAmmoChargeBlueprints.TabIndex = 5
        Me.rbtnBPAmmoChargeBlueprints.TabStop = True
        Me.rbtnBPAmmoChargeBlueprints.Text = "Ammo/Charges"
        Me.rbtnBPAmmoChargeBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPDroneBlueprints
        '
        Me.rbtnBPDroneBlueprints.AutoSize = True
        Me.rbtnBPDroneBlueprints.Location = New System.Drawing.Point(9, 51)
        Me.rbtnBPDroneBlueprints.Name = "rbtnBPDroneBlueprints"
        Me.rbtnBPDroneBlueprints.Size = New System.Drawing.Size(59, 17)
        Me.rbtnBPDroneBlueprints.TabIndex = 6
        Me.rbtnBPDroneBlueprints.TabStop = True
        Me.rbtnBPDroneBlueprints.Text = "Drones"
        Me.rbtnBPDroneBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPComponentBlueprints
        '
        Me.rbtnBPComponentBlueprints.AutoSize = True
        Me.rbtnBPComponentBlueprints.Location = New System.Drawing.Point(97, 68)
        Me.rbtnBPComponentBlueprints.Name = "rbtnBPComponentBlueprints"
        Me.rbtnBPComponentBlueprints.Size = New System.Drawing.Size(84, 17)
        Me.rbtnBPComponentBlueprints.TabIndex = 10
        Me.rbtnBPComponentBlueprints.TabStop = True
        Me.rbtnBPComponentBlueprints.Text = "Components"
        Me.rbtnBPComponentBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPAllBlueprints
        '
        Me.rbtnBPAllBlueprints.AutoSize = True
        Me.rbtnBPAllBlueprints.Location = New System.Drawing.Point(9, 17)
        Me.rbtnBPAllBlueprints.Name = "rbtnBPAllBlueprints"
        Me.rbtnBPAllBlueprints.Size = New System.Drawing.Size(36, 17)
        Me.rbtnBPAllBlueprints.TabIndex = 0
        Me.rbtnBPAllBlueprints.TabStop = True
        Me.rbtnBPAllBlueprints.Text = "All"
        Me.rbtnBPAllBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPShipBlueprints
        '
        Me.rbtnBPShipBlueprints.AutoSize = True
        Me.rbtnBPShipBlueprints.Location = New System.Drawing.Point(9, 34)
        Me.rbtnBPShipBlueprints.Name = "rbtnBPShipBlueprints"
        Me.rbtnBPShipBlueprints.Size = New System.Drawing.Size(51, 17)
        Me.rbtnBPShipBlueprints.TabIndex = 3
        Me.rbtnBPShipBlueprints.TabStop = True
        Me.rbtnBPShipBlueprints.Text = "Ships"
        Me.rbtnBPShipBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPDeployableBlueprints
        '
        Me.rbtnBPDeployableBlueprints.AutoSize = True
        Me.rbtnBPDeployableBlueprints.Location = New System.Drawing.Point(9, 68)
        Me.rbtnBPDeployableBlueprints.Name = "rbtnBPDeployableBlueprints"
        Me.rbtnBPDeployableBlueprints.Size = New System.Drawing.Size(78, 17)
        Me.rbtnBPDeployableBlueprints.TabIndex = 9
        Me.rbtnBPDeployableBlueprints.TabStop = True
        Me.rbtnBPDeployableBlueprints.Text = "Deployable"
        Me.rbtnBPDeployableBlueprints.UseVisualStyleBackColor = True
        '
        'gbBPBlueprintTech
        '
        Me.gbBPBlueprintTech.Controls.Add(Me.chkBPPirateFaction)
        Me.gbBPBlueprintTech.Controls.Add(Me.chkBPStoryline)
        Me.gbBPBlueprintTech.Controls.Add(Me.chkBPNavyFaction)
        Me.gbBPBlueprintTech.Controls.Add(Me.chkBPT3)
        Me.gbBPBlueprintTech.Controls.Add(Me.chkBPT2)
        Me.gbBPBlueprintTech.Controls.Add(Me.chkBPT1)
        Me.gbBPBlueprintTech.Location = New System.Drawing.Point(301, 106)
        Me.gbBPBlueprintTech.Name = "gbBPBlueprintTech"
        Me.gbBPBlueprintTech.Size = New System.Drawing.Size(87, 126)
        Me.gbBPBlueprintTech.TabIndex = 73
        Me.gbBPBlueprintTech.TabStop = False
        Me.gbBPBlueprintTech.Text = "Tech"
        '
        'chkBPPirateFaction
        '
        Me.chkBPPirateFaction.AutoSize = True
        Me.chkBPPirateFaction.Location = New System.Drawing.Point(8, 105)
        Me.chkBPPirateFaction.Name = "chkBPPirateFaction"
        Me.chkBPPirateFaction.Size = New System.Drawing.Size(53, 17)
        Me.chkBPPirateFaction.TabIndex = 5
        Me.chkBPPirateFaction.Text = "Pirate"
        Me.chkBPPirateFaction.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBPPirateFaction.UseVisualStyleBackColor = True
        '
        'chkBPStoryline
        '
        Me.chkBPStoryline.AutoSize = True
        Me.chkBPStoryline.Location = New System.Drawing.Point(8, 69)
        Me.chkBPStoryline.Name = "chkBPStoryline"
        Me.chkBPStoryline.Size = New System.Drawing.Size(66, 17)
        Me.chkBPStoryline.TabIndex = 3
        Me.chkBPStoryline.Text = "Storyline"
        Me.chkBPStoryline.UseVisualStyleBackColor = True
        '
        'chkBPNavyFaction
        '
        Me.chkBPNavyFaction.AutoSize = True
        Me.chkBPNavyFaction.Location = New System.Drawing.Point(8, 87)
        Me.chkBPNavyFaction.Name = "chkBPNavyFaction"
        Me.chkBPNavyFaction.Size = New System.Drawing.Size(51, 17)
        Me.chkBPNavyFaction.TabIndex = 4
        Me.chkBPNavyFaction.Text = "Navy"
        Me.chkBPNavyFaction.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBPNavyFaction.UseVisualStyleBackColor = True
        '
        'chkBPT3
        '
        Me.chkBPT3.AutoSize = True
        Me.chkBPT3.Location = New System.Drawing.Point(8, 51)
        Me.chkBPT3.Name = "chkBPT3"
        Me.chkBPT3.Size = New System.Drawing.Size(60, 17)
        Me.chkBPT3.TabIndex = 2
        Me.chkBPT3.Text = "Tech 3"
        Me.chkBPT3.UseVisualStyleBackColor = True
        '
        'chkBPT2
        '
        Me.chkBPT2.AutoSize = True
        Me.chkBPT2.Location = New System.Drawing.Point(8, 33)
        Me.chkBPT2.Name = "chkBPT2"
        Me.chkBPT2.Size = New System.Drawing.Size(60, 17)
        Me.chkBPT2.TabIndex = 1
        Me.chkBPT2.Text = "Tech 2"
        Me.chkBPT2.UseVisualStyleBackColor = True
        '
        'chkBPT1
        '
        Me.chkBPT1.AutoSize = True
        Me.chkBPT1.Location = New System.Drawing.Point(8, 15)
        Me.chkBPT1.Name = "chkBPT1"
        Me.chkBPT1.Size = New System.Drawing.Size(60, 17)
        Me.chkBPT1.TabIndex = 0
        Me.chkBPT1.Text = "Tech 1"
        Me.chkBPT1.UseVisualStyleBackColor = True
        '
        'gbFilters
        '
        Me.gbFilters.Controls.Add(Me.chkBPXL)
        Me.gbFilters.Controls.Add(Me.chkBPLarge)
        Me.gbFilters.Controls.Add(Me.chkBPMedium)
        Me.gbFilters.Controls.Add(Me.chkBPSmall)
        Me.gbFilters.Location = New System.Drawing.Point(301, 51)
        Me.gbFilters.Name = "gbFilters"
        Me.gbFilters.Size = New System.Drawing.Size(87, 55)
        Me.gbFilters.TabIndex = 72
        Me.gbFilters.TabStop = False
        Me.gbFilters.Text = "Size Limit"
        '
        'chkBPXL
        '
        Me.chkBPXL.AutoSize = True
        Me.chkBPXL.Location = New System.Drawing.Point(43, 33)
        Me.chkBPXL.Name = "chkBPXL"
        Me.chkBPXL.Size = New System.Drawing.Size(39, 17)
        Me.chkBPXL.TabIndex = 4
        Me.chkBPXL.Text = "XL"
        Me.chkBPXL.UseVisualStyleBackColor = True
        '
        'chkBPLarge
        '
        Me.chkBPLarge.AutoSize = True
        Me.chkBPLarge.Location = New System.Drawing.Point(8, 33)
        Me.chkBPLarge.Name = "chkBPLarge"
        Me.chkBPLarge.Size = New System.Drawing.Size(32, 17)
        Me.chkBPLarge.TabIndex = 3
        Me.chkBPLarge.Text = "L"
        Me.chkBPLarge.UseVisualStyleBackColor = True
        '
        'chkBPMedium
        '
        Me.chkBPMedium.AutoSize = True
        Me.chkBPMedium.Location = New System.Drawing.Point(43, 15)
        Me.chkBPMedium.Name = "chkBPMedium"
        Me.chkBPMedium.Size = New System.Drawing.Size(35, 17)
        Me.chkBPMedium.TabIndex = 2
        Me.chkBPMedium.Text = "M"
        Me.chkBPMedium.UseVisualStyleBackColor = True
        '
        'chkBPSmall
        '
        Me.chkBPSmall.AutoSize = True
        Me.chkBPSmall.Location = New System.Drawing.Point(8, 15)
        Me.chkBPSmall.Name = "chkBPSmall"
        Me.chkBPSmall.Size = New System.Drawing.Size(33, 17)
        Me.chkBPSmall.TabIndex = 1
        Me.chkBPSmall.Text = "S"
        Me.chkBPSmall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkBPSmall.UseVisualStyleBackColor = True
        '
        'cmbBPBlueprintSelection
        '
        Me.cmbBPBlueprintSelection.Location = New System.Drawing.Point(4, 27)
        Me.cmbBPBlueprintSelection.Name = "cmbBPBlueprintSelection"
        Me.cmbBPBlueprintSelection.Size = New System.Drawing.Size(322, 21)
        Me.cmbBPBlueprintSelection.TabIndex = 70
        Me.cmbBPBlueprintSelection.Text = "Select Blueprint or Reaction"
        '
        'btnBPListView
        '
        Me.btnBPListView.Location = New System.Drawing.Point(332, 14)
        Me.btnBPListView.Name = "btnBPListView"
        Me.btnBPListView.Size = New System.Drawing.Size(56, 36)
        Me.btnBPListView.TabIndex = 5
        Me.btnBPListView.Text = "Blueprint Viewer"
        Me.btnBPListView.UseVisualStyleBackColor = True
        '
        'btnBPForward
        '
        Me.btnBPForward.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBPForward.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBPForward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnBPForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBPForward.Image = CType(resources.GetObject("btnBPForward.Image"), System.Drawing.Image)
        Me.btnBPForward.Location = New System.Drawing.Point(1114, 236)
        Me.btnBPForward.Name = "btnBPForward"
        Me.btnBPForward.Size = New System.Drawing.Size(17, 19)
        Me.btnBPForward.TabIndex = 37
        Me.btnBPForward.UseVisualStyleBackColor = True
        '
        'btnBPBack
        '
        Me.btnBPBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBPBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBPBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBPBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnBPBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBPBack.Image = CType(resources.GetObject("btnBPBack.Image"), System.Drawing.Image)
        Me.btnBPBack.Location = New System.Drawing.Point(1091, 236)
        Me.btnBPBack.Name = "btnBPBack"
        Me.btnBPBack.Size = New System.Drawing.Size(17, 19)
        Me.btnBPBack.TabIndex = 36
        Me.btnBPBack.UseVisualStyleBackColor = False
        '
        'lblBPSelectBlueprint
        '
        Me.lblBPSelectBlueprint.AutoSize = True
        Me.lblBPSelectBlueprint.Location = New System.Drawing.Point(3, 12)
        Me.lblBPSelectBlueprint.Name = "lblBPSelectBlueprint"
        Me.lblBPSelectBlueprint.Size = New System.Drawing.Size(151, 13)
        Me.lblBPSelectBlueprint.TabIndex = 0
        Me.lblBPSelectBlueprint.Text = "Selected Blueprint or Reaction"
        '
        'gbBPInventionStats
        '
        Me.gbBPInventionStats.BackColor = System.Drawing.Color.Transparent
        Me.gbBPInventionStats.Controls.Add(Me.txtBPBrokerFeeRate)
        Me.gbBPInventionStats.Controls.Add(Me.txtBPMarketPriceEdit)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPProductionTime)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPTotalUnits)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPTaxes)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPTotalUnits1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPBrokerFees)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPPT)
        Me.gbBPInventionStats.Controls.Add(Me.chkBPTaxes)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPMarketCost)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPMarketCost1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawTotalCost)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPCompProfit)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawTotalCost1)
        Me.gbBPInventionStats.Controls.Add(Me.chkBPBrokerFees)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPCompIPH)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawIPH)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPTotalCompCost1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPCompIPH1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPTotalItemPT)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPTotalCompCost)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPCPTPT)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawSVR)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawIPH1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawProfit)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPBPSVR)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPCompProfit1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawProfit1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPBPSVR1)
        Me.gbBPInventionStats.Controls.Add(Me.lblBPRawSVR1)
        Me.gbBPInventionStats.Controls.Add(Me.chkBPPricePerUnit)
        Me.gbBPInventionStats.Location = New System.Drawing.Point(853, 8)
        Me.gbBPInventionStats.Name = "gbBPInventionStats"
        Me.gbBPInventionStats.Size = New System.Drawing.Size(278, 224)
        Me.gbBPInventionStats.TabIndex = 17
        Me.gbBPInventionStats.TabStop = False
        '
        'txtBPBrokerFeeRate
        '
        Me.txtBPBrokerFeeRate.Location = New System.Drawing.Point(225, 77)
        Me.txtBPBrokerFeeRate.Name = "txtBPBrokerFeeRate"
        Me.txtBPBrokerFeeRate.Size = New System.Drawing.Size(48, 20)
        Me.txtBPBrokerFeeRate.TabIndex = 61
        Me.txtBPBrokerFeeRate.TabStop = False
        Me.txtBPBrokerFeeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBPBrokerFeeRate.Visible = False
        '
        'txtBPMarketPriceEdit
        '
        Me.txtBPMarketPriceEdit.Location = New System.Drawing.Point(53, 97)
        Me.txtBPMarketPriceEdit.Name = "txtBPMarketPriceEdit"
        Me.txtBPMarketPriceEdit.Size = New System.Drawing.Size(131, 20)
        Me.txtBPMarketPriceEdit.TabIndex = 60
        Me.txtBPMarketPriceEdit.TabStop = False
        Me.txtBPMarketPriceEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBPMarketPriceEdit.Visible = False
        '
        'lblBPProductionTime
        '
        Me.lblBPProductionTime.BackColor = System.Drawing.Color.Transparent
        Me.lblBPProductionTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPProductionTime.Location = New System.Drawing.Point(5, 25)
        Me.lblBPProductionTime.Name = "lblBPProductionTime"
        Me.lblBPProductionTime.Size = New System.Drawing.Size(132, 17)
        Me.lblBPProductionTime.TabIndex = 1
        Me.lblBPProductionTime.Text = "00:00:00"
        Me.lblBPProductionTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPTotalUnits
        '
        Me.lblBPTotalUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPTotalUnits.Location = New System.Drawing.Point(141, 60)
        Me.lblBPTotalUnits.Name = "lblBPTotalUnits"
        Me.lblBPTotalUnits.Size = New System.Drawing.Size(132, 16)
        Me.lblBPTotalUnits.TabIndex = 7
        Me.lblBPTotalUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPTaxes
        '
        Me.lblBPTaxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPTaxes.Location = New System.Drawing.Point(6, 98)
        Me.lblBPTaxes.Name = "lblBPTaxes"
        Me.lblBPTaxes.Size = New System.Drawing.Size(131, 17)
        Me.lblBPTaxes.TabIndex = 10
        Me.lblBPTaxes.Text = "0.00"
        Me.lblBPTaxes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPTotalUnits1
        '
        Me.lblBPTotalUnits1.AutoSize = True
        Me.lblBPTotalUnits1.Location = New System.Drawing.Point(138, 45)
        Me.lblBPTotalUnits1.Name = "lblBPTotalUnits1"
        Me.lblBPTotalUnits1.Size = New System.Drawing.Size(34, 13)
        Me.lblBPTotalUnits1.TabIndex = 6
        Me.lblBPTotalUnits1.Text = "Units:"
        '
        'lblBPBrokerFees
        '
        Me.lblBPBrokerFees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPBrokerFees.Location = New System.Drawing.Point(141, 98)
        Me.lblBPBrokerFees.Name = "lblBPBrokerFees"
        Me.lblBPBrokerFees.Size = New System.Drawing.Size(132, 17)
        Me.lblBPBrokerFees.TabIndex = 12
        Me.lblBPBrokerFees.Text = "0.00"
        Me.lblBPBrokerFees.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPPT
        '
        Me.lblBPPT.AutoSize = True
        Me.lblBPPT.Location = New System.Drawing.Point(3, 10)
        Me.lblBPPT.Name = "lblBPPT"
        Me.lblBPPT.Size = New System.Drawing.Size(104, 13)
        Me.lblBPPT.TabIndex = 0
        Me.lblBPPT.Text = "BP Production Time:"
        '
        'chkBPTaxes
        '
        Me.chkBPTaxes.AutoSize = True
        Me.chkBPTaxes.Checked = True
        Me.chkBPTaxes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBPTaxes.Location = New System.Drawing.Point(6, 79)
        Me.chkBPTaxes.Name = "chkBPTaxes"
        Me.chkBPTaxes.Size = New System.Drawing.Size(58, 17)
        Me.chkBPTaxes.TabIndex = 9
        Me.chkBPTaxes.Text = "Taxes:"
        Me.chkBPTaxes.UseVisualStyleBackColor = True
        '
        'lblBPMarketCost
        '
        Me.lblBPMarketCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPMarketCost.Location = New System.Drawing.Point(5, 59)
        Me.lblBPMarketCost.Name = "lblBPMarketCost"
        Me.lblBPMarketCost.Size = New System.Drawing.Size(132, 17)
        Me.lblBPMarketCost.TabIndex = 5
        Me.lblBPMarketCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPMarketCost1
        '
        Me.lblBPMarketCost1.AutoSize = True
        Me.lblBPMarketCost1.Location = New System.Drawing.Point(3, 45)
        Me.lblBPMarketCost1.Name = "lblBPMarketCost1"
        Me.lblBPMarketCost1.Size = New System.Drawing.Size(70, 13)
        Me.lblBPMarketCost1.TabIndex = 4
        Me.lblBPMarketCost1.Text = "Market Price:"
        '
        'lblBPRawTotalCost
        '
        Me.lblBPRawTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRawTotalCost.Location = New System.Drawing.Point(141, 132)
        Me.lblBPRawTotalCost.Name = "lblBPRawTotalCost"
        Me.lblBPRawTotalCost.Size = New System.Drawing.Size(132, 17)
        Me.lblBPRawTotalCost.TabIndex = 16
        Me.lblBPRawTotalCost.Text = "0.00"
        Me.lblBPRawTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPCompProfit
        '
        Me.lblBPCompProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPCompProfit.Location = New System.Drawing.Point(5, 166)
        Me.lblBPCompProfit.Name = "lblBPCompProfit"
        Me.lblBPCompProfit.Size = New System.Drawing.Size(132, 17)
        Me.lblBPCompProfit.TabIndex = 18
        Me.lblBPCompProfit.Text = "0.00"
        Me.lblBPCompProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRawTotalCost1
        '
        Me.lblBPRawTotalCost1.AutoSize = True
        Me.lblBPRawTotalCost1.Location = New System.Drawing.Point(138, 118)
        Me.lblBPRawTotalCost1.Name = "lblBPRawTotalCost1"
        Me.lblBPRawTotalCost1.Size = New System.Drawing.Size(104, 13)
        Me.lblBPRawTotalCost1.TabIndex = 15
        Me.lblBPRawTotalCost1.Text = "Total Raw Mat Cost:"
        '
        'chkBPBrokerFees
        '
        Me.chkBPBrokerFees.AutoSize = True
        Me.chkBPBrokerFees.Checked = True
        Me.chkBPBrokerFees.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBPBrokerFees.Location = New System.Drawing.Point(141, 79)
        Me.chkBPBrokerFees.Name = "chkBPBrokerFees"
        Me.chkBPBrokerFees.Size = New System.Drawing.Size(52, 17)
        Me.chkBPBrokerFees.TabIndex = 11
        Me.chkBPBrokerFees.Text = "Fees:"
        Me.chkBPBrokerFees.ThreeState = True
        Me.chkBPBrokerFees.UseVisualStyleBackColor = True
        '
        'lblBPCompIPH
        '
        Me.lblBPCompIPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPCompIPH.Location = New System.Drawing.Point(5, 200)
        Me.lblBPCompIPH.Name = "lblBPCompIPH"
        Me.lblBPCompIPH.Size = New System.Drawing.Size(89, 17)
        Me.lblBPCompIPH.TabIndex = 22
        Me.lblBPCompIPH.Text = "0.00"
        Me.lblBPCompIPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRawIPH
        '
        Me.lblBPRawIPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRawIPH.Location = New System.Drawing.Point(141, 200)
        Me.lblBPRawIPH.Name = "lblBPRawIPH"
        Me.lblBPRawIPH.Size = New System.Drawing.Size(89, 17)
        Me.lblBPRawIPH.TabIndex = 26
        Me.lblBPRawIPH.Text = "0.00"
        Me.lblBPRawIPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPTotalCompCost1
        '
        Me.lblBPTotalCompCost1.AutoSize = True
        Me.lblBPTotalCompCost1.Location = New System.Drawing.Point(3, 118)
        Me.lblBPTotalCompCost1.Name = "lblBPTotalCompCost1"
        Me.lblBPTotalCompCost1.Size = New System.Drawing.Size(115, 13)
        Me.lblBPTotalCompCost1.TabIndex = 13
        Me.lblBPTotalCompCost1.Text = "Total Component Cost:"
        '
        'lblBPCompIPH1
        '
        Me.lblBPCompIPH1.AutoSize = True
        Me.lblBPCompIPH1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblBPCompIPH1.Location = New System.Drawing.Point(3, 186)
        Me.lblBPCompIPH1.Name = "lblBPCompIPH1"
        Me.lblBPCompIPH1.Size = New System.Drawing.Size(88, 13)
        Me.lblBPCompIPH1.TabIndex = 21
        Me.lblBPCompIPH1.Text = "BP ISK per Hour:"
        '
        'lblBPTotalItemPT
        '
        Me.lblBPTotalItemPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPTotalItemPT.Location = New System.Drawing.Point(141, 25)
        Me.lblBPTotalItemPT.Name = "lblBPTotalItemPT"
        Me.lblBPTotalItemPT.Size = New System.Drawing.Size(132, 17)
        Me.lblBPTotalItemPT.TabIndex = 3
        Me.lblBPTotalItemPT.Text = "00:00:00"
        Me.lblBPTotalItemPT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPTotalCompCost
        '
        Me.lblBPTotalCompCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPTotalCompCost.Location = New System.Drawing.Point(5, 132)
        Me.lblBPTotalCompCost.Name = "lblBPTotalCompCost"
        Me.lblBPTotalCompCost.Size = New System.Drawing.Size(132, 17)
        Me.lblBPTotalCompCost.TabIndex = 14
        Me.lblBPTotalCompCost.Text = "0.00"
        Me.lblBPTotalCompCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPCPTPT
        '
        Me.lblBPCPTPT.AutoSize = True
        Me.lblBPCPTPT.Location = New System.Drawing.Point(138, 10)
        Me.lblBPCPTPT.Name = "lblBPCPTPT"
        Me.lblBPCPTPT.Size = New System.Drawing.Size(114, 13)
        Me.lblBPCPTPT.TabIndex = 2
        Me.lblBPCPTPT.Text = "Total Production Time:"
        '
        'lblBPRawSVR
        '
        Me.lblBPRawSVR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRawSVR.Location = New System.Drawing.Point(232, 200)
        Me.lblBPRawSVR.Name = "lblBPRawSVR"
        Me.lblBPRawSVR.Size = New System.Drawing.Size(41, 17)
        Me.lblBPRawSVR.TabIndex = 28
        Me.lblBPRawSVR.Text = "0.00"
        Me.lblBPRawSVR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRawIPH1
        '
        Me.lblBPRawIPH1.AutoSize = True
        Me.lblBPRawIPH1.Location = New System.Drawing.Point(138, 186)
        Me.lblBPRawIPH1.Name = "lblBPRawIPH1"
        Me.lblBPRawIPH1.Size = New System.Drawing.Size(96, 13)
        Me.lblBPRawIPH1.TabIndex = 25
        Me.lblBPRawIPH1.Text = "Raw ISK per Hour:"
        '
        'lblBPRawProfit
        '
        Me.lblBPRawProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRawProfit.Location = New System.Drawing.Point(141, 166)
        Me.lblBPRawProfit.Name = "lblBPRawProfit"
        Me.lblBPRawProfit.Size = New System.Drawing.Size(132, 17)
        Me.lblBPRawProfit.TabIndex = 20
        Me.lblBPRawProfit.Text = "0.00"
        Me.lblBPRawProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPBPSVR
        '
        Me.lblBPBPSVR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPBPSVR.Location = New System.Drawing.Point(97, 200)
        Me.lblBPBPSVR.Name = "lblBPBPSVR"
        Me.lblBPBPSVR.Size = New System.Drawing.Size(40, 17)
        Me.lblBPBPSVR.TabIndex = 24
        Me.lblBPBPSVR.Text = "0.00"
        Me.lblBPBPSVR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPCompProfit1
        '
        Me.lblBPCompProfit1.AutoSize = True
        Me.lblBPCompProfit1.Location = New System.Drawing.Point(3, 152)
        Me.lblBPCompProfit1.Name = "lblBPCompProfit1"
        Me.lblBPCompProfit1.Size = New System.Drawing.Size(91, 13)
        Me.lblBPCompProfit1.TabIndex = 17
        Me.lblBPCompProfit1.Text = "Component Profit:"
        '
        'lblBPRawProfit1
        '
        Me.lblBPRawProfit1.AutoSize = True
        Me.lblBPRawProfit1.Location = New System.Drawing.Point(138, 152)
        Me.lblBPRawProfit1.Name = "lblBPRawProfit1"
        Me.lblBPRawProfit1.Size = New System.Drawing.Size(59, 13)
        Me.lblBPRawProfit1.TabIndex = 19
        Me.lblBPRawProfit1.Text = "Raw Profit:"
        '
        'lblBPBPSVR1
        '
        Me.lblBPBPSVR1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblBPBPSVR1.Location = New System.Drawing.Point(101, 186)
        Me.lblBPBPSVR1.Name = "lblBPBPSVR1"
        Me.lblBPBPSVR1.Size = New System.Drawing.Size(32, 13)
        Me.lblBPBPSVR1.TabIndex = 23
        Me.lblBPBPSVR1.Text = "SVR"
        Me.lblBPBPSVR1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRawSVR1
        '
        Me.lblBPRawSVR1.Location = New System.Drawing.Point(236, 186)
        Me.lblBPRawSVR1.Name = "lblBPRawSVR1"
        Me.lblBPRawSVR1.Size = New System.Drawing.Size(32, 13)
        Me.lblBPRawSVR1.TabIndex = 27
        Me.lblBPRawSVR1.Text = "SVR"
        Me.lblBPRawSVR1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkBPPricePerUnit
        '
        Me.chkBPPricePerUnit.AutoSize = True
        Me.chkBPPricePerUnit.Location = New System.Drawing.Point(225, 44)
        Me.chkBPPricePerUnit.Name = "chkBPPricePerUnit"
        Me.chkBPPricePerUnit.Size = New System.Drawing.Size(48, 17)
        Me.chkBPPricePerUnit.TabIndex = 8
        Me.chkBPPricePerUnit.Text = "PPU"
        Me.chkBPPricePerUnit.UseVisualStyleBackColor = True
        '
        'lblBPBuyColor
        '
        Me.lblBPBuyColor.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.lblBPBuyColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPBuyColor.Location = New System.Drawing.Point(69, 238)
        Me.lblBPBuyColor.Name = "lblBPBuyColor"
        Me.lblBPBuyColor.Size = New System.Drawing.Size(59, 16)
        Me.lblBPBuyColor.TabIndex = 31
        Me.lblBPBuyColor.Text = "Buy Item"
        Me.lblBPBuyColor.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBPBuildColor
        '
        Me.lblBPBuildColor.BackColor = System.Drawing.Color.Gold
        Me.lblBPBuildColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPBuildColor.Location = New System.Drawing.Point(9, 238)
        Me.lblBPBuildColor.Name = "lblBPBuildColor"
        Me.lblBPBuildColor.Size = New System.Drawing.Size(59, 16)
        Me.lblBPBuildColor.TabIndex = 30
        Me.lblBPBuildColor.Text = "Build Item"
        Me.lblBPBuildColor.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbBPShopandCopy
        '
        Me.gbBPShopandCopy.Controls.Add(Me.chkBPSimpleCopy)
        Me.gbBPShopandCopy.Controls.Add(Me.rbtnBPCopyInvREMats)
        Me.gbBPShopandCopy.Controls.Add(Me.rbtnBPComponentCopy)
        Me.gbBPShopandCopy.Controls.Add(Me.rbtnBPRawmatCopy)
        Me.gbBPShopandCopy.Controls.Add(Me.btnBPCopyMatstoClip)
        Me.gbBPShopandCopy.Controls.Add(Me.btnBPAddBPMatstoShoppingList)
        Me.gbBPShopandCopy.Controls.Add(Me.lblBPSimpleCopy)
        Me.gbBPShopandCopy.Location = New System.Drawing.Point(4, 174)
        Me.gbBPShopandCopy.Name = "gbBPShopandCopy"
        Me.gbBPShopandCopy.Size = New System.Drawing.Size(294, 58)
        Me.gbBPShopandCopy.TabIndex = 3
        Me.gbBPShopandCopy.TabStop = False
        '
        'chkBPSimpleCopy
        '
        Me.chkBPSimpleCopy.AutoSize = True
        Me.chkBPSimpleCopy.Location = New System.Drawing.Point(176, 37)
        Me.chkBPSimpleCopy.Name = "chkBPSimpleCopy"
        Me.chkBPSimpleCopy.Size = New System.Drawing.Size(15, 14)
        Me.chkBPSimpleCopy.TabIndex = 6
        Me.chkBPSimpleCopy.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBPSimpleCopy.UseVisualStyleBackColor = True
        '
        'rbtnBPCopyInvREMats
        '
        Me.rbtnBPCopyInvREMats.AutoSize = True
        Me.rbtnBPCopyInvREMats.Location = New System.Drawing.Point(201, 38)
        Me.rbtnBPCopyInvREMats.Name = "rbtnBPCopyInvREMats"
        Me.rbtnBPCopyInvREMats.Size = New System.Drawing.Size(89, 17)
        Me.rbtnBPCopyInvREMats.TabIndex = 4
        Me.rbtnBPCopyInvREMats.TabStop = True
        Me.rbtnBPCopyInvREMats.Text = "Invention/RE"
        Me.rbtnBPCopyInvREMats.UseVisualStyleBackColor = True
        '
        'rbtnBPComponentCopy
        '
        Me.rbtnBPComponentCopy.AutoSize = True
        Me.rbtnBPComponentCopy.Checked = True
        Me.rbtnBPComponentCopy.Location = New System.Drawing.Point(201, 8)
        Me.rbtnBPComponentCopy.Name = "rbtnBPComponentCopy"
        Me.rbtnBPComponentCopy.Size = New System.Drawing.Size(84, 17)
        Me.rbtnBPComponentCopy.TabIndex = 2
        Me.rbtnBPComponentCopy.TabStop = True
        Me.rbtnBPComponentCopy.Text = "Components"
        Me.rbtnBPComponentCopy.UseVisualStyleBackColor = True
        '
        'rbtnBPRawmatCopy
        '
        Me.rbtnBPRawmatCopy.AutoSize = True
        Me.rbtnBPRawmatCopy.Location = New System.Drawing.Point(201, 23)
        Me.rbtnBPRawmatCopy.Name = "rbtnBPRawmatCopy"
        Me.rbtnBPRawmatCopy.Size = New System.Drawing.Size(92, 17)
        Me.rbtnBPRawmatCopy.TabIndex = 3
        Me.rbtnBPRawmatCopy.TabStop = True
        Me.rbtnBPRawmatCopy.Text = "Raw Materials"
        Me.rbtnBPRawmatCopy.UseVisualStyleBackColor = True
        '
        'btnBPCopyMatstoClip
        '
        Me.btnBPCopyMatstoClip.Location = New System.Drawing.Point(85, 12)
        Me.btnBPCopyMatstoClip.Name = "btnBPCopyMatstoClip"
        Me.btnBPCopyMatstoClip.Size = New System.Drawing.Size(79, 39)
        Me.btnBPCopyMatstoClip.TabIndex = 1
        Me.btnBPCopyMatstoClip.Text = "Copy to Clipboard"
        Me.btnBPCopyMatstoClip.UseVisualStyleBackColor = True
        '
        'btnBPAddBPMatstoShoppingList
        '
        Me.btnBPAddBPMatstoShoppingList.Location = New System.Drawing.Point(5, 12)
        Me.btnBPAddBPMatstoShoppingList.Name = "btnBPAddBPMatstoShoppingList"
        Me.btnBPAddBPMatstoShoppingList.Size = New System.Drawing.Size(79, 39)
        Me.btnBPAddBPMatstoShoppingList.TabIndex = 0
        Me.btnBPAddBPMatstoShoppingList.Text = "Add to Shopping List"
        Me.btnBPAddBPMatstoShoppingList.UseVisualStyleBackColor = True
        '
        'lblBPSimpleCopy
        '
        Me.lblBPSimpleCopy.Location = New System.Drawing.Point(164, 9)
        Me.lblBPSimpleCopy.Name = "lblBPSimpleCopy"
        Me.lblBPSimpleCopy.Size = New System.Drawing.Size(39, 28)
        Me.lblBPSimpleCopy.TabIndex = 18
        Me.lblBPSimpleCopy.Text = "Simple Copy"
        Me.lblBPSimpleCopy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBPCanMakeBPAll
        '
        Me.lblBPCanMakeBPAll.ForeColor = System.Drawing.Color.Red
        Me.lblBPCanMakeBPAll.Location = New System.Drawing.Point(723, 591)
        Me.lblBPCanMakeBPAll.Name = "lblBPCanMakeBPAll"
        Me.lblBPCanMakeBPAll.Size = New System.Drawing.Size(205, 16)
        Me.lblBPCanMakeBPAll.TabIndex = 27
        Me.lblBPCanMakeBPAll.Text = "Cannot Make All Components for this Item"
        Me.lblBPCanMakeBPAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRawMatCost
        '
        Me.lblBPRawMatCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPRawMatCost.Location = New System.Drawing.Point(1013, 591)
        Me.lblBPRawMatCost.Name = "lblBPRawMatCost"
        Me.lblBPRawMatCost.Size = New System.Drawing.Size(118, 16)
        Me.lblBPRawMatCost.TabIndex = 24
        Me.lblBPRawMatCost.Text = "0.00"
        Me.lblBPRawMatCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPRawMatCost1
        '
        Me.lblBPRawMatCost1.AutoSize = True
        Me.lblBPRawMatCost1.Location = New System.Drawing.Point(934, 593)
        Me.lblBPRawMatCost1.Name = "lblBPRawMatCost1"
        Me.lblBPRawMatCost1.Size = New System.Drawing.Size(77, 13)
        Me.lblBPRawMatCost1.TabIndex = 23
        Me.lblBPRawMatCost1.Text = "Raw Mat Cost:"
        Me.lblBPRawMatCost1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPCanMakeBP
        '
        Me.lblBPCanMakeBP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPCanMakeBP.ForeColor = System.Drawing.Color.Red
        Me.lblBPCanMakeBP.Location = New System.Drawing.Point(217, 589)
        Me.lblBPCanMakeBP.Name = "lblBPCanMakeBP"
        Me.lblBPCanMakeBP.Size = New System.Drawing.Size(115, 21)
        Me.lblBPCanMakeBP.TabIndex = 13
        Me.lblBPCanMakeBP.Text = "Cannot Make this Item"
        Me.lblBPCanMakeBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPRawMats
        '
        Me.lblBPRawMats.Location = New System.Drawing.Point(569, 235)
        Me.lblBPRawMats.Name = "lblBPRawMats"
        Me.lblBPRawMats.Size = New System.Drawing.Size(562, 20)
        Me.lblBPRawMats.TabIndex = 14
        Me.lblBPRawMats.Text = "Raw Material List"
        Me.lblBPRawMats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPComponentMatCost
        '
        Me.lblBPComponentMatCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBPComponentMatCost.Location = New System.Drawing.Point(448, 591)
        Me.lblBPComponentMatCost.Name = "lblBPComponentMatCost"
        Me.lblBPComponentMatCost.Size = New System.Drawing.Size(118, 16)
        Me.lblBPComponentMatCost.TabIndex = 26
        Me.lblBPComponentMatCost.Text = "0.00"
        Me.lblBPComponentMatCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPComponentMats
        '
        Me.lblBPComponentMats.Location = New System.Drawing.Point(4, 235)
        Me.lblBPComponentMats.Name = "lblBPComponentMats"
        Me.lblBPComponentMats.Size = New System.Drawing.Size(562, 20)
        Me.lblBPComponentMats.TabIndex = 13
        Me.lblBPComponentMats.Text = "Component Material List"
        Me.lblBPComponentMats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPComponentMatCost1
        '
        Me.lblBPComponentMatCost1.AutoSize = True
        Me.lblBPComponentMatCost1.Location = New System.Drawing.Point(338, 593)
        Me.lblBPComponentMatCost1.Name = "lblBPComponentMatCost1"
        Me.lblBPComponentMatCost1.Size = New System.Drawing.Size(109, 13)
        Me.lblBPComponentMatCost1.TabIndex = 25
        Me.lblBPComponentMatCost1.Text = "Component Mat Cost:"
        Me.lblBPComponentMatCost1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lstBPComponentMats
        '
        Me.lstBPComponentMats.BackColor = System.Drawing.SystemColors.Window
        Me.lstBPComponentMats.CheckBoxes = True
        Me.lstBPComponentMats.FullRowSelect = True
        Me.lstBPComponentMats.GridLines = True
        Me.lstBPComponentMats.HideSelection = False
        Me.lstBPComponentMats.Location = New System.Drawing.Point(4, 258)
        Me.lstBPComponentMats.MultiSelect = False
        Me.lstBPComponentMats.Name = "lstBPComponentMats"
        Me.lstBPComponentMats.Size = New System.Drawing.Size(561, 329)
        Me.lstBPComponentMats.TabIndex = 35
        Me.lstBPComponentMats.TabStop = False
        Me.lstBPComponentMats.UseCompatibleStateImageBehavior = False
        Me.lstBPComponentMats.View = System.Windows.Forms.View.Details
        '
        'lstBPRawMats
        '
        Me.lstBPRawMats.BackColor = System.Drawing.SystemColors.Window
        Me.lstBPRawMats.FullRowSelect = True
        Me.lstBPRawMats.GridLines = True
        Me.lstBPRawMats.HideSelection = False
        Me.lstBPRawMats.Location = New System.Drawing.Point(570, 258)
        Me.lstBPRawMats.MultiSelect = False
        Me.lstBPRawMats.Name = "lstBPRawMats"
        Me.lstBPRawMats.Size = New System.Drawing.Size(561, 329)
        Me.lstBPRawMats.TabIndex = 34
        Me.lstBPRawMats.TabStop = False
        Me.lstBPRawMats.UseCompatibleStateImageBehavior = False
        Me.lstBPRawMats.View = System.Windows.Forms.View.Details
        '
        'lstBPBuiltComponents
        '
        Me.lstBPBuiltComponents.FullRowSelect = True
        Me.lstBPBuiltComponents.GridLines = True
        Me.lstBPBuiltComponents.HideSelection = False
        Me.lstBPBuiltComponents.Location = New System.Drawing.Point(4, 258)
        Me.lstBPBuiltComponents.MultiSelect = False
        Me.lstBPBuiltComponents.Name = "lstBPBuiltComponents"
        Me.lstBPBuiltComponents.Size = New System.Drawing.Size(561, 329)
        Me.lstBPBuiltComponents.TabIndex = 77
        Me.lstBPBuiltComponents.TabStop = False
        Me.lstBPBuiltComponents.UseCompatibleStateImageBehavior = False
        Me.lstBPBuiltComponents.View = System.Windows.Forms.View.Details
        Me.lstBPBuiltComponents.Visible = False
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabBlueprints)
        Me.tabMain.Controls.Add(Me.tabUpdatePrices)
        Me.tabMain.Controls.Add(Me.tabManufacturing)
        Me.tabMain.Controls.Add(Me.tabDatacores)
        Me.tabMain.Controls.Add(Me.tabMining)
        Me.tabMain.Controls.Add(Me.tabPI)
        Me.tabMain.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tabMain.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.tabMain.Location = New System.Drawing.Point(2, 26)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(1145, 641)
        Me.tabMain.TabIndex = 1
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1149, 691)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.mnuStripMain)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuStripMain
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE ISK per Hour"
        Me.mnuStripMain.ResumeLayout(False)
        Me.mnuStripMain.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ListOptionsMenu.ResumeLayout(False)
        Me.tabPI.ResumeLayout(False)
        Me.gbPIPlanets.ResumeLayout(False)
        Me.gbPIPlanets.PerformLayout()
        Me.tabMining.ResumeLayout(False)
        Me.tabMining.PerformLayout()
        Me.gbMineCrystalType.ResumeLayout(False)
        Me.gbMineCrystalType.PerformLayout()
        Me.tabMiningDrones.ResumeLayout(False)
        Me.tabShipDrones.ResumeLayout(False)
        Me.tabShipDrones.PerformLayout()
        Me.tabBoosterDrones.ResumeLayout(False)
        Me.tabBoosterDrones.PerformLayout()
        Me.gbMineCrystals.ResumeLayout(False)
        Me.gbMineCrystals.PerformLayout()
        Me.gbMineNumberMiners.ResumeLayout(False)
        Me.gbMineNumberMiners.PerformLayout()
        Me.gbMineOreProcessingType.ResumeLayout(False)
        Me.gbMineOreProcessingType.PerformLayout()
        Me.gbMineTaxBroker.ResumeLayout(False)
        Me.gbMineTaxBroker.PerformLayout()
        Me.gbMineStripStats.ResumeLayout(False)
        Me.gbMineStripStats.PerformLayout()
        Me.gbMineHauling.ResumeLayout(False)
        Me.gbMineHauling.PerformLayout()
        Me.gbMineBooster.ResumeLayout(False)
        Me.gbMineBooster.PerformLayout()
        CType(Me.pictMineLaserOptmize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictMineRangeLink, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictMineFleetBoostShip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMineRefining.ResumeLayout(False)
        Me.gbMineRefining.PerformLayout()
        Me.gbMineOreStuctureRates.ResumeLayout(False)
        Me.gbMineOreStuctureRates.PerformLayout()
        Me.tabMiningProcessingSkills.ResumeLayout(False)
        Me.tabPageOres.ResumeLayout(False)
        Me.tabPageOres.PerformLayout()
        Me.tabPageMoonOres.ResumeLayout(False)
        Me.tabPageMoonOres.PerformLayout()
        Me.tabPageIce.ResumeLayout(False)
        Me.tabPageIce.PerformLayout()
        Me.gbMineShipSetup.ResumeLayout(False)
        Me.gbMineSelectShip.ResumeLayout(False)
        Me.gbMineSelectShip.PerformLayout()
        CType(Me.pictMineSelectedShip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMineShipEquipment.ResumeLayout(False)
        Me.gbMineShipEquipment.PerformLayout()
        Me.gbMiningRigs.ResumeLayout(False)
        Me.gbMineSkills.ResumeLayout(False)
        Me.gbMineSkills.PerformLayout()
        Me.gbMineMain.ResumeLayout(False)
        Me.gbMineMain.PerformLayout()
        Me.gbMineIncludeOres.ResumeLayout(False)
        Me.gbMineIncludeOres.PerformLayout()
        Me.gbMineOreLocSov.ResumeLayout(False)
        Me.gbMineOreLocSov.PerformLayout()
        Me.gbMineWHSpace.ResumeLayout(False)
        Me.gbMineWHSpace.PerformLayout()
        Me.tabDatacores.ResumeLayout(False)
        Me.gbDCOptions.ResumeLayout(False)
        Me.gbDCAgentLocSov.ResumeLayout(False)
        Me.gbDCAgentLocSov.PerformLayout()
        Me.gbDCTotalIPH.ResumeLayout(False)
        Me.gbDCTotalIPH.PerformLayout()
        Me.gbDCPrices.ResumeLayout(False)
        Me.gbDCPrices.PerformLayout()
        Me.gbDCAgentTypes.ResumeLayout(False)
        Me.gbDCAgentTypes.PerformLayout()
        Me.gbDCBaseSkills.ResumeLayout(False)
        Me.gbDCBaseSkills.PerformLayout()
        Me.gbDCDatacores.ResumeLayout(False)
        Me.gbDCDatacores.PerformLayout()
        Me.gbDCCodes.ResumeLayout(False)
        Me.gbDCCodes.PerformLayout()
        Me.gbDCCorpMinmatar.ResumeLayout(False)
        Me.gbDCCorpMinmatar.PerformLayout()
        Me.gbDCCorpAmarr.ResumeLayout(False)
        Me.gbDCCorpAmarr.PerformLayout()
        Me.gbDCCorpsCaldari.ResumeLayout(False)
        Me.gbDCCorpsCaldari.PerformLayout()
        Me.gbDCCorpsGallente.ResumeLayout(False)
        Me.gbDCCorpsGallente.PerformLayout()
        Me.tabManufacturing.ResumeLayout(False)
        Me.gbCalcBPSelectOptions.ResumeLayout(False)
        Me.gbCalcBPSelectOptions.PerformLayout()
        Me.gbCalcIgnoreinCalcs.ResumeLayout(False)
        Me.gbCalcIgnoreinCalcs.PerformLayout()
        Me.gbIncludeTaxesFees.ResumeLayout(False)
        Me.gbIncludeTaxesFees.PerformLayout()
        Me.gbCalcSellExessItems.ResumeLayout(False)
        Me.gbCalcSellExessItems.PerformLayout()
        Me.gbCalcIncludeItems.ResumeLayout(False)
        Me.gbCalcIncludeItems.PerformLayout()
        Me.gbCalcMarketFilters.ResumeLayout(False)
        Me.gbCalcMarketFilters.PerformLayout()
        Me.gbCalcSizeLimit.ResumeLayout(False)
        Me.gbCalcSizeLimit.PerformLayout()
        Me.gbCalcProdLines.ResumeLayout(False)
        Me.gbCalcProdLines.PerformLayout()
        Me.gbCalcCompareType.ResumeLayout(False)
        Me.gbCalcCompareType.PerformLayout()
        Me.gbCalcTextColors.ResumeLayout(False)
        Me.gbCalcTextColors.PerformLayout()
        Me.gbCalcInvention.ResumeLayout(False)
        Me.gbCalcInvention.PerformLayout()
        Me.gbCalcBPRace.ResumeLayout(False)
        Me.gbCalcBPRace.PerformLayout()
        Me.gbTempMEPE.ResumeLayout(False)
        Me.gbTempMEPE.PerformLayout()
        Me.tabCalcFacilities.ResumeLayout(False)
        Me.tabCalcFacilityBase.ResumeLayout(False)
        Me.tabCalcFacilityComponents.ResumeLayout(False)
        Me.tabCalcFacilityCopy.ResumeLayout(False)
        Me.tabCalcFacilityT2Invention.ResumeLayout(False)
        Me.tabCalcFacilityT3Invention.ResumeLayout(False)
        Me.tabCalcFacilitySupers.ResumeLayout(False)
        Me.tabCalcFacilityCapitals.ResumeLayout(False)
        Me.tabCalcFacilityT3Ships.ResumeLayout(False)
        Me.tabCalcFacilitySubsystems.ResumeLayout(False)
        Me.tabCalcFacilityBoosters.ResumeLayout(False)
        Me.tabCalcFacilityReactions.ResumeLayout(False)
        Me.gbCalcFilter.ResumeLayout(False)
        Me.gbCalcBPTech.ResumeLayout(False)
        Me.gbCalcBPTech.PerformLayout()
        Me.gbCalcIncludeOwned.ResumeLayout(False)
        Me.gbCalcIncludeOwned.PerformLayout()
        Me.gbCalcTextFilter.ResumeLayout(False)
        Me.gbCalcTextFilter.PerformLayout()
        Me.gbCalcBPType.ResumeLayout(False)
        Me.gbCalcBPType.PerformLayout()
        Me.gbCalcBPSelect.ResumeLayout(False)
        Me.gbCalcBPSelect.PerformLayout()
        Me.gbCalcRelics.ResumeLayout(False)
        Me.gbCalcRelics.PerformLayout()
        Me.tabUpdatePrices.ResumeLayout(False)
        Me.tabUpdatePrices.PerformLayout()
        Me.gbRawMaterials.ResumeLayout(False)
        Me.gbRawMaterials.PerformLayout()
        Me.gbReactionMaterials.ResumeLayout(False)
        Me.gbReactionMaterials.PerformLayout()
        Me.gbResearchEquipment.ResumeLayout(False)
        Me.gbResearchEquipment.PerformLayout()
        Me.gbSingleSource.ResumeLayout(False)
        Me.gbMarketStructures.ResumeLayout(False)
        Me.gbRegionSystemPrice.ResumeLayout(False)
        Me.gbTradeHubSystems.ResumeLayout(False)
        Me.gbTradeHubSystems.PerformLayout()
        Me.gbPriceProfile.ResumeLayout(False)
        Me.tabPriceProfile.ResumeLayout(False)
        Me.tabPriceProfileRaw.ResumeLayout(False)
        Me.tabPriceProfileManufactured.ResumeLayout(False)
        Me.gbPPDefaultSettings.ResumeLayout(False)
        Me.gbPPDefaultSettings.PerformLayout()
        Me.gbPriceOptions.ResumeLayout(False)
        Me.gbPriceOptions.PerformLayout()
        Me.gbPriceTypes.ResumeLayout(False)
        Me.gbPriceTypes.PerformLayout()
        Me.gbDataSource.ResumeLayout(False)
        Me.gbDataSource.PerformLayout()
        Me.gbManufacturedItems.ResumeLayout(False)
        Me.gbManufacturedItems.PerformLayout()
        Me.gbComponents.ResumeLayout(False)
        Me.gbComponents.PerformLayout()
        Me.gbReprocessables.ResumeLayout(False)
        Me.gbReprocessables.PerformLayout()
        Me.gbItems.ResumeLayout(False)
        Me.gbItems.PerformLayout()
        Me.gbPricesTech.ResumeLayout(False)
        Me.gbPricesTech.PerformLayout()
        Me.tabBlueprints.ResumeLayout(False)
        Me.tabBlueprints.PerformLayout()
        CType(Me.pbReactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbBPMEPEImage.ResumeLayout(False)
        Me.gbBPMEPEImage.PerformLayout()
        Me.gbBPSellExcess.ResumeLayout(False)
        Me.tabBPInventionEquip.ResumeLayout(False)
        Me.tabFacility.ResumeLayout(False)
        Me.tabT3Calcs.ResumeLayout(False)
        Me.tabT3Calcs.PerformLayout()
        Me.tabInventionCalcs.ResumeLayout(False)
        Me.tabInventionCalcs.PerformLayout()
        CType(Me.pictBP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbBPManualSystemCostIndex.ResumeLayout(False)
        Me.gbBPManualSystemCostIndex.PerformLayout()
        Me.gbBPIgnoreinCalcs.ResumeLayout(False)
        Me.gbBPIgnoreinCalcs.PerformLayout()
        Me.gbBPBlueprintType.ResumeLayout(False)
        Me.gbBPBlueprintType.PerformLayout()
        Me.gbBPBlueprintTech.ResumeLayout(False)
        Me.gbBPBlueprintTech.PerformLayout()
        Me.gbFilters.ResumeLayout(False)
        Me.gbFilters.PerformLayout()
        Me.gbBPInventionStats.ResumeLayout(False)
        Me.gbBPInventionStats.PerformLayout()
        Me.gbBPShopandCopy.ResumeLayout(False)
        Me.gbBPShopandCopy.PerformLayout()
        Me.tabMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuStripMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectionExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectionAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlMain As System.Windows.Forms.StatusStrip
    Friend WithEvents pnlStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnlShoppingList As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuSelectDefaultChar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItemUpdatePrices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectionAddChar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuManageBlueprintsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUserSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbSystems As System.Windows.Forms.GroupBox
    Friend WithEvents ttBP As System.Windows.Forms.ToolTip
    Friend WithEvents pnlProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents mnuCheckforUpdates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectionShoppingList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCharacterSkills As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCharacterStandings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlSkills As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSelectionManageCharacters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetPOSDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestoreDefaultTabSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestoreDefaultBP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestoreDefaultUpdatePrices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestoreDefaultManufacturing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestoreDefaultDatacores As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestoreDefaultMining As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCurrentResearchAgents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnuPatchNotes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCurrentIndustryJobs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuReprocessingPlant As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateIndustryFacilities As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateESIMarketPrices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuResetData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetBlueprintData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetPriceData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetAgents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetIndustryJobs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetAssets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuResetAllData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetESIDates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListOptionsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuResetESIMarketPrices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetESIIndustryFacilities As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IgnoreBlueprintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetIgnoredBPs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCharacter As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsCharacter1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCharacter20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ttUpdatePrices As System.Windows.Forms.ToolTip
    Friend WithEvents ttManufacturing As System.Windows.Forms.ToolTip
    Friend WithEvents ttDatacores As System.Windows.Forms.ToolTip
    Friend WithEvents ttReactions As System.Windows.Forms.ToolTip
    Friend WithEvents ttMining As System.Windows.Forms.ToolTip
    Friend WithEvents ttPI As System.Windows.Forms.ToolTip
    Friend WithEvents ViewMarketHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CalcImageList As System.Windows.Forms.ImageList
    Friend WithEvents AddToShoppingListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearBPHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetMarketHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuResetMarketOrders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewAssets As ToolStripMenuItem
    Friend WithEvents mnuUpdateESIPublicStructures As ToolStripMenuItem
    Friend WithEvents mnuResetESIPublicStructures As ToolStripMenuItem
    Friend WithEvents mnuChangeDummyCharacterName As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents mnuViewESIStatus As ToolStripMenuItem
    Friend WithEvents tabPI As TabPage
    Friend WithEvents btnPISaveSettings As Button
    Friend WithEvents gbPIPlanets As GroupBox
    Friend WithEvents chkPILava As CheckBox
    Friend WithEvents chkPIPlasma As CheckBox
    Friend WithEvents chkPIIce As CheckBox
    Friend WithEvents chkPIGas As CheckBox
    Friend WithEvents chkPIOcean As CheckBox
    Friend WithEvents chkPIBarren As CheckBox
    Friend WithEvents chkPIStorm As CheckBox
    Friend WithEvents chkPITemperate As CheckBox
    Friend WithEvents btnPIReset As Button
    Friend WithEvents tabMining As TabPage
    Friend WithEvents gbMineNumberMiners As GroupBox
    Friend WithEvents txtMineNumberMiners As TextBox
    Friend WithEvents lblMineNumberMiners As Label
    Friend WithEvents gbMineOreProcessingType As GroupBox
    Friend WithEvents chkMineUnrefinedOre As CheckBox
    Friend WithEvents chkMineRefinedOre As CheckBox
    Friend WithEvents chkMineCompressedOre As CheckBox
    Friend WithEvents btnMineSaveAllSettings As Button
    Friend WithEvents gbMineTaxBroker As GroupBox
    Friend WithEvents chkMineIncludeTaxes As CheckBox
    Friend WithEvents chkMineIncludeBrokerFees As CheckBox
    Friend WithEvents gbMineStripStats As GroupBox
    Friend WithEvents lblMineRange As Label
    Friend WithEvents lblMineCycleTime1 As Label
    Friend WithEvents lblMineRange1 As Label
    Friend WithEvents lblMineCycleTime As Label
    Friend WithEvents chkMineUseFleetBooster As CheckBox
    Friend WithEvents btnMineReset As Button
    Friend WithEvents gbMineHauling As GroupBox
    Friend WithEvents txtMineHaulerM3 As TextBox
    Friend WithEvents lblMineHaulerM3 As Label
    Friend WithEvents lblMineRTSec As Label
    Friend WithEvents chkMineUseHauler As CheckBox
    Friend WithEvents lblMineRTMin As Label
    Friend WithEvents txtMineRTMin As TextBox
    Friend WithEvents txtMineRTSec As TextBox
    Friend WithEvents lblMineRoundTripTime As Label
    Friend WithEvents btnMineRefresh As Button
    Friend WithEvents gbMineBooster As GroupBox
    Friend WithEvents pictMineLaserOptmize As PictureBox
    Friend WithEvents pictMineRangeLink As PictureBox
    Friend WithEvents pictMineFleetBoostShip As PictureBox
    Friend WithEvents chkMineForemanLaserRangeBoost As CheckBox
    Friend WithEvents cmbMineIndustReconfig As ComboBox
    Friend WithEvents lblMineIndustrialReconfig As Label
    Friend WithEvents chkMineIndyCoreDeployedMode As CheckBox
    Friend WithEvents cmbMineBoosterShipSkill As ComboBox
    Friend WithEvents chkMineForemanMindlink As CheckBox
    Friend WithEvents cmbMineBoosterShipName As ComboBox
    Friend WithEvents cmbMineMiningDirector As ComboBox
    Friend WithEvents chkMineForemanLaserOpBoost As CheckBox
    Friend WithEvents lblMineMiningDirector As Label
    Friend WithEvents cmbMineMiningForeman As ComboBox
    Friend WithEvents lblMineFleetBoosterShip As Label
    Friend WithEvents lblMineMiningForeman As Label
    Friend WithEvents lblMineBoosterShipSkill As Label
    Friend WithEvents gbMineRefining As GroupBox
    Friend WithEvents cmbMineRefining As ComboBox
    Friend WithEvents lblMineRefining As Label
    Friend WithEvents cmbMineRefineryEff As ComboBox
    Friend WithEvents lblMineRefineryEfficiency As Label
    Friend WithEvents gbMineShipSetup As GroupBox
    Friend WithEvents gbMineSelectShip As GroupBox
    Friend WithEvents pictMineSelectedShip As PictureBox
    Friend WithEvents lblMineSelectShip As Label
    Friend WithEvents cmbMineBaseShipSkill As ComboBox
    Friend WithEvents cmbMineAdvShipSkill As ComboBox
    Friend WithEvents cmbMineShipName As ComboBox
    Friend WithEvents lblMineBaseShipSkill As Label
    Friend WithEvents lblMineExhumers As Label
    Friend WithEvents gbMineShipEquipment As GroupBox
    Friend WithEvents gbMiningRigs As GroupBox
    Friend WithEvents cmbMineMiningLaser As ComboBox
    Friend WithEvents lblMineMiningUpgrade As Label
    Friend WithEvents gbMineCrystals As GroupBox
    Friend WithEvents cmbMineNumMiningUpgrades As ComboBox
    Friend WithEvents cmbMineNumLasers As ComboBox
    Friend WithEvents cmbMineMiningUpgrade As ComboBox
    Friend WithEvents cmbMineHighwallImplant As ComboBox
    Friend WithEvents chkMineMichiImplant As CheckBox
    Friend WithEvents lblMineImplants As Label
    Friend WithEvents lblMineLaserNumber As Label
    Friend WithEvents lblMineNumMiningUpgrades As Label
    Friend WithEvents lblMineMinerTurret As Label
    Friend WithEvents gbMineSkills As GroupBox
    Friend WithEvents cmbMineGasIceHarvesting As ComboBox
    Friend WithEvents lblMineGasIceHarvesting As Label
    Friend WithEvents cmbMineDeepCore As ComboBox
    Friend WithEvents lblMineAstrogeology As Label
    Friend WithEvents cmbMineSkill As ComboBox
    Friend WithEvents lblMineSkill As Label
    Friend WithEvents cmbMineAstrogeology As ComboBox
    Friend WithEvents lblMineDeepCore As Label
    Friend WithEvents gbMineMain As GroupBox
    Friend WithEvents gbMineIncludeOres As GroupBox
    Friend WithEvents chkMineIncludeHighSec As CheckBox
    Friend WithEvents chkMineIncludeNullSec As CheckBox
    Friend WithEvents chkMineIncludeLowSec As CheckBox
    Friend WithEvents chkMineIncludeHighYieldOre As CheckBox
    Friend WithEvents lblMineType As Label
    Friend WithEvents cmbMineOreType As ComboBox
    Friend WithEvents gbMineOreLocSov As GroupBox
    Friend WithEvents chkMineMoonMining As CheckBox
    Friend WithEvents chkMineWH As CheckBox
    Friend WithEvents gbMineWHSpace As GroupBox
    Friend WithEvents chkMineC6 As CheckBox
    Friend WithEvents chkMineC2 As CheckBox
    Friend WithEvents chkMineC4 As CheckBox
    Friend WithEvents chkMineC1 As CheckBox
    Friend WithEvents chkMineC5 As CheckBox
    Friend WithEvents chkMineC3 As CheckBox
    Friend WithEvents chkMineCaldari As CheckBox
    Friend WithEvents chkMineMinmatar As CheckBox
    Friend WithEvents chkMineGallente As CheckBox
    Friend WithEvents chkMineAmarr As CheckBox
    Friend WithEvents lstMineGrid As ListView
    Friend WithEvents tabDatacores As TabPage
    Friend WithEvents lstDC As ListView
    Friend WithEvents gbDCOptions As GroupBox
    Friend WithEvents btnDCSaveSettings As Button
    Friend WithEvents gbDCAgentLocSov As GroupBox
    Friend WithEvents chkDCThukkerSov As CheckBox
    Friend WithEvents chkDCKhanidSov As CheckBox
    Friend WithEvents chkDCMinmatarSov As CheckBox
    Friend WithEvents chkDCSyndicateSov As CheckBox
    Friend WithEvents chkDCGallenteSov As CheckBox
    Friend WithEvents chkDCAmarrSov As CheckBox
    Friend WithEvents chkDCAmmatarSov As CheckBox
    Friend WithEvents chkDCCaldariSov As CheckBox
    Friend WithEvents gbDCTotalIPH As GroupBox
    Friend WithEvents lblDCTotalOptIPH As Label
    Friend WithEvents lblDCTotalSelectedIPH As Label
    Friend WithEvents txtDCTotalSelectedIPH As TextBox
    Friend WithEvents txtDCTotalOptIPH As TextBox
    Friend WithEvents gbDCPrices As GroupBox
    Friend WithEvents rbtnDCSystemPrices As RadioButton
    Friend WithEvents rbtnDCRegionPrices As RadioButton
    Friend WithEvents rbtnDCUpdatedPrices As RadioButton
    Friend WithEvents gbDCAgentTypes As GroupBox
    Friend WithEvents cmbDCRegions As ComboBox
    Friend WithEvents lblDCRegion As Label
    Friend WithEvents chkDCLowSecAgents As CheckBox
    Friend WithEvents chkDCHighSecAgents As CheckBox
    Friend WithEvents chkDCIncludeAllAgents As CheckBox
    Friend WithEvents gbDCBaseSkills As GroupBox
    Friend WithEvents cmbDCResearchMgmt As ComboBox
    Friend WithEvents lblDCResearchManagement As Label
    Friend WithEvents lblDCNegotiation As Label
    Friend WithEvents cmbDCConnections As ComboBox
    Friend WithEvents lblDCConnections As Label
    Friend WithEvents cmbDCNegotiation As ComboBox
    Friend WithEvents gbDCDatacores As GroupBox
    Friend WithEvents cmbDCSkillLevel17 As ComboBox
    Friend WithEvents cmbDCSkillLevel16 As ComboBox
    Friend WithEvents cmbDCSkillLevel15 As ComboBox
    Friend WithEvents cmbDCSkillLevel14 As ComboBox
    Friend WithEvents cmbDCSkillLevel13 As ComboBox
    Friend WithEvents cmbDCSkillLevel12 As ComboBox
    Friend WithEvents cmbDCSkillLevel11 As ComboBox
    Friend WithEvents cmbDCSkillLevel10 As ComboBox
    Friend WithEvents cmbDCSkillLevel9 As ComboBox
    Friend WithEvents cmbDCSkillLevel8 As ComboBox
    Friend WithEvents cmbDCSkillLevel7 As ComboBox
    Friend WithEvents cmbDCSkillLevel6 As ComboBox
    Friend WithEvents cmbDCSkillLevel5 As ComboBox
    Friend WithEvents cmbDCSkillLevel4 As ComboBox
    Friend WithEvents cmbDCSkillLevel3 As ComboBox
    Friend WithEvents cmbDCSkillLevel2 As ComboBox
    Friend WithEvents cmbDCSkillLevel1 As ComboBox
    Friend WithEvents chkDC17 As CheckBox
    Friend WithEvents chkDC4 As CheckBox
    Friend WithEvents chkDC16 As CheckBox
    Friend WithEvents chkDC3 As CheckBox
    Friend WithEvents lblDatacore17 As Label
    Friend WithEvents chkDC15 As CheckBox
    Friend WithEvents chkDC2 As CheckBox
    Friend WithEvents chkDC14 As CheckBox
    Friend WithEvents chkDC1 As CheckBox
    Friend WithEvents chkDC13 As CheckBox
    Friend WithEvents chkDC12 As CheckBox
    Friend WithEvents chkDC11 As CheckBox
    Friend WithEvents chkDC10 As CheckBox
    Friend WithEvents lblDatacore16 As Label
    Friend WithEvents lblDatacore4 As Label
    Friend WithEvents lblDatacore15 As Label
    Friend WithEvents chkDC9 As CheckBox
    Friend WithEvents lblDatacore14 As Label
    Friend WithEvents lblDatacore3 As Label
    Friend WithEvents chkDC8 As CheckBox
    Friend WithEvents lblDatacore13 As Label
    Friend WithEvents lblDatacore2 As Label
    Friend WithEvents chkDC7 As CheckBox
    Friend WithEvents chkDC6 As CheckBox
    Friend WithEvents lblDatacore1 As Label
    Friend WithEvents chkDC5 As CheckBox
    Friend WithEvents lblDatacore5 As Label
    Friend WithEvents lblDatacore6 As Label
    Friend WithEvents lblDatacore7 As Label
    Friend WithEvents lblDatacore8 As Label
    Friend WithEvents lblDatacore12 As Label
    Friend WithEvents lblDatacore11 As Label
    Friend WithEvents lblDatacore10 As Label
    Friend WithEvents lblDatacore9 As Label
    Friend WithEvents gbDCCodes As GroupBox
    Friend WithEvents lblDCColors As Label
    Friend WithEvents lblDCRedText As Label
    Friend WithEvents lblDCOrangeText As Label
    Friend WithEvents lblDCGrayText As Label
    Friend WithEvents lblDCBlueText As Label
    Friend WithEvents lblDCGreenBackColor As Label
    Friend WithEvents gbDCCorpMinmatar As GroupBox
    Friend WithEvents lblDCCorp13 As Label
    Friend WithEvents chkDCCorp13 As CheckBox
    Friend WithEvents txtDCStanding13 As TextBox
    Friend WithEvents lblDCCorpLabel4 As Label
    Friend WithEvents lblDCCorp10 As Label
    Friend WithEvents lblDCCorp11 As Label
    Friend WithEvents lblDCCorp12 As Label
    Friend WithEvents chkDCCorp10 As CheckBox
    Friend WithEvents chkDCCorp11 As CheckBox
    Friend WithEvents chkDCCorp12 As CheckBox
    Friend WithEvents txtDCStanding10 As TextBox
    Friend WithEvents txtDCStanding11 As TextBox
    Friend WithEvents txtDCStanding12 As TextBox
    Friend WithEvents lblDCStanding4 As Label
    Friend WithEvents btnDCExporttoClip As Button
    Friend WithEvents gbDCCorpAmarr As GroupBox
    Friend WithEvents lblDCCorpLabel1 As Label
    Friend WithEvents lblDCCorp1 As Label
    Friend WithEvents lblDCCorp2 As Label
    Friend WithEvents lblDCCorp3 As Label
    Friend WithEvents chkDCCorp1 As CheckBox
    Friend WithEvents chkDCCorp2 As CheckBox
    Friend WithEvents chkDCCorp3 As CheckBox
    Friend WithEvents txtDCStanding1 As TextBox
    Friend WithEvents txtDCStanding2 As TextBox
    Friend WithEvents txtDCStanding3 As TextBox
    Friend WithEvents lblDCStanding1 As Label
    Friend WithEvents btnDCReset As Button
    Friend WithEvents gbDCCorpsCaldari As GroupBox
    Friend WithEvents lblDCCorpLabel2 As Label
    Friend WithEvents lblDCCorp4 As Label
    Friend WithEvents lblDCCorp5 As Label
    Friend WithEvents lblDCCorp6 As Label
    Friend WithEvents chkDCCorp4 As CheckBox
    Friend WithEvents chkDCCorp5 As CheckBox
    Friend WithEvents chkDCCorp6 As CheckBox
    Friend WithEvents txtDCStanding4 As TextBox
    Friend WithEvents txtDCStanding5 As TextBox
    Friend WithEvents txtDCStanding6 As TextBox
    Friend WithEvents lblDCStanding2 As Label
    Friend WithEvents gbDCCorpsGallente As GroupBox
    Friend WithEvents lblDCCorpLabel3 As Label
    Friend WithEvents lblDCCorp7 As Label
    Friend WithEvents lblDCCorp8 As Label
    Friend WithEvents lblDCCorp9 As Label
    Friend WithEvents chkDCCorp7 As CheckBox
    Friend WithEvents chkDCCorp8 As CheckBox
    Friend WithEvents chkDCCorp9 As CheckBox
    Friend WithEvents txtDCStanding7 As TextBox
    Friend WithEvents txtDCStanding8 As TextBox
    Friend WithEvents txtDCStanding9 As TextBox
    Friend WithEvents lblDCStanding3 As Label
    Friend WithEvents btnDCRefresh As Button
    Friend WithEvents tabManufacturing As TabPage
    Friend WithEvents lstManufacturing As ManufacturingListView
    Friend WithEvents gbCalcBPSelectOptions As GroupBox
    Friend WithEvents gbCalcSellExessItems As GroupBox
    Friend WithEvents chkCalcSellExessItems As CheckBox
    Friend WithEvents chkCalcNPCBPOs As CheckBox
    Friend WithEvents btnCalcShowAssets As Button
    Friend WithEvents gbCalcIncludeItems As GroupBox
    Friend WithEvents chkCalcCanInvent As CheckBox
    Friend WithEvents chkCalcCanBuild As CheckBox
    Friend WithEvents gbCalcMarketFilters As GroupBox
    Friend WithEvents txtCalcProfitThreshold As TextBox
    Friend WithEvents tpMaxBuildTimeFilter As TimePicker
    Friend WithEvents txtCalcSVRThreshold As TextBox
    Friend WithEvents tpMinBuildTimeFilter As TimePicker
    Friend WithEvents chkCalcMaxBuildTimeFilter As CheckBox
    Friend WithEvents chkCalcMinBuildTimeFilter As CheckBox
    Friend WithEvents cmbCalcPriceTrend As ComboBox
    Friend WithEvents cmbCalcAvgPriceDuration As ComboBox
    Friend WithEvents lblCalcPriceTrend As Label
    Friend WithEvents txtCalcVolumeThreshold As TextBox
    Friend WithEvents cmbCalcHistoryRegion As ComboBox
    Friend WithEvents lblCalcHistoryRegion As Label
    Friend WithEvents lblCalcSVRThreshold As Label
    Friend WithEvents lblCalcAvgPrice As Label
    Friend WithEvents txtCalcIPHThreshold As TextBox
    Friend WithEvents chkCalcProfitThreshold As CheckBox
    Friend WithEvents chkCalcVolumeThreshold As CheckBox
    Friend WithEvents chkCalcSVRIncludeNull As CheckBox
    Friend WithEvents chkCalcIPHThreshold As CheckBox
    Friend WithEvents btnCalcCalculate As Button
    Friend WithEvents gbCalcIgnoreinCalcs As GroupBox
    Friend WithEvents chkCalcIgnoreMinerals As CheckBox
    Friend WithEvents chkCalcIgnoreT1Item As CheckBox
    Friend WithEvents chkCalcIgnoreInvention As CheckBox
    Friend WithEvents gbIncludeTaxesFees As GroupBox
    Friend WithEvents chkCalcFees As CheckBox
    Friend WithEvents chkCalcTaxes As CheckBox
    Friend WithEvents btnCalcSelectColumns As Button
    Friend WithEvents gbCalcSizeLimit As GroupBox
    Friend WithEvents chkCalcXL As CheckBox
    Friend WithEvents chkCalcLarge As CheckBox
    Friend WithEvents chkCalcMedium As CheckBox
    Friend WithEvents chkCalcSmall As CheckBox
    Friend WithEvents gbCalcProdLines As GroupBox
    Friend WithEvents chkCalcAutoCalcT2NumBPs As CheckBox
    Friend WithEvents lblCalcBPs As Label
    Friend WithEvents txtCalcNumBPs As TextBox
    Friend WithEvents txtCalcRuns As TextBox
    Friend WithEvents txtCalcLabLines As TextBox
    Friend WithEvents lblCalcRuns As Label
    Friend WithEvents lblCalcLabLines1 As Label
    Friend WithEvents lblCalcProdLines1 As Label
    Friend WithEvents txtCalcProdLines As TextBox
    Friend WithEvents gbCalcCompareType As GroupBox
    Friend WithEvents chkCalcPPU As CheckBox
    Friend WithEvents rbtnCalcCompareBuildBuy As RadioButton
    Friend WithEvents rbtnCalcCompareRawMats As RadioButton
    Friend WithEvents rbtnCalcCompareComponents As RadioButton
    Friend WithEvents rbtnCalcCompareAll As RadioButton
    Friend WithEvents gbCalcTextColors As GroupBox
    Friend WithEvents lblCalcColorCode6 As Label
    Friend WithEvents lblCalcText As Label
    Friend WithEvents lblCalcColorCode3 As Label
    Friend WithEvents lblCalcColorCode4 As Label
    Friend WithEvents lblCalcColorCode5 As Label
    Friend WithEvents lblCalcColorCode2 As Label
    Friend WithEvents lblCalcColorCode1 As Label
    Friend WithEvents gbCalcInvention As GroupBox
    Friend WithEvents chkCalcDecryptorforT3 As CheckBox
    Friend WithEvents chkCalcDecryptorforT2 As CheckBox
    Friend WithEvents chkCalcDecryptor0 As CheckBox
    Friend WithEvents chkCalcDecryptor9 As CheckBox
    Friend WithEvents chkCalcDecryptor8 As CheckBox
    Friend WithEvents chkCalcDecryptor7 As CheckBox
    Friend WithEvents chkCalcDecryptor6 As CheckBox
    Friend WithEvents chkCalcDecryptor5 As CheckBox
    Friend WithEvents chkCalcDecryptor4 As CheckBox
    Friend WithEvents chkCalcDecryptor3 As CheckBox
    Friend WithEvents chkCalcDecryptor2 As CheckBox
    Friend WithEvents chkCalcDecryptor1 As CheckBox
    Friend WithEvents lblCalcDecryptorUse As Label
    Friend WithEvents gbCalcBPRace As GroupBox
    Friend WithEvents chkCalcRaceOther As CheckBox
    Friend WithEvents chkCalcRacePirate As CheckBox
    Friend WithEvents chkCalcRaceMinmatar As CheckBox
    Friend WithEvents chkCalcRaceGallente As CheckBox
    Friend WithEvents chkCalcRaceCaldari As CheckBox
    Friend WithEvents chkCalcRaceAmarr As CheckBox
    Friend WithEvents gbTempMEPE As GroupBox
    Friend WithEvents txtCalcTempTE As TextBox
    Friend WithEvents lblTempPE As Label
    Friend WithEvents txtCalcTempME As TextBox
    Friend WithEvents lblTempME As Label
    Friend WithEvents tabCalcFacilities As TabControl
    Friend WithEvents tabCalcFacilityBase As TabPage
    Friend WithEvents CalcBaseFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityComponents As TabPage
    Friend WithEvents CalcComponentsFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityCopy As TabPage
    Friend WithEvents CalcCopyFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityT2Invention As TabPage
    Friend WithEvents CalcInventionFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityT3Invention As TabPage
    Friend WithEvents CalcT3InventionFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilitySupers As TabPage
    Friend WithEvents CalcSupersFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityCapitals As TabPage
    Friend WithEvents CalcCapitalsFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityT3Ships As TabPage
    Friend WithEvents CalcT3ShipsFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilitySubsystems As TabPage
    Friend WithEvents CalcSubsystemsFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityBoosters As TabPage
    Friend WithEvents CalcBoostersFacility As ManufacturingFacility
    Friend WithEvents tabCalcFacilityReactions As TabPage
    Friend WithEvents CalcReactionsFacility As ManufacturingFacility
    Friend WithEvents gbCalcFilter As GroupBox
    Friend WithEvents cmbCalcBPTypeFilter As ComboBox
    Friend WithEvents gbCalcBPTech As GroupBox
    Friend WithEvents chkCalcPirateFaction As CheckBox
    Friend WithEvents chkCalcStoryline As CheckBox
    Friend WithEvents chkCalcNavyFaction As CheckBox
    Friend WithEvents chkCalcT3 As CheckBox
    Friend WithEvents chkCalcT2 As CheckBox
    Friend WithEvents chkCalcT1 As CheckBox
    Friend WithEvents gbCalcIncludeOwned As GroupBox
    Friend WithEvents chkCalcIncludeT3Owned As CheckBox
    Friend WithEvents chkCalcIncludeT2Owned As CheckBox
    Friend WithEvents btnCalcSaveSettings As Button
    Friend WithEvents btnCalcExportList As Button
    Friend WithEvents btnCalcPreview As Button
    Friend WithEvents btnCalcReset As Button
    Friend WithEvents gbCalcTextFilter As GroupBox
    Friend WithEvents btnCalcResetTextSearch As Button
    Friend WithEvents txtCalcItemFilter As TextBox
    Friend WithEvents gbCalcBPType As GroupBox
    Friend WithEvents chkCalcReactions As CheckBox
    Friend WithEvents chkCalcCelestials As CheckBox
    Friend WithEvents chkCalcMisc As CheckBox
    Friend WithEvents chkCalcSubsystems As CheckBox
    Friend WithEvents chkCalcDeployables As CheckBox
    Friend WithEvents chkCalcStructures As CheckBox
    Friend WithEvents chkCalcStructureRigs As CheckBox
    Friend WithEvents chkCalcBoosters As CheckBox
    Friend WithEvents chkCalcRigs As CheckBox
    Friend WithEvents chkCalcComponents As CheckBox
    Friend WithEvents chkCalcAmmo As CheckBox
    Friend WithEvents chkCalcDrones As CheckBox
    Friend WithEvents chkCalcModules As CheckBox
    Friend WithEvents chkCalcShips As CheckBox
    Friend WithEvents chkCalcStructureModules As CheckBox
    Friend WithEvents gbCalcBPSelect As GroupBox
    Friend WithEvents rbtnCalcBPFavorites As RadioButton
    Friend WithEvents rbtnCalcAllBPs As RadioButton
    Friend WithEvents rbtnCalcBPOwned As RadioButton
    Friend WithEvents gbCalcRelics As GroupBox
    Friend WithEvents chkCalcRERelic2 As CheckBox
    Friend WithEvents chkCalcRERelic3 As CheckBox
    Friend WithEvents chkCalcRERelic1 As CheckBox
    Friend WithEvents tabUpdatePrices As TabPage
    Friend WithEvents btnLoadPricesfromFile As Button
    Friend WithEvents btnSavePricestoFile As Button
    Friend WithEvents lstPricesView As MyListView
    Friend WithEvents txtPriceItemFilter As TextBox
    Friend WithEvents gbPriceOptions As GroupBox
    Friend WithEvents txtItemsPriceModifier As TextBox
    Friend WithEvents txtRawPriceModifier As TextBox
    Friend WithEvents gbPriceTypes As GroupBox
    Friend WithEvents rbtnPriceSettingPriceProfile As RadioButton
    Friend WithEvents rbtnPriceSettingSingleSelect As RadioButton
    Friend WithEvents lblItemsPriceModifier As Label
    Friend WithEvents lblRawPriceModifier As Label
    Friend WithEvents gbDataSource As GroupBox
    Friend WithEvents rbtnPriceSourceCCPData As RadioButton
    Friend WithEvents rbtnPriceSourceEM As RadioButton
    Friend WithEvents cmbItemsSplitPrices As ComboBox
    Friend WithEvents cmbRawMatsSplitPrices As ComboBox
    Friend WithEvents lblItemsSplitPrices As Label
    Friend WithEvents lblRawMatsSplitPrices As Label
    Friend WithEvents btnSaveUpdatePrices As Button
    Friend WithEvents btnCancelUpdate As Button
    Friend WithEvents btnClearItemFilter As Button
    Friend WithEvents btnToggleAllPriceItems As Button
    Friend WithEvents btnDownloadPrices As Button
    Friend WithEvents lblItemFilter As Label
    Friend WithEvents gbManufacturedItems As GroupBox
    Friend WithEvents chkPriceManufacturedPrices As CheckBox
    Friend WithEvents chkImplants As CheckBox
    Friend WithEvents chkUpdatePricesNoPrice As CheckBox
    Friend WithEvents chkFuelBlocks As CheckBox
    Friend WithEvents chkRAM As CheckBox
    Friend WithEvents gbComponents As GroupBox
    Friend WithEvents chkStructureComponents As CheckBox
    Friend WithEvents chkSubsystemComponents As CheckBox
    Friend WithEvents chkComponents As CheckBox
    Friend WithEvents chkCapitalShipComponents As CheckBox
    Friend WithEvents chkCapT2Components As CheckBox
    Friend WithEvents gbItems As GroupBox
    Friend WithEvents chkStructureModules As CheckBox
    Friend WithEvents chkCelestials As CheckBox
    Friend WithEvents chkDeployables As CheckBox
    Friend WithEvents cmbPriceChargeTypes As ComboBox
    Friend WithEvents chkStructures As CheckBox
    Friend WithEvents chkStructureRigs As CheckBox
    Friend WithEvents chkCharges As CheckBox
    Friend WithEvents chkBoosters As CheckBox
    Friend WithEvents cmbPriceShipTypes As ComboBox
    Friend WithEvents gbPricesTech As GroupBox
    Friend WithEvents chkPricesT4 As CheckBox
    Friend WithEvents chkPricesT6 As CheckBox
    Friend WithEvents chkPricesT5 As CheckBox
    Friend WithEvents chkPricesT3 As CheckBox
    Friend WithEvents chkPricesT2 As CheckBox
    Friend WithEvents chkPricesT1 As CheckBox
    Friend WithEvents chkSubsystems As CheckBox
    Friend WithEvents chkShips As CheckBox
    Friend WithEvents chkModules As CheckBox
    Friend WithEvents chkRigs As CheckBox
    Friend WithEvents chkDrones As CheckBox
    Friend WithEvents gbRawMaterials As GroupBox
    Friend WithEvents chkBPCs As CheckBox
    Friend WithEvents chkMisc As CheckBox
    Friend WithEvents chkRawMaterials As CheckBox
    Friend WithEvents chkPriceMaterialResearchEqPrices As CheckBox
    Friend WithEvents chkPlanetary As CheckBox
    Friend WithEvents chkBoosterMats As CheckBox
    Friend WithEvents chkFactionMaterials As CheckBox
    Friend WithEvents chkAdvancedMats As CheckBox
    Friend WithEvents chkProcessedMats As CheckBox
    Friend WithEvents chkRawMoonMats As CheckBox
    Friend WithEvents chkGas As CheckBox
    Friend WithEvents chkPolymers As CheckBox
    Friend WithEvents chkAncientRelics As CheckBox
    Friend WithEvents chkSalvage As CheckBox
    Friend WithEvents chkDecryptors As CheckBox
    Friend WithEvents chkDatacores As CheckBox
    Friend WithEvents chkIceProducts As CheckBox
    Friend WithEvents chkMinerals As CheckBox
    Friend WithEvents gbTradeHubSystems As GroupBox
    Friend WithEvents cmbPriceSystems As ComboBox
    Friend WithEvents chkSystems2 As CheckBox
    Friend WithEvents chkSystems4 As CheckBox
    Friend WithEvents chkSystems5 As CheckBox
    Friend WithEvents chkSystems3 As CheckBox
    Public WithEvents chkSystems1 As CheckBox
    Friend WithEvents btnViewSavedStructures As Button
    Friend WithEvents btnAddStructureIDs As Button
    Friend WithEvents tabBlueprints As TabPage
    Friend WithEvents rbtnBPRawT2MatType As RadioButton
    Private WithEvents rbtnBPProcT2MatType As RadioButton
    Friend WithEvents rbtnBPAdvT2MatType As RadioButton
    Friend WithEvents lblBPT2MatTypeSelector As Label
    Friend WithEvents lstBPList As ListBox
    Friend WithEvents gbBPBlueprintType As GroupBox
    Friend WithEvents chkBPNPCBPOs As CheckBox
    Friend WithEvents rbtnBPReactionsBlueprints As RadioButton
    Friend WithEvents rbtnBPStructureModulesBlueprints As RadioButton
    Friend WithEvents rbtnBPCelestialsBlueprints As RadioButton
    Friend WithEvents rbtnBPMiscBlueprints As RadioButton
    Friend WithEvents rbtnBPStructureBlueprints As RadioButton
    Friend WithEvents rbtnBPFavoriteBlueprints As RadioButton
    Friend WithEvents rbtnBPStructureRigsBlueprints As RadioButton
    Friend WithEvents rbtnBPOwnedBlueprints As RadioButton
    Friend WithEvents rbtnBPRigBlueprints As RadioButton
    Friend WithEvents rbtnBPBoosterBlueprints As RadioButton
    Friend WithEvents rbtnBPSubsystemBlueprints As RadioButton
    Friend WithEvents rbtnBPModuleBlueprints As RadioButton
    Friend WithEvents rbtnBPAmmoChargeBlueprints As RadioButton
    Friend WithEvents rbtnBPDroneBlueprints As RadioButton
    Friend WithEvents rbtnBPComponentBlueprints As RadioButton
    Friend WithEvents rbtnBPAllBlueprints As RadioButton
    Friend WithEvents rbtnBPShipBlueprints As RadioButton
    Friend WithEvents rbtnBPDeployableBlueprints As RadioButton
    Friend WithEvents gbBPBlueprintTech As GroupBox
    Friend WithEvents chkBPPirateFaction As CheckBox
    Friend WithEvents chkBPStoryline As CheckBox
    Friend WithEvents chkBPNavyFaction As CheckBox
    Friend WithEvents chkBPT3 As CheckBox
    Friend WithEvents chkBPT2 As CheckBox
    Friend WithEvents chkBPT1 As CheckBox
    Friend WithEvents gbFilters As GroupBox
    Friend WithEvents chkBPXL As CheckBox
    Friend WithEvents chkBPLarge As CheckBox
    Friend WithEvents chkBPMedium As CheckBox
    Friend WithEvents chkBPSmall As CheckBox
    Friend WithEvents cmbBPBlueprintSelection As ComboBox
    Friend WithEvents btnBPListView As Button
    Friend WithEvents btnBPForward As Button
    Friend WithEvents btnBPBack As Button
    Friend WithEvents lblBPSelectBlueprint As Label
    Friend WithEvents gbBPInventionStats As GroupBox
    Friend WithEvents txtBPMarketPriceEdit As TextBox
    Friend WithEvents lblBPProductionTime As Label
    Friend WithEvents lblBPTotalUnits As Label
    Friend WithEvents lblBPTaxes As Label
    Friend WithEvents lblBPTotalUnits1 As Label
    Friend WithEvents lblBPBrokerFees As Label
    Friend WithEvents lblBPPT As Label
    Friend WithEvents chkBPTaxes As CheckBox
    Friend WithEvents lblBPMarketCost As Label
    Friend WithEvents lblBPMarketCost1 As Label
    Friend WithEvents lblBPRawTotalCost As Label
    Friend WithEvents lblBPCompProfit As Label
    Friend WithEvents lblBPRawTotalCost1 As Label
    Friend WithEvents chkBPBrokerFees As CheckBox
    Friend WithEvents lblBPCompIPH As Label
    Friend WithEvents lblBPRawIPH As Label
    Friend WithEvents lblBPTotalCompCost1 As Label
    Friend WithEvents lblBPCompIPH1 As Label
    Friend WithEvents lblBPTotalItemPT As Label
    Friend WithEvents lblBPTotalCompCost As Label
    Friend WithEvents lblBPCPTPT As Label
    Friend WithEvents lblBPRawSVR As Label
    Friend WithEvents lblBPRawIPH1 As Label
    Friend WithEvents lblBPRawProfit As Label
    Friend WithEvents lblBPBPSVR As Label
    Friend WithEvents lblBPCompProfit1 As Label
    Friend WithEvents lblBPRawProfit1 As Label
    Friend WithEvents lblBPBPSVR1 As Label
    Friend WithEvents lblBPRawSVR1 As Label
    Friend WithEvents chkBPPricePerUnit As CheckBox
    Friend WithEvents lblBPBuyColor As Label
    Friend WithEvents lblBPBuildColor As Label
    Friend WithEvents gbBPMEPEImage As GroupBox
    Friend WithEvents gbBPSellExcess As GroupBox
    Friend WithEvents btnBPListMats As Button
    Friend WithEvents chkBPSellExcessItems As CheckBox
    Friend WithEvents btnBPSaveBP As Button
    Friend WithEvents tabBPInventionEquip As TabControl
    Friend WithEvents tabFacility As TabPage
    Friend WithEvents BPTabFacility As ManufacturingFacility
    Friend WithEvents tabT3Calcs As TabPage
    Friend WithEvents lblBPT3Decryptor As Label
    Friend WithEvents cmbBPT3Decryptor As ComboBox
    Friend WithEvents lblBPT3Stats As Label
    Friend WithEvents lblBPRelic As Label
    Friend WithEvents txtBPRelicLines As TextBox
    Friend WithEvents lblBPRelicLines As Label
    Friend WithEvents lblBPRETime As Label
    Friend WithEvents cmbBPRelic As ComboBox
    Friend WithEvents lblBPRECost As Label
    Friend WithEvents lblBPT3InventionChance As Label
    Friend WithEvents lblBPT3InventionChance1 As Label
    Friend WithEvents lblT3InventStatus As Label
    Friend WithEvents chkBPIncludeT3Time As CheckBox
    Friend WithEvents chkBPIncludeT3Costs As CheckBox
    Friend WithEvents tabInventionCalcs As TabPage
    Friend WithEvents lblBPCopyTime As Label
    Friend WithEvents lblBPT2InventStatus As Label
    Friend WithEvents lblBPCopyCosts As Label
    Friend WithEvents txtBPInventionLines As TextBox
    Friend WithEvents lblBPInventionLines As Label
    Friend WithEvents lblInventionChance1 As Label
    Friend WithEvents lblBPDecryptor As Label
    Friend WithEvents lblBPInventionTime As Label
    Friend WithEvents lblBPDecryptorStats As Label
    Friend WithEvents lblBPInventionCost As Label
    Friend WithEvents cmbBPInventionDecryptor As ComboBox
    Friend WithEvents lblBPInventionChance As Label
    Friend WithEvents chkBPIncludeInventionTime As CheckBox
    Friend WithEvents chkBPIncludeCopyTime As CheckBox
    Friend WithEvents chkBPIncludeCopyCosts As CheckBox
    Friend WithEvents chkBPIncludeInventionCosts As CheckBox
    Friend WithEvents btnBPSaveSettings As Button
    Friend WithEvents txtBPLines As TextBox
    Friend WithEvents pictBP As PictureBox
    Friend WithEvents gbBPManualSystemCostIndex As GroupBox
    Friend WithEvents btnBPUpdateCostIndex As Button
    Friend WithEvents lblBPSystemCostIndexManual As Label
    Friend WithEvents txtBPUpdateCostIndex As TextBox
    Friend WithEvents txtBPNumBPs As TextBox
    Friend WithEvents btnBPRefreshBP As Button
    Friend WithEvents lblBPLines As Label
    Friend WithEvents txtBPME As TextBox
    Friend WithEvents lblBPRuns As Label
    Friend WithEvents chkBPBuildBuy As CheckBox
    Friend WithEvents txtBPRuns As TextBox
    Friend WithEvents txtBPAddlCosts As TextBox
    Friend WithEvents lblBPAddlCosts As Label
    Friend WithEvents lblBPME As Label
    Friend WithEvents txtBPTE As TextBox
    Friend WithEvents lblBPPE As Label
    Friend WithEvents lblBPNumBPs As Label
    Friend WithEvents gbBPIgnoreinCalcs As GroupBox
    Friend WithEvents chkBPIgnoreMinerals As CheckBox
    Friend WithEvents chkBPIgnoreT1Item As CheckBox
    Friend WithEvents chkBPIgnoreInvention As CheckBox
    Friend WithEvents gbBPShopandCopy As GroupBox
    Friend WithEvents chkBPSimpleCopy As CheckBox
    Friend WithEvents rbtnBPCopyInvREMats As RadioButton
    Friend WithEvents rbtnBPComponentCopy As RadioButton
    Friend WithEvents rbtnBPRawmatCopy As RadioButton
    Friend WithEvents btnBPCopyMatstoClip As Button
    Friend WithEvents btnBPAddBPMatstoShoppingList As Button
    Friend WithEvents lblBPSimpleCopy As Label
    Friend WithEvents lblBPCanMakeBPAll As Label
    Friend WithEvents lblBPRawMatCost As Label
    Friend WithEvents lblBPRawMatCost1 As Label
    Friend WithEvents lblBPCanMakeBP As Label
    Friend WithEvents lblBPRawMats As Label
    Friend WithEvents lblBPComponentMatCost As Label
    Friend WithEvents lblBPComponentMats As Label
    Friend WithEvents lblBPComponentMatCost1 As Label
    Friend WithEvents lstBPRawMats As MyListView
    Friend WithEvents tabMain As TabControl
    Friend WithEvents lstBPComponentMats As MyListView
    Friend WithEvents lstBPBuiltComponents As MyListView
    Friend WithEvents btnBPBuiltComponents As Button
    Friend WithEvents btnBPComponents As Button
    Friend WithEvents FavoriteBlueprintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtBPBrokerFeeRate As TextBox
    Friend WithEvents txtCalcBrokerFeeRate As TextBox
    Friend WithEvents txtMineBrokerFeeRate As TextBox
    Friend WithEvents chkMineTriglavian As CheckBox
    Friend WithEvents mnuResetBuildBuyManualSelections As ToolStripMenuItem
    Friend WithEvents rbtnCalcAdvT2MatType As RadioButton
    Private WithEvents rbtnCalcProcT2MatType As RadioButton
    Friend WithEvents rbtnCalcRawT2MatType As RadioButton
    Friend WithEvents lblMineMiningDroneM3 As Label
    Friend WithEvents lblMineDroneSpecSkill As Label
    Friend WithEvents cmbMineDroneSpecSkill As ComboBox
    Friend WithEvents lblMineDroneOpSkill As Label
    Friend WithEvents cmbMineDroneOpSkill As ComboBox
    Friend WithEvents cmbMineDroneName As ComboBox
    Friend WithEvents lblMineDroneName As Label
    Friend WithEvents cmbMineDroneInterfacingSkill As ComboBox
    Friend WithEvents lblMineDroneInterfacingSkill As Label
    Friend WithEvents chkMineBoosterUseDrones As CheckBox
    Friend WithEvents lblMineNumMiningDrones As Label
    Friend WithEvents cmbMineNumMiningDrones As ComboBox
    Friend WithEvents lblMineMiningDroneYield As Label
    Friend WithEvents lblMineDroneIdealRange As Label
    Friend WithEvents chkMineBoosterDroneRig2 As CheckBox
    Friend WithEvents chkMineBoosterDroneRig1 As CheckBox
    Friend WithEvents tabMiningDrones As TabControl
    Friend WithEvents tabShipDrones As TabPage
    Friend WithEvents tabBoosterDrones As TabPage
    Friend WithEvents lblMineBoosterDroneIdealRange As Label
    Friend WithEvents cmbMineBoosterDroneName As ComboBox
    Friend WithEvents lblMineBoosterMiningDroneYield As Label
    Friend WithEvents cmbMineBoosterDroneOpSkill As ComboBox
    Friend WithEvents lblMineBoosterMiningDroneM3 As Label
    Friend WithEvents lblMineBoosterDroneOpSkill As Label
    Friend WithEvents lblMineBoosterNumMiningDrones As Label
    Friend WithEvents cmbMineBoosterDroneSpecSkill As ComboBox
    Friend WithEvents cmbMineBoosterNumMiningDrones As ComboBox
    Friend WithEvents lblMineBoosterDroneSpecSkill As Label
    Friend WithEvents lblMineBoosterDroneInterfacingSkill As Label
    Friend WithEvents lblMineBoosterDroneName As Label
    Friend WithEvents cmbMineBoosterDroneInterfacingSkill As ComboBox
    Friend WithEvents MineRefineFacility As ManufacturingFacility
    Friend WithEvents chkMineBoosterDroneRig3 As CheckBox
    Friend WithEvents cmbMineMiningRig3 As ComboBox
    Friend WithEvents cmbMineMiningRig2 As ComboBox
    Friend WithEvents cmbMineMiningRig1 As ComboBox
    Friend WithEvents tabPriceProfile As TabControl
    Friend WithEvents tabPriceProfileRaw As TabPage
    Friend WithEvents gbPPDefaultSettings As GroupBox
    Friend WithEvents btnPPUpdateDefaults As Button
    Friend WithEvents cmbPPDefaultsPriceType As ComboBox
    Friend WithEvents lblPPDefaultsSystem As Label
    Friend WithEvents lblPPDefaultsPriceType As Label
    Friend WithEvents cmbPPDefaultsSystem As ComboBox
    Friend WithEvents cmbPPDefaultsRegion As ComboBox
    Friend WithEvents lblPPDefaultsRegion As Label
    Friend WithEvents txtPPDefaultsPriceMod As TextBox
    Friend WithEvents lblPPDefaultsPriceMod As Label
    Friend WithEvents lstRawPriceProfile As MyListView
    Friend WithEvents tabPriceProfileManufactured As TabPage
    Friend WithEvents lstManufacturedPriceProfile As MyListView
    Friend WithEvents cmbPriceRegions As ComboBox
    Friend WithEvents gbSingleSource As GroupBox
    Friend WithEvents gbPriceProfile As GroupBox
    Friend WithEvents gbMarketStructures As GroupBox
    Friend WithEvents gbRegionSystemPrice As GroupBox
    Friend WithEvents btnOpenMarketBrowser As Button
    Friend WithEvents chkSystems6 As CheckBox
    Friend WithEvents chkMolecularForgedMaterials As CheckBox
    Friend WithEvents chkAdvancedProtectiveTechnology As CheckBox
    Friend WithEvents chkMolecularForgingTools As CheckBox
    Friend WithEvents chkNamedComponents As CheckBox
    Friend WithEvents chkRDb As CheckBox
    Friend WithEvents gbReactionMaterials As GroupBox
    Friend WithEvents gbResearchEquipment As GroupBox
    Friend WithEvents chkProtectiveComponents As CheckBox
    Friend WithEvents lblMineFacilityMoonOreRate As Label
    Friend WithEvents lblMineFacilityOreRate As Label
    Friend WithEvents lblMineFacilityMoonOreRate1 As Label
    Friend WithEvents lblMineFacilityOreRate1 As Label
    Friend WithEvents gbMineOreStuctureRates As GroupBox
    Friend WithEvents mnuOreFlips As ToolStripMenuItem
    Friend WithEvents mnuAnomalyOreBelts As ToolStripMenuItem
    Friend WithEvents mnuIceBelts As ToolStripMenuItem
    Friend WithEvents mnuResetSavedFacilities As ToolStripMenuItem
    Friend WithEvents gbReprocessables As GroupBox
    Friend WithEvents chkNoBuildItems As CheckBox
    Friend WithEvents mnuViewErrorLog As ToolStripMenuItem
    Friend WithEvents gbMineCrystalType As GroupBox
    Friend WithEvents chkMineTypeC As CheckBox
    Friend WithEvents chkMineTypeB As CheckBox
    Friend WithEvents chkMineTypeA As CheckBox
    Friend WithEvents chkMineT2Crystals As CheckBox
    Friend WithEvents chkMineT1Crystals As CheckBox
    Friend WithEvents tabMiningProcessingSkills As TabControl
    Friend WithEvents tabPageOres As TabPage
    Friend WithEvents chkOreProcessing1 As CheckBox
    Friend WithEvents cmbOreProcessing2 As ComboBox
    Friend WithEvents lblOreProcessing2 As Label
    Friend WithEvents chkOreProcessing3 As CheckBox
    Friend WithEvents chkOreProcessing2 As CheckBox
    Friend WithEvents chkOreProcessing6 As CheckBox
    Friend WithEvents cmbOreProcessing1 As ComboBox
    Friend WithEvents lblOreProcessing1 As Label
    Friend WithEvents lblOreProcessing6 As Label
    Friend WithEvents chkOreProcessing5 As CheckBox
    Friend WithEvents cmbOreProcessing6 As ComboBox
    Friend WithEvents lblOreProcessing3 As Label
    Friend WithEvents lblOreProcessing5 As Label
    Friend WithEvents cmbOreProcessing4 As ComboBox
    Friend WithEvents cmbOreProcessing3 As ComboBox
    Friend WithEvents chkOreProcessing4 As CheckBox
    Friend WithEvents lblOreProcessing4 As Label
    Friend WithEvents cmbOreProcessing5 As ComboBox
    Friend WithEvents tabPageMoonOres As TabPage
    Friend WithEvents lblOreProcessing7 As Label
    Friend WithEvents lblOreProcessing8 As Label
    Friend WithEvents lblOreProcessing10 As Label
    Friend WithEvents lblOreProcessing11 As Label
    Friend WithEvents cmbOreProcessing11 As ComboBox
    Friend WithEvents chkOreProcessing9 As CheckBox
    Friend WithEvents lblOreProcessing9 As Label
    Friend WithEvents chkOreProcessing8 As CheckBox
    Friend WithEvents cmbOreProcessing10 As ComboBox
    Friend WithEvents cmbOreProcessing7 As ComboBox
    Friend WithEvents chkOreProcessing10 As CheckBox
    Friend WithEvents chkOreProcessing7 As CheckBox
    Friend WithEvents cmbOreProcessing9 As ComboBox
    Friend WithEvents chkOreProcessing11 As CheckBox
    Friend WithEvents cmbOreProcessing8 As ComboBox
    Friend WithEvents tabPageIce As TabPage
    Friend WithEvents cmbOreProcessing12 As ComboBox
    Friend WithEvents lblOreProcessing12 As Label
    Friend WithEvents chkOreProcessing12 As CheckBox
    Friend WithEvents mnuMETECalculator As ToolStripMenuItem
    Friend WithEvents rbtnPriceSourceFW As RadioButton
    Friend WithEvents chkMineEDENCOM As CheckBox
    Friend WithEvents pbReactions As PictureBox
    Friend WithEvents chkBPOptimalT3Decryptor As CheckBox
    Friend WithEvents chkBPOptimalT2Decryptor As CheckBox
End Class
