
Public Class frmReqSkills

    Private SkillTypeDisplay As SkillType

    Public Sub New(ByVal AllBPSkills As SkillType)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SkillTypeDisplay = AllBPSkills

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Hide()
    End Sub

    ' Show the skills required in red if they don't have them, black if they do
    Private Sub frmReqSkills_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim SkillGroup As String = ""
        Dim CurrentNode As TreeNode = Nothing
        Dim CurrentSubNode As TreeNode = Nothing
        Dim DisplaySkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)
        Dim TempReqComponentSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)

        If IsNothing(SelectedBlueprint) Then
            SkillTree.Nodes.Clear()
            Exit Sub
        End If

        If SelectedBlueprint.GetReqBPSkills.NumSkills = 0 And SkillTypeDisplay = SkillType.BPReqSkills Then
            SkillTree.Nodes.Clear()
            Exit Sub
        End If

        If SelectedBlueprint.GetReqComponentSkills.NumSkills = 0 And SkillTypeDisplay = SkillType.BPComponentSkills Then
            SkillTree.Nodes.Clear()
            Exit Sub
        End If

        If SelectedBlueprint.GetReqInventionSkills.NumSkills = 0 And SkillTypeDisplay = SkillType.InventionReqSkills Then
            SkillTree.Nodes.Clear()
            Exit Sub
        End If

        ' Fill the tree
        SkillTree.BeginUpdate()
        SkillTree.Nodes.Clear()

        Select Case SkillTypeDisplay
            Case SkillType.BPReqSkills
                CurrentNode = SkillTree.Nodes.Add("BP Required Manufacturing Skills")
                DisplaySkills = SelectedBlueprint.GetReqBPSkills
            Case SkillType.BPComponentSkills
                ' When there are no components for the object, show the base skills
                If IsNothing(SelectedBlueprint.GetReqComponentSkills.GetSkillList) Then
                    CurrentNode = SkillTree.Nodes.Add("BP Required Manufacturing Skills")
                    DisplaySkills = SelectedBlueprint.GetReqBPSkills
                Else
                    CurrentNode = SkillTree.Nodes.Add("Components Required Manufacturing Skills")
                    DisplaySkills = SelectedBlueprint.GetReqComponentSkills
                End If
            Case SkillType.InventionReqSkills, SkillType.REReqSkills
                CurrentNode = SkillTree.Nodes.Add("Required Invention / RE Skills")
                DisplaySkills = SelectedBlueprint.GetReqInventionSkills
        End Select

        ' Add the nodes
        If DisplaySkills.NumSkills <> 0 Then
            Call AddReqSkillNodes(CurrentNode, DisplaySkills)
        Else
            CurrentNode = CurrentNode.Nodes.Add("No Skills Required")
        End If

        SkillTree.EndUpdate()
        SkillTree.ExpandAll()
        SkillTree.SelectedNode = SkillTree.Nodes(0)

    End Sub

    Private Sub AddReqSkillNodes(ByRef SentSubNode As TreeNode, ByVal PreReqSkills As EVESkillList)
        Dim PreReqNode As TreeNode = Nothing
        Dim SkillList As List(Of EVESkill)
        Dim TempSkillLevel As Integer

        If PreReqSkills.NumSkills = 0 Then
            Exit Sub
        End If

        SkillList = PreReqSkills.GetSkillList

        ' Loop through each skill and add to tree
        For i = 0 To SkillList.Count - 1
            TempSkillLevel = SelectedCharacter.Skills.GetSkillLevel(SkillList(i).TypeID)

            If TempSkillLevel < SkillList(i).Level Then
                PreReqNode = SentSubNode.Nodes.Add(SkillList(i).Name & " - " & CStr(SkillList(i).Level) & " (" & TempSkillLevel & ")")
                PreReqNode.ForeColor = Color.Red
            Else
                PreReqNode = SentSubNode.Nodes.Add(SkillList(i).Name & " - " & CStr(SkillList(i).Level))
                PreReqNode.ForeColor = Color.Black
            End If

            ' If this node has skills, add them too
            If SkillList(i).PreReqSkills.NumSkills <> 0 Then
                Call AddReqSkillNodes(PreReqNode, SkillList(i).PreReqSkills)
            End If

        Next

    End Sub

End Class