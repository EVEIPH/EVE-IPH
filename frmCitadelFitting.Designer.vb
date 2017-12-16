<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCitadelFitting
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("High Slots", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Mid Slots", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Low Slots", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Combat Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Reprocessing Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Engineering Rigs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCitadelFitting))
        Me.FittingImages = New System.Windows.Forms.ImageList(Me.components)
        Me.ServiceModuleListView = New System.Windows.Forms.ListView()
        Me.lblSelectedCitadel = New System.Windows.Forms.Label()
        Me.cmbCitadelName = New System.Windows.Forms.ComboBox()
        Me.chkIncludeFuelCosts = New System.Windows.Forms.CheckBox()
        Me.btnToggleAllPriceItems = New System.Windows.Forms.Button()
        Me.tabCitadel = New System.Windows.Forms.TabControl()
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
        Me.tabFuel = New System.Windows.Forms.TabPage()
        Me.gbPOSFuelPrices = New System.Windows.Forms.GroupBox()
        Me.txtCharters = New System.Windows.Forms.TextBox()
        Me.lblCharters = New System.Windows.Forms.Label()
        Me.btnPOSUpdateFuelPrices = New System.Windows.Forms.Button()
        Me.picPOSCharters = New System.Windows.Forms.PictureBox()
        Me.txtPOS1 = New System.Windows.Forms.TextBox()
        Me.txtPOS12 = New System.Windows.Forms.TextBox()
        Me.pictPOS1 = New System.Windows.Forms.PictureBox()
        Me.lblPOS12 = New System.Windows.Forms.Label()
        Me.pictPOS10 = New System.Windows.Forms.PictureBox()
        Me.pictPOS12 = New System.Windows.Forms.PictureBox()
        Me.pictPOS3 = New System.Windows.Forms.PictureBox()
        Me.txtPOS8 = New System.Windows.Forms.TextBox()
        Me.pictPOS11 = New System.Windows.Forms.PictureBox()
        Me.lblPOS8 = New System.Windows.Forms.Label()
        Me.pictPOS9 = New System.Windows.Forms.PictureBox()
        Me.pictPOS8 = New System.Windows.Forms.PictureBox()
        Me.pictPOS7 = New System.Windows.Forms.PictureBox()
        Me.txtPOS4 = New System.Windows.Forms.TextBox()
        Me.pictPOS6 = New System.Windows.Forms.PictureBox()
        Me.txtPOS2 = New System.Windows.Forms.TextBox()
        Me.pictPOS5 = New System.Windows.Forms.PictureBox()
        Me.lblPOS4 = New System.Windows.Forms.Label()
        Me.lblPOS1 = New System.Windows.Forms.Label()
        Me.lblPOS2 = New System.Windows.Forms.Label()
        Me.lblPOS3 = New System.Windows.Forms.Label()
        Me.pictPOS4 = New System.Windows.Forms.PictureBox()
        Me.lblPOS9 = New System.Windows.Forms.Label()
        Me.pictPOS2 = New System.Windows.Forms.PictureBox()
        Me.lblPOS6 = New System.Windows.Forms.Label()
        Me.txtPOS5 = New System.Windows.Forms.TextBox()
        Me.lblPOS10 = New System.Windows.Forms.Label()
        Me.txtPOS7 = New System.Windows.Forms.TextBox()
        Me.lblPOS11 = New System.Windows.Forms.Label()
        Me.txtPOS11 = New System.Windows.Forms.TextBox()
        Me.lblPOS7 = New System.Windows.Forms.Label()
        Me.txtPOS10 = New System.Windows.Forms.TextBox()
        Me.lblPOS5 = New System.Windows.Forms.Label()
        Me.txtPOS6 = New System.Windows.Forms.TextBox()
        Me.txtPOS3 = New System.Windows.Forms.TextBox()
        Me.txtPOS9 = New System.Windows.Forms.TextBox()
        Me.gbPOSFuelBlocks = New System.Windows.Forms.GroupBox()
        Me.gbFuelBlockOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnPOSBuyBlocks = New System.Windows.Forms.RadioButton()
        Me.rbtnPOSBuildBlocks = New System.Windows.Forms.RadioButton()
        Me.chkAutoUpdateFuelPrice = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.picMinmatarFuelBlock = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblPOSFuelBlockMinmatar = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.picGallenteFuelBlock = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPOSFuelBlockGallente = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.picCaldariFuelBlock = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPOSFuelBlockCaldari = New System.Windows.Forms.Label()
        Me.lblAmarrFuelBlockBuild = New System.Windows.Forms.Label()
        Me.txtAmarrFuelBlockBuy = New System.Windows.Forms.TextBox()
        Me.lblAmarrBlockBuild2 = New System.Windows.Forms.Label()
        Me.lblAmarrBlockBuy2 = New System.Windows.Forms.Label()
        Me.txtAmarrFuelBlockBPME = New System.Windows.Forms.TextBox()
        Me.btnPOSUpdateBlockPrice = New System.Windows.Forms.Button()
        Me.btnRefreshBlockData = New System.Windows.Forms.Button()
        Me.picAmarrFuelBlock = New System.Windows.Forms.PictureBox()
        Me.picPOSCaldariFuelBlock = New System.Windows.Forms.PictureBox()
        Me.lblAmarrFuelBlockBPME = New System.Windows.Forms.Label()
        Me.lblPOSFuelBlockAmarr = New System.Windows.Forms.Label()
        Me.picPOSMinmatarFuelBlock = New System.Windows.Forms.PictureBox()
        Me.picPOSGallenteFuelBlock = New System.Windows.Forms.PictureBox()
        Me.lblLauncherSlots = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkRigTypeViewCombat = New System.Windows.Forms.CheckBox()
        Me.chkRigTypeViewReprocessing = New System.Windows.Forms.CheckBox()
        Me.chkRigTypeViewEngineering = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeLow = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeMedium = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeHigh = New System.Windows.Forms.CheckBox()
        Me.chkItemViewTypeServices = New System.Windows.Forms.CheckBox()
        Me.GBStats = New System.Windows.Forms.GroupBox()
        Me.gbBonuses = New System.Windows.Forms.GroupBox()
        Me.lstRigBonuses = New System.Windows.Forms.ListView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkNullSec = New System.Windows.Forms.CheckBox()
        Me.chkLowSec = New System.Windows.Forms.CheckBox()
        Me.chkHighSec = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtnMinmatarFuelBlock = New System.Windows.Forms.RadioButton()
        Me.rbtnGallenteFuelBlock = New System.Windows.Forms.RadioButton()
        Me.rbtnCaldariFuelBlock = New System.Windows.Forms.RadioButton()
        Me.rbtnAmarrFuelBlock = New System.Windows.Forms.RadioButton()
        Me.lblServiceModuleOnlineAmt = New System.Windows.Forms.Label()
        Me.lblOnlineAmt = New System.Windows.Forms.Label()
        Me.lblFuelBPH = New System.Windows.Forms.Label()
        Me.lblServiceModuleBPH = New System.Windows.Forms.Label()
        Me.lblServiceModuleFCPH = New System.Windows.Forms.Label()
        Me.lblFuelCost = New System.Windows.Forms.Label()
        Me.lblCapacitorValues = New System.Windows.Forms.Label()
        Me.lblCapacitorLabel = New System.Windows.Forms.Label()
        Me.lblCalibration = New System.Windows.Forms.Label()
        Me.Calibration1 = New System.Windows.Forms.Label()
        Me.lblCPU = New System.Windows.Forms.Label()
        Me.lblPowerGrid = New System.Windows.Forms.Label()
        Me.CPU1 = New System.Windows.Forms.Label()
        Me.PowerGrid1 = New System.Windows.Forms.Label()
        Me.btnSaveUpdatePrices = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pbFloat = New System.Windows.Forms.PictureBox()
        Me.btnCloseForm = New System.Windows.Forms.Button()
        Me.gbTextFilter = New System.Windows.Forms.GroupBox()
        Me.btnItemFilter = New System.Windows.Forms.Button()
        Me.btnResetItemFilter = New System.Windows.Forms.Button()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
        Me.tabCitadel.SuspendLayout()
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
        Me.tabFuel.SuspendLayout()
        Me.gbPOSFuelPrices.SuspendLayout()
        CType(Me.picPOSCharters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictPOS2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPOSFuelBlocks.SuspendLayout()
        Me.gbFuelBlockOptions.SuspendLayout()
        CType(Me.picMinmatarFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGallenteFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCaldariFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAmarrFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPOSCaldariFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPOSMinmatarFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPOSGallenteFuelBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GBStats.SuspendLayout()
        Me.gbBonuses.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pbFloat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTextFilter.SuspendLayout()
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
        ListViewGroup1.Header = "Services"
        ListViewGroup1.Name = "ServiceSlots"
        ListViewGroup1.Tag = "ServiceSlot"
        ListViewGroup2.Header = "High Slots"
        ListViewGroup2.Name = "HighSlots"
        ListViewGroup2.Tag = "HighSlot"
        ListViewGroup3.Header = "Mid Slots"
        ListViewGroup3.Name = "MidSlots"
        ListViewGroup3.Tag = "MidSlots"
        ListViewGroup4.Header = "Low Slots"
        ListViewGroup4.Name = "LowSlots"
        ListViewGroup4.Tag = "LowSlot"
        ListViewGroup5.Header = "Combat Rigs"
        ListViewGroup5.Name = "CombatRigs"
        ListViewGroup5.Tag = "RigSlot"
        ListViewGroup6.Header = "Reprocessing Rigs"
        ListViewGroup6.Name = "ReprocessingRigs"
        ListViewGroup6.Tag = "RigSlot"
        ListViewGroup7.Header = "Engineering Rigs"
        ListViewGroup7.Name = "EngineeringRigs"
        ListViewGroup7.Tag = "RigSlot"
        Me.ServiceModuleListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5, ListViewGroup6, ListViewGroup7})
        Me.ServiceModuleListView.HoverSelection = True
        Me.ServiceModuleListView.LargeImageList = Me.FittingImages
        Me.ServiceModuleListView.Location = New System.Drawing.Point(6, 200)
        Me.ServiceModuleListView.MultiSelect = False
        Me.ServiceModuleListView.Name = "ServiceModuleListView"
        Me.ServiceModuleListView.Size = New System.Drawing.Size(342, 407)
        Me.ServiceModuleListView.TabIndex = 8
        Me.ServiceModuleListView.UseCompatibleStateImageBehavior = False
        '
        'lblSelectedCitadel
        '
        Me.lblSelectedCitadel.AutoSize = True
        Me.lblSelectedCitadel.Location = New System.Drawing.Point(6, 12)
        Me.lblSelectedCitadel.Name = "lblSelectedCitadel"
        Me.lblSelectedCitadel.Size = New System.Drawing.Size(87, 13)
        Me.lblSelectedCitadel.TabIndex = 42
        Me.lblSelectedCitadel.Text = "Selected Citadel:"
        '
        'cmbCitadelName
        '
        Me.cmbCitadelName.FormattingEnabled = True
        Me.cmbCitadelName.Location = New System.Drawing.Point(99, 8)
        Me.cmbCitadelName.Name = "cmbCitadelName"
        Me.cmbCitadelName.Size = New System.Drawing.Size(249, 21)
        Me.cmbCitadelName.TabIndex = 43
        '
        'chkIncludeFuelCosts
        '
        Me.chkIncludeFuelCosts.AutoSize = True
        Me.chkIncludeFuelCosts.Location = New System.Drawing.Point(4, 0)
        Me.chkIncludeFuelCosts.Name = "chkIncludeFuelCosts"
        Me.chkIncludeFuelCosts.Size = New System.Drawing.Size(113, 17)
        Me.chkIncludeFuelCosts.TabIndex = 44
        Me.chkIncludeFuelCosts.Text = "Include Fuel Costs"
        Me.chkIncludeFuelCosts.UseVisualStyleBackColor = True
        '
        'btnToggleAllPriceItems
        '
        Me.btnToggleAllPriceItems.Location = New System.Drawing.Point(6, 35)
        Me.btnToggleAllPriceItems.Name = "btnToggleAllPriceItems"
        Me.btnToggleAllPriceItems.Size = New System.Drawing.Size(81, 30)
        Me.btnToggleAllPriceItems.TabIndex = 46
        Me.btnToggleAllPriceItems.Text = "Strip Fitting"
        Me.btnToggleAllPriceItems.UseVisualStyleBackColor = True
        '
        'tabCitadel
        '
        Me.tabCitadel.Controls.Add(Me.tabFitting)
        Me.tabCitadel.Controls.Add(Me.tabFuel)
        Me.tabCitadel.Location = New System.Drawing.Point(354, 4)
        Me.tabCitadel.Name = "tabCitadel"
        Me.tabCitadel.SelectedIndex = 0
        Me.tabCitadel.Size = New System.Drawing.Size(603, 603)
        Me.tabCitadel.TabIndex = 49
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
        'tabFuel
        '
        Me.tabFuel.Controls.Add(Me.gbPOSFuelPrices)
        Me.tabFuel.Controls.Add(Me.gbPOSFuelBlocks)
        Me.tabFuel.Location = New System.Drawing.Point(4, 22)
        Me.tabFuel.Name = "tabFuel"
        Me.tabFuel.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFuel.Size = New System.Drawing.Size(595, 577)
        Me.tabFuel.TabIndex = 1
        Me.tabFuel.Text = "Fuel Settings"
        Me.tabFuel.UseVisualStyleBackColor = True
        '
        'gbPOSFuelPrices
        '
        Me.gbPOSFuelPrices.Controls.Add(Me.txtCharters)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblCharters)
        Me.gbPOSFuelPrices.Controls.Add(Me.btnPOSUpdateFuelPrices)
        Me.gbPOSFuelPrices.Controls.Add(Me.picPOSCharters)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS1)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS12)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS1)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS12)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS10)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS12)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS3)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS8)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS11)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS8)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS9)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS8)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS7)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS4)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS6)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS2)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS5)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS4)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS1)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS2)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS3)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS4)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS9)
        Me.gbPOSFuelPrices.Controls.Add(Me.pictPOS2)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS6)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS5)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS10)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS7)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS11)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS11)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS7)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS10)
        Me.gbPOSFuelPrices.Controls.Add(Me.lblPOS5)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS6)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS3)
        Me.gbPOSFuelPrices.Controls.Add(Me.txtPOS9)
        Me.gbPOSFuelPrices.Location = New System.Drawing.Point(6, 216)
        Me.gbPOSFuelPrices.Name = "gbPOSFuelPrices"
        Me.gbPOSFuelPrices.Size = New System.Drawing.Size(583, 210)
        Me.gbPOSFuelPrices.TabIndex = 225
        Me.gbPOSFuelPrices.TabStop = False
        Me.gbPOSFuelPrices.Text = "Fuel Block Material Prices"
        '
        'txtCharters
        '
        Me.txtCharters.Location = New System.Drawing.Point(470, 75)
        Me.txtCharters.Name = "txtCharters"
        Me.txtCharters.Size = New System.Drawing.Size(101, 20)
        Me.txtCharters.TabIndex = 223
        Me.txtCharters.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCharters
        '
        Me.lblCharters.Location = New System.Drawing.Point(470, 60)
        Me.lblCharters.Name = "lblCharters"
        Me.lblCharters.Size = New System.Drawing.Size(101, 13)
        Me.lblCharters.TabIndex = 225
        Me.lblCharters.Text = "Charters"
        Me.lblCharters.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnPOSUpdateFuelPrices
        '
        Me.btnPOSUpdateFuelPrices.Location = New System.Drawing.Point(229, 161)
        Me.btnPOSUpdateFuelPrices.Name = "btnPOSUpdateFuelPrices"
        Me.btnPOSUpdateFuelPrices.Size = New System.Drawing.Size(124, 34)
        Me.btnPOSUpdateFuelPrices.TabIndex = 226
        Me.btnPOSUpdateFuelPrices.Text = "Update Prices"
        Me.btnPOSUpdateFuelPrices.UseVisualStyleBackColor = True
        '
        'picPOSCharters
        '
        Me.picPOSCharters.BackColor = System.Drawing.Color.Transparent
        Me.picPOSCharters.Image = CType(resources.GetObject("picPOSCharters.Image"), System.Drawing.Image)
        Me.picPOSCharters.Location = New System.Drawing.Point(434, 69)
        Me.picPOSCharters.Name = "picPOSCharters"
        Me.picPOSCharters.Size = New System.Drawing.Size(32, 32)
        Me.picPOSCharters.TabIndex = 224
        Me.picPOSCharters.TabStop = False
        '
        'txtPOS1
        '
        Me.txtPOS1.Location = New System.Drawing.Point(46, 32)
        Me.txtPOS1.Name = "txtPOS1"
        Me.txtPOS1.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS1.TabIndex = 187
        Me.txtPOS1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPOS12
        '
        Me.txtPOS12.Location = New System.Drawing.Point(470, 118)
        Me.txtPOS12.Name = "txtPOS12"
        Me.txtPOS12.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS12.TabIndex = 220
        Me.txtPOS12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pictPOS1
        '
        Me.pictPOS1.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS1.Image = CType(resources.GetObject("pictPOS1.Image"), System.Drawing.Image)
        Me.pictPOS1.Location = New System.Drawing.Point(11, 26)
        Me.pictPOS1.Name = "pictPOS1"
        Me.pictPOS1.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS1.TabIndex = 198
        Me.pictPOS1.TabStop = False
        '
        'lblPOS12
        '
        Me.lblPOS12.Location = New System.Drawing.Point(470, 103)
        Me.lblPOS12.Name = "lblPOS12"
        Me.lblPOS12.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS12.TabIndex = 222
        Me.lblPOS12.Text = "Oxygen"
        Me.lblPOS12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pictPOS10
        '
        Me.pictPOS10.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS10.Image = CType(resources.GetObject("pictPOS10.Image"), System.Drawing.Image)
        Me.pictPOS10.Location = New System.Drawing.Point(293, 112)
        Me.pictPOS10.Name = "pictPOS10"
        Me.pictPOS10.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS10.TabIndex = 199
        Me.pictPOS10.TabStop = False
        '
        'pictPOS12
        '
        Me.pictPOS12.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS12.Image = CType(resources.GetObject("pictPOS12.Image"), System.Drawing.Image)
        Me.pictPOS12.Location = New System.Drawing.Point(434, 112)
        Me.pictPOS12.Name = "pictPOS12"
        Me.pictPOS12.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS12.TabIndex = 221
        Me.pictPOS12.TabStop = False
        '
        'pictPOS3
        '
        Me.pictPOS3.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS3.Image = CType(resources.GetObject("pictPOS3.Image"), System.Drawing.Image)
        Me.pictPOS3.Location = New System.Drawing.Point(11, 69)
        Me.pictPOS3.Name = "pictPOS3"
        Me.pictPOS3.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS3.TabIndex = 200
        Me.pictPOS3.TabStop = False
        '
        'txtPOS8
        '
        Me.txtPOS8.Location = New System.Drawing.Point(188, 118)
        Me.txtPOS8.Name = "txtPOS8"
        Me.txtPOS8.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS8.TabIndex = 194
        Me.txtPOS8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pictPOS11
        '
        Me.pictPOS11.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS11.Image = CType(resources.GetObject("pictPOS11.Image"), System.Drawing.Image)
        Me.pictPOS11.Location = New System.Drawing.Point(12, 155)
        Me.pictPOS11.Name = "pictPOS11"
        Me.pictPOS11.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS11.TabIndex = 201
        Me.pictPOS11.TabStop = False
        '
        'lblPOS8
        '
        Me.lblPOS8.Location = New System.Drawing.Point(188, 103)
        Me.lblPOS8.Name = "lblPOS8"
        Me.lblPOS8.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS8.TabIndex = 219
        Me.lblPOS8.Text = "Robotics"
        Me.lblPOS8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pictPOS9
        '
        Me.pictPOS9.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS9.Image = CType(resources.GetObject("pictPOS9.Image"), System.Drawing.Image)
        Me.pictPOS9.Location = New System.Drawing.Point(11, 112)
        Me.pictPOS9.Name = "pictPOS9"
        Me.pictPOS9.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS9.TabIndex = 202
        Me.pictPOS9.TabStop = False
        '
        'pictPOS8
        '
        Me.pictPOS8.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS8.Image = CType(resources.GetObject("pictPOS8.Image"), System.Drawing.Image)
        Me.pictPOS8.Location = New System.Drawing.Point(152, 112)
        Me.pictPOS8.Name = "pictPOS8"
        Me.pictPOS8.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS8.TabIndex = 218
        Me.pictPOS8.TabStop = False
        '
        'pictPOS7
        '
        Me.pictPOS7.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS7.Image = CType(resources.GetObject("pictPOS7.Image"), System.Drawing.Image)
        Me.pictPOS7.Location = New System.Drawing.Point(293, 69)
        Me.pictPOS7.Name = "pictPOS7"
        Me.pictPOS7.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS7.TabIndex = 203
        Me.pictPOS7.TabStop = False
        '
        'txtPOS4
        '
        Me.txtPOS4.Location = New System.Drawing.Point(188, 75)
        Me.txtPOS4.Name = "txtPOS4"
        Me.txtPOS4.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS4.TabIndex = 190
        Me.txtPOS4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pictPOS6
        '
        Me.pictPOS6.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS6.Image = CType(resources.GetObject("pictPOS6.Image"), System.Drawing.Image)
        Me.pictPOS6.Location = New System.Drawing.Point(434, 26)
        Me.pictPOS6.Name = "pictPOS6"
        Me.pictPOS6.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS6.TabIndex = 204
        Me.pictPOS6.TabStop = False
        '
        'txtPOS2
        '
        Me.txtPOS2.Location = New System.Drawing.Point(188, 32)
        Me.txtPOS2.Name = "txtPOS2"
        Me.txtPOS2.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS2.TabIndex = 188
        Me.txtPOS2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pictPOS5
        '
        Me.pictPOS5.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS5.Image = CType(resources.GetObject("pictPOS5.Image"), System.Drawing.Image)
        Me.pictPOS5.Location = New System.Drawing.Point(293, 26)
        Me.pictPOS5.Name = "pictPOS5"
        Me.pictPOS5.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS5.TabIndex = 205
        Me.pictPOS5.TabStop = False
        '
        'lblPOS4
        '
        Me.lblPOS4.Location = New System.Drawing.Point(188, 60)
        Me.lblPOS4.Name = "lblPOS4"
        Me.lblPOS4.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS4.TabIndex = 217
        Me.lblPOS4.Text = "Oxygen Isotopes"
        Me.lblPOS4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPOS1
        '
        Me.lblPOS1.Location = New System.Drawing.Point(46, 17)
        Me.lblPOS1.Name = "lblPOS1"
        Me.lblPOS1.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS1.TabIndex = 206
        Me.lblPOS1.Text = "Helium Isotopes"
        Me.lblPOS1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPOS2
        '
        Me.lblPOS2.Location = New System.Drawing.Point(188, 17)
        Me.lblPOS2.Name = "lblPOS2"
        Me.lblPOS2.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS2.TabIndex = 216
        Me.lblPOS2.Text = "Hydrogen Isotopes"
        Me.lblPOS2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPOS3
        '
        Me.lblPOS3.Location = New System.Drawing.Point(46, 60)
        Me.lblPOS3.Name = "lblPOS3"
        Me.lblPOS3.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS3.TabIndex = 207
        Me.lblPOS3.Text = "Nitrogen Isotopes"
        Me.lblPOS3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pictPOS4
        '
        Me.pictPOS4.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS4.Image = CType(resources.GetObject("pictPOS4.Image"), System.Drawing.Image)
        Me.pictPOS4.Location = New System.Drawing.Point(152, 69)
        Me.pictPOS4.Name = "pictPOS4"
        Me.pictPOS4.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS4.TabIndex = 215
        Me.pictPOS4.TabStop = False
        '
        'lblPOS9
        '
        Me.lblPOS9.Location = New System.Drawing.Point(46, 103)
        Me.lblPOS9.Name = "lblPOS9"
        Me.lblPOS9.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS9.TabIndex = 208
        Me.lblPOS9.Text = "Mechanical Parts"
        Me.lblPOS9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pictPOS2
        '
        Me.pictPOS2.BackColor = System.Drawing.Color.Transparent
        Me.pictPOS2.Image = CType(resources.GetObject("pictPOS2.Image"), System.Drawing.Image)
        Me.pictPOS2.Location = New System.Drawing.Point(152, 26)
        Me.pictPOS2.Name = "pictPOS2"
        Me.pictPOS2.Size = New System.Drawing.Size(32, 32)
        Me.pictPOS2.TabIndex = 214
        Me.pictPOS2.TabStop = False
        '
        'lblPOS6
        '
        Me.lblPOS6.Location = New System.Drawing.Point(470, 17)
        Me.lblPOS6.Name = "lblPOS6"
        Me.lblPOS6.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS6.TabIndex = 209
        Me.lblPOS6.Text = "Liquid Ozone"
        Me.lblPOS6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPOS5
        '
        Me.txtPOS5.Location = New System.Drawing.Point(331, 32)
        Me.txtPOS5.Name = "txtPOS5"
        Me.txtPOS5.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS5.TabIndex = 191
        Me.txtPOS5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPOS10
        '
        Me.lblPOS10.Location = New System.Drawing.Point(331, 103)
        Me.lblPOS10.Name = "lblPOS10"
        Me.lblPOS10.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS10.TabIndex = 210
        Me.lblPOS10.Text = "Coolant"
        Me.lblPOS10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPOS7
        '
        Me.txtPOS7.Location = New System.Drawing.Point(331, 75)
        Me.txtPOS7.Name = "txtPOS7"
        Me.txtPOS7.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS7.TabIndex = 193
        Me.txtPOS7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPOS11
        '
        Me.lblPOS11.Location = New System.Drawing.Point(46, 146)
        Me.lblPOS11.Name = "lblPOS11"
        Me.lblPOS11.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS11.TabIndex = 211
        Me.lblPOS11.Text = "Enriched Uranium"
        Me.lblPOS11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPOS11
        '
        Me.txtPOS11.Location = New System.Drawing.Point(46, 161)
        Me.txtPOS11.Name = "txtPOS11"
        Me.txtPOS11.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS11.TabIndex = 197
        Me.txtPOS11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPOS7
        '
        Me.lblPOS7.Location = New System.Drawing.Point(331, 60)
        Me.lblPOS7.Name = "lblPOS7"
        Me.lblPOS7.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS7.TabIndex = 212
        Me.lblPOS7.Text = "Strontium Clathrates"
        Me.lblPOS7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPOS10
        '
        Me.txtPOS10.Location = New System.Drawing.Point(331, 118)
        Me.txtPOS10.Name = "txtPOS10"
        Me.txtPOS10.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS10.TabIndex = 196
        Me.txtPOS10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPOS5
        '
        Me.lblPOS5.Location = New System.Drawing.Point(331, 17)
        Me.lblPOS5.Name = "lblPOS5"
        Me.lblPOS5.Size = New System.Drawing.Size(101, 13)
        Me.lblPOS5.TabIndex = 213
        Me.lblPOS5.Text = "Heavy Water"
        Me.lblPOS5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPOS6
        '
        Me.txtPOS6.Location = New System.Drawing.Point(470, 32)
        Me.txtPOS6.Name = "txtPOS6"
        Me.txtPOS6.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS6.TabIndex = 192
        Me.txtPOS6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPOS3
        '
        Me.txtPOS3.Location = New System.Drawing.Point(46, 75)
        Me.txtPOS3.Name = "txtPOS3"
        Me.txtPOS3.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS3.TabIndex = 189
        Me.txtPOS3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPOS9
        '
        Me.txtPOS9.Location = New System.Drawing.Point(46, 118)
        Me.txtPOS9.Name = "txtPOS9"
        Me.txtPOS9.Size = New System.Drawing.Size(101, 20)
        Me.txtPOS9.TabIndex = 195
        Me.txtPOS9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'gbPOSFuelBlocks
        '
        Me.gbPOSFuelBlocks.Controls.Add(Me.gbFuelBlockOptions)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label11)
        Me.gbPOSFuelBlocks.Controls.Add(Me.TextBox5)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label12)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label13)
        Me.gbPOSFuelBlocks.Controls.Add(Me.TextBox6)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picMinmatarFuelBlock)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label14)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblPOSFuelBlockMinmatar)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label6)
        Me.gbPOSFuelBlocks.Controls.Add(Me.TextBox3)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label7)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label8)
        Me.gbPOSFuelBlocks.Controls.Add(Me.TextBox4)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picGallenteFuelBlock)
        Me.gbPOSFuelBlocks.Controls.Add(Me.PictureBox6)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label9)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblPOSFuelBlockGallente)
        Me.gbPOSFuelBlocks.Controls.Add(Me.PictureBox7)
        Me.gbPOSFuelBlocks.Controls.Add(Me.PictureBox8)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label1)
        Me.gbPOSFuelBlocks.Controls.Add(Me.TextBox1)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label2)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label3)
        Me.gbPOSFuelBlocks.Controls.Add(Me.TextBox2)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picCaldariFuelBlock)
        Me.gbPOSFuelBlocks.Controls.Add(Me.Label4)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblPOSFuelBlockCaldari)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblAmarrFuelBlockBuild)
        Me.gbPOSFuelBlocks.Controls.Add(Me.txtAmarrFuelBlockBuy)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblAmarrBlockBuild2)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblAmarrBlockBuy2)
        Me.gbPOSFuelBlocks.Controls.Add(Me.txtAmarrFuelBlockBPME)
        Me.gbPOSFuelBlocks.Controls.Add(Me.btnPOSUpdateBlockPrice)
        Me.gbPOSFuelBlocks.Controls.Add(Me.btnRefreshBlockData)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picAmarrFuelBlock)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picPOSCaldariFuelBlock)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblAmarrFuelBlockBPME)
        Me.gbPOSFuelBlocks.Controls.Add(Me.lblPOSFuelBlockAmarr)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picPOSMinmatarFuelBlock)
        Me.gbPOSFuelBlocks.Controls.Add(Me.picPOSGallenteFuelBlock)
        Me.gbPOSFuelBlocks.Location = New System.Drawing.Point(6, 15)
        Me.gbPOSFuelBlocks.Name = "gbPOSFuelBlocks"
        Me.gbPOSFuelBlocks.Size = New System.Drawing.Size(583, 195)
        Me.gbPOSFuelBlocks.TabIndex = 227
        Me.gbPOSFuelBlocks.TabStop = False
        Me.gbPOSFuelBlocks.Text = "Fuel Blocks"
        '
        'gbFuelBlockOptions
        '
        Me.gbFuelBlockOptions.Controls.Add(Me.rbtnPOSBuyBlocks)
        Me.gbFuelBlockOptions.Controls.Add(Me.rbtnPOSBuildBlocks)
        Me.gbFuelBlockOptions.Controls.Add(Me.chkAutoUpdateFuelPrice)
        Me.gbFuelBlockOptions.Location = New System.Drawing.Point(447, 18)
        Me.gbFuelBlockOptions.Name = "gbFuelBlockOptions"
        Me.gbFuelBlockOptions.Size = New System.Drawing.Size(124, 88)
        Me.gbFuelBlockOptions.TabIndex = 228
        Me.gbFuelBlockOptions.TabStop = False
        Me.gbFuelBlockOptions.Text = "Options"
        '
        'rbtnPOSBuyBlocks
        '
        Me.rbtnPOSBuyBlocks.AutoSize = True
        Me.rbtnPOSBuyBlocks.Location = New System.Drawing.Point(15, 18)
        Me.rbtnPOSBuyBlocks.Name = "rbtnPOSBuyBlocks"
        Me.rbtnPOSBuyBlocks.Size = New System.Drawing.Size(78, 17)
        Me.rbtnPOSBuyBlocks.TabIndex = 127
        Me.rbtnPOSBuyBlocks.TabStop = True
        Me.rbtnPOSBuyBlocks.Text = "Buy Blocks"
        Me.rbtnPOSBuyBlocks.UseVisualStyleBackColor = True
        '
        'rbtnPOSBuildBlocks
        '
        Me.rbtnPOSBuildBlocks.AutoSize = True
        Me.rbtnPOSBuildBlocks.Location = New System.Drawing.Point(15, 39)
        Me.rbtnPOSBuildBlocks.Name = "rbtnPOSBuildBlocks"
        Me.rbtnPOSBuildBlocks.Size = New System.Drawing.Size(83, 17)
        Me.rbtnPOSBuildBlocks.TabIndex = 128
        Me.rbtnPOSBuildBlocks.TabStop = True
        Me.rbtnPOSBuildBlocks.Text = "Build Blocks"
        Me.rbtnPOSBuildBlocks.UseVisualStyleBackColor = True
        '
        'chkAutoUpdateFuelPrice
        '
        Me.chkAutoUpdateFuelPrice.AutoSize = True
        Me.chkAutoUpdateFuelPrice.Location = New System.Drawing.Point(15, 63)
        Me.chkAutoUpdateFuelPrice.Name = "chkAutoUpdateFuelPrice"
        Me.chkAutoUpdateFuelPrice.Size = New System.Drawing.Size(86, 17)
        Me.chkAutoUpdateFuelPrice.TabIndex = 62
        Me.chkAutoUpdateFuelPrice.Text = "Auto Update"
        Me.chkAutoUpdateFuelPrice.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Location = New System.Drawing.Point(320, 143)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 20)
        Me.Label11.TabIndex = 276
        Me.Label11.Text = "0.00"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(320, 121)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(76, 20)
        Me.TextBox5.TabIndex = 268
        Me.TextBox5.Text = "0.00"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(285, 146)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 278
        Me.Label12.Text = "Build:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(290, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(28, 13)
        Me.Label13.TabIndex = 277
        Me.Label13.Text = "Buy:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(355, 165)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(41, 20)
        Me.TextBox6.TabIndex = 274
        Me.TextBox6.Text = "0"
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picMinmatarFuelBlock
        '
        Me.picMinmatarFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picMinmatarFuelBlock.Image = CType(resources.GetObject("picMinmatarFuelBlock.Image"), System.Drawing.Image)
        Me.picMinmatarFuelBlock.Location = New System.Drawing.Point(247, 121)
        Me.picMinmatarFuelBlock.Name = "picMinmatarFuelBlock"
        Me.picMinmatarFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picMinmatarFuelBlock.TabIndex = 269
        Me.picMinmatarFuelBlock.TabStop = False
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(252, 168)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(97, 15)
        Me.Label14.TabIndex = 275
        Me.Label14.Text = "Fuel Block BP ME:"
        '
        'lblPOSFuelBlockMinmatar
        '
        Me.lblPOSFuelBlockMinmatar.Location = New System.Drawing.Point(317, 107)
        Me.lblPOSFuelBlockMinmatar.Name = "lblPOSFuelBlockMinmatar"
        Me.lblPOSFuelBlockMinmatar.Size = New System.Drawing.Size(76, 13)
        Me.lblPOSFuelBlockMinmatar.TabIndex = 271
        Me.lblPOSFuelBlockMinmatar.Text = "Minmatar"
        Me.lblPOSFuelBlockMinmatar.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(112, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 20)
        Me.Label6.TabIndex = 265
        Me.Label6.Text = "0.00"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(112, 121)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(76, 20)
        Me.TextBox3.TabIndex = 257
        Me.TextBox3.Text = "0.00"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(77, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 267
        Me.Label7.Text = "Build:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(82, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 266
        Me.Label8.Text = "Buy:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(147, 165)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(41, 20)
        Me.TextBox4.TabIndex = 263
        Me.TextBox4.Text = "0"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picGallenteFuelBlock
        '
        Me.picGallenteFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picGallenteFuelBlock.Image = CType(resources.GetObject("picGallenteFuelBlock.Image"), System.Drawing.Image)
        Me.picGallenteFuelBlock.Location = New System.Drawing.Point(39, 121)
        Me.picGallenteFuelBlock.Name = "picGallenteFuelBlock"
        Me.picGallenteFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picGallenteFuelBlock.TabIndex = 258
        Me.picGallenteFuelBlock.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox6.Location = New System.Drawing.Point(39, 121)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox6.TabIndex = 259
        Me.PictureBox6.TabStop = False
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(44, 168)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 15)
        Me.Label9.TabIndex = 264
        Me.Label9.Text = "Fuel Block BP ME:"
        '
        'lblPOSFuelBlockGallente
        '
        Me.lblPOSFuelBlockGallente.Location = New System.Drawing.Point(109, 107)
        Me.lblPOSFuelBlockGallente.Name = "lblPOSFuelBlockGallente"
        Me.lblPOSFuelBlockGallente.Size = New System.Drawing.Size(76, 13)
        Me.lblPOSFuelBlockGallente.TabIndex = 260
        Me.lblPOSFuelBlockGallente.Text = "Gallente"
        Me.lblPOSFuelBlockGallente.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PictureBox7
        '
        Me.PictureBox7.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox7.Location = New System.Drawing.Point(39, 121)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox7.TabIndex = 262
        Me.PictureBox7.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox8.Location = New System.Drawing.Point(39, 121)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox8.TabIndex = 261
        Me.PictureBox8.TabStop = False
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(320, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 20)
        Me.Label1.TabIndex = 254
        Me.Label1.Text = "0.00"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(320, 31)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(76, 20)
        Me.TextBox1.TabIndex = 246
        Me.TextBox1.Text = "0.00"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(285, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 256
        Me.Label2.Text = "Build:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(290, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 255
        Me.Label3.Text = "Buy:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(355, 75)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(41, 20)
        Me.TextBox2.TabIndex = 252
        Me.TextBox2.Text = "0"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picCaldariFuelBlock
        '
        Me.picCaldariFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picCaldariFuelBlock.Image = CType(resources.GetObject("picCaldariFuelBlock.Image"), System.Drawing.Image)
        Me.picCaldariFuelBlock.Location = New System.Drawing.Point(247, 31)
        Me.picCaldariFuelBlock.Name = "picCaldariFuelBlock"
        Me.picCaldariFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picCaldariFuelBlock.TabIndex = 247
        Me.picCaldariFuelBlock.TabStop = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(252, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 15)
        Me.Label4.TabIndex = 253
        Me.Label4.Text = "Fuel Block BP ME:"
        '
        'lblPOSFuelBlockCaldari
        '
        Me.lblPOSFuelBlockCaldari.Location = New System.Drawing.Point(317, 17)
        Me.lblPOSFuelBlockCaldari.Name = "lblPOSFuelBlockCaldari"
        Me.lblPOSFuelBlockCaldari.Size = New System.Drawing.Size(76, 13)
        Me.lblPOSFuelBlockCaldari.TabIndex = 249
        Me.lblPOSFuelBlockCaldari.Text = "Caldari"
        Me.lblPOSFuelBlockCaldari.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAmarrFuelBlockBuild
        '
        Me.lblAmarrFuelBlockBuild.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAmarrFuelBlockBuild.Location = New System.Drawing.Point(113, 53)
        Me.lblAmarrFuelBlockBuild.Name = "lblAmarrFuelBlockBuild"
        Me.lblAmarrFuelBlockBuild.Size = New System.Drawing.Size(76, 20)
        Me.lblAmarrFuelBlockBuild.TabIndex = 243
        Me.lblAmarrFuelBlockBuild.Text = "0.00"
        Me.lblAmarrFuelBlockBuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmarrFuelBlockBuy
        '
        Me.txtAmarrFuelBlockBuy.Location = New System.Drawing.Point(113, 31)
        Me.txtAmarrFuelBlockBuy.Name = "txtAmarrFuelBlockBuy"
        Me.txtAmarrFuelBlockBuy.Size = New System.Drawing.Size(76, 20)
        Me.txtAmarrFuelBlockBuy.TabIndex = 220
        Me.txtAmarrFuelBlockBuy.Text = "0.00"
        Me.txtAmarrFuelBlockBuy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAmarrBlockBuild2
        '
        Me.lblAmarrBlockBuild2.AutoSize = True
        Me.lblAmarrBlockBuild2.Location = New System.Drawing.Point(78, 56)
        Me.lblAmarrBlockBuild2.Name = "lblAmarrBlockBuild2"
        Me.lblAmarrBlockBuild2.Size = New System.Drawing.Size(33, 13)
        Me.lblAmarrBlockBuild2.TabIndex = 245
        Me.lblAmarrBlockBuild2.Text = "Build:"
        Me.lblAmarrBlockBuild2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAmarrBlockBuy2
        '
        Me.lblAmarrBlockBuy2.AutoSize = True
        Me.lblAmarrBlockBuy2.Location = New System.Drawing.Point(83, 35)
        Me.lblAmarrBlockBuy2.Name = "lblAmarrBlockBuy2"
        Me.lblAmarrBlockBuy2.Size = New System.Drawing.Size(28, 13)
        Me.lblAmarrBlockBuy2.TabIndex = 244
        Me.lblAmarrBlockBuy2.Text = "Buy:"
        Me.lblAmarrBlockBuy2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtAmarrFuelBlockBPME
        '
        Me.txtAmarrFuelBlockBPME.Location = New System.Drawing.Point(148, 75)
        Me.txtAmarrFuelBlockBPME.Name = "txtAmarrFuelBlockBPME"
        Me.txtAmarrFuelBlockBPME.Size = New System.Drawing.Size(41, 20)
        Me.txtAmarrFuelBlockBPME.TabIndex = 230
        Me.txtAmarrFuelBlockBPME.Text = "0"
        Me.txtAmarrFuelBlockBPME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnPOSUpdateBlockPrice
        '
        Me.btnPOSUpdateBlockPrice.Location = New System.Drawing.Point(447, 150)
        Me.btnPOSUpdateBlockPrice.Name = "btnPOSUpdateBlockPrice"
        Me.btnPOSUpdateBlockPrice.Size = New System.Drawing.Size(124, 34)
        Me.btnPOSUpdateBlockPrice.TabIndex = 233
        Me.btnPOSUpdateBlockPrice.Text = "Update Block Price"
        Me.btnPOSUpdateBlockPrice.UseVisualStyleBackColor = True
        '
        'btnRefreshBlockData
        '
        Me.btnRefreshBlockData.Location = New System.Drawing.Point(447, 110)
        Me.btnRefreshBlockData.Name = "btnRefreshBlockData"
        Me.btnRefreshBlockData.Size = New System.Drawing.Size(124, 34)
        Me.btnRefreshBlockData.TabIndex = 232
        Me.btnRefreshBlockData.Text = "Refresh"
        Me.btnRefreshBlockData.UseVisualStyleBackColor = True
        '
        'picAmarrFuelBlock
        '
        Me.picAmarrFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picAmarrFuelBlock.Image = CType(resources.GetObject("picAmarrFuelBlock.Image"), System.Drawing.Image)
        Me.picAmarrFuelBlock.Location = New System.Drawing.Point(39, 31)
        Me.picAmarrFuelBlock.Name = "picAmarrFuelBlock"
        Me.picAmarrFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picAmarrFuelBlock.TabIndex = 222
        Me.picAmarrFuelBlock.TabStop = False
        '
        'picPOSCaldariFuelBlock
        '
        Me.picPOSCaldariFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picPOSCaldariFuelBlock.Image = CType(resources.GetObject("picPOSCaldariFuelBlock.Image"), System.Drawing.Image)
        Me.picPOSCaldariFuelBlock.Location = New System.Drawing.Point(40, 31)
        Me.picPOSCaldariFuelBlock.Name = "picPOSCaldariFuelBlock"
        Me.picPOSCaldariFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picPOSCaldariFuelBlock.TabIndex = 223
        Me.picPOSCaldariFuelBlock.TabStop = False
        '
        'lblAmarrFuelBlockBPME
        '
        Me.lblAmarrFuelBlockBPME.Location = New System.Drawing.Point(45, 78)
        Me.lblAmarrFuelBlockBPME.Name = "lblAmarrFuelBlockBPME"
        Me.lblAmarrFuelBlockBPME.Size = New System.Drawing.Size(97, 15)
        Me.lblAmarrFuelBlockBPME.TabIndex = 231
        Me.lblAmarrFuelBlockBPME.Text = "Fuel Block BP ME:"
        '
        'lblPOSFuelBlockAmarr
        '
        Me.lblPOSFuelBlockAmarr.Location = New System.Drawing.Point(110, 17)
        Me.lblPOSFuelBlockAmarr.Name = "lblPOSFuelBlockAmarr"
        Me.lblPOSFuelBlockAmarr.Size = New System.Drawing.Size(76, 13)
        Me.lblPOSFuelBlockAmarr.TabIndex = 225
        Me.lblPOSFuelBlockAmarr.Text = "Amarr"
        Me.lblPOSFuelBlockAmarr.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'picPOSMinmatarFuelBlock
        '
        Me.picPOSMinmatarFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picPOSMinmatarFuelBlock.Image = CType(resources.GetObject("picPOSMinmatarFuelBlock.Image"), System.Drawing.Image)
        Me.picPOSMinmatarFuelBlock.Location = New System.Drawing.Point(40, 31)
        Me.picPOSMinmatarFuelBlock.Name = "picPOSMinmatarFuelBlock"
        Me.picPOSMinmatarFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picPOSMinmatarFuelBlock.TabIndex = 227
        Me.picPOSMinmatarFuelBlock.TabStop = False
        '
        'picPOSGallenteFuelBlock
        '
        Me.picPOSGallenteFuelBlock.BackColor = System.Drawing.Color.Transparent
        Me.picPOSGallenteFuelBlock.Image = CType(resources.GetObject("picPOSGallenteFuelBlock.Image"), System.Drawing.Image)
        Me.picPOSGallenteFuelBlock.Location = New System.Drawing.Point(40, 31)
        Me.picPOSGallenteFuelBlock.Name = "picPOSGallenteFuelBlock"
        Me.picPOSGallenteFuelBlock.Size = New System.Drawing.Size(32, 32)
        Me.picPOSGallenteFuelBlock.TabIndex = 226
        Me.picPOSGallenteFuelBlock.TabStop = False
        '
        'lblLauncherSlots
        '
        Me.lblLauncherSlots.AutoSize = True
        Me.lblLauncherSlots.Location = New System.Drawing.Point(6, 182)
        Me.lblLauncherSlots.Name = "lblLauncherSlots"
        Me.lblLauncherSlots.Size = New System.Drawing.Size(81, 13)
        Me.lblLauncherSlots.TabIndex = 63
        Me.lblLauncherSlots.Text = "Launcher Slots:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkRigTypeViewCombat)
        Me.GroupBox1.Controls.Add(Me.chkRigTypeViewReprocessing)
        Me.GroupBox1.Controls.Add(Me.chkRigTypeViewEngineering)
        Me.GroupBox1.Controls.Add(Me.chkItemViewTypeLow)
        Me.GroupBox1.Controls.Add(Me.chkItemViewTypeMedium)
        Me.GroupBox1.Controls.Add(Me.chkItemViewTypeHigh)
        Me.GroupBox1.Controls.Add(Me.chkItemViewTypeServices)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 70)
        Me.GroupBox1.TabIndex = 51
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item View Type:"
        '
        'chkRigTypeViewCombat
        '
        Me.chkRigTypeViewCombat.AutoSize = True
        Me.chkRigTypeViewCombat.Location = New System.Drawing.Point(250, 43)
        Me.chkRigTypeViewCombat.Name = "chkRigTypeViewCombat"
        Me.chkRigTypeViewCombat.Size = New System.Drawing.Size(86, 17)
        Me.chkRigTypeViewCombat.TabIndex = 61
        Me.chkRigTypeViewCombat.Text = "Combat Rigs"
        Me.chkRigTypeViewCombat.UseVisualStyleBackColor = True
        '
        'chkRigTypeViewReprocessing
        '
        Me.chkRigTypeViewReprocessing.AutoSize = True
        Me.chkRigTypeViewReprocessing.Location = New System.Drawing.Point(17, 43)
        Me.chkRigTypeViewReprocessing.Name = "chkRigTypeViewReprocessing"
        Me.chkRigTypeViewReprocessing.Size = New System.Drawing.Size(115, 17)
        Me.chkRigTypeViewReprocessing.TabIndex = 60
        Me.chkRigTypeViewReprocessing.Text = "Reprocessing Rigs"
        Me.chkRigTypeViewReprocessing.UseVisualStyleBackColor = True
        '
        'chkRigTypeViewEngineering
        '
        Me.chkRigTypeViewEngineering.AutoSize = True
        Me.chkRigTypeViewEngineering.Location = New System.Drawing.Point(138, 43)
        Me.chkRigTypeViewEngineering.Name = "chkRigTypeViewEngineering"
        Me.chkRigTypeViewEngineering.Size = New System.Drawing.Size(106, 17)
        Me.chkRigTypeViewEngineering.TabIndex = 59
        Me.chkRigTypeViewEngineering.Text = "Engineering Rigs"
        Me.chkRigTypeViewEngineering.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeLow
        '
        Me.chkItemViewTypeLow.AutoSize = True
        Me.chkItemViewTypeLow.Location = New System.Drawing.Point(190, 20)
        Me.chkItemViewTypeLow.Name = "chkItemViewTypeLow"
        Me.chkItemViewTypeLow.Size = New System.Drawing.Size(72, 17)
        Me.chkItemViewTypeLow.TabIndex = 54
        Me.chkItemViewTypeLow.Text = "Low Slots"
        Me.chkItemViewTypeLow.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeMedium
        '
        Me.chkItemViewTypeMedium.AutoSize = True
        Me.chkItemViewTypeMedium.Location = New System.Drawing.Point(96, 20)
        Me.chkItemViewTypeMedium.Name = "chkItemViewTypeMedium"
        Me.chkItemViewTypeMedium.Size = New System.Drawing.Size(89, 17)
        Me.chkItemViewTypeMedium.TabIndex = 53
        Me.chkItemViewTypeMedium.Text = "Medium Slots"
        Me.chkItemViewTypeMedium.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeHigh
        '
        Me.chkItemViewTypeHigh.AutoSize = True
        Me.chkItemViewTypeHigh.Location = New System.Drawing.Point(17, 20)
        Me.chkItemViewTypeHigh.Name = "chkItemViewTypeHigh"
        Me.chkItemViewTypeHigh.Size = New System.Drawing.Size(74, 17)
        Me.chkItemViewTypeHigh.TabIndex = 52
        Me.chkItemViewTypeHigh.Text = "High Slots"
        Me.chkItemViewTypeHigh.UseVisualStyleBackColor = True
        '
        'chkItemViewTypeServices
        '
        Me.chkItemViewTypeServices.AutoSize = True
        Me.chkItemViewTypeServices.Location = New System.Drawing.Point(267, 20)
        Me.chkItemViewTypeServices.Name = "chkItemViewTypeServices"
        Me.chkItemViewTypeServices.Size = New System.Drawing.Size(67, 17)
        Me.chkItemViewTypeServices.TabIndex = 51
        Me.chkItemViewTypeServices.Text = "Services"
        Me.chkItemViewTypeServices.UseVisualStyleBackColor = True
        '
        'GBStats
        '
        Me.GBStats.Controls.Add(Me.gbBonuses)
        Me.GBStats.Controls.Add(Me.lblLauncherSlots)
        Me.GBStats.Controls.Add(Me.GroupBox2)
        Me.GBStats.Controls.Add(Me.lblCapacitorValues)
        Me.GBStats.Controls.Add(Me.lblCapacitorLabel)
        Me.GBStats.Controls.Add(Me.lblCalibration)
        Me.GBStats.Controls.Add(Me.Calibration1)
        Me.GBStats.Controls.Add(Me.lblCPU)
        Me.GBStats.Controls.Add(Me.lblPowerGrid)
        Me.GBStats.Controls.Add(Me.CPU1)
        Me.GBStats.Controls.Add(Me.PowerGrid1)
        Me.GBStats.Location = New System.Drawing.Point(959, 18)
        Me.GBStats.Name = "GBStats"
        Me.GBStats.Size = New System.Drawing.Size(167, 589)
        Me.GBStats.TabIndex = 59
        Me.GBStats.TabStop = False
        '
        'gbBonuses
        '
        Me.gbBonuses.Controls.Add(Me.lstRigBonuses)
        Me.gbBonuses.Controls.Add(Me.Label5)
        Me.gbBonuses.Controls.Add(Me.chkNullSec)
        Me.gbBonuses.Controls.Add(Me.chkLowSec)
        Me.gbBonuses.Controls.Add(Me.chkHighSec)
        Me.gbBonuses.Location = New System.Drawing.Point(6, 416)
        Me.gbBonuses.Name = "gbBonuses"
        Me.gbBonuses.Size = New System.Drawing.Size(155, 165)
        Me.gbBonuses.TabIndex = 79
        Me.gbBonuses.TabStop = False
        Me.gbBonuses.Text = "Bonuses"
        '
        'lstRigBonuses
        '
        Me.lstRigBonuses.FullRowSelect = True
        Me.lstRigBonuses.GridLines = True
        Me.lstRigBonuses.HideSelection = False
        Me.lstRigBonuses.Location = New System.Drawing.Point(4, 57)
        Me.lstRigBonuses.MultiSelect = False
        Me.lstRigBonuses.Name = "lstRigBonuses"
        Me.lstRigBonuses.Size = New System.Drawing.Size(147, 100)
        Me.lstRigBonuses.TabIndex = 82
        Me.lstRigBonuses.TabStop = False
        Me.lstRigBonuses.UseCompatibleStateImageBehavior = False
        Me.lstRigBonuses.View = System.Windows.Forms.View.Details
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "System Security:"
        '
        'chkNullSec
        '
        Me.chkNullSec.AutoSize = True
        Me.chkNullSec.Location = New System.Drawing.Point(103, 34)
        Me.chkNullSec.Name = "chkNullSec"
        Me.chkNullSec.Size = New System.Drawing.Size(44, 17)
        Me.chkNullSec.TabIndex = 80
        Me.chkNullSec.Text = "Null"
        Me.chkNullSec.UseVisualStyleBackColor = True
        '
        'chkLowSec
        '
        Me.chkLowSec.AutoSize = True
        Me.chkLowSec.Location = New System.Drawing.Point(56, 34)
        Me.chkLowSec.Name = "chkLowSec"
        Me.chkLowSec.Size = New System.Drawing.Size(46, 17)
        Me.chkLowSec.TabIndex = 79
        Me.chkLowSec.Text = "Low"
        Me.chkLowSec.UseVisualStyleBackColor = True
        '
        'chkHighSec
        '
        Me.chkHighSec.AutoSize = True
        Me.chkHighSec.Location = New System.Drawing.Point(7, 34)
        Me.chkHighSec.Name = "chkHighSec"
        Me.chkHighSec.Size = New System.Drawing.Size(48, 17)
        Me.chkHighSec.TabIndex = 78
        Me.chkHighSec.Text = "High"
        Me.chkHighSec.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtnMinmatarFuelBlock)
        Me.GroupBox2.Controls.Add(Me.rbtnGallenteFuelBlock)
        Me.GroupBox2.Controls.Add(Me.rbtnCaldariFuelBlock)
        Me.GroupBox2.Controls.Add(Me.rbtnAmarrFuelBlock)
        Me.GroupBox2.Controls.Add(Me.lblServiceModuleOnlineAmt)
        Me.GroupBox2.Controls.Add(Me.lblOnlineAmt)
        Me.GroupBox2.Controls.Add(Me.lblFuelBPH)
        Me.GroupBox2.Controls.Add(Me.lblServiceModuleBPH)
        Me.GroupBox2.Controls.Add(Me.lblServiceModuleFCPH)
        Me.GroupBox2.Controls.Add(Me.lblFuelCost)
        Me.GroupBox2.Controls.Add(Me.chkIncludeFuelCosts)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 206)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(155, 204)
        Me.GroupBox2.TabIndex = 73
        Me.GroupBox2.TabStop = False
        '
        'rbtnMinmatarFuelBlock
        '
        Me.rbtnMinmatarFuelBlock.AutoSize = True
        Me.rbtnMinmatarFuelBlock.Location = New System.Drawing.Point(9, 181)
        Me.rbtnMinmatarFuelBlock.Name = "rbtnMinmatarFuelBlock"
        Me.rbtnMinmatarFuelBlock.Size = New System.Drawing.Size(121, 17)
        Me.rbtnMinmatarFuelBlock.TabIndex = 77
        Me.rbtnMinmatarFuelBlock.TabStop = True
        Me.rbtnMinmatarFuelBlock.Text = "Minmatar Fuel Block"
        Me.rbtnMinmatarFuelBlock.UseVisualStyleBackColor = True
        '
        'rbtnGallenteFuelBlock
        '
        Me.rbtnGallenteFuelBlock.AutoSize = True
        Me.rbtnGallenteFuelBlock.Location = New System.Drawing.Point(9, 162)
        Me.rbtnGallenteFuelBlock.Name = "rbtnGallenteFuelBlock"
        Me.rbtnGallenteFuelBlock.Size = New System.Drawing.Size(117, 17)
        Me.rbtnGallenteFuelBlock.TabIndex = 76
        Me.rbtnGallenteFuelBlock.TabStop = True
        Me.rbtnGallenteFuelBlock.Text = "Gallente Fuel Block"
        Me.rbtnGallenteFuelBlock.UseVisualStyleBackColor = True
        '
        'rbtnCaldariFuelBlock
        '
        Me.rbtnCaldariFuelBlock.AutoSize = True
        Me.rbtnCaldariFuelBlock.Location = New System.Drawing.Point(9, 143)
        Me.rbtnCaldariFuelBlock.Name = "rbtnCaldariFuelBlock"
        Me.rbtnCaldariFuelBlock.Size = New System.Drawing.Size(110, 17)
        Me.rbtnCaldariFuelBlock.TabIndex = 75
        Me.rbtnCaldariFuelBlock.TabStop = True
        Me.rbtnCaldariFuelBlock.Text = "Caldari Fuel Block"
        Me.rbtnCaldariFuelBlock.UseVisualStyleBackColor = True
        '
        'rbtnAmarrFuelBlock
        '
        Me.rbtnAmarrFuelBlock.AutoSize = True
        Me.rbtnAmarrFuelBlock.Location = New System.Drawing.Point(9, 124)
        Me.rbtnAmarrFuelBlock.Name = "rbtnAmarrFuelBlock"
        Me.rbtnAmarrFuelBlock.Size = New System.Drawing.Size(105, 17)
        Me.rbtnAmarrFuelBlock.TabIndex = 74
        Me.rbtnAmarrFuelBlock.TabStop = True
        Me.rbtnAmarrFuelBlock.Text = "Amarr Fuel Block"
        Me.rbtnAmarrFuelBlock.UseVisualStyleBackColor = True
        '
        'lblServiceModuleOnlineAmt
        '
        Me.lblServiceModuleOnlineAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServiceModuleOnlineAmt.Location = New System.Drawing.Point(6, 102)
        Me.lblServiceModuleOnlineAmt.Name = "lblServiceModuleOnlineAmt"
        Me.lblServiceModuleOnlineAmt.Size = New System.Drawing.Size(140, 16)
        Me.lblServiceModuleOnlineAmt.TabIndex = 73
        Me.lblServiceModuleOnlineAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOnlineAmt
        '
        Me.lblOnlineAmt.Location = New System.Drawing.Point(6, 86)
        Me.lblOnlineAmt.Name = "lblOnlineAmt"
        Me.lblOnlineAmt.Size = New System.Drawing.Size(140, 16)
        Me.lblOnlineAmt.TabIndex = 72
        Me.lblOnlineAmt.Text = "Online Fuel Amount"
        Me.lblOnlineAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFuelBPH
        '
        Me.lblFuelBPH.Location = New System.Drawing.Point(6, 22)
        Me.lblFuelBPH.Name = "lblFuelBPH"
        Me.lblFuelBPH.Size = New System.Drawing.Size(140, 16)
        Me.lblFuelBPH.TabIndex = 68
        Me.lblFuelBPH.Text = "Fuel Blocks per Hour"
        Me.lblFuelBPH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblServiceModuleBPH
        '
        Me.lblServiceModuleBPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServiceModuleBPH.Location = New System.Drawing.Point(6, 38)
        Me.lblServiceModuleBPH.Name = "lblServiceModuleBPH"
        Me.lblServiceModuleBPH.Size = New System.Drawing.Size(140, 16)
        Me.lblServiceModuleBPH.TabIndex = 69
        Me.lblServiceModuleBPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblServiceModuleFCPH
        '
        Me.lblServiceModuleFCPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServiceModuleFCPH.Location = New System.Drawing.Point(6, 70)
        Me.lblServiceModuleFCPH.Name = "lblServiceModuleFCPH"
        Me.lblServiceModuleFCPH.Size = New System.Drawing.Size(140, 16)
        Me.lblServiceModuleFCPH.TabIndex = 71
        Me.lblServiceModuleFCPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFuelCost
        '
        Me.lblFuelCost.Location = New System.Drawing.Point(6, 54)
        Me.lblFuelCost.Name = "lblFuelCost"
        Me.lblFuelCost.Size = New System.Drawing.Size(140, 16)
        Me.lblFuelCost.TabIndex = 70
        Me.lblFuelCost.Text = "Fuel Cost per Hour"
        Me.lblFuelCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCapacitorValues
        '
        Me.lblCapacitorValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCapacitorValues.Location = New System.Drawing.Point(6, 153)
        Me.lblCapacitorValues.Name = "lblCapacitorValues"
        Me.lblCapacitorValues.Size = New System.Drawing.Size(155, 16)
        Me.lblCapacitorValues.TabIndex = 67
        Me.lblCapacitorValues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCapacitorLabel
        '
        Me.lblCapacitorLabel.Location = New System.Drawing.Point(6, 137)
        Me.lblCapacitorLabel.Name = "lblCapacitorLabel"
        Me.lblCapacitorLabel.Size = New System.Drawing.Size(155, 16)
        Me.lblCapacitorLabel.TabIndex = 66
        Me.lblCapacitorLabel.Text = "Capacitor:"
        Me.lblCapacitorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalibration
        '
        Me.lblCalibration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalibration.Location = New System.Drawing.Point(6, 114)
        Me.lblCalibration.Name = "lblCalibration"
        Me.lblCalibration.Size = New System.Drawing.Size(155, 16)
        Me.lblCalibration.TabIndex = 65
        Me.lblCalibration.Text = "400 / 400"
        Me.lblCalibration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Calibration1
        '
        Me.Calibration1.AutoSize = True
        Me.Calibration1.Location = New System.Drawing.Point(6, 100)
        Me.Calibration1.Name = "Calibration1"
        Me.Calibration1.Size = New System.Drawing.Size(59, 13)
        Me.Calibration1.TabIndex = 64
        Me.Calibration1.Text = "Calibration:"
        '
        'lblCPU
        '
        Me.lblCPU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCPU.Location = New System.Drawing.Point(6, 76)
        Me.lblCPU.Name = "lblCPU"
        Me.lblCPU.Size = New System.Drawing.Size(155, 16)
        Me.lblCPU.TabIndex = 63
        Me.lblCPU.Text = "15,000,000 / 15,000,000"
        Me.lblCPU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPowerGrid
        '
        Me.lblPowerGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPowerGrid.Location = New System.Drawing.Point(6, 38)
        Me.lblPowerGrid.Name = "lblPowerGrid"
        Me.lblPowerGrid.Size = New System.Drawing.Size(155, 16)
        Me.lblPowerGrid.TabIndex = 62
        Me.lblPowerGrid.Text = "15,000,000 / 15,000,000"
        Me.lblPowerGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CPU1
        '
        Me.CPU1.AutoSize = True
        Me.CPU1.Location = New System.Drawing.Point(6, 62)
        Me.CPU1.Name = "CPU1"
        Me.CPU1.Size = New System.Drawing.Size(32, 13)
        Me.CPU1.TabIndex = 61
        Me.CPU1.Text = "CPU:"
        '
        'PowerGrid1
        '
        Me.PowerGrid1.AutoSize = True
        Me.PowerGrid1.Location = New System.Drawing.Point(6, 24)
        Me.PowerGrid1.Name = "PowerGrid1"
        Me.PowerGrid1.Size = New System.Drawing.Size(62, 13)
        Me.PowerGrid1.TabIndex = 60
        Me.PowerGrid1.Text = "Power Grid:"
        '
        'btnSaveUpdatePrices
        '
        Me.btnSaveUpdatePrices.Location = New System.Drawing.Point(93, 35)
        Me.btnSaveUpdatePrices.Name = "btnSaveUpdatePrices"
        Me.btnSaveUpdatePrices.Size = New System.Drawing.Size(81, 30)
        Me.btnSaveUpdatePrices.TabIndex = 48
        Me.btnSaveUpdatePrices.Text = "Save Fitting"
        Me.btnSaveUpdatePrices.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(180, 35)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 30)
        Me.Button1.TabIndex = 60
        Me.Button1.Text = "Save Settings"
        Me.Button1.UseVisualStyleBackColor = True
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
        'btnCloseForm
        '
        Me.btnCloseForm.Location = New System.Drawing.Point(267, 35)
        Me.btnCloseForm.Name = "btnCloseForm"
        Me.btnCloseForm.Size = New System.Drawing.Size(81, 30)
        Me.btnCloseForm.TabIndex = 61
        Me.btnCloseForm.Text = "Close"
        Me.btnCloseForm.UseVisualStyleBackColor = True
        '
        'gbTextFilter
        '
        Me.gbTextFilter.Controls.Add(Me.btnItemFilter)
        Me.gbTextFilter.Controls.Add(Me.btnResetItemFilter)
        Me.gbTextFilter.Controls.Add(Me.txtItemFilter)
        Me.gbTextFilter.Location = New System.Drawing.Point(6, 148)
        Me.gbTextFilter.Name = "gbTextFilter"
        Me.gbTextFilter.Size = New System.Drawing.Size(342, 46)
        Me.gbTextFilter.TabIndex = 62
        Me.gbTextFilter.TabStop = False
        Me.gbTextFilter.Text = "Text Search:"
        '
        'btnItemFilter
        '
        Me.btnItemFilter.Location = New System.Drawing.Point(252, 17)
        Me.btnItemFilter.Name = "btnItemFilter"
        Me.btnItemFilter.Size = New System.Drawing.Size(39, 21)
        Me.btnItemFilter.TabIndex = 4
        Me.btnItemFilter.Text = "Filter"
        Me.btnItemFilter.UseVisualStyleBackColor = True
        '
        'btnResetItemFilter
        '
        Me.btnResetItemFilter.Location = New System.Drawing.Point(297, 17)
        Me.btnResetItemFilter.Name = "btnResetItemFilter"
        Me.btnResetItemFilter.Size = New System.Drawing.Size(39, 21)
        Me.btnResetItemFilter.TabIndex = 3
        Me.btnResetItemFilter.Text = "Clear"
        Me.btnResetItemFilter.UseVisualStyleBackColor = True
        '
        'txtItemFilter
        '
        Me.txtItemFilter.Location = New System.Drawing.Point(6, 17)
        Me.txtItemFilter.Name = "txtItemFilter"
        Me.txtItemFilter.Size = New System.Drawing.Size(240, 20)
        Me.txtItemFilter.TabIndex = 2
        '
        'frmCitadelFitting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1134, 611)
        Me.Controls.Add(Me.gbTextFilter)
        Me.Controls.Add(Me.btnCloseForm)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pbFloat)
        Me.Controls.Add(Me.GBStats)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tabCitadel)
        Me.Controls.Add(Me.btnSaveUpdatePrices)
        Me.Controls.Add(Me.btnToggleAllPriceItems)
        Me.Controls.Add(Me.cmbCitadelName)
        Me.Controls.Add(Me.lblSelectedCitadel)
        Me.Controls.Add(Me.ServiceModuleListView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCitadelFitting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Citadel Fitting"
        Me.tabCitadel.ResumeLayout(False)
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
        Me.tabFuel.ResumeLayout(False)
        Me.gbPOSFuelPrices.ResumeLayout(False)
        Me.gbPOSFuelPrices.PerformLayout()
        CType(Me.picPOSCharters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictPOS2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPOSFuelBlocks.ResumeLayout(False)
        Me.gbPOSFuelBlocks.PerformLayout()
        Me.gbFuelBlockOptions.ResumeLayout(False)
        Me.gbFuelBlockOptions.PerformLayout()
        CType(Me.picMinmatarFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGallenteFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCaldariFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAmarrFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPOSCaldariFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPOSMinmatarFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPOSGallenteFuelBlock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBStats.ResumeLayout(False)
        Me.GBStats.PerformLayout()
        Me.gbBonuses.ResumeLayout(False)
        Me.gbBonuses.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.pbFloat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTextFilter.ResumeLayout(False)
        Me.gbTextFilter.PerformLayout()
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
    Friend WithEvents lblSelectedCitadel As Label
    Friend WithEvents cmbCitadelName As ComboBox
    Friend WithEvents chkIncludeFuelCosts As CheckBox
    Friend WithEvents btnToggleAllPriceItems As Button
    Friend WithEvents tabCitadel As TabControl
    Friend WithEvents tabFitting As TabPage
    Friend WithEvents tabFuel As TabPage
    Friend WithEvents gbPOSFuelPrices As GroupBox
    Friend WithEvents txtCharters As TextBox
    Friend WithEvents lblCharters As Label
    Friend WithEvents btnPOSUpdateFuelPrices As Button
    Friend WithEvents picPOSCharters As PictureBox
    Friend WithEvents txtPOS1 As TextBox
    Friend WithEvents txtPOS12 As TextBox
    Friend WithEvents pictPOS1 As PictureBox
    Friend WithEvents lblPOS12 As Label
    Friend WithEvents pictPOS10 As PictureBox
    Friend WithEvents pictPOS12 As PictureBox
    Friend WithEvents pictPOS3 As PictureBox
    Friend WithEvents txtPOS8 As TextBox
    Friend WithEvents pictPOS11 As PictureBox
    Friend WithEvents lblPOS8 As Label
    Friend WithEvents pictPOS9 As PictureBox
    Friend WithEvents pictPOS8 As PictureBox
    Friend WithEvents pictPOS7 As PictureBox
    Friend WithEvents txtPOS4 As TextBox
    Friend WithEvents pictPOS6 As PictureBox
    Friend WithEvents txtPOS2 As TextBox
    Friend WithEvents pictPOS5 As PictureBox
    Friend WithEvents lblPOS4 As Label
    Friend WithEvents lblPOS1 As Label
    Friend WithEvents lblPOS2 As Label
    Friend WithEvents lblPOS3 As Label
    Friend WithEvents pictPOS4 As PictureBox
    Friend WithEvents lblPOS9 As Label
    Friend WithEvents pictPOS2 As PictureBox
    Friend WithEvents lblPOS6 As Label
    Friend WithEvents txtPOS5 As TextBox
    Friend WithEvents lblPOS10 As Label
    Friend WithEvents txtPOS7 As TextBox
    Friend WithEvents lblPOS11 As Label
    Friend WithEvents txtPOS11 As TextBox
    Friend WithEvents lblPOS7 As Label
    Friend WithEvents txtPOS10 As TextBox
    Friend WithEvents lblPOS5 As Label
    Friend WithEvents txtPOS6 As TextBox
    Friend WithEvents txtPOS3 As TextBox
    Friend WithEvents txtPOS9 As TextBox
    Friend WithEvents gbPOSFuelBlocks As GroupBox
    Friend WithEvents lblAmarrFuelBlockBuild As Label
    Friend WithEvents txtAmarrFuelBlockBuy As TextBox
    Friend WithEvents lblAmarrBlockBuild2 As Label
    Friend WithEvents lblAmarrBlockBuy2 As Label
    Friend WithEvents txtAmarrFuelBlockBPME As TextBox
    Friend WithEvents btnPOSUpdateBlockPrice As Button
    Friend WithEvents btnRefreshBlockData As Button
    Friend WithEvents picAmarrFuelBlock As PictureBox
    Friend WithEvents picPOSCaldariFuelBlock As PictureBox
    Friend WithEvents lblAmarrFuelBlockBPME As Label
    Friend WithEvents lblPOSFuelBlockAmarr As Label
    Friend WithEvents picPOSMinmatarFuelBlock As PictureBox
    Friend WithEvents picPOSGallenteFuelBlock As PictureBox
    Friend WithEvents rbtnPOSBuildBlocks As RadioButton
    Friend WithEvents rbtnPOSBuyBlocks As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkItemViewTypeLow As CheckBox
    Friend WithEvents chkItemViewTypeMedium As CheckBox
    Friend WithEvents chkItemViewTypeHigh As CheckBox
    Friend WithEvents chkItemViewTypeServices As CheckBox
    Friend WithEvents GBStats As GroupBox
    Friend WithEvents lblCPU As Label
    Friend WithEvents lblPowerGrid As Label
    Friend WithEvents CPU1 As Label
    Friend WithEvents PowerGrid1 As Label
    Friend WithEvents lblCalibration As Label
    Friend WithEvents Calibration1 As Label
    Friend WithEvents lblCapacitorValues As Label
    Friend WithEvents lblCapacitorLabel As Label
    Friend WithEvents btnSaveUpdatePrices As Button
    Friend WithEvents Button1 As Button
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
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblServiceModuleOnlineAmt As Label
    Friend WithEvents rbtnMinmatarFuelBlock As RadioButton
    Friend WithEvents rbtnGallenteFuelBlock As RadioButton
    Friend WithEvents rbtnCaldariFuelBlock As RadioButton
    Friend WithEvents rbtnAmarrFuelBlock As RadioButton
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents picMinmatarFuelBlock As PictureBox
    Friend WithEvents Label14 As Label
    Friend WithEvents lblPOSFuelBlockMinmatar As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents picGallenteFuelBlock As PictureBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblPOSFuelBlockGallente As Label
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents picCaldariFuelBlock As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblPOSFuelBlockCaldari As Label
    Friend WithEvents gbFuelBlockOptions As GroupBox
    Friend WithEvents gbTextFilter As GroupBox
    Friend WithEvents btnItemFilter As Button
    Friend WithEvents btnResetItemFilter As Button
    Friend WithEvents txtItemFilter As TextBox
    Friend WithEvents lblLauncherSlots As Label
    Friend WithEvents gbBonuses As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents chkNullSec As CheckBox
    Friend WithEvents chkLowSec As CheckBox
    Friend WithEvents chkHighSec As CheckBox
    Friend WithEvents lstRigBonuses As ListView
End Class
