<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCRESTStatus
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
        Me.pgCREST = New System.Windows.Forms.ProgressBar()
        Me.lblCRESTStatus = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pgCREST
        '
        Me.pgCREST.Location = New System.Drawing.Point(10, 42)
        Me.pgCREST.Name = "pgCREST"
        Me.pgCREST.Size = New System.Drawing.Size(254, 13)
        Me.pgCREST.TabIndex = 3
        Me.pgCREST.Visible = False
        '
        'lblCRESTStatus
        '
        Me.lblCRESTStatus.Location = New System.Drawing.Point(13, 10)
        Me.lblCRESTStatus.Name = "lblCRESTStatus"
        Me.lblCRESTStatus.Size = New System.Drawing.Size(249, 29)
        Me.lblCRESTStatus.TabIndex = 2
        Me.lblCRESTStatus.Text = "Status"
        Me.lblCRESTStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmCRESTStatus
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(274, 66)
        Me.ControlBox = False
        Me.Controls.Add(Me.pgCREST)
        Me.Controls.Add(Me.lblCRESTStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCRESTStatus"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pgCREST As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCRESTStatus As System.Windows.Forms.Label

End Class
