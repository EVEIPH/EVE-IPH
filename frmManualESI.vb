
Public Class frmManualESI
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Load the URL into the text box for them to use.
        Dim TempESI As New ESI
        txtURL.Text = TempESI.GetURL

        btnLoadCharacter.Enabled = False

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

    Private Sub btnSelectDefault_Click(sender As Object, e As EventArgs) Handles btnLoadCharacter.Click
        ' Save the code they entered, and try to load it manually
        Dim ESIConnection As New ESI
        Dim AuthToken As String = ""

        ' Process what they entered
        If Not txtReturnLink.Text.Contains("code=") Then
            Call MsgBox("No Authorization Code present. Your return URL should be something like http://127.0.0.1:12500/?code=YOUR_CODE", vbInformation, Application.ProductName)
            Exit Sub
        Else
            ' parse
            AuthToken = ESIConnection.GetAuthToken(txtReturnLink.Text)
        End If

        ' Set the new character data first. This will load the data in the table or update it if they choose a character already loaded
        If ESIConnection.SetCharacterData(Nothing, AuthToken) Then
            ' Refresh the token data to get new scopes if they added
            Me.Cursor = Cursors.WaitCursor
            If SelectedCharacter.ID <> DummyCharacterID Then
                ' If it's an update
                Call SelectedCharacter.RefreshTokenData()
            End If
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        Else
            ' Didn't load, so show the re-enter info button
            If AppRegistered() Then
                If Not CancelESISSOLogin Then
                    MsgBox("The Character failed to load. Please check application registration information.")
                End If
            Else
                MsgBox("You have not registered IPH. Please register IPH through the ESI developers system and try again.")
            End If
        End If

    End Sub

    Private Sub txtReturnLink_TextChanged(sender As Object, e As EventArgs) Handles txtReturnLink.TextChanged
        btnLoadCharacter.Enabled = True
    End Sub

End Class