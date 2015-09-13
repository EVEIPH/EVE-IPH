
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
        Dim UpdatedQuantity As Long

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
            If Not IsNothing(FoundItem.BPMaterialList.GetMaterialList) Then
                With FoundItem.BPMaterialList
                    For i = 0 To .GetMaterialList.Count - 1
                        ' Are we Building or Buying?
                        If .GetMaterialList(i).GetBuildItem = True Then ' Building
                            ' Make sure the item exists (might have been deleted already in the main list) before updating

                            ' Find the built item in the build list for this item - only need name and ME to look up
                            TempBuiltItem.ItemTypeID = .GetMaterialList(i).GetMaterialTypeID
                            TempBuiltItem.ItemName = .GetMaterialList(i).GetMaterialName
                            TempBuiltItem.BuildME = CInt(.GetMaterialList(i).GetItemME)

                            Call TotalBuildList.SetItemToFind(TempBuiltItem)
                            FoundBuildItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

                            If FoundBuildItem IsNot Nothing Then
                                ' Copy current built item info
                                With FoundBuildItem
                                    TempBuiltItem = New BuiltItem
                                    TempBuiltItem.BPTypeID = .BPTypeID
                                    TempBuiltItem.ItemTypeID = .ItemTypeID
                                    TempBuiltItem.ItemName = .ItemName
                                    TempBuiltItem.ItemQuantity = .ItemQuantity
                                    TempBuiltItem.ItemVolume = .ItemVolume
                                    TempBuiltItem.FacilityMEModifier = .FacilityMEModifier

                                    TempBuiltItem.BuildME = .BuildME
                                    TempBuiltItem.BuildTE = .BuildTE
                                    ' If we are building a component, then we are buying all the mats for it so only use the buy list for mats to update
                                    TempBuiltItem.BuildMaterials.InsertMaterialList(.BuildMaterials.GetMaterialList)
                                End With

                                UpdatedQuantity = GetUpdatedQuantity("Build", FoundItem, UpdateItemQuantity, .GetMaterialList(i))

                                ' Need to update to the quantity sent in the Build List
                                Call UpdateShoppingBuiltItemQuantity(TempBuiltItem, UpdatedQuantity)
                            End If
                        Else ' Buying
                            ' Make sure the item exists (might have been deleted already in the main list) before updating
                            If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then
                                UpdatedQuantity = GetUpdatedQuantity("Buy", FoundItem, UpdateItemQuantity, .GetMaterialList(i))
                                ' Need to update to the quantity sent in the Buy list
                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedQuantity)
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
                                UpdatedQuantity = GetUpdatedQuantity("Invention", FoundItem, UpdateItemQuantity, .GetMaterialList(i))

                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedQuantity)
                                ' Update this material in the item's invention list for copy/paste function
                                If UpdatedQuantity > 0 Then
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
                                UpdatedQuantity = GetUpdatedQuantity("Copying", FoundItem, UpdateItemQuantity, .GetMaterialList(i))

                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedQuantity)
                                ' Update this material in the item's invention list for copy/paste function
                                If UpdatedQuantity <= 0 Then
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
                FoundItem.BuildVolume = FoundItem.BuildVolume / FoundItem.Quantity * UpdateItemQuantity
                'FoundItem.TotalMaterialCost = FoundItem.TotalMaterialCost / FoundItem.Quantity * UpdateItemQuantity
                FoundItem.TotalUsage = FoundItem.TotalUsage / FoundItem.Quantity * UpdateItemQuantity
                FoundItem.TotalItemMarketCost = FoundItem.TotalItemMarketCost / FoundItem.Quantity * UpdateItemQuantity
                FoundItem.TotalBuildTime = FoundItem.TotalBuildTime / FoundItem.Quantity * UpdateItemQuantity
                ' Update the invention jobs if they update this later
                If FoundItem.InventionJobs <> 0 Then
                    FoundItem.InventionJobs = CInt(Math.Ceiling(FoundItem.AvgInvRunsforSuccess * Math.Ceiling(UpdateItemQuantity / FoundItem.InventedRunsPerBP)))
                    ' How many bps do we need to make?
                    FoundItem.NumBPs = CInt(Math.Ceiling(UpdateItemQuantity / FoundItem.InventedRunsPerBP))
                End If
                ' Finally update the quantity
                FoundItem.Quantity = UpdateItemQuantity
            End If
        End If

    End Sub

    ' Removes or updates a built item quantity from the build list and its materials from the material list - i.e. remove particle accelerator and raw mats from item - Hammerhead II
    Public Sub UpdateShoppingBuiltItemQuantity(ByVal SentItem As BuiltItem, ByVal UpdateItemQuantity As Long)
        Dim FoundItem As BuiltItem
        Dim UpdatedQuantity As Long ' This is the final mat quantity for updating the shopping buy/build list ammount
        Dim RefMatQuantity As Long ' This is the reference for materials we send in the update quantity - will be the mat quantity for that built item

        Dim UpdateItem As New BuiltItem
        Dim UpdateItemMatList As New Materials
        Dim InsertMat As Material

        Dim ShoppingItem As New ShoppingListItem

        Application.DoEvents()

        ' Task here: Update build list with correct quantity, and then the buy list with the correct material quantities

        ' First look up the item and the mats used to build it in the saved list
        Call TotalBuildList.SetItemToFind(SentItem)
        FoundItem = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem)

        ' Update the materials in the built list, take total number, divide mats by it and then multiply by quantity sent
        If Not IsNothing(FoundItem.BuildMaterials.GetMaterialList) Then
            With FoundItem.BuildMaterials
                For i = 0 To .GetMaterialList.Count - 1
                    ' Make sure the item exists (might have been deleted already in the main list) before updating
                    If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then

                        ' Set the values we need for get updated quantity
                        ShoppingItem.Name = FoundItem.ItemName
                        ShoppingItem.TypeID = FoundItem.ItemTypeID
                        ShoppingItem.FacilityMEModifier = FoundItem.FacilityMEModifier
                        ShoppingItem.BuildLocation = ""
                        ShoppingItem.ItemME = FoundItem.BuildME
                        ShoppingItem.Quantity = FoundItem.ItemQuantity

                        ' Blank these out for now if we use them later
                        ShoppingItem.InventionJobs = 0
                        ShoppingItem.InventedRunsPerBP = 0
                        ShoppingItem.AvgInvRunsforSuccess = 0
                        ShoppingItem.NumBPs = 1 ' Built items (components) are always one bp for now

                        ' Get the new quantity for each material to build this item
                        UpdatedQuantity = GetUpdatedQuantity("Buy", ShoppingItem, UpdateItemQuantity, .GetMaterialList(i), RefMatQuantity)

                        ' Need to update to the quantity sent in the Buy List
                        Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedQuantity)

                        ' Save the updated materials for the build list, if they want to update later
                        With .GetMaterialList(i)
                            ' Need to save the new value we want for this item, not the new quantity to update the other lists with
                            InsertMat = New Material(.GetMaterialTypeID, .GetMaterialName, .GetMaterialGroup, RefMatQuantity, .GetTotalVolume, 0, "")
                        End With

                        If UpdatedQuantity > 0 Then
                            UpdateItemMatList.InsertMaterial(InsertMat)
                        End If

                    End If
                Next
            End With

            ' Save the base data
            UpdateItem.ItemTypeID = FoundItem.ItemTypeID
            UpdateItem.BuildME = FoundItem.BuildME
            UpdateItem.ItemName = FoundItem.ItemName
            UpdateItem.ItemVolume = FoundItem.ItemVolume
            UpdateItem.FacilityMEModifier = FoundItem.FacilityMEModifier

            ' Update the new built list quantity
            UpdateItem.ItemQuantity = UpdateItemQuantity
            ' Update the new built list material list, with updated quantities
            UpdateItem.BuildMaterials = UpdateItemMatList

            ' Update the quantity of the build list item
            Call TotalBuildList.RemoveBuiltItem(FoundItem) ' Remove the old one

            If UpdateItemQuantity <> 0 Then
                Call TotalBuildList.AddBuiltItem(UpdateItem) ' Add the updated one
            End If

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

    ' Calculates the updated quantity for updating lists
    ' ProcessingType = Invention/RE/Build/Buy
    ' CurrentItem = the current item in the shopping list for reference of old values
    ' NewItemQuantity = new quantity of the current item we want to update
    ' UpdateItemMaterial = material of the item we are updating the quantity of based on the new item quantity
    Private Function GetUpdatedQuantity(ByVal ProcessingType As String, _
                                        ByVal CurrentItem As ShoppingListItem,
                                        ByVal NewItemQuantity As Long, _
                                        ByVal UpdateItemMaterial As Material, _
                                        Optional ByRef RefMatQuantity As Long = 0) As Long
        Dim UpdatedQuantity As Long = 0
        Dim OnHandMats As Long
        Dim ListMatQuantity As Long
        Dim NumInventionJobs As Integer
        Dim CurrentMatQuantity As Long = 0
        Dim NewMatQuantity As Long = 0

        ' Set up the ME bonus and then calculate the new material quantity
        Dim MEBonus As Double = 0
        Dim SingleRunQuantity As Long = 0
        Dim rsMatQuantity As SQLiteDataReader
        Dim SQL As String

        If NewItemQuantity = 0 Then
            Return 0
        End If

        ' Look up the cost for the material
        If ProcessingType = "Invention" Or ProcessingType = "Copying" Then
            SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS WHERE PRODUCT_ID = " & CurrentItem.BlueprintTypeID & " AND MATERIAL_ID = " & UpdateItemMaterial.GetMaterialTypeID
            SQL = SQL & " AND ACTIVITY = 8"
        Else
            SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS WHERE PRODUCT_ID = " & CurrentItem.TypeID & " AND MATERIAL_ID = " & UpdateItemMaterial.GetMaterialTypeID
            SQL = SQL & " AND ACTIVITY = 1"
        End If

        DBCommand = New SQLiteCommand(SQL, DB)
        rsMatQuantity = DBCommand.ExecuteReader
        rsMatQuantity.Read()

        SingleRunQuantity = rsMatQuantity.GetInt64(0)

        ' Calc out final mat quantity
        If ProcessingType = "Invention" Or ProcessingType = "Copying" Then

            ListMatQuantity = TotalBuyList.SearchListbyName(UpdateItemMaterial.GetMaterialName).GetQuantity

            ' For invention materials, find out how many mats we need by calcuating the new value from the item runs and invention jobs per item
            If NewItemQuantity <= 0 Then
                ' Easy case, just remove the update material quantity (add the negative)
                NewMatQuantity = ListMatQuantity + NewItemQuantity
            Else
                ' Here, we need to figure out how many items per run to remove (3 inv mats per job, and remove 2 items, then remove 6 invention mats)
                NumInventionJobs = CInt(Math.Ceiling(CurrentItem.AvgInvRunsforSuccess * Math.Ceiling(NewItemQuantity / CurrentItem.InventedRunsPerBP)))

                ' Update quantity based on invention calculations
                NewMatQuantity = NumInventionJobs * SingleRunQuantity
                RefMatQuantity = NewMatQuantity
            End If

        ElseIf ProcessingType = "Buy" Or ProcessingType = "Build" Then

            MEBonus = (1 - (CurrentItem.ItemME / 100)) * CurrentItem.FacilityMEModifier

            ' Figure out how many bps we need now and apply the ME bonus for each bp and sum up
            Dim NewNumBPs As Integer
            Dim NewRunsperBP As Integer

            If CurrentItem.InventionJobs <> 0 Then
                If CurrentItem.NumBPs = 1 Then
                    NewNumBPs = CInt(Math.Ceiling(NewItemQuantity / CurrentItem.InventedRunsPerBP))
                Else
                    NewNumBPs = CInt(Math.Ceiling(NewItemQuantity / (CurrentItem.Quantity / CurrentItem.NumBPs)))
                End If

                NewRunsperBP = CInt(Math.Ceiling(NewItemQuantity / NewNumBPs))
            Else
                ' This isn't invented so just use the number of blueprints
                NewNumBPs = CurrentItem.NumBPs

                NewRunsperBP = CInt(Math.Ceiling(NewItemQuantity / NewNumBPs))

                ' Make sure we aren't building more bps than the quantity
                If NewNumBPs > NewItemQuantity Then
                    NewNumBPs = CInt(NewItemQuantity)
                End If

            End If

            ' For each bp, apply the me bonus and add up
            For i = 1 To NewNumBPs
                ' Set the quantity: required = max(runs,ceil(round(runs * baseQuantity * materialModifier,2))
                NewMatQuantity += CLng(Math.Max(NewRunsperBP, Math.Ceiling(Math.Round(NewRunsperBP * SingleRunQuantity * MEBonus, 2))))
            Next

            ' Set the mat quantity for reference
            RefMatQuantity = NewMatQuantity

            ' Get the quantity from the correct list so we have the right total materials of all items using this material
            If ProcessingType = "Buy" Then
                ListMatQuantity = TotalBuyList.SearchListbyName(UpdateItemMaterial.GetMaterialName).GetQuantity
            ElseIf ProcessingType = "Build" Then
                ListMatQuantity = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem).ItemQuantity
            End If

            ' Set the current mat quantity for calc below
            CurrentMatQuantity = UpdateItemMaterial.GetQuantity

        End If

        ' Update with onhand mats functionality
        If UpdateItemMaterial.GetBuildItem Then ' Building
            ' If entered, use this as the quantity to update to reflect that the user already entered a updated value
            OnHandMats = GetOnHandComponentQuantity(UpdateItemMaterial.GetMaterialName)
        Else ' Buying
            ' If entered, use this as the quantity to update to reflect that the user already entered a updated value
            OnHandMats = GetOnHandMaterialQuantity(UpdateItemMaterial.GetMaterialName)
        End If

        If OnHandMats <> 0 Then
            UpdatedQuantity = NewMatQuantity - OnHandMats
        Else
            UpdatedQuantity = NewMatQuantity
        End If

        ' Decrease the mats in the shopping list from what we had, then add what we now need
        UpdatedQuantity = (ListMatQuantity - UpdateItemMaterial.GetQuantity) + UpdatedQuantity

        ' If the update caused it go below zero, reset
        If UpdatedQuantity < 0 Then
            UpdatedQuantity = 0
        End If

        Return UpdatedQuantity

    End Function

    Private Function GetNewMatQuantity(ByVal ItemData As ShoppingListItem, ByVal Activity As IndustryActivities, _
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
            SQL = SQL & " AND ACTIVITY = 8"
        Else
            SQL = "SELECT QUANTITY FROM ALL_BLUEPRINT_MATERIALS WHERE PRODUCT_ID = " & ItemData.TypeID & " AND MATERIAL_ID = " & UpdateMaterial.GetMaterialTypeID
            SQL = SQL & " AND ACTIVITY = 1"
        End If

        DBCommand = New SQLiteCommand(SQL, DB)
        rsMatQuantity = DBCommand.ExecuteReader
        rsMatQuantity.Read()

        SingleRunQuantity = rsMatQuantity.GetInt64(0)

        MEBonus = (1 - (ItemData.ItemME / 100)) * ItemData.FacilityMEModifier

        ' Figure out how many bps we need now and apply the ME bonus for each bp and sum up
        Dim NewNumBPs As Integer
        Dim NewRunsperBP As Integer

        If ItemData.InventionJobs <> 0 Then
            If ItemData.NumBPs = 1 Then
                NewNumBPs = CInt(Math.Ceiling(NewQuantity / ItemData.InventedRunsPerBP))
            Else
                NewNumBPs = CInt(Math.Ceiling(NewQuantity / (ItemData.Quantity / ItemData.NumBPs)))
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
                .Quantity = .Quantity + SentItem.Quantity
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
                    .BPMaterialList = CType(SentItem.BPMaterialList.Clone, Materials)
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

        ' Total Build
        If Not IsNothing(SentBuildList) Then
            If Not IsNothing(SentBuildList.GetBuiltItemList) Then
                For i = 0 To SentBuildList.GetBuiltItemList.Count - 1
                    TotalBuildList.AddBuiltItem(SentBuildList.GetBuiltItemList(i))
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
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GetMaterialGroup, .GetQuantity, .GetVolume, 0, .GetItemME, .GetBuildItem, .GetItemType)
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
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GetMaterialGroup, .GetQuantity, .GetVolume, 0, .GetItemME, .GetBuildItem, .GetItemType)
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
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GetMaterialGroup, .GetQuantity, .GetVolume, 0, .GetItemME, .GetBuildItem, .GetItemType)
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
                    TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GetMaterialGroup, .GetQuantity, .GetVolume, 0, .GetItemME, .GetBuildItem, .GetItemType)
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
                            TransferMaterial = New Material(.GetMaterialTypeID, .GetMaterialName, .GetMaterialGroup, .GetQuantity, .GetVolume, 0, .GetItemME, .GetBuildItem, .GetItemType)
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
    Public Function GetClipboardList(ByVal ExportFormat As String, ByVal IgnorePriceVolume As Boolean, ByVal MaterialNamesSortOrder() As String, ByVal ItemNamesSortOrder() As String, ByVal BuildItemsSortOrder() As String) As String
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
        FullBuildList = GetFullBuildList() ' GetFullBuildList uses BuildItem for built in pos, and Volume for the facility ME value
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
                    Dim GroupNameItems As String() = .GetMaterialGroup.Split(New [Char]() {"|"c})
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
        If Not IsNothing(FullItemList) Then
            TempListText = FullItemList.GetClipboardList(ExportFormat, IgnorePriceVolume, True, True)
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
            TempListText = InventionMatList.GetClipboardList(ExportFormat, False, False, False)
            If TempListText <> "No items in List" Then
                OutputText = OutputText & "Estimated Invention Materials: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf & vbCrLf
            End If
        End If

        ' RE Materials (If they exist)
        If IncludeREMats Then
            ' Add RE mats if there are any
            TempListText = REMatList.GetClipboardList(ExportFormat, False, False, False)
            If TempListText <> "No items in List" Then
                OutputText = OutputText & "Estimated RE Materials: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf & vbCrLf
            End If
        End If

        ' Sort the Build List by order sent (this is based on how they sorted in the grid)        
        ' Build item sort order - Name, Quantity, and ME
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
            TempListText = FullBuildList.GetClipboardList(ExportFormat, True, True, False)
            If TempListText <> "No items in List" Then
                OutputText = OutputText & "Build Items List: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf
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
            TempListText = FullBuyList.GetClipboardList(ExportFormat, False, False, False)
            If TempListText <> "No materials in List" Then
                OutputText = OutputText & "Buy Materials List: " & vbCrLf
                OutputText = OutputText & TempListText
                ' Spacer
                OutputText = OutputText & vbCrLf
            End If
        End If

        ' Add total build volume to end
        If ExportFormat = CSVDataExport Then
            OutputText = OutputText & "Total Volume of Built Item(s):," & CStr(FullItemList.GetTotalVolume) & ",m3"
        ElseIf ExportFormat = SSVDataExport Then
            OutputText = OutputText & "Total Volume of Built Item(s):;" & CStr(FullItemList.GetTotalVolume) & ";m3"
        Else
            OutputText = OutputText & "Total Volume of Built Item(s): " & FormatNumber(FullItemList.GetTotalVolume, 2) & " m3"
        End If

        Return OutputText

    End Function

    ' Returns the total number of items in the list
    Public Function GetNumShoppingItems() As Long
        Return TotalItemList.Count
    End Function

    ' Returns the full list of build items as materials
    Public Function GetFullBuildList() As Materials
        Dim ReturnBuildItems As New Materials
        Dim TempMat As Material
        Dim TempBuiltItem As BuiltItem

        ' Go through all the built items and insert the materials
        For j = 0 To TotalBuildList.GetBuiltItemList.Count - 1
            If Not IsNothing(TotalBuildList.GetBuiltItemList(j)) Then
                TempBuiltItem = TotalBuildList.GetBuiltItemList(j)
                ' Use Volume for the facility ME value, since this isn't used (also ignore total volume)
                TempMat = New Material(TempBuiltItem.ItemTypeID, TempBuiltItem.ItemName, "Built Item", TempBuiltItem.ItemQuantity, TempBuiltItem.FacilityMEModifier, 0, CStr(TempBuiltItem.BuildME))

                Call ReturnBuildItems.InsertMaterial(TempMat)

            End If
        Next

        ' Sort
        Call ReturnBuildItems.SortMaterialListByQuantity()

        Return ReturnBuildItems

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
                TempMat = New Material(.TypeID, .Name, .BuildType & "|" & .Decryptor & "|" & CStr(.NumBPs) & "|" & CStr(.Relic) & "|" & .BuildLocation, .Quantity, .BuildVolume, 0, CStr(.ItemME))
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
    Public Sub SetPriceData(IncludeMaterialBrokerFees As Boolean, IncludeUsage As Boolean, ItemBuyTypeList As List(Of ItemBuyType))

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
        If IncludeMaterialBrokerFees Then
            MaterialsBrokerFee = CalculateBrokersFees(ItemBuyTypeList)
        End If

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
    Private Function CalculateBrokersFees(ItemList As List(Of ItemBuyType)) As Double
        Dim TotalBrokersFee As Double = 0

        ' Loop through the buy list and check the item list to see if we apply brokers fees or not
        If Not IsNothing(TotalBuyList.GetMaterialList) Then
            For i = 0 To TotalBuyList.GetMaterialList.Count - 1
                For j = 0 To ItemList.Count - 1
                    If TotalBuyList.GetMaterialList(i).GetMaterialName = ItemList(j).ItemName Then
                        If ItemList(j).BuyType = "Buy Order" Then
                            ' Apply broker fee
                            TotalBrokersFee += GetSalesBrokerFee(TotalBuyList.GetMaterialList(i).GetTotalCost)
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
        If Item1.BuildLocation <> Item2.BuildLocation Then
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
            CopyOfMe.TotalCopyMats = CType(.TotalCopyMats, Materials)
            CopyOfMe.OnHandMatList = CType(.OnHandMatList.Clone, Materials)
            CopyOfMe.OnHandComponentList = CType(.OnHandComponentList.Clone, Materials)
            CopyOfMe.AdditionalCosts = .AdditionalCosts
            CopyOfMe.MaterialsBrokerFee = .MaterialsBrokerFee
        End With

        Return CopyOfMe

    End Function

    Private Function ManufacturingFacility() As Object
        Throw New NotImplementedException
    End Function

