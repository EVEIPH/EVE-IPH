Public Class frmManageAccounts

    Private ListColumnClicked As Integer
    Private ListColumnSortOrder As SortOrder
    Private CurrentViewModel As ManageAccountsViewModel

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
        Call ReloadAccountGrid()
    End Sub

    Private Sub ReloadAccountGrid()
        Application.UseWaitCursor = True
        CurrentViewModel = ManageAccountsService.LoadAccounts()
        Call RenderAccountGrid(CurrentViewModel)
        Application.UseWaitCursor = False
    End Sub

    Private Sub RenderAccountGrid(ByVal viewModel As ManageAccountsViewModel)
        Dim lstViewRow As ListViewItem

        btnSelectDefaultChar.Enabled = viewModel.CanSelectDefaultCharacter

        lstAccounts.Items.Clear()
        lstAccounts.BeginUpdate()

        For Each account In viewModel.Accounts
            lstViewRow = New ListViewItem(CStr(account.CharacterID)) ' CHAR ID (Hidden)
            lstViewRow.SubItems.Add(account.CharacterName) ' NAME
            lstViewRow.SubItems.Add(account.CorporationName) ' CORP NAME

            If account.IsDefault Then
                lstViewRow.SubItems.Add("True")
            Else
                lstViewRow.SubItems.Add("False")
            End If

            lstViewRow.Tag = account
            Call lstAccounts.Items.Add(lstViewRow)
        Next

        lstAccounts.EndUpdate()
        Call ClearSelectedAccountDetails()
    End Sub

    Private ReadOnly Property SelectedAccount As ManageAccountItemViewModel
        Get
            If lstAccounts.SelectedItems.Count = 0 Then
                Return Nothing
            End If

            Return TryCast(lstAccounts.SelectedItems.Item(0).Tag, ManageAccountItemViewModel)
        End Get
    End Property

    Private Sub RenderSelectedAccount(ByVal account As ManageAccountItemViewModel)
        If IsNothing(account) Then
            Call ClearSelectedAccountDetails()
            Return
        End If

        Call LoadScopes(account.Scopes)

        txtAccessToken.Text = account.AccessToken
        txtAccessTokenExpDate.Text = account.AccessTokenExpiration.ToString()
        txtRefreshToken.Text = account.RefreshToken
        txtCharacterID.Text = CStr(account.CharacterID)
        txtCorpID.Text = CStr(account.CorporationID)
        chkDirector.Checked = account.IsDirector
        chkFactoryManager.Checked = account.IsFactoryManager
        btnRefreshToken.Enabled = True
        btnDeleteCharacter.Enabled = True
        btnCopyAll.Enabled = True
    End Sub

    Private Sub ClearSelectedAccountDetails()
        chkDirector.Checked = False
        chkFactoryManager.Checked = False
        lstScopes.Items.Clear()
        txtAccessToken.Text = ""
        txtRefreshToken.Text = ""
        txtCharacterID.Text = ""
        txtCorpID.Text = ""
        txtAccessTokenExpDate.Text = ""
        btnRefreshToken.Enabled = False
        btnDeleteCharacter.Enabled = False
        btnCopyAll.Enabled = False
    End Sub

    Private Sub SelectAccountByCharacterID(ByVal characterID As Long)
        For Each item As ListViewItem In lstAccounts.Items
            Dim account = TryCast(item.Tag, ManageAccountItemViewModel)

            If Not IsNothing(account) AndAlso account.CharacterID = characterID Then
                item.Selected = True
                item.Focused = True
                item.EnsureVisible()
                Exit For
            End If
        Next
    End Sub

    Private Sub lstAccounts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstAccounts.SelectedIndexChanged
        Call RenderSelectedAccount(SelectedAccount)
    End Sub

    Private Sub LoadScopes(ByVal scopeList As String)
        Dim parsedScopes As String()

        parsedScopes = scopeList.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        lstScopes.Items.Clear()

        For Each scope In parsedScopes
            lstScopes.Items.Add(scope)
        Next

    End Sub

    Private Sub btnDeleteKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCharacter.Click
        If MessageBox.Show("Are you sure you want to delete this account?", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Dim account = SelectedAccount

        If Not IsNothing(account) Then
            Dim deleteResult = ManageAccountsService.DeleteCharacter(account,
                                                                     UserApplicationSettings.LoadAssetsonStartup,
                                                                     UserApplicationSettings.LoadBPsonStartup)

            Call frmMain.LoadCharacterNamesinMenu()

            If deleteResult.RequiresDefaultCharacterSelection Then
                Dim defaultCharacterForm As New frmSetCharacterDefault
                defaultCharacterForm.ShowDialog()
            End If

            MsgBox("Character Deleted", vbInformation, Application.ProductName)
        End If

        Call ReloadAccountGrid()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCharacter.Click
        Dim fLoadAPI As New frmAddCharacter
        fLoadAPI.ShowDialog()

        lstAccounts.Items.Clear()

        Call frmMain.LoadCharacterNamesinMenu()
        Call ReloadAccountGrid()

    End Sub

    Private Sub btnSelectDefaultChar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDefaultChar.Click
        Dim fDefault As New frmSetCharacterDefault

        fDefault.ShowDialog()

        Call ReloadAccountGrid()

    End Sub

    Private Sub lstAccounts_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lstAccounts.ItemSelectionChanged
        If lstAccounts.SelectedItems.Count = 0 Then
            btnDeleteCharacter.Enabled = False
        Else
            btnDeleteCharacter.Enabled = True
        End If
    End Sub

    Private Sub btnRefreshToken_Click(sender As Object, e As EventArgs) Handles btnRefreshToken.Click
        Dim account = SelectedAccount

        If Not IsNothing(account) Then
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If ManageAccountsService.RefreshCharacterToken(account) Then
                Application.UseWaitCursor = True
                Call lstAccounts.SelectedIndices.Clear()
                txtAccessToken.Text = ""
                txtRefreshToken.Text = ""
                txtAccessTokenExpDate.Text = ""
                lstScopes.Items.Clear()
                lstAccounts.Enabled = False
                lstScopes.Enabled = False
                Application.DoEvents()
                Call ReloadAccountGrid()
                MsgBox("Token Data updated.", vbInformation, Application.ProductName)
                Application.UseWaitCursor = False
                lstAccounts.Enabled = True
                lstScopes.Enabled = True
                Call SelectAccountByCharacterID(account.CharacterID)
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            Else
                Me.Cursor = Cursors.Default
                Application.DoEvents()
                MsgBox("Unable to update Token Data.", vbInformation, Application.ProductName)
            End If

        End If
    End Sub

    Private Sub btnCopyAll_Click(sender As Object, e As EventArgs) Handles btnCopyAll.Click
        Dim outputText As String = ""
        Dim account = SelectedAccount

        If IsNothing(account) Then
            Exit Sub
        End If

        outputText = "Token Data for " & account.CharacterName & vbCrLf
        outputText &= "Access Token: " & account.AccessToken & vbCrLf
        outputText &= "Refresh Token: " & account.RefreshToken & vbCrLf
        outputText &= "Access Token Expires: " & account.AccessTokenExpiration.ToString() & vbCrLf & vbCrLf
        outputText &= "Selected Scopes: " & account.Scopes.Replace(" ", " " & vbCrLf)

        Call CopyTextToClipboard(outputText)

    End Sub

    Private Sub btnCopyCharacterID_Click(sender As Object, e As EventArgs) Handles btnCopyCharacterID.Click
        Call CopyTextToClipboard(txtCharacterID.Text)
    End Sub

    Private Sub btnCopyCorpID_Click(sender As Object, e As EventArgs) Handles btnCopyCorpID.Click
        Call CopyTextToClipboard(txtCorpID.Text)
    End Sub

    Private Sub btnCopyAccesToken_Click(sender As Object, e As EventArgs) Handles btnCopyAccesToken.Click
        Call CopyTextToClipboard(txtAccessToken.Text)
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
