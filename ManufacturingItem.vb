' The manufacturing item to load the grid
Public Class ManufacturingItem
    Implements ICloneable

    Public RecordID As Long ' Unique record id

    Public BPID As Long
    Public ItemGroup As String
    Public ItemGroupID As Integer
    Public ItemCategory As String
    Public ItemCategoryID As Integer
    Public ItemTypeID As Long
    Public ItemName As String
    Public TechLevel As String
    Public Owned As String
    Public BPME As Double
    Public BPTE As Double
    Public Inputs As String
    Public Profit As Double
    Public ProfitPercent As Double
    Public IPH As Double
    Public TotalCost As Double
    Public CalcType As String ' Type of calculation to get the profit - either Components, Raw Mats or Build/Buy
    Public BlueprintType As BPType

    Public Runs As Integer
    Public SingleInventedBPCRunsperBPC As Integer
    Public ProductionLines As Integer
    Public LaboratoryLines As Integer

    ' Inputs
    Public Decryptor As Decryptor
    Public Relic As String

    ' Can do variables
    Public CanBuildBP As Boolean
    Public CanInvent As Boolean
    Public CanRE As Boolean

    Public SVR As Object ' Sales volume ratio, set to an object because this can be Nothing
    Public SVRxIPH As Object ' could be nothing

    Public ManufacturingFacility As IndustryFacility
    Public ManufacturingFacilityUsage As Double
    Public ComponentManufacturingFacility As IndustryFacility
    Public ComponentManufacturingFacilityUsage As Double
    Public CapComponentManufacturingFacility As IndustryFacility
    Public CapComponentManufacturingFacilityUsage As Double

    Public CopyCost As Double
    Public CopyFacilityUsage As Double
    Public CopyFacility As IndustryFacility

    Public InventionCost As Double
    Public InventionFacilityUsage As Double
    Public InventionFacility As IndustryFacility

    Public BPProductionTime As String
    Public TotalProductionTime As String
    Public CopyTime As String
    Public InventionTime As String

    Public ItemMarketPrice As Double

    Public BrokerFees As Double
    Public Taxes As Double

    Public BaseJobCost As Double
    Public NumBPs As Integer
    Public InventionChance As Double
    Public Race As String
    Public VolumeperItem As Double
    Public TotalVolume As Double

    Public JobFee As Double
    Public TeamFee As Double

    Public ManufacturingTeam As IndustryTeam
    Public ComponentTeam As IndustryTeam
    Public InventionTeam As IndustryTeam
    Public CopyTeam As IndustryTeam

    Public ManufacturingTeamUsage As Double
    Public ComponentTeamUsage As Double
    Public CopyTeamUsage As Double
    Public InventionTeamUsage As Double

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim CopyofMe As New ManufacturingItem

        CopyofMe.RecordID = RecordID
        CopyofMe.BPID = BPID
        CopyofMe.ItemGroup = ItemGroup
        CopyofMe.ItemGroupID = ItemGroupID
        CopyofMe.ItemCategory = ItemCategory
        CopyofMe.ItemCategoryID = ItemCategoryID
        CopyofMe.ItemTypeID = ItemTypeID
        CopyofMe.ItemName = ItemName
        CopyofMe.TechLevel = TechLevel
        CopyofMe.Owned = Owned
        CopyofMe.BPME = BPME
        CopyofMe.BPTE = BPTE
        CopyofMe.Inputs = Inputs
        CopyofMe.Profit = Profit
        CopyofMe.ProfitPercent = ProfitPercent
        CopyofMe.IPH = IPH
        CopyofMe.TotalCost = TotalCost
        CopyofMe.CalcType = CalcType
        CopyofMe.BlueprintType = BlueprintType

        CopyofMe.Runs = Runs
        CopyofMe.SingleInventedBPCRunsperBPC = SingleInventedBPCRunsperBPC
        CopyofMe.ProductionLines = ProductionLines
        CopyofMe.LaboratoryLines = LaboratoryLines

        CopyofMe.CopyTime = CopyTime
        CopyofMe.InventionTime = InventionTime

        CopyofMe.Inputs = Inputs
        CopyofMe.Decryptor = Decryptor
        CopyofMe.Relic = Relic

        CopyofMe.CanBuildBP = CanBuildBP
        CopyofMe.CanInvent = CanInvent
        CopyofMe.CanRE = CanRE

        CopyofMe.SVR = SVR
        CopyofMe.SVRxIPH = SVRxIPH
        CopyofMe.CopyCost = CopyCost
        CopyofMe.InventionCost = InventionCost
        CopyofMe.ManufacturingFacilityUsage = ManufacturingFacilityUsage

        CopyofMe.ManufacturingTeam = ManufacturingTeam
        CopyofMe.ComponentTeam = ComponentTeam
        CopyofMe.InventionTeam = InventionTeam
        CopyofMe.CopyTeam = CopyTeam

        CopyofMe.ManufacturingFacility = ManufacturingFacility
        CopyofMe.ComponentManufacturingFacility = ComponentManufacturingFacility
        CopyofMe.CapComponentManufacturingFacility = CapComponentManufacturingFacility
        CopyofMe.InventionFacility = InventionFacility
        CopyofMe.CopyFacility = CopyFacility

        CopyofMe.BPProductionTime = BPProductionTime
        CopyofMe.TotalProductionTime = TotalProductionTime

        CopyofMe.ItemMarketPrice = ItemMarketPrice

        CopyofMe.BrokerFees = BrokerFees
        CopyofMe.Taxes = Taxes

        CopyofMe.BaseJobCost = BaseJobCost

        CopyofMe.NumBPs = NumBPs
        CopyofMe.InventionChance = InventionChance
        CopyofMe.BlueprintType = BlueprintType
        CopyofMe.Race = Race
        CopyofMe.VolumeperItem = VolumeperItem
        CopyofMe.TotalVolume = TotalVolume

        CopyofMe.JobFee = JobFee
        CopyofMe.TeamFee = TeamFee

        CopyofMe.ManufacturingFacilityUsage = ManufacturingFacilityUsage
        CopyofMe.ComponentManufacturingFacilityUsage = ComponentManufacturingFacilityUsage
        CopyofMe.CopyFacilityUsage = CopyFacilityUsage
        CopyofMe.InventionFacilityUsage = InventionFacilityUsage

        CopyofMe.ManufacturingTeamUsage = ManufacturingTeamUsage
        CopyofMe.ComponentTeamUsage = ComponentTeamUsage
        CopyofMe.CopyTeamUsage = CopyTeamUsage
        CopyofMe.InventionTeamUsage = InventionTeamUsage

        Return CopyofMe

    End Function

