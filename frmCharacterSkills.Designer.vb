<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCharacterSkills
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCharacterSkills))
        Me.lblSkillName = New System.Windows.Forms.Label()
        Me.lblCharacterName = New System.Windows.Forms.Label()
        Me.SkillTree = New System.Windows.Forms.TreeView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.contextOverride = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuOrigLevel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLevel0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLevel1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLevel2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLevel3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLevel4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLevel5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbOverride = New System.Windows.Forms.GroupBox()
        Me.chkAllLevel5 = New System.Windows.Forms.CheckBox()
        Me.chkSkillOverride = New System.Windows.Forms.CheckBox()
        Me.gbCharName = New System.Windows.Forms.GroupBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtSkillNameFilter = New System.Windows.Forms.TextBox()
        Me.lblSkillNameFilter = New System.Windows.Forms.Label()
        Me.btnClearItemFilter = New System.Windows.Forms.Button()
        Me.contextOverride.SuspendLayout()
        Me.gbOverride.SuspendLayout()
        Me.gbCharName.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSkillName
        '
        Me.lblSkillName.AutoSize = True
        Me.lblSkillName.Location = New System.Drawing.Point(11, 13)
        Me.lblSkillName.Name = "lblSkillName"
        Me.lblSkillName.Size = New System.Drawing.Size(56, 13)
        Me.lblSkillName.TabIndex = 0
        Me.lblSkillName.Text = "Character:"
        '
        'lblCharacterName
        '
        Me.lblCharacterName.Location = New System.Drawing.Point(64, 13)
        Me.lblCharacterName.Name = "lblCharacterName"
        Me.lblCharacterName.Size = New System.Drawing.Size(268, 13)
        Me.lblCharacterName.TabIndex = 1
        '
        'SkillTree
        '
        Me.SkillTree.Location = New System.Drawing.Point(12, 80)
        Me.SkillTree.Name = "SkillTree"
        Me.SkillTree.Size = New System.Drawing.Size(338, 359)
        Me.SkillTree.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(234, 474)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(94, 25)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(34, 474)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(94, 25)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'contextOverride
        '
        Me.contextOverride.AutoSize = False
        Me.contextOverride.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOrigLevel, Me.ToolStripSeparator1, Me.mnuLevel0, Me.mnuLevel1, Me.mnuLevel2, Me.mnuLevel3, Me.mnuLevel4, Me.mnuLevel5})
        Me.contextOverride.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.contextOverride.Name = "contextOverride"
        Me.contextOverride.ShowCheckMargin = True
        Me.contextOverride.ShowImageMargin = False
        Me.contextOverride.ShowItemToolTips = False
        Me.contextOverride.Size = New System.Drawing.Size(125, 175)
        '
        'mnuOrigLevel
        '
        Me.mnuOrigLevel.AutoSize = False
        Me.mnuOrigLevel.CheckOnClick = True
        Me.mnuOrigLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuOrigLevel.Name = "mnuOrigLevel"
        Me.mnuOrigLevel.Size = New System.Drawing.Size(125, 22)
        Me.mnuOrigLevel.Text = "No Override"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(121, 6)
        '
        'mnuLevel0
        '
        Me.mnuLevel0.Name = "mnuLevel0"
        Me.mnuLevel0.Size = New System.Drawing.Size(138, 22)
        Me.mnuLevel0.Text = "Level 0"
        '
        'mnuLevel1
        '
        Me.mnuLevel1.AutoSize = False
        Me.mnuLevel1.CheckOnClick = True
        Me.mnuLevel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuLevel1.Name = "mnuLevel1"
        Me.mnuLevel1.Size = New System.Drawing.Size(123, 22)
        Me.mnuLevel1.Text = "Level 1"
        '
        'mnuLevel2
        '
        Me.mnuLevel2.AutoSize = False
        Me.mnuLevel2.CheckOnClick = True
        Me.mnuLevel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuLevel2.Name = "mnuLevel2"
        Me.mnuLevel2.Size = New System.Drawing.Size(123, 22)
        Me.mnuLevel2.Text = "Level 2"
        '
        'mnuLevel3
        '
        Me.mnuLevel3.AutoSize = False
        Me.mnuLevel3.CheckOnClick = True
        Me.mnuLevel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuLevel3.Name = "mnuLevel3"
        Me.mnuLevel3.Size = New System.Drawing.Size(123, 22)
        Me.mnuLevel3.Text = "Level 3"
        '
        'mnuLevel4
        '
        Me.mnuLevel4.AutoSize = False
        Me.mnuLevel4.CheckOnClick = True
        Me.mnuLevel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuLevel4.Name = "mnuLevel4"
        Me.mnuLevel4.Size = New System.Drawing.Size(123, 22)
        Me.mnuLevel4.Text = "Level 4"
        '
        'mnuLevel5
        '
        Me.mnuLevel5.AutoSize = False
        Me.mnuLevel5.CheckOnClick = True
        Me.mnuLevel5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuLevel5.Name = "mnuLevel5"
        Me.mnuLevel5.Size = New System.Drawing.Size(123, 22)
        Me.mnuLevel5.Text = "Level 5"
        '
        'gbOverride
        '
        Me.gbOverride.Controls.Add(Me.chkAllLevel5)
        Me.gbOverride.Controls.Add(Me.chkSkillOverride)
        Me.gbOverride.Location = New System.Drawing.Point(12, 40)
        Me.gbOverride.Name = "gbOverride"
        Me.gbOverride.Size = New System.Drawing.Size(338, 34)
        Me.gbOverride.TabIndex = 6
        Me.gbOverride.TabStop = False
        '
        'chkAllLevel5
        '
        Me.chkAllLevel5.AutoSize = True
        Me.chkAllLevel5.Location = New System.Drawing.Point(184, 11)
        Me.chkAllLevel5.Name = "chkAllLevel5"
        Me.chkAllLevel5.Size = New System.Drawing.Size(132, 17)
        Me.chkAllLevel5.TabIndex = 0
        Me.chkAllLevel5.Text = "Set all Skills to Level 5"
        Me.chkAllLevel5.UseVisualStyleBackColor = True
        '
        'chkSkillOverride
        '
        Me.chkSkillOverride.AutoSize = True
        Me.chkSkillOverride.Location = New System.Drawing.Point(22, 11)
        Me.chkSkillOverride.Name = "chkSkillOverride"
        Me.chkSkillOverride.Size = New System.Drawing.Size(93, 17)
        Me.chkSkillOverride.TabIndex = 0
        Me.chkSkillOverride.Text = "Override Skills"
        Me.chkSkillOverride.UseVisualStyleBackColor = True
        '
        'gbCharName
        '
        Me.gbCharName.Controls.Add(Me.lblSkillName)
        Me.gbCharName.Controls.Add(Me.lblCharacterName)
        Me.gbCharName.Location = New System.Drawing.Point(12, 7)
        Me.gbCharName.Name = "gbCharName"
        Me.gbCharName.Size = New System.Drawing.Size(338, 34)
        Me.gbCharName.TabIndex = 7
        Me.gbCharName.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(134, 474)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(94, 25)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save Settings"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtSkillNameFilter
        '
        Me.txtSkillNameFilter.Location = New System.Drawing.Point(111, 445)
        Me.txtSkillNameFilter.Name = "txtSkillNameFilter"
        Me.txtSkillNameFilter.Size = New System.Drawing.Size(166, 20)
        Me.txtSkillNameFilter.TabIndex = 11
        '
        'lblSkillNameFilter
        '
        Me.lblSkillNameFilter.AutoSize = True
        Me.lblSkillNameFilter.Location = New System.Drawing.Point(20, 448)
        Me.lblSkillNameFilter.Name = "lblSkillNameFilter"
        Me.lblSkillNameFilter.Size = New System.Drawing.Size(85, 13)
        Me.lblSkillNameFilter.TabIndex = 10
        Me.lblSkillNameFilter.Text = "Skill Name Filter:"
        '
        'btnClearItemFilter
        '
        Me.btnClearItemFilter.Location = New System.Drawing.Point(283, 444)
        Me.btnClearItemFilter.Name = "btnClearItemFilter"
        Me.btnClearItemFilter.Size = New System.Drawing.Size(59, 21)
        Me.btnClearItemFilter.TabIndex = 13
        Me.btnClearItemFilter.Text = "Clear"
        Me.btnClearItemFilter.UseVisualStyleBackColor = True
        '
        'frmCharacterSkills
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(362, 511)
        Me.Controls.Add(Me.btnClearItemFilter)
        Me.Controls.Add(Me.txtSkillNameFilter)
        Me.Controls.Add(Me.lblSkillNameFilter)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.gbCharName)
        Me.Controls.Add(Me.SkillTree)
        Me.Controls.Add(Me.gbOverride)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCharacterSkills"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE Isk per Hour - Loaded Skills"
        Me.contextOverride.ResumeLayout(False)
        Me.gbOverride.ResumeLayout(False)
        Me.gbOverride.PerformLayout()
        Me.gbCharName.ResumeLayout(False)
        Me.gbCharName.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSkillName As System.Windows.Forms.Label
    Friend WithEvents lblCharacterName As System.Windows.Forms.Label
    Friend WithEvents SkillTree As System.Windows.Forms.TreeView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents contextOverride As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuLevel1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLevel2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLevel3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLevel4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLevel5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbOverride As System.Windows.Forms.GroupBox
    Friend WithEvents chkSkillOverride As System.Windows.Forms.CheckBox
    Friend WithEvents gbCharName As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents mnuOrigLevel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkAllLevel5 As System.Windows.Forms.CheckBox
    Friend WithEvents mnuLevel0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtSkillNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblSkillNameFilter As System.Windows.Forms.Label
    Friend WithEvents btnClearItemFilter As System.Windows.Forms.Button
End Class
