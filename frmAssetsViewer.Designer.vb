<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetsViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetsViewer))
        Me.btnCloseAssets = New System.Windows.Forms.Button()
        Me.gbSortOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnSortQuantity = New System.Windows.Forms.RadioButton()
        Me.rbtnSortName = New System.Windows.Forms.RadioButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.gbAssetTypes = New System.Windows.Forms.GroupBox()
        Me.rbtnAllAssets = New System.Windows.Forms.RadioButton()
        Me.rbtnCorpAssets = New System.Windows.Forms.RadioButton()
        Me.rbtnPersonalAssets = New System.Windows.Forms.RadioButton()
        Me.btnResetItemFilter = New System.Windows.Forms.Button()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
        Me.lblItemFilter = New System.Windows.Forms.Label()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabAssetMain = New System.Windows.Forms.TabPage()
        Me.btnSaveMainSettings = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblReloadCorpAssets = New System.Windows.Forms.Label()
        Me.lblReloadPersonalAssets = New System.Windows.Forms.Label()
        Me.btnScanCorpAssets = New System.Windows.Forms.Button()
        Me.lblReloadCorpAssets2 = New System.Windows.Forms.Label()
        Me.btnScanPersonalAssets = New System.Windows.Forms.Button()
        Me.lblReloadPersonalAssets1 = New System.Windows.Forms.Label()
        Me.picSpaceFiller = New System.Windows.Forms.PictureBox()
        Me.tabSearchSettings = New System.Windows.Forms.TabPage()
        Me.rbtnBPMats = New System.Windows.Forms.RadioButton()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnSearchRefresh = New System.Windows.Forms.Button()
        Me.rbtnAllItems = New System.Windows.Forms.RadioButton()
        Me.gbManufacturedItems = New System.Windows.Forms.GroupBox()
        Me.gbItems = New System.Windows.Forms.GroupBox()
        Me.chkImplants = New System.Windows.Forms.CheckBox()
        Me.chkCelestials = New System.Windows.Forms.CheckBox()
        Me.chkTools = New System.Windows.Forms.CheckBox()
        Me.chkFuelBlocks = New System.Windows.Forms.CheckBox()
        Me.cmbPriceChargeTypes = New System.Windows.Forms.ComboBox()
        Me.chkDataInterfaces = New System.Windows.Forms.CheckBox()
        Me.chkCharges = New System.Windows.Forms.CheckBox()
        Me.chkStructures = New System.Windows.Forms.CheckBox()
        Me.chkBoosters = New System.Windows.Forms.CheckBox()
        Me.chkRigs = New System.Windows.Forms.CheckBox()
        Me.cmbPriceShipTypes = New System.Windows.Forms.ComboBox()
        Me.chkSubsystems = New System.Windows.Forms.CheckBox()
        Me.chkDrones = New System.Windows.Forms.CheckBox()
        Me.chkModules = New System.Windows.Forms.CheckBox()
        Me.chkShips = New System.Windows.Forms.CheckBox()
        Me.chkDeployables = New System.Windows.Forms.CheckBox()
        Me.gbPricesTech = New System.Windows.Forms.GroupBox()
        Me.chkItemsT4 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT6 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT5 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT3 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT2 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT1 = New System.Windows.Forms.CheckBox()
        Me.chkManufacturedItems = New System.Windows.Forms.CheckBox()
        Me.gbComponents = New System.Windows.Forms.GroupBox()
        Me.chkHybrid = New System.Windows.Forms.CheckBox()
        Me.chkComponents = New System.Windows.Forms.CheckBox()
        Me.chkCapitalComponents = New System.Windows.Forms.CheckBox()
        Me.chkCapT2Components = New System.Windows.Forms.CheckBox()
        Me.chkStationComponents = New System.Windows.Forms.CheckBox()
        Me.gbRawMaterials = New System.Windows.Forms.GroupBox()
        Me.chkBPCs = New System.Windows.Forms.CheckBox()
        Me.chkMisc = New System.Windows.Forms.CheckBox()
        Me.chkAsteroids = New System.Windows.Forms.CheckBox()
        Me.chkRawMaterialItems = New System.Windows.Forms.CheckBox()
        Me.chkPlanetary = New System.Windows.Forms.CheckBox()
        Me.chkBoosterMats = New System.Windows.Forms.CheckBox()
        Me.chkDroneComponents = New System.Windows.Forms.CheckBox()
        Me.chkMatsandCompounds = New System.Windows.Forms.CheckBox()
        Me.chkAdvancedMats = New System.Windows.Forms.CheckBox()
        Me.chkProcessedMats = New System.Windows.Forms.CheckBox()
        Me.chkRawMats = New System.Windows.Forms.CheckBox()
        Me.chkGas = New System.Windows.Forms.CheckBox()
        Me.chkPolymers = New System.Windows.Forms.CheckBox()
        Me.chkAncientRelics = New System.Windows.Forms.CheckBox()
        Me.chkAncientSalvage = New System.Windows.Forms.CheckBox()
        Me.chkSalvage = New System.Windows.Forms.CheckBox()
        Me.chkDecryptors = New System.Windows.Forms.CheckBox()
        Me.chkDatacores = New System.Windows.Forms.CheckBox()
        Me.chkIceProducts = New System.Windows.Forms.CheckBox()
        Me.chkMinerals = New System.Windows.Forms.CheckBox()
        Me.btnToggleExpand = New System.Windows.Forms.Button()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkRawMaterialPrices = New System.Windows.Forms.CheckBox()
        Me.btnToggleRetract = New System.Windows.Forms.Button()
        Me.chkToggle = New System.Windows.Forms.CheckBox()
        Me.btnCheckToggle = New System.Windows.Forms.Button()
        Me.AssetTree = New System.Windows.Forms.TreeView()
        Me.gbSortOptions.SuspendLayout()
        Me.gbAssetTypes.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tabAssetMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picSpaceFiller, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSearchSettings.SuspendLayout()
        Me.gbManufacturedItems.SuspendLayout()
        Me.gbItems.SuspendLayout()
        Me.gbPricesTech.SuspendLayout()
        Me.gbComponents.SuspendLayout()
        Me.gbRawMaterials.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCloseAssets
        '
        Me.btnCloseAssets.Location = New System.Drawing.Point(140, 274)
        Me.btnCloseAssets.Name = "btnCloseAssets"
        Me.btnCloseAssets.Size = New System.Drawing.Size(102, 27)
        Me.btnCloseAssets.TabIndex = 233
        Me.btnCloseAssets.Text = "Close"
        Me.btnCloseAssets.UseVisualStyleBackColor = True
        '
        'gbSortOptions
        '
        Me.gbSortOptions.Controls.Add(Me.rbtnSortQuantity)
        Me.gbSortOptions.Controls.Add(Me.rbtnSortName)
        Me.gbSortOptions.Location = New System.Drawing.Point(6, 66)
        Me.gbSortOptions.Name = "gbSortOptions"
        Me.gbSortOptions.Size = New System.Drawing.Size(171, 45)
        Me.gbSortOptions.TabIndex = 236
        Me.gbSortOptions.TabStop = False
        Me.gbSortOptions.Text = "Sort Items By:"
        '
        'rbtnSortQuantity
        '
        Me.rbtnSortQuantity.AutoSize = True
        Me.rbtnSortQuantity.Location = New System.Drawing.Point(86, 19)
        Me.rbtnSortQuantity.Name = "rbtnSortQuantity"
        Me.rbtnSortQuantity.Size = New System.Drawing.Size(64, 17)
        Me.rbtnSortQuantity.TabIndex = 70
        Me.rbtnSortQuantity.Text = "Quantity"
        Me.rbtnSortQuantity.UseVisualStyleBackColor = True
        '
        'rbtnSortName
        '
        Me.rbtnSortName.AutoSize = True
        Me.rbtnSortName.Location = New System.Drawing.Point(10, 19)
        Me.rbtnSortName.Name = "rbtnSortName"
        Me.rbtnSortName.Size = New System.Drawing.Size(53, 17)
        Me.rbtnSortName.TabIndex = 69
        Me.rbtnSortName.Text = "Name"
        Me.rbtnSortName.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'gbAssetTypes
        '
        Me.gbAssetTypes.Controls.Add(Me.rbtnAllAssets)
        Me.gbAssetTypes.Controls.Add(Me.rbtnCorpAssets)
        Me.gbAssetTypes.Controls.Add(Me.rbtnPersonalAssets)
        Me.gbAssetTypes.Location = New System.Drawing.Point(6, 6)
        Me.gbAssetTypes.Name = "gbAssetTypes"
        Me.gbAssetTypes.Size = New System.Drawing.Size(263, 45)
        Me.gbAssetTypes.TabIndex = 238
        Me.gbAssetTypes.TabStop = False
        Me.gbAssetTypes.Text = "Asset Types:"
        '
        'rbtnAllAssets
        '
        Me.rbtnAllAssets.AutoSize = True
        Me.rbtnAllAssets.Location = New System.Drawing.Point(10, 18)
        Me.rbtnAllAssets.Name = "rbtnAllAssets"
        Me.rbtnAllAssets.Size = New System.Drawing.Size(47, 17)
        Me.rbtnAllAssets.TabIndex = 71
        Me.rbtnAllAssets.Text = "Both"
        Me.rbtnAllAssets.UseVisualStyleBackColor = True
        '
        'rbtnCorpAssets
        '
        Me.rbtnCorpAssets.AutoSize = True
        Me.rbtnCorpAssets.Location = New System.Drawing.Point(172, 18)
        Me.rbtnCorpAssets.Name = "rbtnCorpAssets"
        Me.rbtnCorpAssets.Size = New System.Drawing.Size(79, 17)
        Me.rbtnCorpAssets.TabIndex = 70
        Me.rbtnCorpAssets.Text = "Corporation"
        Me.rbtnCorpAssets.UseVisualStyleBackColor = True
        '
        'rbtnPersonalAssets
        '
        Me.rbtnPersonalAssets.AutoSize = True
        Me.rbtnPersonalAssets.Location = New System.Drawing.Point(86, 18)
        Me.rbtnPersonalAssets.Name = "rbtnPersonalAssets"
        Me.rbtnPersonalAssets.Size = New System.Drawing.Size(66, 17)
        Me.rbtnPersonalAssets.TabIndex = 69
        Me.rbtnPersonalAssets.Text = "Personal"
        Me.rbtnPersonalAssets.UseVisualStyleBackColor = True
        '
        'btnResetItemFilter
        '
        Me.btnResetItemFilter.Location = New System.Drawing.Point(95, 62)
        Me.btnResetItemFilter.Name = "btnResetItemFilter"
        Me.btnResetItemFilter.Size = New System.Drawing.Size(86, 24)
        Me.btnResetItemFilter.TabIndex = 244
        Me.btnResetItemFilter.Text = "Reset"
        Me.btnResetItemFilter.UseVisualStyleBackColor = True
        '
        'txtItemFilter
        '
        Me.txtItemFilter.Location = New System.Drawing.Point(63, 36)
        Me.txtItemFilter.Name = "txtItemFilter"
        Me.txtItemFilter.Size = New System.Drawing.Size(208, 20)
        Me.txtItemFilter.TabIndex = 243
        '
        'lblItemFilter
        '
        Me.lblItemFilter.AutoSize = True
        Me.lblItemFilter.Location = New System.Drawing.Point(7, 39)
        Me.lblItemFilter.Name = "lblItemFilter"
        Me.lblItemFilter.Size = New System.Drawing.Size(55, 13)
        Me.lblItemFilter.TabIndex = 242
        Me.lblItemFilter.Text = "Item Filter:"
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabAssetMain)
        Me.tabMain.Controls.Add(Me.tabSearchSettings)
        Me.tabMain.Location = New System.Drawing.Point(353, 4)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(283, 662)
        Me.tabMain.TabIndex = 245
        '
        'tabAssetMain
        '
        Me.tabAssetMain.Controls.Add(Me.btnSaveMainSettings)
        Me.tabAssetMain.Controls.Add(Me.gbAssetTypes)
        Me.tabAssetMain.Controls.Add(Me.GroupBox1)
        Me.tabAssetMain.Controls.Add(Me.gbSortOptions)
        Me.tabAssetMain.Controls.Add(Me.picSpaceFiller)
        Me.tabAssetMain.Controls.Add(Me.btnCloseAssets)
        Me.tabAssetMain.Location = New System.Drawing.Point(4, 22)
        Me.tabAssetMain.Name = "tabAssetMain"
        Me.tabAssetMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAssetMain.Size = New System.Drawing.Size(275, 636)
        Me.tabAssetMain.TabIndex = 0
        Me.tabAssetMain.Text = "Main Search"
        Me.tabAssetMain.UseVisualStyleBackColor = True
        '
        'btnSaveMainSettings
        '
        Me.btnSaveMainSettings.Location = New System.Drawing.Point(32, 274)
        Me.btnSaveMainSettings.Name = "btnSaveMainSettings"
        Me.btnSaveMainSettings.Size = New System.Drawing.Size(102, 27)
        Me.btnSaveMainSettings.TabIndex = 241
        Me.btnSaveMainSettings.Text = "Save Settings"
        Me.btnSaveMainSettings.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblReloadCorpAssets)
        Me.GroupBox1.Controls.Add(Me.lblReloadPersonalAssets)
        Me.GroupBox1.Controls.Add(Me.btnScanCorpAssets)
        Me.GroupBox1.Controls.Add(Me.lblReloadCorpAssets2)
        Me.GroupBox1.Controls.Add(Me.btnScanPersonalAssets)
        Me.GroupBox1.Controls.Add(Me.lblReloadPersonalAssets1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 127)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(263, 139)
        Me.GroupBox1.TabIndex = 240
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Assets from API:"
        '
        'lblReloadCorpAssets
        '
        Me.lblReloadCorpAssets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReloadCorpAssets.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReloadCorpAssets.Location = New System.Drawing.Point(158, 79)
        Me.lblReloadCorpAssets.Name = "lblReloadCorpAssets"
        Me.lblReloadCorpAssets.Size = New System.Drawing.Size(94, 17)
        Me.lblReloadCorpAssets.TabIndex = 60
        Me.lblReloadCorpAssets.Text = "99h 99m 99s"
        Me.lblReloadCorpAssets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReloadPersonalAssets
        '
        Me.lblReloadPersonalAssets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReloadPersonalAssets.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReloadPersonalAssets.Location = New System.Drawing.Point(158, 21)
        Me.lblReloadPersonalAssets.Name = "lblReloadPersonalAssets"
        Me.lblReloadPersonalAssets.Size = New System.Drawing.Size(94, 17)
        Me.lblReloadPersonalAssets.TabIndex = 57
        Me.lblReloadPersonalAssets.Text = "99h 99m 99s"
        Me.lblReloadPersonalAssets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnScanCorpAssets
        '
        Me.btnScanCorpAssets.Location = New System.Drawing.Point(66, 103)
        Me.btnScanCorpAssets.Name = "btnScanCorpAssets"
        Me.btnScanCorpAssets.Size = New System.Drawing.Size(126, 26)
        Me.btnScanCorpAssets.TabIndex = 40
        Me.btnScanCorpAssets.Text = "Scan Corp Assets"
        Me.btnScanCorpAssets.UseVisualStyleBackColor = True
        '
        'lblReloadCorpAssets2
        '
        Me.lblReloadCorpAssets2.AutoSize = True
        Me.lblReloadCorpAssets2.Location = New System.Drawing.Point(6, 81)
        Me.lblReloadCorpAssets2.Name = "lblReloadCorpAssets2"
        Me.lblReloadCorpAssets2.Size = New System.Drawing.Size(136, 13)
        Me.lblReloadCorpAssets2.TabIndex = 59
        Me.lblReloadCorpAssets2.Text = "Can Reload Corp Assets in:"
        '
        'btnScanPersonalAssets
        '
        Me.btnScanPersonalAssets.Location = New System.Drawing.Point(66, 45)
        Me.btnScanPersonalAssets.Name = "btnScanPersonalAssets"
        Me.btnScanPersonalAssets.Size = New System.Drawing.Size(126, 26)
        Me.btnScanPersonalAssets.TabIndex = 39
        Me.btnScanPersonalAssets.Text = "Scan Personal Assets"
        Me.btnScanPersonalAssets.UseVisualStyleBackColor = True
        '
        'lblReloadPersonalAssets1
        '
        Me.lblReloadPersonalAssets1.AutoSize = True
        Me.lblReloadPersonalAssets1.Location = New System.Drawing.Point(4, 23)
        Me.lblReloadPersonalAssets1.Name = "lblReloadPersonalAssets1"
        Me.lblReloadPersonalAssets1.Size = New System.Drawing.Size(155, 13)
        Me.lblReloadPersonalAssets1.TabIndex = 58
        Me.lblReloadPersonalAssets1.Text = "Can Reload Personal Assets in:"
        '
        'picSpaceFiller
        '
        Me.picSpaceFiller.Image = CType(resources.GetObject("picSpaceFiller.Image"), System.Drawing.Image)
        Me.picSpaceFiller.Location = New System.Drawing.Point(193, 57)
        Me.picSpaceFiller.Name = "picSpaceFiller"
        Me.picSpaceFiller.Size = New System.Drawing.Size(64, 64)
        Me.picSpaceFiller.TabIndex = 61
        Me.picSpaceFiller.TabStop = False
        '
        'tabSearchSettings
        '
        Me.tabSearchSettings.Controls.Add(Me.rbtnBPMats)
        Me.tabSearchSettings.Controls.Add(Me.btnSaveSettings)
        Me.tabSearchSettings.Controls.Add(Me.btnSearchRefresh)
        Me.tabSearchSettings.Controls.Add(Me.rbtnAllItems)
        Me.tabSearchSettings.Controls.Add(Me.lblItemFilter)
        Me.tabSearchSettings.Controls.Add(Me.btnResetItemFilter)
        Me.tabSearchSettings.Controls.Add(Me.txtItemFilter)
        Me.tabSearchSettings.Controls.Add(Me.gbManufacturedItems)
        Me.tabSearchSettings.Controls.Add(Me.gbRawMaterials)
        Me.tabSearchSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabSearchSettings.Name = "tabSearchSettings"
        Me.tabSearchSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSearchSettings.Size = New System.Drawing.Size(275, 636)
        Me.tabSearchSettings.TabIndex = 1
        Me.tabSearchSettings.Text = "Selected Material Settings"
        Me.tabSearchSettings.UseVisualStyleBackColor = True
        '
        'rbtnBPMats
        '
        Me.rbtnBPMats.AutoSize = True
        Me.rbtnBPMats.Location = New System.Drawing.Point(122, 10)
        Me.rbtnBPMats.Name = "rbtnBPMats"
        Me.rbtnBPMats.Size = New System.Drawing.Size(111, 17)
        Me.rbtnBPMats.TabIndex = 249
        Me.rbtnBPMats.Text = "Blueprint Materials"
        Me.rbtnBPMats.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(185, 62)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(86, 24)
        Me.btnSaveSettings.TabIndex = 247
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnSearchRefresh
        '
        Me.btnSearchRefresh.Location = New System.Drawing.Point(5, 62)
        Me.btnSearchRefresh.Name = "btnSearchRefresh"
        Me.btnSearchRefresh.Size = New System.Drawing.Size(86, 24)
        Me.btnSearchRefresh.TabIndex = 246
        Me.btnSearchRefresh.Text = "Refresh"
        Me.btnSearchRefresh.UseVisualStyleBackColor = True
        '
        'rbtnAllItems
        '
        Me.rbtnAllItems.AutoSize = True
        Me.rbtnAllItems.Location = New System.Drawing.Point(41, 10)
        Me.rbtnAllItems.Name = "rbtnAllItems"
        Me.rbtnAllItems.Size = New System.Drawing.Size(64, 17)
        Me.rbtnAllItems.TabIndex = 248
        Me.rbtnAllItems.Text = "All Items"
        Me.rbtnAllItems.UseVisualStyleBackColor = True
        '
        'gbManufacturedItems
        '
        Me.gbManufacturedItems.Controls.Add(Me.gbItems)
        Me.gbManufacturedItems.Controls.Add(Me.chkManufacturedItems)
        Me.gbManufacturedItems.Controls.Add(Me.gbComponents)
        Me.gbManufacturedItems.Location = New System.Drawing.Point(3, 325)
        Me.gbManufacturedItems.Name = "gbManufacturedItems"
        Me.gbManufacturedItems.Size = New System.Drawing.Size(269, 308)
        Me.gbManufacturedItems.TabIndex = 6
        Me.gbManufacturedItems.TabStop = False
        '
        'gbItems
        '
        Me.gbItems.Controls.Add(Me.chkImplants)
        Me.gbItems.Controls.Add(Me.chkCelestials)
        Me.gbItems.Controls.Add(Me.chkTools)
        Me.gbItems.Controls.Add(Me.chkFuelBlocks)
        Me.gbItems.Controls.Add(Me.cmbPriceChargeTypes)
        Me.gbItems.Controls.Add(Me.chkDataInterfaces)
        Me.gbItems.Controls.Add(Me.chkCharges)
        Me.gbItems.Controls.Add(Me.chkStructures)
        Me.gbItems.Controls.Add(Me.chkBoosters)
        Me.gbItems.Controls.Add(Me.chkRigs)
        Me.gbItems.Controls.Add(Me.cmbPriceShipTypes)
        Me.gbItems.Controls.Add(Me.chkSubsystems)
        Me.gbItems.Controls.Add(Me.chkDrones)
        Me.gbItems.Controls.Add(Me.chkModules)
        Me.gbItems.Controls.Add(Me.chkShips)
        Me.gbItems.Controls.Add(Me.chkDeployables)
        Me.gbItems.Controls.Add(Me.gbPricesTech)
        Me.gbItems.Location = New System.Drawing.Point(7, 16)
        Me.gbItems.Name = "gbItems"
        Me.gbItems.Size = New System.Drawing.Size(255, 200)
        Me.gbItems.TabIndex = 0
        Me.gbItems.TabStop = False
        Me.gbItems.Text = "Items"
        '
        'chkImplants
        '
        Me.chkImplants.AutoSize = True
        Me.chkImplants.Location = New System.Drawing.Point(159, 119)
        Me.chkImplants.Name = "chkImplants"
        Me.chkImplants.Size = New System.Drawing.Size(65, 17)
        Me.chkImplants.TabIndex = 251
        Me.chkImplants.Text = "Implants"
        Me.chkImplants.UseVisualStyleBackColor = True
        '
        'chkCelestials
        '
        Me.chkCelestials.AutoSize = True
        Me.chkCelestials.Location = New System.Drawing.Point(91, 120)
        Me.chkCelestials.Name = "chkCelestials"
        Me.chkCelestials.Size = New System.Drawing.Size(70, 17)
        Me.chkCelestials.TabIndex = 252
        Me.chkCelestials.Text = "Celestials"
        Me.chkCelestials.UseVisualStyleBackColor = True
        '
        'chkTools
        '
        Me.chkTools.AutoSize = True
        Me.chkTools.Location = New System.Drawing.Point(159, 103)
        Me.chkTools.Name = "chkTools"
        Me.chkTools.Size = New System.Drawing.Size(52, 17)
        Me.chkTools.TabIndex = 1
        Me.chkTools.Text = "Tools"
        Me.chkTools.UseVisualStyleBackColor = True
        '
        'chkFuelBlocks
        '
        Me.chkFuelBlocks.AutoSize = True
        Me.chkFuelBlocks.Location = New System.Drawing.Point(159, 86)
        Me.chkFuelBlocks.Name = "chkFuelBlocks"
        Me.chkFuelBlocks.Size = New System.Drawing.Size(81, 17)
        Me.chkFuelBlocks.TabIndex = 3
        Me.chkFuelBlocks.Text = "Fuel Blocks"
        Me.chkFuelBlocks.UseVisualStyleBackColor = True
        '
        'cmbPriceChargeTypes
        '
        Me.cmbPriceChargeTypes.FormattingEnabled = True
        Me.cmbPriceChargeTypes.Location = New System.Drawing.Point(70, 40)
        Me.cmbPriceChargeTypes.Name = "cmbPriceChargeTypes"
        Me.cmbPriceChargeTypes.Size = New System.Drawing.Size(178, 21)
        Me.cmbPriceChargeTypes.TabIndex = 10
        Me.cmbPriceChargeTypes.Text = "All Charge Types"
        '
        'chkDataInterfaces
        '
        Me.chkDataInterfaces.AutoSize = True
        Me.chkDataInterfaces.Location = New System.Drawing.Point(159, 69)
        Me.chkDataInterfaces.Name = "chkDataInterfaces"
        Me.chkDataInterfaces.Size = New System.Drawing.Size(99, 17)
        Me.chkDataInterfaces.TabIndex = 2
        Me.chkDataInterfaces.Text = "Data Interfaces"
        Me.chkDataInterfaces.UseVisualStyleBackColor = True
        '
        'chkCharges
        '
        Me.chkCharges.AutoSize = True
        Me.chkCharges.Location = New System.Drawing.Point(10, 42)
        Me.chkCharges.Name = "chkCharges"
        Me.chkCharges.Size = New System.Drawing.Size(65, 17)
        Me.chkCharges.TabIndex = 8
        Me.chkCharges.Text = "Charges"
        Me.chkCharges.UseVisualStyleBackColor = True
        '
        'chkStructures
        '
        Me.chkStructures.AutoSize = True
        Me.chkStructures.Location = New System.Drawing.Point(10, 86)
        Me.chkStructures.Name = "chkStructures"
        Me.chkStructures.Size = New System.Drawing.Size(74, 17)
        Me.chkStructures.TabIndex = 7
        Me.chkStructures.Text = "Structures"
        Me.chkStructures.UseVisualStyleBackColor = True
        '
        'chkBoosters
        '
        Me.chkBoosters.AutoSize = True
        Me.chkBoosters.Location = New System.Drawing.Point(10, 103)
        Me.chkBoosters.Name = "chkBoosters"
        Me.chkBoosters.Size = New System.Drawing.Size(67, 17)
        Me.chkBoosters.TabIndex = 6
        Me.chkBoosters.Text = "Boosters"
        Me.chkBoosters.UseVisualStyleBackColor = True
        '
        'chkRigs
        '
        Me.chkRigs.AutoSize = True
        Me.chkRigs.Location = New System.Drawing.Point(91, 69)
        Me.chkRigs.Name = "chkRigs"
        Me.chkRigs.Size = New System.Drawing.Size(47, 17)
        Me.chkRigs.TabIndex = 5
        Me.chkRigs.Text = "Rigs"
        Me.chkRigs.UseVisualStyleBackColor = True
        '
        'cmbPriceShipTypes
        '
        Me.cmbPriceShipTypes.FormattingEnabled = True
        Me.cmbPriceShipTypes.Location = New System.Drawing.Point(70, 17)
        Me.cmbPriceShipTypes.Name = "cmbPriceShipTypes"
        Me.cmbPriceShipTypes.Size = New System.Drawing.Size(178, 21)
        Me.cmbPriceShipTypes.TabIndex = 9
        Me.cmbPriceShipTypes.Text = "All Ship Types"
        '
        'chkSubsystems
        '
        Me.chkSubsystems.AutoSize = True
        Me.chkSubsystems.Location = New System.Drawing.Point(10, 69)
        Me.chkSubsystems.Name = "chkSubsystems"
        Me.chkSubsystems.Size = New System.Drawing.Size(82, 17)
        Me.chkSubsystems.TabIndex = 3
        Me.chkSubsystems.Text = "Subsystems"
        Me.chkSubsystems.UseVisualStyleBackColor = True
        '
        'chkDrones
        '
        Me.chkDrones.AutoSize = True
        Me.chkDrones.Location = New System.Drawing.Point(91, 86)
        Me.chkDrones.Name = "chkDrones"
        Me.chkDrones.Size = New System.Drawing.Size(60, 17)
        Me.chkDrones.TabIndex = 2
        Me.chkDrones.Text = "Drones"
        Me.chkDrones.UseVisualStyleBackColor = True
        '
        'chkModules
        '
        Me.chkModules.AutoSize = True
        Me.chkModules.Location = New System.Drawing.Point(91, 103)
        Me.chkModules.Name = "chkModules"
        Me.chkModules.Size = New System.Drawing.Size(66, 17)
        Me.chkModules.TabIndex = 1
        Me.chkModules.Text = "Modules"
        Me.chkModules.UseVisualStyleBackColor = True
        '
        'chkShips
        '
        Me.chkShips.AutoSize = True
        Me.chkShips.Location = New System.Drawing.Point(10, 19)
        Me.chkShips.Name = "chkShips"
        Me.chkShips.Size = New System.Drawing.Size(52, 17)
        Me.chkShips.TabIndex = 4
        Me.chkShips.Text = "Ships"
        Me.chkShips.UseVisualStyleBackColor = True
        '
        'chkDeployables
        '
        Me.chkDeployables.AutoSize = True
        Me.chkDeployables.Location = New System.Drawing.Point(10, 120)
        Me.chkDeployables.Name = "chkDeployables"
        Me.chkDeployables.Size = New System.Drawing.Size(84, 17)
        Me.chkDeployables.TabIndex = 253
        Me.chkDeployables.Text = "Deployables"
        Me.chkDeployables.UseVisualStyleBackColor = True
        '
        'gbPricesTech
        '
        Me.gbPricesTech.Controls.Add(Me.chkItemsT4)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT6)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT5)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT3)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT2)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT1)
        Me.gbPricesTech.Location = New System.Drawing.Point(44, 133)
        Me.gbPricesTech.Name = "gbPricesTech"
        Me.gbPricesTech.Size = New System.Drawing.Size(166, 62)
        Me.gbPricesTech.TabIndex = 0
        Me.gbPricesTech.TabStop = False
        '
        'chkItemsT4
        '
        Me.chkItemsT4.AutoSize = True
        Me.chkItemsT4.Enabled = False
        Me.chkItemsT4.Location = New System.Drawing.Point(72, 9)
        Me.chkItemsT4.Name = "chkItemsT4"
        Me.chkItemsT4.Size = New System.Drawing.Size(66, 17)
        Me.chkItemsT4.TabIndex = 3
        Me.chkItemsT4.Text = "Storyline"
        Me.chkItemsT4.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkItemsT4.UseVisualStyleBackColor = True
        '
        'chkItemsT6
        '
        Me.chkItemsT6.AutoSize = True
        Me.chkItemsT6.Enabled = False
        Me.chkItemsT6.Location = New System.Drawing.Point(72, 43)
        Me.chkItemsT6.Name = "chkItemsT6"
        Me.chkItemsT6.Size = New System.Drawing.Size(91, 17)
        Me.chkItemsT6.TabIndex = 9
        Me.chkItemsT6.Text = "Pirate Faction"
        Me.chkItemsT6.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkItemsT6.UseVisualStyleBackColor = True
        '
        'chkItemsT5
        '
        Me.chkItemsT5.AutoSize = True
        Me.chkItemsT5.Enabled = False
        Me.chkItemsT5.Location = New System.Drawing.Point(72, 26)
        Me.chkItemsT5.Name = "chkItemsT5"
        Me.chkItemsT5.Size = New System.Drawing.Size(89, 17)
        Me.chkItemsT5.TabIndex = 8
        Me.chkItemsT5.Text = "Navy Faction"
        Me.chkItemsT5.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkItemsT5.UseVisualStyleBackColor = True
        '
        'chkItemsT3
        '
        Me.chkItemsT3.AutoSize = True
        Me.chkItemsT3.Enabled = False
        Me.chkItemsT3.Location = New System.Drawing.Point(6, 43)
        Me.chkItemsT3.Name = "chkItemsT3"
        Me.chkItemsT3.Size = New System.Drawing.Size(60, 17)
        Me.chkItemsT3.TabIndex = 2
        Me.chkItemsT3.Text = "Tech 3"
        Me.chkItemsT3.UseVisualStyleBackColor = True
        '
        'chkItemsT2
        '
        Me.chkItemsT2.AutoSize = True
        Me.chkItemsT2.Enabled = False
        Me.chkItemsT2.Location = New System.Drawing.Point(6, 26)
        Me.chkItemsT2.Name = "chkItemsT2"
        Me.chkItemsT2.Size = New System.Drawing.Size(60, 17)
        Me.chkItemsT2.TabIndex = 1
        Me.chkItemsT2.Text = "Tech 2"
        Me.chkItemsT2.UseVisualStyleBackColor = True
        '
        'chkItemsT1
        '
        Me.chkItemsT1.AutoSize = True
        Me.chkItemsT1.Enabled = False
        Me.chkItemsT1.Location = New System.Drawing.Point(6, 9)
        Me.chkItemsT1.Name = "chkItemsT1"
        Me.chkItemsT1.Size = New System.Drawing.Size(60, 17)
        Me.chkItemsT1.TabIndex = 0
        Me.chkItemsT1.Text = "Tech 1"
        Me.chkItemsT1.UseVisualStyleBackColor = True
        '
        'chkManufacturedItems
        '
        Me.chkManufacturedItems.AutoSize = True
        Me.chkManufacturedItems.BackColor = System.Drawing.Color.White
        Me.chkManufacturedItems.Location = New System.Drawing.Point(6, 1)
        Me.chkManufacturedItems.Name = "chkManufacturedItems"
        Me.chkManufacturedItems.Size = New System.Drawing.Size(120, 17)
        Me.chkManufacturedItems.TabIndex = 19
        Me.chkManufacturedItems.Text = "Manufactured Items"
        Me.chkManufacturedItems.UseVisualStyleBackColor = False
        '
        'gbComponents
        '
        Me.gbComponents.Controls.Add(Me.chkHybrid)
        Me.gbComponents.Controls.Add(Me.chkComponents)
        Me.gbComponents.Controls.Add(Me.chkCapitalComponents)
        Me.gbComponents.Controls.Add(Me.chkCapT2Components)
        Me.gbComponents.Controls.Add(Me.chkStationComponents)
        Me.gbComponents.Location = New System.Drawing.Point(7, 215)
        Me.gbComponents.Name = "gbComponents"
        Me.gbComponents.Size = New System.Drawing.Size(256, 88)
        Me.gbComponents.TabIndex = 1
        Me.gbComponents.TabStop = False
        Me.gbComponents.Text = "Components"
        '
        'chkHybrid
        '
        Me.chkHybrid.AutoSize = True
        Me.chkHybrid.Location = New System.Drawing.Point(9, 66)
        Me.chkHybrid.Name = "chkHybrid"
        Me.chkHybrid.Size = New System.Drawing.Size(146, 17)
        Me.chkHybrid.TabIndex = 5
        Me.chkHybrid.Text = "Hybrid Tech Components"
        Me.chkHybrid.UseVisualStyleBackColor = True
        '
        'chkComponents
        '
        Me.chkComponents.AutoSize = True
        Me.chkComponents.Location = New System.Drawing.Point(9, 49)
        Me.chkComponents.Name = "chkComponents"
        Me.chkComponents.Size = New System.Drawing.Size(147, 17)
        Me.chkComponents.TabIndex = 4
        Me.chkComponents.Text = "Construction Components"
        Me.chkComponents.UseVisualStyleBackColor = True
        '
        'chkCapitalComponents
        '
        Me.chkCapitalComponents.AutoSize = True
        Me.chkCapitalComponents.Location = New System.Drawing.Point(9, 32)
        Me.chkCapitalComponents.Name = "chkCapitalComponents"
        Me.chkCapitalComponents.Size = New System.Drawing.Size(182, 17)
        Me.chkCapitalComponents.TabIndex = 3
        Me.chkCapitalComponents.Text = "Capital Construction Components"
        Me.chkCapitalComponents.UseVisualStyleBackColor = True
        '
        'chkCapT2Components
        '
        Me.chkCapT2Components.AutoSize = True
        Me.chkCapT2Components.Location = New System.Drawing.Point(9, 15)
        Me.chkCapT2Components.Name = "chkCapT2Components"
        Me.chkCapT2Components.Size = New System.Drawing.Size(207, 17)
        Me.chkCapT2Components.TabIndex = 2
        Me.chkCapT2Components.Text = "Adv. Capital Construction Components"
        Me.chkCapT2Components.UseVisualStyleBackColor = True
        '
        'chkStationComponents
        '
        Me.chkStationComponents.AutoSize = True
        Me.chkStationComponents.Location = New System.Drawing.Point(162, 65)
        Me.chkStationComponents.Name = "chkStationComponents"
        Me.chkStationComponents.Size = New System.Drawing.Size(86, 17)
        Me.chkStationComponents.TabIndex = 16
        Me.chkStationComponents.Text = "Station Parts"
        Me.chkStationComponents.UseVisualStyleBackColor = True
        '
        'gbRawMaterials
        '
        Me.gbRawMaterials.Controls.Add(Me.chkBPCs)
        Me.gbRawMaterials.Controls.Add(Me.chkMisc)
        Me.gbRawMaterials.Controls.Add(Me.chkAsteroids)
        Me.gbRawMaterials.Controls.Add(Me.chkRawMaterialItems)
        Me.gbRawMaterials.Controls.Add(Me.chkPlanetary)
        Me.gbRawMaterials.Controls.Add(Me.chkBoosterMats)
        Me.gbRawMaterials.Controls.Add(Me.chkDroneComponents)
        Me.gbRawMaterials.Controls.Add(Me.chkMatsandCompounds)
        Me.gbRawMaterials.Controls.Add(Me.chkAdvancedMats)
        Me.gbRawMaterials.Controls.Add(Me.chkProcessedMats)
        Me.gbRawMaterials.Controls.Add(Me.chkRawMats)
        Me.gbRawMaterials.Controls.Add(Me.chkGas)
        Me.gbRawMaterials.Controls.Add(Me.chkPolymers)
        Me.gbRawMaterials.Controls.Add(Me.chkAncientRelics)
        Me.gbRawMaterials.Controls.Add(Me.chkAncientSalvage)
        Me.gbRawMaterials.Controls.Add(Me.chkSalvage)
        Me.gbRawMaterials.Controls.Add(Me.chkDecryptors)
        Me.gbRawMaterials.Controls.Add(Me.chkDatacores)
        Me.gbRawMaterials.Controls.Add(Me.chkIceProducts)
        Me.gbRawMaterials.Controls.Add(Me.chkMinerals)
        Me.gbRawMaterials.Location = New System.Drawing.Point(3, 88)
        Me.gbRawMaterials.Name = "gbRawMaterials"
        Me.gbRawMaterials.Size = New System.Drawing.Size(269, 237)
        Me.gbRawMaterials.TabIndex = 5
        Me.gbRawMaterials.TabStop = False
        '
        'chkBPCs
        '
        Me.chkBPCs.AutoSize = True
        Me.chkBPCs.Location = New System.Drawing.Point(132, 86)
        Me.chkBPCs.Name = "chkBPCs"
        Me.chkBPCs.Size = New System.Drawing.Size(102, 17)
        Me.chkBPCs.TabIndex = 21
        Me.chkBPCs.Text = "Blueprint Copies"
        Me.chkBPCs.UseVisualStyleBackColor = True
        '
        'chkMisc
        '
        Me.chkMisc.AutoSize = True
        Me.chkMisc.Location = New System.Drawing.Point(186, 103)
        Me.chkMisc.Name = "chkMisc"
        Me.chkMisc.Size = New System.Drawing.Size(51, 17)
        Me.chkMisc.TabIndex = 20
        Me.chkMisc.Text = "Misc."
        Me.chkMisc.UseVisualStyleBackColor = True
        '
        'chkAsteroids
        '
        Me.chkAsteroids.AutoSize = True
        Me.chkAsteroids.Location = New System.Drawing.Point(17, 103)
        Me.chkAsteroids.Name = "chkAsteroids"
        Me.chkAsteroids.Size = New System.Drawing.Size(69, 17)
        Me.chkAsteroids.TabIndex = 19
        Me.chkAsteroids.Text = "Asteroids"
        Me.chkAsteroids.UseVisualStyleBackColor = True
        '
        'chkRawMaterialItems
        '
        Me.chkRawMaterialItems.AutoSize = True
        Me.chkRawMaterialItems.BackColor = System.Drawing.Color.White
        Me.chkRawMaterialItems.Location = New System.Drawing.Point(6, 1)
        Me.chkRawMaterialItems.Name = "chkRawMaterialItems"
        Me.chkRawMaterialItems.Size = New System.Drawing.Size(93, 17)
        Me.chkRawMaterialItems.TabIndex = 18
        Me.chkRawMaterialItems.Text = "Raw Materials"
        Me.chkRawMaterialItems.UseVisualStyleBackColor = False
        '
        'chkPlanetary
        '
        Me.chkPlanetary.AutoSize = True
        Me.chkPlanetary.Location = New System.Drawing.Point(17, 86)
        Me.chkPlanetary.Name = "chkPlanetary"
        Me.chkPlanetary.Size = New System.Drawing.Size(70, 17)
        Me.chkPlanetary.TabIndex = 17
        Me.chkPlanetary.Text = "Planetary"
        Me.chkPlanetary.UseVisualStyleBackColor = True
        '
        'chkBoosterMats
        '
        Me.chkBoosterMats.AutoSize = True
        Me.chkBoosterMats.Location = New System.Drawing.Point(17, 214)
        Me.chkBoosterMats.Name = "chkBoosterMats"
        Me.chkBoosterMats.Size = New System.Drawing.Size(107, 17)
        Me.chkBoosterMats.TabIndex = 15
        Me.chkBoosterMats.Text = "Booster Materials"
        Me.chkBoosterMats.UseVisualStyleBackColor = True
        '
        'chkDroneComponents
        '
        Me.chkDroneComponents.AutoSize = True
        Me.chkDroneComponents.Location = New System.Drawing.Point(17, 197)
        Me.chkDroneComponents.Name = "chkDroneComponents"
        Me.chkDroneComponents.Size = New System.Drawing.Size(117, 17)
        Me.chkDroneComponents.TabIndex = 14
        Me.chkDroneComponents.Text = "Rogue Drone Parts"
        Me.chkDroneComponents.UseVisualStyleBackColor = True
        '
        'chkMatsandCompounds
        '
        Me.chkMatsandCompounds.AutoSize = True
        Me.chkMatsandCompounds.Location = New System.Drawing.Point(17, 180)
        Me.chkMatsandCompounds.Name = "chkMatsandCompounds"
        Me.chkMatsandCompounds.Size = New System.Drawing.Size(136, 17)
        Me.chkMatsandCompounds.TabIndex = 13
        Me.chkMatsandCompounds.Text = "Materials && Compounds"
        Me.chkMatsandCompounds.UseVisualStyleBackColor = True
        '
        'chkAdvancedMats
        '
        Me.chkAdvancedMats.AutoSize = True
        Me.chkAdvancedMats.Location = New System.Drawing.Point(17, 158)
        Me.chkAdvancedMats.Name = "chkAdvancedMats"
        Me.chkAdvancedMats.Size = New System.Drawing.Size(150, 17)
        Me.chkAdvancedMats.TabIndex = 12
        Me.chkAdvancedMats.Text = "Advanced Moon Materials"
        Me.chkAdvancedMats.UseVisualStyleBackColor = True
        '
        'chkProcessedMats
        '
        Me.chkProcessedMats.AutoSize = True
        Me.chkProcessedMats.Location = New System.Drawing.Point(17, 141)
        Me.chkProcessedMats.Name = "chkProcessedMats"
        Me.chkProcessedMats.Size = New System.Drawing.Size(151, 17)
        Me.chkProcessedMats.TabIndex = 11
        Me.chkProcessedMats.Text = "Processed Moon Materials"
        Me.chkProcessedMats.UseVisualStyleBackColor = True
        '
        'chkRawMats
        '
        Me.chkRawMats.AutoSize = True
        Me.chkRawMats.Location = New System.Drawing.Point(17, 124)
        Me.chkRawMats.Name = "chkRawMats"
        Me.chkRawMats.Size = New System.Drawing.Size(123, 17)
        Me.chkRawMats.TabIndex = 10
        Me.chkRawMats.Text = "Raw Moon Materials"
        Me.chkRawMats.UseVisualStyleBackColor = True
        '
        'chkGas
        '
        Me.chkGas.AutoSize = True
        Me.chkGas.Location = New System.Drawing.Point(133, 103)
        Me.chkGas.Name = "chkGas"
        Me.chkGas.Size = New System.Drawing.Size(45, 17)
        Me.chkGas.TabIndex = 9
        Me.chkGas.Text = "Gas"
        Me.chkGas.UseVisualStyleBackColor = True
        '
        'chkPolymers
        '
        Me.chkPolymers.AutoSize = True
        Me.chkPolymers.Location = New System.Drawing.Point(133, 69)
        Me.chkPolymers.Name = "chkPolymers"
        Me.chkPolymers.Size = New System.Drawing.Size(101, 17)
        Me.chkPolymers.TabIndex = 8
        Me.chkPolymers.Text = "Hybrid Polymers"
        Me.chkPolymers.UseVisualStyleBackColor = True
        '
        'chkAncientRelics
        '
        Me.chkAncientRelics.AutoSize = True
        Me.chkAncientRelics.Location = New System.Drawing.Point(133, 52)
        Me.chkAncientRelics.Name = "chkAncientRelics"
        Me.chkAncientRelics.Size = New System.Drawing.Size(94, 17)
        Me.chkAncientRelics.TabIndex = 7
        Me.chkAncientRelics.Text = "Ancient Relics"
        Me.chkAncientRelics.UseVisualStyleBackColor = True
        '
        'chkAncientSalvage
        '
        Me.chkAncientSalvage.AutoSize = True
        Me.chkAncientSalvage.Location = New System.Drawing.Point(133, 35)
        Me.chkAncientSalvage.Name = "chkAncientSalvage"
        Me.chkAncientSalvage.Size = New System.Drawing.Size(104, 17)
        Me.chkAncientSalvage.TabIndex = 6
        Me.chkAncientSalvage.Text = "Ancient Salvage"
        Me.chkAncientSalvage.UseVisualStyleBackColor = True
        '
        'chkSalvage
        '
        Me.chkSalvage.AutoSize = True
        Me.chkSalvage.Location = New System.Drawing.Point(133, 18)
        Me.chkSalvage.Name = "chkSalvage"
        Me.chkSalvage.Size = New System.Drawing.Size(65, 17)
        Me.chkSalvage.TabIndex = 5
        Me.chkSalvage.Text = "Salvage"
        Me.chkSalvage.UseVisualStyleBackColor = True
        '
        'chkDecryptors
        '
        Me.chkDecryptors.AutoSize = True
        Me.chkDecryptors.Location = New System.Drawing.Point(17, 69)
        Me.chkDecryptors.Name = "chkDecryptors"
        Me.chkDecryptors.Size = New System.Drawing.Size(77, 17)
        Me.chkDecryptors.TabIndex = 4
        Me.chkDecryptors.Text = "Decryptors"
        Me.chkDecryptors.UseVisualStyleBackColor = True
        '
        'chkDatacores
        '
        Me.chkDatacores.AutoSize = True
        Me.chkDatacores.Location = New System.Drawing.Point(17, 52)
        Me.chkDatacores.Name = "chkDatacores"
        Me.chkDatacores.Size = New System.Drawing.Size(75, 17)
        Me.chkDatacores.TabIndex = 2
        Me.chkDatacores.Text = "Datacores"
        Me.chkDatacores.UseVisualStyleBackColor = True
        '
        'chkIceProducts
        '
        Me.chkIceProducts.AutoSize = True
        Me.chkIceProducts.Location = New System.Drawing.Point(17, 35)
        Me.chkIceProducts.Name = "chkIceProducts"
        Me.chkIceProducts.Size = New System.Drawing.Size(86, 17)
        Me.chkIceProducts.TabIndex = 1
        Me.chkIceProducts.Text = "Ice Products"
        Me.chkIceProducts.UseVisualStyleBackColor = True
        '
        'chkMinerals
        '
        Me.chkMinerals.AutoSize = True
        Me.chkMinerals.Location = New System.Drawing.Point(17, 18)
        Me.chkMinerals.Name = "chkMinerals"
        Me.chkMinerals.Size = New System.Drawing.Size(65, 17)
        Me.chkMinerals.TabIndex = 0
        Me.chkMinerals.Text = "Minerals"
        Me.chkMinerals.UseVisualStyleBackColor = True
        '
        'btnToggleExpand
        '
        Me.btnToggleExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleExpand.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnToggleExpand.Image = CType(resources.GetObject("btnToggleExpand.Image"), System.Drawing.Image)
        Me.btnToggleExpand.Location = New System.Drawing.Point(304, 4)
        Me.btnToggleExpand.Name = "btnToggleExpand"
        Me.btnToggleExpand.Size = New System.Drawing.Size(25, 25)
        Me.btnToggleExpand.TabIndex = 246
        Me.btnToggleExpand.UseVisualStyleBackColor = False
        '
        'ttMain
        '
        Me.ttMain.IsBalloon = True
        '
        'chkRawMaterialPrices
        '
        Me.chkRawMaterialPrices.AutoSize = True
        Me.chkRawMaterialPrices.BackColor = System.Drawing.Color.White
        Me.chkRawMaterialPrices.Location = New System.Drawing.Point(6, 1)
        Me.chkRawMaterialPrices.Name = "chkRawMaterialPrices"
        Me.chkRawMaterialPrices.Size = New System.Drawing.Size(93, 17)
        Me.chkRawMaterialPrices.TabIndex = 18
        Me.chkRawMaterialPrices.Text = "Raw Materials"
        Me.chkRawMaterialPrices.UseVisualStyleBackColor = False
        '
        'btnToggleRetract
        '
        Me.btnToggleRetract.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleRetract.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnToggleRetract.Image = CType(resources.GetObject("btnToggleRetract.Image"), System.Drawing.Image)
        Me.btnToggleRetract.Location = New System.Drawing.Point(304, 4)
        Me.btnToggleRetract.Name = "btnToggleRetract"
        Me.btnToggleRetract.Size = New System.Drawing.Size(25, 25)
        Me.btnToggleRetract.TabIndex = 247
        Me.btnToggleRetract.UseVisualStyleBackColor = False
        '
        'chkToggle
        '
        Me.chkToggle.AutoSize = True
        Me.chkToggle.BackColor = System.Drawing.Color.Transparent
        Me.chkToggle.Location = New System.Drawing.Point(285, 10)
        Me.chkToggle.Name = "chkToggle"
        Me.chkToggle.Size = New System.Drawing.Size(15, 14)
        Me.chkToggle.TabIndex = 248
        Me.chkToggle.UseVisualStyleBackColor = False
        '
        'btnCheckToggle
        '
        Me.btnCheckToggle.Enabled = False
        Me.btnCheckToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckToggle.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnCheckToggle.Location = New System.Drawing.Point(280, 4)
        Me.btnCheckToggle.Name = "btnCheckToggle"
        Me.btnCheckToggle.Size = New System.Drawing.Size(25, 25)
        Me.btnCheckToggle.TabIndex = 249
        Me.btnCheckToggle.UseVisualStyleBackColor = False
        '
        'AssetTree
        '
        Me.AssetTree.CheckBoxes = True
        Me.AssetTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
        Me.AssetTree.Location = New System.Drawing.Point(4, 4)
        Me.AssetTree.Name = "AssetTree"
        Me.AssetTree.Size = New System.Drawing.Size(346, 662)
        Me.AssetTree.TabIndex = 250
        '
        'frmAssetsViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(641, 672)
        Me.Controls.Add(Me.chkToggle)
        Me.Controls.Add(Me.btnCheckToggle)
        Me.Controls.Add(Me.btnToggleRetract)
        Me.Controls.Add(Me.btnToggleExpand)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.AssetTree)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAssetsViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Assets Viewer"
        Me.gbSortOptions.ResumeLayout(False)
        Me.gbSortOptions.PerformLayout()
        Me.gbAssetTypes.ResumeLayout(False)
        Me.gbAssetTypes.PerformLayout()
        Me.tabMain.ResumeLayout(False)
        Me.tabAssetMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picSpaceFiller, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSearchSettings.ResumeLayout(False)
        Me.tabSearchSettings.PerformLayout()
        Me.gbManufacturedItems.ResumeLayout(False)
        Me.gbManufacturedItems.PerformLayout()
        Me.gbItems.ResumeLayout(False)
        Me.gbItems.PerformLayout()
        Me.gbPricesTech.ResumeLayout(False)
        Me.gbPricesTech.PerformLayout()
        Me.gbComponents.ResumeLayout(False)
        Me.gbComponents.PerformLayout()
        Me.gbRawMaterials.ResumeLayout(False)
        Me.gbRawMaterials.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCloseAssets As System.Windows.Forms.Button
    Friend WithEvents gbSortOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnSortQuantity As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSortName As System.Windows.Forms.RadioButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents gbAssetTypes As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnCorpAssets As System.Windows.Forms.RadioButton
    Friend WithEvents btnResetItemFilter As System.Windows.Forms.Button
    Friend WithEvents txtItemFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblItemFilter As System.Windows.Forms.Label
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tabAssetMain As System.Windows.Forms.TabPage
    Friend WithEvents tabSearchSettings As System.Windows.Forms.TabPage
    Friend WithEvents gbManufacturedItems As System.Windows.Forms.GroupBox
    Friend WithEvents chkManufacturedItems As System.Windows.Forms.CheckBox
    Friend WithEvents chkFuelBlocks As System.Windows.Forms.CheckBox
    Friend WithEvents chkDataInterfaces As System.Windows.Forms.CheckBox
    Friend WithEvents chkTools As System.Windows.Forms.CheckBox
    Friend WithEvents gbComponents As System.Windows.Forms.GroupBox
    Friend WithEvents chkHybrid As System.Windows.Forms.CheckBox
    Friend WithEvents chkComponents As System.Windows.Forms.CheckBox
    Friend WithEvents chkCapitalComponents As System.Windows.Forms.CheckBox
    Friend WithEvents chkCapT2Components As System.Windows.Forms.CheckBox
    Friend WithEvents gbItems As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPriceChargeTypes As System.Windows.Forms.ComboBox
    Friend WithEvents chkCharges As System.Windows.Forms.CheckBox
    Friend WithEvents chkStructures As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoosters As System.Windows.Forms.CheckBox
    Friend WithEvents chkRigs As System.Windows.Forms.CheckBox
    Friend WithEvents cmbPriceShipTypes As System.Windows.Forms.ComboBox
    Friend WithEvents chkSubsystems As System.Windows.Forms.CheckBox
    Friend WithEvents chkDrones As System.Windows.Forms.CheckBox
    Friend WithEvents chkModules As System.Windows.Forms.CheckBox
    Friend WithEvents gbPricesTech As System.Windows.Forms.GroupBox
    Friend WithEvents chkItemsT4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemsT6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemsT5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemsT3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemsT2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemsT1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkShips As System.Windows.Forms.CheckBox
    Friend WithEvents gbRawMaterials As System.Windows.Forms.GroupBox
    Friend WithEvents chkMisc As System.Windows.Forms.CheckBox
    Friend WithEvents chkAsteroids As System.Windows.Forms.CheckBox
    Friend WithEvents chkRawMaterialItems As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlanetary As System.Windows.Forms.CheckBox
    Friend WithEvents chkStationComponents As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoosterMats As System.Windows.Forms.CheckBox
    Friend WithEvents chkDroneComponents As System.Windows.Forms.CheckBox
    Friend WithEvents chkMatsandCompounds As System.Windows.Forms.CheckBox
    Friend WithEvents chkAdvancedMats As System.Windows.Forms.CheckBox
    Friend WithEvents chkProcessedMats As System.Windows.Forms.CheckBox
    Friend WithEvents chkRawMats As System.Windows.Forms.CheckBox
    Friend WithEvents chkGas As System.Windows.Forms.CheckBox
    Friend WithEvents chkPolymers As System.Windows.Forms.CheckBox
    Friend WithEvents chkAncientRelics As System.Windows.Forms.CheckBox
    Friend WithEvents chkAncientSalvage As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalvage As System.Windows.Forms.CheckBox
    Friend WithEvents chkDecryptors As System.Windows.Forms.CheckBox
    Friend WithEvents chkDatacores As System.Windows.Forms.CheckBox
    Friend WithEvents chkIceProducts As System.Windows.Forms.CheckBox
    Friend WithEvents chkMinerals As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearchRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnScanPersonalAssets As System.Windows.Forms.Button
    Friend WithEvents btnScanCorpAssets As System.Windows.Forms.Button
    Friend WithEvents lblReloadPersonalAssets1 As System.Windows.Forms.Label
    Friend WithEvents lblReloadPersonalAssets As System.Windows.Forms.Label
    Friend WithEvents lblReloadCorpAssets As System.Windows.Forms.Label
    Friend WithEvents lblReloadCorpAssets2 As System.Windows.Forms.Label
    Friend WithEvents picSpaceFiller As System.Windows.Forms.PictureBox
    Friend WithEvents btnSaveMainSettings As System.Windows.Forms.Button
    Friend WithEvents btnToggleExpand As System.Windows.Forms.Button
    Friend WithEvents ttMain As System.Windows.Forms.ToolTip
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents chkRawMaterialPrices As System.Windows.Forms.CheckBox
    Friend WithEvents rbtnBPMats As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAllItems As System.Windows.Forms.RadioButton
    Friend WithEvents btnToggleRetract As System.Windows.Forms.Button
    Friend WithEvents chkToggle As System.Windows.Forms.CheckBox
    Friend WithEvents btnCheckToggle As System.Windows.Forms.Button
    Friend WithEvents rbtnAllAssets As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPersonalAssets As System.Windows.Forms.RadioButton
    Friend WithEvents AssetTree As System.Windows.Forms.TreeView
    Friend WithEvents chkImplants As System.Windows.Forms.CheckBox
    Friend WithEvents chkCelestials As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeployables As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPCs As System.Windows.Forms.CheckBox
End Class
