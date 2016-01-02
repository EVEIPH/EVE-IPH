Imports System.Data.SQLite

' Loads a Character API Data into the database
Public Class frmLoadCharacterAPI
    Inherits System.Windows.Forms.Form

    Private m_ControlsCollection As ControlsCollection
    Private CharacterCheckBoxes() As CheckBox

    ' List of characters per api account imported
    Private Characters() As Character
    Private LoadingChars As Boolean = True

    Public PreDefinedID As String
    Public PreDefinedKey As String

    Private EnteredAPI As String
    Private EnteredKeyID As Long
    Private KeyType As String = ""

#Region "Initialization Code"

    Public Sub New()
        MyBase.New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'create the controlscollection class
        m_ControlsCollection = New ControlsCollection(Me)
        CharacterCheckBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "chkCharacter"), CheckBox())

    End Sub

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

#End Region

    Private Sub EnterAPI()
        Dim CharacterAPI As New EVEAPI
        Dim i As Integer
        Dim fAccessError As New frmAPIError
        Dim TempErrorText As String = ""
        Dim OnlyCorpKey As Boolean = False

        ' Data checking
        If Trim(txtUserID.Text) = "" Then
            MsgBox("Must Enter an Account ID", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Not IsNumeric(Trim(txtUserID.Text)) Then
            MsgBox("Must Enter a valid Account ID Number", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Trim(txtAPIKey.Text) = "" Then
            MsgBox("Must Enter an Account API", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Check the data and then try to connect to EVE API to select the data
        ' Show the character selection screen or the error screen
        Call ShowNext()
        Me.Cursor = Cursors.WaitCursor

        ' Get the Character List
        EnteredAPI = Trim(txtAPIKey.Text)
        EnteredKeyID = CLng(txtUserID.Text)

        Characters = CharacterAPI.GetAccountCharacters(EnteredKeyID, EnteredAPI, KeyType)
        TempErrorText = CharacterAPI.GetErrorText

        ' Ignore industry job errors here and show later if they choose to use the invention tracker
        If TempErrorText = NoIndyJobsLoaded Then
            TempErrorText = ""
        End If

        ' Do an error check on API
        If Not NoAPIError(TempErrorText, KeyType) Then
            ' There was an error
            CharacterCheckBoxes(1).Visible = False
            CharacterCheckBoxes(2).Visible = False
            CharacterCheckBoxes(3).Visible = False
            lblErrorText.Text = "There was an error in the Key data." & Environment.NewLine & Environment.NewLine & "Error Text: " & CharacterAPI.GetErrorText
            gbSelectChars.Visible = False
            btnImportAPI.Enabled = False
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ' If this is a corp key and they don't have any keys loaded, then force them to load a dummy
        If KeyType = CorporationAPITypeName Then
            ' See if they have a character key loaded
            Dim CMDCount As New SQLiteCommand("SELECT COUNT(*) FROM API WHERE API_TYPE IN ('Account','Character') AND CHARACTER_ID <> 0", EVEDB.DBREf)
            If CInt(CMDCount.ExecuteScalar()) = 0 Then
                ' No characters, make them load dummy
                Call AskLoadDummy(True)
                ' Load all the data but later on, don't show the import button
                OnlyCorpKey = True
            End If
        End If

        ' Loading characters
        LoadingChars = True

        ' Show the data
        gbSelectChars.Visible = True
        lblErrorText.Visible = False

        ' Don't show the Character checks yet
        CharacterCheckBoxes(1).Visible = False
        CharacterCheckBoxes(2).Visible = False
        CharacterCheckBoxes(3).Visible = False

        lblKeyType.Visible = True
        lblKeyType.Text = "Key Type: " & KeyType & ", Access Mask: " & Characters(0).AccessMask

        ' Loop through the nodes for three characters 
        For i = 0 To Characters.Count - 1
            ' Add names to the checks
            If Characters(i).Name <> "" Then
                If KeyType <> CorporationAPITypeName Then
                    gbSelectChars.Text = "Select Characters:"
                    CharacterCheckBoxes(i + 1).Visible = True
                    lblCorporationName.Visible = False
                    CharacterCheckBoxes(i + 1).Text = Characters(i).Name
                Else ' Load corporation name
                    ' Set check box invisible and show the label. Autocheck the check though even though not visible
                    gbSelectChars.Text = "Select Corporation:"
                    CharacterCheckBoxes(i + 1).Visible = False
                    lblCorporationName.Visible = True
                    CharacterCheckBoxes(i + 1).Checked = True
                    lblCorporationName.Text = Characters(i).CharacterCorporation.CorporationName

                End If
            End If
        Next

        Me.Cursor = Cursors.Default
        LoadingChars = False ' Done loading

        If KeyType = CorporationAPITypeName Then
            If Not OnlyCorpKey Then
                btnImportAPI.Enabled = True
            Else
                btnImportAPI.Enabled = False
            End If
        Else
            btnImportAPI.Enabled = False
        End If

        btnImportAPI.Visible = True

        Exit Sub

    End Sub

    Private Sub AskLoadDummy(ByVal NoCorp As Boolean)
        Dim response As Integer

        If NoCorp Then
            response = MsgBox("This is a corp key and you must load a character API Key first to use IPH. Do you want to continue without loading a character?", vbYesNo, Me.Text)
        Else
            response = MsgBox("If you do not load a character many features will not be available to you. Do you want to continue without loading a character?", vbYesNo, Me.Text)
        End If

        If response = vbYes Then
            Call SelectedCharacter.LoadDummyCharacter()
            Call MsgBox("Dummy Character Loaded", MsgBoxStyle.Information, Application.ProductName)
            Me.Close()
        Else
            Exit Sub
        End If
    End Sub

    ' Uploads the Characters selected to database including skills, and whatever else we decide later
    Private Sub btnImportAPI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAPI.Click
        Dim readerDefault As SQLiteDataReader
        Dim i As Integer
        Dim SQL As String
        Dim DefaultCharID As Long = 0

        Me.Cursor = Cursors.WaitCursor

        If KeyType <> CorporationAPITypeName Then
            ' See if any of the character IDs in this set are defaults
            For i = 0 To Characters.Count - 1
                ' See if it is already set as a default
                SQL = "SELECT 'X' FROM API WHERE CHARACTER_ID=" & Characters(i).ID & " AND IS_DEFAULT <> 0 AND API_TYPE NOT IN ('Corporation', 'Old Key')"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerDefault = DBCommand.ExecuteReader

                ' Mark the default
                If readerDefault.Read() Then
                    ' Save the character id and exit, since there is only one default
                    DefaultCharID = Characters(i).ID
                    readerDefault.Close()
                    Exit For
                End If

                readerDefault.Close()
            Next

            ' Delete all records for this API and reload
            SQL = "DELETE FROM API WHERE KEY_ID = " & EnteredKeyID & " AND API_KEY = '" & EnteredAPI & "'"
            evedb.ExecuteNonQuerySQL(SQL)

            ' Find each character in the list and load data to DB
            For i = 0 To Characters.Count - 1
                If Characters(i).Name = CharacterCheckBoxes(i + 1).Text And CharacterCheckBoxes(i + 1).Checked Then
                    If Characters(i).ID = DefaultCharID Then
                        ' Set default
                        Characters(i).IsDefault = True
                    End If

                    ' Save the data for each character to DB
                    Call SaveAccountAPIData(Characters(i), KeyType)

                End If
            Next

            readerDefault = Nothing
            DBCommand = Nothing

        Else ' Corporation key - just one to save

            ' See if the corp already exists (could have a different key for each corp - only allow one key for a corp)
            Call SaveAccountAPIData(Characters(0), KeyType)

        End If

        ' Reload the data from API since new key information entered may change the data of the account
        Call SelectedCharacter.LoadDefaultCharacter(True, UserApplicationSettings.LoadAssetsonStartup, UserApplicationSettings.LoadBPsonStartup)

        ' All good to go
        Me.Cursor = Cursors.Default
        If KeyType <> CorporationAPITypeName Then
            MsgBox("Selected Characters Loaded", vbInformation)
        Else
            MsgBox("Corporation Loaded", vbInformation)
        End If

        CharactersLoaded = True
        APIAdded = True

        Me.Close()

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Call EnterAPI()
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        Call ShowPrevious()
    End Sub

    Private Sub ShowNext()
        lblAPINote.Visible = False
        lblUserID.Visible = False
        lblAPIKey.Visible = False
        txtAPIKey.Visible = False
        txtUserID.Visible = False
        btnPrevious.Enabled = True
        btnNext.Visible = False
        lblAPIAddress.Visible = False
        lblKeyType.Visible = False
        linklabelPredefined.Visible = False
    End Sub

    Private Sub ShowPrevious()
        lblAPINote.Visible = True
        lblUserID.Visible = True
        lblAPIKey.Visible = True
        txtAPIKey.Visible = True
        txtUserID.Visible = True
        btnPrevious.Enabled = False
        lblKeyType.Text = "Enter your Customizable API"
        lblKeyType.Visible = True
        btnNext.Visible = True
        gbSelectChars.Visible = False
        btnImportAPI.Visible = False
        lblAPIAddress.Visible = True
        linklabelPredefined.Visible = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        CharactersLoaded = False

        Dim CMDCount As New SQLiteCommand("SELECT COUNT(*) FROM API WHERE API_TYPE IN ('Account','Character') AND CHARACTER_ID <> 0", EVEDB.DBREf)

        If CInt(CMDCount.ExecuteScalar()) <> 0 Then
            Me.Close()
        Else ' This is the inital form or no APIs loaded, ask them if they want to continue
            AskLoadDummy(False)
        End If

    End Sub

    Private Sub lblAPIAddress_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblAPIAddress.LinkClicked
        System.Diagnostics.Process.Start("https://community.eveonline.com/support/api-key/")
    End Sub

    Private Sub chkCharacter1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCharacter1.CheckedChanged
        If Not LoadingChars Then
            If Not chkCharacter1.Checked And Not chkCharacter2.Checked And Not chkCharacter3.Checked Then
                btnImportAPI.Enabled = False
            Else
                btnImportAPI.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkCharacter2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCharacter2.CheckedChanged
        If Not LoadingChars Then
            If Not chkCharacter1.Checked And Not chkCharacter2.Checked And Not chkCharacter3.Checked Then
                btnImportAPI.Enabled = False
            Else
                btnImportAPI.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkCharacter3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCharacter3.CheckedChanged
        If Not LoadingChars Then
            If Not chkCharacter1.Checked And Not chkCharacter2.Checked And Not chkCharacter3.Checked Then
                btnImportAPI.Enabled = False
            Else
                btnImportAPI.Enabled = True
            End If
        End If
    End Sub

    Private Sub frmLoadCharacterAPI_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If PreDefinedID <> "" Then
            txtUserID.Text = PreDefinedID
        End If

        If PreDefinedKey <> "" Then
            txtAPIKey.Text = PreDefinedKey
        End If

        Me.Activate()
    End Sub

    Private Sub txtUserID_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtUserID.KeyDown
        'Call ProcessCutCopyPasteSelect(txtUserID, e)
        If e.KeyCode = Keys.Enter Then
            Call EnterAPI()
        End If
    End Sub

    Private Sub txtAPIKey_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAPIKey.KeyDown
        'Call ProcessCutCopyPasteSelect(txtAPIKey, e)
        If e.KeyCode = Keys.Enter Then
            Call EnterAPI()
        End If
    End Sub

    Private Sub linklabelPredefined_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linklabelPredefined.LinkClicked
        System.Diagnostics.Process.Start("http://community.eveonline.com/support/api-key/CreatePredefined?accessMask=589962/")
    End Sub

End Class