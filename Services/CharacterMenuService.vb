Imports System.Data.SQLite

Public NotInheritable Class CharacterMenuService

    Private Sub New()
    End Sub

    Public Shared Function BuildCharacterMenu() As CharacterMenuViewModel
        Dim menuState As New CharacterMenuViewModel
        Dim readerCharacters As SQLiteDataReader
        Dim sql As String

        If Not IsNothing(SelectedCharacter) AndAlso SelectedCharacter.Name <> "" Then
            menuState.LoadedCharacterText = "Character Loaded: " & SelectedCharacter.Name
        End If

        sql = "SELECT CHARACTER_NAME, CASE WHEN GENDER IS NULL THEN 'male' ELSE GENDER END AS GENDER " &
              "FROM ESI_CHARACTER_DATA ORDER BY CHARACTER_NAME"

        DBCommand = New SQLiteCommand(sql, EVEDB.DBREf)
        readerCharacters = DBCommand.ExecuteReader

        While readerCharacters.Read()
            Dim menuItem As New CharacterMenuItemViewModel
            menuItem.CharacterName = readerCharacters.GetString(0)
            menuItem.Gender = readerCharacters.GetString(1)
            menuState.Characters.Add(menuItem)
        End While

        readerCharacters.Close()

        Return menuState
    End Function

End Class
