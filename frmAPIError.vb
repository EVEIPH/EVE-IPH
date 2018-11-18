Public Class frmAPIError


    Public ErrorText As String
    Public ErrorLink As String


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Hide()
    End Sub

    Private Sub frmAPIError_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblMain.Text = ErrorText
    End Sub
End Class