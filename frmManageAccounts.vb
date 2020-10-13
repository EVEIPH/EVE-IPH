Imports System.Data.SQLite

Public Class frmManageAccounts

    Private ListColumnClicked As Integer
    Private ListColumnSortOrder As SortOrder

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ListColumnClicked = 0
        ListColumnSortOrder = SortOrder.None

        btnRefreshToken.Enabled = False

    End Sub

    Private Sub lstAccounts_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstAccounts.ColumnClick
        Call ListViewColumnSorter(e.Column, lstAccounts, ListColumnClicked, ListColumnSortOrder)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub frmManageAccounts_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Call LoadAccountGrid()
    End Sub

    Private Sub LoadAccountGrid()
        Dim rsAccounts As SQLiteDataReader
        Dim SQL As String
        Dim lstViewRow As ListViewItem
        Dim CharList As String = ""

        ' Until there is a key able to set a default to, don't enable the select default button
        btnSelectDefaultChar.Enabled = False

        Application.UseWaitCursor = True

        SQL = "SELECT CHARACTER_ID, CHARACTER_NAME, CORPORATION_NAME, IS_DEFAULT, SCOPES, ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, TOKEN_TYPE, REFRESH_TOKEN "
        SQL &= "FROM ESI_CHARACTER_DATA AS ECHD, ESI_CORPORATION_DATA AS ECRPD "
        SQL &= "WHERE ECHD.CORPORATION_ID = ECRPD.CORPORATION_ID "
        SQL &= "AND CHARACTER_ID <> " & CStr(DummyCharacterID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsAccounts = DBCommand.ExecuteReader

        lstAccounts.Items.Clear()
        lstAccounts.BeginUpdate()

        While rsAccounts.Read

            ' Insert into table
            lstViewRow = New ListViewItem(CStr(rsAccounts.GetInt32(0))) ' CHAR ID (Hidden)
            'The remaining columns are subitems  
            lstViewRow.SubItems.Add(rsAccounts.GetString(1)) ' NAME
            lstViewRow.SubItems.Add(rsAccounts.GetString(2)) ' CORP NAME

            If rsAccounts.GetInt32(3) <> 0 Then
                lstViewRow.SubItems.Add("True")
            Else
                lstViewRow.SubItems.Add("False")
            End If

            lstViewRow.SubItems.Add(rsAccounts.GetString(4)) ' SCOPES (Hidden)
            lstViewRow.SubItems.Add(rsAccounts.GetString(5)) ' Access Token (Hidden)
            lstViewRow.SubItems.Add(rsAccounts.GetString(8)) ' Refresh Token (Hidden)
            lstViewRow.SubItems.Add(Convert.ToDateTime(rsAccounts.GetString(6)).ToString) ' Access Token expire date (Hidden)
            lstViewRow.SubItems.Add(rsAccounts.GetString(7)) ' Token type

            Call lstAccounts.Items.Add(lstViewRow)

            CharList = ""

        End While

        rsAccounts.Close()

        lstAccounts.EndUpdate()

        SQL = "SELECT COUNT(*) FROM ESI_CHARACTER_DATA"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsAccounts = DBCommand.ExecuteReader

        rsAccounts.Read()

        ' Don't enable default setting if there aren't any new api keys
        If CInt(rsAccounts.GetValue(0)) = 0 Then
            btnSelectDefaultChar.Enabled = False
        Else
            btnSelectDefaultChar.Enabled = True
        End If

        Application.UseWaitCursor = False

    End Sub

    Private Sub lstAccounts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstAccounts.SelectedIndexChanged
        If lstAccounts.SelectedItems.Count > 0 Then
            ' Load the scopes for the character (hidden in the list of the account)
            Dim ScopeList As String = lstAccounts.SelectedItems.Item(0).SubItems(4).Text

            Call LoadScopes(ScopeList)

            txtAccessToken.Text = lstAccounts.SelectedItems.Item(0).SubItems(5).Text
            txtAccessTokenExpDate.Text = lstAccounts.SelectedItems.Item(0).SubItems(7).Text
            txtRefreshToken.Text = lstAccounts.SelectedItems.Item(0).SubItems(6).Text
            btnRefreshToken.Enabled = True
            btnDeleteCharacter.Enabled = True
            btnCopyAll.Enabled = True
        Else
            lstScopes.Items.Clear()
            txtAccessToken.Text = ""
            txtAccessTokenExpDate.Text = ""
            txtAccessTokenExpDate.Text = ""
            btnRefreshToken.Enabled = False
            btnDeleteCharacter.Enabled = False
            btnCopyAll.Enabled = False
        End If
    End Sub

    Private Sub LoadScopes(ScopeList As String)
        ' Parse it for entry
        Dim ParsedScopes As String()

        ParsedScopes = ScopeList.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        ' Clear the list
        lstScopes.Items.Clear()

        For Each Scope In ParsedScopes
            lstScopes.Items.Add(Scope)
        Next

    End Sub

    Private Sub btnDeleteKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCharacter.Click
        Dim SQL As String

        If lstAccounts.SelectedItems.Count > 0 Then
            Dim CharacterID As Integer = CInt(lstAccounts.SelectedItems.Item(0).SubItems(0).Text)

            Call EVEDB.BeginSQLiteTransaction()

            ' Delete all the information associated with this key FIX (SKILLS, STANDINGS, ASSETS, JOBS, AGENTS)
            SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & CStr(CharacterID)
            EVEDB.ExecuteNonQuerySQL(SQL)

            SQL = "DELETE FROM CHARACTER_STANDINGS WHERE CHARACTER_ID = " & CStr(CharacterID)

            SQL = "DELETE FROM CURRENT_RESEARCH_AGENTS WHERE CHARACTER_ID = " & CStr(CharacterID)
            EVEDB.ExecuteNonQuerySQL(SQL)

            SQL = "DELETE FROM ASSETS WHERE ID = " & CStr(CharacterID)
            EVEDB.ExecuteNonQuerySQL(SQL)

            SQL = "DELETE FROM INDUSTRY_JOBS WHERE installerID = " & CStr(CharacterID)
            EVEDB.ExecuteNonQuerySQL(SQL)

            SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID = " & CStr(CharacterID)
            EVEDB.ExecuteNonQuerySQL(SQL)

            SQL = "DELETE FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = " & CStr(CharacterID)
            EVEDB.ExecuteNonQuerySQL(SQL)

            ' Finally see if we have more accounts that are not the dummy - if only the dummy exists, set it to default and load it
            SQL = "SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> " & CStr(DummyCharacterID)
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)

            If CInt(DBCommand.ExecuteScalar()) = 0 Then
                ' Only the dummy is loaded, so set it to default
                EVEDB.ExecuteNonQuerySQL("UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = " & CStr(DefaultCharacterCode) & " WHERE CHARACTER_ID = " & CStr(DummyCharacterID))
            End If

            Call EVEDB.CommitSQLiteTransaction()

            ' Reload the characters - this will do the default selection, etc
            Call LoadCharacter(UserApplicationSettings.LoadAssetsonStartup, UserApplicationSettings.LoadBPsonStartup)

            MsgBox("Character Deleted", vbInformation, Application.ProductName)

        End If

        ' Reload accounts
        Call LoadAccountGrid()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCharacter.Click
        Dim fLoadAPI As New frmAddCharacter
        fLoadAPI.ShowDialog()

        lstAccounts.Items.Clear()

        ' Reload accounts
        Call LoadAccountGrid()

    End Sub

    Private Sub btnSelectDefaultChar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDefaultChar.Click
        Dim fDefault As New frmSetCharacterDefault

        fDefault.ShowDialog()

        Call LoadAccountGrid()

    End Sub

    Private Sub lstAccounts_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lstAccounts.ItemSelectionChanged
        If lstAccounts.SelectedItems.Count = 0 Then
            btnDeleteCharacter.Enabled = False
        Else
            btnDeleteCharacter.Enabled = True
        End If
    End Sub

    ' For the selected character, refresh the token data again without regard to the cache date
    Private Sub btnRefreshToken_Click(sender As Object, e As EventArgs) Handles btnRefreshToken.Click
        If lstAccounts.SelectedItems.Count > 0 Then
            ' Get the token data from the grid since they can't edit them anyway
            Dim CharacterTokenData As New SavedTokenData
            Dim ESIData As New ESI

            CharacterTokenData.CharacterID = CLng(lstAccounts.SelectedItems.Item(0).SubItems(0).Text)
            CharacterTokenData.Scopes = lstAccounts.SelectedItems.Item(0).SubItems(4).Text
            CharacterTokenData.AccessToken = lstAccounts.SelectedItems.Item(0).SubItems(5).Text
            CharacterTokenData.TokenExpiration = CDate(lstAccounts.SelectedItems.Item(0).SubItems(7).Text)
            CharacterTokenData.TokenType = lstAccounts.SelectedItems.Item(0).SubItems(8).Text
            CharacterTokenData.RefreshToken = lstAccounts.SelectedItems.Item(0).SubItems(6).Text

            If ESIData.SetCharacterData(CharacterTokenData, "", True) Then
                ' Need to reload the data and set access flags based on scopes
                Application.UseWaitCursor = True
                Dim SelectedIndex As Integer = lstAccounts.SelectedIndices(0)
                Call lstAccounts.SelectedIndices.Clear()
                txtAccessToken.Text = ""
                txtRefreshToken.Text = ""
                txtAccessTokenExpDate.Text = ""
                lstScopes.Items.Clear()
                lstAccounts.Enabled = False
                lstScopes.Enabled = False
                Application.DoEvents()
                Call SelectedCharacter.LoadCharacterData(CharacterTokenData, False, False)
                Call LoadAccountGrid()
                MsgBox("Token Data updated.", vbInformation, Application.ProductName)
                Application.UseWaitCursor = False
                lstAccounts.Enabled = True
                lstScopes.Enabled = True
                lstAccounts.Items(SelectedIndex).Selected = True
                Application.DoEvents()
            Else
                MsgBox("Unable to update Token Data.", vbInformation, Application.ProductName)
            End If

        End If
    End Sub

    Private Sub btnCopyAll_Click(sender As Object, e As EventArgs) Handles btnCopyAll.Click
        Dim ClipboardData = New DataObject
        Dim OutputText As String = ""

        OutputText = "Token Data for " & lstAccounts.SelectedItems.Item(0).SubItems(1).Text & vbCrLf
        OutputText &= "Access Token: " & lstAccounts.SelectedItems.Item(0).SubItems(5).Text & vbCrLf
        OutputText &= "Refresh Token: " & lstAccounts.SelectedItems.Item(0).SubItems(6).Text & vbCrLf
        OutputText &= "Access Token Expires: " & lstAccounts.SelectedItems.Item(0).SubItems(7).Text & vbCrLf & vbCrLf
        OutputText &= "Selected Scopes: " & lstAccounts.SelectedItems.Item(0).SubItems(4).Text.Replace(" ", " " & vbCrLf)

        ' Paste to clipboard
        Call CopyTextToClipboard(OutputText)

    End Sub

    Private Sub txtRefreshToken_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRefreshToken.KeyDown
        Select Case e.KeyCode
            Case Keys.A, Keys.C
                If e.Modifiers = Keys.Control Then
                    Application.DoEvents()
                Else
                    e.Handled = True
                End If
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub txtAccessTokenExpDate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccessTokenExpDate.KeyDown
        Select Case e.KeyCode
            Case Keys.A, Keys.C
                If e.Modifiers = Keys.Control Then
                    Application.DoEvents()
                Else
                    e.Handled = True
                End If
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub txtAccessToken_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccessToken.KeyDown
        Select Case e.KeyCode
            Case Keys.A, Keys.C
                If e.Modifiers = Keys.Control Then
                    Application.DoEvents()
                Else
                    e.Handled = True
                End If
            Case Else
                e.Handled = True
        End Select
    End Sub

End Class