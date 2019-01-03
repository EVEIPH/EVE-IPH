<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManualESI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManualESI))
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.txtURL = New System.Windows.Forms.RichTextBox()
        Me.txtReturnLink = New System.Windows.Forms.RichTextBox()
        Me.lblReturnValue = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnLoadCharacter = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblInstructions
        '
        Me.lblInstructions.Location = New System.Drawing.Point(12, 9)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(460, 33)
        Me.lblInstructions.TabIndex = 0
        Me.lblInstructions.Text = "If you are unable to add characters to IPH and feel you have all the information " &
    "correct, paste the URL listed below into your web browser and hit enter.  You ma" &
    "y edit the URL here if needed."
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(12, 45)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(460, 125)
        Me.txtURL.TabIndex = 1
        Me.txtURL.Text = ""
        '
        'txtReturnLink
        '
        Me.txtReturnLink.Location = New System.Drawing.Point(12, 218)
        Me.txtReturnLink.Name = "txtReturnLink"
        Me.txtReturnLink.Size = New System.Drawing.Size(460, 74)
        Me.txtReturnLink.TabIndex = 2
        Me.txtReturnLink.Text = ""
        '
        'lblReturnValue
        '
        Me.lblReturnValue.Location = New System.Drawing.Point(12, 182)
        Me.lblReturnValue.Name = "lblReturnValue"
        Me.lblReturnValue.Size = New System.Drawing.Size(460, 33)
        Me.lblReturnValue.TabIndex = 3
        Me.lblReturnValue.Text = "Enter the response URL you received from your browser. The page will probably sho" &
    "w 'Unable to Connect' or another message. That's fine, just paste the resulting " &
    "URL from your browser here."
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(245, 298)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(99, 26)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnLoadCharacter
        '
        Me.btnLoadCharacter.Location = New System.Drawing.Point(140, 298)
        Me.btnLoadCharacter.Name = "btnLoadCharacter"
        Me.btnLoadCharacter.Size = New System.Drawing.Size(99, 26)
        Me.btnLoadCharacter.TabIndex = 6
        Me.btnLoadCharacter.Text = "Load Character"
        Me.btnLoadCharacter.UseVisualStyleBackColor = True
        '
        'frmManualESI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 331)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLoadCharacter)
        Me.Controls.Add(Me.lblReturnValue)
        Me.Controls.Add(Me.txtReturnLink)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.lblInstructions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmManualESI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add ESI Character Manually"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblInstructions As Label
    Friend WithEvents txtURL As RichTextBox
    Friend WithEvents txtReturnLink As RichTextBox
    Friend WithEvents lblReturnValue As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnLoadCharacter As Button
End Class
