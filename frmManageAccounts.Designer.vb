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
        Me.colCharacterID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCharacterName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCorporationName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colIsDefault = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAccountScopes = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnDeleteCharacter = New System.Windows.Forms.Button()
        Me.btnAddCharacter = New System.Windows.Forms.Button()
        Me.btnSelectDefaultChar = New System.Windows.Forms.Button()
        Me.lstScopes = New System.Windows.Forms.ListView()
        Me.colScopes = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnRegisterProgram = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstAccounts
        '
        Me.lstAccounts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colCharacterID, Me.colCharacterName, Me.colCorporationName, Me.colIsDefault, Me.colAccountScopes})
        Me.lstAccounts.FullRowSelect = True
        Me.lstAccounts.GridLines = True
        Me.lstAccounts.HideSelection = False
        Me.lstAccounts.Location = New System.Drawing.Point(12, 12)
        Me.lstAccounts.MultiSelect = False
        Me.lstAccounts.Name = "lstAccounts"
        Me.lstAccounts.Size = New System.Drawing.Size(594, 231)
        Me.lstAccounts.TabIndex = 3
        Me.lstAccounts.UseCompatibleStateImageBehavior = False
        Me.lstAccounts.View = System.Windows.Forms.View.Details
        '
        'colCharacterID
        '
        Me.colCharacterID.Text = "Character ID"
        Me.colCharacterID.Width = 0
        '
        'colCharacterName
        '
        Me.colCharacterName.Text = "Character Name"
        Me.colCharacterName.Width = 238
        '
        'colCorporationName
        '
        Me.colCorporationName.Text = "Corporation Name"
        Me.colCorporationName.Width = 275
        '
        'colIsDefault
        '
        Me.colIsDefault.Text = "Is Default"
        '
        'colAccountScopes
        '
        Me.colAccountScopes.Text = "Scopes"
        Me.colAccountScopes.Width = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(682, 257)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(122, 30)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnDeleteCharacter
        '
        Me.btnDeleteCharacter.Enabled = False
        Me.btnDeleteCharacter.Location = New System.Drawing.Point(298, 257)
        Me.btnDeleteCharacter.Name = "btnDeleteCharacter"
        Me.btnDeleteCharacter.Size = New System.Drawing.Size(122, 30)
        Me.btnDeleteCharacter.TabIndex = 6
        Me.btnDeleteCharacter.Text = "Delete Character"
        Me.btnDeleteCharacter.UseVisualStyleBackColor = True
        '
        'btnAddCharacter
        '
        Me.btnAddCharacter.Location = New System.Drawing.Point(170, 257)
        Me.btnAddCharacter.Name = "btnAddCharacter"
        Me.btnAddCharacter.Size = New System.Drawing.Size(122, 30)
        Me.btnAddCharacter.TabIndex = 7
        Me.btnAddCharacter.Text = "Add Character"
        Me.btnAddCharacter.UseVisualStyleBackColor = True
        '
        'btnSelectDefaultChar
        '
        Me.btnSelectDefaultChar.Enabled = False
        Me.btnSelectDefaultChar.Location = New System.Drawing.Point(426, 257)
        Me.btnSelectDefaultChar.Name = "btnSelectDefaultChar"
        Me.btnSelectDefaultChar.Size = New System.Drawing.Size(122, 30)
        Me.btnSelectDefaultChar.TabIndex = 8
        Me.btnSelectDefaultChar.Text = "Set Default Character"
        Me.btnSelectDefaultChar.UseVisualStyleBackColor = True
        '
        'lstScopes
        '
        Me.lstScopes.CausesValidation = False
        Me.lstScopes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colScopes})
        Me.lstScopes.Enabled = False
        Me.lstScopes.HideSelection = False
        Me.lstScopes.Location = New System.Drawing.Point(612, 12)
        Me.lstScopes.MultiSelect = False
        Me.lstScopes.Name = "lstScopes"
        Me.lstScopes.Size = New System.Drawing.Size(350, 231)
        Me.lstScopes.TabIndex = 9
        Me.lstScopes.UseCompatibleStateImageBehavior = False
        Me.lstScopes.View = System.Windows.Forms.View.Details
        '
        'colScopes
        '
        Me.colScopes.Text = "Scopes"
        Me.colScopes.Width = 345
        '
        'btnRegisterProgram
        '
        Me.btnRegisterProgram.Location = New System.Drawing.Point(554, 257)
        Me.btnRegisterProgram.Name = "btnRegisterProgram"
        Me.btnRegisterProgram.Size = New System.Drawing.Size(122, 30)
        Me.btnRegisterProgram.TabIndex = 10
        Me.btnRegisterProgram.Text = "Register Program"
        Me.btnRegisterProgram.UseVisualStyleBackColor = True
        '
        'frmManageAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(974, 302)
        Me.Controls.Add(Me.btnRegisterProgram)
        Me.Controls.Add(Me.lstScopes)
        Me.Controls.Add(Me.btnSelectDefaultChar)
        Me.Controls.Add(Me.btnAddCharacter)
        Me.Controls.Add(Me.btnDeleteCharacter)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lstAccounts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmManageAccounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Accounts"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstAccounts As System.Windows.Forms.ListView
    Friend WithEvents colCharacterName As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnDeleteCharacter As System.Windows.Forms.Button
    Friend WithEvents btnAddCharacter As System.Windows.Forms.Button
    Friend WithEvents btnSelectDefaultChar As System.Windows.Forms.Button
    Friend WithEvents colCorporationName As ColumnHeader
    Friend WithEvents lstScopes As ListView
    Friend WithEvents colScopes As ColumnHeader
    Friend WithEvents colIsDefault As ColumnHeader
    Friend WithEvents colAccountScopes As ColumnHeader
    Friend WithEvents colCharacterID As ColumnHeader
    Friend WithEvents btnRegisterProgram As Button
End Class
