
Imports System.Data.SQLite

Public Class ShoppingList
    Implements ICloneable

    ' Master Lists of materials to display. These are single lists that are updated when deleting quantity or full items
    Private TotalItemList As List(Of ShoppingListItem) ' This is the total list of items, with orginal values - not updated
    Private TotalBuyList As Materials ' Buy mats
    Private TotalBuildList As BuiltItemList ' Build mats (components)
    Private TotalInventionMats As Materials ' All Invention/RE materials used
    Private TotalCopyMats As Materials ' All the copy materials needed to make copies to invent

    ' Use onhandlist of materials so we can keep track of user entries (of calculations they make based on mats on hand) 
    Public OnHandMatList As New Materials
    Public OnHandComponentList As New Materials

    ' Price data
    Private AdditionalCosts As Double ' For any additional costs added on the shopping list form
    Private MaterialsBrokerFee As Double ' For the total broker fee for buying the materials in the list
    Private TotalListUsage As Double ' Total of all usage values for the items in the list
    Private TotalListMarketPrice As Double ' Total market price of everything in the list
    Private TotalListCost As Double ' Total cost of everything in the list mats (with invention/copy costs) + usage + taxes + fees
    Private TotalListBuildTime As Double ' Total time to build the items in the list in seconds
    Private TotalListInventionCost As Double ' Total of all the invention materials in the list
    Private TotalListCopyCost As Double ' Total of all the copy materials in the list

    Protected ItemToFind As ShoppingListItem
    Protected ProfitItemtoFind As String

    Public Sub New()

        Call Clear()

    End Sub

    Public Sub Clear()

        TotalItemList = New List(Of ShoppingListItem)

        TotalBuildList = New BuiltItemList
        TotalBuyList = New Materials

        TotalInventionMats = New Materials
        TotalCopyMats = New Materials

        AdditionalCosts = 0
        MaterialsBrokerFee = 0
        TotalListUsage = 0
        TotalListMarketPrice = 0
        TotalListCost = 0

        ItemToFind = Nothing

        ' Reset onhand mats lists
        OnHandMatList = New Materials
        OnHandComponentList = New Materials

    End Sub

