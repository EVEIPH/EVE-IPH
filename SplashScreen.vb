
Public NotInheritable Class SplashScreen

    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Application title
        ApplicationTitle.Text = "EVE" & Environment.NewLine & "Isk per Hour"
        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        Copyright.Text = ""

    End Sub

    ' For updating the progress
    Public Sub SetProgress(ByVal progress As String)
        Me.lblUpdate.Text = progress
        Application.DoEvents()
    End Sub

End Class
