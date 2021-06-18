<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetsViewer
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetsViewer))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkRawMaterialPrices = New System.Windows.Forms.CheckBox()
        Me.mainAssetViewer = New EVE_Isk_per_Hour.AssetViewer()
        Me.SuspendLayout()
        '
        'ttMain
        '
        Me.ttMain.IsBalloon = True
        '
        'chkRawMaterialPrices
        '
        Me.chkRawMaterialPrices.AutoSize = True
        Me.chkRawMaterialPrices.BackColor = System.Drawing.Color.White
        Me.chkRawMaterialPrices.Location = New System.Drawing.Point(6, 1)
        Me.chkRawMaterialPrices.Name = "chkRawMaterialPrices"
        Me.chkRawMaterialPrices.Size = New System.Drawing.Size(93, 17)
        Me.chkRawMaterialPrices.TabIndex = 18
        Me.chkRawMaterialPrices.Text = "Raw Materials"
        Me.chkRawMaterialPrices.UseVisualStyleBackColor = False
        '
        'mainAssetViewer
        '
        Me.mainAssetViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.mainAssetViewer.Location = New System.Drawing.Point(3, 2)
        Me.mainAssetViewer.Name = "mainAssetViewer"
        Me.mainAssetViewer.Size = New System.Drawing.Size(637, 667)
        Me.mainAssetViewer.TabIndex = 0
        '
        'frmAssetsViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(641, 671)
        Me.Controls.Add(Me.mainAssetViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAssetsViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Assets Viewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ttMain As System.Windows.Forms.ToolTip
    Friend WithEvents chkRawMaterialPrices As System.Windows.Forms.CheckBox
    Friend WithEvents mainAssetViewer As AssetViewer
End Class
