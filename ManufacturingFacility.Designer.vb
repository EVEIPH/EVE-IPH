<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManufacturingFacility
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
        Me.lblInclude = New System.Windows.Forms.Label()
        Me.chkFacilityIncludeUsage = New System.Windows.Forms.CheckBox()
        Me.chkFacilityIncludeTime = New System.Windows.Forms.CheckBox()
        Me.lblFacilityDefault = New System.Windows.Forms.Label()
        Me.chkFacilityIncludeCost = New System.Windows.Forms.CheckBox()
        Me.cmbFacilityorArray = New System.Windows.Forms.ComboBox()
        Me.cmbFacilitySystem = New System.Windows.Forms.ComboBox()
        Me.cmbFacilityRegion = New System.Windows.Forms.ComboBox()
        Me.lblFacilityLocation = New System.Windows.Forms.Label()
        Me.lblFacilityType = New System.Windows.Forms.Label()
        Me.cmbFacilityType = New System.Windows.Forms.ComboBox()
        Me.cmbLargeShips = New System.Windows.Forms.ComboBox()
        Me.lblLargeShips = New System.Windows.Forms.Label()
        Me.cmbFuelBlocks = New System.Windows.Forms.ComboBox()
        Me.lblFuelBlocks = New System.Windows.Forms.Label()
        Me.cmbModules = New System.Windows.Forms.ComboBox()
        Me.lblModules = New System.Windows.Forms.Label()
        Me.btnFacilityFitting = New System.Windows.Forms.Button()
        Me.txtFacilityManualTax = New System.Windows.Forms.TextBox()
        Me.lblFacilityManualTax = New System.Windows.Forms.Label()
        Me.btnFacilitySave = New System.Windows.Forms.Button()
        Me.txtFacilityManualTE = New System.Windows.Forms.TextBox()
        Me.txtFacilityManualME = New System.Windows.Forms.TextBox()
        Me.lblFacilityManualTE = New System.Windows.Forms.Label()
        Me.lblFacilityManualME = New System.Windows.Forms.Label()
        Me.lblCostBonus = New System.Windows.Forms.Label()
        Me.txtCostBonus = New System.Windows.Forms.TextBox()
        Me.lblFWUpgrade = New System.Windows.Forms.Label()
        Me.cmbFWUpgrade = New System.Windows.Forms.ComboBox()
        Me.chkFacilityToggle = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblInclude
        '
        Me.lblInclude.AutoSize = True
        Me.lblInclude.Location = New System.Drawing.Point(136, 4)
        Me.lblInclude.Name = "lblInclude"
        Me.lblInclude.Size = New System.Drawing.Size(45, 13)
        Me.lblInclude.TabIndex = 25
        Me.lblInclude.Text = "Include:"
        '
        'chkFacilityIncludeUsage
        '
        Me.chkFacilityIncludeUsage.AutoSize = True
        Me.chkFacilityIncludeUsage.Location = New System.Drawing.Point(139, 20)
        Me.chkFacilityIncludeUsage.Name = "chkFacilityIncludeUsage"
        Me.chkFacilityIncludeUsage.Size = New System.Drawing.Size(57, 17)
        Me.chkFacilityIncludeUsage.TabIndex = 26
        Me.chkFacilityIncludeUsage.Text = "Usage"
        Me.chkFacilityIncludeUsage.UseVisualStyleBackColor = True
        '
        'chkFacilityIncludeTime
        '
        Me.chkFacilityIncludeTime.AutoSize = True
        Me.chkFacilityIncludeTime.Location = New System.Drawing.Point(243, 20)
        Me.chkFacilityIncludeTime.Name = "chkFacilityIncludeTime"
        Me.chkFacilityIncludeTime.Size = New System.Drawing.Size(49, 17)
        Me.chkFacilityIncludeTime.TabIndex = 28
        Me.chkFacilityIncludeTime.Text = "Time"
        Me.chkFacilityIncludeTime.UseVisualStyleBackColor = True
        '
        'lblFacilityDefault
        '
        Me.lblFacilityDefault.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFacilityDefault.Location = New System.Drawing.Point(218, -2)
        Me.lblFacilityDefault.Name = "lblFacilityDefault"
        Me.lblFacilityDefault.Size = New System.Drawing.Size(79, 20)
        Me.lblFacilityDefault.TabIndex = 29
        Me.lblFacilityDefault.Text = "Default Facility"
        Me.lblFacilityDefault.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkFacilityIncludeCost
        '
        Me.chkFacilityIncludeCost.AutoSize = True
        Me.chkFacilityIncludeCost.Location = New System.Drawing.Point(196, 20)
        Me.chkFacilityIncludeCost.Name = "chkFacilityIncludeCost"
        Me.chkFacilityIncludeCost.Size = New System.Drawing.Size(47, 17)
        Me.chkFacilityIncludeCost.TabIndex = 27
        Me.chkFacilityIncludeCost.Text = "Cost"
        Me.chkFacilityIncludeCost.UseVisualStyleBackColor = True
        '
        'cmbFacilityorArray
        '
        Me.cmbFacilityorArray.FormattingEnabled = True
        Me.cmbFacilityorArray.ItemHeight = 13
        Me.cmbFacilityorArray.Location = New System.Drawing.Point(5, 60)
        Me.cmbFacilityorArray.Name = "cmbFacilityorArray"
        Me.cmbFacilityorArray.Size = New System.Drawing.Size(291, 21)
        Me.cmbFacilityorArray.TabIndex = 33
        Me.cmbFacilityorArray.Text = "Select Facility / Array"
        '
        'cmbFacilitySystem
        '
        Me.cmbFacilitySystem.FormattingEnabled = True
        Me.cmbFacilitySystem.Location = New System.Drawing.Point(139, 37)
        Me.cmbFacilitySystem.Name = "cmbFacilitySystem"
        Me.cmbFacilitySystem.Size = New System.Drawing.Size(157, 21)
        Me.cmbFacilitySystem.TabIndex = 32
        Me.cmbFacilitySystem.Text = "Select System"
        '
        'cmbFacilityRegion
        '
        Me.cmbFacilityRegion.FormattingEnabled = True
        Me.cmbFacilityRegion.Location = New System.Drawing.Point(5, 37)
        Me.cmbFacilityRegion.Name = "cmbFacilityRegion"
        Me.cmbFacilityRegion.Size = New System.Drawing.Size(130, 21)
        Me.cmbFacilityRegion.TabIndex = 31
        Me.cmbFacilityRegion.Text = "Select Region"
        '
        'lblFacilityLocation
        '
        Me.lblFacilityLocation.AutoSize = True
        Me.lblFacilityLocation.Location = New System.Drawing.Point(3, 22)
        Me.lblFacilityLocation.Name = "lblFacilityLocation"
        Me.lblFacilityLocation.Size = New System.Drawing.Size(51, 13)
        Me.lblFacilityLocation.TabIndex = 30
        Me.lblFacilityLocation.Text = "Location:"
        '
        'lblFacilityType
        '
        Me.lblFacilityType.AutoSize = True
        Me.lblFacilityType.Location = New System.Drawing.Point(3, 4)
        Me.lblFacilityType.Name = "lblFacilityType"
        Me.lblFacilityType.Size = New System.Drawing.Size(69, 13)
        Me.lblFacilityType.TabIndex = 23
        Me.lblFacilityType.Text = "Facility Type:"
        '
        'cmbFacilityType
        '
        Me.cmbFacilityType.Enabled = False
        Me.cmbFacilityType.FormattingEnabled = True
        Me.cmbFacilityType.ItemHeight = 13
        Me.cmbFacilityType.Items.AddRange(New Object() {"NPC Station", "Outpost", "POS"})
        Me.cmbFacilityType.Location = New System.Drawing.Point(73, 1)
        Me.cmbFacilityType.Name = "cmbFacilityType"
        Me.cmbFacilityType.Size = New System.Drawing.Size(62, 21)
        Me.cmbFacilityType.TabIndex = 24
        Me.cmbFacilityType.Text = "Type"
        '
        'cmbLargeShips
        '
        Me.cmbLargeShips.FormattingEnabled = True
        Me.cmbLargeShips.Items.AddRange(New Object() {"All", "Capital", "Large"})
        Me.cmbLargeShips.Location = New System.Drawing.Point(164, 85)
        Me.cmbLargeShips.Name = "cmbLargeShips"
        Me.cmbLargeShips.Size = New System.Drawing.Size(66, 21)
        Me.cmbLargeShips.TabIndex = 46
        Me.cmbLargeShips.Text = "All"
        '
        'lblLargeShips
        '
        Me.lblLargeShips.Location = New System.Drawing.Point(162, 57)
        Me.lblLargeShips.Name = "lblLargeShips"
        Me.lblLargeShips.Size = New System.Drawing.Size(110, 30)
        Me.lblLargeShips.TabIndex = 43
        Me.lblLargeShips.Text = "Large Ships Assembly Array:"
        '
        'cmbFuelBlocks
        '
        Me.cmbFuelBlocks.FormattingEnabled = True
        Me.cmbFuelBlocks.Items.AddRange(New Object() {"All", "Ammunition", "Component"})
        Me.cmbFuelBlocks.Location = New System.Drawing.Point(84, 85)
        Me.cmbFuelBlocks.Name = "cmbFuelBlocks"
        Me.cmbFuelBlocks.Size = New System.Drawing.Size(78, 21)
        Me.cmbFuelBlocks.TabIndex = 45
        Me.cmbFuelBlocks.Text = "All"
        '
        'lblFuelBlocks
        '
        Me.lblFuelBlocks.Location = New System.Drawing.Point(82, 57)
        Me.lblFuelBlocks.Name = "lblFuelBlocks"
        Me.lblFuelBlocks.Size = New System.Drawing.Size(81, 30)
        Me.lblFuelBlocks.TabIndex = 42
        Me.lblFuelBlocks.Text = "Fuel Block Assembly Array:"
        '
        'cmbModules
        '
        Me.cmbModules.FormattingEnabled = True
        Me.cmbModules.Items.AddRange(New Object() {"All", "Equipment", "Rapid"})
        Me.cmbModules.Location = New System.Drawing.Point(4, 85)
        Me.cmbModules.Name = "cmbModules"
        Me.cmbModules.Size = New System.Drawing.Size(78, 21)
        Me.cmbModules.TabIndex = 44
        Me.cmbModules.Text = "All"
        '
        'lblModules
        '
        Me.lblModules.Location = New System.Drawing.Point(2, 57)
        Me.lblModules.Name = "lblModules"
        Me.lblModules.Size = New System.Drawing.Size(81, 30)
        Me.lblModules.TabIndex = 41
        Me.lblModules.Text = "Module Assembly Array:"
        '
        'btnFacilityFitting
        '
        Me.btnFacilityFitting.Location = New System.Drawing.Point(213, 83)
        Me.btnFacilityFitting.Name = "btnFacilityFitting"
        Me.btnFacilityFitting.Size = New System.Drawing.Size(43, 22)
        Me.btnFacilityFitting.TabIndex = 92
        Me.btnFacilityFitting.Text = "Fitting"
        Me.btnFacilityFitting.UseVisualStyleBackColor = True
        Me.btnFacilityFitting.Visible = False
        '
        'txtFacilityManualTax
        '
        Me.txtFacilityManualTax.Location = New System.Drawing.Point(115, 105)
        Me.txtFacilityManualTax.MaxLength = 5
        Me.txtFacilityManualTax.Name = "txtFacilityManualTax"
        Me.txtFacilityManualTax.Size = New System.Drawing.Size(38, 20)
        Me.txtFacilityManualTax.TabIndex = 90
        Me.txtFacilityManualTax.Text = "-99.9%"
        Me.txtFacilityManualTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFacilityManualTax
        '
        Me.lblFacilityManualTax.AutoSize = True
        Me.lblFacilityManualTax.Location = New System.Drawing.Point(81, 109)
        Me.lblFacilityManualTax.Name = "lblFacilityManualTax"
        Me.lblFacilityManualTax.Size = New System.Drawing.Size(28, 13)
        Me.lblFacilityManualTax.TabIndex = 89
        Me.lblFacilityManualTax.Text = "Tax:"
        '
        'btnFacilitySave
        '
        Me.btnFacilitySave.Enabled = False
        Me.btnFacilitySave.Location = New System.Drawing.Point(257, 83)
        Me.btnFacilitySave.Name = "btnFacilitySave"
        Me.btnFacilitySave.Size = New System.Drawing.Size(40, 22)
        Me.btnFacilitySave.TabIndex = 91
        Me.btnFacilitySave.Text = "Save"
        Me.btnFacilitySave.UseVisualStyleBackColor = True
        '
        'txtFacilityManualTE
        '
        Me.txtFacilityManualTE.Location = New System.Drawing.Point(115, 84)
        Me.txtFacilityManualTE.MaxLength = 5
        Me.txtFacilityManualTE.Name = "txtFacilityManualTE"
        Me.txtFacilityManualTE.Size = New System.Drawing.Size(38, 20)
        Me.txtFacilityManualTE.TabIndex = 88
        Me.txtFacilityManualTE.Text = "-99.9%"
        Me.txtFacilityManualTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFacilityManualME
        '
        Me.txtFacilityManualME.Location = New System.Drawing.Point(38, 84)
        Me.txtFacilityManualME.MaxLength = 5
        Me.txtFacilityManualME.Name = "txtFacilityManualME"
        Me.txtFacilityManualME.Size = New System.Drawing.Size(38, 20)
        Me.txtFacilityManualME.TabIndex = 86
        Me.txtFacilityManualME.Text = "-99.9%"
        Me.txtFacilityManualME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFacilityManualTE
        '
        Me.lblFacilityManualTE.AutoSize = True
        Me.lblFacilityManualTE.Location = New System.Drawing.Point(85, 88)
        Me.lblFacilityManualTE.Name = "lblFacilityManualTE"
        Me.lblFacilityManualTE.Size = New System.Drawing.Size(24, 13)
        Me.lblFacilityManualTE.TabIndex = 87
        Me.lblFacilityManualTE.Text = "TE:"
        '
        'lblFacilityManualME
        '
        Me.lblFacilityManualME.AutoSize = True
        Me.lblFacilityManualME.Location = New System.Drawing.Point(6, 88)
        Me.lblFacilityManualME.Name = "lblFacilityManualME"
        Me.lblFacilityManualME.Size = New System.Drawing.Size(26, 13)
        Me.lblFacilityManualME.TabIndex = 85
        Me.lblFacilityManualME.Text = "ME:"
        '
        'lblCostBonus
        '
        Me.lblCostBonus.AutoSize = True
        Me.lblCostBonus.Location = New System.Drawing.Point(6, 109)
        Me.lblCostBonus.Name = "lblCostBonus"
        Me.lblCostBonus.Size = New System.Drawing.Size(31, 13)
        Me.lblCostBonus.TabIndex = 93
        Me.lblCostBonus.Text = "Cost:"
        '
        'txtCostBonus
        '
        Me.txtCostBonus.Location = New System.Drawing.Point(38, 105)
        Me.txtCostBonus.MaxLength = 5
        Me.txtCostBonus.Name = "txtCostBonus"
        Me.txtCostBonus.Size = New System.Drawing.Size(38, 20)
        Me.txtCostBonus.TabIndex = 94
        Me.txtCostBonus.Text = "-99.9%"
        Me.txtCostBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFWUpgrade
        '
        Me.lblFWUpgrade.AutoSize = True
        Me.lblFWUpgrade.Location = New System.Drawing.Point(155, 109)
        Me.lblFWUpgrade.Name = "lblFWUpgrade"
        Me.lblFWUpgrade.Size = New System.Drawing.Size(71, 13)
        Me.lblFWUpgrade.TabIndex = 96
        Me.lblFWUpgrade.Text = "FW Upgrade:"
        '
        'cmbFWUpgrade
        '
        Me.cmbFWUpgrade.FormattingEnabled = True
        Me.cmbFWUpgrade.Items.AddRange(New Object() {"None", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5"})
        Me.cmbFWUpgrade.Location = New System.Drawing.Point(230, 106)
        Me.cmbFWUpgrade.Name = "cmbFWUpgrade"
        Me.cmbFWUpgrade.Size = New System.Drawing.Size(66, 21)
        Me.cmbFWUpgrade.TabIndex = 95
        Me.cmbFWUpgrade.Text = "Level 5"
        '
        'chkFacilityToggle
        '
        Me.chkFacilityToggle.AutoSize = True
        Me.chkFacilityToggle.Location = New System.Drawing.Point(139, 3)
        Me.chkFacilityToggle.Name = "chkFacilityToggle"
        Me.chkFacilityToggle.Size = New System.Drawing.Size(79, 17)
        Me.chkFacilityToggle.TabIndex = 97
        Me.chkFacilityToggle.Text = "Dessy/Cap"
        Me.chkFacilityToggle.UseVisualStyleBackColor = True
        '
        'ManufacturingFacility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chkFacilityToggle)
        Me.Controls.Add(Me.lblFWUpgrade)
        Me.Controls.Add(Me.cmbFWUpgrade)
        Me.Controls.Add(Me.txtCostBonus)
        Me.Controls.Add(Me.lblCostBonus)
        Me.Controls.Add(Me.btnFacilityFitting)
        Me.Controls.Add(Me.txtFacilityManualTax)
        Me.Controls.Add(Me.lblFacilityManualTax)
        Me.Controls.Add(Me.btnFacilitySave)
        Me.Controls.Add(Me.txtFacilityManualTE)
        Me.Controls.Add(Me.txtFacilityManualME)
        Me.Controls.Add(Me.lblFacilityManualTE)
        Me.Controls.Add(Me.lblFacilityManualME)
        Me.Controls.Add(Me.lblInclude)
        Me.Controls.Add(Me.chkFacilityIncludeUsage)
        Me.Controls.Add(Me.chkFacilityIncludeTime)
        Me.Controls.Add(Me.lblFacilityDefault)
        Me.Controls.Add(Me.chkFacilityIncludeCost)
        Me.Controls.Add(Me.cmbFacilityorArray)
        Me.Controls.Add(Me.cmbFacilitySystem)
        Me.Controls.Add(Me.cmbFacilityRegion)
        Me.Controls.Add(Me.lblFacilityLocation)
        Me.Controls.Add(Me.lblFacilityType)
        Me.Controls.Add(Me.cmbFacilityType)
        Me.Controls.Add(Me.cmbLargeShips)
        Me.Controls.Add(Me.lblLargeShips)
        Me.Controls.Add(Me.cmbFuelBlocks)
        Me.Controls.Add(Me.lblFuelBlocks)
        Me.Controls.Add(Me.cmbModules)
        Me.Controls.Add(Me.lblModules)
        Me.Name = "ManufacturingFacility"
        Me.Size = New System.Drawing.Size(302, 128)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInclude As Label
    Friend WithEvents chkFacilityIncludeUsage As CheckBox
    Friend WithEvents chkFacilityIncludeTime As CheckBox
    Friend WithEvents lblFacilityDefault As Label
    Friend WithEvents chkFacilityIncludeCost As CheckBox
    Friend WithEvents cmbFacilityorArray As ComboBox
    Friend WithEvents cmbFacilitySystem As ComboBox
    Friend WithEvents cmbFacilityRegion As ComboBox
    Friend WithEvents lblFacilityLocation As Label
    Friend WithEvents lblFacilityType As Label
    Friend WithEvents cmbFacilityType As ComboBox
    Friend WithEvents cmbLargeShips As ComboBox
    Friend WithEvents lblLargeShips As Label
    Friend WithEvents cmbFuelBlocks As ComboBox
    Friend WithEvents lblFuelBlocks As Label
    Friend WithEvents cmbModules As ComboBox
    Friend WithEvents lblModules As Label
    Friend WithEvents btnFacilityFitting As Button
    Friend WithEvents txtFacilityManualTax As TextBox
    Friend WithEvents lblFacilityManualTax As Label
    Friend WithEvents btnFacilitySave As Button
    Friend WithEvents txtFacilityManualTE As TextBox
    Friend WithEvents txtFacilityManualME As TextBox
    Friend WithEvents lblFacilityManualTE As Label
    Friend WithEvents lblFacilityManualME As Label
    Friend WithEvents lblCostBonus As Label
    Friend WithEvents txtCostBonus As TextBox
    Friend WithEvents lblFWUpgrade As Label
    Friend WithEvents cmbFWUpgrade As ComboBox
    Friend WithEvents chkFacilityToggle As CheckBox
End Class
