Imports System.Data.SQLite

Public Class frmAddCharacter

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Always check all
        ESIScopesString = ""
        Call SelectCheckboxes(True) ' Check all as default

        ' Set the tool tips for api
        With ttAPI
            .SetToolTip(chkReadCharacterAgentsResearch, "Reads a list of research agents' information for a character.")
            .SetToolTip(chkReadCharacterAssets, "Reads a list of the character's assets.")
            .SetToolTip(chkReadCharacterBlueprints, "Reads a list of blueprints the character owns.")
            .SetToolTip(chkReadCharacterJobs, "Reads a list of the character's industry jobs.")
            .SetToolTip(chkReadCharacterStandings, "Reads a list of character standings from agents, NPC corporations, and factions.")
            .SetToolTip(chkReadCharacterOrders, "Reads a list of all open market orders on the market for a character and allows a list of cancelled and expired market orders placed by the character up to 90 days in the past.")
            .SetToolTip(chkReadCharacterWallet, "Reads the wallet balance, transactions, and journal of a character going 30 days back.")
            .SetToolTip(chkReadCharacterPlanetary, "Reads a list of all planetary colonies and layouts owned by a character.")
            .SetToolTip(chkReadCharacterShipLocation, "Reads the current ship type and name of the character.")

            .SetToolTip(chkReadCorporationAssets, "Reads a list of the corporation assets.")
            .SetToolTip(chkReadCorporationBlueprints, "Reads a list of blueprints the corporation owns. Must have Director or Factory Manager role to see Corporation Blueprints.")
            .SetToolTip(chkReadCorporationJobs, "List industry jobs run by a corporation. Must have Director or Factory Manager role to see Corporation Jobs.")
            .SetToolTip(chkReadCorporationMembership, "List of the current members a corporation and titles (for corp roles).")
            .SetToolTip(chkReadCorporationDivisions, "Lists the names of corporation hanger and wallet division names if not default.")
            .SetToolTip(chkReadCorporationOrders, "Reads a list of all open market orders on the market for a corporation and allows a list of cancelled and expired market orders placed by the corporation up to 90 days in the past. Must have the Director, Accountant, or Trader role to see Corporation orders.")
            .SetToolTip(chkReadCorporationWallet, "Reads the wallet balance, transactions, and journal of a corporation going 30 days back. Must have Director, Accountant, or Junior Accountant role to see Corporation Wallet.")

            .SetToolTip(chkReadStructures, "Returns information on requested structure if you are on the Access Control List.")
            .SetToolTip(chkReadStructureMarkets, "Reads market orders from a specific structure with a market installed.")

        End With

    End Sub

    Private Sub btnEVESSOLogin_Click(sender As Object, e As EventArgs) Handles btnEVESSOLogin.Click
        Dim ESIConnection As New ESI
        Dim CharacterData As New SavedTokenData
        Dim CharCorpID As Long = 0

        Me.Cursor = Cursors.WaitCursor
        Call EnableDisableForm(False) ' Disable until we return
        Application.DoEvents()

        ' Strip the last space
        ESIScopesString = ESIScopesString.Substring(0, Len(ESIScopesString) - 1)

        '  Set the final scopes string - always needs skills
        ESIScopesString = "esi-skills.read_skills.v1 " & ESIScopesString

        ' Set the new character data first. This will load the data in the table or update it if they choose a character already loaded
        If ESIConnection.SetCharacterData(False, CharacterData, "", False, True, CharCorpID) Then

            ' Refresh the token data to get new scopes list if they added/removed
            If CharacterData.CharacterID <> DummyCharacterID And CharacterData.CharacterID > 0 Then
                Call SelectedCharacter.RefreshTokenData(CharacterData.CharacterID, CharCorpID)
            End If

            ' If they loaded a character for the first time, set it from Dummy to this character as the default
            If SelectedCharacter.ID = DummyCharacterID Then
                ' See if only one other character exists in db (the one we just added)
                Dim rsCheck As SQLiteDataReader
                DBCommand = New SQLiteCommand("SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> " & DummyCharacterID, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader
                rsCheck.Read()

                If rsCheck.GetInt32(0) = 1 Then
                    ' They only have one other character in the db and the selected is dummy, so set this to the default
                    rsCheck.Close()
                    DBCommand = New SQLiteCommand("SELECT CHARACTER_NAME FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> " & DummyCharacterID, EVEDB.DBREf)
                    rsCheck = DBCommand.ExecuteReader
                    rsCheck.Read()
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    Call SetDefaultCharacter(rsCheck.GetString(0))
                    Me.Cursor = Cursors.Default
                    Application.DoEvents()
                End If
            Else
                MsgBox("Character successfully added to IPH", vbInformation, Application.ProductName)
            End If
        Else
            ' Didn't load, so show the re-enter info button
            If Not CancelESISSOLogin Then
                MsgBox("The Character failed to load. Please check application registration information.")
            End If
        End If

        Me.Cursor = Cursors.Default
        Call EnableDisableForm(True)
        Application.DoEvents()

        ' Close the form - users will have to select what one to set as default
        Me.Hide()

    End Sub

    Private Sub UpdateScopesString(AppendString As String, AddString As Boolean)
        If AddString Then
            ESIScopesString &= AppendString & " "
        Else
            ' Need to remove the string, parse and remove, then rebuild
            Dim Scopes As String()

            Scopes = ESIScopesString.Split(New Char() {" "c})
            ESIScopesString = "" ' Reset

            For i = 0 To Scopes.Count - 1
                If Scopes(i) <> AppendString And Trim(Scopes(i)) <> "" Then
                    ESIScopesString &= Scopes(i) & " "
                End If
            Next
        End If

    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        Call SelectCheckboxes(True)
    End Sub

    Private Sub btnDeselectAll_Click(sender As Object, e As EventArgs) Handles btnDeselectAll.Click
        Call SelectCheckboxes(False)
    End Sub

    Private Sub chkReadCharacterStandings_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterStandings.CheckedChanged
        With chkReadCharacterStandings
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharacterAgentsResearch_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterAgentsResearch.CheckedChanged
        With chkReadCharacterAgentsResearch
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharacterJobs_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterJobs.CheckedChanged
        With chkReadCharacterJobs
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharacterAssets_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterAssets.CheckedChanged
        With chkReadCharacterAssets
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterBlueprints.CheckedChanged
        With chkReadCharacterBlueprints
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharacterOrders_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterOrders.CheckedChanged
        With chkReadCharacterOrders
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharacterWallet_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterWallet.CheckedChanged
        With chkReadCharacterWallet
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharactership_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterShipLocation.CheckedChanged
        With chkReadCharacterShipLocation
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCharacterLoyalty_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterLoyalty.CheckedChanged
        With chkReadCharacterLoyalty
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkManagePlanets_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCharacterPlanetary.CheckedChanged
        With chkReadCharacterPlanetary
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationMembership_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationMembership.CheckedChanged
        With chkReadCorporationMembership
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationDivisions_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationDivisions.CheckedChanged
        With chkReadCorporationDivisions
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationJobs_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationJobs.CheckedChanged
        With chkReadCorporationJobs
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationAssets_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationAssets.CheckedChanged
        With chkReadCorporationAssets
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationBlueprints.CheckedChanged
        With chkReadCorporationBlueprints
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationOrders_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationOrders.CheckedChanged
        With chkReadCorporationOrders
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadCorporationWallet_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadCorporationWallet.CheckedChanged
        With chkReadCorporationWallet
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadStructures_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadStructures.CheckedChanged
        With chkReadStructures
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub chkReadStructureMarkets_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadStructureMarkets.CheckedChanged
        With chkReadStructureMarkets
            Call UpdateScopesString(CStr(.Tag), .Checked)
        End With
    End Sub

    Private Sub EnableDisableForm(Setting As Boolean)
        btnEVESSOLogin.Enabled = False
        chkReadCharacterPlanetary.Enabled = Setting
        chkReadCharacterAgentsResearch.Enabled = Setting
        chkReadCharacterAssets.Enabled = Setting
        chkReadCharacterBlueprints.Enabled = Setting
        chkReadCharacterJobs.Enabled = Setting
        chkReadCharacterOrders.Enabled = Setting
        chkReadCharacterWallet.Enabled = Setting
        chkReadCharacterShipLocation.Enabled = Setting
        chkReadCharacterLoyalty.Enabled = Setting
        chkReadCharacterPlanetary.Enabled = Setting

        chkReadCorporationAssets.Enabled = Setting
        chkReadCorporationBlueprints.Enabled = Setting
        chkReadCorporationJobs.Enabled = Setting
        chkReadCorporationMembership.Enabled = Setting
        chkReadCorporationDivisions.Enabled = Setting
        chkReadCorporationWallet.Enabled = Setting
        chkReadCorporationOrders.Enabled = Setting
        chkReadCharacterStandings.Enabled = Setting

        chkReadStructures.Enabled = Setting
        chkReadStructureMarkets.Enabled = Setting
    End Sub

    Private Sub SelectCheckboxes(Setting As Boolean)
        chkReadCharacterPlanetary.Checked = Setting
        chkReadCharacterAgentsResearch.Checked = Setting
        chkReadCharacterAssets.Checked = Setting
        chkReadCharacterBlueprints.Checked = Setting
        chkReadCharacterJobs.Checked = Setting
        chkReadCharacterOrders.Checked = Setting
        chkReadCharacterWallet.Checked = Setting
        chkReadCharacterShipLocation.Checked = Setting
        chkReadCharacterLoyalty.Checked = Setting
        chkReadCharacterPlanetary.Checked = Setting

        chkReadCorporationAssets.Checked = Setting
        chkReadCorporationBlueprints.Checked = Setting
        chkReadCorporationJobs.Checked = Setting
        chkReadCorporationMembership.Checked = Setting
        chkReadCorporationDivisions.Checked = Setting
        chkReadCorporationWallet.Checked = Setting
        chkReadCorporationOrders.Checked = Setting
        chkReadCharacterStandings.Checked = Setting

        chkReadStructures.Checked = Setting
        chkReadStructureMarkets.Checked = Setting
    End Sub

End Class