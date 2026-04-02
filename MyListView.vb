Imports System.Data.SQLite

Public Class MyListView
    Inherits ListView

    Public Event ProcMsg(ByVal m As Message)
    Public Const WM_VSCROLL As Integer = 277

    Private WithEvents txtListEditBox As TextBox
    Private WithEvents cmbEditBox As ComboBox

    ' Inline grid row update variables
    Private Structure SavedLoc
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Private SavedListClickLoc As SavedLoc
    Private RefreshingGrid As Boolean

    Private CurrentRow As ListViewItem
    Private PreviousRow As ListViewItem
    Private NextRow As ListViewItem
    Private NextCellRow As ListViewItem
    Private PreviousCellRow As ListViewItem
    Private CurrentCell As ListViewItem.ListViewSubItem
    Private PreviousCell As ListViewItem.ListViewSubItem
    Private NextCell As ListViewItem.ListViewSubItem

    Private PriceUpdate As Boolean
    Private QuantityUpdate As Boolean
    Private DataUpdated As Boolean
    Private DataEntered As Boolean

    Private PriceTypeUpdate As Boolean
    Private PriceSystemUpdate As Boolean
    Private PriceRegionUpdate As Boolean
    Private PriceModifierUpdate As Boolean
    Private PreviousPriceType As String
    Private PreviousRegion As String
    Private PreviousSystem As String
    Private PreviousPriceMod As String
    Private TabPressed As Boolean
    Private UpdatingCombo As Boolean

    Private IgnoreFocus As Boolean
    Private IgnoreMarketFocus As Boolean

    ' For BP management
    Private MEUpdate As Boolean
    Private TEUpdate As Boolean
    Private FavoriteUpdate As Boolean
    Private OwnedTypeUpdate As Boolean
    Private IgnoredBPUpdate As Boolean
    Private BPTypeUpdate As Boolean

    Private Const BPRaw As String = "lstBPRawMats"
    Private Const BPComponents As String = "lstBPComponentMats"
    Private Const UpdatePrices As String = "lstPricesView"
    Private Const RawPriceProfile As String = "lstRawPriceProfile"
    Private Const ManufacturedPriceProfile As String = "lstManufacturedPriceProfile"
    Private Const RefineryOutput As String = "lstRefineOutput"
    Private Const RefineryItems As String = "lstItemstoRefine"
    Private Const BPManagement As String = "lstBPs"
    Private Const InventionMats As String = "lstInventionMats"

    Private Const ShoppingListItems As String = "lstItems"
    Private Const ShoppingListBuy As String = "lstBuy"
    Private Const ShoppingListBuild As String = "lstBuild"

    Public Sub New()

        Call InitializeComponent()

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        txtListEditBox = New TextBox
        Me.SuspendLayout()
        Me.Controls.Add(txtListEditBox)
        txtListEditBox.SetBounds(5, 5, 20, 20)
        txtListEditBox.Hide()

        cmbEditBox = New ComboBox
        cmbEditBox.DropDownStyle = ComboBoxStyle.DropDownList
        Me.SuspendLayout()
        Me.Controls.Add(cmbeditBox)
        cmbeditBox.SetBounds(5, 5, 20, 20)
        cmbeditBox.Hide()

    End Sub

    ' Determines where to show the text box when clicking on the list sent
    Private Sub MyListView_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Dim iSubIndex As Integer = 0

        ' Hide the text box when a new line is selected
        txtListEditBox.Hide()
        cmbeditBox.Hide()

        CurrentRow = Me.GetItemAt(e.X, e.Y) ' which listviewitem was clicked

        If CurrentRow Is Nothing Then
            Exit Sub
        End If

        CurrentCell = CurrentRow.GetSubItemAt(e.X, e.Y)  ' which subitem was clicked

        ' Determine where the previous and next item boxes will be based on what they clicked - used in tab event handling
        Call SetNextandPreviousCells()

        ' See which column has been clicked
        iSubIndex = CurrentRow.SubItems.IndexOf(CurrentCell)

        Select Case Me.Name
            Case BPRaw, BPComponents
                ' Set the columns that can be edited, just ME and Price
                If iSubIndex = 2 Or iSubIndex = 3 Then

                    If iSubIndex = 2 Then
                        MEUpdate = True
                    Else
                        MEUpdate = False
                    End If

                    If iSubIndex = 3 Then
                        PriceUpdate = True
                    Else
                        PriceUpdate = False
                    End If

                    ' For the update grids in the Blueprint Tab, only show the box if
                    ' 1 - If the ME is clicked and it has something other than a '-' in it (meaning no BP)
                    ' 2 - If the Price is clicked and the ME box has '-' in it
                    If (CurrentRow.SubItems(2).Text <> "-" And MEUpdate) Or PriceUpdate Then
                        Call ShowEditBox()
                    End If

                End If
            Case UpdatePrices, RefineryOutput, RefineryItems, InventionMats
                ' Set the columns that can be edited, just Price
                If ((Me.Name = RefineryItems Or Me.Name = InventionMats) And iSubIndex = 2) Or (Me.Name <> RefineryItems And Me.Name <> InventionMats And iSubIndex = 3) Then
                    Call ShowEditBox()
                    PriceUpdate = True
                End If
            Case RawPriceProfile, ManufacturedPriceProfile
                If iSubIndex > 0 Then
                    ' Reset update type
                    Call SetPriceProfileVariables(iSubIndex)
                    Call ShowEditBox()
                End If
            Case BPManagement
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

                    Call ShowEditBox()

                ElseIf iSubIndex = 8 Then ' Owned Type

                    MEUpdate = False
                    TEUpdate = False
                    FavoriteUpdate = False
                    IgnoredBPUpdate = False
                    OwnedTypeUpdate = True

                    Call ShowEditBox()

                ElseIf iSubIndex = 9 Then ' Favorite

                    MEUpdate = False
                    TEUpdate = False
                    FavoriteUpdate = True
                    IgnoredBPUpdate = False
                    OwnedTypeUpdate = False

                    Call ShowEditBox()

                ElseIf iSubIndex = 10 Then ' Ignored

                    MEUpdate = False
                    TEUpdate = False
                    FavoriteUpdate = False
                    IgnoredBPUpdate = True
                    OwnedTypeUpdate = False

                    Call ShowEditBox()

                Else
                    ' Not updatable so leave
                    Exit Sub
                End If
            Case ShoppingListBuild, ShoppingListBuy, ShoppingListItems
                ' Look at the buy list for price and quantity
                If Me.Name = ShoppingListBuy Then
                    ' Set the columns that can be edited, just Quantity and Price
                    Select Case iSubIndex
                        Case 2
                            QuantityUpdate = True
                            PriceUpdate = False
                            Call ShowEditBox()
                        Case 3
                            QuantityUpdate = False
                            PriceUpdate = True
                            Call ShowEditBox()
                        Case Else
                            QuantityUpdate = False
                            PriceUpdate = False
                    End Select

                Else
                    ' Just quantity for the other 2
                    If iSubIndex = 2 Then
                        QuantityUpdate = True
                        PriceUpdate = False
                        Call ShowEditBox()
                    Else
                        QuantityUpdate = False
                        PriceUpdate = False
                    End If
                End If
        End Select

    End Sub

    ' Shows the text/combo box on the grid where clicked if enabled
    Private Sub ShowEditBox(Optional TextBoxAlignment As HorizontalAlignment = HorizontalAlignment.Right)
        Dim RowIndex As Integer = 0

        ' Save the center location of the edit box
        SavedListClickLoc.X = CurrentCell.Bounds.Left + CInt(CurrentCell.Bounds.Width / 2)
        SavedListClickLoc.Y = CurrentCell.Bounds.Top + CInt(CurrentCell.Bounds.Height / 2)

        ' Get the boundry data for the control now
        Dim pLeft As Integer = CurrentCell.Bounds.Left
        Dim pTop As Integer = CurrentCell.Bounds.Top - 1

        Select Case Me.Name
            Case BPComponents
                If Me.Columns(CurrentRow.SubItems.IndexOf(CurrentCell)).Text = "ME" Then
                    Call ShowTextBox(pLeft, pTop, HorizontalAlignment.Center)
                Else
                    Call ShowTextBox(pLeft, pTop, TextBoxAlignment)
                End If
            Case BPRaw, UpdatePrices, RefineryOutput, RefineryItems, InventionMats, ShoppingListItems, ShoppingListBuild, ShoppingListBuy
                Call ShowTextBox(pLeft, pTop, TextBoxAlignment)
            Case ManufacturedPriceProfile, RawPriceProfile
                If PriceModifierUpdate Then
                    ' Show the text box
                    Call ShowTextBox(pLeft, pTop, TextBoxAlignment)
                Else ' Show combo box
                    With cmbEditBox
                        UpdatingCombo = True
                        .Hide()

                        If PriceRegionUpdate Then
                            Call LoadRegionCombo(cmbEditBox, CurrentCell.Text)
                        Else
                            .BeginUpdate()
                            .Items.Clear()
                            Dim rsData As SQLiteDataReader
                            Dim SQL As String = ""

                            If PriceSystemUpdate Then
                                ' Base it off the data in the region cell
                                SQL = "SELECT solarSystemName FROM SOLAR_SYSTEMS, REGIONS "
                                SQL &= "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "
                                SQL &= "AND REGIONS.regionName = '" & PreviousCell.Text & "' "
                                SQL &= "ORDER BY solarSystemName"
                                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                                rsData = DBCommand.ExecuteReader

                                ' Add all systems if it's the system
                                .Items.Add(AllSystems)
                                While rsData.Read
                                    .Items.Add(rsData.GetString(0))
                                    ' Special processing for The Forge
                                    If PreviousCell.Text = "The Forge" And rsData.GetString(0) = "Jita" Then
                                        .Items.Add(JitaPerimeter)
                                    End If
                                End While

                                rsData.Close()

                            ElseIf PriceTypeUpdate Then
                                ' Manually enter these
                                .Items.Add("Min Sell")
                                .Items.Add("Max Sell")
                                .Items.Add("Avg Sell")
                                .Items.Add("Median Sell")
                                .Items.Add("Percentile Sell")
                                .Items.Add("Min Buy")
                                .Items.Add("Max Buy")
                                .Items.Add("Avg Buy")
                                .Items.Add("Median Buy")
                                .Items.Add("Percentile Buy")
                                .Items.Add("Split Price")
                            End If
                            .EndUpdate()
                        End If

                        ' Set the bounds of the control
                        .SetBounds(pLeft, pTop, CurrentCell.Bounds.Width, CurrentCell.Bounds.Height)
                        .Text = CurrentCell.Text
                        .Show()
                        .Focus()

                        DataEntered = False ' We just updated so reset
                        UpdatingCombo = False
                    End With
                    txtListEditBox.Visible = False
                End If

            Case BPManagement
                If MEUpdate Or TEUpdate Then
                    Call ShowTextBox(pLeft, pTop, TextBoxAlignment)
                Else ' OwnedType/Favorites/Ignore
                    With cmbEditBox
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
                        .SetBounds(pLeft, pTop, CurrentCell.Bounds.Width, CurrentCell.Bounds.Height)
                        .Text = CurrentCell.Text
                        DataEntered = False ' We just updated so reset
                        .Show()
                        .Focus()

                    End With
                    txtListEditBox.Visible = False
                End If

        End Select
    End Sub

    ' For updating the items in the list by clicking on them
    Private Sub ProcessKeyDownEdit(SentKey As Keys)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader

        Dim MEValue As String = ""
        Dim TEValue As String = ""
        Dim FavoriteValue As String
        Dim OwnedTypeValue As String
        Dim IgnoredValue As String
        Dim TempBPType As BPType
        Dim UpdatedBPType As BPType
        Dim SetasFavorite As Boolean
        Dim SetasIgnore As Boolean

        Dim PriceValue As Double = 0
        Dim QuantityValue As Integer = 0
        Dim PriceUpdated As Boolean = False

        ' Change blank entry to 0
        If Trim(txtListEditBox.Text) = "" Then
            txtListEditBox.Text = "0"
        End If

        DataUpdated = False

        ' If they hit enter or tab away, mark the BP as owned in the DB with the values entered
        If (SentKey = Keys.Enter Or SentKey = Keys.ShiftKey Or SentKey = Keys.Tab) And DataEntered Then

            ' Check the input first
            If Not IsNumeric(txtListEditBox.Text) And MEUpdate Then
                MsgBox("Invalid ME Value", vbExclamation)
                Exit Sub
            End If

            If Not IsNumeric(txtListEditBox.Text) And PriceUpdate Then
                MsgBox("Invalid Price Value", vbExclamation)
                Exit Sub
            End If

            ' Save the data depending on what we are updating
            If MEUpdate Then
                MEValue = txtListEditBox.Text
            End If

            If PriceUpdate Then
                PriceValue = CDbl(txtListEditBox.Text)
            End If

            Select Case Me.Name
                Case BPRaw, BPComponents
                    ' Check the numbers, if the same then don't update
                    If MEValue = CurrentRow.SubItems(2).Text And PriceValue = CDbl(CurrentRow.SubItems(3).Text) Then
                        ' Skip down
                        GoTo Tabs
                    End If

                    ' First, see if we are updating an ME or a price, then deal with each separately
                    If MEUpdate Then
                        ' First we need to look up the Blueprint ID
                        SQL = "SELECT ALL_BLUEPRINTS.BLUEPRINT_ID, ALL_BLUEPRINTS.BLUEPRINT_NAME, TECH_LEVEL, "
                        SQL &= "CASE WHEN ALL_BLUEPRINTS.FAVORITE IS NULL THEN 0 ELSE ALL_BLUEPRINTS.FAVORITE END AS FAVORITE, IGNORE, "
                        SQL &= "CASE WHEN TE IS NULL THEN 0 ELSE TE END AS BP_TE "
                        SQL &= "FROM ALL_BLUEPRINTS LEFT JOIN OWNED_BLUEPRINTS ON ALL_BLUEPRINTS.BLUEPRINT_ID = OWNED_BLUEPRINTS.BLUEPRINT_ID  "
                        SQL &= "WHERE ITEM_NAME = '" & RemoveItemNameRuns(CurrentRow.SubItems(0).Text) & "'"

                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsData = DBCommand.ExecuteReader
                        rsData.Read()

                        ' If they update the ME of the blueprint, then we mark it as Owned and a 0 for TE value, but set the type depending on the bp loaded
                        Dim TempTE As Integer = rsData.GetInt32(5)

                        If rsData.GetInt64(2) = BPTechLevel.T1 Then
                            ' T1 BPO
                            TempBPType = BPType.Original
                        Else
                            ' Remaining T2 and T3 must be invited
                            TempBPType = BPType.InventedBPC
                        End If

                        ' If there is no TE for an invented BPC then set it to the base
                        If TempBPType = BPType.InventedBPC And TempTE = 0 Then
                            TempTE = BaseT2T3TE
                        End If

                        Call UpdateBPinDB(rsData.GetInt64(0), CInt(MEValue), TempTE, TempBPType, CInt(MEValue), 0,
                                          CBool(rsData.GetInt32(3)), CBool(rsData.GetInt32(4)), 0)

                        ' Mark the line with white color since it's no longer going to be unowned
                        CurrentRow.BackColor = Color.White

                        rsData.Close()

                    Else ' Price per unit update

                        SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CStr(CDbl(txtListEditBox.Text)) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & GetTypeID(RemoveItemNameRuns(CurrentRow.SubItems(0).Text))
                        Call EVEDB.ExecuteNonQuerySQL(SQL)

                        ' Mark the line text with black incase it is red for no price
                        CurrentRow.ForeColor = Color.Black

                        PriceUpdated = True

                    End If

                    ' Update the data in the current row
                    CurrentRow.SubItems(2).Text = CStr(MEValue)
                    CurrentRow.SubItems(3).Text = FormatNumber(PriceValue, 2)

                    ' For both ME and Prices, we need to re-calculate the blueprint (hit the Refresh Button) to reflect the new numbers
                    ' First save the current grid for locations
                    RefreshingGrid = True
                    Call frmMain.RefreshBP()
                    RefreshingGrid = False

                Case UpdatePrices, RefineryOutput, RefineryItems, InventionMats
                    ' Price List Update
                    SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CStr(CDbl(txtListEditBox.Text)) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = "
                    If Me.Name = RefineryItems Then
                        SQL &= CurrentRow.SubItems(7).Text
                    Else
                        SQL &= CurrentRow.SubItems(0).Text
                    End If

                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    ' Change the value in the price grid, but don't update the grid
                    CurrentRow.SubItems(GetPriceColumnIndex).Text = FormatNumber(txtListEditBox.Text, 2)
                    If Me.Name = UpdatePrices Then
                        ' Color the cell blue
                        CurrentRow.ForeColor = Color.Blue
                    End If
                    PriceUpdated = True

                Case ManufacturedPriceProfile, RawPriceProfile
                    ' Price Profiles update
                    Dim RawMat As String
                    If Me.Name = RawPriceProfile Then
                        RawMat = "1"
                    Else
                        RawMat = "0"
                    End If

                    ' See if they have the profile set already
                    SQL = "SELECT 'X' FROM PRICE_PROFILES WHERE ID = " & CStr(SelectedCharacter.ID) & " "
                    SQL &= "AND GROUP_NAME = '" & CurrentRow.SubItems(0).Text & "' "
                    SQL &= "AND RAW_MATERIAL = " & RawMat

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsData = DBCommand.ExecuteReader

                    If rsData.Read() Then
                        ' Update
                        SQL = "UPDATE PRICE_PROFILES SET "
                        If PriceTypeUpdate Then
                            ' Save current region/system
                            SQL &= "PRICE_TYPE = '" & cmbeditBox.Text & "' "
                            CurrentRow.SubItems(1).Text = cmbeditBox.Text
                        ElseIf PriceSystemUpdate Then
                            ' Just update system, save others
                            SQL &= "SOLAR_SYSTEM_NAME = '" & cmbeditBox.Text & "' "
                            CurrentRow.SubItems(3).Text = cmbeditBox.Text
                        ElseIf PriceRegionUpdate Then
                            ' Set region, but set system to all systems (blank)
                            SQL &= "REGION_NAME ='" & cmbeditBox.Text & "', SOLAR_SYSTEM_NAME = 'All Systems' "
                            CurrentRow.SubItems(2).Text = cmbeditBox.Text
                            CurrentRow.SubItems(3).Text = AllSystems
                        ElseIf PriceModifierUpdate Then
                            Dim PM As Double = CDbl(txtListEditBox.Text.Replace("%", "")) / 100
                            SQL &= "PRICE_MODIFIER = " & CStr(PM) & " "
                            CurrentRow.SubItems(4).Text = FormatPercent(PM, 1)
                        End If

                        SQL &= "WHERE ID = " & CStr(SelectedCharacter.ID) & " "
                        SQL &= "AND GROUP_NAME ='" & CurrentRow.SubItems(0).Text & "' "
                        SQL &= "AND RAW_MATERIAL = " & RawMat

                        rsData.Close()

                    Else
                        ' Insert new record
                        Dim TempPercent As String = CStr(CDbl(CurrentRow.SubItems(4).Text.Replace("%", "")) / 100)
                        SQL = "INSERT INTO PRICE_PROFILES VALUES (" & CStr(SelectedCharacter.ID) & ",'" & CurrentRow.SubItems(0).Text & "','"
                        If PriceTypeUpdate Then
                            ' Save current region/system
                            SQL &= FormatDBString(cmbeditBox.Text) & "','" & CurrentRow.SubItems(2).Text & "','" & CurrentRow.SubItems(3).Text & "'," & TempPercent & "," & RawMat & ")"
                            CurrentRow.SubItems(1).Text = cmbeditBox.Text
                        ElseIf PriceSystemUpdate Then
                            ' Just update system, save others
                            SQL &= CurrentRow.SubItems(1).Text & "','" & CurrentRow.SubItems(2).Text & "','" & FormatDBString(cmbeditBox.Text) & "'," & TempPercent & "," & RawMat & ")"
                            CurrentRow.SubItems(3).Text = cmbeditBox.Text
                        ElseIf PriceRegionUpdate Then
                            ' Set region, but set system to all systems (blank)
                            SQL &= CurrentRow.SubItems(1).Text & "','" & FormatDBString(cmbeditBox.Text) & "','All Systems'," & TempPercent & "," & RawMat & ")"
                            ' Set the text
                            CurrentRow.SubItems(2).Text = cmbeditBox.Text
                            CurrentRow.SubItems(3).Text = AllSystems
                        ElseIf PriceModifierUpdate Then
                            ' Save current region/system/type
                            SQL &= CurrentRow.SubItems(1).Text & "','" & CurrentRow.SubItems(2).Text & "','" & CurrentRow.SubItems(3).Text & "',"
                            Dim PM As Double = CDbl(txtListEditBox.Text.Replace("%", "")) / 100
                            SQL &= CStr(PM) & "," & RawMat & ")"
                            CurrentRow.SubItems(4).Text = FormatPercent(PM, 1)
                        End If
                    End If

                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    ' Reset these
                    PriceTypeUpdate = False
                    PriceRegionUpdate = False
                    PriceSystemUpdate = False
                    PreviousPriceType = ""
                    PreviousRegion = ""
                    PreviousSystem = ""
                    PriceUpdated = False

                Case BPManagement
                    ' Save the data, Save as no scan, but BPO, BP Type
                    If MEUpdate Then
                        MEValue = txtListEditBox.Text
                    Else
                        MEValue = CurrentRow.SubItems(5).Text
                    End If

                    If TEUpdate Then
                        TEValue = txtListEditBox.Text
                    Else
                        TEValue = CurrentRow.SubItems(6).Text
                    End If

                    If OwnedTypeUpdate Then
                        OwnedTypeValue = cmbEditBox.Text
                    Else
                        OwnedTypeValue = CurrentRow.SubItems(8).Text
                    End If

                    If FavoriteUpdate Then
                        FavoriteValue = cmbEditBox.Text
                    Else
                        FavoriteValue = CurrentRow.SubItems(9).Text
                    End If

                    If FavoriteValue = Yes Then
                        SetasFavorite = True
                    Else
                        SetasFavorite = False
                    End If

                    If IgnoredBPUpdate Then
                        IgnoredValue = cmbEditBox.Text
                    Else
                        IgnoredValue = CurrentRow.SubItems(10).Text
                    End If

                    If IgnoredValue = Yes Then
                        SetasIgnore = True
                    Else
                        SetasIgnore = False
                    End If

                    ' Check the numbers, if they are the same, then don't mark as owned
                    If MEValue = CurrentRow.SubItems(5).Text _
                        And TEValue = CurrentRow.SubItems(6).Text _
                        And OwnedTypeValue = CurrentRow.SubItems(8).Text _
                        And FavoriteValue = CurrentRow.SubItems(9).Text _
                        And IgnoredValue = CurrentRow.SubItems(10).Text Then
                        ' Skip down
                        GoTo Tabs
                    End If

                    ' Set the bp type to make sure we set the owned flag correctly
                    TempBPType = GetBPType(OwnedTypeValue)

                    Dim TempRuns As Integer

                    UpdatedBPType = UpdateBPinDB(CLng(CurrentRow.SubItems(1).Text), CInt(MEValue), CInt(TEValue), TempBPType,
                                      CInt(CurrentRow.SubItems(5).Text), CInt(CurrentRow.SubItems(6).Text), SetasFavorite, SetasIgnore)

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

                    ' Mark as owned and change color
                    If CBool(UpdatedBPType) Then ' 14 = Owned
                        CurrentRow.SubItems.Add(Yes)
                        CurrentRow.ForeColor = Color.Blue
                    Else
                        CurrentRow.SubItems.Add(No)
                        CurrentRow.ForeColor = Color.Black
                        CurrentRow.BackColor = Color.White
                    End If

                Case ShoppingListItems, ShoppingListBuy, ShoppingListBuild
                    ' Save the data depending on what we are updating
                    If QuantityUpdate Then
                        QuantityValue = CInt(txtListEditBox.Text)
                        ' Check the numbers, if the same then don't update
                        If QuantityValue = CInt(CurrentRow.SubItems(2).Text) Then
                            ' Skip down and don't update
                            GoTo Tabs
                        End If
                    End If

                    If PriceUpdate Then
                        PriceValue = CDbl(txtListEditBox.Text)
                        ' Check the numbers, if the same then don't update
                        If PriceValue = CDbl(CurrentRow.SubItems(3).Text) Then
                            ' Skip down and don't update
                            GoTo Tabs
                        End If
                    End If

                    ' Update the quantity data in all three grids
                    If QuantityUpdate Then
                        ' Adjust the mats to what they enter - if it said 100, and they enter 90, then adjust to 90
                        If Me.Name = ShoppingListBuy Then ' The materials we buy to build items 
                            ' Save the mats they probably have on hand to make this change - calc from value in grid vs. value entered
                            Dim OnHandQuantity As Long = CLng(CurrentRow.SubItems(2).Text) - QuantityValue
                            Dim OnHandMaterial As New Material(0, CurrentRow.SubItems(1).Text, "", OnHandQuantity, 0, 0, "", "")
                            TotalShoppingList.OnHandMatList.InsertMaterial(OnHandMaterial)

                            ' Update the buy list
                            Call TotalShoppingList.UpdateShoppingBuyQuantity(CurrentRow.SubItems(1).Text, QuantityValue)

                        ElseIf Me.Name = ShoppingListBuild Then ' The components we are building to make the item
                            Dim TempBuiltItem As New BuiltItem
                            TempBuiltItem.ItemTypeID = CLng(CurrentRow.SubItems(0).Text)
                            TempBuiltItem.ItemName = CurrentRow.SubItems(1).Text
                            TempBuiltItem.ItemQuantity = CLng(CurrentRow.SubItems(2).Text)
                            TempBuiltItem.BuildME = CInt(CurrentRow.SubItems(3).Text)
                            TempBuiltItem.ManufacturingFacility.FacilityName = CurrentRow.SubItems(5).Text

                            ' Save the built components they probably have on hand to make this change - calc from value in grid vs. value entered
                            Dim OnHandQuantity As Long = CLng(CurrentRow.SubItems(2).Text) - QuantityValue
                            Dim OnHandMaterial As New Material(0, CurrentRow.SubItems(1).Text, "", OnHandQuantity, 0, 0, "", "")
                            TotalShoppingList.OnHandComponentList.InsertMaterial(OnHandMaterial)

                            ' Update the build list
                            Call TotalShoppingList.UpdateShoppingBuiltItemQuantity(TempBuiltItem, QuantityValue)

                        ElseIf Me.Name = ShoppingListItems Then ' The items we are building
                            Dim ShopListItem As New ShoppingListItem
                            Dim TempName As String = CurrentRow.SubItems(1).Text
                            If TempName.Contains("(") Then
                                ShopListItem.Name = TempName.Substring(0, InStr(TempName, "(") - 2)
                                ShopListItem.Relic = TempName.Substring(InStr(TempName, "("), InStr(TempName, ")") - InStr(TempName, "(") - 1)
                            Else
                                ShopListItem.Name = TempName
                                ShopListItem.Relic = ""
                            End If
                            ShopListItem.Runs = CLng(CurrentRow.SubItems(2).Text)
                            ShopListItem.ItemME = CInt(CurrentRow.SubItems(3).Text)
                            ShopListItem.ItemTE = CInt(CurrentRow.SubItems(15).Text)
                            ShopListItem.NumBPs = CInt(CurrentRow.SubItems(4).Text)
                            ShopListItem.BuildType = CurrentRow.SubItems(5).Text
                            ShopListItem.Decryptor = CurrentRow.SubItems(6).Text
                            ShopListItem.InventedRunsPerBP = CInt(Math.Ceiling(ShopListItem.Runs / ShopListItem.NumBPs))
                            ShopListItem.ManufacturingFacility.FacilityName = CurrentRow.SubItems(7).Text

                            ' Update the full shopping list
                            Call TotalShoppingList.UpdateShoppingItemQuantity(ShopListItem, QuantityValue)

                        End If

                        ' refresh all three lists with the quantity updated
                        Call frmShop.RefreshLists()

                    ElseIf Me.Name = ShoppingListBuy And PriceUpdate Then ' Price update on the lstBuy screen
                        ' Update the price in the database
                        SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CStr(CDbl(txtListEditBox.Text)) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & CurrentRow.SubItems(0).Text
                        Call EVEDB.ExecuteNonQuerySQL(SQL)

                        ' Change the value in the price grid
                        CurrentRow.SubItems(3).Text = FormatNumber(txtListEditBox.Text, 2)
                        PriceUpdated = True
                    Else
                        GoTo Tabs
                    End If
            End Select

            ' If we updated a price, then update the program everywhere to be consistent
            If PriceUpdated Then
                IgnoreFocus = True
                Call UpdateProgramPrices(False) ' Don't refresh the grid, we are already updating it
                IgnoreFocus = False
            End If

            ' Play sound to indicate update complete
            If PriceUpdated Or Me.Name = BPManagement Or Me.Name = ShoppingListBuild Or Me.Name = ShoppingListBuy Or Me.Name = ShoppingListItems Then
                Call PlayNotifySound()
            End If

            ' Reset text they entered if tabbed
            If SentKey = Keys.ShiftKey Or SentKey = Keys.Tab Then
                txtListEditBox.Text = ""
                cmbeditBox.Text = ""
            End If

            If SentKey = Keys.Enter Then
                ' Just refresh and select the current row
                CurrentRow.Selected = True
                txtListEditBox.Visible = False
                cmbEditBox.Visible = False
            End If

            ' Data updated, so reset
            DataEntered = False
            DataUpdated = True

        End If

