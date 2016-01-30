
Public Class IPHIndustryFacility

    Private FacilityLoaded As Boolean
    Private FacilityRegionsLoaded As Boolean
    Private FacilitySystemsLoaded As Boolean
    Private FacilitiesLoaded As Boolean

    Private PreviousIndustryType As IndustryType
    Private PreviousFacilityType As String
    Private PreviousFacilityRegion As String
    Private PreviousFacilitySystem As String
    Private PreviousFacilityEquipment As String
    Private PreviousActivity As String
    Private CurrentIndustryType As IndustryType
    Private CurrentBPGroupID As Integer
    Private CurrentBPCategoryID As Integer

    Private ttBP As ToolTip
    Private cmbCalcFWManufUpgradeLevel As ComboBox
    Private DefaultFuelBlockFacility As IndustryFacility
    Private DefaultLargeShipFacility As IndustryFacility
    Private DefaultManufacturingFacility As IndustryFacility
    Private DefaultModuleFacility As IndustryFacility
    Private SelectedFuelBlockFacility As IndustryFacility
    Private SelectedLargeShipFacility As IndustryFacility
    Private SelectedManufacturingFacility As IndustryFacility
    Private SelectedModuleFacility As IndustryFacility

    Private Sub cmbFacilityType_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityType.DropDown
        PreviousFacilityType = cmbFacilityType.Text
    End Sub

    Private Sub cmbFacilityType_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityType.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbFacilityType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityType.SelectedIndexChanged
        ' Don't do anything if it's the same as the old type
        If PreviousFacilityType <> cmbFacilityType.Text And Not FirstLoad Then
            If Not LoadingFacilityTypes And Not FirstLoad Then
                Call LoadFacilityRegions(0, 0, True, _
                                         ActivityManufacturing, cmbFacilityType, cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                         lblFacilityBonus, lblFacilityDefault, _
                                         lblFacilityManualME, txtFacilityManualME, _
                                         lblFacilityManualTE, txtFacilityManualTE, _
                                         lblFacilityManualTax, txtFacilityManualTax, _
                                         btnFacilitySave, lblFacilityTaxRate, CalcTab, chkFacilityIncludeUsage)
                Call cmbFacilityRegion.Focus()
            End If

            ' hide array selection for non-pos, hide the other drop downs
            If cmbFacilityType.Text = POSFacility Then
                Call SetPOSMultiUseArraysVisibility(True, False)
            Else
                Call SetPOSMultiUseArraysVisibility(False, False)
            End If

            ' Anytime this changes, set all the other ME/TE boxes to not viewed
            Call HideFacilityBonusBoxes(lblFacilityBonus, lblFacilityTaxRate, _
                                        lblFacilityManualME, lblFacilityManualTE, _
                                        txtFacilityManualME, txtFacilityManualTE, _
                                        lblFacilityManualTax, txtFacilityManualTax)
            FacilityLoaded = False
        End If
    End Sub

    Private Sub cmbFacilityRegion_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityRegion.DropDown
        If Not FirstLoad And Not FacilityRegionsLoaded Then
            ' Save the current
            PreviousFacilityRegion = cmbFacilityRegion.Text
            Call LoadFacilityRegions(0, 0, False, _
                                     ActivityManufacturing, cmbFacilityType, cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                     lblFacilityBonus, lblFacilityDefault, _
                                     lblFacilityManualME, txtFacilityManualME, _
                                     lblFacilityManualTE, txtFacilityManualTE, _
                                     lblFacilityManualTax, txtFacilityManualTax, _
                                     btnFacilitySave, lblFacilityTaxRate, CalcTab, chkFacilityIncludeUsage)
        End If
    End Sub

    Private Sub cmbFacilityRegion_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityRegion.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbFacilityRegion_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityRegion.SelectedIndexChanged

        If Not LoadingFacilityRegions And Not FirstLoad And PreviousFacilityRegion <> cmbFacilityRegion.Text Then
            Call LoadFacilitySystems(0, 0, True, _
                                     ActivityManufacturing, cmbFacilityType, cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                     lblFacilityBonus, lblFacilityTaxRate, _
                                     lblFacilityManualME, txtFacilityManualME, _
                                     lblFacilityManualTE, txtFacilityManualTE, _
                                     lblFacilityManualTax, txtFacilityManualTax, _
                                     lblFacilityDefault, btnFacilitySave, CalcTab, chkFacilityIncludeUsage)
            Call cmbFacilitySystem.Focus()
            Call HideFacilityBonusBoxes(lblFacilityBonus, lblFacilityTaxRate, _
                             lblFacilityManualME, lblFacilityManualTE, _
                             txtFacilityManualME, txtFacilityManualTE, _
                             lblFacilityManualTax, txtFacilityManualTax)
            FacilityLoaded = False
            PreviousFacilityRegion = cmbFacilityRegion.Text

            ' Make sure the pos facility stuff is still hidden
            ' hide array selection for non-pos, hide the other drop downs
            If cmbFacilityType.Text = POSFacility Then
                Call SetPOSMultiUseArraysVisibility(True, False)
            Else
                Call SetPOSMultiUseArraysVisibility(False, False)
            End If

        End If
    End Sub

    Private Sub cmbFacilitySystem_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilitySystem.DropDown
        If Not FacilitySystemsLoaded And Not FirstLoad Then
            PreviousFacilitySystem = cmbFacilitySystem.Text
            Call LoadFacilitySystems(0, 0, False, _
                                     ActivityManufacturing, cmbFacilityType, cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                     lblFacilityBonus, lblFacilityTaxRate, _
                                     lblFacilityManualME, txtFacilityManualME, _
                                     lblFacilityManualTE, txtFacilityManualTE, _
                                     lblFacilityManualTax, txtFacilityManualTax, _
                                     lblFacilityDefault, btnFacilitySave, CalcTab, chkFacilityIncludeUsage)
        End If
    End Sub

    Private Sub cmbFacilitySystem_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilitySystem.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbFacilitySystem_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilitySystem.SelectedIndexChanged
        Dim OverrideFacilityName As String = ""
        Dim Autoload As Boolean = False

        If Not LoadingFacilitySystems And Not FirstLoad And PreviousFacilitySystem <> cmbFacilitySystem.Text Then

            If cmbFacilityType.Text = OutpostFacility Then
                OverrideFacilityName = ""
                Autoload = True
            ElseIf cmbFacilityType.Text = POSFacility Then
                OverrideFacilityName = "" ' Will trigger an autoload
            End If

            ' Load the facility and set the auto
            Call LoadFacilities(0, 0, False, _
                                ActivityManufacturing, cmbFacilityType, cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                lblFacilityBonus, lblFacilityDefault, _
                                lblFacilityManualME, txtFacilityManualME, _
                                lblFacilityManualTE, txtFacilityManualTE, _
                                lblFacilityManualTax, txtFacilityManualTax, _
                                btnFacilitySave, lblFacilityTaxRate, CalcTab, _
                                chkFacilityIncludeUsage, Nothing, Nothing, chkFacilityIncludeUsage.Checked, Autoload, OverrideFacilityName)

            If cmbFacilityType.Text = POSFacility Then
                ' Hide all these labels so we can see the other blocks
                Call HideFacilityBonusBoxes(lblFacilityBonus, lblFacilityTaxRate, _
                                            lblFacilityManualME, lblFacilityManualTE, _
                                            txtFacilityManualME, txtFacilityManualTE, _
                                            lblFacilityManualTax, txtFacilityManualTax)

                ' See if this is the default pos
                With DefaultManufacturingFacility
                    If cmbFacilityRegion.Text = .RegionName _
                        And cmbFacilitySystem.Text = .SolarSystemName _
                        And chkFacilityIncludeUsage.Checked = .IncludeActivityUsage Then
                        ' Is the default, set it
                        btnFacilitySave.Enabled = False
                        lblFacilityDefault.ForeColor = SystemColors.Highlight
                        Call ResetToolTipforDefaultFacilityLabel(lblFacilityDefault, False, ttBP)
                        SelectedManufacturingFacility.IsDefault = True
                    Else
                        ' Allow saving of default
                        btnFacilitySave.Enabled = True
                        lblFacilityDefault.ForeColor = SystemColors.ButtonShadow
                        Call ResetToolTipforDefaultFacilityLabel(lblFacilityDefault, True, ttBP)
                        SelectedManufacturingFacility.IsDefault = False
                    End If
                End With

            End If

            ' Hide for non-pos but show and enable for pos
            If cmbFacilityType.Text = POSFacility Then
                Call SetPOSMultiUseArraysVisibility(True, True)
            Else
                Call SetPOSMultiUseArraysVisibility(False, False)
            End If

            If Autoload Or cmbFacilityType.Text = POSFacility Then
                ' reload bp Use the original ME and TE values when they change the meta level
                FacilityLoaded = True
                FacilitiesLoaded = True
            Else
                FacilitiesLoaded = False
            End If

            Call cmbFacilityorArray.Focus()
            PreviousFacilitySystem = cmbFacilitySystem.Text

            ' Set the save button in case it's enabled when we don't have a facility selected
            If cmbFacilityorArray.Text = "Select Facility" Then
                btnFacilitySave.Enabled = False
            End If
        End If

    End Sub

    Private Sub SetPOSMultiUseArraysVisibility(ByVal Visible As Boolean, ByVal Enabled As Boolean)
        If Visible Then
            cmbFacilityorArray.Visible = False
            ' Show the POS modules for multi-use
            cmbFuelBlocks.Visible = True
            cmbLargeShips.Visible = True
            cmbModules.Visible = True
            lblFuelBlocks.Visible = True
            lblLargeShips.Visible = True
            lblModules.Visible = True
        Else
            cmbFacilityorArray.Visible = True
            ' Hide the POS modules for multi-use
            cmbFuelBlocks.Visible = False
            cmbLargeShips.Visible = False
            cmbModules.Visible = False
            lblFuelBlocks.Visible = False
            lblLargeShips.Visible = False
            lblModules.Visible = False
        End If

        If Enabled Then
            ' Enable the POS modules for multi-use
            cmbFuelBlocks.Enabled = True
            cmbLargeShips.Enabled = True
            cmbModules.Enabled = True
            lblFuelBlocks.Enabled = True
            lblLargeShips.Enabled = True
            lblModules.Enabled = True
        Else
            ' Disable the POS modules for multi-use
            cmbFuelBlocks.Enabled = False
            cmbLargeShips.Enabled = False
            cmbModules.Enabled = False
            lblFuelBlocks.Enabled = False
            lblLargeShips.Enabled = False
            lblModules.Enabled = False
        End If

    End Sub

    Private Sub cmbFacilityorArray_DropDown(sender As Object, e As System.EventArgs) Handles cmbFacilityorArray.DropDown
        If Not FacilitiesLoaded And Not FirstLoad And cmbFacilityType.Text <> POSFacility Then
            Call LoadFacilities(0, 0, False, _
                                ActivityManufacturing, cmbFacilityType, cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                lblFacilityBonus, lblFacilityDefault, _
                                lblFacilityManualME, txtFacilityManualME, _
                                lblFacilityManualTE, txtFacilityManualTE, _
                                lblFacilityManualTax, txtFacilityManualTax, _
                                btnFacilitySave, lblFacilityTaxRate, CalcTab, _
                                chkFacilityIncludeUsage, Nothing, Nothing, Nothing, chkFacilityIncludeUsage.Checked)
        End If
    End Sub

    Private Sub cmbFacilityorArray_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbFacilityorArray.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbFacilityArrayName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFacilityorArray.SelectedIndexChanged

        If Not LoadingFacilities And Not FirstLoad And PreviousFacilityEquipment <> cmbFacilityorArray.Text Then
            ' We won't have any MM or TM to send, so just do default
            Dim Defaults As New ProgramSettings

            Call DisplayFacilityBonus(IndustryType.Manufacturing, _
                                      Defaults.FacilityDefaultMM, Defaults.FacilityDefaultTM, Defaults.FacilityDefaultTax, 0, 0, _
                                      ActivityManufacturing, cmbFacilityType.Text, cmbFacilityorArray.Text, _
                                      cmbFacilityRegion, cmbFacilitySystem, cmbFacilityorArray, _
                                      lblFacilityBonus, lblFacilityDefault, _
                                      lblFacilityManualME, txtFacilityManualME, _
                                      lblFacilityManualTE, txtFacilityManualTE, _
                                      lblFacilityManualTax, txtFacilityManualTax, _
                                      btnFacilitySave, lblFacilityTaxRate, _
                                      chkFacilityIncludeUsage, Nothing, Nothing, CalcTab, FacilityLoaded, _
                                      chkFacilityIncludeUsage.Checked, ttBP, GetFWUpgradeLevel(cmbCalcFWManufUpgradeLevel, cmbFacilitySystem.Text))

            If txtFacilityManualME.Visible Then
                Call txtFacilityManualME.Focus()
            End If

            PreviousFacilityEquipment = cmbFacilityorArray.Text

        End If
    End Sub

    Private Sub btnFacilitySave_Click(sender As System.Object, e As System.EventArgs) Handles btnFacilitySave.Click

        SelectedManufacturingFacility.IncludeActivityUsage = chkFacilityIncludeUsage.Checked
        Call SelectedManufacturingFacility.SaveFacility(CalcTab)
        'Call UpdateMMTMTaxDataforOutpost(SelectedManufacturingFacility, IndustryActivities.Manufacturing)
        'DefaultManufacturingFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)

        'If cmbFuelBlocks.Visible Then
        '    ' Save the three POS module selections for multi-modules
        '    ' Always reset each to the current facility and then set the facility name by the text box entry
        '    SelectedFuelBlockFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
        '    SelectedFuelBlockFacility.ProductionType = IndustryType.POSFuelBlockManufacturing
        '    SelectedFuelBlockFacility.FacilityName = GetMultiUseArrayName(cmbFuelBlocks.Text)
        '    Call SelectedFuelBlockFacility.SaveFacility(CalcTab)
        '    DefaultFuelBlockFacility = CType(SelectedFuelBlockFacility.Clone, IndustryFacility)

        '    SelectedLargeShipFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
        '    SelectedLargeShipFacility.ProductionType = IndustryType.POSLargeShipManufacturing
        '    SelectedLargeShipFacility.FacilityName = GetMultiUseArrayName(cmbLargeShips.Text)
        '    Call SelectedLargeShipFacility.SaveFacility(CalcTab)
        '    DefaultLargeShipFacility = CType(SelectedLargeShipFacility.Clone, IndustryFacility)

        '    SelectedModuleFacility = CType(SelectedManufacturingFacility.Clone, IndustryFacility)
        '    SelectedModuleFacility.ProductionType = IndustryType.POSModuleManufacturing
        '    SelectedModuleFacility.FacilityName = GetMultiUseArrayName(cmbModules.Text)
        '    Call SelectedModuleFacility.SaveFacility(CalcTab)
        '    DefaultModuleFacility = CType(SelectedModuleFacility.Clone, IndustryFacility)
        'End If

        lblFacilityDefault.ForeColor = SystemColors.Highlight
        Call ResetToolTipforDefaultFacilityLabel(lblFacilityDefault, False, ttBP)

        ' They just saved it
        btnFacilitySave.Enabled = False

        MsgBox("Default Facility Saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub txtFacilityManualME_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFacilityManualME.KeyPress
        Call OutpostMETETaxText_KeyPress(e)
    End Sub

    Private Sub txtFacilityManualME_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFacilityManualME.KeyUp
        Call OutpostMETETaxText_KeyUp("ME", txtFacilityManualME, SelectedManufacturingFacility, _
                                      cmbFacilityType, btnFacilitySave, lblFacilityDefault)
    End Sub

    Private Sub txtFacilityManualME_LostFocus(sender As Object, e As System.EventArgs) Handles txtFacilityManualME.LostFocus
        Call OutpostMETETaxText_LostFocus(txtFacilityManualME, cmbFacilityType, SelectedManufacturingFacility.MaterialMultiplier, False)
    End Sub

    Private Sub txtFacilityManualTE_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFacilityManualTE.KeyPress
        Call OutpostMETETaxText_KeyPress(e)
    End Sub

    Private Sub txtFacilityManualTE_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFacilityManualTE.KeyUp
        Call OutpostMETETaxText_KeyUp("TE", txtFacilityManualTE, SelectedManufacturingFacility, _
                                      cmbFacilityType, btnFacilitySave, lblFacilityDefault)
    End Sub

    Private Sub txtFacilityManualTE_LostFocus(sender As Object, e As System.EventArgs) Handles txtFacilityManualTE.LostFocus
        Call OutpostMETETaxText_LostFocus(txtFacilityManualTE, cmbFacilityType, SelectedManufacturingFacility.MaterialMultiplier, False)
    End Sub

    Private Sub txtFacilityManualTax_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFacilityManualTax.KeyPress
        Call OutpostMETETaxText_KeyPress(e)
    End Sub

    Private Sub txtFacilityManualTax_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFacilityManualTax.KeyUp
        Call OutpostMETETaxText_KeyUp("Tax", txtFacilityManualTax, SelectedManufacturingFacility, _
                                      cmbFacilityType, btnFacilitySave, lblFacilityDefault)
    End Sub

    Private Sub txtFacilityManualTax_LostFocus(sender As Object, e As System.EventArgs) Handles txtFacilityManualTax.LostFocus
        Call OutpostMETETaxText_LostFocus(txtFacilityManualTax, cmbFacilityType, SelectedManufacturingFacility.MaterialMultiplier, False)
    End Sub

    Private Sub chkFacilityIncludeCosts_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFacilityIncludeUsage.CheckedChanged
        If Not FirstLoad Then

            Call SetDefaultFacilitybyCheck(GetProductionType(ActivityManufacturing, 0, 0, cmbFacilityType.Text), _
                               chkFacilityIncludeUsage, CalcTab, cmbFacilityType.Text, cmbFacilityorArray, _
                               lblFacilityDefault, btnFacilitySave, Nothing, Nothing, ttBP)

            If cmbFacilityType.Text = POSFacility Then
                ' For this check, if a POS is selected, we need to check all three multi-array types plus the base facility
                Call SetDefaultFacilitybyCheck(IndustryType.POSModuleManufacturing, _
                   chkFacilityIncludeUsage, CalcTab, cmbFacilityType.Text, cmbFacilityorArray, _
                   lblFacilityDefault, btnFacilitySave, Nothing, Nothing, ttBP)

                Call SetDefaultFacilitybyCheck(IndustryType.POSFuelBlockManufacturing, _
                   chkFacilityIncludeUsage, CalcTab, cmbFacilityType.Text, cmbFacilityorArray, _
                   lblFacilityDefault, btnFacilitySave, Nothing, Nothing, ttBP)

                Call SetDefaultFacilitybyCheck(IndustryType.POSLargeShipManufacturing, _
                   chkFacilityIncludeUsage, CalcTab, cmbFacilityType.Text, cmbFacilityorArray, _
                   lblFacilityDefault, btnFacilitySave, Nothing, Nothing, ttBP)

            End If

            'Call ResetRefresh()
        End If
    End Sub

    ' Keypress, Keyup, and Lost focus functions for manual ME/TE/Tax boxes
    Private Sub OutpostMETETaxText_KeyPress(e As System.Windows.Forms.KeyPressEventArgs)
        ' only let them enter the right things
        If e.KeyChar <> ControlChars.Back Then
            If allowedMETEChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub OutpostMETETaxText_KeyUp(ByVal UpdateType As String, ByRef ManualTextBox As TextBox, ByRef SelectedFacility As IndustryFacility, _
                                         ByRef FacilityTypeCombo As ComboBox, ByRef SaveButton As Button, ByRef DefaultLabel As Label)
        Dim Temp As String
        Dim TempValue As Decimal

        ' Get rid of the percent sign if it exists
        Temp = Replace(ManualTextBox.Text, "%", "")

        If Not IsNumeric(Temp) Then
            Temp = "0.0"
            ManualTextBox.Text = "0.0%"
        End If

        TempValue = (100 - CDec(Temp)) / 100

        ' If it's an outpost, then save the ME/TE/Tax for this in the current facility
        If FacilityTypeCombo.Text = OutpostFacility Then
            If UpdateType = "ME" Then
                SelectedFacility.MaterialMultiplier = TempValue
            ElseIf UpdateType = "TE" Then
                SelectedFacility.TimeMultiplier = TempValue
            Else
                SelectedFacility.TaxRate = CDbl(1 - TempValue) ' Tax rate is a straight multiplication, not multiplied with other bonuses
            End If

        End If

        ' They changed the value, so enable save
        SaveButton.Enabled = True
        ' changed so not the default
        DefaultLabel.Visible = False

    End Sub

    Private Sub OutpostMETETaxText_LostFocus(ByRef ManualTextBox As TextBox, ByRef FacilityTypeCombo As ComboBox, _
                                             ByRef MaterialMultiplier As Double, ByVal BPTab As Boolean)
        If Trim(Replace(ManualTextBox.Text, "%", "")) = "" And FacilityTypeCombo.Text = OutpostFacility Then
            ManualTextBox.Text = FormatPercent(MaterialMultiplier, 1)
        End If

        If Not ManualTextBox.Text.Contains("%") Then
            ' Format with percent sign
            ManualTextBox.Text = FormatPercent(CDbl(ManualTextBox.Text) / 100, 1)
        End If

        If BPTab Then
            'Call RefreshBP(True)
        End If
    End Sub

End Class
