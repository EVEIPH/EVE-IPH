Imports System.Data.SQLite

Public Class Material
    Implements ICloneable

    Private TypeID As Long
    Private TypeName As String
    Private GroupName As String
    Private Quantity As Long
    Private Volume As Double

    ' Look up cost per item when not set
    Private CostPerItem As Double

    ' If this material is a buildable item, store the ME for the grid
    Private ItemME As String
    Private ItemTE As String
    Private BuildItem As Boolean ' Whether we should build the item or not

    ' Calculate
    Private TotalMatVolume As Double
    Private TotalCost As Double

    Private ItemType As Integer ' My item type value

    Public Sub New(ByVal SentTypeID As Long, ByVal SentTypeName As String, ByVal SentGroupName As String, ByVal SentQuantity As Long, _
                   ByVal SentVolume As Double, ByVal SentPrice As Double, ByVal SentItemME As String, ByVal SentItemTE As String, _
                   Optional ByVal SentBuild As Boolean = False, Optional ByVal SentItemType As Integer = 0)
        TypeID = SentTypeID
        TypeName = SentTypeName
        Quantity = SentQuantity
        Volume = SentVolume
        GroupName = SentGroupName
        BuildItem = SentBuild
        ItemType = SentItemType

        If Trim(SentItemME) <> "" Then
            ItemME = SentItemME
        Else
            ItemME = "-"
        End If

        If Trim(SentItemTE) <> "" Then
            ItemTE = SentItemTE
        Else
            ItemTE = "-"
        End If

        If SentPrice = 0 Then
            CostPerItem = GetItemPrice(TypeID)
        Else
            CostPerItem = SentPrice
        End If

        ' Set the cost and volume
        Call SetTotalCostVolume()

    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe As New Material(Me.TypeID, Me.TypeName, Me.GroupName, Me.Quantity, Me.Volume, Me.CostPerItem, Me.ItemME, Me.ItemTE, Me.BuildItem, Me.ItemType)
        Return CopyOfMe
    End Function

    Private Sub SetTotalCostVolume()
        TotalCost = CostPerItem * Quantity
        TotalMatVolume = Volume * Quantity
    End Sub

    ' Adds quantity to the material and upates the total cost and volume
    Public Sub AddQuantity(ByVal SentQuantity As Long)
        Quantity = Quantity + SentQuantity
        ' New quantity means new total price and volume
        Call SetTotalCostVolume()
    End Sub

    ' Sets the quantity of the material and sets the total cost and volume
    Public Sub SetQuantity(ByVal SentQuantity As Long)
        Quantity = SentQuantity
        ' New quantity means new total price and volume
        Call SetTotalCostVolume()
    End Sub

    ' Sets the items ME
    Public Sub SetItemME(ByVal SentME As String)
        ItemME = SentME
    End Sub

    ' Sets the items TE
    Public Sub SetItemTE(ByVal SentTE As String)
        ItemTE = SentTE
    End Sub

    ' Sets the item as built
    Public Sub SetBuildItem(ByVal BuildValue As Boolean)
        BuildItem = BuildValue
    End Sub

    ' Sets the Total Cost of the material to the sent cost only if it's built
    Public Sub SetBuildCost(ByVal BuildCost As Double)
        If BuildItem Then
            CostPerItem = BuildCost
            Call SetTotalCostVolume()
        End If
    End Sub

    ' Allow setting total cost
    Public Sub SetTotalCost(ByVal SentTotalCost As Double)
        TotalCost = SentTotalCost
    End Sub

    Public Function GetItemType() As Integer
        Return ItemType
    End Function

    Public Function GetMaterialTypeID() As Long
        Return TypeID
    End Function

    Public Function GetMaterialName() As String
        Return TypeName
    End Function

    Public Function GetQuantity() As Long
        Return Quantity
    End Function

    Public Function GetMaterialGroup() As String
        Return GroupName
    End Function

    Public Function GetVolume() As Double
        Return Volume
    End Function

    Public Function GetCostPerItem() As Double
        Return CostPerItem
    End Function

    Public Function GetTotalVolume() As Double
        Return TotalMatVolume
    End Function

    Public Function GetTotalCost() As Double
        Return TotalCost
    End Function

    Public Function GetItemME() As String
        Return ItemME
    End Function

    Public Function GetItemTE() As String
        Return ItemTE
    End Function

    Public Function GetBuildItem() As Boolean
        Return BuildItem
    End Function

End Class