Tabs:
        ' If they hit tab, then tab to the next cell
        If SentKey = Keys.Tab Then
            If CurrentRow.Index = -1 Then
                ' Reset the current row based on the original click
                CurrentRow = Me.GetItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                CurrentCell = CurrentRow.GetSubItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                ' Reset the next and previous cells
                SetNextandPreviousCells()
            End If

            CurrentCell = NextCell
            ' Reset these each time
            Call SetNextandPreviousCells("Next")
            If CurrentRow.Index = 0 Then
                ' Scroll to top
                Me.Items.Item(0).Selected = True
                Me.EnsureVisible(0)
                Me.Update()
            Else
                ' Make sure the row is visible
                Me.EnsureVisible(CurrentRow.Index)
            End If

            ' Show the text box
            Call ShowEditBox()
        End If

        ' If shift+tab, then go to the previous cell 
        If SentKey = Keys.ShiftKey Then
            If CurrentRow.Index = -1 Then
                ' Reset the current row based on the original click
                CurrentRow = Me.GetItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                CurrentCell = CurrentRow.GetSubItemAt(SavedListClickLoc.X, SavedListClickLoc.Y)
                ' Reset the next and previous cells
                SetNextandPreviousCells()
            End If

            CurrentCell = PreviousCell
            ' Reset these each time
            Call SetNextandPreviousCells("Previous")
            If CurrentRow.Index = Me.Items.Count - 1 Then
                ' Scroll to bottom
                Me.Items.Item(Me.Items.Count - 1).Selected = True
                Me.EnsureVisible(Me.Items.Count - 1)
                Me.Update()
            Else
                ' Make sure the row is visible
                Me.EnsureVisible(CurrentRow.Index)
            End If

            ' Show the text box
            Call ShowEditBox()
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
        If Me.Items.Count = 1 Then
            NextRow = CurrentRow
            PreviousRow = CurrentRow
        ElseIf CurrentRow.Index <> Me.Items.Count - 1 And CurrentRow.Index <> 0 Then
            ' Not the last line, so set the next and previous
            NextRow = Me.Items.Item(CurrentRow.Index + 1)
            PreviousRow = Me.Items.Item(CurrentRow.Index - 1)
        ElseIf CurrentRow.Index = 0 Then
            NextRow = Me.Items.Item(CurrentRow.Index + 1)
            ' Wrap to bottom
            PreviousRow = Me.Items.Item(Me.Items.Count - 1)
        ElseIf CurrentRow.Index = Me.Items.Count - 1 Then
            ' Need to wrap up to top
            NextRow = Me.Items.Item(0)
            PreviousRow = Me.Items.Item(CurrentRow.Index - 1)
        End If

        ' BP Grids
        Select Case Me.Name
            Case BPRaw, BPComponents
                ' For the update grids in the Blueprint Tab, only show the box if
                ' 1 - If the ME is clicked and it has something other than a '-' in it (meaning no BP)
                ' 2 - If the Price is clicked and the ME box has '-' in it

                ' The next row must be an ME or Price box on the next row 
                ' or a previous ME or price box on the previous row
                If iSubIndex = 2 Or iSubIndex = 3 Then
                    ' Set the next and previous ME boxes (subitems)
                    ' If the next row ME box is a '-' then the next row cell is Price
                    If NextRow.SubItems(2).Text = "-" Then
                        NextCell = NextRow.SubItems.Item(3) ' Next row price box
                    Else ' It can be the ME box in the next row
                        NextCell = NextRow.SubItems.Item(2) ' Next row ME box
                    End If

                    NextCellRow = NextRow

                    'If the previous row ME box is a '-' then the previous row is Price
                    If PreviousRow.SubItems(2).Text = "-" Then
                        PreviousCell = PreviousRow.SubItems.Item(3) ' Next row price box
                    Else ' It can be the ME box in the next row
                        PreviousCell = PreviousRow.SubItems.Item(2) ' Next row ME box
                    End If

                    PreviousCellRow = PreviousRow

                    If iSubIndex = 2 Then
                        MEUpdate = True
                        PriceUpdate = False
                    Else
                        MEUpdate = False
                        PriceUpdate = True
                    End If

                Else
                    NextCell = Nothing
                    PreviousCell = Nothing
                    CurrentCell = Nothing
                End If

            Case RawPriceProfile, ManufacturedPriceProfile
                If iSubIndex <> 0 Then
                    ' Set the next and previous combo boxes
                    If iSubIndex = 4 Then
                        NextCell = NextRow.SubItems.Item(1) ' Next now price type box
                        NextCellRow = NextRow
                    Else
                        NextCell = CurrentRow.SubItems.Item(iSubIndex + 1) ' current row, next cell
                        NextCellRow = CurrentRow
                    End If

                    If iSubIndex = 1 Then
                        PreviousCell = PreviousRow.SubItems.Item(4) ' Previous row price mod
                        PreviousCellRow = PreviousRow
                    Else
                        PreviousCell = CurrentRow.SubItems.Item(iSubIndex - 1) ' Same row, just back a cell
                        PreviousCellRow = CurrentRow
                    End If

                    ' Reset update type
                    Call SetPriceProfileVariables(iSubIndex)

                Else
                    NextCell = Nothing
                    PreviousCell = Nothing
                    CurrentCell = Nothing
                End If

            Case BPManagement
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

            Case ShoppingListItems, ShoppingListBuy, ShoppingListBuild
                ' Check for buy list
                If Me.Name = ShoppingListBuy Then
                    ' The next row must be a Quantity or Price box on the next row 
                    ' or a previous Quantity or Price box on the previous row
                    Select Case iSubIndex
                        Case 2 ' Quantity
                            NextCell = CurrentRow.SubItems.Item(3) ' Current row Cost box
                            NextCellRow = CurrentRow
                            PreviousCell = PreviousRow.SubItems.Item(3) ' previous row price
                            PreviousCellRow = PreviousRow

                            QuantityUpdate = True
                            PriceUpdate = False
                        Case 3 ' Price
                            NextCell = NextRow.SubItems.Item(2) ' Next row quantity box
                            NextCellRow = NextRow
                            PreviousCell = CurrentRow.SubItems.Item(2) ' Current row quantity box
                            PreviousCellRow = CurrentRow

                            QuantityUpdate = False
                            PriceUpdate = True
                        Case Else
                            NextCell = Nothing
                            PreviousCell = Nothing
                            CurrentCell = Nothing
                    End Select

                Else ' For quantity updates only
                    ' Set the next and previous quantity boxes
                    If iSubIndex = 2 Then
                        NextCell = NextRow.SubItems.Item(2) ' Next quantity box
                        NextCellRow = NextRow
                        PreviousCell = PreviousRow.SubItems.Item(2) ' Previous quantity box
                        PreviousCellRow = PreviousRow

                        QuantityUpdate = True
                        PriceUpdate = False
                    Else
                        NextCell = Nothing
                        PreviousCell = Nothing
                        CurrentCell = Nothing
                    End If
                End If

            Case Else ' Price list - single column
                Dim Index As Integer = GetPriceColumnIndex()
                ' For this, just go up and down the rows
                NextCell = NextRow.SubItems.Item(Index)
                NextCellRow = NextRow
                PreviousCell = PreviousRow.SubItems.Item(Index)
                PreviousCellRow = PreviousRow
                PriceUpdate = True
                MEUpdate = False
        End Select

    End Sub

    Private Sub ShowTextBox(pLeft As Integer, pTop As Integer, Optional TextBoxAlignment As HorizontalAlignment = HorizontalAlignment.Right)
        With txtListEditBox
            .Hide()
            ' Set the bounds of the control
            .SetBounds(pLeft, pTop, CurrentCell.Bounds.Width, CurrentCell.Bounds.Height)
            If Me.Name = RefineryItems Then
                .Text = FormatNumber(CDbl(CurrentCell.Text) / CInt(CurrentRow.SubItems(GetPriceColumnIndex() - 1).Text), 2) ' Show the unit price for refinery items list
            Else
                .Text = CurrentCell.Text
            End If
            .Show()
            .TextAlign = TextBoxAlignment
            .Focus()
        End With
        cmbEditBox.Visible = False
    End Sub

    ' Returns the row number that allows price updates
    Private Function GetPriceColumnIndex() As Integer
        Select Case Me.Name
            Case BPRaw, BPComponents, UpdatePrices, RefineryOutput
                Return 3
            Case RefineryItems, InventionMats
                Return 2
            Case ManufacturedPriceProfile, RawPriceProfile
                If PriceModifierUpdate Then
                    Return 4
                End If
        End Select

        Return 0

    End Function

    ' Processes the tab function in the text box for the grid. This overrides the default tabbing between controls
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If txtListEditBox.Visible Or cmbeditBox.Visible Then
            If (keyData = Keys.Tab) Then
                Call ProcessKeyDownEdit(Keys.Tab)
                Return True
            ElseIf keyData = Keys.Tab + Keys.Shift Then
                ' This is Shift + Tab but just send Shift for ease of processing
                Call ProcessKeyDownEdit(Keys.ShiftKey)
                Return True
            End If
        End If
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case WM_VSCROLL
                RaiseEvent ProcMsg(m)
        End Select
        MyBase.WndProc(m)
        Me.DoubleBuffered = True
    End Sub

    Private Sub txtListEditBox_GotFocus(sender As Object, e As System.EventArgs) Handles txtListEditBox.GotFocus
        Call txtListEditBox.SelectAll()
    End Sub

    Private Sub txtListEditBox_KeyDown(sender As Object, e As KeyEventArgs) Handles txtListEditBox.KeyDown
        If Not DataEntered Then ' If data already entered, then they didn't do it through paste
            DataEntered = ProcessCutCopyPasteSelect(txtListEditBox, e)
        End If

        If e.KeyCode = Keys.Enter Then
            IgnoreFocus = True
            Call ProcessKeyDownEdit(Keys.Enter)
            IgnoreFocus = False
        End If
    End Sub

    Private Sub txtListEditBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtListEditBox.KeyPress
        ' Make sure it's the right format for ME or Price update
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If MEUpdate Or TEUpdate Then
                If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                    ' Invalid Character
                    e.Handled = True
                Else
                    DataEntered = True
                End If
            ElseIf PriceUpdate Then
                If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                    ' Invalid Character
                    e.Handled = True
                Else
                    DataEntered = True
                End If
            ElseIf QuantityUpdate Then
                If allowedRunschars.IndexOf(e.KeyChar) = -1 Then
                    ' Invalid Character
                    e.Handled = True
                Else
                    DataEntered = True
                End If
            ElseIf PriceModifierUpdate Then
                e.Handled = CheckPercentCharEntry(e, txtListEditBox)
                If e.Handled = False Then
                    DataEntered = True
                End If
            End If
        End If

    End Sub

    Private Sub txtListEditBox_LostFocus(sender As Object, e As System.EventArgs) Handles txtListEditBox.LostFocus
        If Not RefreshingGrid And DataEntered And Not IgnoreFocus And (PriceModifierUpdate And txtListEditBox.Text <> PreviousPriceMod) And Not TabPressed Then
            Call ProcessKeyDownEdit(Keys.Enter)
        End If
        TabPressed = False ' Reset
        txtListEditBox.Visible = False
    End Sub

    Private Sub txtListEditBox_TextChanged(sender As Object, e As System.EventArgs) Handles txtListEditBox.TextChanged
        If MEUpdate Then ' make sure they only enter 0-10 for values
            Call VerifyMETEEntry(txtListEditBox, "ME")
        ElseIf TEUpdate Then
            Call VerifyMETEEntry(txtListEditBox, "TE")
        End If
    End Sub

    Private Sub cmbEditBox_DropDownClosed(sender As Object, e As System.EventArgs) Handles cmbeditBox.DropDownClosed
        If (PriceRegionUpdate And cmbeditBox.Text <> PreviousRegion) Or
            (PriceSystemUpdate And cmbeditBox.Text <> PreviousSystem) Or
            (PriceTypeUpdate And cmbeditBox.Text <> PreviousPriceType) And Not UpdatingCombo Then
            DataEntered = True
            Call ProcessKeyDownEdit(Keys.Enter)
        End If
    End Sub

    Private Sub cmbEditBox_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbeditBox.SelectedIndexChanged
        ' If Not DataUpdated Then
        DataEntered = True
        ' End If
    End Sub

    Private Sub cmbEditBox_LostFocus(sender As Object, e As System.EventArgs) Handles cmbeditBox.LostFocus
        ' Lost focus some other way than tabbing
        If ((PriceRegionUpdate And cmbeditBox.Text <> PreviousRegion) Or
            (PriceSystemUpdate And cmbeditBox.Text <> PreviousSystem) Or
            (PriceTypeUpdate And cmbeditBox.Text <> PreviousPriceType)) _
            And Not TabPressed And Not UpdatingCombo Then
            DataEntered = True
            Call ProcessKeyDownEdit(Keys.Enter)
        End If
        TabPressed = False
        cmbEditBox.Visible = False
    End Sub

    ' Sets the variables for price profiles
    Private Sub SetPriceProfileVariables(Index As Integer)
        PriceTypeUpdate = False
        PriceRegionUpdate = False
        PriceSystemUpdate = False
        PriceModifierUpdate = False

        Select Case Index
            Case 1
                PriceTypeUpdate = True
                PreviousPriceType = CurrentCell.Text
            Case 2
                PriceRegionUpdate = True
                PreviousRegion = CurrentCell.Text
            Case 3
                PriceSystemUpdate = True
                PreviousSystem = CurrentCell.Text
            Case 4
                PriceModifierUpdate = True
                PreviousPriceMod = CurrentCell.Text
        End Select

    End Sub

    ' Detects Scroll event and hides boxes
    Private Sub List_ProcMsg(ByVal m As Message) Handles Me.ProcMsg
        txtListEditBox.Hide()
        cmbeditBox.Hide()
    End Sub

    ' Hide edit boxes when scrolling
    Private Sub MyListView_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        ' hide the boxes
        Call txtListEditBox.Hide()
        Call cmbeditBox.Hide()
    End Sub

End Class
