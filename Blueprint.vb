
' Class for Blueprint functions
Imports System.Data.SQLite

Public Class Blueprint

    ' Base variables
    Private BlueprintID As Integer
    Private BaseBP As Boolean ' for knowing the base bp opposed to component bps
    Private BlueprintName As String
    Private BlueprintGroupID As Integer
    Private ItemID As Long
    Private ItemName As String
    Private ItemCategoryID As Integer
    Private ItemGroup As String
    Private ItemGroupID As Integer
    Private TechLevel As Integer
    Private PortionSize As Long ' Number of items produced by one run of blueprint
    Private BaseProductionTime As Long ' In seconds
    Private MaxProductionLimit As Integer
    Private ItemType As Integer
    Private BlueprintRace As Integer
    Private ItemVolume As Double ' Volume of produced item (1 item only)

    ' If we compare the components for building or buying
    Private BuildBuy As Boolean
    Private HasBuildableComponents As Boolean = False
    Private AdditionalCosts As Double

    ' Helps determine if this is a component that might need special processing
    Private BBItemtoFind As Long
    Private BBList As New List(Of BuildBuyItem)
    Private NewBPRequest As Boolean

    ' Taxes/Fees
    '   •	Buy - When you buy something off the market (Buy from someone’s Sell Order – So Minimum Sell), you don’t pay taxes or broker fees
    '       o	No Tax, No Broker Fee
    '   •	Sell Order - When you set up a sell order, you pay broker fees up front and taxes for items when sold. (This will be min sell usually)
    '       o	Tax, Broker Fee
    '   •	Buy Order - When you set up buy order, you pay broker fees up front but no tax when someone sells to you. (This is max buy usually).
    '       o	No Tax, Broker Fee
    '   •	Sell - When you Sell to a buy order (simple sell), you only pay taxes. (This will be Max buy)
    '       o 	Tax, No Broker Fee

    Private Taxes As Double ' See Above - Sell Order or Sell
    Private DisplayTaxes As Double ' Public updatable number for display updates, for easy updates when clicked
    Private BrokerFees As Double ' See above - Sell Order or Buy Order
    Private DisplayBrokerFees As Double ' Public updatable number for display updates, for easy updates when clicked

    ' New cost variables
    Private EIV As Double ' Estimated Item Value - Total per material used * average price 
    Private BaseCopyJobCost As Double ' Total job cost for copying (need to use the BPC job cost)
    Private BaseInventionJobCost As Double ' Total job cost for invention (need to use the BPC job cost)

    ' Base Fees for activity
    Private JobFee As Double
    Private AlphaCloneTax As Double
    Private TotalUsage As Double

    ' How much it costs to use each facility to manufacture items and parts
    Private ManufacturingFacilityUsage As Double
    Private IncludeManufacturingUsage As Boolean
    Private ReprocessingUsage As Double
    Private ComponentFacilityUsage As Double
    Private CapComponentFacilityUsage As Double
    Private ReactionFacilityUsage As Double ' This stores the main reaction usage if the reaction is a composite and has other reactions
    Private TotalReactionFacilityUsage As Double ' Total of all reaction facilities usage for totaling all reactions below a composite or collection of composites

    ' Variables for calcuations
    Private BPProductionTime As Double ' Production Time for 1 Run of Blueprint 
    Private TotalProductionTime As Double ' Production Time for 1 run of BP plus any components (this is to compare buying components vs. making them)
    Private iME As Integer ' ME of Blueprint
    Private iTE As Integer ' TE of Blueprint
    Private UserRuns As Long ' Number of runs for blueprint the user selects
    Private NumberofBlueprints As Integer ' Number of blueprints that the user is running
    Private NumberofProductionLines As Integer ' Number of production lines the user is using
    Private NumberofLaboratoryLines As Integer ' Number of laboratory lines the user is using
    Private ComponentProductionTimes As New List(Of Double) ' A list of production times for components in this BP

    ' Character skills we are making this blueprint with
    Private BPCharacter As Character ' The character for this BP
    Private IndustrySkill As Integer ' Industry skill level of character
    Private AdvancedIndustrySkill As Integer ' Old Production Efficiency skill, now reduces TE on building, reactions, researching
    Private ScienceSkill As Integer
    Private AIImplantValue As Double ' Advanced Industry Implant on character
    Private CopyImplantValue As Double ' Copy implant value for this character

    Private CharAdvIndustialShipConstruction As Integer ' Industry skill for advanced indy ships (T2)
    Private CharAdvCapitalShipConstruction As Integer ' Industry skill for advanced Cap ships (T2) - Lancers

    Private Const AdvISCSkill As Integer = 3396
    Private Const AdvCSCSkill As Integer = 77725

    ' Can do variables
    Private CanInventRE As Boolean ' if the sent character for the blueprint can invent it from a T1 or artifact
    Private CanBuildBP As Boolean ' if the user can build this BP
    Private CanBuildAll As Boolean ' if the user can build this BP and all components

    ' Material lists
    Public RawMaterials As Materials ' The list of All Raw materials for this item including the raw mats to make the buildable components in info list
    Public ComponentMaterials As Materials ' List of all the required materials to make the item as shown in info list
    Public BuiltComponentList As BuiltItemList ' Saving all the materials for each built component
    Private BPRawMats As Materials ' Saves all the raw materials on the bp that are not built

    ' Skills required to make it
    Private ReqBuildSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels) ' Just this BP
    Private ReqBuildComponentSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels) ' All the skills to build just the components

    ' Invention variables
    Private MaxRunsPerBP As Integer ' The max runs for a copy or invented bpc. Zero is unlimited runs
    Private ReqInventionSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels) ' For inventing this BP
    Private ReqCopySkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels) ' For copying the BPC
    Public InventionMaterials As Materials
    Public CopyMaterials As Materials ' Some copies require items
    Private InventionChance As Double
    Private InventionDecryptor As New Decryptor
    Private Relic As String ' Name of relic
    Private TotalInventedRuns As Integer ' Number of runs all the invention jobs will produce
    Private SingleInventedBPCRuns As Integer ' The runs on one bp invented
    Private NumInventionJobs As Integer ' Number of invention jobs we will do
    Private PerInventionRunCost As Double ' The cost per invention run based on the probability of success

    Private TotalCopyCost As Double ' Total Cost of the BPCs for the T2 item - for copy materials for things like data sheets, etc when needed, and get enough successful inventions for these runs
    Private CopyCost As Double ' Cost for the runs given
    Private CopyTime As Double ' Total time in seconds to copy the BPCs needed for the T2 item
    Private CopyUsage As Double ' Total Cost to make a copy

    Private IncludeCopyTime As Boolean
    Private IncludeCopyCosts As Boolean
    Private IncludeCopyUsage As Boolean

    Private InventionCost As Double ' Cost for the runs given 
    Private InventionTime As Double ' Total time in seconds to invent this bp
    Private InventionUsage As Double ' Total cost to do this activity in a facility

    Private IncludeInventionCosts As Boolean
    Private IncludeInventionTime As Boolean
    Private IncludeInventionUsage As Boolean ' just the facility usage, not the full cost use for both T2 and T3

    Private AdvManufacturingSkillLevelBonus As Double ' The total TE reduction from skills required to invent and build this item (T2/T3)

    Private InventionBPCTypeID As Long ' BP used to invent the BP we are building

    ' Price Variables
    Private ItemMarketCost As Double ' Market cost of item 
    Private TotalRawCost As Double
    Private TotalComponentCost As Double
    Private TotalRawProfit As Double
    Private TotalComponentProfit As Double
    Private TotalRawProfitPercent As Double
    Private TotalComponentProfitPercent As Double
    Private TotalIPHRaw As Double
    Private TotalIPHComponent As Double

    ' Save all the settings here, which has all the standings, fees, etc in it
    Private BPUserSettings As ApplicationSettings

    Private T2T3MaterialType As BuildMatType ' How do we build T2 and T3 items for components that are reactions and how deep they want to go

    Private SellExcessItems As Boolean
    Private SellExcessAmount As Double
    Private ExcessMaterials As Materials ' This contains all materials for the entire blueprint as a reference
    Private BPExcessMaterials As Materials ' Just the excess for the bp
    Private UsedExcessMaterials As Materials ' List of materials updated by using from full excess list for updating BP list

    ' What facility are they using to produce?
    Private MainManufacturingFacility As IndustryFacility
    Private ComponentManufacturingFacility As IndustryFacility
    Private CapitalComponentManufacturingFacility As IndustryFacility ' For all capital parts
    Private ReactionFacility As IndustryFacility
    Private CopyFacility As IndustryFacility
    Private InventionFacility As IndustryFacility
    Private ReprocessingFacility As IndustryFacility

    Private OreConversionSettings As ConversionToOreSettings

    ' This is to save the entire chain of blueprints on each line we have used and runs for each one
    Private ProductionChain As List(Of List(Of Integer))

    '  Private FWManufacturingCostBonus As Double
    Private FWCopyingCostBonus As Double
    Private FWInventionCostBonus As Double

    Private ReactionBPGroups As New List(Of Integer)(New Integer() {1888, 1889, 1890, 4097})

    ' BP Constructor
    Public Sub New(ByVal BPBlueprintID As Long, ByVal BPRuns As Long, ByVal BPME As Integer, ByVal BPTE As Integer,
                ByVal NumBlueprints As Integer, ByVal NumProductionLines As Integer, ByVal UserCharacter As Character,
                ByVal UserSettings As ApplicationSettings, ByVal BPBuildBuy As Boolean, ByVal UserAddlCosts As Double,
                ByVal BPProductionFacility As IndustryFacility, ByVal BPComponentProductionFacility As IndustryFacility,
                ByVal BPCapComponentProductionFacility As IndustryFacility, ByVal BPReactionFacility As IndustryFacility,
                ByVal BPSellExcessItems As Boolean, ByVal BuildT2T3MaterialType As BuildMatType, ByVal OriginalBlueprint As Boolean,
                Optional ByRef BuildBuyList As List(Of BuildBuyItem) = Nothing,
                Optional ByRef BPReprocessingFacility As IndustryFacility = Nothing,
                Optional CompressedOreSettings As ConversionToOreSettings = Nothing)

        Dim readerBP As SQLiteDataReader
        Dim SQL As String = ""

        SQL = "SELECT BLUEPRINT_ID, BLUEPRINT_GROUP_ID, ITEM_ID, ITEM_GROUP_ID, ITEM_CATEGORY_ID, "
        SQL &= "TECH_LEVEL, PORTION_SIZE, BASE_PRODUCTION_TIME, MAX_PRODUCTION_LIMIT, ITEM_TYPE, RACE_ID, packagedVolume "
        SQL &= "FROM ALL_BLUEPRINTS_FACT INNER JOIN INVENTORY_TYPES ON ALL_BLUEPRINTS_FACT.ITEM_ID = INVENTORY_TYPES.typeID "
        SQL &= "WHERE BLUEPRINT_ID =" & BPBlueprintID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader

        If readerBP.Read Then
            ' Set the variables
            BlueprintID = readerBP.GetInt32(0)
            BlueprintName = GetTypeName(readerBP.GetInt32(0))
            BlueprintGroupID = readerBP.GetInt32(1)
            ItemID = readerBP.GetInt64(2)
            ItemName = GetTypeName(readerBP.GetInt32(2))
            ItemGroupID = readerBP.GetInt32(3)
            ItemCategoryID = readerBP.GetInt32(4)
            TechLevel = readerBP.GetInt32(5)
            PortionSize = readerBP.GetInt64(6)
            BaseProductionTime = readerBP.GetInt64(7)
            MaxProductionLimit = readerBP.GetInt32(8)
            ItemType = readerBP.GetInt32(9)

            If Not readerBP.IsDBNull(10) Then
                BlueprintRace = readerBP.GetInt32(10)
            Else
                BlueprintRace = 0
            End If
            If Not readerBP.IsDBNull(11) Then
                ItemVolume = readerBP.GetDouble(11) * PortionSize ' Ammo, blocks, bombs, etc have more items per run
            Else
                ItemVolume = 10
            End If
        Else
            Exit Sub
        End If

        readerBP.Close()

        ' Settings
        BPUserSettings = UserSettings
        BaseBP = OriginalBlueprint
        T2T3MaterialType = BuildT2T3MaterialType

        RawMaterials = New Materials
        ComponentMaterials = New Materials
        InventionMaterials = New Materials
        CopyMaterials = New Materials

        TotalCopyCost = 0
        CopyTime = 0
        InventionTime = 0

        ManufacturingFacilityUsage = 0
        ComponentFacilityUsage = 0
        CapComponentFacilityUsage = 0
        ReactionFacilityUsage = 0
        TotalReactionFacilityUsage = 0
        ReprocessingUsage = 0

        CopyUsage = 0
        InventionUsage = 0

        EIV = 0
        JobFee = 0
        AlphaCloneTax = 0
        TotalUsage = 0

        InventionDecryptor = NoDecryptor
        Relic = ""
        TotalInventedRuns = 0

        NumInventionJobs = 0

        ' Do build/buy 
        BuildBuy = BPBuildBuy
        If Not IsNothing(BuildBuyList) Then
            BBList = BuildBuyList
            NewBPRequest = False
        Else
            NewBPRequest = True
        End If

        SellExcessItems = BPSellExcessItems
        SellExcessAmount = 0
        BPExcessMaterials = New Materials
        ExcessMaterials = New Materials
        UsedExcessMaterials = New Materials

        iME = BPME
        iTE = BPTE

        Taxes = 0
        BrokerFees = 0

        ' If they send zero lines, then set to the user skills
        If NumProductionLines = 0 Then ' 3387 mass production and 24625 is adv mass production
            NumberofProductionLines = BPCharacter.Skills.GetSkillLevel(3387) + BPCharacter.Skills.GetSkillLevel(24625) + 1
        Else
            NumberofProductionLines = NumProductionLines
        End If

        UserRuns = BPRuns
        NumberofBlueprints = NumBlueprints
        AdditionalCosts = UserAddlCosts

        'If TechLevel > 1 Then
        '    UserRuns = CInt(Math.Ceiling(BPRuns / PortionSize))
        'Else
        UserRuns = BPRuns
        'End If

        BPCharacter = UserCharacter

        ' Set the skills to use for this blueprint - changed to type ID's due to name changes (1/29/2014)
        If IsReaction(ItemGroupID) Then
            ' Advanced industry only affects manufacturing and research times
            AdvancedIndustrySkill = 0
        Else
            AdvancedIndustrySkill = BPCharacter.Skills.GetSkillLevel(3388)
        End If

        IndustrySkill = BPCharacter.Skills.GetSkillLevel(3380)
        ScienceSkill = BPCharacter.Skills.GetSkillLevel(3402)

        ' Add production implant from settings
        AIImplantValue = 1 - UserSettings.ManufacturingImplantValue

        ' Production facilities
        MainManufacturingFacility = BPProductionFacility
        ComponentManufacturingFacility = BPComponentProductionFacility
        CapitalComponentManufacturingFacility = BPCapComponentProductionFacility
        ReactionFacility = BPReactionFacility

        OreConversionSettings = CompressedOreSettings
        ReprocessingFacility = BPReprocessingFacility

        ' See if we want to include the costs
        IncludeManufacturingUsage = BPProductionFacility.IncludeActivityUsage

        '' Set the faction warfare bonus for the usage calculations
        'Select Case MainManufacturingFacility.FWUpgradeLevel
        '    Case 1
        '        FWManufacturingCostBonus = 0.9
        '    Case 2
        '        FWManufacturingCostBonus = 0.8
        '    Case 3
        '        FWManufacturingCostBonus = 0.7
        '    Case 4
        '        FWManufacturingCostBonus = 0.6
        '    Case 5
        '        FWManufacturingCostBonus = 0.5
        '    Case Else
        '        FWManufacturingCostBonus = 1
        'End Select

        ' Set the flag if the user sent to this blueprint can invent it
        CanInventRE = False ' Can invent T1 BP to this T2 BP
        CanBuildBP = True ' Can build BP (assume we can until we change it)
        CanBuildAll = True ' Can build all components (assume we can until we change it)

        HasBuildableComponents = False

        ' Full cost of items is portion size (ammo, bombs, etc) times runs times cost
        ItemMarketCost = GetItemPrice(ItemID) * UserRuns * PortionSize

        BuiltComponentList = New BuiltItemList
        BPRawMats = New Materials

        ' Set the invention variables to default
        IncludeInventionCosts = False
        IncludeInventionTime = False
        IncludeInventionUsage = False

        IncludeCopyCosts = False
        IncludeCopyTime = False
        IncludeCopyUsage = False

        InventionChance = 0

        TotalInventedRuns = 0
        SingleInventedBPCRuns = 0
        NumInventionJobs = 0

        TotalCopyCost = 0
        CopyTime = 0
        CopyUsage = 0

        InventionTime = 0
        InventionUsage = 0

        InventionDecryptor = NoDecryptor
        Relic = ""

        ' 3406 laboratory operation and 24624 is adv laboratory operation
        NumberofLaboratoryLines = 0

        ' Save copy and invention facility
        CopyFacility = NoFacility
        InventionFacility = NoFacility

        ' Invention variable inputs - The BPC or Relic first
        InventionBPCTypeID = 0

        ' Set the Decryptor data
        InventionDecryptor = NoDecryptor

        ' Implement passing in the runs per copy later based on user, right now though this is unlimited
        MaxRunsPerBP = 0

        ProductionChain = New List(Of List(Of Integer))

    End Sub

    Public Function InventBlueprint(ByVal NumLaboratoryLines As Integer, ByVal BPDecryptor As Decryptor,
                ByVal BPInventionFacility As IndustryFacility, ByVal BPCopyFacility As IndustryFacility, ByVal InventionItemTypeID As Long) As Integer

        ' 3406 laboratory operation and 24624 is adv laboratory operation
        NumberofLaboratoryLines = NumLaboratoryLines

        ' Save copy and invention facility
        CopyFacility = BPCopyFacility
        InventionFacility = BPInventionFacility

        ' Refresh the data on these for blueprints - categoryID = 9
        CopyFacility.RefreshMMTMCMBonuses(0, 9)
        InventionFacility.RefreshMMTMCMBonuses(0, 9)

        '' Set the FW bonus levels
        'Select Case CopyFacility.FWUpgradeLevel
        '    Case 1
        '        FWCopyingCostBonus = 0.9
        '    Case 2
        '        FWCopyingCostBonus = 0.8
        '    Case 3
        '        FWCopyingCostBonus = 0.7
        '    Case 4
        '        FWCopyingCostBonus = 0.6
        '    Case 5
        '        FWCopyingCostBonus = 0.5
        '    Case Else
        '        FWCopyingCostBonus = 1
        'End Select

        'Select Case InventionFacility.FWUpgradeLevel
        '    Case 1
        '        FWInventionCostBonus = 0.9
        '    Case 2
        '        FWInventionCostBonus = 0.8
        '    Case 3
        '        FWInventionCostBonus = 0.7
        '    Case 4
        '        FWInventionCostBonus = 0.6
        '    Case 5
        '        FWInventionCostBonus = 0.5
        '    Case Else
        '        FWInventionCostBonus = 1
        'End Select

        ' Invention variable inputs - The BPC or Relic first
        InventionBPCTypeID = InventionItemTypeID

        ' Set the Decryptor data
        InventionDecryptor = BPDecryptor

        ' Invention and Copy costs/times are set after getting the full base job materials
        IncludeInventionCosts = InventionFacility.IncludeActivityCost
        IncludeInventionTime = InventionFacility.IncludeActivityTime
        IncludeInventionUsage = InventionFacility.IncludeActivityUsage

        IncludeCopyCosts = CopyFacility.IncludeActivityCost
        IncludeCopyTime = CopyFacility.IncludeActivityTime
        IncludeCopyUsage = CopyFacility.IncludeActivityUsage

        ' Set the T2/T3 skills to invent from the T1 version
        Call SetInventionSkills()

        ' Set the T2/T3 skills to copy from the T1 BPC
        Call SetCopySkills()

        ' Set the invention flag
        CanInventRE = UserHasReqSkills(BPCharacter.Skills, ReqInventionSkills)

        ' Use typical invention costs to invent this
        Dim InventedBPs As Integer = InventREBlueprint(Not CanInventRE)

        ' Save the max runs per invented bpc
        MaxRunsPerBP = SingleInventedBPCRuns

        ' Reset the number of bps needed based on the runs we have
        NumberofBlueprints = CInt(Math.Ceiling(UserRuns / MaxRunsPerBP))

        Return InventedBPs

    End Function

    ' Base build function that takes a look at the number of blueprints the user wants to use and then builts each blueprint batch
    Public Sub BuildItems(ByVal SetTaxes As Boolean, ByVal BrokerFeeData As BrokerFeeInfo, ByVal SetProductionCosts As Boolean,
                        ByVal IgnoreMinerals As Boolean, ByVal IgnoreT1Item As Boolean)

        ' Need to check for the number of BPs sent and run multiple Sessions if necessary. Also, look at the number of lines per batch
        If NumberofBlueprints = 1 Then
            'Just run the normal function and it will set everything
            Call BuildItem(SetTaxes, BrokerFeeData, SetProductionCosts, IgnoreMinerals, IgnoreT1Item, ExcessMaterials)
        Else ' Multi bps
            Dim BatchBlueprint As Blueprint
            Dim ComponentBlueprint As Blueprint

            Dim RunsPerLine As Integer
            Dim ExtraRuns As Integer
            Dim AdjRunsperBP As Integer

            Dim BatchList As New List(Of Integer)
            Dim Batches As Integer

            If UserRuns < NumberofBlueprints Then
                ' Can't run more bps than runs, so reset to the runs - 1 bp per run
                NumberofBlueprints = CInt(UserRuns)
            End If

            ' For bps with unlimited runs, assume that the most efficient is to run max runs on each line in one batch, 
            ' so reset if bps are greater than lines
            If MaxRunsPerBP = 0 Then
                If NumberofBlueprints > NumberofProductionLines Then
                    ' We can't run more bps than the lines entered, so reset this
                    NumberofBlueprints = NumberofProductionLines
                End If
                Batches = 1
            Else
                ' How many batches do we run in the production chain?
                Batches = CInt(Math.Ceiling(UserRuns / (MaxRunsPerBP * NumberofProductionLines)))
            End If

            ' set the minimum per bp, shouldn't go over the runs per bp since the user sends in the total numbps they need
            RunsPerLine = CInt(Math.Floor(UserRuns / NumberofBlueprints))
            ExtraRuns = CInt(UserRuns - (RunsPerLine * NumberofBlueprints))

            ' To track how many runs we have used in the batch setup
            Dim RunTracker As Long = 0

            ' Fill a list of runs per bp
            For i = 0 To Batches - 1
                For j = 0 To NumberofProductionLines - 1
                    ' As we add the runs, adjust with extra runs proportionally until they are gone
                    If ExtraRuns <> 0 Then
                        ' Since it's a fraction of a total batch run, this will always just be one until gone ** not right?
                        AdjRunsperBP = RunsPerLine + 1
                        ExtraRuns = ExtraRuns - 1 ' Adjust extra
                    Else
                        ' No extra runs, so just add the original runs now
                        AdjRunsperBP = RunsPerLine
                    End If

                    BatchList.Add(AdjRunsperBP)

                    ' If we have used up all the runs, then exit the loop
                    RunTracker += AdjRunsperBP
                    If RunTracker = UserRuns Then
                        Exit For
                    End If

                    If AdjRunsperBP = MaxRunsPerBP Then
                        ' Reset the adjusteded runs per bp to match invented amount, or if zero let it keep summing up for T1
                        AdjRunsperBP = 0
                    End If

                Next

                ' Add the above batchlist to the chain
                ProductionChain.Add(BatchList)
                ' Reset the batch list
                BatchList = New List(Of Integer)

            Next

            ' First get the BP's that are components of the main item we are building for future calculations
            Dim rsBPComps As SQLiteDataReader
            Dim BPComponentIDs As New List(Of Integer)
            DBCommand = New SQLiteCommand(String.Format("SELECT MATERIAL_ID FROM ALL_BLUEPRINT_MATERIALS_FACT WHERE BLUEPRINT_ID={0} AND ACTIVITY IN (1,11) 
                                                         AND CONSUME = 1 AND MATERIAL_ID IN (SELECT ITEM_ID FROM ALL_BLUEPRINTS_FACT)", BlueprintID), EVEDB.DBREf)
            rsBPComps = DBCommand.ExecuteReader

            While rsBPComps.Read
                ' These are the only items that are built from the base BP
                BPComponentIDs.Add(rsBPComps.GetInt32(0))
            End While

            Dim BatchExcessMats As New Materials ' Excess with first bp

            rsBPComps.Close()

            ' Now we just build each BP for the runs in the batch and total up all the variables - apply additional costs per batch
            ' Need to revisit for efficiency - will run one batch for each unique runs in the production chain and muliply by number of unique run batches
            For i = 0 To ProductionChain.Count - 1
                For j = 0 To ProductionChain(i).Count - 1
                    Application.DoEvents()

                    BatchBlueprint = New Blueprint(BlueprintID, ProductionChain(i)(j), iME, iTE, 1, NumberofProductionLines, BPCharacter, BPUserSettings, BuildBuy,
                                            CDbl(AdditionalCosts / ProductionChain.Count), MainManufacturingFacility, ComponentManufacturingFacility,
                                            CapitalComponentManufacturingFacility, ReactionFacility, SellExcessItems, T2T3MaterialType, True, BBList)

                    Call BatchBlueprint.BuildItem(SetTaxes, BrokerFeeData, SetProductionCosts, IgnoreMinerals, IgnoreT1Item, Nothing)

                    ' Sum up all the stuff that is batch dependent
                    With BatchBlueprint

                        ' Save all the variables
                        If BatchBlueprint.HasBuildableComponents And HasBuildableComponents = False Then
                            HasBuildableComponents = True
                        End If

                        ' Assumption is that we can build the bp
                        If Not BatchBlueprint.CanBuildBP And CanBuildBP = True Then
                            CanBuildBP = False
                        End If

                        If Not BatchBlueprint.CanBuildAll And CanBuildAll = True Then
                            CanBuildAll = False
                        End If

                        ' Material lists - don't copy the raw mats yet, will be rebuilt below
                        If Not IsNothing(.GetComponentMaterials.GetMaterialList) Then
                            For k = 0 To .GetComponentMaterials.GetMaterialList.Count - 1
                                ' Only add materials we are not building, built materials will get added below
                                If .GetComponentMaterials.GetMaterialList(k).GetBuildItem = False Then
                                    Call ComponentMaterials.InsertMaterial(.GetComponentMaterials.GetMaterialList(k))
                                End If
                            Next
                        End If

                        ' Add all new components to the blueprint list to rebuild later
                        For Each BI In .GetComponentsList.GetBuiltItemList
                            ' Only add components from the build that are part of the main BP, any additional components will be added below
                            If BPComponentIDs.Contains(CInt(BI.ItemTypeID)) Then
                                Call BuiltComponentList.AddBuiltItem(BI)
                            End If
                        Next

                        ' Save the raw mats on the bp only
                        If Not IsNothing(.GetBPRawMaterials.GetMaterialList) Then
                            Call BPRawMats.InsertMaterialList(.GetBPRawMaterials.GetMaterialList)
                        End If

                        ' If we build this blueprint, add on the skills required
                        If BatchBlueprint.ReqBuildSkills.NumSkills <> 0 Then
                            ReqBuildSkills.InsertSkills(BatchBlueprint.ReqBuildSkills, True)
                        End If

                        ' Don't add this, it's only the largest time from the batch session and then multiply it later
                        If .GetProductionTime > BPProductionTime Then
                            BPProductionTime = .GetProductionTime
                        End If

                        Taxes += .GetSalesTaxes
                        BrokerFees += .GetSalesBrokerFees

                        ' New cost variables
                        EIV += .GetEstimatedItemValue

                        ' Base Fees for activity
                        JobFee += .GetJobFee

                        Select Case BatchBlueprint.GetItemGroupID
                            Case ReactionGroupID(BatchBlueprint.GetItemGroupID)
                                ReactionFacilityUsage += .GetReactionFacilityUsage
                            Case Else
                                ' How much it costs to use each facility to manufacture items 
                                ManufacturingFacilityUsage += .GetManufacturingFacilityUsage
                        End Select

                    End With
                Next
            Next

            ' Finally we need to calculate each component again as 1 bp and 1 batch so that the numbers line up
            ' We assume that we will build all the components first before doing Sessions - this makes shopping list updates easier
            ' Will revisit but the number of components is set by the blueprint

            ' First copy in all the raw mats from the blueprint
            For i = 0 To BPRawMats.GetMaterialList.Count - 1
                Call RawMaterials.InsertMaterial(BPRawMats.GetMaterialList(i))
            Next

            ' Reset the production times
            ComponentProductionTimes = New List(Of Double)

            Dim TempBuiltComponents As BuiltItemList = CType(BuiltComponentList.Clone, BuiltItemList)

            ' Now build the components as x runs, with 1 bp, that are connected to the main blueprint
            For i = 0 To TempBuiltComponents.GetBuiltItemList.Count - 1
                Dim TempComponentFacility As IndustryFacility
                Dim rsCheck As SQLiteDataReader
                Dim SQL As String
                Dim CategoryID As String = ""
                Dim GroupID As Integer = 0
                Dim OneItemMarketPrice As Double = 0
                Dim PortionSize As Integer = 1

                With TempBuiltComponents.GetBuiltItemList(i)
                    Application.DoEvents()

                    SQL = "SELECT ALL_BLUEPRINTS_FACT.ITEM_GROUP_ID, ALL_BLUEPRINTS_FACT.ITEM_CATEGORY_ID, ITEM_PRICES_FACT.PRICE, PORTION_SIZE "
                    SQL &= "FROM ALL_BLUEPRINTS_FACT, ITEM_PRICES_FACT WHERE ALL_BLUEPRINTS_FACT.ITEM_ID = ITEM_PRICES_FACT.ITEM_ID "
                    SQL &= "AND ALL_BLUEPRINTS_FACT.ITEM_ID = " & .ItemTypeID

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsCheck = DBCommand.ExecuteReader

                    If rsCheck.Read() Then
                        GroupID = rsCheck.GetInt32(0)
                        CategoryID = CStr(rsCheck.GetInt32(1))
                        OneItemMarketPrice = rsCheck.GetDouble(2)
                        PortionSize = rsCheck.GetInt32(3)
                    End If

                    rsCheck.Close()

                    ' Build the T1 component
                    If GroupID = ItemIDs.AdvCapitalComponentGroupID Or GroupID = ItemIDs.CapitalComponentGroupID Then
                        ' Use capital component facility
                        TempComponentFacility = CapitalComponentManufacturingFacility
                    ElseIf IsT1BaseItemforT2(CInt(CategoryID)) Then
                        ' Want to build this in the manufacturing facility we are using for base T1 items used in T2
                        TempComponentFacility = MainManufacturingFacility
                        'ElseIf GroupID = ItemIDs.ReactionBiochemicalsGroupID Or GroupID = ItemIDs.ReactionCompositesGroupID Or GroupID = ItemIDs.ReactionPolymersGroupID Or GroupID = ItemIDs.ReactionsIntermediateGroupID Then
                        '    TempComponentFacility = ReactionFacility
                    Else ' Components
                        TempComponentFacility = ComponentManufacturingFacility
                    End If

                    ' Set the quantity to what was used - save the old required quantity for component list
                    Dim ComponentQuantity As Long = .ItemQuantity
                    Dim BuildQuantity As Long = CInt(Math.Ceiling(.UsedQuantity))

                    ComponentBlueprint = New Blueprint(.BPTypeID, BuildQuantity, .BuildME, .BuildTE, 1,
                                                       NumberofProductionLines, BPCharacter, BPUserSettings, BuildBuy, 0,
                                                       TempComponentFacility, ComponentManufacturingFacility,
                                                       CapitalComponentManufacturingFacility, ReactionFacility, SellExcessItems, T2T3MaterialType, True, BBList)

                    Call ComponentBlueprint.BuildItem(SetTaxes, BrokerFeeData, SetProductionCosts, IgnoreMinerals, IgnoreT1Item, ExcessMaterials)

                    '' Update the BuiltComponetList quantity with what the build quantity needs are
                    'For Each Mat In ComponentBlueprint.UsedExcessMaterials.GetMaterialList
                    '    For Each BC In BuiltComponentList.GetBuiltItemList
                    '        If BC.ItemTypeID = Mat.GetMaterialTypeID Then
                    '            ' Update the built item
                    '            BC.ItemQuantity = BC.ItemQuantity + Mat.GetQuantity
                    '            BC.BPRuns = CLng(Math.Ceiling(BC.ItemQuantity / BC.PortionSize))
                    '            Exit For
                    '        End If
                    '    Next
                    'Next

                    Dim ExtraItems As Long
                    Dim ExtraMaterial As Material = Nothing

                    ' Also, if this built item is more than we need for the main blueprint, add it to excess
                    If ComponentQuantity < (ComponentBlueprint.PortionSize * ComponentBlueprint.GetUserRuns) Then
                        ExtraItems = (ComponentBlueprint.PortionSize * ComponentBlueprint.GetUserRuns) - ComponentQuantity
                        ExtraMaterial = New Material(.ItemTypeID, RemoveItemNameRuns(.ItemName), CategoryID, ExtraItems, .ItemVolume, 0, CStr(.BuildME), CStr(.BuildTE))
                        Call ExcessMaterials.InsertMaterial(ExtraMaterial)
                    End If

                    ' Reset the component's material list for shopping list functionality
                    .BuildMaterials = CType(ComponentBlueprint.RawMaterials, Materials)

                    ' Add any built components to the list as well
                    For Each BI In ComponentBlueprint.BuiltComponentList.GetBuiltItemList
                        Call BuiltComponentList.AddBuiltItem(BI)
                    Next

                    ' Set the variables
                    .ManufacturingFacility = ComponentBlueprint.MainManufacturingFacility
                    .IncludeActivityCost = ComponentBlueprint.MainManufacturingFacility.IncludeActivityCost
                    .IncludeActivityTime = ComponentBlueprint.MainManufacturingFacility.IncludeActivityTime
                    .IncludeActivityUsage = ComponentBlueprint.MainManufacturingFacility.IncludeActivityUsage

                    Dim ItemPrice As Double = 0
                    Dim OwnedBP As Boolean

                    Call GetMETEforBP(ComponentBlueprint.BlueprintID, ComponentBlueprint.TechLevel, BPUserSettings.DefaultBPME, BPUserSettings.DefaultBPTE, OwnedBP)

                    ' Reset item name
                    If .ItemName.Contains("(") Then
                        .ItemName = Trim(.ItemName.Substring(0, InStr(.ItemName, "(") - 1))
                    End If

                    ' Figure out if we build or buy
                    Dim BuildFlag As Boolean = GetBuildFlag(ComponentBlueprint, OneItemMarketPrice, BuildQuantity, OwnedBP, SetTaxes, BrokerFeeData)

                    If (BuildBuy And BuildFlag) Then
                        ' Market cost is greater than build cost, so set the mat cost to the build cost - or just building (not build/buy)
                        ItemPrice = ComponentBlueprint.GetRawMaterials.GetTotalMaterialsCost / .ItemQuantity
                        ' Adjust the runs of this BP in the name for built bps
                        .ItemName = UpdateItemNamewithRuns(.ItemName, ComponentBlueprint.GetUserRuns)
                    Else
                        ' Buying item
                        ItemPrice = OneItemMarketPrice
                        BuildFlag = False
                    End If

                    ' If we build this blueprint, add on the skills required
                    If ComponentBlueprint.ReqBuildSkills.NumSkills <> 0 Then
                        ReqBuildComponentSkills.InsertSkills(ComponentBlueprint.ReqBuildSkills, True)
                    End If

                    ' Add the built material to the component list now - this way we only add one blueprint produced material - use saved component quantity
                    Dim TempMat As New Material(.ItemTypeID, .ItemName, ComponentBlueprint.GetItemData.GroupName, .ItemQuantity, .ItemVolume, ItemPrice, CStr(.BuildME), CStr(.BuildTE), BuildFlag)
                    ComponentMaterials.InsertMaterial(TempMat)

                    ' Building, so add the raw materials to the raw mats list
                    Call RawMaterials.InsertMaterialList(ComponentBlueprint.GetRawMaterials.GetMaterialList)
                End With

                ' Add the production time of this component to the total production time
                Call ComponentProductionTimes.Add(ComponentBlueprint.GetProductionTime)

                ' Get the usage
                Select Case GroupID
                    Case ItemIDs.AdvCapitalComponentGroupID, ItemIDs.CapitalComponentGroupID
                        CapComponentFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                    Case ReactionGroupID(GroupID)
                        TotalReactionFacilityUsage += ComponentBlueprint.GetReactionFacilityUsage
                    Case Else
                        If ReactionBPGroups.Contains(BlueprintGroupID) Then
                            ' Save the manufacturing cost for fuel blocks
                            ManufacturingFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                        Else
                            ComponentFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                        End If
                End Select
            Next

            ' Add any items that are not built but could be to the raw list
            For j = 0 To ComponentMaterials.GetMaterialList.Count - 1
                If ComponentMaterials.GetMaterialList(j).GetBuildItem = False And ComponentMaterials.GetMaterialList(j).GetItemME <> "-" And BuildBuy Then
                    Call RawMaterials.InsertMaterial(ComponentMaterials.GetMaterialList(j))
                End If
            Next

            ' Update the bp production time to equal the longest runs per line times the number of batches - add in copy and invention time if we invented (totaled up in invention function)
            BPProductionTime = (BPProductionTime * Batches) + CopyTime + InventionTime

            ' Set the total production time by adding just the components (invention and copy already included in bp production time
            If Not IsNothing(ComponentProductionTimes) Then
                TotalProductionTime = BPProductionTime + GetComponentProductionTime(ComponentProductionTimes)
            End If

            ' Finally recalculate our prices
            Call SetPriceData(SetTaxes, BrokerFeeData)

        End If

        Dim ReturnedExcess As New Materials
        ReprocessingUsage = 0

        ' If the BP has a reprocessing facility, see if we want to convert minerals/ice to ore and then run this
        If Not IsNothing(ReprocessingFacility) Then
            If ReprocessingFacility.ConvertToOre Then
                Dim ReplaceMinerals As New ConvertToOre(ReprocessingFacility, UserConversiontoOreSettings)
                RawMaterials = ReplaceMinerals.GetOresfromMinerals(RawMaterials, ReturnedExcess, ReprocessingUsage)
                ' Add the excess minerals to the main excess function
                Call ExcessMaterials.InsertMaterialList(ReturnedExcess.GetMaterialList)
                ' Adjust the price data again to handle the update to excess prices and reprocessing usage
                Call SetPriceData(SetTaxes, BrokerFeeData)
            End If
        End If

    End Sub

    ' Sets the material versions for our blueprint
    Private Sub BuildItem(ByVal SetTaxes As Boolean, ByVal BrokerFeeData As BrokerFeeInfo, ByVal SetProductionCosts As Boolean,
                          ByVal IgnoreMinerals As Boolean, ByVal IgnoreT1Item As Boolean,
                          Optional ByRef ExcessBuildMaterials As Materials = Nothing)
        ' Database stuff
        Dim SQL As String
        Dim SQLAdd As String = ""
        Dim readerBP As SQLiteDataReader
        Dim readerME As SQLiteDataReader

        Dim TempME As Integer
        Dim TempTE As Integer
        Dim OwnedBP As Boolean = False

        ' Recursion variables
        Dim ComponentBlueprint As Blueprint = Nothing
        Dim TempSkills As New EVESkillList(BPUserSettings.UseActiveSkillLevels)

        ' The current material we are working with
        Dim CurrentMaterial As Material
        Dim CurrentMatQuantity As Long
        Dim CurrentMaterialGroupID As Integer
        Dim CurrentMaterialCategoryID As Integer

        ' The quantity that we want to use to build this item (may be different than quantity need if portionsize <> runs)
        Dim BuildQuantity As Long = 0
        Dim ComponentBPPortionSize As Long = 1

        ' Temp Materials for passing
        Dim TempMaterials As New Materials
        Dim TempNumBPs As Integer = 1

        Dim SingleRunBuildCost As Double = -1
        Dim SavedExcessMaterialList As New Materials
        Dim SavedBPExcessMaterialList As New Materials
        Dim SavedUpdateBPExcessMaterialList As New Materials

        Dim Mat As Material
        Dim LookupMaterial As Material
        Dim UsedExcessMaterial As Material = Nothing
        Dim ExtraItems As Long
        Dim SellExcessAmount As Double = 0
        Dim AdjCurrentMatQuantity As Long = 0
        Dim ExtraMaterial As Material = Nothing
        Dim RefUsedMat As Material = Nothing

        Dim UsesReactions As Boolean = False
        Dim IgnoreBuild As Boolean = False

        ' Select all materials to buid this BP
        SQL = "SELECT ABM.BLUEPRINT_ID, MATERIAL_ID, QUANTITY, MATERIAL, MATERIAL_GROUP_ID, MATERIAL_CATEGORY_ID,  "
        SQL &= "ACTIVITY, MATERIAL_VOLUME, PRICE, ADJUSTED_PRICE, PORTION_SIZE, groupName "
        SQL &= "FROM ALL_BLUEPRINT_MATERIALS_FACT AS ABM, INVENTORY_TYPES, INVENTORY_GROUPS "
        SQL &= "LEFT OUTER JOIN ITEM_PRICES_FACT ON ABM.MATERIAL_ID = ITEM_PRICES_FACT.ITEM_ID "
        SQL &= "LEFT OUTER JOIN ALL_BLUEPRINTS_FACT ON ALL_BLUEPRINTS_FACT.ITEM_ID = ABM.MATERIAL_ID "
        SQL &= "WHERE ABM.BLUEPRINT_ID =" & CStr(BlueprintID) & " And ACTIVITY IN (1,11) "
        SQL &= "AND MATERIAL_ID = INVENTORY_TYPES.typeID AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader

        ' For each material in the blueprint, calculate the total mats
        ' and load them into the list
        While readerBP.Read
            CurrentMaterialGroupID = readerBP.GetInt32(4)
            CurrentMaterialCategoryID = readerBP.GetInt32(5)

            ' Adjust the number of runs I need for the main item based on what I have in the excess mats
            Dim TempMat As Material = New Material(ItemID, ItemName, CStr(ItemGroupID), UserRuns * PortionSize, ItemVolume, 0, "", "")
            Dim TempRuns As Long = TempMat.GetQuantity

            UserRuns = CLng(Math.Ceiling(TempRuns / PortionSize))

            If CurrentMaterialCategoryID = 16 Then
                ' It's a skill, so just add it to the main list of BP skills
                ReqBuildSkills.InsertSkill(readerBP.GetInt64(1), readerBP.GetInt32(2), readerBP.GetInt32(2), readerBP.GetInt32(2), 0, False, 0, "", Nothing, True)

            ElseIf AddMaterial(CurrentMaterialCategoryID, CurrentMaterialGroupID, IgnoreMinerals, IgnoreT1Item) Then

                ' Save a copy of the excess materials lists in case we need to replace it if we don't decide to build this component
                If Not IsNothing(ExcessBuildMaterials) Then
                    If ExcessBuildMaterials.GetMaterialList.Count <> 0 Then
                        SavedExcessMaterialList = CType(ExcessBuildMaterials.Clone, Materials)
                    End If
                End If

                If Not IsNothing(BPExcessMaterials) Then
                    If BPExcessMaterials.GetMaterialList.Count <> 0 Then
                        SavedBPExcessMaterialList = CType(BPExcessMaterials.Clone, Materials)
                    End If
                End If

                ' Set the current material - adjust with portion size though if sent
                CurrentMaterial = New Material(readerBP.GetInt64(1), readerBP.GetString(3), readerBP.GetString(11), readerBP.GetInt64(2), readerBP.GetDouble(7), If(readerBP.IsDBNull(8), 0, readerBP.GetDouble(8)), "", "")

                ' Refresh the facility bonuses before we start calculations of ME/TE
                Call MainManufacturingFacility.RefreshMMTMCMBonuses(ItemGroupID, ItemCategoryID)

                ' Save the base costs - before applying ME - if value is null (no price record) then set to 0
                EIV += CurrentMaterial.GetQuantity * If(IsDBNull(readerBP.GetValue(9)), 0, readerBP.GetDouble(9))

                ' Set the quantity: required = max(runs,ceil(round(runs * baseQuantity * materialModifier,2))
                CurrentMatQuantity = CLng(Math.Max(UserRuns, Math.Ceiling(Math.Round(UserRuns * CurrentMaterial.GetQuantity * SetBPMaterialModifier(MainManufacturingFacility), 2))))
                ' Update the quantity - just add the negative percent of the ME modifier to 1 and multiply
                Call CurrentMaterial.SetQuantity(CurrentMatQuantity)

                ' Before going any further, see if we have this material in excess materials and if so, adjust the quantity that we will need to build
                If IsNothing(ExcessBuildMaterials) Then
                    AdjCurrentMatQuantity = CurrentMatQuantity
                Else
                    AdjCurrentMatQuantity = GetAdjustedQuantity(ExcessBuildMaterials, CurrentMaterial.GetMaterialTypeID, CurrentMatQuantity, RefUsedMat)
                End If

                If AdjCurrentMatQuantity = 0 Then
                    ' If original blueprint and the component was already built, need to build it with the original mat quantity 
                    ' but Not add any materials to the list after completed so it shows the item needing to be built
                    If BaseBP Then
                        ' Add to main list 
                        ' Set the name of the material to include the build runs if built
                        If BuildBuy Then
                            If RefUsedMat.GetBuildItem Then
                                RefUsedMat.SetName(UpdateItemNamewithRuns(RefUsedMat.GetMaterialName, RefUsedMat.GetQuantity))
                                ' also, since this was already built in a component, we can't charge the cost to the main list
                                ' so it matches up with the build buy costs, reset cost to 0
                                ComponentMaterials.InsertMaterial(RefUsedMat, 0)
                            Else
                                ' Buying, so add to raw mat list too
                                '(if it's built already, the raw mats will already be in the list)
                                RawMaterials.InsertMaterial(RefUsedMat)
                            End If
                        Else
                            ComponentMaterials.InsertMaterial(RefUsedMat)
                        End If
                    End If
                    GoTo SkipProcessing
                End If

                If Not IsDBNull(readerBP.GetValue(10)) Then
                    ComponentBPPortionSize = readerBP.GetInt32(10)
                    ' Divide by the portion size if this item has one (component buildable) for the build quantity
                    BuildQuantity = CLng(Math.Ceiling(AdjCurrentMatQuantity / ComponentBPPortionSize))
                Else
                    BuildQuantity = AdjCurrentMatQuantity
                    ComponentBPPortionSize = 1
                End If

                IgnoreBuild = False

                ' For molecular forged materials, these are reactions but are like advanced T2 components, so don't build them if advanced selected to match other components
                If T2T3MaterialType = BuildMatType.AdvMaterials And CurrentMaterialGroupID = ItemIDs.ReactionMolecularForgedGroupID Then
                    IgnoreBuild = True
                End If

                ' For R.A.M.s and Fuel Blocks
                If (CurrentMaterialGroupID = 1136 And BPUserSettings.AlwaysBuyFuelBlocks) Or (CurrentMaterialGroupID = 332 And BPUserSettings.AlwaysBuyRAMs) Then
                    IgnoreBuild = True
                End If

                ' If this is an advanced composite reaction, and the advanced option is selected, then don't build anything and add as raw material
                If (ItemGroupID = ItemIDs.ReactionCompositesGroupID Or ItemGroupID = ItemIDs.ReactionMolecularForgedGroupID) And T2T3MaterialType = BuildMatType.AdvMaterials Then
                    IgnoreBuild = True
                ElseIf (BlueprintName.Contains("Standard") Or BlueprintName.Contains("Synth")) And T2T3MaterialType = BuildMatType.ProcessedMaterials And CurrentMaterialGroupID <> ItemIDs.ReactionBiochemicalsGroupID Then
                    IgnoreBuild = True
                ElseIf (BlueprintName.Contains("Improved") Or BlueprintName.Contains("Strong")) And T2T3MaterialType <> BuildMatType.RawMaterials And CurrentMaterialGroupID <> ItemIDs.ReactionBiochemicalsGroupID Then
                    IgnoreBuild = True
                End If

                UsesReactions = False

                ' See what material type this is and if we want to build it (reactions)
                Select Case CurrentMaterialGroupID
                    Case ItemIDs.ReactionCompositesGroupID, ItemIDs.ReactionMolecularForgedGroupID
                        If T2T3MaterialType = BuildMatType.ProcessedMaterials Or T2T3MaterialType = BuildMatType.RawMaterials Then
                            UsesReactions = True
                        End If
                    Case ItemIDs.ReactionsIntermediateGroupID, ItemIDs.ReactionPolymersGroupID
                        If T2T3MaterialType = BuildMatType.RawMaterials Then ' Or (ItemGroupID = ItemIDs.ReactionCompositesGroupID And T2T3MaterialType = BuildMatType.AdvMaterials) Then
                            UsesReactions = True
                        End If
                    Case ItemIDs.ReactionBiochemicalsGroupID
                        ' Special processing for boosters
                        If CurrentMaterial.GetMaterialName.Contains("Improved") Or CurrentMaterial.GetMaterialName.Contains("Strong") Then
                            ' This has intermediate material types
                            UsesReactions = True
                        ElseIf (CurrentMaterial.GetMaterialName.Contains("Standard") Or CurrentMaterial.GetMaterialName.Contains("Synth")) And T2T3MaterialType = BuildMatType.ProcessedMaterials Then
                            UsesReactions = True
                        Else ' Only builds one level
                            If T2T3MaterialType = BuildMatType.RawMaterials Then
                                UsesReactions = True
                            End If
                        End If
                End Select

                If Not UsesReactions Then
                    SQLAdd = " AND BLUEPRINT_GROUP_ID NOT IN (1888,1889,1890,4097)"
                Else
                    SQLAdd = ""
                End If

                ' If it has a value in the main bp table, then the item can be built from it's own BP - do a check if they want to use reactions to drill down to raw mats
                SQL = "SELECT BLUEPRINT_ID, TECH_LEVEL FROM ALL_BLUEPRINTS_FACT WHERE ITEM_ID =" & CurrentMaterial.GetMaterialTypeID & SQLAdd
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerME = DBCommand.ExecuteReader

                If readerME.Read And Not IgnoreBuild Then
                    ' We can build it from another BP 
                    HasBuildableComponents = True

                    ' Look up the ME/TE and owned data for the bp
                    Call GetMETEforBP(readerME.GetInt32(0), readerME.GetInt32(1), TempME, TempTE, OwnedBP)

                    ' Update the current material's ME
                    CurrentMaterial.SetItemME(CStr(TempME))

                    Dim TempComponentFacility As IndustryFacility

                    ' Build the T1/Reaction component
                    Select Case CurrentMaterialGroupID
                        Case ItemIDs.AdvCapitalComponentGroupID, ItemIDs.CapitalComponentGroupID
                            ' Use capital component facility
                            TempComponentFacility = CapitalComponentManufacturingFacility
                        Case ReactionGroupID(CurrentMaterialGroupID)
                            TempComponentFacility = ReactionFacility
                        Case Else
                            If IsT1BaseItemforT2(CurrentMaterialCategoryID) Then
                                ' Want to build this in the manufacturing facility we are using for base T1 items used in T2
                                TempComponentFacility = MainManufacturingFacility
                            Else
                                ' Components
                                TempComponentFacility = ComponentManufacturingFacility
                            End If
                    End Select

                    ' For now only assume 1 bp and 1 line to build it - Later this section will have to be updated to use the remaining lines or maybe lines = numbps
                    ComponentBlueprint = New Blueprint(readerME.GetInt32(0), BuildQuantity, TempME, TempTE,
                                                           1, 1, BPCharacter, BPUserSettings, BuildBuy, 0, TempComponentFacility,
                                                           ComponentManufacturingFacility, CapitalComponentManufacturingFacility,
                                                           ReactionFacility, SellExcessItems, T2T3MaterialType, False, BBList)

                    ' Set this blueprint with the quantity needed and get it's mats
                    Call ComponentBlueprint.BuildItem(SetTaxes, BrokerFeeData, SetProductionCosts, IgnoreMinerals, IgnoreT1Item, ExcessBuildMaterials)

                    Call BPExcessMaterials.InsertMaterialList(ComponentBlueprint.BPExcessMaterials.GetMaterialList)

                    ' Update this BP's excess materials if any were used from the excess list in this component bp when built
                    For Each Mat In ComponentBlueprint.UsedExcessMaterials.GetMaterialList
                        If Mat.GetMaterialTypeID <> CurrentMaterial.GetMaterialTypeID Then
                            ' See if material used is in the BP list and update quantity from what we used
                            LookupMaterial = BPExcessMaterials.SearchListbyName(Mat.GetMaterialName, True)
                            If Not IsNothing(LookupMaterial) Then
                                Dim UpdateQuantity As Long = LookupMaterial.GetQuantity - Mat.GetQuantity
                                If UpdateQuantity <= 0 Then
                                    ' Used it all
                                    Call BPExcessMaterials.RemoveMaterial(LookupMaterial)
                                Else
                                    ' Remove from list to update total list amount
                                    Call BPExcessMaterials.RemoveMaterial(LookupMaterial)
                                    ' Update what we used and insert back into list
                                    LookupMaterial.SetQuantity(UpdateQuantity)
                                    Call BPExcessMaterials.InsertMaterial(LookupMaterial)
                                End If
                            Else
                                Call UsedExcessMaterials.InsertMaterial(Mat)
                            End If
                            ' Save if we don't end up building this and we can readjust
                            Call SavedUpdateBPExcessMaterialList.InsertMaterial(Mat)

                            '' Update the BuiltComponentList quantity with what the build quantity needs are
                            'For Each BC In BuiltComponentList.GetBuiltItemList
                            '    If BC.ItemTypeID = Mat.GetMaterialTypeID Then
                            '        BC.ItemQuantity += Mat.GetQuantity
                            '        Exit For
                            '    End If
                            'Next

                        End If
                    Next

                    Dim BuiltQuantity As Long = ComponentBlueprint.GetPortionSize * ComponentBlueprint.GetUserRuns

                    ' Set all the materials that are excess and save later if built
                    ExtraMaterial = Nothing

                    ' If this built item is more than we need for the main blueprint, add it to excess. If less, then we have already calced what we need plus excess above
                    If CurrentMaterial.GetQuantity < (ComponentBlueprint.PortionSize * ComponentBlueprint.GetUserRuns) Then
                        ExtraItems = (ComponentBlueprint.PortionSize * ComponentBlueprint.GetUserRuns) - CurrentMaterial.GetQuantity
                        ExtraMaterial = CType(CurrentMaterial.Clone, Material)
                        Call ExtraMaterial.SetQuantity(ExtraItems)
                    End If

                    ' Figure out if we build or if cheaper to buy
                    Dim BuildItem As Boolean = GetBuildFlag(ComponentBlueprint, CurrentMaterial.GetCostPerItem, BuildQuantity, OwnedBP, SetTaxes, BrokerFeeData)

                    If (BuildItem And BuildBuy) Or Not BuildBuy Then
                        '*** BUILD ***

                        ' We want to build this item
                        CurrentMaterial.SetBuildItem(True)

                        ' Update this if used later
                        If Not IsNothing(ExtraMaterial) Then
                            ExtraMaterial.SetBuildItem(True)
                        End If

                        ' Set the name of the material to include the build runs only if build/buy
                        If BuildBuy Then
                            CurrentMaterial.SetName(UpdateItemNamewithRuns(CurrentMaterial.GetMaterialName, BuildQuantity))
                        End If

                        ' Use any materials before continuing
                        If Not IsNothing(ExcessBuildMaterials) Then
                            Call UseExcessMaterials(ExcessBuildMaterials, CurrentMaterial.GetMaterialTypeID, CurrentMaterial.GetQuantity - BuiltQuantity,
                                            SavedExcessMaterialList, UsedExcessMaterial)
                        End If

                        ' Save the production time for this component
                        Call ComponentProductionTimes.Add(ComponentBlueprint.GetTotalProductionTime)

                        ' Get the skills for BP to build it and add them to the list
                        TempSkills = ComponentBlueprint.GetReqBPSkills

                        ' Get the component usage
                        Select Case ComponentBlueprint.GetItemGroupID
                            Case ItemIDs.AdvCapitalComponentGroupID, ItemIDs.CapitalComponentGroupID
                                CapComponentFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                            Case ReactionGroupID(ComponentBlueprint.GetItemGroupID)
                                ' Save reaction and fuel block usage for reaction bps
                                TotalReactionFacilityUsage += ComponentBlueprint.GetReactionFacilityUsage
                                ManufacturingFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                            Case Else
                                If ReactionBPGroups.Contains(BlueprintGroupID) Then
                                    ' Save fuel block usage
                                    ManufacturingFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                                Else
                                    ' Just a regular component
                                    ComponentFacilityUsage += ComponentBlueprint.GetManufacturingFacilityUsage
                                End If
                        End Select

                        ' Insert the raw mats of this blueprint
                        RawMaterials.InsertMaterialList(ComponentBlueprint.GetRawMaterials.GetMaterialList)

                        ' If this item has buildable components, add those to this main list too so it nests up
                        If Not IsNothing(ExcessBuildMaterials) Then
                            For i = 0 To ComponentBlueprint.BuiltComponentList.GetBuiltItemList.Count - 1
                                BuiltComponentList.AddBuiltItem(CType(ComponentBlueprint.BuiltComponentList.GetBuiltItemList(i).Clone, BuiltItem))
                            Next
                        End If

                        Dim OverrideRunsModifier As Integer = -1

                        ' *** BUILD OR BUY ***
                        If BuildBuy Then
                            ' Get the total component cost and adjust total with any excess sell
                            Dim TotalComponentCost As Double = ComponentBlueprint.GetTotalComponentCost
                            '- AdjustPriceforTaxesandFees(ComponentBlueprint.BPExcessMaterials.GetTotalMaterialsCost, SetTaxes, BrokerFeeData)

                            ' Add in taxes and fees to the total build cost
                            SingleRunBuildCost = TotalComponentCost / BuildQuantity
                            CurrentMaterial.SetBuildCostPerItem(SingleRunBuildCost)

                            ' Save the item built, it's ME and the materials it used
                            Dim TempBuiltItem As New BuiltItem

                            ' Add the built item to the built component list for later use
                            TempBuiltItem = SetBuiltItem(readerME.GetInt64(0), CurrentMaterial, CurrentMatQuantity, ComponentBPPortionSize,
                                                    TempME, TempTE, ComponentBlueprint, BuildQuantity, SetTaxes, BrokerFeeData)

                            TempBuiltItem.BuildMaterials = ComponentBlueprint.GetComponentMaterials

                            If ComponentBlueprint.BuiltComponentList.GetBuiltItemList.Count <> 0 Then
                                ' Add any buildable components to this item list
                                For i = 0 To ComponentBlueprint.BuiltComponentList.GetBuiltItemList.Count - 1
                                    TempBuiltItem.ComponentBuildList.Add(CType(ComponentBlueprint.BuiltComponentList.GetBuiltItemList(i).Clone, BuiltItem))
                                Next
                            End If

                            ' Add this item to the main build list 
                            BuiltComponentList.AddBuiltItem(CType(TempBuiltItem.Clone, BuiltItem))

                            ' Use the build cost for the material if the item quantity doesn't match portion size, and insert all components into the build/buy list
                            If CurrentMatQuantity <> BuildQuantity Then
                                ' Override the total build cost (which for this is the number of runs we need and the cost of each) mostly for items with portion sizes
                                ComponentMaterials.InsertMaterial(CurrentMaterial, SingleRunBuildCost * BuildQuantity)
                            Else
                                ComponentMaterials.InsertMaterial(CurrentMaterial, TotalComponentCost)
                            End If

                        Else '*** BUILD ALL COMPONENTS - so use BUY prices ***
                            ' Add the built item to the built component list for later use
                            BuiltComponentList.AddBuiltItem(CType(SetBuiltItem(readerME.GetInt64(0), CurrentMaterial, CurrentMatQuantity, ComponentBPPortionSize, TempME, TempTE,
                                            ComponentBlueprint, BuildQuantity, SetTaxes, BrokerFeeData).Clone, BuiltItem))

                            ' Insert the existing component that we are using into the component list as set in the original BP
                            ComponentMaterials.InsertMaterial(CurrentMaterial)

                        End If

                    Else ' *** BUY ***
                        ' We want to buy this item, don't add raw mats but add the component to the buy list (raw mats)
                        CurrentMaterial.SetBuildItem(False)
                        ' Update this if used later
                        If Not IsNothing(ExtraMaterial) Then
                            ExtraMaterial.SetBuildItem(False)
                        End If
                        ' Also, not adding the build time to the lists
                        RawMaterials.InsertMaterial(CurrentMaterial)
                        ComponentMaterials.InsertMaterial(CurrentMaterial)
                    End If

                    ' If we build this blueprint, add on the skills required
                    If TempSkills.NumSkills <> 0 Then
                        ReqBuildComponentSkills.InsertSkills(TempSkills, True)
                    End If

                    ' Check if we can build all components. If we can't make one item then we set it to false and leave it that way
                    If CanBuildAll Then
                        If Not UserHasReqSkills(BPCharacter.Skills, ComponentBlueprint.GetReqBPSkills) Then
                            ' Can't build this item, so we can't build all components from main blueprint
                            CanBuildAll = False
                        End If
                    End If

                    ' Adjust excess lists 
                    If CurrentMaterial.GetBuildItem Then
                        ' Save the excess for this item
                        If Not IsNothing(ExcessBuildMaterials) Then
                            Call ExcessBuildMaterials.InsertMaterial(ExtraMaterial)
                        End If
                        ' Add the used material to the BP excess materials since for this BP it was excess 
                        Call BPExcessMaterials.InsertMaterial(ExtraMaterial)
                    ElseIf Not CurrentMaterial.GetBuildItem Then
                        ' Not building this item, so replace the copy we made before we started this bp to reset the list back
                        ExcessBuildMaterials = CType(SavedExcessMaterialList.Clone, Materials)
                        BPExcessMaterials = CType(SavedBPExcessMaterialList.Clone, Materials)
                        ' Loop through and add back any used excess since we are buying this item
                        'For Each Mat In SavedUpdateBPExcessMaterialList.GetMaterialList
                        '    LookupMaterial = BPExcessMaterials.SearchListbyName(Mat.GetMaterialName)
                        '    If Not IsNothing(LookupMaterial) Then
                        '        ' Remove from list to update total list amount
                        '        Call BPExcessMaterials.RemoveMaterial(LookupMaterial)
                        '        ' Update what we used and insert back into list
                        '        LookupMaterial.SetQuantity(LookupMaterial.GetQuantity + Mat.GetQuantity)
                        '        Call BPExcessMaterials.InsertMaterial(LookupMaterial)
                        '    Else
                        '        ' Just add it back to the list
                        '        BPExcessMaterials.InsertMaterial(Mat)
                        '    End If
                        'Next
                    End If

                Else ' Just raw material 
                    If readerME.HasRows Then
                        ' This is a component, so look up the ME of the item to put on the material before adding (fixes issue when searching for shopping list items of the same type - no ME is "-" and these have an me
                        ' For example, see modulated core strip miner and polarized heavy pulse weapons.
                        Call GetMETEforBP(readerME.GetInt64(0), readerME.GetInt32(1), TempME, TempTE, OwnedBP)
                        CurrentMaterial.SetItemME(CStr(TempME))
                    End If

                    ' We are not building these
                    CurrentMaterial.SetBuildItem(False)

                    ' Insert the raw mats
                    RawMaterials.InsertMaterial(CurrentMaterial)
                    ' Also insert into component list
                    ComponentMaterials.InsertMaterial(CurrentMaterial)
                    ' These are from the bp and not a component
                    BPRawMats.InsertMaterial(CurrentMaterial)

                End If

                readerME.Close()

SkipProcessing:

                End If

        End While

        readerBP.Close()

        ' Set the build flag for the blueprint
        If UserHasReqSkills(BPCharacter.Skills, ReqBuildSkills) Then
            CanBuildBP = True
        Else
            CanBuildBP = False
        End If

        ' Set the Advanced Skill levels to build this item for later application of Production Time
        AdvManufacturingSkillLevelBonus = SetAdvManufacturingSkillLevels(ReqBuildSkills)

        ' Set the production time
        Call SetProductionTime()

        ' Set taxes and fees on this item only (materials set in shopping list)
        Taxes = GetSalesTax(ItemMarketCost)
        BrokerFees = GetSalesBrokerFee(ItemMarketCost, BrokerFeeData)

        ' Set the costs for making this item
        Call SetManufacturingCostsAndFees()

        ' Update the total time, if we used components
        If Not IsNothing(ComponentProductionTimes) Then
            TotalProductionTime = TotalProductionTime + GetComponentProductionTime(ComponentProductionTimes)
        End If

        ' Add all the times here - only include copy, re, and invention times here since it's the total time
        TotalProductionTime = TotalProductionTime + BPProductionTime + CopyTime + InventionTime
        ' Finally, add in the copy, invention and RE time if they sent it
        BPProductionTime = BPProductionTime + CopyTime + InventionTime

        ' Update the built runs on each built item in the built component list
        For Each BI In BuiltComponentList.GetBuiltItemList
            BI.ItemName = UpdateItemNamewithRuns(BI.ItemName, CLng(Math.Ceiling(BI.ItemQuantity / BI.PortionSize)))
        Next

        ' Finally set all the price data
        Call SetPriceData(SetTaxes, BrokerFeeData)

    End Sub

    ' See if the item is in the list and if so, return that value else false
    Private Function ManualBuildBuyValue(ItemID As Long, DefaultValue As Boolean) As Boolean
        Dim FoundItem As BuildBuyItem
        BBItemtoFind = ItemID
        FoundItem = BBList.Find(AddressOf FindBBItem)

        If FoundItem.ItemID <> 0 Then
            Return FoundItem.BuildItem
        Else
            Return DefaultValue
        End If

    End Function

    ' Just look up excess to see if we have enough materials to use for material quantity and return difference needed to build if diff greater than zero (will be used later if so)
    Private Function GetAdjustedQuantity(ByRef ExcessBuildItems As Materials, ByVal MaterialTypeID As Long, ByVal MaterialQuantity As Long, ByRef ItemUsed As Material) As Long
        Dim ReturnQuantity As Long = MaterialQuantity
        Dim UsedMat As Material
        Dim UsedQuantity As Long

        If Not IsNothing(ExcessBuildItems) Then
            If ExcessBuildItems.GetMaterialList.Count <> 0 Then
                Dim TempList As Materials = CType(ExcessBuildItems.Clone, Materials)
                For Each ExcessMat In TempList.GetMaterialList
                    If ExcessMat.GetMaterialTypeID = MaterialTypeID Then
                        ' Get a copy first
                        UsedMat = CType(ExcessMat.Clone, Material)
                        ' If have excess materials from then use those mats, and only build what we need
                        If MaterialQuantity > ExcessMat.GetQuantity Then
                            ' Need to update current mat quantity and continue to run as we need more now but still using all
                            ReturnQuantity = MaterialQuantity - ExcessMat.GetQuantity
                            UsedQuantity = ReturnQuantity
                        Else
                            ' We are using what's in the list and can skip processing - some left over
                            ReturnQuantity = 0 ' Skip
                            UsedQuantity = MaterialQuantity
                            ' Adjust the list
                            Call ExcessBuildItems.RemoveMaterial(ExcessMat)
                            ' Update the quantity used from excess and skip processing
                            ExcessMat.SetQuantity(ExcessMat.GetQuantity - MaterialQuantity)
                            If ExcessMat.GetQuantity > 0 Then
                                Call ExcessBuildItems.InsertMaterial(ExcessMat)
                            End If
                        End If

                        If Not IsNothing(UsedMat) Then
                            ' If using all mats, then clear out all excess materials below it
                            If ExcessMat.GetQuantity = 0 Then
                                UsedExcessMaterials = New Materials
                            End If
                            UsedMat.SetQuantity(UsedQuantity)
                            UsedExcessMaterials.InsertMaterial(UsedMat)
                        End If

                        ItemUsed = UsedMat

                        Exit For
                    End If
                Next
            End If
        End If

        Return ReturnQuantity

    End Function

    ' Adjusts the lists for use of excess materials - True means continue to build the updated material quantity, false means don't build
    Private Sub UseExcessMaterials(ByRef ExcessBuildItems As Materials, ByVal MaterialTypeID As Long, ByVal MaterialQuantity As Long,
                                        Optional ByRef SavedExcessList As Materials = Nothing,
                                        Optional ByRef UpdatedMaterial As Material = Nothing)
        Dim LookupMaterial As Material
        Dim UpdatedQuantity As Long

        If MaterialQuantity <= 0 Then
            ' We built what we need so we won't use anything
            Exit Sub
        End If

        If Not IsNothing(ExcessBuildItems) Then
            Dim TempList As Materials = CType(ExcessBuildItems.Clone, Materials)
            If TempList.GetMaterialList.Count <> 0 Then
                For Each ExcessMat In TempList.GetMaterialList
                    If ExcessMat.GetMaterialTypeID = MaterialTypeID Then
                        ' Remove first, then add back updated quantity after use
                        Call ExcessBuildItems.RemoveMaterial(ExcessMat)
                        UpdatedQuantity = ExcessMat.GetQuantity - MaterialQuantity
                        If UpdatedQuantity > 0 Then
                            ' Update the quantity used from excess
                            ExcessMat.SetQuantity(UpdatedQuantity)
                            Call ExcessBuildItems.InsertMaterial(ExcessMat)
                        End If
                        ' Save that we used this material for the quantity sent, even if the remaining is 0
                        UpdatedMaterial = CType(ExcessMat.Clone, Material)
                    End If
                Next
            End If
        End If

        If Not IsNothing(SavedExcessList) Then
            ' We used excess mats so no need to build - however, need to restore excess for anything we built and used in drill down
            For Each Mat In SavedExcessList.GetMaterialList
                If Mat.GetMaterialTypeID <> MaterialTypeID Then
                    ' See if material is even in list (could have been removed when used)
                    LookupMaterial = ExcessBuildItems.SearchListbyName(Mat.GetMaterialName, True)
                    If IsNothing(LookupMaterial) Then
                        ' Not found, so add back in
                        Call ExcessBuildItems.InsertMaterial(Mat)
                    ElseIf LookupMaterial.GetQuantity <> Mat.GetQuantity Then ' Look up the material from the excess list and compare quantity
                        ' If different, restore the original quantity to the excess list
                        Call ExcessBuildItems.RemoveMaterial(LookupMaterial)
                        ' Update what we used and insert back into list
                        LookupMaterial.SetQuantity(Mat.GetQuantity)
                        Call ExcessBuildItems.InsertMaterial(LookupMaterial)
                    End If
                End If
            Next
        End If

        If Not IsNothing(UpdatedMaterial) Then
            ' If using all mats, then clear out all excess materials below it
            If MaterialQuantity = 0 Then
                UsedExcessMaterials = New Materials
            End If
            UsedExcessMaterials.InsertMaterial(UpdatedMaterial)
        End If

    End Sub

    Private Function SetBuiltItem(TypeID As Long, SentMaterial As Material, MaterialQuantity As Long, PortionSize As Long,
                                  iME As Integer, iTE As Integer, RefBlueprint As Blueprint, BuildRuns As Long,
                                  SetTaxes As Boolean, BFData As BrokerFeeInfo) As BuiltItem

        ' Save the item built, it's ME and the materials it used
        Dim TempBuiltItem As New BuiltItem

        With TempBuiltItem
            .BPTypeID = TypeID
            .ItemTypeID = SentMaterial.GetMaterialTypeID
            .ItemName = SentMaterial.GetMaterialName
            .ItemQuantity = MaterialQuantity
            .UsedQuantity = CDec(MaterialQuantity / PortionSize) ' use decimal here, don't round yet
            .BuildME = iME
            .BuildTE = iTE
            .ItemVolume = SentMaterial.GetVolume
            .BPRuns = BuildRuns
            .PortionSize = PortionSize

            .BuildMaterials = RefBlueprint.GetRawMaterials
            .ManufacturingFacility = RefBlueprint.MainManufacturingFacility
            .IncludeActivityCost = RefBlueprint.MainManufacturingFacility.IncludeActivityCost
            .IncludeActivityTime = RefBlueprint.MainManufacturingFacility.IncludeActivityTime
            .IncludeActivityUsage = RefBlueprint.MainManufacturingFacility.IncludeActivityUsage

            .TotalBuildCost = RefBlueprint.GetComponentMaterials.GetTotalMaterialsCost + +RefBlueprint.TotalUsage + RefBlueprint.GetSalesTaxes + RefBlueprint.GetSalesBrokerFees
            .TotalExcessSellBuildCost = .TotalBuildCost - AdjustPriceforTaxesandFees(RefBlueprint.BPExcessMaterials.GetTotalMaterialsCost, SetTaxes, BFData)

        End With

        Return TempBuiltItem

    End Function

    ' Determine ProductionTime of Components - they have 15 components, and 10 usable production lines, then take the max time, and sum up the rest and divide as sections of the max
    ' So if they have a 10 minute component, and 5, 1 minute components, we can make all in 2 jobs and the total time is 10 min. If they go over max jobs,
    ' then take the max component and add on the max job of the 2nd component
    ' TODO - FIX
    Private Function GetComponentProductionTime(ByVal SentTimes As List(Of Double)) As Double
        Dim MaxComponentTime As Double = 0
        Dim RemainingTimeSum As Double = 0
        Dim JobTimeSum As Double = 0
        Dim Temp As New List(Of Double)
        Dim JobCount As Integer

        Dim i As Integer
        Dim SessionTime As Double

        'Dim ProductionCombos As New List(Of BPCombinations) ' Each entry is a combination, each index is a line, total time is the combined times
        'Dim CurrentBPCombo As BPCombinations = Nothing
        ''Dim BPTimeStartIndex As Integer
        'Dim LinesIndex As Integer
        'Dim TimeIndex As Integer
        'Dim CompareTime As Double
        'Dim MinimumTimeIndex As Integer

        'Dim TopBPTimesperCombo() As Double

        If SentTimes.Count = 0 Then
            Return 0
        End If

        'If TestingVersion Then
        '    ' Simple case 1
        '    If NumberofProductionLines = 1 Then
        '        ' Just sum up the times
        '        For i = 0 To SentTimes.Count - 1
        '            SessionTime = SessionTime + SentTimes(i)
        '        Next

        '    ElseIf ComponentProductionTimes.Count <= NumberofProductionLines Then
        '        ' Simple case 2
        '        ' Just return the max, we can make all the others within the time to make the first,
        '        ' we have enough lines, and we have to wait for the first to end
        '        SessionTime = SentTimes.Max

        '    Else ' Hard case, need to find most optimal combination

        '        ' Set the lines
        '        ReDim CurrentBPCombo.CombinedTimes(NumberofProductionLines - 1)
        '        LinesIndex = 0
        '        BPTimeStartIndex = 0

        '        ' This is the number of combinations we should get
        '        For i = 0 To SentTimes.Count - 1

        '            ' Loop through each blueprint and each set of combos, start on a new initial bp time
        '            For j = 0 To SentTimes.Count - 1

        '                ' This moves the index from the start (say 3) to loop back to 0 when necessary
        '                If BPTimeStartIndex + j < SentTimes.Count Then
        '                    ' If less than count, just increment with j from start
        '                    TimeIndex = BPTimeStartIndex + j
        '                ElseIf BPTimeStartIndex + j > SentTimes.Count Then
        '                    ' If we go over count, then the index must have been set to 0 (when equal) so increment the timeindex
        '                    TimeIndex += 1
        '                Else ' equal
        '                    TimeIndex = 0
        '                End If

        '                ' Loop through each BP and move through the index
        '                CurrentBPCombo.CombinedTimes(LinesIndex) = CurrentBPCombo.CombinedTimes(LinesIndex) + SentTimes(TimeIndex)

        '                ' Reset line index so we add to the right line
        '                If LinesIndex = CurrentBPCombo.CombinedTimes.Count - 1 Then
        '                    LinesIndex = 0
        '                Else
        '                    LinesIndex += 1
        '                End If

        '            Next

        '            ' Insert the current bp combo into the list, then start processing the next combination
        '            ProductionCombos.Insert(i, CurrentBPCombo)

        '            ' Increment the BP Start index
        '            BPTimeStartIndex += 1
        '            ' Reset combined times
        '            LinesIndex = 0
        '            ReDim CurrentBPCombo.CombinedTimes(NumberofProductionLines - 1)

        '        Next

        '        ' Now we should have a list of possible line and bp time (bps) time combinations
        '        ' Find the largest of each index and save time - This is the max time it will take to make all the blueprints in that combo
        '        ReDim TopBPTimesperCombo(ProductionCombos.Count - 1)

        '        For i = 0 To ProductionCombos.Count - 1
        '            TopBPTimesperCombo(i) = ProductionCombos(i).CombinedTimes.Max
        '        Next

        '        ' Finally, find the minimum time of the maximums from the combos (save the index by doing a loop instead of just getting the min)
        '        ' This is the ideal BP production combination and the optimal time to make the item
        '        For i = 0 To TopBPTimesperCombo.Count - 1
        '            CompareTime = TopBPTimesperCombo(i)

        '            If CompareTime < SessionTime Or SessionTime = 0 Then
        '                SessionTime = CompareTime
        '                MinimumTimeIndex = i
        '            End If
        '        Next

        '    End If

        'Else

        ' Easy case - NOT WORKING RIGHT - LOOK AT FENRIR
        If NumberofProductionLines = 1 Then
            ' Nothing simpler than this, it's just the total time to make components back to back
            For i = 0 To SentTimes.Count - 1
                RemainingTimeSum = RemainingTimeSum + SentTimes(i)
            Next

            SessionTime = RemainingTimeSum

        Else
            If ComponentProductionTimes.Count <= NumberofProductionLines Then
                ' Just return the max, we can make all the others within the time to make the first,
                ' we have enough lines, and we have to wait for the first to end
                SessionTime = SentTimes.Max
            Else ' Have some extra bps to make vs. lines - IE 7 components and 4 lines
                ' The max time is the metric for time per session
                MaxComponentTime = SentTimes.Max
                ' Sort the array (in ascending order)
                SentTimes.Sort()
                ' Sum up the rest of the jobs, skipping the last one
                For i = 0 To SentTimes.Count - 2
                    RemainingTimeSum = RemainingTimeSum + SentTimes(i)
                Next

                If MaxComponentTime > RemainingTimeSum Then
                    ' We can do all jobs in the time it takes to make the longest one
                    SessionTime = MaxComponentTime
                Else
                    ' Have more than one set to do
                    ' Reset time
                    RemainingTimeSum = 0
                    JobTimeSum = 0
                    JobCount = 1 ' First new job
                    ' Loop through times, and save index of last time that fits, start largest to smallest skipping first one
                    For i = SentTimes.Count - 2 To 1 Step -1
                        JobTimeSum = JobTimeSum + SentTimes(i)
                        If JobTimeSum > MaxComponentTime Then
                            ' We went over, so pull off the last time and step back i
                            JobTimeSum = JobTimeSum - SentTimes(i)
                            i = i + 1
                            If JobCount < NumberofProductionLines Then
                                ' One production line gone and we have more, move to the next
                                JobCount = JobCount + 1
                                ' Save this job time
                                RemainingTimeSum = RemainingTimeSum + JobTimeSum
                                JobTimeSum = 0
                            Else
                                ' No more lines left, need to get a new job going
                                ' Need to add this to the time and exit
                                RemainingTimeSum = RemainingTimeSum + JobTimeSum
                                Exit For
                            End If
                        End If
                    Next

                    If JobCount = NumberofProductionLines Then
                        ' Need to get the time of the next session, call this again and get the next session time
                        Dim j As Integer = i - 1
                        ' Copy in the final values
                        For i = 0 To j - 1
                            Temp.Add(SentTimes(i))
                        Next

                        RemainingTimeSum = RemainingTimeSum + GetComponentProductionTime(Temp)
                    End If

                    ' Add up the final time
                    SessionTime = MaxComponentTime + RemainingTimeSum

                End If

            End If
        End If
        'End If

        Return SessionTime

    End Function

    ' Sets the Production time for this Blueprint
    Private Sub SetProductionTime()
        ' For total runs
        Dim FullJobSessions As Long = 0
        Dim JobsPerBatch As Long = 0

        ' If this is using advanced Industrial/Capital skills, then add this bonus as well to build time
        Dim AdvIndySkill As Double = 1
        Dim AdvCapskill As Double = 1
        Dim TempSkill As Integer = 0

        ' Include advanced industry skill bonuses
        TempSkill = ReqBuildSkills.GetSkillLevel(AdvISCSkill)
        If TempSkill <> 0 Then
            AdvIndySkill = 1 - (BPCharacter.Skills.GetSkillLevel(AdvISCSkill) * 0.01) ' 1% reduction in time per level
        End If

        TempSkill = ReqBuildSkills.GetSkillLevel(AdvCSCSkill)
        If TempSkill <> 0 Then
            AdvCapskill = 1 - (BPCharacter.Skills.GetSkillLevel(AdvCSCSkill) * 0.01) ' 1% reduction in time per level
        End If

        ' For 1 run of this item add in the modifier plus skill level modifiers
        BPProductionTime = BaseProductionTime * SetBPTimeModifier() * AdvManufacturingSkillLevelBonus * AdvIndySkill * AdvCapskill

        ' Figure out how many jobs per batch we need to run, find the smallest of the two
        If NumberofBlueprints > NumberofProductionLines Then
            JobsPerBatch = NumberofProductionLines
        ElseIf NumberofBlueprints <= NumberofProductionLines Then
            JobsPerBatch = NumberofBlueprints
        End If

        ' Batches more than runs aren't used, so just normalize to runs
        If JobsPerBatch > UserRuns Then
            JobsPerBatch = UserRuns
        End If

        ' Now find the number of job sessions of 1 run each we need to do, round up to next whole integer - 1.1 sessions is 2
        FullJobSessions = CLng(Math.Ceiling(UserRuns / JobsPerBatch))

        ' Total time is just the total sessions multiplied by the production time
        BPProductionTime = FullJobSessions * BPProductionTime

    End Sub

    Private Sub AdjustSellExcessValue(ByVal _SetTaxes As Boolean, ByVal _BrokerFeeData As BrokerFeeInfo)
        'Total up the excess material amounts
        SellExcessAmount = 0 ' reset

        If SellExcessItems Then
            For Each Item In ExcessMaterials.GetMaterialList
                SellExcessAmount += Item.GetTotalCost
            Next

            ' Apply taxes and fees
            SellExcessAmount = AdjustPriceforTaxesandFees(SellExcessAmount, _SetTaxes, _BrokerFeeData)

            If SellExcessAmount < 0 Then
                SellExcessAmount = 0
            End If
        End If
    End Sub

    ' Sets all price data for the user to get on this blueprint, Set public so can reset with fees/taxes
    Public Sub SetPriceData(ByVal SetTaxes As Boolean, ByVal BrokerFeeData As BrokerFeeInfo)
        Dim TaxesFees As Double = 0
        Dim ComponentUsage As Double = 0
        Dim MainFacilityUsage As Double = 0
        Dim RemainingReactionUsage As Double = 0

        If SetTaxes Then
            DisplayTaxes = GetSalesTax(ItemMarketCost)
        Else
            DisplayTaxes = 0
        End If

        DisplayBrokerFees = GetSalesBrokerFee(ItemMarketCost, BrokerFeeData)

        TaxesFees = DisplayTaxes + DisplayBrokerFees

        If ComponentManufacturingFacility.IncludeActivityUsage Then
            ComponentUsage += ComponentFacilityUsage
        End If

        If CapitalComponentManufacturingFacility.IncludeActivityUsage Then
            ComponentUsage += CapComponentFacilityUsage
        End If

        If ReactionFacility.IncludeActivityUsage And MainManufacturingFacility.FacilityProductionType = ProductionType.Reactions Then
            MainFacilityUsage = ReactionFacilityUsage
            ComponentUsage += ManufacturingFacilityUsage
            RemainingReactionUsage = TotalReactionFacilityUsage - ReactionFacilityUsage
        Else
            MainFacilityUsage = ManufacturingFacilityUsage
            ComponentUsage += ReactionFacilityUsage
        End If

        If Not IsNothing(ReprocessingFacility) Then
            If Not ReprocessingFacility.IncludeActivityUsage Then
                ' if we don't want to show this, reset to 0
                ReprocessingUsage = 0
            End If
        End If

        ' Finalize invention and copying usage and total cost of all invention
        Call SetCopyUsage()
        Call SetInventionUsage()

        ' Set copy and invention costs (total mats needed + usage) for the number of runs they want
        If IncludeInventionCosts Then
            ' Set the total cost for the sent runs by totaling all to get success needed, then dividing it by the runs invented
            ' (some bps have more runs than 1 - i.e. Drones = 10) to get the cost per run, then multiply that cost by the number of runs
            ' InventionCost = TotalInventionCost / TotalInventedRuns * UserRuns
            ' Use the Per Run Cost for a more accurate invention cost assuming a large number of runs - more accurate than small runs however will not be the exact cost of the invention materials needed
            InventionCost = PerInventionRunCost * UserRuns
        Else
            InventionCost = 0
        End If

        If IncludeCopyCosts And TechLevel <> BPTechLevel.T3 Then
            ' Set the total cost for the sent runs by totaling all to get success needed, then dividing it by the runs invented
            ' (some bps have more runs than 1 - i.e. Drones = 10) to get the cost per run, then multiply that cost by the number of runs
            CopyCost = TotalCopyCost / TotalInventedRuns * UserRuns
        Else
            CopyCost = 0
        End If

        TotalUsage = MainFacilityUsage + ComponentUsage + InventionUsage + CopyUsage + RemainingReactionUsage + ReprocessingUsage

        ' Set up the excess material amounts
        Call AdjustSellExcessValue(SetTaxes, BrokerFeeData)

        ' Totals 
        TotalRawCost = RawMaterials.GetTotalMaterialsCost + InventionCost + CopyCost + TaxesFees + AdditionalCosts + TotalUsage - SellExcessAmount
        TotalComponentCost = ComponentMaterials.GetTotalMaterialsCost + InventionCost + CopyCost + TaxesFees + AdditionalCosts + (TotalUsage - ComponentUsage - RemainingReactionUsage - ReprocessingUsage) - SellExcessAmount ' don't build components

        ' Don't include usage in the total cost above but if we are doing build/buy add it back
        If BuildBuy Then
            TotalComponentCost += (ComponentUsage + RemainingReactionUsage + ReprocessingUsage)
        Else
            ' For component cost without build/buy, we are buying all the components so nothing to build and sell
            TotalComponentCost += SellExcessAmount
        End If

        ' Profit market cost - total cost of mats and invention and fees
        TotalRawProfit = ItemMarketCost - TotalRawCost
        TotalComponentProfit = ItemMarketCost - TotalComponentCost

        If ItemMarketCost = 0 Then
            TotalRawProfitPercent = 0
            TotalComponentProfitPercent = 0
        Else
            TotalRawProfitPercent = 1 - (TotalRawCost / ItemMarketCost)
            TotalComponentProfitPercent = 1 - (TotalComponentCost / ItemMarketCost)
        End If

        ' Final Calculation
        ' ISK per Hour (divide total cost by production time in seconds for a isk per second calc, then multiply by 3600 for isk per hour)
        TotalIPHRaw = TotalRawProfit / TotalProductionTime * 3600 ' Build everything

        ' If we are doing build/buy then the total IPH will be the same as RAW since the lists are identical for what to buy 
        If BuildBuy Then
            TotalIPHComponent = TotalIPHRaw
        Else
            TotalIPHComponent = TotalComponentProfit / BPProductionTime * 3600 ' Buy all components, just production time of BP
        End If

    End Sub

    ' Determines if we should add this material to the list or not, based on passed settings
    Private Function AddMaterial(CategoryID As Integer, GroupID As Integer, IgnoreMinerals As Boolean, IgnoreT1BaseItem As Boolean) As Boolean

        If IgnoreT1BaseItem And IsT1BaseItemforT2(CategoryID) Then
            Return False
        End If

        If IgnoreMinerals And GroupID = 18 Then ' 18= Minerals
            Return False
        End If

        Return True

    End Function

    ' Determines if the item category sent is a T1 Base item used to make T2
    Private Function IsT1BaseItemforT2(CategoryID As Integer) As Boolean
        Select Case CategoryID
            Case 6, 7, 8, 18 '"Ship", "Drone", "Module", "Charge"
                Return True
            Case Else
                Return False
        End Select
    End Function

    ' Calculates the total material muliplier for the blueprint based on the bp, and facility
    Private Function SetBPMaterialModifier(ByRef SentFacility As IndustryFacility) As Double

        ' Material modifier is the BP ME, and Facility - Facility is saved as a straight multiplier, the others need to be set
        Return (1 - (iME / 100)) * SentFacility.MaterialMultiplier

    End Function

    ' Gets the ME/TE for the BP
    Public Sub GetMETEforBP(ByVal BlueprintID As Long, ByVal BPTech As Integer, ByRef RefME As Integer, ByRef RefTE As Integer, ByRef OwnedBP As Boolean)
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader

        ' See what ID we use for character bps
        Dim UserID As String
        If UserApplicationSettings.LoadBPsbyChar Then
            ' Use the ID sent
            UserID = CStr(SelectedCharacter.ID)
        Else
            UserID = CStr(CommonLoadBPsID)
        End If

        ' The user can't define an ME or TE for this blueprint, so just look it up
        SQL = "Select ME, TE, OWNED FROM OWNED_BLUEPRINTS WHERE USER_ID In (" & UserID & "," & BPCharacter.CharacterCorporation.CorporationID & ") "
        SQL &= " And BLUEPRINT_ID =" & CStr(BlueprintID) & " And OWNED <> 0 "
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader

        If readerLookup.Read Then
            RefME = readerLookup.GetInt32(0)
            RefTE = readerLookup.GetInt32(1)
            ' Check if owned
            OwnedBP = CBool(readerLookup.GetInt64(2))
        Else
            ' T2
            If BPTech = BPTechLevel.T2 Or BPTech = BPTechLevel.T3 Then
                RefME = BaseT2T3ME
                RefTE = BaseT2T3TE
            Else
                If Not IsReaction(ItemGroupID) Then
                    RefME = BPUserSettings.DefaultBPME
                    RefTE = BPUserSettings.DefaultBPTE
                Else
                    RefME = 0
                    RefTE = 0
                End If
            End If
            OwnedBP = False
        End If

        readerLookup.Close()

    End Sub

    ' Calculates the total time muliplier for the blueprint based on the bp, facility, amd implants
    Private Function SetBPTimeModifier() As Double
        Dim Modifier As Double

        ' Time modifier is the BP ME, and Facility - Facility is saved as a straight multiplier, the others need to be set, then do the skills
        Modifier = (1 - (iTE / 100)) * MainManufacturingFacility.TimeMultiplier * AIImplantValue * (1 - (IndustrySkill * 0.04)) * (1 - (AdvancedIndustrySkill * 0.03))

        Return Modifier

    End Function

    ' Returns T/F if the user has the required skills sent in when compared to character skills
    Private Function UserHasReqSkills(ByVal EVESkillList As EVESkillList, ByVal RequiredSkills As EVESkillList) As Boolean
        Dim i As Integer
        Dim SkillFound As Boolean = False
        Dim HasSkills As Boolean = False

        ' Compare the required invention skills from blueprint to user skills
        ' Start looping through the skills for the blueprint
        If RequiredSkills.NumSkills <> 0 Then
            For i = 0 To RequiredSkills.GetSkillList.Count - 1
                ' Check for the skill in the character skills for the appropriate level
                If EVESkillList.GetSkillLevel(RequiredSkills.GetSkillList(i).TypeID) <> 0 Then
                    SkillFound = True
                End If

                If SkillFound Then
                    If EVESkillList.GetSkillLevel(RequiredSkills.GetSkillList(i).TypeID) <RequiredSkills.GetSkillList(i).Level Then
                        ' They have this skill but it isn't the correct level
                                                                                                 ' They don't have this, so just leave
                                                                                                 Return False
                    End If
                Else
                    ' Skill not found, just leave
                    Return False
                End If

                SkillFound = False
            Next
        Else
            ' If the Req Skills is empty, then return true since there are no required skills
            Return True
        End If

        Return True

    End Function

    ' Sets the fees for setting up a job to build this item
    Private Sub SetManufacturingCostsAndFees()
        Dim FacilityUsage As Double = 0
        Dim TotalEIV As Double = 0
        Dim Indexbonuses As Double = 0

        AlphaCloneTax = 0

        ' Formula: FacilityUsage = EstItemValue * ((SystemCostIndex * bonuses) + FacilityTax + SCC + AlphaClone) 

        If IncludeManufacturingUsage Then
            ' EIV = Sum(eachmaterialquantity * adjustedPrice) - set in build function
            TotalEIV = CLng(EIV * UserRuns)
            Indexbonuses = MainManufacturingFacility.CostIndex * MainManufacturingFacility.CostMultiplier

            ' Set Alpha tax
            If BPUserSettings.AlphaAccount Then
                AlphaCloneTax = AlphaAccountTaxRate
            End If

            FacilityUsage = TotalEIV * (Indexbonuses + MainManufacturingFacility.TaxRate + SCCIndustryFeeSurcharge + AlphaCloneTax)
        Else
            FacilityUsage = 0
        End If

        If MainManufacturingFacility.FacilityProductionType = ProductionType.Reactions Then
            ReactionFacilityUsage = FacilityUsage
            TotalReactionFacilityUsage += FacilityUsage
        Else
            ManufacturingFacilityUsage = FacilityUsage
        End If

    End Sub

    ' Totals up all the skill levels for advanced manufacturing skills for TE reduction bonus
    Private Function SetAdvManufacturingSkillLevels(BuildSkills As EVESkillList) As Double
        Dim BonusSum As Double = 1

        'These skills for T2 now reduce TE by 1% per level for the manufacturing job with Pheobe
        '3398	Advanced Large Ship Construction
        '3397	Advanced Medium Ship Construction
        '3395	Advanced Small Ship Construction
        '11444	Amarr Starship Engineering
        '11454	Caldari Starship Engineering
        '11448  Electromagnetic Physics
        '11453  Electronic Engineering
        '11450	Gallente Starship Engineering
        '11446  Graviton Physics
        '11433	High Energy Physics
        '11443  Hydromagnetic Physics
        '11447  Laser Physics
        '11452  Mechanical Engineering
        '11445	Minmatar Starship Engineering
        '11529  Molecular Engineering
        '11451  Nuclear Physics
        '11441  Plasma Physics
        '11455  Quantum Physics
        '11449  Rocket Science

        ' Read through all the skills and if the ID is in the list, then sum up the levels
        For i = 0 To BuildSkills.NumSkills - 1
            Select Case BuildSkills.GetSkillList(i).TypeID
                Case 3398, 3397, 3395, 11444, 11454, 11448, 11453, 11450, 11446, 11433, 11443, 11447, 11452, 11445, 11529, 11451, 11441, 11455, 11449
                    ' each skill is mulitplied by 1% then normalized percentage, then mulitiplied to any others to get the bonus
                    BonusSum = BonusSum * (1 - 0.01 * BPCharacter.Skills.GetSkillLevel(BuildSkills.GetSkillList(i).TypeID))
            End Select
        Next

        Return BonusSum

    End Function

    ' Determines if the item we are building should be bought or built for the main bp
    Private Function GetBuildFlag(ByVal ItemBlueprint As Blueprint, ByVal OneItemMarketCost As Double, ByVal Runs As Long,
                                  ByVal OwnedBP As Boolean, ByVal SetTaxes As Boolean, ByVal BFData As BrokerFeeInfo) As Boolean
        Dim CheapertoBuild As Boolean = False
        Dim ExcessAmount As Double = 0

        ' First, check the overrides based on settings
        If (BPUserSettings.AlwaysBuyFuelBlocks And ItemBlueprint.BlueprintName.Contains("Fuel Block")) Or (BPUserSettings.AlwaysBuyRAMs And ItemBlueprint.BlueprintName.Contains("R.A.M.")) Then
            Return False
        End If

        ' Get the excess amount cost of the build item for checking build/buy
        If SellExcessItems Then
            ExcessAmount = ItemBlueprint.BPExcessMaterials.GetTotalMaterialsCost
            ' Add any excess for the main component too
            If SetTaxes Then
                ExcessAmount -= GetSalesTax(ExcessAmount)
            End If

            ExcessAmount -= GetSalesBrokerFee(ExcessAmount, BFData)
        End If

        ' See if the costs to build are less than buy - cost to buy is greater than cost to build (compare total portion size for component)
        If ((OneItemMarketCost * ItemBlueprint.GetPortionSize) * Runs) > (ItemBlueprint.GetTotalBuildCost - ExcessAmount) Then
            CheapertoBuild = True
        End If

        ' Regardless of whatever is set, we want to override with what is in the BB list for base items on the main bp
        Dim BuildItem As Boolean = CheapertoBuild

        ' First time you run the bp, just use the base check 
        If NewBPRequest Then
            BuildItem = (OneItemMarketCost = 0) Or (CheapertoBuild And ((BPUserSettings.SuggestBuildBPNotOwned) Or (OwnedBP And Not BPUserSettings.SuggestBuildBPNotOwned)))
        Else
            ' Look up the override value and if not found, use the default
            BuildItem = ManualBuildBuyValue(ItemBlueprint.ItemID, CheapertoBuild)
        End If

        Return BuildItem

    End Function

    ' Returns the BPC used as a string
    Public Function GetInventionBPC() As String

        For i = 0 To InventionMaterials.GetMaterialList.Count - 1
            If InventionMaterials.GetMaterialList(i).GetMaterialTypeID = InventionBPCTypeID Then
                Return InventionMaterials.GetMaterialList(i).GetMaterialName
            End If
        Next

        Return ""
    End Function

    ' Predicate for finding the BuildBuyItem in full list
    Public Function FindBBItem(ByVal Item As BuildBuyItem) As Boolean
        If BBItemtoFind = Item.ItemID Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "Invention/RE Functions"

    ' Sets the invention cost and materials for this BP
    Private Function InventREBlueprint(Optional ByVal UseTypical As Boolean = False) As Integer
        Dim AvgRunsforSuccess As Double
        Dim readerBP As SQLiteDataReader
        Dim readerCost As SQLiteDataReader
        Dim MatCost As Double = 0

        Dim SQL As String
        Dim InventionMat As Material = Nothing
        Dim CopyMat As Material = Nothing
        Dim SingleInventionMats As New Materials
        Dim SingleCopyMats As New Materials
        Dim NumInventionSessions As Integer = 0 ' How many sessions (runs per set of lines) ie. 10 runs 5 lines = 2 sessions

        ' First select the datacores needed
        SQL = "SELECT MATERIAL_ID, MATERIAL, MATERIAL_CATEGORY, QUANTITY, MATERIAL_VOLUME, PRICE, MATERIAL_GROUP "
        SQL &= "FROM ALL_BLUEPRINT_MATERIALS LEFT OUTER JOIN ITEM_PRICES_FACT On ALL_BLUEPRINT_MATERIALS.MATERIAL_ID = ITEM_PRICES_FACT.ITEM_ID "
        SQL &= "WHERE BLUEPRINT_ID = " & InventionBPCTypeID & " And PRODUCT_ID = " & BlueprintID & " "
        SQL &= "AND ACTIVITY = 8 And MATERIAL_GROUP = 'Datacores'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader()

        ' Get all the Datacores
        While readerBP.Read
            ' Add this to the invention materials - add price for data cores
            InventionMat = New Material(readerBP.GetInt64(0), readerBP.GetString(1), readerBP.GetString(2),
                                    readerBP.GetInt64(3), readerBP.GetDouble(4), If(readerBP.IsDBNull(5), 0, readerBP.GetDouble(5)), "", "")
            SingleInventionMats.InsertMaterial(InventionMat)
        End While

        readerBP.Close()

        ' If they selected a decryptor, add that cost for one invention run
        If InventionDecryptor.Name <> None Then
            InventionMat = New Material(InventionDecryptor.TypeID, InventionDecryptor.Name, "Decryptors", 1, 0.1, GetItemPrice(InventionDecryptor.TypeID), "", "")
            SingleInventionMats.InsertMaterial(InventionMat)
        End If

        ' If this is T3, get the relic and add it to the list of invention materials
        If TechLevel = BPTechLevel.T3 Then
            ' Look up the cost for the material
            SQL = "SELECT PRICE, ITEM_NAME FROM ITEM_PRICES WHERE ITEM_ID =" & InventionBPCTypeID

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerCost = DBCommand.ExecuteReader

            If readerCost.Read Then
                InventionMat = New Material(InventionBPCTypeID, readerCost.GetString(1), "Ancient Relics", 1, 100, readerCost.GetDouble(0), "", "")
                SingleInventionMats.InsertMaterial(InventionMat)
            End If

            readerCost.Close()

        ElseIf TechLevel = BPTechLevel.T2 Then
            SQL = "SELECT typeName, PRICE FROM INVENTORY_TYPES "
            SQL &= "LEFT OUTER JOIN ITEM_PRICES ON typeID = ITEM_ID "
            SQL &= "WHERE typeID = " & CStr(InventionBPCTypeID)

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader()
            readerBP.Read()

            If Not IsNothing(readerBP.GetValue(0)) Then
                Dim Price As Double = 0
                If Not IsNothing(readerBP.GetValue(1)) Then
                    Price = readerBP.GetDouble(1)
                End If
                ' Add the T2 blueprint and cost
                Dim TempMat = New Material(InventionBPCTypeID, readerBP.GetString(0) & " Copy", "Blueprint", 1, 0.1, Price, "", "")
                SingleInventionMats.InsertMaterial(TempMat)
            End If
            readerBP.Close()
        End If

        ' Get and set the invention chance
        InventionChance = SetInventionChance(UseTypical)

        ' Use the max runs for the T2 item and this should be the invented runs for one bpc
        If TechLevel = BPTechLevel.T2 Then
            SingleInventedBPCRuns = MaxProductionLimit + InventionDecryptor.RunMod
        Else
            ' Base it off of the relic type - need to look it up based on the TypeID
            SQL = "SELECT typeName, quantity FROM INVENTORY_TYPES, INDUSTRY_ACTIVITY_PRODUCTS "
            SQL &= "WHERE typeID = blueprintTypeID And typeID = " & CStr(InventionBPCTypeID) & " AND productTypeID = " & CStr(BlueprintID)

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader()

            If readerBP.Read Then
                ' Set the name
                Relic = readerBP.GetString(0)
                MaxProductionLimit = readerBP.GetInt32(1)
            Else
                MaxProductionLimit = 3
                Relic = WreckedRelic
            End If

            ' Set the final runs for one bp
            SingleInventedBPCRuns = MaxProductionLimit + InventionDecryptor.RunMod
        End If

        ' Averages and final cost per run
        AvgRunsforSuccess = 1 / InventionChance

        ' Set how many total invention runs we will need to do - take the number of bpc's we'll need and multiply by how many runs for a success - round up
        If InventionChance <> 0 Then
            NumInventionJobs = CInt(Math.Ceiling(AvgRunsforSuccess * Math.Ceiling(UserRuns / SingleInventedBPCRuns)))
        End If

        ' Now set the total runs we will get from all jobs
        TotalInventedRuns = CInt(Math.Ceiling(UserRuns / SingleInventedBPCRuns) * SingleInventedBPCRuns)

        ' Find the number of invention sessions we'll need to invent the number of runs for this item. This will be used in the copy and invention times
        ' Basically, the number avg number of runs for success times the total runs wanted is the total invention runs needed for single runs. Divide this
        ' by the invented runs, then divide that by how many laboratory lines we are using.  Need to round up each time
        ' Ex. avgruns = 2, user runs = 100, inventedruns = 10, lines = 10 => 200/10 = 20/10 = 2 invention sessions to get enough bps to make 100 runs.
        NumInventionSessions = CInt(Math.Ceiling(NumInventionJobs / NumberofLaboratoryLines))

        If IncludeCopyTime And TechLevel <> BPTechLevel.T3 Then
            ' Set the total copy time based on the number of invention jobs we need - assume only one bp to copy
            CopyTime = GetCopyTime(NumInventionJobs)
        Else
            CopyTime = 0 ' No copies for T3
        End If

        If IncludeCopyCosts And TechLevel <> BPTechLevel.T3 Then
            ' Get the copy materials and update
            SQL = "SELECT MATERIAL_ID, MATERIAL, MATERIAL_CATEGORY, QUANTITY, MATERIAL_VOLUME, PRICE, MATERIAL_GROUP "
            SQL &= "FROM ALL_BLUEPRINT_MATERIALS LEFT OUTER JOIN ITEM_PRICES_FACT ON ALL_BLUEPRINT_MATERIALS.MATERIAL_ID = ITEM_PRICES_FACT.ITEM_ID "
            SQL &= "WHERE BLUEPRINT_ID = " & InventionBPCTypeID & " AND PRODUCT_ID = " & InventionBPCTypeID & " "
            SQL &= "AND ACTIVITY = 5 AND MATERIAL_CATEGORY <> 'Skill'"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerBP = DBCommand.ExecuteReader()

            ' Get all the mats and add
            While readerBP.Read
                ' Add this to the copy materials 
                CopyMat = New Material(readerBP.GetInt64(0), readerBP.GetString(1), readerBP.GetString(2),
                                        readerBP.GetInt64(3), readerBP.GetDouble(4), If(readerBP.IsDBNull(5), 0, readerBP.GetDouble(5)), "", "")
                SingleCopyMats.InsertMaterial(CopyMat)
            End While

            readerBP.Close()

            If Not IsNothing(SingleCopyMats.GetMaterialList) Then
                ' Update the copy mats to reflect the number of copy runs we will do and save into the final list
                For i = 0 To SingleCopyMats.GetMaterialList.Count - 1
                    SingleCopyMats.GetMaterialList(i).SetQuantity(SingleCopyMats.GetMaterialList(i).GetQuantity * NumInventionJobs)
                Next

                ' Now insert all the materials in a new list to get the correct cost (kind of a hack, need a better process - no automatic way to update the total price in a material list)
                For i = 0 To SingleCopyMats.GetMaterialList.Count - 1
                    CopyMaterials.InsertMaterial(SingleCopyMats.GetMaterialList(i))
                Next
            End If

            ' Finally set the cost
            TotalCopyCost = CopyMaterials.GetTotalMaterialsCost
        Else
            TotalCopyCost = 0 ' No copies for T3
        End If

        ' Set invention time
        If IncludeInventionTime Then
            Call SetInventionTime(NumInventionSessions)
        End If

        ' Finally set the total invention cost for just inventing
        If IncludeInventionCosts Then

            ' Before updating the materials, use the cost of a single run to determine a single run invention cost. 
            ' This Is an accurate cost based on the probability of success with a large number of runs
            PerInventionRunCost = (SingleInventionMats.GetTotalMaterialsCost() / InventionChance) / SingleInventedBPCRuns

            ' Update the invention mats to reflect the number of invention runs we will do and save into the final list
            For i = 0 To SingleInventionMats.GetMaterialList.Count - 1
                SingleInventionMats.GetMaterialList(i).SetQuantity(SingleInventionMats.GetMaterialList(i).GetQuantity * NumInventionJobs)
            Next

            ' Now insert all the materials in a new list to get the correct cost (kind of a hack, need a better process - no automatic way to update the total price in a material list)
            For i = 0 To SingleInventionMats.GetMaterialList.Count - 1
                InventionMaterials.InsertMaterial(SingleInventionMats.GetMaterialList(i))
            Next

        End If

        Return CInt(Math.Ceiling(TotalInventedRuns / SingleInventedBPCRuns))

    End Function

    ' Sets the usage for Copying, which is based on the base mats cost
    Private Sub SetCopyUsage()

        If IncludeCopyUsage And TechLevel <> BPTechLevel.T3 Then
            ' Set the copy cost based on the number of copies we'll need for these runs
            CopyUsage = GetCopyUsage(NumInventionJobs) / TotalInventedRuns
            'InventionMaterials.InsertMaterial(New Material(0, "Copy Usage", "Usage", 1, 0, CopyUsage, ""))
        Else
            CopyUsage = 0 ' No copies for T3
        End If
    End Sub

    ' Sets the usage for Invention, which is based on the base mats cost
    Private Sub SetInventionUsage()

        If IncludeInventionUsage Then
            ' Set the usage for these invention runs only
            InventionUsage = GetInventionUsage(NumInventionJobs) / TotalInventedRuns
            ' Add the usage
            'InventionMaterials.InsertMaterial(New Material(0, "Invention Usage", "Usage", 1, 0, InventionUsage, ""))
        Else
            InventionUsage = 0
        End If

    End Sub

    ' Sets the invention chance of the blueprint if set
    Private Function SetInventionChance(ByVal UseTypical As Boolean) As Double
        Dim BaseInventionChance As Double
        Dim i As Integer = 0
        Dim readerLookup As SQLiteDataReader
        Dim SQL As String

        Dim EncryptionSkillLevel As Integer
        Dim InventionSkillLevels As New List(Of Integer) ' 

        ' Get the base invention chance from the activities for the T1 BPO
        SQL = "SELECT probability FROM INDUSTRY_ACTIVITY_PRODUCTS WHERE blueprintTypeID = " & InventionBPCTypeID
        SQL &= " AND activityID = 8 AND productTypeID = " & BlueprintID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader()
        readerLookup.Read()

        If readerLookup.HasRows Then
            BaseInventionChance = readerLookup.GetDouble(0)
        Else
            BaseInventionChance = 0
        End If

        readerLookup.Close()

        ' Pull out the invention skills
        For i = 0 To ReqInventionSkills.GetSkillList.Count - 1
            ' Look up the level of the character's skills
            If ReqInventionSkills.GetSkillList(i).Name.Contains("Encryption") Then
                EncryptionSkillLevel = BPCharacter.Skills.GetSkillLevel(ReqInventionSkills.GetSkillList(i).TypeID)
            Else
                ' A datacore or supporting skill (i.e. cap ship construction)
                InventionSkillLevels.Add(BPCharacter.Skills.GetSkillLevel(ReqInventionSkills.GetSkillList(i).TypeID))
            End If
        Next

        If Not UseTypical Then
            Dim TotalInventionSkillLevels As Integer = 0
            For Each skill In InventionSkillLevels
                TotalInventionSkillLevels += skill
            Next
            ' BaseChance * [ 1 + (((ScienceSkill1 + ScienceSkill2 + ...) / 30) + (EncryptionSkill / 40 ))]
            InventionChance = BaseInventionChance * (1 + (TotalInventionSkillLevels / 30) + (EncryptionSkillLevel / 40)) * InventionDecryptor.ProductionMod
            '(1 + (0.01 * EncryptionSkillLevel) + (0.02 * (DatacoreSkillLevels(0) + DatacoreSkillLevels(1)))) * InventionDecryptor.ProductionMod
        Else
            ' Just use typical invention costs - ie, all level 4 skills
            InventionChance = BaseInventionChance * (1 + (((4 + 4) / 30) + (4 / 40))) * InventionDecryptor.ProductionMod
        End If

        Return InventionChance

    End Function

    ' Sets the cost of doing the number of invention jobs sent
    Private Function GetInventionUsage(InventionJobs As Double) As Double
        'jobFee = baseJobCost * systemCostIndex * 0.02
        BaseInventionJobCost = GetBaseJobCostforBPC(InventionBPCTypeID)
        Dim InventionJobFee As Double = BaseInventionJobCost * InventionFacility.CostIndex * 0.02 * InventionJobs

        ' facilityUsage = (jobFee) * taxRate
        Dim InventionFacilityTax As Double = InventionJobFee * InventionFacility.TaxRate

        ' totalInstallationCost = jobFee + facilityTax * bonus for FW and invention facility
        Return (InventionJobFee + InventionFacilityTax) * FWInventionCostBonus * InventionFacility.CostMultiplier

    End Function

    ' Sets the invention time for the sent BP 
    Private Sub SetInventionTime(NumInventionSessions As Integer)
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader
        Dim TempTime As Double

        ' Look up the blueprint name from the sent blueprint ID
        If TechLevel = BPTechLevel.T3 Then
            ' Hardcode this to 3600 for now. Later need to figure out the logic for looking it up, since the "T1" BP is a relic, we can't do anything but invent it and don't want to include it in the all_blueprints table since we only use that to select what to build
            TempTime = 3600
        Else
            ' Look it up
            SQL = "SELECT BASE_INVENTION_TIME FROM ALL_BLUEPRINTS_FACT WHERE BLUEPRINT_ID =" & InventionBPCTypeID

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerLookup = DBCommand.ExecuteReader

            ' inventionTime = baseInventionTime * facilityModifier * 3% of AI level * implant (doesn't work)
            If readerLookup.Read Then
                TempTime = CDbl(readerLookup.GetInt64(0)) * InventionFacility.TimeMultiplier * (1 - (0.03 * AdvancedIndustrySkill)) * 1 '* InventionImplantValue
            Else
                TempTime = 0
            End If

            readerLookup.Close()
        End If

        ' Finally, set the time
        InventionTime = TempTime * NumInventionSessions

    End Sub

    ' Sets and returns the copy cost for the number of copies sent
    Private Function GetCopyUsage(NumberofCopies As Integer) As Double
        ' jobFee = baseJobCost * systemCostIndex * 0.02 * runs * runsPerCopy (just use the total number of copies here)
        BaseCopyJobCost = GetBaseJobCostforBPC(InventionBPCTypeID)
        Dim CopyJobFee As Double = BaseCopyJobCost * CopyFacility.CostIndex * 0.02 * NumberofCopies

        ' facilityUsage = jobFee * taxRate
        Dim CopyFacilityTax As Double = CopyJobFee * CopyFacility.TaxRate

        ' totalInstallationCost = jobFee +  facilityTax * bonus for FW and copy facility
        Return (CopyJobFee + CopyFacilityTax) * FWCopyingCostBonus * CopyFacility.CostMultiplier

    End Function

    ' Returns the copy time for a single T1 copy in seconds to copy the sent number of runs
    Private Function GetCopyTime(UserCopyRuns As Integer) As Double
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader
        Dim TempTime As Decimal

        ' Look up the blueprint name from the sent blueprint ID 
        SQL = "SELECT BASE_COPY_TIME FROM ALL_BLUEPRINTS_FACT WHERE BLUEPRINT_ID =" & InventionBPCTypeID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader

        ' copyTime = BaseCopyTime * runs * runsperBP * (1 - (0.05 * science)) * (1 - (0.03 * advancedindustry)) * facility copyslotmod * (1-implant)
        If readerLookup.Read Then ' just use the number of runs we need to make
            TempTime = CDec((readerLookup.GetInt64(0)) * (1 - (0.05 * ScienceSkill)) * (1 - (0.03 * AdvancedIndustrySkill)) * CopyFacility.TimeMultiplier * (1 - BPUserSettings.CopyImplantValue))
        Else
            TempTime = 0
        End If

        readerLookup.Close()

        Return TempTime * UserCopyRuns

    End Function

    ' Sets the skills for inventing this blueprint (T2 or T3 blueprint types)
    Private Sub SetInventionSkills()
        Dim SQL As String = ""
        Dim readerItems As SQLiteDataReader

        ' Tech 2 items are invented from T1 blueprint copies, so take the T1 component ID and look it up for
        ' the invention skill requirements (for datacores and data interface)
        SQL = "SELECT MATERIAL_ID, QUANTITY FROM ALL_BLUEPRINT_MATERIALS_FACT "
        SQL &= "WHERE BLUEPRINT_ID = " & InventionBPCTypeID & " "
        SQL &= "AND ACTIVITY = 8 AND MATERIAL_CATEGORY_ID = 16" ' 16 is Skill Category

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerItems = DBCommand.ExecuteReader

        ' Just add all the skills and levels
        While readerItems.Read
            ReqInventionSkills.InsertSkill(readerItems.GetInt64(0), readerItems.GetInt32(1), readerItems.GetInt32(1), readerItems.GetInt32(1), 0, False, 0, "", Nothing, True)
        End While

        readerItems.Close()

    End Sub

    ' Sets the skills for copying this blueprint (T2 or T3 blueprint types)
    Private Sub SetCopySkills()
        Dim SQL As String = ""
        Dim readerItems As SQLiteDataReader

        ' Tech 2 items are invented from T1 blueprint copies, so take the T1 component ID and look it up for
        ' the invention skill requirements (for datacores and data interface)
        SQL = "SELECT MATERIAL_ID, QUANTITY FROM ALL_BLUEPRINT_MATERIALS_FACT "
        SQL &= "WHERE BLUEPRINT_ID = " & InventionBPCTypeID & " "
        SQL &= "AND ACTIVITY = 5 AND MATERIAL_CATEGORY_ID = 16" ' 16 is Skill Category

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerItems = DBCommand.ExecuteReader

        ' Just add all the skills and levels
        While readerItems.Read
            ReqCopySkills.InsertSkill(readerItems.GetInt64(0), readerItems.GetInt32(1), readerItems.GetInt32(1), readerItems.GetInt32(1), 0, False, 0, "", Nothing, True)
        End While

        readerItems.Close()

    End Sub

    ' Gets the job fee for the BPC and not the current T2/T3 bp
    Private Function GetBaseJobCostforBPC(ByVal BPCTypeID As Long) As Double
        Dim SQL As String
        Dim readerLookup As SQLiteDataReader
        Dim BaseJobCost As Double = 0

        ' Look up the sum of the quantity from the sent BPC ID 
        SQL = "SELECT QUANTITY, ADJUSTED_PRICE FROM ALL_BLUEPRINT_MATERIALS_FACT "
        SQL &= "LEFT OUTER JOIN ITEM_PRICES_FACT ON ALL_BLUEPRINT_MATERIALS_FACT.MATERIAL_ID = ITEM_PRICES_FACT.ITEM_ID "
        SQL &= "WHERE BLUEPRINT_ID =" & InventionBPCTypeID & " AND ACTIVITY IN (1,11) "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLookup = DBCommand.ExecuteReader

        While readerLookup.Read
            BaseJobCost += readerLookup.GetInt64(0) * If(IsDBNull(readerLookup.GetValue(1)), 0, readerLookup.GetDouble(1))
        End While

        readerLookup.Close()

        Return BaseJobCost

    End Function

