<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventionMonitor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventionMonitor))
        Me.lstInventionItems = New System.Windows.Forms.ListView()
        Me.dtpInventionStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpInventionEndDate = New System.Windows.Forms.DateTimePicker()
        Me.gbBPImage = New System.Windows.Forms.GroupBox()
        Me.pictInvention = New System.Windows.Forms.PictureBox()
        Me.gbInventionStats = New System.Windows.Forms.GroupBox()
        Me.lblTotalSuccesses = New System.Windows.Forms.Label()
        Me.lblSuccessRate = New System.Windows.Forms.Label()
        Me.lblTotalSuccesses1 = New System.Windows.Forms.Label()
        Me.lblSuccessRate1 = New System.Windows.Forms.Label()
        Me.lblTotalAttempts = New System.Windows.Forms.Label()
        Me.lblTotalAttempts1 = New System.Windows.Forms.Label()
        Me.gbIndustryJobType = New System.Windows.Forms.GroupBox()
        Me.rbtnBothJobs = New System.Windows.Forms.RadioButton()
        Me.rbtnCorpJobs = New System.Windows.Forms.RadioButton()
        Me.rbtnPersonalJobs = New System.Windows.Forms.RadioButton()
        Me.btnUpdateJobs = New System.Windows.Forms.Button()
        Me.lblSelectedBlueprint = New System.Windows.Forms.Label()
        Me.gbSkills = New System.Windows.Forms.GroupBox()
        Me.lblSkill3 = New System.Windows.Forms.Label()
        Me.cmbSkill3 = New System.Windows.Forms.ComboBox()
        Me.cmbBPDecryptor = New System.Windows.Forms.ComboBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.lblBPDecryptor = New System.Windows.Forms.Label()
        Me.lblSuccessChance = New System.Windows.Forms.Label()
        Me.lblSkill1 = New System.Windows.Forms.Label()
        Me.cmbSkill1 = New System.Windows.Forms.ComboBox()
        Me.lblSkill2 = New System.Windows.Forms.Label()
        Me.cmbSkill2 = New System.Windows.Forms.ComboBox()
        Me.lblSuccessChance1 = New System.Windows.Forms.Label()
        Me.lblSelectedBP = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.gbMain = New System.Windows.Forms.GroupBox()
        Me.gbDateSelect = New System.Windows.Forms.GroupBox()
        Me.gbInventionMonitor = New System.Windows.Forms.GroupBox()
        Me.gbBPImage.SuspendLayout()
        CType(Me.pictInvention, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbInventionStats.SuspendLayout()
        Me.gbIndustryJobType.SuspendLayout()
        Me.gbSkills.SuspendLayout()
        Me.gbMain.SuspendLayout()
        Me.gbDateSelect.SuspendLayout()
        Me.gbInventionMonitor.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstInventionItems
        '
        Me.lstInventionItems.FullRowSelect = True
        Me.lstInventionItems.GridLines = True
        Me.lstInventionItems.HideSelection = False
        Me.lstInventionItems.Location = New System.Drawing.Point(17, 229)
        Me.lstInventionItems.MultiSelect = False
        Me.lstInventionItems.Name = "lstInventionItems"
        Me.lstInventionItems.Size = New System.Drawing.Size(597, 206)
        Me.lstInventionItems.TabIndex = 8
        Me.lstInventionItems.UseCompatibleStateImageBehavior = False
        Me.lstInventionItems.View = System.Windows.Forms.View.Details
        '
        'dtpInventionStartDate
        '
        Me.dtpInventionStartDate.CustomFormat = ""
        Me.dtpInventionStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInventionStartDate.Location = New System.Drawing.Point(85, 19)
        Me.dtpInventionStartDate.Name = "dtpInventionStartDate"
        Me.dtpInventionStartDate.Size = New System.Drawing.Size(99, 20)
        Me.dtpInventionStartDate.TabIndex = 9
        '
        'dtpInventionEndDate
        '
        Me.dtpInventionEndDate.CustomFormat = ""
        Me.dtpInventionEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInventionEndDate.Location = New System.Drawing.Point(251, 19)
        Me.dtpInventionEndDate.Name = "dtpInventionEndDate"
        Me.dtpInventionEndDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpInventionEndDate.TabIndex = 10
        Me.dtpInventionEndDate.Value = New Date(2014, 1, 29, 21, 10, 0, 0)
        '
        'gbBPImage
        '
        Me.gbBPImage.Controls.Add(Me.pictInvention)
        Me.gbBPImage.Location = New System.Drawing.Point(282, 52)
        Me.gbBPImage.Name = "gbBPImage"
        Me.gbBPImage.Size = New System.Drawing.Size(81, 89)
        Me.gbBPImage.TabIndex = 65
        Me.gbBPImage.TabStop = False
        '
        'pictInvention
        '
        Me.pictInvention.BackColor = System.Drawing.Color.White
        Me.pictInvention.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictInvention.Location = New System.Drawing.Point(7, 13)
        Me.pictInvention.Name = "pictInvention"
        Me.pictInvention.Size = New System.Drawing.Size(68, 69)
        Me.pictInvention.TabIndex = 66
        Me.pictInvention.TabStop = False
        '
        'gbInventionStats
        '
        Me.gbInventionStats.Controls.Add(Me.lblTotalSuccesses)
        Me.gbInventionStats.Controls.Add(Me.lblSuccessRate)
        Me.gbInventionStats.Controls.Add(Me.lblTotalSuccesses1)
        Me.gbInventionStats.Controls.Add(Me.lblSuccessRate1)
        Me.gbInventionStats.Controls.Add(Me.lblTotalAttempts)
        Me.gbInventionStats.Controls.Add(Me.lblTotalAttempts1)
        Me.gbInventionStats.Location = New System.Drawing.Point(123, 52)
        Me.gbInventionStats.Name = "gbInventionStats"
        Me.gbInventionStats.Size = New System.Drawing.Size(153, 89)
        Me.gbInventionStats.TabIndex = 64
        Me.gbInventionStats.TabStop = False
        Me.gbInventionStats.Text = "Stats:"
        '
        'lblTotalSuccesses
        '
        Me.lblTotalSuccesses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalSuccesses.Location = New System.Drawing.Point(95, 32)
        Me.lblTotalSuccesses.Name = "lblTotalSuccesses"
        Me.lblTotalSuccesses.Size = New System.Drawing.Size(47, 17)
        Me.lblTotalSuccesses.TabIndex = 58
        Me.lblTotalSuccesses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSuccessRate
        '
        Me.lblSuccessRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuccessRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuccessRate.Location = New System.Drawing.Point(55, 51)
        Me.lblSuccessRate.Name = "lblSuccessRate"
        Me.lblSuccessRate.Size = New System.Drawing.Size(87, 30)
        Me.lblSuccessRate.TabIndex = 56
        Me.lblSuccessRate.Text = "86.99%"
        Me.lblSuccessRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalSuccesses1
        '
        Me.lblTotalSuccesses1.Location = New System.Drawing.Point(6, 32)
        Me.lblTotalSuccesses1.Name = "lblTotalSuccesses1"
        Me.lblTotalSuccesses1.Size = New System.Drawing.Size(92, 17)
        Me.lblTotalSuccesses1.TabIndex = 59
        Me.lblTotalSuccesses1.Text = "Total Successes:"
        Me.lblTotalSuccesses1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuccessRate1
        '
        Me.lblSuccessRate1.Location = New System.Drawing.Point(6, 51)
        Me.lblSuccessRate1.Name = "lblSuccessRate1"
        Me.lblSuccessRate1.Size = New System.Drawing.Size(50, 30)
        Me.lblSuccessRate1.TabIndex = 53
        Me.lblSuccessRate1.Text = "Success Rate:"
        Me.lblSuccessRate1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalAttempts
        '
        Me.lblTotalAttempts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalAttempts.Location = New System.Drawing.Point(95, 13)
        Me.lblTotalAttempts.Name = "lblTotalAttempts"
        Me.lblTotalAttempts.Size = New System.Drawing.Size(47, 17)
        Me.lblTotalAttempts.TabIndex = 55
        Me.lblTotalAttempts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalAttempts1
        '
        Me.lblTotalAttempts1.Location = New System.Drawing.Point(6, 13)
        Me.lblTotalAttempts1.Name = "lblTotalAttempts1"
        Me.lblTotalAttempts1.Size = New System.Drawing.Size(83, 17)
        Me.lblTotalAttempts1.TabIndex = 57
        Me.lblTotalAttempts1.Text = "Total Attempts:"
        Me.lblTotalAttempts1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbIndustryJobType
        '
        Me.gbIndustryJobType.Controls.Add(Me.rbtnBothJobs)
        Me.gbIndustryJobType.Controls.Add(Me.rbtnCorpJobs)
        Me.gbIndustryJobType.Controls.Add(Me.rbtnPersonalJobs)
        Me.gbIndustryJobType.Location = New System.Drawing.Point(9, 52)
        Me.gbIndustryJobType.Name = "gbIndustryJobType"
        Me.gbIndustryJobType.Size = New System.Drawing.Size(108, 89)
        Me.gbIndustryJobType.TabIndex = 63
        Me.gbIndustryJobType.TabStop = False
        Me.gbIndustryJobType.Text = "Industry Job Type:"
        '
        'rbtnBothJobs
        '
        Me.rbtnBothJobs.AutoSize = True
        Me.rbtnBothJobs.Location = New System.Drawing.Point(15, 60)
        Me.rbtnBothJobs.Name = "rbtnBothJobs"
        Me.rbtnBothJobs.Size = New System.Drawing.Size(47, 17)
        Me.rbtnBothJobs.TabIndex = 2
        Me.rbtnBothJobs.TabStop = True
        Me.rbtnBothJobs.Text = "Both"
        Me.rbtnBothJobs.UseVisualStyleBackColor = True
        '
        'rbtnCorpJobs
        '
        Me.rbtnCorpJobs.AutoSize = True
        Me.rbtnCorpJobs.Location = New System.Drawing.Point(15, 39)
        Me.rbtnCorpJobs.Name = "rbtnCorpJobs"
        Me.rbtnCorpJobs.Size = New System.Drawing.Size(79, 17)
        Me.rbtnCorpJobs.TabIndex = 1
        Me.rbtnCorpJobs.TabStop = True
        Me.rbtnCorpJobs.Text = "Corporation"
        Me.rbtnCorpJobs.UseVisualStyleBackColor = True
        '
        'rbtnPersonalJobs
        '
        Me.rbtnPersonalJobs.AutoSize = True
        Me.rbtnPersonalJobs.Location = New System.Drawing.Point(15, 18)
        Me.rbtnPersonalJobs.Name = "rbtnPersonalJobs"
        Me.rbtnPersonalJobs.Size = New System.Drawing.Size(66, 17)
        Me.rbtnPersonalJobs.TabIndex = 0
        Me.rbtnPersonalJobs.TabStop = True
        Me.rbtnPersonalJobs.Text = "Personal"
        Me.rbtnPersonalJobs.UseVisualStyleBackColor = True
        '
        'btnUpdateJobs
        '
        Me.btnUpdateJobs.Location = New System.Drawing.Point(119, 126)
        Me.btnUpdateJobs.Name = "btnUpdateJobs"
        Me.btnUpdateJobs.Size = New System.Drawing.Size(88, 25)
        Me.btnUpdateJobs.TabIndex = 62
        Me.btnUpdateJobs.Text = "Update Jobs"
        Me.btnUpdateJobs.UseVisualStyleBackColor = True
        '
        'lblSelectedBlueprint
        '
        Me.lblSelectedBlueprint.AutoSize = True
        Me.lblSelectedBlueprint.Location = New System.Drawing.Point(6, 16)
        Me.lblSelectedBlueprint.Name = "lblSelectedBlueprint"
        Me.lblSelectedBlueprint.Size = New System.Drawing.Size(141, 13)
        Me.lblSelectedBlueprint.TabIndex = 61
        Me.lblSelectedBlueprint.Text = "Selected Invented Blueprint:"
        '
        'gbSkills
        '
        Me.gbSkills.Controls.Add(Me.btnUpdateJobs)
        Me.gbSkills.Controls.Add(Me.lblSkill3)
        Me.gbSkills.Controls.Add(Me.cmbSkill3)
        Me.gbSkills.Controls.Add(Me.cmbBPDecryptor)
        Me.gbSkills.Controls.Add(Me.btnReset)
        Me.gbSkills.Controls.Add(Me.lblBPDecryptor)
        Me.gbSkills.Controls.Add(Me.lblSuccessChance)
        Me.gbSkills.Controls.Add(Me.lblSkill1)
        Me.gbSkills.Controls.Add(Me.cmbSkill1)
        Me.gbSkills.Controls.Add(Me.lblSkill2)
        Me.gbSkills.Controls.Add(Me.cmbSkill2)
        Me.gbSkills.Controls.Add(Me.lblSuccessChance1)
        Me.gbSkills.Location = New System.Drawing.Point(396, 15)
        Me.gbSkills.Name = "gbSkills"
        Me.gbSkills.Size = New System.Drawing.Size(218, 208)
        Me.gbSkills.TabIndex = 12
        Me.gbSkills.TabStop = False
        Me.gbSkills.Text = "Success Formula Variables:"
        '
        'lblSkill3
        '
        Me.lblSkill3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSkill3.Location = New System.Drawing.Point(10, 62)
        Me.lblSkill3.Name = "lblSkill3"
        Me.lblSkill3.Size = New System.Drawing.Size(158, 17)
        Me.lblSkill3.TabIndex = 50
        '
        'cmbSkill3
        '
        Me.cmbSkill3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSkill3.FormattingEnabled = True
        Me.cmbSkill3.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbSkill3.Location = New System.Drawing.Point(171, 60)
        Me.cmbSkill3.Name = "cmbSkill3"
        Me.cmbSkill3.Size = New System.Drawing.Size(36, 21)
        Me.cmbSkill3.TabIndex = 51
        '
        'cmbBPDecryptor
        '
        Me.cmbBPDecryptor.FormattingEnabled = True
        Me.cmbBPDecryptor.ItemHeight = 13
        Me.cmbBPDecryptor.Location = New System.Drawing.Point(10, 101)
        Me.cmbBPDecryptor.Name = "cmbBPDecryptor"
        Me.cmbBPDecryptor.Size = New System.Drawing.Size(197, 21)
        Me.cmbBPDecryptor.TabIndex = 49
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(119, 151)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(88, 25)
        Me.btnReset.TabIndex = 53
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'lblBPDecryptor
        '
        Me.lblBPDecryptor.Location = New System.Drawing.Point(7, 86)
        Me.lblBPDecryptor.Name = "lblBPDecryptor"
        Me.lblBPDecryptor.Size = New System.Drawing.Size(93, 14)
        Me.lblBPDecryptor.TabIndex = 47
        Me.lblBPDecryptor.Text = "Decryptor:"
        '
        'lblSuccessChance
        '
        Me.lblSuccessChance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuccessChance.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuccessChance.Location = New System.Drawing.Point(19, 163)
        Me.lblSuccessChance.Name = "lblSuccessChance"
        Me.lblSuccessChance.Size = New System.Drawing.Size(93, 27)
        Me.lblSuccessChance.TabIndex = 44
        Me.lblSuccessChance.Text = "100%"
        Me.lblSuccessChance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSkill1
        '
        Me.lblSkill1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSkill1.Location = New System.Drawing.Point(10, 15)
        Me.lblSkill1.Name = "lblSkill1"
        Me.lblSkill1.Size = New System.Drawing.Size(158, 17)
        Me.lblSkill1.TabIndex = 40
        '
        'cmbSkill1
        '
        Me.cmbSkill1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSkill1.FormattingEnabled = True
        Me.cmbSkill1.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbSkill1.Location = New System.Drawing.Point(171, 13)
        Me.cmbSkill1.Name = "cmbSkill1"
        Me.cmbSkill1.Size = New System.Drawing.Size(36, 21)
        Me.cmbSkill1.TabIndex = 41
        '
        'lblSkill2
        '
        Me.lblSkill2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSkill2.Location = New System.Drawing.Point(10, 38)
        Me.lblSkill2.Name = "lblSkill2"
        Me.lblSkill2.Size = New System.Drawing.Size(158, 17)
        Me.lblSkill2.TabIndex = 42
        '
        'cmbSkill2
        '
        Me.cmbSkill2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSkill2.FormattingEnabled = True
        Me.cmbSkill2.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbSkill2.Location = New System.Drawing.Point(171, 36)
        Me.cmbSkill2.Name = "cmbSkill2"
        Me.cmbSkill2.Size = New System.Drawing.Size(36, 21)
        Me.cmbSkill2.TabIndex = 43
        '
        'lblSuccessChance1
        '
        Me.lblSuccessChance1.Location = New System.Drawing.Point(19, 146)
        Me.lblSuccessChance1.Name = "lblSuccessChance1"
        Me.lblSuccessChance1.Size = New System.Drawing.Size(93, 17)
        Me.lblSuccessChance1.TabIndex = 45
        Me.lblSuccessChance1.Text = "Success Chance:"
        Me.lblSuccessChance1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSelectedBP
        '
        Me.lblSelectedBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSelectedBP.Location = New System.Drawing.Point(9, 29)
        Me.lblSelectedBP.Name = "lblSelectedBP"
        Me.lblSelectedBP.Size = New System.Drawing.Size(354, 17)
        Me.lblSelectedBP.TabIndex = 60
        Me.lblSelectedBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(521, 192)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 25)
        Me.btnClose.TabIndex = 54
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(190, 23)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(55, 13)
        Me.lblEndDate.TabIndex = 12
        Me.lblEndDate.Text = "End Date:"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(21, 23)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(58, 13)
        Me.lblStartDate.TabIndex = 11
        Me.lblStartDate.Text = "Start Date:"
        '
        'gbMain
        '
        Me.gbMain.Controls.Add(Me.gbInventionStats)
        Me.gbMain.Controls.Add(Me.gbBPImage)
        Me.gbMain.Controls.Add(Me.gbIndustryJobType)
        Me.gbMain.Controls.Add(Me.lblSelectedBlueprint)
        Me.gbMain.Controls.Add(Me.lblSelectedBP)
        Me.gbMain.Location = New System.Drawing.Point(17, 72)
        Me.gbMain.Name = "gbMain"
        Me.gbMain.Size = New System.Drawing.Size(373, 151)
        Me.gbMain.TabIndex = 66
        Me.gbMain.TabStop = False
        '
        'gbDateSelect
        '
        Me.gbDateSelect.Controls.Add(Me.lblStartDate)
        Me.gbDateSelect.Controls.Add(Me.dtpInventionStartDate)
        Me.gbDateSelect.Controls.Add(Me.lblEndDate)
        Me.gbDateSelect.Controls.Add(Me.dtpInventionEndDate)
        Me.gbDateSelect.Location = New System.Drawing.Point(17, 15)
        Me.gbDateSelect.Name = "gbDateSelect"
        Me.gbDateSelect.Size = New System.Drawing.Size(373, 51)
        Me.gbDateSelect.TabIndex = 67
        Me.gbDateSelect.TabStop = False
        Me.gbDateSelect.Text = "Select Date Range:"
        '
        'gbInventionMonitor
        '
        Me.gbInventionMonitor.Controls.Add(Me.gbDateSelect)
        Me.gbInventionMonitor.Controls.Add(Me.gbSkills)
        Me.gbInventionMonitor.Controls.Add(Me.lstInventionItems)
        Me.gbInventionMonitor.Controls.Add(Me.gbMain)
        Me.gbInventionMonitor.Location = New System.Drawing.Point(6, 1)
        Me.gbInventionMonitor.Name = "gbInventionMonitor"
        Me.gbInventionMonitor.Size = New System.Drawing.Size(630, 450)
        Me.gbInventionMonitor.TabIndex = 68
        Me.gbInventionMonitor.TabStop = False
        '
        'frmInventionMonitor
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(643, 457)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gbInventionMonitor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInventionMonitor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invention Results Monitor"
        Me.gbBPImage.ResumeLayout(False)
        CType(Me.pictInvention, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbInventionStats.ResumeLayout(False)
        Me.gbIndustryJobType.ResumeLayout(False)
        Me.gbIndustryJobType.PerformLayout()
        Me.gbSkills.ResumeLayout(False)
        Me.gbMain.ResumeLayout(False)
        Me.gbMain.PerformLayout()
        Me.gbDateSelect.ResumeLayout(False)
        Me.gbDateSelect.PerformLayout()
        Me.gbInventionMonitor.ResumeLayout(False)
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents lstInventionItems As System.Windows.Forms.ListView
    Friend WithEvents dtpInventionStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInventionEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents cmbSkill2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblSkill2 As System.Windows.Forms.Label
    Friend WithEvents cmbSkill1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblSkill1 As System.Windows.Forms.Label
    Friend WithEvents lblSelectedBlueprint As System.Windows.Forms.Label
    Friend WithEvents gbSkills As System.Windows.Forms.GroupBox
    Friend WithEvents lblSuccessChance As System.Windows.Forms.Label
    Friend WithEvents lblSuccessChance1 As System.Windows.Forms.Label
    Friend WithEvents lblSelectedBP As System.Windows.Forms.Label
    Friend WithEvents lblTotalSuccesses1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalSuccesses As System.Windows.Forms.Label
    Friend WithEvents lblTotalAttempts1 As System.Windows.Forms.Label
    Friend WithEvents lblSuccessRate As System.Windows.Forms.Label
    Friend WithEvents lblTotalAttempts As System.Windows.Forms.Label
    Friend WithEvents lblSuccessRate1 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateJobs As System.Windows.Forms.Button
    Friend WithEvents gbInventionStats As System.Windows.Forms.GroupBox
    Friend WithEvents gbIndustryJobType As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnBothJobs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCorpJobs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPersonalJobs As System.Windows.Forms.RadioButton
    Friend WithEvents cmbBPDecryptor As System.Windows.Forms.ComboBox
    Friend WithEvents lblBPDecryptor As System.Windows.Forms.Label
    Friend WithEvents gbBPImage As System.Windows.Forms.GroupBox
    Friend WithEvents pictInvention As System.Windows.Forms.PictureBox
    Friend WithEvents lblSkill3 As System.Windows.Forms.Label
    Friend WithEvents cmbSkill3 As System.Windows.Forms.ComboBox
    Friend WithEvents gbMain As System.Windows.Forms.GroupBox
    Friend WithEvents gbDateSelect As System.Windows.Forms.GroupBox
    Friend WithEvents gbInventionMonitor As System.Windows.Forms.GroupBox
End Class
