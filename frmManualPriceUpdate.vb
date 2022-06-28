
Imports System.Data.SQLite

' Form allows the user to manually update prices in the database
' The update prices tab will override the prices set here if scheduled to be updated
Public Class frmManualPriceUpdate

    Private m_ControlsCollection As ControlsCollection

    Private MineralTextBoxes() As TextBox
    Private MineralPictures() As PictureBox
    Private MineralLabels() As Label

    Private MoonTextBoxes() As TextBox
    Private MoonPictures() As PictureBox
    Private MoonLabels() As Label

    Private MineralPricesUpdated As Boolean
    Private MoonPricesUpdated As Boolean
    Private POSPricesUpdated As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Width is 382 for columns on manual update - add the columns
        lstPriceLookup.Columns.Add("Material", 261, HorizontalAlignment.Left)
        lstPriceLookup.Columns.Add("Price", 100, HorizontalAlignment.Right)

        ' Create the controls collection class for text boxes
        m_ControlsCollection = New ControlsCollection(Me)

        ' Get controls (note index starts at 1)
        MineralTextBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "txtMineral"), TextBox())
        MineralPictures = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "pictMineral"), PictureBox())
        MineralLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblMineral"), Label())

        MoonTextBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "txtMoon"), TextBox())
        MoonPictures = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "pictMoon"), PictureBox())
        MoonLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblMoon"), Label())

        'POSTextBoxes = ControlArrayUtils.getControlArray(Me, Me.MyControls, "txtPOS")
        'POSPictures = ControlArrayUtils.getControlArray(Me, Me.MyControls, "pictPOS")
        'POSLabels = ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblPOS")

        ' Load the prices
        Call LoadMineralPrices()
        Application.DoEvents()

    End Sub

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

    ' Functions and procedures to update minerals