#Region "Update Shopping List Functions"

    ' Removes or updates the item quantity and all mats associated with that item from the full list - i.e. Anshar
    Public Sub UpdateShoppingItemQuantity(ByVal SentItem As ShoppingListItem, ByVal UpdateItemQuantity As Long)
        Dim FoundItem As ShoppingListItem
        Dim FoundBuildItem As BuiltItem
        Dim TempBuiltItem As New BuiltItem
        Dim UpdatedRunQuantity As Long
        Dim BuiltComponents As New BuiltItemList

        ' First, see if there are any other items in the list, if this is the only one and the quantity is 0 then just clear all lists and leave
        If UpdateItemQuantity <= 0 And TotalItemList.Count = 1 And TotalItemList(0).Name = SentItem.Name Then
            Call Clear()
            Exit Sub
        End If

        ' Look for the item
        ItemToFind = SentItem
        FoundItem = TotalItemList.Find(AddressOf FindItem)

        ' Remove or update quantity for materials, built items, RE and invention mats
        ' Check the component list of the BP first, if we are building it, then update the number for built items, else we are buying it and update that
        If FoundItem IsNot Nothing Then
            ' Look at built items first, then check materials only
            If Not IsNothing(FoundItem.BPBuiltItems) Then
                With FoundItem.BPBuiltItems
                    For i = 0 To .GetBuiltItemList.Count - 1
                        ' Make sure the item exists (might have been deleted already in the main list) before updating
                        ' Find the built item in the build list for this item
                        Call TotalBuildList.SetItemToFind(.GetBuiltItemList(i))
                        FoundBuildItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

                        If FoundBuildItem IsNot Nothing Then
                            ' Copy current built item info
                            TempBuiltItem = New BuiltItem
                            TempBuiltItem = CType(FoundBuildItem.Clone, BuiltItem)

                            ' Use group name as facility location
                            Dim UpdateMaterial As New Material(TempBuiltItem.ItemTypeID, TempBuiltItem.ItemName, TempBuiltItem.ManufacturingFacility.FacilityName, TempBuiltItem.ItemQuantity,
                                                        TempBuiltItem.ItemVolume, 0, CStr(TempBuiltItem.BuildME), CStr(TempBuiltItem.BuildTE), True)

                            ' Figure out how many runs we need to do of this component item for the updated quantity of the main item
                            UpdatedRunQuantity = GetUpdatedQuantity("Build", FoundItem, UpdateItemQuantity, UpdateMaterial, True, TempBuiltItem.PortionSize)

                            ' Update built item name with runs we did to get this quantity
                            TempBuiltItem.ItemName = UpdateItemNamewithRuns(TempBuiltItem.ItemName, CLng(Math.Ceiling(UpdatedRunQuantity / TempBuiltItem.PortionSize)))

                            ' Need to update to the quantity sent in the built item list
                            Call UpdateShoppingBuiltItemQuantity(TempBuiltItem, UpdatedRunQuantity)

                        End If
                    Next
                End With
            End If

            ' Now look at the materials
            If Not IsNothing(FoundItem.BPMaterialList) Then
                With FoundItem.BPMaterialList
                    For i = 0 To .GetMaterialList.Count - 1
                        ' Look at buy items
                        If .GetMaterialList(i).GetBuildItem = False Then
                            ' Make sure the item exists (might have been deleted already in the main list) before updating
                            If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then
                                UpdatedRunQuantity = GetUpdatedQuantity("Buy", FoundItem, UpdateItemQuantity, .GetMaterialList(i), True)
                                ' Need to update to the quantity sent in the Buy list
                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedRunQuantity)
                            End If
                        End If

                        ' Update the quantity of the material in the total list too, but needs to be for each individual material
                        Dim x As Long = GetNewMatQuantity(FoundItem, IndustryActivities.Manufacturing, .GetMaterialList(i), UpdateItemQuantity)
                        Call .GetMaterialList(i).SetQuantity(x)
                    Next
                End With
            End If

            ' Update Buy List with invention mats
            If Not IsNothing(FoundItem.InventionMaterials) Then
                If Not IsNothing(FoundItem.InventionMaterials.GetMaterialList) Then
                    With FoundItem.InventionMaterials ' Update all base materials for this item first
                        Dim TempInventionMaterials As New Materials
                        For i = 0 To .GetMaterialList.Count - 1
                            ' Make sure the material exists (might have been deleted already in the main list) before updating
                            If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then
                                ' Need to update to the quantity sent in the Buy List
                                UpdatedRunQuantity = GetUpdatedQuantity("Invention", FoundItem, UpdateItemQuantity, .GetMaterialList(i), True)

                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedRunQuantity)
                                ' Update this material in the item's invention list for copy/paste function
                                If UpdatedRunQuantity > 0 Then
                                    ' Need to copy, remove, update, then add to update the volumes and prices of the material lists
                                    Dim TempMat As Material
                                    TempMat = CType(TotalInventionMats.SearchListbyName(.GetMaterialList(i).GetMaterialName).Clone, Material)
                                    Call TempInventionMaterials.InsertMaterial(TempMat)
                                End If
                            End If
                        Next

                        ' Reset the Invention Materials for this item
                        FoundItem.InventionMaterials = TempInventionMaterials

                    End With
                End If
            End If

            ' Update buy list with copy materials
            If Not IsNothing(FoundItem.CopyMaterials) Then
                If Not IsNothing(FoundItem.CopyMaterials.GetMaterialList) Then
                    With FoundItem.CopyMaterials ' Update all base materials for this item first
                        Dim TempCopyMaterials As New Materials
                        For i = 0 To .GetMaterialList.Count - 1
                            ' Make sure the material exists (might have been deleted already in the main list) before updating
                            If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then
                                ' Need to update to the quantity sent in the Buy List
                                UpdatedRunQuantity = GetUpdatedQuantity("Copying", FoundItem, UpdateItemQuantity, .GetMaterialList(i), True)

                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedRunQuantity)
                                ' Update this material in the item's invention list for copy/paste function
                                If UpdatedRunQuantity <= 0 Then
                                    Call TotalCopyMats.RemoveMaterial(.GetMaterialList(i))
                                Else
                                    ' Need to copy, remove, update, then add to update the volumes and prices of the material lists
                                    Dim TempMat As Material
                                    TempMat = CType(TotalCopyMats.SearchListbyName(.GetMaterialList(i).GetMaterialName).Clone, Material)
                                    Call TempCopyMaterials.InsertMaterial(TempMat)
                                End If
                            End If
                        Next

                        ' Reset the Invention Materials for this item
                        FoundItem.CopyMaterials = TempCopyMaterials

                    End With
                End If
            End If

            ' Need to increment or decrement the new item quantity and volume, the rest of the mats and components will be updated above
            If UpdateItemQuantity = 0 Then
                Call TotalItemList.Remove(FoundItem)
            Else
                ' This is simplistic but the easiest way to get an approximate value for a change in the shopping list - won't be exact!
                FoundItem.BuildVolume = FoundItem.BuildVolume / FoundItem.Runs * UpdateItemQuantity
                'FoundItem.TotalMaterialCost = FoundItem.TotalMaterialCost / FoundItem.Quantity * UpdateItemQuantity
                FoundItem.TotalUsage = FoundItem.TotalUsage / FoundItem.Runs * UpdateItemQuantity
                FoundItem.TotalItemMarketCost = FoundItem.TotalItemMarketCost / FoundItem.Runs * UpdateItemQuantity
                FoundItem.TotalBuildTime = FoundItem.TotalBuildTime / FoundItem.Runs * UpdateItemQuantity
                ' Update the invention jobs if they update this later
                If FoundItem.InventionJobs <> 0 Then
                    FoundItem.InventionJobs = CInt(Math.Ceiling(FoundItem.AvgInvRunsforSuccess * Math.Ceiling(UpdateItemQuantity / FoundItem.InventedRunsPerBP)))
                    ' How many bps do we need to make?
                    FoundItem.NumBPs = CInt(Math.Ceiling(UpdateItemQuantity / FoundItem.InventedRunsPerBP))
                End If
                ' Finally update the quantity
                FoundItem.Runs = UpdateItemQuantity
            End If
        End If

    End Sub

    ' Removes or updates a built item quantity from the build list and its materials from the material list - i.e. remove particle accelerator and raw mats from item - Hammerhead II
    Public Sub UpdateShoppingBuiltItemQuantity(ByRef SentItem As BuiltItem, ByVal UpdateItemQuantity As Long)
        Dim FoundItem As BuiltItem
        Dim UpdatedQuantity As Long ' This is the final mat quantity for updating the shopping buy/build list ammount
        Dim RefMatQuantity As Long ' This is the reference for materials we send in the update quantity - will be the mat quantity for that built item

        Dim UpdateItem As New BuiltItem
        Dim UpdateItemMatList As New Materials
        Dim UpdateBuiltItemMatList As New Materials
        Dim UpdateBuildList As New List(Of BuiltItem)
        Dim RefBuildList As New List(Of BuiltItem)
        Dim InsertMat As Material
        Dim InsertBuiltItem As New BuiltItem
        Dim BuiltComponentList As New List(Of BuiltItem)
        Dim ShoppingItem As New ShoppingListItem
        Dim UpdateMaterial As Material

        Dim SQL As String = ""
        Dim rsMatCheck As SQLiteDataReader

        Application.DoEvents()

        ' Task here: Update build list with correct quantity, and then the buy list with the correct material quantities

        ' First look up the item and the mats used to build it in the saved list
        Call TotalBuildList.SetItemToFind(SentItem)
        FoundItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

        ' Update the materials in the built list, take total number, divide mats by it and then multiply by quantity sent
        If Not IsNothing(FoundItem) Then
            With FoundItem.BuildMaterials
                For i = 0 To .GetMaterialList.Count - 1
                    ' Make sure the item exists (might have been deleted already in the main list) before updating
                    If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then
                        ' Make sure the material is part of the build materials for the built item
                        ' Look up mat quantities for this BP
                        SQL = "SELECT 'X' FROM ALL_BLUEPRINT_MATERIALS "
                        SQL &= "WHERE PRODUCT_ID = " & FoundItem.ItemTypeID & " AND MATERIAL_ID = " & .GetMaterialList(i).GetMaterialTypeID & " AND ACTIVITY IN (1,11)"

                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsMatCheck = DBCommand.ExecuteReader

                        If rsMatCheck.HasRows Then
                            ' Set the values we need to get updated quantity
                            ShoppingItem.Name = FoundItem.ItemName
                            ShoppingItem.TypeID = FoundItem.ItemTypeID
                            ShoppingItem.ManufacturingFacility = FoundItem.ManufacturingFacility
                            ShoppingItem.IncludeActivityCost = FoundItem.IncludeActivityCost
                            ShoppingItem.IncludeActivityTime = FoundItem.IncludeActivityTime
                            ShoppingItem.IncludeActivityUsage = FoundItem.IncludeActivityUsage
                            ShoppingItem.ItemME = FoundItem.BuildME
                            ShoppingItem.ItemTE = FoundItem.BuildTE
                            ShoppingItem.Runs = FoundItem.ItemQuantity
                            ShoppingItem.PortionSize = FoundItem.PortionSize

                            ' Blank these out for now if we use them later
                            ShoppingItem.InventionJobs = 0
                            ShoppingItem.InventedRunsPerBP = 0
                            ShoppingItem.AvgInvRunsforSuccess = 0
                            ShoppingItem.NumBPs = 1 ' Built items (components) are always one bp for now

                            UpdateMaterial = CType(.GetMaterialList(i).Clone, Material)

                            ' Get the new quantity for each material to build this item - which will be in the buy list
                            UpdatedQuantity = GetUpdatedQuantity("Buy", ShoppingItem, UpdateItemQuantity, UpdateMaterial, False, 1, RefMatQuantity)

                            ' Need to update to the quantity sent in the Buy List
                            Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedQuantity)

                            ' Save the updated materials for the build list with the difference
                            If UpdatedQuantity > 0 Then
                                With .GetMaterialList(i)
                                    ' Need to save the new value we want for this item, not the new quantity, to update the other lists with 
                                    InsertMat = New Material(.GetMaterialTypeID, .GetMaterialName, .GroupName, RefMatQuantity, .GetTotalVolume, 0, "", "")
                                End With

                                UpdateItemMatList.InsertMaterial(InsertMat)

                            End If
                        End If

                        rsMatCheck.Close()

                    End If
                Next
            End With

            ' Now update the materials from any component items that this item may have built for the found item
            For i = 0 To FoundItem.ComponentBuildList.Count - 1
                Dim RefBuiltItem As New BuiltItem

                ' Look up how many runs of the component we need
                If UpdateItemQuantity = 0 Then
                    ' Need to update the quantity in the build list to delete the total we needed here - so what's in build list now minus this quantity
                    Dim FoundBuildItem As New BuiltItem
                    ' Look up current built items
                    Call TotalBuildList.SetItemToFind(FoundItem.ComponentBuildList(i))
                    FoundBuildItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

                    If FoundBuildItem IsNot Nothing Then
                        UpdatedQuantity = FoundBuildItem.ItemQuantity - FoundItem.ComponentBuildList(i).ItemQuantity
                    Else
                        UpdatedQuantity = 0
                    End If

                    If UpdatedQuantity < 0 Then
                        UpdatedQuantity = 0
                    End If

                Else
                    ' Need to calculate the new amount
                    SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS "
                    SQL &= "WHERE BLUEPRINT_ID = " & FoundItem.BPTypeID
                    SQL &= " AND ACTIVITY IN (1,11)"
                    SQL &= " AND MATERIAL_ID = " & FoundItem.ComponentBuildList(i).ItemTypeID

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsMatCheck = DBCommand.ExecuteReader

                    If rsMatCheck.Read Then
                        ' Now adjust the quantity based on the ME bonus and runs for the original bp
                        Dim Runs As Long = CInt(Math.Ceiling(UpdateItemQuantity / FoundItem.PortionSize))
                        Dim MEBonus As Double = GetMEBonus(FoundItem.BuildME, FoundItem.ManufacturingFacility.MaterialMultiplier)
                        Dim TempBuildQuantity As Long = 0

                        RefBuiltItem = CType(FoundItem.ComponentBuildList(i).Clone, BuiltItem)

                        Dim FoundbuiltItem As BuiltItem
                        Dim ListmatQuantity As Long
                        Call TotalBuildList.SetItemToFind(RefBuiltItem)

                        FoundbuiltItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

                        If FoundbuiltItem IsNot Nothing Then
                            ListmatQuantity = FoundbuiltItem.ItemQuantity
                        Else
                            ListmatQuantity = 0
                        End If

                        TempBuildQuantity = CLng(Math.Max(Runs, Math.Ceiling(Math.Round(Runs * rsMatCheck.GetInt64(0) * MEBonus, 2))))

                        ' Figure out what we needed prior to update
                        Dim OldMatQuantity As Long = CLng(Math.Max(SentItem.BPRuns, Math.Ceiling(Math.Round(SentItem.BPRuns * rsMatCheck.GetInt64(0) * MEBonus, 2))))

                        ' Remove what was in the list prior (old mat quantity)
                        UpdatedQuantity = ListMatQuantity - OldMatQuantity

                        If UpdatedQuantity < 0 Then
                            UpdatedQuantity = 0
                        End If

                        ' Add what we need now
                        UpdatedQuantity += TempBuildQuantity

                        ' UpdatedQuantity = GetUpdatedQuantity("Buy", FoundItem, UpdateItemQuantity, UpdateMaterial, False, 1, RefMatQuantity)

                        ' Save what we need in the sent item reference
                        RefBuiltItem.ItemQuantity = TempBuildQuantity
                        RefBuiltItem.BPRuns = CLng(Math.Ceiling(TempBuildQuantity / RefBuiltItem.PortionSize))
                        RefBuiltItem.ItemName = UpdateItemNamewithRuns(RefBuiltItem.ItemName, RefBuiltItem.BPRuns)

                        rsMatCheck.Close()

                    End If
                End If

                ' Update this component in the main built item list with the new quantity
                Call UpdateShoppingBuiltItemQuantity(FoundItem.ComponentBuildList(i), UpdatedQuantity)

                ' Add the item with the new quantity to the reference build list
                UpdateBuildList.Add(RefBuiltItem)

            Next

            ' Save the base data
            UpdateItem = New BuiltItem
            UpdateItem = CType(FoundItem.Clone, BuiltItem)
            Dim BPRuns As Long = CLng(Math.Ceiling(UpdateItemQuantity / FoundItem.PortionSize))

            ' Update built item name with runs we did to get this quantity
            UpdateItem.ItemName = UpdateItemNamewithRuns(UpdateItem.ItemName, BPRuns)

            ' Update the new built list quantity and runs
            UpdateItem.ItemQuantity = UpdateItemQuantity
            UpdateItem.BPRuns = BPRuns
            ' Update the new built list material list, with updated quantities
            UpdateItem.BuildMaterials = UpdateItemMatList
            ' Update the component build list as well with updated quantities
            UpdateItem.ComponentBuildList = UpdateBuildList

            ' Update the data of the build list item
            Call TotalBuildList.RemoveBuiltItem(FoundItem) ' Remove the old one

            If UpdateItemQuantity <> 0 Then
                Call TotalBuildList.AddBuiltItem(UpdateItem) ' Add the updated one
            End If

            ' Update the sent item reference list
            SentItem.ComponentBuildList = RefBuildList

        End If

    End Sub

    ' Removes or updates the quantity of material in all lists of materials. i.e. Tritanium
    Public Sub UpdateShoppingBuyQuantity(ByVal SentItemName As String, ByVal Quantity As Long)

        If Not IsNothing(TotalBuyList) Then
            If Not IsNothing(TotalBuyList.GetMaterialList) Then

                If Quantity <= 0 Then
                    ' We just delete the item (all quantity) from the total materials list
                    Call TotalBuyList.RemoveMaterial(TotalBuyList.SearchListbyName(SentItemName))
                    ' Also remove from the total copy and invention lists
                    Call TotalInventionMats.RemoveMaterial(TotalInventionMats.SearchListbyName(SentItemName))
                    Call TotalCopyMats.RemoveMaterial(TotalCopyMats.SearchListbyName(SentItemName))
                Else
                    Dim TempMaterial As Material
                    Dim FindMaterial As Material = TotalBuyList.SearchListbyName(SentItemName)
                    ' Catch if item isn't in the list to what was sent
                    If Not IsNothing(FindMaterial) Then
                        TempMaterial = CType(FindMaterial.Clone, Material)
                        TotalBuyList.RemoveMaterial(TempMaterial)

                        ' Set the new quantity
                        Call TempMaterial.SetQuantity(Quantity)

                        ' Re-add so the prices are updated
                        TotalBuyList.InsertMaterial(TempMaterial)
                    End If

                    ' Update Invention mats (if there)
                    FindMaterial = TotalInventionMats.SearchListbyName(SentItemName)

                    ' Catch if item isn't in the list to what was sent
                    If Not IsNothing(FindMaterial) Then
                        TempMaterial = CType(FindMaterial.Clone, Material)
                        TotalInventionMats.RemoveMaterial(TempMaterial)

                        ' Set the new quantity
                        Call TempMaterial.SetQuantity(Quantity)

                        ' Re-add so the prices are updated
                        TotalInventionMats.InsertMaterial(TempMaterial)
                    End If

                    ' Update Copy mats (if there)
                    FindMaterial = TotalCopyMats.SearchListbyName(SentItemName)

                    ' Catch if item isn't in the list to what was sent
                    If Not IsNothing(FindMaterial) Then
                        TempMaterial = CType(FindMaterial.Clone, Material)
                        TotalCopyMats.RemoveMaterial(TempMaterial)

                        ' Set the new quantity
                        Call TempMaterial.SetQuantity(Quantity)

                        ' Re-add so the prices are updated
                        TotalCopyMats.InsertMaterial(TempMaterial)
                    End If

                End If
            End If
        End If

    End Sub

    ' Calculates the MEBonus based on inputs
    Private Function GetMEBonus(ItemME As Double, FacilityMEModifier As Double) As Double
        Return (1 - (ItemME / 100)) * FacilityMEModifier
    End Function

    ' Calculates the updated item quantity for updating lists
    ' ProcessingType = Invention/RE/Build/Buy
    ' CurrentItem = the current item in the shopping list for reference of old values
    ' NewMaterialQuantity = new quantity of the current item (not runs) we want to update
    ' UpdateMaterial = material of the item we are updating the quantity of based on the new item quantity
    ' UpdateMaterialPortionSize = portion size of the bp for the update material
    ' BuiltComponents = ref of any built components for the update material to save
    ' ShoppingItem = flag if it's a built item (uses quantity) or a main shopping item from the list (uses runs for quantity)
    Private Function GetUpdatedQuantity(ByVal ProcessingType As String, ByVal CurrentItem As ShoppingListItem,
                                        ByVal NewMaterialQuantity As Long, ByVal UpdateMaterial As Material,
                                        ByVal ShoppingItem As Boolean, Optional ByVal UpdateMaterialPortionSize As Long = 1,
                                        Optional ByRef RefMatQuantity As Long = 0) As Long
        Dim UpdatedQuantity As Long = 0
        Dim OnHandMats As Long
        Dim ListMatQuantity As Long
        Dim NumInventionJobs As Integer
        Dim NewMatQuantity As Long = 0
        Dim OldMatQuantity As Long = 0

        ' Set up the ME bonus and then calculate the new material quantity
        Dim MEBonus As Double = 0
        Dim SingleRunQuantity As Long = 0
        Dim ItemBPRuns As Long = 0

        Dim rsMatQuantity As SQLiteDataReader
        Dim SQL As String
        Dim ActivitySQL As String
        Dim ProductIDSQL As String

        ' Set how many runs of the main item bp are we going to do
        If ShoppingItem Then
            ItemBPRuns = NewMaterialQuantity
        Else
            ItemBPRuns = CLng(Math.Ceiling(NewMaterialQuantity / CurrentItem.PortionSize))
        End If

        Select Case ProcessingType
            Case "Invention"
                ProductIDSQL = CStr(CurrentItem.BlueprintTypeID)
                ActivitySQL = " AND ACTIVITY = 8"
            Case "Copying"
                ProductIDSQL = CStr(CurrentItem.BlueprintTypeID)
                ActivitySQL = " AND ACTIVITY = 5"
            Case Else
                ProductIDSQL = CStr(CurrentItem.TypeID)
                ActivitySQL = " AND ACTIVITY IN (1,11)"
        End Select

        ' Look up the single run quantity for the material
        SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS LEFT OUTER JOIN ALL_BLUEPRINTS ON ALL_BLUEPRINTS.ITEM_ID = ALL_BLUEPRINT_MATERIALS.MATERIAL_ID "
        SQL &= "WHERE PRODUCT_ID = " & ProductIDSQL & " AND MATERIAL_ID = " & UpdateMaterial.GetMaterialTypeID
        SQL &= ActivitySQL

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsMatQuantity = DBCommand.ExecuteReader

        If rsMatQuantity.Read Then
            SingleRunQuantity = rsMatQuantity.GetInt64(0)
            rsMatQuantity.Close()
        Else
            ' This item isn't required to build this bp, so exit
            rsMatQuantity.Close()
            RefMatQuantity = 0
            Return UpdateMaterial.GetQuantity
        End If

        ' Calc out final mat quantity
        If ProcessingType = "Invention" Or ProcessingType = "Copying" Then

            ListMatQuantity = TotalBuyList.SearchListbyName(UpdateMaterial.GetMaterialName).GetQuantity

            ' For invention materials, find out how many mats we need by calcuating the new value from the item runs and invention jobs per item
            If NewMaterialQuantity <= 0 Then
                ' Easy case, just remove the update material quantity (add the negative)
                NewMatQuantity = 0
            Else
                ' Here, we need to figure out how many items per run to remove (3 inv mats per job, and remove 2 items, then remove 6 invention mats)
                NumInventionJobs = CInt(Math.Ceiling(CurrentItem.AvgInvRunsforSuccess * Math.Ceiling(ItemBPRuns / CurrentItem.InventedRunsPerBP)))

                ' Update quantity based on invention calculations
                NewMatQuantity = NumInventionJobs * SingleRunQuantity
                RefMatQuantity = NewMatQuantity
            End If

        ElseIf ProcessingType = "Buy" Or ProcessingType = "Build" Then
            Dim UpdatedItemNumBPs As Integer = 0

            ' Figure out how many bps for the component we need 
            If CurrentItem.InventionJobs <> 0 Then
                If NewMaterialQuantity <> 0 Then
                    ' Calc how many bps we will need based off of invention
                    UpdatedItemNumBPs = CInt(Math.Ceiling(ItemBPRuns / CurrentItem.InventedRunsPerBP))
                Else
                    ' Deleting, so need the original amount and bps
                    UpdatedItemNumBPs = CurrentItem.NumBPs
                End If
            Else
                ' This isn't invented so just use the number of blueprints
                UpdatedItemNumBPs = CurrentItem.NumBPs

                ' Make sure we aren't building more bps than the quantity
                If UpdatedItemNumBPs > NewMaterialQuantity And NewMaterialQuantity <> 0 Then
                    UpdatedItemNumBPs = CInt(NewMaterialQuantity)
                End If

            End If

            Dim TempItemRuns As Long = 0

            If NewMaterialQuantity = 0 Then
                ' Deleting so use the original numbers to figure out what to remove later
                TempItemRuns = CurrentItem.Runs
            Else
                TempItemRuns = ItemBPRuns
            End If

            ' Set the minimum per bp, shouldn't go over the runs per bp since the user sends in the total numbps they need
            Dim RunsPerLine As Integer = CInt(Math.Floor(TempItemRuns / UpdatedItemNumBPs))
            ' Add these extra runs evenly (until gone) for each bp on runs per line
            Dim ExtraRuns As Integer = CInt(TempItemRuns - (RunsPerLine * UpdatedItemNumBPs))

            ' To track how many runs we have used in the batch setup
            Dim RunTracker As Long = 0
            Dim AdjRunsperBP As Integer
            ' List to use later to calc values
            Dim BlueprintsRunList As New List(Of Integer)

            ' Fill a list of runs per bp
            For i = 0 To UpdatedItemNumBPs - 1
                ' As we add the runs, adjust with extra runs proportionally until they are gone
                If ExtraRuns <> 0 Then
                    ' Since it's a fraction of a total batch run, this will always just be one until gone ** not right?
                    AdjRunsperBP = RunsPerLine + 1
                    ExtraRuns = ExtraRuns - 1 ' Adjust extra
                Else
                    ' No extra runs, so just add the original runs now
                    AdjRunsperBP = RunsPerLine
                End If

                ' Add the bp runs to the list to run
                BlueprintsRunList.Add(AdjRunsperBP)

            Next

            ' Set the ME of the main item to calculate how many runs we need of the component
            MEBonus = GetMEBonus(CurrentItem.ItemME, CurrentItem.ManufacturingFacility.MaterialMultiplier)

            ' Get the quantity from the correct list so we have the right total materials of all items using this material
            If ProcessingType = "Buy" Then
                ListMatQuantity = TotalBuyList.SearchListbyName(UpdateMaterial.GetMaterialName).GetQuantity
                ' Figure out what we needed prior to update
                Dim OldRuns As Long
                If ShoppingItem Then
                    OldRuns = CurrentItem.Runs
                Else
                    OldRuns = CLng(Math.Ceiling(CurrentItem.Runs / CurrentItem.PortionSize))
                End If

                OldMatQuantity = CLng(Math.Max(OldRuns, Math.Ceiling(Math.Round(OldRuns * SingleRunQuantity * MEBonus, 2))))

            ElseIf ProcessingType = "Build" Then
                Dim TempBuildItem As New BuiltItem
                TempBuildItem.ItemTypeID = UpdateMaterial.GetMaterialTypeID
                TempBuildItem.BuildME = CInt(UpdateMaterial.GetItemME)
                TempBuildItem.ManufacturingFacility.FacilityName = UpdateMaterial.GroupName

                Call TotalBuildList.SetItemToFind(TempBuildItem)

                Dim FoundBuiltItem As BuiltItem
                FoundBuiltItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)
                If FoundBuiltItem IsNot Nothing Then
                    ListMatQuantity = FoundBuiltItem.ItemQuantity
                Else
                    ListMatQuantity = 0
                End If

                ' Figure out what we needed prior to update - adjust ME bonus for each blueprint need, not the total
                'If Not ShoppingItem Then
                OldMatQuantity = CLng(Math.Max(CurrentItem.Runs, Math.Ceiling(Math.Round(CurrentItem.Runs * SingleRunQuantity * MEBonus, 2))))
                'Else
                '    For i = 0 To BlueprintsRunList.Count - 1
                '        OldMatQuantity += CLng(Math.Max(BlueprintsRunList(i), Math.Ceiling(Math.Round(BlueprintsRunList(i) * SingleRunQuantity * MEBonus, 2))))
                '    Next
                'End If

            End If

            If NewMaterialQuantity <> 0 Then
                ' Now for each bp, calc the runs with the ME value - only do this for buy
                If ProcessingType = "Buy" Then
                    For i = 0 To BlueprintsRunList.Count - 1
                        ' Set the quantity required = max(runs,ceil(round(runs * baseQuantity * materialModifier,2)) and sum for each bp
                        NewMatQuantity += CLng(Math.Max(BlueprintsRunList(i), Math.Ceiling(Math.Round(BlueprintsRunList(i) * SingleRunQuantity * MEBonus, 2))))
                    Next
                Else
                    ' Default to 1 run for build items to match the main blueprint function - this is the quantity of the new material for the item runs
                    NewMatQuantity = CLng(Math.Max(TempItemRuns, Math.Ceiling(Math.Round(TempItemRuns * SingleRunQuantity * MEBonus, 2))))
                End If

            Else
                ' Deleting, so no need to calculate, just reduce from what was in list for this item already
                NewMatQuantity = 0
            End If

            ' Set the mat quantity for reference
            RefMatQuantity = NewMatQuantity

        End If

        ' Update with onhand mats functionality
        If UpdateMaterial.GetBuildItem Then ' Building
            ' If entered, use this as the quantity to update to reflect that the user already entered a updated value
            OnHandMats = GetOnHandComponentQuantity(UpdateMaterial.GetMaterialName)
        Else ' Buying
            ' If entered, use this as the quantity to update to reflect that the user already entered a updated value
            OnHandMats = GetOnHandMaterialQuantity(UpdateMaterial.GetMaterialName)
        End If

        If OnHandMats <> 0 Then
            UpdatedQuantity = NewMatQuantity - OnHandMats
        Else
            UpdatedQuantity = NewMatQuantity ' Quantity we need now
        End If

        ' Decrease the mats in the shopping list from what we needed before then add what we need now
        UpdatedQuantity = (ListMatQuantity - OldMatQuantity) + UpdatedQuantity

        ' If the update caused it go below zero, reset
        If UpdatedQuantity < 0 Then
            UpdatedQuantity = 0
        End If

        Return UpdatedQuantity

    End Function

    Private Function GetNewMatQuantity(ByVal ItemData As ShoppingListItem, ByVal Activity As IndustryActivities,
                                       ByVal UpdateMaterial As Material, ByVal NewQuantity As Long) As Long
        ' Set up the ME bonus and then calculate the new material quantity
        Dim MEBonus As Double = 0
        Dim SingleRunQuantity As Long = 0
        Dim rsMatQuantity As SQLiteDataReader
        Dim SQL As String

        If NewQuantity = 0 Then
            Return 0
        End If

        ' Look up the cost for the material
        If Activity = IndustryActivities.Invention Or Activity = IndustryActivities.Copying Then
            SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS WHERE PRODUCT_ID = " & ItemData.BlueprintTypeID & " AND MATERIAL_ID = " & UpdateMaterial.GetMaterialTypeID
            SQL &= " AND ACTIVITY = 8"
        Else
            SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS WHERE PRODUCT_ID = " & ItemData.TypeID & " AND MATERIAL_ID = " & UpdateMaterial.GetMaterialTypeID
            SQL &= " AND ACTIVITY IN (1,11)"
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsMatQuantity = DBCommand.ExecuteReader
        rsMatQuantity.Read()

        SingleRunQuantity = rsMatQuantity.GetInt64(0)

        rsMatQuantity.Close()

        MEBonus = (1 - (ItemData.ItemME / 100)) * ItemData.ManufacturingFacility.MaterialMultiplier

        ' Figure out how many bps we need now and apply the ME bonus for each bp and sum up
        Dim NewNumBPs As Integer
        Dim NewRunsperBP As Integer

        If ItemData.InventionJobs <> 0 Then
            If ItemData.NumBPs = 1 Then
                NewNumBPs = CInt(Math.Ceiling(NewQuantity / ItemData.InventedRunsPerBP))
            Else
                NewNumBPs = CInt(Math.Ceiling(NewQuantity / (ItemData.Runs / ItemData.NumBPs)))
            End If

            NewRunsperBP = CInt(Math.Ceiling(NewQuantity / NewNumBPs))
        Else
            ' This isn't invented so just use the number of blueprints
            NewNumBPs = ItemData.NumBPs

            NewRunsperBP = CInt(Math.Ceiling(NewQuantity / NewNumBPs))

            ' Make sure we aren't building more bps than the quantity
            If NewNumBPs > NewQuantity Then
                NewNumBPs = CInt(NewQuantity)
            End If

        End If

        Dim NewMatQuantity As Long = 0

        ' For each bp, apply the me bonus and add up
        For i = 1 To NewNumBPs
            ' Set the quantity: required = max(runs,ceil(round(runs * baseQuantity * materialModifier,2))
            NewMatQuantity += CLng(Math.Max(NewRunsperBP, Math.Ceiling(Math.Round(NewRunsperBP * SingleRunQuantity * MEBonus, 2))))
        Next

        Return NewMatQuantity

    End Function

