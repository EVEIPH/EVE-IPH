<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IPHIndustryFacility
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtFacilityManualTax = New System.Windows.Forms.TextBox()
        Me.lblFacilityManualTax = New System.Windows.Forms.Label()
        Me.lblInclude = New System.Windows.Forms.Label()
        Me.chkFacilityIncludeUsage = New System.Windows.Forms.CheckBox()
        Me.chkFacilityIncludeTime = New System.Windows.Forms.CheckBox()
        Me.lblFacilityDefault = New System.Windows.Forms.Label()
        Me.chkFacilityIncludeCost = New System.Windows.Forms.CheckBox()
        Me.btnFacilitySave = New System.Windows.Forms.Button()
        Me.txtFacilityManualTE = New System.Windows.Forms.TextBox()
        Me.txtFacilityManualME = New System.Windows.Forms.TextBox()
        Me.cmbFacilityorArray = New System.Windows.Forms.ComboBox()
        Me.cmbFacilitySystem = New System.Windows.Forms.ComboBox()
        Me.cmbFacilityRegion = New System.Windows.Forms.ComboBox()
        Me.lblFacilityManualTE = New System.Windows.Forms.Label()
        Me.lblFacilityLocation = New System.Windows.Forms.Label()
        Me.lblFacilityType = New System.Windows.Forms.Label()
        Me.cmbFacilityType = New System.Windows.Forms.ComboBox()
        Me.lblFacilityManualME = New System.Windows.Forms.Label()
        Me.lblFacilityBonus = New System.Windows.Forms.Label()
        Me.chkComponentsFacility = New System.Windows.Forms.CheckBox()
        Me.lblFacilityTaxRate = New System.Windows.Forms.Label()
        Me.cmbFuelBlocks = New System.Windows.Forms.ComboBox()
        Me.cmbModules = New System.Windows.Forms.ComboBox()
        Me.lblFuelBlocks = New System.Windows.Forms.Label()
        Me.lblModules = New System.Windows.Forms.Label()
        Me.cmbLargeShips = New System.Windows.Forms.ComboBox()
        Me.lblLargeShips = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'txtFacilityManualTax
        '
        Me.txtFacilityManualTax.Location = New System.Drawing.Point(195, 87)
        Me.txtFacilityManualTax.MaxLength = 5
        Me.txtFacilityManualTax.Name = "txtFacilityManualTax"
        Me.txtFacilityManualTax.Size = New System.Drawing.Size(35, 20)
        Me.txtFacilityManualTax.TabIndex = 40
        Me.txtFacilityManualTax.Text = "0%"
        Me.txtFacilityManualTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFacilityManualTax.Visible = false
        '
        'lblFacilityManualTax
        '
        Me.lblFacilityManualTax.AutoSize = true
        Me.lblFacilityManualTax.Location = New System.Drawing.Point(166, 90)
        Me.lblFacilityManualTax.Name = "lblFacilityManualTax"
        Me.lblFacilityManualTax.Size = New System.Drawing.Size(28, 13)
        Me.lblFacilityManualTax.TabIndex = 39
        Me.lblFacilityManualTax.Text = "Tax:"
        Me.lblFacilityManualTax.Visible = false
        '
        'lblInclude
        '
        Me.lblInclude.AutoSize = true
        Me.lblInclude.Location = New System.Drawing.Point(134, 7)
        Me.lblInclude.Name = "lblInclude"
        Me.lblInclude.Size = New System.Drawing.Size(45, 13)
        Me.lblInclude.TabIndex = 25
        Me.lblInclude.Text = "Include:"
        '
        'chkFacilityIncludeUsage
        '
        Me.chkFacilityIncludeUsage.AutoSize = true
        Me.chkFacilityIncludeUsage.Location = New System.Drawing.Point(137, 23)
        Me.chkFacilityIncludeUsage.Name = "chkFacilityIncludeUsage"
        Me.chkFacilityIncludeUsage.Size = New System.Drawing.Size(57, 17)
        Me.chkFacilityIncludeUsage.TabIndex = 26
        Me.chkFacilityIncludeUsage.Text = "Usage"
        Me.chkFacilityIncludeUsage.UseVisualStyleBackColor = true
        '
        'chkFacilityIncludeTime
        '
        Me.chkFacilityIncludeTime.AutoSize = true
        Me.chkFacilityIncludeTime.Location = New System.Drawing.Point(241, 23)
        Me.chkFacilityIncludeTime.Name = "chkFacilityIncludeTime"
        Me.chkFacilityIncludeTime.Size = New System.Drawing.Size(49, 17)
        Me.chkFacilityIncludeTime.TabIndex = 28
        Me.chkFacilityIncludeTime.Text = "Time"
        Me.chkFacilityIncludeTime.UseVisualStyleBackColor = true
        '
        'lblFacilityDefault
        '
        Me.lblFacilityDefault.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFacilityDefault.Location = New System.Drawing.Point(216, 1)
        Me.lblFacilityDefault.Name = "lblFacilityDefault"
        Me.lblFacilityDefault.Size = New System.Drawing.Size(79, 20)
        Me.lblFacilityDefault.TabIndex = 29
        Me.lblFacilityDefault.Text = "Default Facility"
        Me.lblFacilityDefault.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkFacilityIncludeCost
        '
        Me.chkFacilityIncludeCost.AutoSize = true
        Me.chkFacilityIncludeCost.Location = New System.Drawing.Point(194, 23)
        Me.chkFacilityIncludeCost.Name = "chkFacilityIncludeCost"
        Me.chkFacilityIncludeCost.Size = New System.Drawing.Size(47, 17)
        Me.chkFacilityIncludeCost.TabIndex = 27
        Me.chkFacilityIncludeCost.Text = "Cost"
        Me.chkFacilityIncludeCost.UseVisualStyleBackColor = true
        '
        'btnFacilitySave
        '
        Me.btnFacilitySave.Enabled = false
        Me.btnFacilitySave.Location = New System.Drawing.Point(238, 86)
        Me.btnFacilitySave.Name = "btnFacilitySave"
        Me.btnFacilitySave.Size = New System.Drawing.Size(56, 22)
        Me.btnFacilitySave.TabIndex = 41
        Me.btnFacilitySave.Text = "Save"
        Me.btnFacilitySave.UseVisualStyleBackColor = true
        '
        'txtFacilityManualTE
        '
        Me.txtFacilityManualTE.Location = New System.Drawing.Point(98, 87)
        Me.txtFacilityManualTE.MaxLength = 5
        Me.txtFacilityManualTE.Name = "txtFacilityManualTE"
        Me.txtFacilityManualTE.Size = New System.Drawing.Size(35, 20)
        Me.txtFacilityManualTE.TabIndex = 38
        Me.txtFacilityManualTE.Text = "0%"
        Me.txtFacilityManualTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFacilityManualTE.Visible = false
        '
        'txtFacilityManualME
        '
        Me.txtFacilityManualME.Location = New System.Drawing.Point(37, 87)
        Me.txtFacilityManualME.MaxLength = 5
        Me.txtFacilityManualME.Name = "txtFacilityManualME"
        Me.txtFacilityManualME.Size = New System.Drawing.Size(35, 20)
        Me.txtFacilityManualME.TabIndex = 36
        Me.txtFacilityManualME.Text = "0%"
        Me.txtFacilityManualME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFacilityManualME.Visible = false
        '
        'cmbFacilityorArray
        '
        Me.cmbFacilityorArray.FormattingEnabled = true
        Me.cmbFacilityorArray.ItemHeight = 13
        Me.cmbFacilityorArray.Location = New System.Drawing.Point(3, 63)
        Me.cmbFacilityorArray.Name = "cmbFacilityorArray"
        Me.cmbFacilityorArray.Size = New System.Drawing.Size(291, 21)
        Me.cmbFacilityorArray.TabIndex = 33
        Me.cmbFacilityorArray.Text = "Select Facility / Array"
        '
        'cmbFacilitySystem
        '
        Me.cmbFacilitySystem.FormattingEnabled = true
        Me.cmbFacilitySystem.Location = New System.Drawing.Point(137, 40)
        Me.cmbFacilitySystem.Name = "cmbFacilitySystem"
        Me.cmbFacilitySystem.Size = New System.Drawing.Size(157, 21)
        Me.cmbFacilitySystem.TabIndex = 32
        Me.cmbFacilitySystem.Text = "Select System"
        '
        'cmbFacilityRegion
        '
        Me.cmbFacilityRegion.FormattingEnabled = true
        Me.cmbFacilityRegion.Location = New System.Drawing.Point(3, 40)
        Me.cmbFacilityRegion.Name = "cmbFacilityRegion"
        Me.cmbFacilityRegion.Size = New System.Drawing.Size(130, 21)
        Me.cmbFacilityRegion.TabIndex = 31
        Me.cmbFacilityRegion.Text = "Select Region"
        '
        'lblFacilityManualTE
        '
        Me.lblFacilityManualTE.AutoSize = true
        Me.lblFacilityManualTE.Location = New System.Drawing.Point(75, 90)
        Me.lblFacilityManualTE.Name = "lblFacilityManualTE"
        Me.lblFacilityManualTE.Size = New System.Drawing.Size(24, 13)
        Me.lblFacilityManualTE.TabIndex = 37
        Me.lblFacilityManualTE.Text = "TE:"
        Me.lblFacilityManualTE.Visible = false
        '
        'lblFacilityLocation
        '
        Me.lblFacilityLocation.AutoSize = true
        Me.lblFacilityLocation.Location = New System.Drawing.Point(1, 25)
        Me.lblFacilityLocation.Name = "lblFacilityLocation"
        Me.lblFacilityLocation.Size = New System.Drawing.Size(51, 13)
        Me.lblFacilityLocation.TabIndex = 30
        Me.lblFacilityLocation.Text = "Location:"
        '
        'lblFacilityType
        '
        Me.lblFacilityType.AutoSize = true
        Me.lblFacilityType.Location = New System.Drawing.Point(1, 7)
        Me.lblFacilityType.Name = "lblFacilityType"
        Me.lblFacilityType.Size = New System.Drawing.Size(69, 13)
        Me.lblFacilityType.TabIndex = 23
        Me.lblFacilityType.Text = "Facility Type:"
        '
        'cmbFacilityType
        '
        Me.cmbFacilityType.Enabled = false
        Me.cmbFacilityType.FormattingEnabled = true
        Me.cmbFacilityType.ItemHeight = 13
        Me.cmbFacilityType.Items.AddRange(New Object() {"NPC Station", "Outpost", "POS"})
        Me.cmbFacilityType.Location = New System.Drawing.Point(71, 4)
        Me.cmbFacilityType.Name = "cmbFacilityType"
        Me.cmbFacilityType.Size = New System.Drawing.Size(62, 21)
        Me.cmbFacilityType.TabIndex = 24
        Me.cmbFacilityType.Text = "Type"
        '
        'lblFacilityManualME
        '
        Me.lblFacilityManualME.AutoSize = true
        Me.lblFacilityManualME.Location = New System.Drawing.Point(12, 90)
        Me.lblFacilityManualME.Name = "lblFacilityManualME"
        Me.lblFacilityManualME.Size = New System.Drawing.Size(26, 13)
        Me.lblFacilityManualME.TabIndex = 35
        Me.lblFacilityManualME.Text = "ME:"
        Me.lblFacilityManualME.Visible = false
        '
        'lblFacilityBonus
        '
        Me.lblFacilityBonus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFacilityBonus.Location = New System.Drawing.Point(4, 87)
        Me.lblFacilityBonus.Name = "lblFacilityBonus"
        Me.lblFacilityBonus.Size = New System.Drawing.Size(159, 20)
        Me.lblFacilityBonus.TabIndex = 34
        Me.lblFacilityBonus.Text = " Bonus: -20% ME; -20% TE"
        Me.lblFacilityBonus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFacilityBonus.Visible = false
        '
        'chkComponentsFacility
        '
        Me.chkComponentsFacility.AutoSize = true
        Me.chkComponentsFacility.Location = New System.Drawing.Point(137, 6)
        Me.chkComponentsFacility.Name = "chkComponentsFacility"
        Me.chkComponentsFacility.Size = New System.Drawing.Size(72, 17)
        Me.chkComponentsFacility.TabIndex = 42
        Me.chkComponentsFacility.Text = "Cap Parts"
        Me.chkComponentsFacility.UseVisualStyleBackColor = true
        '
        'lblFacilityTaxRate
        '
        Me.lblFacilityTaxRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFacilityTaxRate.Location = New System.Drawing.Point(169, 87)
        Me.lblFacilityTaxRate.Name = "lblFacilityTaxRate"
        Me.lblFacilityTaxRate.Size = New System.Drawing.Size(65, 20)
        Me.lblFacilityTaxRate.TabIndex = 43
        Me.lblFacilityTaxRate.Text = "Tax: 50.0%"
        Me.lblFacilityTaxRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFacilityTaxRate.Visible = false
        '
        'cmbFuelBlocks
        '
        Me.cmbFuelBlocks.FormattingEnabled = true
        Me.cmbFuelBlocks.Items.AddRange(New Object() {"All", "Ammunition", "Component"})
        Me.cmbFuelBlocks.Location = New System.Drawing.Point(115, 182)
        Me.cmbFuelBlocks.Name = "cmbFuelBlocks"
        Me.cmbFuelBlocks.Size = New System.Drawing.Size(78, 21)
        Me.cmbFuelBlocks.TabIndex = 52
        Me.cmbFuelBlocks.Text = "All"
        '
        'cmbModules
        '
        Me.cmbModules.FormattingEnabled = true
        Me.cmbModules.Items.AddRange(New Object() {"All", "Equipment", "Rapid"})
        Me.cmbModules.Location = New System.Drawing.Point(35, 182)
        Me.cmbModules.Name = "cmbModules"
        Me.cmbModules.Size = New System.Drawing.Size(78, 21)
        Me.cmbModules.TabIndex = 51
        Me.cmbModules.Text = "All"
        '
        'lblFuelBlocks
        '
        Me.lblFuelBlocks.Location = New System.Drawing.Point(112, 155)
        Me.lblFuelBlocks.Name = "lblFuelBlocks"
        Me.lblFuelBlocks.Size = New System.Drawing.Size(81, 30)
        Me.lblFuelBlocks.TabIndex = 55
        Me.lblFuelBlocks.Text = "Fuel Block Assembly Array:"
        '
        'lblModules
        '
        Me.lblModules.Location = New System.Drawing.Point(32, 155)
        Me.lblModules.Name = "lblModules"
        Me.lblModules.Size = New System.Drawing.Size(81, 30)
        Me.lblModules.TabIndex = 54
        Me.lblModules.Text = "Module Assembly Array:"
        '
        'cmbLargeShips
        '
        Me.cmbLargeShips.FormattingEnabled = true
        Me.cmbLargeShips.Items.AddRange(New Object() {"All", "Capital", "Large"})
        Me.cmbLargeShips.Location = New System.Drawing.Point(195, 182)
        Me.cmbLargeShips.Name = "cmbLargeShips"
        Me.cmbLargeShips.Size = New System.Drawing.Size(66, 21)
        Me.cmbLargeShips.TabIndex = 53
        Me.cmbLargeShips.Text = "All"
        '
        'lblLargeShips
        '
        Me.lblLargeShips.Location = New System.Drawing.Point(193, 155)
        Me.lblLargeShips.Name = "lblLargeShips"
        Me.lblLargeShips.Size = New System.Drawing.Size(110, 30)
        Me.lblLargeShips.TabIndex = 50
        Me.lblLargeShips.Text = "Large Ships Assembly Array:"
        '
        'IPHIndustryFacility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmbFuelBlocks)
        Me.Controls.Add(Me.cmbModules)
        Me.Controls.Add(Me.lblFuelBlocks)
        Me.Controls.Add(Me.lblModules)
        Me.Controls.Add(Me.cmbLargeShips)
        Me.Controls.Add(Me.lblLargeShips)
        Me.Controls.Add(Me.chkComponentsFacility)
        Me.Controls.Add(Me.txtFacilityManualTax)
        Me.Controls.Add(Me.lblFacilityManualTax)
        Me.Controls.Add(Me.lblInclude)
        Me.Controls.Add(Me.chkFacilityIncludeUsage)
        Me.Controls.Add(Me.chkFacilityIncludeTime)
        Me.Controls.Add(Me.lblFacilityDefault)
        Me.Controls.Add(Me.chkFacilityIncludeCost)
        Me.Controls.Add(Me.btnFacilitySave)
        Me.Controls.Add(Me.txtFacilityManualTE)
        Me.Controls.Add(Me.txtFacilityManualME)
        Me.Controls.Add(Me.cmbFacilityorArray)
        Me.Controls.Add(Me.cmbFacilitySystem)
        Me.Controls.Add(Me.cmbFacilityRegion)
        Me.Controls.Add(Me.lblFacilityManualTE)
        Me.Controls.Add(Me.lblFacilityLocation)
        Me.Controls.Add(Me.lblFacilityType)
        Me.Controls.Add(Me.cmbFacilityType)
        Me.Controls.Add(Me.lblFacilityManualME)
        Me.Controls.Add(Me.lblFacilityBonus)
        Me.Controls.Add(Me.lblFacilityTaxRate)
        Me.Name = "IPHIndustryFacility"
        Me.Size = New System.Drawing.Size(391, 255)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents txtFacilityManualTax As System.Windows.Forms.TextBox
    Friend WithEvents lblFacilityManualTax As System.Windows.Forms.Label
    Friend WithEvents lblInclude As System.Windows.Forms.Label
    Friend WithEvents chkFacilityIncludeUsage As System.Windows.Forms.CheckBox
    Friend WithEvents chkFacilityIncludeTime As System.Windows.Forms.CheckBox
    Friend WithEvents lblFacilityDefault As System.Windows.Forms.Label
    Friend WithEvents chkFacilityIncludeCost As System.Windows.Forms.CheckBox
    Friend WithEvents btnFacilitySave As System.Windows.Forms.Button
    Friend WithEvents txtFacilityManualTE As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityManualME As System.Windows.Forms.TextBox
    Friend WithEvents cmbFacilityorArray As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFacilitySystem As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFacilityRegion As System.Windows.Forms.ComboBox
    Friend WithEvents lblFacilityManualTE As System.Windows.Forms.Label
    Friend WithEvents lblFacilityLocation As System.Windows.Forms.Label
    Friend WithEvents lblFacilityType As System.Windows.Forms.Label
    Friend WithEvents cmbFacilityType As System.Windows.Forms.ComboBox
    Friend WithEvents lblFacilityManualME As System.Windows.Forms.Label
    Friend WithEvents lblFacilityBonus As System.Windows.Forms.Label
    Friend WithEvents chkComponentsFacility As System.Windows.Forms.CheckBox
    Friend WithEvents lblFacilityTaxRate As System.Windows.Forms.Label
    Friend WithEvents cmbFuelBlocks As System.Windows.Forms.ComboBox
    Friend WithEvents cmbModules As System.Windows.Forms.ComboBox
    Friend WithEvents lblFuelBlocks As System.Windows.Forms.Label
    Friend WithEvents lblModules As System.Windows.Forms.Label
    Friend WithEvents cmbLargeShips As System.Windows.Forms.ComboBox
    Friend WithEvents lblLargeShips As System.Windows.Forms.Label

End Class
