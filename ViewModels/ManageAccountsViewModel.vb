Public Class ManageAccountsViewModel

    Public Property Accounts As List(Of ManageAccountItemViewModel)
    Public Property CanSelectDefaultCharacter As Boolean

    Public Sub New()
        Accounts = New List(Of ManageAccountItemViewModel)
        CanSelectDefaultCharacter = False
    End Sub

End Class

Public Class ManageAccountItemViewModel

    Public Property CharacterID As Long
    Public Property CharacterName As String
    Public Property CorporationID As Long
    Public Property CorporationName As String
    Public Property IsDefault As Boolean
    Public Property Scopes As String
    Public Property AccessToken As String
    Public Property RefreshToken As String
    Public Property AccessTokenExpiration As Date
    Public Property TokenType As String
    Public Property IsDirector As Boolean
    Public Property IsFactoryManager As Boolean

    Public Sub New()
        CharacterID = 0
        CharacterName = ""
        CorporationID = 0
        CorporationName = ""
        IsDefault = False
        Scopes = ""
        AccessToken = ""
        RefreshToken = ""
        AccessTokenExpiration = NoDate
        TokenType = ""
        IsDirector = False
        IsFactoryManager = False
    End Sub

End Class

Public Class ManageAccountDeleteResult

    Public Property RequiresDefaultCharacterSelection As Boolean

    Public Sub New()
        RequiresDefaultCharacterSelection = False
    End Sub

End Class
