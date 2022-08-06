
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
        cmbReprocessing.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(3385))
        cmbReprocessingEff.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(3389))
        cmbScrapMetalProcessing.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(12196))

        Dim TempSkillLevel As Integer
        For i = 1 To ProcessingCheckBoxes.Count - 1
            TempSkillLevel = SelectedCharacter.Skills.GetSkillLevel(SelectedCharacter.Skills.GetSkillTypeID(ProcessingLabels(i).Text))
            If TempSkillLevel <> 0 Then
                ProcessingCombos(i).Text = CStr(TempSkillLevel)
                ProcessingCheckBoxes(i).Checked = True
            Else
                ProcessingCombos(i).Text = "0"
                ProcessingCheckBoxes(i).Checked = False
            End If
        Next

        Dim AttribLookup As New EVEAttributes
        Dim Defaults As New ProgramSettings
        Select Case UserApplicationSettings.RefiningImplantValue
            Case (AttribLookup.GetAttribute(Defaults.RBeanCounterName & "1", ItemAttributes.refiningYieldMutator) / 100)
                cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "1"
            Case (AttribLookup.GetAttribute(Defaults.RBeanCounterName & "2", ItemAttributes.refiningYieldMutator) / 100)
                cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "2"
            Case (AttribLookup.GetAttribute(Defaults.RBeanCounterName & "4", ItemAttributes.refiningYieldMutator) / 100)
                cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "4"
            Case Else
                cmbBeanCounterRefining.Text = None
        End Select

        chkRecursiveRefine.Checked = UserApplicationSettings.RefineDrillDown

        ' Update the ore processing skills
        Call UpdateProcessingSkills()

        Call InitializeReprocessingFacility()
        Call RefreshRefiningRates()

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
                                                                                                                      chkOreProcessing10.CheckedChanged, chkOreProcessing11.CheckedChanged, chkOreProcessing12.CheckedChanged


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

        If cmbReprocessing.Text = "4" Or cmbReprocessing.Text = "5" Then
            ' Simple - Veld, Scordite, Pyroxeres, and Plag
            Call EnableOreProcessingGroup(1, True)
        End If

        If cmbReprocessing.Text = "5" Then
            ' Coherent - Hemo, Hedbergite, Jaspet, Kernite, Omber
            Call EnableOreProcessingGroup(2, True)
        End If

        If cmbReprocessingEff.Text = "4" Or cmbReprocessingEff.Text = "5" Then
            ' Variegated - Crokite, Dark Ochre, Gneiss
            Call EnableOreProcessingGroup(3, True)
        End If

        If cmbReprocessingEff.Text = "5" Then
            ' Complex - Ark, Bisot, Spod
            Call EnableOreProcessingGroup(4, True)
            ' Mercoxit
            Call EnableOreProcessingGroup(6, True)
            ' Moon mining
            Call EnableOreProcessingGroup(7, True)
            Call EnableOreProcessingGroup(8, True)
            Call EnableOreProcessingGroup(9, True)
            Call EnableOreProcessingGroup(10, True)
            Call EnableOreProcessingGroup(11, True)
            ' Trig mining
            Call EnableOreProcessingGroup(5, True)
        End If

        If cmbReprocessing.Text = "4" Then
            cmbReprocessingEff.Enabled = True
        End If

        ' Ice
        If cmbReprocessingEff.Enabled And cmbReprocessingEff.Text = "5" Then
            Call EnableOreProcessingGroup(12, True)
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
    Private Sub btnReprocess_Click(sender As Object, e As EventArgs) Handles btnReprocess.Click
        Call Reprocess()
    End Sub

    Private Sub btnReprocess2_Click(sender As Object, e As EventArgs) Handles btnReprocess2.Click
        Call Reprocess()
    End Sub

    Private Sub Reprocess()
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
        Dim Attriblookup As New EVEAttributes
        Dim ImplantValue As Double = Attriblookup.GetAttribute(cmbBeanCounterRefining.Text, ItemAttributes.refiningYieldMutator) / 100
        Dim ReprocessingStation As New ReprocessingPlant(ReprocessingFacility.GetFacility(ProductionType.Reprocessing), ImplantValue)

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

        If cmbReprocessingEff.Enabled = True Then
            RefineryEfficency = CInt(cmbReprocessingEff.Text)
        End If

        ' Refine the first item
        TempOutputs = ReprocessingStation.Reprocess(ItemID, CInt(cmbReprocessing.Text), RefineryEfficency, GetProcessingSkill(ItemName, ItemGroup),
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
        If ItemGroup = "Ice" Then
            Return CInt(cmbOreProcessing12.Text)
        ElseIf ItemGroup = "Scrap" Then
            Return CInt(cmbScrapMetalProcessing.Text)
        End If

        ' It's an ore, so just return the processing value
        Return GetFormOreProcessingSkill(ItemName, ProcessingLabels, ProcessingCombos)

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

    Private Sub cmbRefining_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReprocessing.SelectedIndexChanged
        If CInt(cmbReprocessing.Text) >= 4 Then
            cmbReprocessingEff.Enabled = True
        Else
            cmbReprocessingEff.Enabled = False
        End If
        ' Update the ore processing skills
        Call UpdateProcessingSkills()
    End Sub

    Private Sub cmbRefineryEff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReprocessingEff.SelectedIndexChanged
        ' Update the ore processing skills
        Call UpdateProcessingSkills()
    End Sub

End Class