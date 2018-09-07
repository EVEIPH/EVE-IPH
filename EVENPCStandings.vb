
Imports System.Data.SQLite

Public Class EVENPCStandings

    Private NPCStandings As List(Of NPCStanding)
    Private StandingToFind As NPCStanding

    Private IDtoFind As Long

    Public Class NPCStanding
        Public NPCID As Long
        Public NPCType As String ' Agent, Faction, or Corporation
        Public NPCName As String
        Public Standing As Double ' Will be a base standing (no connections skill applied)
    End Class

    Public Sub New()
        NPCStandings = New List(Of NPCStanding)
    End Sub

    ' Returns the standing of a sent NPC ID
    Public Function GetStanding(ByVal NPCID As Long) As Double

        ' Find the Industry Skill level
        If NPCStandings.Count <> 0 Then
            For i = 0 To NPCStandings.Count - 1
                If NPCStandings(i).NPCID = NPCID Then
                    Return NPCStandings(i).Standing
                    Exit Function
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    ' Returns the standing of a sent name
    Public Function GetStanding(ByVal NPCName As String) As Double

        ' Find the Industry Skill level
        If NPCStandings.Count <> 0 Then
            For i = 0 To NPCStandings.Count - 1
                If NPCStandings(i).NPCName = NPCName Then
                    Return NPCStandings(i).Standing
                    Exit Function
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    ' Returns the effective standing of a sent NPC ID
    Public Function GetEffectiveStanding(ByVal NPCID As Long, SentConnections As Integer, SentDiplomacy As Integer) As Double

        ' Find the Industry Skill level
        If NPCStandings.Count <> 0 Then
            For i = 0 To NPCStandings.Count - 1
                If NPCStandings(i).NPCID = NPCID Then
                    Return CalcEffectiveStanding(NPCStandings(i).Standing, SentConnections, SentDiplomacy)
                    Exit Function
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    ' Returns the effective standing of a sent name
    Public Function GetEffectiveStanding(ByVal NPCName As String, SentConnections As Integer, SentDiplomacy As Integer) As Double

        ' Find the Industry Skill level
        If NPCStandings.Count <> 0 Then
            For i = 0 To NPCStandings.Count - 1
                If NPCStandings(i).NPCName = NPCName Then
                    Return CalcEffectiveStanding(NPCStandings(i).Standing, SentConnections, SentDiplomacy)
                    Exit Function
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    Private Function CalcEffectiveStanding(BaseCorpStanding As Double, Connections As Integer, Diplomacy As Integer) As Double
        Dim EffectiveStanding As Double

        If BaseCorpStanding < 0 Then
            ' Use Diplomacy
            EffectiveStanding = BaseCorpStanding + ((10 - BaseCorpStanding) * (0.04 * Diplomacy))
        ElseIf BaseCorpStanding > 0 Then
            ' Use connections
            EffectiveStanding = BaseCorpStanding + ((10 - BaseCorpStanding) * (0.04 * Connections))
        Else
            EffectiveStanding = 0
        End If

        Return EffectiveStanding

    End Function

    ' Returns the list of standings
    Public Function GetStandingsList() As List(Of NPCStanding)
        Return NPCStandings
    End Function

    ' Returns the number of standings in the list
    Public Function NumStandings() As Integer
        If NPCStandings.Count <> 0 Then
            Return NPCStandings.Count
        Else
            Return 0
        End If
    End Function

    ' Inserts standing with each value
    Public Sub InsertStanding(ByVal sentNPCID As Long, ByVal sentNPCType As String, ByVal sentNPCName As String, ByVal sentStanding As Double)
        Dim TempStanding As New NPCStanding

        TempStanding.NPCID = sentNPCID
        TempStanding.NPCName = sentNPCName
        TempStanding.NPCType = sentNPCType
        TempStanding.Standing = sentStanding

        Call InsertStanding(TempStanding)

    End Sub

    ' Inserts a set of character skills into the current set
    Public Sub InsertStanding(ByVal SentStanding As NPCStanding)
        Dim FoundStanding As NPCStanding
        Dim i As Integer = 0

        ' See if the skill exists already
        StandingToFind = SentStanding
        FoundStanding = NPCStandings.Find(AddressOf FindStanding)

        If FoundStanding IsNot Nothing Then
            Exit Sub
        Else ' add standing
            NPCStandings.Add(SentStanding)
        End If

    End Sub

    ' Predicate for finding the standing
    Private Function FindStanding(ByVal SStanding As NPCStanding) As Boolean

        If SStanding.NPCID = StandingToFind.NPCID And SStanding.NPCType = StandingToFind.NPCType And SStanding.NPCName = StandingToFind.NPCName Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Updates and Loads the character's standings from DB
    Public Sub LoadCharacterStandings(ByVal CharacterID As Long, ByVal CharacterTokenData As SavedTokenData)
        Dim SQL As String
        Dim readerStandings As SQLiteDataReader
        Dim Tempstandings As New EVENPCStandings

        ' Don't try to update/load dummy standings
        If CharacterID = 0 Then
            Exit Sub
        End If

        ' First update the standings
        Call UpdateCharacterStandings(CharacterID, CharacterTokenData)

        ' Load the standings
        SQL = "SELECT NPC_TYPE_ID, NPC_TYPE, NPC_NAME, STANDING FROM CHARACTER_STANDINGS WHERE CHARACTER_ID=" & CharacterID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerStandings = DBCommand.ExecuteReader

        While readerStandings.Read
            ' Insert standing
            InsertStanding(readerStandings.GetInt64(0), readerStandings.GetString(1), readerStandings.GetString(2), readerStandings.GetDouble(3))
        End While

        readerStandings.Close()
        DBCommand = Nothing
        readerStandings = Nothing

    End Sub

    ' Updates the Character Standings from ESI for the sent character and inserts them into the Database for later queries
    Private Sub UpdateCharacterStandings(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData)
        Dim SQL As String
        Dim i As Integer
        Dim TempStandings As EVENPCStandings = Nothing
        Dim NonFactionIDs As New List(Of Long)
        Dim ReturnNameData As New List(Of ESINameData)
        Dim ReturnFactionData As New List(Of ESIFactionData)
        Dim TempStanding As NPCStanding

        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Get updated standings
        If CB.DataUpdateable(CacheDateType.Standings, ID) Then
            TempStandings = ESIData.GetCharacterStandings(ID, CharacterTokenData, CacheDate)

            If Not IsNothing(TempStandings) Then
                If TempStandings.GetStandingsList.Count > 0 Then
                    ' Get all the standing names for corps and agents first
                    For Each entry In TempStandings.NPCStandings
                        If entry.NPCType <> "Faction" Then
                            NonFactionIDs.Add(entry.NPCID)
                        End If
                    Next

                    ' Get the faction names
                    ReturnFactionData = ESIData.GetFactionData()

                    For Each Record In ReturnFactionData
                        ' Update the Standings list with name
                        IDtoFind = Record.faction_id
                        TempStanding = TempStandings.NPCStandings.Find(AddressOf FindNPCID)
                        If Not IsNothing(TempStanding) Then
                            Call TempStandings.NPCStandings.Remove(TempStanding)
                            TempStanding.NPCName = Record.name
                            Call TempStandings.NPCStandings.Add(TempStanding)
                        End If
                    Next

                    ' Get the corp and agent names
                    ReturnNameData = ESIData.GetNameData(NonFactionIDs)

                    If Not IsNothing(ReturnNameData) Then
                        For Each Record In ReturnNameData
                            ' Update the Standings list with name
                            IDtoFind = Record.id
                            TempStanding = TempStandings.NPCStandings.Find(AddressOf FindNPCID)
                            If Not IsNothing(TempStanding) Then
                                Call TempStandings.NPCStandings.Remove(TempStanding)
                                TempStanding.NPCName = Record.name
                                Call TempStandings.NPCStandings.Add(TempStanding)
                            End If
                        Next
                    End If

                    Call EVEDB.BeginSQLiteTransaction()

                    ' Delete the old standings data
                    SQL = "DELETE FROM CHARACTER_STANDINGS WHERE CHARACTER_ID = " & ID
                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    ' Insert new standings data
                    For i = 0 To TempStandings.NumStandings - 1
                        SQL = "INSERT INTO CHARACTER_STANDINGS (CHARACTER_ID, NPC_TYPE_ID, NPC_TYPE, NPC_NAME, STANDING) "
                        SQL = SQL & " VALUES (" & ID & "," & TempStandings.GetStandingsList(i).NPCID
                        SQL = SQL & ",'" & TempStandings.GetStandingsList(i).NPCType
                        SQL = SQL & "','" & FormatDBString(TempStandings.GetStandingsList(i).NPCName)
                        SQL = SQL & "'," & TempStandings.GetStandingsList(i).Standing & ")"
                        Call EVEDB.ExecuteNonQuerySQL(SQL)
                    Next

                    DBCommand = Nothing

                    Call EVEDB.CommitSQLiteTransaction()

                End If
                ' Update cache date now that it's all set
                Call CB.UpdateCacheDate(CacheDateType.Standings, CacheDate, ID)
            End If
        End If
    End Sub

    ' Predicate for finding an npc record
    Private Function FindNPCID(ByVal Item As NPCStanding) As Boolean
        If Item.NPCID = IDtoFind Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Predicate for finding an npc record
    Private Function FindFactionID(ByVal Item As ESIFactionData) As Boolean
        If Item.faction_id = IDtoFind Then
            Return True
        Else
            Return False
        End If
    End Function

End Class

