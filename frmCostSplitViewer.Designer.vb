<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCostSplitViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCostSplitViewer))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCopyMats = New System.Windows.Forms.Button()
        Me.lstCosts = New EVE_Isk_per_Hour.MyListView()
        Me.gbExportOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnExportSSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportCSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportDefault = New System.Windows.Forms.RadioButton()
        Me.gbExportOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(47, 292)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(96, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCopyMats
        '
        Me.btnCopyMats.Location = New System.Drawing.Point(180, 292)
        Me.btnCopyMats.Name = "btnCopyMats"
        Me.btnCopyMats.Size = New System.Drawing.Size(96, 30)
        Me.btnCopyMats.TabIndex = 2
        Me.btnCopyMats.Text = "Copy List"
        Me.btnCopyMats.UseVisualStyleBackColor = True
        '
        'lstCosts
        '
        Me.lstCosts.FullRowSelect = True
        Me.lstCosts.GridLines = True
        Me.lstCosts.Location = New System.Drawing.Point(11, 9)
        Me.lstCosts.MultiSelect = False
        Me.lstCosts.Name = "lstCosts"
        Me.lstCosts.Size = New System.Drawing.Size(305, 231)
        Me.lstCosts.TabIndex = 40
        Me.lstCosts.TabStop = False
        Me.lstCosts.Tag = "20"
        Me.lstCosts.UseCompatibleStateImageBehavior = False
        Me.lstCosts.View = System.Windows.Forms.View.Details
        '
        'gbExportOptions
        '
        Me.gbExportOptions.Controls.Add(Me.rbtnExportSSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportCSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportDefault)
        Me.gbExportOptions.Location = New System.Drawing.Point(72, 245)
        Me.gbExportOptions.Name = "gbExportOptions"
        Me.gbExportOptions.Size = New System.Drawing.Size(178, 41)
        Me.gbExportOptions.TabIndex = 75
        Me.gbExportOptions.TabStop = False
        Me.gbExportOptions.Text = "Export Data in:"
        '
        'rbtnExportSSV
        '
        Me.rbtnExportSSV.AutoSize = True
        Me.rbtnExportSSV.Location = New System.Drawing.Point(125, 17)
        Me.rbtnExportSSV.Name = "rbtnExportSSV"
        Me.rbtnExportSSV.Size = New System.Drawing.Size(46, 17)
        Me.rbtnExportSSV.TabIndex = 2
        Me.rbtnExportSSV.TabStop = True
        Me.rbtnExportSSV.Text = "SSV"
        Me.rbtnExportSSV.UseVisualStyleBackColor = True
        '
        'rbtnExportCSV
        '
        Me.rbtnExportCSV.AutoSize = True
        Me.rbtnExportCSV.Location = New System.Drawing.Point(73, 17)
        Me.rbtnExportCSV.Name = "rbtnExportCSV"
        Me.rbtnExportCSV.Size = New System.Drawing.Size(46, 17)
        Me.rbtnExportCSV.TabIndex = 1
        Me.rbtnExportCSV.TabStop = True
        Me.rbtnExportCSV.Text = "CSV"
        Me.rbtnExportCSV.UseVisualStyleBackColor = True
        '
        'rbtnExportDefault
        '
        Me.rbtnExportDefault.AutoSize = True
        Me.rbtnExportDefault.Location = New System.Drawing.Point(8, 17)
        Me.rbtnExportDefault.Name = "rbtnExportDefault"
        Me.rbtnExportDefault.Size = New System.Drawing.Size(59, 17)
        Me.rbtnExportDefault.TabIndex = 0
        Me.rbtnExportDefault.TabStop = True
        Me.rbtnExportDefault.Text = "Default"
        Me.rbtnExportDefault.UseVisualStyleBackColor = True
        '
        'frmCostSplitViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(323, 328)
        Me.Controls.Add(Me.gbExportOptions)
        Me.Controls.Add(Me.lstCosts)
        Me.Controls.Add(Me.btnCopyMats)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCostSplitViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cost Split Viewer"
        Me.gbExportOptions.ResumeLayout(False)
        Me.gbExportOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCopyMats As System.Windows.Forms.Button
    Friend WithEvents lstCosts As EVE_Isk_per_Hour.MyListView
    Friend WithEvents gbExportOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnExportSSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportCSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportDefault As System.Windows.Forms.RadioButton
End Class
