

Public Class frmLoadESIAuthorization

    ' Launches the application registration website
    Private Sub btnRegisterApplication_Click(sender As Object, e As EventArgs) Handles btnRegisterApplication.Click
        Process.Start("https://developers.eveonline.com/applications")
    End Sub

    ' Launches the instruction page 
    Private Sub btnLaunchInstructions_Click(sender As Object, e As EventArgs) Handles btnLaunchInstructions.Click

    End Sub

    Private Sub btnSkipEntry_Click(sender As Object, e As EventArgs) Handles btnSkipEntry.Click
        ' Allow dummy variable use

    End Sub

    ' Save the data to a local text file for use by the program
    Private Sub btnSaveApplicationInfo_Click(sender As Object, e As EventArgs) Handles btnSaveApplicationInfo.Click
        Dim Settings As AppRegistrationInformationSettings

        ' Error checking
        If Trim(txtClientID.Text) = "" Then
            Call MsgBox("You must enter a Client ID", vbInformation, Application.ProductName)
            txtClientID.Focus()
            Exit Sub
        End If

        If Trim(txtSecretKey.Text) = "" Then
            Call MsgBox("You must enter a Secret Key", vbInformation, Application.ProductName)
            txtSecretKey.Focus()
            Exit Sub
        End If

        If Trim(txtPort.Text) = "" Then
            Call MsgBox("You must enter a Port Number", vbInformation, Application.ProductName)
            txtPort.Focus()
            Exit Sub
        End If

        If Trim(txtScopes.Text) = "" Then
            Call MsgBox("You must enter a set of Scopes", vbInformation, Application.ProductName)
            txtScopes.Focus()
            Exit Sub
        End If

        ' Scopes need to be space, comma, or CR separated
        If Not Trim(txtScopes.Text).Contains(" ") Or Not Trim(txtScopes.Text).Contains(vbCrLf) Or Not Trim(txtScopes.Text).Contains(",") Then
            Call MsgBox("Scopes must be separated by a space, comma, or Carriage Return", vbInformation, Application.ProductName)
            txtScopes.Focus()
            Exit Sub
        End If

        ' Now make sure they have the scopes we want


        ' Good to go, save it
        Settings.ClientID = Trim(txtClientID.Text)
        Settings.SecretKey = Trim(txtSecretKey.Text)
        Settings.Port = CInt(Trim(txtPort.Text))
        Settings.Scopes = Trim(txtScopes.Text)

    End Sub

    Private Sub txtClientID_GotFocus(sender As Object, e As EventArgs) Handles txtClientID.GotFocus
        txtClientID.SelectAll()
    End Sub

    Private Sub txtPort_GotFocus(sender As Object, e As EventArgs) Handles txtPort.GotFocus
        txtPort.SelectAll()
    End Sub

    Private Sub txtScopes_GotFocus(sender As Object, e As EventArgs) Handles txtScopes.GotFocus
        txtScopes.SelectAll()
    End Sub

    Private Sub txtSecretKey_GotFocus(sender As Object, e As EventArgs) Handles txtSecretKey.GotFocus
        txtSecretKey.SelectAll()
    End Sub
End Class