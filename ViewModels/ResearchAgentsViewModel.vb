Public Class ResearchAgentsViewModel

    Public Property Agents As List(Of ResearchAgentItemViewModel)
    Public Property TotalValue As Double

    Public Sub New()
        Agents = New List(Of ResearchAgentItemViewModel)
        TotalValue = 0
    End Sub

End Class

Public Class ResearchAgentItemViewModel

    Public Property AgentName As String
    Public Property Field As String
    Public Property CurrentResearchPoints As Double
    Public Property NumberOfCores As Long
    Public Property CurrentValue As Double
    Public Property ResearchPointsPerDay As Double
    Public Property AgentLevel As Integer
    Public Property Location As String

    Public Sub New()
        AgentName = ""
        Field = ""
        CurrentResearchPoints = 0
        NumberOfCores = 0
        CurrentValue = 0
        ResearchPointsPerDay = 0
        AgentLevel = 0
        Location = ""
    End Sub

End Class
