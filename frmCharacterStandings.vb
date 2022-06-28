
Imports System.Data.SQLite

Public Class frmCharacterStandings

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lstStandings.Columns.Add("NPC Type", 75, HorizontalAlignment.Left)
        lstStandings.Columns.Add("NPC Name", 184, HorizontalAlignment.Left)
        lstStandings.Columns.Add("Standing", 70, HorizontalAlignment.Right)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    ' Load the skills
    Private Sub frmCharacterStandings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Refresh()
        Application.DoEvents()

        lblCharacterName.Text = SelectedCharacter.Name

        Call LoadStandings()

    End Sub

    Private Sub LoadStandings()
        Dim SQL As String
        Dim readerStandings As SQLiteDataReader

        Dim lstViewRow As ListViewItem

        SQL = "SELECT NPC_TYPE, NPC_NAME, STANDING FROM CHARACTER_STANDINGS "
        SQL &= "WHERE CHARACTER_ID = " & SelectedCharacter.ID & " "
        If rbtnNegative.Checked Then
            SQL &= "AND STANDING < 0 "
        ElseIf rbtnPostive.Checked Then
            SQL &= "AND STANDING >= 0 "
        End If

        If rbtnSortName.Checked Then
            SQL &= "ORDER BY NPC_TYPE DESC, NPC_NAME"
        ElseIf rbtnSortStanding.Checked Then
            SQL &= "ORDER BY NPC_TYPE DESC, STANDING DESC"
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerStandings = DBCommand.ExecuteReader

        If readerStandings.HasRows Then
            lstStandings.BeginUpdate()
            lstStandings.Items.Clear()

            While readerStandings.Read
                Application.DoEvents()

                lstViewRow = New ListViewItem(readerStandings.GetString(0))
                'The remaining columns are subitems  
                lstViewRow.SubItems.Add(readerStandings.GetString(1))
                lstViewRow.SubItems.Add(FormatNumber(readerStandings.GetDouble(2), 2))
                Call lstStandings.Items.Add(lstViewRow)
            End While

            lstStandings.EndUpdate()
        End If

        readerStandings.Close()
        readerStandings = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub rbtnPostive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnPostive.CheckedChanged
        Call LoadStandings()
    End Sub

    Private Sub rbtnNegative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNegative.CheckedChanged
        Call LoadStandings()
    End Sub

    Private Sub rbtnBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBoth.CheckedChanged
        Call LoadStandings()
    End Sub

    Private Sub rbtnSortName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSortName.CheckedChanged
        Call LoadStandings()
    End Sub

    Private Sub rbtnSortStanding_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSortStanding.CheckedChanged
        Call LoadStandings()
    End Sub
End Class