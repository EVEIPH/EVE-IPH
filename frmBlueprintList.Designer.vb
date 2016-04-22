<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBlueprintList
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Ammunition/Charges")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Drone")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Ship Equipment")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Ships")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Implants")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Materials")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Commodity")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Subsystem")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Starbase")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Special Edition Assets")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Sovereignty Structures")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Infrastructure Upgrades")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Orbitals")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Deployable")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Celestial")
        Me.treBlueprintTreeView = New System.Windows.Forms.TreeView()
        Me.lblIntro = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'treBlueprintTreeView
        '
        Me.treBlueprintTreeView.Location = New System.Drawing.Point(12, 56)
        Me.treBlueprintTreeView.Name = "treBlueprintTreeView"
        TreeNode1.Name = "Charge"
        TreeNode1.Text = "Ammunition/Charges"
        TreeNode2.Name = "Drone"
        TreeNode2.Text = "Drone"
        TreeNode3.Name = "Module"
        TreeNode3.Text = "Ship Equipment"
        TreeNode4.Name = "Ship"
        TreeNode4.Text = "Ships"
        TreeNode5.Name = "Implant"
        TreeNode5.Text = "Implants"
        TreeNode6.Name = "Material"
        TreeNode6.Text = "Materials"
        TreeNode7.Name = "Commodity"
        TreeNode7.Text = "Commodity"
        TreeNode8.Name = "Subsystem"
        TreeNode8.Text = "Subsystem"
        TreeNode9.Name = "Starbase"
        TreeNode9.Text = "Starbase"
        TreeNode10.Name = "Special Edition Assets"
        TreeNode10.Text = "Special Edition Assets"
        TreeNode11.Name = "Sovereignty Structures"
        TreeNode11.Text = "Sovereignty Structures"
        TreeNode12.Name = "Infrastructure Upgrades"
        TreeNode12.Text = "Infrastructure Upgrades"
        TreeNode13.Name = "Orbitals"
        TreeNode13.Text = "Orbitals"
        TreeNode14.Name = "Deployable"
        TreeNode14.Text = "Deployable"
        TreeNode15.Name = "Celestial"
        TreeNode15.Text = "Celestial"
        Me.treBlueprintTreeView.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11, TreeNode12, TreeNode13, TreeNode14, TreeNode15})
        Me.treBlueprintTreeView.Size = New System.Drawing.Size(368, 284)
        Me.treBlueprintTreeView.TabIndex = 0
        '
        'lblIntro
        '
        Me.lblIntro.AutoSize = true
        Me.lblIntro.Location = New System.Drawing.Point(12, 9)
        Me.lblIntro.Name = "lblIntro"
        Me.lblIntro.Size = New System.Drawing.Size(0, 13)
        Me.lblIntro.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(305, 369)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = true
        '
        'frmBlueprintList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 404)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblIntro)
        Me.Controls.Add(Me.treBlueprintTreeView)
        Me.Name = "frmBlueprintList"
        Me.Text = "frmBlueprintList"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents treBlueprintTreeView As TreeView
    Friend WithEvents lblIntro As Label
    Friend WithEvents btnClose As Button
End Class
