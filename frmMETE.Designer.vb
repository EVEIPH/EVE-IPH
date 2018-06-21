<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMETE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMETE))
        Me.METEFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.SuspendLayout()
        '
        'METEFacility
        '
        Me.METEFacility.Location = New System.Drawing.Point(12, 23)
        Me.METEFacility.Name = "METEFacility"
        Me.METEFacility.Size = New System.Drawing.Size(300, 125)
        Me.METEFacility.TabIndex = 0
        '
        'frmMETE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 490)
        Me.Controls.Add(Me.METEFacility)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMETE"
        Me.Text = "frmMETE"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents METEFacility As ManufacturingFacility
End Class
