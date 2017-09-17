Imports System.Data.SQLite

Public Class ManufacturingFacility

    Private EVEDBRef As DBConnection
    Private EVEDBCommandRef As SQLiteCommand

    Private MY_SelectedView As FacilityView
    Private MY_SelectedProductionType As ProductionType
    Private MY_SelectedFacility As IndustryFacility ' This is the active facility for the control, if not loaded will use the default

    Private MY_BPTech As Integer
    Private MY_BPHasComponents As Boolean
    Private MY_SelectedBPGroupID As Integer
    Private MY_SelectedBPCategoryID As Integer

    Private MY_CurrentIndyType As ProductionType
    Private MY_CurrentBPGroupID As Integer
    Private MY_CurrentBPCategoryID As Integer

    ' Use these to determine the facility types for these cases - TODO put these here and in manufacturing facility
    ' These are all the capital ships that use capital parts
    Private Const MY_CapitalIndustrialShipGroupID As Integer = 883
    Private Const MY_CarrierGroupID As Integer = 547
    Private Const MY_DreadnoughtGroupID As Integer = 485
    Private Const MY_FreighterGroupID As Integer = 513
    Private Const MY_IndustrialCommandShipGroupID As Integer = 941
    Private Const MY_JumpFreighterGroupID As Integer = 902
    Private Const MY_SupercarrierGroupID As Integer = 659
    Private Const MY_FAXGroupID As Integer = 1538
    Private Const MY_TitanGroupID As Integer = 30
    Private Const MY_BoosterGroupID As Integer = 303

    Private Const MY_StrategicCruiserGroupID As Integer = 963
    Private Const MY_TacticalDestroyerGroupID As Integer = 1305
    Private Const MY_SubsystemCategoryID As Integer = 32

    ' For looking up pos stuff in facilities
    Private Const MY_FuelBlockGroupID As Integer = 1136
    Private Const MY_BattleshipGroupID As Integer = 27
    Private Const MY_ModuleCategoryID As Integer = 7

    Private Const MY_ShipCategoryID As Integer = 6 ' for loading invention and copying 

    ' T3 Bps for facility updates
    Private Const MY_StrategicCruiserBPGroupID As Integer = 996
    Private Const MY_TacticalDestroyerBPGroupID As Integer = 1309
    Private Const MY_SubsystemBPGroupID As Integer = 973

    Private Const MY_ConstructionComponentsGroupID As Integer = 334 ' Use this for all non-capital components
    Private Const MY_ComponentCategoryID As Integer = 17
    Private Const MY_CapitalComponentGroupID As Integer = 873
    Private Const MY_AdvCapitalComponentGroupID As Integer = 913

    ' Categories (has multiple groups)
    Private Const MY_StationEggGroupID As Integer = 307 ' This is for loading No POS build items
    Private Const MY_SovStructureCategoryID As Integer = 3 ' For stations - I don't think this is used anymore (everything can be built at a pos?)
    Private Const MY_StationPartsGroupID As Integer = 536

    ' Constant activities
    Private Const MY_ActivityManufacturing As String = "Manufacturing"
    Private Const MY_ActivityComponentManufacturing As String = "Component Manufacturing"
    Private Const MY_ActivityCapComponentManufacturing As String = "Cap Component Manufacturing"
    Private Const MY_ActivityCopying As String = "Copying"
    Private Const MY_ActivityInvention As String = "Invention"

    Private Const MY_POSFacility As String = "POS"
    Private Const MY_StationFacility As String = "Station"
    Private Const MY_OutpostFacility As String = "Outpost"
    Private Const MY_CitadelFacility As String = "Citadel"

    ' To check if we are loading and stop click events when changing values
    Private MY_LoadingActivities As Boolean
    Private MY_LoadingTypes As Boolean
    Private MY_LoadingRegions As Boolean
    Private MY_LoadingSystems As Boolean
    Private MY_LoadingFacilities As Boolean
    Private MY_ChangingUsageChecks As Boolean

    ' To save previous values for checking and loading
    Private MY_CurrentProductionType As ProductionType
    Private MY_PreviousProductionType As ProductionType
    Private MY_PreviousType As String
    Private MY_PreviousRegion As String
    Private MY_PreviousSystem As String
    Private MY_PreviousEquipment As String
    Private MY_PreviousActivity As String

    Private MY_FullyLoadedFacility As Boolean
    Private MY_tt As ToolTip
    Private MY_FirstLoad As Boolean

    Private NoPOSCategoryIDs As New List(Of Long) ' For facilities

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        MY_FirstLoad = True

        ' Add any initialization after the InitializeComponent() call.

        ' Hide everything until constructed with the options sent
        cmbFacilityActivities.Visible = False
        lblFacilityActivity.Visible = False
        lblFacilityUsage.Visible = False
        chkFacilityToggle.Visible = False
        lblFWUpgrade.Visible = False
        cmbFWUpgrade.Visible = False
        txtFacilityManualCost.Visible = False
        lblFacilityManualCost.Visible = False
        btnFacilityFitting.Visible = False
        txtFacilityManualTax.Visible = False
        lblFacilityManualTax.Visible = False
        btnFacilitySave.Visible = False
        txtFacilityManualTE.Visible = False
        txtFacilityManualME.Visible = False
        lblFacilityManualTE.Visible = False
        lblFacilityManualME.Visible = False
        lblInclude.Visible = False
        chkFacilityIncludeUsage.Visible = False
        chkFacilityIncludeTime.Visible = False
        lblFacilityDefault.Visible = False
        chkFacilityIncludeCost.Visible = False
        cmbFacilityorArray.Visible = False
        cmbFacilitySystem.Visible = False
        cmbFacilityRegion.Visible = False
        lblFacilityLocation.Visible = False
        lblFacilityType.Visible = False
        cmbFacilityType.Visible = False
        cmbLargeShips.Visible = False
        lblLargeShips.Visible = False
        cmbFuelBlocks.Visible = False
        lblFuelBlocks.Visible = False
        cmbModules.Visible = False
        lblModules.Visible = False

        FirstLoad = False

        MY_SelectedFacility = New IndustryFacility

    End Sub

    ''' <summary>
    ''' Before any controls are shown, the control needs to be initilaized by sending the view type.
    ''' </summary>
    ''' <param name="ViewType"></param>
    Public Sub InitializeControl(ViewType As FacilityView, InitialProductionType As ProductionType)
        Const SolarSystemWidthBP As Integer = 142
        'Const SolarSystemWidthCalc As Integer = 156

        Const FacilityArrayWidthBP As Integer = 274
        'Const FacilityArrayWidthCalc As Integer = 291

        Const LeftObjectLocation As Integer = 3
        Const LeftLabelLocation As Integer = 1

        Const DefaultLabelWidthBP As Integer = 48
        Const DefaultLabelHeightBP As Integer = 34

        ' Load the NoPOS id's first
        Call SetNoPOSGroupIDs()

        ' Save for later
        MY_SelectedView = ViewType
        MY_SelectedProductionType = InitialProductionType

        EVEDBRef = EVEDB
        EVEDBCommandRef = DBCommand

        ' Move and show the selected controls depending on the view sent
        Select Case ViewType
            Case FacilityView.FullControls

                lblFacilityActivity.Top = 2
                lblFacilityActivity.Left = LeftLabelLocation
                lblFacilityActivity.Visible = True

                cmbFacilityActivities.Top = lblFacilityActivity.Top + lblFacilityActivity.Height + 1
                cmbFacilityActivities.Left = LeftObjectLocation
                cmbFacilityActivities.Visible = True

                lblFacilityType.Top = 1
                lblFacilityType.Left = cmbFacilityActivities.Left + cmbFacilityActivities.Width
                lblFacilityType.Visible = True

                cmbFacilityType.Top = cmbFacilityActivities.Top
                cmbFacilityType.Left = cmbFacilityActivities.Left + cmbFacilityActivities.Width + 2
                cmbFacilityType.Visible = True

                lblFacilityDefault.Height = DefaultLabelHeightBP
                lblFacilityDefault.Width = DefaultLabelWidthBP
                Call lblFacilityDefault.SendToBack()
                lblFacilityDefault.Top = lblFacilityType.Top + 1
                lblFacilityDefault.Left = cmbFacilityType.Left + cmbFacilityType.Width - 2
                lblFacilityDefault.Visible = True

                lblFacilityLocation.Top = cmbFacilityActivities.Top + cmbFacilityActivities.Height + 3
                lblFacilityLocation.Left = LeftLabelLocation
                lblFacilityLocation.Visible = True

                chkFacilityIncludeUsage.Top = lblFacilityLocation.Top - 1
                chkFacilityIncludeUsage.Left = lblFacilityLocation.Left + lblFacilityLocation.Width + 27
                chkFacilityIncludeUsage.Text = "Usage:"
                chkFacilityIncludeUsage.Visible = True

                lblFacilityUsage.Top = chkFacilityIncludeUsage.Top - 1
                lblFacilityUsage.Left = chkFacilityIncludeUsage.Left + chkFacilityIncludeUsage.Width - 4
                lblFacilityUsage.Width = SolarSystemWidthBP
                lblFacilityUsage.Visible = True

                cmbFacilityRegion.Top = lblFacilityLocation.Top + lblFacilityLocation.Height + 3
                cmbFacilityRegion.Left = LeftObjectLocation
                cmbFacilityRegion.Visible = True

                cmbFacilitySystem.Top = cmbFacilityRegion.Top
                cmbFacilitySystem.Left = cmbFacilityRegion.Left + cmbFacilityRegion.Width + 2
                cmbFacilitySystem.Width = SolarSystemWidthBP
                cmbFacilitySystem.Visible = True

                cmbFacilityorArray.Top = cmbFacilityRegion.Top + cmbFacilityRegion.Height + 1
                cmbFacilityorArray.Left = LeftObjectLocation
                cmbFacilityorArray.Width = FacilityArrayWidthBP
                cmbFacilityorArray.Visible = True

                lblFacilityManualME.Top = cmbFacilityorArray.Top + cmbFacilityorArray.Height + 4
                lblFacilityManualME.Left = LeftLabelLocation + 10
                lblFacilityManualME.Text = "ME:"
                lblFacilityManualME.Visible = True

                txtFacilityManualME.Top = cmbFacilityorArray.Top + cmbFacilityorArray.Height + 1
                txtFacilityManualME.Left = lblFacilityManualME.Left + lblFacilityManualME.Width
                txtFacilityManualME.Visible = True

                lblFacilityManualCost.Top = lblFacilityManualME.Top + lblFacilityManualME.Height + 7
                lblFacilityManualCost.Left = LeftLabelLocation + 10
                lblFacilityManualCost.Text = "Cost:"
                lblFacilityManualCost.Visible = True

                txtFacilityManualCost.Top = txtFacilityManualME.Top + txtFacilityManualME.Height + 1
                txtFacilityManualCost.Left = lblFacilityManualCost.Left + lblFacilityManualCost.Width
                txtFacilityManualCost.Visible = True

                ' Reset manual ME so it aligns with cost box
                txtFacilityManualME.Left = txtFacilityManualCost.Left

                lblFacilityManualTE.Top = lblFacilityManualME.Top
                lblFacilityManualTE.Left = txtFacilityManualME.Left + txtFacilityManualME.Width + 10
                lblFacilityManualTE.Text = "TE:"
                lblFacilityManualTE.Visible = True

                txtFacilityManualTE.Top = txtFacilityManualME.Top
                txtFacilityManualTE.Left = lblFacilityManualTE.Left + lblFacilityManualTE.Width
                txtFacilityManualTE.Visible = True

                lblFacilityManualTax.Top = lblFacilityManualCost.Top
                lblFacilityManualTax.Left = txtFacilityManualCost.Left + txtFacilityManualCost.Width + 10
                lblFacilityManualTax.Text = "Tax:"
                lblFacilityManualTax.Visible = True

                txtFacilityManualTax.Top = txtFacilityManualCost.Top
                txtFacilityManualTax.Left = lblFacilityManualTax.Left + lblFacilityManualTax.Width
                txtFacilityManualTax.Visible = True

                txtFacilityManualTE.Left = txtFacilityManualTax.Left

                btnFacilityFitting.Top = txtFacilityManualME.Top + CInt(txtFacilityManualME.Height / 2)
                btnFacilityFitting.Left = txtFacilityManualTE.Left + txtFacilityManualTE.Width + 20
                btnFacilityFitting.Visible = True

                btnFacilitySave.Top = btnFacilityFitting.Top
                btnFacilitySave.Left = btnFacilityFitting.Left + btnFacilityFitting.Width + 2
                btnFacilitySave.Visible = True

                ' Load all the facilities for BP tab
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.Manufacturing, ViewType)

            Case FacilityView.LimitedControls

                ' Select what facility to load based on the industry type
                Select Case InitialProductionType
                    Case ProductionType.None
                    Case ProductionType.Manufacturing
                    Case ProductionType.ComponentManufacturing
                    Case ProductionType.CapitalComponentManufacturing
                    Case ProductionType.CapitalManufacturing
                    Case ProductionType.SuperManufacturing
                    Case ProductionType.T3CruiserManufacturing
                    Case ProductionType.SubsystemManufacturing
                    Case ProductionType.BoosterManufacturing
                    Case ProductionType.Copying
                    Case ProductionType.Invention
                    Case ProductionType.NoPOSManufacturing
                    Case ProductionType.T3Invention
                    Case ProductionType.T3DestroyerManufacturing
                    Case Else

                End Select

            Case Else
                ' Leave, no valid option sent
                Exit Sub
        End Select

    End Sub

    ' Loads the facility activity combo - checks group and category ID's if it has components to set component activities
    Public Sub MY_LoadFacilityActivities(BlueprintTech As Integer, HasComponents As Boolean, BPGroupID As Long, BPCategoryID As Long)

        MY_LoadingActivities = True
        cmbFacilityActivities.BeginUpdate()

        Select Case BlueprintTech
            Case BlueprintTechLevel.T1
                ' Just manufacturing (add components later if there are any)
                cmbFacilityActivities.Items.Clear()
                cmbFacilityActivities.Items.Add(ActivityManufacturing)

            Case BlueprintTechLevel.T2
                ' Add only T2 activities to equipment
                cmbFacilityActivities.Items.Clear()
                cmbFacilityActivities.Items.Add(ActivityManufacturing)
                cmbFacilityActivities.Items.Add(ActivityCopying)
                cmbFacilityActivities.Items.Add(ActivityInvention)

            Case BlueprintTechLevel.T3
                ' Add only T3 activities to eqipment
                cmbFacilityActivities.Items.Clear()
                cmbFacilityActivities.Items.Add(ActivityManufacturing)
                cmbFacilityActivities.Items.Add(ActivityInvention)

        End Select

        ' Add components as a manufacturing facility option if this bp has any
        If HasComponents Then
            Select Case BPGroupID
                Case MY_TitanGroupID, MY_DreadnoughtGroupID, MY_CarrierGroupID, MY_SupercarrierGroupID, MY_CapitalIndustrialShipGroupID,
                        MY_IndustrialCommandShipGroupID, MY_FreighterGroupID, MY_JumpFreighterGroupID, MY_FAXGroupID

                    cmbFacilityActivities.Items.Add(ActivityCapComponentManufacturing)
                    If BPGroupID = MY_JumpFreighterGroupID Then
                        ' Need to add both cap and components
                        cmbFacilityActivities.Items.Add(ActivityComponentManufacturing)
                    End If
                Case Else
                    ' Just regular
                    cmbFacilityActivities.Items.Add(ActivityComponentManufacturing)
            End Select
        End If

        MY_LoadingActivities = False
        cmbFacilityActivities.EndUpdate()

    End Sub

    ' Loads the class facility and objects
    Public Sub MY_LoadFacility(ByVal BlueprintTech As Integer, ByVal BPHasComponents As Boolean, ByVal ItemGroupID As Integer,
                               ByVal ItemCategoryID As Integer)

        ' Save these variables if they change for the class
        MY_BPTech = BlueprintTech
        MY_BPHasComponents = BPHasComponents
        MY_CurrentBPCategoryID = ItemCategoryID
        MY_CurrentBPGroupID = ItemGroupID

        If MY_SelectedView = FacilityView.FullControls Then
            Call MY_LoadFacilityActivities(MY_BPTech, MY_BPHasComponents, MY_CurrentBPGroupID, MY_CurrentBPCategoryID)
            cmbFacilityActivities.Text = MY_ActivityManufacturing
        End If

        ' Activity combo is loaded so set the activity Text
        MY_LoadingActivities = True

        MY_PreviousProductionType = MY_SelectedProductionType
        MY_PreviousActivity = MY_ActivityManufacturing
        MY_LoadingActivities = False

        '' Facility Type combo
        '' Load the combo if they want to change
        'Call LoadFacilityTypeCombo(ProductionType, FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo,
        '                           FacilityCombo, FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualMETextBox, FacilityManualTaxTextBox,
        '                           FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
        '                           FacilitySaveButton, TAB, FacilityUsageLabel, FacilityUsageCheck, ManualSystemIndexGroupBox)

        '' Enable the type of facility and set
        'LoadingFacilityTypes = True
        'FacilityTypeCombo.Enabled = True
        'FacilityTypeCombo.Text = SelectedFacility.FacilityType
        'LoadingFacilityTypes = False

        'If SelectedFacility.FacilityType = None Then
        '    ' Just hide the boxes and exit
        '    Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
        '                               FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, False, FacilityUsageLabel)
        '    Call SetNoFacility(FacilityRegionCombo, FacilitySystemCombo, FacilityCombo, FacilityUsageCheck,
        '                       FacilityActivityCostCheck, FacilityActivityTimeCheck, FacilityIncludeLabel)
        '    FacilityLoaded = True ' Even with none, it's loaded
        '    Exit Sub
        'End If

        '' Region name Combo
        'LoadingFacilityRegions = True
        'FacilityRegionCombo.Enabled = True
        'FacilityRegionCombo.Text = SelectedFacility.RegionName
        'LoadingFacilityRegions = False

        '' Systems combo
        'LoadingFacilitySystems = True
        'FacilitySystemCombo.Enabled = True
        'FacilitySystemCombo.Text = SelectedFacility.SolarSystemName
        'LoadingFacilitySystems = False

        '' Facility/Array combo
        'LoadingFacilities = True
        'FacilityCombo.Enabled = True
        'Dim AutoLoad As Boolean = False
        ''If it's a pos, need to auto-load the facility for that item selected
        'If FacilityTypeCombo.Text = POSFacility And TAB() = BPTab Then
        '    Call LoadFacilities(ItemGroupID, ItemCategoryID, False,
        '                        FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
        '                        FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
        '                        FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
        '                        FacilitySaveButton, TAB, FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, AutoLoad,
        '                        SelectedFacility.IncludeActivityUsage, SelectedFacility.FacilityName, FacilityUsageLabel, ManualSystemIndexGroupBox, ToolTipRef, FWUpgradeLabel, FWUpgradeCombo)
        'ElseIf TAB() = CalcTab Then
        '    ' Load all facilities for each calc tab facility
        '    Call LoadFacilities(ItemGroupID, ItemCategoryID, False,
        '            FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
        '            FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
        '            FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, FacilitySaveButton, TAB,
        '            FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, AutoLoad,
        '            SelectedFacility.IncludeActivityUsage, SelectedFacility.FacilityName, FacilityUsageLabel, ManualSystemIndexGroupBox, ToolTipRef)
        'End If
        'LoadingFacilities = False

        '' Usage checks
        'ChangingUsageChecks = True
        'FacilityUsageCheck.Checked = SelectedFacility.IncludeActivityUsage

        'If Not IsNothing(FacilityActivityCostCheck) Then
        '    FacilityActivityCostCheck.Checked = SelectedFacility.IncludeActivityCost
        'End If

        'If Not IsNothing(FacilityActivityTimeCheck) Then
        '    FacilityActivityTimeCheck.Checked = SelectedFacility.IncludeActivityTime
        'End If
        'ChangingUsageChecks = False

        '' Finally show the results and save the facility locally
        'If Not AutoLoad Then
        '    LoadingFacilities = True
        '    FacilityCombo.Text = SelectedFacility.FacilityName
        '    Call DisplayFacilityBonus(SelectedFacility.ProductionType, SelectedFacility.MaterialMultiplier, SelectedFacility.TimeMultiplier, SelectedFacility.TaxRate,
        '                              ItemGroupID, ItemCategoryID,
        '                              FacilityActivity, FacilityTypeCombo.Text, FacilityCombo.Text,
        '                              FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
        '                              FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
        '                              FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
        '                              FacilitySaveButton, FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, TAB, FacilityLoaded,
        '                              SelectedFacility.IncludeActivityUsage, ToolTipRef, GetFWUpgradeLevel(FWUpgradeCombo, FacilitySystemCombo.Text))
        '    LoadingFacilities = False
        'End If

        '' If this is the BP tab, then refresh the BP prices
        'If Not FirstLoad And SetTaxFeeChecks And TAB() = BPTab And RefreshBP Then
        '    If Not IsNothing(SelectedBlueprint) Then
        '        Call SelectedBlueprint.SetPriceData(frmMain.chkBPTaxes.Checked, frmMain.chkBPBrokerFees.Checked)
        '        Call frmMain.UpdateBPPriceLabels()
        '    End If
        'End If

        'Call ResetComboLoadVariables(TAB, ProductionType, False, False, False, True, ManualSystemIndexGroupBox)

        '' Set here in case we load the bonuses
        'Call SetFWUpgradeControls(TAB, FWUpgradeLabel, FWUpgradeCombo, SelectedFacility.SolarSystemName, FWUpgradeButton)

        '' All facilities loaded
        'FacilityLoaded = True

        'If TAB() = CalcTab And Not FirstLoad Then
        '    Call frmMain.ResetRefresh()
        'End If

    End Sub

    Private Sub cmbFacilityActivities_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityActivities.SelectedIndexChanged

        If Not MY_LoadingActivities And Not FirstLoad Then
            MY_CurrentProductionType = MY_GetProductionType(cmbFacilityActivities.Text, MY_CurrentBPGroupID, MY_CurrentBPCategoryID, cmbFacilityType.Text)

            ' If they switch the activity and it changed from the previous, then load the selected facility for this activity
            If MY_CurrentProductionType <> MY_PreviousProductionType Then

                Call MY_LoadFacility(MY_BPTech, MY_BPHasComponents, MY_CurrentBPGroupID, MY_CurrentBPCategoryID)

                MY_PreviousProductionType = MY_CurrentProductionType
                ' Reset all previous to current list, since all the combos should be loaded
                MY_PreviousType = cmbFacilityType.Text
                MY_PreviousEquipment = cmbFacilityorArray.Text
                MY_PreviousRegion = cmbFacilityRegion.Text
                MY_PreviousSystem = cmbFacilitySystem.Text
            End If

            Call cmbFacilityType.Focus()

        End If
    End Sub

    ' Loads the facility from table based on the industry type and facility view sent
    Private Function LoadFacility(ViewType As FacilityView, InitialProductionType As ProductionType) As IndustryFacility
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim ReturnFacility As New IndustryFacility

        '' When loading a POS Facility with multi-use or Component array, which allows 'All' then do special processing
        'With SearchFacilitySettings
        '        If .FacilityType = POSFacility And (.Facility = "All" Or .Facility = "Component" Or .Facility = "Large" _
        '                                            Or .Facility = "Equipment") Then
        '            ' Set the name to what was sent, save the sent info, and exit. This will only happen with the manufacturing tab section
        '            SQL = "SELECT '" & .Facility & "' AS FACILITY_NAME, "
        '            ' Need to load location from the settings since the location is specific to the user
        '            SQL = SQL & "'" & .RegionName & "' AS REGION_NAME, " & CStr(.RegionID) & " AS REGION_ID, '"
        '            SQL = SQL & .SolarSystemName & "' AS SOLAR_SYSTEM_NAME, " & CStr(.SolarSystemID) & " AS SSID, " & CStr(POSTaxRate) & " AS FACILITY_TAX, COST_INDEX, "
        '            SQL = SQL & "MATERIAL_MULTIPLIER AS MATERIAL_MULTIPLIER, TIME_MULTIPLIER AS TIME_MULTIPLIER, "
        '            SQL = SQL & "ASSEMBLY_ARRAYS.ACTIVITY_ID AS AID, ARRAY_TYPE_ID AS FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
        '            SQL = SQL & "FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID "
        '            SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
        '            SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
        '            SQL = SQL & "AND AID = " & CStr(.ActivityID) & " "

        '            If .Facility <> "All" Then
        '                ' Must be a fuel block, module, or large ship array choice
        '                Select Case .Facility
        '                    Case "Component"
        '                        SQL = SQL & "AND ARRAY_NAME LIKE 'Component%' AND GROUP_ID = " & FuelBlockGroupID
        '                    Case "Large"
        '                        SQL = SQL & "AND ARRAY_NAME LIKE 'Large%' AND GROUP_ID = " & BattleshipGroupID
        '                    Case "Equipment"
        '                        SQL = SQL & "AND ARRAY_NAME LIKE 'Equipment%' AND CATEGORY_ID = " & ModuleCategoryID
        '                End Select
        '            End If

        '            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        '            rsLoader = DBCommand.ExecuteReader
        '            rsLoader.Read()

        '            If rsLoader.HasRows Then
        '                FacilityType = .FacilityType
        '                FacilityName = rsLoader.GetString(0)
        '                ProductionType = .ProductionType
        '                RegionName = rsLoader.GetString(1)
        '                RegionID = rsLoader.GetInt64(2)
        '                SolarSystemName = rsLoader.GetString(3)
        '                SolarSystemID = rsLoader.GetInt64(4)
        '                TaxRate = rsLoader.GetDouble(5)
        '                CostIndex = rsLoader.GetFloat(6)
        '                MaterialMultiplier = rsLoader.GetDouble(7)
        '                TimeMultiplier = rsLoader.GetDouble(8)
        '                ActivityID = rsLoader.GetInt32(9)
        '                ActivityCostPerSecond = .ActivityCostperSecond
        '                IsDefault = FacilityDefault
        '                FacilityTypeID = rsLoader.GetInt64(10)

        '                IncludeActivityCost = .IncludeActivityCost
        '                IncludeActivityTime = .IncludeActivityTime
        '                IncludeActivityUsage = .IncludeActivityUsage

        '                FWUpgradeLevel = rsLoader.GetInt32(13)

        '                rsLoader.Close()
        '                rsLoader = Nothing
        '                DBCommand = Nothing

        '            End If

        '            ' We set this, now leave
        '            Exit Sub

        '        End If
        '    End With

        Dim USEDBData As Boolean
        Dim searchfacilitysettings As IndustryFacility = Nothing

        With SearchFacilitySettings
            Select Case .FacilityType
                Case POSFacility
                    SQL = "SELECT ARRAY_NAME AS FACILITY_NAME, "
                    ' Need to load location from the settings since the location is specific to the user
                    SQL = SQL & "'" & .RegionName & "' AS REGION_NAME, " & CStr(.RegionID) & " AS REGION_ID, '"
                    SQL = SQL & .SolarSystemName & "' AS SOLAR_SYSTEM_NAME, " & CStr(.SolarSystemID) & " AS SSID, " & CStr(POSTaxRate) & " AS FACILITY_TAX, COST_INDEX, "
                    SQL = SQL & "MATERIAL_MULTIPLIER AS MATERIAL_MULTIPLIER, TIME_MULTIPLIER AS TIME_MULTIPLIER, "
                    SQL = SQL & "ASSEMBLY_ARRAYS.ACTIVITY_ID AS AID, ARRAY_TYPE_ID AS FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
                    SQL = SQL & "FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID "
                    SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
                    SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "

                Case OutpostFacility
                    SQL = "SELECT FACILITY_NAME, REGION_NAME, REGION_ID, "
                    SQL = SQL & "SOLAR_SYSTEM_NAME, STATION_FACILITIES.SOLAR_SYSTEM_ID AS SSID, FACILITY_TAX, COST_INDEX, "
                    ' Check the values sent to see if they set it to something instead of loading from DB
                    If .MaterialMultiplier <> 1 And .MaterialMultiplier <> 0 Then ' Check for default or 0
                        ' They didn't set a value, so load the default for each type
                        SQL = SQL & CStr(.MaterialMultiplier) & " AS MATERIAL_MULTIPLIER, "
                        USEDBData = False
                    Else
                        SQL = SQL & "MATERIAL_MULTIPLIER, "
                    End If

                    If .TimeMultiplier <> 1 And .TimeMultiplier <> 0 Then ' Check for default or 0
                        ' They didn't set a value, so load the default for each type
                        SQL = SQL & CStr(.TimeMultiplier) & " AS TIME_MULTIPLIER, "
                        USEDBData = False
                    Else
                        SQL = SQL & "TIME_MULTIPLIER, "
                    End If
                    SQL = SQL & "ACTIVITY_ID AS AID, FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
                    SQL = SQL & "FROM STATION_FACILITIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = STATION_FACILITIES.SOLAR_SYSTEM_ID "
                    SQL = SQL & "WHERE OUTPOST = " & CStr(StationType.Outpost) & " "
                Case StationFacility
                    SQL = "SELECT FACILITY_NAME, REGION_NAME, REGION_ID, "
                    SQL = SQL & "SOLAR_SYSTEM_NAME, STATION_FACILITIES.SOLAR_SYSTEM_ID AS SSID, FACILITY_TAX, COST_INDEX, "
                    SQL = SQL & "MATERIAL_MULTIPLIER, TIME_MULTIPLIER, "
                    SQL = SQL & "ACTIVITY_ID AS AID, FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
                    SQL = SQL & "FROM STATION_FACILITIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = STATION_FACILITIES.SOLAR_SYSTEM_ID "
                    SQL = SQL & "WHERE OUTPOST = " & CStr(StationType.Station) & " "
            End Select

            SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString("") & "' "
            SQL = SQL & "AND AID = " & CStr(.ActivityID) & " "

            If .FacilityType = POSFacility Then
                Select Case .FacilityProductionType
                    Case ProductionType.CapitalManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(CapitalIndustrialShipGroupID) & ", " & CStr(CarrierGroupID) & ", " & CStr(DreadnoughtGroupID) & ", " & CStr(FAXGroupID) & ") "
                    Case ProductionType.SuperManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(TitanGroupID) & ", " & CStr(SupercarrierGroupID) & ") "
                    Case ProductionType.BoosterManufacturing
                        SQL = SQL & "AND GROUP_ID = " & BoosterGroupID & " "
                    Case ProductionType.T3CruiserManufacturing
                        SQL = SQL & "AND GROUP_ID = " & StrategicCruiserGroupID & " "
                    Case ProductionType.SubsystemManufacturing
                        SQL = SQL & "AND CATEGORY_ID = " & SubsystemCategoryID & " "
                    Case ProductionType.ComponentManufacturing
                        SQL = SQL & "AND GROUP_ID = " & ConstructionComponentsGroupID & " "
                    Case ProductionType.CapitalComponentManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(AdvCapitalComponentGroupID) & ", " & CStr(CapitalComponentGroupID) & ") "
                End Select
            Else ' Stations
                Select Case .FacilityProductionType
                    Case ProductionType.CapitalManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(CapitalIndustrialShipGroupID) & ", " & CStr(CarrierGroupID) & ", " & CStr(DreadnoughtGroupID) & ", " & CStr(FAXGroupID) & ") "
                    Case ProductionType.SuperManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(TitanGroupID) & ", " & CStr(SupercarrierGroupID) & ") "
                    Case ProductionType.BoosterManufacturing
                        SQL = SQL & "AND GROUP_ID = " & BoosterGroupID & " "
                    Case ProductionType.T3CruiserManufacturing
                        SQL = SQL & "AND GROUP_ID = " & StrategicCruiserGroupID & " "
                    Case ProductionType.SubsystemManufacturing
                        SQL = SQL & "AND CATEGORY_ID = " & SubsystemCategoryID & " "
                    Case ProductionType.ComponentManufacturing, ProductionType.CapitalComponentManufacturing
                        SQL = SQL & "AND CATEGORY_ID = " & ComponentCategoryID & " "
                End Select
            End If

            SQL = SQL & "GROUP BY FACILITY_NAME, REGION_NAME, REGION_ID, SOLAR_SYSTEM_NAME, SSID, FACILITY_TAX, "
            SQL = SQL & "COST_INDEX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, AID, FACILITY_TYPE_ID, FW_UPGRADE_LEVEL"

        End With

        ' Set the query
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader
        rsLoader.Read()

        'If rsLoader.HasRows Then
        '    FacilityType = SearchFacilitySettings.FacilityType
        '    FacilityName = rsLoader.GetString(0)
        '    ProductionType = SearchFacilitySettings.ProductionType
        '    RegionName = rsLoader.GetString(1)
        '    RegionID = rsLoader.GetInt64(2)
        '    CostIndex = rsLoader.GetFloat(6)
        '    SolarSystemName = rsLoader.GetString(3) & " (" & FormatNumber(CostIndex, 3) & ")"
        '    SolarSystemID = rsLoader.GetInt64(4)
        '    If SearchFacilitySettings.FacilityType = OutpostFacility And Not USEDBData Then
        '        MaterialMultiplier = SearchFacilitySettings.MaterialMultiplier
        '        TimeMultiplier = SearchFacilitySettings.TimeMultiplier
        '        TaxRate = SearchFacilitySettings.TaxRate
        '    Else
        '        MaterialMultiplier = rsLoader.GetDouble(7)
        '        TimeMultiplier = rsLoader.GetDouble(8)
        '        TaxRate = rsLoader.GetDouble(5)
        '    End If
        '    ActivityID = rsLoader.GetInt32(9)
        '    ActivityCostPerSecond = SearchFacilitySettings.ActivityCostperSecond
        '    IsDefault = FacilityDefault
        '    FacilityTypeID = rsLoader.GetInt64(10)

        '    IncludeActivityCost = SearchFacilitySettings.IncludeActivityCost
        '    IncludeActivityTime = SearchFacilitySettings.IncludeActivityTime
        '    IncludeActivityUsage = SearchFacilitySettings.IncludeActivityUsage

        '    FWUpgradeLevel = rsLoader.GetInt32(13)

        '    rsLoader.Close()
        '    rsLoader = Nothing
        '    DBCommand = Nothing

        'End If

        Return ReturnFacility

    End Function


    ' Returns the type of production done for the activity and bp data sent
    Private Function MY_GetProductionType(Activity As String, ItemGroupID As Long, ItemCategoryID As Long, FacilityType As String) As ProductionType
        Dim SelectedIndyType As ProductionType

        ' Determine if it's a fuel block, module, or big ship that can use a multi-use array in a POS - Need to add as a query, not hard code
        If FacilityType = MY_POSFacility And (ItemGroupID = 1136 _
                                           Or ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
                                           Or ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 _
                                           Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649
                                           ) And Activity = MY_ActivityManufacturing Then
            If ItemGroupID = 1136 Then
                SelectedIndyType = ProductionType.POSFuelBlockManufacturing
            ElseIf ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 Then
                SelectedIndyType = ProductionType.POSLargeShipManufacturing
            ElseIf ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
                Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649 Then
                SelectedIndyType = ProductionType.POSModuleManufacturing
            End If
        Else
            Select Case Activity
                ' TODO look into making these a lookup with the facility type if there are category or groupid's in the tables for them
                Case MY_ActivityManufacturing
                    ' Need to load selected manufacturing facility
                    Select Case ItemGroupID
                        Case MY_SupercarrierGroupID, MY_TitanGroupID
                            SelectedIndyType = ProductionType.SuperManufacturing
                        Case MY_BoosterGroupID
                            SelectedIndyType = ProductionType.BoosterManufacturing
                        Case MY_CarrierGroupID, MY_DreadnoughtGroupID, MY_CapitalIndustrialShipGroupID, MY_FAXGroupID
                            SelectedIndyType = ProductionType.CapitalManufacturing
                        Case MY_StrategicCruiserGroupID
                            SelectedIndyType = ProductionType.T3CruiserManufacturing
                        Case MY_TacticalDestroyerGroupID
                            SelectedIndyType = ProductionType.T3DestroyerManufacturing
                        Case Else
                            SelectedIndyType = ProductionType.Manufacturing

                            If ItemCategoryID = MY_SubsystemCategoryID Then
                                SelectedIndyType = ProductionType.SubsystemManufacturing
                            ElseIf ItemCategoryID = MY_ComponentCategoryID Then
                                ' Add category for component
                                If ItemGroupID = MY_CapitalComponentGroupID Or ItemGroupID = MY_AdvCapitalComponentGroupID Then
                                    SelectedIndyType = ProductionType.CapitalComponentManufacturing ' These all use cap components
                                Else
                                    SelectedIndyType = ProductionType.ComponentManufacturing
                                End If
                            ElseIf NoPOSCategoryIDs.Contains(ItemCategoryID) Or ItemGroupID = MY_StationEggGroupID Then
                                SelectedIndyType = ProductionType.NoPOSManufacturing
                            End If
                    End Select
                Case MY_ActivityComponentManufacturing
                    SelectedIndyType = ProductionType.ComponentManufacturing
                Case MY_ActivityCapComponentManufacturing
                    SelectedIndyType = ProductionType.CapitalComponentManufacturing
                Case MY_ActivityCopying
                    SelectedIndyType = ProductionType.Copying
                Case MY_ActivityInvention
                    If ItemCategoryID = MY_SubsystemCategoryID Or ItemGroupID = MY_StrategicCruiserGroupID Then
                        ' Need to invent this at a station or pos
                        SelectedIndyType = ProductionType.T3Invention
                    Else
                        SelectedIndyType = ProductionType.Invention
                    End If
            End Select
        End If

        Return SelectedIndyType

    End Function

    ' Sets the categories that must be made in a station/outpost
    Private Sub SetNoPOSGroupIDs()
        Dim SQL As String
        Dim rsCheck As SQLiteDataReader

        NoPOSCategoryIDs = New List(Of Long)

        SQL = "SELECT DISTINCT INVENTORY_CATEGORIES.categoryID "
        SQL = SQL & "FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_CATEGORIES "
        SQL = SQL & "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
        SQL = SQL & "AND INVENTORY_GROUPS.categoryID = INVENTORY_CATEGORIES.categoryID "
        SQL = SQL & "AND INVENTORY_CATEGORIES.categoryID NOT IN (SELECT CATEGORY_ID FROM ASSEMBLY_ARRAYS) "
        SQL = SQL & "AND INVENTORY_GROUPS.groupID NOT IN (SELECT GROUP_ID FROM ASSEMBLY_ARRAYS) "
        SQL = SQL & "AND INVENTORY_TYPES.typeID IN (SELECT ITEM_ID FROM ALL_BLUEPRINTS) "
        SQL = SQL & "AND INVENTORY_TYPES.typeID <> 33017"

        EVEDBCommandRef = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCheck = EVEDBCommandRef.ExecuteReader

        While rsCheck.Read
            NoPOSCategoryIDs.Add(rsCheck.GetInt64(0))
        End While

        On Error Resume Next
        SQL = ""
        rsCheck.Close()
        EVEDBCommandRef = Nothing
        On Error GoTo 0

    End Sub


    '#Region "Facilities"

    '    ' Returns the type of production done for the activity and bp data sent
    '    Public Function GetProductionType(Activity As String, ItemGroupID As Long, ItemCategoryID As Long, FacilityType As String) As IndustryType
    '        Dim SelectedIndyType As IndustryType

    '        ' Determine if it's a fuel block, module, or big ship that can use a multi-use array
    '        If FacilityType = POSFacility And (ItemGroupID = 1136 _
    '                                           Or ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
    '                                           Or ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 _
    '                                           Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649
    '                                           ) And Activity = ActivityManufacturing Then
    '            If ItemGroupID = 1136 Then
    '                SelectedIndyType = IndustryType.POSFuelBlockManufacturing
    '            ElseIf ItemGroupID = 27 Or ItemGroupID = 513 Or ItemGroupID = 941 Then
    '                SelectedIndyType = IndustryType.POSLargeShipManufacturing
    '            ElseIf ItemCategoryID = 7 Or ItemCategoryID = 20 Or ItemCategoryID = 22 Or ItemCategoryID = 23 _
    '                Or ItemGroupID = 12 Or ItemGroupID = 340 Or ItemGroupID = 448 Or ItemGroupID = 649 Then
    '                SelectedIndyType = IndustryType.POSModuleManufacturing
    '            End If
    '        Else
    '            Select Case Activity
    '                Case ActivityManufacturing
    '                    ' Need to load selected manufacturing facility
    '                    Select Case ItemGroupID
    '                        Case SupercarrierGroupID, TitanGroupID
    '                            SelectedIndyType = IndustryType.SuperManufacturing
    '                        Case BoosterGroupID
    '                            SelectedIndyType = IndustryType.BoosterManufacturing
    '                        Case CarrierGroupID, DreadnoughtGroupID, CapitalIndustrialShipGroupID, FAXGroupID
    '                            SelectedIndyType = IndustryType.CapitalManufacturing
    '                        Case StrategicCruiserGroupID
    '                            SelectedIndyType = IndustryType.T3CruiserManufacturing
    '                        Case TacticalDestroyerGroupID
    '                            SelectedIndyType = IndustryType.T3DestroyerManufacturing
    '                        Case Else
    '                            SelectedIndyType = IndustryType.Manufacturing

    '                            If ItemCategoryID = SubsystemCategoryID Then
    '                                SelectedIndyType = IndustryType.SubsystemManufacturing
    '                            ElseIf ItemCategoryID = ComponentCategoryID Then
    '                                ' Add category for component
    '                                If ItemGroupID = CapitalComponentGroupID Or ItemGroupID = AdvCapitalComponentGroupID Then
    '                                    SelectedIndyType = IndustryType.CapitalComponentManufacturing ' These all use cap components
    '                                Else
    '                                    SelectedIndyType = IndustryType.ComponentManufacturing
    '                                End If
    '                            ElseIf NoPOSCategoryIDs.Contains(ItemCategoryID) Or ItemGroupID = StationEggGroupID Then
    '                                SelectedIndyType = IndustryType.NoPOSManufacturing
    '                            End If
    '                    End Select
    '                Case ActivityComponentManufacturing
    '                    SelectedIndyType = IndustryType.ComponentManufacturing
    '                Case ActivityCapComponentManufacturing
    '                    SelectedIndyType = IndustryType.CapitalComponentManufacturing
    '                Case ActivityCopying
    '                    SelectedIndyType = IndustryType.Copying
    '                Case ActivityInvention
    '                    If ItemCategoryID = SubsystemCategoryID Or ItemGroupID = StrategicCruiserGroupID Then
    '                        ' Need to invent this at a station or pos
    '                        SelectedIndyType = IndustryType.T3Invention
    '                    Else
    '                        SelectedIndyType = IndustryType.Invention
    '                    End If
    '            End Select
    '        End If

    '        Return SelectedIndyType

    '    End Function

    '    ' Returns the facility with sent indytype of facilities
    '    Public Function GetManufacturingFacility(IndyType As IndustryType, Tab As String, Optional Clone As Boolean = True) As IndustryFacility
    '        Dim FacilityReference As IndustryFacility

    '        If Tab = BPTab Then
    '            Select Case IndyType
    '                Case IndustryType.Manufacturing
    '                    FacilityReference = SelectedBPManufacturingFacility
    '                Case IndustryType.BoosterManufacturing
    '                    FacilityReference = SelectedBPBoosterManufacturingFacility
    '                Case IndustryType.CapitalManufacturing
    '                    FacilityReference = SelectedBPCapitalManufacturingFacility
    '                Case IndustryType.CapitalComponentManufacturing
    '                    FacilityReference = SelectedBPCapitalComponentManufacturingFacility
    '                Case IndustryType.ComponentManufacturing
    '                    FacilityReference = SelectedBPComponentManufacturingFacility
    '                Case IndustryType.SubsystemManufacturing
    '                    FacilityReference = SelectedBPSubsystemManufacturingFacility
    '                Case IndustryType.SuperManufacturing
    '                    FacilityReference = SelectedBPSuperManufacturingFacility
    '                Case IndustryType.T3CruiserManufacturing
    '                    FacilityReference = SelectedBPT3CruiserManufacturingFacility
    '                Case IndustryType.T3DestroyerManufacturing
    '                    FacilityReference = SelectedBPT3DestroyerManufacturingFacility
    '                Case IndustryType.POSFuelBlockManufacturing
    '                    FacilityReference = SelectedBPPOSFuelBlockFacility
    '                Case IndustryType.POSLargeShipManufacturing
    '                    FacilityReference = SelectedBPPOSLargeShipFacility
    '                Case IndustryType.POSModuleManufacturing
    '                    FacilityReference = SelectedBPPOSModuleFacility
    '                Case IndustryType.NoPOSManufacturing
    '                    FacilityReference = SelectedBPNoPOSFacility
    '                Case IndustryType.Invention
    '                    FacilityReference = SelectedBPInventionFacility
    '                Case IndustryType.T3Invention
    '                    FacilityReference = SelectedBPT3InventionFacility
    '                Case IndustryType.Copying
    '                    FacilityReference = SelectedBPCopyFacility
    '                Case Else
    '                    FacilityReference = SelectedBPManufacturingFacility
    '            End Select
    '        Else
    '            Select Case IndyType
    '                Case IndustryType.Manufacturing
    '                    FacilityReference = SelectedCalcBaseManufacturingFacility
    '                Case IndustryType.BoosterManufacturing
    '                    FacilityReference = SelectedCalcBoosterManufacturingFacility
    '                Case IndustryType.CapitalManufacturing
    '                    FacilityReference = SelectedCalcCapitalManufacturingFacility
    '                Case IndustryType.ComponentManufacturing
    '                    FacilityReference = SelectedCalcComponentManufacturingFacility
    '                Case IndustryType.CapitalComponentManufacturing
    '                    FacilityReference = SelectedCalcCapitalComponentManufacturingFacility
    '                Case IndustryType.SubsystemManufacturing
    '                    FacilityReference = SelectedCalcSubsystemManufacturingFacility
    '                Case IndustryType.SuperManufacturing
    '                    FacilityReference = SelectedCalcSuperManufacturingFacility
    '                Case IndustryType.T3DestroyerManufacturing
    '                    FacilityReference = SelectedCalcT3DestroyerManufacturingFacility
    '                Case IndustryType.T3CruiserManufacturing
    '                    FacilityReference = SelectedCalcT3CruiserManufacturingFacility
    '                Case IndustryType.POSFuelBlockManufacturing
    '                    FacilityReference = SelectedCalcPOSFuelBlockFacility
    '                Case IndustryType.POSLargeShipManufacturing
    '                    FacilityReference = SelectedCalcPOSLargeShipFacility
    '                Case IndustryType.POSModuleManufacturing
    '                    FacilityReference = SelectedCalcPOSModuleFacility
    '                Case IndustryType.NoPOSManufacturing
    '                    FacilityReference = SelectedCalcNoPOSFacility
    '                Case IndustryType.Invention
    '                    FacilityReference = SelectedCalcInventionFacility
    '                Case IndustryType.T3Invention
    '                    FacilityReference = SelectedCalcT3InventionFacility
    '                Case IndustryType.Copying
    '                    FacilityReference = SelectedCalcCopyFacility
    '                Case Else
    '                    FacilityReference = SelectedCalcBaseManufacturingFacility
    '            End Select
    '        End If

    '        If Clone Then
    '            Return CType(FacilityReference.Clone(), IndustryFacility)
    '        Else
    '            Return FacilityReference ' Only return the reference
    '        End If

    '    End Function

    '    ' Resets all combo boxes that might need to be updated 
    '    Public Sub ResetComboLoadVariables(Tab As String, ProductionType As IndustryType, RegionsValue As Boolean, SystemsValue As Boolean,
    '                                       FacilitiesValue As Boolean, ManualIndexUpdate As Boolean, ByRef ManualSystemIndexGroupBox As GroupBox)

    '        If Tab = BPTab Then
    '            BPFacilityRegionsLoaded = RegionsValue
    '            BPFacilitySystemsLoaded = SystemsValue
    '            BPFacilitiesLoaded = FacilitiesValue
    '            ManualSystemIndexGroupBox.Enabled = ManualIndexUpdate
    '        Else
    '            Select Case ProductionType
    '                Case IndustryType.Manufacturing
    '                    CalcBaseFacilitiesLoaded = FacilitiesValue
    '                    CalcBaseFacilitySystemsLoaded = SystemsValue
    '                    CalcBaseFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.SuperManufacturing
    '                    CalcSuperFacilitiesLoaded = FacilitiesValue
    '                    CalcSuperFacilitySystemsLoaded = SystemsValue
    '                    CalcSuperFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.CapitalManufacturing
    '                    CalcCapitalFacilitiesLoaded = FacilitiesValue
    '                    CalcCapitalFacilitySystemsLoaded = SystemsValue
    '                    CalcCapitalFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.BoosterManufacturing
    '                    CalcBoosterFacilitiesLoaded = FacilitiesValue
    '                    CalcBoosterFacilitySystemsLoaded = SystemsValue
    '                    CalcBoosterFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.T3CruiserManufacturing, IndustryType.T3DestroyerManufacturing
    '                    CalcT3FacilitiesLoaded = FacilitiesValue
    '                    CalcT3FacilitySystemsLoaded = SystemsValue
    '                    CalcT3FacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.SubsystemManufacturing
    '                    CalcSubsystemFacilitiesLoaded = FacilitiesValue
    '                    CalcSubsystemFacilitySystemsLoaded = SystemsValue
    '                    CalcSubsystemFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.Invention
    '                    CalcInventionFacilitiesLoaded = FacilitiesValue
    '                    CalcInventionFacilitySystemsLoaded = SystemsValue
    '                    CalcInventionFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.T3Invention
    '                    CalcT3InventionFacilitiesLoaded = FacilitiesValue
    '                    CalcT3InventionFacilitySystemsLoaded = SystemsValue
    '                    CalcT3InventionFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.Copying
    '                    CalcCopyFacilitiesLoaded = FacilitiesValue
    '                    CalcCopyFacilitySystemsLoaded = SystemsValue
    '                    CalcCopyFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.NoPOSManufacturing
    '                    CalcNoPOSFacilitiesLoaded = FacilitiesValue
    '                    CalcNoPOSFacilitySystemsLoaded = SystemsValue
    '                    CalcNoPOSFacilityRegionsLoaded = RegionsValue
    '                Case IndustryType.ComponentManufacturing, IndustryType.CapitalComponentManufacturing
    '                    CalcComponentFacilitiesLoaded = FacilitiesValue
    '                    CalcComponentFacilitySystemsLoaded = SystemsValue
    '                    CalcComponentFacilityRegionsLoaded = RegionsValue
    '            End Select
    '        End If
    '    End Sub

    '    ' Loads the default facility for activity sent unless specified
    '    Public Sub LoadFacility(ProductionType As IndustryType, IsDefault As Boolean, NewBP As Boolean,
    '                             FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox, ByRef FacilityRegionCombo As ComboBox,
    '                             ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
    '                             ByRef FacilityDefaultLabel As Label,
    '                             ByRef FacilityManualMETextBox As TextBox, ByRef FacilityManualTETextBox As TextBox, ByRef FacilityManualTaxTextBox As TextBox,
    '                             ByRef FacilityManualMELabel As Label, ByRef FacilityManualTELabel As Label, ByRef FacilityManualTaxLabel As Label,
    '                             ByRef FacilitySaveButton As Button, Tab As String,
    '                             ByRef FacilityUsageCheck As CheckBox, ByRef FacilityIncludeLabel As Label,
    '                             ByRef FacilityActivityCostCheck As CheckBox, ByRef FacilityActivityTimeCheck As CheckBox,
    '                             ByRef FacilityLoaded As Boolean,
    '                             Optional ByRef FacilityActivityCombo As ComboBox = Nothing, Optional BPTech As Integer = 1,
    '                             Optional ItemGroupID As Integer = 0, Optional ItemCategoryID As Integer = 0,
    '                             Optional LoadActivites As Boolean = True, Optional RefreshBP As Boolean = True,
    '                             Optional ByRef FacilityUsageLabel As Label = Nothing,
    '                             Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing, Optional ByRef ToolTipRef As ToolTip = Nothing,
    '                             Optional ByRef FWUpgradeLabel As Label = Nothing, Optional ByRef FWUpgradeCombo As ComboBox = Nothing,
    '                             Optional ByRef FWUpgradeButton As Button = Nothing)

    '        Dim SelectedFacility As New IndustryFacility
    '        Dim SelectedActivity As String = ActivityManufacturing
    '        Dim FacilityName As String = ""

    '        If Tab = BPTab Then
    '            If IsDefault Then
    '                Select Case ProductionType
    '                    Case IndustryType.Manufacturing
    '                        SelectedFacility = CType(DefaultBPManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SuperManufacturing
    '                        SelectedFacility = CType(DefaultBPSuperManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.CapitalManufacturing
    '                        SelectedFacility = CType(DefaultBPCapitalManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.BoosterManufacturing
    '                        SelectedFacility = CType(DefaultBPBoosterManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3CruiserManufacturing
    '                        SelectedFacility = CType(DefaultBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3DestroyerManufacturing
    '                        SelectedFacility = CType(DefaultBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SubsystemManufacturing
    '                        SelectedFacility = CType(DefaultBPSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.Invention
    '                        SelectedFacility = CType(DefaultBPInventionFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityInvention
    '                    Case IndustryType.T3Invention
    '                        SelectedFacility = CType(DefaultBPT3InventionFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityInvention
    '                    Case IndustryType.Copying
    '                        SelectedActivity = ActivityCopying
    '                        SelectedFacility = CType(DefaultBPCopyFacility.Clone, IndustryFacility)
    '                    Case IndustryType.NoPOSManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultBPNoPOSFacility.Clone, IndustryFacility)
    '                    Case IndustryType.ComponentManufacturing
    '                        SelectedActivity = ActivityComponentManufacturing
    '                        SelectedFacility = CType(DefaultBPComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.CapitalComponentManufacturing
    '                        SelectedActivity = ActivityCapComponentManufacturing
    '                        SelectedFacility = CType(DefaultBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSFuelBlockManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultBPPOSFuelBlockFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSLargeShipManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultBPPOSLargeShipFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSModuleManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultBPPOSModuleFacility.Clone, IndustryFacility)
    '                End Select

    '            Else
    '                Select Case ProductionType
    '                    Case IndustryType.Manufacturing
    '                        SelectedFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SuperManufacturing
    '                        SelectedFacility = CType(SelectedBPSuperManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.CapitalManufacturing
    '                        SelectedFacility = CType(SelectedBPCapitalManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.BoosterManufacturing
    '                        SelectedFacility = CType(SelectedBPBoosterManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3CruiserManufacturing
    '                        SelectedFacility = CType(SelectedBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3DestroyerManufacturing
    '                        SelectedFacility = CType(SelectedBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SubsystemManufacturing
    '                        SelectedFacility = CType(SelectedBPSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.Invention
    '                        SelectedActivity = ActivityInvention
    '                        SelectedFacility = CType(SelectedBPInventionFacility.Clone, IndustryFacility)
    '                    Case IndustryType.T3Invention
    '                        SelectedActivity = ActivityInvention
    '                        SelectedFacility = CType(SelectedBPT3InventionFacility.Clone, IndustryFacility)
    '                    Case IndustryType.Copying
    '                        SelectedActivity = ActivityCopying
    '                        SelectedFacility = CType(SelectedBPCopyFacility.Clone, IndustryFacility)
    '                    Case IndustryType.NoPOSManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedBPNoPOSFacility.Clone, IndustryFacility)
    '                    Case IndustryType.ComponentManufacturing
    '                        SelectedActivity = ActivityComponentManufacturing
    '                        SelectedFacility = CType(SelectedBPComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.CapitalComponentManufacturing
    '                        SelectedActivity = ActivityCapComponentManufacturing
    '                        SelectedFacility = CType(SelectedBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSFuelBlockManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedBPPOSFuelBlockFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSLargeShipManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedBPPOSLargeShipFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSModuleManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedBPPOSModuleFacility.Clone, IndustryFacility)
    '                End Select
    '            End If
    '        Else
    '            If IsDefault Then
    '                Select Case ProductionType
    '                    Case IndustryType.Manufacturing
    '                        SelectedFacility = CType(DefaultCalcBaseManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SuperManufacturing
    '                        SelectedFacility = CType(DefaultCalcSuperManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.CapitalManufacturing
    '                        SelectedFacility = CType(DefaultCalcCapitalManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.BoosterManufacturing
    '                        SelectedFacility = CType(DefaultCalcBoosterManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3CruiserManufacturing
    '                        SelectedFacility = CType(DefaultCalcT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3DestroyerManufacturing
    '                        SelectedFacility = CType(DefaultCalcT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SubsystemManufacturing
    '                        SelectedFacility = CType(DefaultCalcSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.Invention
    '                        SelectedActivity = ActivityInvention
    '                        SelectedFacility = CType(DefaultCalcInventionFacility.Clone, IndustryFacility)
    '                    Case IndustryType.T3Invention
    '                        SelectedActivity = ActivityInvention
    '                        SelectedFacility = CType(DefaultCalcT3InventionFacility.Clone, IndustryFacility)
    '                    Case IndustryType.Copying
    '                        SelectedActivity = ActivityCopying
    '                        SelectedFacility = CType(DefaultCalcCopyFacility.Clone, IndustryFacility)
    '                    Case IndustryType.NoPOSManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultCalcNoPOSFacility.Clone, IndustryFacility)
    '                    Case IndustryType.ComponentManufacturing
    '                        SelectedActivity = ActivityComponentManufacturing
    '                        SelectedFacility = CType(DefaultCalcComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.CapitalComponentManufacturing
    '                        SelectedActivity = ActivityCapComponentManufacturing
    '                        SelectedFacility = CType(DefaultCalcCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSFuelBlockManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultCalcPOSFuelBlockFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSLargeShipManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultCalcPOSLargeShipFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSModuleManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(DefaultCalcPOSModuleFacility.Clone, IndustryFacility)
    '                End Select

    '            Else
    '                Select Case ProductionType
    '                    Case IndustryType.Manufacturing
    '                        SelectedFacility = CType(SelectedCalcBaseManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SuperManufacturing
    '                        SelectedFacility = CType(SelectedCalcSuperManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.CapitalManufacturing
    '                        SelectedFacility = CType(SelectedCalcCapitalManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.BoosterManufacturing
    '                        SelectedFacility = CType(SelectedCalcBoosterManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3CruiserManufacturing
    '                        SelectedFacility = CType(SelectedCalcT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.T3DestroyerManufacturing
    '                        SelectedFacility = CType(SelectedCalcT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.SubsystemManufacturing
    '                        SelectedFacility = CType(SelectedCalcSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                        SelectedActivity = ActivityManufacturing
    '                    Case IndustryType.Invention
    '                        SelectedActivity = ActivityInvention
    '                        SelectedFacility = CType(SelectedCalcInventionFacility.Clone, IndustryFacility)
    '                    Case IndustryType.T3Invention
    '                        SelectedActivity = ActivityInvention
    '                        SelectedFacility = CType(SelectedCalcT3InventionFacility.Clone, IndustryFacility)
    '                    Case IndustryType.Copying
    '                        SelectedActivity = ActivityCopying
    '                        SelectedFacility = CType(SelectedCalcCopyFacility.Clone, IndustryFacility)
    '                    Case IndustryType.NoPOSManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedCalcNoPOSFacility.Clone, IndustryFacility)
    '                    Case IndustryType.ComponentManufacturing
    '                        SelectedActivity = ActivityComponentManufacturing
    '                        SelectedFacility = CType(SelectedCalcComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.CapitalComponentManufacturing
    '                        SelectedActivity = ActivityCapComponentManufacturing
    '                        SelectedFacility = CType(SelectedCalcCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSFuelBlockManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedCalcPOSFuelBlockFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSLargeShipManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedCalcPOSLargeShipFacility.Clone, IndustryFacility)
    '                    Case IndustryType.POSModuleManufacturing
    '                        SelectedActivity = ActivityManufacturing
    '                        SelectedFacility = CType(SelectedCalcPOSModuleFacility.Clone, IndustryFacility)
    '                End Select
    '            End If
    '        End If

    '        If LoadActivites And Not IsNothing(FacilityActivityCombo) Then
    '            Call LoadFacilityActivities(BPTech, NewBP, FacilityActivityCombo, ItemGroupID, ItemCategoryID)
    '        End If

    '        ' Activity combo is loaded so set the activity Text
    '        LoadingFacilityActivities = True
    '        If Not IsNothing(FacilityActivityCombo) Then
    '            FacilityActivityCombo.Text = SelectedActivity
    '        End If
    '        PreviousIndustryType = ProductionType
    '        PreviousActivity = SelectedActivity
    '        LoadingFacilityActivities = False

    '        ' Facility Type combo
    '        ' Load the combo if they want to change
    '        Call LoadFacilityTypeCombo(ProductionType, FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo,
    '                                   FacilityCombo, FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualMETextBox, FacilityManualTaxTextBox,
    '                                   FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
    '                                   FacilitySaveButton, Tab, FacilityUsageLabel, FacilityUsageCheck, ManualSystemIndexGroupBox)

    '        ' Enable the type of facility and set
    '        LoadingFacilityTypes = True
    '        FacilityTypeCombo.Enabled = True
    '        FacilityTypeCombo.Text = SelectedFacility.FacilityType
    '        LoadingFacilityTypes = False

    '        If SelectedFacility.FacilityType = None Then
    '            ' Just hide the boxes and exit
    '            Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                       FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, False, FacilityUsageLabel)
    '            Call SetNoFacility(FacilityRegionCombo, FacilitySystemCombo, FacilityCombo, FacilityUsageCheck,
    '                               FacilityActivityCostCheck, FacilityActivityTimeCheck, FacilityIncludeLabel)
    '            FacilityLoaded = True ' Even with none, it's loaded
    '            Exit Sub
    '        End If

    '        ' Region name Combo
    '        LoadingFacilityRegions = True
    '        FacilityRegionCombo.Enabled = True
    '        FacilityRegionCombo.Text = SelectedFacility.RegionName
    '        LoadingFacilityRegions = False

    '        ' Systems combo
    '        LoadingFacilitySystems = True
    '        FacilitySystemCombo.Enabled = True
    '        FacilitySystemCombo.Text = SelectedFacility.SolarSystemName
    '        LoadingFacilitySystems = False

    '        ' Facility/Array combo
    '        LoadingFacilities = True
    '        FacilityCombo.Enabled = True
    '        Dim AutoLoad As Boolean = False
    '        'If it's a pos, need to auto-load the facility for that item selected
    '        If FacilityTypeCombo.Text = POSFacility And Tab = BPTab Then
    '            Call LoadFacilities(ItemGroupID, ItemCategoryID, False,
    '                                FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
    '                                FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
    '                                FacilitySaveButton, Tab, FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, AutoLoad,
    '                                SelectedFacility.IncludeActivityUsage, SelectedFacility.FacilityName, FacilityUsageLabel, ManualSystemIndexGroupBox, ToolTipRef, FWUpgradeLabel, FWUpgradeCombo)
    '        ElseIf Tab = CalcTab Then
    '            ' Load all facilities for each calc tab facility
    '            Call LoadFacilities(ItemGroupID, ItemCategoryID, False,
    '                    FacilityActivity, FacilityTypeCombo, FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
    '                    FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                    FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, FacilitySaveButton, Tab,
    '                    FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, AutoLoad,
    '                    SelectedFacility.IncludeActivityUsage, SelectedFacility.FacilityName, FacilityUsageLabel, ManualSystemIndexGroupBox, ToolTipRef)
    '        End If
    '        LoadingFacilities = False

    '        ' Usage checks
    '        ChangingUsageChecks = True
    '        FacilityUsageCheck.Checked = SelectedFacility.IncludeActivityUsage

    '        If Not IsNothing(FacilityActivityCostCheck) Then
    '            FacilityActivityCostCheck.Checked = SelectedFacility.IncludeActivityCost
    '        End If

    '        If Not IsNothing(FacilityActivityTimeCheck) Then
    '            FacilityActivityTimeCheck.Checked = SelectedFacility.IncludeActivityTime
    '        End If
    '        ChangingUsageChecks = False

    '        ' Finally show the results and save the facility locally
    '        If Not AutoLoad Then
    '            LoadingFacilities = True
    '            FacilityCombo.Text = SelectedFacility.FacilityName
    '            Call DisplayFacilityBonus(SelectedFacility.ProductionType, SelectedFacility.MaterialMultiplier, SelectedFacility.TimeMultiplier, SelectedFacility.TaxRate,
    '                                      ItemGroupID, ItemCategoryID,
    '                                      FacilityActivity, FacilityTypeCombo.Text, FacilityCombo.Text,
    '                                      FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
    '                                      FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                      FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
    '                                      FacilitySaveButton, FacilityUsageCheck, FacilityActivityCostCheck, FacilityActivityTimeCheck, Tab, FacilityLoaded,
    '                                      SelectedFacility.IncludeActivityUsage, ToolTipRef, GetFWUpgradeLevel(FWUpgradeCombo, FacilitySystemCombo.Text))
    '            LoadingFacilities = False
    '        End If

    '        ' If this is the BP tab, then refresh the BP prices
    '        If Not FirstLoad And SetTaxFeeChecks And Tab = BPTab And RefreshBP Then
    '            If Not IsNothing(SelectedBlueprint) Then
    '                Call SelectedBlueprint.SetPriceData(frmMain.chkBPTaxes.Checked, frmMain.chkBPBrokerFees.Checked)
    '                Call frmMain.UpdateBPPriceLabels()
    '            End If
    '        End If

    '        Call ResetComboLoadVariables(Tab, ProductionType, False, False, False, True, ManualSystemIndexGroupBox)

    '        ' Set here in case we load the bonuses
    '        Call SetFWUpgradeControls(Tab, FWUpgradeLabel, FWUpgradeCombo, SelectedFacility.SolarSystemName, FWUpgradeButton)

    '        ' All facilities loaded
    '        FacilityLoaded = True

    '        If Tab = CalcTab And Not FirstLoad Then
    '            Call frmMain.ResetRefresh()
    '        End If

    '    End Sub

    '    ' Loads the bp facility activity combo
    '    Public Sub LoadFacilityActivities(BPTech As Integer, NewBP As Boolean, ByRef FacilityActivitiesCombo As ComboBox,
    '                                      BPGroupID As Long, BPCategoryID As Long)

    '        LoadingFacilityActivities = True
    '        FacilityActivitiesCombo.BeginUpdate()

    '        Select Case BPTech
    '            Case BlueprintTechLevel.T1
    '                ' Just manufacturing (add components later if there are any)
    '                FacilityActivitiesCombo.Items.Clear()
    '                FacilityActivitiesCombo.Items.Add(ActivityManufacturing)

    '            Case BlueprintTechLevel.T2
    '                ' Add only T2 activities to equipment
    '                FacilityActivitiesCombo.Items.Clear()
    '                FacilityActivitiesCombo.Items.Add(ActivityManufacturing)
    '                FacilityActivitiesCombo.Items.Add(ActivityCopying)
    '                FacilityActivitiesCombo.Items.Add(ActivityInvention)

    '            Case BlueprintTechLevel.T3
    '                ' Add only T3 activities to eqipment
    '                FacilityActivitiesCombo.Items.Clear()
    '                FacilityActivitiesCombo.Items.Add(ActivityManufacturing)
    '                FacilityActivitiesCombo.Items.Add(ActivityInvention)

    '        End Select

    '        ' Add components as a manufacturing facility option if this bp has any
    '        If Not IsNothing(SelectedBlueprint) And Not NewBP Then
    '            If SelectedBlueprint.HasComponents Then
    '                Select Case BPGroupID
    '                    Case TitanGroupID, DreadnoughtGroupID, CarrierGroupID, SupercarrierGroupID, CapitalIndustrialShipGroupID,
    '                         IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID, FAXGroupID
    '                        FacilityActivitiesCombo.Items.Add(ActivityCapComponentManufacturing)
    '                        If BPGroupID = JumpFreighterGroupID Then
    '                            ' Need to add both cap and components
    '                            FacilityActivitiesCombo.Items.Add(ActivityComponentManufacturing)
    '                        End If
    '                    Case Else
    '                        FacilityActivitiesCombo.Items.Add(ActivityComponentManufacturing)
    '                End Select
    '            End If
    '        End If

    '        ' Only the BP tab will call this
    '        BPFacilitiesLoaded = False
    '        BPFacilityRegionsLoaded = False
    '        BPFacilitySystemsLoaded = False

    '        LoadingFacilityActivities = False
    '        FacilityActivitiesCombo.EndUpdate()

    '    End Sub

    '    ' Loads the facility types in the sent combo
    '    Public Sub LoadFacilityTypeCombo(ProductionType As IndustryType,
    '                             ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
    '                             ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
    '                             ByRef FacilityDefaultLabel As Label, ByRef FacilityManualMETextBox As TextBox, ByRef FacilityManualTETextBox As TextBox, ByRef FacilityManualTaxTextBox As TextBox,
    '                             ByRef FacilityManualMELabel As Label, ByRef FacilityManualTELabel As Label, ByRef FacilityManualTaxLabel As Label,
    '                             ByRef FacilitySaveButton As Button, Tab As String, ByRef FacilityUsageLabel As Label, ByRef FacilityUsageCheck As CheckBox, ByRef ManualSystemIndexGroupBox As GroupBox)

    '        LoadingFacilityTypes = True
    '        LoadingFacilityRegions = True
    '        LoadingFacilitySystems = True
    '        LoadingFacilities = True

    '        ' Clear the types each time for a fresh set of options
    '        FacilityTypeCombo.Items.Clear()

    '        ' Load the facility type options
    '        Select Case FacilityActivity
    '            ' Load up None for Invention/RE, Copy - they could buy the BP or T2 BPO
    '            Case ActivityCopying, ActivityInvention
    '                Select Case ProductionType
    '                    Case IndustryType.T3Invention
    '                        ' Can be invented in outposts and POS
    '                        FacilityTypeCombo.Items.Add(OutpostFacility)
    '                        FacilityTypeCombo.Items.Add(POSFacility)
    '                        'FacilityTypeCombo.Items.Add(CitadelFacility)
    '                        FacilityTypeCombo.Items.Add(None)
    '                    Case Else
    '                        FacilityTypeCombo.Items.Add(StationFacility)
    '                        FacilityTypeCombo.Items.Add(OutpostFacility)
    '                        'FacilityTypeCombo.Items.Add(CitadelFacility)
    '                        FacilityTypeCombo.Items.Add(POSFacility)
    '                        FacilityTypeCombo.Items.Add(None)
    '                End Select
    '            Case ActivityManufacturing
    '                Select Case ProductionType
    '                    Case IndustryType.SuperManufacturing
    '                        ' Check types, supers can only be built in a pos
    '                        FacilityTypeCombo.Items.Add(POSFacility)
    '                        'FacilityTypeCombo.Items.Add(CitadelFacility)
    '                    Case IndustryType.BoosterManufacturing, IndustryType.SubsystemManufacturing, IndustryType.T3CruiserManufacturing, IndustryType.T3DestroyerManufacturing
    '                        ' Can be built in outposts and POS
    '                        FacilityTypeCombo.Items.Add(OutpostFacility)
    '                        FacilityTypeCombo.Items.Add(POSFacility)
    '                        'FacilityTypeCombo.Items.Add(CitadelFacility)
    '                    Case IndustryType.NoPOSManufacturing
    '                        ' No POS for stuff like infrastructure hubs
    '                        FacilityTypeCombo.Items.Add(StationFacility)
    '                        FacilityTypeCombo.Items.Add(OutpostFacility)
    '                        'FacilityTypeCombo.Items.Add(CitadelFacility)
    '                    Case Else
    '                        ' Add all
    '                        FacilityTypeCombo.Items.Add(StationFacility)
    '                        FacilityTypeCombo.Items.Add(OutpostFacility)
    '                        FacilityTypeCombo.Items.Add(POSFacility)
    '                        'FacilityTypeCombo.Items.Add(CitadelFacility)
    '                End Select
    '            Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                ' Can do these anywhere
    '                FacilityTypeCombo.Items.Add(StationFacility)
    '                FacilityTypeCombo.Items.Add(OutpostFacility)
    '                FacilityTypeCombo.Items.Add(POSFacility)
    '                'FacilityTypeCombo.Items.Add(CitadelFacility)
    '        End Select

    '        ' Only reset if they changed it
    '        If ProductionType <> PreviousIndustryType Or FacilityActivity <> PreviousActivity Then
    '            ' Reset all other dropdowns
    '            FacilityTypeCombo.Text = "Select Type"
    '            FacilityRegionCombo.Items.Clear()
    '            FacilityRegionCombo.Text = "Select Region"
    '            FacilityRegionCombo.Enabled = False
    '            FacilitySystemCombo.Items.Clear()
    '            FacilitySystemCombo.Text = "Select System"
    '            FacilitySystemCombo.Enabled = False
    '            FacilityCombo.Items.Clear()
    '            FacilityCombo.Text = "Select Facility / Array"
    '            FacilityCombo.Enabled = False
    '            FacilityUsageCheck.Enabled = False
    '            PreviousIndustryType = ProductionType
    '            PreviousActivity = FacilityActivity
    '            Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                       FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, False, FacilityUsageLabel)

    '        End If

    '        ' Double check the text selected and reset 
    '        If Not FacilityTypeCombo.Items.Contains(FacilityTypeCombo.Text) Then
    '            FacilityTypeCombo.Text = POSFacility ' can build almost everything (if not all) in a pos
    '        End If

    '        ' Enable the facility type combo
    '        FacilityTypeCombo.Enabled = True

    '        ' Make sure default is not shown yet
    '        'FacilityDefaultLabel.Visible = False
    '        FacilitySaveButton.Enabled = False

    '        LoadingFacilityTypes = False
    '        LoadingFacilityRegions = False
    '        LoadingFacilitySystems = False
    '        LoadingFacilities = False

    '        Call ResetComboLoadVariables(Tab, ProductionType, False, False, False, False, ManualSystemIndexGroupBox)

    '    End Sub

    '    ' Based on the selections, load the region combo
    '    Public Sub LoadFacilityRegions(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean,
    '                                    ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
    '                                    ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox, ByRef FacilityDefaultLabel As Label,
    '                                    ByRef FacilityManualMETextBox As TextBox, ByRef FacilityManualTETextBox As TextBox, ByRef FacilityManualTaxTextBox As TextBox,
    '                                    ByRef FacilityManualMELabel As Label, ByRef FacilityManualTELabel As Label, ByRef FacilityManualTaxLabel As Label,
    '                                    ByRef FacilitySaveButton As Button, Tab As String,
    '                                    ByRef FacilityUsageCheck As CheckBox,
    '                                    Optional ByRef FacilityUsageLabel As Label = Nothing,
    '                                    Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing)
    '        Dim SQL As String = ""
    '        Dim rsLoader As SQLiteDataReader

    '        LoadingFacilityRegions = True
    '        LoadingFacilitySystems = True
    '        LoadingFacilities = True

    '        FacilityRegionCombo.Items.Clear()

    '        ' Load regions from the facilities table - only load regions for our activity type and item group/category
    '        Select Case FacilityTypeCombo.Text

    '            Case OutpostFacility, StationFacility

    '                SQL = "SELECT DISTINCT REGION_NAME FROM STATION_FACILITIES WHERE OUTPOST "

    '                ' Set flag for outpost just to delineate
    '                If FacilityTypeCombo.Text = StationFacility Then
    '                    SQL = SQL & " = " & CStr(StationType.Station) & " "
    '                Else
    '                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
    '                End If

    '                Select Case FacilityActivity
    '                    Case ActivityManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Add only regions with stations that can make what we sent
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
    '                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Add category for components - All types can be built in stations
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
    '                    Case ActivityCopying
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
    '                    Case ActivityInvention
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
    '                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
    '                End Select

    '            Case POSFacility
    '                ' For a POS, load all regions as options, but adding only one wormhole region option and don't show Jove regions
    '                SQL = "SELECT DISTINCT CASE WHEN (REGIONS.regionID >=11000000 and REGIONS.regionid <=11000030) THEN 'Wormhole Space' ELSE regionName END AS REGION_NAME "
    '                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
    '                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "
    '                SQL = SQL & "AND (factionID <> 500005 OR factionID IS NULL) "

    '                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
    '                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
    '                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
    '                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
    '                    ' For caps, only show low sec
    '                    SQL = SQL & " AND security < .45 "
    '                End If

    '        End Select

    '        SQL = SQL & "GROUP BY REGION_NAME "

    '        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '        rsLoader = DBCommand.ExecuteReader

    '        While rsLoader.Read
    '            FacilityRegionCombo.Items.Add(rsLoader.GetString(0))
    '        End While

    '        ' Enable the region combo
    '        FacilityRegionCombo.Enabled = True

    '        ' Only turn off everything if it's set to select region
    '        If NewFacility Then
    '            FacilitySystemCombo.Items.Clear()
    '            FacilitySystemCombo.Text = "Select System"
    '            FacilitySystemCombo.Enabled = False
    '            FacilityCombo.Items.Clear()
    '            FacilityCombo.Text = "Select Facility / Array"
    '            FacilityCombo.Enabled = False
    '            ' Make sure default is not checked yet
    '            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
    '            FacilitySaveButton.Enabled = False
    '            FacilityUsageCheck.Enabled = False
    '            Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                       FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, False, FacilityUsageLabel)
    '        End If

    '        ' Only reset the region if the current selected region is not in list, also if it is in list, enable solarsystem
    '        If Not FacilityRegionCombo.Items.Contains(FacilityRegionCombo.Text) Then
    '            FacilityRegionCombo.Text = "Select Region"
    '        Else
    '            FacilitySystemCombo.Enabled = True
    '        End If

    '        LoadingFacilityRegions = False
    '        LoadingFacilitySystems = False
    '        LoadingFacilities = False

    '        Call ResetComboLoadVariables(Tab, GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text), True, False, False, False, ManualSystemIndexGroupBox)

    '        rsLoader.Close()
    '        rsLoader = Nothing
    '        DBCommand = Nothing

    '    End Sub

    '    ' Based on the selections, load the systems combo
    '    Public Sub LoadFacilitySystems(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean,
    '                                ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
    '                                ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox, ByRef FacilityDefaultLabel As Label,
    '                                ByRef FacilityManualMETextBox As TextBox, ByRef FacilityManualTETextBox As TextBox, ByRef FacilityManualTaxTextBox As TextBox,
    '                                ByRef FacilityManualMELabel As Label, ByRef FacilityManualTELabel As Label, ByRef FacilityManualTaxLabel As Label,
    '                                ByRef FacilitySaveButton As Button, Tab As String, ByRef FacilityUsageCheck As CheckBox,
    '                                Optional ByRef FacilityUsageLabel As Label = Nothing,
    '                                Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing)

    '        Dim SQL As String = ""
    '        Dim rsLoader As SQLiteDataReader

    '        LoadingFacilitySystems = True
    '        LoadingFacilities = True

    '        FacilitySystemCombo.Items.Clear()

    '        Select Case FacilityTypeCombo.Text

    '            Case OutpostFacility, StationFacility

    '                SQL = "SELECT DISTINCT SOLAR_SYSTEM_NAME, COST_INDEX FROM STATION_FACILITIES WHERE OUTPOST "

    '                ' Set flag for outpost just to delineate
    '                If FacilityTypeCombo.Text = StationFacility Then
    '                    SQL = SQL & " = " & CStr(StationType.Station) & " "
    '                Else
    '                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
    '                End If

    '                Select Case FacilityActivity
    '                    Case ActivityManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
    '                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Add category for components - All types can be built in stations
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
    '                    Case ActivityCopying
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
    '                    Case ActivityInvention
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
    '                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
    '                End Select

    '                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(FacilityRegionCombo.Text) & "'"

    '            Case POSFacility
    '                ' For a POS, load all systems, if wormhole 'region' selected, then load jspace systems
    '                SQL = "SELECT DISTINCT solarSystemName AS SOLAR_SYSTEM_NAME, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS COST_INDEX "
    '                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
    '                SQL = SQL & "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON solarSystemID = SOLAR_SYSTEM_ID "

    '                Select Case FacilityActivity
    '                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                    Case ActivityCopying
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
    '                    Case ActivityInvention
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
    '                End Select

    '                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "

    '                If FacilityRegionCombo.Text = "Wormhole Space" Then
    '                    SQL = SQL & "AND SOLAR_SYSTEMS.regionID >=11000000 and SOLAR_SYSTEMS.regionid <=11000030 "
    '                Else
    '                    ' For a POS, load all systems that have records linked
    '                    SQL = SQL & "AND regionName = '" & FormatDBString(FacilityRegionCombo.Text) & "'"
    '                End If

    '                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
    '                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
    '                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
    '                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
    '                    ' For caps, only show low sec
    '                    SQL = SQL & " AND security < .45 "
    '                End If

    '        End Select

    '        SQL = SQL & " GROUP BY SOLAR_SYSTEM_NAME, COST_INDEX"

    '        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '        rsLoader = DBCommand.ExecuteReader

    '        While rsLoader.Read
    '            FacilitySystemCombo.Items.Add(rsLoader.GetString(0) & " (" & FormatNumber(rsLoader.GetDouble(1), 3) & ")")
    '        End While

    '        ' Enable the system combo
    '        FacilitySystemCombo.Enabled = True

    '        ' Only turn off everything if it's set to select a system
    '        If NewFacility Then
    '            FacilityCombo.Items.Clear()
    '            If FacilityTypeCombo.Text = POSFacility Then
    '                FacilityCombo.Text = "Select Array"
    '            Else
    '                FacilityCombo.Text = "Select Facility"
    '            End If
    '            FacilityCombo.Enabled = False
    '            ' Make sure default is not checked yet
    '            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
    '            FacilitySaveButton.Enabled = False
    '            FacilityUsageCheck.Enabled = False
    '            Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                       FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, False, FacilityUsageLabel)
    '        End If

    '        ' Only reset the system if the current selected system is not in list, also if it is in list, enable facilty
    '        If Not FacilitySystemCombo.Items.Contains(FacilitySystemCombo.Text) Then
    '            FacilitySystemCombo.Text = "Select System"
    '        Else
    '            FacilityCombo.Enabled = True
    '        End If

    '        LoadingFacilitySystems = False
    '        LoadingFacilities = False

    '        Call ResetComboLoadVariables(Tab, GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text), False, True, False, False, ManualSystemIndexGroupBox)

    '        rsLoader.Close()
    '        rsLoader = Nothing
    '        DBCommand = Nothing

    '    End Sub

    '    ' Based on the selections, load the facilities/arrays combo - an itemcategory or itemgroup id of -1 means to ignore it when filling arrays
    '    Public Sub LoadFacilities(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean,
    '                               ByRef FacilityActivity As String, ByRef FacilityTypeCombo As ComboBox,
    '                               ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
    '                               ByRef FacilityDefaultLabel As Label, ByRef FacilityManualMETextBox As TextBox, ByRef FacilityManualTETextBox As TextBox, ByRef FacilityManualTaxTextBox As TextBox,
    '                               ByRef FacilityManualMELabel As Label, ByRef FacilityManualTELabel As Label, ByRef FacilityManualTaxLabel As Label,
    '                               ByRef FacilitySaveButton As Button, ByVal Tab As String, ByRef FacilityUsageCheck As CheckBox,
    '                               ByRef FacilityIncludeActivityCostsCheck As CheckBox, ByRef FacilityIncludeActivityTimeCheck As CheckBox,
    '                               ByRef AutoLoadFacility As Boolean, ByVal FacilityUsageCheckValue As Boolean,
    '                               Optional OverrideFacilityName As String = "", Optional ByRef FacilityUsageLabel As Label = Nothing,
    '                               Optional ByRef ManualSystemIndexGroupBox As GroupBox = Nothing, Optional ByRef ToolTipRef As ToolTip = Nothing,
    '                               Optional ByRef FWUpgradeLabel As Label = Nothing, Optional ByRef FWUpgradeCombo As ComboBox = Nothing, Optional ByVal SolarSystemName As String = "",
    '                               Optional ByRef FWUpgradeButton As Button = Nothing)
    '        Dim SQL As String = ""
    '        Dim rsLoader As SQLiteDataReader

    '        LoadingFacilities = True

    '        Select Case FacilityTypeCombo.Text

    '            Case StationFacility, OutpostFacility
    '                ' Load the Stations in system for the activity we are doing
    '                SQL = "SELECT DISTINCT FACILITY_NAME FROM STATION_FACILITIES WHERE OUTPOST "

    '                ' Set flag for outpost just to delineate
    '                If FacilityTypeCombo.Text = StationFacility Then
    '                    SQL = SQL & " = " & CStr(StationType.Station) & " "
    '                Else
    '                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
    '                End If

    '                Select Case FacilityActivity
    '                    Case ActivityManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Check groups and categories
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
    '                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Add category for components - All types can be built in stations
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
    '                    Case ActivityCopying
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
    '                    Case ActivityInvention
    '                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
    '                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
    '                End Select

    '                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(FacilityRegionCombo.Text) & "' "
    '                Dim SystemName As String = FacilitySystemCombo.Text.Substring(0, InStr(FacilitySystemCombo.Text, "(") - 2)
    '                SQL = SQL & "AND SOLAR_SYSTEM_NAME = '" & FormatDBString(SystemName) & "' "

    '            Case POSFacility

    '                ' Load all the array types up into the combo for a POS
    '                SQL = "SELECT DISTINCT ARRAY_NAME AS FACILITY_NAME FROM ASSEMBLY_ARRAYS "
    '                SQL = SQL & "WHERE ACTIVITY_ID = "

    '                Select Case FacilityActivity
    '                    Case ActivityManufacturing
    '                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Check groups and categories
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
    '                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
    '                        ' Add category for component
    '                        Select Case ItemGroupID
    '                            Case TitanGroupID, SupercarrierGroupID, DreadnoughtGroupID, CarrierGroupID,
    '                                CapitalIndustrialShipGroupID, IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID,
    '                                AdvCapitalComponentGroupID, CapitalComponentGroupID, FAXGroupID
    '                                SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
    '                            Case Else
    '                                SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
    '                        End Select
    '                    Case ActivityCopying
    '                        SQL = SQL & CStr(IndustryActivities.Copying) & " "
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
    '                    Case ActivityInvention
    '                        ' POS invention you can only do T3 in certain arrays
    '                        SQL = SQL & CStr(IndustryActivities.Invention) & " "
    '                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
    '                End Select

    '        End Select

    '        ' This is helpful if we auto-load (Capital array before super capital, equipment array before rapid equipment) to choose the one more likely
    '        SQL = SQL & " ORDER BY FACILITY_NAME"

    '        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '        rsLoader = DBCommand.ExecuteReader

    '        FacilityCombo.Enabled = True
    '        FacilityCombo.Items.Clear()

    '        Dim AutoLoadName As String = ""
    '        Dim i As Integer = 0

    '        While rsLoader.Read
    '            If rsLoader.GetString(0).Contains("Thukker") And FacilityTypeCombo.Text = POSFacility Then
    '                ' Need to make sure it's a low sec system selected
    '                Dim rsCheck As SQLiteDataReader
    '                SQL = "SELECT SECURITY FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(FacilitySystemCombo.Text.Substring(0, InStr(FacilitySystemCombo.Text, "(") - 2)) & "'"
    '                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '                rsCheck = DBCommand.ExecuteReader

    '                If rsCheck.Read Then
    '                    If rsCheck.GetDouble(0) < 0.45 Then
    '                        ' Thukker is only low sec - no easy way to weed this out
    '                        FacilityCombo.Items.Add(rsLoader.GetString(0))
    '                    End If
    '                Else
    '                    ' Allow it
    '                    FacilityCombo.Items.Add(rsLoader.GetString(0))
    '                End If
    '            Else
    '                FacilityCombo.Items.Add(rsLoader.GetString(0))
    '            End If

    '            i += 1 ' get the count
    '            ' Load the first one - auto choose subsystem array over advanced medium array unless already selected
    '            If AutoLoadName = "" Or (rsLoader.GetString(0) = "Subsystem Assembly Array" And OverrideFacilityName = "") Then
    '                AutoLoadName = rsLoader.GetString(0)
    '            End If
    '        End While

    '        ' Always load the facility if there is only one and we have a reference to auto load or we are loading a specific facility
    '        If (i = 1 And Not IsNothing(AutoLoadFacility)) Or FacilityCombo.Items.Contains(OverrideFacilityName) _
    '            Or FacilityCombo.Items.Contains(FacilityCombo.Text) Or OverrideFacilityName = "CalcBase" Then
    '            ' Check the override, if they want to use a rapid assembly it will override here, otherwise the other facility types should handle it (e.g. super, cap, etc)
    '            If OverrideFacilityName <> "" And FacilityCombo.Items.Contains(OverrideFacilityName) Then
    '                FacilityCombo.Text = OverrideFacilityName
    '            Else
    '                FacilityCombo.Text = AutoLoadName
    '            End If

    '            AutoLoadFacility = True
    '            ' Display bonuses - Need to load everything since the array won't change to cause it to reload
    '            Dim Defaults As New ProgramSettings

    '            ' Set the FW controls before loading bonuses
    '            Call SetFWUpgradeControls(Tab, FWUpgradeLabel, FWUpgradeCombo, FacilitySystemCombo.Text, FWUpgradeButton)

    '            ' For a pos, need to display the results and reload the bp
    '            Call DisplayFacilityBonus(GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text),
    '                                      Defaults.FacilityDefaultMM, Defaults.FacilityDefaultTM, Defaults.FacilityDefaultTax, ItemGroupID, ItemCategoryID,
    '                                      FacilityActivity, FacilityTypeCombo.Text, FacilityCombo.Text,
    '                                      FacilityRegionCombo, FacilitySystemCombo, FacilityCombo,
    '                                      FacilityDefaultLabel, FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                      FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel,
    '                                      FacilitySaveButton, FacilityUsageCheck, FacilityIncludeActivityCostsCheck, FacilityIncludeActivityTimeCheck,
    '                                      Tab, FullyLoadedBPFacility, FacilityUsageCheckValue, ToolTipRef, GetFWUpgradeLevel(FWUpgradeCombo, FacilitySystemCombo.Text))

    '        Else
    '            If Not FacilityCombo.Items.Contains(FacilityCombo.Text) Then
    '                ' Only load if the item isn't in the combo
    '                Select Case FacilityTypeCombo.Text
    '                    Case OutpostFacility, StationFacility
    '                        FacilityCombo.Text = "Select Facility"
    '                    Case POSFacility
    '                        FacilityCombo.Text = "Select Array"
    '                End Select

    '                ' Make sure default is turned off since we still have to load the array
    '                FacilitySaveButton.Enabled = False
    '                FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
    '                FacilityUsageCheck.Enabled = False ' Don't enable the usage either
    '            Else
    '                ' Since this is a different system but facility is loaded, enable save
    '                FacilitySaveButton.Enabled = True
    '                FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
    '                FacilityUsageCheck.Enabled = True
    '            End If

    '            AutoLoadFacility = False

    '        End If

    '        If NewFacility Then
    '            ' Make sure default is not checked yet
    '            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
    '            FacilitySaveButton.Enabled = False
    '            Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                       FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, True, FacilityUsageLabel)
    '        End If

    '        ' Users might select the facility drop down first, so reload all others
    '        Call ResetComboLoadVariables(Tab, GetProductionType(FacilityActivity, ItemGroupID, ItemCategoryID, FacilityTypeCombo.Text), False, False, True, True, ManualSystemIndexGroupBox)

    '        LoadingFacilities = False

    '        rsLoader.Close()
    '        rsLoader = Nothing
    '        DBCommand = Nothing

    '    End Sub

    '    ' Displays the bonus for the facility selected in the facility or array combo
    '    Public Sub DisplayFacilityBonus(ProductionType As IndustryType, SentMM As Double, SentTM As Double, SentTax As Double,
    '                                     ItemGroupID As Integer, ItemCategoryID As Integer,
    '                                     Activity As String, FacilityType As String, FacilityName As String,
    '                                     ByRef FacilityRegionCombo As ComboBox, ByRef FacilitySystemCombo As ComboBox, ByRef FacilityCombo As ComboBox,
    '                                     ByRef FacilityDefaultLabel As Label,
    '                                     ByRef FacilityManualMETextBox As TextBox, ByRef FacilityManualTETextBox As TextBox, ByRef FacilityManualTaxTextBox As TextBox,
    '                                     ByRef FacilityManualMELabel As Label, ByRef FacilityManualTELabel As Label, ByRef FacilityManualTaxLabel As Label,
    '                                     ByRef FacilitySaveButton As Button, ByRef FacilityUsageCheck As CheckBox,
    '                                     ByRef ActivityCostCheck As CheckBox, ByRef ActivityTimeCheck As CheckBox,
    '                                     ByRef Tab As String, ByRef FacilityLoaded As Boolean, ByRef FacilityUsageCheckValue As Boolean,
    '                                     ByRef ToolTipRef As ToolTip, ByVal FWUpgradeLevel As Integer)
    '        Dim SQL As String = ""
    '        Dim rsLoader As SQLiteDataReader

    '        Dim FacilityID As Long
    '        Dim FacilityTypeID As Long
    '        Dim MaterialMultiplier As Double
    '        Dim TimeMultiplier As Double
    '        Dim CostMultiplier As Double
    '        Dim Tax As Double

    '        Dim Defaults As New ProgramSettings
    '        Dim TempDefaultFacility As New IndustryFacility

    '        Dim SelectedFacility As New IndustryFacility
    '        Dim CompareCostCheck As Boolean = False
    '        Dim CompareTimeCheck As Boolean = False

    '        If FacilityType <> None Then
    '            Select Case FacilityType

    '                Case OutpostFacility, StationFacility

    '                    ' Load the Stations in system for the activity we are doing
    '                    SQL = "SELECT FACILITY_ID, FACILITY_TYPE_ID, MATERIAL_MULTIPLIER, "
    '                    SQL = SQL & "TIME_MULTIPLIER, COST_MULTIPLIER, "
    '                    SQL = SQL & "FACILITY_TAX FROM STATION_FACILITIES WHERE OUTPOST  "

    '                    ' Set flag for outpost just to delineate
    '                    If FacilityType = StationFacility Then
    '                        SQL = SQL & " = " & CStr(StationType.Station) & " "
    '                    Else
    '                        SQL = SQL & " = " & CStr(StationType.Outpost) & " "
    '                    End If
    '                    SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString(FacilityName) & "' "

    '                Case POSFacility

    '                    SQL = "SELECT 0 AS FACILITY_ID, ARRAY_TYPE_ID, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, 1 AS COST_MULTIPLIER, " & CStr(POSTaxRate) & " as TAX "
    '                    SQL = SQL & "FROM ASSEMBLY_ARRAYS "
    '                    SQL = SQL & "WHERE ARRAY_NAME = '" & FormatDBString(FacilityName) & "' "

    '            End Select

    '            Select Case Activity
    '                Case ActivityManufacturing
    '                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
    '                Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
    '                    ' Add category for component
    '                    Select Case ItemGroupID
    '                        Case TitanGroupID, SupercarrierGroupID, DreadnoughtGroupID, CarrierGroupID,
    '                            CapitalIndustrialShipGroupID, IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID,
    '                                AdvCapitalComponentGroupID, CapitalComponentGroupID, FAXGroupID
    '                            SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
    '                        Case Else
    '                            SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
    '                    End Select
    '                Case ActivityCopying
    '                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
    '                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
    '                Case ActivityInvention
    '                    SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
    '                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
    '            End Select

    '            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '            rsLoader = DBCommand.ExecuteReader

    '            If rsLoader.Read Then
    '                ' If we have values that are not the defaults, then they sent in something else saved (outposts) so set them here
    '                If FacilityType = OutpostFacility Then
    '                    If SentMM <> Defaults.FacilityDefaultMM Then
    '                        MaterialMultiplier = SentMM
    '                    Else
    '                        MaterialMultiplier = rsLoader.GetDouble(2)
    '                    End If

    '                    If SentTM <> Defaults.FacilityDefaultTM Then
    '                        TimeMultiplier = SentTM
    '                    Else
    '                        TimeMultiplier = rsLoader.GetDouble(3)
    '                    End If

    '                    If SentTax <> Defaults.FacilityDefaultTax Then
    '                        Tax = SentTax
    '                    Else
    '                        Tax = rsLoader.GetDouble(5)
    '                    End If

    '                Else ' For POS and Stations, this is already set
    '                    MaterialMultiplier = rsLoader.GetDouble(2)
    '                    TimeMultiplier = rsLoader.GetDouble(3)
    '                    Tax = rsLoader.GetDouble(5)
    '                End If

    '                CostMultiplier = rsLoader.GetDouble(4)

    '                FacilityID = rsLoader.GetInt64(0)
    '                FacilityTypeID = rsLoader.GetInt64(1)

    '                rsLoader.Close()
    '            Else
    '                ' Set the facility to none if not found
    '                FacilityType = None
    '            End If

    '        End If

    '        If FacilityType = None Then
    '            ' None selected or not found
    '            FacilityName = None
    '            FacilityID = 0
    '            FacilityTypeID = 0
    '            MaterialMultiplier = Defaults.FacilityDefaultMM
    '            TimeMultiplier = Defaults.FacilityDefaultTM
    '            CostMultiplier = 1
    '            Tax = Defaults.FacilityDefaultTax
    '        End If

    '        Dim MMText As String = FormatPercent(1 - MaterialMultiplier, 1)
    '        Dim TMText As String = FormatPercent(1 - TimeMultiplier, 1)
    '        Dim TaxText As String = FormatPercent(Tax, 1)

    '        If FacilityType = OutpostFacility Then
    '            FacilityManualMETextBox.Enabled = True
    '            FacilityManualTETextBox.Enabled = True
    '            FacilityManualTaxTextBox.Enabled = True
    '        Else ' Disable for non-outpost
    '            FacilityManualMETextBox.Enabled = False
    '            FacilityManualTETextBox.Enabled = False
    '            FacilityManualTaxTextBox.Enabled = False
    '        End If

    '        ' Set the values
    '        FacilityManualMETextBox.Text = MMText
    '        FacilityManualTETextBox.Text = TMText
    '        FacilityManualTaxTextBox.Text = TaxText

    '        ' Now that we have everything, load the full facility into the appropriate selected facility to use later
    '        With SelectedFacility
    '            .FacilityName = FacilityName
    '            Select Case Activity
    '                Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
    '                    .ActivityID = IndustryActivities.Manufacturing
    '                Case ActivityCopying
    '                    .ActivityID = IndustryActivities.Copying
    '                Case ActivityInvention
    '                    .ActivityID = IndustryActivities.Invention
    '            End Select

    '            .ActivityCostPerSecond = 0
    '            .FacilityType = FacilityType
    '            .MaterialMultiplier = MaterialMultiplier
    '            .TimeMultiplier = TimeMultiplier
    '            .RegionName = FacilityRegionCombo.Text
    '            .SolarSystemName = FacilitySystemCombo.Text
    '            .ProductionType = ProductionType
    '            ChangingUsageChecks = True
    '            .IncludeActivityUsage = FacilityUsageCheckValue ' Use this value when loading from Load Facility (using the selected facility) or from the form dropdown (use the checkbox)
    '            ChangingUsageChecks = False
    '            .TaxRate = Tax
    '            .FWUpgradeLevel = FWUpgradeLevel

    '            If Not IsNothing(ActivityCostCheck) Then
    '                .IncludeActivityCost = ActivityCostCheck.Checked
    '            Else
    '                .IncludeActivityTime = False
    '            End If

    '            If Not IsNothing(ActivityTimeCheck) Then
    '                .IncludeActivityTime = ActivityTimeCheck.Checked
    '            Else
    '                .IncludeActivityTime = False
    '            End If

    '            If FacilityType <> None And .SolarSystemID = 0 Then
    '                ' Quick look up for the solarsystemid and region id, Strip off the system index first
    '                Dim SystemName As String = .SolarSystemName.Substring(0, InStr(.SolarSystemName, "(") - 2)
    '                SQL = "SELECT solarSystemID, regionID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SystemName) & "'"

    '                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '                rsLoader = DBCommand.ExecuteReader
    '                rsLoader.Read()

    '                .SolarSystemID = rsLoader.GetInt64(0)
    '                .RegionID = rsLoader.GetInt64(1)
    '                rsLoader.Close()

    '                ' Now look up the cost index 
    '                If FacilityType <> POSFacility Then
    '                    SQL = "SELECT COST_INDEX FROM STATION_FACILITIES WHERE FACILITY_NAME = '" & FormatDBString(FacilityName) & "'"
    '                    SQL = SQL & "AND ACTIVITY_ID = " & .ActivityID & " "
    '                Else
    '                    SQL = "SELECT COST_INDEX FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES "
    '                    SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
    '                    SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
    '                    SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = " & .ActivityID & " "
    '                End If

    '                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '                rsLoader = DBCommand.ExecuteReader

    '                If rsLoader.Read() Then
    '                    .CostIndex = rsLoader.GetDouble(0)
    '                Else
    '                    .CostIndex = 0
    '                End If

    '                rsLoader.Close()
    '            Else
    '                .SolarSystemID = 0
    '                .RegionID = 0
    '                .CostIndex = 0
    '            End If
    '        End With

    '        ' Show the boxes
    '        Call SetFacilityBonusBoxes(FacilityManualMETextBox, FacilityManualTETextBox, FacilityManualTaxTextBox,
    '                                   FacilityManualMELabel, FacilityManualTELabel, FacilityManualTaxLabel, True)

    '        Call SetFacilityandDefault(SelectedFacility, ProductionType, Tab, FacilityType, FacilityCombo,
    '                                   FacilityDefaultLabel, FacilitySaveButton, CompareCostCheck, CompareTimeCheck, ToolTipRef)

    '        ' Make sure the usage check is now enabled
    '        If FacilityType <> None Then
    '            FacilityUsageCheck.Enabled = True
    '        End If

    '        FacilityLoaded = True

    '        If Tab = CalcTab And Not FirstLoad Then
    '            Call frmMain.ResetRefresh()
    '        End If

    '        Application.DoEvents()

    '    End Sub

    '    Public Function GetFWUpgradeLevel(SolarSystemName As String) As Integer
    '        Dim rsFW As SQLiteDataReader

    '        ' Format system name
    '        If SolarSystemName.Contains("(") Then
    '            SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
    '        End If

    '        Dim SQL As String = "SELECT UPGRADE_LEVEL FROM SOLAR_SYSTEMS, FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = solarSystemID AND factionWarzone <> 0 AND solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
    '        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '        rsFW = DBCommand.ExecuteReader

    '        If rsFW.Read Then
    '            Return rsFW.GetInt32(0)
    '        Else
    '            Return 0
    '        End If

    '    End Function

    '    Public Function GetFWUpgradeLevel(FWUpgradeCombo As ComboBox, SolarSystemName As String) As Integer
    '        Dim FWUpgradeLevel As Integer

    '        Dim rsFW As SQLiteDataReader
    '        Dim SSID As Long

    '        ' Format system name
    '        If SolarSystemName.Contains("(") Then
    '            SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
    '        End If

    '        Dim SQL As String = "SELECT factionWarzone, solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
    '        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '        rsFW = DBCommand.ExecuteReader

    '        Dim Warzone As Boolean
    '        If rsFW.Read Then
    '            Warzone = CBool(rsFW.GetInt32(0))
    '            SSID = rsFW.GetInt64(1)
    '        Else
    '            Warzone = False
    '        End If

    '        If Warzone Then
    '            If Not IsNothing(FWUpgradeCombo) Then
    '                Select Case FWUpgradeCombo.Text
    '                    Case "Level 1"
    '                        FWUpgradeLevel = 1
    '                    Case "Level 2"
    '                        FWUpgradeLevel = 2
    '                    Case "Level 3"
    '                        FWUpgradeLevel = 3
    '                    Case "Level 4"
    '                        FWUpgradeLevel = 4
    '                    Case "Level 5"
    '                        FWUpgradeLevel = 5
    '                    Case Else
    '                        FWUpgradeLevel = 0
    '                End Select
    '            Else
    '                FWUpgradeLevel = 0
    '            End If
    '        Else
    '            FWUpgradeLevel = 0
    '        End If

    '        Return FWUpgradeLevel

    '    End Function

    '    ' enables the controls for fw settings on the bp tab
    '    Public Sub SetFWUpgradeControls(Tab As String, ByRef FWUpgradeLabel As Label, ByRef FWUpgradeCombo As ComboBox, ByVal SolarSystemName As String, ByRef FWUpgradeButton As Button)
    '        ' Load the faction warfare upgrade if set for the BP tab
    '        If Tab = BPTab Then
    '            Dim rsFW As SQLiteDataReader
    '            Dim SSID As Long

    '            ' Format system name
    '            If SolarSystemName.Contains("(") Then
    '                SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
    '            End If

    '            Dim SQL As String = "SELECT factionWarzone, solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
    '            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '            rsFW = DBCommand.ExecuteReader

    '            Dim Warzone As Boolean
    '            If rsFW.Read Then
    '                Warzone = CBool(rsFW.GetInt32(0))
    '                SSID = rsFW.GetInt64(1)
    '            Else
    '                Warzone = False
    '            End If

    '            If Warzone Then
    '                FWUpgradeLabel.Enabled = True
    '                FWUpgradeCombo.Enabled = True
    '                ' look up level
    '                Dim rsFWLevel As SQLiteDataReader
    '                SQL = "SELECT UPGRADE_LEVEL FROM FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = " & CStr(SSID)
    '                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '                rsFWLevel = DBCommand.ExecuteReader
    '                rsFWLevel.Read()

    '                If rsFWLevel.HasRows Then
    '                    If rsFWLevel.GetInt32(0) = 0 Then
    '                        FWUpgradeCombo.Text = None
    '                    Else
    '                        FWUpgradeCombo.Text = "Level " & CStr(rsFWLevel.GetInt32(0))
    '                    End If
    '                Else
    '                    FWUpgradeCombo.Text = None
    '                End If
    '            Else
    '                FWUpgradeLabel.Enabled = False
    '                FWUpgradeCombo.Enabled = False
    '                FWUpgradeCombo.Text = None
    '            End If
    '            rsFW.Close()
    '        End If

    '        If Not IsNothing(FWUpgradeButton) Then
    '            FWUpgradeButton.Enabled = False
    '        End If

    '    End Sub

    '    ' Sets the sent facility to the one we are selecting and sets the default 
    '    Public Sub SetFacilityandDefault(ByVal SelectedFacility As IndustryFacility, ProductionType As IndustryType, Tab As String,
    '                                      ByRef FacilityType As String, ByRef FacilityCombo As ComboBox,
    '                                      ByRef FacilityDefaultLabel As Label, ByRef FacilitySaveButton As Button,
    '                                      ByVal CompareIncludeCostCheck As Boolean,
    '                                      ByVal CompareIncludeTimeCheck As Boolean, ByRef ToolTipRef As ToolTip)
    '        ' For checking change from stations to pos on bp tab
    '        Dim PreviousFacility As New IndustryFacility

    '        ' Based on the type of activity, set the selected facility for that type
    '        If Tab = BPTab Then
    '            Select Case ProductionType
    '                Case IndustryType.Manufacturing
    '                    PreviousFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
    '                    ' Set the other three types for pos too
    '                    If SelectedFacility.FacilityType = POSFacility Then
    '                        SelectedFacility.FacilityName = SelectedBPPOSFuelBlockFacility.FacilityName
    '                        SelectedFacility.FacilityType = SelectedBPPOSFuelBlockFacility.FacilityType
    '                        SelectedBPPOSFuelBlockFacility = CType(SelectedFacility.Clone, IndustryFacility)

    '                        SelectedFacility.FacilityName = SelectedBPPOSLargeShipFacility.FacilityName
    '                        SelectedFacility.FacilityType = SelectedBPPOSLargeShipFacility.FacilityType
    '                        SelectedBPPOSLargeShipFacility = CType(SelectedFacility.Clone, IndustryFacility)

    '                        SelectedFacility.FacilityName = SelectedBPPOSModuleFacility.FacilityName
    '                        SelectedFacility.FacilityType = SelectedBPPOSModuleFacility.FacilityType
    '                        SelectedBPPOSModuleFacility = CType(SelectedFacility.Clone, IndustryFacility)
    '                    End If
    '                    If SelectedBPManufacturingFacility.IsEqual(DefaultBPManufacturingFacility) Then
    '                        SelectedBPManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.POSFuelBlockManufacturing
    '                    PreviousFacility = CType(SelectedBPPOSFuelBlockFacility.Clone, IndustryFacility)
    '                    SelectedBPPOSFuelBlockFacility = SelectedFacility
    '                    SelectedBPManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
    '                    If SelectedBPPOSFuelBlockFacility.IsEqual(DefaultBPPOSFuelBlockFacility) Then
    '                        SelectedBPPOSFuelBlockFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPPOSFuelBlockFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.POSLargeShipManufacturing
    '                    PreviousFacility = CType(SelectedBPPOSLargeShipFacility.Clone, IndustryFacility)
    '                    SelectedBPPOSLargeShipFacility = SelectedFacility
    '                    SelectedBPManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
    '                    If SelectedBPPOSLargeShipFacility.IsEqual(DefaultBPPOSLargeShipFacility) Then
    '                        SelectedBPPOSLargeShipFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPPOSLargeShipFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.POSModuleManufacturing
    '                    PreviousFacility = CType(SelectedBPPOSModuleFacility.Clone, IndustryFacility)
    '                    SelectedBPPOSModuleFacility = SelectedFacility
    '                    SelectedBPManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
    '                    If SelectedBPPOSModuleFacility.IsEqual(DefaultBPPOSModuleFacility) Then
    '                        SelectedBPPOSModuleFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPPOSModuleFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.BoosterManufacturing
    '                    PreviousFacility = CType(SelectedBPBoosterManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPBoosterManufacturingFacility = SelectedFacility
    '                    If SelectedBPBoosterManufacturingFacility.IsEqual(DefaultBPBoosterManufacturingFacility) Then
    '                        SelectedBPBoosterManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPBoosterManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.CapitalManufacturing
    '                    PreviousFacility = CType(SelectedBPCapitalManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPCapitalManufacturingFacility = SelectedFacility
    '                    If SelectedBPCapitalManufacturingFacility.IsEqual(DefaultBPCapitalManufacturingFacility) Then
    '                        SelectedBPCapitalManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPCapitalManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.SuperManufacturing
    '                    PreviousFacility = CType(SelectedBPSuperManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPSuperManufacturingFacility = SelectedFacility
    '                    If SelectedBPSuperManufacturingFacility.IsEqual(DefaultBPSuperManufacturingFacility) Then
    '                        SelectedBPSuperManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPSuperManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.T3CruiserManufacturing
    '                    PreviousFacility = CType(SelectedBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPT3CruiserManufacturingFacility = SelectedFacility
    '                    If SelectedBPT3CruiserManufacturingFacility.IsEqual(DefaultBPT3CruiserManufacturingFacility) Then
    '                        SelectedBPT3CruiserManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPT3CruiserManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.T3DestroyerManufacturing
    '                    PreviousFacility = CType(SelectedBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPT3DestroyerManufacturingFacility = SelectedFacility
    '                    If SelectedBPT3DestroyerManufacturingFacility.IsEqual(DefaultBPT3DestroyerManufacturingFacility) Then
    '                        SelectedBPT3DestroyerManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPT3DestroyerManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.SubsystemManufacturing
    '                    PreviousFacility = CType(SelectedBPSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPSubsystemManufacturingFacility = SelectedFacility
    '                    If SelectedBPSubsystemManufacturingFacility.IsEqual(DefaultBPSubsystemManufacturingFacility) Then
    '                        SelectedBPSubsystemManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPSubsystemManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.ComponentManufacturing
    '                    PreviousFacility = CType(SelectedBPComponentManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPComponentManufacturingFacility = SelectedFacility
    '                    If SelectedBPComponentManufacturingFacility.IsEqual(DefaultBPComponentManufacturingFacility) Then
    '                        SelectedBPComponentManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPComponentManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.CapitalComponentManufacturing
    '                    PreviousFacility = CType(SelectedBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPCapitalComponentManufacturingFacility = SelectedFacility
    '                    If SelectedBPCapitalComponentManufacturingFacility.IsEqual(DefaultBPCapitalComponentManufacturingFacility) Then
    '                        SelectedBPCapitalComponentManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPCapitalComponentManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.Invention
    '                    PreviousFacility = CType(SelectedBPInventionFacility.Clone, IndustryFacility)
    '                    SelectedBPInventionFacility = SelectedFacility
    '                    If SelectedBPInventionFacility.IsEqual(DefaultBPInventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
    '                        SelectedBPInventionFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPInventionFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.T3Invention
    '                    PreviousFacility = CType(SelectedBPT3InventionFacility.Clone, IndustryFacility)
    '                    SelectedBPT3InventionFacility = SelectedFacility
    '                    If SelectedBPT3InventionFacility.IsEqual(DefaultBPT3InventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
    '                        SelectedBPT3InventionFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPT3InventionFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.Copying
    '                    PreviousFacility = CType(SelectedBPCopyFacility.Clone, IndustryFacility)
    '                    SelectedBPCopyFacility = SelectedFacility
    '                    If SelectedBPCopyFacility.IsEqual(DefaultBPCopyFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
    '                        SelectedBPCopyFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPCopyFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.NoPOSManufacturing
    '                    PreviousFacility = CType(SelectedBPNoPOSFacility.Clone, IndustryFacility)
    '                    SelectedBPNoPOSFacility = SelectedFacility
    '                    If SelectedBPNoPOSFacility.IsEqual(DefaultBPNoPOSFacility) Then
    '                        SelectedBPNoPOSFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPNoPOSFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case Else
    '                    PreviousFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
    '                    SelectedBPManufacturingFacility = SelectedFacility
    '                    If SelectedBPManufacturingFacility.IsEqual(DefaultBPManufacturingFacility) Then
    '                        SelectedBPManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedBPManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '            End Select
    '        Else
    '            Select Case ProductionType
    '                Case IndustryType.Manufacturing
    '                    SelectedCalcBaseManufacturingFacility = SelectedFacility
    '                    If SelectedCalcBaseManufacturingFacility.IsEqual(DefaultCalcBaseManufacturingFacility) Then
    '                        SelectedCalcBaseManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcBaseManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.POSFuelBlockManufacturing
    '                    SelectedCalcPOSFuelBlockFacility = SelectedFacility
    '                    If SelectedCalcPOSFuelBlockFacility.IsEqual(DefaultCalcPOSFuelBlockFacility) And DefaultCalcBaseManufacturingFacility.FacilityType = POSFacility Then
    '                        SelectedCalcPOSFuelBlockFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcPOSFuelBlockFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.POSLargeShipManufacturing
    '                    SelectedCalcPOSLargeShipFacility = SelectedFacility
    '                    If SelectedCalcPOSLargeShipFacility.IsEqual(DefaultCalcPOSLargeShipFacility) And DefaultCalcBaseManufacturingFacility.FacilityType = POSFacility Then
    '                        SelectedCalcPOSLargeShipFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcPOSLargeShipFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.POSModuleManufacturing
    '                    SelectedCalcPOSModuleFacility = SelectedFacility
    '                    If SelectedCalcPOSModuleFacility.IsEqual(DefaultCalcPOSModuleFacility) And DefaultCalcBaseManufacturingFacility.FacilityType = POSFacility Then
    '                        SelectedCalcPOSModuleFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcPOSModuleFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.BoosterManufacturing
    '                    SelectedCalcBoosterManufacturingFacility = SelectedFacility
    '                    If SelectedCalcBoosterManufacturingFacility.IsEqual(DefaultCalcBoosterManufacturingFacility) Then
    '                        SelectedCalcBoosterManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcBoosterManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.CapitalManufacturing
    '                    SelectedCalcCapitalManufacturingFacility = SelectedFacility
    '                    If SelectedCalcCapitalManufacturingFacility.IsEqual(DefaultCalcCapitalManufacturingFacility) Then
    '                        SelectedCalcCapitalManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcCapitalManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.SuperManufacturing
    '                    SelectedCalcSuperManufacturingFacility = SelectedFacility
    '                    If SelectedCalcSuperManufacturingFacility.IsEqual(DefaultCalcSuperManufacturingFacility) Then
    '                        SelectedCalcSuperManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcSuperManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.T3CruiserManufacturing
    '                    SelectedCalcT3CruiserManufacturingFacility = SelectedFacility
    '                    If SelectedCalcT3CruiserManufacturingFacility.IsEqual(DefaultCalcT3CruiserManufacturingFacility) Then
    '                        SelectedCalcT3CruiserManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcT3CruiserManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.T3DestroyerManufacturing
    '                    SelectedCalcT3DestroyerManufacturingFacility = SelectedFacility
    '                    If SelectedCalcT3DestroyerManufacturingFacility.IsEqual(DefaultCalcT3DestroyerManufacturingFacility) Then
    '                        SelectedCalcT3DestroyerManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcT3DestroyerManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.SubsystemManufacturing
    '                    SelectedCalcSubsystemManufacturingFacility = SelectedFacility
    '                    If SelectedCalcSubsystemManufacturingFacility.IsEqual(DefaultCalcSubsystemManufacturingFacility) Then
    '                        SelectedCalcSubsystemManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcSubsystemManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.ComponentManufacturing
    '                    SelectedCalcComponentManufacturingFacility = SelectedFacility
    '                    If SelectedCalcComponentManufacturingFacility.IsEqual(DefaultCalcComponentManufacturingFacility) Then
    '                        SelectedCalcComponentManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcComponentManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.CapitalComponentManufacturing
    '                    SelectedCalcCapitalComponentManufacturingFacility = SelectedFacility
    '                    If SelectedCalcCapitalComponentManufacturingFacility.IsEqual(DefaultCalcCapitalComponentManufacturingFacility) Then
    '                        SelectedCalcCapitalComponentManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcCapitalComponentManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.Invention
    '                    SelectedCalcInventionFacility = SelectedFacility
    '                    If SelectedCalcInventionFacility.IsEqual(DefaultCalcInventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
    '                        SelectedCalcInventionFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcInventionFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.T3Invention
    '                    SelectedCalcT3InventionFacility = SelectedFacility
    '                    If SelectedCalcT3InventionFacility.IsEqual(DefaultCalcT3InventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
    '                        SelectedCalcT3InventionFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcT3InventionFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.Copying
    '                    SelectedCalcCopyFacility = SelectedFacility
    '                    If SelectedCalcCopyFacility.IsEqual(DefaultCalcCopyFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
    '                        SelectedCalcCopyFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcCopyFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case IndustryType.NoPOSManufacturing
    '                    SelectedCalcNoPOSFacility = SelectedFacility
    '                    If SelectedCalcNoPOSFacility.IsEqual(DefaultCalcNoPOSFacility) Then
    '                        SelectedCalcNoPOSFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcNoPOSFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '                Case Else
    '                    SelectedCalcBaseManufacturingFacility = SelectedFacility
    '                    If SelectedCalcBaseManufacturingFacility.IsEqual(DefaultCalcBaseManufacturingFacility) Then
    '                        SelectedCalcBaseManufacturingFacility.IsDefault = True
    '                        SelectedFacility.IsDefault = True
    '                    Else
    '                        SelectedCalcBaseManufacturingFacility.IsDefault = False
    '                        SelectedFacility.IsDefault = False
    '                    End If
    '            End Select
    '        End If

    '        ' Set the default 
    '        If SelectedFacility.IsDefault = True Then 'Or (FacilityType = POSFacility And FacilityCombo.Items.Count = 1 And Tab = BPTab _
    '            'And PreviousFacility.FacilityType = SelectedFacility.FacilityType _
    '            'And PreviousFacility.SolarSystemName = SelectedFacility.SolarSystemName _
    '            'And PreviousFacility.RegionName = SelectedFacility.RegionName _
    '            'And PreviousFacility.IncludeActivityUsage = SelectedFacility.IncludeActivityUsage) Then
    '            FacilityDefaultLabel.ForeColor = SystemColors.Highlight
    '            Call ResetToolTipforDefaultFacilityLabel(FacilityDefaultLabel, False, ToolTipRef)
    '            FacilitySaveButton.Enabled = False ' don't enable since it's already the default, it's pointless to save it
    '        Else
    '            FacilityDefaultLabel.ForeColor = SystemColors.ButtonShadow
    '            Call ResetToolTipforDefaultFacilityLabel(FacilityDefaultLabel, True, ToolTipRef)
    '            FacilitySaveButton.Enabled = True
    '        End If

    '    End Sub

    '    ' Returns the SQL string for querying by category or group id's 
    '    Public Function GetFacilityCatGroupIDSQL(ByVal CategoryID As Integer, ByVal GroupID As Integer, ByVal Activity As IndustryActivities) As String
    '        Dim SQL As String = ""
    '        Dim TempGroupID As Integer
    '        Dim TempCategoryID As Integer

    '        ' If the categoryID or groupID is for T3 invention, then switch the item ID's to the blueprint groupID for that item to match CCP's logic in table
    '        If Activity = IndustryActivities.Invention Then
    '            If CategoryID = SubsystemCategoryID Then
    '                TempGroupID = SubsystemBPGroupID
    '                TempCategoryID = 0
    '            ElseIf GroupID = StrategicCruiserGroupID Then
    '                TempGroupID = StrategicCruiserBPGroupID
    '                TempCategoryID = 0
    '            ElseIf GroupID = TacticalDestroyerGroupID Then
    '                TempGroupID = TacticalDestroyerBPGroupID
    '                TempCategoryID = 0
    '            Else
    '                TempGroupID = GroupID
    '                TempCategoryID = CategoryID
    '            End If
    '        Else
    '            TempGroupID = GroupID
    '            TempCategoryID = CategoryID
    '        End If

    '        SQL = "AND (GROUP_ID = " & CStr(TempGroupID) & " OR (GROUP_ID = 0 AND CATEGORY_ID = " & CStr(TempCategoryID) & ")) "

    '        Return SQL

    '    End Function

    '    ' Hides all the facility bonus boxes and such
    '    Public Sub SetFacilityBonusBoxes(ByRef TextME As TextBox, ByRef TextTE As TextBox, ByRef TextTax As TextBox,
    '                                      ByRef LabelME As Label, ByRef LabelTE As Label, ByRef LabelTax As Label,
    '                                      ByVal Value As Boolean,
    '                                      Optional ByRef UsageLabel As Label = Nothing)

    '        TextME.Visible = Value
    '        TextTE.Visible = Value
    '        TextTax.Visible = Value

    '        LabelME.Visible = Value
    '        LabelTE.Visible = Value
    '        LabelTax.Visible = Value

    '        ' Clear the usage until these are set
    '        If Not IsNothing(UsageLabel) Then
    '            UsageLabel.Text = ""
    '        End If

    '    End Sub

    '    ' Sets all the combos to unenabled and base text to show no facility for stuff like Invention, Copy and RE where they might buy the item
    '    Public Sub SetNoFacility(ByRef RegionCombo As ComboBox, ByRef SystemCombo As ComboBox, ByRef FacilityorArray As ComboBox,
    '                              ByRef CheckUsage As CheckBox, Optional IncludeCostCheck As CheckBox = Nothing,
    '                              Optional IncludeTimeCheck As CheckBox = Nothing, Optional IncludeLabel As Label = Nothing)
    '        RegionCombo.Items.Clear()
    '        RegionCombo.Text = "Select Region"
    '        RegionCombo.Enabled = False
    '        SystemCombo.Items.Clear()
    '        SystemCombo.Text = "Select System"
    '        SystemCombo.Enabled = False
    '        FacilityorArray.Items.Clear()
    '        FacilityorArray.Text = "Select Facility / Array"
    '        CheckUsage.Enabled = False

    '        If Not IsNothing(IncludeCostCheck) Then
    '            IncludeCostCheck.Enabled = False
    '        End If
    '        If Not IsNothing(IncludeTimeCheck) Then
    '            IncludeTimeCheck.Enabled = False
    '        End If
    '        FacilityorArray.Enabled = False
    '        If Not IsNothing(IncludeLabel) Then
    '            IncludeLabel.Enabled = False
    '        End If
    '    End Sub

    '    ' Sets the default based on the cost check change
    '    Public Sub SetDefaultFacilitybyCheck(ProductionType As IndustryType, IncludeUsageCheck As CheckBox, Tab As String,
    '                                          FacilityType As String, FacilityArrayCombo As ComboBox, FacilityDefaultLabel As Label,
    '                                          FacilitySaveButton As Button, Optional IncludeCostCheck As CheckBox = Nothing,
    '                                          Optional IncludeTimeCheck As CheckBox = Nothing, Optional ToolTipRef As ToolTip = Nothing)
    '        Dim SelectedFacility As IndustryFacility
    '        Dim CompareTime As Boolean = False
    '        Dim CompareCost As Boolean = False

    '        If Tab = BPTab Then
    '            Select Case ProductionType
    '                Case IndustryType.Manufacturing
    '                    SelectedFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.POSFuelBlockManufacturing
    '                    SelectedFacility = CType(SelectedBPPOSFuelBlockFacility, IndustryFacility)
    '                Case IndustryType.POSLargeShipManufacturing
    '                    SelectedFacility = CType(SelectedBPPOSLargeShipFacility, IndustryFacility)
    '                Case IndustryType.POSModuleManufacturing
    '                    SelectedFacility = CType(SelectedBPPOSModuleFacility, IndustryFacility)
    '                Case IndustryType.BoosterManufacturing
    '                    SelectedFacility = CType(SelectedBPBoosterManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.CapitalManufacturing
    '                    SelectedFacility = CType(SelectedBPCapitalManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.SuperManufacturing
    '                    SelectedFacility = CType(SelectedBPSuperManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.T3CruiserManufacturing
    '                    SelectedFacility = CType(SelectedBPT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.T3DestroyerManufacturing
    '                    SelectedFacility = CType(SelectedBPT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.SubsystemManufacturing
    '                    SelectedFacility = CType(SelectedBPSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.ComponentManufacturing
    '                    SelectedFacility = CType(SelectedBPComponentManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.CapitalComponentManufacturing
    '                    SelectedFacility = CType(SelectedBPCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.Invention
    '                    SelectedFacility = CType(SelectedBPInventionFacility, IndustryFacility)
    '                Case IndustryType.T3Invention
    '                    SelectedFacility = CType(SelectedBPT3InventionFacility, IndustryFacility)
    '                Case IndustryType.Copying
    '                    SelectedFacility = CType(SelectedBPCopyFacility, IndustryFacility)
    '                Case IndustryType.NoPOSManufacturing
    '                    SelectedFacility = CType(SelectedBPNoPOSFacility, IndustryFacility)
    '                Case Else
    '                    SelectedFacility = CType(SelectedBPManufacturingFacility.Clone, IndustryFacility)
    '            End Select
    '        Else
    '            Select Case ProductionType
    '                Case IndustryType.Manufacturing
    '                    SelectedFacility = CType(SelectedCalcBaseManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.POSFuelBlockManufacturing
    '                    SelectedFacility = CType(SelectedCalcPOSFuelBlockFacility, IndustryFacility)
    '                Case IndustryType.POSLargeShipManufacturing
    '                    SelectedFacility = CType(SelectedCalcPOSLargeShipFacility, IndustryFacility)
    '                Case IndustryType.POSModuleManufacturing
    '                    SelectedFacility = CType(SelectedCalcPOSModuleFacility, IndustryFacility)
    '                Case IndustryType.BoosterManufacturing
    '                    SelectedFacility = CType(SelectedCalcBoosterManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.CapitalManufacturing
    '                    SelectedFacility = CType(SelectedCalcCapitalManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.SuperManufacturing
    '                    SelectedFacility = CType(SelectedCalcSuperManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.T3CruiserManufacturing
    '                    SelectedFacility = CType(SelectedCalcT3CruiserManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.T3DestroyerManufacturing
    '                    SelectedFacility = CType(SelectedCalcT3DestroyerManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.SubsystemManufacturing
    '                    SelectedFacility = CType(SelectedCalcSubsystemManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.CapitalComponentManufacturing
    '                    SelectedFacility = CType(SelectedCalcCapitalComponentManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.ComponentManufacturing
    '                    SelectedFacility = CType(SelectedCalcComponentManufacturingFacility.Clone, IndustryFacility)
    '                Case IndustryType.Invention
    '                    SelectedFacility = CType(SelectedCalcInventionFacility, IndustryFacility)
    '                Case IndustryType.T3Invention
    '                    SelectedFacility = CType(SelectedCalcT3InventionFacility, IndustryFacility)
    '                Case IndustryType.Copying
    '                    SelectedFacility = CType(SelectedCalcCopyFacility, IndustryFacility)
    '                Case IndustryType.NoPOSManufacturing
    '                    SelectedFacility = CType(SelectedCalcNoPOSFacility, IndustryFacility)
    '                Case Else
    '                    SelectedFacility = CType(SelectedCalcBaseManufacturingFacility.Clone, IndustryFacility)
    '            End Select
    '        End If

    '        SelectedFacility.IncludeActivityUsage = IncludeUsageCheck.Checked

    '        If Not IsNothing(IncludeCostCheck) Then
    '            SelectedFacility.IncludeActivityCost = IncludeCostCheck.Checked
    '            CompareCost = True
    '        Else
    '            CompareCost = False
    '        End If

    '        If Not IsNothing(IncludeTimeCheck) Then
    '            SelectedFacility.IncludeActivityTime = IncludeTimeCheck.Checked
    '            CompareTime = True
    '        Else
    '            ' Don't compare this value
    '            CompareTime = False
    '        End If

    '        ' Set the default based on the checkbox 
    '        Call SetFacilityandDefault(SelectedFacility, ProductionType, Tab, FacilityType, FacilityArrayCombo,
    '                                   FacilityDefaultLabel, FacilitySaveButton, CompareCost, CompareTime, ToolTipRef)

    '    End Sub

    '    ' Sets the tool tip text for default facility labels if they can double click to reload
    '    Public Sub ResetToolTipforDefaultFacilityLabel(ByRef FacilityDefaultLabel As Label, ByVal ShowTip As Boolean, ByRef ToolTipRef As ToolTip)
    '        If Not IsNothing(ToolTipRef) Then
    '            If ShowTip And UserApplicationSettings.ShowToolTips Then
    '                ToolTipRef.SetToolTip(FacilityDefaultLabel, "Double-Click to reload default facility")
    '            Else
    '                ToolTipRef.SetToolTip(FacilityDefaultLabel, "")
    '            End If
    '        End If
    '    End Sub

    '#End Region

