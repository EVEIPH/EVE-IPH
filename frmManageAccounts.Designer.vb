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
        Me.gbAccountData = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCopyAll = New System.Windows.Forms.Button()
        Me.lblRefreshToken = New System.Windows.Forms.Label()
        Me.lblAccessToken = New System.Windows.Forms.Label()
        Me.txtRefreshToken = New System.Windows.Forms.TextBox()
        Me.txtAccessTokenExpDate = New System.Windows.Forms.TextBox()
        Me.txtAccessToken = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.gbAccountData.SuspendLayout()
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
        Me.lstAccounts.Size = New System.Drawing.Size(594, 370)
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
        Me.btnClose.Location = New System.Drawing.Point(762, 391)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(122, 30)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnDeleteCharacter
        '
        Me.btnDeleteCharacter.Enabled = False
        Me.btnDeleteCharacter.Location = New System.Drawing.Point(378, 391)
        Me.btnDeleteCharacter.Name = "btnDeleteCharacter"
        Me.btnDeleteCharacter.Size = New System.Drawing.Size(122, 30)
        Me.btnDeleteCharacter.TabIndex = 6
        Me.btnDeleteCharacter.Text = "Delete Character"
        Me.btnDeleteCharacter.UseVisualStyleBackColor = True
        '
        'btnAddCharacter
        '
        Me.btnAddCharacter.Location = New System.Drawing.Point(250, 391)
        Me.btnAddCharacter.Name = "btnAddCharacter"
        Me.btnAddCharacter.Size = New System.Drawing.Size(122, 30)
        Me.btnAddCharacter.TabIndex = 7
        Me.btnAddCharacter.Text = "Add Character"
        Me.btnAddCharacter.UseVisualStyleBackColor = True
        '
        'btnSelectDefaultChar
        '
        Me.btnSelectDefaultChar.Enabled = False
        Me.btnSelectDefaultChar.Location = New System.Drawing.Point(506, 391)
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
        Me.lstScopes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstScopes.HideSelection = False
        Me.lstScopes.Location = New System.Drawing.Point(612, 12)
        Me.lstScopes.MultiSelect = False
        Me.lstScopes.Name = "lstScopes"
        Me.lstScopes.Size = New System.Drawing.Size(516, 231)
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
        Me.btnRegisterProgram.Location = New System.Drawing.Point(634, 391)
        Me.btnRegisterProgram.Name = "btnRegisterProgram"
        Me.btnRegisterProgram.Size = New System.Drawing.Size(122, 30)
        Me.btnRegisterProgram.TabIndex = 10
        Me.btnRegisterProgram.Text = "Register Program"
        Me.btnRegisterProgram.UseVisualStyleBackColor = True
        '
        'gbAccountData
        '
        Me.gbAccountData.Controls.Add(Me.btnExport)
        Me.gbAccountData.Controls.Add(Me.Label1)
        Me.gbAccountData.Controls.Add(Me.btnCopyAll)
        Me.gbAccountData.Controls.Add(Me.lblRefreshToken)
        Me.gbAccountData.Controls.Add(Me.lblAccessToken)
        Me.gbAccountData.Controls.Add(Me.txtRefreshToken)
        Me.gbAccountData.Controls.Add(Me.txtAccessTokenExpDate)
        Me.gbAccountData.Controls.Add(Me.txtAccessToken)
        Me.gbAccountData.Location = New System.Drawing.Point(612, 249)
        Me.gbAccountData.Name = "gbAccountData"
        Me.gbAccountData.Size = New System.Drawing.Size(516, 133)
        Me.gbAccountData.TabIndex = 11
        Me.gbAccountData.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Access Token Expiration Date:"
        '
        'btnCopyAll
        '
        Me.btnCopyAll.Location = New System.Drawing.Point(434, 102)
        Me.btnCopyAll.Name = "btnCopyAll"
        Me.btnCopyAll.Size = New System.Drawing.Size(76, 21)
        Me.btnCopyAll.TabIndex = 12
        Me.btnCopyAll.Text = "Copy All"
        Me.btnCopyAll.UseVisualStyleBackColor = True
        '
        'lblRefreshToken
        '
        Me.lblRefreshToken.AutoSize = True
        Me.lblRefreshToken.Location = New System.Drawing.Point(3, 61)
        Me.lblRefreshToken.Name = "lblRefreshToken"
        Me.lblRefreshToken.Size = New System.Drawing.Size(81, 13)
        Me.lblRefreshToken.TabIndex = 4
        Me.lblRefreshToken.Text = "Refresh Token:"
        '
        'lblAccessToken
        '
        Me.lblAccessToken.AutoSize = True
        Me.lblAccessToken.Location = New System.Drawing.Point(3, 16)
        Me.lblAccessToken.Name = "lblAccessToken"
        Me.lblAccessToken.Size = New System.Drawing.Size(79, 13)
        Me.lblAccessToken.TabIndex = 3
        Me.lblAccessToken.Text = "Access Token:"
        '
        'txtRefreshToken
        '
        Me.txtRefreshToken.Location = New System.Drawing.Point(6, 77)
        Me.txtRefreshToken.Name = "txtRefreshToken"
        Me.txtRefreshToken.Size = New System.Drawing.Size(504, 20)
        Me.txtRefreshToken.TabIndex = 2
        '
        'txtAccessTokenExpDate
        '
        Me.txtAccessTokenExpDate.Location = New System.Drawing.Point(163, 103)
        Me.txtAccessTokenExpDate.Name = "txtAccessTokenExpDate"
        Me.txtAccessTokenExpDate.Size = New System.Drawing.Size(122, 20)
        Me.txtAccessTokenExpDate.TabIndex = 1
        '
        'txtAccessToken
        '
        Me.txtAccessToken.Location = New System.Drawing.Point(6, 34)
        Me.txtAccessToken.Name = "txtAccessToken"
        Me.txtAccessToken.Size = New System.Drawing.Size(504, 20)
        Me.txtAccessToken.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(352, 102)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(76, 21)
        Me.btnExport.TabIndex = 14
        Me.btnExport.Text = "Export All"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'frmManageAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1134, 430)
        Me.Controls.Add(Me.gbAccountData)
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
        Me.gbAccountData.ResumeLayout(False)
        Me.gbAccountData.PerformLayout()
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
    Friend WithEvents gbAccountData As GroupBox
    Friend WithEvents txtRefreshToken As TextBox
    Friend WithEvents txtAccessTokenExpDate As TextBox
    Friend WithEvents txtAccessToken As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCopyAll As Button
    Friend WithEvents lblRefreshToken As Label
    Friend WithEvents lblAccessToken As Label
    Friend WithEvents btnExport As Button
End Class