#End Region

    ' Inserts a full shopping list item into the list
    Public Sub InsertShoppingItem(ByVal SentItem As ShoppingListItem, ByVal SentBuildList As BuiltItemList, ByVal SentBuyList As Materials)
        Dim FoundItem As New ShoppingListItem
        Dim TempMats As New Materials
        Dim TempItems As New BuiltItemList
        Dim SearchBuiltItems As New BuiltItemList

        ' Look for the item
        ItemToFind = SentItem
        FoundItem = TotalItemList.Find(AddressOf FindItem)

        If FoundItem IsNot Nothing Then
            ' If it's already in the list, then remove it, and add the sent items to it, then re-add
            TotalItemList.Remove(FoundItem)

            ' Add the new data to this item
            With FoundItem
                ' Increment items
                .Runs = .Runs + SentItem.Runs
                .BuildVolume = .BuildVolume + SentItem.BuildVolume
                .TotalUsage = .TotalUsage + SentItem.TotalUsage
                .TotalItemMarketCost = .TotalItemMarketCost + SentItem.TotalItemMarketCost
                .TotalBuildTime = .TotalBuildTime + SentItem.TotalBuildTime
                .NumBPs = .NumBPs + SentItem.NumBPs ' Need to add the set of numbps used to the current
                .InventionJobs = .InventionJobs + SentItem.InventionJobs

                ' Increment BP Mat List
                If Not IsNothing(SentItem.BPMaterialList) Then
                    TempMats = New Materials
                    TempMats = CType(.BPMaterialList.Clone, Materials)
                    If Not IsNothing(SentItem.BPMaterialList) Then
                        TempMats.InsertMaterialList(SentItem.BPMaterialList.GetMaterialList)
                    End If

                    .BPMaterialList = CType(TempMats.Clone, Materials)
                Else
                    .BPMaterialList = New Materials
                    .BPMaterialList = CType(FoundItem.BPMaterialList.Clone, Materials)
                End If

                ' Increment BP Build List
                If Not IsNothing(SentItem.BPBuiltItems) Then
                    TempItems = New BuiltItemList
                    TempItems = CType(.BPBuiltItems.Clone, BuiltItemList)
                    If Not IsNothing(SentItem.BPBuiltItems) Then
                        For i = 0 To SentItem.BPBuiltItems.GetBuiltItemList.Count - 1
                            TempItems.AddBuiltItem(SentItem.BPBuiltItems.GetBuiltItemList(i))
                        Next
                    End If

                    .BPBuiltItems = CType(TempItems.Clone, BuiltItemList)
                Else
                    .BPBuiltItems = New BuiltItemList
                    .BPBuiltItems = CType(FoundItem.BPBuiltItems.Clone, BuiltItemList)
                End If

                ' Update invention mats
                If Not IsNothing(SentItem.InventionMaterials) Then
                    .InventionMaterials.InsertMaterialList(SentItem.InventionMaterials.GetMaterialList)
                End If

                ' Update copy mats
                If Not IsNothing(SentItem.CopyMaterials) Then
                    .CopyMaterials.InsertMaterialList(SentItem.CopyMaterials.GetMaterialList)
                End If

            End With

            ' Now re-add, not a new item so number of items the same
            TotalItemList.Add(FoundItem)

        Else ' Just add it
            TotalItemList.Add(SentItem)
        End If

        ' Total Buy
        If Not IsNothing(SentBuyList) Then
            If Not IsNothing(SentBuyList.GetMaterialList) Then
                TotalBuyList.InsertMaterialList(SentBuyList.GetMaterialList)
            End If
        End If

        ' Total Build - Need to rebuild every component as if we are only using one bp to get the numbers exact
        If Not IsNothing(SentBuildList) Then
            If Not IsNothing(SentBuildList.GetBuiltItemList) Then
                ' Loop through everything and find all the items to update
                For i = 0 To SentBuildList.GetBuiltItemList.Count - 1
                    Dim FoundBuildItem As New BuiltItem
                    ' Look up current built items
                    Call TotalBuildList.SetItemToFind(CType(SentBuildList.GetBuiltItemList(i).Clone, BuiltItem))
                    FoundBuildItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

                    ' Rebuild with new quantity
                    If FoundBuildItem IsNot Nothing Then
                        ' With this item, update the quantity and rebuild for new materials list
                        With SentBuildList.GetBuiltItemList(i)
                            ' Re-run with new quantity
                            Dim NewRuns As Integer = CInt(Math.Ceiling((FoundBuildItem.ItemQuantity + .ItemQuantity) / FoundBuildItem.PortionSize))


                            Dim TempBP As New Blueprint(.BPTypeID, NewRuns, .BuildME, .BuildTE, 1,
                               UserBPTabSettings.ProductionLines, SelectedCharacter, UserApplicationSettings, False, 0,
                               .ManufacturingFacility, .ManufacturingFacility, .ManufacturingFacility, .ManufacturingFacility, True, UserBPTabSettings.BuildT2T3Materials, True)

                            Dim BFI As BrokerFeeInfo
                            BFI.IncludeFee = CType(UserBPTabSettings.IncludeFees, BrokerFeeType)
                            BFI.FixedRate = UserBPTabSettings.BrokerFeeRate

                            Call TempBP.BuildItems(UserBPTabSettings.IncludeTaxes, bfi, True,
                                                   UserBPTabSettings.IgnoreMinerals, UserBPTabSettings.IgnoreT1Item)

                            Dim InsertBuildItem As New BuiltItem

                            InsertBuildItem.BuildMaterials = TempBP.GetRawMaterials
                            InsertBuildItem.BPTypeID = TempBP.GetTypeID
                            InsertBuildItem.ItemTypeID = TempBP.GetItemID
                            InsertBuildItem.ItemName = UpdateItemNamewithRuns(TempBP.GetItemName, NewRuns)
                            InsertBuildItem.ItemQuantity = FoundBuildItem.ItemQuantity + .ItemQuantity
                            InsertBuildItem.BuildME = TempBP.GetME
                            InsertBuildItem.BuildTE = TempBP.GetTE
                            InsertBuildItem.ItemVolume = TempBP.GetTotalItemVolume
                            InsertBuildItem.BuildMaterials = TempBP.GetRawMaterials
                            InsertBuildItem.ManufacturingFacility = TempBP.GetManufacturingFacility
                            InsertBuildItem.IncludeActivityCost = TempBP.GetManufacturingFacility.IncludeActivityCost
                            InsertBuildItem.IncludeActivityTime = TempBP.GetManufacturingFacility.IncludeActivityTime
                            InsertBuildItem.IncludeActivityUsage = TempBP.GetManufacturingFacility.IncludeActivityUsage
                            InsertBuildItem.PortionSize = TempBP.GetPortionSize

                            ' Remove the old record
                            TotalBuildList.RemoveBuiltItem(FoundBuildItem)
                            ' Now add the updated item
                            TotalBuildList.AddBuiltItem(InsertBuildItem)

                        End With
                    Else
                        ' Just add the new item
                        TotalBuildList.AddBuiltItem(SentBuildList.GetBuiltItemList(i))
                    End If
                Next
            End If
        End If

        ' Invention Materials
        If Not IsNothing(SentItem.InventionMaterials) Then
            TotalInventionMats.InsertMaterialList(SentItem.InventionMaterials.GetMaterialList)
        End If

        ' Copy Materials
        If Not IsNothing(SentItem.CopyMaterials) Then
            TotalCopyMats.InsertMaterialList(SentItem.CopyMaterials.GetMaterialList)
        End If

        ItemToFind = Nothing

    End Sub

    ' Will update all the prices in the shopping list
    Public Sub UpdateListPrices()
        Dim TempBuyList As New Materials ' Buy mats
        Dim TempBuildList As New BuiltItemList ' Build mats (components)
        Dim TempInventionMats As New Materials ' All Invention materials used
        Dim TempCopyMats As New Materials ' All the copy materials used
        Dim TempBPMatList As New Materials ' For the Total Item List

        Dim TransferMaterial As Material
        Dim TransferBuiltItem As BuiltItem

        ' Basically take the materials from the current lists and put them into new lists with 0 prices to get them updated

        ' Item List
        For i = 0 To TotalItemList.Count - 1
            For j = 0 To TotalItemList(i).BPMaterialList.GetMaterialList.Count - 1
                With TotalItemList(i).BPMaterialList.GetMaterialList(j)
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GroupName, .GetQuantity, .GetVolume, 0, .GetItemME, .GetItemTE, .GetBuildItem, .GetItemType)
                End With

                ' Add to the temp list
                Call TempBPMatList.InsertMaterial(TransferMaterial)
            Next

            ' Reset each BPMaterial list on each item
            TotalItemList(i).BPMaterialList = TempBPMatList
        Next

        ' Buy List
        If Not IsNothing(TotalBuyList.GetMaterialList) Then
            For i = 0 To TotalBuyList.GetMaterialList.Count - 1
                With TotalBuyList.GetMaterialList(i)
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GroupName, .GetQuantity, .GetVolume, 0, .GetItemME, .GetItemTE, .GetBuildItem, .GetItemType)
                End With

                ' Add the material with new price to list
                Call TempBuyList.InsertMaterial(TransferMaterial)

            Next
        End If

        ' Reset List
        TotalBuyList = TempBuyList

        ' Invention List
        If Not IsNothing(TotalInventionMats.GetMaterialList) Then
            For i = 0 To TotalInventionMats.GetMaterialList.Count - 1
                With TotalInventionMats.GetMaterialList(i)
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GroupName, .GetQuantity, .GetVolume, 0, .GetItemME, .GetItemTE, .GetBuildItem, .GetItemType)
                End With

                ' Add the material with new price to list
                Call TempInventionMats.InsertMaterial(TransferMaterial)

            Next
        End If

        ' Reset List
        TotalInventionMats = TempInventionMats

        ' Copy List
        If Not IsNothing(TotalCopyMats.GetMaterialList) Then
            For i = 0 To TotalCopyMats.GetMaterialList.Count - 1
                With TotalCopyMats.GetMaterialList(i)
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GroupName, .GetQuantity, .GetVolume, 0, .GetItemME, .GetItemTE, .GetBuildItem, .GetItemType)
                End With

                ' Add the material with new price to list
                Call TempCopyMats.InsertMaterial(TransferMaterial)

            Next
        End If

        ' Reset List
        TotalCopyMats = TempCopyMats

        ' Build Item List
        If TotalBuildList.GetBuiltItemList.Count <> 0 Then
            For i = 0 To TotalBuildList.GetBuiltItemList.Count - 1
                ' Copy the old data and reset the materials
                TransferBuiltItem = New BuiltItem
                TransferBuiltItem = CType(TotalBuildList.GetBuiltItemList(i).Clone, BuiltItem)
                TransferBuiltItem.BuildMaterials = New Materials

                ' Get the new material prices
                If Not IsNothing(TotalBuildList.GetBuiltItemList(i).BuildMaterials.GetMaterialList) Then
                    For j = 0 To TotalBuildList.GetBuiltItemList(i).BuildMaterials.GetMaterialList.Count - 1
                        With TotalBuildList.GetBuiltItemList(i).BuildMaterials.GetMaterialList(j)
                            TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GroupName, .GetQuantity, .GetVolume, 0, .GetItemME, .GetItemTE, .GetBuildItem, .GetItemType)
                        End With
                        ' Insert the mat to the item list
                        Call TransferBuiltItem.BuildMaterials.InsertMaterial(TransferMaterial)
                    Next
                End If

                ' Add to the temp list
                TempBuildList.AddBuiltItem(TransferBuiltItem)
            Next
        End If

        ' Reset the list
        TotalBuildList = TempBuildList

    End Sub

    ' Exports the shoppinglist data in CSV format if true, ignores the price volume if true, and sorts the raw mats by the order given
    Public Function GetClipboardList(ByVal ExportFormat As String, ByVal IgnorePriceVolume As Boolean, ByVal MaterialNamesSortOrder() As String,
                                     ByVal ItemNamesSortOrder() As String, ByVal BuildItemsSortOrder() As String, ByVal IncludeLinks As Boolean) As String
        Dim i As Integer
        Dim OutputText As String = ""
        Dim TempListText As String

        Dim FullBuildList As New Materials
        Dim FullBuyList As New Materials
        Dim FullItemList As New Materials

        Dim TempMatList As New Materials

        Dim InventionMatList As New Materials
        Dim REMatList As New Materials

        Dim IncludeInventionMats As Boolean = False
        Dim IncludeREMats As Boolean = False

        ' Full output lists
        FullBuildList = GetFullBuildMaterialList() ' GetFullBuildList uses BuildItem and Volume for the facility ME value
        FullBuyList = CType(TotalBuyList.Clone, Materials)
        FullItemList = GetFullItemList()

        ' Add the Invention mats to buy
        InventionMatList = GetFullInventionList()
        If Not IsNothing(InventionMatList.GetMaterialList) Then
            IncludeInventionMats = True
            ' Remove the invention materials from the buy list so we can separate them in the output
            Call FullBuyList.RemoveMaterialList(InventionMatList.GetMaterialList)
            ' Update the total though as if the materials were in the full list for price purposes
            FullBuyList.AddTotalValue(InventionMatList.GetTotalMaterialsCost)
            FullBuyList.AddTotalVolume(InventionMatList.GetTotalVolume)
        End If

        ' Sort the Item List by order sent (this is based on how they sorted in the grid)
        ' Item sort order Name, Quantity, ME, Num BPs, Build Type, Decryptor, and Relic
        For i = 0 To ItemNamesSortOrder.Count - 1
            ' Parse the sort order fields
            Dim ItemColumns As String() = ItemNamesSortOrder(i).Split(New [Char]() {"|"c})

            ' For each item, find it in the current buy list and replace
            ' Item sort order Name, Quantity, ME, Num BPs, Build Type, Decryptor/Relic, Location
            For j = 0 To FullItemList.GetMaterialList.Count - 1
                With FullItemList.GetMaterialList(j) ' GroupName stores the build type Decryptor/Relic in item type
                    ' Split out the Build Type, Decryptor, NumBps, and Relic
                    Dim GroupNameItems As String() = .GroupName.Split(New [Char]() {"|"c})
                    Dim ItemName As String = ""
                    Dim RelicName As String = ""

                    If ItemColumns(0).Contains("(") Then
                        ItemName = ItemColumns(0).Substring(0, InStr(ItemColumns(0), "(") - 2)
                        RelicName = ItemColumns(0).Substring(InStr(ItemColumns(0), "("), InStr(ItemColumns(0), ")") - InStr(ItemColumns(0), "(") - 1)
                    Else
                        ItemName = ItemColumns(0)
                    End If

                    If ItemName = .GetMaterialName And CLng(ItemColumns(1)) = .GetQuantity And ItemColumns(2) = .GetItemME _
                     And ItemColumns(4) = GroupNameItems(0) And ItemColumns(5) = GroupNameItems(1) _
                     And ItemColumns(3) = GroupNameItems(2) And RelicName = GroupNameItems(3) And ItemColumns(6) = GroupNameItems(4) Then
                        ' Found it, so insert into temp list
                        TempMatList.InsertMaterial(FullItemList.GetMaterialList(j))
                        Exit For
                    End If
                End With
            Next
        Next

        FullItemList = CType(TempMatList.Clone, Materials)
        TempMatList = New Materials

        ' Get the Shopping list for items
        If Not IsNothing(FullItemList) And ExportFormat <> MultiBuyDataExport Then
            TempListText = FullItemList.GetClipboardList(ExportFormat, IgnorePriceVolume, True, True, UserApplicationSettings.IncludeInGameLinksinCopyText)
            If TempListText <> "No items in List" Then
                OutputText = "Shopping List for: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf
            End If
        End If

        ' Invention materials (If they exist)
        If IncludeInventionMats Then
            ' Add Invention mats if there are any
            TempListText = InventionMatList.GetClipboardList(ExportFormat, False, False, False, UserApplicationSettings.IncludeInGameLinksinCopyText)
            If TempListText <> "No items in List" And ExportFormat <> MultiBuyDataExport Then
                OutputText = OutputText & "Estimated Invention Materials: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf
            ElseIf ExportFormat = MultiBuyDataExport Then
                OutputText = OutputText & TempListText
            End If
        End If

        ' RE Materials (If they exist)
        If IncludeREMats Then
            ' Add RE mats if there are any
            TempListText = REMatList.GetClipboardList(ExportFormat, False, False, False, UserApplicationSettings.IncludeInGameLinksinCopyText)
            If TempListText <> "No items in List" And ExportFormat <> MultiBuyDataExport Then
                OutputText = OutputText & "Estimated RE Materials: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf
            ElseIf ExportFormat = MultiBuyDataExport Then
                OutputText = OutputText & TempListText
            End If
        End If

        ' Sort the Build List by order sent (this is based on how they sorted in the grid)        
        ' Build item sort order - Name, Quantity, and ME
        If ExportFormat <> MultiBuyDataExport Then
            For i = 0 To BuildItemsSortOrder.Count - 1
                ' For each item, find it in the current buy list and replace
                For j = 0 To FullBuildList.GetMaterialList.Count - 1
                    ' Parse the sort order fields
                    Dim ItemColumns As String() = BuildItemsSortOrder(i).Split(New [Char]() {"|"c})

                    With FullBuildList.GetMaterialList(j) ' Mat group stores the build type and meta is in item type
                        If ItemColumns(0) = .GetMaterialName And CLng(ItemColumns(1)) = .GetQuantity And ItemColumns(2) = .GetItemME Then
                            ' Found it, so insert into temp list
                            TempMatList.InsertMaterial(FullBuildList.GetMaterialList(j))
                            Exit For
                        End If
                    End With
                Next
            Next

            FullBuildList = CType(TempMatList.Clone, Materials)
            TempMatList = New Materials

            ' Output the Build List - list the ME for each - assume no decryptor or relic
            If Not IsNothing(FullBuildList) Then
                TempListText = FullBuildList.GetClipboardList(ExportFormat, True, True, False, UserApplicationSettings.IncludeInGameLinksinCopyText)
                If TempListText <> "No items in List" And ExportFormat <> MultiBuyDataExport Then
                    OutputText = OutputText & "Build Items List: " & vbCrLf
                    OutputText = OutputText & TempListText
                    ' Spacer
                    OutputText = OutputText & vbCrLf
                ElseIf ExportFormat = MultiBuyDataExport Then
                    OutputText = OutputText & TempListText
                End If
            End If
        End If

        ' Now sort the buy material list by the order sent in the grid
        ' Material sort order - Just Name
        For i = 0 To MaterialNamesSortOrder.Count - 1
            ' For each item, find it in the current buy list and replace
            For j = 0 To FullBuyList.GetMaterialList.Count - 1
                If MaterialNamesSortOrder(i) = FullBuyList.GetMaterialList(j).GetMaterialName Then
                    ' Found it, so insert into temp list
                    TempMatList.InsertMaterial(FullBuyList.GetMaterialList(j))
                    Exit For
                End If
            Next
        Next

        FullBuyList = CType(TempMatList.Clone, Materials)
        TempMatList = New Materials

        ' Output the Buy list, add the price and volume to it - in Buy lists don't list ME
        If Not IsNothing(FullBuyList) Then
            TempListText = FullBuyList.GetClipboardList(ExportFormat, False, False, False, UserApplicationSettings.IncludeInGameLinksinCopyText)
            If TempListText <> "No materials in List" And ExportFormat <> MultiBuyDataExport Then
                OutputText = OutputText & "Buy Materials List: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf
            ElseIf ExportFormat = MultiBuyDataExport Then
                OutputText = OutputText & TempListText
            End If
        End If

        ' Add total build volume to end - Make sure we get quantity and not runs, so use portion size
        If ExportFormat = CSVDataExport Then
            OutputText = OutputText & "Total Volume of Built Item(s):," & CStr(FullItemList.GetTotalVolume) & ",m3"
        ElseIf ExportFormat = SSVDataExport Then
            OutputText = OutputText & "Total Volume of Built Item(s):;" & CStr(FullItemList.GetTotalVolume) & ";m3"
        ElseIf ExportFormat = DefaultTextDataExport Then
            OutputText = OutputText & "Total Volume of Built Item(s): " & FormatNumber(FullItemList.GetTotalVolume, 2) & " m3"
        End If

        Return OutputText

    End Function

    ' Returns the total number of items in the list
    Public Function GetNumShoppingItems() As Long
        Return TotalItemList.Count
    End Function

    ' Returns the full list of build items as materials
    Public Function GetFullBuildMaterialList() As Materials
        Dim ReturnBuildItems As New Materials
        Dim TempMat As Material
        Dim TempBuiltItem As BuiltItem

        ' Go through all the built items and insert the materials
        For j = 0 To TotalBuildList.GetBuiltItemList.Count - 1
            If Not IsNothing(TotalBuildList.GetBuiltItemList(j)) Then
                TempBuiltItem = TotalBuildList.GetBuiltItemList(j)
                ' Use Volume for the facility ME value, since this isn't used (also ignore total volume)
                TempMat = New Material(TempBuiltItem.ItemTypeID, TempBuiltItem.ItemName, "Built Item", TempBuiltItem.ItemQuantity,
                                       TempBuiltItem.ManufacturingFacility.MaterialMultiplier, 0, CStr(TempBuiltItem.BuildME), CStr(TempBuiltItem.BuildTE))

                Call ReturnBuildItems.InsertMaterial(TempMat)

            End If
        Next

        ' Sort
        Call ReturnBuildItems.SortMaterialListByQuantity()

        Return ReturnBuildItems

    End Function

    Public Function GetFullBuildList() As BuiltItemList
        ' Sort it first - quantity descending
        TotalBuildList.GetBuiltItemList.Sort(Function(x, y) y.ItemQuantity.CompareTo(x.ItemQuantity))

        Return TotalBuildList

    End Function

    ' Returns the full built item list as a built item
    Public Function GetFullBuiltItemList() As BuiltItemList
        Return TotalBuildList
    End Function

    ' Returns the list of buy materials
    Public Function GetFullBuyList() As Materials

        If Not IsNothing(TotalBuyList.GetMaterialList) Then
            ' Sort
            Call TotalBuyList.SortMaterialListByQuantity()
        End If

        Return TotalBuyList

    End Function

    ' Returns the list of Invention items and quantity
    Public Function GetFullInventionList() As Materials
        Dim TempInventionMats As New Materials

        For i = 0 To TotalItemList.Count - 1
            If TotalItemList(i).TechLevel <> 1 And Not IsNothing(TotalItemList(i).InventionMaterials) Then
                TempInventionMats.InsertMaterialList(TotalItemList(i).InventionMaterials.GetMaterialList)
            End If
        Next

        Return TempInventionMats

    End Function

    ' Returns the list of Invention items and quantity
    Public Function GetFullCopyList() As Materials
        Dim TempCopyMats As New Materials

        For i = 0 To TotalItemList.Count - 1
            If TotalItemList(i).TechLevel = 2 And Not IsNothing(TotalItemList(i).CopyMaterials) Then
                TempCopyMats.InsertMaterialList(TotalItemList(i).CopyMaterials.GetMaterialList)
            End If
        Next

        Return TempCopyMats

    End Function

    ' Returns the full list of Items we want to build in the shopping list
    Public Function GetFullItemList() As Materials
        Dim TempMat As Material
        Dim ReturnMaterials As New Materials

        For i = 0 To TotalItemList.Count - 1
            With TotalItemList(i)
                ' Item sort order is Build Type, Decryptor, NumBps, and Relic for the group name
                TempMat = New Material(.TypeID, .Name, .BuildType & "|" & .Decryptor & "|" & CStr(.NumBPs) & "|" & CStr(.Relic) & "|" _
                                       & .ManufacturingFacility.FacilityName, .Runs, (.BuildVolume / .Runs) / .PortionSize, 0, CStr(.ItemME), CStr(.ItemTE))
            End With
            ReturnMaterials.InsertMaterial(TempMat)
        Next

        Return ReturnMaterials

    End Function

    ' Returns the full shopping list as a list of shopping list items
    Public Function GetFullShoppingList() As List(Of ShoppingListItem)
        Return TotalItemList
    End Function

    ' Sets the additional costs for this list
    Public Sub SetAdditionalCosts(AddlCosts As Double)
        AdditionalCosts = AddlCosts
    End Sub

    ' Sets all the price data for the shopping list after updates
    Public Sub SetPriceData(BrokerFeeData As BrokerFeeInfo, IncludeUsage As Boolean, ItemBuyTypeList As List(Of ItemBuyType))

        ' First, Total up all the material costs, build time and market prices from the items we have and then add to total costs
        TotalListBuildTime = 0
        TotalListMarketPrice = 0
        For i = 0 To TotalShoppingList.TotalItemList.Count - 1
            TotalListBuildTime += TotalShoppingList.TotalItemList(i).TotalBuildTime
            TotalListMarketPrice += TotalShoppingList.TotalItemList(i).TotalItemMarketCost
        Next

        ' Use the master lists for these
        TotalListInventionCost = TotalShoppingList.TotalInventionMats.GetTotalMaterialsCost
        TotalListCopyCost = TotalShoppingList.TotalCopyMats.GetTotalMaterialsCost

        ' The only fee that applies when shopping is either a buy order or directly buying - Broker fees are all that apply during a buy order
        MaterialsBrokerFee = 0 ' Reset
        MaterialsBrokerFee = CalculateBrokersFees(ItemBuyTypeList, BrokerFeeData)

        ' Total usage
        TotalListUsage = 0 ' Reset
        If IncludeUsage Then
            ' Read through all the items in the list and sum up each usage value
            For i = 0 To TotalShoppingList.TotalItemList.Count - 1
                TotalListUsage += TotalShoppingList.TotalItemList(i).TotalUsage
            Next
        End If

        ' Set the total cost of materials (includes copy and invention mats) plus usage, the broker fee for the materials, and additional costs
        TotalListCost = TotalBuyList.GetTotalMaterialsCost + MaterialsBrokerFee + TotalListUsage + AdditionalCosts

    End Sub

    ' Gets the broker fees based on user options that determine if each item is bought from market or through orders
    Private Function CalculateBrokersFees(ItemList As List(Of ItemBuyType), BrokerData As BrokerFeeInfo) As Double
        Dim TotalBrokersFee As Double = 0

        ' Loop through the buy list and check the item list to see if we apply brokers fees or not
        If Not IsNothing(TotalBuyList.GetMaterialList) And Not IsNothing(ItemList) Then
            For i = 0 To TotalBuyList.GetMaterialList.Count - 1
                For j = 0 To ItemList.Count - 1
                    If TotalBuyList.GetMaterialList(i).GetMaterialName = ItemList(j).ItemName Then
                        If ItemList(j).BuyType = "Buy Order" Then
                            ' Apply broker fee
                            TotalBrokersFee += GetSalesBrokerFee(TotalBuyList.GetMaterialList(i).GetTotalCost, BrokerData)
                        End If
                    End If
                Next
            Next
        End If

        Return TotalBrokersFee

    End Function

    ' Returns the total profit of the items made with raw mats in the list
    Public Function GetTotalProfit() As Double
        Return TotalListMarketPrice - TotalListCost
    End Function

    ' Returns the total IPH (approx) for the items in the list
    Public Function GetTotalIPH() As Double
        Return GetTotalProfit() / TotalListBuildTime * 3600 ' Isk per second then multiply it by seconds per hour for IPH
    End Function

    ' Returns the total fees to set up sell orders for the total value of buy materials
    Public Function GetTotalMaterialsBrokersFees() As Double
        Return MaterialsBrokerFee
    End Function

    ' Returns the total cost of the list
    Public Function GetTotalCost() As Double
        Return TotalListCost
    End Function

    ' Returns the total invention cost of materials
    Public Function GetTotalInventionCosts() As Double
        Return TotalListInventionCost
    End Function

    ' Returns the total copy cost of materials
    Public Function GetTotalCopyCosts() As Double
        Return TotalListCopyCost
    End Function

    ' Returns the total volume of the buy items
    Public Function GetTotalVolume() As Double
        Return TotalBuyList.GetTotalVolume
    End Function

    ' Returns the total volume of built items in the shopping list
    Public Function GetBuiltItemVolume() As Double
        Dim TotalVolume As Double = 0

        For i = 0 To TotalItemList.Count - 1
            TotalVolume += TotalItemList(i).BuildVolume
        Next

        Return TotalVolume

    End Function

    ' Returns the total usage of the shopping list items
    Public Function GetTotalUsage() As Double
        Return TotalListUsage
    End Function

    ' Predicate for finding the item in full list
    Public Function FindItem(ByVal Item As ShoppingListItem) As Boolean
        If ShopListItemsEqual(Item, ItemToFind) Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Predicate to compare two shopping list items
    Private Function ShopListItemsEqual(Item1 As ShoppingListItem, Item2 As ShoppingListItem) As Boolean
        If Item1.ItemME <> Item2.ItemME Then
            Return False
        End If
        If Item1.Name <> Item2.Name Then
            Return False
        End If
        If Item1.BuildType <> Item2.BuildType Then
            Return False
        End If
        If Item1.Decryptor <> Item2.Decryptor Then
            Return False
        End If
        If Item1.Relic <> Item2.Relic Then
            Return False
        End If
        'If Item1.NumBPs <> Item2.NumBPs Then
        '    Return False
        'End If
        If Item1.ManufacturingFacility.FacilityName <> Item2.ManufacturingFacility.FacilityName Then
            Return False
        End If

        Return True

    End Function

    ' Looks up a material and returns the quantity of the users entry (on hand) or zero if not found
    Private Function GetOnHandMaterialQuantity(ByVal MaterialName As String) As Long

        If Not IsNothing(OnHandMatList.GetMaterialList) Then
            For i = 0 To OnHandMatList.GetMaterialList.Count - 1
                If OnHandMatList.GetMaterialList(0).GetMaterialName = MaterialName Then
                    Return OnHandMatList.GetMaterialList(0).GetQuantity
                End If
            Next
        End If
        ' not found
        Return 0

    End Function

    ' Looks up a component and returns the quantity of the user's entry (on hand) or zero if not found
    Private Function GetOnHandComponentQuantity(ByVal ComponentName As String) As Long

        If Not IsNothing(OnHandComponentList.GetMaterialList) Then
            For i = 0 To OnHandComponentList.GetMaterialList.Count - 1
                If OnHandComponentList.GetMaterialList(0).GetMaterialName = ComponentName Then
                    Return OnHandComponentList.GetMaterialList(0).GetQuantity
                End If
            Next
        End If
        ' not found
        Return 0

    End Function

    ' For doing a deep copy of a shopping list
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New ShoppingList
        Dim BuyList As New Materials

        With Me
            CopyOfMe.TotalItemList = .TotalItemList
            CopyOfMe.TotalBuyList = CType(.TotalBuyList.Clone, Materials)
            CopyOfMe.TotalBuildList = CType(.TotalBuildList.Clone, BuiltItemList)
            CopyOfMe.TotalInventionMats = CType(.TotalInventionMats, Materials)
            CopyOfMe.TotalCopyMats = CType(.TotalCopyMats.Clone, Materials)
            CopyOfMe.OnHandMatList = CType(.OnHandMatList.Clone, Materials)
            CopyOfMe.OnHandComponentList = CType(.OnHandComponentList.Clone, Materials)
            CopyOfMe.AdditionalCosts = .AdditionalCosts
            CopyOfMe.MaterialsBrokerFee = .MaterialsBrokerFee
        End With

        Return CopyOfMe

    End Function

