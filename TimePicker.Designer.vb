<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimePicker
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblDays = New System.Windows.Forms.Label()
        Me.lblHourMinColon = New System.Windows.Forms.Label()
        Me.lblMinSecColon = New System.Windows.Forms.Label()
        Me.Seconds = New EVE_Isk_per_Hour.MyDomainUpDown()
        Me.Minutes = New EVE_Isk_per_Hour.MyDomainUpDown()
        Me.Hours = New EVE_Isk_per_Hour.MyDomainUpDown()
        Me.Days = New EVE_Isk_per_Hour.MyDomainUpDown()
        Me.SuspendLayout()
        '
        'lblDays
        '
        Me.lblDays.AutoSize = True
        Me.lblDays.BackColor = System.Drawing.SystemColors.Window
        Me.lblDays.Location = New System.Drawing.Point(18, 3)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.Size = New System.Drawing.Size(31, 13)
        Me.lblDays.TabIndex = 77
        Me.lblDays.Text = "Days"
        '
        'lblHourMinColon
        '
        Me.lblHourMinColon.AutoSize = True
        Me.lblHourMinColon.BackColor = System.Drawing.SystemColors.Window
        Me.lblHourMinColon.Location = New System.Drawing.Point(60, 2)
        Me.lblHourMinColon.Name = "lblHourMinColon"
        Me.lblHourMinColon.Size = New System.Drawing.Size(10, 13)
        Me.lblHourMinColon.TabIndex = 80
        Me.lblHourMinColon.Text = ":"
        '
        'lblMinSecColon
        '
        Me.lblMinSecColon.AutoSize = True
        Me.lblMinSecColon.BackColor = System.Drawing.SystemColors.Window
        Me.lblMinSecColon.Location = New System.Drawing.Point(82, 2)
        Me.lblMinSecColon.Name = "lblMinSecColon"
        Me.lblMinSecColon.Size = New System.Drawing.Size(10, 13)
        Me.lblMinSecColon.TabIndex = 83
        Me.lblMinSecColon.Text = ":"
        '
        'Seconds
        '
        Me.Seconds.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Seconds.Location = New System.Drawing.Point(92, 3)
        Me.Seconds.Name = "Seconds"
        Me.Seconds.Size = New System.Drawing.Size(30, 16)
        Me.Seconds.TabIndex = 86
        Me.Seconds.Text = "00"
        '
        'Minutes
        '
        Me.Minutes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Minutes.Location = New System.Drawing.Point(70, 3)
        Me.Minutes.Name = "Minutes"
        Me.Minutes.Size = New System.Drawing.Size(30, 16)
        Me.Minutes.TabIndex = 84
        Me.Minutes.Text = "00"
        '
        'Hours
        '
        Me.Hours.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Hours.Location = New System.Drawing.Point(48, 3)
        Me.Hours.Name = "Hours"
        Me.Hours.Size = New System.Drawing.Size(30, 16)
        Me.Hours.TabIndex = 82
        Me.Hours.Text = "01"
        '
        'Days
        '
        Me.Days.BackColor = System.Drawing.SystemColors.Window
        Me.Days.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Days.Location = New System.Drawing.Point(-17, 3)
        Me.Days.Name = "Days"
        Me.Days.Size = New System.Drawing.Size(34, 16)
        Me.Days.TabIndex = 81
        Me.Days.Text = "0"
        Me.Days.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Days.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'TimePicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblMinSecColon)
        Me.Controls.Add(Me.lblHourMinColon)
        Me.Controls.Add(Me.Seconds)
        Me.Controls.Add(Me.Minutes)
        Me.Controls.Add(Me.Hours)
        Me.Controls.Add(Me.lblDays)
        Me.Controls.Add(Me.Days)
        Me.Name = "TimePicker"
        Me.Size = New System.Drawing.Size(110, 19)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDays As System.Windows.Forms.Label
    Friend WithEvents lblHourMinColon As System.Windows.Forms.Label
    Friend WithEvents Days As EVE_Isk_per_Hour.MyDomainUpDown
    Friend WithEvents Hours As EVE_Isk_per_Hour.MyDomainUpDown
    Friend WithEvents lblMinSecColon As System.Windows.Forms.Label
    Friend WithEvents Minutes As EVE_Isk_per_Hour.MyDomainUpDown
    Friend WithEvents Seconds As EVE_Isk_per_Hour.MyDomainUpDown

End Class
