<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BugReportForm
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
        Me.OSBox = New System.Windows.Forms.TextBox()
        Me.OSLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabScreenBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ReproStepsBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ScreenLinkBox = New System.Windows.Forms.TextBox()
        Me.StackTraceLabel = New System.Windows.Forms.Label()
        Me.StackTraceBox = New System.Windows.Forms.TextBox()
        Me.CopyButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OSBox
        '
        Me.OSBox.Location = New System.Drawing.Point(12, 72)
        Me.OSBox.Name = "OSBox"
        Me.OSBox.Size = New System.Drawing.Size(258, 20)
        Me.OSBox.TabIndex = 0
        '
        'OSLabel
        '
        Me.OSLabel.AutoSize = True
        Me.OSLabel.Location = New System.Drawing.Point(12, 56)
        Me.OSLabel.Name = "OSLabel"
        Me.OSLabel.Size = New System.Drawing.Size(154, 13)
        Me.OSLabel.TabIndex = 2
        Me.OSLabel.Text = "What is your operating system?"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(249, 39)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "An Unhandled Exception has occured and EVE Isk" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "per Hour will now close. Please f" & _
    "ill out the following" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "information so I can investigate the bug."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(207, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "On what tab or screen did the error occur?"
        '
        'TabScreenBox
        '
        Me.TabScreenBox.Location = New System.Drawing.Point(12, 111)
        Me.TabScreenBox.Name = "TabScreenBox"
        Me.TabScreenBox.Size = New System.Drawing.Size(258, 20)
        Me.TabScreenBox.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(176, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Can you reproduce the error? How?"
        '
        'ReproStepsBox
        '
        Me.ReproStepsBox.Location = New System.Drawing.Point(12, 150)
        Me.ReproStepsBox.Multiline = True
        Me.ReproStepsBox.Name = "ReproStepsBox"
        Me.ReproStepsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ReproStepsBox.Size = New System.Drawing.Size(258, 112)
        Me.ReproStepsBox.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 265)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Link to a screenshot of the error"
        '
        'ScreenLinkBox
        '
        Me.ScreenLinkBox.Location = New System.Drawing.Point(12, 281)
        Me.ScreenLinkBox.Name = "ScreenLinkBox"
        Me.ScreenLinkBox.Size = New System.Drawing.Size(258, 20)
        Me.ScreenLinkBox.TabIndex = 8
        '
        'StackTraceLabel
        '
        Me.StackTraceLabel.AutoSize = True
        Me.StackTraceLabel.Location = New System.Drawing.Point(12, 307)
        Me.StackTraceLabel.Name = "StackTraceLabel"
        Me.StackTraceLabel.Size = New System.Drawing.Size(62, 13)
        Me.StackTraceLabel.TabIndex = 11
        Me.StackTraceLabel.Text = "Stack trace"
        '
        'StackTraceBox
        '
        Me.StackTraceBox.Enabled = False
        Me.StackTraceBox.Location = New System.Drawing.Point(12, 323)
        Me.StackTraceBox.Multiline = True
        Me.StackTraceBox.Name = "StackTraceBox"
        Me.StackTraceBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.StackTraceBox.Size = New System.Drawing.Size(258, 112)
        Me.StackTraceBox.TabIndex = 10
        '
        'CopyButton
        '
        Me.CopyButton.Location = New System.Drawing.Point(12, 441)
        Me.CopyButton.Name = "CopyButton"
        Me.CopyButton.Size = New System.Drawing.Size(258, 23)
        Me.CopyButton.TabIndex = 12
        Me.CopyButton.Text = "Copy to clipboard"
        Me.CopyButton.UseVisualStyleBackColor = True
        '
        'BugReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 474)
        Me.Controls.Add(Me.CopyButton)
        Me.Controls.Add(Me.StackTraceLabel)
        Me.Controls.Add(Me.StackTraceBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ScreenLinkBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ReproStepsBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TabScreenBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OSLabel)
        Me.Controls.Add(Me.OSBox)
        Me.Name = "BugReportForm"
        Me.Text = "Bug Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OSBox As System.Windows.Forms.TextBox
    Friend WithEvents OSLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabScreenBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReproStepsBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ScreenLinkBox As System.Windows.Forms.TextBox
    Friend WithEvents StackTraceLabel As System.Windows.Forms.Label
    Friend WithEvents StackTraceBox As System.Windows.Forms.TextBox
    Friend WithEvents CopyButton As System.Windows.Forms.Button
End Class
