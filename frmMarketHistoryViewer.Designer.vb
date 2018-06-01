<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMarketHistoryViewer
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMarketHistoryViewer))
        Me.chrtMarketHistory = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chkVolume = New System.Windows.Forms.CheckBox()
        Me.chk5DayAverage = New System.Windows.Forms.CheckBox()
        Me.chk20DayAverage = New System.Windows.Forms.CheckBox()
        Me.chkMinMax = New System.Windows.Forms.CheckBox()
        Me.chkDonchianChannel = New System.Windows.Forms.CheckBox()
        Me.gbDateSelect = New System.Windows.Forms.GroupBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblAvgPrice = New System.Windows.Forms.Label()
        Me.cmbAvgPriceDuration = New System.Windows.Forms.ComboBox()
        Me.rbtnByDays = New System.Windows.Forms.RadioButton()
        Me.rbtnByDate = New System.Windows.Forms.RadioButton()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.gbDataOptions = New System.Windows.Forms.GroupBox()
        Me.chkLinearAverage = New System.Windows.Forms.CheckBox()
        Me.gbTrendOptions = New System.Windows.Forms.GroupBox()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.chrtMarketHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDateSelect.SuspendLayout()
        Me.gbDataOptions.SuspendLayout()
        Me.gbTrendOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'chrtMarketHistory
        '
        Me.chrtMarketHistory.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid
        ChartArea1.Name = "Main"
        Me.chrtMarketHistory.ChartAreas.Add(ChartArea1)
        Me.chrtMarketHistory.Location = New System.Drawing.Point(12, 12)
        Me.chrtMarketHistory.Name = "chrtMarketHistory"
        Me.chrtMarketHistory.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale
        Me.chrtMarketHistory.Size = New System.Drawing.Size(860, 504)
        Me.chrtMarketHistory.TabIndex = 0
        Title1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "RegionName"
        Title1.Text = "Name Here"
        Title2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title2.Name = "ItemName"
        Title2.Position.Auto = False
        Title2.Position.Height = 5.271496!
        Title2.Position.Width = 94.0!
        Title2.Position.X = 3.0!
        Title2.Position.Y = 8.0!
        Title2.Text = "Name Here"
        Me.chrtMarketHistory.Titles.Add(Title1)
        Me.chrtMarketHistory.Titles.Add(Title2)
        '
        'chkVolume
        '
        Me.chkVolume.AutoSize = True
        Me.chkVolume.Location = New System.Drawing.Point(6, 36)
        Me.chkVolume.Name = "chkVolume"
        Me.chkVolume.Size = New System.Drawing.Size(61, 17)
        Me.chkVolume.TabIndex = 1
        Me.chkVolume.Text = "Volume"
        Me.chkVolume.UseVisualStyleBackColor = True
        '
        'chk5DayAverage
        '
        Me.chk5DayAverage.AutoSize = True
        Me.chk5DayAverage.Location = New System.Drawing.Point(6, 36)
        Me.chk5DayAverage.Name = "chk5DayAverage"
        Me.chk5DayAverage.Size = New System.Drawing.Size(135, 17)
        Me.chk5DayAverage.TabIndex = 2
        Me.chk5DayAverage.Text = "5 Day Moving Average"
        Me.chk5DayAverage.UseVisualStyleBackColor = True
        '
        'chk20DayAverage
        '
        Me.chk20DayAverage.AutoSize = True
        Me.chk20DayAverage.Location = New System.Drawing.Point(6, 54)
        Me.chk20DayAverage.Name = "chk20DayAverage"
        Me.chk20DayAverage.Size = New System.Drawing.Size(141, 17)
        Me.chk20DayAverage.TabIndex = 3
        Me.chk20DayAverage.Text = "20 Day Moving Average"
        Me.chk20DayAverage.UseVisualStyleBackColor = True
        '
        'chkMinMax
        '
        Me.chkMinMax.AutoSize = True
        Me.chkMinMax.Location = New System.Drawing.Point(6, 18)
        Me.chkMinMax.Name = "chkMinMax"
        Me.chkMinMax.Size = New System.Drawing.Size(163, 17)
        Me.chkMinMax.TabIndex = 5
        Me.chkMinMax.Text = "Minimum / Maxium Day Price"
        Me.chkMinMax.UseVisualStyleBackColor = True
        '
        'chkDonchianChannel
        '
        Me.chkDonchianChannel.AutoSize = True
        Me.chkDonchianChannel.Location = New System.Drawing.Point(67, 18)
        Me.chkDonchianChannel.Name = "chkDonchianChannel"
        Me.chkDonchianChannel.Size = New System.Drawing.Size(114, 17)
        Me.chkDonchianChannel.TabIndex = 7
        Me.chkDonchianChannel.Text = "Donchian Channel"
        Me.chkDonchianChannel.UseVisualStyleBackColor = True
        '
        'gbDateSelect
        '
        Me.gbDateSelect.Controls.Add(Me.btnRefresh)
        Me.gbDateSelect.Controls.Add(Me.lblAvgPrice)
        Me.gbDateSelect.Controls.Add(Me.cmbAvgPriceDuration)
        Me.gbDateSelect.Controls.Add(Me.rbtnByDays)
        Me.gbDateSelect.Controls.Add(Me.rbtnByDate)
        Me.gbDateSelect.Controls.Add(Me.lblStartDate)
        Me.gbDateSelect.Controls.Add(Me.dtpStartDate)
        Me.gbDateSelect.Controls.Add(Me.lblEndDate)
        Me.gbDateSelect.Controls.Add(Me.dtpEndDate)
        Me.gbDateSelect.Location = New System.Drawing.Point(12, 522)
        Me.gbDateSelect.Name = "gbDateSelect"
        Me.gbDateSelect.Size = New System.Drawing.Size(368, 77)
        Me.gbDateSelect.TabIndex = 68
        Me.gbDateSelect.TabStop = False
        Me.gbDateSelect.Text = "Select Date Range:"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(277, 50)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(74, 21)
        Me.btnRefresh.TabIndex = 73
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblAvgPrice
        '
        Me.lblAvgPrice.AutoSize = True
        Me.lblAvgPrice.Location = New System.Drawing.Point(274, 11)
        Me.lblAvgPrice.Name = "lblAvgPrice"
        Me.lblAvgPrice.Size = New System.Drawing.Size(77, 13)
        Me.lblAvgPrice.TabIndex = 15
        Me.lblAvgPrice.Text = "Average Days:"
        Me.lblAvgPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbAvgPriceDuration
        '
        Me.cmbAvgPriceDuration.FormattingEnabled = True
        Me.cmbAvgPriceDuration.Items.AddRange(New Object() {"7", "15", "30", "60", "90", "180", "365"})
        Me.cmbAvgPriceDuration.Location = New System.Drawing.Point(277, 27)
        Me.cmbAvgPriceDuration.MaxLength = 3
        Me.cmbAvgPriceDuration.Name = "cmbAvgPriceDuration"
        Me.cmbAvgPriceDuration.Size = New System.Drawing.Size(74, 21)
        Me.cmbAvgPriceDuration.TabIndex = 16
        '
        'rbtnByDays
        '
        Me.rbtnByDays.AutoSize = True
        Me.rbtnByDays.Location = New System.Drawing.Point(14, 22)
        Me.rbtnByDays.Name = "rbtnByDays"
        Me.rbtnByDays.Size = New System.Drawing.Size(64, 17)
        Me.rbtnByDays.TabIndex = 14
        Me.rbtnByDays.TabStop = True
        Me.rbtnByDays.Text = "By Days"
        Me.rbtnByDays.UseVisualStyleBackColor = True
        '
        'rbtnByDate
        '
        Me.rbtnByDate.AutoSize = True
        Me.rbtnByDate.Location = New System.Drawing.Point(14, 45)
        Me.rbtnByDate.Name = "rbtnByDate"
        Me.rbtnByDate.Size = New System.Drawing.Size(63, 17)
        Me.rbtnByDate.TabIndex = 13
        Me.rbtnByDate.TabStop = True
        Me.rbtnByDate.Text = "By Date"
        Me.rbtnByDate.UseVisualStyleBackColor = True
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(94, 24)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(58, 13)
        Me.lblStartDate.TabIndex = 11
        Me.lblStartDate.Text = "Start Date:"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = ""
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(158, 20)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(99, 20)
        Me.dtpStartDate.TabIndex = 9
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(94, 48)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(55, 13)
        Me.lblEndDate.TabIndex = 12
        Me.lblEndDate.Text = "End Date:"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CustomFormat = ""
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(158, 44)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(99, 20)
        Me.dtpEndDate.TabIndex = 10
        Me.dtpEndDate.Value = New Date(2014, 1, 29, 21, 10, 0, 0)
        '
        'gbDataOptions
        '
        Me.gbDataOptions.Controls.Add(Me.chkMinMax)
        Me.gbDataOptions.Controls.Add(Me.chkVolume)
        Me.gbDataOptions.Location = New System.Drawing.Point(386, 522)
        Me.gbDataOptions.Name = "gbDataOptions"
        Me.gbDataOptions.Size = New System.Drawing.Size(178, 77)
        Me.gbDataOptions.TabIndex = 69
        Me.gbDataOptions.TabStop = False
        Me.gbDataOptions.Text = "Show Data:"
        '
        'chkLinearAverage
        '
        Me.chkLinearAverage.AutoSize = True
        Me.chkLinearAverage.Location = New System.Drawing.Point(6, 18)
        Me.chkLinearAverage.Name = "chkLinearAverage"
        Me.chkLinearAverage.Size = New System.Drawing.Size(55, 17)
        Me.chkLinearAverage.TabIndex = 4
        Me.chkLinearAverage.Text = "Linear"
        Me.chkLinearAverage.UseVisualStyleBackColor = True
        '
        'gbTrendOptions
        '
        Me.gbTrendOptions.Controls.Add(Me.chkLinearAverage)
        Me.gbTrendOptions.Controls.Add(Me.chkDonchianChannel)
        Me.gbTrendOptions.Controls.Add(Me.chk20DayAverage)
        Me.gbTrendOptions.Controls.Add(Me.chk5DayAverage)
        Me.gbTrendOptions.Location = New System.Drawing.Point(570, 522)
        Me.gbTrendOptions.Name = "gbTrendOptions"
        Me.gbTrendOptions.Size = New System.Drawing.Size(184, 77)
        Me.gbTrendOptions.TabIndex = 70
        Me.gbTrendOptions.TabStop = False
        Me.gbTrendOptions.Text = "Trendlines:"
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(760, 527)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(112, 33)
        Me.btnSaveSettings.TabIndex = 71
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(760, 566)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(112, 33)
        Me.btnClose.TabIndex = 72
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmMarketHistoryViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(884, 611)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gbTrendOptions)
        Me.Controls.Add(Me.gbDataOptions)
        Me.Controls.Add(Me.gbDateSelect)
        Me.Controls.Add(Me.chrtMarketHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMarketHistoryViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Market Viewer"
        CType(Me.chrtMarketHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDateSelect.ResumeLayout(False)
        Me.gbDateSelect.PerformLayout()
        Me.gbDataOptions.ResumeLayout(False)
        Me.gbDataOptions.PerformLayout()
        Me.gbTrendOptions.ResumeLayout(False)
        Me.gbTrendOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chrtMarketHistory As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents chkVolume As System.Windows.Forms.CheckBox
    Friend WithEvents chk5DayAverage As System.Windows.Forms.CheckBox
    Friend WithEvents chk20DayAverage As System.Windows.Forms.CheckBox
    Friend WithEvents chkMinMax As System.Windows.Forms.CheckBox
    Friend WithEvents chkDonchianChannel As System.Windows.Forms.CheckBox
    Friend WithEvents gbDateSelect As System.Windows.Forms.GroupBox
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents rbtnByDays As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnByDate As System.Windows.Forms.RadioButton
    Friend WithEvents lblAvgPrice As System.Windows.Forms.Label
    Friend WithEvents cmbAvgPriceDuration As System.Windows.Forms.ComboBox
    Friend WithEvents gbDataOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkLinearAverage As System.Windows.Forms.CheckBox
    Friend WithEvents gbTrendOptions As System.Windows.Forms.GroupBox
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
End Class
