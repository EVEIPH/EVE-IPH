<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.chkCheckUpdatesStartup = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbGeneral = New System.Windows.Forms.GroupBox()
        Me.chkShareFacilities = New System.Windows.Forms.CheckBox()
        Me.chkDisableTracking = New System.Windows.Forms.CheckBox()
        Me.chkLoadBPsbyChar = New System.Windows.Forms.CheckBox()
        Me.chkSaveFacilitiesbyChar = New System.Windows.Forms.CheckBox()
        Me.chkLinksInCopyText = New System.Windows.Forms.CheckBox()
        Me.chkDisableSound = New System.Windows.Forms.CheckBox()
        Me.chkDisableSVR = New System.Windows.Forms.CheckBox()
        Me.chkShowToolTips = New System.Windows.Forms.CheckBox()
        Me.chkRefreshBPsonStartup = New System.Windows.Forms.CheckBox()
        Me.chkRefreshAssetsonStartup = New System.Windows.Forms.CheckBox()
        Me.chkBeanCounterManufacturing = New System.Windows.Forms.CheckBox()
        Me.cmbBeanCounterRefining = New System.Windows.Forms.ComboBox()
        Me.cmbBeanCounterManufacturing = New System.Windows.Forms.ComboBox()
        Me.chkBeanCounterRefining = New System.Windows.Forms.CheckBox()
        Me.gbStationStandings = New System.Windows.Forms.GroupBox()
        Me.txtBrokerCorpStanding = New System.Windows.Forms.TextBox()
        Me.chkBrokerCorpStanding = New System.Windows.Forms.CheckBox()
        Me.txtBrokerFactionStanding = New System.Windows.Forms.TextBox()
        Me.chkBrokerFactionStanding = New System.Windows.Forms.CheckBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.gbBuildBuySettings = New System.Windows.Forms.GroupBox()
        Me.chkBuildWhenNotEnoughItemsonMarket = New System.Windows.Forms.CheckBox()
        Me.chkAlwaysBuyRAMs = New System.Windows.Forms.CheckBox()
        Me.chkAlwaysBuyFuelBlocks = New System.Windows.Forms.CheckBox()
        Me.chkSaveBPRelicsDecryptors = New System.Windows.Forms.CheckBox()
        Me.chkBuildBuyDefault = New System.Windows.Forms.CheckBox()
        Me.chkSuggestBuildwhenBPnotOwned = New System.Windows.Forms.CheckBox()
        Me.chkBeanCounterCopy = New System.Windows.Forms.CheckBox()
        Me.cmbBeanCounterCopy = New System.Windows.Forms.ComboBox()
        Me.gbDefaultMEPE = New System.Windows.Forms.GroupBox()
        Me.txtDefaultTE = New System.Windows.Forms.TextBox()
        Me.chkDefaultTE = New System.Windows.Forms.CheckBox()
        Me.txtDefaultME = New System.Windows.Forms.TextBox()
        Me.chkDefaultME = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbShoppingList = New System.Windows.Forms.GroupBox()
        Me.chkIncludeShopListInventMats = New System.Windows.Forms.CheckBox()
        Me.chkIncludeShopListCopyMats = New System.Windows.Forms.CheckBox()
        Me.gb3rdpartyMarketRefresh = New System.Windows.Forms.GroupBox()
        Me.txtFuzzworksMarketInterval = New System.Windows.Forms.TextBox()
        Me.chkFuzzworksMarketInterval = New System.Windows.Forms.CheckBox()
        Me.gbImplants = New System.Windows.Forms.GroupBox()
        Me.gbStartupOptions = New System.Windows.Forms.GroupBox()
        Me.chkSupressESImsgs = New System.Windows.Forms.CheckBox()
        Me.chkRefreshPublicStructureDataonStartup = New System.Windows.Forms.CheckBox()
        Me.chkRefreshSystemCostIndiciesDataonStartup = New System.Windows.Forms.CheckBox()
        Me.chkRefreshMarketDataonStartup = New System.Windows.Forms.CheckBox()
        Me.gbExportOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnExportSSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportCSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportDefault = New System.Windows.Forms.RadioButton()
        Me.gbCalcAvgPrice = New System.Windows.Forms.GroupBox()
        Me.cmbSVRAvgPriceDuration = New System.Windows.Forms.ComboBox()
        Me.chkAutoUpdateSVRBPTab = New System.Windows.Forms.CheckBox()
        Me.lblSVRRegion = New System.Windows.Forms.Label()
        Me.lblSVRAvgPrice = New System.Windows.Forms.Label()
        Me.cmbSVRRegion = New System.Windows.Forms.ComboBox()
        Me.txtSVRThreshold = New System.Windows.Forms.TextBox()
        Me.lblSVRThreshold = New System.Windows.Forms.Label()
        Me.gbProxySettings = New System.Windows.Forms.GroupBox()
        Me.txtProxyAddress = New System.Windows.Forms.TextBox()
        Me.lblProxyAddress = New System.Windows.Forms.Label()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.lblProxyPort = New System.Windows.Forms.Label()
        Me.gbCharacterOptions = New System.Windows.Forms.GroupBox()
        Me.chkLoadMaxAlphaSkills = New System.Windows.Forms.CheckBox()
        Me.chkUseActiveSkills = New System.Windows.Forms.CheckBox()
        Me.chkAlphaAccount = New System.Windows.Forms.CheckBox()
        Me.gbPriceOptions = New System.Windows.Forms.GroupBox()
        Me.chkManualPriceOverride = New System.Windows.Forms.CheckBox()
        Me.gbGeneral.SuspendLayout()
        Me.gbStationStandings.SuspendLayout()
        Me.gbBuildBuySettings.SuspendLayout()
        Me.gbDefaultMEPE.SuspendLayout()
        Me.gbShoppingList.SuspendLayout()
        Me.gb3rdpartyMarketRefresh.SuspendLayout()
        Me.gbImplants.SuspendLayout()
        Me.gbStartupOptions.SuspendLayout()
        Me.gbExportOptions.SuspendLayout()
        Me.gbCalcAvgPrice.SuspendLayout()
        Me.gbProxySettings.SuspendLayout()
        Me.gbCharacterOptions.SuspendLayout()
        Me.gbPriceOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkCheckUpdatesStartup
        '
        Me.chkCheckUpdatesStartup.AutoSize = True
        Me.chkCheckUpdatesStartup.Location = New System.Drawing.Point(17, 19)
        Me.chkCheckUpdatesStartup.Name = "chkCheckUpdatesStartup"
        Me.chkCheckUpdatesStartup.Size = New System.Drawing.Size(157, 17)
        Me.chkCheckUpdatesStartup.TabIndex = 0
        Me.chkCheckUpdatesStartup.Text = "Check for Program Updates"
        Me.chkCheckUpdatesStartup.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(184, 473)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 30)
        Me.btnSave.TabIndex = 29
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(394, 473)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 30)
        Me.btnCancel.TabIndex = 31
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gbGeneral
        '
        Me.gbGeneral.Controls.Add(Me.chkShareFacilities)
        Me.gbGeneral.Controls.Add(Me.chkDisableTracking)
        Me.gbGeneral.Controls.Add(Me.chkLoadBPsbyChar)
        Me.gbGeneral.Controls.Add(Me.chkSaveFacilitiesbyChar)
        Me.gbGeneral.Controls.Add(Me.chkLinksInCopyText)
        Me.gbGeneral.Controls.Add(Me.chkDisableSound)
        Me.gbGeneral.Controls.Add(Me.chkDisableSVR)
        Me.gbGeneral.Controls.Add(Me.chkShowToolTips)
        Me.gbGeneral.Location = New System.Drawing.Point(12, 12)
        Me.gbGeneral.Name = "gbGeneral"
        Me.gbGeneral.Size = New System.Drawing.Size(235, 186)
        Me.gbGeneral.TabIndex = 4
        Me.gbGeneral.TabStop = False
        Me.gbGeneral.Text = "General:"
        '
        'chkShareFacilities
        '
        Me.chkShareFacilities.AutoSize = True
        Me.chkShareFacilities.Location = New System.Drawing.Point(17, 139)
        Me.chkShareFacilities.Name = "chkShareFacilities"
        Me.chkShareFacilities.Size = New System.Drawing.Size(131, 17)
        Me.chkShareFacilities.TabIndex = 42
        Me.chkShareFacilities.Text = "Share Saved Facilities"
        Me.chkShareFacilities.UseVisualStyleBackColor = True
        '
        'chkDisableTracking
        '
        Me.chkDisableTracking.AutoSize = True
        Me.chkDisableTracking.Location = New System.Drawing.Point(17, 159)
        Me.chkDisableTracking.Name = "chkDisableTracking"
        Me.chkDisableTracking.Size = New System.Drawing.Size(199, 17)
        Me.chkDisableTracking.TabIndex = 41
        Me.chkDisableTracking.Text = "Disable Anonomous Usage Tracking"
        Me.chkDisableTracking.UseVisualStyleBackColor = True
        '
        'chkLoadBPsbyChar
        '
        Me.chkLoadBPsbyChar.AutoSize = True
        Me.chkLoadBPsbyChar.Location = New System.Drawing.Point(17, 119)
        Me.chkLoadBPsbyChar.Name = "chkLoadBPsbyChar"
        Me.chkLoadBPsbyChar.Size = New System.Drawing.Size(162, 17)
        Me.chkLoadBPsbyChar.TabIndex = 40
        Me.chkLoadBPsbyChar.Text = "Load Blueprints by Character"
        Me.chkLoadBPsbyChar.UseVisualStyleBackColor = True
        '
        'chkSaveFacilitiesbyChar
        '
        Me.chkSaveFacilitiesbyChar.AutoSize = True
        Me.chkSaveFacilitiesbyChar.Location = New System.Drawing.Point(17, 99)
        Me.chkSaveFacilitiesbyChar.Name = "chkSaveFacilitiesbyChar"
        Me.chkSaveFacilitiesbyChar.Size = New System.Drawing.Size(185, 17)
        Me.chkSaveFacilitiesbyChar.TabIndex = 39
        Me.chkSaveFacilitiesbyChar.Text = "Save Facilities for each Character"
        Me.chkSaveFacilitiesbyChar.UseVisualStyleBackColor = True
        '
        'chkLinksInCopyText
        '
        Me.chkLinksInCopyText.Location = New System.Drawing.Point(17, 39)
        Me.chkLinksInCopyText.Name = "chkLinksInCopyText"
        Me.chkLinksInCopyText.Size = New System.Drawing.Size(214, 17)
        Me.chkLinksInCopyText.TabIndex = 38
        Me.chkLinksInCopyText.Text = "Include InGame Links in Copy Text"
        Me.chkLinksInCopyText.UseVisualStyleBackColor = True
        '
        'chkDisableSound
        '
        Me.chkDisableSound.AutoSize = True
        Me.chkDisableSound.Location = New System.Drawing.Point(17, 79)
        Me.chkDisableSound.Name = "chkDisableSound"
        Me.chkDisableSound.Size = New System.Drawing.Size(95, 17)
        Me.chkDisableSound.TabIndex = 24
        Me.chkDisableSound.Text = "Disable Sound"
        Me.chkDisableSound.UseVisualStyleBackColor = True
        '
        'chkDisableSVR
        '
        Me.chkDisableSVR.AutoSize = True
        Me.chkDisableSVR.Location = New System.Drawing.Point(17, 59)
        Me.chkDisableSVR.Name = "chkDisableSVR"
        Me.chkDisableSVR.Size = New System.Drawing.Size(129, 17)
        Me.chkDisableSVR.TabIndex = 0
        Me.chkDisableSVR.Text = "Disable SVR Updates"
        Me.chkDisableSVR.UseVisualStyleBackColor = True
        '
        'chkShowToolTips
        '
        Me.chkShowToolTips.AutoSize = True
        Me.chkShowToolTips.Location = New System.Drawing.Point(17, 19)
        Me.chkShowToolTips.Name = "chkShowToolTips"
        Me.chkShowToolTips.Size = New System.Drawing.Size(100, 17)
        Me.chkShowToolTips.TabIndex = 2
        Me.chkShowToolTips.Text = "Show Tool Tips"
        Me.chkShowToolTips.UseVisualStyleBackColor = True
        '
        'chkRefreshBPsonStartup
        '
        Me.chkRefreshBPsonStartup.AutoSize = True
        Me.chkRefreshBPsonStartup.Location = New System.Drawing.Point(17, 59)
        Me.chkRefreshBPsonStartup.Name = "chkRefreshBPsonStartup"
        Me.chkRefreshBPsonStartup.Size = New System.Drawing.Size(85, 17)
        Me.chkRefreshBPsonStartup.TabIndex = 26
        Me.chkRefreshBPsonStartup.Text = "Refresh BPs"
        Me.chkRefreshBPsonStartup.UseVisualStyleBackColor = True
        '
        'chkRefreshAssetsonStartup
        '
        Me.chkRefreshAssetsonStartup.AutoSize = True
        Me.chkRefreshAssetsonStartup.Location = New System.Drawing.Point(17, 39)
        Me.chkRefreshAssetsonStartup.Name = "chkRefreshAssetsonStartup"
        Me.chkRefreshAssetsonStartup.Size = New System.Drawing.Size(97, 17)
        Me.chkRefreshAssetsonStartup.TabIndex = 23
        Me.chkRefreshAssetsonStartup.Text = "Refresh Assets"
        Me.chkRefreshAssetsonStartup.UseVisualStyleBackColor = True
        '
        'chkBeanCounterManufacturing
        '
        Me.chkBeanCounterManufacturing.AutoSize = True
        Me.chkBeanCounterManufacturing.Location = New System.Drawing.Point(9, 19)
        Me.chkBeanCounterManufacturing.Name = "chkBeanCounterManufacturing"
        Me.chkBeanCounterManufacturing.Size = New System.Drawing.Size(195, 17)
        Me.chkBeanCounterManufacturing.TabIndex = 3
        Me.chkBeanCounterManufacturing.Text = "Manufacturing Beancounter Implant"
        Me.chkBeanCounterManufacturing.UseVisualStyleBackColor = True
        '
        'cmbBeanCounterRefining
        '
        Me.cmbBeanCounterRefining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBeanCounterRefining.FormattingEnabled = True
        Me.cmbBeanCounterRefining.Items.AddRange(New Object() {"Zainou 'Beancounter' Reprocessing RX-801", "Zainou 'Beancounter' Reprocessing RX-802", "Zainou 'Beancounter' Reprocessing RX-804"})
        Me.cmbBeanCounterRefining.Location = New System.Drawing.Point(9, 83)
        Me.cmbBeanCounterRefining.Name = "cmbBeanCounterRefining"
        Me.cmbBeanCounterRefining.Size = New System.Drawing.Size(235, 21)
        Me.cmbBeanCounterRefining.TabIndex = 5
        '
        'cmbBeanCounterManufacturing
        '
        Me.cmbBeanCounterManufacturing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBeanCounterManufacturing.FormattingEnabled = True
        Me.cmbBeanCounterManufacturing.Items.AddRange(New Object() {"Zainou 'Beancounter' Industry BX-801", "Zainou 'Beancounter' Industry BX-802", "Zainou 'Beancounter' Industry BX-804"})
        Me.cmbBeanCounterManufacturing.Location = New System.Drawing.Point(9, 38)
        Me.cmbBeanCounterManufacturing.Name = "cmbBeanCounterManufacturing"
        Me.cmbBeanCounterManufacturing.Size = New System.Drawing.Size(235, 21)
        Me.cmbBeanCounterManufacturing.TabIndex = 4
        '
        'chkBeanCounterRefining
        '
        Me.chkBeanCounterRefining.AutoSize = True
        Me.chkBeanCounterRefining.Location = New System.Drawing.Point(9, 64)
        Me.chkBeanCounterRefining.Name = "chkBeanCounterRefining"
        Me.chkBeanCounterRefining.Size = New System.Drawing.Size(192, 17)
        Me.chkBeanCounterRefining.TabIndex = 5
        Me.chkBeanCounterRefining.Text = "Reprocessing Beancounter Implant"
        Me.chkBeanCounterRefining.UseVisualStyleBackColor = True
        '
        'gbStationStandings
        '
        Me.gbStationStandings.Controls.Add(Me.txtBrokerCorpStanding)
        Me.gbStationStandings.Controls.Add(Me.chkBrokerCorpStanding)
        Me.gbStationStandings.Controls.Add(Me.txtBrokerFactionStanding)
        Me.gbStationStandings.Controls.Add(Me.chkBrokerFactionStanding)
        Me.gbStationStandings.Location = New System.Drawing.Point(253, 151)
        Me.gbStationStandings.Name = "gbStationStandings"
        Me.gbStationStandings.Size = New System.Drawing.Size(160, 63)
        Me.gbStationStandings.TabIndex = 7
        Me.gbStationStandings.TabStop = False
        Me.gbStationStandings.Text = "Station (base) Standings:"
        '
        'txtBrokerCorpStanding
        '
        Me.txtBrokerCorpStanding.Location = New System.Drawing.Point(110, 15)
        Me.txtBrokerCorpStanding.MaxLength = 5
        Me.txtBrokerCorpStanding.Name = "txtBrokerCorpStanding"
        Me.txtBrokerCorpStanding.Size = New System.Drawing.Size(41, 20)
        Me.txtBrokerCorpStanding.TabIndex = 26
        Me.txtBrokerCorpStanding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkBrokerCorpStanding
        '
        Me.chkBrokerCorpStanding.Location = New System.Drawing.Point(9, 17)
        Me.chkBrokerCorpStanding.Name = "chkBrokerCorpStanding"
        Me.chkBrokerCorpStanding.Size = New System.Drawing.Size(85, 17)
        Me.chkBrokerCorpStanding.TabIndex = 25
        Me.chkBrokerCorpStanding.Text = "Broker Corp:"
        Me.chkBrokerCorpStanding.UseVisualStyleBackColor = True
        '
        'txtBrokerFactionStanding
        '
        Me.txtBrokerFactionStanding.Location = New System.Drawing.Point(110, 37)
        Me.txtBrokerFactionStanding.MaxLength = 5
        Me.txtBrokerFactionStanding.Name = "txtBrokerFactionStanding"
        Me.txtBrokerFactionStanding.Size = New System.Drawing.Size(41, 20)
        Me.txtBrokerFactionStanding.TabIndex = 28
        Me.txtBrokerFactionStanding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkBrokerFactionStanding
        '
        Me.chkBrokerFactionStanding.Location = New System.Drawing.Point(9, 39)
        Me.chkBrokerFactionStanding.Name = "chkBrokerFactionStanding"
        Me.chkBrokerFactionStanding.Size = New System.Drawing.Size(98, 17)
        Me.chkBrokerFactionStanding.TabIndex = 27
        Me.chkBrokerFactionStanding.Text = "Broker Faction:"
        Me.chkBrokerFactionStanding.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(289, 473)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(101, 30)
        Me.btnReset.TabIndex = 30
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'gbBuildBuySettings
        '
        Me.gbBuildBuySettings.Controls.Add(Me.chkBuildWhenNotEnoughItemsonMarket)
        Me.gbBuildBuySettings.Controls.Add(Me.chkAlwaysBuyRAMs)
        Me.gbBuildBuySettings.Controls.Add(Me.chkAlwaysBuyFuelBlocks)
        Me.gbBuildBuySettings.Controls.Add(Me.chkSaveBPRelicsDecryptors)
        Me.gbBuildBuySettings.Controls.Add(Me.chkBuildBuyDefault)
        Me.gbBuildBuySettings.Controls.Add(Me.chkSuggestBuildwhenBPnotOwned)
        Me.gbBuildBuySettings.Location = New System.Drawing.Point(417, 175)
        Me.gbBuildBuySettings.Name = "gbBuildBuySettings"
        Me.gbBuildBuySettings.Size = New System.Drawing.Size(250, 145)
        Me.gbBuildBuySettings.TabIndex = 9
        Me.gbBuildBuySettings.TabStop = False
        Me.gbBuildBuySettings.Text = "Build Settings:"
        '
        'chkBuildWhenNotEnoughItemsonMarket
        '
        Me.chkBuildWhenNotEnoughItemsonMarket.AutoSize = True
        Me.chkBuildWhenNotEnoughItemsonMarket.Location = New System.Drawing.Point(9, 58)
        Me.chkBuildWhenNotEnoughItemsonMarket.Name = "chkBuildWhenNotEnoughItemsonMarket"
        Me.chkBuildWhenNotEnoughItemsonMarket.Size = New System.Drawing.Size(213, 17)
        Me.chkBuildWhenNotEnoughItemsonMarket.TabIndex = 41
        Me.chkBuildWhenNotEnoughItemsonMarket.Text = "Build when not enough items on Market"
        Me.chkBuildWhenNotEnoughItemsonMarket.UseVisualStyleBackColor = True
        '
        'chkAlwaysBuyRAMs
        '
        Me.chkAlwaysBuyRAMs.AutoSize = True
        Me.chkAlwaysBuyRAMs.Location = New System.Drawing.Point(9, 118)
        Me.chkAlwaysBuyRAMs.Name = "chkAlwaysBuyRAMs"
        Me.chkAlwaysBuyRAMs.Size = New System.Drawing.Size(121, 17)
        Me.chkAlwaysBuyRAMs.TabIndex = 40
        Me.chkAlwaysBuyRAMs.Text = "Always Buy R.A.M.s"
        Me.chkAlwaysBuyRAMs.UseVisualStyleBackColor = True
        '
        'chkAlwaysBuyFuelBlocks
        '
        Me.chkAlwaysBuyFuelBlocks.AutoSize = True
        Me.chkAlwaysBuyFuelBlocks.Location = New System.Drawing.Point(9, 98)
        Me.chkAlwaysBuyFuelBlocks.Name = "chkAlwaysBuyFuelBlocks"
        Me.chkAlwaysBuyFuelBlocks.Size = New System.Drawing.Size(138, 17)
        Me.chkAlwaysBuyFuelBlocks.TabIndex = 39
        Me.chkAlwaysBuyFuelBlocks.Text = "Always Buy Fuel Blocks"
        Me.chkAlwaysBuyFuelBlocks.UseVisualStyleBackColor = True
        '
        'chkSaveBPRelicsDecryptors
        '
        Me.chkSaveBPRelicsDecryptors.AutoSize = True
        Me.chkSaveBPRelicsDecryptors.Location = New System.Drawing.Point(9, 78)
        Me.chkSaveBPRelicsDecryptors.Name = "chkSaveBPRelicsDecryptors"
        Me.chkSaveBPRelicsDecryptors.Size = New System.Drawing.Size(212, 17)
        Me.chkSaveBPRelicsDecryptors.TabIndex = 38
        Me.chkSaveBPRelicsDecryptors.Text = "Save Relics and Decryptors on BP Tab"
        Me.chkSaveBPRelicsDecryptors.UseVisualStyleBackColor = True
        '
        'chkBuildBuyDefault
        '
        Me.chkBuildBuyDefault.AutoSize = True
        Me.chkBuildBuyDefault.Location = New System.Drawing.Point(9, 18)
        Me.chkBuildBuyDefault.Name = "chkBuildBuyDefault"
        Me.chkBuildBuyDefault.Size = New System.Drawing.Size(109, 17)
        Me.chkBuildBuyDefault.TabIndex = 32
        Me.chkBuildBuyDefault.Text = "Default Build/Buy"
        Me.chkBuildBuyDefault.UseVisualStyleBackColor = True
        '
        'chkSuggestBuildwhenBPnotOwned
        '
        Me.chkSuggestBuildwhenBPnotOwned.AutoSize = True
        Me.chkSuggestBuildwhenBPnotOwned.Location = New System.Drawing.Point(9, 38)
        Me.chkSuggestBuildwhenBPnotOwned.Name = "chkSuggestBuildwhenBPnotOwned"
        Me.chkSuggestBuildwhenBPnotOwned.Size = New System.Drawing.Size(222, 17)
        Me.chkSuggestBuildwhenBPnotOwned.TabIndex = 37
        Me.chkSuggestBuildwhenBPnotOwned.Text = "Suggest Build option when BP not owned"
        Me.chkSuggestBuildwhenBPnotOwned.UseVisualStyleBackColor = True
        '
        'chkBeanCounterCopy
        '
        Me.chkBeanCounterCopy.AutoSize = True
        Me.chkBeanCounterCopy.Location = New System.Drawing.Point(9, 109)
        Me.chkBeanCounterCopy.Name = "chkBeanCounterCopy"
        Me.chkBeanCounterCopy.Size = New System.Drawing.Size(151, 17)
        Me.chkBeanCounterCopy.TabIndex = 35
        Me.chkBeanCounterCopy.Text = "Copy Beancounter Implant"
        Me.chkBeanCounterCopy.UseVisualStyleBackColor = True
        '
        'cmbBeanCounterCopy
        '
        Me.cmbBeanCounterCopy.DisplayMember = "Zainou 'Beancounter' Science SC-805"
        Me.cmbBeanCounterCopy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBeanCounterCopy.FormattingEnabled = True
        Me.cmbBeanCounterCopy.Items.AddRange(New Object() {"Zainou 'Beancounter' Science SC-801", "Zainou 'Beancounter' Science SC-803", "Zainou 'Beancounter' Science SC-805"})
        Me.cmbBeanCounterCopy.Location = New System.Drawing.Point(9, 126)
        Me.cmbBeanCounterCopy.Name = "cmbBeanCounterCopy"
        Me.cmbBeanCounterCopy.Size = New System.Drawing.Size(235, 21)
        Me.cmbBeanCounterCopy.TabIndex = 36
        '
        'gbDefaultMEPE
        '
        Me.gbDefaultMEPE.Controls.Add(Me.txtDefaultTE)
        Me.gbDefaultMEPE.Controls.Add(Me.chkDefaultTE)
        Me.gbDefaultMEPE.Controls.Add(Me.txtDefaultME)
        Me.gbDefaultMEPE.Controls.Add(Me.chkDefaultME)
        Me.gbDefaultMEPE.Location = New System.Drawing.Point(253, 12)
        Me.gbDefaultMEPE.Name = "gbDefaultMEPE"
        Me.gbDefaultMEPE.Size = New System.Drawing.Size(160, 63)
        Me.gbDefaultMEPE.TabIndex = 34
        Me.gbDefaultMEPE.TabStop = False
        Me.gbDefaultMEPE.Text = "Default ME/TE:"
        '
        'txtDefaultTE
        '
        Me.txtDefaultTE.Location = New System.Drawing.Point(110, 37)
        Me.txtDefaultTE.Name = "txtDefaultTE"
        Me.txtDefaultTE.Size = New System.Drawing.Size(41, 20)
        Me.txtDefaultTE.TabIndex = 26
        Me.txtDefaultTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkDefaultTE
        '
        Me.chkDefaultTE.Location = New System.Drawing.Point(9, 39)
        Me.chkDefaultTE.Name = "chkDefaultTE"
        Me.chkDefaultTE.Size = New System.Drawing.Size(85, 17)
        Me.chkDefaultTE.TabIndex = 25
        Me.chkDefaultTE.Text = "Default TE:"
        Me.chkDefaultTE.UseVisualStyleBackColor = True
        '
        'txtDefaultME
        '
        Me.txtDefaultME.Location = New System.Drawing.Point(110, 15)
        Me.txtDefaultME.Name = "txtDefaultME"
        Me.txtDefaultME.Size = New System.Drawing.Size(41, 20)
        Me.txtDefaultME.TabIndex = 22
        Me.txtDefaultME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkDefaultME
        '
        Me.chkDefaultME.Location = New System.Drawing.Point(9, 16)
        Me.chkDefaultME.Name = "chkDefaultME"
        Me.chkDefaultME.Size = New System.Drawing.Size(85, 17)
        Me.chkDefaultME.TabIndex = 21
        Me.chkDefaultME.Text = "Default ME:"
        Me.chkDefaultME.UseVisualStyleBackColor = True
        '
        'gbShoppingList
        '
        Me.gbShoppingList.Controls.Add(Me.chkIncludeShopListInventMats)
        Me.gbShoppingList.Controls.Add(Me.chkIncludeShopListCopyMats)
        Me.gbShoppingList.Location = New System.Drawing.Point(253, 81)
        Me.gbShoppingList.Name = "gbShoppingList"
        Me.gbShoppingList.Size = New System.Drawing.Size(160, 63)
        Me.gbShoppingList.TabIndex = 37
        Me.gbShoppingList.TabStop = False
        Me.gbShoppingList.Text = "Shopping List:"
        '
        'chkIncludeShopListInventMats
        '
        Me.chkIncludeShopListInventMats.AutoSize = True
        Me.chkIncludeShopListInventMats.Location = New System.Drawing.Point(9, 19)
        Me.chkIncludeShopListInventMats.Name = "chkIncludeShopListInventMats"
        Me.chkIncludeShopListInventMats.Size = New System.Drawing.Size(134, 17)
        Me.chkIncludeShopListInventMats.TabIndex = 0
        Me.chkIncludeShopListInventMats.Text = "Include Invention Mats"
        Me.chkIncludeShopListInventMats.UseVisualStyleBackColor = True
        '
        'chkIncludeShopListCopyMats
        '
        Me.chkIncludeShopListCopyMats.AutoSize = True
        Me.chkIncludeShopListCopyMats.Location = New System.Drawing.Point(9, 40)
        Me.chkIncludeShopListCopyMats.Name = "chkIncludeShopListCopyMats"
        Me.chkIncludeShopListCopyMats.Size = New System.Drawing.Size(114, 17)
        Me.chkIncludeShopListCopyMats.TabIndex = 1
        Me.chkIncludeShopListCopyMats.Text = "Include Copy Mats"
        Me.chkIncludeShopListCopyMats.UseVisualStyleBackColor = True
        '
        'gb3rdpartyMarketRefresh
        '
        Me.gb3rdpartyMarketRefresh.Controls.Add(Me.txtFuzzworksMarketInterval)
        Me.gb3rdpartyMarketRefresh.Controls.Add(Me.chkFuzzworksMarketInterval)
        Me.gb3rdpartyMarketRefresh.Location = New System.Drawing.Point(417, 326)
        Me.gb3rdpartyMarketRefresh.Name = "gb3rdpartyMarketRefresh"
        Me.gb3rdpartyMarketRefresh.Size = New System.Drawing.Size(250, 44)
        Me.gb3rdpartyMarketRefresh.TabIndex = 38
        Me.gb3rdpartyMarketRefresh.TabStop = False
        Me.gb3rdpartyMarketRefresh.Text = "Fuzzworks Price Updates:"
        '
        'txtFuzzworksMarketInterval
        '
        Me.txtFuzzworksMarketInterval.Location = New System.Drawing.Point(165, 16)
        Me.txtFuzzworksMarketInterval.Name = "txtFuzzworksMarketInterval"
        Me.txtFuzzworksMarketInterval.Size = New System.Drawing.Size(41, 20)
        Me.txtFuzzworksMarketInterval.TabIndex = 24
        Me.txtFuzzworksMarketInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkFuzzworksMarketInterval
        '
        Me.chkFuzzworksMarketInterval.AutoSize = True
        Me.chkFuzzworksMarketInterval.Location = New System.Drawing.Point(9, 19)
        Me.chkFuzzworksMarketInterval.Name = "chkFuzzworksMarketInterval"
        Me.chkFuzzworksMarketInterval.Size = New System.Drawing.Size(150, 17)
        Me.chkFuzzworksMarketInterval.TabIndex = 23
        Me.chkFuzzworksMarketInterval.Text = "Refresh Interval (Minutes):"
        Me.chkFuzzworksMarketInterval.UseVisualStyleBackColor = True
        '
        'gbImplants
        '
        Me.gbImplants.Controls.Add(Me.chkBeanCounterManufacturing)
        Me.gbImplants.Controls.Add(Me.chkBeanCounterCopy)
        Me.gbImplants.Controls.Add(Me.cmbBeanCounterManufacturing)
        Me.gbImplants.Controls.Add(Me.chkBeanCounterRefining)
        Me.gbImplants.Controls.Add(Me.cmbBeanCounterCopy)
        Me.gbImplants.Controls.Add(Me.cmbBeanCounterRefining)
        Me.gbImplants.Location = New System.Drawing.Point(417, 12)
        Me.gbImplants.Name = "gbImplants"
        Me.gbImplants.Size = New System.Drawing.Size(250, 157)
        Me.gbImplants.TabIndex = 36
        Me.gbImplants.TabStop = False
        Me.gbImplants.Text = "Implants:"
        '
        'gbStartupOptions
        '
        Me.gbStartupOptions.Controls.Add(Me.chkSupressESImsgs)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshPublicStructureDataonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshSystemCostIndiciesDataonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshMarketDataonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshBPsonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkCheckUpdatesStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshAssetsonStartup)
        Me.gbStartupOptions.Location = New System.Drawing.Point(12, 204)
        Me.gbStartupOptions.Name = "gbStartupOptions"
        Me.gbStartupOptions.Size = New System.Drawing.Size(235, 162)
        Me.gbStartupOptions.TabIndex = 39
        Me.gbStartupOptions.TabStop = False
        Me.gbStartupOptions.Text = "Startup Options"
        '
        'chkSupressESImsgs
        '
        Me.chkSupressESImsgs.AutoSize = True
        Me.chkSupressESImsgs.Location = New System.Drawing.Point(17, 139)
        Me.chkSupressESImsgs.Name = "chkSupressESImsgs"
        Me.chkSupressESImsgs.Size = New System.Drawing.Size(168, 17)
        Me.chkSupressESImsgs.TabIndex = 31
        Me.chkSupressESImsgs.Text = "Supress ESI Status Messages"
        Me.chkSupressESImsgs.UseVisualStyleBackColor = True
        '
        'chkRefreshPublicStructureDataonStartup
        '
        Me.chkRefreshPublicStructureDataonStartup.AutoSize = True
        Me.chkRefreshPublicStructureDataonStartup.Location = New System.Drawing.Point(17, 119)
        Me.chkRefreshPublicStructureDataonStartup.Name = "chkRefreshPublicStructureDataonStartup"
        Me.chkRefreshPublicStructureDataonStartup.Size = New System.Drawing.Size(167, 17)
        Me.chkRefreshPublicStructureDataonStartup.TabIndex = 30
        Me.chkRefreshPublicStructureDataonStartup.Text = "Refresh Public Structure Data"
        Me.chkRefreshPublicStructureDataonStartup.UseVisualStyleBackColor = True
        '
        'chkRefreshSystemCostIndiciesDataonStartup
        '
        Me.chkRefreshSystemCostIndiciesDataonStartup.AutoSize = True
        Me.chkRefreshSystemCostIndiciesDataonStartup.Location = New System.Drawing.Point(17, 99)
        Me.chkRefreshSystemCostIndiciesDataonStartup.Name = "chkRefreshSystemCostIndiciesDataonStartup"
        Me.chkRefreshSystemCostIndiciesDataonStartup.Size = New System.Drawing.Size(179, 17)
        Me.chkRefreshSystemCostIndiciesDataonStartup.TabIndex = 29
        Me.chkRefreshSystemCostIndiciesDataonStartup.Text = "Refresh System Industry Indicies"
        Me.chkRefreshSystemCostIndiciesDataonStartup.UseVisualStyleBackColor = True
        '
        'chkRefreshMarketDataonStartup
        '
        Me.chkRefreshMarketDataonStartup.AutoSize = True
        Me.chkRefreshMarketDataonStartup.Location = New System.Drawing.Point(17, 79)
        Me.chkRefreshMarketDataonStartup.Name = "chkRefreshMarketDataonStartup"
        Me.chkRefreshMarketDataonStartup.Size = New System.Drawing.Size(125, 17)
        Me.chkRefreshMarketDataonStartup.TabIndex = 28
        Me.chkRefreshMarketDataonStartup.Text = "Refresh Market Data"
        Me.chkRefreshMarketDataonStartup.UseVisualStyleBackColor = True
        '
        'gbExportOptions
        '
        Me.gbExportOptions.Controls.Add(Me.rbtnExportSSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportCSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportDefault)
        Me.gbExportOptions.Location = New System.Drawing.Point(252, 306)
        Me.gbExportOptions.Name = "gbExportOptions"
        Me.gbExportOptions.Size = New System.Drawing.Size(161, 64)
        Me.gbExportOptions.TabIndex = 38
        Me.gbExportOptions.TabStop = False
        Me.gbExportOptions.Text = "Export Data in:"
        '
        'rbtnExportSSV
        '
        Me.rbtnExportSSV.AutoSize = True
        Me.rbtnExportSSV.Location = New System.Drawing.Point(113, 25)
        Me.rbtnExportSSV.Name = "rbtnExportSSV"
        Me.rbtnExportSSV.Size = New System.Drawing.Size(46, 17)
        Me.rbtnExportSSV.TabIndex = 2
        Me.rbtnExportSSV.TabStop = True
        Me.rbtnExportSSV.Text = "SSV"
        Me.rbtnExportSSV.UseVisualStyleBackColor = True
        '
        'rbtnExportCSV
        '
        Me.rbtnExportCSV.AutoSize = True
        Me.rbtnExportCSV.Location = New System.Drawing.Point(66, 25)
        Me.rbtnExportCSV.Name = "rbtnExportCSV"
        Me.rbtnExportCSV.Size = New System.Drawing.Size(46, 17)
        Me.rbtnExportCSV.TabIndex = 1
        Me.rbtnExportCSV.TabStop = True
        Me.rbtnExportCSV.Text = "CSV"
        Me.rbtnExportCSV.UseVisualStyleBackColor = True
        '
        'rbtnExportDefault
        '
        Me.rbtnExportDefault.AutoSize = True
        Me.rbtnExportDefault.Location = New System.Drawing.Point(9, 25)
        Me.rbtnExportDefault.Name = "rbtnExportDefault"
        Me.rbtnExportDefault.Size = New System.Drawing.Size(59, 17)
        Me.rbtnExportDefault.TabIndex = 0
        Me.rbtnExportDefault.TabStop = True
        Me.rbtnExportDefault.Text = "Default"
        Me.rbtnExportDefault.UseVisualStyleBackColor = True
        '
        'gbCalcAvgPrice
        '
        Me.gbCalcAvgPrice.Controls.Add(Me.cmbSVRAvgPriceDuration)
        Me.gbCalcAvgPrice.Controls.Add(Me.chkAutoUpdateSVRBPTab)
        Me.gbCalcAvgPrice.Controls.Add(Me.lblSVRRegion)
        Me.gbCalcAvgPrice.Controls.Add(Me.lblSVRAvgPrice)
        Me.gbCalcAvgPrice.Controls.Add(Me.cmbSVRRegion)
        Me.gbCalcAvgPrice.Controls.Add(Me.txtSVRThreshold)
        Me.gbCalcAvgPrice.Controls.Add(Me.lblSVRThreshold)
        Me.gbCalcAvgPrice.Location = New System.Drawing.Point(12, 372)
        Me.gbCalcAvgPrice.Name = "gbCalcAvgPrice"
        Me.gbCalcAvgPrice.Size = New System.Drawing.Size(235, 87)
        Me.gbCalcAvgPrice.TabIndex = 40
        Me.gbCalcAvgPrice.TabStop = False
        Me.gbCalcAvgPrice.Text = "SVR Settings:"
        '
        'cmbSVRAvgPriceDuration
        '
        Me.cmbSVRAvgPriceDuration.FormattingEnabled = True
        Me.cmbSVRAvgPriceDuration.Items.AddRange(New Object() {"7", "15", "30", "60", "90", "180", "365"})
        Me.cmbSVRAvgPriceDuration.Location = New System.Drawing.Point(188, 14)
        Me.cmbSVRAvgPriceDuration.MaxLength = 3
        Me.cmbSVRAvgPriceDuration.Name = "cmbSVRAvgPriceDuration"
        Me.cmbSVRAvgPriceDuration.Size = New System.Drawing.Size(41, 21)
        Me.cmbSVRAvgPriceDuration.TabIndex = 3
        '
        'chkAutoUpdateSVRBPTab
        '
        Me.chkAutoUpdateSVRBPTab.AutoSize = True
        Me.chkAutoUpdateSVRBPTab.Location = New System.Drawing.Point(17, 64)
        Me.chkAutoUpdateSVRBPTab.Name = "chkAutoUpdateSVRBPTab"
        Me.chkAutoUpdateSVRBPTab.Size = New System.Drawing.Size(203, 17)
        Me.chkAutoUpdateSVRBPTab.TabIndex = 39
        Me.chkAutoUpdateSVRBPTab.Text = "Automatically update SVR on BP Tab"
        Me.chkAutoUpdateSVRBPTab.UseVisualStyleBackColor = True
        '
        'lblSVRRegion
        '
        Me.lblSVRRegion.AutoSize = True
        Me.lblSVRRegion.Location = New System.Drawing.Point(17, 42)
        Me.lblSVRRegion.Name = "lblSVRRegion"
        Me.lblSVRRegion.Size = New System.Drawing.Size(44, 13)
        Me.lblSVRRegion.TabIndex = 4
        Me.lblSVRRegion.Text = "Region:"
        '
        'lblSVRAvgPrice
        '
        Me.lblSVRAvgPrice.Location = New System.Drawing.Point(111, 10)
        Me.lblSVRAvgPrice.Name = "lblSVRAvgPrice"
        Me.lblSVRAvgPrice.Size = New System.Drawing.Size(78, 28)
        Me.lblSVRAvgPrice.TabIndex = 2
        Me.lblSVRAvgPrice.Text = "Average Days:"
        Me.lblSVRAvgPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSVRRegion
        '
        Me.cmbSVRRegion.FormattingEnabled = True
        Me.cmbSVRRegion.Location = New System.Drawing.Point(61, 39)
        Me.cmbSVRRegion.Name = "cmbSVRRegion"
        Me.cmbSVRRegion.Size = New System.Drawing.Size(168, 21)
        Me.cmbSVRRegion.TabIndex = 5
        '
        'txtSVRThreshold
        '
        Me.txtSVRThreshold.Location = New System.Drawing.Point(61, 15)
        Me.txtSVRThreshold.MaxLength = 10
        Me.txtSVRThreshold.Name = "txtSVRThreshold"
        Me.txtSVRThreshold.Size = New System.Drawing.Size(45, 20)
        Me.txtSVRThreshold.TabIndex = 1
        Me.txtSVRThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSVRThreshold
        '
        Me.lblSVRThreshold.AutoSize = True
        Me.lblSVRThreshold.Location = New System.Drawing.Point(4, 18)
        Me.lblSVRThreshold.Name = "lblSVRThreshold"
        Me.lblSVRThreshold.Size = New System.Drawing.Size(57, 13)
        Me.lblSVRThreshold.TabIndex = 0
        Me.lblSVRThreshold.Text = "Threshold:"
        '
        'gbProxySettings
        '
        Me.gbProxySettings.Controls.Add(Me.txtProxyAddress)
        Me.gbProxySettings.Controls.Add(Me.lblProxyAddress)
        Me.gbProxySettings.Controls.Add(Me.txtProxyPort)
        Me.gbProxySettings.Controls.Add(Me.lblProxyPort)
        Me.gbProxySettings.Location = New System.Drawing.Point(417, 376)
        Me.gbProxySettings.Name = "gbProxySettings"
        Me.gbProxySettings.Size = New System.Drawing.Size(250, 83)
        Me.gbProxySettings.TabIndex = 41
        Me.gbProxySettings.TabStop = False
        Me.gbProxySettings.Text = "Proxy Settings:"
        '
        'txtProxyAddress
        '
        Me.txtProxyAddress.Location = New System.Drawing.Point(54, 18)
        Me.txtProxyAddress.MaxLength = 0
        Me.txtProxyAddress.Name = "txtProxyAddress"
        Me.txtProxyAddress.Size = New System.Drawing.Size(190, 20)
        Me.txtProxyAddress.TabIndex = 5
        '
        'lblProxyAddress
        '
        Me.lblProxyAddress.AutoSize = True
        Me.lblProxyAddress.Location = New System.Drawing.Point(6, 21)
        Me.lblProxyAddress.Name = "lblProxyAddress"
        Me.lblProxyAddress.Size = New System.Drawing.Size(48, 13)
        Me.lblProxyAddress.TabIndex = 4
        Me.lblProxyAddress.Text = "Address:"
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Location = New System.Drawing.Point(54, 44)
        Me.txtProxyPort.MaxLength = 0
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(45, 20)
        Me.txtProxyPort.TabIndex = 3
        '
        'lblProxyPort
        '
        Me.lblProxyPort.AutoSize = True
        Me.lblProxyPort.Location = New System.Drawing.Point(25, 47)
        Me.lblProxyPort.Name = "lblProxyPort"
        Me.lblProxyPort.Size = New System.Drawing.Size(29, 13)
        Me.lblProxyPort.TabIndex = 2
        Me.lblProxyPort.Text = "Port:"
        '
        'gbCharacterOptions
        '
        Me.gbCharacterOptions.Controls.Add(Me.chkLoadMaxAlphaSkills)
        Me.gbCharacterOptions.Controls.Add(Me.chkUseActiveSkills)
        Me.gbCharacterOptions.Controls.Add(Me.chkAlphaAccount)
        Me.gbCharacterOptions.Location = New System.Drawing.Point(253, 220)
        Me.gbCharacterOptions.Name = "gbCharacterOptions"
        Me.gbCharacterOptions.Size = New System.Drawing.Size(160, 80)
        Me.gbCharacterOptions.TabIndex = 39
        Me.gbCharacterOptions.TabStop = False
        Me.gbCharacterOptions.Text = "Character Options:"
        '
        'chkLoadMaxAlphaSkills
        '
        Me.chkLoadMaxAlphaSkills.AutoSize = True
        Me.chkLoadMaxAlphaSkills.Location = New System.Drawing.Point(9, 38)
        Me.chkLoadMaxAlphaSkills.Name = "chkLoadMaxAlphaSkills"
        Me.chkLoadMaxAlphaSkills.Size = New System.Drawing.Size(147, 17)
        Me.chkLoadMaxAlphaSkills.TabIndex = 33
        Me.chkLoadMaxAlphaSkills.Text = "Max Alpha Skills (Dummy)"
        Me.chkLoadMaxAlphaSkills.UseVisualStyleBackColor = True
        '
        'chkUseActiveSkills
        '
        Me.chkUseActiveSkills.AutoSize = True
        Me.chkUseActiveSkills.Location = New System.Drawing.Point(9, 57)
        Me.chkUseActiveSkills.Name = "chkUseActiveSkills"
        Me.chkUseActiveSkills.Size = New System.Drawing.Size(105, 17)
        Me.chkUseActiveSkills.TabIndex = 32
        Me.chkUseActiveSkills.Text = "Use Active Skills"
        Me.chkUseActiveSkills.UseVisualStyleBackColor = True
        '
        'chkAlphaAccount
        '
        Me.chkAlphaAccount.AutoSize = True
        Me.chkAlphaAccount.Location = New System.Drawing.Point(9, 16)
        Me.chkAlphaAccount.Name = "chkAlphaAccount"
        Me.chkAlphaAccount.Size = New System.Drawing.Size(145, 17)
        Me.chkAlphaAccount.TabIndex = 31
        Me.chkAlphaAccount.Text = "Alpha Account (.25% tax)"
        Me.chkAlphaAccount.UseVisualStyleBackColor = True
        '
        'gbPriceOptions
        '
        Me.gbPriceOptions.Controls.Add(Me.chkManualPriceOverride)
        Me.gbPriceOptions.Location = New System.Drawing.Point(252, 376)
        Me.gbPriceOptions.Name = "gbPriceOptions"
        Me.gbPriceOptions.Size = New System.Drawing.Size(161, 83)
        Me.gbPriceOptions.TabIndex = 39
        Me.gbPriceOptions.TabStop = False
        Me.gbPriceOptions.Text = "Price Options:"
        '
        'chkManualPriceOverride
        '
        Me.chkManualPriceOverride.Location = New System.Drawing.Point(10, 21)
        Me.chkManualPriceOverride.Name = "chkManualPriceOverride"
        Me.chkManualPriceOverride.Size = New System.Drawing.Size(145, 30)
        Me.chkManualPriceOverride.TabIndex = 33
        Me.chkManualPriceOverride.Text = "Do not overwrite manual price updates."
        Me.chkManualPriceOverride.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(678, 516)
        Me.Controls.Add(Me.gbPriceOptions)
        Me.Controls.Add(Me.gbCharacterOptions)
        Me.Controls.Add(Me.gbBuildBuySettings)
        Me.Controls.Add(Me.gbProxySettings)
        Me.Controls.Add(Me.gbCalcAvgPrice)
        Me.Controls.Add(Me.gbExportOptions)
        Me.Controls.Add(Me.gbStartupOptions)
        Me.Controls.Add(Me.gbImplants)
        Me.Controls.Add(Me.gb3rdpartyMarketRefresh)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.gbStationStandings)
        Me.Controls.Add(Me.gbGeneral)
        Me.Controls.Add(Me.gbDefaultMEPE)
        Me.Controls.Add(Me.gbShoppingList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Application Settings"
        Me.gbGeneral.ResumeLayout(False)
        Me.gbGeneral.PerformLayout()
        Me.gbStationStandings.ResumeLayout(False)
        Me.gbStationStandings.PerformLayout()
        Me.gbBuildBuySettings.ResumeLayout(False)
        Me.gbBuildBuySettings.PerformLayout()
        Me.gbDefaultMEPE.ResumeLayout(False)
        Me.gbDefaultMEPE.PerformLayout()
        Me.gbShoppingList.ResumeLayout(False)
        Me.gbShoppingList.PerformLayout()
        Me.gb3rdpartyMarketRefresh.ResumeLayout(False)
        Me.gb3rdpartyMarketRefresh.PerformLayout()
        Me.gbImplants.ResumeLayout(False)
        Me.gbImplants.PerformLayout()
        Me.gbStartupOptions.ResumeLayout(False)
        Me.gbStartupOptions.PerformLayout()
        Me.gbExportOptions.ResumeLayout(False)
        Me.gbExportOptions.PerformLayout()
        Me.gbCalcAvgPrice.ResumeLayout(False)
        Me.gbCalcAvgPrice.PerformLayout()
        Me.gbProxySettings.ResumeLayout(False)
        Me.gbProxySettings.PerformLayout()
        Me.gbCharacterOptions.ResumeLayout(False)
        Me.gbCharacterOptions.PerformLayout()
        Me.gbPriceOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkCheckUpdatesStartup As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowToolTips As System.Windows.Forms.CheckBox
    Friend WithEvents chkBeanCounterRefining As System.Windows.Forms.CheckBox
    Friend WithEvents cmbBeanCounterRefining As System.Windows.Forms.ComboBox
    Friend WithEvents chkBeanCounterManufacturing As System.Windows.Forms.CheckBox
    Friend WithEvents cmbBeanCounterManufacturing As System.Windows.Forms.ComboBox
    Friend WithEvents gbStationStandings As System.Windows.Forms.GroupBox
    Friend WithEvents txtBrokerCorpStanding As System.Windows.Forms.TextBox
    Friend WithEvents chkBrokerCorpStanding As System.Windows.Forms.CheckBox
    Friend WithEvents txtBrokerFactionStanding As System.Windows.Forms.TextBox
    Friend WithEvents chkBrokerFactionStanding As System.Windows.Forms.CheckBox
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents gbBuildBuySettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkBuildBuyDefault As System.Windows.Forms.CheckBox
    Friend WithEvents chkBeanCounterCopy As System.Windows.Forms.CheckBox
    Friend WithEvents cmbBeanCounterCopy As System.Windows.Forms.ComboBox
    Friend WithEvents gbDefaultMEPE As System.Windows.Forms.GroupBox
    Friend WithEvents txtDefaultTE As System.Windows.Forms.TextBox
    Friend WithEvents chkDefaultTE As System.Windows.Forms.CheckBox
    Friend WithEvents txtDefaultME As System.Windows.Forms.TextBox
    Friend WithEvents chkDefaultME As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisableSVR As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents gbShoppingList As System.Windows.Forms.GroupBox
    Friend WithEvents chkIncludeShopListCopyMats As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeShopListInventMats As System.Windows.Forms.CheckBox
    Friend WithEvents chkSuggestBuildwhenBPnotOwned As System.Windows.Forms.CheckBox
    Friend WithEvents gb3rdpartyMarketRefresh As System.Windows.Forms.GroupBox
    Friend WithEvents txtFuzzworksMarketInterval As System.Windows.Forms.TextBox
    Friend WithEvents chkFuzzworksMarketInterval As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshAssetsonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents gbImplants As System.Windows.Forms.GroupBox
    Friend WithEvents chkDisableSound As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshBPsonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents gbStartupOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkRefreshSystemCostIndiciesDataonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshMarketDataonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents gbExportOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnExportDefault As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportSSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportCSV As System.Windows.Forms.RadioButton
    Friend WithEvents chkSaveBPRelicsDecryptors As System.Windows.Forms.CheckBox
    Friend WithEvents chkLinksInCopyText As System.Windows.Forms.CheckBox
    Friend WithEvents gbCalcAvgPrice As System.Windows.Forms.GroupBox
    Friend WithEvents lblSVRRegion As System.Windows.Forms.Label
    Friend WithEvents lblSVRAvgPrice As System.Windows.Forms.Label
    Friend WithEvents cmbSVRRegion As System.Windows.Forms.ComboBox
    Friend WithEvents txtSVRThreshold As System.Windows.Forms.TextBox
    Friend WithEvents lblSVRThreshold As System.Windows.Forms.Label
    Friend WithEvents cmbSVRAvgPriceDuration As System.Windows.Forms.ComboBox
    Friend WithEvents chkAutoUpdateSVRBPTab As System.Windows.Forms.CheckBox
    Friend WithEvents gbProxySettings As System.Windows.Forms.GroupBox
    Friend WithEvents txtProxyAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblProxyAddress As System.Windows.Forms.Label
    Friend WithEvents txtProxyPort As System.Windows.Forms.TextBox
    Friend WithEvents lblProxyPort As System.Windows.Forms.Label
    Friend WithEvents chkLoadBPsbyChar As CheckBox
    Friend WithEvents chkSaveFacilitiesbyChar As CheckBox
    Friend WithEvents chkRefreshPublicStructureDataonStartup As CheckBox
    Friend WithEvents chkDisableTracking As CheckBox
    Friend WithEvents gbCharacterOptions As GroupBox
    Friend WithEvents chkUseActiveSkills As CheckBox
    Friend WithEvents chkAlphaAccount As CheckBox
    Friend WithEvents chkSupressESImsgs As CheckBox
    Friend WithEvents chkLoadMaxAlphaSkills As CheckBox
    Friend WithEvents chkShareFacilities As CheckBox
    Friend WithEvents chkAlwaysBuyRAMs As CheckBox
    Friend WithEvents chkAlwaysBuyFuelBlocks As CheckBox
    Friend WithEvents chkBuildWhenNotEnoughItemsonMarket As CheckBox
    Friend WithEvents gbPriceOptions As GroupBox
    Friend WithEvents chkManualPriceOverride As CheckBox
End Class
