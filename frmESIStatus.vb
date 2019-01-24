Imports System.Data.SQLite

Public Class frmESIStatus

    Private Sub RefreshGrid()
        Dim SQL As String
        Dim rsStatus As SQLiteDataReader
        Dim lstViewrow As ListViewItem

        lstStatus.Items.Clear()
        lstStatus.BeginUpdate()

        SQL = "SELECT scope, purpose, status FROM ESI_STATUS_ITEMS, ESI_ENDPOINT_ROUTE_TO_SCOPE WHERE route = endpoint_route ORDER BY SCOPE"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsStatus = DBCommand.ExecuteReader

        While rsStatus.Read
            lstViewRow = New ListViewItem(rsStatus.GetString(0))
            lstViewrow.SubItems.Add(rsStatus.GetString(1))
            Select Case rsStatus.GetString(2)
                Case "green"
                    lstViewrow.SubItems.Add("Good")
                    lstViewrow.BackColor = Color.LightGreen
                Case "yellow"
                    lstViewrow.SubItems.Add("Degraded")
                    lstViewrow.BackColor = Color.Yellow
                Case Else
                    lstViewrow.SubItems.Add("Down")
                    lstViewrow.BackColor = Color.Red
            End Select

            Call lstStatus.Items.Add(lstViewrow)
            Application.DoEvents()
        End While

        lstStatus.EndUpdate()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ESIData As New ESI

        Application.UseWaitCursor = True
        Application.DoEvents()
        Call ESIData.GetESIStatus()
        Call RefreshGrid()
        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

    Private Sub frmESIStatus_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.UseWaitCursor = True
        Application.DoEvents()
        Call RefreshGrid()
        Application.UseWaitCursor = False
        Application.DoEvents()
    End Sub

End Class