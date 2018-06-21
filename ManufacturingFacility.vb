Imports System.Data.SQLite

Public Class ManufacturingFacility

    Private EVEDBRef As DBConnection
    Private EVEDBCommandRef As SQLiteCommand

    Private SelectedFacility As IndustryFacility ' This is the active facility for the control, if not loaded will use the default
    Private SelectedView As FacilityView
    Private SelectedLocation As ProgramLocation
    Private SelectedCharacterID As Long
    Private SelectedProductionType As ProductionType

    Private SelectedBPTech As Integer
    Private SelectedBPHasComponents As Boolean
    Private SelectedBPGroupID As Integer
    Private SelectedBPCategoryID As Integer

    ' To check if we are loading and stop click events when changing values
    Private LoadingActivities As Boolean
    Private LoadingFacilityTypes As Boolean
    Private LoadingRegions As Boolean
    Private LoadingSystems As Boolean
    Private LoadingFacilities As Boolean
    Private ChangingUsageChecks As Boolean

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
    Private FacilityorArrayLoaded As Boolean

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
    Private DefaultReactionsFacility As New IndustryFacility

    ' Constant activities
    Public Const ActivityManufacturing As String = "Manufacturing"
    Public Const ActivityComponentManufacturing As String = "Component Manufacturing"
    Public Const ActivityCapComponentManufacturing As String = "Cap Component Manufacturing"
    Public Const ActivityCopying As String = "Copying"
    Public Const ActivityInvention As String = "Invention"
    Public Const ActivityReactions As String = "Reactions"

    ' For verifying activity and facility type combos selected something
    Private Const InitialTypeComboText = "Type"
    Private Const InitialActivityComboText = "Select Activity"
    Private Const InitialRegionComboText = "Select Region"
    Private Const InitialSolarSystemComboText = "Select System"
    Private Const InitialFacilityComboText = "Select Facility"

    Public Const POSFacility As String = "POS"
    Public Const StationFacility As String = "Station"
    Public Const OutpostFacility As String = "Outpost"
    Public Const StructureFacility As String = "Structure"

    Private FactionCitadelList As New List(Of Integer)

    Private FacilityLabelDefaultColor As Color = SystemColors.Highlight
    Private FacilityLabelNonDefaultColor As Color = SystemColors.ButtonShadow

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
    Public Sub InitializeControl(ByVal ViewType As FacilityView, ByVal SentSelectedCharacterID As Long, FormLocation As ProgramLocation,
                                 ByVal InitialProductionType As ProductionType)
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
        SelectedView = ViewType
        SelectedLocation = FormLocation
        SelectedProductionType = InitialProductionType
        SelectedCharacterID = SentSelectedCharacterID

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

                cmbFacilityorArray.Top = cmbFacilityRegion.Top + cmbFacilityRegion.Height + 1
                cmbFacilityorArray.Left = LeftObjectLocation
                cmbFacilityorArray.Width = FacilityArrayWidthBP
                cmbFacilityorArray.Text = InitialFacilityComboText
                cmbFacilityorArray.Visible = True

                btnFacilitySave.Top = cmbFacilityorArray.Top + cmbFacilityorArray.Height
                btnFacilitySave.Left = (cmbFacilityorArray.Left + cmbFacilityorArray.Width) - btnFacilitySave.Width + 1
                btnFacilitySave.Visible = True
                btnFacilitySave.Enabled = False

                btnFacilityFitting.Top = btnFacilitySave.Top
                btnFacilityFitting.Left = btnFacilitySave.Left - (btnFacilityFitting.Width + 2)
                btnFacilityFitting.Visible = False
                btnFacilityFitting.Enabled = False

                ' Load all the manual lables and text
                lblFacilityManualME.Top = cmbFacilityorArray.Top + cmbFacilityorArray.Height + 4
                lblFacilityManualME.Left = LeftLabelLocation
                lblFacilityManualME.Text = "ME:"
                lblFacilityManualME.Visible = True

                txtFacilityManualME.Top = cmbFacilityorArray.Top + cmbFacilityorArray.Height + 1
                txtFacilityManualME.Left = lblFacilityManualME.Left + lblFacilityManualME.Width
                txtFacilityManualME.Visible = True

                lblFacilityManualCost.Top = lblFacilityManualME.Top + lblFacilityManualME.Height + 7
                lblFacilityManualCost.Left = LeftLabelLocation
                lblFacilityManualCost.Text = "Cost:"
                lblFacilityManualCost.Visible = True

                txtFacilityManualCost.Top = txtFacilityManualME.Top + txtFacilityManualME.Height + 1
                txtFacilityManualCost.Left = lblFacilityManualCost.Left + lblFacilityManualCost.Width
                txtFacilityManualCost.Visible = True

                ' Reset manual ME so it aligns with cost box
                txtFacilityManualME.Left = txtFacilityManualCost.Left

                lblFacilityManualTE.Top = lblFacilityManualME.Top
                lblFacilityManualTE.Left = txtFacilityManualME.Left + txtFacilityManualME.Width + 3
                lblFacilityManualTE.Text = "TE:"
                lblFacilityManualTE.Visible = True

                txtFacilityManualTE.Top = txtFacilityManualME.Top
                txtFacilityManualTE.Left = lblFacilityManualTE.Left + lblFacilityManualTE.Width
                txtFacilityManualTE.Visible = True

                lblFacilityManualTax.Top = lblFacilityManualCost.Top
                lblFacilityManualTax.Left = txtFacilityManualCost.Left + txtFacilityManualCost.Width + 3
                lblFacilityManualTax.Text = "Tax:"
                lblFacilityManualTax.Visible = True

                txtFacilityManualTax.Top = txtFacilityManualCost.Top
                txtFacilityManualTax.Left = lblFacilityManualTax.Left + lblFacilityManualTax.Width
                txtFacilityManualTax.Visible = True

                txtFacilityManualTE.Left = txtFacilityManualTax.Left

                cmbFacilityFWUpgrade.Top = btnFacilitySave.Top + btnFacilitySave.Height
                cmbFacilityFWUpgrade.Left = (cmbFacilityorArray.Left + cmbFacilityorArray.Width) - cmbFacilityFWUpgrade.Width
                cmbFacilityFWUpgrade.Visible = False

                lblFacilityFWUpgrade.Height = 30
                lblFacilityFWUpgrade.Width = 51
                lblFacilityFWUpgrade.Top = (cmbFacilityFWUpgrade.Top + cmbFacilityFWUpgrade.Height) - lblFacilityFWUpgrade.Height
                lblFacilityFWUpgrade.Left = cmbFacilityFWUpgrade.Left - lblFacilityFWUpgrade.Width
                lblFacilityFWUpgrade.Visible = False
                Call lblFacilityFWUpgrade.SendToBack()

                ' Set initial settings to load 
                SelectedBPCategoryID = ItemIDs.ShipCategoryID
                SelectedBPGroupID = ItemIDs.FrigateGroupID
                SelectedBPTech = BPTechLevel.T1

                ' Load all the facilities for full controls tab 
                Call InitializeFacilities(FacilityView.FullControls)

            Case FacilityView.LimitedControls

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

                cmbFacilityorArray.Top = cmbFacilityRegion.Top + cmbFacilityRegion.Height + 1
                cmbFacilityorArray.Left = LeftObjectLocation
                cmbFacilityorArray.Width = FacilityArrayWidthCalc
                cmbFacilityorArray.Text = InitialFacilityComboText
                cmbFacilityorArray.Visible = True

                lblFacilityDefault.Visible = True
                lblFacilityDefault.Top = chkFacilityToggle.Top
                lblFacilityDefault.Left = (cmbFacilityorArray.Left + cmbFacilityorArray.Width) - lblFacilityDefault.Width
                Call lblFacilityDefault.SendToBack()

                btnFacilitySave.Top = cmbFacilityorArray.Top + cmbFacilityorArray.Height + 2
                btnFacilitySave.Left = (cmbFacilityorArray.Left + cmbFacilityorArray.Width) - btnFacilitySave.Width + 1
                btnFacilitySave.Visible = True
                btnFacilitySave.Enabled = False

                btnFacilityFitting.Top = btnFacilitySave.Top
                btnFacilityFitting.Left = btnFacilitySave.Left - (btnFacilityFitting.Width + 2)
                btnFacilityFitting.Visible = False
                btnFacilityFitting.Enabled = False

                ' Manual text boxes and labels
                lblFacilityManualME.Top = btnFacilitySave.Top + 6
                lblFacilityManualME.Left = LeftLabelLocation
                lblFacilityManualME.Text = "ME:"
                lblFacilityManualME.Visible = False

                txtFacilityManualME.Top = btnFacilitySave.Top + 2
                txtFacilityManualME.Left = lblFacilityManualME.Left + lblFacilityManualME.Width
                txtFacilityManualME.Visible = False

                lblFacilityManualCost.Top = lblFacilityManualME.Top + lblFacilityManualME.Height + 7
                lblFacilityManualCost.Left = LeftLabelLocation
                lblFacilityManualCost.Text = "Cost:"
                lblFacilityManualCost.Visible = False

                txtFacilityManualCost.Top = txtFacilityManualME.Top + txtFacilityManualME.Height + 1
                txtFacilityManualCost.Left = lblFacilityManualCost.Left + lblFacilityManualCost.Width
                txtFacilityManualCost.Visible = False

                ' Reset manual ME so it aligns with cost box
                txtFacilityManualME.Left = txtFacilityManualCost.Left

                lblFacilityManualTE.Top = lblFacilityManualME.Top
                lblFacilityManualTE.Left = txtFacilityManualME.Left + txtFacilityManualME.Width + 3
                lblFacilityManualTE.Text = "TE:"
                lblFacilityManualTE.Visible = False

                txtFacilityManualTE.Top = txtFacilityManualME.Top
                txtFacilityManualTE.Left = lblFacilityManualTE.Left + lblFacilityManualTE.Width
                txtFacilityManualTE.Visible = False

                lblFacilityManualTax.Top = lblFacilityManualCost.Top
                lblFacilityManualTax.Left = txtFacilityManualCost.Left + txtFacilityManualCost.Width + 3
                lblFacilityManualTax.Text = "Tax:"
                lblFacilityManualTax.Visible = False

                txtFacilityManualTax.Top = txtFacilityManualCost.Top
                txtFacilityManualTax.Left = lblFacilityManualTax.Left + lblFacilityManualTax.Width
                txtFacilityManualTax.Visible = False

                txtFacilityManualTE.Left = txtFacilityManualTax.Left

                ' Visible will be set later
                cmbFacilityFWUpgrade.Top = btnFacilitySave.Top + btnFacilitySave.Height + 4
                cmbFacilityFWUpgrade.Left = (cmbFacilityorArray.Left + cmbFacilityorArray.Width) - cmbFacilityFWUpgrade.Width
                cmbFacilityFWUpgrade.Visible = False

                lblFacilityFWUpgrade.Height = 13
                lblFacilityFWUpgrade.Width = 71
                lblFacilityFWUpgrade.Top = cmbFacilityFWUpgrade.Top + 4
                lblFacilityFWUpgrade.Left = cmbFacilityFWUpgrade.Left - lblFacilityFWUpgrade.Width
                lblFacilityFWUpgrade.Visible = False
                Call lblFacilityFWUpgrade.SendToBack()

                ' Position these but they will be shown later
                lblModules.Top = cmbFacilityorArray.Top
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
                Select Case InitialProductionType
                    Case ProductionType.Manufacturing
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.FrigateGroupID
                        SelectedBPTech = BPTechLevel.T2
                        cmbFacilityActivities.Text = ActivityManufacturing
                    Case ProductionType.CapitalComponentManufacturing
                        SelectedBPCategoryID = ItemIDs.CapitalComponentGroupID
                        SelectedBPGroupID = ItemIDs.None
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityCapComponentManufacturing
                    Case ProductionType.ComponentManufacturing
                        SelectedBPCategoryID = ItemIDs.ComponentCategoryID
                        SelectedBPGroupID = ItemIDs.None
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityComponentManufacturing
                    Case ProductionType.CapitalManufacturing
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.CarrierGroupID
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityManufacturing
                    Case ProductionType.SuperManufacturing
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.SupercarrierGroupID
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityManufacturing
                    Case ProductionType.T3CruiserManufacturing
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.StrategicCruiserGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                    Case ProductionType.SubsystemManufacturing
                        SelectedBPCategoryID = ItemIDs.SubsystemCategoryID
                        SelectedBPGroupID = ItemIDs.None
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                    Case ProductionType.BoosterManufacturing
                        SelectedBPCategoryID = ItemIDs.None
                        SelectedBPGroupID = ItemIDs.BoosterGroupID
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityManufacturing
                    Case ProductionType.Copying
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.FrigateGroupID
                        SelectedBPTech = BPTechLevel.T2
                        cmbFacilityActivities.Text = ActivityCopying
                    Case ProductionType.Invention
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.FrigateGroupID
                        SelectedBPTech = BPTechLevel.T2
                        cmbFacilityActivities.Text = ActivityInvention
                    Case ProductionType.Reactions
                        SelectedBPCategoryID = ItemIDs.None
                        SelectedBPGroupID = ItemIDs.ReactionPolymersGroupID
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityReactions
                    Case ProductionType.T3Invention
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.TacticalDestroyerGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityInvention
                    Case ProductionType.T3DestroyerManufacturing
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.TacticalDestroyerGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                End Select

                ' Load the defaults
                Call InitializeFacilities(FacilityView.LimitedControls, InitialProductionType)

            Case Else
                ' Leave, no valid option sent
                Exit Sub
        End Select

    End Sub

    ' Loads all the facilities for the view type sent to include defaults
    Public Sub InitializeFacilities(ViewType As FacilityView, Optional InitialProductionType As ProductionType = ProductionType.Manufacturing)

        Select Case ViewType
            Case FacilityView.FullControls
                ' Load all the facilities for  tab - always start with manufacturing
                Call SelectedFacility.InitalizeFacility(ProductionType.Manufacturing, ViewType)
                SelectedManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.ComponentManufacturing, ViewType)
                SelectedComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.CapitalComponentManufacturing, ViewType)
                SelectedCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.CapitalManufacturing, ViewType)
                SelectedCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.SuperManufacturing, ViewType)
                SelectedSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.T3CruiserManufacturing, ViewType)
                SelectedT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.SubsystemManufacturing, ViewType)
                SelectedSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultSubsystemManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.BoosterManufacturing, ViewType)
                SelectedBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultBoosterManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.Copying, ViewType)
                SelectedCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultCopyFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.Invention, ViewType)
                SelectedInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultInventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.Reactions, ViewType)
                SelectedReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultReactionsFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.T3Invention, ViewType)
                SelectedT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultT3InventionFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Call SelectedFacility.InitalizeFacility(ProductionType.T3DestroyerManufacturing, ViewType)
                SelectedT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                DefaultT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)

            Case FacilityView.LimitedControls

                ' Select what facility to load based on the industry type
                Call SelectedFacility.InitalizeFacility(InitialProductionType, ViewType)

                'Now save the default and selected facility to the appropriate variable
                Select Case InitialProductionType
                    Case ProductionType.Manufacturing
                        SelectedManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.CapitalComponentManufacturing
                        SelectedCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        ' Load component too so we can click back and forth
                        Call SelectedFacility.InitalizeFacility(ProductionType.ComponentManufacturing, ViewType)
                        SelectedComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.ComponentManufacturing
                        SelectedComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        ' Load cap component too so we can click back and forth
                        Call SelectedFacility.InitalizeFacility(ProductionType.CapitalComponentManufacturing, ViewType)
                        SelectedCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultCapitalComponentManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.CapitalManufacturing
                        SelectedCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultCapitalManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.SuperManufacturing
                        SelectedSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultSuperManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                    Case ProductionType.T3CruiserManufacturing
                        SelectedT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        ' Load T3 destroyers too so we can click back and forth
                        Call SelectedFacility.InitalizeFacility(ProductionType.T3DestroyerManufacturing, ViewType)
                        SelectedT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultT3DestroyerManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
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
                        ' Load T3 cruisers too so we can click back and forth
                        Call SelectedFacility.InitalizeFacility(ProductionType.T3CruiserManufacturing, ViewType)
                        SelectedT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                        DefaultT3CruiserManufacturingFacility = CType(SelectedFacility.Clone, IndustryFacility)
                End Select

            Case Else
                ' Leave, no valid option sent
                Exit Sub
        End Select

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
        FacilityorArrayLoaded = False

        LoadingActivities = False
        LoadingFacilityTypes = False
        LoadingRegions = False
        LoadingSystems = False
        LoadingFacilities = False
        ChangingUsageChecks = False

        ' Load the selected facility with set bp
        Call LoadFacility(SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, True)

    End Sub

    ' Loads the class facility and objects
    Public Sub LoadFacility(ByVal ItemGroupID As Integer, ByVal ItemCategoryID As Integer,
                            ByVal BlueprintTech As Integer, ByVal BPHasComponents As Boolean,
                            Optional ByVal LoadDefault As Boolean = False, Optional DefaultFacility As IndustryFacility = Nothing,
                            Optional ByVal ComboSelect As Boolean = False)

        ' Save these for later use
        SelectedBPCategoryID = ItemCategoryID
        SelectedBPGroupID = ItemGroupID
        SelectedBPTech = BlueprintTech
        SelectedBPHasComponents = BPHasComponents

        ' Process the activities combo if showing full controls
        If SelectedView = FacilityView.FullControls Then
            Call LoadFacilityActivities(ItemGroupID, ItemCategoryID, BlueprintTech, BPHasComponents, ComboSelect)
            PreviousActivity = cmbFacilityActivities.Text
        End If

        ' Get the production type, based on activity selected
        SelectedProductionType = GetProductionType(ItemGroupID, ItemCategoryID, cmbFacilityActivities.Text)
        Application.DoEvents()

        ' If we get a facility sent, load that, else load it up from the other information
        If Not IsNothing(DefaultFacility) Then
            SelectedFacility = DefaultFacility
        Else
            ' Look up Facility - activity set to facility inside
            SelectedFacility = SelectFacility(SelectedProductionType, LoadDefault)
        End If

        ' Facility Type combo, load it if they want to change
        Call LoadFacilityTypes(SelectedProductionType, SelectedFacility.Activity)

        ' Enable the type of facility and set
        LoadingFacilityTypes = True
        cmbFacilityType.Enabled = True
        cmbFacilityType.Text = GetFacilityNameCode(SelectedFacility.FacilityType)
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
        cmbFacilityorArray.Enabled = True
        Dim AutoLoad As Boolean = False
        Call LoadFacilities(False, SelectedFacility.Activity, AutoLoad, SelectedFacility.FacilityName)
        LoadingFacilities = False

        ' Usage checks
        ChangingUsageChecks = True

        ' Usage always visible
        chkFacilityIncludeUsage.Checked = SelectedFacility.IncludeActivityUsage

        If Not IsNothing(chkFacilityIncludeCost) Then
            chkFacilityIncludeCost.Checked = SelectedFacility.IncludeActivityCost
        End If

        If Not IsNothing(chkFacilityIncludeTime) Then
            chkFacilityIncludeTime.Checked = SelectedFacility.IncludeActivityTime
        End If

        ChangingUsageChecks = False

        ' Finally show the results and save the facility locally
        If Not AutoLoad Then
            LoadingFacilities = True
            With SelectedFacility
                cmbFacilityorArray.Text = .FacilityName
                Call DisplayFacilityBonus(.FacilityProductionType, ItemGroupID, ItemCategoryID, SelectedFacility.Activity,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)
            End With
            LoadingFacilities = False
        End If

        Call ResetComboLoadVariables(False, False, False)

        ' All data loaded
        SelectedFacility.FullyLoaded = True

        ' Facility is loaded, so save it to default and dynamic variable
        Call SetFacility(SelectedFacility, SelectedProductionType, False, False)

    End Sub

    ' Loads the facility activity combo - checks group and category ID's if it has components to set component activities
    Public Sub LoadFacilityActivities(BPGroupID As Long, BPCategoryID As Long, BlueprintTech As Integer, HasComponents As Boolean, FromComboSelect As Boolean)

        LoadingActivities = True
        Dim ActivityText As String = cmbFacilityActivities.Text ' Save what is selected first
        cmbFacilityActivities.BeginUpdate()

        ' If it's a reaction, only load that activity and manufacturing for fuel blocks
        If BPGroupID = ItemIDs.ReactionBiochmeicalsGroupID Or BPGroupID = ItemIDs.ReactionCompositesGroupID Or BPGroupID = ItemIDs.ReactionPolymersGroupID Or BPGroupID = ItemIDs.ReactionsIntermediateGroupID Then
            cmbFacilityActivities.Items.Clear()
            cmbFacilityActivities.Items.Add(ActivityReactions)
            cmbFacilityActivities.Items.Add(ActivityManufacturing)
            If FromComboSelect Then
                ' use what was there
                cmbFacilityActivities.Text = ActivityText
            Else
                ' Start with reactions for a new facility because its a call to load not from combo
                cmbFacilityActivities.Text = ActivityReactions
            End If

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
                    ' Just regular
                    cmbFacilityActivities.Items.Add(ActivityComponentManufacturing)
            End Select
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

                ' Load the facility for this activity - flag that it was loaded from this combo
                Call LoadFacility(SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, SelectedBPHasComponents, True, Nothing, True)

                ' Reset all previous to current list, since all the combos should be loaded
                PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
                PreviousEquipment = cmbFacilityorArray.Text
                PreviousRegion = cmbFacilityRegion.Text
                PreviousSystem = cmbFacilitySystem.Text
            End If

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
        Dim Station As String = GetFacilityNameCode(FacilityTypes.Station)
        Dim Outpost As String = GetFacilityNameCode(FacilityTypes.Outpost)
        Dim POS As String = GetFacilityNameCode(FacilityTypes.POS)
        Dim UpwellStructure As String = GetFacilityNameCode(FacilityTypes.UpwellStructure)
        Dim NoneFacility As String = GetFacilityNameCode(FacilityTypes.None)

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
            Case ActivityManufacturing
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
                    Case Else
                        ' Add all
                        If Station <> "" Then cmbFacilityType.Items.Add(Station)
                        If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                        If POS <> "" Then cmbFacilityType.Items.Add(POS)
                        If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
                End Select
            Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                ' Can do these anywhere
                If Station <> "" Then cmbFacilityType.Items.Add(Station)
                If Outpost <> "" Then cmbFacilityType.Items.Add(Outpost)
                If POS <> "" Then cmbFacilityType.Items.Add(POS)
                If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
            Case ActivityReactions
                ' Only in upwells
                If UpwellStructure <> "" Then cmbFacilityType.Items.Add(UpwellStructure)
        End Select

        ' Only reset if they changed it
        If FacilityProductionType <> PreviousProductionType Or FacilityActivity <> PreviousActivity Then
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
            PreviousProductionType = FacilityProductionType
            PreviousActivity = FacilityActivity

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
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)
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
    End Sub
    Private Sub cmbFacilityType_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityType.DropDown
        PreviousFacilityType = GetFacilityTypeCode(cmbFacilityType.Text)
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
    Public Sub LoadFacilityRegions(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean, ByRef FacilityActivity As String)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        LoadingRegions = True
        LoadingSystems = True
        LoadingFacilities = True

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
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, -1, IndustryActivities.Manufacturing)
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

                ' Make sure the region listed has at least one system not in the disallowed anchoring lists
                ' Upwell Structures can be anchored almost anywhere except starter systems, trade hubs, and shattered wormholes (including Thera)
                ' Check both disallowable anchor tables
                SQL = SQL & "AND (solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_CATEGORIES WHERE CATEGORY_ID = 65) AND "
                SQL = SQL & "solarSystemID NOT IN (SELECT SOLAR_SYSTEM_ID FROM MAP_DISALLOWED_ANCHOR_GROUPS WHERE GROUP_ID = 65)) "

                ' For supers, only show null regions where you can have sov (no factionID excludes NPC null, etc)
                If ItemGroupID = ItemIDs.SupercarrierGroupID Or ItemGroupID = ItemIDs.TitanGroupID Then
                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = ItemIDs.DreadnoughtGroupID Or ItemGroupID = ItemIDs.CarrierGroupID Or ItemGroupID = ItemIDs.CapitalIndustrialShipGroupID Or ItemGroupID = ItemIDs.FAXGroupID Or
                    ItemGroupID = ItemIDs.ReactionBiochmeicalsGroupID Or ItemGroupID = ItemIDs.ReactionCompositesGroupID Or ItemGroupID = ItemIDs.ReactionPolymersGroupID Or ItemGroupID = ItemIDs.ReactionsIntermediateGroupID Then
                    ' For caps and reactions, only show low sec
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
            Call SetDefaultVisuals(False)
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

        LoadingRegions = False
        LoadingSystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(True, False, False)

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub
    Private Sub cmbFacilityRegion_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityRegion.SelectedIndexChanged
        If Not LoadingRegions And Not FirstLoad And PreviousRegion <> cmbFacilityRegion.Text Then
            Call LoadFacilitySystems(SelectedBPGroupID, SelectedBPCategoryID, True, cmbFacilityActivities.Text)
            Call cmbFacilitySystem.Focus()
            Call SetFacilityBonusBoxes(False)
            SelectedFacility.FullyLoaded = False
            PreviousRegion = cmbFacilityRegion.Text
        End If
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
    Private Sub cmbFacilityRegion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFacilityRegion.KeyPress
        e.Handled = True
    End Sub
    Private Sub cmbFacilityRegion_GotFocus(sender As Object, e As EventArgs) Handles cmbFacilityRegion.GotFocus
        Call cmbFacilityRegion.SelectAll()
    End Sub
    Private Sub cmbFacilityRegion_LostFocus(sender As Object, e As EventArgs) Handles cmbFacilityRegion.LostFocus
        cmbFacilitySystem.SelectionLength = 0

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

    ' Based on the selections, load the systems combo
    Public Sub LoadFacilitySystems(ItemGroupID As Integer, ItemCategoryID As Integer, NewFacility As Boolean, ByRef FacilityActivity As String)

        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        LoadingSystems = True
        LoadingFacilities = True

        cmbFacilitySystem.Items.Clear()

        Select Case cmbFacilityType.Text

            Case OutpostFacility, StationFacility

                SQL = "SELECT DISTINCT INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_NAME AS SSN, INDUSTRY_SYSTEMS_COST_INDICIES.COST_INDEX AS CI "
                SQL &= "FROM STATION_FACILITIES, INDUSTRY_SYSTEMS_COST_INDICIES "
                SQL &= "WHERE STATION_FACILITIES.SOLAR_SYSTEM_ID = INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID "
                SQL &= "AND STATION_FACILITIES.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "

                ' Set flag for outpost just to delineate
                If cmbFacilityType.Text = StationFacility Then
                    SQL = SQL & "AND OUTPOST  = " & CStr(StationType.Station) & " "
                Else
                    SQL = SQL & "AND OUTPOST  = " & CStr(StationType.Outpost) & " "
                End If

                SQL &= "AND STATION_FACILITIES.ACTIVITY_ID = "

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                End Select

                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(cmbFacilityRegion.Text) & "'"

            Case POSFacility
                ' For a POS, load all systems, if wormhole 'region' selected, then load jspace systems
                SQL = "SELECT DISTINCT solarSystemName AS SSN, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END AS CI  "
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
                If ItemGroupID = ItemIDs.SupercarrierGroupID Or ItemGroupID = ItemIDs.TitanGroupID Then
                    SQL = SQL & " AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = ItemIDs.DreadnoughtGroupID Or ItemGroupID = ItemIDs.CarrierGroupID Or ItemGroupID = ItemIDs.CapitalIndustrialShipGroupID Or ItemGroupID = ItemIDs.FAXGroupID Then
                    ' For caps, only show low sec
                    SQL = SQL & " AND security < .45 "
                End If

            Case StructureFacility
                SQL = "SELECT DISTINCT solarSystemName AS SSN, CASE WHEN COST_INDEX IS NOT NULL THEN COST_INDEX ELSE 0 END  AS CI "
                SQL = SQL & "FROM REGIONS, SOLAR_SYSTEMS "
                SQL = SQL & "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES ON solarSystemID = SOLAR_SYSTEM_ID "

                Select Case FacilityActivity
                    Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                    Case ActivityReactions
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reactions) & " "
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
                If ItemGroupID = ItemIDs.SupercarrierGroupID Or ItemGroupID = ItemIDs.TitanGroupID Then
                    SQL = SQL & "AND security <= 0.0 AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                ElseIf ItemGroupID = ItemIDs.DreadnoughtGroupID Or ItemGroupID = ItemIDs.CarrierGroupID Or ItemGroupID = ItemIDs.CapitalIndustrialShipGroupID Or ItemGroupID = ItemIDs.FAXGroupID Or
                    ItemGroupID = ItemIDs.ReactionBiochmeicalsGroupID Or ItemGroupID = ItemIDs.ReactionCompositesGroupID Or ItemGroupID = ItemIDs.ReactionPolymersGroupID Or ItemGroupID = ItemIDs.ReactionsIntermediateGroupID Then
                    ' For caps and reactions, only show low sec
                    SQL = SQL & "AND security < .45 "
                End If

        End Select

        SQL = SQL & " GROUP BY SSN, CI"

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
            Call SetDefaultVisuals(False)
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

        LoadingSystems = False
        LoadingFacilities = False

        Call ResetComboLoadVariables(False, True, False)

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub
    Private Sub cmbFacilitySystem_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilitySystem.SelectedIndexChanged
        Dim OverrideFacilityName As String = ""
        Dim Autoload As Boolean = False

        cmbFacilitySystem.SelectionLength = 0

        If Not LoadingSystems And Not FirstLoad And PreviousSystem <> cmbFacilitySystem.Text Then

            Call SetFacilityBonusBoxes(False)

            If cmbFacilityType.Text = OutpostFacility Then
                OverrideFacilityName = ""
                Autoload = True
            ElseIf cmbFacilityType.Text = POSFacility Then
                OverrideFacilityName = GetPOSManufacturingFacilityName(SelectedFacility)
            End If

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

            Call cmbFacilityorArray.Focus()

            PreviousSystem = cmbFacilitySystem.Text
        End If

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
    End Sub
    Private Sub cmbFacilitySystem_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilitySystem.KeyPress
        e.Handled = True
    End Sub

    ' Based on the selections, load the facilities/arrays combo - an itemcategory or itemgroup id of -1 means to ignore it when filling arrays
    Private Sub LoadFacilities(NewFacility As Boolean, ByRef FacilityActivity As String,
                                 ByRef AutoLoadFacility As Boolean, Optional OverrideFacilityName As String = "")
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader

        LoadingFacilities = True

        Dim SystemName As String
        If cmbFacilitySystem.Text.Contains("(") Then
            SystemName = cmbFacilitySystem.Text.Substring(0, InStr(cmbFacilitySystem.Text, "(") - 2)
        Else
            SystemName = cmbFacilitySystem.Text
        End If

        Select Case GetFacilityTypeCode(cmbFacilityType.Text)

            Case FacilityTypes.Station, FacilityTypes.Outpost
                ' Load the Stations in system for the activity we are doing
                SQL = "SELECT DISTINCT FACILITY_NAME, FACILITY_ID FROM STATION_FACILITIES WHERE OUTPOST "

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
                        SQL = SQL & GetFacilityCatGroupIDSQL(SelectedBPCategoryID, SelectedBPGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for components - All types can be built in stations
                        SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, -1, IndustryActivities.Manufacturing)
                    Case ActivityCopying
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(SelectedBPCategoryID, SelectedBPGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                        ' For T3 stuff, need to make sure we only show facilities that can do T3 invention (Caldari Outposts)
                        SQL = SQL & GetFacilityCatGroupIDSQL(SelectedBPCategoryID, SelectedBPGroupID, IndustryActivities.Invention)
                End Select

                SQL = SQL & "AND REGION_NAME = '" & FormatDBString(cmbFacilityRegion.Text) & "' "
                SQL = SQL & "AND SOLAR_SYSTEM_NAME = '" & FormatDBString(SystemName) & "' "

            Case FacilityTypes.POS

                ' Load all the array types up into the combo for a POS
                SQL = "SELECT DISTINCT ARRAY_NAME AS FACILITY_NAME, ARRAY_TYPE_ID FROM ASSEMBLY_ARRAYS "
                SQL = SQL & "WHERE ACTIVITY_ID = "

                Select Case FacilityActivity
                    Case ActivityManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Check groups and categories
                        SQL = SQL & GetFacilityCatGroupIDSQL(SelectedBPCategoryID, SelectedBPGroupID, IndustryActivities.Manufacturing)
                    Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                        SQL = SQL & CStr(IndustryActivities.Manufacturing) & " "
                        ' Add category for component
                        Select Case SelectedBPGroupID
                            Case ItemIDs.TitanGroupID, ItemIDs.SupercarrierGroupID, ItemIDs.DreadnoughtGroupID, ItemIDs.CarrierGroupID,
                                ItemIDs.CapitalIndustrialShipGroupID, ItemIDs.IndustrialCommandShipGroupID, ItemIDs.FreighterGroupID, ItemIDs.JumpFreighterGroupID,
                                ItemIDs.AdvCapitalComponentGroupID, ItemIDs.CapitalComponentGroupID, ItemIDs.FAXGroupID
                                SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, ItemIDs.CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
                            Case Else
                                SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, ItemIDs.ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
                        End Select
                    Case ActivityCopying
                        SQL = SQL & CStr(IndustryActivities.Copying) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(SelectedBPCategoryID, SelectedBPGroupID, IndustryActivities.Copying)
                    Case ActivityInvention
                        ' POS invention you can only do T3 in certain arrays
                        SQL = SQL & CStr(IndustryActivities.Invention) & " "
                        SQL = SQL & GetFacilityCatGroupIDSQL(SelectedBPCategoryID, SelectedBPGroupID, IndustryActivities.Invention)
                End Select

            Case FacilityTypes.UpwellStructure
                ' Load all the upwell structures based on the production type
                SQL = "SELECT typeName as FACILITY_NAME, typeID FROM INVENTORY_TYPES, INVENTORY_GROUPS "
                SQL &= "WHERE INVENTORY_GROUPS.categoryID = 65 "
                SQL &= "AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupid "
                SQL &= "AND INVENTORY_TYPES.published = 1 "
                SQL &= "AND (typeID IN (SELECT COALESCE(valuefloat, valueint) AS UPWELL_STRUCTURE_ID "
                SQL &= "FROM TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
                SQL &= "WHERE ATTRIBUTE_TYPES.attributeID = TYPE_ATTRIBUTES.attributeID "
                SQL &= "AND attributeName Like 'canFitShipType%' "
                SQL &= "AND TYPE_ATTRIBUTES.typeID = {0}) "
                SQL &= "OR INVENTORY_TYPES.groupID In (Select COALESCE(valuefloat, valueint) As UPWELL_STRUCTURE_ID "
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
                    Case Else ' All others get manufacturing
                        SQL = String.Format(SQL, CInt(Services.StandupManufacturingPlant))
                End Select

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
                Dim security As Double = GetSolarSystemSecurityLevel(SystemName)

                If security <> -1 Then
                    If security < 0.45 Then
                        ' Thukker is only low sec - no easy way to weed this out
                        cmbFacilityorArray.Items.Add(rsLoader.GetString(0))
                    End If
                Else
                    ' Allow it
                    cmbFacilityorArray.Items.Add(rsLoader.GetString(0))
                End If
            ElseIf factioncitadellist.Contains(rsLoader.GetInt32(1)) Then
                ' These are only in nullsec space (if we can look up in ESI then later maybe)
                SQL = "SELECT security, factionID FROM REGIONS, SOLAR_SYSTEMS WHERE REGIONS.regionID = SOLAR_SYSTEMS.regionID "
                SQL &= "AND factionID IS NULL AND regionName <> 'Wormhole Space' "
                SQL &= "AND security <= 0.0 AND SOLAR_SYSTEMS.solarSystemName = '" & SystemName & "'"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    ' no sov and it's nullsec, so add it
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
            ElseIf cmbFacilityorArray.Items.Contains(cmbFacilityorArray.Text) Then
                ' Leave it as is
                Application.DoEvents()
            Else
                cmbFacilityorArray.Text = AutoLoadName
            End If

            AutoLoadFacility = True
            ' Display bonuses - Need to load everything since the array won't change to cause it to reload
            Call DisplayFacilityBonus(SelectedProductionType, SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text,
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
                    Case FacilityTypes.UpwellStructure
                        cmbFacilityorArray.Text = "Select Upwell Structure"
                End Select

                ' Make sure default is turned off since we still have to load the array
                btnFacilitySave.Enabled = False
                Call SetDefaultVisuals(False)
                chkFacilityIncludeUsage.Enabled = False ' Don't enable the usage either
            Else
                ' Since this is a different system but facility is loaded, enable save
                btnFacilitySave.Enabled = True
                Call SetDefaultVisuals(False)
                chkFacilityIncludeUsage.Enabled = True
            End If

            AutoLoadFacility = False

        End If

        If NewFacility Then
            ' Make sure default is not checked yet
            Call SetDefaultVisuals(False)
            btnFacilitySave.Enabled = False
            Call SetFacilityBonusBoxes(True)
        End If

        ' Users might select the facility drop down first, so reload all others
        Call ResetComboLoadVariables(False, False, True)

        LoadingFacilities = False

        rsLoader.Close()
        rsLoader = Nothing
        DBCommand = Nothing

    End Sub
    Private Sub cmbFacilityorArray_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityorArray.SelectedIndexChanged

        If Not LoadingFacilities And Not FirstLoad And PreviousEquipment <> cmbFacilityorArray.Text Then
            Call DisplayFacilityBonus(SelectedProductionType, SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)

            PreviousEquipment = cmbFacilityorArray.Text
            Call UpdateBlueprint()
        End If
    End Sub
    Private Sub cmbFacilityorArray_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityorArray.DropDown
        ' If you drop down, don't show the text window
        cmbFacilityorArray.AutoCompleteMode = AutoCompleteMode.None

        If Not FacilityorArrayLoaded And Not FirstLoad Then
            PreviousEquipment = cmbFacilityorArray.Text
            Call LoadFacilities(False, cmbFacilityActivities.Text, False, "")
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
            LoadingActivities = True ' Don't trigger a combo load yet
            Dim SelectedFacility As New IndustryFacility
            Dim SelectedActivity As String = ""

            SelectedFacility = SelectFacility(SelectedProductionType, True)

            If Not IsNothing(SelectedBlueprint) Then
                With SelectedBlueprint
                    Call LoadFacility(.GetItemGroupID, .GetItemCategoryID, .GetTechLevel, .HasComponents, True)
                End With
            End If
            ' Set the default based on the checkbox 
            Call SetFacility(SelectedFacility, SelectedProductionType, False, False)

            LoadingActivities = False
        End If
    End Sub

    ' Displays the bonus for the facility selected in the facility or array combo
    Private Sub DisplayFacilityBonus(BuildType As ProductionType, ItemGroupID As Integer, ItemCategoryID As Integer, Activity As String,
                                    FacilityType As FacilityTypes, FacilityName As String)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim rsStats As SQLiteDataReader

        Dim FacilityID As Long
        Dim FacilityTypeID As Integer
        Dim MaterialMultiplier As Double
        Dim TimeMultiplier As Double
        Dim CostMultiplier As Double
        Dim Tax As Double

        Dim TempDefaultFacility As New IndustryFacility

        ' Process system if needed
        Dim SystemName As String
        If cmbFacilitySystem.Text.Contains("(") Then
            SystemName = cmbFacilitySystem.Text.Substring(0, InStr(cmbFacilitySystem.Text, "(") - 2)
        Else
            SystemName = cmbFacilitySystem.Text
        End If

        If FacilityType <> FacilityTypes.None Then

            ' First, see if this facility is a saved facility, and use the values from the table
            SQL = "Select FACILITY_ID, FACILITY_TYPE_ID, FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
            SQL &= "FROM SAVED_FACILITIES, REGIONS, SOLAR_SYSTEMS, INVENTORY_TYPES "
            SQL &= "WHERE SAVED_FACILITIES.REGION_ID = REGIONS.regionID "
            SQL &= "And INVENTORY_TYPES.typeID = FACILITY_TYPE_ID "
            SQL &= "And SAVED_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
            SQL &= String.Format("And CHARACTER_ID = {0} ", CStr(SelectedCharacter.ID))
            SQL &= String.Format("And PRODUCTION_TYPE = {0} And FACILITY_VIEW = {1} ", CStr(BuildType), CStr(SelectedView))
            SQL &= "And REGIONS.regionName = '" & FormatDBString(cmbFacilityRegion.Text) & "' "
            SQL &= "AND SOLAR_SYSTEMS.solarSystemName = '" & SystemName & "' "
            SQL &= "AND typeName = '" & FormatDBString(cmbFacilityorArray.Text) & "' "

            Dim SQLCharID As String = "AND CHARACTER_ID = {0}"
            Dim UsedCharID As String = ""

            ' Save for use later if needed
            UsedCharID = CStr(SelectedCharacter.ID)

            ' First look up the character to see if it's saved there first (initially only do one set of facilities then allow by character via a setting)
            DBCommand = New SQLiteCommand(SQL & String.Format(SQLCharID, CStr(SelectedCharacter.ID)), EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            rsLoader.Read()

            If Not rsLoader.HasRows Then
                ' Need to look up the default
                rsLoader.Close()
                DBCommand = New SQLiteCommand(SQL & String.Format(SQLCharID, "0"), EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()
                UsedCharID = "0"
            End If

            If Not rsLoader.HasRows Then
                rsLoader.Close()

                ' Not in there for either character or default, so use the defaults from lookup
                Select Case FacilityType

                    Case FacilityTypes.Outpost, FacilityTypes.Station

                        ' Load the Stations in system for the activity we are doing
                        SQL = "SELECT DISTINCT FACILITY_ID, FACILITY_TYPE_ID, FACILITY_TAX, NULL, NULL, NULL "
                        SQL = SQL & "FROM STATION_FACILITIES WHERE OUTPOST  "

                        ' Set flag for outpost just to delineate
                        If FacilityType = FacilityTypes.Station Then
                            SQL = SQL & " = " & CStr(StationType.Station) & " "
                        Else
                            SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                        End If
                        SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString(FacilityName) & "' "

                    Case FacilityTypes.POS

                        SQL = "SELECT ARRAY_TYPE_ID AS FACILITY_ID, ARRAY_TYPE_ID, " & CStr(POSTaxRate) & " as TAX, NULL, NULL, NULL "
                        SQL = SQL & "FROM ASSEMBLY_ARRAYS WHERE ARRAY_NAME = '" & FormatDBString(FacilityName) & "' "

                    Case FacilityTypes.UpwellStructure

                        SQL = "SELECT UPWELL_STRUCTURE_TYPE_ID AS FACILITY_ID, UPWELL_STRUCTURE_TYPE_ID, " & CStr(POSTaxRate) & " as TAX, NULL, NULL, NULL "
                        SQL &= "FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_NAME = '" & FormatDBString(FacilityName) & "' "

                End Select

                If FacilityType <> FacilityTypes.UpwellStructure Then
                    Select Case Activity
                        Case ActivityManufacturing
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Manufacturing)
                        Case ActivityComponentManufacturing, ActivityCapComponentManufacturing
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                            ' Add category for component
                            Select Case ItemGroupID
                                Case ItemIDs.TitanGroupID, ItemIDs.SupercarrierGroupID, ItemIDs.DreadnoughtGroupID, ItemIDs.CarrierGroupID,
                            ItemIDs.CapitalIndustrialShipGroupID, ItemIDs.IndustrialCommandShipGroupID, ItemIDs.FreighterGroupID, ItemIDs.JumpFreighterGroupID,
                                ItemIDs.AdvCapitalComponentGroupID, ItemIDs.CapitalComponentGroupID, ItemIDs.FAXGroupID
                                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, ItemIDs.CapitalComponentGroupID, IndustryActivities.Manufacturing) ' These all use cap components
                                Case Else
                                    SQL = SQL & GetFacilityCatGroupIDSQL(ItemIDs.ComponentCategoryID, ItemIDs.ConstructionComponentsGroupID, IndustryActivities.Manufacturing)
                            End Select
                        Case ActivityCopying
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Copying)
                        Case ActivityInvention
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Invention)
                        Case ActivityReactions
                            SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reactions) & " "
                            SQL = SQL & GetFacilityCatGroupIDSQL(ItemCategoryID, ItemGroupID, IndustryActivities.Reactions)
                    End Select
                End If

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()
            End If

            If rsLoader.HasRows Then
                ' Load the saved data but we may have to look up the material, time, and cost multipliers (not null if they set a manual value for outpost or upwells)
                FacilityID = rsLoader.GetInt64(0)
                FacilityTypeID = rsLoader.GetInt32(1)
                Tax = rsLoader.GetDouble(2)

                ' Pull data for ME/TE/CE
                Select Case FacilityType
                    Case FacilityTypes.Outpost, FacilityTypes.Station

                        ' Load the Stations in system for the activity we are doing
                        SQL = "SELECT DISTINCT MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                        SQL = SQL & "FROM STATION_FACILITIES WHERE OUTPOST  "

                        ' Set flag for outpost just to delineate
                        If FacilityType = FacilityTypes.Station Then
                            SQL = SQL & " = " & CStr(StationType.Station) & " "
                        Else
                            SQL = SQL & " = " & CStr(StationType.Outpost) & " "
                        End If
                        SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString(FacilityName) & "' "

                    Case FacilityTypes.POS

                        SQL = "SELECT MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER  "
                        SQL = SQL & "FROM ASSEMBLY_ARRAYS WHERE ARRAY_NAME = '" & FormatDBString(FacilityName) & "' "

                    Case FacilityTypes.UpwellStructure

                        SQL = "SELECT MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                        SQL &= "FROM UPWELL_STRUCTURES WHERE UPWELL_STRUCTURE_NAME = '" & FormatDBString(FacilityName) & "' "

                        Select Case Activity
                            Case ActivityManufacturing, ActivityComponentManufacturing, ActivityCapComponentManufacturing
                                SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Manufacturing) & " "
                            Case ActivityCopying
                                SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Copying) & " "
                            Case ActivityInvention
                                SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Invention) & " "
                            Case ActivityReactions
                                SQL = SQL & "AND ACTIVITY_ID = " & CStr(IndustryActivities.Reactions) & " "
                        End Select

                End Select

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsStats = DBCommand.ExecuteReader
                rsStats.Read()

                ' If the saved values are there, use those, else use the stats pull data
                If IsDBNull(rsLoader.GetValue(3)) Then
                    MaterialMultiplier = rsStats.GetDouble(0)
                Else
                    MaterialMultiplier = rsLoader.GetDouble(3)
                End If

                If IsDBNull(rsLoader.GetValue(4)) Then
                    TimeMultiplier = rsStats.GetDouble(1)
                Else
                    TimeMultiplier = rsLoader.GetDouble(4)
                End If

                If IsDBNull(rsLoader.GetValue(5)) Then
                    CostMultiplier = rsStats.GetDouble(2)
                Else
                    CostMultiplier = rsLoader.GetDouble(4)
                End If

                rsStats.Close()
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

        ' Now that we have everything, load the full facility into the appropriate selected facility to use later
        With SelectedFacility
            ' First, if this is a citadel, then look up any saved modules and adjust the MM/TM/CM
            If .FacilityType = FacilityTypes.UpwellStructure Then
                Dim InstalledModules = New List(Of Integer) ' Reset
                Dim SystemID As Long = GetSolarSystemID(SystemName)

                SQL = "SELECT INSTALLED_MODULE_ID FROM UPWELL_STRUCTURES_INSTALLED_MODULES, ENGINEERING_RIG_BONUSES "
                SQL &= "WHERE CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_VIEW = {3} AND FACILITY_ID = {4} "
                SQL &= "AND UPWELL_STRUCTURES_INSTALLED_MODULES.INSTALLED_MODULE_ID = ENGINEERING_RIG_BONUSES.typeID "
                SQL &= "AND ((categoryID = {5} AND groupID IS NULL) OR (categoryID IS NULL AND groupID = {6}))"
                DBCommand = New SQLiteCommand(String.Format(SQL, SelectedCharacterID, CStr(SelectedProductionType), CStr(SystemID), CStr(SelectedView),
                                              CStr(FacilityID), CStr(SelectedBPCategoryID), CStr(SelectedBPGroupID)), EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader

                While rsLoader.Read()
                    InstalledModules.Add(rsLoader.GetInt32(0))
                End While
                rsLoader.Close()

                If InstalledModules.Count <> 0 Then
                    ' Get the system security first
                    Dim security As Double = GetSolarSystemSecurityLevel(SystemName)
                    Dim securityAttribute As ItemAttributes

                    If Not IsNothing(security) Then
                        If security <= 0.0 Then
                            securityAttribute = ItemAttributes.nullSecModifier
                        ElseIf security < 0.45 Then
                            securityAttribute = ItemAttributes.lowSecModifier
                        Else
                            securityAttribute = ItemAttributes.hiSecModifier
                        End If
                    Else
                        ' Just assume null
                        securityAttribute = ItemAttributes.nullSecModifier
                    End If

                    ' Now, adjust the MM, TM, CM based on modules installed
                    For Each RigID In InstalledModules
                        ' Look up the bonus while adjusting for the type of space we are in
                        SQL = "SELECT attributeID, "
                        SQL &= "ABS((COALESCE(VALUEFLOAT, VALUEINT) * (SELECT COALESCE(VALUEFLOAT, VALUEINT) FROM TYPE_ATTRIBUTES WHERE TYPEID = {0} AND ATTRIBUTEID = {1}))/100) AS BONUS "
                        SQL &= "FROM TYPE_ATTRIBUTES WHERE ATTRIBUTEID IN (2593,2594,2595,2713,2714,2653) "
                        SQL &= "AND COALESCE(VALUEFLOAT, VALUEINT) <> 0 AND TYPEID = {0}"
                        SQL = String.Format(SQL, RigID, CInt(securityAttribute))
                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsLoader = DBCommand.ExecuteReader

                        While rsLoader.Read()
                            ' Adjust MM, TM, CM by attribute and set the base to this as well, override whatever they had before
                            Select Case rsLoader.GetInt32(0)
                                Case ItemAttributes.attributeEngRigCostBonus
                                    CostMultiplier = CostMultiplier * (1 - rsLoader.GetDouble(1))
                                    SelectedFacility.BaseCost = CostMultiplier
                                Case ItemAttributes.attributeEngRigMatBonus, ItemAttributes.RefRigMatBonus, ItemAttributes.attributeThukkerEngRigMatBonus
                                    MaterialMultiplier = MaterialMultiplier * (1 - rsLoader.GetDouble(1))
                                    SelectedFacility.BaseME = MaterialMultiplier
                                Case ItemAttributes.attributeEngRigTimeBonus, ItemAttributes.RefRigTimeBonus
                                    TimeMultiplier = TimeMultiplier * (1 - rsLoader.GetDouble(1))
                                    SelectedFacility.BaseTE = TimeMultiplier
                            End Select
                        End While
                    Next

                End If
            End If

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
            End Select

            .Activity = Activity
            .FacilityID = FacilityID
            .FacilityName = FacilityName
            .FacilityTypeID = FacilityTypeID
            .FacilityType = FacilityType
            .MaterialMultiplier = MaterialMultiplier
            .BaseME = MaterialMultiplier
            .TimeMultiplier = TimeMultiplier
            .BaseTE = TimeMultiplier
            .CostMultiplier = CostMultiplier
            .BaseCost = CostMultiplier
            .RegionName = cmbFacilityRegion.Text
            .SolarSystemName = cmbFacilitySystem.Text
            .FacilityProductionType = BuildType
            .TaxRate = Tax

            ChangingUsageChecks = True

            '.IncludeActivityUsage = chkFacilityIncludeUsage.Checked ' Use this value when loading from Load Facility (using the selected facility) or from the form dropdown (use the checkbox)

            If Not IsNothing(chkFacilityIncludeCost) And chkFacilityIncludeCost.Visible Then
                .IncludeActivityCost = chkFacilityIncludeCost.Checked
            Else
                .IncludeActivityCost = False
            End If

            If Not IsNothing(chkFacilityIncludeTime) And chkFacilityIncludeTime.Visible Then
                .IncludeActivityTime = chkFacilityIncludeTime.Checked
            Else
                .IncludeActivityTime = False
            End If
            ChangingUsageChecks = False

            If FacilityType <> FacilityTypes.None Then
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
                SQL = "SELECT COST_INDEX FROM INDUSTRY_SYSTEMS_COST_INDICIES "
                SQL &= "WHERE INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
                SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = " & .ActivityID & " "

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

        End With

        ' Always display the bonus, not the multiplier
        Dim MMText As String = FormatPercent(1 - MaterialMultiplier, 2)
        Dim TMText As String = FormatPercent(1 - TimeMultiplier, 2)
        Dim CostText As String = FormatPercent(1 - CostMultiplier, 2)
        Dim TaxText As String = FormatPercent(Tax, 1)

        If FacilityType = FacilityTypes.Outpost Or FacilityType = FacilityTypes.UpwellStructure Then
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
        UpdatingManualBoxes = True
        txtFacilityManualME.Text = MMText
        txtFacilityManualTE.Text = TMText
        txtFacilityManualTax.Text = TaxText
        txtFacilityManualCost.Text = CostText
        UpdatingManualBoxes = False

        ' Show the boxes
        Call SetFacilityBonusBoxes(True)

        ' Make sure the usage check is now enabled and update the box if a value exists
        If FacilityType <> FacilityTypes.None Then
            chkFacilityIncludeUsage.Enabled = True
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
        If Not FirstLoad Then
            Call SetFWUpgradeControls(SelectedFacility.SolarSystemName)
            If SelectedLocation = ProgramLocation.BlueprintTab Then
                Call CostIndexUpdateText()
            End If
        End If

        ' Loaded up, let them save it
        btnFacilitySave.Visible = True
        PreviousEquipment = cmbFacilityorArray.Text
        ' Fully loaded
        SelectedFacility.FullyLoaded = True

        ' Facility is loaded, so save it to default and dynamic variable
        Call SetFacility(SelectedFacility, BuildType, False, False)

        Application.DoEvents()

    End Sub

    ' Sets the sent facility To the one we are selecting And sets the Default 
    Private Sub SetFacility(ByVal SentFacility As IndustryFacility, BuildType As ProductionType,
                           ByVal CompareIncludeCostCheck As Boolean, ByVal CompareIncludeTimeCheck As Boolean)

        ' For checking change from stations to pos on  tab
        Dim PreviousFacility As New IndustryFacility

        Select Case BuildType
            Case ProductionType.Manufacturing
                PreviousFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
                SelectedManufacturingFacility = CType(SentFacility.Clone, IndustryFacility)
                ' Set the other three types for pos too
                If SentFacility.FacilityType = FacilityTypes.POS Then
                    SentFacility.FacilityName = SelectedPOSFuelBlockFacility.FacilityName
                    SentFacility.FacilityType = SelectedPOSFuelBlockFacility.FacilityType
                    SelectedPOSFuelBlockFacility = CType(SentFacility.Clone, IndustryFacility)

                    SentFacility.FacilityName = SelectedPOSLargeShipFacility.FacilityName
                    SentFacility.FacilityType = SelectedPOSLargeShipFacility.FacilityType
                    SelectedPOSLargeShipFacility = CType(SentFacility.Clone, IndustryFacility)

                    SentFacility.FacilityName = SelectedPOSModuleFacility.FacilityName
                    SentFacility.FacilityType = SelectedPOSModuleFacility.FacilityType
                    SelectedPOSModuleFacility = CType(SentFacility.Clone, IndustryFacility)
                End If
                If SelectedManufacturingFacility.IsEqual(DefaultManufacturingFacility) Then
                    SelectedManufacturingFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedManufacturingFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.POSFuelBlockManufacturing
                PreviousFacility = CType(SelectedPOSFuelBlockFacility.Clone, IndustryFacility)
                SelectedPOSFuelBlockFacility = SentFacility
                SelectedManufacturingFacility = SentFacility ' This is also the default POS for everything else, so save
                If SelectedPOSFuelBlockFacility.IsEqual(DefaultPOSFuelBlockFacility) Then
                    SelectedPOSFuelBlockFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedPOSFuelBlockFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.POSLargeShipManufacturing
                PreviousFacility = CType(SelectedPOSLargeShipFacility.Clone, IndustryFacility)
                SelectedPOSLargeShipFacility = SentFacility
                SelectedManufacturingFacility = SentFacility ' This is also the default POS for everything else, so save
                If SelectedPOSLargeShipFacility.IsEqual(DefaultPOSLargeShipFacility) Then
                    SelectedPOSLargeShipFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedPOSLargeShipFacility.IsDefault = False
                    SentFacility.IsDefault = False
                End If
            Case ProductionType.POSModuleManufacturing
                PreviousFacility = CType(SelectedPOSModuleFacility.Clone, IndustryFacility)
                SelectedPOSModuleFacility = SentFacility
                SelectedManufacturingFacility = SentFacility ' This is also the default POS for everything else, so save
                If SelectedPOSModuleFacility.IsEqual(DefaultPOSModuleFacility) Then
                    SelectedPOSModuleFacility.IsDefault = True
                    SentFacility.IsDefault = True
                Else
                    SelectedPOSModuleFacility.IsDefault = False
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
            ' Re-run the blueprint
            If SelectedLocation = ProgramLocation.BlueprintTab Then
                If chkFacilityIncludeUsage.Checked = True Then
                    SelectedFacility.IncludeActivityUsage = True
                Else
                    SelectedFacility.IncludeActivityUsage = False
                End If
                ' Facility is loaded, so save it to default and dynamic variable
                Call SetFacility(SelectedFacility, SelectedProductionType, False, False)
                If Not IsNothing(SelectedBlueprint) Then
                    Dim SentFrom As SentFromLocation
                    If SelectedLocation = ProgramLocation.BlueprintTab Then
                        SentFrom = SentFromLocation.BlueprintTab
                    ElseIf SelectedLocation = ProgramLocation.ManufacturingTab Then
                        SentFrom = SentFromLocation.ManufacturingTab
                    End If
                    With SelectedBlueprint
                        Call frmMain.UpdateBPGrids(.GetTypeID, .GetTechLevel, False, .GetItemGroupID, .GetItemCategoryID, SentFrom)
                    End With
                End If
            End If

            lblFacilityUsage.Text = FormatNumber(GetSelectedFacility.FacilityUsage, 2)

        End If
    End Sub

    Private Sub btnFacilityFitting_Click(sender As Object, e As EventArgs) Handles btnFacilityFitting.Click
        Dim StructureViewer As New frmUpwellStructureFitting(cmbFacilityorArray.Text, SelectedCharacterID,
                                                   SelectedProductionType, SelectedView, SelectedFacility.SolarSystemName)
        Call StructureViewer.ShowDialog()

        ' After showing, select the name of the citadel chosen and then dispose
        cmbFacilityorArray.Text = StructureViewer.UpwellStructureName

        Call StructureViewer.Dispose()

        ' Reload the stats each time
        Call DisplayFacilityBonus(SelectedProductionType, SelectedBPGroupID, SelectedBPCategoryID, cmbFacilityActivities.Text,
                                          GetFacilityTypeCode(cmbFacilityType.Text), cmbFacilityorArray.Text)

    End Sub

    Private Sub btnFacilitySave_Click(sender As Object, e As EventArgs) Handles btnFacilitySave.Click
        If SelectedFacility.FullyLoaded Then

            If SelectedFacility.SaveFacility(SelectedView, SelectedCharacterID) Then
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
                Case ProductionType.POSFuelBlockManufacturing
                    DefaultPOSFuelBlockFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.POSLargeShipManufacturing
                    DefaultPOSLargeShipFacility = CType(SelectedFacility.Clone, IndustryFacility)
                Case ProductionType.POSModuleManufacturing
                    DefaultPOSModuleFacility = CType(SelectedFacility.Clone, IndustryFacility)
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
                        Call LoadFacility(SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, False, False)
                    Else
                        SelectedProductionType = ProductionType.ComponentManufacturing
                        SelectedBPCategoryID = ItemIDs.ComponentCategoryID
                        SelectedBPGroupID = ItemIDs.None
                        SelectedBPTech = BPTechLevel.T1
                        cmbFacilityActivities.Text = ActivityComponentManufacturing
                        Call LoadFacility(SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, False, False)
                    End If
                Case ProductionType.T3DestroyerManufacturing, ProductionType.T3CruiserManufacturing
                    If chkFacilityToggle.Checked Then
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.TacticalDestroyerGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                        Call LoadFacility(SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, True, False)
                    Else
                        SelectedBPCategoryID = ItemIDs.ShipCategoryID
                        SelectedBPGroupID = ItemIDs.StrategicCruiserGroupID
                        SelectedBPTech = BPTechLevel.T3
                        cmbFacilityActivities.Text = ActivityManufacturing
                        Call LoadFacility(SelectedBPGroupID, SelectedBPCategoryID, SelectedBPTech, True, False)
                    End If
            End Select
        End If
    End Sub

#Region "Support Functions"

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
                Case ProductionType.POSFuelBlockManufacturing
                    FacilityActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultPOSFuelBlockFacility.Clone, IndustryFacility)
                Case ProductionType.POSLargeShipManufacturing
                    FacilityActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultPOSLargeShipFacility.Clone, IndustryFacility)
                Case ProductionType.POSModuleManufacturing
                    FacilityActivity = ActivityManufacturing
                    ReturnFacility = CType(DefaultPOSModuleFacility.Clone, IndustryFacility)
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
                Case ProductionType.POSFuelBlockManufacturing
                    FacilityActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedPOSFuelBlockFacility.Clone, IndustryFacility)
                Case ProductionType.POSLargeShipManufacturing
                    FacilityActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedPOSLargeShipFacility.Clone, IndustryFacility)
                Case ProductionType.POSModuleManufacturing
                    FacilityActivity = ActivityManufacturing
                    ReturnFacility = CType(SelectedPOSModuleFacility.Clone, IndustryFacility)
            End Select
        End If

        ' Set the activity text here
        ReturnFacility.Activity = FacilityActivity

        Return ReturnFacility

    End Function

    ' Returns the SQL string for querying by category or group id's 
    Public Function GetFacilityCatGroupIDSQL(ByVal CategoryID As Integer, ByVal GroupID As Integer, ByVal Activity As IndustryActivities) As String
        Dim SQL As String = ""
        Dim TempGroupID As Integer
        Dim TempCategoryID As Integer

        ' If the categoryID or groupID is for T3 invention, then switch the item ID's to the blueprint groupID for that item to match CCP's logic in table
        If Activity = IndustryActivities.Invention Then
            If CategoryID = ItemIDs.SubsystemCategoryID Then
                TempGroupID = ItemIDs.SubsystemBPGroupID
                TempCategoryID = 0
            ElseIf GroupID = ItemIDs.StrategicCruiserGroupID Then
                TempGroupID = ItemIDs.StrategicCruiserBPGroupID
                TempCategoryID = 0
            ElseIf GroupID = ItemIDs.TacticalDestroyerGroupID Then
                TempGroupID = ItemIDs.TacticalDestroyerBPGroupID
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
            lblFacilityDefault.Visible = True
            Call ResetToolTipforDefaultFacilityLabel(False)
            btnFacilitySave.Enabled = False ' don't enable since it's already the default, it's pointless to save it
        Else
            lblFacilityDefault.ForeColor = FacilityLabelNonDefaultColor
            lblFacilityDefault.Visible = True
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
        FacilityorArrayLoaded = FacilitiesValue

    End Sub

    ' Returns the POS manufacturing facility for the type sent
    Private Function GetPOSManufacturingFacilityName(ByVal SentFacility As IndustryFacility) As String

        Select Case SelectedProductionType
            Case ProductionType.POSFuelBlockManufacturing
                Return SelectedPOSFuelBlockFacility.FacilityName
            Case ProductionType.POSLargeShipManufacturing
                Return SelectedPOSLargeShipFacility.FacilityName
            Case ProductionType.POSModuleManufacturing
                Return SelectedPOSModuleFacility.FacilityName
            Case Else
                Return SentFacility.FacilityName
        End Select

    End Function

    Public Function GetPOSFuelBlockComboName() As String
        Return cmbFuelBlocks.Text
    End Function

    Public Function GetPOSLargeShipComboName() As String
        Return cmbLargeShips.Text
    End Function

    Public Function GetPOSModulesComboName() As String
        Return cmbModules.Text
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

        ' Determine if it's a fuel block, module, or big ship that can use a multi-use array in a POS - Need to add as a query, not hard code
        If FacilityType = FacilityTypes.POS And (BPGroupID = 1136 _
                                           Or BPCategoryID = 7 Or BPCategoryID = 20 Or BPCategoryID = 22 Or BPCategoryID = 23 _
                                           Or BPGroupID = 27 Or BPGroupID = 513 Or BPGroupID = 941 _
                                           Or BPGroupID = 12 Or BPGroupID = 340 Or BPGroupID = 448 Or BPGroupID = 649
                                           ) And BaseActivity = ActivityManufacturing Then
            If BPGroupID = 1136 Then
                SelectedIndyType = ProductionType.POSFuelBlockManufacturing
            ElseIf BPGroupID = 27 Or BPGroupID = 513 Or BPGroupID = 941 Then
                SelectedIndyType = ProductionType.POSLargeShipManufacturing
            ElseIf BPCategoryID = 7 Or BPCategoryID = 20 Or BPCategoryID = 22 Or BPCategoryID = 23 _
                Or BPGroupID = 12 Or BPGroupID = 340 Or BPGroupID = 448 Or BPGroupID = 649 Then
                SelectedIndyType = ProductionType.POSModuleManufacturing
            End If
        Else
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
                        ' Need to invent this at a station or pos
                        SelectedIndyType = ProductionType.T3Invention
                    Else
                        SelectedIndyType = ProductionType.Invention
                    End If
                Case ActivityReactions
                    SelectedIndyType = ProductionType.Reactions
            End Select
        End If

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
            RawCostSplit.UsageValue = GetSelectedManufacturingFacility.FacilityUsage
            f1.UsageSplits.Add(RawCostSplit)

            If SelectedBlueprint.HasComponents Then
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

            f1.Show()
        End If
    End Sub

    ' Updates the cost index text box on the bp tab
    Private Sub CostIndexUpdateText()

        If SelectedLocation = ProgramLocation.BlueprintTab And Not FirstLoad Then
            Dim System As String = cmbFacilitySystem.Text
            Dim Start As Integer = InStr(System, "(")
            If System.Contains("(") Then
                frmMain.txtBPUpdateCostIndex.Text = FormatPercent(System.Substring(Start, InStr(System, ")") - (Start + 1)), 2)
            End If
        End If

    End Sub

    ' Updates the blueprint on the bp tab if it's that facility
    Private Sub UpdateBlueprint()
        ' Load the bp on bp tab
        If Not IsNothing(SelectedBlueprint) And SelectedLocation = ProgramLocation.BlueprintTab Then
            Dim SentFrom As SentFromLocation
            If SelectedLocation = ProgramLocation.BlueprintTab Then
                SentFrom = SentFromLocation.BlueprintTab
            ElseIf SelectedLocation = ProgramLocation.ManufacturingTab Then
                SentFrom = SentFromLocation.ManufacturingTab
            End If
            With SelectedBlueprint
                Call frmMain.UpdateBPGrids(.GetTypeID, .GetTechLevel, False, .GetItemGroupID, .GetItemCategoryID, SentFrom)
            End With
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

        If Warzone Then
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
        Else
            lblFacilityFWUpgrade.Enabled = False
            lblFacilityFWUpgrade.Visible = False
            cmbFacilityFWUpgrade.Enabled = False
            cmbFacilityFWUpgrade.Visible = False
            cmbFacilityFWUpgrade.Text = None
            SelectedFacility.FWUpgradeLevel = -1
        End If
        rsFW.Close()

    End Sub

    Private Sub cmbFWUpgrade_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityFWUpgrade.SelectedIndexChanged
        If Not FirstLoad Then
            btnFacilitySave.Enabled = True
            ' Set the selected level
            SelectedFacility.FWUpgradeLevel = GetFWUpgradeLevel(cmbFacilitySystem.Text)
        End If
    End Sub

    Private Sub cmbFWUpgrade_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityFWUpgrade.KeyPress
        e.Handled = True
    End Sub

