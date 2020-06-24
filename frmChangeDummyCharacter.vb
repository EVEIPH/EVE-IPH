Imports System.Data.SQLite

Public Class frmChangeDummyCharacter

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Call SaveName()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmChangeDummyCharacter_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim rsData As SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT CHARACTER_NAME FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = " & CStr(DummyCharacterID), EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader
        rsData.Read()

        lblCurrentName.Text = rsData.GetString(0)

        rsData.Close()

        Call txtName.Focus()

    End Sub

    Private Sub SaveName()

        If Trim(txtName.Text) = "" Then
            MsgBox("You cannot enter a blank name.", vbInformation, Application.ProductName)
            Exit Sub
        End If

        ' Just save what they enter and put a asterisk on the end to singify it's a dummy account
        Try
            Call EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET CHARACTER_NAME = '" & FormatDBString(Trim(txtName.Text)) & "*' WHERE CHARACTER_ID = " & CStr(DummyCharacterID))

            MsgBox("Dummy Character Name updated.", vbInformation, Application.ProductName)

        Catch ex As Exception
            MsgBox("Could not save name. Error: " & ex.Message)
        End Try

    End Sub

    Private Sub txtName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SaveName()
        End If
    End Sub
End Class