#End Region

#Region "Get Functions"

#Region "Invention Gets"

    ' Returns the copy time for a T1 copy used to make a T2
    Public Function GetCopyTime() As Double
        Return CopyTime
    End Function

    ' Returns the total usage cost to make the copy
    Public Function GetCopyUsage() As Double
        Return CopyUsage
    End Function

    ' Returns the total cost of materials for the copy
    Public Function GetCopyCost() As Double
        Return CopyCost
    End Function

    ' Returns the list of materials used to make a copy for this BP
    Public Function GetCopyMaterials() As Materials
        Return CopyMaterials
    End Function

    ' Returns the invention time in friendly format it took to make a T2/T3 BPC 
    Public Function GetInventionTime() As Double
        Return InventionTime
    End Function

    ' Gets the invention usage fees for installing this invention job for this BP
    Public Function GetInventionUsage() As Double
        Return InventionUsage
    End Function

    ' Gets the total Invention Cost of this Blueprint if it can be invented
    Public Function GetInventionCost() As Double
        Return InventionCost
    End Function

    ' Returns the list of invention materials used
    Public Function GetInventionMaterials() As Materials
        Return InventionMaterials
    End Function

    ' Returns the decryptor used in this BP
    Public Function GetDecryptor() As Decryptor
        Return InventionDecryptor
    End Function

    ' Returns the Relic name used in this BP
    Public Function GetRelic() As String
        Return Relic
    End Function

    ' Gets the Invention Chance this blueprint is invented if it can be
    Public Function GetInventionChance() As Double
        Return InventionChance
    End Function

    ' Gets the total invented runs for each BPC
    Public Function GetSingleInventedBPCRuns() As Integer
        Return SingleInventedBPCRuns
    End Function

    ' Gets the total runs invented for the entire set of runs
    Public Function GetTotalInventedRuns() As Integer
        Return TotalInventedRuns
    End Function

    ' Returns the number of jobs we'll have to do
    Public Function GetInventionJobs() As Integer
        Return NumInventionJobs
    End Function

    ' Returns the Invention facility we used
    Public Function GetInventionFacility() As IndustryFacility
        Return InventionFacility
    End Function

    ' Returns the Copy facility we used
    Public Function GetCopyFacility() As IndustryFacility
        Return CopyFacility
    End Function