#End Region

#Region "Public Functions"

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
            Case ProductionType.POSFuelBlockManufacturing
                SelectedPOSFuelBlockFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.POSLargeShipManufacturing
                SelectedPOSLargeShipFacility = CType(UpdatedFacility.Clone, IndustryFacility)
            Case ProductionType.POSModuleManufacturing
                SelectedPOSModuleFacility = CType(UpdatedFacility.Clone, IndustryFacility)
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
            Case ProductionType.POSFuelBlockManufacturing
                If SelectedPOSFuelBlockFacility.FullyLoaded Then
                    Return SelectedPOSFuelBlockFacility
                Else
                    Return DefaultPOSFuelBlockFacility
                End If
            Case ProductionType.POSLargeShipManufacturing
                If SelectedPOSLargeShipFacility.FullyLoaded Then
                    Return SelectedPOSLargeShipFacility
                Else

                    Return DefaultPOSLargeShipFacility
                End If
            Case ProductionType.POSModuleManufacturing
                If SelectedPOSModuleFacility.FullyLoaded Then
                    Return SelectedPOSModuleFacility
                Else
                    Return DefaultPOSModuleFacility
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
            Case Else
                Return Nothing
        End Select

        Return Nothing

    End Function

    ' Gets the facility for manufacturing based on the bp data on initialization or sent bp data
    Public Function GetSelectedManufacturingFacility(Optional BPGroupID As Integer = 0, Optional BPCategoryID As Integer = 0,
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
            SelectedActivity = ActivityManufacturing
        Else
            SelectedActivity = ActivityManufacturing
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
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedNegativePercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub txtFacilityManualME_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualME.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._ME)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualME_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualME.GotFocus
        Call txtFacilityManualCost.SelectAll()
    End Sub

    Private Sub txtFacilityManualME_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualME.LostFocus
        Call SetManualTextBoxValue(BoxType._ME)
        Call UpdateBlueprint()
    End Sub

    Private Sub txtFacilityManualTE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualTE.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedNegativePercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtFacilityManualTE_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualTE.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._TE)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualTE_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTE.GotFocus
        Call txtFacilityManualTE.SelectAll()
    End Sub

    Private Sub txtFacilityManualTE_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTE.LostFocus
        Call SetManualTextBoxValue(BoxType._TE)
        Call UpdateBlueprint()
    End Sub

    Private Sub txtFacilityManualCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualCost.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedNegativePercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtFacilityManualCost_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualCost.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._Cost)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualCost_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualCost.GotFocus
        Call txtFacilityManualCost.SelectAll()
    End Sub

    Private Sub txtFacilityManualCost_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualCost.LostFocus
        Call SetManualTextBoxValue(BoxType._Cost)
        Call UpdateBlueprint()
    End Sub

    Private Sub txtFacilityManualTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFacilityManualTax.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedNegativePercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtFacilityManualTax_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFacilityManualTax.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SetManualTextBoxValue(BoxType._Tax)
            Call UpdateBlueprint()
        End If
    End Sub

    Private Sub txtFacilityManualTax_GotFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTax.GotFocus
        Call txtFacilityManualTax.SelectAll()
    End Sub

    Private Sub txtFacilityManualTax_LostFocus(sender As Object, e As EventArgs) Handles txtFacilityManualTax.LostFocus
        Call SetManualTextBoxValue(BoxType._Tax)
        Call UpdateBlueprint()
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
                    SelectedFacility.MaterialMultiplier = 1 - FormatManualEntry(txtFacilityManualME.Text)
                    GetFacility(SelectedFacility.FacilityProductionType).MaterialMultiplier = SelectedFacility.MaterialMultiplier
                    txtFacilityManualME.Text = FormatPercent(1 - SelectedFacility.MaterialMultiplier, 2)
                Case BoxType._TE
                    SelectedFacility.TimeMultiplier = 1 - FormatManualEntry(txtFacilityManualTE.Text)
                    GetFacility(SelectedFacility.FacilityProductionType).TimeMultiplier = SelectedFacility.TimeMultiplier
                    txtFacilityManualTE.Text = FormatPercent(1 - SelectedFacility.TimeMultiplier, 2)
                Case BoxType._Cost
                    SelectedFacility.CostMultiplier = 1 - FormatManualEntry(txtFacilityManualCost.Text)
                    GetFacility(SelectedFacility.FacilityProductionType).CostMultiplier = SelectedFacility.CostMultiplier
                    txtFacilityManualCost.Text = FormatPercent(1 - SelectedFacility.CostMultiplier, 2)
                Case BoxType._Tax
                    SelectedFacility.TaxRate = FormatManualEntry(txtFacilityManualTax.Text)
                    GetFacility(SelectedFacility.FacilityProductionType).TaxRate = SelectedFacility.TaxRate
                    txtFacilityManualTax.Text = FormatPercent(SelectedFacility.TaxRate, 2)
            End Select

            UpdatingManualBoxes = False

        End If

    End Sub

    Private Enum BoxType
        _ME = 0
        _TE = 1
        _Cost = 2
        _Tax = 3
    End Enum

