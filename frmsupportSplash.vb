Public NotInheritable Class frmsupportSplash

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        End
    End Sub

    Private Sub pbPaypal_Click(sender As Object, e As EventArgs) Handles pbPaypal.Click
        ' Take them to the donation page
        Call Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=HSZKRQYTX5HR6&source=url")
    End Sub
End Class
