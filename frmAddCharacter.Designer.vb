<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddCharacter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddCharacter))
        Me.btnEVESSOLogin = New System.Windows.Forms.Button()
        Me.lblKeyType = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnEVESSOLogin
        '
        Me.btnEVESSOLogin.BackgroundImage = CType(resources.GetObject("btnEVESSOLogin.BackgroundImage"), System.Drawing.Image)
        Me.btnEVESSOLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnEVESSOLogin.Location = New System.Drawing.Point(155, 39)
        Me.btnEVESSOLogin.Name = "btnEVESSOLogin"
        Me.btnEVESSOLogin.Size = New System.Drawing.Size(270, 46)
        Me.btnEVESSOLogin.TabIndex = 2
        Me.btnEVESSOLogin.UseVisualStyleBackColor = True
        '
        'lblKeyType
        '
        Me.lblKeyType.AutoSize = True
        Me.lblKeyType.Location = New System.Drawing.Point(12, 9)
        Me.lblKeyType.Name = "lblKeyType"
        Me.lblKeyType.Size = New System.Drawing.Size(527, 13)
        Me.lblKeyType.TabIndex = 13
        Me.lblKeyType.Text = "Select the scopes you wish to authorize for this Character and log into EVE to au" &
    "thorize your character for IPH."
        '
        'frmAddCharacter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 388)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblKeyType)
        Me.Controls.Add(Me.btnEVESSOLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddCharacter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Character"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnEVESSOLogin As Button
    Friend WithEvents lblKeyType As Label
End Class
