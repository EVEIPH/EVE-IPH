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
        Me.gbMineTaxBroker = New System.Windows.Forms.GroupBox()
        Me.txtBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.chkIPHperMiner = New System.Windows.Forms.CheckBox()
        Me.chkCompressIce = New System.Windows.Forms.CheckBox()
        Me.chkIncludeTaxes = New System.Windows.Forms.CheckBox()
        Me.chkBrokerFees = New System.Windows.Forms.CheckBox()
        Me.ReprocessingFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.gbTrueSec = New System.Windows.Forms.GroupBox()
        Me.rbtnNullWeak = New System.Windows.Forms.RadioButton()
        Me.rbtnLowsec = New System.Windows.Forms.RadioButton()
        Me.rbtnHighsec = New System.Windows.Forms.RadioButton()
        Me.rbtnNullStrong = New System.Windows.Forms.RadioButton()
        Me.gbSpace = New System.Windows.Forms.GroupBox()
        Me.rbtnCaldari = New System.Windows.Forms.RadioButton()
        Me.rbtnMinmatar = New System.Windows.Forms.RadioButton()
        Me.rbtnGallente = New System.Windows.Forms.RadioButton()
        Me.rbtnAmarr = New System.Windows.Forms.RadioButton()
        Me.btnRefine = New System.Windows.Forms.Button()
        Me.lblCycleTime = New System.Windows.Forms.Label()
        Me.txtCycleTime = New System.Windows.Forms.TextBox()
        Me.txtm3perCycle = New System.Windows.Forms.TextBox()
        Me.lblm3perCycle = New System.Windows.Forms.Label()
        Me.cmbNumMiners = New System.Windows.Forms.ComboBox()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.lblNumMiners = New System.Windows.Forms.Label()
        Me.lblm3perhrperminer = New System.Windows.Forms.Label()
        Me.lblm3perhrperminer1 = New System.Windows.Forms.Label()
        Me.btnCloseSmall = New System.Windows.Forms.Button()
        Me.btnSaveChecks = New System.Windows.Forms.Button()
        Me.gbSum1 = New System.Windows.Forms.GroupBox()
        Me.lblBeltIPH1 = New System.Windows.Forms.Label()
        Me.lblBeltIPH = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTotalRefineVolume = New System.Windows.Forms.Label()
        Me.lblTotalBeltValue = New System.Windows.Forms.Label()
        Me.lblTotalIPH1 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVolume = New System.Windows.Forms.Label()
        Me.lblTotalBeltValue1 = New System.Windows.Forms.Label()
        Me.lblTotalRefineValue1 = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip = New System.Windows.Forms.Label()
        Me.lblTotalRefineValue = New System.Windows.Forms.Label()
        Me.lblRefineIPH = New System.Windows.Forms.Label()
        Me.lblTotalBeltVol1 = New System.Windows.Forms.Label()
        Me.lblHourstoFlip1 = New System.Windows.Forms.Label()
        Me.lblIceProductComposition = New System.Windows.Forms.Label()
        Me.lstIce = New System.Windows.Forms.ListView()
        Me.checkboxIce = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IceName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IceUnits = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IcePPU = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstIceProducts = New System.Windows.Forms.ListView()
        Me.iceProducts = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.units = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.totalISK = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblBeltComp = New System.Windows.Forms.Label()
        Me.gbSummarySettings.SuspendLayout()
        Me.gbMineTaxBroker.SuspendLayout()
        Me.gbTrueSec.SuspendLayout()
        Me.gbSpace.SuspendLayout()
        Me.gbSum1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbSummarySettings
        '
        Me.gbSummarySettings.Controls.Add(Me.gbMineTaxBroker)
        Me.gbSummarySettings.Controls.Add(Me.ReprocessingFacility)
        Me.gbSummarySettings.Controls.Add(Me.gbTrueSec)
        Me.gbSummarySettings.Controls.Add(Me.gbSpace)
        Me.gbSummarySettings.Controls.Add(Me.btnRefine)
        Me.gbSummarySettings.Controls.Add(Me.lblCycleTime)
        Me.gbSummarySettings.Controls.Add(Me.txtCycleTime)
        Me.gbSummarySettings.Controls.Add(Me.txtm3perCycle)
        Me.gbSummarySettings.Controls.Add(Me.lblm3perCycle)
        Me.gbSummarySettings.Controls.Add(Me.cmbNumMiners)
        Me.gbSummarySettings.Controls.Add(Me.btnSaveSettings)
        Me.gbSummarySettings.Controls.Add(Me.lblNumMiners)
        Me.gbSummarySettings.Controls.Add(Me.lblm3perhrperminer)
        Me.gbSummarySettings.Controls.Add(Me.lblm3perhrperminer1)
        Me.gbSummarySettings.Location = New System.Drawing.Point(9, 3)
        Me.gbSummarySettings.Name = "gbSummarySettings"
        Me.gbSummarySettings.Size = New System.Drawing.Size(601, 177)
        Me.gbSummarySettings.TabIndex = 140
        Me.gbSummarySettings.TabStop = False
        Me.gbSummarySettings.Text = "Settings"
        '
        'gbMineTaxBroker
        '
        Me.gbMineTaxBroker.Controls.Add(Me.txtBrokerFeeRate)
        Me.gbMineTaxBroker.Controls.Add(Me.chkIPHperMiner)
        Me.gbMineTaxBroker.Controls.Add(Me.chkCompressIce)
        Me.gbMineTaxBroker.Controls.Add(Me.chkIncludeTaxes)
        Me.gbMineTaxBroker.Controls.Add(Me.chkBrokerFees)
        Me.gbMineTaxBroker.Location = New System.Drawing.Point(313, 26)
        Me.gbMineTaxBroker.Name = "gbMineTaxBroker"
        Me.gbMineTaxBroker.Size = New System.Drawing.Size(114, 88)
        Me.gbMineTaxBroker.TabIndex = 126
        Me.gbMineTaxBroker.TabStop = False
        Me.gbMineTaxBroker.Text = "Options:"
        '
        'txtBrokerFeeRate
        '
        Me.txtBrokerFeeRate.Location = New System.Drawing.Point(57, 63)
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
        Me.chkIPHperMiner.Location = New System.Drawing.Point(9, 33)
        Me.chkIPHperMiner.Name = "chkIPHperMiner"
        Me.chkIPHperMiner.Size = New System.Drawing.Size(91, 17)
        Me.chkIPHperMiner.TabIndex = 9
        Me.chkIPHperMiner.Text = "IPH per Miner"
        Me.chkIPHperMiner.UseVisualStyleBackColor = True
        '
        'chkCompressIce
        '
        Me.chkCompressIce.AutoSize = True
        Me.chkCompressIce.Checked = True
        Me.chkCompressIce.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressIce.Location = New System.Drawing.Point(9, 17)
        Me.chkCompressIce.Name = "chkCompressIce"
        Me.chkCompressIce.Size = New System.Drawing.Size(90, 17)
        Me.chkCompressIce.TabIndex = 8
        Me.chkCompressIce.Text = "Compress Ice"
        Me.chkCompressIce.UseVisualStyleBackColor = True
        '
        'chkIncludeTaxes
        '
        Me.chkIncludeTaxes.AutoSize = True
        Me.chkIncludeTaxes.Checked = True
        Me.chkIncludeTaxes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeTaxes.Location = New System.Drawing.Point(9, 49)
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
        Me.chkBrokerFees.Location = New System.Drawing.Point(9, 65)
        Me.chkBrokerFees.Name = "chkBrokerFees"
        Me.chkBrokerFees.Size = New System.Drawing.Size(49, 17)
        Me.chkBrokerFees.TabIndex = 10
        Me.chkBrokerFees.Text = "Fees"
        Me.chkBrokerFees.ThreeState = True
        Me.chkBrokerFees.UseVisualStyleBackColor = True
        '
        'ReprocessingFacility
        '
        Me.ReprocessingFacility.BackColor = System.Drawing.Color.Transparent
        Me.ReprocessingFacility.Location = New System.Drawing.Point(6, 60)
        Me.ReprocessingFacility.Name = "ReprocessingFacility"
        Me.ReprocessingFacility.Size = New System.Drawing.Size(303, 108)
        Me.ReprocessingFacility.TabIndex = 125
        '
        'gbTrueSec
        '
        Me.gbTrueSec.Controls.Add(Me.rbtnNullWeak)
        Me.gbTrueSec.Controls.Add(Me.rbtnLowsec)
        Me.gbTrueSec.Controls.Add(Me.rbtnHighsec)
        Me.gbTrueSec.Controls.Add(Me.rbtnNullStrong)
        Me.gbTrueSec.Location = New System.Drawing.Point(433, 26)
        Me.gbTrueSec.Name = "gbTrueSec"
        Me.gbTrueSec.Size = New System.Drawing.Size(161, 88)
        Me.gbTrueSec.TabIndex = 123
        Me.gbTrueSec.TabStop = False
        Me.gbTrueSec.Text = "System Security:"
        '
        'rbtnNullWeak
        '
        Me.rbtnNullWeak.AutoSize = True
        Me.rbtnNullWeak.Location = New System.Drawing.Point(6, 39)
        Me.rbtnNullWeak.Name = "rbtnNullWeak"
        Me.rbtnNullWeak.Size = New System.Drawing.Size(149, 17)
        Me.rbtnNullWeak.TabIndex = 5
        Me.rbtnNullWeak.Text = "Nullsec Weak (0.0 to -0.5)"
        Me.rbtnNullWeak.UseVisualStyleBackColor = True
        '
        'rbtnLowsec
        '
        Me.rbtnLowsec.AutoSize = True
        Me.rbtnLowsec.Location = New System.Drawing.Point(92, 19)
        Me.rbtnLowsec.Name = "rbtnLowsec"
        Me.rbtnLowsec.Size = New System.Drawing.Size(62, 17)
        Me.rbtnLowsec.TabIndex = 4
        Me.rbtnLowsec.Text = "Lowsec"
        Me.rbtnLowsec.UseVisualStyleBackColor = True
        '
        'rbtnHighsec
        '
        Me.rbtnHighsec.AutoSize = True
        Me.rbtnHighsec.Checked = True
        Me.rbtnHighsec.Location = New System.Drawing.Point(6, 19)
        Me.rbtnHighsec.Name = "rbtnHighsec"
        Me.rbtnHighsec.Size = New System.Drawing.Size(64, 17)
        Me.rbtnHighsec.TabIndex = 3
        Me.rbtnHighsec.TabStop = True
        Me.rbtnHighsec.Text = "Highsec"
        Me.rbtnHighsec.UseVisualStyleBackColor = True
        '
        'rbtnNullStrong
        '
        Me.rbtnNullStrong.AutoSize = True
        Me.rbtnNullStrong.Location = New System.Drawing.Point(6, 59)
        Me.rbtnNullStrong.Name = "rbtnNullStrong"
        Me.rbtnNullStrong.Size = New System.Drawing.Size(154, 17)
        Me.rbtnNullStrong.TabIndex = 6
        Me.rbtnNullStrong.Text = "Nullsec Strong (-0.5 to -1.0)"
        Me.rbtnNullStrong.UseVisualStyleBackColor = True
        '
        'gbSpace
        '
        Me.gbSpace.Controls.Add(Me.rbtnCaldari)
        Me.gbSpace.Controls.Add(Me.rbtnMinmatar)
        Me.gbSpace.Controls.Add(Me.rbtnGallente)
        Me.gbSpace.Controls.Add(Me.rbtnAmarr)
        Me.gbSpace.Location = New System.Drawing.Point(433, 114)
        Me.gbSpace.Name = "gbSpace"
        Me.gbSpace.Size = New System.Drawing.Size(161, 55)
        Me.gbSpace.TabIndex = 124
        Me.gbSpace.TabStop = False
        Me.gbSpace.Text = "Space:"
        '
        'rbtnCaldari
        '
        Me.rbtnCaldari.AutoSize = True
        Me.rbtnCaldari.Location = New System.Drawing.Point(83, 14)
        Me.rbtnCaldari.Name = "rbtnCaldari"
        Me.rbtnCaldari.Size = New System.Drawing.Size(57, 17)
        Me.rbtnCaldari.TabIndex = 4
        Me.rbtnCaldari.Text = "Caldari"
        Me.rbtnCaldari.UseVisualStyleBackColor = True
        '
        'rbtnMinmatar
        '
        Me.rbtnMinmatar.AutoSize = True
        Me.rbtnMinmatar.Location = New System.Drawing.Point(83, 32)
        Me.rbtnMinmatar.Name = "rbtnMinmatar"
        Me.rbtnMinmatar.Size = New System.Drawing.Size(68, 17)
        Me.rbtnMinmatar.TabIndex = 6
        Me.rbtnMinmatar.Text = "Minmatar"
        Me.rbtnMinmatar.UseVisualStyleBackColor = True
        '
        'rbtnGallente
        '
        Me.rbtnGallente.AutoSize = True
        Me.rbtnGallente.Location = New System.Drawing.Point(9, 32)
        Me.rbtnGallente.Name = "rbtnGallente"
        Me.rbtnGallente.Size = New System.Drawing.Size(64, 17)
        Me.rbtnGallente.TabIndex = 5
        Me.rbtnGallente.Text = "Gallente"
        Me.rbtnGallente.UseVisualStyleBackColor = True
        '
        'rbtnAmarr
        '
        Me.rbtnAmarr.AutoSize = True
        Me.rbtnAmarr.Checked = True
        Me.rbtnAmarr.Location = New System.Drawing.Point(9, 14)
        Me.rbtnAmarr.Name = "rbtnAmarr"
        Me.rbtnAmarr.Size = New System.Drawing.Size(52, 17)
        Me.rbtnAmarr.TabIndex = 3
        Me.rbtnAmarr.TabStop = True
        Me.rbtnAmarr.Text = "Amarr"
        Me.rbtnAmarr.UseVisualStyleBackColor = True
        '
        'btnRefine
        '
        Me.btnRefine.Location = New System.Drawing.Point(313, 117)
        Me.btnRefine.Name = "btnRefine"
        Me.btnRefine.Size = New System.Drawing.Size(114, 26)
        Me.btnRefine.TabIndex = 3
        Me.btnRefine.Text = "Refine"
        Me.btnRefine.UseVisualStyleBackColor = True
        '
        'lblCycleTime
        '
        Me.lblCycleTime.AutoSize = True
        Me.lblCycleTime.Location = New System.Drawing.Point(75, 17)
        Me.lblCycleTime.Name = "lblCycleTime"
        Me.lblCycleTime.Size = New System.Drawing.Size(62, 13)
        Me.lblCycleTime.TabIndex = 111
        Me.lblCycleTime.Text = "Cycle Time:"
        '
        'txtCycleTime
        '
        Me.txtCycleTime.Location = New System.Drawing.Point(75, 32)
        Me.txtCycleTime.Name = "txtCycleTime"
        Me.txtCycleTime.Size = New System.Drawing.Size(66, 20)
        Me.txtCycleTime.TabIndex = 1
        Me.txtCycleTime.Text = "104.67"
        Me.txtCycleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtm3perCycle
        '
        Me.txtm3perCycle.Location = New System.Drawing.Point(144, 32)
        Me.txtm3perCycle.Name = "txtm3perCycle"
        Me.txtm3perCycle.Size = New System.Drawing.Size(66, 20)
        Me.txtm3perCycle.TabIndex = 2
        Me.txtm3perCycle.Text = "5833.11"
        Me.txtm3perCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblm3perCycle
        '
        Me.lblm3perCycle.AutoSize = True
        Me.lblm3perCycle.Location = New System.Drawing.Point(141, 16)
        Me.lblm3perCycle.Name = "lblm3perCycle"
        Me.lblm3perCycle.Size = New System.Drawing.Size(71, 13)
        Me.lblm3perCycle.TabIndex = 113
        Me.lblm3perCycle.Text = "m3 per Cycle:"
        '
        'cmbNumMiners
        '
        Me.cmbNumMiners.FormattingEnabled = True
        Me.cmbNumMiners.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbNumMiners.Location = New System.Drawing.Point(6, 32)
        Me.cmbNumMiners.Name = "cmbNumMiners"
        Me.cmbNumMiners.Size = New System.Drawing.Size(66, 21)
        Me.cmbNumMiners.TabIndex = 0
        Me.cmbNumMiners.Text = "10"
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(313, 143)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(114, 26)
        Me.btnSaveSettings.TabIndex = 4
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'lblNumMiners
        '
        Me.lblNumMiners.AutoSize = True
        Me.lblNumMiners.Location = New System.Drawing.Point(6, 17)
        Me.lblNumMiners.Name = "lblNumMiners"
        Me.lblNumMiners.Size = New System.Drawing.Size(66, 13)
        Me.lblNumMiners.TabIndex = 115
        Me.lblNumMiners.Text = "Num Miners:"
        '
        'lblm3perhrperminer
        '
        Me.lblm3perhrperminer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblm3perhrperminer.Location = New System.Drawing.Point(213, 32)
        Me.lblm3perhrperminer.Name = "lblm3perhrperminer"
        Me.lblm3perhrperminer.Size = New System.Drawing.Size(97, 20)
        Me.lblm3perhrperminer.TabIndex = 122
        Me.lblm3perhrperminer.Text = "999,999.00"
        Me.lblm3perhrperminer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblm3perhrperminer1
        '
        Me.lblm3perhrperminer1.AutoSize = True
        Me.lblm3perhrperminer1.Location = New System.Drawing.Point(210, 16)
        Me.lblm3perhrperminer1.Name = "lblm3perhrperminer1"
        Me.lblm3perhrperminer1.Size = New System.Drawing.Size(68, 13)
        Me.lblm3perhrperminer1.TabIndex = 121
        Me.lblm3perhrperminer1.Text = "m3/hr/miner:"
        '
        'btnCloseSmall
        '
        Me.btnCloseSmall.Location = New System.Drawing.Point(523, 400)
        Me.btnCloseSmall.Name = "btnCloseSmall"
        Me.btnCloseSmall.Size = New System.Drawing.Size(87, 40)
        Me.btnCloseSmall.TabIndex = 145
        Me.btnCloseSmall.Text = "Close"
        Me.btnCloseSmall.UseVisualStyleBackColor = True
        '
        'btnSaveChecks
        '
        Me.btnSaveChecks.Location = New System.Drawing.Point(523, 354)
        Me.btnSaveChecks.Name = "btnSaveChecks"
        Me.btnSaveChecks.Size = New System.Drawing.Size(87, 40)
        Me.btnSaveChecks.TabIndex = 144
        Me.btnSaveChecks.Text = "Save Ice Selected "
        Me.btnSaveChecks.UseVisualStyleBackColor = True
        '
        'gbSum1
        '
        Me.gbSum1.Controls.Add(Me.lblBeltIPH1)
        Me.gbSum1.Controls.Add(Me.lblBeltIPH)
        Me.gbSum1.Controls.Add(Me.Label1)
        Me.gbSum1.Controls.Add(Me.lblTotalRefineVolume)
        Me.gbSum1.Controls.Add(Me.lblTotalBeltValue)
        Me.gbSum1.Controls.Add(Me.lblTotalIPH1)
        Me.gbSum1.Controls.Add(Me.lblTotalBeltVolume)
        Me.gbSum1.Controls.Add(Me.lblTotalBeltValue1)
        Me.gbSum1.Controls.Add(Me.lblTotalRefineValue1)
        Me.gbSum1.Controls.Add(Me.lblTotalHourstoFlip)
        Me.gbSum1.Controls.Add(Me.lblTotalRefineValue)
        Me.gbSum1.Controls.Add(Me.lblRefineIPH)
        Me.gbSum1.Controls.Add(Me.lblTotalBeltVol1)
        Me.gbSum1.Controls.Add(Me.lblHourstoFlip1)
        Me.gbSum1.Location = New System.Drawing.Point(8, 344)
        Me.gbSum1.Name = "gbSum1"
        Me.gbSum1.Size = New System.Drawing.Size(509, 99)
        Me.gbSum1.TabIndex = 147
        Me.gbSum1.TabStop = False
        '
        'lblBeltIPH1
        '
        Me.lblBeltIPH1.AutoSize = True
        Me.lblBeltIPH1.Location = New System.Drawing.Point(38, 78)
        Me.lblBeltIPH1.Name = "lblBeltIPH1"
        Me.lblBeltIPH1.Size = New System.Drawing.Size(68, 13)
        Me.lblBeltIPH1.TabIndex = 67
        Me.lblBeltIPH1.Text = "Isk per Hour:"
        Me.lblBeltIPH1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBeltIPH
        '
        Me.lblBeltIPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBeltIPH.Location = New System.Drawing.Point(112, 75)
        Me.lblBeltIPH.Name = "lblBeltIPH"
        Me.lblBeltIPH.Size = New System.Drawing.Size(125, 18)
        Me.lblBeltIPH.TabIndex = 66
        Me.lblBeltIPH.Text = "100,000.00"
        Me.lblBeltIPH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(271, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Refine Total Volume:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalRefineVolume
        '
        Me.lblTotalRefineVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalRefineVolume.Location = New System.Drawing.Point(378, 33)
        Me.lblTotalRefineVolume.Name = "lblTotalRefineVolume"
        Me.lblTotalRefineVolume.Size = New System.Drawing.Size(125, 18)
        Me.lblTotalRefineVolume.TabIndex = 64
        Me.lblTotalRefineVolume.Text = "100,000.00"
        Me.lblTotalRefineVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltValue
        '
        Me.lblTotalBeltValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltValue.Location = New System.Drawing.Point(112, 12)
        Me.lblTotalBeltValue.Name = "lblTotalBeltValue"
        Me.lblTotalBeltValue.Size = New System.Drawing.Size(125, 18)
        Me.lblTotalBeltValue.TabIndex = 63
        Me.lblTotalBeltValue.Text = "100,000.00"
        Me.lblTotalBeltValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIPH1
        '
        Me.lblTotalIPH1.AutoSize = True
        Me.lblTotalIPH1.Location = New System.Drawing.Point(275, 57)
        Me.lblTotalIPH1.Name = "lblTotalIPH1"
        Me.lblTotalIPH1.Size = New System.Drawing.Size(102, 13)
        Me.lblTotalIPH1.TabIndex = 61
        Me.lblTotalIPH1.Text = "Refine Isk per Hour:"
        Me.lblTotalIPH1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBeltVolume
        '
        Me.lblTotalBeltVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume.Location = New System.Drawing.Point(112, 33)
        Me.lblTotalBeltVolume.Name = "lblTotalBeltVolume"
        Me.lblTotalBeltVolume.Size = New System.Drawing.Size(125, 18)
        Me.lblTotalBeltVolume.TabIndex = 60
        Me.lblTotalBeltVolume.Text = "100,000.00"
        Me.lblTotalBeltVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltValue1
        '
        Me.lblTotalBeltValue1.AutoSize = True
        Me.lblTotalBeltValue1.Location = New System.Drawing.Point(21, 13)
        Me.lblTotalBeltValue1.Name = "lblTotalBeltValue1"
        Me.lblTotalBeltValue1.Size = New System.Drawing.Size(85, 13)
        Me.lblTotalBeltValue1.TabIndex = 62
        Me.lblTotalBeltValue1.Text = "Belt Total Value:"
        Me.lblTotalBeltValue1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalRefineValue1
        '
        Me.lblTotalRefineValue1.AutoSize = True
        Me.lblTotalRefineValue1.Location = New System.Drawing.Point(279, 15)
        Me.lblTotalRefineValue1.Name = "lblTotalRefineValue1"
        Me.lblTotalRefineValue1.Size = New System.Drawing.Size(98, 13)
        Me.lblTotalRefineValue1.TabIndex = 52
        Me.lblTotalRefineValue1.Text = "Refine Total Value:"
        Me.lblTotalRefineValue1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip
        '
        Me.lblTotalHourstoFlip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip.Location = New System.Drawing.Point(112, 54)
        Me.lblTotalHourstoFlip.Name = "lblTotalHourstoFlip"
        Me.lblTotalHourstoFlip.Size = New System.Drawing.Size(125, 18)
        Me.lblTotalHourstoFlip.TabIndex = 57
        Me.lblTotalHourstoFlip.Text = "100,000.00"
        Me.lblTotalHourstoFlip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalRefineValue
        '
        Me.lblTotalRefineValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalRefineValue.Location = New System.Drawing.Point(378, 12)
        Me.lblTotalRefineValue.Name = "lblTotalRefineValue"
        Me.lblTotalRefineValue.Size = New System.Drawing.Size(125, 18)
        Me.lblTotalRefineValue.TabIndex = 53
        Me.lblTotalRefineValue.Text = "100,000.00"
        Me.lblTotalRefineValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRefineIPH
        '
        Me.lblRefineIPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRefineIPH.Location = New System.Drawing.Point(378, 54)
        Me.lblRefineIPH.Name = "lblRefineIPH"
        Me.lblRefineIPH.Size = New System.Drawing.Size(125, 18)
        Me.lblRefineIPH.TabIndex = 59
        Me.lblRefineIPH.Text = "100,000.00"
        Me.lblRefineIPH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltVol1
        '
        Me.lblTotalBeltVol1.AutoSize = True
        Me.lblTotalBeltVol1.Location = New System.Drawing.Point(13, 33)
        Me.lblTotalBeltVol1.Name = "lblTotalBeltVol1"
        Me.lblTotalBeltVol1.Size = New System.Drawing.Size(93, 13)
        Me.lblTotalBeltVol1.TabIndex = 54
        Me.lblTotalBeltVol1.Text = "Belt Total Volume:"
        Me.lblTotalBeltVol1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip1
        '
        Me.lblHourstoFlip1.AutoSize = True
        Me.lblHourstoFlip1.Location = New System.Drawing.Point(37, 57)
        Me.lblHourstoFlip1.Name = "lblHourstoFlip1"
        Me.lblHourstoFlip1.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip1.TabIndex = 56
        Me.lblHourstoFlip1.Text = "Hours to Flip:"
        Me.lblHourstoFlip1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIceProductComposition
        '
        Me.lblIceProductComposition.AutoSize = True
        Me.lblIceProductComposition.Location = New System.Drawing.Point(395, 183)
        Me.lblIceProductComposition.Name = "lblIceProductComposition"
        Me.lblIceProductComposition.Size = New System.Drawing.Size(122, 13)
        Me.lblIceProductComposition.TabIndex = 146
        Me.lblIceProductComposition.Text = "Ice Product Composition"
        Me.lblIceProductComposition.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstIce
        '
        Me.lstIce.CheckBoxes = True
        Me.lstIce.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxIce, Me.IceName, Me.IceUnits, Me.IcePPU})
        Me.lstIce.FullRowSelect = True
        Me.lstIce.GridLines = True
        Me.lstIce.HideSelection = False
        Me.lstIce.Location = New System.Drawing.Point(9, 199)
        Me.lstIce.MultiSelect = False
        Me.lstIce.Name = "lstIce"
        Me.lstIce.Size = New System.Drawing.Size(287, 148)
        Me.lstIce.TabIndex = 141
        Me.lstIce.UseCompatibleStateImageBehavior = False
        Me.lstIce.View = System.Windows.Forms.View.Details
        '
        'checkboxIce
        '
        Me.checkboxIce.Text = ""
        Me.checkboxIce.Width = 25
        '
        'IceName
        '
        Me.IceName.Text = "Ice"
        Me.IceName.Width = 112
        '
        'IceUnits
        '
        Me.IceUnits.Text = "Units"
        Me.IceUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.IceUnits.Width = 50
        '
        'IcePPU
        '
        Me.IcePPU.Text = "Price per Unit"
        Me.IcePPU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.IcePPU.Width = 96
        '
        'lstIceProducts
        '
        Me.lstIceProducts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.iceProducts, Me.units, Me.totalISK})
        Me.lstIceProducts.FullRowSelect = True
        Me.lstIceProducts.GridLines = True
        Me.lstIceProducts.HideSelection = False
        Me.lstIceProducts.Location = New System.Drawing.Point(302, 199)
        Me.lstIceProducts.MultiSelect = False
        Me.lstIceProducts.Name = "lstIceProducts"
        Me.lstIceProducts.Size = New System.Drawing.Size(308, 148)
        Me.lstIceProducts.TabIndex = 142
        Me.lstIceProducts.UseCompatibleStateImageBehavior = False
        Me.lstIceProducts.View = System.Windows.Forms.View.Details
        '
        'iceProducts
        '
        Me.iceProducts.Text = "Ice Product"
        Me.iceProducts.Width = 120
        '
        'units
        '
        Me.units.Text = "Units"
        Me.units.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.units.Width = 78
        '
        'totalISK
        '
        Me.totalISK.Text = "Total Isk"
        Me.totalISK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.totalISK.Width = 106
        '
        'lblBeltComp
        '
        Me.lblBeltComp.AutoSize = True
        Me.lblBeltComp.Location = New System.Drawing.Point(101, 183)
        Me.lblBeltComp.Name = "lblBeltComp"
        Me.lblBeltComp.Size = New System.Drawing.Size(103, 13)
        Me.lblBeltComp.TabIndex = 143
        Me.lblBeltComp.Text = "Ice Belt Composition"
        Me.lblBeltComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmIceBeltFlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 446)
        Me.Controls.Add(Me.lblIceProductComposition)
        Me.Controls.Add(Me.lstIce)
        Me.Controls.Add(Me.lstIceProducts)
        Me.Controls.Add(Me.lblBeltComp)
        Me.Controls.Add(Me.gbSummarySettings)
        Me.Controls.Add(Me.btnCloseSmall)
        Me.Controls.Add(Me.btnSaveChecks)
        Me.Controls.Add(Me.gbSum1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIceBeltFlip"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ice Anomaly Belts"
        Me.gbSummarySettings.ResumeLayout(False)
        Me.gbSummarySettings.PerformLayout()
        Me.gbMineTaxBroker.ResumeLayout(False)
        Me.gbMineTaxBroker.PerformLayout()
        Me.gbTrueSec.ResumeLayout(False)
        Me.gbTrueSec.PerformLayout()
        Me.gbSpace.ResumeLayout(False)
        Me.gbSpace.PerformLayout()
        Me.gbSum1.ResumeLayout(False)
        Me.gbSum1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbSummarySettings As GroupBox
    Friend WithEvents gbTrueSec As GroupBox
    Friend WithEvents rbtnNullWeak As RadioButton
    Friend WithEvents rbtnLowsec As RadioButton
    Friend WithEvents rbtnHighsec As RadioButton
    Friend WithEvents btnRefine As Button
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
    Friend WithEvents btnSaveChecks As Button
    Friend WithEvents gbSum1 As GroupBox
    Friend WithEvents lblTotalIPH1 As Label
    Friend WithEvents lblTotalBeltVolume As Label
    Friend WithEvents lblTotalRefineValue1 As Label
    Friend WithEvents lblTotalHourstoFlip As Label
    Friend WithEvents lblTotalRefineValue As Label
    Friend WithEvents lblRefineIPH As Label
    Friend WithEvents lblTotalBeltVol1 As Label
    Friend WithEvents lblHourstoFlip1 As Label
    Friend WithEvents lblIceProductComposition As Label
    Friend WithEvents lstIce As ListView
    Friend WithEvents checkboxIce As ColumnHeader
    Friend WithEvents IceName As ColumnHeader
    Friend WithEvents IceUnits As ColumnHeader
    Friend WithEvents lstIceProducts As ListView
    Friend WithEvents iceProducts As ColumnHeader
    Friend WithEvents units As ColumnHeader
    Friend WithEvents totalISK As ColumnHeader
    Friend WithEvents lblBeltComp As Label
    Friend WithEvents rbtnNullStrong As RadioButton
    Friend WithEvents gbSpace As GroupBox
    Friend WithEvents rbtnMinmatar As RadioButton
    Friend WithEvents rbtnGallente As RadioButton
    Friend WithEvents rbtnCaldari As RadioButton
    Friend WithEvents rbtnAmarr As RadioButton
    Friend WithEvents ReprocessingFacility As ManufacturingFacility
    Friend WithEvents gbMineTaxBroker As GroupBox
    Friend WithEvents txtBrokerFeeRate As TextBox
    Friend WithEvents chkIPHperMiner As CheckBox
    Friend WithEvents chkCompressIce As CheckBox
    Friend WithEvents chkIncludeTaxes As CheckBox
    Friend WithEvents chkBrokerFees As CheckBox
    Friend WithEvents IcePPU As ColumnHeader
    Friend WithEvents lblTotalBeltValue As Label
    Friend WithEvents lblTotalBeltValue1 As Label
    Friend WithEvents lblBeltIPH1 As Label
    Friend WithEvents lblBeltIPH As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblTotalRefineVolume As Label
End Class
