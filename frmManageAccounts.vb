Imports System.Data.SQLite

Public Class frmManageAccounts

    Private lstColumnSorter As ListViewColumnSorter

    Private Sub lstAccounts_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstAccounts.ColumnClick

        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, lstColumnSorter)

        ' Perform the sort with these new sort options.
        lstAccounts.Sort()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub frmManageAccounts_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Call LoadAccountGrid()
    End Sub

    Private Sub LoadAccountGrid()
        Dim readerAccounts As SQLiteDataReader
        Dim readerChars As SQLiteDataReader
        Dim SQL As String
        Dim lstViewRow As ListViewItem
        Dim CharList As String = ""

        ' Until there is a key able to set a default to, don't enable the select default button
        btnSelectDefaultChar.Enabled = False

        lblDefaultChar.Text = "Default Character: " & SelectedCharacter.Name
        lblDefaultChar.Left = CInt((Me.Width / 2) - (lblDefaultChar.Width / 2))

        Application.UseWaitCursor = True

        SQL = "SELECT API_TYPE, KEY_ID, API_KEY, ACCESS_MASK, KEY_EXPIRATION_DATE, CORPORATION_NAME FROM API WHERE KEY_ID <> 0 "
        SQL = SQL & "GROUP BY KEY_EXPIRATION_DATE, API_TYPE, KEY_ID, API_KEY, ACCESS_MASK"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerAccounts = DBCommand.ExecuteReader

        lstColumnSorter = New ListViewColumnSorter()
        lstAccounts.ListViewItemSorter = lstColumnSorter

        lstAccounts.Items.Clear()
        lstAccounts.BeginUpdate()

        While readerAccounts.Read

            ' Insert into table
            lstViewRow = lstAccounts.Items.Add(readerAccounts.GetString(0)) ' API Type
            'The remaining columns are subitems  
            lstViewRow.SubItems.Add(CStr(CLng(readerAccounts.GetValue(1)))) ' KeyID
            lstViewRow.SubItems.Add(readerAccounts.GetString(2)) ' API

            If readerAccounts.GetString(0) <> CorporationAPITypeName Then
                ' Get the characters for this key
                SQL = "SELECT CHARACTER_NAME FROM API WHERE KEY_ID=" & CStr(readerAccounts.GetValue(1)) & " AND API_KEY = '" & CStr(readerAccounts.GetString(2)) & "'"
                DBCommand = New SQLiteCommand(SQL, DB)
                readerChars = DBCommand.ExecuteReader

                While readerChars.Read
                    CharList = CharList & readerChars.GetString(0) & ", "
                End While

                ' Strip comma
                CharList = CharList.Substring(0, Len(CharList) - 2)

                ' Have account APIs to use, so enable the set default button
                btnSelectDefaultChar.Enabled = True

            Else ' use corp name
                CharList = readerAccounts.GetString(5)
            End If

            lstViewRow.SubItems.Add(CharList) ' Characters
            lstViewRow.SubItems.Add(CStr(CDbl(readerAccounts.GetValue(3)))) ' Mask
            ' Expiration Date
            If readerAccounts.GetString(4) = ExpiredKey Then
                lstViewRow.SubItems.Add(ExpiredKey)
            ElseIf CDate(readerAccounts.GetString(4)) = NoExpiry Then
                lstViewRow.SubItems.Add("Never")
            Else
                lstViewRow.SubItems.Add(CStr(CDate(readerAccounts.GetString(4))))
            End If

            CharList = ""

        End While

        lstAccounts.EndUpdate()

        SQL = "SELECT COUNT(*) FROM API WHERE API_TYPE <> 'Old Key'"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerChars = DBCommand.ExecuteReader

        readerChars.Read()

        ' Don't enable default setting if there aren't any new api keys
        If CInt(readerChars.GetValue(0)) = 0 Then
            btnSelectDefaultChar.Enabled = False
        End If

        Application.UseWaitCursor = False

    End Sub

    Private Sub lstAccounts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAccounts.SelectedIndexChanged
        Dim SelectedAccessMask As Long
        Dim BitString As String
        Dim BitLen As Integer

        If lstAccounts.SelectedItems.Count > 0 Then
            btnDeleteKey.Enabled = True
            btnUpdateKey.Enabled = True

            ' Take the selected row and update the check boxes based on the access mask
            SelectedAccessMask = CLng(lstAccounts.SelectedItems(0).SubItems(4).Text)

            ' Access mask is a bitmask 
            BitString = GetBits(SelectedAccessMask)

            BitLen = Len(BitString)

            If lstAccounts.SelectedItems(0).SubItems(0).Text <> CorporationAPITypeName Then

                chkAccessCharacterSheet.Enabled = True
                chkAccessSIResearch.Enabled = True
                chkAccessStandings.Enabled = True

                ' Just do a bool cast on the bits for any API access stuff we want
                If BitLen >= AccessMaskBitLocs.CharacterSheet Then
                    chkAccessCharacterSheet.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.CharacterSheet, 1))
                Else
                    chkAccessCharacterSheet.Checked = False
                End If

                If BitLen >= AccessMaskBitLocs.Standings Then
                    chkAccessStandings.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.Standings, 1))
                Else
                    chkAccessStandings.Checked = False
                End If

                If BitLen >= AccessMaskBitLocs.AssetList Then
                    chkAccessAssets.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.AssetList, 1))
                Else
                    chkAccessAssets.Checked = False
                End If

                If BitLen >= AccessMaskBitLocs.IndustryJobs Then
                    chkAccessSIIndustryJobs.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.IndustryJobs, 1))
                Else
                    chkAccessSIIndustryJobs.Checked = False
                End If

                If BitLen >= AccessMaskBitLocs.Research Then
                    chkAccessSIResearch.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.Research, 1))
                Else
                    chkAccessSIResearch.Checked = False
                End If
            Else
                ' Corp key
                chkAccessCharacterSheet.Enabled = False
                chkAccessCharacterSheet.Checked = False
                chkAccessSIResearch.Enabled = False
                chkAccessSIResearch.Checked = False
                chkAccessStandings.Enabled = False
                chkAccessStandings.Checked = False

                If BitLen >= AccessMaskBitLocs.IndustryJobs Then
                    chkAccessSIIndustryJobs.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.IndustryJobs, 1))
                Else
                    chkAccessSIIndustryJobs.Checked = False
                End If

                If BitLen >= AccessMaskBitLocs.AssetList Then
                    chkAccessAssets.Checked = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.AssetList, 1))
                Else
                    chkAccessAssets.Checked = False
                End If

            End If
        End If
    End Sub

    Private Sub btnUpdateKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateKey.Click
        Dim fLoadAPI As New frmLoadCharacterAPI

        If lstAccounts.SelectedItems.Count > 0 Then
            fLoadAPI.PreDefinedID = lstAccounts.SelectedItems.Item(0).SubItems(1).Text
            fLoadAPI.PreDefinedKey = lstAccounts.SelectedItems.Item(0).SubItems(2).Text
        End If

        fLoadAPI.ShowDialog()

        ' Reload accounts
        Call LoadAccountGrid()

    End Sub

    Private Sub btnDeleteKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteKey.Click
        Dim DKeyID As Long
        Dim DKey As String
        Dim SQL As String
        Dim rsAPI As SQLiteDataReader
        Dim CorpID As Long

        If lstAccounts.SelectedItems.Count > 0 Then
            DKeyID = CLng(lstAccounts.SelectedItems.Item(0).SubItems(1).Text)
            DKey = lstAccounts.SelectedItems.Item(0).SubItems(2).Text

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Call BeginSQLiteTransaction()

            ' Find out what type of key it is - corp or personal
            If lstAccounts.SelectedItems.Item(0).SubItems(0).Text = "Corporation" Then
                ' Just delete assets and jobs for this corporation ID
                SQL = "SELECT CORPORATION_ID FROM API WHERE KEY_ID = " & DKeyID

                DBCommand = New SQLiteCommand(SQL, DB)
                rsAPI = DBCommand.ExecuteReader
                rsAPI.Read()

                ' Get the corp id first, then use that to 
                CorpID = rsAPI.GetInt64(0)

                ' Now look up all the accounts that have this corp ID to delete all associated corp jobs
                SQL = "SELECT CHARACTER_ID FROM API WHERE CORPORATION_ID = " & CorpID

                DBCommand = New SQLiteCommand(SQL, DB)
                rsAPI = DBCommand.ExecuteReader

                While rsAPI.Read
                    SQL = "DELETE FROM INDUSTRY_JOBS WHERE installerID = " & rsAPI.GetInt64(0) & " AND JobType = " & ScanType.Corporation
                    ExecuteNonQuerySQL(SQL)
                End While

                SQL = "DELETE FROM ASSETS WHERE ID = " & CorpID
                ExecuteNonQuerySQL(SQL)

                SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID = " & CorpID
                ExecuteNonQuerySQL(SQL)

            Else ' Need to delete any stored skills, standings, agents, assets, and jobs for all characters
                SQL = "SELECT CHARACTER_ID FROM API WHERE KEY_ID = " & DKeyID

                DBCommand = New SQLiteCommand(SQL, DB)
                rsAPI = DBCommand.ExecuteReader

                While rsAPI.Read
                    ' Delete all the information associated with this key FIX (SKILLS, STANDINGS, ASSETS, JOBS, AGENTS)
                    SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & rsAPI.GetInt64(0)
                    ExecuteNonQuerySQL(SQL)

                    SQL = "DELETE FROM CHARACTER_STANDINGS WHERE CHARACTER_ID = " & rsAPI.GetInt64(0)
                    ExecuteNonQuerySQL(SQL)

                    SQL = "DELETE FROM CURRENT_RESEARCH_AGENTS WHERE CHARACTER_ID = " & rsAPI.GetInt64(0)
                    ExecuteNonQuerySQL(SQL)

                    SQL = "DELETE FROM ASSETS WHERE ID = " & rsAPI.GetInt64(0)
                    ExecuteNonQuerySQL(SQL)

                    SQL = "DELETE FROM INDUSTRY_JOBS WHERE installerID = " & rsAPI.GetInt64(0)
                    ExecuteNonQuerySQL(SQL)

                    SQL = "DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID = " & rsAPI.GetInt64(0)
                    ExecuteNonQuerySQL(SQL)

                End While

            End If

            SQL = "DELETE FROM API WHERE KEY_ID = " & DKeyID & " AND API_KEY = '" & DKey & "'"
            ExecuteNonQuerySQL(SQL)

            ' If it's the account we have loaded, and we just deleted the key, then reset the selected character
            If lstAccounts.SelectedItems.Item(0).SubItems(0).Text <> CorporationAPITypeName And _
                InStr(lstAccounts.SelectedItems.Item(0).SubItems(3).Text, SelectedCharacter.Name) <> 0 Then
                SelectedCharacter = New Character
            End If

            Call CommitSQLiteTransaction()

            ' Reload the characters - this will do the default selection, etc
            Call LoadCharacter(UserApplicationSettings.LoadAssetsonStartup, UserApplicationSettings.LoadBPsonStartup)

            MsgBox("Character API Deleted", vbInformation, Application.ProductName)
        End If

        ' Reload accounts
        Call LoadAccountGrid()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim fLoadAPI As New frmLoadCharacterAPI
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

End Class