
Imports System.Data.SQLite

Public Class frmLPStore

    Private SelectedCorporationList As List(Of String)
    Private FirstShow As Boolean

    Private StoreColumnClicked As Integer
    Private StoreColumnSortOrder As SortOrder

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FirstShow = True
        UserLPStoreSettings = AllSettings.LoadLPStoreSettings

        SelectedCorporationList = New List(Of String)

        lstCorporations.Columns.Add("", 25, HorizontalAlignment.Right) ' Check
        lstCorporations.Columns.Add("", 0, HorizontalAlignment.Center) ' Hidden corp ID
        lstCorporations.Columns.Add("Corporation Name", 200, HorizontalAlignment.Left)

        ' 256 width 235 with spacer
        lstRequiredMats.Columns.Add("Required Material", 183, HorizontalAlignment.Left)
        lstRequiredMats.Columns.Add("Quantity", 52, HorizontalAlignment.Right)

        ' Width is 1061 - 21 for scroll so total of 1040
        lstStoreItems.Columns.Add("Offer ID", 0, HorizontalAlignment.Center) ' Hidden order id for requirements look up
        lstStoreItems.Columns.Add("Corporation Name", 150, HorizontalAlignment.Left)
        lstStoreItems.Columns.Add("Standings", 60, HorizontalAlignment.Right)
        lstStoreItems.Columns.Add("Item Name", 270, HorizontalAlignment.Left)
        lstStoreItems.Columns.Add("Item Category", 77, HorizontalAlignment.Left)
        lstStoreItems.Columns.Add("Item Group", 133, HorizontalAlignment.Left)
        lstStoreItems.Columns.Add("Quantity", 51, HorizontalAlignment.Right)
        lstStoreItems.Columns.Add("LP Cost", 60, HorizontalAlignment.Right)
        lstStoreItems.Columns.Add("ISK Cost", 72, HorizontalAlignment.Right)
        lstStoreItems.Columns.Add("ISK/LP", 66, HorizontalAlignment.Right)
        lstStoreItems.Columns.Add("Profit", 100, HorizontalAlignment.Right)

        StoreColumnClicked = 0
        StoreColumnSortOrder = SortOrder.Ascending

    End Sub

    Private Sub frmLPStore_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim icons As Icon = SystemIcons.WinLogo

        ' Load settings first
        With UserLPStoreSettings
            Select Case .RewardType
                Case rbtnAll.Text
                    rbtnAll.Checked = True
                Case rbtnAmmoCharge.Text
                    rbtnAmmoCharge.Checked = True
                Case rbtnApparel.Text
                    rbtnApparel.Checked = True
                Case rbtnBlueprints.Text
                    rbtnBlueprints.Checked = True
                Case rbtnCommodities.Text
                    rbtnCommodities.Checked = True
                Case rbtnDeployable.Text
                    rbtnDeployable.Checked = True
                Case rbtnDrones.Text
                    rbtnDrones.Checked = True
                Case rbtnImplants.Text
                    rbtnImplants.Checked = True
                Case rbtnModules.Text
                    rbtnModules.Checked = True
                Case rbtnShips.Text
                    rbtnShips.Checked = True
                Case rbtnSkills.Text
                    rbtnSkills.Checked = True
                Case Else
                    rbtnAll.Checked = True
            End Select

            txtItemFilter.Text = .TextItemSearch

            txtLPGreaterThan.Text = .LPCostGreaterThan
            txtLPLessThan.Text = .LPCostLessThan
            txtISKGreaterThan.Text = .ISKCostGreaterThan
            txtISKLessThan.Text = .ISKCostLessThan
            txtStandingsLessThan.Text = .StandingLessThan
            txtStandingsGreaterThan.Text = .StandingGreaterThan

            chkHighlightCorps.Checked = .HighlightCheck

            Select Case .SearchOption
                Case rbtnCorpswStanding.Text
                    rbtnCorpswStanding.Checked = True
                Case Else
                    rbtnAllCorps.Checked = True
            End Select

            Select Case .SortByOption
                Case rbtnSortbyProfit.Text
                    rbtnSortbyProfit.Checked = True
                Case Else
                    rbtnSortbyISKperLP.Checked = True
            End Select

            chkLevel1Agent.Checked = .CheckAgentLevel1
            chkLevel2Agent.Checked = .CheckAgentLevel2
            chkLevel3Agent.Checked = .CheckAgentLevel3
            chkLevel4Agent.Checked = .CheckAgentLevel4
            chkLevel5Agent.Checked = .CheckAgentLevel5

            .HighlightCheck = chkHighlightCorps.Checked

            If .CorpFilter = rbtnCorpFilterUseStandings.Text Then
                rbtnCorpFilterUseAgentLevels.Checked = True
            Else
                rbtnCorpFilterUseStandings.Checked = True
            End If

            Dim CorpList As String()

            ' Separate by the ","
            CorpList = .SelectedCorporations.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)

            For i = 0 To CorpList.Count - 1
                If CorpList(i) <> "" Then
                    SelectedCorporationList.Add(CorpList(i))
                End If
            Next

        End With

        ' Load image List
        LPStoreItemImages.Images.Clear()

        Dim dir As New IO.DirectoryInfo("EVEIPH Images\LP Store")
        For Each file As IO.FileInfo In dir.GetFiles("*")
            LPStoreItemImages.Images.Add(icons)
        Next

        ' Load the corporation list
        Call LoadCorpList()

        FirstShow = False

    End Sub

    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click
        Dim TempSettings As LPStore = Nothing

        With TempSettings
            If rbtnAll.Checked Then
                .RewardType = rbtnAll.Text
            ElseIf rbtnAmmoCharge.Checked Then
                .RewardType = rbtnAmmoCharge.Text
            ElseIf rbtnApparel.Checked Then
                .RewardType = rbtnApparel.Text
            ElseIf rbtnBlueprints.Checked Then
                .RewardType = rbtnBlueprints.Text
            ElseIf rbtnCommodities.Checked Then
                .RewardType = rbtnCommodities.Text
            ElseIf rbtnDeployable.Checked Then
                .RewardType = rbtnDeployable.Text
            ElseIf rbtnDrones.Checked Then
                .RewardType = rbtnDrones.Text
            ElseIf rbtnImplants.Checked Then
                .RewardType = rbtnImplants.Text
            ElseIf rbtnModules.Checked Then
                .RewardType = rbtnModules.Text
            ElseIf rbtnShips.Checked Then
                .RewardType = rbtnShips.Text
            ElseIf rbtnSkills.Checked Then
                .RewardType = rbtnSkills.Text
            Else
                .RewardType = rbtnAll.Text
            End If

            .TextItemSearch = txtItemFilter.Text

            .LPCostGreaterThan = txtLPGreaterThan.Text
            .LPCostLessThan = txtLPLessThan.Text
            .ISKCostGreaterThan = txtISKGreaterThan.Text
            .ISKCostLessThan = txtISKLessThan.Text
            .StandingLessThan = txtStandingsLessThan.Text
            .StandingGreaterThan = txtStandingsGreaterThan.Text

            .CheckAgentLevel1 = chkLevel1Agent.Checked
            .CheckAgentLevel2 = chkLevel2Agent.Checked
            .CheckAgentLevel3 = chkLevel3Agent.Checked
            .CheckAgentLevel4 = chkLevel4Agent.Checked
            .CheckAgentLevel5 = chkLevel5Agent.Checked

            .HighlightCheck = chkHighlightCorps.Checked

            If rbtnCorpFilterUseAgentLevels.Checked Then
                .CorpFilter = rbtnCorpFilterUseAgentLevels.Text
            Else
                .CorpFilter = rbtnCorpFilterUseStandings.Text
            End If

            If rbtnCorpswStanding.Checked Then
                .SearchOption = rbtnCorpswStanding.Text
            Else
                .SearchOption = rbtnAllCorps.Text
            End If

            If rbtnSortbyProfit.Checked Then
                .SortByOption = rbtnSortbyProfit.Text
            Else
                .SortByOption = rbtnSortbyISKperLP.Text
            End If

            .SelectedCorporations = ""

            If lstCorporations.CheckedItems.Count <> 0 Then
                For i = 0 To lstCorporations.CheckedItems.Count - 1
                    .SelectedCorporations = .SelectedCorporations & lstCorporations.CheckedItems(i).SubItems(2).Text & ","
                Next
            Else
                .SelectedCorporations = ""
            End If

            ' Reset this now 
            UserLPStoreSettings.SelectedCorporations = .SelectedCorporations
            ' Now refresh corp list based on new selections
            Call LoadCorpList()

        End With

        AllSettings.SaveLPStoreSettings(TempSettings)

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub LoadLPStore()
        Dim SQL As String = ""
        Dim LPItemRow As ListViewItem
        Dim readerLP As SQLiteDataReader

        ' Error check the text boxes
        If Trim(txtISKGreaterThan.Text) = "" Or Not IsNumeric(txtISKGreaterThan.Text) Then
            MsgBox("You must enter a valid number for the greater than ISK filter", vbExclamation, Application.ProductName)
            txtISKGreaterThan.SelectAll()
            txtISKGreaterThan.Focus()
            Exit Sub
        End If

        If Trim(txtISKLessThan.Text) = "" Or Not IsNumeric(txtISKLessThan.Text) Then
            MsgBox("You must enter a valid number for the greater than ISK filter", vbExclamation, Application.ProductName)
            txtISKLessThan.SelectAll()
            txtISKLessThan.Focus()
            Exit Sub
        End If

        If Trim(txtLPGreaterThan.Text) = "" Or Not IsNumeric(txtLPGreaterThan.Text) Then
            MsgBox("You must enter a valid number for the greater than LP filter", vbExclamation, Application.ProductName)
            txtLPGreaterThan.SelectAll()
            txtLPGreaterThan.Focus()
            Exit Sub
        End If

        If Trim(txtLPLessThan.Text) = "" Or Not IsNumeric(txtLPLessThan.Text) Then
            MsgBox("You must enter a valid number for the greater than LP filter", vbExclamation, Application.ProductName)
            txtLPLessThan.SelectAll()
            txtLPLessThan.Focus()
            Exit Sub
        End If

        If Trim(txtStandingsGreaterThan.Text) = "" Or Not IsNumeric(txtStandingsGreaterThan.Text) Then
            MsgBox("You must enter a valid number for the greater than Standings filter", vbExclamation, Application.ProductName)
            txtStandingsGreaterThan.SelectAll()
            txtStandingsGreaterThan.Focus()
            Exit Sub
        End If

        If Trim(txtStandingsLessThan.Text) = "" Or Not IsNumeric(txtStandingsLessThan.Text) Then
            MsgBox("You must enter a valid number for the greater than Standings filter", vbExclamation, Application.ProductName)
            txtStandingsLessThan.SelectAll()
            txtStandingsLessThan.Focus()
            Exit Sub
        End If

        ' Refresh corp list
        Call LoadCorpList()

        SQL = "SELECT CORP_NAME, ITEM, categoryName, groupName, LP_STORE.ITEM_ID,  "
        SQL = SQL & "ITEM_QUANTITY, LP_COST, ISK_COST, CASE WHEN categoryName = 'Blueprint' "
        SQL = SQL & "THEN (SELECT PRICE FROM ITEM_PRICES, INDUSTRY_ACTIVITY_PRODUCTS "
        SQL = SQL & "WHERE ITEM_ID = productTypeID AND blueprintTypeID = LP_STORE.ITEM_ID)  "
        SQL = SQL & "ELSE (PRICE * ITEM_QUANTITY) END AS SELL_PRICE, "
        SQL = SQL & "CASE WHEN STANDING IS NULL THEN 0 ELSE STANDING END AS STANDING, OFFER_ID "
        SQL = SQL & "FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_CATEGORIES, ITEM_PRICES, LP_STORE "
        SQL = SQL & "LEFT JOIN CHARACTER_STANDINGS ON LP_STORE.CORP_ID = CHARACTER_STANDINGS.NPC_TYPE_ID "
        SQL = SQL & "WHERE LP_STORE.ITEM_ID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
        SQL = SQL & "AND INVENTORY_GROUPS.categoryID = INVENTORY_CATEGORIES.categoryID "
        SQL = SQL & "AND LP_STORE.ITEM_ID = ITEM_PRICES.ITEM_ID "

        ' Set the type of item in the store
        If rbtnApparel.Checked Then
            SQL = SQL & "AND categoryName = 'Apparel' "
        ElseIf rbtnBlueprints.Checked Then
            SQL = SQL & "AND categoryName = 'Blueprint' "
        ElseIf rbtnAmmoCharge.Checked Then
            SQL = SQL & "AND categoryName = 'Charge' "
        ElseIf rbtnCommodities.Checked Then
            SQL = SQL & "AND categoryName = 'Commodity' "
        ElseIf rbtnDeployable.Checked Then
            SQL = SQL & "AND categoryName = 'Deployable' "
        ElseIf rbtnDrones.Checked Then
            SQL = SQL & "AND categoryName = 'Drone' "
        ElseIf rbtnImplants.Checked Then
            SQL = SQL & "AND categoryName = 'Implant' "
        ElseIf rbtnModules.Checked Then
            SQL = SQL & "AND categoryName = 'Module' "
        ElseIf rbtnShips.Checked Then
            SQL = SQL & "AND categoryName = 'Ship' "
        ElseIf rbtnSkills.Checked Then
            SQL = SQL & "AND categoryName = 'Skill' "
        End If

        If Trim(txtItemFilter.Text) <> "" Then
            SQL = SQL & "AND ITEM LIKE '%" & FormatDBString(txtItemFilter.Text) & "%' "
        End If

        DBCommand = New SQLiteCommand(SQL, DB)
        readerLP = DBCommand.ExecuteReader

        Me.Cursor = Cursors.WaitCursor
        Call lstStoreItems.Items.Clear()
        ' Disable sorting because it will crawl after we update if there are too many records
        lstStoreItems.ListViewItemSorter = Nothing
        Call lstStoreItems.BeginUpdate()

        While readerLP.Read
            LPItemRow = New ListViewItem(CStr(readerLP.GetInt64(10)))
            LPItemRow.SubItems.Add(readerLP.GetString(0))
            LPItemRow.SubItems.Add(FormatNumber(readerLP.GetDouble(9), 2))
            LPItemRow.SubItems.Add(readerLP.GetString(1))
            LPItemRow.SubItems.Add(readerLP.GetString(2))
            LPItemRow.SubItems.Add(readerLP.GetString(3))
            LPItemRow.SubItems.Add(CStr(readerLP.GetInt32(5)))
            LPItemRow.SubItems.Add(FormatNumber(readerLP.GetInt32(6), 0))
            LPItemRow.SubItems.Add(FormatNumber(readerLP.GetInt32(7), 0))
            LPItemRow.SubItems.Add("0") ' ISK/LP
            LPItemRow.SubItems.Add("0") ' Profit
            Call lstStoreItems.Items.Add(LPItemRow)
            Application.DoEvents()
        End While

        Call lstStoreItems.EndUpdate()

        Dim SortColumn As Integer
        If rbtnSortbyISKperLP.Checked Then
            SortColumn = 9
        ElseIf rbtnSortbyProfit.Checked Then
            SortColumn = 10
        End If

        Call ListViewColumnSorter(SortColumn, lstStoreItems, StoreColumnClicked, SortOrder.Descending)

        Me.Cursor = Cursors.Default
        Application.DoEvents()

    End Sub

    Private Sub LoadCorpList()
        Dim readerCorp As SQLiteDataReader
        Dim corpListViewRow As ListViewItem
        Dim SQL As String

        ' Load up all the corporations 
        SQL = "SELECT DISTINCT CORP_ID, CORP_NAME FROM LP_STORE "

        DBCommand = New SQLiteCommand(SQL & "ORDER BY CORP_NAME ", DB)
        readerCorp = DBCommand.ExecuteReader

        lstCorporations.Items.Clear()
        lstCorporations.BeginUpdate()

        While readerCorp.Read
            corpListViewRow = New ListViewItem("")
            corpListViewRow.SubItems.Add(CStr(readerCorp.GetInt64(0)))
            corpListViewRow.SubItems.Add(readerCorp.GetString(1))
            If UserLPStoreSettings.SelectedCorporations.Contains(readerCorp.GetString(1)) Or UserLPStoreSettings.SelectedCorporations = "" Then
                corpListViewRow.Checked = True
            Else
                corpListViewRow.Checked = False
            End If
            Call lstCorporations.Items.Add(corpListViewRow)
        End While

        lstCorporations.EndUpdate()
        Application.DoEvents()

    End Sub

    ' When selected, load the required materials to buy this item
    Private Sub lstStoreItems_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstStoreItems.SelectedIndexChanged
        Dim SQL As String
        Dim readerReq As SQLiteDataReader
        Dim lstReqRow As ListViewItem

        ' Load up the inputs into the boxes depending on number of mats
        ' Reactions is form global and since it is sorted, it represents what is in lstStoreItems
        If lstStoreItems.SelectedItems.Count > 0 Then
            SQL = "SELECT typeName, REQ_QUANTITY FROM LP_OFFER_REQUIREMENTS, INVENTORY_TYPES "
            SQL = SQL & "WHERE REQ_TYPE_ID = typeID AND OFFER_ID = " & lstStoreItems.SelectedItems(0).SubItems(0).Text & " ORDER BY typeName"

            DBCommand = New SQLiteCommand(SQL, DB)
            readerReq = DBCommand.ExecuteReader

            lstRequiredMats.Items.Clear()
            lstRequiredMats.BeginUpdate()
            While readerReq.Read
                lstReqRow = New ListViewItem(readerReq.GetString(0))
                lstReqRow.SubItems.Add(FormatNumber(readerReq.GetInt32(1), 0))
                Call lstRequiredMats.Items.Add(lstReqRow)
            End While
            lstRequiredMats.EndUpdate()
        End If

    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Call LoadLPStore()
    End Sub

    Private Sub rbtnAllCorps_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnAllCorps.CheckedChanged
        If rbtnCorpswStanding.Checked Then
            chkHighlightCorps.Enabled = False
        Else
            chkHighlightCorps.Enabled = True
        End If
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub rbtnCorpswStanding_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCorpswStanding.CheckedChanged
        If rbtnCorpswStanding.Checked Then
            chkHighlightCorps.Enabled = False
        Else
            chkHighlightCorps.Enabled = True
        End If
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub chkLevel1Agent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLevel1Agent.CheckedChanged
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub chkLevel2Agent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLevel2Agent.CheckedChanged
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub chkLevel3Agent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLevel3Agent.CheckedChanged
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub chkLevel4Agent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLevel4Agent.CheckedChanged
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub chkLevel5Agent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLevel5Agent.CheckedChanged
        If Not FirstShow Then
            Call LoadCorpList()
        End If
    End Sub

    Private Sub lstStoreItems_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstStoreItems.ColumnClick
        Call ListViewColumnSorter(e.Column, lstStoreItems, StoreColumnClicked, StoreColumnSortOrder)
    End Sub

    Private Sub rbtnCorpFilterUseStandings_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCorpFilterUseStandings.CheckedChanged
        gbAgentLevels.Enabled = False
        gbStandings.Enabled = True
    End Sub

    Private Sub rbtnCorpFilterUseAgentLevels_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCorpFilterUseAgentLevels.CheckedChanged
        gbAgentLevels.Enabled = True
        gbStandings.Enabled = False
    End Sub

    Private Sub txtItemFilter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItemFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call LoadLPStore()
        End If
    End Sub

End Class