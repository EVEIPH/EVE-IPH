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
        Me.colAccessToken = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRefreshToken = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAccessTokenExpireDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTokenType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnDeleteCharacter = New System.Windows.Forms.Button()
        Me.btnAddCharacter = New System.Windows.Forms.Button()
        Me.btnSelectDefaultChar = New System.Windows.Forms.Button()
        Me.lstScopes = New System.Windows.Forms.ListView()
        Me.colScopes = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbAccountData = New System.Windows.Forms.GroupBox()
        Me.btnCopyCorpID = New System.Windows.Forms.Button()
        Me.lblCorpID = New System.Windows.Forms.Label()
        Me.txtCorpID = New System.Windows.Forms.TextBox()
        Me.lblCharacterID = New System.Windows.Forms.Label()
        Me.txtCharacterID = New System.Windows.Forms.TextBox()
        Me.btnCopyCharacterID = New System.Windows.Forms.Button()
        Me.btnCopyAccesToken = New System.Windows.Forms.Button()
        Me.btnCopyAll = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblRefreshToken = New System.Windows.Forms.Label()
        Me.lblAccessToken = New System.Windows.Forms.Label()
        Me.txtRefreshToken = New System.Windows.Forms.TextBox()
        Me.txtAccessTokenExpDate = New System.Windows.Forms.TextBox()
        Me.txtAccessToken = New System.Windows.Forms.TextBox()
        Me.btnRefreshToken = New System.Windows.Forms.Button()
        Me.chkDirector = New System.Windows.Forms.CheckBox()
        Me.chkFactoryManager = New System.Windows.Forms.CheckBox()
        Me.colCorpID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbAccountData.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstAccounts
        '
        Me.lstAccounts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colCharacterID, Me.colCharacterName, Me.colCorporationName, Me.colIsDefault, Me.colAccountScopes, Me.colAccessToken, Me.colRefreshToken, Me.colAccessTokenExpireDate, Me.colTokenType, Me.colCorpID})
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
        'colAccessToken
        '
        Me.colAccessToken.Width = 0
        '
        'colRefreshToken
        '
        Me.colRefreshToken.Width = 0
        '
        'colAccessTokenExpireDate
        '
        Me.colAccessTokenExpireDate.Width = 0
        '
        'colTokenType
        '
        Me.colTokenType.Width = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(735, 391)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(122, 30)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnDeleteCharacter
        '
        Me.btnDeleteCharacter.Enabled = False
        Me.btnDeleteCharacter.Location = New System.Drawing.Point(351, 391)
        Me.btnDeleteCharacter.Name = "btnDeleteCharacter"
        Me.btnDeleteCharacter.Size = New System.Drawing.Size(122, 30)
        Me.btnDeleteCharacter.TabIndex = 6
        Me.btnDeleteCharacter.Text = "Delete Character"
        Me.btnDeleteCharacter.UseVisualStyleBackColor = True
        '
        'btnAddCharacter
        '
        Me.btnAddCharacter.Location = New System.Drawing.Point(223, 391)
        Me.btnAddCharacter.Name = "btnAddCharacter"
        Me.btnAddCharacter.Size = New System.Drawing.Size(122, 30)
        Me.btnAddCharacter.TabIndex = 7
        Me.btnAddCharacter.Text = "Add Character"
        Me.btnAddCharacter.UseVisualStyleBackColor = True
        '
        'btnSelectDefaultChar
        '
        Me.btnSelectDefaultChar.Enabled = False
        Me.btnSelectDefaultChar.Location = New System.Drawing.Point(479, 391)
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
        Me.lstScopes.Size = New System.Drawing.Size(458, 215)
        Me.lstScopes.TabIndex = 9
        Me.lstScopes.UseCompatibleStateImageBehavior = False
        Me.lstScopes.View = System.Windows.Forms.View.Details
        '
        'colScopes
        '
        Me.colScopes.Text = "Scopes"
        Me.colScopes.Width = 437
        '
        'gbAccountData
        '
        Me.gbAccountData.Controls.Add(Me.btnCopyCorpID)
        Me.gbAccountData.Controls.Add(Me.lblCorpID)
        Me.gbAccountData.Controls.Add(Me.txtCorpID)
        Me.gbAccountData.Controls.Add(Me.lblCharacterID)
        Me.gbAccountData.Controls.Add(Me.txtCharacterID)
        Me.gbAccountData.Controls.Add(Me.btnCopyCharacterID)
        Me.gbAccountData.Controls.Add(Me.btnCopyAccesToken)
        Me.gbAccountData.Controls.Add(Me.btnCopyAll)
        Me.gbAccountData.Controls.Add(Me.Label1)
        Me.gbAccountData.Controls.Add(Me.lblRefreshToken)
        Me.gbAccountData.Controls.Add(Me.lblAccessToken)
        Me.gbAccountData.Controls.Add(Me.txtRefreshToken)
        Me.gbAccountData.Controls.Add(Me.txtAccessTokenExpDate)
        Me.gbAccountData.Controls.Add(Me.txtAccessToken)
        Me.gbAccountData.Location = New System.Drawing.Point(612, 249)
        Me.gbAccountData.Name = "gbAccountData"
        Me.gbAccountData.Size = New System.Drawing.Size(458, 133)
        Me.gbAccountData.TabIndex = 11
        Me.gbAccountData.TabStop = False
        '
        'btnCopyCorpID
        '
        Me.btnCopyCorpID.Location = New System.Drawing.Point(336, 56)
        Me.btnCopyCorpID.Name = "btnCopyCorpID"
        Me.btnCopyCorpID.Size = New System.Drawing.Size(116, 21)
        Me.btnCopyCorpID.TabIndex = 20
        Me.btnCopyCorpID.Text = "Copy Corporation ID"
        Me.btnCopyCorpID.UseVisualStyleBackColor = True
        '
        'lblCorpID
        '
        Me.lblCorpID.AutoSize = True
        Me.lblCorpID.Location = New System.Drawing.Point(247, 61)
        Me.lblCorpID.Name = "lblCorpID"
        Me.lblCorpID.Size = New System.Drawing.Size(78, 13)
        Me.lblCorpID.TabIndex = 19
        Me.lblCorpID.Text = "Corporation ID:"
        '
        'txtCorpID
        '
        Me.txtCorpID.Location = New System.Drawing.Point(250, 77)
        Me.txtCorpID.Name = "txtCorpID"
        Me.txtCorpID.ReadOnly = True
        Me.txtCorpID.Size = New System.Drawing.Size(80, 20)
        Me.txtCorpID.TabIndex = 18
        '
        'lblCharacterID
        '
        Me.lblCharacterID.AutoSize = True
        Me.lblCharacterID.Location = New System.Drawing.Point(160, 61)
        Me.lblCharacterID.Name = "lblCharacterID"
        Me.lblCharacterID.Size = New System.Drawing.Size(70, 13)
        Me.lblCharacterID.TabIndex = 17
        Me.lblCharacterID.Text = "Character ID:"
        '
        'txtCharacterID
        '
        Me.txtCharacterID.Location = New System.Drawing.Point(163, 77)
        Me.txtCharacterID.Name = "txtCharacterID"
        Me.txtCharacterID.ReadOnly = True
        Me.txtCharacterID.Size = New System.Drawing.Size(80, 20)
        Me.txtCharacterID.TabIndex = 16
        '
        'btnCopyCharacterID
        '
        Me.btnCopyCharacterID.Location = New System.Drawing.Point(336, 79)
        Me.btnCopyCharacterID.Name = "btnCopyCharacterID"
        Me.btnCopyCharacterID.Size = New System.Drawing.Size(116, 21)
        Me.btnCopyCharacterID.TabIndex = 15
        Me.btnCopyCharacterID.Text = "Copy Character ID"
        Me.btnCopyCharacterID.UseVisualStyleBackColor = True
        '
        'btnCopyAccesToken
        '
        Me.btnCopyAccesToken.Location = New System.Drawing.Point(336, 33)
        Me.btnCopyAccesToken.Name = "btnCopyAccesToken"
        Me.btnCopyAccesToken.Size = New System.Drawing.Size(116, 21)
        Me.btnCopyAccesToken.TabIndex = 14
        Me.btnCopyAccesToken.Text = "Copy Acces Token"
        Me.btnCopyAccesToken.UseVisualStyleBackColor = True
        '
        'btnCopyAll
        '
        Me.btnCopyAll.Location = New System.Drawing.Point(336, 102)
        Me.btnCopyAll.Name = "btnCopyAll"
        Me.btnCopyAll.Size = New System.Drawing.Size(116, 21)
        Me.btnCopyAll.TabIndex = 12
        Me.btnCopyAll.Text = "Copy Token Data"
        Me.btnCopyAll.UseVisualStyleBackColor = True
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
        Me.txtRefreshToken.ReadOnly = True
        Me.txtRefreshToken.Size = New System.Drawing.Size(151, 20)
        Me.txtRefreshToken.TabIndex = 2
        '
        'txtAccessTokenExpDate
        '
        Me.txtAccessTokenExpDate.Location = New System.Drawing.Point(163, 103)
        Me.txtAccessTokenExpDate.Name = "txtAccessTokenExpDate"
        Me.txtAccessTokenExpDate.ReadOnly = True
        Me.txtAccessTokenExpDate.Size = New System.Drawing.Size(167, 20)
        Me.txtAccessTokenExpDate.TabIndex = 1
        Me.txtAccessTokenExpDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAccessToken
        '
        Me.txtAccessToken.Location = New System.Drawing.Point(6, 34)
        Me.txtAccessToken.Name = "txtAccessToken"
        Me.txtAccessToken.ReadOnly = True
        Me.txtAccessToken.Size = New System.Drawing.Size(324, 20)
        Me.txtAccessToken.TabIndex = 0
        '
        'btnRefreshToken
        '
        Me.btnRefreshToken.Location = New System.Drawing.Point(607, 391)
        Me.btnRefreshToken.Name = "btnRefreshToken"
        Me.btnRefreshToken.Size = New System.Drawing.Size(122, 30)
        Me.btnRefreshToken.TabIndex = 12
        Me.btnRefreshToken.Text = "Refresh Token Data"
        Me.btnRefreshToken.UseVisualStyleBackColor = True
        '
        'chkDirector
        '
        Me.chkDirector.AutoCheck = False
        Me.chkDirector.AutoSize = True
        Me.chkDirector.Location = New System.Drawing.Point(618, 233)
        Me.chkDirector.Name = "chkDirector"
        Me.chkDirector.Size = New System.Drawing.Size(88, 17)
        Me.chkDirector.TabIndex = 13
        Me.chkDirector.Text = "Director Role"
        Me.chkDirector.UseVisualStyleBackColor = True
        '
        'chkFactoryManager
        '
        Me.chkFactoryManager.AutoCheck = False
        Me.chkFactoryManager.AutoSize = True
        Me.chkFactoryManager.Location = New System.Drawing.Point(725, 233)
        Me.chkFactoryManager.Name = "chkFactoryManager"
        Me.chkFactoryManager.Size = New System.Drawing.Size(131, 17)
        Me.chkFactoryManager.TabIndex = 14
        Me.chkFactoryManager.Text = "Factory Manager Role"
        Me.chkFactoryManager.UseVisualStyleBackColor = True
        '
        'colCorpID
        '
        Me.colCorpID.Width = 0
        '
        'frmManageAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1080, 430)
        Me.Controls.Add(Me.chkFactoryManager)
        Me.Controls.Add(Me.chkDirector)
        Me.Controls.Add(Me.btnRefreshToken)
        Me.Controls.Add(Me.gbAccountData)
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
        Me.PerformLayout()

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
    Friend WithEvents gbAccountData As GroupBox
    Friend WithEvents txtRefreshToken As TextBox
    Friend WithEvents txtAccessTokenExpDate As TextBox
    Friend WithEvents txtAccessToken As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCopyAll As Button
    Friend WithEvents lblRefreshToken As Label
    Friend WithEvents lblAccessToken As Label
    Friend WithEvents colAccessToken As ColumnHeader
    Friend WithEvents colRefreshToken As ColumnHeader
    Friend WithEvents colAccessTokenExpireDate As ColumnHeader
    Friend WithEvents btnRefreshToken As Button
    Friend WithEvents colTokenType As ColumnHeader
    Friend WithEvents lblCharacterID As Label
    Friend WithEvents txtCharacterID As TextBox
    Friend WithEvents btnCopyCharacterID As Button
    Friend WithEvents btnCopyAccesToken As Button
    Friend WithEvents chkDirector As CheckBox
    Friend WithEvents chkFactoryManager As CheckBox
    Friend WithEvents btnCopyCorpID As Button
    Friend WithEvents lblCorpID As Label
    Friend WithEvents txtCorpID As TextBox
    Friend WithEvents colCorpID As ColumnHeader
End Class