#End Region

End Class

' What type of view are we looking at
Public Enum FacilityView
    FullControls = 0 ' for BP tab right now
    LimitedControls = 1 ' for use on manufacturing tab now
    NoView = 2 ' For not connecting this to a tab or facilty view
End Enum

Public Enum ProgramLocation
    BlueprintTab = 0
    ManufacturingTab = 1
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

    ' Special POS Arrays
    POSModuleManufacturing = 14
    POSFuelBlockManufacturing = 15
    POSLargeShipManufacturing = 16

End Enum

Public Enum ItemIDs
    None = 0

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

    ShipCategoryID = 6 ' for loading invention and copying and basic t1
    FrigateGroupID = 25

    ' Reactions
    ReactionsIntermediateGroupID = 428
    ReactionCompositesGroupID = 429
    ReactionPolymersGroupID = 974
    ReactionBiochmeicalsGroupID = 712

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
    UpwellStructure = 3
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
    Public Activity As String ' String value of the activity
    Public RegionName As String ' Region of this facility
    Public RegionID As Long
    Public SolarSystemName As String ' System where this is located
    Public SolarSystemID As Long
    Public SolarSystemSecurity As Double
    Public FWUpgradeLevel As Integer ' Level of the FW upgrade for this system (if applies)
    Public CostIndex As Double ' Cost index for the system and activity from ESI
    Public ActivityCostPerSecond As Double ' The cost to conduct the activity for this facility per second - my setting for POS and ECs
    Public IsDefault As Boolean
    Public IncludeActivityCost As Boolean ' This is the total cost of materials to do the activiy
    Public IncludeActivityTime As Boolean ' This is the time for doing the activity
    Public IncludeActivityUsage As Boolean ' This is the cost of using the facility only

    Public FacilityUsage As Double ' The usage charged by this facility, set after bp has run
    Public UsageToolTipText As String ' The text to display for the usage label

    ' Nullable fields
    Public TaxRate As Double ' The tax rate
    ' Remove these eventually when outposts and pos removed
    Public MaterialMultiplier As Double ' The bonus material percentage for materials used in this facility
    Public TimeMultiplier As Double ' The bonus to time to conduct an activity in this facility
    Public CostMultiplier As Double ' The bonus to cost to conduct an activity in this facility
    Public BaseME As Double ' The ME bonus from default
    Public BaseTE As Double ' The TE bonus from default
    Public BaseCost As Double ' The Cost bonus from default

    Public FullyLoaded As Boolean ' This facility was fully loaded in all parts

    ' Default multiplier rates if we can't find them
    Public Const DefaultTaxRate As Double = 0
    Public Const DefaultMaterialMultiplier As Double = 1
    Public Const DefaultTimeMultiplier As Double = 1
    Public Const DefaultCostMultiplier As Double = 1

    Public Sub New()

        FacilityID = 0
        FacilityName = None
        FacilityType = FacilityTypes.None
        FacilityTypeID = 0
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
        BaseME = 0
        BaseTE = 0
        BaseCost = 0

        FullyLoaded = False

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
        CopyOfMe.BaseME = BaseME
        CopyOfMe.BaseTE = BaseTE
        CopyOfMe.BaseCost = BaseCost
        CopyOfMe.FullyLoaded = FullyLoaded

        Return CopyOfMe

    End Function

    ' Load up the facility data from the table as default
    Public Sub InitalizeFacility(InitialProductionType As ProductionType, FacilityTab As FacilityView)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader

        ' Look up all the data in two queries - first base data and try to get the multipliers and cost data - it should only be there for saved outposts (which are being removed)
        SQL = "SELECT SF.FACILITY_ID, SF.FACILITY_TYPE, SF.FACILITY_TYPE_ID, "
        SQL &= "FACILITY_PRODUCTION_TYPES.ACTIVITY_ID, RAM_ACTIVITIES.activityName, "
        SQL &= "REGIONS.regionName, REGIONS.regionID, SOLAR_SYSTEMS.solarSystemName, SOLAR_SYSTEMS.solarSystemID, "
        SQL &= "CASE WHEN UPGRADE_LEVEL IS NULL THEN 0 ELSE UPGRADE_LEVEL END AS FW_UPGRADE_LEVEL, SF.ACTIVITY_COST_PER_SECOND, "
        SQL &= "CASE WHEN COST_INDEX IS NULL THEN 0 ELSE COST_INDEX END AS COST_INDEX,"
        SQL &= "SF.INCLUDE_ACTIVITY_COST, SF.INCLUDE_ACTIVITY_TIME, SF.INCLUDE_ACTIVITY_USAGE, "
        SQL &= "SF.FACILITY_TAX, SF.MATERIAL_MULTIPLIER, SF.TIME_MULTIPLIER, SF.COST_MULTIPLIER, security "
        SQL &= "FROM SAVED_FACILITIES AS SF, FACILITY_PRODUCTION_TYPES, REGIONS, SOLAR_SYSTEMS, FACILITY_TYPES, RAM_ACTIVITIES "
        SQL &= "LEFT JOIN FW_SYSTEM_UPGRADES On FW_SYSTEM_UPGRADES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID "
        SQL &= "LEFT JOIN INDUSTRY_SYSTEMS_COST_INDICIES "
        SQL &= "ON INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = SF.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = FACILITY_PRODUCTION_TYPES.ACTIVITY_ID "
        SQL &= "WHERE SF.PRODUCTION_TYPE = FACILITY_PRODUCTION_TYPES.PRODUCTION_TYPE "
        SQL &= "AND SF.REGION_ID = REGIONS.regionID "
        SQL &= "AND SF.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND SF.FACILITY_TYPE = FACILITY_TYPES.FACILITY_TYPE_ID "
        SQL &= "AND FACILITY_PRODUCTION_TYPES.ACTIVITY_ID = RAM_ACTIVITIES.activityID "
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
                Activity = .GetString(4)
                RegionName = .GetString(5)
                RegionID = .GetInt64(6)
                ' Paste the cost index to the solar system name
                CostIndex = .GetFloat(11)
                SolarSystemName = .GetString(7) & " (" & FormatNumber(CostIndex, 3) & ")"
                SolarSystemID = .GetInt64(8)
                SolarSystemSecurity = .GetDouble(19)
                FWUpgradeLevel = .GetInt32(9)
                ActivityCostPerSecond = .GetFloat(10)

                IncludeActivityCost = CBool(.GetInt32(12))
                IncludeActivityTime = CBool(.GetInt32(13))
                IncludeActivityUsage = CBool(.GetInt32(14))

                ' Save these values for later lookup - use -1 for null indicator - these are what they saved manually
                If IsDBNull(.GetValue(15)) Then
                    TaxRate = -1
                Else
                    TaxRate = .GetDouble(15)
                End If

                If IsDBNull(.GetValue(16)) Then
                    MaterialMultiplier = -1
                Else
                    MaterialMultiplier = .GetDouble(16)
                End If

                If IsDBNull(.GetValue(17)) Then
                    TimeMultiplier = -1
                Else
                    TimeMultiplier = .GetDouble(17)
                End If

                If IsDBNull(.GetValue(18)) Then
                    CostMultiplier = -1
                Else
                    CostMultiplier = .GetDouble(18)
                End If

                ' Now, depending on type, look up the name, cost index, tax, and multipliers from the station_facilities table (this is mainly for speed)
                If FacilityType = FacilityTypes.POS Then
                    SQL = "SELECT DISTINCT ARRAY_NAME, " & CStr(POSTaxRate) & " AS FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                    SQL = SQL & "FROM ASSEMBLY_ARRAYS WHERE ARRAY_TYPE_ID = " & CStr(FacilityTypeID) & " " ' use typeid for the module used
                ElseIf FacilityType = FacilityTypes.Station Or FacilityType = FacilityTypes.Outpost Then ' Stations, outposts
                    SQL = "SELECT DISTINCT FACILITY_NAME, FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
                    SQL = SQL & "FROM STATION_FACILITIES WHERE FACILITY_ID = " & CStr(FacilityID) & " "
                ElseIf FacilityType = FacilityTypes.UpwellStructure Then
                    SQL = "SELECT DISTINCT UPWELL_STRUCTURE_NAME, " & CStr(POSTaxRate) & " AS FACILITY_TAX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, COST_MULTIPLIER "
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
            SQL = String.Format("SELECT 'X' FROM SAVED_FACILITIES WHERE PRODUCTION_TYPE = {0} AND FACILITY_VIEW = {1} AND CHARACTER_ID = {2}",
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

                If FacilityType = FacilityTypes.Outpost Or FacilityType = FacilityTypes.UpwellStructure Then
                    ' if what they have now is different from what they started with, then they made a change
                    ' for upwell structures, the base is updated when they make changes to the facility fitting
                    If MaterialMultiplier <> BaseME Then
                        TempSQL &= "MATERIAL_MULTIPLIER = " & CStr(MaterialMultiplier) & ", "
                    Else
                        TempSQL &= "MATERIAL_MULTIPLIER = NULL, "
                    End If

                    If TimeMultiplier <> BaseTE Then
                        TempSQL &= "TIME_MULTIPLIER = " & CStr(TimeMultiplier) & ", "
                    Else
                        TempSQL &= "TIME_MULTIPLIER = NULL, "
                    End If
                    If CostMultiplier <> BaseCost Then
                        TempSQL &= "COST_MULTIPLIER = " & CStr(CostMultiplier) & " "
                    Else
                        TempSQL &= "COST_MULTIPLIER = NULL "
                    End If
                Else
                    TempSQL &= "MATERIAL_MULTIPLIER = NULL, "
                    TempSQL &= "TIME_MULTIPLIER = NULL, "
                    TempSQL &= "COST_MULTIPLIER = NULL "
                End If
                TempSQL &= "WHERE PRODUCTION_TYPE = {10} AND FACILITY_VIEW = {11} AND CHARACTER_ID = {12}"

                SQL = String.Format(TempSQL, FacilityID, CInt(FacilityType), FacilityTypeID,
                        RegionID, SolarSystemID, ActivityCostPerSecond,
                        CInt(IncludeActivityCost), CInt(IncludeActivityTime), CInt(IncludeActivityUsage),
                        TaxRate, CInt(FacilityProductionType), CInt(ViewType), CharacterID)
            Else
                Dim MEValue As String = "NULL"
                Dim TEValue As String = "NULL"
                Dim CostValue As String = "NULL"

                If FacilityType = FacilityTypes.Outpost Or FacilityType = FacilityTypes.UpwellStructure Then
                    ' if what they have now is different from what they started with, then they made a change
                    ' for upwell structures, the base is updated when they make changes to the facility fitting
                    If MaterialMultiplier <> BaseME Then
                        MEValue = CStr(MaterialMultiplier)
                    End If

                    If TimeMultiplier <> BaseTE Then
                        TEValue = CStr(TimeMultiplier)
                    End If
                    If CostMultiplier <> BaseCost Then
                        CostValue = CStr(CostMultiplier)
                    End If
                End If

                ' Insert
                SQL = String.Format("INSERT INTO SAVED_FACILITIES VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})",
                                CharacterID, CInt(FacilityProductionType), CInt(ViewType), FacilityID, CInt(FacilityType), FacilityTypeID,
                                RegionID, SolarSystemID, ActivityCostPerSecond,
                                CInt(IncludeActivityCost), CInt(IncludeActivityTime), CInt(IncludeActivityUsage),
                                TaxRate, MEValue, TEValue, CostValue)
            End If

            ' Commit the change
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            ' Update FW upgrade
            If FWUpgradeLevel <> -1 Then
                ' See if we update or insert
                SQL = "SELECT * FROM FW_SYSTEM_UPGRADES WHERE SOLAR_SYSTEM_ID = " & CStr(SolarSystemID) & " "

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader
                rsCheck.Read()

                If rsCheck.HasRows Then
                    SQL = "UPDATE FW_SYSTEM_UPGRADES SET UPGRADE_LEVEL = " & CStr(FWUpgradeLevel)
                    SQL = SQL & " WHERE SOLAR_SYSTEM_ID = " & SolarSystemID
                Else
                    SQL = "INSERT INTO FW_SYSTEM_UPGRADES VALUES (" & SolarSystemID & "," & CStr(FWUpgradeLevel) & ")"
                End If

                Call EVEDB.ExecuteNonQuerySQL(SQL)
                rsCheck.Close()
            End If
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

    Public Function GetFacilityTypeDescription() As String
        Select Case FacilityType
            Case FacilityTypes.POS
                Return ManufacturingFacility.POSFacility
            Case FacilityTypes.Outpost
                Return ManufacturingFacility.OutpostFacility
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

End Class
