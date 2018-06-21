<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBonusPopout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBonusPopout))
        Me.lstUpwellStructureBonuses = New System.Windows.Forms.ListView()
        Me.BonusAppliesTo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Activity = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Bonuses = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Source = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstUpwellStructureBonuses
        '
        Me.lstUpwellStructureBonuses.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.BonusAppliesTo, Me.Activity, Me.Bonuses, Me.Source})
        Me.lstUpwellStructureBonuses.FullRowSelect = True
        Me.lstUpwellStructureBonuses.GridLines = True
        Me.lstUpwellStructureBonuses.HideSelection = False
        Me.lstUpwellStructureBonuses.Location = New System.Drawing.Point(11, 15)
        Me.lstUpwellStructureBonuses.MultiSelect = False
        Me.lstUpwellStructureBonuses.Name = "lstUpwellStructureBonuses"
        Me.lstUpwellStructureBonuses.Size = New System.Drawing.Size(712, 181)
        Me.lstUpwellStructureBonuses.TabIndex = 83
        Me.lstUpwellStructureBonuses.TabStop = False
        Me.lstUpwellStructureBonuses.UseCompatibleStateImageBehavior = False
        Me.lstUpwellStructureBonuses.View = System.Windows.Forms.View.Details
        '
        'BonusAppliesTo
        '
        Me.BonusAppliesTo.Text = "Bonus Applies to"
        Me.BonusAppliesTo.Width = 150
        '
        'Activity
        '
        Me.Activity.Text = "Activity"
        Me.Activity.Width = 125
        '
        'Bonuses
        '
        Me.Bonuses.Text = "Bonuses"
        Me.Bonuses.Width = 250
        '
        'Source
        '
        Me.Source.Text = "Bonus Source"
        Me.Source.Width = 165
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(271, 202)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(86, 28)
        Me.btnSaveSettings.TabIndex = 84
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(377, 202)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 28)
        Me.btnClose.TabIndex = 85
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmBonusPopout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 236)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.lstUpwellStructureBonuses)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(750, 275)
        Me.Name = "frmBonusPopout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Facility Bonuses"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstUpwellStructureBonuses As ListView
    Friend WithEvents BonusAppliesTo As ColumnHeader
    Friend WithEvents Activity As ColumnHeader
    Friend WithEvents Bonuses As ColumnHeader
    Friend WithEvents Source As ColumnHeader
    Friend WithEvents btnSaveSettings As Button
    Friend WithEvents btnClose As Button
End Class