#Region "Minerals Tab"

    Public Sub LoadMineralPrices()
        Dim SQL As String
        Dim readerMinerals As SQLiteDataReader
        Me.Cursor = Cursors.WaitCursor

        SQL = "SELECT ITEM_PRICES.ITEM_NAME, ITEM_PRICES.PRICE "
        SQL &= "FROM ITEM_PRICES "
        SQL &= "WHERE ITEM_PRICES.ITEM_NAME IN ('Tritanium','Pyerite','Mexallon','Nocxium','Isogen','Zydrine','Megacyte','Morphite')"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerMinerals = DBCommand.ExecuteReader()

        While readerMinerals.Read
            ' Update the textboxes and images
            For i = 1 To MineralTextBoxes.Count - 1
                If MineralLabels(i).Text = readerMinerals.GetString(0) Then
                    MineralTextBoxes(i).Text = FormatNumber(readerMinerals.GetDouble(1), 2) ' Price
                End If
            Next
            Application.DoEvents()
        End While

        Me.Cursor = Cursors.Default
        txtMineral1.Focus()

        readerMinerals.Close()
        readerMinerals = Nothing
        DBCommand = Nothing

        MineralPricesUpdated = False

    End Sub

    ' Updates all the prices if they are changed
    Private Sub UpdateMineralPrices()
        Dim SQL As String
        Dim i As Integer
        Dim Prices() As Double

        If MineralPricesUpdated Then
            Me.Cursor = Cursors.WaitCursor

            ReDim Prices(MineralTextBoxes.Count - 1)

            ' Check the prices first
            For i = 1 To MineralTextBoxes.Count - 1
                If Not IsNumeric(MineralTextBoxes(i).Text) Then
                    MsgBox("Invalid " & MineralLabels(i).Text & " Price", vbExclamation, Me.Text)
                    MineralTextBoxes(i).Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    Prices(i) = CDbl(MineralTextBoxes(i).Text)
                End If
            Next

            ' Update all the prices
            For i = 1 To MineralTextBoxes.Count - 1
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & Prices(i) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & GetTypeID(MineralLabels(i).Text)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            Next

            MsgBox("Prices Updated", vbInformation, Me.Text)
            Me.Cursor = Cursors.Default
        Else
            MsgBox("No Prices were Updated", vbInformation, Me.Text)
        End If

        ' Finally update the Program prices
        Call UpdateProgramPrices()

    End Sub

    Private Sub btnUpdateMineralPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMineralPrices.Click
        Call UpdateMineralPrices()
    End Sub

    Private Sub MineralTextBoxes_GotFocus(ByVal index As Integer, ByRef e As System.EventArgs)
        Call MineralTextBoxes(index).SelectAll()
    End Sub

    Private Sub MineralTextBoxes_KeyPress(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub MineralTextBoxes_KeyDown(SentTextBox As TextBox, ByVal e As System.Windows.Forms.KeyEventArgs)
        Call ProcessCutCopyPasteSelect(SentTextBox, e)
        If e.KeyCode = Keys.Enter Then
            Call UpdateMineralPrices()
        End If
    End Sub

    Private Sub txtMineral1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral1.GotFocus
        Call MineralTextBoxes_GotFocus(1, e)
    End Sub

    Private Sub txtMineral1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral1.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral1, e)
    End Sub

    Private Sub txtMineral1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral1.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral1.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral2.GotFocus
        Call MineralTextBoxes_GotFocus(2, e)
    End Sub

    Private Sub txtMineral2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral2.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral2, e)
    End Sub

    Private Sub txtMineral2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral2.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral2.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral3.GotFocus
        Call MineralTextBoxes_GotFocus(3, e)
    End Sub

    Private Sub txtMineral3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral3.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral3, e)
    End Sub

    Private Sub txtMineral3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral3.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral3.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral4.GotFocus
        Call MineralTextBoxes_GotFocus(4, e)
    End Sub

    Private Sub txtMineral4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral4.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral4, e)
    End Sub

    Private Sub txtMineral4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral4.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral4.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral5.GotFocus
        Call MineralTextBoxes_GotFocus(5, e)
    End Sub

    Private Sub txtMineral5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral5.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral5, e)
    End Sub

    Private Sub txtMineral5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral5.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral5.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral6_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral6.GotFocus
        Call MineralTextBoxes_GotFocus(6, e)
    End Sub

    Private Sub txtMineral6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral6.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral6, e)
    End Sub

    Private Sub txtMineral6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral6.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral6.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral7.GotFocus
        Call MineralTextBoxes_GotFocus(7, e)
    End Sub

    Private Sub txtMineral7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral7.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral7, e)
    End Sub

    Private Sub txtMineral7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral7.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral7.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub txtMineral8_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral8.GotFocus
        Call MineralTextBoxes_GotFocus(8, e)
    End Sub

    Private Sub txtMineral8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMineral8.KeyDown
        Call MineralTextBoxes_KeyDown(txtMineral8, e)
    End Sub

    Private Sub txtMineral8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineral8.KeyPress
        Call MineralTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMineral8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMineral8.TextChanged
        MineralPricesUpdated = True
    End Sub

    Private Sub btnCloseMinerals_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseMinerals.Click
        Me.Hide()
    End Sub

#End Region

    ' Functions and procedures to update moon materials
