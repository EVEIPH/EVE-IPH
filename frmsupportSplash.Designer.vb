<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmsupportSplash
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pbPaypal = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pbPaetron = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.pbPaypal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPaetron, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(290, 267)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 32)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'pbPaypal
        '
        Me.pbPaypal.BackColor = System.Drawing.Color.Transparent
        Me.pbPaypal.Image = Global.EVE_Isk_per_Hour.My.Resources.Resources.PayPalButton
        Me.pbPaypal.Location = New System.Drawing.Point(132, 264)
        Me.pbPaypal.Name = "pbPaypal"
        Me.pbPaypal.Size = New System.Drawing.Size(124, 35)
        Me.pbPaypal.TabIndex = 6
        Me.pbPaypal.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 89)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Are you Enjoying EVE IPH? Please support continued development by Donating!"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(47, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(294, 54)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "EVE IPH takes a lot of time to update and improve. If you can donate anything, it" &
    " would be much appreciated."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(47, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(294, 40)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Either donate to ZifrianEVE@gmail.com directly through Paypal or click the link b" &
    "elow."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(47, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(294, 23)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Thank you!"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbPaetron
        '
        Me.pbPaetron.BackColor = System.Drawing.Color.Transparent
        Me.pbPaetron.Image = Global.EVE_Isk_per_Hour.My.Resources.Resources.patreon_sm
        Me.pbPaetron.Location = New System.Drawing.Point(132, 223)
        Me.pbPaetron.Name = "pbPaetron"
        Me.pbPaetron.Size = New System.Drawing.Size(124, 35)
        Me.pbPaetron.TabIndex = 11
        Me.pbPaetron.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(47, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(294, 54)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "EVE IPH takes a lot of time to update and improve. If you can donate anything, it" &
    " would be much appreciated."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(47, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(294, 40)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Either donate to ZifrianEVE@gmail.com directly through Paypal or click the link b" &
    "elow."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmsupportSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 311)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbPaetron)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbPaypal)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmsupportSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.pbPaypal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPaetron, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents pbPaypal As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents pbPaetron As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
End Class
