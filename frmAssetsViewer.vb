
Imports System.Runtime.InteropServices
Imports System.Data.SQLite

Public Class frmAssetsViewer

    Private ToggleAllOpen As Boolean
    Private UpdatingChecks As Boolean
    Private RefreshAssetButton As Boolean

    Private SelectedSettings As AssetWindowSettings

    ' The saved location ids
    Private SavedLocationIDs As List(Of LocationInfo)

    Private m_ControlsCollection As ControlsCollection
    Private TechCheckBoxes(6) As CheckBox
    Private UpdateAllTechChecks As Boolean = True ' Whether to update all Tech checks in items or not
    Private FirstPriceShipTypesComboLoad As Boolean
    Private FirstPriceChargeTypesComboLoad As Boolean

    ' For checks that are enabled
    Private PriceCheckT1Enabled As Boolean
    Private PriceCheckT2Enabled As Boolean
    Private PriceCheckT3Enabled As Boolean
    Private PriceCheckT4Enabled As Boolean
    Private PriceCheckT5Enabled As Boolean
    Private PriceCheckT6Enabled As Boolean

    ' Where the form was loaded from
    Private WindowForm As AssetWindow

    ' For drawing checkboxes
    Private Const TVIF_STATE As Integer = &H8
    Private Const TVIS_STATEIMAGEMASK As Integer = &HF000
    Private Const TV_FIRST As Integer = &H1100
    Private Const TVM_SETITEM As Integer = TV_FIRST + 63

#Region "Special processing for checks"
    <StructLayout(LayoutKind.Sequential)>
    Public Structure TVITEM
        Public mask As Integer
        Public hItem As IntPtr
        Public state As Integer
        Public stateMask As Integer
        <MarshalAs(UnmanagedType.LPTStr)>
        Public lpszText As String
        Public cchTextMax As Integer
        Public iImage As Integer
        Public iSelectedImage As Integer
        Public cChildren As Integer
        Public lParam As IntPtr
    End Structure

    Private Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As TVITEM) As Integer

    Private Sub HideRootCheckBox(ByVal node As TreeNode)
        Dim tvi As New TVITEM
        tvi.hItem = node.Handle
        tvi.mask = TVIF_STATE
        tvi.stateMask = TVIS_STATEIMAGEMASK
        tvi.state = 0
        SendMessage(AssetTree.Handle, TVM_SETITEM, IntPtr.Zero, tvi)
    End Sub

    Private Sub AssetTree_DrawNode(ByVal sender As Object, ByVal e As DrawTreeNodeEventArgs) Handles AssetTree.DrawNode
        ' Don't show the top node with a check box
        If e.Node.Parent Is Nothing Then
            HideRootCheckBox(e.Node)
        End If

        ' For high, mid, rigs, and low slots on ships, don't show check boxes
        If e.Node.Text.Contains("power slot") Or e.Node.Text.Contains("Personal Assets") Or e.Node.Text.Contains("Corporation Assets") Then
            HideRootCheckBox(e.Node)
        End If

        ' Don't show a checkbox on any nodes without children
        If e.Node.Nodes.Count = 0 Then
            HideRootCheckBox(e.Node)
        End If

        e.DrawDefault = True

    End Sub

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

