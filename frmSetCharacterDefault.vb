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
        Dim i As Integer = 0
        Dim ErrorData As ErrObject = Nothing

        If chkListDefaultChar.CheckedItems.Count = 0 Then
            MsgBox("Please select a default character", vbExclamation, Application.ProductName)
            Exit Sub
        End If

        ' Should only be one checked
        For Each item In chkListDefaultChar.CheckedItems
            SelectedCharacterName = item.ToString
        Next

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        ' If we get here, just clear out the old default and set the new one
        Call LoadCharacter(SelectedCharacterName, False)
        ' Refresh all screens
        If Application.OpenForms().OfType(Of frmMain).Any Then
            Call frmMain.ResetTabs()
        End If

        DefaultCharSelected = True
        MsgBox(SelectedCharacterName & " selected as Default Character", vbInformation, Application.ProductName)

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

    Private Sub btnEVESSOLogin_Click(sender As Object, e As EventArgs) Handles btnEVESSOLogin.Click
        Dim ESIConnection As New ESI

        btnEVESSOLogin.Enabled = False ' Disable until we return

        ' Set the new character data first. This will load the data in the table or update it if they choose a character already loaded
        If ESIConnection.SetCharacterData() Then
            ' Refresh the token data to get new scopes if they added
            Me.Cursor = Cursors.WaitCursor
            If SelectedCharacter.ID <> DummyCharacterID And SelectedCharacter.ID > 0 Then
                Call SelectedCharacter.RefreshTokenData()
            End If
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            ' Now update the character list
            Call UpdateCharacterList()
        Else
            ' Didn't load, so show the re-enter info button
            If Not CancelESISSOLogin Then
                MsgBox("The Character failed to load. Please check application registration information.")
            End If
        End If

        btnEVESSOLogin.Enabled = True ' Enable for another try if they want

    End Sub

    ' Update the list with the current loaded characters in the table
    Private Sub UpdateCharacterList()
        Dim readerCharacters As SQLiteDataReader
        Dim SQL As String
        Dim numChars As Long
        Dim i As Integer = 0

        ' Load up the grid with characters on this computer
        DefaultCharSelected = False

        chkListDefaultChar.Items.Clear()

        SQL = "SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> {0}"

        DBCommand = New SQLiteCommand(String.Format(SQL, DummyCharacterID), EVEDB.DBREf)
        numChars = CLng(DBCommand.ExecuteScalar())

        SQL = "SELECT CHARACTER_NAME, IS_DEFAULT FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> {0}"

        DBCommand = New SQLiteCommand(String.Format(SQL, DummyCharacterID), EVEDB.DBREf)
        readerCharacters = DBCommand.ExecuteReader()

        While readerCharacters.Read()
            chkListDefaultChar.Items.Add(readerCharacters.GetString(0))
            ' If there is a default already, check it
            If CInt(readerCharacters.GetValue(1)) <> 0 Then
                chkListDefaultChar.SetItemChecked(i, True)
            End If
            i += 1
        End While

        ' If only one character, then check it
        If numChars = 1 Then
            chkListDefaultChar.SetItemChecked(0, True)
        End If

        If numChars >= 1 Then
            btnSelectDefault.Enabled = True
        Else
            ' Disable select default button until they load one up
            btnSelectDefault.Enabled = False
        End If

        readerCharacters.Close()
        readerCharacters = Nothing
        DBCommand = Nothing

    End Sub

End Class