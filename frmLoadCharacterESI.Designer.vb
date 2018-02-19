<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadCharacterESI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadCharacterESI))
        Me.btnLaunchWebAuthorization = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnLaunchWebAuthorization
        '
        Me.btnLaunchWebAuthorization.BackgroundImage = CType(resources.GetObject("btnLaunchWebAuthorization.BackgroundImage"), System.Drawing.Image)
        Me.btnLaunchWebAuthorization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnLaunchWebAuthorization.Location = New System.Drawing.Point(13, 153)
        Me.btnLaunchWebAuthorization.Name = "btnLaunchWebAuthorization"
        Me.btnLaunchWebAuthorization.Size = New System.Drawing.Size(270, 46)
        Me.btnLaunchWebAuthorization.TabIndex = 1
        Me.btnLaunchWebAuthorization.UseVisualStyleBackColor = True
        '
        'frmLoadCharacterESI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(297, 211)
        Me.Controls.Add(Me.btnLaunchWebAuthorization)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadCharacterESI"
        Me.Text = "frmLoadCharacterESI"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnLaunchWebAuthorization As Button
End Class