#End Region

    Public Sub New(ByVal AssetType As AssetWindow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        WindowForm = AssetType

        ' Mark where the window is attached - can have multiple open
        Select Case AssetType
            Case AssetWindow.DefaultView
                Me.Text = "Default Asset Viewer"
                SelectedSettings = UserAssetWindowDefaultSettings
            Case AssetWindow.ManufacturingTab
                Me.Text = "Manufacturing Asset Viewer"
                SelectedSettings = UserAssetWindowManufacturingTabSettings
            Case AssetWindow.ShoppingList
                Me.Text = "Shopping List Assets"
                SelectedSettings = UserAssetWindowShoppingListSettings
            Case AssetWindow.ReprocessingPlant
                Me.Text = "Refinery Assets"
                SelectedSettings = UserAssetWindowRefinerySettings
        End Select

        ' Width 253, 21 for scrollbar, 25 for check (207)
        lstCharacters.Columns.Add("", -2, HorizontalAlignment.Left)
        lstCharacters.Columns.Add("Character Name", 90, HorizontalAlignment.Left)
        lstCharacters.Columns.Add("Character Corporation", 117, HorizontalAlignment.Left)
        lstCharacters.Columns.Add("CharID", 0, HorizontalAlignment.Left) ' Hidden
        lstCharacters.Columns.Add("CorpID", 0, HorizontalAlignment.Left) ' Hidden

        ' For the disabling of the price update form
        PriceCheckT1Enabled = True
        PriceCheckT2Enabled = True
        PriceCheckT3Enabled = True
        PriceCheckT4Enabled = True
        PriceCheckT5Enabled = True
        PriceCheckT6Enabled = True

        SavedLocationIDs = New List(Of LocationInfo)

        AssetTree.DrawMode = TreeViewDrawMode.OwnerDrawAll
        AssetTree.CheckBoxes = True

    End Sub

    Private Sub frmAssetsViewer_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ' If we have no assets, then refresh the table to show that
        If SelectedCharacter.GetAssets.GetAssetCount = 0 Then
            Call RefreshTree()
        End If
    End Sub

    ' Initialize the form based on user settings
    Private Sub frmAssetsViewer_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Application.DoEvents()

        FirstLoad = True

        Timer1.Interval = 1000 ' 1 second
        Timer1.Enabled = True

        FirstPriceChargeTypesComboLoad = True
        FirstPriceShipTypesComboLoad = True

        btnScanCorpAssets.Enabled = False
        btnScanPersonalAssets.Enabled = False

        ' Get Region check boxes (note index starts at 1)
        TechCheckBoxes(1) = chkItemsT1
        TechCheckBoxes(2) = chkItemsT2
        TechCheckBoxes(3) = chkItemsT3
        TechCheckBoxes(4) = chkItemsT4
        TechCheckBoxes(5) = chkItemsT5
        TechCheckBoxes(6) = chkItemsT6

        pnlStatus.Text = ""
        pnlProgressBar.Visible = False

        ' Main form
        Select Case SelectedSettings.AssetType
            Case rbtnAllAssets.Text
                rbtnAllAssets.Checked = True
            Case rbtnPersonalAssets.Text
                rbtnPersonalAssets.Checked = True
            Case rbtnCorpAssets.Text
                rbtnCorpAssets.Checked = True
        End Select

        If SelectedSettings.SortbyName Then
            rbtnSortName.Checked = True
        Else
            rbtnSortQuantity.Checked = True
        End If

        lblReloadCorpAssets.Text = "---"
        lblReloadPersonalAssets.Text = "---"

        ' Search settings form
        If SelectedSettings.AllItems Then
            rbtnAllItems.Checked = True
        Else
            rbtnBPMats.Checked = True
        End If

        If SelectedSettings.SelectedAccount Then
            rbtnSelectedAccount.Checked = True
        Else
            rbtnMultiAccounts.Checked = True
        End If

        ' Regardless, load up all account info
        Call LoadAccountList()

        txtItemFilter.Text = SelectedSettings.ItemFilterText

        ' Load the search settings
        With SelectedSettings
            chkMaterialResearchEqPrices.Checked = .AllRawMats

            chkAdvancedProtectiveTechnology.Checked = .AdvancedProtectiveTechnology
            chkGas.Checked = .Gas
            chkIceProducts.Checked = .IceProducts
            chkMolecularForgingTools.Checked = .MolecularForgingTools
            chkFactionMaterials.Checked = .FactionMaterials
            chkNamedComponents.Checked = .NamedComponents
            chkMinerals.Checked = .Minerals
            chkPlanetary.Checked = .Planetary
            chkRawMaterials.Checked = .RawMaterials
            chkSalvage.Checked = .Salvage
            chkMisc.Checked = .Misc
            chkBPCs.Checked = .BPCs

            chkAdvancedMats.Checked = .AdvancedMoonMats
            chkBoosterMats.Checked = .BoosterMats
            chkMolecularForgedMaterials.Checked = .MolecularForgedMats
            chkPolymers.Checked = .Polymers
            chkProcessedMats.Checked = .ProcessedMoonMats
            chkRawMoonMats.Checked = .RawMoonMats

            chkAncientRelics.Checked = .AncientRelics
            chkDatacores.Checked = .Datacores
            chkDecryptors.Checked = .Decryptors
            chkRDb.Checked = .RDB

            chkManufacturedItems.Checked = .AllManufacturedItems

            chkShips.Checked = .Ships
            chkCharges.Checked = .Charges
            chkModules.Checked = .Modules
            chkDrones.Checked = .Drones
            chkRigs.Checked = .Rigs
            chkSubsystems.Checked = .Subsystems
            chkDeployables.Checked = .Deployables
            chkBoosters.Checked = .Boosters
            chkStructures.Checked = .Structures
            chkStructureRigs.Checked = .StructureRigs
            chkCelestials.Checked = .Celestials
            chkStructureModules.Checked = .StructureModules
            chkImplants.Checked = .Implants

            chkCapT2Components.Checked = .AdvancedCapComponents
            chkAdvancedComponents.Checked = .AdvancedComponents
            chkFuelBlocks.Checked = .FuelBlocks
            chkProtectiveComponents.Checked = .ProtectiveComponents
            chkRAM.Checked = .RAM
            chkNobuild.Checked = .NoBuildItems
            chkCapitalShipComponents.Checked = .CapitalShipComponents
            chkStructureComponents.Checked = .StructureComponents
            chkSubsystemComponents.Checked = .SubsystemComponents

            chkItemsT1.Checked = .T1
            chkItemsT2.Checked = .T2
            chkItemsT3.Checked = .T3
            chkItemsT4.Checked = .Storyline
            chkItemsT5.Checked = .Faction
            chkItemsT6.Checked = .Pirate
        End With

        FirstLoad = False

        ' Everything will be just normal at first - add to settings for the format they save? Also, check the locations they have checked only TODO-AV!
        ToggleAllOpen = False

        Call RefreshTree()

        Application.DoEvents()

    End Sub

    ' Refreshes the account list with character account info
    Private Sub LoadAccountList()
        Dim rsAccounts As SQLiteDataReader
        Dim lstCharacterRow As ListViewItem
        Dim SQL As String = "SELECT CHARACTER_NAME, CORPORATION_NAME, CHARACTER_ID, ESI_CHARACTER_DATA.CORPORATION_ID FROM ESI_CHARACTER_DATA, ESI_CORPORATION_DATA
                             WHERE ESI_CHARACTER_DATA.CORPORATION_ID = ESI_CORPORATION_DATA.CORPORATION_ID AND CHARACTER_ID <> " & CStr(DummyCharacterID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsAccounts = DBCommand.ExecuteReader
        lstCharacters.BeginUpdate()
        lstCharacters.Items.Clear()

        While rsAccounts.Read
            lstCharacterRow = New ListViewItem("") ' Check
            lstCharacterRow.SubItems.Add(rsAccounts.GetString(0))
            lstCharacterRow.SubItems.Add(rsAccounts.GetString(1))
            lstCharacterRow.SubItems.Add(CStr(rsAccounts.GetInt64(2)))
            lstCharacterRow.SubItems.Add(CStr(rsAccounts.GetInt64(3)))
            Call lstCharacters.Items.Add(lstCharacterRow)
            ' check the selected character to start if only selected account 
            If rbtnMultiAccounts.Checked And SelectedSettings.SelectedCharacterIDs.Contains(rsAccounts.GetInt64(2).ToString) Then
                lstCharacterRow.Checked = True
            End If
        End While
        lstCharacters.EndUpdate()
    End Sub

    ' Returns true if an selected item is checked
    Private Function ItemsChecked() As Boolean

        If chkAdvancedProtectiveTechnology.Checked Then Return True
        If chkGas.Checked Then Return True
        If chkIceProducts.Checked Then Return True
        If chkMolecularForgingTools.Checked Then Return True
        If chkFactionMaterials.Checked Then Return True
        If chkNamedComponents.Checked Then Return True
        If chkMinerals.Checked Then Return True
        If chkPlanetary.Checked Then Return True
        If chkRawMaterials.Checked Then Return True
        If chkSalvage.Checked Then Return True
        If chkMisc.Checked Then Return True
        If chkBPCs.Checked Then Return True

        If chkAdvancedMats.Checked Then Return True
        If chkBoosterMats.Checked Then Return True
        If chkMolecularForgedMaterials.Checked Then Return True
        If chkPolymers.Checked Then Return True
        If chkProcessedMats.Checked Then Return True
        If chkRawMoonMats.Checked Then Return True

        If chkAncientRelics.Checked Then Return True
        If chkDatacores.Checked Then Return True
        If chkDecryptors.Checked Then Return True
        If chkRDb.Checked Then Return True

        If chkShips.Checked Then Return True
        If chkCharges.Checked Then Return True
        If chkModules.Checked Then Return True
        If chkDrones.Checked Then Return True
        If chkRigs.Checked Then Return True
        If chkSubsystems.Checked Then Return True
        If chkDeployables.Checked Then Return True
        If chkBoosters.Checked Then Return True
        If chkStructures.Checked Then Return True
        If chkStructureRigs.Checked Then Return True
        If chkCelestials.Checked Then Return True
        If chkStructureModules.Checked Then Return True
        If chkImplants.Checked Then Return True

        If chkCapT2Components.Checked Then Return True
        If chkAdvancedComponents.Checked Then Return True
        If chkFuelBlocks.Checked Then Return True
        If chkProtectiveComponents.Checked Then Return True
        If chkRAM.Checked Then Return True
        If chkCapitalShipComponents.Checked Then Return True
        If chkStructureComponents.Checked Then Return True
        If chkSubsystemComponents.Checked Then Return True

        ' If we got here, nothing checked
        Return False

    End Function

    Private Structure AssetDataInfo
        Dim ID As Long
        Dim Name As String
    End Structure

    ' Main function that refresh's the tree
    Public Sub RefreshTree()
        Dim BaseNode As New TreeNode
        Dim SortOption As SortType
        Dim SearchItemList As List(Of Long)
        Dim OnlyBPCs As Boolean ' Pass through if the BPIDs we sent need to only be shown if a copy
        Dim AnchorNode As New TreeNode

        pnlStatus.Text = ""
        ActiveForm.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        ' Make sure they have an item selected
        If Not ItemsChecked() And Not rbtnAllItems.Checked Then
            MsgBox("You must select an item category to display.", vbExclamation, Application.ProductName)
            tabMain.SelectedTab = tabSearchSettings
            ActiveForm.Cursor = Cursors.Default
            Exit Sub
        End If

        ' Set the tree object
        AssetTree.BeginUpdate()
        AssetTree.Nodes.Clear()

        If rbtnSortName.Checked Then
            SortOption = SortType.Name
        ElseIf rbtnSortQuantity.Checked Then
            SortOption = SortType.Quantity
        End If

        ' Get the list based on options selected, if not all or if all, they have a text item to search
        If rbtnBPMats.Checked Or Trim(txtItemFilter.Text) <> "" Then
            SearchItemList = GetSearchItemsList(rbtnAllItems.Checked)
        Else
            SearchItemList = New List(Of Long) ' set to a blank list
        End If

        ' Set OnlyBPCs
        If chkBPCs.Checked And rbtnBPMats.Checked Then
            OnlyBPCs = True
        Else
            OnlyBPCs = False
        End If

        ' If we get nothing back from the search item list, then just clear the assets and exit - we have no items to display
        If IsNothing(SearchItemList) Then
            AssetTree.EndUpdate()
            AssetTree.Refresh()

            ActiveForm.Cursor = Cursors.Default
            Application.DoEvents()
            MsgBox("No items found.", vbInformation, Application.ProductName)
            Me.Refresh()
            Exit Sub
        End If

        ' Now add all the characters we want to update for assets - add character name in a node first, then attach all the assets to that character node
        ' Loop through all characters and add each - including corporations. For Corporations, just add separately so we only load one corporation tree for 
        ' each corporation from the list of characters
        Dim AssetData As New List(Of AssetDataInfo)
        Dim Tempinfo As AssetDataInfo

        If rbtnSelectedAccount.Checked Then
            ' Just use the selected ID and corporation
            Tempinfo.ID = SelectedCharacter.ID
            Tempinfo.Name = SelectedCharacter.Name
            AssetData.Add(Tempinfo)
        Else
            ' Go through the list and build all CharacterID's and Corps for those selected
            If lstCharacters.CheckedItems.Count > 0 Then
                For i = 0 To lstCharacters.CheckedItems.Count - 1
                    ' Add to the list
                    Tempinfo.ID = CLng(lstCharacters.CheckedItems(i).SubItems(3).Text)
                    Tempinfo.Name = lstCharacters.CheckedItems(i).SubItems(1).Text
                    AssetData.Add(Tempinfo)
                Next
            Else ' nothing selected
                For Each item In lstCharacters.SelectedItems
                    MsgBox("You must select a character for assets", vbExclamation, Application.ProductName)
                    ActiveForm.Cursor = Cursors.Default
                    Application.DoEvents()
                    Exit Sub
                Next
            End If
        End If

        ' Add the base node
        AnchorNode = AssetTree.Nodes.Add("Asset List")

        Dim TokenData As New SavedTokenData
        Dim AssetCharacterList As New List(Of Character)
        Dim TempCharacter As New Character

        For Each entry In AssetData
            ' Create a character, then update the assets, then run asset tree node on that set of assets
            pnlStatus.Text = "Refreshing Assets for " & entry.Name
            TokenData.CharacterID = entry.ID

            ' Update Assets only if different character than selected
            If entry.ID <> SelectedCharacter.ID Then
                TempCharacter = New Character
                TempCharacter.LoadCharacterData(TokenData, False, True, False)
            Else
                TempCharacter = SelectedCharacter
            End If

            ' Get the asset locations saved for the character
            Call SavedLocationIDs.AddRange(TempCharacter.Assets.GetAssetLocationIDs(WindowForm, TempCharacter.ID, TempCharacter.CharacterCorporation))
            Call AssetCharacterList.Add(TempCharacter)

            ' Now run the tree build
            pnlStatus.Text = "Loading Assets for " & entry.Name
            Application.DoEvents()

            ' Get the base node of the full tree (may want to save these options globally so we don't need to load them every time)
            If rbtnPersonalAssets.Checked Or rbtnAllAssets.Checked Then
                BaseNode = TempCharacter.GetAssets.GetAssetTreeReturnNode(SortOption, SearchItemList, entry.Name & " - Personal Assets", entry.ID, SavedLocationIDs, OnlyBPCs, TempCharacter.CharacterTokenData)

                ' Need to add it to the tree but as a clone
                AnchorNode.Nodes.Add(CType(BaseNode.Clone, TreeNode))
            End If
        Next

        Dim LoadedCorpIDs As New List(Of Long) ' only load the corp once
        ' Now load the corporation data at the end
        For Each entry In AssetCharacterList
            If (rbtnCorpAssets.Checked Or rbtnAllAssets.Checked) And Not LoadedCorpIDs.Contains(entry.CharacterCorporation.CorporationID) Then
                pnlStatus.Text = "Loading Assets for " & entry.CharacterCorporation.Name
                Application.DoEvents()
                BaseNode = entry.CharacterCorporation.GetAssets.GetAssetTreeReturnNode(SortOption, SearchItemList, entry.CharacterCorporation.Name & " - Corporation Assets", entry.CharacterCorporation.CorporationID, SavedLocationIDs, OnlyBPCs)

                If Not BaseNode.Text.Contains("No Assets Loaded") Then
                    ' Save this corp as assets already loaded
                    Call LoadedCorpIDs.Add(entry.CharacterCorporation.CorporationID)
                    Dim node As TreeNode
                    ' Remove any nodes that are the same corp and no assets loaded
                    For Each node In AnchorNode.Nodes
                        If node.Text.Contains("No Assets Loaded") And node.Name = CStr(entry.CharacterCorporation.CorporationID) Then
                            AnchorNode.Nodes.Remove(node)
                            Exit For
                        End If
                    Next
                End If

                ' Need to add it to the tree but as a clone
                AnchorNode.Nodes.Add(CType(BaseNode.Clone, TreeNode))

            End If
        Next

        ' Update
        pnlStatus.Text = ""
        Application.DoEvents()
        AssetTree.EndUpdate()
        AssetTree.Refresh()

        ' Open up the top node and the personal/corp nodes. Plus reset toggle since we just reloaded
        ToggleAllOpen = False
        AssetTree.TopNode.Expand()

        ' expand all nodes for each character/corp loaded
        For i = 0 To AssetTree.Nodes(0).Nodes.Count - 1
            AssetTree.Nodes(0).Nodes(i).Expand()
        Next

        ' Expand all parents for check boxes that have values checked
        Call ExpandCheckedNodes(AssetTree.Nodes(0).Nodes, AssetTree)

        ' Scroll to top
        AssetTree.TopNode = AssetTree.Nodes(0)

        On Error Resume Next
        ActiveForm.Cursor = Cursors.Default
        Application.DoEvents()
        Me.Refresh()

    End Sub

    Private Sub ExpandCheckedNodes(NodeSet As TreeNodeCollection, ByRef BaseTree As TreeView)
        Dim node As New TreeNode

        For Each node In NodeSet
            FindCheckedNode(node, BaseTree)
        Next

    End Sub

    Private Sub FindCheckedNode(SentNode As TreeNode, ByRef BaseTree As TreeView)
        Dim tn As TreeNode

        For Each tn In SentNode.Nodes
            If tn.Checked = True Then
                BaseTree.SelectedNode = tn
                BaseTree.SelectedNode.Parent.Expand()
            End If
            FindCheckedNode(tn, BaseTree)
        Next
    End Sub

    ' Gets TypeIDs for items we only want to see in the asset tree from stuff we can set a price for
    Private Function GetSearchItemsList(SearchAllItems As Boolean) As List(Of Long)
        Dim ItemIDList As New List(Of Long)
        Dim readerMats As SQLiteDataReader
        Dim SQL As String
        Dim TechSQL As String = ""
        Dim TechChecked As Boolean = False
        Dim ItemChecked As Boolean = False

        ' Working
        ActiveForm.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        ' If we want all items, look in inventory types with links to groups/categories
        If SearchAllItems Then
            ' Get the item id from prices, since these are items we can set a price for and will be used in building items
            SQL = "SELECT typeID AS ITEM_ID FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_CATEGORIES "
            SQL &= "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
            SQL &= "AND INVENTORY_GROUPS.categoryID = INVENTORY_CATEGORIES.categoryID "

            ' Search based on text
            If txtItemFilter.Text <> "" Then
                SQL &= " AND typeName LIKE '%" & FormatDBString(Trim(txtItemFilter.Text)) & "%' "
            End If

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerMats = DBCommand.ExecuteReader

            ' Fill list
            While readerMats.Read
                Call ItemIDList.Add(readerMats.GetInt64(0))
            End While

            readerMats.Close()

        Else
            ' Want just building materials (from prices)
            ' Get the item id from prices, since these are items we can set a price for and will be used in building items
            SQL = "SELECT ITEM_ID FROM ITEM_PRICES, INVENTORY_TYPES"
            SQL &= " WHERE ITEM_PRICES.ITEM_ID = INVENTORY_TYPES.typeID AND ("

            Dim GroupSQL As String = GetItemPriceGroupListSQL(chkAdvancedComponents, chkAdvancedMats, chkAdvancedProtectiveTechnology, chkAncientRelics,
                                                              chkBoosterMats, chkBoosters, chkBPCs, chkCapitalShipComponents, chkCapT2Components,
                                                              chkCelestials, chkCharges, chkDatacores, chkDecryptors, chkDeployables, chkDrones,
                                                              chkFactionMaterials, chkFuelBlocks, chkGas, chkIceProducts, chkImplants,
                                                              chkMinerals, chkMisc, chkModules, chkMolecularForgedMaterials, chkMolecularForgingTools,
                                                              chkNamedComponents, chkPlanetary, chkPolymers, chkProcessedMats, chkProtectiveComponents,
                                                              chkRAM, chkRawMaterials, chkRawMoonMats, chkRDb, chkRigs, chkSalvage, chkShips,
                                                              chkStructureComponents, chkStructureModules, chkStructureRigs, chkStructures,
                                                              chkSubsystemComponents, chkSubsystems, cmbPriceChargeTypes, cmbPriceShipTypes,
                                                              chkItemsT1, PriceCheckT1Enabled, chkItemsT2, PriceCheckT2Enabled, chkItemsT3, PriceCheckT3Enabled,
                                                              chkItemsT4, PriceCheckT4Enabled, chkItemsT5, PriceCheckT5Enabled, chkItemsT6, PriceCheckT6Enabled, chkNobuild)

            ' Leave function if no items checked
            If GroupSQL <> "" Then
                SQL &= GroupSQL & ")"
                ' Search based on text
                If txtItemFilter.Text <> "" Then
                    SQL &= " AND ITEM_NAME LIKE '%" & FormatDBString(Trim(txtItemFilter.Text)) & "%' "
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerMats = DBCommand.ExecuteReader

                ' Fill list
                While readerMats.Read
                    Call ItemIDList.Add(readerMats.GetInt64(0))
                End While

                readerMats.Close()

            End If

            ' Blueprint Copies
            If chkBPCs.Checked And Not SearchAllItems Then
                ' Look up the typeIDs for all BPs and add them to the list
                SQL = "SELECT BLUEPRINT_ID, ITEM_NAME FROM ALL_BLUEPRINTS "
                ' Search based on text
                If txtItemFilter.Text <> "" Then
                    SQL &= " WHERE ITEM_NAME LIKE '%" & FormatDBString(Trim(txtItemFilter.Text)) & "%' "
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerMats = DBCommand.ExecuteReader

                ' Fill list
                While readerMats.Read
                    Call ItemIDList.Add(readerMats.GetInt64(0))
                End While

                readerMats.Close()

            End If

        End If

        ActiveForm.Cursor = Cursors.Default
        Application.DoEvents()

        ' If we have no items to return, then return nothing not a blank list
        If ItemIDList.Count = 0 Then
            Return Nothing
        Else
            Return ItemIDList
        End If

    End Function

    ' Just loads the assets from API then DB
    Private Sub ScanForAssets(ByVal BPScanType As ScanType)

        ' New scan, so run update and reload assets
        If BPScanType = ScanType.Personal Then
            SelectedCharacter.GetAssets.LoadAssets(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, True)
        Else
            SelectedCharacter.CharacterCorporation.GetAssets.LoadAssets(SelectedCharacter.CharacterCorporation.CorporationID,
                                                                        SelectedCharacter.CharacterTokenData, True)
        End If

        ' Reload the tree
        Call RefreshTree()

    End Sub

    ' Will use CAK and scan for bps in the user's items and store a temp table of these bps for loading in the grid
    Private Sub btnScanPersonalBPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanPersonalAssets.Click

        RefreshAssetButton = True
        ActiveForm.Cursor = Cursors.WaitCursor
        If rbtnAllAssets.Checked = False Then
            rbtnPersonalAssets.Checked = True
        End If

        Application.DoEvents()

        If SelectedCharacter.AssetsAccess Then
            Call ScanForAssets(ScanType.Personal)
        Else
            MsgBox("You have not enabled access to Assets with this key.", vbExclamation, Application.ProductName)
        End If

        ActiveForm.Cursor = Cursors.Default
        Application.DoEvents()

        RefreshAssetButton = False

    End Sub

    ' Will use CAK and scan for bps in the corps items and store a temp table of these bps for loading in the grid
    Private Sub btnScanCorpBPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanCorpAssets.Click

        RefreshAssetButton = True
        ActiveForm.Cursor = Cursors.WaitCursor
        If rbtnAllAssets.Checked = False Then
            rbtnCorpAssets.Checked = True
        End If

        Application.DoEvents()

        If SelectedCharacter.CharacterCorporation.AssetAccess Then
            Call ScanForAssets(ScanType.Corporation)
        Else
            MsgBox("You do not have a corporation key installed with access to Assets.", vbExclamation, Application.ProductName)
        End If

        ActiveForm.Cursor = Cursors.Default
        Application.DoEvents()

        RefreshAssetButton = False

    End Sub

    ' Displays the time remaining on refreshing assets
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim TempTime As Long

        ' On each tick just update the labels
        If SelectedCharacter.AssetsAccess Then
            TempTime = DateDiff(DateInterval.Second, Date.UtcNow, SelectedCharacter.GetAssets.CachedUntil)
            If TempTime <= 0 Then
                lblReloadPersonalAssets.Text = "Now"
            Else
                lblReloadPersonalAssets.Text = FormatTimeToComplete(TempTime)
            End If
        Else
            lblReloadPersonalAssets.Text = "No Access"
        End If

        If lblReloadPersonalAssets.Text = "Now" Then
            ' Enable refresh button
            btnScanPersonalAssets.Enabled = True
        Else
            btnScanPersonalAssets.Enabled = False
        End If

        If SelectedCharacter.CharacterCorporation.AssetAccess Then
            TempTime = DateDiff(DateInterval.Second, Date.UtcNow, SelectedCharacter.CharacterCorporation.GetAssets.CachedUntil)
            If TempTime <= 0 Then
                lblReloadCorpAssets.Text = "Now"
            Else
                lblReloadCorpAssets.Text = FormatTimeToComplete(TempTime)
            End If
        Else
            lblReloadCorpAssets.Text = "No Access"
        End If

        If lblReloadCorpAssets.Text = "Now" Then
            ' Enable refresh button
            btnScanCorpAssets.Enabled = True
        Else
            btnScanCorpAssets.Enabled = False
        End If

    End Sub

    ' Saves the settings on the Selected Items Form for later loading
    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click

        Call SaveWindowSettings()

    End Sub

    ' Saves the main settings for the general form
    Private Sub btnSaveMainSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveMainSettings.Click

        Call SaveWindowSettings()

    End Sub

    Private Function GetCharIDs() As String
        Dim CharIDs As String = ""

        If rbtnSelectedAccount.Checked Then
            CharIDs = CStr(SelectedCharacter.ID)
        Else
            For i = 0 To lstCharacters.CheckedItems.Count - 1
                CharIDs = CharIDs & CStr(lstCharacters.CheckedItems(i).SubItems(3).Text) & ","
            Next

            ' Strip the last comma
            If CharIDs <> "" Then
                CharIDs = CharIDs.Substring(0, Len(CharIDs) - 1)
            End If
        End If

        Return CharIDs

    End Function

    ' Saves the settings on both tabs for the asset window
    Private Sub SaveWindowSettings()
        Dim TempSettings As AssetWindowSettings = Nothing
        Dim TempLocationIDs As New List(Of Long)
        Dim SQL As String
        Dim rsCheck As SQLiteDataReader

        With TempSettings
            .SortbyName = rbtnSortName.Checked

            If rbtnAllAssets.Checked Then
                .AssetType = rbtnAllAssets.Text
            ElseIf rbtnPersonalAssets.Checked Then
                .AssetType = rbtnPersonalAssets.Text
            ElseIf rbtnCorpAssets.Checked Then
                .AssetType = rbtnCorpAssets.Text
            End If

            If rbtnAllItems.Checked Then
                .AllItems = True
            Else
                .AllItems = False
            End If

            .SelectedAccount = rbtnSelectedAccount.Checked
            .SelectedCharacterIDs = GetCharIDs()

            .AllRawMats = chkMaterialResearchEqPrices.Checked

            .AdvancedProtectiveTechnology = chkAdvancedProtectiveTechnology.Checked
            .Gas = chkGas.Checked
            .IceProducts = chkIceProducts.Checked
            .MolecularForgingTools = chkMolecularForgingTools.Checked
            .FactionMaterials = chkFactionMaterials.Checked
            .NamedComponents = chkNamedComponents.Checked
            .Minerals = chkMinerals.Checked
            .Planetary = chkPlanetary.Checked
            .RawMaterials = chkRawMaterials.Checked
            .Salvage = chkSalvage.Checked
            .Misc = chkMisc.Checked
            .BPCs = chkBPCs.Checked

            .AdvancedMoonMats = chkAdvancedMats.Checked
            .BoosterMats = chkBoosterMats.Checked
            .MolecularForgedMats = chkMolecularForgedMaterials.Checked
            .Polymers = chkPolymers.Checked
            .ProcessedMoonMats = chkProcessedMats.Checked
            .RawMoonMats = chkRawMoonMats.Checked

            .AncientRelics = chkAncientRelics.Checked
            .Datacores = chkDatacores.Checked
            .Decryptors = chkDecryptors.Checked
            .RDB = chkRDb.Checked

            .AllManufacturedItems = chkManufacturedItems.Checked

            .Ships = chkShips.Checked
            .Charges = chkCharges.Checked
            .Modules = chkModules.Checked
            .Drones = chkDrones.Checked
            .Rigs = chkRigs.Checked
            .Subsystems = chkSubsystems.Checked
            .Deployables = chkDeployables.Checked
            .Boosters = chkBoosters.Checked
            .Structures = chkStructures.Checked
            .StructureRigs = chkStructureRigs.Checked
            .Celestials = chkCelestials.Checked
            .StructureModules = chkStructureModules.Checked
            .Implants = chkImplants.Checked

            .AdvancedCapComponents = chkCapT2Components.Checked
            .AdvancedComponents = chkAdvancedComponents.Checked
            .FuelBlocks = chkFuelBlocks.Checked
            .ProtectiveComponents = chkProtectiveComponents.Checked
            .RAM = chkRAM.Checked
            .NoBuildItems = chkNobuild.Checked
            .CapitalShipComponents = chkCapitalShipComponents.Checked
            .StructureComponents = chkStructureComponents.Checked
            .SubsystemComponents = chkSubsystemComponents.Checked

            .T1 = chkItemsT1.Checked
            .T2 = chkItemsT2.Checked
            .T3 = chkItemsT3.Checked
            .Storyline = chkItemsT4.Checked
            .Faction = chkItemsT5.Checked
            .Pirate = chkItemsT6.Checked

            ' Finally get all the locations from the checked data for each main account/corp node and save
            Dim TempTree As TreeNode = AssetTree.Nodes(0)
            Dim AccountNode As TreeNode
            Dim AccountsString As String = ""
            SavedLocationIDs = New List(Of LocationInfo)
            For Each AccountNode In TempTree.Nodes
                SavedLocationIDs.AddRange(GetCheckedLocations(AccountNode, CLng(AccountNode.Name)))
                AccountsString &= CLng(AccountNode.Name) & ","
            Next

            AccountsString = AccountsString.Substring(0, Len(AccountsString) - 1)

            ' Since a lot of locations will bog down the settings loading, store in a table for this character and corporation
            Call EVEDB.BeginSQLiteTransaction()

            ' First clear out any records in there for both the account and corp assets on the account
            SQL = "DELETE FROM ASSET_LOCATIONS WHERE EnumAssetType = " & CStr(WindowForm) & " AND ID IN (" & AccountsString & ")"
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            For i = 0 To SavedLocationIDs.Count - 1
                ' See if it's in there already, and only add if not
                SQL = "SELECT 'X' FROM ASSET_LOCATIONS WHERE EnumAssetType=" & CStr(WindowForm) & " AND ID=" & CStr(SavedLocationIDs(i).AccountID)
                SQL &= " AND LocationID=" & CStr(SavedLocationIDs(i).LocationID) & " AND FlagID=" & CStr(SavedLocationIDs(i).FlagID)
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader
                rsCheck.Read()

                If Not rsCheck.HasRows Then
                    SQL = "INSERT INTO ASSET_LOCATIONS (EnumAssetType, ID, LocationID, FlagID) VALUES "
                    SQL &= "(" & CStr(WindowForm) & "," & CStr(SavedLocationIDs(i).AccountID) & "," & CStr(SavedLocationIDs(i).LocationID) & "," & CStr(SavedLocationIDs(i).FlagID) & ")"
                    Call EVEDB.ExecuteNonQuerySQL(SQL)
                End If
                rsCheck.Close()
            Next

            Call EVEDB.CommitSQLiteTransaction()

        End With

        ' Save the data in the XML file
        Call AllSettings.SaveAssetWindowSettings(TempSettings, WindowForm)

        ' Save the data to the local variable
        Select Case WindowForm
            Case AssetWindow.DefaultView
                UserAssetWindowDefaultSettings = TempSettings
            Case AssetWindow.ManufacturingTab
                UserAssetWindowManufacturingTabSettings = TempSettings
            Case AssetWindow.ShoppingList
                UserAssetWindowShoppingListSettings = TempSettings
            Case AssetWindow.ReprocessingPlant
                UserAssetWindowRefinerySettings = TempSettings
        End Select

        MsgBox("Asset Window Settings Saved", vbInformation, Application.ProductName)
        btnSaveSettings.Focus()

    End Sub

    ' Gets all the checked locations in the tree
    Private Function GetCheckedLocations(SentNode As TreeNode, AccountID As Long) As List(Of LocationInfo)
        Dim LocationIDList As New List(Of LocationInfo)
        Dim RList As New List(Of LocationInfo)
        Dim SubNode As TreeNode = Nothing

        For Each SubNode In SentNode.Nodes
            If Not IsNothing(SubNode.Tag) Then
                Dim TempPair As New LocationInfo
                Dim FlagValue As Integer = CType(SubNode.Tag, EVEAssets.TagInfo).FlagValue

                If SubNode.Checked Then
                    TempPair.FlagID = FlagValue
                    TempPair.LocationID = CLng(SubNode.Name)
                    TempPair.AccountID = AccountID

                    Call LocationIDList.Add(TempPair)
                End If

                If SubNode.Nodes.Count > 0 Then
                    RList = GetCheckedLocations(SubNode, AccountID)
                End If

                If RList.Count <> 0 Then
                    ' Add to main list
                    LocationIDList.AddRange(RList)
                End If
            End If
        Next

        Return LocationIDList

    End Function

