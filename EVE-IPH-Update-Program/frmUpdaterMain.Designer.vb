<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdaterMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdaterMain))
        Me.lblUpdateMain = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pgUpdate = New System.Windows.Forms.ProgressBar()
        Me.BGWorker = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'lblUpdateMain
        '
        Me.lblUpdateMain.Location = New System.Drawing.Point(11, 14)
        Me.lblUpdateMain.Name = "lblUpdateMain"
        Me.lblUpdateMain.Size = New System.Drawing.Size(266, 19)
        Me.lblUpdateMain.TabIndex = 0
        Me.lblUpdateMain.Text = "Updating Program..."
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(98, 56)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pgUpdate
        '
        Me.pgUpdate.Location = New System.Drawing.Point(12, 36)
        Me.pgUpdate.Name = "pgUpdate"
        Me.pgUpdate.Size = New System.Drawing.Size(265, 14)
        Me.pgUpdate.TabIndex = 2
        Me.pgUpdate.Visible = False
        '
        'BGWorker
        '
        '
        'frmUpdaterMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(289, 89)
        Me.ControlBox = False
        Me.Controls.Add(Me.pgUpdate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblUpdateMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdaterMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE Isk per Hour - Updater"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblUpdateMain As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pgUpdate As System.Windows.Forms.ProgressBar
    Friend WithEvents BGWorker As System.ComponentModel.BackgroundWorker

End Class