End Class

' What type of view are we looking at
Public Enum FacilityView
    FullControls = 0 ' for BP tab right now
    LimitedControls = 1 ' for use on manufacturing tab now
End Enum

' Types of actual activities that you can conduct in a facility
Public Enum IndustryActivities
    None = 0
    Manufacturing = 1
    ResearchingTechnology = 2
    ResearchingTimeLevel = 3
    ResearchingMaterialLevel = 4
    Copying = 5
    Duplicating = 6
    Invention = 8
End Enum

' These are the different types of industry fuction we will distinguish between facilities since they all have special restrictions
Public Enum ProductionType
    None = 0
    Manufacturing = 1
    ComponentManufacturing = 2
    CapitalComponentManufacturing = 3
    CapitalManufacturing = 4
    SuperManufacturing = 5
    T3CruiserManufacturing = 6
    SubsystemManufacturing = 7
    BoosterManufacturing = 8
    Copying = 9
    Invention = 10
    NoPOSManufacturing = 11
    T3Invention = 12
    T3DestroyerManufacturing = 13

    ' Special POS Arrays
    POSModuleManufacturing = 14
    POSFuelBlockManufacturing = 15
    POSLargeShipManufacturing = 16

End Enum

' Move these into the class when it's complete
Public Module FacilityVariables

