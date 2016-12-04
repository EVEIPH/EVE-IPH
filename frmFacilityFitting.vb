

Public Class frmFacilityFitting

    Dim SelectedStructure As EngineeringComplex

    Public Sub New(ByVal EC As EngineeringComplex)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SelectedStructure = EC

    End Sub
End Class
