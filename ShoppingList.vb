
Public Class ShoppingList
    Implements ICloneable

    ' Master Lists of materials to display. These are single lists that are updated when deleting quantity or full items
    Private TotalItemList As List(Of ShoppingListItem) ' This is the total list of items
    Private TotalBuyList As Materials ' Buy mats
    Private TotalBuildList As BuiltItemList ' Build mats (components)
    Private TotalInventionMats As Materials ' All Invention/RE materials used

    ' Array of all the items sent with their base build & sell prices and time to build
    Private ItemProfitIPHList As List(Of ItemProfitIPH)

    ' Use onhandlist of materials so we can keep track of user entries (of calculations they make based on mats on hand) 
    Public OnHandMatList As New Materials
    Public OnHandComponentList As New Materials

    Private AdditionalCosts As Double

    ' Price data
    Private MaterialsBrokerFee As Double
    Private ItemsTax As Double
    Private ItemsBrokerFee As Double
    Private TotalUsage As Double
    Private TotalCost As Double
    Private TotalItemsCost As Double

    Protected ItemToFind As ShoppingListItem
    Protected ProfitItemtoFind As String

    Private Class ItemProfitIPH

        Implements ICloneable

        ' Use Time to build and Total Sale Price with Total Cost to find IPH and Profit later
        Public TotalTimetoBuild As Double ' In seconds
        Public TotalItemsSellPrice As Double ' Total of expected sell market price for the items

        ' Item info
        Public SavedItemName As String
        Public SavedItemQuantity As Long
        Public SavedTechLevel As Integer

        ' Other Costs
        Public Usage As Double
        Public InventionUsage As Double
        Public InventionRunsCost As Double ' Cost to invent/RE the quantity given

        Public ItemtoFind As String

        Public Sub New()
            TotalTimetoBuild = 0
            TotalItemsSellPrice = 0
            SavedItemName = ""
            SavedItemQuantity = 0
            SavedTechLevel = 0
            Usage = 0
            InventionUsage = 0
            InventionRunsCost = 0
        End Sub

        Public Function Clone() As Object Implements System.ICloneable.Clone
            Dim CopyOfMe = New ItemProfitIPH

            With Me
                CopyOfMe.TotalTimetoBuild = Me.TotalTimetoBuild
                CopyOfMe.TotalItemsSellPrice = Me.TotalItemsSellPrice
                CopyOfMe.SavedItemName = Me.SavedItemName
                CopyOfMe.SavedItemQuantity = Me.SavedItemQuantity
                CopyOfMe.SavedTechLevel = Me.SavedTechLevel
                CopyOfMe.Usage = Me.Usage
                CopyOfMe.InventionUsage = Me.InventionUsage
                CopyOfMe.InventionRunsCost = Me.InventionRunsCost
            End With

            Return CopyOfMe

        End Function

    End Class

    Public Sub New()

        Call Clear()

    End Sub

    Public Sub Clear()

        TotalItemList = New List(Of ShoppingListItem)

        TotalBuildList = New BuiltItemList
        TotalBuyList = New Materials

        TotalInventionMats = New Materials

        AdditionalCosts = 0
        ItemsTax = 0
        ItemsBrokerFee = 0
        MaterialsBrokerFee = 0
        TotalUsage = 0

        TotalCost = 0
        TotalItemsCost = 0

        ItemToFind = Nothing

        ItemProfitIPHList = New List(Of ItemProfitIPH)

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
                            TempBuiltItem.BuildME = CLng(.GetMaterialList(i).GetItemME)

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
                        Call .GetMaterialList(i).SetQuantity(CLng(.GetMaterialList(i).GetQuantity / FoundItem.Quantity) * UpdateItemQuantity)

                    Next
                End With
            End If

            ' Update Buy List with Materials, Invention Mats, and RE mats (if they exist)
            If Not IsNothing(FoundItem.InventionMaterials) Then
                If Not IsNothing(FoundItem.InventionMaterials.GetMaterialList) Then
                    With FoundItem.InventionMaterials ' Update all base materials for this item first
                        For i = 0 To .GetMaterialList.Count - 1
                            ' Make sure the material exists (might have been deleted already in the main list) before updating
                            If Not IsNothing(TotalBuyList.SearchListbyName(.GetMaterialList(i).GetMaterialName)) Then
                                ' Need to update to the quantity sent in the Buy List
                                If FoundItem.TechLevel = 2 Then
                                    UpdatedQuantity = GetUpdatedQuantity("Invention", FoundItem, UpdateItemQuantity, .GetMaterialList(i))
                                ElseIf FoundItem.TechLevel = 3 Then
                                    UpdatedQuantity = GetUpdatedQuantity("RE", FoundItem, UpdateItemQuantity, .GetMaterialList(i))
                                End If

                                Call UpdateShoppingBuyQuantity(.GetMaterialList(i).GetMaterialName, UpdatedQuantity)
                                ' Update this material in the item's invention list for copy/paste function
                                If UpdatedQuantity <= 0 Then
                                    Call TotalInventionMats.RemoveMaterial(.GetMaterialList(i))
                                Else
                                    ' Need to copy, remove, update, then add to update the volumes and prices of the material lists
                                    Dim TempMat As Material
                                    TempMat = CType(TotalInventionMats.SearchListbyName(.GetMaterialList(i).GetMaterialName).Clone, Material)
                                    TempMat.SetQuantity(UpdatedQuantity)
                                    Call TotalInventionMats.RemoveMaterial(.GetMaterialList(i))
                                    Call TotalInventionMats.InsertMaterial(TempMat)
                                End If
                            End If
                        Next
                    End With
                End If
            End If

            ' Remove the item if zero or update
            If UpdateItemQuantity = 0 Then
                ' Find the item and remove it from the list
                Dim ProfitItem As New ItemProfitIPH
                ProfitItemtoFind = SentItem.Name
                ProfitItem = ItemProfitIPHList.Find(AddressOf FindItemProfitRecord)
                If ProfitItem IsNot Nothing Then
                    ItemProfitIPHList.Remove(ProfitItem)
                End If
            Else
                ' Update the build times and total sell prices for this item
                For i = 0 To ItemProfitIPHList.Count - 1
                    With ItemProfitIPHList(i)
                        If .SavedItemName = FoundItem.Name Then
                            ' Normalize the price and time, then multiply by the new update quantity
                            Dim Denominator As Double = .SavedItemQuantity
                            .TotalItemsSellPrice = .TotalItemsSellPrice / Denominator * UpdateItemQuantity
                            .TotalTimetoBuild = .TotalTimetoBuild / Denominator * UpdateItemQuantity
                            .Usage = .Usage / Denominator * UpdateItemQuantity
                            .SavedItemQuantity = UpdateItemQuantity
                        End If
                    End With
                Next
            End If

            ' Need to increment or decrement the new item quantity and volume, the rest of the mats and components will be updated above
            If UpdateItemQuantity = 0 Then
                Call TotalItemList.Remove(FoundItem)
            Else
                FoundItem.BuildVolume = FoundItem.BuildVolume / FoundItem.Quantity * UpdateItemQuantity
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
                        ShoppingItem.FacilityMEModifier = FoundItem.FacilityMEModifier
                        ShoppingItem.ItemME = FoundItem.BuildME
                        ShoppingItem.Quantity = FoundItem.ItemQuantity

                        ' Blank these out for now if we use them later
                        ShoppingItem.InventionJobs = 1
                        ShoppingItem.NumBPs = 1
                        ShoppingItem.InventedRuns = 1
                        ShoppingItem.AvgInvRunsforSuccess = 1

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
        Dim UpdatedQuantity As Long
        Dim OnHandMats As Long
        Dim ListMatQuantity As Long
        Dim SingleRunQuantity As Long

        ' Set up the ME bonus and then calculate the new material quantity
        Dim MEBonus As Double = (1 - (CurrentItem.ItemME / 100)) * CurrentItem.FacilityMEModifier

        ' Figure out what the original material amount was by working backwards
        ' if the mat quantity is less than user runs, then the quantity is user runs
        If UpdateItemMaterial.GetQuantity <= CurrentItem.Quantity Then
            SingleRunQuantity = 1
        Else
            ' mat quantity / (orig item quantity * me bonus) - round but use floor to reverse the celiling
            SingleRunQuantity = CLng(Math.Floor(Math.Round(UpdateItemMaterial.GetQuantity / (CurrentItem.Quantity * MEBonus), 2)))
        End If

        ' Recalculate the new quantity - Assume that every component is only built with 1 bp
        Dim NewMatQuantity As Long = CLng(Math.Max(NewItemQuantity, Math.Ceiling(Math.Round(NewItemQuantity * SingleRunQuantity * MEBonus, 2))))

        ' Set the mat quantity for reference
        RefMatQuantity = NewMatQuantity

        If ProcessingType = "Invention" Or ProcessingType = "RE" Then

            ListMatQuantity = TotalBuyList.SearchListbyName(UpdateItemMaterial.GetMaterialName).GetQuantity

            ' For invention materials, find out how many mats we need by calcuating the new value from the item runs and invention jobs per item
            If NewItemQuantity <= 0 Then
                ' Easy case, just remove the update material quantity (add the negative)
                UpdatedQuantity = ListMatQuantity + NewMatQuantity
            Else
                ' Here, we need to figure out how many items per run to remove (3 inv mats per job, and remove 2 items, then remove 6 invention mats)
                Dim NumInventionJobs As Integer = 0
                Dim MatsperItem As Integer = CInt(UpdateItemMaterial.GetQuantity / CurrentItem.InventionJobs)
                Dim RunsPerBP As Integer = CInt(NewItemQuantity / CurrentItem.NumBPs)
                ' for each blueprint, we need to calculate the mats to make it
                For i = 0 To CurrentItem.NumBPs - 1
                    NumInventionJobs += CInt(Math.Ceiling(CurrentItem.AvgInvRunsforSuccess * Math.Ceiling(RunsPerBP / CurrentItem.InventedRuns)))
                Next

                ' Update quantity based on invention calculations
                UpdatedQuantity = NumInventionJobs * MatsperItem

            End If

        ElseIf ProcessingType = "Buy" Or ProcessingType = "Build" Then
            ' Get the quantity from the correct list
            If ProcessingType = "Buy" Then
                ListMatQuantity = TotalBuyList.SearchListbyName(UpdateItemMaterial.GetMaterialName).GetQuantity
            ElseIf ProcessingType = "Build" Then
                ListMatQuantity = TotalBuildList.GetBuiltItemList.Find(AddressOf TotalBuildList.FindBuiltItem).ItemQuantity
            End If

            ' Take away what we had in the list last time and add the new quantity to the list
            UpdatedQuantity = (ListMatQuantity - UpdateItemMaterial.GetQuantity) + NewMatQuantity

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
            UpdatedQuantity = UpdatedQuantity - OnHandMats
        End If

        ' If the update caused it go below zero, reset
        If UpdatedQuantity < 0 Then
            UpdatedQuantity = 0
        End If

        Return UpdatedQuantity

    End Function

