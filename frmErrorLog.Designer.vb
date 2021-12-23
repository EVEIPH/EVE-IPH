<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmErrorLog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErrorLog))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.btnCopyLog = New System.Windows.Forms.Button()
        Me.btnClearLog = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Name = "btnClose"
        '
        'txtLog
        '
        Me.txtLog.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.txtLog, "txtLog")
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        '
        'btnCopyLog
        '
        resources.ApplyResources(Me.btnCopyLog, "btnCopyLog")
        Me.btnCopyLog.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCopyLog.Name = "btnCopyLog"
        '
        'btnClearLog
        '
        resources.ApplyResources(Me.btnClearLog, "btnClearLog")
        Me.btnClearLog.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClearLog.Name = "btnClearLog"
        '
        'frmErrorLog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.btnClearLog)
        Me.Controls.Add(Me.btnCopyLog)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmErrorLog"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents btnCopyLog As Button
    Friend WithEvents btnClearLog As Button
End Class