#Region "Facility Variables"
    ' All locally saved facility variables will be here
    Public SelectedBPManufacturingFacility As New IndustryFacility
    Public SelectedBPComponentManufacturingFacility As New IndustryFacility
    Public SelectedBPCapitalComponentManufacturingFacility As New IndustryFacility
    Public SelectedBPCapitalManufacturingFacility As New IndustryFacility
    Public SelectedBPSuperManufacturingFacility As New IndustryFacility
    Public SelectedBPT3CruiserManufacturingFacility As New IndustryFacility
    Public SelectedBPT3DestroyerManufacturingFacility As New IndustryFacility
    Public SelectedBPSubsystemManufacturingFacility As New IndustryFacility
    Public SelectedBPBoosterManufacturingFacility As New IndustryFacility
    Public SelectedBPInventionFacility As New IndustryFacility
    Public SelectedBPT3InventionFacility As New IndustryFacility
    Public SelectedBPCopyFacility As New IndustryFacility
    Public SelectedBPNoPOSFacility As New IndustryFacility

    ' Special cases for POS Facilities where items can be produced at more than one array
    Public SelectedBPPOSFuelBlockFacility As New IndustryFacility
    Public SelectedBPPOSLargeShipFacility As New IndustryFacility
    Public SelectedBPPOSModuleFacility As New IndustryFacility

    ' Selected manufacturing tab facilities
    Public SelectedCalcBaseManufacturingFacility As New IndustryFacility
    Public SelectedCalcComponentManufacturingFacility As New IndustryFacility
    Public SelectedCalcCapitalComponentManufacturingFacility As New IndustryFacility
    Public SelectedCalcCapitalManufacturingFacility As New IndustryFacility
    Public SelectedCalcSuperManufacturingFacility As New IndustryFacility
    Public SelectedCalcT3CruiserManufacturingFacility As New IndustryFacility
    Public SelectedCalcT3DestroyerManufacturingFacility As New IndustryFacility
    Public SelectedCalcSubsystemManufacturingFacility As New IndustryFacility
    Public SelectedCalcBoosterManufacturingFacility As New IndustryFacility
    Public SelectedCalcInventionFacility As New IndustryFacility
    Public SelectedCalcT3InventionFacility As New IndustryFacility
    Public SelectedCalcCopyFacility As New IndustryFacility
    Public SelectedCalcNoPOSFacility As New IndustryFacility

    ' Special cases for POS Facilities where items can be produced at more than one array
    Public SelectedCalcPOSFuelBlockFacility As New IndustryFacility
    Public SelectedCalcPOSLargeShipFacility As New IndustryFacility
    Public SelectedCalcPOSModuleFacility As New IndustryFacility

    ' Save the default data for checking if the selected facility is a default and quick reference
    Public DefaultBPPOSFuelBlockFacility As New IndustryFacility
    Public DefaultBPPOSLargeShipFacility As New IndustryFacility
    Public DefaultBPPOSModuleFacility As New IndustryFacility

    Public DefaultBPManufacturingFacility As New IndustryFacility
    Public DefaultBPComponentManufacturingFacility As New IndustryFacility
    Public DefaultBPCapitalComponentManufacturingFacility As New IndustryFacility
    Public DefaultBPCapitalManufacturingFacility As New IndustryFacility
    Public DefaultBPSuperManufacturingFacility As New IndustryFacility
    Public DefaultBPT3CruiserManufacturingFacility As New IndustryFacility
    Public DefaultBPT3DestroyerManufacturingFacility As New IndustryFacility
    Public DefaultBPSubsystemManufacturingFacility As New IndustryFacility
    Public DefaultBPBoosterManufacturingFacility As New IndustryFacility
    Public DefaultBPInventionFacility As New IndustryFacility
    Public DefaultBPT3InventionFacility As New IndustryFacility
    Public DefaultBPCopyFacility As New IndustryFacility
    Public DefaultBPNoPOSFacility As New IndustryFacility

    Public DefaultCalcBaseManufacturingFacility As New IndustryFacility
    Public DefaultCalcComponentManufacturingFacility As New IndustryFacility
    Public DefaultCalcCapitalComponentManufacturingFacility As New IndustryFacility
    Public DefaultCalcCapitalManufacturingFacility As New IndustryFacility
    Public DefaultCalcSuperManufacturingFacility As New IndustryFacility
    Public DefaultCalcT3CruiserManufacturingFacility As New IndustryFacility
    Public DefaultCalcT3DestroyerManufacturingFacility As New IndustryFacility
    Public DefaultCalcSubsystemManufacturingFacility As New IndustryFacility
    Public DefaultCalcBoosterManufacturingFacility As New IndustryFacility
    Public DefaultCalcInventionFacility As New IndustryFacility
    Public DefaultCalcT3InventionFacility As New IndustryFacility
    Public DefaultCalcCopyFacility As New IndustryFacility
    Public DefaultCalcNoPOSFacility As New IndustryFacility

    Public DefaultCalcPOSFuelBlockFacility As New IndustryFacility
    Public DefaultCalcPOSLargeShipFacility As New IndustryFacility
    Public DefaultCalcPOSModuleFacility As New IndustryFacility