#End Region

    ' Inserts a full shopping list item into the list
    Public Sub InsertShoppingItem(ByVal SentItem As ShoppingListItem, ByVal SentBuildList As BuiltItemList, ByVal SentBuyList As Materials)
        Dim FoundItem As New ShoppingListItem
        Dim TempMats As New Materials
        Dim TempItems As New BuiltItemList
        Dim SearchBuiltItems As New BuiltItemList
        Dim TempProfitIPH As New ItemProfitIPH

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

        ' Invention/RE Materials
        If Not IsNothing(SentItem.InventionMaterials) Then
            TotalInventionMats.InsertMaterialList(SentItem.InventionMaterials.GetMaterialList)
        End If

        ' Add this item to the saved list of with all the base price data
        TempProfitIPH.TotalTimetoBuild = SentItem.TotalBuildTime
        TempProfitIPH.TotalItemsSellPrice = SentItem.TotalMarketCost
        TempProfitIPH.SavedItemName = SentItem.Name
        TempProfitIPH.SavedItemQuantity = SentItem.Quantity
        TempProfitIPH.SavedTechLevel = SentItem.TechLevel
        TempProfitIPH.Usage = SentItem.ItemUsage
        TempProfitIPH.InventionRunsCost = SentItem.InventionCost
        TempProfitIPH.InventionUsage = SentItem.InventionUsage

        Call ItemProfitIPHList.Add(TempProfitIPH)

        ItemToFind = Nothing

    End Sub

    ' Will update all the prices in the shopping list
    Public Sub UpdateListPrices()
        Dim TempBuyList As New Materials ' Buy mats
        Dim TempBuildList As New BuiltItemList ' Build mats (components)
        Dim TempInventionMats As New Materials ' All Invention/RE materials used
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

        ' Invention/RE List
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

        ' Add the RE mats to buy
        REMatList = GetFullREList()
        If Not IsNothing(REMatList.GetMaterialList) Then
            IncludeREMats = True
            ' Remove the RE materials from the buy list so we can separate them in the output
            Call FullBuyList.RemoveMaterialList(REMatList.GetMaterialList)
            ' Update the total though as if the materials were in the full list for price purposes
            FullBuyList.AddTotalValue(REMatList.GetTotalMaterialsCost)
            FullBuyList.AddTotalVolume(REMatList.GetTotalVolume)
        End If

        ' Sort the Item List by order sent (this is based on how they sorted in the grid)
        ' Item sort order Name, Quantity, ME, Num BPs, Build Type, Decryptor, and Relic
        For i = 0 To ItemNamesSortOrder.Count - 1
            ' Parse the sort order fields
            Dim ItemColumns As String() = ItemNamesSortOrder(i).Split(New [Char]() {"|"c})

            ' For each item, find it in the current buy list and replace
            ' Item sort order Name, Quantity, ME, Num BPs, Build Type, Decryptor/Relic
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
                     And ItemColumns(3) = GroupNameItems(2) And RelicName = GroupNameItems(3) Then
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
        Dim TotalInventionMats As New Materials

        For i = 0 To TotalItemList.Count - 1
            If TotalItemList(i).TechLevel = 2 And Not IsNothing(TotalItemList(i).InventionMaterials) Then
                TotalInventionMats.InsertMaterialList(TotalItemList(i).InventionMaterials.GetMaterialList)
            End If
        Next

        Return TotalInventionMats

    End Function

    ' Returns the list of RE items and quantity
    Public Function GetFullREList() As Materials
        Dim TotalREMats As New Materials

        For i = 0 To TotalItemList.Count - 1
            If TotalItemList(i).TechLevel = 3 And Not IsNothing(TotalItemList(i).InventionMaterials) Then
                TotalREMats.InsertMaterialList(TotalItemList(i).InventionMaterials.GetMaterialList)
            End If
        Next
        Return TotalREMats

    End Function

    ' Returns the full list of Items we want to build in the shopping list
    Public Function GetFullItemList() As Materials
        Dim TempMat As Material
        Dim ReturnMaterials As New Materials

        For i = 0 To TotalItemList.Count - 1
            With TotalItemList(i)
                ' Item sort order is Build Type, Decryptor, NumBps, and Relic for the group name
                TempMat = New Material(.TypeID, .Name, .BuildType & "|" & .Decryptor & "|" & CStr(.NumBPs) & "|" & CStr(.Relic), .Quantity, .BuildVolume, 0, CStr(.ItemME))
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
    Public Sub SetPriceData(IncludeMaterialBrokerFees As Boolean, IncludeItemsTaxes As Boolean, IncludeItemsBrokerFees As Boolean, IncludeUsage As Boolean, ItemBuyTypeList As List(Of ItemBuyType))

        ' The only fee that applies when shopping is either a buy order or directly buying - Broker fees are all that apply during a buy order
        If IncludeMaterialBrokerFees Then
            MaterialsBrokerFee = CalculateBrokersFees(ItemBuyTypeList)
        Else
            MaterialsBrokerFee = 0
        End If

        ' Usage for all the jobs
        If IncludeUsage Then
            TotalUsage = GetTotalItemsUsageCosts()
        Else
            TotalUsage = 0
        End If

        ' Now apply taxes or broker fees for the items
        TotalItemsCost = GetTotalItemsSellPrice()

        If IncludeItemsTaxes Then
            ItemsTax = GetTaxes(TotalItemsCost)
        Else
            ItemsTax = 0
        End If

        If IncludeItemsBrokerFees Then
            ItemsBrokerFee = GetBrokerFee(TotalItemsCost)
        Else
            ItemsBrokerFee = 0
        End If

        ' Set the total cost of materials plus any invention or RE
        TotalCost = TotalBuyList.GetTotalMaterialsCost + GetTotalInventionCost() + GetTotalRECost() + AdditionalCosts + MaterialsBrokerFee + TotalUsage + ItemsTax + ItemsBrokerFee

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
                            TotalBrokersFee += GetBrokerFee(TotalBuyList.GetMaterialList(i).GetTotalCost)
                        End If
                    End If
                Next
            Next
        End If

        Return TotalBrokersFee

    End Function

    ' Returns the total profit of the items made with raw mats in the list
    Public Function GetTotalProfit() As Double
        Return TotalItemsCost - TotalCost
    End Function

    ' Returns the average IPH for the items in this list
    Public Function GetTotalIPH() As Double
        Dim TotalSeconds As Double

        For i = 0 To ItemProfitIPHList.Count - 1
            TotalSeconds = TotalSeconds + ItemProfitIPHList(i).TotalTimetoBuild
        Next

        ' Don't divide by zero
        If TotalSeconds > 0 Then
            Return GetTotalProfit() / TotalSeconds * 3600 ' Build everything
        Else
            Return 0
        End If

    End Function

    ' Returns the total tax for the items in the list
    Public Function GetItemsTax() As Double
        Return ItemsTax
    End Function

    ' Returns the total broker fees for the items in the lsit
    Public Function GetItemsBrokersFee() As Double
        Return ItemsBrokerFee
    End Function

    ' Returns the total fees to set up sell orders for the total value of buy materials
    Public Function GetTotalMaterialsBrokersFees() As Double
        Return MaterialsBrokerFee
    End Function

    ' Returns the total cost of the materials to buy plus any additional costs
    Public Function GetTotalCost() As Double
        Return TotalBuyList.GetTotalMaterialsCost + AdditionalCosts
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
        Return TotalUsage
    End Function

    ' Returns the total market price for all stuff added
    Private Function GetTotalItemsSellPrice() As Double
        Dim Total As Double

        For i = 0 To ItemProfitIPHList.Count - 1
            Total = Total + ItemProfitIPHList(i).TotalItemsSellPrice
        Next

        Return Total

    End Function

    ' Returns the total cost of Invention for the shopping list
    Private Function GetTotalInventionCost() As Double
        Dim MaterialCosts As Double = 0
        Dim UsageCosts As Double = 0
        Dim TotalCost As Double = 0

        ' Total up the material costs
        For i = 0 To TotalItemList.Count - 1
            If Not IsNothing(TotalItemList(i).InventionMaterials) And TotalItemList(i).TechLevel = 2 Then
                MaterialCosts += TotalItemList(i).InventionMaterials.GetTotalMaterialsCost
                ' Find out how many invention runs we are going to do
                TotalCost += (TotalItemList(i).InventionJobs * TotalItemList(i).InventionUsage)
            End If
        Next

        ' If no mats sent then use the invention costs sent, which includes all costs for runs
        If MaterialCosts = 0 Then
            For i = 0 To ItemProfitIPHList.Count - 1
                If ItemProfitIPHList(i).SavedTechLevel = 2 Then
                    TotalCost += ItemProfitIPHList(i).InventionRunsCost
                End If
            Next
        End If

        Return TotalCost

    End Function

    ' Returns the total cost of RE for the shopping list
    Private Function GetTotalRECost() As Double
        Dim MaterialCosts As Double = 0
        Dim UsageCosts As Double = 0
        Dim TotalCost As Double = 0

        ' Total up the material costs
        For i = 0 To TotalItemList.Count - 1
            If Not IsNothing(TotalItemList(i).InventionMaterials) And TotalItemList(i).TechLevel = 3 Then
                MaterialCosts += TotalItemList(i).InventionMaterials.GetTotalMaterialsCost
                ' Find out how many invention runs we are going to do
                TotalCost += (TotalItemList(i).InventionJobs * TotalItemList(i).InventionUsage)
            End If
        Next

        ' If no mats sent then use the invention costs sent, which includes all costs for runs
        If MaterialCosts = 0 Then
            For i = 0 To ItemProfitIPHList.Count - 1
                If ItemProfitIPHList(i).SavedTechLevel = 3 Then
                    TotalCost += ItemProfitIPHList(i).InventionRunsCost
                End If
            Next
        End If

        Return TotalCost
    End Function

    ' Returns the total usage fees for all stuff added
    Private Function GetTotalItemsUsageCosts() As Double
        Dim Total As Double

        For i = 0 To ItemProfitIPHList.Count - 1
            Total = Total + ItemProfitIPHList(i).Usage
        Next

        Return Total

    End Function

    ' Predicate for finding the item in full list
    Public Function FindItem(ByVal Item As ShoppingListItem) As Boolean
        If ShopListItemsEqual(Item, ItemToFind) Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Predicate for finding an item in the profit list
    Private Function FindItemProfitRecord(ByVal Item As ItemProfitIPH) As Boolean
        If Item.SavedItemName = ProfitItemtoFind Then
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
        If Item1.NumBPs <> Item2.NumBPs Then
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
        Dim NewItemProfitItemList As New List(Of ItemProfitIPH)
        Dim NewItemProfitItem As New ItemProfitIPH

        With Me
            CopyOfMe.TotalItemList = .TotalItemList
            CopyOfMe.TotalBuyList = CType(.TotalBuyList.Clone, Materials)
            CopyOfMe.TotalBuildList = CType(.TotalBuildList.Clone, BuiltItemList)
            CopyOfMe.TotalInventionMats = CType(.TotalInventionMats, Materials)

            'For i = 0 To .ItemProfitIPHList.Count - 1
            '    Call NewItemProfitItemList.Add(CType(.ItemProfitIPHList(i).Clone, ItemProfitIPH))
            'Next
            'CopyOfMe.ItemProfitIPHList = NewItemProfitItemList

            CopyOfMe.ItemProfitIPHList = .ItemProfitIPHList

            CopyOfMe.OnHandMatList = CType(.OnHandMatList.Clone, Materials)
            CopyOfMe.OnHandComponentList = CType(.OnHandComponentList.Clone, Materials)
            CopyOfMe.AdditionalCosts = .AdditionalCosts
            CopyOfMe.MaterialsBrokerFee = .MaterialsBrokerFee
            CopyOfMe.ItemsBrokerFee = .ItemsBrokerFee
            CopyOfMe.ItemsTax = .ItemsTax
            CopyOfMe.TotalUsage = .TotalUsage
            CopyOfMe.TotalCost = .TotalCost
            CopyOfMe.TotalItemsCost = .TotalItemsCost
        End With


        ' Master Lists of materials to display. These are single lists that are updated when deleting quantity or full items
        'Private TotalItemList As List(Of ShoppingListItem) ' This is the total list of items
        'Private ItemProfitIPHList As List(Of ItemProfitIPH)

        Return CopyOfMe

    End Function

    Private Function ManufacturingFacility() As Object
        Throw New NotImplementedException
    End Function

