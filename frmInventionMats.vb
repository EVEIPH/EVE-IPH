Public Class frmInventionMats

    Public MaterialList As Materials
    Public TotalInventedRuns As Integer
    Public UserRuns As Long
    Public MatType As String
    Public ListType As String
    Private FirstLoad As Boolean

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

        ' Just load the mats into the list
        lstInventionMats.Columns.Add("Material ID", 0, HorizontalAlignment.Left) ' Hidden
        lstInventionMats.Columns.Add("Material", 253, HorizontalAlignment.Left)
        lstInventionMats.Columns.Add("Cost per Item", 90, HorizontalAlignment.Right)
        lstInventionMats.Columns.Add("Total Cost", 100, HorizontalAlignment.Right)
        lstInventionMats.Columns.Add("Quantity", 75, HorizontalAlignment.Right)

        FirstLoad = True

    End Sub

    Private Sub frmInventionREMats_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmInventionREMats_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Call RefreshInventionMatsGrid()
    End Sub

    Public Sub RefreshInventionMatsGrid()
        Dim MatList As ListViewItem
        Dim Quantity As Long
        Dim MatCost As Double
        Dim TotalCost As Double ' For summing up all the costs

        If Not IsNothing(MaterialList.GetMaterialList) Then
            If MaterialList.GetMaterialList.Count > 0 Then
                Application.UseWaitCursor = True
                Application.DoEvents()

                Call lstInventionMats.BeginUpdate()
                Call lstInventionMats.Items.Clear()

                Me.Text = MatType

                If FirstLoad Then
                    ' Sort by quantity
                    Call MaterialList.SortMaterialListByQuantity()
                End If

                For i = 0 To MaterialList.GetMaterialList.Count - 1
                    Application.DoEvents()
                    MatList = New ListViewItem(CStr(MaterialList.GetMaterialList(i).GetMaterialTypeID))
                    MatCost = GetItemPrice(MaterialList.GetMaterialList(i).GetMaterialTypeID)
                    Quantity = CLng(MaterialList.GetMaterialList(i).GetQuantity)
                    MatList.SubItems.Add(MaterialList.GetMaterialList(i).GetMaterialName)
                    MatList.SubItems.Add(FormatNumber(MatCost, 2))
                    MatList.SubItems.Add(FormatNumber(MatCost * Quantity, 2))
                    MatList.SubItems.Add(FormatNumber(Quantity, 0))
                    TotalCost += MatCost
                    Call lstInventionMats.Items.Add(MatList)
                Next

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

                Call lstInventionMats.Items.Add(MatList)
                Call lstInventionMats.EndUpdate()

                Application.UseWaitCursor = False
                FirstLoad = False
            End If
        End If
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