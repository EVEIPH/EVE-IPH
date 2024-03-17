Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.IO

Public Class frmUpwellStructureFitting

    Declare Function SendMessage Lib "User32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Const WM_NCLBUTTONDOWN As Integer = &HA1
    Const HTCAPTION As Integer = 2

    Private SlotPictureBoxList As New List(Of PictureBox)
    Private FirstLoad As Boolean
    Private FirstModuleLoad As Boolean
    Private UpdateChecks As Boolean

    ' Public settings after intialized and returned for setting in the facilities
    Public UpwellStructureName As String = ""
    Public SavedFacility As Boolean
    Private SelectedStructureLocation As ProgramLocation ' To help determine where we save citadels, etc. 
    Private SelectedCharacterID As Long
    Private SelectedFacilityProductionType As ProductionType
    Private SelectedSolarSystemID As Long
    Private SelectedFacilityID As Long
    ' Save the selected Upwell Structure so we don't need to look it up
    Private SelectedUpwellStructure As UpwellStructureDBData

    Private Attributes As New EVEAttributes
    ' Stores all the stats for the selected citadel
    Private UpwellStructureStats As New StructureAttributes

    Private Const UsesMissilesEffectID As Integer = 101

    Private StructureDBDataList As New List(Of UpwellStructureDBData) ' For storing all the types of citadel structures

    Private HighSlotBaseX As Integer
    Private HighSlotBaseWidth As Integer
    Private HighSlotSpacing As Integer
    Private ServiceSlotBaseX As Integer
    Private ServiceSlotBaseWidth As Integer
    Private ServiceSlotSpacing As Integer

    Private NitrogenFuelBlockBPUpdated As Boolean
    Private HeliumFuelBlockBPUpdated As Boolean
    Private HydrogenFuelBlockBPUpdated As Boolean
    Private OxygenFuelBlockBPUpdated As Boolean

    ' Save these here incase we update the ME
    Private OriginalNitrogenFuelBlockBPME As Integer
    Private OriginalHeliumFuelBlockBPME As Integer
    Private OriginalHydrogenFuelBlockBPME As Integer
    Private OriginalOxygenFuelBlockBPME As Integer
    Private OriginalNitrogenFuelBlockBPTE As Integer
    Private OriginalHeliumFuelBlockBPTE As Integer
    Private OriginalHydrogenFuelBlockBPTE As Integer
    Private OriginalOxygenFuelBlockBPTE As Integer

    Private SecurityCheckBoxes As List(Of CheckBox)

    Private frmPopout As frmBonusPopout

    Private InstalledModules As List(Of Integer)

    Private ColumnClicked As Integer
    Private ColumnSortType As SortOrder
    Private ModulesColumnClicked As Integer
    Private ModulesColumnSortType As SortOrder

    ' Fuel block IDs
    Private Enum FuelBlocks
        Nitrogen = 4051
        NitrogenBP = 4314
        Hydrogen = 4246
        HydrogenBP = 4316
        Helium = 4247
        HeliumBP = 4315
        Oxygen = 4312
        OxygenBP = 4313
    End Enum

    ' Used to look up modules and rigs to go into what slot
    Private Enum SlotSizes
        LowSlot = 11
        MediumSlot = 13
        HighSlot = 12
    End Enum

    Public Structure StructureModule
        Dim typeID As Integer
        Dim moduleType As String
    End Structure

    Private Structure UpwellStructureDBData
        Dim Name As String
        Dim TypeID As Integer
        Dim GroupID As Integer
    End Structure

    Private Enum ServiceType
        Citadel = 1321
        Resource = 1322
        Engineering = 1415
        MoonDrill = 1887
    End Enum

    Private Enum StructureGroup
        Citadel = 1657
        Refinery = 1406
        Engineering = 1404
    End Enum

    ' For saving and updating the selected upwell structure
    Private Structure StructureAttributes
        Dim CPU As Double
        Dim MaxCPU As Double
        Dim PG As Double
        Dim MaxPG As Double
        Dim Calibration As Double
        Dim MaxCalibration As Double
        Dim Capacitor As Double
        Dim MaxCapacitor As Double
        Dim CapacitorRechargeRate As Double
        Dim BaseCapRechargeRate As Double
        Dim ServiceModuleFuelBPH As Integer ' Blocks per hour
        Dim OnlineFuelAmount As Integer ' Blocks to bring services online
        Dim FuelBonus As Double ' Bonus for the type of modules it supports
        Dim LauncherSlots As Integer

    End Structure

    Public Sub New(ByVal InitUSName As String, ByVal CharacterID As Long, ByVal ProductionTypeCode As ProductionType,
                   ByVal FacilityLocation As ProgramLocation, ByVal FacilitySystem As String)

        FirstLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Save these varibles for later
        SelectedCharacterID = CharacterID
        SelectedFacilityProductionType = ProductionTypeCode
        SelectedStructureLocation = FacilityLocation

        ' Put all the slot images into an array
        With SlotPictureBoxList
            .Add(HighSlot1)
            .Add(HighSlot2)
            .Add(HighSlot3)
            .Add(HighSlot4)
            .Add(HighSlot5)
            .Add(HighSlot6)
            .Add(HighSlot7)
            .Add(HighSlot8)

            .Add(MidSlot1)
            .Add(MidSlot2)
            .Add(MidSlot3)
            .Add(MidSlot4)
            .Add(MidSlot5)
            .Add(MidSlot6)
            .Add(MidSlot7)
            .Add(MidSlot8)

            .Add(LowSlot1)
            .Add(LowSlot2)
            .Add(LowSlot3)
            .Add(LowSlot4)
            .Add(LowSlot5)
            .Add(LowSlot6)
            .Add(LowSlot7)
            .Add(LowSlot8)

            .Add(ServiceSlot1)
            .Add(ServiceSlot2)
            .Add(ServiceSlot3)
            .Add(ServiceSlot4)
            .Add(ServiceSlot5)
            .Add(ServiceSlot6)

            .Add(RigSlot1)
            .Add(RigSlot2)
            .Add(RigSlot3)
        End With

        ' Save values
        HighSlotBaseX = HighSlot1.Location.X
        HighSlotBaseWidth = HighSlot1.Width
        HighSlotSpacing = HighSlot2.Location.X - (HighSlot1.Location.X + HighSlot1.Width)
        ServiceSlotBaseX = ServiceSlot1.Location.X
        ServiceSlotBaseWidth = ServiceSlot1.Width
        ServiceSlotSpacing = ServiceSlot2.Location.X - (ServiceSlot1.Location.X + ServiceSlot1.Width)

        SecurityCheckBoxes = New List(Of CheckBox)
        Call SecurityCheckBoxes.Add(chkHighSec)
        Call SecurityCheckBoxes.Add(chkLowSec)
        Call SecurityCheckBoxes.Add(chkNullSec)

        ' Get the security of the system
        Dim System As String = FacilitySystem

        If FacilitySystem.Contains("(") Then
            ' Reset if it has the system index
            System = FacilitySystem.Substring(0, InStr(FacilitySystem, "(") - 2)
        End If

        Dim FacilitySystemSecurity As Double = GetSolarSystemSecurityLevel(System)
        SelectedSolarSystemID = GetSolarSystemID(System)

        ' Select the security check box
        If FacilitySystemSecurity <= 0.0 Then
            chkNullSec.Checked = True
        ElseIf FacilitySystemSecurity < 0.45 Then
            chkLowSec.Checked = True
        Else
            chkHighSec.Checked = True
        End If

        'enable/ disable depending on the view
        If SelectedStructureLocation = ProgramLocation.None Then
            ' They aren't connected to a system
            chkHighSec.Enabled = True
            chkLowSec.Enabled = True
            chkNullSec.Enabled = True
        Else
            ' They are launching from a facility to view a system, don't let them change it
            chkHighSec.Enabled = False
            chkLowSec.Enabled = False
            chkNullSec.Enabled = False
        End If

        With UserUpwellStructureSettings
            chkItemViewTypeHigh.Checked = .HighSlotsCheck
            chkItemViewTypeMedium.Checked = .MediumSlotsCheck
            chkItemViewTypeLow.Checked = .LowSlotsCheck
            chkItemViewTypeServices.Checked = .ServicesCheck

            chkRigTypeViewReprocessing.Checked = .ReprocessingRigsCheck
            chkRigTypeViewEngineering.Checked = .EngineeringRigsCheck
            chkRigTypeViewCombat.Checked = .CombatRigsCheck
            chkRigTypeViewReaction.Checked = .ReactionsRigsCheck
            chkRigTypeViewDrilling.Checked = .DrillingRigsCheck

            txtItemFilter.Text = .SearchFilterText

            chkIncludeFuelCosts.Checked = .IncludeFuelCostsCheck

            Select Case .FuelBlockType
                Case rbtnHeliumFuelBlock.Text
                    rbtnHeliumFuelBlock.Checked = True
                Case rbtnHydrogenFuelBlock.Text
                    rbtnHydrogenFuelBlock.Checked = True
                Case rbtnNitrogenFuelBlock.Text
                    rbtnNitrogenFuelBlock.Checked = True
                Case rbtnOxygenFuelBlock.Text
                    rbtnOxygenFuelBlock.Checked = True
                Case Else
                    rbtnHeliumFuelBlock.Checked = True
            End Select

            Select Case .BuyBuildBlockOption
                Case rbtnBuildBlocks.Text
                    rbtnBuildBlocks.Checked = True
                Case rbtnBuyBlocks.Text
                    rbtnBuyBlocks.Checked = True
                Case Else
                    rbtnBuildBlocks.Checked = True
            End Select

            If .IconListView Then
                rbtnViewIcons.Checked = True
            Else
                rbtnListView.Checked = True
            End If

        End With

        InstalledModules = New List(Of Integer)

        ' Get all data on structures for DB look ups first
        Call LoadStructureDBData()

        ' Add all the images to the image list
        Call LoadFittingImages()

        ' Load the facility default if saved
        Call LoadStructure(InitUSName)

        ' Load up all the fuel block data on that tab
        Call LoadFuelBlockDataTab()

        NitrogenFuelBlockBPUpdated = False
        HeliumFuelBlockBPUpdated = False
        HydrogenFuelBlockBPUpdated = False
        OxygenFuelBlockBPUpdated = False

        ' Set tool tips
        If UserApplicationSettings.ShowToolTips Then
            With MainToolTip
                .SetToolTip(btnRefreshPrices, "Refreshes prices on screen (useful if update done in other parts of program)")
                .SetToolTip(btnSavePrices, "Saves prices entered for buying fuel blocks or materials if building")
                .SetToolTip(btnUpdateBuildCost, "Updated the build cost after updating a Fuel Block ME value")
                .SetToolTip(btnSaveFuelBlockInfo, "Saves the Fuel Block BP ME data if changed")
                .SetToolTip(lblFuelReductionBonus, "Fuel bonus only applies to certain services for the structure.")
            End With
        End If

        frmPopout = New frmBonusPopout

        SavedFacility = False
        FirstLoad = False

    End Sub

    Private Sub LoadStructureDBData()
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        SQL = "SELECT typeID, typeName, INVENTORY_GROUPS.groupID FROM INVENTORY_TYPES, INVENTORY_GROUPS WHERE INVENTORY_GROUPS.categoryID = 65 
               AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupid AND INVENTORY_TYPES.published = 1 
               AND INVENTORY_TYPES.groupID NOT IN (1408, 2016, 2017)"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        ' Clear the combo
        Call cmbUpwellStructureName.Items.Clear()

        While rsReader.Read()
            Dim TempData As UpwellStructureDBData

            TempData.TypeID = rsReader.GetInt32(0)
            TempData.Name = rsReader.GetString(1)
            TempData.GroupID = rsReader.GetInt32(2)

            Call StructureDBDataList.Add(TempData)

            ' Also add each to the combo box
            Call cmbUpwellStructureName.Items.Add(TempData.Name)

        End While

        rsReader.Close()

    End Sub

    Private Sub ServiceModuleListView_MouseDown(sender As Object, e As MouseEventArgs) Handles FittingListViewIcons.MouseDown
        ' Make sure we select the image
        Dim Selection As ListViewItem = FittingListViewIcons.GetItemAt(e.X, e.Y)

        If Not IsNothing(Selection) Then
            Dim ModuleTypeID As Integer = CInt(Selection.ImageKey)

            If Not IsNothing(Selection) Then
                pbFloat.Image = FittingImages.Images(Selection.ImageKey)
                pbFloat.Name = Selection.Group.Name
                pbFloat.Tag = Selection.Group.Tag
                pbFloat.Text = Selection.Text
            Else
                pbFloat.Image = Nothing
            End If

            If Not IsNothing(pbFloat.Image) Then
                pbFloat.Visible = True
                pbFloat.Location = New Point(e.X + FittingListViewIcons.Left, e.Y + FittingListViewIcons.Top)
                ' Now select the image and connect it to the mouse cursor
                SendMessage(pbFloat.Handle.ToInt32, WM_NCLBUTTONDOWN, HTCAPTION, 0&)
            Else
                pbFloat.Visible = False
            End If

            pbFloat.Visible = False

            Dim SlotLocation As Point
            Dim WHAdjust As Integer = 64
            Dim MP As Point = PointToClient(MousePosition)

            ' Loop through all the picture boxes and update the one they clicked over
            For Each Slot In SlotPictureBoxList

                SlotLocation = Slot.Location
                SlotLocation.X += tabUpwellStructure.Left
                SlotLocation.Y += tabUpwellStructure.Top

                ' See if they dropped the image on a fitting slot and change the selected item
                If MP.X > SlotLocation.X And MP.X < SlotLocation.X + WHAdjust And
                    MP.Y > SlotLocation.Y And MP.Y < SlotLocation.Y + WHAdjust Then
                    Dim FloatSlot As String = CStr(pbFloat.Tag)

                    If FloatSlot.Contains(Slot.Name.Substring(0, Len(Slot.Name) - 1)) Then

                        If Not CheckSlots(ModuleTypeID) Then
                            Exit Sub
                        End If

                        ' Set the image info
                        Slot.Image = pbFloat.Image
                        Slot.Image.Tag = ModuleTypeID
                        Slot.Tag = pbFloat.Name
                        Slot.Text = pbFloat.Text

                        ' Update the slot stats
                        Call UpdateUpwellStructureStats()
                        ' Update the launcher slots if added a launcher
                        Call UpdateLauncherSlots(False, ModuleTypeID)
                        ' Done updating
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub

    ' Loads a selected image in a free slot - use for double-click on an item
    Private Sub LoadSelectedImageInFreeSlot()
        Dim Selection As ListViewItem = FittingListViewIcons.SelectedItems(0)

        If Not IsNothing(Selection) Then
            Call LoadImageInFreeSlot(CInt(Selection.ImageKey), Selection.Text, CStr(Selection.Group.Tag), Selection.Group.Name)
        End If

    End Sub

    Private Sub FittingListViewDetails_DoubleClick(sender As Object, e As EventArgs) Handles FittingListViewDetails.DoubleClick
        Dim Selection As ListViewItem = FittingListViewDetails.SelectedItems(0)

        If Not IsNothing(Selection) Then
            Call LoadImageInFreeSlot(CInt(Selection.SubItems(2).Text), Selection.SubItems(0).Text, Selection.SubItems(3).Text, Selection.SubItems(1).Text)
        End If
    End Sub

    ' Loads the image in the first free slot if available - use for double-click an item
    Private Sub LoadImageInFreeSlot(ByVal ModuleTypeID As Integer, ByVal ModuleName As String, ByVal GroupTag As String, ByVal GroupName As String)

        ' Loop through all the picture boxes and add the first one that is empty
        For Each Slot In SlotPictureBoxList
            Dim FloatSlot As String = GroupTag

            If FloatSlot.Contains(Slot.Name.Substring(0, Len(Slot.Name) - 1)) Then

                If Not CheckSlots(ModuleTypeID) Then
                    Exit Sub
                End If

                ' Set the image info if nothing, then exit
                If IsNothing(Slot.Image) Then
                    Slot.Image = FittingImages.Images(CStr(ModuleTypeID))
                    If IsNothing(Slot.Image) Then
                        Slot.Image = New Bitmap(64, 64)
                        Slot.BackgroundImage = Nothing
                    End If
                    Slot.Image.Tag = ModuleTypeID
                    Slot.Tag = GroupName
                    Slot.Text = ModuleName

                    ' Update the slot stats
                    Call UpdateUpwellStructureStats()
                    ' Update the launcher slots if added a launcher
                    Call UpdateLauncherSlots(False, ModuleTypeID)
                    ' Done updating
                    Exit For
                End If
            End If
        Next

    End Sub

    Private Function CheckSlots(ByVal ModuleTypeID As Integer) As Boolean

        ' Only drop if over the right slot
        If RigFound(ModuleTypeID) Then
            ' They already used this rig, so don't allow
            Return False
        End If

        If ServiceFound(ModuleTypeID) Then
            ' Already have this service installed
            Return False
        End If

        ' Check launchers
        If IsMissileLauncher(ModuleTypeID) Then
            If UpwellStructureStats.LauncherSlots = 0 Then
                ' They don't have any slots left
                Return False
            End If
        End If

        ' Finally, look up if the item group is already used
        Dim CurrentRigs As List(Of Integer) = GetCurrentRigList()
        Dim rsLoader As SQLiteDataReader
        Dim MaxModules As Integer = 0
        Dim GroupID As Integer = 0
        If CurrentRigs.Count > 0 Then
            For Each Rig In CurrentRigs
                ' Get the group ID of the installed RIG
                DBCommand = New SQLiteCommand("SELECT groupID FROM INVENTORY_TYPES WHERE typeID = " & CStr(Rig), EVEDB.DBREf)
                rsLoader = DBCommand.ExecuteReader

                If rsLoader.Read() Then
                    GroupID = rsLoader.GetInt32(0)

                    ' Look up the rig max modules value, then compare to the group of this rig they want to add
                    DBCommand = New SQLiteCommand("SELECT value FROM TYPE_ATTRIBUTES AS TA, INVENTORY_TYPES AS IT WHERE attributeID = 1544 AND IT.typeID = TA.typeID AND TA.typeID = " & CStr(ModuleTypeID) & " AND groupID =" & CStr(GroupID), EVEDB.DBREf)
                    rsLoader = DBCommand.ExecuteReader

                    If rsLoader.Read() Then
                        MaxModules = CInt(rsLoader.GetDouble(0))
                    End If

                    If MaxModules = 1 Then
                        ' Don't let them add another
                        rsLoader.Close()
                        Return False
                    End If

                End If

                rsLoader.Close()
            Next
        End If

        Return True

    End Function

    ' Determines if the item is a missile launcher or not to adjust weapon slots
    Private Function IsMissileLauncher(TypeID As Integer) As Boolean
        Dim SQL As String = String.Format("SELECT * FROM type_effects WHERe typeid = {0} AND effectID = {1}", CStr(TypeID), UsesMissilesEffectID)
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        If rsReader.Read() Then
            ' Found it
            rsReader.Close()
            Return True
        Else
            rsReader.Close()
            Return False
        End If

    End Function

    ' Sees if the rig is already used or not
    Private Function RigFound(TypeID As Integer) As Boolean
        If GetCurrentRigList.Contains(TypeID) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function GetCurrentRigList() As List(Of Integer)
        Dim CurrentRigTypes As New List(Of Integer)

        If Not IsNothing(RigSlot1.Image) Then
            CurrentRigTypes.Add(CInt(RigSlot1.Image.Tag))
        End If
        If Not IsNothing(RigSlot2.Image) Then
            CurrentRigTypes.Add(CInt(RigSlot2.Image.Tag))
        End If
        If Not IsNothing(RigSlot3.Image) Then
            CurrentRigTypes.Add(CInt(RigSlot3.Image.Tag))
        End If

        Return CurrentRigTypes

    End Function

    ' See if the service is already used or not
    Private Function ServiceFound(TypeID As Integer) As Boolean
        Dim CurrentServiceTypes As New List(Of Integer)

        If Not IsNothing(ServiceSlot1.Image) Then
            CurrentServiceTypes.Add(CInt(ServiceSlot1.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot2.Image) Then
            CurrentServiceTypes.Add(CInt(ServiceSlot2.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot3.Image) Then
            CurrentServiceTypes.Add(CInt(ServiceSlot3.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot4.Image) Then
            CurrentServiceTypes.Add(CInt(ServiceSlot4.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot5.Image) Then
            CurrentServiceTypes.Add(CInt(ServiceSlot5.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot6.Image) Then
            CurrentServiceTypes.Add(CInt(ServiceSlot6.Image.Tag))
        End If

        If CurrentServiceTypes.Contains(TypeID) Then
            Return True
        Else
            '' Special case, check if they have a research lab loaded already, only allow one
            'If CurrentServiceTypes.Contains(ServiceResearchLabI) And TypeID = ServiceHyasyodaLab Or
            '        CurrentServiceTypes.Contains(ServiceHyasyodaLab) And TypeID = ServiceResearchLabI Then
            '    Return True
            'Else
            Return False
            'End If
        End If

    End Function

    ' Loads up the structure and modules with it
    Private Sub LoadStructure(ByVal SentUSName As String)
        Dim SQL As String = ""
        Dim rsStructure As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        Application.UseWaitCursor = True
        Me.Enabled = False

        ' Load the structure first
        UpwellStructureName = SentUSName

        ' First get the data to use
        SelectedUpwellStructure = GetCitadelData(SentUSName)
        ' Set the combo text
        RemoveHandler cmbUpwellStructureName.SelectedIndexChanged, AddressOf cmbUpwellStructureName_SelectedIndexChanged
        cmbUpwellStructureName.Text = SelectedUpwellStructure.Name
        AddHandler cmbUpwellStructureName.SelectedIndexChanged, AddressOf cmbUpwellStructureName_SelectedIndexChanged

        ' Load the image
        Call LoadStructureRenderImage()
        ' Refresh the items list
        Call UpdateFittingImages()
        ' Set the slots
        Call UpdateCitadelSlots()
        ' Set the stats
        Call LoadUpwellStuctureStats()
        ' Reset bonus window
        Call UpdateUpwellStructureBonuses()

        ' Now load up the modules if any are saved for this structure
        SQL = "SELECT INSTALLED_MODULE_ID FROM UPWELL_STRUCTURES_INSTALLED_MODULES, INVENTORY_TYPES "
        SQL &= "WHERE UPWELL_STRUCTURES_INSTALLED_MODULES.FACILITY_ID = INVENTORY_TYPES.typeID "
        SQL &= "AND FACILITY_VIEW = {0} And PRODUCTION_TYPE = {1} And CHARACTER_ID = {2} And SOLAR_SYSTEM_ID = {3} And typeName = '{4}'"

        DBCommand = New SQLiteCommand(String.Format(SQL, CInt(SelectedStructureLocation), CInt(SelectedFacilityProductionType), SelectedCharacterID, SelectedSolarSystemID, FormatDBString(SentUSName)), EVEDB.DBREf)
        rsStructure = DBCommand.ExecuteReader

        InstalledModules = New List(Of Integer)

        If rsStructure.HasRows() Then
            ' Need to load each module
            While rsStructure.Read
                InstalledModules.Add(rsStructure.GetInt32(0))
            End While
        End If

        If FirstModuleLoad Then
            ' Reset this so it won't load anymore unless we save this set
            FirstModuleLoad = False
        End If

        rsStructure.Close()
        DBCommand = Nothing

        ' Now fill the slots with the modules in the list
        For Each typeID In InstalledModules

            If CheckSlots(typeID) Then
                SQL = "SELECT typeName, INVENTORY_TYPES.groupID, groupName, "
                SQL &= "CASE WHEN effectID IS NULL THEN -1 ELSE effectID END AS EffID, "
                SQL &= "CASE WHEN value IS NULL THEN -1 ELSE value END AS RIG_SIZE "
                SQL &= "FROM INVENTORY_GROUPS, INVENTORY_TYPES "
                SQL &= "LEFT JOIN TYPE_EFFECTS ON INVENTORY_TYPES.typeID = TYPE_EFFECTS.typeID AND effectID IN (12,13,11) "
                SQL &= "LEFT JOIN TYPE_ATTRIBUTES ON INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID "
                SQL &= "AND attributeID = " & CStr(ItemAttributes.rigSize) & " "
                SQL &= "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID And ABS(categoryID) = 66 " ' Rigs are -66
                SQL &= "AND INVENTORY_TYPES.published <> 0 "
                SQL &= "AND INVENTORY_TYPES.typeID = " & CStr(typeID)

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsStructure = DBCommand.ExecuteReader

                If rsStructure.Read() Then
                    Dim ModuleTypeID As Integer = typeID
                    Dim ModuleName As String = rsStructure.GetString(0)
                    Dim GroupName As String = ""
                    Dim GroupTag As String = ""

                    Dim GroupID As Integer = rsStructure.GetInt32(1)
                    Dim ModuleGroupName As String = rsStructure.GetString(2)
                    Dim EffID As Integer = rsStructure.GetInt32(3)
                    Dim RigCheck As Integer = CInt(rsStructure.GetValue(4))
                    Dim Rig As Boolean

                    If RigCheck = -1 Then
                        Rig = False
                    Else
                        Rig = True
                    End If

                    If GroupID = ServiceType.Citadel Or GroupID = ServiceType.Engineering Or GroupID = ServiceType.Resource Or GroupID = ServiceType.MoonDrill Then
                        GroupName = "ServiceSlots"
                        GroupTag = "ServiceSlot"
                    ElseIf EffID = SlotSizes.HighSlot Then
                        GroupName = "HighSlots"
                        GroupTag = "HighSlot"
                    ElseIf EffID = SlotSizes.MediumSlot Then
                        GroupName = "MidSlots"
                        GroupTag = "MidSlot"
                    ElseIf EffID = SlotSizes.LowSlot Then
                        GroupName = "LowSlots"
                        GroupTag = "LowSlot"
                    Else
                        ' Rigs
                        If ModuleGroupName.Contains("Combat") Then
                            GroupName = "CombatRigs"
                            GroupTag = "RigSlot"
                        ElseIf ModuleGroupName.Contains("Reprocessing") Or ModuleGroupName.Contains("Grading") Then
                            GroupName = "ReprocessingRigs"
                            GroupTag = "RigSlot"
                        ElseIf ModuleGroupName.Contains("Engineering") Then
                            GroupName = "EngineeringRigs"
                            GroupTag = "RigSlot"
                        ElseIf ModuleGroupName.Contains("Reactor") Then
                            GroupName = "ReactionRigs"
                            GroupTag = "RigSlot"
                        ElseIf ModuleGroupName.Contains("Drilling") Then
                            GroupName = "DrillingRigs"
                            GroupTag = "RigSlot"
                        End If
                    End If

                    ' Now add the image to an image slot
                    Call LoadImageInFreeSlot(ModuleTypeID, ModuleName, GroupTag, GroupName)

                End If
            End If
        Next

        Application.UseWaitCursor = False
        Me.Enabled = True

    End Sub

    ' Strips all modules and services from the fitting
    Private Sub StripFitting()

        HighSlot1.Image = Nothing
        HighSlot2.Image = Nothing
        HighSlot3.Image = Nothing
        HighSlot4.Image = Nothing
        HighSlot5.Image = Nothing
        HighSlot6.Image = Nothing
        HighSlot7.Image = Nothing
        HighSlot8.Image = Nothing

        MidSlot1.Image = Nothing
        MidSlot2.Image = Nothing
        MidSlot3.Image = Nothing
        MidSlot4.Image = Nothing
        MidSlot5.Image = Nothing
        MidSlot6.Image = Nothing
        MidSlot7.Image = Nothing
        MidSlot8.Image = Nothing

        LowSlot1.Image = Nothing
        LowSlot2.Image = Nothing
        LowSlot3.Image = Nothing
        LowSlot4.Image = Nothing
        LowSlot5.Image = Nothing
        LowSlot6.Image = Nothing
        LowSlot7.Image = Nothing
        LowSlot8.Image = Nothing

        ServiceSlot1.Image = Nothing
        ServiceSlot2.Image = Nothing
        ServiceSlot3.Image = Nothing
        ServiceSlot4.Image = Nothing
        ServiceSlot5.Image = Nothing
        ServiceSlot6.Image = Nothing

        RigSlot1.Image = Nothing
        RigSlot2.Image = Nothing
        RigSlot3.Image = Nothing

        ' Reset bonus window
        Call UpdateUpwellStructureBonuses()

        ' init the upwell structure stats
        Call LoadUpwellStuctureStats()

    End Sub

    ' Load the image into the background
    Private Sub LoadStructureRenderImage()

        For Each UPWStructure In StructureDBDataList
            ' Look for the name and then load the render image from the typeID (should be in images folder)
            If UPWStructure.Name = cmbUpwellStructureName.Text Then
                If File.Exists(Path.Combine(UserImagePath, CStr(UPWStructure.TypeID) & ".png")) Then
                    StructurePicture.Image = Image.FromFile(Path.Combine(UserImagePath, UPWStructure.TypeID & ".png"))
                Else
                    StructurePicture.Image = Nothing
                End If
                Exit For
            End If
        Next

        StructurePicture.Refresh()
        Application.DoEvents()

    End Sub

    ' Gets and returns the upwell structure data
    Private Function GetCitadelData(ByVal LookupName As String) As UpwellStructureDBData
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        SQL = "Select typeID, groupID FROM INVENTORY_TYPES "
        SQL &= "WHERE INVENTORY_TYPES.published <> 0 And typeName = '" & FormatDBString(LookupName) & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        If rsReader.Read() Then
            Dim TempData As UpwellStructureDBData

            TempData.TypeID = rsReader.GetInt32(0)
            TempData.Name = LookupName
            TempData.GroupID = rsReader.GetInt32(1)
            rsReader.Close()

            Return TempData

        Else
            Return Nothing
        End If

    End Function

    ' Clear and Set the slots to match the upwell structure we are using
    Private Sub UpdateCitadelSlots()
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand
        Dim AID As Integer

        ' Query all the stats for the selected Upwell Structure and process slots
        SQL = "Select attributeID, value "
        SQL &= "FROM TYPE_ATTRIBUTES, INVENTORY_TYPES "
        SQL &= "WHERE attributeID In (" & ItemAttributes.hiSlots & "," & ItemAttributes.medSlots & "," & ItemAttributes.lowSlots & "," & ItemAttributes.serviceSlots & "," & ItemAttributes.rigSlots & ") "
        SQL &= "And INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID And typeName = '" & FormatDBString(cmbUpwellStructureName.Text) & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        While rsReader.Read()
            AID = rsReader.GetInt32(0)
            If AID = ItemAttributes.hiSlots Then
                Call SetHighSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.medSlots Then
                Call SetMidSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.lowSlots Then
                Call SetLowSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.rigSlots Then
                Call SetRigSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.serviceSlots Then
                Call SetServiceSlots(CInt(rsReader.GetValue(1)))
            End If
        End While

        rsReader.Close()

    End Sub

    ' Updates the stats after a module is chosen
    Private Sub LoadUpwellStuctureStats(Optional IgnoreLabelUpdate As Boolean = False)
        Dim Stats As New List(Of AttributeRecord)
        Dim AttributesLookup As New EVEAttributes

        ' Get all the stats for the upwell structure 
        Stats = AttributesLookup.GetAttributes(SelectedUpwellStructure.Name)

        ' Loop through and get the stuff we want, save it locally for update
        For Each Stat In Stats
            Select Case Stat.ID
                Case ItemAttributes.cpuOutput
                    UpwellStructureStats.MaxCPU = Stat.Value
                    UpwellStructureStats.CPU = Stat.Value
                Case ItemAttributes.powerOutput
                    UpwellStructureStats.MaxPG = Stat.Value
                    UpwellStructureStats.PG = Stat.Value
                Case ItemAttributes.upgradeCapacity ' Calibration
                    UpwellStructureStats.MaxCalibration = Stat.Value
                    UpwellStructureStats.Calibration = Stat.Value
                Case ItemAttributes.capacitorCapacity
                    UpwellStructureStats.Capacitor = Stat.Value
                    UpwellStructureStats.MaxCapacitor = Stat.Value
                Case ItemAttributes.rechargeRate
                    UpwellStructureStats.CapacitorRechargeRate = 100
                    UpwellStructureStats.BaseCapRechargeRate = Stat.Value
                Case ItemAttributes.structureServiceRoleBonus
                    UpwellStructureStats.FuelBonus = Stat.Value
                Case ItemAttributes.launcherSlotsLeft
                    If Not IgnoreLabelUpdate Then
                        ' Only update this if we are updating the label too
                        UpwellStructureStats.LauncherSlots = CInt(Stat.Value)
                    End If
            End Select
        Next

        ' Fuel is always 0 to start with no limit
        UpwellStructureStats.ServiceModuleFuelBPH = 0
        UpwellStructureStats.OnlineFuelAmount = 0

        ' Update the stats
        If Not IgnoreLabelUpdate Then
            Call UpdateUpwellStructureStatLabels()
        End If

    End Sub

    ' Updates the label stats of the upwell structure to include any items selected and installed
    Private Sub UpdateUpwellStructureStatLabels()

        ' Update the labels
        lblCPU.Text = FormatNumber(UpwellStructureStats.CPU) & " / " & FormatNumber(UpwellStructureStats.MaxCPU)
        If UpwellStructureStats.CPU < 0 Then
            lblCPU.ForeColor = Color.Red
        Else
            lblCPU.ForeColor = Color.Black
        End If

        lblPowerGrid.Text = FormatNumber(UpwellStructureStats.PG) & " / " & FormatNumber(UpwellStructureStats.MaxPG)
        If UpwellStructureStats.PG < 0 Then
            lblPowerGrid.ForeColor = Color.Red
        Else
            lblPowerGrid.ForeColor = Color.Black
        End If

        lblCalibration.Text = FormatNumber(UpwellStructureStats.Calibration) & " / " & FormatNumber(UpwellStructureStats.MaxCalibration)
        If UpwellStructureStats.Calibration < 0 Then
            lblCalibration.ForeColor = Color.Red
        Else
            lblCalibration.ForeColor = Color.Black
        End If

        lblCapacitor.Text = FormatNumber(UpwellStructureStats.Capacitor) & " / " & FormatNumber(UpwellStructureStats.MaxCapacitor)
        If UpwellStructureStats.Capacitor < 0 Then
            lblCapacitor.ForeColor = Color.Red
        Else
            lblCapacitor.ForeColor = Color.Black
        End If

        lblLauncherSlots.Text = "Launcher Slots: " & CStr(UpwellStructureStats.LauncherSlots)

        ' Update the fuel costs label
        Call UpdateFuelCostLabels()

    End Sub

    Private Sub UpdateUpwellStructureStats()
        Dim InstalledSlots As New List(Of StructureModule)
        Dim Attributes As New List(Of AttributeRecord)
        Dim AttribLookup As New EVEAttributes
        Dim FuelBlocks As Integer = 0

        InstalledSlots = GetInstalledSlots()

        ' Reset the totals each time before updating
        Call LoadUpwellStuctureStats(True)

        InstalledModules = New List(Of Integer)

        For Each Item In InstalledSlots
            ' Look up the attributes for each slot and update the stats we want
            Attributes = AttribLookup.GetAttributes(Item.typeID)

            ' insert into the installed modules to reset new list
            InstalledModules.Add(Item.typeID)

            For Each Attribute In Attributes
                Select Case Attribute.ID
                    Case ItemAttributes.power
                        UpwellStructureStats.PG -= Attribute.Value
                    Case ItemAttributes.cpu
                        UpwellStructureStats.CPU -= Attribute.Value
                    Case ItemAttributes.capacitorNeed
                        UpwellStructureStats.Capacitor -= Attribute.Value
                    Case ItemAttributes.upgradeCost ' Calibration
                        UpwellStructureStats.Calibration -= Attribute.Value
                    Case ItemAttributes.cpuMultiplier
                        UpwellStructureStats.MaxCPU = UpwellStructureStats.MaxCPU * Attribute.Value
                    Case ItemAttributes.powerOutputMultiplier
                        UpwellStructureStats.MaxPG = UpwellStructureStats.MaxPG * Attribute.Value
                    Case ItemAttributes.serviceModuleFuelAmount
                        ' Apply fuel bonus for this type of structure
                        FuelBlocks = CInt(Attribute.Value)
                        If FuelBonusApplies(Item.typeID) Then
                            FuelBlocks = CInt(Math.Floor(FuelBlocks * (1 + (UpwellStructureStats.FuelBonus / 100))))
                        End If
                        UpwellStructureStats.ServiceModuleFuelBPH += FuelBlocks

                    Case ItemAttributes.serviceModuleFuelOnlineAmount
                        UpwellStructureStats.OnlineFuelAmount += CInt(Attribute.Value)
                End Select
            Next
        Next

        ' Update the stat labels
        Call UpdateUpwellStructureStatLabels()

        ' Update the bonuses from items installed
        Call UpdateUpwellStructureBonuses()

    End Sub

    Private Function FuelBonusApplies(StructureModuleID As Integer) As Boolean
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand
        Dim GroupID As Integer

        Try
            SQL = "SELECT groupID FROM INVENTORY_TYPES WHERE typeID = " & StructureModuleID

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsReader = DBCommand.ExecuteReader
            rsReader.Read()
            GroupID = rsReader.GetInt32(0)
            rsReader.Close()

            Select Case SelectedUpwellStructure.GroupID
                Case StructureGroup.Citadel
                    If GroupID = ServiceType.Citadel Then
                        Return True
                    End If
                Case StructureGroup.Engineering
                    If GroupID = ServiceType.Engineering Then
                        Return True
                    End If
                Case StructureGroup.Refinery ' no bonus for moon drill
                    If GroupID = ServiceType.Resource Then
                        Return True
                    End If
            End Select
        Catch ex As Exception
            Return False
        End Try

        Return False

    End Function

    ' If true, increments the launcher slots, else decrements
    Private Sub UpdateLauncherSlots(ByVal Increment As Boolean, ByVal ModuleTypeID As Integer)
        ' Update number of launchers
        If IsMissileLauncher(ModuleTypeID) Then
            If Not Increment Then
                If UpwellStructureStats.LauncherSlots > 0 Then
                    UpwellStructureStats.LauncherSlots -= 1
                End If

            Else
                UpwellStructureStats.LauncherSlots += 1
            End If
        End If

        lblLauncherSlots.Text = "Launcher Slots: " & CStr(UpwellStructureStats.LauncherSlots)

    End Sub

    ' Returns the list of moduleIDs installed in the upwell structure
    Private Function GetInstalledSlots() As List(Of StructureModule)
        Dim ReturnItems As New List(Of StructureModule)
        Dim Entry As StructureModule

        ' Go through all slots and return the typeIDs (saved in tag of image) for each installed item
        If Not IsNothing(HighSlot1.Image) Then
            Entry.typeID = CInt(HighSlot1.Image.Tag)
            Entry.moduleType = CStr(HighSlot1.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot2.Image) Then
            Entry.typeID = CInt(HighSlot2.Image.Tag)
            Entry.moduleType = CStr(HighSlot2.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot3.Image) Then
            Entry.typeID = CInt(HighSlot3.Image.Tag)
            Entry.moduleType = CStr(HighSlot3.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot4.Image) Then
            Entry.typeID = CInt(HighSlot4.Image.Tag)
            Entry.moduleType = CStr(HighSlot4.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot5.Image) Then
            Entry.typeID = CInt(HighSlot5.Image.Tag)
            Entry.moduleType = CStr(HighSlot5.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot6.Image) Then
            Entry.typeID = CInt(HighSlot6.Image.Tag)
            Entry.moduleType = CStr(HighSlot6.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot7.Image) Then
            Entry.typeID = CInt(HighSlot7.Image.Tag)
            Entry.moduleType = CStr(HighSlot7.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(HighSlot8.Image) Then
            Entry.typeID = CInt(HighSlot8.Image.Tag)
            Entry.moduleType = CStr(HighSlot8.Tag)
            ReturnItems.Add(Entry)
        End If

        If Not IsNothing(MidSlot1.Image) Then
            Entry.typeID = CInt(MidSlot1.Image.Tag)
            Entry.moduleType = CStr(MidSlot1.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot2.Image) Then
            Entry.typeID = CInt(MidSlot2.Image.Tag)
            Entry.moduleType = CStr(MidSlot2.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot3.Image) Then
            Entry.typeID = CInt(MidSlot3.Image.Tag)
            Entry.moduleType = CStr(MidSlot3.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot4.Image) Then
            Entry.typeID = CInt(MidSlot4.Image.Tag)
            Entry.moduleType = CStr(MidSlot4.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot5.Image) Then
            Entry.typeID = CInt(MidSlot5.Image.Tag)
            Entry.moduleType = CStr(MidSlot5.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot6.Image) Then
            Entry.typeID = CInt(MidSlot6.Image.Tag)
            Entry.moduleType = CStr(MidSlot6.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot7.Image) Then
            Entry.typeID = CInt(MidSlot7.Image.Tag)
            Entry.moduleType = CStr(MidSlot7.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(MidSlot8.Image) Then
            Entry.typeID = CInt(MidSlot8.Image.Tag)
            Entry.moduleType = CStr(MidSlot8.Tag)
            ReturnItems.Add(Entry)
        End If

        If Not IsNothing(LowSlot1.Image) Then
            Entry.typeID = CInt(LowSlot1.Image.Tag)
            Entry.moduleType = CStr(LowSlot1.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot2.Image) Then
            Entry.typeID = CInt(LowSlot2.Image.Tag)
            Entry.moduleType = CStr(LowSlot2.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot3.Image) Then
            Entry.typeID = CInt(LowSlot3.Image.Tag)
            Entry.moduleType = CStr(LowSlot3.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot4.Image) Then
            Entry.typeID = CInt(HighSlot1.Image.Tag)
            Entry.moduleType = CStr(HighSlot1.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot5.Image) Then
            Entry.typeID = CInt(LowSlot5.Image.Tag)
            Entry.moduleType = CStr(LowSlot5.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot6.Image) Then
            Entry.typeID = CInt(LowSlot6.Image.Tag)
            Entry.moduleType = CStr(LowSlot6.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot7.Image) Then
            Entry.typeID = CInt(LowSlot7.Image.Tag)
            Entry.moduleType = CStr(LowSlot7.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(LowSlot8.Image) Then
            Entry.typeID = CInt(LowSlot8.Image.Tag)
            Entry.moduleType = CStr(LowSlot8.Tag)
            ReturnItems.Add(Entry)
        End If

        ' Rigs!
        If Not IsNothing(RigSlot1.Image) Then
            Entry.typeID = CInt(RigSlot1.Image.Tag)
            Entry.moduleType = CStr(RigSlot1.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(RigSlot2.Image) Then
            Entry.typeID = CInt(RigSlot2.Image.Tag)
            Entry.moduleType = CStr(RigSlot2.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(RigSlot3.Image) Then
            Entry.typeID = CInt(RigSlot3.Image.Tag)
            Entry.moduleType = CStr(RigSlot3.Tag)
            ReturnItems.Add(Entry)
        End If

        If Not IsNothing(ServiceSlot1.Image) Then
            Entry.typeID = CInt(ServiceSlot1.Image.Tag)
            Entry.moduleType = CStr(ServiceSlot1.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(ServiceSlot2.Image) Then
            Entry.typeID = CInt(ServiceSlot2.Image.Tag)
            Entry.moduleType = CStr(ServiceSlot2.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(ServiceSlot3.Image) Then
            Entry.typeID = CInt(ServiceSlot3.Image.Tag)
            Entry.moduleType = CStr(ServiceSlot3.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(ServiceSlot4.Image) Then
            Entry.typeID = CInt(ServiceSlot4.Image.Tag)
            Entry.moduleType = CStr(ServiceSlot4.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(ServiceSlot5.Image) Then
            Entry.typeID = CInt(ServiceSlot5.Image.Tag)
            Entry.moduleType = CStr(ServiceSlot5.Tag)
            ReturnItems.Add(Entry)
        End If
        If Not IsNothing(ServiceSlot6.Image) Then
            Entry.typeID = CInt(ServiceSlot6.Image.Tag)
            Entry.moduleType = CStr(ServiceSlot6.Tag)
            ReturnItems.Add(Entry)
        End If

        Return ReturnItems

    End Function

    Private Function GetFuelCost(ByVal NumBlocks As Integer) As String

        'Select the type of fuel and then update the cost per hour from the text boxes
        If rbtnBuyBlocks.Checked Then
            If rbtnHeliumFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(txtHeliumFuelBlockBuyPrice.Text))
            ElseIf rbtnHydrogenFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(txtHydrogenFuelBlockBuyPrice.Text))
            ElseIf rbtnNitrogenFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(txtNitrogenFuelBlockBuyPrice.Text))
            ElseIf rbtnOxygenFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(txtOxygenFuelBlockBuyPrice.Text))
            End If
        Else
            If rbtnHeliumFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(lblHeliumFuelBlockBuildPrice.Text))
            ElseIf rbtnHydrogenFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(lblHydrogenFuelBlockBuildPrice.Text))
            ElseIf rbtnNitrogenFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(lblNitrogenFuelBlockBuildPrice.Text))
            ElseIf rbtnOxygenFuelBlock.Checked Then
                Return FormatNumber(NumBlocks * CDbl(lblOxygenFuelBlockBuildPrice.Text))
            End If
        End If

        Return ""

    End Function

    Private Sub UpdateFuelCostLabels()
        ' If they want fuel cost
        If chkIncludeFuelCosts.Checked Then
            ' Select blocks and online amount (shouldn't change)
            lblServiceModuleBPH.Text = FormatNumber(UpwellStructureStats.ServiceModuleFuelBPH, 0) & " Blocks per Hour"
            lblServiceModuleOnlineAmt.Text = FormatNumber(UpwellStructureStats.OnlineFuelAmount, 0) & " Blocks"
            lblServiceModuleFCPH.Text = GetFuelCost(UpwellStructureStats.ServiceModuleFuelBPH)
            lblServiceModuleBPD.Text = FormatNumber(UpwellStructureStats.ServiceModuleFuelBPH * 24, 0) & " Blocks per Day"
            lblFuelReductionBonus.Text = "Fuel Bonus: " & FormatPercent(UpwellStructureStats.FuelBonus / 100, 0)
        Else
            lblServiceModuleBPH.Text = "-"
            lblServiceModuleBPD.Text = "-"
            lblServiceModuleOnlineAmt.Text = "-"
            lblServiceModuleFCPH.Text = "-"
            lblFuelReductionBonus.Text = "-"
        End If
    End Sub

    ' Loads the images for fittings in the image lists
    Private Sub LoadFittingImages()
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        Try

            SQL = "SELECT typeID, typeName FROM INVENTORY_TYPES, INVENTORY_GROUPS "
            SQL &= "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID AND ABS(categoryID) = 66 " ' I save rigs as -66
            SQL &= "AND INVENTORY_TYPES.published <> 0"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsReader = DBCommand.ExecuteReader

            Dim myImage As Image
            Dim typeID As String
            Dim typeName As String

            While rsReader.Read()
                ' Add to the image list, and put in view with names
                typeID = CStr(rsReader.GetInt32(0))
                typeName = rsReader.GetString(1)
                If File.Exists(Path.Combine(UserImagePath, typeID & "_64.png")) Then
                    myImage = Image.FromFile(Path.Combine(UserImagePath, typeID & "_64.png"))

                    Call FittingImages.Images.Add(typeID, myImage)
                End If
            End While

            rsReader.Close()

        Catch ex As Exception
            Application.DoEvents()
        End Try
    End Sub

    Private Function ModuleTypeSelected() As Boolean
        If chkItemViewTypeHigh.Checked Or chkItemViewTypeLow.Checked Or chkItemViewTypeMedium.Checked Or
           chkItemViewTypeServices.Checked Or chkRigTypeViewCombat.Checked Or chkRigTypeViewDrilling.Checked Or
           chkRigTypeViewEngineering.Checked Or chkRigTypeViewReaction.Checked Or chkRigTypeViewReprocessing.Checked Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Updates all the fitting images based on the check boxes in the list view
    Private Sub UpdateFittingImages()

        ' Clear current images and items
        FittingListViewIcons.BeginUpdate()
        FittingListViewDetails.BeginUpdate()
        FittingListViewIcons.Items.Clear()
        FittingListViewDetails.Items.Clear()

        If ModuleTypeSelected() Then

            Dim SQL As String = ""
            Dim RigString As String = ""
            Dim SlotString As String = ""
            Dim SQLList As New List(Of String)
            Dim rsReader As SQLiteDataReader
            Dim DBCommand As SQLiteCommand
            Dim DVI As New ListViewItem

            ' query for all types of modules, rigs, and services to fit
            SQL = "SELECT INVENTORY_TYPES.typeID, INVENTORY_GROUPS.groupID, typeName, "
            SQL &= "CASE WHEN effectID IS NULL THEN -1 ELSE effectID END AS EffID, groupName, "
            SQL &= "CASE WHEN value IS NULL THEN -1 ELSE value END AS RIG_SIZE, "
            SQL &= "CASE WHEN (SELECT value FROM TYPE_ATTRIBUTES "
            SQL &= "WHERE typeID = INVENTORY_TYPES.typeID AND (attributeID = " & ItemAttributes.disallowInHighSec & " OR attributeID = " & ItemAttributes.disallowInEmpireSpace & ") "
            SQL &= ") = 1 THEN 0 ELSE 1 END AS ALLOW_IN_HS "
            SQL &= "FROM INVENTORY_GROUPS, INVENTORY_TYPES "
            SQL &= "LEFT JOIN TYPE_EFFECTS ON INVENTORY_TYPES.typeID = TYPE_EFFECTS.typeID AND effectID IN (12,13,11) "
            SQL &= "LEFT JOIN TYPE_ATTRIBUTES ON INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID "
            SQL &= "AND attributeID = " & CStr(ItemAttributes.rigSize) & " "
            SQL &= "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID And ABS(categoryID) = 66 " ' I save structure rigs as -66
            SQL &= "AND INVENTORY_TYPES.published <> 0 "

            ' Add text first
            If Trim(txtItemFilter.Text) <> "" Then
                SQL &= "And " & GetSearchText(txtItemFilter.Text, "typeName") & " "
            End If

            If chkItemViewTypeServices.Checked Then
                ' Add the sql
                Call SQLList.Add("(INVENTORY_TYPES.groupID In (" & CStr(ServiceType.Citadel) & "," & CStr(ServiceType.Engineering) & "," & CStr(ServiceType.MoonDrill) & "," & CStr(ServiceType.Resource) & ")) ")
            End If

            ' Process high, medium, and low slots together
            If chkItemViewTypeHigh.Checked Then
                SlotString &= CStr(SlotSizes.HighSlot) & ","
            End If

            If chkItemViewTypeMedium.Checked Then
                SlotString &= CStr(SlotSizes.MediumSlot) & ","
            End If

            If chkItemViewTypeLow.Checked Then
                SlotString &= CStr(SlotSizes.LowSlot) & ","
            End If

            If SlotString <> "" Then
                SlotString = SlotString.Substring(0, Len(SlotString) - 1)
                SlotString = "(EffID In (" & SlotString & "))"
                ' Add the sql
                Call SQLList.Add(SlotString)
            End If

            If chkRigTypeViewCombat.Checked Or chkRigTypeViewEngineering.Checked Or chkRigTypeViewReprocessing.Checked Or
                chkRigTypeViewDrilling.Checked Or chkRigTypeViewReaction.Checked Then
                If chkRigTypeViewCombat.Checked Then
                    Call SQLList.Add("(groupName Like '%Combat Rig%')")
                End If

                If chkRigTypeViewEngineering.Checked Then
                    Call SQLList.Add("(groupName LIKE '%Engineering Rig%')")
                End If

                If chkRigTypeViewReprocessing.Checked Then
                    Call SQLList.Add("(groupName LIKE '%Resource Rig%')")
                End If

                If chkRigTypeViewReaction.Checked Then
                    Call SQLList.Add("(groupName LIKE '%Reactor Rig%')")
                End If

                If chkRigTypeViewDrilling.Checked Then
                    Call SQLList.Add("(groupName LIKE '%Drilling Rig%')")
                End If

                Dim Attrib As New EVEAttributes

                ' Add the check for rig size to limit, -1 is the default value
                SQL &= "AND RIG_SIZE IN (-1," & CInt(Attrib.GetAttribute(SelectedUpwellStructure.TypeID, ItemAttributes.rigSize)) & ") "

            End If

            ' Set the SQL
            If SQLList.Count > 0 Then
                SQL &= "AND ("
                For Each entry In SQLList
                    SQL &= "(" & entry & ") OR "
                Next
                ' Strip last OR
                SQL = SQL.Substring(0, Len(SQL) - 4)
                SQL &= ")"
            Else
                Exit Sub
            End If

            SQL &= " ORDER BY groupName, typeName"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsReader = DBCommand.ExecuteReader

            While rsReader.Read()
                Dim GID As Integer = rsReader.GetInt32(1)
                Dim EID As Integer = rsReader.GetInt32(3)
                Dim LVI As New ListViewItem
                Dim AllowinHighSec As Boolean

                If rsReader.GetInt32(6) <> 0 Then
                    AllowinHighSec = True
                Else
                    AllowinHighSec = False
                End If

                ' Only add if it can be fit to the selected upwell structure and it meets the space requirements
                If StructureCanFitItem(SelectedUpwellStructure.TypeID, SelectedUpwellStructure.GroupID, rsReader.GetInt32(0)) And
                    ((chkHighSec.Checked = True And AllowinHighSec) Or chkHighSec.Checked = False) Then

                    '& CStr(ItemAttributes.disallowInHighSec) & ") "
                    If GID = ServiceType.Citadel Or GID = ServiceType.Engineering Or GID = ServiceType.Resource Or GID = ServiceType.MoonDrill Then
                        LVI.Group = FittingListViewIcons.Groups(0) ' 0 is services
                    ElseIf EID = SlotSizes.HighSlot Then
                        LVI.Group = FittingListViewIcons.Groups(1) ' 1 is high
                    ElseIf EID = SlotSizes.MediumSlot Then
                        LVI.Group = FittingListViewIcons.Groups(2) ' 2 is medium
                    ElseIf EID = SlotSizes.LowSlot Then
                        LVI.Group = FittingListViewIcons.Groups(3) ' 3 is low
                    Else
                        ' Rigs
                        If rsReader.GetString(4).Contains("Combat") Then
                            LVI.Group = FittingListViewIcons.Groups(4) ' 4 is Combat rigs
                        ElseIf rsReader.GetString(4).Contains("Reprocessing") Or rsReader.GetString(4).Contains("Grading") Then
                            LVI.Group = FittingListViewIcons.Groups(5) ' 5 is Reprocessing rigs
                        ElseIf rsReader.GetString(4).Contains("Engineering") Then
                            LVI.Group = FittingListViewIcons.Groups(6) ' 6 is Engineering rigs
                        ElseIf rsReader.GetString(4).Contains("Reactor") Then
                            LVI.Group = FittingListViewIcons.Groups(7) ' 7 is Reaction rigs
                        ElseIf rsReader.GetString(4).Contains("Drilling") Then
                            LVI.Group = FittingListViewIcons.Groups(8) ' 8 is Drilling rigs
                        End If
                    End If

                    ' add the image
                    LVI.ImageKey = CStr(rsReader.GetInt32(0))
                    LVI.Text = rsReader.GetString(2)
                    FittingListViewIcons.Items.Add(LVI)

                    ' Add the details list too
                    DVI = New ListViewItem(rsReader.GetString(2)) ' Name
                    DVI.SubItems.Add(LVI.Group.Name) ' Group name
                    DVI.SubItems.Add(LVI.ImageKey) ' Module type id - Hidden
                    DVI.SubItems.Add(CStr(LVI.Group.Tag)) ' Group tag - Hidden
                    Call FittingListViewDetails.Items.Add(DVI)
                End If

            End While

            ' Sort the grid
            Call ListViewColumnSorter(1, CType(FittingListViewDetails, ListView), 1, SortOrder.Ascending)

        End If

        FittingListViewDetails.EndUpdate()
        FittingListViewIcons.EndUpdate()


    End Sub

    ' Reads the attributes to see if the itemID sent can be fit to the upwell structureID sent
    Private Function StructureCanFitItem(ByVal StructureTypeID As Integer, ByVal StructureGroupID As Integer, ByVal ItemTypeID As Integer) As Boolean
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        SQL = "SELECT value AS STRUCTURE_ID FROM TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
        SQL &= "WHERE TYPE_ATTRIBUTES.typeID = {0} AND ATTRIBUTE_TYPES.attributeID = TYPE_ATTRIBUTES.attributeID "
        SQL &= "AND (attributeName LIKE 'canFitShipType%' OR attributeName LIKE 'canFitShipGroup%')"
        ' Add typeid to look up
        SQL = String.Format(SQL, ItemTypeID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        While rsReader.Read()
            Dim IDtoCheck As Integer = CInt(rsReader.GetValue(0))
            If IDtoCheck = StructureTypeID Or IDtoCheck = StructureGroupID Then
                Return True
            End If
        End While

        ' Not found
        Return False

    End Function

    ' Moves the high slots to center based on the rig slot images
    Private Sub ShiftHighSlotImages()

        ' Move the top 5 over to match the rig slot locations
        HighSlot1.Left = RigSlot2.Left
        Call AlignHighSlotsfromBase()

    End Sub

    Private Sub ResetHighSlotImages()

        ' Move the top 5 back since 6 and above won't move
        HighSlot1.Left = HighSlotBaseX
        Call AlignHighSlotsfromBase()

    End Sub

    Private Sub AlignHighSlotsfromBase()
        ' Aligns the high slots based on the first high slot position
        HighSlot2.Left = HighSlot1.Left + HighSlotSpacing + HighSlotBaseWidth
        HighSlot3.Left = HighSlot1.Left - HighSlotBaseWidth - HighSlotSpacing
        HighSlot4.Left = HighSlot2.Left + HighSlotSpacing + HighSlotBaseWidth
        HighSlot5.Left = HighSlot3.Left - HighSlotBaseWidth - HighSlotSpacing
    End Sub

    ' Moves the high slots to center based on the rig slot images
    Private Sub ShiftServiceSlotImages()

        ' Move the top 5 over to match the rig slot locations
        ServiceSlot1.Left = RigSlot2.Left
        Call AlignServiceSlotsfromBase()

    End Sub

    Private Sub ResetServiceSlotImages()

        ' Move the top 5 back since 6 and above won't move
        ServiceSlot1.Left = ServiceSlotBaseX
        Call AlignServiceSlotsfromBase()

    End Sub

    Private Sub AlignServiceSlotsfromBase()
        ' Aligns the service slots based on the first high slot position
        ServiceSlot2.Left = ServiceSlot1.Left + ServiceSlotSpacing + ServiceSlotBaseWidth
        ServiceSlot3.Left = ServiceSlot1.Left - ServiceSlotBaseWidth - ServiceSlotSpacing
        ServiceSlot4.Left = ServiceSlot2.Left + ServiceSlotSpacing + ServiceSlotBaseWidth
        ServiceSlot5.Left = ServiceSlot3.Left - ServiceSlotBaseWidth - ServiceSlotSpacing
    End Sub

    Private Sub SetHighSlots(Slots As Integer)

        ' Init slots
        HighSlot1.Visible = False
        HighSlot2.Visible = False
        HighSlot3.Visible = False
        HighSlot4.Visible = False
        HighSlot5.Visible = False
        HighSlot6.Visible = False
        HighSlot7.Visible = False
        HighSlot8.Visible = False

        HighSlot1.Image = Nothing
        HighSlot2.Image = Nothing
        HighSlot3.Image = Nothing
        HighSlot4.Image = Nothing
        HighSlot5.Image = Nothing
        HighSlot6.Image = Nothing
        HighSlot7.Image = Nothing
        HighSlot8.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    HighSlot1.Visible = True
                Case 2
                    HighSlot2.Visible = True
                Case 3
                    HighSlot3.Visible = True
                Case 4
                    HighSlot4.Visible = True
                Case 5
                    HighSlot5.Visible = True
                Case 6
                    HighSlot6.Visible = True
                Case 7
                    HighSlot7.Visible = True
                Case 8
                    HighSlot8.Visible = True
            End Select
        Next

        If Slots Mod 2 > 0 And Slots < 6 Then
            ' Move the slots if we are on the first line
            Call ShiftHighSlotImages()
        Else
            ' Reset them to the base positions
            Call ResetHighSlotImages()
        End If

    End Sub

    Private Sub SetMidSlots(Slots As Integer)

        ' Init slots
        MidSlot1.Visible = False
        MidSlot2.Visible = False
        MidSlot3.Visible = False
        MidSlot4.Visible = False
        MidSlot5.Visible = False
        MidSlot6.Visible = False
        MidSlot7.Visible = False
        MidSlot8.Visible = False

        MidSlot1.Image = Nothing
        MidSlot2.Image = Nothing
        MidSlot3.Image = Nothing
        MidSlot4.Image = Nothing
        MidSlot5.Image = Nothing
        MidSlot6.Image = Nothing
        MidSlot7.Image = Nothing
        MidSlot8.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    MidSlot1.Visible = True
                Case 2
                    MidSlot2.Visible = True
                Case 3
                    MidSlot3.Visible = True
                Case 4
                    MidSlot4.Visible = True
                Case 5
                    MidSlot5.Visible = True
                Case 6
                    MidSlot6.Visible = True
                Case 7
                    MidSlot7.Visible = True
                Case 8
                    MidSlot8.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetLowSlots(Slots As Integer)

        ' Init slots
        LowSlot1.Visible = False
        LowSlot2.Visible = False
        LowSlot3.Visible = False
        LowSlot4.Visible = False
        LowSlot5.Visible = False
        LowSlot6.Visible = False
        LowSlot7.Visible = False
        LowSlot8.Visible = False

        LowSlot1.Image = Nothing
        LowSlot2.Image = Nothing
        LowSlot3.Image = Nothing
        LowSlot4.Image = Nothing
        LowSlot5.Image = Nothing
        LowSlot6.Image = Nothing
        LowSlot7.Image = Nothing
        LowSlot8.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    LowSlot1.Visible = True
                Case 2
                    LowSlot2.Visible = True
                Case 3
                    LowSlot3.Visible = True
                Case 4
                    LowSlot4.Visible = True
                Case 5
                    LowSlot5.Visible = True
                Case 6
                    LowSlot6.Visible = True
                Case 7
                    LowSlot7.Visible = True
                Case 8
                    LowSlot8.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetRigSlots(Slots As Integer)

        ' Init slots
        RigSlot1.Visible = False
        RigSlot2.Visible = False
        RigSlot3.Visible = False

        RigSlot1.Image = Nothing
        RigSlot2.Image = Nothing
        RigSlot3.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    RigSlot1.Visible = True
                Case 2
                    RigSlot2.Visible = True
                Case 3
                    RigSlot3.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetServiceSlots(Slots As Integer)

        ' Init slots
        ServiceSlot1.Visible = False
        ServiceSlot2.Visible = False
        ServiceSlot3.Visible = False
        ServiceSlot4.Visible = False
        ServiceSlot5.Visible = False
        ServiceSlot6.Visible = False

        ServiceSlot1.Image = Nothing
        ServiceSlot2.Image = Nothing
        ServiceSlot3.Image = Nothing
        ServiceSlot4.Image = Nothing
        ServiceSlot5.Image = Nothing
        ServiceSlot6.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    ServiceSlot1.Visible = True
                Case 2
                    ServiceSlot2.Visible = True
                Case 3
                    ServiceSlot3.Visible = True
                Case 4
                    ServiceSlot4.Visible = True
                Case 5
                    ServiceSlot5.Visible = True
                Case 6
                    ServiceSlot6.Visible = True
            End Select
        Next

        If Slots Mod 2 > 0 And Slots < 6 Then
            ' Move the slots if we are on the first line
            Call ShiftServiceSlotImages()
        Else
            ' Reset them to the base positions
            Call ResetServiceSlotImages()
        End If

    End Sub

    Private Sub btnSaveFitting_Click(sender As Object, e As EventArgs) Handles btnSaveFitting.Click

        Try
            Dim LocationList As New List(Of Integer)
            Dim SQL As String
            Dim LID As Integer

            EVEDB.BeginSQLiteTransaction()

            ' See what type of character ID
            Dim CharID As Long = 0
            If UserApplicationSettings.SaveFacilitiesbyChar Then
                CharID = SelectedCharacterID
            Else
                CharID = CommonSavedFacilitiesID
            End If

            If UserApplicationSettings.ShareSavedFacilities Then
                ' Need to get each location for saving
                For Each LID In System.Enum.GetValues(GetType(ProgramLocation))
                    Call LocationList.Add(LID)
                Next
            Else
                ' Just use the one sent
                Call LocationList.Add(SelectedStructureLocation)
            End If

            ' Delete everything first, then insert the new records
            For Each LID In LocationList
                EVEDB.ExecuteNonQuerySQL(String.Format("DELETE FROM UPWELL_STRUCTURES_INSTALLED_MODULES WHERE CHARACTER_ID = {0} 
                                                        AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_VIEW = {3} AND FACILITY_ID = {4} ",
                                                        CharID, CStr(SelectedFacilityProductionType), SelectedSolarSystemID, CStr(LID), SelectedUpwellStructure.TypeID))
            Next

            ' Insert all the modules on the facility
            Dim Modules As New List(Of StructureModule)
            Modules = GetInstalledSlots()
            For Each InstalledModule In Modules
                For Each LID In LocationList
                    SQL = String.Format("INSERT INTO UPWELL_STRUCTURES_INSTALLED_MODULES VALUES({0},{1},{2},{3},{4},{5})",
                    SelectedCharacterID, CStr(SelectedFacilityProductionType), SelectedSolarSystemID, CStr(LID), SelectedUpwellStructure.TypeID, InstalledModule.typeID)
                    EVEDB.ExecuteNonQuerySQL(SQL)
                Next
            Next

            ' If there are rigs fit to this, then delete any saved multipliers they have saved manually
            If Not IsNothing(RigSlot1.Image) Or Not IsNothing(RigSlot2.Image) Or Not IsNothing(RigSlot3.Image) Then
                ' Don't change tax value
                SQL = "UPDATE SAVED_FACILITIES SET MATERIAL_MULTIPLIER = NULL, TIME_MULTIPLIER = NULL, COST_MULTIPLIER = NULL "
                SQL &= "WHERE CHARACTER_ID = {0} AND PRODUCTION_TYPE = {1} AND SOLAR_SYSTEM_ID = {2} AND FACILITY_VIEW = {3} AND FACILITY_TYPE_ID = {4}"
                EVEDB.ExecuteNonQuerySQL(String.Format(SQL, CharID, CStr(SelectedFacilityProductionType), SelectedSolarSystemID, CStr(SelectedStructureLocation), CStr(FacilityTypes.UpwellStructure)))
                ' Reset the manual flags in the facility
                ResetManualEntries = True
            End If

            EVEDB.CommitSQLiteTransaction()

            ' Since they saved this, go ahead and save the facility too
            SavedFacility = True

            MsgBox("Facility Saved", vbInformation, Application.ProductName)

        Catch ex As Exception
            EVEDB.RollbackSQLiteTransaction()
            MsgBox("Facility failed to save: " & ex.Message, vbExclamation, Application.ProductName)
        End Try

    End Sub

    ' Loads up the bonuses from the modules installed in the list
    Private Sub UpdateUpwellStructureBonuses()
        Dim SQL As String
        Dim SystemSecurityBonus As Double
        Dim rsReader As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        Dim BonusList As ListViewItem
        lstUpwellStructureBonuses.Items.Clear() ' Clear all items each time

        If Application.OpenForms().OfType(Of frmBonusPopout).Any Then
            ' if the form is active, clear any items first in case we want to add them later
            Call frmPopout.lstUpwellStructureBonuses.Items.Clear()
        End If

        ' Loop through each module installed and get a total of all the stats affected and how
        For Each InstalledModule In GetInstalledSlots()
            ' Only look at rig bonuses for now
            If InstalledModule.moduleType.Contains("Rig") Then
                ' Get the security modifier first - set to 1 if not found
                SQL = "SELECT value FROM TYPE_ATTRIBUTES WHERE typeID = {0} AND attributeID = "
                If chkHighSec.Checked Then
                    SQL &= CStr(ItemAttributes.hiSecModifier) & " "
                ElseIf chkLowSec.Checked Then
                    SQL &= CStr(ItemAttributes.lowSecModifier) & " "
                ElseIf chkNullSec.Checked Then
                    SQL &= CStr(ItemAttributes.nullSecModifier) & " "
                End If

                DBCommand = New SQLiteCommand(String.Format(SQL, InstalledModule.typeID), EVEDB.DBREf)
                rsReader = DBCommand.ExecuteReader

                If rsReader.Read Then
                    ' Save bonus for later application
                    SystemSecurityBonus = rsReader.GetDouble(0)
                Else
                    SystemSecurityBonus = 1
                End If

                ' Engineering Rigs
                Select Case InstalledModule.moduleType
                    Case "EngineeringRigs"
                        SQL = "SELECT CASE WHEN groupName IS NULL THEN categoryName ELSE groupname END AS BONUS_APPLIES_TO, "
                        SQL &= "INDUSTRY_ACTIVITIES.activityName AS ACTIVITY, "
                        SQL &= "AT.displayNameID AS BONUS_NAME, "
                        SQL &= "value / 100 * " & CStr(SystemSecurityBonus) & " AS BONUS, "
                        SQL &= "typeName AS BONUS_SOURCE "
                        SQL &= "FROM TYPE_ATTRIBUTES AS TA, ENGINEERING_RIG_BONUSES AS ERB, INVENTORY_TYPES AS IT, ATTRIBUTE_TYPES AS AT "
                        SQL &= "LEFT JOIN INVENTORY_GROUPS ON ERB.groupID = INVENTORY_GROUPS.groupID "
                        SQL &= "LEFT JOIN INVENTORY_CATEGORIES ON ERB.categoryID = INVENTORY_CATEGORIES.categoryID "
                        SQL &= "LEFT JOIN INDUSTRY_ACTIVITIES ON ERB.activityID = INDUSTRY_ACTIVITIES.activityID "
                        SQL &= "WHERE TA.attributeID = AT.attributeID AND ERB.typeID = IT.typeID AND TA.typeID = IT.typeID "
                        SQL &= "AND TA.attributeID IN (SELECT attributeID FROM ATTRIBUTE_TYPES WHERE attributeName LIKE 'attributeEngRig%') "

                        ' Only include this if it's a Thukker array, else don't limit
                        DBCommand = New SQLiteCommand("SELECT 'X' FROM INVENTORY_TYPES WHERE typeName LIKE '%Standup%-Set Thukker%' AND typeName NOT LIKE '%Blueprint' AND typeID = " & CStr(InstalledModule.typeID), EVEDB.DBREf)
                        rsCheck = DBCommand.ExecuteReader

                        If rsCheck.Read Then
                            SQL &= "AND (ERB.groupID NOT IN (873,913) OR ERB.groupID IS NULL) "
                        End If

                        SQL &= "AND BONUS <> 0 AND TA.typeID = {0} "
                        ' The rest is for thukker bonus (if it applies)
                        SQL &= "UNION "
                        SQL &= "SELECT CASE WHEN groupName IS NULL THEN categoryName ELSE groupname END AS BONUS_APPLIES_TO, "
                        SQL &= "INDUSTRY_ACTIVITIES.activityName AS ACTIVITY, "
                        SQL &= "AT.displayNameID AS BONUS_NAME, "
                        SQL &= "value/ 100 * " & CStr(SystemSecurityBonus) & " AS BONUS, "
                        SQL &= "typeName AS BONUS_SOURCE "
                        SQL &= "FROM TYPE_ATTRIBUTES AS TA, ENGINEERING_RIG_BONUSES AS ERB, INVENTORY_TYPES AS IT, ATTRIBUTE_TYPES AS AT "
                        SQL &= "LEFT JOIN INVENTORY_GROUPS ON ERB.groupID = INVENTORY_GROUPS.groupID "
                        SQL &= "LEFT JOIN INVENTORY_CATEGORIES ON ERB.categoryID = INVENTORY_CATEGORIES.categoryID "
                        SQL &= "LEFT JOIN INDUSTRY_ACTIVITIES ON ERB.activityID = INDUSTRY_ACTIVITIES.activityID "
                        SQL &= "WHERE TA.attributeID = AT.attributeID AND ERB.typeID = IT.typeID AND TA.typeID = IT.typeID "
                        SQL &= "AND TA.attributeID IN (SELECT attributeID FROM ATTRIBUTE_TYPES WHERE attributeName LIKE 'attributeThukkerEngRig%' OR attributeName LIKE 'attributeEngRig%') "
                        SQL &= "AND TA.attributeID <> 2594 AND ERB.groupID IN (873,913) "

                    Case "CombatRigs"

                        SQL = "SELECT 'Combat' AS BONUS_APPLIES_TO, "
                        SQL &= "'Combat' AS ACTIVITY, "
                        SQL &= "AT.displayNameID AS BONUS_NAME, "
                        SQL &= "value/ 100 * " & CStr(SystemSecurityBonus) & " AS BONUS, "
                        SQL &= "typeName AS BONUS_SOURCE "
                        SQL &= "FROM TYPE_ATTRIBUTES AS TA, INVENTORY_TYPES AS IT, ATTRIBUTE_TYPES AS AT "
                        SQL &= "WHERE TA.attributeID = AT.attributeID "
                        SQL &= "AND TA.typeID = IT.typeID  "
                        SQL &= "AND TA.attributeID IN (SELECT attributeID FROM ATTRIBUTE_TYPES WHERE attributeName LIKE 'structureRig%') "

                    Case "ReprocessingRigs"

                        SQL = "SELECT 'Refining' AS BONUS_APPLIES_TO, "
                        SQL &= "'Refining' AS ACTIVITY, "
                        SQL &= "AT.displayNameID AS BONUS_NAME, "
                        SQL &= "value* " & CStr(SystemSecurityBonus) & " AS BONUS, " ' Data is stored as a decimal but others it's a full number
                        SQL &= "typeName AS BONUS_SOURCE "
                        SQL &= "FROM TYPE_ATTRIBUTES AS TA, INVENTORY_TYPES AS IT, ATTRIBUTE_TYPES AS AT "
                        SQL &= "WHERE TA.attributeID = AT.attributeID "
                        SQL &= "AND TA.typeID = IT.typeID  "
                        SQL &= "AND TA.attributeID IN (SELECT attributeID FROM ATTRIBUTE_TYPES WHERE attributeName LIKE 'refiningYield%') "

                    Case "ReactionRigs"

                        SQL = "SELECT 'Reactions' AS BONUS_APPLIES_TO, "
                        SQL &= "'Reactions' AS ACTIVITY, "
                        SQL &= "AT.displayNameID AS BONUS_NAME, "
                        SQL &= "value/ 100 * " & CStr(SystemSecurityBonus) & " AS BONUS, "
                        SQL &= "typeName AS BONUS_SOURCE "
                        SQL &= "FROM TYPE_ATTRIBUTES AS TA, INVENTORY_TYPES AS IT, ATTRIBUTE_TYPES AS AT "
                        SQL &= "WHERE TA.attributeID = AT.attributeID "
                        SQL &= "AND TA.typeID = IT.typeID  "
                        SQL &= "AND TA.attributeID IN (SELECT attributeID FROM ATTRIBUTE_TYPES WHERE attributeName LIKE 'attributeEngRig%') "

                    Case "DrillingRigs"

                        SQL = "SELECT 'Moon Mining' AS BONUS_APPLIES_TO, "
                        SQL &= "'Moon Mining' AS ACTIVITY, "
                        SQL &= "AT.displayNameID AS BONUS_NAME, "
                        SQL &= "value/ 100 * " & CStr(SystemSecurityBonus) & " AS BONUS, "
                        SQL &= "typeName AS BONUS_SOURCE "
                        SQL &= "FROM TYPE_ATTRIBUTES AS TA, INVENTORY_TYPES AS IT, ATTRIBUTE_TYPES AS AT "
                        SQL &= "WHERE TA.attributeID = AT.attributeID "
                        SQL &= "AND TA.typeID = IT.typeID  "
                        SQL &= "AND TA.attributeID IN (SELECT attributeID FROM ATTRIBUTE_TYPES WHERE attributeName LIKE 'moonRig%') "

                    Case Else
                        Exit For
                End Select

                SQL &= "AND BONUS <> 0 AND TA.typeID = {0} "

                DBCommand = New SQLiteCommand(String.Format(SQL, InstalledModule.typeID), EVEDB.DBREf)
                rsReader = DBCommand.ExecuteReader

                While rsReader.Read
                    ' Insert a row with the data pulled
                    ' Columns: Bonus Applies to, Activity, Bonuses, Bonus Source
                    BonusList = New ListViewItem(rsReader.GetString(0)) ' Group or Category bonus is applied
                    BonusList.SubItems.Add(CStr(rsReader.GetString(1))) ' Activity
                    BonusList.SubItems.Add(CStr(rsReader.GetString(2)) & ": " & FormatPercent(rsReader.GetDouble(3), 2)) ' Combine bonus and bonus name
                    BonusList.SubItems.Add(CStr(rsReader.GetString(4))) ' Source of bonus

                    ' Make sure it's visible, then refresh the list
                    If Application.OpenForms().OfType(Of frmBonusPopout).Any Then
                        ' add the record to the popout list too
                        Dim PopupBonusList As ListViewItem = CType(BonusList.Clone, ListViewItem)
                        Call frmPopout.lstUpwellStructureBonuses.Items.Add(PopupBonusList)
                    End If

                    ' Update the final list
                    Call lstUpwellStructureBonuses.Items.Add(BonusList)

                End While
            End If
        Next

    End Sub

    Private Sub btnBonusPopout_Click(sender As Object, e As EventArgs) Handles btnBonusPopout.Click
        Dim BonusList As ListViewItem

        If Application.OpenForms().OfType(Of frmBonusPopout).Any Then
            Exit Sub
        End If

        frmPopout = New frmBonusPopout

        Call frmPopout.lstUpwellStructureBonuses.Items.Clear()

        For i = 0 To lstUpwellStructureBonuses.Items.Count - 1
            BonusList = New ListViewItem(lstUpwellStructureBonuses.Items(i).SubItems(0).Text)
            BonusList.SubItems.Add(lstUpwellStructureBonuses.Items(i).SubItems(1).Text)
            BonusList.SubItems.Add(lstUpwellStructureBonuses.Items(i).SubItems(2).Text)
            BonusList.SubItems.Add(lstUpwellStructureBonuses.Items(i).SubItems(3).Text)

            ' Update the form list
            frmPopout.lstUpwellStructureBonuses.Items.Add(BonusList)

        Next

        frmPopout.Show()

    End Sub

#Region "Fuel Settings"

    Private Sub btnRefreshPrices_Click(sender As Object, e As EventArgs) Handles btnRefreshPrices.Click
        ' Refresh all prices
        Call LoadFuelPrices()
    End Sub

    Private Sub btnSavePrices_Click(sender As Object, e As EventArgs) Handles btnSavePrices.Click
        Dim SQL As String = ""

        Try

            EVEDB.BeginSQLiteTransaction()
            ' Buying, so save only the fuel block prices
            If Not rbtnBuildBlocks.Checked Then
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtHeliumFuelBlockBuyPrice.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & CStr(FuelBlocks.Helium)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtHydrogenFuelBlockBuyPrice.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & CStr(FuelBlocks.Hydrogen)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtNitrogenFuelBlockBuyPrice.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & CStr(FuelBlocks.Nitrogen)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtNitrogenFuelBlockBuyPrice.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = " & CStr(FuelBlocks.Oxygen)
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            Else ' Only save mats
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtHeliumIsotopes.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 16274"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtHydrogenIsotopes.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 17889"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtNitrogenIsotopes.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 17888"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtOxygenIsotopes.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 17887"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtCoolant.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 9832"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtEnrichedUranium.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 44"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtHeavyWater.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 16272"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtLiquidOzone.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 16273"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtMechanicalParts.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 3689"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtOxygen.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 3683"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtRobotics.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 9848"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
                SQL = "UPDATE ITEM_PRICES_FACT SET PRICE = " & CDbl(txtStrontiumClathrates.Text) & ", PRICE_TYPE = 'User' WHERE ITEM_ID = 16275"
                Call EVEDB.ExecuteNonQuerySQL(SQL)
            End If

            MsgBox("Prices Saved", vbInformation, Me.Text)
            EVEDB.CommitSQLiteTransaction()

            ' Refresh the prices
            Call LoadFuelPrices()
        Catch EX As Exception
            MsgBox("Prices not saved, Error: " & EX.Message, vbExclamation, Me.Text)
            EVEDB.RollbackSQLiteTransaction()
        End Try

    End Sub

    Private Sub btnUpdateBuildCost_Click(sender As Object, e As EventArgs) Handles btnUpdateBuildCost.Click
        ' Refresh the price based on building the blocks with ME's entered (just do all to simplify)
        Call SetFuelBlockBuildcost(FuelBlocks.Helium)
        Call SetFuelBlockBuildcost(FuelBlocks.Hydrogen)
        Call SetFuelBlockBuildcost(FuelBlocks.Nitrogen)
        Call SetFuelBlockBuildcost(FuelBlocks.Oxygen)
    End Sub

    Private Sub btnSaveFuelBlockInfo_Click(sender As Object, e As EventArgs) Handles btnSaveFuelBlockInfo.Click

        ' Save just the bp ME data if building
        If rbtnBuildBlocks.Checked Then
            If HeliumFuelBlockBPUpdated Then
                Call UpdateBPinDB(FuelBlocks.HeliumBP, CInt(txtHeliumFuelBlockBPME.Text), OriginalHeliumFuelBlockBPTE, BPType.Original,
                             OriginalHeliumFuelBlockBPME, OriginalHeliumFuelBlockBPTE)
                HeliumFuelBlockBPUpdated = False ' Saved, so updated
            End If

            If HydrogenFuelBlockBPUpdated Then
                Call UpdateBPinDB(FuelBlocks.HeliumBP, CInt(txtHydrogenFuelBlockBPME.Text), OriginalHydrogenFuelBlockBPTE, BPType.Original,
                             OriginalHydrogenFuelBlockBPME, OriginalHydrogenFuelBlockBPTE)
                HydrogenFuelBlockBPUpdated = False ' Saved, so updated
            End If

            If NitrogenFuelBlockBPUpdated Then
                Call UpdateBPinDB(FuelBlocks.HeliumBP, CInt(txtNitrogenFuelBlockBPME.Text), OriginalNitrogenFuelBlockBPTE, BPType.Original,
                             OriginalNitrogenFuelBlockBPME, OriginalNitrogenFuelBlockBPTE)
                NitrogenFuelBlockBPUpdated = False ' Saved, so updated
            End If

            If OxygenFuelBlockBPUpdated Then
                Call UpdateBPinDB(FuelBlocks.HeliumBP, CInt(txtOxygenFuelBlockBPME.Text), OriginalOxygenFuelBlockBPTE, BPType.Original,
                             OriginalOxygenFuelBlockBPME, OriginalOxygenFuelBlockBPTE)
                OxygenFuelBlockBPUpdated = False ' Saved, so updated
            End If
        End If

        MsgBox("BP Information Saved", vbInformation, Me.Text)

    End Sub

    Private Sub LoadFuelBlockDataTab()

        ' Dynamically load images
        Call LoadFuelBlockImages()
        ' Load all the fuel prices
        Call LoadFuelPrices()
        ' Load the ME's for each fuel block bp
        Call LoadBlockBPMEs()
        ' Load the costs to build the blocks with current settings
        Call SetFuelBlockBuildcost(FuelBlocks.Helium)
        Call SetFuelBlockBuildcost(FuelBlocks.Hydrogen)
        Call SetFuelBlockBuildcost(FuelBlocks.Nitrogen)
        Call SetFuelBlockBuildcost(FuelBlocks.Oxygen)

    End Sub

    Private Sub LoadBlockBPMEs()
        ' Load the ME for the type of block that we are using for this tower
        Dim SQL As String
        Dim reader As SQLiteDataReader
        Dim HasHelium As Boolean = False
        Dim HasHydrogen As Boolean = False
        Dim HasNitrogen As Boolean = False
        Dim HasOxygen As Boolean = False
        Dim FoundME As String = ""
        Dim FoundTE As Integer = 0

        SQL = "SELECT ALL_BLUEPRINTS.BLUEPRINT_ID, ME, TE FROM OWNED_BLUEPRINTS, ALL_BLUEPRINTS "
        SQL &= "WHERE ALL_BLUEPRINTS.BLUEPRINT_ID = OWNED_BLUEPRINTS.BLUEPRINT_ID "
        SQL &= "AND ALL_BLUEPRINTS.BLUEPRINT_ID IN (" & CStr(FuelBlocks.HeliumBP) & "," & CStr(FuelBlocks.HydrogenBP) & "," & CStr(FuelBlocks.NitrogenBP) & "," & CStr(FuelBlocks.OxygenBP) & ")"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        reader = DBCommand.ExecuteReader()

        While reader.Read
            FoundME = CStr(reader.GetInt64(1))
            FoundTE = reader.GetInt32(2)

            Select Case reader.GetInt64(0)
                Case FuelBlocks.NitrogenBP
                    txtNitrogenFuelBlockBPME.Text = FoundME
                    OriginalNitrogenFuelBlockBPME = CInt(FoundME)
                    OriginalNitrogenFuelBlockBPTE = FoundTE
                    HasNitrogen = True
                Case FuelBlocks.HydrogenBP
                    txtHydrogenFuelBlockBPME.Text = FoundME
                    OriginalHydrogenFuelBlockBPME = CInt(FoundME)
                    OriginalHydrogenFuelBlockBPTE = FoundTE
                    HasHydrogen = True
                Case FuelBlocks.HeliumBP
                    txtHeliumFuelBlockBPME.Text = FoundME
                    OriginalHeliumFuelBlockBPME = CInt(FoundME)
                    OriginalHeliumFuelBlockBPTE = FoundTE
                    HasHelium = True
                Case FuelBlocks.OxygenBP
                    txtOxygenFuelBlockBPME.Text = FoundME
                    OriginalOxygenFuelBlockBPME = CInt(FoundME)
                    OriginalOxygenFuelBlockBPTE = FoundTE
                    HasOxygen = True
            End Select

            ' See what they didn't have and set to 0 ME
            If Not HasNitrogen Then
                txtNitrogenFuelBlockBPME.Text = "0"
                OriginalNitrogenFuelBlockBPME = 0
                OriginalNitrogenFuelBlockBPTE = 0
            End If
            If Not HasHydrogen Then
                txtHydrogenFuelBlockBPME.Text = "0"
                OriginalHydrogenFuelBlockBPME = 0
                OriginalHydrogenFuelBlockBPTE = 0
            End If
            If Not HasHelium Then
                txtHeliumFuelBlockBPME.Text = "0"
                OriginalHeliumFuelBlockBPME = 0
                OriginalHeliumFuelBlockBPTE = 0
            End If
            If Not HasOxygen Then
                txtOxygenFuelBlockBPME.Text = "0"
                OriginalOxygenFuelBlockBPME = 0
                OriginalOxygenFuelBlockBPTE = 0
            End If

        End While

    End Sub

    Private Sub SetFuelBlockBuildcost(FuelBlock As FuelBlocks)

        ' Go through each fuel block and build it, then set the price. Make sure the ME's are valid first
        Select Case FuelBlock
            Case FuelBlocks.Helium
                If Not IsNumeric(txtHeliumFuelBlockBPME.Text) Then
                    MsgBox("Invalid Fuel Block BPO ME", vbExclamation, Application.ProductName)
                    txtHeliumFuelBlockBPME.Focus()
                    Exit Sub
                End If

                lblHeliumFuelBlockBuildPrice.Text = CStr(GetFuelBlockBuildCost(FuelBlocks.Helium, CInt(txtHeliumFuelBlockBPME.Text)))

            Case FuelBlocks.Hydrogen
                If Not IsNumeric(txtHydrogenFuelBlockBPME.Text) Then
                    MsgBox("Invalid Fuel Block BPO ME", vbExclamation, Application.ProductName)
                    txtHydrogenFuelBlockBPME.Focus()
                    Exit Sub
                End If

                lblHydrogenFuelBlockBuildPrice.Text = CStr(GetFuelBlockBuildCost(FuelBlocks.Hydrogen, CInt(txtHydrogenFuelBlockBPME.Text)))

            Case FuelBlocks.Nitrogen
                If Not IsNumeric(txtNitrogenFuelBlockBPME.Text) Then
                    MsgBox("Invalid Fuel Block BPO ME", vbExclamation, Application.ProductName)
                    txtNitrogenFuelBlockBPME.Focus()
                    Exit Sub
                End If

                lblNitrogenFuelBlockBuildPrice.Text = CStr(GetFuelBlockBuildCost(FuelBlocks.Nitrogen, CInt(txtNitrogenFuelBlockBPME.Text)))

            Case FuelBlocks.Oxygen
                If Not IsNumeric(txtOxygenFuelBlockBPME.Text) Then
                    MsgBox("Invalid Fuel Block BPO ME", vbExclamation, Application.ProductName)
                    txtOxygenFuelBlockBPME.Focus()
                    Exit Sub
                End If

                lblOxygenFuelBlockBuildPrice.Text = CStr(GetFuelBlockBuildCost(FuelBlocks.Oxygen, CInt(txtOxygenFuelBlockBPME.Text)))

        End Select

    End Sub

    Private Function GetFuelBlockBuildCost(FuelBlock As FuelBlocks, bpME As Integer) As Double
        Dim BuildFacility As IndustryFacility = frmMain.BPTabFacility.GetFacility(ProductionType.Manufacturing)
        Dim ComponentFacility As IndustryFacility = frmMain.BPTabFacility.GetFacility(ProductionType.ComponentManufacturing)
        Dim CapComponentFacility As IndustryFacility = frmMain.BPTabFacility.GetFacility(ProductionType.CapitalComponentManufacturing)
        Dim ReactionFacility As IndustryFacility = frmMain.BPTabFacility.GetFacility(ProductionType.Reactions)
        Dim BPID As Integer

        Select Case FuelBlock
            Case FuelBlocks.Nitrogen
                BPID = FuelBlocks.NitrogenBP
            Case FuelBlocks.Helium
                BPID = FuelBlocks.HeliumBP
            Case FuelBlocks.Hydrogen
                BPID = FuelBlocks.HydrogenBP
            Case FuelBlocks.Oxygen
                BPID = FuelBlocks.OxygenBP
        End Select

        ' Build T1 BP for the block, standard settings with whatever is on bp tab
        Dim BlockBP = New Blueprint(BPID, 1, bpME, 0, 1, 1, SelectedCharacter, UserApplicationSettings, False, 0,
                                    BuildFacility, ComponentFacility, CapComponentFacility, ReactionFacility,
                                    UserBPTabSettings.SellExcessBuildItems, UserBPTabSettings.BuildT2T3Materials, True)
        Dim TempBF As BrokerFeeInfo
        TempBF.IncludeFee = BrokerFeeType.NoFee
        Call BlockBP.BuildItems(False, TempBF, False, False, False)

        Return BlockBP.GetRawItemUnitPrice

    End Function

    Private Sub LoadFuelBlockImages()
        ' Just load up all the images dyamically

        If File.Exists(Path.Combine(UserImagePath, CStr(FuelBlocks.Nitrogen) & "_32.png")) Then
            picNitrogenFuelBlock.Image = Image.FromFile(Path.Combine(UserImagePath, CStr(FuelBlocks.Nitrogen) & "_32.png"))
        Else
            picNitrogenFuelBlock.Image = Nothing
        End If

        If File.Exists(Path.Combine(UserImagePath, CStr(FuelBlocks.Oxygen) & "_32.png")) Then
            picOxygenFuelBlock.Image = Image.FromFile(Path.Combine(UserImagePath, CStr(FuelBlocks.Oxygen) & "_32.png"))
        Else
            picOxygenFuelBlock.Image = Nothing
        End If

        If File.Exists(Path.Combine(UserImagePath, CStr(FuelBlocks.Hydrogen) & "_32.png")) Then
            picHydrogenFuelBlock.Image = Image.FromFile(Path.Combine(UserImagePath, CStr(FuelBlocks.Hydrogen) & "_32.png"))
        Else
            picHydrogenFuelBlock.Image = Nothing
        End If

        If File.Exists(Path.Combine(UserImagePath, CStr(FuelBlocks.Helium) & "_32.png")) Then
            picHeliumFuelBlock.Image = Image.FromFile(Path.Combine(UserImagePath, CStr(FuelBlocks.Helium) & "_32.png"))
        Else
            picHeliumFuelBlock.Image = Nothing
        End If

        picNitrogenFuelBlock.Refresh()
        picOxygenFuelBlock.Refresh()
        picHydrogenFuelBlock.Refresh()
        picHeliumFuelBlock.Refresh()

        If File.Exists(Path.Combine(UserImagePath, "9832_32.png")) Then
            picCoolant.Image = Image.FromFile(Path.Combine(UserImagePath, "9832_32.png"))
        Else
            picCoolant.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "44_32.png")) Then
            picEnrichedUranium.Image = Image.FromFile(Path.Combine(UserImagePath, "44_32.png"))
        Else
            picEnrichedUranium.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "16272_32.png")) Then
            picHeavyWater.Image = Image.FromFile(Path.Combine(UserImagePath, "16272_32.png"))
        Else
            picHeavyWater.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "16274_32.png")) Then
            picHeliumIsotopes.Image = Image.FromFile(Path.Combine(UserImagePath, "16274_32.png"))
        Else
            picHeliumIsotopes.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "17889_32.png")) Then
            picHydrogenIsotopes.Image = Image.FromFile(Path.Combine(UserImagePath, "17889_32.png"))
        Else
            picHydrogenIsotopes.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "16273_32.png")) Then
            picLiquidOzone.Image = Image.FromFile(Path.Combine(UserImagePath, "16273_32.png"))
        Else
            picLiquidOzone.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "3689_32.png")) Then
            picMechanicalParts.Image = Image.FromFile(Path.Combine(UserImagePath, "3689_32.png"))
        Else
            picMechanicalParts.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "17888_32.png")) Then
            picNitrogenIsotopes.Image = Image.FromFile(Path.Combine(UserImagePath, "17888_32.png"))
        Else
            picNitrogenIsotopes.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "3683_32.png")) Then
            picOxygen.Image = Image.FromFile(Path.Combine(UserImagePath, "3683_32.png"))
        Else
            picOxygen.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "17887_32.png")) Then
            picOxygenIsotopes.Image = Image.FromFile(Path.Combine(UserImagePath, "17887_32.png"))
        Else
            picOxygenIsotopes.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "9848_32.png")) Then
            picRobotics.Image = Image.FromFile(Path.Combine(UserImagePath, "9848_32.png"))
        Else
            picRobotics.Image = Nothing
        End If
        If File.Exists(Path.Combine(UserImagePath, "16275_32.png")) Then
            picStrontiumClathrates.Image = Image.FromFile(Path.Combine(UserImagePath, "16275_32.png"))
        Else
            picStrontiumClathrates.Image = Nothing
        End If

        picCoolant.Refresh()
        picEnrichedUranium.Refresh()
        picHeavyWater.Refresh()
        picHeliumIsotopes.Refresh()
        picHydrogenIsotopes.Refresh()
        picLiquidOzone.Refresh()
        picMechanicalParts.Refresh()
        picNitrogenIsotopes.Refresh()
        picOxygen.Refresh()
        picOxygenIsotopes.Refresh()
        picRobotics.Refresh()
        picStrontiumClathrates.Refresh()

        Application.DoEvents()

    End Sub

    Private Sub LoadFuelPrices()
        Dim SQL As String
        Dim reader As SQLiteDataReader
        Dim Price As String

        Me.Cursor = Cursors.WaitCursor

        SQL = "SELECT ITEM_PRICES.ITEM_ID, ITEM_PRICES.PRICE "
        SQL &= "FROM ITEM_PRICES "
        SQL &= "WHERE ITEM_PRICES.ITEM_ID IN "
        SQL &= "(9832, 44, 16272, 16274, 17889, 16273, 3689, 17888, 3683, 17887, 9848, 16275)" ' Mats
        SQL &= "OR ITEM_PRICES.ITEM_ID IN (" & CStr(FuelBlocks.Nitrogen) & "," & CStr(FuelBlocks.Hydrogen) & "," & CStr(FuelBlocks.Helium) & "," & CStr(FuelBlocks.Oxygen) & ")"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        reader = DBCommand.ExecuteReader()

        While reader.Read

            Price = FormatNumber(reader.GetDouble(1), 2)

            ' Update the textboxes with Prices
            Select Case reader.GetInt64(0)
                Case 17889 'Hydrogen Isotopes'
                    txtHydrogenIsotopes.Text = Price
                Case 17887 'Oxygen Isotopes'
                    txtOxygenIsotopes.Text = Price
                Case 17888 'Nitrogen Isotopes'
                    txtNitrogenIsotopes.Text = Price
                Case 16274 'Helium Isotopes'
                    txtHeliumIsotopes.Text = Price
                Case 16275 'Strontium Clathrates'
                    txtStrontiumClathrates.Text = Price
                Case 16272 'Heavy Water'
                    txtHeavyWater.Text = Price
                Case 16273 'Liquid Ozone'
                    txtLiquidOzone.Text = Price
                Case 9848 'Robotics'
                    txtRobotics.Text = Price
                Case 3683 'Oxygen'
                    txtOxygen.Text = Price
                Case 3689 'Mechanical Parts'
                    txtMechanicalParts.Text = Price
                Case 9832 'Coolant'
                    txtCoolant.Text = Price
                Case 44 'Enriched Uranium'
                    txtEnrichedUranium.Text = Price
                Case FuelBlocks.Nitrogen
                    txtNitrogenFuelBlockBuyPrice.Text = Price
                Case FuelBlocks.Hydrogen
                    txtHydrogenFuelBlockBuyPrice.Text = Price
                Case FuelBlocks.Helium
                    txtHeliumFuelBlockBuyPrice.Text = Price
                Case FuelBlocks.Oxygen
                    txtOxygenFuelBlockBuyPrice.Text = Price
            End Select
            Application.DoEvents()
        End While

        Me.Cursor = Cursors.Default
        txtHeliumIsotopes.Focus()

        reader.Close()
        reader = Nothing
        DBCommand = Nothing

    End Sub

