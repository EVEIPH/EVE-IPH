' The manufacturing item to load the grid
Public Class ManufacturingItem
    Implements ICloneable

    Public Property RecordID As Long ' Unique record id

    Public Property BPID As Long
    Public Property ItemGroup As String
    Public Property ItemGroupID As Integer
    Public Property ItemCategory As String
    Public Property ItemCategoryID As Integer
    Public Property ItemTypeID As Long
    Public Property ItemName As String
    Public Property TechLevel As String
    Public Property Owned As String
    Public Property BPME As Double
    Public Property BPTE As Double
    Public Property Inputs As String
    Public Property Profit As Double
    Public Property ProfitPercent As Double
    Public Property IPH As Double
    Public Property TotalCost As Double
    Public Property CalcType As String ' Type of calculation to get the profit - either Components, Raw Mats or Build/Buy
    Public Property BlueprintType As BPType

    Public Property Runs As Integer
    Public Property SingleInventedBPCRunsperBPC As Integer
    Public Property ProductionLines As Integer
    Public Property LaboratoryLines As Integer

    ' Inputs
    Public Property Decryptor As Decryptor
    Public Property Relic As String

    ' Can do variables
    Public Property CanBuildBP As Boolean
    Public Property CanInvent As Boolean
    Public Property CanRE As Boolean

    Public Property SVR As Object ' Sales volume ratio, set to an object because this can be Nothing
    Public Property SVRxIPH As Object ' could be nothing

    Public Property ManufacturingFacility As IndustryFacility
    Public Property ManufacturingFacilityUsage As Double
    Public Property ComponentManufacturingFacility As IndustryFacility
    Public Property ComponentManufacturingFacilityUsage As Double
    Public Property CapComponentManufacturingFacility As IndustryFacility
    Public Property CapComponentManufacturingFacilityUsage As Double

    Public Property CopyCost As Double
    Public Property CopyFacilityUsage As Double
    Public Property CopyFacility As IndustryFacility

    Public Property InventionCost As Double
    Public Property InventionFacilityUsage As Double
    Public Property InventionFacility As IndustryFacility

    Public Property BPProductionTime As String
    Public Property TotalProductionTime As String
    Public Property CopyTime As String
    Public Property InventionTime As String

    Public Property ItemMarketPrice As Double

    Public Property BrokerFees As Double
    Public Property Taxes As Double

    Public Property BaseJobCost As Double
    Public Property NumBPs As Integer
    Public Property InventionChance As Double
    Public Property Race As String
    Public Property VolumeperItem As Double
    Public Property TotalVolume As Double

    Public Property JobFee As Double
    Public Property TeamFee As Double

    Public Property ManufacturingTeam As IndustryTeam
    Public Property ComponentTeam As IndustryTeam
    Public Property InventionTeam As IndustryTeam
    Public Property CopyTeam As IndustryTeam

    Public Property ManufacturingTeamUsage As Double
    Public Property ComponentTeamUsage As Double
    Public Property CopyTeamUsage As Double
    Public Property InventionTeamUsage As Double

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