Public Class frmEditDefaultRates
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmEditDefaultRates_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Load the current rates saved
        With UserApplicationSettings
            txtBaseBrokerFee.Text = FormatPercent(.BaseBrokerFeeRate / 100, 2)
            txtBaseSalesTax.Text = FormatPercent(.BaseSalesTaxRate / 100, 2)

            txtAlphaAccountTaxRate.Text = FormatPercent(.AlphaAccountTaxRate, 2)
            txtDefaultStationTaxRate.Text = FormatPercent(.StationTaxRate, 2)
            txtDefaultStructureTaxRate.Text = FormatPercent(.StructureTaxRate, 2)
            txtSCCBrokerFeeSurcharge.Text = FormatPercent(.SCCBrokerFeeSurcharge, 2)
            txtSCCIndustryFeeSurcharge.Text = FormatPercent(.SCCIndustryFeeSurcharge, 2)
        End With
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim DataCheckGood As Boolean = True
        Dim TextBoxRef As New TextBox

        ' Data check
        If Not IsNumeric(txtBaseBrokerFee.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtBaseBrokerFee
        End If
        If Not IsNumeric(txtBaseSalesTax.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtBaseSalesTax
        End If
        If Not IsNumeric(txtSCCBrokerFeeSurcharge.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtSCCBrokerFeeSurcharge
        End If
        If Not IsNumeric(txtSCCIndustryFeeSurcharge.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtSCCIndustryFeeSurcharge
        End If
        If Not IsNumeric(txtAlphaAccountTaxRate.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtAlphaAccountTaxRate
        End If
        If Not IsNumeric(txtDefaultStationTaxRate.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtDefaultStationTaxRate
        End If
        If Not IsNumeric(txtDefaultStructureTaxRate.Text.Replace("%", "")) Then
            DataCheckGood = False
            TextBoxRef = txtDefaultStructureTaxRate
        End If

        If Not DataCheckGood Then
            MsgBox("Invalid Entry Data", vbExclamation, TextBoxRef.Name)
            TextBoxRef.SelectAll()
            Exit Sub
        End If

        ' Save just these to the userapp settings
        With UserApplicationSettings
            ' Need digits for these
            .BaseBrokerFeeRate = CDbl(txtBaseBrokerFee.Text.Replace("%", ""))
            .BaseSalesTaxRate = CDbl(txtBaseSalesTax.Text.Replace("%", ""))
            ' Just save these normally
            .SCCBrokerFeeSurcharge = CDbl(txtSCCBrokerFeeSurcharge.Text.Replace("%", "")) / 100
            .SCCIndustryFeeSurcharge = CDbl(txtSCCIndustryFeeSurcharge.Text.Replace("%", "")) / 100
            .StructureTaxRate = CDbl(txtDefaultStructureTaxRate.Text.Replace("%", "")) / 100
            .StationTaxRate = CDbl(txtDefaultStationTaxRate.Text.Replace("%", "")) / 100
            .AlphaAccountTaxRate = CDbl(txtAlphaAccountTaxRate.Text.Replace("%", "")) / 100
        End With

        ' Just Grab whatever is set already and update with the above
        Dim Tempsettings As ApplicationSettings = UserApplicationSettings
        Dim Settings As New ProgramSettings

        ' Save the data in the XML file
        Call Settings.SaveApplicationSettings(Tempsettings)

        ' Load all the forms' facilities 
        Call frmMain.LoadFacilities()
        ' Re-init any tabs that have settings changes before displaying dialog
        Call frmMain.ResetTabs(False)
        Call frmMain.ResetRefresh()

        Call MsgBox("Defaults Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub txtEntries_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAlphaAccountTaxRate.KeyPress,
                                                                              txtBaseBrokerFee.KeyPress, txtBaseSalesTax.KeyPress, txtDefaultStationTaxRate.KeyPress,
                                                                              txtDefaultStructureTaxRate.KeyPress, txtSCCBrokerFeeSurcharge.KeyPress, txtSCCIndustryFeeSurcharge.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            If allowedPercentChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtEntries_LostFocus(sender As Object, e As EventArgs) Handles txtAlphaAccountTaxRate.LostFocus,
                                                                              txtBaseBrokerFee.LostFocus, txtBaseSalesTax.LostFocus, txtDefaultStationTaxRate.LostFocus,
                                                                              txtDefaultStructureTaxRate.LostFocus, txtSCCBrokerFeeSurcharge.LostFocus, txtSCCIndustryFeeSurcharge.LostFocus
        CType(sender, TextBox).Text = FormatRate(CType(sender, TextBox))
    End Sub

    Private Function FormatRate(PricetxtBox As TextBox) As String
        If Trim(PricetxtBox.Text) = "" Then
            Return "0.0%"
        Else
            Return FormatPercent(CDbl(PricetxtBox.Text.Replace("%", "")) / 100, 2)
        End If
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim S As New ProgramSettings

        With UserApplicationSettings
            txtBaseBrokerFee.Text = FormatPercent(S.DefaultBaseBrokerFeeRate / 100, 2)
            txtBaseSalesTax.Text = FormatPercent(S.DefaultBaseSalesTaxRate / 100, 2)

            txtAlphaAccountTaxRate.Text = FormatPercent(S.DefaultAlphaAccountTaxRate, 2)
            txtDefaultStationTaxRate.Text = FormatPercent(S.DefaultStationTaxRate, 2)
            txtDefaultStructureTaxRate.Text = FormatPercent(S.DefaultStructureTaxRate, 2)
            txtSCCBrokerFeeSurcharge.Text = FormatPercent(S.DefaultSCCBrokerFeeSurcharge, 2)
            txtSCCIndustryFeeSurcharge.Text = FormatPercent(S.DefaultSCCIndustryFeeSurcharge, 2)
        End With

        Call MsgBox("Defaults Loaded", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Hide()
    End Sub

End Class