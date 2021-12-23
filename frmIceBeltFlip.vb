Imports System.Data.SQLite

Public Class frmIceBeltFlip
    Private IceColumnClicked As Integer
    Private IceColumnSortOrder As SortOrder
    Private ProductColumnClicked As Integer
    Private ProductColumnSortOrder As SortOrder

    Private FirstLoad As Boolean
    Private ReprocessingStation As ReprocessingPlant
    Dim BeltTotalValue As Double

    Private Enum IceLocationID
        Highsec = 0
        Lowsec = 1
        NullWeak = 2
        NullStrong = 3
    End Enum

    Public Sub New()
        FirstLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call LoadSettings()

        Call InitializeReprocessingFacility()

        IceColumnClicked = 0
        IceColumnSortOrder = SortOrder.None
        ProductColumnClicked = 0
        ProductColumnSortOrder = SortOrder.None

        FirstLoad = False

    End Sub

    Public Sub InitializeReprocessingFacility()
        ' Load the mining tab refinery
        Call ReprocessingFacility.InitializeControl(SelectedCharacter.ID, ProgramLocation.IceBelts, ProductionType.Reprocessing, Me)
    End Sub

    Private Sub frmIceBeltFlip_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Cursor = Cursors.WaitCursor

        Call LoadIceTable()

        Me.Cursor = Cursors.Default
        FirstLoad = False

    End Sub

    Private Sub LoadSettings()
        ' Miner settings
        txtCycleTime.Text = FormatNumber(UserIceBeltFlipSettings.CycleTime)
        txtm3perCycle.Text = FormatNumber(UserIceBeltFlipSettings.m3perCycle)
        cmbNumMiners.Text = CStr(UserIceBeltFlipSettings.NumMiners)
        chkCompressIce.Checked = UserIceBeltFlipSettings.CompressOre
        chkIPHperMiner.Checked = UserIceBeltFlipSettings.IPHperMiner

        If UserIceBeltFlipSettings.SystemSecurity = "" Or UserIceBeltFlipSettings.SystemSecurity = rbtnHighsec.Text Then
            rbtnHighsec.Checked = True
        ElseIf UserIceBeltFlipSettings.SystemSecurity = rbtnLowsec.Text Then
            rbtnLowsec.Checked = True
        ElseIf UserIceBeltFlipSettings.SystemSecurity = rbtnNullWeak.Text Then
            rbtnNullWeak.Checked = True
        ElseIf UserIceBeltFlipSettings.SystemSecurity = rbtnNullStrong.Text Then
            rbtnNullStrong.Checked = True
        End If

        'm3/hr/miner =  m3 per cycle / cycletime * 3600
        lblm3perhrperminer.Text = FormatNumber(CDbl(txtm3perCycle.Text) / CDbl(txtCycleTime.Text) * 3600, 2)

        ' Tax settings
        Select Case UserIceBeltFlipSettings.IncludeBrokerFees
            Case 2
                chkBrokerFees.CheckState = CheckState.Indeterminate
                txtBrokerFeeRate.Visible = True
            Case 1
                chkBrokerFees.CheckState = CheckState.Checked
            Case 0
                chkBrokerFees.CheckState = CheckState.Unchecked
        End Select

        chkIncludeTaxes.Checked = UserIceBeltFlipSettings.IncludeTaxes
        txtBrokerFeeRate.Text = FormatPercent(UserIceBeltFlipSettings.BrokerFeeRate, 1)

    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Dim TempSettings As IceBeltFlipSettings = Nothing
        Dim Settings As New ProgramSettings

        If Not CheckEnteredData() Then
            Exit Sub
        End If

        TempSettings.CompressOre = chkCompressIce.Checked
        TempSettings.IPHperMiner = chkIPHperMiner.Checked
        TempSettings.CycleTime = CDbl(txtCycleTime.Text)
        TempSettings.m3perCycle = CDbl(txtm3perCycle.Text)
        TempSettings.NumMiners = CInt(cmbNumMiners.Text)

        TempSettings.IncludeTaxes = chkIncludeTaxes.Checked
        TempSettings.IncludeBrokerFees = CType(chkBrokerFees.CheckState, Integer)
        TempSettings.BrokerFeeRate = FormatManualPercentEntry(txtBrokerFeeRate.Text)

        If rbtnHighsec.Checked Then
            TempSettings.SystemSecurity = rbtnHighsec.Text
        ElseIf rbtnLowsec.Checked Then
            TempSettings.SystemSecurity = rbtnLowsec.Text
        ElseIf rbtnNullWeak.Checked Then
            TempSettings.SystemSecurity = rbtnNullWeak.Text
        ElseIf rbtnNullStrong.Checked Then
            TempSettings.SystemSecurity = rbtnNullStrong.Text
        End If

        If rbtnAmarr.Checked Then
            TempSettings.Space = rbtnAmarr.Text
        ElseIf rbtnCaldari.Checked Then
            TempSettings.Space = rbtnCaldari.Text
        ElseIf rbtnGallente.Checked Then
            TempSettings.Space = rbtnGallente.Text
        ElseIf rbtnMinmatar.Checked Then
            TempSettings.Space = rbtnMinmatar.Text
        End If

        ' Save it in the Application settings
        Call Settings.SaveApplicationSettings(UserApplicationSettings)

        ' Save the data in the XML file
        Call Settings.SaveIceBeltFlipSettings(TempSettings)

        ' Save the data to the local variable
        UserIceBeltFlipSettings = TempSettings

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnSaveChecks_Click(sender As Object, e As EventArgs) Handles btnSaveChecks.Click
        Dim TempSettings As IceBeltCheckSettings = Nothing
        Dim Settings As New ProgramSettings
        Dim SavedSettings As New IceBeltCheckSettings

        ' Reset them all to default settings first if not found
        TempSettings = AllSettings.SetDefaultIceBeltChecksSettings()

        With TempSettings
            ' Loop through the ore list and save the value
            For i = 0 To lstIce.Items.Count - 1
                Select Case lstIce.Items(i).SubItems(1).Text
                    Case "Blue Ice"
                        .BlueIce = lstIce.Items(i).Checked
                    Case "Clear Icicle"
                        .ClearIcicle = lstIce.Items(i).Checked
                    Case "Dark Glitter"
                        .DarkGlitter = lstIce.Items(i).Checked
                    Case "Enriched Clear Icicle"
                        .EnrichedClearIcicle = lstIce.Items(i).Checked
                    Case "Gelidus"
                        .Gelidus = lstIce.Items(i).Checked
                    Case "Glacial Mass"
                        .GlacialMass = lstIce.Items(i).Checked
                    Case "Glare Crust"
                        .GlareCrust = lstIce.Items(i).Checked
                    Case "Krystallos"
                        .Krystallos = lstIce.Items(i).Checked
                    Case "Pristine White Glaze"
                        .PristineWhiteGlaze = lstIce.Items(i).Checked
                    Case "Smooth Glacia lMass"
                        .SmoothGlacialMass = lstIce.Items(i).Checked
                    Case "Thick Blue Ice"
                        .ThickBlueIce = lstIce.Items(i).Checked
                    Case "White Glaze"
                        .WhiteGlaze = lstIce.Items(i).Checked
                    Case "Compressed Blue Ice"
                        .CompressedBlueIce = lstIce.Items(i).Checked
                    Case "Compressed Clear Icicle"
                        .CompressedClearIcicle = lstIce.Items(i).Checked
                    Case "Compressed Dark Glitter"
                        .CompressedDarkGlitter = lstIce.Items(i).Checked
                    Case "Compressed Enriched Clear Icicle"
                        .CompressedEnrichedClearIcicle = lstIce.Items(i).Checked
                    Case "Compressed Gelidus"
                        .CompressedGelidus = lstIce.Items(i).Checked
                    Case "Compressed Glacial Mass"
                        .CompressedGlacialMass = lstIce.Items(i).Checked
                    Case "Compressed Glare Crust"
                        .CompressedGlareCrust = lstIce.Items(i).Checked
                    Case "Compressed Krystallos"
                        .CompressedKrystallos = lstIce.Items(i).Checked
                    Case "Compressed Pristine White Glaze"
                        .CompressedPristineWhiteGlaze = lstIce.Items(i).Checked
                    Case "Compressed Smooth Glacial Mass"
                        .CompressedSmoothGlacialMass = lstIce.Items(i).Checked
                    Case "Compressed Thick Blue Ice"
                        .CompressedThickBlueIce = lstIce.Items(i).Checked
                    Case "Compressed White Glaze"
                        .CompressedWhiteGlaze = lstIce.Items(i).Checked
                End Select
            Next
        End With

        ' Save the data in the XML file
        Settings.SaveIceBeltChecksSettings(TempSettings)

        ' Save them locally
        UserIceBeltCheckSettings = TempSettings

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub RefineIce()
        Dim SQL As String = ""
        Dim readerBelts As SQLiteDataReader
        Dim lstOreRow As ListViewItem = Nothing

        Dim item As ListViewItem
        Dim checkedItems As ListView.CheckedListViewItemCollection
        Dim TotalRefinedMinerals As New Materials
        Dim TotalCost As Double
        Dim OutputNumber As Double
        Dim IceName As String

        Dim BeltVolume As Double
        Dim TimeToFlip As Double
        Dim TimeToFlipPer As Double

        Dim RefinedMaterials As New Materials
        Dim TotalRefiningUsage As Double = 0
        Dim SingleRefiningUsage As Double = 0

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If Not CheckEnteredData() Then
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            Exit Sub
        End If

        Dim BFI As New BrokerFeeInfo
        BFI = GetBrokerFeeData(chkBrokerFees, txtBrokerFeeRate)

        ReprocessingStation = New ReprocessingPlant(ReprocessingFacility.GetFacility(ProductionType.Reprocessing), UserApplicationSettings.RefiningImplantValue)

        ' Make sure to refine ice
        ReprocessingStation.GetFacilility.MaterialMultiplier = ReprocessingStation.GetFacilility.IceFacilityRefineRate
        ' Update the label to show the base refine bonus with rigs
        ReprocessingFacility.UpdateRefineYieldLabel(ReprocessingStation.GetFacilility.IceFacilityRefineRate)

        ' Just work with the ones that are checked
        checkedItems = lstIce.CheckedItems

        If checkedItems.Count > 0 Then
            ' Update each item based on inputs
            For Each item In checkedItems
                IceName = CType(item.SubItems(1).Text, String)
                SQL = "SELECT typeID from INVENTORY_TYPES WHERE typeName = '" & IceName & "'"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerBelts = DBCommand.ExecuteReader

                If readerBelts.Read Then
                    ' Refine each ore in the ore list, store refined minerals
                    RefinedMaterials = ReprocessingStation.Reprocess(readerBelts.GetInt64(0), SelectedCharacter.Skills.GetSkillLevel(3385), SelectedCharacter.Skills.GetSkillLevel(3389),
                                                                 SelectedCharacter.Skills.GetSkillLevel("Ice Processing"),
                                                                 CType(item.SubItems(2).Text, Double), chkIncludeTaxes.Checked, BFI, OutputNumber, SingleRefiningUsage)
                    TotalRefiningUsage += SingleRefiningUsage

                    ' Store the refined materials
                    TotalRefinedMinerals.InsertMaterialList(RefinedMaterials.GetMaterialList)
                    ' Save the total cost separate so we take into account taxes and fees
                    TotalCost = TotalCost + RefinedMaterials.GetTotalMaterialsCost

                    ' Apply taxes and fees
                    TotalCost = AdjustPriceforTaxesandFees(TotalCost, chkIncludeTaxes.Checked, GetBrokerFeeData(chkBrokerFees, txtBrokerFeeRate))

                    ' Reset the value of the refined materials
                    TotalRefinedMinerals.ResetTotalValue(TotalCost)

                End If
                readerBelts.Close()
                DBCommand = Nothing

            Next

            ' Update the total usage for doing this refining
            ReprocessingFacility.GetSelectedFacility.FacilityUsage = TotalRefiningUsage

            ' Sort the list
            Call TotalRefinedMinerals.SortMaterialListByQuantity()

            Call lstIceProducts.BeginUpdate()
            Call lstIceProducts.Items.Clear()

            ' Now that we've refined all the ores, put the minerals into minerals list
            For i = 0 To TotalRefinedMinerals.GetMaterialList.Count - 1
                lstOreRow = New ListViewItem(TotalRefinedMinerals.GetMaterialList(i).GetMaterialName)
                'The remaining columns are subitems
                lstOreRow.SubItems.Add(FormatNumber(TotalRefinedMinerals.GetMaterialList(i).GetQuantity, 0))
                lstOreRow.SubItems.Add(FormatNumber(TotalRefinedMinerals.GetMaterialList(i).GetTotalCost, 2))
                Call lstIceProducts.Items.Add(lstOreRow)
            Next

            Call lstIceProducts.EndUpdate()

            ' Belt Values
            BeltVolume = GetTotalVolume()
            lblTotalBeltVolume.Text = FormatNumber(BeltVolume, 2)

            TimeToFlip = (BeltVolume / (CDbl(lblm3perhrperminer.Text) * CInt(cmbNumMiners.Text))) * 3600
            TimeToFlipPer = (BeltVolume / CDbl(lblm3perhrperminer.Text)) * 3600
            lblTotalHourstoFlip.Text = FormatIPHTime(TimeToFlip)

            If chkIPHperMiner.Checked = True Then
                lblBeltIPH.Text = FormatNumber(BeltTotalValue / (TimeToFlipPer / 3600), 2)
            Else
                lblBeltIPH.Text = FormatNumber(BeltTotalValue / (TimeToFlip / 3600), 2)
            End If

            ' Refine values
            lblTotalRefineValue.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost, 2)
            lblTotalRefineVolume.Text = FormatNumber(TotalRefinedMinerals.GetTotalVolume, 2)

            If chkIPHperMiner.Checked = True Then
                lblRefineIPH.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost / (TimeToFlipPer / 3600), 2)
            Else
                lblRefineIPH.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost / (TimeToFlip / 3600), 2)
            End If

        Else
            ' Nothing checked so clear
            lstIceProducts.Items.Clear()
        End If

        Me.Cursor = Cursors.Default
        Application.DoEvents()


    End Sub

    Private Sub LoadIceTable()
        Dim SQL As String
        Dim readerBelts As SQLiteDataReader
        Dim lstRow As ListViewItem
        Dim SpaceRaceIDSQL As String
        Dim LocationID As String

        If Not CheckEnteredData() Then
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            Exit Sub
        End If

        BeltTotalValue = 0

        SpaceRaceIDSQL = " AND RACE_ID = "
        If rbtnAmarr.Checked Then
            SpaceRaceIDSQL &= "4"
        ElseIf rbtnCaldari.Checked Then
            SpaceRaceIDSQL &= "1"
        ElseIf rbtnGallente.Checked Then
            SpaceRaceIDSQL &= "8"
        ElseIf rbtnMinmatar.Checked Then
            SpaceRaceIDSQL &= "2"
        End If

        LocationID = " AND LOCATION_ID ="
        If rbtnHighsec.Checked Then
            LocationID &= IceLocationID.Highsec
        ElseIf rbtnLowsec.Checked Then
            LocationID &= IceLocationID.Lowsec
        ElseIf rbtnNullWeak.Checked Then
            LocationID &= IceLocationID.NullWeak
        ElseIf rbtnNullStrong.Checked Then
            LocationID &= IceLocationID.NullStrong
        End If

        SQL = "SELECT typeName, AMOUNT, PRICE FROM ICE_BELTS, INVENTORY_TYPES LEFT JOIN ITEM_PRICES_FACT ON ITEM_ID = ICE_ID "
        SQL &= " WHERE ICE_ID = typeID " & SpaceRaceIDSQL & LocationID
        If chkCompressIce.Checked Then
            SQL &= " AND COMPRESSED = 1"
        Else
            SQL &= " AND COMPRESSED = 0"
        End If
        SQL &= " ORDER BY typeName"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBelts = DBCommand.ExecuteReader

        Call lstIce.Items.Clear()
        Call lstIce.BeginUpdate()

        While readerBelts.Read
            lstRow = New ListViewItem("")
            'The remaining columns are subitems
            lstRow.SubItems.Add(readerBelts.GetString(0))
            lstRow.SubItems.Add(FormatNumber(readerBelts.GetInt32(1), 0))
            lstRow.SubItems.Add(FormatNumber(readerBelts.GetDouble(2), 2))
            lstRow.Checked = GetIceCheckValue(readerBelts.GetString(0)) ' Set Check
            Call lstIce.Items.Add(lstRow)

            ' Save the total value for the belt
            BeltTotalValue += CDbl(readerBelts.GetDouble(2) * readerBelts.GetInt32(1))
        End While

        Call lstIce.EndUpdate()

        lblTotalBeltValue.Text = FormatNumber(BeltTotalValue, 2)

        ' Refresh this m3/hr/miner =  m3 per cycle / cycletime * 3600
        lblm3perhrperminer.Text = FormatNumber(CDbl(txtm3perCycle.Text) / CDbl(txtCycleTime.Text) * 3600, 2)

    End Sub

    ' Called from Manufacturing Facility
    Public Sub RefreshGrids()
        Call LoadIceTable()
        Call RefineIce()
    End Sub

    ' Set ice checks by the type of ice
    Private Function GetIceCheckValue(ByVal IceName As String) As Boolean
        Dim Settings As IceBeltCheckSettings = UserIceBeltCheckSettings
        Dim ReturnValue As Boolean

        Select Case IceName
            Case "Blue Ice"
                ReturnValue = Settings.BlueIce
            Case "Clear Icicle"
                ReturnValue = Settings.ClearIcicle
            Case "Dark Glitter"
                ReturnValue = Settings.DarkGlitter
            Case "Enriched Clear Icicle"
                ReturnValue = Settings.EnrichedClearIcicle
            Case "Gelidus"
                ReturnValue = Settings.Gelidus
            Case "Glacial Mass"
                ReturnValue = Settings.GlacialMass
            Case "Glare Crust"
                ReturnValue = Settings.GlareCrust
            Case "Krystallos"
                ReturnValue = Settings.Krystallos
            Case "Pristine White Glaze"
                ReturnValue = Settings.PristineWhiteGlaze
            Case "Smooth Glacial Mass"
                ReturnValue = Settings.SmoothGlacialMass
            Case "Thick Blue Ice"
                ReturnValue = Settings.ThickBlueIce
            Case "White Glaze"
                ReturnValue = Settings.CompressedWhiteGlaze
            Case "Compressed Blue Ice"
                ReturnValue = Settings.CompressedBlueIce
            Case "Compressed Clear Icicle"
                ReturnValue = Settings.CompressedClearIcicle
            Case "Compressed Dark Glitter"
                ReturnValue = Settings.CompressedDarkGlitter
            Case "Compressed Enriched Clear Icicle"
                ReturnValue = Settings.CompressedEnrichedClearIcicle
            Case "Compressed Gelidus"
                ReturnValue = Settings.CompressedGelidus
            Case "Compressed Glacial Mass"
                ReturnValue = Settings.CompressedGlacialMass
            Case "Compressed Glare Crust"
                ReturnValue = Settings.CompressedGlareCrust
            Case "Compressed Krystallos"
                ReturnValue = Settings.CompressedKrystallos
            Case "Compressed Pristine White Glaze"
                ReturnValue = Settings.CompressedPristineWhiteGlaze
            Case "Compressed Smooth Glacial Mass"
                ReturnValue = Settings.CompressedSmoothGlacialMass
            Case "Compressed Thick Blue Ice"
                ReturnValue = Settings.CompressedThickBlueIce
            Case "Compressed White Glaze"
                ReturnValue = Settings.CompressedWhiteGlaze
        End Select

        Return ReturnValue

    End Function

    Private Function CheckEnteredData() As Boolean

        If Trim(cmbNumMiners.Text) <> "" Then
            If Not IsNumeric(cmbNumMiners.Text) Then
                MsgBox("Invalid Number of Miners value", vbExclamation, Application.ProductName)
                cmbNumMiners.Focus()
                Return False
            End If
        Else
            MsgBox("Please enter a Number of Miners value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            Return False
        End If

        If CInt(cmbNumMiners.Text) > 101 Then
            MsgBox("Maximum miners is 100", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            Return False
        End If

        If CInt(cmbNumMiners.Text) <= 0 Then
            MsgBox("Number of miners must be greater than 0", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            Return False
        End If

        If Trim(txtCycleTime.Text) <> "" Then
            If Not IsNumeric(txtCycleTime.Text) Then
                MsgBox("Invalid Cycle Time value", vbExclamation, Application.ProductName)
                txtCycleTime.Focus()
                Return False
            End If
        Else
            MsgBox("Please enter a Cycle Time value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            Return False
        End If

        If CDbl(txtCycleTime.Text) <= 0 Then
            MsgBox("Cycle time must be greater than 0", vbExclamation, Application.ProductName)
            txtm3perCycle.Focus()
            Return False
        End If

        If Trim(txtm3perCycle.Text) <> "" Then
            If Not IsNumeric(txtm3perCycle.Text) Then
                MsgBox("Invalid m3 per Cycle value", vbExclamation, Application.ProductName)
                txtm3perCycle.Focus()
                Return False
            End If
        Else
            MsgBox("Please enter a m3 per Cycle value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            Return False
        End If

        If CDbl(txtm3perCycle.Text) <= 0 Then
            MsgBox("m3 per Cycle value must be greater than 0", vbExclamation, Application.ProductName)
            txtm3perCycle.Focus()
        End If

        Return True

    End Function

    ' Returns the total volume compressed or regular for the belt sent
    Private Function GetTotalVolume() As Double
        Dim SQL As String = ""
        Dim readerBelts As SQLiteDataReader
        Dim checkedItems As ListView.CheckedListViewItemCollection
        Dim IceName As String
        Dim IceUnits As Long
        Dim IceVolume As Double
        Dim TotalIceVolume As Double = 0

        Dim item As ListViewItem

        ' Just work with the ones that are checked
        checkedItems = lstIce.CheckedItems

        If checkedItems.Count > 0 Then
            ' For each row of Ice, look up the volume and total
            For Each item In checkedItems

                IceName = CType(item.SubItems(1).Text, String)
                IceUnits = CType(item.SubItems(2).Text, Integer)

                SQL = "SELECT ORE_VOLUME FROM ORES WHERE BELT_TYPE = 'Ice' "
                SQL &= "AND ORE_NAME = '" & IceName & "'"
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerBelts = DBCommand.ExecuteReader

                If readerBelts.Read Then
                    ' Get all the base values
                    IceVolume = readerBelts.GetDouble(0)
                Else
                    Return 0
                End If

                readerBelts.Close()

                TotalIceVolume = TotalIceVolume + (IceVolume * IceUnits)

            Next

        End If

        Return TotalIceVolume

    End Function

#Region "Object Processing"

    Private Sub cmbNumMiners_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbNumMiners.SelectedIndexChanged
        If Not FirstLoad Then
            Call RefreshGrids()
        End If
    End Sub

    Private Sub TextBoxes_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCycleTime.KeyDown, cmbNumMiners.KeyDown, txtm3perCycle.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab And Not FirstLoad Then
            Call RefreshGrids()
        End If
    End Sub

    Private Sub Text_LostFocus(sender As Object, e As EventArgs) Handles txtCycleTime.LostFocus, cmbNumMiners.LostFocus, txtm3perCycle.LostFocus
        If Not FirstLoad Then
            Call RefreshGrids()
        End If
    End Sub

    Private Sub listIce_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lstIce.ColumnClick
        Call ListViewColumnSorter(e.Column, lstIce, IceColumnClicked, IceColumnSortOrder)
    End Sub

    Private Sub lstIceProducts_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstIceProducts.ColumnClick
        Call ListViewColumnSorter(e.Column, lstIceProducts, ProductColumnClicked, ProductColumnSortOrder)
    End Sub

    Private Sub listIce_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lstIce.ItemChecked
        Dim s As ListView = CType(sender, ListView)

        If Not FirstLoad And CType(s.ContainsFocus, Boolean) Then
            Call RefineIce()
        End If
    End Sub

    Private Sub btnCloseSmall_Click(sender As Object, e As EventArgs) Handles btnCloseSmall.Click
        Me.Hide()
    End Sub

    Private Sub btnRefine_Click(sender As Object, e As EventArgs) Handles btnRefine.Click
        Call RefineIce()
    End Sub

    Private Sub rbtnCheckedChanged(sender As Object, e As EventArgs) Handles rbtnHighsec.CheckedChanged, rbtnLowsec.CheckedChanged,
                                                                                   rbtnNullWeak.CheckedChanged, rbtnNullStrong.CheckedChanged,
                                                                                   rbtnAmarr.CheckedChanged, rbtnCaldari.CheckedChanged,
                                                                                   rbtnGallente.CheckedChanged, rbtnMinmatar.CheckedChanged
        If Not FirstLoad And CType(sender, RadioButton).Checked = True Then
            Call RefreshGrids()
        End If
    End Sub

    Private Sub CheckChanged(sender As Object, e As EventArgs) Handles chkCompressIce.CheckedChanged, chkIPHperMiner.CheckedChanged
        If Not FirstLoad Then
            Call RefreshGrids()
        End If
    End Sub

    Private Sub chkBrokerFees_Click(sender As Object, e As EventArgs) Handles chkBrokerFees.Click
        If chkBrokerFees.Checked And chkBrokerFees.CheckState = CheckState.Indeterminate Then ' Show rate box
            txtBrokerFeeRate.Visible = True
        Else
            txtBrokerFeeRate.Visible = False
        End If
    End Sub

    Private Sub txtBrokerFeeRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBrokerFeeRate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBrokerFeeRate.Text = GetFormattedPercentEntry(txtBrokerFeeRate)
        End If
    End Sub

    Private Sub txtBrokerFeeRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBrokerFeeRate.KeyPress
        ' Only allow numbers, decimal, percent or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtBrokerFeeRate_GotFocus(sender As Object, e As EventArgs) Handles txtBrokerFeeRate.GotFocus
        Call txtBrokerFeeRate.SelectAll()
    End Sub

    Private Sub txtBrokerFeeRate_LostFocus(sender As Object, e As EventArgs) Handles txtBrokerFeeRate.LostFocus
        txtBrokerFeeRate.Text = GetFormattedPercentEntry(txtBrokerFeeRate)
    End Sub

    Private Sub frmIceBeltFlip_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        IceBeltFlipOpen = False
    End Sub

#End Region

End Class