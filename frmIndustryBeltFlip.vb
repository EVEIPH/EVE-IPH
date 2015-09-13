﻿
Imports System.Data.SQLite

Public Class frmIndustryBeltFlip

    Private Ore1ColumnSorter As ListViewColumnSorter
    Private Mineral1ColumnSorter As ListViewColumnSorter
    Private Ore2ColumnSorter As ListViewColumnSorter
    Private Mineral2ColumnSorter As ListViewColumnSorter
    Private Ore3ColumnSorter As ListViewColumnSorter
    Private Mineral3ColumnSorter As ListViewColumnSorter
    Private Ore4ColumnSorter As ListViewColumnSorter
    Private Mineral4ColumnSorter As ListViewColumnSorter
    Private Ore5ColumnSorter As ListViewColumnSorter
    Private Mineral5ColumnSorter As ListViewColumnSorter

    Private FirstLoad As Boolean

    Public Sub New()

        FirstLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call LoadSettings()

    End Sub

    Private Sub frmIndustryBeltFlip_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor

        Call LoadAllTables()

        Me.Cursor = Cursors.Default
        FirstLoad = False

    End Sub

    Private Sub LoadSettings()

        ' Station refinery settings
        cmbMineStationEff.Text = FormatPercent(UserApplicationSettings.RefiningEfficiency, 0)
        txtMineRefineStanding.Text = FormatNumber(UserApplicationSettings.RefineCorpStanding, 2)
        cmbRefineStationTax.Text = FormatPercent(UserApplicationSettings.RefiningTax, 1)

        ' Miner settings
        txtCycleTime.Text = FormatNumber(UserIndustryFlipBeltSettings.CycleTime)
        txtm3perCycle.Text = FormatNumber(UserIndustryFlipBeltSettings.m3perCycle)
        cmbNumMiners.Text = CStr(UserIndustryFlipBeltSettings.NumMiners)
        chkCompressOre.Checked = UserIndustryFlipBeltSettings.CompressOre
        chkIPHperMiner.Checked = UserIndustryFlipBeltSettings.IPHperMiner

        If UserIndustryFlipBeltSettings.TrueSec = "" Or UserIndustryFlipBeltSettings.TrueSec = rbtn0percent.Text Then
            rbtn0percent.Checked = True
        ElseIf UserIndustryFlipBeltSettings.TrueSec = rbtn5percent.Text Then
            rbtn5percent.Checked = True
        ElseIf UserIndustryFlipBeltSettings.TrueSec = rbtn10percent.Text Then
            rbtn10percent.Checked = True
        End If

        'm3/hr/miner =  m3 per cycle / cycletime * 3600
        lblm3perhrperminer.Text = FormatNumber(CDbl(txtm3perCycle.Text) / CDbl(txtCycleTime.Text) * 3600, 2)

        ' Tax settings
        chkMineIncludeBrokerFees.Checked = UserIndustryFlipBeltSettings.IncludeBrokerFees
        chkMineIncludeTaxes.Checked = UserIndustryFlipBeltSettings.IncludeTaxes

        If UserApplicationSettings.ShowToolTips Then
            ttMain.SetToolTip(rbtn0percent, "No Bonus for Enormous or Colossal Belts")
            ttMain.SetToolTip(rbtn5percent, "5% Ore Variants in Enormous or Colossal Belts")
            ttMain.SetToolTip(rbtn10percent, "10% Ore Variants in Enormous or Colossal Belts")
        End If

    End Sub

    Private Function CheckEnteredData() As Boolean

        If Trim(cmbNumMiners.Text) <> "" Then
            If Not IsNumeric(cmbNumMiners.Text) Then
                MsgBox("Invalid Number of Miners value", vbExclamation, Application.ProductName)
                cmbNumMiners.Focus()
                tabIndustryBelts.SelectedTab = tabSummary
                Return False
            End If
        Else
            MsgBox("Please enter a Number of Miners value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If CInt(cmbNumMiners.Text) > 101 Then
            MsgBox("Maximum miners is 100", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If CInt(cmbNumMiners.Text) <= 0 Then
            MsgBox("Number of miners must be greater than 0", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If Trim(txtCycleTime.Text) <> "" Then
            If Not IsNumeric(txtCycleTime.Text) Then
                MsgBox("Invalid Cycle Time value", vbExclamation, Application.ProductName)
                txtCycleTime.Focus()
                tabIndustryBelts.SelectedTab = tabSummary
                Return False
            End If
        Else
            MsgBox("Please enter a Cycle Time value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If CDbl(txtCycleTime.Text) <= 0 Then
            MsgBox("Cycle time must be greater than 0", vbExclamation, Application.ProductName)
            txtm3perCycle.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If Trim(txtm3perCycle.Text) <> "" Then
            If Not IsNumeric(txtm3perCycle.Text) Then
                MsgBox("Invalid m3 per Cycle value", vbExclamation, Application.ProductName)
                txtm3perCycle.Focus()
                tabIndustryBelts.SelectedTab = tabSummary
                Return False
            End If
        Else
            MsgBox("Please enter a m3 per Cycle value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If CDbl(txtm3perCycle.Text) <= 0 Then
            MsgBox("m3 per Cycle value must be greater than 0", vbExclamation, Application.ProductName)
            txtm3perCycle.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        ' Check the refine values
        If Trim(txtMineRefineStanding.Text) <> "" Then
            If CDbl(txtMineRefineStanding.Text) > 10 Then
                MsgBox("Please set a smaller station standing value", vbExclamation, Application.ProductName)
                txtMineRefineStanding.Focus()
                tabIndustryBelts.SelectedTab = tabSummary
                Return False
            End If
        Else
            MsgBox("Please enter a Refine Standing value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        If Trim(cmbRefineStationTax.Text) = "" Then
            MsgBox("Please enter a Refine Station Tax value", vbExclamation, Application.ProductName)
            cmbNumMiners.Focus()
            tabIndustryBelts.SelectedTab = tabSummary
            Return False
        End If

        Return True

    End Function

    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click
        Dim TempSettings As IndustryFlipBeltSettings = Nothing
        Dim Settings As New ProgramSettings
        Dim TempDouble As Double

        If Not CheckEnteredData() Then
            Exit Sub
        End If

        ' Refining
        ' Station numbers
        TempDouble = CDbl(cmbMineStationEff.Text.Replace("%", ""))

        If TempDouble <= 0 Then
            UserApplicationSettings.RefiningEfficiency = 0
        Else
            UserApplicationSettings.RefiningEfficiency = TempDouble / 100
        End If

        TempDouble = CDbl(cmbRefineStationTax.Text.Replace("%", ""))

        If TempDouble <= 0 Then
            UserApplicationSettings.RefiningTax = 0
        Else
            UserApplicationSettings.RefiningTax = TempDouble / 100
        End If

        ' Allow them to update the refine standing here as well
        UserApplicationSettings.RefineCorpStanding = CDbl(txtMineRefineStanding.Text)

        TempSettings.CompressOre = chkCompressOre.Checked
        TempSettings.IPHperMiner = chkIPHperMiner.Checked
        TempSettings.CycleTime = CDbl(txtCycleTime.Text)
        TempSettings.m3perCycle = CDbl(txtm3perCycle.Text)
        TempSettings.NumMiners = CInt(cmbNumMiners.Text)

        TempSettings.IncludeBrokerFees = chkMineIncludeBrokerFees.Checked
        TempSettings.IncludeTaxes = chkMineIncludeTaxes.Checked

        If rbtn0percent.Checked Then
            TempSettings.TrueSec = rbtn0percent.Text
        ElseIf rbtn5percent.Checked Then
            TempSettings.TrueSec = rbtn5percent.Text
        ElseIf rbtn10percent.Checked Then
            TempSettings.TrueSec = rbtn10percent.Text
        End If

        ' Save it in the Application settings
        Call Settings.SaveApplicationSettings(UserApplicationSettings)

        ' Save the data in the XML file
        Call Settings.SaveIndustryFlipBeltSettings(TempSettings)

        ' Save the data to the local variable
        UserIndustryFlipBeltSettings = TempSettings

        ' Refresh tables
        Call LoadAllTables()

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Call LoadAllTables()
    End Sub

    Private Sub LoadAllTables()

        FirstLoad = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If Not CheckEnteredData() Then
            FirstLoad = False
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            Exit Sub
        End If

        ' Calc the m3 per hr per miner first
        ' m3/hr/miner =  m3 per cycle / cycletime * 3600
        lblm3perhrperminer.Text = FormatNumber(CDbl(txtm3perCycle.Text) / CDbl(txtCycleTime.Text) * 3600, 2)

        ' Small
        Call LoadBeltTable(BeltType.Small)
        Call DisplayBeltMinerals(BeltType.Small)

        ' Moderate
        Call LoadBeltTable(BeltType.Medium)
        Call DisplayBeltMinerals(BeltType.Medium)

        ' Large
        Call LoadBeltTable(BeltType.Large)
        Call DisplayBeltMinerals(BeltType.Large)

        ' Extra Large
        Call LoadBeltTable(BeltType.Enormous)
        Call DisplayBeltMinerals(BeltType.Enormous)

        ' Giant
        Call LoadBeltTable(BeltType.Colossal)
        Call DisplayBeltMinerals(BeltType.Colossal)

        FirstLoad = False
        Me.Cursor = Cursors.Default
        Application.DoEvents()

    End Sub

    Private Sub LoadBeltTable(Belt As BeltType)
        Dim SQL As String
        Dim readerBelts As SQLiteDataReader
        Dim lstOreRow As ListViewItem
        Dim CurrentList As ListView = Nothing

        If Not CheckEnteredData() Then
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            Exit Sub
        End If

        SQL = "SELECT ORE, AMOUNT, NUMBER_ASTEROIDS FROM INDUSTRY_UPGRADE_BELTS "
        SQL = SQL & "WHERE ( BELT_NAME = "

        Select Case Belt
            Case BeltType.Small
                SQL = SQL & "'Small' "
                CurrentList = lstOresLevel1
                Ore1ColumnSorter = New ListViewColumnSorter()
                lstOresLevel1.ListViewItemSorter = Ore1ColumnSorter
            Case BeltType.Medium
                SQL = SQL & "'Medium' "
                CurrentList = lstOresLevel2
                Ore2ColumnSorter = New ListViewColumnSorter()
                lstOresLevel2.ListViewItemSorter = Ore2ColumnSorter
            Case BeltType.Large
                SQL = SQL & "'Large' "
                CurrentList = lstOresLevel3
                Ore3ColumnSorter = New ListViewColumnSorter()
                lstOresLevel3.ListViewItemSorter = Ore3ColumnSorter
            Case BeltType.Enormous
                SQL = SQL & "'Enormous' "
                CurrentList = lstOresLevel4
                Ore4ColumnSorter = New ListViewColumnSorter()
                lstOresLevel4.ListViewItemSorter = Ore4ColumnSorter
            Case BeltType.Colossal
                SQL = SQL & "'Colossal' "
                CurrentList = lstOresLevel5
                Ore5ColumnSorter = New ListViewColumnSorter()
                lstOresLevel5.ListViewItemSorter = Ore5ColumnSorter
        End Select

        SQL = SQL & ") "

        ' Select ore type based on truesec bonus
        If rbtn0percent.Checked Or Belt = BeltType.Small Or Belt = BeltType.Medium Then ' Small and Medium belts are always base ores
            SQL = SQL & "AND TRUESEC_BONUS = 0 "
        ElseIf rbtn5percent.Checked Then
            SQL = SQL & "AND TRUESEC_BONUS = 5 "
        ElseIf rbtn10percent.Checked Then
            SQL = SQL & "AND TRUESEC_BONUS = 10 "
        End If

        SQL = SQL & "ORDER BY ORE"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerBelts = DBCommand.ExecuteReader

        Call CurrentList.Items.Clear()
        Call CurrentList.BeginUpdate()

        While readerBelts.Read
            lstOreRow = CurrentList.Items.Add("")
            'The remaining columns are subitems
            lstOreRow.SubItems.Add(readerBelts.GetString(0))
            lstOreRow.SubItems.Add(CStr(readerBelts.GetInt32(2)))
            lstOreRow.SubItems.Add(FormatNumber(readerBelts.GetInt32(1), 0))

            ' All records are initially checked
            lstOreRow.Checked = GetOreCheckValue(readerBelts.GetString(0), Belt)
        End While

        Call CurrentList.EndUpdate()

    End Sub

    ' Set ore checks by the type of belt sent and ore
    Private Function GetOreCheckValue(ByVal OreName As String, Belt As BeltType) As Boolean
        Dim Settings As IndustryBeltOreChecks
        Dim ReturnValue As Boolean

        Select Case Belt
            Case BeltType.Small
                Settings = UserIndustryFlipBeltOreCheckSettings1
            Case BeltType.Medium
                Settings = UserIndustryFlipBeltOreCheckSettings2
            Case BeltType.Large
                Settings = UserIndustryFlipBeltOreCheckSettings3
            Case BeltType.Enormous
                Settings = UserIndustryFlipBeltOreCheckSettings4
            Case BeltType.Colossal
                Settings = UserIndustryFlipBeltOreCheckSettings5
        End Select

        Select Case OreName
            Case "Plagioclase"
                ReturnValue = Settings.Plagioclase
            Case "Spodumain"
                ReturnValue = Settings.Spodumain
            Case "Kernite"
                ReturnValue = Settings.Kernite
            Case "Hedbergite"
                ReturnValue = Settings.Hedbergite
            Case "Arkonor"
                ReturnValue = Settings.Arkonor
            Case "Bistot"
                ReturnValue = Settings.Bistot
            Case "Pyroxeres"
                ReturnValue = Settings.Pyroxeres
            Case "Crokite"
                ReturnValue = Settings.Crokite
            Case "Jaspet"
                ReturnValue = Settings.Jaspet
            Case "Omber"
                ReturnValue = Settings.Omber
            Case "Scordite"
                ReturnValue = Settings.Scordite
            Case "Gneiss"
                ReturnValue = Settings.Gneiss
            Case "Veldspar"
                ReturnValue = Settings.Veldspar
            Case "Hemorphite"
                ReturnValue = Settings.Hemorphite
            Case "Dark Ochre"
                ReturnValue = Settings.DarkOchre
            Case "Mercoxit"
                ReturnValue = Settings.Mercoxit
            Case "Crimson Arkonor"
                ReturnValue = Settings.CrimsonArkonor
            Case "Prime Arkonor"
                ReturnValue = Settings.PrimeArkonor
            Case "Triclinic Bistot"
                ReturnValue = Settings.TriclinicBistot
            Case "Monoclinic Bistot"
                ReturnValue = Settings.MonoclinicBistot
            Case "Sharp Crokite"
                ReturnValue = Settings.SharpCrokite
            Case "Crystalline Crokite"
                ReturnValue = Settings.CrystallineCrokite
            Case "Onyx Ochre"
                ReturnValue = Settings.OnyxOchre
            Case "Obsidian Ochre"
                ReturnValue = Settings.ObsidianOchre
            Case "Vitric Hedbergite"
                ReturnValue = Settings.VitricHedbergite
            Case "Glazed Hedbergite"
                ReturnValue = Settings.GlazedHedbergite
            Case "Vivid Hemorphite"
                ReturnValue = Settings.VividHemorphite
            Case "Radiant Hemorphite"
                ReturnValue = Settings.RadiantHemorphite
            Case "Pure Jaspet"
                ReturnValue = Settings.PureJaspet
            Case "Pristine Jaspet"
                ReturnValue = Settings.PristineJaspet
            Case "Luminous Kernite"
                ReturnValue = Settings.LuminousKernite
            Case "Fiery Kernite"
                ReturnValue = Settings.FieryKernite
            Case "Azure Plagioclase"
                ReturnValue = Settings.AzurePlagioclase
            Case "Rich Plagioclase"
                ReturnValue = Settings.RichPlagioclase
            Case "Solid Pyroxeres"
                ReturnValue = Settings.SolidPyroxeres
            Case "Viscous Pyroxeres"
                ReturnValue = Settings.ViscousPyroxeres
            Case "Condensed Scordite"
                ReturnValue = Settings.CondensedScordite
            Case "Massive Scordite"
                ReturnValue = Settings.MassiveScordite
            Case "Bright Spodumain"
                ReturnValue = Settings.BrightSpodumain
            Case "Gleaming Spodumain"
                ReturnValue = Settings.GleamingSpodumain
            Case "Concentrated Veldspar"
                ReturnValue = Settings.ConcentratedVeldspar
            Case "Dense Veldspar"
                ReturnValue = Settings.DenseVeldspar
            Case "Iridescent Gneiss"
                ReturnValue = Settings.IridescentGneiss
            Case "Prismatic Gneiss"
                ReturnValue = Settings.PrismaticGneiss
            Case "Silvery Omber"
                ReturnValue = Settings.SilveryOmber
            Case "Golden Omber"
                ReturnValue = Settings.GoldenOmber
            Case "Magma Mercoxit"
                ReturnValue = Settings.MagmaMercoxit
            Case "Vitreous Mercoxit"
                ReturnValue = Settings.VitreousMercoxit
        End Select

        Return ReturnValue

    End Function

    ' Refines the belt sent and loads the appropriate table
    Private Sub DisplayBeltMinerals(Belt As BeltType)
        Dim SQL As String = ""
        Dim readerBelts As SQLiteDataReader
        Dim lstOreRow As ListViewItem = Nothing
        Dim CurrentOreList As ListView = Nothing
        Dim CurrentMineralList As ListView = Nothing

        Dim TotalIskLabelForm As Label = Nothing
        Dim HrsToFlipForm As Label = Nothing
        Dim IPHForm As Label = Nothing
        Dim TotalVolumeForm As Label = Nothing

        Dim TotalIskLabelSum As Label = Nothing
        Dim HrsToFlipSum As Label = Nothing
        Dim IPHSum As Label = Nothing
        Dim TotalVolumeSum As Label = Nothing

        Dim item As ListViewItem
        Dim checkedItems As ListView.CheckedListViewItemCollection
        Dim TotalRefinedMinerals As New Materials
        Dim TotalCost As Double = 0
        Dim OutputNumber As Double
        Dim OreName As String

        Dim BeltVolume As Double
        Dim CompressedVolume As Double
        Dim TimeToFlip As Double
        Dim TimeToFlipPer As Double

        ' Refining
        Dim StationEffiency As Double
        Dim StationTax As Double

        Dim TempDouble = CDbl(cmbMineStationEff.Text.Replace("%", ""))

        If TempDouble <= 0 Then
            StationEffiency = 0
        Else
            StationEffiency = TempDouble / 100
        End If

        TempDouble = CDbl(cmbRefineStationTax.Text.Replace("%", ""))

        If TempDouble <= 0 Then
            StationTax = 0
        Else
            StationTax = TempDouble / 100
        End If

        Dim RefinedMaterials As New Materials
        Dim RefiningStation As New RefiningReprocessing(SelectedCharacter.Skills.GetSkillLevel(3385), _
                                                        SelectedCharacter.Skills.GetSkillLevel(3389), _
                                                        SelectedCharacter.Skills.GetSkillLevel(12196), _
                                                        UserApplicationSettings.RefiningImplantValue, _
                                                        StationEffiency, StationTax, CDbl(txtMineRefineStanding.Text))

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If Not CheckEnteredData() Then
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            Exit Sub
        End If

        Select Case Belt
            Case BeltType.Small
                SQL = SQL & "'Small' "
                CurrentOreList = lstOresLevel1
                CurrentMineralList = lstMineralsLevel1
                Mineral1ColumnSorter = New ListViewColumnSorter()
                lstMineralsLevel1.ListViewItemSorter = Mineral1ColumnSorter

                TotalIskLabelForm = lblTotalIskLevel1
                HrsToFlipForm = lblTotalHourstoFlip1
                IPHForm = lblIPH1
                TotalVolumeForm = lblTotalBeltVolume1

                TotalIskLabelSum = lblTotalIskLevel1Sum
                HrsToFlipSum = lblTotalHourstoFlip1Sum
                IPHSum = lblTotalIPH1Sum
                TotalVolumeSum = lblTotalBeltVolume1Sum

            Case BeltType.Medium
                SQL = SQL & "'Medium' "
                CurrentOreList = lstOresLevel2
                CurrentMineralList = lstMineralsLevel2
                Mineral2ColumnSorter = New ListViewColumnSorter()
                lstMineralsLevel2.ListViewItemSorter = Mineral2ColumnSorter

                TotalIskLabelForm = lblTotalIskLevel2
                HrsToFlipForm = lblTotalHourstoFlip2
                IPHForm = lblIPH2
                TotalVolumeForm = lblTotalBeltVolume2

                TotalIskLabelSum = lblTotalIskLevel2Sum
                HrsToFlipSum = lblTotalHourstoFlip2Sum
                IPHSum = lblTotalIPH2Sum
                TotalVolumeSum = lblTotalBeltVolume2Sum

            Case BeltType.Large
                SQL = SQL & "'Large' "
                CurrentOreList = lstOresLevel3
                CurrentMineralList = lstMineralsLevel3
                Mineral3ColumnSorter = New ListViewColumnSorter()
                lstMineralsLevel3.ListViewItemSorter = Mineral3ColumnSorter

                TotalIskLabelForm = lblTotalIskLevel3
                HrsToFlipForm = lblTotalHourstoFlip3
                IPHForm = lblIPH3
                TotalVolumeForm = lblTotalBeltVolume3

                TotalIskLabelSum = lblTotalIskLevel3Sum
                HrsToFlipSum = lblTotalHourstoFlip3Sum
                IPHSum = lblTotalIPH3Sum
                TotalVolumeSum = lblTotalBeltVolume3Sum

            Case BeltType.Enormous
                SQL = SQL & "'Extra Large' "
                CurrentOreList = lstOresLevel4
                CurrentMineralList = lstMineralsLevel4
                Mineral4ColumnSorter = New ListViewColumnSorter()
                lstMineralsLevel4.ListViewItemSorter = Mineral4ColumnSorter

                TotalIskLabelForm = lblTotalIskLevel4
                HrsToFlipForm = lblTotalHourstoFlip4
                IPHForm = lblIPH4
                TotalVolumeForm = lblTotalBeltVolume4

                TotalIskLabelSum = lblTotalIskLevel4Sum
                HrsToFlipSum = lblTotalHourstoFlip4Sum
                IPHSum = lblTotalIPH4Sum
                TotalVolumeSum = lblTotalBeltVolume4Sum

            Case BeltType.Colossal
                SQL = SQL & "'Giant' "
                CurrentOreList = lstOresLevel5
                CurrentMineralList = lstMineralsLevel5
                Mineral5ColumnSorter = New ListViewColumnSorter()
                lstMineralsLevel5.ListViewItemSorter = Mineral5ColumnSorter

                TotalIskLabelForm = lblTotalIskLevel5
                HrsToFlipForm = lblTotalHourstoFlip5
                IPHForm = lblIPH5
                TotalVolumeForm = lblTotalBeltVolume5

                TotalIskLabelSum = lblTotalIskLevel5Sum
                HrsToFlipSum = lblTotalHourstoFlip5Sum
                IPHSum = lblTotalIPH5Sum
                TotalVolumeSum = lblTotalBeltVolume5Sum

        End Select

        ' Just work with the ones that are checked
        checkedItems = CurrentOreList.CheckedItems

        If checkedItems.Count > 0 Then
            ' Update each item based on inputs
            For Each item In checkedItems
                OreName = CType(item.SubItems(1).Text, String)
                SQL = "SELECT typeID from INVENTORY_TYPES WHERE typeName = '" & OreName & "'"

                DBCommand = New SQLiteCommand(SQL, DB)
                readerBelts = DBCommand.ExecuteReader

                If readerBelts.Read Then
                    ' Refine each ore in the ore list, store refined minerals
                    RefinedMaterials = RefiningStation.RefineOre(readerBelts.GetInt64(0), SelectedCharacter.Skills.GetSkillLevel(OreName & " Processing"), _
                                    CType(item.SubItems(3).Text, Double), chkMineIncludeTaxes.Checked, chkMineIncludeBrokerFees.Checked, OutputNumber)

                    ' Store the refined materials
                    TotalRefinedMinerals.InsertMaterialList(RefinedMaterials.GetMaterialList)

                    If chkCompressOre.Checked = False Then
                        ' Save the total cost separate so we take into account taxes and fees
                        TotalCost = TotalCost + RefinedMaterials.GetTotalMaterialsCost
                    Else
                        Dim readerOre As SQLiteDataReader
                        Dim OreUnitPrice As Double
                        Dim TotalCompressedUnits As Integer

                        ' First, get the unit price and volume for the compressed ore
                        SQL = "SELECT PRICE FROM ITEM_PRICES WHERE ITEM_NAME LIKE 'Compressed " & OreName & "'"
                        DBCommand = New SQLiteCommand(SQL, DB)
                        readerOre = DBCommand.ExecuteReader

                        If readerOre.Read() Then
                            OreUnitPrice = readerOre.GetDouble(0)
                            TotalCompressedUnits = CInt(Math.Floor(CInt(item.SubItems(3).Text) / 100))
                            ' Calc total cost, assume all mined and then compressed
                            TotalCost = TotalCost + (OreUnitPrice * TotalCompressedUnits)
                        End If

                        readerOre.Close()

                    End If

                    ' Reset the value of the refined materials
                    TotalRefinedMinerals.ResetTotalValue(TotalCost)

                End If
                readerBelts.Close()
                DBCommand = Nothing

            Next

            ' Sort the list
            Call TotalRefinedMinerals.SortMaterialListByQuantity()

            Call CurrentMineralList.BeginUpdate()
            Call CurrentMineralList.Items.Clear()

            ' Now that we've refined all the ores, put the minerals into minerals list
            For i = 0 To TotalRefinedMinerals.GetMaterialList.Count - 1
                lstOreRow = CurrentMineralList.Items.Add(TotalRefinedMinerals.GetMaterialList(i).GetMaterialName)
                'The remaining columns are subitems
                lstOreRow.SubItems.Add(FormatNumber(TotalRefinedMinerals.GetMaterialList(i).GetQuantity, 0))
                lstOreRow.SubItems.Add(FormatNumber(TotalRefinedMinerals.GetMaterialList(i).GetTotalCost, 2))
            Next

            Call CurrentMineralList.EndUpdate()

            TotalIskLabelForm.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost, 2) & " ISK"
            TotalIskLabelSum.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost, 2) & " ISK"

            CompressedVolume = GetTotalVolume(Belt, chkCompressOre.Checked)
            BeltVolume = GetTotalVolume(Belt, False)

            TotalVolumeSum.Text = FormatNumber(CompressedVolume, 2)
            TotalVolumeForm.Text = FormatNumber(CompressedVolume, 2)

            TimeToFlip = (BeltVolume / (CDbl(lblm3perhrperminer.Text) * CInt(cmbNumMiners.Text))) * 3600
            TimeToFlipPer = (BeltVolume / CDbl(lblm3perhrperminer.Text)) * 3600

            HrsToFlipForm.Text = FormatIPHTime(TimeToFlip)
            HrsToFlipSum.Text = FormatIPHTime(TimeToFlip)

            If chkIPHperMiner.Checked = True Then
                IPHForm.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost / (TimeToFlipPer / 3600), 2)
                IPHSum.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost / (TimeToFlipPer / 3600), 2)
            Else
                IPHForm.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost / (TimeToFlip / 3600), 2)
                IPHSum.Text = FormatNumber(TotalRefinedMinerals.GetTotalMaterialsCost / (TimeToFlip / 3600), 2)
            End If

        Else
            ' Nothing checked so clear
            CurrentMineralList.Items.Clear()
        End If

        Me.Cursor = Cursors.Default
        Application.DoEvents()

    End Sub

    ' Returns the total volume compressed or regular for the belt sent - DOESN'T WORK ANYMORE - TODO
    Private Function GetTotalVolume(ByVal Belt As BeltType, ByVal Compress As Boolean) As Double
        Dim SQL As String = ""
        Dim readerBelts As SQLiteDataReader
        Dim CurrentOreList As ListView = Nothing
        Dim checkedItems As ListView.CheckedListViewItemCollection
        Dim OreName As String
        Dim OreUnits As Long
        Dim OreVolume As Double
        Dim TotalOreVolume As Double

        Dim CompressedBlockVolume As Double
        Dim CompressedBlocks As Long
        Dim UnitsToCompress As Long

        Dim item As ListViewItem

        Select Case Belt
            Case BeltType.Small
                CurrentOreList = lstOresLevel1
            Case BeltType.Medium
                CurrentOreList = lstOresLevel2
            Case BeltType.Large
                CurrentOreList = lstOresLevel3
            Case BeltType.Enormous
                CurrentOreList = lstOresLevel4
            Case BeltType.Colossal
                CurrentOreList = lstOresLevel5
        End Select

        ' Just work with the ones that are checked
        checkedItems = CurrentOreList.CheckedItems

        If checkedItems.Count > 0 Then
            ' For each row of ore, look up the volume and compressed volume. Then total volume + leftover amount
            For Each item In checkedItems

                OreName = CType(item.SubItems(1).Text, String)
                OreUnits = CType(item.SubItems(3).Text, Integer)

                SQL = "SELECT ORE_VOLUME "
                SQL = SQL & "FROM ORES WHERE BELT_TYPE = 'Ore' "
                If Compress Then
                    SQL = SQL & "AND ORE_NAME ='Compressed " & OreName & "'"
                Else
                    SQL = SQL & "AND ORE_NAME = '" & OreName & "'"
                End If
                DBCommand = New SQLiteCommand(SQL, DB)
                readerBelts = DBCommand.ExecuteReader

                If readerBelts.Read Then
                    ' Get all the base values
                    OreVolume = readerBelts.GetDouble(0)
                    UnitsToCompress = 100 ' Always 100 now
                Else
                    Return 0
                End If

                readerBelts.Close()

                If Compress Then
                    ' Compressed ORE
                    CompressedBlocks = CLng(Math.Floor(OreUnits / UnitsToCompress))
                    CompressedBlockVolume = OreVolume
                    ' Reset the total ore units so we can look up the rest
                    OreUnits = OreUnits - (UnitsToCompress * CompressedBlocks)

                Else ' Only use the total ore volume
                    CompressedBlockVolume = 0
                End If

                TotalOreVolume = TotalOreVolume + (CompressedBlockVolume * CompressedBlocks) + (OreVolume * OreUnits)

            Next

        End If

        Return TotalOreVolume

    End Function

    Private Sub SaveSelectedOres(Belt As BeltType)
        Dim TempSettings As IndustryBeltOreChecks = Nothing
        Dim Settings As New ProgramSettings
        Dim SavedSettings As New IndustryBeltOreChecks
        Dim OreList As New ListView

        Select Case Belt
            Case BeltType.Small
                OreList = lstOresLevel1
            Case BeltType.Medium
                OreList = lstOresLevel2
            Case BeltType.Large
                OreList = lstOresLevel3
            Case BeltType.Enormous
                OreList = lstOresLevel4
            Case BeltType.Colossal
                OreList = lstOresLevel5
        End Select

        ' Reset them all to default settings first if not found
        TempSettings = AllSettings.SetDefaultIndustryBeltOreChecksSettings((Belt))

        With TempSettings
            ' Loop through the ore list and save the value
            For i = 0 To OreList.Items.Count - 1
                Select Case OreList.Items(i).SubItems(1).Text
                    Case "Plagioclase"
                        .Plagioclase = OreList.Items(i).Checked
                    Case "Spodumain"
                        .Spodumain = OreList.Items(i).Checked
                    Case "Kernite"
                        .Kernite = OreList.Items(i).Checked
                    Case "Hedbergite"
                        .Hedbergite = OreList.Items(i).Checked
                    Case "Arkonor"
                        .Arkonor = OreList.Items(i).Checked
                    Case "Bistot"
                        .Bistot = OreList.Items(i).Checked
                    Case "Pyroxeres"
                        .Pyroxeres = OreList.Items(i).Checked
                    Case "Crokite"
                        .Crokite = OreList.Items(i).Checked
                    Case "Jaspet"
                        .Jaspet = OreList.Items(i).Checked
                    Case "Omber"
                        .Omber = OreList.Items(i).Checked
                    Case "Scordite"
                        .Scordite = OreList.Items(i).Checked
                    Case "Gneiss"
                        .Gneiss = OreList.Items(i).Checked
                    Case "Veldspar"
                        .Veldspar = OreList.Items(i).Checked
                    Case "Hemorphite"
                        .Hemorphite = OreList.Items(i).Checked
                    Case "Dark Ochre"
                        .DarkOchre = OreList.Items(i).Checked
                    Case "Mercoxit"
                        .Mercoxit = OreList.Items(i).Checked
                    Case "Crimson Arkonor"
                        .CrimsonArkonor = OreList.Items(i).Checked
                    Case "Prime Arkonor"
                        .PrimeArkonor = OreList.Items(i).Checked
                    Case "Triclinic Bistot"
                        .TriclinicBistot = OreList.Items(i).Checked
                    Case "Monoclinic Bistot"
                        .MonoclinicBistot = OreList.Items(i).Checked
                    Case "Sharp Crokite"
                        .SharpCrokite = OreList.Items(i).Checked
                    Case "Crystalline Crokite"
                        .CrystallineCrokite = OreList.Items(i).Checked
                    Case "Onyx Ochre"
                        .OnyxOchre = OreList.Items(i).Checked
                    Case "Obsidian Ochre"
                        .ObsidianOchre = OreList.Items(i).Checked
                    Case "Vitric Hedbergite"
                        .VitricHedbergite = OreList.Items(i).Checked
                    Case "Glazed Hedbergite"
                        .GlazedHedbergite = OreList.Items(i).Checked
                    Case "Vivid Hemorphite"
                        .VividHemorphite = OreList.Items(i).Checked
                    Case "Radiant Hemorphite"
                        .RadiantHemorphite = OreList.Items(i).Checked
                    Case "Pure Jaspet"
                        .PureJaspet = OreList.Items(i).Checked
                    Case "Pristine Jaspet"
                        .PristineJaspet = OreList.Items(i).Checked
                    Case "Luminous Kernite"
                        .LuminousKernite = OreList.Items(i).Checked
                    Case "Fiery Kernite"
                        .FieryKernite = OreList.Items(i).Checked
                    Case "Azure Plagioclase"
                        .AzurePlagioclase = OreList.Items(i).Checked
                    Case "Rich Plagioclase"
                        .RichPlagioclase = OreList.Items(i).Checked
                    Case "Solid Pyroxeres"
                        .SolidPyroxeres = OreList.Items(i).Checked
                    Case "Viscous Pyroxeres"
                        .ViscousPyroxeres = OreList.Items(i).Checked
                    Case "Condensed Scordite"
                        .CondensedScordite = OreList.Items(i).Checked
                    Case "Massive Scordite"
                        .MassiveScordite = OreList.Items(i).Checked
                    Case "Bright Spodumain"
                        .BrightSpodumain = OreList.Items(i).Checked
                    Case "Gleaming Spodumain"
                        .GleamingSpodumain = OreList.Items(i).Checked
                    Case "Concentrated Veldspar"
                        .ConcentratedVeldspar = OreList.Items(i).Checked
                    Case "Dense Veldspar"
                        .DenseVeldspar = OreList.Items(i).Checked
                    Case "Iridescent Gneiss"
                        .IridescentGneiss = OreList.Items(i).Checked
                    Case "Prismatic Gneiss"
                        .PrismaticGneiss = OreList.Items(i).Checked
                    Case "Silvery Omber"
                        .SilveryOmber = OreList.Items(i).Checked
                    Case "Golden Omber"
                        .GoldenOmber = OreList.Items(i).Checked
                    Case "Magma Mercoxit"
                        .MagmaMercoxit = OreList.Items(i).Checked
                    Case "Vitreous Mercoxit"
                        .VitreousMercoxit = OreList.Items(i).Checked
                End Select
            Next
        End With

        ' Save the data in the XML file
        Settings.SaveIndustryBeltOreChecksSettings(TempSettings, Belt)

        ' Save them locally
        Select Case Belt
            Case BeltType.Small
                UserIndustryFlipBeltOreCheckSettings1 = TempSettings
            Case BeltType.Medium
                UserIndustryFlipBeltOreCheckSettings2 = TempSettings
            Case BeltType.Large
                UserIndustryFlipBeltOreCheckSettings3 = TempSettings
            Case BeltType.Enormous
                UserIndustryFlipBeltOreCheckSettings4 = TempSettings
            Case BeltType.Colossal
                UserIndustryFlipBeltOreCheckSettings5 = TempSettings
        End Select

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

#Region "Event Functions"

    Private Sub cmbNumMiners_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbNumMiners.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab And Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub cmbNumMiners_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbNumMiners.SelectedIndexChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub cmbNumMiners_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNumMiners.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedRunschars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtCycleTime_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCycleTime.KeyDown
        Call ProcessCutCopyPasteSelect(txtCycleTime, e)
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab And Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub txtCycleTime_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCycleTime.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtm3perCycle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtm3perCycle.KeyDown
        Call ProcessCutCopyPasteSelect(txtm3perCycle, e)
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab And Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub txtm3perCycle_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtm3perCycle.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtMineRefineStanding_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMineRefineStanding.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPriceChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtMineRefineStanding_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtMineRefineStanding.KeyDown
        Call ProcessCutCopyPasteSelect(txtMineRefineStanding, e)
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab And Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub cmbMineRefineStationTax_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbRefineStationTax.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cmbMineRefineStationTax_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbRefineStationTax.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab And Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub cmbMineStationEff_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMineStationEff.SelectedIndexChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub chkCompressOre_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCompressOre.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub rbtn0percent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtn0percent.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub rbtn5percent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtn5percent.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub rbtn10percent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtn10percent.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub chkIPHperMiner_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkIPHperMiner.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub chkMineIncludeBrokerFees_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMineIncludeBrokerFees.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub chkMineIncludeTaxes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMineIncludeTaxes.CheckedChanged
        If Not FirstLoad Then
            Call LoadAllTables()
        End If
    End Sub

    Private Sub btnSaveSettingsSmall_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettingsSmall.Click
        Call SaveSelectedOres(BeltType.Small)
    End Sub

    Private Sub btnSaveSettingsMedium_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettingsMedium.Click
        Call SaveSelectedOres(BeltType.Medium)
    End Sub

    Private Sub btnSaveSettingsLarge_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettingsLarge.Click
        Call SaveSelectedOres(BeltType.Large)
    End Sub

    Private Sub btnSaveSettingsXLLarge_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettingsXLLarge.Click
        Call SaveSelectedOres(BeltType.Enormous)
    End Sub

    Private Sub btnSaveSettingsGiant_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettingsGiant.Click
        Call SaveSelectedOres(BeltType.Colossal)
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub lstOresLevel1_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstOresLevel1.ColumnClick
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Ore1ColumnSorter)

        ' Perform the sort with these new sort options.
        lstOresLevel1.Sort()
    End Sub

    Private Sub lstMineralsLevel1_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstMineralsLevel1.ColumnClick
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Mineral1ColumnSorter)

        ' Perform the sort with these new sort options.
        lstMineralsLevel1.Sort()
    End Sub

    Private Sub lstOresLevel1_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstOresLevel1.ItemChecked
        Dim s As ListView = CType(sender, ListView)

        If Not FirstLoad And CType(s.ContainsFocus, Boolean) Then
            Call DisplayBeltMinerals(BeltType.Small)
        End If
    End Sub

    Private Sub lstOresLevel2_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Ore2ColumnSorter)

        ' Perform the sort with these new sort options.
        lstOresLevel2.Sort()
    End Sub

    Private Sub lstMineralsLevel2_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Mineral2ColumnSorter)

        ' Perform the sort with these new sort options.
        lstMineralsLevel2.Sort()
    End Sub

    Private Sub lstOresLevel2_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstOresLevel2.ItemChecked
        Dim s As ListView = CType(sender, ListView)

        If Not FirstLoad And CType(s.ContainsFocus, Boolean) Then
            Call DisplayBeltMinerals(BeltType.Medium)
        End If
    End Sub

    Private Sub lstOresLevel3_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Ore3ColumnSorter)

        ' Perform the sort with these new sort options.
        lstOresLevel3.Sort()
    End Sub

    Private Sub lstMineralsLevel3_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Mineral3ColumnSorter)

        ' Perform the sort with these new sort options.
        lstMineralsLevel3.Sort()
    End Sub

    Private Sub lstOresLevel3_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstOresLevel3.ItemChecked
        Dim s As ListView = CType(sender, ListView)

        If Not FirstLoad And CType(s.ContainsFocus, Boolean) Then
            Call DisplayBeltMinerals(BeltType.Large)
        End If
    End Sub

    Private Sub lstOresLevel4_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Ore4ColumnSorter)

        ' Perform the sort with these new sort options.
        lstOresLevel4.Sort()
    End Sub

    Private Sub lstMineralsLevel4_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Mineral4ColumnSorter)

        ' Perform the sort with these new sort options.
        lstMineralsLevel4.Sort()
    End Sub

    Private Sub lstOresLevel4_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstOresLevel4.ItemChecked
        Dim s As ListView = CType(sender, ListView)

        If Not FirstLoad And CType(s.ContainsFocus, Boolean) Then
            Call DisplayBeltMinerals(BeltType.Enormous)
        End If
    End Sub

    Private Sub lstOresLevel5_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Ore5ColumnSorter)

        ' Perform the sort with these new sort options.
        lstOresLevel5.Sort()
    End Sub

    Private Sub lstMineralsLevel5_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)
        ' Set the sort order options
        Call SetLstVwColumnSortOrder(e, Mineral5ColumnSorter)

        ' Perform the sort with these new sort options.
        lstMineralsLevel5.Sort()
    End Sub

    Private Sub lstOresLevel5_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstOresLevel5.ItemChecked
        Dim s As ListView = CType(sender, ListView)

        If Not FirstLoad And CType(s.ContainsFocus, Boolean) Then
            Call DisplayBeltMinerals(BeltType.Colossal)
        End If
    End Sub

    Private Sub btnCloseGiant_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseGiant.Click
        Me.Hide()
    End Sub

    Private Sub btnCloseSmall_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseSmall.Click
        Me.Hide()
    End Sub

    Private Sub btnCloseMedium_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseMedium.Click
        Me.Hide()
    End Sub

    Private Sub btnCloseLarge_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseLarge.Click
        Me.Hide()
    End Sub

    Private Sub btnCloseXL_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseXL.Click
        Me.Hide()
    End Sub

#End Region

End Class