#Region "Moon Materials Tab"

    Public Sub LoadMoonPrices()
        Dim SQL As String
        Dim readerMoon As SQLiteDataReader

        Me.Cursor = Cursors.WaitCursor

        SQL = "SELECT ITEM_PRICES.ITEM_NAME, ITEM_PRICES.PRICE "
        SQL &= "FROM ITEM_PRICES "
        SQL &= "WHERE ITEM_PRICES.ITEM_NAME IN "
        SQL &= "('Ferrogel','Crystalline Carbonide','Fermionic Condensates','Titanium Carbide','Fullerides',"
        SQL &= "'Hypersynaptic Fibers','Nanotransistors','Phenolic Composites','Tungsten Carbide','Sylramic Fibers','Fernite Carbide')"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerMoon = DBCommand.ExecuteReader()

        While readerMoon.Read
            ' Update the textboxes and images
            For i = 1 To MoonTextBoxes.Count - 1
                If MoonLabels(i).Text = readerMoon.GetString(0) Then
                    MoonTextBoxes(i).Text = FormatNumber(readerMoon.GetDouble(1), 2) ' Price
                End If
            Next
            Application.DoEvents()
        End While

        Me.Cursor = Cursors.Default
        txtMoon1.Focus()

        readerMoon.Close()
        readerMoon = Nothing
        DBCommand = Nothing

        MoonPricesUpdated = False

    End Sub

    Private Sub UpdateMoonPrices()
        Dim SQL As String
        Dim i As Integer
        Dim Prices() As Double

        If MoonPricesUpdated Then
            Me.Cursor = Cursors.WaitCursor

            ReDim Prices(MoonTextBoxes.Count - 1)

            ' Check the prices first
            For i = 1 To MoonTextBoxes.Count - 1
                If Not IsNumeric(MoonTextBoxes(i).Text) Then
                    MsgBox("Invalid " & MoonLabels(i).Text & " Price", vbExclamation, Me.Text)
                    MoonTextBoxes(i).Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    Prices(i) = CDbl(MoonTextBoxes(i).Text)
                End If
            Next

            ' Update all the prices
            For i = 1 To MoonTextBoxes.Count - 1
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & Prices(i) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & GetTypeID(MoonLabels(i).Text)
                Call evedb.ExecuteNonQuerySQL(SQL)
            Next

            MsgBox("Prices Updated", vbInformation, Me.Text)
            Me.Cursor = Cursors.Default
        Else
            MsgBox("No Prices were Updated", vbInformation, Me.Text)
        End If

        ' Finally update the Program prices
        Call UpdateProgramPrices()

    End Sub

    ' Updates all the prices if they are changed
    Private Sub btnUpdateMoonMatPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMoonMatPrices.Click
        Call UpdateMoonPrices()
    End Sub

    Private Sub MoonTextBoxes_GotFocus(ByVal index As Integer, ByRef e As System.EventArgs)
        Call MoonTextBoxes(index).SelectAll()
    End Sub

    Private Sub MoonTextBoxes_KeyPress(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub MoonTextBoxes_KeyDown(SentTextBox As TextBox, ByVal e As System.Windows.Forms.KeyEventArgs)
        Call ProcessCutCopyPasteSelect(SentTextBox, e)
        If e.KeyCode = Keys.Enter Then
            Call UpdateMoonPrices()
        End If
    End Sub

    Private Sub txtMoon1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon1.GotFocus
        Call MoonTextBoxes_GotFocus(1, e)
    End Sub

    Private Sub txtMoon1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon1.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon1, e)
    End Sub

    Private Sub txtMoon1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon1.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon1.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon2.GotFocus
        Call MoonTextBoxes_GotFocus(2, e)
    End Sub

    Private Sub txtMoon2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon2.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon2, e)
    End Sub

    Private Sub txtMoon2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon2.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon2.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon3.GotFocus
        Call MoonTextBoxes_GotFocus(3, e)
    End Sub

    Private Sub txtMoon3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon3.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon3, e)
    End Sub

    Private Sub txtMoon3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon3.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon3.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon4.GotFocus
        Call MoonTextBoxes_GotFocus(4, e)
    End Sub

    Private Sub txtMoon4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon4.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon4, e)
    End Sub

    Private Sub txtMoon4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon4.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon4.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon5.GotFocus
        Call MoonTextBoxes_GotFocus(5, e)
    End Sub

    Private Sub txtMoon5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon5.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon5, e)
    End Sub

    Private Sub txtMoon5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon5.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon5.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon6_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon6.GotFocus
        Call MoonTextBoxes_GotFocus(6, e)
    End Sub

    Private Sub txtMoon6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon6.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon6, e)
    End Sub

    Private Sub txtMoon6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon6.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon6.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon7.GotFocus
        Call MoonTextBoxes_GotFocus(7, e)
    End Sub

    Private Sub txtMoon7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon7.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon7, e)
    End Sub

    Private Sub txtMoon7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon7.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon7.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon8_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon8.GotFocus
        Call MoonTextBoxes_GotFocus(8, e)
    End Sub

    Private Sub txtMoon8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon8.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon8, e)
    End Sub

    Private Sub txtMoon8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon8.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon8.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon9_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon9.GotFocus
        Call MoonTextBoxes_GotFocus(9, e)
    End Sub

    Private Sub txtMoon9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon9.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon9, e)
    End Sub

    Private Sub txtMoon9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon9.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon9.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon10_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon10.GotFocus
        Call MoonTextBoxes_GotFocus(10, e)
    End Sub

    Private Sub txtMoon10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon10.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon10, e)
    End Sub

    Private Sub txtMoon10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon10.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon10.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub txtMoon11_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon11.GotFocus
        Call MoonTextBoxes_GotFocus(11, e)
    End Sub

    Private Sub txtMoon11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoon11.KeyDown
        Call MoonTextBoxes_KeyDown(txtMoon11, e)
    End Sub

    Private Sub txtMoon11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMoon11.KeyPress
        Call MoonTextBoxes_KeyPress(e)
    End Sub

    Private Sub txtMoon11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoon11.TextChanged
        MoonPricesUpdated = True
    End Sub

    Private Sub btnCloseMoonMats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseMoonMats.Click
        Me.Hide()
    End Sub

