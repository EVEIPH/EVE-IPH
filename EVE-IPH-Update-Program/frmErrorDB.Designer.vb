<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmErrorDB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErrorDB))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.llblAccessDBLink = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(102, 72)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(84, 24)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(263, 30)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "This verison of EVE IPH is no longer supported. Please visit the link below for t" & _
    "he latest version."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'llblAccessDBLink
        '
        Me.llblAccessDBLink.Location = New System.Drawing.Point(13, 44)
        Me.llblAccessDBLink.Name = "llblAccessDBLink"
        Me.llblAccessDBLink.Size = New System.Drawing.Size(263, 20)
        Me.llblAccessDBLink.TabIndex = 3
        Me.llblAccessDBLink.TabStop = True
        Me.llblAccessDBLink.Text = "http://eveiph.github.io/"
        Me.llblAccessDBLink.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmErrorDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(285, 110)
        Me.Controls.Add(Me.llblAccessDBLink)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmErrorDB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE Isk per Hour - Unsupported Version"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents llblAccessDBLink As System.Windows.Forms.LinkLabel
End Class
