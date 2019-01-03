<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUpwellStructureFitting
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
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup11 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("High Slots", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup12 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Mid Slots", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup13 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Low Slots", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup14 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Combat Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup15 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Reprocessing Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup16 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Engineering Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup17 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Reaction Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup18 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Drilling Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpwellStructureFitting))
        Me.FittingImages = New System.Windows.Forms.ImageList(Me.components)
        Me.ServiceModuleListView = New System.Windows.Forms.ListView()
        Me.lblSelectedUpwellStructure = New System.Windows.Forms.Label()
        Me.cmbUpwellStructureName = New System.Windows.Forms.ComboBox()
        Me.chkIncludeFuelCosts = New System.Windows.Forms.CheckBox()
        Me.btnStripFitting = New System.Windows.Forms.Button()
        Me.tabUpwellStructure = New System.Windows.Forms.TabControl()
        Me.tabFitting = New System.Windows.Forms.TabPage()
        Me.LowSlot3 = New System.Windows.Forms.PictureBox()
        Me.RigSlot2 = New System.Windows.Forms.PictureBox()
        Me.LowSlot6 = New System.Windows.Forms.PictureBox()
        Me.RigSlot3 = New System.Windows.Forms.PictureBox()
        Me.LowSlot2 = New System.Windows.Forms.PictureBox()
        Me.RigSlot1 = New System.Windows.Forms.PictureBox()
        Me.LowSlot7 = New System.Windows.Forms.PictureBox()
        Me.ServiceSlot1 = New System.Windows.Forms.PictureBox()
        Me.LowSlot8 = New System.Windows.Forms.PictureBox()
        Me.ServiceSlot2 = New System.Windows.Forms.PictureBox()
        Me.LowSlot4 = New System.Windows.Forms.PictureBox()
        Me.ServiceSlot3 = New System.Windows.Forms.PictureBox()
        Me.LowSlot5 = New System.Windows.Forms.PictureBox()
        Me.ServiceSlot4 = New System.Windows.Forms.PictureBox()
        Me.LowSlot1 = New System.Windows.Forms.PictureBox()
        Me.HighSlot8 = New System.Windows.Forms.PictureBox()
        Me.ServiceSlot5 = New System.Windows.Forms.PictureBox()
        Me.HighSlot5 = New System.Windows.Forms.PictureBox()
        Me.ServiceSlot6 = New System.Windows.Forms.PictureBox()
        Me.HighSlot6 = New System.Windows.Forms.PictureBox()
        Me.MidSlot3 = New System.Windows.Forms.PictureBox()
        Me.HighSlot4 = New System.Windows.Forms.PictureBox()
        Me.MidSlot4 = New System.Windows.Forms.PictureBox()
        Me.HighSlot2 = New System.Windows.Forms.PictureBox()
        Me.MidSlot2 = New System.Windows.Forms.PictureBox()
        Me.HighSlot3 = New System.Windows.Forms.PictureBox()
        Me.MidSlot1 = New System.Windows.Forms.PictureBox()
        Me.HighSlot7 = New System.Windows.Forms.PictureBox()
        Me.MidSlot5 = New System.Windows.Forms.PictureBox()
        Me.MidSlot8 = New System.Windows.Forms.PictureBox()
        Me.HighSlot1 = New System.Windows.Forms.PictureBox()
        Me.MidSlot6 = New System.Windows.Forms.PictureBox()
        Me.MidSlot7 = New System.Windows.Forms.PictureBox()
        Me.StructurePicture = New System.Windows.Forms.PictureBox()
        Me.tabFuelandBonuses = New System.Windows.Forms.TabPage()
        Me.gbFacilityBonuses = New System.Windows.Forms.GroupBox()
        Me.btnBonusPopout = New System.Windows.Forms.Button()
        Me.lstUpwellStructureBonuses = New System.Windows.Forms.ListView()
        Me.BonusAppliesTo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Activity = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Bonuses = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Source = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbFuelBlocks = New System.Windows.Forms.GroupBox()
        Me.btnRefreshPrices = New System.Windows.Forms.Button()
        Me.gbFuelPrices = New System.Windows.Forms.GroupBox()
        Me.lblHydrogenIsotopes = New System.Windows.Forms.Label()
        Me.lblHeavyWater = New System.Windows.Forms.Label()
        Me.lblStrontiumClathrates = New System.Windows.Forms.Label()
        Me.lblEnrichedUranium = New System.Windows.Forms.Label()
        Me.lblOxygen = New System.Windows.Forms.Label()
        Me.lblCoolant = New System.Windows.Forms.Label()
        Me.lblLiquidOzone = New System.Windows.Forms.Label()
        Me.lblMechanicalParts = New System.Windows.Forms.Label()
        Me.lblRobotics = New System.Windows.Forms.Label()
        Me.lblNitrogenIsotopes = New System.Windows.Forms.Label()
        Me.lblHeliumIsotopes = New System.Windows.Forms.Label()
        Me.lblOxygenIsotopes = New System.Windows.Forms.Label()
        Me.picHeliumIsotopes = New System.Windows.Forms.PictureBox()
        Me.txtMechanicalParts = New System.Windows.Forms.TextBox()
        Me.txtNitrogenIsotopes = New System.Windows.Forms.TextBox()
        Me.txtLiquidOzone = New System.Windows.Forms.TextBox()
        Me.txtCoolant = New System.Windows.Forms.TextBox()
        Me.txtOxygen = New System.Windows.Forms.TextBox()
        Me.txtEnrichedUranium = New System.Windows.Forms.TextBox()
        Me.txtStrontiumClathrates = New System.Windows.Forms.TextBox()
        Me.picCoolant = New System.Windows.Forms.PictureBox()
        Me.picOxygen = New System.Windows.Forms.PictureBox()
        Me.txtHeavyWater = New System.Windows.Forms.TextBox()
        Me.picNitrogenIsotopes = New System.Windows.Forms.PictureBox()
        Me.txtRobotics = New System.Windows.Forms.TextBox()
        Me.picHydrogenIsotopes = New System.Windows.Forms.PictureBox()
        Me.picEnrichedUranium = New System.Windows.Forms.PictureBox()
        Me.picOxygenIsotopes = New System.Windows.Forms.PictureBox()
        Me.picMechanicalParts = New System.Windows.Forms.PictureBox()
        Me.picRobotics = New System.Windows.Forms.PictureBox()
        Me.picStrontiumClathrates = New System.Windows.Forms.PictureBox()
        Me.txtOxygenIsotopes = New System.Windows.Forms.TextBox()
        Me.picLiquidOzone = New System.Windows.Forms.PictureBox()
        Me.picHeavyWater = New System.Windows.Forms.PictureBox()
        Me.txtHydrogenIsotopes = New System.Windows.Forms.TextBox()
        Me.txtHeliumIsotopes = New System.Windows.Forms.TextBox()
        Me.btnSaveFuelBlockInfo = New System.Windows.Forms.Button()
        Me.gbFuelBlocks2 = New System.Windows.Forms.GroupBox()
        Me.picHeliumFuelBlock = New System.Windows.Forms.PictureBox()
        Me.lblHeliumFuelBlock = New System.Windows.Forms.Label()
        Me.lblHeliumFuelBlockBPME = New System.Windows.Forms.Label()
        Me.txtHeliumFuelBlockBPME = New System.Windows.Forms.TextBox()
        Me.lblHeliumFuelBlockBuy = New System.Windows.Forms.Label()
        Me.lblHeliumFuelBlockBuild = New System.Windows.Forms.Label()
        Me.txtHeliumFuelBlockBuyPrice = New System.Windows.Forms.TextBox()
        Me.lblHeliumFuelBlockBuildPrice = New System.Windows.Forms.Label()
        Me.lblHydrogenFuelBlock = New System.Windows.Forms.Label()
        Me.lblHydrogenFuelBlockBPME = New System.Windows.Forms.Label()
        Me.picHydrogenFuelBlock = New System.Windows.Forms.PictureBox()
        Me.txtHydrogenFuelBlockBPME = New System.Windows.Forms.TextBox()
        Me.lblHydrogenBlockBuy = New System.Windows.Forms.Label()
        Me.lblHydrogenFuelBlockBuild = New System.Windows.Forms.Label()
        Me.txtHydrogenFuelBlockBuyPrice = New System.Windows.Forms.TextBox()
        Me.lblHydrogenFuelBlockBuildPrice = New System.Windows.Forms.Label()
        Me.lblNitrogenFuelBlock = New System.Windows.Forms.Label()
        Me.lblNitrogenFuelBlockBPME = New System.Windows.Forms.Label()
        Me.picNitrogenFuelBlock = New System.Windows.Forms.PictureBox()
        Me.txtNitrogenFuelBlockBPME = New System.Windows.Forms.TextBox()
        Me.lblNitrogenFuelBlockBuy = New System.Windows.Forms.Label()
        Me.lblNitrogenFuelBlockBuild = New System.Windows.Forms.Label()
        Me.txtNitrogenFuelBlockBuyPrice = New System.Windows.Forms.TextBox()
        Me.lblNitrogenFuelBlockBuildPrice = New System.Windows.Forms.Label()
        Me.lblOxygenFuelBlock = New System.Windows.Forms.Label()
        Me.lblOxygenFuelBlockBPME = New System.Windows.Forms.Label()
        Me.picOxygenFuelBlock = New System.Windows.Forms.PictureBox()
        Me.txtOxygenFuelBlockBPME = New System.Windows.Forms.TextBox()
        Me.lblOxygenFuelBlockBuy = New System.Windows.Forms.Label()
        Me.lblOxygenFuelBlockBuild = New System.Windows.Forms.Label()
        Me.txtOxygenFuelBlockBuyPrice = New System.Windows.Forms.TextBox()
        Me.lblOxygenFuelBlockBuildPrice = New System.Windows.Forms.Label()
        Me.gbFuelBlockOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnBuyBlocks = New System.Windows.Forms.RadioButton()
        Me.rbtnBuildBlocks = New System.Windows.Forms.RadioButton()
        Me.chkAutoUpdateFuelPrice = New System.Windows.Forms.CheckBox()
        Me.btnSavePrices = New System.Windows.Forms.Button()
        Me.btnUpdateBuildCost = New System.Windows.Forms.Button()
        Me.lblLauncherSlots = New System.Windows.Forms.Label()
        Me.gbFilterOptions = New System.Windows.Forms.GroupBox()
        Me.chkRigTypeViewDrilling = New System.Windows.Forms.CheckBox()
        Me.chkRigTypeViewReaction = New System.Windows.Forms.CheckBox()
        Me.chkRigTypeViewCombat = New System.Windows.Forms.CheckBox()
        Me.chkRigTypeViewReprocessing = New System.Windows.Forms.CheckBox()
        Me.chkRigTypeViewEngineering = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeLow = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeMedium = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeHigh = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeServices = New System.Windows.Forms.CheckBox()
        Me.gbStatsandOptions = New System.Windows.Forms.GroupBox()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.lblSystemSecurity = New System.Windows.Forms.Label()
        Me.chkNullSec = New System.Windows.Forms.CheckBox()
        Me.chkLowSec = New System.Windows.Forms.CheckBox()
        Me.chkHighSec = New System.Windows.Forms.CheckBox()
        Me.gbIncludeFuelBlocks = New System.Windows.Forms.GroupBox()
        Me.rbtnOxygenFuelBlock = New System.Windows.Forms.RadioButton()
        Me.rbtnNitrogenFuelBlock = New System.Windows.Forms.RadioButton()
        Me.rbtnHydrogenFuelBlock = New System.Windows.Forms.RadioButton()
        Me.rbtnHeliumFuelBlock = New System.Windows.Forms.RadioButton()
        Me.lblServiceModuleOnlineAmt = New System.Windows.Forms.Label()
        Me.lblOnlineAmt = New System.Windows.Forms.Label()
        Me.lblFuelBPH = New System.Windows.Forms.Label()
        Me.lblServiceModuleBPH = New System.Windows.Forms.Label()
        Me.lblServiceModuleFCPH = New System.Windows.Forms.Label()
        Me.lblFuelCost = New System.Windows.Forms.Label()
        Me.lblCapacitor = New System.Windows.Forms.Label()
        Me.lblCapacitor1 = New System.Windows.Forms.Label()
        Me.lblCalibration = New System.Windows.Forms.Label()
        Me.lblCalibration1 = New System.Windows.Forms.Label()
        Me.lblCPU = New System.Windows.Forms.Label()
        Me.lblPowerGrid = New System.Windows.Forms.Label()
        Me.lblCPU1 = New System.Windows.Forms.Label()
        Me.lblPowerGrid1 = New System.Windows.Forms.Label()
        Me.btnSaveFitting = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnCloseForm = New System.Windows.Forms.Button()
        Me.gbTextFilter = New System.Windows.Forms.GroupBox()
        Me.btnItemFilter = New System.Windows.Forms.Button()
        Me.btnResetItemFilter = New System.Windows.Forms.Button()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
        Me.EventLog1 = New System.Diagnostics.EventLog()
        Me.pbFloat = New System.Windows.Forms.PictureBox()
        Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabUpwellStructure.SuspendLayout()
        Me.tabFitting.SuspendLayout()
        CType(Me.LowSlot3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RigSlot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RigSlot3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RigSlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceSlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceSlot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceSlot3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceSlot4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LowSlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceSlot5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceSlot6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HighSlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MidSlot7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StructurePicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabFuelandBonuses.SuspendLayout()
        Me.gbFacilityBonuses.SuspendLayout()
        Me.gbFuelBlocks.SuspendLayout()
        Me.gbFuelPrices.SuspendLayout()
        CType(Me.picHeliumIsotopes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCoolant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picOxygen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNitrogenIsotopes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHydrogenIsotopes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEnrichedUranium, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picOxygenIsotopes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMechanicalParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRobotics, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStrontiumClathrates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLiquidOzone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHeavyWater, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFuelBlocks2.SuspendLayout()
        CType(Me.picHeliumFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHydrogenFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNitrogenFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picOxygenFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFuelBlockOptions.SuspendLayout()
        Me.gbFilterOptions.SuspendLayout()
        Me.gbStatsandOptions.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.gbIncludeFuelBlocks.SuspendLayout()
        Me.gbTextFilter.SuspendLayout()
        CType(Me.EventLog1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFloat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FittingImages
        '
        Me.FittingImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.FittingImages.ImageSize = New System.Drawing.Size(64, 64)
        Me.FittingImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'ServiceModuleListView
        '
        Me.ServiceModuleListView.Activation = System.Windows.Forms.ItemActivation.TwoClick
        Me.ServiceModuleListView.AllowDrop = True
        Me.ServiceModuleListView.AutoArrange = False
        ListViewGroup10.Header = "Services"
        ListViewGroup10.Name = "ServiceSlots"
        ListViewGroup10.Tag = "ServiceSlot"
        ListViewGroup11.Header = "High Slots"
        ListViewGroup11.Name = "HighSlots"
        ListViewGroup11.Tag = "HighSlot"
        ListViewGroup12.Header = "Mid Slots"
        ListViewGroup12.Name = "MidSlots"
        ListViewGroup12.Tag = "MidSlot"
        ListViewGroup13.Header = "Low Slots"
        ListViewGroup13.Name = "LowSlots"
        ListViewGroup13.Tag = "LowSlot"
        ListViewGroup14.Header = "Combat Rigs"
        ListViewGroup14.Name = "CombatRigs"
        ListViewGroup14.Tag = "RigSlot"
        ListViewGroup15.Header = "Reprocessing Rigs"
        ListViewGroup15.Name = "ReprocessingRigs"
        ListViewGroup15.Tag = "RigSlot"
        ListViewGroup16.Header = "Engineering Rigs"
        ListViewGroup16.Name = "EngineeringRigs"
        ListViewGroup16.Tag = "RigSlot"
        ListViewGroup17.Header = "Reaction Rigs"
        ListViewGroup17.Name = "ReactionRigs"
        ListViewGroup17.Tag = "RigSlot"
        ListViewGroup18.Header = "Drilling Rigs"
        ListViewGroup18.Name = "DrillingRigs"
        ListViewGroup18.Tag = "RigSlot"
        Me.ServiceModuleListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup10, ListViewGroup11, ListViewGroup12, ListViewGroup13, ListViewGroup14, ListViewGroup15, ListViewGroup16, ListViewGroup17, ListViewGroup18})
        Me.ServiceModuleListView.HoverSelection = True
        Me.ServiceModuleListView.LargeImageList = Me.FittingImages
        Me.ServiceModuleListView.Location = New System.Drawing.Point(6, 210)
        Me.ServiceModuleListView.MultiSelect = False
        Me.ServiceModuleListView.Name = "ServiceModuleListView"
        Me.ServiceModuleListView.Size = New System.Drawing.Size(342, 397)
        Me.ServiceModuleListView.TabIndex = 8
        Me.ServiceModuleListView.UseCompatibleStateImageBehavior = False
        '
        'lblSelectedUpwellStructure
        '
        Me.lblSelectedUpwellStructure.AutoSize = True
        Me.lblSelectedUpwellStructure.Location = New System.Drawing.Point(6, 12)
        Me.lblSelectedUpwellStructure.Name = "lblSelectedUpwellStructure"
        Me.lblSelectedUpwellStructure.Size = New System.Drawing.Size(133, 13)
        Me.lblSelectedUpwellStructure.TabIndex = 0
        Me.lblSelectedUpwellStructure.Text = "Selected Upwell Structure:"
        '
        'cmbUpwellStructureName
        '
        Me.cmbUpwellStructureName.FormattingEnabled = True
        Me.cmbUpwellStructureName.Location = New System.Drawing.Point(138, 8)
        Me.cmbUpwellStructureName.Name = "cmbUpwellStructureName"
        Me.cmbUpwellStructureName.Size = New System.Drawing.Size(210, 21)
        Me.cmbUpwellStructureName.TabIndex = 1
        '
        'chkIncludeFuelCosts
        '
        Me.chkIncludeFuelCosts.AutoSize = True
        Me.chkIncludeFuelCosts.Location = New System.Drawing.Point(6, 193)
        Me.chkIncludeFuelCosts.Name = "chkIncludeFuelCosts"
        Me.chkIncludeFuelCosts.Size = New System.Drawing.Size(113, 17)
        Me.chkIncludeFuelCosts.TabIndex = 9
        Me.chkIncludeFuelCosts.Text = "Include Fuel Costs"
        Me.chkIncludeFuelCosts.UseVisualStyleBackColor = True
        '
        'btnStripFitting
        '
        Me.btnStripFitting.Location = New System.Drawing.Point(6, 35)
        Me.btnStripFitting.Name = "btnStripFitting"
        Me.btnStripFitting.Size = New System.Drawing.Size(81, 30)
        Me.btnStripFitting.TabIndex = 2
        Me.btnStripFitting.Text = "Strip Fitting"
        Me.btnStripFitting.UseVisualStyleBackColor = True
        '
        'tabUpwellStructure
        '
        Me.tabUpwellStructure.Controls.Add(Me.tabFitting)
        Me.tabUpwellStructure.Controls.Add(Me.tabFuelandBonuses)
        Me.tabUpwellStructure.Location = New System.Drawing.Point(354, 4)
        Me.tabUpwellStructure.Name = "tabUpwellStructure"
        Me.tabUpwellStructure.SelectedIndex = 0
        Me.tabUpwellStructure.Size = New System.Drawing.Size(603, 603)
        Me.tabUpwellStructure.TabIndex = 9
        '
        'tabFitting
        '
        Me.tabFitting.Controls.Add(Me.LowSlot3)
        Me.tabFitting.Controls.Add(Me.RigSlot2)
        Me.tabFitting.Controls.Add(Me.LowSlot6)
        Me.tabFitting.Controls.Add(Me.RigSlot3)
        Me.tabFitting.Controls.Add(Me.LowSlot2)
        Me.tabFitting.Controls.Add(Me.RigSlot1)
        Me.tabFitting.Controls.Add(Me.LowSlot7)
        Me.tabFitting.Controls.Add(Me.ServiceSlot1)
        Me.tabFitting.Controls.Add(Me.LowSlot8)
        Me.tabFitting.Controls.Add(Me.ServiceSlot2)
        Me.tabFitting.Controls.Add(Me.LowSlot4)
        Me.tabFitting.Controls.Add(Me.ServiceSlot3)
        Me.tabFitting.Controls.Add(Me.LowSlot5)
        Me.tabFitting.Controls.Add(Me.ServiceSlot4)
        Me.tabFitting.Controls.Add(Me.LowSlot1)
        Me.tabFitting.Controls.Add(Me.HighSlot8)
        Me.tabFitting.Controls.Add(Me.ServiceSlot5)
        Me.tabFitting.Controls.Add(Me.HighSlot5)
        Me.tabFitting.Controls.Add(Me.ServiceSlot6)
        Me.tabFitting.Controls.Add(Me.HighSlot6)
        Me.tabFitting.Controls.Add(Me.MidSlot3)
        Me.tabFitting.Controls.Add(Me.HighSlot4)
        Me.tabFitting.Controls.Add(Me.MidSlot4)
        Me.tabFitting.Controls.Add(Me.HighSlot2)
        Me.tabFitting.Controls.Add(Me.MidSlot2)
        Me.tabFitting.Controls.Add(Me.HighSlot3)
        Me.tabFitting.Controls.Add(Me.MidSlot1)
        Me.tabFitting.Controls.Add(Me.HighSlot7)
        Me.tabFitting.Controls.Add(Me.MidSlot5)
        Me.tabFitting.Controls.Add(Me.MidSlot8)
        Me.tabFitting.Controls.Add(Me.HighSlot1)
        Me.tabFitting.Controls.Add(Me.MidSlot6)
        Me.tabFitting.Controls.Add(Me.MidSlot7)
        Me.tabFitting.Controls.Add(Me.StructurePicture)
        Me.tabFitting.Location = New System.Drawing.Point(4, 22)
        Me.tabFitting.Name = "tabFitting"
        Me.tabFitting.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFitting.Size = New System.Drawing.Size(595, 577)
        Me.tabFitting.TabIndex = 0
        Me.tabFitting.Text = "Fitting"
        Me.tabFitting.UseVisualStyleBackColor = True
        '
        'LowSlot3
        '
        Me.LowSlot3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot3.BackgroundImage = CType(resources.GetObject("LowSlot3.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot3.Location = New System.Drawing.Point(518, 151)
        Me.LowSlot3.Name = "LowSlot3"
        Me.LowSlot3.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot3.TabIndex = 34
        Me.LowSlot3.TabStop = False
        '
        'RigSlot2
        '
        Me.RigSlot2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.RigSlot2.BackgroundImage = CType(resources.GetObject("RigSlot2.BackgroundImage"), System.Drawing.Image)
        Me.RigSlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RigSlot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RigSlot2.Location = New System.Drawing.Point(267, 431)
        Me.RigSlot2.Name = "RigSlot2"
        Me.RigSlot2.Size = New System.Drawing.Size(64, 64)
        Me.RigSlot2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.RigSlot2.TabIndex = 26
        Me.RigSlot2.TabStop = False
        '
        'LowSlot6
        '
        Me.LowSlot6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot6.BackgroundImage = CType(resources.GetObject("LowSlot6.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot6.Location = New System.Drawing.Point(518, 361)
        Me.LowSlot6.Name = "LowSlot6"
        Me.LowSlot6.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot6.TabIndex = 33
        Me.LowSlot6.TabStop = False
        '
        'RigSlot3
        '
        Me.RigSlot3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.RigSlot3.BackgroundImage = CType(resources.GetObject("RigSlot3.BackgroundImage"), System.Drawing.Image)
        Me.RigSlot3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RigSlot3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RigSlot3.Location = New System.Drawing.Point(337, 431)
        Me.RigSlot3.Name = "RigSlot3"
        Me.RigSlot3.Size = New System.Drawing.Size(64, 64)
        Me.RigSlot3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.RigSlot3.TabIndex = 25
        Me.RigSlot3.TabStop = False
        '
        'LowSlot2
        '
        Me.LowSlot2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot2.BackgroundImage = CType(resources.GetObject("LowSlot2.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot2.Location = New System.Drawing.Point(518, 81)
        Me.LowSlot2.Name = "LowSlot2"
        Me.LowSlot2.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot2.TabIndex = 32
        Me.LowSlot2.TabStop = False
        '
        'RigSlot1
        '
        Me.RigSlot1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.RigSlot1.BackgroundImage = CType(resources.GetObject("RigSlot1.BackgroundImage"), System.Drawing.Image)
        Me.RigSlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RigSlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RigSlot1.Location = New System.Drawing.Point(197, 431)
        Me.RigSlot1.Name = "RigSlot1"
        Me.RigSlot1.Size = New System.Drawing.Size(64, 64)
        Me.RigSlot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.RigSlot1.TabIndex = 6
        Me.RigSlot1.TabStop = False
        '
        'LowSlot7
        '
        Me.LowSlot7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot7.BackgroundImage = CType(resources.GetObject("LowSlot7.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot7.Location = New System.Drawing.Point(518, 431)
        Me.LowSlot7.Name = "LowSlot7"
        Me.LowSlot7.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot7.TabIndex = 31
        Me.LowSlot7.TabStop = False
        '
        'ServiceSlot1
        '
        Me.ServiceSlot1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ServiceSlot1.BackgroundImage = CType(resources.GetObject("ServiceSlot1.BackgroundImage"), System.Drawing.Image)
        Me.ServiceSlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ServiceSlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServiceSlot1.Location = New System.Drawing.Point(232, 501)
        Me.ServiceSlot1.Name = "ServiceSlot1"
        Me.ServiceSlot1.Size = New System.Drawing.Size(64, 64)
        Me.ServiceSlot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServiceSlot1.TabIndex = 12
        Me.ServiceSlot1.TabStop = False
        '
        'LowSlot8
        '
        Me.LowSlot8.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot8.BackgroundImage = CType(resources.GetObject("LowSlot8.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot8.Location = New System.Drawing.Point(518, 501)
        Me.LowSlot8.Name = "LowSlot8"
        Me.LowSlot8.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot8.TabIndex = 30
        Me.LowSlot8.TabStop = False
        '
        'ServiceSlot2
        '
        Me.ServiceSlot2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ServiceSlot2.BackgroundImage = CType(resources.GetObject("ServiceSlot2.BackgroundImage"), System.Drawing.Image)
        Me.ServiceSlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ServiceSlot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServiceSlot2.Location = New System.Drawing.Point(302, 501)
        Me.ServiceSlot2.Name = "ServiceSlot2"
        Me.ServiceSlot2.Size = New System.Drawing.Size(64, 64)
        Me.ServiceSlot2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServiceSlot2.TabIndex = 13
        Me.ServiceSlot2.TabStop = False
        '
        'LowSlot4
        '
        Me.LowSlot4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot4.BackgroundImage = CType(resources.GetObject("LowSlot4.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot4.Location = New System.Drawing.Point(518, 221)
        Me.LowSlot4.Name = "LowSlot4"
        Me.LowSlot4.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot4.TabIndex = 29
        Me.LowSlot4.TabStop = False
        '
        'ServiceSlot3
        '
        Me.ServiceSlot3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ServiceSlot3.BackgroundImage = CType(resources.GetObject("ServiceSlot3.BackgroundImage"), System.Drawing.Image)
        Me.ServiceSlot3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ServiceSlot3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServiceSlot3.Location = New System.Drawing.Point(162, 501)
        Me.ServiceSlot3.Name = "ServiceSlot3"
        Me.ServiceSlot3.Size = New System.Drawing.Size(64, 64)
        Me.ServiceSlot3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServiceSlot3.TabIndex = 14
        Me.ServiceSlot3.TabStop = False
        '
        'LowSlot5
        '
        Me.LowSlot5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot5.BackgroundImage = CType(resources.GetObject("LowSlot5.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot5.Location = New System.Drawing.Point(518, 291)
        Me.LowSlot5.Name = "LowSlot5"
        Me.LowSlot5.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot5.TabIndex = 27
        Me.LowSlot5.TabStop = False
        '
        'ServiceSlot4
        '
        Me.ServiceSlot4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ServiceSlot4.BackgroundImage = CType(resources.GetObject("ServiceSlot4.BackgroundImage"), System.Drawing.Image)
        Me.ServiceSlot4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ServiceSlot4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServiceSlot4.Location = New System.Drawing.Point(372, 501)
        Me.ServiceSlot4.Name = "ServiceSlot4"
        Me.ServiceSlot4.Size = New System.Drawing.Size(64, 64)
        Me.ServiceSlot4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServiceSlot4.TabIndex = 15
        Me.ServiceSlot4.TabStop = False
        '
        'LowSlot1
        '
        Me.LowSlot1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LowSlot1.BackgroundImage = CType(resources.GetObject("LowSlot1.BackgroundImage"), System.Drawing.Image)
        Me.LowSlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.LowSlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowSlot1.Location = New System.Drawing.Point(518, 11)
        Me.LowSlot1.Name = "LowSlot1"
        Me.LowSlot1.Size = New System.Drawing.Size(64, 64)
        Me.LowSlot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LowSlot1.TabIndex = 11
        Me.LowSlot1.TabStop = False
        '
        'HighSlot8
        '
        Me.HighSlot8.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot8.BackgroundImage = CType(resources.GetObject("HighSlot8.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot8.InitialImage = CType(resources.GetObject("HighSlot8.InitialImage"), System.Drawing.Image)
        Me.HighSlot8.Location = New System.Drawing.Point(442, 81)
        Me.HighSlot8.Name = "HighSlot8"
        Me.HighSlot8.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot8.TabIndex = 24
        Me.HighSlot8.TabStop = False
        '
        'ServiceSlot5
        '
        Me.ServiceSlot5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ServiceSlot5.BackgroundImage = CType(resources.GetObject("ServiceSlot5.BackgroundImage"), System.Drawing.Image)
        Me.ServiceSlot5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ServiceSlot5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServiceSlot5.Location = New System.Drawing.Point(92, 501)
        Me.ServiceSlot5.Name = "ServiceSlot5"
        Me.ServiceSlot5.Size = New System.Drawing.Size(64, 64)
        Me.ServiceSlot5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServiceSlot5.TabIndex = 16
        Me.ServiceSlot5.TabStop = False
        '
        'HighSlot5
        '
        Me.HighSlot5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot5.BackgroundImage = CType(resources.GetObject("HighSlot5.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot5.InitialImage = CType(resources.GetObject("HighSlot5.InitialImage"), System.Drawing.Image)
        Me.HighSlot5.Location = New System.Drawing.Point(92, 11)
        Me.HighSlot5.Name = "HighSlot5"
        Me.HighSlot5.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot5.TabIndex = 23
        Me.HighSlot5.TabStop = False
        '
        'ServiceSlot6
        '
        Me.ServiceSlot6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ServiceSlot6.BackgroundImage = CType(resources.GetObject("ServiceSlot6.BackgroundImage"), System.Drawing.Image)
        Me.ServiceSlot6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ServiceSlot6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServiceSlot6.Location = New System.Drawing.Point(442, 501)
        Me.ServiceSlot6.Name = "ServiceSlot6"
        Me.ServiceSlot6.Size = New System.Drawing.Size(64, 64)
        Me.ServiceSlot6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServiceSlot6.TabIndex = 17
        Me.ServiceSlot6.TabStop = False
        '
        'HighSlot6
        '
        Me.HighSlot6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot6.BackgroundImage = CType(resources.GetObject("HighSlot6.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot6.InitialImage = CType(resources.GetObject("HighSlot6.InitialImage"), System.Drawing.Image)
        Me.HighSlot6.Location = New System.Drawing.Point(442, 11)
        Me.HighSlot6.Name = "HighSlot6"
        Me.HighSlot6.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot6.TabIndex = 22
        Me.HighSlot6.TabStop = False
        '
        'MidSlot3
        '
        Me.MidSlot3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot3.BackgroundImage = CType(resources.GetObject("MidSlot3.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot3.Location = New System.Drawing.Point(15, 151)
        Me.MidSlot3.Name = "MidSlot3"
        Me.MidSlot3.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot3.TabIndex = 41
        Me.MidSlot3.TabStop = False
        '
        'HighSlot4
        '
        Me.HighSlot4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot4.BackgroundImage = CType(resources.GetObject("HighSlot4.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot4.InitialImage = CType(resources.GetObject("HighSlot4.InitialImage"), System.Drawing.Image)
        Me.HighSlot4.Location = New System.Drawing.Point(372, 11)
        Me.HighSlot4.Name = "HighSlot4"
        Me.HighSlot4.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot4.TabIndex = 21
        Me.HighSlot4.TabStop = False
        '
        'MidSlot4
        '
        Me.MidSlot4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot4.BackgroundImage = CType(resources.GetObject("MidSlot4.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot4.Location = New System.Drawing.Point(15, 221)
        Me.MidSlot4.Name = "MidSlot4"
        Me.MidSlot4.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot4.TabIndex = 40
        Me.MidSlot4.TabStop = False
        '
        'HighSlot2
        '
        Me.HighSlot2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot2.BackgroundImage = CType(resources.GetObject("HighSlot2.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot2.InitialImage = CType(resources.GetObject("HighSlot2.InitialImage"), System.Drawing.Image)
        Me.HighSlot2.Location = New System.Drawing.Point(302, 11)
        Me.HighSlot2.Name = "HighSlot2"
        Me.HighSlot2.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot2.TabIndex = 20
        Me.HighSlot2.TabStop = False
        '
        'MidSlot2
        '
        Me.MidSlot2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot2.BackgroundImage = CType(resources.GetObject("MidSlot2.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot2.Location = New System.Drawing.Point(15, 81)
        Me.MidSlot2.Name = "MidSlot2"
        Me.MidSlot2.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot2.TabIndex = 39
        Me.MidSlot2.TabStop = False
        '
        'HighSlot3
        '
        Me.HighSlot3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot3.BackgroundImage = CType(resources.GetObject("HighSlot3.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot3.InitialImage = CType(resources.GetObject("HighSlot3.InitialImage"), System.Drawing.Image)
        Me.HighSlot3.Location = New System.Drawing.Point(162, 11)
        Me.HighSlot3.Name = "HighSlot3"
        Me.HighSlot3.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot3.TabIndex = 19
        Me.HighSlot3.TabStop = False
        '
        'MidSlot1
        '
        Me.MidSlot1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot1.BackgroundImage = CType(resources.GetObject("MidSlot1.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot1.Location = New System.Drawing.Point(15, 11)
        Me.MidSlot1.Name = "MidSlot1"
        Me.MidSlot1.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot1.TabIndex = 10
        Me.MidSlot1.TabStop = False
        '
        'HighSlot7
        '
        Me.HighSlot7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot7.BackgroundImage = CType(resources.GetObject("HighSlot7.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot7.InitialImage = CType(resources.GetObject("HighSlot7.InitialImage"), System.Drawing.Image)
        Me.HighSlot7.Location = New System.Drawing.Point(92, 81)
        Me.HighSlot7.Name = "HighSlot7"
        Me.HighSlot7.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot7.TabIndex = 18
        Me.HighSlot7.TabStop = False
        '
        'MidSlot5
        '
        Me.MidSlot5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot5.BackgroundImage = CType(resources.GetObject("MidSlot5.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot5.Location = New System.Drawing.Point(15, 291)
        Me.MidSlot5.Name = "MidSlot5"
        Me.MidSlot5.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot5.TabIndex = 38
        Me.MidSlot5.TabStop = False
        '
        'MidSlot8
        '
        Me.MidSlot8.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot8.BackgroundImage = CType(resources.GetObject("MidSlot8.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot8.Location = New System.Drawing.Point(15, 501)
        Me.MidSlot8.Name = "MidSlot8"
        Me.MidSlot8.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot8.TabIndex = 35
        Me.MidSlot8.TabStop = False
        '
        'HighSlot1
        '
        Me.HighSlot1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HighSlot1.BackgroundImage = CType(resources.GetObject("HighSlot1.BackgroundImage"), System.Drawing.Image)
        Me.HighSlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HighSlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighSlot1.InitialImage = CType(resources.GetObject("HighSlot1.InitialImage"), System.Drawing.Image)
        Me.HighSlot1.Location = New System.Drawing.Point(232, 11)
        Me.HighSlot1.Name = "HighSlot1"
        Me.HighSlot1.Size = New System.Drawing.Size(64, 64)
        Me.HighSlot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HighSlot1.TabIndex = 5
        Me.HighSlot1.TabStop = False
        '
        'MidSlot6
        '
        Me.MidSlot6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot6.BackgroundImage = CType(resources.GetObject("MidSlot6.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot6.Location = New System.Drawing.Point(15, 361)
        Me.MidSlot6.Name = "MidSlot6"
        Me.MidSlot6.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot6.TabIndex = 37
        Me.MidSlot6.TabStop = False
        '
        'MidSlot7
        '
        Me.MidSlot7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MidSlot7.BackgroundImage = CType(resources.GetObject("MidSlot7.BackgroundImage"), System.Drawing.Image)
        Me.MidSlot7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MidSlot7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidSlot7.Location = New System.Drawing.Point(15, 431)
        Me.MidSlot7.Name = "MidSlot7"
        Me.MidSlot7.Size = New System.Drawing.Size(64, 64)
        Me.MidSlot7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.MidSlot7.TabIndex = 36
        Me.MidSlot7.TabStop = False
        '
        'StructurePicture
        '
        Me.StructurePicture.BackColor = System.Drawing.Color.Black
        Me.StructurePicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.StructurePicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StructurePicture.Location = New System.Drawing.Point(5, 6)
        Me.StructurePicture.Name = "StructurePicture"
        Me.StructurePicture.Size = New System.Drawing.Size(586, 566)
        Me.StructurePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.StructurePicture.TabIndex = 9
        Me.StructurePicture.TabStop = False
        '
        'tabFuelandBonuses
        '
        Me.tabFuelandBonuses.Controls.Add(Me.gbFacilityBonuses)
        Me.tabFuelandBonuses.Controls.Add(Me.gbFuelBlocks)
        Me.tabFuelandBonuses.Location = New System.Drawing.Point(4, 22)
        Me.tabFuelandBonuses.Name = "tabFuelandBonuses"
        Me.tabFuelandBonuses.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFuelandBonuses.Size = New System.Drawing.Size(595, 577)
        Me.tabFuelandBonuses.TabIndex = 1
        Me.tabFuelandBonuses.Text = "Fuel Settings and Bonuses"
        Me.tabFuelandBonuses.UseVisualStyleBackColor = True
        '
        'gbFacilityBonuses
        '
        Me.gbFacilityBonuses.Controls.Add(Me.btnBonusPopout)
        Me.gbFacilityBonuses.Controls.Add(Me.lstUpwellStructureBonuses)
        Me.gbFacilityBonuses.Location = New System.Drawing.Point(6, 10)
        Me.gbFacilityBonuses.Name = "gbFacilityBonuses"
        Me.gbFacilityBonuses.Size = New System.Drawing.Size(583, 145)
        Me.gbFacilityBonuses.TabIndex = 0
        Me.gbFacilityBonuses.TabStop = False
        Me.gbFacilityBonuses.Text = "Facility Bonuses"
        '
        'btnBonusPopout
        '
        Me.btnBonusPopout.FlatAppearance.BorderSize = 0
        Me.btnBonusPopout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBonusPopout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBonusPopout.Image = CType(resources.GetObject("btnBonusPopout.Image"), System.Drawing.Image)
        Me.btnBonusPopout.Location = New System.Drawing.Point(551, 20)
        Me.btnBonusPopout.Name = "btnBonusPopout"
        Me.btnBonusPopout.Size = New System.Drawing.Size(18, 18)
        Me.btnBonusPopout.TabIndex = 0
        Me.btnBonusPopout.UseVisualStyleBackColor = True
        '
        'lstUpwellStructureBonuses
        '
        Me.lstUpwellStructureBonuses.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.BonusAppliesTo, Me.Activity, Me.Bonuses, Me.Source})
        Me.lstUpwellStructureBonuses.FullRowSelect = True
        Me.lstUpwellStructureBonuses.GridLines = True
        Me.lstUpwellStructureBonuses.HideSelection = False
        Me.lstUpwellStructureBonuses.Location = New System.Drawing.Point(12, 19)
        Me.lstUpwellStructureBonuses.MultiSelect = False
        Me.lstUpwellStructureBonuses.Name = "lstUpwellStructureBonuses"
        Me.lstUpwellStructureBonuses.Size = New System.Drawing.Size(560, 116)
        Me.lstUpwellStructureBonuses.TabIndex = 0
        Me.lstUpwellStructureBonuses.TabStop = False
        Me.lstUpwellStructureBonuses.UseCompatibleStateImageBehavior = False
        Me.lstUpwellStructureBonuses.View = System.Windows.Forms.View.Details
        '
        'BonusAppliesTo
        '
        Me.BonusAppliesTo.Text = "Bonus Applies to"
        Me.BonusAppliesTo.Width = 125
        '
        'Activity
        '
        Me.Activity.Text = "Activity"
        Me.Activity.Width = 100
        '
        'Bonuses
        '
        Me.Bonuses.Text = "Bonuses"
        Me.Bonuses.Width = 200
        '
        'Source
        '
        Me.Source.Text = "Bonus Source"
        Me.Source.Width = 131
        '
        'gbFuelBlocks
        '
        Me.gbFuelBlocks.Controls.Add(Me.btnRefreshPrices)
        Me.gbFuelBlocks.Controls.Add(Me.gbFuelPrices)
        Me.gbFuelBlocks.Controls.Add(Me.btnSaveFuelBlockInfo)
        Me.gbFuelBlocks.Controls.Add(Me.gbFuelBlocks2)
        Me.gbFuelBlocks.Controls.Add(Me.gbFuelBlockOptions)
        Me.gbFuelBlocks.Controls.Add(Me.btnSavePrices)
        Me.gbFuelBlocks.Controls.Add(Me.btnUpdateBuildCost)
        Me.gbFuelBlocks.Location = New System.Drawing.Point(6, 161)
        Me.gbFuelBlocks.Name = "gbFuelBlocks"
        Me.gbFuelBlocks.Size = New System.Drawing.Size(583, 410)
        Me.gbFuelBlocks.TabIndex = 0
        Me.gbFuelBlocks.TabStop = False
        Me.gbFuelBlocks.Text = "Fuel Blocks"
        '
        'btnRefreshPrices
        '
        Me.btnRefreshPrices.Location = New System.Drawing.Point(444, 19)
        Me.btnRefreshPrices.Name = "btnRefreshPrices"
        Me.btnRefreshPrices.Size = New System.Drawing.Size(125, 34)
        Me.btnRefreshPrices.TabIndex = 6
        Me.btnRefreshPrices.Text = "Refresh Prices"
        Me.btnRefreshPrices.UseVisualStyleBackColor = True
        '
        'gbFuelPrices
        '
        Me.gbFuelPrices.Controls.Add(Me.lblHydrogenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.lblHeavyWater)
        Me.gbFuelPrices.Controls.Add(Me.lblStrontiumClathrates)
        Me.gbFuelPrices.Controls.Add(Me.lblEnrichedUranium)
        Me.gbFuelPrices.Controls.Add(Me.lblOxygen)
        Me.gbFuelPrices.Controls.Add(Me.lblCoolant)
        Me.gbFuelPrices.Controls.Add(Me.lblLiquidOzone)
        Me.gbFuelPrices.Controls.Add(Me.lblMechanicalParts)
        Me.gbFuelPrices.Controls.Add(Me.lblRobotics)
        Me.gbFuelPrices.Controls.Add(Me.lblNitrogenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.lblHeliumIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.lblOxygenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.picHeliumIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.txtMechanicalParts)
        Me.gbFuelPrices.Controls.Add(Me.txtNitrogenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.txtLiquidOzone)
        Me.gbFuelPrices.Controls.Add(Me.txtCoolant)
        Me.gbFuelPrices.Controls.Add(Me.txtOxygen)
        Me.gbFuelPrices.Controls.Add(Me.txtEnrichedUranium)
        Me.gbFuelPrices.Controls.Add(Me.txtStrontiumClathrates)
        Me.gbFuelPrices.Controls.Add(Me.picCoolant)
        Me.gbFuelPrices.Controls.Add(Me.picOxygen)
        Me.gbFuelPrices.Controls.Add(Me.txtHeavyWater)
        Me.gbFuelPrices.Controls.Add(Me.picNitrogenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.txtRobotics)
        Me.gbFuelPrices.Controls.Add(Me.picHydrogenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.picEnrichedUranium)
        Me.gbFuelPrices.Controls.Add(Me.picOxygenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.picMechanicalParts)
        Me.gbFuelPrices.Controls.Add(Me.picRobotics)
        Me.gbFuelPrices.Controls.Add(Me.picStrontiumClathrates)
        Me.gbFuelPrices.Controls.Add(Me.txtOxygenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.picLiquidOzone)
        Me.gbFuelPrices.Controls.Add(Me.picHeavyWater)
        Me.gbFuelPrices.Controls.Add(Me.txtHydrogenIsotopes)
        Me.gbFuelPrices.Controls.Add(Me.txtHeliumIsotopes)
        Me.gbFuelPrices.Location = New System.Drawing.Point(13, 219)
        Me.gbFuelPrices.Name = "gbFuelPrices"
        Me.gbFuelPrices.Size = New System.Drawing.Size(420, 191)
        Me.gbFuelPrices.TabIndex = 1
        Me.gbFuelPrices.TabStop = False
        '
        'lblHydrogenIsotopes
        '
        Me.lblHydrogenIsotopes.Location = New System.Drawing.Point(174, 12)
        Me.lblHydrogenIsotopes.Name = "lblHydrogenIsotopes"
        Me.lblHydrogenIsotopes.Size = New System.Drawing.Size(101, 16)
        Me.lblHydrogenIsotopes.TabIndex = 2
        Me.lblHydrogenIsotopes.Text = "Hydrogen Isotopes"
        Me.lblHydrogenIsotopes.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblHeavyWater
        '
        Me.lblHeavyWater.Location = New System.Drawing.Point(315, 12)
        Me.lblHeavyWater.Name = "lblHeavyWater"
        Me.lblHeavyWater.Size = New System.Drawing.Size(93, 16)
        Me.lblHeavyWater.TabIndex = 4
        Me.lblHeavyWater.Text = "Heavy Water"
        Me.lblHeavyWater.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblStrontiumClathrates
        '
        Me.lblStrontiumClathrates.Location = New System.Drawing.Point(311, 55)
        Me.lblStrontiumClathrates.Name = "lblStrontiumClathrates"
        Me.lblStrontiumClathrates.Size = New System.Drawing.Size(101, 16)
        Me.lblStrontiumClathrates.TabIndex = 10
        Me.lblStrontiumClathrates.Text = "Strontium Clathrates"
        Me.lblStrontiumClathrates.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEnrichedUranium
        '
        Me.lblEnrichedUranium.Location = New System.Drawing.Point(43, 141)
        Me.lblEnrichedUranium.Name = "lblEnrichedUranium"
        Me.lblEnrichedUranium.Size = New System.Drawing.Size(93, 16)
        Me.lblEnrichedUranium.TabIndex = 18
        Me.lblEnrichedUranium.Text = "Enriched Uranium"
        Me.lblEnrichedUranium.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblOxygen
        '
        Me.lblOxygen.Location = New System.Drawing.Point(178, 141)
        Me.lblOxygen.Name = "lblOxygen"
        Me.lblOxygen.Size = New System.Drawing.Size(93, 16)
        Me.lblOxygen.TabIndex = 20
        Me.lblOxygen.Text = "Oxygen"
        Me.lblOxygen.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCoolant
        '
        Me.lblCoolant.Location = New System.Drawing.Point(315, 98)
        Me.lblCoolant.Name = "lblCoolant"
        Me.lblCoolant.Size = New System.Drawing.Size(93, 16)
        Me.lblCoolant.TabIndex = 16
        Me.lblCoolant.Text = "Coolant"
        Me.lblCoolant.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblLiquidOzone
        '
        Me.lblLiquidOzone.Location = New System.Drawing.Point(313, 141)
        Me.lblLiquidOzone.Name = "lblLiquidOzone"
        Me.lblLiquidOzone.Size = New System.Drawing.Size(93, 16)
        Me.lblLiquidOzone.TabIndex = 22
        Me.lblLiquidOzone.Text = "Liquid Ozone"
        Me.lblLiquidOzone.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblMechanicalParts
        '
        Me.lblMechanicalParts.Location = New System.Drawing.Point(43, 98)
        Me.lblMechanicalParts.Name = "lblMechanicalParts"
        Me.lblMechanicalParts.Size = New System.Drawing.Size(93, 16)
        Me.lblMechanicalParts.TabIndex = 12
        Me.lblMechanicalParts.Text = "Mechanical Parts"
        Me.lblMechanicalParts.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblRobotics
        '
        Me.lblRobotics.Location = New System.Drawing.Point(178, 98)
        Me.lblRobotics.Name = "lblRobotics"
        Me.lblRobotics.Size = New System.Drawing.Size(93, 16)
        Me.lblRobotics.TabIndex = 14
        Me.lblRobotics.Text = "Robotics"
        Me.lblRobotics.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblNitrogenIsotopes
        '
        Me.lblNitrogenIsotopes.Location = New System.Drawing.Point(43, 55)
        Me.lblNitrogenIsotopes.Name = "lblNitrogenIsotopes"
        Me.lblNitrogenIsotopes.Size = New System.Drawing.Size(93, 16)
        Me.lblNitrogenIsotopes.TabIndex = 6
        Me.lblNitrogenIsotopes.Text = "Nitrogen Isotopes"
        Me.lblNitrogenIsotopes.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblHeliumIsotopes
        '
        Me.lblHeliumIsotopes.Location = New System.Drawing.Point(43, 12)
        Me.lblHeliumIsotopes.Name = "lblHeliumIsotopes"
        Me.lblHeliumIsotopes.Size = New System.Drawing.Size(93, 16)
        Me.lblHeliumIsotopes.TabIndex = 0
        Me.lblHeliumIsotopes.Text = "Helium Isotopes"
        Me.lblHeliumIsotopes.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblOxygenIsotopes
        '
        Me.lblOxygenIsotopes.Location = New System.Drawing.Point(178, 55)
        Me.lblOxygenIsotopes.Name = "lblOxygenIsotopes"
        Me.lblOxygenIsotopes.Size = New System.Drawing.Size(93, 16)
        Me.lblOxygenIsotopes.TabIndex = 8
        Me.lblOxygenIsotopes.Text = "Oxygen Isotopes"
        Me.lblOxygenIsotopes.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'picHeliumIsotopes
        '
        Me.picHeliumIsotopes.BackColor = System.Drawing.Color.Transparent
        Me.picHeliumIsotopes.Location = New System.Drawing.Point(8, 22)
        Me.picHeliumIsotopes.Name = "picHeliumIsotopes"
        Me.picHeliumIsotopes.Size = New System.Drawing.Size(32, 32)
        Me.picHeliumIsotopes.TabIndex = 290
        Me.picHeliumIsotopes.TabStop = False
        '
        'txtMechanicalParts
        '
        Me.txtMechanicalParts.Location = New System.Drawing.Point(43, 114)
        Me.txtMechanicalParts.Name = "txtMechanicalParts"
        Me.txtMechanicalParts.Size = New System.Drawing.Size(93, 20)
        Me.txtMechanicalParts.TabIndex = 13
        Me.txtMechanicalParts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNitrogenIsotopes
        '
        Me.txtNitrogenIsotopes.Location = New System.Drawing.Point(43, 71)
        Me.txtNitrogenIsotopes.Name = "txtNitrogenIsotopes"
        Me.txtNitrogenIsotopes.Size = New System.Drawing.Size(93, 20)
        Me.txtNitrogenIsotopes.TabIndex = 7
        Me.txtNitrogenIsotopes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLiquidOzone
        '
        Me.txtLiquidOzone.Location = New System.Drawing.Point(313, 157)
        Me.txtLiquidOzone.Name = "txtLiquidOzone"
        Me.txtLiquidOzone.Size = New System.Drawing.Size(93, 20)
        Me.txtLiquidOzone.TabIndex = 23
        Me.txtLiquidOzone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCoolant
        '
        Me.txtCoolant.Location = New System.Drawing.Point(315, 114)
        Me.txtCoolant.Name = "txtCoolant"
        Me.txtCoolant.Size = New System.Drawing.Size(93, 20)
        Me.txtCoolant.TabIndex = 17
        Me.txtCoolant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOxygen
        '
        Me.txtOxygen.Location = New System.Drawing.Point(178, 157)
        Me.txtOxygen.Name = "txtOxygen"
        Me.txtOxygen.Size = New System.Drawing.Size(93, 20)
        Me.txtOxygen.TabIndex = 21
        Me.txtOxygen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEnrichedUranium
        '
        Me.txtEnrichedUranium.Location = New System.Drawing.Point(43, 157)
        Me.txtEnrichedUranium.Name = "txtEnrichedUranium"
        Me.txtEnrichedUranium.Size = New System.Drawing.Size(93, 20)
        Me.txtEnrichedUranium.TabIndex = 19
        Me.txtEnrichedUranium.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStrontiumClathrates
        '
        Me.txtStrontiumClathrates.Location = New System.Drawing.Point(315, 71)
        Me.txtStrontiumClathrates.Name = "txtStrontiumClathrates"
        Me.txtStrontiumClathrates.Size = New System.Drawing.Size(93, 20)
        Me.txtStrontiumClathrates.TabIndex = 11
        Me.txtStrontiumClathrates.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picCoolant
        '
        Me.picCoolant.BackColor = System.Drawing.Color.Transparent
        Me.picCoolant.Location = New System.Drawing.Point(277, 108)
        Me.picCoolant.Name = "picCoolant"
        Me.picCoolant.Size = New System.Drawing.Size(32, 32)
        Me.picCoolant.TabIndex = 291
        Me.picCoolant.TabStop = False
        '
        'picOxygen
        '
        Me.picOxygen.BackColor = System.Drawing.Color.Transparent
        Me.picOxygen.Location = New System.Drawing.Point(142, 151)
        Me.picOxygen.Name = "picOxygen"
        Me.picOxygen.Size = New System.Drawing.Size(32, 32)
        Me.picOxygen.TabIndex = 313
        Me.picOxygen.TabStop = False
        '
        'txtHeavyWater
        '
        Me.txtHeavyWater.Location = New System.Drawing.Point(315, 28)
        Me.txtHeavyWater.Name = "txtHeavyWater"
        Me.txtHeavyWater.Size = New System.Drawing.Size(93, 20)
        Me.txtHeavyWater.TabIndex = 5
        Me.txtHeavyWater.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picNitrogenIsotopes
        '
        Me.picNitrogenIsotopes.BackColor = System.Drawing.Color.Transparent
        Me.picNitrogenIsotopes.Location = New System.Drawing.Point(8, 65)
        Me.picNitrogenIsotopes.Name = "picNitrogenIsotopes"
        Me.picNitrogenIsotopes.Size = New System.Drawing.Size(32, 32)
        Me.picNitrogenIsotopes.TabIndex = 292
        Me.picNitrogenIsotopes.TabStop = False
        '
        'txtRobotics
        '
        Me.txtRobotics.Location = New System.Drawing.Point(178, 114)
        Me.txtRobotics.Name = "txtRobotics"
        Me.txtRobotics.Size = New System.Drawing.Size(93, 20)
        Me.txtRobotics.TabIndex = 15
        Me.txtRobotics.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picHydrogenIsotopes
        '
        Me.picHydrogenIsotopes.BackColor = System.Drawing.Color.Transparent
        Me.picHydrogenIsotopes.Location = New System.Drawing.Point(142, 22)
        Me.picHydrogenIsotopes.Name = "picHydrogenIsotopes"
        Me.picHydrogenIsotopes.Size = New System.Drawing.Size(32, 32)
        Me.picHydrogenIsotopes.TabIndex = 306
        Me.picHydrogenIsotopes.TabStop = False
        '
        'picEnrichedUranium
        '
        Me.picEnrichedUranium.BackColor = System.Drawing.Color.Transparent
        Me.picEnrichedUranium.Location = New System.Drawing.Point(9, 151)
        Me.picEnrichedUranium.Name = "picEnrichedUranium"
        Me.picEnrichedUranium.Size = New System.Drawing.Size(32, 32)
        Me.picEnrichedUranium.TabIndex = 293
        Me.picEnrichedUranium.TabStop = False
        '
        'picOxygenIsotopes
        '
        Me.picOxygenIsotopes.BackColor = System.Drawing.Color.Transparent
        Me.picOxygenIsotopes.Location = New System.Drawing.Point(142, 65)
        Me.picOxygenIsotopes.Name = "picOxygenIsotopes"
        Me.picOxygenIsotopes.Size = New System.Drawing.Size(32, 32)
        Me.picOxygenIsotopes.TabIndex = 307
        Me.picOxygenIsotopes.TabStop = False
        '
        'picMechanicalParts
        '
        Me.picMechanicalParts.BackColor = System.Drawing.Color.Transparent
        Me.picMechanicalParts.Location = New System.Drawing.Point(8, 108)
        Me.picMechanicalParts.Name = "picMechanicalParts"
        Me.picMechanicalParts.Size = New System.Drawing.Size(32, 32)
        Me.picMechanicalParts.TabIndex = 294
        Me.picMechanicalParts.TabStop = False
        '
        'picRobotics
        '
        Me.picRobotics.BackColor = System.Drawing.Color.Transparent
        Me.picRobotics.Location = New System.Drawing.Point(142, 108)
        Me.picRobotics.Name = "picRobotics"
        Me.picRobotics.Size = New System.Drawing.Size(32, 32)
        Me.picRobotics.TabIndex = 310
        Me.picRobotics.TabStop = False
        '
        'picStrontiumClathrates
        '
        Me.picStrontiumClathrates.BackColor = System.Drawing.Color.Transparent
        Me.picStrontiumClathrates.Location = New System.Drawing.Point(277, 65)
        Me.picStrontiumClathrates.Name = "picStrontiumClathrates"
        Me.picStrontiumClathrates.Size = New System.Drawing.Size(32, 32)
        Me.picStrontiumClathrates.TabIndex = 295
        Me.picStrontiumClathrates.TabStop = False
        '
        'txtOxygenIsotopes
        '
        Me.txtOxygenIsotopes.Location = New System.Drawing.Point(178, 71)
        Me.txtOxygenIsotopes.Name = "txtOxygenIsotopes"
        Me.txtOxygenIsotopes.Size = New System.Drawing.Size(93, 20)
        Me.txtOxygenIsotopes.TabIndex = 9
        Me.txtOxygenIsotopes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picLiquidOzone
        '
        Me.picLiquidOzone.BackColor = System.Drawing.Color.Transparent
        Me.picLiquidOzone.Location = New System.Drawing.Point(277, 151)
        Me.picLiquidOzone.Name = "picLiquidOzone"
        Me.picLiquidOzone.Size = New System.Drawing.Size(32, 32)
        Me.picLiquidOzone.TabIndex = 296
        Me.picLiquidOzone.TabStop = False
        '
        'picHeavyWater
        '
        Me.picHeavyWater.BackColor = System.Drawing.Color.Transparent
        Me.picHeavyWater.Location = New System.Drawing.Point(277, 22)
        Me.picHeavyWater.Name = "picHeavyWater"
        Me.picHeavyWater.Size = New System.Drawing.Size(32, 32)
        Me.picHeavyWater.TabIndex = 297
        Me.picHeavyWater.TabStop = False
        '
        'txtHydrogenIsotopes
        '
        Me.txtHydrogenIsotopes.Location = New System.Drawing.Point(178, 28)
        Me.txtHydrogenIsotopes.Name = "txtHydrogenIsotopes"
        Me.txtHydrogenIsotopes.Size = New System.Drawing.Size(93, 20)
        Me.txtHydrogenIsotopes.TabIndex = 3
        Me.txtHydrogenIsotopes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHeliumIsotopes
        '
        Me.txtHeliumIsotopes.Location = New System.Drawing.Point(43, 28)
        Me.txtHeliumIsotopes.Name = "txtHeliumIsotopes"
        Me.txtHeliumIsotopes.Size = New System.Drawing.Size(93, 20)
        Me.txtHeliumIsotopes.TabIndex = 1
        Me.txtHeliumIsotopes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSaveFuelBlockInfo
        '
        Me.btnSaveFuelBlockInfo.Location = New System.Drawing.Point(444, 133)
        Me.btnSaveFuelBlockInfo.Name = "btnSaveFuelBlockInfo"
        Me.btnSaveFuelBlockInfo.Size = New System.Drawing.Size(125, 34)
        Me.btnSaveFuelBlockInfo.TabIndex = 4
        Me.btnSaveFuelBlockInfo.Text = "Save Fuel Block Info"
        Me.btnSaveFuelBlockInfo.UseVisualStyleBackColor = True
        '
        'gbFuelBlocks2
        '
        Me.gbFuelBlocks2.Controls.Add(Me.picHeliumFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHeliumFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHeliumFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.txtHeliumFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHeliumFuelBlockBuy)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHeliumFuelBlockBuild)
        Me.gbFuelBlocks2.Controls.Add(Me.txtHeliumFuelBlockBuyPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHeliumFuelBlockBuildPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHydrogenFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHydrogenFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.picHydrogenFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.txtHydrogenFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHydrogenBlockBuy)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHydrogenFuelBlockBuild)
        Me.gbFuelBlocks2.Controls.Add(Me.txtHydrogenFuelBlockBuyPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblHydrogenFuelBlockBuildPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblNitrogenFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.lblNitrogenFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.picNitrogenFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.txtNitrogenFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.lblNitrogenFuelBlockBuy)
        Me.gbFuelBlocks2.Controls.Add(Me.lblNitrogenFuelBlockBuild)
        Me.gbFuelBlocks2.Controls.Add(Me.txtNitrogenFuelBlockBuyPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblNitrogenFuelBlockBuildPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblOxygenFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.lblOxygenFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.picOxygenFuelBlock)
        Me.gbFuelBlocks2.Controls.Add(Me.txtOxygenFuelBlockBPME)
        Me.gbFuelBlocks2.Controls.Add(Me.lblOxygenFuelBlockBuy)
        Me.gbFuelBlocks2.Controls.Add(Me.lblOxygenFuelBlockBuild)
        Me.gbFuelBlocks2.Controls.Add(Me.txtOxygenFuelBlockBuyPrice)
        Me.gbFuelBlocks2.Controls.Add(Me.lblOxygenFuelBlockBuildPrice)
        Me.gbFuelBlocks2.Location = New System.Drawing.Point(13, 14)
        Me.gbFuelBlocks2.Name = "gbFuelBlocks2"
        Me.gbFuelBlocks2.Size = New System.Drawing.Size(420, 205)
        Me.gbFuelBlocks2.TabIndex = 0
        Me.gbFuelBlocks2.TabStop = False
        '
        'picHeliumFuelBlock
        '
        Me.picHeliumFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picHeliumFuelBlock.Location = New System.Drawing.Point(32, 32)
        Me.picHeliumFuelBlock.Name = "picHeliumFuelBlock"
        Me.picHeliumFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picHeliumFuelBlock.TabIndex = 222
        Me.picHeliumFuelBlock.TabStop = False
        '
        'lblHeliumFuelBlock
        '
        Me.lblHeliumFuelBlock.Location = New System.Drawing.Point(103, 18)
        Me.lblHeliumFuelBlock.Name = "lblHeliumFuelBlock"
        Me.lblHeliumFuelBlock.Size = New System.Drawing.Size(76, 13)
        Me.lblHeliumFuelBlock.TabIndex = 0
        Me.lblHeliumFuelBlock.Text = "Helium"
        Me.lblHeliumFuelBlock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblHeliumFuelBlockBPME
        '
        Me.lblHeliumFuelBlockBPME.Location = New System.Drawing.Point(38, 79)
        Me.lblHeliumFuelBlockBPME.Name = "lblHeliumFuelBlockBPME"
        Me.lblHeliumFuelBlockBPME.Size = New System.Drawing.Size(97, 15)
        Me.lblHeliumFuelBlockBPME.TabIndex = 5
        Me.lblHeliumFuelBlockBPME.Text = "Fuel Block BP ME:"
        '
        'txtHeliumFuelBlockBPME
        '
        Me.txtHeliumFuelBlockBPME.Location = New System.Drawing.Point(141, 76)
        Me.txtHeliumFuelBlockBPME.Name = "txtHeliumFuelBlockBPME"
        Me.txtHeliumFuelBlockBPME.Size = New System.Drawing.Size(41, 20)
        Me.txtHeliumFuelBlockBPME.TabIndex = 6
        Me.txtHeliumFuelBlockBPME.Text = "0"
        Me.txtHeliumFuelBlockBPME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHeliumFuelBlockBuy
        '
        Me.lblHeliumFuelBlockBuy.AutoSize = True
        Me.lblHeliumFuelBlockBuy.Location = New System.Drawing.Point(76, 36)
        Me.lblHeliumFuelBlockBuy.Name = "lblHeliumFuelBlockBuy"
        Me.lblHeliumFuelBlockBuy.Size = New System.Drawing.Size(28, 13)
        Me.lblHeliumFuelBlockBuy.TabIndex = 1
        Me.lblHeliumFuelBlockBuy.Text = "Buy:"
        Me.lblHeliumFuelBlockBuy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblHeliumFuelBlockBuild
        '
        Me.lblHeliumFuelBlockBuild.AutoSize = True
        Me.lblHeliumFuelBlockBuild.Location = New System.Drawing.Point(71, 57)
        Me.lblHeliumFuelBlockBuild.Name = "lblHeliumFuelBlockBuild"
        Me.lblHeliumFuelBlockBuild.Size = New System.Drawing.Size(33, 13)
        Me.lblHeliumFuelBlockBuild.TabIndex = 3
        Me.lblHeliumFuelBlockBuild.Text = "Build:"
        Me.lblHeliumFuelBlockBuild.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtHeliumFuelBlockBuyPrice
        '
        Me.txtHeliumFuelBlockBuyPrice.Location = New System.Drawing.Point(106, 32)
        Me.txtHeliumFuelBlockBuyPrice.Name = "txtHeliumFuelBlockBuyPrice"
        Me.txtHeliumFuelBlockBuyPrice.Size = New System.Drawing.Size(76, 20)
        Me.txtHeliumFuelBlockBuyPrice.TabIndex = 2
        Me.txtHeliumFuelBlockBuyPrice.Text = "0.00"
        Me.txtHeliumFuelBlockBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHeliumFuelBlockBuildPrice
        '
        Me.lblHeliumFuelBlockBuildPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHeliumFuelBlockBuildPrice.Location = New System.Drawing.Point(106, 54)
        Me.lblHeliumFuelBlockBuildPrice.Name = "lblHeliumFuelBlockBuildPrice"
        Me.lblHeliumFuelBlockBuildPrice.Size = New System.Drawing.Size(76, 20)
        Me.lblHeliumFuelBlockBuildPrice.TabIndex = 4
        Me.lblHeliumFuelBlockBuildPrice.Text = "0.00"
        Me.lblHeliumFuelBlockBuildPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHydrogenFuelBlock
        '
        Me.lblHydrogenFuelBlock.Location = New System.Drawing.Point(310, 18)
        Me.lblHydrogenFuelBlock.Name = "lblHydrogenFuelBlock"
        Me.lblHydrogenFuelBlock.Size = New System.Drawing.Size(76, 13)
        Me.lblHydrogenFuelBlock.TabIndex = 7
        Me.lblHydrogenFuelBlock.Text = "Hydrogen"
        Me.lblHydrogenFuelBlock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblHydrogenFuelBlockBPME
        '
        Me.lblHydrogenFuelBlockBPME.Location = New System.Drawing.Point(245, 79)
        Me.lblHydrogenFuelBlockBPME.Name = "lblHydrogenFuelBlockBPME"
        Me.lblHydrogenFuelBlockBPME.Size = New System.Drawing.Size(97, 15)
        Me.lblHydrogenFuelBlockBPME.TabIndex = 12
        Me.lblHydrogenFuelBlockBPME.Text = "Fuel Block BP ME:"
        '
        'picHydrogenFuelBlock
        '
        Me.picHydrogenFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picHydrogenFuelBlock.Location = New System.Drawing.Point(240, 32)
        Me.picHydrogenFuelBlock.Name = "picHydrogenFuelBlock"
        Me.picHydrogenFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picHydrogenFuelBlock.TabIndex = 247
        Me.picHydrogenFuelBlock.TabStop = False
        '
        'txtHydrogenFuelBlockBPME
        '
        Me.txtHydrogenFuelBlockBPME.Location = New System.Drawing.Point(348, 76)
        Me.txtHydrogenFuelBlockBPME.Name = "txtHydrogenFuelBlockBPME"
        Me.txtHydrogenFuelBlockBPME.Size = New System.Drawing.Size(41, 20)
        Me.txtHydrogenFuelBlockBPME.TabIndex = 13
        Me.txtHydrogenFuelBlockBPME.Text = "0"
        Me.txtHydrogenFuelBlockBPME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHydrogenBlockBuy
        '
        Me.lblHydrogenBlockBuy.AutoSize = True
        Me.lblHydrogenBlockBuy.Location = New System.Drawing.Point(283, 36)
        Me.lblHydrogenBlockBuy.Name = "lblHydrogenBlockBuy"
        Me.lblHydrogenBlockBuy.Size = New System.Drawing.Size(28, 13)
        Me.lblHydrogenBlockBuy.TabIndex = 8
        Me.lblHydrogenBlockBuy.Text = "Buy:"
        Me.lblHydrogenBlockBuy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblHydrogenFuelBlockBuild
        '
        Me.lblHydrogenFuelBlockBuild.AutoSize = True
        Me.lblHydrogenFuelBlockBuild.Location = New System.Drawing.Point(278, 57)
        Me.lblHydrogenFuelBlockBuild.Name = "lblHydrogenFuelBlockBuild"
        Me.lblHydrogenFuelBlockBuild.Size = New System.Drawing.Size(33, 13)
        Me.lblHydrogenFuelBlockBuild.TabIndex = 10
        Me.lblHydrogenFuelBlockBuild.Text = "Build:"
        Me.lblHydrogenFuelBlockBuild.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtHydrogenFuelBlockBuyPrice
        '
        Me.txtHydrogenFuelBlockBuyPrice.Location = New System.Drawing.Point(313, 32)
        Me.txtHydrogenFuelBlockBuyPrice.Name = "txtHydrogenFuelBlockBuyPrice"
        Me.txtHydrogenFuelBlockBuyPrice.Size = New System.Drawing.Size(76, 20)
        Me.txtHydrogenFuelBlockBuyPrice.TabIndex = 9
        Me.txtHydrogenFuelBlockBuyPrice.Text = "0.00"
        Me.txtHydrogenFuelBlockBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHydrogenFuelBlockBuildPrice
        '
        Me.lblHydrogenFuelBlockBuildPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHydrogenFuelBlockBuildPrice.Location = New System.Drawing.Point(313, 54)
        Me.lblHydrogenFuelBlockBuildPrice.Name = "lblHydrogenFuelBlockBuildPrice"
        Me.lblHydrogenFuelBlockBuildPrice.Size = New System.Drawing.Size(76, 20)
        Me.lblHydrogenFuelBlockBuildPrice.TabIndex = 11
        Me.lblHydrogenFuelBlockBuildPrice.Text = "0.00"
        Me.lblHydrogenFuelBlockBuildPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNitrogenFuelBlock
        '
        Me.lblNitrogenFuelBlock.Location = New System.Drawing.Point(102, 108)
        Me.lblNitrogenFuelBlock.Name = "lblNitrogenFuelBlock"
        Me.lblNitrogenFuelBlock.Size = New System.Drawing.Size(76, 13)
        Me.lblNitrogenFuelBlock.TabIndex = 14
        Me.lblNitrogenFuelBlock.Text = "Nitrogen"
        Me.lblNitrogenFuelBlock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblNitrogenFuelBlockBPME
        '
        Me.lblNitrogenFuelBlockBPME.Location = New System.Drawing.Point(37, 169)
        Me.lblNitrogenFuelBlockBPME.Name = "lblNitrogenFuelBlockBPME"
        Me.lblNitrogenFuelBlockBPME.Size = New System.Drawing.Size(97, 15)
        Me.lblNitrogenFuelBlockBPME.TabIndex = 19
        Me.lblNitrogenFuelBlockBPME.Text = "Fuel Block BP ME:"
        '
        'picNitrogenFuelBlock
        '
        Me.picNitrogenFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picNitrogenFuelBlock.Location = New System.Drawing.Point(32, 122)
        Me.picNitrogenFuelBlock.Name = "picNitrogenFuelBlock"
        Me.picNitrogenFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picNitrogenFuelBlock.TabIndex = 258
        Me.picNitrogenFuelBlock.TabStop = False
        '
        'txtNitrogenFuelBlockBPME
        '
        Me.txtNitrogenFuelBlockBPME.Location = New System.Drawing.Point(140, 166)
        Me.txtNitrogenFuelBlockBPME.Name = "txtNitrogenFuelBlockBPME"
        Me.txtNitrogenFuelBlockBPME.Size = New System.Drawing.Size(41, 20)
        Me.txtNitrogenFuelBlockBPME.TabIndex = 20
        Me.txtNitrogenFuelBlockBPME.Text = "0"
        Me.txtNitrogenFuelBlockBPME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNitrogenFuelBlockBuy
        '
        Me.lblNitrogenFuelBlockBuy.AutoSize = True
        Me.lblNitrogenFuelBlockBuy.Location = New System.Drawing.Point(75, 126)
        Me.lblNitrogenFuelBlockBuy.Name = "lblNitrogenFuelBlockBuy"
        Me.lblNitrogenFuelBlockBuy.Size = New System.Drawing.Size(28, 13)
        Me.lblNitrogenFuelBlockBuy.TabIndex = 15
        Me.lblNitrogenFuelBlockBuy.Text = "Buy:"
        Me.lblNitrogenFuelBlockBuy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblNitrogenFuelBlockBuild
        '
        Me.lblNitrogenFuelBlockBuild.AutoSize = True
        Me.lblNitrogenFuelBlockBuild.Location = New System.Drawing.Point(70, 147)
        Me.lblNitrogenFuelBlockBuild.Name = "lblNitrogenFuelBlockBuild"
        Me.lblNitrogenFuelBlockBuild.Size = New System.Drawing.Size(33, 13)
        Me.lblNitrogenFuelBlockBuild.TabIndex = 17
        Me.lblNitrogenFuelBlockBuild.Text = "Build:"
        Me.lblNitrogenFuelBlockBuild.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtNitrogenFuelBlockBuyPrice
        '
        Me.txtNitrogenFuelBlockBuyPrice.Location = New System.Drawing.Point(105, 122)
        Me.txtNitrogenFuelBlockBuyPrice.Name = "txtNitrogenFuelBlockBuyPrice"
        Me.txtNitrogenFuelBlockBuyPrice.Size = New System.Drawing.Size(76, 20)
        Me.txtNitrogenFuelBlockBuyPrice.TabIndex = 16
        Me.txtNitrogenFuelBlockBuyPrice.Text = "0.00"
        Me.txtNitrogenFuelBlockBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNitrogenFuelBlockBuildPrice
        '
        Me.lblNitrogenFuelBlockBuildPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNitrogenFuelBlockBuildPrice.Location = New System.Drawing.Point(105, 144)
        Me.lblNitrogenFuelBlockBuildPrice.Name = "lblNitrogenFuelBlockBuildPrice"
        Me.lblNitrogenFuelBlockBuildPrice.Size = New System.Drawing.Size(76, 20)
        Me.lblNitrogenFuelBlockBuildPrice.TabIndex = 18
        Me.lblNitrogenFuelBlockBuildPrice.Text = "0.00"
        Me.lblNitrogenFuelBlockBuildPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOxygenFuelBlock
        '
        Me.lblOxygenFuelBlock.Location = New System.Drawing.Point(310, 108)
        Me.lblOxygenFuelBlock.Name = "lblOxygenFuelBlock"
        Me.lblOxygenFuelBlock.Size = New System.Drawing.Size(76, 13)
        Me.lblOxygenFuelBlock.TabIndex = 21
        Me.lblOxygenFuelBlock.Text = "Oxygen"
        Me.lblOxygenFuelBlock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblOxygenFuelBlockBPME
        '
        Me.lblOxygenFuelBlockBPME.Location = New System.Drawing.Point(245, 169)
        Me.lblOxygenFuelBlockBPME.Name = "lblOxygenFuelBlockBPME"
        Me.lblOxygenFuelBlockBPME.Size = New System.Drawing.Size(97, 15)
        Me.lblOxygenFuelBlockBPME.TabIndex = 26
        Me.lblOxygenFuelBlockBPME.Text = "Fuel Block BP ME:"
        '
        'picOxygenFuelBlock
        '
        Me.picOxygenFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picOxygenFuelBlock.Location = New System.Drawing.Point(240, 122)
        Me.picOxygenFuelBlock.Name = "picOxygenFuelBlock"
        Me.picOxygenFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picOxygenFuelBlock.TabIndex = 269
        Me.picOxygenFuelBlock.TabStop = False
        '
        'txtOxygenFuelBlockBPME
        '
        Me.txtOxygenFuelBlockBPME.Location = New System.Drawing.Point(348, 166)
        Me.txtOxygenFuelBlockBPME.Name = "txtOxygenFuelBlockBPME"
        Me.txtOxygenFuelBlockBPME.Size = New System.Drawing.Size(41, 20)
        Me.txtOxygenFuelBlockBPME.TabIndex = 27
        Me.txtOxygenFuelBlockBPME.Text = "0"
        Me.txtOxygenFuelBlockBPME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblOxygenFuelBlockBuy
        '
        Me.lblOxygenFuelBlockBuy.AutoSize = True
        Me.lblOxygenFuelBlockBuy.Location = New System.Drawing.Point(283, 126)
        Me.lblOxygenFuelBlockBuy.Name = "lblOxygenFuelBlockBuy"
        Me.lblOxygenFuelBlockBuy.Size = New System.Drawing.Size(28, 13)
        Me.lblOxygenFuelBlockBuy.TabIndex = 22
        Me.lblOxygenFuelBlockBuy.Text = "Buy:"
        Me.lblOxygenFuelBlockBuy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblOxygenFuelBlockBuild
        '
        Me.lblOxygenFuelBlockBuild.AutoSize = True
        Me.lblOxygenFuelBlockBuild.Location = New System.Drawing.Point(278, 147)
        Me.lblOxygenFuelBlockBuild.Name = "lblOxygenFuelBlockBuild"
        Me.lblOxygenFuelBlockBuild.Size = New System.Drawing.Size(33, 13)
        Me.lblOxygenFuelBlockBuild.TabIndex = 24
        Me.lblOxygenFuelBlockBuild.Text = "Build:"
        Me.lblOxygenFuelBlockBuild.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtOxygenFuelBlockBuyPrice
        '
        Me.txtOxygenFuelBlockBuyPrice.Location = New System.Drawing.Point(313, 122)
        Me.txtOxygenFuelBlockBuyPrice.Name = "txtOxygenFuelBlockBuyPrice"
        Me.txtOxygenFuelBlockBuyPrice.Size = New System.Drawing.Size(76, 20)
        Me.txtOxygenFuelBlockBuyPrice.TabIndex = 23
        Me.txtOxygenFuelBlockBuyPrice.Text = "0.00"
        Me.txtOxygenFuelBlockBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblOxygenFuelBlockBuildPrice
        '
        Me.lblOxygenFuelBlockBuildPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOxygenFuelBlockBuildPrice.Location = New System.Drawing.Point(313, 144)
        Me.lblOxygenFuelBlockBuildPrice.Name = "lblOxygenFuelBlockBuildPrice"
        Me.lblOxygenFuelBlockBuildPrice.Size = New System.Drawing.Size(76, 20)
        Me.lblOxygenFuelBlockBuildPrice.TabIndex = 25
        Me.lblOxygenFuelBlockBuildPrice.Text = "0.00"
        Me.lblOxygenFuelBlockBuildPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbFuelBlockOptions
        '
        Me.gbFuelBlockOptions.Controls.Add(Me.rbtnBuyBlocks)
        Me.gbFuelBlockOptions.Controls.Add(Me.rbtnBuildBlocks)
        Me.gbFuelBlockOptions.Controls.Add(Me.chkAutoUpdateFuelPrice)
        Me.gbFuelBlockOptions.Location = New System.Drawing.Point(444, 173)
        Me.gbFuelBlockOptions.Name = "gbFuelBlockOptions"
        Me.gbFuelBlockOptions.Size = New System.Drawing.Size(125, 64)
        Me.gbFuelBlockOptions.TabIndex = 5
        Me.gbFuelBlockOptions.TabStop = False
        Me.gbFuelBlockOptions.Text = "Options"
        '
        'rbtnBuyBlocks
        '
        Me.rbtnBuyBlocks.AutoSize = True
        Me.rbtnBuyBlocks.Location = New System.Drawing.Point(15, 18)
        Me.rbtnBuyBlocks.Name = "rbtnBuyBlocks"
        Me.rbtnBuyBlocks.Size = New System.Drawing.Size(78, 17)
        Me.rbtnBuyBlocks.TabIndex = 0
        Me.rbtnBuyBlocks.TabStop = True
        Me.rbtnBuyBlocks.Text = "Buy Blocks"
        Me.rbtnBuyBlocks.UseVisualStyleBackColor = True
        '
        'rbtnBuildBlocks
        '
        Me.rbtnBuildBlocks.AutoSize = True
        Me.rbtnBuildBlocks.Location = New System.Drawing.Point(15, 39)
        Me.rbtnBuildBlocks.Name = "rbtnBuildBlocks"
        Me.rbtnBuildBlocks.Size = New System.Drawing.Size(83, 17)
        Me.rbtnBuildBlocks.TabIndex = 1
        Me.rbtnBuildBlocks.TabStop = True
        Me.rbtnBuildBlocks.Text = "Build Blocks"
        Me.rbtnBuildBlocks.UseVisualStyleBackColor = True
        '
        'chkAutoUpdateFuelPrice
        '
        Me.chkAutoUpdateFuelPrice.AutoSize = True
        Me.chkAutoUpdateFuelPrice.Location = New System.Drawing.Point(15, 63)
        Me.chkAutoUpdateFuelPrice.Name = "chkAutoUpdateFuelPrice"
        Me.chkAutoUpdateFuelPrice.Size = New System.Drawing.Size(86, 17)
        Me.chkAutoUpdateFuelPrice.TabIndex = 2
        Me.chkAutoUpdateFuelPrice.Text = "Auto Update"
        Me.chkAutoUpdateFuelPrice.UseVisualStyleBackColor = True
        Me.chkAutoUpdateFuelPrice.Visible = False
        '
        'btnSavePrices
        '
        Me.btnSavePrices.Location = New System.Drawing.Point(444, 57)
        Me.btnSavePrices.Name = "btnSavePrices"
        Me.btnSavePrices.Size = New System.Drawing.Size(125, 34)
        Me.btnSavePrices.TabIndex = 3
        Me.btnSavePrices.Text = "Save Prices"
        Me.btnSavePrices.UseVisualStyleBackColor = True
        '
        'btnUpdateBuildCost
        '
        Me.btnUpdateBuildCost.Location = New System.Drawing.Point(444, 95)
        Me.btnUpdateBuildCost.Name = "btnUpdateBuildCost"
        Me.btnUpdateBuildCost.Size = New System.Drawing.Size(125, 34)
        Me.btnUpdateBuildCost.TabIndex = 2
        Me.btnUpdateBuildCost.Text = "Update Build Cost"
        Me.btnUpdateBuildCost.UseVisualStyleBackColor = True
        '
        'lblLauncherSlots
        '
        Me.lblLauncherSlots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLauncherSlots.Location = New System.Drawing.Point(6, 169)
        Me.lblLauncherSlots.Name = "lblLauncherSlots"
        Me.lblLauncherSlots.Size = New System.Drawing.Size(155, 18)
        Me.lblLauncherSlots.TabIndex = 8
        Me.lblLauncherSlots.Text = "Launcher Slots:"
        Me.lblLauncherSlots.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbFilterOptions
        '
        Me.gbFilterOptions.Controls.Add(Me.chkRigTypeViewDrilling)
        Me.gbFilterOptions.Controls.Add(Me.chkRigTypeViewReaction)
        Me.gbFilterOptions.Controls.Add(Me.chkRigTypeViewCombat)
        Me.gbFilterOptions.Controls.Add(Me.chkRigTypeViewReprocessing)
        Me.gbFilterOptions.Controls.Add(Me.chkRigTypeViewEngineering)
        Me.gbFilterOptions.Controls.Add(Me.chkItemViewTypeLow)
        Me.gbFilterOptions.Controls.Add(Me.chkItemViewTypeMedium)
        Me.gbFilterOptions.Controls.Add(Me.chkItemViewTypeHigh)
        Me.gbFilterOptions.Controls.Add(Me.chkItemViewTypeServices)
        Me.gbFilterOptions.Location = New System.Drawing.Point(6, 72)
        Me.gbFilterOptions.Name = "gbFilterOptions"
        Me.gbFilterOptions.Size = New System.Drawing.Size(342, 85)
        Me.gbFilterOptions.TabIndex = 6
        Me.gbFilterOptions.TabStop = False
        Me.gbFilterOptions.Text = "Item View Type:"
        '
        'chkRigTypeViewDrilling
        '
        Me.chkRigTypeViewDrilling.AutoSize = True
        Me.chkRigTypeViewDrilling.Location = New System.Drawing.Point(219, 62)
        Me.chkRigTypeViewDrilling.Name = "chkRigTypeViewDrilling"
        Me.chkRigTypeViewDrilling.Size = New System.Drawing.Size(81, 17)
        Me.chkRigTypeViewDrilling.TabIndex = 8
        Me.chkRigTypeViewDrilling.Text = "Drilling Rigs"
        Me.chkRigTypeViewDrilling.UseVisualStyleBackColor = True
        '
        'chkRigTypeViewReaction
        '
        Me.chkRigTypeViewReaction.AutoSize = True
        Me.chkRigTypeViewReaction.Location = New System.Drawing.Point(109, 62)
        Me.chkRigTypeViewReaction.Name = "chkRigTypeViewReaction"
        Me.chkRigTypeViewReaction.Size = New System.Drawing.Size(93, 17)
        Me.chkRigTypeViewReaction.TabIndex = 7
        Me.chkRigTypeViewReaction.Text = "Reaction Rigs"
        Me.chkRigTypeViewReaction.UseVisualStyleBackColor = True
        '
        'chkRigTypeViewCombat
        '
        Me.chkRigTypeViewCombat.AutoSize = True
        Me.chkRigTypeViewCombat.Location = New System.Drawing.Point(17, 62)
        Me.chkRigTypeViewCombat.Name = "chkRigTypeViewCombat"
        Me.chkRigTypeViewCombat.Size = New System.Drawing.Size(86, 17)
        Me.chkRigTypeViewCombat.TabIndex = 6
        Me.chkRigTypeViewCombat.Text = "Combat Rigs"
        Me.chkRigTypeViewCombat.UseVisualStyleBackColor = True
        '
        'chkRigTypeViewReprocessing
        '
        Me.chkRigTypeViewReprocessing.AutoSize = True
        Me.chkRigTypeViewReprocessing.Location = New System.Drawing.Point(219, 40)
        Me.chkRigTypeViewReprocessing.Name = "chkRigTypeViewReprocessing"
        Me.chkRigTypeViewReprocessing.Size = New System.Drawing.Size(115, 17)
        Me.chkRigTypeViewReprocessing.TabIndex = 5
        Me.chkRigTypeViewReprocessing.Text = "Reprocessing Rigs"
        Me.chkRigTypeViewReprocessing.UseVisualStyleBackColor = True
        '
        'chkRigTypeViewEngineering
        '
        Me.chkRigTypeViewEngineering.AutoSize = True
        Me.chkRigTypeViewEngineering.Location = New System.Drawing.Point(109, 40)
        Me.chkRigTypeViewEngineering.Name = "chkRigTypeViewEngineering"
        Me.chkRigTypeViewEngineering.Size = New System.Drawing.Size(106, 17)
        Me.chkRigTypeViewEngineering.TabIndex = 4
        Me.chkRigTypeViewEngineering.Text = "Engineering Rigs"
        Me.chkRigTypeViewEngineering.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeLow
        '
        Me.chkItemViewTypeLow.AutoSize = True
        Me.chkItemViewTypeLow.Location = New System.Drawing.Point(219, 18)
        Me.chkItemViewTypeLow.Name = "chkItemViewTypeLow"
        Me.chkItemViewTypeLow.Size = New System.Drawing.Size(72, 17)
        Me.chkItemViewTypeLow.TabIndex = 2
        Me.chkItemViewTypeLow.Text = "Low Slots"
        Me.chkItemViewTypeLow.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeMedium
        '
        Me.chkItemViewTypeMedium.AutoSize = True
        Me.chkItemViewTypeMedium.Location = New System.Drawing.Point(109, 18)
        Me.chkItemViewTypeMedium.Name = "chkItemViewTypeMedium"
        Me.chkItemViewTypeMedium.Size = New System.Drawing.Size(89, 17)
        Me.chkItemViewTypeMedium.TabIndex = 1
        Me.chkItemViewTypeMedium.Text = "Medium Slots"
        Me.chkItemViewTypeMedium.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeHigh
        '
        Me.chkItemViewTypeHigh.AutoSize = True
        Me.chkItemViewTypeHigh.Location = New System.Drawing.Point(17, 18)
        Me.chkItemViewTypeHigh.Name = "chkItemViewTypeHigh"
        Me.chkItemViewTypeHigh.Size = New System.Drawing.Size(74, 17)
        Me.chkItemViewTypeHigh.TabIndex = 0
        Me.chkItemViewTypeHigh.Text = "High Slots"
        Me.chkItemViewTypeHigh.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeServices
        '
        Me.chkItemViewTypeServices.AutoSize = True
        Me.chkItemViewTypeServices.Location = New System.Drawing.Point(17, 40)
        Me.chkItemViewTypeServices.Name = "chkItemViewTypeServices"
        Me.chkItemViewTypeServices.Size = New System.Drawing.Size(67, 17)
        Me.chkItemViewTypeServices.TabIndex = 3
        Me.chkItemViewTypeServices.Text = "Services"
        Me.chkItemViewTypeServices.UseVisualStyleBackColor = True
        '
        'gbStatsandOptions
        '
        Me.gbStatsandOptions.Controls.Add(Me.chkIncludeFuelCosts)
        Me.gbStatsandOptions.Controls.Add(Me.gbOptions)
        Me.gbStatsandOptions.Controls.Add(Me.lblLauncherSlots)
        Me.gbStatsandOptions.Controls.Add(Me.gbIncludeFuelBlocks)
        Me.gbStatsandOptions.Controls.Add(Me.lblCapacitor)
        Me.gbStatsandOptions.Controls.Add(Me.lblCapacitor1)
        Me.gbStatsandOptions.Controls.Add(Me.lblCalibration)
        Me.gbStatsandOptions.Controls.Add(Me.lblCalibration1)
        Me.gbStatsandOptions.Controls.Add(Me.lblCPU)
        Me.gbStatsandOptions.Controls.Add(Me.lblPowerGrid)
        Me.gbStatsandOptions.Controls.Add(Me.lblCPU1)
        Me.gbStatsandOptions.Controls.Add(Me.lblPowerGrid1)
        Me.gbStatsandOptions.Location = New System.Drawing.Point(959, 18)
        Me.gbStatsandOptions.Name = "gbStatsandOptions"
        Me.gbStatsandOptions.Size = New System.Drawing.Size(167, 589)
        Me.gbStatsandOptions.TabIndex = 10
        Me.gbStatsandOptions.TabStop = False
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.lblSystemSecurity)
        Me.gbOptions.Controls.Add(Me.chkNullSec)
        Me.gbOptions.Controls.Add(Me.chkLowSec)
        Me.gbOptions.Controls.Add(Me.chkHighSec)
        Me.gbOptions.Location = New System.Drawing.Point(6, 405)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(155, 63)
        Me.gbOptions.TabIndex = 11
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options"
        '
        'lblSystemSecurity
        '
        Me.lblSystemSecurity.AutoSize = True
        Me.lblSystemSecurity.Location = New System.Drawing.Point(3, 18)
        Me.lblSystemSecurity.Name = "lblSystemSecurity"
        Me.lblSystemSecurity.Size = New System.Drawing.Size(85, 13)
        Me.lblSystemSecurity.TabIndex = 0
        Me.lblSystemSecurity.Text = "System Security:"
        '
        'chkNullSec
        '
        Me.chkNullSec.AutoSize = True
        Me.chkNullSec.Location = New System.Drawing.Point(103, 34)
        Me.chkNullSec.Name = "chkNullSec"
        Me.chkNullSec.Size = New System.Drawing.Size(44, 17)
        Me.chkNullSec.TabIndex = 3
        Me.chkNullSec.Text = "Null"
        Me.chkNullSec.UseVisualStyleBackColor = True
        '
        'chkLowSec
        '
        Me.chkLowSec.AutoSize = True
        Me.chkLowSec.Location = New System.Drawing.Point(56, 34)
        Me.chkLowSec.Name = "chkLowSec"
        Me.chkLowSec.Size = New System.Drawing.Size(46, 17)
        Me.chkLowSec.TabIndex = 2
        Me.chkLowSec.Text = "Low"
        Me.chkLowSec.UseVisualStyleBackColor = True
        '
        'chkHighSec
        '
        Me.chkHighSec.AutoSize = True
        Me.chkHighSec.Location = New System.Drawing.Point(7, 34)
        Me.chkHighSec.Name = "chkHighSec"
        Me.chkHighSec.Size = New System.Drawing.Size(48, 17)
        Me.chkHighSec.TabIndex = 1
        Me.chkHighSec.Text = "High"
        Me.chkHighSec.UseVisualStyleBackColor = True
        '
        'gbIncludeFuelBlocks
        '
        Me.gbIncludeFuelBlocks.Controls.Add(Me.rbtnOxygenFuelBlock)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.rbtnNitrogenFuelBlock)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.rbtnHydrogenFuelBlock)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.rbtnHeliumFuelBlock)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.lblServiceModuleOnlineAmt)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.lblOnlineAmt)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.lblFuelBPH)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.lblServiceModuleBPH)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.lblServiceModuleFCPH)
        Me.gbIncludeFuelBlocks.Controls.Add(Me.lblFuelCost)
        Me.gbIncludeFuelBlocks.Enabled = False
        Me.gbIncludeFuelBlocks.Location = New System.Drawing.Point(6, 195)
        Me.gbIncludeFuelBlocks.Name = "gbIncludeFuelBlocks"
        Me.gbIncludeFuelBlocks.Size = New System.Drawing.Size(155, 204)
        Me.gbIncludeFuelBlocks.TabIndex = 10
        Me.gbIncludeFuelBlocks.TabStop = False
        '
        'rbtnOxygenFuelBlock
        '
        Me.rbtnOxygenFuelBlock.AutoSize = True
        Me.rbtnOxygenFuelBlock.Location = New System.Drawing.Point(10, 180)
        Me.rbtnOxygenFuelBlock.Name = "rbtnOxygenFuelBlock"
        Me.rbtnOxygenFuelBlock.Size = New System.Drawing.Size(114, 17)
        Me.rbtnOxygenFuelBlock.TabIndex = 9
        Me.rbtnOxygenFuelBlock.TabStop = True
        Me.rbtnOxygenFuelBlock.Text = "Oxygen Fuel Block"
        Me.rbtnOxygenFuelBlock.UseVisualStyleBackColor = True
        '
        'rbtnNitrogenFuelBlock
        '
        Me.rbtnNitrogenFuelBlock.AutoSize = True
        Me.rbtnNitrogenFuelBlock.Location = New System.Drawing.Point(10, 161)
        Me.rbtnNitrogenFuelBlock.Name = "rbtnNitrogenFuelBlock"
        Me.rbtnNitrogenFuelBlock.Size = New System.Drawing.Size(118, 17)
        Me.rbtnNitrogenFuelBlock.TabIndex = 8
        Me.rbtnNitrogenFuelBlock.TabStop = True
        Me.rbtnNitrogenFuelBlock.Text = "Nitrogen Fuel Block"
        Me.rbtnNitrogenFuelBlock.UseVisualStyleBackColor = True
        '
        'rbtnHydrogenFuelBlock
        '
        Me.rbtnHydrogenFuelBlock.AutoSize = True
        Me.rbtnHydrogenFuelBlock.Location = New System.Drawing.Point(10, 142)
        Me.rbtnHydrogenFuelBlock.Name = "rbtnHydrogenFuelBlock"
        Me.rbtnHydrogenFuelBlock.Size = New System.Drawing.Size(124, 17)
        Me.rbtnHydrogenFuelBlock.TabIndex = 7
        Me.rbtnHydrogenFuelBlock.TabStop = True
        Me.rbtnHydrogenFuelBlock.Text = "Hydrogen Fuel Block"
        Me.rbtnHydrogenFuelBlock.UseVisualStyleBackColor = True
        '
        'rbtnHeliumFuelBlock
        '
        Me.rbtnHeliumFuelBlock.AutoSize = True
        Me.rbtnHeliumFuelBlock.Location = New System.Drawing.Point(10, 123)
        Me.rbtnHeliumFuelBlock.Name = "rbtnHeliumFuelBlock"
        Me.rbtnHeliumFuelBlock.Size = New System.Drawing.Size(110, 17)
        Me.rbtnHeliumFuelBlock.TabIndex = 6
        Me.rbtnHeliumFuelBlock.TabStop = True
        Me.rbtnHeliumFuelBlock.Text = "Helium Fuel Block"
        Me.rbtnHeliumFuelBlock.UseVisualStyleBackColor = True
        '
        'lblServiceModuleOnlineAmt
        '
        Me.lblServiceModuleOnlineAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServiceModuleOnlineAmt.Location = New System.Drawing.Point(7, 101)
        Me.lblServiceModuleOnlineAmt.Name = "lblServiceModuleOnlineAmt"
        Me.lblServiceModuleOnlineAmt.Size = New System.Drawing.Size(140, 16)
        Me.lblServiceModuleOnlineAmt.TabIndex = 5
        Me.lblServiceModuleOnlineAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOnlineAmt
        '
        Me.lblOnlineAmt.Location = New System.Drawing.Point(7, 85)
        Me.lblOnlineAmt.Name = "lblOnlineAmt"
        Me.lblOnlineAmt.Size = New System.Drawing.Size(140, 16)
        Me.lblOnlineAmt.TabIndex = 4
        Me.lblOnlineAmt.Text = "Online Fuel Amount"
        Me.lblOnlineAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFuelBPH
        '
        Me.lblFuelBPH.Location = New System.Drawing.Point(7, 21)
        Me.lblFuelBPH.Name = "lblFuelBPH"
        Me.lblFuelBPH.Size = New System.Drawing.Size(140, 16)
        Me.lblFuelBPH.TabIndex = 0
        Me.lblFuelBPH.Text = "Fuel Blocks per Hour"
        Me.lblFuelBPH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblServiceModuleBPH
        '
        Me.lblServiceModuleBPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServiceModuleBPH.Location = New System.Drawing.Point(7, 37)
        Me.lblServiceModuleBPH.Name = "lblServiceModuleBPH"
        Me.lblServiceModuleBPH.Size = New System.Drawing.Size(140, 16)
        Me.lblServiceModuleBPH.TabIndex = 1
        Me.lblServiceModuleBPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblServiceModuleFCPH
        '
        Me.lblServiceModuleFCPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServiceModuleFCPH.Location = New System.Drawing.Point(7, 69)
        Me.lblServiceModuleFCPH.Name = "lblServiceModuleFCPH"
        Me.lblServiceModuleFCPH.Size = New System.Drawing.Size(140, 16)
        Me.lblServiceModuleFCPH.TabIndex = 3
        Me.lblServiceModuleFCPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFuelCost
        '
        Me.lblFuelCost.Location = New System.Drawing.Point(7, 53)
        Me.lblFuelCost.Name = "lblFuelCost"
        Me.lblFuelCost.Size = New System.Drawing.Size(140, 16)
        Me.lblFuelCost.TabIndex = 2
        Me.lblFuelCost.Text = "Fuel Cost per Hour"
        Me.lblFuelCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCapacitor
        '
        Me.lblCapacitor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCapacitor.Location = New System.Drawing.Point(6, 140)
        Me.lblCapacitor.Name = "lblCapacitor"
        Me.lblCapacitor.Size = New System.Drawing.Size(155, 16)
        Me.lblCapacitor.TabIndex = 7
        Me.lblCapacitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCapacitor1
        '
        Me.lblCapacitor1.Location = New System.Drawing.Point(6, 124)
        Me.lblCapacitor1.Name = "lblCapacitor1"
        Me.lblCapacitor1.Size = New System.Drawing.Size(155, 16)
        Me.lblCapacitor1.TabIndex = 6
        Me.lblCapacitor1.Text = "Capacitor:"
        Me.lblCapacitor1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalibration
        '
        Me.lblCalibration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalibration.Location = New System.Drawing.Point(6, 101)
        Me.lblCalibration.Name = "lblCalibration"
        Me.lblCalibration.Size = New System.Drawing.Size(155, 16)
        Me.lblCalibration.TabIndex = 5
        Me.lblCalibration.Text = "400 / 400"
        Me.lblCalibration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCalibration1
        '
        Me.lblCalibration1.AutoSize = True
        Me.lblCalibration1.Location = New System.Drawing.Point(6, 87)
        Me.lblCalibration1.Name = "lblCalibration1"
        Me.lblCalibration1.Size = New System.Drawing.Size(59, 13)
        Me.lblCalibration1.TabIndex = 4
        Me.lblCalibration1.Text = "Calibration:"
        '
        'lblCPU
        '
        Me.lblCPU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCPU.Location = New System.Drawing.Point(6, 63)
        Me.lblCPU.Name = "lblCPU"
        Me.lblCPU.Size = New System.Drawing.Size(155, 16)
        Me.lblCPU.TabIndex = 3
        Me.lblCPU.Text = "15,000,000 / 15,000,000"
        Me.lblCPU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPowerGrid
        '
        Me.lblPowerGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPowerGrid.Location = New System.Drawing.Point(6, 25)
        Me.lblPowerGrid.Name = "lblPowerGrid"
        Me.lblPowerGrid.Size = New System.Drawing.Size(155, 16)
        Me.lblPowerGrid.TabIndex = 1
        Me.lblPowerGrid.Text = "15,000,000 / 15,000,000"
        Me.lblPowerGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCPU1
        '
        Me.lblCPU1.AutoSize = True
        Me.lblCPU1.Location = New System.Drawing.Point(6, 49)
        Me.lblCPU1.Name = "lblCPU1"
        Me.lblCPU1.Size = New System.Drawing.Size(32, 13)
        Me.lblCPU1.TabIndex = 2
        Me.lblCPU1.Text = "CPU:"
        '
        'lblPowerGrid1
        '
        Me.lblPowerGrid1.AutoSize = True
        Me.lblPowerGrid1.Location = New System.Drawing.Point(6, 11)
        Me.lblPowerGrid1.Name = "lblPowerGrid1"
        Me.lblPowerGrid1.Size = New System.Drawing.Size(62, 13)
        Me.lblPowerGrid1.TabIndex = 0
        Me.lblPowerGrid1.Text = "Power Grid:"
        '
        'btnSaveFitting
        '
        Me.btnSaveFitting.Location = New System.Drawing.Point(93, 35)
        Me.btnSaveFitting.Name = "btnSaveFitting"
        Me.btnSaveFitting.Size = New System.Drawing.Size(81, 30)
        Me.btnSaveFitting.TabIndex = 3
        Me.btnSaveFitting.Text = "Save Fitting"
        Me.btnSaveFitting.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(180, 35)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(81, 30)
        Me.btnSaveSettings.TabIndex = 4
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnCloseForm
        '
        Me.btnCloseForm.Location = New System.Drawing.Point(267, 35)
        Me.btnCloseForm.Name = "btnCloseForm"
        Me.btnCloseForm.Size = New System.Drawing.Size(81, 30)
        Me.btnCloseForm.TabIndex = 5
        Me.btnCloseForm.Text = "Close"
        Me.btnCloseForm.UseVisualStyleBackColor = True
        '
        'gbTextFilter
        '
        Me.gbTextFilter.Controls.Add(Me.btnItemFilter)
        Me.gbTextFilter.Controls.Add(Me.btnResetItemFilter)
        Me.gbTextFilter.Controls.Add(Me.txtItemFilter)
        Me.gbTextFilter.Location = New System.Drawing.Point(6, 159)
        Me.gbTextFilter.Name = "gbTextFilter"
        Me.gbTextFilter.Size = New System.Drawing.Size(342, 45)
        Me.gbTextFilter.TabIndex = 7
        Me.gbTextFilter.TabStop = False
        Me.gbTextFilter.Text = "Text Search:"
        '
        'btnItemFilter
        '
        Me.btnItemFilter.Location = New System.Drawing.Point(252, 17)
        Me.btnItemFilter.Name = "btnItemFilter"
        Me.btnItemFilter.Size = New System.Drawing.Size(39, 21)
        Me.btnItemFilter.TabIndex = 1
        Me.btnItemFilter.Text = "Filter"
        Me.btnItemFilter.UseVisualStyleBackColor = True
        '
        'btnResetItemFilter
        '
        Me.btnResetItemFilter.Location = New System.Drawing.Point(297, 17)
        Me.btnResetItemFilter.Name = "btnResetItemFilter"
        Me.btnResetItemFilter.Size = New System.Drawing.Size(39, 21)
        Me.btnResetItemFilter.TabIndex = 2
        Me.btnResetItemFilter.Text = "Clear"
        Me.btnResetItemFilter.UseVisualStyleBackColor = True
        '
        'txtItemFilter
        '
        Me.txtItemFilter.Location = New System.Drawing.Point(6, 17)
        Me.txtItemFilter.Name = "txtItemFilter"
        Me.txtItemFilter.Size = New System.Drawing.Size(240, 20)
        Me.txtItemFilter.TabIndex = 0
        '
        'EventLog1
        '
        Me.EventLog1.SynchronizingObject = Me
        '
        'pbFloat
        '
        Me.pbFloat.BackColor = System.Drawing.Color.White
        Me.pbFloat.InitialImage = Nothing
        Me.pbFloat.Location = New System.Drawing.Point(254, 531)
        Me.pbFloat.Name = "pbFloat"
        Me.pbFloat.Size = New System.Drawing.Size(64, 64)
        Me.pbFloat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbFloat.TabIndex = 7
        Me.pbFloat.TabStop = False
        Me.pbFloat.Visible = False
        '
        'frmUpwellStructureFitting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1134, 611)
        Me.Controls.Add(Me.gbTextFilter)
        Me.Controls.Add(Me.btnCloseForm)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.pbFloat)
        Me.Controls.Add(Me.gbStatsandOptions)
        Me.Controls.Add(Me.gbFilterOptions)
        Me.Controls.Add(Me.tabUpwellStructure)
        Me.Controls.Add(Me.btnSaveFitting)
        Me.Controls.Add(Me.btnStripFitting)
        Me.Controls.Add(Me.cmbUpwellStructureName)
        Me.Controls.Add(Me.lblSelectedUpwellStructure)
        Me.Controls.Add(Me.ServiceModuleListView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmUpwellStructureFitting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upwell Structure Fitting"
        Me.tabUpwellStructure.ResumeLayout(False)
        Me.tabFitting.ResumeLayout(False)
        CType(Me.LowSlot3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RigSlot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RigSlot3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RigSlot1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceSlot1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceSlot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceSlot3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceSlot4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LowSlot1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceSlot5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceSlot6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HighSlot1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MidSlot7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StructurePicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabFuelandBonuses.ResumeLayout(False)
        Me.gbFacilityBonuses.ResumeLayout(False)
        Me.gbFuelBlocks.ResumeLayout(False)
        Me.gbFuelPrices.ResumeLayout(False)
        Me.gbFuelPrices.PerformLayout()
        CType(Me.picHeliumIsotopes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCoolant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picOxygen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNitrogenIsotopes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHydrogenIsotopes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEnrichedUranium, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picOxygenIsotopes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMechanicalParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRobotics, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStrontiumClathrates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLiquidOzone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHeavyWater, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFuelBlocks2.ResumeLayout(False)
        Me.gbFuelBlocks2.PerformLayout()
        CType(Me.picHeliumFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHydrogenFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNitrogenFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picOxygenFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFuelBlockOptions.ResumeLayout(False)
        Me.gbFuelBlockOptions.PerformLayout()
        Me.gbFilterOptions.ResumeLayout(False)
        Me.gbFilterOptions.PerformLayout()
        Me.gbStatsandOptions.ResumeLayout(False)
        Me.gbStatsandOptions.PerformLayout()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.gbIncludeFuelBlocks.ResumeLayout(False)
        Me.gbIncludeFuelBlocks.PerformLayout()
        Me.gbTextFilter.ResumeLayout(False)
        Me.gbTextFilter.PerformLayout()
        CType(Me.EventLog1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFloat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RigSlot1 As PictureBox
    Friend WithEvents HighSlot1 As PictureBox
    Friend WithEvents FittingImages As ImageList
    Friend WithEvents ServiceModuleListView As ListView
    Friend WithEvents StructurePicture As PictureBox
    Friend WithEvents MidSlot1 As PictureBox
    Friend WithEvents LowSlot1 As PictureBox
    Friend WithEvents ServiceSlot1 As PictureBox
    Friend WithEvents ServiceSlot2 As PictureBox
    Friend WithEvents ServiceSlot3 As PictureBox
    Friend WithEvents ServiceSlot4 As PictureBox
    Friend WithEvents ServiceSlot5 As PictureBox
    Friend WithEvents ServiceSlot6 As PictureBox
    Friend WithEvents HighSlot7 As PictureBox
    Friend WithEvents HighSlot3 As PictureBox
    Friend WithEvents HighSlot2 As PictureBox
    Friend WithEvents HighSlot4 As PictureBox
    Friend WithEvents HighSlot6 As PictureBox
    Friend WithEvents HighSlot5 As PictureBox
    Friend WithEvents HighSlot8 As PictureBox
    Friend WithEvents RigSlot3 As PictureBox
    Friend WithEvents RigSlot2 As PictureBox
    Friend WithEvents LowSlot5 As PictureBox
    Friend WithEvents LowSlot4 As PictureBox
    Friend WithEvents LowSlot8 As PictureBox
    Friend WithEvents LowSlot7 As PictureBox
    Friend WithEvents LowSlot2 As PictureBox
    Friend WithEvents LowSlot6 As PictureBox
    Friend WithEvents LowSlot3 As PictureBox
    Friend WithEvents MidSlot8 As PictureBox
    Friend WithEvents MidSlot7 As PictureBox
    Friend WithEvents MidSlot6 As PictureBox
    Friend WithEvents MidSlot5 As PictureBox
    Friend WithEvents MidSlot2 As PictureBox
    Friend WithEvents MidSlot4 As PictureBox
    Friend WithEvents MidSlot3 As PictureBox
    Friend WithEvents pbFloat As PictureBox
    Friend WithEvents lblSelectedUpwellStructure As Label
    Friend WithEvents cmbUpwellStructureName As ComboBox
    Friend WithEvents chkIncludeFuelCosts As CheckBox
    Friend WithEvents btnStripFitting As Button
    Friend WithEvents tabUpwellStructure As TabControl
    Friend WithEvents tabFitting As TabPage
    Friend WithEvents tabFuelandBonuses As TabPage
    Friend WithEvents gbFuelBlocks As GroupBox
    Friend WithEvents lblHeliumFuelBlockBuildPrice As Label
    Friend WithEvents txtHeliumFuelBlockBuyPrice As TextBox
    Friend WithEvents lblHeliumFuelBlockBuild As Label
    Friend WithEvents lblHeliumFuelBlockBuy As Label
    Friend WithEvents txtHeliumFuelBlockBPME As TextBox
    Friend WithEvents btnSavePrices As Button
    Friend WithEvents btnUpdateBuildCost As Button
    Friend WithEvents picHeliumFuelBlock As PictureBox
    Friend WithEvents lblHeliumFuelBlockBPME As Label
    Friend WithEvents rbtnBuildBlocks As RadioButton
    Friend WithEvents rbtnBuyBlocks As RadioButton
    Friend WithEvents gbFilterOptions As GroupBox
    Friend WithEvents chkItemViewTypeLow As CheckBox
    Friend WithEvents chkItemViewTypeMedium As CheckBox
    Friend WithEvents chkItemViewTypeHigh As CheckBox
    Friend WithEvents chkItemViewTypeServices As CheckBox
    Friend WithEvents gbStatsandOptions As GroupBox
    Friend WithEvents lblCPU As Label
    Friend WithEvents lblPowerGrid As Label
    Friend WithEvents lblCPU1 As Label
    Friend WithEvents lblPowerGrid1 As Label
    Friend WithEvents lblCalibration As Label
    Friend WithEvents lblCalibration1 As Label
    Friend WithEvents lblCapacitor As Label
    Friend WithEvents lblCapacitor1 As Label
    Friend WithEvents btnSaveFitting As Button
    Friend WithEvents btnSaveSettings As Button
    Friend WithEvents chkRigTypeViewCombat As CheckBox
    Friend WithEvents chkRigTypeViewReprocessing As CheckBox
    Friend WithEvents chkRigTypeViewEngineering As CheckBox
    Friend WithEvents lblServiceModuleFCPH As Label
    Friend WithEvents lblFuelCost As Label
    Friend WithEvents lblServiceModuleBPH As Label
    Friend WithEvents lblFuelBPH As Label
    Friend WithEvents chkAutoUpdateFuelPrice As CheckBox
    Friend WithEvents btnCloseForm As Button
    Friend WithEvents lblOnlineAmt As Label
    Friend WithEvents gbIncludeFuelBlocks As GroupBox
    Friend WithEvents lblServiceModuleOnlineAmt As Label
    Friend WithEvents rbtnOxygenFuelBlock As RadioButton
    Friend WithEvents rbtnNitrogenFuelBlock As RadioButton
    Friend WithEvents rbtnHydrogenFuelBlock As RadioButton
    Friend WithEvents rbtnHeliumFuelBlock As RadioButton
    Friend WithEvents lblOxygenFuelBlockBuildPrice As Label
    Friend WithEvents txtOxygenFuelBlockBuyPrice As TextBox
    Friend WithEvents lblOxygenFuelBlockBuild As Label
    Friend WithEvents lblOxygenFuelBlockBuy As Label
    Friend WithEvents txtOxygenFuelBlockBPME As TextBox
    Friend WithEvents picOxygenFuelBlock As PictureBox
    Friend WithEvents lblOxygenFuelBlockBPME As Label
    Friend WithEvents lblNitrogenFuelBlockBuildPrice As Label
    Friend WithEvents txtNitrogenFuelBlockBuyPrice As TextBox
    Friend WithEvents lblNitrogenFuelBlockBuild As Label
    Friend WithEvents lblNitrogenFuelBlockBuy As Label
    Friend WithEvents txtNitrogenFuelBlockBPME As TextBox
    Friend WithEvents lblNitrogenFuelBlockBPME As Label
    Friend WithEvents lblHydrogenFuelBlockBuildPrice As Label
    Friend WithEvents txtHydrogenFuelBlockBuyPrice As TextBox
    Friend WithEvents lblHydrogenFuelBlockBuild As Label
    Friend WithEvents lblHydrogenBlockBuy As Label
    Friend WithEvents txtHydrogenFuelBlockBPME As TextBox
    Friend WithEvents picHydrogenFuelBlock As PictureBox
    Friend WithEvents lblHydrogenFuelBlockBPME As Label
    Friend WithEvents gbFuelBlockOptions As GroupBox
    Friend WithEvents gbTextFilter As GroupBox
    Friend WithEvents btnItemFilter As Button
    Friend WithEvents btnResetItemFilter As Button
    Friend WithEvents txtItemFilter As TextBox
    Friend WithEvents lblLauncherSlots As Label
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents lblSystemSecurity As Label
    Friend WithEvents chkNullSec As CheckBox
    Friend WithEvents chkLowSec As CheckBox
    Friend WithEvents chkHighSec As CheckBox
    Friend WithEvents lstUpwellStructureBonuses As ListView
    Friend WithEvents gbFacilityBonuses As GroupBox
    Friend WithEvents gbFuelBlocks2 As GroupBox
    Friend WithEvents txtHeliumIsotopes As TextBox
    Friend WithEvents txtOxygen As TextBox
    Friend WithEvents picHeliumIsotopes As PictureBox
    Friend WithEvents lblOxygen As Label
    Friend WithEvents picCoolant As PictureBox
    Friend WithEvents picOxygen As PictureBox
    Friend WithEvents picNitrogenIsotopes As PictureBox
    Friend WithEvents txtRobotics As TextBox
    Friend WithEvents picEnrichedUranium As PictureBox
    Friend WithEvents lblRobotics As Label
    Friend WithEvents picMechanicalParts As PictureBox
    Friend WithEvents picRobotics As PictureBox
    Friend WithEvents picStrontiumClathrates As PictureBox
    Friend WithEvents txtOxygenIsotopes As TextBox
    Friend WithEvents picLiquidOzone As PictureBox
    Friend WithEvents txtHydrogenIsotopes As TextBox
    Friend WithEvents picHeavyWater As PictureBox
    Friend WithEvents lblOxygenIsotopes As Label
    Friend WithEvents lblHeliumIsotopes As Label
    Friend WithEvents lblHydrogenIsotopes As Label
    Friend WithEvents lblNitrogenIsotopes As Label
    Friend WithEvents picOxygenIsotopes As PictureBox
    Friend WithEvents lblMechanicalParts As Label
    Friend WithEvents picHydrogenIsotopes As PictureBox
    Friend WithEvents lblLiquidOzone As Label
    Friend WithEvents txtHeavyWater As TextBox
    Friend WithEvents lblCoolant As Label
    Friend WithEvents txtStrontiumClathrates As TextBox
    Friend WithEvents lblEnrichedUranium As Label
    Friend WithEvents txtEnrichedUranium As TextBox
    Friend WithEvents lblStrontiumClathrates As Label
    Friend WithEvents txtCoolant As TextBox
    Friend WithEvents lblHeavyWater As Label
    Friend WithEvents txtLiquidOzone As TextBox
    Friend WithEvents txtNitrogenIsotopes As TextBox
    Friend WithEvents txtMechanicalParts As TextBox
    Friend WithEvents picNitrogenFuelBlock As PictureBox
    Friend WithEvents btnSaveFuelBlockInfo As Button
    Friend WithEvents gbFuelPrices As GroupBox
    Friend WithEvents BonusAppliesTo As ColumnHeader
    Friend WithEvents Bonuses As ColumnHeader
    Friend WithEvents Source As ColumnHeader
    Friend WithEvents Activity As ColumnHeader
    Friend WithEvents chkRigTypeViewReaction As CheckBox
    Friend WithEvents chkRigTypeViewDrilling As CheckBox
    Friend WithEvents EventLog1 As EventLog
    Friend WithEvents btnBonusPopout As Button
    Friend WithEvents lblHydrogenFuelBlock As Label
    Friend WithEvents lblOxygenFuelBlock As Label
    Friend WithEvents lblHeliumFuelBlock As Label
    Friend WithEvents lblNitrogenFuelBlock As Label
    Friend WithEvents btnRefreshPrices As Button
    Friend WithEvents MainToolTip As ToolTip
End Class
