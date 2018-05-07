<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetCharacterDefault
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetCharacterDefault))
        Me.chkListDefaultChar = New System.Windows.Forms.CheckedListBox()
        Me.btnSelectDefault = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnEVESSOLogin = New System.Windows.Forms.Button()
        Me.lblKeyType = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'chkListDefaultChar
        '
        Me.chkListDefaultChar.CheckOnClick = True
        Me.chkListDefaultChar.FormattingEnabled = True
        Me.chkListDefaultChar.Location = New System.Drawing.Point(20, 106)
        Me.chkListDefaultChar.Name = "chkListDefaultChar"
        Me.chkListDefaultChar.Size = New System.Drawing.Size(270, 199)
        Me.chkListDefaultChar.TabIndex = 0
        Me.chkListDefaultChar.ThreeDCheckBoxes = True
        '
        'btnSelectDefault
        '
        Me.btnSelectDefault.Location = New System.Drawing.Point(42, 314)
        Me.btnSelectDefault.Name = "btnSelectDefault"
        Me.btnSelectDefault.Size = New System.Drawing.Size(99, 26)
        Me.btnSelectDefault.TabIndex = 4
        Me.btnSelectDefault.Text = "Select"
        Me.btnSelectDefault.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(170, 314)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(99, 26)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEVESSOLogin
        '
        Me.btnEVESSOLogin.BackgroundImage = CType(resources.GetObject("btnEVESSOLogin.BackgroundImage"), System.Drawing.Image)
        Me.btnEVESSOLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnEVESSOLogin.Location = New System.Drawing.Point(20, 54)
        Me.btnEVESSOLogin.Name = "btnEVESSOLogin"
        Me.btnEVESSOLogin.Size = New System.Drawing.Size(270, 46)
        Me.btnEVESSOLogin.TabIndex = 1
        Me.btnEVESSOLogin.UseVisualStyleBackColor = True
        '
        'lblKeyType
        '
        Me.lblKeyType.Location = New System.Drawing.Point(20, 9)
        Me.lblKeyType.Name = "lblKeyType"
        Me.lblKeyType.Size = New System.Drawing.Size(270, 42)
        Me.lblKeyType.TabIndex = 12
        Me.lblKeyType.Text = "Select the default character from the list below. If there are no characters sele" &
    "cted, then log in to the EVE SSO below to authorize your caracters for use in IP" &
    "H."
        '
        'frmSetCharacterDefault
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(311, 352)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblKeyType)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnEVESSOLogin)
        Me.Controls.Add(Me.btnSelectDefault)
        Me.Controls.Add(Me.chkListDefaultChar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetCharacterDefault"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Choose Default Character"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkListDefaultChar As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSelectDefault As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEVESSOLogin As Button
    Friend WithEvents lblKeyType As Label
End Class
