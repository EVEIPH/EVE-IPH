<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIceBeltFlip
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIceBeltFlip))
        Me.gbSummarySettings = New System.Windows.Forms.GroupBox()
        Me.gbTrueSec = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.rbtn10percent = New System.Windows.Forms.RadioButton()
        Me.rbtn5percent = New System.Windows.Forms.RadioButton()
        Me.rbtnHighsec = New System.Windows.Forms.RadioButton()
        Me.gbSpace = New System.Windows.Forms.GroupBox()
        Me.rbtnMinmatar = New System.Windows.Forms.RadioButton()
        Me.rbtnGallente = New System.Windows.Forms.RadioButton()
        Me.rbtnCaldari = New System.Windows.Forms.RadioButton()
        Me.rbtnAmarr = New System.Windows.Forms.RadioButton()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.gbMineTaxBroker = New System.Windows.Forms.GroupBox()
        Me.txtBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.chkIPHperMiner = New System.Windows.Forms.CheckBox()
        Me.chkCompressOre = New System.Windows.Forms.CheckBox()
        Me.chkIncludeTaxes = New System.Windows.Forms.CheckBox()
        Me.chkBrokerFees = New System.Windows.Forms.CheckBox()
        Me.lblCycleTime = New System.Windows.Forms.Label()
        Me.txtCycleTime = New System.Windows.Forms.TextBox()
        Me.txtm3perCycle = New System.Windows.Forms.TextBox()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.lblm3perCycle = New System.Windows.Forms.Label()
        Me.cmbNumMiners = New System.Windows.Forms.ComboBox()
        Me.lblNumMiners = New System.Windows.Forms.Label()
        Me.lblm3perhrperminer = New System.Windows.Forms.Label()
        Me.lblm3perhrperminer1 = New System.Windows.Forms.Label()
        Me.btnCloseSmall = New System.Windows.Forms.Button()
        Me.btnSaveSettingsSmall = New System.Windows.Forms.Button()
        Me.gbSum1 = New System.Windows.Forms.GroupBox()
        Me.lblTotalIPH1 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVolume1 = New System.Windows.Forms.Label()
        Me.lblBeltTotalIsk1 = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip1 = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel1 = New System.Windows.Forms.Label()
        Me.lblIPH1 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVol1 = New System.Windows.Forms.Label()
        Me.lblHourstoFlip1 = New System.Windows.Forms.Label()
        Me.lblSmallBeltMineralComp = New System.Windows.Forms.Label()
        Me.lstOresLevel1 = New System.Windows.Forms.ListView()
        Me.checkboxSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.orenameSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.numberroidsSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.unitsSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstMineralsLevel1 = New System.Windows.Forms.ListView()
        Me.mineralsSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.minsunitsSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.totaliskSmall = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblSmallBeltOreComp = New System.Windows.Forms.Label()
        Me.RefineStation = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.gbSummarySettings.SuspendLayout()
        Me.gbTrueSec.SuspendLayout()
        Me.gbSpace.SuspendLayout()
        Me.gbMineTaxBroker.SuspendLayout()
        Me.gbSum1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbSummarySettings
        '
        Me.gbSummarySettings.Controls.Add(Me.RefineStation)
        Me.gbSummarySettings.Controls.Add(Me.gbTrueSec)
        Me.gbSummarySettings.Controls.Add(Me.gbSpace)
        Me.gbSummarySettings.Controls.Add(Me.btnRefresh)
        Me.gbSummarySettings.Controls.Add(Me.gbMineTaxBroker)
        Me.gbSummarySettings.Controls.Add(Me.lblCycleTime)
        Me.gbSummarySettings.Controls.Add(Me.txtCycleTime)
        Me.gbSummarySettings.Controls.Add(Me.txtm3perCycle)
        Me.gbSummarySettings.Controls.Add(Me.btnSaveSettings)
        Me.gbSummarySettings.Controls.Add(Me.lblm3perCycle)
        Me.gbSummarySettings.Controls.Add(Me.cmbNumMiners)
        Me.gbSummarySettings.Controls.Add(Me.lblNumMiners)
        Me.gbSummarySettings.Controls.Add(Me.lblm3perhrperminer)
        Me.gbSummarySettings.Controls.Add(Me.lblm3perhrperminer1)
        Me.gbSummarySettings.Location = New System.Drawing.Point(12, 12)
        Me.gbSummarySettings.Name = "gbSummarySettings"
        Me.gbSummarySettings.Size = New System.Drawing.Size(618, 240)
        Me.gbSummarySettings.TabIndex = 140
        Me.gbSummarySettings.TabStop = False
        Me.gbSummarySettings.Text = "Settings"
        '
        'gbTrueSec
        '
        Me.gbTrueSec.Controls.Add(Me.RadioButton1)
        Me.gbTrueSec.Controls.Add(Me.rbtn10percent)
        Me.gbTrueSec.Controls.Add(Me.rbtn5percent)
        Me.gbTrueSec.Controls.Add(Me.rbtnHighsec)
        Me.gbTrueSec.Location = New System.Drawing.Point(6, 108)
        Me.gbTrueSec.Name = "gbTrueSec"
        Me.gbTrueSec.Size = New System.Drawing.Size(108, 83)
        Me.gbTrueSec.TabIndex = 123
        Me.gbTrueSec.TabStop = False
        Me.gbTrueSec.Text = "System Security:"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(6, 61)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(94, 17)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.Text = "Nullsec Strong"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'rbtn10percent
        '
        Me.rbtn10percent.AutoSize = True
        Me.rbtn10percent.Location = New System.Drawing.Point(6, 45)
        Me.rbtn10percent.Name = "rbtn10percent"
        Me.rbtn10percent.Size = New System.Drawing.Size(92, 17)
        Me.rbtn10percent.TabIndex = 5
        Me.rbtn10percent.Text = "Nullsec Weak"
        Me.rbtn10percent.UseVisualStyleBackColor = True
        '
        'rbtn5percent
        '
        Me.rbtn5percent.AutoSize = True
        Me.rbtn5percent.Location = New System.Drawing.Point(6, 30)
        Me.rbtn5percent.Name = "rbtn5percent"
        Me.rbtn5percent.Size = New System.Drawing.Size(62, 17)
        Me.rbtn5percent.TabIndex = 4
        Me.rbtn5percent.Text = "Lowsec"
        Me.rbtn5percent.UseVisualStyleBackColor = True
        '
        'rbtnHighsec
        '
        Me.rbtnHighsec.AutoSize = True
        Me.rbtnHighsec.Checked = True
        Me.rbtnHighsec.Location = New System.Drawing.Point(6, 15)
        Me.rbtnHighsec.Name = "rbtnHighsec"
        Me.rbtnHighsec.Size = New System.Drawing.Size(64, 17)
        Me.rbtnHighsec.TabIndex = 3
        Me.rbtnHighsec.TabStop = True
        Me.rbtnHighsec.Text = "Highsec"
        Me.rbtnHighsec.UseVisualStyleBackColor = True
        '
        'gbSpace
        '
        Me.gbSpace.Controls.Add(Me.rbtnMinmatar)
        Me.gbSpace.Controls.Add(Me.rbtnGallente)
        Me.gbSpace.Controls.Add(Me.rbtnCaldari)
        Me.gbSpace.Controls.Add(Me.rbtnAmarr)
        Me.gbSpace.Location = New System.Drawing.Point(6, 19)
        Me.gbSpace.Name = "gbSpace"
        Me.gbSpace.Size = New System.Drawing.Size(108, 83)
        Me.gbSpace.TabIndex = 124
        Me.gbSpace.TabStop = False
        Me.gbSpace.Text = "Space:"
        '
        'rbtnMinmatar
        '
        Me.rbtnMinmatar.AutoSize = True
        Me.rbtnMinmatar.Location = New System.Drawing.Point(6, 61)
        Me.rbtnMinmatar.Name = "rbtnMinmatar"
        Me.rbtnMinmatar.Size = New System.Drawing.Size(68, 17)
        Me.rbtnMinmatar.TabIndex = 6
        Me.rbtnMinmatar.Text = "Minmatar"
        Me.rbtnMinmatar.UseVisualStyleBackColor = True
        '
        'rbtnGallente
        '
        Me.rbtnGallente.AutoSize = True
        Me.rbtnGallente.Location = New System.Drawing.Point(6, 45)
        Me.rbtnGallente.Name = "rbtnGallente"
        Me.rbtnGallente.Size = New System.Drawing.Size(64, 17)
        Me.rbtnGallente.TabIndex = 5
        Me.rbtnGallente.Text = "Gallente"
        Me.rbtnGallente.UseVisualStyleBackColor = True
        '
        'rbtnCaldari
        '
        Me.rbtnCaldari.AutoSize = True
        Me.rbtnCaldari.Location = New System.Drawing.Point(6, 30)
        Me.rbtnCaldari.Name = "rbtnCaldari"
        Me.rbtnCaldari.Size = New System.Drawing.Size(57, 17)
        Me.rbtnCaldari.TabIndex = 4
        Me.rbtnCaldari.Text = "Caldari"
        Me.rbtnCaldari.UseVisualStyleBackColor = True
        '
        'rbtnAmarr
        '
        Me.rbtnAmarr.AutoSize = True
        Me.rbtnAmarr.Checked = True
        Me.rbtnAmarr.Location = New System.Drawing.Point(6, 15)
        Me.rbtnAmarr.Name = "rbtnAmarr"
        Me.rbtnAmarr.Size = New System.Drawing.Size(52, 17)
        Me.rbtnAmarr.TabIndex = 3
        Me.rbtnAmarr.TabStop = True
        Me.rbtnAmarr.Text = "Amarr"
        Me.rbtnAmarr.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(144, 50)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(81, 28)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'gbMineTaxBroker
        '
        Me.gbMineTaxBroker.Controls.Add(Me.txtBrokerFeeRate)
        Me.gbMineTaxBroker.Controls.Add(Me.chkIPHperMiner)
        Me.gbMineTaxBroker.Controls.Add(Me.chkCompressOre)
        Me.gbMineTaxBroker.Controls.Add(Me.chkIncludeTaxes)
        Me.gbMineTaxBroker.Controls.Add(Me.chkBrokerFees)
        Me.gbMineTaxBroker.Location = New System.Drawing.Point(144, 123)
        Me.gbMineTaxBroker.Name = "gbMineTaxBroker"
        Me.gbMineTaxBroker.Size = New System.Drawing.Size(127, 98)
        Me.gbMineTaxBroker.TabIndex = 16
        Me.gbMineTaxBroker.TabStop = False
        Me.gbMineTaxBroker.Text = "Options:"
        '
        'txtBrokerFeeRate
        '
        Me.txtBrokerFeeRate.Location = New System.Drawing.Point(64, 55)
        Me.txtBrokerFeeRate.Name = "txtBrokerFeeRate"
        Me.txtBrokerFeeRate.Size = New System.Drawing.Size(48, 20)
        Me.txtBrokerFeeRate.TabIndex = 41
        Me.txtBrokerFeeRate.TabStop = False
        Me.txtBrokerFeeRate.Visible = False
        '
        'chkIPHperMiner
        '
        Me.chkIPHperMiner.AutoSize = True
        Me.chkIPHperMiner.Checked = True
        Me.chkIPHperMiner.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIPHperMiner.Location = New System.Drawing.Point(9, 38)
        Me.chkIPHperMiner.Name = "chkIPHperMiner"
        Me.chkIPHperMiner.Size = New System.Drawing.Size(91, 17)
        Me.chkIPHperMiner.TabIndex = 9
        Me.chkIPHperMiner.Text = "IPH per Miner"
        Me.chkIPHperMiner.UseVisualStyleBackColor = True
        '
        'chkCompressOre
        '
        Me.chkCompressOre.AutoSize = True
        Me.chkCompressOre.Checked = True
        Me.chkCompressOre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressOre.Location = New System.Drawing.Point(9, 19)
        Me.chkCompressOre.Name = "chkCompressOre"
        Me.chkCompressOre.Size = New System.Drawing.Size(90, 17)
        Me.chkCompressOre.TabIndex = 8
        Me.chkCompressOre.Text = "Compress Ice"
        Me.chkCompressOre.UseVisualStyleBackColor = True
        '
        'chkIncludeTaxes
        '
        Me.chkIncludeTaxes.AutoSize = True
        Me.chkIncludeTaxes.Checked = True
        Me.chkIncludeTaxes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeTaxes.Location = New System.Drawing.Point(9, 76)
        Me.chkIncludeTaxes.Name = "chkIncludeTaxes"
        Me.chkIncludeTaxes.Size = New System.Drawing.Size(55, 17)
        Me.chkIncludeTaxes.TabIndex = 11
        Me.chkIncludeTaxes.Text = "Taxes"
        Me.chkIncludeTaxes.UseVisualStyleBackColor = True
        '
        'chkBrokerFees
        '
        Me.chkBrokerFees.AutoSize = True
        Me.chkBrokerFees.Checked = True
        Me.chkBrokerFees.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBrokerFees.Location = New System.Drawing.Point(9, 57)
        Me.chkBrokerFees.Name = "chkBrokerFees"
        Me.chkBrokerFees.Size = New System.Drawing.Size(49, 17)
        Me.chkBrokerFees.TabIndex = 10
        Me.chkBrokerFees.Text = "Fees"
        Me.chkBrokerFees.ThreeState = True
        Me.chkBrokerFees.UseVisualStyleBackColor = True
        '
        'lblCycleTime
        '
        Me.lblCycleTime.AutoSize = True
        Me.lblCycleTime.Location = New System.Drawing.Point(228, 9)
        Me.lblCycleTime.Name = "lblCycleTime"
        Me.lblCycleTime.Size = New System.Drawing.Size(62, 13)
        Me.lblCycleTime.TabIndex = 111
        Me.lblCycleTime.Text = "Cycle Time:"
        '
        'txtCycleTime
        '
        Me.txtCycleTime.Location = New System.Drawing.Point(231, 24)
        Me.txtCycleTime.Name = "txtCycleTime"
        Me.txtCycleTime.Size = New System.Drawing.Size(74, 20)
        Me.txtCycleTime.TabIndex = 1
        Me.txtCycleTime.Text = "104.67"
        Me.txtCycleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtm3perCycle
        '
        Me.txtm3perCycle.Location = New System.Drawing.Point(310, 24)
        Me.txtm3perCycle.Name = "txtm3perCycle"
        Me.txtm3perCycle.Size = New System.Drawing.Size(76, 20)
        Me.txtm3perCycle.TabIndex = 2
        Me.txtm3perCycle.Text = "5833.11"
        Me.txtm3perCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(144, 78)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(81, 28)
        Me.btnSaveSettings.TabIndex = 4
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'lblm3perCycle
        '
        Me.lblm3perCycle.AutoSize = True
        Me.lblm3perCycle.Location = New System.Drawing.Point(308, 9)
        Me.lblm3perCycle.Name = "lblm3perCycle"
        Me.lblm3perCycle.Size = New System.Drawing.Size(71, 13)
        Me.lblm3perCycle.TabIndex = 113
        Me.lblm3perCycle.Text = "m3 per Cycle:"
        '
        'cmbNumMiners
        '
        Me.cmbNumMiners.FormattingEnabled = True
        Me.cmbNumMiners.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbNumMiners.Location = New System.Drawing.Point(144, 24)
        Me.cmbNumMiners.Name = "cmbNumMiners"
        Me.cmbNumMiners.Size = New System.Drawing.Size(81, 21)
        Me.cmbNumMiners.TabIndex = 0
        Me.cmbNumMiners.Text = "10"
        '
        'lblNumMiners
        '
        Me.lblNumMiners.AutoSize = True
        Me.lblNumMiners.Location = New System.Drawing.Point(144, 9)
        Me.lblNumMiners.Name = "lblNumMiners"
        Me.lblNumMiners.Size = New System.Drawing.Size(66, 13)
        Me.lblNumMiners.TabIndex = 115
        Me.lblNumMiners.Text = "Num Miners:"
        '
        'lblm3perhrperminer
        '
        Me.lblm3perhrperminer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblm3perhrperminer.Location = New System.Drawing.Point(390, 25)
        Me.lblm3perhrperminer.Name = "lblm3perhrperminer"
        Me.lblm3perhrperminer.Size = New System.Drawing.Size(90, 18)
        Me.lblm3perhrperminer.TabIndex = 122
        Me.lblm3perhrperminer.Text = "999,999.00"
        Me.lblm3perhrperminer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblm3perhrperminer1
        '
        Me.lblm3perhrperminer1.AutoSize = True
        Me.lblm3perhrperminer1.Location = New System.Drawing.Point(390, 9)
        Me.lblm3perhrperminer1.Name = "lblm3perhrperminer1"
        Me.lblm3perhrperminer1.Size = New System.Drawing.Size(68, 13)
        Me.lblm3perhrperminer1.TabIndex = 121
        Me.lblm3perhrperminer1.Text = "m3/hr/miner:"
        '
        'btnCloseSmall
        '
        Me.btnCloseSmall.Location = New System.Drawing.Point(380, 481)
        Me.btnCloseSmall.Name = "btnCloseSmall"
        Me.btnCloseSmall.Size = New System.Drawing.Size(113, 28)
        Me.btnCloseSmall.TabIndex = 145
        Me.btnCloseSmall.Text = "Close"
        Me.btnCloseSmall.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsSmall
        '
        Me.btnSaveSettingsSmall.Location = New System.Drawing.Point(254, 481)
        Me.btnSaveSettingsSmall.Name = "btnSaveSettingsSmall"
        Me.btnSaveSettingsSmall.Size = New System.Drawing.Size(113, 28)
        Me.btnSaveSettingsSmall.TabIndex = 144
        Me.btnSaveSettingsSmall.Text = "Save Selected Ores"
        Me.btnSaveSettingsSmall.UseVisualStyleBackColor = True
        '
        'gbSum1
        '
        Me.gbSum1.Controls.Add(Me.lblTotalIPH1)
        Me.gbSum1.Controls.Add(Me.lblTotalBeltVolume1)
        Me.gbSum1.Controls.Add(Me.lblBeltTotalIsk1)
        Me.gbSum1.Controls.Add(Me.lblTotalHourstoFlip1)
        Me.gbSum1.Controls.Add(Me.lblTotalIskLevel1)
        Me.gbSum1.Controls.Add(Me.lblIPH1)
        Me.gbSum1.Controls.Add(Me.lblTotalBeltVol1)
        Me.gbSum1.Controls.Add(Me.lblHourstoFlip1)
        Me.gbSum1.Location = New System.Drawing.Point(253, 358)
        Me.gbSum1.Name = "gbSum1"
        Me.gbSum1.Size = New System.Drawing.Size(243, 117)
        Me.gbSum1.TabIndex = 147
        Me.gbSum1.TabStop = False
        '
        'lblTotalIPH1
        '
        Me.lblTotalIPH1.AutoSize = True
        Me.lblTotalIPH1.Location = New System.Drawing.Point(18, 65)
        Me.lblTotalIPH1.Name = "lblTotalIPH1"
        Me.lblTotalIPH1.Size = New System.Drawing.Size(68, 13)
        Me.lblTotalIPH1.TabIndex = 61
        Me.lblTotalIPH1.Text = "Isk per Hour:"
        Me.lblTotalIPH1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBeltVolume1
        '
        Me.lblTotalBeltVolume1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume1.Location = New System.Drawing.Point(97, 84)
        Me.lblTotalBeltVolume1.Name = "lblTotalBeltVolume1"
        Me.lblTotalBeltVolume1.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume1.TabIndex = 60
        Me.lblTotalBeltVolume1.Text = "100,000.00"
        Me.lblTotalBeltVolume1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIsk1
        '
        Me.lblBeltTotalIsk1.AutoSize = True
        Me.lblBeltTotalIsk1.Location = New System.Drawing.Point(18, 21)
        Me.lblBeltTotalIsk1.Name = "lblBeltTotalIsk1"
        Me.lblBeltTotalIsk1.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIsk1.TabIndex = 52
        Me.lblBeltTotalIsk1.Text = "Belt Total Isk:"
        Me.lblBeltTotalIsk1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip1
        '
        Me.lblTotalHourstoFlip1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip1.Location = New System.Drawing.Point(97, 40)
        Me.lblTotalHourstoFlip1.Name = "lblTotalHourstoFlip1"
        Me.lblTotalHourstoFlip1.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip1.TabIndex = 57
        Me.lblTotalHourstoFlip1.Text = "100,000.00"
        Me.lblTotalHourstoFlip1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIskLevel1
        '
        Me.lblTotalIskLevel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel1.Location = New System.Drawing.Point(97, 18)
        Me.lblTotalIskLevel1.Name = "lblTotalIskLevel1"
        Me.lblTotalIskLevel1.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel1.TabIndex = 53
        Me.lblTotalIskLevel1.Text = "100,000.00"
        Me.lblTotalIskLevel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH1
        '
        Me.lblIPH1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIPH1.Location = New System.Drawing.Point(97, 62)
        Me.lblIPH1.Name = "lblIPH1"
        Me.lblIPH1.Size = New System.Drawing.Size(127, 18)
        Me.lblIPH1.TabIndex = 59
        Me.lblIPH1.Text = "100,000.00"
        Me.lblIPH1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltVol1
        '
        Me.lblTotalBeltVol1.AutoSize = True
        Me.lblTotalBeltVol1.Location = New System.Drawing.Point(18, 87)
        Me.lblTotalBeltVol1.Name = "lblTotalBeltVol1"
        Me.lblTotalBeltVol1.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalBeltVol1.TabIndex = 54
        Me.lblTotalBeltVol1.Text = "Belt Total Vol:"
        Me.lblTotalBeltVol1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip1
        '
        Me.lblHourstoFlip1.AutoSize = True
        Me.lblHourstoFlip1.Location = New System.Drawing.Point(18, 43)
        Me.lblHourstoFlip1.Name = "lblHourstoFlip1"
        Me.lblHourstoFlip1.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip1.TabIndex = 56
        Me.lblHourstoFlip1.Text = "Hours to Flip:"
        Me.lblHourstoFlip1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSmallBeltMineralComp
        '
        Me.lblSmallBeltMineralComp.AutoSize = True
        Me.lblSmallBeltMineralComp.Location = New System.Drawing.Point(320, 255)
        Me.lblSmallBeltMineralComp.Name = "lblSmallBeltMineralComp"
        Me.lblSmallBeltMineralComp.Size = New System.Drawing.Size(122, 13)
        Me.lblSmallBeltMineralComp.TabIndex = 146
        Me.lblSmallBeltMineralComp.Text = "Belt Mineral Composition"
        Me.lblSmallBeltMineralComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstOresLevel1
        '
        Me.lstOresLevel1.CheckBoxes = True
        Me.lstOresLevel1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxSmall, Me.orenameSmall, Me.numberroidsSmall, Me.unitsSmall})
        Me.lstOresLevel1.FullRowSelect = True
        Me.lstOresLevel1.GridLines = True
        Me.lstOresLevel1.HideSelection = False
        Me.lstOresLevel1.Location = New System.Drawing.Point(12, 271)
        Me.lstOresLevel1.MultiSelect = False
        Me.lstOresLevel1.Name = "lstOresLevel1"
        Me.lstOresLevel1.Size = New System.Drawing.Size(235, 238)
        Me.lstOresLevel1.TabIndex = 141
        Me.lstOresLevel1.UseCompatibleStateImageBehavior = False
        Me.lstOresLevel1.View = System.Windows.Forms.View.Details
        '
        'checkboxSmall
        '
        Me.checkboxSmall.Text = ""
        Me.checkboxSmall.Width = 25
        '
        'orenameSmall
        '
        Me.orenameSmall.Text = "Ore"
        Me.orenameSmall.Width = 100
        '
        'numberroidsSmall
        '
        Me.numberroidsSmall.Text = "Rocks"
        Me.numberroidsSmall.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numberroidsSmall.Width = 43
        '
        'unitsSmall
        '
        Me.unitsSmall.Text = "Units"
        Me.unitsSmall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.unitsSmall.Width = 63
        '
        'lstMineralsLevel1
        '
        Me.lstMineralsLevel1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.mineralsSmall, Me.minsunitsSmall, Me.totaliskSmall})
        Me.lstMineralsLevel1.FullRowSelect = True
        Me.lstMineralsLevel1.GridLines = True
        Me.lstMineralsLevel1.HideSelection = False
        Me.lstMineralsLevel1.Location = New System.Drawing.Point(253, 271)
        Me.lstMineralsLevel1.MultiSelect = False
        Me.lstMineralsLevel1.Name = "lstMineralsLevel1"
        Me.lstMineralsLevel1.Size = New System.Drawing.Size(243, 81)
        Me.lstMineralsLevel1.TabIndex = 142
        Me.lstMineralsLevel1.UseCompatibleStateImageBehavior = False
        Me.lstMineralsLevel1.View = System.Windows.Forms.View.Details
        '
        'mineralsSmall
        '
        Me.mineralsSmall.Text = "Mineral"
        Me.mineralsSmall.Width = 57
        '
        'minsunitsSmall
        '
        Me.minsunitsSmall.Text = "Units"
        Me.minsunitsSmall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.minsunitsSmall.Width = 79
        '
        'totaliskSmall
        '
        Me.totaliskSmall.Text = "Total Isk"
        Me.totaliskSmall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.totaliskSmall.Width = 103
        '
        'lblSmallBeltOreComp
        '
        Me.lblSmallBeltOreComp.AutoSize = True
        Me.lblSmallBeltOreComp.Location = New System.Drawing.Point(79, 255)
        Me.lblSmallBeltOreComp.Name = "lblSmallBeltOreComp"
        Me.lblSmallBeltOreComp.Size = New System.Drawing.Size(105, 13)
        Me.lblSmallBeltOreComp.TabIndex = 143
        Me.lblSmallBeltOreComp.Text = "Belt Ore Composition"
        Me.lblSmallBeltOreComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'RefineStation
        '
        Me.RefineStation.Location = New System.Drawing.Point(286, 70)
        Me.RefineStation.Name = "RefineStation"
        Me.RefineStation.Size = New System.Drawing.Size(303, 128)
        Me.RefineStation.TabIndex = 125
        '
        'frmIceBeltFlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 537)
        Me.Controls.Add(Me.btnCloseSmall)
        Me.Controls.Add(Me.btnSaveSettingsSmall)
        Me.Controls.Add(Me.gbSum1)
        Me.Controls.Add(Me.lblSmallBeltMineralComp)
        Me.Controls.Add(Me.lstOresLevel1)
        Me.Controls.Add(Me.lstMineralsLevel1)
        Me.Controls.Add(Me.lblSmallBeltOreComp)
        Me.Controls.Add(Me.gbSummarySettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIceBeltFlip"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ice Anomaly Belts"
        Me.gbSummarySettings.ResumeLayout(False)
        Me.gbSummarySettings.PerformLayout()
        Me.gbTrueSec.ResumeLayout(False)
        Me.gbTrueSec.PerformLayout()
        Me.gbSpace.ResumeLayout(False)
        Me.gbSpace.PerformLayout()
        Me.gbMineTaxBroker.ResumeLayout(False)
        Me.gbMineTaxBroker.PerformLayout()
        Me.gbSum1.ResumeLayout(False)
        Me.gbSum1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbSummarySettings As GroupBox
    Friend WithEvents gbTrueSec As GroupBox
    Friend WithEvents rbtn10percent As RadioButton
    Friend WithEvents rbtn5percent As RadioButton
    Friend WithEvents rbtnHighsec As RadioButton
    Friend WithEvents btnRefresh As Button
    Friend WithEvents gbMineTaxBroker As GroupBox
    Friend WithEvents txtBrokerFeeRate As TextBox
    Friend WithEvents chkIPHperMiner As CheckBox
    Friend WithEvents chkCompressOre As CheckBox
    Friend WithEvents chkIncludeTaxes As CheckBox
    Friend WithEvents chkBrokerFees As CheckBox
    Friend WithEvents lblCycleTime As Label
    Friend WithEvents txtCycleTime As TextBox
    Friend WithEvents txtm3perCycle As TextBox
    Friend WithEvents btnSaveSettings As Button
    Friend WithEvents lblm3perCycle As Label
    Friend WithEvents cmbNumMiners As ComboBox
    Friend WithEvents lblNumMiners As Label
    Friend WithEvents lblm3perhrperminer As Label
    Friend WithEvents lblm3perhrperminer1 As Label
    Friend WithEvents btnCloseSmall As Button
    Friend WithEvents btnSaveSettingsSmall As Button
    Friend WithEvents gbSum1 As GroupBox
    Friend WithEvents lblTotalIPH1 As Label
    Friend WithEvents lblTotalBeltVolume1 As Label
    Friend WithEvents lblBeltTotalIsk1 As Label
    Friend WithEvents lblTotalHourstoFlip1 As Label
    Friend WithEvents lblTotalIskLevel1 As Label
    Friend WithEvents lblIPH1 As Label
    Friend WithEvents lblTotalBeltVol1 As Label
    Friend WithEvents lblHourstoFlip1 As Label
    Friend WithEvents lblSmallBeltMineralComp As Label
    Friend WithEvents lstOresLevel1 As ListView
    Friend WithEvents checkboxSmall As ColumnHeader
    Friend WithEvents orenameSmall As ColumnHeader
    Friend WithEvents numberroidsSmall As ColumnHeader
    Friend WithEvents unitsSmall As ColumnHeader
    Friend WithEvents lstMineralsLevel1 As ListView
    Friend WithEvents mineralsSmall As ColumnHeader
    Friend WithEvents minsunitsSmall As ColumnHeader
    Friend WithEvents totaliskSmall As ColumnHeader
    Friend WithEvents lblSmallBeltOreComp As Label
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents gbSpace As GroupBox
    Friend WithEvents rbtnMinmatar As RadioButton
    Friend WithEvents rbtnGallente As RadioButton
    Friend WithEvents rbtnCaldari As RadioButton
    Friend WithEvents rbtnAmarr As RadioButton
    Friend WithEvents RefineStation As ManufacturingFacility
End Class
