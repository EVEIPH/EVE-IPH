
Imports System.Data.SQLite

Public Class frmInventionMonitor

    Private SelectedT1BPID As Long
    Private SelectedDecryptor As New Decryptor
    Private SelectedBPID As Long
    Private SettingComboSkills As Boolean
    Private FirstFormLoad As Boolean

    Public Sub New()

        FirstFormLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lstInventionItems.Columns.Add("BPTypeID", 0, HorizontalAlignment.Left) ' Hidden
        lstInventionItems.Columns.Add("Invented Blueprint", 216, HorizontalAlignment.Left)
        lstInventionItems.Columns.Add("ME", 30, HorizontalAlignment.Center)
        lstInventionItems.Columns.Add("TE", 30, HorizontalAlignment.Center)
        lstInventionItems.Columns.Add("System", 100, HorizontalAlignment.Left)
        lstInventionItems.Columns.Add("Location", 150, HorizontalAlignment.Left) ' Station or POS
        lstInventionItems.Columns.Add("Failures", 0, HorizontalAlignment.Right) ' Hidden
        lstInventionItems.Columns.Add("Successes", 0, HorizontalAlignment.Right) ' Hidden
        lstInventionItems.Columns.Add("Total Attempts", 0, HorizontalAlignment.Right) ' Hidden

        pictInvention.Image = Nothing
        pictInvention.BackgroundImage = Nothing
        pictInvention.Update()

        SelectedT1BPID = 0
        SelectedDecryptor = NoDecryptor

        SettingComboSkills = False

        FirstFormLoad = False

    End Sub

    Private Sub frmInventionTracker_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim fAccessError As New frmAPIError

        Call ClearForm()

        ' See if they can load the jobs at all
        If Not SelectedCharacter.JobsAccess Then
            fAccessError.ErrorText = "This API did not allow industry jobs to be loaded for this character." & _
                Environment.NewLine & Environment.NewLine & "Please ensure your Customizable API includes 'IndustryJobs' under the 'Science & Industry' section to include industry jobs and then reload the API."
            fAccessError.Text = "API: No Industry Jobs Loaded"

            fAccessError.ErrorLink = "http://support.eveonline.com/api/Key/CreatePredefined/589962/"
            fAccessError.ShowDialog()

            gbInventionMonitor.Enabled = False
            lstInventionItems.Enabled = False
        Else
            gbInventionMonitor.Enabled = True
            lstInventionItems.Enabled = True
            Call ResetForm()
        End If

    End Sub

    Private Sub ClearForm()

        lblSuccessChance.Text = ""
        lblSuccessRate.Text = ""
        lblTotalAttempts.Text = ""
        lblTotalSuccesses.Text = ""
        lblSelectedBP.Text = ""
        lstInventionItems.Items.Clear()
        lstInventionItems.Update()
        pictInvention.Image = Nothing

        lblSkill1.Text = ""
        lblSkill2.Text = ""
        lblSkill3.Text = ""

        SettingComboSkills = True
        cmbSkill1.Text = "0"
        cmbSkill2.Text = "0"
        cmbSkill3.Text = "0"
        cmbSkill1.Enabled = False
        cmbSkill2.Enabled = False
        cmbSkill3.Enabled = False
        SettingComboSkills = False

        cmbBPDecryptor.Text = None
        cmbBPDecryptor.Enabled = False

    End Sub

    Private Sub ResetForm()
        Dim rsInvention As SQLiteDataReader
        Dim SQL As String

        Call ClearForm()

        ' They'll at least have personal jobs if they get this far
        rbtnPersonalJobs.Checked = True

        ' If they have a corp key, enable the other job type options (add tool tip to tell them to add a corp key of we need this)
        If SelectedCharacter.CharacterCorporation.JobsAccess Then
            rbtnBothJobs.Enabled = True
            rbtnCorpJobs.Enabled = True
        Else
            rbtnBothJobs.Enabled = False
            rbtnCorpJobs.Enabled = False
        End If

        ' Load up the earliest date they have in the system and today
        SQL = "SELECT MIN(DATETIME(endProductionTime)) FROM INDUSTRY_JOBS WHERE installerID = " & SelectedCharacter.ID & " "

        ' If not both, then select a key value
        If rbtnCorpJobs.Checked = True Then
            SQL = SQL & "AND JobType = " & CStr(ScanType.Corporation) & " "
        ElseIf rbtnPersonalJobs.Checked = True Then
            SQL = SQL & "AND JobType = " & CStr(ScanType.Personal) & " "
        End If

        DBCommand = New SQLiteCommand(SQL, DB)
        rsInvention = DBCommand.ExecuteReader

        If rsInvention.Read Then
            ' Then the record needs to be updated, so insert it to the list
            If Not IsDBNull(rsInvention.GetValue(0)) Then
                dtpInventionStartDate.Value = DateTime.ParseExact(rsInvention.GetString(0), SQLiteDateFormat, LocalCulture)
                dtpInventionEndDate.Value = Now
            Else
                dtpInventionEndDate.Value = Now
                dtpInventionStartDate.Value = Now
            End If
        Else
            dtpInventionEndDate.Value = Now
            dtpInventionStartDate.Value = Now
        End If

    End Sub

    Private Sub RefreshGrid()
        Dim rsInvention As SQLiteDataReader
        Dim SQL As String
        Dim lstJobRow As ListViewItem

        SQL = "SELECT CASE WHEN CONTAINER_NAME.typeName IS NOT NULL THEN CONTAINER_NAME.typeName ELSE 'Unknown' END AS Location,"
        SQL = SQL & "INDUSTRY_JOBS.outputTypeID as BPTypeID,"
        SQL = SQL & "INVENTORY_TYPES.typeName AS BP,"
        SQL = SQL & "INDUSTRY_JOBS.licensedProductionRuns,"
        SQL = SQL & "INDUSTRY_JOBS.materialMultiplier,"
        SQL = SQL & "INDUSTRY_JOBS.timeMultiplier,"
        SQL = SQL & "CASE WHEN FAIL.TOTAL IS NOT NULL THEN FAIL.TOTAL ELSE 0 END AS Failures,"
        SQL = SQL & "CASE WHEN SUCCESS.TOTAL IS NOT NULL THEN SUCCESS.TOTAL ELSE 0 END AS Successes,"
        SQL = SQL & "CASE WHEN TOTAL_JOBS.TOTAL IS NOT NULL THEN TOTAL_JOBS.TOTAL ELSE 0 END AS TotalJobs,"
        SQL = SQL & "installedItemLicensedProductionRunsRemaining,"
        SQL = SQL & "SOLAR_SYSTEMS.solarSystemName "
        SQL = SQL & "FROM INDUSTRY_JOBS "

        SQL = SQL & "LEFT OUTER JOIN (SELECT COUNT(*) AS TOTAL, outputTypeID, licensedProductionRuns, materialMultiplier, timeMultiplier "
        SQL = SQL & "FROM INDUSTRY_JOBS WHERE activityID = 8 AND completed = 1 AND completedStatus = 1 "
        SQL = SQL & "AND InstallerID =" & SelectedCharacter.ID & " "
        SQL = SQL & "AND (DATETIME(endProductionTime) >= DateTime('" & Format(dtpInventionStartDate.Value, SQLiteDateFormat) & "') "
        SQL = SQL & "AND DATETIME(endProductionTime) <= DateTime('" & Format(dtpInventionEndDate.Value, SQLiteDateFormat) & "')) "
        SQL = SQL & "GROUP BY outputTypeID, licensedProductionRuns, materialMultiplier, timeMultiplier) AS SUCCESS "
        SQL = SQL & "ON INDUSTRY_JOBS.outputTypeID = SUCCESS.outputTypeID "
        SQL = SQL & "AND INDUSTRY_JOBS.licensedProductionRuns = SUCCESS.licensedProductionRuns "
        SQL = SQL & "AND INDUSTRY_JOBS.materialMultiplier = SUCCESS.materialMultiplier "
        SQL = SQL & "AND INDUSTRY_JOBS.timeMultiplier = SUCCESS.timeMultiplier "

        SQL = SQL & "LEFT OUTER JOIN (SELECT COUNT(*) AS TOTAL, outputTypeID, licensedProductionRuns, materialMultiplier, timeMultiplier "
        SQL = SQL & "FROM INDUSTRY_JOBS WHERE activityID = 8 AND completed = 1 AND completedStatus = 0 "
        SQL = SQL & "AND InstallerID =" & SelectedCharacter.ID & " AND (DATETIME(endProductionTime) >= DateTime('" & Format(dtpInventionStartDate.Value, SQLiteDateFormat) & "') "
        SQL = SQL & "AND DATETIME(endProductionTime) <= DateTime('" & Format(dtpInventionEndDate.Value, SQLiteDateFormat) & "')) "
        SQL = SQL & "GROUP BY outputTypeID, licensedProductionRuns, materialMultiplier, timeMultiplier) AS FAIL "
        SQL = SQL & "ON INDUSTRY_JOBS.outputTypeID = FAIL.outputTypeID "
        SQL = SQL & "AND INDUSTRY_JOBS.licensedProductionRuns = FAIL.licensedProductionRuns "
        SQL = SQL & "AND INDUSTRY_JOBS.materialMultiplier = FAIL.materialMultiplier "
        SQL = SQL & "AND INDUSTRY_JOBS.timeMultiplier = FAIL.timeMultiplier "

        SQL = SQL & "LEFT OUTER JOIN (SELECT COUNT(*) AS TOTAL, outputTypeID, licensedProductionRuns, materialMultiplier, timeMultiplier "
        SQL = SQL & "FROM INDUSTRY_JOBS WHERE activityID = 8 AND completed = 1 "
        SQL = SQL & "AND InstallerID =" & SelectedCharacter.ID & " AND (DATETIME(endProductionTime) >= DateTime('" & Format(dtpInventionStartDate.Value, SQLiteDateFormat) & "') "
        SQL = SQL & "AND DATETIME(endProductionTime) <= DateTime('" & Format(dtpInventionEndDate.Value, SQLiteDateFormat) & "')) "
        SQL = SQL & "GROUP BY outputTypeID, licensedProductionRuns, materialMultiplier, timeMultiplier) AS TOTAL_JOBS "
        SQL = SQL & "ON INDUSTRY_JOBS.outputTypeID = TOTAL_JOBS.outputTypeID "
        SQL = SQL & "AND INDUSTRY_JOBS.licensedProductionRuns = TOTAL_JOBS.licensedProductionRuns "
        SQL = SQL & "AND INDUSTRY_JOBS.materialMultiplier = TOTAL_JOBS.materialMultiplier "
        SQL = SQL & "AND INDUSTRY_JOBS.timeMultiplier = TOTAL_JOBS.timeMultiplier "

        SQL = SQL & "LEFT OUTER JOIN (SELECT typeName, typeID FROM INVENTORY_TYPES) AS CONTAINER_NAME ON INDUSTRY_JOBS.containerTypeID = CONTAINER_NAME.typeID, "
        SQL = SQL & "INVENTORY_TYPES, SOLAR_SYSTEMS "
        SQL = SQL & "WHERE (DATETIME(endProductionTime) >= DateTime('" & Format(dtpInventionStartDate.Value, SQLiteDateFormat) & "') "
        SQL = SQL & "AND DATETIME(endProductionTime) <= DateTime('" & Format(dtpInventionEndDate.Value, SQLiteDateFormat) & "')) "
        SQL = SQL & "AND INDUSTRY_JOBS.outputTypeID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND INDUSTRY_JOBS.installedInSolarSystemID = SOLAR_SYSTEMS.solarsystemID "
        SQL = SQL & "AND activityID = 8 AND completed = 1 AND InstallerID =" & SelectedCharacter.ID & " "

        If rbtnCorpJobs.Checked Then
            SQL = SQL & "AND JobType = " & CStr(ScanType.Corporation) & " "
        ElseIf rbtnPersonalJobs.Checked Then
            SQL = SQL & "AND JobType = " & CStr(ScanType.Personal) & " "
        End If

        ' Ignore perpetual motion machines
        SQL = SQL & "AND INDUSTRY_JOBS.outputTypeID NOT IN (27022,27024) "

        SQL = SQL & "GROUP BY containerTypeID, INDUSTRY_JOBS.outputTypeID, INDUSTRY_JOBS.licensedProductionRuns, "
        SQL = SQL & "INDUSTRY_JOBS.materialMultiplier, INDUSTRY_JOBS.timeMultiplier, FAIL.TOTAL, SUCCESS.TOTAL, "
        SQL = SQL & "TOTAL_JOBS.TOTAL, installedItemLicensedProductionRunsRemaining, SOLAR_SYSTEMS.solarSystemName "

        DBCommand = New SQLiteCommand(SQL, DB)
        rsInvention = DBCommand.ExecuteReader

        Application.UseWaitCursor = True
        lstInventionItems.Items.Clear()
        lstInventionItems.BeginUpdate()

        While rsInvention.Read
            lstJobRow = lstInventionItems.Items.Add(CStr(rsInvention.GetInt32(1))) ' BP ID
            'The remaining columns are subitems  
            lstJobRow.SubItems.Add(rsInvention.GetString(2)) ' BP Name
            lstJobRow.SubItems.Add(CStr(rsInvention.GetInt32(3))) ' BP Runs
            lstJobRow.SubItems.Add(CStr(rsInvention.GetDouble(4))) ' ME
            lstJobRow.SubItems.Add(CStr(rsInvention.GetDouble(5))) ' TE
            lstJobRow.SubItems.Add(rsInvention.GetString(10)) ' Location Solar System
            lstJobRow.SubItems.Add(rsInvention.GetString(0)) ' Location
            lstJobRow.SubItems.Add(CStr(rsInvention.GetInt32(6))) ' Failures
            lstJobRow.SubItems.Add(CStr(rsInvention.GetInt32(7))) ' Successes
            lstJobRow.SubItems.Add(CStr(rsInvention.GetInt32(8))) ' Total Attempts
            lstJobRow.SubItems.Add(CStr(rsInvention.GetInt32(9))) ' BPC Runs
        End While

        lstInventionItems.EndUpdate()
        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

    Private Sub btnUpdateJobs_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateJobs.Click
        Application.UseWaitCursor = True
        Application.DoEvents()

        ' Update jobs from API
        If rbtnPersonalJobs.Checked Or rbtnBothJobs.Checked Then
            ' Load the personal jobs
            Call SelectedCharacter.GetIndustryJobs.LoadIndustryJobs(ScanType.Personal, True)
        End If

        If rbtnBothJobs.Checked Or rbtnCorpJobs.Checked Then
            Call SelectedCharacter.CharacterCorporation.GetIndustryJobs.LoadIndustryJobs(ScanType.Corporation, True)
        End If

        Application.UseWaitCursor = False
        Application.DoEvents()

        MsgBox("Industry Jobs updated.", vbInformation, Application.ProductName)

        ' Update the grid
        Call RefreshGrid()

    End Sub

    ' On clicking a row, need to update the form 
    Private Sub lstInventionItems_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstInventionItems.SelectedIndexChanged
        Dim BPImage As String
        Dim BPTechImagePath As String = ""
        Dim Attempts As Integer
        Dim Successes As Integer
        Dim readerDecryptor As SQLiteDataReader
        'Dim SQL As String
        'Dim TempDecryptor As Decryptor

        If lstInventionItems.SelectedIndices.Count <> 0 Then
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            SelectedBPID = CLng(lstInventionItems.SelectedItems(0).SubItems(0).Text)

            ' First put the blueprint in the top label
            lblSelectedBP.Text = lstInventionItems.SelectedItems(0).SubItems(1).Text

            ' Now select the BP image
            BPImage = UserImagePath & CStr(SelectedBPID & "_64.png")

            If System.IO.File.Exists(BPImage) Then
                pictInvention.Image = Image.FromFile(BPImage)
            Else
                pictInvention.Image = Nothing
            End If

            pictInvention.Update()

            ' Attempt stats
            Attempts = CInt(lstInventionItems.SelectedItems(0).SubItems(9).Text)
            Successes = CInt(lstInventionItems.SelectedItems(0).SubItems(8).Text)
            lblTotalAttempts.Text = lstInventionItems.SelectedItems(0).SubItems(9).Text
            lblTotalSuccesses.Text = lstInventionItems.SelectedItems(0).SubItems(8).Text
            lblSuccessRate.Text = FormatPercent(Successes / Attempts, 2)

            ' Load the decryptor list first for this BP and select the one they used if any
            ' Clear anything that was there
            cmbBPDecryptor.Items.Clear()

            ' Add NONE
            cmbBPDecryptor.Items.Add(None)
            ' If the ME is base, no decryptor was used
            If lstInventionItems.SelectedItems(0).SubItems(4).Text = CStr(BaseT2T3ME) Then
                SelectedDecryptor = NoDecryptor
            End If

            'SQL = "SELECT typeName FROM INVENTORY_TYPES WHERE groupID =" & DecryptorGroup

            'DBCommand = New SQLiteCommand(SQL, DB)
            'readerDecryptor = DBCommand.ExecuteReader

            'Dim InventionDecryptors As New DecryptorList()

            'While readerDecryptor.Read
            '    cmbBPDecryptor.Items.Add(readerDecryptor.GetString(0))

            '    ' Get the decryptor and compare it to the ME of the final BP they selected
            '    TempDecryptor = InventionDecryptors.GetDecryptor(readerDecryptor.GetString(0))

            '    If TempDecryptor.MEMod + BaseT2T3ME = CInt(lstInventionItems.SelectedItems(0).SubItems(3).Text) Then
            '        SelectedDecryptor = TempDecryptor
            '    End If
            'End While

            'readerDecryptor.Close()

            ' Finally set the decryptor used in the combo
            cmbBPDecryptor.Text = SelectedDecryptor.Name

            readerDecryptor = Nothing
            DBCommand = Nothing

            ' Display stats and load invention bp
            Call DisplayInventionStats()

        End If

        Me.Cursor = Cursors.Default
        lstInventionItems.UseWaitCursor = False
        cmbSkill1.Enabled = True
        cmbSkill2.Enabled = True
        cmbSkill3.Enabled = True
        cmbBPDecryptor.Enabled = True
        Application.DoEvents()

    End Sub

    ' Just display the invention chance in the label
    Private Sub DisplayInventionStats()
        Dim TempBlueprint As Blueprint
        Dim InventionSkills As New EVESkillList
        Dim SelectedDecryptor As New Decryptor

        Dim InventionDecryptors As New DecryptorList()
        SelectedDecryptor = InventionDecryptors.GetDecryptor(cmbBPDecryptor.Text)

        ' Now show the user information for invention
        gbSkills.Visible = True

        ' Build the T2 item as a blueprint and then get stats - CHECK
        TempBlueprint = New Blueprint(CLng(lstInventionItems.SelectedItems(0).SubItems(0).Text), 1, 0, 0, 1, 1, SelectedCharacter, _
                                        UserApplicationSettings, False, 0, NoTeam, SelectedBPManufacturingFacility, NoTeam, _
                                        SelectedBPComponentManufacturingFacility, SelectedBPCapitalComponentManufacturingFacility)
        ' Invent the bp
        Call TempBlueprint.InventBlueprint(1, SelectedDecryptor, SelectedBPInventionFacility, NoTeam, SelectedBPCopyFacility, NoTeam, 0)
        Call TempBlueprint.BuildItems(False, False, False, False, False)

        ' Now get the data from the blueprint
        lblSuccessChance.Text = FormatPercent(TempBlueprint.GetInventionChance, 2)
        ' Skills
        InventionSkills = TempBlueprint.GetReqInventionSkills()

        ' Set the combos and labels for skills
        SettingComboSkills = True
        lblSkill1.Text = InventionSkills.GetSkillList(0).Name
        cmbSkill1.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(InventionSkills.GetSkillList(0).Name))
        lblSkill2.Text = InventionSkills.GetSkillList(1).Name
        cmbSkill2.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(InventionSkills.GetSkillList(1).Name))
        lblSkill3.Text = InventionSkills.GetSkillList(2).Name
        cmbSkill3.Text = CStr(SelectedCharacter.Skills.GetSkillLevel(InventionSkills.GetSkillList(2).Name))
        SettingComboSkills = False

        Application.DoEvents()

    End Sub

    ' This if for when the user just selects each skill after clicking on the BP to see what would change with skills/decryptor
    Private Sub DisplayUpdatedInventionChance()
        Dim EncryptionSkillLevel As Integer = 0
        Dim DatacoreSkill1 As Integer = 0
        Dim DatacoreSkill2 As Integer = 0
        Dim BaseInventionChance As Double = 0
        Dim EncryptionSkillIndex As Integer = -1

        If Not SettingComboSkills Then

            ' Take the data in the decryptor and skill levels and update the invention chance
            If lblSkill1.Text.Contains("Encryption") Then
                EncryptionSkillLevel = CInt(cmbSkill1.Text)
                EncryptionSkillIndex = 1
            ElseIf lblSkill2.Text.Contains("Encryption") Then
                EncryptionSkillLevel = CInt(cmbSkill2.Text)
                EncryptionSkillIndex = 2
            ElseIf lblSkill3.Text.Contains("Encryption") Then
                EncryptionSkillLevel = CInt(cmbSkill3.Text)
                EncryptionSkillIndex = 3
            End If

            Select Case EncryptionSkillIndex
                Case 1
                    DatacoreSkill1 = CInt(cmbSkill2.Text)
                    DatacoreSkill2 = CInt(cmbSkill3.Text)
                Case 2
                    DatacoreSkill1 = CInt(cmbSkill1.Text)
                    DatacoreSkill2 = CInt(cmbSkill3.Text)
                Case 3
                    DatacoreSkill1 = CInt(cmbSkill1.Text)
                    DatacoreSkill2 = CInt(cmbSkill2.Text)
            End Select

            lblSuccessChance.Text = FormatPercent(GetBaseInventionChance(GetT1Material(SelectedBPID)) * (1 + (0.01 * EncryptionSkillLevel)) * (1 + (0.02 * (DatacoreSkill1 + DatacoreSkill2))) * SelectedDecryptor.ProductionMod, 2)

        End If
    End Sub

    ' Sets the base invention chance - takes a group of blueprint as main argument
    Private Function GetBaseInventionChance(T1Material As Material) As Double
        Dim BaseInventionChance As Double = 0
        Dim GroupName As String

        '* Base chance is 20% for battlecruisers, battleships, Hulk
        '** Base chance is 25% for cruisers, industrials, Mackinaw
        '*** Base chance is 30% for frigates, destroyers, Skiff, freighters
        '**** Base chance is 40% for all other inventables
        Select Case T1Material.GetMaterialName ' Check the item name
            ' Need to check this
            Case Procurer, Retriever, Covetor
                GroupName = T1Material.GetMaterialName
            Case Else
                GroupName = T1Material.GetMaterialGroup
        End Select

        Select Case GroupName
            Case "Frigate", "Destroyer", Procurer, "Freighter"
                BaseInventionChance = 0.3
            Case "Cruiser", "Industrial", Retriever
                BaseInventionChance = 0.25
            Case "Combat Battlecruiser", "Attack Battlecrusier", "Battleship", Covetor
                BaseInventionChance = 0.2
            Case Else
                BaseInventionChance = 0.4
        End Select

        Return BaseInventionChance

    End Function

