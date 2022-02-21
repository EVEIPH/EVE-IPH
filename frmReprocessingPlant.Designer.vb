<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReprocessingPlant
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReprocessingPlant))
        Me.btnReprocess = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnShowAssets = New System.Windows.Forms.Button()
        Me.btnCopyPasteAssets = New System.Windows.Forms.Button()
        Me.btnSelectAssets = New System.Windows.Forms.Button()
        Me.cmbReprocessing = New System.Windows.Forms.ComboBox()
        Me.lblRefining = New System.Windows.Forms.Label()
        Me.cmbReprocessingEff = New System.Windows.Forms.ComboBox()
        Me.lblRefineryEfficiency = New System.Windows.Forms.Label()
        Me.gbRefineYields = New System.Windows.Forms.GroupBox()
        Me.lblScrapRate = New System.Windows.Forms.Label()
        Me.lblIceRate = New System.Windows.Forms.Label()
        Me.lblIce = New System.Windows.Forms.Label()
        Me.lblScrap = New System.Windows.Forms.Label()
        Me.lblOreRate = New System.Windows.Forms.Label()
        Me.lblMoonRate = New System.Windows.Forms.Label()
        Me.lblOre = New System.Windows.Forms.Label()
        Me.lblMoonOre = New System.Windows.Forms.Label()
        Me.cmbScrapMetalProcessing = New System.Windows.Forms.ComboBox()
        Me.lblScrapMetalProcessing = New System.Windows.Forms.Label()
        Me.lblReprocessingOutput = New System.Windows.Forms.Label()
        Me.lblItemList = New System.Windows.Forms.Label()
        Me.lblItemSelect = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tabRefinery = New System.Windows.Forms.TabControl()
        Me.tabpMain = New System.Windows.Forms.TabPage()
        Me.chkRecursiveRefine = New System.Windows.Forms.CheckBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCopyOutput = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBeanCounterRefining = New System.Windows.Forms.ComboBox()
        Me.tabpSkills = New System.Windows.Forms.TabPage()
        Me.tabMiningProcessingSkills = New System.Windows.Forms.TabControl()
        Me.tabPageOres = New System.Windows.Forms.TabPage()
        Me.chkOreProcessing1 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing2 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing2 = New System.Windows.Forms.Label()
        Me.chkOreProcessing3 = New System.Windows.Forms.CheckBox()
        Me.chkOreProcessing2 = New System.Windows.Forms.CheckBox()
        Me.chkOreProcessing6 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing1 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing1 = New System.Windows.Forms.Label()
        Me.lblOreProcessing6 = New System.Windows.Forms.Label()
        Me.chkOreProcessing5 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing6 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing3 = New System.Windows.Forms.Label()
        Me.lblOreProcessing5 = New System.Windows.Forms.Label()
        Me.cmbOreProcessing4 = New System.Windows.Forms.ComboBox()
        Me.cmbOreProcessing3 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing4 = New System.Windows.Forms.CheckBox()
        Me.lblOreProcessing4 = New System.Windows.Forms.Label()
        Me.cmbOreProcessing5 = New System.Windows.Forms.ComboBox()
        Me.tabPageMoonOres = New System.Windows.Forms.TabPage()
        Me.lblOreProcessing7 = New System.Windows.Forms.Label()
        Me.lblOreProcessing8 = New System.Windows.Forms.Label()
        Me.lblOreProcessing10 = New System.Windows.Forms.Label()
        Me.lblOreProcessing11 = New System.Windows.Forms.Label()
        Me.cmbOreProcessing11 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing9 = New System.Windows.Forms.CheckBox()
        Me.lblOreProcessing9 = New System.Windows.Forms.Label()
        Me.chkOreProcessing8 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing10 = New System.Windows.Forms.ComboBox()
        Me.cmbOreProcessing7 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing10 = New System.Windows.Forms.CheckBox()
        Me.chkOreProcessing7 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing9 = New System.Windows.Forms.ComboBox()
        Me.chkOreProcessing11 = New System.Windows.Forms.CheckBox()
        Me.cmbOreProcessing8 = New System.Windows.Forms.ComboBox()
        Me.tabPageIce = New System.Windows.Forms.TabPage()
        Me.cmbOreProcessing12 = New System.Windows.Forms.ComboBox()
        Me.lblOreProcessing12 = New System.Windows.Forms.Label()
        Me.chkOreProcessing12 = New System.Windows.Forms.CheckBox()
        Me.btnClear2 = New System.Windows.Forms.Button()
        Me.btnCopyOutput2 = New System.Windows.Forms.Button()
        Me.btnReprocess2 = New System.Windows.Forms.Button()
        Me.btnClose2 = New System.Windows.Forms.Button()
        Me.chkToggle = New System.Windows.Forms.CheckBox()
        Me.lblTotalItemList = New System.Windows.Forms.Label()
        Me.lblReprocessingVolumeTotal = New System.Windows.Forms.Label()
        Me.lblReprocessingTotalRate = New System.Windows.Forms.Label()
        Me.lblReprocessingTotal = New System.Windows.Forms.Label()
        Me.lblListTotalValueOutput = New System.Windows.Forms.Label()
        Me.lblReturnRatePercentOutput = New System.Windows.Forms.Label()
        Me.lblReprocessingValueOutput = New System.Windows.Forms.Label()
        Me.lblReprocessingVolumeOutput = New System.Windows.Forms.Label()
        Me.ReprocessingFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.lstItemstoRefine = New EVE_Isk_per_Hour.MyListView()
        Me.lstRefineOutput = New EVE_Isk_per_Hour.MyListView()
        Me.gbRefineYields.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tabRefinery.SuspendLayout()
        Me.tabpMain.SuspendLayout()
        Me.tabpSkills.SuspendLayout()
        Me.tabMiningProcessingSkills.SuspendLayout()
        Me.tabPageOres.SuspendLayout()
        Me.tabPageMoonOres.SuspendLayout()
        Me.tabPageIce.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReprocess
        '
        Me.btnReprocess.Location = New System.Drawing.Point(259, 128)
        Me.btnReprocess.Name = "btnReprocess"
        Me.btnReprocess.Size = New System.Drawing.Size(74, 30)
        Me.btnReprocess.TabIndex = 36
        Me.btnReprocess.Text = "Reprocess"
        Me.btnReprocess.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(481, 128)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 30)
        Me.btnClose.TabIndex = 38
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnShowAssets
        '
        Me.btnShowAssets.Image = CType(resources.GetObject("btnShowAssets.Image"), System.Drawing.Image)
        Me.btnShowAssets.Location = New System.Drawing.Point(373, 25)
        Me.btnShowAssets.Name = "btnShowAssets"
        Me.btnShowAssets.Size = New System.Drawing.Size(48, 48)
        Me.btnShowAssets.TabIndex = 44
        Me.btnShowAssets.UseVisualStyleBackColor = True
        '
        'btnCopyPasteAssets
        '
        Me.btnCopyPasteAssets.Location = New System.Drawing.Point(315, 76)
        Me.btnCopyPasteAssets.Name = "btnCopyPasteAssets"
        Me.btnCopyPasteAssets.Size = New System.Drawing.Size(106, 35)
        Me.btnCopyPasteAssets.TabIndex = 46
        Me.btnCopyPasteAssets.Text = "Copy and Paste"
        Me.btnCopyPasteAssets.UseVisualStyleBackColor = True
        '
        'btnSelectAssets
        '
        Me.btnSelectAssets.Location = New System.Drawing.Point(315, 25)
        Me.btnSelectAssets.Name = "btnSelectAssets"
        Me.btnSelectAssets.Size = New System.Drawing.Size(52, 48)
        Me.btnSelectAssets.TabIndex = 45
        Me.btnSelectAssets.Text = "Select from Assets"
        Me.btnSelectAssets.UseVisualStyleBackColor = True
        '
        'cmbReprocessing
        '
        Me.cmbReprocessing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReprocessing.FormattingEnabled = True
        Me.cmbReprocessing.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbReprocessing.Location = New System.Drawing.Point(95, 14)
        Me.cmbReprocessing.Name = "cmbReprocessing"
        Me.cmbReprocessing.Size = New System.Drawing.Size(30, 21)
        Me.cmbReprocessing.TabIndex = 110
        '
        'lblRefining
        '
        Me.lblRefining.AutoSize = True
        Me.lblRefining.Location = New System.Drawing.Point(14, 18)
        Me.lblRefining.Name = "lblRefining"
        Me.lblRefining.Size = New System.Drawing.Size(75, 13)
        Me.lblRefining.TabIndex = 112
        Me.lblRefining.Text = "Reprocessing:"
        '
        'cmbReprocessingEff
        '
        Me.cmbReprocessingEff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReprocessingEff.FormattingEnabled = True
        Me.cmbReprocessingEff.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbReprocessingEff.Location = New System.Drawing.Point(95, 46)
        Me.cmbReprocessingEff.Name = "cmbReprocessingEff"
        Me.cmbReprocessingEff.Size = New System.Drawing.Size(30, 21)
        Me.cmbReprocessingEff.TabIndex = 111
        '
        'lblRefineryEfficiency
        '
        Me.lblRefineryEfficiency.Location = New System.Drawing.Point(6, 41)
        Me.lblRefineryEfficiency.Name = "lblRefineryEfficiency"
        Me.lblRefineryEfficiency.Size = New System.Drawing.Size(80, 26)
        Me.lblRefineryEfficiency.TabIndex = 113
        Me.lblRefineryEfficiency.Text = "Reprocessing  Efficiency:"
        Me.lblRefineryEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbRefineYields
        '
        Me.gbRefineYields.Controls.Add(Me.lblScrapRate)
        Me.gbRefineYields.Controls.Add(Me.lblIceRate)
        Me.gbRefineYields.Controls.Add(Me.lblIce)
        Me.gbRefineYields.Controls.Add(Me.lblScrap)
        Me.gbRefineYields.Controls.Add(Me.lblOreRate)
        Me.gbRefineYields.Controls.Add(Me.lblMoonRate)
        Me.gbRefineYields.Controls.Add(Me.lblOre)
        Me.gbRefineYields.Controls.Add(Me.lblMoonOre)
        Me.gbRefineYields.Location = New System.Drawing.Point(434, 10)
        Me.gbRefineYields.Name = "gbRefineYields"
        Me.gbRefineYields.Size = New System.Drawing.Size(121, 101)
        Me.gbRefineYields.TabIndex = 122
        Me.gbRefineYields.TabStop = False
        Me.gbRefineYields.Text = "Facility Refine Yields:"
        '
        'lblScrapRate
        '
        Me.lblScrapRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblScrapRate.Location = New System.Drawing.Point(53, 76)
        Me.lblScrapRate.Name = "lblScrapRate"
        Me.lblScrapRate.Size = New System.Drawing.Size(50, 18)
        Me.lblScrapRate.TabIndex = 141
        Me.lblScrapRate.Text = "100.00%"
        Me.lblScrapRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIceRate
        '
        Me.lblIceRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIceRate.Location = New System.Drawing.Point(53, 38)
        Me.lblIceRate.Name = "lblIceRate"
        Me.lblIceRate.Size = New System.Drawing.Size(50, 18)
        Me.lblIceRate.TabIndex = 140
        Me.lblIceRate.Text = "100.00%"
        Me.lblIceRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIce
        '
        Me.lblIce.AutoSize = True
        Me.lblIce.Location = New System.Drawing.Point(22, 41)
        Me.lblIce.Name = "lblIce"
        Me.lblIce.Size = New System.Drawing.Size(25, 13)
        Me.lblIce.TabIndex = 139
        Me.lblIce.Text = "Ice:"
        '
        'lblScrap
        '
        Me.lblScrap.AutoSize = True
        Me.lblScrap.Location = New System.Drawing.Point(9, 79)
        Me.lblScrap.Name = "lblScrap"
        Me.lblScrap.Size = New System.Drawing.Size(38, 13)
        Me.lblScrap.TabIndex = 138
        Me.lblScrap.Text = "Scrap:"
        '
        'lblOreRate
        '
        Me.lblOreRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOreRate.Location = New System.Drawing.Point(53, 18)
        Me.lblOreRate.Name = "lblOreRate"
        Me.lblOreRate.Size = New System.Drawing.Size(50, 18)
        Me.lblOreRate.TabIndex = 136
        Me.lblOreRate.Text = "100.00%"
        Me.lblOreRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMoonRate
        '
        Me.lblMoonRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMoonRate.Location = New System.Drawing.Point(53, 57)
        Me.lblMoonRate.Name = "lblMoonRate"
        Me.lblMoonRate.Size = New System.Drawing.Size(50, 18)
        Me.lblMoonRate.TabIndex = 137
        Me.lblMoonRate.Text = "100.00%"
        Me.lblMoonRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOre
        '
        Me.lblOre.AutoSize = True
        Me.lblOre.Location = New System.Drawing.Point(20, 21)
        Me.lblOre.Name = "lblOre"
        Me.lblOre.Size = New System.Drawing.Size(27, 13)
        Me.lblOre.TabIndex = 123
        Me.lblOre.Text = "Ore:"
        '
        'lblMoonOre
        '
        Me.lblMoonOre.AutoSize = True
        Me.lblMoonOre.Location = New System.Drawing.Point(10, 60)
        Me.lblMoonOre.Name = "lblMoonOre"
        Me.lblMoonOre.Size = New System.Drawing.Size(37, 13)
        Me.lblMoonOre.TabIndex = 125
        Me.lblMoonOre.Text = "Moon:"
        '
        'cmbScrapMetalProcessing
        '
        Me.cmbScrapMetalProcessing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScrapMetalProcessing.FormattingEnabled = True
        Me.cmbScrapMetalProcessing.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbScrapMetalProcessing.Location = New System.Drawing.Point(95, 79)
        Me.cmbScrapMetalProcessing.Name = "cmbScrapMetalProcessing"
        Me.cmbScrapMetalProcessing.Size = New System.Drawing.Size(30, 21)
        Me.cmbScrapMetalProcessing.TabIndex = 124
        '
        'lblScrapMetalProcessing
        '
        Me.lblScrapMetalProcessing.Location = New System.Drawing.Point(6, 74)
        Me.lblScrapMetalProcessing.Name = "lblScrapMetalProcessing"
        Me.lblScrapMetalProcessing.Size = New System.Drawing.Size(80, 26)
        Me.lblScrapMetalProcessing.TabIndex = 125
        Me.lblScrapMetalProcessing.Text = "Scrapmetal  Processing:"
        Me.lblScrapMetalProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReprocessingOutput
        '
        Me.lblReprocessingOutput.AutoSize = True
        Me.lblReprocessingOutput.Location = New System.Drawing.Point(239, 416)
        Me.lblReprocessingOutput.Name = "lblReprocessingOutput"
        Me.lblReprocessingOutput.Size = New System.Drawing.Size(107, 13)
        Me.lblReprocessingOutput.TabIndex = 126
        Me.lblReprocessingOutput.Text = "Reprocessing Output"
        Me.lblReprocessingOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblItemList
        '
        Me.lblItemList.AutoSize = True
        Me.lblItemList.Location = New System.Drawing.Point(269, 222)
        Me.lblItemList.Name = "lblItemList"
        Me.lblItemList.Size = New System.Drawing.Size(46, 13)
        Me.lblItemList.TabIndex = 127
        Me.lblItemList.Text = "Item List"
        Me.lblItemList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblItemSelect
        '
        Me.lblItemSelect.AutoSize = True
        Me.lblItemSelect.Location = New System.Drawing.Point(319, 10)
        Me.lblItemSelect.Name = "lblItemSelect"
        Me.lblItemSelect.Size = New System.Drawing.Size(102, 13)
        Me.lblItemSelect.TabIndex = 128
        Me.lblItemSelect.Text = "Select Refine Items:"
        Me.lblItemSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbReprocessingEff)
        Me.GroupBox1.Controls.Add(Me.lblRefineryEfficiency)
        Me.GroupBox1.Controls.Add(Me.cmbScrapMetalProcessing)
        Me.GroupBox1.Controls.Add(Me.lblRefining)
        Me.GroupBox1.Controls.Add(Me.lblScrapMetalProcessing)
        Me.GroupBox1.Controls.Add(Me.cmbReprocessing)
        Me.GroupBox1.Location = New System.Drawing.Point(6, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(143, 114)
        Me.GroupBox1.TabIndex = 129
        Me.GroupBox1.TabStop = False
        '
        'tabRefinery
        '
        Me.tabRefinery.Controls.Add(Me.tabpMain)
        Me.tabRefinery.Controls.Add(Me.tabpSkills)
        Me.tabRefinery.Location = New System.Drawing.Point(7, 7)
        Me.tabRefinery.Name = "tabRefinery"
        Me.tabRefinery.SelectedIndex = 0
        Me.tabRefinery.Size = New System.Drawing.Size(570, 205)
        Me.tabRefinery.TabIndex = 130
        '
        'tabpMain
        '
        Me.tabpMain.Controls.Add(Me.chkRecursiveRefine)
        Me.tabpMain.Controls.Add(Me.btnClear)
        Me.tabpMain.Controls.Add(Me.btnCopyOutput)
        Me.tabpMain.Controls.Add(Me.Label1)
        Me.tabpMain.Controls.Add(Me.cmbBeanCounterRefining)
        Me.tabpMain.Controls.Add(Me.ReprocessingFacility)
        Me.tabpMain.Controls.Add(Me.lblItemSelect)
        Me.tabpMain.Controls.Add(Me.btnCopyPasteAssets)
        Me.tabpMain.Controls.Add(Me.btnShowAssets)
        Me.tabpMain.Controls.Add(Me.btnReprocess)
        Me.tabpMain.Controls.Add(Me.btnSelectAssets)
        Me.tabpMain.Controls.Add(Me.btnClose)
        Me.tabpMain.Controls.Add(Me.gbRefineYields)
        Me.tabpMain.Location = New System.Drawing.Point(4, 22)
        Me.tabpMain.Name = "tabpMain"
        Me.tabpMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tabpMain.Size = New System.Drawing.Size(562, 179)
        Me.tabpMain.TabIndex = 0
        Me.tabpMain.Text = "Main"
        Me.tabpMain.UseVisualStyleBackColor = True
        '
        'chkRecursiveRefine
        '
        Me.chkRecursiveRefine.AutoSize = True
        Me.chkRecursiveRefine.Location = New System.Drawing.Point(62, 157)
        Me.chkRecursiveRefine.Name = "chkRecursiveRefine"
        Me.chkRecursiveRefine.Size = New System.Drawing.Size(128, 17)
        Me.chkRecursiveRefine.TabIndex = 132
        Me.chkRecursiveRefine.Text = "Drill Down Reprocess"
        Me.chkRecursiveRefine.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(333, 128)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(74, 30)
        Me.btnClear.TabIndex = 133
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnCopyOutput
        '
        Me.btnCopyOutput.Location = New System.Drawing.Point(407, 128)
        Me.btnCopyOutput.Name = "btnCopyOutput"
        Me.btnCopyOutput.Size = New System.Drawing.Size(74, 30)
        Me.btnCopyOutput.TabIndex = 132
        Me.btnCopyOutput.Text = "Copy Output"
        Me.btnCopyOutput.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 13)
        Me.Label1.TabIndex = 131
        Me.Label1.Text = "Reprocessing Beancounter Implant:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbBeanCounterRefining
        '
        Me.cmbBeanCounterRefining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBeanCounterRefining.FormattingEnabled = True
        Me.cmbBeanCounterRefining.Items.AddRange(New Object() {"None", "Zainou 'Beancounter' Reprocessing RX-801", "Zainou 'Beancounter' Reprocessing RX-802", "Zainou 'Beancounter' Reprocessing RX-804"})
        Me.cmbBeanCounterRefining.Location = New System.Drawing.Point(9, 130)
        Me.cmbBeanCounterRefining.Name = "cmbBeanCounterRefining"
        Me.cmbBeanCounterRefining.Size = New System.Drawing.Size(235, 21)
        Me.cmbBeanCounterRefining.TabIndex = 130
        '
        'tabpSkills
        '
        Me.tabpSkills.Controls.Add(Me.tabMiningProcessingSkills)
        Me.tabpSkills.Controls.Add(Me.btnClear2)
        Me.tabpSkills.Controls.Add(Me.btnCopyOutput2)
        Me.tabpSkills.Controls.Add(Me.btnReprocess2)
        Me.tabpSkills.Controls.Add(Me.btnClose2)
        Me.tabpSkills.Controls.Add(Me.GroupBox1)
        Me.tabpSkills.Location = New System.Drawing.Point(4, 22)
        Me.tabpSkills.Name = "tabpSkills"
        Me.tabpSkills.Padding = New System.Windows.Forms.Padding(3)
        Me.tabpSkills.Size = New System.Drawing.Size(562, 179)
        Me.tabpSkills.TabIndex = 1
        Me.tabpSkills.Text = "Skills"
        Me.tabpSkills.UseVisualStyleBackColor = True
        '
        'tabMiningProcessingSkills
        '
        Me.tabMiningProcessingSkills.Controls.Add(Me.tabPageOres)
        Me.tabMiningProcessingSkills.Controls.Add(Me.tabPageMoonOres)
        Me.tabMiningProcessingSkills.Controls.Add(Me.tabPageIce)
        Me.tabMiningProcessingSkills.Location = New System.Drawing.Point(157, 4)
        Me.tabMiningProcessingSkills.Name = "tabMiningProcessingSkills"
        Me.tabMiningProcessingSkills.SelectedIndex = 0
        Me.tabMiningProcessingSkills.Size = New System.Drawing.Size(318, 170)
        Me.tabMiningProcessingSkills.TabIndex = 138
        '
        'tabPageOres
        '
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing1)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing2)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing2)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing3)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing2)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing6)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing1)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing1)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing6)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing5)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing6)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing3)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing5)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing4)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing3)
        Me.tabPageOres.Controls.Add(Me.chkOreProcessing4)
        Me.tabPageOres.Controls.Add(Me.lblOreProcessing4)
        Me.tabPageOres.Controls.Add(Me.cmbOreProcessing5)
        Me.tabPageOres.Location = New System.Drawing.Point(4, 22)
        Me.tabPageOres.Name = "tabPageOres"
        Me.tabPageOres.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageOres.Size = New System.Drawing.Size(310, 144)
        Me.tabPageOres.TabIndex = 0
        Me.tabPageOres.Text = "Ore Processing"
        Me.tabPageOres.UseVisualStyleBackColor = True
        '
        'chkOreProcessing1
        '
        Me.chkOreProcessing1.AutoSize = True
        Me.chkOreProcessing1.Location = New System.Drawing.Point(10, 10)
        Me.chkOreProcessing1.Name = "chkOreProcessing1"
        Me.chkOreProcessing1.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing1.TabIndex = 95
        Me.chkOreProcessing1.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing2
        '
        Me.cmbOreProcessing2.FormattingEnabled = True
        Me.cmbOreProcessing2.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing2.Location = New System.Drawing.Point(168, 29)
        Me.cmbOreProcessing2.Name = "cmbOreProcessing2"
        Me.cmbOreProcessing2.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing2.TabIndex = 102
        '
        'lblOreProcessing2
        '
        Me.lblOreProcessing2.Location = New System.Drawing.Point(32, 32)
        Me.lblOreProcessing2.Name = "lblOreProcessing2"
        Me.lblOreProcessing2.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing2.TabIndex = 133
        Me.lblOreProcessing2.Text = "Coherent Ore Processing"
        '
        'chkOreProcessing3
        '
        Me.chkOreProcessing3.AutoSize = True
        Me.chkOreProcessing3.Location = New System.Drawing.Point(10, 54)
        Me.chkOreProcessing3.Name = "chkOreProcessing3"
        Me.chkOreProcessing3.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing3.TabIndex = 107
        Me.chkOreProcessing3.UseVisualStyleBackColor = True
        '
        'chkOreProcessing2
        '
        Me.chkOreProcessing2.AutoSize = True
        Me.chkOreProcessing2.Location = New System.Drawing.Point(10, 32)
        Me.chkOreProcessing2.Name = "chkOreProcessing2"
        Me.chkOreProcessing2.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing2.TabIndex = 101
        Me.chkOreProcessing2.UseVisualStyleBackColor = True
        '
        'chkOreProcessing6
        '
        Me.chkOreProcessing6.AutoSize = True
        Me.chkOreProcessing6.Location = New System.Drawing.Point(10, 120)
        Me.chkOreProcessing6.Name = "chkOreProcessing6"
        Me.chkOreProcessing6.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing6.TabIndex = 125
        Me.chkOreProcessing6.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing1
        '
        Me.cmbOreProcessing1.FormattingEnabled = True
        Me.cmbOreProcessing1.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing1.Location = New System.Drawing.Point(168, 7)
        Me.cmbOreProcessing1.Name = "cmbOreProcessing1"
        Me.cmbOreProcessing1.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing1.TabIndex = 96
        '
        'lblOreProcessing1
        '
        Me.lblOreProcessing1.Location = New System.Drawing.Point(32, 10)
        Me.lblOreProcessing1.Name = "lblOreProcessing1"
        Me.lblOreProcessing1.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing1.TabIndex = 127
        Me.lblOreProcessing1.Text = "Simple Ore Processing"
        '
        'lblOreProcessing6
        '
        Me.lblOreProcessing6.Location = New System.Drawing.Point(32, 120)
        Me.lblOreProcessing6.Name = "lblOreProcessing6"
        Me.lblOreProcessing6.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing6.TabIndex = 142
        Me.lblOreProcessing6.Text = "Mercoxit Ore Processing"
        '
        'chkOreProcessing5
        '
        Me.chkOreProcessing5.AutoSize = True
        Me.chkOreProcessing5.Location = New System.Drawing.Point(10, 98)
        Me.chkOreProcessing5.Name = "chkOreProcessing5"
        Me.chkOreProcessing5.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing5.TabIndex = 121
        Me.chkOreProcessing5.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing6
        '
        Me.cmbOreProcessing6.FormattingEnabled = True
        Me.cmbOreProcessing6.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing6.Location = New System.Drawing.Point(168, 117)
        Me.cmbOreProcessing6.Name = "cmbOreProcessing6"
        Me.cmbOreProcessing6.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing6.TabIndex = 126
        '
        'lblOreProcessing3
        '
        Me.lblOreProcessing3.Location = New System.Drawing.Point(32, 54)
        Me.lblOreProcessing3.Name = "lblOreProcessing3"
        Me.lblOreProcessing3.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing3.TabIndex = 130
        Me.lblOreProcessing3.Text = "Variegated Ore Processing"
        '
        'lblOreProcessing5
        '
        Me.lblOreProcessing5.Location = New System.Drawing.Point(32, 98)
        Me.lblOreProcessing5.Name = "lblOreProcessing5"
        Me.lblOreProcessing5.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing5.TabIndex = 141
        Me.lblOreProcessing5.Text = "Abyssal Ore Processing"
        '
        'cmbOreProcessing4
        '
        Me.cmbOreProcessing4.FormattingEnabled = True
        Me.cmbOreProcessing4.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing4.Location = New System.Drawing.Point(168, 73)
        Me.cmbOreProcessing4.Name = "cmbOreProcessing4"
        Me.cmbOreProcessing4.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing4.TabIndex = 114
        '
        'cmbOreProcessing3
        '
        Me.cmbOreProcessing3.FormattingEnabled = True
        Me.cmbOreProcessing3.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing3.Location = New System.Drawing.Point(168, 51)
        Me.cmbOreProcessing3.Name = "cmbOreProcessing3"
        Me.cmbOreProcessing3.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing3.TabIndex = 108
        '
        'chkOreProcessing4
        '
        Me.chkOreProcessing4.AutoSize = True
        Me.chkOreProcessing4.Location = New System.Drawing.Point(10, 76)
        Me.chkOreProcessing4.Name = "chkOreProcessing4"
        Me.chkOreProcessing4.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing4.TabIndex = 113
        Me.chkOreProcessing4.UseVisualStyleBackColor = True
        '
        'lblOreProcessing4
        '
        Me.lblOreProcessing4.Location = New System.Drawing.Point(32, 76)
        Me.lblOreProcessing4.Name = "lblOreProcessing4"
        Me.lblOreProcessing4.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing4.TabIndex = 139
        Me.lblOreProcessing4.Text = "Complex Ore Processing"
        '
        'cmbOreProcessing5
        '
        Me.cmbOreProcessing5.FormattingEnabled = True
        Me.cmbOreProcessing5.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing5.Location = New System.Drawing.Point(168, 95)
        Me.cmbOreProcessing5.Name = "cmbOreProcessing5"
        Me.cmbOreProcessing5.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing5.TabIndex = 122
        '
        'tabPageMoonOres
        '
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing7)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing8)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing10)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing11)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing11)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing9)
        Me.tabPageMoonOres.Controls.Add(Me.lblOreProcessing9)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing8)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing10)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing7)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing10)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing7)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing9)
        Me.tabPageMoonOres.Controls.Add(Me.chkOreProcessing11)
        Me.tabPageMoonOres.Controls.Add(Me.cmbOreProcessing8)
        Me.tabPageMoonOres.Location = New System.Drawing.Point(4, 22)
        Me.tabPageMoonOres.Name = "tabPageMoonOres"
        Me.tabPageMoonOres.Size = New System.Drawing.Size(310, 144)
        Me.tabPageMoonOres.TabIndex = 2
        Me.tabPageMoonOres.Text = "Moon Ore Processing"
        Me.tabPageMoonOres.UseVisualStyleBackColor = True
        '
        'lblOreProcessing7
        '
        Me.lblOreProcessing7.Location = New System.Drawing.Point(31, 10)
        Me.lblOreProcessing7.Name = "lblOreProcessing7"
        Me.lblOreProcessing7.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing7.TabIndex = 148
        Me.lblOreProcessing7.Text = "Ubiquitous Moon Ore Processing"
        '
        'lblOreProcessing8
        '
        Me.lblOreProcessing8.Location = New System.Drawing.Point(31, 32)
        Me.lblOreProcessing8.Name = "lblOreProcessing8"
        Me.lblOreProcessing8.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing8.TabIndex = 149
        Me.lblOreProcessing8.Text = "Uncommon Moon Ore Processing"
        '
        'lblOreProcessing10
        '
        Me.lblOreProcessing10.Location = New System.Drawing.Point(31, 76)
        Me.lblOreProcessing10.Name = "lblOreProcessing10"
        Me.lblOreProcessing10.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing10.TabIndex = 145
        Me.lblOreProcessing10.Text = "Common Moon Ore Processing"
        '
        'lblOreProcessing11
        '
        Me.lblOreProcessing11.Location = New System.Drawing.Point(31, 98)
        Me.lblOreProcessing11.Name = "lblOreProcessing11"
        Me.lblOreProcessing11.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing11.TabIndex = 147
        Me.lblOreProcessing11.Text = "Rare Moon Ore Processing"
        '
        'cmbOreProcessing11
        '
        Me.cmbOreProcessing11.FormattingEnabled = True
        Me.cmbOreProcessing11.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing11.Location = New System.Drawing.Point(202, 95)
        Me.cmbOreProcessing11.Name = "cmbOreProcessing11"
        Me.cmbOreProcessing11.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing11.TabIndex = 140
        '
        'chkOreProcessing9
        '
        Me.chkOreProcessing9.AutoSize = True
        Me.chkOreProcessing9.Location = New System.Drawing.Point(10, 54)
        Me.chkOreProcessing9.Name = "chkOreProcessing9"
        Me.chkOreProcessing9.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing9.TabIndex = 137
        Me.chkOreProcessing9.UseVisualStyleBackColor = True
        '
        'lblOreProcessing9
        '
        Me.lblOreProcessing9.Location = New System.Drawing.Point(31, 54)
        Me.lblOreProcessing9.Name = "lblOreProcessing9"
        Me.lblOreProcessing9.Size = New System.Drawing.Size(167, 13)
        Me.lblOreProcessing9.TabIndex = 146
        Me.lblOreProcessing9.Text = "Exceptional Moon Ore Processing"
        '
        'chkOreProcessing8
        '
        Me.chkOreProcessing8.AutoSize = True
        Me.chkOreProcessing8.Location = New System.Drawing.Point(10, 32)
        Me.chkOreProcessing8.Name = "chkOreProcessing8"
        Me.chkOreProcessing8.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing8.TabIndex = 143
        Me.chkOreProcessing8.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing10
        '
        Me.cmbOreProcessing10.FormattingEnabled = True
        Me.cmbOreProcessing10.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing10.Location = New System.Drawing.Point(202, 73)
        Me.cmbOreProcessing10.Name = "cmbOreProcessing10"
        Me.cmbOreProcessing10.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing10.TabIndex = 136
        '
        'cmbOreProcessing7
        '
        Me.cmbOreProcessing7.FormattingEnabled = True
        Me.cmbOreProcessing7.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing7.Location = New System.Drawing.Point(202, 7)
        Me.cmbOreProcessing7.Name = "cmbOreProcessing7"
        Me.cmbOreProcessing7.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing7.TabIndex = 142
        '
        'chkOreProcessing10
        '
        Me.chkOreProcessing10.AutoSize = True
        Me.chkOreProcessing10.Location = New System.Drawing.Point(10, 76)
        Me.chkOreProcessing10.Name = "chkOreProcessing10"
        Me.chkOreProcessing10.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing10.TabIndex = 135
        Me.chkOreProcessing10.UseVisualStyleBackColor = True
        '
        'chkOreProcessing7
        '
        Me.chkOreProcessing7.AutoSize = True
        Me.chkOreProcessing7.Location = New System.Drawing.Point(10, 10)
        Me.chkOreProcessing7.Name = "chkOreProcessing7"
        Me.chkOreProcessing7.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing7.TabIndex = 141
        Me.chkOreProcessing7.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing9
        '
        Me.cmbOreProcessing9.FormattingEnabled = True
        Me.cmbOreProcessing9.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing9.Location = New System.Drawing.Point(202, 51)
        Me.cmbOreProcessing9.Name = "cmbOreProcessing9"
        Me.cmbOreProcessing9.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing9.TabIndex = 138
        '
        'chkOreProcessing11
        '
        Me.chkOreProcessing11.AutoSize = True
        Me.chkOreProcessing11.Location = New System.Drawing.Point(10, 98)
        Me.chkOreProcessing11.Name = "chkOreProcessing11"
        Me.chkOreProcessing11.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing11.TabIndex = 139
        Me.chkOreProcessing11.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing8
        '
        Me.cmbOreProcessing8.FormattingEnabled = True
        Me.cmbOreProcessing8.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing8.Location = New System.Drawing.Point(202, 29)
        Me.cmbOreProcessing8.Name = "cmbOreProcessing8"
        Me.cmbOreProcessing8.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing8.TabIndex = 144
        '
        'tabPageIce
        '
        Me.tabPageIce.Controls.Add(Me.cmbOreProcessing12)
        Me.tabPageIce.Controls.Add(Me.lblOreProcessing12)
        Me.tabPageIce.Controls.Add(Me.chkOreProcessing12)
        Me.tabPageIce.Location = New System.Drawing.Point(4, 22)
        Me.tabPageIce.Name = "tabPageIce"
        Me.tabPageIce.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageIce.Size = New System.Drawing.Size(310, 144)
        Me.tabPageIce.TabIndex = 3
        Me.tabPageIce.Text = "Ice Processing"
        Me.tabPageIce.UseVisualStyleBackColor = True
        '
        'cmbOreProcessing12
        '
        Me.cmbOreProcessing12.FormattingEnabled = True
        Me.cmbOreProcessing12.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbOreProcessing12.Location = New System.Drawing.Point(168, 7)
        Me.cmbOreProcessing12.Name = "cmbOreProcessing12"
        Me.cmbOreProcessing12.Size = New System.Drawing.Size(36, 21)
        Me.cmbOreProcessing12.TabIndex = 147
        '
        'lblOreProcessing12
        '
        Me.lblOreProcessing12.Location = New System.Drawing.Point(29, 11)
        Me.lblOreProcessing12.Name = "lblOreProcessing12"
        Me.lblOreProcessing12.Size = New System.Drawing.Size(133, 13)
        Me.lblOreProcessing12.TabIndex = 148
        Me.lblOreProcessing12.Text = "Ice Processing"
        '
        'chkOreProcessing12
        '
        Me.chkOreProcessing12.AutoSize = True
        Me.chkOreProcessing12.Location = New System.Drawing.Point(10, 10)
        Me.chkOreProcessing12.Name = "chkOreProcessing12"
        Me.chkOreProcessing12.Size = New System.Drawing.Size(15, 14)
        Me.chkOreProcessing12.TabIndex = 146
        Me.chkOreProcessing12.UseVisualStyleBackColor = True
        '
        'btnClear2
        '
        Me.btnClear2.Location = New System.Drawing.Point(77, 113)
        Me.btnClear2.Name = "btnClear2"
        Me.btnClear2.Size = New System.Drawing.Size(74, 30)
        Me.btnClear2.TabIndex = 137
        Me.btnClear2.Text = "Clear"
        Me.btnClear2.UseVisualStyleBackColor = True
        '
        'btnCopyOutput2
        '
        Me.btnCopyOutput2.Location = New System.Drawing.Point(4, 143)
        Me.btnCopyOutput2.Name = "btnCopyOutput2"
        Me.btnCopyOutput2.Size = New System.Drawing.Size(74, 30)
        Me.btnCopyOutput2.TabIndex = 136
        Me.btnCopyOutput2.Text = "Copy Output"
        Me.btnCopyOutput2.UseVisualStyleBackColor = True
        '
        'btnReprocess2
        '
        Me.btnReprocess2.Location = New System.Drawing.Point(4, 113)
        Me.btnReprocess2.Name = "btnReprocess2"
        Me.btnReprocess2.Size = New System.Drawing.Size(74, 30)
        Me.btnReprocess2.TabIndex = 134
        Me.btnReprocess2.Text = "Reprocess"
        Me.btnReprocess2.UseVisualStyleBackColor = True
        '
        'btnClose2
        '
        Me.btnClose2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose2.Location = New System.Drawing.Point(77, 143)
        Me.btnClose2.Name = "btnClose2"
        Me.btnClose2.Size = New System.Drawing.Size(74, 30)
        Me.btnClose2.TabIndex = 135
        Me.btnClose2.Text = "Close"
        Me.btnClose2.UseVisualStyleBackColor = True
        '
        'chkToggle
        '
        Me.chkToggle.AutoSize = True
        Me.chkToggle.BackColor = System.Drawing.Color.Transparent
        Me.chkToggle.Location = New System.Drawing.Point(444, 221)
        Me.chkToggle.Name = "chkToggle"
        Me.chkToggle.Size = New System.Drawing.Size(99, 17)
        Me.chkToggle.TabIndex = 255
        Me.chkToggle.Text = "Check All Items"
        Me.chkToggle.UseVisualStyleBackColor = False
        '
        'lblTotalItemList
        '
        Me.lblTotalItemList.AutoSize = True
        Me.lblTotalItemList.Location = New System.Drawing.Point(27, 615)
        Me.lblTotalItemList.Name = "lblTotalItemList"
        Me.lblTotalItemList.Size = New System.Drawing.Size(106, 13)
        Me.lblTotalItemList.TabIndex = 256
        Me.lblTotalItemList.Text = "Item List Total Value:"
        Me.lblTotalItemList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReprocessingVolumeTotal
        '
        Me.lblReprocessingVolumeTotal.AutoSize = True
        Me.lblReprocessingVolumeTotal.Location = New System.Drawing.Point(279, 637)
        Me.lblReprocessingVolumeTotal.Name = "lblReprocessingVolumeTotal"
        Me.lblReprocessingVolumeTotal.Size = New System.Drawing.Size(148, 13)
        Me.lblReprocessingVolumeTotal.TabIndex = 257
        Me.lblReprocessingVolumeTotal.Text = "Reprocessing Output Volume:"
        Me.lblReprocessingVolumeTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReprocessingTotalRate
        '
        Me.lblReprocessingTotalRate.AutoSize = True
        Me.lblReprocessingTotalRate.Location = New System.Drawing.Point(25, 637)
        Me.lblReprocessingTotalRate.Name = "lblReprocessingTotalRate"
        Me.lblReprocessingTotalRate.Size = New System.Drawing.Size(108, 13)
        Me.lblReprocessingTotalRate.TabIndex = 258
        Me.lblReprocessingTotalRate.Text = "Return Rate Percent:"
        Me.lblReprocessingTotalRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReprocessingTotal
        '
        Me.lblReprocessingTotal.AutoSize = True
        Me.lblReprocessingTotal.Location = New System.Drawing.Point(287, 615)
        Me.lblReprocessingTotal.Name = "lblReprocessingTotal"
        Me.lblReprocessingTotal.Size = New System.Drawing.Size(140, 13)
        Me.lblReprocessingTotal.TabIndex = 259
        Me.lblReprocessingTotal.Text = "Reprocessing Output Value:"
        Me.lblReprocessingTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblListTotalValueOutput
        '
        Me.lblListTotalValueOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblListTotalValueOutput.Location = New System.Drawing.Point(139, 612)
        Me.lblListTotalValueOutput.Name = "lblListTotalValueOutput"
        Me.lblListTotalValueOutput.Size = New System.Drawing.Size(127, 18)
        Me.lblListTotalValueOutput.TabIndex = 260
        Me.lblListTotalValueOutput.Text = "-"
        Me.lblListTotalValueOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReturnRatePercentOutput
        '
        Me.lblReturnRatePercentOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReturnRatePercentOutput.Location = New System.Drawing.Point(139, 634)
        Me.lblReturnRatePercentOutput.Name = "lblReturnRatePercentOutput"
        Me.lblReturnRatePercentOutput.Size = New System.Drawing.Size(127, 18)
        Me.lblReturnRatePercentOutput.TabIndex = 261
        Me.lblReturnRatePercentOutput.Text = "-"
        Me.lblReturnRatePercentOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReprocessingValueOutput
        '
        Me.lblReprocessingValueOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReprocessingValueOutput.Location = New System.Drawing.Point(433, 612)
        Me.lblReprocessingValueOutput.Name = "lblReprocessingValueOutput"
        Me.lblReprocessingValueOutput.Size = New System.Drawing.Size(127, 18)
        Me.lblReprocessingValueOutput.TabIndex = 262
        Me.lblReprocessingValueOutput.Text = "-"
        Me.lblReprocessingValueOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReprocessingVolumeOutput
        '
        Me.lblReprocessingVolumeOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReprocessingVolumeOutput.Location = New System.Drawing.Point(433, 634)
        Me.lblReprocessingVolumeOutput.Name = "lblReprocessingVolumeOutput"
        Me.lblReprocessingVolumeOutput.Size = New System.Drawing.Size(127, 18)
        Me.lblReprocessingVolumeOutput.TabIndex = 263
        Me.lblReprocessingVolumeOutput.Text = "-"
        Me.lblReprocessingVolumeOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ReprocessingFacility
        '
        Me.ReprocessingFacility.BackColor = System.Drawing.Color.Transparent
        Me.ReprocessingFacility.Location = New System.Drawing.Point(6, 6)
        Me.ReprocessingFacility.Name = "ReprocessingFacility"
        Me.ReprocessingFacility.Size = New System.Drawing.Size(303, 105)
        Me.ReprocessingFacility.TabIndex = 43
        '
        'lstItemstoRefine
        '
        Me.lstItemstoRefine.CheckBoxes = True
        Me.lstItemstoRefine.FullRowSelect = True
        Me.lstItemstoRefine.GridLines = True
        Me.lstItemstoRefine.HideSelection = False
        Me.lstItemstoRefine.Location = New System.Drawing.Point(7, 239)
        Me.lstItemstoRefine.MultiSelect = False
        Me.lstItemstoRefine.Name = "lstItemstoRefine"
        Me.lstItemstoRefine.Size = New System.Drawing.Size(570, 165)
        Me.lstItemstoRefine.TabIndex = 42
        Me.lstItemstoRefine.TabStop = False
        Me.lstItemstoRefine.UseCompatibleStateImageBehavior = False
        Me.lstItemstoRefine.View = System.Windows.Forms.View.Details
        '
        'lstRefineOutput
        '
        Me.lstRefineOutput.FullRowSelect = True
        Me.lstRefineOutput.GridLines = True
        Me.lstRefineOutput.HideSelection = False
        Me.lstRefineOutput.Location = New System.Drawing.Point(7, 433)
        Me.lstRefineOutput.MultiSelect = False
        Me.lstRefineOutput.Name = "lstRefineOutput"
        Me.lstRefineOutput.Size = New System.Drawing.Size(570, 165)
        Me.lstRefineOutput.TabIndex = 41
        Me.lstRefineOutput.TabStop = False
        Me.lstRefineOutput.UseCompatibleStateImageBehavior = False
        Me.lstRefineOutput.View = System.Windows.Forms.View.Details
        '
        'frmReprocessingPlant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(584, 661)
        Me.Controls.Add(Me.lblReprocessingVolumeOutput)
        Me.Controls.Add(Me.lblReprocessingValueOutput)
        Me.Controls.Add(Me.lblReturnRatePercentOutput)
        Me.Controls.Add(Me.lblListTotalValueOutput)
        Me.Controls.Add(Me.lblReprocessingVolumeTotal)
        Me.Controls.Add(Me.lblReprocessingTotal)
        Me.Controls.Add(Me.lblReprocessingTotalRate)
        Me.Controls.Add(Me.chkToggle)
        Me.Controls.Add(Me.tabRefinery)
        Me.Controls.Add(Me.lblTotalItemList)
        Me.Controls.Add(Me.lblItemList)
        Me.Controls.Add(Me.lblReprocessingOutput)
        Me.Controls.Add(Me.lstItemstoRefine)
        Me.Controls.Add(Me.lstRefineOutput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReprocessingPlant"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reprocessing Plant"
        Me.gbRefineYields.ResumeLayout(False)
        Me.gbRefineYields.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tabRefinery.ResumeLayout(False)
        Me.tabpMain.ResumeLayout(False)
        Me.tabpMain.PerformLayout()
        Me.tabpSkills.ResumeLayout(False)
        Me.tabMiningProcessingSkills.ResumeLayout(False)
        Me.tabPageOres.ResumeLayout(False)
        Me.tabPageOres.PerformLayout()
        Me.tabPageMoonOres.ResumeLayout(False)
        Me.tabPageMoonOres.PerformLayout()
        Me.tabPageIce.ResumeLayout(False)
        Me.tabPageIce.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReprocess As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lstRefineOutput As MyListView
    Friend WithEvents lstItemstoRefine As MyListView
    Friend WithEvents ReprocessingFacility As ManufacturingFacility
    Friend WithEvents btnShowAssets As Button
    Friend WithEvents btnCopyPasteAssets As Button
    Friend WithEvents btnSelectAssets As Button
    Friend WithEvents cmbReprocessing As ComboBox
    Friend WithEvents lblRefining As Label
    Friend WithEvents cmbReprocessingEff As ComboBox
    Friend WithEvents lblRefineryEfficiency As Label
    Friend WithEvents gbRefineYields As GroupBox
    Friend WithEvents lblIce As Label
    Friend WithEvents lblScrap As Label
    Friend WithEvents lblOreRate As Label
    Friend WithEvents lblMoonRate As Label
    Friend WithEvents lblOre As Label
    Friend WithEvents lblMoonOre As Label
    Friend WithEvents cmbScrapMetalProcessing As ComboBox
    Friend WithEvents lblScrapMetalProcessing As Label
    Friend WithEvents lblReprocessingOutput As Label
    Friend WithEvents lblItemList As Label
    Friend WithEvents lblScrapRate As Label
    Friend WithEvents lblIceRate As Label
    Friend WithEvents lblItemSelect As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tabRefinery As TabControl
    Friend WithEvents tabpMain As TabPage
    Friend WithEvents tabpSkills As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBeanCounterRefining As ComboBox
    Friend WithEvents btnCopyOutput As Button
    Friend WithEvents chkRecursiveRefine As CheckBox
    Friend WithEvents chkToggle As CheckBox
    Friend WithEvents lblTotalItemList As Label
    Friend WithEvents lblReprocessingVolumeTotal As Label
    Friend WithEvents lblReprocessingTotalRate As Label
    Friend WithEvents lblReprocessingTotal As Label
    Friend WithEvents lblListTotalValueOutput As Label
    Friend WithEvents lblReturnRatePercentOutput As Label
    Friend WithEvents lblReprocessingValueOutput As Label
    Friend WithEvents lblReprocessingVolumeOutput As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents btnClear2 As Button
    Friend WithEvents btnCopyOutput2 As Button
    Friend WithEvents btnReprocess2 As Button
    Friend WithEvents btnClose2 As Button
    Friend WithEvents tabMiningProcessingSkills As TabControl
    Friend WithEvents tabPageOres As TabPage
    Friend WithEvents chkOreProcessing1 As CheckBox
    Friend WithEvents cmbOreProcessing2 As ComboBox
    Friend WithEvents lblOreProcessing2 As Label
    Friend WithEvents chkOreProcessing3 As CheckBox
    Friend WithEvents chkOreProcessing2 As CheckBox
    Friend WithEvents chkOreProcessing6 As CheckBox
    Friend WithEvents cmbOreProcessing1 As ComboBox
    Friend WithEvents lblOreProcessing1 As Label
    Friend WithEvents lblOreProcessing6 As Label
    Friend WithEvents chkOreProcessing5 As CheckBox
    Friend WithEvents cmbOreProcessing6 As ComboBox
    Friend WithEvents lblOreProcessing3 As Label
    Friend WithEvents lblOreProcessing5 As Label
    Friend WithEvents cmbOreProcessing4 As ComboBox
    Friend WithEvents cmbOreProcessing3 As ComboBox
    Friend WithEvents chkOreProcessing4 As CheckBox
    Friend WithEvents lblOreProcessing4 As Label
    Friend WithEvents cmbOreProcessing5 As ComboBox
    Friend WithEvents tabPageMoonOres As TabPage
    Friend WithEvents lblOreProcessing7 As Label
    Friend WithEvents lblOreProcessing8 As Label
    Friend WithEvents lblOreProcessing10 As Label
    Friend WithEvents lblOreProcessing11 As Label
    Friend WithEvents cmbOreProcessing11 As ComboBox
    Friend WithEvents chkOreProcessing9 As CheckBox
    Friend WithEvents lblOreProcessing9 As Label
    Friend WithEvents chkOreProcessing8 As CheckBox
    Friend WithEvents cmbOreProcessing10 As ComboBox
    Friend WithEvents cmbOreProcessing7 As ComboBox
    Friend WithEvents chkOreProcessing10 As CheckBox
    Friend WithEvents chkOreProcessing7 As CheckBox
    Friend WithEvents cmbOreProcessing9 As ComboBox
    Friend WithEvents chkOreProcessing11 As CheckBox
    Friend WithEvents cmbOreProcessing8 As ComboBox
    Friend WithEvents tabPageIce As TabPage
    Friend WithEvents cmbOreProcessing12 As ComboBox
    Friend WithEvents lblOreProcessing12 As Label
    Friend WithEvents chkOreProcessing12 As CheckBox
End Class
