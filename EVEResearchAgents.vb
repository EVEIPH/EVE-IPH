
Imports System.Data.SQLite

Public Class EVEResearchAgents

    Private AgentList As List(Of ResearchAgent)

    Public Sub New()

        AgentList = New List(Of ResearchAgent)

    End Sub

    Public Sub LoadResearchAgents(ByVal CharacterID As Long, ByVal TokenData As SavedTokenData)
        Dim readerResearch As SQLiteDataReader
        Dim SQL As String
        Dim TempAgent As ResearchAgent
        Dim ResearchStartDate As Date
        Dim Difference As TimeSpan
        Dim DateDifference As Double

        ' Update them first
        Call UpdateResearchAgents(CharacterID, TokenData)

        ' Load the data from the DB
        SQL = "SELECT AGENT_NAME, typeName AS RESEARCH_FIELD, RP_PER_DAY, LEVEL, STATION, RESEARCH_START_DATE, REMAINDER_POINTS "
        SQL &= "FROM RESEARCH_AGENTS, INVENTORY_TYPES, CURRENT_RESEARCH_AGENTS  "
        SQL &= "WHERE CURRENT_RESEARCH_AGENTS.AGENT_ID = RESEARCH_AGENTS.AGENT_ID "
        SQL &= "AND CURRENT_RESEARCH_AGENTS.SKILL_TYPE_ID= INVENTORY_TYPES.typeID "
        SQL &= "AND CHARACTER_ID = " & CStr(CharacterID) & " "
        SQL &= "GROUP BY AGENT_NAME, typeName, RP_PER_DAY, LEVEL, STATION, RESEARCH_START_DATE, REMAINDER_POINTS "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerResearch = DBCommand.ExecuteReader

        ' New list
        AgentList = New List(Of ResearchAgent)

        While readerResearch.Read()
            TempAgent.Agent = readerResearch.GetString(0)
            TempAgent.Field = readerResearch.GetString(1)

            TempAgent.AgentLevel = readerResearch.GetInt32(3)
            TempAgent.Location = readerResearch.GetString(4)

            TempAgent.RPperDay = readerResearch.GetDouble(2)

            ResearchStartDate = Date.ParseExact(readerResearch.GetString(5), SQLiteDateFormat, LocalCulture)
            Difference = Now.Subtract(ResearchStartDate)
            DateDifference = Difference.Days + (Difference.Hours / 24) + (Difference.Seconds / (24 * 60))

            ' Calculate the current rps - Diff of now from start date + remainder points
            TempAgent.CurrentRP = CDbl(readerResearch.GetDouble(2)) * DateDifference + CDbl(readerResearch.GetDouble(6))

            ' Add the agent to the list
            AgentList.Add(TempAgent)

        End While

        readerResearch.Close()

    End Sub

    Private Sub UpdateResearchAgents(ByVal CharacterID As Long, ByVal CharacterTokenData As SavedTokenData)
        ' Refresh the data from the API
        Dim SQL As String = ""
        Dim CurrentAgents As List(Of ESIResearchAgent) = Nothing

        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Get the current list of agents updated
        If CB.DataUpdateable(CacheDateType.ResearchAgents, CharacterID) Then
            CurrentAgents = ESIData.GetCurrentResearchAgents(CharacterID, CharacterTokenData, CacheDate)

            If Not IsNothing(CurrentAgents) Then
                If CurrentAgents.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Delete all the current records and refresh them
                    SQL = "DELETE FROM CURRENT_RESEARCH_AGENTS WHERE CHARACTER_ID = " & CStr(CharacterID)
                    Call EVEDB.ExecuteNonQuerySQL(SQL)


                    ' Insert new data
                    For i = 0 To CurrentAgents.Count - 1
                        With CurrentAgents(i)
                            SQL = "INSERT INTO CURRENT_RESEARCH_AGENTS (AGENT_ID, SKILL_TYPE_ID, "
                            SQL &= "RP_PER_DAY, RESEARCH_START_DATE, REMAINDER_POINTS, CHARACTER_ID) VALUES "
                            SQL &= "(" & CStr(.agent_id) & "," & CStr(.skill_type_id) & "," & CStr(.points_per_day) & ",'"
                            SQL &= Format(CDate(CurrentAgents(i).started_at.Replace("T", " ").Replace("Z", "")), SQLiteDateFormat) & "',"
                            SQL &= CStr(.remainder_points) & "," & CStr(CharacterID) & ")"
                        End With

                        EVEDB.ExecuteNonQuerySQL(SQL)
                    Next

                    Call EVEDB.CommitSQLiteTransaction()
                End If
                ' All set, update cache date before leaving
                Call CB.UpdateCacheDate(CacheDateType.ResearchAgents, CacheDate, CharacterID)
            End If
        End If

    End Sub

    Public Function GetResearchAgents() As List(Of ResearchAgent)
        Return AgentList
    End Function

End Class

Public Structure ResearchAgent
    Public Agent As String
    Public Field As String
    Public CurrentRP As Double
    Public RPperDay As Double
    Public AgentLevel As Integer
    Public Location As String
End Structure