#Region "ObjectFunctions"

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Call ClearForm()
        Call ResetForm()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub dtpInventionStartDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpInventionStartDate.ValueChanged
        If Not FirstFormLoad Then
            Call ClearForm()
            ' For some reason, a > of two equal dates returns true when it should be false - so decrement by 1 second
            If DateAdd(DateInterval.Second, -1, dtpInventionStartDate.Value) > dtpInventionEndDate.Value Then
                MsgBox("The Invention Start Date cannot be greater than the End Date", vbExclamation, Application.ProductName)
                dtpInventionStartDate.Focus()
            Else
                RefreshGrid()
            End If
        End If
    End Sub

    Private Sub dtpInventionEndDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpInventionEndDate.ValueChanged
        If Not firstformload Then
            Call ClearForm()
            If dtpInventionStartDate.Value > dtpInventionEndDate.Value Then
                MsgBox("The Invention Start Date cannot be greater than the End Date", vbExclamation, Application.ProductName)
                dtpInventionStartDate.Focus()
            Else
                RefreshGrid()
            End If
        End If
    End Sub

    Private Sub rbtnBothJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBothJobs.CheckedChanged
        Call RefreshGrid()
    End Sub

    Private Sub rbtnPersonalJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPersonalJobs.CheckedChanged
        Call RefreshGrid()
    End Sub

    Private Sub rbtnCorpJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCorpJobs.CheckedChanged
        Call RefreshGrid()
    End Sub

    Private Sub cmbSkill1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSkill1.SelectedIndexChanged
        Call DisplayUpdatedInventionChance()
    End Sub

    Private Sub cmbSkill2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSkill2.SelectedIndexChanged
        Call DisplayUpdatedInventionChance()
    End Sub

    Private Sub cmbSkill3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSkill3.SelectedIndexChanged
        Call DisplayUpdatedInventionChance()
    End Sub

    Private Sub cmbBPDecryptor_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbBPDecryptor.SelectedIndexChanged
        If cmbBPDecryptor.Text = None Or cmbBPDecryptor.Text = "" Then
            SelectedDecryptor = NoDecryptor
        Else
            Dim InventionDecryptors As New DecryptorList()
            SelectedDecryptor = InventionDecryptors.GetDecryptor(cmbBPDecryptor.Text)
        End If
        Call DisplayUpdatedInventionChance()
    End Sub

#End Region

End Class