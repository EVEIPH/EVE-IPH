Imports System.Data.SQLite

Public Class MyListView
    Inherits ListView

    Public Event ProcMsg(ByVal m As Message)
    Public Const WM_VSCROLL As Integer = 277
    Private WithEvents txtListEditBox As TextBox
    Private WithEvents cmbeditBox As ComboBox

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
    Private DataUpdated As Boolean
    Private DataEntered As Boolean
    Private SelectedGrid As ListView

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
    Private Const MiningGrid As String = "lstMineGrid"

    Public Sub New()

        Call InitializeComponent()

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        txtListEditBox = New TextBox
        Me.SuspendLayout()
        Me.Controls.Add(txtListEditBox)
        txtListEditBox.SetBounds(5, 5, 20, 20)
        txtListEditBox.Hide()

        cmbeditBox = New ComboBox
        Me.SuspendLayout()
        Me.Controls.Add(cmbeditBox)
        cmbeditBox.SetBounds(5, 5, 20, 20)
        cmbeditBox.Hide()

    End Sub

    ' Determines where to show the text box when clicking on the list sent
    Public Sub ListClicked(ListRef As ListView, sender As Object, e As MouseEventArgs)
        Dim iSubIndex As Integer = 0

        ' Hide the text box when a new line is selected
        txtListEditBox.Hide()
        cmbeditBox.Hide()

        CurrentRow = ListRef.GetItemAt(e.X, e.Y) ' which listviewitem was clicked
        SelectedGrid = ListRef

        If CurrentRow Is Nothing Then
            Exit Sub
        End If

        CurrentCell = CurrentRow.GetSubItemAt(e.X, e.Y)  ' which subitem was clicked

        ' Determine where the previous and next item boxes will be based on what they clicked - used in tab event handling
        Call SetNextandPreviousCells(ListRef)

        ' See which column has been clicked
        iSubIndex = CurrentRow.SubItems.IndexOf(CurrentCell)

        Select Case ListRef.Name
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
                        Call ShowEditBox(ListRef)
                    End If

                End If
            Case UpdatePrices, RefineryOutput, RefineryItems
                ' Set the columns that can be edited, just Price
                If (ListRef.Name = RefineryItems And iSubIndex = 2) Or (ListRef.Name <> RefineryItems And iSubIndex = 3) Then
                    Call ShowEditBox(ListRef)
                    PriceUpdate = True
                End If
            Case RawPriceProfile, ManufacturedPriceProfile
                If iSubIndex > 0 Then
                    ' Reset update type
                    Call SetPriceProfileVariables(iSubIndex)
                    Call ShowEditBox(ListRef)
                End If
        End Select

    End Sub

    ' For updating the items in the list by clicking on them
    Private Sub ProcessKeyDownEdit(SentKey As Keys, ListRef As ListView)
        Dim SQL As String = ""
        Dim rsData As SQLiteDataReader

        Dim MEValue As String = ""
        Dim PriceValue As Double = 0
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

            Select Case ListRef.Name
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
                        Dim TempBPType As BPType
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

                Case UpdatePrices, RefineryOutput, RefineryItems
                    ' Price List Update
                    SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CStr(CDbl(txtListEditBox.Text)) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = "
                    If ListRef.Name = RefineryItems Then
                        SQL &= CurrentRow.SubItems(7).Text
                    Else
                        SQL &= CurrentRow.SubItems(0).Text
                    End If

                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    ' Change the value in the price grid, but don't update the grid
                    CurrentRow.SubItems(3).Text = FormatNumber(txtListEditBox.Text, 2)
                    PriceUpdated = True

                Case ManufacturedPriceProfile, RawPriceProfile
                    ' Price Profiles update
                    Dim RawMat As String
                    If ListRef.Name = RawPriceProfile Then
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
            End Select

            ' If we updated a price, then update the program everywhere to be consistent
            If PriceUpdated Then
                IgnoreFocus = True
                Call UpdateProgramPrices(False) ' Don't refresh the grid, we are already updating it
                IgnoreFocus = False
            End If

            ' Play sound to indicate update complete
            If PriceUpdated Then
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
            Call ShowEditBox(ListRef)
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
            Call ShowEditBox(ListRef)
        End If

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

        ' BP Grids
        If ListRef.Name = BPRaw Or ListRef.Name = BPComponents Then

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

        ElseIf ListRef.Name = RawPriceProfile Or ListRef.Name = ManufacturedPriceProfile Then

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

        Else ' Price list 
            Dim Index As Integer = GetPriceColumnIndex(ListRef)
            ' For this, just go up and down the rows
            NextCell = NextRow.SubItems.Item(Index)
            NextCellRow = NextRow
            PreviousCell = PreviousRow.SubItems.Item(Index)
            PreviousCellRow = PreviousRow
            PriceUpdate = True
            MEUpdate = False
        End If

    End Sub

    Private Sub ShowTextBox(ListColumnIndex As Integer, pLeft As Integer, pTop As Integer)
        With txtListEditBox
            .Hide()
            ' Set the bounds of the control
            .SetBounds(pLeft, pTop, CurrentCell.Bounds.Width, CurrentCell.Bounds.Height)
            If SelectedGrid.Name = RefineryItems Then
                .Text = FormatNumber(CDbl(CurrentCell.Text) / CInt(CurrentRow.SubItems(ListColumnIndex - 1).Text), 2) ' Show the unit price for refinery items list
            Else
                .Text = CurrentCell.Text
            End If
            .Show()
            If Me.Columns(CurrentRow.SubItems.IndexOf(CurrentCell)).Text = "ME" Then
                .TextAlign = HorizontalAlignment.Center
            Else
                .TextAlign = HorizontalAlignment.Right
            End If
            .Focus()
        End With
        cmbeditBox.Visible = False
    End Sub

    ' Returns the row number that allows price updates
    Private Function GetPriceColumnIndex(SentList As ListView) As Integer
        Select Case SentList.Name
            Case BPRaw, BPComponents, UpdatePrices, RefineryOutput
                Return 3
            Case RefineryItems
                Return 2
            Case ManufacturedPriceProfile, RawPriceProfile
                If PriceModifierUpdate Then
                    Return 4
                End If
        End Select

        Return 0

    End Function

    ' Shows the text/combo box on the grid where clicked if enabled
    Private Sub ShowEditBox(ListRef As ListView)
        Dim RowIndex As Integer = 0

        ' Save the center location of the edit box
        SavedListClickLoc.X = CurrentCell.Bounds.Left + CInt(CurrentCell.Bounds.Width / 2)
        SavedListClickLoc.Y = CurrentCell.Bounds.Top + CInt(CurrentCell.Bounds.Height / 2)

        ' Get the boundry data for the control now
        Dim pLeft As Integer = CurrentCell.Bounds.Left
        Dim pTop As Integer = CurrentCell.Bounds.Top - 1

        Select Case ListRef.Name
            Case BPRaw, BPComponents, UpdatePrices, RefineryOutput, RefineryItems
                Call ShowTextBox(GetPriceColumnIndex(ListRef), pLeft, pTop)
            Case ManufacturedPriceProfile, RawPriceProfile
                If PriceModifierUpdate Then
                    ' Show the text box
                    Call ShowTextBox(GetPriceColumnIndex(ListRef), pLeft, pTop)
                Else ' Show combo box
                    With cmbeditBox
                        UpdatingCombo = True
                        .Hide()

                        If PriceRegionUpdate Then
                            Call LoadRegionCombo(cmbeditBox, CurrentCell.Text)
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
        End Select
    End Sub

    ' Processes the tab function in the text box for the grid. This overrides the default tabbing between controls
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If txtListEditBox.Visible Or cmbeditBox.Visible Then
            If (keyData = Keys.Tab) Then
                Call ProcessKeyDownEdit(Keys.Tab, SelectedGrid)
                Return True
            ElseIf keyData = Keys.Tab + Keys.Shift Then
                ' This is Shift + Tab but just send Shift for ease of processing
                Call ProcessKeyDownEdit(Keys.ShiftKey, SelectedGrid)
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
            Call ProcessKeyDownEdit(Keys.Enter, Me)
            IgnoreFocus = False
        End If
    End Sub

    Private Sub txtListEditBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtListEditBox.KeyPress
        ' Make sure it's the right format for ME or Price update
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If MEUpdate Then
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
            ElseIf PriceModifierUpdate Then
                e.Handled = CheckPercentCharEntry(e, txtListEditBox)
                If e.Handled = False Then
                    DataEntered = True
                End If
            End If
        End If

    End Sub

    Private Sub txtListEditBox_LostFocus(sender As Object, e As System.EventArgs) Handles txtListEditBox.LostFocus
        If Not RefreshingGrid And DataEntered And Not IgnoreFocus And (PriceModifierUpdate And txtListEditBox.Text <> PreviousPriceMod) Then
            Call ProcessKeyDownEdit(Keys.Enter, SelectedGrid)
        End If
        txtListEditBox.Visible = False
    End Sub

    Private Sub txtListEditBox_TextChanged(sender As Object, e As System.EventArgs) Handles txtListEditBox.TextChanged
        If MEUpdate Then ' make sure they only enter 0-10 for values
            Call VerifyMETEEntry(txtListEditBox, "ME")
        End If
    End Sub

    Private Sub cmbEditBox_DropDownClosed(sender As Object, e As System.EventArgs) Handles cmbeditBox.DropDownClosed
        If (PriceRegionUpdate And cmbeditBox.Text <> PreviousRegion) Or
            (PriceSystemUpdate And cmbeditBox.Text <> PreviousSystem) Or
            (PriceTypeUpdate And cmbeditBox.Text <> PreviousPriceType) And Not UpdatingCombo Then
            DataEntered = True
            Call ProcessKeyDownEdit(Keys.Enter, SelectedGrid)
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
            Call ProcessKeyDownEdit(Keys.Enter, SelectedGrid)
        End If
        cmbeditBox.Visible = False
        TabPressed = False
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
