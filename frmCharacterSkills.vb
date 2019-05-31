
Public Class frmCharacterSkills

    Private AllowSkillOverride As Boolean
    Private OverrideSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels) ' To save all the skills we are working with here
    Private UpdateSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels) ' The list of the ones we want to update only
    'Private SelectedNode As TreeNode
    Private SelectedSkill As EVESkill

    Private SkillOverridden As Boolean ' If they make a change, and switch back ask them if they want to lose their work
    Private CheckLoadsTree As Boolean ' If hitting the check box will load the skills into the tree
    Private FullOverrideChange As Boolean ' If they change from just override, to not override, need to track for saving
    Private CheckSaveBeforeExit As Boolean

#Region "Object Functions"

    Private Sub mnuOrigLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrigLevel.Click
        ' Removes the skill from override and sets it back to the real level
        Call UpdateSkill(-1, SkillTree.SelectedNode)
    End Sub

    Private Sub mnuLevel0_Click(sender As System.Object, e As System.EventArgs) Handles mnuLevel0.Click
        Call UpdateSkill(0, SkillTree.SelectedNode)
    End Sub

    Private Sub mnuLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLevel1.Click
        Call UpdateSkill(1, SkillTree.SelectedNode)
    End Sub

    Private Sub mnuLevel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLevel2.Click
        Call UpdateSkill(2, SkillTree.SelectedNode)
    End Sub

    Private Sub mnuLevel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLevel3.Click
        Call UpdateSkill(3, SkillTree.SelectedNode)
    End Sub

    Private Sub mnuLevel4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLevel4.Click
        Call UpdateSkill(4, SkillTree.SelectedNode)
    End Sub

    Private Sub mnuLevel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLevel5.Click
        Call UpdateSkill(5, SkillTree.SelectedNode)
    End Sub

    ' Sets the box check in the context menu
    Private Sub SetCheck(ByVal CheckIndex As Integer)
        Select Case CheckIndex
            Case 0
                mnuOrigLevel.Checked = True
                mnuLevel1.Checked = False
                mnuLevel2.Checked = False
                mnuLevel3.Checked = False
                mnuLevel4.Checked = False
                mnuLevel5.Checked = False
            Case 1
                mnuOrigLevel.Checked = False
                mnuLevel1.Checked = True
                mnuLevel2.Checked = False
                mnuLevel3.Checked = False
                mnuLevel4.Checked = False
                mnuLevel5.Checked = False
            Case 2
                mnuOrigLevel.Checked = False
                mnuLevel1.Checked = False
                mnuLevel2.Checked = True
                mnuLevel3.Checked = False
                mnuLevel4.Checked = False
                mnuLevel5.Checked = False
            Case 3
                mnuOrigLevel.Checked = False
                mnuLevel1.Checked = False
                mnuLevel2.Checked = False
                mnuLevel3.Checked = True
                mnuLevel4.Checked = False
                mnuLevel5.Checked = False
            Case 4
                mnuOrigLevel.Checked = False
                mnuLevel1.Checked = False
                mnuLevel2.Checked = False
                mnuLevel3.Checked = False
                mnuLevel4.Checked = True
                mnuLevel5.Checked = False
            Case 5
                mnuOrigLevel.Checked = False
                mnuLevel1.Checked = False
                mnuLevel2.Checked = False
                mnuLevel3.Checked = False
                mnuLevel4.Checked = False
                mnuLevel5.Checked = True
        End Select
    End Sub

    ' Selects the node selected if they choose a node and right click and shows the context menu
    Private Sub SkillTree_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SkillTree.MouseClick
        Dim p As Point
        Dim ClickedNode As TreeNode

        If e.Button = MouseButtons.Right And AllowSkillOverride Then

            ClickedNode = SkillTree.GetNodeAt(e.X, e.Y)

            If Not IsNothing(ClickedNode) Then
                SkillTree.SelectedNode = ClickedNode
            End If

            ' Only allow right click context on the skills
            If Not IsNothing(ClickedNode.Parent) Then ' if the node has no parent, we are at the top level
                p = New Point(e.X, e.Y)
                ' Get the skill name and look it up in the skills
                SelectedSkill = OverrideSkills.GetSkill(ClickedNode.Text.Substring(0, InStr(ClickedNode.Text, "-") - 2))

                If SelectedSkill.Overridden Then
                    ' Set the top level to the base skill level
                    mnuOrigLevel.Text = "Stored Level: " & CStr(SelectedSkill.OverriddenLevel)
                Else
                    mnuOrigLevel.Text = "Not Overridden"
                End If

                ' Set the menu checked based on skill level
                Call SetCheck(SelectedSkill.Level)

                ' Now show the context on the point
                contextOverride.Show(SkillTree, p)
            End If
        End If
    End Sub

