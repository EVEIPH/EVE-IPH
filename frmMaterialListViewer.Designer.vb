<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterialListViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaterialListViewer))
        Me.lstMaterials = New EVE_Isk_per_Hour.MyListView()
        Me.Material = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Quantity = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TotalCost = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstMaterials
        '
        Me.lstMaterials.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Material, Me.Quantity, Me.TotalCost})
        Me.lstMaterials.FullRowSelect = True
        Me.lstMaterials.GridLines = True
        Me.lstMaterials.Location = New System.Drawing.Point(11, 12)
        Me.lstMaterials.MultiSelect = False
        Me.lstMaterials.Name = "lstMaterials"
        Me.lstMaterials.Size = New System.Drawing.Size(351, 231)
        Me.lstMaterials.TabIndex = 42
        Me.lstMaterials.TabStop = False
        Me.lstMaterials.Tag = "20"
        Me.lstMaterials.UseCompatibleStateImageBehavior = False
        Me.lstMaterials.View = System.Windows.Forms.View.Details
        '
        'Material
        '
        Me.Material.Text = "Material"
        Me.Material.Width = 170
        '
        'Quantity
        '
        Me.Quantity.Text = "Quantity"
        Me.Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TotalCost
        '
        Me.TotalCost.Text = "Total Sell Cost"
        Me.TotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalCost.Width = 100
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(138, 249)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(96, 30)
        Me.btnOK.TabIndex = 41
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmMaterialListViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(372, 287)
        Me.Controls.Add(Me.lstMaterials)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMaterialListViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Excess Materials List"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstMaterials As MyListView
    Friend WithEvents btnOK As Button
    Friend WithEvents Material As ColumnHeader
    Friend WithEvents Quantity As ColumnHeader
    Friend WithEvents TotalCost As ColumnHeader
End Class
