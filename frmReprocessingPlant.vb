﻿
Imports System.Data.SQLite

Public Class frmReprocessingPlant

    Private ItemsColumnClicked As Integer
    Private ItemsColumnSortType As SortOrder
    Private OutputColumnClicked As Integer
    Private OutputColumnSortType As SortOrder

    ' Ore processing skills
    Private m_ControlsCollection As ControlsCollection
    Private ProcessingCheckBoxes() As CheckBox
    Private ProcessingLabels() As Label
    Private ProcessingCombos() As ComboBox

    Private CheckedItems() As Integer

    Private MaterialOutput As New Materials ' Save globally for easy exporting

    Private CheckedRefineItems As New List(Of RefineItem)
    Private IgnoreChecks As Boolean
    Private RefineItemtoFind As Long

    Private Structure RefineItem
        Dim ItemID As Long
        Dim BuildItem As Boolean ' True, we refine it regardless, False we do not regardless. If not in list, we don't do anything differently
    End Structure

    ' Predicate for finding the BuildBuyItem in full list
    Private Function FindBPBBItem(ByVal Item As RefineItem) As Boolean
        If RefineItemtoFind = Item.ItemID Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_ControlsCollection = New ControlsCollection(Me)

        ' Set up grids
        ' Width is now 570, scrollbar is 21  27
        lstItemstoRefine.Columns.Add("Material", 200, HorizontalAlignment.Left) ' add 25 for check
        lstItemstoRefine.Columns.Add("Quantity", 51, HorizontalAlignment.Right)
        lstItemstoRefine.Columns.Add("Total Cost", 100, HorizontalAlignment.Right)
        lstItemstoRefine.Columns.Add("Rate", 43, HorizontalAlignment.Right)
        lstItemstoRefine.Columns.Add("Refined Value", 100, HorizontalAlignment.Right)
        lstItemstoRefine.Columns.Add("% Return", 55, HorizontalAlignment.Right)
        lstItemstoRefine.Columns.Add("Material Group", 0, HorizontalAlignment.Right) ' Hidden
        lstItemstoRefine.Columns.Add("Item ID", 0, HorizontalAlignment.Right) ' Hidden

        lstRefineOutput.Columns.Add("Material", 200, HorizontalAlignment.Left)
        lstRefineOutput.Columns.Add("Quantity", 90, HorizontalAlignment.Right)
        lstRefineOutput.Columns.Add("Cost Per Item", 77, HorizontalAlignment.Right)
        lstRefineOutput.Columns.Add("Total Cost", 105, HorizontalAlignment.Right)
        lstRefineOutput.Columns.Add("Total Volume", 77, HorizontalAlignment.Right)

        IgnoreChecks = True
        chkToggle.Checked = True ' Default
        IgnoreChecks = False

        ProcessingCheckBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "chkOreProcessing"), CheckBox())
        ProcessingLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblOreProcessing"), Label())
        ProcessingCombos = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "cmbOreProcessing"), ComboBox())

        ' Load all the skills
        cmbRefining.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(3385))
        cmbRefineryEff.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(3389))
        cmbScrapMetalProcessing.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(12196))

        Dim TempSkillLevel As Integer
        For i = 1 To ProcessingCheckBoxes.Count - 1
            TempSkillLevel = SelectedCharacter.Skills.GetSkillLevel(SelectedCharacter.Skills.GetSkillTypeID(ProcessingLabels(i).Text & " Processing"))
            If TempSkillLevel <> 0 Then
                ProcessingCombos(i).Text = CStr(TempSkillLevel)
                ProcessingCheckBoxes(i).Checked = True
            Else
                ProcessingCombos(i).Text = "0"
                ProcessingCheckBoxes(i).Checked = False
            End If
        Next

        Dim Defaults As New ProgramSettings
        Select Case UserApplicationSettings.RefiningImplantValue
            Case (GetAttribute("refiningYieldMutator", Defaults.RBeanCounterName & "1") / 100)
                cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "1"
            Case (GetAttribute("refiningYieldMutator", Defaults.RBeanCounterName & "2") / 100)
                cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "2"
            Case (GetAttribute("refiningYieldMutator", Defaults.RBeanCounterName & "4") / 100)
                cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "4"
            Case Else
                cmbBeanCounterRefining.Text = None
        End Select

        chkRecursiveRefine.Checked = UserApplicationSettings.RefineDrillDown

        ' Update the ore processing skills
        Call UpdateProcessingSkills()

        Call InitializeReprocessingFacility()

        ItemsColumnClicked = 1
        ItemsColumnSortType = SortOrder.Ascending
        OutputColumnClicked = 1
        OutputColumnSortType = SortOrder.Ascending

    End Sub

    Public Sub InitializeReprocessingFacility()
        ' Load the facility
        Call ReprocessingFacility.InitializeControl(SelectedCharacter.ID, ProgramLocation.ReprocessingPlant, ProductionType.Reprocessing, Me)
    End Sub

