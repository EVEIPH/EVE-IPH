
Imports System.Data.SQLite

Public Class frmSettings

    Private SSLoaded As Boolean
    Private RegionLoaded As Boolean
    Private FirstLoad As Boolean
    Private SelectedReset As Boolean

    Private Defaults As New ProgramSettings ' For default constants

#Region "Click object Functions"

    Private Sub chkRefineStationTax_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRefineStationTax.CheckedChanged
        If chkRefineStationTax.Checked = True Then
            cmbRefineTax.Enabled = True
            cmbRefineTax.Focus()
        Else
            cmbRefineTax.Enabled = False
            cmbRefineTax.Text = FormatPercent(Defaults.DefaultRefineTax, 1)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkBeanCounterManufacturing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBeanCounterManufacturing.CheckedChanged
        If Not FirstLoad Then
            If chkBeanCounterManufacturing.Checked Then
                cmbBeanCounterManufacturing.Enabled = True
                cmbBeanCounterManufacturing.Text = "Zainou 'Beancounter' Industry BX-802"
            Else
                cmbBeanCounterManufacturing.Enabled = False
                cmbBeanCounterManufacturing.Text = ""
            End If
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkBeanCounterRefining_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBeanCounterRefining.CheckedChanged
        If Not FirstLoad Then
            If chkBeanCounterRefining.Checked Then
                cmbBeanCounterRefining.Enabled = True
                cmbBeanCounterRefining.Text = "Zainou 'Beancounter' Reprocessing RX-802"
            Else
                cmbBeanCounterRefining.Enabled = False
                cmbBeanCounterRefining.Text = ""
            End If
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkBeanCounterCopy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBeanCounterCopy.CheckedChanged
        If Not FirstLoad Then
            If chkBeanCounterCopy.Checked Then
                cmbBeanCounterCopy.Enabled = True
                cmbBeanCounterCopy.Text = "Zainou 'Beancounter' Science SC-803"
            Else
                cmbBeanCounterCopy.Enabled = False
                cmbBeanCounterCopy.Text = ""
            End If
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkBrokerCorpStanding_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBrokerCorpStanding.CheckedChanged
        If chkBrokerCorpStanding.Checked = True Then
            txtBrokerCorpStanding.Enabled = True
            txtBrokerCorpStanding.Focus()
        Else
            txtBrokerCorpStanding.Enabled = False
            txtBrokerCorpStanding.Text = FormatNumber(Defaults.DefaultBrokerCorpStanding, 2)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkBrokerFactionStanding_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBrokerFactionStanding.CheckedChanged
        If chkBrokerFactionStanding.Checked = True Then
            txtBrokerFactionStanding.Enabled = True
            txtBrokerFactionStanding.Focus()
        Else
            txtBrokerFactionStanding.Enabled = False
            txtBrokerFactionStanding.Text = FormatNumber(Defaults.DefaultBrokerFactionStanding, 2)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkRefineCorpStanding_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRefineCorpStanding.CheckedChanged
        If chkRefineCorpStanding.Checked = True Then
            txtRefineCorpStanding.Enabled = True
            txtRefineCorpStanding.Focus()
        Else
            txtRefineCorpStanding.Enabled = False
            txtRefineCorpStanding.Text = FormatNumber(Defaults.DefaultRefineCorpStanding, 2)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub txtEVECentralInterval_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtEVECentralInterval.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            ' Only integer values
            If allowedRunschars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtBrokerFactionStandings_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrokerFactionStanding.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedDecimalChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtBrokerCorpStandings_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrokerCorpStanding.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedDecimalChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRefineCorpStanding_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtRefineCorpStanding.KeyPress
        ' Only allow numbers or backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedDecimalChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub chkShowToolTips_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowToolTips.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub chkCheckUpdatesStartup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckUpdatesStartup.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub chkBuildBuyDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBuildBuyDefault.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub chkDefaultME_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDefaultME.CheckedChanged
        If chkDefaultME.Checked = True Then
            txtDefaultME.Enabled = True
            txtDefaultME.Focus()
        Else
            txtDefaultME.Enabled = False
            txtDefaultME.Text = FormatNumber(Defaults.DefaultSettingME, 0)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkEVECentralInterval_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEVECentralInterval.CheckedChanged
        If chkEVECentralInterval.Checked = True Then
            txtEVECentralInterval.Enabled = True
            txtEVECentralInterval.Focus()
        Else
            txtEVECentralInterval.Enabled = False
            txtEVECentralInterval.Text = FormatNumber(Defaults.DefaultEVECentralRefreshInterval, 0)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkDefaultPE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDefaultTE.CheckedChanged
        If chkDefaultTE.Checked = True Then
            txtDefaultTE.Enabled = True
            txtDefaultTE.Focus()
        Else
            txtDefaultTE.Enabled = False
            txtDefaultTE.Text = FormatNumber(Defaults.DefaultSettingTE, 0)
        End If
        btnSave.Text = "Save"
    End Sub

    Private Sub chkDisableSVR_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDisableSVR.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub cmbRefineTax_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbRefineTax.KeyPress
        ' Only allow numbers, period or percent and backspace
        If e.KeyChar <> ControlChars.Back Then
            If allowedPercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SSLoaded = False
        RegionLoaded = False
        btnSave.Text = "Save"
        FirstLoad = True
        SelectedReset = False

        If UserApplicationSettings.ShowToolTips Then
            ToolTip1.SetToolTip(chkDisableSVR, "If you have issues with SVR updates on the Manufacturing Tab (ie website down, etc), you can disable those queries here.")
            ToolTip1.SetToolTip(rbtnExportCSV, "Exports data in Common Separated Values with periods for decimals")
            ToolTip1.SetToolTip(rbtnExportSSV, "Exports data in SemiColon Separated Values with commas for decimals")
            ToolTip1.SetToolTip(rbtnExportDefault, "Exports data in basic space or dashes to separate data for easy readability")
            ToolTip1.SetToolTip(chkSaveBPRelicsDecryptors, "When selected, Saving Settings on the BP tab will also save the Decryptor and Relic Types if selected and autoload them for each relevant BP.")
        End If

    End Sub

    Private Sub frmSettings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' Load the settings for the program from DB
        frmMain.ReloadSettings = False
        Call LoadFormSettings()
    End Sub

    Private Sub LoadFormSettings()

        With UserApplicationSettings
            ' General Settings
            chkCheckUpdatesStartup.Checked = .CheckforUpdatesonStart

            If rbtnExportCSV.Text = .DataExportFormat Then
                rbtnExportCSV.Checked = True
            ElseIf rbtnExportSSV.Text = .DataExportFormat Then
                rbtnExportSSV.Checked = True
            ElseIf rbtnExportDefault.Text = .DataExportFormat Then
                rbtnExportDefault.Checked = True
            End If

            chkShowToolTips.Checked = .ShowToolTips
            chkRefreshAssetsonStartup.Checked = .LoadAssetsonStartup
            chkRefreshBPsonStartup.Checked = .LoadBPsonStartup
            chkDisableSound.Checked = .DisableSound

            ' CREST
            chkRefreshFacilityDataonStartup.Checked = .LoadCRESTFacilityDataonStartup
            chkRefreshMarketDataonStartup.Checked = .LoadCRESTMarketDataonStartup
            chkRefreshTeamDataonStartup.Checked = .LoadCRESTTeamDataonStartup

            If .BrokerCorpStanding = Defaults.DefaultBrokerCorpStanding Then
                ' Default
                chkBrokerCorpStanding.Checked = False
                txtBrokerCorpStanding.Enabled = False
                txtBrokerCorpStanding.Text = FormatNumber(Defaults.DefaultBrokerCorpStanding, 2)
            Else
                ' User
                chkBrokerCorpStanding.Checked = True
                txtBrokerCorpStanding.Enabled = True
                txtBrokerCorpStanding.Text = FormatNumber(.BrokerCorpStanding, 2)
            End If

            If .BrokerFactionStanding = Defaults.DefaultBrokerFactionStanding Then
                ' Default
                chkBrokerFactionStanding.Checked = False
                txtBrokerFactionStanding.Enabled = False
                txtBrokerFactionStanding.Text = FormatNumber(Defaults.DefaultBrokerFactionStanding, 2)
            Else
                ' User
                chkBrokerFactionStanding.Checked = True
                txtBrokerFactionStanding.Enabled = True
                txtBrokerFactionStanding.Text = FormatNumber(.BrokerFactionStanding, 2)
            End If

            If .RefineCorpStanding = Defaults.DefaultRefineCorpStanding Then
                ' Default
                chkRefineCorpStanding.Checked = False
                txtRefineCorpStanding.Enabled = False
                txtRefineCorpStanding.Text = FormatNumber(Defaults.DefaultRefineCorpStanding, 2)
            Else
                ' User
                chkRefineCorpStanding.Checked = True
                txtRefineCorpStanding.Enabled = True
                txtRefineCorpStanding.Text = FormatNumber(.RefineCorpStanding, 2)
            End If

            ' Refine Tax
            If .RefiningTax = Defaults.DefaultRefineTax Then
                ' Default
                chkRefineStationTax.Checked = False
                cmbRefineTax.Enabled = False
                cmbRefineTax.Text = FormatPercent(Defaults.DefaultRefineTax, 1)
            Else
                ' User
                chkRefineStationTax.Checked = True
                cmbRefineTax.Enabled = True
                cmbRefineTax.Text = FormatPercent(.RefiningTax, 1)
            End If

            ' Implants
            If .ManufacturingImplantValue > 0 Then
                chkBeanCounterManufacturing.Checked = True
                Select Case .ManufacturingImplantValue
                    Case (-1 * GetAttribute("manufacturingTimeBonus", Defaults.MBeanCounterName & "1") / 100)
                        cmbBeanCounterManufacturing.Text = Defaults.MBeanCounterName & "1"
                    Case (-1 * GetAttribute("manufacturingTimeBonus", Defaults.MBeanCounterName & "2") / 100)
                        cmbBeanCounterManufacturing.Text = Defaults.MBeanCounterName & "2"
                    Case (-1 * GetAttribute("manufacturingTimeBonus", Defaults.MBeanCounterName & "4") / 100)
                        cmbBeanCounterManufacturing.Text = Defaults.MBeanCounterName & "4"
                End Select
            Else
                cmbBeanCounterManufacturing.Enabled = False
            End If

            If .RefiningImplantValue > 0 Then
                chkBeanCounterRefining.Checked = True
                Select Case .RefiningImplantValue
                    Case (GetAttribute("refiningYieldMutator", Defaults.RBeanCounterName & "1") / 100)
                        cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "1"
                    Case (GetAttribute("refiningYieldMutator", Defaults.RBeanCounterName & "2") / 100)
                        cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "2"
                    Case (GetAttribute("refiningYieldMutator", Defaults.RBeanCounterName & "4") / 100)
                        cmbBeanCounterRefining.Text = Defaults.RBeanCounterName & "4"
                End Select
            Else
                cmbBeanCounterRefining.Enabled = False
            End If

            If .CopyImplantValue > 0 Then
                chkBeanCounterCopy.Checked = True
                Select Case .CopyImplantValue
                    Case (-1 * GetAttribute("copySpeedBonus", Defaults.CBeanCounterName & "1") / 100)
                        cmbBeanCounterCopy.Text = Defaults.CBeanCounterName & "1"
                    Case (-1 * GetAttribute("copySpeedBonus", Defaults.CBeanCounterName & "3") / 100)
                        cmbBeanCounterCopy.Text = Defaults.CBeanCounterName & "3"
                    Case (-1 * GetAttribute("copySpeedBonus", Defaults.CBeanCounterName & "5") / 100)
                        cmbBeanCounterCopy.Text = Defaults.CBeanCounterName & "5"
                End Select
            Else
                cmbBeanCounterCopy.Enabled = False
            End If

            ' For Build/Buy
            chkBuildBuyDefault.Checked = .CheckBuildBuy
            chkSuggestBuildwhenBPnotOwned.Checked = .SuggestBuildBPNotOwned
            chkSaveBPRelicsDecryptors.Checked = .SaveBPRelicsDecryptors

            chkDisableSVR.Checked = .DisableSVR

            ' ShoppingList
            chkIncludeShopListInventMats.Checked = .ShopListIncludeInventMats
            chkIncludeShopListT3InventionMats.Checked = .ShopListIncludeREMats

            If .RefiningEfficiency <> Defaults.DefaultRefiningEfficency Then
                cmbRefiningEfficiency.Text = FormatPercent(.RefiningEfficiency, 0)
            Else
                cmbRefiningEfficiency.Text = FormatPercent(Defaults.DefaultRefiningEfficency, 0)
            End If

            If .DefaultBPME = 0 Then
                txtDefaultME.Text = CStr(Defaults.DefaultSettingME)
                chkDefaultME.Checked = False
                txtDefaultME.Enabled = False
            Else
                txtDefaultME.Text = CStr(.DefaultBPME)
                chkDefaultME.Checked = True
                txtDefaultME.Enabled = True
            End If

            If .DefaultBPTE = 0 Then
                txtDefaultTE.Text = CStr(Defaults.DefaultSettingTE)
                chkDefaultTE.Checked = False
                txtDefaultTE.Enabled = False
            Else
                txtDefaultTE.Text = CStr(.DefaultBPTE)
                chkDefaultTE.Checked = True
                txtDefaultTE.Enabled = True
            End If

            If .EVECentralRefreshInterval <> Defaults.DefaultEVECentralRefreshInterval Then
                chkEVECentralInterval.Checked = True
                txtEVECentralInterval.Enabled = True
                txtEVECentralInterval.Text = CStr(.EVECentralRefreshInterval)
            Else
                chkEVECentralInterval.Checked = False
                txtEVECentralInterval.Enabled = False
                txtEVECentralInterval.Text = CStr(Defaults.DefaultEVECentralRefreshInterval)
            End If

        End With

        FirstLoad = False

        btnSave.Focus()

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim TempSettings As ApplicationSettings = Nothing

        ' Default values are 0 for implants in settings, since the value stored will get added later. This is the value bonus of the implant
        Dim RefineImplantValue As Double = 0
        Dim ManufacturingImplantValue As Double = 0
        Dim CopyImplantValue As Double = 0

        Dim Settings As New ProgramSettings

        If btnSave.Text = "Save" Then

            ' Make sure accurate data is entered
            If Not CheckEntries() Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            ' Get the implant values if set
            If chkBeanCounterManufacturing.Checked Then
                ManufacturingImplantValue = -1 * GetAttribute("manufacturingTimeBonus", cmbBeanCounterManufacturing.Text) / 100
            End If

            If chkBeanCounterRefining.Checked Then
                RefineImplantValue = GetAttribute("refiningYieldMutator", cmbBeanCounterRefining.Text) / 100
            End If

            If chkBeanCounterCopy.Checked Then
                CopyImplantValue = -1 * GetAttribute("copySpeedBonus", cmbBeanCounterCopy.Text) / 100
            End If

            TempSettings.CheckforUpdatesonStart = CBool(chkCheckUpdatesStartup.Checked)
            If rbtnExportDefault.Checked Then
                TempSettings.DataExportFormat = rbtnExportDefault.Text
            ElseIf rbtnExportCSV.Checked Then
                TempSettings.DataExportFormat = rbtnExportCSV.Text
            ElseIf rbtnExportSSV.Checked Then
                TempSettings.DataExportFormat = rbtnExportSSV.Text
            End If
            TempSettings.ShowToolTips = CBool(chkShowToolTips.Checked)
            TempSettings.DisableSound = CBool(chkDisableSound.Checked)
            TempSettings.RefiningImplantValue = RefineImplantValue
            TempSettings.ManufacturingImplantValue = ManufacturingImplantValue
            TempSettings.CopyImplantValue = CopyImplantValue

            ' CREST
            TempSettings.LoadCRESTFacilityDataonStartup = chkRefreshFacilityDataonStartup.Checked
            TempSettings.LoadCRESTMarketDataonStartup = chkRefreshMarketDataonStartup.Checked
            TempSettings.LoadCRESTTeamDataonStartup = chkRefreshTeamDataonStartup.Checked

            ' If they didn't have this checked before, refresh assets
            If TempSettings.LoadAssetsonStartup = False And chkRefreshAssetsonStartup.Checked Then
                Call SelectedCharacter.GetAssets.LoadAssets(ScanType.Personal, True)
                Call SelectedCharacter.CharacterCorporation.GetAssets.LoadAssets(ScanType.Corporation, True)
            End If

            ' Same with blueprints
            If TempSettings.LoadBPsonStartup = False And chkRefreshBPsonStartup.Checked Then
                Call SelectedCharacter.GetBlueprints.LoadBlueprints(ScanType.Personal, True)
                Call SelectedCharacter.CharacterCorporation.GetBlueprints.LoadBlueprints(ScanType.Corporation, True)
            End If

            ' Now set these
            TempSettings.LoadAssetsonStartup = CBool(chkRefreshAssetsonStartup.Checked)
            TempSettings.LoadBPsonStartup = CBool(chkRefreshBPsonStartup.Checked)

            ' Standings
            TempSettings.BrokerCorpStanding = CDbl(txtBrokerCorpStanding.Text)
            TempSettings.BrokerFactionStanding = CDbl(txtBrokerFactionStanding.Text)
            TempSettings.RefineCorpStanding = CDbl(txtRefineCorpStanding.Text)

            ' Default build/buy
            TempSettings.CheckBuildBuy = CBool(chkBuildBuyDefault.Checked)

            If cmbRefiningEfficiency.Text.Contains("%") Then
                TempSettings.RefiningEfficiency = CDbl(cmbRefiningEfficiency.Text.Substring(0, Len(cmbRefiningEfficiency.Text) - 1)) / 100
            Else
                TempSettings.RefiningEfficiency = CDbl(cmbRefiningEfficiency.Text) / 100
            End If

            TempSettings.DefaultBPME = CInt(txtDefaultME.Text)
            TempSettings.DefaultBPTE = CInt(txtDefaultTE.Text)

            TempSettings.DisableSVR = chkDisableSVR.Checked
            TempSettings.SuggestBuildBPNotOwned = chkSuggestBuildwhenBPnotOwned.Checked
            TempSettings.SaveBPRelicsDecryptors = chkSaveBPRelicsDecryptors.Checked

            TempSettings.ShopListIncludeInventMats = chkIncludeShopListInventMats.Checked
            TempSettings.ShopListIncludeREMats = chkIncludeShopListT3InventionMats.Checked

            Dim RefineTax As Double = CDbl(cmbRefineTax.Text.Replace("%", ""))

            If RefineTax <= 0 Then
                TempSettings.RefiningTax = 0
            Else
                TempSettings.RefiningTax = RefineTax / 100
            End If

            TempSettings.EVECentralRefreshInterval = CInt(txtEVECentralInterval.Text)

            ' Save the data in the XML file
            Call Settings.SaveApplicationSettings(TempSettings)

            ' Save the data to the local variable
            UserApplicationSettings = TempSettings

            MsgBox("Settings Saved", vbInformation, Application.ProductName)

            btnSave.Text = "OK"
            GoTo ExitProc
        Else
            ' Just exit
            Me.Hide()
        End If

        btnSave.Focus()

        Exit Sub