End Class

Public Class ShoppingListItem
    Public BlueprintTypeID As Long ' BP Type ID
    Public TypeID As Long ' TypeID for the item 
    Public Runs As Long ' Number of runs of the main items (not quantity)
    Public Name As String ' Item we want to shop for * Key Value
    Public BuildType As String ' Component / Raw / Build/Buy * Key Value
    Public ItemME As Double ' The ME of the Shopping item * Key Value
    Public ItemTE As Double
    Public TechLevel As Integer ' T1, T2, or T3
    Public BuildVolume As Double ' Volume of the built item
    Public PortionSize As Long ' Portion size of one run of this blueprint

    Public Decryptor As String ' If it's invented or RE'd, then store the Relic or Decryptor name here * Key value
    Public Relic As String ' Relic used for T3

    Public InventionMaterials As New Materials ' The List of Invention materials needed to build the T2 item
    Public CopyMaterials As New Materials ' List of materials used to make the copies to invent
    Public AvgInvRunsforSuccess As Double ' How many Invention/RE runs we need for success - used to calculate correct changes in list for mats
    Public InventionJobs As Long ' How many jobs did we do
    Public InventedRunsPerBP As Integer ' Number of runs for each bp in NumBPs (helps with determining invention changes later)

    Public NumBPs As Integer ' Number of BPs used to build item

    Public BPMaterialList As New Materials ' This is the list of items on the Blueprint so we have a record of what they are building - it is not updated
    Public BPBuiltItems As New BuiltItemList ' List of items we build for this bp - not updated

    ' Set the component facility (use BP tab for now)
    Public ManufacturingFacility As IndustryFacility
    Public ComponentManufacturingFacility As IndustryFacility
    Public ReactionFacility As IndustryFacility

    ' Ignore Variables
    Public IgnoredInvention As Boolean
    Public IgnoredMinerals As Boolean
    Public IgnoredT1BaseItem As Boolean

    Public IncludeActivityCost As Boolean
    Public IncludeActivityTime As Boolean
    Public IncludeActivityUsage As Boolean

    ' Saved price variables for this item - can be updated when quantity updated
    ' Public TotalMaterialCost As Double ' This is the cost of all the materials to build the item (does not include usage, item taxes or item fees) and invention and copy costs
    Public TotalUsage As Double ' Includes Manufacturing, Components, Invention, and Copying usage
    Public TotalItemMarketCost As Double ' This is the market cost of the items in the list
    Public TotalBuildTime As Double ' Total time to build the items for the given type of building

    Public Sub New()

        BlueprintTypeID = 0
        TypeID = 0
        Runs = 0
        Name = ""
        BuildType = ""
        ItemME = 0
        ItemTE = 0
        TechLevel = 0
        BuildVolume = 0
        PortionSize = 0

        Decryptor = ""
        Relic = ""

        InventionMaterials = New Materials
        CopyMaterials = New Materials
        AvgInvRunsforSuccess = 0
        InventedRunsPerBP = 0
        InventionJobs = 0

        NumBPs = 0

        BPMaterialList = Nothing
        BPBuiltItems = Nothing

        ManufacturingFacility = New IndustryFacility
        ComponentManufacturingFacility = New IndustryFacility
        ReactionFacility = New IndustryFacility

        IgnoredInvention = False
        IgnoredMinerals = False
        IgnoredT1BaseItem = False

        IncludeActivityCost = False
        IncludeActivityTime = False
        IncludeActivityUsage = False

    End Sub