#Region "Object Events"

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

    Private Sub OreCheckProcessing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOreProcessing1.CheckedChanged, chkOreProcessing2.CheckedChanged, chkOreProcessing3.CheckedChanged,
                                                                                                                    chkOreProcessing4.CheckedChanged, chkOreProcessing5.CheckedChanged, chkOreProcessing6.CheckedChanged,
                                                                                                                    chkOreProcessing7.CheckedChanged, chkOreProcessing8.CheckedChanged, chkOreProcessing9.CheckedChanged,
                                                                                                                    chkOreProcessing10.CheckedChanged, chkOreProcessing11.CheckedChanged, chkOreProcessing12.CheckedChanged,
                                                                                                                    chkOreProcessing13.CheckedChanged, chkOreProcessing14.CheckedChanged, chkOreProcessing15.CheckedChanged,
                                                                                                                    chkOreProcessing16.CheckedChanged, chkOreProcessing17.CheckedChanged, chkOreProcessing18.CheckedChanged,
                                                                                                                    chkOreProcessing19.CheckedChanged, chkOreProcessing20.CheckedChanged, chkOreProcessing21.CheckedChanged,
                                                                                                                    chkOreProcessing22.CheckedChanged, chkOreProcessing23.CheckedChanged, chkOreProcessing24.CheckedChanged,
                                                                                                                    chkOreProcessing25.CheckedChanged

        Call UpdateProcessingSkillBoxes(CInt(CType(sender, CheckBox).Name.Substring(16)), CType(sender, CheckBox).Checked)
    End Sub

    Private Sub UpdateProcessingSkillBoxes(ByVal Index As Integer, ByVal Checked As Boolean)
        ProcessingCombos(Index).Enabled = Checked
        ProcessingLabels(Index).Enabled = Checked
    End Sub

    ' Updates the processing skills (enable, disable) depending on the refining skills selected
    Private Sub UpdateProcessingSkills()

        If FirstLoad Then
            Exit Sub
        End If

        ' Set them all false first
        For i = 1 To ProcessingCheckBoxes.Count - 1
            ProcessingCheckBoxes(i).Enabled = False
        Next

        For i = 1 To ProcessingCombos.Count - 1
            ProcessingCombos(i).Enabled = False
        Next

        For i = 1 To ProcessingLabels.Count - 1
            ProcessingLabels(i).Enabled = False
        Next

        If cmbRefining.Text = "4" Or cmbRefining.Text = "5" Then
            ' Veld, Scordite, Pyroxeres, and Plag
            Call EnableOreProcessingGroup(1, True)
            Call EnableOreProcessingGroup(2, True)
            Call EnableOreProcessingGroup(9, True)
            Call EnableOreProcessingGroup(10, True)
        End If

        If cmbRefining.Text = "5" Then
            ' Hemo, Jaspet, Kernite, Omber, Refinery Effy
            Call EnableOreProcessingGroup(3, True)
            Call EnableOreProcessingGroup(4, True)
            Call EnableOreProcessingGroup(11, True)
            Call EnableOreProcessingGroup(12, True)
        End If

        If cmbRefineryEff.Text = "4" Or cmbRefineryEff.Text = "5" Then
            ' Dark Ochre, Gneiss, Hedb, Spod
            Call EnableOreProcessingGroup(5, True)
            Call EnableOreProcessingGroup(6, True)
            Call EnableOreProcessingGroup(13, True)
            Call EnableOreProcessingGroup(14, True)
        End If

        If cmbRefineryEff.Text = "5" Then
            ' Ark, Bisot, Crokite, Mercoxit
            Call EnableOreProcessingGroup(7, True)
            Call EnableOreProcessingGroup(8, True)
            Call EnableOreProcessingGroup(15, True)
            Call EnableOreProcessingGroup(16, True)
            ' Moon mining

            Call EnableOreProcessingGroup(18, True)
            Call EnableOreProcessingGroup(19, True)
            Call EnableOreProcessingGroup(20, True)
            Call EnableOreProcessingGroup(21, True)
            Call EnableOreProcessingGroup(22, True)
            ' Trig mining
            Call EnableOreProcessingGroup(23, True)
            Call EnableOreProcessingGroup(24, True)
            Call EnableOreProcessingGroup(25, True)
        End If

        If cmbRefining.Text = "4" Then
            cmbRefineryEff.Enabled = True
        End If

        If cmbRefineryEff.Enabled And cmbRefineryEff.Text = "5" Then
            Call EnableOreProcessingGroup(17, True)
        End If

    End Sub

    ' Changes the ore processing skill group to enabled or disabled
    Private Sub EnableOreProcessingGroup(ByVal Index As Integer, ByVal EnableObject As Boolean)
        If ProcessingCheckBoxes(Index).Checked And EnableObject Then
            ' Ok to enable
            ProcessingCombos(Index).Enabled = True
            ProcessingLabels(Index).Enabled = True
        Else
            ' Don't enable
            ProcessingCombos(Index).Enabled = False
            ProcessingLabels(Index).Enabled = False
        End If

        ProcessingCheckBoxes(Index).Enabled = EnableObject

    End Sub

    Private Sub lstItemstoRefine_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lstItemstoRefine.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstItemstoRefine, ListView), ItemsColumnClicked, ItemsColumnSortType)
    End Sub

    Private Sub lstRefineOutput_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstRefineOutput.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstRefineOutput, ListView), OutputColumnClicked, OutputColumnSortType)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        Me.Hide()
    End Sub

    Private Sub lstItemstoRefine_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lstItemstoRefine.ItemChecked
        If Not IgnoreChecks Then
            Dim TempItem As RefineItem
            TempItem.BuildItem = e.Item.Checked
            TempItem.ItemID = CInt(e.Item.SubItems(7).Text)

            Dim FoundItem As RefineItem = GetItemInCheckedList(TempItem.ItemID)

            ' See if the item checked is in the list, if so, update the temp, remove the old items and replace
            If FoundItem.ItemID <> 0 Then
                ' In list, so just remove and re-add
                CheckedRefineItems.Remove(FoundItem)
                ' Add the item with current info
                CheckedRefineItems.Add(TempItem)
            Else
                ' New item to add, now add the item that was toggled
                Call CheckedRefineItems.Add(TempItem)
            End If

        End If
    End Sub

    ' Look in the checked item list and see if we check this item or leave it checked
    Private Function GetCheckforItem(ItemID As Long) As Boolean
        Dim FoundItem As RefineItem = GetItemInCheckedList(ItemID)

        If FoundItem.ItemID <> 0 Then
            Return FoundItem.BuildItem
        Else
            Return True
        End If
    End Function

    Private Function GetItemInCheckedList(ItemID As Long) As RefineItem
        Dim FoundItem As RefineItem
        RefineItemtoFind = ItemID
        FoundItem = CheckedRefineItems.Find(AddressOf FindBPBBItem)

        If FoundItem.ItemID <> 0 Then
            Return FoundItem
        Else
            Return Nothing
        End If
    End Function

    Private Sub chkToggle_CheckedChanged(sender As Object, e As EventArgs) Handles chkToggle.CheckedChanged
        Dim CheckValue As Boolean
        If chkToggle.Checked Then
            chkToggle.Text = "Uncheck All Items"
            CheckValue = True
        Else
            chkToggle.Text = "Check All Items"
            CheckValue = False
        End If

        If Not IgnoreChecks Then
            For Each item As ListViewItem In Me.lstItemstoRefine.Items
                item.Checked = CheckValue
            Next
        End If
    End Sub

