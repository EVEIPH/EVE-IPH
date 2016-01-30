
Public Class ManufacturingListView : Inherits System.Windows.Forms.ListView

    Public Event ProcMsg(ByVal m As Message)
    Public Const WM_VSCROLL As Integer = 277

    Public RowFormats As List(Of RowFormat)

    Public ListIDtoFind As Integer

    Public Sub New()

        Call InitializeComponent()

        ' Double buffer cuts down on screen flicker
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        RowFormats = New List(Of RowFormat)

    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_VSCROLL
                RaiseEvent ProcMsg(m)
        End Select
        MyBase.WndProc(m)
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnDrawColumnHeader(e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs)
        MyBase.OnDrawColumnHeader(e)
        e.DrawDefault = True
    End Sub

    Protected Overrides Sub OnResize(e As System.EventArgs)
        MyBase.OnResize(e)
    End Sub

    ' Sends in the formats to set each row
    Public Sub SetRowFormats(ByVal SentFormats As List(Of RowFormat))
        RowFormats = SentFormats
    End Sub

    ' Predicate for finding an item in a list EVE Market Data of items
    Private Function FindRowFormat(ByVal Item As RowFormat) As Boolean
        If Item.ListID = ListIDtoFind Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Overrides Sub OnDrawSubItem(e As System.Windows.Forms.DrawListViewSubItemEventArgs)
        MyBase.OnDrawSubItem(e)
        ' Draw everything
        e.DrawDefault = False

        Dim CellRectangle As New RectangleF
        Dim ExtraWidth As Integer = 0

        Dim Format As New StringFormat

        Dim RowBackgroundColor As Brush
        Dim RowTextColor As Brush

        ' Get the background info and set
        ListIDtoFind = CInt(e.Item.Text)
        Dim CurrentRowFormat As RowFormat = RowFormats.Find(AddressOf FindRowFormat)

        If IsNothing(CurrentRowFormat.BackColor) Then
            ' Set to default
            CurrentRowFormat.BackColor = Brushes.White
        End If

        If IsNothing(CurrentRowFormat.ForeColor) Then
            ' Set to default
            CurrentRowFormat.ForeColor = Brushes.Black
        End If

        ' Highlight the row if selected
        If e.Item.Selected Then
            RowBackgroundColor = Brushes.DodgerBlue
            RowTextColor = Brushes.White
        Else
            ' Use the set background color
            RowBackgroundColor = CurrentRowFormat.BackColor
            ' Set the text color
            RowTextColor = CurrentRowFormat.ForeColor
        End If

        ' Set the allignment from the headers, which are drawn already and aligned
        Select Case e.Header.TextAlign
            Case HorizontalAlignment.Center
                Format.Alignment = StringAlignment.Center
            Case (HorizontalAlignment.Right)
                Format.Alignment = StringAlignment.Far ' Right
            Case Else
                Format.Alignment = StringAlignment.Near ' Left
        End Select

        ' Paint the background first before images
        Call e.Graphics.FillRectangle(RowBackgroundColor, e.Bounds)

        ' Figure out if this is the price trend 
        If e.Header.Text = ProgramSettings.PriceTrendColumnName Then
            Dim imageindex As Integer = 0

            Dim TrendValue As Double = Val(e.SubItem.Text.Replace("%", ""))

            If TrendValue < 0 Then
                imageindex = 8 ' down
            ElseIf TrendValue > 0 Then '
                imageindex = 7 ' up
            Else ' Don't show for 0%
                imageindex = 6 ' Blank box
            End If

            ' Draw the image
            e.Graphics.DrawImage(e.Item.ImageList.Images(imageindex), e.SubItem.Bounds.Location)
            ' Save the width
            ExtraWidth = e.Item.ImageList.Images(imageindex).Width

        ElseIf e.Header.Text = ProgramSettings.TechColumnName Then
            Dim imageindex As Integer = 0

            Dim TechValue As String = e.SubItem.Text

            If TechValue = "T2" Then
                imageindex = 2
            ElseIf TechValue = "T3" Then
                imageindex = 3
            ElseIf TechValue = "Storyline" Then
                imageindex = 4
            ElseIf TechValue = "Pirate" Or TechValue = "Navy" Then
                imageindex = 5
            Else ' Don't show for T1 but use blank for formatting
                imageindex = 6
            End If

            ' Draw the image
            e.Graphics.DrawImage(e.Item.ImageList.Images(imageindex), e.SubItem.Bounds.Location)
            ExtraWidth = e.Item.ImageList.Images(imageindex).Width
        Else
            ExtraWidth = 0
        End If

        If Format.Alignment = StringAlignment.Far Then
            CellRectangle.X = e.SubItem.Bounds.Location.X - ExtraWidth
            CellRectangle.Width = e.SubItem.Bounds.Width + ExtraWidth
        Else
            CellRectangle.X = e.SubItem.Bounds.Location.X + ExtraWidth
            CellRectangle.Width = e.SubItem.Bounds.Width - ExtraWidth
        End If

        CellRectangle.Y = e.SubItem.Bounds.Location.Y
        CellRectangle.Height = e.SubItem.Bounds.Height - 2
        Format.LineAlignment = StringAlignment.Center

        ' Draw the column data
        e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, RowTextColor, CellRectangle, Format)

    End Sub

End Class

Public Structure RowFormat
    Dim ListID As Integer

    Dim BackColor As Brush
    Dim ForeColor As Brush

End Structure