<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIndustryBeltFlip
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIndustryBeltFlip))
        Me.lstOresLevel1 = New System.Windows.Forms.ListView()
        Me.checkboxSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.orenameSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.numberroidsSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.unitsSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.lstMineralsLevel1 = New System.Windows.Forms.ListView()
        Me.mineralsSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.minsunitsSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.totaliskSmall = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.lblSmallBeltOreComp = New System.Windows.Forms.Label()
        Me.lblBeltTotalIskLevel1Sum = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel1Sum = New System.Windows.Forms.Label()
        Me.gbMineTaxBroker = New System.Windows.Forms.GroupBox()
        Me.txtBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.chkIPHperMiner = New System.Windows.Forms.CheckBox()
        Me.chkCompressOre = New System.Windows.Forms.CheckBox()
        Me.chkIncludeTaxes = New System.Windows.Forms.CheckBox()
        Me.chkBrokerFees = New System.Windows.Forms.CheckBox()
        Me.lblTotalBeltVolume1Sum = New System.Windows.Forms.Label()
        Me.lblBeltVolume1Sum = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip1Sum = New System.Windows.Forms.Label()
        Me.lblHourstoFlip1Sum = New System.Windows.Forms.Label()
        Me.lblTotalIPH1Sum = New System.Windows.Forms.Label()
        Me.lblIPH1Sum = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.lblCycleTime = New System.Windows.Forms.Label()
        Me.txtCycleTime = New System.Windows.Forms.TextBox()
        Me.lblm3perCycle = New System.Windows.Forms.Label()
        Me.txtm3perCycle = New System.Windows.Forms.TextBox()
        Me.lblNumMiners = New System.Windows.Forms.Label()
        Me.cmbNumMiners = New System.Windows.Forms.ComboBox()
        Me.lblm3perhrperminer1 = New System.Windows.Forms.Label()
        Me.lblm3perhrperminer = New System.Windows.Forms.Label()
        Me.tabIndustryBelts = New System.Windows.Forms.TabControl()
        Me.tabSummary = New System.Windows.Forms.TabPage()
        Me.gbGiant = New System.Windows.Forms.GroupBox()
        Me.lblTotalBeltVolume4Sum = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip4Sum = New System.Windows.Forms.Label()
        Me.lblIPH4Sum = New System.Windows.Forms.Label()
        Me.lblBeltVolume4Sum = New System.Windows.Forms.Label()
        Me.lblHourstoFlip4Sum = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel4Sum = New System.Windows.Forms.Label()
        Me.lblBeltTotalIskLevel4Sum = New System.Windows.Forms.Label()
        Me.lblTotalIPH4Sum = New System.Windows.Forms.Label()
        Me.gbExtraLarge = New System.Windows.Forms.GroupBox()
        Me.lblTotalBeltVolume5Sum = New System.Windows.Forms.Label()
        Me.lblTotalIPH5Sum = New System.Windows.Forms.Label()
        Me.lblIPH5Sum = New System.Windows.Forms.Label()
        Me.lblBeltTotalIskLevel5Sum = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip5Sum = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel5Sum = New System.Windows.Forms.Label()
        Me.lblBeltVolume5Sum = New System.Windows.Forms.Label()
        Me.lblHourstoFlip5Sum = New System.Windows.Forms.Label()
        Me.gbLarge = New System.Windows.Forms.GroupBox()
        Me.lblTotalBeltVolume3Sum = New System.Windows.Forms.Label()
        Me.lblIPH3Sum = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip3Sum = New System.Windows.Forms.Label()
        Me.lblTotalIPH3Sum = New System.Windows.Forms.Label()
        Me.lblHourstoFlip3Sum = New System.Windows.Forms.Label()
        Me.lblBeltVolume3Sum = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel3Sum = New System.Windows.Forms.Label()
        Me.lblBeltTotalIskLevel3Sum = New System.Windows.Forms.Label()
        Me.gbModerate = New System.Windows.Forms.GroupBox()
        Me.lblTotalBeltVolume2Sum = New System.Windows.Forms.Label()
        Me.lblIPH2Sum = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip2Sum = New System.Windows.Forms.Label()
        Me.lblTotalIPH2Sum = New System.Windows.Forms.Label()
        Me.lblHourstoFlip2Sum = New System.Windows.Forms.Label()
        Me.lblBeltVolume2Sum = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel2Sum = New System.Windows.Forms.Label()
        Me.lblBeltTotalIskLevel2Sum = New System.Windows.Forms.Label()
        Me.gbSmall = New System.Windows.Forms.GroupBox()
        Me.gbSummarySettings = New System.Windows.Forms.GroupBox()
        Me.ReprocessingFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.gbTruesec = New System.Windows.Forms.GroupBox()
        Me.rbtn10percent = New System.Windows.Forms.RadioButton()
        Me.rbtn0percent = New System.Windows.Forms.RadioButton()
        Me.rbtn5percent = New System.Windows.Forms.RadioButton()
        Me.btnRefine = New System.Windows.Forms.Button()
        Me.tabSmall = New System.Windows.Forms.TabPage()
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
        Me.tabMedium = New System.Windows.Forms.TabPage()
        Me.btnCloseMedium = New System.Windows.Forms.Button()
        Me.btnSaveSettingsMedium = New System.Windows.Forms.Button()
        Me.gbSum2 = New System.Windows.Forms.GroupBox()
        Me.lblTotalIPH2 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVolume2 = New System.Windows.Forms.Label()
        Me.lblBeltTotalIsk2 = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip2 = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel2 = New System.Windows.Forms.Label()
        Me.lblIPH2 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVol2 = New System.Windows.Forms.Label()
        Me.lblHourstoFlip2 = New System.Windows.Forms.Label()
        Me.lstMineralsLevel2 = New System.Windows.Forms.ListView()
        Me.mineralsMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.minsunitsMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.totaliskMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstOresLevel2 = New System.Windows.Forms.ListView()
        Me.checkboxMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.orenameMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.numberroidsMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.unitsMedium = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblMediumBeltMineralComp = New System.Windows.Forms.Label()
        Me.lblMediumBeltOreComp = New System.Windows.Forms.Label()
        Me.tabLarge = New System.Windows.Forms.TabPage()
        Me.btnCloseLarge = New System.Windows.Forms.Button()
        Me.btnSaveSettingsLarge = New System.Windows.Forms.Button()
        Me.gbSum3 = New System.Windows.Forms.GroupBox()
        Me.lblTotalIPH3 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVolume3 = New System.Windows.Forms.Label()
        Me.lblBeltTotalIsk3 = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip3 = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel3 = New System.Windows.Forms.Label()
        Me.lblIPH3 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVol3 = New System.Windows.Forms.Label()
        Me.lblHourstoFlip3 = New System.Windows.Forms.Label()
        Me.lblLargeBeltMineralComp = New System.Windows.Forms.Label()
        Me.lstOresLevel3 = New System.Windows.Forms.ListView()
        Me.checkboxLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.orenameLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.numberroidsLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.unitsLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstMineralsLevel3 = New System.Windows.Forms.ListView()
        Me.mineralsLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.minsunitsLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.totaliskLarge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblLargeBeltOreComp = New System.Windows.Forms.Label()
        Me.tabEnormous = New System.Windows.Forms.TabPage()
        Me.btnCloseXL = New System.Windows.Forms.Button()
        Me.btnSaveSettingsXLarge = New System.Windows.Forms.Button()
        Me.gbSum4 = New System.Windows.Forms.GroupBox()
        Me.lblTotalIPH4 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVolume4 = New System.Windows.Forms.Label()
        Me.lblBeltTotalIsk4 = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip4 = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel4 = New System.Windows.Forms.Label()
        Me.lblIPH4 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVol4 = New System.Windows.Forms.Label()
        Me.lblHourstoFlip4 = New System.Windows.Forms.Label()
        Me.lblXLBeltMineralComp = New System.Windows.Forms.Label()
        Me.lstOresLevel4 = New System.Windows.Forms.ListView()
        Me.checkboxXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.orenameXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.numberroidsXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.unitsXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstMineralsLevel4 = New System.Windows.Forms.ListView()
        Me.mineralsXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.minsunitsXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.totaliskXL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblXLBeltOreComp = New System.Windows.Forms.Label()
        Me.tabColossal = New System.Windows.Forms.TabPage()
        Me.btnCloseGiant = New System.Windows.Forms.Button()
        Me.btnSaveSettingsGiant = New System.Windows.Forms.Button()
        Me.gbSum5 = New System.Windows.Forms.GroupBox()
        Me.lblTotalIPH5 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVolume5 = New System.Windows.Forms.Label()
        Me.lblBeltTotalIsk5 = New System.Windows.Forms.Label()
        Me.lblTotalHourstoFlip5 = New System.Windows.Forms.Label()
        Me.lblTotalIskLevel5 = New System.Windows.Forms.Label()
        Me.lblIPH5 = New System.Windows.Forms.Label()
        Me.lblTotalBeltVol5 = New System.Windows.Forms.Label()
        Me.lblHourstoFlip5 = New System.Windows.Forms.Label()
        Me.lblGiantBeltMineralComp = New System.Windows.Forms.Label()
        Me.lstOresLevel5 = New System.Windows.Forms.ListView()
        Me.checkboxGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.orenameGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.numberroidsGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.unitsGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstMineralsLevel5 = New System.Windows.Forms.ListView()
        Me.mineralsGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.minsunitsGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.totaliskGiant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblGiantBeltOreComp = New System.Windows.Forms.Label()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbMineTaxBroker.SuspendLayout()
        Me.tabIndustryBelts.SuspendLayout()
        Me.tabSummary.SuspendLayout()
        Me.gbGiant.SuspendLayout()
        Me.gbExtraLarge.SuspendLayout()
        Me.gbLarge.SuspendLayout()
        Me.gbModerate.SuspendLayout()
        Me.gbSmall.SuspendLayout()
        Me.gbSummarySettings.SuspendLayout()
        Me.gbTruesec.SuspendLayout()
        Me.tabSmall.SuspendLayout()
        Me.gbSum1.SuspendLayout()
        Me.tabMedium.SuspendLayout()
        Me.gbSum2.SuspendLayout()
        Me.tabLarge.SuspendLayout()
        Me.gbSum3.SuspendLayout()
        Me.tabEnormous.SuspendLayout()
        Me.gbSum4.SuspendLayout()
        Me.tabColossal.SuspendLayout()
        Me.gbSum5.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstOresLevel1
        '
        Me.lstOresLevel1.CheckBoxes = True
        Me.lstOresLevel1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxSmall, Me.orenameSmall, Me.numberroidsSmall, Me.unitsSmall})
        Me.lstOresLevel1.FullRowSelect = True
        Me.lstOresLevel1.GridLines = True
        Me.lstOresLevel1.HideSelection = False
        Me.lstOresLevel1.Location = New System.Drawing.Point(6, 30)
        Me.lstOresLevel1.MultiSelect = False
        Me.lstOresLevel1.Name = "lstOresLevel1"
        Me.lstOresLevel1.Size = New System.Drawing.Size(235, 322)
        Me.lstOresLevel1.TabIndex = 6
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
        Me.lstMineralsLevel1.Location = New System.Drawing.Point(247, 30)
        Me.lstMineralsLevel1.MultiSelect = False
        Me.lstMineralsLevel1.Name = "lstMineralsLevel1"
        Me.lstMineralsLevel1.Size = New System.Drawing.Size(243, 165)
        Me.lstMineralsLevel1.TabIndex = 11
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
        Me.lblSmallBeltOreComp.Location = New System.Drawing.Point(71, 15)
        Me.lblSmallBeltOreComp.Name = "lblSmallBeltOreComp"
        Me.lblSmallBeltOreComp.Size = New System.Drawing.Size(105, 13)
        Me.lblSmallBeltOreComp.TabIndex = 12
        Me.lblSmallBeltOreComp.Text = "Belt Ore Composition"
        Me.lblSmallBeltOreComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBeltTotalIskLevel1Sum
        '
        Me.lblBeltTotalIskLevel1Sum.AutoSize = True
        Me.lblBeltTotalIskLevel1Sum.Location = New System.Drawing.Point(16, 23)
        Me.lblBeltTotalIskLevel1Sum.Name = "lblBeltTotalIskLevel1Sum"
        Me.lblBeltTotalIskLevel1Sum.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIskLevel1Sum.TabIndex = 13
        Me.lblBeltTotalIskLevel1Sum.Text = "Belt Total Isk:"
        Me.lblBeltTotalIskLevel1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIskLevel1Sum
        '
        Me.lblTotalIskLevel1Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel1Sum.Location = New System.Drawing.Point(91, 20)
        Me.lblTotalIskLevel1Sum.Name = "lblTotalIskLevel1Sum"
        Me.lblTotalIskLevel1Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel1Sum.TabIndex = 14
        Me.lblTotalIskLevel1Sum.Text = "100,000.00"
        Me.lblTotalIskLevel1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbMineTaxBroker
        '
        Me.gbMineTaxBroker.Controls.Add(Me.txtBrokerFeeRate)
        Me.gbMineTaxBroker.Controls.Add(Me.chkIPHperMiner)
        Me.gbMineTaxBroker.Controls.Add(Me.chkCompressOre)
        Me.gbMineTaxBroker.Controls.Add(Me.chkIncludeTaxes)
        Me.gbMineTaxBroker.Controls.Add(Me.chkBrokerFees)
        Me.gbMineTaxBroker.Location = New System.Drawing.Point(269, 5)
        Me.gbMineTaxBroker.Name = "gbMineTaxBroker"
        Me.gbMineTaxBroker.Size = New System.Drawing.Size(211, 55)
        Me.gbMineTaxBroker.TabIndex = 16
        Me.gbMineTaxBroker.TabStop = False
        Me.gbMineTaxBroker.Text = "Options:"
        '
        'txtBrokerFeeRate
        '
        Me.txtBrokerFeeRate.Location = New System.Drawing.Point(64, 31)
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
        Me.chkIPHperMiner.Location = New System.Drawing.Point(118, 15)
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
        Me.chkCompressOre.Location = New System.Drawing.Point(9, 15)
        Me.chkCompressOre.Name = "chkCompressOre"
        Me.chkCompressOre.Size = New System.Drawing.Size(92, 17)
        Me.chkCompressOre.TabIndex = 8
        Me.chkCompressOre.Text = "Compress Ore"
        Me.chkCompressOre.UseVisualStyleBackColor = True
        '
        'chkIncludeTaxes
        '
        Me.chkIncludeTaxes.AutoSize = True
        Me.chkIncludeTaxes.Checked = True
        Me.chkIncludeTaxes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeTaxes.Location = New System.Drawing.Point(118, 33)
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
        Me.chkBrokerFees.Location = New System.Drawing.Point(9, 33)
        Me.chkBrokerFees.Name = "chkBrokerFees"
        Me.chkBrokerFees.Size = New System.Drawing.Size(49, 17)
        Me.chkBrokerFees.TabIndex = 10
        Me.chkBrokerFees.Text = "Fees"
        Me.chkBrokerFees.ThreeState = True
        Me.chkBrokerFees.UseVisualStyleBackColor = True
        '
        'lblTotalBeltVolume1Sum
        '
        Me.lblTotalBeltVolume1Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume1Sum.Location = New System.Drawing.Point(339, 42)
        Me.lblTotalBeltVolume1Sum.Name = "lblTotalBeltVolume1Sum"
        Me.lblTotalBeltVolume1Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume1Sum.TabIndex = 47
        Me.lblTotalBeltVolume1Sum.Text = "100,000.00"
        Me.lblTotalBeltVolume1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltVolume1Sum
        '
        Me.lblBeltVolume1Sum.AutoSize = True
        Me.lblBeltVolume1Sum.Location = New System.Drawing.Point(243, 45)
        Me.lblBeltVolume1Sum.Name = "lblBeltVolume1Sum"
        Me.lblBeltVolume1Sum.Size = New System.Drawing.Size(92, 13)
        Me.lblBeltVolume1Sum.TabIndex = 46
        Me.lblBeltVolume1Sum.Text = "Total Ore Volume:"
        Me.lblBeltVolume1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip1Sum
        '
        Me.lblTotalHourstoFlip1Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip1Sum.Location = New System.Drawing.Point(91, 42)
        Me.lblTotalHourstoFlip1Sum.Name = "lblTotalHourstoFlip1Sum"
        Me.lblTotalHourstoFlip1Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip1Sum.TabIndex = 49
        Me.lblTotalHourstoFlip1Sum.Text = "100,000.00"
        Me.lblTotalHourstoFlip1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHourstoFlip1Sum
        '
        Me.lblHourstoFlip1Sum.AutoSize = True
        Me.lblHourstoFlip1Sum.Location = New System.Drawing.Point(16, 45)
        Me.lblHourstoFlip1Sum.Name = "lblHourstoFlip1Sum"
        Me.lblHourstoFlip1Sum.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip1Sum.TabIndex = 48
        Me.lblHourstoFlip1Sum.Text = "Hours to Flip:"
        Me.lblHourstoFlip1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIPH1Sum
        '
        Me.lblTotalIPH1Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIPH1Sum.Location = New System.Drawing.Point(339, 20)
        Me.lblTotalIPH1Sum.Name = "lblTotalIPH1Sum"
        Me.lblTotalIPH1Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIPH1Sum.TabIndex = 51
        Me.lblTotalIPH1Sum.Text = "100,000.00"
        Me.lblTotalIPH1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH1Sum
        '
        Me.lblIPH1Sum.AutoSize = True
        Me.lblIPH1Sum.Location = New System.Drawing.Point(268, 23)
        Me.lblIPH1Sum.Name = "lblIPH1Sum"
        Me.lblIPH1Sum.Size = New System.Drawing.Size(68, 13)
        Me.lblIPH1Sum.TabIndex = 50
        Me.lblIPH1Sum.Text = "Isk per Hour:"
        Me.lblIPH1Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(192, 588)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(113, 28)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(93, 58)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(81, 28)
        Me.btnSaveSettings.TabIndex = 4
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'lblCycleTime
        '
        Me.lblCycleTime.AutoSize = True
        Me.lblCycleTime.Location = New System.Drawing.Point(90, 16)
        Me.lblCycleTime.Name = "lblCycleTime"
        Me.lblCycleTime.Size = New System.Drawing.Size(62, 13)
        Me.lblCycleTime.TabIndex = 111
        Me.lblCycleTime.Text = "Cycle Time:"
        '
        'txtCycleTime
        '
        Me.txtCycleTime.Location = New System.Drawing.Point(93, 31)
        Me.txtCycleTime.Name = "txtCycleTime"
        Me.txtCycleTime.Size = New System.Drawing.Size(81, 20)
        Me.txtCycleTime.TabIndex = 1
        Me.txtCycleTime.Text = "104.67"
        Me.txtCycleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblm3perCycle
        '
        Me.lblm3perCycle.AutoSize = True
        Me.lblm3perCycle.Location = New System.Drawing.Point(174, 15)
        Me.lblm3perCycle.Name = "lblm3perCycle"
        Me.lblm3perCycle.Size = New System.Drawing.Size(71, 13)
        Me.lblm3perCycle.TabIndex = 113
        Me.lblm3perCycle.Text = "m3 per Cycle:"
        '
        'txtm3perCycle
        '
        Me.txtm3perCycle.Location = New System.Drawing.Point(177, 31)
        Me.txtm3perCycle.Name = "txtm3perCycle"
        Me.txtm3perCycle.Size = New System.Drawing.Size(81, 20)
        Me.txtm3perCycle.TabIndex = 2
        Me.txtm3perCycle.Text = "5833.11"
        Me.txtm3perCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblNumMiners
        '
        Me.lblNumMiners.AutoSize = True
        Me.lblNumMiners.Location = New System.Drawing.Point(9, 16)
        Me.lblNumMiners.Name = "lblNumMiners"
        Me.lblNumMiners.Size = New System.Drawing.Size(66, 13)
        Me.lblNumMiners.TabIndex = 115
        Me.lblNumMiners.Text = "Num Miners:"
        '
        'cmbNumMiners
        '
        Me.cmbNumMiners.FormattingEnabled = True
        Me.cmbNumMiners.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbNumMiners.Location = New System.Drawing.Point(9, 31)
        Me.cmbNumMiners.Name = "cmbNumMiners"
        Me.cmbNumMiners.Size = New System.Drawing.Size(81, 21)
        Me.cmbNumMiners.TabIndex = 0
        Me.cmbNumMiners.Text = "10"
        '
        'lblm3perhrperminer1
        '
        Me.lblm3perhrperminer1.AutoSize = True
        Me.lblm3perhrperminer1.Location = New System.Drawing.Point(9, 161)
        Me.lblm3perhrperminer1.Name = "lblm3perhrperminer1"
        Me.lblm3perhrperminer1.Size = New System.Drawing.Size(68, 13)
        Me.lblm3perhrperminer1.TabIndex = 121
        Me.lblm3perhrperminer1.Text = "m3/hr/miner:"
        '
        'lblm3perhrperminer
        '
        Me.lblm3perhrperminer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblm3perhrperminer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblm3perhrperminer.Location = New System.Drawing.Point(76, 158)
        Me.lblm3perhrperminer.Name = "lblm3perhrperminer"
        Me.lblm3perhrperminer.Size = New System.Drawing.Size(98, 18)
        Me.lblm3perhrperminer.TabIndex = 122
        Me.lblm3perhrperminer.Text = "999,999.00"
        Me.lblm3perhrperminer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabIndustryBelts
        '
        Me.tabIndustryBelts.Controls.Add(Me.tabSummary)
        Me.tabIndustryBelts.Controls.Add(Me.tabSmall)
        Me.tabIndustryBelts.Controls.Add(Me.tabMedium)
        Me.tabIndustryBelts.Controls.Add(Me.tabLarge)
        Me.tabIndustryBelts.Controls.Add(Me.tabEnormous)
        Me.tabIndustryBelts.Controls.Add(Me.tabColossal)
        Me.tabIndustryBelts.Location = New System.Drawing.Point(7, 12)
        Me.tabIndustryBelts.Name = "tabIndustryBelts"
        Me.tabIndustryBelts.SelectedIndex = 0
        Me.tabIndustryBelts.Size = New System.Drawing.Size(504, 647)
        Me.tabIndustryBelts.TabIndex = 139
        '
        'tabSummary
        '
        Me.tabSummary.Controls.Add(Me.gbGiant)
        Me.tabSummary.Controls.Add(Me.gbExtraLarge)
        Me.tabSummary.Controls.Add(Me.gbLarge)
        Me.tabSummary.Controls.Add(Me.gbModerate)
        Me.tabSummary.Controls.Add(Me.gbSmall)
        Me.tabSummary.Controls.Add(Me.gbSummarySettings)
        Me.tabSummary.Controls.Add(Me.btnClose)
        Me.tabSummary.Location = New System.Drawing.Point(4, 22)
        Me.tabSummary.Name = "tabSummary"
        Me.tabSummary.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSummary.Size = New System.Drawing.Size(496, 621)
        Me.tabSummary.TabIndex = 0
        Me.tabSummary.Text = "Summary"
        Me.tabSummary.UseVisualStyleBackColor = True
        '
        'gbGiant
        '
        Me.gbGiant.Controls.Add(Me.lblTotalBeltVolume4Sum)
        Me.gbGiant.Controls.Add(Me.lblTotalHourstoFlip4Sum)
        Me.gbGiant.Controls.Add(Me.lblIPH4Sum)
        Me.gbGiant.Controls.Add(Me.lblBeltVolume4Sum)
        Me.gbGiant.Controls.Add(Me.lblHourstoFlip4Sum)
        Me.gbGiant.Controls.Add(Me.lblTotalIskLevel4Sum)
        Me.gbGiant.Controls.Add(Me.lblBeltTotalIskLevel4Sum)
        Me.gbGiant.Controls.Add(Me.lblTotalIPH4Sum)
        Me.gbGiant.Location = New System.Drawing.Point(8, 430)
        Me.gbGiant.Name = "gbGiant"
        Me.gbGiant.Size = New System.Drawing.Size(484, 75)
        Me.gbGiant.TabIndex = 143
        Me.gbGiant.TabStop = False
        Me.gbGiant.Text = "Extra-Large Belt - Level 4"
        '
        'lblTotalBeltVolume4Sum
        '
        Me.lblTotalBeltVolume4Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume4Sum.Location = New System.Drawing.Point(339, 42)
        Me.lblTotalBeltVolume4Sum.Name = "lblTotalBeltVolume4Sum"
        Me.lblTotalBeltVolume4Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume4Sum.TabIndex = 47
        Me.lblTotalBeltVolume4Sum.Text = "100,000.00"
        Me.lblTotalBeltVolume4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalHourstoFlip4Sum
        '
        Me.lblTotalHourstoFlip4Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip4Sum.Location = New System.Drawing.Point(91, 42)
        Me.lblTotalHourstoFlip4Sum.Name = "lblTotalHourstoFlip4Sum"
        Me.lblTotalHourstoFlip4Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip4Sum.TabIndex = 49
        Me.lblTotalHourstoFlip4Sum.Text = "100,000.00"
        Me.lblTotalHourstoFlip4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH4Sum
        '
        Me.lblIPH4Sum.AutoSize = True
        Me.lblIPH4Sum.Location = New System.Drawing.Point(268, 23)
        Me.lblIPH4Sum.Name = "lblIPH4Sum"
        Me.lblIPH4Sum.Size = New System.Drawing.Size(68, 13)
        Me.lblIPH4Sum.TabIndex = 50
        Me.lblIPH4Sum.Text = "Isk per Hour:"
        Me.lblIPH4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBeltVolume4Sum
        '
        Me.lblBeltVolume4Sum.AutoSize = True
        Me.lblBeltVolume4Sum.Location = New System.Drawing.Point(243, 45)
        Me.lblBeltVolume4Sum.Name = "lblBeltVolume4Sum"
        Me.lblBeltVolume4Sum.Size = New System.Drawing.Size(92, 13)
        Me.lblBeltVolume4Sum.TabIndex = 46
        Me.lblBeltVolume4Sum.Text = "Total Ore Volume:"
        Me.lblBeltVolume4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip4Sum
        '
        Me.lblHourstoFlip4Sum.AutoSize = True
        Me.lblHourstoFlip4Sum.Location = New System.Drawing.Point(16, 45)
        Me.lblHourstoFlip4Sum.Name = "lblHourstoFlip4Sum"
        Me.lblHourstoFlip4Sum.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip4Sum.TabIndex = 48
        Me.lblHourstoFlip4Sum.Text = "Hours to Flip:"
        Me.lblHourstoFlip4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIskLevel4Sum
        '
        Me.lblTotalIskLevel4Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel4Sum.Location = New System.Drawing.Point(91, 20)
        Me.lblTotalIskLevel4Sum.Name = "lblTotalIskLevel4Sum"
        Me.lblTotalIskLevel4Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel4Sum.TabIndex = 14
        Me.lblTotalIskLevel4Sum.Text = "100,000.00"
        Me.lblTotalIskLevel4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIskLevel4Sum
        '
        Me.lblBeltTotalIskLevel4Sum.AutoSize = True
        Me.lblBeltTotalIskLevel4Sum.Location = New System.Drawing.Point(16, 23)
        Me.lblBeltTotalIskLevel4Sum.Name = "lblBeltTotalIskLevel4Sum"
        Me.lblBeltTotalIskLevel4Sum.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIskLevel4Sum.TabIndex = 13
        Me.lblBeltTotalIskLevel4Sum.Text = "Belt Total Isk:"
        Me.lblBeltTotalIskLevel4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIPH4Sum
        '
        Me.lblTotalIPH4Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIPH4Sum.Location = New System.Drawing.Point(339, 20)
        Me.lblTotalIPH4Sum.Name = "lblTotalIPH4Sum"
        Me.lblTotalIPH4Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIPH4Sum.TabIndex = 51
        Me.lblTotalIPH4Sum.Text = "100,000.00"
        Me.lblTotalIPH4Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbExtraLarge
        '
        Me.gbExtraLarge.Controls.Add(Me.lblTotalBeltVolume5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblTotalIPH5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblIPH5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblBeltTotalIskLevel5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblTotalHourstoFlip5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblTotalIskLevel5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblBeltVolume5Sum)
        Me.gbExtraLarge.Controls.Add(Me.lblHourstoFlip5Sum)
        Me.gbExtraLarge.Location = New System.Drawing.Point(8, 509)
        Me.gbExtraLarge.Name = "gbExtraLarge"
        Me.gbExtraLarge.Size = New System.Drawing.Size(484, 75)
        Me.gbExtraLarge.TabIndex = 142
        Me.gbExtraLarge.TabStop = False
        Me.gbExtraLarge.Text = "Giant Belt - Level 5"
        '
        'lblTotalBeltVolume5Sum
        '
        Me.lblTotalBeltVolume5Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume5Sum.Location = New System.Drawing.Point(339, 42)
        Me.lblTotalBeltVolume5Sum.Name = "lblTotalBeltVolume5Sum"
        Me.lblTotalBeltVolume5Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume5Sum.TabIndex = 47
        Me.lblTotalBeltVolume5Sum.Text = "100,000.00"
        Me.lblTotalBeltVolume5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIPH5Sum
        '
        Me.lblTotalIPH5Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIPH5Sum.Location = New System.Drawing.Point(339, 20)
        Me.lblTotalIPH5Sum.Name = "lblTotalIPH5Sum"
        Me.lblTotalIPH5Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIPH5Sum.TabIndex = 51
        Me.lblTotalIPH5Sum.Text = "100,000.00"
        Me.lblTotalIPH5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH5Sum
        '
        Me.lblIPH5Sum.AutoSize = True
        Me.lblIPH5Sum.Location = New System.Drawing.Point(269, 23)
        Me.lblIPH5Sum.Name = "lblIPH5Sum"
        Me.lblIPH5Sum.Size = New System.Drawing.Size(68, 13)
        Me.lblIPH5Sum.TabIndex = 50
        Me.lblIPH5Sum.Text = "Isk per Hour:"
        Me.lblIPH5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBeltTotalIskLevel5Sum
        '
        Me.lblBeltTotalIskLevel5Sum.AutoSize = True
        Me.lblBeltTotalIskLevel5Sum.Location = New System.Drawing.Point(16, 23)
        Me.lblBeltTotalIskLevel5Sum.Name = "lblBeltTotalIskLevel5Sum"
        Me.lblBeltTotalIskLevel5Sum.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIskLevel5Sum.TabIndex = 13
        Me.lblBeltTotalIskLevel5Sum.Text = "Belt Total Isk:"
        Me.lblBeltTotalIskLevel5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip5Sum
        '
        Me.lblTotalHourstoFlip5Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip5Sum.Location = New System.Drawing.Point(91, 42)
        Me.lblTotalHourstoFlip5Sum.Name = "lblTotalHourstoFlip5Sum"
        Me.lblTotalHourstoFlip5Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip5Sum.TabIndex = 49
        Me.lblTotalHourstoFlip5Sum.Text = "100,000.00"
        Me.lblTotalHourstoFlip5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIskLevel5Sum
        '
        Me.lblTotalIskLevel5Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel5Sum.Location = New System.Drawing.Point(91, 20)
        Me.lblTotalIskLevel5Sum.Name = "lblTotalIskLevel5Sum"
        Me.lblTotalIskLevel5Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel5Sum.TabIndex = 14
        Me.lblTotalIskLevel5Sum.Text = "100,000.00"
        Me.lblTotalIskLevel5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltVolume5Sum
        '
        Me.lblBeltVolume5Sum.AutoSize = True
        Me.lblBeltVolume5Sum.Location = New System.Drawing.Point(244, 45)
        Me.lblBeltVolume5Sum.Name = "lblBeltVolume5Sum"
        Me.lblBeltVolume5Sum.Size = New System.Drawing.Size(92, 13)
        Me.lblBeltVolume5Sum.TabIndex = 46
        Me.lblBeltVolume5Sum.Text = "Total Ore Volume:"
        Me.lblBeltVolume5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip5Sum
        '
        Me.lblHourstoFlip5Sum.AutoSize = True
        Me.lblHourstoFlip5Sum.Location = New System.Drawing.Point(16, 45)
        Me.lblHourstoFlip5Sum.Name = "lblHourstoFlip5Sum"
        Me.lblHourstoFlip5Sum.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip5Sum.TabIndex = 48
        Me.lblHourstoFlip5Sum.Text = "Hours to Flip:"
        Me.lblHourstoFlip5Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbLarge
        '
        Me.gbLarge.Controls.Add(Me.lblTotalBeltVolume3Sum)
        Me.gbLarge.Controls.Add(Me.lblIPH3Sum)
        Me.gbLarge.Controls.Add(Me.lblTotalHourstoFlip3Sum)
        Me.gbLarge.Controls.Add(Me.lblTotalIPH3Sum)
        Me.gbLarge.Controls.Add(Me.lblHourstoFlip3Sum)
        Me.gbLarge.Controls.Add(Me.lblBeltVolume3Sum)
        Me.gbLarge.Controls.Add(Me.lblTotalIskLevel3Sum)
        Me.gbLarge.Controls.Add(Me.lblBeltTotalIskLevel3Sum)
        Me.gbLarge.Location = New System.Drawing.Point(8, 351)
        Me.gbLarge.Name = "gbLarge"
        Me.gbLarge.Size = New System.Drawing.Size(484, 75)
        Me.gbLarge.TabIndex = 142
        Me.gbLarge.TabStop = False
        Me.gbLarge.Text = "Large Belt - Level 3"
        '
        'lblTotalBeltVolume3Sum
        '
        Me.lblTotalBeltVolume3Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume3Sum.Location = New System.Drawing.Point(339, 42)
        Me.lblTotalBeltVolume3Sum.Name = "lblTotalBeltVolume3Sum"
        Me.lblTotalBeltVolume3Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume3Sum.TabIndex = 47
        Me.lblTotalBeltVolume3Sum.Text = "100,000.00"
        Me.lblTotalBeltVolume3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH3Sum
        '
        Me.lblIPH3Sum.AutoSize = True
        Me.lblIPH3Sum.Location = New System.Drawing.Point(268, 23)
        Me.lblIPH3Sum.Name = "lblIPH3Sum"
        Me.lblIPH3Sum.Size = New System.Drawing.Size(68, 13)
        Me.lblIPH3Sum.TabIndex = 50
        Me.lblIPH3Sum.Text = "Isk per Hour:"
        Me.lblIPH3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip3Sum
        '
        Me.lblTotalHourstoFlip3Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip3Sum.Location = New System.Drawing.Point(91, 42)
        Me.lblTotalHourstoFlip3Sum.Name = "lblTotalHourstoFlip3Sum"
        Me.lblTotalHourstoFlip3Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip3Sum.TabIndex = 49
        Me.lblTotalHourstoFlip3Sum.Text = "100,000.00"
        Me.lblTotalHourstoFlip3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIPH3Sum
        '
        Me.lblTotalIPH3Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIPH3Sum.Location = New System.Drawing.Point(339, 20)
        Me.lblTotalIPH3Sum.Name = "lblTotalIPH3Sum"
        Me.lblTotalIPH3Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIPH3Sum.TabIndex = 51
        Me.lblTotalIPH3Sum.Text = "100,000.00"
        Me.lblTotalIPH3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHourstoFlip3Sum
        '
        Me.lblHourstoFlip3Sum.AutoSize = True
        Me.lblHourstoFlip3Sum.Location = New System.Drawing.Point(16, 45)
        Me.lblHourstoFlip3Sum.Name = "lblHourstoFlip3Sum"
        Me.lblHourstoFlip3Sum.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip3Sum.TabIndex = 48
        Me.lblHourstoFlip3Sum.Text = "Hours to Flip:"
        Me.lblHourstoFlip3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBeltVolume3Sum
        '
        Me.lblBeltVolume3Sum.AutoSize = True
        Me.lblBeltVolume3Sum.Location = New System.Drawing.Point(243, 45)
        Me.lblBeltVolume3Sum.Name = "lblBeltVolume3Sum"
        Me.lblBeltVolume3Sum.Size = New System.Drawing.Size(92, 13)
        Me.lblBeltVolume3Sum.TabIndex = 46
        Me.lblBeltVolume3Sum.Text = "Total Ore Volume:"
        Me.lblBeltVolume3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIskLevel3Sum
        '
        Me.lblTotalIskLevel3Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel3Sum.Location = New System.Drawing.Point(91, 20)
        Me.lblTotalIskLevel3Sum.Name = "lblTotalIskLevel3Sum"
        Me.lblTotalIskLevel3Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel3Sum.TabIndex = 14
        Me.lblTotalIskLevel3Sum.Text = "100,000.00"
        Me.lblTotalIskLevel3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIskLevel3Sum
        '
        Me.lblBeltTotalIskLevel3Sum.AutoSize = True
        Me.lblBeltTotalIskLevel3Sum.Location = New System.Drawing.Point(16, 23)
        Me.lblBeltTotalIskLevel3Sum.Name = "lblBeltTotalIskLevel3Sum"
        Me.lblBeltTotalIskLevel3Sum.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIskLevel3Sum.TabIndex = 13
        Me.lblBeltTotalIskLevel3Sum.Text = "Belt Total Isk:"
        Me.lblBeltTotalIskLevel3Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbModerate
        '
        Me.gbModerate.Controls.Add(Me.lblTotalBeltVolume2Sum)
        Me.gbModerate.Controls.Add(Me.lblIPH2Sum)
        Me.gbModerate.Controls.Add(Me.lblTotalHourstoFlip2Sum)
        Me.gbModerate.Controls.Add(Me.lblTotalIPH2Sum)
        Me.gbModerate.Controls.Add(Me.lblHourstoFlip2Sum)
        Me.gbModerate.Controls.Add(Me.lblBeltVolume2Sum)
        Me.gbModerate.Controls.Add(Me.lblTotalIskLevel2Sum)
        Me.gbModerate.Controls.Add(Me.lblBeltTotalIskLevel2Sum)
        Me.gbModerate.Location = New System.Drawing.Point(8, 272)
        Me.gbModerate.Name = "gbModerate"
        Me.gbModerate.Size = New System.Drawing.Size(484, 75)
        Me.gbModerate.TabIndex = 141
        Me.gbModerate.TabStop = False
        Me.gbModerate.Text = "Moderate Belt - Level 2"
        '
        'lblTotalBeltVolume2Sum
        '
        Me.lblTotalBeltVolume2Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume2Sum.Location = New System.Drawing.Point(339, 42)
        Me.lblTotalBeltVolume2Sum.Name = "lblTotalBeltVolume2Sum"
        Me.lblTotalBeltVolume2Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume2Sum.TabIndex = 47
        Me.lblTotalBeltVolume2Sum.Text = "100,000.00"
        Me.lblTotalBeltVolume2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH2Sum
        '
        Me.lblIPH2Sum.AutoSize = True
        Me.lblIPH2Sum.Location = New System.Drawing.Point(268, 23)
        Me.lblIPH2Sum.Name = "lblIPH2Sum"
        Me.lblIPH2Sum.Size = New System.Drawing.Size(68, 13)
        Me.lblIPH2Sum.TabIndex = 50
        Me.lblIPH2Sum.Text = "Isk per Hour:"
        Me.lblIPH2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip2Sum
        '
        Me.lblTotalHourstoFlip2Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip2Sum.Location = New System.Drawing.Point(91, 42)
        Me.lblTotalHourstoFlip2Sum.Name = "lblTotalHourstoFlip2Sum"
        Me.lblTotalHourstoFlip2Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip2Sum.TabIndex = 49
        Me.lblTotalHourstoFlip2Sum.Text = "100,000.00"
        Me.lblTotalHourstoFlip2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIPH2Sum
        '
        Me.lblTotalIPH2Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIPH2Sum.Location = New System.Drawing.Point(339, 20)
        Me.lblTotalIPH2Sum.Name = "lblTotalIPH2Sum"
        Me.lblTotalIPH2Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIPH2Sum.TabIndex = 51
        Me.lblTotalIPH2Sum.Text = "100,000.00"
        Me.lblTotalIPH2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHourstoFlip2Sum
        '
        Me.lblHourstoFlip2Sum.AutoSize = True
        Me.lblHourstoFlip2Sum.Location = New System.Drawing.Point(16, 45)
        Me.lblHourstoFlip2Sum.Name = "lblHourstoFlip2Sum"
        Me.lblHourstoFlip2Sum.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip2Sum.TabIndex = 48
        Me.lblHourstoFlip2Sum.Text = "Hours to Flip:"
        Me.lblHourstoFlip2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBeltVolume2Sum
        '
        Me.lblBeltVolume2Sum.AutoSize = True
        Me.lblBeltVolume2Sum.Location = New System.Drawing.Point(243, 45)
        Me.lblBeltVolume2Sum.Name = "lblBeltVolume2Sum"
        Me.lblBeltVolume2Sum.Size = New System.Drawing.Size(92, 13)
        Me.lblBeltVolume2Sum.TabIndex = 46
        Me.lblBeltVolume2Sum.Text = "Total Ore Volume:"
        Me.lblBeltVolume2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIskLevel2Sum
        '
        Me.lblTotalIskLevel2Sum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel2Sum.Location = New System.Drawing.Point(91, 20)
        Me.lblTotalIskLevel2Sum.Name = "lblTotalIskLevel2Sum"
        Me.lblTotalIskLevel2Sum.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel2Sum.TabIndex = 14
        Me.lblTotalIskLevel2Sum.Text = "100,000.00"
        Me.lblTotalIskLevel2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIskLevel2Sum
        '
        Me.lblBeltTotalIskLevel2Sum.AutoSize = True
        Me.lblBeltTotalIskLevel2Sum.Location = New System.Drawing.Point(16, 23)
        Me.lblBeltTotalIskLevel2Sum.Name = "lblBeltTotalIskLevel2Sum"
        Me.lblBeltTotalIskLevel2Sum.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIskLevel2Sum.TabIndex = 13
        Me.lblBeltTotalIskLevel2Sum.Text = "Belt Total Isk:"
        Me.lblBeltTotalIskLevel2Sum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbSmall
        '
        Me.gbSmall.Controls.Add(Me.lblTotalBeltVolume1Sum)
        Me.gbSmall.Controls.Add(Me.lblIPH1Sum)
        Me.gbSmall.Controls.Add(Me.lblTotalHourstoFlip1Sum)
        Me.gbSmall.Controls.Add(Me.lblTotalIPH1Sum)
        Me.gbSmall.Controls.Add(Me.lblHourstoFlip1Sum)
        Me.gbSmall.Controls.Add(Me.lblBeltVolume1Sum)
        Me.gbSmall.Controls.Add(Me.lblTotalIskLevel1Sum)
        Me.gbSmall.Controls.Add(Me.lblBeltTotalIskLevel1Sum)
        Me.gbSmall.Location = New System.Drawing.Point(6, 193)
        Me.gbSmall.Name = "gbSmall"
        Me.gbSmall.Size = New System.Drawing.Size(484, 75)
        Me.gbSmall.TabIndex = 140
        Me.gbSmall.TabStop = False
        Me.gbSmall.Text = "Small Belt - Level 1"
        '
        'gbSummarySettings
        '
        Me.gbSummarySettings.Controls.Add(Me.ReprocessingFacility)
        Me.gbSummarySettings.Controls.Add(Me.gbTruesec)
        Me.gbSummarySettings.Controls.Add(Me.btnRefine)
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
        Me.gbSummarySettings.Location = New System.Drawing.Point(6, 6)
        Me.gbSummarySettings.Name = "gbSummarySettings"
        Me.gbSummarySettings.Size = New System.Drawing.Size(484, 183)
        Me.gbSummarySettings.TabIndex = 139
        Me.gbSummarySettings.TabStop = False
        Me.gbSummarySettings.Text = "Settings"
        '
        'ReprocessingFacility
        '
        Me.ReprocessingFacility.BackColor = System.Drawing.Color.Transparent
        Me.ReprocessingFacility.Location = New System.Drawing.Point(177, 66)
        Me.ReprocessingFacility.Name = "ReprocessingFacility"
        Me.ReprocessingFacility.Size = New System.Drawing.Size(303, 108)
        Me.ReprocessingFacility.TabIndex = 124
        '
        'gbTruesec
        '
        Me.gbTruesec.Controls.Add(Me.rbtn10percent)
        Me.gbTruesec.Controls.Add(Me.rbtn0percent)
        Me.gbTruesec.Controls.Add(Me.rbtn5percent)
        Me.gbTruesec.Location = New System.Drawing.Point(9, 91)
        Me.gbTruesec.Name = "gbTruesec"
        Me.gbTruesec.Size = New System.Drawing.Size(165, 64)
        Me.gbTruesec.TabIndex = 128
        Me.gbTruesec.TabStop = False
        Me.gbTruesec.Text = "System Truesec:"
        '
        'rbtn10percent
        '
        Me.rbtn10percent.AutoSize = True
        Me.rbtn10percent.Location = New System.Drawing.Point(12, 43)
        Me.rbtn10percent.Name = "rbtn10percent"
        Me.rbtn10percent.Size = New System.Drawing.Size(88, 17)
        Me.rbtn10percent.TabIndex = 127
        Me.rbtn10percent.TabStop = True
        Me.rbtn10percent.Text = "-0.85 to -1.00"
        Me.rbtn10percent.UseVisualStyleBackColor = True
        '
        'rbtn0percent
        '
        Me.rbtn0percent.AutoSize = True
        Me.rbtn0percent.Location = New System.Drawing.Point(12, 13)
        Me.rbtn0percent.Name = "rbtn0percent"
        Me.rbtn0percent.Size = New System.Drawing.Size(88, 17)
        Me.rbtn0percent.TabIndex = 125
        Me.rbtn0percent.TabStop = True
        Me.rbtn0percent.Text = "-0.00 to -0.44"
        Me.rbtn0percent.UseVisualStyleBackColor = True
        '
        'rbtn5percent
        '
        Me.rbtn5percent.AutoSize = True
        Me.rbtn5percent.Location = New System.Drawing.Point(12, 28)
        Me.rbtn5percent.Name = "rbtn5percent"
        Me.rbtn5percent.Size = New System.Drawing.Size(88, 17)
        Me.rbtn5percent.TabIndex = 126
        Me.rbtn5percent.TabStop = True
        Me.rbtn5percent.Text = "-0.45 to -0.84"
        Me.rbtn5percent.UseVisualStyleBackColor = True
        '
        'btnRefine
        '
        Me.btnRefine.Location = New System.Drawing.Point(9, 58)
        Me.btnRefine.Name = "btnRefine"
        Me.btnRefine.Size = New System.Drawing.Size(81, 28)
        Me.btnRefine.TabIndex = 3
        Me.btnRefine.Text = "Refine"
        Me.btnRefine.UseVisualStyleBackColor = True
        '
        'tabSmall
        '
        Me.tabSmall.Controls.Add(Me.btnCloseSmall)
        Me.tabSmall.Controls.Add(Me.btnSaveSettingsSmall)
        Me.tabSmall.Controls.Add(Me.gbSum1)
        Me.tabSmall.Controls.Add(Me.lblSmallBeltMineralComp)
        Me.tabSmall.Controls.Add(Me.lstOresLevel1)
        Me.tabSmall.Controls.Add(Me.lstMineralsLevel1)
        Me.tabSmall.Controls.Add(Me.lblSmallBeltOreComp)
        Me.tabSmall.Location = New System.Drawing.Point(4, 22)
        Me.tabSmall.Name = "tabSmall"
        Me.tabSmall.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSmall.Size = New System.Drawing.Size(496, 621)
        Me.tabSmall.TabIndex = 1
        Me.tabSmall.Text = "Small Belt"
        Me.tabSmall.UseVisualStyleBackColor = True
        '
        'btnCloseSmall
        '
        Me.btnCloseSmall.Location = New System.Drawing.Point(374, 324)
        Me.btnCloseSmall.Name = "btnCloseSmall"
        Me.btnCloseSmall.Size = New System.Drawing.Size(113, 28)
        Me.btnCloseSmall.TabIndex = 14
        Me.btnCloseSmall.Text = "Close"
        Me.btnCloseSmall.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsSmall
        '
        Me.btnSaveSettingsSmall.Location = New System.Drawing.Point(248, 324)
        Me.btnSaveSettingsSmall.Name = "btnSaveSettingsSmall"
        Me.btnSaveSettingsSmall.Size = New System.Drawing.Size(113, 28)
        Me.btnSaveSettingsSmall.TabIndex = 13
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
        Me.gbSum1.Location = New System.Drawing.Point(247, 201)
        Me.gbSum1.Name = "gbSum1"
        Me.gbSum1.Size = New System.Drawing.Size(243, 117)
        Me.gbSum1.TabIndex = 77
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
        Me.lblSmallBeltMineralComp.Location = New System.Drawing.Point(307, 15)
        Me.lblSmallBeltMineralComp.Name = "lblSmallBeltMineralComp"
        Me.lblSmallBeltMineralComp.Size = New System.Drawing.Size(122, 13)
        Me.lblSmallBeltMineralComp.TabIndex = 60
        Me.lblSmallBeltMineralComp.Text = "Belt Mineral Composition"
        Me.lblSmallBeltMineralComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tabMedium
        '
        Me.tabMedium.Controls.Add(Me.btnCloseMedium)
        Me.tabMedium.Controls.Add(Me.btnSaveSettingsMedium)
        Me.tabMedium.Controls.Add(Me.gbSum2)
        Me.tabMedium.Controls.Add(Me.lstMineralsLevel2)
        Me.tabMedium.Controls.Add(Me.lstOresLevel2)
        Me.tabMedium.Controls.Add(Me.lblMediumBeltMineralComp)
        Me.tabMedium.Controls.Add(Me.lblMediumBeltOreComp)
        Me.tabMedium.Location = New System.Drawing.Point(4, 22)
        Me.tabMedium.Name = "tabMedium"
        Me.tabMedium.Size = New System.Drawing.Size(496, 621)
        Me.tabMedium.TabIndex = 2
        Me.tabMedium.Text = "Medium Belt"
        Me.tabMedium.UseVisualStyleBackColor = True
        '
        'btnCloseMedium
        '
        Me.btnCloseMedium.Location = New System.Drawing.Point(374, 324)
        Me.btnCloseMedium.Name = "btnCloseMedium"
        Me.btnCloseMedium.Size = New System.Drawing.Size(113, 28)
        Me.btnCloseMedium.TabIndex = 16
        Me.btnCloseMedium.Text = "Close"
        Me.btnCloseMedium.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsMedium
        '
        Me.btnSaveSettingsMedium.Location = New System.Drawing.Point(248, 324)
        Me.btnSaveSettingsMedium.Name = "btnSaveSettingsMedium"
        Me.btnSaveSettingsMedium.Size = New System.Drawing.Size(113, 28)
        Me.btnSaveSettingsMedium.TabIndex = 15
        Me.btnSaveSettingsMedium.Text = "Save Selected Ores"
        Me.btnSaveSettingsMedium.UseVisualStyleBackColor = True
        '
        'gbSum2
        '
        Me.gbSum2.Controls.Add(Me.lblTotalIPH2)
        Me.gbSum2.Controls.Add(Me.lblTotalBeltVolume2)
        Me.gbSum2.Controls.Add(Me.lblBeltTotalIsk2)
        Me.gbSum2.Controls.Add(Me.lblTotalHourstoFlip2)
        Me.gbSum2.Controls.Add(Me.lblTotalIskLevel2)
        Me.gbSum2.Controls.Add(Me.lblIPH2)
        Me.gbSum2.Controls.Add(Me.lblTotalBeltVol2)
        Me.gbSum2.Controls.Add(Me.lblHourstoFlip2)
        Me.gbSum2.Location = New System.Drawing.Point(247, 201)
        Me.gbSum2.Name = "gbSum2"
        Me.gbSum2.Size = New System.Drawing.Size(243, 117)
        Me.gbSum2.TabIndex = 77
        Me.gbSum2.TabStop = False
        '
        'lblTotalIPH2
        '
        Me.lblTotalIPH2.AutoSize = True
        Me.lblTotalIPH2.Location = New System.Drawing.Point(18, 65)
        Me.lblTotalIPH2.Name = "lblTotalIPH2"
        Me.lblTotalIPH2.Size = New System.Drawing.Size(68, 13)
        Me.lblTotalIPH2.TabIndex = 61
        Me.lblTotalIPH2.Text = "Isk per Hour:"
        Me.lblTotalIPH2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBeltVolume2
        '
        Me.lblTotalBeltVolume2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume2.Location = New System.Drawing.Point(97, 84)
        Me.lblTotalBeltVolume2.Name = "lblTotalBeltVolume2"
        Me.lblTotalBeltVolume2.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume2.TabIndex = 60
        Me.lblTotalBeltVolume2.Text = "100,000.00"
        Me.lblTotalBeltVolume2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIsk2
        '
        Me.lblBeltTotalIsk2.AutoSize = True
        Me.lblBeltTotalIsk2.Location = New System.Drawing.Point(18, 21)
        Me.lblBeltTotalIsk2.Name = "lblBeltTotalIsk2"
        Me.lblBeltTotalIsk2.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIsk2.TabIndex = 52
        Me.lblBeltTotalIsk2.Text = "Belt Total Isk:"
        Me.lblBeltTotalIsk2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip2
        '
        Me.lblTotalHourstoFlip2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip2.Location = New System.Drawing.Point(97, 40)
        Me.lblTotalHourstoFlip2.Name = "lblTotalHourstoFlip2"
        Me.lblTotalHourstoFlip2.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip2.TabIndex = 57
        Me.lblTotalHourstoFlip2.Text = "100,000.00"
        Me.lblTotalHourstoFlip2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIskLevel2
        '
        Me.lblTotalIskLevel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel2.Location = New System.Drawing.Point(97, 18)
        Me.lblTotalIskLevel2.Name = "lblTotalIskLevel2"
        Me.lblTotalIskLevel2.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel2.TabIndex = 53
        Me.lblTotalIskLevel2.Text = "100,000.00"
        Me.lblTotalIskLevel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH2
        '
        Me.lblIPH2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIPH2.Location = New System.Drawing.Point(97, 62)
        Me.lblIPH2.Name = "lblIPH2"
        Me.lblIPH2.Size = New System.Drawing.Size(127, 18)
        Me.lblIPH2.TabIndex = 59
        Me.lblIPH2.Text = "100,000.00"
        Me.lblIPH2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltVol2
        '
        Me.lblTotalBeltVol2.AutoSize = True
        Me.lblTotalBeltVol2.Location = New System.Drawing.Point(18, 87)
        Me.lblTotalBeltVol2.Name = "lblTotalBeltVol2"
        Me.lblTotalBeltVol2.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalBeltVol2.TabIndex = 54
        Me.lblTotalBeltVol2.Text = "Belt Total Vol:"
        Me.lblTotalBeltVol2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip2
        '
        Me.lblHourstoFlip2.AutoSize = True
        Me.lblHourstoFlip2.Location = New System.Drawing.Point(18, 43)
        Me.lblHourstoFlip2.Name = "lblHourstoFlip2"
        Me.lblHourstoFlip2.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip2.TabIndex = 56
        Me.lblHourstoFlip2.Text = "Hours to Flip:"
        Me.lblHourstoFlip2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstMineralsLevel2
        '
        Me.lstMineralsLevel2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.mineralsMedium, Me.minsunitsMedium, Me.totaliskMedium})
        Me.lstMineralsLevel2.FullRowSelect = True
        Me.lstMineralsLevel2.GridLines = True
        Me.lstMineralsLevel2.HideSelection = False
        Me.lstMineralsLevel2.Location = New System.Drawing.Point(247, 30)
        Me.lstMineralsLevel2.MultiSelect = False
        Me.lstMineralsLevel2.Name = "lstMineralsLevel2"
        Me.lstMineralsLevel2.Size = New System.Drawing.Size(243, 165)
        Me.lstMineralsLevel2.TabIndex = 64
        Me.lstMineralsLevel2.UseCompatibleStateImageBehavior = False
        Me.lstMineralsLevel2.View = System.Windows.Forms.View.Details
        '
        'mineralsMedium
        '
        Me.mineralsMedium.Text = "Mineral"
        Me.mineralsMedium.Width = 57
        '
        'minsunitsMedium
        '
        Me.minsunitsMedium.Text = "Units"
        Me.minsunitsMedium.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.minsunitsMedium.Width = 79
        '
        'totaliskMedium
        '
        Me.totaliskMedium.Text = "Total Isk"
        Me.totaliskMedium.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.totaliskMedium.Width = 103
        '
        'lstOresLevel2
        '
        Me.lstOresLevel2.CheckBoxes = True
        Me.lstOresLevel2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxMedium, Me.orenameMedium, Me.numberroidsMedium, Me.unitsMedium})
        Me.lstOresLevel2.FullRowSelect = True
        Me.lstOresLevel2.GridLines = True
        Me.lstOresLevel2.HideSelection = False
        Me.lstOresLevel2.Location = New System.Drawing.Point(6, 30)
        Me.lstOresLevel2.MultiSelect = False
        Me.lstOresLevel2.Name = "lstOresLevel2"
        Me.lstOresLevel2.Size = New System.Drawing.Size(235, 322)
        Me.lstOresLevel2.TabIndex = 63
        Me.lstOresLevel2.UseCompatibleStateImageBehavior = False
        Me.lstOresLevel2.View = System.Windows.Forms.View.Details
        '
        'checkboxMedium
        '
        Me.checkboxMedium.Text = ""
        Me.checkboxMedium.Width = 25
        '
        'orenameMedium
        '
        Me.orenameMedium.Text = "Ore"
        Me.orenameMedium.Width = 100
        '
        'numberroidsMedium
        '
        Me.numberroidsMedium.Text = "Rocks"
        Me.numberroidsMedium.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numberroidsMedium.Width = 43
        '
        'unitsMedium
        '
        Me.unitsMedium.Text = "Units"
        Me.unitsMedium.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.unitsMedium.Width = 63
        '
        'lblMediumBeltMineralComp
        '
        Me.lblMediumBeltMineralComp.AutoSize = True
        Me.lblMediumBeltMineralComp.Location = New System.Drawing.Point(307, 15)
        Me.lblMediumBeltMineralComp.Name = "lblMediumBeltMineralComp"
        Me.lblMediumBeltMineralComp.Size = New System.Drawing.Size(122, 13)
        Me.lblMediumBeltMineralComp.TabIndex = 62
        Me.lblMediumBeltMineralComp.Text = "Belt Mineral Composition"
        Me.lblMediumBeltMineralComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblMediumBeltOreComp
        '
        Me.lblMediumBeltOreComp.AutoSize = True
        Me.lblMediumBeltOreComp.Location = New System.Drawing.Point(71, 15)
        Me.lblMediumBeltOreComp.Name = "lblMediumBeltOreComp"
        Me.lblMediumBeltOreComp.Size = New System.Drawing.Size(105, 13)
        Me.lblMediumBeltOreComp.TabIndex = 61
        Me.lblMediumBeltOreComp.Text = "Belt Ore Composition"
        Me.lblMediumBeltOreComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tabLarge
        '
        Me.tabLarge.Controls.Add(Me.btnCloseLarge)
        Me.tabLarge.Controls.Add(Me.btnSaveSettingsLarge)
        Me.tabLarge.Controls.Add(Me.gbSum3)
        Me.tabLarge.Controls.Add(Me.lblLargeBeltMineralComp)
        Me.tabLarge.Controls.Add(Me.lstOresLevel3)
        Me.tabLarge.Controls.Add(Me.lstMineralsLevel3)
        Me.tabLarge.Controls.Add(Me.lblLargeBeltOreComp)
        Me.tabLarge.Location = New System.Drawing.Point(4, 22)
        Me.tabLarge.Name = "tabLarge"
        Me.tabLarge.Size = New System.Drawing.Size(496, 621)
        Me.tabLarge.TabIndex = 3
        Me.tabLarge.Text = "Large Belt"
        Me.tabLarge.UseVisualStyleBackColor = True
        '
        'btnCloseLarge
        '
        Me.btnCloseLarge.Location = New System.Drawing.Point(374, 324)
        Me.btnCloseLarge.Name = "btnCloseLarge"
        Me.btnCloseLarge.Size = New System.Drawing.Size(113, 28)
        Me.btnCloseLarge.TabIndex = 18
        Me.btnCloseLarge.Text = "Close"
        Me.btnCloseLarge.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsLarge
        '
        Me.btnSaveSettingsLarge.Location = New System.Drawing.Point(248, 324)
        Me.btnSaveSettingsLarge.Name = "btnSaveSettingsLarge"
        Me.btnSaveSettingsLarge.Size = New System.Drawing.Size(113, 28)
        Me.btnSaveSettingsLarge.TabIndex = 17
        Me.btnSaveSettingsLarge.Text = "Save Selected Ores"
        Me.btnSaveSettingsLarge.UseVisualStyleBackColor = True
        '
        'gbSum3
        '
        Me.gbSum3.Controls.Add(Me.lblTotalIPH3)
        Me.gbSum3.Controls.Add(Me.lblTotalBeltVolume3)
        Me.gbSum3.Controls.Add(Me.lblBeltTotalIsk3)
        Me.gbSum3.Controls.Add(Me.lblTotalHourstoFlip3)
        Me.gbSum3.Controls.Add(Me.lblTotalIskLevel3)
        Me.gbSum3.Controls.Add(Me.lblIPH3)
        Me.gbSum3.Controls.Add(Me.lblTotalBeltVol3)
        Me.gbSum3.Controls.Add(Me.lblHourstoFlip3)
        Me.gbSum3.Location = New System.Drawing.Point(247, 201)
        Me.gbSum3.Name = "gbSum3"
        Me.gbSum3.Size = New System.Drawing.Size(243, 117)
        Me.gbSum3.TabIndex = 77
        Me.gbSum3.TabStop = False
        '
        'lblTotalIPH3
        '
        Me.lblTotalIPH3.AutoSize = True
        Me.lblTotalIPH3.Location = New System.Drawing.Point(18, 65)
        Me.lblTotalIPH3.Name = "lblTotalIPH3"
        Me.lblTotalIPH3.Size = New System.Drawing.Size(68, 13)
        Me.lblTotalIPH3.TabIndex = 61
        Me.lblTotalIPH3.Text = "Isk per Hour:"
        Me.lblTotalIPH3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBeltVolume3
        '
        Me.lblTotalBeltVolume3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume3.Location = New System.Drawing.Point(97, 84)
        Me.lblTotalBeltVolume3.Name = "lblTotalBeltVolume3"
        Me.lblTotalBeltVolume3.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume3.TabIndex = 60
        Me.lblTotalBeltVolume3.Text = "100,000.00"
        Me.lblTotalBeltVolume3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIsk3
        '
        Me.lblBeltTotalIsk3.AutoSize = True
        Me.lblBeltTotalIsk3.Location = New System.Drawing.Point(18, 21)
        Me.lblBeltTotalIsk3.Name = "lblBeltTotalIsk3"
        Me.lblBeltTotalIsk3.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIsk3.TabIndex = 52
        Me.lblBeltTotalIsk3.Text = "Belt Total Isk:"
        Me.lblBeltTotalIsk3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip3
        '
        Me.lblTotalHourstoFlip3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip3.Location = New System.Drawing.Point(97, 40)
        Me.lblTotalHourstoFlip3.Name = "lblTotalHourstoFlip3"
        Me.lblTotalHourstoFlip3.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip3.TabIndex = 57
        Me.lblTotalHourstoFlip3.Text = "100,000.00"
        Me.lblTotalHourstoFlip3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIskLevel3
        '
        Me.lblTotalIskLevel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel3.Location = New System.Drawing.Point(97, 18)
        Me.lblTotalIskLevel3.Name = "lblTotalIskLevel3"
        Me.lblTotalIskLevel3.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel3.TabIndex = 53
        Me.lblTotalIskLevel3.Text = "100,000.00"
        Me.lblTotalIskLevel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH3
        '
        Me.lblIPH3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIPH3.Location = New System.Drawing.Point(97, 62)
        Me.lblIPH3.Name = "lblIPH3"
        Me.lblIPH3.Size = New System.Drawing.Size(127, 18)
        Me.lblIPH3.TabIndex = 59
        Me.lblIPH3.Text = "100,000.00"
        Me.lblIPH3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltVol3
        '
        Me.lblTotalBeltVol3.AutoSize = True
        Me.lblTotalBeltVol3.Location = New System.Drawing.Point(18, 87)
        Me.lblTotalBeltVol3.Name = "lblTotalBeltVol3"
        Me.lblTotalBeltVol3.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalBeltVol3.TabIndex = 54
        Me.lblTotalBeltVol3.Text = "Belt Total Vol:"
        Me.lblTotalBeltVol3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip3
        '
        Me.lblHourstoFlip3.AutoSize = True
        Me.lblHourstoFlip3.Location = New System.Drawing.Point(18, 43)
        Me.lblHourstoFlip3.Name = "lblHourstoFlip3"
        Me.lblHourstoFlip3.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip3.TabIndex = 56
        Me.lblHourstoFlip3.Text = "Hours to Flip:"
        Me.lblHourstoFlip3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLargeBeltMineralComp
        '
        Me.lblLargeBeltMineralComp.AutoSize = True
        Me.lblLargeBeltMineralComp.Location = New System.Drawing.Point(307, 15)
        Me.lblLargeBeltMineralComp.Name = "lblLargeBeltMineralComp"
        Me.lblLargeBeltMineralComp.Size = New System.Drawing.Size(122, 13)
        Me.lblLargeBeltMineralComp.TabIndex = 65
        Me.lblLargeBeltMineralComp.Text = "Belt Mineral Composition"
        Me.lblLargeBeltMineralComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstOresLevel3
        '
        Me.lstOresLevel3.CheckBoxes = True
        Me.lstOresLevel3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxLarge, Me.orenameLarge, Me.numberroidsLarge, Me.unitsLarge})
        Me.lstOresLevel3.FullRowSelect = True
        Me.lstOresLevel3.GridLines = True
        Me.lstOresLevel3.HideSelection = False
        Me.lstOresLevel3.Location = New System.Drawing.Point(6, 30)
        Me.lstOresLevel3.MultiSelect = False
        Me.lstOresLevel3.Name = "lstOresLevel3"
        Me.lstOresLevel3.Size = New System.Drawing.Size(235, 322)
        Me.lstOresLevel3.TabIndex = 62
        Me.lstOresLevel3.UseCompatibleStateImageBehavior = False
        Me.lstOresLevel3.View = System.Windows.Forms.View.Details
        '
        'checkboxLarge
        '
        Me.checkboxLarge.Text = ""
        Me.checkboxLarge.Width = 25
        '
        'orenameLarge
        '
        Me.orenameLarge.Text = "Ore"
        Me.orenameLarge.Width = 100
        '
        'numberroidsLarge
        '
        Me.numberroidsLarge.Text = "Rocks"
        Me.numberroidsLarge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numberroidsLarge.Width = 43
        '
        'unitsLarge
        '
        Me.unitsLarge.Text = "Units"
        Me.unitsLarge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.unitsLarge.Width = 63
        '
        'lstMineralsLevel3
        '
        Me.lstMineralsLevel3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.mineralsLarge, Me.minsunitsLarge, Me.totaliskLarge})
        Me.lstMineralsLevel3.FullRowSelect = True
        Me.lstMineralsLevel3.GridLines = True
        Me.lstMineralsLevel3.HideSelection = False
        Me.lstMineralsLevel3.Location = New System.Drawing.Point(247, 30)
        Me.lstMineralsLevel3.MultiSelect = False
        Me.lstMineralsLevel3.Name = "lstMineralsLevel3"
        Me.lstMineralsLevel3.Size = New System.Drawing.Size(243, 165)
        Me.lstMineralsLevel3.TabIndex = 63
        Me.lstMineralsLevel3.UseCompatibleStateImageBehavior = False
        Me.lstMineralsLevel3.View = System.Windows.Forms.View.Details
        '
        'mineralsLarge
        '
        Me.mineralsLarge.Text = "Mineral"
        Me.mineralsLarge.Width = 57
        '
        'minsunitsLarge
        '
        Me.minsunitsLarge.Text = "Units"
        Me.minsunitsLarge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.minsunitsLarge.Width = 79
        '
        'totaliskLarge
        '
        Me.totaliskLarge.Text = "Total Isk"
        Me.totaliskLarge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.totaliskLarge.Width = 103
        '
        'lblLargeBeltOreComp
        '
        Me.lblLargeBeltOreComp.AutoSize = True
        Me.lblLargeBeltOreComp.Location = New System.Drawing.Point(71, 15)
        Me.lblLargeBeltOreComp.Name = "lblLargeBeltOreComp"
        Me.lblLargeBeltOreComp.Size = New System.Drawing.Size(105, 13)
        Me.lblLargeBeltOreComp.TabIndex = 64
        Me.lblLargeBeltOreComp.Text = "Belt Ore Composition"
        Me.lblLargeBeltOreComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tabEnormous
        '
        Me.tabEnormous.Controls.Add(Me.btnCloseXL)
        Me.tabEnormous.Controls.Add(Me.btnSaveSettingsXLarge)
        Me.tabEnormous.Controls.Add(Me.gbSum4)
        Me.tabEnormous.Controls.Add(Me.lblXLBeltMineralComp)
        Me.tabEnormous.Controls.Add(Me.lstOresLevel4)
        Me.tabEnormous.Controls.Add(Me.lstMineralsLevel4)
        Me.tabEnormous.Controls.Add(Me.lblXLBeltOreComp)
        Me.tabEnormous.Location = New System.Drawing.Point(4, 22)
        Me.tabEnormous.Name = "tabEnormous"
        Me.tabEnormous.Size = New System.Drawing.Size(496, 621)
        Me.tabEnormous.TabIndex = 5
        Me.tabEnormous.Text = "Enormous Belt"
        Me.tabEnormous.UseVisualStyleBackColor = True
        '
        'btnCloseXL
        '
        Me.btnCloseXL.Location = New System.Drawing.Point(374, 324)
        Me.btnCloseXL.Name = "btnCloseXL"
        Me.btnCloseXL.Size = New System.Drawing.Size(113, 28)
        Me.btnCloseXL.TabIndex = 20
        Me.btnCloseXL.Text = "Close"
        Me.btnCloseXL.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsXLarge
        '
        Me.btnSaveSettingsXLarge.Location = New System.Drawing.Point(248, 324)
        Me.btnSaveSettingsXLarge.Name = "btnSaveSettingsXLarge"
        Me.btnSaveSettingsXLarge.Size = New System.Drawing.Size(113, 28)
        Me.btnSaveSettingsXLarge.TabIndex = 19
        Me.btnSaveSettingsXLarge.Text = "Save Selected Ores"
        Me.btnSaveSettingsXLarge.UseVisualStyleBackColor = True
        '
        'gbSum4
        '
        Me.gbSum4.Controls.Add(Me.lblTotalIPH4)
        Me.gbSum4.Controls.Add(Me.lblTotalBeltVolume4)
        Me.gbSum4.Controls.Add(Me.lblBeltTotalIsk4)
        Me.gbSum4.Controls.Add(Me.lblTotalHourstoFlip4)
        Me.gbSum4.Controls.Add(Me.lblTotalIskLevel4)
        Me.gbSum4.Controls.Add(Me.lblIPH4)
        Me.gbSum4.Controls.Add(Me.lblTotalBeltVol4)
        Me.gbSum4.Controls.Add(Me.lblHourstoFlip4)
        Me.gbSum4.Location = New System.Drawing.Point(247, 201)
        Me.gbSum4.Name = "gbSum4"
        Me.gbSum4.Size = New System.Drawing.Size(243, 117)
        Me.gbSum4.TabIndex = 77
        Me.gbSum4.TabStop = False
        '
        'lblTotalIPH4
        '
        Me.lblTotalIPH4.AutoSize = True
        Me.lblTotalIPH4.Location = New System.Drawing.Point(18, 65)
        Me.lblTotalIPH4.Name = "lblTotalIPH4"
        Me.lblTotalIPH4.Size = New System.Drawing.Size(68, 13)
        Me.lblTotalIPH4.TabIndex = 61
        Me.lblTotalIPH4.Text = "Isk per Hour:"
        Me.lblTotalIPH4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBeltVolume4
        '
        Me.lblTotalBeltVolume4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume4.Location = New System.Drawing.Point(97, 84)
        Me.lblTotalBeltVolume4.Name = "lblTotalBeltVolume4"
        Me.lblTotalBeltVolume4.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume4.TabIndex = 60
        Me.lblTotalBeltVolume4.Text = "100,000.00"
        Me.lblTotalBeltVolume4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIsk4
        '
        Me.lblBeltTotalIsk4.AutoSize = True
        Me.lblBeltTotalIsk4.Location = New System.Drawing.Point(18, 21)
        Me.lblBeltTotalIsk4.Name = "lblBeltTotalIsk4"
        Me.lblBeltTotalIsk4.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIsk4.TabIndex = 52
        Me.lblBeltTotalIsk4.Text = "Belt Total Isk:"
        Me.lblBeltTotalIsk4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip4
        '
        Me.lblTotalHourstoFlip4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip4.Location = New System.Drawing.Point(97, 40)
        Me.lblTotalHourstoFlip4.Name = "lblTotalHourstoFlip4"
        Me.lblTotalHourstoFlip4.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip4.TabIndex = 57
        Me.lblTotalHourstoFlip4.Text = "100,000.00"
        Me.lblTotalHourstoFlip4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIskLevel4
        '
        Me.lblTotalIskLevel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel4.Location = New System.Drawing.Point(97, 18)
        Me.lblTotalIskLevel4.Name = "lblTotalIskLevel4"
        Me.lblTotalIskLevel4.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel4.TabIndex = 53
        Me.lblTotalIskLevel4.Text = "100,000.00"
        Me.lblTotalIskLevel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH4
        '
        Me.lblIPH4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIPH4.Location = New System.Drawing.Point(97, 62)
        Me.lblIPH4.Name = "lblIPH4"
        Me.lblIPH4.Size = New System.Drawing.Size(127, 18)
        Me.lblIPH4.TabIndex = 59
        Me.lblIPH4.Text = "100,000.00"
        Me.lblIPH4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltVol4
        '
        Me.lblTotalBeltVol4.AutoSize = True
        Me.lblTotalBeltVol4.Location = New System.Drawing.Point(18, 87)
        Me.lblTotalBeltVol4.Name = "lblTotalBeltVol4"
        Me.lblTotalBeltVol4.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalBeltVol4.TabIndex = 54
        Me.lblTotalBeltVol4.Text = "Belt Total Vol:"
        Me.lblTotalBeltVol4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip4
        '
        Me.lblHourstoFlip4.AutoSize = True
        Me.lblHourstoFlip4.Location = New System.Drawing.Point(18, 43)
        Me.lblHourstoFlip4.Name = "lblHourstoFlip4"
        Me.lblHourstoFlip4.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip4.TabIndex = 56
        Me.lblHourstoFlip4.Text = "Hours to Flip:"
        Me.lblHourstoFlip4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblXLBeltMineralComp
        '
        Me.lblXLBeltMineralComp.AutoSize = True
        Me.lblXLBeltMineralComp.Location = New System.Drawing.Point(307, 15)
        Me.lblXLBeltMineralComp.Name = "lblXLBeltMineralComp"
        Me.lblXLBeltMineralComp.Size = New System.Drawing.Size(122, 13)
        Me.lblXLBeltMineralComp.TabIndex = 70
        Me.lblXLBeltMineralComp.Text = "Belt Mineral Composition"
        Me.lblXLBeltMineralComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstOresLevel4
        '
        Me.lstOresLevel4.CheckBoxes = True
        Me.lstOresLevel4.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxXL, Me.orenameXL, Me.numberroidsXL, Me.unitsXL})
        Me.lstOresLevel4.FullRowSelect = True
        Me.lstOresLevel4.GridLines = True
        Me.lstOresLevel4.HideSelection = False
        Me.lstOresLevel4.Location = New System.Drawing.Point(6, 30)
        Me.lstOresLevel4.MultiSelect = False
        Me.lstOresLevel4.Name = "lstOresLevel4"
        Me.lstOresLevel4.Size = New System.Drawing.Size(235, 322)
        Me.lstOresLevel4.TabIndex = 67
        Me.lstOresLevel4.UseCompatibleStateImageBehavior = False
        Me.lstOresLevel4.View = System.Windows.Forms.View.Details
        '
        'checkboxXL
        '
        Me.checkboxXL.Text = ""
        Me.checkboxXL.Width = 25
        '
        'orenameXL
        '
        Me.orenameXL.Text = "Ore"
        Me.orenameXL.Width = 100
        '
        'numberroidsXL
        '
        Me.numberroidsXL.Text = "Rocks"
        Me.numberroidsXL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numberroidsXL.Width = 43
        '
        'unitsXL
        '
        Me.unitsXL.Text = "Units"
        Me.unitsXL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.unitsXL.Width = 63
        '
        'lstMineralsLevel4
        '
        Me.lstMineralsLevel4.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.mineralsXL, Me.minsunitsXL, Me.totaliskXL})
        Me.lstMineralsLevel4.FullRowSelect = True
        Me.lstMineralsLevel4.GridLines = True
        Me.lstMineralsLevel4.HideSelection = False
        Me.lstMineralsLevel4.Location = New System.Drawing.Point(247, 30)
        Me.lstMineralsLevel4.MultiSelect = False
        Me.lstMineralsLevel4.Name = "lstMineralsLevel4"
        Me.lstMineralsLevel4.Size = New System.Drawing.Size(243, 165)
        Me.lstMineralsLevel4.TabIndex = 68
        Me.lstMineralsLevel4.UseCompatibleStateImageBehavior = False
        Me.lstMineralsLevel4.View = System.Windows.Forms.View.Details
        '
        'mineralsXL
        '
        Me.mineralsXL.Text = "Mineral"
        Me.mineralsXL.Width = 57
        '
        'minsunitsXL
        '
        Me.minsunitsXL.Text = "Units"
        Me.minsunitsXL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.minsunitsXL.Width = 79
        '
        'totaliskXL
        '
        Me.totaliskXL.Text = "Total Isk"
        Me.totaliskXL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.totaliskXL.Width = 103
        '
        'lblXLBeltOreComp
        '
        Me.lblXLBeltOreComp.AutoSize = True
        Me.lblXLBeltOreComp.Location = New System.Drawing.Point(71, 15)
        Me.lblXLBeltOreComp.Name = "lblXLBeltOreComp"
        Me.lblXLBeltOreComp.Size = New System.Drawing.Size(105, 13)
        Me.lblXLBeltOreComp.TabIndex = 69
        Me.lblXLBeltOreComp.Text = "Belt Ore Composition"
        Me.lblXLBeltOreComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tabColossal
        '
        Me.tabColossal.Controls.Add(Me.btnCloseGiant)
        Me.tabColossal.Controls.Add(Me.btnSaveSettingsGiant)
        Me.tabColossal.Controls.Add(Me.gbSum5)
        Me.tabColossal.Controls.Add(Me.lblGiantBeltMineralComp)
        Me.tabColossal.Controls.Add(Me.lstOresLevel5)
        Me.tabColossal.Controls.Add(Me.lstMineralsLevel5)
        Me.tabColossal.Controls.Add(Me.lblGiantBeltOreComp)
        Me.tabColossal.Location = New System.Drawing.Point(4, 22)
        Me.tabColossal.Name = "tabColossal"
        Me.tabColossal.Size = New System.Drawing.Size(496, 621)
        Me.tabColossal.TabIndex = 4
        Me.tabColossal.Text = "Colossal Belt"
        Me.tabColossal.UseVisualStyleBackColor = True
        '
        'btnCloseGiant
        '
        Me.btnCloseGiant.Location = New System.Drawing.Point(374, 324)
        Me.btnCloseGiant.Name = "btnCloseGiant"
        Me.btnCloseGiant.Size = New System.Drawing.Size(113, 28)
        Me.btnCloseGiant.TabIndex = 22
        Me.btnCloseGiant.Text = "Close"
        Me.btnCloseGiant.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsGiant
        '
        Me.btnSaveSettingsGiant.Location = New System.Drawing.Point(248, 324)
        Me.btnSaveSettingsGiant.Name = "btnSaveSettingsGiant"
        Me.btnSaveSettingsGiant.Size = New System.Drawing.Size(113, 28)
        Me.btnSaveSettingsGiant.TabIndex = 21
        Me.btnSaveSettingsGiant.Text = "Save Selected Ores"
        Me.btnSaveSettingsGiant.UseVisualStyleBackColor = True
        '
        'gbSum5
        '
        Me.gbSum5.Controls.Add(Me.lblTotalIPH5)
        Me.gbSum5.Controls.Add(Me.lblTotalBeltVolume5)
        Me.gbSum5.Controls.Add(Me.lblBeltTotalIsk5)
        Me.gbSum5.Controls.Add(Me.lblTotalHourstoFlip5)
        Me.gbSum5.Controls.Add(Me.lblTotalIskLevel5)
        Me.gbSum5.Controls.Add(Me.lblIPH5)
        Me.gbSum5.Controls.Add(Me.lblTotalBeltVol5)
        Me.gbSum5.Controls.Add(Me.lblHourstoFlip5)
        Me.gbSum5.Location = New System.Drawing.Point(247, 201)
        Me.gbSum5.Name = "gbSum5"
        Me.gbSum5.Size = New System.Drawing.Size(243, 117)
        Me.gbSum5.TabIndex = 76
        Me.gbSum5.TabStop = False
        '
        'lblTotalIPH5
        '
        Me.lblTotalIPH5.AutoSize = True
        Me.lblTotalIPH5.Location = New System.Drawing.Point(18, 65)
        Me.lblTotalIPH5.Name = "lblTotalIPH5"
        Me.lblTotalIPH5.Size = New System.Drawing.Size(68, 13)
        Me.lblTotalIPH5.TabIndex = 61
        Me.lblTotalIPH5.Text = "Isk per Hour:"
        Me.lblTotalIPH5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBeltVolume5
        '
        Me.lblTotalBeltVolume5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalBeltVolume5.Location = New System.Drawing.Point(97, 84)
        Me.lblTotalBeltVolume5.Name = "lblTotalBeltVolume5"
        Me.lblTotalBeltVolume5.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalBeltVolume5.TabIndex = 60
        Me.lblTotalBeltVolume5.Text = "100,000.00"
        Me.lblTotalBeltVolume5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBeltTotalIsk5
        '
        Me.lblBeltTotalIsk5.AutoSize = True
        Me.lblBeltTotalIsk5.Location = New System.Drawing.Point(18, 21)
        Me.lblBeltTotalIsk5.Name = "lblBeltTotalIsk5"
        Me.lblBeltTotalIsk5.Size = New System.Drawing.Size(72, 13)
        Me.lblBeltTotalIsk5.TabIndex = 52
        Me.lblBeltTotalIsk5.Text = "Belt Total Isk:"
        Me.lblBeltTotalIsk5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalHourstoFlip5
        '
        Me.lblTotalHourstoFlip5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalHourstoFlip5.Location = New System.Drawing.Point(97, 40)
        Me.lblTotalHourstoFlip5.Name = "lblTotalHourstoFlip5"
        Me.lblTotalHourstoFlip5.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalHourstoFlip5.TabIndex = 57
        Me.lblTotalHourstoFlip5.Text = "100,000.00"
        Me.lblTotalHourstoFlip5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIskLevel5
        '
        Me.lblTotalIskLevel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalIskLevel5.Location = New System.Drawing.Point(97, 18)
        Me.lblTotalIskLevel5.Name = "lblTotalIskLevel5"
        Me.lblTotalIskLevel5.Size = New System.Drawing.Size(127, 18)
        Me.lblTotalIskLevel5.TabIndex = 53
        Me.lblTotalIskLevel5.Text = "100,000.00"
        Me.lblTotalIskLevel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIPH5
        '
        Me.lblIPH5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIPH5.Location = New System.Drawing.Point(97, 62)
        Me.lblIPH5.Name = "lblIPH5"
        Me.lblIPH5.Size = New System.Drawing.Size(127, 18)
        Me.lblIPH5.TabIndex = 59
        Me.lblIPH5.Text = "100,000.00"
        Me.lblIPH5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBeltVol5
        '
        Me.lblTotalBeltVol5.AutoSize = True
        Me.lblTotalBeltVol5.Location = New System.Drawing.Point(18, 87)
        Me.lblTotalBeltVol5.Name = "lblTotalBeltVol5"
        Me.lblTotalBeltVol5.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalBeltVol5.TabIndex = 54
        Me.lblTotalBeltVol5.Text = "Belt Total Vol:"
        Me.lblTotalBeltVol5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHourstoFlip5
        '
        Me.lblHourstoFlip5.AutoSize = True
        Me.lblHourstoFlip5.Location = New System.Drawing.Point(18, 43)
        Me.lblHourstoFlip5.Name = "lblHourstoFlip5"
        Me.lblHourstoFlip5.Size = New System.Drawing.Size(69, 13)
        Me.lblHourstoFlip5.TabIndex = 56
        Me.lblHourstoFlip5.Text = "Hours to Flip:"
        Me.lblHourstoFlip5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGiantBeltMineralComp
        '
        Me.lblGiantBeltMineralComp.AutoSize = True
        Me.lblGiantBeltMineralComp.Location = New System.Drawing.Point(307, 15)
        Me.lblGiantBeltMineralComp.Name = "lblGiantBeltMineralComp"
        Me.lblGiantBeltMineralComp.Size = New System.Drawing.Size(122, 13)
        Me.lblGiantBeltMineralComp.TabIndex = 75
        Me.lblGiantBeltMineralComp.Text = "Belt Mineral Composition"
        Me.lblGiantBeltMineralComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstOresLevel5
        '
        Me.lstOresLevel5.CheckBoxes = True
        Me.lstOresLevel5.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.checkboxGiant, Me.orenameGiant, Me.numberroidsGiant, Me.unitsGiant})
        Me.lstOresLevel5.FullRowSelect = True
        Me.lstOresLevel5.GridLines = True
        Me.lstOresLevel5.HideSelection = False
        Me.lstOresLevel5.Location = New System.Drawing.Point(6, 30)
        Me.lstOresLevel5.MultiSelect = False
        Me.lstOresLevel5.Name = "lstOresLevel5"
        Me.lstOresLevel5.Size = New System.Drawing.Size(235, 322)
        Me.lstOresLevel5.TabIndex = 72
        Me.lstOresLevel5.UseCompatibleStateImageBehavior = False
        Me.lstOresLevel5.View = System.Windows.Forms.View.Details
        '
        'checkboxGiant
        '
        Me.checkboxGiant.Text = ""
        Me.checkboxGiant.Width = 25
        '
        'orenameGiant
        '
        Me.orenameGiant.Text = "Ore"
        Me.orenameGiant.Width = 100
        '
        'numberroidsGiant
        '
        Me.numberroidsGiant.Text = "Rocks"
        Me.numberroidsGiant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numberroidsGiant.Width = 43
        '
        'unitsGiant
        '
        Me.unitsGiant.Text = "Units"
        Me.unitsGiant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.unitsGiant.Width = 63
        '
        'lstMineralsLevel5
        '
        Me.lstMineralsLevel5.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.mineralsGiant, Me.minsunitsGiant, Me.totaliskGiant})
        Me.lstMineralsLevel5.FullRowSelect = True
        Me.lstMineralsLevel5.GridLines = True
        Me.lstMineralsLevel5.HideSelection = False
        Me.lstMineralsLevel5.Location = New System.Drawing.Point(247, 30)
        Me.lstMineralsLevel5.MultiSelect = False
        Me.lstMineralsLevel5.Name = "lstMineralsLevel5"
        Me.lstMineralsLevel5.Size = New System.Drawing.Size(243, 165)
        Me.lstMineralsLevel5.TabIndex = 73
        Me.lstMineralsLevel5.UseCompatibleStateImageBehavior = False
        Me.lstMineralsLevel5.View = System.Windows.Forms.View.Details
        '
        'mineralsGiant
        '
        Me.mineralsGiant.Text = "Mineral"
        Me.mineralsGiant.Width = 57
        '
        'minsunitsGiant
        '
        Me.minsunitsGiant.Text = "Units"
        Me.minsunitsGiant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.minsunitsGiant.Width = 79
        '
        'totaliskGiant
        '
        Me.totaliskGiant.Text = "Total Isk"
        Me.totaliskGiant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.totaliskGiant.Width = 103
        '
        'lblGiantBeltOreComp
        '
        Me.lblGiantBeltOreComp.AutoSize = True
        Me.lblGiantBeltOreComp.Location = New System.Drawing.Point(71, 15)
        Me.lblGiantBeltOreComp.Name = "lblGiantBeltOreComp"
        Me.lblGiantBeltOreComp.Size = New System.Drawing.Size(105, 13)
        Me.lblGiantBeltOreComp.TabIndex = 74
        Me.lblGiantBeltOreComp.Text = "Belt Ore Composition"
        Me.lblGiantBeltOreComp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ttMain
        '
        Me.ttMain.IsBalloon = True
        '
        'frmIndustryBeltFlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(519, 661)
        Me.Controls.Add(Me.tabIndustryBelts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIndustryBeltFlip"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Industry Upgrade Belts"
        Me.gbMineTaxBroker.ResumeLayout(False)
        Me.gbMineTaxBroker.PerformLayout()
        Me.tabIndustryBelts.ResumeLayout(False)
        Me.tabSummary.ResumeLayout(False)
        Me.gbGiant.ResumeLayout(False)
        Me.gbGiant.PerformLayout()
        Me.gbExtraLarge.ResumeLayout(False)
        Me.gbExtraLarge.PerformLayout()
        Me.gbLarge.ResumeLayout(False)
        Me.gbLarge.PerformLayout()
        Me.gbModerate.ResumeLayout(False)
        Me.gbModerate.PerformLayout()
        Me.gbSmall.ResumeLayout(False)
        Me.gbSmall.PerformLayout()
        Me.gbSummarySettings.ResumeLayout(False)
        Me.gbSummarySettings.PerformLayout()
        Me.gbTruesec.ResumeLayout(False)
        Me.gbTruesec.PerformLayout()
        Me.tabSmall.ResumeLayout(False)
        Me.tabSmall.PerformLayout()
        Me.gbSum1.ResumeLayout(False)
        Me.gbSum1.PerformLayout()
        Me.tabMedium.ResumeLayout(False)
        Me.tabMedium.PerformLayout()
        Me.gbSum2.ResumeLayout(False)
        Me.gbSum2.PerformLayout()
        Me.tabLarge.ResumeLayout(False)
        Me.tabLarge.PerformLayout()
        Me.gbSum3.ResumeLayout(False)
        Me.gbSum3.PerformLayout()
        Me.tabEnormous.ResumeLayout(False)
        Me.tabEnormous.PerformLayout()
        Me.gbSum4.ResumeLayout(False)
        Me.gbSum4.PerformLayout()
        Me.tabColossal.ResumeLayout(False)
        Me.tabColossal.PerformLayout()
        Me.gbSum5.ResumeLayout(False)
        Me.gbSum5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstOresLevel1 As System.Windows.Forms.ListView
    Friend WithEvents lstMineralsLevel1 As System.Windows.Forms.ListView
    Friend WithEvents orenameSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents unitsSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents numberroidsSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents mineralsSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents minsunitsSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents totaliskSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblSmallBeltOreComp As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIskLevel1Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel1Sum As System.Windows.Forms.Label
    Friend WithEvents checkboxSmall As System.Windows.Forms.ColumnHeader
    Friend WithEvents gbMineTaxBroker As System.Windows.Forms.GroupBox
    Friend WithEvents chkIncludeTaxes As System.Windows.Forms.CheckBox
    Friend WithEvents chkBrokerFees As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalBeltVolume1Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltVolume1Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip1Sum As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip1Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIPH1Sum As System.Windows.Forms.Label
    Friend WithEvents lblIPH1Sum As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents lblCycleTime As System.Windows.Forms.Label
    Friend WithEvents txtCycleTime As System.Windows.Forms.TextBox
    Friend WithEvents lblm3perCycle As System.Windows.Forms.Label
    Friend WithEvents txtm3perCycle As System.Windows.Forms.TextBox
    Friend WithEvents lblNumMiners As System.Windows.Forms.Label
    Friend WithEvents cmbNumMiners As System.Windows.Forms.ComboBox
    Friend WithEvents lblm3perhrperminer1 As System.Windows.Forms.Label
    Friend WithEvents lblm3perhrperminer As System.Windows.Forms.Label
    Friend WithEvents tabIndustryBelts As System.Windows.Forms.TabControl
    Friend WithEvents tabSummary As System.Windows.Forms.TabPage
    Friend WithEvents tabSmall As System.Windows.Forms.TabPage
    Friend WithEvents tabMedium As System.Windows.Forms.TabPage
    Friend WithEvents tabLarge As System.Windows.Forms.TabPage
    Friend WithEvents tabEnormous As System.Windows.Forms.TabPage
    Friend WithEvents tabColossal As System.Windows.Forms.TabPage
    Friend WithEvents gbSummarySettings As System.Windows.Forms.GroupBox
    Friend WithEvents gbGiant As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalBeltVolume5Sum As System.Windows.Forms.Label
    Friend WithEvents lblIPH5Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip5Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIPH5Sum As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip5Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltVolume5Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel5Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIskLevel5Sum As System.Windows.Forms.Label
    Friend WithEvents gbExtraLarge As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalBeltVolume4Sum As System.Windows.Forms.Label
    Friend WithEvents lblIPH4Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip4Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIPH4Sum As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip4Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltVolume4Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel4Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIskLevel4Sum As System.Windows.Forms.Label
    Friend WithEvents gbLarge As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalBeltVolume3Sum As System.Windows.Forms.Label
    Friend WithEvents lblIPH3Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip3Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIPH3Sum As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip3Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltVolume3Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel3Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIskLevel3Sum As System.Windows.Forms.Label
    Friend WithEvents gbModerate As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalBeltVolume2Sum As System.Windows.Forms.Label
    Friend WithEvents lblIPH2Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip2Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIPH2Sum As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip2Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltVolume2Sum As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel2Sum As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIskLevel2Sum As System.Windows.Forms.Label
    Friend WithEvents gbSmall As System.Windows.Forms.GroupBox
    Friend WithEvents lblSmallBeltMineralComp As System.Windows.Forms.Label
    Friend WithEvents lstMineralsLevel2 As System.Windows.Forms.ListView
    Friend WithEvents mineralsMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents minsunitsMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents totaliskMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstOresLevel2 As System.Windows.Forms.ListView
    Friend WithEvents checkboxMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents orenameMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents numberroidsMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents unitsMedium As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblMediumBeltMineralComp As System.Windows.Forms.Label
    Friend WithEvents lblMediumBeltOreComp As System.Windows.Forms.Label
    Friend WithEvents lblLargeBeltMineralComp As System.Windows.Forms.Label
    Friend WithEvents lstOresLevel3 As System.Windows.Forms.ListView
    Friend WithEvents checkboxLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents orenameLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents numberroidsLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents unitsLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstMineralsLevel3 As System.Windows.Forms.ListView
    Friend WithEvents mineralsLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents minsunitsLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents totaliskLarge As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblLargeBeltOreComp As System.Windows.Forms.Label
    Friend WithEvents lblXLBeltMineralComp As System.Windows.Forms.Label
    Friend WithEvents lstOresLevel4 As System.Windows.Forms.ListView
    Friend WithEvents checkboxXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents orenameXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents numberroidsXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents unitsXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstMineralsLevel4 As System.Windows.Forms.ListView
    Friend WithEvents mineralsXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents minsunitsXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents totaliskXL As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblXLBeltOreComp As System.Windows.Forms.Label
    Friend WithEvents gbSum5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblBeltTotalIsk5 As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip5 As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel5 As System.Windows.Forms.Label
    Friend WithEvents lblIPH5 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVol5 As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip5 As System.Windows.Forms.Label
    Friend WithEvents lblGiantBeltMineralComp As System.Windows.Forms.Label
    Friend WithEvents lstOresLevel5 As System.Windows.Forms.ListView
    Friend WithEvents checkboxGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents orenameGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents numberroidsGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents unitsGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstMineralsLevel5 As System.Windows.Forms.ListView
    Friend WithEvents mineralsGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents minsunitsGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents totaliskGiant As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblGiantBeltOreComp As System.Windows.Forms.Label
    Friend WithEvents gbSum1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalIPH1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVolume1 As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIsk1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel1 As System.Windows.Forms.Label
    Friend WithEvents lblIPH1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVol1 As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip1 As System.Windows.Forms.Label
    Friend WithEvents gbSum2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalIPH2 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVolume2 As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIsk2 As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip2 As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel2 As System.Windows.Forms.Label
    Friend WithEvents lblIPH2 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVol2 As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip2 As System.Windows.Forms.Label
    Friend WithEvents gbSum3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalIPH3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVolume3 As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIsk3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel3 As System.Windows.Forms.Label
    Friend WithEvents lblIPH3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVol3 As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip3 As System.Windows.Forms.Label
    Friend WithEvents gbSum4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalIPH4 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVolume4 As System.Windows.Forms.Label
    Friend WithEvents lblBeltTotalIsk4 As System.Windows.Forms.Label
    Friend WithEvents lblTotalHourstoFlip4 As System.Windows.Forms.Label
    Friend WithEvents lblTotalIskLevel4 As System.Windows.Forms.Label
    Friend WithEvents lblIPH4 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVol4 As System.Windows.Forms.Label
    Friend WithEvents lblHourstoFlip4 As System.Windows.Forms.Label
    Friend WithEvents lblTotalIPH5 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBeltVolume5 As System.Windows.Forms.Label
    Friend WithEvents btnCloseSmall As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettingsSmall As System.Windows.Forms.Button
    Friend WithEvents btnCloseMedium As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettingsMedium As System.Windows.Forms.Button
    Friend WithEvents btnCloseLarge As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettingsLarge As System.Windows.Forms.Button
    Friend WithEvents btnCloseXL As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettingsXLarge As System.Windows.Forms.Button
    Friend WithEvents btnCloseGiant As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettingsGiant As System.Windows.Forms.Button
    Friend WithEvents chkCompressOre As System.Windows.Forms.CheckBox
    Friend WithEvents btnRefine As System.Windows.Forms.Button
    Friend WithEvents chkIPHperMiner As System.Windows.Forms.CheckBox
    Friend WithEvents ttMain As System.Windows.Forms.ToolTip
    Friend WithEvents txtBrokerFeeRate As TextBox
    Friend WithEvents ReprocessingFacility As ManufacturingFacility
    Friend WithEvents gbTruesec As GroupBox
    Friend WithEvents rbtn10percent As RadioButton
    Friend WithEvents rbtn0percent As RadioButton
    Friend WithEvents rbtn5percent As RadioButton
End Class
