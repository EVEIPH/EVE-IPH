Imports System.Data.SQLite

Public Class frmESIStatus

    Private Sub RefreshGrid()
        Dim SQL As String
        Dim rsStatus As SQLiteDataReader
        Dim lstViewrow As ListViewItem
        Dim ScopesList As String()
        Dim ScopesSQL As String = ""
        Dim EndLoc As Integer = 0

        Dim StartLoc As Integer = 0
        Dim Length As Integer = 0

        lstStatus.Items.Clear()
        lstStatus.BeginUpdate()

        If SelectedCharacter.CharacterTokenData.Scopes = "No Scopes" Then
            lstStatus.EndUpdate()
            Exit Sub
        End If

        SQL = "SELECT scope, purpose, status FROM ESI_STATUS_ITEMS, ESI_ENDPOINT_ROUTE_TO_SCOPE WHERE route = endpoint_route "
        ScopesList = SelectedCharacter.CharacterTokenData.Scopes.Split(CChar(" "))

        For Each scope In ScopesList
            If InStr(scope, ".") <> 0 Then
                ScopesSQL &= scope.Substring(0, InStr(InStr(scope, ".") + 1, scope, ".") - 1) & "','"
            End If
        Next

        ScopesSQL = ScopesSQL.Substring(0, Len(ScopesSQL) - 3)

        SQL &= "AND scope IN ('" & ScopesSQL & "') "
        SQL &= "ORDER BY tag1"

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
                    lstViewrow.BackColor = Color.IndianRed
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