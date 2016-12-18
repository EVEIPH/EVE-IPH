
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
    Private TechCheckBoxes() As CheckBox
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

    Private AnchorNode As TreeNode

    'Private AssetTreeTest As TriStateTreeView

    ' For drawing checkboxes
    Private Const TVIF_STATE As Integer = &H8
    Private Const TVIS_STATEIMAGEMASK As Integer = &HF000
    Private Const TV_FIRST As Integer = &H1100
    Private Const TVM_SETITEM As Integer = TV_FIRST + 63

#Region "Special processing for checks"
    <StructLayout(LayoutKind.Sequential)> _
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
        End Select

        ' For this window, get the asset locations saved for the character
        SavedLocationIDs = SelectedCharacter.GetAssetLocationIDs(WindowForm)

        ' For the disabling of the price update form
        PriceCheckT1Enabled = True
        PriceCheckT2Enabled = True
        PriceCheckT3Enabled = True
        PriceCheckT4Enabled = True
        PriceCheckT5Enabled = True
        PriceCheckT6Enabled = True

        AssetTree.DrawMode = TreeViewDrawMode.OwnerDrawAll
        AssetTree.CheckBoxes = True

    End Sub

    Private Sub frmAssetsViewer_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ' If we have no assets, then refresh the table to show that
        If SelectedCharacter.GetAssets.GetAssetCount = 0 Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub frmAssetsViewer_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Call InitForm()
    End Sub

    ' Initialize the form based on user settings
    Private Sub InitForm()

        Application.DoEvents()

        FirstLoad = True

        btnToggleExpand.Visible = True
        btnToggleRetract.Visible = False

        Timer1.Interval = 1000 ' 1 second
        Timer1.Enabled = True

        FirstPriceChargeTypesComboLoad = True
        FirstPriceShipTypesComboLoad = True

        btnScanCorpAssets.Enabled = False
        btnScanPersonalAssets.Enabled = False

        ' Create the controls collection class
        m_ControlsCollection = New ControlsCollection(Me)
        ' Get Region check boxes (note index starts at 1)
        TechCheckBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "chkItemsT"), CheckBox())

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

        If UserApplicationSettings.ShowToolTips Then
            ttMain.SetToolTip(btnToggleExpand, "Expand all Assets")
            ttMain.SetToolTip(btnToggleRetract, "Retract all Assets")
            ttMain.SetToolTip(chkToggle, "Check/Uncheck all Assets")
        End If

        ' Search settings form
        If SelectedSettings.AllItems Then
            rbtnAllItems.Checked = True
        Else
            rbtnBPMats.Checked = True
        End If

        txtItemFilter.Text = SelectedSettings.ItemFilterText

        ' Load the search settings
        With SelectedSettings
            chkRawMaterialItems.Checked = .AllRawMats
            chkMinerals.Checked = .Minerals
            chkIceProducts.Checked = .IceProducts
            chkGas.Checked = .Gas
            chkMisc.Checked = .Misc
            chkBPCs.Checked = .BPCs
            chkAncientRelics.Checked = .AncientRelics
            chkAncientSalvage.Checked = .AncientSalvage
            chkSalvage.Checked = .Salvage
            chkPlanetary.Checked = .Planetary
            chkDatacores.Checked = .Datacores
            chkDecryptors.Checked = .Decryptors
            chkRawMats.Checked = .RawMats
            chkProcessedMats.Checked = .ProcessedMats
            chkAdvancedMats.Checked = .AdvancedMats
            chkMatsandCompounds.Checked = .MatsandCompounds
            chkDroneComponents.Checked = .DroneComponents
            chkBoosterMats.Checked = .BoosterMats
            chkPolymers.Checked = .Polymers
            chkAsteroids.Checked = .Asteroids

            chkManufacturedItems.Checked = .AllManufacturedItems
            chkShips.Checked = .Ships
            chkModules.Checked = .Modules
            chkDrones.Checked = .Drones
            chkBoosters.Checked = .Boosters
            chkRigs.Checked = .Rigs
            chkCharges.Checked = .Charges
            chkSubsystems.Checked = .Subsystems
            chkStructures.Checked = .Structures
            chkTools.Checked = .Tools
            chkCelestials.Checked = .Celestials
            chkDeployables.Checked = .Deployables
            chkImplants.Checked = .Implants
            chkStationComponents.Checked = .StationComponents
            chkDataInterfaces.Checked = .DataInterfaces
            chkCapT2Components.Checked = .CapT2Components
            chkCapitalComponents.Checked = .CapitalComponents
            chkComponents.Checked = .Components
            chkHybrid.Checked = .Hybrid
            chkFuelBlocks.Checked = .FuelBlocks

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

    End Sub

    ' Returns true if an selected item is checked
    Private Function ItemsChecked() As Boolean

        If chkMinerals.Checked Then
            Return True
        ElseIf chkIceProducts.Checked Then
            Return True
        ElseIf chkGas.Checked Then
            Return True
        ElseIf chkMisc.Checked Then
            Return True
        ElseIf chkBPCs.Checked Then
            Return True
        ElseIf chkAncientRelics.Checked Then
            Return True
        ElseIf chkAncientSalvage.Checked Then
            Return True
        ElseIf chkSalvage.Checked Then
            Return True
        ElseIf chkStationComponents.Checked Then
            Return True
        ElseIf chkPlanetary.Checked Then
            Return True
        ElseIf chkDatacores.Checked Then
            Return True
        ElseIf chkDecryptors.Checked Then
            Return True
        ElseIf chkRawMats.Checked Then
            Return True
        ElseIf chkProcessedMats.Checked Then
            Return True
        ElseIf chkAdvancedMats.Checked Then
            Return True
        ElseIf chkMatsandCompounds.Checked Then
            Return True
        ElseIf chkDroneComponents.Checked Then
            Return True
        ElseIf chkBoosterMats.Checked Then
            Return True
        ElseIf chkPolymers.Checked Then
            Return True
        ElseIf chkAsteroids.Checked Then
            Return True
        ElseIf chkShips.Checked Then
            Return True
        ElseIf chkModules.Checked Then
            Return True
        ElseIf chkDrones.Checked Then
            Return True
        ElseIf chkBoosters.Checked Then
            Return True
        ElseIf chkRigs.Checked Then
            Return True
        ElseIf chkCharges.Checked Then
            Return True
        ElseIf chkSubsystems.Checked Then
            Return True
        ElseIf chkStructures.Checked Then
            Return True
        ElseIf chkTools.Checked Then
            Return True
        ElseIf chkDataInterfaces.Checked Then
            Return True
        ElseIf chkCapT2Components.Checked Then
            Return True
        ElseIf chkCapitalComponents.Checked Then
            Return True
        ElseIf chkComponents.Checked Then
            Return True
        ElseIf chkHybrid.Checked Then
            Return True
        ElseIf chkFuelBlocks.Checked Then
            Return True
        ElseIf chkStationComponents.Checked Then
            Return True
        ElseIf chkCelestials.Checked Then
            Return True
        ElseIf chkDeployables.Checked Then
            Return True
        ElseIf chkImplants.Checked Then
            Return True
        End If

        ' If we got here, nothing checked
        Return False

    End Function

    ' Main function that refresh's the tree
    Public Sub RefreshTree()
        Dim BaseNode As New TreeNode
        Dim SortOption As SortType
        Dim SearchItemList As List(Of Long)
        Dim OnlyBPCs As Boolean ' Pass through if the BPIDs we sent need to only be shown if a copy

        Application.UseWaitCursor = True
        Application.DoEvents()

        ' Make sure they have an item selected
        If Not ItemsChecked() And Not rbtnAllItems.Checked Then
            MsgBox("You must select an item category to display.", vbExclamation, Application.ProductName)
            tabMain.SelectedTab = tabSearchSettings
            Me.Cursor = Cursors.Default
            Application.UseWaitCursor = False
            Exit Sub
        End If

        ' Set the tree object
        AssetTree.Update()
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

        ' Add the base node
        AnchorNode = AssetTree.Nodes.Add("Asset List")

        ' If we get nothing back from the search item list, then just clear the assets and exit - we have no items to display
        If IsNothing(SearchItemList) Then
            AssetTree.EndUpdate()
            AssetTree.Refresh()

            Application.UseWaitCursor = False
            Application.DoEvents()
            MsgBox("No items found.", vbInformation, Application.ProductName)
            Me.Refresh()
            Exit Sub
        End If

        ' Get the base node of the full tree (may want to save these options globally so we don't need to load them every time)
        If rbtnPersonalAssets.Checked Or rbtnAllAssets.Checked Then
            BaseNode = SelectedCharacter.GetAssets.GetAssetTreeAnchorNode(SortOption, SearchItemList, "Personal Assets", SelectedCharacter.ID, SavedLocationIDs, OnlyBPCs)
            ' Need to add it to the tree but as a clone
            AnchorNode.Nodes.Add(CType(BaseNode.Clone, TreeNode))
        End If
        If rbtnCorpAssets.Checked Or rbtnAllAssets.Checked Then
            BaseNode = SelectedCharacter.CharacterCorporation.GetAssets.GetAssetTreeAnchorNode(SortOption, SearchItemList, "Corporation Assets", SelectedCharacter.CharacterCorporation.CorporationID, SavedLocationIDs, OnlyBPCs)
            ' Need to add it to the tree but as a clone
            AnchorNode.Nodes.Add(CType(BaseNode.Clone, TreeNode))
        End If

        ' Update
        AssetTree.EndUpdate()
        AssetTree.Refresh()

        ' Open up the top node and the personal/corp nodes. Plus reset toggle since we just reloaded
        ToggleAllOpen = False
        AssetTree.TopNode.Expand()
        AssetTree.Nodes(0).Nodes(0).Expand()
        If rbtnAllAssets.Checked Then
            AssetTree.Nodes(0).Nodes(1).Expand()
        End If

        ' Expand all parents for check boxes that have values checked
        Call ExpandCheckedNodes(AssetTree.Nodes(0).Nodes, AssetTree)

        ' Scroll to top
        AssetTree.TopNode = AssetTree.Nodes(0)

        Application.UseWaitCursor = False
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
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        ' If we want all items, look in inventory types with links to groups/categories
        If SearchAllItems Then
            ' Get the item id from prices, since these are items we can set a price for and will be used in building items
            SQL = "SELECT typeID AS ITEM_ID FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_CATEGORIES "
            SQL = SQL & "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
            SQL = SQL & "AND INVENTORY_GROUPS.categoryID = INVENTORY_CATEGORIES.categoryID "

            ' Search based on text
            If txtItemFilter.Text <> "" Then
                SQL = SQL & " AND typeName LIKE '%" & FormatDBString(Trim(txtItemFilter.Text)) & "%' "
            End If

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerMats = DBCommand.ExecuteReader

            ' Fill list
            While readerMats.Read
                Call ItemIDList.Add(readerMats.GetInt64(0))
            End While

            readerMats.Close()
            readerMats = Nothing
            DBCommand = Nothing

        Else
            ' Want just building materials (from prices)
            ' Get the item id from prices, since these are items we can set a price for and will be used in building items
            SQL = "SELECT ITEM_ID FROM ITEM_PRICES, INVENTORY_TYPES"
            SQL = SQL & " WHERE ITEM_PRICES.ITEM_ID = INVENTORY_TYPES.typeID AND ("

            ' Raw materials - non-manufacturable
            If chkMinerals.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Mineral' OR "
                ItemChecked = True
            End If
            If chkIceProducts.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Ice Product' OR "
                ItemChecked = True
            End If
            If chkPlanetary.Checked Then
                SQL = SQL & "(ITEM_CATEGORY LIKE 'Planetary%' OR ITEM_NAME IN ('Oxygen','Water')) OR "
                ItemChecked = True
            End If
            If chkDatacores.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Datacores' OR "
                ItemChecked = True
            End If
            If chkDecryptors.Checked Then
                SQL = SQL & "ITEM_GROUP LIKE '%Decryptor%' OR " ' Storyline decryptors are category 'Commodity'
                ItemChecked = True
            End If
            If chkGas.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Harvestable Cloud' OR "
                ItemChecked = True
            End If
            If chkBPCs.Checked Then
                SQL = SQL & "ITEM_CATEGORY = 'Blueprint' OR "
                ItemChecked = True
            End If
            If chkMisc.Checked Then
                SQL = SQL & "(" & "ITEM_GROUP IN ('General','Livestock','Radioactive','Biohazard','Commodities', 'Miscellaneous') AND ITEM_NAME NOT IN ('Oxygen','Water')) OR "
                ItemChecked = True
            End If
            If chkSalvage.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Salvaged Materials' OR "
                ItemChecked = True
            End If
            If chkAncientSalvage.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Ancient Salvage' OR "
                ItemChecked = True
            End If
            If chkAncientRelics.Checked Then
                SQL = SQL & "ITEM_CATEGORY = 'Ancient Relics' OR "
                ItemChecked = True
            End If
            If chkPolymers.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Hybrid Polymers' OR "
                ItemChecked = True
            End If
            If chkRawMats.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Moon Materials' OR "
                ItemChecked = True
            End If
            If chkProcessedMats.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Intermediate Materials' OR "
                ItemChecked = True
            End If
            If chkAdvancedMats.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Composite' OR "
                ItemChecked = True
            End If
            If chkMatsandCompounds.Checked Then
                SQL = SQL & "ITEM_GROUP IN ('Materials and Compounds', 'Artifacts and Prototypes') OR "
                ItemChecked = True
            End If
            If chkDroneComponents.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Rogue Drone Components' OR ITEM_NAME = 'Elite Drone AI' OR "
                ItemChecked = True
            End If
            If chkBoosterMats.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Biochemical Material' OR "
                ItemChecked = True
            End If
            If chkStationComponents.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Station Components' OR "
                ItemChecked = True
            End If
            If chkAsteroids.Checked Then
                SQL = SQL & "ITEM_CATEGORY = 'Asteroid' OR "
                ItemChecked = True
            End If

            ' Other Manufacturables
            If chkCapT2Components.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Advanced Capital Construction Components' OR "
                ItemChecked = True
            End If
            If chkCapitalComponents.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Capital Construction Components' OR "
                ItemChecked = True
            End If
            If chkComponents.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Construction Components' OR "
                ItemChecked = True
            End If
            If chkHybrid.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Hybrid Tech Components' OR "
                ItemChecked = True
            End If
            If chkTools.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Tool' OR "
                ItemChecked = True
            End If
            If chkDataInterfaces.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Data Interfaces' OR "
                ItemChecked = True
            End If
            If chkFuelBlocks.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Fuel Block' OR "
                ItemChecked = True
            End If
            If chkImplants.Checked Then
                SQL = SQL & "ITEM_GROUP = 'Cyberimplant' OR "
                ItemChecked = True
            End If
            If chkDeployables.Checked Then
                SQL = SQL & "ITEM_CATEGORY = 'Deployable' OR "
                ItemChecked = True
            End If
            If chkCelestials.Checked Then
                SQL = SQL & "(ITEM_CATEGORY IN ('Celestial','Orbitals','Sovereignty Structures', 'Station','Accessories','Infrastructure Upgrades') AND ITEM_GROUP <> 'Harvestable Cloud') OR "
                ItemChecked = True
            End If

            ' Manufactured Items
            If (chkShips.Checked Or chkModules.Checked Or chkDrones.Checked Or chkBoosters.Checked Or chkRigs.Checked Or chkSubsystems.Checked Or chkStructures.Checked Or chkCharges.Checked) Then

                ' Make sure we have at least one tech checked that is enabled
                TechChecked = CheckTechChecks()

                If Not TechChecked And Not ItemChecked Then
                    ' There isn't an item checked before this and these items all require tech, so exit
                    ItemChecked = False
                Else
                    ItemChecked = True
                End If

                ' If they choose a tech level, then build this part of the SQL query
                If TechChecked Then
                    If PriceCheckT1Enabled Then
                        If chkItemsT1.Checked Then
                            ' Add to SQL query for tech level
                            TechSQL = TechSQL & "ITEM_TYPE = 1 OR "
                        End If
                    End If

                    If PriceCheckT2Enabled Then
                        If chkItemsT2.Checked Then
                            ' Add to SQL query for tech level
                            TechSQL = TechSQL & "ITEM_TYPE = 2 OR "
                        End If
                    End If

                    If PriceCheckT3Enabled Then
                        If chkItemsT3.Checked Then
                            ' Add to SQL query for tech level
                            TechSQL = TechSQL & "ITEM_TYPE = 14 OR "
                        End If
                    End If

                    ' Add the Pirate, Storyline, Navy search string
                    ' Storyline
                    If PriceCheckT4Enabled Then
                        If chkItemsT4.Checked Then
                            ' Add to SQL query for tech level
                            TechSQL = TechSQL & "ITEM_TYPE = 3 OR "
                        End If
                    End If

                    ' Navy
                    If PriceCheckT5Enabled Then
                        If chkItemsT5.Checked Then
                            ' Add to SQL query for tech level
                            TechSQL = TechSQL & "ITEM_TYPE = 16 OR "
                        End If
                    End If

                    ' Pirate
                    If PriceCheckT6Enabled Then
                        If chkItemsT6.Checked Then
                            ' Add to SQL query for tech level
                            TechSQL = TechSQL & "ITEM_TYPE = 15 OR "
                        End If
                    End If

                    ' Format TechSQL - Add on Meta codes
                    If TechSQL <> "" Then
                        TechSQL = "(" & TechSQL.Substring(0, TechSQL.Length - 3) & " OR ITEM_NAME IN (21,22,23,24))"
                    End If

                    ' Build Tech 1,2,3 Manufactured Items
                    If chkCharges.Checked Then
                        SQL = SQL & "(ITEM_CATEGORY= 'Charge' AND " & TechSQL
                        If cmbPriceChargeTypes.Text <> "All Charge Types" Then
                            SQL = SQL & " AND ITEM_GROUP = '" & cmbPriceChargeTypes.Text & "'"
                        End If
                        SQL = SQL & ") OR "
                    End If

                    If chkDrones.Checked Then
                        SQL = SQL & "(ITEM_CATEGORY IN ('Drone', 'Fighter') AND " & TechSQL & ") OR "
                    End If

                    If chkModules.Checked Then ' Not rigs but Modules
                        SQL = SQL & "(ITEM_CATEGORY IN ('Module', 'Deployable') AND " & "ITEM_GROUP NOT LIKE 'Rig%' AND " & TechSQL & ") OR "
                    End If

                    If chkShips.Checked Then
                        SQL = SQL & "(ITEM_CATEGORY= 'Ship' AND " & TechSQL
                        If cmbPriceShipTypes.Text <> "All Ship Types" Then
                            SQL = SQL & " AND ITEM_GROUP = '" & cmbPriceShipTypes.Text & "'"
                        End If
                        SQL = SQL & ") OR "
                    End If

                    If chkSubsystems.Checked Then
                        SQL = SQL & "(ITEM_CATEGORY= 'Subsystem' AND " & TechSQL & ") OR "
                    End If

                    If chkBoosters.Checked Then
                        SQL = SQL & "(ITEM_GROUP = 'Booster' AND " & TechSQL & ") OR "
                    End If

                    If chkRigs.Checked Then ' Rigs
                        SQL = SQL & "(ITEM_CATEGORY = 'Module' AND ITEM_GROUP LIKE 'Rig%' AND " & TechSQL & ") OR "
                    End If

                    If chkStructures.Checked Then
                        SQL = SQL & "(ITEM_CATEGORY IN ('Starbase', 'Structure Module') AND " & TechSQL & ") OR "
                    End If
                Else
                    ' No tech level chosen, so just continue with other options and skip these that require a tech selection
                End If
            End If

            ' Leave function if no items checked
            If ItemChecked Then
                ' Take off last OR and add the final )
                SQL = SQL.Substring(0, SQL.Length - 4)
                SQL = SQL & ")"

                ' Search based on text
                If txtItemFilter.Text <> "" Then
                    SQL = SQL & " AND ITEM_NAME LIKE '%" & FormatDBString(Trim(txtItemFilter.Text)) & "%' "
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerMats = DBCommand.ExecuteReader

                ' Fill list
                While readerMats.Read
                    Call ItemIDList.Add(readerMats.GetInt64(0))
                End While

                readerMats.Close()
                readerMats = Nothing
                DBCommand = Nothing
            End If

            ' Blueprint Copies
            If chkBPCs.Checked And Not SearchAllItems Then
                ' Look up the typeIDs for all BPs and add them to the list
                SQL = "SELECT BLUEPRINT_ID, ITEM_NAME FROM ALL_BLUEPRINTS "
                ' Search based on text
                If txtItemFilter.Text <> "" Then
                    SQL = SQL & " WHERE ITEM_NAME LIKE '%" & FormatDBString(Trim(txtItemFilter.Text)) & "%' "
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerMats = DBCommand.ExecuteReader

                ' Fill list
                While readerMats.Read
                    Call ItemIDList.Add(readerMats.GetInt64(0))
                End While

                readerMats.Close()
                readerMats = Nothing
                DBCommand = Nothing

            End If

        End If

        Me.Cursor = Cursors.Default
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
            SelectedCharacter.GetAssets.LoadAssets(ScanType.Personal, True)
        Else
            SelectedCharacter.CharacterCorporation.GetAssets.LoadAssets(ScanType.Corporation, True)
        End If

        ' Reload the tree
        Call RefreshTree()

    End Sub

    ' Will use CAK and scan for bps in the user's items and store a temp table of these bps for loading in the grid
    Private Sub btnScanPersonalBPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanPersonalAssets.Click

        RefreshAssetButton = True
        Me.Cursor = Cursors.WaitCursor
        If rbtnAllAssets.Checked = False Then
            rbtnPersonalAssets.Checked = True
        End If

        Application.DoEvents()

        If SelectedCharacter.AssetsAccess Then
            Call ScanForAssets(ScanType.Personal)
        Else
            MsgBox("You have not enabled access to Assets with this key.", vbExclamation, Application.ProductName)
        End If

        Me.Cursor = Cursors.Default
        Application.DoEvents()

        RefreshAssetButton = False

    End Sub

    ' Will use CAK and scan for bps in the corps items and store a temp table of these bps for loading in the grid
    Private Sub btnScanCorpBPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanCorpAssets.Click

        RefreshAssetButton = True
        Me.Cursor = Cursors.WaitCursor
        If rbtnAllAssets.Checked = False Then
            rbtnCorpAssets.Checked = True
        End If

        Application.DoEvents()

        If SelectedCharacter.CharacterCorporation.AssetAccess Then
            Call ScanForAssets(ScanType.Corporation)
        Else
            MsgBox("You do not have a corporation key installed with access to Assets.", vbExclamation, Application.ProductName)
        End If

        Me.Cursor = Cursors.Default
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

    ' Saves the settings on both tabs for the asset window
    Private Sub SaveWindowSettings()
        Dim TempSettings As AssetWindowSettings = Nothing
        Dim TempLocationIDs As New List(Of Long)
        Dim SQL As String

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

            .AllRawMats = chkRawMaterialItems.Checked
            .Minerals = chkMinerals.Checked
            .IceProducts = chkIceProducts.Checked
            .Gas = chkGas.Checked
            .Misc = chkMisc.Checked
            .BPCs = chkBPCs.Checked
            .AncientRelics = chkAncientRelics.Checked
            .AncientSalvage = chkAncientSalvage.Checked
            .Salvage = chkSalvage.Checked
            .StationComponents = chkStationComponents.Checked
            .Planetary = chkPlanetary.Checked
            .Datacores = chkDatacores.Checked
            .Decryptors = chkDecryptors.Checked
            .RawMats = chkRawMats.Checked
            .ProcessedMats = chkProcessedMats.Checked
            .AdvancedMats = chkAdvancedMats.Checked
            .MatsandCompounds = chkMatsandCompounds.Checked
            .DroneComponents = chkDroneComponents.Checked
            .BoosterMats = chkBoosterMats.Checked
            .Polymers = chkPolymers.Checked
            .Asteroids = chkAsteroids.Checked
            .AllManufacturedItems = chkManufacturedItems.Checked
            .Ships = chkShips.Checked
            .Modules = chkModules.Checked
            .Drones = chkDrones.Checked
            .Boosters = chkBoosters.Checked
            .Rigs = chkRigs.Checked
            .Charges = chkCharges.Checked
            .Subsystems = chkSubsystems.Checked
            .Structures = chkStructures.Checked
            .Tools = chkTools.Checked
            .DataInterfaces = chkDataInterfaces.Checked
            .CapT2Components = chkCapT2Components.Checked
            .CapitalComponents = chkCapitalComponents.Checked
            .Components = chkComponents.Checked
            .Hybrid = chkHybrid.Checked
            .FuelBlocks = chkFuelBlocks.Checked
            .T1 = chkItemsT1.Checked
            .T2 = chkItemsT2.Checked
            .T3 = chkItemsT3.Checked
            .Storyline = chkItemsT4.Checked
            .Faction = chkItemsT5.Checked
            .Pirate = chkItemsT6.Checked
            .Celestials = chkCelestials.Checked
            .Deployables = chkDeployables.Checked
            .Implants = chkImplants.Checked

            ' Finally get all the locations from the checked data move from saved
            SavedLocationIDs = GetCheckedLocations(AssetTree.Nodes(0))

            ' Since a lot of locations will bog down the settings loading, store in a table for this character and corporation
            Call EVEDB.BeginSQLiteTransaction()

            ' First clear out any records in there for both the account and corp assets on the account
            SQL = "DELETE FROM ASSET_LOCATIONS WHERE EnumAssetType = " & CStr(WindowForm)
            SQL = SQL & " AND ID IN (" & CStr(SelectedCharacter.ID) & "," & CStr(SelectedCharacter.CharacterCorporation.CorporationID) & ")"
            Call evedb.ExecuteNonQuerySQL(SQL)

            For i = 0 To SavedLocationIDs.Count - 1
                SQL = "INSERT INTO ASSET_LOCATIONS (EnumAssetType, ID, LocationID, FlagID) VALUES "
                SQL = SQL & "(" & CStr(WindowForm) & "," & CStr(SavedLocationIDs(i).AccountID) & "," & CStr(SavedLocationIDs(i).LocationID) & "," & CStr(SavedLocationIDs(i).FlagID) & ")"
                Call evedb.ExecuteNonQuerySQL(SQL)
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
        End Select

        MsgBox("Asset Window Settings Saved", vbInformation, Application.ProductName)
        btnSaveSettings.Focus()
        Application.UseWaitCursor = False

    End Sub

    ' Gets all the checked locations in the tree
    Private Function GetCheckedLocations(SentNode As TreeNode) As List(Of LocationInfo)
        Dim LocationIDList As New List(Of LocationInfo)
        Dim RList As New List(Of LocationInfo)
        Dim SubNode As TreeNode = Nothing

        For Each SubNode In SentNode.Nodes
            If Not IsNothing(SubNode.Tag) Then
                Dim TempPair As New LocationInfo
                TempPair = CType(SubNode.Tag, LocationInfo)

                If SubNode.Checked And CLng(TempPair.LocationID) <> -1 Then
                    ' SubNode tag is a location pair
                    Call LocationIDList.Add(CType(SubNode.Tag, LocationInfo))
                End If

                If SubNode.Nodes.Count > 0 Then
                    RList = GetCheckedLocations(SubNode)
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

    Private Sub btnToggleExpand_Click(sender As System.Object, e As System.EventArgs) Handles btnToggleExpand.Click

        Call AssetTree.ExpandAll()

        ' Scroll to top
        AssetTree.TopNode = AssetTree.Nodes(0)
        AssetTree.Focus()

        ' Hide button, show retract
        btnToggleExpand.Visible = False
        btnToggleRetract.Visible = True

    End Sub

    Private Sub btnToggleRetract_Click(sender As System.Object, e As System.EventArgs) Handles btnToggleRetract.Click

        Call AssetTree.CollapseAll()
        ' Open top one though
        AssetTree.TopNode.Expand()

        ' Scroll to top
        AssetTree.TopNode = AssetTree.Nodes(0)
        AssetTree.Focus()

        ' Hide button, show expand
        btnToggleExpand.Visible = True
        btnToggleRetract.Visible = False

    End Sub

    Private Sub txtItemFilter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItemFilter.KeyDown
        Call ProcessCutCopyPasteSelect(txtItemFilter, e)
        If e.KeyCode = Keys.Enter Then
            Call RefreshTree()
        End If
    End Sub

    Private Sub btnRefreshMain_Click(sender As System.Object, e As System.EventArgs)
        Call RefreshTree()
    End Sub

    Private Sub btnSearchRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchRefresh.Click
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
        chkManufacturedItems.Checked = True
        chkRawMaterialItems.Checked = True
    End Sub

    Private Sub chkRawMaterialItems_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRawMaterialItems.CheckedChanged
        Call CheckAllRawItems()
    End Sub

    Private Sub chkManufacturedItems_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkManufacturedItems.CheckedChanged
        Call CheckAllManufacturedItems()
    End Sub

    ' Checks or unchecks just the prices for raw material items
    Private Sub CheckAllRawItems()

        UpdatingChecks = False

        ' Check all item boxes and do not run updates
        If chkRawMaterialItems.Checked = True Then
            chkMinerals.Checked = True
            chkIceProducts.Checked = True
            chkGas.Checked = True
            chkMisc.Checked = True
            chkBPCs.Checked = True
            chkAncientRelics.Checked = True
            chkAncientSalvage.Checked = True
            chkSalvage.Checked = True
            chkPlanetary.Checked = True
            chkDatacores.Checked = True
            chkDecryptors.Checked = True
            chkRawMats.Checked = True
            chkProcessedMats.Checked = True
            chkAdvancedMats.Checked = True
            chkMatsandCompounds.Checked = True
            chkDroneComponents.Checked = True
            chkBoosterMats.Checked = True
            chkPolymers.Checked = True
            chkAsteroids.Checked = True
        Else ' Turn off all item checks
            chkMinerals.Checked = False
            chkIceProducts.Checked = False
            chkGas.Checked = False
            chkMisc.Checked = False
            chkBPCs.Checked = False
            chkAncientRelics.Checked = False
            chkAncientSalvage.Checked = False
            chkSalvage.Checked = False
            chkPlanetary.Checked = False
            chkDatacores.Checked = False
            chkDecryptors.Checked = False
            chkRawMats.Checked = False
            chkProcessedMats.Checked = False
            chkAdvancedMats.Checked = False
            chkMatsandCompounds.Checked = False
            chkDroneComponents.Checked = False
            chkBoosterMats.Checked = False
            chkPolymers.Checked = False
            chkAsteroids.Checked = False
        End If

        UpdatingChecks = True

    End Sub

    ' Checks or unchecks just the prices for manufactured items
    Private Sub CheckAllManufacturedItems()

        UpdatingChecks = False

        ' Check all item boxes and do not run updates
        If chkManufacturedItems.Checked = True Then
            chkShips.Checked = True
            chkModules.Checked = True
            chkDrones.Checked = True
            chkBoosters.Checked = True
            chkRigs.Checked = True
            chkCharges.Checked = True
            chkSubsystems.Checked = True
            chkStructures.Checked = True
            chkTools.Checked = True
            chkDataInterfaces.Checked = True
            chkCapT2Components.Checked = True
            chkCapitalComponents.Checked = True
            chkComponents.Checked = True
            chkHybrid.Checked = True
            chkFuelBlocks.Checked = True
            chkCelestials.Checked = True
            chkStationComponents.Checked = True
            chkDeployables.Checked = True
            chkImplants.Checked = True
        Else ' Turn off all item checks
            chkShips.Checked = False
            chkModules.Checked = False
            chkDrones.Checked = False
            chkBoosters.Checked = False
            chkRigs.Checked = False
            chkCharges.Checked = False
            chkSubsystems.Checked = False
            chkStructures.Checked = False
            chkTools.Checked = False
            chkDataInterfaces.Checked = False
            chkCapT2Components.Checked = False
            chkCapitalComponents.Checked = False
            chkComponents.Checked = False
            chkHybrid.Checked = False
            chkFuelBlocks.Checked = False
            chkCelestials.Checked = False
            chkStationComponents.Checked = False
            chkDeployables.Checked = False
            chkImplants.Checked = False
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

    Private Sub chkToggle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkToggle.CheckedChanged
        Call ToggleNodeChecks(AssetTree.Nodes(0), chkToggle.Checked)
    End Sub

    Private Sub ToggleNodeChecks(SentNode As TreeNode, Check As Boolean)

        For Each child As TreeNode In SentNode.Nodes
            child.Checked = Check
            If child.Nodes.Count > 0 Then ToggleNodeChecks(child, Check)
        Next
    End Sub

#End Region

End Class