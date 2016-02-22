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
        Me.gbSearchOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnCorpswStanding = New System.Windows.Forms.RadioButton()
        Me.chkHighlightCorps = New System.Windows.Forms.CheckBox()
        Me.rbtnAllCorps = New System.Windows.Forms.RadioButton()
        Me.gbItemFilter = New System.Windows.Forms.GroupBox()
        Me.btnResetItemFilterSearch = New System.Windows.Forms.Button()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
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
        Me.txtStandingsGreaterThan = New System.Windows.Forms.TextBox()
        Me.lblStandingsGreaterThan = New System.Windows.Forms.Label()
        Me.txtStandingsLessThan = New System.Windows.Forms.TextBox()
        Me.lblStandingsLessThan = New System.Windows.Forms.Label()
        Me.gbSortBy = New System.Windows.Forms.GroupBox()
        Me.rbtnSortbyProfit = New System.Windows.Forms.RadioButton()
        Me.rbtnSortbyISKperLP = New System.Windows.Forms.RadioButton()
        Me.lstStoreItems = New System.Windows.Forms.ListView()
        Me.lstCorporations = New EVE_Isk_per_Hour.MyListView()
        Me.chkLevel3Agent = New System.Windows.Forms.CheckBox()
        Me.chkLevel2Agent = New System.Windows.Forms.CheckBox()
        Me.chkLevel1Agent = New System.Windows.Forms.CheckBox()
        Me.chkLevel4Agent = New System.Windows.Forms.CheckBox()
        Me.chkLevel5Agent = New System.Windows.Forms.CheckBox()
        Me.gbCorpFilter = New System.Windows.Forms.GroupBox()
        Me.gbAgentLevels = New System.Windows.Forms.GroupBox()
        Me.gbStandings = New System.Windows.Forms.GroupBox()
        Me.rbtnCorpFilterUseAgentLevels = New System.Windows.Forms.RadioButton()
        Me.rbtnCorpFilterUseStandings = New System.Windows.Forms.RadioButton()
        Me.lstRequiredMats = New System.Windows.Forms.ListView()
        Me.lblCorporationList = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkManufactureBP = New System.Windows.Forms.CheckBox()
        Me.gbRewardType.SuspendLayout()
        Me.gbSearchOptions.SuspendLayout()
        Me.gbItemFilter.SuspendLayout()
        Me.gbLPCost.SuspendLayout()
        Me.gbISKCost.SuspendLayout()
        Me.gbSortBy.SuspendLayout()
        Me.gbCorpFilter.SuspendLayout()
        Me.gbAgentLevels.SuspendLayout()
        Me.gbStandings.SuspendLayout()
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
        Me.gbRewardType.Controls.Add(Me.chkManufactureBP)
        Me.gbRewardType.Location = New System.Drawing.Point(12, 384)
        Me.gbRewardType.Name = "gbRewardType"
        Me.gbRewardType.Size = New System.Drawing.Size(204, 117)
        Me.gbRewardType.TabIndex = 37
        Me.gbRewardType.TabStop = False
        Me.gbRewardType.Text = "Reward Type"
        '
        'rbtnModules
        '
        Me.rbtnModules.AutoSize = True
        Me.rbtnModules.Location = New System.Drawing.Point(9, 79)
        Me.rbtnModules.Name = "rbtnModules"
        Me.rbtnModules.Size = New System.Drawing.Size(65, 17)
        Me.rbtnModules.TabIndex = 12
        Me.rbtnModules.Text = "Modules"
        Me.rbtnModules.UseVisualStyleBackColor = True
        '
        'rbtnShips
        '
        Me.rbtnShips.AutoSize = True
        Me.rbtnShips.Location = New System.Drawing.Point(95, 79)
        Me.rbtnShips.Name = "rbtnShips"
        Me.rbtnShips.Size = New System.Drawing.Size(51, 17)
        Me.rbtnShips.TabIndex = 13
        Me.rbtnShips.Text = "Ships"
        Me.rbtnShips.UseVisualStyleBackColor = True
        '
        'rbtnDrones
        '
        Me.rbtnDrones.AutoSize = True
        Me.rbtnDrones.Location = New System.Drawing.Point(9, 64)
        Me.rbtnDrones.Name = "rbtnDrones"
        Me.rbtnDrones.Size = New System.Drawing.Size(59, 17)
        Me.rbtnDrones.TabIndex = 6
        Me.rbtnDrones.Text = "Drones"
        Me.rbtnDrones.UseVisualStyleBackColor = True
        '
        'rbtnCommodities
        '
        Me.rbtnCommodities.AutoSize = True
        Me.rbtnCommodities.Location = New System.Drawing.Point(9, 49)
        Me.rbtnCommodities.Name = "rbtnCommodities"
        Me.rbtnCommodities.Size = New System.Drawing.Size(84, 17)
        Me.rbtnCommodities.TabIndex = 7
        Me.rbtnCommodities.Text = "Commodities"
        Me.rbtnCommodities.UseVisualStyleBackColor = True
        '
        'rbtnSkills
        '
        Me.rbtnSkills.AutoSize = True
        Me.rbtnSkills.Location = New System.Drawing.Point(9, 94)
        Me.rbtnSkills.Name = "rbtnSkills"
        Me.rbtnSkills.Size = New System.Drawing.Size(49, 17)
        Me.rbtnSkills.TabIndex = 1
        Me.rbtnSkills.Text = "Skills"
        Me.rbtnSkills.UseVisualStyleBackColor = True
        '
        'rbtnBlueprints
        '
        Me.rbtnBlueprints.AutoSize = True
        Me.rbtnBlueprints.Location = New System.Drawing.Point(95, 34)
        Me.rbtnBlueprints.Name = "rbtnBlueprints"
        Me.rbtnBlueprints.Size = New System.Drawing.Size(71, 17)
        Me.rbtnBlueprints.TabIndex = 4
        Me.rbtnBlueprints.Text = "Blueprints"
        Me.rbtnBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnAmmoCharge
        '
        Me.rbtnAmmoCharge.AutoSize = True
        Me.rbtnAmmoCharge.Location = New System.Drawing.Point(95, 18)
        Me.rbtnAmmoCharge.Name = "rbtnAmmoCharge"
        Me.rbtnAmmoCharge.Size = New System.Drawing.Size(98, 17)
        Me.rbtnAmmoCharge.TabIndex = 5
        Me.rbtnAmmoCharge.Text = "Ammo/Charges"
        Me.rbtnAmmoCharge.UseVisualStyleBackColor = True
        '
        'rbtnImplants
        '
        Me.rbtnImplants.AutoSize = True
        Me.rbtnImplants.Location = New System.Drawing.Point(95, 64)
        Me.rbtnImplants.Name = "rbtnImplants"
        Me.rbtnImplants.Size = New System.Drawing.Size(64, 17)
        Me.rbtnImplants.TabIndex = 10
        Me.rbtnImplants.Text = "Implants"
        Me.rbtnImplants.UseVisualStyleBackColor = True
        '
        'rbtnAll
        '
        Me.rbtnAll.AutoSize = True
        Me.rbtnAll.Checked = True
        Me.rbtnAll.Location = New System.Drawing.Point(9, 19)
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
        Me.rbtnApparel.Text = "Apparel"
        Me.rbtnApparel.UseVisualStyleBackColor = True
        '
        'rbtnDeployable
        '
        Me.rbtnDeployable.AutoSize = True
        Me.rbtnDeployable.Location = New System.Drawing.Point(95, 49)
        Me.rbtnDeployable.Name = "rbtnDeployable"
        Me.rbtnDeployable.Size = New System.Drawing.Size(78, 17)
        Me.rbtnDeployable.TabIndex = 9
        Me.rbtnDeployable.Text = "Deployable"
        Me.rbtnDeployable.UseVisualStyleBackColor = True
        '
        'gbSearchOptions
        '
        Me.gbSearchOptions.Controls.Add(Me.rbtnCorpswStanding)
        Me.gbSearchOptions.Controls.Add(Me.chkHighlightCorps)
        Me.gbSearchOptions.Controls.Add(Me.rbtnAllCorps)
        Me.gbSearchOptions.Location = New System.Drawing.Point(652, 467)
        Me.gbSearchOptions.Name = "gbSearchOptions"
        Me.gbSearchOptions.Size = New System.Drawing.Size(136, 112)
        Me.gbSearchOptions.TabIndex = 39
        Me.gbSearchOptions.TabStop = False
        Me.gbSearchOptions.Text = "Search Options"
        '
        'rbtnCorpswStanding
        '
        Me.rbtnCorpswStanding.Location = New System.Drawing.Point(11, 40)
        Me.rbtnCorpswStanding.Name = "rbtnCorpswStanding"
        Me.rbtnCorpswStanding.Size = New System.Drawing.Size(98, 30)
        Me.rbtnCorpswStanding.TabIndex = 40
        Me.rbtnCorpswStanding.TabStop = True
        Me.rbtnCorpswStanding.Text = "Corporations with Standings"
        Me.rbtnCorpswStanding.UseVisualStyleBackColor = True
        '
        'chkHighlightCorps
        '
        Me.chkHighlightCorps.AutoSize = True
        Me.chkHighlightCorps.Location = New System.Drawing.Point(11, 80)
        Me.chkHighlightCorps.Name = "chkHighlightCorps"
        Me.chkHighlightCorps.Size = New System.Drawing.Size(114, 17)
        Me.chkHighlightCorps.TabIndex = 41
        Me.chkHighlightCorps.Text = "Highlight My Corps"
        Me.chkHighlightCorps.UseVisualStyleBackColor = True
        '
        'rbtnAllCorps
        '
        Me.rbtnAllCorps.AutoSize = True
        Me.rbtnAllCorps.Location = New System.Drawing.Point(11, 22)
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
        Me.gbItemFilter.Location = New System.Drawing.Point(11, 504)
        Me.gbItemFilter.Name = "gbItemFilter"
        Me.gbItemFilter.Size = New System.Drawing.Size(205, 43)
        Me.gbItemFilter.TabIndex = 40
        Me.gbItemFilter.TabStop = False
        Me.gbItemFilter.Text = "Item Filter:"
        '
        'btnResetItemFilterSearch
        '
        Me.btnResetItemFilterSearch.Location = New System.Drawing.Point(160, 16)
        Me.btnResetItemFilterSearch.Name = "btnResetItemFilterSearch"
        Me.btnResetItemFilterSearch.Size = New System.Drawing.Size(39, 21)
        Me.btnResetItemFilterSearch.TabIndex = 1
        Me.btnResetItemFilterSearch.Text = "Clear"
        Me.btnResetItemFilterSearch.UseVisualStyleBackColor = True
        '
        'txtItemFilter
        '
        Me.txtItemFilter.Location = New System.Drawing.Point(10, 16)
        Me.txtItemFilter.Name = "txtItemFilter"
        Me.txtItemFilter.Size = New System.Drawing.Size(144, 20)
        Me.txtItemFilter.TabIndex = 0
        '
        'gbLPCost
        '
        Me.gbLPCost.Controls.Add(Me.txtLPGreaterThan)
        Me.gbLPCost.Controls.Add(Me.lblLPGreaterThan)
        Me.gbLPCost.Controls.Add(Me.txtLPLessThan)
        Me.gbLPCost.Controls.Add(Me.lblLPLessThan)
        Me.gbLPCost.Location = New System.Drawing.Point(222, 461)
        Me.gbLPCost.Name = "gbLPCost"
        Me.gbLPCost.Size = New System.Drawing.Size(161, 73)
        Me.gbLPCost.TabIndex = 42
        Me.gbLPCost.TabStop = False
        Me.gbLPCost.Text = "LP Cost Filter"
        '
        'txtLPGreaterThan
        '
        Me.txtLPGreaterThan.Location = New System.Drawing.Point(86, 43)
        Me.txtLPGreaterThan.Name = "txtLPGreaterThan"
        Me.txtLPGreaterThan.Size = New System.Drawing.Size(68, 20)
        Me.txtLPGreaterThan.TabIndex = 3
        '
        'lblLPGreaterThan
        '
        Me.lblLPGreaterThan.AutoSize = True
        Me.lblLPGreaterThan.Location = New System.Drawing.Point(7, 46)
        Me.lblLPGreaterThan.Name = "lblLPGreaterThan"
        Me.lblLPGreaterThan.Size = New System.Drawing.Size(73, 13)
        Me.lblLPGreaterThan.TabIndex = 2
        Me.lblLPGreaterThan.Text = "Greater Than:"
        '
        'txtLPLessThan
        '
        Me.txtLPLessThan.Location = New System.Drawing.Point(86, 20)
        Me.txtLPLessThan.Name = "txtLPLessThan"
        Me.txtLPLessThan.Size = New System.Drawing.Size(68, 20)
        Me.txtLPLessThan.TabIndex = 1
        '
        'lblLPLessThan
        '
        Me.lblLPLessThan.AutoSize = True
        Me.lblLPLessThan.Location = New System.Drawing.Point(7, 23)
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
        Me.gbISKCost.Location = New System.Drawing.Point(222, 384)
        Me.gbISKCost.Name = "gbISKCost"
        Me.gbISKCost.Size = New System.Drawing.Size(161, 73)
        Me.gbISKCost.TabIndex = 43
        Me.gbISKCost.TabStop = False
        Me.gbISKCost.Text = "ISK Cost Filter"
        '
        'txtISKGreaterThan
        '
        Me.txtISKGreaterThan.Location = New System.Drawing.Point(86, 42)
        Me.txtISKGreaterThan.Name = "txtISKGreaterThan"
        Me.txtISKGreaterThan.Size = New System.Drawing.Size(68, 20)
        Me.txtISKGreaterThan.TabIndex = 3
        '
        'lblISKGreaterThan
        '
        Me.lblISKGreaterThan.AutoSize = True
        Me.lblISKGreaterThan.Location = New System.Drawing.Point(10, 45)
        Me.lblISKGreaterThan.Name = "lblISKGreaterThan"
        Me.lblISKGreaterThan.Size = New System.Drawing.Size(73, 13)
        Me.lblISKGreaterThan.TabIndex = 2
        Me.lblISKGreaterThan.Text = "Greater Than:"
        '
        'txtISKLessThan
        '
        Me.txtISKLessThan.Location = New System.Drawing.Point(86, 19)
        Me.txtISKLessThan.Name = "txtISKLessThan"
        Me.txtISKLessThan.Size = New System.Drawing.Size(68, 20)
        Me.txtISKLessThan.TabIndex = 1
        '
        'lblISKLessThan
        '
        Me.lblISKLessThan.AutoSize = True
        Me.lblISKLessThan.Location = New System.Drawing.Point(10, 22)
        Me.lblISKLessThan.Name = "lblISKLessThan"
        Me.lblISKLessThan.Size = New System.Drawing.Size(60, 13)
        Me.lblISKLessThan.TabIndex = 0
        Me.lblISKLessThan.Text = "Less Than:"
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(119, 553)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(96, 26)
        Me.btnSaveSettings.TabIndex = 46
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(17, 553)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(96, 26)
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
        'txtStandingsGreaterThan
        '
        Me.txtStandingsGreaterThan.Location = New System.Drawing.Point(77, 39)
        Me.txtStandingsGreaterThan.Name = "txtStandingsGreaterThan"
        Me.txtStandingsGreaterThan.Size = New System.Drawing.Size(47, 20)
        Me.txtStandingsGreaterThan.TabIndex = 3
        '
        'lblStandingsGreaterThan
        '
        Me.lblStandingsGreaterThan.AutoSize = True
        Me.lblStandingsGreaterThan.Location = New System.Drawing.Point(3, 42)
        Me.lblStandingsGreaterThan.Name = "lblStandingsGreaterThan"
        Me.lblStandingsGreaterThan.Size = New System.Drawing.Size(73, 13)
        Me.lblStandingsGreaterThan.TabIndex = 2
        Me.lblStandingsGreaterThan.Text = "Greater Than:"
        '
        'txtStandingsLessThan
        '
        Me.txtStandingsLessThan.Location = New System.Drawing.Point(77, 16)
        Me.txtStandingsLessThan.Name = "txtStandingsLessThan"
        Me.txtStandingsLessThan.Size = New System.Drawing.Size(47, 20)
        Me.txtStandingsLessThan.TabIndex = 1
        '
        'lblStandingsLessThan
        '
        Me.lblStandingsLessThan.AutoSize = True
        Me.lblStandingsLessThan.Location = New System.Drawing.Point(3, 19)
        Me.lblStandingsLessThan.Name = "lblStandingsLessThan"
        Me.lblStandingsLessThan.Size = New System.Drawing.Size(60, 13)
        Me.lblStandingsLessThan.TabIndex = 0
        Me.lblStandingsLessThan.Text = "Less Than:"
        '
        'gbSortBy
        '
        Me.gbSortBy.Controls.Add(Me.rbtnSortbyProfit)
        Me.gbSortBy.Controls.Add(Me.rbtnSortbyISKperLP)
        Me.gbSortBy.Location = New System.Drawing.Point(222, 536)
        Me.gbSortBy.Name = "gbSortBy"
        Me.gbSortBy.Size = New System.Drawing.Size(161, 43)
        Me.gbSortBy.TabIndex = 41
        Me.gbSortBy.TabStop = False
        Me.gbSortBy.Text = "Sort by:"
        '
        'rbtnSortbyProfit
        '
        Me.rbtnSortbyProfit.AutoSize = True
        Me.rbtnSortbyProfit.Location = New System.Drawing.Point(95, 19)
        Me.rbtnSortbyProfit.Name = "rbtnSortbyProfit"
        Me.rbtnSortbyProfit.Size = New System.Drawing.Size(49, 17)
        Me.rbtnSortbyProfit.TabIndex = 40
        Me.rbtnSortbyProfit.TabStop = True
        Me.rbtnSortbyProfit.Text = "Profit"
        Me.rbtnSortbyProfit.UseVisualStyleBackColor = True
        '
        'rbtnSortbyISKperLP
        '
        Me.rbtnSortbyISKperLP.AutoSize = True
        Me.rbtnSortbyISKperLP.Location = New System.Drawing.Point(20, 19)
        Me.rbtnSortbyISKperLP.Name = "rbtnSortbyISKperLP"
        Me.rbtnSortbyISKperLP.Size = New System.Drawing.Size(60, 17)
        Me.rbtnSortbyISKperLP.TabIndex = 39
        Me.rbtnSortbyISKperLP.TabStop = True
        Me.rbtnSortbyISKperLP.Text = "ISK/LP"
        Me.rbtnSortbyISKperLP.UseVisualStyleBackColor = True
        '
        'lstStoreItems
        '
        Me.lstStoreItems.FullRowSelect = True
        Me.lstStoreItems.GridLines = True
        Me.lstStoreItems.HideSelection = False
        Me.lstStoreItems.Location = New System.Drawing.Point(11, 12)
        Me.lstStoreItems.MultiSelect = False
        Me.lstStoreItems.Name = "lstStoreItems"
        Me.lstStoreItems.Size = New System.Drawing.Size(1061, 368)
        Me.lstStoreItems.TabIndex = 64
        Me.lstStoreItems.UseCompatibleStateImageBehavior = False
        Me.lstStoreItems.View = System.Windows.Forms.View.Details
        '
        'lstCorporations
        '
        Me.lstCorporations.CheckBoxes = True
        Me.lstCorporations.FullRowSelect = True
        Me.lstCorporations.GridLines = True
        Me.lstCorporations.HideSelection = False
        Me.lstCorporations.Location = New System.Drawing.Point(797, 415)
        Me.lstCorporations.MultiSelect = False
        Me.lstCorporations.Name = "lstCorporations"
        Me.lstCorporations.Size = New System.Drawing.Size(275, 164)
        Me.lstCorporations.TabIndex = 44
        Me.lstCorporations.TabStop = False
        Me.lstCorporations.UseCompatibleStateImageBehavior = False
        Me.lstCorporations.View = System.Windows.Forms.View.Details
        '
        'chkLevel3Agent
        '
        Me.chkLevel3Agent.AutoSize = True
        Me.chkLevel3Agent.Location = New System.Drawing.Point(6, 48)
        Me.chkLevel3Agent.Name = "chkLevel3Agent"
        Me.chkLevel3Agent.Size = New System.Drawing.Size(61, 17)
        Me.chkLevel3Agent.TabIndex = 66
        Me.chkLevel3Agent.Text = "Level 3"
        Me.chkLevel3Agent.UseVisualStyleBackColor = True
        '
        'chkLevel2Agent
        '
        Me.chkLevel2Agent.AutoSize = True
        Me.chkLevel2Agent.Location = New System.Drawing.Point(6, 32)
        Me.chkLevel2Agent.Name = "chkLevel2Agent"
        Me.chkLevel2Agent.Size = New System.Drawing.Size(61, 17)
        Me.chkLevel2Agent.TabIndex = 67
        Me.chkLevel2Agent.Text = "Level 2"
        Me.chkLevel2Agent.UseVisualStyleBackColor = True
        '
        'chkLevel1Agent
        '
        Me.chkLevel1Agent.AutoSize = True
        Me.chkLevel1Agent.Location = New System.Drawing.Point(6, 16)
        Me.chkLevel1Agent.Name = "chkLevel1Agent"
        Me.chkLevel1Agent.Size = New System.Drawing.Size(61, 17)
        Me.chkLevel1Agent.TabIndex = 68
        Me.chkLevel1Agent.Text = "Level 1"
        Me.chkLevel1Agent.UseVisualStyleBackColor = True
        '
        'chkLevel4Agent
        '
        Me.chkLevel4Agent.AutoSize = True
        Me.chkLevel4Agent.Location = New System.Drawing.Point(67, 16)
        Me.chkLevel4Agent.Name = "chkLevel4Agent"
        Me.chkLevel4Agent.Size = New System.Drawing.Size(61, 17)
        Me.chkLevel4Agent.TabIndex = 69
        Me.chkLevel4Agent.Text = "Level 4"
        Me.chkLevel4Agent.UseVisualStyleBackColor = True
        '
        'chkLevel5Agent
        '
        Me.chkLevel5Agent.AutoSize = True
        Me.chkLevel5Agent.Location = New System.Drawing.Point(67, 32)
        Me.chkLevel5Agent.Name = "chkLevel5Agent"
        Me.chkLevel5Agent.Size = New System.Drawing.Size(61, 17)
        Me.chkLevel5Agent.TabIndex = 70
        Me.chkLevel5Agent.Text = "Level 5"
        Me.chkLevel5Agent.UseVisualStyleBackColor = True
        '
        'gbCorpFilter
        '
        Me.gbCorpFilter.Controls.Add(Me.gbAgentLevels)
        Me.gbCorpFilter.Controls.Add(Me.gbStandings)
        Me.gbCorpFilter.Controls.Add(Me.rbtnCorpFilterUseAgentLevels)
        Me.gbCorpFilter.Controls.Add(Me.rbtnCorpFilterUseStandings)
        Me.gbCorpFilter.Location = New System.Drawing.Point(389, 384)
        Me.gbCorpFilter.Name = "gbCorpFilter"
        Me.gbCorpFilter.Size = New System.Drawing.Size(399, 80)
        Me.gbCorpFilter.TabIndex = 39
        Me.gbCorpFilter.TabStop = False
        Me.gbCorpFilter.Text = "Corporation Filter"
        '
        'gbAgentLevels
        '
        Me.gbAgentLevels.Controls.Add(Me.chkLevel1Agent)
        Me.gbAgentLevels.Controls.Add(Me.chkLevel2Agent)
        Me.gbAgentLevels.Controls.Add(Me.chkLevel5Agent)
        Me.gbAgentLevels.Controls.Add(Me.chkLevel3Agent)
        Me.gbAgentLevels.Controls.Add(Me.chkLevel4Agent)
        Me.gbAgentLevels.Location = New System.Drawing.Point(263, 7)
        Me.gbAgentLevels.Name = "gbAgentLevels"
        Me.gbAgentLevels.Size = New System.Drawing.Size(130, 67)
        Me.gbAgentLevels.TabIndex = 71
        Me.gbAgentLevels.TabStop = False
        Me.gbAgentLevels.Text = "Agent Levels"
        '
        'gbStandings
        '
        Me.gbStandings.Controls.Add(Me.lblStandingsLessThan)
        Me.gbStandings.Controls.Add(Me.txtStandingsLessThan)
        Me.gbStandings.Controls.Add(Me.lblStandingsGreaterThan)
        Me.gbStandings.Controls.Add(Me.txtStandingsGreaterThan)
        Me.gbStandings.Location = New System.Drawing.Point(126, 7)
        Me.gbStandings.Name = "gbStandings"
        Me.gbStandings.Size = New System.Drawing.Size(130, 67)
        Me.gbStandings.TabIndex = 41
        Me.gbStandings.TabStop = False
        Me.gbStandings.Text = "Standings"
        '
        'rbtnCorpFilterUseAgentLevels
        '
        Me.rbtnCorpFilterUseAgentLevels.AutoSize = True
        Me.rbtnCorpFilterUseAgentLevels.Location = New System.Drawing.Point(11, 47)
        Me.rbtnCorpFilterUseAgentLevels.Name = "rbtnCorpFilterUseAgentLevels"
        Me.rbtnCorpFilterUseAgentLevels.Size = New System.Drawing.Size(109, 17)
        Me.rbtnCorpFilterUseAgentLevels.TabIndex = 42
        Me.rbtnCorpFilterUseAgentLevels.TabStop = True
        Me.rbtnCorpFilterUseAgentLevels.Text = "Use Agent Levels"
        Me.rbtnCorpFilterUseAgentLevels.UseVisualStyleBackColor = True
        '
        'rbtnCorpFilterUseStandings
        '
        Me.rbtnCorpFilterUseStandings.AutoSize = True
        Me.rbtnCorpFilterUseStandings.Location = New System.Drawing.Point(11, 24)
        Me.rbtnCorpFilterUseStandings.Name = "rbtnCorpFilterUseStandings"
        Me.rbtnCorpFilterUseStandings.Size = New System.Drawing.Size(94, 17)
        Me.rbtnCorpFilterUseStandings.TabIndex = 41
        Me.rbtnCorpFilterUseStandings.TabStop = True
        Me.rbtnCorpFilterUseStandings.Text = "Use Standings"
        Me.rbtnCorpFilterUseStandings.UseVisualStyleBackColor = True
        '
        'lstRequiredMats
        '
        Me.lstRequiredMats.FullRowSelect = True
        Me.lstRequiredMats.GridLines = True
        Me.lstRequiredMats.HideSelection = False
        Me.lstRequiredMats.Location = New System.Drawing.Point(389, 483)
        Me.lstRequiredMats.MultiSelect = False
        Me.lstRequiredMats.Name = "lstRequiredMats"
        Me.lstRequiredMats.Size = New System.Drawing.Size(256, 96)
        Me.lstRequiredMats.TabIndex = 65
        Me.lstRequiredMats.TabStop = False
        Me.lstRequiredMats.UseCompatibleStateImageBehavior = False
        Me.lstRequiredMats.View = System.Windows.Forms.View.Details
        '
        'lblCorporationList
        '
        Me.lblCorporationList.Location = New System.Drawing.Point(794, 392)
        Me.lblCorporationList.Name = "lblCorporationList"
        Me.lblCorporationList.Size = New System.Drawing.Size(278, 17)
        Me.lblCorporationList.TabIndex = 4
        Me.lblCorporationList.Text = "Corporation List"
        Me.lblCorporationList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(389, 464)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 19)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Total Requirements Cost:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkManufactureBP
        '
        Me.chkManufactureBP.AutoSize = True
        Me.chkManufactureBP.Location = New System.Drawing.Point(95, 97)
        Me.chkManufactureBP.Name = "chkManufactureBP"
        Me.chkManufactureBP.Size = New System.Drawing.Size(108, 17)
        Me.chkManufactureBP.TabIndex = 71
        Me.chkManufactureBP.Text = "Manufacture BPs"
        Me.chkManufactureBP.UseVisualStyleBackColor = True
        '
        'frmLPStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 585)
        Me.Controls.Add(Me.lblCorporationList)
        Me.Controls.Add(Me.lstRequiredMats)
        Me.Controls.Add(Me.gbCorpFilter)
        Me.Controls.Add(Me.lstStoreItems)
        Me.Controls.Add(Me.gbSortBy)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lstCorporations)
        Me.Controls.Add(Me.gbISKCost)
        Me.Controls.Add(Me.gbLPCost)
        Me.Controls.Add(Me.gbItemFilter)
        Me.Controls.Add(Me.gbSearchOptions)
        Me.Controls.Add(Me.gbRewardType)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLPStore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Loyalty Store Viewer"
        Me.gbRewardType.ResumeLayout(false)
        Me.gbRewardType.PerformLayout
        Me.gbSearchOptions.ResumeLayout(false)
        Me.gbSearchOptions.PerformLayout
        Me.gbItemFilter.ResumeLayout(false)
        Me.gbItemFilter.PerformLayout
        Me.gbLPCost.ResumeLayout(false)
        Me.gbLPCost.PerformLayout
        Me.gbISKCost.ResumeLayout(false)
        Me.gbISKCost.PerformLayout
        Me.gbSortBy.ResumeLayout(false)
        Me.gbSortBy.PerformLayout
        Me.gbCorpFilter.ResumeLayout(false)
        Me.gbCorpFilter.PerformLayout
        Me.gbAgentLevels.ResumeLayout(false)
        Me.gbAgentLevels.PerformLayout
        Me.gbStandings.ResumeLayout(false)
        Me.gbStandings.PerformLayout
        Me.ResumeLayout(false)

