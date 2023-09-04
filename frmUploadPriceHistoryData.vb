Imports System.Data.SQLite

Public Class frmUploadPriceHistoryData
    Private KeyHandled As Boolean
    Private ItemComboLoaded As Boolean
    Private GroupComboLoaded As Boolean
    Private RegionComboLoaded As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ItemComboLoaded = False
        GroupComboLoaded = False
        RegionComboLoaded = False

        cmbItemGroup.Items.Add("All")
        cmbItemGroup.Text = "All"

        cmbRegions.Items.Add("The Forge")
        cmbRegions.Text = "The Forge"

        cmbItems.Items.Add("Select Item")
        cmbItems.Text = "Select Item"

        btnImport.Enabled = False

    End Sub

    Private Sub txtPaste_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPaste.KeyDown
        If e.KeyCode = Keys.A AndAlso e.Control = True Then ' Select All
            txtPaste.SelectAll()
            KeyHandled = True
        Else
            KeyHandled = False
        End If

    End Sub

    Private Sub txtPaste_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPaste.KeyPress
        ' Special handling - if select all is pressed for some reason the notification sound happens
        e.Handled = KeyHandled
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        ' Insert the data (should be tab separated) into the table
        ' Get the item ID for the table
        If Trim(txtPaste.Text) = "" Then
            MsgBox("No data to import", vbExclamation, "No Data")
            Exit Sub
        End If

        Dim ItemID As String = ""
        Dim RegionID As String = ""

        Dim rsItems As SQLiteDataReader
        DBCommand = New SQLiteCommand("SELECT ITEM_ID FROM ALL_BLUEPRINTS WHERE ITEM_NAME = '" & FormatDBString(cmbItems.Text) & "'", EVEDB.DBREf)
        rsItems = DBCommand.ExecuteReader

        If rsItems.Read Then
            ItemID = rsItems.GetInt64(0).ToString
        Else
            MsgBox("Invalid Item Name", vbExclamation, "Wrong Item Name")
            rsItems.Close()
            Exit Sub
        End If

        ' Get RegionID
        DBCommand = New SQLiteCommand("SELECT regionID FROM REGIONS WHERE regionName = '" & cmbRegions.Text & "'", EVEDB.DBREf)
        rsItems = DBCommand.ExecuteReader

        If rsItems.Read Then
            RegionID = rsItems.GetInt64(0).ToString
        Else
            MsgBox("Invalid Item Name", vbExclamation, "Wrong Item Name")
            rsItems.Close()
            Exit Sub
        End If

        Try
            EVEDB.BeginSQLiteTransaction()
            ' Delete anything there for item selected
            EVEDB.ExecuteNonQuerySQL(String.Format("DELETE FROM MARKET_HISTORY WHERE TYPE_ID = {0} AND REGION_ID = {1}", ItemID, RegionID))

            ' Insert from the pasted data
            Dim Dataset As String = txtPaste.Text.Replace(vbCrLf, vbTab)
            Dataset = Dataset.Replace(vbLf, vbTab)
            Dim ItemData() As String = Dataset.Split(ControlChars.Tab)

            Dim PriceDate As String = ""

            For i = 0 To ItemData.Count - 1
                If i Mod 6 = 0 And i <> 0 Then
                    ' Every 6th item, insert
                    PriceDate = ItemData(i - 6).Replace(".", "-")
                    Dim insertsql As String = String.Format("INSERT INTO MARKET_HISTORY VALUES({0},{1},'{2}',{3},{4},{5},{6},{7})",
                                                           ItemID, RegionID,
                                                           Format(DateValue(PriceDate.Substring(0, 10)), SQLiteDateFormat),
                                                           ConvertPriceHistoryEUDecimal(ItemData(i - 3).Replace(" ISK", "")),
                                                           ConvertPriceHistoryEUDecimal(ItemData(i - 2).Replace(" ISK", "")),
                                                           ConvertPriceHistoryEUDecimal(ItemData(i - 1).Replace(" ISK", "")),
                                                           ConvertPriceHistoryEUDecimal(ItemData(i - 5)),
                                                           ConvertPriceHistoryEUDecimal(ItemData(i - 4)))
                    EVEDB.ExecuteNonQuerySQL(insertsql)
                End If
            Next

            EVEDB.CommitSQLiteTransaction()
            MsgBox("Data Imported!", vbInformation, "Success")
        Catch ex As Exception
            MsgBox("Data Import Failed", vbExclamation, "Error")
            EVEDB.RollbackSQLiteTransaction()
        End Try

        rsItems.Close()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtPaste_TextChanged(sender As Object, e As EventArgs) Handles txtPaste.TextChanged
        btnImport.Enabled = True
    End Sub

    Private Sub cmbItems_DropDown(sender As Object, e As EventArgs) Handles cmbItems.DropDown
        If Not ItemComboLoaded Then
            Dim rsItems As SQLiteDataReader
            Dim SQL As String = ""
            cmbItems.Items.Clear()
            cmbItems.Items.Add("Select Item")
            SQL = "SELECT ITEM_NAME FROM ALL_BLUEPRINTS"

            If cmbItemGroup.Text <> "All" Then
                SQL &= " WHERE ITEM_GROUP = '" & cmbItemGroup.Text & "'"
            End If

            DBCommand = New SQLiteCommand(SQL & " ORDER BY ITEM_NAME", EVEDB.DBREf)
            rsItems = DBCommand.ExecuteReader

            While rsItems.Read()
                cmbItems.Items.Add(rsItems.GetString(0))
            End While
            ItemComboLoaded = True
            rsItems.Close()
        End If
    End Sub
    Private Sub cmbItemGroup_DropDown(sender As Object, e As EventArgs) Handles cmbItemGroup.DropDown
        If Not GroupComboLoaded Then
            Dim rsItems As SQLiteDataReader
            cmbItemGroup.Items.Clear()

            DBCommand = New SQLiteCommand("SELECT ITEM_GROUP FROM ALL_BLUEPRINTS GROUP BY ITEM_GROUP", EVEDB.DBREf)
            rsItems = DBCommand.ExecuteReader
            cmbItemGroup.Items.Add("All")
            While rsItems.Read()
                cmbItemGroup.Items.Add(rsItems.GetString(0))
            End While
            GroupComboLoaded = True
            rsItems.Close()
        End If
    End Sub

    Private Sub cmbItemGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItemGroup.SelectedIndexChanged
        cmbItems.Text = "Select Item"
        btnImport.Enabled = False
        ItemComboLoaded = False
    End Sub

    Private Sub cmbRegions_DropDown(sender As Object, e As EventArgs) Handles cmbRegions.DropDown
        If Not RegionComboLoaded Then
            Call LoadRegionCombo(cmbRegions, "Jita")
            RegionComboLoaded = True
        End If
    End Sub

    Private Sub cmbItemGroup_DropDownClosed(sender As Object, e As EventArgs) Handles cmbItemGroup.DropDownClosed
        If Trim(cmbItemGroup.Text) = "" Then
            cmbItemGroup.Text = "All"
        End If
    End Sub

    Private Sub cmbItemGroup_LostFocus(sender As Object, e As EventArgs) Handles cmbItemGroup.LostFocus
        If Trim(cmbItemGroup.Text) = "" Then
            cmbItemGroup.Text = "All"
        End If
    End Sub

    Private Sub cmbRegions_LostFocus(sender As Object, e As EventArgs) Handles cmbRegions.LostFocus
        If Trim(cmbRegions.Text) = "" Then
            cmbRegions.Text = "The Forge"
        End If
    End Sub

    Private Sub cmbRegions_DropDownClosed(sender As Object, e As EventArgs) Handles cmbRegions.DropDownClosed
        If Trim(cmbRegions.Text) = "" Then
            cmbRegions.Text = "The Forge"
        End If
    End Sub

    Private Sub cmbItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItems.SelectedIndexChanged
        btnImport.Enabled = True
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtPaste.Text = ""
    End Sub
End Class