End Class

Public Class ShoppingListItem
    Public BlueprintTypeID As Long ' BP Type ID
    Public TypeID As Long ' TypeID for the item 
    Public Quantity As Long ' Number of items we are shopping for
    Public Name As String ' Item we want to shop for * Key Value
    Public BuildType As String ' Component / Raw / Build/Buy * Key Value
    Public ItemME As Double ' The ME of the Shopping item * Key Value
    Public TechLevel As Integer ' T1, T2, or T3
    Public BuildVolume As Double ' Volume of the built item

    Public Decryptor As String ' If it's invented or RE'd, then store the Relic or Decryptor name here * Key value
    Public Relic As String ' Relic used for T3

    Public InventionMaterials As New Materials ' The List of Invention materials needed to build the T2 item
    Public CopyMaterials As New Materials ' List of materials used to make the copies to invent
    Public AvgInvRunsforSuccess As Double ' How many Invention/RE runs we need for success - used to calculate correct changes in list for mats
    Public InventionJobs As Long ' How many jobs did we do
    Public InventedRunsPerBP As Integer ' Number of runs for each bp in NumBPs (helps with determining invention changes later)

    Public NumBPs As Integer ' Number of BPs used to build item

    Public BPMaterialList As New Materials ' This is the list of items on the Blueprint so we have a record of what they are building - it is not updated

    ' For ME values to update with add/subtract in shopping list, item is either cap component, component, or anything else we are building
    Public FacilityMEModifier As Double

    Public BuildLocation As String ' This is the name of the station or Array (with system name) where we build the items

    ' Flag to tell if the item is built in a POS or not
    Public BuiltInPOS As Boolean
    ' Ignore Variables
    Public IgnoredInvention As Boolean
    Public IgnoredMinerals As Boolean
    Public IgnoredT1BaseItem As Boolean

    ' Saved price variables for this item - can be updated when quantity updated
    ' Public TotalMaterialCost As Double ' This is the cost of all the materials to build the item (does not include usage, item taxes or item fees) and invention and copy costs
    Public TotalUsage As Double ' Includes Manufacturing, Components, Invention, and Copying usage
    Public TotalItemMarketCost As Double ' This is the market cost of the items in the list
    Public TotalBuildTime As Double ' Total time to build the items for the given type of building

    Public Sub New()

        BlueprintTypeID = 0
        TypeID = 0
        Quantity = 0
        Name = ""
        BuildType = ""
        ItemME = 0
        TechLevel = 0
        BuildVolume = 0

        Decryptor = ""
        Relic = ""

        InventionMaterials = New Materials
        CopyMaterials = New Materials
        AvgInvRunsforSuccess = 0
        InventedRunsPerBP = 0
        InventionJobs = 0

        NumBPs = 0

        BPMaterialList = Nothing

        FacilityMEModifier = 1
        BuildLocation = ""
        BuiltInPOS = False
        IgnoredInvention = False
        IgnoredMinerals = False
        IgnoredT1BaseItem = False

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
            TempItem = CType(ItemList(i).Clone(), BuiltItem)
            CopyOfMe.AddBuiltItem(TempItem)
        Next

        CopyOfMe.TotalCost = Me.TotalCost

        Return CopyOfMe

    End Function

    ' Adds a sent built item to the main list, updating quantities for same items
    Public Sub AddBuiltItem(ByVal SentItem As BuiltItem)
        Dim FoundItem As New BuiltItem
        Dim UpdateItem As New BuiltItem
        Dim CombinedMaterials As New Materials
        Dim AddItem As BuiltItem = CType(sentitem.clone, BuiltItem)

        ' Search the list to see if the item exists
        ItemToFind = AddItem
        FoundItem = ItemList.Find(AddressOf FindBuiltItem)

        If FoundItem IsNot Nothing Then
            ' Exists, so update the quantity and materials
            ' Remove the item from the list we are updating
            Call ItemList.Remove(FoundItem)
            UpdateItem.BPTypeID = AddItem.BPTypeID
            UpdateItem.ItemTypeID = FoundItem.ItemTypeID
            UpdateItem.ItemName = AddItem.ItemName
            UpdateItem.BuildME = AddItem.BuildME
            UpdateItem.BuildTE = AddItem.BuildTE
            UpdateItem.ItemVolume = AddItem.ItemVolume
            UpdateItem.FacilityMEModifier = AddItem.FacilityMEModifier

            ' Combine the materials
            CombinedMaterials.InsertMaterialList(FoundItem.BuildMaterials.GetMaterialList)
            CombinedMaterials.InsertMaterialList(AddItem.BuildMaterials.GetMaterialList)
            UpdateItem.BuildMaterials = CType(CombinedMaterials.Clone, Materials)

            ' Update the quantity
            UpdateItem.ItemQuantity = AddItem.ItemQuantity + FoundItem.ItemQuantity

            ' Cost
            TotalCost = TotalCost + UpdateItem.BuildMaterials.GetTotalMaterialsCost

            ' Re-add the item
            Call ItemList.Add(UpdateItem)

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
            TotalCost = TotalCost - RemoveItem.BuildMaterials.GetTotalMaterialsCost

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
        If Item.ItemTypeID = ItemToFind.ItemTypeID And Item.BuildME = ItemToFind.BuildME Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Function takes an item name and returns an array of all the items with that same name (could have different MEs)
    Public Function FindBuiltItems(ByVal ItemName As String) As BuiltItemList
        Dim ReturnList As New BuiltItemList

        For i = 0 To ItemList.Count - 1
            If ItemList(i).ItemName = ItemName Then
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
    Public ItemVolume As Double
    Public BuildME As Integer
    Public BuildTE As Integer
    Public BuildMaterials As Materials
    ' These two fields are for shopping list update functions
    Public FacilityMEModifier As Double

    Public Sub New()
        ItemTypeID = 0
        ItemName = ""
        ItemQuantity = 0
        ItemVolume = 0
        BuildME = 0
        BuildMaterials = New Materials
    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe As New BuiltItem
        CopyOfMe.BPTypeID = Me.BPTypeID
        CopyOfMe.ItemTypeID = Me.ItemTypeID
        CopyOfMe.ItemName = Me.ItemName
        CopyOfMe.ItemQuantity = Me.ItemQuantity
        CopyOfMe.ItemVolume = Me.ItemVolume
        CopyOfMe.BuildME = Me.BuildME
        CopyOfMe.BuildTE = Me.BuildTE
        CopyOfMe.BuildMaterials = Me.BuildMaterials
        CopyOfMe.FacilityMEModifier = Me.FacilityMEModifier
        Return CopyOfMe
    End Function

End Class