End Sub
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
    Friend WithEvents gbSearchOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnCorpswStanding As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAllCorps As System.Windows.Forms.RadioButton
    Friend WithEvents gbItemFilter As System.Windows.Forms.GroupBox
    Friend WithEvents btnResetItemFilterSearch As System.Windows.Forms.Button
    Friend WithEvents txtItemFilter As System.Windows.Forms.TextBox
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
    Friend WithEvents txtStandingsGreaterThan As System.Windows.Forms.TextBox
    Friend WithEvents lblStandingsGreaterThan As System.Windows.Forms.Label
    Friend WithEvents txtStandingsLessThan As System.Windows.Forms.TextBox
    Friend WithEvents lblStandingsLessThan As System.Windows.Forms.Label
    Friend WithEvents gbSortBy As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnSortbyProfit As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSortbyISKperLP As System.Windows.Forms.RadioButton
    Friend WithEvents lstStoreItems As System.Windows.Forms.ListView
    Friend WithEvents chkLevel3Agent As System.Windows.Forms.CheckBox
    Friend WithEvents chkLevel2Agent As System.Windows.Forms.CheckBox
    Friend WithEvents chkLevel1Agent As System.Windows.Forms.CheckBox
    Friend WithEvents chkLevel4Agent As System.Windows.Forms.CheckBox
    Friend WithEvents chkLevel5Agent As System.Windows.Forms.CheckBox
    Friend WithEvents gbCorpFilter As System.Windows.Forms.GroupBox
    Friend WithEvents gbAgentLevels As System.Windows.Forms.GroupBox
    Friend WithEvents gbStandings As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnCorpFilterUseAgentLevels As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCorpFilterUseStandings As System.Windows.Forms.RadioButton
    Friend WithEvents lstRequiredMats As System.Windows.Forms.ListView
    Friend WithEvents lblCorporationList As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkManufactureBP As CheckBox
End Class
