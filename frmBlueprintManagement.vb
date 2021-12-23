
Imports System.Data.SQLite
Imports System.IO

Public Class frmBlueprintManagement
    Private cmbBPTypeFilterLoaded As Boolean
    Private FirstLoad As Boolean
    Private SelectedType As String

    Private DataEntered As Boolean
    Private TabPressed As Boolean

    ' Inline grid row update variables
    Private CurrentRow As ListViewItem
    Private PreviousRow As ListViewItem
    Private NextRow As ListViewItem

    Private NextCellRow As ListViewItem
    Private PreviousCellRow As ListViewItem

    Private CurrentCell As ListViewItem.ListViewSubItem
    Private PreviousCell As ListViewItem.ListViewSubItem
    Private NextCell As ListViewItem.ListViewSubItem

    Private MEUpdate As Boolean
    Private TEUpdate As Boolean
    Private FavoriteUpdate As Boolean
    Private OwnedTypeUpdate As Boolean
    Private IgnoredBPUpdate As Boolean
    Private BPTypeUpdate As Boolean

    Private BPColumnClicked As Integer
    Private BPColumnSortOrder As SortOrder

    Private Const SelectTypeText As String = "Select Type"

    Private Structure BlueprintAsset
        Dim TypeID As Long
        Dim BPType As Integer
    End Structure

#Region "ObjectSection"

    Private Sub lstBPs_ColumnClick(sender As System.Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstBPs.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstBPs, ListView), BPColumnClicked, BPColumnSortOrder)
    End Sub

    Private Sub lstBPs_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstBPs.SelectedIndexChanged

        If lstBPs.SelectedIndices.Count <> 0 Then
            ' Put the selected BP ME/TE in the boxes when clicked
            txtBPME.Text = lstBPs.SelectedItems(0).SubItems(5).Text
            txtBPTE.Text = lstBPs.SelectedItems(0).SubItems(6).Text
        End If

    End Sub

    Private Sub txtBPME_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPME.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtBPPE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPTE.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtBPME_TextChanged(sender As Object, e As System.EventArgs) Handles txtBPME.TextChanged
        Call VerifyMETEEntry(txtBPME, "ME")
    End Sub

    Private Sub txtBPTE_TextChanged(sender As Object, e As System.EventArgs) Handles txtBPTE.TextChanged
        Call VerifyMETEEntry(txtBPTE, "TE")
    End Sub

    Private Sub btnResetSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetSearch.Click
        txtBPSearch.Text = ""
        Call UpdateBlueprintGrid(True)
    End Sub

    Private Sub rbtnOwned_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnOwned.CheckedChanged
        If Not FirstLoad And rbtnOwned.Checked Then
            ' Disable both scan types
            btnScanPersonalBPs.Enabled = False
            btnScanCorpBPs.Enabled = False
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(False)
        End If
    End Sub

    Private Sub rbtnAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnAllBPs.CheckedChanged
        If Not FirstLoad And rbtnAllBPs.Checked Then
            Call SetScanEnableButtons()
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(False)
        End If
    End Sub

    Private Sub rbtnFavorites_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnFavorites.CheckedChanged
        If Not FirstLoad And rbtnFavorites.Checked Then
            Call SetScanEnableButtons()
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(False)
        End If
    End Sub

    Private Sub rbtnIgnored_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnIgnored.CheckedChanged
        If Not FirstLoad And rbtnIgnored.Checked Then
            Call SetScanEnableButtons()
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(False)
        End If
    End Sub

    Private Sub rbtnScannedPersonalBPs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnScannedPersonalBPs.CheckedChanged

        ' Disable scan button
        If rbtnScannedPersonalBPs.Checked Then
            btnScanPersonalBPs.Enabled = False
            btnScanCorpBPs.Enabled = False
        Else
            Call SetScanEnableButtons()
        End If

        If Not FirstLoad And rbtnScannedPersonalBPs.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(False)
        End If

    End Sub

    Private Sub rbtnScannedCorpBPs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnScannedCorpBPs.CheckedChanged
        ' Disable scan button
        If rbtnScannedCorpBPs.Checked Then
            btnScanPersonalBPs.Enabled = False
            btnScanCorpBPs.Enabled = False
        Else
            Call SetScanEnableButtons()
        End If

        If Not FirstLoad And rbtnScannedCorpBPs.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(False)
        End If

    End Sub

    Private Sub rbtnMarkasOwned_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnMarkasOwned.CheckedChanged
        chkMarkasFavorite.Enabled = True
        chkMarkasIgnored.Enabled = True
        chkEnableMETE.Enabled = True

        If Not chkEnableMETE.Checked Then
            Call EnableMETE(False)
        Else
            Call EnableMETE(True)
        End If

    End Sub

    Private Sub rbtnMarkasUnowned_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnMarkasUnowned.CheckedChanged
        chkMarkasFavorite.Enabled = True
        chkMarkasIgnored.Enabled = True

        If rbtnMarkasUnowned.Checked = True Then
            Call EnableMETE(False)
            chkEnableMETE.Enabled = False
        Else
            If Not chkEnableMETE.Checked Then
                Call EnableMETE(False)
            End If
            chkEnableMETE.Enabled = True
        End If
    End Sub

    Private Sub rbtnRemoveAllSettings_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnRemoveAllSettings.CheckedChanged
        chkMarkasFavorite.Enabled = False
        chkMarkasIgnored.Enabled = False
        chkEnableMETE.Enabled = False
        Call EnableMETE(False)
    End Sub

    Private Sub ResetTypeCmbFillGrid()
        If Not FirstLoad Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub chkBPT1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPT1.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPT2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPT2.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPT3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPT3.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPStoryline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPStoryline.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPNavyFaction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPNavyFaction.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPPirateFaction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPPirateFaction.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPSmall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPSmall.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPMedium_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPMedium.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPXL_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPXL.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkBPLarge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPLarge.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub rbtnShipBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnShipBlueprints.CheckedChanged
        If Not FirstLoad And rbtnShipBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnModuleBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnModuleBlueprints.CheckedChanged
        If Not FirstLoad And rbtnModuleBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnDroneBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDroneBlueprints.CheckedChanged
        If Not FirstLoad And rbtnDroneBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnAmmoChargeBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnAmmoChargeBlueprints.CheckedChanged
        If Not FirstLoad And rbtnAmmoChargeBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnRigBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnRigBlueprints.CheckedChanged
        If Not FirstLoad And rbtnRigBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnComponentBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnComponentBlueprints.CheckedChanged
        If Not FirstLoad And rbtnComponentBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnSubsystemBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSubsystemBlueprints.CheckedChanged
        If Not FirstLoad And rbtnSubsystemBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnStructureBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnStructureBlueprints.CheckedChanged
        If Not FirstLoad And rbtnStructureBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnStructureModulesBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnStructureModulesBlueprints.CheckedChanged
        If Not FirstLoad And rbtnStructureModulesBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnCelestialBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnCelestialsBlueprints.CheckedChanged
        If Not FirstLoad And rbtnCelestialsBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnStationPartsBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnStructureRigsBlueprints.CheckedChanged
        If Not FirstLoad And rbtnStructureRigsBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnDeployableBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDeployableBlueprints.CheckedChanged
        If Not FirstLoad And rbtnDeployableBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnReactionBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnReactionBlueprints.CheckedChanged
        If Not FirstLoad And rbtnReactionBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnAllBPTypes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnAllBPTypes.CheckedChanged
        If Not FirstLoad And rbtnAllBPTypes.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnBoosterBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBoosterBlueprints.CheckedChanged
        If Not FirstLoad And rbtnBoosterBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnToolBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnMiscBlueprints.CheckedChanged
        If Not FirstLoad And rbtnMiscBlueprints.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnBPAllBPtypes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnShowAllBPtypes.CheckedChanged
        If Not FirstLoad And rbtnShowAllBPtypes.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnBPOnlyBPO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnOnlyBPO.CheckedChanged
        If Not FirstLoad And rbtnOnlyBPO.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnBPOnlyCopies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnOnlyCopies.CheckedChanged
        If Not FirstLoad And rbtnOnlyCopies.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub rbtnOnlyInventedBPCs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnOnlyInventedBPCs.CheckedChanged
        If Not FirstLoad And rbtnOnlyInventedBPCs.Checked Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub chkNotOwned_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNotOwned.CheckedChanged
        If Not FirstLoad Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub chkNotIgnored_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNotIgnored.CheckedChanged
        If Not FirstLoad Then
            cmbBPTypeFilterLoaded = False
            cmbBPTypeFilter.Text = SelectTypeText
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    Private Sub chkRaceAmarr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRaceAmarr.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkRaceCaldari_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRaceCaldari.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkRaceGallente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRaceGallente.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkRaceMinmatar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRaceMinmatar.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkRacePirate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRacePirate.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub chkRaceOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRaceOther.CheckedChanged
        Call ResetTypeCmbFillGrid()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Call UpdateBlueprintGrid(True)
    End Sub

    Private Sub cmbBPTypeFilter_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBPTypeFilter.DropDown
        If Not cmbBPTypeFilterLoaded And (SelectedType <> cmbBPTypeFilter.Text) Then
            Call LoadBPTypes()
            cmbBPTypeFilterLoaded = True
        End If
    End Sub

    Private Sub cmbBPTypeFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBPTypeFilter.SelectedIndexChanged
        If SelectedType <> cmbBPTypeFilter.Text And cmbBPTypeFilter.Text <> SelectTypeText Then
            Call UpdateBlueprintGrid(True)
            SelectedType = cmbBPTypeFilter.Text
            cmbBPTypeFilterLoaded = False
        End If
    End Sub

    Private Sub txtBPSearch_GotFocus(sender As Object, e As System.EventArgs) Handles txtBPSearch.GotFocus
        If Not FirstLoad Then
            Call txtBPSearch.SelectAll()
        End If
    End Sub

    Private Sub txtBPSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBPSearch.KeyDown
        'Call ProcessCutCopyPasteSelect(txtBPSearch, e)
        If e.KeyCode = Keys.Enter Then
            Call UpdateBlueprintGrid(True)
        End If
    End Sub

    ' Calls grid update based on text entered
    Private Sub btnBPSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPSearch.Click

        If Trim(txtBPSearch.Text) = "" Then
            MsgBox("Must enter a search item", vbExclamation, Me.Text)
            txtBPSearch.Focus()
            Exit Sub
        End If

        Call UpdateBlueprintGrid(True)

        txtBPSearch.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub chkEnableMETE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEnableMETE.CheckedChanged
        Call EnableMETE(chkEnableMETE.Checked)
    End Sub

    Private Sub EnableMETE(Value As Boolean)
        ' if true it enables, false disables
        lblBPME.Enabled = Value
        lblBPTE.Enabled = Value
        txtBPME.Enabled = Value
        txtBPTE.Enabled = Value
    End Sub