#End Region

    ' Functions and procedures to update via manual search
#Region "Search Update Tab"

    ' Function fills the list with items as defined in the search box
    Public Sub FillSearchGrid(ByVal ItemText As String)
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader
        Dim matList As ListViewItem

        ' Clear old data
        lstPriceLookup.Items.Clear()
        lstPriceLookup.Update()

        SQL = "SELECT ITEM_NAME, PRICE FROM ITEM_PRICES WHERE ITEM_NAME LIKE '%" & FormatDBString(ItemText) & "%'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader()

        While readerLookup.Read
            ' Add the items to the list
            matList = lstPriceLookup.Items.Add(readerLookup.GetString(0))
            'The remaining columns are subitems  
            matList.SubItems.Add(FormatNumber(readerLookup.GetDouble(1), 2))

        End While

        readerLookup.Close()
        readerLookup = Nothing
        DBCommand = Nothing

    End Sub

    ' Updates price on the form
    Private Sub UpdateItemPrices()
        Dim SQL As String

        ' Check the price first
        If Not IsNumeric(txtItemPriceUpdate.Text) Then
            MsgBox("Invalid Price", vbExclamation, Me.Text)
            txtItemPriceUpdate.Focus()
            Exit Sub
        End If

        ' Make sure they selected an item
        If Trim(lblSelectedItem.Text) = "" Then
            MsgBox("You must select an Item", vbExclamation, Me.Text)
            txtItemSearch.Focus()
            Exit Sub
        End If

        ' Update the price
        SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtItemPriceUpdate.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & GetTypeID(lblSelectedItem.Text)
        Call evedb.ExecuteNonQuerySQL(SQL)

        ' Finally update the Program prices
        Call UpdateProgramPrices()

        ' Done
        MsgBox("Price updated", vbInformation, Me.Text)

        ' Clear the price
        txtItemPriceUpdate.Text = ""
        lblSelectedItem.Text = ""

        ' Select the search box
        Call txtItemSearch.SelectAll()
        txtItemSearch.Focus()

    End Sub

    Private Sub btnItemSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemSearch.Click
        Call FillSearchGrid(txtItemSearch.Text)
    End Sub

    Private Sub btnSearchClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchClose.Click
        Me.Hide()
    End Sub

    ' Updates the price based on the manual search for the item
    Private Sub btnSearchUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchUpdate.Click
        Call UpdateItemPrices()
    End Sub

    Private Sub txtItemSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItemSearch.KeyDown
        Call ProcessCutCopyPasteSelect(txtItemSearch, e)
        If e.KeyCode = Keys.Enter Then
            Call FillSearchGrid(txtItemSearch.Text)
        End If
    End Sub

    Private Sub lstPriceLookup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPriceLookup.SelectedIndexChanged
        ' When you unselect a row, the indexes are cleared and will error if referenced, so check
        If lstPriceLookup.SelectedItems.Count > 0 Then
            lblSelectedItem.Text = lstPriceLookup.SelectedItems(0).SubItems(0).Text
        End If
    End Sub

    Private Sub tabPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabPrices.Click
        If tabPrices.SelectedTab.Text = "Minerals" Then
            Call LoadMineralPrices()
        ElseIf tabPrices.SelectedTab.Text = "Advanced Composites" Then
            Call LoadMoonPrices()
        ElseIf tabPrices.SelectedTab.Text = "Item Search" Then
            txtItemSearch.Focus()
        End If
    End Sub

    Private Sub lstPriceLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPriceLookup.Click
        txtItemPriceUpdate.Focus()
    End Sub

    Private Sub txtItemPriceUpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItemPriceUpdate.KeyDown
        Call ProcessCutCopyPasteSelect(txtItemPriceUpdate, e)
        If e.KeyCode = Keys.Enter Then
            Call UpdateItemPrices()
        End If
    End Sub

    Private Sub txtItemPriceUpdate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemPriceUpdate.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

#End Region

End Class