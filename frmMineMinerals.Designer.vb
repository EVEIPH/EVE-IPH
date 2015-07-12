<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMineMinerals
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMineMinerals))
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.lstOretoMine = New System.Windows.Forms.ListView()
        Me.lstMineralsNeeded = New System.Windows.Forms.ListView()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCalculate
        '
        Me.btnCalculate.Location = New System.Drawing.Point(93, 324)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(149, 34)
        Me.btnCalculate.TabIndex = 0
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'lstOretoMine
        '
        Me.lstOretoMine.CheckBoxes = True
        Me.lstOretoMine.FullRowSelect = True
        Me.lstOretoMine.GridLines = True
        Me.lstOretoMine.HideSelection = False
        Me.lstOretoMine.Location = New System.Drawing.Point(331, 25)
        Me.lstOretoMine.Name = "lstOretoMine"
        Me.lstOretoMine.Size = New System.Drawing.Size(274, 265)
        Me.lstOretoMine.TabIndex = 58
        Me.lstOretoMine.TabStop = False
        Me.lstOretoMine.UseCompatibleStateImageBehavior = False
        Me.lstOretoMine.View = System.Windows.Forms.View.Details
        '
        'lstMineralsNeeded
        '
        Me.lstMineralsNeeded.CheckBoxes = True
        Me.lstMineralsNeeded.FullRowSelect = True
        Me.lstMineralsNeeded.GridLines = True
        Me.lstMineralsNeeded.HideSelection = False
        Me.lstMineralsNeeded.Location = New System.Drawing.Point(26, 25)
        Me.lstMineralsNeeded.Name = "lstMineralsNeeded"
        Me.lstMineralsNeeded.Size = New System.Drawing.Size(274, 265)
        Me.lstMineralsNeeded.TabIndex = 59
        Me.lstMineralsNeeded.TabStop = False
        Me.lstMineralsNeeded.UseCompatibleStateImageBehavior = False
        Me.lstMineralsNeeded.View = System.Windows.Forms.View.Details
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(393, 324)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(149, 34)
        Me.btnExit.TabIndex = 60
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmMineMinerals
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(632, 377)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lstMineralsNeeded)
        Me.Controls.Add(Me.lstOretoMine)
        Me.Controls.Add(Me.btnCalculate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMineMinerals"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mine Minerals"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents lstOretoMine As System.Windows.Forms.ListView
    Friend WithEvents lstMineralsNeeded As System.Windows.Forms.ListView
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
