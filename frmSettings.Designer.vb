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
        Me.gbEVECentral = New System.Windows.Forms.GroupBox()
        Me.txtEVECentralInterval = New System.Windows.Forms.TextBox()
        Me.chkEVECentralInterval = New System.Windows.Forms.CheckBox()
        Me.gbImplants = New System.Windows.Forms.GroupBox()
        Me.gbStartupOptions = New System.Windows.Forms.GroupBox()
        Me.chkRefreshFacilityDataonStartup = New System.Windows.Forms.CheckBox()
        Me.chkRefreshMarketDataonStartup = New System.Windows.Forms.CheckBox()
        Me.chkRefreshTeamDataonStartup = New System.Windows.Forms.CheckBox()
        Me.gbExportOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnExportSSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportCSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportDefault = New System.Windows.Forms.RadioButton()
        Me.gbCalcAvgPrice = New System.Windows.Forms.GroupBox()
        Me.chkAutoUpdateSVRBPTab = New System.Windows.Forms.CheckBox()
        Me.lblSVRRegion = New System.Windows.Forms.Label()
        Me.lblSVRAvgPrice = New System.Windows.Forms.Label()
        Me.cmbSVRRegion = New System.Windows.Forms.ComboBox()
        Me.txtSVRThreshold = New System.Windows.Forms.TextBox()
        Me.lblSVRThreshold = New System.Windows.Forms.Label()
        Me.cmbSVRAvgPriceDuration = New System.Windows.Forms.ComboBox()
        Me.gbSVRSource = New System.Windows.Forms.GroupBox()
        Me.rbtnSVRSourceEMD = New System.Windows.Forms.RadioButton()
        Me.rbtnSVRSourceCCP = New System.Windows.Forms.RadioButton()
        Me.gbGeneral.SuspendLayout()
        Me.gbStationStandings.SuspendLayout()
        Me.gbBuildBuySettings.SuspendLayout()
        Me.gbDefaultMEPE.SuspendLayout()
        Me.gbShoppingList.SuspendLayout()
        Me.gbEVECentral.SuspendLayout()
        Me.gbImplants.SuspendLayout()
        Me.gbStartupOptions.SuspendLayout()
        Me.gbExportOptions.SuspendLayout()
        Me.gbCalcAvgPrice.SuspendLayout()
        Me.gbSVRSource.SuspendLayout()
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
        Me.btnSave.Location = New System.Drawing.Point(143, 339)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(117, 30)
        Me.btnSave.TabIndex = 29
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(405, 339)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(117, 30)
        Me.btnCancel.TabIndex = 31
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gbGeneral
        '
        Me.gbGeneral.Controls.Add(Me.chkLinksInCopyText)
        Me.gbGeneral.Controls.Add(Me.chkDisableSound)
        Me.gbGeneral.Controls.Add(Me.chkDisableSVR)
        Me.gbGeneral.Controls.Add(Me.chkShowToolTips)
        Me.gbGeneral.Location = New System.Drawing.Point(5, 12)
        Me.gbGeneral.Name = "gbGeneral"
        Me.gbGeneral.Size = New System.Drawing.Size(237, 104)
        Me.gbGeneral.TabIndex = 4
        Me.gbGeneral.TabStop = False
        Me.gbGeneral.Text = "General:"
        '
        'chkLinksInCopyText
        '
        Me.chkLinksInCopyText.Location = New System.Drawing.Point(17, 38)
        Me.chkLinksInCopyText.Name = "chkLinksInCopyText"
        Me.chkLinksInCopyText.Size = New System.Drawing.Size(214, 17)
        Me.chkLinksInCopyText.TabIndex = 38
        Me.chkLinksInCopyText.Text = "Include InGame Links in Copy Text"
        Me.chkLinksInCopyText.UseVisualStyleBackColor = True
        '
        'chkDisableSound
        '
        Me.chkDisableSound.AutoSize = True
        Me.chkDisableSound.Location = New System.Drawing.Point(17, 78)
        Me.chkDisableSound.Name = "chkDisableSound"
        Me.chkDisableSound.Size = New System.Drawing.Size(95, 17)
        Me.chkDisableSound.TabIndex = 24
        Me.chkDisableSound.Text = "Disable Sound"
        Me.chkDisableSound.UseVisualStyleBackColor = True
        '
        'chkDisableSVR
        '
        Me.chkDisableSVR.AutoSize = True
        Me.chkDisableSVR.Location = New System.Drawing.Point(17, 58)
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
        Me.chkRefreshBPsonStartup.Location = New System.Drawing.Point(17, 60)
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
        Me.chkBeanCounterRefining.Size = New System.Drawing.Size(166, 17)
        Me.chkBeanCounterRefining.TabIndex = 5
        Me.chkBeanCounterRefining.Text = "Refining Beancounter Implant"
        Me.chkBeanCounterRefining.UseVisualStyleBackColor = True
        '
        'gbStationStandings
        '
        Me.gbStationStandings.Controls.Add(Me.txtBrokerCorpStanding)
        Me.gbStationStandings.Controls.Add(Me.chkBrokerCorpStanding)
        Me.gbStationStandings.Controls.Add(Me.txtBrokerFactionStanding)
        Me.gbStationStandings.Controls.Add(Me.chkBrokerFactionStanding)
        Me.gbStationStandings.Location = New System.Drawing.Point(246, 200)
        Me.gbStationStandings.Name = "gbStationStandings"
        Me.gbStationStandings.Size = New System.Drawing.Size(160, 64)
        Me.gbStationStandings.TabIndex = 7
        Me.gbStationStandings.TabStop = False
        Me.gbStationStandings.Text = "Station Standings:"
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
        Me.btnReset.Location = New System.Drawing.Point(274, 339)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(117, 30)
        Me.btnReset.TabIndex = 30
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'gbBuildBuySettings
        '
        Me.gbBuildBuySettings.Controls.Add(Me.chkSaveBPRelicsDecryptors)
        Me.gbBuildBuySettings.Controls.Add(Me.chkBuildBuyDefault)
        Me.gbBuildBuySettings.Controls.Add(Me.chkSuggestBuildwhenBPnotOwned)
        Me.gbBuildBuySettings.Location = New System.Drawing.Point(410, 171)
        Me.gbBuildBuySettings.Name = "gbBuildBuySettings"
        Me.gbBuildBuySettings.Size = New System.Drawing.Size(250, 73)
        Me.gbBuildBuySettings.TabIndex = 9
        Me.gbBuildBuySettings.TabStop = False
        Me.gbBuildBuySettings.Text = "Build Settings:"
        '
        'chkSaveBPRelicsDecryptors
        '
        Me.chkSaveBPRelicsDecryptors.AutoSize = True
        Me.chkSaveBPRelicsDecryptors.Location = New System.Drawing.Point(9, 51)
        Me.chkSaveBPRelicsDecryptors.Name = "chkSaveBPRelicsDecryptors"
        Me.chkSaveBPRelicsDecryptors.Size = New System.Drawing.Size(212, 17)
        Me.chkSaveBPRelicsDecryptors.TabIndex = 38
        Me.chkSaveBPRelicsDecryptors.Text = "Save Relics and Decryptors on BP Tab"
        Me.chkSaveBPRelicsDecryptors.UseVisualStyleBackColor = True
        '
        'chkBuildBuyDefault
        '
        Me.chkBuildBuyDefault.AutoSize = True
        Me.chkBuildBuyDefault.Location = New System.Drawing.Point(9, 17)
        Me.chkBuildBuyDefault.Name = "chkBuildBuyDefault"
        Me.chkBuildBuyDefault.Size = New System.Drawing.Size(109, 17)
        Me.chkBuildBuyDefault.TabIndex = 32
        Me.chkBuildBuyDefault.Text = "Default Build/Buy"
        Me.chkBuildBuyDefault.UseVisualStyleBackColor = True
        '
        'chkSuggestBuildwhenBPnotOwned
        '
        Me.chkSuggestBuildwhenBPnotOwned.AutoSize = True
        Me.chkSuggestBuildwhenBPnotOwned.Location = New System.Drawing.Point(9, 34)
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
        Me.gbDefaultMEPE.Location = New System.Drawing.Point(246, 12)
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
        Me.gbShoppingList.Location = New System.Drawing.Point(246, 77)
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
        'gbEVECentral
        '
        Me.gbEVECentral.Controls.Add(Me.txtEVECentralInterval)
        Me.gbEVECentral.Controls.Add(Me.chkEVECentralInterval)
        Me.gbEVECentral.Location = New System.Drawing.Point(246, 142)
        Me.gbEVECentral.Name = "gbEVECentral"
        Me.gbEVECentral.Size = New System.Drawing.Size(160, 56)
        Me.gbEVECentral.TabIndex = 38
        Me.gbEVECentral.TabStop = False
        Me.gbEVECentral.Text = "EVE Central Price Updates:"
        '
        'txtEVECentralInterval
        '
        Me.txtEVECentralInterval.Location = New System.Drawing.Point(110, 25)
        Me.txtEVECentralInterval.Name = "txtEVECentralInterval"
        Me.txtEVECentralInterval.Size = New System.Drawing.Size(41, 20)
        Me.txtEVECentralInterval.TabIndex = 24
        Me.txtEVECentralInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkEVECentralInterval
        '
        Me.chkEVECentralInterval.Location = New System.Drawing.Point(9, 18)
        Me.chkEVECentralInterval.Name = "chkEVECentralInterval"
        Me.chkEVECentralInterval.Size = New System.Drawing.Size(105, 34)
        Me.chkEVECentralInterval.TabIndex = 23
        Me.chkEVECentralInterval.Text = "Refresh Interval (Hours):"
        Me.chkEVECentralInterval.UseVisualStyleBackColor = True
        '
        'gbImplants
        '
        Me.gbImplants.Controls.Add(Me.chkBeanCounterManufacturing)
        Me.gbImplants.Controls.Add(Me.chkBeanCounterCopy)
        Me.gbImplants.Controls.Add(Me.cmbBeanCounterManufacturing)
        Me.gbImplants.Controls.Add(Me.chkBeanCounterRefining)
        Me.gbImplants.Controls.Add(Me.cmbBeanCounterCopy)
        Me.gbImplants.Controls.Add(Me.cmbBeanCounterRefining)
        Me.gbImplants.Location = New System.Drawing.Point(410, 12)
        Me.gbImplants.Name = "gbImplants"
        Me.gbImplants.Size = New System.Drawing.Size(250, 157)
        Me.gbImplants.TabIndex = 36
        Me.gbImplants.TabStop = False
        Me.gbImplants.Text = "Implants:"
        '
        'gbStartupOptions
        '
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshFacilityDataonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshMarketDataonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshTeamDataonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshBPsonStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkCheckUpdatesStartup)
        Me.gbStartupOptions.Controls.Add(Me.chkRefreshAssetsonStartup)
        Me.gbStartupOptions.Location = New System.Drawing.Point(5, 121)
        Me.gbStartupOptions.Name = "gbStartupOptions"
        Me.gbStartupOptions.Size = New System.Drawing.Size(237, 144)
        Me.gbStartupOptions.TabIndex = 39
        Me.gbStartupOptions.TabStop = False
        Me.gbStartupOptions.Text = "Startup Options"
        '
        'chkRefreshFacilityDataonStartup
        '
        Me.chkRefreshFacilityDataonStartup.AutoSize = True
        Me.chkRefreshFacilityDataonStartup.Location = New System.Drawing.Point(17, 123)
        Me.chkRefreshFacilityDataonStartup.Name = "chkRefreshFacilityDataonStartup"
        Me.chkRefreshFacilityDataonStartup.Size = New System.Drawing.Size(124, 17)
        Me.chkRefreshFacilityDataonStartup.TabIndex = 29
        Me.chkRefreshFacilityDataonStartup.Text = "Refresh Facility Data"
        Me.chkRefreshFacilityDataonStartup.UseVisualStyleBackColor = True
        '
        'chkRefreshMarketDataonStartup
        '
        Me.chkRefreshMarketDataonStartup.AutoSize = True
        Me.chkRefreshMarketDataonStartup.Location = New System.Drawing.Point(17, 102)
        Me.chkRefreshMarketDataonStartup.Name = "chkRefreshMarketDataonStartup"
        Me.chkRefreshMarketDataonStartup.Size = New System.Drawing.Size(125, 17)
        Me.chkRefreshMarketDataonStartup.TabIndex = 28
        Me.chkRefreshMarketDataonStartup.Text = "Refresh Market Data"
        Me.chkRefreshMarketDataonStartup.UseVisualStyleBackColor = True
        '
        'chkRefreshTeamDataonStartup
        '
        Me.chkRefreshTeamDataonStartup.AutoSize = True
        Me.chkRefreshTeamDataonStartup.Enabled = False
        Me.chkRefreshTeamDataonStartup.Location = New System.Drawing.Point(17, 81)
        Me.chkRefreshTeamDataonStartup.Name = "chkRefreshTeamDataonStartup"
        Me.chkRefreshTeamDataonStartup.Size = New System.Drawing.Size(119, 17)
        Me.chkRefreshTeamDataonStartup.TabIndex = 27
        Me.chkRefreshTeamDataonStartup.Text = "Refresh Team Data"
        Me.chkRefreshTeamDataonStartup.UseVisualStyleBackColor = True
        '
        'gbExportOptions
        '
        Me.gbExportOptions.Controls.Add(Me.rbtnExportSSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportCSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportDefault)
        Me.gbExportOptions.Location = New System.Drawing.Point(5, 270)
        Me.gbExportOptions.Name = "gbExportOptions"
        Me.gbExportOptions.Size = New System.Drawing.Size(237, 45)
        Me.gbExportOptions.TabIndex = 38
        Me.gbExportOptions.TabStop = False
        Me.gbExportOptions.Text = "Export Data in:"
        '
        'rbtnExportSSV
        '
        Me.rbtnExportSSV.AutoSize = True
        Me.rbtnExportSSV.Location = New System.Drawing.Point(172, 19)
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
        Me.rbtnExportCSV.Location = New System.Drawing.Point(101, 19)
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
        Me.rbtnExportDefault.Location = New System.Drawing.Point(17, 19)
        Me.rbtnExportDefault.Name = "rbtnExportDefault"
        Me.rbtnExportDefault.Size = New System.Drawing.Size(59, 17)
        Me.rbtnExportDefault.TabIndex = 0
        Me.rbtnExportDefault.TabStop = True
        Me.rbtnExportDefault.Text = "Default"
        Me.rbtnExportDefault.UseVisualStyleBackColor = True
        '
        'gbCalcAvgPrice
        '
        Me.gbCalcAvgPrice.Controls.Add(Me.chkAutoUpdateSVRBPTab)
        Me.gbCalcAvgPrice.Controls.Add(Me.lblSVRRegion)
        Me.gbCalcAvgPrice.Controls.Add(Me.lblSVRAvgPrice)
        Me.gbCalcAvgPrice.Controls.Add(Me.cmbSVRRegion)
        Me.gbCalcAvgPrice.Controls.Add(Me.txtSVRThreshold)
        Me.gbCalcAvgPrice.Controls.Add(Me.lblSVRThreshold)
        Me.gbCalcAvgPrice.Controls.Add(Me.cmbSVRAvgPriceDuration)
        Me.gbCalcAvgPrice.Location = New System.Drawing.Point(410, 246)
        Me.gbCalcAvgPrice.Name = "gbCalcAvgPrice"
        Me.gbCalcAvgPrice.Size = New System.Drawing.Size(250, 87)
        Me.gbCalcAvgPrice.TabIndex = 40
        Me.gbCalcAvgPrice.TabStop = False
        Me.gbCalcAvgPrice.Text = "SVR Settings:"
        '
        'chkAutoUpdateSVRBPTab
        '
        Me.chkAutoUpdateSVRBPTab.AutoSize = True
        Me.chkAutoUpdateSVRBPTab.Location = New System.Drawing.Point(9, 64)
        Me.chkAutoUpdateSVRBPTab.Name = "chkAutoUpdateSVRBPTab"
        Me.chkAutoUpdateSVRBPTab.Size = New System.Drawing.Size(203, 17)
        Me.chkAutoUpdateSVRBPTab.TabIndex = 39
        Me.chkAutoUpdateSVRBPTab.Text = "Automatically update SVR on BP Tab"
        Me.chkAutoUpdateSVRBPTab.UseVisualStyleBackColor = True
        '
        'lblSVRRegion
        '
        Me.lblSVRRegion.AutoSize = True
        Me.lblSVRRegion.Location = New System.Drawing.Point(19, 42)
        Me.lblSVRRegion.Name = "lblSVRRegion"
        Me.lblSVRRegion.Size = New System.Drawing.Size(44, 13)
        Me.lblSVRRegion.TabIndex = 4
        Me.lblSVRRegion.Text = "Region:"
        '
        'lblSVRAvgPrice
        '
        Me.lblSVRAvgPrice.Location = New System.Drawing.Point(114, 10)
        Me.lblSVRAvgPrice.Name = "lblSVRAvgPrice"
        Me.lblSVRAvgPrice.Size = New System.Drawing.Size(78, 28)
        Me.lblSVRAvgPrice.TabIndex = 2
        Me.lblSVRAvgPrice.Text = "Average Days:"
        Me.lblSVRAvgPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSVRRegion
        '
        Me.cmbSVRRegion.FormattingEnabled = True
        Me.cmbSVRRegion.Location = New System.Drawing.Point(63, 39)
        Me.cmbSVRRegion.Name = "cmbSVRRegion"
        Me.cmbSVRRegion.Size = New System.Drawing.Size(176, 21)
        Me.cmbSVRRegion.TabIndex = 5
        '
        'txtSVRThreshold
        '
        Me.txtSVRThreshold.Location = New System.Drawing.Point(63, 15)
        Me.txtSVRThreshold.MaxLength = 10
        Me.txtSVRThreshold.Name = "txtSVRThreshold"
        Me.txtSVRThreshold.Size = New System.Drawing.Size(45, 20)
        Me.txtSVRThreshold.TabIndex = 1
        Me.txtSVRThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSVRThreshold
        '
        Me.lblSVRThreshold.AutoSize = True
        Me.lblSVRThreshold.Location = New System.Drawing.Point(6, 18)
        Me.lblSVRThreshold.Name = "lblSVRThreshold"
        Me.lblSVRThreshold.Size = New System.Drawing.Size(57, 13)
        Me.lblSVRThreshold.TabIndex = 0
        Me.lblSVRThreshold.Text = "Threshold:"
        '
        'cmbSVRAvgPriceDuration
        '
        Me.cmbSVRAvgPriceDuration.FormattingEnabled = True
        Me.cmbSVRAvgPriceDuration.Items.AddRange(New Object() {"7", "15", "30", "60", "90", "180", "365"})
        Me.cmbSVRAvgPriceDuration.Location = New System.Drawing.Point(198, 14)
        Me.cmbSVRAvgPriceDuration.MaxLength = 3
        Me.cmbSVRAvgPriceDuration.Name = "cmbSVRAvgPriceDuration"
        Me.cmbSVRAvgPriceDuration.Size = New System.Drawing.Size(41, 21)
        Me.cmbSVRAvgPriceDuration.TabIndex = 3
        '
        'gbSVRSource
        '
        Me.gbSVRSource.Controls.Add(Me.rbtnSVRSourceEMD)
        Me.gbSVRSource.Controls.Add(Me.rbtnSVRSourceCCP)
        Me.gbSVRSource.Location = New System.Drawing.Point(246, 270)
        Me.gbSVRSource.Name = "gbSVRSource"
        Me.gbSVRSource.Size = New System.Drawing.Size(160, 45)
        Me.gbSVRSource.TabIndex = 39
        Me.gbSVRSource.TabStop = False
        Me.gbSVRSource.Text = "SVR Source:"
        '
        'rbtnSVRSourceEMD
        '
        Me.rbtnSVRSourceEMD.AutoSize = True
        Me.rbtnSVRSourceEMD.Location = New System.Drawing.Point(88, 19)
        Me.rbtnSVRSourceEMD.Name = "rbtnSVRSourceEMD"
        Me.rbtnSVRSourceEMD.Size = New System.Drawing.Size(49, 17)
        Me.rbtnSVRSourceEMD.TabIndex = 1
        Me.rbtnSVRSourceEMD.TabStop = True
        Me.rbtnSVRSourceEMD.Text = "EMD"
        Me.rbtnSVRSourceEMD.UseVisualStyleBackColor = True
        '
        'rbtnSVRSourceCCP
        '
        Me.rbtnSVRSourceCCP.AutoSize = True
        Me.rbtnSVRSourceCCP.Location = New System.Drawing.Point(23, 19)
        Me.rbtnSVRSourceCCP.Name = "rbtnSVRSourceCCP"
        Me.rbtnSVRSourceCCP.Size = New System.Drawing.Size(46, 17)
        Me.rbtnSVRSourceCCP.TabIndex = 0
        Me.rbtnSVRSourceCCP.TabStop = True
        Me.rbtnSVRSourceCCP.Text = "CCP"
        Me.rbtnSVRSourceCCP.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(665, 381)
        Me.Controls.Add(Me.gbSVRSource)
        Me.Controls.Add(Me.gbCalcAvgPrice)
        Me.Controls.Add(Me.gbExportOptions)
        Me.Controls.Add(Me.gbStartupOptions)
        Me.Controls.Add(Me.gbImplants)
        Me.Controls.Add(Me.gbEVECentral)
        Me.Controls.Add(Me.gbBuildBuySettings)
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
        Me.gbEVECentral.ResumeLayout(False)
        Me.gbEVECentral.PerformLayout()
        Me.gbImplants.ResumeLayout(False)
        Me.gbImplants.PerformLayout()
        Me.gbStartupOptions.ResumeLayout(False)
        Me.gbStartupOptions.PerformLayout()
        Me.gbExportOptions.ResumeLayout(False)
        Me.gbExportOptions.PerformLayout()
        Me.gbCalcAvgPrice.ResumeLayout(False)
        Me.gbCalcAvgPrice.PerformLayout()
        Me.gbSVRSource.ResumeLayout(False)
        Me.gbSVRSource.PerformLayout()
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
    Friend WithEvents gbEVECentral As System.Windows.Forms.GroupBox
    Friend WithEvents txtEVECentralInterval As System.Windows.Forms.TextBox
    Friend WithEvents chkEVECentralInterval As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshAssetsonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents gbImplants As System.Windows.Forms.GroupBox
    Friend WithEvents chkDisableSound As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshBPsonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents gbStartupOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkRefreshTeamDataonStartup As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshFacilityDataonStartup As System.Windows.Forms.CheckBox
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
    Friend WithEvents gbSVRSource As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnSVRSourceEMD As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSVRSourceCCP As System.Windows.Forms.RadioButton
    Friend WithEvents chkAutoUpdateSVRBPTab As System.Windows.Forms.CheckBox
End Class