#End Region
End Module

Public Class IndustryFacility
    Implements ICloneable

    ' For industry Facilities
    Public FacilityID As Long ' ID Of the facility
    Public FacilityName As String ' Station/Outpost Name or the Array name
    Public FacilityType As String ' POS, Station, Outpost
    Public FacilityTypeID As Long ' type ID for facility - type of outpost, etc
    Public FacilityProductionType As ProductionType ' What we are doing at this facility
    Public FacilityProductionTypeDescription As String ' Text version of what the production type is
    Public ActivityID As Integer ' Activity code of the facility (remove)
    Public RegionName As String ' Region of this facility
    Public RegionID As Long
    Public SolarSystemName As String ' System where this is located
    Public SolarSystemID As Long
    Public FWUpgradeLevel As Integer ' Level of the FW upgrade for this system (if applies)
    Public CostIndex As Double ' Cost index for the system and activity from CREST
    Public ActivityCostPerSecond As Double ' The cost to conduct the activity for this facility per second - my setting for POS and ECs
    Public InstalledModules As List(Of Integer) ' For EC use, to list all items installed on the structure
    Public IsDefault As Boolean
    Public IncludeActivityCost As Boolean ' This is the total cost of materials to do the activiy
    Public IncludeActivityTime As Boolean ' This is the time for doing the activity
    Public IncludeActivityUsage As Boolean ' This is the cost of using the facility only

    ' Nullable fields
    Public TaxRate As Double ' The tax rate
    ' Remove these eventually when outposts and pos removed
    Public MaterialMultiplier As Double ' The bonus material percentage for materials used in this facility
    Public TimeMultiplier As Double ' The bonus to time to conduct an activity in this facility
    Public CostMultiplier As Double ' The bonus to cost to conduct an activity in this facility

    Public Sub New()

        FacilityID = 0
        FacilityName = None
        FacilityType = None
        FacilityProductionType = ProductionType.None
        ActivityID = 0
        RegionName = None
        RegionID = 0
        SolarSystemName = None
        SolarSystemID = 0
        FWUpgradeLevel = 0
        TaxRate = 0
        MaterialMultiplier = 0
        TimeMultiplier = 0
        CostMultiplier = 0
        ActivityCostPerSecond = 0
        InstalledModules = New List(Of Integer)

        IncludeActivityCost = False
        IncludeActivityTime = False
        IncludeActivityUsage = False

        IsDefault = False

    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New IndustryFacility

        CopyOfMe.FacilityName = FacilityName
        CopyOfMe.FacilityType = FacilityType
        CopyOfMe.FacilityTypeID = FacilityTypeID
        CopyOfMe.FacilityProductionType = FacilityProductionType
        CopyOfMe.ActivityID = ActivityID
        CopyOfMe.RegionName = RegionName
        CopyOfMe.RegionID = RegionID
        CopyOfMe.SolarSystemName = SolarSystemName
        CopyOfMe.SolarSystemID = SolarSystemID
        CopyOfMe.FWUpgradeLevel = FWUpgradeLevel
        CopyOfMe.TaxRate = TaxRate
        CopyOfMe.MaterialMultiplier = MaterialMultiplier
        CopyOfMe.TimeMultiplier = TimeMultiplier
        CopyOfMe.CostMultiplier = CostMultiplier
        CopyOfMe.CostIndex = CostIndex
        CopyOfMe.ActivityCostPerSecond = ActivityCostPerSecond
        CopyOfMe.InstalledModules = InstalledModules
        CopyOfMe.IsDefault = IsDefault
        CopyOfMe.IncludeActivityCost = IncludeActivityCost
        CopyOfMe.IncludeActivityTime = IncludeActivityTime
        CopyOfMe.IncludeActivityUsage = IncludeActivityUsage

        Return CopyOfMe

    End Function

    ' Load up the facility data from the table as default
    Public Sub InitalizeFacility(InitalProductionType As ProductionType, FacilityTab As FacilityView)
        Dim SQL As String = ""
        Dim TempSQL As String = ""
        Dim rsLoader As SQLiteDataReader

        '  Dim SearchFacilitySettings As FacilitySettings
        '  Dim Defaults As New ProgramSettings

        ' Look up all the data in one query except the multipliers, which will be removed after outpost and pos changes anyway
        SQL = String.Format("SELECT SF.FACILITY_ID, 
                            CASE WHEN STF.FACILITY_NAME IS NOT NULL THEN STF.FACILITY_NAME ELSE AAS.ARRAY_NAME END AS FACILITY_NAME,
                            FACILITY_TYPE_NAME, SF.FACILITY_TYPE_ID, FACILITY_PRODUCTION_TYPES.DESCRIPTION, FACILITY_PRODUCTION_TYPES.ACTIVITY_ID,
                            REGIONNAME, REGIONS.REGIONID, SOLARSYSTEMNAME, SOLARSYSTEMID, 
                            CASE WHEN STF.FACILITY_TAX IS NOT NULL THEN STF.FACILITY_TAX ELSE 
                            CASE WHEN SF.FACILITY_TAX IS NOT NULL THEN SF.FACILITY_TAX ELSE 0 END END AS TAX,
                            CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL, 
                            CASE WHEN COST_INDEX IS NULL THEN 0 ELSE COST_INDEX END AS COST_INDEX,
                            ACTIVITY_COST_PER_SECOND, INCLUDE_ACTIVITY_COST, INCLUDE_ACTIVITY_TIME, INCLUDE_ACTIVITY_USAGE,
                            MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER
                            FROM SAVED_FACILITIES AS SF, FACILITY_PRODUCTION_TYPES, REGIONS, SOLAR_SYSTEMS, FACILITY_TYPES
                            LEFT JOIN (SELECT DISTINCT FACILITY_ID, FACILITY_NAME, FACILITY_TAX FROM STATION_FACILITIES) AS STF ON SF.FACILITY_ID = STF.FACILITY_ID
                            LEFT JOIN (SELECT DISTINCT ARRAY_TYPE_ID, ARRAY_NAME FROM ASSEMBLY_ARRAYS) AS AAS ON SF.FACILITY_ID = AAS.ARRAY_TYPE_ID
                            LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID
                            LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON SF.SOLAR_SYSTEM_ID = INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID
                            AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = FACILITY_PRODUCTION_TYPES.ACTIVITY_ID
                            WHERE SF.PRODUCTION_TYPE = FACILITY_PRODUCTION_TYPES.PRODUCTION_TYPE
                            AND SF.REGION_ID = REGIONS.regionID
                            AND SF.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID
                            AND SF.FACILITY_TYPE = FACILITY_TYPES.FACILITY_TYPE_ID
                            AND SF.PRODUCTION_TYPE = {0} AND SF.FACILITY_VIEW = {1} ", CStr(InitalProductionType), CStr(FacilityTab))

        ' First look up the character to see if it's saved there first (initially only do one set of facilities then allow by character via a setting)
        DBCommand = New SQLiteCommand(SQL & "AND CHARACTER_ID <> 0", EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader
        rsLoader.Read()

        If Not rsLoader.HasRows Then
            ' Need to look up the default
            rsLoader.Close()
            DBCommand = New SQLiteCommand(SQL & "AND CHARACTER_ID = 0", EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            rsLoader.Read()
        End If

        ' Should have data one way or another now
        If rsLoader.HasRows Then
            With rsLoader
                FacilityID = .GetInt32(0)
                FacilityName = .GetString(1)
                FacilityType = .GetString(2)
                FacilityTypeID = .GetInt32(3)
                FacilityProductionType = InitalProductionType
                FacilityProductionTypeDescription = .GetString(4)
                ActivityID = .GetInt32(5)
                RegionName = .GetString(6)
                RegionID = .GetInt64(7)
                SolarSystemName = .GetString(8)
                SolarSystemID = .GetInt64(9)
                TaxRate = .GetDouble(10)
                FWUpgradeLevel = .GetInt32(11)
                CostIndex = .GetFloat(12)
                ActivityCostPerSecond = .GetFloat(13)
                IncludeActivityCost = CBool(.GetInt32(14))
                IncludeActivityTime = CBool(.GetInt32(15))
                IncludeActivityUsage = CBool(.GetInt32(16))



                'MaterialMultiplier = rsLoader.GetDouble(7)
                'TimeMultiplier = rsLoader.GetDouble(8)
                'CostMultiplier = rsLoader.GetDouble(9)

                ' If this is a citadel, then look up any saved modules
                If FacilityType = "Citadel" Then
                    'Public InstalledModules As List(Of Integer) ' For EC use, to list all items installed on the structure
                End If

                IsDefault = True ' Always loading default with initialize

            End With

            rsLoader.Close()
            rsLoader = Nothing
            DBCommand = Nothing

        Else
            ' Something went wrong
            MsgBox("The facility failed to load", vbCritical, Application.ProductName)
            Exit Sub
        End If


        '' When loading a POS Facility with multi-use or Component array, which allows 'All' then do special processing
        'With SearchFacilitySettings
        '    If .FacilityType = POSFacility And (.Facility = "All" Or .Facility = "Component" Or .Facility = "Large" _
        '                                        Or .Facility = "Equipment") Then
        '        ' Set the name to what was sent, save the sent info, and exit. This will only happen with the manufacturing tab section
        '        Sql = "SELECT '" & .Facility & "' AS FACILITY_NAME, "
        '        ' Need to load location from the settings since the location is specific to the user
        '        Sql = Sql & "'" & .RegionName & "' AS REGION_NAME, " & CStr(.RegionID) & " AS REGION_ID, '"
        '        Sql = Sql & .SolarSystemName & "' AS SOLAR_SYSTEM_NAME, " & CStr(.SolarSystemID) & " AS SSID, " & CStr(POSTaxRate) & " AS FACILITY_TAX, COST_INDEX, "
        '        Sql = Sql & "MATERIAL_MULTIPLIER AS MATERIAL_MULTIPLIER, TIME_MULTIPLIER AS TIME_MULTIPLIER, "
        '        Sql = Sql & "ASSEMBLY_ARRAYS.ACTIVITY_ID AS AID, ARRAY_TYPE_ID AS FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
        '        Sql = Sql & "FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID "
        '        Sql = Sql & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
        '        Sql = Sql & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
        '        Sql = Sql & "AND AID = " & CStr(.ActivityID) & " "

        '        If .Facility <> "All" Then
        '            ' Must be a fuel block, module, or large ship array choice
        '            Select Case .Facility
        '                Case "Component"
        '                    Sql = Sql & "AND ARRAY_NAME LIKE 'Component%' AND GROUP_ID = " & FuelBlockGroupID
        '                Case "Large"
        '                    Sql = Sql & "AND ARRAY_NAME LIKE 'Large%' AND GROUP_ID = " & BattleshipGroupID
        '                Case "Equipment"
        '                    Sql = Sql & "AND ARRAY_NAME LIKE 'Equipment%' AND CATEGORY_ID = " & ModuleCategoryID
        '            End Select
        '        End If

        '        DBCommand = New SQLiteCommand(Sql, EVEDB.DBREf)
        '        rsLoader = DBCommand.ExecuteReader
        '        rsLoader.Read()

        '        If rsLoader.HasRows Then
        '            FacilityType = .FacilityType
        '            FacilityName = rsLoader.GetString(0)
        '            ProductionType = .ProductionType
        '            RegionName = rsLoader.GetString(1)
        '            RegionID = rsLoader.GetInt64(2)
        '            SolarSystemName = rsLoader.GetString(3)
        '            SolarSystemID = rsLoader.GetInt64(4)
        '            TaxRate = rsLoader.GetDouble(5)
        '            CostIndex = rsLoader.GetFloat(6)
        '            MaterialMultiplier = rsLoader.GetDouble(7)
        '            TimeMultiplier = rsLoader.GetDouble(8)
        '            ActivityID = rsLoader.GetInt32(9)
        '            ActivityCostPerSecond = .ActivityCostperSecond
        '            IsDefault = FacilityDefault
        '            FacilityTypeID = rsLoader.GetInt64(10)

        '            IncludeActivityCost = .IncludeActivityCost
        '            IncludeActivityTime = .IncludeActivityTime
        '            IncludeActivityUsage = .IncludeActivityUsage

        '            FWUpgradeLevel = rsLoader.GetInt32(13)

        '            rsLoader.Close()
        '            rsLoader = Nothing
        '            DBCommand = Nothing

        '        End If

        '        ' We set this, now leave
        '        Exit Sub

        '    End If
        'End With

        'Dim UseDBData As Boolean = True

        'With SearchFacilitySettings
        '    Select Case .FacilityType
        '        Case POSFacility
        '            Sql = "SELECT ARRAY_NAME AS FACILITY_NAME, "
        '            ' Need to load location from the settings since the location is specific to the user
        '            Sql = Sql & "'" & .RegionName & "' AS REGION_NAME, " & CStr(.RegionID) & " AS REGION_ID, '"
        '            Sql = Sql & .SolarSystemName & "' AS SOLAR_SYSTEM_NAME, " & CStr(.SolarSystemID) & " AS SSID, " & CStr(POSTaxRate) & " AS FACILITY_TAX, COST_INDEX, "
        '            Sql = Sql & "MATERIAL_MULTIPLIER AS MATERIAL_MULTIPLIER, TIME_MULTIPLIER AS TIME_MULTIPLIER, "
        '            Sql = Sql & "ASSEMBLY_ARRAYS.ACTIVITY_ID AS AID, ARRAY_TYPE_ID AS FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
        '            Sql = Sql & "FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID "
        '            Sql = Sql & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
        '            Sql = Sql & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "

        '        Case OutpostFacility
        '            Sql = "SELECT FACILITY_NAME, REGION_NAME, REGION_ID, "
        '            Sql = Sql & "SOLAR_SYSTEM_NAME, STATION_FACILITIES.SOLAR_SYSTEM_ID AS SSID, FACILITY_TAX, COST_INDEX, "
        '            ' Check the values sent to see if they set it to something instead of loading from DB
        '            If .MaterialMultiplier <> Defaults.FacilityDefaultMM And .MaterialMultiplier <> 0 Then
        '                ' They didn't set a value, so load the default for each type
        '                Sql = Sql & CStr(.MaterialMultiplier) & " AS MATERIAL_MULTIPLIER, "
        '                UseDBData = False
        '            Else
        '                Sql = Sql & "MATERIAL_MULTIPLIER, "
        '            End If

        '            If .TimeMultiplier <> Defaults.FacilityDefaultTM And .TimeMultiplier <> 0 Then
        '                ' They didn't set a value, so load the default for each type
        '                Sql = Sql & CStr(.TimeMultiplier) & " AS TIME_MULTIPLIER, "
        '                UseDBData = False
        '            Else
        '                Sql = Sql & "TIME_MULTIPLIER, "
        '            End If
        '            Sql = Sql & "ACTIVITY_ID AS AID, FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
        '            Sql = Sql & "FROM STATION_FACILITIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = STATION_FACILITIES.SOLAR_SYSTEM_ID "
        '            Sql = Sql & "WHERE OUTPOST = " & CStr(StationType.Outpost) & " "
        '        Case StationFacility
        '            Sql = "SELECT FACILITY_NAME, REGION_NAME, REGION_ID, "
        '            Sql = Sql & "SOLAR_SYSTEM_NAME, STATION_FACILITIES.SOLAR_SYSTEM_ID AS SSID, FACILITY_TAX, COST_INDEX, "
        '            Sql = Sql & "MATERIAL_MULTIPLIER, TIME_MULTIPLIER, "
        '            Sql = Sql & "ACTIVITY_ID AS AID, FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID, CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL "
        '            Sql = Sql & "FROM STATION_FACILITIES LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = STATION_FACILITIES.SOLAR_SYSTEM_ID "
        '            Sql = Sql & "WHERE OUTPOST = " & CStr(StationType.Station) & " "
        '    End Select

        '    Sql = Sql & "AND FACILITY_NAME = '" & FormatDBString(SearchFacilitySettings.Facility) & "' "
        '    Sql = Sql & "AND AID = " & CStr(.ActivityID) & " "

        '    If .FacilityType = POSFacility Then
        '        Select Case .ProductionType
        '            Case IndustryType.CapitalManufacturing
        '                Sql = Sql & "AND GROUP_ID IN (" & CStr(CapitalIndustrialShipGroupID) & ", " & CStr(CarrierGroupID) & ", " & CStr(DreadnoughtGroupID) & ", " & CStr(FAXGroupID) & ") "
        '            Case IndustryType.SuperManufacturing
        '                Sql = Sql & "AND GROUP_ID IN (" & CStr(TitanGroupID) & ", " & CStr(SupercarrierGroupID) & ") "
        '            Case IndustryType.BoosterManufacturing
        '                Sql = Sql & "AND GROUP_ID = " & BoosterGroupID & " "
        '            Case IndustryType.T3CruiserManufacturing
        '                Sql = Sql & "AND GROUP_ID = " & StrategicCruiserGroupID & " "
        '            Case IndustryType.SubsystemManufacturing
        '                Sql = Sql & "AND CATEGORY_ID = " & SubsystemCategoryID & " "
        '            Case IndustryType.ComponentManufacturing
        '                Sql = Sql & "AND GROUP_ID = " & ConstructionComponentsGroupID & " "
        '            Case IndustryType.CapitalComponentManufacturing
        '                Sql = Sql & "AND GROUP_ID IN (" & CStr(AdvCapitalComponentGroupID) & ", " & CStr(CapitalComponentGroupID) & ") "
        '        End Select
        '    Else ' Stations
        '        Select Case .ProductionType
        '            Case IndustryType.CapitalManufacturing
        '                Sql = Sql & "AND GROUP_ID IN (" & CStr(CapitalIndustrialShipGroupID) & ", " & CStr(CarrierGroupID) & ", " & CStr(DreadnoughtGroupID) & ", " & CStr(FAXGroupID) & ") "
        '            Case IndustryType.SuperManufacturing
        '                Sql = Sql & "AND GROUP_ID IN (" & CStr(TitanGroupID) & ", " & CStr(SupercarrierGroupID) & ") "
        '            Case IndustryType.BoosterManufacturing
        '                Sql = Sql & "AND GROUP_ID = " & BoosterGroupID & " "
        '            Case IndustryType.T3CruiserManufacturing
        '                Sql = Sql & "AND GROUP_ID = " & StrategicCruiserGroupID & " "
        '            Case IndustryType.SubsystemManufacturing
        '                Sql = Sql & "AND CATEGORY_ID = " & SubsystemCategoryID & " "
        '            Case IndustryType.ComponentManufacturing, IndustryType.CapitalComponentManufacturing
        '                Sql = Sql & "AND CATEGORY_ID = " & ComponentCategoryID & " "
        '        End Select
        '    End If

        '    Sql = Sql & "GROUP BY FACILITY_NAME, REGION_NAME, REGION_ID, SOLAR_SYSTEM_NAME, SSID, FACILITY_TAX, "
        '    Sql = Sql & "COST_INDEX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, AID, FACILITY_TYPE_ID, FW_UPGRADE_LEVEL"

        'End With

        '' Set the query
        'DBCommand = New SQLiteCommand(Sql, EVEDB.DBREf)
        'rsLoader = DBCommand.ExecuteReader
        'rsLoader.Read()

        'If rsLoader.HasRows Then
        '    FacilityType = SearchFacilitySettings.FacilityType
        '    FacilityName = rsLoader.GetString(0)
        '    ProductionType = SearchFacilitySettings.ProductionType
        '    RegionName = rsLoader.GetString(1)
        '    RegionID = rsLoader.GetInt64(2)
        '    CostIndex = rsLoader.GetFloat(6)
        '    SolarSystemName = rsLoader.GetString(3) & " (" & FormatNumber(CostIndex, 3) & ")"
        '    SolarSystemID = rsLoader.GetInt64(4)
        '    If SearchFacilitySettings.FacilityType = OutpostFacility And Not UseDBData Then
        '        MaterialMultiplier = SearchFacilitySettings.MaterialMultiplier
        '        TimeMultiplier = SearchFacilitySettings.TimeMultiplier
        '        TaxRate = SearchFacilitySettings.TaxRate
        '    Else
        '        MaterialMultiplier = rsLoader.GetDouble(7)
        '        TimeMultiplier = rsLoader.GetDouble(8)
        '        TaxRate = rsLoader.GetDouble(5)
        '    End If
        '    ActivityID = rsLoader.GetInt32(9)
        '    ActivityCostPerSecond = SearchFacilitySettings.ActivityCostperSecond
        '    IsDefault = FacilityDefault
        '    FacilityTypeID = rsLoader.GetInt64(10)

        '    IncludeActivityCost = SearchFacilitySettings.IncludeActivityCost
        '    IncludeActivityTime = SearchFacilitySettings.IncludeActivityTime
        '    IncludeActivityUsage = SearchFacilitySettings.IncludeActivityUsage

        '    FWUpgradeLevel = rsLoader.GetInt32(13)

        '    rsLoader.Close()
        '    rsLoader = Nothing
        '    DBCommand = Nothing

        '    ' It loaded, so leave function
        '    Exit Sub

        'End If

        '' Just use everything we can from the search settings and set the others to defaults
        'FacilityType = SearchFacilitySettings.FacilityType
        'FacilityName = SearchFacilitySettings.Facility
        'ProductionType = SearchFacilitySettings.ProductionType
        'RegionName = SearchFacilitySettings.RegionName
        'RegionID = SearchFacilitySettings.RegionID
        'SolarSystemName = SearchFacilitySettings.SolarSystemName & " (0.000)"
        'SolarSystemID = SearchFacilitySettings.SolarSystemID
        'FWUpgradeLevel = 0
        'TaxRate = 0
        'CostIndex = 0
        'MaterialMultiplier = SearchFacilitySettings.MaterialMultiplier
        'TimeMultiplier = SearchFacilitySettings.TimeMultiplier
        'ActivityID = SearchFacilitySettings.ActivityID
        'ActivityCostPerSecond = SearchFacilitySettings.ActivityCostperSecond
        'IsDefault = FacilityDefault
        'FacilityTypeID = 0

        'IncludeActivityCost = SearchFacilitySettings.IncludeActivityCost
        'IncludeActivityTime = SearchFacilitySettings.IncludeActivityTime
        'IncludeActivityUsage = SearchFacilitySettings.IncludeActivityUsage


    End Sub

    Public Sub SaveFacility(Tab As String)


    End Sub

    ' Compares the sent facility To the current one And returns a Boolean On equivlancy
    Public Function IsEqual(CompareFacility As IndustryFacility, Optional CompareCostCheck As Boolean = False, Optional CompareTimeCheck As Boolean = False) As Boolean

        With CompareFacility
            If .FacilityType <> FacilityType Then
                Return False
            ElseIf .FacilityProductionType <> FacilityProductionType Then
                Return False
            ElseIf .FacilityName <> FacilityName And Not (.FacilityType = POSFacility And FacilityProductionType = ProductionType.Manufacturing) Then
                Return False
            ElseIf .RegionName <> RegionName Then
                Return False
            ElseIf .RegionID <> RegionID Then
                Return False
            ElseIf .SolarSystemName <> SolarSystemName Then
                Return False
            ElseIf .SolarSystemID <> SolarSystemID Then
                Return False
            ElseIf .FWUpgradeLevel <> FWUpgradeLevel Then
                Return False
            ElseIf .TaxRate <> TaxRate Then
                Return False
            ElseIf .MaterialMultiplier <> MaterialMultiplier And .FacilityType <> POSFacility Then ' Only for non-pos
                Return False
            ElseIf .TimeMultiplier <> TimeMultiplier And .FacilityType <> POSFacility Then ' Only for non-pos
                Return False
            ElseIf .CostMultiplier <> CostMultiplier And .FacilityType <> POSFacility Then ' Only for non-pos
                Return False
            ElseIf .IncludeActivityCost <> IncludeActivityCost And CompareCostCheck Then
                Return False
            ElseIf .IncludeActivityTime <> IncludeActivityTime And CompareTimeCheck Then
                Return False
            ElseIf .IncludeActivityUsage <> IncludeActivityUsage Then
                Return False
            End If
        End With

        Return True

    End Function

    'Public Function GetFacilitySettings(ItemName As String, FacilityName As String, FacilityType As String, ActivityID As Integer,
    '                                                 IncludeActivityCost As Boolean, IncludeActivityTime As Boolean, IncludeActivityUsage As Boolean) As FacilitySettings
    '    Dim FS As New FacilitySettings
    '    Dim rsData As SQLiteDataReader
    '    Dim SQL As String = ""
    '    Dim TempSSName As String = ""
    '    Dim TempIndyType As IndustryType

    '    ' Look up BP data
    '    SQL = "SELECT BLUEPRINT_ID, TECH_LEVEL, ITEM_GROUP_ID, ITEM_CATEGORY_ID FROM ALL_BLUEPRINTS WHERE ITEM_NAME = '" & FormatDBString(ItemName) & "'"
    '    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '    rsData = DBCommand.ExecuteReader

    '    'If rsData.Read() Then
    '    '    ' Determine the build facility 
    '    '    TempIndyType = GetProductionType(ActivityManufacturing, rsData.GetInt64(2), rsData.GetInt64(3), FacilityType)
    '    'Else
    '    '    TempIndyType = IndustryType.Manufacturing
    '    'End If

    '    FS.ActivityID = ActivityID
    '    FS.ActivityCostperSecond = 0

    '    If FacilityName.Contains("(") And FacilityType = OutpostFacility Then
    '        ' Get name without (A), (G), etc.
    '        FS.Facility = FacilityName.Substring(0, InStr(FacilityName, "(") - 2)
    '    ElseIf FacilityName.Contains("(") And FacilityType = POSFacility Then
    '        FS.Facility = FacilityName.Substring(0, InStr(FacilityName, "(") - 2)
    '        ' Get the Solar System name from the POS facility name
    '        TempSSName = FacilityName.Substring(InStr(FacilityName, "("))
    '        TempSSName = TempSSName.Substring(0, InStr(TempSSName, "(") - 2)
    '    Else
    '        FS.Facility = FacilityName
    '    End If

    '    If TempSSName = "" Then
    '        ' Need to look up system, region from station name
    '        SQL = "SELECT solarSystemID, solarSystemName, REGIONS.regionID, regionName, STATION_NAME "
    '        SQL = SQL & "FROM SOLAR_SYSTEMS, REGIONS, STATIONS "
    '        SQL = SQL & "WHERE STATIONS.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID AND SOLAR_SYSTEMS.regionID = REGIONS.regionID "

    '        ' If it's an outpost, then there is only one per system...so only look for a like with the first three letters because people update the station names
    '        If FacilityType = OutpostFacility Then
    '            SQL = SQL & "AND STATION_NAME LIKE '" & FormatDBString(FS.Facility.Substring(0, 3)) & "%'"
    '        Else
    '            SQL = SQL & "AND STATION_NAME = '" & FormatDBString(FS.Facility) & "'"
    '        End If
    '    Else ' Look up the info from the solar system name
    '        SQL = "SELECT solarSystemID, solarSystemName, REGIONS.regionID, regionName,'" & FS.Facility & "' AS FACILITY_NAME "
    '        SQL = SQL & "FROM SOLAR_SYSTEMS, REGIONS "
    '        SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID AND solarSystemName = '" & FormatDBString(TempSSName) & "'"
    '    End If

    '    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
    '    rsData = DBCommand.ExecuteReader

    '    If rsData.Read() Then

    '        FS.Facility = rsData.GetString(4)
    '        FS.FacilityType = FacilityType
    '        FS.SolarSystemID = rsData.GetInt64(0)
    '        FS.SolarSystemName = rsData.GetString(1)
    '        FS.RegionID = rsData.GetInt64(2)
    '        FS.RegionName = rsData.GetString(3)

    '        FS.IncludeActivityCost = IncludeActivityCost
    '        FS.IncludeActivityTime = IncludeActivityTime
    '        FS.IncludeActivityUsage = IncludeActivityUsage

    '        FS.MaterialMultiplier = 1
    '        FS.TimeMultiplier = 1

    '        FS.ProductionType = TempIndyType
    '        FS.TaxRate = 0

    '    End If

    '    Return FS

    'End Function

End Class