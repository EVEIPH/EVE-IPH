Imports System.Data.SQLite

Public Class IndustryFacility
    Implements ICloneable

    ' For industry Facilities
    Public FacilityID As Long ' ID Of the facility
    Public FacilityName As String ' Station/Outpost Name or the Array name
    Public FacilityType As String ' POS, Station, Outpost
    Public FacilityTypeID As Long ' type ID for facility - type of outpost, etc
    Public ProductionType As IndustryType ' What we are doing at this facility
    Public Activity As String ' Activity type of the facility
    Public ActivityID As Integer
    Public RegionName As String ' Region of this facility
    Public RegionID As Long
    Public SolarSystemName As String ' System where this is located
    Public SolarSystemID As Long
    Public TaxRate As Double ' The tax rate
    Public MaterialMultiplier As Double ' The bonus material percentage for materials used in this facility
    Public TimeMultiplier As Double ' The bonus to time to conduct an activity in this facility
    Public CostIndex As Double ' Cost index for the system and activity from CREST
    Public ActivityCostPerSecond As Double ' The cost to conduct the activity for this facility per second - my setting for POS data
    Public IsDefault As Boolean
    Public IncludeActivityCost As Boolean ' This is the total cost of materials to do the activiy
    Public IncludeActivityTime As Boolean ' This is the time for doing the activity
    Public IncludeActivityUsage As Boolean ' This is the cost of using the facility only

    Public Sub New()

        FacilityID = 0
        FacilityName = None
        FacilityType = None
        ProductionType = IndustryType.None
        Activity = None
        ActivityID = 0
        RegionName = None
        RegionID = 0
        SolarSystemName = None
        SolarSystemID = 0
        TaxRate = 0
        MaterialMultiplier = 0
        TimeMultiplier = 0
        ActivityCostPerSecond = 0

        IncludeActivityCost = False
        IncludeActivityTime = False
        IncludeActivityUsage = False

        IsDefault = False
    End Sub

    ' Loads the facility from settings - later will probably just save all the data for this class to an XML settings file instead of this duplicate lookup
    Public Sub LoadFacility(SearchFacilitySettings As FacilitySettings, FacilityDefault As Boolean)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim Defaults As New ProgramSettings

        ' First, figure out the type of facility if the type is set to None (bandaid bug fix for now - don't know why saving these pos facilities saves type as "None")
        ' Only look up factory if we have an id
        If SearchFacilitySettings.FacilityType = None And (SearchFacilitySettings.Facility <> "" Or SearchFacilitySettings.FacilityType <> None) Then
            ' Try to look up the facility name first and if you find it, then set the type, else send it on
            SQL = "SELECT * FROM (SELECT DISTINCT ARRAY_NAME AS FACILITY_NAME, 'POS' AS FACILITY_TYPE FROM ASSEMBLY_ARRAYS UNION "
            SQL = SQL & "SELECT DISTINCT FACILITY_NAME, CASE WHEN OUTPOST = 0 THEN 'STATION' ELSE 'OUTPOST' END AS FACILITY_TYPE FROM STATION_FACILITIES) AS X "
            SQL = SQL & "WHERE FACILITY_NAME = '" & SearchFacilitySettings.Facility & "'"

            DBCommand = New SQLiteCommand(SQL, DB)
            rsLoader = DBCommand.ExecuteReader

            If rsLoader.Read() Then
                SearchFacilitySettings.FacilityType = rsLoader.GetString(1)
            End If

            rsLoader.Close()

        ElseIf SearchFacilitySettings.Facility = "" Then
            ' Set it to default for the production type, use BP settings
            Select Case SearchFacilitySettings.ProductionType
                Case IndustryType.BoosterManufacturing
                    SearchFacilitySettings.Facility = DefaultBPBoosterManufacturingFacility.FacilityName
                Case IndustryType.CapitalComponentManufacturing
                    SearchFacilitySettings.Facility = DefaultBPCapitalComponentManufacturingFacility.FacilityName
                Case IndustryType.CapitalManufacturing
                    SearchFacilitySettings.Facility = DefaultBPCapitalManufacturingFacility.FacilityName
                Case IndustryType.ComponentManufacturing
                    SearchFacilitySettings.Facility = DefaultBPComponentManufacturingFacility.FacilityName
                Case IndustryType.Copying
                    SearchFacilitySettings.Facility = DefaultBPCopyFacility.FacilityName
                Case IndustryType.Invention
                    SearchFacilitySettings.Facility = DefaultBPInventionFacility.FacilityName
                Case IndustryType.Manufacturing
                    SearchFacilitySettings.Facility = DefaultBPManufacturingFacility.FacilityName
                Case IndustryType.NoPOSManufacturing
                    SearchFacilitySettings.Facility = DefaultBPNoPOSFacility.FacilityName
                Case IndustryType.POSFuelBlockManufacturing
                    SearchFacilitySettings.Facility = DefaultBPPOSFuelBlockFacility.FacilityName
                Case IndustryType.POSLargeShipManufacturing
                    SearchFacilitySettings.Facility = DefaultBPPOSLargeShipFacility.FacilityName
                Case IndustryType.POSModuleManufacturing
                    SearchFacilitySettings.Facility = DefaultBPPOSModuleFacility.FacilityName
                Case IndustryType.SubsystemManufacturing
                    SearchFacilitySettings.Facility = DefaultBPSubsystemManufacturingFacility.FacilityName
                Case IndustryType.SuperManufacturing
                    SearchFacilitySettings.Facility = DefaultBPSuperManufacturingFacility.FacilityName
                Case IndustryType.T3CruiserManufacturing
                    SearchFacilitySettings.Facility = DefaultBPT3CruiserManufacturingFacility.FacilityName
                Case IndustryType.T3DestroyerManufacturing
                    SearchFacilitySettings.Facility = DefaultBPT3DestroyerManufacturingFacility.FacilityName
                Case IndustryType.T3Invention
                    SearchFacilitySettings.Facility = DefaultBPT3InventionFacility.FacilityName
                Case Else
                    SearchFacilitySettings.Facility = DefaultBPManufacturingFacility.FacilityName
            End Select
        ElseIf SearchFacilitySettings.Facility = None Then
            ' If none, then set the facility based on entry and exit
            With SearchFacilitySettings
                FacilityID = 0 ' not used
                FacilityName = .Facility
                FacilityType = .FacilityType
                ProductionType = .ProductionType
                Select Case ProductionType
                    Case IndustryType.CapitalComponentManufacturing
                        Activity = ActivityCapComponentManufacturing
                        ActivityID = 1
                    Case IndustryType.ComponentManufacturing
                        Activity = ActivityComponentManufacturing
                        ActivityID = 1
                    Case IndustryType.Copying
                        Activity = ActivityCopying
                        ActivityID = 7
                    Case IndustryType.Invention
                        Activity = ActivityInvention
                        ActivityID = 8
                    Case Else
                        Activity = ActivityManufacturing
                        ActivityID = 1
                End Select
                RegionName = .RegionName
                RegionID = .RegionID
                SolarSystemName = .SolarSystemName
                SolarSystemID = .SolarSystemID
                TaxRate = .TaxRate
                MaterialMultiplier = .MaterialMultiplier
                TimeMultiplier = .TimeMultiplier
                ActivityCostPerSecond = .ActivityCostperSecond

                IsDefault = False
            End With

            Exit Sub
        End If

        ' When loading a POS Facility with multi-use or Component array, which allows 'All' then do special processing
        With SearchFacilitySettings
            If .FacilityType = POSFacility And (.Facility = "All" Or .Facility = "Component" Or .Facility = "Large" _
                                                Or .Facility = "Equipment") Then
                ' Set the name to what was sent, save the sent info, and exit. This will only happen with the manufacturing tab section
                SQL = "SELECT '" & .Facility & "' AS FACILITY_NAME, "
                ' Need to load location from the settings since the location is specific to the user
                SQL = SQL & "'" & .RegionName & "' AS REGION_NAME, " & CStr(.RegionID) & " AS REGION_ID, '"
                SQL = SQL & .SolarSystemName & "' AS SOLAR_SYSTEM_NAME, " & CStr(.SolarSystemID) & " AS SOLAR_SYSTEM_ID, " & CStr(POSTaxRate) & " AS FACILITY_TAX, COST_INDEX, "
                SQL = SQL & "MATERIAL_MULTIPLIER AS MATERIAL_MULTIPLIER, TIME_MULTIPLIER AS TIME_MULTIPLIER, "
                SQL = SQL & "ASSEMBLY_ARRAYS.ACTIVITY_ID AS AID, ASSEMBLY_ARRAYS.ACTIVITY_NAME AS AN, ARRAY_TYPE_ID AS FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID "
                SQL = SQL & "FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES "
                SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
                SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "
                SQL = SQL & "AND AID = " & CStr(.ActivityID) & " "

                If .Facility <> "All" Then
                    ' Must be a fuel block, module, or large ship array choice
                    Select Case .Facility
                        Case "Component"
                            SQL = SQL & "AND ARRAY_NAME LIKE 'Component%' AND GROUP_NAME = 'Fuel Block' "
                        Case "Large"
                            SQL = SQL & "AND ARRAY_NAME LIKE 'Large%' AND GROUP_NAME = 'Battleship' "
                        Case "Equipment"
                            SQL = SQL & "AND ARRAY_NAME LIKE 'Equipment%' AND CATEGORY_NAME = 'Module' "
                    End Select
                End If

                DBCommand = New SQLiteCommand(SQL, DB)
                rsLoader = DBCommand.ExecuteReader
                rsLoader.Read()

                If rsLoader.HasRows Then
                    FacilityType = .FacilityType
                    FacilityName = rsLoader.GetString(0)
                    ProductionType = .ProductionType
                    RegionName = rsLoader.GetString(1)
                    RegionID = rsLoader.GetInt64(2)
                    SolarSystemName = rsLoader.GetString(3)
                    SolarSystemID = rsLoader.GetInt64(4)
                    TaxRate = rsLoader.GetDouble(5)
                    CostIndex = rsLoader.GetFloat(6)
                    MaterialMultiplier = rsLoader.GetDouble(7)
                    TimeMultiplier = rsLoader.GetDouble(8)
                    ActivityID = rsLoader.GetInt32(9)
                    Activity = rsLoader.GetString(10)
                    ActivityCostPerSecond = .ActivityCostperSecond
                    IsDefault = FacilityDefault
                    FacilityTypeID = rsLoader.GetInt64(11)

                    IncludeActivityCost = .IncludeActivityCost
                    IncludeActivityTime = .IncludeActivityTime
                    IncludeActivityUsage = .IncludeActivityUsage

                    rsLoader.Close()
                    rsLoader = Nothing
                    DBCommand = Nothing

                End If

                ' We set this, now leave
                Exit Sub

            End If
        End With

        With SearchFacilitySettings
            Select Case .FacilityType
                Case POSFacility
                    SQL = "SELECT ARRAY_NAME AS FACILITY_NAME, "
                    ' Need to load location from the settings since the location is specific to the user
                    SQL = SQL & "'" & .RegionName & "' AS REGION_NAME, " & CStr(.RegionID) & " AS REGION_ID, '"
                    SQL = SQL & .SolarSystemName & "' AS SOLAR_SYSTEM_NAME, " & CStr(.SolarSystemID) & " AS SOLAR_SYSTEM_ID, " & CStr(POSTaxRate) & " AS FACILITY_TAX, COST_INDEX, "
                    SQL = SQL & "MATERIAL_MULTIPLIER AS MATERIAL_MULTIPLIER, TIME_MULTIPLIER AS TIME_MULTIPLIER, "
                    SQL = SQL & "ASSEMBLY_ARRAYS.ACTIVITY_ID AS AID, ASSEMBLY_ARRAYS.ACTIVITY_NAME AS AN, ARRAY_TYPE_ID AS FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID "
                    SQL = SQL & "FROM ASSEMBLY_ARRAYS, INDUSTRY_SYSTEMS_COST_INDICIES "
                    SQL = SQL & "WHERE ASSEMBLY_ARRAYS.ACTIVITY_ID = INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID "
                    SQL = SQL & "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = " & .SolarSystemID & " "

                Case OutpostFacility
                    SQL = "SELECT FACILITY_NAME, REGION_NAME, REGION_ID, "
                    SQL = SQL & "SOLAR_SYSTEM_NAME, SOLAR_SYSTEM_ID, FACILITY_TAX, COST_INDEX, "
                    ' Check the values sent to see if they set it to something instead of loading from DB
                    If .MaterialMultiplier <> Defaults.FacilityDefaultMM Then
                        ' They didn't set a value, so load the default for each type
                        SQL = SQL & CStr(.MaterialMultiplier) & " AS MATERIAL_MULTIPLIER, "
                    Else
                        SQL = SQL & "MATERIAL_MULTIPLIER, "
                    End If

                    If .TimeMultiplier <> Defaults.FacilityDefaultMM Then
                        ' They didn't set a value, so load the default for each type
                        SQL = SQL & CStr(.TimeMultiplier) & " AS TIME_MULTIPLIER, "
                    Else
                        SQL = SQL & "TIME_MULTIPLIER, "
                    End If
                    SQL = SQL & "ACTIVITY_ID AS AID, ACTIVITY_NAME AS AN, FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID "
                    SQL = SQL & "FROM STATION_FACILITIES "
                    SQL = SQL & "WHERE OUTPOST = " & CStr(StationType.Outpost) & " "
                Case StationFacility
                    SQL = "SELECT FACILITY_NAME, REGION_NAME, REGION_ID, "
                    SQL = SQL & "SOLAR_SYSTEM_NAME, SOLAR_SYSTEM_ID, FACILITY_TAX, COST_INDEX, "
                    SQL = SQL & "MATERIAL_MULTIPLIER, TIME_MULTIPLIER, "
                    SQL = SQL & "ACTIVITY_ID AS AID, ACTIVITY_NAME AS AN, FACILITY_TYPE_ID, GROUP_ID, CATEGORY_ID "
                    SQL = SQL & "FROM STATION_FACILITIES "
                    SQL = SQL & "WHERE OUTPOST = " & CStr(StationType.Station) & " "
            End Select

            SQL = SQL & "AND FACILITY_NAME = '" & FormatDBString(SearchFacilitySettings.Facility) & "' "
            SQL = SQL & "AND AID = " & CStr(.ActivityID) & " "

            If .FacilityType = POSFacility Then
                Select Case .ProductionType
                    Case IndustryType.CapitalManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(CapitalIndustrialShipGroupID) & ", " & CStr(CarrierGroupID) & ", " & CStr(DreadnoughtGroupID) & ") "
                    Case IndustryType.SuperManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(TitanGroupID) & ", " & CStr(SupercarrierGroupID) & ") "
                    Case IndustryType.BoosterManufacturing
                        SQL = SQL & "AND GROUP_ID = " & BoosterGroupID & " "
                    Case IndustryType.T3CruiserManufacturing
                        SQL = SQL & "AND GROUP_ID = " & StrategicCruiserGroupID & " "
                    Case IndustryType.SubsystemManufacturing
                        SQL = SQL & "AND CATEGORY_ID = " & SubsystemCategoryID & " "
                    Case IndustryType.ComponentManufacturing
                        SQL = SQL & "AND GROUP_ID = " & ConstructionComponentsGroupID & " "
                    Case IndustryType.CapitalComponentManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(AdvCapitalComponentGroupID) & ", " & CStr(CapitalComponentGroupID) & ") "
                End Select
            Else ' Stations
                Select Case .ProductionType
                    Case IndustryType.CapitalManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(CapitalIndustrialShipGroupID) & ", " & CStr(CarrierGroupID) & ", " & CStr(DreadnoughtGroupID) & ") "
                    Case IndustryType.SuperManufacturing
                        SQL = SQL & "AND GROUP_ID IN (" & CStr(TitanGroupID) & ", " & CStr(SupercarrierGroupID) & ") "
                    Case IndustryType.BoosterManufacturing
                        SQL = SQL & "AND GROUP_ID = " & BoosterGroupID & " "
                    Case IndustryType.T3CruiserManufacturing
                        SQL = SQL & "AND GROUP_ID = " & StrategicCruiserGroupID & " "
                    Case IndustryType.SubsystemManufacturing
                        SQL = SQL & "AND CATEGORY_ID = " & SubsystemCategoryID & " "
                    Case IndustryType.ComponentManufacturing, IndustryType.CapitalComponentManufacturing
                        SQL = SQL & "AND CATEGORY_ID = " & ComponentCategoryID & " "
                End Select
            End If

            SQL = SQL & "GROUP BY FACILITY_NAME, REGION_NAME, REGION_ID, SOLAR_SYSTEM_NAME, SOLAR_SYSTEM_ID, FACILITY_TAX, "
            SQL = SQL & "COST_INDEX, MATERIAL_MULTIPLIER, TIME_MULTIPLIER, AID, AN, FACILITY_TYPE_ID"

        End With

        DBCommand = New SQLiteCommand(SQL, DB)
        rsLoader = DBCommand.ExecuteReader
        rsLoader.Read()

        If rsLoader.HasRows Then
            FacilityType = SearchFacilitySettings.FacilityType
            FacilityName = rsLoader.GetString(0)
            ProductionType = SearchFacilitySettings.ProductionType
            RegionName = rsLoader.GetString(1)
            RegionID = rsLoader.GetInt64(2)
            SolarSystemName = rsLoader.GetString(3)
            SolarSystemID = rsLoader.GetInt64(4)
            CostIndex = rsLoader.GetFloat(6)
            If SearchFacilitySettings.FacilityType = OutpostFacility Then
                MaterialMultiplier = SearchFacilitySettings.MaterialMultiplier
                TimeMultiplier = SearchFacilitySettings.TimeMultiplier
                TaxRate = SearchFacilitySettings.TaxRate
            Else
                MaterialMultiplier = rsLoader.GetDouble(7)
                TimeMultiplier = rsLoader.GetDouble(8)
                TaxRate = rsLoader.GetDouble(5)
            End If
            ActivityID = rsLoader.GetInt32(9)
            Activity = rsLoader.GetString(10)
            ActivityCostPerSecond = SearchFacilitySettings.ActivityCostperSecond
            IsDefault = FacilityDefault
            FacilityTypeID = rsLoader.GetInt64(11)

            IncludeActivityCost = SearchFacilitySettings.IncludeActivityCost
            IncludeActivityTime = SearchFacilitySettings.IncludeActivityTime
            IncludeActivityUsage = SearchFacilitySettings.IncludeActivityUsage

            rsLoader.Close()
            rsLoader = Nothing
            DBCommand = Nothing
        Else
            ' Just use everything we can from the search settings and set the others to defaults (ie. if they don't have the index table loaded, this will happen)
            FacilityType = SearchFacilitySettings.FacilityType
            FacilityName = SearchFacilitySettings.Facility
            ProductionType = SearchFacilitySettings.ProductionType
            RegionName = SearchFacilitySettings.RegionName
            RegionID = SearchFacilitySettings.RegionID
            SolarSystemName = SearchFacilitySettings.SolarSystemName
            SolarSystemID = SearchFacilitySettings.SolarSystemID
            TaxRate = 0
            CostIndex = 0
            MaterialMultiplier = SearchFacilitySettings.MaterialMultiplier
            TimeMultiplier = SearchFacilitySettings.TimeMultiplier
            ActivityID = SearchFacilitySettings.ActivityID
            Select Case ActivityID
                Case 7
                    Activity = ActivityCopying
                Case 8
                    Activity = ActivityInvention
                Case Else
                    Activity = ActivityManufacturing
            End Select
            ActivityCostPerSecond = SearchFacilitySettings.ActivityCostperSecond
            IsDefault = FacilityDefault
            FacilityTypeID = 0

            IncludeActivityCost = SearchFacilitySettings.IncludeActivityCost
            IncludeActivityTime = SearchFacilitySettings.IncludeActivityTime
            IncludeActivityUsage = SearchFacilitySettings.IncludeActivityUsage
        End If

    End Sub

    Public Sub SaveFacility(Tab As String)
        ' If we are saving, then set the data and then save
        Dim SaveSettings As New FacilitySettings

        ' Save data
        With SaveSettings
            .Facility = FacilityName
            .FacilityType = FacilityType
            .ProductionType = ProductionType
            .ActivityID = ActivityID
            .MaterialMultiplier = MaterialMultiplier
            .TimeMultiplier = TimeMultiplier
            .TaxRate = TaxRate
            .SolarSystemID = SolarSystemID
            .SolarSystemName = SolarSystemName
            .RegionID = RegionID
            .RegionName = RegionName
            .ActivityCostperSecond = ActivityCostPerSecond
            .IncludeActivityUsage = IncludeActivityUsage

            If Not IsNothing(IncludeActivityCost) Then
                .IncludeActivityCost = IncludeActivityCost
            End If
            If Not IsNothing(IncludeActivityTime) Then
                .IncludeActivityTime = IncludeActivityTime
            End If
        End With

        Call AllSettings.FacilitySaveSettings(SaveSettings, ProductionType, Tab)

    End Sub

    ' Compares the sent facility to the current one and returns a boolean on equivlancy
    Public Function IsEqual(CompareFacility As IndustryFacility, Optional CompareCostCheck As Boolean = False, Optional CompareTimeCheck As Boolean = False) As Boolean

        With CompareFacility
            If .FacilityType <> FacilityType Then
                Return False
            ElseIf .ProductionType <> ProductionType Then
                Return False
            ElseIf .Activity <> Activity Then
                Return False
            ElseIf .ActivityID <> ActivityID Then
                Return False
            ElseIf .FacilityName <> FacilityName And Not (.FacilityType = POSFacility And ProductionType = IndustryType.Manufacturing) Then
                Return False
            ElseIf .RegionName <> RegionName Then
                Return False
            ElseIf .RegionID <> RegionID Then
                Return False
            ElseIf .SolarSystemName <> SolarSystemName Then
                Return False
            ElseIf .SolarSystemID <> SolarSystemID Then
                Return False
            ElseIf .TaxRate <> TaxRate Then
                Return False
            ElseIf .MaterialMultiplier <> MaterialMultiplier And .FacilityType <> POSFacility Then ' Only for non-pos
                Return False
            ElseIf .TimeMultiplier <> TimeMultiplier And .FacilityType <> POSFacility Then ' Only for non-pos
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

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New IndustryFacility

        CopyOfMe.FacilityName = FacilityName
        CopyOfMe.FacilityType = FacilityType
        CopyOfMe.FacilityTypeID = FacilityTypeID
        CopyOfMe.ProductionType = ProductionType
        CopyOfMe.Activity = Activity
        CopyOfMe.ActivityID = ActivityID
        CopyOfMe.RegionName = RegionName
        CopyOfMe.RegionID = RegionID
        CopyOfMe.SolarSystemName = SolarSystemName
        CopyOfMe.SolarSystemID = SolarSystemID
        CopyOfMe.TaxRate = TaxRate
        CopyOfMe.MaterialMultiplier = MaterialMultiplier
        CopyOfMe.TimeMultiplier = TimeMultiplier
        CopyOfMe.CostIndex = CostIndex
        CopyOfMe.ActivityCostPerSecond = ActivityCostPerSecond
        CopyOfMe.IsDefault = IsDefault
        CopyOfMe.IncludeActivityCost = IncludeActivityCost
        CopyOfMe.IncludeActivityTime = IncludeActivityTime
        CopyOfMe.IncludeActivityUsage = IncludeActivityUsage

        Return CopyOfMe

    End Function

End Class

Public Module FacilityVariables

    ' Selected facilities
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

    ' Selected facilities
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

    ' Special cases for POS Facilities where items can be produced at more than one array
    Public SelectedCalcPOSFuelBlockFacility As New IndustryFacility
    Public SelectedCalcPOSLargeShipFacility As New IndustryFacility
    Public SelectedCalcPOSModuleFacility As New IndustryFacility

    Public DefaultCalcPOSFuelBlockFacility As New IndustryFacility
    Public DefaultCalcPOSLargeShipFacility As New IndustryFacility
    Public DefaultCalcPOSModuleFacility As New IndustryFacility

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
    Public Enum IndustryType
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

End Module