ExitProc:
        Me.Cursor = Cursors.Default
        frmMain.ReloadSettings = True

    End Sub

    Private Function CheckEntries() As Boolean
        Dim TempTextBox As TextBox = Nothing
        Dim TempCheckBox As CheckBox = Nothing
        Dim TempComboBox As ComboBox = Nothing

        Dim TempBuildDiscount As String
        TempBuildDiscount = cmbRefineTax.Text.Replace("%", "")

        If (Not IsNumeric(txtBrokerCorpStanding.Text) Or Trim(txtBrokerCorpStanding.Text) = "") And chkBrokerCorpStanding.Checked Then
            TempTextBox = txtBrokerCorpStanding
            TempCheckBox = chkBrokerCorpStanding
            GoTo InvalidData
        ElseIf CDbl(txtBrokerCorpStanding.Text) > 10 Then
            txtBrokerCorpStanding.Text = "10.0"
        End If

        If (Not IsNumeric(txtBrokerFactionStanding.Text) Or Trim(txtBrokerFactionStanding.Text) = "") And chkBrokerFactionStanding.Checked Then
            TempTextBox = txtBrokerFactionStanding
            TempCheckBox = chkBrokerFactionStanding
            GoTo InvalidData
        ElseIf CDbl(txtBrokerFactionStanding.Text) > 10 Then
            txtBrokerFactionStanding.Text = "10.0"
        End If

        If (Not IsNumeric(txtRefineCorpStanding.Text) Or Trim(txtRefineCorpStanding.Text) = "") And chkBrokerFactionStanding.Checked Then
            TempTextBox = txtRefineCorpStanding
            TempCheckBox = chkRefineCorpStanding
            GoTo InvalidData
        ElseIf CDbl(txtRefineCorpStanding.Text) > 10 Then
            txtRefineCorpStanding.Text = "10.0"
        End If

        ' Refine Tax
        Dim TempRefine As String
        TempRefine = cmbRefineTax.Text.Replace("%", "")

        If (Not IsNumeric(TempRefine) Or Trim(TempRefine) = "") And chkRefineStationTax.Checked Then
            TempComboBox = cmbRefineTax
            TempCheckBox = chkRefineStationTax
            GoTo InvalidData
        ElseIf CDbl(TempRefine) > 10 Then
            txtBrokerFactionStanding.Text = "10.0"
        End If

        ' ME/TE
        If (Not IsNumeric(txtDefaultME.Text) Or Trim(txtDefaultME.Text) = "") And chkDefaultME.Checked Then
            TempTextBox = txtDefaultME
            TempCheckBox = chkDefaultME
            GoTo InvalidData
        End If

        If (Not IsNumeric(txtDefaultTE.Text) Or Trim(txtDefaultTE.Text) = "") And chkDefaultTE.Checked Then
            TempTextBox = txtDefaultTE
            TempCheckBox = chkDefaultTE
            GoTo InvalidData
        End If

        If (Not IsNumeric(txtEVECentralInterval.Text) Or Trim(txtEVECentralInterval.Text) = "") And chkEVECentralInterval.Checked Then
            TempTextBox = txtEVECentralInterval
            TempCheckBox = chkEVECentralInterval
            GoTo InvalidData
        ElseIf CInt(txtEVECentralInterval.Text) <= 0 Then
            MsgBox("Cannot set EVE Central Update Interval less than 1 Hour", vbExclamation, Application.ProductName)
            txtEVECentralInterval.Focus()
            Call txtEVECentralInterval.SelectAll()
            Return False
        ElseIf CInt(txtEVECentralInterval.Text) > 99 Then
            MsgBox("Cannot set EVE Central Update Interval greater than 99 hours", vbExclamation, Application.ProductName)
            txtEVECentralInterval.Focus()
            Call txtEVECentralInterval.SelectAll()
            Return False
        End If

        Return True

