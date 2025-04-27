Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Threading
Public Class frmRefreshMarketHistory

    Private RegionComboLoaded As Boolean = True
    Private SelectedRegionID As Long
    Private MainThread As Thread
    Private ClosingForm As Boolean
    Private UpdateRunning As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MarketHistoryPG.Visible = False
        MainThread = New Thread(AddressOf UpdateHistory)

        RegionComboLoaded = False
        CancelHistoryImport = False
        ClosingForm = False
        UpdateRunning = False

        ' Load Default
        cmbRegions.Items.Add(UserApplicationSettings.MarketHistoryRegion)
        cmbRegions.Text = UserApplicationSettings.MarketHistoryRegion
        SelectedRegionID = GetRegionID(UserApplicationSettings.MarketHistoryRegion)
        Call UpdateLabelStats()

    End Sub

    Private Sub btnRefreshHistory_Click(sender As Object, e As EventArgs) Handles btnRefreshHistory.Click

        Try
            Dim AllItems As New List(Of Long)
            Dim rsItems As SQLiteDataReader

            btnRefreshHistory.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            ' Get all the items we can build with a blueprint and save them
            DBCommand = New SQLiteCommand("SELECT ITEM_ID FROM ALL_BLUEPRINTS WHERE MARKET_GROUP IS NOT NULL", EVEDB.DBREf)
            rsItems = DBCommand.ExecuteReader

            ' Build the list of items
            While rsItems.Read
                AllItems.Add(rsItems.GetInt64(0))
            End While
            rsItems.Close()

            ' Thread this out to the background
            MainThread = New Thread(AddressOf UpdateHistory)
            MainThread.IsBackground = True
            MainThread.Start(AllItems)
            UpdateRunning = True

            Do While MainThread.IsAlive
                If CancelHistoryImport Then
                    Call CancelUpdate()
                    GoTo ExitSub
                End If
                Application.DoEvents()
            Loop

            UpdateRunning = False

            ' Done, so update
            If Not CancelHistoryImport And Not ClosingForm Then
                Call UpdateLabelStats()
                Call MsgBox("All Market History for " & cmbRegions.Text & " Updated", vbInformation, Application.ProductName)
            End If

        Catch ex As Exception
            ' there was an error
            Call MsgBox("There was an error updating market history for " & cmbRegions.Text & ". Error: " & Err.Description, vbInformation, Application.ProductName)
        End Try

ExitSub:
        UpdateRunning = False
        btnRefreshHistory.Enabled = True
        Me.Cursor = Cursors.Default

    End Sub

    ' For threading the update process in background
    Private Sub UpdateHistory(AllItems As Object)
        Dim Items As List(Of Long) = DirectCast(AllItems, List(Of Long))
        Dim MH As New MarketPriceInterface(MarketHistoryPG)

        ' Set the global flag to prevent other updates during this
        RunningAllHistoryUpdate = True

        If MH.UpdateESIPriceHistory(Items, SelectedRegionID, True) Then
            ' Success
            Application.DoEvents()
        End If

        RunningAllHistoryUpdate = False

    End Sub

    ' Returns the date of the region selected in the combo
    Private Sub UpdateLabelStats()
        Dim rsData As SQLiteDataReader
        Dim CacheDate As Date = NoDate
        Dim RecordCount As Integer = 0
        Dim TotalRecords As Integer
        Dim SQL As String

        If UpdateRunning Then
            Exit Sub
        End If

        SQL = "SELECT CACHE_DATE, RECORD_COUNT FROM (SELECT CACHE_DATE, COUNT(*) AS RECORD_COUNT FROM MARKET_HISTORY_UPDATE_CACHE "
        SQL &= "WHERE REGION_ID = " & SelectedRegionID & " GROUP BY CACHE_DATE) ORDER BY CACHE_DATE DESC"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)

        rsData = DBCommand.ExecuteReader

        ' Get the first row for stats
        If rsData.HasRows() Then
            CacheDate = ProcessCacheDate(rsData)
            RecordCount = rsData.GetInt32(1)
        End If

        ' Get the total items we can update
        DBCommand = New SQLiteCommand("SELECT COUNT(ITEM_ID) FROM ALL_BLUEPRINTS WHERE MARKET_GROUP IS NOT NULL", EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        rsData.Read()
        TotalRecords = rsData.GetInt32(0)
        rsData.Close()

        If CacheDate = NoDate Then
            lblLastUpdateDate.Text = "No Records"
            lblItemsLastUpdated.Text = "None"
        Else
            lblLastUpdateDate.Text = CStr(CacheDate)
            lblItemsLastUpdated.Text = CStr(RecordCount) & " of " & CStr(TotalRecords)
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelHistoryImport = True
        MarketHistoryPG.Visible = False
        MarketHistoryPG.Value = 0
        btnRefreshHistory.Enabled = True
        Application.DoEvents()
    End Sub

    Private Sub CancelUpdate()
        If MainThread.IsAlive Then
            MainThread.Abort()
        End If
        If CancelHistoryImport Then
            Call MsgBox("Update Canceled", vbInformation, Application.ProductName)
        End If
        RunningAllHistoryUpdate = False
        CancelHistoryImport = False
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        ClosingForm = True
        Call CancelUpdate()
        Me.Hide()
    End Sub

    Private Sub frmRefreshMarketHistory_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ClosingForm = True
        Call CancelUpdate()
    End Sub

    Private Sub cmbRegions_DropDown(sender As Object, e As EventArgs) Handles cmbRegions.DropDown
        If Not RegionComboLoaded Then
            Call LoadRegionCombo(cmbRegions, UserApplicationSettings.MarketHistoryRegion)
            RegionComboLoaded = True
        End If
    End Sub

    Private Sub cmbRegions_LostFocus(sender As Object, e As EventArgs) Handles cmbRegions.LostFocus
        If Trim(cmbRegions.Text) = "" Then
            cmbRegions.Text = UserApplicationSettings.MarketHistoryRegion
        End If
        ' Save the region
        SaveRegionSetting()
    End Sub

    Private Sub cmbRegions_DropDownClosed(sender As Object, e As EventArgs) Handles cmbRegions.DropDownClosed
        If Trim(cmbRegions.Text) = "" Then
            cmbRegions.Text = UserApplicationSettings.MarketHistoryRegion
        End If
        ' Save the region
        SaveRegionSetting()
    End Sub

    Private Sub SaveRegionSetting()
        Dim PS As New ProgramSettings
        UserApplicationSettings.MarketHistoryRegion = cmbRegions.Text
        Call PS.SaveApplicationSettings(UserApplicationSettings)

        ' Reset every time they change this
        SelectedRegionID = GetRegionID(UserApplicationSettings.MarketHistoryRegion)

        ' Also update labels based on the new region
        Call UpdateLabelStats()
    End Sub

End Class