Imports System.Data.SQLite

Public Class frmFacilityTest
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Temp connection - use main connection when we switch over
        EVEDB = New DBConnection(SQLiteDBFileName)

        ' Add any initialization after the InitializeComponent() call.
        Call BPFacility.InitializeControl(FacilityView.FullControls, ProductionType.Manufacturing)

    End Sub
End Class