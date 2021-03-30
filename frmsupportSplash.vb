Public NotInheritable Class frmsupportSplash

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        End
    End Sub

    Private Sub pbPaypal_Click(sender As Object, e As EventArgs) Handles pbPaypal.Click
        ' Take them to the donation page
        Call Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=HSZKRQYTX5HR6&source=url")
    End Sub

    Private Sub pbPaetron_Click(sender As Object, e As EventArgs) Handles pbPaetron.Click
        ' Take them to the donation page
        Call Process.Start("https://www.patreon.com/user?u=51064427&fan_landing=true")
    End Sub

    Private Sub pbPaetron_MouseEnter(sender As Object, e As EventArgs) Handles pbPaetron.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbPaetron_MouseLeave(sender As Object, e As EventArgs) Handles pbPaetron.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pbPaypal_MouseEnter(sender As Object, e As EventArgs) Handles pbPaypal.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pbPaypal_MouseLeave(sender As Object, e As EventArgs) Handles pbPaypal.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub
End Class
