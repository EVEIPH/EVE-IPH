
Imports System.Data.SQLite

Public Class frmLPStore

    Private SelectedCorporationList As List(Of String)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        UserLPStoreSettings = AllSettings.LoadLPStoreSettings

        SelectedCorporationList = New List(Of String)

        lstCorporations.Columns.Add("", 25, HorizontalAlignment.Right) ' Check
        lstCorporations.Columns.Add("Corporation Name", 200, HorizontalAlignment.Left)

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
            End Select

            chkRaceAmarr.Checked = .CheckRaceAmarr
            chkRaceCaldari.Checked = .CheckRaceCaldari
            chkRaceGallente.Checked = .CheckRaceGallente
            chkRaceMinmatar.Checked = .CheckRaceMinmatar
            chkRacePirate.Checked = .CheckRacePirate

            txtItemFilter.Text = .TextItemSearch
            txtReqItemFilter.Text = .TextReqItemSearch

            txtLPGreaterThan.Text = .LPCostGreaterThan
            txtLPLessThan.Text = .LPCostLessThan
            txtISKGreaterThan.Text = .ISKCostGreaterThan
            txtISKLessThan.Text = .ISKCostLessThan

            chkHighlightCorps.Checked = .HighlightCheck

            Select Case .SearchOption
                Case rbtnAllCorps.Text
                    rbtnAllCorps.Checked = True
                Case rbtnCorpswStanding.Text
                    rbtnCorpswStanding.Checked = True
            End Select

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
            End If

            .CheckRaceAmarr = chkRaceAmarr.Checked
            .CheckRaceCaldari = chkRaceCaldari.Checked
            .CheckRaceGallente = chkRaceGallente.Checked
            .CheckRaceMinmatar = chkRaceMinmatar.Checked
            .CheckRacePirate = chkRacePirate.Checked

            .TextItemSearch = txtItemFilter.Text
            .TextReqItemSearch = txtReqItemFilter.Text

            .LPCostGreaterThan = txtLPGreaterThan.Text
            .LPCostLessThan = txtLPLessThan.Text
            .ISKCostGreaterThan = txtISKGreaterThan.Text
            .ISKCostLessThan = txtISKLessThan.Text

            .HighlightCheck = chkHighlightCorps.Checked

            If rbtnAllCorps.Checked Then
                .SearchOption = rbtnAllCorps.Text
            ElseIf rbtnCorpswStanding.Checked Then
                .SearchOption = rbtnCorpswStanding.Text
            End If

            .SelectedCorporations = ""

            For i = 0 To lstCorporations.CheckedItems.Count - 1
                .SelectedCorporations = .SelectedCorporations & lstCorporations.CheckedItems(i).SubItems(1).Text & ","
            Next

        End With

        AllSettings.SaveLPStoreSettings(TempSettings)

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Dim SQL As String = ""




        ' Refresh corp list
        Call LoadCorpList()

    End Sub

    Private Sub LoadCorpList()
        Dim TempRace As String = ""
        Dim readerCorp As SQLiteDataReader
        Dim corpListViewRow As ListViewItem
        Dim SQL As String

        If chkRaceAmarr.Checked = False And chkRaceCaldari.Checked = False And chkRaceGallente.Checked = False And chkRaceMinmatar.Checked = False And chkRacePirate.Checked = False Then
            lstCorporations.Items.Clear()
            Application.DoEvents()
            Exit Sub
        End If

        ' Determine what race we are looking at
        If chkRaceAmarr.Checked Then
            TempRace = TempRace & "4,"
        End If
        If chkRaceCaldari.Checked Then
            TempRace = TempRace & "1,"
        End If
        If chkRaceMinmatar.Checked Then
            TempRace = TempRace & "2,"
        End If
        If chkRaceGallente.Checked Then
            TempRace = TempRace & "8,"
        End If
        If chkRacePirate.Checked Then
            TempRace = TempRace & "9,15,"
        End If

        If TempRace <> "" Then
            TempRace = "(" & TempRace.Substring(0, Len(TempRace) - 1) & ")"
            TempRace = "WHERE RACE_ID IN " & TempRace
        End If

        ' Load up all the corporations 
        SQL = "SELECT DISTINCT CORP_NAME FROM LP_STORE "

        DBCommand = New SQLiteCommand(Sql & TempRace & "ORDER BY CORP_NAME ", DB)
        readerCorp = DBCommand.ExecuteReader

        lstCorporations.Items.Clear()
        lstCorporations.BeginUpdate()

        While readerCorp.Read
            corpListViewRow = New ListViewItem("")
            'The remaining columns are subitems  
            corpListViewRow.SubItems.Add(readerCorp.GetString(0))
            Call lstCorporations.Items.Add(corpListViewRow)
        End While

        lstCorporations.EndUpdate()
        Application.DoEvents()

    End Sub

End Class