End Class

' For sorting a ManufacturingList by IPH
Public Class CalcIPHComparer

    Implements System.Collections.Generic.IComparer(Of ManufacturingItem)

    Public Function Compare(ByVal p1 As ManufacturingItem, ByVal p2 As ManufacturingItem) As Integer Implements IComparer(Of ManufacturingItem).Compare
        ' swap p2 and p1 to do decending sort
        Return p2.IPH.CompareTo(p1.IPH)
    End Function

End Class

' For sorting a ManufacturingList by IPH
Public Class CalcProfitComparer

    Implements System.Collections.Generic.IComparer(Of ManufacturingItem)

    Public Function Compare(ByVal p1 As ManufacturingItem, ByVal p2 As ManufacturingItem) As Integer Implements IComparer(Of ManufacturingItem).Compare
        ' swap p2 and p1 to do decending sort
        Return p2.Profit.CompareTo(p1.Profit)
    End Function

End Class

' For sorting a ManufacturingList by Profit Percent
Public Class CalcProfitPComparer

    Implements System.Collections.Generic.IComparer(Of ManufacturingItem)

    Public Function Compare(ByVal p1 As ManufacturingItem, ByVal p2 As ManufacturingItem) As Integer Implements IComparer(Of ManufacturingItem).Compare
        ' swap p2 and p1 to do decending sort
        Return p2.ProfitPercent.CompareTo(p1.ProfitPercent)
    End Function

End Class

' For sorting a ManufacturingList by SVR
Public Class CalcSVRComparer

    Implements System.Collections.Generic.IComparer(Of ManufacturingItem)

    Public Function Compare(ByVal p1 As ManufacturingItem, ByVal p2 As ManufacturingItem) As Integer Implements IComparer(Of ManufacturingItem).Compare
        Dim SVR1 As Double
        Dim SVR2 As Double

        ' swap p2 and p1 to do decending sort
        If IsNothing(p1.SVR) Then
            SVR1 = 0
        Else
            SVR1 = CDbl(p1.SVR)
        End If

        If IsNothing(p2.SVR) Then
            SVR2 = 0
        Else
            SVR2 = CDbl(p2.SVR)
        End If

        ' swap p2 and p1 to do decending sort
        Return SVR2.CompareTo(SVR1)

    End Function

End Class

' For sorting a ManufacturingList by SVR * IPH
Public Class CalcSVRIPHComparer

    Implements System.Collections.Generic.IComparer(Of ManufacturingItem)

    Public Function Compare(ByVal p1 As ManufacturingItem, ByVal p2 As ManufacturingItem) As Integer Implements IComparer(Of ManufacturingItem).Compare
        Dim SVRIPH1 As Double
        Dim SVRIPH2 As Double

        ' swap p2 and p1 to do decending sort
        If IsNothing(p1.SVRxIPH) Then
            SVRIPH1 = 0
        Else
            SVRIPH1 = CDbl(p1.SVRxIPH)
        End If

        If IsNothing(p1.SVR) Then
            SVRIPH2 = 0
        Else
            SVRIPH2 = CDbl(p2.SVRxIPH)
        End If

        Return SVRIPH2.CompareTo(SVRIPH1)

    End Function

End Class