InvalidData:

        If Not IsNothing(TempComboBox) Then
            MsgBox("Invalid " & TempComboBox.Name & " Value", vbExclamation, Application.ProductName)
            TempComboBox.Focus()
            Call TempComboBox.SelectAll()
        Else
            MsgBox("Invalid " & TempCheckBox.Name & " Value", vbExclamation, Application.ProductName)
            TempTextBox.Focus()
            Call TempTextBox.SelectAll()
        End If

        Return False

    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If SelectedReset Then
            ' If we hit reset, then we need to get the current list of settings, not just what is loaded (might be defaults)
            ' So just reload the settings
            UserApplicationSettings = AllSettings.LoadApplicationSettings()
        End If
        Me.Hide()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        SelectedReset = True
        ' Load default settings
        UserApplicationSettings = AllSettings.SetDefaultApplicationSettings()
        ' Reload the form
        Call LoadFormSettings()

    End Sub

    Private Sub btnEditPOS_Click(sender As System.Object, e As System.EventArgs)
        Dim f1 As New frmPOSSettings
        f1.ShowDialog()
    End Sub

    Private Sub chkRefreshTeamDataonStartup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRefreshTeamDataonStartup.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub chkRefreshMarketDataonStartup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRefreshMarketDataonStartup.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub chkRefreshFacilityDataonStartup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRefreshFacilityDataonStartup.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub rbtnExportDefault_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnExportDefault.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub rbtnExportCSV_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnExportCSV.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub rbtnExportSSV_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnExportSSV.CheckedChanged
        btnSave.Text = "Save"
    End Sub

    Private Sub chkLinkBPTabTeamstoSystem_CheckedChanged(sender As System.Object, e As System.EventArgs)
        btnSave.Text = "Save"
    End Sub

    Private Sub chkSaveBPRelicsDecryptors_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSaveBPRelicsDecryptors.CheckedChanged
        btnSave.Text = "Save"
    End Sub

End Class