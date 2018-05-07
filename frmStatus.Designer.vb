<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStatus
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
        Me.pgStatus = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pgStatus
        '
        Me.pgStatus.Location = New System.Drawing.Point(10, 42)
        Me.pgStatus.Name = "pgStatus"
        Me.pgStatus.Size = New System.Drawing.Size(254, 13)
        Me.pgStatus.TabIndex = 3
        Me.pgStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(13, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(249, 29)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(274, 66)
        Me.ControlBox = False
        Me.Controls.Add(Me.pgStatus)
        Me.Controls.Add(Me.lblStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStatus"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pgStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label

End Class
