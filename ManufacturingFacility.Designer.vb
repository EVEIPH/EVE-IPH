<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManufacturingFacility
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblInclude = New System.Windows.Forms.Label()
        Me.chkFacilityIncludeUsage = New System.Windows.Forms.CheckBox()
        Me.chkFacilityIncludeTime = New System.Windows.Forms.CheckBox()
        Me.lblFacilityDefault = New System.Windows.Forms.Label()
        Me.chkFacilityIncludeCost = New System.Windows.Forms.CheckBox()
        Me.cmbFacility = New System.Windows.Forms.ComboBox()
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
        Me.lblFacilityManualCost = New System.Windows.Forms.Label()
        Me.txtFacilityManualCost = New System.Windows.Forms.TextBox()
        Me.lblFacilityFWUpgrade = New System.Windows.Forms.Label()
        Me.cmbFacilityFWUpgrade = New System.Windows.Forms.ComboBox()
        Me.chkFacilityToggle = New System.Windows.Forms.CheckBox()
        Me.lblFacilityUsage = New System.Windows.Forms.Label()
        Me.cmbFacilityActivities = New System.Windows.Forms.ComboBox()
        Me.lblFacilityActivity = New System.Windows.Forms.Label()
        Me.mainToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'lblInclude
        '
        Me.lblInclude.AutoSize = True
        Me.lblInclude.Location = New System.Drawing.Point(136, 4)
        Me.lblInclude.Name = "lblInclude"
        Me.lblInclude.Size = New System.Drawing.Size(45, 13)
        Me.lblInclude.TabIndex = 6
        Me.lblInclude.Text = "Include:"
        '
        'chkFacilityIncludeUsage
        '
        Me.chkFacilityIncludeUsage.AutoSize = True
        Me.chkFacilityIncludeUsage.Location = New System.Drawing.Point(139, 20)
        Me.chkFacilityIncludeUsage.Name = "chkFacilityIncludeUsage"
        Me.chkFacilityIncludeUsage.Size = New System.Drawing.Size(57, 17)
        Me.chkFacilityIncludeUsage.TabIndex = 7
        Me.chkFacilityIncludeUsage.Text = "Usage"
        Me.chkFacilityIncludeUsage.UseVisualStyleBackColor = True
        '
        'chkFacilityIncludeTime
        '
        Me.chkFacilityIncludeTime.AutoSize = True
        Me.chkFacilityIncludeTime.Location = New System.Drawing.Point(243, 20)
        Me.chkFacilityIncludeTime.Name = "chkFacilityIncludeTime"
        Me.chkFacilityIncludeTime.Size = New System.Drawing.Size(49, 17)
        Me.chkFacilityIncludeTime.TabIndex = 10
        Me.chkFacilityIncludeTime.Text = "Time"
        Me.chkFacilityIncludeTime.UseVisualStyleBackColor = True
        '
        'lblFacilityDefault
        '
        Me.lblFacilityDefault.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFacilityDefault.Location = New System.Drawing.Point(218, 3)
        Me.lblFacilityDefault.Name = "lblFacilityDefault"
        Me.lblFacilityDefault.Size = New System.Drawing.Size(79, 20)
        Me.lblFacilityDefault.TabIndex = 4
        Me.lblFacilityDefault.Text = "Default Facility"
        Me.lblFacilityDefault.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chkFacilityIncludeCost
        '
        Me.chkFacilityIncludeCost.AutoSize = True
        Me.chkFacilityIncludeCost.Location = New System.Drawing.Point(196, 20)
        Me.chkFacilityIncludeCost.Name = "chkFacilityIncludeCost"
        Me.chkFacilityIncludeCost.Size = New System.Drawing.Size(47, 17)
        Me.chkFacilityIncludeCost.TabIndex = 9
        Me.chkFacilityIncludeCost.Text = "Cost"
        Me.chkFacilityIncludeCost.UseVisualStyleBackColor = True
        '
        'cmbFacility
        '
        Me.cmbFacility.FormattingEnabled = True
        Me.cmbFacility.ItemHeight = 13
        Me.cmbFacility.Location = New System.Drawing.Point(4, 63)
        Me.cmbFacility.Name = "cmbFacility"
        Me.cmbFacility.Size = New System.Drawing.Size(291, 21)
        Me.cmbFacility.TabIndex = 14
        Me.cmbFacility.Text = "Select Facility"
        '
        'cmbFacilitySystem
        '
        Me.cmbFacilitySystem.FormattingEnabled = True
        Me.cmbFacilitySystem.Location = New System.Drawing.Point(139, 37)
        Me.cmbFacilitySystem.Name = "cmbFacilitySystem"
        Me.cmbFacilitySystem.Size = New System.Drawing.Size(157, 21)
        Me.cmbFacilitySystem.TabIndex = 13
        Me.cmbFacilitySystem.Text = "Select System"
        '
        'cmbFacilityRegion
        '
        Me.cmbFacilityRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFacilityRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFacilityRegion.FormattingEnabled = True
        Me.cmbFacilityRegion.Location = New System.Drawing.Point(5, 37)
        Me.cmbFacilityRegion.Name = "cmbFacilityRegion"
        Me.cmbFacilityRegion.Size = New System.Drawing.Size(130, 21)
        Me.cmbFacilityRegion.TabIndex = 12
        Me.cmbFacilityRegion.Text = "Select Region"
        '
        'lblFacilityLocation
        '
        Me.lblFacilityLocation.AutoSize = True
        Me.lblFacilityLocation.Location = New System.Drawing.Point(3, 22)
        Me.lblFacilityLocation.Name = "lblFacilityLocation"
        Me.lblFacilityLocation.Size = New System.Drawing.Size(51, 13)
        Me.lblFacilityLocation.TabIndex = 11
        Me.lblFacilityLocation.Text = "Location:"
        '
        'lblFacilityType
        '
        Me.lblFacilityType.AutoSize = True
        Me.lblFacilityType.Location = New System.Drawing.Point(1, 4)
        Me.lblFacilityType.Name = "lblFacilityType"
        Me.lblFacilityType.Size = New System.Drawing.Size(69, 13)
        Me.lblFacilityType.TabIndex = 2
        Me.lblFacilityType.Text = "Facility Type:"
        '
        'cmbFacilityType
        '
        Me.cmbFacilityType.FormattingEnabled = True
        Me.cmbFacilityType.ItemHeight = 13
        Me.cmbFacilityType.Location = New System.Drawing.Point(68, 1)
        Me.cmbFacilityType.Name = "cmbFacilityType"
        Me.cmbFacilityType.Size = New System.Drawing.Size(65, 21)
        Me.cmbFacilityType.TabIndex = 3
        '
        'cmbLargeShips
        '
        Me.cmbLargeShips.FormattingEnabled = True
        Me.cmbLargeShips.Items.AddRange(New Object() {"All", "Capital", "Large"})
        Me.cmbLargeShips.Location = New System.Drawing.Point(196, 229)
        Me.cmbLargeShips.Name = "cmbLargeShips"
        Me.cmbLargeShips.Size = New System.Drawing.Size(78, 21)
        Me.cmbLargeShips.TabIndex = 32
        Me.cmbLargeShips.Text = "All"
        '
        'lblLargeShips
        '
        Me.lblLargeShips.AutoSize = True
        Me.lblLargeShips.Location = New System.Drawing.Point(193, 213)
        Me.lblLargeShips.Name = "lblLargeShips"
        Me.lblLargeShips.Size = New System.Drawing.Size(66, 13)
        Me.lblLargeShips.TabIndex = 31
        Me.lblLargeShips.Text = "Large Ships:"
        '
        'cmbFuelBlocks
        '
        Me.cmbFuelBlocks.FormattingEnabled = True
        Me.cmbFuelBlocks.Items.AddRange(New Object() {"All", "Ammunition", "Component"})
        Me.cmbFuelBlocks.Location = New System.Drawing.Point(102, 229)
        Me.cmbFuelBlocks.Name = "cmbFuelBlocks"
        Me.cmbFuelBlocks.Size = New System.Drawing.Size(78, 21)
        Me.cmbFuelBlocks.TabIndex = 30
        Me.cmbFuelBlocks.Text = "All"
        '
        'lblFuelBlocks
        '
        Me.lblFuelBlocks.AutoSize = True
        Me.lblFuelBlocks.Location = New System.Drawing.Point(99, 213)
        Me.lblFuelBlocks.Name = "lblFuelBlocks"
        Me.lblFuelBlocks.Size = New System.Drawing.Size(65, 13)
        Me.lblFuelBlocks.TabIndex = 29
        Me.lblFuelBlocks.Text = "Fuel Blocks:"
        '
        'cmbModules
        '
        Me.cmbModules.FormattingEnabled = True
        Me.cmbModules.Items.AddRange(New Object() {"All", "Equipment", "Rapid"})
        Me.cmbModules.Location = New System.Drawing.Point(9, 229)
        Me.cmbModules.Name = "cmbModules"
        Me.cmbModules.Size = New System.Drawing.Size(78, 21)
        Me.cmbModules.TabIndex = 28
        Me.cmbModules.Text = "All"
        '
        'lblModules
        '
        Me.lblModules.Location = New System.Drawing.Point(6, 196)
        Me.lblModules.Name = "lblModules"
        Me.lblModules.Size = New System.Drawing.Size(87, 30)
        Me.lblModules.TabIndex = 27
        Me.lblModules.Text = "Assembly Arrays: Modules:"
        '
        'btnFacilityFitting
        '
        Me.btnFacilityFitting.Location = New System.Drawing.Point(209, 85)
        Me.btnFacilityFitting.Name = "btnFacilityFitting"
        Me.btnFacilityFitting.Size = New System.Drawing.Size(43, 22)
        Me.btnFacilityFitting.TabIndex = 23
        Me.btnFacilityFitting.Text = "Fitting"
        Me.btnFacilityFitting.UseVisualStyleBackColor = True
        '
        'txtFacilityManualTax
        '
        Me.txtFacilityManualTax.Location = New System.Drawing.Point(133, 105)
        Me.txtFacilityManualTax.MaxLength = 7
        Me.txtFacilityManualTax.Name = "txtFacilityManualTax"
        Me.txtFacilityManualTax.Size = New System.Drawing.Size(50, 20)
        Me.txtFacilityManualTax.TabIndex = 22
        Me.txtFacilityManualTax.Text = "100.00%"
        Me.txtFacilityManualTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFacilityManualTax
        '
        Me.lblFacilityManualTax.AutoSize = True
        Me.lblFacilityManualTax.Location = New System.Drawing.Point(99, 109)
        Me.lblFacilityManualTax.Name = "lblFacilityManualTax"
        Me.lblFacilityManualTax.Size = New System.Drawing.Size(28, 13)
        Me.lblFacilityManualTax.TabIndex = 21
        Me.lblFacilityManualTax.Text = "Tax:"
        '
        'btnFacilitySave
        '
        Me.btnFacilitySave.Location = New System.Drawing.Point(255, 85)
        Me.btnFacilitySave.Name = "btnFacilitySave"
        Me.btnFacilitySave.Size = New System.Drawing.Size(40, 22)
        Me.btnFacilitySave.TabIndex = 24
        Me.btnFacilitySave.Text = "Save"
        Me.btnFacilitySave.UseVisualStyleBackColor = True
        '
        'txtFacilityManualTE
        '
        Me.txtFacilityManualTE.Location = New System.Drawing.Point(133, 84)
        Me.txtFacilityManualTE.MaxLength = 7
        Me.txtFacilityManualTE.Name = "txtFacilityManualTE"
        Me.txtFacilityManualTE.Size = New System.Drawing.Size(50, 20)
        Me.txtFacilityManualTE.TabIndex = 20
        Me.txtFacilityManualTE.Text = "100.00%"
        Me.txtFacilityManualTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFacilityManualME
        '
        Me.txtFacilityManualME.Location = New System.Drawing.Point(38, 84)
        Me.txtFacilityManualME.MaxLength = 7
        Me.txtFacilityManualME.Name = "txtFacilityManualME"
        Me.txtFacilityManualME.Size = New System.Drawing.Size(50, 20)
        Me.txtFacilityManualME.TabIndex = 16
        Me.txtFacilityManualME.Text = "100.00%"
        Me.txtFacilityManualME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFacilityManualTE
        '
        Me.lblFacilityManualTE.AutoSize = True
        Me.lblFacilityManualTE.Location = New System.Drawing.Point(103, 88)
        Me.lblFacilityManualTE.Name = "lblFacilityManualTE"
        Me.lblFacilityManualTE.Size = New System.Drawing.Size(24, 13)
        Me.lblFacilityManualTE.TabIndex = 19
        Me.lblFacilityManualTE.Text = "TE:"
        '
        'lblFacilityManualME
        '
        Me.lblFacilityManualME.AutoSize = True
        Me.lblFacilityManualME.Location = New System.Drawing.Point(6, 88)
        Me.lblFacilityManualME.Name = "lblFacilityManualME"
        Me.lblFacilityManualME.Size = New System.Drawing.Size(26, 13)
        Me.lblFacilityManualME.TabIndex = 15
        Me.lblFacilityManualME.Text = "ME:"
        '
        'lblFacilityManualCost
        '
        Me.lblFacilityManualCost.AutoSize = True
        Me.lblFacilityManualCost.Location = New System.Drawing.Point(6, 109)
        Me.lblFacilityManualCost.Name = "lblFacilityManualCost"
        Me.lblFacilityManualCost.Size = New System.Drawing.Size(31, 13)
        Me.lblFacilityManualCost.TabIndex = 17
        Me.lblFacilityManualCost.Text = "Cost:"
        '
        'txtFacilityManualCost
        '
        Me.txtFacilityManualCost.Location = New System.Drawing.Point(38, 105)
        Me.txtFacilityManualCost.MaxLength = 7
        Me.txtFacilityManualCost.Name = "txtFacilityManualCost"
        Me.txtFacilityManualCost.Size = New System.Drawing.Size(50, 20)
        Me.txtFacilityManualCost.TabIndex = 18
        Me.txtFacilityManualCost.Text = "100.00%"
        Me.txtFacilityManualCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFacilityFWUpgrade
        '
        Me.lblFacilityFWUpgrade.Location = New System.Drawing.Point(180, 164)
        Me.lblFacilityFWUpgrade.Name = "lblFacilityFWUpgrade"
        Me.lblFacilityFWUpgrade.Size = New System.Drawing.Size(51, 30)
        Me.lblFacilityFWUpgrade.TabIndex = 25
        Me.lblFacilityFWUpgrade.Text = "FW Upgrade:"
        '
        'cmbFacilityFWUpgrade
        '
        Me.cmbFacilityFWUpgrade.FormattingEnabled = True
        Me.cmbFacilityFWUpgrade.Items.AddRange(New Object() {"None", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5"})
        Me.cmbFacilityFWUpgrade.Location = New System.Drawing.Point(231, 164)
        Me.cmbFacilityFWUpgrade.Name = "cmbFacilityFWUpgrade"
        Me.cmbFacilityFWUpgrade.Size = New System.Drawing.Size(61, 21)
        Me.cmbFacilityFWUpgrade.TabIndex = 26
        Me.cmbFacilityFWUpgrade.Text = "Level 5"
        '
        'chkFacilityToggle
        '
        Me.chkFacilityToggle.AutoSize = True
        Me.chkFacilityToggle.Location = New System.Drawing.Point(139, 3)
        Me.chkFacilityToggle.Name = "chkFacilityToggle"
        Me.chkFacilityToggle.Size = New System.Drawing.Size(79, 17)
        Me.chkFacilityToggle.TabIndex = 5
        Me.chkFacilityToggle.Text = "Dessy/Cap"
        Me.chkFacilityToggle.UseVisualStyleBackColor = True
        '
        'lblFacilityUsage
        '
        Me.lblFacilityUsage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFacilityUsage.Location = New System.Drawing.Point(115, 141)
        Me.lblFacilityUsage.Name = "lblFacilityUsage"
        Me.lblFacilityUsage.Size = New System.Drawing.Size(177, 17)
        Me.lblFacilityUsage.TabIndex = 8
        Me.lblFacilityUsage.Text = "0.00"
        Me.lblFacilityUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbFacilityActivities
        '
        Me.cmbFacilityActivities.FormattingEnabled = True
        Me.cmbFacilityActivities.Location = New System.Drawing.Point(4, 161)
        Me.cmbFacilityActivities.Name = "cmbFacilityActivities"
        Me.cmbFacilityActivities.Size = New System.Drawing.Size(170, 21)
        Me.cmbFacilityActivities.TabIndex = 1
        Me.cmbFacilityActivities.Text = "Select Activity"
        '
        'lblFacilityActivity
        '
        Me.lblFacilityActivity.AutoSize = True
        Me.lblFacilityActivity.Location = New System.Drawing.Point(3, 145)
        Me.lblFacilityActivity.Name = "lblFacilityActivity"
        Me.lblFacilityActivity.Size = New System.Drawing.Size(44, 13)
        Me.lblFacilityActivity.TabIndex = 0
        Me.lblFacilityActivity.Text = "Activity:"
        '
        'ManufacturingFacility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblModules)
        Me.Controls.Add(Me.lblFuelBlocks)
        Me.Controls.Add(Me.lblLargeShips)
        Me.Controls.Add(Me.lblFacilityFWUpgrade)
        Me.Controls.Add(Me.cmbFacilityType)
        Me.Controls.Add(Me.cmbFacilityActivities)
        Me.Controls.Add(Me.lblFacilityActivity)
        Me.Controls.Add(Me.lblFacilityUsage)
        Me.Controls.Add(Me.chkFacilityToggle)
        Me.Controls.Add(Me.cmbFacilityFWUpgrade)
        Me.Controls.Add(Me.txtFacilityManualCost)
        Me.Controls.Add(Me.lblFacilityManualCost)
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
        Me.Controls.Add(Me.cmbFacility)
        Me.Controls.Add(Me.cmbFacilitySystem)
        Me.Controls.Add(Me.cmbFacilityRegion)
        Me.Controls.Add(Me.lblFacilityLocation)
        Me.Controls.Add(Me.lblFacilityType)
        Me.Controls.Add(Me.cmbLargeShips)
        Me.Controls.Add(Me.cmbFuelBlocks)
        Me.Controls.Add(Me.cmbModules)
        Me.Name = "ManufacturingFacility"
        Me.Size = New System.Drawing.Size(303, 343)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInclude As Label
    Friend WithEvents chkFacilityIncludeUsage As CheckBox
    Friend WithEvents chkFacilityIncludeTime As CheckBox
    Friend WithEvents lblFacilityDefault As Label
    Friend WithEvents chkFacilityIncludeCost As CheckBox
    Friend WithEvents cmbFacility As ComboBox
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
    Friend WithEvents lblFacilityManualCost As Label
    Friend WithEvents txtFacilityManualCost As TextBox
    Friend WithEvents lblFacilityFWUpgrade As Label
    Friend WithEvents cmbFacilityFWUpgrade As ComboBox
    Friend WithEvents chkFacilityToggle As CheckBox
    Friend WithEvents lblFacilityUsage As Label
    Friend WithEvents cmbFacilityActivities As ComboBox
    Friend WithEvents lblFacilityActivity As Label
    Friend WithEvents mainToolTip As ToolTip
End Class
