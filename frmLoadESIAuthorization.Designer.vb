<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadESIAuthorization
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadESIAuthorization))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRegisterApplication = New System.Windows.Forms.Button()
        Me.txtClientID = New System.Windows.Forms.TextBox()
        Me.lblClientID = New System.Windows.Forms.Label()
        Me.lblSecretKey = New System.Windows.Forms.Label()
        Me.txtSecretKey = New System.Windows.Forms.TextBox()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtScopes = New System.Windows.Forms.TextBox()
        Me.lblScopes = New System.Windows.Forms.Label()
        Me.btnSaveApplicationInfo = New System.Windows.Forms.Button()
        Me.btnLaunchInstructions = New System.Windows.Forms.Button()
        Me.gbRegister = New System.Windows.Forms.GroupBox()
        Me.btnSkipEntry = New System.Windows.Forms.Button()
        Me.gbRegister.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(338, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(368, 350)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'btnRegisterApplication
        '
        Me.btnRegisterApplication.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.btnRegisterApplication.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegisterApplication.ForeColor = System.Drawing.Color.White
        Me.btnRegisterApplication.Location = New System.Drawing.Point(10, 15)
        Me.btnRegisterApplication.Name = "btnRegisterApplication"
        Me.btnRegisterApplication.Size = New System.Drawing.Size(185, 46)
        Me.btnRegisterApplication.TabIndex = 2
        Me.btnRegisterApplication.Text = "Register Application"
        Me.btnRegisterApplication.UseVisualStyleBackColor = False
        '
        'txtClientID
        '
        Me.txtClientID.Location = New System.Drawing.Point(10, 80)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.Size = New System.Drawing.Size(244, 20)
        Me.txtClientID.TabIndex = 3
        '
        'lblClientID
        '
        Me.lblClientID.AutoSize = True
        Me.lblClientID.Location = New System.Drawing.Point(7, 64)
        Me.lblClientID.Name = "lblClientID"
        Me.lblClientID.Size = New System.Drawing.Size(47, 13)
        Me.lblClientID.TabIndex = 4
        Me.lblClientID.Text = "ClientID:"
        '
        'lblSecretKey
        '
        Me.lblSecretKey.AutoSize = True
        Me.lblSecretKey.Location = New System.Drawing.Point(7, 103)
        Me.lblSecretKey.Name = "lblSecretKey"
        Me.lblSecretKey.Size = New System.Drawing.Size(62, 13)
        Me.lblSecretKey.TabIndex = 6
        Me.lblSecretKey.Text = "Secret Key:"
        '
        'txtSecretKey
        '
        Me.txtSecretKey.Location = New System.Drawing.Point(10, 119)
        Me.txtSecretKey.Name = "txtSecretKey"
        Me.txtSecretKey.Size = New System.Drawing.Size(299, 20)
        Me.txtSecretKey.TabIndex = 5
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.Location = New System.Drawing.Point(256, 64)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(29, 13)
        Me.lblPort.TabIndex = 8
        Me.lblPort.Text = "Port:"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(259, 80)
        Me.txtPort.MaxLength = 5
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(50, 20)
        Me.txtPort.TabIndex = 7
        Me.txtPort.Text = "13000"
        '
        'txtScopes
        '
        Me.txtScopes.Location = New System.Drawing.Point(9, 158)
        Me.txtScopes.Multiline = True
        Me.txtScopes.Name = "txtScopes"
        Me.txtScopes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtScopes.Size = New System.Drawing.Size(300, 144)
        Me.txtScopes.TabIndex = 9
        '
        'lblScopes
        '
        Me.lblScopes.AutoSize = True
        Me.lblScopes.Location = New System.Drawing.Point(7, 142)
        Me.lblScopes.Name = "lblScopes"
        Me.lblScopes.Size = New System.Drawing.Size(46, 13)
        Me.lblScopes.TabIndex = 10
        Me.lblScopes.Text = "Scopes:"
        '
        'btnSaveApplicationInfo
        '
        Me.btnSaveApplicationInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.btnSaveApplicationInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveApplicationInfo.ForeColor = System.Drawing.Color.White
        Me.btnSaveApplicationInfo.Location = New System.Drawing.Point(25, 313)
        Me.btnSaveApplicationInfo.Name = "btnSaveApplicationInfo"
        Me.btnSaveApplicationInfo.Size = New System.Drawing.Size(145, 46)
        Me.btnSaveApplicationInfo.TabIndex = 11
        Me.btnSaveApplicationInfo.Text = "Save Data"
        Me.btnSaveApplicationInfo.UseVisualStyleBackColor = False
        '
        'btnLaunchInstructions
        '
        Me.btnLaunchInstructions.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.btnLaunchInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnLaunchInstructions.ForeColor = System.Drawing.Color.White
        Me.btnLaunchInstructions.Location = New System.Drawing.Point(194, 15)
        Me.btnLaunchInstructions.Name = "btnLaunchInstructions"
        Me.btnLaunchInstructions.Size = New System.Drawing.Size(115, 46)
        Me.btnLaunchInstructions.TabIndex = 12
        Me.btnLaunchInstructions.Text = "Instructions"
        Me.btnLaunchInstructions.UseVisualStyleBackColor = False
        '
        'gbRegister
        '
        Me.gbRegister.Controls.Add(Me.btnSkipEntry)
        Me.gbRegister.Controls.Add(Me.btnSaveApplicationInfo)
        Me.gbRegister.Controls.Add(Me.lblScopes)
        Me.gbRegister.Controls.Add(Me.btnRegisterApplication)
        Me.gbRegister.Controls.Add(Me.txtClientID)
        Me.gbRegister.Controls.Add(Me.lblSecretKey)
        Me.gbRegister.Controls.Add(Me.lblClientID)
        Me.gbRegister.Controls.Add(Me.btnLaunchInstructions)
        Me.gbRegister.Controls.Add(Me.txtSecretKey)
        Me.gbRegister.Controls.Add(Me.txtPort)
        Me.gbRegister.Controls.Add(Me.txtScopes)
        Me.gbRegister.Controls.Add(Me.lblPort)
        Me.gbRegister.Location = New System.Drawing.Point(12, 12)
        Me.gbRegister.Name = "gbRegister"
        Me.gbRegister.Size = New System.Drawing.Size(320, 365)
        Me.gbRegister.TabIndex = 13
        Me.gbRegister.TabStop = False
        '
        'btnSkipEntry
        '
        Me.btnSkipEntry.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.btnSkipEntry.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSkipEntry.ForeColor = System.Drawing.Color.White
        Me.btnSkipEntry.Location = New System.Drawing.Point(170, 313)
        Me.btnSkipEntry.Name = "btnSkipEntry"
        Me.btnSkipEntry.Size = New System.Drawing.Size(125, 46)
        Me.btnSkipEntry.TabIndex = 13
        Me.btnSkipEntry.Text = "Skip Entry"
        Me.btnSkipEntry.UseVisualStyleBackColor = False
        '
        'frmLoadESIAuthorization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(714, 393)
        Me.Controls.Add(Me.gbRegister)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadESIAuthorization"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ESI Authorization - Register EVE IPH"
        Me.gbRegister.ResumeLayout(False)
        Me.gbRegister.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents btnRegisterApplication As Button
    Friend WithEvents txtClientID As TextBox
    Friend WithEvents lblClientID As Label
    Friend WithEvents lblSecretKey As Label
    Friend WithEvents txtSecretKey As TextBox
    Friend WithEvents lblPort As Label
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtScopes As TextBox
    Friend WithEvents lblScopes As Label
    Friend WithEvents btnSaveApplicationInfo As Button
    Friend WithEvents btnLaunchInstructions As Button
    Friend WithEvents gbRegister As GroupBox
    Friend WithEvents btnSkipEntry As Button
End Class