End Class

Public Class BuiltItemList
    Implements ICloneable

    Private ItemList As List(Of BuiltItem) ' List of all the items and materials
    Private TotalCost As Double ' Total cost of the list

    Private ItemToFind As BuiltItem

    Public Sub New()
        ItemList = New List(Of BuiltItem)
        ItemToFind = Nothing
        TotalCost = 0
    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New BuiltItemList
        Dim TempItem As BuiltItem

        For i = 0 To ItemList.Count - 1
            TempItem = CType(ItemList(i).Clone, BuiltItem)
            CopyOfMe.AddBuiltItem(TempItem)
        Next

        CopyOfMe.TotalCost = Me.TotalCost

        Return CopyOfMe

    End Function

    ' Adds a sent built item to the main list, updating quantities for same items
    Public Sub AddBuiltItem(ByVal SentItem As BuiltItem)
        Dim FoundItem As New BuiltItem
        Dim ComponentFoundItem As New BuiltItem
        Dim CombinedMaterials As New Materials
        Dim AddItem As BuiltItem = CType(SentItem.Clone, BuiltItem)

        ' Search the list to see if the item exists
        ItemToFind = AddItem
        FoundItem = ItemList.Find(AddressOf FindBuiltItem)

        If FoundItem IsNot Nothing Then
            ' Exists, so update the quantity and materials
            ' Remove the item from the list we are updating
            Call ItemList.Remove(FoundItem)

            ' Get the runs of this blueprint to set the right amount of build materials needed
            Dim Runs As Long = CLng(Math.Ceiling((FoundItem.ItemQuantity + AddItem.ItemQuantity) / AddItem.PortionSize))

            ' Combine the materials
            CombinedMaterials.InsertMaterialList(AddItem.BuildMaterials.GetMaterialList)
            CombinedMaterials.InsertMaterialList(FoundItem.BuildMaterials.GetMaterialList)

            AddItem.BuildMaterials = CType(CombinedMaterials.Clone, Materials)

            Dim BuiltComponentList As New List(Of BuiltItem)

            ' Combine the built item lists by updating the quantity and saving
            For Each AddItemBI In AddItem.ComponentBuildList
                ' Find the item in the current list, if found, update the built item quantity, else, add it
                ItemToFind = AddItemBI
                ComponentFoundItem = FoundItem.ComponentBuildList.Find(AddressOf FindBuiltItem)
                If ComponentFoundItem IsNot Nothing Then
                    Dim TempComponent As New BuiltItem
                    TempComponent = CType(ComponentFoundItem.Clone, BuiltItem)
                    TempComponent.ItemQuantity = ComponentFoundItem.ItemQuantity + AddItemBI.ItemQuantity
                    ' Add updated quantity
                    BuiltComponentList.Add(TempComponent)
                Else
                    ' not found, add it
                    BuiltComponentList.Add(AddItemBI)
                End If
            Next

            ' Add all the combined components
            AddItem.ComponentBuildList = BuiltComponentList

            ' Update the quantities
            AddItem.ItemQuantity = AddItem.ItemQuantity + FoundItem.ItemQuantity
            AddItem.UsedQuantity = AddItem.UsedQuantity + FoundItem.UsedQuantity
            AddItem.BPRuns = Runs
            AddItem.PortionSize = AddItem.PortionSize

            AddItem.ItemName = UpdateItemNamewithRuns(AddItem.ItemName, Runs)

            ' Cost
            TotalCost = TotalCost + AddItem.TotalBuildCost

            ' Re-add the item
            Call ItemList.Add(AddItem)

        Else
            ' Add the item to the list
            Call ItemList.Add(AddItem)
        End If

    End Sub

    ' Removes an item from the list in the quantity sent
    Public Sub RemoveBuiltItem(ByVal RemoveItem As BuiltItem)
        Dim FoundItem As New BuiltItem
        Dim CombinedMaterials As New Materials

        ' Search the list to see if the item exists
        ItemToFind = RemoveItem
        FoundItem = ItemList.Find(AddressOf FindBuiltItem)

        If FoundItem IsNot Nothing Then
            If FoundItem.ItemQuantity = RemoveItem.ItemQuantity Then
                ' Just remove it
                Call ItemList.Remove(RemoveItem)
            Else
                ' Remove the found item and update
                Call ItemList.Remove(FoundItem)

                ' Update quantity and materials
                FoundItem.ItemQuantity = FoundItem.ItemQuantity - RemoveItem.ItemQuantity
                FoundItem.BuildMaterials.RemoveMaterialList(RemoveItem.BuildMaterials.GetMaterialList)

                ' Add the updated item to the list
                ItemList.Add(FoundItem)

            End If

            ' Update cost
            TotalCost = TotalCost - RemoveItem.TotalBuildCost

        End If

    End Sub

    ' Returns the list of all built items only
    Public Function GetBuiltItemList() As List(Of BuiltItem)
        Return ItemList
    End Function

    ' Returns the total cost of the list
    Public Function GetTotalCost() As Double
        Return TotalCost
    End Function

    ' So outside functions can use the find predecate
    Public Sub SetItemToFind(FindItem As BuiltItem)
        ItemToFind = FindItem
    End Sub

    ' Predicate for finding a component item in the list
    Public Function FindBuiltItem(ByVal Item As BuiltItem) As Boolean
        If Item.ItemTypeID = ItemToFind.ItemTypeID And Item.BuildME = ItemToFind.BuildME And Item.ManufacturingFacility.FacilityName = ItemToFind.ManufacturingFacility.FacilityName Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Function takes an item name and returns an array of all the items with that same name (could have different MEs)
    Public Function FindBuiltItems(ByVal ItemName As String) As BuiltItemList
        Dim ReturnList As New BuiltItemList

        For i = 0 To ItemList.Count - 1
            If RemoveItemNameRuns(ItemList(i).ItemName) = ItemName Then
                Call ReturnList.AddBuiltItem(ItemList(i))
            End If
        Next

        Return ReturnList

    End Function

