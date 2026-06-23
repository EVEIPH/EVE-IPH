Imports System.Data.SQLite

Public NotInheritable Class ResearchAgentsService

    Private Sub New()
    End Sub

    Public Shared Function BuildViewModel(ByVal character As Character) As ResearchAgentsViewModel
        Dim viewModel As New ResearchAgentsViewModel
        Dim datacorePrices As Dictionary(Of String, Double) = LoadDatacorePrices()

        If IsNothing(character) OrElse IsNothing(character.GetResearchAgents()) Then
            Return viewModel
        End If

        For Each agent In character.GetResearchAgents().GetResearchAgents()
            Dim agentViewModel As New ResearchAgentItemViewModel
            Dim datacoreName As String = GetDatacoreItemName(agent.Field)
            Dim datacorePrice As Double = 0

            agentViewModel.AgentName = agent.Agent
            agentViewModel.Field = agent.Field
            agentViewModel.CurrentResearchPoints = agent.CurrentRP
            agentViewModel.ResearchPointsPerDay = agent.RPperDay
            agentViewModel.AgentLevel = agent.AgentLevel
            agentViewModel.Location = agent.Location
            agentViewModel.NumberOfCores = CLng(Math.Floor(agent.CurrentRP / 100))

            If datacorePrices.ContainsKey(datacoreName) Then
                datacorePrice = datacorePrices(datacoreName)
                agentViewModel.CurrentValue = agentViewModel.NumberOfCores * (datacorePrice - DataCoreRedeemCost)
            End If

            viewModel.Agents.Add(agentViewModel)
            viewModel.TotalValue += agentViewModel.CurrentValue
        Next

        Return viewModel
    End Function

    Private Shared Function LoadDatacorePrices() As Dictionary(Of String, Double)
        Dim datacorePrices As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
        Dim readerPrices As SQLiteDataReader
        Dim sql As String = "SELECT ITEM_NAME, PRICE FROM ITEM_PRICES WHERE ITEM_NAME LIKE 'Datacore - %'"

        DBCommand = New SQLiteCommand(sql, EVEDB.DBREf)
        readerPrices = DBCommand.ExecuteReader()

        While readerPrices.Read()
            datacorePrices(readerPrices.GetString(0)) = readerPrices.GetDouble(1)
        End While

        readerPrices.Close()

        Return datacorePrices
    End Function

    Private Shared Function GetDatacoreItemName(ByVal field As String) As String
        If field.Contains("Gallente Starship") Then
            Return "Datacore - Gallentean Starship Engineering"
        ElseIf field.Contains("Amarr Starship") Then
            Return "Datacore - Amarian Starship Engineering"
        Else
            Return "Datacore - " & field
        End If
    End Function

End Class
