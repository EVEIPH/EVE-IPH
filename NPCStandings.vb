
Public Class NPCStandings

    Private NPCStandings As List(Of NPCStanding)
    Private StandingToFind As NPCStanding

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

        Call InsertSTanding(TempStanding)

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

End Class

