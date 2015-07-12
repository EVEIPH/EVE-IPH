Public Class frmAPIError


    Public ErrorText As String
    Public ErrorLink As String


    Public Sub New()

        Me.AutoScaleMode = AutoScaleSetting

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Hide()
    End Sub

    Private Sub llMain_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llMain.LinkClicked
        System.Diagnostics.Process.Start(ErrorLink)
    End Sub

    Private Sub frmAPIError_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblMain.Text = ErrorText
        llMain.Text = ErrorLink
    End Sub
End Class