#End Region

    ' Returns the manufacturing facility used
    Public Function GetManufacturingFacility() As IndustryFacility
        Return MainManufacturingFacility
    End Function

    ' Returns the component manufacturing facility used
    Public Function GetComponentManufacturingFacility() As IndustryFacility
        Return ComponentManufacturingFacility
    End Function

    ' Returns the capital component manufacturing facility used
    Public Function GetCapitalComponentManufacturingFacility() As IndustryFacility
        Return CapitalComponentManufacturingFacility
    End Function

    ' Returns the Reaction facility used
    Public Function GetReactionFacility() As IndustryFacility
        Return ReactionFacility
    End Function

    ' Returns the Reprocessing facility used
    Public Function GetReprocessingFacility() As IndustryFacility
        Return ReprocessingFacility
    End Function

    ' Returns the base job cost for this blueprint
    Public Function GetEstimatedItemValue() As Double
        Return EIV
    End Function

    ' Returns the base job cost for the BPC to make this bp
    Public Function GetBaseInventionJobCost() As Double
        Return BaseInventionJobCost
    End Function

    ' Returns the base job cost for the BPC to make the invention bpc
    Public Function GetBaseCopyJobCost() As Double
        Return BaseCopyJobCost
    End Function

    ' Returns the Job fee based on the system index
    Public Function GetJobFee() As Double
        Return JobFee
    End Function

    ' Returns the cost of setting up a job to build this item
    Public Function GetManufacturingFacilityUsage() As Double
        Return ManufacturingFacilityUsage
    End Function

    ' Returns the base facility tax/fee for this blueprints components
    Public Function GetComponentFacilityUsage() As Double
        Return ComponentFacilityUsage
    End Function

    ' Returns the base facility tax/fee for this blueprint's cap components
    Public Function GetCapComponentFacilityUsage() As Double
        Return CapComponentFacilityUsage
    End Function

    ' Returns the base facility tax/fee usage for this blueprints reactions
    Public Function GetReactionFacilityUsage() As Double
        Return ReactionFacilityUsage
    End Function

    ' Returns the total facility tax/fee usage for all reaction usage
    Public Function GetTotalReactionFacilityUsage() As Double
        Return TotalReactionFacilityUsage
    End Function

    ' Returns the cost to refine any ores that were converted from minerals
    Public Function GetReprocessingUsage() As Double
        Return ReprocessingUsage
    End Function

    ' Returns the max production limit for this blueprint
    Public Function GetMaxProductionLimit() As Long

        If TechLevel = 1 Then
            Return MaxProductionLimit
        ElseIf TechLevel = 2 Then
            If TotalInventedRuns = 0 Then
                Return MaxProductionLimit
            Else
                Return TotalInventedRuns
            End If
        Else
            Return MaxProductionLimit
        End If

    End Function

    ' Returns the Tech Level of the blueprint
    Public Function GetTechLevel() As Integer
        Return TechLevel
    End Function

    ' Returns the Runs that were sent to this blueprint
    Public Function GetUserRuns() As Long
        Return UserRuns
    End Function

    ' Returns the item type of the blueprint, which is really the Tech that I set instead of what is in the DB I.e. 'Augmented' drones show as T2 but act more like faction even though the BP's need T2 skills - however, they can't be invented anymore
    Public Function GetItemType() As Integer
        Return ItemType
    End Function

    ' Returns the Item ID made from this blueprint
    Public Function GetItemID() As Long
        Return ItemID
    End Function

    ' Returns the item name made from this blueprint
    Public Function GetItemName() As String
        Return ItemName
    End Function

    ' Returns the sum of taxes for setting up a sell order for this BP item
    Public Function GetSalesTaxes() As Double
        Return DisplayTaxes
    End Function

    ' Returns the total broker fees for 
    Public Function GetSalesBrokerFees() As Double
        Return DisplayBrokerFees
    End Function

    ' Returns the total units this blueprint muliplied by runs, will create
    Public Function GetTotalUnits() As Long
        Return PortionSize * UserRuns
    End Function

    Public Function GetPortionSize() As Long
        Return PortionSize
    End Function

    ' Returns the production time as a double for just the blueprint
    Public Function GetProductionTime() As Double
        Return BPProductionTime
    End Function

    ' Returns the production time as a double for all components
    Public Function GetTotalProductionTime() As Double
        Return TotalProductionTime
    End Function

    ' Returns the Race ID of the item built by this BP
    Public Function GetRaceID() As Integer
        Return BlueprintRace
    End Function

    ' Returns the category id for the item this BP builds
    Public Function GetItemCategoryID() As Integer
        Return ItemCategoryID
    End Function

    ' Function returns the array of the required character skills for building this blueprint
    Public Function GetReqBPSkills() As EVESkillList
        Return ReqBuildSkills
    End Function

    ' Returns the total cost of the blueprint using raw materials
    Public Function GetTotalRawCost() As Double
        Return TotalRawCost
    End Function

    ' Returns the total build cost, which is everything except taxes and fees (? Not sure why I did this - it doesn't match up with double click to BP tab)
    Public Function GetTotalBuildCost() As Double
        Return TotalRawCost
        'Dim BuildCost As Double = TotalRawCost

        'If DisplayTaxes <> 0 Then
        '    BuildCost -= Taxes
        'End If
        'Return BuildCost - BrokerFees
    End Function

    ' Returns the total cost of the blueprint using components
    Public Function GetTotalComponentCost() As Double
        Return TotalComponentCost
    End Function

    ' Returns the total profit for the blueprint using raw materials
    Public Function GetTotalRawProfit() As Double
        Return TotalRawProfit
    End Function

    ' Returns the total profit for the blueprint using components
    Public Function GetTotalComponentProfit() As Double
        Return TotalComponentProfit
    End Function

    ' Returns the total profitas a percent for the blueprint using raw materials
    Public Function GetTotalRawProfitPercent() As Double
        Return TotalRawProfitPercent
    End Function

    ' Returns the total profit as a percent for the blueprint using components
    Public Function GetTotalComponentProfitPercent() As Double
        Return TotalComponentProfitPercent
    End Function

    ' Returns the amount we used to calculate sell excess
    Public Function GetSellExcessAmount() As Double
        Return SellExcessAmount
    End Function

    ' Returns the excess materials in a list of materials 
    Public Function GetExcessMaterials() As Materials
        Return ExcessMaterials
    End Function

    ' Returns the Isk per hour using Raw mats
    Public Function GetTotalIskperHourRaw() As Double
        Return TotalIPHRaw
    End Function

    ' Returns the Isk per hour using components
    Public Function GetTotalIskperHourComponents() As Double
        Return TotalIPHComponent
    End Function

    ' Returns whether this BP had buildable components or not
    Public Function HasComponents() As Boolean
        Return HasBuildableComponents
    End Function

    ' Returns the BP ID
    Public Function GetBPID() As Integer
        Return BlueprintID
    End Function

    ' Gets the Item's GroupID of the blueprint
    Public Function GetItemGroupID() As Integer
        Return ItemGroupID
    End Function

    ' Gets the built item's volume
    Public Function GetItemVolume() As Double
        Return ItemVolume
    End Function

    ' Gets the total volume of the items built
    Public Function GetTotalItemVolume() As Double
        Return ItemVolume * UserRuns
    End Function

    ' Returns the required skills to build all the components for this bp
    Public Function GetReqComponentSkills() As EVESkillList
        Return ReqBuildComponentSkills
    End Function

    ' Function returns the array of all the character skills to invent this blueprint
    Public Function GetReqInventionSkills() As EVESkillList
        Return ReqInventionSkills
    End Function

    ' Function returns the array of all the character skills to copy this blueprint
    Public Function GetReqCopySkills() As EVESkillList
        Return ReqCopySkills
    End Function

    ' Returns the total list of raw materials for the Blueprint
    Public Function GetRawMaterials() As Materials
        Return RawMaterials
    End Function

    ' Returns the Components and other mats for the Blueprint
    Public Function GetComponentMaterials() As Materials
        Return ComponentMaterials
    End Function

    Private Function GetBPRawMaterials() As Materials
        Return BPRawMats
    End Function

    ' Returns the component lists used to build this item, with materials
    Public Function GetComponentsList() As BuiltItemList
        Return BuiltComponentList
    End Function

    ' Returns information on the item that this BP makes, For now, name, runs and the type id
    Public Function GetItemData() As Material
        Dim TempMat As Material
        Dim rsGroup As SQLiteDataReader
        DBCommand = New SQLiteCommand("SELECT groupName FROM INVENTORY_GROUPS WHERE groupID = " & CStr(ItemGroupID), EVEDB.DBREf)
        rsGroup = DBCommand.ExecuteReader
        rsGroup.Read()

        ' Volume doesn't matter
        TempMat = New Material(ItemID, ItemName, rsGroup.GetString(0), UserRuns, 0, ItemMarketCost, "", "")

        rsGroup.Close()

        Return TempMat

    End Function

    ' Returns the TypeID of the BP
    Public Function GetTypeID() As Integer
        Return BlueprintID
    End Function

    ' Returns the blueprint name
    Public Function GetName() As String
        Return BlueprintName
    End Function

    ' Returns the number of blueprints used
    Public Function GetUsedNumBPs() As Integer
        Return NumberofBlueprints
    End Function

    ' Gets the market price of the produced item from this blueprint
    Public Function GetItemMarketPrice() As Double
        Return ItemMarketCost
    End Function

    ' Gets the raw build cost for one unit
    Public Function GetRawItemUnitPrice() As Double
        Return GetTotalBuildCost() / PortionSize
    End Function

    ' Gets the component build cost for one unit
    Public Function GetComponentItemUnitPrice() As Double
        Return GetTotalComponentCost() / PortionSize
    End Function

    ' Returns T/F if the user for this blueprint can build  the blueprint
    Public Function UserCanBuildBlueprint() As Boolean
        Return CanBuildBP
    End Function

    ' Returns T/F if the user for this blueprint can build all components
    Public Function UserCanBuildAllComponents() As Boolean
        Return CanBuildAll
    End Function

    Public Function UserCanInventRE() As Boolean
        Return CanInventRE
    End Function

    ' Returns the ME of the blueprint
    Public Function GetME() As Integer
        Return iME
    End Function

    ' Returns the TE of the blueprint
    Public Function GetTE() As Integer
        Return iTE
    End Function

    ' Returns the additional costs the user sent in
    Public Function GetAdditionalCosts() As Double
        Return AdditionalCosts
    End Function

#End Region

End Class