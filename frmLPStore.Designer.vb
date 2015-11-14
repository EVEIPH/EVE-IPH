<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLPStore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLPStore))
        Me.gbRewardType = New System.Windows.Forms.GroupBox()
        Me.rbtnModules = New System.Windows.Forms.RadioButton()
        Me.rbtnShips = New System.Windows.Forms.RadioButton()
        Me.rbtnDrones = New System.Windows.Forms.RadioButton()
        Me.rbtnCommodities = New System.Windows.Forms.RadioButton()
        Me.rbtnSkills = New System.Windows.Forms.RadioButton()
        Me.rbtnBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnAmmoCharge = New System.Windows.Forms.RadioButton()
        Me.rbtnImplants = New System.Windows.Forms.RadioButton()
        Me.rbtnAll = New System.Windows.Forms.RadioButton()
        Me.rbtnApparel = New System.Windows.Forms.RadioButton()
        Me.rbtnDeployable = New System.Windows.Forms.RadioButton()
        Me.gbCorpRace = New System.Windows.Forms.GroupBox()
        Me.chkRacePirate = New System.Windows.Forms.CheckBox()
        Me.chkRaceMinmatar = New System.Windows.Forms.CheckBox()
        Me.chkRaceGallente = New System.Windows.Forms.CheckBox()
        Me.chkRaceCaldari = New System.Windows.Forms.CheckBox()
        Me.chkRaceAmarr = New System.Windows.Forms.CheckBox()
        Me.gbSearchOptions = New System.Windows.Forms.GroupBox()
        Me.chkHighlightCorps = New System.Windows.Forms.CheckBox()
        Me.rbtnCorpswStanding = New System.Windows.Forms.RadioButton()
        Me.rbtnAllCorps = New System.Windows.Forms.RadioButton()
        Me.gbItemFilter = New System.Windows.Forms.GroupBox()
        Me.btnResetItemFilterSearch = New System.Windows.Forms.Button()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
        Me.gbReqItemFilter = New System.Windows.Forms.GroupBox()
        Me.btnResetReqItemSearch = New System.Windows.Forms.Button()
        Me.txtReqItemFilter = New System.Windows.Forms.TextBox()
        Me.gbLPCost = New System.Windows.Forms.GroupBox()
        Me.txtLPGreaterThan = New System.Windows.Forms.TextBox()
        Me.lblLPGreaterThan = New System.Windows.Forms.Label()
        Me.txtLPLessThan = New System.Windows.Forms.TextBox()
        Me.lblLPLessThan = New System.Windows.Forms.Label()
        Me.gbISKCost = New System.Windows.Forms.GroupBox()
        Me.txtISKGreaterThan = New System.Windows.Forms.TextBox()
        Me.lblISKGreaterThan = New System.Windows.Forms.Label()
        Me.txtISKLessThan = New System.Windows.Forms.TextBox()
        Me.lblISKLessThan = New System.Windows.Forms.Label()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.LPStoreItemImages = New System.Windows.Forms.ImageList(Me.components)
        Me.lstCorporations = New EVE_Isk_per_Hour.MyListView()
        Me.lstStoreItems = New EVE_Isk_per_Hour.MyListView()
        Me.gbRewardType.SuspendLayout()
        Me.gbCorpRace.SuspendLayout()
        Me.gbSearchOptions.SuspendLayout()
        Me.gbItemFilter.SuspendLayout()
        Me.gbReqItemFilter.SuspendLayout()
        Me.gbLPCost.SuspendLayout()
        Me.gbISKCost.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbRewardType
        '
        Me.gbRewardType.Controls.Add(Me.rbtnModules)
        Me.gbRewardType.Controls.Add(Me.rbtnShips)
        Me.gbRewardType.Controls.Add(Me.rbtnDrones)
        Me.gbRewardType.Controls.Add(Me.rbtnCommodities)
        Me.gbRewardType.Controls.Add(Me.rbtnSkills)
        Me.gbRewardType.Controls.Add(Me.rbtnBlueprints)
        Me.gbRewardType.Controls.Add(Me.rbtnAmmoCharge)
        Me.gbRewardType.Controls.Add(Me.rbtnImplants)
        Me.gbRewardType.Controls.Add(Me.rbtnAll)
        Me.gbRewardType.Controls.Add(Me.rbtnApparel)
        Me.gbRewardType.Controls.Add(Me.rbtnDeployable)
        Me.gbRewardType.Location = New System.Drawing.Point(12, 396)
        Me.gbRewardType.Name = "gbRewardType"
        Me.gbRewardType.Size = New System.Drawing.Size(204, 125)
        Me.gbRewardType.TabIndex = 37
        Me.gbRewardType.TabStop = False
        Me.gbRewardType.Text = "Reward Type"
        '
        'rbtnModules
        '
        Me.rbtnModules.AutoSize = True
        Me.rbtnModules.Location = New System.Drawing.Point(9, 85)
        Me.rbtnModules.Name = "rbtnModules"
        Me.rbtnModules.Size = New System.Drawing.Size(65, 17)
        Me.rbtnModules.TabIndex = 12
        Me.rbtnModules.TabStop = True
        Me.rbtnModules.Text = "Modules"
        Me.rbtnModules.UseVisualStyleBackColor = True
        '
        'rbtnShips
        '
        Me.rbtnShips.AutoSize = True
        Me.rbtnShips.Location = New System.Drawing.Point(98, 85)
        Me.rbtnShips.Name = "rbtnShips"
        Me.rbtnShips.Size = New System.Drawing.Size(51, 17)
        Me.rbtnShips.TabIndex = 13
        Me.rbtnShips.TabStop = True
        Me.rbtnShips.Text = "Ships"
        Me.rbtnShips.UseVisualStyleBackColor = True
        '
        'rbtnDrones
        '
        Me.rbtnDrones.AutoSize = True
        Me.rbtnDrones.Location = New System.Drawing.Point(9, 68)
        Me.rbtnDrones.Name = "rbtnDrones"
        Me.rbtnDrones.Size = New System.Drawing.Size(59, 17)
        Me.rbtnDrones.TabIndex = 6
        Me.rbtnDrones.TabStop = True
        Me.rbtnDrones.Text = "Drones"
        Me.rbtnDrones.UseVisualStyleBackColor = True
        '
        'rbtnCommodities
        '
        Me.rbtnCommodities.AutoSize = True
        Me.rbtnCommodities.Location = New System.Drawing.Point(9, 51)
        Me.rbtnCommodities.Name = "rbtnCommodities"
        Me.rbtnCommodities.Size = New System.Drawing.Size(84, 17)
        Me.rbtnCommodities.TabIndex = 7
        Me.rbtnCommodities.TabStop = True
        Me.rbtnCommodities.Text = "Commodities"
        Me.rbtnCommodities.UseVisualStyleBackColor = True
        '
        'rbtnSkills
        '
        Me.rbtnSkills.AutoSize = True
        Me.rbtnSkills.Location = New System.Drawing.Point(9, 102)
        Me.rbtnSkills.Name = "rbtnSkills"
        Me.rbtnSkills.Size = New System.Drawing.Size(49, 17)
        Me.rbtnSkills.TabIndex = 1
        Me.rbtnSkills.TabStop = True
        Me.rbtnSkills.Text = "Skills"
        Me.rbtnSkills.UseVisualStyleBackColor = True
        '
        'rbtnBlueprints
        '
        Me.rbtnBlueprints.AutoSize = True
        Me.rbtnBlueprints.Location = New System.Drawing.Point(98, 34)
        Me.rbtnBlueprints.Name = "rbtnBlueprints"
        Me.rbtnBlueprints.Size = New System.Drawing.Size(71, 17)
        Me.rbtnBlueprints.TabIndex = 4
        Me.rbtnBlueprints.TabStop = True
        Me.rbtnBlueprints.Text = "Blueprints"
        Me.rbtnBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnAmmoCharge
        '
        Me.rbtnAmmoCharge.AutoSize = True
        Me.rbtnAmmoCharge.Location = New System.Drawing.Point(98, 17)
        Me.rbtnAmmoCharge.Name = "rbtnAmmoCharge"
        Me.rbtnAmmoCharge.Size = New System.Drawing.Size(98, 17)
        Me.rbtnAmmoCharge.TabIndex = 5
        Me.rbtnAmmoCharge.TabStop = True
        Me.rbtnAmmoCharge.Text = "Ammo/Charges"
        Me.rbtnAmmoCharge.UseVisualStyleBackColor = True
        '
        'rbtnImplants
        '
        Me.rbtnImplants.AutoSize = True
        Me.rbtnImplants.Location = New System.Drawing.Point(98, 68)
        Me.rbtnImplants.Name = "rbtnImplants"
        Me.rbtnImplants.Size = New System.Drawing.Size(64, 17)
        Me.rbtnImplants.TabIndex = 10
        Me.rbtnImplants.TabStop = True
        Me.rbtnImplants.Text = "Implants"
        Me.rbtnImplants.UseVisualStyleBackColor = True
        '
        'rbtnAll
        '
        Me.rbtnAll.AutoSize = True
        Me.rbtnAll.Location = New System.Drawing.Point(9, 17)
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(36, 17)
        Me.rbtnAll.TabIndex = 0
        Me.rbtnAll.TabStop = True
        Me.rbtnAll.Text = "All"
        Me.rbtnAll.UseVisualStyleBackColor = True
        '
        'rbtnApparel
        '
        Me.rbtnApparel.AutoSize = True
        Me.rbtnApparel.Location = New System.Drawing.Point(9, 34)
        Me.rbtnApparel.Name = "rbtnApparel"
        Me.rbtnApparel.Size = New System.Drawing.Size(61, 17)
        Me.rbtnApparel.TabIndex = 3
        Me.rbtnApparel.TabStop = True
        Me.rbtnApparel.Text = "Apparel"
        Me.rbtnApparel.UseVisualStyleBackColor = True
        '
        'rbtnDeployable
        '
        Me.rbtnDeployable.AutoSize = True
        Me.rbtnDeployable.Location = New System.Drawing.Point(98, 51)
        Me.rbtnDeployable.Name = "rbtnDeployable"
        Me.rbtnDeployable.Size = New System.Drawing.Size(78, 17)
        Me.rbtnDeployable.TabIndex = 9
        Me.rbtnDeployable.TabStop = True
        Me.rbtnDeployable.Text = "Deployable"
        Me.rbtnDeployable.UseVisualStyleBackColor = True
        '
        'gbCorpRace
        '
        Me.gbCorpRace.Controls.Add(Me.chkRacePirate)
        Me.gbCorpRace.Controls.Add(Me.chkRaceMinmatar)
        Me.gbCorpRace.Controls.Add(Me.chkRaceGallente)
        Me.gbCorpRace.Controls.Add(Me.chkRaceCaldari)
        Me.gbCorpRace.Controls.Add(Me.chkRaceAmarr)
        Me.gbCorpRace.Location = New System.Drawing.Point(222, 492)
        Me.gbCorpRace.Name = "gbCorpRace"
        Me.gbCorpRace.Size = New System.Drawing.Size(198, 65)
        Me.gbCorpRace.TabIndex = 38
        Me.gbCorpRace.TabStop = False
        Me.gbCorpRace.Text = "Corporation Race"
        '
        'chkRacePirate
        '
        Me.chkRacePirate.AutoSize = True
        Me.chkRacePirate.Location = New System.Drawing.Point(136, 17)
        Me.chkRacePirate.Name = "chkRacePirate"
        Me.chkRacePirate.Size = New System.Drawing.Size(53, 17)
        Me.chkRacePirate.TabIndex = 2
        Me.chkRacePirate.Text = "Pirate"
        Me.chkRacePirate.UseVisualStyleBackColor = True
        '
        'chkRaceMinmatar
        '
        Me.chkRaceMinmatar.AutoSize = True
        Me.chkRaceMinmatar.Location = New System.Drawing.Point(69, 40)
        Me.chkRaceMinmatar.Name = "chkRaceMinmatar"
        Me.chkRaceMinmatar.Size = New System.Drawing.Size(69, 17)
        Me.chkRaceMinmatar.TabIndex = 4
        Me.chkRaceMinmatar.Text = "Minmatar"
        Me.chkRaceMinmatar.UseVisualStyleBackColor = True
        '
        'chkRaceGallente
        '
        Me.chkRaceGallente.AutoSize = True
        Me.chkRaceGallente.Location = New System.Drawing.Point(6, 40)
        Me.chkRaceGallente.Name = "chkRaceGallente"
        Me.chkRaceGallente.Size = New System.Drawing.Size(65, 17)
        Me.chkRaceGallente.TabIndex = 3
        Me.chkRaceGallente.Text = "Gallente"
        Me.chkRaceGallente.UseVisualStyleBackColor = True
        '
        'chkRaceCaldari
        '
        Me.chkRaceCaldari.AutoSize = True
        Me.chkRaceCaldari.Location = New System.Drawing.Point(69, 17)
        Me.chkRaceCaldari.Name = "chkRaceCaldari"
        Me.chkRaceCaldari.Size = New System.Drawing.Size(58, 17)
        Me.chkRaceCaldari.TabIndex = 1
        Me.chkRaceCaldari.Text = "Caldari"
        Me.chkRaceCaldari.UseVisualStyleBackColor = True
        '
        'chkRaceAmarr
        '
        Me.chkRaceAmarr.AutoSize = True
        Me.chkRaceAmarr.Location = New System.Drawing.Point(6, 17)
        Me.chkRaceAmarr.Name = "chkRaceAmarr"
        Me.chkRaceAmarr.Size = New System.Drawing.Size(53, 17)
        Me.chkRaceAmarr.TabIndex = 0
        Me.chkRaceAmarr.Text = "Amarr"
        Me.chkRaceAmarr.UseVisualStyleBackColor = True
        '
        'gbSearchOptions
        '
        Me.gbSearchOptions.Controls.Add(Me.chkHighlightCorps)
        Me.gbSearchOptions.Controls.Add(Me.rbtnCorpswStanding)
        Me.gbSearchOptions.Controls.Add(Me.rbtnAllCorps)
        Me.gbSearchOptions.Location = New System.Drawing.Point(653, 396)
        Me.gbSearchOptions.Name = "gbSearchOptions"
        Me.gbSearchOptions.Size = New System.Drawing.Size(348, 47)
        Me.gbSearchOptions.TabIndex = 39
        Me.gbSearchOptions.TabStop = False
        Me.gbSearchOptions.Text = "Search Options"
        '
        'chkHighlightCorps
        '
        Me.chkHighlightCorps.AutoSize = True
        Me.chkHighlightCorps.Location = New System.Drawing.Point(275, 20)
        Me.chkHighlightCorps.Name = "chkHighlightCorps"
        Me.chkHighlightCorps.Size = New System.Drawing.Size(67, 17)
        Me.chkHighlightCorps.TabIndex = 41
        Me.chkHighlightCorps.Text = "Highlight"
        Me.chkHighlightCorps.UseVisualStyleBackColor = True
        '
        'rbtnCorpswStanding
        '
        Me.rbtnCorpswStanding.AutoSize = True
        Me.rbtnCorpswStanding.Location = New System.Drawing.Point(116, 19)
        Me.rbtnCorpswStanding.Name = "rbtnCorpswStanding"
        Me.rbtnCorpswStanding.Size = New System.Drawing.Size(156, 17)
        Me.rbtnCorpswStanding.TabIndex = 40
        Me.rbtnCorpswStanding.TabStop = True
        Me.rbtnCorpswStanding.Text = "Corporations with Standings"
        Me.rbtnCorpswStanding.UseVisualStyleBackColor = True
        '
        'rbtnAllCorps
        '
        Me.rbtnAllCorps.AutoSize = True
        Me.rbtnAllCorps.Location = New System.Drawing.Point(12, 19)
        Me.rbtnAllCorps.Name = "rbtnAllCorps"
        Me.rbtnAllCorps.Size = New System.Drawing.Size(98, 17)
        Me.rbtnAllCorps.TabIndex = 39
        Me.rbtnAllCorps.TabStop = True
        Me.rbtnAllCorps.Text = "All Corporations"
        Me.rbtnAllCorps.UseVisualStyleBackColor = True
        '
        'gbItemFilter
        '
        Me.gbItemFilter.Controls.Add(Me.btnResetItemFilterSearch)
        Me.gbItemFilter.Controls.Add(Me.txtItemFilter)
        Me.gbItemFilter.Location = New System.Drawing.Point(222, 396)
        Me.gbItemFilter.Name = "gbItemFilter"
        Me.gbItemFilter.Size = New System.Drawing.Size(198, 43)
        Me.gbItemFilter.TabIndex = 40
        Me.gbItemFilter.TabStop = False
        Me.gbItemFilter.Text = "Item Filter:"
        '
        'btnResetItemFilterSearch
        '
        Me.btnResetItemFilterSearch.Location = New System.Drawing.Point(153, 16)
        Me.btnResetItemFilterSearch.Name = "btnResetItemFilterSearch"
        Me.btnResetItemFilterSearch.Size = New System.Drawing.Size(39, 21)
        Me.btnResetItemFilterSearch.TabIndex = 1
        Me.btnResetItemFilterSearch.Text = "Clear"
        Me.btnResetItemFilterSearch.UseVisualStyleBackColor = True
        '
        'txtItemFilter
        '
        Me.txtItemFilter.Location = New System.Drawing.Point(11, 16)
        Me.txtItemFilter.Name = "txtItemFilter"
        Me.txtItemFilter.Size = New System.Drawing.Size(136, 20)
        Me.txtItemFilter.TabIndex = 0
        '
        'gbReqItemFilter
        '
        Me.gbReqItemFilter.Controls.Add(Me.btnResetReqItemSearch)
        Me.gbReqItemFilter.Controls.Add(Me.txtReqItemFilter)
        Me.gbReqItemFilter.Location = New System.Drawing.Point(222, 446)
        Me.gbReqItemFilter.Name = "gbReqItemFilter"
        Me.gbReqItemFilter.Size = New System.Drawing.Size(198, 43)
        Me.gbReqItemFilter.TabIndex = 41
        Me.gbReqItemFilter.TabStop = False
        Me.gbReqItemFilter.Text = "Required Item Filter:"
        '
        'btnResetReqItemSearch
        '
        Me.btnResetReqItemSearch.Location = New System.Drawing.Point(153, 16)
        Me.btnResetReqItemSearch.Name = "btnResetReqItemSearch"
        Me.btnResetReqItemSearch.Size = New System.Drawing.Size(39, 21)
        Me.btnResetReqItemSearch.TabIndex = 1
        Me.btnResetReqItemSearch.Text = "Clear"
        Me.btnResetReqItemSearch.UseVisualStyleBackColor = True
        '
        'txtReqItemFilter
        '
        Me.txtReqItemFilter.Location = New System.Drawing.Point(11, 16)
        Me.txtReqItemFilter.Name = "txtReqItemFilter"
        Me.txtReqItemFilter.Size = New System.Drawing.Size(136, 20)
        Me.txtReqItemFilter.TabIndex = 0
        '
        'gbLPCost
        '
        Me.gbLPCost.Controls.Add(Me.txtLPGreaterThan)
        Me.gbLPCost.Controls.Add(Me.lblLPGreaterThan)
        Me.gbLPCost.Controls.Add(Me.txtLPLessThan)
        Me.gbLPCost.Controls.Add(Me.lblLPLessThan)
        Me.gbLPCost.Location = New System.Drawing.Point(426, 396)
        Me.gbLPCost.Name = "gbLPCost"
        Me.gbLPCost.Size = New System.Drawing.Size(166, 71)
        Me.gbLPCost.TabIndex = 42
        Me.gbLPCost.TabStop = False
        Me.gbLPCost.Text = "LP Cost - Hide items"
        '
        'txtLPGreaterThan
        '
        Me.txtLPGreaterThan.Location = New System.Drawing.Point(92, 42)
        Me.txtLPGreaterThan.Name = "txtLPGreaterThan"
        Me.txtLPGreaterThan.Size = New System.Drawing.Size(58, 20)
        Me.txtLPGreaterThan.TabIndex = 3
        '
        'lblLPGreaterThan
        '
        Me.lblLPGreaterThan.AutoSize = True
        Me.lblLPGreaterThan.Location = New System.Drawing.Point(13, 45)
        Me.lblLPGreaterThan.Name = "lblLPGreaterThan"
        Me.lblLPGreaterThan.Size = New System.Drawing.Size(73, 13)
        Me.lblLPGreaterThan.TabIndex = 2
        Me.lblLPGreaterThan.Text = "Greater Than:"
        '
        'txtLPLessThan
        '
        Me.txtLPLessThan.Location = New System.Drawing.Point(92, 19)
        Me.txtLPLessThan.Name = "txtLPLessThan"
        Me.txtLPLessThan.Size = New System.Drawing.Size(58, 20)
        Me.txtLPLessThan.TabIndex = 1
        '
        'lblLPLessThan
        '
        Me.lblLPLessThan.AutoSize = True
        Me.lblLPLessThan.Location = New System.Drawing.Point(13, 26)
        Me.lblLPLessThan.Name = "lblLPLessThan"
        Me.lblLPLessThan.Size = New System.Drawing.Size(60, 13)
        Me.lblLPLessThan.TabIndex = 0
        Me.lblLPLessThan.Text = "Less Than:"
        '
        'gbISKCost
        '
        Me.gbISKCost.Controls.Add(Me.txtISKGreaterThan)
        Me.gbISKCost.Controls.Add(Me.lblISKGreaterThan)
        Me.gbISKCost.Controls.Add(Me.txtISKLessThan)
        Me.gbISKCost.Controls.Add(Me.lblISKLessThan)
        Me.gbISKCost.Location = New System.Drawing.Point(426, 475)
        Me.gbISKCost.Name = "gbISKCost"
        Me.gbISKCost.Size = New System.Drawing.Size(166, 71)
        Me.gbISKCost.TabIndex = 43
        Me.gbISKCost.TabStop = False
        Me.gbISKCost.Text = "ISK Cost - Hide items"
        '
        'txtISKGreaterThan
        '
        Me.txtISKGreaterThan.Location = New System.Drawing.Point(92, 40)
        Me.txtISKGreaterThan.Name = "txtISKGreaterThan"
        Me.txtISKGreaterThan.Size = New System.Drawing.Size(58, 20)
        Me.txtISKGreaterThan.TabIndex = 3
        '
        'lblISKGreaterThan
        '
        Me.lblISKGreaterThan.AutoSize = True
        Me.lblISKGreaterThan.Location = New System.Drawing.Point(13, 43)
        Me.lblISKGreaterThan.Name = "lblISKGreaterThan"
        Me.lblISKGreaterThan.Size = New System.Drawing.Size(73, 13)
        Me.lblISKGreaterThan.TabIndex = 2
        Me.lblISKGreaterThan.Text = "Greater Than:"
        '
        'txtISKLessThan
        '
        Me.txtISKLessThan.Location = New System.Drawing.Point(92, 17)
        Me.txtISKLessThan.Name = "txtISKLessThan"
        Me.txtISKLessThan.Size = New System.Drawing.Size(58, 20)
        Me.txtISKLessThan.TabIndex = 1
        '
        'lblISKLessThan
        '
        Me.lblISKLessThan.AutoSize = True
        Me.lblISKLessThan.Location = New System.Drawing.Point(13, 24)
        Me.lblISKLessThan.Name = "lblISKLessThan"
        Me.lblISKLessThan.Size = New System.Drawing.Size(60, 13)
        Me.lblISKLessThan.TabIndex = 0
        Me.lblISKLessThan.Text = "Less Than:"
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(120, 527)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(96, 27)
        Me.btnSaveSettings.TabIndex = 46
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(12, 526)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(96, 27)
        Me.btnRefresh.TabIndex = 45
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'LPStoreItemImages
        '
        Me.LPStoreItemImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.LPStoreItemImages.ImageSize = New System.Drawing.Size(16, 16)
        Me.LPStoreItemImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'lstCorporations
        '
        Me.lstCorporations.CheckBoxes = True
        Me.lstCorporations.FullRowSelect = True
        Me.lstCorporations.GridLines = True
        Me.lstCorporations.HideSelection = False
        Me.lstCorporations.Location = New System.Drawing.Point(653, 449)
        Me.lstCorporations.MultiSelect = False
        Me.lstCorporations.Name = "lstCorporations"
        Me.lstCorporations.Size = New System.Drawing.Size(348, 124)
        Me.lstCorporations.TabIndex = 44
        Me.lstCorporations.TabStop = False
        Me.lstCorporations.UseCompatibleStateImageBehavior = False
        Me.lstCorporations.View = System.Windows.Forms.View.Details
        '
        'lstStoreItems
        '
        Me.lstStoreItems.FullRowSelect = True
        Me.lstStoreItems.GridLines = True
        Me.lstStoreItems.HideSelection = False
        Me.lstStoreItems.Location = New System.Drawing.Point(12, 12)
        Me.lstStoreItems.MultiSelect = False
        Me.lstStoreItems.Name = "lstStoreItems"
        Me.lstStoreItems.Size = New System.Drawing.Size(989, 371)
        Me.lstStoreItems.TabIndex = 36
        Me.lstStoreItems.TabStop = False
        Me.lstStoreItems.UseCompatibleStateImageBehavior = False
        Me.lstStoreItems.View = System.Windows.Forms.View.Details
        '
        'frmLPStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1013, 585)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lstCorporations)
        Me.Controls.Add(Me.gbISKCost)
        Me.Controls.Add(Me.gbLPCost)
        Me.Controls.Add(Me.gbReqItemFilter)
        Me.Controls.Add(Me.gbItemFilter)
        Me.Controls.Add(Me.gbSearchOptions)
        Me.Controls.Add(Me.gbRewardType)
        Me.Controls.Add(Me.gbCorpRace)
        Me.Controls.Add(Me.lstStoreItems)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLPStore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Loyalty Store Viewer"
        Me.gbRewardType.ResumeLayout(False)
        Me.gbRewardType.PerformLayout()
        Me.gbCorpRace.ResumeLayout(False)
        Me.gbCorpRace.PerformLayout()
        Me.gbSearchOptions.ResumeLayout(False)
        Me.gbSearchOptions.PerformLayout()
        Me.gbItemFilter.ResumeLayout(False)
        Me.gbItemFilter.PerformLayout()
        Me.gbReqItemFilter.ResumeLayout(False)
        Me.gbReqItemFilter.PerformLayout()
        Me.gbLPCost.ResumeLayout(False)
        Me.gbLPCost.PerformLayout()
        Me.gbISKCost.ResumeLayout(False)
        Me.gbISKCost.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstStoreItems As EVE_Isk_per_Hour.MyListView
    Friend WithEvents gbRewardType As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnModules As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnShips As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDrones As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCommodities As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSkills As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAmmoCharge As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnImplants As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnApparel As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDeployable As System.Windows.Forms.RadioButton
    Friend WithEvents gbCorpRace As System.Windows.Forms.GroupBox
    Friend WithEvents chkRacePirate As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceMinmatar As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceGallente As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceCaldari As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceAmarr As System.Windows.Forms.CheckBox
    Friend WithEvents gbSearchOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnCorpswStanding As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAllCorps As System.Windows.Forms.RadioButton
    Friend WithEvents gbItemFilter As System.Windows.Forms.GroupBox
    Friend WithEvents btnResetItemFilterSearch As System.Windows.Forms.Button
    Friend WithEvents txtItemFilter As System.Windows.Forms.TextBox
    Friend WithEvents gbReqItemFilter As System.Windows.Forms.GroupBox
    Friend WithEvents btnResetReqItemSearch As System.Windows.Forms.Button
    Friend WithEvents txtReqItemFilter As System.Windows.Forms.TextBox
    Friend WithEvents gbLPCost As System.Windows.Forms.GroupBox
    Friend WithEvents txtLPGreaterThan As System.Windows.Forms.TextBox
    Friend WithEvents lblLPGreaterThan As System.Windows.Forms.Label
    Friend WithEvents txtLPLessThan As System.Windows.Forms.TextBox
    Friend WithEvents lblLPLessThan As System.Windows.Forms.Label
    Friend WithEvents gbISKCost As System.Windows.Forms.GroupBox
    Friend WithEvents txtISKGreaterThan As System.Windows.Forms.TextBox
    Friend WithEvents lblISKGreaterThan As System.Windows.Forms.Label
    Friend WithEvents txtISKLessThan As System.Windows.Forms.TextBox
    Friend WithEvents lblISKLessThan As System.Windows.Forms.Label
    Friend WithEvents lstCorporations As EVE_Isk_per_Hour.MyListView
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents chkHighlightCorps As System.Windows.Forms.CheckBox
    Friend WithEvents LPStoreItemImages As System.Windows.Forms.ImageList
End Class
