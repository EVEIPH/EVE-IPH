<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAPIError
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAPIError))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblMain = New System.Windows.Forms.Label()
        Me.llMain = New System.Windows.Forms.LinkLabel()
        Me.lblLinkInstruction = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(162, 129)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(96, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblMain
        '
        Me.lblMain.Location = New System.Drawing.Point(11, 9)
        Me.lblMain.Name = "lblMain"
        Me.lblMain.Size = New System.Drawing.Size(399, 65)
        Me.lblMain.TabIndex = 1
        Me.lblMain.Text = "Label1"
        Me.lblMain.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'llMain
        '
        Me.llMain.Location = New System.Drawing.Point(11, 93)
        Me.llMain.Name = "llMain"
        Me.llMain.Size = New System.Drawing.Size(399, 21)
        Me.llMain.TabIndex = 2
        Me.llMain.TabStop = True
        Me.llMain.Text = "LinkLabel1"
        Me.llMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLinkInstruction
        '
        Me.lblLinkInstruction.Location = New System.Drawing.Point(11, 74)
        Me.lblLinkInstruction.Name = "lblLinkInstruction"
        Me.lblLinkInstruction.Size = New System.Drawing.Size(399, 19)
        Me.lblLinkInstruction.TabIndex = 3
        Me.lblLinkInstruction.Text = "Click here to create a link with needed access:"
        Me.lblLinkInstruction.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'frmAPIError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 170)
        Me.Controls.Add(Me.lblLinkInstruction)
        Me.Controls.Add(Me.llMain)
        Me.Controls.Add(Me.lblMain)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAPIError"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAPIError"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblMain As System.Windows.Forms.Label
    Friend WithEvents llMain As System.Windows.Forms.LinkLabel
    Friend WithEvents lblLinkInstruction As System.Windows.Forms.Label
End Class