#Region "Click Events"

    Private Sub btnCloseAssets_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseAssets.Click
        Me.Dispose()
        Me.Hide()
    End Sub

    Private Sub txtItemFilter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItemFilter.KeyDown
        Call ProcessCutCopyPasteSelect(txtItemFilter, e)
        If e.KeyCode = Keys.Enter Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub btnSearchRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchRefresh.Click
        Call RefreshTree()
    End Sub

    Private Sub btnMainRefresh_Click(sender As Object, e As EventArgs) Handles btnMainRefresh.Click
        Call RefreshTree()
    End Sub

    Private Sub rbtnCorpAssets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCorpAssets.CheckedChanged
        If rbtnCorpAssets.Checked And Not FirstLoad And Not RefreshAssetButton Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub rbtnPersonalAssets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPersonalAssets.CheckedChanged
        If rbtnPersonalAssets.Checked And Not FirstLoad And Not RefreshAssetButton Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub btnAllAssets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnAllAssets.CheckedChanged
        If rbtnAllAssets.Checked And Not FirstLoad And Not RefreshAssetButton Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub rbtnSortQuantity_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnSortQuantity.CheckedChanged
        If rbtnSortQuantity.Checked And Not FirstLoad Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub rbtnSortName_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnSortName.CheckedChanged
        If rbtnSortName.Checked And Not FirstLoad Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub btnResetItemFilter_Click(sender As System.Object, e As System.EventArgs) Handles btnResetItemFilter.Click
        txtItemFilter.Text = ""
        UpdatingChecks = True
        chkManufacturedItems.Checked = True
        chkMaterialResearchEqPrices.Checked = True
        Call CheckAllManufacturedItems()
        Call CheckAllRawItems()
        UpdatingChecks = False
        For i = 1 To TechCheckBoxes.Length - 1
            TechCheckBoxes(i).Checked = True
        Next i
    End Sub

    Private Sub chkMaterialResearchEqPrices_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMaterialResearchEqPrices.CheckedChanged
        Call CheckAllRawItems()
    End Sub

    Private Sub chkManufacturedItems_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkManufacturedItems.CheckedChanged
        Call CheckAllManufacturedItems()
    End Sub

    ' Checks or unchecks just the prices for raw material items
    Private Sub CheckAllRawItems()

        UpdatingChecks = False

        ' Check all item boxes and do not run updates
        If chkMaterialResearchEqPrices.Checked = True Then
            chkAdvancedProtectiveTechnology.Checked = True
            chkGas.Checked = True
            chkIceProducts.Checked = True
            chkMolecularForgingTools.Checked = True
            chkFactionMaterials.Checked = True
            chkNamedComponents.Checked = True
            chkMinerals.Checked = True
            chkPlanetary.Checked = True
            chkRawMaterials.Checked = True
            chkSalvage.Checked = True
            chkMisc.Checked = True
            chkBPCs.Checked = True

            chkAdvancedMats.Checked = True
            chkBoosterMats.Checked = True
            chkMolecularForgedMaterials.Checked = True
            chkPolymers.Checked = True
            chkProcessedMats.Checked = True
            chkRawMoonMats.Checked = True

            chkAncientRelics.Checked = True
            chkDatacores.Checked = True
            chkDecryptors.Checked = True
            chkRDb.Checked = True

        Else ' Turn off all item checks
            chkAdvancedProtectiveTechnology.Checked = False
            chkGas.Checked = False
            chkIceProducts.Checked = False
            chkMolecularForgingTools.Checked = False
            chkFactionMaterials.Checked = False
            chkNamedComponents.Checked = False
            chkMinerals.Checked = False
            chkPlanetary.Checked = False
            chkRawMaterials.Checked = False
            chkSalvage.Checked = False
            chkMisc.Checked = False
            chkBPCs.Checked = False

            chkAdvancedMats.Checked = False
            chkBoosterMats.Checked = False
            chkMolecularForgedMaterials.Checked = False
            chkPolymers.Checked = False
            chkProcessedMats.Checked = False
            chkRawMoonMats.Checked = False

            chkAncientRelics.Checked = False
            chkDatacores.Checked = False
            chkDecryptors.Checked = False
            chkRDb.Checked = False
        End If

        UpdatingChecks = True

    End Sub

    ' Checks or unchecks just the prices for manufactured items
    Private Sub CheckAllManufacturedItems()

        UpdatingChecks = False

        ' Check all item boxes and do not run updates
        If chkManufacturedItems.Checked = True Then
            chkShips.Checked = True
            chkCharges.Checked = True
            chkModules.Checked = True
            chkDrones.Checked = True
            chkRigs.Checked = True
            chkSubsystems.Checked = True
            chkDeployables.Checked = True
            chkBoosters.Checked = True
            chkStructures.Checked = True
            chkStructureRigs.Checked = True
            chkCelestials.Checked = True
            chkStructureModules.Checked = True
            chkImplants.Checked = True

            chkCapT2Components.Checked = True
            chkAdvancedComponents.Checked = True
            chkFuelBlocks.Checked = True
            chkProtectiveComponents.Checked = True
            chkRAM.Checked = True
            chkCapitalShipComponents.Checked = True
            chkStructureComponents.Checked = True
            chkSubsystemComponents.Checked = True

        Else ' Turn off all item checks
            chkShips.Checked = False
            chkCharges.Checked = False
            chkModules.Checked = False
            chkDrones.Checked = False
            chkRigs.Checked = False
            chkSubsystems.Checked = False
            chkDeployables.Checked = False
            chkBoosters.Checked = False
            chkStructures.Checked = False
            chkStructureRigs.Checked = False
            chkCelestials.Checked = False
            chkStructureModules.Checked = False
            chkImplants.Checked = False

            chkCapT2Components.Checked = False
            chkAdvancedComponents.Checked = False
            chkFuelBlocks.Checked = False
            chkProtectiveComponents.Checked = False
            chkRAM.Checked = False
            chkCapitalShipComponents.Checked = False
            chkStructureComponents.Checked = False
            chkSubsystemComponents.Checked = False

        End If

        UpdatingChecks = True

    End Sub

    ' Makes sure a tech is enabled and checked for items that require tech based on saved values, not current due to disabling form
    Private Function CheckTechChecks() As Boolean

        If PriceCheckT1Enabled Then
            If TechCheckBoxes(1).Checked Then
                Return True
            End If
        End If

        If PriceCheckT2Enabled Then
            If TechCheckBoxes(2).Checked Then
                Return True
            End If
        End If

        If PriceCheckT3Enabled Then
            If TechCheckBoxes(3).Checked Then
                Return True
            End If
        End If

        If PriceCheckT4Enabled Then
            If TechCheckBoxes(4).Checked Then
                Return True
            End If
        End If

        If PriceCheckT5Enabled Then
            If TechCheckBoxes(5).Checked Then
                Return True
            End If
        End If

        If PriceCheckT6Enabled Then
            If TechCheckBoxes(6).Checked Then
                Return True
            End If
        End If

        Return False

    End Function

    ' Updates the T1, T2 and T3 check boxes depending on item selections
    Private Sub UpdateTechChecks()
        Dim T1 As Boolean = False
        Dim T2 As Boolean = False
        Dim T3 As Boolean = False
        Dim Storyline As Boolean = False
        Dim Navy As Boolean = False
        Dim Pirate As Boolean = False

        Dim ItemsSelected As Boolean = False
        Dim i As Integer
        Dim TechChecks As Boolean = False

        ' For check all 
        If Not UpdatingChecks And UpdateAllTechChecks Then
            UpdateAllTechChecks = False
            ' Check all and leave
            For i = 1 To TechCheckBoxes.Length - 1
                TechCheckBoxes(i).Enabled = True
                ' Check this one and leave
                TechCheckBoxes(i).Checked = True
            Next i
            Exit Sub
        End If

        ' Check each item checked and set the check boxes accordingly
        If chkShips.Checked Then
            T1 = True
            T2 = True
            T3 = True
            Navy = True
            Pirate = True
            ItemsSelected = True
        End If

        If chkModules.Checked Then
            T1 = True
            T2 = True
            Navy = True
            Storyline = True
            ItemsSelected = True
        End If

        If chkSubsystems.Checked Then
            T3 = True
            ItemsSelected = True
        End If

        If chkDrones.Checked Then
            T1 = True
            T2 = True
            ItemsSelected = True
        End If

        If chkRigs.Checked Then
            T1 = True
            T2 = True
            ItemsSelected = True
        End If

        If chkBoosters.Checked Then
            T1 = True
            ItemsSelected = True
        End If

        If chkStructures.Checked Then
            T1 = True
            ItemsSelected = True
        End If

        If chkCharges.Checked Then
            T1 = True
            T2 = True
            ItemsSelected = True
        End If

        ' If none are checked, then uncheck and un-enable all
        If ItemsSelected Then

            ' Enable the Checks
            If T1 Then
                chkItemsT1.Enabled = True
            Else
                chkItemsT1.Enabled = False
            End If

            If T2 Then
                chkItemsT2.Enabled = True
            Else
                chkItemsT2.Enabled = False
            End If

            If T3 Then
                chkItemsT3.Enabled = True
            Else
                chkItemsT3.Enabled = False
            End If

            If Storyline Then
                chkItemsT4.Enabled = True
            Else
                chkItemsT4.Enabled = False
            End If

            If Navy Then
                chkItemsT5.Enabled = True
            Else
                chkItemsT5.Enabled = False
            End If

            If Pirate Then
                chkItemsT6.Enabled = True
            Else
                chkItemsT6.Enabled = False
            End If

            ' Make sure we have at le=t one checked
            For i = 1 To TechCheckBoxes.Length - 1
                If TechCheckBoxes(i).Enabled Then
                    If TechCheckBoxes(i).Checked Then
                        TechChecks = True
                        ' Found one enabled and checked, so leave for
                        Exit For
                    End If
                End If
            Next i

            If Not TechChecks Then
                ' Need to check at le=t one
                For i = 1 To TechCheckBoxes.Length - 1
                    If TechCheckBoxes(i).Enabled Then
                        ' Check this one and leave
                        TechCheckBoxes(i).Checked = True
                    End If
                Next i
            End If

        Else
            chkItemsT1.Enabled = False
            chkItemsT2.Enabled = False
            chkItemsT3.Enabled = False
            chkItemsT4.Enabled = False
            chkItemsT5.Enabled = False
            chkItemsT6.Enabled = False
        End If

        ' Save status of the Tech check boxes
        PriceCheckT1Enabled = chkItemsT1.Enabled
        PriceCheckT2Enabled = chkItemsT2.Enabled
        PriceCheckT3Enabled = chkItemsT3.Enabled
        PriceCheckT4Enabled = chkItemsT4.Enabled
        PriceCheckT5Enabled = chkItemsT5.Enabled
        PriceCheckT6Enabled = chkItemsT6.Enabled

    End Sub

    Private Sub TechChecks_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemsT1.CheckedChanged, chkItemsT2.CheckedChanged, chkItemsT3.CheckedChanged,
                                                                                    chkItemsT4.CheckedChanged, chkItemsT5.CheckedChanged, chkItemsT6.CheckedChanged
        UpdateTechChecks()
    End Sub

    Private Sub cmbPriceShipTypes_DropDown(sender As Object, e As System.EventArgs) Handles cmbPriceShipTypes.DropDown
        If FirstPriceShipTypesComboLoad Then
            Call LoadPriceShipTypes()
            FirstPriceShipTypesComboLoad = False
        End If
    End Sub

    Private Sub cmbPriceChargeTypes_DropDown(sender As Object, e As System.EventArgs) Handles cmbPriceChargeTypes.DropDown
        If FirstPriceChargeTypesComboLoad Then
            Call LoadPriceChargeTypes()
            FirstPriceChargeTypesComboLoad = False
        End If
    End Sub

    Private Sub LoadPriceShipTypes()
        Dim SQL As String
        Dim readerShipType As SQLiteDataReader

        ' Load the select systems combobox with systems
        SQL = "SELECT groupName from inventory_types, inventory_groups, inventory_categories "
        SQL = SQL & "WHERE  inventory_types.groupID = inventory_groups.groupID "
        SQL = SQL & "AND inventory_groups.categoryID = inventory_categories.categoryID "
        SQL = SQL & "AND categoryname = 'Ship' AND groupName NOT IN ('Rookie ship','Prototype Exploration Ship') "
        SQL = SQL & "AND inventory_types.published <> 0 and inventory_groups.published <> 0 and inventory_categories.published <> 0 "
        SQL = SQL & "GROUP BY groupName "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerShipType = DBCommand.ExecuteReader

        cmbPriceShipTypes.Items.Add("All Ship Types")

        While readerShipType.Read
            cmbPriceShipTypes.Items.Add(readerShipType.GetString(0))
        End While

        readerShipType.Close()
        readerShipType = Nothing
        DBCommand = Nothing

        cmbPriceShipTypes.Text = "All Ship Types"

    End Sub

    Private Sub LoadPriceChargeTypes()
        Dim SQL As String
        Dim readerChargeType As SQLiteDataReader

        ' Load the select systems combobox with systems
        SQL = "SELECT groupName from inventory_types, inventory_groups, inventory_categories "
        SQL = SQL & "WHERE  inventory_types.groupID = inventory_groups.groupID "
        SQL = SQL & "AND inventory_groups.categoryID = inventory_categories.categoryID "
        SQL = SQL & "AND categoryname = 'Charge' "
        SQL = SQL & "AND inventory_types.published <> 0 and inventory_groups.published <> 0 and inventory_categories.published <> 0 "
        SQL = SQL & "GROUP BY groupName "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerChargeType = DBCommand.ExecuteReader

        cmbPriceChargeTypes.Items.Add("All Charge Types")

        While readerChargeType.Read
            cmbPriceChargeTypes.Items.Add(readerChargeType.GetString(0))
        End While

        readerChargeType.Close()
        readerChargeType = Nothing
        DBCommand = Nothing

        cmbPriceChargeTypes.Text = "All Charge Types"

    End Sub

    Private Sub chkBoosters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoosters.CheckedChanged
        Call UpdateTechChecks()
    End Sub

    Private Sub chkRigs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRigs.CheckedChanged
        Call UpdateTechChecks()
    End Sub

    Private Sub chkShips_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShips.CheckedChanged

        If chkShips.Checked = True Then
            cmbPriceShipTypes.Enabled = True
        ElseIf chkShips.Checked = False Then
            cmbPriceShipTypes.Enabled = False
        End If

        Call UpdateTechChecks()

    End Sub

    Private Sub chkModules_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkModules.CheckedChanged
        Call UpdateTechChecks()
    End Sub

    Private Sub chkDrones_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDrones.CheckedChanged
        Call UpdateTechChecks()
    End Sub

    Private Sub chkCharges_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCharges.CheckedChanged

        If chkCharges.Checked = True Then
            cmbPriceChargeTypes.Enabled = True
        ElseIf chkCharges.Checked = False Then
            cmbPriceChargeTypes.Enabled = False
        End If

        Call UpdateTechChecks()

    End Sub

    Private Sub chkSubsystems_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSubsystems.CheckedChanged
        Call UpdateTechChecks()
    End Sub

    Private Sub chkStructures_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStructures.CheckedChanged
        Call UpdateTechChecks()
    End Sub

    Private Sub rbtnAllItems_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnAllItems.CheckedChanged
        If rbtnAllItems.Checked Then
            ' Don't enable checks
            gbRawMaterials.Enabled = False
            gbManufacturedItems.Enabled = False
        End If
    End Sub

    Private Sub rbtnBPMats_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBPMats.CheckedChanged
        If rbtnBPMats.Checked Then
            ' Enable checks
            gbRawMaterials.Enabled = True
            gbManufacturedItems.Enabled = True
        End If
    End Sub

    Private Sub ToggleNodeChecks(SentNode As TreeNode, Check As Boolean)
        ActiveForm.Cursor = Cursors.WaitCursor
        For Each child As TreeNode In SentNode.Nodes
            Application.DoEvents()
            child.Checked = Check
            If child.Nodes.Count > 0 Then ToggleNodeChecks(child, Check)
        Next
        ActiveForm.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub mnuCheckAll_Click(sender As Object, e As EventArgs) Handles mnuCheckAll.Click
        AssetTree.SelectedNode.Checked = True
        Call ToggleNodeChecks(AssetTree.SelectedNode, True)
    End Sub

    Private Sub mnuUncheckAll_Click(sender As Object, e As EventArgs) Handles mnuUncheckAll.Click
        AssetTree.SelectedNode.Checked = False
        Call ToggleNodeChecks(AssetTree.SelectedNode, False)
    End Sub

    Private Sub mnuExpandNodes_Click(sender As Object, e As EventArgs) Handles mnuExpandNodes.Click
        Call AssetTree.SelectedNode.ExpandAll()
    End Sub

    Private Sub mnuCollapseNodes_Click(sender As Object, e As EventArgs) Handles mnuCollapseNodes.Click
        Call AssetTree.SelectedNode.Collapse(False)
    End Sub

    Private Sub AssetTree_MouseDown(sender As Object, e As MouseEventArgs) Handles AssetTree.MouseDown
        If e.Button = MouseButtons.Right Then
            AssetTree.SelectedNode = AssetTree.GetNodeAt(e.X, e.Y)
        End If
    End Sub

    Private Sub AccountToggle_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSelectedAccount.CheckedChanged, rbtnMultiAccounts.CheckedChanged
        If rbtnSelectedAccount.Checked Then
            lstCharacters.Enabled = False
        ElseIf rbtnMultiAccounts.Checked Then
            lstCharacters.Enabled = True
        End If
    End Sub

#End Region

End Class