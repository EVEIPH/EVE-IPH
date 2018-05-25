Imports System.Data.SQLite

Public Class frmFacilityTest
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Temp connection - use main connection when we switch over
        EVEDB = New DBConnection(SQLiteDBFileName)

        UserUpwellStructureSettings = AllSettings.LoadUpwellStructureViewerSettings
        StructureBonusPopoutViewerSettings = AllSettings.LoadStructureBonusPopoutViewerSettings
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub
End Class