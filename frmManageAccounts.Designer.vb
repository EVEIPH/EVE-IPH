<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageAccounts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageAccounts))
        Me.lstAccounts = New System.Windows.Forms.ListView()
        Me.colKeyType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colKeyID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colvCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colChars = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAccessMask = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colExpDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnUpdateKey = New System.Windows.Forms.Button()
        Me.btnDeleteKey = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSelectDefaultChar = New System.Windows.Forms.Button()
        Me.lblDefaultChar = New System.Windows.Forms.Label()
        Me.chkAccessStandings = New System.Windows.Forms.CheckBox()
        Me.chkAccessCharacterSheet = New System.Windows.Forms.CheckBox()
        Me.chkAccessAssets = New System.Windows.Forms.CheckBox()
        Me.chkAccessSIIndustryJobs = New System.Windows.Forms.CheckBox()
        Me.chkAccessSIResearch = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstAccounts
        '
        Me.lstAccounts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colKeyType, Me.colKeyID, Me.colvCode, Me.colChars, Me.colAccessMask, Me.colExpDate})
        Me.lstAccounts.FullRowSelect = True
        Me.lstAccounts.GridLines = True
        Me.lstAccounts.HideSelection = False
        Me.lstAccounts.Location = New System.Drawing.Point(12, 12)
        Me.lstAccounts.MultiSelect = False
        Me.lstAccounts.Name = "lstAccounts"
        Me.lstAccounts.Size = New System.Drawing.Size(950, 148)
        Me.lstAccounts.TabIndex = 3
        Me.lstAccounts.UseCompatibleStateImageBehavior = False
        Me.lstAccounts.View = System.Windows.Forms.View.Details
        '
        'colKeyType
        '
        Me.colKeyType.Text = "Key Type"
        Me.colKeyType.Width = 63
        '
        'colKeyID
        '
        Me.colKeyID.Text = "Key ID"
        Me.colKeyID.Width = 65
        '
        'colvCode
        '
        Me.colvCode.Text = "Verification Code (API Key)"
        Me.colvCode.Width = 430
        '
        'colChars
        '
        Me.colChars.Text = "Characters / Corporation Name"
        Me.colChars.Width = 181
        '
        'colAccessMask
        '
        Me.colAccessMask.Text = "Access Mask"
        Me.colAccessMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colAccessMask.Width = 80
        '
        'colExpDate
        '
        Me.colExpDate.Text = "Key Expiration Date"
        Me.colExpDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colExpDate.Width = 110
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(686, 249)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(122, 30)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnUpdateKey
        '
        Me.btnUpdateKey.Enabled = False
        Me.btnUpdateKey.Location = New System.Drawing.Point(296, 249)
        Me.btnUpdateKey.Name = "btnUpdateKey"
        Me.btnUpdateKey.Size = New System.Drawing.Size(122, 30)
        Me.btnUpdateKey.TabIndex = 5
        Me.btnUpdateKey.Text = "Update Key"
        Me.btnUpdateKey.UseVisualStyleBackColor = True
        '
        'btnDeleteKey
        '
        Me.btnDeleteKey.Enabled = False
        Me.btnDeleteKey.Location = New System.Drawing.Point(426, 249)
        Me.btnDeleteKey.Name = "btnDeleteKey"
        Me.btnDeleteKey.Size = New System.Drawing.Size(122, 30)
        Me.btnDeleteKey.TabIndex = 6
        Me.btnDeleteKey.Text = "Delete Key"
        Me.btnDeleteKey.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(166, 249)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(122, 30)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add Key"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSelectDefaultChar
        '
        Me.btnSelectDefaultChar.Location = New System.Drawing.Point(556, 249)
        Me.btnSelectDefaultChar.Name = "btnSelectDefaultChar"
        Me.btnSelectDefaultChar.Size = New System.Drawing.Size(122, 30)
        Me.btnSelectDefaultChar.TabIndex = 8
        Me.btnSelectDefaultChar.Text = "Set Default Character"
        Me.btnSelectDefaultChar.UseVisualStyleBackColor = True
        '
        'lblDefaultChar
        '
        Me.lblDefaultChar.AutoSize = True
        Me.lblDefaultChar.Location = New System.Drawing.Point(425, 224)
        Me.lblDefaultChar.Name = "lblDefaultChar"
        Me.lblDefaultChar.Size = New System.Drawing.Size(125, 13)
        Me.lblDefaultChar.TabIndex = 9
        Me.lblDefaultChar.Text = "Default Character: Zifrian"
        '
        'chkAccessStandings
        '
        Me.chkAccessStandings.AutoSize = True
        Me.chkAccessStandings.Location = New System.Drawing.Point(19, 19)
        Me.chkAccessStandings.Name = "chkAccessStandings"
        Me.chkAccessStandings.Size = New System.Drawing.Size(163, 17)
        Me.chkAccessStandings.TabIndex = 10
        Me.chkAccessStandings.Text = "Public Information: Standings"
        Me.chkAccessStandings.UseVisualStyleBackColor = True
        '
        'chkAccessCharacterSheet
        '
        Me.chkAccessCharacterSheet.AutoSize = True
        Me.chkAccessCharacterSheet.Location = New System.Drawing.Point(188, 19)
        Me.chkAccessCharacterSheet.Name = "chkAccessCharacterSheet"
        Me.chkAccessCharacterSheet.Size = New System.Drawing.Size(194, 17)
        Me.chkAccessCharacterSheet.TabIndex = 11
        Me.chkAccessCharacterSheet.Text = "Private Information: CharacterSheet"
        Me.chkAccessCharacterSheet.UseVisualStyleBackColor = True
        '
        'chkAccessAssets
        '
        Me.chkAccessAssets.AutoSize = True
        Me.chkAccessAssets.Location = New System.Drawing.Point(388, 19)
        Me.chkAccessAssets.Name = "chkAccessAssets"
        Me.chkAccessAssets.Size = New System.Drawing.Size(162, 17)
        Me.chkAccessAssets.TabIndex = 12
        Me.chkAccessAssets.Text = "Private Information: AssetList"
        Me.chkAccessAssets.UseVisualStyleBackColor = True
        '
        'chkAccessSIIndustryJobs
        '
        Me.chkAccessSIIndustryJobs.AutoSize = True
        Me.chkAccessSIIndustryJobs.Location = New System.Drawing.Point(556, 19)
        Me.chkAccessSIIndustryJobs.Name = "chkAccessSIIndustryJobs"
        Me.chkAccessSIIndustryJobs.Size = New System.Drawing.Size(191, 17)
        Me.chkAccessSIIndustryJobs.TabIndex = 13
        Me.chkAccessSIIndustryJobs.Text = "Science and Industry: IndustryJobs"
        Me.chkAccessSIIndustryJobs.UseVisualStyleBackColor = True
        '
        'chkAccessSIResearch
        '
        Me.chkAccessSIResearch.AutoSize = True
        Me.chkAccessSIResearch.Location = New System.Drawing.Point(753, 19)
        Me.chkAccessSIResearch.Name = "chkAccessSIResearch"
        Me.chkAccessSIResearch.Size = New System.Drawing.Size(178, 17)
        Me.chkAccessSIResearch.TabIndex = 14
        Me.chkAccessSIResearch.Text = "Science and Industry: Research"
        Me.chkAccessSIResearch.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAccessSIResearch)
        Me.GroupBox1.Controls.Add(Me.chkAccessStandings)
        Me.GroupBox1.Controls.Add(Me.chkAccessSIIndustryJobs)
        Me.GroupBox1.Controls.Add(Me.chkAccessCharacterSheet)
        Me.GroupBox1.Controls.Add(Me.chkAccessAssets)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 166)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(950, 47)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Selected API Access"
        '
        'frmManageAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 289)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblDefaultChar)
        Me.Controls.Add(Me.btnSelectDefaultChar)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDeleteKey)
        Me.Controls.Add(Me.btnUpdateKey)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lstAccounts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmManageAccounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Accounts"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstAccounts As System.Windows.Forms.ListView
    Friend WithEvents colKeyType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colKeyID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colvCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents colChars As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAccessMask As System.Windows.Forms.ColumnHeader
    Friend WithEvents colExpDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnUpdateKey As System.Windows.Forms.Button
    Friend WithEvents btnDeleteKey As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnSelectDefaultChar As System.Windows.Forms.Button
    Friend WithEvents lblDefaultChar As System.Windows.Forms.Label
    Friend WithEvents chkAccessStandings As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessCharacterSheet As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessAssets As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessSIIndustryJobs As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessSIResearch As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
