
Imports System.ComponentModel

Public Class frmLoadESIAuthorization

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' See if there is a settings file saved already and load it if so
        Dim AuthSettings As AppRegistrationInformationSettings

        AuthSettings = AllSettings.LoadAppRegistrationInformationSettings

        With AuthSettings
            ' Dummy is -1 client id, error would be 0
            If .ClientID <> "0" Or .ClientID <> "-1" Then
                ' Load up all the values
                txtClientID.Text = .ClientID
                txtSecretKey.Text = .SecretKey
                ' txtPort.Text = CStr(.Port)
                txtScopes.Text = .Scopes
            End If
        End With

    End Sub

    ' Launches the application registration website
    Private Sub btnRegisterApplication_Click(sender As Object, e As EventArgs) Handles btnRegisterApplication.Click
        Process.Start("https://developers.eveonline.com/applications")
    End Sub

    ' Launches the instruction page 
    Private Sub btnLaunchInstructions_Click(sender As Object, e As EventArgs) Handles btnLaunchInstructions.Click
        Process.Start("http://eveiph.github.io/ESIAuthorizationInstructions.html")
    End Sub

    Private Sub btnSkipEntry_Click(sender As Object, e As EventArgs) Handles btnSkipEntry.Click
        Call ProcessDummyCharacter()
    End Sub

    Private Sub ProcessDummyCharacter()
        ' Load the dummy
        If SelectedCharacter.LoadDummyCharacter(False) = TriState.UseDefault Then
            ' They said no, cancel and let them re-choose
            Exit Sub
        ElseIf SelectedCharacter.LoadDummyCharacter(False) = TriState.True Then
            Call MsgBox("Dummy Character Loaded", MsgBoxStyle.OkOnly, Application.ProductName)
            Me.Hide()
        Else
            Call MsgBox("Unable to save Dummy Character", vbInformation, Application.ProductName)
            Exit Sub
        End If
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

        If Trim(txtScopes.Text) = "" Then
            Call MsgBox("You must enter a set of Scopes", vbInformation, Application.ProductName)
            txtScopes.Focus()
            Exit Sub
        End If

        ' Now make sure they have the scopes we want - for now, only skills is required
        If Not txtScopes.Text.Contains(ESICharacterSkillsScope) Then
            Call MsgBox("Scopes must contain esi-skills.read_skills for EVE IPH.", vbInformation, Application.ProductName)
            txtScopes.Focus()
            Exit Sub
        End If

        ' Scopes need to be space separated - so only do that check, we can format below
        If Not Trim(txtScopes.Text).Contains(" ") And Trim(txtScopes.Text) <> ESICharacterSkillsScope Then
            ' It's not just one line of entry for character skills, so it should be separated property so we can read it later
            Call MsgBox("Scopes must be separated by a space.", vbInformation, Application.ProductName)
            txtScopes.Focus()
            Exit Sub
        End If

        ' Good to go, save it then do a check
        Settings.ClientID = Trim(txtClientID.Text)
        Settings.SecretKey = Trim(txtSecretKey.Text)
        ' Process the scopes and only leave one space between each
        Settings.Scopes = String.Join(" ", txtScopes.Text.Split(New String() {" ", ",", "%20", "%2520", vbCr, vbLf, vbCrLf}, StringSplitOptions.RemoveEmptyEntries))

        If AllSettings.SaveAppRegistrationInformationSettings(Settings) Then
            ' Data is saved but they need to re-register characters to add updates
            Call MsgBox("Registration Information Saved!", vbInformation, Application.ProductName)
        Else
            Call MsgBox("Settings failed to save.", vbInformation, Application.ProductName)
            Exit Sub
        End If

        Me.Hide()

    End Sub

    Private Sub txtClientID_GotFocus(sender As Object, e As EventArgs) Handles txtClientID.GotFocus
        txtClientID.SelectAll()
    End Sub

    Private Sub txtScopes_GotFocus(sender As Object, e As EventArgs)
        txtScopes.SelectAll()
    End Sub

    Private Sub txtSecretKey_GotFocus(sender As Object, e As EventArgs) Handles txtSecretKey.GotFocus
        txtSecretKey.SelectAll()
    End Sub

    Private Sub frmLoadESIAuthorization_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

End Class