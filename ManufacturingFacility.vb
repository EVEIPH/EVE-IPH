Imports System.Data.SQLite

Public Class ManufacturingFacility

    Private EVEDBRef As DBConnection
    Private EVEDBCommandRef As SQLiteCommand

    Private MY_SelectedView As FacilityView
    Private MY_SelectedFacility As IndustryFacility ' This is the active facility for the control, if not loaded will use the default
    Private MY_SelectedProductionType As ProductionType
    Private MY_SelectedCharacterID As Long

    Private MY_BPTech As Integer
    Private MY_BPHasComponents As Boolean
    Private MY_SelectedBPGroupID As Integer
    Private MY_SelectedBPCategoryID As Integer

    ' To check if we are loading and stop click events when changing values
    Private MY_LoadingActivities As Boolean
    Private MY_LoadingFacilityTypes As Boolean
    Private MY_LoadingRegions As Boolean
    Private MY_LoadingSystems As Boolean
    Private MY_LoadingFacilities As Boolean
    Private MY_ChangingUsageChecks As Boolean

    ' To save previous values for checking and loading
    Private MY_PreviousProductionType As ProductionType
    Private MY_PreviousFacilityType As FacilityTypes
    Private MY_PreviousRegion As String
    Private MY_PreviousSystem As String
    Private MY_PreviousEquipment As String
    Private MY_PreviousActivity As String

    ' Loaded variables
    Private MY_FacilityFullyLoaded As Boolean ' Determines if the facility was fully loaded through control changes or not
    Private MY_FacilityRegionsLoaded As Boolean
    Private MY_FacilitySystemsLoaded As Boolean
    Private MY_FacilityorArrayLoaded As Boolean

    Private MY_tt As ToolTip
    Private MY_FirstLoad As Boolean

    ' Save these options here in the facility and allow functions to get the values publically
    Private FacilityIncludeCopyCost As Boolean
    Private FacilityIncludeCopyTime As Boolean
    Private FacilityIncludeInventionCost As Boolean
    Private FacilityIncludeInventionTime As Boolean

    Private NoPOSCategoryIDs As New List(Of Long) ' For facilities

    ' All locally saved facility variables will be here
    Private SelectedManufacturingFacility As New IndustryFacility
    Private SelectedComponentManufacturingFacility As New IndustryFacility
    Private SelectedCapitalComponentManufacturingFacility As New IndustryFacility
    Private SelectedCapitalManufacturingFacility As New IndustryFacility
    Private SelectedSuperManufacturingFacility As New IndustryFacility
    Private SelectedT3CruiserManufacturingFacility As New IndustryFacility
    Private SelectedT3DestroyerManufacturingFacility As New IndustryFacility
    Private SelectedSubsystemManufacturingFacility As New IndustryFacility
    Private SelectedBoosterManufacturingFacility As New IndustryFacility
    Private SelectedInventionFacility As New IndustryFacility
    Private SelectedT3InventionFacility As New IndustryFacility
    Private SelectedCopyFacility As New IndustryFacility
    Private SelectedNoPOSFacility As New IndustryFacility

    ' Special cases for POS Facilities where items can be produced at more than one array
    Private SelectedPOSFuelBlockFacility As New IndustryFacility
    Private SelectedPOSLargeShipFacility As New IndustryFacility
    Private SelectedPOSModuleFacility As New IndustryFacility

    ' Save the default data for checking if the selected facility is a default and quick reference
    Private DefaultPOSFuelBlockFacility As New IndustryFacility
    Private DefaultPOSLargeShipFacility As New IndustryFacility
    Private DefaultPOSModuleFacility As New IndustryFacility

    Private DefaultManufacturingFacility As New IndustryFacility
    Private DefaultComponentManufacturingFacility As New IndustryFacility
    Private DefaultCapitalComponentManufacturingFacility As New IndustryFacility
    Private DefaultCapitalManufacturingFacility As New IndustryFacility
    Private DefaultSuperManufacturingFacility As New IndustryFacility
    Private DefaultT3CruiserManufacturingFacility As New IndustryFacility
    Private DefaultT3DestroyerManufacturingFacility As New IndustryFacility
    Private DefaultSubsystemManufacturingFacility As New IndustryFacility
    Private DefaultBoosterManufacturingFacility As New IndustryFacility
    Private DefaultInventionFacility As New IndustryFacility
    Private DefaultT3InventionFacility As New IndustryFacility
    Private DefaultCopyFacility As New IndustryFacility
    Private DefaultNoPOSFacility As New IndustryFacility

    ' Constant activities
    Private Const MY_ActivityManufacturing As String = "Manufacturing"
    Private Const MY_ActivityComponentManufacturing As String = "Component Manufacturing"
    Private Const MY_ActivityCapComponentManufacturing As String = "Cap Component Manufacturing"
    Private Const MY_ActivityCopying As String = "Copying"
    Private Const MY_ActivityInvention As String = "Invention"

    Private FacilityLabelDefaultColor As Color = SystemColors.Highlight
    Private FacilityLabelNonDefaultColor As Color = SystemColors.ButtonShadow

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
    Public Sub InitializeControl(ViewType As FacilityView, InitialProductionType As ProductionType, SentSelectedCharacterID As Long)
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
        MY_SelectedCharacterID = SentSelectedCharacterID

        MY_SelectedBPCategoryID = 0
        MY_SelectedBPGroupID = 0

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
                btnFacilityFitting.Visible = False
                btnFacilityFitting.Enabled = False

                btnFacilitySave.Top = btnFacilityFitting.Top
                btnFacilitySave.Left = btnFacilityFitting.Left + btnFacilityFitting.Width + 2
                btnFacilitySave.Visible = True
                btnFacilitySave.Enabled = False

                ' Load all the facilities for  tab - always start with manufacturing
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.Manufacturing, ViewType)
                SelectedManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.ComponentManufacturing, ViewType)
                SelectedComponentManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultComponentManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.CapitalComponentManufacturing, ViewType)
                SelectedCapitalComponentManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultCapitalComponentManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.CapitalManufacturing, ViewType)
                SelectedCapitalManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultCapitalManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.SuperManufacturing, ViewType)
                SelectedSuperManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultSuperManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.T3CruiserManufacturing, ViewType)
                SelectedT3CruiserManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultT3CruiserManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.SubsystemManufacturing, ViewType)
                SelectedSubsystemManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultSuperManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.BoosterManufacturing, ViewType)
                SelectedBoosterManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultBoosterManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.Copying, ViewType)
                SelectedCopyFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultCopyFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.Invention, ViewType)
                SelectedInventionFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultInventionFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.NoPOSManufacturing, ViewType)
                SelectedNoPOSFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultNoPOSFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.T3Invention, ViewType)
                SelectedT3InventionFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultT3InventionFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                Call MY_SelectedFacility.InitalizeFacility(ProductionType.T3DestroyerManufacturing, ViewType)
                SelectedT3DestroyerManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                DefaultT3DestroyerManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)

                ' Always load up the default manufacturing facility on initialization
                Call MY_LoadFacility(ProductionType.Manufacturing, 1, False, 25, 6)

            Case FacilityView.LimitedControls

                ' TODO - Move the objects around and place in control for view type

                ' Select what facility to load based on the industry type
                Call MY_SelectedFacility.InitalizeFacility(InitialProductionType, ViewType)

                'Now save the default and selected facility to the appropriate variable
                Select Case InitialProductionType
                    Case ProductionType.Manufacturing
                        SelectedManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.CapitalComponentManufacturing
                        SelectedCapitalComponentManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultCapitalComponentManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.CapitalManufacturing
                        SelectedCapitalManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultCapitalManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.SuperManufacturing
                        SelectedSuperManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultSuperManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.T3CruiserManufacturing
                        SelectedT3CruiserManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultT3CruiserManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.SubsystemManufacturing
                        SelectedSubsystemManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultSuperManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.BoosterManufacturing
                        SelectedBoosterManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultBoosterManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.Copying
                        SelectedCopyFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultCopyFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.Invention
                        SelectedInventionFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultInventionFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.NoPOSManufacturing
                        SelectedNoPOSFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultNoPOSFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.T3Invention
                        SelectedT3DestroyerManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultT3DestroyerManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.T3DestroyerManufacturing
                        SelectedT3DestroyerManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                        DefaultT3DestroyerManufacturingFacility = CType(MY_SelectedFacility.Clone, IndustryFacility)
                End Select

            Case Else
                ' Leave, no valid option sent
                Exit Sub
        End Select

    End Sub

    ' Loads the class facility and objects
    Public Sub MY_LoadFacility(ByVal FacilityBuildType As ProductionType, ByVal BlueprintTech As Integer, ByVal BPHasComponents As Boolean,
                               ByVal ItemGroupID As Integer, ByVal ItemCategoryID As Integer,
                               Optional ByVal LoadDefault As Boolean = False)

        Dim SelectedFacility As New IndustryFacility
        Dim SelectedActivity As String = ""

        ' Save these for later use
        MY_SelectedBPCategoryID = ItemCategoryID
        MY_SelectedBPGroupID = ItemGroupID
        MY_SelectedProductionType = FacilityBuildType
        MY_BPTech = BlueprintTech
        MY_BPHasComponents = BPHasComponents

        ' Look up Facility and Activity by reference
        Call SelectFacility(FacilityBuildType, LoadDefault, SelectedFacility, SelectedActivity)

        ' Process the activities combo if showing full controls
        If MY_SelectedView = FacilityView.FullControls Then
            Call MY_LoadFacilityActivities(BlueprintTech, BPHasComponents, ItemGroupID, ItemCategoryID)
            ' Activity combo is loaded so set the activity Text
            MY_LoadingActivities = True
            cmbFacilityActivities.Text = SelectedActivity
            MY_PreviousActivity = MY_ActivityManufacturing
            MY_LoadingActivities = False
        End If

        ' Facility Type combo, load it if they want to change
        Call MY_LoadFacilityTypes(MY_SelectedProductionType, SelectedActivity)

        ' Enable the type of facility and set
        MY_LoadingFacilityTypes = True
        cmbFacilityType.Enabled = True
        cmbFacilityType.Text = GetFacilityNameCode(SelectedFacility.FacilityType)
        MY_LoadingFacilityTypes = False

        If SelectedFacility.FacilityType = FacilityTypes.None Then
            ' Just hide the boxes and exit
            Call SetFacilityBonusBoxes(False)
            Call SetNoFacility()
            MY_FacilityFullyLoaded = True ' Even with none, it's loaded
            Exit Sub ' Leave, all loaded
        End If

        ' Region name Combo
        MY_LoadingRegions = True
        cmbFacilityRegion.Enabled = True
        cmbFacilityRegion.Text = SelectedFacility.RegionName
        MY_LoadingRegions = False

        ' Systems combo
        MY_LoadingSystems = True
        cmbFacilitySystem.Enabled = True
        cmbFacilitySystem.Text = SelectedFacility.SolarSystemName
        MY_LoadingSystems = False

        ' Facility/Array combo
        MY_LoadingFacilities = True
        cmbFacilityorArray.Enabled = True
        Dim AutoLoad As Boolean = False
        Call MY_LoadFacilities(False, SelectedActivity, AutoLoad, SelectedFacility.FacilityName)
        MY_LoadingFacilities = False

        ' Usage checks
        MY_ChangingUsageChecks = True

        ' Usage always visible
        chkFacilityIncludeUsage.Checked = SelectedFacility.IncludeActivityUsage

        If Not IsNothing(chkFacilityIncludeCost) Then
            chkFacilityIncludeCost.Checked = SelectedFacility.IncludeActivityCost
        End If

        If Not IsNothing(chkFacilityIncludeTime) Then
            chkFacilityIncludeTime.Checked = SelectedFacility.IncludeActivityTime
        End If

        MY_ChangingUsageChecks = False

        ' Finally show the results and save the facility locally
        If Not AutoLoad Then
            MY_LoadingFacilities = True
            With SelectedFacility
                cmbFacilityorArray.Text = .FacilityName
                Call DisplayFacilityBonus(.FacilityProductionType, ItemGroupID, ItemCategoryID, SelectedActivity,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)
            End With
            MY_LoadingFacilities = False
        End If

        Call ResetComboLoadVariables(False, False, False)

        ' All facilities loaded
        MY_FacilityFullyLoaded = True

    End Sub

    ' Loads the facility activity combo - checks group and category ID's if it has components to set component activities
    Public Sub MY_LoadFacilityActivities(BlueprintTech As Integer, HasComponents As Boolean, BPGroupID As Long, BPCategoryID As Long)

        MY_LoadingActivities = True
        cmbFacilityActivities.BeginUpdate()

        Select Case BlueprintTech
            Case BlueprintTechLevel.T1
                ' Just manufacturing (add components later if there are any)
                cmbFacilityActivities.Items.Clear()
                cmbFacilityActivities.Items.Add(MY_ActivityManufacturing)

            Case BlueprintTechLevel.T2
                ' Add only T2 activities to equipment
                cmbFacilityActivities.Items.Clear()
                cmbFacilityActivities.Items.Add(MY_ActivityManufacturing)
                cmbFacilityActivities.Items.Add(MY_ActivityCopying)
                cmbFacilityActivities.Items.Add(MY_ActivityInvention)

            Case BlueprintTechLevel.T3
                ' Add only T3 activities to eqipment
                cmbFacilityActivities.Items.Clear()
                cmbFacilityActivities.Items.Add(MY_ActivityManufacturing)
                cmbFacilityActivities.Items.Add(MY_ActivityInvention)

        End Select

        ' Add components as a manufacturing facility option if this bp has any
        If HasComponents Then
            Select Case BPGroupID
                Case ItemIDs.TitanGroupID, ItemIDs.DreadnoughtGroupID, ItemIDs.CarrierGroupID, ItemIDs.SupercarrierGroupID, ItemIDs.CapitalIndustrialShipGroupID,
                        ItemIDs.IndustrialCommandShipGroupID, ItemIDs.FreighterGroupID, ItemIDs.JumpFreighterGroupID, ItemIDs.FAXGroupID

                    cmbFacilityActivities.Items.Add(MY_ActivityCapComponentManufacturing)
                    If BPGroupID = ItemIDs.JumpFreighterGroupID Then
                        ' Need to add both cap and components
                        cmbFacilityActivities.Items.Add(MY_ActivityComponentManufacturing)
                    End If
                Case Else
                    ' Just regular
                    cmbFacilityActivities.Items.Add(MY_ActivityComponentManufacturing)
            End Select
        End If

        MY_LoadingActivities = False
        cmbFacilityActivities.EndUpdate()

    End Sub

    Private Sub cmbFacilityActivities_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityActivities.SelectedIndexChanged

        If Not MY_LoadingActivities And Not FirstLoad Then
            MY_SelectedProductionType = MY_GetProductionType(cmbFacilityActivities.Text, GetFacilityTypeCode(cmbFacilityType.Text), MY_SelectedBPGroupID, MY_SelectedBPCategoryID)

            ' If they switch the activity and it changed from the previous, then load the selected facility for this activity
            If MY_SelectedProductionType <> MY_PreviousProductionType Then
                MY_PreviousProductionType = MY_SelectedProductionType

                ' Load the facility for this activity
                Call MY_LoadFacility(MY_SelectedProductionType, MY_BPTech, MY_BPHasComponents, MY_SelectedBPGroupID, MY_SelectedBPCategoryID)

                ' Reset all previous to current list, since all the combos should be loaded
                MY_PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
                MY_PreviousEquipment = cmbFacilityorArray.Text
                MY_PreviousRegion = cmbFacilityRegion.Text
                MY_PreviousSystem = cmbFacilitySystem.Text
            End If

            Call cmbFacilityType.Focus()

        End If
    End Sub
    Private Sub cmbFacilityActivities_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityActivities.DropDown
        MY_PreviousActivity = cmbFacilityActivities.Text
    End Sub

    Private Sub cmbFacilityActivities_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityActivities.GotFocus
        Call cmbFacilityActivities.SelectAll()
    End Sub
    Private Sub cmbFacilityActivities_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityActivities.LostFocus
        cmbFacilityActivities.SelectionLength = 0
    End Sub
    Private Sub cmbFacilityActivities_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityActivities.KeyPress
        e.Handled = True
    End Sub

    ' Loads the facility types in the sent combo
    Public Sub MY_LoadFacilityTypes(FacilityProductionType As ProductionType, FacilityActivity As String)
        Dim Station As String = GetFacilityNameCode(FacilityTypes.Station)
        Dim Outpost As String = GetFacilityNameCode(FacilityTypes.Outpost)
        Dim POS As String = GetFacilityNameCode(FacilityTypes.POS)
        Dim UpwellStructure As String = GetFacilityNameCode(FacilityTypes.UpwellStructures)
        Dim NoneFacility As String = GetFacilityNameCode(FacilityTypes.None)

        MY_LoadingFacilityTypes = True
        MY_LoadingRegions = True
        MY_LoadingSystems = True
        MY_LoadingFacilities = True

        ' Clear the types each time for a fresh set of options
        cmbFacilityType.Items.Clear()

        ' Load the facility type options
        Select Case FacilityActivity
                ' Load up None for Invention/RE, Copy - they could buy the BP or T2 BPO
            Case MY_ActivityCopying, MY_ActivityInvention
                Select Case FacilityProductionType
                    Case ProductionType.T3Invention
                        ' Can be invented in outposts and POS
                        If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                        If POS <> "" Then cmbFacilityType.Items.Add(POS)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                        If NoneFacility <> "" Then cmbFacilityType.Items.Add(NoneFacility)
                    Case Else
                        If Station <> "" Then cmbFacilityType.Items.Add(Station)
                        If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                        If POS <> "" Then cmbFacilityType.Items.Add(POS)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                        If NoneFacility <> "" Then cmbFacilityType.Items.Add(NoneFacility)
                End Select
            Case MY_ActivityManufacturing
                Select Case FacilityProductionType
                    Case ProductionType.SuperManufacturing
                        ' Check types, supers can only be built in a pos
                        If POS <> "" Then cmbFacilityType.Items.Add(POS)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                    Case ProductionType.BoosterManufacturing, ProductionType.SubsystemManufacturing, ProductionType.T3CruiserManufacturing, ProductionType.T3DestroyerManufacturing
                        ' Can be built in outposts and POS
                        If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                        If POS <> "" Then cmbFacilityType.Items.Add(POS)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                    Case ProductionType.NoPOSManufacturing
                        ' No POS stuff like infrastructure hubs
                        If Station <> "" Then cmbFacilityType.Items.Add(Station)
                        If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                    Case Else
                        ' Add all
                        If Station <> "" Then cmbFacilityType.Items.Add(Station)
                        If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                        If POS <> "" Then cmbFacilityType.Items.Add(POS)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                End Select
            Case MY_ActivityComponentManufacturing, MY_ActivityCapComponentManufacturing
                ' Can do these anywhere
                If Station <> "" Then cmbFacilityType.Items.Add(Station)
                If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                If POS <> "" Then cmbFacilityType.Items.Add(POS)
                If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
        End Select

        ' Only reset if they changed it
        If FacilityProductionType <> MY_PreviousProductionType Or FacilityActivity <> MY_PreviousActivity Then
            ' Reset all other dropdowns
            cmbFacilityType.Text = "Select Type"
            cmbFacilityRegion.Items.Clear()
            cmbFacilityRegion.Text = "Select Region"
            cmbFacilityRegion.Enabled = False
            cmbFacilitySystem.Items.Clear()
            cmbFacilitySystem.Text = "Select System"
            cmbFacilitySystem.Enabled = False
            cmbFacilityorArray.Items.Clear()
            cmbFacilityorArray.Text = "Select Facility / Array"
            cmbFacilityorArray.Enabled = False
            chkFacilityIncludeUsage.Enabled = False
            MY_PreviousProductionType = FacilityProductionType
            MY_PreviousActivity = FacilityActivity

            Call SetFacilityBonusBoxes(False)

        End If

        ' Double check the text selected and reset 
        If Not cmbFacilityType.Items.Contains(cmbFacilityType.Text) Then
            cmbFacilityType.Text = GetFacilityNameCode(FacilityTypes.POS) ' can build almost everything (if not all) in a pos
        End If

        ' Enable the facility type combo
        cmbFacilityType.Enabled = True

        ' Make sure default is not shown yet
        lblFacilityDefault.Visible = False
        btnFacilitySave.Enabled = False

        MY_LoadingFacilityTypes = False
        MY_LoadingRegions = False
        MY_LoadingSystems = False
        MY_LoadingFacilities = False

        Call ResetComboLoadVariables(False, False, False)

    End Sub

    Private Sub cmbFacilityType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityType.SelectedIndexChanged
        ' Don't do anything if it's the same as the old type
        If MY_PreviousFacilityType <> GetFacilityTypeCode(cmbFacilityType.Text) Then
            ' Might not want to set a facility for copy or invention - "None" is an option for these two activities
            If Not MY_LoadingFacilityTypes And Not FirstLoad And GetFacilityTypeCode(cmbFacilityType.Text) <> FacilityTypes.None Then

                Call MY_LoadFacilityRegions(MY_SelectedBPGroupID, MY_SelectedBPCategoryID, True, cmbFacilityActivities.Text)
                Call cmbFacilityRegion.Focus()

            ElseIf GetFacilityTypeCode(cmbFacilityType.Text) = FacilityTypes.None Then ' Invention or Copy facility - set to none

                Call SetNoFacility()

                ' Allow this to be saved as a default though
                btnFacilitySave.Enabled = True
                ' changed so not the default
                lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
                ' Save the facility locally
                Call DisplayFacilityBonus(MY_SelectedProductionType, MY_SelectedBPGroupID, MY_SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)
            End If

            ' Anytime this changes, set all the other ME/TE boxes to not viewed
            Call SetFacilityBonusBoxes(False)
            MY_FacilityFullyLoaded = False
            MY_PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
            ' Reset the previous records
            MY_PreviousEquipment = ""
            MY_PreviousRegion = ""
            MY_PreviousSystem = ""

        End If
    End Sub

    Private Sub cmbFacilityType_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityType.DropDown
        MY_PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
    End Sub

    Private Sub cmbFacilityType_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityType.GotFocus
        Call cmbFacilityType.SelectAll()
    End Sub
    Private Sub cmbFacilityType_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityType.LostFocus
        cmbFacilityType.SelectionLength = 0
    End Sub
    Private Sub cmbFacilityType_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityType.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the region combo
    Public Sub MY_LoadFacilityRegions(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean, ByRef FacilityActivity As String)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        MY_LoadingRegions = True
        MY_LoadingSystems = True
        MY_LoadingFacilities = True

        cmbFacilityRegion.Items.Clear()

        ' Load regions from the facilities table - only load regions for our activity type and item group/category
        Select Case cmbFacilityType.Text

            Case OutpostFacility, StationFacility

                SQL = "SELECT DISTINCT REGION_NAME FROM STATION_FACILITIES WHERE OUTPOST "

                ' Set flag for outpost just to delineate
                If cmbFacilityType.Text = StationFacility Then
                    SQL = SQL & " = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                End If

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add only regions with stations that can make what we sent
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

            Case POSFacility, StructureFacility
                ' For a POS and Upwell Structures, load all regions as options, but adding only one wormhole region option and don't show Jove regions
                SQL = "SELECT DISTINCT CASE WHEN (REGIONS.regionID >=11000000 and REGIONS.regionid <=11000030) THEN 'Wormhole Space' ELSE regionName END AS REGION_NAME "
                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "
                SQL = SQL & "AND (factionID <> 500005 OR factionID IS NULL) "

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
                    ' For caps, only show low sec
                    SQL = SQL & " AND security < .45 "
                End If

        End Select

        SQL = SQL & "GROUP BY REGION_NAME "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            cmbFacilityRegion.Items.Add(rsLoader.GetString(0))
        End While

        ' Enable the region combo
        cmbFacilityRegion.Enabled = True

        ' Only turn off everything if it's set to select region
        If NewFacility Then
            cmbFacilitySystem.Items.Clear()
            cmbFacilitySystem.Text = "Select System"
            cmbFacilitySystem.Enabled = False
            cmbFacilityorArray.Items.Clear()
            cmbFacilityorArray.Text = "Select Facility / Array"
            cmbFacilityorArray.Enabled = False
            ' Make sure default is not checked yet
            lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
            btnFacilitySave.Enabled = False
            chkFacilityIncludeUsage.Enabled = False
            Call SetFacilityBonusBoxes(False)
        End If

        ' Only reset the region if the current selected region is not in list, also if it is in list, enable solarsystem
        If Not cmbFacilityRegion.Items.Contains(cmbFacilityRegion.Text) Then
            cmbFacilityRegion.Text = "Select Region"
        Else
            cmbFacilitySystem.Enabled = True
        End If

        MY_LoadingRegions = False
        MY_LoadingSystems = False
        MY_LoadingFacilities = False

        Call ResetComboLoadVariables(True, False, False)

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub cmbFacilityRegion_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityRegion.SelectedIndexChanged
        If Not MY_LoadingRegions And Not FirstLoad And MY_PreviousRegion <> cmbFacilityRegion.Text Then
            Call MY_LoadFacilitySystems(MY_SelectedBPGroupID, MY_SelectedBPCategoryID, True, cmbFacilityActivities.Text)
            Call cmbFacilitySystem.Focus()
            Call SetFacilityBonusBoxes(False)
            MY_FacilityFullyLoaded = False
            MY_PreviousRegion = cmbFacilityRegion.Text
        End If
    End Sub

    Private Sub cmbFacilityRegion_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityRegion.DropDown
        ' If you drop down, don't show the text window
        cmbFacilityRegion.AutoCompleteMode = AutoCompleteMode.None

        If Not FirstLoad And Not MY_FacilityRegionsLoaded Then
            MY_PreviousRegion = cmbFacilityRegion.Text
            ' Save the current
            MY_PreviousRegion = cmbFacilityRegion.Text

            Call MY_LoadFacilityRegions(MY_SelectedBPGroupID, MY_SelectedBPCategoryID, False, cmbFacilityActivities.Text)

        End If
    End Sub

    Private Sub cmbFacilityRegion_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityRegion.GotFocus
        Call cmbFacilityRegion.SelectAll()
    End Sub
    Private Sub cmbFacilityRegion_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityRegion.LostFocus
        cmbFacilitySystem.SelectionLength = 0
    End Sub
    Private Sub cmbFacilityRegion_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityRegion.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the systems combo
    Public Sub MY_LoadFacilitySystems(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean, ByRef FacilityActivity As String)

        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        MY_LoadingSystems = True
        MY_LoadingFacilities = True

        cmbFacilitySystem.Items.Clear()

        Select Case cmbFacilityType.Text

            Case OutpostFacility, StationFacility

                SQL = "SELECT DISTINCT SOLAR_SYSTEM_NAME, COST_INDEX FROM STATION_FACILITIES WHERE OUTPOST "

                ' Set flag for outpost just to delineate
                If cmbFacilityType.Text = StationFacility Then
                    SQL = SQL & " = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                End If

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(cmbFacilityRegion.Text) & "'"

            Case POSFacility
                ' For a POS, load all systems, if wormhole 'region' selected, then load jspace systems
                SQL = "SELECT DISTINCT solarSystemName AS SOLAR_SYSTEM_NAME, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS COST_INDEX "
                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
                SQL = SQL & "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON solarSystemID = SOLAR_SYSTEM_ID "

                Select Case FacilityActivity
                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                End Select

                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "

                If cmbFacilityRegion.Text = "Wormhole Space" Then
                    SQL = SQL & "AND SOLAR_SYSTEMS.regionID >=11000000 and SOLAR_SYSTEMS.regionid <=11000030 "
                Else
                    ' For a POS, load all systems that have records linked
                    SQL = SQL & "AND regionName = '" & FormatDBString(cmbFacilityRegion.Text) & "'"
                End If

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
                    ' For caps, only show low sec
                    SQL = SQL & " AND security < .45 "
                End If

            Case StructureFacility
                SQL = "SELECT DISTINCT solarSystemName AS SOLAR_SYSTEM_NAME, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS COST_INDEX "
                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
                SQL = SQL & "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON solarSystemID = SOLAR_SYSTEM_ID "

                Select Case FacilityActivity
                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                End Select

                SQL = SQL & "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "

                ' Upwell Structures can be anchored almost anywhere except starter systems, trade hubs, and shattered wormholes (including Thera)
                ' Check both disallowable anchor tables
                SQL = SQL & "AND (solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_CATEGORIES WHERE CATEGORY_ID = 65) AND "
                SQL = SQL & "solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_GROUPS WHERE GROUP_ID = 65)) "

                If cmbFacilityRegion.Text = "Wormhole Space" Then
                    SQL = SQL & "AND SOLAR_SYSTEMS.regionID >=11000000 and SOLAR_SYSTEMS.regionid <=11000030 "
                Else
                    ' For a citadel, load all systems that have records linked
                    SQL = SQL & "AND regionName = '" & FormatDBString(cmbFacilityRegion.Text) & "' "
                End If

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = SupercarrierGroupID Or ItemGroupID = TitanGroupID Then
                    SQL = SQL & "AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = DreadnoughtGroupID Or ItemGroupID = CarrierGroupID Or ItemGroupID = CapitalIndustrialShipGroupID Or ItemGroupID = FAXGroupID Then
                    ' For caps, only show low sec
                    SQL = SQL & "AND security < .45 "
                End If

        End Select

        SQL = SQL & " GROUP BY SOLAR_SYSTEM_NAME, COST_INDEX"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            cmbFacilitySystem.Items.Add(rsLoader.GetString(0) & " (" & FormatNumber(rsLoader.GetDouble(1), 3) & ")")
        End While

        ' Enable the system combo
        cmbFacilitySystem.Enabled = True

        ' Only turn off everything if it's set to select a system
        If NewFacility Then
            cmbFacilityorArray.Items.Clear()
            If cmbFacilityType.Text = POSFacility Then
                cmbFacilityorArray.Text = "Select Array"
            Else
                cmbFacilityorArray.Text = "Select Facility"
            End If
            cmbFacilityorArray.Enabled = False
            ' Make sure default is not checked yet
            lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
            btnFacilitySave.Enabled = False
            chkFacilityIncludeUsage.Enabled = False
            Call SetFacilityBonusBoxes(False)
        End If

        ' Only reset the system if the current selected system is not in list, also if it is in list, enable facilty
        If Not cmbFacilitySystem.Items.Contains(cmbFacilitySystem.Text) Then
            cmbFacilitySystem.Text = "Select System"
        Else
            cmbFacilityorArray.Enabled = True
        End If

        MY_LoadingSystems = False
        MY_LoadingFacilities = False

        Call ResetComboLoadVariables(False, True, False)

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub cmbFacilitySystem_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilitySystem.SelectedIndexChanged
        Dim OverrideFacilityName As String = ""
        Dim Autoload As Boolean = False

        cmbFacilitySystem.SelectionLength = 0

        If Not MY_LoadingSystems And Not FirstLoad And MY_PreviousSystem <> cmbFacilitySystem.Text Then

            Call SetFacilityBonusBoxes(False)

            If cmbFacilityType.Text = OutpostFacility Then
                OverrideFacilityName = ""
                Autoload = True
            ElseIf cmbFacilityType.Text = POSFacility Then
                OverrideFacilityName = GetPOSManufacturingFacility(MY_SelectedFacility).FacilityName
            End If

            ' Load the facilities
            Call MY_LoadFacilities(False, cmbFacilityActivities.Text, Autoload, OverrideFacilityName)

            If Autoload Then
                MY_FacilityFullyLoaded = True
            Else
                Call SetFacilityBonusBoxes(False)
                MY_FacilityFullyLoaded = False
            End If

            Call cmbFacilityorArray.Focus()

            MY_PreviousSystem = cmbFacilitySystem.Text
        End If

    End Sub

    Private Sub cmbFacilitySystem_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilitySystem.DropDown
        ' If you drop down, don't show the text window
        cmbFacilitySystem.AutoCompleteMode = AutoCompleteMode.None

        If Not MY_FacilitySystemsLoaded And Not FirstLoad Then
            MY_PreviousSystem = cmbFacilitySystem.Text
            Call MY_LoadFacilitySystems(MY_SelectedBPGroupID, MY_SelectedBPCategoryID, False, cmbFacilityActivities.Text)
        End If
    End Sub

    Private Sub cmbFacilitySystem_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilitySystem.GotFocus
        Call cmbFacilitySystem.SelectAll()
    End Sub
    Private Sub cmbFacilitySystem_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilitySystem.LostFocus
        cmbFacilitySystem.SelectionLength = 0
    End Sub
    Private Sub cmbFacilitySystem_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilitySystem.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the facilities/arrays combo - an itemcategory or itemgroup id of -1 means to ignore it when filling arrays
    Public Sub MY_LoadFacilities(NewFacility As Boolean, ByRef FacilityActivity As String,
                                 ByRef AutoLoadFacility As Boolean, Optional OverrideFacilityName As String = "")
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        MY_LoadingFacilities = True

        Dim SystemName As String
        If cmbFacilitySystem.Text.Contains("(") Then
            SystemName = cmbFacilitySystem.Text.Substring(0, InStr(cmbFacilitySystem.Text, "(") - 2)
        Else
            SystemName = cmbFacilitySystem.Text
        End If

        Select Case GetFacilityTypeCode(cmbFacilityType.Text)

            Case FacilityTypes.Station, FacilityTypes.Outpost
                ' Load the Stations in system for the activity we are doing
                SQL = "SELECT DISTINCT FACILITY_NAME FROM STATION_FACILITIES WHERE OUTPOST "

                ' Set flag for outpost just to delineate
                If GetFacilityTypeCode(cmbFacilityType.Text) = FacilityTypes.Station Then
                    SQL = SQL & " = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                End If

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Check groups and categories
                        SQL = SQL & GetFacilityCatGroupIDSQL(MY_SelectedBPCategoryID, MY_SelectedBPGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(MY_SelectedBPCategoryID, MY_SelectedBPGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(MY_SelectedBPCategoryID, MY_SelectedBPGroupID, IndustryActivities.Invention)
                End Select

                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(cmbFacilityRegion.Text) & "' "
                SQL = SQL & "AND SOLAR_SYSTEM_NAME = '" & FormatDBString(SystemName) & "' "

            Case FacilityTypes.POS

                ' Load all the array types up into the combo for a POS
                SQL = "SELECT DISTINCT ARRAY_NAME AS FACILITY_NAME FROM ASSEMBLY_ARRAYS "
                SQL = SQL & "WHERE ACTIVITY_ID = "

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Check groups and categories
                        SQL = SQL & GetFacilityCatGroupIDSQL(MY_SelectedBPCategoryID, MY_SelectedBPGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for component
                        Select Case MY_SelectedBPGroupID
                            Case TitanGroupID, SupercarrierGroupID, DreadnoughtGroupID, CarrierGroupID,
                                CapitalIndustrialShipGroupID, IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID,
                                AdvCapitalComponentGroupID, CapitalComponentGroupID, FAXGroupID
                                SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
                            Case Else
                                SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
                        End Select
                    Case ActivityCopying
                        SQL = SQL & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(MY_SelectedBPCategoryID, MY_SelectedBPGroupID, IndustryActivities.Invention)
                    Case ActivityInvention
                        ' POS invention you can only do T3 in certain arrays
                        SQL = SQL & CStr(IndustryActivities.Invention) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(MY_SelectedBPCategoryID, MY_SelectedBPGroupID, IndustryActivities.Invention)
                End Select

            Case FacilityTypes.UpwellStructures
                ' Load all the upwell structures
                SQL = "SELECT typeName as FACILITY_NAME FROM INVENTORY_TYPES, INVENTORY_GROUPS WHERE INVENTORY_GROUPS.categoryID = 65 
                AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupid AND INVENTORY_TYPES.published = 1"

        End Select

        ' This is helpful if we auto-load (Capital array before super capital, equipment array before rapid equipment) to choose the one more likely
        SQL = SQL & " ORDER BY FACILITY_NAME"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        cmbFacilityorArray.Enabled = True
        cmbFacilityorArray.Items.Clear()

        Dim AutoLoadName As String = ""
        Dim i As Integer = 0

        While rsLoader.Read
            If rsLoader.GetString(0).Contains("Thukker") And GetFacilityTypeCode(cmbFacilityType.Text) = FacilityTypes.POS Then
                ' Need to make sure it's a low sec system selected
                Dim rsCheck As SQLiteDataReader
                SQL = "SELECT SECURITY FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SystemName) & "'"
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    If rsCheck.GetDouble(0) < 0.45 Then
                        ' Thukker is only low sec - no easy way to weed this out
                        cmbFacilityorArray.Items.Add(rsLoader.GetString(0))
                    End If
                Else
                    ' Allow it
                    cmbFacilityorArray.Items.Add(rsLoader.GetString(0))
                End If
            Else
                cmbFacilityorArray.Items.Add(rsLoader.GetString(0))
            End If

            i += 1 ' get the count
            ' Load the first one - auto choose subsystem array over advanced medium array unless already selected
            If AutoLoadName = "" Or (rsLoader.GetString(0) = "Subsystem Assembly Array" And OverrideFacilityName = "") Then
                AutoLoadName = rsLoader.GetString(0)
            End If
        End While

        ' Always load the facility if there is only one and we have a reference to auto load or we are loading a specific facility
        If (i = 1 And Not IsNothing(AutoLoadFacility)) Or cmbFacilityorArray.Items.Contains(OverrideFacilityName) _
            Or cmbFacilityorArray.Items.Contains(cmbFacilityorArray.Text) Or OverrideFacilityName = "CalcBase" Then
            ' Check the override, if they want to use a rapid assembly it will override here, otherwise the other facility types should handle it (e.g. super, cap, etc)
            If OverrideFacilityName <> "" And cmbFacilityorArray.Items.Contains(OverrideFacilityName) Then
                cmbFacilityorArray.Text = OverrideFacilityName
            Else
                cmbFacilityorArray.Text = AutoLoadName
            End If

            AutoLoadFacility = True
            ' Display bonuses - Need to load everything since the array won't change to cause it to reload
            Call DisplayFacilityBonus(MY_SelectedProductionType, MY_SelectedBPGroupID, MY_SelectedBPCategoryID, cmbFacilityActivities.Text,
                                      GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)
        Else
            If Not cmbFacilityorArray.Items.Contains(cmbFacilityorArray.Text) Then
                ' Only load if the item isn't in the combo
                Select Case GetFacilityTypeCode(cmbFacilityType.Text)
                    Case FacilityTypes.Station
                        cmbFacilityorArray.Text = "Select Station"
                    Case FacilityTypes.Outpost
                        cmbFacilityorArray.Text = "Select Outpost"
                    Case FacilityTypes.POS
                        cmbFacilityorArray.Text = "Select Array"
                    Case FacilityTypes.UpwellStructures
                        cmbFacilityorArray.Text = "Select Upwell Structure"
                End Select

                ' Make sure default is turned off since we still have to load the array
                btnFacilitySave.Enabled = False
                lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
                chkFacilityIncludeUsage.Enabled = False ' Don't enable the usage either
            Else
                ' Since this is a different system but facility is loaded, enable save
                btnFacilitySave.Enabled = True
                lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
                chkFacilityIncludeUsage.Enabled = True
            End If

            AutoLoadFacility = False

        End If

        If NewFacility Then
            ' Make sure default is not checked yet
            lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
            btnFacilitySave.Enabled = False
            Call SetFacilityBonusBoxes(True)
        End If

        ' Users might select the facility drop down first, so reload all others
        Call ResetComboLoadVariables(False, False, True)

        MY_LoadingFacilities = False

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub cmbFacilityorArray_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityorArray.SelectedIndexChanged

        If Not MY_LoadingFacilities And Not FirstLoad And MY_PreviousEquipment <> cmbFacilityorArray.Text Then
            Call DisplayFacilityBonus(MY_SelectedProductionType, MY_SelectedBPGroupID, MY_SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)

            MY_PreviousEquipment = cmbFacilityorArray.Text

        End If
    End Sub

    Private Sub cmbFacilityorArray_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityorArray.DropDown
        ' If you drop down, don't show the text window
        cmbFacilityorArray.AutoCompleteMode = AutoCompleteMode.None

        If Not MY_FacilityorArrayLoaded And Not FirstLoad Then
            MY_PreviousEquipment = cmbFacilityorArray.Text
            Call MY_LoadFacilities(False, cmbFacilityActivities.Text, False, "")
        End If
    End Sub

    Private Sub cmbFacilityorArray_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityorArray.GotFocus
        Call cmbFacilityorArray.SelectAll()
    End Sub
    Private Sub cmbFacilityorArray_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityorArray.LostFocus
        cmbFacilityorArray.SelectionLength = 0
    End Sub
    Private Sub cmbFacilityorArray_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityorArray.KeyPress
        e.Handled = True
    End Sub
    Private Sub lblFacilityDefault_DoubleClick(sender As Object, e As EventArgs) Handles lblFacilityDefault.DoubleClick
        ' Load the default facility for the selected activity if it's not already the default
        If lblFacilityDefault.ForeColor = SystemColors.ButtonShadow Then
            MY_LoadingActivities = True ' Don't trigger a combo load yet
            ' Call MY_LoadFacility(MY_SelectedProductionType, True, MY_SelectedBPGroupID, MY_SelectedBPCategoryID)
            MY_LoadingActivities = False
        End If
    End Sub

    ' Displays the bonus for the facility selected in the facility or array combo
    Public Sub DisplayFacilityBonus(BuildType As ProductionType, ItemGroupID As Integer, ItemCategoryID As Integer, Activity As String,
                                    FacilityType As FacilityTypes, FacilityName As String)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        Dim FacilityID As Long
        Dim FacilityTypeID As Integer
        Dim MaterialMultiplier As Double
        Dim TimeMultiplier As Double
        Dim CostMultiplier As Double
        Dim Tax As Double

        Dim TempDefaultFacility As New IndustryFacility

        Dim SelectedFacility As New IndustryFacility
        Dim CompareCostCheck As Boolean = False
        Dim CompareTimeCheck As Boolean = False

        ' Process system if needed
        Dim SystemName As String
        If cmbFacilitySystem.Text.Contains("(") Then
            SystemName = cmbFacilitySystem.Text.Substring(0, InStr(cmbFacilitySystem.Text, "(") - 2)
        Else
            SystemName = cmbFacilitySystem.Text
        End If

        If FacilityType <> FacilityTypes.None Then

            ' First, see if this facility is a saved facility, and use the values from the table
            SQL = "SELECT FACILITY_ID, FACILITY_TYPE_ID, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER, FACILITY_TAX "
            SQL &= "FROM SAVED_FACILITIES, REGIONS, SOLAR_SYSTEMS, INVENTORY_TYPES "
            SQL &= "WHERE SAVED_FACILITIES.REGION_ID = REGIONS.regionID "
            SQL &= "AND INVENTORY_TYPES.typeID = FACILITY_TYPE_ID "
            SQL &= "AND SAVED_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
            SQL &= String.Format("AND CHARACTER_ID = {0} ", CStr(SelectedCharacter.ID))
            SQL &= String.Format("AND PRODUCTION_TYPE = {0} AND FACILITY_VIEW = {1} ", CStr(BuildType), CStr(MY_SelectedView))
            SQL &= "AND REGIONS.regionName = '" & cmbFacilityRegion.Text & "' "
            SQL &= "AND SOLAR_SYSTEMS.solarSystemName = '" & SystemName & "' "
            SQL &= "AND typeName = '" & cmbFacilityorArray.Text & "'"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader

            If Not rsLoader.HasRows Then
                rsLoader.Close()

                ' Not in there, so use the defaults
                Select Case FacilityType

                    Case FacilityTypes.Outpost, FacilityTypes.Station

                        ' Load the Stations in system for the activity we are doing
                        SQL = "SELECT DISTINCT FACILITY_ID, FACILITY_TYPE_ID, MATERIAL_MULTIPLIER, "
                        SQL = SQL & "TIME_MULTIPLIER, COST_MULTIPLIER, "
                        SQL = SQL & "FACILITY_TAX FROM STATION_FACILITIES WHERE OUTPOST  "

                        ' Set flag for outpost just to delineate
                        If FacilityType = FacilityTypes.Station Then
                            SQL = SQL & " = " & CStr(StationType.Station) & " "
                        Else
                            SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                        End If
                        SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString(FacilityName) & "' "

                    Case FacilityTypes.POS

                        SQL = "SELECT ARRAY_TYPE_ID AS FACILITY_ID, ARRAY_TYPE_ID, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER, " & CStr(POSTaxRate) & " as TAX "
                        SQL = SQL & "FROM ASSEMBLY_ARRAYS WHERE ARRAY_NAME = '" & FormatDBString(FacilityName) & "' "

                    Case FacilityTypes.UpwellStructures

                        SQL = "SELECT UPWELL_STRUCTURE_TYPE_ID AS FACILITY_ID, UPWELL_STRUCTURE_TYPE_ID, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER, " & CStr(POSTaxRate) & " as TAX "
                        SQL &= "FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_NAME = '" & FormatDBString(FacilityName) & "' "

                End Select

                If FacilityType <> FacilityTypes.UpwellStructures Then
                    Select Case Activity
                        Case ActivityManufacturing
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                        Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                            ' Add category for component
                            Select Case ItemGroupID
                                Case TitanGroupID, SupercarrierGroupID, DreadnoughtGroupID, CarrierGroupID,
                            CapitalIndustrialShipGroupID, IndustrialCommandShipGroupID, FreighterGroupID, JumpFreighterGroupID,
                                AdvCapitalComponentGroupID, CapitalComponentGroupID, FAXGroupID
                                    SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
                                Case Else
                                    SQL = SQL & GetFacilityCatGroupIDSQL(ComponentCategoryID, ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
                            End Select
                        Case ActivityCopying
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                        Case ActivityInvention
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                    End Select
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader

            End If

            If rsLoader.Read Then
                ' Load the data looked up
                FacilityID = rsLoader.GetInt64(0)
                FacilityTypeID = rsLoader.GetInt32(1)
                MaterialMultiplier = rsLoader.GetDouble(2)
                TimeMultiplier = rsLoader.GetDouble(3)
                CostMultiplier = rsLoader.GetDouble(4)
                Tax = rsLoader.GetDouble(5)

                rsLoader.Close()
            Else
                ' Set the facility to none if not found
                FacilityType = FacilityTypes.None
            End If

        End If

        ' None selected or not found
        If FacilityType = FacilityTypes.None Then
            Dim Defaults As New ProgramSettings
            FacilityName = None
            FacilityID = 0
            FacilityTypeID = 0
            MaterialMultiplier = Defaults.FacilityDefaultMM
            TimeMultiplier = Defaults.FacilityDefaultTM
            CostMultiplier = 1
            Tax = Defaults.FacilityDefaultTax
        End If

        Dim MMText As String = FormatPercent(1 - MaterialMultiplier, 1)
        Dim TMText As String = FormatPercent(1 - TimeMultiplier, 1)
        Dim CostText As String = FormatPercent(1 - CostMultiplier, 1)
        Dim TaxText As String = FormatPercent(Tax, 1)

        If FacilityType = FacilityTypes.Outpost Or FacilityType = FacilityTypes.UpwellStructures Then
            txtFacilityManualME.Enabled = True
            txtFacilityManualTE.Enabled = True
            txtFacilityManualTax.Enabled = True
            txtFacilityManualCost.Enabled = True
        Else ' Disable for non-outpost/citadel
            txtFacilityManualME.Enabled = False
            txtFacilityManualTE.Enabled = False
            txtFacilityManualTax.Enabled = False
            txtFacilityManualCost.Enabled = False
        End If

        ' Set the values
        txtFacilityManualME.Text = MMText
        txtFacilityManualTE.Text = TMText
        txtFacilityManualTax.Text = TaxText
        txtFacilityManualCost.Text = CostText

        ' Now that we have everything, load the full facility into the appropriate selected facility to use later
        With SelectedFacility
            .ActivityCostPerSecond = 0
            Select Case Activity
                Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                    .ActivityID = IndustryActivities.Manufacturing
                Case ActivityCopying
                    .ActivityID = IndustryActivities.Copying
                Case ActivityInvention
                    .ActivityID = IndustryActivities.Invention
            End Select

            .FacilityID = FacilityID
            .FacilityName = FacilityName
            .FacilityTypeID = FacilityTypeID
            .FacilityType = FacilityType
            .MaterialMultiplier = MaterialMultiplier
            .TimeMultiplier = TimeMultiplier
            .CostMultiplier = CostMultiplier
            .RegionName = cmbFacilityRegion.Text
            .SolarSystemName = cmbFacilitySystem.Text
            .FacilityProductionType = BuildType
            MY_ChangingUsageChecks = True
            .IncludeActivityUsage = chkFacilityIncludeUsage.Checked ' Use this value when loading from Load Facility (using the selected facility) or from the form dropdown (use the checkbox)
            .TaxRate = Tax

            If Not IsNothing(chkFacilityIncludeCost) Then
                .IncludeActivityCost = chkFacilityIncludeCost.Checked
            Else
                .IncludeActivityCost = False
            End If

            If Not IsNothing(chkFacilityIncludeTime) Then
                .IncludeActivityTime = chkFacilityIncludeTime.Checked
            Else
                .IncludeActivityTime = False
            End If
            MY_ChangingUsageChecks = False

            If FacilityType <> FacilityTypes.None And .SolarSystemID = 0 Then
                ' Quick look up for the solarsystemid and region id, Strip off the system index first
                SQL = "SELECT solarSystemID, security, regionID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SystemName) & "'"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                .SolarSystemID = rsLoader.GetInt64(0)
                .SolarSystemSecurity = rsLoader.GetDouble(1)
                .RegionID = rsLoader.GetInt64(2)
                rsLoader.Close()

                ' Now look up the cost index 
                Select Case FacilityType
                    Case FacilityTypes.Station, FacilityTypes.Outpost
                        SQL = "SELECT COST_INDEX FROM STATION_FACILITIES WHERE FACILITY_NAME = '" & FormatDBString(FacilityName) & "'"
                        SQL = SQL & "AND ACTIVITY_ID = " & .ActivityID & " "
                    Case FacilityTypes.POS
                        SQL = "SELECT COST_INDEX FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES "
                        SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
                        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
                        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = " & .ActivityID & " "
                    Case FacilityTypes.UpwellStructures
                        SQL = "SELECT COST_INDEX FROM UPWELL_STRUCTURES, INDUSTRY_SYSTEMS_COST_INDICIES "
                        SQL = SQL & "WHERE UPWELL_STRUCTURES.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
                        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
                        SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = " & .ActivityID & " "
                End Select

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader

                If rsLoader.Read() Then
                    .CostIndex = rsLoader.GetFloat(0)
                Else
                    .CostIndex = 0
                End If

                rsLoader.Close()
            Else
                .SolarSystemID = 0
                .RegionID = 0
                .CostIndex = 0
            End If

            ' If this is a citadel, then look up any saved modules and store them
            If .FacilityType = FacilityTypes.UpwellStructures Then
                SQL = "SELECT INSTALLED_MODULE_ID FROM FACILITY_INSTALLED_MODULES "
                SQL &= "WHERE CHARACTER_ID = {0} AND INDUSTRY_TYPE = {1} AND FACILITY_VIEW = {2}"
                DBCommand = New SQLiteCommand(String.Format(SQL, MY_SelectedCharacterID, CStr(MY_SelectedProductionType), CStr(MY_SelectedView)), EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                .InstalledModules = New List(Of Integer)

                While rsLoader.Read()
                    .InstalledModules.Add(rsLoader.GetInt32(0))
                End While
                rsLoader.Close()

                ' Now, adjust the MM, TM, CM based on modules installed


            End If

        End With

        ' Show the boxes
        Call SetFacilityBonusBoxes(True)

        ' Facility is loaded, so save it to default and dynamic variable
        Call SetFacility(SelectedFacility, BuildType, CompareCostCheck, CompareTimeCheck)

        ' Make sure the usage check is now enabled
        If FacilityType <> FacilityTypes.None Then
            chkFacilityIncludeUsage.Enabled = True
        End If

        If FacilityType = FacilityTypes.UpwellStructures Then
            ' Enable fitting
            btnFacilityFitting.Enabled = True
            btnFacilityFitting.Visible = True
        Else
            btnFacilityFitting.Enabled = False
            btnFacilityFitting.Visible = False
        End If

        ' Loaded up, let them save it
        btnFacilitySave.Visible = True

        MY_FacilityFullyLoaded = True

        Application.DoEvents()

    End Sub

    ' Sets the sent facility To the one we are selecting And sets the Default 
    Public Sub SetFacility(ByVal SelectedFacility As IndustryFacility, BuildType As ProductionType,
                           ByVal CompareIncludeCostCheck As Boolean, ByVal CompareIncludeTimeCheck As Boolean)

        ' For checking change from stations to pos on  tab
        Dim PreviousFacility As New IndustryFacility

        Select Case BuildType
            Case ProductionType.Manufacturing
                PreviousFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                SelectedManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                ' Set the other three types for pos too
                If SelectedFacility.FacilityType = FacilityTypes.POS Then
                    SelectedFacility.FacilityName = SelectedPOSFuelBlockFacility.FacilityName
                    SelectedFacility.FacilityType = SelectedPOSFuelBlockFacility.FacilityType
                    SelectedPOSFuelBlockFacility = CType(SelectedFacility.Clone, IndustryFacility)

                    SelectedFacility.FacilityName = SelectedPOSLargeShipFacility.FacilityName
                    SelectedFacility.FacilityType = SelectedPOSLargeShipFacility.FacilityType
                    SelectedPOSLargeShipFacility = CType(SelectedFacility.Clone, IndustryFacility)

                    SelectedFacility.FacilityName = SelectedPOSModuleFacility.FacilityName
                    SelectedFacility.FacilityType = SelectedPOSModuleFacility.FacilityType
                    SelectedPOSModuleFacility = CType(SelectedFacility.Clone, IndustryFacility)
                End If
                If SelectedManufacturingFacility.IsEqual(DefaultManufacturingFacility) Then
                    SelectedManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.POSFuelBlockManufacturing
                PreviousFacility = CType(SelectedPOSFuelBlockFacility.Clone, IndustryFacility)
                SelectedPOSFuelBlockFacility = SelectedFacility
                SelectedManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
                If SelectedPOSFuelBlockFacility.IsEqual(DefaultPOSFuelBlockFacility) Then
                    SelectedPOSFuelBlockFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedPOSFuelBlockFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.POSLargeShipManufacturing
                PreviousFacility = CType(SelectedPOSLargeShipFacility.Clone, IndustryFacility)
                SelectedPOSLargeShipFacility = SelectedFacility
                SelectedManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
                If SelectedPOSLargeShipFacility.IsEqual(DefaultPOSLargeShipFacility) Then
                    SelectedPOSLargeShipFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedPOSLargeShipFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.POSModuleManufacturing
                PreviousFacility = CType(SelectedPOSModuleFacility.Clone, IndustryFacility)
                SelectedPOSModuleFacility = SelectedFacility
                SelectedManufacturingFacility = SelectedFacility ' This is also the default POS for everything else, so save
                If SelectedPOSModuleFacility.IsEqual(DefaultPOSModuleFacility) Then
                    SelectedPOSModuleFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedPOSModuleFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.BoosterManufacturing
                PreviousFacility = CType(SelectedBoosterManufacturingFacility.Clone, IndustryFacility)
                SelectedBoosterManufacturingFacility = SelectedFacility
                If SelectedBoosterManufacturingFacility.IsEqual(DefaultBoosterManufacturingFacility) Then
                    SelectedBoosterManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedBoosterManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.CapitalManufacturing
                PreviousFacility = CType(SelectedCapitalManufacturingFacility.Clone, IndustryFacility)
                SelectedCapitalManufacturingFacility = SelectedFacility
                If SelectedCapitalManufacturingFacility.IsEqual(DefaultCapitalManufacturingFacility) Then
                    SelectedCapitalManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedCapitalManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.SuperManufacturing
                PreviousFacility = CType(SelectedSuperManufacturingFacility.Clone, IndustryFacility)
                SelectedSuperManufacturingFacility = SelectedFacility
                If SelectedSuperManufacturingFacility.IsEqual(DefaultSuperManufacturingFacility) Then
                    SelectedSuperManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedSuperManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.T3CruiserManufacturing
                PreviousFacility = CType(SelectedT3CruiserManufacturingFacility.Clone, IndustryFacility)
                SelectedT3CruiserManufacturingFacility = SelectedFacility
                If SelectedT3CruiserManufacturingFacility.IsEqual(DefaultT3CruiserManufacturingFacility) Then
                    SelectedT3CruiserManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedT3CruiserManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.T3DestroyerManufacturing
                PreviousFacility = CType(SelectedT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                SelectedT3DestroyerManufacturingFacility = SelectedFacility
                If SelectedT3DestroyerManufacturingFacility.IsEqual(DefaultT3DestroyerManufacturingFacility) Then
                    SelectedT3DestroyerManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedT3DestroyerManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.SubsystemManufacturing
                PreviousFacility = CType(SelectedSubsystemManufacturingFacility.Clone, IndustryFacility)
                SelectedSubsystemManufacturingFacility = SelectedFacility
                If SelectedSubsystemManufacturingFacility.IsEqual(DefaultSubsystemManufacturingFacility) Then
                    SelectedSubsystemManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedSubsystemManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.ComponentManufacturing
                PreviousFacility = CType(SelectedComponentManufacturingFacility.Clone, IndustryFacility)
                SelectedComponentManufacturingFacility = SelectedFacility
                If SelectedComponentManufacturingFacility.IsEqual(DefaultComponentManufacturingFacility) Then
                    SelectedComponentManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedComponentManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.CapitalComponentManufacturing
                PreviousFacility = CType(SelectedCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                SelectedCapitalComponentManufacturingFacility = SelectedFacility
                If SelectedCapitalComponentManufacturingFacility.IsEqual(DefaultCapitalComponentManufacturingFacility) Then
                    SelectedCapitalComponentManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedCapitalComponentManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.Invention
                PreviousFacility = CType(SelectedInventionFacility.Clone, IndustryFacility)
                SelectedInventionFacility = SelectedFacility
                If SelectedInventionFacility.IsEqual(DefaultInventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                    SelectedInventionFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedInventionFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.T3Invention
                PreviousFacility = CType(SelectedT3InventionFacility.Clone, IndustryFacility)
                SelectedT3InventionFacility = SelectedFacility
                If SelectedT3InventionFacility.IsEqual(DefaultT3InventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                    SelectedT3InventionFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedT3InventionFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.Copying
                PreviousFacility = CType(SelectedCopyFacility.Clone, IndustryFacility)
                SelectedCopyFacility = SelectedFacility
                If SelectedCopyFacility.IsEqual(DefaultCopyFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                    SelectedCopyFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedCopyFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case ProductionType.NoPOSManufacturing
                PreviousFacility = CType(SelectedNoPOSFacility.Clone, IndustryFacility)
                SelectedNoPOSFacility = SelectedFacility
                If SelectedNoPOSFacility.IsEqual(DefaultNoPOSFacility) Then
                    SelectedNoPOSFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedNoPOSFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
            Case Else
                PreviousFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                SelectedManufacturingFacility = SelectedFacility
                If SelectedManufacturingFacility.IsEqual(DefaultManufacturingFacility) Then
                    SelectedManufacturingFacility.IsDefault = True
                    SelectedFacility.IsDefault = True
                Else
                    SelectedManufacturingFacility.IsDefault = False
                    SelectedFacility.IsDefault = False
                End If
        End Select

        ' Set the default 
        Call SetDefaultVisuals(SelectedFacility.IsDefault)

        ' Save the selected facility locally
        MY_SelectedFacility = CType(SelectedFacility.Clone, IndustryFacility)

        lblFacilityDefault.Visible = True

    End Sub

    Private Sub chkFacilityIncludeUsage_CheckedChanged(sender As Object, e As EventArgs) Handles chkFacilityIncludeUsage.CheckedChanged
        Call SetDefaultFacilitybyCheck(MY_SelectedProductionType)

    End Sub

    Private Sub btnFacilityFitting_Click(sender As Object, e As EventArgs) Handles btnFacilityFitting.Click
        Dim CitadelViewer As New frmCitadelFitting(cmbFacilityorArray.Text, MY_SelectedCharacterID,
                                                   MY_SelectedProductionType, MY_SelectedView, MY_SelectedFacility.SolarSystemSecurity)
        CitadelViewer.ShowDialog()

        ' After showing, select the name of the citadel chosen and then dispose
        cmbFacilityorArray.Text = CitadelViewer.CitadelName

        Call CitadelViewer.Dispose()

        ' Reload the stats each time
        Call DisplayFacilityBonus(MY_SelectedProductionType, MY_SelectedBPGroupID, MY_SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)

    End Sub

    Private Sub btnFacilitySave_Click(sender As Object, e As EventArgs) Handles btnFacilitySave.Click
        If MY_FacilityFullyLoaded Then
            If MY_SelectedFacility.SaveFacility(MY_SelectedView, MY_SelectedCharacterID) Then
                ' Just saved, so must be the default
                Call SetDefaultVisuals(True)
            Else
                Call SetDefaultVisuals(False)
            End If
        End If
    End Sub

#Region "Support Functions"

    ' Selects the facility and returns it and the activity through reference
    Private Sub SelectFacility(ByVal BuildType As ProductionType, ByVal IsDefault As Boolean,
                                    ByRef ReturnFacility As IndustryFacility, ByRef ReturnActivity As String)
        If IsDefault Then
            Select Case BuildType
                Case ProductionType.Manufacturing
                    ReturnFacility = CType(DefaultManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.SuperManufacturing
                    ReturnFacility = CType(DefaultSuperManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.CapitalManufacturing
                    ReturnFacility = CType(DefaultCapitalManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.BoosterManufacturing
                    ReturnFacility = CType(DefaultBoosterManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.T3CruiserManufacturing
                    ReturnFacility = CType(DefaultT3CruiserManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.T3DestroyerManufacturing
                    ReturnFacility = CType(DefaultT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.SubsystemManufacturing
                    ReturnFacility = CType(DefaultSubsystemManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.Invention
                    ReturnFacility = CType(DefaultInventionFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityInvention
                Case ProductionType.T3Invention
                    ReturnFacility = CType(DefaultT3InventionFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityInvention
                Case ProductionType.Copying
                    ReturnActivity = ActivityCopying
                    ReturnFacility = CType(DefaultCopyFacility.Clone, IndustryFacility)
                Case ProductionType.NoPOSManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultNoPOSFacility.Clone, IndustryFacility)
                Case ProductionType.ComponentManufacturing
                    ReturnActivity = ActivityComponentManufacturing
                    ReturnFacility = CType(DefaultComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.CapitalComponentManufacturing
                    ReturnActivity = ActivityCapComponentManufacturing
                    ReturnFacility = CType(DefaultCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.POSFuelBlockManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultPOSFuelBlockFacility.Clone, IndustryFacility)
                Case ProductionType.POSLargeShipManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultPOSLargeShipFacility.Clone, IndustryFacility)
                Case ProductionType.POSModuleManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultPOSModuleFacility.Clone, IndustryFacility)
            End Select

        Else
            Select Case BuildType
                Case ProductionType.Manufacturing
                    ReturnFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.SuperManufacturing
                    ReturnFacility = CType(SelectedSuperManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.CapitalManufacturing
                    ReturnFacility = CType(SelectedCapitalManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.BoosterManufacturing
                    ReturnFacility = CType(SelectedBoosterManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.T3CruiserManufacturing
                    ReturnFacility = CType(SelectedT3CruiserManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.T3DestroyerManufacturing
                    ReturnFacility = CType(SelectedT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.SubsystemManufacturing
                    ReturnFacility = CType(SelectedSubsystemManufacturingFacility.Clone, IndustryFacility)
                    ReturnActivity = ActivityManufacturing
                Case ProductionType.Invention
                    ReturnActivity = ActivityInvention
                    ReturnFacility = CType(SelectedInventionFacility.Clone, IndustryFacility)
                Case ProductionType.T3Invention
                    ReturnActivity = ActivityInvention
                    ReturnFacility = CType(SelectedT3InventionFacility.Clone, IndustryFacility)
                Case ProductionType.Copying
                    ReturnActivity = ActivityCopying
                    ReturnFacility = CType(SelectedCopyFacility.Clone, IndustryFacility)
                Case ProductionType.NoPOSManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedNoPOSFacility.Clone, IndustryFacility)
                Case ProductionType.ComponentManufacturing
                    ReturnActivity = ActivityComponentManufacturing
                    ReturnFacility = CType(SelectedComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.CapitalComponentManufacturing
                    ReturnActivity = ActivityCapComponentManufacturing
                    ReturnFacility = CType(SelectedCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.POSFuelBlockManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedPOSFuelBlockFacility.Clone, IndustryFacility)
                Case ProductionType.POSLargeShipManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedPOSLargeShipFacility.Clone, IndustryFacility)
                Case ProductionType.POSModuleManufacturing
                    ReturnActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedPOSModuleFacility.Clone, IndustryFacility)
            End Select
        End If

    End Sub

    ' Returns the type of production done for the activity and bp data sent
    Private Function MY_GetProductionType(Activity As String, FacilityType As FacilityTypes, BPGroupID As Integer, BPCategoryID As Integer) As ProductionType
        Dim SelectedIndyType As ProductionType

        ' Determine if it's a fuel block, module, or big ship that can use a multi-use array in a POS - Need to add as a query, not hard code
        If FacilityType = FacilityTypes.POS And (BPGroupID = 1136 _
                                           Or BPCategoryID = 7 Or BPCategoryID = 20 Or BPCategoryID = 22 Or BPCategoryID = 23 _
                                           Or BPGroupID = 27 Or BPGroupID = 513 Or BPGroupID = 941 _
                                           Or BPGroupID = 12 Or BPGroupID = 340 Or BPGroupID = 448 Or BPGroupID = 649
                                           ) And Activity = MY_ActivityManufacturing Then
            If BPGroupID = 1136 Then
                SelectedIndyType = ProductionType.POSFuelBlockManufacturing
            ElseIf BPGroupID = 27 Or BPGroupID = 513 Or BPGroupID = 941 Then
                SelectedIndyType = ProductionType.POSLargeShipManufacturing
            ElseIf BPCategoryID = 7 Or BPCategoryID = 20 Or BPCategoryID = 22 Or BPCategoryID = 23 _
                Or BPGroupID = 12 Or BPGroupID = 340 Or BPGroupID = 448 Or BPGroupID = 649 Then
                SelectedIndyType = ProductionType.POSModuleManufacturing
            End If
        Else
            Select Case Activity
                ' TODO look into making these a lookup with the facility type if there are category or groupid's in the tables for them
                Case MY_ActivityManufacturing
                    ' Need to load selected manufacturing facility
                    Select Case BPGroupID
                        Case ItemIDs.SupercarrierGroupID, ItemIDs.TitanGroupID
                            SelectedIndyType = ProductionType.SuperManufacturing
                        Case ItemIDs.BoosterGroupID
                            SelectedIndyType = ProductionType.BoosterManufacturing
                        Case ItemIDs.CarrierGroupID, ItemIDs.DreadnoughtGroupID, ItemIDs.CapitalIndustrialShipGroupID, ItemIDs.FAXGroupID
                            SelectedIndyType = ProductionType.CapitalManufacturing
                        Case ItemIDs.StrategicCruiserGroupID
                            SelectedIndyType = ProductionType.T3CruiserManufacturing
                        Case ItemIDs.TacticalDestroyerGroupID
                            SelectedIndyType = ProductionType.T3DestroyerManufacturing
                        Case Else
                            SelectedIndyType = ProductionType.Manufacturing

                            If BPCategoryID = ItemIDs.SubsystemCategoryID Then
                                SelectedIndyType = ProductionType.SubsystemManufacturing
                            ElseIf BPCategoryID = ItemIDs.ComponentCategoryID Then
                                ' Add category for component
                                If BPGroupID = ItemIDs.CapitalComponentGroupID Or BPGroupID = ItemIDs.AdvCapitalComponentGroupID Then
                                    SelectedIndyType = ProductionType.CapitalComponentManufacturing ' These all use cap components
                                Else
                                    SelectedIndyType = ProductionType.ComponentManufacturing
                                End If
                            ElseIf NoPOSCategoryIDs.Contains(BPCategoryID) Or BPGroupID = ItemIDs.StationEggGroupID Then
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
                    If BPCategoryID = ItemIDs.SubsystemCategoryID Or BPGroupID = ItemIDs.StrategicCruiserGroupID Then
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

    ' Returns the SQL string for querying by category or group id's 
    Private Function GetFacilityCatGroupIDSQL(ByVal CategoryID As Integer, ByVal GroupID As Integer, ByVal Activity As IndustryActivities) As String
        Dim SQL As String = ""
        Dim TempGroupID As Integer
        Dim TempCategoryID As Integer

        ' If the categoryID or groupID is for T3 invention, then switch the item ID's to the blueprint groupID for that item to match CCP's logic in table
        If Activity = IndustryActivities.Invention Then
            If CategoryID = SubsystemCategoryID Then
                TempGroupID = SubsystemBPGroupID
                TempCategoryID = 0
            ElseIf GroupID = StrategicCruiserGroupID Then
                TempGroupID = StrategicCruiserBPGroupID
                TempCategoryID = 0
            ElseIf GroupID = TacticalDestroyerGroupID Then
                TempGroupID = TacticalDestroyerBPGroupID
                TempCategoryID = 0
            Else
                TempGroupID = GroupID
                TempCategoryID = CategoryID
            End If
        Else
            TempGroupID = GroupID
            TempCategoryID = CategoryID
        End If

        SQL = "AND (GROUP_ID = " & CStr(TempGroupID) & " OR (GROUP_ID = 0 AND CATEGORY_ID = " & CStr(TempCategoryID) & ")) "

        Return SQL

    End Function

    ' Sets all the combos to unenabled and base text to show no facility for stuff like Invention, Copy and RE where they might buy the item
    Private Sub SetNoFacility()
        cmbFacilityRegion.Items.Clear()
        cmbFacilityRegion.Text = "Select Region"
        cmbFacilityRegion.Enabled = False
        cmbFacilitySystem.Items.Clear()
        cmbFacilitySystem.Text = "Select System"
        cmbFacilitySystem.Enabled = False
        cmbFacilityorArray.Items.Clear()
        cmbFacilityorArray.Text = "Select Facility / Array"
        chkFacilityIncludeUsage.Enabled = False

        If Not IsNothing(chkFacilityIncludeCost) Then
            chkFacilityIncludeCost.Enabled = False
        End If
        If Not IsNothing(chkFacilityIncludeTime) Then
            chkFacilityIncludeTime.Enabled = False
        End If
        cmbFacilityorArray.Enabled = False
        If Not IsNothing(chkFacilityToggle) Then
            chkFacilityToggle.Enabled = False
        End If
    End Sub

    ' Sets the visual data for default facility
    Private Sub SetDefaultVisuals(isDefault As Boolean)
        If isDefault = True Then
            lblFacilityDefault.ForeColor = FacilityLabelDefaultColor
            Call ResetToolTipforDefaultFacilityLabel(False)
            btnFacilitySave.Enabled = False ' don't enable since it's already the default, it's pointless to save it
        Else
            lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
            Call ResetToolTipforDefaultFacilityLabel(True)
            btnFacilitySave.Enabled = True
        End If
    End Sub

    ' Translates the string facility type into the enum code
    Private Function GetFacilityTypeCode(FacilityType As String) As FacilityTypes
        Dim rsLookup As SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT FACILITY_TYPE_ID FROM FACILITY_TYPES WHERE FACILITY_TYPE_NAME = '" & FacilityType & "'", EVEDB.DBREf)
        rsLookup = DBCommand.ExecuteReader
        If rsLookup.Read() Then
            Return CType(rsLookup.GetInt32(0), FacilityTypes)
        Else
            Return FacilityTypes.None
        End If

    End Function

    ' Translates facility code into name
    Private Function GetFacilityNameCode(FacilityType As FacilityTypes) As String
        Dim rsLookup As SQLiteDataReader

        DBCommand = New SQLiteCommand("SELECT FACILITY_TYPE_NAME FROM FACILITY_TYPES WHERE FACILITY_TYPE_ID = " & CInt(FacilityType), EVEDB.DBREf)
        rsLookup = DBCommand.ExecuteReader
        If rsLookup.Read() Then
            Return rsLookup.GetString(0)
        Else
            Return ""
        End If

    End Function

    ' Hides all the facility bonus boxes and such
    Private Sub SetFacilityBonusBoxes(ByVal Value As Boolean)

        txtFacilityManualME.Visible = Value
        txtFacilityManualTE.Visible = Value
        txtFacilityManualTax.Visible = Value
        txtFacilityManualCost.Visible = Value

        lblFacilityManualME.Visible = Value
        lblFacilityManualTE.Visible = Value
        lblFacilityManualTax.Visible = Value
        lblFacilityManualCost.Visible = Value

        ' Clear the usage until these are set
        If Not IsNothing(lblFacilityUsage) Then
            lblFacilityUsage.Text = ""
        End If

    End Sub

    ' Resets all combo boxes toggles that might need to be updated 
    Private Sub ResetComboLoadVariables(RegionsValue As Boolean, SystemsValue As Boolean, FacilitiesValue As Boolean)

        MY_FacilityRegionsLoaded = RegionsValue
        MY_FacilitySystemsLoaded = SystemsValue
        MY_FacilityorArrayLoaded = FacilitiesValue

    End Sub

    ' Returns the POS manufacturing facility for the type sent
    Private Function GetPOSManufacturingFacility(ByVal SentFacility As IndustryFacility) As IndustryFacility
        'Dim IndyType As ProductionType

        'IndyType = MY_GetProductionType(ActivityManufacturing, POSFacility)

        Select Case MY_SelectedProductionType
            Case ProductionType.POSFuelBlockManufacturing
                Return SelectedBPPOSFuelBlockFacility
            Case ProductionType.POSLargeShipManufacturing
                Return SelectedBPPOSLargeShipFacility
            Case ProductionType.POSModuleManufacturing
                Return SelectedBPPOSModuleFacility
            Case Else
                Return SentFacility
        End Select

    End Function

    ' Sets the tool tip text for default facility labels if they can double click to reload
    Private Sub ResetToolTipforDefaultFacilityLabel(ByVal ShowTip As Boolean)
        If Not IsNothing(mainToolTip) Then
            If ShowTip And UserApplicationSettings.ShowToolTips Then
                mainToolTip.SetToolTip(lblFacilityDefault, "Double-Click to reload default facility")
            Else
                mainToolTip.SetToolTip(lblFacilityDefault, "")
            End If
        End If
    End Sub

    ' Sets the default facility based on a check change
    Public Sub SetDefaultFacilitybyCheck(IndustryType As ProductionType,
                                         Optional CompareTime As Boolean = False,
                                         Optional CompareCost As Boolean = False)
        Dim SelectedFacility As New IndustryFacility
        Dim SelectedActivity As String = ""

        Call SelectFacility(IndustryType, False, SelectedFacility, SelectedActivity)

        SelectedFacility.IncludeActivityUsage = chkFacilityIncludeUsage.Checked

        ' Set the default based on the checkbox 
        Call SetFacility(SelectedFacility, IndustryType, CompareCost, CompareTime)

    End Sub

    ' Allows outside the form to update the rig list when saved for the selected facility
    Public Sub UpdateRigs(RigList As List(Of Integer))
        MY_SelectedFacility.InstalledModules = RigList
    End Sub

#End Region

End Class

' What type of view are we looking at
Public Enum FacilityView
    FullControls = 0 ' for BP tab right now
    LimitedControls = 1 ' for use on manufacturing tab now
    NoView = 2 ' For not connecting this to a tab or facilty view
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

Public Enum ItemIDs
    ' These are all the capital ships that use capital parts
    CapitalIndustrialShipGroupID = 883
    CarrierGroupID = 547
    DreadnoughtGroupID = 485
    FreighterGroupID = 513
    IndustrialCommandShipGroupID = 941
    JumpFreighterGroupID = 902
    SupercarrierGroupID = 659
    FAXGroupID = 1538
    TitanGroupID = 30
    BoosterGroupID = 303

    ' T3 items
    StrategicCruiserGroupID = 963
    TacticalDestroyerGroupID = 1305
    SubsystemCategoryID = 32

    ' T3 Bps for facility updates
    StrategicCruiserBPGroupID = 996
    TacticalDestroyerBPGroupID = 1309
    SubsystemBPGroupID = 973

    ' For looking up pos stuff in facilities
    FuelBlockGroupID = 1136
    BattleshipGroupID = 27
    ModuleCategoryID = 7

    ShipCategoryID = 6 ' for loading invention and copying 

    ConstructionComponentsGroupID = 334 ' Use this for all non-capital components
    ComponentCategoryID = 17
    CapitalComponentGroupID = 873
    AdvCapitalComponentGroupID = 913

    ' Categories (has multiple groups)
    StationEggGroupID = 307 ' This is for loading No POS build items
    SovStructureCategoryID = 3 ' For stations - I don't think this is used anymore (everything can be built at a pos?)
    StationPartsGroupID = 536

End Enum

Public Enum FacilityTypes
    None = -1
    Station = 0
    POS = 1
    Outpost = 2
    UpwellStructures = 3
End Enum

' Industry facility class, move to private use if possible
Public Class IndustryFacility
    Implements ICloneable

    ' For industry Facilities
    Public FacilityID As Long ' ID Of the facility
    Public FacilityName As String ' Station/Outpost Name or the Array name
    Public FacilityType As FacilityTypes ' POS, Station, Outpost, Upwell Structure
    Public FacilityTypeID As Integer ' type ID for facility - type of outpost, etc
    Public FacilityProductionType As ProductionType ' What we are doing at this facility
    Public ActivityID As Integer ' Activity code of the facility
    Public RegionName As String ' Region of this facility
    Public RegionID As Long
    Public SolarSystemName As String ' System where this is located
    Public SolarSystemID As Long
    Public SolarSystemSecurity As Double
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

    ' Default multiplier rates if we can't find them
    Public Const DefaultTaxRate As Double = 0
    Public Const DefaultMaterialMultiplier As Double = 1
    Public Const DefaultTimeMultiplier As Double = 1
    Public Const DefaultCostMultiplier As Double = 1

    Public Sub New()

        FacilityID = 0
        FacilityName = None
        FacilityType = FacilityTypes.None
        FacilityProductionType = ProductionType.None
        ActivityID = 0
        RegionName = None
        RegionID = 0
        SolarSystemName = None
        SolarSystemID = 0
        SolarSystemSecurity = 0
        FWUpgradeLevel = 0
        CostIndex = 0
        MaterialMultiplier = 0
        TimeMultiplier = 0
        CostMultiplier = 0
        ActivityCostPerSecond = 0
        TaxRate = 0

        IncludeActivityCost = False
        IncludeActivityTime = False
        IncludeActivityUsage = False

        InstalledModules = New List(Of Integer)

        IsDefault = False

    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New IndustryFacility

        CopyOfMe.FacilityID = FacilityID
        CopyOfMe.FacilityName = FacilityName
        CopyOfMe.FacilityType = FacilityType
        CopyOfMe.FacilityTypeID = FacilityTypeID
        CopyOfMe.FacilityProductionType = FacilityProductionType
        CopyOfMe.ActivityID = ActivityID
        CopyOfMe.RegionName = RegionName
        CopyOfMe.RegionID = RegionID
        CopyOfMe.SolarSystemName = SolarSystemName
        CopyOfMe.SolarSystemID = SolarSystemID
        CopyOfMe.SolarSystemSecurity = SolarSystemSecurity
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
    Public Sub InitalizeFacility(InitialProductionType As ProductionType, FacilityTab As FacilityView)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        ' Look up all the data in two queries - first base data and try to get the multipliers and cost data - it should only be there for saved outposts (which are being removed)
        SQL = "SELECT SF.FACILITY_ID, SF.FACILITY_TYPE, SF.FACILITY_TYPE_ID, "
        SQL &= "FACILITY_PRODUCTION_TYPES.ACTIVITY_ID, "
        SQL &= "REGIONS.regionName, REGIONS.regionID, SOLAR_SYSTEMS.solarSystemName, SOLAR_SYSTEMS.solarSystemID, "
        SQL &= "CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL, SF.ACTIVITY_COST_PER_SECOND, "
        SQL &= "CASE WHEN COST_INDEX IS NULL THEN 0 ELSE COST_INDEX END AS COST_INDEX,"
        SQL &= "SF.INCLUDE_ACTIVITY_COST, SF.INCLUDE_ACTIVITY_TIME, SF.INCLUDE_ACTIVITY_USAGE, "
        SQL &= "SF.FACILITY_TAX, SF.MATERIAL_MULTIPLIER, SF.TIME_MULTIPLIER, SF.COST_MULTIPLIER, security "
        SQL &= "FROM SAVED_FACILITIES AS SF, FACILITY_PRODUCTION_TYPES, REGIONS, SOLAR_SYSTEMS, FACILITY_TYPES "
        SQL &= "LEFT JOIN FW_SYSTEM_UPGRADES On FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID "
        SQL &= "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES "
        SQL &= "ON INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = FACILITY_PRODUCTION_TYPES.ACTIVITY_ID "
        SQL &= "WHERE SF.PRODUCTION_TYPE = FACILITY_PRODUCTION_TYPES.PRODUCTION_TYPE "
        SQL &= "AND SF.REGION_ID = REGIONS.regionID "
        SQL &= "AND SF.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND SF.FACILITY_TYPE = FACILITY_TYPES.FACILITY_TYPE_ID "
        SQL &= String.Format("AND SF.PRODUCTION_TYPE = {0} AND SF.FACILITY_VIEW = {1} ", CStr(InitialProductionType), CStr(FacilityTab))

        Dim SQLCharID As String = "AND CHARACTER_ID = {0}"
        Dim UsedCharID As String = ""

        ' First look up the character to see if it's saved there first (initially only do one set of facilities then allow by character via a setting)
        DBCommand = New SQLiteCommand(SQL & String.Format(SQLCharID, CStr(SelectedCharacter.ID)), EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader
        rsLoader.Read()

        ' Save for use later if needed
        UsedCharID = CStr(SelectedCharacter.ID)

        If Not rsLoader.HasRows Then
            ' Need to look up the default
            rsLoader.Close()
            DBCommand = New SQLiteCommand(SQL & String.Format(SQLCharID, "0"), EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            rsLoader.Read()
            UsedCharID = "0"
        End If

        ' Should have data one way or another now
        If rsLoader.HasRows Then
            With rsLoader
                FacilityID = .GetInt32(0)
                FacilityType = CType(.GetInt32(1), FacilityTypes) ' Station, Upwell Structure, etc.
                FacilityTypeID = .GetInt32(2)
                FacilityProductionType = InitialProductionType
                ActivityID = .GetInt32(3)
                RegionName = .GetString(4)
                RegionID = .GetInt64(5)
                SolarSystemName = .GetString(6)
                SolarSystemID = .GetInt64(7)
                SolarSystemSecurity = .GetDouble(18)
                FWUpgradeLevel = .GetInt32(8)
                ActivityCostPerSecond = .GetFloat(9)
                CostIndex = .GetFloat(10)
                IncludeActivityCost = CBool(.GetInt32(11))
                IncludeActivityTime = CBool(.GetInt32(12))
                IncludeActivityUsage = CBool(.GetInt32(13))

                ' Save these values for later lookup - use -1 for null indicator
                If IsDBNull(.GetValue(14)) Then
                    TaxRate = -1
                Else
                    TaxRate = .GetDouble(14)
                End If

                If IsDBNull(.GetValue(15)) Then
                    MaterialMultiplier = -1
                Else
                    MaterialMultiplier = .GetDouble(15)
                End If

                If IsDBNull(.GetValue(16)) Then
                    TimeMultiplier = -1
                Else
                    TimeMultiplier = .GetDouble(16)
                End If

                If IsDBNull(.GetValue(17)) Then
                    CostMultiplier = -1
                Else
                    CostMultiplier = .GetDouble(17)
                End If

                ' Now, depending on type, look up the name, cost index, tax, and multipliers from the station_facilities table (this is mainly for speed)
                If FacilityType = FacilityTypes.POS Then
                    SQL = "SELECT DISTINCT ARRAY_NAME, 0 AS FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                    SQL = SQL & "FROM ASSEMBLY_ARRAYS WHERE ARRAY_TYPE_ID = " & CStr(FacilityID) & " "
                ElseIf FacilityType = FacilityTypes.Station Or FacilityType = FacilityTypes.Outpost Then ' Stations, outposts
                    SQL = "SELECT DISTINCT FACILITY_NAME, FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                    SQL = SQL & "FROM STATION_FACILITIES WHERE FACILITY_ID = " & CStr(FacilityID) & " "
                ElseIf FacilityType = FacilityTypes.UpwellStructures Then
                    SQL = "SELECT DISTINCT UPWELL_STRUCTURE_NAME, 0 AS FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                    SQL = SQL & "FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_TYPE_ID = " & CStr(FacilityID) & " "
                End If

                SQL = SQL & "AND ACTIVITY_ID = " & CStr(ActivityID) & " "

                rsLoader.Close()
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                If rsLoader.HasRows Then
                    With rsLoader
                        FacilityName = .GetString(0) ' Need to deal with the case of all on calc base facility

                        ' For the remaining values, if they saved a value manually, then use that, else use what was queried and if null, use the default
                        If IsDBNull(.GetValue(1)) And TaxRate = -1 Then
                            ' Nothing in DB and they didn't save anything, so use the default rate
                            TaxRate = DefaultTaxRate
                        ElseIf TaxRate = -1 Then
                            ' use what was in db
                            TaxRate = .GetDouble(1)
                        End If

                        If IsDBNull(.GetValue(2)) And MaterialMultiplier = -1 Then
                            MaterialMultiplier = DefaultMaterialMultiplier
                        ElseIf MaterialMultiplier = -1 Then
                            MaterialMultiplier = .GetDouble(2)
                        End If

                        If IsDBNull(.GetValue(3)) And TimeMultiplier = -1 Then
                            TimeMultiplier = DefaultTimeMultiplier
                        ElseIf TimeMultiplier = -1 Then
                            TimeMultiplier = .GetDouble(3)
                        End If

                        If IsDBNull(.GetValue(4)) And CostMultiplier = -1 Then
                            CostMultiplier = DefaultCostMultiplier
                        ElseIf CostMultiplier = -1 Then
                            CostMultiplier = .GetDouble(4)
                        End If
                    End With
                Else
                    ' Something went wrong
                    MsgBox("The facility failed To load", vbCritical, Application.ProductName)
                    GoTo ExitBlock
                End If

                ' If this is a upwell structure, then look up any saved modules
                If FacilityType = FacilityTypes.UpwellStructures Then
                    rsLoader.Close()
                    SQL = "SELECT INSTALLED_MODULE_ID FROM FACILITY_INSTALLED_MODULES "
                    SQL &= "WHERE CHARACTER_ID = {0} AND INDUSTRY_TYPE = {1} AND FACILITY_VIEW = {2}"
                    DBCommand = New SQLiteCommand(String.Format(SQL, UsedCharID, CStr(InitialProductionType), CStr(FacilityTab)), EVEDB.DBREf)
                    rsLoader = DBCommand.ExecuteReader
                    rsLoader.Read()

                    InstalledModules = New List(Of Integer)

                    While rsLoader.Read()
                        InstalledModules.Add(rsLoader.GetInt32(0))
                    End While
                End If

                IsDefault = True ' Always loading default with initialize

            End With

        Else
            ' Something went wrong
            MsgBox("The facility failed To load", vbCritical, Application.ProductName)
            GoTo ExitBlock
        End If

ExitBlock:
        On Error Resume Next
        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing
        On Error GoTo 0

    End Sub

    Public Function SaveFacility(ViewType As FacilityView, CharacterID As Long) As Boolean
        Dim SQL As String
        Dim TempSQL As String
        Dim rsCheck As SQLiteDataReader

        Try

            ' See if the record exists - only save one set of facilities for now
            SQL = String.Format("Select 'X' FROM SAVED_FACILITIES WHERE PRODUCTION_TYPE = {0} AND FACILITY_VIEW = {1} AND CHARACTER_ID = {2}",
                            CInt(FacilityProductionType), CInt(ViewType), CharacterID)
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsCheck = DBCommand.ExecuteReader

            If rsCheck.Read() Then
                ' Need to update
                TempSQL = "UPDATE SAVED_FACILITIES "
                TempSQL &= "SET FACILITY_ID = {0}, "
                TempSQL &= "FACILITY_TYPE = {1}, "
                TempSQL &= "FACILITY_TYPE_ID = {2}, "
                TempSQL &= "REGION_ID = {3}, "
                TempSQL &= "SOLAR_SYSTEM_ID = {4}, "
                TempSQL &= "ACTIVITY_COST_PER_SECOND = {5}, "
                TempSQL &= "INCLUDE_ACTIVITY_COST = {6}, "
                TempSQL &= "INCLUDE_ACTIVITY_TIME = {7}, "
                TempSQL &= "INCLUDE_ACTIVITY_USAGE = {8}, "
                TempSQL &= "FACILITY_TAX = {9}, "
                TempSQL &= "MATERIAL_MULTIPLIER = {10}, "
                TempSQL &= "TIME_MULTIPLIER = {11}, "
                TempSQL &= "COST_MULTIPLIER = {12} "
                TempSQL &= "WHERE PRODUCTION_TYPE = {13} AND FACILITY_VIEW = {14} AND CHARACTER_ID = {15}"

                SQL = String.Format(TempSQL, FacilityID, CInt(FacilityType), FacilityTypeID,
                        RegionID, SolarSystemID, ActivityCostPerSecond,
                        CInt(IncludeActivityCost), CInt(IncludeActivityTime), CInt(IncludeActivityUsage),
                        TaxRate, MaterialMultiplier, TimeMultiplier, CostMultiplier,
                        CInt(FacilityProductionType), CInt(ViewType), CharacterID)
            Else
                ' Insert
                SQL = String.Format("INSERT INTO SAVED_FACILITIES VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})",
                                    CharacterID, CInt(FacilityProductionType), CInt(ViewType), FacilityID, FacilityType, FacilityTypeID,
                                    RegionID, SolarSystemID, ActivityCostPerSecond,
                                    CInt(IncludeActivityCost), CInt(IncludeActivityTime), CInt(IncludeActivityUsage),
                                    TaxRate, MaterialMultiplier, TimeMultiplier, CostMultiplier)
            End If

            ' Commit the change
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            Call MsgBox("Facility Saved", vbInformation, Application.ProductName)

            Return True

        Catch ex As Exception
            Call MsgBox("Facility Failed to Save", vbExclamation, Application.ProductName)
            Return False
        End Try

    End Function

    ' Compares the sent facility To the current one And returns a Boolean On equivlancy
    Public Function IsEqual(CompareFacility As IndustryFacility,
                            Optional CompareCostCheck As Boolean = False,
                            Optional CompareTimeCheck As Boolean = False) As Boolean

        With CompareFacility
            If .FacilityType <> FacilityType Then
                Return False
            ElseIf .FacilityTypeID <> FacilityTypeID Then
                Return False
            ElseIf .FacilityProductionType <> FacilityProductionType Then
                Return False
            ElseIf .FacilityName <> FacilityName And Not (.FacilityType = FacilityTypes.pos And FacilityProductionType = ProductionType.Manufacturing) Then
                Return False
            ElseIf .RegionName <> RegionName Then
                Return False
            ElseIf .RegionID <> RegionID Then
                Return False
            ElseIf .SolarSystemName <> SolarSystemName Then
                Return False
            ElseIf .SolarSystemID <> SolarSystemID Then
                Return False
                'ElseIf .FWUpgradeLevel <> FWUpgradeLevel Then
                '    Return False
            ElseIf .TaxRate <> TaxRate Then
                Return False
            ElseIf .MaterialMultiplier <> MaterialMultiplier And .FacilityType <> FacilityTypes.pos Then ' Only for non-pos
                Return False
            ElseIf .TimeMultiplier <> TimeMultiplier And .FacilityType <> FacilityTypes.pos Then ' Only for non-pos
                Return False
            ElseIf .CostMultiplier <> CostMultiplier And .FacilityType <> FacilityTypes.pos Then ' Only for non-pos
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

End Class
