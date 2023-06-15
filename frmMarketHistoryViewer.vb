Imports System.Data.SQLite
Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmMarketHistoryViewer

    Private FirstFormLoad As Boolean
    Private ItemID As Long
    Private ItemName As String
    Private RegionID As Long
    Private RegionName As String
    Private Days As Integer

    Public Sub New(ByVal _ItemID As Long, ByVal _ItemName As String, ByVal _RegionID As Long, ByVal _RegionName As String, ByVal _Days As Integer)

        FirstFormLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ItemID = _ItemID
        ItemName = _ItemName
        RegionID = _RegionID
        RegionName = _RegionName
        Days = _Days

        Call InitForm()

        FirstFormLoad = False

    End Sub

    Private Sub frmMarketHistoryViewer_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        Call RefreshGraph()

    End Sub

    Private Sub InitForm()

        With UserMHViewerSettings
            chkMinMax.Checked = .MinMaxDayPrice
            chkVolume.Checked = .Volume

            chkLinearAverage.Checked = .LinearTrend
            chkDonchianChannel.Checked = .DochianChannel
            chk5DayAverage.Checked = .FiveDayAvg
            chk20DayAverage.Checked = .TwentyDayAvg

            If .DatePreference = rbtnByDays.Text Then
                rbtnByDays.Checked = True
            Else
                rbtnByDate.Checked = True
            End If
        End With

        ' Dates are always set on what is sent to the constructor
        dtpStartDate.Value = DateAdd(DateInterval.Day, -(Days + 1), Now.Date)
        dtpEndDate.Value = Now

        cmbAvgPriceDuration.Text = CStr(Days)

    End Sub

    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click
        Dim TempSettings As MarketHistoryViewerSettings = Nothing
        Dim Settings As New ProgramSettings

        With TempSettings
            .MinMaxDayPrice = chkMinMax.Checked
            .Volume = chkVolume.Checked

            .LinearTrend = chkLinearAverage.Checked
            .FiveDayAvg = chk5DayAverage.Checked
            .TwentyDayAvg = chk20DayAverage.Checked
            .DochianChannel = chkDonchianChannel.Checked

            If rbtnByDays.Checked Then
                .DatePreference = rbtnByDays.Text
            Else
                .DatePreference = rbtnByDate.Text
            End If
        End With

        ' Save the data in the XML file
        Call Settings.SaveMarketHistoryViewerSettingsSettings(TempSettings)

        ' Save the data to the local variable
        UserMHViewerSettings = TempSettings

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Structure DataPoint
        Dim XValue As Integer
        Dim YValue As Double
        Dim XValueLabel As String
        Dim YVolume As Integer
        Dim YMinValue As Double
        Dim YMaxValue As Double
    End Structure

    Private Sub RefreshGraph()
        Dim SQL As String
        Dim rsHistory As SQLiteDataReader

        Dim AveragePrices As New List(Of Double)
        Dim Volumes As New List(Of Integer)
        Dim MaxPrices As New List(Of Double)
        Dim MinPrices As New List(Of Double)
        Dim DonchianMin5 As New List(Of Double)
        Dim DonchianMax5 As New List(Of Double)

        Dim Count As Integer = 0
        Dim MinXDate As Date

        Dim AllData As New List(Of DataPoint)
        Dim MainData As New List(Of DataPoint)
        Dim _5DayData As New List(Of DataPoint)
        Dim _20DayData As New List(Of DataPoint)
        Dim DonchianData As New List(Of DataPoint)

        Dim MainDataCount As Integer = 0
        Dim _5DayCount As Integer = 0
        Dim _20DayCount As Integer = 0

        Dim MH As New MarketPriceInterface(Nothing)

        ' Determine dates and add 20 days (subtract) to it regardless to help build the different trend lines (they get cut off from the front)
        Dim StartDate As Date
        Dim EndDate As Date

        If rbtnByDate.Checked Then
            StartDate = DateAdd(DateInterval.Day, -20, dtpStartDate.Value)
            MinXDate = dtpStartDate.Value
            EndDate = dtpEndDate.Value
        Else
            StartDate = DateAdd(DateInterval.Day, -(CInt(cmbAvgPriceDuration.Text) + 20), Date.UtcNow.Date)
            MinXDate = DateAdd(DateInterval.Day, -(CInt(cmbAvgPriceDuration.Text) + 1), Date.UtcNow.Date)
            EndDate = Now
        End If

        Dim TypeID As New List(Of Long)
        TypeID.Add(ItemID)

        ' Update the prices
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        If Not MH.UpdateESIPriceHistory(TypeID, RegionID) Then
            Call MsgBox("Some prices did not update. Please try again.", vbInformation, Application.ProductName)
        End If
        Me.Cursor = Cursors.Default
        Application.DoEvents()

        ' Set up the chart
        With chrtMarketHistory
            .Titles("RegionName").Text = RegionName & " Regional Market"
            .Titles("ItemName").Text = ItemName

            .Series.Clear()

            .Series.Add("Prices")
            .Series("Prices").XAxisType = AxisType.Primary
            .Series("Prices").YAxisType = AxisType.Primary
            If chkMinMax.Checked Then
                .Series("Prices").ChartType = SeriesChartType.ErrorBar
            Else
                .Series("Prices").ChartType = SeriesChartType.Point
            End If
            .Series("Prices").IsVisibleInLegend = False
            .Series("Prices").ChartArea = "Main"

            If chkVolume.Checked Then
                .Series.Add("Volume")
                .Series("Volume").XAxisType = AxisType.Primary
                .Series("Volume").YAxisType = AxisType.Secondary
                .Series("Volume").ChartType = SeriesChartType.Column
                .Series("Volume").IsVisibleInLegend = False
                .Series("Volume").ChartArea = "Main"
            End If

            If chkLinearAverage.Checked Then
                .Series.Add("LinearTrend")
                .Series("LinearTrend").ChartType = SeriesChartType.Line
                .Series("LinearTrend").XAxisType = AxisType.Primary
                .Series("LinearTrend").YAxisType = AxisType.Primary
                .Series("LinearTrend").BorderWidth = 1
                .Series("LinearTrend").Color = Color.Crimson
                .Series("LinearTrend").IsVisibleInLegend = False
                .Series("LinearTrend").ChartArea = "Main"
            End If

            If chk5DayAverage.Checked Then
                .Series.Add("5DayTrend")
                .Series("5DayTrend").ChartType = SeriesChartType.Line
                .Series("5DayTrend").XAxisType = AxisType.Primary
                .Series("5DayTrend").YAxisType = AxisType.Primary
                .Series("5DayTrend").BorderWidth = 2
                .Series("5DayTrend").Color = Color.SeaGreen
                .Series("5DayTrend").IsVisibleInLegend = False
                .Series("5DayTrend").ChartArea = "Main"
            End If

            If chk20DayAverage.Checked Then
                .Series.Add("20DayTrend")
                .Series("20DayTrend").ChartType = SeriesChartType.Line
                .Series("20DayTrend").XAxisType = AxisType.Primary
                .Series("20DayTrend").YAxisType = AxisType.Primary
                .Series("20DayTrend").BorderWidth = 2
                .Series("20DayTrend").Color = Color.DarkOrange
                .Series("20DayTrend").IsVisibleInLegend = False
                .Series("20DayTrend").ChartArea = "Main"
            End If

            If chkDonchianChannel.Checked Then
                .Series.Add("MinDonchian")
                .Series("MinDonchian").ChartType = SeriesChartType.Line
                .Series("MinDonchian").XAxisType = AxisType.Primary
                .Series("MinDonchian").YAxisType = AxisType.Primary
                .Series("MinDonchian").BorderWidth = 1
                .Series("MinDonchian").Color = Color.RoyalBlue
                .Series("MinDonchian").IsVisibleInLegend = False
                .Series("MinDonchian").ChartArea = "Main"

                .Series.Add("MaxDonchian")
                .Series("MaxDonchian").ChartType = SeriesChartType.Line
                .Series("MaxDonchian").XAxisType = AxisType.Primary
                .Series("MaxDonchian").YAxisType = AxisType.Primary
                .Series("MaxDonchian").BorderWidth = 1
                .Series("MaxDonchian").Color = Color.RoyalBlue
                .Series("MaxDonchian").IsVisibleInLegend = False
                .Series("MaxDonchian").ChartArea = "Main"
            End If
        End With

        SQL = "SELECT PRICE_HISTORY_DATE, AVG_PRICE, LOW_PRICE, HIGH_PRICE, TOTAL_VOLUME_FILLED FROM MARKET_HISTORY "
        SQL &= "WHERE TYPE_ID = " & ItemID & " AND REGION_ID = " & RegionID & " "
        SQL &= "AND DATETIME(PRICE_HISTORY_DATE) >= " & " DateTime('" & Format(StartDate, SQLiteDateFormat) & "') "
        SQL &= "AND DATETIME(PRICE_HISTORY_DATE) < " & " DateTime('" & Format(EndDate, SQLiteDateFormat) & "') "
        SQL &= "AND TOTAL_VOLUME_FILLED IS NOT NULL "
        SQL &= "ORDER BY PRICE_HISTORY_DATE"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsHistory = DBCommand.ExecuteReader

        Dim DP As New DataPoint
        Dim TempDP As New DataPoint

        ' Make all the series first, then add them after
        While rsHistory.Read()
            DP.XValue = Count
            DP.XValueLabel = Format(DateValue(rsHistory.GetString(0)), "dd-MMM")
            DP.YValue = rsHistory.GetDouble(1)
            DP.YMinValue = rsHistory.GetDouble(2)
            DP.YMaxValue = rsHistory.GetDouble(3)
            DP.YVolume = rsHistory.GetInt32(4)

            AveragePrices.Add(rsHistory.GetDouble(1))
            MinPrices.Add(rsHistory.GetDouble(2))
            MaxPrices.Add(rsHistory.GetDouble(3))
            Volumes.Add(rsHistory.GetInt32(4))

            ' If it's within the range, add to the main data
            If MinXDate < DateValue(rsHistory.GetString(0)) Then
                Dim StartIndex As Integer = 0

                TempDP = DP
                TempDP.XValue = MainDataCount

                MainData.Add(TempDP)
                MainDataCount += 1

                If Count <= 4 Then
                    StartIndex = 0
                Else
                    StartIndex = Count - 5
                End If

                ' Get the 5 day average
                TempDP.YValue = GetDayAverage(StartIndex, Count - 1, AllData)
                _5DayData.Add(TempDP)

                If Count <= 19 Then
                    StartIndex = 0
                Else
                    StartIndex = Count - 20
                End If

                ' Now get the 20 day average
                TempDP.YValue = GetDayAverage(StartIndex, Count - 1, AllData)
                _20DayData.Add(TempDP)

                ' Set the Donchian channel 
                DonchianMin5 = New List(Of Double)
                DonchianMax5 = New List(Of Double)

                If Count > 4 Then
                    For i = (Count - 4) To Count
                        ' Get min and max values from the main data
                        DonchianMin5.Add(MinPrices(i))
                        DonchianMax5.Add(MaxPrices(i))
                    Next

                    ' Set the data
                    TempDP.YMinValue = DonchianMin5.Min
                    TempDP.YMaxValue = DonchianMax5.Max
                    TempDP.YValue = 0
                    ' Add and reset
                    DonchianData.Add(TempDP)
                Else
                    ' Set the data
                    TempDP.YMinValue = 0
                    TempDP.YMaxValue = 0
                    TempDP.YValue = 0
                    ' Add and reset
                    DonchianData.Add(TempDP)
                End If

            End If

            AllData.Add(DP)
            Count += 1

        End While

        rsHistory.Close()

        ' Loop through and add all the series
        For i = 0 To MainData.Count - 1
            With MainData(i)
                If chkMinMax.Checked Then
                    ' min/max
                    chrtMarketHistory.Series("Prices").Points.AddXY(.XValue, .YValue, .YMinValue, .YMaxValue)
                    chrtMarketHistory.Series("Prices").Points(i).AxisLabel = .XValueLabel
                    chrtMarketHistory.Series("Prices").Points(i).Color = Color.DarkBlue
                    chrtMarketHistory.Series("Prices").Points(i).MarkerSize = 1
                Else
                    chrtMarketHistory.Series("Prices").Points.AddXY(.XValue, .YValue)
                    chrtMarketHistory.Series("Prices").Points(i).AxisLabel = .XValueLabel
                    chrtMarketHistory.Series("Prices").Points(i).Color = Color.DarkBlue
                    chrtMarketHistory.Series("Prices").Points(i).MarkerSize = 4
                    chrtMarketHistory.Series("Prices").Points(i).MarkerStyle = MarkerStyle.Circle
                End If


                ' If they want volume, add it
                If chkVolume.Checked Then
                    chrtMarketHistory.Series("Volume").Points.AddY(.YVolume)
                    chrtMarketHistory.Series("Volume").Points(i).Color = Color.Maroon
                    chrtMarketHistory.Series("Volume").Points(i).MarkerSize = 1
                End If

            End With

            If chk5DayAverage.Checked Then
                chrtMarketHistory.Series("5DayTrend").Points.AddXY(_5DayData(i).XValue, _5DayData(i).YValue)
                chrtMarketHistory.Series("5DayTrend").Points(i).AxisLabel = _5DayData(i).XValueLabel
            End If

            If chk20DayAverage.Checked Then
                chrtMarketHistory.Series("20DayTrend").Points.AddXY(_20DayData(i).XValue, _20DayData(i).YValue)
                chrtMarketHistory.Series("20DayTrend").Points(i).AxisLabel = _20DayData(i).XValueLabel
            End If


            If chkDonchianChannel.Checked Then
                chrtMarketHistory.Series("MinDonchian").Points.AddXY(DonchianData(i).XValue, DonchianData(i).YMinValue)
                chrtMarketHistory.Series("MinDonchian").Points(i).AxisLabel = DonchianData(i).XValueLabel
                chrtMarketHistory.Series("MaxDonchian").Points.AddXY(DonchianData(i).XValue, DonchianData(i).YMaxValue)
                chrtMarketHistory.Series("MaxDonchian").Points(i).AxisLabel = DonchianData(i).XValueLabel
            End If

        Next

        ' Exit if no records
        If Count = 0 Then
            MsgBox("No Data to show", vbInformation, Application.ProductName)
            Exit Sub
        End If

        ' Reset chart
        If MaxPrices.Max / 1000000000 > 1 Then
            ' Billions
            chrtMarketHistory.ChartAreas("Main").AxisY.LabelStyle.Format = "#,,," & "B"
        ElseIf MaxPrices.Max / 1000000 > 1 Then
            ' Millions
            chrtMarketHistory.ChartAreas("Main").AxisY.LabelStyle.Format = "#,," & "M"
        ElseIf MaxPrices.Max / 1000 > 1 Then
            ' Thousands
            chrtMarketHistory.ChartAreas("Main").AxisY.LabelStyle.Format = "#," & "K"
        End If

        ' Set up the chart to scale
        With chrtMarketHistory.ChartAreas("Main")
            If chkMinMax.Checked Or chkDonchianChannel.Checked Then
                .AxisY.Minimum = MinPrices.Min - (MinPrices.Min * 0.03)
                .AxisY.Maximum = MaxPrices.Max + (MaxPrices.Max * 0.03)
                .AxisX.Interval = CInt(Count / 7)
                .AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet
            Else
                .AxisY.Minimum = AveragePrices.Min - (AveragePrices.Min * 0.03)
                .AxisY.Maximum = AveragePrices.Max + (AveragePrices.Max * 0.03)
                .AxisX.Interval = CInt(Count / 7)
                .AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet
            End If

            ' For volume
            .AxisY2.Minimum = 0
            .AxisY2.Maximum = Volumes.Max * 5
            .AxisY2.LabelStyle.Format = "#,##0"
            .AxisX2.MajorGrid.LineDashStyle = ChartDashStyle.NotSet
        End With

        If MainData.Count > 1 And chkLinearAverage.Checked Then
            ' Use the price series only for linear
            chrtMarketHistory.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", chrtMarketHistory.Series("Prices"), chrtMarketHistory.Series("LinearTrend"))
        End If

    End Sub

    Private Function GetDayAverage(StartIndex As Integer, EndIndex As Integer, ByRef Data As List(Of DataPoint)) As Double
        Dim Sum As Double = 0

        For i = StartIndex To EndIndex
            Sum += Data(i).YValue
        Next

        Return Sum / (EndIndex - StartIndex + 1)

    End Function

    Private Sub dtpStartDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpStartDate.ValueChanged
        If Not FirstFormLoad Then
            ' For some reason, a > of two equal dates returns true when it should be false - so decrement by 1 second
            If DateAdd(DateInterval.Second, -1, dtpStartDate.Value) > dtpEndDate.Value Then
                MsgBox("The Start Date cannot be greater than the End Date", vbExclamation, Application.ProductName)
                dtpStartDate.Value = DateAdd(DateInterval.Day, -1, dtpEndDate.Value)
                dtpStartDate.Focus()
            Else
                Call RefreshGraph()
            End If
        End If
    End Sub

    Private Sub dtpEndDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpEndDate.ValueChanged
        If Not FirstFormLoad Then
            If dtpStartDate.Value > dtpEndDate.Value Then
                MsgBox("The End Date cannot be less than the Start Date", vbExclamation, Application.ProductName)
                dtpEndDate.Value = DateAdd(DateInterval.Day, 1, dtpStartDate.Value)
                dtpStartDate.Focus()
            Else
                Call RefreshGraph()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub chkMinMax_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMinMax.CheckedChanged
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub chkVolume_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVolume.CheckedChanged
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub chkLinearAverage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLinearAverage.CheckedChanged
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDonchianChannel.CheckedChanged
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub chk5DayAverage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk5DayAverage.CheckedChanged
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub chk20DayAverage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk20DayAverage.CheckedChanged
        If Not FirstFormLoad Then
            Call RefreshGraph()
        End If
    End Sub

    Private Sub rbtnByDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnByDate.CheckedChanged
        Call ToggleDateEntry()
    End Sub

    Private Sub rbtnByDays_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnByDays.CheckedChanged
        Call ToggleDateEntry()
    End Sub

    Private Sub ToggleDateEntry()
        If rbtnByDate.Checked Then
            dtpStartDate.Enabled = True
            dtpEndDate.Enabled = True
            lblEndDate.Enabled = True
            lblStartDate.Enabled = True

            lblAvgPrice.Enabled = False
            cmbAvgPriceDuration.Enabled = False
            btnRefresh.Enabled = False
        Else
            dtpStartDate.Enabled = False
            dtpEndDate.Enabled = False
            lblEndDate.Enabled = False
            lblStartDate.Enabled = False

            lblAvgPrice.Enabled = True
            cmbAvgPriceDuration.Enabled = True
            btnRefresh.Enabled = True
        End If
    End Sub

    Private Sub cmbAvgPriceDuration_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbAvgPriceDuration.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedRunschars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Call RefreshGraph()
    End Sub

End Class