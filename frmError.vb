Public Class frmError

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        End
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmError_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtError.Text = frmErrorText
        Me.Activate()
    End Sub
End Class