<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmError
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmError))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtError = New System.Windows.Forms.TextBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(274, 426)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(97, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "Close"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtError
        '
        Me.txtError.Location = New System.Drawing.Point(12, 5)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtError.Size = New System.Drawing.Size(505, 415)
        Me.txtError.TabIndex = 1
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(157, 426)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(97, 30)
        Me.btnCopy.TabIndex = 2
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'frmError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(529, 468)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.txtError)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmError"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE Isk per Hour - Unhandled Error"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents btnCopy As Button
End Class
