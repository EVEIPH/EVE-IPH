
Public Class BuildBuyItems
    Private BBItems As List(Of BBItem)

    Private BBItemIDtoFind As Integer
    Private BBItemtoFind As New BuildBuyItem

    Public Structure BBItem
        Dim BPID As Integer
        Dim BBItems As List(Of BuildBuyItem)
    End Structure

    Public Sub New()
        BBItems = New List(Of BBItem)
    End Sub
End Class
