Public Class frmMaterialListViewer

    Private ItemListColumnClicked As Integer
    Private ItemListColumnSortOrder As SortOrder

    Public Sub New(MaterialList As Materials, IncludeTaxes As Boolean, BrokerFeeData As BrokerFeeInfo)

        ' This call is required by the designer.
        InitializeComponent()

        ItemListColumnClicked = 0
        ItemListColumnSortOrder = SortOrder.None

        ' Add any initialization after the InitializeComponent() call.
        Dim matLine As ListViewItem
        Dim TotalCost As Double = 0

        Call MaterialList.SortMaterialListByQuantity()

        For Each Mat In MaterialList.GetMaterialList
            ' Add all the mats to the grid
            matLine = New ListViewItem(Mat.GetMaterialName)
            matLine.SubItems.Add(FormatNumber(Mat.GetQuantity, 0))
            matLine.SubItems.Add(FormatNumber(Mat.GetTotalCost, 2))
            Call lstMaterials.Items.Add(matLine)
            TotalCost += Mat.GetTotalCost
        Next

        ' Add the tax, fees, and final cost cost
        If TotalCost > 0 Then
            Dim Taxes As Double = 0
            Dim BrokerFees As Double = 0

            matLine = New ListViewItem("Taxes")
            matLine.SubItems.Add("")
            If IncludeTaxes Then
                Taxes = GetSalesTax(TotalCost)
                matLine.SubItems.Add("-" & FormatNumber(Taxes, 2))
            Else
                matLine.SubItems.Add("0.00")
            End If
            Call lstMaterials.Items.Add(matLine)

            matLine = New ListViewItem("Broker Fees")
            matLine.SubItems.Add("")
            If BrokerFeeData.IncludeFee <> BrokerFeeType.NoFee Then
                BrokerFees = GetSalesBrokerFee(TotalCost, BrokerFeeData)
                matLine.SubItems.Add("-" & FormatNumber(BrokerFees, 2))
            Else
                matLine.SubItems.Add("0.00")
            End If
            Call lstMaterials.Items.Add(matLine)

            matLine = New ListViewItem("Total Sold Value")
            matLine.SubItems.Add("")
            matLine.SubItems.Add(FormatNumber(TotalCost - Taxes - BrokerFees, 2))
            Call lstMaterials.Items.Add(matLine)
        End If

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub lstMaterials_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstMaterials.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstMaterials, ListView), ItemListColumnClicked, ItemListColumnSortOrder)
    End Sub
End Class