
Public Class ListViewItemComparer
    Implements IComparer, IComparer(Of ListViewItem)

    Private ColumnIndex As Integer
    Private OrderOfSort As SortOrder

    Public Sub New(SentColumnIndex As Integer, SortType As SortOrder)
        ColumnIndex = SentColumnIndex
        OrderOfSort = SortType
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Return CompareCore(DirectCast(x, ListViewItem), DirectCast(y, ListViewItem))
    End Function

    Private Function CompareCore(x As ListViewItem, y As ListViewItem) As Integer Implements IComparer(Of ListViewItem).Compare
        Dim result As Integer

        Try
            ' Get the values
            Dim StrX As String = x.SubItems(ColumnIndex).Text
            Dim StrY As String = y.SubItems(ColumnIndex).Text

            If x.ListView.Columns(ColumnIndex).Text.Contains("Time") And y.ListView.Columns(ColumnIndex).Text.Contains("Time") Then
                ' This is a special case to sort by converting the values back to seconds then sending on
                StrX = CStr(ConvertDHMSTimetoSeconds(StrX))
                StrY = CStr(ConvertDHMSTimetoSeconds(StrY))
            End If

            ' Strip out any percentages
            StrX = Trim(StrX.Replace("%", ""))
            StrY = Trim(StrY.Replace("%", ""))

            ' Sort numbers
            If IsNumeric(StrX) And IsNumeric(StrY) Then

                result = CDbl(StrX).CompareTo(CDbl(StrY))

            ElseIf IsDate(StrX) And IsDate(StrY) Then
                ' Compare the dates
                result = CDate(StrX).CompareTo(DateTime.Parse(StrY))

            ElseIf IsStringdate(StrX) And IsStringdate(StrY) Then
                ' This is a date sorted as a string like 2d 14:23:22
                result = CDbl(FormatStringdate(StrX)).CompareTo(FormatStringdate(StrY))
            Else ' Strings or any other object
                ' Compare the two items.
                result = StrX.CompareTo(StrY)
            End If

            ' Calculate the correct return value based on the object 
            ' comparison.
            If (OrderOfSort = SortOrder.Ascending) Then
                Return result
            ElseIf (OrderOfSort = SortOrder.Descending) Then
                ' Descending sort is selected, return negative result of 
                ' compare operation.
                Return (-result)
            Else
                ' Return '0' to indicate that they are equal.
                Return 0
            End If

            Return result

        Catch ex As Exception
            Return 0 ' Something happened so just assume equal for now
        End Try

    End Function

End Class
