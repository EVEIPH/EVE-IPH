Imports System.Data.SQLite
Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmMarketHistoryViewer

    Private FirstFormLoad As Boolean
    Private ItemID As Long
    Private ItemName As String
    Private RegionID As Long
    Private RegionName As String
    Private Days As Integer
    Private UseCRESTData As Boolean

    Public Sub New(ByVal _ItemID As Long, ByVal _ItemName As String, ByVal _RegionID As Long, ByVal _RegionName As String, ByVal _Days As Integer, ByVal _UseCRESTData As Boolean)

        FirstFormLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ItemID = _ItemID
        ItemName = _ItemName
        RegionID = _RegionID
        RegionName = _RegionName
        Days = _Days
        UseCRESTData = _UseCRESTData

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

    Private Sub RefreshGraph()
        Dim SQL As String
        Dim rsHistory As SQLiteDataReader

        Dim AveragePrices As New List(Of Double)
        Dim Volumes As New List(Of Integer)
        Dim MaxPrices As New List(Of Double)
        Dim MinPrices As New List(Of Double)
        Dim DonchianMin5 As New List(Of Double)
        Dim DonchianMax5 As New List(Of Double)

        Dim XDate As String
        Dim YAveragePrice As Double = 0
        Dim YVolume As Integer

        Dim DonchianCount As Integer = 0

        Dim Count As Integer = 0

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
            .Series("Prices").ChartArea = "Prices"

            If chkVolume.Checked Then
                .Series.Add("Volume")
                .Series("Volume").XAxisType = AxisType.Primary
                .Series("Volume").YAxisType = AxisType.Secondary
                .Series("Volume").ChartType = SeriesChartType.Column
                .Series("Volume").IsVisibleInLegend = False
                .Series("Volume").ChartArea = "Prices"
            End If

            If chkLinearAverage.Checked Then
                .Series.Add("LinearTrend")
                .Series("LinearTrend").ChartType = SeriesChartType.Line
                .Series("LinearTrend").XAxisType = AxisType.Primary
                .Series("LinearTrend").YAxisType = AxisType.Primary
                .Series("LinearTrend").BorderWidth = 1
                .Series("LinearTrend").Color = Color.Crimson
                .Series("LinearTrend").IsVisibleInLegend = False
                .Series("LinearTrend").ChartArea = "Prices"
            End If

            If chk5DayAverage.Checked Then
                .Series.Add("5DayTrend")
                .Series("5DayTrend").ChartType = SeriesChartType.Line
                .Series("5DayTrend").XAxisType = AxisType.Primary
                .Series("5DayTrend").YAxisType = AxisType.Primary
                .Series("5DayTrend").BorderWidth = 2
                .Series("5DayTrend").Color = Color.SeaGreen
                .Series("5DayTrend").IsVisibleInLegend = False
                .Series("5DayTrend").ChartArea = "Prices"
            End If

            If chk20DayAverage.Checked Then
                .Series.Add("20DayTrend")
                .Series("20DayTrend").ChartType = SeriesChartType.Line
                .Series("20DayTrend").XAxisType = AxisType.Primary
                .Series("20DayTrend").YAxisType = AxisType.Primary
                .Series("20DayTrend").BorderWidth = 2
                .Series("20DayTrend").Color = Color.DarkOrange
                .Series("20DayTrend").IsVisibleInLegend = False
                .Series("20DayTrend").ChartArea = "Prices"
            End If

            If chkDonchianChannel.Checked Then
                .Series.Add("MinDonchian")
                .Series("MinDonchian").ChartType = SeriesChartType.Line
                .Series("MinDonchian").XAxisType = AxisType.Primary
                .Series("MinDonchian").YAxisType = AxisType.Primary
                .Series("MinDonchian").BorderWidth = 1
                .Series("MinDonchian").Color = Color.RoyalBlue
                .Series("MinDonchian").IsVisibleInLegend = False
                .Series("MinDonchian").ChartArea = "Prices"

                .Series.Add("MaxDonchian")
                .Series("MaxDonchian").ChartType = SeriesChartType.Line
                .Series("MaxDonchian").XAxisType = AxisType.Primary
                .Series("MaxDonchian").YAxisType = AxisType.Primary
                .Series("MaxDonchian").BorderWidth = 1
                .Series("MaxDonchian").Color = Color.RoyalBlue
                .Series("MaxDonchian").IsVisibleInLegend = False
                .Series("MaxDonchian").ChartArea = "Prices"
            End If
        End With

        If UseCRESTData Then
            SQL = "SELECT PRICE_HISTORY_DATE, AVG_PRICE, LOW_PRICE, HIGH_PRICE, TOTAL_VOLUME_FILLED FROM MARKET_HISTORY "
        Else ' EMD
            SQL = "SELECT PRICE_HISTORY_DATE, AVG_PRICE, LOW_PRICE, HIGH_PRICE, TOTAL_VOLUME_FILLED FROM EMD_ITEM_PRICE_HISTORY "
        End If

        SQL = SQL & "WHERE TYPE_ID = " & ItemID & " AND REGION_ID = " & RegionID & " "
        If rbtnByDate.Checked Then
            SQL = SQL & "AND DATETIME(PRICE_HISTORY_DATE) >= " & " DateTime('" & Format(dtpStartDate.Value, SQLiteDateFormat) & "') "
            SQL = SQL & "AND DATETIME(PRICE_HISTORY_DATE) < " & " DateTime('" & Format(dtpEndDate.Value, SQLiteDateFormat) & "') "
        Else
            SQL = SQL & "AND DATETIME(PRICE_HISTORY_DATE) >= "
            SQL = SQL & " DateTime('" & Format(DateAdd(DateInterval.Day, -(CInt(cmbAvgPriceDuration.Text) + 1), Now.Date), SQLiteDateFormat) & "') "
            SQL = SQL & "AND DATETIME(PRICE_HISTORY_DATE) < " & " DateTime('" & Format(Now, SQLiteDateFormat) & "') "
        End If

        SQL = SQL & "AND TOTAL_VOLUME_FILLED IS NOT NULL "
        SQL = SQL & "ORDER BY PRICE_HISTORY_DATE"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsHistory = DBCommand.ExecuteReader

        While rsHistory.Read()
            XDate = Format(DateValue(rsHistory.GetString(0)), "dd-MMM")
            YAveragePrice = rsHistory.GetDouble(1)
            YVolume = rsHistory.GetInt32(4)

            AveragePrices.Add(YAveragePrice)
            Volumes.Add(YVolume)
            MinPrices.Add(rsHistory.GetDouble(2))
            MaxPrices.Add(rsHistory.GetDouble(3))

            If chkMinMax.Checked Then
                ' min/max
                chrtMarketHistory.Series("Prices").Points.AddXY(XDate, YAveragePrice, rsHistory.GetDouble(2), rsHistory.GetDouble(3))
                chrtMarketHistory.Series("Prices").Points(Count).Color = Color.DarkBlue
                chrtMarketHistory.Series("Prices").Points(Count).MarkerSize = 1
            Else
                chrtMarketHistory.Series("Prices").Points.AddXY(XDate, YAveragePrice)
                chrtMarketHistory.Series("Prices").Points(Count).Color = Color.DarkBlue
                chrtMarketHistory.Series("Prices").Points(Count).MarkerSize = 4
                chrtMarketHistory.Series("Prices").Points(Count).MarkerStyle = MarkerStyle.Circle
            End If

            ' If they want volume, add it
            If chkVolume.Checked Then
                chrtMarketHistory.Series("Volume").Points.AddY(YVolume)
                chrtMarketHistory.Series("Volume").Points(Count).Color = Color.Maroon
                chrtMarketHistory.Series("Volume").Points(Count).MarkerSize = 1
            End If

            If chkDonchianChannel.Checked Then
                DonchianCount += 1

                ' Insert and Reset every 5 values
                If DonchianCount = 5 Then
                    DonchianMin5 = New List(Of Double)
                    DonchianMax5 = New List(Of Double)

                    For i = (Count - 4) To Count
                        ' Get min and max values
                        DonchianMin5.Add(MinPrices(i))
                        DonchianMax5.Add(MaxPrices(i))
                    Next
                    DonchianCount = 0
                End If

                If Count < 4 Then
                    chrtMarketHistory.Series("MinDonchian").Points.AddXY(XDate, rsHistory.GetDouble(2))
                    chrtMarketHistory.Series("MaxDonchian").Points.AddXY(XDate, rsHistory.GetDouble(3))
                Else
                    chrtMarketHistory.Series("MinDonchian").Points.AddXY(XDate, DonchianMin5.Min)
                    chrtMarketHistory.Series("MaxDonchian").Points.AddXY(XDate, DonchianMax5.Max)
                End If
            End If

            Count += 1

        End While

        rsHistory.Close()

        ' Exit if no records
        If Count = 0 Then
            MsgBox("No Data to show", vbInformation, Application.ProductName)
            Exit Sub
        End If

        ' Reset chart
        If MaxPrices.Max / 1000000000 > 1 Then
            ' Billions
            chrtMarketHistory.ChartAreas("Prices").AxisY.LabelStyle.Format = "#,,," & "B"
        ElseIf MaxPrices.Max / 1000000 > 1 Then
            ' Millions
            chrtMarketHistory.ChartAreas("Prices").AxisY.LabelStyle.Format = "#,," & "M"
        ElseIf MaxPrices.Max / 1000 > 1 Then
            ' Thousands
            chrtMarketHistory.ChartAreas("Prices").AxisY.LabelStyle.Format = "#," & "K"
        End If

        ' Set up the chart to scale
        With chrtMarketHistory.ChartAreas("Prices")
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
            .AxisX2.MajorGrid.LineDashStyle = ChartDashStyle.NotSet
        End With

        ' Set trends if we have more than the needed data points
        If Count > 4 And chk5DayAverage.Checked Then
            chrtMarketHistory.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "5", chrtMarketHistory.Series("Prices"), chrtMarketHistory.Series("5DayTrend"))
        End If

        If Count > 19 And chk20DayAverage.Checked Then
            chrtMarketHistory.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "20", chrtMarketHistory.Series("Prices"), chrtMarketHistory.Series("20DayTrend"))
        End If

        If Count > 1 And chkLinearAverage.Checked Then
            chrtMarketHistory.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", chrtMarketHistory.Series("Prices"), chrtMarketHistory.Series("LinearTrend"))
        End If

    End Sub

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
        If Not firstformload Then
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