Public Class frmCopyandPaste

    Private KeyHandled As Boolean
    Private SentWindowType As CopyPasteWindowType

    Public Sub New(WindowType As CopyPasteWindowType)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SentWindowType = WindowType

    End Sub

    Private Sub txtPaste_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPaste.KeyDown
        If e.KeyCode = Keys.A AndAlso e.Control = True Then ' Select All
            txtPaste.SelectAll()
            KeyHandled = True
        Else
            KeyHandled = False
        End If

    End Sub

    Private Sub txtPaste_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPaste.KeyPress
        ' Special handling - if select all is pressed for some reason the notification sound happens
        e.Handled = KeyHandled
    End Sub

    Private Sub btnImport_Click(sender As System.Object, e As System.EventArgs) Handles btnImport.Click
        If SentWindowType = CopyPasteWindowType.Materials Then
            frmShop.CopyPasteMaterialText = txtPaste.Text
        ElseIf SentWindowType = CopyPasteWindowType.Blueprints Then
            ' TODO
        End If

        ' Close the form
        Me.Dispose()
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtPaste_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPaste.TextChanged
        btnImport.Enabled = True
    End Sub
End Class