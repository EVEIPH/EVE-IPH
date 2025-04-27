<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRefreshMarketHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefreshMarketHistory))
        Me.btnRefreshHistory = New System.Windows.Forms.Button()
        Me.cmbRegions = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.gbUpdateTimer = New System.Windows.Forms.GroupBox()
        Me.lblItemsLastUpdated = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblLastUpdateDate = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.MarketHistoryPG = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbUpdateTimer.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRefreshHistory
        '
        Me.btnRefreshHistory.Location = New System.Drawing.Point(12, 136)
        Me.btnRefreshHistory.Name = "btnRefreshHistory"
        Me.btnRefreshHistory.Size = New System.Drawing.Size(92, 36)
        Me.btnRefreshHistory.TabIndex = 59
        Me.btnRefreshHistory.Text = "Refresh History"
        Me.btnRefreshHistory.UseVisualStyleBackColor = True
        '
        'cmbRegions
        '
        Me.cmbRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRegions.FormattingEnabled = True
        Me.cmbRegions.Location = New System.Drawing.Point(12, 27)
        Me.cmbRegions.Name = "cmbRegions"
        Me.cmbRegions.Size = New System.Drawing.Size(287, 21)
        Me.cmbRegions.TabIndex = 129
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 128
        Me.Label2.Text = "Select Region:"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(206, 136)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 36)
        Me.btnClose.TabIndex = 130
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'gbUpdateTimer
        '
        Me.gbUpdateTimer.Controls.Add(Me.lblItemsLastUpdated)
        Me.gbUpdateTimer.Controls.Add(Me.Label4)
        Me.gbUpdateTimer.Controls.Add(Me.Label3)
        Me.gbUpdateTimer.Controls.Add(Me.lblLastUpdateDate)
        Me.gbUpdateTimer.Location = New System.Drawing.Point(12, 54)
        Me.gbUpdateTimer.Name = "gbUpdateTimer"
        Me.gbUpdateTimer.Size = New System.Drawing.Size(287, 72)
        Me.gbUpdateTimer.TabIndex = 133
        Me.gbUpdateTimer.TabStop = False
        '
        'lblItemsLastUpdated
        '
        Me.lblItemsLastUpdated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblItemsLastUpdated.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemsLastUpdated.Location = New System.Drawing.Point(174, 28)
        Me.lblItemsLastUpdated.Name = "lblItemsLastUpdated"
        Me.lblItemsLastUpdated.Size = New System.Drawing.Size(107, 32)
        Me.lblItemsLastUpdated.TabIndex = 138
        Me.lblItemsLastUpdated.Text = "9999 of 9999"
        Me.lblItemsLastUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 137
        Me.Label4.Text = "Last Updated:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(174, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "Items Updated:"
        '
        'lblLastUpdateDate
        '
        Me.lblLastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLastUpdateDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastUpdateDate.Location = New System.Drawing.Point(6, 28)
        Me.lblLastUpdateDate.Name = "lblLastUpdateDate"
        Me.lblLastUpdateDate.Size = New System.Drawing.Size(162, 32)
        Me.lblLastUpdateDate.TabIndex = 61
        Me.lblLastUpdateDate.Text = "99h 99m 99s"
        Me.lblLastUpdateDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MarketHistoryPG})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 185)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(309, 26)
        Me.StatusStrip1.TabIndex = 134
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'MarketHistoryPG
        '
        Me.MarketHistoryPG.Name = "MarketHistoryPG"
        Me.MarketHistoryPG.Size = New System.Drawing.Size(290, 20)
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(109, 136)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 36)
        Me.btnCancel.TabIndex = 135
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmRefreshMarketHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 211)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.gbUpdateTimer)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.cmbRegions)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnRefreshHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRefreshMarketHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Refresh Market History"
        Me.gbUpdateTimer.ResumeLayout(False)
        Me.gbUpdateTimer.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRefreshHistory As Button
    Friend WithEvents cmbRegions As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents gbUpdateTimer As GroupBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents MarketHistoryPG As ToolStripProgressBar
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblItemsLastUpdated As Label
    Friend WithEvents lblLastUpdateDate As Label
End Class
