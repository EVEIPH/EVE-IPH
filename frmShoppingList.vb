
Imports System.Data.SQLite
Imports System.IO

Public Class frmShoppingList

    ' Inline grid row update variables
    Private Structure SavedLoc
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Private SavedListClickLoc As SavedLoc
    Private RefreshingGrid As Boolean
    Private CutPasteUpdate As Boolean

    Private CurrentRow As ListViewItem
    Private PreviousRow As ListViewItem
    Private NextRow As ListViewItem

    Private NextCellRow As ListViewItem
    Private PreviousCellRow As ListViewItem

    Private CurrentCell As ListViewItem.ListViewSubItem
    Private PreviousCell As ListViewItem.ListViewSubItem
    Private NextCell As ListViewItem.ListViewSubItem

    Private UpdateQuantity As Boolean
    Private UpdatePrice As Boolean
    Private DataEntered As Boolean
    Private IgnoreFocusChange As Boolean
    Private SelectedGrid As ListView

    Private ItemListColumnClicked As Integer
    Private ItemListColumnSortOrder As SortOrder
    Private BuyListColumnClicked As Integer
    Private BuyListColumnSortOrder As SortOrder
    Private BuildListColumnClicked As Integer
    Private BuildListColumnSortOrder As SortOrder

    Private ItemBuyTypeList As List(Of ItemBuyType)

    Private Const BuyListLabel As String = "Buy List"
    Private Const BuildListLabel As String = "Build List"
    Private Const ItemsListLabel As String = "Item List"

    ' To use for copy and pasting data from the game into IPH
    Public CopyPasteMaterialText As String

    ' For finding structure in import lists
    Private ItemQuantityToFind As ItemQuantity

    Private BuyListHeaderCSV As String = "Material,Quantity,Cost Per Item,Min Sell,Max Buy,Buy Type,Total m3,Isk/m3,TotalCost"
    Private BuildListHeaderCSV As String = "Build Item,Quantity,ME"
    Private ItemsListHeaderCSV As String = "Item,Quantity,ME,NumBps,Build Type,Decryptor,Relic"
    Private ItemsListHeaderCSVAdd As String = ",POS Build,IgnoredInvention,IgnoredMinerals,IgnoredT1BaseItem,Location"

    Private BuyListHeaderTXT As String = "Material|Quantity|Cost Per Item|Min Sell|Max Buy|Buy Type|Total m3|Isk/m3|TotalCost"
    Private BuildListHeaderTXT As String = "Build Item|Quantity|ME"
    Private ItemsListHeaderTXT As String = "Item|Quantity|ME|NumBps|Build Type|Decryptor|Relic"
    Private ItemsListHeaderTXTAdd As String = "|POS Build|IgnoredInvention|IgnoredMinerals|IgnoredT1BaseItem|Location"

    Private BuyListHeaderSSV As String = "Material;Quantity;Cost Per Item;Min Sell;Max Buy;Buy Type;Total m3;Isk/m3;TotalCost"
    Private BuildListHeaderSSV As String = "Build Item;Quantity;ME"
    Private ItemsListHeaderSSV As String = "Item;Quantity;ME;NumBps;Build Type;Decryptor;Relic"
    Private ItemsListHeaderSSVAdd As String = ";POS Build;IgnoredInvention;IgnoredMinerals;IgnoredT1BaseItem;Location"

    Private FirstFormLoad As Boolean

    Private Structure ItemQuantity
        Dim ItemName As String
        Dim ItemQuantity As Long
        Dim ItemME As Integer
    End Structure

    ' Predicate for finding the item in full list
    Private Function FindItemQuantity(ByVal Item As ItemQuantity) As Boolean
        If Item.ItemName = ItemQuantityToFind.ItemName Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Structure BPItem
        'Item List Format: Item, Quantity, ME, NumBPs Build Type, Decryptor, Relic (in file, in grid it is put with item name)
        Dim ItemName As String
        Dim ItemQuantity As Long
        Dim ItemME As Integer
        Dim NumBPs As Integer
        Dim BuildType As String
        Dim Decryptor As String
        Dim Relic As String
        Dim BuildLocation As String
        Dim BuiltInPOS As Boolean
        Dim IgnoredInvention As Boolean
        Dim IgnoredMinerals As Boolean
        Dim IgnoredT1BaseItem As Boolean
    End Structure

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Item Buy List - width = 1081 (21 width for verticle scroll bar)
        lstBuy.Columns.Add("TypeID", 0, HorizontalAlignment.Left) ' Hidden
        lstBuy.Columns.Add("Material", 245, HorizontalAlignment.Left)
        lstBuy.Columns.Add("Quantity", 94, HorizontalAlignment.Right) ' Min 94
        lstBuy.Columns.Add("Cost per Item", 90, HorizontalAlignment.Right) ' Min 90
        lstBuy.Columns.Add("Min Sell", 90, HorizontalAlignment.Right) ' Min 90
        lstBuy.Columns.Add("Max Buy", 90, HorizontalAlignment.Right) ' Min 90
        lstBuy.Columns.Add("Buy Type", 66, HorizontalAlignment.Right) ' Min 66
        lstBuy.Columns.Add("Total m3", 100, HorizontalAlignment.Right) ' Min 100
        lstBuy.Columns.Add("Isk/m3", 83, HorizontalAlignment.Right) ' Min 85
        lstBuy.Columns.Add("Fees", 90, HorizontalAlignment.Right)
        lstBuy.Columns.Add("Total Cost", 112, HorizontalAlignment.Right) ' Min 129

        ' Built Item List for items we are building - width = 371 (21 for verticle scroll bar)
        lstBuild.Columns.Add("TypeID", 0, HorizontalAlignment.Center) ' always left allignment this column for some reason, so add a dummy
        lstBuild.Columns.Add("Build Item", 237, HorizontalAlignment.Left)
        lstBuild.Columns.Add("Quantity", 80, HorizontalAlignment.Right)
        lstBuild.Columns.Add("ME", 30, HorizontalAlignment.Right)

        ' Item List - What we are building - width = 711 (21 for verticle scroll bar)
        lstItems.Columns.Add("TypeID", 0, HorizontalAlignment.Center) ' always left allignment this column for some reason, so add a dummy, store bpID here though
        lstItems.Columns.Add("Item", 225, HorizontalAlignment.Left) ' 
        lstItems.Columns.Add("Quantity", 67, HorizontalAlignment.Right) ' 51 min text
        lstItems.Columns.Add("ME", 30, HorizontalAlignment.Right) ' 30 min text
        lstItems.Columns.Add("Num BPs", 60, HorizontalAlignment.Left) ' 60 min text
        lstItems.Columns.Add("Build Type", 71, HorizontalAlignment.Left) ' 71 min text
        lstItems.Columns.Add("Decryptor", 105, HorizontalAlignment.Left) '105 min text
        lstItems.Columns.Add("Location", 132, HorizontalAlignment.Left) '132 min text
        lstItems.Columns.Add("BuildInPOS", 0, HorizontalAlignment.Left) 'Hidden flag for pos building
        lstItems.Columns.Add("IgnoredInvention", 0, HorizontalAlignment.Left) 'Hidden flag for ignore variables
        lstItems.Columns.Add("IgnoredMinerals", 0, HorizontalAlignment.Left) 'Hidden flag for ignore variables
        lstItems.Columns.Add("IgnoredT1BaseItem", 0, HorizontalAlignment.Left) 'Hidden flag for ignore variables

        If UserApplicationSettings.ShowToolTips Then
            ttMain.SetToolTip(btnShowAssets, "Show Assets")
            ttMain.SetToolTip(chkFees, "When checked, will total all items listed in buy list as 'Buy Order'.")
            ttMain.SetToolTip(chkBuyorBuyOrder, "When checked, IPH will attempt to determine whether it is better to buy the item directly off of the market or to set up a buy order.")
            ttMain.SetToolTip(chkUsage, "Estimated Usage Fees to build the items in the Items and Components to Build Lists.")
            ttMain.SetToolTip(lblAddlCosts, "Addtional costs you want to add to this shopping list (i.e. BPC costs). This value not affected by taxes or fees.")
            ttMain.SetToolTip(chkUpdateAssetsWhenUsed, "If checked, when updating the list with scanned assets IPH will subtract all used materials from your asset list.")
            ttMain.SetToolTip(rbtnExportCSV, "Exports data in Common Separated Values with periods for decimals")
            ttMain.SetToolTip(rbtnExportSSV, "Exports data in SemiColon Separated Values with commas for decimals")
            ttMain.SetToolTip(rbtnExportDefault, "Exports data in basic space or dashes to separate data for easy readability")
            ttMain.SetToolTip(btnUpdateListwithAssets, "Update the Shopping List based on materials you have in your selected asset location(s).")
            ttMain.SetToolTip(btnShowAssets, "Open the Asset Viewer to set the default location(s) for materials to use for updating the Shopping List.")
            ttMain.SetToolTip(lblTIC, "Total of all invention materials in the buy list.")
            ttMain.SetToolTip(lblTCC, "Total of all the copy materials in the buy list.")
        End If

        IgnoreFocusChange = False

        If rbtnExportCSV.Text = UserApplicationSettings.DataExportFormat Then
            rbtnExportCSV.Checked = True
        ElseIf rbtnExportSSV.Text = UserApplicationSettings.DataExportFormat Then
            rbtnExportSSV.Checked = True
        ElseIf rbtnExportDefault.Text = UserApplicationSettings.DataExportFormat Then
            rbtnExportDefault.Checked = True
        End If

        btnCopy.Enabled = False
        btnSaveListToFile.Enabled = False

        ItemListColumnClicked = 0
        ItemListColumnSortOrder = SortOrder.None
        BuyListColumnClicked = 0
        BuyListColumnSortOrder = SortOrder.None
        BuildListColumnClicked = 0
        BuildListColumnSortOrder = SortOrder.None

        CutPasteUpdate = False

        FirstFormLoad = True

        CopyPasteMaterialText = ""

    End Sub

    Private Sub frmShoppingList_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        ' Load settings
        chkUsage.Checked = UserShoppingListSettings.Usage
        chkFees.Checked = UserShoppingListSettings.Fees
        chkAlwaysOnTop.Checked = UserShoppingListSettings.AlwaysonTop
        chkBuyorBuyOrder.Checked = UserShoppingListSettings.CalcBuyBuyOrder
        If rbtnExportCSV.Text = UserShoppingListSettings.DataExportFormat Then
            rbtnExportCSV.Checked = True
        ElseIf rbtnExportSSV.Text = UserShoppingListSettings.DataExportFormat Then
            rbtnExportSSV.Checked = True
        ElseIf rbtnExportDefault.Text = UserShoppingListSettings.DataExportFormat Then
            rbtnExportDefault.Checked = True
        End If
        chkUpdateAssetsWhenUsed.Checked = UserShoppingListSettings.UpdateAssetsWhenUsed

        ' Only enable the clear button when something in the list
        If TotalShoppingList.GetNumShoppingItems > 0 Then
            btnClear.Enabled = True
            gbUpdateList.Enabled = True
        Else
            btnClear.Enabled = False
            gbUpdateList.Enabled = False
        End If

    End Sub

    Public Sub RefreshLists()
        RefreshingGrid = True
        Call LoadBuyList()
        Call LoadItemList()
        Call LoadBuildList()
        Call LoadFormStats()

        ' Enable the buttons if there are rows
        If lstItems.Items.Count > 0 Then
            btnCopy.Enabled = True
            btnSaveListToFile.Enabled = True
            btnClear.Enabled = True
            gbUpdateList.Enabled = True
        Else
            btnCopy.Enabled = False
            btnSaveListToFile.Enabled = False
            btnClear.Enabled = False
            gbUpdateList.Enabled = False
            ' No more items so clear lists
            Call ClearLists()
        End If

        RefreshingGrid = False
    End Sub

    Private Sub ClearLists()
        ' Reset global list
        TotalShoppingList.Clear()

        lstItems.Items.Clear()
        lstBuild.Items.Clear()
        lstBuy.Items.Clear()

        lstItems.Update()
        lstBuild.Update()
        lstBuy.Update()

        lblTotalProfit.Text = "0.00 ISK"
        lblAvgIPH.Text = "0.00 ISK"

        lblTotalCost.Text = "0.00 ISK"
        lblTotalVolume.Text = "0.00 m3"
        lblTotalBuiltVolume.Text = "0.00 m3"
        lblTotalCopyCost.Text = "0.00 ISK"
        lblTotalInventionCost.Text = "0.00 ISK"

        lblUsage.Text = "0.00"
        lblFees.Text = "0.00"

        ' Update the main form notice of no items
        frmMain.pnlShoppingList.Text = "No Items in Shopping List"
        frmMain.pnlShoppingList.ForeColor = Color.Black

        btnCopy.Enabled = False
        btnSaveListToFile.Enabled = False

        Me.Refresh()

    End Sub

    ' Loads all the main items we want to buy into the main table
    Private Sub LoadBuyList()
        Dim RawmatList As ListViewItem
        Dim RawItems As New Materials

        Dim SQL As String
        Dim readerItemPrices As SQLiteDataReader

        Dim BuyOrderText As String
        Dim SellPrice As Double
        Dim BuyOrderPrice As Double
        Dim BuyOrderFees As Double
        Dim TotalPrice As Double
        Dim PriceType As String
        Dim StoredPrice As Double
        Dim MinSellUnitPrice As Double
        Dim MaxBuyUnitPrice As Double

        Const BuyOrder As String = "Buy Order"
        Const BuyMarket As String = "Buy Market"
        Const Unknown As String = "Unknown"

        lstBuy.Items.Clear()
        lstBuy.BeginUpdate()

        ' Get the list of items
        RawItems = TotalShoppingList.GetFullBuyList

        ' Reset buy type list
        ItemBuyTypeList = New List(Of ItemBuyType)

        ' Set to 0 first
        lblTotalProfit.Text = "0.00 ISK"
        lblAvgIPH.Text = "0.00 ISK"
        lblTotalBuiltVolume.Text = "0.00 m3"
        lblTotalVolume.Text = "0.00 m3"
        lblTotalCost.Text = "0.00 ISK"

        If Not IsNothing(RawItems) Then
            If Not IsNothing(RawItems.GetMaterialList) Then

                ' Sort the list of mats
                RawItems.SortMaterialListByQuantity()

                ' Fill Component List
                For i = 0 To RawItems.GetMaterialList.Count - 1
                    ' Reset
                    BuyOrderText = Unknown

                    RawmatList = lstBuy.Items.Add(CStr(RawItems.GetMaterialList(i).GetMaterialTypeID)) ' Hidden
                    'The remaining columns are subitems  
                    RawmatList.SubItems.Add(RawItems.GetMaterialList(i).GetMaterialName)
                    RawmatList.SubItems.Add(FormatNumber(RawItems.GetMaterialList(i).GetQuantity, 0))
                    RawmatList.SubItems.Add(FormatNumber(RawItems.GetMaterialList(i).GetCostPerItem, 2)) ' Cost per item (as the user has it stored)

                    ' See if we want to determine if we compare prices for Buy Order / Buy Market
                    ' The rules are:
                    ' - If Fees is checked and CalcBuyType then calculate for buy order or buy market
                    ' - If Fees is not checked, then CalcBuyType will be disabled, and the user wants to buy all from market
                    ' - If Fees is checked, and CalcBuyType is not, then you want to buy all from a buy order

                    ' Look up the price of buying directly off the market (min sell - no tax, no broker fee) and compare it to the price
                    ' of max buy (buy order) plus the brokers fees to set up that order (no tax). Then show the value in the grid of what they should do
                    ' First find out what price and type we have stored
                    SQL = "SELECT PRICE, PRICE_TYPE FROM ITEM_PRICES WHERE ITEM_ID = " & RawItems.GetMaterialList(i).GetMaterialTypeID
                    DBCommand = New SQLiteCommand(SQL, DB)
                    readerItemPrices = DBCommand.ExecuteReader
                    readerItemPrices.Read()

                    If readerItemPrices.HasRows Then
                        ' Figure out what they have stored so we know what type of price we need to get
                        StoredPrice = CDbl(readerItemPrices.GetValue(0))
                        PriceType = readerItemPrices.GetString(1)
                    Else
                        PriceType = None
                    End If

                    readerItemPrices.Close()

                    ' Load the Min Sell and Max Buy prices from cache
                    SQL = "SELECT sellMin, buyMax FROM ITEM_PRICES_CACHE WHERE typeID = " & RawItems.GetMaterialList(i).GetMaterialTypeID
                    SQL = SQL & " AND sellMin IS NOT NULL and buyMax IS NOT NULL"
                    DBCommand = New SQLiteCommand(SQL, DB)
                    readerItemPrices = DBCommand.ExecuteReader
                    readerItemPrices.Read()

                    ' Get the buy and sell prices
                    If readerItemPrices.HasRows Then
                        MinSellUnitPrice = CDbl(readerItemPrices.GetValue(0))
                        MaxBuyUnitPrice = CDbl(readerItemPrices.GetValue(1))
                    Else
                        ' Something went wrong, so re-mark as none
                        PriceType = None
                    End If

                    readerItemPrices.Close()

                    ' Four cases - None or error, set to zero. If they had a user entered price, assume min sell. Otherwise, use stored values
                    If PriceType = None Then
                        ' Price isn't set yet or an error. Either way they are zero 
                        MinSellUnitPrice = 0
                        MaxBuyUnitPrice = 0

                    ElseIf PriceType = "User" Or PriceType.Contains("sell") Then
                        ' The updated a price, so no matter what they intended - I'm assuming they meant minSell (50/50 chance)
                        ' or they stored some sell price, so set that and compare to the stored maxbuy price
                        MinSellUnitPrice = StoredPrice
                    ElseIf PriceType.Contains("buy") Or PriceType.Contains("all") Then
                        ' They stored a buy price...so set that and we'll compare it to the stored minsell price - also use average here too
                        MaxBuyUnitPrice = StoredPrice
                    End If

                    If chkBuyorBuyOrder.Checked And chkBuyorBuyOrder.Enabled = True And chkFees.Checked Then
                        ' Now that we have the prices, compare the two
                        If MinSellUnitPrice <> 0 And MaxBuyUnitPrice <> 0 Then

                            ' Now look at max buy
                            TotalPrice = MaxBuyUnitPrice * RawItems.GetMaterialList(i).GetQuantity
                            BuyOrderFees = GetSalesBrokerFee(TotalPrice)
                            BuyOrderPrice = TotalPrice + BuyOrderFees

                            ' Use min sell
                            SellPrice = MinSellUnitPrice * RawItems.GetMaterialList(i).GetQuantity

                            If BuyOrderPrice < SellPrice Then
                                ' They should do an order
                                BuyOrderText = BuyOrder
                            Else
                                ' Buy from the Market
                                BuyOrderText = BuyMarket
                                ' No fees straight off market
                                BuyOrderFees = 0
                            End If
                        Else
                            BuyOrderText = Unknown
                            BuyOrderFees = 0
                        End If
                    ElseIf chkFees.Checked = False Then
                        ' User wants to buy all from market, don't apply broker fees
                        BuyOrderText = BuyMarket
                    ElseIf chkFees.Checked = True And chkBuyorBuyOrder.Checked = False Then
                        ' They want a buy order for all items
                        BuyOrderText = BuyOrder
                        If MaxBuyUnitPrice <> 0 Then
                            BuyOrderFees = GetSalesBrokerFee(MaxBuyUnitPrice * RawItems.GetMaterialList(i).GetQuantity)
                        Else
                            BuyOrderFees = 0
                        End If
                    End If

                    ' Add the minsell/maxbuy for reference
                    RawmatList.SubItems.Add(FormatNumber(MinSellUnitPrice, 2))
                    RawmatList.SubItems.Add(FormatNumber(MaxBuyUnitPrice, 2))

                    ' Finally Add the correct column value for how to buy it
                    RawmatList.SubItems.Add(BuyOrderText) ' Buy or Buy Market flag

                    ' Add this item to the list
                    Dim Temp As ItemBuyType
                    Temp.ItemName = RawItems.GetMaterialList(i).GetMaterialName
                    Temp.BuyType = BuyOrderText
                    ItemBuyTypeList.Add(Temp)

                    RawmatList.SubItems.Add(FormatNumber(RawItems.GetMaterialList(i).GetTotalVolume, 2)) ' Volume

                    If RawItems.GetMaterialList(i).GetTotalVolume <> 0 Then
                        RawmatList.SubItems.Add(FormatNumber(RawItems.GetMaterialList(i).GetTotalCost / RawItems.GetMaterialList(i).GetTotalVolume, 2)) ' Isk per m3
                    Else
                        RawmatList.SubItems.Add("0.00") ' Isk per m3
                    End If

                    RawmatList.SubItems.Add(FormatNumber(BuyOrderFees, 2)) ' Fees for buy orders
                    RawmatList.SubItems.Add(FormatNumber(RawItems.GetMaterialList(i).GetTotalCost + BuyOrderFees, 2)) ' Total Cost
                Next

            End If
        End If

        lstBuy.EndUpdate()

    End Sub

    ' Loads the list of items into the items list
    Private Sub LoadItemList()
        Dim lstItem As ListViewItem
        Dim ItemList As List(Of ShoppingListItem)

        lstItems.BeginUpdate()
        lstItems.Items.Clear()

        ItemList = TotalShoppingList.GetFullShoppingList()

        ' Loop through the list and display all items
        For i = 0 To ItemList.Count - 1
            With ItemList(i)
                lstItem = lstItems.Items.Add(CStr(.TypeID)) ' Hidden
                If .TechLevel <> 3 Then
                    lstItem.SubItems.Add(ItemList(i).Name)
                Else
                    lstItem.SubItems.Add(ItemList(i).Name & " (" & ItemList(i).Relic & ")") ' Add relic name after the item
                End If
                lstItem.SubItems.Add(CStr(FormatNumber(ItemList(i).Quantity, 0)))
                lstItem.SubItems.Add(CStr(ItemList(i).ItemME))
                lstItem.SubItems.Add(CStr(ItemList(i).NumBPs))
                lstItem.SubItems.Add(ItemList(i).BuildType)
                lstItem.SubItems.Add(ItemList(i).Decryptor)
                lstItem.SubItems.Add(ItemList(i).BuildLocation)
                lstItem.SubItems.Add(CStr(CInt(ItemList(i).BuiltInPOS)))
                lstItem.SubItems.Add(CStr(CInt(ItemList(i).IgnoredInvention)))
                lstItem.SubItems.Add(CStr(CInt(ItemList(i).IgnoredMinerals)))
                lstItem.SubItems.Add(CStr(CInt(ItemList(i).IgnoredT1BaseItem)))
            End With
        Next

        ' Add the number of item(s) to the label on the shopping list
        Dim ItemCount As Integer = ItemList.Count

        If ItemList.Count <> 1 Then
            lblTotalItemsInList.Text = FormatNumber(ItemCount, 0) & vbCrLf & "Items in list"
        Else
            lblTotalItemsInList.Text = "1" & vbCrLf & "Item in list"
        End If

        lstItems.EndUpdate()

    End Sub

    ' Loads up the IPH, profit, etc
    Private Sub LoadFormStats()

        If TotalShoppingList.GetNumShoppingItems <> 0 Then
            If Trim(txtAddlCosts.Text) <> "" Then
                Call TotalShoppingList.SetAdditionalCosts(CDbl(txtAddlCosts.Text))
            End If

            Call TotalShoppingList.SetPriceData(chkFees.Checked, chkUsage.Checked, ItemBuyTypeList)

            lblTotalCost.Text = FormatNumber(TotalShoppingList.GetTotalCost, 2) & " ISK"
            lblTotalVolume.Text = FormatNumber(TotalShoppingList.GetTotalVolume, 2) & " m3"

            lblTotalProfit.Text = FormatNumber(TotalShoppingList.GetTotalProfit(), 2) & " ISK"
            lblAvgIPH.Text = FormatNumber(TotalShoppingList.GetTotalIPH(), 2) & " ISK"

            lblTotalBuiltVolume.Text = FormatNumber(TotalShoppingList.GetBuiltItemVolume, 2) & " m3"

            lblTotalInventionCost.Text = FormatNumber(TotalShoppingList.GetTotalInventionCosts, 2) & " ISK"
            lblTotalCopyCost.Text = FormatNumber(TotalShoppingList.GetTotalCopyCosts, 2) & " ISK"

            lblFees.Text = FormatNumber(TotalShoppingList.GetTotalMaterialsBrokersFees)
            lblUsage.Text = FormatNumber(TotalShoppingList.GetTotalUsage)

        End If

    End Sub

    ' Loads the list of items to build into the items list
    Private Sub LoadBuildList()
        Dim i As Integer
        Dim lstBuildItem As ListViewItem
        Dim BuildItems As New Materials
        Dim SQL As String

        lstBuild.BeginUpdate()
        lstBuild.Items.Clear()

        ' TotalShoppingList.GetFullBuildList uses BuildItem for built in pos, and Volume for the facility ME value
        BuildItems = TotalShoppingList.GetFullBuildList

        ' Now load the grid with all the mats
        If Not IsNothing(BuildItems) Then
            If Not IsNothing(BuildItems.GetMaterialList) Then
                For i = 0 To BuildItems.GetMaterialList.Count - 1
                    ' Find the BP Type ID
                    SQL = "SELECT ITEM_ID FROM ALL_BLUEPRINTS WHERE ITEM_NAME = '" & BuildItems.GetMaterialList(i).GetMaterialName & "'"
                    Dim readerBP As SQLiteDataReader

                    DBCommand = New SQLiteCommand(SQL, DB)
                    readerBP = DBCommand.ExecuteReader
                    readerBP.Read()

                    lstBuildItem = lstBuild.Items.Add(CStr(readerBP.GetValue(0))) ' Add the bp type id here for double clicking later
                    'The remaining columns are subitems  
                    lstBuildItem.SubItems.Add(BuildItems.GetMaterialList(i).GetMaterialName)
                    lstBuildItem.SubItems.Add(CStr(FormatNumber(BuildItems.GetMaterialList(i).GetQuantity, 0)))
                    lstBuildItem.SubItems.Add(BuildItems.GetMaterialList(i).GetItemME)

                    readerBP.Close()
                    readerBP = Nothing
                Next
            End If
        End If

        lstBuild.EndUpdate()

    End Sub

    ' Update the shopping list with all items we may have. If we have components, update those and then update the number of mats accordingly in the buy list
    Private Sub btnUpdateListwithAssets_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateListwithAssets.Click
        Call UpdateShoppingListwithAssets()
    End Sub

    ' Updates the shopping list values on screen if we are sent materials or if not, looking up in the DB
    Private Sub UpdateShoppingListwithAssets(Optional PasteMaterialList As Materials = Nothing)
        Dim ProcessList As New Materials
        Dim FoundBuildItem As New BuiltItem
        Dim SQL As String
        Dim readerAssets As SQLiteDataReader
        Dim UpdatedQuantity As Long
        Dim UserQuantity As Long
        Dim CurrentItemName As String = ""

        Application.UseWaitCursor = True
        Application.DoEvents()

        Dim IDString As String = ""

        ' Set the ID string we will use to update
        If UserAssetWindowShoppingListSettings.AssetType = "Both" Then
            IDString = CStr(SelectedCharacter.ID) & "," & CStr(SelectedCharacter.CharacterCorporation.CorporationID)
        ElseIf UserAssetWindowShoppingListSettings.AssetType = "Personal" Then
            IDString = CStr(SelectedCharacter.ID)
        ElseIf UserAssetWindowShoppingListSettings.AssetType = "Corporation" Then
            IDString = CStr(SelectedCharacter.CharacterCorporation.CorporationID)
        End If

        ' Build the where clause to look up data
        Dim AssetWhereClause As String = ""
        ' First look up the location and flagID pairs - unique ID of asset locations
        SQL = "SELECT LocationID, FlagID FROM ASSET_LOCATIONS WHERE EnumAssetType = 1 AND ID IN (" & IDString & ")" ' Enum type 1 is shopping list
        DBCommand = New SQLiteCommand(SQL, DB)
        readerAssets = DBCommand.ExecuteReader

        While readerAssets.Read
            AssetWhereClause = AssetWhereClause & "(LocationID = " & CStr(readerAssets.GetInt64(0)) & " AND Flag = " & CStr(readerAssets.GetInt32(1)) & ") OR "
        End While

        readerAssets.Close()

        If AssetWhereClause <> "" Then
            ' Strip the last OR
            AssetWhereClause = AssetWhereClause.Substring(0, Len(AssetWhereClause) - 4)

            ' Loop through the lists, starting with the build list first and find quantities in hanger to build
            For i = 0 To 3 ' 4 lists
                Application.DoEvents()
                ' TotalShoppingList.GetFullBuildList.Clone uses BuildItem for built in pos, and Volume for the facility ME value
                Select Case i
                    Case 0
                        ProcessList = CType(TotalShoppingList.GetFullBuildList.Clone, Materials)
                    Case 1
                        ProcessList = CType(TotalShoppingList.GetFullBuyList.Clone, Materials)
                    Case 2
                        ProcessList = CType(TotalShoppingList.GetFullInventionList.Clone, Materials)
                    Case 3
                        ProcessList = CType(TotalShoppingList.GetFullCopyList.Clone, Materials)
                End Select

                If Not IsNothing(ProcessList) Then
                    If Not IsNothing(ProcessList.GetMaterialList) Then
                        For j = 0 To ProcessList.GetMaterialList.Count - 1
                            Application.DoEvents()
                            UserQuantity = 0
                            CurrentItemName = ""

                            ' Look in table or in the paste list
                            If Not CutPasteUpdate Then

                                ' Look up each item in their assets in their locations stored, and sum up the quantity
                                SQL = "SELECT typeName, SUM(Quantity) FROM ASSETS, INVENTORY_TYPES "
                                SQL = SQL & "WHERE (" & AssetWhereClause & ") "
                                SQL = SQL & " AND INVENTORY_TYPES.typeID = ASSETS.TypeID"
                                SQL = SQL & " AND ASSETS.TypeID = " & ProcessList.GetMaterialList(j).GetMaterialTypeID
                                SQL = SQL & " AND ID IN (" & IDString & ")"

                                DBCommand = New SQLiteCommand(SQL, DB)
                                readerAssets = DBCommand.ExecuteReader
                                readerAssets.Read()

                                If readerAssets.HasRows And Not IsDBNull(readerAssets.GetValue(1)) Then
                                    CurrentItemName = readerAssets.GetString(0)
                                    UserQuantity = CLng(readerAssets.GetValue(1))
                                End If

                            Else ' Look up in asset list
                                Dim TempMaterial As Material

                                If Not IsNothing(PasteMaterialList) Then
                                    TempMaterial = PasteMaterialList.SearchListbyName(ProcessList.GetMaterialList(j).GetMaterialName, True)

                                    If Not IsNothing(TempMaterial) Then
                                        ' Found it
                                        CurrentItemName = TempMaterial.GetMaterialName
                                        UserQuantity = CLng(TempMaterial.GetQuantity)
                                    End If
                                Else
                                    CurrentItemName = ""
                                    UserQuantity = 0
                                End If

                            End If

                            If UserQuantity <> 0 And CurrentItemName <> "" Then
                                ' Call shoppinglist update numbers with new number
                                Select Case i
                                    Case 0
                                        ' Need to look up the full built item data, however only by name since we don't care how it was built if they already have it
                                        ' plus we don't have the ME data anyway
                                        Dim ListQuantity As Long = 0
                                        Dim TempBuiltList As BuiltItemList

                                        ' We could have multiple items in the list (because of different MEs), so loop through all of them and get the quantities on hand
                                        TempBuiltList = TotalShoppingList.GetFullBuiltItemList.FindBuiltItems(CurrentItemName)

                                        For k = 0 To TempBuiltList.GetBuiltItemList.Count - 1
                                            ListQuantity = TempBuiltList.GetBuiltItemList(k).ItemQuantity
                                            If ListQuantity <= UserQuantity Then
                                                ' We found enough already to remove all for this built item, need to keep track and update the rest
                                                UpdatedQuantity = ListQuantity
                                                Call TotalShoppingList.UpdateShoppingBuiltItemQuantity(TempBuiltList.GetBuiltItemList(k), 0)

                                                ' If the user wants to update the DB with materials they "used" here, update
                                                If chkUpdateAssetsWhenUsed.Checked Then
                                                    Call UpdateUsedAssets(ProcessList.GetMaterialList(j).GetMaterialTypeID, UserQuantity, UpdatedQuantity)
                                                End If
                                            Else
                                                ' This list has more, so just remove the difference and leave
                                                UpdatedQuantity = ListQuantity - UserQuantity
                                                Call TotalShoppingList.UpdateShoppingBuiltItemQuantity(TempBuiltList.GetBuiltItemList(k), UpdatedQuantity)
                                                ' If the user wants to update the DB with materials they "used" here, update
                                                If chkUpdateAssetsWhenUsed.Checked Then
                                                    Call UpdateUsedAssets(ProcessList.GetMaterialList(j).GetMaterialTypeID, UserQuantity, UpdatedQuantity)
                                                End If

                                                Exit For
                                            End If
                                        Next

                                    Case 1, 2, 3

                                        Dim UsedQuantity As Long
                                        UsedQuantity = ProcessList.GetMaterialList(j).GetQuantity

                                        ' See what the new value is for setting the shopping list
                                        UpdatedQuantity = ProcessList.GetMaterialList(j).GetQuantity - UserQuantity

                                        If UpdatedQuantity < 0 Then
                                            ' We have more than this item requires, so zero out the quantity in the shopping list (delete)
                                            UpdatedQuantity = 0
                                        End If

                                        ' Invention and RE are contained in Buy mats list
                                        Call TotalShoppingList.UpdateShoppingBuyQuantity(ProcessList.GetMaterialList(j).GetMaterialName, UpdatedQuantity)

                                        ' If the user wants to update the DB with materials they "used" here, update
                                        If chkUpdateAssetsWhenUsed.Checked Then
                                            Call UpdateUsedAssets(ProcessList.GetMaterialList(j).GetMaterialTypeID, UserQuantity, UsedQuantity)
                                        End If

                                End Select
                            End If
                        Next
                    End If
                End If
            Next

            Application.UseWaitCursor = False
            Application.DoEvents()

            ' Play notification sound
            Call PlayNotifySound()

            ' Refresh the updated lists
            Call RefreshLists()

            ' Refresh the asset list with updated assets
            If chkUpdateAssetsWhenUsed.Checked Then
                ' First, need to refresh assets for character and corp if used
                If Not IsNothing(frmShoppingAssets) Then
                    If Not frmShoppingAssets.IsDisposed Then
                        If frmShoppingAssets.rbtnAllAssets.Checked = True Or frmShoppingAssets.rbtnCorpAssets.Checked = True Then
                            SelectedCharacter.GetAssets.LoadAssets(ScanType.Corporation, UserApplicationSettings.LoadAssetsonStartup)
                        Else ' Just personal
                            SelectedCharacter.GetAssets.LoadAssets(ScanType.Personal, UserApplicationSettings.LoadAssetsonStartup)
                        End If
                        frmShoppingAssets.RefreshTree()
                    End If
                End If

                If Not IsNothing(frmDefaultAssets) Then
                    If Not frmDefaultAssets.IsDisposed Then
                        If frmDefaultAssets.rbtnAllAssets.Checked = True Or frmDefaultAssets.rbtnCorpAssets.Checked = True Then
                            SelectedCharacter.GetAssets.LoadAssets(ScanType.Corporation, UserApplicationSettings.LoadAssetsonStartup)
                        Else ' Just personal
                            SelectedCharacter.GetAssets.LoadAssets(ScanType.Personal, UserApplicationSettings.LoadAssetsonStartup)
                        End If
                        frmDefaultAssets.RefreshTree()
                    End If
                End If
            End If
            Application.DoEvents()
        Else
            MsgBox("You do not have an asset location selected", vbInformation, Application.ProductName)
            Application.UseWaitCursor = False
            Application.DoEvents()
        End If

    End Sub

    ' Updates the assets table reflecting that you "used" the materials already in the shopping list
    Private Sub UpdateUsedAssets(MaterialTypeID As Long, UserQuantity As Long, UsedQuantity As Long)
        Dim SQL As String
        Dim IDString As String = ""
        Dim readerAssets As SQLite.SQLiteDataReader
        Dim UsedQuantityRemaining As Long = 0
        Dim LocUserQuantity As Long = 0
        Dim LocationID As Long = 0

        If UserAssetWindowShoppingListSettings.AssetType = "Both" Then
            IDString = CStr(SelectedCharacter.ID) & "," & CStr(SelectedCharacter.CharacterCorporation.CorporationID)
        ElseIf UserAssetWindowShoppingListSettings.AssetType = "Personal" Then
            IDString = CStr(SelectedCharacter.ID)
        ElseIf UserAssetWindowShoppingListSettings.AssetType = "Corporation" Then
            IDString = CStr(SelectedCharacter.CharacterCorporation.CorporationID)
        End If

        If UserQuantity <= UsedQuantity Then
            ' Need to just delete the records because we are using everything we have in all locations
            SQL = "DELETE FROM ASSETS WHERE TypeID = " & MaterialTypeID & " AND LocationID IN"
            SQL = SQL & " (SELECT LocationID FROM ASSET_LOCATIONS WHERE EnumAssetType = 1 AND ID IN (" & IDString & "))" ' Enum type 1 is shopping list
            SQL = SQL & " AND ID IN (" & IDString & ")"

            Call ExecuteNonQuerySQL(SQL)

        Else ' Only using part of what we have
            ' Look up each item in their assets in their locations stored, and loop through them
            SQL = "SELECT Quantity, LocationID FROM ASSETS, INVENTORY_TYPES WHERE LocationID IN"
            SQL = SQL & " (SELECT LocationID FROM ASSET_LOCATIONS WHERE EnumAssetType = 1 AND ID IN (" & IDString & "))" ' Enum type 1 is shopping list
            SQL = SQL & " AND ID IN (" & IDString & ")"
            SQL = SQL & " AND INVENTORY_TYPES.typeID = ASSETS.TypeID"
            SQL = SQL & " AND ASSETS.TypeID = " & MaterialTypeID
            SQL = SQL & " ORDER BY Quantity DESC"

            DBCommand = New SQLiteCommand(SQL, DB)
            readerAssets = DBCommand.ExecuteReader

            UsedQuantityRemaining = UsedQuantity

            While readerAssets.Read
                LocUserQuantity = readerAssets.GetInt64(0)
                LocationID = readerAssets.GetInt64(1)

                ' Keep track of what we need to update - we have more than we need to build this item, so need to update that total from our summed mins
                If LocUserQuantity > UsedQuantityRemaining Then
                    ' Whatever we have in this location is greater than the quantity remaining, so update this and leave loop
                    SQL = "UPDATE ASSETS SET Quantity = " & LocUserQuantity - UsedQuantityRemaining
                    SQL = SQL & " WHERE TypeID = " & MaterialTypeID & " AND LocationID = " & CStr(LocationID) ' Locid set above so it's good
                    SQL = SQL & " AND ID IN (" & IDString & ")"

                    Call ExecuteNonQuerySQL(SQL)
                    Exit While
                Else
                    ' Its less than or equal to the quantity so we need to delete this location's value and update the used quantity
                    SQL = "DELETE FROM ASSETS WHERE TypeID = " & MaterialTypeID & " AND LocationID = " & CStr(LocationID)
                    SQL = SQL & " AND ID IN (" & IDString & ")"
                    Call ExecuteNonQuerySQL(SQL)

                    ' Update used quantity
                    UsedQuantityRemaining = UsedQuantityRemaining - LocUserQuantity

                End If

            End While

        End If

    End Sub

    ' Close the form
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub frmShoppingList_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
        Me.Hide()
    End Sub

    ' Clears the lists and variables
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        If MsgBox("Delete all items in the shopping list?", CType(vbYesNo + vbQuestion, MsgBoxStyle), Application.ProductName) = vbYes Then
            Call ClearLists()
            btnClear.Enabled = False
            gbUpdateList.Enabled = False
            Call PlayNotifySound()
            frmMain.pnlShoppingList.Text = "No Items in Shopping List"
            frmMain.pnlShoppingList.ForeColor = Color.Black
            lblTotalItemsInList.Text = "0" & vbCrLf & "Items in list"
        End If

    End Sub

    ' Save the few settings on the form to xml
    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click
        Dim TempList As ShoppingListSettings = Nothing
        Dim TempSettings As New ProgramSettings

        TempList.AlwaysonTop = chkAlwaysOnTop.Checked
        If rbtnExportDefault.Checked Then
            TempList.DataExportFormat = rbtnExportDefault.Text
        ElseIf rbtnExportCSV.Checked Then
            TempList.DataExportFormat = rbtnExportCSV.Text
        ElseIf rbtnExportSSV.Checked Then
            TempList.DataExportFormat = rbtnExportSSV.Text
        End If
        TempList.UpdateAssetsWhenUsed = chkUpdateAssetsWhenUsed.Checked
        TempList.Usage = chkUsage.Checked
        TempList.Fees = chkFees.Checked
        TempList.CalcBuyBuyOrder = chkBuyorBuyOrder.Checked

        ' Save the data in the XML file
        Call TempSettings.SaveShoppingListSettings(TempList)

        MsgBox("Shopping List Settings Saved", vbInformation, Application.ProductName)
        Application.UseWaitCursor = False

    End Sub

    ' Save the lists to file 
    Private Sub btnSaveListToFile_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveListToFile.Click
        Dim MyStream As StreamWriter
        Dim FileName As String
        Dim OutputText As String
        Dim ListItem As ListViewItem

        Dim Items As ListView.ListViewItemCollection
        Dim i As Integer = 0

        ' Show the dialog
        Dim ExportTypeString As String
        Dim BuyListHeader As String
        Dim BuildListHeader As String
        Dim ItemsListHeader As String
        Dim Separator As String

        If rbtnExportCSV.Checked Then
            ' Save file name with date
            FileName = "Shopping List - " & Format(Now, "MMddyyyy") & ".csv"
            ExportTypeString = CSVDataExport
            Separator = ","
            BuyListHeader = BuyListHeaderCSV
            BuildListHeader = BuildListHeaderCSV
            ItemsListHeader = ItemsListHeaderCSV & ItemsListHeaderCSVAdd
            SaveFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
        ElseIf rbtnExportSSV.Checked Then
            ' Save file name with date
            FileName = "Shopping List - " & Format(Now, "MMddyyyy") & ".ssv"
            ExportTypeString = SSVDataExport
            Separator = ";"
            BuyListHeader = BuyListHeaderSSV
            BuildListHeader = BuildListHeaderSSV
            ItemsListHeader = ItemsListHeaderSSV & ItemsListHeaderSSVAdd
            SaveFileDialog.Filter = "ssv files (*.ssv*)|*.ssv*|All files (*.*)|*.*"
        Else
            ' Save file name with date
            FileName = "Shopping List - " & Format(Now, "MMddyyyy") & ".txt"
            ExportTypeString = DefaultTextDataExport
            Separator = "|"
            BuyListHeader = BuyListHeaderTXT
            BuildListHeader = BuildListHeaderTXT
            ItemsListHeader = ItemsListHeaderTXT & ItemsListHeaderTXTAdd
            SaveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        End If

        SaveFileDialog.FilterIndex = 1
        SaveFileDialog.RestoreDirectory = True
        SaveFileDialog.FileName = FileName

        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                MyStream = File.CreateText(SaveFileDialog.FileName)

                If Not (MyStream Is Nothing) Then

                    ' Output the buy list first
                    Items = lstBuy.Items

                    If Items.Count > 0 Then
                        Me.Cursor = Cursors.WaitCursor

                        Application.DoEvents()

                        OutputText = BuyListLabel
                        MyStream.Write(OutputText & Environment.NewLine)
                        OutputText = BuyListHeader
                        MyStream.Write(OutputText & Environment.NewLine)

                        For Each ListItem In Items
                            Application.DoEvents()

                            ' Build the output text 
                            If ExportTypeString = SSVDataExport Then
                                ' Format to EU
                                OutputText = ListItem.SubItems(1).Text & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(2).Text) & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(3).Text) & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(4).Text) & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(5).Text) & Separator
                                OutputText = OutputText & ListItem.SubItems(6).Text & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(7).Text) & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(8).Text) & Separator
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(9).Text) 
                            Else
                                OutputText = ListItem.SubItems(1).Text & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(2).Text, "Fixed") & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(3).Text, "Fixed") & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(4).Text, "Fixed") & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(5).Text, "Fixed") & Separator
                                OutputText = OutputText & ListItem.SubItems(6).Text & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(7).Text, "Fixed") & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(8).Text, "Fixed") & Separator
                                OutputText = OutputText & Format(ListItem.SubItems(9).Text, "Fixed") 
                            End If

                            MyStream.Write(OutputText & Environment.NewLine)
                        Next

                    End If

                    MyStream.Write("" & Environment.NewLine)

                    ' Output the build list
                    Items = lstBuild.Items

                    If Items.Count > 0 Then
                        Me.Cursor = Cursors.WaitCursor

                        Application.DoEvents()

                        OutputText = BuildListLabel
                        MyStream.Write(OutputText & Environment.NewLine)
                        OutputText = BuildListHeader
                        MyStream.Write(OutputText & Environment.NewLine)

                        For Each ListItem In Items
                            Application.DoEvents()

                            ' Build the output text for checked items
                            OutputText = ListItem.SubItems(1).Text & Separator
                            If ExportTypeString = SSVDataExport Then
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(2).Text) & Separator
                            Else
                                OutputText = OutputText & Format(ListItem.SubItems(2).Text, "Fixed") & Separator
                            End If

                            OutputText = OutputText & ListItem.SubItems(3).Text 

                            MyStream.Write(OutputText & Environment.NewLine)
                        Next

                    End If

                    MyStream.Write("" & Environment.NewLine)

                    ' Output the item list
                    Items = lstItems.Items

                    If Items.Count > 0 Then
                        Me.Cursor = Cursors.WaitCursor

                        Application.DoEvents()
                        Dim TempName As String = ""
                        Dim TempRelic As String = ""

                        OutputText = ItemsListLabel
                        MyStream.Write(OutputText & Environment.NewLine)
                        OutputText = ItemsListHeader
                        MyStream.Write(OutputText & Environment.NewLine)

                        For Each ListItem In Items
                            Application.DoEvents()

                            If ListItem.SubItems(1).Text.Contains("(") Then
                                TempName = ListItem.SubItems(1).Text.Substring(0, InStr(ListItem.SubItems(1).Text, "(") - 2)
                                TempRelic = ListItem.SubItems(1).Text.Substring(InStr(ListItem.SubItems(1).Text, "("), InStr(ListItem.SubItems(1).Text, ")") - InStr(ListItem.SubItems(1).Text, "(") - 1)
                            Else
                                TempName = ListItem.SubItems(1).Text
                                TempRelic = ""
                            End If
                            
                            ' Build the output text for checked items
                            OutputText = TempName & Separator
                            If ExportTypeString = SSVDataExport Then
                                OutputText = OutputText & ConvertUStoEUDecimal(ListItem.SubItems(2).Text) & Separator
                            Else
                                OutputText = OutputText & Format(ListItem.SubItems(2).Text, "Fixed") & Separator
                            End If
                            OutputText = OutputText & ListItem.SubItems(3).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(4).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(5).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(6).Text & Separator
                            OutputText = OutputText & TempRelic & Separator
                            OutputText = OutputText & ListItem.SubItems(8).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(9).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(10).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(11).Text & Separator
                            OutputText = OutputText & ListItem.SubItems(7).Text & Separator ' Add location on the end
                            MyStream.Write(OutputText & Environment.NewLine)
                        Next

                    End If

                    MyStream.Flush()
                    MyStream.Close()

                    MsgBox("Shopping List Saved", vbInformation, Application.ProductName)

                End If
            Catch
                MsgBox(Err.Description, vbExclamation, Application.ProductName)
            End Try
        End If

        ' Done processing the blueprints
        Me.Cursor = Cursors.Default
        Me.Refresh()
        Application.DoEvents()

    End Sub

    ' Load the lists from file 
    Private Sub btnLoadListFromFile_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadListFromFile.Click

        ' Logic - import the lists, rebuild all blueprints with parameters listed in the items list
        ' then read through just the items and quantities and run the updates if they are not the same
        ' this will allow users to make any changes and save them to be reloaded later
        Dim BPStream As StreamReader = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        Dim Line As String
        Dim CurrentList As String = ""
        Dim TempBP As Blueprint = Nothing
        Dim readerBP As SQLiteDataReader
        Dim SQL As String

        ' Import Lists
        Dim BuyList As New List(Of ItemQuantity)
        Dim BuildList As New List(Of ItemQuantity)
        Dim ItemList As New List(Of BPItem)
        Dim TempItem As ItemQuantity
        Dim TempBPItem As BPItem
        Dim Separator As String = ""

        Dim BuyListHeader As String
        Dim BuildListHeader As String
        Dim ItemsListHeader As String
        Dim ItemsListHeaderAdd As String

        ' To save the old shopping list in case of an error and to reload
        Dim SavedShoppingList As ShoppingList = CType(TotalShoppingList.Clone, ShoppingList)

        ' For cloning to update
        Dim ClonedBuyList As New Materials
        Dim ClonedBuildList As New BuiltItemList

        'openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "csv files (*.csv)|*.csv|ssv files (*.ssv*)|*.ssv*|txt files (*.txt*)|*.txt*|All files (*.*)|*.*"
        If rbtnExportSSV.Checked = True Then
            openFileDialog1.FileName = "*.ssv"
            Separator = ";"
            BuyListHeader = BuyListHeaderSSV
            BuildListHeader = BuildListHeaderSSV
            ItemsListHeader = ItemsListHeaderSSV
            ItemsListHeaderAdd = ItemsListHeaderSSV & ItemsListHeaderSSVAdd
        ElseIf rbtnExportCSV.Checked = True Then
            openFileDialog1.FileName = "*.csv"
            Separator = ","
            BuyListHeader = BuyListHeaderCSV
            BuildListHeader = BuildListHeaderCSV
            ItemsListHeader = ItemsListHeaderCSV
            ItemsListHeaderAdd = ItemsListHeaderCSV & ItemsListHeaderCSVAdd
        Else
            openFileDialog1.FileName = "*.txt"
            Separator = "|"
            BuyListHeader = BuyListHeaderTXT
            BuildListHeader = BuildListHeaderTXT
            ItemsListHeader = ItemsListHeaderTXT
            ItemsListHeaderAdd = ItemsListHeaderTXT & ItemsListHeaderTXTAdd
        End If

        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                BPStream = New System.IO.StreamReader(openFileDialog1.FileName)

                If (BPStream IsNot Nothing) Then
                    Application.UseWaitCursor = True

                    ' Clear the old list - adding to an existing list is a bit of work, so just make this a requirement
                    Call ClearLists()

                    ' Read the file line by line here, start with headers
                    Line = BPStream.ReadLine

                    While Line IsNot Nothing
                        Application.DoEvents()

                        ' If you save a CSV in Excel, it'll put in extra commas - check and remove
                        If Line.Contains(BuyListLabel) Then
                            CurrentList = BuyListLabel
                            Line = Line.Replace(",", "")
                        ElseIf Line.Contains(BuildListLabel) Then
                            CurrentList = BuildListLabel
                            Line = Line.Replace(",", "")
                        ElseIf Line.Contains(ItemsListLabel) Then
                            CurrentList = ItemsListLabel
                            Line = Line.Replace(",", "")
                        End If

                        ' If the line has records, import it into the correct lists
                        If Line.Contains(Separator) And _
                            Not (Line.Contains(BuyListHeader) Or Line.Contains(BuildListHeader) Or Line.Contains(ItemsListHeader) Or Line.Contains(ItemsListHeaderAdd)) Then
                            ' Parse the line
                            Dim Record As String()

                            ' Set the split records
                            If Separator = ";" Then
                                ' To properly process, need to replace swap all the commas and periods
                                ' For R.A.M.'s, special processing
                                If Line.Substring(0, 6) = "R.A.M." Then
                                    Line = "R.A.M." & Line.Substring(6).Replace(".", "") ' Just replace the periods as they are commas for numbers, which aren't needed, after the R.A.M.
                                ElseIf Line.Substring(0, 4) = "R.Db" Then
                                    Line = "R.Db" & Line.Substring(3).Replace(".", "") ' Just replace the periods as they are commas for numbers, which aren't needed, after the R.Db
                                Else
                                    Line = Line.Replace(".", "") ' Just replace the periods as they are commas for numbers, which aren't needed
                                End If
                                Line = Line.Replace(",", ".")
                                Record = Line.Split(New Char() {";"c})
                            ElseIf Separator = "," Then
                                Record = Line.Split(New Char() {","c})
                            Else
                                Record = Line.Split(New Char() {"|"c})
                            End If

                            ' Make sure the record has a name
                            If Trim(Record(0)) <> "" Then
                                ' Import the current list
                                Select Case CurrentList
                                    Case BuyListLabel
                                        ' Buy List Format: Material, Quantity, Cost Per Item, Buy Type, Total m3, Isk/m3, TotalCost
                                        ' Add just the name and quantity to the list for checking later
                                        TempItem.ItemName = Record(0)
                                        If Trim(Record(1)) = "" Then
                                            TempItem.ItemQuantity = 1 ' Blank is one item (unpackaged)
                                        Else
                                            TempItem.ItemQuantity = CLng(Record(1))
                                        End If
                                        Call BuyList.Add(TempItem)
                                    Case BuildListLabel
                                        ' Build List Format: Build Item, Quantity, ME
                                        ' Add just the name and quantity to the list for checking later
                                        TempItem.ItemName = Record(0)
                                        If Trim(Record(1)) = "" Then
                                            TempItem.ItemQuantity = 1 ' Blank is one item (unpackaged)
                                        Else
                                            TempItem.ItemQuantity = CLng(Record(1))
                                        End If
                                        TempItem.ItemME = CInt(Record(2))
                                        Call BuildList.Add(TempItem)
                                    Case ItemsListLabel
                                        ' Item List Format: Item, Quantity, ME, NumBPs, Build Type, Decryptor, Facility ME Modifier
                                        ' POS Build, IgnoredInvention, IgnoredMinerals, IgnoredT1BaseItem - might not have these
                                        ' Save all the fields
                                        TempBPItem.ItemName = Record(0)
                                        If Trim(Record(1)) = "" Then
                                            TempBPItem.ItemQuantity = 1 ' Blank is one item (unpackaged)
                                        Else
                                            TempBPItem.ItemQuantity = CLng(Record(1))
                                        End If
                                        TempBPItem.ItemME = CInt(Record(2))
                                        TempBPItem.NumBPs = CInt(Record(3))
                                        TempBPItem.BuildType = Record(4)
                                        TempBPItem.Decryptor = Record(5)
                                        TempBPItem.Relic = Record(6)

                                        If Record.Count = 11 Then
                                            ' Add the new records
                                            TempBPItem.BuiltInPOS = CBool(Record(7))
                                            TempBPItem.IgnoredInvention = CBool(Record(8))
                                            TempBPItem.IgnoredMinerals = CBool(Record(9))
                                            TempBPItem.IgnoredT1BaseItem = CBool(Record(10))
                                            TempBPItem.BuildLocation = Record(11)
                                        Else
                                            TempBPItem.BuiltInPOS = False
                                            TempBPItem.IgnoredInvention = False
                                            TempBPItem.IgnoredMinerals = False
                                            TempBPItem.IgnoredT1BaseItem = False
                                            TempBPItem.BuildLocation = ""
                                        End If

                                        Call ItemList.Add(TempBPItem)
                                End Select
                            End If
                        End If

                        Line = BPStream.ReadLine ' Read next line

                    End While

                    ' Now that all the lists are loaded, we need to build the blueprints and adjust the final lists
                    For i = 0 To ItemList.Count - 1

                        ' Look up BP data
                        SQL = "SELECT BLUEPRINT_ID, TECH_LEVEL, ITEM_GROUP_ID, ITEM_CATEGORY_ID FROM ALL_BLUEPRINTS WHERE ITEM_NAME = '" & FormatDBString(ItemList(i).ItemName) & "'"

                        DBCommand = New SQLiteCommand(SQL, DB)
                        readerBP = DBCommand.ExecuteReader
                        readerBP.Read()

                        ' Get the decryptor
                        Dim TempDecryptor As New Decryptor
                        Dim BuildBuy As Boolean
                        Dim FacilityType As String
                        Dim InventionDecryptors As New DecryptorList()
                        TempDecryptor = InventionDecryptors.GetDecryptor(ItemList(i).Decryptor)

                        If ItemList(i).BuildType = "Build/Buy" Then
                            BuildBuy = True
                        Else
                            BuildBuy = False
                        End If

                        If ItemList(i).BuiltInPOS Then
                            FacilityType = POSFacility
                        Else
                            FacilityType = StationFacility
                        End If

                        ' Determine the build facility 
                        Dim TempBuildFacility As IndustryFacility
                        Dim TempIndyType As IndustryType
                        TempIndyType = GetProductionType(ActivityManufacturing, readerBP.GetInt64(2), readerBP.GetInt64(3), FacilityType)
                        TempBuildFacility = GetManufacturingFacility(TempIndyType, BPTab, True)

                        ' Build the Item - use everything we can from file import except the component facilities, that's just too hard to save now
                        TempBP = New Blueprint(CLng(readerBP.GetValue(0)), ItemList(i).ItemQuantity, ItemList(i).ItemME, 0, ItemList(i).NumBPs, 1, _
                        SelectedCharacter, UserApplicationSettings, BuildBuy, 0, NoTeam, TempBuildFacility, _
                        NoTeam, SelectedBPComponentManufacturingFacility, SelectedBPCapitalComponentManufacturingFacility)

                        ' See if we invent, use selected BP facilities for invention
                        If readerBP.GetInt32(1) <> 1 And Not ItemList(i).IgnoredInvention Then
                            Dim MaximumLaboratoryLines As Integer = SelectedCharacter.Skills.GetSkillLevel(3406) + SelectedCharacter.Skills.GetSkillLevel(24624) + 1

                            TempBP.InventBlueprint(MaximumLaboratoryLines, TempDecryptor, SelectedBPInventionFacility, SelectedBPInventionTeam, _
                                                   SelectedBPCopyFacility, SelectedBPCopyTeam, GetInventItemTypeID(CLng(readerBP.GetValue(0)), ItemList(i).Relic))
                        End If

                        ' Build the item and get the list of materials
                        Call TempBP.BuildItems(frmMain.chkBPTaxes.Checked, frmMain.chkBPTaxes.Checked, frmMain.chkBPFacilityIncludeUsage.Checked, ItemList(i).IgnoredMinerals, ItemList(i).IgnoredT1BaseItem)

                        ' Add to shopping list but use BP tab settings
                        Call AddToShoppingList(TempBP, BuildBuy, frmMain.rbtnBPRawmatCopy.Checked, frmMain.rbtnBPComponentCopy.Checked, _
                                               TempBuildFacility.MaterialMultiplier, ItemList(i).BuiltInPOS, ItemList(i).IgnoredInvention, _
                                               ItemList(i).IgnoredMinerals, ItemList(i).IgnoredT1BaseItem)

                    Next

                    ' All the items have been built and added to shopping list, now update the materials and build lists with what the user adjusted in build and buy

                    ' Check built items first, loop through all required components, if not there remove, if number is different, adjust
                    If Not IsNothing(TotalShoppingList.GetFullBuiltItemList.GetBuiltItemList) Then

                        ' Clone this for updates
                        ClonedBuildList = CType(TotalShoppingList.GetFullBuiltItemList.Clone, BuiltItemList)

                        For i = 0 To ClonedBuildList.GetBuiltItemList.Count - 1
                            Dim TempItemQuantity As ItemQuantity = Nothing
                            Dim CurrentItem As BuiltItem = ClonedBuildList.GetBuiltItemList(i)

                            ' Look it up in the imported list
                            ItemQuantityToFind.ItemName = CurrentItem.ItemName
                            TempItemQuantity = BuildList.Find(AddressOf FindItemQuantity)

                            If Not IsNothing(TempItemQuantity.ItemName) Then
                                ' See what quantity is set and update if different
                                If CurrentItem.ItemQuantity <> TempItemQuantity.ItemQuantity Then
                                    Call TotalShoppingList.UpdateShoppingBuiltItemQuantity(CurrentItem, TempItemQuantity.ItemQuantity)
                                End If
                            Else
                                ' Not found so we need to remove the total item
                                Call TotalShoppingList.UpdateShoppingBuiltItemQuantity(CurrentItem, 0)
                            End If

                        Next
                    End If

                    ' Now check all the materials
                    If Not IsNothing(TotalShoppingList.GetFullBuyList.GetMaterialList) Then

                        ' Clone this for updates
                        ClonedBuyList = CType(TotalShoppingList.GetFullBuyList.Clone, Materials)

                        For i = 0 To ClonedBuyList.GetMaterialList.Count - 1
                            Dim TempItemQuantity As ItemQuantity = Nothing
                            Dim CurrentItem As Material = ClonedBuyList.GetMaterialList(i)

                            ' Look it up in the imported list
                            ItemQuantityToFind.ItemName = CurrentItem.GetMaterialName
                            TempItemQuantity = BuyList.Find(AddressOf FindItemQuantity)

                            If Not IsNothing(TempItemQuantity.ItemName) Then
                                ' See what quantity is set and update if different
                                If CurrentItem.GetQuantity <> TempItemQuantity.ItemQuantity Then
                                    ' See what quantity is set and update if different
                                    Call TotalShoppingList.UpdateShoppingBuyQuantity(CurrentItem.GetMaterialName, TempItemQuantity.ItemQuantity)
                                End If
                            Else
                                ' Not found so we need to remove the total item quantity
                                Call TotalShoppingList.UpdateShoppingBuyQuantity(CurrentItem.GetMaterialName, 0)
                            End If
                        Next
                    End If

                    Application.UseWaitCursor = False
                    ' Now load all the lists
                    Call RefreshLists()

                    ' Mark as items in list
                    frmMain.pnlShoppingList.Text = "Items in Shopping List"
                    frmMain.pnlShoppingList.ForeColor = Color.Red

                    MsgBox("Shopping List Loaded", vbInformation, Application.ProductName)

                End If

            Catch Ex As Exception
                ' Error'd so restore old shopping list
                TotalShoppingList = CType(SavedShoppingList.Clone, ShoppingList)
                Application.UseWaitCursor = False
                MessageBox.Show("Cannot read file from disk - Error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (BPStream IsNot Nothing) Then
                    BPStream.Close()
                End If
            End Try
        End If

        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

    ' Copy's data shown and exports it to clipboard
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim ClipboardData As New DataObject
        Dim MatList() As String
        Dim BuildList() As String
        Dim ItemList() As String

        Dim i As Integer

        ' Get the order of the list of items that they set up by clicking on the columns - They could sort on any column so focus on unique columns to sort
        ReDim MatList(lstBuy.Items.Count - 1)
        ReDim BuildList(lstBuild.Items.Count - 1)
        ReDim ItemList(lstItems.Items.Count - 1)

        i = 0
        ' Material sort order - Just Name
        For Each item As ListViewItem In lstBuy.Items
            MatList(i) = item.SubItems(1).Text
            i += 1
        Next

        i = 0
        ' Build item sort order - Name, Quantity, and ME
        For Each item As ListViewItem In lstBuild.Items
            BuildList(i) = item.SubItems(1).Text & "|" & item.SubItems(2).Text & "|" & item.SubItems(3).Text
            i += 1
        Next

        i = 0
        ' List Item sort order Name, Quantity, ME, Num BPs, Build Type, Decryptor, Location
        For Each item As ListViewItem In lstItems.Items
            ItemList(i) = item.SubItems(1).Text & "|" & item.SubItems(2).Text & "|" & item.SubItems(3).Text & "|" & item.SubItems(4).Text & "|" & item.SubItems(5).Text & "|" & item.SubItems(6).Text & "|" & item.SubItems(7).Text
            i += 1
        Next

        Dim ExportTypeString As String

        If rbtnExportCSV.Checked Then
            ExportTypeString = CSVDataExport
        ElseIf rbtnExportSSV.Checked Then
            ExportTypeString = SSVDataExport
        Else
            ExportTypeString = DefaultTextDataExport
        End If

        ' Paste to clipboard
        Call CopyTextToClipboard(TotalShoppingList.GetClipboardList(ExportTypeString, True, MatList, ItemList, BuildList))

    End Sub

    Private Sub btnShowAssets_Click(sender As System.Object, e As System.EventArgs) Handles btnShowAssets.Click
        ' Make sure it's not disposed
        If IsNothing(frmShoppingAssets) Then
            ' Make new form
            frmShoppingAssets = New frmAssetsViewer(AssetWindow.ShoppingList)
        Else
            If frmShoppingAssets.IsDisposed Then
                ' Make new form
                frmShoppingAssets = New frmAssetsViewer(AssetWindow.ShoppingList)
            End If
        End If

        ' Now open the Asset List
        frmShoppingAssets.Show()
        frmShoppingAssets.Focus()

        Application.DoEvents()

    End Sub

    Private Sub chkTaxes_CheckedChanged(sender As System.Object, e As System.EventArgs)
        Call LoadFormStats()
    End Sub

    Private Sub chkFees_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFees.CheckedChanged
        If Not FirstLoad Then
            If chkFees.Checked = False Then
                ' Disable calc buy order type - since they aren't going to apply fees, we can't calculate it
                chkBuyorBuyOrder.Enabled = False
            Else
                chkBuyorBuyOrder.Enabled = True
            End If
            ' Reload the list
            Call LoadBuyList()
            Call LoadFormStats()
        End If
    End Sub

    Private Sub chkBuyorBuyOrder_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBuyorBuyOrder.CheckedChanged
        If Not FirstLoad Then
            ' Reload the list
            Call LoadBuyList()
            Call LoadFormStats()
        End If
    End Sub

    Private Sub chkUsage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUsage.CheckedChanged
        If Not FirstLoad Then
            Call LoadFormStats()
        End If
    End Sub

    Private Sub txtAddlCosts_GotFocus(sender As Object, e As System.EventArgs) Handles txtAddlCosts.GotFocus
        Call txtAddlCosts.SelectAll()
    End Sub

    Private Sub txtAddlCosts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAddlCosts.KeyDown
        Call ProcessCutCopyPasteSelect(txtAddlCosts, e)
        If AddlCostsValidEntry() Then
            If e.KeyCode = Keys.Enter Then
                Call LoadFormStats()
                txtAddlCosts.Focus()
            End If
        End If
    End Sub

    Private Sub txtAddlCosts_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddlCosts.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtAddlCosts_LostFocus(sender As Object, e As System.EventArgs) Handles txtAddlCosts.LostFocus
        If Trim(txtAddlCosts.Text) = "" Then
            txtAddlCosts.Text = "0.00"
            Call LoadFormStats()
        ElseIf IsNumeric(txtAddlCosts.Text) Then
            txtAddlCosts.Text = FormatNumber(txtAddlCosts.Text, 2)
            Call LoadFormStats()
        Else
            MsgBox("Invalid Additional Costs Entry", vbExclamation, Application.ProductName)
            txtAddlCosts.Focus()
        End If
    End Sub

    Private Function AddlCostsValidEntry() As Boolean
        If Trim(txtAddlCosts.Text) = "" Then
            ' Reset to 0
            txtAddlCosts.Text = "0.00"
        End If

        If IsNumeric(txtAddlCosts.Text) Then
            Return True
        Else
            MsgBox("Invalid Additional Costs Entry", vbExclamation, Application.ProductName)
            txtAddlCosts.Focus()
            Return False
        End If
    End Function

    Private Sub chkAlwaysOnTop_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAlwaysOnTop.CheckedChanged
        If chkAlwaysOnTop.Checked Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub btnCopyPasteAssets_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyPasteAssets.Click
        Dim f1 As New frmCopyandPaste(CopyPasteWindowType.Materials)

        f1.ShowDialog()

        ' Update with new materials
        If CopyPasteMaterialText <> "" Then
            CutPasteUpdate = True
            Call UpdateShoppingListwithAssets(ImportCopyPasteText(CopyPasteMaterialText))
            ' Refresh lists
            Call RefreshLists()
        End If

        CutPasteUpdate = False

        f1.Dispose()

    End Sub

    Private Sub lstBuild_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstBuild.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstBuild, ListView), BuildListColumnClicked, BuildListColumnSortOrder)
    End Sub

    ' Don't allow the first column to show with resize
    Private Sub lstBuild_ColumnWidthChanging(sender As Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lstBuild.ColumnWidthChanging
        If e.ColumnIndex = 0 Then
            e.Cancel = True
            e.NewWidth = lstItems.Columns(e.ColumnIndex).Width
        End If
    End Sub

    ' Double Click build and load the blueprint for the component they clicked
    Private Sub lstBuild_DoubleClick(sender As Object, e As System.EventArgs) Handles lstBuild.DoubleClick
        Dim rsBPLookup As SQLiteDataReader
        Dim SQL As String

        SQL = "SELECT BLUEPRINT_ID FROM ALL_BLUEPRINTS WHERE ITEM_ID = " & lstBuild.SelectedItems(0).SubItems(0).Text

        DBCommand = New SQLiteCommand(Sql, DB)
        rsBPLookup = DBCommand.ExecuteReader
        rsBPLookup.Read()

        Call frmMain.LoadBPfromDoubleClick(rsBPLookup.GetInt64(0), "Raw", None, "Shopping List", _
                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                           UserBPTabSettings.IncludeTaxes, UserBPTabSettings.IncludeFees, chkUsage.Checked, _
                                           lstBuild.SelectedItems(0).SubItems(3).Text, lstBuild.SelectedItems(0).SubItems(2).Text, "0", "1") ' Any buildable component here is one 1 bp
    End Sub

    Private Sub lstItems_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstItems.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstItems, ListView), ItemListColumnClicked, ItemListColumnSortOrder)
    End Sub

    ' Turn off resizing for the last 4 columns
    Private Sub lstItems_ColumnWidthChanging(sender As Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lstItems.ColumnWidthChanging

        If e.ColumnIndex >= 8 Or e.ColumnIndex = 0 Then
            e.Cancel = True
            e.NewWidth = lstItems.Columns(e.ColumnIndex).Width
        End If

    End Sub

    ' Double Click build and load the blueprint for the item they clicked
    Private Sub lstItems_DoubleClick(sender As Object, e As System.EventArgs) Handles lstItems.DoubleClick
        Dim InputDecryptor As String = ""
        Dim InputRelic As String = ""
        Dim Inputs As String = None
        Dim TempMaterial As Material = Nothing
        Dim rsBPLookup As SQLiteDataReader
        Dim SQL As String

        ' Check Decryptor
        If lstItems.SelectedItems(0).SubItems(5).Text <> "" Then
            InputDecryptor = lstItems.SelectedItems(0).SubItems(6).Text
        End If

        ' Check for relic
        If lstItems.SelectedItems(0).SubItems(1).Text.Contains("(") Then
            With lstItems.SelectedItems(0).SubItems(1)
                InputRelic = .Text.Substring(InStr(.Text, "("), InStr(.Text, ")") - InStr(.Text, "(") - 1)
            End With
        End If

        If InputDecryptor <> "" And InputRelic <> "" Then
            Inputs = InputDecryptor & "|" & InputRelic
        ElseIf InputRelic <> "" Then
            Inputs = InputRelic
        ElseIf InputDecryptor <> "" Then
            Inputs = InputDecryptor
        Else
            Inputs = ""
        End If

        SQL = "SELECT BLUEPRINT_ID FROM ALL_BLUEPRINTS WHERE ITEM_ID = " & lstItems.SelectedItems(0).SubItems(0).Text

        DBCommand = New SQLiteCommand(SQL, DB)
        rsBPLookup = DBCommand.ExecuteReader
        rsBPLookup.Read()

        ' Get the decryptor or relic used from the item
        Call frmMain.LoadBPfromDoubleClick(CLng(rsBPLookup.GetValue(0)), lstItems.SelectedItems(0).SubItems(5).Text, Inputs, "Shopping List", _
                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                           UserBPTabSettings.IncludeTaxes, UserBPTabSettings.IncludeFees, chkUsage.Checked, _
                                           lstItems.SelectedItems(0).SubItems(3).Text, lstItems.SelectedItems(0).SubItems(2).Text, "0", _
                                           lstItems.SelectedItems(0).SubItems(4).Text)
    End Sub

    Private Sub lstBuy_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstBuy.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstBuy, ListView), BuyListColumnClicked, BuyListColumnSortOrder)
    End Sub

    Private Sub lstBuild_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lstBuild.KeyDown
        If e.KeyCode = Keys.Delete Then
            Call DeleteBuilds()
        End If
    End Sub

    ' Don't allow resizing of the first oclumn (hidden)
    Private Sub lstBuy_ColumnWidthChanging(sender As Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lstBuy.ColumnWidthChanging
        If e.ColumnIndex = 0 Then
            e.Cancel = True
            e.NewWidth = lstItems.Columns(e.ColumnIndex).Width
        End If
    End Sub

    Private Sub lstBuy_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lstBuy.KeyDown
        If e.KeyCode = Keys.Delete Then
            Call DeleteMaterials()
        End If
    End Sub

    Private Sub lstItems_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lstItems.KeyDown
        If e.KeyCode = Keys.Delete Then
            Call DeleteItems()
        End If
    End Sub

    ' Add or take away tax from the total items from total and refresh prices
    Private Sub chkTotalItemTax_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If Not FirstLoad Then
            Call LoadFormStats()
        End If
    End Sub

    ' Add or take away brokers fees from the total items from total and refresh prices
    Private Sub chkTotalItemFees_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If Not FirstLoad Then
            Call LoadFormStats()
        End If
    End Sub

#Region "Delete list items"

    ' Checks to see if we have any items left and resets the lists and panel on frmmain and refreshes the lists
    Private Sub ReloadListsafterDelete()

        ' Just deleted, so notify
        Call PlayNotifySound()

        ' Check the total items in the list, if we delete all the materials, we aren't building anything anymore
        If IsNothing(TotalShoppingList.GetFullBuyList) Or TotalShoppingList.GetNumShoppingItems = 0 Then
            Call ClearLists()
        End If

        ' Refresh grids
        Call RefreshLists()

        Application.DoEvents()

    End Sub

    Private Sub DeleteItemStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DeleteItemStrip.Opening

        If lstItems.SelectedItems.Count = 0 Then
            e.Cancel = True
        End If

        ' Change the name of the strip to allow for multiple mat selection
        If lstItems.SelectedItems.Count > 1 Then
            DeleteItem.Text = "Delete Items"
        Else
            DeleteItem.Text = "Delete Item"
        End If

    End Sub

    Private Sub DeleteMaterialStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DeleteMaterialStrip.Opening

        If lstBuy.SelectedItems.Count = 0 Then
            e.Cancel = True
        End If

        ' Change the name of the strip to allow for multiple mat selection
        If lstBuy.SelectedItems.Count > 1 Then
            DeleteMaterial.Text = "Delete Materials"
        Else
            DeleteMaterial.Text = "Delete Material"
        End If

    End Sub

    Private Sub DeleteBuildStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DeleteBuildStrip.Opening

        If lstBuild.SelectedItems.Count = 0 Then
            e.Cancel = True
        End If

        ' Change the name of the strip to allow for multiple mat selection
        If lstBuild.SelectedItems.Count > 1 Then
            DeleteMaterial.Text = "Delete Build Items"
        Else
            DeleteMaterial.Text = "Delete Build Item"
        End If

    End Sub

    Private Sub DeleteItemStrip_Click(sender As System.Object, e As System.EventArgs) Handles DeleteItem.Click
        Call DeleteItems()
    End Sub

    Private Sub DeleteMaterialStrip_Click(sender As System.Object, e As System.EventArgs) Handles DeleteMaterial.Click
        Call DeleteMaterials()
    End Sub

    Private Sub DeleteBuildStrip_Click(sender As System.Object, e As System.EventArgs) Handles DeleteBuildItem.Click
        Call DeleteBuilds()
    End Sub

    Private Sub DeleteItems()
        Dim ShopListItem As New ShoppingListItem
        Dim SelectedItem As String

        If lstItems.SelectedItems.Count > 0 Then
            For i = 0 To lstItems.SelectedItems.Count - 1

                SelectedItem = lstItems.SelectedItems(i).SubItems(1).Text

                If SelectedItem <> "" Then
                    ' Get the name, build type, and ME, and meta of the item selected
                    If SelectedItem.Contains("(") Then
                        ' Strip off the relic from the name
                        ShopListItem.Name = SelectedItem.Substring(0, InStr(SelectedItem, "(") - 2)
                        ShopListItem.Relic = SelectedItem.Substring(InStr(SelectedItem, "("), InStr(SelectedItem, ")") - InStr(SelectedItem, "(") - 1)
                    Else
                        ShopListItem.Name = SelectedItem
                        ShopListItem.Relic = ""
                    End If
                    ShopListItem.Quantity = CLng(lstItems.SelectedItems(i).SubItems(2).Text)
                    ShopListItem.ItemME = CInt(lstItems.SelectedItems(i).SubItems(3).Text)
                    ShopListItem.NumBPs = CInt(lstItems.SelectedItems(i).SubItems(4).Text)
                    ShopListItem.BuildType = lstItems.SelectedItems(i).SubItems(5).Text
                    ShopListItem.Decryptor = lstItems.SelectedItems(i).SubItems(6).Text
                    ShopListItem.BuildLocation = lstItems.SelectedItems(i).SubItems(7).Text

                    ' Remove it from shopping listxc 
                    TotalShoppingList.UpdateShoppingItemQuantity(ShopListItem, 0)

                End If
            Next

            ' Just updated, so notify
            Call PlayNotifySound()
            Call RefreshLists()

        End If
    End Sub

    Private Sub DeleteMaterials()
        If lstBuy.SelectedItems.Count > 0 Then
            For i = 0 To lstBuy.SelectedItems.Count - 1
                ' Remove it
                Call TotalShoppingList.UpdateShoppingBuyQuantity(lstBuy.SelectedItems(i).SubItems(1).Text, 0)
            Next

            ' Just updated, so notify
            Call PlayNotifySound()
            Call RefreshLists()

        End If
    End Sub

    Private Sub DeleteBuilds()
        Dim TempBuiltItem As New BuiltItem
        Dim i As Integer

        If lstBuild.SelectedItems.Count > 0 Then
            For i = 0 To lstBuild.SelectedItems.Count - 1
                TempBuiltItem.ItemTypeID = CLng(lstBuild.SelectedItems(i).SubItems(0).Text)
                TempBuiltItem.ItemName = lstBuild.SelectedItems(i).SubItems(1).Text
                TempBuiltItem.ItemQuantity = CLng(lstBuild.SelectedItems(i).SubItems(2).Text)
                TempBuiltItem.BuildME = CInt(lstBuild.SelectedItems(i).SubItems(3).Text)

                ' Remove it from shopping list, sending the grid quantity
                TotalShoppingList.UpdateShoppingBuiltItemQuantity(TempBuiltItem, 0)
            Next

        End If

        ' Just updated, so notify
        Call PlayNotifySound()
        Call RefreshLists()
    End Sub

#End Region

#Region "InlineListUpdate"

    ' Determines where to show the text box when clicking on the list sent
    Private Sub ListClicked(ListRef As ListView, sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Dim iSubIndex As Integer = 0

        ' Hide the text box when a new line is selected
        txtListEdit.Hide()

        CurrentRow = ListRef.GetItemAt(e.X, e.Y) ' which listviewitem was clicked
        SelectedGrid = ListRef

        If CurrentRow Is Nothing Then
            Exit Sub
        End If

        CurrentCell = CurrentRow.GetSubItemAt(e.X, e.Y)  ' which subitem was clicked

        ' See which column has been clicked
        iSubIndex = CurrentRow.SubItems.IndexOf(CurrentCell)

        ' Determine where the previous and next item boxes will be based on what they clicked - used in tab event handling as well
        Call SetNextandPreviousCells(ListRef)

        ' Look at the buy list for price and quantity
        If ListRef.Name = lstBuy.Name Then
            ' Set the columns that can be edited, just Quantity and Price
            Select Case iSubIndex
                Case 1
                    ' Item - only showing box
                    UpdateQuantity = False
                    UpdatePrice = False
                    Call ShowUpdateTextBox(ListRef, HorizontalAlignment.Left)
                Case 2
                    UpdateQuantity = True
                    UpdatePrice = False
                    Call ShowUpdateTextBox(ListRef)
                Case 3
                    UpdateQuantity = False
                    UpdatePrice = True
                    Call ShowUpdateTextBox(ListRef)
                Case Else
                    UpdateQuantity = False
                    UpdatePrice = False
            End Select

        Else ' Just Quantity updates in the other two grids
            ' Set the columns that can be edited, just Price
            If iSubIndex = 2 Then
                UpdateQuantity = True
                UpdatePrice = False
                Call ShowUpdateTextBox(ListRef)
            ElseIf iSubIndex = 1 Then
                ' Show the item box for copy/paste purposes
                UpdateQuantity = False
                UpdatePrice = False
                Call ShowUpdateTextBox(ListRef, HorizontalAlignment.Left)
            Else
                UpdateQuantity = False
                UpdatePrice = False
            End If
        End If

    End Sub

    ' Shows the text box on the grid where clicked if enabled
    Private Sub ShowUpdateTextBox(ListRef As ListView, Optional TextAlignment As HorizontalAlignment = HorizontalAlignment.Right)
        Dim lLeft As Integer = 0
        Dim lWidth As Integer = 0

        ' Get size of column and location
        lLeft = CurrentCell.Bounds.Left + 2
        lWidth = CurrentCell.Bounds.Width

        ' Save the center location of the edit box
        SavedListClickLoc.X = CurrentCell.Bounds.Left + CInt(CurrentCell.Bounds.Width / 2)
        SavedListClickLoc.Y = CurrentCell.Bounds.Top + CInt(CurrentCell.Bounds.Height / 2)

        With txtListEdit
            .Hide()
            .SetBounds(lLeft + ListRef.Left, CurrentCell.Bounds.Top + ListRef.Top, lWidth, CurrentCell.Bounds.Height)
            .Text = CurrentCell.Text
            .Show()
            .TextAlign = TextAlignment
            .Focus()
        End With

    End Sub

    ' Determines where the previous and next item boxes will be based on what they clicked - used in tab event handling
    Private Sub SetNextandPreviousCells(ListRef As ListView, Optional CellType As String = "")
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
        If ListRef.Items.Count = 1 Then
            NextRow = CurrentRow
            PreviousRow = CurrentRow
        ElseIf CurrentRow.Index <> ListRef.Items.Count - 1 And CurrentRow.Index <> 0 Then
            ' Not the last line, so set the next and previous
            NextRow = ListRef.Items.Item(CurrentRow.Index + 1)
            PreviousRow = ListRef.Items.Item(CurrentRow.Index - 1)
        ElseIf CurrentRow.Index = 0 Then
            NextRow = ListRef.Items.Item(CurrentRow.Index + 1)
            ' Wrap to bottom
            PreviousRow = ListRef.Items.Item(ListRef.Items.Count - 1)
        ElseIf CurrentRow.Index = ListRef.Items.Count - 1 Then
            ' Need to wrap up to top
            NextRow = ListRef.Items.Item(0)
            PreviousRow = ListRef.Items.Item(CurrentRow.Index - 1)
        End If

        ' Check for buy list
        If ListRef.Name = lstBuy.Name Then

            ' The next row must be a Quantity or Price box on the next row 
            ' or a previous Quantity or Price box on the previous row
            Select Case iSubIndex
                Case 1 ' Just item
                    NextCell = CurrentRow.SubItems.Item(2) ' Current row Quantity box
                    NextCellRow = CurrentRow
                    PreviousCell = PreviousRow.SubItems.Item(3) ' Previous row Price box
                    PreviousCellRow = PreviousRow

                    UpdateQuantity = False
                    UpdatePrice = False
                Case 2 ' Quantity
                    NextCell = CurrentRow.SubItems.Item(3) ' Current row Price box
                    NextCellRow = CurrentRow
                    PreviousCell = CurrentRow.SubItems.Item(1) ' Current row Item box
                    PreviousCellRow = CurrentRow

                    UpdateQuantity = True
                    UpdatePrice = False
                Case 3 ' Price
                    NextCell = NextRow.SubItems.Item(1) ' Next row Item box
                    NextCellRow = NextRow
                    PreviousCell = CurrentRow.SubItems.Item(2) ' Current row Quantity box
                    PreviousCellRow = CurrentRow

                    UpdateQuantity = False
                    UpdatePrice = True
                Case Else
                    NextCell = Nothing
                    PreviousCell = Nothing
                    CurrentCell = Nothing
            End Select

        Else ' For quantity updates only
            ' Set the next and previous quantity boxes
            If iSubIndex = 1 Then
                NextCell = CurrentRow.SubItems.Item(2) ' Next quantity box
                NextCellRow = CurrentRow
                PreviousCell = PreviousRow.SubItems.Item(2) ' Previous quantity box
                PreviousCellRow = PreviousRow

                UpdateQuantity = True
                UpdatePrice = False
            ElseIf iSubIndex = 2 Then
                NextCell = NextRow.SubItems.Item(1) ' Next name box
                NextCellRow = NextRow
                PreviousCell = CurrentRow.SubItems.Item(1) ' Previous name box
                PreviousCellRow = CurrentRow

                UpdateQuantity = False
                UpdatePrice = False
            Else
                NextCell = Nothing
                PreviousCell = Nothing
                CurrentCell = Nothing
            End If
        End If

    End Sub

    ' For updating the items in the list by clicking on them
    Private Sub ProcessKeyDownUpdateEdit(SentKey As Keys, ListRef As ListView)
        Dim QuantityValue As Integer = 0
        Dim PriceValue As Double = 0
        Dim SQL As String

        ' Change blank entry to 0
        If Trim(txtListEdit.Text) = "" Then
            txtListEdit.Text = "0"
        End If

        ' If they hit enter or tab away, mark the BP as owned in the DB with the values entered
        If (SentKey = Keys.Enter Or SentKey = Keys.ShiftKey Or SentKey = Keys.Tab) And DataEntered Then

            ' Check the input first
            If Not IsNumeric(txtListEdit.Text) And UpdateQuantity Then
                MsgBox("Invalid Quantity Value", vbExclamation)
                Exit Sub
            End If

            If Not IsNumeric(txtListEdit.Text) And UpdatePrice Then
                MsgBox("Invalid Price Value", vbExclamation)
                Exit Sub
            End If

            ' Save the data depending on what we are updating
            If UpdateQuantity Then
                QuantityValue = CInt(txtListEdit.Text)
            End If

            If UpdatePrice Then
                PriceValue = CDbl(txtListEdit.Text)
            End If

            ' Update the quantity data in all three grids
            If UpdateQuantity Then

                ' Adjust the mats to what they enter - if it said 100, and they enter 90, then adjust to 90
                If ListRef.Name = lstBuy.Name Then ' The materials we buy to build items 
                    ' Check the numbers, if the same then don't update
                    If QuantityValue = CInt(CurrentRow.SubItems(2).Text) And PriceValue = CDbl(CurrentRow.SubItems(3).Text) Then
                        ' Skip down
                        GoTo Tabs
                    End If

                    ' Save the mats they probably have on hand to make this change - calc from value in grid vs. value entered
                    Dim OnHandQuantity As Long = CLng(CurrentRow.SubItems(2).Text) - QuantityValue
                    Dim OnHandMaterial As New Material(0, CurrentRow.SubItems(1).Text, "", OnHandQuantity, 0, 0, "")
                    TotalShoppingList.OnHandMatList.InsertMaterial(OnHandMaterial)

                    ' Update the buy list
                    Call TotalShoppingList.UpdateShoppingBuyQuantity(CurrentRow.SubItems(1).Text, QuantityValue)

                ElseIf ListRef.Name = lstBuild.Name Then ' The components we are building to make the item
                    ' Check the numbers, if the same then don't update
                    If QuantityValue = CInt(CurrentRow.SubItems(2).Text) Then
                        ' Skip down
                        GoTo Tabs
                    End If

                    Dim TempBuiltItem As New BuiltItem
                    TempBuiltItem.ItemTypeID = CLng(CurrentRow.SubItems(0).Text)
                    TempBuiltItem.ItemName = CurrentRow.SubItems(1).Text
                    TempBuiltItem.ItemQuantity = CLng(CurrentRow.SubItems(2).Text)
                    TempBuiltItem.BuildME = CInt(CurrentRow.SubItems(3).Text)

                    ' Save the built components they probably have on hand to make this change - calc from value in grid vs. value entered
                    Dim OnHandQuantity As Long = CLng(CurrentRow.SubItems(2).Text) - QuantityValue
                    Dim OnHandMaterial As New Material(0, CurrentRow.SubItems(1).Text, "", OnHandQuantity, 0, 0, "")
                    TotalShoppingList.OnHandComponentList.InsertMaterial(OnHandMaterial)

                    ' Update the build list
                    Call TotalShoppingList.UpdateShoppingBuiltItemQuantity(TempBuiltItem, QuantityValue)

                ElseIf ListRef.Name = lstItems.Name Then ' The items we are building
                    ' Check the numbers, if the same then don't update
                    If QuantityValue = CInt(CurrentRow.SubItems(2).Text) Then
                        ' Skip down
                        GoTo Tabs
                    End If

                    Dim ShopListItem As New ShoppingListItem
                    Dim TempName As String = CurrentRow.SubItems(1).Text
                    If TempName.Contains("(") Then
                        ShopListItem.Name = TempName.Substring(0, InStr(TempName, "(") - 2)
                        ShopListItem.Relic = TempName.Substring(InStr(TempName, "("), InStr(TempName, ")") - InStr(TempName, "(") - 1)
                    Else
                        ShopListItem.Name = TempName
                        ShopListItem.Relic = ""
                    End If
                    ShopListItem.Quantity = CLng(CurrentRow.SubItems(2).Text)
                    ShopListItem.ItemME = CInt(CurrentRow.SubItems(3).Text)
                    ShopListItem.NumBPs = CInt(CurrentRow.SubItems(4).Text)
                    ShopListItem.BuildType = CurrentRow.SubItems(5).Text
                    ShopListItem.Decryptor = CurrentRow.SubItems(6).Text
                    ShopListItem.InventedRunsPerBP = CInt(Math.Ceiling(ShopListItem.Quantity / ShopListItem.NumBPs))
                    ShopListItem.BuildLocation = CurrentRow.SubItems(7).Text

                    ' Update the full shopping list
                    Call TotalShoppingList.UpdateShoppingItemQuantity(ShopListItem, QuantityValue)

                End If

            ElseIf ListRef.Name = lstBuy.Name And UpdatePrice Then ' Price update on the lstBuy screen
                ' Update the price in the database
                SQL = "UPDATE ITEM_PRICES SET PRICE = " & CStr(CDbl(txtListEdit.Text)) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & CurrentRow.SubItems(0).Text
                Call ExecuteNonQuerySQL(SQL)

                ' Change the value in the price grid, but don't update the grid
                CurrentRow.SubItems(2).Text = FormatNumber(txtListEdit.Text, 2)

                ' Update the Prices
                Call UpdateProgramPrices()
                Me.Focus()

            Else
                GoTo Tabs
            End If

            Call RefreshLists()

            ' Just updated, so notify
            Call PlayNotifySound()

            ' Reset text they entered if tabbed
            If SentKey = Keys.ShiftKey Or SentKey = Keys.Tab Then
                txtListEdit.Text = ""
            End If

            ' Data updated, so reset
            DataEntered = False

            If SentKey = Keys.Enter Then
                ' Just refresh and select the current row
                CurrentRow.Selected = True
                txtListEdit.Visible = False
            End If

        End If

Tabs:
        ' If they hit tab, then tab to the next cell
        If SentKey = Keys.Tab Then
            If CurrentRow.Index = -1 Then
                ' Reset the current row based on the original click
                CurrentRow = ListRef.GetItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                CurrentCell = CurrentRow.GetSubItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                ' Reset the next and previous cells
                SetNextandPreviousCells(ListRef)
            End If

            CurrentCell = NextCell

            ' Reset these each time
            Call SetNextandPreviousCells(ListRef, "Next")
            If CurrentRow.Index = 0 Then
                ' Scroll to top
                ListRef.Items.Item(0).Selected = True
                ListRef.EnsureVisible(0)
                ListRef.Update()
            Else
                ' Make sure the row is visible
                ListRef.EnsureVisible(CurrentRow.Index)
            End If

            ' Show the text box
            If CurrentRow.SubItems.IndexOf(CurrentCell) = 1 Then
                Call ShowUpdateTextBox(ListRef, HorizontalAlignment.Left)
            Else
                Call ShowUpdateTextBox(ListRef)
            End If

        End If

        ' If shift+tab, then go to the previous cell 
        If SentKey = Keys.ShiftKey Then
            If CurrentRow.Index = -1 Then
                ' Reset the current row based on the original click
                CurrentRow = ListRef.GetItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                CurrentCell = CurrentRow.GetSubItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                ' Reset the next and previous cells
                SetNextandPreviousCells(ListRef)
            End If

            CurrentCell = PreviousCell
            ' Reset these each time
            Call SetNextandPreviousCells(ListRef, "Previous")
            If CurrentRow.Index = ListRef.Items.Count - 1 Then
                ' Scroll to bottom
                ListRef.Items.Item(ListRef.Items.Count - 1).Selected = True
                ListRef.EnsureVisible(ListRef.Items.Count - 1)
                ListRef.Update()
            Else
                ' Make sure the row is visible
                ListRef.EnsureVisible(CurrentRow.Index)
            End If

            ' Show the text box
            If CurrentRow.SubItems.IndexOf(CurrentCell) = 1 Then
                Call ShowUpdateTextBox(ListRef, HorizontalAlignment.Left)
            Else
                Call ShowUpdateTextBox(ListRef)
            End If

        End If

    End Sub

    ' Processes the tab function in the text box for the grid. This overrides the default tabbing between controls
    Protected Overrides Function ProcessTabKey(ByVal TabForward As Boolean) As Boolean
        Dim ac As Control = Me.ActiveControl

        If TabForward Then
            If ac Is txtListEdit Then
                Call ProcessKeyDownUpdateEdit(Keys.Tab, SelectedGrid)
                Return True
            End If
        Else
            If ac Is txtListEdit Then
                ' This is Shift + Tab but just send Shift for ease of processing
                Call ProcessKeyDownUpdateEdit(Keys.ShiftKey, SelectedGrid)
                Return True
            End If
        End If

        Return MyBase.ProcessTabKey(TabForward)

    End Function

    Private Sub txtListEdit_GotFocus(sender As Object, e As System.EventArgs) Handles txtListEdit.GotFocus
        Call txtListEdit.SelectAll()
    End Sub

    Private Sub txtListEdit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtListEdit.KeyDown
        If Not DataEntered Then ' If data already entered, then they didn't do it through paste
            DataEntered = ProcessCutCopyPasteSelect(txtListEdit, e)
        End If

        If e.KeyCode = Keys.Enter Then
            IgnoreFocusChange = True
            Call ProcessKeyDownUpdateEdit(Keys.Enter, SelectedGrid)
            IgnoreFocusChange = False
        End If
    End Sub

    Private Sub txtListEdit_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtListEdit.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If UpdateQuantity Then
                If allowedRunschars.IndexOf(e.KeyChar) = -1 Then
                    ' Invalid Character
                    e.Handled = True
                Else
                    DataEntered = True
                End If
            ElseIf UpdatePrice Then
                If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                    ' Invalid Character
                    e.Handled = True
                Else
                    DataEntered = True
                End If
            End If

        End If

    End Sub

    Private Sub txtListEdit_LostFocus(sender As Object, e As System.EventArgs) Handles txtListEdit.LostFocus
        If Not RefreshingGrid And DataEntered And Not IgnoreFocusChange Then
            Call ProcessKeyDownUpdateEdit(Keys.Enter, SelectedGrid)
        End If
        txtListEdit.Visible = False
    End Sub

    ' Grid clicks
    Private Sub lstBuild_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstBuild.MouseClick
        If e.Button <> Windows.Forms.MouseButtons.Right And Not My.Computer.Keyboard.ShiftKeyDown Then
            Call ListClicked(lstBuild, sender, e)
        End If
    End Sub

    Private Sub lstBuy_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstBuy.MouseClick
        If e.Button <> Windows.Forms.MouseButtons.Right And Not My.Computer.Keyboard.ShiftKeyDown Then
            Call ListClicked(lstBuy, sender, e)
        End If
    End Sub

    Private Sub lstItems_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstItems.MouseClick
        If e.Button <> Windows.Forms.MouseButtons.Right And Not My.Computer.Keyboard.ShiftKeyDown Then
            Call ListClicked(lstItems, sender, e)
        End If
    End Sub

    ' Detects Scroll event and hides boxes
    Private Sub lstBuild_ProcMsg(ByVal m As System.Windows.Forms.Message) Handles lstBuild.ProcMsg
        txtListEdit.Hide()
    End Sub

    ' Detects Scroll event and hides boxes
    Private Sub lstBuy_ProcMsg(ByVal m As System.Windows.Forms.Message) Handles lstBuy.ProcMsg
        txtListEdit.Hide()
    End Sub

    ' Detects Scroll event and hides boxes
    Private Sub lstItems_ProcMsg(ByVal m As System.Windows.Forms.Message) Handles lstItems.ProcMsg
        txtListEdit.Hide()
    End Sub

#End Region

End Class