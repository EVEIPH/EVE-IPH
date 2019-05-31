
Imports System.Data.SQLite

Public Class frmViewSavedStructures

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Total is 660, minus 25 for check, and 21 for scroll = 614
        lstStructures.Columns(0).Width = -2
        lstStructures.Columns(1).Width = 100
        lstStructures.Columns(2).Width = 195
        lstStructures.Columns(3).Width = 90
        lstStructures.Columns(4).Width = 90
        lstStructures.Columns(5).Width = 139

    End Sub

    ' Load up the structures
    Private Sub frmViewSavedStructures_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.UseWaitCursor = True
        Me.Enabled = False
        btnExit.Enabled = False
        btnRemove.Enabled = False
        lstStructures.Enabled = False
        Application.DoEvents()

        Call LoadStructureGrid()

        btnExit.Enabled = True
        btnRemove.Enabled = True
        lstStructures.Enabled = True
        Me.Enabled = True
        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

    Public Sub LoadStructureGrid()
        Dim SQL As String
        Dim rsList As SQLiteDataReader
        Dim ESIData As New ESI
        Dim lstViewRow As New ListViewItem

        SQL = "SELECT STATION_ID, STATION_NAME, solarSystemName, regionName FROM STATIONS, SOLAR_SYSTEMS, REGIONS "
        SQL &= "WHERE STATIONS.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND STATIONS.REGION_ID = REGIONS.regionID AND MANUAL_ENTRY <> 0 "
        SQL &= "ORDER BY regionName, solarSystemName, STATION_NAME"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsList = DBCommand.ExecuteReader

        lstStructures.Items.Clear()
        lstStructures.BeginUpdate()

        While rsList.Read
            lstViewRow = New ListViewItem("") ' Check
            lstViewRow.SubItems.Add(CStr(rsList.GetInt64(0))) 'ID
            lstViewRow.SubItems.Add(rsList.GetString(1)) 'Name
            lstViewRow.SubItems.Add(rsList.GetString(2)) 'System
            lstViewRow.SubItems.Add(rsList.GetString(3)) 'Region

            ' For each one, look up the market status and set
            If ESIData.CheckStructureMarketData(rsList.GetInt64(0), SelectedCharacter.CharacterTokenData, True) Then
                lstViewRow.SubItems.Add("OK") ' Status
            Else
                lstViewRow.SubItems.Add("Market Access Denied") ' Status
            End If

            Call lstStructures.Items.Add(lstViewRow)
            Application.DoEvents()

        End While

        lstStructures.EndUpdate()

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim checkeditems As ListView.CheckedListViewItemCollection
        Dim SQL As String = "DELETE FROM STATIONS WHERE STATION_ID = {0}"

        checkeditems = lstStructures.CheckedItems

        For i = 0 To checkeditems.Count - 1
            EVEDB.ExecuteNonQuerySQL(String.Format(SQL, (checkeditems(i).SubItems(1).Text)))
        Next

        Call LoadStructureGrid()

        MsgBox("Selected Structures removed.", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Hide()
    End Sub

End Class