<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AssetViewer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AssetViewer))
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabAssetMain = New System.Windows.Forms.TabPage()
        Me.btnSaveMainSettings = New System.Windows.Forms.Button()
        Me.gbAssetTypes = New System.Windows.Forms.GroupBox()
        Me.rbtnAllAssets = New System.Windows.Forms.RadioButton()
        Me.rbtnCorpAssets = New System.Windows.Forms.RadioButton()
        Me.rbtnPersonalAssets = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblReloadCorpAssets = New System.Windows.Forms.Label()
        Me.lblReloadPersonalAssets = New System.Windows.Forms.Label()
        Me.btnScanCorpAssets = New System.Windows.Forms.Button()
        Me.lblReloadCorpAssets2 = New System.Windows.Forms.Label()
        Me.btnScanPersonalAssets = New System.Windows.Forms.Button()
        Me.lblReloadPersonalAssets1 = New System.Windows.Forms.Label()
        Me.gbSortOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnSortQuantity = New System.Windows.Forms.RadioButton()
        Me.rbtnSortName = New System.Windows.Forms.RadioButton()
        Me.picSpaceFiller = New System.Windows.Forms.PictureBox()
        Me.btnCloseAssets = New System.Windows.Forms.Button()
        Me.tabSearchSettings = New System.Windows.Forms.TabPage()
        Me.rbtnBPMats = New System.Windows.Forms.RadioButton()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnSearchRefresh = New System.Windows.Forms.Button()
        Me.rbtnAllItems = New System.Windows.Forms.RadioButton()
        Me.lblItemFilter = New System.Windows.Forms.Label()
        Me.gbRawMaterials = New System.Windows.Forms.GroupBox()
        Me.gbResearchEquipment = New System.Windows.Forms.GroupBox()
        Me.chkRDb = New System.Windows.Forms.CheckBox()
        Me.chkAncientRelics = New System.Windows.Forms.CheckBox()
        Me.chkDecryptors = New System.Windows.Forms.CheckBox()
        Me.chkDatacores = New System.Windows.Forms.CheckBox()
        Me.gbReactionMaterials = New System.Windows.Forms.GroupBox()
        Me.chkAdvancedMats = New System.Windows.Forms.CheckBox()
        Me.chkMolecularForgedMaterials = New System.Windows.Forms.CheckBox()
        Me.chkProcessedMats = New System.Windows.Forms.CheckBox()
        Me.chkBoosterMats = New System.Windows.Forms.CheckBox()
        Me.chkPolymers = New System.Windows.Forms.CheckBox()
        Me.chkRawMoonMats = New System.Windows.Forms.CheckBox()
        Me.chkFactionMaterials = New System.Windows.Forms.CheckBox()
        Me.chkMolecularForgingTools = New System.Windows.Forms.CheckBox()
        Me.chkAdvancedProtectiveTechnology = New System.Windows.Forms.CheckBox()
        Me.chkBPCs = New System.Windows.Forms.CheckBox()
        Me.chkMisc = New System.Windows.Forms.CheckBox()
        Me.chkRawMaterials = New System.Windows.Forms.CheckBox()
        Me.chkMaterialResearchEqPrices = New System.Windows.Forms.CheckBox()
        Me.chkPlanetary = New System.Windows.Forms.CheckBox()
        Me.chkNamedComponents = New System.Windows.Forms.CheckBox()
        Me.chkGas = New System.Windows.Forms.CheckBox()
        Me.chkSalvage = New System.Windows.Forms.CheckBox()
        Me.chkIceProducts = New System.Windows.Forms.CheckBox()
        Me.chkMinerals = New System.Windows.Forms.CheckBox()
        Me.gbManufacturedItems = New System.Windows.Forms.GroupBox()
        Me.gbComponents = New System.Windows.Forms.GroupBox()
        Me.chkRAM = New System.Windows.Forms.CheckBox()
        Me.chkStructureComponents = New System.Windows.Forms.CheckBox()
        Me.chkProtectiveComponents = New System.Windows.Forms.CheckBox()
        Me.chkFuelBlocks = New System.Windows.Forms.CheckBox()
        Me.chkSubsystemComponents = New System.Windows.Forms.CheckBox()
        Me.chkAdvancedComponents = New System.Windows.Forms.CheckBox()
        Me.chkCapitalShipComponents = New System.Windows.Forms.CheckBox()
        Me.chkCapT2Components = New System.Windows.Forms.CheckBox()
        Me.gbItems = New System.Windows.Forms.GroupBox()
        Me.chkNobuild = New System.Windows.Forms.CheckBox()
        Me.chkImplants = New System.Windows.Forms.CheckBox()
        Me.chkBoosters = New System.Windows.Forms.CheckBox()
        Me.chkDeployables = New System.Windows.Forms.CheckBox()
        Me.chkStructureModules = New System.Windows.Forms.CheckBox()
        Me.chkCelestials = New System.Windows.Forms.CheckBox()
        Me.chkStructures = New System.Windows.Forms.CheckBox()
        Me.chkStructureRigs = New System.Windows.Forms.CheckBox()
        Me.chkSubsystems = New System.Windows.Forms.CheckBox()
        Me.chkRigs = New System.Windows.Forms.CheckBox()
        Me.cmbPriceChargeTypes = New System.Windows.Forms.ComboBox()
        Me.chkCharges = New System.Windows.Forms.CheckBox()
        Me.cmbPriceShipTypes = New System.Windows.Forms.ComboBox()
        Me.chkDrones = New System.Windows.Forms.CheckBox()
        Me.chkModules = New System.Windows.Forms.CheckBox()
        Me.chkShips = New System.Windows.Forms.CheckBox()
        Me.chkManufacturedItems = New System.Windows.Forms.CheckBox()
        Me.gbPricesTech = New System.Windows.Forms.GroupBox()
        Me.chkItemsT4 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT6 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT5 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT3 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT2 = New System.Windows.Forms.CheckBox()
        Me.chkItemsT1 = New System.Windows.Forms.CheckBox()
        Me.btnResetItemFilter = New System.Windows.Forms.Button()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
        Me.AssetTree = New System.Windows.Forms.TreeView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkToggle = New System.Windows.Forms.CheckBox()
        Me.btnToggleRetract = New System.Windows.Forms.Button()
        Me.btnCheckToggle = New System.Windows.Forms.Button()
        Me.btnToggleExpand = New System.Windows.Forms.Button()
        Me.tabMain.SuspendLayout()
        Me.tabAssetMain.SuspendLayout()
        Me.gbAssetTypes.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbSortOptions.SuspendLayout()
        CType(Me.picSpaceFiller, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSearchSettings.SuspendLayout()
        Me.gbRawMaterials.SuspendLayout()
        Me.gbResearchEquipment.SuspendLayout()
        Me.gbReactionMaterials.SuspendLayout()
        Me.gbManufacturedItems.SuspendLayout()
        Me.gbComponents.SuspendLayout()
        Me.gbItems.SuspendLayout()
        Me.gbPricesTech.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabAssetMain)
        Me.tabMain.Controls.Add(Me.tabSearchSettings)
        Me.tabMain.Location = New System.Drawing.Point(355, 3)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(284, 662)
        Me.tabMain.TabIndex = 251
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
        Me.tabAssetMain.Size = New System.Drawing.Size(276, 636)
        Me.tabAssetMain.TabIndex = 0
        Me.tabAssetMain.Text = "Main Search"
        Me.tabAssetMain.UseVisualStyleBackColor = True
        '
        'btnSaveMainSettings
        '
        Me.btnSaveMainSettings.Location = New System.Drawing.Point(32, 274)
        Me.btnSaveMainSettings.Name = "btnSaveMainSettings"
        Me.btnSaveMainSettings.Size = New System.Drawing.Size(102, 27)
        Me.btnSaveMainSettings.TabIndex = 6
        Me.btnSaveMainSettings.Text = "Save Settings"
        Me.btnSaveMainSettings.UseVisualStyleBackColor = True
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
        Me.rbtnAllAssets.TabIndex = 0
        Me.rbtnAllAssets.TabStop = True
        Me.rbtnAllAssets.Text = "Both"
        Me.rbtnAllAssets.UseVisualStyleBackColor = True
        '
        'rbtnCorpAssets
        '
        Me.rbtnCorpAssets.AutoSize = True
        Me.rbtnCorpAssets.Location = New System.Drawing.Point(172, 18)
        Me.rbtnCorpAssets.Name = "rbtnCorpAssets"
        Me.rbtnCorpAssets.Size = New System.Drawing.Size(79, 17)
        Me.rbtnCorpAssets.TabIndex = 2
        Me.rbtnCorpAssets.TabStop = True
        Me.rbtnCorpAssets.Text = "Corporation"
        Me.rbtnCorpAssets.UseVisualStyleBackColor = True
        '
        'rbtnPersonalAssets
        '
        Me.rbtnPersonalAssets.AutoSize = True
        Me.rbtnPersonalAssets.Location = New System.Drawing.Point(86, 18)
        Me.rbtnPersonalAssets.Name = "rbtnPersonalAssets"
        Me.rbtnPersonalAssets.Size = New System.Drawing.Size(66, 17)
        Me.rbtnPersonalAssets.TabIndex = 1
        Me.rbtnPersonalAssets.TabStop = True
        Me.rbtnPersonalAssets.Text = "Personal"
        Me.rbtnPersonalAssets.UseVisualStyleBackColor = True
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
        Me.btnScanCorpAssets.TabIndex = 5
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
        Me.btnScanPersonalAssets.TabIndex = 4
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
        Me.rbtnSortQuantity.TabIndex = 4
        Me.rbtnSortQuantity.TabStop = True
        Me.rbtnSortQuantity.Text = "Quantity"
        Me.rbtnSortQuantity.UseVisualStyleBackColor = True
        '
        'rbtnSortName
        '
        Me.rbtnSortName.AutoSize = True
        Me.rbtnSortName.Location = New System.Drawing.Point(10, 19)
        Me.rbtnSortName.Name = "rbtnSortName"
        Me.rbtnSortName.Size = New System.Drawing.Size(53, 17)
        Me.rbtnSortName.TabIndex = 3
        Me.rbtnSortName.TabStop = True
        Me.rbtnSortName.Text = "Name"
        Me.rbtnSortName.UseVisualStyleBackColor = True
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
        'btnCloseAssets
        '
        Me.btnCloseAssets.Location = New System.Drawing.Point(140, 274)
        Me.btnCloseAssets.Name = "btnCloseAssets"
        Me.btnCloseAssets.Size = New System.Drawing.Size(102, 27)
        Me.btnCloseAssets.TabIndex = 7
        Me.btnCloseAssets.Text = "Close"
        Me.btnCloseAssets.UseVisualStyleBackColor = True
        '
        'tabSearchSettings
        '
        Me.tabSearchSettings.Controls.Add(Me.rbtnBPMats)
        Me.tabSearchSettings.Controls.Add(Me.btnSaveSettings)
        Me.tabSearchSettings.Controls.Add(Me.btnSearchRefresh)
        Me.tabSearchSettings.Controls.Add(Me.rbtnAllItems)
        Me.tabSearchSettings.Controls.Add(Me.lblItemFilter)
        Me.tabSearchSettings.Controls.Add(Me.gbRawMaterials)
        Me.tabSearchSettings.Controls.Add(Me.gbManufacturedItems)
        Me.tabSearchSettings.Controls.Add(Me.btnResetItemFilter)
        Me.tabSearchSettings.Controls.Add(Me.txtItemFilter)
        Me.tabSearchSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabSearchSettings.Name = "tabSearchSettings"
        Me.tabSearchSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSearchSettings.Size = New System.Drawing.Size(276, 636)
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
        'lblItemFilter
        '
        Me.lblItemFilter.AutoSize = True
        Me.lblItemFilter.Location = New System.Drawing.Point(7, 39)
        Me.lblItemFilter.Name = "lblItemFilter"
        Me.lblItemFilter.Size = New System.Drawing.Size(55, 13)
        Me.lblItemFilter.TabIndex = 242
        Me.lblItemFilter.Text = "Item Filter:"
        '
        'gbRawMaterials
        '
        Me.gbRawMaterials.Controls.Add(Me.gbResearchEquipment)
        Me.gbRawMaterials.Controls.Add(Me.gbReactionMaterials)
        Me.gbRawMaterials.Controls.Add(Me.chkFactionMaterials)
        Me.gbRawMaterials.Controls.Add(Me.chkMolecularForgingTools)
        Me.gbRawMaterials.Controls.Add(Me.chkAdvancedProtectiveTechnology)
        Me.gbRawMaterials.Controls.Add(Me.chkBPCs)
        Me.gbRawMaterials.Controls.Add(Me.chkMisc)
        Me.gbRawMaterials.Controls.Add(Me.chkRawMaterials)
        Me.gbRawMaterials.Controls.Add(Me.chkMaterialResearchEqPrices)
        Me.gbRawMaterials.Controls.Add(Me.chkPlanetary)
        Me.gbRawMaterials.Controls.Add(Me.chkNamedComponents)
        Me.gbRawMaterials.Controls.Add(Me.chkGas)
        Me.gbRawMaterials.Controls.Add(Me.chkSalvage)
        Me.gbRawMaterials.Controls.Add(Me.chkIceProducts)
        Me.gbRawMaterials.Controls.Add(Me.chkMinerals)
        Me.gbRawMaterials.Location = New System.Drawing.Point(3, 92)
        Me.gbRawMaterials.Name = "gbRawMaterials"
        Me.gbRawMaterials.Size = New System.Drawing.Size(269, 246)
        Me.gbRawMaterials.TabIndex = 5
        Me.gbRawMaterials.TabStop = False
        '
        'gbResearchEquipment
        '
        Me.gbResearchEquipment.Controls.Add(Me.chkRDb)
        Me.gbResearchEquipment.Controls.Add(Me.chkAncientRelics)
        Me.gbResearchEquipment.Controls.Add(Me.chkDecryptors)
        Me.gbResearchEquipment.Controls.Add(Me.chkDatacores)
        Me.gbResearchEquipment.Location = New System.Drawing.Point(7, 202)
        Me.gbResearchEquipment.Name = "gbResearchEquipment"
        Me.gbResearchEquipment.Size = New System.Drawing.Size(254, 37)
        Me.gbResearchEquipment.TabIndex = 29
        Me.gbResearchEquipment.TabStop = False
        Me.gbResearchEquipment.Text = "Research Equipment"
        '
        'chkRDb
        '
        Me.chkRDb.AutoSize = True
        Me.chkRDb.Location = New System.Drawing.Point(203, 16)
        Me.chkRDb.Name = "chkRDb"
        Me.chkRDb.Size = New System.Drawing.Size(51, 17)
        Me.chkRDb.TabIndex = 24
        Me.chkRDb.Text = "R.Db"
        Me.chkRDb.UseVisualStyleBackColor = True
        '
        'chkAncientRelics
        '
        Me.chkAncientRelics.AutoSize = True
        Me.chkAncientRelics.Location = New System.Drawing.Point(151, 16)
        Me.chkAncientRelics.Name = "chkAncientRelics"
        Me.chkAncientRelics.Size = New System.Drawing.Size(55, 17)
        Me.chkAncientRelics.TabIndex = 7
        Me.chkAncientRelics.Text = "Relics"
        Me.chkAncientRelics.UseVisualStyleBackColor = True
        '
        'chkDecryptors
        '
        Me.chkDecryptors.AutoSize = True
        Me.chkDecryptors.Location = New System.Drawing.Point(78, 16)
        Me.chkDecryptors.Name = "chkDecryptors"
        Me.chkDecryptors.Size = New System.Drawing.Size(77, 17)
        Me.chkDecryptors.TabIndex = 4
        Me.chkDecryptors.Text = "Decryptors"
        Me.chkDecryptors.UseVisualStyleBackColor = True
        '
        'chkDatacores
        '
        Me.chkDatacores.AutoSize = True
        Me.chkDatacores.Location = New System.Drawing.Point(6, 16)
        Me.chkDatacores.Name = "chkDatacores"
        Me.chkDatacores.Size = New System.Drawing.Size(75, 17)
        Me.chkDatacores.TabIndex = 2
        Me.chkDatacores.Text = "Datacores"
        Me.chkDatacores.UseVisualStyleBackColor = True
        '
        'gbReactionMaterials
        '
        Me.gbReactionMaterials.Controls.Add(Me.chkAdvancedMats)
        Me.gbReactionMaterials.Controls.Add(Me.chkMolecularForgedMaterials)
        Me.gbReactionMaterials.Controls.Add(Me.chkProcessedMats)
        Me.gbReactionMaterials.Controls.Add(Me.chkBoosterMats)
        Me.gbReactionMaterials.Controls.Add(Me.chkPolymers)
        Me.gbReactionMaterials.Controls.Add(Me.chkRawMoonMats)
        Me.gbReactionMaterials.Location = New System.Drawing.Point(7, 132)
        Me.gbReactionMaterials.Name = "gbReactionMaterials"
        Me.gbReactionMaterials.Size = New System.Drawing.Size(254, 68)
        Me.gbReactionMaterials.TabIndex = 28
        Me.gbReactionMaterials.TabStop = False
        Me.gbReactionMaterials.Text = "Reaction Materials"
        '
        'chkAdvancedMats
        '
        Me.chkAdvancedMats.AutoSize = True
        Me.chkAdvancedMats.Location = New System.Drawing.Point(6, 15)
        Me.chkAdvancedMats.Name = "chkAdvancedMats"
        Me.chkAdvancedMats.Size = New System.Drawing.Size(105, 17)
        Me.chkAdvancedMats.TabIndex = 12
        Me.chkAdvancedMats.Text = "Advanced Moon"
        Me.chkAdvancedMats.UseVisualStyleBackColor = True
        '
        'chkMolecularForgedMaterials
        '
        Me.chkMolecularForgedMaterials.AutoSize = True
        Me.chkMolecularForgedMaterials.Location = New System.Drawing.Point(6, 32)
        Me.chkMolecularForgedMaterials.Name = "chkMolecularForgedMaterials"
        Me.chkMolecularForgedMaterials.Size = New System.Drawing.Size(108, 17)
        Me.chkMolecularForgedMaterials.TabIndex = 27
        Me.chkMolecularForgedMaterials.Text = "Molecular-Forged"
        Me.chkMolecularForgedMaterials.UseVisualStyleBackColor = True
        '
        'chkProcessedMats
        '
        Me.chkProcessedMats.AutoSize = True
        Me.chkProcessedMats.Location = New System.Drawing.Point(6, 49)
        Me.chkProcessedMats.Name = "chkProcessedMats"
        Me.chkProcessedMats.Size = New System.Drawing.Size(106, 17)
        Me.chkProcessedMats.TabIndex = 11
        Me.chkProcessedMats.Text = "Processed Moon"
        Me.chkProcessedMats.UseVisualStyleBackColor = True
        '
        'chkBoosterMats
        '
        Me.chkBoosterMats.AutoSize = True
        Me.chkBoosterMats.Location = New System.Drawing.Point(127, 15)
        Me.chkBoosterMats.Name = "chkBoosterMats"
        Me.chkBoosterMats.Size = New System.Drawing.Size(107, 17)
        Me.chkBoosterMats.TabIndex = 15
        Me.chkBoosterMats.Text = "Booster Materials"
        Me.chkBoosterMats.UseVisualStyleBackColor = True
        '
        'chkPolymers
        '
        Me.chkPolymers.AutoSize = True
        Me.chkPolymers.Location = New System.Drawing.Point(127, 32)
        Me.chkPolymers.Name = "chkPolymers"
        Me.chkPolymers.Size = New System.Drawing.Size(108, 17)
        Me.chkPolymers.TabIndex = 8
        Me.chkPolymers.Text = "Polymer Materials"
        Me.chkPolymers.UseVisualStyleBackColor = True
        '
        'chkRawMoonMats
        '
        Me.chkRawMoonMats.AutoSize = True
        Me.chkRawMoonMats.Location = New System.Drawing.Point(127, 49)
        Me.chkRawMoonMats.Name = "chkRawMoonMats"
        Me.chkRawMoonMats.Size = New System.Drawing.Size(123, 17)
        Me.chkRawMoonMats.TabIndex = 10
        Me.chkRawMoonMats.Text = "Raw Moon Materials"
        Me.chkRawMoonMats.UseVisualStyleBackColor = True
        '
        'chkFactionMaterials
        '
        Me.chkFactionMaterials.AutoSize = True
        Me.chkFactionMaterials.Location = New System.Drawing.Point(155, 58)
        Me.chkFactionMaterials.Name = "chkFactionMaterials"
        Me.chkFactionMaterials.Size = New System.Drawing.Size(106, 17)
        Me.chkFactionMaterials.TabIndex = 26
        Me.chkFactionMaterials.Text = "Faction Materials"
        Me.chkFactionMaterials.UseVisualStyleBackColor = True
        '
        'chkMolecularForgingTools
        '
        Me.chkMolecularForgingTools.AutoSize = True
        Me.chkMolecularForgingTools.Location = New System.Drawing.Point(13, 58)
        Me.chkMolecularForgingTools.Name = "chkMolecularForgingTools"
        Me.chkMolecularForgingTools.Size = New System.Drawing.Size(139, 17)
        Me.chkMolecularForgingTools.TabIndex = 25
        Me.chkMolecularForgingTools.Text = "Molecular-Forging Tools"
        Me.chkMolecularForgingTools.UseVisualStyleBackColor = True
        '
        'chkAdvancedProtectiveTechnology
        '
        Me.chkAdvancedProtectiveTechnology.AutoSize = True
        Me.chkAdvancedProtectiveTechnology.Location = New System.Drawing.Point(13, 24)
        Me.chkAdvancedProtectiveTechnology.Name = "chkAdvancedProtectiveTechnology"
        Me.chkAdvancedProtectiveTechnology.Size = New System.Drawing.Size(185, 17)
        Me.chkAdvancedProtectiveTechnology.TabIndex = 23
        Me.chkAdvancedProtectiveTechnology.Text = "Advanced Protective Technology"
        Me.chkAdvancedProtectiveTechnology.UseVisualStyleBackColor = True
        '
        'chkBPCs
        '
        Me.chkBPCs.AutoSize = True
        Me.chkBPCs.Location = New System.Drawing.Point(155, 109)
        Me.chkBPCs.Name = "chkBPCs"
        Me.chkBPCs.Size = New System.Drawing.Size(102, 17)
        Me.chkBPCs.TabIndex = 21
        Me.chkBPCs.Text = "Blueprint Copies"
        Me.chkBPCs.UseVisualStyleBackColor = True
        '
        'chkMisc
        '
        Me.chkMisc.AutoSize = True
        Me.chkMisc.Location = New System.Drawing.Point(87, 109)
        Me.chkMisc.Name = "chkMisc"
        Me.chkMisc.Size = New System.Drawing.Size(51, 17)
        Me.chkMisc.TabIndex = 20
        Me.chkMisc.Text = "Misc."
        Me.chkMisc.UseVisualStyleBackColor = True
        '
        'chkRawMaterials
        '
        Me.chkRawMaterials.AutoSize = True
        Me.chkRawMaterials.Location = New System.Drawing.Point(155, 92)
        Me.chkRawMaterials.Name = "chkRawMaterials"
        Me.chkRawMaterials.Size = New System.Drawing.Size(93, 17)
        Me.chkRawMaterials.TabIndex = 19
        Me.chkRawMaterials.Text = "Raw Materials"
        Me.chkRawMaterials.UseVisualStyleBackColor = True
        '
        'chkMaterialResearchEqPrices
        '
        Me.chkMaterialResearchEqPrices.AutoSize = True
        Me.chkMaterialResearchEqPrices.BackColor = System.Drawing.Color.White
        Me.chkMaterialResearchEqPrices.Location = New System.Drawing.Point(6, 1)
        Me.chkMaterialResearchEqPrices.Name = "chkMaterialResearchEqPrices"
        Me.chkMaterialResearchEqPrices.Size = New System.Drawing.Size(179, 17)
        Me.chkMaterialResearchEqPrices.TabIndex = 18
        Me.chkMaterialResearchEqPrices.Text = "Materials && Research Equipment"
        Me.chkMaterialResearchEqPrices.UseVisualStyleBackColor = False
        '
        'chkPlanetary
        '
        Me.chkPlanetary.AutoSize = True
        Me.chkPlanetary.Location = New System.Drawing.Point(13, 92)
        Me.chkPlanetary.Name = "chkPlanetary"
        Me.chkPlanetary.Size = New System.Drawing.Size(115, 17)
        Me.chkPlanetary.TabIndex = 17
        Me.chkPlanetary.Text = "Planetary Materials"
        Me.chkPlanetary.UseVisualStyleBackColor = True
        '
        'chkNamedComponents
        '
        Me.chkNamedComponents.AutoSize = True
        Me.chkNamedComponents.Location = New System.Drawing.Point(13, 75)
        Me.chkNamedComponents.Name = "chkNamedComponents"
        Me.chkNamedComponents.Size = New System.Drawing.Size(122, 17)
        Me.chkNamedComponents.TabIndex = 13
        Me.chkNamedComponents.Text = "Named Components"
        Me.chkNamedComponents.UseVisualStyleBackColor = True
        '
        'chkGas
        '
        Me.chkGas.AutoSize = True
        Me.chkGas.Location = New System.Drawing.Point(13, 41)
        Me.chkGas.Name = "chkGas"
        Me.chkGas.Size = New System.Drawing.Size(120, 17)
        Me.chkGas.TabIndex = 9
        Me.chkGas.Text = "Gas Cloud Materials"
        Me.chkGas.UseVisualStyleBackColor = True
        '
        'chkSalvage
        '
        Me.chkSalvage.AutoSize = True
        Me.chkSalvage.Location = New System.Drawing.Point(13, 109)
        Me.chkSalvage.Name = "chkSalvage"
        Me.chkSalvage.Size = New System.Drawing.Size(65, 17)
        Me.chkSalvage.TabIndex = 5
        Me.chkSalvage.Text = "Salvage"
        Me.chkSalvage.UseVisualStyleBackColor = True
        '
        'chkIceProducts
        '
        Me.chkIceProducts.AutoSize = True
        Me.chkIceProducts.Location = New System.Drawing.Point(155, 41)
        Me.chkIceProducts.Name = "chkIceProducts"
        Me.chkIceProducts.Size = New System.Drawing.Size(86, 17)
        Me.chkIceProducts.TabIndex = 1
        Me.chkIceProducts.Text = "Ice Products"
        Me.chkIceProducts.UseVisualStyleBackColor = True
        '
        'chkMinerals
        '
        Me.chkMinerals.AutoSize = True
        Me.chkMinerals.Location = New System.Drawing.Point(155, 75)
        Me.chkMinerals.Name = "chkMinerals"
        Me.chkMinerals.Size = New System.Drawing.Size(65, 17)
        Me.chkMinerals.TabIndex = 0
        Me.chkMinerals.Text = "Minerals"
        Me.chkMinerals.UseVisualStyleBackColor = True
        '
        'gbManufacturedItems
        '
        Me.gbManufacturedItems.Controls.Add(Me.gbComponents)
        Me.gbManufacturedItems.Controls.Add(Me.gbItems)
        Me.gbManufacturedItems.Controls.Add(Me.chkManufacturedItems)
        Me.gbManufacturedItems.Controls.Add(Me.gbPricesTech)
        Me.gbManufacturedItems.Location = New System.Drawing.Point(3, 338)
        Me.gbManufacturedItems.Name = "gbManufacturedItems"
        Me.gbManufacturedItems.Size = New System.Drawing.Size(269, 308)
        Me.gbManufacturedItems.TabIndex = 6
        Me.gbManufacturedItems.TabStop = False
        '
        'gbComponents
        '
        Me.gbComponents.Controls.Add(Me.chkRAM)
        Me.gbComponents.Controls.Add(Me.chkStructureComponents)
        Me.gbComponents.Controls.Add(Me.chkProtectiveComponents)
        Me.gbComponents.Controls.Add(Me.chkFuelBlocks)
        Me.gbComponents.Controls.Add(Me.chkSubsystemComponents)
        Me.gbComponents.Controls.Add(Me.chkAdvancedComponents)
        Me.gbComponents.Controls.Add(Me.chkCapitalShipComponents)
        Me.gbComponents.Controls.Add(Me.chkCapT2Components)
        Me.gbComponents.Location = New System.Drawing.Point(6, 154)
        Me.gbComponents.Name = "gbComponents"
        Me.gbComponents.Size = New System.Drawing.Size(159, 140)
        Me.gbComponents.TabIndex = 1
        Me.gbComponents.TabStop = False
        Me.gbComponents.Text = "Components"
        '
        'chkRAM
        '
        Me.chkRAM.AutoSize = True
        Me.chkRAM.Location = New System.Drawing.Point(87, 118)
        Me.chkRAM.Name = "chkRAM"
        Me.chkRAM.Size = New System.Drawing.Size(64, 17)
        Me.chkRAM.TabIndex = 1
        Me.chkRAM.Text = "R.A.M.s"
        Me.chkRAM.UseVisualStyleBackColor = True
        '
        'chkStructureComponents
        '
        Me.chkStructureComponents.AutoSize = True
        Me.chkStructureComponents.Location = New System.Drawing.Point(8, 84)
        Me.chkStructureComponents.Name = "chkStructureComponents"
        Me.chkStructureComponents.Size = New System.Drawing.Size(131, 17)
        Me.chkStructureComponents.TabIndex = 18
        Me.chkStructureComponents.Text = "Structure Components"
        Me.chkStructureComponents.UseVisualStyleBackColor = True
        '
        'chkProtectiveComponents
        '
        Me.chkProtectiveComponents.AutoSize = True
        Me.chkProtectiveComponents.Location = New System.Drawing.Point(8, 101)
        Me.chkProtectiveComponents.Name = "chkProtectiveComponents"
        Me.chkProtectiveComponents.Size = New System.Drawing.Size(136, 17)
        Me.chkProtectiveComponents.TabIndex = 20
        Me.chkProtectiveComponents.Text = "Protective Components"
        Me.chkProtectiveComponents.UseVisualStyleBackColor = True
        '
        'chkFuelBlocks
        '
        Me.chkFuelBlocks.AutoSize = True
        Me.chkFuelBlocks.Location = New System.Drawing.Point(8, 118)
        Me.chkFuelBlocks.Name = "chkFuelBlocks"
        Me.chkFuelBlocks.Size = New System.Drawing.Size(81, 17)
        Me.chkFuelBlocks.TabIndex = 3
        Me.chkFuelBlocks.Text = "Fuel Blocks"
        Me.chkFuelBlocks.UseVisualStyleBackColor = True
        '
        'chkSubsystemComponents
        '
        Me.chkSubsystemComponents.AutoSize = True
        Me.chkSubsystemComponents.Location = New System.Drawing.Point(8, 67)
        Me.chkSubsystemComponents.Name = "chkSubsystemComponents"
        Me.chkSubsystemComponents.Size = New System.Drawing.Size(139, 17)
        Me.chkSubsystemComponents.TabIndex = 5
        Me.chkSubsystemComponents.Text = "Subsystem Components"
        Me.chkSubsystemComponents.UseVisualStyleBackColor = True
        '
        'chkAdvancedComponents
        '
        Me.chkAdvancedComponents.AutoSize = True
        Me.chkAdvancedComponents.Location = New System.Drawing.Point(8, 50)
        Me.chkAdvancedComponents.Name = "chkAdvancedComponents"
        Me.chkAdvancedComponents.Size = New System.Drawing.Size(137, 17)
        Me.chkAdvancedComponents.TabIndex = 4
        Me.chkAdvancedComponents.Text = "Advanced Components"
        Me.chkAdvancedComponents.UseVisualStyleBackColor = True
        '
        'chkCapitalShipComponents
        '
        Me.chkCapitalShipComponents.AutoSize = True
        Me.chkCapitalShipComponents.Location = New System.Drawing.Point(8, 33)
        Me.chkCapitalShipComponents.Name = "chkCapitalShipComponents"
        Me.chkCapitalShipComponents.Size = New System.Drawing.Size(144, 17)
        Me.chkCapitalShipComponents.TabIndex = 3
        Me.chkCapitalShipComponents.Text = "Capital Ship Components"
        Me.chkCapitalShipComponents.UseVisualStyleBackColor = True
        '
        'chkCapT2Components
        '
        Me.chkCapT2Components.AutoSize = True
        Me.chkCapT2Components.Location = New System.Drawing.Point(8, 16)
        Me.chkCapT2Components.Name = "chkCapT2Components"
        Me.chkCapT2Components.Size = New System.Drawing.Size(145, 17)
        Me.chkCapT2Components.TabIndex = 2
        Me.chkCapT2Components.Text = "Adv. Capital Components"
        Me.chkCapT2Components.UseVisualStyleBackColor = True
        '
        'gbItems
        '
        Me.gbItems.Controls.Add(Me.chkNobuild)
        Me.gbItems.Controls.Add(Me.chkImplants)
        Me.gbItems.Controls.Add(Me.chkBoosters)
        Me.gbItems.Controls.Add(Me.chkDeployables)
        Me.gbItems.Controls.Add(Me.chkStructureModules)
        Me.gbItems.Controls.Add(Me.chkCelestials)
        Me.gbItems.Controls.Add(Me.chkStructures)
        Me.gbItems.Controls.Add(Me.chkStructureRigs)
        Me.gbItems.Controls.Add(Me.chkSubsystems)
        Me.gbItems.Controls.Add(Me.chkRigs)
        Me.gbItems.Controls.Add(Me.cmbPriceChargeTypes)
        Me.gbItems.Controls.Add(Me.chkCharges)
        Me.gbItems.Controls.Add(Me.cmbPriceShipTypes)
        Me.gbItems.Controls.Add(Me.chkDrones)
        Me.gbItems.Controls.Add(Me.chkModules)
        Me.gbItems.Controls.Add(Me.chkShips)
        Me.gbItems.Location = New System.Drawing.Point(6, 19)
        Me.gbItems.Name = "gbItems"
        Me.gbItems.Size = New System.Drawing.Size(255, 133)
        Me.gbItems.TabIndex = 0
        Me.gbItems.TabStop = False
        Me.gbItems.Text = "Items"
        '
        'chkNobuild
        '
        Me.chkNobuild.AutoSize = True
        Me.chkNobuild.Location = New System.Drawing.Point(182, 111)
        Me.chkNobuild.Name = "chkNobuild"
        Me.chkNobuild.Size = New System.Drawing.Size(66, 17)
        Me.chkNobuild.TabIndex = 21
        Me.chkNobuild.Text = "No Build"
        Me.chkNobuild.UseVisualStyleBackColor = True
        '
        'chkImplants
        '
        Me.chkImplants.AutoSize = True
        Me.chkImplants.Location = New System.Drawing.Point(119, 111)
        Me.chkImplants.Name = "chkImplants"
        Me.chkImplants.Size = New System.Drawing.Size(65, 17)
        Me.chkImplants.TabIndex = 251
        Me.chkImplants.Text = "Implants"
        Me.chkImplants.UseVisualStyleBackColor = True
        '
        'chkBoosters
        '
        Me.chkBoosters.AutoSize = True
        Me.chkBoosters.Location = New System.Drawing.Point(182, 77)
        Me.chkBoosters.Name = "chkBoosters"
        Me.chkBoosters.Size = New System.Drawing.Size(67, 17)
        Me.chkBoosters.TabIndex = 6
        Me.chkBoosters.Text = "Boosters"
        Me.chkBoosters.UseVisualStyleBackColor = True
        '
        'chkDeployables
        '
        Me.chkDeployables.AutoSize = True
        Me.chkDeployables.Location = New System.Drawing.Point(91, 77)
        Me.chkDeployables.Name = "chkDeployables"
        Me.chkDeployables.Size = New System.Drawing.Size(84, 17)
        Me.chkDeployables.TabIndex = 253
        Me.chkDeployables.Text = "Deployables"
        Me.chkDeployables.UseVisualStyleBackColor = True
        '
        'chkStructureModules
        '
        Me.chkStructureModules.AutoSize = True
        Me.chkStructureModules.Location = New System.Drawing.Point(9, 111)
        Me.chkStructureModules.Name = "chkStructureModules"
        Me.chkStructureModules.Size = New System.Drawing.Size(112, 17)
        Me.chkStructureModules.TabIndex = 254
        Me.chkStructureModules.Text = "Structure Modules"
        Me.chkStructureModules.UseVisualStyleBackColor = True
        '
        'chkCelestials
        '
        Me.chkCelestials.AutoSize = True
        Me.chkCelestials.Location = New System.Drawing.Point(182, 94)
        Me.chkCelestials.Name = "chkCelestials"
        Me.chkCelestials.Size = New System.Drawing.Size(70, 17)
        Me.chkCelestials.TabIndex = 252
        Me.chkCelestials.Text = "Celestials"
        Me.chkCelestials.UseVisualStyleBackColor = True
        '
        'chkStructures
        '
        Me.chkStructures.AutoSize = True
        Me.chkStructures.Location = New System.Drawing.Point(9, 94)
        Me.chkStructures.Name = "chkStructures"
        Me.chkStructures.Size = New System.Drawing.Size(74, 17)
        Me.chkStructures.TabIndex = 7
        Me.chkStructures.Text = "Structures"
        Me.chkStructures.UseVisualStyleBackColor = True
        '
        'chkStructureRigs
        '
        Me.chkStructureRigs.AutoSize = True
        Me.chkStructureRigs.Location = New System.Drawing.Point(91, 94)
        Me.chkStructureRigs.Name = "chkStructureRigs"
        Me.chkStructureRigs.Size = New System.Drawing.Size(93, 17)
        Me.chkStructureRigs.TabIndex = 16
        Me.chkStructureRigs.Text = "Structure Rigs"
        Me.chkStructureRigs.UseVisualStyleBackColor = True
        '
        'chkSubsystems
        '
        Me.chkSubsystems.AutoSize = True
        Me.chkSubsystems.Location = New System.Drawing.Point(9, 77)
        Me.chkSubsystems.Name = "chkSubsystems"
        Me.chkSubsystems.Size = New System.Drawing.Size(82, 17)
        Me.chkSubsystems.TabIndex = 3
        Me.chkSubsystems.Text = "Subsystems"
        Me.chkSubsystems.UseVisualStyleBackColor = True
        '
        'chkRigs
        '
        Me.chkRigs.AutoSize = True
        Me.chkRigs.Location = New System.Drawing.Point(182, 60)
        Me.chkRigs.Name = "chkRigs"
        Me.chkRigs.Size = New System.Drawing.Size(47, 17)
        Me.chkRigs.TabIndex = 5
        Me.chkRigs.Text = "Rigs"
        Me.chkRigs.UseVisualStyleBackColor = True
        '
        'cmbPriceChargeTypes
        '
        Me.cmbPriceChargeTypes.FormattingEnabled = True
        Me.cmbPriceChargeTypes.Location = New System.Drawing.Point(70, 35)
        Me.cmbPriceChargeTypes.Name = "cmbPriceChargeTypes"
        Me.cmbPriceChargeTypes.Size = New System.Drawing.Size(178, 21)
        Me.cmbPriceChargeTypes.TabIndex = 10
        Me.cmbPriceChargeTypes.Text = "All Charge Types"
        '
        'chkCharges
        '
        Me.chkCharges.AutoSize = True
        Me.chkCharges.Location = New System.Drawing.Point(10, 37)
        Me.chkCharges.Name = "chkCharges"
        Me.chkCharges.Size = New System.Drawing.Size(65, 17)
        Me.chkCharges.TabIndex = 8
        Me.chkCharges.Text = "Charges"
        Me.chkCharges.UseVisualStyleBackColor = True
        '
        'cmbPriceShipTypes
        '
        Me.cmbPriceShipTypes.FormattingEnabled = True
        Me.cmbPriceShipTypes.Location = New System.Drawing.Point(70, 12)
        Me.cmbPriceShipTypes.Name = "cmbPriceShipTypes"
        Me.cmbPriceShipTypes.Size = New System.Drawing.Size(178, 21)
        Me.cmbPriceShipTypes.TabIndex = 9
        Me.cmbPriceShipTypes.Text = "All Ship Types"
        '
        'chkDrones
        '
        Me.chkDrones.AutoSize = True
        Me.chkDrones.Location = New System.Drawing.Point(91, 60)
        Me.chkDrones.Name = "chkDrones"
        Me.chkDrones.Size = New System.Drawing.Size(60, 17)
        Me.chkDrones.TabIndex = 2
        Me.chkDrones.Text = "Drones"
        Me.chkDrones.UseVisualStyleBackColor = True
        '
        'chkModules
        '
        Me.chkModules.AutoSize = True
        Me.chkModules.Location = New System.Drawing.Point(9, 60)
        Me.chkModules.Name = "chkModules"
        Me.chkModules.Size = New System.Drawing.Size(66, 17)
        Me.chkModules.TabIndex = 1
        Me.chkModules.Text = "Modules"
        Me.chkModules.UseVisualStyleBackColor = True
        '
        'chkShips
        '
        Me.chkShips.AutoSize = True
        Me.chkShips.Location = New System.Drawing.Point(10, 14)
        Me.chkShips.Name = "chkShips"
        Me.chkShips.Size = New System.Drawing.Size(52, 17)
        Me.chkShips.TabIndex = 4
        Me.chkShips.Text = "Ships"
        Me.chkShips.UseVisualStyleBackColor = True
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
        'gbPricesTech
        '
        Me.gbPricesTech.Controls.Add(Me.chkItemsT4)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT6)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT5)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT3)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT2)
        Me.gbPricesTech.Controls.Add(Me.chkItemsT1)
        Me.gbPricesTech.Location = New System.Drawing.Point(167, 154)
        Me.gbPricesTech.Name = "gbPricesTech"
        Me.gbPricesTech.Size = New System.Drawing.Size(94, 140)
        Me.gbPricesTech.TabIndex = 0
        Me.gbPricesTech.TabStop = False
        '
        'chkItemsT4
        '
        Me.chkItemsT4.AutoSize = True
        Me.chkItemsT4.Enabled = False
        Me.chkItemsT4.Location = New System.Drawing.Point(6, 67)
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
        Me.chkItemsT6.Location = New System.Drawing.Point(6, 101)
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
        Me.chkItemsT5.Location = New System.Drawing.Point(6, 84)
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
        Me.chkItemsT3.Location = New System.Drawing.Point(6, 50)
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
        Me.chkItemsT2.Location = New System.Drawing.Point(6, 33)
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
        Me.chkItemsT1.Location = New System.Drawing.Point(6, 16)
        Me.chkItemsT1.Name = "chkItemsT1"
        Me.chkItemsT1.Size = New System.Drawing.Size(60, 17)
        Me.chkItemsT1.TabIndex = 0
        Me.chkItemsT1.Text = "Tech 1"
        Me.chkItemsT1.UseVisualStyleBackColor = True
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
        'AssetTree
        '
        Me.AssetTree.CheckBoxes = True
        Me.AssetTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
        Me.AssetTree.Location = New System.Drawing.Point(3, 3)
        Me.AssetTree.Name = "AssetTree"
        Me.AssetTree.Size = New System.Drawing.Size(346, 660)
        Me.AssetTree.TabIndex = 252
        '
        'Timer1
        '
        '
        'ttMain
        '
        Me.ttMain.IsBalloon = True
        '
        'chkToggle
        '
        Me.chkToggle.AutoSize = True
        Me.chkToggle.BackColor = System.Drawing.Color.Transparent
        Me.chkToggle.Location = New System.Drawing.Point(306, 9)
        Me.chkToggle.Name = "chkToggle"
        Me.chkToggle.Size = New System.Drawing.Size(15, 14)
        Me.chkToggle.TabIndex = 254
        Me.chkToggle.UseVisualStyleBackColor = False
        '
        'btnToggleRetract
        '
        Me.btnToggleRetract.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleRetract.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnToggleRetract.Image = CType(resources.GetObject("btnToggleRetract.Image"), System.Drawing.Image)
        Me.btnToggleRetract.Location = New System.Drawing.Point(324, 3)
        Me.btnToggleRetract.Name = "btnToggleRetract"
        Me.btnToggleRetract.Size = New System.Drawing.Size(25, 25)
        Me.btnToggleRetract.TabIndex = 253
        Me.btnToggleRetract.UseVisualStyleBackColor = False
        '
        'btnCheckToggle
        '
        Me.btnCheckToggle.Enabled = False
        Me.btnCheckToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckToggle.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnCheckToggle.Location = New System.Drawing.Point(300, 3)
        Me.btnCheckToggle.Name = "btnCheckToggle"
        Me.btnCheckToggle.Size = New System.Drawing.Size(25, 25)
        Me.btnCheckToggle.TabIndex = 250
        Me.btnCheckToggle.UseVisualStyleBackColor = False
        '
        'btnToggleExpand
        '
        Me.btnToggleExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleExpand.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnToggleExpand.Image = CType(resources.GetObject("btnToggleExpand.Image"), System.Drawing.Image)
        Me.btnToggleExpand.Location = New System.Drawing.Point(324, 3)
        Me.btnToggleExpand.Name = "btnToggleExpand"
        Me.btnToggleExpand.Size = New System.Drawing.Size(25, 25)
        Me.btnToggleExpand.TabIndex = 255
        Me.btnToggleExpand.UseVisualStyleBackColor = False
        '
        'AssetViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnToggleExpand)
        Me.Controls.Add(Me.chkToggle)
        Me.Controls.Add(Me.btnCheckToggle)
        Me.Controls.Add(Me.btnToggleRetract)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.AssetTree)
        Me.Name = "AssetViewer"
        Me.Size = New System.Drawing.Size(640, 667)
        Me.tabMain.ResumeLayout(False)
        Me.tabAssetMain.ResumeLayout(False)
        Me.gbAssetTypes.ResumeLayout(False)
        Me.gbAssetTypes.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSortOptions.ResumeLayout(False)
        Me.gbSortOptions.PerformLayout()
        CType(Me.picSpaceFiller, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSearchSettings.ResumeLayout(False)
        Me.tabSearchSettings.PerformLayout()
        Me.gbRawMaterials.ResumeLayout(False)
        Me.gbRawMaterials.PerformLayout()
        Me.gbResearchEquipment.ResumeLayout(False)
        Me.gbResearchEquipment.PerformLayout()
        Me.gbReactionMaterials.ResumeLayout(False)
        Me.gbReactionMaterials.PerformLayout()
        Me.gbManufacturedItems.ResumeLayout(False)
        Me.gbManufacturedItems.PerformLayout()
        Me.gbComponents.ResumeLayout(False)
        Me.gbComponents.PerformLayout()
        Me.gbItems.ResumeLayout(False)
        Me.gbItems.PerformLayout()
        Me.gbPricesTech.ResumeLayout(False)
        Me.gbPricesTech.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabAssetMain As TabPage
    Friend WithEvents btnSaveMainSettings As Button
    Friend WithEvents gbAssetTypes As GroupBox
    Friend WithEvents rbtnAllAssets As RadioButton
    Friend WithEvents rbtnCorpAssets As RadioButton
    Friend WithEvents rbtnPersonalAssets As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblReloadCorpAssets As Label
    Friend WithEvents lblReloadPersonalAssets As Label
    Friend WithEvents btnScanCorpAssets As Button
    Friend WithEvents lblReloadCorpAssets2 As Label
    Friend WithEvents btnScanPersonalAssets As Button
    Friend WithEvents lblReloadPersonalAssets1 As Label
    Friend WithEvents gbSortOptions As GroupBox
    Friend WithEvents rbtnSortQuantity As RadioButton
    Friend WithEvents rbtnSortName As RadioButton
    Friend WithEvents picSpaceFiller As PictureBox
    Friend WithEvents btnCloseAssets As Button
    Friend WithEvents tabSearchSettings As TabPage
    Friend WithEvents rbtnBPMats As RadioButton
    Friend WithEvents btnSaveSettings As Button
    Friend WithEvents btnSearchRefresh As Button
    Friend WithEvents rbtnAllItems As RadioButton
    Friend WithEvents lblItemFilter As Label
    Friend WithEvents btnResetItemFilter As Button
    Friend WithEvents txtItemFilter As TextBox
    Friend WithEvents gbManufacturedItems As GroupBox
    Friend WithEvents gbItems As GroupBox
    Friend WithEvents chkStructureModules As CheckBox
    Friend WithEvents chkImplants As CheckBox
    Friend WithEvents chkCelestials As CheckBox
    Friend WithEvents chkRAM As CheckBox
    Friend WithEvents chkFuelBlocks As CheckBox
    Friend WithEvents chkStructureRigs As CheckBox
    Friend WithEvents cmbPriceChargeTypes As ComboBox
    Friend WithEvents chkCharges As CheckBox
    Friend WithEvents chkStructures As CheckBox
    Friend WithEvents chkBoosters As CheckBox
    Friend WithEvents chkRigs As CheckBox
    Friend WithEvents cmbPriceShipTypes As ComboBox
    Friend WithEvents chkSubsystems As CheckBox
    Friend WithEvents chkDrones As CheckBox
    Friend WithEvents chkModules As CheckBox
    Friend WithEvents chkShips As CheckBox
    Friend WithEvents chkDeployables As CheckBox
    Friend WithEvents gbPricesTech As GroupBox
    Friend WithEvents chkItemsT4 As CheckBox
    Friend WithEvents chkItemsT6 As CheckBox
    Friend WithEvents chkItemsT5 As CheckBox
    Friend WithEvents chkItemsT3 As CheckBox
    Friend WithEvents chkItemsT2 As CheckBox
    Friend WithEvents chkItemsT1 As CheckBox
    Friend WithEvents chkManufacturedItems As CheckBox
    Friend WithEvents gbComponents As GroupBox
    Friend WithEvents chkStructureComponents As CheckBox
    Friend WithEvents chkSubsystemComponents As CheckBox
    Friend WithEvents chkAdvancedComponents As CheckBox
    Friend WithEvents chkCapitalShipComponents As CheckBox
    Friend WithEvents chkCapT2Components As CheckBox
    Friend WithEvents gbRawMaterials As GroupBox
    Friend WithEvents chkBPCs As CheckBox
    Friend WithEvents chkMisc As CheckBox
    Friend WithEvents chkRawMaterials As CheckBox
    Friend WithEvents chkMaterialResearchEqPrices As CheckBox
    Friend WithEvents chkPlanetary As CheckBox
    Friend WithEvents chkBoosterMats As CheckBox
    Friend WithEvents chkNamedComponents As CheckBox
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
    Friend WithEvents AssetTree As TreeView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents chkToggle As CheckBox
    Friend WithEvents btnToggleRetract As Button
    Friend WithEvents btnCheckToggle As Button
    Friend WithEvents btnToggleExpand As Button
    Friend WithEvents chkAdvancedProtectiveTechnology As CheckBox
    Friend WithEvents chkRDb As CheckBox
    Friend WithEvents chkMolecularForgingTools As CheckBox
    Friend WithEvents chkFactionMaterials As CheckBox
    Friend WithEvents chkMolecularForgedMaterials As CheckBox
    Friend WithEvents gbResearchEquipment As GroupBox
    Friend WithEvents gbReactionMaterials As GroupBox
    Friend WithEvents chkProtectiveComponents As CheckBox
    Friend WithEvents chkNobuild As CheckBox
End Class
