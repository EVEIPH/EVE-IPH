
Imports System.Data.SQLite

Public Class ResearchAgents

    Private AgentList As List(Of ResearchAgent)
    Private KeyData As APIKeyData
    Private CacheDate As Date

    Public Sub New(Optional ByVal SentKey As APIKeyData = Nothing)

        AgentList = New List(Of ResearchAgent)

        KeyData = SentKey
        CacheDate = NoDate

    End Sub

    Public Sub LoadResearchAgents(UpdatefromAPI As Boolean)
        Dim readerResearch As SQLiteDataReader
        Dim SQL As String
        Dim TempAgent As ResearchAgent
        Dim ResearchStartDate As Date
        Dim Difference As TimeSpan
        Dim DateDifference As Double

        If Not KeyData.Access Then
            ' No access to research agents in API, so leave
            Exit Sub
        End If

        ' Update them first
        Call UpdateResearchAgents(UpdatefromAPI)

        ' Load the data from the DB
        SQL = "SELECT AGENT_NAME, typeName AS RESEARCH_FIELD, RP_PER_DAY, LEVEL, STATION, RESEARCH_START_DATE, REMAINDER_POINTS "
        SQL = SQL & "FROM RESEARCH_AGENTS, INVENTORY_TYPES, CURRENT_RESEARCH_AGENTS  "
        SQL = SQL & "WHERE CURRENT_RESEARCH_AGENTS.AGENT_ID = RESEARCH_AGENTS.AGENT_ID "
        SQL = SQL & "AND CURRENT_RESEARCH_AGENTS.SKILL_TYPE_ID= INVENTORY_TYPES.typeID "
        SQL = SQL & "AND CHARACTER_ID = " & KeyData.ID & " "
        SQL = SQL & "GROUP BY AGENT_NAME, typeName, RP_PER_DAY, LEVEL, STATION, RESEARCH_START_DATE, REMAINDER_POINTS "

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

            ResearchStartDate = DateTime.ParseExact(readerResearch.GetString(5), SQLiteDateFormat, LocalCulture)
            Difference = Now.Subtract(ResearchStartDate)
            DateDifference = Difference.Days + (Difference.Hours / 24) + (Difference.Seconds / (24 * 60))

            ' Calculate the current rps - Diff of now from start date + remainder points
            TempAgent.CurrentRP = CDbl(readerResearch.GetDouble(2)) * DateDifference + CDbl(readerResearch.GetDouble(6))

            ' Add the agent to the list
            AgentList.Add(TempAgent)

        End While

        readerResearch.Close()
        DBCommand = Nothing
        readerResearch = Nothing

    End Sub

    Private Sub UpdateResearchAgents(UpdateAPI As Boolean)
        ' Refresh the data from the API
        Dim API As New EVEAPI
        Dim SQL As String
        Dim readerResearch As SQLiteDataReader
        Dim CurrentAgents As List(Of CurrentResearchAgent)
        Dim RefreshDate As Date ' To check the update of the API.

        ' See if we are doing an API update 
        If Not UpdateAPI Then
            Exit Sub
        End If

        ' First see if we can update yet (Cached for 60 minutes)
        SQL = "SELECT RESEARCH_AGENTS_CACHED_UNTIL FROM API WHERE CHARACTER_ID = " & KeyData.ID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerResearch = DBCommand.ExecuteReader

        If readerResearch.Read Then
            If Not IsDBNull(readerResearch.GetValue(0)) Then
                If readerResearch.GetString(0) = "" Then
                    RefreshDate = NoDate
                Else
                    RefreshDate = CDate(readerResearch.GetString(0))
                End If
            Else
                RefreshDate = NoDate
            End If
        Else
            RefreshDate = NoDate
        End If

        ' See if we refresh the data 
        If RefreshDate <= DateTime.UtcNow Then
            readerResearch.Close()

            Call EVEDB.BeginSQLiteTransaction()

            ' Delete all the current records and refresh them
            SQL = "DELETE FROM CURRENT_RESEARCH_AGENTS WHERE CHARACTER_ID = " & KeyData.ID
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            CurrentAgents = API.GetCurrentResearchAgents(KeyData, CacheDate)

            If Not NoAPIError(API.GetErrorText, "Character") Then
                ' Errored, exit
                Call EVEDB.RollbackSQLiteTransaction()
                Exit Sub
            End If

            ' Insert new data
            For i = 0 To CurrentAgents.Count - 1
                With CurrentAgents(i)
                    SQL = "INSERT INTO CURRENT_RESEARCH_AGENTS (AGENT_ID, SKILL_TYPE_ID, "
                    SQL = SQL & "RP_PER_DAY, RESEARCH_START_DATE, REMAINDER_POINTS, CHARACTER_ID) VALUES "
                    SQL = SQL & "(" & CStr(.agentID) & "," & CStr(.skillTypeID) & "," & CStr(.pointsPerDay) & ",'" & Format(.researchStartDate, SQLiteDateFormat) & "',"
                    SQL = SQL & CStr(.remainderPoints) & "," & CStr(KeyData.ID) & ")"
                End With

                evedb.ExecuteNonQuerySQL(SQL)
            Next

            ' Update the cache date
            SQL = "UPDATE API SET RESEARCH_AGENTS_CACHED_UNTIL = '" & Format(CacheDate, SQLiteDateFormat) & "' "
            SQL = SQL & "WHERE CHARACTER_ID=" & KeyData.ID & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            evedb.ExecuteNonQuerySQL(SQL)

            Call EVEDB.CommitSQLiteTransaction()

        End If

    End Sub

    Public Function GetResearchAgents() As List(Of ResearchAgent)
        Return AgentList
    End Function

    ReadOnly Property CachedUntil() As Date
        Get
            Return CacheDate
        End Get
    End Property

End Class

Public Structure ResearchAgent

    Public Agent As String
    Public Field As String
    Public CurrentRP As Double
    Public RPperDay As Double
    Public AgentLevel As Integer
    Public Location As String

End Structure