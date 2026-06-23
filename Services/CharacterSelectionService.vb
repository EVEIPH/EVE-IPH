Imports System.Data.SQLite

Public NotInheritable Class CharacterSelectionService

    Private Sub New()
    End Sub

    Public Shared Function LoadCurrentCharacter(ByVal refreshAssets As Boolean, ByVal refreshBPs As Boolean) As CharacterSelectionViewModel
        Dim selectionState As CharacterSelectionViewModel

        If SelectedCharacter.LoadDefaultCharacter(refreshBPs, refreshAssets) Then
            selectionState = GetSelectionState()
            selectionState.SelectedCharacterName = SelectedCharacter.Name
            selectionState.IsDummyCharacterLoaded = (SelectedCharacter.ID = DummyCharacterID)
            Return selectionState
        End If

        selectionState = GetSelectionState()

        If Not selectionState.HasRealCharacters Then
            Call SelectedCharacter.LoadDummyCharacter(True)
            selectionState = GetSelectionState()
            selectionState.SelectedCharacterName = SelectedCharacter.Name
            selectionState.IsDummyCharacterLoaded = True
        Else
            selectionState.RequiresDefaultCharacterSelection = True
        End If

        Return selectionState
    End Function

    Public Shared Function SelectCharacterByName(ByVal characterName As String,
                                                 ByVal refreshAssets As Boolean,
                                                 ByVal refreshBPs As Boolean,
                                                 Optional ByVal playSound As Boolean = True) As Boolean

        If SelectedCharacter.Name = characterName Then
            Return False
        End If

        EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = 0")
        EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = " & CStr(DefaultCharacterCode) &
                                 " WHERE CHARACTER_NAME = '" & FormatDBString(characterName) & "'")

        Call SelectedCharacter.LoadDefaultCharacter(refreshAssets, refreshBPs)

        If playSound Then
            Call PlayNotifySound()
        End If

        Return True
    End Function

    Public Shared Function GetSelectionState() As CharacterSelectionViewModel
        Dim selectionState As New CharacterSelectionViewModel
        Dim readerCharacters As SQLiteDataReader
        Dim sql As String

        sql = "SELECT CHARACTER_NAME, IS_DEFAULT FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> {0} ORDER BY CHARACTER_NAME"
        DBCommand = New SQLiteCommand(String.Format(sql, DummyCharacterID), EVEDB.DBREf)
        readerCharacters = DBCommand.ExecuteReader()

        While readerCharacters.Read()
            Dim characterName As String = readerCharacters.GetString(0)
            selectionState.AvailableCharacters.Add(characterName)

            If CInt(readerCharacters.GetValue(1)) <> 0 Then
                selectionState.DefaultCharacterName = characterName
            End If
        End While

        readerCharacters.Close()

        selectionState.HasRealCharacters = (selectionState.AvailableCharacters.Count > 0)
        selectionState.RequiresDefaultCharacterSelection = selectionState.HasRealCharacters AndAlso selectionState.DefaultCharacterName = ""

        If Not IsNothing(SelectedCharacter) Then
            selectionState.SelectedCharacterName = SelectedCharacter.Name
            selectionState.IsDummyCharacterLoaded = (SelectedCharacter.ID = DummyCharacterID)
        End If

        Return selectionState
    End Function

End Class