#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FirstLoad = True
        DataEntered = False
        TabPressed = False

        ' Add the colums to the list
        lstBPs.Columns.Add("", 25, HorizontalAlignment.Center) ' -2 is autosize, but check boxes set to 25
        lstBPs.Columns.Add("BPTypeID", 0, HorizontalAlignment.Left) ' Hidden
        lstBPs.Columns.Add("Blueprint Group", 260, HorizontalAlignment.Left)
        lstBPs.Columns.Add("Blueprint Name", 300, HorizontalAlignment.Left)
        lstBPs.Columns.Add("Tech", 52, HorizontalAlignment.Center)
        lstBPs.Columns.Add("ME", 40, HorizontalAlignment.Center)
        lstBPs.Columns.Add("TE", 40, HorizontalAlignment.Center)
        lstBPs.Columns.Add("Owned", 50, HorizontalAlignment.Center)
        lstBPs.Columns.Add("BP Type", 78, HorizontalAlignment.Center)
        lstBPs.Columns.Add("Fav", 48, HorizontalAlignment.Center)
        lstBPs.Columns.Add("Ignored", 48, HorizontalAlignment.Center)
        lstBPs.Columns.Add("Runs", 55, HorizontalAlignment.Center)
        ' lstBPs.Columns.Add("Quantity", 51, HorizontalAlignment.Right) ' This is different than the API value
        lstBPs.Columns.Add("Additional Costs", 88, HorizontalAlignment.Right)
        lstBPs.Columns.Add("Scanned", 0, HorizontalAlignment.Center) ' Hidden

        Call SetScanEnableButtons()

        ttBPManage.SetToolTip(btnBackupBPs, "Exports all Blueprint Data to a csv format for all user APIs")
        ttBPManage.SetToolTip(btnLoadBPs, "Loads exported Blueprint Data from csv format")
        ttBPManage.SetToolTip(rbtnRemoveAllSettings, "Removes all saved blueprint data for selected blueprints")

    End Sub

    Private Sub SetScanEnableButtons()

        ' Tool Tip for Scan button
        If SelectedCharacter.AssetsAccess = False Then
            ttBPManage.SetToolTip(grpScanAssets, "Scanning Assets only available with correct key access")
            ' Disable
            btnScanPersonalBPs.Enabled = False
        Else
            ttBPManage.SetToolTip(btnScanPersonalBPs, "Assets refresh every 6 Hours")
            btnScanPersonalBPs.Enabled = True
        End If

        If SelectedCharacter.CharacterCorporation.AssetAccess = False Then
            ttBPManage.SetToolTip(grpScanAssets, "Scanning Assets only available with correct key access")
            ' Disable
            btnScanCorpBPs.Enabled = False
        Else
            ttBPManage.SetToolTip(btnScanCorpBPs, "Assets refresh every 6 Hours")
            btnScanCorpBPs.Enabled = True
        End If

    End Sub

    Private Sub frmBlueprintManagement_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Application.DoEvents()

        If FirstLoad Then
            Call InitForm()
        End If

        FirstLoad = False

    End Sub

    Private Sub InitForm()

        cmbBPTypeFilterLoaded = False
        cmbBPTypeFilter.Text = SelectTypeText
        FirstLoad = True

        ' Race Checks
        chkRaceAmarr.Checked = True
        chkRaceCaldari.Checked = True
        chkRaceGallente.Checked = True
        chkRaceMinmatar.Checked = True
        chkRacePirate.Checked = True
        chkRaceOther.Checked = True

        ' Set the checks
        chkBPNavyFaction.Checked = True
        chkBPPirateFaction.Checked = True
        chkBPStoryline.Checked = True
        chkBPT1.Checked = True
        chkBPT2.Checked = True
        chkBPT3.Checked = True

        txtBPME.Text = "0"
        txtBPTE.Text = "0"

        rbtnAllBPs.Checked = True
        rbtnAllBPTypes.Checked = True
        rbtnMarkasOwned.Checked = True

        rbtnShowAllBPtypes.Checked = True

        txtBPSearch.Text = ""

        ' Start out not checking this incase people accidently hit it
        chkEnableMETE.Checked = False
        Call EnableMETE(False)

        FirstLoad = False

        Call UpdateBlueprintGrid(False)

    End Sub

    ' Updates the blueprint grid with a list of bps
    Private Sub UpdateBlueprintGrid(ByVal CheckAllItems As Boolean)
        Dim SQL As String
        Dim SQL1 As String = ""
        Dim readerBP As SQLiteDataReader
        Dim rsLookup As SQLiteDataReader
        Dim ScannedValue As Integer
        Dim BPUserID As Long

        Dim BPList As ListViewItem

        ' Get the selection clause from options on screen
        SQL = BuildBPSelectQuery()

        If SQL = "" Then
            ' No valid query so just show nothing
            lstBPs.Items.Clear()
            Exit Sub
        End If

        ' Make sure to set the USER ID for the owned blueprint query
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)

        If rbtnScannedCorpBPs.Checked Then
            ' Set the correct ID
            BPUserID = SelectedCharacter.CharacterCorporation.CorporationID
        Else
            BPUserID = GetBPUserID(SelectedCharacter.ID)
        End If

        DBCommand.Parameters.AddWithValue("@USERBP_USERID", BPUserID) ' need to search for corp ID too
        DBCommand.Parameters.AddWithValue("@USERBP_CORPID", CStr(SelectedCharacter.CharacterCorporation.CorporationID))
        readerBP = DBCommand.ExecuteReader

        lstBPs.Visible = False
        Me.Cursor = Cursors.WaitCursor

        ' Disable buttons till done
        gbBPFilter.Enabled = False
        gbUpdateOptions.Enabled = False
        grpScanAssets.Enabled = False
        txtBPEdit.Visible = False
        cmbEdit.Visible = False

        Me.Refresh()
        Application.DoEvents()

        lstBPs.Items.Clear()
        ' Disable sorting because it will crawl after we update if there are too many records
        lstBPs.ListViewItemSorter = Nothing
        lstBPs.BeginUpdate()

        ' Add the records
        While readerBP.Read

            Application.DoEvents()

            BPList = New ListViewItem("")
            'The remaining columns are subitems  
            ' 0-BP_ID, 1-BLUEPRINT_GROUP, 2-BLUEPRINT_NAME, 3-ITEM_GROUP_ID, 4-ITEM_GROUP, 5-ITEM_CATEGORY_ID, 
            ' 6-ITEM_CATEGORY, 7-ITEM_ID, 8-ITEM_NAME, 9-ME, 10-TE, 11-USERID, 12-ITEM_TYPE, 13-RACE_ID, 14-OWNED, 15-SCANNED 
            ' 16-BP_TYPE, 17-UNIQUE_BP_ITEM_ID, 18-FAVORITE, 19-VOLUME, 20-MARKET_GROUP_ID, 21-ADDITIONAL_COSTS, 
            ' 22-LOCATION_ID, 23-QUANTITY, 24-FLAG_ID, 25-RUNS, 26-IGNORE, 27-TECH_LEVEL

            BPList.SubItems.Add(CStr(readerBP.GetInt64(0))) ' BP ID
            BPList.SubItems.Add(readerBP.GetString(1)) ' BP Group
            BPList.SubItems.Add(readerBP.GetString(2)) ' BP Name

            ' 1, 2, 14 are T1, T2, T3
            ' 3 is Storyline
            ' 15 is Pirate Faction
            ' 16 is Navy Faction
            Select Case readerBP.GetInt32(12)
                Case 1
                    BPList.SubItems.Add("T1")
                Case 2
                    BPList.SubItems.Add("T2")
                Case 14
                    BPList.SubItems.Add("T3")
                Case 3
                    BPList.SubItems.Add("Storyline")
                Case 15
                    BPList.SubItems.Add("Pirate")
                Case 16
                    BPList.SubItems.Add("Navy")
                Case Else
                    BPList.SubItems.Add(Unknown)
            End Select

            BPList.SubItems.Add(CStr(readerBP.GetDouble(9))) ' ME
            BPList.SubItems.Add(CStr(readerBP.GetDouble(10))) ' TE

            Call SetOwnedFlagandColors(BPList, CBool(readerBP.GetValue(14))) ' 14 = Owned

            ' BP Type
            BPList.SubItems.Add(GetBPTypeString(readerBP.GetInt32(16)))

            If readerBP.GetInt32(18) = 0 Then
                BPList.SubItems.Add(No) ' Favorite
            Else
                BPList.SubItems.Add(Yes) ' Favorite
            End If

            If readerBP.GetInt32(26) = 0 Then
                BPList.SubItems.Add(No) ' Ignored
            Else
                BPList.SubItems.Add(Yes) ' Ignored
            End If

            ' If they don't own it, look it up, else use the API data
            If CInt(readerBP.GetValue(14)) = 0 Then
                ' Set runs for bpcs, or unlimited for bpo
                If (readerBP.GetInt32(16) = BPType.Copy Or readerBP.GetInt32(16) = BPType.InventedBPC Or readerBP.GetInt32(16) = BPType.NotOwned) And readerBP.GetInt32(27) <> 1 Then
                    ' If T2/T3 invented - look up base runs for the bpc assuming no decryptors, else get the value stored
                    If readerBP.GetInt32(27) = 2 Then
                        SQL = "SELECT quantity FROM INDUSTRY_ACTIVITY_PRODUCTS WHERE productTypeID = " & CStr(readerBP.GetInt64(0)) & " AND activityID = 8"
                    Else ' T3
                        Dim BPIDLookup As Long
                        BPIDLookup = GetInventItemTypeID(readerBP.GetInt64(0), "Wrecked")
                        SQL = "SELECT quantity FROM INDUSTRY_ACTIVITY_PRODUCTS WHERE blueprintTypeID = " & CStr(BPIDLookup) & " AND activityID = 8 "
                        SQL = SQL & "AND productTypeID = " & CStr(readerBP.GetInt64(0))
                    End If

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLookup = DBCommand.ExecuteReader

                    If rsLookup.Read Then
                        BPList.SubItems.Add(CStr(rsLookup.GetInt32(0)))
                    Else
                        BPList.SubItems.Add(Unknown)
                    End If

                    rsLookup.Close()
                    rsLookup = Nothing
                ElseIf readerBP.GetInt32(16) = BPType.NotOwned Then
                    ' Get max runs from all_blueprints for unowned bps
                    SQL = "SELECT MAX_PRODUCTION_LIMIT FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID = " & CStr(readerBP.GetInt64(0))
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLookup = DBCommand.ExecuteReader

                    If rsLookup.Read Then
                        BPList.SubItems.Add(CStr(rsLookup.GetInt32(0)))
                    Else
                        BPList.SubItems.Add(Unknown)
                    End If

                    rsLookup.Close()
                    rsLookup = Nothing

                ElseIf CInt(readerBP.GetValue(16)) = BPType.Original Then
                    ' BPO's are unlimited runs
                    BPList.SubItems.Add(Unlimited)
                End If
            Else ' Get from API store for T1 BPCs
                If readerBP.GetInt32(25) = -1 Then
                    BPList.SubItems.Add(Unlimited)
                Else
                    BPList.SubItems.Add(CStr(readerBP.GetInt32(25)))
                End If
            End If

            ' For now, quantity is 1 until I can do multi-bp processing where we would group common ME/TE Bps
            'BPList.SubItems.Add("1")
            BPList.SubItems.Add(FormatNumber(readerBP.GetDouble(21), 2)) ' Additional Costs

            ScannedValue = readerBP.GetInt32(15)

            If ScannedValue = 0 Then
                BPList.BackColor = Color.White
            Else
                If SelectedCharacter.ID = readerBP.GetInt32(11) Or readerBP.GetInt32(11) = CommonLoadBPsID Then
                    BPList.BackColor = Color.BlanchedAlmond
                Else ' must be corp
                    BPList.BackColor = Color.LightGreen
                End If
            End If

            BPList.SubItems.Add(CStr(If(readerBP.IsDBNull(15), 0, readerBP.GetInt32(15)))) ' Scanned

            If CheckAllItems Then
                BPList.Checked = True
            Else
                BPList.Checked = False
            End If

            Call lstBPs.Items.Add(BPList)

        End While

        ' Now sort this
        Dim TempType As SortOrder
        If BPColumnSortOrder = SortOrder.Ascending Then
            TempType = SortOrder.Descending
        Else
            TempType = SortOrder.Ascending
        End If
        Call ListViewColumnSorter(BPColumnClicked, CType(lstBPs, ListView), BPColumnClicked, TempType)
        lstBPs.EndUpdate()

        If CheckAllItems Then
            btnSelectAll.Text = "Uncheck All"
        Else
            btnSelectAll.Text = "Select All"
        End If

        readerBP.Close()
        readerBP = Nothing
        DBCommand = Nothing

        lstBPs.Visible = True
        Me.Cursor = Cursors.Default
        gbBPFilter.Enabled = True
        gbUpdateOptions.Enabled = True
        grpScanAssets.Enabled = True
        SelectedType = "" ' Reset this on each load
        Application.DoEvents()

        Exit Sub

    End Sub

    ' Sets the referenced list view owned colors and back color
    Private Sub SetOwnedFlagandColors(ByRef ListSubitem As ListViewItem, ByVal OwnedBP As Boolean)
        If OwnedBP Then ' 14 = Owned
            ListSubitem.SubItems.Add(Yes)
            ListSubitem.ForeColor = Color.Blue
        Else
            ListSubitem.SubItems.Add(No)
            ListSubitem.ForeColor = Color.Black
            ListSubitem.BackColor = Color.White
        End If
    End Sub

    ' Builds the query for the select list
    Private Function BuildBPSelectQuery() As String
        Dim SQL As String = ""
        Dim ScannedAdd As String = ""
        Dim WhereClause As String = ""
        Dim RaceClause As String = ""
        Dim TempRace As String = ""
        Dim SQLItemType As String = ""
        Dim TextClause As String = ""
        Dim ComboType As String = ""
        Dim Copies As String = ""
        Dim SizesClause As String = ""

        ' Base query
        SQL = "SELECT * FROM " & USER_BLUEPRINTS

        ' Find what type of blueprint we want
        With Me
            If .rbtnAmmoChargeBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Charge' "
            ElseIf .rbtnDroneBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY in ('Drone', 'Fighter') "
            ElseIf .rbtnModuleBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE (ITEM_CATEGORY ='Module' AND ITEM_GROUP NOT LIKE 'Rig%') "
            ElseIf .rbtnShipBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Ship' "
            ElseIf .rbtnSubsystemBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Subsystem' "
            ElseIf .rbtnBoosterBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Implant' "
            ElseIf .rbtnComponentBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE (ITEM_GROUP LIKE '%Components%' AND ITEM_GROUP <> 'Station Components') "
            ElseIf .rbtnMiscBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_GROUP IN ('Tool','Data Interfaces','Cyberimplant','Fuel Block') "
            ElseIf .rbtnDeployableBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Deployable' "
            ElseIf .rbtnCelestialsBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY IN ('Celestial','Orbitals','Sovereignty Structures', 'Station', 'Accessories', 'Infrastructure Upgrades') "
            ElseIf .rbtnStructureBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE (ITEM_CATEGORY IN ('Starbase','Structure') OR ITEM_GROUP = 'Station Components')"
            ElseIf .rbtnStructureRigsBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Structure Rigs' "
            ElseIf .rbtnStructureModulesBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE (ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP NOT LIKE '%Rig%') "
            ElseIf .rbtnReactionBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE BLUEPRINT_GROUP LIKE '%Reaction Formulas' "
            ElseIf .rbtnRigBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE (BLUEPRINT_GROUP = 'Rig Blueprint' OR (ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP LIKE '%Rig%'))"
            End If
        End With

        Dim TempClause As String = ""

        If rbtnOwned.Checked Then
            ' Ignore scanned BP's
            TempClause = "OWNED <> 0 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & "AND " & TempClause
            End If
        ElseIf rbtnScannedPersonalBPs.Checked Then
            Dim CharID As Long = 0
            If UserApplicationSettings.LoadBPsbyChar Then
                ' Use the ID sent
                CharID = SelectedCharacter.ID
            Else
                CharID = CommonLoadBPsID
            End If
            TempClause = "USER_ID = " & CharID & " AND SCANNED <> 0 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & "AND " & TempClause
            End If
        ElseIf rbtnScannedCorpBPs.Checked Then
            ' Include corp scanned
            TempClause = "USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & " AND SCANNED <> 0 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & " AND " & TempClause
            End If
        ElseIf rbtnFavorites.Checked Then
            ' Favorites for the user
            TempClause = "FAVORITE = 1 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & "AND " & TempClause
            End If
        ElseIf rbtnIgnored.Checked Then
            ' All ignored
            TempClause = "IGNORE = 1 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & "AND " & TempClause
            End If
        End If

        ' Add not owned if checked
        If chkNotOwned.Checked Then
            TempClause = "OWNED = 0 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & "AND " & TempClause
            End If
        End If

        ' Add not Ignored if checked
        If chkNotIgnored.Checked Then
            TempClause = "IGNORE = 0 "
            If WhereClause = "" Then
                WhereClause = "WHERE " & TempClause
            Else
                WhereClause = WhereClause & "AND " & TempClause
            End If
        End If

        ' Item Type Definitions - These are set by me based on existing data
        ' 1, 2, 14 are T1, T2, T3
        ' 3 is Storyline
        ' 15 is Pirate Faction
        ' 16 is Navy Faction

        ' Check Tech version
        If chkBPT1.Enabled Then
            ' Only a Subsystem so T3
            If chkBPT1.Checked Then
                SQLItemType = SQLItemType & "1,"
            End If
        End If

        If chkBPT2.Enabled Then
            If chkBPT2.Checked Then
                SQLItemType = SQLItemType & "2,"
            End If
        End If

        If chkBPT3.Enabled Then
            If chkBPT3.Checked Then
                SQLItemType = SQLItemType & "14,"
            End If
        End If

        If chkBPStoryline.Enabled Then
            If chkBPStoryline.Checked Then
                SQLItemType = SQLItemType & "3,"
            End If
        End If

        If chkBPPirateFaction.Enabled Then
            If chkBPPirateFaction.Checked Then
                SQLItemType = SQLItemType & "15,"
            End If
        End If

        If chkBPNavyFaction.Enabled Then
            If chkBPNavyFaction.Checked Then
                SQLItemType = SQLItemType & "16,"
            End If
        End If

        ' Add Item Type
        If SQLItemType <> "" Then
            SQLItemType = "ITEM_TYPE In (" & SQLItemType.Substring(0, SQLItemType.Length - 1) & ") "
        Else
            ' They need to have at least one. If not, just return nothing
            BuildBPSelectQuery = ""
            Exit Function
        End If

        ' Determine what race we are looking at
        If chkRaceAmarr.Checked Then
            TempRace = TempRace & "4,"
        End If
        If chkRaceCaldari.Checked Then
            TempRace = TempRace & "1,"
        End If
        If chkRaceMinmatar.Checked Then
            TempRace = TempRace & "2,"
        End If
        If chkRaceGallente.Checked Then
            TempRace = TempRace & "8,"
        End If
        If chkRacePirate.Checked Then
            TempRace = TempRace & "15,"
        End If
        If chkRaceOther.Checked Then
            TempRace = TempRace & "0,"
        End If

        If TempRace <> "" Then
            TempRace = "(" & TempRace.Substring(0, Len(TempRace) - 1) & ")"
            RaceClause = "And (RACE_ID In " & TempRace & ") "
        Else
            ' They need to have at least one. If not, just return nothing
            BuildBPSelectQuery = ""
            Exit Function
        End If

        ' Finally add on text if they added it
        If Trim(txtBPSearch.Text) <> "" Then
            TextClause = TextClause & "And " & GetSearchText(txtBPSearch.Text, "BLUEPRINT_NAME", "BLUEPRINT_GROUP")
        End If

        ' If they select a type of item, set that
        If Trim(cmbBPTypeFilter.Text) <> SelectTypeText Then
            ComboType = "And ITEM_GROUP ='" & FormatDBString(Trim(cmbBPTypeFilter.Text)) & "' "
        End If

        ' See if they want BPOs, Copies, or Invented BPCs
        If rbtnOnlyBPO.Checked Then
            Copies = " AND BP_TYPE IN (" & CStr(BPType.Original) & ",0) " ' Only BPO's
        ElseIf rbtnOnlyCopies.Checked Then
            Copies = " AND BP_TYPE IN (" & CStr(BPType.Copy) & ",0) " ' Only Copies
        ElseIf rbtnOnlyInventedBPCs.Checked Then
            Copies = " AND BP_TYPE IN (" & CStr(BPType.InventedBPC) & ",0) " ' Only invented bpcs
        End If

        SizesClause = ""

        ' Finally add the sizes
        If chkBPSmall.Checked Then ' Light
            SizesClause = SizesClause & "'S',"
        End If

        If chkBPMedium.Checked Then ' Medium
            SizesClause = SizesClause & "'M',"
        End If

        If chkBPLarge.Checked Then ' Heavy
            SizesClause = SizesClause & "'L',"
        End If

        If chkBPXL.Checked Then ' Fighters
            SizesClause = SizesClause & "'XL',"
        End If

        If SizesClause <> "" Then
            SizesClause = " AND SIZE_GROUP IN (" & SizesClause.Substring(0, Len(SizesClause) - 1) & ") "
        End If

        ' No where clause means we are selecting all
        If WhereClause <> "" Then
            SQL = SQL & WhereClause & "AND " & SQLItemType & RaceClause & TextClause & ComboType & Copies & SizesClause & " ORDER BY BLUEPRINT_GROUP, BLUEPRINT_NAME"
        Else
            SQL = SQL & "WHERE " & SQLItemType & RaceClause & TextClause & ComboType & Copies & SizesClause & " ORDER BY BLUEPRINT_GROUP, BLUEPRINT_NAME"
        End If

        BuildBPSelectQuery = SQL

    End Function

    ' Builds the where clause for the calc screen based on Tech and Group selections
    Private Function BuildWhereClause() As String
        Dim WhereClause As String = ""
        Dim SQLItemType As String = ""
        Dim SQL As String = ""
        Dim Copies As String = ""
        Dim TempRace As String = ""
        Dim RaceClause As String = ""
        Dim SizesClause As String = ""

        With Me
            If .rbtnAmmoChargeBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY = 'Charge' "
            ElseIf .rbtnDroneBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY in ('Drone', 'Fighter') "
            ElseIf .rbtnModuleBlueprints.Checked Then
                WhereClause = WhereClause & "(ITEM_CATEGORY ='Module' ITEM_GROUP NOT LIKE 'Rig%') "
            ElseIf .rbtnShipBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY = 'Ship' "
            ElseIf .rbtnSubsystemBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY = 'Subsystem' "
            ElseIf .rbtnBoosterBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY = 'Implant' "
            ElseIf .rbtnComponentBlueprints.Checked Then
                WhereClause = WhereClause & "(ITEM_GROUP LIKE '%Components%' ITEM_GROUP <> 'Station Components') "
            ElseIf .rbtnMiscBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_GROUP IN ('Tool','Data Interfaces','Cyberimplant','Fuel Block') "
            ElseIf .rbtnDeployableBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY = 'Deployable' "
            ElseIf .rbtnCelestialsBlueprints.Checked Then
                WhereClause = WhereClause & "ITEM_CATEGORY IN ('Celestial','Orbitals','Sovereignty Structures', 'Station', 'Accessories', 'Infrastructure Upgrades') "
            ElseIf .rbtnStructureBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE (ITEM_CATEGORY IN ('Starbase','Structure') OR ITEM_GROUP = 'Station Components')"
            ElseIf .rbtnStructureRigsBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE ITEM_CATEGORY = 'Stucture Rigs' "
            ElseIf .rbtnReactionBlueprints.Checked Then
                WhereClause = WhereClause & "WHERE BLUEPRINT_GROUP LIKE '%Reaction Formulas' "
            ElseIf .rbtnStructureModulesBlueprints.Checked Then
                WhereClause = WhereClause & "(ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP NOT LIKE '%Rig%') "
            ElseIf .rbtnRigBlueprints.Checked Then
                WhereClause = WhereClause & "(BLUEPRINT_GROUP = 'Rig Blueprint' OR (ITEM_CATEGORY = 'Structure Module' AND ITEM_GROUP LIKE '%Rig%'))"
            End If
        End With

        ' Item Type Definitions - These are set by me based on existing data
        ' 1, 2, 14 are T1, T2, T3
        ' 3 is Storyline
        ' 15 is Pirate Faction
        ' 16 is Navy Faction

        ' Check Tech version
        If chkBPT1.Enabled Then
            ' Only a Subsystem so T3
            If chkBPT1.Checked Then
                SQLItemType = SQLItemType & "1,"
            End If
        End If

        If chkBPT2.Enabled Then
            If chkBPT2.Checked Then
                SQLItemType = SQLItemType & "2,"
            End If
        End If

        If chkBPT3.Enabled Then
            If chkBPT3.Checked Then
                SQLItemType = SQLItemType & "14,"
            End If
        End If

        If chkBPStoryline.Enabled Then
            If chkBPStoryline.Checked Then
                SQLItemType = SQLItemType & "3,"
            End If
        End If

        If chkBPPirateFaction.Enabled Then
            If chkBPPirateFaction.Checked Then
                SQLItemType = SQLItemType & "15,"
            End If
        End If

        If chkBPNavyFaction.Enabled Then
            If chkBPNavyFaction.Checked Then
                SQLItemType = SQLItemType & "16,"
            End If
        End If

        ' Add Item Type
        If SQLItemType <> "" Then
            SQLItemType = "ITEM_TYPE IN (" & SQLItemType.Substring(0, SQLItemType.Length - 1) & ")"
        Else
            Return ""
        End If

        ' Adjust where clause
        If WhereClause <> "" And SQLItemType <> "" Then
            WhereClause = "WHERE (" & WhereClause & ") AND (" & SQLItemType & ")"
        ElseIf SQLItemType <> "" Then
            ' They want all items with these techs
            WhereClause = "WHERE (" & SQLItemType & ")"
        End If

        ' Determine what race we are looking at
        If chkRaceAmarr.Checked Then
            TempRace = TempRace & "4,"
        End If
        If chkRaceCaldari.Checked Then
            TempRace = TempRace & "1,"
        End If
        If chkRaceMinmatar.Checked Then
            TempRace = TempRace & "2,"
        End If
        If chkRaceGallente.Checked Then
            TempRace = TempRace & "8,"
        End If
        If chkRacePirate.Checked Then
            TempRace = TempRace & "15,"
        End If
        If chkRaceOther.Checked Then
            TempRace = TempRace & "0,"
        End If

        If TempRace <> "" Then
            TempRace = "(" & TempRace.Substring(0, Len(TempRace) - 1) & ")"
            RaceClause = "AND (RACE_ID IN " & TempRace & ") "
        Else
            ' They need to have at least one. If not, just return nothing
            Return ""
        End If

        ' See if they want copies
        If rbtnOnlyBPO.Checked Then
            Copies = " AND BP_TYPE <> " & CStr(BPType.Copy) & " "  ' Only BPO's or Unknown
        ElseIf rbtnOnlyCopies.Checked Then
            Copies = " AND BP_TYPE = " & CStr(BPType.Copy) & " " ' Only Copies
        End If

        ' Finally add the sizes
        If chkBPSmall.Checked Then ' Light
            SizesClause = SizesClause & "'S',"
        End If

        If chkBPMedium.Checked Then ' Medium
            SizesClause = SizesClause & "'M',"
        End If

        If chkBPLarge.Checked Then ' Heavy
            SizesClause = SizesClause & "'L',"
        End If

        If chkBPXL.Checked Then ' Fighters
            SizesClause = SizesClause & "'XL',"
        End If

        If SizesClause <> "" Then
            SizesClause = " AND SIZE_GROUP IN (" & SizesClause.Substring(0, Len(SizesClause) - 1) & ") "
        End If

        Return WhereClause & Copies & RaceClause & SizesClause

    End Function

    ' Loads the cmbBPTypeFilter object with types based on the radio button selected - Ie, Drones will load Drone types (Small, Medium, Heavy...etc)
    Private Sub LoadBPTypes()
        Dim SQL As String
        Dim WhereClause As String = ""
        Dim readerTypes As SQLiteDataReader
        Dim BPUserID As Long

        cmbBPTypeFilter.Text = SelectTypeText
        SQL = "SELECT ITEM_GROUP FROM " & USER_BLUEPRINTS

        WhereClause = BuildWhereClause()

        If WhereClause = "" Then
            ' They didn't select anything, just clear and exit
            cmbBPTypeFilter.Items.Clear()
            cmbBPTypeFilter.Text = SelectTypeText
            Exit Sub
        End If

        ' See if we are looking at User Owned blueprints or All
        If rbtnOwned.Checked Then
            ' Ignore scanned BP's
            If WhereClause = "" Then
                WhereClause = "WHERE (USER_ID =" & SelectedCharacter.ID & "OR USER_ID = " & SelectedCharacter.CharacterCorporation.CorporationID & ") AND OWNED <> 0  "
            Else
                WhereClause = WhereClause & " AND USER_ID =" & SelectedCharacter.ID & " AND OWNED <> 0  "
            End If

            ' Set the correct ID
            BPUserID = SelectedCharacter.ID

            ElseIf rbtnScannedPersonalBPs.Checked Then
            ' Include all BP's
            If WhereClause = "" Then
                WhereClause = "WHERE USER_ID = " & SelectedCharacter.ID & " AND SCANNED <> 0 "
            Else
                WhereClause = WhereClause & " AND USER_ID = " & SelectedCharacter.ID & " AND SCANNED <> 0 "
            End If

            ' Set the correct ID
            BPUserID = SelectedCharacter.ID

        ElseIf rbtnScannedCorpBPs.Checked Then
            ' Include corp scanned
            If WhereClause = "" Then
                WhereClause = "WHERE USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & " AND SCANNED <> 0 "
            Else
                WhereClause = WhereClause & " AND USER_ID =" & SelectedCharacter.CharacterCorporation.CorporationID & " AND SCANNED <> 0 "
            End If

            ' Set the correct ID
            BPUserID = SelectedCharacter.CharacterCorporation.CorporationID

        End If

        SQL = SQL & WhereClause & " AND IGNORE = 0 GROUP BY ITEM_GROUP"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)

        DBCommand.Parameters.AddWithValue("@USERBP_USERID", GetBPUserID(BPUserID)) ' need to search for corp ID too
        DBCommand.Parameters.AddWithValue("@USERBP_CORPID", CStr(SelectedCharacter.CharacterCorporation.CorporationID))
        readerTypes = DBCommand.ExecuteReader

        cmbBPTypeFilter.Items.Clear()
        ' Add Select Type to top
        cmbBPTypeFilter.Items.Add(SelectTypeText)
        ' Add rest
        While readerTypes.Read
            cmbBPTypeFilter.Items.Add(readerTypes.GetString(0))
        End While

        readerTypes.Close()
        readerTypes = Nothing
        DBCommand = Nothing

    End Sub

    ' Checks the ME and TE boxes to make sure they are ok and errors if not
    Private Function CorrectMETE() As Boolean

        If Not IsNumeric(txtBPME.Text) Or Trim(txtBPME.Text) = "" Then
            MsgBox("Invalid ME Value", vbExclamation, Application.ProductName)
            Return False
        End If

        If Not IsNumeric(txtBPTE.Text) Or Trim(txtBPTE.Text) = "" Then
            MsgBox("Invalid TE Value", vbExclamation, Application.ProductName)
            Return False
        End If

        If Val(txtBPME.Text) > 10 Then
            MsgBox("ME value cannot be greater than 10", vbExclamation, Application.ProductName)
            Return False
        End If

        If Val(txtBPTE.Text) > 20 Then
            MsgBox("TE value cannot be greater than 20", vbExclamation, Application.ProductName)
            Return False
        End If

        If Val(txtBPME.Text) < 0 Then
            MsgBox("ME value cannot be less than zero", vbExclamation, Application.ProductName)
            Return False
        End If

        If Val(txtBPTE.Text) < 0 Then
            MsgBox("TE value cannot be less than zero", vbExclamation, Application.ProductName)
            Return False
        End If

        Return True

    End Function

    ' Will use CAK and scan for bps in the user's items and store a temp table of these bps for loading in the grid
    Private Sub btnScanPersonalBPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanPersonalBPs.Click

        If SelectedCharacter.BlueprintsAccess Then
            Application.UseWaitCursor = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Call SelectedCharacter.GetBlueprints.LoadBlueprints(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, ScanType.Personal, True)
            MsgBox("Blueprints Loaded", vbInformation, Application.ProductName)
            rbtnScannedPersonalBPs.Checked = True ' Auto load
            Cursor = Cursors.Default
            Application.UseWaitCursor = False
            Me.Refresh()
            Application.DoEvents()
        Else
            MsgBox("You do not have the scope: " & ESI.ESICharacterBlueprintsScope & " registered for this application. Please update your developer scopes.", vbExclamation, Application.ProductName)
        End If

    End Sub

    ' Will use CAK and scan for bps in the corps items and store a temp table of these bps for loading in the grid
    Private Sub btnScanCorpBPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanCorpBPs.Click

        If SelectedCharacter.CharacterCorporation.BlueprintsAccess Then
            Application.UseWaitCursor = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Call SelectedCharacter.CharacterCorporation.GetBlueprints.LoadBlueprints(SelectedCharacter.CharacterCorporation.CorporationID, SelectedCharacter.CharacterTokenData, ScanType.Corporation, True)
            MsgBox("Blueprints Loaded", vbInformation, Application.ProductName)
            rbtnScannedCorpBPs.Checked = True ' Auto load
            Me.Cursor = Cursors.Default
            Application.UseWaitCursor = False
            Me.Refresh()
            Application.DoEvents()
        Else
            MsgBox("You do not have the scope: " & ESI.ESICorporationBlueprintsScope & " registered for this application. Please 
your developer scopes.", vbExclamation, Application.ProductName)
        End If

    End Sub

    ' Clears all the checked items in grid
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Call InitForm()
    End Sub

    ' Checks all the items in the grid
    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Dim item As ListViewItem
        Dim items As ListView.ListViewItemCollection

        ' Change to toggle "Select All" or "Uncheck All"
        If btnSelectAll.Text = "Select All" Then
            btnSelectAll.Text = "Uncheck All"
            items = lstBPs.Items
            lstBPs.BeginUpdate()
            For Each item In items
                item.Checked = True
            Next
            lstBPs.EndUpdate()
        Else
            btnSelectAll.Text = "Select All"
            items = lstBPs.Items
            lstBPs.BeginUpdate()
            For Each item In items
                item.Checked = False
            Next
            lstBPs.EndUpdate()
        End If
    End Sub

    ' Updates selected items in the grid
    Private Sub btnUpdateSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSelected.Click
        Dim item As ListViewItem
        Dim checkedItems As ListView.CheckedListViewItemCollection
        Dim TempBPType As BPType

        Dim TempME As Integer
        Dim TempTE As Integer

        ' Make sure the ME/TE boxes are good
        If Not CorrectMETE() Then
            Exit Sub
        End If

        ' Make sure they actually have something selected
        If lstBPs.CheckedItems.Count = 0 Then
            MsgBox("No Blueprints are Checked", vbExclamation, Me.Text)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        ' Just work with the ones that are checked
        checkedItems = lstBPs.CheckedItems

        ' Update each item based on inputs
        For Each item In checkedItems

            ' Select the BP type of using update selected and marking owned
            If rbtnMarkasOwned.Checked And item.SubItems(7).Text <> Yes Then
                ' Save all BPs as copies if they aren't already owned - they can update if they want to, all items can have bpcs and it doesn't affect processing so this is easier
                TempBPType = BPType.Copy
            ElseIf rbtnMarkasUnowned.Checked Then
                TempBPType = BPType.NotOwned
            Else
                ' Set it to what they had in the grid
                Select Case item.SubItems(8).Text
                    Case BPO
                        TempBPType = BPType.Original
                    Case BPC
                        TempBPType = BPType.Copy
                    Case InventedBPC
                        TempBPType = BPType.InventedBPC
                    Case Unknown
                        TempBPType = BPType.NotOwned
                End Select
            End If

            ' Need to add selected blueprints to the character blueprints table, and set the ME and TE's as given or as stored
            If chkEnableMETE.Checked = False Or chkEnableMETE.Enabled = False Then
                ' Need to use the values in the grid, not the text boxes
                TempME = CInt(item.SubItems(5).Text)
                TempTE = CInt(item.SubItems(6).Text)
            Else
                ' Use what they put in text
                TempME = CInt(txtBPME.Text)
                TempTE = CInt(txtBPTE.Text)
            End If

            Call UpdateBPinDB(CInt(CDbl(item.SubItems(1).Text)), TempME, TempTE, TempBPType,
                              CInt(item.SubItems(5).Text), CInt(item.SubItems(6).Text), chkMarkasFavorite.Checked,
                              chkMarkasIgnored.Checked, 0, rbtnRemoveAllSettings.Checked)

        Next

        ' Refresh grid
        Call UpdateBlueprintGrid(False)

        txtBPME.Text = "0"
        txtBPTE.Text = "0"

        ' Done
        rbtnMarkasOwned.Checked = True
        Me.Cursor = Cursors.Default

        MsgBox("Blueprints Updated", vbInformation, Me.Text)

    End Sub

    ' Deletes all blueprints that are owned or scanned
    Private Sub btnResetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetAll.Click
        Dim Response As MsgBoxResult

        Response = MsgBox("This will reset all blueprints for this character" & Environment.NewLine & "deleting all scanned data and stored ME/TE values." & Environment.NewLine & Environment.NewLine & "Are you sure you want to do this?", vbYesNo, Application.ProductName)

        If Response = vbYes Then
            Call ResetAllBPData()

            Call UpdateBlueprintGrid(True)
        End If

    End Sub

    ' Output full list of BPs, order by BP name, in CSV
    Private Sub btnBackupBPs_Click(sender As System.Object, e As System.EventArgs) Handles btnBackupBPs.Click
        Dim MyStream As StreamWriter
        Dim FileName As String

        ' Save file name with date
        FileName = "Blueprints Backup - " & Format(Now, "MMddyyyy") & " - " & SelectedCharacter.Name & ".csv"

        ' Show the dialog
        SaveFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
        SaveFileDialog.FilterIndex = 1
        SaveFileDialog.RestoreDirectory = True
        SaveFileDialog.FileName = FileName

        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                MyStream = File.CreateText(SaveFileDialog.FileName)

                If Not (MyStream Is Nothing) Then
                    ' Pull all the blueprints, including not owned and output data for character and the character's corp
                    Call WriteDatatoFile(MyStream, SelectedCharacter.ID, True, False)
                    Call WriteDatatoFile(MyStream, SelectedCharacter.CharacterCorporation.CorporationID, False, True)
                    MyStream.Flush()
                    MyStream.Close()

                    MsgBox("Blueprints Backed Up", vbInformation, Application.ProductName)

                End If
            Catch
                MsgBox(Err.Description, vbExclamation, Application.ProductName)
            End Try
        End If

    End Sub

    Private Sub WriteDatatoFile(ByRef MyStream As StreamWriter, API_ID As Long, PrintHeader As Boolean, IgnoreNonOwned As Boolean)
        Dim OutputText As String

        Dim SQL As String
        Dim readerBP As SQLiteDataReader

        ' Pull all the blueprints, including not owned and output data
        SQL = "SELECT * FROM " & USER_BLUEPRINTS & " ORDER BY BLUEPRINT_GROUP, BLUEPRINT_NAME"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)

        DBCommand.Parameters.AddWithValue("@USERBP_USERID", GetBPUserID(API_ID)) ' need to search for corp ID too
        DBCommand.Parameters.AddWithValue("@USERBP_CORPID", CStr(SelectedCharacter.CharacterCorporation.CorporationID))
        readerBP = DBCommand.ExecuteReader

        ' 0-BP_ID, 1-BLUEPRINT_GROUP, 2-BLUEPRINT_NAME, 3-ITEM_GROUP_ID, 4-ITEM_GROUP, 5-ITEM_CATEGORY_ID, 
        ' 6-ITEM_CATEGORY, 7-ITEM_ID, 8-ITEM_NAME, 9-ME, 10-TE, 11-USERID, 12-ITEM_TYPE, 13-RACE_ID, 14-OWNED, 15-SCANNED 
        ' 16-BP_TYPE, 17-UNIQUE_BP_ITEM_ID, 18-FAVORITE, 19-VOLUME, 20-MARKET_GROUP_ID, 21-ADDITIONAL_COSTS, 
        ' 22-LOCATION_ID, 23-QUANTITY, 24-FLAG_ID, 25-RUNS, 26-IGNORE, 27-TECH_LEVEL

        ' Output the columns first
        If PrintHeader Then
            OutputText = "API ID, Location ID, Item ID, Blueprint ID, Blueprint Group, Blueprint Name, "
            OutputText = OutputText & "Quantity, Flag ID, ME, TE, Runs, BP Type, Owned, Scanned, Favorite, Additional Costs" & Environment.NewLine
            MyStream.Write(OutputText)
        End If

        While readerBP.Read
            With readerBP
                If (IgnoreNonOwned And .GetInt64(14) <> 0) Or Not IgnoreNonOwned Then
                    OutputText = CLng(.GetValue(11)) & "," ' ID
                    OutputText = OutputText & CLng(.GetValue(22)) & "," ' Location Id
                    OutputText = OutputText & CLng(.GetValue(7)) & "," ' Item Id
                    OutputText = OutputText & CLng(.GetValue(0)) & "," ' BP ID
                    OutputText = OutputText & .GetString(1) & "," ' BP Group
                    OutputText = OutputText & .GetString(2) & "," ' BP Name
                    OutputText = OutputText & CLng(.GetValue(23)) & "," ' Quantity
                    OutputText = OutputText & CLng(.GetValue(24)) & "," ' Flag ID
                    OutputText = OutputText & CInt(.GetValue(9)) & "," ' ME
                    OutputText = OutputText & CInt(.GetValue(10)) & "," ' TE
                    OutputText = OutputText & CInt(.GetValue(25)) & "," ' Runs
                    OutputText = OutputText & GetBPTypeString(.GetInt32(16)) & "," ' BP Type
                    If Math.Abs(CInt(.GetValue(14))) = 1 Then ' Owned
                        OutputText = OutputText & "True,"
                    Else
                        OutputText = OutputText & "False,"
                    End If

                    If CInt(.GetValue(15)) = 1 Or CInt(.GetValue(15)) = 2 Then ' Scanned
                        OutputText = OutputText & "True,"
                    Else
                        OutputText = OutputText & "False,"
                    End If

                    ' Favorites
                    If CInt(.GetValue(18)) = 1 Then
                        OutputText = OutputText & "True,"
                    Else
                        OutputText = OutputText & "False,"
                    End If
                    OutputText = OutputText & CDbl(.GetValue(21))  ' Additional Costs

                    MyStream.Write(OutputText & Environment.NewLine)
                End If
            End With
        End While

    End Sub

    ' Loads BPs from a full list
    Private Sub btnLoadBPs_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadBPs.Click
        Dim SQL As String
        Dim BPStream As StreamReader = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        Dim Line As String
        Dim ParsedLine As String()

        'openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
        openFileDialog1.FileName = "*.csv"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Call EVEDB.BeginSQLiteTransaction()
            Try
                BPStream = New StreamReader(openFileDialog1.FileName)

                If (BPStream IsNot Nothing) Then
                    ' Read the file line by line here, start with headers
                    Line = BPStream.ReadLine
                    Line = BPStream.ReadLine ' First line of data

                    If Line IsNot Nothing Then
                        ' Start the session and delete all the records out of the table for this user
                        ' See what ID we use for character bps
                        Dim TempID As Long = 0
                        If UserApplicationSettings.LoadBPsbyChar Then
                            ' Use the ID sent
                            TempID = SelectedCharacter.ID
                        Else
                            TempID = CommonLoadBPsID
                        End If
                        Call EVEDB.ExecuteNonQuerySQL("DELETE FROM OWNED_BLUEPRINTS WHERE USER_ID IN (" & TempID & "," & SelectedCharacter.CharacterCorporation.CorporationID & ")")
                    Else
                        ' Leave loop
                        Exit Try
                    End If

                    Application.UseWaitCursor = True

                    While Line IsNot Nothing
                        lstBPs.Enabled = False
                        gbBPFilter.Enabled = False
                        gbUpdateOptions.Enabled = False
                        Application.DoEvents()
                        ' Format is: 0-API ID, 1-Location ID, 2-Item ID, 3-Blueprint ID, 4-Blueprint Group, 5-Blueprint Name, 
                        ' 6-Quantity, 7-Flag ID, 8-ME, 9-TE, 10-Runs, 11-BP Type, 12-Owned, 13-Scanned, 14-Favorite, 15-Additional Costs

                        ' Parse it
                        ParsedLine = Line.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)

                        ' Only load BP's that are marked as something other than 'Unowned'
                        If ParsedLine(11) <> UnownedBP Then
                            SQL = "INSERT INTO OWNED_BLUEPRINTS VALUES ("
                            ' See what ID we use for character bps
                            Dim TempID As Long = 0
                            If UserApplicationSettings.LoadBPsbyChar Then
                                ' Use the ID sent
                                SQL = SQL & ParsedLine(0) & "," ' API ID
                            Else
                                SQL = SQL & CommonLoadBPsID & "," ' API ID
                            End If

                            SQL = SQL & ParsedLine(1) & "," ' Location ID
                            SQL = SQL & ParsedLine(2) & "," ' Item ID
                            SQL = SQL & ParsedLine(3) & "," ' Blueprint ID
                            SQL = SQL & "'" & FormatDBString(ParsedLine(5)) & "'," ' Blueprint Name
                            SQL = SQL & ParsedLine(6) & "," ' Quantity
                            SQL = SQL & ParsedLine(7) & "," ' Flag ID
                            SQL = SQL & ParsedLine(8) & "," ' ME
                            SQL = SQL & ParsedLine(9) & "," ' TE
                            SQL = SQL & ParsedLine(10) & "," ' Runs

                            Dim TempBPType As BPType
                            Dim TempOwned As Boolean
                            Dim TempScanned As Boolean

                            ' BP Type 
                            TempBPType = GetBPType(ParsedLine(11))
                            ' Owned
                            TempOwned = CBool(UCase(ParsedLine(12)))
                            ' Scanned
                            TempScanned = CBool(UCase(ParsedLine(13)))

                            If Not TempOwned Then
                                ' Set the bp type regardless
                                TempBPType = BPType.NotOwned
                            End If

                            ' BP Type SQL
                            SQL = SQL & CStr(TempBPType) & ","
                            ' Owned SQL
                            If TempOwned Then
                                If TempScanned Then
                                    SQL = SQL & "1,"
                                Else
                                    SQL = SQL & "-1,"
                                End If
                            Else
                                SQL = SQL & "0,"
                            End If

                            ' Scanned SQL
                            If TempScanned Then
                                If CLng(ParsedLine(0)) = SelectedCharacter.CharacterCorporation.CorporationID Then
                                    SQL = SQL & "2," ' Corp BP
                                Else
                                    SQL = SQL & "1,"
                                End If
                            Else
                                SQL = SQL & "0,"
                            End If

                            ' Favorite
                            If UCase(ParsedLine(14)) = "TRUE" Then
                                SQL = SQL & "1,"
                            Else
                                SQL = SQL & "0,"
                            End If

                            SQL = SQL & ParsedLine(15) ' Additional Costs
                            SQL = SQL & ")"

                            ' Insert the record
                            Call EVEDB.ExecuteNonQuerySQL(SQL)

                        End If

                        Line = BPStream.ReadLine ' Read next line

                    End While

                    Call EVEDB.CommitSQLiteTransaction()

                    Application.UseWaitCursor = False
                    MsgBox("Blueprints Loaded", vbInformation, Application.ProductName)

                End If
            Catch Ex As Exception
                Application.UseWaitCursor = False
                Call EVEDB.RollbackSQLiteTransaction()
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (BPStream IsNot Nothing) Then
                    BPStream.Close()
                End If
            End Try
        End If

        Application.UseWaitCursor = False
        lstBPs.Enabled = True
        gbBPFilter.Enabled = True
        gbUpdateOptions.Enabled = True

        Call UpdateBlueprintGrid(True)
        Application.DoEvents()

    End Sub

