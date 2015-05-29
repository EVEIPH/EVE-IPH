
Imports System.Collections
Imports System.Windows.Forms

Public Class ListViewColumnSorter
    Implements System.Collections.IComparer

    Private ColumnToSort As Integer
    Private OrderOfSort As SortOrder
    Private ObjectCompare As CaseInsensitiveComparer

    Public Sub New()
        ' Initialize the column to '0'.
        ColumnToSort = 0

        ' Initialize the sort order to 'none'.
        OrderOfSort = SortOrder.None

        ' Initialize the CaseInsensitiveComparer object.
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim compareResult As Integer
        Dim listviewX As ListViewItem
        Dim listviewY As ListViewItem

        ' Cast the objects to be compared to ListViewItem objects.
        listviewX = CType(x, ListViewItem)
        listviewY = CType(y, ListViewItem)

        Dim StrX As String = listviewX.SubItems(ColumnToSort).Text
        Dim StrY As String = listviewY.SubItems(ColumnToSort).Text

        If listviewX.ListView.Columns(ColumnToSort).Text = "Time to Complete" And listviewY.ListView.Columns(ColumnToSort).Text = "Time to Complete" Then
            ' This is a special case to sort by converting the values back to seconds then sending on
            StrX = CStr(ConvertDHMSTimetoSeconds(StrX))
            StrY = CStr(ConvertDHMSTimetoSeconds(StrY))
        End If

        ' Sort numbers
        If IsNumeric(StrX) And IsNumeric(StrY) Then

            compareResult = CDbl(StrX).CompareTo(CDbl(StrY))

        ElseIf IsDate(StrX) And IsDate(StrY) Then
            ' Compare the dates
            compareResult = DateTime.Parse(StrX).CompareTo(DateTime.Parse(StrY))

        ElseIf IsStringdate(StrX) And IsStringdate(StrY) Then

            compareResult = CDbl(FormatStringdate(StrX)).CompareTo(FormatStringdate(StrY))

        Else ' Strings or any other object
            ' Compare the two items.
            compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)
        End If

        ' Calculate the correct return value based on the object 
        ' comparison.
        If (OrderOfSort = SortOrder.Ascending) Then
            Return compareResult
        ElseIf (OrderOfSort = SortOrder.Descending) Then
            ' Descending sort is selected, return negative result of 
            ' compare operation.
            Return (-compareResult)
        Else
            ' Return '0' to indicate that they are equal.
            Return 0
        End If
    End Function

    Public Property SortColumn() As Integer
        Set(ByVal Value As Integer)
            ColumnToSort = Value
        End Set

        Get
            Return ColumnToSort
        End Get
    End Property

    Public Property Order() As SortOrder
        Set(ByVal Value As SortOrder)
            OrderOfSort = Value
        End Set

        Get
            Return OrderOfSort
        End Get
    End Property

End Class
