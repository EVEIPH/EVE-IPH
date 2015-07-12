<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCharacterStandings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCharacterStandings))
        Me.lblSkillName = New System.Windows.Forms.Label()
        Me.lblCharacterName = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lstStandings = New System.Windows.Forms.ListView()
        Me.rbtnPostive = New System.Windows.Forms.RadioButton()
        Me.rbtnNegative = New System.Windows.Forms.RadioButton()
        Me.rbtnBoth = New System.Windows.Forms.RadioButton()
        Me.gbStandingType = New System.Windows.Forms.GroupBox()
        Me.gbSort = New System.Windows.Forms.GroupBox()
        Me.rbtnSortName = New System.Windows.Forms.RadioButton()
        Me.rbtnSortStanding = New System.Windows.Forms.RadioButton()
        Me.gbStandingType.SuspendLayout()
        Me.gbSort.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSkillName
        '
        Me.lblSkillName.AutoSize = True
        Me.lblSkillName.Location = New System.Drawing.Point(12, 21)
        Me.lblSkillName.Name = "lblSkillName"
        Me.lblSkillName.Size = New System.Drawing.Size(121, 13)
        Me.lblSkillName.TabIndex = 0
        Me.lblSkillName.Text = "Character Standings for:"
        '
        'lblCharacterName
        '
        Me.lblCharacterName.Location = New System.Drawing.Point(134, 21)
        Me.lblCharacterName.Name = "lblCharacterName"
        Me.lblCharacterName.Size = New System.Drawing.Size(216, 13)
        Me.lblCharacterName.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(142, 400)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(94, 25)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lstStandings
        '
        Me.lstStandings.FullRowSelect = True
        Me.lstStandings.GridLines = True
        Me.lstStandings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstStandings.HideSelection = False
        Me.lstStandings.Location = New System.Drawing.Point(15, 37)
        Me.lstStandings.MultiSelect = False
        Me.lstStandings.Name = "lstStandings"
        Me.lstStandings.Size = New System.Drawing.Size(350, 310)
        Me.lstStandings.TabIndex = 4
        Me.lstStandings.UseCompatibleStateImageBehavior = False
        Me.lstStandings.View = System.Windows.Forms.View.Details
        '
        'rbtnPostive
        '
        Me.rbtnPostive.AutoSize = True
        Me.rbtnPostive.Location = New System.Drawing.Point(68, 16)
        Me.rbtnPostive.Name = "rbtnPostive"
        Me.rbtnPostive.Size = New System.Drawing.Size(62, 17)
        Me.rbtnPostive.TabIndex = 5
        Me.rbtnPostive.Text = "Positive"
        Me.rbtnPostive.UseVisualStyleBackColor = True
        '
        'rbtnNegative
        '
        Me.rbtnNegative.AutoSize = True
        Me.rbtnNegative.Location = New System.Drawing.Point(136, 16)
        Me.rbtnNegative.Name = "rbtnNegative"
        Me.rbtnNegative.Size = New System.Drawing.Size(68, 17)
        Me.rbtnNegative.TabIndex = 6
        Me.rbtnNegative.Text = "Negative"
        Me.rbtnNegative.UseVisualStyleBackColor = True
        '
        'rbtnBoth
        '
        Me.rbtnBoth.AutoSize = True
        Me.rbtnBoth.Checked = True
        Me.rbtnBoth.Location = New System.Drawing.Point(15, 16)
        Me.rbtnBoth.Name = "rbtnBoth"
        Me.rbtnBoth.Size = New System.Drawing.Size(47, 17)
        Me.rbtnBoth.TabIndex = 7
        Me.rbtnBoth.TabStop = True
        Me.rbtnBoth.Text = "Both"
        Me.rbtnBoth.UseVisualStyleBackColor = True
        '
        'gbStandingType
        '
        Me.gbStandingType.Controls.Add(Me.rbtnBoth)
        Me.gbStandingType.Controls.Add(Me.rbtnPostive)
        Me.gbStandingType.Controls.Add(Me.rbtnNegative)
        Me.gbStandingType.Location = New System.Drawing.Point(15, 353)
        Me.gbStandingType.Name = "gbStandingType"
        Me.gbStandingType.Size = New System.Drawing.Size(210, 41)
        Me.gbStandingType.TabIndex = 8
        Me.gbStandingType.TabStop = False
        Me.gbStandingType.Text = "Standing:"
        '
        'gbSort
        '
        Me.gbSort.Controls.Add(Me.rbtnSortName)
        Me.gbSort.Controls.Add(Me.rbtnSortStanding)
        Me.gbSort.Location = New System.Drawing.Point(231, 353)
        Me.gbSort.Name = "gbSort"
        Me.gbSort.Size = New System.Drawing.Size(134, 41)
        Me.gbSort.TabIndex = 9
        Me.gbSort.TabStop = False
        Me.gbSort.Text = "Sort by:"
        '
        'rbtnSortName
        '
        Me.rbtnSortName.AutoSize = True
        Me.rbtnSortName.Location = New System.Drawing.Point(75, 16)
        Me.rbtnSortName.Name = "rbtnSortName"
        Me.rbtnSortName.Size = New System.Drawing.Size(53, 17)
        Me.rbtnSortName.TabIndex = 5
        Me.rbtnSortName.Text = "Name"
        Me.rbtnSortName.UseVisualStyleBackColor = True
        '
        'rbtnSortStanding
        '
        Me.rbtnSortStanding.AutoSize = True
        Me.rbtnSortStanding.Checked = True
        Me.rbtnSortStanding.Location = New System.Drawing.Point(6, 16)
        Me.rbtnSortStanding.Name = "rbtnSortStanding"
        Me.rbtnSortStanding.Size = New System.Drawing.Size(67, 17)
        Me.rbtnSortStanding.TabIndex = 6
        Me.rbtnSortStanding.TabStop = True
        Me.rbtnSortStanding.Text = "Standing"
        Me.rbtnSortStanding.UseVisualStyleBackColor = True
        '
        'frmCharacterStandings
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(378, 435)
        Me.Controls.Add(Me.gbSort)
        Me.Controls.Add(Me.gbStandingType)
        Me.Controls.Add(Me.lstStandings)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblCharacterName)
        Me.Controls.Add(Me.lblSkillName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCharacterStandings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE Isk per Hour - Loaded Skills"
        Me.gbStandingType.ResumeLayout(False)
        Me.gbStandingType.PerformLayout()
        Me.gbSort.ResumeLayout(False)
        Me.gbSort.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSkillName As System.Windows.Forms.Label
    Friend WithEvents lblCharacterName As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lstStandings As System.Windows.Forms.ListView
    Friend WithEvents rbtnPostive As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNegative As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBoth As System.Windows.Forms.RadioButton
    Friend WithEvents gbStandingType As System.Windows.Forms.GroupBox
    Friend WithEvents gbSort As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnSortName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSortStanding As System.Windows.Forms.RadioButton
End Class