#Region "InlineListUpdates"

    Private Sub lstBPs_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstBPs.MouseClick
        Dim iSubIndex As Integer = 0

        ' Hide the text box when a new line is selected
        txtBPEdit.Hide()

        CurrentRow = lstBPs.GetItemAt(e.X, e.Y)     ' which listviewitem was clicked

        If CurrentRow Is Nothing Then
            Exit Sub
        End If

        CurrentCell = CurrentRow.GetSubItemAt(e.X, e.Y)  ' which subitem was clicked

        ' Determine where the previous and next item boxes will be based on what they clicked - used in tab event handling
        Call SetNextandPreviousCells()

        ' See which column has been clicked
        iSubIndex = CurrentRow.SubItems.IndexOf(CurrentCell)

        ' Set the columns that can be edited, just ME and TE
        If iSubIndex = 5 Or iSubIndex = 6 Then

            If iSubIndex = 5 Then
                MEUpdate = True
            Else
                MEUpdate = False
            End If

            If iSubIndex = 6 Then
                TEUpdate = True
            Else
                TEUpdate = False
            End If

            FavoriteUpdate = False
            IgnoredBPUpdate = False
            OwnedTypeUpdate = False

            Call ShowEditBoxes()

        ElseIf iSubIndex = 8 Then ' Owned Type

            MEUpdate = False
            TEUpdate = False
            FavoriteUpdate = False
            IgnoredBPUpdate = False
            OwnedTypeUpdate = True

            Call ShowEditBoxes()

        ElseIf iSubIndex = 9 Then ' Favorite

            MEUpdate = False
            TEUpdate = False
            FavoriteUpdate = True
            IgnoredBPUpdate = False
            OwnedTypeUpdate = False

            Call ShowEditBoxes()

        ElseIf iSubIndex = 10 Then ' Ignored

            MEUpdate = False
            TEUpdate = False
            FavoriteUpdate = False
            IgnoredBPUpdate = True
            OwnedTypeUpdate = False

            Call ShowEditBoxes()

        Else
            ' Not updatable so leave
            Exit Sub
        End If

    End Sub

    Private Sub ProcessKeyDownBPEdit(SentKey As Keys)
        Dim SQL As String = ""
        Dim MEValue As Integer
        Dim TEValue As Integer
        Dim FavoriteValue As String
        Dim OwnedTypeValue As String
        Dim IgnoredValue As String
        Dim TempBPType As BPType
        Dim UpdatedBPType As BPType

        Dim SetasFavorite As Boolean
        Dim SetasIgnore As Boolean

        ' Change blank entry to 0
        If Trim(txtBPEdit.Text) = "" Then
            txtBPEdit.Text = "0"
        End If

        ' If they hit enter or tab away, mark the BP as owned in the DB with the values entered
        If (SentKey = Keys.Enter Or SentKey = Keys.ShiftKey Or SentKey = Keys.Tab) And DataEntered Then

            ' Check the input first
            If Not IsNumeric(txtBPEdit.Text) And MEUpdate Then
                MsgBox("Invalid ME Value", vbExclamation)
                Exit Sub
            End If

            If Not IsNumeric(txtBPEdit.Text) And TEUpdate Then
                MsgBox("Invalid TE Value", vbExclamation)
                Exit Sub
            End If

            ' Save the data, Save as no scan, but BPO, BP Type
            If MEUpdate Then
                MEValue = CInt(txtBPEdit.Text)
            Else
                MEValue = CInt(CurrentRow.SubItems(5).Text)
            End If

            If TEUpdate Then
                TEValue = CInt(txtBPEdit.Text)
            Else
                TEValue = CInt(CurrentRow.SubItems(6).Text)
            End If

            If OwnedTypeUpdate Then
                OwnedTypeValue = cmbEdit.Text
            Else
                OwnedTypeValue = CurrentRow.SubItems(8).Text
            End If

            If FavoriteUpdate Then
                FavoriteValue = cmbEdit.Text
            Else
                FavoriteValue = CurrentRow.SubItems(9).Text
            End If

            If FavoriteValue = Yes Then
                SetasFavorite = True
            Else
                SetasFavorite = False
            End If

            If IgnoredBPUpdate Then
                IgnoredValue = cmbEdit.Text
            Else
                IgnoredValue = CurrentRow.SubItems(10).Text
            End If

            If IgnoredValue = Yes Then
                SetasIgnore = True
            Else
                SetasIgnore = False
            End If

            ' Check the numbers, if they are the same, then don't mark as owned
            If MEValue = CInt(CurrentRow.SubItems(5).Text) _
                And TEValue = CInt(CurrentRow.SubItems(6).Text) _
                And OwnedTypeValue = CurrentRow.SubItems(8).Text _
                And FavoriteValue = CurrentRow.SubItems(9).Text _
                And IgnoredValue = CurrentRow.SubItems(10).Text Then
                ' Skip down
                GoTo Tabs
            End If

            ' Set the bp type to make sure we set the owned flag correctly
            TempBPType = GetBPType(OwnedTypeValue)

            Dim TempRuns As Integer

            UpdatedBPType = UpdateBPinDB(CLng(CurrentRow.SubItems(1).Text), MEValue, TEValue, TempBPType,
                              CInt(CurrentRow.SubItems(5).Text), CInt(CurrentRow.SubItems(6).Text), SetasFavorite, SetasIgnore)

            Call PlayNotifySound()

            ' Reset text they entered if tabbed
            If SentKey = Keys.ShiftKey Or SentKey = Keys.Tab Then
                txtBPEdit.Text = ""
            End If

            ' Update the data in the current row
            CurrentRow.SubItems(8).Text = GetBPTypeString(UpdatedBPType)
            If UpdatedBPType = BPType.NotOwned Or CurrentRow.SubItems(2).Text.Contains("Reaction Formulas") Then ' Reactions are always set to 0
                ' Update the ME/TE to 0 
                CurrentRow.SubItems(5).Text = "0"
                CurrentRow.SubItems(6).Text = "0"
            Else
                CurrentRow.SubItems(5).Text = CStr(MEValue)
                CurrentRow.SubItems(6).Text = CStr(TEValue)
            End If

            If TempRuns = -1 Then
                CurrentRow.SubItems(11).Text = Unlimited
            Else
                CurrentRow.SubItems(11).Text = CStr(TempRuns)
            End If

            CurrentRow.SubItems(9).Text = FavoriteValue
            CurrentRow.SubItems(10).Text = IgnoredValue

            ' Since they selected the row, update the ME/TE boxes with new data
            txtBPME.Text = CStr(MEValue)
            txtBPTE.Text = CStr(TEValue)

            ' Mark as owned and change color
            Call SetOwnedFlagandColors(CurrentRow, CBool(UpdatedBPType))

            If UpdatedBPType = BPType.NotOwned Then ' 14 = Owned
                CurrentRow.SubItems(7).Text = No
                CurrentRow.ForeColor = Color.Black
                CurrentRow.BackColor = Color.White
            Else
                CurrentRow.SubItems(7).Text = Yes
                CurrentRow.ForeColor = Color.Blue
            End If

            If SentKey = Keys.Enter Then
                ' Just refresh and select the current row
                CurrentRow.Selected = True
                txtBPEdit.Visible = False
                cmbEdit.Visible = False
            End If

            ' Data updated, so reset
            DataEntered = False

        End If

