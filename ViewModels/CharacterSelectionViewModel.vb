Public Class CharacterSelectionViewModel

    Public Property AvailableCharacters As List(Of String)
    Public Property DefaultCharacterName As String
    Public Property SelectedCharacterName As String
    Public Property HasRealCharacters As Boolean
    Public Property RequiresDefaultCharacterSelection As Boolean
    Public Property IsDummyCharacterLoaded As Boolean

    Public Sub New()
        AvailableCharacters = New List(Of String)
        DefaultCharacterName = ""
        SelectedCharacterName = ""
        HasRealCharacters = False
        RequiresDefaultCharacterSelection = False
        IsDummyCharacterLoaded = False
    End Sub

End Class
