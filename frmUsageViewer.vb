
Public Class frmUsageViewer

    Public UsageSplits As List(Of UsageSplit)

    Public Sub New()

        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

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

        UsageSplits = New List(Of UsageSplit)

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

        lstCosts.Clear()
        lstCosts.BeginUpdate()

        ' Just load the mats into the list
        lstCosts.Columns.Add("Usage Type", 175, HorizontalAlignment.Left)
        lstCosts.Columns.Add("Usage Cost", 125, HorizontalAlignment.Right)

        If Not IsNothing(UsageSplits) Then
            For i = 0 To UsageSplits.Count - 1
                Application.DoEvents()
                MatList = lstCosts.Items.Add(UsageSplits(i).UsageName)
                MatList.SubItems.Add(FormatNumber(UsageSplits(i).UsageValue, 2))
                TotalCost += UsageSplits(i).UsageValue
            Next
        End If

        ' Finally add the usage cost
        MatList = lstCosts.Items.Add("Total Usage")
        ' Color this last line grey
        MatList.BackColor = Color.LightGray
        MatList.SubItems.Add(FormatNumber(TotalCost, 2))

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
        ClipboardText = "Usage Name" & Separator & "Usage Cost" & vbCrLf

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

Public Structure UsageSplit
    Dim UsageName As String
    Dim UsageValue As Double
End Structure