Tabs:
        ' If they hit tab, then tab to the next cell
        If SentKey = Keys.Tab Then
            CurrentCell = NextCell
            ' Reset these each time
            Call SetNextandPreviousCells("Next")
            If CurrentRow.Index = 0 Then
                ' Scroll to top
                lstBPs.Items.Item(0).Selected = True
                lstBPs.EnsureVisible(0)
                lstBPs.Update()
            Else
                ' Make sure the row is visible
                lstBPs.EnsureVisible(CurrentRow.Index)
            End If
            ' Show the text box
            Call ShowEditBoxes()
        End If

        ' If shift+tab, then go to the previous cell 
        If SentKey = Keys.ShiftKey Then
            CurrentCell = PreviousCell
            ' Reset these each time
            Call SetNextandPreviousCells("Previous")
            If CurrentRow.Index = lstBPs.Items.Count - 1 Then
                ' Scroll to bottom
                lstBPs.Items.Item(lstBPs.Items.Count - 1).Selected = True
                lstBPs.EnsureVisible(lstBPs.Items.Count - 1)
                lstBPs.Update()
            Else
                ' Make sure the row is visible
                lstBPs.EnsureVisible(CurrentRow.Index)
            End If
            ' Show the text box
            Call ShowEditBoxes()
        End If

    End Sub

    ' Determines where the previous and next item boxes will be based on what they clicked - used in tab event handling
    Private Sub SetNextandPreviousCells(Optional CellType As String = "")
        Dim iSubIndex As Integer = 0

        ' Normal Row
        If CellType = "Next" Then
            CurrentRow = NextCellRow
        ElseIf CellType = "Previous" Then
            CurrentRow = PreviousCellRow
        End If

        ' Get index of column
        iSubIndex = CurrentRow.SubItems.IndexOf(CurrentCell)

        ' Get next and previous rows. If at end, wrap to top. If at top, wrap to bottom
        If lstBPs.Items.Count = 1 Then
            NextRow = CurrentRow
            PreviousRow = CurrentRow
        ElseIf CurrentRow.Index <> lstBPs.Items.Count - 1 And CurrentRow.Index <> 0 Then
            ' Not the last line, so set the next and previous
            NextRow = lstBPs.Items.Item(CurrentRow.Index + 1)
            PreviousRow = lstBPs.Items.Item(CurrentRow.Index - 1)
        ElseIf CurrentRow.Index = 0 Then
            NextRow = lstBPs.Items.Item(CurrentRow.Index + 1)
            ' Wrap to bottom
            PreviousRow = lstBPs.Items.Item(lstBPs.Items.Count - 1)
        ElseIf CurrentRow.Index = lstBPs.Items.Count - 1 Then
            ' Need to wrap up to top
            NextRow = lstBPs.Items.Item(0)
            PreviousRow = lstBPs.Items.Item(CurrentRow.Index - 1)
        End If

        ' ME box is selected
        If iSubIndex = 5 Then
            ' Set the next and previous ME boxes (subitems)
            NextCell = CurrentRow.SubItems.Item(6) ' On Same Line 
            NextCellRow = CurrentRow
            PreviousCell = PreviousRow.SubItems.Item(10) ' On previous line 
            PreviousCellRow = PreviousRow
            MEUpdate = True
            TEUpdate = False
            FavoriteUpdate = False
            IgnoredBPUpdate = False
            OwnedTypeUpdate = False
        ElseIf iSubIndex = 6 Then ' TE box is selected
            ' Set the next and previous ME or favorite boxes (subitems)
            NextCell = CurrentRow.SubItems.Item(8) ' On same line 
            NextCellRow = CurrentRow
            PreviousCell = CurrentRow.SubItems.Item(5) ' On same line 
            PreviousCellRow = CurrentRow
            MEUpdate = False
            TEUpdate = True
            FavoriteUpdate = False
            IgnoredBPUpdate = False
            OwnedTypeUpdate = False
        ElseIf iSubIndex = 8 Then ' Owned Type combo
            ' Set the next and previous ME boxes (subitems)
            NextCell = CurrentRow.SubItems.Item(9) ' On On same line 
            NextCellRow = CurrentRow
            PreviousCell = CurrentRow.SubItems.Item(6) ' On same line 
            PreviousCellRow = CurrentRow
            MEUpdate = False
            TEUpdate = False
            FavoriteUpdate = False
            IgnoredBPUpdate = False
            OwnedTypeUpdate = True
        ElseIf iSubIndex = 9 Then ' Favorite combo
            ' Set the next and previous ME boxes (subitems)
            NextCell = CurrentRow.SubItems.Item(10) ' On same line
            NextCellRow = CurrentRow
            PreviousCell = CurrentRow.SubItems.Item(8) ' On same line 
            PreviousCellRow = CurrentRow
            MEUpdate = False
            TEUpdate = False
            FavoriteUpdate = True
            IgnoredBPUpdate = False
            OwnedTypeUpdate = False
        ElseIf iSubIndex = 10 Then ' Ignore combo
            ' Set the next and previous ME boxes (subitems)
            NextCell = NextRow.SubItems.Item(5) ' On next line 
            NextCellRow = NextRow
            PreviousCell = CurrentRow.SubItems.Item(9) ' On same line 
            PreviousCellRow = CurrentRow
            MEUpdate = False
            TEUpdate = False
            FavoriteUpdate = False
            IgnoredBPUpdate = True
            OwnedTypeUpdate = False
        Else
            NextCell = Nothing
            PreviousCell = Nothing
            CurrentCell = Nothing
        End If

    End Sub

    Private Sub ShowEditBoxes()
        Dim lLeft As Integer = 0
        Dim lWidth As Integer = 0

        ' Get size of column and location
        lLeft = CurrentCell.Bounds.Left + 2
        lWidth = CurrentCell.Bounds.Width

        If MEUpdate Or TEUpdate Then
            With txtBPEdit
                .Hide()
                .SetBounds(lLeft + lstBPs.Left, CurrentCell.Bounds.Top + _
                           lstBPs.Top, lWidth, CurrentCell.Bounds.Height)
                .Text = CurrentCell.Text
                .Show()
                .TextAlign = HorizontalAlignment.Center
                .Focus()
            End With
            cmbEdit.Visible = False
        Else ' OwnedType/Favorites/Ignore
            With cmbEdit
                If OwnedTypeUpdate Then
                    .Items.Clear()
                    If CurrentRow.SubItems.Item(4).Text = "T1" Or CurrentRow.SubItems.Item(4).Text = "T2" Then
                        .Items.Add(BPO) ' Only T1 and T2 have BPOs
                    End If

                    If CurrentRow.SubItems.Item(4).Text = "T2" Or CurrentRow.SubItems.Item(4).Text = "T3" Then
                        ' Invented BPCs are T2, T3 
                        .Items.Add(InventedBPC)
                    End If

                    If CurrentRow.SubItems.Item(4).Text <> "T3" Then
                        .Items.Add(BPC) ' Copies from BPOs, which are T1 and T2 only
                    End If
                    .Items.Add(UnownedBP)
                Else ' Favorite / Ignore
                    .Items.Clear()
                    .Items.Add(Yes)
                    .Items.Add(No)
                End If

                .Hide()
                .SetBounds(lLeft + lstBPs.Left, CurrentCell.Bounds.Top + _
                            lstBPs.Top, lWidth, CurrentCell.Bounds.Height)
                .Text = CurrentCell.Text
                DataEntered = False ' We just updated so reset
                .Show()
                .Focus()

            End With
            txtBPEdit.Visible = False
        End If

    End Sub

    ' Processes the tab function in the text box for the grid. This overrides the default tabbing between controls
    Protected Overrides Function ProcessTabKey(ByVal TabForward As Boolean) As Boolean
        Dim ac As Control = Me.ActiveControl

        TabPressed = True

        If TabForward Then
            If ac Is txtBPEdit Or ac Is cmbEdit Then
                Call ProcessKeyDownBPEdit(Keys.Tab)
                Return True
            End If
        Else
            If ac Is txtBPEdit Or ac Is cmbEdit Then
                ' This is Shift + Tab but just send Shift for ease of processing
                Call ProcessKeyDownBPEdit(Keys.ShiftKey)
                Return True
            End If
        End If

        Return MyBase.ProcessTabKey(TabForward)

    End Function

    ' Detects Scroll event and hides boxes
    Private Sub lstBPs_ProcMsg(ByVal m As System.Windows.Forms.Message) Handles lstBPs.ProcMsg
        txtBPEdit.Hide()
        cmbEdit.Hide()
    End Sub

    Private Sub txtBPEdit_GotFocus(sender As Object, e As System.EventArgs) Handles txtBPEdit.GotFocus
        If Not FirstLoad Then
            Call txtBPEdit.SelectAll()
        End If
    End Sub

    Private Sub txtBPEdit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBPEdit.KeyDown
        Call ProcessCutCopyPasteSelect(txtBPEdit, e)
        If e.KeyCode = Keys.Enter Then
            Call ProcessKeyDownBPEdit(Keys.Enter)
        End If
    End Sub

    Private Sub txtBPEdit_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPEdit.KeyPress
        ' Make sure it's the right format for ME/TE
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            Else
                DataEntered = True
            End If
        End If

    End Sub

    Private Sub txtBPEdit_LostFocus(sender As Object, e As System.EventArgs) Handles txtBPEdit.LostFocus
        ' Lost focus some other way than tabbing
        If Not TabPressed Then
            Call ProcessKeyDownBPEdit(Keys.Enter)
        End If
        TabPressed = False ' Reset
        txtBPEdit.Visible = False
    End Sub

    Private Sub txtBPEdit_TextChanged(sender As Object, e As System.EventArgs) Handles txtBPEdit.TextChanged
        If MEUpdate Then
            Call VerifyMETEEntry(txtBPEdit, "ME")
        Else
            Call VerifyMETEEntry(txtBPEdit, "TE")
        End If
    End Sub

    Private Sub cmbEdit_LostFocus(sender As Object, e As System.EventArgs) Handles cmbEdit.LostFocus, cmbEdit.LostFocus
        ' Lost focus some other way than tabbing
        If Not TabPressed Then
            Call ProcessKeyDownBPEdit(Keys.Enter)
        End If
        TabPressed = False ' Reset
        cmbEdit.Visible = False
    End Sub

    Private Sub cmbEdit_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbEdit.SelectedIndexChanged
        DataEntered = True
    End Sub

#End Region

End Class