#End Region

#Region "Click Events"

    Private Sub cmbUpwellStructureName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUpwellStructureName.SelectedIndexChanged
        Call LoadStructure(cmbUpwellStructureName.Text)
    End Sub

    Private Sub chkItemViewTypeAll_CheckedChanged(sender As Object, e As EventArgs)
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeHigh_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeHigh.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeLow_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeLow.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeMedium_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeMedium.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeServices_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeServices.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewCombat_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewCombat.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewReaction_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewReaction.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewDrilling_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewDrilling.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewEngineering_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewEngineering.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewReprocessing_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewReprocessing.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub cmbUpwellStructureName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbUpwellStructureName.KeyPress
        e.Handled = True
    End Sub

    Private Sub MidSlot1_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot1.DoubleClick
        MidSlot1.Image = Nothing
        MidSlot1.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot2_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot2.DoubleClick
        MidSlot2.Image = Nothing
        MidSlot2.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot3_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot3.DoubleClick
        MidSlot3.Image = Nothing
        MidSlot3.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot4_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot4.DoubleClick
        MidSlot4.Image = Nothing
        MidSlot4.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot5_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot5.DoubleClick
        MidSlot5.Image = Nothing
        MidSlot5.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot6_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot6.DoubleClick
        MidSlot6.Image = Nothing
        MidSlot6.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot7_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot7.DoubleClick
        MidSlot7.Image = Nothing
        MidSlot7.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub MidSlot8_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot8.DoubleClick
        MidSlot8.Image = Nothing
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot1_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot1.DoubleClick
        If Not IsNothing(HighSlot1.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot1.Image.Tag))
        End If
        HighSlot1.Image = Nothing
        HighSlot1.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot2_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot2.DoubleClick
        If Not IsNothing(HighSlot2.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot2.Image.Tag))
        End If
        HighSlot2.Image = Nothing
        HighSlot2.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot3_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot3.DoubleClick
        If Not IsNothing(HighSlot3.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot3.Image.Tag))
        End If
        HighSlot3.Image = Nothing
        HighSlot3.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot5_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot5.DoubleClick
        If Not IsNothing(HighSlot5.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot5.Image.Tag))
        End If
        HighSlot5.Image = Nothing
        HighSlot5.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot7_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot7.DoubleClick
        If Not IsNothing(HighSlot7.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot7.Image.Tag))
        End If
        HighSlot7.Image = Nothing
        HighSlot7.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot4_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot4.DoubleClick
        If Not IsNothing(HighSlot4.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot4.Image.Tag))
        End If
        HighSlot4.Image = Nothing
        HighSlot4.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot6_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot6.DoubleClick
        If Not IsNothing(HighSlot6.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot6.Image.Tag))
        End If
        HighSlot6.Image = Nothing
        HighSlot6.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub HighSlot8_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot8.DoubleClick
        If Not IsNothing(HighSlot8.Image) Then
            Call UpdateLauncherSlots(True, CInt(HighSlot8.Image.Tag))
        End If
        HighSlot8.Image = Nothing
        HighSlot8.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub RigSlot3_DoubleClick(sender As Object, e As EventArgs) Handles RigSlot3.DoubleClick
        RigSlot3.Image = Nothing
        RigSlot3.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub RigSlot2_DoubleClick(sender As Object, e As EventArgs) Handles RigSlot2.DoubleClick
        RigSlot2.Image = Nothing
        RigSlot2.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub RigSlot1_DoubleClick(sender As Object, e As EventArgs) Handles RigSlot1.DoubleClick
        RigSlot1.Image = Nothing
        RigSlot1.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot1_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot1.DoubleClick
        LowSlot1.Image = Nothing
        LowSlot1.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot2_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot2.DoubleClick
        LowSlot2.Image = Nothing
        LowSlot2.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot3_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot3.DoubleClick
        LowSlot3.Image = Nothing
        LowSlot3.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot4_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot4.DoubleClick
        LowSlot4.Image = Nothing
        LowSlot4.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot5_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot5.DoubleClick
        LowSlot5.Image = Nothing
        LowSlot5.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot6_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot6.DoubleClick
        LowSlot6.Image = Nothing
        LowSlot6.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot7_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot7.DoubleClick
        LowSlot7.Image = Nothing
        LowSlot7.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub LowSlot8_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot8.DoubleClick
        LowSlot8.Image = Nothing
        LowSlot8.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub ServiceSlot5_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot5.DoubleClick
        ServiceSlot5.Image = Nothing
        ServiceSlot5.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub ServiceSlot3_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot3.DoubleClick
        ServiceSlot3.Image = Nothing
        ServiceSlot3.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub ServiceSlot1_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot1.DoubleClick
        ServiceSlot1.Image = Nothing
        ServiceSlot1.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub ServiceSlot2_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot2.DoubleClick
        ServiceSlot2.Image = Nothing
        ServiceSlot2.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub ServiceSlot4_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot4.DoubleClick
        ServiceSlot4.Image = Nothing
        ServiceSlot4.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub ServiceSlot6_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot6.DoubleClick
        ServiceSlot6.Image = Nothing
        ServiceSlot6.ResetText()
        Call UpdateUpwellStructureStats()
    End Sub

    Private Sub btnToggleAllPriceItems_Click(sender As Object, e As EventArgs) Handles btnStripFitting.Click
        Call StripFitting()
    End Sub

    Private Sub chkIncludeFuelCosts_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeFuelCosts.CheckedChanged
        If chkIncludeFuelCosts.Checked Then
            gbIncludeFuelBlocks.Enabled = True
        Else
            gbIncludeFuelBlocks.Enabled = False
        End If
        Call UpdateFuelCostLabels()
    End Sub

    Private Sub btnItemFilter_Click(sender As Object, e As EventArgs) Handles btnItemFilter.Click
        Call UpdateFittingImages()
    End Sub

    Private Sub btnResetItemFilter_Click(sender As Object, e As EventArgs) Handles btnResetItemFilter.Click
        txtItemFilter.Text = ""
        Call UpdateFittingImages()
    End Sub

    Private Sub txtItemFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItemFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call UpdateFittingImages()
        End If
    End Sub

    Private Sub ServiceModuleListView_ItemActivate(sender As Object, e As EventArgs) Handles FittingListViewIcons.ItemActivate
        Call LoadSelectedImageInFreeSlot()
    End Sub

    Private Sub btnCloseForm_Click(sender As Object, e As EventArgs) Handles btnCloseForm.Click
        Me.Hide()
    End Sub

    Private Sub chkHighSec_CheckedChanged(sender As Object, e As EventArgs) Handles chkHighSec.CheckedChanged
        If Not UpdateChecks Then
            Call SetSpaceSecurityChecks(0)
            Call UpdateUpwellStructureBonuses()
        End If
    End Sub

    Private Sub chkLowSec_CheckedChanged(sender As Object, e As EventArgs) Handles chkLowSec.CheckedChanged
        If Not UpdateChecks Then
            Call SetSpaceSecurityChecks(1)
            Call UpdateUpwellStructureBonuses()
        End If
    End Sub

    Private Sub chkNullSec_CheckedChanged(sender As Object, e As EventArgs) Handles chkNullSec.CheckedChanged
        If Not UpdateChecks Then
            Call SetSpaceSecurityChecks(2)
            Call UpdateUpwellStructureBonuses()
        End If
    End Sub

    ' Ensures one is at least checked
    Private Sub SetSpaceSecurityChecks(ByVal TriggerIndex As Integer)
        Dim i As Integer

        If Not FirstLoad Then
            ' Adjust the checks depending on options
            For i = 0 To SecurityCheckBoxes.Count - 1
                UpdateChecks = True
                If i <> TriggerIndex Then
                    SecurityCheckBoxes(i).Checked = False
                ElseIf i = TriggerIndex And SecurityCheckBoxes(i).Checked = False Then
                    SecurityCheckBoxes(i).Checked = True ' Don't let them uncheck the value
                End If
                UpdateChecks = False
            Next
        End If
        'End If
    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Dim TempSettings As UpwellStructureSettings = Nothing
        Dim Settings As New ProgramSettings

        With TempSettings
            .HighSlotsCheck = chkItemViewTypeHigh.Checked
            .MediumSlotsCheck = chkItemViewTypeMedium.Checked
            .LowSlotsCheck = chkItemViewTypeLow.Checked
            .ServicesCheck = chkItemViewTypeServices.Checked

            .ReprocessingRigsCheck = chkRigTypeViewReprocessing.Checked
            .EngineeringRigsCheck = chkRigTypeViewEngineering.Checked
            .CombatRigsCheck = chkRigTypeViewCombat.Checked
            .ReactionsRigsCheck = chkRigTypeViewReaction.Checked
            .DrillingRigsCheck = chkRigTypeViewDrilling.Checked

            .SearchFilterText = txtItemFilter.Text

            .IncludeFuelCostsCheck = chkIncludeFuelCosts.Checked

            If rbtnHeliumFuelBlock.Checked Then
                .FuelBlockType = rbtnHeliumFuelBlock.Text
            ElseIf rbtnHydrogenFuelBlock.Checked Then
                .FuelBlockType = rbtnHydrogenFuelBlock.Text
            ElseIf rbtnNitrogenFuelBlock.Checked Then
                .FuelBlockType = rbtnNitrogenFuelBlock.Text
            ElseIf rbtnOxygenFuelBlock.Checked Then
                .FuelBlockType = rbtnOxygenFuelBlock.Text
            End If

            If rbtnBuildBlocks.Checked Then
                .BuyBuildBlockOption = rbtnBuildBlocks.Text
            ElseIf rbtnBuyBlocks.Checked Then
                .BuyBuildBlockOption = rbtnBuyBlocks.Text
            End If

            If rbtnListView.Checked Then
                .IconListView = False
            ElseIf rbtnViewIcons.Checked Then
                .IconListView = True
            End If

            .SelectedStructureName = cmbUpwellStructureName.Text

        End With

        ' Save the data in the XML file
        Call Settings.SaveUpwellStructureViewerSettings(TempSettings)

        ' Save the data to the local variable
        UserUpwellStructureSettings = TempSettings

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub rbtnHeliumFuelBlock_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnHeliumFuelBlock.CheckedChanged
        Call UpdateFuelCostLabels()
    End Sub

    Private Sub rbtnHydrogenFuelBlock_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnHydrogenFuelBlock.CheckedChanged
        Call UpdateFuelCostLabels()
    End Sub

    Private Sub rbtnNitrogenFuelBlock_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnNitrogenFuelBlock.CheckedChanged
        Call UpdateFuelCostLabels()
    End Sub

    Private Sub rbtnOxygenFuelBlock_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnOxygenFuelBlock.CheckedChanged
        Call UpdateFuelCostLabels()
    End Sub

    Private Sub rbtnBuyBlocks_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnBuyBlocks.CheckedChanged

        txtNitrogenFuelBlockBuyPrice.Enabled = True
        lblNitrogenFuelBlockBuy.Enabled = True
        txtHydrogenFuelBlockBuyPrice.Enabled = True
        lblHydrogenBlockBuy.Enabled = True
        txtHeliumFuelBlockBuyPrice.Enabled = True
        lblHeliumFuelBlockBuy.Enabled = True
        txtOxygenFuelBlockBuyPrice.Enabled = True
        lblOxygenFuelBlockBuy.Enabled = True

        txtHeliumFuelBlockBPME.Enabled = False
        lblHeliumFuelBlockBPME.Enabled = False
        txtNitrogenFuelBlockBPME.Enabled = False
        lblNitrogenFuelBlockBPME.Enabled = False
        txtHydrogenFuelBlockBPME.Enabled = False
        lblHydrogenFuelBlockBPME.Enabled = False
        txtOxygenFuelBlockBPME.Enabled = False
        lblOxygenFuelBlockBPME.Enabled = False
        lblNitrogenFuelBlockBuildPrice.Enabled = False
        lblOxygenFuelBlockBuildPrice.Enabled = False
        lblHeliumFuelBlockBuild.Enabled = False
        lblHydrogenFuelBlockBuildPrice.Enabled = False
        lblNitrogenFuelBlockBuild.Enabled = False
        lblOxygenFuelBlockBuild.Enabled = False
        lblHeliumFuelBlockBuildPrice.Enabled = False
        lblHydrogenFuelBlockBuild.Enabled = False

        ' Not building so disable prices
        gbFuelPrices.Enabled = False

        ' Disable the update build cost and save fuel block info buttons too
        btnUpdateBuildCost.Enabled = False
        btnSaveFuelBlockInfo.Enabled = False

    End Sub

    Private Sub rbtnBuildBlocks_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnBuildBlocks.CheckedChanged

        txtNitrogenFuelBlockBuyPrice.Enabled = False
        lblNitrogenFuelBlockBuy.Enabled = False
        txtHydrogenFuelBlockBuyPrice.Enabled = False
        lblHydrogenBlockBuy.Enabled = False
        txtHeliumFuelBlockBuyPrice.Enabled = False
        lblHeliumFuelBlockBuy.Enabled = False
        txtOxygenFuelBlockBuyPrice.Enabled = False
        lblOxygenFuelBlockBuy.Enabled = False

        txtHeliumFuelBlockBPME.Enabled = True
        lblHeliumFuelBlockBPME.Enabled = True
        txtNitrogenFuelBlockBPME.Enabled = True
        lblNitrogenFuelBlockBPME.Enabled = True
        txtHydrogenFuelBlockBPME.Enabled = True
        lblHydrogenFuelBlockBPME.Enabled = True
        txtOxygenFuelBlockBPME.Enabled = True
        lblOxygenFuelBlockBPME.Enabled = True
        lblNitrogenFuelBlockBuildPrice.Enabled = True
        lblOxygenFuelBlockBuildPrice.Enabled = True
        lblHeliumFuelBlockBuild.Enabled = True
        lblHydrogenFuelBlockBuildPrice.Enabled = True
        lblNitrogenFuelBlockBuild.Enabled = True
        lblOxygenFuelBlockBuild.Enabled = True
        lblHeliumFuelBlockBuildPrice.Enabled = True
        lblHydrogenFuelBlockBuild.Enabled = True

        ' Building so enable all prices
        gbFuelPrices.Enabled = True

        ' Building, so enable the update build cost and save fuel block info buttons
        btnUpdateBuildCost.Enabled = True
        btnSaveFuelBlockInfo.Enabled = True

    End Sub

    Private Sub ProcessKeyPress(ByRef e As KeyPressEventArgs)
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub ProcessMaterialKeyDown(e As KeyEventArgs, ProcessTextBox As TextBox)
        Call ProcessCutCopyPasteSelect(ProcessTextBox, e)
        If e.KeyCode = Keys.Enter Then
            ' Set them all
            Call SetFuelBlockBuildcost(FuelBlocks.Helium)
            Call SetFuelBlockBuildcost(FuelBlocks.Hydrogen)
            Call SetFuelBlockBuildcost(FuelBlocks.Oxygen)
            Call SetFuelBlockBuildcost(FuelBlocks.Nitrogen)
        End If
    End Sub

    Private Sub ProcessBlockBuyKeyDown(e As KeyEventArgs, ProcessTextBox As TextBox)
        Call ProcessCutCopyPasteSelect(ProcessTextBox, e)
        If e.KeyCode = Keys.Enter Then
            Call UpdateFuelCostLabels()
        End If
    End Sub

    Private Sub txtHeliumFuelBlockBPME_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHeliumFuelBlockBPME.KeyDown
        Call ProcessCutCopyPasteSelect(txtHeliumFuelBlockBPME, e)
        If e.KeyCode = Keys.Enter Then
            Call SetFuelBlockBuildcost(FuelBlocks.Helium)
        End If
    End Sub

    Private Sub txtHeliumFuelBlockBPME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHeliumFuelBlockBPME.KeyPress
        Call ProcessKeyPress(e)
        HeliumFuelBlockBPUpdated = True
    End Sub

    Private Sub txtHeliumFuelBlockBPME_GotFocus(sender As Object, e As EventArgs) Handles txtHeliumFuelBlockBPME.GotFocus
        Call txtHeliumFuelBlockBPME.SelectAll()
    End Sub

    Private Sub txtHydrogenFuelBlockBPME_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHydrogenFuelBlockBPME.KeyDown
        Call ProcessCutCopyPasteSelect(txtHydrogenFuelBlockBPME, e)
        If e.KeyCode = Keys.Enter Then
            Call SetFuelBlockBuildcost(FuelBlocks.Hydrogen)
        End If
    End Sub

    Private Sub txtHydrogenFuelBlockBPME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHydrogenFuelBlockBPME.KeyPress
        Call ProcessKeyPress(e)
        HydrogenFuelBlockBPUpdated = True
    End Sub

    Private Sub txtHydrogenFuelBlockBPME_GotFocus(sender As Object, e As EventArgs) Handles txtHydrogenFuelBlockBPME.GotFocus
        Call txtHydrogenFuelBlockBPME.SelectAll()
    End Sub

    Private Sub txtNitrogenFuelBlockBPME_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNitrogenFuelBlockBPME.KeyDown
        Call ProcessCutCopyPasteSelect(txtNitrogenFuelBlockBPME, e)
        If e.KeyCode = Keys.Enter Then
            Call SetFuelBlockBuildcost(FuelBlocks.Nitrogen)
        End If
    End Sub

    Private Sub txtNitrogenFuelBlockBPME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNitrogenFuelBlockBPME.KeyPress
        Call ProcessKeyPress(e)
        NitrogenFuelBlockBPUpdated = True
    End Sub

    Private Sub txtNitrogenFuelBlockBPME_GotFocus(sender As Object, e As EventArgs) Handles txtNitrogenFuelBlockBPME.GotFocus
        Call txtNitrogenFuelBlockBPME.SelectAll()
    End Sub

    Private Sub txtOxygenFuelBlockBPME_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOxygenFuelBlockBPME.KeyDown
        Call ProcessCutCopyPasteSelect(txtOxygenFuelBlockBPME, e)
        If e.KeyCode = Keys.Enter Then
            Call SetFuelBlockBuildcost(FuelBlocks.Oxygen)
        End If
    End Sub

    Private Sub txtOxygenFuelBlockBPME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOxygenFuelBlockBPME.KeyPress
        Call ProcessKeyPress(e)
        OxygenFuelBlockBPUpdated = True
    End Sub

    Private Sub txtOxygenFuelBlockBPME_GotFocus(sender As Object, e As EventArgs) Handles txtOxygenFuelBlockBPME.GotFocus
        Call txtOxygenFuelBlockBPME.SelectAll()
    End Sub

    Private Sub txtHeliumFuelBlockBuyPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHeliumFuelBlockBuyPrice.KeyDown
        Call ProcessBlockBuyKeyDown(e, txtHeliumFuelBlockBuyPrice)
    End Sub

    Private Sub txtHeliumFuelBlockBuyPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHeliumFuelBlockBuyPrice.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtHeliumFuelBlockBuyPrice_GotFocus(sender As Object, e As EventArgs) Handles txtHeliumFuelBlockBuyPrice.GotFocus
        Call txtHeliumFuelBlockBuyPrice.SelectAll()
    End Sub

    Private Sub txtHydrogenFuelBlockBuyPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHydrogenFuelBlockBuyPrice.KeyDown
        Call ProcessBlockBuyKeyDown(e, txtHydrogenFuelBlockBuyPrice)
    End Sub

    Private Sub txtHydrogenFuelBlockBuyPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHydrogenFuelBlockBuyPrice.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtHydrogenFuelBlockBuyPrice_GotFocus(sender As Object, e As EventArgs) Handles txtHydrogenFuelBlockBuyPrice.GotFocus
        Call txtHydrogenFuelBlockBuyPrice.SelectAll()
    End Sub

    Private Sub txtNitrogenFuelBlockBuyPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNitrogenFuelBlockBuyPrice.KeyDown
        Call ProcessBlockBuyKeyDown(e, txtNitrogenFuelBlockBuyPrice)
    End Sub

    Private Sub txtNitrogenFuelBlockBuyPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNitrogenFuelBlockBuyPrice.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtNitrogenFuelBlockBuyPrice_GotFocus(sender As Object, e As EventArgs) Handles txtNitrogenFuelBlockBuyPrice.GotFocus
        Call txtNitrogenFuelBlockBuyPrice.SelectAll()
    End Sub

    Private Sub txtOxygenFuelBlockBuyPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOxygenFuelBlockBuyPrice.KeyDown
        Call ProcessBlockBuyKeyDown(e, txtOxygenFuelBlockBuyPrice)
    End Sub

    Private Sub txtOxygenFuelBlockBuyPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOxygenFuelBlockBuyPrice.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtOxygenFuelBlockBuyPrice_GotFocus(sender As Object, e As EventArgs) Handles txtOxygenFuelBlockBuyPrice.GotFocus
        Call txtOxygenFuelBlockBuyPrice.SelectAll()
    End Sub

    Private Sub txtHeliumIsotopes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHeliumIsotopes.KeyDown
        Call ProcessMaterialKeyDown(e, txtHeliumIsotopes)
    End Sub

    Private Sub txtHeliumIsotopes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHeliumIsotopes.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtHeliumIsotopes_GotFocus(sender As Object, e As EventArgs) Handles txtHeliumIsotopes.GotFocus
        Call txtHeliumIsotopes.SelectAll()
    End Sub

    Private Sub txtHydrogenIsotopes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHydrogenIsotopes.KeyDown
        Call ProcessMaterialKeyDown(e, txtHydrogenIsotopes)
    End Sub

    Private Sub txtHydrogenIsotopes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHydrogenIsotopes.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtHydrogenIsotopes_GotFocus(sender As Object, e As EventArgs) Handles txtHydrogenIsotopes.GotFocus
        Call txtHydrogenIsotopes.SelectAll()
    End Sub

    Private Sub txtHeavyWater_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHeavyWater.KeyDown
        Call ProcessMaterialKeyDown(e, txtHeavyWater)
    End Sub

    Private Sub txtHeavyWater_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHeavyWater.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtHeavyWater_GotFocus(sender As Object, e As EventArgs) Handles txtHeavyWater.GotFocus
        Call txtHeavyWater.SelectAll()
    End Sub

    Private Sub txtNitrogenIsotopes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNitrogenIsotopes.KeyDown
        Call ProcessMaterialKeyDown(e, txtNitrogenIsotopes)
    End Sub

    Private Sub txtNitrogenIsotopes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNitrogenIsotopes.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtNitrogenIsotopes_GotFocus(sender As Object, e As EventArgs) Handles txtNitrogenIsotopes.GotFocus
        Call txtNitrogenIsotopes.SelectAll()
    End Sub

    Private Sub txtOxygenIsotopes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOxygenIsotopes.KeyDown
        Call ProcessMaterialKeyDown(e, txtOxygenIsotopes)
    End Sub

    Private Sub txtOxygenIsotopes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOxygenIsotopes.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtOxygenIsotopes_GotFocus(sender As Object, e As EventArgs) Handles txtOxygenIsotopes.GotFocus
        Call txtOxygenIsotopes.SelectAll()
    End Sub

    Private Sub txtStrontiumClathrates_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStrontiumClathrates.KeyDown
        Call ProcessMaterialKeyDown(e, txtStrontiumClathrates)
    End Sub

    Private Sub txtStrontiumClathrates_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStrontiumClathrates.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtStrontiumClathrates_GotFocus(sender As Object, e As EventArgs) Handles txtStrontiumClathrates.GotFocus
        Call txtStrontiumClathrates.SelectAll()
    End Sub

    Private Sub txtMechanicalParts_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMechanicalParts.KeyDown
        Call ProcessMaterialKeyDown(e, txtMechanicalParts)
    End Sub

    Private Sub txtMechanicalParts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMechanicalParts.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtMechanicalParts_GotFocus(sender As Object, e As EventArgs) Handles txtMechanicalParts.GotFocus
        Call txtMechanicalParts.SelectAll()
    End Sub

    Private Sub txtRobotics_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRobotics.KeyDown
        Call ProcessMaterialKeyDown(e, txtRobotics)
    End Sub

    Private Sub txtRobotics_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRobotics.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtRobotics_GotFocus(sender As Object, e As EventArgs) Handles txtRobotics.GotFocus
        Call txtRobotics.SelectAll()
    End Sub

    Private Sub txtCoolant_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCoolant.KeyDown
        Call ProcessMaterialKeyDown(e, txtCoolant)
    End Sub

    Private Sub txtCoolant_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCoolant.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtCoolant_GotFocus(sender As Object, e As EventArgs) Handles txtCoolant.GotFocus
        Call txtCoolant.SelectAll()
    End Sub

    Private Sub txtEnrichedUranium_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEnrichedUranium.KeyDown
        Call ProcessMaterialKeyDown(e, txtEnrichedUranium)
    End Sub

    Private Sub txtEnrichedUranium_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEnrichedUranium.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtEnrichedUranium_GotFocus(sender As Object, e As EventArgs) Handles txtEnrichedUranium.GotFocus
        Call txtEnrichedUranium.SelectAll()
    End Sub

    Private Sub txtOxygen_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOxygen.KeyDown
        Call ProcessMaterialKeyDown(e, txtOxygen)
    End Sub

    Private Sub txtOxygen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOxygen.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtOxygen_GotFocus(sender As Object, e As EventArgs) Handles txtOxygen.GotFocus
        Call txtOxygen.SelectAll()
    End Sub

    Private Sub txtLiquidOzone_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLiquidOzone.KeyDown
        Call ProcessMaterialKeyDown(e, txtLiquidOzone)
    End Sub

    Private Sub txtLiquidOzone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLiquidOzone.KeyPress
        Call ProcessKeyPress(e)
    End Sub

    Private Sub txtLiquidOzone_GotFocus(sender As Object, e As EventArgs) Handles txtLiquidOzone.GotFocus
        Call txtLiquidOzone.SelectAll()
    End Sub

    Private Sub ShowToolTipForModule(ByRef SentSender As Object)
        Dim SO As PictureBox
        SO = CType(SentSender, PictureBox)
        If SO.Text <> "" Then
            MainToolTip.SetToolTip(SO, SO.Text)
        End If
    End Sub

    Private Sub RigSlot1_MouseEnter(sender As Object, e As EventArgs) Handles RigSlot1.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub RigSlot2_MouseEnter(sender As Object, e As EventArgs) Handles RigSlot2.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub RigSlot3_MouseEnter(sender As Object, e As EventArgs) Handles RigSlot3.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot1_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot1.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot2_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot2.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot3_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot3.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot4_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot4.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot5_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot5.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot6_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot6.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot7_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot7.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub MidSlot8_MouseEnter(sender As Object, e As EventArgs) Handles MidSlot8.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub ServiceSlot5_MouseEnter(sender As Object, e As EventArgs) Handles ServiceSlot5.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub ServiceSlot3_MouseEnter(sender As Object, e As EventArgs) Handles ServiceSlot3.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub ServiceSlot1_MouseEnter(sender As Object, e As EventArgs) Handles ServiceSlot1.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub ServiceSlot2_MouseEnter(sender As Object, e As EventArgs) Handles ServiceSlot2.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub ServiceSlot4_MouseEnter(sender As Object, e As EventArgs) Handles ServiceSlot4.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub ServiceSlot6_MouseEnter(sender As Object, e As EventArgs) Handles ServiceSlot6.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot8_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot8.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot7_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot7.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot6_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot6.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot5_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot5.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot4_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot4.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot3_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot3.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot2_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot2.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub LowSlot1_MouseEnter(sender As Object, e As EventArgs) Handles LowSlot1.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot7_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot7.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot8_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot8.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot5_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot5.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot3_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot3.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot1_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot1.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot2_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot2.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot4_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot4.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub HighSlot6_MouseEnter(sender As Object, e As EventArgs) Handles HighSlot6.MouseEnter
        Call ShowToolTipForModule(sender)
    End Sub

    Private Sub lstUpwellStructureBonuses_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstUpwellStructureBonuses.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstUpwellStructureBonuses, ListView), ColumnClicked, ColumnSortType)
    End Sub

    Private Sub FittingListViewDetails_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles FittingListViewDetails.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(FittingListViewDetails, ListView), ModulesColumnClicked, ModulesColumnSortType)
    End Sub

    Private Sub rbtnListView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnListView.CheckedChanged
        Call DisplayListView()
    End Sub

    Private Sub rbtnViewIcons_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnViewIcons.CheckedChanged
        Call DisplayListView()
    End Sub

    Private Sub DisplayListView()
        If rbtnViewIcons.Checked Then
            FittingListViewIcons.Visible = True
            FittingListViewDetails.Visible = False
        Else
            FittingListViewIcons.Visible = False
            FittingListViewDetails.Visible = True
        End If
    End Sub

#End Region

End Class

Public Enum Services
    StandupBiochemicalReactor = 45539 ' Boosters
    StandupCompositeReactor = 45537 ' Moon mats
    StandupHybridReactor = 45538 ' T3 mats
    StandupManufacturingPlant = 35878
    StandupCapitalShipyard = 35881
    StandupSupercapitalShipyard = 35877

    StandupInventionLab = 35886 ' Invention
    StandupResearchLab = 35891 ' Copying, ME/TE research
    StandupHyasyodaResearchLab = 45550 ' Copying, ME/TE research
    StandupReprocessingFaclity = 35899
End Enum
