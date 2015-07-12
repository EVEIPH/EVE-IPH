' Form just allows a user to select a default character

Imports System.Data.SQLite

Public Class frmSetCharacterDefault

    Public Sub New()

        Dim readerCharacters As SQLiteDataReader

        Dim SQL As String
        Dim numChars As Long
        Dim i As Integer = 0

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Load up the grid with characters on this computer
        CharactersLoaded = False
        DefaultCharSelected = False

        SQL = "SELECT COUNT(*) FROM API WHERE CHARACTER_NAME <> 'None' AND API_TYPE NOT IN ('Corporation', 'Old Key')"

        DBCommand = New SQLiteCommand(SQL, DB)
        numChars = CLng(DBCommand.ExecuteScalar())

        SQL = "SELECT CHARACTER_NAME, IS_DEFAULT FROM API WHERE CHARACTER_NAME <> 'None' AND API_TYPE NOT IN ('Corporation', 'Old Key')"

        DBCommand = New SQLiteCommand(SQL, DB)
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

        readerCharacters.Close()
        readerCharacters = Nothing
        DBCommand = Nothing

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
        ' Update them all to 0 first
        Call ExecuteNonQuerySQL("UPDATE API SET IS_DEFAULT = 0 WHERE API_TYPE <> 'Corporation'")
        Call ExecuteNonQuerySQL("UPDATE API SET IS_DEFAULT = -1 WHERE CHARACTER_NAME = '" & FormatDBString(SelectedCharacterName) & "' AND API_TYPE NOT IN ('Corporation', 'Old Key')")

        ' Load the character as default for program and reload additional API data
        Call SelectedCharacter.LoadDefaultCharacter(True, UserApplicationSettings.LoadAssetsonStartup, UserApplicationSettings.LoadBPsonStartup)

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
        Dim Response As MsgBoxResult
        Dim NoCharacter As Boolean

        ' Only ask if they want to cancel if there isn't a character loaded yet
        If SelectedCharacter.ID = 0 Then
            NoCharacter = True
        Else
            NoCharacter = False
        End If

        If NoCharacter Then
            Response = MsgBox("Are you sure you do not want to select a default character?", vbYesNo, Application.ProductName)

            If Response = vbYes Then
                ' Load the dummy
                Call SelectedCharacter.LoadDummyCharacter()
                Call MsgBox("Dummy Character Loaded", MsgBoxStyle.OkOnly, Application.ProductName)
                Me.Hide()
            End If
        Else
            Me.Hide()
        End If
    End Sub

    Private Sub frmSetCharacterDefault_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

End Class