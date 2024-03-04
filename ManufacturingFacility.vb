Imports System.Data.SQLite

Public Class ManufacturingFacility

    Private SelectedFacility As IndustryFacility ' This is the active facility for the control, if not loaded will use the default
    Private SelectedLocation As ProgramLocation
    Private SelectedCharacterID As Long
    Private SelectedProductionType As ProductionType
    Private SelectedControlForm As Form ' Where the control lives

    Private SelectedBPTech As Integer
    Private SelectedBPID As Integer
    Private SelectedBPGroupID As Integer
    Private SelectedBPCategoryID As Integer

    ' To check if we are loading and stop click events when changing values
    Private LoadingActivities As Boolean
    Private LoadingFacilityTypes As Boolean
    Private LoadingRegions As Boolean
    Private LoadingSystems As Boolean
    Private LoadingFacilities As Boolean
    Private ChangingUsageChecks As Boolean
    Private UpdatingFWComboText As Boolean

    ' To save previous values for checking and loading
    Private PreviousProductionType As ProductionType
    Private PreviousFacilityType As FacilityTypes
    Private PreviousRegion As String
    Private PreviousSystem As String
    Private PreviousEquipment As String
    Private PreviousActivity As String

    ' Loaded variables
    Private FacilityRegionsLoaded As Boolean
    Private FacilitySystemsLoaded As Boolean
    Private FacilityLoaded As Boolean

    Private UpdatingManualBoxes As Boolean

    ' Save these options here in the facility and allow functions to get the values publically
    Private FacilityIncludeCopyCost As Boolean
    Private FacilityIncludeCopyTime As Boolean
    Private FacilityIncludeInventionCost As Boolean
    Private FacilityIncludeInventionTime As Boolean

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
    Private SelectedReactionsFacility As New IndustryFacility

    Private SelectedReprocessingFacility As New IndustryFacility

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
    Private DefaultReactionsFacility As New IndustryFacility
    Private DefaultRefiningFacility As New IndustryFacility

    ' Constant activities
    Public Const ActivityManufacturing As String = "Manufacturing"
    Public Const ActivityComponentManufacturing As String = "Component Manufacturing"
    Public Const ActivityCapComponentManufacturing As String = "Cap Component Manufacturing"
    Public Const ActivityCopying As String = "Copying"
    Public Const ActivityInvention As String = "Invention"
    Public Const ActivityReactions As String = "Reactions"
    Public Const ActivityReprocessing As String = "Reprocessing"

    ' For verifying activity and facility type combos selected something
    Private Const InitialTypeComboText = "Select Type"
    Private Const InitialActivityComboText = "Select Activity"
    Private Const InitialRegionComboText = "Select Region"
    Private Const InitialSolarSystemComboText = "Select System"
    Private Const InitialFacilityComboText = "Select Facility"

    Public Const StationFacility As String = "Station"
    Public Const StructureFacility As String = "Structure"

    Private FactionCitadelList As New List(Of Integer)

    Private FacilityLabelDefaultColor As Color = SystemColors.Highlight
    Private FacilityLabelNonDefaultColor As Color = SystemColors.ButtonShadow

    Private Enum StationServices
        ReprocessingPlant = 5
        Factory = 14
        Laboratory = 15
    End Enum

    Public Sub New()
        FirstLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Hide everything until constructed with the options sent
        cmbFacilityActivities.Visible = False
        lblFacilityActivity.Visible = False
        lblFacilityUsage.Visible = False
        chkFacilityToggle.Visible = False
        lblFacilityFWUpgrade.Visible = False
        cmbFacilityFWUpgrade.Visible = False
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
        cmbFacility.Visible = False
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

        UpdatingManualBoxes = False

        ' For checking facilities later - will remove if we can get locations from ESI
        FactionCitadelList.Add(47512) ' Moreau Fortizar
        FactionCitadelList.Add(47513) ' Draccous Fortizar
        FactionCitadelList.Add(47514) ' Horizion Fortizar
        FactionCitadelList.Add(47515) ' Marginis Fortizar
        FactionCitadelList.Add(47516) ' Prometheus Fortizar

        FirstLoad = False

        SelectedFacility = New IndustryFacility

    End Sub

    ' Before any controls are shown, the control needs to be initilaized by sending the view type.
    Public Sub InitializeControl(ByVal SentSelectedCharacterID As Long, FormLocation As ProgramLocation,
                                 ByVal InitialProductionType As ProductionType, ByRef ControlForm As Form)
        Const SolarSystemWidthBP As Integer = 142
        Const SolarSystemWidthCalc As Integer = 157

        Const RegionWidthBP As Integer = 130
        Const RegionWidthCalc As Integer = 133

        Const FacilityArrayWidthBP As Integer = 274
        Const FacilityArrayWidthCalc As Integer = 295

        Const LeftObjectLocation As Integer = 3
        Const LeftLabelLocation As Integer = 1

        Const DefaultLabelWidthBP As Integer = 48
        Const DefaultLabelHeightBP As Integer = 34

        ' Save for later
        SelectedLocation = FormLocation
        SelectedProductionType = InitialProductionType
        SelectedCharacterID = SentSelectedCharacterID
        SelectedControlForm = ControlForm

        ' Move and show the selected controls depending on the view sent
        Select Case FormLocation
            Case ProgramLocation.BlueprintTab
                lblFacilityActivity.Top = 1
                lblFacilityActivity.Left = LeftLabelLocation
                lblFacilityActivity.Visible = True

                cmbFacilityActivities.Top = lblFacilityActivity.Top + lblFacilityActivity.Height + 1
                cmbFacilityActivities.Left = LeftObjectLocation
                cmbFacilityActivities.Visible = True
                cmbFacilityActivities.Text = InitialActivityComboText

                lblFacilityType.Top = 1
                lblFacilityType.Left = cmbFacilityActivities.Left + cmbFacilityActivities.Width
                lblFacilityType.Visible = True

                cmbFacilityType.Top = cmbFacilityActivities.Top
                cmbFacilityType.Left = cmbFacilityActivities.Left + cmbFacilityActivities.Width + 2
                cmbFacilityType.Visible = True
                cmbFacilityType.Text = InitialTypeComboText

                lblFacilityDefault.Height = DefaultLabelHeightBP
                lblFacilityDefault.Width = DefaultLabelWidthBP
                Call lblFacilityDefault.SendToBack()
                lblFacilityDefault.Top = lblFacilityType.Top + 5
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
                cmbFacilityRegion.Width = RegionWidthBP
                cmbFacilityRegion.Text = InitialRegionComboText
                cmbFacilityRegion.Visible = True

                cmbFacilitySystem.Top = cmbFacilityRegion.Top
                cmbFacilitySystem.Left = cmbFacilityRegion.Left + cmbFacilityRegion.Width + 2
                cmbFacilitySystem.Width = SolarSystemWidthBP
                cmbFacilitySystem.Text = InitialSolarSystemComboText
                cmbFacilitySystem.Visible = True

                cmbFacility.Top = cmbFacilityRegion.Top + cmbFacilityRegion.Height + 1
                cmbFacility.Left = LeftObjectLocation
                cmbFacility.Width = FacilityArrayWidthBP
                cmbFacility.Text = InitialFacilityComboText
                cmbFacility.Visible = True

                btnFacilitySave.Top = cmbFacility.Top + cmbFacility.Height
                btnFacilitySave.Left = (cmbFacility.Left + cmbFacility.Width) - btnFacilitySave.Width + 1
                btnFacilitySave.Visible = True
                btnFacilitySave.Enabled = False

                btnFacilityFitting.Top = btnFacilitySave.Top
                btnFacilityFitting.Left = btnFacilitySave.Left - (btnFacilityFitting.Width + 2)
                btnFacilityFitting.Visible = False
                btnFacilityFitting.Enabled = False

                Call LoadManualBoxes(InitialProductionType, FormLocation)

                ' Set initial settings to load 
                If SelectedBPID = 0 Then
                    SelectedBPCategoryID = ItemIDs.ShipCategoryID
                    SelectedBPGroupID = ItemIDs.FrigateGroupID
                    SelectedBPTech = BPTechLevel.T1
                Else
                    ' Set based on BP
                    Dim rsBP As SQLiteDataReader
                    DBCommand = New SQLiteCommand(String.Format("SELECT ITEM_GROUP_ID, ITEM_CATEGORY_ID, TECH_LEVEL FROM ALL_BLUEPRINTS_FACT WHERE BLUEPRINT_ID = {0}", SelectedBPID), EVEDB.DBREf)
                    rsBP = DBCommand.ExecuteReader
                    rsBP.Read()
                    SelectedBPGroupID = rsBP.GetInt32(0)
                    SelectedBPCategoryID = rsBP.GetInt32(1)
                    SelectedBPTech = rsBP.GetInt32(2)
                    rsBP.Close()
                End If

            Case ProgramLocation.ManufacturingTab

                cmbFacilityActivities.Visible = False

                lblFacilityType.Top = 5
                lblFacilityType.Left = 0
                lblFacilityType.Visible = True

                cmbFacilityType.Top = lblFacilityType.Top - 2
                cmbFacilityType.Left = lblFacilityType.Width + 2
                cmbFacilityType.Visible = True
                cmbFacilityType.Text = InitialTypeComboText

                chkFacilityToggle.Top = lblFacilityType.Top - 2
                chkFacilityToggle.Left = cmbFacilityType.Left + cmbFacilityType.Width + 5

                chkFacilityIncludeUsage.Visible = True
                chkFacilityIncludeUsage.Left = chkFacilityToggle.Left
                chkFacilityIncludeUsage.Top = chkFacilityToggle.Top + chkFacilityToggle.Height - 1

                chkFacilityIncludeUsage.Text = "Include Usage"
                chkFacilityIncludeTime.Visible = False
                chkFacilityIncludeCost.Visible = False
                lblInclude.Visible = False

                Select Case InitialProductionType
                    Case ProductionType.CapitalComponentManufacturing, ProductionType.ComponentManufacturing
                        chkFacilityToggle.Visible = True
                        chkFacilityToggle.Text = "Cap Parts"
                        If InitialProductionType = ProductionType.CapitalComponentManufacturing Then
                            chkFacilityToggle.Checked = True
                        End If
                    Case ProductionType.T3DestroyerManufacturing, ProductionType.T3CruiserManufacturing
                        chkFacilityToggle.Visible = True
                        chkFacilityToggle.Text = "Destroyers"
                        If InitialProductionType = ProductionType.T3DestroyerManufacturing Then
                            chkFacilityToggle.Checked = True
                        End If
                    Case ProductionType.Invention, ProductionType.T3Invention, ProductionType.Copying
                        chkFacilityIncludeUsage.Text = "Usage"
                        lblInclude.Top = lblFacilityType.Top
                        lblInclude.Left = chkFacilityIncludeUsage.Left - 2
                        lblInclude.Visible = True
                        chkFacilityIncludeCost.Top = chkFacilityIncludeUsage.Top
                        chkFacilityIncludeCost.Left = chkFacilityIncludeUsage.Left + chkFacilityIncludeUsage.Width
                        chkFacilityIncludeCost.Visible = True
                        chkFacilityIncludeTime.Top = chkFacilityIncludeUsage.Top
                        chkFacilityIncludeTime.Left = chkFacilityIncludeCost.Left + chkFacilityIncludeCost.Width
                        chkFacilityIncludeTime.Visible = True
                    Case Else
                        chkFacilityToggle.Visible = False
                End Select

                lblFacilityLocation.Visible = True
                lblFacilityLocation.Left = 0
                lblFacilityLocation.Top = chkFacilityIncludeUsage.Top + 2

                cmbFacilityRegion.Left = LeftObjectLocation
                cmbFacilityRegion.Top = lblFacilityLocation.Top + lblFacilityLocation.Height + 2
                cmbFacilityRegion.Text = InitialRegionComboText
                cmbFacilityRegion.Width = RegionWidthCalc
                cmbFacilityRegion.Visible = True

                cmbFacilitySystem.Top = cmbFacilityRegion.Top
                cmbFacilitySystem.Left = chkFacilityIncludeUsage.Left
                cmbFacilitySystem.Width = SolarSystemWidthCalc
                cmbFacilitySystem.Text = InitialSolarSystemComboText
                cmbFacilitySystem.Visible = True

                cmbFacility.Top = cmbFacilityRegion.Top + cmbFacilityRegion.Height + 1
                cmbFacility.Left = LeftObjectLocation
                cmbFacility.Width = FacilityArrayWidthCalc
                cmbFacility.Text = InitialFacilityComboText
                cmbFacility.Visible = True

                lblFacilityDefault.Visible = True
                lblFacilityDefault.Top = chkFacilityToggle.Top
                lblFacilityDefault.Left = (cmbFacility.Left + cmbFacility.Width) - lblFacilityDefault.Width
                Call lblFacilityDefault.SendToBack()

                btnFacilitySave.Top = cmbFacility.Top + cmbFacility.Height + 3
                btnFacilitySave.Left = (cmbFacility.Left + cmbFacility.Width) - btnFacilitySave.Width + 1
                btnFacilitySave.Visible = True
                btnFacilitySave.Enabled = False

                btnFacilityFitting.Top = btnFacilitySave.Top
                btnFacilityFitting.Left = btnFacilitySave.Left - (btnFacilityFitting.Width + 2)
                btnFacilityFitting.Visible = False
                btnFacilityFitting.Enabled = False

                Call LoadManualBoxes(InitialProductionType, FormLocation)

                ' Position these but they will be shown later
                lblModules.Top = cmbFacility.Top
                lblModules.Left = 0
                lblModules.Visible = False
                Call lblModules.SendToBack()

                cmbModules.Top = lblModules.Top + lblModules.Height - 3
                cmbModules.Left = cmbFacilityRegion.Left
                cmbModules.Visible = False

                cmbFuelBlocks.Top = cmbModules.Top
                cmbFuelBlocks.Left = cmbModules.Left + cmbModules.Width + 2
                cmbFuelBlocks.Visible = False

                cmbLargeShips.Top = cmbModules.Top + cmbModules.Height + 1
                cmbLargeShips.Left = cmbModules.Left + cmbModules.Width + 2
                cmbLargeShips.Visible = False

                lblFuelBlocks.Top = cmbFuelBlocks.Top - lblFuelBlocks.Height - 1
                lblFuelBlocks.Left = cmbFuelBlocks.Left - 2
                lblFuelBlocks.Visible = False

                lblLargeShips.Left = lblModules.Left
                lblLargeShips.Top = lblFacilityFWUpgrade.Top
                lblLargeShips.Visible = False

                ' Set the initial group/category IDs
                ' also set the activity combo text to show what type of activity this facility is, even if not visible
                Call GetFacilityBPItemData(InitialProductionType, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, cmbFacilityActivities.Text)

            Case ProgramLocation.MiningTab, ProgramLocation.ReprocessingPlant, ProgramLocation.SovBelts, ProgramLocation.IceBelts
                cmbFacilityActivities.Visible = False

                lblFacilityType.Top = 5
                lblFacilityType.Left = 0
                lblFacilityType.Visible = True

                cmbFacilityType.Top = lblFacilityType.Top - 2
                cmbFacilityType.Left = lblFacilityType.Width + 2
                cmbFacilityType.Visible = True
                cmbFacilityType.Text = InitialTypeComboText

                chkFacilityToggle.Top = lblFacilityType.Top - 2
                chkFacilityToggle.Left = cmbFacilityType.Left + cmbFacilityType.Width + 5

                ' Usage will be the facility tax for refining
                chkFacilityIncludeUsage.Visible = True
                chkFacilityIncludeUsage.Left = chkFacilityToggle.Left
                chkFacilityIncludeUsage.Top = chkFacilityToggle.Top + chkFacilityToggle.Height - 1

                chkFacilityIncludeUsage.Text = "Include Reprocessing Tax"
                chkFacilityIncludeTime.Visible = False
                chkFacilityIncludeCost.Visible = False

                lblFacilityUsage.Top = chkFacilityIncludeUsage.Top - 1
                lblFacilityUsage.Left = chkFacilityIncludeUsage.Left + chkFacilityIncludeUsage.Width
                lblFacilityUsage.Width = cmbFacilitySystem.Width - chkFacilityIncludeUsage.Width
                lblFacilityUsage.Visible = False
                lblFacilityUsage.Enabled = False

                chkFacilityToggle.Visible = False

                lblFacilityLocation.Visible = True
                lblFacilityLocation.Left = 0
                lblFacilityLocation.Top = chkFacilityIncludeUsage.Top + 2

                cmbFacilityRegion.Left = LeftObjectLocation
                cmbFacilityRegion.Top = lblFacilityLocation.Top + lblFacilityLocation.Height + 2
                cmbFacilityRegion.Text = InitialRegionComboText
                cmbFacilityRegion.Width = RegionWidthCalc
                cmbFacilityRegion.Visible = True

                cmbFacilitySystem.Top = cmbFacilityRegion.Top
                cmbFacilitySystem.Left = chkFacilityIncludeUsage.Left
                cmbFacilitySystem.Width = SolarSystemWidthCalc
                cmbFacilitySystem.Text = InitialSolarSystemComboText
                cmbFacilitySystem.Visible = True

                cmbFacility.Top = cmbFacilityRegion.Top + cmbFacilityRegion.Height + 1
                cmbFacility.Left = LeftObjectLocation
                cmbFacility.Width = FacilityArrayWidthCalc
                cmbFacility.Text = InitialFacilityComboText
                cmbFacility.Visible = True

                lblFacilityDefault.Visible = True
                lblFacilityDefault.Top = chkFacilityToggle.Top
                lblFacilityDefault.Left = (cmbFacility.Left + cmbFacility.Width) - lblFacilityDefault.Width
                Call lblFacilityDefault.SendToBack()

                btnFacilitySave.Top = cmbFacility.Top + cmbFacility.Height + 2
                btnFacilitySave.Left = (cmbFacility.Left + cmbFacility.Width) - btnFacilitySave.Width + 1
                btnFacilitySave.Visible = True
                btnFacilitySave.Enabled = False

                btnFacilityFitting.Top = btnFacilitySave.Top
                btnFacilityFitting.Left = btnFacilitySave.Left - (btnFacilityFitting.Width + 2)
                btnFacilityFitting.Visible = False
                btnFacilityFitting.Enabled = False

                Call LoadManualBoxes(InitialProductionType, FormLocation)

                ' Never will be visible for refining
                lblFacilityManualCost.Visible = False
                txtFacilityManualCost.Visible = False
                txtFacilityManualTE.Visible = False
                lblFacilityManualTE.Visible = False
                cmbFacilityFWUpgrade.Visible = False
                lblFacilityFWUpgrade.Visible = False
                lblModules.Visible = False
                cmbModules.Visible = False
                cmbFuelBlocks.Visible = False
                cmbLargeShips.Visible = False
                lblFuelBlocks.Visible = False
                lblLargeShips.Visible = False

                ' Set the initial group/category IDs
                ' also set the activity combo text to show what type of activity this facility is, even if not visible
                Call GetFacilityBPItemData(InitialProductionType, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, cmbFacilityActivities.Text)

            Case Else
                ' Leave, no valid option sent
                Exit Sub
        End Select

        ' Load the defaults
        Call InitializeFacilities(FormLocation, InitialProductionType)

    End Sub

    ' Loads all the facilities for the view type sent to include defaults
    Public Sub InitializeFacilities(FacilityLocation As ProgramLocation, Optional InitialProductionType As ProductionType = ProductionType.Manufacturing,
                                    Optional RefreshSelectedOnly As Boolean = False)

        If FacilityLocation = ProgramLocation.BlueprintTab And Not RefreshSelectedOnly Then
            ' Load all the facilities for  tab - always start with manufacturing
            Call SelectedFacility.InitalizeFacility(ProductionType.Manufacturing, FacilityLocation)
            SelectedManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.ComponentManufacturing, FacilityLocation)
            SelectedComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.CapitalComponentManufacturing, FacilityLocation)
            SelectedCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.CapitalManufacturing, FacilityLocation)
            SelectedCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.SuperManufacturing, FacilityLocation)
            SelectedSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.T3CruiserManufacturing, FacilityLocation)
            SelectedT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.SubsystemManufacturing, FacilityLocation)
            SelectedSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.BoosterManufacturing, FacilityLocation)
            SelectedBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.Copying, FacilityLocation)
            SelectedCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.Invention, FacilityLocation)
            SelectedInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.Reactions, FacilityLocation)
            SelectedReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.T3Invention, FacilityLocation)
            SelectedT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.T3DestroyerManufacturing, FacilityLocation)
            SelectedT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Call SelectedFacility.InitalizeFacility(ProductionType.Reprocessing, FacilityLocation)
            ' Set the refining rates for the reprocessing facility so these are set on first load
            Call SetRefiningRates(SelectedFacility)
            SelectedReprocessingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultRefiningFacility = CType(SelectedFacility.Clone, IndustryFacility)

        ElseIf FacilityLocation = ProgramLocation.ManufacturingTab Or RefreshSelectedOnly Then

            ' Select what facility to load based on the industry type
            Call SelectedFacility.InitalizeFacility(InitialProductionType, FacilityLocation)

            Call SetSelectedFacility(InitialProductionType, FacilityLocation)

        ElseIf FacilityLocation = ProgramLocation.MiningTab Or FacilityLocation = ProgramLocation.ReprocessingPlant Or FacilityLocation = ProgramLocation.SovBelts Or FacilityLocation = ProgramLocation.IceBelts Then

            Call SelectedFacility.InitalizeFacility(ProductionType.Reprocessing, FacilityLocation)
            ' Set the refining rates for the reprocessing facility so these are set on first load
            Call SetRefiningRates(SelectedFacility)
            SelectedReprocessingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            DefaultRefiningFacility = CType(SelectedFacility.Clone, IndustryFacility)
        Else
            ' Leave, no valid option sent
            Exit Sub
        End If

        ' Reset these
        ' To save previous values for checking and loading
        PreviousProductionType = ProductionType.None
        PreviousFacilityType = FacilityTypes.None
        PreviousRegion = ""
        PreviousSystem = ""
        PreviousEquipment = ""
        PreviousActivity = ""

        ' Loaded variables
        FacilityRegionsLoaded = False
        FacilitySystemsLoaded = False
        FacilityLoaded = False

        LoadingActivities = False
        LoadingFacilityTypes = False
        LoadingRegions = False
        LoadingSystems = False
        LoadingFacilities = False
        ChangingUsageChecks = False
        UpdatingFWComboText = False

        ' Load the selected facility with set bp
        Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, True)

    End Sub

    Public Sub SetSelectedFacility(BuildType As ProductionType, FacilityLocation As ProgramLocation, Optional LoadDualFacilities As Boolean = True)

        'Now save the default and selected facility to the appropriate variable
        Select Case BuildType
            Case ProductionType.Manufacturing
                SelectedManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.CapitalComponentManufacturing
                SelectedCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                If LoadDualFacilities Then
                    ' Load component too so we can click back and forth
                    Call SelectedFacility.InitalizeFacility(ProductionType.ComponentManufacturing, FacilityLocation)
                    SelectedComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                End If
            Case ProductionType.ComponentManufacturing
                SelectedComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                If LoadDualFacilities Then
                    ' Load cap component too so we can click back and forth
                    Call SelectedFacility.InitalizeFacility(ProductionType.CapitalComponentManufacturing, FacilityLocation)
                    SelectedCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                End If
            Case ProductionType.CapitalManufacturing
                SelectedCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.SuperManufacturing
                SelectedSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.T3CruiserManufacturing
                SelectedT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                If LoadDualFacilities Then
                    ' Load T3 destroyers too so we can click back and forth
                    Call SelectedFacility.InitalizeFacility(ProductionType.T3DestroyerManufacturing, FacilityLocation)
                    SelectedT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    DefaultT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                End If
            Case ProductionType.SubsystemManufacturing
                SelectedSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.BoosterManufacturing
                SelectedBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.Copying
                SelectedCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.Invention
                SelectedInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.Reactions
                SelectedReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.T3Invention
                SelectedT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
            Case ProductionType.T3DestroyerManufacturing
                SelectedT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                If LoadDualFacilities Then
                    ' Load T3 cruisers too so we can click back and forth
                    Call SelectedFacility.InitalizeFacility(ProductionType.T3CruiserManufacturing, FacilityLocation)
                    SelectedT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                End If
            Case ProductionType.Reprocessing
                SelectedReprocessingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultRefiningFacility = CType(SelectedFacility.Clone, IndustryFacility)
        End Select

    End Sub

    ' Loads the class facility and objects
    Public Sub LoadFacility(ByVal ItemBPID As Integer, ByVal ItemGroupID As Integer, ByVal ItemCategoryID As Integer, ByVal BlueprintTech As Integer,
                            Optional ByVal LoadDefault As Boolean = False, Optional ByVal ActivityComboSelect As Boolean = False,
                            Optional RefreshBP As Boolean = True, Optional BuildT2T3Type As BuildMatType = BuildMatType.AdvMaterials)

        ' Save these for later use
        SelectedBPID = ItemBPID
        SelectedBPCategoryID = ItemCategoryID
        SelectedBPGroupID = ItemGroupID
        SelectedBPTech = BlueprintTech

        ' Process the activities combo if showing full controls
        If SelectedLocation = ProgramLocation.BlueprintTab Then
            If Not ActivityComboSelect Then ' only load if from the activities combo
                Call LoadFacilityActivities(ItemGroupID, ItemCategoryID, BlueprintTech, SelectedBPID, BuildT2T3Type)
            End If
            PreviousActivity = cmbFacilityActivities.Text
        End If

        ' Get the production type, based on activity selected
        SelectedProductionType = GetProductionType(ItemGroupID, ItemCategoryID, cmbFacilityActivities.Text)

        ' Reload the manual text boxes based on the production type
        Call LoadManualBoxes(SelectedProductionType, SelectedLocation)

        Application.DoEvents()

        ' Look up Facility - activity set to facility inside
        SelectedFacility = SelectFacility(SelectedProductionType, LoadDefault)

        ' Facility Type combo, load it if they want to change
        Call LoadFacilityTypes(SelectedProductionType, SelectedFacility.Activity)

        ' Enable the type of facility and set
        LoadingFacilityTypes = True
        cmbFacilityType.Enabled = True
        cmbFacilityType.Text = GetFacilityNamefromCode(SelectedFacility.FacilityType)
        LoadingFacilityTypes = False

        If SelectedFacility.FacilityType = FacilityTypes.None Then
            ' Just hide the boxes and exit
            Call SetFacilityBonusBoxes(False)
            SelectedFacility.FullyLoaded = True ' Even with none, it's loaded
            Call SetNoFacility()
            Exit Sub ' Leave, all loaded
        End If

        ' Region name Combo
        LoadingRegions = True
        cmbFacilityRegion.Enabled = True
        cmbFacilityRegion.Text = SelectedFacility.RegionName
        LoadingRegions = False

        ' Systems combo
        LoadingSystems = True
        cmbFacilitySystem.Enabled = True
        cmbFacilitySystem.Text = SelectedFacility.SolarSystemName
        LoadingSystems = False

        ' Facility/Array combo
        LoadingFacilities = True
        cmbFacility.Enabled = True
        Dim AutoLoad As Boolean = False
        Call LoadFacilities(False, SelectedFacility.Activity, AutoLoad, SelectedFacility.FacilityName)
        LoadingFacilities = False

        ' Usage checks
        ChangingUsageChecks = True

        chkFacilityIncludeUsage.Checked = SelectedFacility.IncludeActivityUsage
        chkFacilityIncludeCost.Checked = SelectedFacility.IncludeActivityCost
        chkFacilityIncludeTime.Checked = SelectedFacility.IncludeActivityTime

        chkFacilityConvertToOre.Checked = SelectedFacility.ConvertToOre

        ChangingUsageChecks = False

        ' Finally show the results and save the facility locally
        If Not AutoLoad Then
            LoadingFacilities = True
            With SelectedFacility
                cmbFacility.Text = .FacilityName
                Call DisplayFacilityBonus(.FacilityProductionType, ItemGroupID, ItemCategoryID, SelectedFacility.Activity,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacility.Text)
            End With
            LoadingFacilities = False
        End If

        'Call ResetComboLoadVariables(False, False, False)

        ' All data loaded
        SelectedFacility.FullyLoaded = True

        ' Facility is loaded, so save it to default and dynamic variable
        If LoadDefault Then
            Call SetSelectedFacility(SelectedProductionType, SelectedLocation, False)
        End If
        Call SetFacility(SelectedFacility, SelectedProductionType, False, False)

        ' Refresh the blueprint if it's the bp tab
        If RefreshBP Then
            Call UpdateBlueprint()
        End If

    End Sub

    ' Loads the facility manual boxes depending on the type of facility
    Private Sub LoadManualBoxes(PT As ProductionType, SentFrom As ProgramLocation)
        Const LeftLabelLocation As Integer = 1
        Dim Spacer As Integer = 0

        If SentFrom = ProgramLocation.BlueprintTab Then
            Spacer = 1
        End If

        ' Load all the manual labels and text
        lblFacilityManualME.Top = btnFacilitySave.Top + 4
        lblFacilityManualME.Left = LeftLabelLocation
        txtFacilityManualME.Top = btnFacilitySave.Top + 1
        txtFacilityManualME.Left = lblFacilityManualME.Left + lblFacilityManualME.Width

        lblFacilityManualCost.Top = lblFacilityManualME.Top + lblFacilityManualME.Height + 8
        lblFacilityManualCost.Left = LeftLabelLocation
        lblFacilityManualCost.Text = "Cost:"
        txtFacilityManualCost.Top = txtFacilityManualME.Top + txtFacilityManualME.Height + 2
        txtFacilityManualCost.Left = lblFacilityManualCost.Left + lblFacilityManualCost.Width

        ' Reset manual ME so it aligns with cost box
        txtFacilityManualME.Left = txtFacilityManualCost.Left

        lblFacilityManualTE.Top = lblFacilityManualME.Top
        lblFacilityManualTE.Left = txtFacilityManualME.Left + txtFacilityManualME.Width + 3
        txtFacilityManualTE.Top = txtFacilityManualME.Top
        txtFacilityManualTE.Left = lblFacilityManualTE.Left + lblFacilityManualTE.Width

        lblFacilityManualTax.Top = lblFacilityManualCost.Top
        lblFacilityManualTax.Left = txtFacilityManualCost.Left + txtFacilityManualCost.Width + 3
        lblFacilityManualTax.Text = "Tax:"
        txtFacilityManualTax.Top = txtFacilityManualCost.Top
        txtFacilityManualTax.Left = lblFacilityManualTax.Left + lblFacilityManualTax.Width
        txtFacilityManualTE.Left = txtFacilityManualTax.Left

        ' FW Boxes - visible is set if the system is FW system - doesn't affect reprocessing so don't load for those
        cmbFacilityFWUpgrade.Top = btnFacilitySave.Top + btnFacilitySave.Height + 1
        cmbFacilityFWUpgrade.Left = (cmbFacility.Left + cmbFacility.Width) - cmbFacilityFWUpgrade.Width
        cmbFacilityFWUpgrade.Visible = False

        lblFacilityFWUpgrade.Top = cmbFacilityFWUpgrade.Top + 2
        lblFacilityFWUpgrade.Left = cmbFacilityFWUpgrade.Left - lblFacilityFWUpgrade.Width + 2
        lblFacilityFWUpgrade.Visible = False
        Call lblFacilityFWUpgrade.SendToBack()

        chkFacilityConvertToOre.Visible = False
        btnConversiontoOreSettings.Visible = False

        If PT = ProductionType.Reprocessing Then
            Dim AdjustmentSpace As Integer
            If SelectedLocation = ProgramLocation.BlueprintTab Then
                lblFacilityManualME.Text = "Ore Eff:"
                AdjustmentSpace = 0
            Else
                lblFacilityManualME.Text = "Base Efficiency:"
                AdjustmentSpace = -2
            End If

            txtFacilityManualME.Left = lblFacilityManualME.Left + lblFacilityManualME.Width ' Reset this
            mainToolTip.SetToolTip(lblFacilityManualME, "This is the facilities base refining rate.")

            ' Move the tax box up to match the TE box positions
            lblFacilityManualTax.Top = lblFacilityManualME.Top
            lblFacilityManualTax.Left = txtFacilityManualME.Left + txtFacilityManualME.Width + 3 + AdjustmentSpace
            txtFacilityManualTax.Top = txtFacilityManualME.Top
            txtFacilityManualTax.Left = lblFacilityManualTax.Left + lblFacilityManualTax.Width + AdjustmentSpace

            ' Add reprocessing check and settings button if on BP tab and a refinery
            If SelectedLocation = ProgramLocation.BlueprintTab Then
                ' Move the TE box under the ME box for Ore/Ice rates
                lblFacilityManualTE.Top = lblFacilityManualME.Top + lblFacilityManualME.Height + 8
                lblFacilityManualTE.Left = LeftLabelLocation
                lblFacilityManualTE.Text = "Ice Eff:"
                txtFacilityManualTE.Top = txtFacilityManualME.Top + txtFacilityManualME.Height + 2
                txtFacilityManualTE.Left = txtFacilityManualME.Left

                btnConversiontoOreSettings.Top = btnFacilitySave.Top + btnFacilitySave.Height + 1
                btnConversiontoOreSettings.Left = btnFacilitySave.Left - 16
                btnConversiontoOreSettings.Visible = True

                chkFacilityConvertToOre.Top = txtFacilityManualME.Top + txtFacilityManualME.Height + 6
                chkFacilityConvertToOre.Left = btnConversiontoOreSettings.Left - chkFacilityConvertToOre.Width + 5
                chkFacilityConvertToOre.Visible = True

                ' Reset the left position of the tax box based on the check box for conversion
                txtFacilityManualTax.Left = chkFacilityConvertToOre.Left
                lblFacilityManualTax.Left = txtFacilityManualTax.Left - lblFacilityManualTax.Width
            End If

            lblFacilityManualME.Visible = True
            txtFacilityManualME.Visible = True
            lblFacilityManualTE.Visible = True
            txtFacilityManualTE.Visible = True
            lblFacilityManualCost.Visible = False
            txtFacilityManualCost.Visible = False
            lblFacilityManualTax.Visible = True
            txtFacilityManualTax.Visible = True

        Else
            lblFacilityManualME.Text = "ME:"
            mainToolTip.SetToolTip(lblFacilityManualME, "")
            lblFacilityManualTE.Text = "TE:"
            mainToolTip.SetToolTip(lblFacilityManualTE, "")

            lblFacilityManualME.Visible = True
            txtFacilityManualME.Visible = True
            lblFacilityManualCost.Visible = True
            txtFacilityManualCost.Visible = True
            lblFacilityManualTE.Visible = True
            txtFacilityManualTE.Visible = True
            lblFacilityManualTax.Visible = True
            txtFacilityManualTax.Visible = True
        End If

    End Sub

    ' Loads the facility activity combo - checks group and category ID's if it has components to set component activities
    Public Sub LoadFacilityActivities(BPGroupID As Integer, BPCategoryID As Integer, BlueprintTech As Integer, BPID As Integer,
                                      BuildMatTypeSelection As BuildMatType)

        LoadingActivities = True
        Dim HasComponents As Boolean = False
        Dim ActivityText As String = cmbFacilityActivities.Text ' Save what is selected first
        cmbFacilityActivities.BeginUpdate()

        ' If it's a reaction, only load that activity and manufacturing for fuel blocks
        If IsReaction(BPGroupID) Or BPCategoryID = ItemIDs.BoosterCategoryID Then
            cmbFacilityActivities.Items.Clear()
            cmbFacilityActivities.Items.Add(ActivityReactions)
            cmbFacilityActivities.Items.Add(ActivityManufacturing)
            ' Add reprocessing due to minerals
            cmbFacilityActivities.Items.Add(ActivityReprocessing)

            ' Start with reactions for a new facility because its a call to load not from combo
            cmbFacilityActivities.Text = ActivityReactions

            cmbFacilityActivities.EndUpdate()
            LoadingActivities = False
            Exit Sub
        Else
            Select Case BlueprintTech
                Case BPTechLevel.T1
                    ' Just manufacturing (add components later if there are any)
                    cmbFacilityActivities.Items.Clear()
                    cmbFacilityActivities.Items.Add(ActivityManufacturing)

                Case BPTechLevel.T2
                    ' Add only T2 activities to equipment
                    cmbFacilityActivities.Items.Clear()
                    cmbFacilityActivities.Items.Add(ActivityManufacturing)
                    cmbFacilityActivities.Items.Add(ActivityCopying)
                    cmbFacilityActivities.Items.Add(ActivityInvention)

                Case BPTechLevel.T3
                    ' Add only T3 activities to eqipment
                    cmbFacilityActivities.Items.Clear()
                    cmbFacilityActivities.Items.Add(ActivityManufacturing)
                    cmbFacilityActivities.Items.Add(ActivityInvention)

            End Select
        End If

        Dim SQL As String
        Dim readerBP As SQLiteDataReader

        ' See if this has buildable components
        SQL = "SELECT DISTINCT 'X' FROM ALL_BLUEPRINTS "
        SQL &= "WHERE ITEM_ID IN (SELECT MATERIAL_ID FROM ALL_BLUEPRINT_MATERIALS WHERE BLUEPRINT_ID = {0})"
        DBCommand = New SQLiteCommand(String.Format(SQL, BPID), EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader

        If readerBP.Read Then
            HasComponents = True
        Else
            HasComponents = False
        End If

        readerBP.Close()

        ' Add components as a manufacturing facility option if this bp has any
        If HasComponents Then
            Select Case BPGroupID
                Case ItemIDs.TitanGroupID, ItemIDs.DreadnoughtGroupID, ItemIDs.CarrierGroupID, ItemIDs.SupercarrierGroupID, ItemIDs.CapitalIndustrialShipGroupID,
                        ItemIDs.IndustrialCommandShipGroupID, ItemIDs.FreighterGroupID, ItemIDs.JumpFreighterGroupID, ItemIDs.FAXGroupID
                    cmbFacilityActivities.Items.Add(ActivityCapComponentManufacturing)
                    If BPGroupID = ItemIDs.JumpFreighterGroupID Then
                        ' Need to add both cap and components
                        cmbFacilityActivities.Items.Add(ActivityComponentManufacturing)
                    End If
                Case Else
                    ' Iif it's not a T2 component, then load the component manufacturing activity else it will get a reaction load below
                    If Not (BPCategoryID = ItemIDs.ComponentCategoryID Or BPGroupID = ItemIDs.AdvCapitalComponentGroupID) Then
                        ' Just regular
                        cmbFacilityActivities.Items.Add(ActivityComponentManufacturing)
                    End If
            End Select

            ' If they want to drill down on reactions, add the reactions facility option
            If BPHasProcRawMats(BPID, BuildMatTypeSelection) Then
                cmbFacilityActivities.Items.Add(ActivityReactions)
            End If

        End If

        ' If we are on the blueprint tab, then add reprocessing activity because everything can have minerals or the components do
        If SelectedLocation = ProgramLocation.BlueprintTab Then
            cmbFacilityActivities.Items.Add(ActivityReprocessing)
        End If

        cmbFacilityActivities.EndUpdate()

        ' Set default activity text if it's not in the list
        If Not cmbFacilityActivities.Items.Contains(ActivityText) Then
            cmbFacilityActivities.Text = ActivityManufacturing
        Else
            cmbFacilityActivities.Text = ActivityText
        End If

        LoadingActivities = False

    End Sub
    Private Sub cmbFacilityActivities_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityActivities.SelectedIndexChanged

        If Not LoadingActivities And Not FirstLoad Then
            SelectedProductionType = GetProductionType(SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text)

            ' If they switch the activity and it changed from the previous, then load the selected facility for this activity
            If SelectedProductionType <> PreviousProductionType Then
                PreviousProductionType = SelectedProductionType

                ' Load the facility for this activity and flag that it was loaded from this combo
                Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, False, True, False)

                ' Reset all previous to current list, since all the combos should be loaded
                PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
                PreviousEquipment = cmbFacility.Text
                PreviousRegion = cmbFacilityRegion.Text
                PreviousSystem = cmbFacilitySystem.Text
            End If

            If cmbFacilityActivities.Text = ActivityReprocessing And SelectedLocation = ProgramLocation.BlueprintTab Then
                ' Show the combos for mineral conversion

            Else

            End If

            ' Make sure the usage is updated
            Call frmMain.UpdateBPPriceLabels()

            Call cmbFacilityType.Focus()

        End If
    End Sub
    Private Sub cmbFacilityActivities_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityActivities.DropDown
        PreviousActivity = cmbFacilityActivities.Text
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
    Public Sub LoadFacilityTypes(FacilityProductionType As ProductionType, FacilityActivity As String)
        Dim Station As String = GetFacilityNamefromCode(FacilityTypes.Station)
        Dim UpwellStructure As String = GetFacilityNamefromCode(FacilityTypes.UpwellStructure)
        Dim NoneFacility As String = GetFacilityNamefromCode(FacilityTypes.None)

        LoadingFacilityTypes = True
        LoadingRegions = True
        LoadingSystems = True
        LoadingFacilities = True

        ' Clear the types each time for a fresh set of options
        cmbFacilityType.Items.Clear()

        ' Load the facility type options
        Select Case FacilityActivity
                ' Load up None for Invention/RE, Copy - they could buy the BP or T2 BPO
            Case ActivityCopying, ActivityInvention
                Select Case FacilityProductionType
                    Case ProductionType.T3Invention
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                        If NoneFacility <> "" Then cmbFacilityType.Items.Add(NoneFacility)
                    Case Else
                        If Station <> "" Then cmbFacilityType.Items.Add(Station)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                        If NoneFacility <> "" Then cmbFacilityType.Items.Add(NoneFacility)
                End Select
            Case ActivityManufacturing
                Select Case FacilityProductionType
                    Case ProductionType.SuperManufacturing, ProductionType.SubsystemManufacturing, ProductionType.T3CruiserManufacturing, ProductionType.T3DestroyerManufacturing
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                    Case Else
                        ' Add all
                        If Station <> "" Then cmbFacilityType.Items.Add(Station)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                End Select
            Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                ' Can do these anywhere
                If Station <> "" Then cmbFacilityType.Items.Add(Station)
                If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
            Case ActivityReactions
                ' Only in upwells
                If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
            Case ActivityReprocessing
                ' Can do these anywhere
                If Station <> "" Then cmbFacilityType.Items.Add(Station)
                If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
        End Select

        ' Only reset if they changed it
        If FacilityProductionType <> PreviousProductionType Or FacilityActivity <> PreviousActivity Then
            ' Reset all other dropdowns
            cmbFacilityType.Text = InitialTypeComboText
            cmbFacilityRegion.Items.Clear()
            cmbFacilityRegion.Text = InitialRegionComboText
            cmbFacilityRegion.Enabled = False
            cmbFacilitySystem.Items.Clear()
            cmbFacilitySystem.Text = InitialSolarSystemComboText
            cmbFacilitySystem.Enabled = False
            cmbFacility.Items.Clear()
            cmbFacility.Text = InitialFacilityComboText
            ' Reset the facility so it can load later
            PreviousEquipment = InitialFacilityComboText
            cmbFacility.Enabled = False
            chkFacilityIncludeUsage.Enabled = False
            chkFacilityIncludeCost.Enabled = False
            chkFacilityIncludeTime.Enabled = False

            PreviousProductionType = FacilityProductionType
            PreviousActivity = FacilityActivity

            Call SetFacilityBonusBoxes(False)

        End If

        ' Enable the facility type combo
        cmbFacilityType.Enabled = True

        ' Make sure default is not shown yet
        lblFacilityDefault.Visible = False
        btnFacilitySave.Enabled = False

        LoadingFacilityTypes = False
        LoadingRegions = False
        LoadingSystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(False, False, False)

    End Sub
    Private Sub cmbFacilityType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityType.SelectedIndexChanged
        ' Don't do anything if it's the same as the old type
        If PreviousFacilityType <> GetFacilityTypeCode(cmbFacilityType.Text) Then
            ' Might not want to set a facility for copy or invention - "None" is an option for these two activities
            If Not LoadingFacilityTypes And Not FirstLoad And GetFacilityTypeCode(cmbFacilityType.Text) <> FacilityTypes.None Then

                Call LoadFacilityRegions(SelectedBPGroupID, SelectedBPCategoryID, True, cmbFacilityActivities.Text)
                Call cmbFacilityRegion.Focus()

            ElseIf GetFacilityTypeCode(cmbFacilityType.Text) = FacilityTypes.None Then ' Invention or Copy facility - set to none

                Call SetNoFacility()

                ' Allow this to be saved as a default though
                btnFacilitySave.Enabled = True
                ' changed so not the default
                Call SetDefaultVisuals(False)
                ' Save the facility locally
                Call DisplayFacilityBonus(SelectedProductionType, SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacility.Text)
            End If

            ' Anytime this changes, set all the other ME/TE boxes to not viewed
            Call SetFacilityBonusBoxes(False)
            SelectedFacility.FullyLoaded = False
            PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
            ' Reset the previous records
            PreviousEquipment = ""
            PreviousRegion = ""
            PreviousSystem = ""

        End If

        Call SetResetRefresh()

    End Sub
    Private Sub cmbFacilityType_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityType.DropDown
        PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
    End Sub
    Private Sub cmbFacilityType_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityType.GotFocus
        Call cmbFacilityType.SelectAll()
    End Sub
    Private Sub cmbFacilityType_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityType.LostFocus
        cmbFacilityType.SelectionLength = 0
        If Trim(cmbFacilityType.Text) = "" Then
            cmbFacilityType.Text = GetFacilityNamefromCode(PreviousFacilityType)
        End If
    End Sub
    Private Sub cmbFacilityType_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityType.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the region combo
    Public Sub LoadFacilityRegions(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean, ByRef FacilityActivity As String)
        Dim SQL As String = ""

        LoadingRegions = True
        LoadingSystems = True
        LoadingFacilities = True

        cmbFacilityRegion.Items.Clear()

        ' Load regions from the facilities table - only load regions for our activity type and item group/category
        Select Case cmbFacilityType.Text

            Case StationFacility

                ' Load the Stations in system for the activity we are doing
                SQL = "SELECT DISTINCT regionName AS REGION_NAME FROM STATIONS, STATION_SERVICES, REGIONS "
                SQL &= "WHERE STATIONS.STATION_ID = STATION_SERVICES.STATION_ID "
                SQL &= "AND STATIONS.REGION_ID = REGIONS.regionID "
                SQL &= "AND regionName NOT IN ('A821-A','J7HZ-F','PR-01','UUA-F4') AND regionName NOT LIKE 'ADR%' "

                Select Case FacilityActivity
                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL &= "AND SERVICE_ID = " & CStr(StationServices.Factory)
                    Case ActivityCopying, ActivityInvention
                        SQL &= "AND SERVICE_ID = " & CStr(StationServices.Laboratory)
                    Case ActivityReprocessing
                        SQL &= "AND SERVICE_ID = " & CStr(StationServices.ReprocessingPlant)
                End Select

            Case StructureFacility
                ' For Upwell Structures, load all regions as options, but adding only one wormhole region option and don't show Jove regions
                SQL = "SELECT DISTINCT CASE WHEN (REGIONS.regionID >=11000000 and REGIONS.regionid <=11000030) THEN 'Wormhole Space' ELSE regionName END AS REGION_NAME "
                SQL &= "FROM REGIONS, SOLAR_SYSTEMS "
                SQL &= "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "
                SQL &= "AND (factionID <> 500005 OR factionID IS NULL) "
                SQL &= "AND regionName NOT IN ('A821-A','J7HZ-F','PR-01','UUA-F4') AND regionName NOT LIKE 'ADR%' "

                ' Make sure the region listed has at least one system not in the disallowed anchoring lists
                ' Upwell Structures can be anchored almost anywhere except starter systems, trade hubs, and shattered wormholes (including Thera)
                ' Check both disallowable anchor tables
                SQL &= "AND (solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_CATEGORIES WHERE CATEGORY_ID = 65) AND "
                SQL &= "solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_GROUPS WHERE GROUP_ID = 65)) "

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = ItemIDs.SupercarrierGroupID Or ItemGroupID = ItemIDs.TitanGroupID Then
                    SQL &= " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = ItemIDs.DreadnoughtGroupID Or ItemGroupID = ItemIDs.CarrierGroupID Or ItemGroupID = ItemIDs.CapitalIndustrialShipGroupID Or ItemGroupID = ItemIDs.FAXGroupID Or IsReaction(ItemGroupID) Then
                    ' For caps and reactions, only show low sec
                    SQL &= " AND security < .45"
                End If

        End Select

        SQL &= " GROUP BY REGION_NAME "

        Dim rsLoader As SQLiteDataReader

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            cmbFacilityRegion.Items.Add(rsLoader.GetString(0))
        End While

        rsLoader.Close()

        ' Enable the region combo
        cmbFacilityRegion.Enabled = True

        ' Only turn off everything if it's set to select region
        If NewFacility Then
            cmbFacilitySystem.Items.Clear()
            cmbFacilitySystem.Text = InitialSolarSystemComboText
            cmbFacilitySystem.Enabled = False
            cmbFacility.Items.Clear()
            cmbFacility.Text = InitialFacilityComboText
            ' Reset the facility so it can load later
            PreviousEquipment = InitialFacilityComboText
            cmbFacility.Enabled = False
            ' Make sure default is not checked yet
            Call SetDefaultVisuals(False)
            btnFacilitySave.Enabled = False
            btnFacilityFitting.Visible = False
            chkFacilityIncludeUsage.Enabled = False
            chkFacilityIncludeCost.Enabled = False
            chkFacilityIncludeTime.Enabled = False

            Call SetFacilityBonusBoxes(False)
        End If

        ' Only reset the region if the current selected region is not in list, also if it is in list, enable solarsystem
        If Not cmbFacilityRegion.Items.Contains(cmbFacilityRegion.Text) Then
            cmbFacilityRegion.Text = "Select Region"
        Else
            cmbFacilitySystem.Enabled = True
        End If

        LoadingRegions = False
        LoadingSystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(True, False, False)

    End Sub
    Private Sub cmbFacilityRegion_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityRegion.SelectedIndexChanged
        If Not LoadingRegions And Not FirstLoad And PreviousRegion <> cmbFacilityRegion.Text Then
            Call LoadFacilitySystems(SelectedBPGroupID, SelectedBPCategoryID, True, cmbFacilityActivities.Text)
            Call cmbFacilitySystem.Focus()
            Call SetFacilityBonusBoxes(False)
            SelectedFacility.FullyLoaded = False
            PreviousRegion = cmbFacilityRegion.Text
        End If

        Call SetResetRefresh()

    End Sub
    Private Sub cmbFacilityRegion_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityRegion.DropDown
        ' If you drop down, don't show the text window
        cmbFacilityRegion.AutoCompleteMode = AutoCompleteMode.None

        If Not FirstLoad And Not FacilityRegionsLoaded Then
            PreviousRegion = cmbFacilityRegion.Text
            ' Save the current
            PreviousRegion = cmbFacilityRegion.Text

            Call LoadFacilityRegions(SelectedBPGroupID, SelectedBPCategoryID, False, cmbFacilityActivities.Text)

        End If
    End Sub
    Private Sub cmbFacilityRegion_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityRegion.GotFocus
        Call cmbFacilityRegion.SelectAll()
    End Sub
    Private Sub cmbFacilityRegion_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityRegion.LostFocus
        cmbFacilitySystem.SelectionLength = 0
        If Trim(cmbFacilityRegion.Text) = "" Then
            cmbFacilityRegion.Text = PreviousRegion
        End If
        ' Look up entered item to make sure it's in the list, if not, then auto select
        'If Not cmbFacilityRegion.Items.Contains(cmbFacilityRegion.Text) Then
        '    If SelectedFacility.RegionName <> InitialRegionComboText Then
        '        cmbFacilityRegion.Text = SelectedFacility.RegionName
        '    Else
        '        ' Select the first thing 
        '        cmbFacilityRegion.Text = cmbFacilityRegion.Items(0).ToString
        '    End If
        'Else
        '    If Not LoadingRegions And Not FirstLoad And PreviousRegion <> cmbFacilityRegion.Text Then
        '        ' Need to load up the rest of the combos
        '        Call LoadFacilitySystems(SelectedBPGroupID, SelectedBPCategoryID, True, cmbFacilityActivities.Text)
        '    End If
        'End If
    End Sub
    Private Sub cmbFacilityRegion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFacilityRegion.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the systems combo
    Public Sub LoadFacilitySystems(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean, ByRef FacilityActivity As String)
        Dim SQL As String = ""

        LoadingSystems = True
        LoadingFacilities = True

        cmbFacilitySystem.Items.Clear()

        Select Case cmbFacilityType.Text

            Case StationFacility
                If FacilityActivity <> ActivityReprocessing Then
                    Dim ServiceIDSQL As String = ""
                    ' Load the Stations in system for the activity we are doing
                    SQL = "SELECT DISTINCT solarSystemName AS SSN, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS CI "
                    SQL &= "FROM STATION_SERVICES, SOLAR_SYSTEMS, STATIONS "
                    SQL &= "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = STATIONS.SOLAR_SYSTEM_ID "
                    Select Case FacilityActivity
                        Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing)
                            ServiceIDSQL = " AND SERVICE_ID = " & CStr(StationServices.Factory)
                        Case ActivityCopying
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying)
                            ServiceIDSQL = " AND SERVICE_ID = " & CStr(StationServices.Laboratory)
                        Case ActivityInvention
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention)
                            ServiceIDSQL = " AND SERVICE_ID = " & CStr(StationServices.Laboratory)
                        Case ActivityReactions ' Shouldn't return anything until you can do reactions in stations
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reactions)
                            ServiceIDSQL = " AND SERVICE_ID = " & CStr(StationServices.Factory)
                    End Select
                    SQL &= " WHERE STATIONS.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
                    SQL &= " AND STATIONS.STATION_ID = STATION_SERVICES.STATION_ID "
                    SQL &= " AND REGION_ID = " & CStr(GetRegionID(cmbFacilityRegion.Text)) & " "
                    SQL &= ServiceIDSQL
                Else
                    ' Refining doesn't have a cost index so just build a different query
                    SQL = "SELECT DISTINCT solarSystemName AS SSN, 0 AS CI "
                    SQL &= "FROM STATION_SERVICES, SOLAR_SYSTEMS, STATIONS "
                    SQL &= "WHERE STATIONS.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
                    SQL &= "AND STATIONS.STATION_ID = STATION_SERVICES.STATION_ID "
                    SQL &= "AND SERVICE_ID = " & CStr(StationServices.ReprocessingPlant) & " "
                    SQL &= "AND REGION_ID = " & CStr(GetRegionID(cmbFacilityRegion.Text))
                End If

            Case StructureFacility
                If FacilityActivity <> ActivityReprocessing Then
                    SQL = "SELECT DISTINCT solarSystemName AS SSN, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS CI "
                    SQL &= "FROM REGIONS, SOLAR_SYSTEMS "
                    SQL &= "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON solarSystemID = SOLAR_SYSTEM_ID "

                    Select Case FacilityActivity
                        Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        Case ActivityCopying
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        Case ActivityInvention
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        Case ActivityReactions
                            SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reactions) & " "
                    End Select
                Else
                    ' Refining doesn't have a cost index so just build a different query
                    SQL = "SELECT DISTINCT solarSystemName AS SSN, 0 AS CI "
                    SQL &= "FROM REGIONS, SOLAR_SYSTEMS "
                End If

                SQL &= "WHERE SOLAR_SYSTEMS.regionID = REGIONS.regionID "

                ' Upwell Structures can be anchored almost anywhere except starter systems, trade hubs, and shattered wormholes (including Thera)
                ' Check both disallowable anchor tables
                SQL &= "AND (solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_CATEGORIES WHERE CATEGORY_ID = 65) AND "
                SQL &= "solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_GROUPS WHERE GROUP_ID = 65)) "

                If cmbFacilityRegion.Text = "Wormhole Space" Then
                    SQL &= "AND SOLAR_SYSTEMS.regionID >=11000000 and SOLAR_SYSTEMS.regionid <=11000030 "
                Else
                    ' For an upwell, load all systems that have records linked
                    SQL &= "AND regionName = '" & FormatDBString(cmbFacilityRegion.Text) & "' "
                End If

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = ItemIDs.SupercarrierGroupID Or ItemGroupID = ItemIDs.TitanGroupID Then
                    SQL &= "AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf (ItemGroupID = ItemIDs.DreadnoughtGroupID Or ItemGroupID = ItemIDs.CarrierGroupID Or
                    ItemGroupID = ItemIDs.CapitalIndustrialShipGroupID Or ItemGroupID = ItemIDs.FAXGroupID Or
                    IsReaction(ItemGroupID)) And FacilityActivity = ActivityManufacturing Or FacilityActivity = ActivityReactions Then
                    ' For caps and reactions, only show low sec
                    SQL &= "AND security < .45 "
                End If

        End Select

        SQL &= " GROUP BY SSN, CI"

        Dim rsLoader As SQLiteDataReader
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            If FacilityActivity <> ActivityReprocessing Then
                cmbFacilitySystem.Items.Add(rsLoader.GetString(0) & " (" & FormatNumber(rsLoader.GetDouble(1), 3) & ")")
            Else
                cmbFacilitySystem.Items.Add(rsLoader.GetString(0))
            End If
        End While

        rsLoader.Close()

        ' Enable the system combo
        cmbFacilitySystem.Enabled = True

        ' Only turn off everything if it's set to select a system
        If NewFacility Then
            cmbFacility.Items.Clear()
            cmbFacility.Text = InitialFacilityComboText
            ' Reset the facility so it can load later
            PreviousEquipment = InitialFacilityComboText
            cmbFacility.Enabled = False
            ' Make sure default is not checked yet
            Call SetDefaultVisuals(False)
            btnFacilitySave.Enabled = False
            chkFacilityIncludeUsage.Enabled = False
            chkFacilityIncludeCost.Enabled = False
            chkFacilityIncludeTime.Enabled = False

            Call SetFacilityBonusBoxes(False)
        End If

        ' Only reset the system if the current selected system is not in list, also if it is in list, enable facilty
        If Not cmbFacilitySystem.Items.Contains(cmbFacilitySystem.Text) Then
            cmbFacilitySystem.Text = InitialSolarSystemComboText
        Else
            cmbFacility.Enabled = True
        End If

        LoadingSystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(False, True, False)

    End Sub
    Private Sub cmbFacilitySystem_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilitySystem.SelectedIndexChanged
        Dim OverrideFacilityName As String = ""
        Dim Autoload As Boolean = False

        cmbFacilitySystem.SelectionLength = 0

        If Not LoadingSystems And Not FirstLoad And PreviousSystem <> cmbFacilitySystem.Text Then

            Call SetFacilityBonusBoxes(False)

            ' Load the facilities
            Call LoadFacilities(False, cmbFacilityActivities.Text, Autoload, OverrideFacilityName)

            If Autoload Then
                SelectedFacility.FullyLoaded = True
                ' Facility is loaded, so save it to default and dynamic variable
                Call SetFacility(SelectedFacility, SelectedProductionType, False, False)
                Call UpdateBlueprint()
            Else
                Call SetFacilityBonusBoxes(False)
                SelectedFacility.FullyLoaded = False
            End If

            Call cmbFacility.Focus()

            PreviousSystem = cmbFacilitySystem.Text
        End If

        Call SetResetRefresh()

    End Sub
    Private Sub cmbFacilitySystem_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilitySystem.DropDown
        ' If you drop down, don't show the text window
        cmbFacilitySystem.AutoCompleteMode = AutoCompleteMode.None

        If Not FacilitySystemsLoaded And Not FirstLoad Then
            PreviousSystem = cmbFacilitySystem.Text
            Call LoadFacilitySystems(SelectedBPGroupID, SelectedBPCategoryID, False, cmbFacilityActivities.Text)
        End If
    End Sub
    Private Sub cmbFacilitySystem_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilitySystem.GotFocus
        Call cmbFacilitySystem.SelectAll()
    End Sub
    Private Sub cmbFacilitySystem_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilitySystem.LostFocus
        cmbFacilitySystem.SelectionLength = 0
        If Trim(cmbFacilitySystem.Text) = "" Then
            cmbFacilitySystem.Text = PreviousSystem
        End If
    End Sub
    Private Sub cmbFacilitySystem_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilitySystem.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the facilities/arrays combo - an itemcategory or itemgroup id of -1 means to ignore it when filling arrays
    Private Sub LoadFacilities(NewFacility As Boolean, ByRef FacilityActivity As String,
                                 ByRef AutoLoadFacility As Boolean, Optional OverrideFacilityName As String = "")
        Dim SQL As String = ""
        LoadingFacilities = True

        Dim SystemName As String
        If cmbFacilitySystem.Text.Contains("(") Then
            SystemName = cmbFacilitySystem.Text.Substring(0, InStr(cmbFacilitySystem.Text, "(") - 2)
        Else
            SystemName = cmbFacilitySystem.Text
        End If

        Dim LocalFacilityType As FacilityTypes = GetFacilityTypeCode(cmbFacilityType.Text)

        If FacilityActivity = ActivityReactions And LocalFacilityType = FacilityTypes.Station Then
            ' Need to force it to use the upwell structure since we can only do reactions there
            LocalFacilityType = FacilityTypes.UpwellStructure
            AutoLoadFacility = True
        End If

        Select Case LocalFacilityType

            Case FacilityTypes.Station
                ' Load the Stations in system for the activity we are doing
                SQL = "SELECT STATION_NAME AS FACILITY_NAME, STATIONS.STATION_ID FROM STATIONS, STATION_SERVICES "
                SQL &= "WHERE STATIONS.STATION_ID = STATION_SERVICES.STATION_ID "

                Select Case FacilityActivity
                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL &= "AND SERVICE_ID = " & CStr(StationServices.Factory)
                    Case ActivityCopying, ActivityInvention
                        SQL &= "AND SERVICE_ID = " & CStr(StationServices.Laboratory)
                    Case ActivityReprocessing
                        SQL &= "AND SERVICE_ID = " & CStr(StationServices.ReprocessingPlant)
                End Select

                SQL &= " AND REGION_ID = " & CStr(GetRegionID(cmbFacilityRegion.Text))
                SQL &= " AND SOLAR_SYSTEM_ID = " & CStr(GetSolarSystemID(SystemName))

            Case FacilityTypes.UpwellStructure
                ' Load all the upwell structures based on the production type
                SQL = "SELECT typeName as FACILITY_NAME, typeID FROM INVENTORY_TYPES, INVENTORY_GROUPS "
                SQL &= "WHERE INVENTORY_GROUPS.categoryID = 65 "
                SQL &= "AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupid "
                SQL &= "AND INVENTORY_TYPES.published = 1 "
                SQL &= "AND (typeID IN (SELECT value AS UPWELL_STRUCTURE_ID "
                SQL &= "FROM TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
                SQL &= "WHERE ATTRIBUTE_TYPES.attributeID = TYPE_ATTRIBUTES.attributeID "
                SQL &= "AND attributeName Like 'canFitShipType%' "
                SQL &= "AND TYPE_ATTRIBUTES.typeID = {0}) "
                SQL &= "OR INVENTORY_TYPES.groupID In (Select value As UPWELL_STRUCTURE_ID "
                SQL &= "FROM TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
                SQL &= "WHERE ATTRIBUTE_TYPES.attributeID = TYPE_ATTRIBUTES.attributeID "
                SQL &= "AND attributeName LIKE 'canFitShipGroup%' "
                SQL &= "AND TYPE_ATTRIBUTES.typeID = {0})) "

                ' Check for production types so that we don't show facilities that can't use services for that type (i.e. capital building)
                Select Case SelectedProductionType
                    Case ProductionType.BoosterManufacturing
                        SQL = String.Format(SQL, CInt(Services.StandupBiochemicalReactor))
                    Case ProductionType.CapitalManufacturing
                        SQL = String.Format(SQL, CInt(Services.StandupCapitalShipyard))
                    Case ProductionType.SuperManufacturing
                        SQL = String.Format(SQL, CInt(Services.StandupSupercapitalShipyard))
                    Case ProductionType.Copying
                        SQL = String.Format(SQL, CInt(Services.StandupResearchLab))
                    Case ProductionType.Invention, ProductionType.T3Invention
                        SQL = String.Format(SQL, CInt(Services.StandupInventionLab))
                    Case ProductionType.Reactions
                        SQL = String.Format(SQL, CInt(Services.StandupCompositeReactor))
                    Case ProductionType.Reprocessing
                        SQL = String.Format(SQL, CInt(Services.StandupReprocessingFaclity))
                    Case Else ' All others get manufacturing
                        SQL = String.Format(SQL, CInt(Services.StandupManufacturingPlant))
                End Select

        End Select

        ' This is helpful if we auto-load (Capital array before super capital, equipment array before rapid equipment) to choose the one more likely
        SQL &= " ORDER BY FACILITY_NAME"

        Dim rsLoader As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        cmbFacility.Enabled = True
        cmbFacility.Items.Clear()

        Dim AutoLoadName As String = ""
        Dim i As Integer = 0

        While rsLoader.Read
            If FactionCitadelList.Contains(rsLoader.GetInt32(1)) Then
                ' These are only in nullsec space (if we can look up in ESI then later maybe)
                SQL = "SELECT security, factionID FROM REGIONS, SOLAR_SYSTEMS WHERE REGIONS.regionID = SOLAR_SYSTEMS.regionID "
                SQL &= "AND factionID IS NULL AND regionName NOT LIKE '%-R%' " ' -R region names are wormhole regions
                SQL &= "AND security <= 0.0 AND SOLAR_SYSTEMS.solarSystemName = '" & SystemName & "'"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    ' no sov and it's nullsec, so add it
                    cmbFacility.Items.Add(rsLoader.GetString(0))
                End If
                rsCheck.Close()
            Else
                cmbFacility.Items.Add(rsLoader.GetString(0))
            End If

            i += 1 ' get the count
            ' Load the first one - auto choose subsystem array over advanced medium array unless already selected
            If AutoLoadName = "" Or (rsLoader.GetString(0) = "Subsystem Assembly Array" And OverrideFacilityName = "") Then
                AutoLoadName = rsLoader.GetString(0)
            End If
        End While

        rsLoader.Close()

        ' Always load the facility if there is only one and we have a reference to auto load or we are loading a specific facility
        If (i = 1 And Not IsNothing(AutoLoadFacility)) Or cmbFacility.Items.Contains(OverrideFacilityName) _
            Or cmbFacility.Items.Contains(cmbFacility.Text) Or OverrideFacilityName = "CalcBase" Then
            ' Check the override, if they want to use a rapid assembly it will override here, otherwise the other facility types should handle it (e.g. super, cap, etc)
            If OverrideFacilityName <> "" And cmbFacility.Items.Contains(OverrideFacilityName) Then
                cmbFacility.Text = OverrideFacilityName
            ElseIf cmbFacility.Items.Contains(cmbFacility.Text) Then
                ' Leave it as is
                Application.DoEvents()
            Else
                cmbFacility.Text = AutoLoadName
            End If

            AutoLoadFacility = True
            ' Display bonuses - Need to load everything since the array won't change to cause it to reload
            Call DisplayFacilityBonus(SelectedProductionType, SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text,
                                      GetFacilityTypeCode(cmbFacilityType.Text), cmbFacility.Text)
        Else
            If Not cmbFacility.Items.Contains(cmbFacility.Text) Then
                ' Only load if the item isn't in the combo
                Select Case GetFacilityTypeCode(cmbFacilityType.Text)
                    Case FacilityTypes.Station
                        cmbFacility.Text = "Select Station"
                    Case FacilityTypes.UpwellStructure
                        cmbFacility.Text = "Select Upwell Structure"
                End Select

                ' Make sure default is turned off since we still have to load the array
                btnFacilitySave.Enabled = False
                Call SetDefaultVisuals(False)
                chkFacilityIncludeUsage.Enabled = False ' Don't enable the usage either
                chkFacilityIncludeCost.Enabled = False
                chkFacilityIncludeTime.Enabled = False
            Else
                ' Since this is a different system but facility is loaded, enable save
                btnFacilitySave.Enabled = True
                Call SetDefaultVisuals(False)
                chkFacilityIncludeUsage.Enabled = True
                chkFacilityIncludeCost.Enabled = True
                chkFacilityIncludeTime.Enabled = True
            End If

            AutoLoadFacility = False

        End If

        If NewFacility Then
            ' Make sure default is not checked yet
            Call SetDefaultVisuals(False)
            btnFacilitySave.Enabled = False
            Call SetFacilityBonusBoxes(True, FacilityActivity)
        End If

        ' Users might select the facility drop down first, so reload all others
        Call ResetComboLoadVariables(False, False, True)

        LoadingFacilities = False

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub
    Private Sub cmbFacilityorArray_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacility.SelectedIndexChanged

        If Not LoadingFacilities And Not FirstLoad And PreviousEquipment <> cmbFacility.Text Then
            Call DisplayFacilityBonus(SelectedProductionType, SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacility.Text)

            PreviousEquipment = cmbFacility.Text
            Call UpdateBlueprint()
        End If

        Call SetResetRefresh()

    End Sub
    Private Sub cmbFacilityorArray_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacility.DropDown
        ' If you drop down, don't show the text window
        cmbFacility.AutoCompleteMode = AutoCompleteMode.None

        If Not FacilityLoaded And Not FirstLoad Then
            PreviousEquipment = cmbFacility.Text
            Call LoadFacilities(False, cmbFacilityActivities.Text, False, "")
        End If
    End Sub
    Private Sub cmbFacilityorArray_GotFocus(sender As Object, e As EventArgs) Handles cmbFacility.GotFocus
        Call cmbFacility.SelectAll()
    End Sub
    Private Sub cmbFacilityorArray_LostFocus(sender As Object, e As EventArgs) Handles cmbFacility.LostFocus
        cmbFacility.SelectionLength = 0
        If Trim(cmbFacility.Text) = "" Then
            cmbFacility.Text = PreviousEquipment
        End If
    End Sub
    Private Sub cmbFacilityorArray_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacility.KeyPress
        e.Handled = True
    End Sub
    Private Sub lblFacilityDefault_DoubleClick(sender As Object, e As EventArgs) Handles lblFacilityDefault.DoubleClick
        ' Load the default facility for the selected activity if it's not already the default
        If lblFacilityDefault.ForeColor = SystemColors.ButtonShadow Then
            LoadingActivities = True ' Don't trigger a combo load yet
            Dim SelectedFacility As New IndustryFacility
            Dim SelectedActivity As String = ""
            Dim BPID As Integer = 0
            Dim ItemGroupID As Integer = 0
            Dim ItemCategoryID As Integer = 0
            Dim TechLevel As Integer = 0
            Dim Activity As String = ""
            Dim OriginalProductionType As ProductionType = SelectedProductionType

            SelectedFacility = SelectFacility(OriginalProductionType, True)

            If Not IsNothing(SelectedBlueprint) Then
                With SelectedBlueprint
                    BPID = .GetBPID
                    ItemGroupID = .GetItemGroupID
                    ItemCategoryID = .GetItemCategoryID
                    TechLevel = .GetTechLevel
                End With
            ElseIf SelectedLocation = ProgramLocation.ManufacturingTab Then
                BPID = 0 ' this only matters for the activity combo
                ' For the manufacturing tab, we manually put in the IDs, so get the data first
                Call GetFacilityBPItemData(OriginalProductionType, ItemGroupID, ItemCategoryID, TechLevel, Activity)
            End If

            ' Load up the default based on the BPID - assume we selected from combo to bypass loading activities again
            Call LoadFacility(BPID, ItemGroupID, ItemCategoryID, TechLevel, True, True)

            'If ReactionTypes.Contains(SelectedBlueprint.GetItemGroup) And OriginalProductionType = ProductionType.Manufacturing Then
            '    ' Need to make sure the default of the manufacturing facility is loaded and not reactions
            '    ' Use the Fuelblock blueprint data
            '    Call LoadFacility(4314, 1136, 4, 1, True)
            'End If

            ' Set the default based on the checkbox 
            Call SetFacility(SelectedFacility, OriginalProductionType, False, False)

            LoadingActivities = False
        End If
    End Sub

    ' Displays the bonus for the facility selected in the facility or array combo
    Private Sub DisplayFacilityBonus(BuildType As ProductionType, ItemGroupID As Integer, ItemCategoryID As Integer, Activity As String,
                                    FacilityType As FacilityTypes, FacilityName As String)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        Dim FacilityID As Long
        Dim DFMaterialMultiplier As Double = 0
        Dim DFTimeMultiplier As Double = 0
        Dim DFCostMultiplier As Double = 0
        Dim DFTax As Double = 0
        Dim SavedMaterialMultiplier As Double = -1
        Dim SavedTimeMultiplier As Double = -1
        Dim SavedCostMultiplier As Double = -1
        Dim SavedTax As Double = -1

        Dim StructureModifier As Double = 0
        Dim TempDefaultFacility As New IndustryFacility

        Dim CostText As String
        Dim TaxText As String
        Dim MMText As String
        Dim TMText As String

        ' Get the facility ID first
        ' Not in there for either character or default, so use the defaults
        If FacilityType = FacilityTypes.Station Then
            ' Load the Stations in system for the activity we are doing
            SQL = "SELECT STATION_ID FROM STATIONS WHERE STATION_NAME ='" & FormatDBString(FacilityName) & "' "
        ElseIf FacilityType = FacilityTypes.UpwellStructure Then
            SQL = "SELECT UPWELL_STRUCTURE_TYPE_ID FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_NAME = '" & FormatDBString(FacilityName) & "' "
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader
        rsLoader.Read()

        If rsLoader.HasRows Then
            FacilityID = rsLoader.GetInt64(0)
        Else
            FacilityID = -1
        End If

        rsLoader.Close()

        Dim CharID As String = ""

        ' See what type of character ID
        If UserApplicationSettings.SaveFacilitiesbyChar Then
            CharID = CStr(SelectedCharacter.ID)
        Else
            CharID = CStr(CommonSavedFacilitiesID)
        End If

        ' Process system if needed
        Dim SystemName As String
        If cmbFacilitySystem.Text.Contains("(") Then
            SystemName = cmbFacilitySystem.Text.Substring(0, InStr(cmbFacilitySystem.Text, "(") - 2)
        Else
            SystemName = cmbFacilitySystem.Text
        End If

        Dim SystemID As Long = GetSolarSystemID(SystemName)

        If FacilityType <> FacilityTypes.None Then

            ' First, see if this facility is a saved facility, and use the values saved in the table
            SQL = "SELECT FACILITY_ID, FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
            SQL &= "FROM SAVED_FACILITIES "
            SQL &= "WHERE CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND FACILITY_TYPE = {2} AND FACILITY_VIEW = {3} "
            SQL &= "AND REGION_ID = " & CStr(GetRegionID(cmbFacilityRegion.Text)) & " "
            SQL &= "AND SOLAR_SYSTEM_ID = " & CStr(SystemID) & " "
            SQL &= "AND FACILITY_ID = {4}"

            ' First look up the character to see if it's saved there first (initially only do one set of facilities then allow by character via a setting)
            DBCommand = New SQLiteCommand(String.Format(SQL, CharID, CStr(BuildType), CStr(FacilityType), CStr(SelectedLocation), CStr(FacilityID)), EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            rsLoader.Read()

            If Not rsLoader.HasRows Then
                ' Need to look up the default - CharID = 0
                rsLoader.Close()
                DBCommand = New SQLiteCommand(String.Format(SQL, "0", CStr(BuildType), CStr(FacilityType), CStr(SelectedLocation), CStr(FacilityID)), EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()
            End If

            '  Load values from the saved facility
            If rsLoader.HasRows Then

                ' Save the values from the saved information - saved facility values
                If Not IsDBNull(rsLoader.GetValue(1)) Then
                    SavedTax = rsLoader.GetDouble(1)
                End If

                If Not IsDBNull(rsLoader.GetValue(2)) Then
                    SavedMaterialMultiplier = rsLoader.GetDouble(2)
                End If

                If Not IsDBNull(rsLoader.GetValue(3)) Then
                    SavedTimeMultiplier = rsLoader.GetDouble(3)
                End If

                If Not IsDBNull(rsLoader.GetValue(4)) Then
                    SavedCostMultiplier = rsLoader.GetDouble(4)
                End If
            End If

            rsLoader.Close()

            If FacilityID <> -1 Then
                ' Pull default data for ME/TE/CE
                Select Case FacilityType
                    Case FacilityTypes.Station
                        If BuildType = ProductionType.Reprocessing Then
                            ' Get Refine rate and tax for station
                            SelectedFacility.BaseTax = SelectedFacility.CalculateStationReprocessingTaxRate(SelectedCharacterID, FacilityID, SelectedFacility.BaseME)
                        Else
                            SelectedFacility.BaseTax = DefaultStationTaxRate
                            SelectedFacility.BaseCost = 1

                            ' Special case to update for Fulcrum if the BP is a sub-cap Angel or Gurista ship
                            If GetFulcrumBonusFlagforItem(FacilityID, SelectedBPID) Then
                                ' Override the ME and TE bonus for fulcrum on this bp
                                SelectedFacility.BaseME = 0.94
                                SelectedFacility.BaseTE = 0.3
                            Else
                                ' For production in stations, they are always 1
                                SelectedFacility.BaseME = 1
                                SelectedFacility.BaseTE = 1
                            End If
                        End If

                    Case FacilityTypes.UpwellStructure

                        ' Get the base facilility multipler - reprocessing is always base modified by the structure multiplier - REPLACE WITH ATTRIBUTES!
                        SQL = "SELECT CASE WHEN ACTIVITY_ID = -2 THEN " & CStr(BaseRefineRate) & " * (1 + MATERIAL_MULTIPLIER) ELSE MATERIAL_MULTIPLIER END,"
                        SQL &= "TIME_MULTIPLIER, COST_MULTIPLIER FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_NAME = '" & FormatDBString(FacilityName) & "' "

                        Select Case Activity
                            Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                                SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                            Case ActivityCopying
                                SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                            Case ActivityInvention
                                SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                            Case ActivityReactions
                                SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reactions) & " "
                            Case ActivityReprocessing
                                SQL &= "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reprocessing) & " "
                        End Select

                        Dim rsStats As SQLiteDataReader
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsStats = DBCommand.ExecuteReader
                        rsStats.Read()

                        ' Set base multipliers 
                        SelectedFacility.BaseME = rsStats.GetDouble(0)
                        SelectedFacility.BaseTE = rsStats.GetDouble(1)
                        SelectedFacility.BaseCost = rsStats.GetDouble(2)
                        SelectedFacility.BaseTax = DefaultStructureTaxRate

                        rsStats.Close()

                End Select
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
            SelectedFacility.BaseME = Defaults.FacilityDefaultMM
            SelectedFacility.BaseTE = Defaults.FacilityDefaultTM
            SelectedFacility.BaseCost = Defaults.FacilityDefaultCM
            SelectedFacility.BaseTax = Defaults.FacilityDefaultTax
        End If

        ' Now that we have everything, load the full facility into the appropriate selected facility to use later
        With SelectedFacility

            DFMaterialMultiplier = .BaseME
            DFTimeMultiplier = .BaseTE
            DFCostMultiplier = .BaseCost
            DFTax = .BaseTax

            .ActivityCostPerSecond = 0
            Select Case Activity
                Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                    .ActivityID = IndustryActivities.Manufacturing
                Case ActivityCopying
                    .ActivityID = IndustryActivities.Copying
                Case ActivityInvention
                    .ActivityID = IndustryActivities.Invention
                Case ActivityReactions
                    .ActivityID = IndustryActivities.Reactions
                Case ActivityReprocessing
                    .ActivityID = IndustryActivities.Reprocessing
            End Select

            .Activity = Activity
            .FacilityID = FacilityID
            .FacilityName = FacilityName
            .FacilityType = FacilityType
            .RegionName = cmbFacilityRegion.Text
            .SolarSystemName = cmbFacilitySystem.Text
            .SolarSystemID = SystemID
            .FacilityProductionType = BuildType

            ' First, if this is a citadel, then look up any saved modules and adjust the MM/TM/CM
            If FacilityType = FacilityTypes.UpwellStructure Then
                ' Refresh the installed rig list
                Call .UpdateProductionFittingInformation(CLng(CharID))

                DFMaterialMultiplier *= (1 - .GetFacilityBonusMulitiplier(IndustryFacility.ModifierType.MaterialModifier, .ActivityID, ItemGroupID, ItemCategoryID))
                DFTimeMultiplier *= (1 - .GetFacilityBonusMulitiplier(IndustryFacility.ModifierType.TimeModifier, .ActivityID, ItemGroupID, ItemCategoryID))
                DFCostMultiplier *= (1 - .GetFacilityBonusMulitiplier(IndustryFacility.ModifierType.CostModifier, .ActivityID, ItemGroupID, ItemCategoryID))
            End If

            ' Set the final rates on what we calculated or saved
            If SavedTax = -1 Then
                .TaxRate = DFTax
            Else
                .TaxRate = SavedTax
            End If

            If SavedMaterialMultiplier = -1 Then
                .MaterialMultiplier = DFMaterialMultiplier
            Else
                .MaterialMultiplier = SavedMaterialMultiplier
            End If

            If SavedTimeMultiplier = -1 Then
                .TimeMultiplier = DFTimeMultiplier
            Else
                .TimeMultiplier = SavedTimeMultiplier
            End If

            If SavedCostMultiplier = -1 Then
                .CostMultiplier = DFCostMultiplier
            Else
                .CostMultiplier = SavedCostMultiplier
            End If

            If FacilityType <> FacilityTypes.None Then
                ' Quick look up for the solarsystemid and region id
                SQL = "SELECT security, regionID FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(SystemID)

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                .SolarSystemID = SystemID
                .SolarSystemSecurity = rsLoader.GetDouble(0)
                .RegionID = rsLoader.GetInt64(1)
                rsLoader.Close()

                ' Now look up the cost index 
                SQL = "SELECT COST_INDEX FROM INDUSTRY_SYSTEMS_COST_INDICIES "
                SQL &= "WHERE INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
                SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = " & .ActivityID & " "

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader

                If rsLoader.Read() Then
                    .CostIndex = rsLoader.GetDouble(0)
                Else
                    .CostIndex = 0
                End If

                rsLoader.Close()
            Else
                .SolarSystemID = 0
                .RegionID = 0
                .CostIndex = 0
            End If

            ' Always display the bonus, not the multiplier
            If Activity = ActivityReprocessing Then
                ' Reset the refine rates first, then refresh
                Call SetRefiningRates(SelectedFacility)
                ' This is just the calculated value since it's really not a multiplier - get full value
                If SelectedLocation = ProgramLocation.BlueprintTab Then
                    MMText = FormatPercent(.OreFacilityRefineRate, 2)
                    TMText = FormatPercent(.IceFacilityRefineRate, 2)
                Else
                    MMText = FormatPercent(.MaterialMultiplier, 2) ' For non-BP reprocessing facilties, show the base rate only
                    TMText = FormatPercent(1 - .TimeMultiplier, 2)
                End If
            Else
                MMText = FormatPercent(1 - SelectedFacility.MaterialMultiplier, 2)
                TMText = FormatPercent(1 - SelectedFacility.TimeMultiplier, 2)
            End If

            CostText = FormatPercent(1 - SelectedFacility.CostMultiplier, 2)
            TaxText = FormatPercent(SelectedFacility.TaxRate, 1)
        End With

        If FacilityType = FacilityTypes.UpwellStructure Then
            If SelectedProductionType = ProductionType.Reprocessing Then
                txtFacilityManualME.Enabled = False ' Disable the updating of base refining efficiency
                txtFacilityManualTE.Enabled = False
            Else
                txtFacilityManualME.Enabled = True
                txtFacilityManualTE.Enabled = True
            End If
            txtFacilityManualTax.Enabled = True
            txtFacilityManualCost.Enabled = True
        Else ' Disable for non-upwell
            txtFacilityManualME.Enabled = False
            txtFacilityManualTE.Enabled = False
            txtFacilityManualTax.Enabled = False
            txtFacilityManualCost.Enabled = False
        End If

        ' Set the values
        UpdatingManualBoxes = True
        txtFacilityManualME.Text = MMText
        txtFacilityManualTE.Text = TMText
        txtFacilityManualTax.Text = TaxText
        txtFacilityManualCost.Text = CostText
        UpdatingManualBoxes = False

        ' Show the boxes
        Call SetFacilityBonusBoxes(True, Activity)

        ' Make sure the usage check is now enabled and update the box if a value exists
        If FacilityType <> FacilityTypes.None Then
            chkFacilityIncludeUsage.Enabled = True
            chkFacilityIncludeCost.Enabled = True
            chkFacilityIncludeTime.Enabled = True
            lblFacilityUsage.Text = FormatNumber(SelectedFacility.FacilityUsage, 2)
        End If

        If FacilityType = FacilityTypes.UpwellStructure Then
            ' Enable fitting
            btnFacilityFitting.Enabled = True
            btnFacilityFitting.Visible = True
        Else
            btnFacilityFitting.Enabled = False
            btnFacilityFitting.Visible = False
        End If

        ' Enable the FW settings 
        Call SetFWUpgradeControls(SelectedFacility.SolarSystemName)

        If SelectedLocation = ProgramLocation.BlueprintTab Then
            Call CostIndexUpdateText()
        End If

        ' Update the refining rates for refinery facilities
        If SelectedProductionType = ProductionType.Reprocessing Then
            If Not FirstLoad Then
                Select Case SelectedLocation
                    Case ProgramLocation.MiningTab
                        Call CType(SelectedControlForm, frmMain).RefreshMiningTabRefiningRates()
                    Case ProgramLocation.ReprocessingPlant
                        Call CType(SelectedControlForm, frmReprocessingPlant).RefreshRefiningRates()
                        'Case ProgramLocation.SovBelts
                        '    Call CType(SelectedControlForm, frmIndustryBeltFlip).LoadAllTables()
                        'Case ProgramLocation.IceBelts
                        '    Call CType(SelectedControlForm, frmIceBeltFlip).RefreshGrids()
                End Select
            End If
        End If

        ' Loaded up, let them save it
        btnFacilitySave.Visible = True
        PreviousEquipment = cmbFacility.Text
        ' Fully loaded
        SelectedFacility.FullyLoaded = True

        ' Facility is loaded, so save it to default and dynamic variable
        Call SetFacility(SelectedFacility, BuildType, False, False)

        Application.DoEvents()

    End Sub

    ' Sets the sent facility To the one we are selecting And sets the Default 
    Private Sub SetFacility(ByVal SentFacility As IndustryFacility, BuildType As ProductionType,
                           ByVal CompareIncludeCostCheck As Boolean, ByVal CompareIncludeTimeCheck As Boolean)

        ' For checking change from stations on tab
        Dim PreviousFacility As New IndustryFacility

        Select Case BuildType
            Case ProductionType.Manufacturing
                PreviousFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                SelectedManufacturingFacility = CType(SentFacility.Clone, IndustryFacility)
                If SelectedManufacturingFacility.IsEqual(DefaultManufacturingFacility) Then
                    SelectedManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.BoosterManufacturing
                PreviousFacility = CType(SelectedBoosterManufacturingFacility.Clone, IndustryFacility)
                SelectedBoosterManufacturingFacility = SentFacility
                If SelectedBoosterManufacturingFacility.IsEqual(DefaultBoosterManufacturingFacility) Then
                    SelectedBoosterManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedBoosterManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.CapitalManufacturing
                PreviousFacility = CType(SelectedCapitalManufacturingFacility.Clone, IndustryFacility)
                SelectedCapitalManufacturingFacility = SentFacility
                If SelectedCapitalManufacturingFacility.IsEqual(DefaultCapitalManufacturingFacility) Then
                    SelectedCapitalManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedCapitalManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.SuperManufacturing
                PreviousFacility = CType(SelectedSuperManufacturingFacility.Clone, IndustryFacility)
                SelectedSuperManufacturingFacility = SentFacility
                If SelectedSuperManufacturingFacility.IsEqual(DefaultSuperManufacturingFacility) Then
                    SelectedSuperManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedSuperManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.T3CruiserManufacturing
                PreviousFacility = CType(SelectedT3CruiserManufacturingFacility.Clone, IndustryFacility)
                SelectedT3CruiserManufacturingFacility = SentFacility
                If SelectedT3CruiserManufacturingFacility.IsEqual(DefaultT3CruiserManufacturingFacility) Then
                    SelectedT3CruiserManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedT3CruiserManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.T3DestroyerManufacturing
                PreviousFacility = CType(SelectedT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                SelectedT3DestroyerManufacturingFacility = SentFacility
                If SelectedT3DestroyerManufacturingFacility.IsEqual(DefaultT3DestroyerManufacturingFacility) Then
                    SelectedT3DestroyerManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedT3DestroyerManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.SubsystemManufacturing
                PreviousFacility = CType(SelectedSubsystemManufacturingFacility.Clone, IndustryFacility)
                SelectedSubsystemManufacturingFacility = SentFacility
                If SelectedSubsystemManufacturingFacility.IsEqual(DefaultSubsystemManufacturingFacility) Then
                    SelectedSubsystemManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedSubsystemManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.ComponentManufacturing
                PreviousFacility = CType(SelectedComponentManufacturingFacility.Clone, IndustryFacility)
                SelectedComponentManufacturingFacility = SentFacility
                If SelectedComponentManufacturingFacility.IsEqual(DefaultComponentManufacturingFacility) Then
                    SelectedComponentManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedComponentManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.CapitalComponentManufacturing
                PreviousFacility = CType(SelectedCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                SelectedCapitalComponentManufacturingFacility = SentFacility
                If SelectedCapitalComponentManufacturingFacility.IsEqual(DefaultCapitalComponentManufacturingFacility) Then
                    SelectedCapitalComponentManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedCapitalComponentManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.Invention
                PreviousFacility = CType(SelectedInventionFacility.Clone, IndustryFacility)
                SelectedInventionFacility = SentFacility
                If SelectedInventionFacility.IsEqual(DefaultInventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                    SelectedInventionFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedInventionFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.T3Invention
                PreviousFacility = CType(SelectedT3InventionFacility.Clone, IndustryFacility)
                SelectedT3InventionFacility = SentFacility
                If SelectedT3InventionFacility.IsEqual(DefaultT3InventionFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                    SelectedT3InventionFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedT3InventionFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.Copying
                PreviousFacility = CType(SelectedCopyFacility.Clone, IndustryFacility)
                SelectedCopyFacility = SentFacility
                If SelectedCopyFacility.IsEqual(DefaultCopyFacility, CompareIncludeCostCheck, CompareIncludeTimeCheck) Then
                    SelectedCopyFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedCopyFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.Reactions
                PreviousFacility = CType(SelectedReactionsFacility.Clone, IndustryFacility)
                SelectedReactionsFacility = SentFacility
                If SelectedReactionsFacility.IsEqual(DefaultReactionsFacility) Then
                    SelectedReactionsFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedReactionsFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.Reprocessing
                PreviousFacility = CType(SelectedReprocessingFacility.Clone, IndustryFacility)
                SelectedReprocessingFacility = SentFacility
                If SelectedReprocessingFacility.IsEqual(DefaultRefiningFacility) Then
                    SelectedReprocessingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedReprocessingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case Else
                PreviousFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                SelectedManufacturingFacility = SentFacility
                If SelectedManufacturingFacility.IsEqual(DefaultManufacturingFacility) Then
                    SelectedManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
        End Select

        ' Set the default 
        Call SetDefaultVisuals(SentFacility.IsDefault)

        ' Save the selected facility locally
        SelectedFacility = CType(SentFacility.Clone, IndustryFacility)

    End Sub

    Private Sub chkFacilityIncludeUsage_CheckedChanged(sender As Object, e As EventArgs) Handles chkFacilityIncludeUsage.CheckedChanged
        If Not ChangingUsageChecks Then

            SelectedFacility.IncludeActivityUsage = chkFacilityIncludeUsage.Checked

            ' Facility is loaded, so save it to default and dynamic variable
            Call SetFacility(SelectedFacility, SelectedProductionType, False, False)

            ' See if we update the price labels on the BP tab
            Call RefreshMainBP()

            lblFacilityUsage.Text = FormatNumber(GetSelectedFacility.FacilityUsage, 2)

        End If

        Call SetResetRefresh()

    End Sub

    Private Sub chkFacilityIncludeCost_CheckedChanged(sender As Object, e As EventArgs) Handles chkFacilityIncludeCost.CheckedChanged
        If Not ChangingUsageChecks Then
            If chkFacilityIncludeCost.Checked = True And SelectedFacility.IncludeActivityCost = False _
            Or chkFacilityIncludeCost.Checked = False And SelectedFacility.IncludeActivityCost = True Then
                ' Different than what was set, so set default visuals to false
                Call SetDefaultVisuals(False)
                SelectedFacility.IncludeActivityCost = chkFacilityIncludeCost.Checked
                ' Now set the facility
                Call SetFacility(SelectedFacility, SelectedFacility.FacilityProductionType, True, True)
            Else
                ' Same as what was set so set to true
                Call SetDefaultVisuals(True)
            End If
        End If

        Call SetResetRefresh()

    End Sub

    Private Sub chkFacilityIncludeTime_CheckedChanged(sender As Object, e As EventArgs) Handles chkFacilityIncludeTime.CheckedChanged
        If Not ChangingUsageChecks Then
            If chkFacilityIncludeTime.Checked = True And SelectedFacility.IncludeActivityTime = False _
            Or chkFacilityIncludeTime.Checked = False And SelectedFacility.IncludeActivityTime = True Then
                ' Different than what was set, so set default visuals to false
                Call SetDefaultVisuals(False)
                SelectedFacility.IncludeActivityTime = chkFacilityIncludeTime.Checked
                ' Now set the facility
                Call SetFacility(SelectedFacility, SelectedFacility.FacilityProductionType, True, True)
            Else
                ' Same as what was set so set to true
                Call SetDefaultVisuals(True)
            End If
        End If

        Call SetResetRefresh()

    End Sub

    Private Sub btnFacilityFitting_Click(sender As Object, e As EventArgs) Handles btnFacilityFitting.Click
        Dim StructureViewer As New frmUpwellStructureFitting(cmbFacility.Text, SelectedCharacterID,
                                                   SelectedProductionType, SelectedLocation, SelectedFacility.SolarSystemName)
        Call StructureViewer.ShowDialog()

        ' After showing, select the name of the citadel chosen and then dispose
        cmbFacility.Text = StructureViewer.UpwellStructureName

        Call StructureViewer.Dispose()

        ' If we updated the facility, save it first before loading as it may not have been saved yet
        If StructureViewer.SavedFacility Then
            ' Reset any manual settings
            Call SelectedFacility.ResetManualToggles()
            ' Save it here too but don't show the dialog
            Call SaveSelectedFacility(True)
        End If

        ' Reload the facility each time we return - use initialize and just load the one we changed
        If SelectedFacility.IsDefault Then
            ' If they saved fittings for the default, reset the default values
            Call InitializeFacilities(SelectedLocation, SelectedProductionType, True)
        Else
            ' If it's not the default, just load the facility so we get the changes from the fitting
            Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech)
        End If

        If StructureViewer.ResetManualEntries Then
            With SelectedFacility
                .ManualCost = False
                .ManualME = False
                .ManualTax = False
                .ManualTE = False
            End With
        End If

        Call SetResetRefresh()

    End Sub

    Private Sub btnFacilitySave_MouseHover(sender As Object, e As EventArgs) Handles btnFacilitySave.MouseHover
        If txtFacilityManualTax.Focused Then
            btnFacilityFitting.Focus()
        End If
    End Sub

    Private Sub btnFacilitySave_MouseLeave(sender As Object, e As EventArgs) Handles btnFacilitySave.MouseLeave
        'MouseOverSave = False
    End Sub

    Private Sub btnFacilitySave_Click(sender As Object, e As EventArgs) Handles btnFacilitySave.Click
        Call SaveSelectedFacility(False)
    End Sub

    Private Sub SaveSelectedFacility(SupressSaveMessage As Boolean)
        If SelectedFacility.FullyLoaded Then
            If SelectedFacility.SaveFacility(SelectedCharacterID, SelectedLocation, SupressSaveMessage) Then
                ' Just saved, so must be the default
                Call SetDefaultVisuals(True)
            Else
                Call SetDefaultVisuals(False)
                Exit Sub
            End If

            ' Need to update the local default copy of the facility first
            Select Case SelectedFacility.FacilityProductionType
                Case ProductionType.BoosterManufacturing
                    DefaultBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.CapitalComponentManufacturing
                    DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.CapitalManufacturing
                    DefaultCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.ComponentManufacturing
                    DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.Copying
                    DefaultCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.Invention
                    DefaultInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.Manufacturing
                    DefaultManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.Reactions
                    DefaultReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.SubsystemManufacturing
                    DefaultSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.SuperManufacturing
                    DefaultSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.T3CruiserManufacturing
                    DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.T3DestroyerManufacturing
                    DefaultT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.T3Invention
                    DefaultT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.Reprocessing
                    DefaultRefiningFacility = CType(SelectedFacility.Clone, IndustryFacility)
            End Select

            ' Now set the facility
            Call SetFacility(SelectedFacility, SelectedFacility.FacilityProductionType, True, True)

        End If

        ' Update the blueprint if we can after a save
        Call UpdateBlueprint()

    End Sub

    ' Load the facility for the check - either components or cap components OR T3 destroyers or T3 cruisers (only need to do this with limited controls)
    Private Sub chkFacilityToggle_CheckedChanged(sender As Object, e As EventArgs) Handles chkFacilityToggle.CheckedChanged
        If Not FirstLoad Then
            Select Case SelectedProductionType
                Case ProductionType.CapitalComponentManufacturing, ProductionType.ComponentManufacturing
                    If chkFacilityToggle.Checked Then
                        SelectedProductionType = ProductionType.CapitalComponentManufacturing
                        SelectedBPCategoryID = ItemIDs.CapitalComponentGroupID
                        SelectedBPGroupID = ItemIDs.None
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityCapComponentManufacturing
                        Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech)
                    Else
                        SelectedProductionType = ProductionType.ComponentManufacturing
                        SelectedBPCategoryID = ItemIDs.ComponentCategoryID
                        SelectedBPGroupID = ItemIDs.None
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityComponentManufacturing
                        Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech)
                    End If
                Case ProductionType.T3DestroyerManufacturing, ProductionType.T3CruiserManufacturing
                    If chkFacilityToggle.Checked Then
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.TacticalDestroyerGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                        Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech)
                    Else
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.StrategicCruiserGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                        Call LoadFacility(SelectedBPID, SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech)
                    End If
            End Select
        End If
    End Sub

#Region "Support Functions"

    ' Returns references to the GroupID, CategoryID, TechLevel, and Activity Combo Text when sent the production type
    Private Sub GetFacilityBPItemData(ByVal SentProductionType As ProductionType, ByRef GroupID As Integer,
                                      ByRef CategoryID As Integer, ByRef TechLevel As Integer, ByRef ActivityComboText As String)
        Select Case SentProductionType
            Case ProductionType.Manufacturing
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.FrigateGroupID
                TechLevel = BPTechLevel.T2
                ActivityComboText = ActivityManufacturing
            Case ProductionType.CapitalComponentManufacturing
                CategoryID = ItemIDs.CapitalComponentGroupID
                GroupID = ItemIDs.None
                TechLevel = BPTechLevel.T1
                ActivityComboText = ActivityCapComponentManufacturing
            Case ProductionType.ComponentManufacturing
                CategoryID = ItemIDs.ComponentCategoryID
                GroupID = ItemIDs.None
                TechLevel = BPTechLevel.T1
                ActivityComboText = ActivityComponentManufacturing
            Case ProductionType.CapitalManufacturing
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.CarrierGroupID
                TechLevel = BPTechLevel.T1
                ActivityComboText = ActivityManufacturing
            Case ProductionType.SuperManufacturing
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.SupercarrierGroupID
                TechLevel = BPTechLevel.T1
                ActivityComboText = ActivityManufacturing
            Case ProductionType.T3CruiserManufacturing
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.StrategicCruiserGroupID
                TechLevel = BPTechLevel.T3
                ActivityComboText = ActivityManufacturing
            Case ProductionType.SubsystemManufacturing
                CategoryID = ItemIDs.SubsystemCategoryID
                GroupID = ItemIDs.None
                TechLevel = BPTechLevel.T3
                ActivityComboText = ActivityManufacturing
            Case ProductionType.BoosterManufacturing
                CategoryID = ItemIDs.BoosterCategoryID
                GroupID = ItemIDs.BoosterGroupID
                TechLevel = BPTechLevel.T1
                ActivityComboText = ActivityManufacturing
            Case ProductionType.Copying
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.FrigateGroupID
                TechLevel = BPTechLevel.T2
                ActivityComboText = ActivityCopying
            Case ProductionType.Invention
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.FrigateGroupID
                TechLevel = BPTechLevel.T2
                ActivityComboText = ActivityInvention
            Case ProductionType.Reactions
                CategoryID = ItemIDs.None
                GroupID = ItemIDs.ReactionPolymersGroupID
                TechLevel = BPTechLevel.T1
                ActivityComboText = ActivityReactions
            Case ProductionType.T3Invention
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.TacticalDestroyerGroupID
                TechLevel = BPTechLevel.T3
                ActivityComboText = ActivityInvention
            Case ProductionType.T3DestroyerManufacturing
                CategoryID = ItemIDs.ShipCategoryID
                GroupID = ItemIDs.TacticalDestroyerGroupID
                TechLevel = BPTechLevel.T3
                ActivityComboText = ActivityManufacturing
            Case ProductionType.Reprocessing
                CategoryID = ItemIDs.AsteroidsCategoryID
                GroupID = 0
                TechLevel = 0
                ActivityComboText = ActivityReprocessing
        End Select

    End Sub

    ' Selects the facility and returns it and sets the activity on the facility found
    Private Function SelectFacility(ByVal BuildType As ProductionType, ByVal IsDefault As Boolean) As IndustryFacility

        Dim FacilityActivity As String = ""
        Dim ReturnFacility As New IndustryFacility

        If IsDefault Then
            Select Case BuildType
                Case ProductionType.Manufacturing
                    ReturnFacility = CType(DefaultManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.SuperManufacturing
                    ReturnFacility = CType(DefaultSuperManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.CapitalManufacturing
                    ReturnFacility = CType(DefaultCapitalManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.BoosterManufacturing
                    ReturnFacility = CType(DefaultBoosterManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.T3CruiserManufacturing
                    ReturnFacility = CType(DefaultT3CruiserManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.T3DestroyerManufacturing
                    ReturnFacility = CType(DefaultT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.SubsystemManufacturing
                    ReturnFacility = CType(DefaultSubsystemManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.Invention
                    ReturnFacility = CType(DefaultInventionFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityInvention
                Case ProductionType.T3Invention
                    ReturnFacility = CType(DefaultT3InventionFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityInvention
                Case ProductionType.Copying
                    FacilityActivity = ActivityCopying
                    ReturnFacility = CType(DefaultCopyFacility.Clone, IndustryFacility)
                Case ProductionType.Reactions
                    FacilityActivity = ActivityReactions
                    ReturnFacility = CType(DefaultReactionsFacility.Clone, IndustryFacility)
                Case ProductionType.ComponentManufacturing
                    FacilityActivity = ActivityComponentManufacturing
                    ReturnFacility = CType(DefaultComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.CapitalComponentManufacturing
                    FacilityActivity = ActivityCapComponentManufacturing
                    ReturnFacility = CType(DefaultCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.Reprocessing
                    FacilityActivity = ActivityReprocessing
                    ReturnFacility = CType(DefaultRefiningFacility.Clone, IndustryFacility)
            End Select
        Else
            Select Case BuildType
                Case ProductionType.Manufacturing
                    ReturnFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.SuperManufacturing
                    ReturnFacility = CType(SelectedSuperManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.CapitalManufacturing
                    ReturnFacility = CType(SelectedCapitalManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.BoosterManufacturing
                    ReturnFacility = CType(SelectedBoosterManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.T3CruiserManufacturing
                    ReturnFacility = CType(SelectedT3CruiserManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.T3DestroyerManufacturing
                    ReturnFacility = CType(SelectedT3DestroyerManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.SubsystemManufacturing
                    ReturnFacility = CType(SelectedSubsystemManufacturingFacility.Clone, IndustryFacility)
                    FacilityActivity = ActivityManufacturing
                Case ProductionType.Invention
                    FacilityActivity = ActivityInvention
                    ReturnFacility = CType(SelectedInventionFacility.Clone, IndustryFacility)
                Case ProductionType.T3Invention
                    FacilityActivity = ActivityInvention
                    ReturnFacility = CType(SelectedT3InventionFacility.Clone, IndustryFacility)
                Case ProductionType.Copying
                    FacilityActivity = ActivityCopying
                    ReturnFacility = CType(SelectedCopyFacility.Clone, IndustryFacility)
                Case ProductionType.Reactions
                    FacilityActivity = ActivityReactions
                    ReturnFacility = CType(SelectedReactionsFacility.Clone, IndustryFacility)
                Case ProductionType.ComponentManufacturing
                    FacilityActivity = ActivityComponentManufacturing
                    ReturnFacility = CType(SelectedComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.CapitalComponentManufacturing
                    FacilityActivity = ActivityCapComponentManufacturing
                    ReturnFacility = CType(SelectedCapitalComponentManufacturingFacility.Clone, IndustryFacility)
                Case ProductionType.Reprocessing
                    FacilityActivity = ActivityReprocessing
                    ReturnFacility = CType(SelectedReprocessingFacility.Clone, IndustryFacility)
            End Select
        End If

        ' Set the activity text here
        ReturnFacility.Activity = FacilityActivity

        Return ReturnFacility

    End Function

    ' Sets all the combos to unenabled and base text to show no facility for stuff like Invention, Copy and RE where they might buy the item
    Private Sub SetNoFacility()
        cmbFacilityRegion.Items.Clear()
        cmbFacilityRegion.Text = "Select Region"
        cmbFacilityRegion.Enabled = False
        cmbFacilitySystem.Items.Clear()
        cmbFacilitySystem.Text = InitialSolarSystemComboText
        cmbFacilitySystem.Enabled = False
        cmbFacility.Items.Clear()
        cmbFacility.Text = InitialFacilityComboText
        chkFacilityIncludeUsage.Enabled = False

        If Not IsNothing(chkFacilityIncludeCost) Then
            chkFacilityIncludeCost.Enabled = False
        End If
        If Not IsNothing(chkFacilityIncludeTime) Then
            chkFacilityIncludeTime.Enabled = False
        End If
        cmbFacility.Enabled = False
        If Not IsNothing(chkFacilityToggle) Then
            chkFacilityToggle.Enabled = False
        End If
    End Sub

    ' Sets the visual data for default facility
    Private Sub SetDefaultVisuals(isDefault As Boolean)
        If isDefault = True Then
            lblFacilityDefault.ForeColor = FacilityLabelDefaultColor
            lblFacilityDefault.Visible = True
            Call ResetToolTipforDefaultFacilityLabel(False)
            btnFacilitySave.Enabled = False ' don't enable since it's already the default, it's pointless to save it
        Else
            lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
            lblFacilityDefault.Visible = True
            Call ResetToolTipforDefaultFacilityLabel(True)
            If SelectedFacility.FullyLoaded Then
                btnFacilitySave.Enabled = True
            End If
        End If
    End Sub

    ' Translates the string facility type into the enum code
    Private Function GetFacilityTypeCode(FacilityType As String) As FacilityTypes
        Dim rsLookup As SQLiteDataReader
        Dim ReturnType As FacilityTypes = FacilityTypes.None

        DBCommand = New SQLiteCommand("SELECT FACILITY_TYPE_ID FROM FACILITY_TYPES WHERE FACILITY_TYPE_NAME = '" & FacilityType & "'", EVEDB.DBREf)
        rsLookup = DBCommand.ExecuteReader
        If rsLookup.Read() Then
            ReturnType = CType(rsLookup.GetInt32(0), FacilityTypes)
        End If

        rsLookup.Close()

        Return ReturnType

    End Function

    ' Translates facility code into name
    Private Function GetFacilityNamefromCode(FacilityType As FacilityTypes) As String
        Dim rsLookup As SQLiteDataReader
        Dim ReturnName As String = ""

        DBCommand = New SQLiteCommand("SELECT FACILITY_TYPE_NAME FROM FACILITY_TYPES WHERE FACILITY_TYPE_ID = " & CInt(FacilityType), EVEDB.DBREf)
        rsLookup = DBCommand.ExecuteReader
        If rsLookup.Read() Then
            ReturnName = rsLookup.GetString(0)
        End If

        rsLookup.Close()

        Return ReturnName

    End Function

    ' Hides all the facility bonus boxes and such
    Private Sub SetFacilityBonusBoxes(ByVal Value As Boolean, Optional Activity As String = "")

        If Activity = ActivityReprocessing Then
            ' Only two boxes shown for refining
            txtFacilityManualME.Visible = Value
            If SelectedLocation = ProgramLocation.BlueprintTab Then
                txtFacilityManualTE.Visible = Value
            Else
                txtFacilityManualTE.Visible = False
            End If
            txtFacilityManualTax.Visible = Value
            txtFacilityManualCost.Visible = False

            lblFacilityManualME.Visible = Value
            lblFacilityManualTE.Visible = Value
            lblFacilityManualTax.Visible = Value
            lblFacilityManualCost.Visible = False
        Else
            txtFacilityManualME.Visible = Value
            txtFacilityManualTE.Visible = Value
            txtFacilityManualTax.Visible = Value
            txtFacilityManualCost.Visible = Value

            lblFacilityManualME.Visible = Value
            lblFacilityManualTE.Visible = Value
            lblFacilityManualTax.Visible = Value
            lblFacilityManualCost.Visible = Value
        End If

        ' only set these false when this is called, it will load if needed elsewhere
        lblFacilityFWUpgrade.Visible = False
        cmbFacilityFWUpgrade.Visible = False

        ' Clear the usage until these are set
        If Not IsNothing(lblFacilityUsage) Then
            lblFacilityUsage.Text = ""
        End If

    End Sub

    ' Resets all combo boxes toggles that might need to be updated 
    Private Sub ResetComboLoadVariables(RegionsValue As Boolean, SystemsValue As Boolean, FacilitiesValue As Boolean)

        FacilityRegionsLoaded = RegionsValue
        FacilitySystemsLoaded = SystemsValue
        FacilityLoaded = FacilitiesValue

    End Sub

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

    ' Returns the type of production done for the activity and bp data sent
    Public Function GetProductionType(BPGroupID As Integer, BPCategoryID As Integer, SelectedActivity As String) As ProductionType
        Dim SelectedIndyType As ProductionType

        Dim FacilityType As FacilityTypes
        Dim BaseActivity As String

        ' Select the facility type from the combo or default
        If cmbFacilityType.Text = InitialTypeComboText Then
            FacilityType = FacilityTypes.Station
        Else
            FacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
        End If

        ' Select the activity type from combo or default
        If SelectedActivity = InitialActivityComboText Then
            ' Use the manufacturing activity for these
            BaseActivity = ActivityManufacturing
        Else
            BaseActivity = SelectedActivity
        End If

        Select Case BaseActivity
                ' TODO look into making these a lookup with the facility type if there are category or groupid's in the tables for them. 
            Case ActivityManufacturing
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
                    Case ItemIDs.ProtectiveComponentGroupID
                        SelectedIndyType = ProductionType.ComponentManufacturing
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
                        End If
                End Select
            Case ActivityComponentManufacturing
                SelectedIndyType = ProductionType.ComponentManufacturing
            Case ActivityCapComponentManufacturing
                SelectedIndyType = ProductionType.CapitalComponentManufacturing
            Case ActivityCopying
                SelectedIndyType = ProductionType.Copying
            Case ActivityInvention
                If BPCategoryID = ItemIDs.SubsystemCategoryID Or BPGroupID = ItemIDs.StrategicCruiserGroupID Or BPGroupID = ItemIDs.TacticalDestroyerGroupID Then
                    ' Need to invent this at a station
                    SelectedIndyType = ProductionType.T3Invention
                Else
                    SelectedIndyType = ProductionType.Invention
                End If
            Case ActivityReactions
                SelectedIndyType = ProductionType.Reactions
            Case ActivityReprocessing
                SelectedIndyType = ProductionType.Reprocessing
        End Select

        Return SelectedIndyType

    End Function

    ' Loads up all the usage for all facilities on this bp into a form
    Private Sub lblFacilityUsage_DoubleClick(sender As System.Object, e As System.EventArgs) Handles lblFacilityUsage.DoubleClick
        Dim f1 As New frmUsageViewer
        Dim RawCostSplit As UsageSplit

        ' Fill up the array to display only if on the bp tab
        If Not IsNothing(SelectedBlueprint) And SelectedLocation = ProgramLocation.BlueprintTab Then

            ' Manufacturing Facility usage
            RawCostSplit.UsageName = "Manufacturing Facility Usage"
            If Not ReactionTypes.Contains(SelectedBlueprint.GetItemData.GroupName) Then
                RawCostSplit.UsageValue = GetSelectedManufacturingFacility(SelectedBlueprint.GetItemGroupID, SelectedBlueprint.GetItemCategoryID).FacilityUsage
            Else
                ' Add fuel block usage
                RawCostSplit.UsageValue = SelectedManufacturingFacility.FacilityUsage
            End If
            f1.UsageSplits.Add(RawCostSplit)

            If SelectedBlueprint.HasComponents And SelectedBlueprint.GetItemCategoryID <> ItemIDs.ComponentCategoryID And SelectedBlueprint.GetItemGroupID <> ItemIDs.AdvCapitalComponentGroupID And
            Not ReactionTypes.Contains(SelectedBlueprint.GetItemData.GroupName) Then
                ' Component Facility Usage
                RawCostSplit.UsageName = "Component Facility Usage"
                RawCostSplit.UsageValue = SelectedComponentManufacturingFacility.FacilityUsage
                f1.UsageSplits.Add(RawCostSplit)

                ' Capital Component Facility Usage
                Select Case SelectedBlueprint.GetItemGroupID
                    Case ItemIDs.TitanGroupID, ItemIDs.SupercarrierGroupID, ItemIDs.CarrierGroupID, ItemIDs.DreadnoughtGroupID,
                    ItemIDs.JumpFreighterGroupID, ItemIDs.FreighterGroupID, ItemIDs.IndustrialCommandShipGroupID, ItemIDs.CapitalIndustrialShipGroupID, ItemIDs.FAXGroupID
                        ' Only add cap component usage for ships that use them
                        RawCostSplit.UsageName = "Capital Component Facility Usage"
                        RawCostSplit.UsageValue = SelectedCapitalComponentManufacturingFacility.FacilityUsage
                        f1.UsageSplits.Add(RawCostSplit)
                End Select
            ElseIf (SelectedBlueprint.GetItemCategoryID = ItemIDs.ComponentCategoryID Or SelectedBlueprint.GetItemGroupID = ItemIDs.AdvCapitalComponentGroupID) Or
            ReactionTypes.Contains(SelectedBlueprint.GetItemData.GroupName) Then
                ' Load reactions usage
                RawCostSplit.UsageName = "Reaction Facility Usage"
                RawCostSplit.UsageValue = SelectedReactionsFacility.FacilityUsage
                f1.UsageSplits.Add(RawCostSplit)
            End If

            If SelectedBlueprint.GetTechLevel <> BPTechLevel.T1 Then
                ' Invention Facility
                RawCostSplit.UsageName = "Invention Usage"
                RawCostSplit.UsageValue = SelectedInventionFacility.FacilityUsage
                f1.UsageSplits.Add(RawCostSplit)
            End If

            If SelectedBlueprint.GetTechLevel = BPTechLevel.T2 Then
                ' Copy Facility
                RawCostSplit.UsageName = "Copy Usage"
                RawCostSplit.UsageValue = SelectedCopyFacility.FacilityUsage
                f1.UsageSplits.Add(RawCostSplit)
            End If

            'Reprocessing usage
            If SelectedReprocessingFacility.ConvertToOre Then
                RawCostSplit.UsageName = "Reprocessing Usage"
                RawCostSplit.UsageValue = SelectedReprocessingFacility.FacilityUsage
                f1.UsageSplits.Add(RawCostSplit)
            End If

            f1.Show()
        End If
    End Sub

    ' Updates the cost index text box on the bp tab
    Private Sub CostIndexUpdateText()

        If SelectedLocation = ProgramLocation.BlueprintTab And Not FirstLoad Then
            Dim System As String = ""
            Dim Start As Integer = 0
            Dim IndexValue As String = "0"

            Try
                System = cmbFacilitySystem.Text
                Start = InStr(System, "(")
                If System.Contains("(") Then
                    IndexValue = System.Substring(Start, InStr(System, ")") - (Start + 1))

                    If Not IsNumeric(IndexValue) Then
                        ' Write the values to error log for now
                        Call WriteMsgToLog("CostIndexUpdateText Error: System = " & System & ", Start = " & CStr(Start) & " Index Value = " & IndexValue)
                    End If
                End If
            Catch ex As Exception
                Call WriteMsgToLog("CostIndexUpdateText Error: System = " & System & ", Start = " & CStr(Start) & " Index Value = " & IndexValue & " Facility System Text:" & cmbFacilitySystem.Text & " Error Message:" & ex.Message)
            End Try

            frmMain.txtBPUpdateCostIndex.Text = FormatPercent(IndexValue, 2)

        End If

    End Sub

    ' Updates the blueprint on the bp tab if it's that facility
    Private Sub UpdateBlueprint()
        ' Load the bp on bp tab
        If Not IsNothing(SelectedBlueprint) And SelectedLocation = ProgramLocation.BlueprintTab Then
            With SelectedBlueprint
                Call frmMain.UpdateBPGrids(.GetTypeID, .GetTechLevel, False, .GetItemGroupID, .GetItemCategoryID, SentFromLocation.BlueprintTab)
            End With
        End If
    End Sub

    Private Sub SetResetRefresh()
        If SelectedLocation = ProgramLocation.ManufacturingTab And Not FirstLoad Then
            Call frmMain.ResetRefresh()
        End If
    End Sub

#End Region

#Region "Faction Warfare Functions"

    Private Function GetFWUpgradeLevel(SolarSystemName As String) As Integer

        If Not FirstLoad And cmbFacilitySystem.Text <> InitialSolarSystemComboText Then
            Dim FWUpgradeLevel As Integer

            Dim rsFW As SQLiteDataReader
            Dim SSID As Long

            ' Format system name
            If SolarSystemName.Contains("(") Then
                SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
            End If

            Dim SQL As String = "SELECT factionWarzone, solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsFW = DBCommand.ExecuteReader

            Dim Warzone As Boolean
            If rsFW.Read Then
                Warzone = CBool(rsFW.GetInt32(0))
                SSID = rsFW.GetInt64(1)
            Else
                Warzone = False
            End If

            rsFW.Close()

            If Warzone Then
                If Not IsNothing(cmbFacilityFWUpgrade) Then
                    Select Case cmbFacilityFWUpgrade.Text
                        Case "Level 1"
                            FWUpgradeLevel = 1
                        Case "Level 2"
                            FWUpgradeLevel = 2
                        Case "Level 3"
                            FWUpgradeLevel = 3
                        Case "Level 4"
                            FWUpgradeLevel = 4
                        Case "Level 5"
                            FWUpgradeLevel = 5
                        Case Else
                            FWUpgradeLevel = 0
                    End Select
                Else
                    FWUpgradeLevel = 0
                End If
            Else
                FWUpgradeLevel = -1
            End If

            Return FWUpgradeLevel
        End If

        Return -1

    End Function

    ' Enables the controls for FW settings on the bp tab
    Private Sub SetFWUpgradeControls(ByVal SolarSystemName As String)
        ' Load the faction warfare upgrade
        Dim rsFW As SQLiteDataReader
        Dim SSID As Long
        Dim Warzone As Boolean = False

        ' Format system name
        If SolarSystemName.Contains("(") Then
            SolarSystemName = SolarSystemName.Substring(0, InStr(SolarSystemName, "(") - 2)
        End If

        Dim SQL As String = "SELECT factionWarzone, solarSystemID FROM SOLAR_SYSTEMS WHERE solarSystemName = '" & FormatDBString(SolarSystemName) & "'"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsFW = DBCommand.ExecuteReader

        If rsFW.Read Then
            Warzone = CBool(rsFW.GetInt32(0))
            SSID = rsFW.GetInt64(1)
        Else
            Warzone = False
        End If

        rsFW.Close()
        UpdatingFWComboText = True

        If Warzone And SelectedFacility.FacilityProductionType <> ProductionType.Reprocessing Then
            lblFacilityFWUpgrade.Enabled = True
            lblFacilityFWUpgrade.Visible = True
            cmbFacilityFWUpgrade.Enabled = True
            cmbFacilityFWUpgrade.Visible = True
            ' look up level
            Dim rsFWLevel As SQLiteDataReader
            SQL = "SELECT UPGRADE_LEVEL FROM FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = " & CStr(SSID)
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsFWLevel = DBCommand.ExecuteReader
            rsFWLevel.Read()

            If rsFWLevel.HasRows Then
                If rsFWLevel.GetInt32(0) = 0 Then
                    cmbFacilityFWUpgrade.Text = None
                Else
                    cmbFacilityFWUpgrade.Text = "Level " & CStr(rsFWLevel.GetInt32(0))
                End If
            Else
                cmbFacilityFWUpgrade.Text = None
            End If

            rsFWLevel.Close()
        Else
            lblFacilityFWUpgrade.Enabled = False
            lblFacilityFWUpgrade.Visible = False
            cmbFacilityFWUpgrade.Enabled = False
            cmbFacilityFWUpgrade.Visible = False
            cmbFacilityFWUpgrade.Text = None
            SelectedFacility.FWUpgradeLevel = -1
        End If

        UpdatingFWComboText = False

    End Sub

    Private Sub cmbFWUpgrade_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityFWUpgrade.SelectedIndexChanged
        If Not FirstLoad And Not UpdatingFWComboText Then
            ' Set the selected level
            SelectedFacility.FWUpgradeLevel = GetFWUpgradeLevel(cmbFacilitySystem.Text)
            ' Facility is loaded, so save it to default and dynamic variable
            Call SetFacility(SelectedFacility, SelectedProductionType, False, False)
            ' Let them save the change
            Call SetDefaultVisuals(False)
            ' If this changed, we need to update the usage
            Call RefreshMainBP(True)
        End If

        Call SetResetRefresh()

    End Sub

    Private Sub cmbFWUpgrade_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityFWUpgrade.KeyPress
        e.Handled = True
    End Sub

#End Region

#Region "Public Functions"

    ' Resets the char id of the facility
    Public Sub ResetSelectedCharacterID(NewCharacterID As Long)
        SelectedCharacterID = NewCharacterID
    End Sub

    ' Loads the facility sent into the type of the facility
    Public Sub UpdateFacility(UpdatedFacility As IndustryFacility)

        Select Case UpdatedFacility.FacilityProductionType
            Case ProductionType.BoosterManufacturing
                SelectedBoosterManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.CapitalComponentManufacturing
                SelectedCapitalComponentManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.CapitalManufacturing
                SelectedCapitalManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.ComponentManufacturing
                SelectedComponentManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.Copying
                SelectedCopyFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.Invention
                SelectedInventionFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.Manufacturing
                SelectedManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.Reactions
                SelectedReactionsFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.SubsystemManufacturing
                SelectedSubsystemManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.SuperManufacturing
                SelectedSuperManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.T3CruiserManufacturing
                SelectedT3CruiserManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.T3DestroyerManufacturing
                SelectedT3DestroyerManufacturingFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.T3Invention
                SelectedT3InventionFacility = CType(UpdatedFacility.Clone, IndustryFacility)
        End Select

    End Sub

    ' Returns the facilty for the production type sent
    Public Function GetFacility(BuildType As ProductionType) As IndustryFacility

        ' Select based on input type. If not fully loaded, then load the default and also load the default facility in the facility controls
        Select Case BuildType
            Case ProductionType.BoosterManufacturing
                If SelectedBoosterManufacturingFacility.FullyLoaded Then
                    Return SelectedBoosterManufacturingFacility
                Else
                    Return DefaultBoosterManufacturingFacility
                End If
            Case ProductionType.CapitalComponentManufacturing
                If SelectedCapitalComponentManufacturingFacility.FullyLoaded Then
                    Return SelectedCapitalComponentManufacturingFacility
                Else
                    Return DefaultCapitalComponentManufacturingFacility
                End If
            Case ProductionType.CapitalManufacturing
                If SelectedCapitalManufacturingFacility.FullyLoaded Then
                    Return SelectedCapitalManufacturingFacility
                Else
                    Return DefaultCapitalManufacturingFacility
                End If
            Case ProductionType.ComponentManufacturing
                If SelectedComponentManufacturingFacility.FullyLoaded Then
                    Return SelectedComponentManufacturingFacility
                Else
                    Return DefaultComponentManufacturingFacility
                End If
            Case ProductionType.Copying
                If SelectedCopyFacility.FullyLoaded Then
                    Return SelectedCopyFacility
                Else
                    Return DefaultCopyFacility
                End If
            Case ProductionType.Invention
                If SelectedInventionFacility.FullyLoaded Then
                    Return SelectedInventionFacility
                Else
                    Return DefaultInventionFacility
                End If
            Case ProductionType.Manufacturing
                If SelectedManufacturingFacility.FullyLoaded Then
                    Return SelectedManufacturingFacility
                Else
                    Return DefaultManufacturingFacility
                End If
            Case ProductionType.Reactions
                If SelectedReactionsFacility.FullyLoaded Then
                    Return SelectedReactionsFacility
                Else
                    Return DefaultReactionsFacility
                End If
            Case ProductionType.SubsystemManufacturing
                If SelectedSubsystemManufacturingFacility.FullyLoaded Then
                    Return SelectedSubsystemManufacturingFacility
                Else
                    Return DefaultSubsystemManufacturingFacility
                End If
            Case ProductionType.SuperManufacturing
                If SelectedSuperManufacturingFacility.FullyLoaded Then
                    Return SelectedSuperManufacturingFacility
                Else
                    Return DefaultSuperManufacturingFacility
                End If
            Case ProductionType.T3CruiserManufacturing
                If SelectedT3CruiserManufacturingFacility.FullyLoaded Then
                    Return SelectedT3CruiserManufacturingFacility
                Else
                    Return DefaultT3CruiserManufacturingFacility
                End If
            Case ProductionType.T3DestroyerManufacturing
                If SelectedT3DestroyerManufacturingFacility.FullyLoaded Then
                    Return SelectedT3DestroyerManufacturingFacility
                Else
                    Return DefaultT3DestroyerManufacturingFacility
                End If
            Case ProductionType.T3Invention
                If SelectedT3InventionFacility.FullyLoaded Then
                    Return SelectedT3InventionFacility
                Else
                    Return DefaultT3InventionFacility
                End If
            Case ProductionType.Reprocessing
                If SelectedReprocessingFacility.FullyLoaded Then
                    Return SelectedReprocessingFacility
                Else
                    Return DefaultRefiningFacility
                End If
            Case Else
                Return Nothing
        End Select

        Return Nothing

    End Function

    ' Gets the facility for manufacturing based on the bp data on initialization or sent bp data
    Public Function GetSelectedManufacturingFacility(BPGroupID As Integer, BPCategoryID As Integer,
                                                     Optional OverrideActivity As String = "") As IndustryFacility
        Dim TempGroupID As Integer
        Dim TempCategoryID As Integer
        Dim SelectedActivity As String

        ' If either one of the numbers are 0, then use the init data
        If BPGroupID = 0 Or BPCategoryID = 0 Then
            TempGroupID = SelectedBPGroupID
            TempCategoryID = SelectedBPCategoryID
        Else
            TempGroupID = BPGroupID
            TempCategoryID = BPCategoryID
        End If

        If OverrideActivity <> "" Then
            SelectedActivity = OverrideActivity
        Else
            ' default setting, if the bp is a reaction, then return the reaction facility not manufacturing
            SelectedActivity = ActivityManufacturing

            'Look up the groups for reactions
            If SelectedActivity = ActivityManufacturing Then
                Dim rsCheck As SQLiteDataReader
                DBCommand = New SQLiteCommand("SELECT DISTINCT ITEM_GROUP_ID FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID IN (SELECT typeID FROM INVENTORY_TYPES WHERE typeName LIKE '%Reaction Formula%')", EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                While rsCheck.Read
                    If rsCheck.GetInt32(0) = BPGroupID Then
                        SelectedActivity = ActivityReactions
                    End If
                End While
                rsCheck.Close()
            End If
        End If

        ' Determine the production type and then pull the correct facility for manufacturing only based on the category and group id not the activity selected
        Dim PT As ProductionType = GetProductionType(TempGroupID, TempCategoryID, SelectedActivity)

        Return GetFacility(PT)

    End Function

    ' Gets the facility for invention based on the bp data on initialization or sent bp data
    Public Function GetSelectedInventionFacility(Optional BPGroupID As Integer = 0, Optional BPCategoryID As Integer = 0) As IndustryFacility
        Dim TempGroupID As Integer
        Dim TempCategoryID As Integer

        ' If either one of the numbers are 0, then use the init data
        If BPGroupID = 0 Or BPCategoryID = 0 Then
            TempGroupID = SelectedBPGroupID
            TempCategoryID = SelectedBPCategoryID
        Else
            TempGroupID = BPGroupID
            TempCategoryID = BPCategoryID
        End If

        ' Determine the production type and then pull the correct facility for manufacturing only based on the category and group id not the activity selected
        Dim PT As ProductionType = GetProductionType(TempGroupID, TempCategoryID, ActivityInvention)

        Return GetFacility(PT)

    End Function

    ' Just return the current facility selected
    Public Function GetSelectedFacility() As IndustryFacility
        Return SelectedFacility
    End Function

    Public Sub UpdateRefineYieldLabel(NewValue As Double)
        If SelectedProductionType = ProductionType.Reprocessing Then
            txtFacilityManualME.Text = FormatPercent(NewValue, 2)
        End If
    End Sub

    ' Returns if the facility is fully loaded or not
    Public Function FullyLoaded() As Boolean
        Return SelectedFacility.FullyLoaded
    End Function

    ' Returns the current selected facility production type
    Public Function GetCurrentFacilityProductionType() As ProductionType
        Return SelectedFacility.FacilityProductionType
    End Function

    ' Updates the usage value   
    Public Sub UpdateUsage(ToolTipText As String)
        lblFacilityUsage.Text = FormatNumber(SelectedFacility.FacilityUsage)
        Call mainToolTip.SetToolTip(lblFacilityUsage, ToolTipText)
    End Sub

    ' Sets all the refine rates for the three different types of refinables for the selected facility
    Private Sub SetRefiningRates(ByRef ReprocessingFacility As IndustryFacility)
        With ReprocessingFacility
            If ReprocessingFacility.FacilityProductionType = ProductionType.Reprocessing Then
                Dim DefaultRefineRate As Double = .MaterialMultiplier
                .OreFacilityRefineRate = GetRefineRate(RefineMaterialType.Ore, DefaultRefineRate)
                .MoonOreFacilityRefineRate = GetRefineRate(RefineMaterialType.MoonOre, DefaultRefineRate)
                .IceFacilityRefineRate = GetRefineRate(RefineMaterialType.Ice, DefaultRefineRate)
                .ScrapmetalRefineRate = GetRefineRate(RefineMaterialType.Scrapmetal, DefaultRefineRate)
            End If
        End With
    End Sub

    ' Looks up any modules installed on the selected facility and returns the refining rate
    Private Function GetRefineRate(RefineType As RefineMaterialType, DefaultValue As Double) As Double
        Dim RefineValue As Double = DefaultValue

        ' Process all but scrapmetal to account for rigs/etc.
        If SelectedProductionType = ProductionType.Reprocessing And SelectedFacility.FacilityType = FacilityTypes.UpwellStructure Then
            If RefineType <> RefineMaterialType.Scrapmetal Then
                Dim ItemGroupID As Integer
                Dim ItemCategoryID As Integer = 25

                Select Case RefineType
                    Case RefineMaterialType.Ore
                        ItemGroupID = ItemIDs.Arkonor
                    Case RefineMaterialType.Ice
                        ItemGroupID = ItemIDs.IceGroupID
                    Case RefineMaterialType.MoonOre
                        ItemGroupID = ItemIDs.CommonMoonAsteroids
                End Select

                With SelectedFacility
                    Dim BonusValue As Double = .GetFacilityBonusMulitiplier(IndustryFacility.ModifierType.ReprocessingRate, .ActivityID, ItemGroupID, ItemCategoryID)
                    If BonusValue > 0 Then
                        RefineValue = (BonusValue * 100) * (.MaterialMultiplier / BaseRefineRate)
                    End If
                End With
            Else
                ' Scrapmetal is pretty basic - just return the base ME as 50% for all structures and we will adjust outside of object for scrapmetal
                RefineValue = BaseRefineRate
            End If
        End If

        Return RefineValue

    End Function

    Public Sub SetIgnoreInvention(ByVal Ignore As Boolean, ByVal InventionType As ProductionType, ByVal UsageCheckValue As Boolean)

        If Ignore Then
            ' Remove copy and invention activities
            cmbFacilityActivities.Items.Remove(ActivityInvention)
            cmbFacilityActivities.Items.Remove(ActivityCopying)
        Else
            ' Add the copy and invention activities
            If Not cmbFacilityActivities.Items.Contains(ActivityInvention) Then
                cmbFacilityActivities.Items.Add(ActivityInvention)
            End If
            If Not cmbFacilityActivities.Items.Contains(ActivityCopying) Then
                cmbFacilityActivities.Items.Add(ActivityCopying)
            End If
        End If

        ' Set the usage
        ChangingUsageChecks = True
        chkFacilityIncludeUsage.Checked = UsageCheckValue
        ChangingUsageChecks = False

    End Sub

    Private Sub txtFacilityManualME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualME.KeyPress
        SelectedFacility.ManualME = True
        Call SetResetRefresh()
        e.Handled = ProcessKeyPressInput(e)
    End Sub

    Private Sub txtFacilityManualME_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualME.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._ME)
            Call UpdateBlueprint()
        ElseIf e.KeyCode = Keys.Delete Then
            btnFacilitySave.Enabled = True
        End If
    End Sub

    Private Sub txtFacilityManualME_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualME.GotFocus
        Call txtFacilityManualCost.SelectAll()
    End Sub

    Private Sub txtFacilityManualME_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualME.LostFocus
        If SelectedFacility.ManualME Then
            Call SetManualTextBoxValue(BoxType._ME)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualTE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualTE.KeyPress
        SelectedFacility.ManualTE = True
        Call SetResetRefresh()
        e.Handled = ProcessKeyPressInput(e)
    End Sub

    Private Sub txtFacilityManualTE_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualTE.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._TE)
            Call UpdateBlueprint()
        ElseIf e.KeyCode = Keys.Delete Then
            btnFacilitySave.Enabled = True
        End If
    End Sub

    Private Sub txtFacilityManualTE_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTE.GotFocus
        Call txtFacilityManualTE.SelectAll()
    End Sub

    Private Sub txtFacilityManualTE_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTE.LostFocus
        If SelectedFacility.ManualTE Then
            Call SetManualTextBoxValue(BoxType._TE)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualCost.KeyPress
        SelectedFacility.ManualCost = True
        e.Handled = ProcessKeyPressInput(e)
    End Sub

    Private Sub txtFacilityManualCost_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualCost.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._Cost)
            Call UpdateBlueprint()
        ElseIf e.KeyCode = Keys.Delete Then
            btnFacilitySave.Enabled = True
        End If
    End Sub

    Private Sub txtFacilityManualCost_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualCost.GotFocus
        Call txtFacilityManualCost.SelectAll()
    End Sub

    Private Sub txtFacilityManualCost_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualCost.LostFocus
        If SelectedFacility.ManualCost Then
            Call SetManualTextBoxValue(BoxType._Cost)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualTax.KeyPress
        SelectedFacility.ManualTax = True
        Call SetResetRefresh()
        e.Handled = ProcessKeyPressInput(e)
    End Sub

    Private Sub txtFacilityManualTax_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualTax.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._Tax)
            Call UpdateBlueprint()
        ElseIf e.KeyCode = Keys.Delete Then
            btnFacilitySave.Enabled = True
        End If
    End Sub

    Private Sub txtFacilityManualTax_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTax.GotFocus
        Call txtFacilityManualTax.SelectAll()
    End Sub

    Private Sub txtFacilityManualTax_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTax.LostFocus
        If SelectedFacility.ManualTax Then
            Call SetManualTextBoxValue(BoxType._Tax)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Function FormatManualEntry(Entry As String) As Double
        Dim EntryText As String

        If Entry.Contains("%") Then
            ' Strip the percent first
            EntryText = Entry.Substring(0, Len(Entry) - 1)
        Else
            EntryText = Entry
        End If

        If IsNumeric(EntryText) Then
            Return CDbl(EntryText) / 100
        Else
            Return 0
        End If

    End Function

    Private Sub SetManualTextBoxValue(Box As BoxType)

        If Not FirstLoad And Not IsNothing(SelectedFacility) And Not UpdatingManualBoxes Then
            ' Format for text box
            UpdatingManualBoxes = True
            btnFacilitySave.Enabled = True

            Select Case Box
                Case BoxType._ME
                    If SelectedFacility.FacilityProductionType = ProductionType.Reprocessing And SelectedLocation = ProgramLocation.BlueprintTab Then
                        ' For reprocessing, just set it to what they put in as it's the rate they refine
                        ' This is the ore rate but for others, save it as the base refine rate
                        SelectedFacility.OreFacilityRefineRate = FormatManualEntry(txtFacilityManualME.Text)
                        GetFacility(SelectedFacility.FacilityProductionType).OreFacilityRefineRate = SelectedFacility.OreFacilityRefineRate
                        txtFacilityManualME.Text = FormatPercent(SelectedFacility.OreFacilityRefineRate, 2)
                    Else
                        SelectedFacility.MaterialMultiplier = 1 - FormatManualEntry(txtFacilityManualME.Text)
                        GetFacility(SelectedFacility.FacilityProductionType).MaterialMultiplier = SelectedFacility.MaterialMultiplier
                        txtFacilityManualME.Text = FormatPercent(1 - SelectedFacility.MaterialMultiplier, 2)
                        GetFacility(SelectedFacility.FacilityProductionType).ManualME = True
                    End If
                Case BoxType._TE
                    If SelectedFacility.FacilityProductionType = ProductionType.Reprocessing And SelectedLocation = ProgramLocation.BlueprintTab Then
                        ' For reprocessing, just set it to what they put in as it's the rate they refine
                        ' This is the ice rate
                        SelectedFacility.IceFacilityRefineRate = FormatManualEntry(txtFacilityManualTE.Text)
                        GetFacility(SelectedFacility.FacilityProductionType).TimeMultiplier = SelectedFacility.IceFacilityRefineRate
                        txtFacilityManualTE.Text = FormatPercent(SelectedFacility.IceFacilityRefineRate, 2)
                    Else
                        SelectedFacility.TimeMultiplier = 1 - FormatManualEntry(txtFacilityManualTE.Text)
                        GetFacility(SelectedFacility.FacilityProductionType).TimeMultiplier = SelectedFacility.TimeMultiplier
                        txtFacilityManualTE.Text = FormatPercent(1 - SelectedFacility.TimeMultiplier, 2)
                        GetFacility(SelectedFacility.FacilityProductionType).ManualTE = True
                    End If
                Case BoxType._Cost
                    SelectedFacility.CostMultiplier = 1 - FormatManualEntry(txtFacilityManualCost.Text)
                    GetFacility(SelectedFacility.FacilityProductionType).CostMultiplier = SelectedFacility.CostMultiplier
                    txtFacilityManualCost.Text = FormatPercent(1 - SelectedFacility.CostMultiplier, 2)
                    GetFacility(SelectedFacility.FacilityProductionType).ManualCost = True
                Case BoxType._Tax
                    SelectedFacility.TaxRate = FormatManualEntry(txtFacilityManualTax.Text)
                    GetFacility(SelectedFacility.FacilityProductionType).TaxRate = SelectedFacility.TaxRate
                    txtFacilityManualTax.Text = FormatPercent(SelectedFacility.TaxRate, 2)
                    GetFacility(SelectedFacility.FacilityProductionType).ManualTax = True
            End Select

            ' No longer a default
            Call SetDefaultVisuals(False)

            UpdatingManualBoxes = False

        End If

    End Sub

    Private Enum BoxType
        _ME = 0
        _TE = 1
        _Cost = 2
        _Tax = 3
    End Enum

    Private Function ProcessKeyPressInput(e As KeyPressEventArgs) As Boolean
        Dim EnableButton As Boolean = True
        Dim ReturnValue As Boolean = False

        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedNegativePercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                ReturnValue = True
                EnableButton = False
            End If
        End If

        ' If we set this to true, then we changed input and it's not default anymore
        Call SetDefaultVisuals(Not EnableButton)

        Call SetResetRefresh()

        Return ReturnValue

    End Function

    Private Sub POSCombos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbModules.SelectedIndexChanged, cmbFuelBlocks.SelectedIndexChanged, cmbLargeShips.SelectedIndexChanged

        Call SetResetRefresh()

    End Sub

    Private Sub RefreshMainBP(Optional UpdateUsageLabel As Boolean = False)
        ' See if we update the price labels on the BP tab
        If Not IsNothing(SelectedBlueprint) And SelectedLocation = ProgramLocation.BlueprintTab Then
            Call frmMain.RefreshBP()
            If UpdateUsageLabel Then
                Call UpdateUsage("")
            End If
        End If
    End Sub

    Private Sub btnConversiontoOreSettings_Click(sender As Object, e As EventArgs) Handles btnConversiontoOreSettings.Click
        ' Make sure it's not disposed
        If frmConversionOptions.IsDisposed Then
            ' Make new form
            frmConversionOptions = New frmConversiontoOreSettings()
        End If
        ' Save where this form is from
        frmConversionOptions.SelectedLocation = SelectedLocation

        ' Now open the form
        frmConversionOptions.Show()
        frmConversionOptions.Focus()

        Application.DoEvents()

    End Sub

    Private Sub chkFacilityConvertToOre_CheckedChanged(sender As Object, e As EventArgs) Handles chkFacilityConvertToOre.CheckedChanged
        If Not ChangingUsageChecks Then
            SelectedFacility.ConvertToOre = chkFacilityConvertToOre.Checked
            ' Facility is loaded, so save it to default and dynamic variable
            Call SetFacility(SelectedFacility, SelectedProductionType, False, False)
            Call RefreshMainBP()
            ' Update usage after completed building the bp
            lblFacilityUsage.Text = FormatNumber(GetSelectedFacility.FacilityUsage, 2)
        End If
    End Sub

#End Region

End Class

Public Enum ProgramLocation
    None = -1
    BlueprintTab = 0
    ManufacturingTab = 1
    MiningTab = 2
    ReprocessingPlant = 3
    SovBelts = 4
    IceBelts = 5
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
    Reactions = 11
    MoonDrilling = -1
    Reprocessing = -2
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
    Reactions = 11
    T3Invention = 12
    T3DestroyerManufacturing = 13

    ' New types
    Reprocessing = 17

End Enum

Public Enum RefineMaterialType
    Ore = 1
    Ice = 2
    MoonOre = 3
    Scrapmetal = 4
End Enum

Public Enum FacilityTypes
    None = -1
    Station = 0
    UpwellStructure = 3
End Enum

' Industry facility class, move to private use if possible
Public Class IndustryFacility
    Implements ICloneable

    ' For industry Facilities
    Public FacilityID As Long ' ID Of the facility
    Public FacilityName As String ' Station/Outpost Name or the Array name
    Public FacilityType As FacilityTypes ' Station, Upwell Structure
    Public FacilityProductionType As ProductionType ' What we are doing at this facility
    Public ActivityID As Integer ' Activity code of the facility
    Public Activity As String ' String value of the activity
    Public RegionName As String ' Region of this facility
    Public RegionID As Long
    Public SolarSystemName As String ' System where this is located
    Public SolarSystemID As Long
    Public SolarSystemSecurity As Double
    Public FWUpgradeLevel As Integer ' Level of the FW upgrade for this system (if applies)
    Public CostIndex As Double ' Cost index for the system and activity from ESI
    Public ActivityCostPerSecond As Double ' The cost to conduct the activity for this facility per second - my setting for ECs
    Public IsDefault As Boolean
    Public IncludeActivityCost As Boolean ' This is the total cost of materials to do the activiy
    Public IncludeActivityTime As Boolean ' This is the time for doing the activity
    Public IncludeActivityUsage As Boolean ' This is the cost of using the facility only

    Public FacilityUsage As Double ' The usage charged by this facility, set after bp has run
    Public UsageToolTipText As String ' The text to display for the usage label

    ' Nullable fields
    Public TaxRate As Double ' The tax rate
    Public MaterialMultiplier As Double ' The bonus material percentage or refining rate for materials used in this facility
    Public TimeMultiplier As Double ' The bonus to time to conduct an activity in this facility
    Public CostMultiplier As Double ' The bonus to cost to conduct an activity in this facility
    Public ManualME As Boolean ' To check for manual entries if different
    Public ManualTE As Boolean ' To check for manual entries if different
    Public ManualCost As Boolean ' To check for manual entries if different
    Public ManualTax As Boolean ' To check for manual entries if different
    Public BaseTax As Double ' Base tax rate from default
    Public BaseME As Double ' The ME bonus from default
    Public BaseTE As Double ' The TE bonus from default
    Public BaseCost As Double ' The Cost bonus from default

    Public FullyLoaded As Boolean ' This facility was fully loaded in all parts
    Private ControlLocation As ProgramLocation ' Where in the program is this facility
    Public ConvertToOre As Boolean ' if convert to ore option is selected

    ' For all the fitting data
    Private FittingBonuses As List(Of FittingBonusInfo)

    ' Default multiplier rates if we can't find them
    Public Const DefaultTaxRate As Double = 0
    Public Const DefaultMaterialMultiplier As Double = 1
    Public Const DefaultTimeMultiplier As Double = 1
    Public Const DefaultCostMultiplier As Double = 1

    ' Refine rates for the facility
    Public OreFacilityRefineRate As Double
    Public MoonOreFacilityRefineRate As Double
    Public IceFacilityRefineRate As Double
    Public ScrapmetalRefineRate As Double

    Private ThukkerRigIDs As List(Of Integer)

    Private Structure FittingBonusInfo
        Dim RigID As Integer
        Dim AttributeID As Integer
        Dim CategoryID As Object
        Dim GroupID As Object
        Dim ActivityID As Integer
        Dim Bonus As Double
    End Structure

    Private Structure Attributebonus
        Dim AttributeID As Integer
        Dim Bonus As Double
    End Structure

    Public Enum ModifierType
        MaterialModifier = 0
        TimeModifier = 1
        CostModifier = 2
        ReprocessingRate = 3
    End Enum

    Private BonusDataToFind As FittingBonusInfo

    Public Sub New()

        FacilityID = 0
        FacilityName = None
        FacilityType = FacilityTypes.None
        FacilityProductionType = ProductionType.None
        ActivityID = 0
        Activity = ""
        RegionName = None
        RegionID = 0
        SolarSystemName = None
        SolarSystemID = 0
        SolarSystemSecurity = 0
        FWUpgradeLevel = -1
        CostIndex = 0
        ActivityCostPerSecond = 0
        IsDefault = False
        IncludeActivityCost = False
        IncludeActivityTime = False
        IncludeActivityUsage = False

        TaxRate = 0
        MaterialMultiplier = 0
        TimeMultiplier = 0
        CostMultiplier = 0
        ManualTax = False
        ManualME = False
        ManualTE = False
        ManualCost = False
        BaseME = 0
        BaseTE = 0
        BaseCost = 0
        BaseTax = 0

        OreFacilityRefineRate = 0
        MoonOreFacilityRefineRate = 0
        IceFacilityRefineRate = 0
        ScrapmetalRefineRate = 0

        ControlLocation = Nothing
        ConvertToOre = False
        FullyLoaded = False

        ThukkerRigIDs = New List(Of Integer)
        FittingBonuses = New List(Of FittingBonusInfo)

    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New IndustryFacility

        CopyOfMe.FacilityID = FacilityID
        CopyOfMe.FacilityName = FacilityName
        CopyOfMe.FacilityType = FacilityType
        CopyOfMe.FacilityProductionType = FacilityProductionType
        CopyOfMe.ActivityID = ActivityID
        CopyOfMe.Activity = Activity
        CopyOfMe.RegionName = RegionName
        CopyOfMe.RegionID = RegionID
        CopyOfMe.SolarSystemName = SolarSystemName
        CopyOfMe.SolarSystemID = SolarSystemID
        CopyOfMe.SolarSystemSecurity = SolarSystemSecurity
        CopyOfMe.FWUpgradeLevel = FWUpgradeLevel
        CopyOfMe.CostIndex = CostIndex
        CopyOfMe.ActivityCostPerSecond = ActivityCostPerSecond
        CopyOfMe.IsDefault = IsDefault
        CopyOfMe.IncludeActivityCost = IncludeActivityCost
        CopyOfMe.IncludeActivityTime = IncludeActivityTime
        CopyOfMe.IncludeActivityUsage = IncludeActivityUsage
        CopyOfMe.FacilityUsage = FacilityUsage
        CopyOfMe.UsageToolTipText = UsageToolTipText
        CopyOfMe.TaxRate = TaxRate
        CopyOfMe.MaterialMultiplier = MaterialMultiplier
        CopyOfMe.TimeMultiplier = TimeMultiplier
        CopyOfMe.CostMultiplier = CostMultiplier
        CopyOfMe.ManualME = ManualME
        CopyOfMe.ManualTE = ManualTE
        CopyOfMe.ManualCost = ManualCost
        CopyOfMe.ManualTax = ManualTax
        CopyOfMe.BaseME = BaseME
        CopyOfMe.BaseTE = BaseTE
        CopyOfMe.BaseCost = BaseCost
        CopyOfMe.BaseTax = BaseTax
        CopyOfMe.OreFacilityRefineRate = OreFacilityRefineRate
        CopyOfMe.MoonOreFacilityRefineRate = MoonOreFacilityRefineRate
        CopyOfMe.IceFacilityRefineRate = IceFacilityRefineRate
        CopyOfMe.ScrapmetalRefineRate = ScrapmetalRefineRate
        CopyOfMe.FullyLoaded = FullyLoaded
        CopyOfMe.ControlLocation = ControlLocation
        CopyOfMe.ConvertToOre = ConvertToOre
        CopyOfMe.FittingBonuses = FittingBonuses
        CopyOfMe.ThukkerRigIDs = ThukkerRigIDs

        Return CopyOfMe

    End Function

    ' Load up the facility data from the table as default
    Public Sub InitalizeFacility(ByVal InitialProductionType As ProductionType, ByVal FacilityLocation As ProgramLocation)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        ' Load these for later use
        ThukkerRigIDs = New List(Of Integer)

        DBCommand = New SQLiteCommand("SELECT typeID FROM INVENTORY_TYPES WHERE typeName LIKE 'Standup %Thukker%' AND groupID <> 1708", EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read
            ThukkerRigIDs.Add(rsLoader.GetInt32(0))
        End While

        rsLoader.Close()

        ' Save the reference to the program location
        ControlLocation = FacilityLocation

        ' Look up all the data in two queries - first base data and try to get the multipliers and cost data - it should only be there for saved outposts (which are being removed)
        SQL = "SELECT SF.FACILITY_ID, SF.FACILITY_TYPE, "
        SQL &= "FACILITY_PRODUCTION_TYPES.ACTIVITY_ID, INDUSTRY_ACTIVITIES.activityName, "
        SQL &= "REGIONS.regionName, REGIONS.regionID, SOLAR_SYSTEMS.solarSystemName, SOLAR_SYSTEMS.solarSystemID, "
        SQL &= "CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL, SF.ACTIVITY_COST_PER_SECOND, "
        SQL &= "CASE WHEN COST_INDEX IS NULL THEN 0 ELSE COST_INDEX END AS COST_INDEX,"
        SQL &= "SF.INCLUDE_ACTIVITY_COST, SF.INCLUDE_ACTIVITY_TIME, SF.INCLUDE_ACTIVITY_USAGE, "
        SQL &= "SF.FACILITY_TAX, SF.MATERIAL_MULTIPLIER, SF.TIME_MULTIPLIER, SF.COST_MULTIPLIER, security, CONVERT_TO_ORE "
        SQL &= "FROM SAVED_FACILITIES AS SF, FACILITY_PRODUCTION_TYPES, REGIONS, SOLAR_SYSTEMS, FACILITY_TYPES, INDUSTRY_ACTIVITIES "
        SQL &= "LEFT JOIN FW_SYSTEM_UPGRADES ON FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID "
        SQL &= "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES "
        SQL &= "ON INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = FACILITY_PRODUCTION_TYPES.ACTIVITY_ID "
        SQL &= "WHERE SF.PRODUCTION_TYPE = FACILITY_PRODUCTION_TYPES.PRODUCTION_TYPE "
        SQL &= "AND SF.REGION_ID = REGIONS.regionID "
        SQL &= "AND SF.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND SF.FACILITY_TYPE = FACILITY_TYPES.FACILITY_TYPE_ID "
        SQL &= "AND FACILITY_PRODUCTION_TYPES.ACTIVITY_ID = INDUSTRY_ACTIVITIES.activityID "
        SQL &= String.Format("AND SF.PRODUCTION_TYPE = {0} AND SF.FACILITY_VIEW = {1} ", CStr(InitialProductionType), CStr(FacilityLocation))

        Dim SQLCharID As String = "AND CHARACTER_ID = {0}"
        Dim CharID As String = ""

        ' See what type of character ID
        If UserApplicationSettings.SaveFacilitiesbyChar Then
            CharID = CStr(SelectedCharacter.ID)
        Else
            CharID = CStr(CommonSavedFacilitiesID)
        End If

        ' First look up the character to see if it's saved there first (initially only do one set of facilities then allow by character via a setting)
        DBCommand = New SQLiteCommand(SQL & String.Format(SQLCharID, CharID), EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader
        rsLoader.Read()

        If Not rsLoader.HasRows Then
            ' Need to look up the default
            rsLoader.Close()
            DBCommand = New SQLiteCommand(SQL & String.Format(SQLCharID, "0"), EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            rsLoader.Read()
        End If

        ' Should have data one way or another now
        If rsLoader.HasRows Then
            With rsLoader
                FacilityID = .GetInt32(0)
                FacilityType = CType(.GetInt32(1), FacilityTypes) ' Station, Upwell Structure, etc.
                FacilityProductionType = InitialProductionType
                ActivityID = .GetInt32(2)
                Activity = .GetString(3)
                RegionName = .GetString(4)
                RegionID = .GetInt64(5)
                ' Paste the cost index to the solar system name
                CostIndex = .GetFloat(10)
                If InitialProductionType <> ProductionType.Reprocessing Then
                    SolarSystemName = .GetString(6) & " (" & FormatNumber(CostIndex, 4) & ")"
                Else
                    SolarSystemName = .GetString(6)
                End If
                SolarSystemID = .GetInt64(7)
                SolarSystemSecurity = .GetDouble(18)
                FWUpgradeLevel = .GetInt32(8)
                ActivityCostPerSecond = .GetFloat(9)
                ConvertToOre = CBool(.GetInt32(19))

                IncludeActivityCost = CBool(.GetInt32(11))
                IncludeActivityTime = CBool(.GetInt32(12))
                IncludeActivityUsage = CBool(.GetInt32(13))

                ' Refresh the rigs installed for this facility and information to apply the bonuses
                Call UpdateProductionFittingInformation(CLng(CharID))

                ' Save these values for later lookup - use -1 for null indicator - these are what they saved manually - can't save a value for a station
                If IsDBNull(.GetValue(14)) Or FacilityType = FacilityTypes.Station Then
                    TaxRate = -1
                Else
                    TaxRate = .GetDouble(14)
                End If

                If IsDBNull(.GetValue(15)) Or FacilityType = FacilityTypes.Station Then
                    MaterialMultiplier = -1
                Else
                    MaterialMultiplier = .GetDouble(15)
                End If

                If IsDBNull(.GetValue(16)) Or FacilityType = FacilityTypes.Station Then
                    TimeMultiplier = -1
                Else
                    TimeMultiplier = .GetDouble(16)
                End If

                If IsDBNull(.GetValue(17)) Or FacilityType = FacilityTypes.Station Then
                    CostMultiplier = -1
                Else
                    CostMultiplier = .GetDouble(17)
                End If

                ' Now, depending on type, look up the name, cost index, tax, and multipliers from the stations table (this is mainly for speed)
                If FacilityType = FacilityTypes.Station Then ' Stations
                    If InitialProductionType = ProductionType.Reprocessing Then
                        SQL = "SELECT STATION_NAME, REPROCESSING_TAX_RATE, REPROCESSING_EFFICIENCY, 0, 0 "
                    Else
                        SQL = "SELECT STATION_NAME," & CStr(DefaultStationTaxRate) & ", 1, 1, 1 "
                    End If
                    SQL &= "FROM STATIONS WHERE STATION_ID = " & CStr(FacilityID) & " "
                ElseIf FacilityType = FacilityTypes.UpwellStructure Then
                    SQL = "SELECT DISTINCT UPWELL_STRUCTURE_NAME, " & CStr(DefaultStructureTaxRate) & " AS FACILITY_TAX, "
                    SQL &= "CASE WHEN ACTIVITY_ID = -2 THEN " & CStr(BaseRefineRate) & " * (1 + MATERIAL_MULTIPLIER) ELSE MATERIAL_MULTIPLIER END, TIME_MULTIPLIER, COST_MULTIPLIER "
                    SQL &= "FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_TYPE_ID = " & CStr(FacilityID) & " "
                    SQL &= "AND ACTIVITY_ID = " & CStr(ActivityID) & " "
                End If

                rsLoader.Close()
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                If rsLoader.HasRows Then
                    With rsLoader
                        FacilityName = .GetString(0) ' Need to deal with the case of all on calc base facility

                        If IsDBNull(.GetValue(2)) And MaterialMultiplier = -1 Then
                            MaterialMultiplier = DefaultMaterialMultiplier
                        ElseIf MaterialMultiplier = -1 Then
                            MaterialMultiplier = .GetDouble(2)
                        End If

                        BaseME = .GetDouble(2)

                        If IsDBNull(.GetValue(3)) And TimeMultiplier = -1 Then
                            TimeMultiplier = DefaultTimeMultiplier
                        ElseIf TimeMultiplier = -1 Then
                            TimeMultiplier = .GetDouble(3)
                        End If

                        BaseTE = .GetDouble(3)

                        If IsDBNull(.GetValue(4)) And CostMultiplier = -1 Then
                            CostMultiplier = DefaultCostMultiplier
                        ElseIf CostMultiplier = -1 Then
                            CostMultiplier = .GetDouble(4)
                        End If

                        BaseCost = .GetDouble(4)

                        ' For the remaining values, if they saved a value manually, then use that, else use what was queried and if null, use the default
                        If IsDBNull(.GetValue(1)) And TaxRate = -1 Then
                            ' Nothing in DB and they didn't save anything, so use the default rate
                            TaxRate = DefaultTaxRate
                        ElseIf TaxRate = -1 Then
                            ' Nothing from saved facilties, so use what is in SDE unless it's 
                            If InitialProductionType = ProductionType.Reprocessing And FacilityType = FacilityTypes.Station Then
                                ' Calculate it for reprocessing in stations - structures are always 0 unless saved otherwise
                                TaxRate = CalculateStationReprocessingTaxRate(CLng(CharID), FacilityID, Nothing)
                            Else
                                TaxRate = .GetDouble(1)
                            End If
                        End If

                        BaseTax = TaxRate

                    End With
                Else
                    ' Something went wrong
                    MsgBox("The facility failed To load", vbCritical, Application.ProductName)
                    GoTo ExitBlock
                End If

                FullyLoaded = True

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
        On Error GoTo 0

    End Sub

    Public Function CalculateStationReprocessingTaxRate(CharacterID As Long, SentFacilityID As Long, ByRef EfficiencyRate As Double) As Double
        Dim Standing As Double = 0
        Dim CalcTax As Double = 0
        Dim UnadjustedCorpStanding As Double = 0

        Dim SQL As String = ""
        Dim rsStats As SQLiteDataReader

        SQL = "SELECT REPROCESSING_TAX_RATE, CASE WHEN STANDING IS NULL THEN 0 ELSE STANDING END, REPROCESSING_EFFICIENCY FROM STATIONS "
        SQL &= "LEFT JOIN CHARACTER_STANDINGS ON STATIONS.CORPORATION_ID = CHARACTER_STANDINGS.NPC_TYPE_ID And CHARACTER_STANDINGS.CHARACTER_ID = {0} WHERE STATION_ID = {1}"

        DBCommand = New SQLiteCommand(String.Format(SQL, CharacterID, SentFacilityID), EVEDB.DBREf)
        rsStats = DBCommand.ExecuteReader
        rsStats.Read()

        UnadjustedCorpStanding = rsStats.GetDouble(1)
        EfficiencyRate = rsStats.GetDouble(2)

        ' Refinery Tax rate will be based on standings with station corp
        ' Effective Standing = Unadjusted Standing + (10 - Unadjusted Standing) * 4% * Connections Skill Level
        Standing = UnadjustedCorpStanding + (10 - UnadjustedCorpStanding) * (0.04 * SelectedCharacter.Skills.GetSkillLevel(3359))
        ' Rate = StationBaseTaxRate * (1-(.75 * CorpStanding)/5) - Note: Corp Standing affected by Connections
        CalcTax = rsStats.GetDouble(0) * (1 - (0.75 * Standing) / 5)

        If CalcTax < 0 Then
            CalcTax = 0
        End If

        rsStats.Close()

        Return CalcTax

    End Function

    Public Sub ResetManualToggles()
        ManualME = False
        ManualTE = False
        ManualCost = False
    End Sub

    Public Function SaveFacility(CharacterID As Long, Location As ProgramLocation, SupressNotice As Boolean) As Boolean
        Dim SQL As String
        Dim TempSQL As String
        Dim rsCheck As SQLiteDataReader
        Dim ManualEntries As Boolean = False
        Dim LocationList As New List(Of Integer)
        Dim LID As Integer

        Try

            If UserApplicationSettings.ShareSavedFacilities Then
                ' Need to get each location for saving
                For Each LID In System.Enum.GetValues(GetType(ProgramLocation))
                    Call LocationList.Add(LID)
                Next
            Else
                ' Just use the one sent
                Call LocationList.Add(Location)
            End If

            For Each LID In LocationList
                ' See if the record exists - only save one set of facilities for now
                SQL = String.Format("SELECT 'X' FROM SAVED_FACILITIES WHERE PRODUCTION_TYPE = {0} AND FACILITY_VIEW = {1} AND CHARACTER_ID = {2}", CInt(FacilityProductionType), LID, CharacterID)
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read() Then
                    ' Need to update
                    TempSQL = "UPDATE SAVED_FACILITIES "
                    TempSQL &= "SET FACILITY_ID = {0}, "
                    TempSQL &= "FACILITY_TYPE = {1}, "
                    TempSQL &= "REGION_ID = {2}, "
                    TempSQL &= "SOLAR_SYSTEM_ID = {3}, "
                    TempSQL &= "ACTIVITY_COST_PER_SECOND = {4}, "
                    TempSQL &= "INCLUDE_ACTIVITY_COST = {5}, "
                    TempSQL &= "INCLUDE_ACTIVITY_TIME = {6}, "
                    TempSQL &= "INCLUDE_ACTIVITY_USAGE = {7}, "
                    TempSQL &= "CONVERT_TO_ORE = {8},"
                    TempSQL &= "FACILITY_TYPE_ID = {11},"

                    If FacilityType = FacilityTypes.UpwellStructure Then
                        ' if what they have now is different from what they started with, then they made a change
                        ' for upwell structures, the base is updated when they make changes to the facility fitting
                        '    If ManualTax Then - Tax is always manual so just save what was set earlier. 
                        TempSQL &= "FACILITY_TAX = " & CStr(TaxRate) & ", "
                        ManualEntries = False ' Tax rate won't affect any of the multiplers so don't reset with rigs
                        'Else
                        '    TempSQL &= "FACILITY_TAX = NULL, "
                        'End If

                        If ManualME Then
                            TempSQL &= "MATERIAL_MULTIPLIER = " & CStr(MaterialMultiplier) & ", "
                            ManualEntries = True
                        Else
                            TempSQL &= "MATERIAL_MULTIPLIER = NULL, "
                        End If

                        If ManualTE Then
                            TempSQL &= "TIME_MULTIPLIER = " & CStr(TimeMultiplier) & ", "
                            ManualEntries = True
                        Else
                            TempSQL &= "TIME_MULTIPLIER = NULL, "
                        End If
                        If ManualCost Then
                            TempSQL &= "COST_MULTIPLIER = " & CStr(CostMultiplier) & " "
                            ManualEntries = True
                        Else
                            TempSQL &= "COST_MULTIPLIER = NULL "
                        End If
                    Else
                        TempSQL &= "MATERIAL_MULTIPLIER = NULL, "
                        TempSQL &= "TIME_MULTIPLIER = NULL, "
                        TempSQL &= "COST_MULTIPLIER = NULL, "
                        TempSQL &= "FACILITY_TAX = NULL "
                    End If

                    TempSQL &= "WHERE PRODUCTION_TYPE = {9} AND CHARACTER_ID = {10} "
                    TempSQL &= "AND FACILITY_VIEW = " & CStr(LID)

                    SQL = String.Format(TempSQL, FacilityID, CInt(FacilityType), RegionID, SolarSystemID, ActivityCostPerSecond,
                    CInt(IncludeActivityCost), CInt(IncludeActivityTime), CInt(IncludeActivityUsage), ConvertToOre, CInt(FacilityProductionType),
                    CharacterID, CStr(FacilityType))

                Else
                    Dim MEValue As String = "NULL"
                    Dim TEValue As String = "NULL"
                    Dim CostValue As String = "NULL"
                    Dim TaxValue As String = "NULL"

                    If FacilityType = FacilityTypes.UpwellStructure Then
                        ' if what they have now is different from what they started with, then they made a change
                        ' for upwell structures, the base is updated when they make changes to the facility fitting
                        If ManualTax Then
                            TaxValue = CStr(TaxRate)
                            ManualEntries = False ' Tax rate won't affect any of the multiplers so don't reset rigs
                        End If

                        If ManualME Then
                            MEValue = CStr(MaterialMultiplier)
                            ManualEntries = True
                        End If

                        If ManualTE Then
                            TEValue = CStr(TimeMultiplier)
                            ManualEntries = True
                        End If
                        If ManualCost Then
                            CostValue = CStr(CostMultiplier)
                            ManualEntries = True
                        End If
                    End If

                    ' Insert
                    SQL = String.Format("INSERT INTO SAVED_FACILITIES VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16});",
                                        CharacterID, CInt(FacilityProductionType), LID, FacilityID, CStr(FacilityType), CStr(FacilityType), RegionID, SolarSystemID, ActivityCostPerSecond,
                                        CInt(IncludeActivityCost), CInt(IncludeActivityTime), CInt(IncludeActivityUsage), TaxValue, MEValue, TEValue, CostValue, ConvertToOre)

                End If

                ' Save it
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                rsCheck.Close()
            Next

            ' If they save a structure with manual values, then delete any fittings they may have saved for this structure
            If FacilityType = FacilityTypes.UpwellStructure Then
                If ManualEntries Then
                    SQL = "DELETE FROM UPWELL_STRUCTURES_INSTALLED_MODULES WHERE CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_VIEW = {3} AND FACILITY_ID = {4}"
                    EVEDB.ExecuteNonQuerySQL(String.Format(SQL, CharacterID, CInt(FacilityProductionType), SolarSystemID, LID, FacilityID))
                End If
            End If

            ' Update FW upgrade
            If FWUpgradeLevel <> -1 Then
                ' See if we update or insert
                SQL = "SELECT * FROM FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = " & CStr(SolarSystemID) & " "

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader
                rsCheck.Read()

                If rsCheck.HasRows Then
                    SQL = "UPDATE FW_SYSTEM_UPGRADES SET UPGRADE_LEVEL = " & CStr(FWUpgradeLevel)
                    SQL &= " WHERE SOLAR_SYSTEM_ID = " & SolarSystemID
                Else
                    SQL = "INSERT INTO FW_SYSTEM_UPGRADES VALUES (" & SolarSystemID & "," & CStr(FWUpgradeLevel) & ")"
                End If

                Call EVEDB.ExecuteNonQuerySQL(SQL)
                rsCheck.Close()
            End If

            ' If this is an upwell, and we are saving multiple structures (shared) then we need to update all the shared structure modules as well
            If FacilityType = FacilityTypes.UpwellStructure And UserApplicationSettings.ShareSavedFacilities Then
                Dim InstalledModules As New List(Of Integer)
                ' Look up all the installed modules for the location sent
                SQL = String.Format("SELECT INSTALLED_MODULE_ID FROM UPWELL_STRUCTURES_INSTALLED_MODULES WHERE PRODUCTION_TYPE = {0} AND FACILITY_VIEW = {1} AND CHARACTER_ID = {2} AND FACILITY_ID = {3} AND SOLAR_SYSTEM_ID = {4}",
                                    CInt(FacilityProductionType), CStr(Location), CharacterID, FacilityID, SolarSystemID)
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                While rsCheck.Read()
                    InstalledModules.Add(rsCheck.GetInt32(0))
                End While

                rsCheck.Close()

                If InstalledModules.Count > 0 Then
                    ' Delete all in there first for this location ID
                    EVEDB.BeginSQLiteTransaction()
                    SQL = "DELETE FROM UPWELL_STRUCTURES_INSTALLED_MODULES WHERE CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_ID = {3}"
                    EVEDB.ExecuteNonQuerySQL(String.Format(SQL, CharacterID, CStr(FacilityProductionType), SolarSystemID, FacilityID))

                    ' Now insert all the modules installed on this structure for sharing
                    For Each ModID In InstalledModules
                        For Each LID In LocationList
                            SQL = String.Format("INSERT INTO UPWELL_STRUCTURES_INSTALLED_MODULES VALUES({0},{1},{2},{3},{4},{5})",
                                    CharacterID, CStr(FacilityProductionType), SolarSystemID, CStr(LID), FacilityID, ModID)
                            EVEDB.ExecuteNonQuerySQL(SQL)
                        Next
                    Next
                    EVEDB.CommitSQLiteTransaction()
                End If
            End If

            ' Refresh the main facilites if sharing facility saves
            If UserApplicationSettings.ShareSavedFacilities Then
                ' Refresh the facilities - except the one I just saved it on
                If FacilityProductionType = ProductionType.Reprocessing Then
                    If Location = ProgramLocation.ReprocessingPlant Or Location = ProgramLocation.SovBelts Or Location = ProgramLocation.IceBelts Then
                        ' If any of these checked, need to refresh the bp, mining, and manufacturing tabs - the others will load when opened
                        Call frmMain.LoadFacilities(ProgramLocation.BlueprintTab, FacilityProductionType)
                        Call frmMain.LoadFacilities(ProgramLocation.MiningTab, FacilityProductionType)
                        Call frmMain.LoadFacilities(ProgramLocation.ManufacturingTab, FacilityProductionType)
                    ElseIf Location = ProgramLocation.BlueprintTab Then
                        Call frmMain.LoadFacilities(ProgramLocation.MiningTab, FacilityProductionType)
                        Call frmMain.LoadFacilities(ProgramLocation.ManufacturingTab, FacilityProductionType)
                    ElseIf Location = ProgramLocation.MiningTab Then
                        Call frmMain.LoadFacilities(ProgramLocation.BlueprintTab, FacilityProductionType)
                        Call frmMain.LoadFacilities(ProgramLocation.ManufacturingTab, FacilityProductionType)
                    ElseIf Location = ProgramLocation.ManufacturingTab Then
                        Call frmMain.LoadFacilities(ProgramLocation.BlueprintTab, FacilityProductionType)
                        Call frmMain.LoadFacilities(ProgramLocation.MiningTab, FacilityProductionType)
                    End If
                    If Location <> ProgramLocation.ReprocessingPlant And ReprocessingPlantOpen Then
                        Call CType(Application.OpenForms.Item("frmReprocessingPlant"), frmReprocessingPlant).InitializeReprocessingFacility()
                    End If

                    'If Location <> ProgramLocation.IceBelts And IceBeltFlipOpen And FacilityProductionType = ProductionType.Reprocessing Then
                    '    Call CType(Application.OpenForms.Item("frmIceBeltFlip"), frmIceBeltFlip).InitializeReprocessingFacility()
                    'End If

                    'If Location <> ProgramLocation.SovBelts And OreBeltFlipOpen And FacilityProductionType = ProductionType.Reprocessing Then
                    '    Call CType(Application.OpenForms.Item("frmIndustryBeltFlip"), frmIndustryBeltFlip).InitializeReprocessingFacility()
                    'End If
                Else
                    ' Non-reprocessing is just limited to bp and manufacturing tab
                    If Location = ProgramLocation.BlueprintTab Then
                        Call frmMain.LoadFacilities(ProgramLocation.ManufacturingTab, FacilityProductionType)
                    Else
                        Call frmMain.LoadFacilities(ProgramLocation.BlueprintTab, FacilityProductionType)
                    End If
                End If
            End If

            If Not SupressNotice Then
                Call MsgBox("Facility Saved", vbInformation, Application.ProductName)
            End If

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
            ElseIf .FacilityProductionType <> FacilityProductionType Then
                Return False
            ElseIf .FacilityName <> FacilityName Then
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
            ElseIf .MaterialMultiplier <> MaterialMultiplier Then
                Return False
            ElseIf .TimeMultiplier <> TimeMultiplier Then
                Return False
            ElseIf .CostMultiplier <> CostMultiplier Then
                Return False
            ElseIf .IncludeActivityCost <> IncludeActivityCost And CompareCostCheck Then
                Return False
            ElseIf .IncludeActivityTime <> IncludeActivityTime And CompareTimeCheck Then
                Return False
            ElseIf .IncludeActivityUsage <> IncludeActivityUsage Then
                Return False
            ElseIf .ConvertToOre <> ConvertToOre And FacilityProductionType = ProductionType.Reprocessing Then
                Return False
            End If
        End With

        Return True

    End Function

    Public Function GetFacilityTypeDescription() As String
        Select Case FacilityType
            Case FacilityTypes.Station
                Return ManufacturingFacility.StationFacility
            Case FacilityTypes.UpwellStructure
                Return ManufacturingFacility.StructureFacility
            Case FacilityTypes.None
                Return None
            Case Else
                Return None
        End Select
    End Function

    ' Refreshes the MM/TM/CM bonuses for all three for the information sent if it's a structure
    Public Sub RefreshMMTMCMBonuses(ItemGroupID As Integer, ItemCategoryID As Integer)
        If FacilityType <> FacilityTypes.Station Then
            ' Cost
            If Not ManualCost Then
                CostMultiplier = BaseCost * (1 - GetFacilityBonusMulitiplier(ModifierType.CostModifier, ActivityID, ItemGroupID, ItemCategoryID))
            End If

            ' Material
            If Not ManualME Then
                MaterialMultiplier = BaseME * (1 - GetFacilityBonusMulitiplier(ModifierType.MaterialModifier, ActivityID, ItemGroupID, ItemCategoryID))
            End If

            ' Time
            If Not ManualTE Then
                TimeMultiplier = BaseTE * (1 - GetFacilityBonusMulitiplier(ModifierType.TimeModifier, ActivityID, ItemGroupID, ItemCategoryID))
            End If
        End If
    End Sub

    ' Predicate for searching fitting bonuses 
    Private Function FindStructureBonus(ByVal Item As FittingBonusInfo) As Boolean
        'SQL &= "AND ((categoryID = {5} AND groupID IS NULL) OR (categoryID IS NULL AND groupID = {6}))"
        With BonusDataToFind
            If .ActivityID = Item.ActivityID And .AttributeID = Item.AttributeID Then
                If IsNothing(Item.GroupID) Then
                    If CInt(.CategoryID) = CInt(Item.CategoryID) Then
                        Return True
                    End If
                Else
                    ' Groupid
                    If CInt(.GroupID) = CInt(Item.GroupID) Then
                        Return True
                    End If
                End If
            End If
        End With

        Return False

    End Function

    Public Function GetFacilityBonusMulitiplier(BonusType As ModifierType, ActivityID As Integer, ItemGroupID As Integer, ItemCategoryID As Integer) As Double
        Dim FoundData As FittingBonusInfo
        Dim CategoryID As Integer
        Dim GroupID As Integer

        Select Case ActivityID
            Case IndustryActivities.Copying, IndustryActivities.Invention, IndustryActivities.ResearchingTimeLevel, IndustryActivities.ResearchingMaterialLevel
                CategoryID = 9
                GroupID = Nothing
            Case Else
                CategoryID = ItemCategoryID
                GroupID = ItemGroupID
        End Select

        ' Set the base lookup data
        BonusDataToFind.ActivityID = ActivityID
        If Not IsNothing(ItemCategoryID) Then
            BonusDataToFind.CategoryID = CategoryID
        Else
            BonusDataToFind.CategoryID = Nothing
        End If
        If Not IsNothing(ItemGroupID) Then
            BonusDataToFind.GroupID = GroupID
        Else
            BonusDataToFind.GroupID = Nothing
        End If

        Select Case BonusType
            Case ModifierType.MaterialModifier
                ' Look up thukker first if it's in the correct groupids
                If Not IsNothing(ItemGroupID) Then
                    If ItemGroupID = ItemIDs.AdvCapitalComponentGroupID Or ItemGroupID = ItemIDs.CapitalComponentGroupID Then
                        ' Look for thukker bonus
                        BonusDataToFind.AttributeID = ItemAttributes.attributeThukkerEngRigMatBonus
                        FoundData = FittingBonuses.Find(AddressOf FindStructureBonus)

                        If FoundData.Bonus > 0 Then
                            ' Found the tukker bonus and it's in the groups we are looking at
                            If ThukkerRigIDs.Contains(FoundData.RigID) Then
                                Return FoundData.Bonus
                            End If
                        End If
                    End If

                    ' Just look up the normal Material bonus
                    BonusDataToFind.AttributeID = ItemAttributes.attributeEngRigMatBonus

                End If
            Case ModifierType.TimeModifier
                BonusDataToFind.AttributeID = ItemAttributes.attributeEngRigTimeBonus
            Case ModifierType.CostModifier
                BonusDataToFind.AttributeID = ItemAttributes.attributeEngRigCostBonus
            Case ModifierType.ReprocessingRate
                BonusDataToFind.AttributeID = ItemAttributes.refiningYieldMultiplier
            Case Else
                Return 0
        End Select

        FoundData = FittingBonuses.Find(AddressOf FindStructureBonus)

        Return FoundData.Bonus

    End Function

    ' Updates the fitting bonuses for the facility if upwell and saves that list locally to the facility for searching later
    Public Sub UpdateProductionFittingInformation(CharacterID As Long)
        Dim RigBonuses As New List(Of Attributebonus)
        Dim SQL As String
        Dim rsLookup1 As SQLiteDataReader
        Dim rsLookup2 As SQLiteDataReader
        Dim RigID As Integer
        Dim Info As FittingBonusInfo

        If FacilityType = FacilityTypes.UpwellStructure Then
            ' Reset the list
            FittingBonuses = New List(Of FittingBonusInfo)

            ' Get all the rigs and category/group combos installed on the facility
            SQL = "SELECT DISTINCT INSTALLED_MODULE_ID FROM UPWELL_STRUCTURES_INSTALLED_MODULES AS USIM, ENGINEERING_RIG_BONUSES AS ERB WHERE ERB.typeID = USIM.INSTALLED_MODULE_ID  "
            SQL &= "AND CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_VIEW = {3} AND FACILITY_ID = {4}"
            DBCommand = New SQLiteCommand(String.Format(SQL, CharacterID, CStr(FacilityProductionType), CStr(SolarSystemID), CStr(ControlLocation), CStr(FacilityID)), EVEDB.DBREf)
            rsLookup1 = DBCommand.ExecuteReader

            While rsLookup1.Read()
                RigID = rsLookup1.GetInt32(0)
                ' Get the bonues that apply for each rig
                RigBonuses = GetRigBonuses(RigID, SolarSystemID)

                ' Now lookup all category/group combos and apply all bonuses to it
                DBCommand = New SQLiteCommand("SELECT groupID, categoryID, activityID FROM ENGINEERING_RIG_BONUSES WHERE typeID =" & CStr(RigID), EVEDB.DBREf)
                rsLookup2 = DBCommand.ExecuteReader

                While rsLookup2.Read
                    For Each BonusData In RigBonuses
                        Info.RigID = RigID
                        Info.AttributeID = BonusData.AttributeID
                        Info.Bonus = BonusData.Bonus
                        If Not IsDBNull(rsLookup2.GetValue(0)) Then
                            Info.GroupID = rsLookup2.GetValue(0)
                        Else
                            Info.GroupID = Nothing
                        End If
                        If Not IsDBNull(rsLookup2.GetValue(1)) Then
                            Info.CategoryID = rsLookup2.GetValue(1)
                        Else
                            Info.CategoryID = Nothing
                        End If

                        Info.ActivityID = rsLookup2.GetInt32(2)

                        ' Insert the fitting bonus
                        FittingBonuses.Add(Info)
                    Next
                End While
                rsLookup2.Close()
            End While

            rsLookup1.Close()
        End If

    End Sub

    ' Returns the Structure modifier based on installed rigs for the sent type
    Public Function GetStructureModifier(SentModifier As ModifierType, BaseValue As Double, _SystemID As Long, _Activity As String,
                                         _FacilityID As Long, _ItemGroupID As Integer, _ItemCategoryID As Integer,
                                         CharacterID As Long, Location As ProgramLocation) As Double
        Dim rsLoader As SQLiteDataReader
        Dim InstalledModules = New List(Of Integer) ' Reset

        Dim MM As Double = 1
        Dim TM As Double = 1
        Dim CM As Double = 1
        Dim RefineValue As Double = 0

        InstalledModules = GetInstalledModules(_Activity, _FacilityID, _ItemGroupID, _ItemCategoryID, _SystemID, CharacterID, Location)

        If InstalledModules.Count <> 0 Then
            ' Get a list of the IDs that we want to use the thukker mat bonus on
            Dim ThukkerRigIDs As New List(Of Integer)
            Dim AttributeID As Integer = 0

            DBCommand = New SQLiteCommand("SELECT typeID FROM INVENTORY_TYPES WHERE typeName LIKE 'Standup %Thukker%' AND groupID <> 1708", EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader

            While rsLoader.Read
                ThukkerRigIDs.Add(rsLoader.GetInt32(0))
            End While

            rsLoader.Close()

            ' Now, adjust the MM, TM, CM based on modules installed
            For Each RigID In InstalledModules
                ' Look up the bonus while adjusting for the type of space we are in
                Dim RigBonuses As List(Of Attributebonus)
                RigBonuses = GetRigBonuses(RigID, _SystemID)

                ' Adjust MM, TM, CM by attribute and set the base to this as well, override whatever they had before
                For Each Rig In RigBonuses
                    Select Case Rig.AttributeID
                        Case ItemAttributes.attributeEngRigCostBonus
                            ' Cost
                            CM *= (1 - Rig.Bonus)
                        Case ItemAttributes.attributeEngRigMatBonus, ItemAttributes.RefRigMatBonus, ItemAttributes.attributeThukkerEngRigMatBonus
                            ' ME - Thukker only applies to cap components and advanced versions, else use the regular bonus
                            If (ThukkerRigIDs.Contains(RigID) And AttributeID = ItemAttributes.attributeThukkerEngRigMatBonus _
                                    And (_ItemGroupID = ItemIDs.AdvCapitalComponentGroupID Or _ItemGroupID = ItemIDs.CapitalComponentGroupID)) _
                                    Or Not ThukkerRigIDs.Contains(RigID) And AttributeID <> ItemAttributes.attributeThukkerEngRigMatBonus Then
                                MM *= (1 - Rig.Bonus)
                            End If
                        Case ItemAttributes.attributeEngRigTimeBonus, ItemAttributes.RefRigTimeBonus
                            ' TE
                            TM *= (1 - Rig.Bonus)
                        Case ItemAttributes.refiningYieldMultiplier
                            RefineValue = (Rig.Bonus * 100) * (Me.MaterialMultiplier / BaseRefineRate) ' Calculate new base refine amount (the structure modifier is multiplied to 50% base)
                    End Select
                Next
            Next
            rsLoader.Close()
        End If

        Select Case SentModifier
            Case ModifierType.MaterialModifier
                Return BaseValue * MM
            Case ModifierType.TimeModifier
                Return BaseValue * TM
            Case ModifierType.CostModifier
                Return BaseValue * CM
            Case ModifierType.ReprocessingRate
                Return RefineValue
            Case Else
                Return BaseValue
        End Select

    End Function

    ' Returns an array of rigs installed on the facility for info sent
    Private Function GetInstalledModules(ByVal Activity As String, ByVal FacilityID As Long, ByVal ItemGroupID As Integer,
                                         ByVal ItemCategoryID As Integer, ByVal SystemID As Long, ByVal SelectedCharacterID As Long,
                                         ByVal SelectedLocation As ProgramLocation) As List(Of Integer)
        Dim InstalledModules As New List(Of Integer)

        ' Save the current data - this will work for reactions since the groupID is the same for the item being made for lookup
        Dim TempBPGroupID As Integer = ItemGroupID
        Dim TempBPCategoryID As Integer = ItemCategoryID

        ' Check the activity and adjust the bp data if needed for components to query the bonuses they saved
        If Activity = ManufacturingFacility.ActivityComponentManufacturing Or Activity = ManufacturingFacility.ActivityCapComponentManufacturing Then
            Select Case ItemGroupID
                Case ItemIDs.TitanGroupID, ItemIDs.SupercarrierGroupID, ItemIDs.DreadnoughtGroupID, ItemIDs.CarrierGroupID,
                     ItemIDs.CapitalIndustrialShipGroupID, ItemIDs.IndustrialCommandShipGroupID, ItemIDs.FreighterGroupID, ItemIDs.JumpFreighterGroupID,
                     ItemIDs.AdvCapitalComponentGroupID, ItemIDs.CapitalComponentGroupID, ItemIDs.FAXGroupID
                    TempBPGroupID = ItemIDs.CapitalComponentGroupID
                    TempBPCategoryID = ItemIDs.ComponentCategoryID
                Case Else
                    TempBPGroupID = ItemIDs.ConstructionComponentsGroupID
                    TempBPCategoryID = ItemIDs.ComponentCategoryID
            End Select
        ElseIf Activity = ManufacturingFacility.ActivityCopying Or Activity = ManufacturingFacility.ActivityInvention Then
            TempBPCategoryID = ItemIDs.BlueprintCategoryID
            TempBPGroupID = ItemIDs.FrigateBlueprintGroupID
        ElseIf Activity = ManufacturingFacility.ActivityReprocessing Then
            Select Case ItemGroupID
                Case ItemIDs.IceGroupID ' Ice
                    TempBPGroupID = ItemGroupID
                Case ItemIDs.CommonMoonAsteroids, ItemIDs.ExceptionalMoonAsteroids, ItemIDs.RareMoonAsteroids, ItemIDs.UbiquitousMoonAsteroids, ItemIDs.UncommonMoonAsteroids ' Moon ores
                    TempBPGroupID = ItemIDs.CommonMoonAsteroids
                Case ItemIDs.Arkonor, ItemIDs.Bistot, ItemIDs.Crokite, ItemIDs.DarkOchre, ItemIDs.Gneiss, ItemIDs.Hedbergite, ItemIDs.Hemorphite, ItemIDs.Jaspet,
                     ItemIDs.Kernite, ItemIDs.Mercoxit, ItemIDs.Omber, ItemIDs.Plagioclase, ItemIDs.Pyroxeres, ItemIDs.Scordite, ItemIDs.Spodumain, ItemIDs.Veldspar
                    TempBPGroupID = ItemIDs.Arkonor ' this is the default for all ores
            End Select
            TempBPCategoryID = ItemIDs.AsteroidsCategoryID
        End If

        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        SQL = "SELECT INSTALLED_MODULE_ID FROM UPWELL_STRUCTURES_INSTALLED_MODULES, ENGINEERING_RIG_BONUSES "
        SQL &= "WHERE CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_VIEW = {3} AND FACILITY_ID = {4} "
        SQL &= "AND UPWELL_STRUCTURES_INSTALLED_MODULES.INSTALLED_MODULE_ID = ENGINEERING_RIG_BONUSES.typeID AND activityId = {7} "
        SQL &= "AND ((categoryID = {5} AND groupID IS NULL) OR (categoryID IS NULL AND groupID = {6}))"
        DBCommand = New SQLiteCommand(String.Format(SQL, SelectedCharacterID, CStr(FacilityProductionType), CStr(SystemID), CStr(SelectedLocation),
                                      CStr(FacilityID), CStr(TempBPCategoryID), CStr(TempBPGroupID), CStr(ActivityID)), EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read()
            InstalledModules.Add(rsLoader.GetInt32(0))
        End While
        rsLoader.Close()

        Return InstalledModules

    End Function

    ' Returns the attribute and bonus for the rig ID and system security sent by reference
    Private Function GetRigBonuses(ByVal RigID As Integer, ByVal SystemID As Long) As List(Of Attributebonus)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim TempAttribute As Attributebonus
        Dim SecModifier As ItemAttributes
        Dim AttributeData As New List(Of Attributebonus)

        ' Get the system security first
        Dim security As Double = GetSolarSystemSecurityLevel(SystemID)

        If Not IsNothing(security) Then
            If security <= 0.0 Then
                SecModifier = ItemAttributes.nullSecModifier
            ElseIf security < 0.45 Then
                SecModifier = ItemAttributes.lowSecModifier
            Else
                SecModifier = ItemAttributes.hiSecModifier
            End If
        Else
            ' Just assume null
            SecModifier = ItemAttributes.nullSecModifier
        End If

        ' Look up the bonus while adjusting for the type of space we are in
        SQL = "SELECT attributeID, ABS(value * (SELECT value FROM TYPE_ATTRIBUTES WHERE TYPEID = {0} AND attributeID = {1})/100) AS BONUS "
        SQL &= "FROM TYPE_ATTRIBUTES WHERE attributeID IN (2593,2594,2595,2713,2714,2653,717) "
        SQL &= "AND value <> 0 AND TYPEID = {0}"
        SQL = String.Format(SQL, RigID, CInt(SecModifier))
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsLoader = DBCommand.ExecuteReader

        While rsLoader.Read()
            TempAttribute.AttributeID = rsLoader.GetInt32(0)
            TempAttribute.Bonus = rsLoader.GetDouble(1)
            AttributeData.Add(TempAttribute)
        End While

        rsLoader.Close()

        Return AttributeData

    End Function

End Class