End Class

Public Class ShoppingListItem
    Public TypeID As Long ' TypeID for the item 
    Public Quantity As Long ' Number of items we are shopping for
    Public Name As String ' Item we want to shop for * Key Value
    Public BuildType As String ' Component / Raw / Build/Buy * Key Value
    Public ItemME As Double ' The ME of the Shopping item * Key Value
    Public TechLevel As Integer ' T1, T2, or T3
    Public BuildVolume As Double ' Volume of the built item
    Public NumBPs As Integer ' Number of BPs used to build item

    Public Decryptor As String ' If it's invented or RE'd, then store the Relic or Decryptor name here * Key value
    Public Relic As String ' Relic used for T3

    Public InventionMaterials As New Materials ' The List of Invention materials needed to build the T2 item
    Public AvgInvRunsforSuccess As Double ' How many Invention/RE runs we need for success - used to calculate correct changes in list for mats
    Public InventionJobs As Long ' How many jobs did we do
    Public InventedRuns As Long ' How many Invented or RE'd runs are produced
    Public InventionCost As Double ' Cost of materials to Invent or RE the the quantity of items sent
    Public InventionUsage As Double ' Cost of installing and using the industry line for a single RE or Invention job

    Public BPMaterialList As New Materials ' This is the list of items on the Blueprint so we have a record of what they are building - it is not updated

    Public TotalMarketCost As Double ' Total Market price of items in once built
    Public TotalBuildTime As Double ' Total build time of items listed in seconds
    Public ItemTaxes As Double ' Taxes for the materials added to the list
    Public ItemBrokerFees As Double ' Broker fees for orders set up on this item
    Public ItemUsage As Double ' How much usage to build the item
    Public ItemMarketCost As Double
    Public ItemBuildTime As Double

    ' For ME values to update with add/subtract in shopping list, item is either cap component, component, or anything else we are building
    Public FacilityMEModifier As Double
    ' Flag to tell if the item is built in a POS or not
    Public BuiltInPOS As Boolean
    ' Ignore Variables
    Public IgnoredInvention As Boolean
    Public IgnoredMinerals As Boolean
    Public IgnoredT1BaseItem As Boolean

    Public Sub New()

        TypeID = 0
        Quantity = 0
        Name = ""
        BuildType = ""
        ItemME = 0
        TechLevel = 0
        BuildVolume = 0

        Decryptor = ""
        Relic = ""

        InventionMaterials = Nothing
        AvgInvRunsforSuccess = 0
        InventedRuns = 0
        InventionCost = 0
        InventionUsage = 0
        InventionJobs = 0

        BPMaterialList = Nothing

        TotalMarketCost = 0
        TotalBuildTime = 0
        ItemTaxes = 0
        ItemUsage = 0
        ItemMarketCost = 0
        ItemBuildTime = 0

        FacilityMEModifier = 1
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
    Public BuildME As Double
    Public BuildTE As Double
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