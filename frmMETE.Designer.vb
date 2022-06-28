<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMETE
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMETE))
        Me.gbBPBlueprintType = New System.Windows.Forms.GroupBox()
        Me.rbtnBPStructureModulesBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPCelestialsBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPMiscBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPStructureBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPStructureRigsBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPOwnedBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPRigBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPBoosterBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPModuleBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPAmmoChargeBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPDroneBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPComponentBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPAllBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPShipBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBPDeployableBlueprints = New System.Windows.Forms.RadioButton()
        Me.cmbBlueprintSelection = New System.Windows.Forms.ComboBox()
        Me.lblSelectBlueprint = New System.Windows.Forms.Label()
        Me.ResearchFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.gbBPBlueprintType.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbBPBlueprintType
        '
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPStructureModulesBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPCelestialsBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPMiscBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPStructureBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPStructureRigsBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPOwnedBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPRigBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPBoosterBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPModuleBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPAmmoChargeBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPDroneBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPComponentBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPAllBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPShipBlueprints)
        Me.gbBPBlueprintType.Controls.Add(Me.rbtnBPDeployableBlueprints)
        Me.gbBPBlueprintType.Location = New System.Drawing.Point(24, 59)
        Me.gbBPBlueprintType.Name = "gbBPBlueprintType"
        Me.gbBPBlueprintType.Size = New System.Drawing.Size(348, 125)
        Me.gbBPBlueprintType.TabIndex = 74
        Me.gbBPBlueprintType.TabStop = False
        Me.gbBPBlueprintType.Text = "Blueprint Type"
        '
        'rbtnBPStructureModulesBlueprints
        '
        Me.rbtnBPStructureModulesBlueprints.AutoSize = True
        Me.rbtnBPStructureModulesBlueprints.Location = New System.Drawing.Point(208, 85)
        Me.rbtnBPStructureModulesBlueprints.Name = "rbtnBPStructureModulesBlueprints"
        Me.rbtnBPStructureModulesBlueprints.Size = New System.Drawing.Size(111, 17)
        Me.rbtnBPStructureModulesBlueprints.TabIndex = 65
        Me.rbtnBPStructureModulesBlueprints.TabStop = True
        Me.rbtnBPStructureModulesBlueprints.Text = "Structure Modules"
        Me.rbtnBPStructureModulesBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPCelestialsBlueprints
        '
        Me.rbtnBPCelestialsBlueprints.AutoSize = True
        Me.rbtnBPCelestialsBlueprints.Location = New System.Drawing.Point(208, 51)
        Me.rbtnBPCelestialsBlueprints.Name = "rbtnBPCelestialsBlueprints"
        Me.rbtnBPCelestialsBlueprints.Size = New System.Drawing.Size(69, 17)
        Me.rbtnBPCelestialsBlueprints.TabIndex = 14
        Me.rbtnBPCelestialsBlueprints.TabStop = True
        Me.rbtnBPCelestialsBlueprints.Text = "Celestials"
        Me.rbtnBPCelestialsBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPMiscBlueprints
        '
        Me.rbtnBPMiscBlueprints.AutoSize = True
        Me.rbtnBPMiscBlueprints.Location = New System.Drawing.Point(208, 68)
        Me.rbtnBPMiscBlueprints.Name = "rbtnBPMiscBlueprints"
        Me.rbtnBPMiscBlueprints.Size = New System.Drawing.Size(50, 17)
        Me.rbtnBPMiscBlueprints.TabIndex = 15
        Me.rbtnBPMiscBlueprints.TabStop = True
        Me.rbtnBPMiscBlueprints.Text = "Misc."
        Me.rbtnBPMiscBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPStructureBlueprints
        '
        Me.rbtnBPStructureBlueprints.AutoSize = True
        Me.rbtnBPStructureBlueprints.Location = New System.Drawing.Point(9, 85)
        Me.rbtnBPStructureBlueprints.Name = "rbtnBPStructureBlueprints"
        Me.rbtnBPStructureBlueprints.Size = New System.Drawing.Size(73, 17)
        Me.rbtnBPStructureBlueprints.TabIndex = 12
        Me.rbtnBPStructureBlueprints.TabStop = True
        Me.rbtnBPStructureBlueprints.Text = "Structures"
        Me.rbtnBPStructureBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPStructureRigsBlueprints
        '
        Me.rbtnBPStructureRigsBlueprints.AutoSize = True
        Me.rbtnBPStructureRigsBlueprints.Location = New System.Drawing.Point(97, 85)
        Me.rbtnBPStructureRigsBlueprints.Name = "rbtnBPStructureRigsBlueprints"
        Me.rbtnBPStructureRigsBlueprints.Size = New System.Drawing.Size(92, 17)
        Me.rbtnBPStructureRigsBlueprints.TabIndex = 13
        Me.rbtnBPStructureRigsBlueprints.TabStop = True
        Me.rbtnBPStructureRigsBlueprints.Text = "Structure Rigs"
        Me.rbtnBPStructureRigsBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPOwnedBlueprints
        '
        Me.rbtnBPOwnedBlueprints.AutoSize = True
        Me.rbtnBPOwnedBlueprints.Location = New System.Drawing.Point(97, 17)
        Me.rbtnBPOwnedBlueprints.Name = "rbtnBPOwnedBlueprints"
        Me.rbtnBPOwnedBlueprints.Size = New System.Drawing.Size(59, 17)
        Me.rbtnBPOwnedBlueprints.TabIndex = 1
        Me.rbtnBPOwnedBlueprints.TabStop = True
        Me.rbtnBPOwnedBlueprints.Text = "Owned"
        Me.rbtnBPOwnedBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPRigBlueprints
        '
        Me.rbtnBPRigBlueprints.AutoSize = True
        Me.rbtnBPRigBlueprints.Location = New System.Drawing.Point(97, 51)
        Me.rbtnBPRigBlueprints.Name = "rbtnBPRigBlueprints"
        Me.rbtnBPRigBlueprints.Size = New System.Drawing.Size(46, 17)
        Me.rbtnBPRigBlueprints.TabIndex = 7
        Me.rbtnBPRigBlueprints.TabStop = True
        Me.rbtnBPRigBlueprints.Text = "Rigs"
        Me.rbtnBPRigBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPBoosterBlueprints
        '
        Me.rbtnBPBoosterBlueprints.AutoSize = True
        Me.rbtnBPBoosterBlueprints.Location = New System.Drawing.Point(208, 34)
        Me.rbtnBPBoosterBlueprints.Name = "rbtnBPBoosterBlueprints"
        Me.rbtnBPBoosterBlueprints.Size = New System.Drawing.Size(66, 17)
        Me.rbtnBPBoosterBlueprints.TabIndex = 11
        Me.rbtnBPBoosterBlueprints.TabStop = True
        Me.rbtnBPBoosterBlueprints.Text = "Boosters"
        Me.rbtnBPBoosterBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPModuleBlueprints
        '
        Me.rbtnBPModuleBlueprints.AutoSize = True
        Me.rbtnBPModuleBlueprints.Location = New System.Drawing.Point(208, 17)
        Me.rbtnBPModuleBlueprints.Name = "rbtnBPModuleBlueprints"
        Me.rbtnBPModuleBlueprints.Size = New System.Drawing.Size(65, 17)
        Me.rbtnBPModuleBlueprints.TabIndex = 4
        Me.rbtnBPModuleBlueprints.TabStop = True
        Me.rbtnBPModuleBlueprints.Text = "Modules"
        Me.rbtnBPModuleBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPAmmoChargeBlueprints
        '
        Me.rbtnBPAmmoChargeBlueprints.AutoSize = True
        Me.rbtnBPAmmoChargeBlueprints.Location = New System.Drawing.Point(97, 34)
        Me.rbtnBPAmmoChargeBlueprints.Name = "rbtnBPAmmoChargeBlueprints"
        Me.rbtnBPAmmoChargeBlueprints.Size = New System.Drawing.Size(98, 17)
        Me.rbtnBPAmmoChargeBlueprints.TabIndex = 5
        Me.rbtnBPAmmoChargeBlueprints.TabStop = True
        Me.rbtnBPAmmoChargeBlueprints.Text = "Ammo/Charges"
        Me.rbtnBPAmmoChargeBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPDroneBlueprints
        '
        Me.rbtnBPDroneBlueprints.AutoSize = True
        Me.rbtnBPDroneBlueprints.Location = New System.Drawing.Point(9, 51)
        Me.rbtnBPDroneBlueprints.Name = "rbtnBPDroneBlueprints"
        Me.rbtnBPDroneBlueprints.Size = New System.Drawing.Size(59, 17)
        Me.rbtnBPDroneBlueprints.TabIndex = 6
        Me.rbtnBPDroneBlueprints.TabStop = True
        Me.rbtnBPDroneBlueprints.Text = "Drones"
        Me.rbtnBPDroneBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPComponentBlueprints
        '
        Me.rbtnBPComponentBlueprints.AutoSize = True
        Me.rbtnBPComponentBlueprints.Location = New System.Drawing.Point(97, 68)
        Me.rbtnBPComponentBlueprints.Name = "rbtnBPComponentBlueprints"
        Me.rbtnBPComponentBlueprints.Size = New System.Drawing.Size(84, 17)
        Me.rbtnBPComponentBlueprints.TabIndex = 10
        Me.rbtnBPComponentBlueprints.TabStop = True
        Me.rbtnBPComponentBlueprints.Text = "Components"
        Me.rbtnBPComponentBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPAllBlueprints
        '
        Me.rbtnBPAllBlueprints.AutoSize = True
        Me.rbtnBPAllBlueprints.Location = New System.Drawing.Point(9, 17)
        Me.rbtnBPAllBlueprints.Name = "rbtnBPAllBlueprints"
        Me.rbtnBPAllBlueprints.Size = New System.Drawing.Size(36, 17)
        Me.rbtnBPAllBlueprints.TabIndex = 0
        Me.rbtnBPAllBlueprints.TabStop = True
        Me.rbtnBPAllBlueprints.Text = "All"
        Me.rbtnBPAllBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPShipBlueprints
        '
        Me.rbtnBPShipBlueprints.AutoSize = True
        Me.rbtnBPShipBlueprints.Location = New System.Drawing.Point(9, 34)
        Me.rbtnBPShipBlueprints.Name = "rbtnBPShipBlueprints"
        Me.rbtnBPShipBlueprints.Size = New System.Drawing.Size(51, 17)
        Me.rbtnBPShipBlueprints.TabIndex = 3
        Me.rbtnBPShipBlueprints.TabStop = True
        Me.rbtnBPShipBlueprints.Text = "Ships"
        Me.rbtnBPShipBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBPDeployableBlueprints
        '
        Me.rbtnBPDeployableBlueprints.AutoSize = True
        Me.rbtnBPDeployableBlueprints.Location = New System.Drawing.Point(9, 68)
        Me.rbtnBPDeployableBlueprints.Name = "rbtnBPDeployableBlueprints"
        Me.rbtnBPDeployableBlueprints.Size = New System.Drawing.Size(78, 17)
        Me.rbtnBPDeployableBlueprints.TabIndex = 9
        Me.rbtnBPDeployableBlueprints.TabStop = True
        Me.rbtnBPDeployableBlueprints.Text = "Deployable"
        Me.rbtnBPDeployableBlueprints.UseVisualStyleBackColor = True
        '
        'cmbBlueprintSelection
        '
        Me.cmbBlueprintSelection.Location = New System.Drawing.Point(24, 35)
        Me.cmbBlueprintSelection.Name = "cmbBlueprintSelection"
        Me.cmbBlueprintSelection.Size = New System.Drawing.Size(294, 21)
        Me.cmbBlueprintSelection.TabIndex = 73
        Me.cmbBlueprintSelection.Text = "Select Blueprint"
        '
        'lblSelectBlueprint
        '
        Me.lblSelectBlueprint.AutoSize = True
        Me.lblSelectBlueprint.Location = New System.Drawing.Point(23, 20)
        Me.lblSelectBlueprint.Name = "lblSelectBlueprint"
        Me.lblSelectBlueprint.Size = New System.Drawing.Size(93, 13)
        Me.lblSelectBlueprint.TabIndex = 72
        Me.lblSelectBlueprint.Text = "Selected Blueprint"
        '
        'ResearchFacility
        '
        Me.ResearchFacility.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ResearchFacility.Location = New System.Drawing.Point(33, 207)
        Me.ResearchFacility.Name = "ResearchFacility"
        Me.ResearchFacility.Size = New System.Drawing.Size(280, 142)
        Me.ResearchFacility.TabIndex = 75
        '
        'frmMETE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 483)
        Me.Controls.Add(Me.ResearchFacility)
        Me.Controls.Add(Me.gbBPBlueprintType)
        Me.Controls.Add(Me.cmbBlueprintSelection)
        Me.Controls.Add(Me.lblSelectBlueprint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMETE"
        Me.Text = "Blueprint Research Calculator"
        Me.gbBPBlueprintType.ResumeLayout(False)
        Me.gbBPBlueprintType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbBPBlueprintType As GroupBox
    Friend WithEvents rbtnBPStructureModulesBlueprints As RadioButton
    Friend WithEvents rbtnBPCelestialsBlueprints As RadioButton
    Friend WithEvents rbtnBPMiscBlueprints As RadioButton
    Friend WithEvents rbtnBPStructureBlueprints As RadioButton
    Friend WithEvents rbtnBPStructureRigsBlueprints As RadioButton
    Friend WithEvents rbtnBPOwnedBlueprints As RadioButton
    Friend WithEvents rbtnBPRigBlueprints As RadioButton
    Friend WithEvents rbtnBPBoosterBlueprints As RadioButton
    Friend WithEvents rbtnBPModuleBlueprints As RadioButton
    Friend WithEvents rbtnBPAmmoChargeBlueprints As RadioButton
    Friend WithEvents rbtnBPDroneBlueprints As RadioButton
    Friend WithEvents rbtnBPComponentBlueprints As RadioButton
    Friend WithEvents rbtnBPAllBlueprints As RadioButton
    Friend WithEvents rbtnBPShipBlueprints As RadioButton
    Friend WithEvents rbtnBPDeployableBlueprints As RadioButton
    Friend WithEvents cmbBlueprintSelection As ComboBox
    Friend WithEvents lblSelectBlueprint As Label
    Friend WithEvents ResearchFacility As ManufacturingFacility
End Class