#End Region

    Private Sub btnShowAssets_Click(sender As Object, e As EventArgs) Handles btnShowAssets.Click

        ' Make sure it's not disposed
        If IsNothing(frmRefineryAssets) Then
            ' Make new form
            frmRefineryAssets = New frmAssetsViewer(AssetWindow.Refinery)
        Else
            If frmRefineryAssets.IsDisposed Then
                ' Make new form
                frmRefineryAssets = New frmAssetsViewer(AssetWindow.Refinery)
            End If
        End If

        ' Now open the Asset List
        frmRefineryAssets.Show()
        frmRefineryAssets.Focus()

        Application.DoEvents()

    End Sub

    Private Sub btnCopyPasteAssets_Click(sender As Object, e As EventArgs) Handles btnCopyPasteAssets.Click
        Dim f1 As New frmCopyandPaste(CopyPasteWindowType.Materials, CopyPasteWindowLocation.RefineMaterials)

        f1.ShowDialog()

        ' Update with new materials
        If CopyPasteRefineryMaterialText <> "" Then
            Call RefreshMaterialList(ImportCopyPasteText(CopyPasteRefineryMaterialText))
        End If

        f1.Dispose()

    End Sub

    Private Sub btnSelectAssets_Click(sender As Object, e As EventArgs) Handles btnSelectAssets.Click
        ' They update assets already by pressing on the safe
        CopyPasteRefineryMaterialText = "" ' Reset this
        Call RefreshMaterialList()
    End Sub

    ' Refines all materials in the item list and updates the item list with return amount of refining and the output list of materials
    Private Sub btnRefine_Click(sender As Object, e As EventArgs) Handles btnRefine.Click
        Call Refine()
    End Sub

    Private Sub btnRefine2_Click(sender As Object, e As EventArgs) Handles btnRefine2.Click
        Call Refine()
    End Sub

    Private Sub Refine()
        Dim ReprocessedMaterials As New Materials
        Dim ReprocessedCost As Double
        Dim ReprocessingUsage As Double
        Dim TotalReprocessingUsage As Double = 0
        Dim ItemCost As Double
        Dim ItemlstViewRow As ListViewItem
        Dim TotalItemListValue As Double = 0

        If lstItemstoRefine.CheckedItems.Count = 0 Then
            MsgBox("No items selected to refine.", vbExclamation, Application.ProductName)
            Exit Sub
        End If

        ' Reset the output
        MaterialOutput = New Materials

        ' Read through the list, refine each material 
        lstItemstoRefine.BeginUpdate()

        For Each Item As ListViewItem In Me.lstItemstoRefine.Items
            If Item.Checked Then

                Dim ReprocessingYield As Double = 0

                TotalItemListValue += CDbl(Item.SubItems.Item(2).Text)
                Dim ItemGroup As String = Item.SubItems.Item(6).Text

                ' Refine the item
                With Item.SubItems
                    Call ReprocessMaterial(CInt(.Item(7).Text), .Item(0).Text, CInt(.Item(1).Text), .Item(6).Text, chkRecursiveRefine.Checked,
                                                             ReprocessedMaterials, ReprocessingYield, ReprocessingUsage)
                End With

                ' Save the processing cost
                TotalReprocessingUsage += ReprocessingUsage

                ' Update the refine rate to the current row, refined cost, and loss % of the refine
                Item.SubItems.Item(3).Text = FormatPercent(ReprocessingYield, 1)
                ReprocessedCost = ReprocessedMaterials.GetTotalMaterialsCost
                Item.SubItems.Item(4).Text = FormatNumber(ReprocessedCost, 2)
                ItemCost = CDbl(Item.SubItems.Item(2).Text)
                If ItemCost = 0 Then
                    Item.SubItems.Item(5).Text = FormatPercent(1, 1)
                Else
                    Item.SubItems.Item(5).Text = FormatPercent(ReprocessedCost / ItemCost, 1)
                End If

                ' Add the materials to the main material list
                Call MaterialOutput.InsertMaterialList(ReprocessedMaterials.GetMaterialList)
                Application.DoEvents()
            Else
                ' Clear the output data 
                Item.SubItems.Item(3).Text = ""
                Item.SubItems.Item(4).Text = ""
                Item.SubItems.Item(5).Text = ""
            End If
        Next
        lstItemstoRefine.EndUpdate()

        ' Update the total usage for doing this refining
        ReprocessingFacility.GetSelectedFacility.FacilityUsage = TotalReprocessingUsage

        ' Now update the main output list
        lstRefineOutput.Items.Clear()
        lstRefineOutput.BeginUpdate()
        For Each mat In MaterialOutput.GetMaterialList
            ItemlstViewRow = New ListViewItem(mat.GetMaterialName)
            ItemlstViewRow.SubItems.Add(FormatNumber(mat.GetQuantity, 0))
            ItemlstViewRow.SubItems.Add(FormatNumber(mat.GetCostPerItem, 2))
            ItemlstViewRow.SubItems.Add(FormatNumber(mat.GetTotalCost, 2))
            ItemlstViewRow.SubItems.Add(FormatNumber(mat.GetTotalVolume, 2))
            Call lstRefineOutput.Items.Add(ItemlstViewRow)
        Next
        lstRefineOutput.EndUpdate()

        ' Update the total values for the two lists and rate
        Dim TotalValue = MaterialOutput.GetTotalMaterialsCost - TotalReprocessingUsage  ' Subtract usage first
        lblListTotalValueOutput.Text = FormatNumber(TotalItemListValue, 2) ' Total value of the items reprocessed
        lblReturnRatePercentOutput.Text = FormatPercent(TotalValue / TotalItemListValue, 1) ' Amount of stuff recieved / total value of stuff reprocessed
        lblReprocessingValueOutput.Text = FormatNumber(TotalValue, 2) ' Total value of stuff reprocessed minus usage
        lblReprocessingVolumeOutput.Text = FormatNumber(MaterialOutput.GetTotalVolume, 2) ' Total volume of output stuff

        ' Sort the  list
        Call ListViewColumnSorter(OutputColumnClicked, CType(lstRefineOutput, ListView), OutputColumnClicked, OutputColumnSortType)

        Application.DoEvents()

    End Sub

    Private Sub ReprocessMaterial(ByVal ItemID As Long, ByVal ItemName As String, ByVal ItemQuantity As Long, ByVal ItemGroup As String,
                                  ByVal RecursiveRefine As Boolean, ByRef MaterialOutputs As Materials, ByRef ReprocessingYieldOutput As Double,
                                  ByRef ReprocessingFees As Double)
        Dim BFI As New BrokerFeeInfo
        Dim TempOutputs As New Materials
        Dim RecursiveOutput As New Materials
        Dim UpdatedOutputs As New Materials
        Dim ReprocessingYield As Double
        Dim ReprocessingUsage As Double
        Dim LocalReprocessingUsage As Double = 0

        ' These will only set up base refine rates, we need to adjust with the rig updated rates
        Dim ReprocessingStation As New ReprocessingPlant(ReprocessingFacility.GetFacility(ProductionType.Reprocessing), GetAttribute("refiningYieldMutator", cmbBeanCounterRefining.Text) / 100)

        ' Update the material modifier based on the type of ore
        If ItemGroup.Contains("Moon") Then
            ReprocessingStation.GetFacilility.MaterialMultiplier = ReprocessingStation.GetFacilility.MoonOreFacilityRefineRate
        ElseIf ItemGroup = "Ore" Then
            ReprocessingStation.GetFacilility.MaterialMultiplier = ReprocessingStation.GetFacilility.OreFacilityRefineRate
        ElseIf ItemGroup = "Ice" Then
            ReprocessingStation.GetFacilility.MaterialMultiplier = ReprocessingStation.GetFacilility.IceFacilityRefineRate
        ElseIf ItemGroup = "Scrap" Then
            ReprocessingStation.GetFacilility.MaterialMultiplier = ReprocessingStation.GetFacilility.ScrapmetalRefineRate
        End If

        Dim RefineryEfficency As Integer = 0

        If cmbRefineryEff.Enabled = True Then
            RefineryEfficency = CInt(cmbRefineryEff.Text)
        End If

        ' Refine the first item
        TempOutputs = ReprocessingStation.Reprocess(ItemID, CInt(cmbRefining.Text), RefineryEfficency, GetProcessingSkill(ItemName, ItemGroup),
                                                    ItemQuantity, False, BFI, ReprocessingYield, ReprocessingUsage)
        LocalReprocessingUsage = ReprocessingUsage

        ' If the items returned can be further refined, and we want to recursively refine, then send again
        If RecursiveRefine Then
            ' For each refined item, see if it can be refined further
            Dim rsRefine As SQLiteDataReader

            For Each Mat In TempOutputs.GetMaterialList
                DBCommand = New SQLiteCommand("SELECT 'X' FROM REPROCESSING WHERE ITEM_ID = " & CStr(Mat.GetMaterialTypeID), EVEDB.DBREf)
                rsRefine = DBCommand.ExecuteReader
                rsRefine.Read()

                If rsRefine.HasRows Then
                    ' Refine the item again and add it's materials to the main list
                    Call ReprocessMaterial(Mat.GetMaterialTypeID, Mat.GetMaterialName, Mat.GetQuantity, ItemGroup,
                                           chkRecursiveRefine.Checked, RecursiveOutput, ReprocessingYield, ReprocessingUsage)
                    ' Add the final refined output to the main list
                    UpdatedOutputs.InsertMaterialList(RecursiveOutput.GetMaterialList)
                    ' Save usage
                    LocalReprocessingUsage += ReprocessingUsage
                Else
                    UpdatedOutputs.InsertMaterial(Mat)
                End If
            Next
            TempOutputs = UpdatedOutputs
        End If

        MaterialOutputs = TempOutputs
        ReprocessingYieldOutput = ReprocessingYield
        ReprocessingFees = LocalReprocessingUsage

    End Sub

    ' Updates the refine list if we are sent materials or if not, looking up in the DB for assets
    Public Sub RefreshMaterialList(Optional PasteMaterialList As Materials = Nothing)
        Dim SQL As String = ""
        Dim readerItems As SQLiteDataReader
        Dim ItemlstViewRow As ListViewItem
        Dim CleanMaterialList As New Materials
        Dim TempMaterialList As New Materials
        Dim TempMaterial As Material
        Dim ProcessMat As Material

        Application.UseWaitCursor = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If IsNothing(PasteMaterialList) And CopyPasteRefineryMaterialText <> "" Then
            ' They refreshed prices most likely so use the original copy paste list they set
            PasteMaterialList = ImportCopyPasteText(CopyPasteRefineryMaterialText)
        End If

        Dim IDString As String = ""

        If IsNothing(PasteMaterialList) Then
            ' Read all the assets into the list as selected
            ' Set the ID string we will use to update
            If UserAssetWindowRefinerySettings.AssetType = "Both" Then
                IDString = CStr(SelectedCharacter.ID) & "," & CStr(SelectedCharacter.CharacterCorporation.CorporationID)
            ElseIf UserAssetWindowRefinerySettings.AssetType = "Personal" Then
                IDString = CStr(SelectedCharacter.ID)
            ElseIf UserAssetWindowRefinerySettings.AssetType = "Corporation" Then
                IDString = CStr(SelectedCharacter.CharacterCorporation.CorporationID)
            End If

            ' Build the where clause to look up data
            Dim AssetLocationFlagList As String = ""
            ' First look up the location and flagID pairs - unique ID of asset locations
            SQL = "SELECT LocationID, FlagID FROM ASSET_LOCATIONS WHERE EnumAssetType = " & CStr(AssetWindow.Refinery) & " AND ID IN (" & IDString & ")"
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerItems = DBCommand.ExecuteReader

            While readerItems.Read
                If (readerItems.GetInt32(1) = -4 Or readerItems.GetInt64(0) > 1000000000000) Then
                    ' If the flag is the base location, then we want all items at the location id
                    AssetLocationFlagList &= "(LocationID = " & CStr(readerItems.GetInt64(0)) & ") OR "
                Else
                    AssetLocationFlagList &= "(LocationID = " & CStr(readerItems.GetInt64(0)) & " AND Flag = " & CStr(readerItems.GetInt32(1)) & ") OR "
                End If
            End While

            readerItems.Close()

            If AssetLocationFlagList = "" Then
                MsgBox("You do not have an asset location selected", vbInformation, Application.ProductName)
                Application.UseWaitCursor = False
                Me.Cursor = Cursors.Default
                Application.DoEvents()
                Exit Sub
            Else
                ' Strip the last OR
                AssetLocationFlagList = AssetLocationFlagList.Substring(0, Len(AssetLocationFlagList) - 4)
            End If

            ' Now get all the assets from the checked locations
            SQL = "SELECT IT.typeID, IT.typeName, SUM(Quantity), CASE WHEN IT.volume IS NULL THEN 1 ELSE IT.volume END FROM "
            SQL &= "ASSETS, INVENTORY_TYPES AS IT "
            SQL &= "WHERE (" & AssetLocationFlagList & ") "
            SQL &= "AND IT.typeID = ASSETS.TypeID "
            SQL &= "AND ID IN (" & IDString & ") "
            SQL &= "GROUP BY IT.typeID, IT.typeName, IT.volume "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerItems = DBCommand.ExecuteReader

            While readerItems.Read
                ' Add each material to the temp list
                With readerItems
                    TempMaterial = New Material(.GetInt64(0), .GetString(1), "", .GetInt64(2), .GetDouble(3), 0, "", "")
                End With

                Call TempMaterialList.InsertMaterial(TempMaterial)

            End While
        Else
            TempMaterialList = PasteMaterialList
        End If

        ' First, only add items to the list that we can refine - filter out all the other junk
        For Each Mat In TempMaterialList.GetMaterialList
            SQL = "SELECT ITEM_ID, BELT_TYPE FROM REPROCESSING LEFT JOIN ORES ON REPROCESSING.ITEM_ID = ORES.ORE_ID "
            SQL &= "WHERE ITEM_ID =" & CStr(Mat.GetMaterialTypeID) & " GROUP BY ITEM_ID, BELT_TYPE "
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerItems = DBCommand.ExecuteReader

            If readerItems.Read Then
                ' Adjust GroupName for item type to set the refine rate with
                If Not IsDBNull(readerItems.GetValue(1)) Then
                    Mat.GroupName = readerItems.GetString(1)
                Else
                    Mat.GroupName = "Scrap"
                End If
                ' Found - add to list
                CleanMaterialList.InsertMaterial(Mat)
            End If
        Next

        ' Read through the list and insert the items, quantity, total value of the items
        IgnoreChecks = True
        lstItemstoRefine.Items.Clear()
        lstItemstoRefine.BeginUpdate()
        lstItemstoRefine.Enabled = False
        For Each ProcessMat In CleanMaterialList.GetMaterialList
            ItemlstViewRow = New ListViewItem(ProcessMat.GetMaterialName)
            ItemlstViewRow.SubItems.Add(FormatNumber(ProcessMat.GetQuantity, 0))
            ItemlstViewRow.SubItems.Add(FormatNumber(ProcessMat.GetTotalCost, 2))
            ItemlstViewRow.SubItems.Add("-")
            ItemlstViewRow.SubItems.Add("-")
            ItemlstViewRow.SubItems.Add("-")
            ItemlstViewRow.SubItems.Add(ProcessMat.GroupName)
            ItemlstViewRow.SubItems.Add(CStr(ProcessMat.GetMaterialTypeID))
            ' Check the row if in not in the list
            ItemlstViewRow.Checked = GetCheckforItem(ProcessMat.GetMaterialTypeID)
            Call lstItemstoRefine.Items.Add(ItemlstViewRow)
            Application.DoEvents()
        Next
        lstItemstoRefine.EndUpdate()
        lstItemstoRefine.Enabled = True
        IgnoreChecks = False

        ' Sort the  list
        Call ListViewColumnSorter(ItemsColumnClicked, CType(lstItemstoRefine, ListView), ItemsColumnClicked, SortOrder.Ascending)

        Application.UseWaitCursor = False
        Me.Cursor = Cursors.Default
        ' Play notification sound
        Call PlayNotifySound()
        Application.DoEvents()

    End Sub

    Public Sub RefreshRefiningRates()
        With ReprocessingFacility.GetSelectedFacility
            ' These are bases rate without processing skills or implant
            lblOreRate.Text = FormatPercent(.OreFacilityRefineRate, 2)
            lblMoonRate.Text = FormatPercent(.MoonOreFacilityRefineRate, 2)
            lblIceRate.Text = FormatPercent(.IceFacilityRefineRate, 2)
            lblScrapRate.Text = FormatPercent(.ScrapmetalRefineRate, 2)
        End With
    End Sub

    ' Returns the ore processing skill level on the screen for the ore name sent
    Private Function GetProcessingSkill(ByVal ItemName As String, ByVal ItemGroup As String) As Integer
        Dim i As Integer
        Dim CurrentProcessingLabel As String

        If ItemGroup = "Ice" Then
            Return CInt(cmbOreProcessing17.Text)
        ElseIf ItemGroup = "Scrap" Then
            Return CInt(cmbScrapMetalProcessing.Text)
        End If

        If ItemName.Contains("Ochre") Then
            ItemName = "Dark Ochre"
        End If

        For i = 1 To ProcessingCombos.Count - 1
            CurrentProcessingLabel = ProcessingLabels(i).Text

            ' Special processing for Dark Ochre
            If CurrentProcessingLabel = "Dark" Then
                CurrentProcessingLabel = "Dark Ochre"
            End If

            If ProcessingCombos(i).Enabled = True And CBool(InStr(ItemName, CurrentProcessingLabel)) Then
                ' Found it, return value
                Return CInt(ProcessingCombos(i).Text)
            Else
                ' If it didn't find it, it might be a moon ore - look up the type and check each of those
                Dim rsCheck As SQLiteDataReader
                DBCommand = New SQLiteCommand("SELECT groupName FROM INVENTORY_TYPES AS IT, INVENTORY_GROUPS AS IG WHERE IT.groupID = IG.groupID and typeName = '" & ItemName & "'", EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    If rsCheck.GetString(0).Contains(" ") Then
                        If CurrentProcessingLabel.Contains(rsCheck.GetString(0).Substring(0, InStr(rsCheck.GetString(0), " ") - 1)) Then
                            Return CInt(ProcessingCombos(i).Text)
                        End If
                    End If
                End If
                rsCheck.Close()
                DBCommand = Nothing
            End If
        Next

        Return 0

    End Function

    Private Sub chkRecursiveRefine_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecursiveRefine.CheckedChanged

        UserApplicationSettings.RefineDrillDown = chkRecursiveRefine.Checked
        ' Save it
        Call AllSettings.SaveApplicationSettings(UserApplicationSettings)

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call ClearItems()
    End Sub

    Private Sub btnClear2_Click(sender As Object, e As EventArgs) Handles btnClear2.Click
        Call ClearItems()
    End Sub

    Private Sub ClearItems()
        lstItemstoRefine.Items.Clear()
        lstRefineOutput.Items.Clear()

        lblListTotalValueOutput.Text = "-"
        lblReturnRatePercentOutput.Text = "-"
        lblReprocessingValueOutput.Text = "-"
        lblReprocessingVolumeOutput.Text = "-"

    End Sub

    Private Sub btnCopyOutput_Click(sender As Object, e As EventArgs) Handles btnCopyOutput.Click
        Call CopyOutput()
    End Sub

    Private Sub btnCopyOutput2_Click(sender As Object, e As EventArgs) Handles btnCopyOutput2.Click
        Call CopyOutput()
    End Sub

    Private Sub CopyOutput()
        Dim ClipboardData As New DataObject

        Call MaterialOutput.SortMaterialListByQuantity()

        ' Paste to clipboard
        Call CopyTextToClipboard(MaterialOutput.GetClipboardList(UserApplicationSettings.DataExportFormat, False, False, False, False))

    End Sub

    Private Sub frmReprocessingPlant_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        ReprocessingPlantOpen = False
    End Sub

    Private Sub cmbRefining_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRefining.SelectedIndexChanged
        If CInt(cmbRefining.Text) >= 4 Then
            cmbRefineryEff.Enabled = True
        Else
            cmbRefineryEff.Enabled = False
        End If
        ' Update the ore processing skills
        Call UpdateProcessingSkills()
    End Sub

    Private Sub cmbRefineryEff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRefineryEff.SelectedIndexChanged
        ' Update the ore processing skills
        Call UpdateProcessingSkills()
    End Sub
End Class