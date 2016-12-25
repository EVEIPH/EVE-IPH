
Imports System.Data.SQLite

Public Class frmPOSSettings

    Private m_ControlsCollection As ControlsCollection

    Private POSTextBoxes() As TextBox
    Private POSPictures() As PictureBox
    Private POSLabels() As Label

    Private POSFuelPricesUpdated As Boolean
    Private POSFuelBlockPricesUpdated As Boolean
    Private SelectedTowerRaceID As Integer
    Private LoadingTab As Boolean

    Private Const NotApplicable As String = "N/A"

#Region "Object Fuctions"

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

    Private Sub rbtnPOSBuyBlocks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnPOSBuyBlocks.CheckedChanged
        If Not LoadingTab Then
            If rbtnPOSBuildBlocks.Checked Then
                ' Set the build cost first
                Call SetFuelBlockBuildcost()

                txtPOSFuelBlockBPME.Enabled = True
                lblPOSFuelBlockBPME.Enabled = True
                btnPOSUpdateBlockPrice.Enabled = False
                btnPOSUpdateFuelPrices.Enabled = True
                btnRefreshBlockData.Enabled = True
                gbPOSFuelPrices.Enabled = True
                txtPOSFuelBlockBuy.Enabled = False
                txtPOSFuelBlockBPME.Enabled = True
            Else ' Buy blocks
                ' Refresh the price
                Call LoadPOSFuelBlockPrice()

                btnPOSUpdateBlockPrice.Enabled = True
                btnPOSUpdateFuelPrices.Enabled = False
                btnRefreshBlockData.Enabled = False
                txtPOSFuelBlockBPME.Enabled = False
                lblPOSFuelBlockBPME.Enabled = False
                gbPOSFuelPrices.Enabled = False
                txtPOSFuelBlockBuy.Enabled = True
                txtPOSFuelBlockBPME.Enabled = False

            End If
            Call UpdateCosts()
        End If
    End Sub

    Private Sub rbtnPOSBuildBlocks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnPOSBuildBlocks.CheckedChanged
        If Not LoadingTab Then
            If rbtnPOSBuildBlocks.Checked Then
                If cmbPOSTower.Text = None Then
                    MsgBox("You need to select a tower to set build parameters", vbExclamation, Application.ProductName)
                    rbtnPOSBuyBlocks.Checked = True
                    cmbPOSTower.Focus()
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub cmbPOSTower_GotFocus(sender As Object, e As System.EventArgs) Handles cmbPOSTower.GotFocus
        Call cmbPOSTower.SelectAll()
    End Sub

    Private Sub cmbPOSTower_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPOSTower.SelectedIndexChanged
        If Not LoadingTab Then
            Call LoadPOSData()
        End If
    End Sub

    Private Sub cmbPOSTower_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPOSTower.DropDown
        Dim SQL As String
        Dim readerPOS As SQLiteDataReader

        SQL = "SELECT ITEM_NAME FROM ALL_BLUEPRINTS "
        SQL = SQL & "WHERE ITEM_GROUP = 'Control Tower' "

        If rbtnPOSNPCTowers.Checked Then
            SQL = SQL & "AND ITEM_TYPE = 1 "
        Else
            SQL = SQL & "AND ITEM_TYPE = 15 "
        End If

        If rbtnPOSSizeSmall.Checked Then
            SQL = SQL & "AND ITEM_NAME LIKE '%Small%' "
        ElseIf rbtnPOSSizeMedium.Checked Then
            SQL = SQL & "AND ITEM_NAME LIKE '%Medium%' "
        ElseIf rbtnPOSSizeLarge.Checked Then
            SQL = SQL & "AND (ITEM_NAME NOT LIKE '%Small%' AND ITEM_NAME NOT LIKE '%Medium%') "
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerPOS = DBCommand.ExecuteReader

        cmbPOSTower.Items.Clear()

        While readerPOS.Read()
            cmbPOSTower.Items.Add(readerPOS.GetString(0))
        End While

    End Sub

    Private Sub btnUpdatePOSMatPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOSUpdateFuelPrices.Click
        Call UpdatePOSFuelPrices()
    End Sub

    Private Sub btnPOSUpdateBlockPrices_Click(sender As System.Object, e As System.EventArgs) Handles btnPOSUpdateBlockPrice.Click
        Call UpdatePOSFuelBlockPrices()
    End Sub

    Private Sub btnClosePOSFuel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePOSFuel.Click
        Me.Hide()
    End Sub

    Private Sub btnRefreshPOSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshBlockData.Click
        ' Load the ME data for the bp
        Call UpdateFuelBlockData(cmbPOSTower.Text, False)
    End Sub

    Private Sub rbtnPOSNPCTowers_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPOSNPCTowers.CheckedChanged
        If Not LoadingTab Then
            cmbPOSTower.Text = None
            cmbPOSTower.Focus()
        End If
    End Sub

    Private Sub rbtnPOSFactionTowers_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPOSFactionTowers.CheckedChanged
        If Not LoadingTab Then
            cmbPOSTower.Text = None
            cmbPOSTower.Focus()
        End If
    End Sub

    Private Sub txtPOSFuelBlock_GotFocus(sender As Object, e As System.EventArgs)
        Call txtPOSFuelBlockBuy.SelectAll()
    End Sub

    Private Sub txtPOSFuelBlockBPME_GotFocus(sender As Object, e As System.EventArgs) Handles txtPOSFuelBlockBPME.GotFocus
        Call cmbPOSTower.SelectAll()
    End Sub

    Private Sub txtPOSFuelBlockBPME_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPOSFuelBlockBPME.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPOSFuelBlockBPME_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOSFuelBlockBPME.KeyDown
        Call ProcessCutCopyPasteSelect(txtPOSFuelBlockBPME, e)
        If e.KeyCode = Keys.Enter Then
            Call SetFuelBlockBuildcost()
        End If
    End Sub

    Private Sub btnPOSFuelReset_Click(sender As System.Object, e As System.EventArgs) Handles btnPOSFuelReset.Click
        Call LoadPOSDataTab()
    End Sub

    Private Sub rbtnPOSSizeLarge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPOSSizeLarge.CheckedChanged
        If Not LoadingTab Then
            cmbPOSTower.Text = None
            cmbPOSTower.Focus()
        End If
    End Sub

    Private Sub rbtnPOSSizeMedium_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPOSSizeMedium.CheckedChanged
        If Not LoadingTab Then
            cmbPOSTower.Text = None
            cmbPOSTower.Focus()
        End If
    End Sub

    Private Sub rbtnPOSSizeSmall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPOSSizeSmall.CheckedChanged
        If Not LoadingTab Then
            cmbPOSTower.Text = None
            cmbPOSTower.Focus()
        End If
    End Sub

    Private Sub txtPOSFuelBlockBuy_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOSFuelBlockBuy.KeyDown
        Call ProcessCutCopyPasteSelect(txtPOSFuelBlockBuy, e)
        If e.KeyCode = Keys.Enter Then
            Call UpdatePOSFuelBlockPrices()
        End If
    End Sub

    Private Sub txtPOSFuelBlockBuy_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOSFuelBlockBuy.TextChanged
        POSFuelBlockPricesUpdated = True
    End Sub

    Private Sub txtPOS1_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS1.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS2_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS2.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS3_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS3.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS4_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS4.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS5_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS5.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS6_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS6.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS7_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS7.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS8_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS8.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS9_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS9.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS10_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS10.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS11_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS11.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtPOS12_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS12.TextChanged
        POSFuelPricesUpdated = True
    End Sub

    Private Sub txtCharters_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCharters.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

