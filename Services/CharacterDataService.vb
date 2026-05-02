Public NotInheritable Class CharacterDataService

    Private Sub New()
    End Sub

    Public Shared Function RefreshAssets(ByVal character As Character, ByVal refreshAssetsFlag As Boolean) As Boolean
        If IsNothing(character) OrElse character.ID = DummyCharacterID Then
            Return False
        End If

        character.GetAssets.LoadAssets(character.ID, character.CharacterTokenData, refreshAssetsFlag)

        If Not IsNothing(character.CharacterCorporation) AndAlso character.CharacterCorporation.CorporationID <> 0 Then
            character.CharacterCorporation.GetAssets.LoadAssets(character.CharacterCorporation.CorporationID, character.CharacterTokenData, refreshAssetsFlag)
        End If

        Return True
    End Function

    Public Shared Sub RefreshAssets(ByVal character As Character, ByVal scanType As ScanType, ByVal refreshAssetsFlag As Boolean)
        If IsNothing(character) Then
            Exit Sub
        End If

        If scanType = ScanType.Personal Then
            character.GetAssets.LoadAssets(character.ID, character.CharacterTokenData, refreshAssetsFlag)
        ElseIf Not IsNothing(character.CharacterCorporation) Then
            character.CharacterCorporation.GetAssets.LoadAssets(character.CharacterCorporation.CorporationID, character.CharacterTokenData, refreshAssetsFlag)
        End If
    End Sub

    Public Shared Function RefreshBlueprints(ByVal character As Character, ByVal refreshBlueprintsFlag As Boolean) As Boolean
        If IsNothing(character) OrElse character.ID = DummyCharacterID Then
            Return False
        End If

        character.GetBlueprints.LoadBlueprints(character.ID, character.CharacterTokenData, refreshBlueprintsFlag)

        If Not IsNothing(character.CharacterCorporation) AndAlso character.CharacterCorporation.CorporationID <> 0 Then
            character.CharacterCorporation.GetBlueprints.LoadBlueprints(character.CharacterCorporation.CorporationID, character.CharacterTokenData, refreshBlueprintsFlag)
        End If

        Return True
    End Function

    Public Shared Sub RefreshBlueprints(ByVal character As Character, ByVal scanType As ScanType, ByVal refreshBlueprintsFlag As Boolean)
        If IsNothing(character) Then
            Exit Sub
        End If

        If scanType = ScanType.Personal Then
            character.GetBlueprints.LoadBlueprints(character.ID, character.CharacterTokenData, refreshBlueprintsFlag)
        ElseIf Not IsNothing(character.CharacterCorporation) Then
            character.CharacterCorporation.GetBlueprints.LoadBlueprints(character.CharacterCorporation.CorporationID, character.CharacterTokenData, refreshBlueprintsFlag)
        End If
    End Sub

    Public Shared Function RefreshResearchAgents(ByVal character As Character) As Boolean
        If IsNothing(character) OrElse character.ID = DummyCharacterID Then
            Return False
        End If

        character.GetResearchAgents.LoadResearchAgents(character.ID, character.CharacterTokenData)
        Return True
    End Function

    Public Shared Sub RefreshPersonalAssets(ByVal characterID As Long, ByVal tokenData As SavedTokenData, ByVal refreshAssets As Boolean)
        If characterID <= 0 OrElse IsNothing(tokenData) Then
            Exit Sub
        End If

        Dim assets As New EVEAssets(ScanType.Personal)
        assets.LoadAssets(characterID, tokenData, refreshAssets)
    End Sub

    Public Shared Sub RefreshCorporationAssets(ByVal corporationID As Long, ByVal tokenData As SavedTokenData, ByVal refreshAssets As Boolean)
        If corporationID <= 0 OrElse IsNothing(tokenData) Then
            Exit Sub
        End If

        Dim assets As New EVEAssets(ScanType.Corporation)
        assets.LoadAssets(corporationID, tokenData, refreshAssets)
    End Sub

    Public Shared Sub RefreshPersonalBlueprints(ByVal characterID As Long, ByVal tokenData As SavedTokenData, ByVal refreshBlueprints As Boolean)
        If characterID <= 0 OrElse IsNothing(tokenData) Then
            Exit Sub
        End If

        Dim blueprints As New EVEBlueprints(ScanType.Personal)
        blueprints.LoadBlueprints(characterID, tokenData, refreshBlueprints)
    End Sub

    Public Shared Sub RefreshCorporationBlueprints(ByVal corporationID As Long, ByVal tokenData As SavedTokenData, ByVal refreshBlueprints As Boolean)
        If corporationID <= 0 OrElse IsNothing(tokenData) Then
            Exit Sub
        End If

        Dim blueprints As New EVEBlueprints(ScanType.Corporation)
        blueprints.LoadBlueprints(corporationID, tokenData, refreshBlueprints)
    End Sub

End Class