#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AllowSkillOverride = UserApplicationSettings.AllowSkillOverride
        FullOverrideChange = UserApplicationSettings.AllowSkillOverride ' Save this for checking

    End Sub

    Private Sub frmCharacterSkills_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.Refresh()
        CheckLoadsTree = False
        chkSkillOverride.Checked = UserApplicationSettings.AllowSkillOverride
        CheckLoadsTree = True
        Call LoadSkillsInTree(AllowSkillOverride)
        CheckSaveBeforeExit = False
        SkillOverridden = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Dim Response As MsgBoxResult

        If CheckSaveBeforeExit Then
            ' Mark a skill as overridden
            If Not SkillOverridden Then
                SkillOverridden = True
            End If

            Response = CheckSave()

            If Response <> vbCancel Then
                Me.Hide()
            End If
        Else
            Me.Hide()
        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Call RefreshTree()
    End Sub

    Private Sub RefreshTree()
        ' Dim API As New EVEAPI
        Dim TempCharacter(0) As Character
        Dim Response As MsgBoxResult = Nothing
        Dim TempKeyType As String = ""

        ' Update the skills if they can be updated
        Cursor = Cursors.WaitCursor
        ' Just refresh the skills from API
        SelectedCharacter.Skills.LoadCharacterSkills(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, False, Trim(txtSkillNameFilter.Text))
        ' Refresh
        Call LoadSkillsInTree(chkSkillOverride.Checked)
        Cursor = Cursors.Default

        If SkillOverridden Then
            Response = CheckSave()
        End If

        ' If they didn't hit cancel, reload the skills
        If Response <> vbCancel Then
            Call LoadSkillsInTree(chkSkillOverride.Checked)
        End If

        ' If they are searching, then open all the nodes
        If Trim(txtSkillNameFilter.Text) <> "" Then
            Call SkillTree.ExpandAll()
        Else
            Call SkillTree.CollapseAll()
        End If

    End Sub

    ' Saves the skills that get overridden
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Call SaveSkills()
    End Sub

    Private Sub chkSkillOverride_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSkillOverride.CheckedChanged
        Call CheckOverRide()
        Call RefreshTree()
    End Sub

    Private Sub chkAllLevel5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllLevel5.CheckedChanged

        ' As long as All Skills 5 checked, you can't uncheck override
        If chkAllLevel5.Checked = True Then
            chkSkillOverride.Enabled = False
        Else
            chkSkillOverride.Enabled = True
        End If

        Application.DoEvents()

        If chkAllLevel5.Checked = True And chkSkillOverride.Checked = False Then
            chkSkillOverride.Checked = True
        Else
            Call CheckOverRide()
        End If
    End Sub

    ' Saves the skills that the user changed (overridden)
    Private Sub SaveSkills()

        ' Save the updated skills list
        SelectedCharacter.Skills.SaveOverRideSkills(UpdateSkills)
        SkillsUpdated = True

        Call FinalizeUpdate()

    End Sub

    ' Loads the tree with the skills
    Private Sub LoadSkillsInTree(ByVal LoadAllSkillsforOverride As Boolean)
        Dim TempSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)
        Dim SkillGroup As String = ""
        Dim CurrentNode As TreeNode = Nothing
        Dim CurrentSubNode As TreeNode = Nothing

        Dim TempSkillLevel As Integer

        Dim SearchText As String = UCase(Trim(txtSkillNameFilter.Text))

        Me.Cursor = Cursors.WaitCursor

        ' Reset these every new load
        OverrideSkills = New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)
        UpdateSkills = New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)

        lblCharacterName.Text = SelectedCharacter.Name

        ' If they want max alpha, load those skills into the selected character (dummy)

        ' Load whatever is in the database
        Call SelectedCharacter.Skills.LoadCharacterSkills(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, LoadAllSkillsforOverride)

        If Not IsNothing(SelectedCharacter.Skills) Then
            ' Fill the tree
            SkillTree.BeginUpdate()
            SkillTree.Nodes.Clear()

            For i = 0 To SelectedCharacter.Skills.NumSkills - 1
                With SelectedCharacter.Skills.GetSkillList(i)
                    ' Only add if the search bar is blank or the skills name is like the skill name
                    If SearchText = "" Or UCase(.Name).Contains(SearchText) Then
                        Application.DoEvents()
                        If SkillGroup <> .Group Then
                            ' Save the skill group
                            SkillGroup = SelectedCharacter.Skills.GetSkillList(i).Group
                            ' Add the group to the tree
                            CurrentNode = SkillTree.Nodes.Add(SkillGroup)
                        End If

                        ' Add the skill and level
                        If chkAllLevel5.Checked Then
                            TempSkillLevel = 5
                        Else
                            TempSkillLevel = .Level
                        End If

                        If TempSkillLevel <> 0 Or (TempSkillLevel = 0 And chkSkillOverride.Checked = True) Then
                            CurrentSubNode = CurrentNode.Nodes.Add(.Name & " - " & CStr(TempSkillLevel))
                        End If

                        ' Save the current skills
                        OverrideSkills.InsertSkill(.TypeID, .Level, .TrainedLevel, .ActiveLevel, 0, .Overridden, .OverriddenLevel)

                        ' Save the skill here if it's level 5
                        If chkAllLevel5.Checked Then
                            ' Set the selected skill since the user isn't clicking on these
                            SelectedSkill = OverrideSkills.GetSkill(.Name)
                            UpdateSkill(5, CurrentSubNode)
                            ' They just changed a skill
                            SkillOverridden = True
                        End If
                    End If
                End With
            Next

            SkillTree.EndUpdate()
        End If

        ' If they are searching, then open all the nodes
        If Trim(txtSkillNameFilter.Text) <> "" Then
            Call SkillTree.ExpandAll()
        Else
            Call SkillTree.CollapseAll()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    ' Updates the sent node and inserts the override skill into the array
    Private Sub UpdateSkill(ByVal Level As Integer, ByVal SentNode As TreeNode)
        Dim CurrentSkill As EVESkill

        ' If they selected the same skill level, just exit
        If Level = SelectedSkill.Level Then
            Exit Sub
        End If

        ' Get the user data if it exists
        CurrentSkill = SelectedCharacter.Skills.GetSkill(SelectedSkill.Name)

        ' Set the check to the level selected
        Call SetCheck(Level)

        ' Update the text in the node to the new level
        SentNode.Text = SentNode.Text.Substring(0, InStr(SentNode.Text, "-")) & " " & CStr(Level)

        If Level <> -1 Then
            ' Need to set the Top label to the original level for reference
            mnuOrigLevel.Text = "Stored Level: " & CStr(SelectedSkill.Level)
            SelectedSkill.Overridden = True
            SelectedSkill.Level = Level
            SelectedSkill.OverriddenLevel = Level
        Else
            ' Not overridden, so load in the original skill level from the character data
            mnuOrigLevel.Text = "Not Overridden"

            ' If it returned nothing, then set the skill level to 0 because there is no saved skill
            If CurrentSkill.TypeID = 0 Then
                SelectedSkill.OverriddenLevel = 0
                SelectedSkill.Level = 0
                SelectedSkill.Overridden = False
            Else
                ' Just save the temp skill data back
                SelectedSkill = CurrentSkill
            End If

        End If

        ' Now update the skill in the override list
        OverrideSkills.UpdateSkill(SelectedSkill)

        ' Save it in the update list for later as we would insert it into the DB (so swap overridden and skill level)
        With SelectedSkill
            UpdateSkills.InsertSkill(.TypeID, .Level, .TrainedLevel, .ActiveLevel, .SkillPoints, .Overridden, .OverriddenLevel)
        End With

        ' They just changed a skill
        SkillOverridden = True

    End Sub

    ' Checks to see if they overrided any skills
    Private Sub CheckOverRide()
        Dim Response As MsgBoxResult

        If ((FullOverrideChange <> chkSkillOverride.Checked) Or (FullOverrideChange <> chkAllLevel5.Checked)) And Not SkillOverridden Then
            ' They changed the original check for overriding skills but didn't update any skills
            CheckSaveBeforeExit = True
        End If

        If CheckLoadsTree Then
            If chkSkillOverride.Checked = True And chkAllLevel5.Checked = False Then
                ' They just want to override skills
                AllowSkillOverride = True
                Call LoadSkillsInTree(True)
            ElseIf chkAllLevel5.Checked = True Then
                ' They just set them all to level 5, but not new skills
                AllowSkillOverride = True ' But they are overridden
                Call LoadSkillsInTree(False)
            Else ' They want normal skills
                Response = CheckSave()

                ' If they didn't hit cancel, reload the skills
                If Not Response = vbCancel Then
                    AllowSkillOverride = False
                    Call LoadSkillsInTree(False)
                Else
                    ' Exit this sub and return to the screen and keep the check the same
                    CheckLoadsTree = False
                    chkSkillOverride.Checked = True
                    Exit Sub
                End If
            End If

            ' Reset this as they either save, disgard or they didn't check override
            SkillOverridden = False
        End If

        CheckLoadsTree = True
    End Sub

    ' Checks if we need to ask the user if we save their changes and returns true for move on, or false for staying there
    Private Function CheckSave() As MsgBoxResult

        If SkillOverridden Then
            ' See if they want to do this or not if they made changes and didn't save
            CheckSave = MsgBox("You have unsaved skill changes. Do you want to save?", DirectCast(MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question, MsgBoxStyle), Application.ProductName)

            If CheckSave = vbYes Then
                ' Save the updated skills
                Call SaveSkills()
            End If
        Else
            CheckSave = vbYes
        End If

    End Function

    Public Sub FinalizeUpdate()

        ' Saved skills, so this is now the default
        FullOverrideChange = chkSkillOverride.Checked
        CheckSaveBeforeExit = False
        SkillOverridden = False ' Reset change flag

        Call LoadSkillsInTree(chkSkillOverride.Checked)

        ' Finally reload the skills for the program - whatever changes were done just load them from DB
        Call SelectedCharacter.Skills.LoadCharacterSkills(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, False)

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnClearItemFilter_Click(sender As System.Object, e As System.EventArgs) Handles btnClearItemFilter.Click
        txtSkillNameFilter.Text = ""
        Call RefreshTree()
    End Sub

    Private Sub txtSkillNameFilter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSkillNameFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call RefreshTree()
        End If
    End Sub

End Class