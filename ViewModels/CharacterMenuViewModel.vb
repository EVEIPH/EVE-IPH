Public Class CharacterMenuViewModel

    Public Property LoadedCharacterText As String
    Public Property Characters As List(Of CharacterMenuItemViewModel)

    Public Sub New()
        LoadedCharacterText = "Character Loaded:"
        Characters = New List(Of CharacterMenuItemViewModel)
    End Sub

End Class

Public Class CharacterMenuItemViewModel

    Public Property CharacterName As String
    Public Property Gender As String

    Public Sub New()
        CharacterName = ""
        Gender = Male
    End Sub

End Class
