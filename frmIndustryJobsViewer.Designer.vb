<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIndustryJobsViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId:="myTimer")>
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIndustryJobsViewer))
        Me.lstIndustryJobs = New System.Windows.Forms.ListView()
        Me.gbIndustryJobType = New System.Windows.Forms.GroupBox()
        Me.rbtnBothJobs = New System.Windows.Forms.RadioButton()
        Me.rbtnCorpJobs = New System.Windows.Forms.RadioButton()
        Me.rbtnPersonalJobs = New System.Windows.Forms.RadioButton()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnUpdateJobs = New System.Windows.Forms.Button()
        Me.btnSelectColumns = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbInventionJobs = New System.Windows.Forms.GroupBox()
        Me.gbJobTimes = New System.Windows.Forms.GroupBox()
        Me.rbtnJobHistory = New System.Windows.Forms.RadioButton()
        Me.rbtnCurrentJobs = New System.Windows.Forms.RadioButton()
        Me.gbButtons = New System.Windows.Forms.GroupBox()
        Me.chkAutoUpdate = New System.Windows.Forms.CheckBox()
        Me.btnRefreshList = New System.Windows.Forms.Button()
        Me.lstCharacters = New EVE_Isk_per_Hour.MyListView()
        Me.gbIndustryJobType.SuspendLayout()
        Me.gbInventionJobs.SuspendLayout()
        Me.gbJobTimes.SuspendLayout()
        Me.gbButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstIndustryJobs
        '
        Me.lstIndustryJobs.AllowColumnReorder = True
        Me.lstIndustryJobs.FullRowSelect = True
        Me.lstIndustryJobs.HideSelection = False
        Me.lstIndustryJobs.Location = New System.Drawing.Point(12, 11)
        Me.lstIndustryJobs.MultiSelect = False
        Me.lstIndustryJobs.Name = "lstIndustryJobs"
        Me.lstIndustryJobs.Size = New System.Drawing.Size(1068, 489)
        Me.lstIndustryJobs.TabIndex = 9
        Me.lstIndustryJobs.UseCompatibleStateImageBehavior = False
        Me.lstIndustryJobs.View = System.Windows.Forms.View.Details
        '
        'gbIndustryJobType
        '
        Me.gbIndustryJobType.Controls.Add(Me.rbtnBothJobs)
        Me.gbIndustryJobType.Controls.Add(Me.rbtnCorpJobs)
        Me.gbIndustryJobType.Controls.Add(Me.rbtnPersonalJobs)
        Me.gbIndustryJobType.Location = New System.Drawing.Point(12, 506)
        Me.gbIndustryJobType.Name = "gbIndustryJobType"
        Me.gbIndustryJobType.Size = New System.Drawing.Size(108, 76)
        Me.gbIndustryJobType.TabIndex = 64
        Me.gbIndustryJobType.TabStop = False
        Me.gbIndustryJobType.Text = "Industry Job Type:"
        '
        'rbtnBothJobs
        '
        Me.rbtnBothJobs.AutoSize = True
        Me.rbtnBothJobs.Location = New System.Drawing.Point(11, 51)
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
        Me.rbtnCorpJobs.Location = New System.Drawing.Point(11, 34)
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
        Me.rbtnPersonalJobs.Location = New System.Drawing.Point(11, 17)
        Me.rbtnPersonalJobs.Name = "rbtnPersonalJobs"
        Me.rbtnPersonalJobs.Size = New System.Drawing.Size(66, 17)
        Me.rbtnPersonalJobs.TabIndex = 0
        Me.rbtnPersonalJobs.TabStop = True
        Me.rbtnPersonalJobs.Text = "Personal"
        Me.rbtnPersonalJobs.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(112, 56)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 65
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnUpdateJobs
        '
        Me.btnUpdateJobs.Location = New System.Drawing.Point(6, 56)
        Me.btnUpdateJobs.Name = "btnUpdateJobs"
        Me.btnUpdateJobs.Size = New System.Drawing.Size(100, 40)
        Me.btnUpdateJobs.TabIndex = 66
        Me.btnUpdateJobs.Text = "Update Jobs"
        Me.btnUpdateJobs.UseVisualStyleBackColor = True
        '
        'btnSelectColumns
        '
        Me.btnSelectColumns.Location = New System.Drawing.Point(6, 101)
        Me.btnSelectColumns.Name = "btnSelectColumns"
        Me.btnSelectColumns.Size = New System.Drawing.Size(100, 40)
        Me.btnSelectColumns.TabIndex = 67
        Me.btnSelectColumns.Text = "Select Columns"
        Me.btnSelectColumns.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(112, 11)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(100, 40)
        Me.btnSaveSettings.TabIndex = 68
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'ttMain
        '
        Me.ttMain.IsBalloon = True
        '
        'gbInventionJobs
        '
        Me.gbInventionJobs.Controls.Add(Me.gbIndustryJobType)
        Me.gbInventionJobs.Controls.Add(Me.lstCharacters)
        Me.gbInventionJobs.Controls.Add(Me.gbJobTimes)
        Me.gbInventionJobs.Controls.Add(Me.gbButtons)
        Me.gbInventionJobs.Controls.Add(Me.lstIndustryJobs)
        Me.gbInventionJobs.Location = New System.Drawing.Point(7, 1)
        Me.gbInventionJobs.Name = "gbInventionJobs"
        Me.gbInventionJobs.Size = New System.Drawing.Size(1080, 659)
        Me.gbInventionJobs.TabIndex = 69
        Me.gbInventionJobs.TabStop = False
        '
        'gbJobTimes
        '
        Me.gbJobTimes.Controls.Add(Me.rbtnJobHistory)
        Me.gbJobTimes.Controls.Add(Me.rbtnCurrentJobs)
        Me.gbJobTimes.Location = New System.Drawing.Point(12, 588)
        Me.gbJobTimes.Name = "gbJobTimes"
        Me.gbJobTimes.Size = New System.Drawing.Size(108, 64)
        Me.gbJobTimes.TabIndex = 65
        Me.gbJobTimes.TabStop = False
        Me.gbJobTimes.Text = "Job Display Type:"
        '
        'rbtnJobHistory
        '
        Me.rbtnJobHistory.AutoSize = True
        Me.rbtnJobHistory.Location = New System.Drawing.Point(12, 39)
        Me.rbtnJobHistory.Name = "rbtnJobHistory"
        Me.rbtnJobHistory.Size = New System.Drawing.Size(77, 17)
        Me.rbtnJobHistory.TabIndex = 1
        Me.rbtnJobHistory.TabStop = True
        Me.rbtnJobHistory.Text = "Job History"
        Me.rbtnJobHistory.UseVisualStyleBackColor = True
        '
        'rbtnCurrentJobs
        '
        Me.rbtnCurrentJobs.AutoSize = True
        Me.rbtnCurrentJobs.Location = New System.Drawing.Point(12, 21)
        Me.rbtnCurrentJobs.Name = "rbtnCurrentJobs"
        Me.rbtnCurrentJobs.Size = New System.Drawing.Size(84, 17)
        Me.rbtnCurrentJobs.TabIndex = 0
        Me.rbtnCurrentJobs.TabStop = True
        Me.rbtnCurrentJobs.Text = "Current Jobs"
        Me.rbtnCurrentJobs.UseVisualStyleBackColor = True
        '
        'gbButtons
        '
        Me.gbButtons.Controls.Add(Me.chkAutoUpdate)
        Me.gbButtons.Controls.Add(Me.btnRefreshList)
        Me.gbButtons.Controls.Add(Me.btnClose)
        Me.gbButtons.Controls.Add(Me.btnUpdateJobs)
        Me.gbButtons.Controls.Add(Me.btnSelectColumns)
        Me.gbButtons.Controls.Add(Me.btnSaveSettings)
        Me.gbButtons.Location = New System.Drawing.Point(126, 506)
        Me.gbButtons.Name = "gbButtons"
        Me.gbButtons.Size = New System.Drawing.Size(217, 146)
        Me.gbButtons.TabIndex = 69
        Me.gbButtons.TabStop = False
        '
        'chkAutoUpdate
        '
        Me.chkAutoUpdate.Location = New System.Drawing.Point(117, 100)
        Me.chkAutoUpdate.Name = "chkAutoUpdate"
        Me.chkAutoUpdate.Size = New System.Drawing.Size(94, 43)
        Me.chkAutoUpdate.TabIndex = 0
        Me.chkAutoUpdate.Text = "Automatically Update Jobs on Startup"
        Me.chkAutoUpdate.UseVisualStyleBackColor = True
        '
        'btnRefreshList
        '
        Me.btnRefreshList.Location = New System.Drawing.Point(6, 11)
        Me.btnRefreshList.Name = "btnRefreshList"
        Me.btnRefreshList.Size = New System.Drawing.Size(100, 40)
        Me.btnRefreshList.TabIndex = 70
        Me.btnRefreshList.Text = "Refresh"
        Me.btnRefreshList.UseVisualStyleBackColor = True
        '
        'lstCharacters
        '
        Me.lstCharacters.CheckBoxes = True
        Me.lstCharacters.FullRowSelect = True
        Me.lstCharacters.GridLines = True
        Me.lstCharacters.Location = New System.Drawing.Point(349, 512)
        Me.lstCharacters.MultiSelect = False
        Me.lstCharacters.Name = "lstCharacters"
        Me.lstCharacters.Size = New System.Drawing.Size(725, 140)
        Me.lstCharacters.TabIndex = 70
        Me.lstCharacters.TabStop = False
        Me.lstCharacters.Tag = "20"
        Me.lstCharacters.UseCompatibleStateImageBehavior = False
        Me.lstCharacters.View = System.Windows.Forms.View.Details
        '
        'frmIndustryJobsViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1094, 665)
        Me.Controls.Add(Me.gbInventionJobs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIndustryJobsViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Current Industry Jobs"
        Me.gbIndustryJobType.ResumeLayout(False)
        Me.gbIndustryJobType.PerformLayout()
        Me.gbInventionJobs.ResumeLayout(False)
        Me.gbJobTimes.ResumeLayout(False)
        Me.gbJobTimes.PerformLayout()
        Me.gbButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstIndustryJobs As System.Windows.Forms.ListView
    Friend WithEvents gbIndustryJobType As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnBothJobs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCorpJobs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPersonalJobs As System.Windows.Forms.RadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnUpdateJobs As System.Windows.Forms.Button
    Friend WithEvents btnSelectColumns As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents ttMain As System.Windows.Forms.ToolTip
    Friend WithEvents gbInventionJobs As System.Windows.Forms.GroupBox
    Friend WithEvents gbButtons As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefreshList As System.Windows.Forms.Button
    Friend WithEvents gbJobTimes As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnJobHistory As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCurrentJobs As System.Windows.Forms.RadioButton
    Friend WithEvents lstCharacters As EVE_Isk_per_Hour.MyListView
    Friend WithEvents chkAutoUpdate As CheckBox
End Class
