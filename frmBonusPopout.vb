
Public Class frmBonusPopout
    Private ColumnClicked As Integer
    Private ColumnSortType As SortOrder

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Set up all the sizes
        Me.Height = StructureBonusPopoutViewerSettings.FormHeight
        Me.Width = StructureBonusPopoutViewerSettings.FormWidth

        lstUpwellStructureBonuses.Columns(0).Width = StructureBonusPopoutViewerSettings.BonusAppliesColumnWidth
        lstUpwellStructureBonuses.Columns(1).Width = StructureBonusPopoutViewerSettings.ActivityColumnWidth
        lstUpwellStructureBonuses.Columns(2).Width = StructureBonusPopoutViewerSettings.BonusesColumnWidth
        lstUpwellStructureBonuses.Columns(3).Width = StructureBonusPopoutViewerSettings.BonusSourceColumnWidth

    End Sub

    Private Sub frmBonusPopout_Layout(sender As Object, e As LayoutEventArgs) Handles Me.Layout

        ' Resize the grid
        lstUpwellStructureBonuses.Height = Me.Height - 94
        lstUpwellStructureBonuses.Width = Me.Width - 38

        ' Move the buttons
        btnSaveSettings.Left = CInt(Me.Width / 2) - btnSaveSettings.Width - 10 ' middle of form minus half spacing and button width
        btnSaveSettings.Top = Me.Height - 73

        btnClose.Left = CInt(Me.Width / 2) + 10 ' middle of form plus half spacing
        btnClose.Top = Me.Height - 73

        Application.DoEvents()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
        Me.Dispose()
    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Dim TempSettings As StructureBonusPopoutSettings = Nothing

        ' Save the height, width, and column sizes for later
        Try
            With TempSettings
                .FormHeight = Me.Height
                .FormWidth = Me.Width

                .BonusAppliesColumnWidth = lstUpwellStructureBonuses.Columns(0).Width
                .ActivityColumnWidth = lstUpwellStructureBonuses.Columns(1).Width
                .BonusesColumnWidth = lstUpwellStructureBonuses.Columns(2).Width
                .BonusSourceColumnWidth = lstUpwellStructureBonuses.Columns(3).Width

            End With

            AllSettings.SaveStructureBonusPopoutViewerSettings(TempSettings)
            StructureBonusPopoutViewerSettings = TempSettings

            Call MsgBox("Settings Saved", vbInformation, Application.ProductName)

        Catch ex As Exception
            Call MsgBox("Settings failed to save: " & ex.Message, vbExclamation, Application.ProductName)
        End Try

        Application.DoEvents()

    End Sub

    Private Sub lstUpwellStructureBonuses_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstUpwellStructureBonuses.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstUpwellStructureBonuses, ListView), ColumnClicked, ColumnSortType)
    End Sub
End Class