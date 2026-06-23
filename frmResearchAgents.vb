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
        Call RefreshResearchAgentsGrid()
    End Sub

    ' Sort columns
    Private Sub lstAgents_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstAgents.ColumnClick
        Call ListViewColumnSorter(e.Column, lstAgents, ListColumnClicked, ListColumnSortOrder)
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub RefreshResearchAgentsGrid()
        Application.UseWaitCursor = True
        Call CharacterDataService.RefreshResearchAgents(SelectedCharacter)
        Call RenderGrid(ResearchAgentsService.BuildViewModel(SelectedCharacter))
        Application.UseWaitCursor = False
    End Sub

    Private Sub RenderGrid(ByVal viewModel As ResearchAgentsViewModel)
        Dim lstViewRow As ListViewItem

        lstAgents.Items.Clear()
        lstAgents.BeginUpdate()

        For Each agent In viewModel.Agents
            lstViewRow = lstAgents.Items.Add(agent.AgentName)
            lstViewRow.SubItems.Add(agent.Field)
            lstViewRow.SubItems.Add(FormatNumber(agent.CurrentResearchPoints, 2))
            lstViewRow.SubItems.Add(FormatNumber(agent.NumberOfCores, 0))
            lstViewRow.SubItems.Add(FormatNumber(agent.CurrentValue, 2))
            lstViewRow.SubItems.Add(FormatNumber(agent.ResearchPointsPerDay, 2))
            lstViewRow.SubItems.Add(CStr(agent.AgentLevel))
            lstViewRow.SubItems.Add(agent.Location)
        Next

        lblTotalDCValue.Text = FormatNumber(viewModel.TotalValue, 2) & " ISK"
        btnRefresh.Enabled = True
        lstAgents.EndUpdate()
    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Call RefreshResearchAgentsGrid()
        MsgBox("Records Refreshed", vbInformation, Application.ProductName)
    End Sub

End Class
