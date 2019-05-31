Public Class frmInventionMats

    Public MaterialList As Materials
    Public TotalInventedRuns As Integer
    Public UserRuns As Long
    Public MatType As String
    Public ListType As String

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

    End Sub

    Private Sub frmInventionREMats_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmInventionREMats_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim MatList As ListViewItem
        Dim Quantity As Long
        Dim MatCost As Double
        Dim TotalCost As Double ' For summing up all the costs

        Application.UseWaitCursor = True
        Application.DoEvents()

        Me.Text = MatType

        ' Sort by quantity
        Call MaterialList.SortMaterialListByQuantity()

        ' Just load the mats into the list
        lstMats.Columns.Add("Material", 253, HorizontalAlignment.Left)
        lstMats.Columns.Add("Cost per Item", 90, HorizontalAlignment.Right)
        lstMats.Columns.Add("Total Cost", 100, HorizontalAlignment.Right)
        lstMats.Columns.Add("Quantity", 75, HorizontalAlignment.Right)

        If Not IsNothing(MaterialList.GetMaterialList) Then
            For i = 0 To MaterialList.GetMaterialList.Count - 1
                Application.DoEvents()
                MatList = New ListViewItem(MaterialList.GetMaterialList(i).GetMaterialName)
                MatCost = CDbl(MaterialList.GetMaterialList(i).GetTotalCost)
                Quantity = CLng(MaterialList.GetMaterialList(i).GetQuantity)
                MatList.SubItems.Add(FormatNumber(MatCost / Quantity, 2))
                MatList.SubItems.Add(FormatNumber(MatCost, 2))
                MatList.SubItems.Add(FormatNumber(Quantity, 0))
                TotalCost += MatCost
                Call lstMats.Items.Add(MatList)
            Next
        End If

        ' Add the total cost
        MatList = New ListViewItem("Total " & ListType & " Cost")
        ' Color this last line grey
        MatList.BackColor = Color.WhiteSmoke
        MatList.SubItems.Add("")
        MatList.SubItems.Add(FormatNumber(TotalCost, 2)) ' Put in the Total Cost column
        MatList.SubItems.Add(FormatNumber(TotalInventedRuns, 0))

        If ListType.Contains("Invention") Then
            ' Finally add the cost per bp
            MatList = New ListViewItem("Total Invention Cost for " & CStr(UserRuns) & " Runs")
            ' Color this last line grey
            MatList.BackColor = Color.LightGray
            MatList.SubItems.Add("")
            MatList.SubItems.Add(FormatNumber(TotalCost / TotalInventedRuns * UserRuns, 2))
            MatList.SubItems.Add(FormatNumber(UserRuns, 0))
        End If

        Call lstMats.Items.Add(MatList)

        Application.UseWaitCursor = False

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Dispose()
    End Sub

    Private Sub btnCopyMats_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyMats.Click
        Dim TempExportType As String

        If rbtnExportCSV.Checked Then
            TempExportType = CSVDataExport
        ElseIf rbtnExportSSV.Checked Then
            TempExportType = SSVDataExport
        ElseIf rbtnExportSimple.checked Then
            TempExportType = MultiBuyDataExport
        Else
            TempExportType = DefaultTextDataExport
        End If

        ' Paste to clipboard
        Call CopyTextToClipboard(MaterialList.GetClipboardList(TempExportType, True, False, False, UserApplicationSettings.IncludeInGameLinksinCopyText))
    End Sub

End Class