
Imports System.Data.SQLite

Public Class frmResearchAgents

    Private ListColumnClicked As Integer
    Private ListColumnSortOrder As SortOrder

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lstAgents.Columns.Add("Agent", 130, HorizontalAlignment.Left)
        lstAgents.Columns.Add("Field", 150, HorizontalAlignment.Left)
        lstAgents.Columns.Add("Current RP", 80, HorizontalAlignment.Right)
        lstAgents.Columns.Add("Number of Cores", 95, HorizontalAlignment.Right)
        lstAgents.Columns.Add("Current Value", 90, HorizontalAlignment.Right)
        lstAgents.Columns.Add("RP/Day", 60, HorizontalAlignment.Right)
        lstAgents.Columns.Add("Level", 50, HorizontalAlignment.Center)
        lstAgents.Columns.Add("Location", 265, HorizontalAlignment.Left)

        ListColumnClicked = 0
        ListColumnSortOrder = SortOrder.None

    End Sub

    Private Sub frmResearchAgents_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        ' Reload the agents and update from API if necessary
        Call SelectedCharacter.GetResearchAgents.LoadResearchAgents(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData)

        Call LoadGrid()

    End Sub

    ' Sort columns
    Private Sub lstAgents_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstAgents.ColumnClick
        Call ListViewColumnSorter(e.Column, lstAgents, ListColumnClicked, ListColumnSortOrder)
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub LoadGrid()
        Dim lstViewRow As ListViewItem
        Dim fAccessError As New frmAPIError

        Dim readerPriceLookup As SQLiteDataReader
        Dim SQL As String
        Dim CurrentValue As Double
        Dim CurrentNumberofCores As Long
        Dim TotalValue As Double = 0

        lstAgents.Items.Clear()

        Application.UseWaitCursor = True

        lstAgents.BeginUpdate()

        With SelectedCharacter.GetResearchAgents
            For i = 0 To .GetResearchAgents.Count - 1
                ' Get the total value of the datacores if I were to cash them in today - Price minus the DataCoreRedeemCost
                If .GetResearchAgents(i).Field.Contains("Gallente Starship") Then
                    SQL = "SELECT PRICE FROM ITEM_PRICES WHERE ITEM_NAME ='Datacore - Gallentean Starship Engineering'"
                ElseIf .GetResearchAgents(i).Field.Contains("Amarr Starship") Then
                    SQL = "SELECT PRICE FROM ITEM_PRICES WHERE ITEM_NAME ='Datacore - Amarian Starship Engineering'"
                Else
                    SQL = "SELECT PRICE FROM ITEM_PRICES WHERE ITEM_NAME ='Datacore - " & .GetResearchAgents(i).Field & "'"
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerPriceLookup = DBCommand.ExecuteReader()

                If readerPriceLookup.Read() Then
                    ' Get the number of cores we would get, minus the redeem cost from each 
                    CurrentNumberofCores = CLng(Math.Floor(.GetResearchAgents(i).CurrentRP / 100))
                    CurrentValue = Math.Floor(.GetResearchAgents(i).CurrentRP / 100) * (readerPriceLookup.GetDouble(0) - DataCoreRedeemCost)
                Else
                    CurrentNumberofCores = 0
                    CurrentValue = 0
                End If

                ' Load the current data
                lstViewRow = lstAgents.Items.Add(.GetResearchAgents(i).Agent) ' Agent Name
                'The remaining columns are subitems  
                lstViewRow.SubItems.Add(.GetResearchAgents(i).Field) ' Field
                lstViewRow.SubItems.Add(FormatNumber(.GetResearchAgents(i).CurrentRP, 2)) ' Current RP
                lstViewRow.SubItems.Add(FormatNumber(CurrentNumberofCores, 0)) ' Current number of cores
                lstViewRow.SubItems.Add(FormatNumber(CurrentValue, 2)) ' Current Value
                lstViewRow.SubItems.Add(FormatNumber(.GetResearchAgents(i).RPperDay, 2)) ' RP/Day
                lstViewRow.SubItems.Add(CStr(.GetResearchAgents(i).AgentLevel)) ' Level
                lstViewRow.SubItems.Add(.GetResearchAgents(i).Location) ' Location
                TotalValue = TotalValue + CurrentValue
            Next
        End With

        ' Set total isk
        lblTotalDCValue.Text = FormatNumber(TotalValue, 2) & " ISK"

        ' Make sure the refresh button is enabled
        btnRefresh.Enabled = True
        lstAgents.EndUpdate()
        Application.UseWaitCursor = False

    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Application.UseWaitCursor = True

        ' Reload the agents and update from API if necessary
        Call SelectedCharacter.GetResearchAgents.LoadResearchAgents(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData)

        ' Refresh the data
        Call LoadGrid()

        Application.UseWaitCursor = False
        MsgBox("Records Refreshed", vbInformation, Application.ProductName)

    End Sub

End Class
