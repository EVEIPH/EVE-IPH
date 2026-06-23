Imports System.Data.SQLite

Public NotInheritable Class ManageAccountsService

    Private Sub New()
    End Sub

    Public Shared Function LoadAccounts() As ManageAccountsViewModel
        Dim viewModel As New ManageAccountsViewModel
        Dim readerAccounts As SQLiteDataReader
        Dim sql As String

        sql = "SELECT ECHD.CHARACTER_ID, ECHD.CHARACTER_NAME, ECRPD.CORPORATION_NAME, ECHD.IS_DEFAULT, " &
              "ECHD.SCOPES, ECHD.ACCESS_TOKEN, ECHD.ACCESS_TOKEN_EXPIRE_DATE_TIME, ECHD.TOKEN_TYPE, " &
              "ECHD.REFRESH_TOKEN, ECHD.CORPORATION_ID, " &
              "CASE WHEN EXISTS (SELECT 1 FROM ESI_CORPORATION_ROLES AS ECR " &
              "                  WHERE ECR.CHARACTER_ID = ECHD.CHARACTER_ID " &
              "                  AND ECR.CORPORATION_ID = ECHD.CORPORATION_ID " &
              "                  AND ECR.ROLE = 'Director') THEN 1 ELSE 0 END AS IS_DIRECTOR, " &
              "CASE WHEN EXISTS (SELECT 1 FROM ESI_CORPORATION_ROLES AS ECR " &
              "                  WHERE ECR.CHARACTER_ID = ECHD.CHARACTER_ID " &
              "                  AND ECR.CORPORATION_ID = ECHD.CORPORATION_ID " &
              "                  AND ECR.ROLE = 'Factory_Manager') THEN 1 ELSE 0 END AS IS_FACTORY_MANAGER " &
              "FROM ESI_CHARACTER_DATA AS ECHD " &
              "INNER JOIN ESI_CORPORATION_DATA AS ECRPD ON ECHD.CORPORATION_ID = ECRPD.CORPORATION_ID " &
              "WHERE ECHD.CHARACTER_ID <> " & CStr(DummyCharacterID) & " " &
              "ORDER BY ECHD.CHARACTER_NAME"

        DBCommand = New SQLiteCommand(sql, EVEDB.DBREf)
        readerAccounts = DBCommand.ExecuteReader

        While readerAccounts.Read()
            Dim account As New ManageAccountItemViewModel

            account.CharacterID = readerAccounts.GetInt64(0)
            account.CharacterName = readerAccounts.GetString(1)
            account.CorporationName = readerAccounts.GetString(2)
            account.IsDefault = (readerAccounts.GetInt32(3) <> 0)
            account.Scopes = readerAccounts.GetString(4)
            account.AccessToken = readerAccounts.GetString(5)
            account.AccessTokenExpiration = CDate(readerAccounts.GetString(6))
            account.TokenType = readerAccounts.GetString(7)
            account.RefreshToken = readerAccounts.GetString(8)
            account.CorporationID = readerAccounts.GetInt64(9)
            account.IsDirector = (readerAccounts.GetInt32(10) <> 0)
            account.IsFactoryManager = (readerAccounts.GetInt32(11) <> 0)

            viewModel.Accounts.Add(account)
        End While

        readerAccounts.Close()

        viewModel.CanSelectDefaultCharacter = (GetCharacterCount() <> 0)

        Return viewModel
    End Function

    Public Shared Function DeleteCharacter(ByVal account As ManageAccountItemViewModel,
                                           ByVal refreshAssets As Boolean,
                                           ByVal refreshBlueprints As Boolean) As ManageAccountDeleteResult
        Dim deleteResult As New ManageAccountDeleteResult

        Try
            Call EVEDB.BeginSQLiteTransaction()

            Call DeleteCharacterData(account.CharacterID)
            Call DeleteCorporationDataIfUnused(account.CorporationID)
            EVEDB.ExecuteNonQuerySQL("DELETE FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = " & CStr(account.CharacterID))
            Call EnsureDummyCharacterDefaultIfRequired()

            Call EVEDB.CommitSQLiteTransaction()
        Catch
            If EVEDB.TransactionActive() Then
                Call EVEDB.RollbackSQLiteTransaction()
            End If

            Throw
        End Try

        Dim selectionState = CharacterSelectionService.LoadCurrentCharacter(refreshAssets, refreshBlueprints)
        deleteResult.RequiresDefaultCharacterSelection = selectionState.RequiresDefaultCharacterSelection

        Return deleteResult
    End Function

    Public Shared Function RefreshCharacterToken(ByVal account As ManageAccountItemViewModel) As Boolean
        Dim characterTokenData As SavedTokenData = BuildSavedTokenData(account)
        Dim esiData As New ESI

        If Not esiData.SetCharacterData(True, characterTokenData, "", True) Then
            Return False
        End If

        Call SelectedCharacter.LoadCharacterData(characterTokenData, False, False)

        Return True
    End Function

    Private Shared Function GetCharacterCount() As Integer
        DBCommand = New SQLiteCommand("SELECT COUNT(*) FROM ESI_CHARACTER_DATA", EVEDB.DBREf)
        Return CInt(DBCommand.ExecuteScalar())
    End Function

    Private Shared Sub DeleteCharacterData(ByVal characterID As Long)
        EVEDB.ExecuteNonQuerySQL("DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & CStr(characterID))
        EVEDB.ExecuteNonQuerySQL("DELETE FROM CHARACTER_STANDINGS WHERE CHARACTER_ID = " & CStr(characterID))
        EVEDB.ExecuteNonQuerySQL("DELETE FROM CURRENT_RESEARCH_AGENTS WHERE CHARACTER_ID = " & CStr(characterID))
        EVEDB.ExecuteNonQuerySQL("DELETE FROM ASSETS WHERE ID = " & CStr(characterID))
        EVEDB.ExecuteNonQuerySQL("DELETE FROM INDUSTRY_JOBS WHERE installerID = " & CStr(characterID))
        EVEDB.ExecuteNonQuerySQL("DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID = " & CStr(characterID))
        EVEDB.ExecuteNonQuerySQL("DELETE FROM ESI_CORPORATION_ROLES WHERE CHARACTER_ID = " & CStr(characterID))
    End Sub

    Private Shared Sub DeleteCorporationDataIfUnused(ByVal corporationID As Long)
        Dim sql As String

        sql = "SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE CORPORATION_ID = " & CStr(corporationID)
        DBCommand = New SQLiteCommand(sql, EVEDB.DBREf)

        If CInt(DBCommand.ExecuteScalar()) = 1 Then
            EVEDB.ExecuteNonQuerySQL("DELETE FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & CStr(corporationID))
            EVEDB.ExecuteNonQuerySQL("DELETE FROM ESI_CORPORATION_DIVISIONS WHERE CORPORATION_ID = " & CStr(corporationID))
        End If
    End Sub

    Private Shared Sub EnsureDummyCharacterDefaultIfRequired()
        Dim sql As String

        sql = "SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> " & CStr(DummyCharacterID)
        DBCommand = New SQLiteCommand(sql, EVEDB.DBREf)

        If CInt(DBCommand.ExecuteScalar()) = 0 Then
            EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = " & CStr(DefaultCharacterCode) &
                                     " WHERE CHARACTER_ID = " & CStr(DummyCharacterID))
        End If
    End Sub

    Private Shared Function BuildSavedTokenData(ByVal account As ManageAccountItemViewModel) As SavedTokenData
        Dim characterTokenData As New SavedTokenData

        characterTokenData.CharacterID = account.CharacterID
        characterTokenData.Scopes = account.Scopes
        characterTokenData.AccessToken = account.AccessToken
        characterTokenData.TokenExpiration = account.AccessTokenExpiration
        characterTokenData.TokenType = account.TokenType
        characterTokenData.RefreshToken = account.RefreshToken

        Return characterTokenData
    End Function

End Class
