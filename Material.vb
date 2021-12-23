
Public Class Material
    Implements ICloneable

    Private TypeID As Long
    Private TypeName As String
    Private _GroupName As String
    Private Quantity As Long
    Private DQuantity As Double
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

    Public Sub New(ByVal SentTypeID As Long, ByVal SentTypeName As String, ByVal SentGroupName As String, ByVal SentQuantity As Long,
                   ByVal SentVolume As Double, ByVal SentPrice As Double, ByVal SentItemME As String, ByVal SentItemTE As String,
                   Optional ByVal isBuiltItem As Boolean = False, Optional ByVal SentItemType As Integer = 0)
        TypeID = SentTypeID
        TypeName = SentTypeName
        Quantity = SentQuantity
        DQuantity = -1
        Volume = SentVolume
        _GroupName = SentGroupName
        BuildItem = isBuiltItem
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

        Call SetTotalCostVolume()

    End Sub

    Public Sub New(ByVal SentTypeID As Long, ByVal SentTypeName As String, ByVal SentGroupName As String, ByVal SentQuantity As Double,
                   ByVal SentVolume As Double, ByVal SentPrice As Double, ByVal SentItemME As String, ByVal SentItemTE As String,
                   Optional ByVal isBuiltItem As Boolean = False, Optional ByVal SentItemType As Integer = 0)
        TypeID = SentTypeID
        TypeName = SentTypeName
        Quantity = -1
        DQuantity = SentQuantity
        Volume = SentVolume
        _GroupName = SentGroupName
        BuildItem = isBuiltItem
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

        Call SetTotalCostVolume()
    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyofMe As Material
        If Me.DQuantity = -1 Then
            CopyofMe = New Material(Me.TypeID, Me.TypeName, Me.GroupName, Me.Quantity, Me.Volume, Me.CostPerItem, Me.ItemME, Me.ItemTE, Me.BuildItem, Me.ItemType)
        Else
            CopyofMe = New Material(Me.TypeID, Me.TypeName, Me.GroupName, Me.DQuantity, Me.Volume, Me.CostPerItem, Me.ItemME, Me.ItemTE, Me.BuildItem, Me.ItemType)
        End If

        Return CopyOfMe
    End Function

    Private Sub SetTotalCostVolume()
        If DQuantity = -1 Then
            TotalCost = CostPerItem * Quantity
            TotalMatVolume = Volume * Quantity
        Else
            TotalCost = CostPerItem * DQuantity
            TotalMatVolume = Volume * DQuantity
        End If
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

    ' Adds quantity to the material and upates the total cost and volume
    Public Sub AddDQuantity(ByVal SentQuantity As Double)
        DQuantity = DQuantity + SentQuantity
        ' New quantity means new total price and volume
        Call SetTotalCostVolume()
    End Sub

    ' Sets the quantity of the material and sets the total cost and volume
    Public Sub SetDQuantity(ByVal SentQuantity As Double)
        DQuantity = SentQuantity
        ' New quantity means new total price and volume
        Call SetTotalCostVolume()
    End Sub

    ' Sets the Total Cost of the material to the sent cost only if it's built
    Public Sub SetBuildCostPerItem(ByVal BuildCost As Double)
        If BuildItem Then
            CostPerItem = BuildCost
            Call SetTotalCostVolume()
        End If
    End Sub

    ' Sets the name with the sent name
    Public Sub SetName(ByVal SentName As String)
        TypeName = SentName
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

    Public Function GetDQuantity() As Double
        Return DQuantity
    End Function

    'Public Function GetMaterialGroup() As String
    '    Return GroupName
    'End Function

    Public Property GroupName() As String
        Get
            Return _GroupName
        End Get
        Set(value As String)
            _GroupName = value
        End Set
    End Property

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
