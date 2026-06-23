' Form just allows a user to select a default character

Imports System.Data.SQLite

Public Class frmSetCharacterDefault

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        CancelESISSOLogin = False

        ' Add any initialization after the InitializeComponent() call.
        Call UpdateCharacterList()

    End Sub

    ' Updates the character list with a default character
    Private Sub btnSelectDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDefault.Click
        Dim SelectedCharacterName As String = ""

        If chkListDefaultChar.CheckedItems.Count = 0 Then
            MsgBox("Please select a default character", vbExclamation, Application.ProductName)
            Exit Sub
        End If

        ' Should only be one checked
        For Each item In chkListDefaultChar.CheckedItems
            SelectedCharacterName = item.ToString
        Next

        Me.Cursor = Cursors.WaitCursor
        If CharacterSelectionService.SelectCharacterByName(SelectedCharacterName,
                                                           UserApplicationSettings.LoadAssetsonStartup,
                                                           UserApplicationSettings.LoadBPsonStartup,
                                                           False) Then
            If Application.OpenForms().OfType(Of frmMain).Any Then
                Call frmMain.ResetTabs()
                Call frmMain.ResetCharacterIDonFacilties()
            End If

            DefaultCharSelected = True
            MsgBox(SelectedCharacterName & " selected as Default Character", vbInformation, Application.ProductName)
        End If
        Me.Cursor = Cursors.Default
        Me.Close()

    End Sub

    Private Sub chkListDefaultChar_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListDefaultChar.ItemCheck
        Dim idx As Integer

        If (e.NewValue = CheckState.Checked) Then
            ' Uncheck all others not sent
            For idx = 0 To chkListDefaultChar.Items.Count - 1
                If idx <> e.Index Then
                    chkListDefaultChar.SetItemChecked(idx, False)
                End If
            Next
        End If

    End Sub

    ' Checks if the user selected a default or not. If not, verifies that they don't want to set a default and want to go with dummy
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        CancelESISSOLogin = True
        Me.Hide()

    End Sub

    Private Sub frmSetCharacterDefault_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    ' Update the list with the current loaded characters in the table
    Private Sub UpdateCharacterList()
        Dim selectionState = CharacterSelectionService.GetSelectionState()

        ' Load up the grid with characters on this computer
        DefaultCharSelected = False

        chkListDefaultChar.Items.Clear()

        For Each characterName In selectionState.AvailableCharacters
            chkListDefaultChar.Items.Add(characterName)
        Next

        If selectionState.DefaultCharacterName <> "" Then
            chkListDefaultChar.SetItemChecked(chkListDefaultChar.Items.IndexOf(selectionState.DefaultCharacterName), True)
        End If

        ' If only one character, then check it
        If selectionState.AvailableCharacters.Count = 1 Then
            chkListDefaultChar.SetItemChecked(0, True)
        End If

        If selectionState.HasRealCharacters Then
            btnSelectDefault.Enabled = True
        Else
            ' Disable select default button until they load one up
            btnSelectDefault.Enabled = False
        End If

    End Sub

    Protected Overrides Sub Finalize()
        CancelESISSOLogin = False ' Reset on close
        MyBase.Finalize()
    End Sub
End Class
