<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResearchAgents
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResearchAgents))
        Me.lstAgents = New System.Windows.Forms.ListView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblTotalDCValue = New System.Windows.Forms.Label()
        Me.lblBPMarketCost1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lstAgents
        '
        Me.lstAgents.FullRowSelect = True
        Me.lstAgents.HideSelection = False
        Me.lstAgents.Location = New System.Drawing.Point(10, 12)
        Me.lstAgents.MultiSelect = False
        Me.lstAgents.Name = "lstAgents"
        Me.lstAgents.Size = New System.Drawing.Size(925, 131)
        Me.lstAgents.TabIndex = 8
        Me.lstAgents.UseCompatibleStateImageBehavior = False
        Me.lstAgents.View = System.Windows.Forms.View.Details
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(488, 149)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 28)
        Me.btnClose.TabIndex = 33
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(370, 149)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(86, 28)
        Me.btnRefresh.TabIndex = 34
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblTotalDCValue
        '
        Me.lblTotalDCValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalDCValue.Location = New System.Drawing.Point(757, 155)
        Me.lblTotalDCValue.Name = "lblTotalDCValue"
        Me.lblTotalDCValue.Size = New System.Drawing.Size(175, 17)
        Me.lblTotalDCValue.TabIndex = 36
        Me.lblTotalDCValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBPMarketCost1
        '
        Me.lblBPMarketCost1.AutoSize = True
        Me.lblBPMarketCost1.Location = New System.Drawing.Point(640, 157)
        Me.lblBPMarketCost1.Name = "lblBPMarketCost1"
        Me.lblBPMarketCost1.Size = New System.Drawing.Size(111, 13)
        Me.lblBPMarketCost1.TabIndex = 35
        Me.lblBPMarketCost1.Text = "Total Datacore Value:"
        '
        'frmResearchAgents
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(944, 183)
        Me.Controls.Add(Me.lblTotalDCValue)
        Me.Controls.Add(Me.lblBPMarketCost1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lstAgents)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmResearchAgents"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Current Research Agents"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstAgents As System.Windows.Forms.ListView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblTotalDCValue As System.Windows.Forms.Label
    Friend WithEvents lblBPMarketCost1 As System.Windows.Forms.Label
End Class