#End Region

    Public Sub New()

        LoadingTab = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Create the controls collection class for text boxes
        m_ControlsCollection = New ControlsCollection(Me)

        ' Add any initialization after the InitializeComponent() call.
        POSTextBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "txtPOS"), TextBox())
        POSPictures = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "pictPOS"), PictureBox())
        POSLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblPOS"), Label())

        LoadPOSDataTab()

        LoadingTab = False

    End Sub

    Private Sub LoadPOSDataTab()

        txtPOSFuelBlockBPME.Text = "0"
        cmbPOSTower.Text = SelectedTower.TowerName
        SelectedTowerRaceID = SelectedTower.TowerRaceID


        rbtnPOSNPCTowers.Checked = True
        rbtnPOSSizeLarge.Checked = True

        ' Building
        If SelectedTower.FuelBlockBuild Then
            rbtnPOSBuildBlocks.Checked = True
            gbPOSFuelPrices.Enabled = True
            btnPOSUpdateBlockPrice.Enabled = False
            btnPOSUpdateFuelPrices.Enabled = True
            btnRefreshBlockData.Enabled = True
            txtPOSFuelBlockBPME.Enabled = True
            lblPOSFuelBlockBPME.Enabled = True
            txtPOSFuelBlockBuy.Enabled = False
        Else ' Buying
            rbtnPOSBuyBlocks.Checked = True
            gbPOSFuelPrices.Enabled = False
            btnPOSUpdateBlockPrice.Enabled = True
            btnPOSUpdateFuelPrices.Enabled = False
            btnRefreshBlockData.Enabled = False
            txtPOSFuelBlockBPME.Enabled = False
            lblPOSFuelBlockBPME.Enabled = False
            txtPOSFuelBlockBuy.Enabled = True
        End If

        If SelectedTower.TowerType = "Standard" Then
            rbtnPOSNPCTowers.Checked = True
        ElseIf SelectedTower.TowerType = "Faction" Then
            rbtnPOSFactionTowers.Checked = True
        End If

        Select Case SelectedTower.TowerSize
            Case "Large"
                rbtnPOSSizeLarge.Checked = True
            Case "Medium"
                rbtnPOSSizeMedium.Checked = True
            Case "Small"
                rbtnPOSSizeSmall.Checked = True
            Case Else
                rbtnPOSSizeLarge.Checked = True
        End Select


        lblPOSCostperHour.Text = FormatNumber(SelectedTower.CostperHour, 2)
        lblPOSCostperDay.Text = FormatNumber(SelectedTower.CostperHour * 24, 2)
        lblPOSCostperMonth.Text = FormatNumber(SelectedTower.CostperHour * 24 * 30, 2)

        txtCharters.Text = FormatNumber(SelectedTower.CharterCost, 2)

        Call LoadPOSFuelPrices()
        ' Load both the block build and buy prices
        Call LoadPOSFuelBlockPrice()
        Call SetFuelBlockBuildcost()

        If cmbPOSTower.Text <> None Then
            ' Have a pos selected so load the data
            Call LoadPOSData()
        End If

        cmbPOSTower.Focus()

    End Sub

    Public Sub LoadPOSData()

        ' Load the block data
        Call UpdateFuelBlockData(cmbPOSTower.Text, True)
        Call LoadPOSFuelBlockPrice()
        Call UpdateCosts()

    End Sub

    Private Sub UpdateFuelBlockData(ByVal TowerName As String, ReloadME As Boolean)
        Dim SQL As String
        Dim readerPOS As SQLiteDataReader
        Dim FuelBlock As String = ""

        SQL = "SELECT raceID FROM INVENTORY_TYPES WHERE typeName ='" & TowerName & "' "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerPOS = DBCommand.ExecuteReader()

        If readerPOS.Read Then
            SelectedTowerRaceID = readerPOS.GetInt32(0)
        Else
            MsgBox("Unknown Tower. Cannot calculate.", vbExclamation, Application.ProductName)
            Exit Sub
        End If

        picPOSAmarrFuelBlock.Visible = False
        picPOSCaldariFuelBlock.Visible = False
        picPOSGallenteFuelBlock.Visible = False
        picPOSMinmatarFuelBlock.Visible = False

        ' Based on the race of the tower, choose the type of fuel block it will use
        Select Case SelectedTowerRaceID
            Case 1
                FuelBlock = "Caldari Fuel Block"
                lblPOSFuelBlock.Text = "Caldari"
                picPOSCaldariFuelBlock.Visible = True
            Case 2
                FuelBlock = "Minmatar Fuel Block"
                lblPOSFuelBlock.Text = "Minmatar"
                picPOSMinmatarFuelBlock.Visible = True
            Case 4
                FuelBlock = "Amarr Fuel Block"
                lblPOSFuelBlock.Text = "Amarr"
                picPOSAmarrFuelBlock.Visible = True
            Case 8
                FuelBlock = "Gallente Fuel Block"
                lblPOSFuelBlock.Text = "Gallente"
                picPOSGallenteFuelBlock.Visible = True
        End Select

        ' Reload the ME if we need too
        If ReloadME Then
            Call LoadBlockBPME(FuelBlock)
        End If

        ' Build the block value if we are building
        If rbtnPOSBuildBlocks.Checked Then
            Call SetFuelBlockBuildcost()
        End If

    End Sub

    Private Sub LoadBlockBPME(FuelBlockName As String)
        ' Load the ME for the type of block that we are using for this tower
        Dim SQL As String
        Dim readerPOS As SQLiteDataReader

        SQL = "SELECT ME FROM OWNED_BLUEPRINTS, ALL_BLUEPRINTS "
        SQL = SQL & "WHERE ALL_BLUEPRINTS.BLUEPRINT_ID = OWNED_BLUEPRINTS.BLUEPRINT_ID "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerPOS = DBCommand.ExecuteReader()

        If readerPOS.Read Then
            ' Owned and they have it
            txtPOSFuelBlockBPME.Text = CStr(readerPOS.GetValue(0))
        Else
            txtPOSFuelBlockBPME.Text = "0"
        End If

    End Sub

    Private Sub UpdatePOSFuelPrices()
        Dim SQL As String
        Dim i As Integer
        Dim Prices() As Double

        If POSFuelPricesUpdated Then
            Me.Cursor = Cursors.WaitCursor

            ReDim Prices(POSTextBoxes.Count - 1)

            ' Check the prices first
            For i = 1 To POSTextBoxes.Count - 1
                If Not IsNumeric(POSTextBoxes(i).Text) Then
                    MsgBox("Invalid " & POSLabels(i).Text & " Price", vbExclamation, Me.Text)
                    POSTextBoxes(i).Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    Prices(i) = CDbl(POSTextBoxes(i).Text)
                End If
            Next

            ' Update all the prices
            For i = 1 To POSTextBoxes.Count - 1
                SQL = "UPDATE ITEM_PRICES SET PRICE = " & Prices(i) & ", PRICE_TYPE = 'User' WHERE ITEM_NAME = '" & POSLabels(i).Text & "'"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            Next

            MsgBox("Prices Updated", vbInformation, Me.Text)
            Me.Cursor = Cursors.Default

            ' Update the block data
            Call SetFuelBlockBuildcost()
        Else
            MsgBox("No Prices were Updated", vbInformation, Me.Text)
        End If

        ' Refresh the prices
        Call LoadPOSFuelPrices()

    End Sub

    Private Sub LoadPOSFuelPrices()
        Dim SQL As String
        Dim readerPOS As SQLiteDataReader

        Me.Cursor = Cursors.WaitCursor

        SQL = "SELECT ITEM_PRICES.ITEM_NAME, ITEM_PRICES.PRICE "
        SQL = SQL & "FROM ITEM_PRICES "
        SQL = SQL & "WHERE ITEM_PRICES.ITEM_NAME IN "
        SQL = SQL & "('Hydrogen Isotopes','Oxygen Isotopes','Nitrogen Isotopes','Helium Isotopes','Strontium Clathrates',"
        SQL = SQL & "'Heavy Water','Liquid Ozone','Robotics','Oxygen','Mechanical Parts','Coolant','Enriched Uranium')"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerPOS = DBCommand.ExecuteReader()

        While readerPOS.Read
            ' Update the textboxes and images
            For i = 1 To POSTextBoxes.Count - 1
                If POSLabels(i).Text = readerPOS.GetString(0) Then
                    POSTextBoxes(i).Text = FormatNumber(readerPOS.GetDouble(1), 2) ' Price
                End If
            Next
            Application.DoEvents()
        End While

        Me.Cursor = Cursors.Default
        txtPOS1.Focus()

        readerPOS.Close()
        readerPOS = Nothing
        DBCommand = Nothing

        POSFuelPricesUpdated = False

    End Sub

    Private Sub UpdatePOSFuelBlockPrices()
        Dim SQL As String

        If POSFuelBlockPricesUpdated Then
            Me.Cursor = Cursors.WaitCursor

            ' Check the prices first

            If Not IsNumeric(txtPOSFuelBlockBuy.Text) Then
                MsgBox("Invalid Fuel Block Price", vbExclamation, Application.ProductName)
                txtPOSFuelBlockBuy.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ' Update the prices
            SQL = "UPDATE ITEM_PRICES SET PRICE = " & CDec(txtPOSFuelBlockBuy.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_NAME = '" & lblPOSFuelBlock.Text & " Fuel Block'"
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            MsgBox("Prices Updated", vbInformation, Me.Text)
            Me.Cursor = Cursors.Default
        Else
            MsgBox("No Prices were Updated", vbInformation, Me.Text)
        End If

        ' Refresh the prices
        Call LoadPOSFuelBlockPrice()

    End Sub

    Private Sub LoadPOSFuelBlockPrice()
        Dim SQL As String
        Dim readerPOS As SQLiteDataReader

        Me.Cursor = Cursors.WaitCursor

        readerPOS = Nothing
        DBCommand = Nothing

        If cmbPOSTower.Text <> None Then
            ' Load the fuel block price
            SQL = "SELECT ITEM_PRICES.ITEM_NAME, ITEM_PRICES.PRICE "
            SQL = SQL & "FROM ITEM_PRICES, INVENTORY_TYPES "
            SQL = SQL & "WHERE ITEM_PRICES.ITEM_NAME = "
            Select Case SelectedTowerRaceID
                Case 1
                    SQL = SQL & "'Caldari Fuel Block' "
                Case 2
                    SQL = SQL & "'Minmatar Fuel Block' "
                Case 4
                    SQL = SQL & "'Amarr Fuel Block' "
                Case 8
                    SQL = SQL & "'Gallente Fuel Block' "
            End Select
            SQL = SQL & "AND INVENTORY_TYPES.typeID = ITEM_PRICES.ITEM_ID "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerPOS = DBCommand.ExecuteReader()
            readerPOS.Read()

            txtPOSFuelBlockBuy.Text = FormatNumber(readerPOS.GetValue(1))
            readerPOS.Close()
        Else
            txtPOSFuelBlockBuy.Text = "0.00"
        End If

        Me.Cursor = Cursors.Default

        readerPOS = Nothing
        DBCommand = Nothing

        POSFuelBlockPricesUpdated = False

    End Sub

    Private Sub UpdateCosts()
        Dim CostperBlock As Double
        Dim CostperHour As Double
        Dim Multiplier As Integer

        ' Get the block we are using
        If rbtnPOSBuildBlocks.Checked Then
            CostperBlock = CDbl(lblPOSFuelBlockBuild.Text)
        Else
            CostperBlock = CDbl(txtPOSFuelBlockBuy.Text)
        End If

        '10 block/hour for small towers, 20 blocks/hour for medium and 40 blocks/hour for large
        If rbtnPOSNPCTowers.Checked Then
            If rbtnPOSSizeLarge.Checked Then
                Multiplier = 40
            ElseIf rbtnPOSSizeMedium.Checked Then
                Multiplier = 20
            ElseIf rbtnPOSSizeSmall.Checked Then
                Multiplier = 10
            End If
        Else
            If rbtnPOSSizeLarge.Checked Then
                Multiplier = 32
            ElseIf rbtnPOSSizeMedium.Checked Then
                Multiplier = 16
            ElseIf rbtnPOSSizeSmall.Checked Then
                Multiplier = 8
            End If
        End If

        CostperHour = CostperBlock * Multiplier
        lblPOSCostperHour.Text = FormatNumber(CostperHour, 2)
        lblPOSCostperDay.Text = FormatNumber(CostperHour * 24, 2)
        lblPOSCostperMonth.Text = FormatNumber(CostperHour * 24 * 30, 2)

    End Sub

    Private Sub SetFuelBlockBuildcost()

        ' Make sure it's valid
        If Not IsNumeric(txtPOSFuelBlockBPME.Text) Then
            MsgBox("Invalid Fuel Block BPO ME", vbExclamation, Application.ProductName)
            txtPOSFuelBlockBPME.Focus()
            Exit Sub
        End If

        If Trim(txtCharters.Text) = "" Or Not IsNumeric(txtCharters.Text) Then
            MsgBox("Invalid Charter Cost", vbExclamation, Application.ProductName)
            txtCharters.Focus()
            Exit Sub
        End If

        ' First set all to 0 so we only build for the tower we are using
        lblPOSFuelBlockBuild.Text = "0.00"

        ' Get cost for building 1 block and add charter cost (divide by 40 since that's the number of blocks that is made for 1 charter)
        Select Case SelectedTowerRaceID
            Case 1
                lblPOSFuelBlockBuild.Text = FormatNumber(GetFuelBlockBuildCost("Caldari Fuel Block", CInt(txtPOSFuelBlockBPME.Text)) + (CInt(txtCharters.Text) / 40), 2)
            Case 2
                lblPOSFuelBlockBuild.Text = FormatNumber(GetFuelBlockBuildCost("Minmatar Fuel Block", CInt(txtPOSFuelBlockBPME.Text)) + (CInt(txtCharters.Text) / 40), 2)
            Case 4
                lblPOSFuelBlockBuild.Text = FormatNumber(GetFuelBlockBuildCost("Amarr Fuel Block", CInt(txtPOSFuelBlockBPME.Text)) + (CInt(txtCharters.Text) / 40), 2)
            Case 8
                lblPOSFuelBlockBuild.Text = FormatNumber(GetFuelBlockBuildCost("Gallente Fuel Block", CInt(txtPOSFuelBlockBPME.Text)) + (CInt(txtCharters.Text) / 40), 2)
        End Select

    End Sub

    Private Function GetFuelBlockBuildCost(FuelBlock As String, bpME As Integer) As Double
        Dim SQL As String
        Dim readerPOS As SQLiteDataReader


        SQL = "SELECT BLUEPRINT_ID FROM ALL_BLUEPRINTS WHERE ITEM_NAME = '" & FuelBlock & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerPOS = DBCommand.ExecuteReader()

        If readerPOS.Read Then
            ' Build T1 BP for the block, standard settings - CHECK
            Dim BlockBP = New Blueprint(readerPOS.GetInt64(0), 1, bpME, 0, 1, 1, SelectedCharacter, UserApplicationSettings, False, 0, NoTeam,
                                        SelectedBPManufacturingFacility, NoTeam, SelectedBPComponentManufacturingFacility, SelectedBPCapitalComponentManufacturingFacility)
            Call BlockBP.BuildItems(False, False, False, False, False)
            Return BlockBP.GetRawItemUnitPrice
        Else
            Return 0
        End If

    End Function

    Private Sub btnPOSSaveTower_Click(sender As System.Object, e As System.EventArgs) Handles btnPOSSaveTower.Click
        Dim TempSettings As New ProgramSettings
        Dim TempTower As PlayerOwnedStationSettings = Nothing

        If cmbPOSTower.Text = None Then
            MsgBox("No Tower Selected", vbExclamation, Application.ProductName)
            cmbPOSTower.Focus()
            Exit Sub
        End If

        If Trim(txtCharters.Text) = "" Or Not IsNumeric(txtCharters.Text) Then
            MsgBox("Invalid Charter Cost", vbExclamation, Application.ProductName)
            txtCharters.Focus()
            Exit Sub
        End If

        Application.UseWaitCursor = True

        ' Get the data
        TempTower.TowerRaceID = SelectedTowerRaceID
        TempTower.TowerName = cmbPOSTower.Text
        TempTower.CostperHour = CDbl(lblPOSCostperHour.Text)

        ' Tower type
        If rbtnPOSNPCTowers.Checked Then
            TempTower.TowerType = "Standard"
        ElseIf rbtnPOSFactionTowers.Checked Then
            TempTower.TowerType = "Faction"
        End If

        If rbtnPOSSizeLarge.Checked Then
            TempTower.TowerSize = "Large"
        ElseIf rbtnPOSSizeMedium.Checked Then
            TempTower.TowerSize = "Medium"
        ElseIf rbtnPOSSizeSmall.Checked Then
            TempTower.TowerSize = "Small"
        End If

        If rbtnPOSBuildBlocks.Checked Then
            TempTower.FuelBlockBuild = True
        ElseIf rbtnPOSBuyBlocks.Checked Then
            TempTower.FuelBlockBuild = False
        End If

        TempTower.CharterCost = CDbl(txtCharters.Text)

        ' Save the data in the XML file
        Call TempSettings.SavePOSSettings(TempTower)

        ' Save the data to the local variable
        SelectedTower = TempTower

        MsgBox("Tower Settings Saved", vbInformation, Application.ProductName)
        cmbPOSTower.Focus()
        Application.UseWaitCursor = False

    End Sub

    Private Sub gbPOSFuelPrices_Enter(sender As Object, e As EventArgs) Handles gbPOSFuelPrices.Enter

    End Sub

    Private Sub gbPOSFuelBlocks_Enter(sender As Object, e As EventArgs) Handles gbPOSFuelBlocks.Enter

    End Sub
End Class