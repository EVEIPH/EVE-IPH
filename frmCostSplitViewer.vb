
Public Class frmCostSplitViewer

    Public CostSplits As List(Of CostSplit)
    Public CostSplitType As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If rbtnExportCSV.Text = UserApplicationSettings.DataExportFormat Then
            rbtnExportCSV.Checked = True
        ElseIf rbtnExportSSV.Text = UserApplicationSettings.DataExportFormat Then
            rbtnExportSSV.Checked = True
        ElseIf rbtnExportDefault.Text = UserApplicationSettings.DataExportFormat Then
            rbtnExportDefault.Checked = True
        End If

        CostSplits = New List(Of CostSplit)

    End Sub

    Private Sub frmCostSplitViewer_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmCostSplitViewer_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim MatList As ListViewItem
        Dim TotalCost As Double ' For summing up all the costs

        Application.UseWaitCursor = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Me.Text = CostSplitType
        lstCosts.Clear()
        lstCosts.BeginUpdate()

        ' Just load the mats into the list
        lstCosts.Columns.Add("Cost Split", 200, HorizontalAlignment.Left)
        lstCosts.Columns.Add("Total Cost", 100, HorizontalAlignment.Right)

        If Not IsNothing(CostSplits) Then
            For i = 0 To CostSplits.Count - 1
                Application.DoEvents()
                MatList = New ListViewItem(CostSplits(i).SplitName)
                MatList.SubItems.Add(FormatNumber(CostSplits(i).SplitValue, 2))
                TotalCost += CostSplits(i).SplitValue
                Call lstCosts.Items.Add(MatList)
            Next
        End If

        ' Finally add the total cost
        MatList = New ListViewItem("Total Cost")
        ' Color this last line grey
        MatList.BackColor = Color.LightGray
        MatList.SubItems.Add(FormatNumber(TotalCost, 2))
        Call lstCosts.Items.Add(MatList)

        lstCosts.EndUpdate()
        Application.UseWaitCursor = False
        Me.Cursor = Cursors.Default
        Application.DoEvents()

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Dispose()
    End Sub

    Private Sub btnCopyMats_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyMats.Click
        Dim Separator As String
        Dim ClipboardText As String = ""
        Dim Items As ListView.ListViewItemCollection
        Dim ListItem As ListViewItem

        If rbtnExportCSV.Checked Then
            Separator = ","
        ElseIf rbtnExportSSV.Checked Then
            Separator = ";"
        Else
            Separator = " "
        End If

        ' Add the top row
        ClipboardText = "Cost Split" & Separator & "Total Cost" & vbCrLf

        Items = lstCosts.Items

        For Each ListItem In Items
            ClipboardText = ClipboardText & ListItem.SubItems(0).Text & Separator
            If rbtnExportCSV.Checked Then
                ' No commas in number
                ClipboardText = ClipboardText & Format(ListItem.SubItems(1).Text, "Fixed") & vbCrLf
            ElseIf rbtnExportSSV.Checked Then
                ' Switch commas and periods
                ClipboardText = ClipboardText & ConvertUStoEUDecimal(ListItem.SubItems(1).Text) & vbCrLf
            Else
                ClipboardText = ClipboardText & ListItem.SubItems(1).Text & vbCrLf
            End If
        Next

        ' Paste to clipboard
        Call CopyTextToClipboard(ClipboardText)

    End Sub

End Class

Public Structure CostSplit
    Dim SplitName As String
    Dim SplitValue As Double
End Structure