End Class

Public Class BuiltItem
    Implements ICloneable

    Public BPTypeID As Long
    Public ItemTypeID As Long
    Public ItemName As String
    Public ItemQuantity As Long
    Public UsedQuantity As Decimal ' For stuff with portion sizes
    Public ItemVolume As Double
    Public BuildME As Integer
    Public BuildTE As Integer
    Public BuildMaterials As Materials
    Public ComponentBuildList As List(Of BuiltItem) ' This item may also have build able items
    Public BPRuns As Long ' How many runs of this item to get the item quantity
    Public PortionSize As Long

    ' These fields are for shopping list update functions
    Public ManufacturingFacility As New IndustryFacility

    Public IncludeActivityCost As Boolean
    Public IncludeActivityTime As Boolean
    Public IncludeActivityUsage As Boolean

    Public TotalBuildCost As Double
    Public TotalExcessSellBuildCost As Double

    Public Sub New()
        ItemTypeID = 0
        ItemName = ""
        ItemQuantity = 0
        UsedQuantity = 0
        ItemVolume = 0
        BuildME = 0
        BuildMaterials = New Materials
        ComponentBuildList = New List(Of BuiltItem)
        BPRuns = 0
        PortionSize = 0

        ManufacturingFacility = New IndustryFacility

        IncludeActivityCost = False
        IncludeActivityTime = False
        IncludeActivityUsage = False

        TotalBuildCost = 0
        TotalExcessSellBuildCost = 0

    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe As New BuiltItem
        CopyOfMe.BPTypeID = Me.BPTypeID
        CopyOfMe.ItemTypeID = Me.ItemTypeID
        CopyOfMe.ItemName = Me.ItemName
        CopyOfMe.ItemQuantity = Me.ItemQuantity
        CopyOfMe.UsedQuantity = Me.UsedQuantity
        CopyOfMe.ItemVolume = Me.ItemVolume
        CopyOfMe.BuildME = Me.BuildME
        CopyOfMe.BuildTE = Me.BuildTE
        CopyOfMe.BuildMaterials = CType(Me.BuildMaterials.Clone, Materials)
        CopyOfMe.BPRuns = Me.BPRuns
        CopyOfMe.PortionSize = Me.PortionSize
        CopyOfMe.ManufacturingFacility = CType(Me.ManufacturingFacility.Clone, IndustryFacility)
        CopyOfMe.IncludeActivityUsage = Me.IncludeActivityUsage
        CopyOfMe.IncludeActivityTime = Me.IncludeActivityTime
        CopyOfMe.IncludeActivityCost = Me.IncludeActivityCost
        CopyOfMe.TotalBuildCost = Me.TotalBuildCost
        CopyOfMe.TotalExcessSellBuildCost = Me.TotalExcessSellBuildCost
        CopyOfMe.ComponentBuildList = New List(Of BuiltItem)
        ' Clone each built item in the list
        For Each BI In Me.ComponentBuildList
            CopyOfMe.ComponentBuildList.Add(CType(BI.Clone, BuiltItem))
        Next
        Return CopyOfMe
    End Function

End Class