<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSetCharacterDefault
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetCharacterDefault))
        Me.chkListDefaultChar = New System.Windows.Forms.CheckedListBox()
        Me.btnSelectDefault = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblKeyType = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'chkListDefaultChar
        '
        Me.chkListDefaultChar.CheckOnClick = True
        Me.chkListDefaultChar.FormattingEnabled = True
        Me.chkListDefaultChar.Location = New System.Drawing.Point(20, 68)
        Me.chkListDefaultChar.Name = "chkListDefaultChar"
        Me.chkListDefaultChar.Size = New System.Drawing.Size(270, 259)
        Me.chkListDefaultChar.TabIndex = 0
        Me.chkListDefaultChar.ThreeDCheckBoxes = True
        '
        'btnSelectDefault
        '
        Me.btnSelectDefault.Location = New System.Drawing.Point(53, 334)
        Me.btnSelectDefault.Name = "btnSelectDefault"
        Me.btnSelectDefault.Size = New System.Drawing.Size(99, 26)
        Me.btnSelectDefault.TabIndex = 4
        Me.btnSelectDefault.Text = "Select Default"
        Me.btnSelectDefault.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(158, 334)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(99, 26)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblKeyType
        '
        Me.lblKeyType.Location = New System.Drawing.Point(20, 9)
        Me.lblKeyType.Name = "lblKeyType"
        Me.lblKeyType.Size = New System.Drawing.Size(270, 56)
        Me.lblKeyType.TabIndex = 12
        Me.lblKeyType.Text = "Select the default character from the list below. If there are no characters sele" &
    "cted, then choose Add Character from the File Menu in the main program and add y" &
    "our characters via ESI."
        '
        'frmSetCharacterDefault
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(311, 372)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblKeyType)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSelectDefault)
        Me.Controls.Add(Me.chkListDefaultChar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetCharacterDefault"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Choose Default Character"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkListDefaultChar As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSelectDefault As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblKeyType As Label
End Class
