<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventionMats
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventionMats))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCopyMats = New System.Windows.Forms.Button()
        Me.lstMats = New EVE_Isk_per_Hour.MyListView()
        Me.gbExportOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnExportSSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportCSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportDefault = New System.Windows.Forms.RadioButton()
        Me.rbtnExportSimple = New System.Windows.Forms.RadioButton()
        Me.gbExportOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(168, 193)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(96, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCopyMats
        '
        Me.btnCopyMats.Location = New System.Drawing.Point(301, 193)
        Me.btnCopyMats.Name = "btnCopyMats"
        Me.btnCopyMats.Size = New System.Drawing.Size(96, 30)
        Me.btnCopyMats.TabIndex = 2
        Me.btnCopyMats.Text = "Copy List"
        Me.btnCopyMats.UseVisualStyleBackColor = True
        '
        'lstMats
        '
        Me.lstMats.FullRowSelect = True
        Me.lstMats.GridLines = True
        Me.lstMats.Location = New System.Drawing.Point(12, 9)
        Me.lstMats.Name = "lstMats"
        Me.lstMats.Size = New System.Drawing.Size(539, 130)
        Me.lstMats.TabIndex = 40
        Me.lstMats.TabStop = False
        Me.lstMats.Tag = "20"
        Me.lstMats.UseCompatibleStateImageBehavior = False
        Me.lstMats.View = System.Windows.Forms.View.Details
        '
        'gbExportOptions
        '
        Me.gbExportOptions.Controls.Add(Me.rbtnExportSimple)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportSSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportCSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportDefault)
        Me.gbExportOptions.Location = New System.Drawing.Point(155, 146)
        Me.gbExportOptions.Name = "gbExportOptions"
        Me.gbExportOptions.Size = New System.Drawing.Size(254, 41)
        Me.gbExportOptions.TabIndex = 75
        Me.gbExportOptions.TabStop = False
        Me.gbExportOptions.Text = "Export Data in:"
        '
        'rbtnExportSSV
        '
        Me.rbtnExportSSV.AutoSize = True
        Me.rbtnExportSSV.Location = New System.Drawing.Point(132, 17)
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
        Me.rbtnExportCSV.Location = New System.Drawing.Point(80, 17)
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
        Me.rbtnExportDefault.Location = New System.Drawing.Point(15, 17)
        Me.rbtnExportDefault.Name = "rbtnExportDefault"
        Me.rbtnExportDefault.Size = New System.Drawing.Size(59, 17)
        Me.rbtnExportDefault.TabIndex = 0
        Me.rbtnExportDefault.TabStop = True
        Me.rbtnExportDefault.Text = "Default"
        Me.rbtnExportDefault.UseVisualStyleBackColor = True
        '
        'rbtnExportSimple
        '
        Me.rbtnExportSimple.AutoSize = True
        Me.rbtnExportSimple.Location = New System.Drawing.Point(184, 17)
        Me.rbtnExportSimple.Name = "rbtnExportSimple"
        Me.rbtnExportSimple.Size = New System.Drawing.Size(56, 17)
        Me.rbtnExportSimple.TabIndex = 3
        Me.rbtnExportSimple.TabStop = True
        Me.rbtnExportSimple.Text = "Simple"
        Me.rbtnExportSimple.UseVisualStyleBackColor = True
        '
        'frmInventionMats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(564, 230)
        Me.Controls.Add(Me.gbExportOptions)
        Me.Controls.Add(Me.lstMats)
        Me.Controls.Add(Me.btnCopyMats)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInventionMats"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmInventionREMats"
        Me.gbExportOptions.ResumeLayout(False)
        Me.gbExportOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCopyMats As System.Windows.Forms.Button
    Friend WithEvents lstMats As EVE_Isk_per_Hour.MyListView
    Friend WithEvents gbExportOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnExportSSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportCSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportDefault As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportSimple As RadioButton
End Class
