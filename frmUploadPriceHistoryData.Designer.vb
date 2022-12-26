<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadPriceHistoryData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUploadPriceHistoryData))
        Me.txtPaste = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lblItemsSplitPrices = New System.Windows.Forms.Label()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbItemGroup = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbRegions = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtPaste
        '
        Me.txtPaste.Location = New System.Drawing.Point(4, 94)
        Me.txtPaste.Multiline = True
        Me.txtPaste.Name = "txtPaste"
        Me.txtPaste.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPaste.Size = New System.Drawing.Size(749, 607)
        Me.txtPaste.TabIndex = 2
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(666, 64)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 24)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(486, 64)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(84, 24)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'lblItemsSplitPrices
        '
        Me.lblItemsSplitPrices.AutoSize = True
        Me.lblItemsSplitPrices.Location = New System.Drawing.Point(12, 51)
        Me.lblItemsSplitPrices.Name = "lblItemsSplitPrices"
        Me.lblItemsSplitPrices.Size = New System.Drawing.Size(136, 13)
        Me.lblItemsSplitPrices.TabIndex = 7
        Me.lblItemsSplitPrices.Text = "Select Item for Data Import:"
        '
        'cmbItems
        '
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.FormattingEnabled = True
        Me.cmbItems.Location = New System.Drawing.Point(15, 66)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(465, 21)
        Me.cmbItems.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Select Item Group:"
        '
        'cmbItemGroup
        '
        Me.cmbItemGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemGroup.FormattingEnabled = True
        Me.cmbItemGroup.Location = New System.Drawing.Point(15, 25)
        Me.cmbItemGroup.Name = "cmbItemGroup"
        Me.cmbItemGroup.Size = New System.Drawing.Size(337, 21)
        Me.cmbItemGroup.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(361, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Select Region:"
        '
        'cmbRegions
        '
        Me.cmbRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRegions.FormattingEnabled = True
        Me.cmbRegions.Location = New System.Drawing.Point(364, 25)
        Me.cmbRegions.Name = "cmbRegions"
        Me.cmbRegions.Size = New System.Drawing.Size(386, 21)
        Me.cmbRegions.TabIndex = 125
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(576, 64)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(84, 24)
        Me.btnClear.TabIndex = 126
        Me.btnClear.Text = "Clear Data"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmUploadPriceHistoryData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 713)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.cmbRegions)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbItemGroup)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbItems)
        Me.Controls.Add(Me.lblItemsSplitPrices)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.txtPaste)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUploadPriceHistoryData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upload Price History Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtPaste As TextBox
    Friend WithEvents btnExit As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents lblItemsSplitPrices As Label
    Friend WithEvents cmbItems As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbItemGroup As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbRegions As ComboBox
    Friend WithEvents btnClear As Button
End Class
