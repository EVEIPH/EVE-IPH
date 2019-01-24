<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmESIStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmESIStatus))
        Me.lstStatus = New EVE_Isk_per_Hour.MyListView()
        Me.colScope = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPurpose = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstStatus
        '
        Me.lstStatus.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colScope, Me.colPurpose, Me.colStatus})
        Me.lstStatus.FullRowSelect = True
        Me.lstStatus.GridLines = True
        Me.lstStatus.HideSelection = False
        Me.lstStatus.Location = New System.Drawing.Point(16, 12)
        Me.lstStatus.MultiSelect = False
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.Size = New System.Drawing.Size(536, 402)
        Me.lstStatus.TabIndex = 36
        Me.lstStatus.TabStop = False
        Me.lstStatus.UseCompatibleStateImageBehavior = False
        Me.lstStatus.View = System.Windows.Forms.View.Details
        '
        'colScope
        '
        Me.colScope.Text = "Scope"
        Me.colScope.Width = 205
        '
        'colPurpose
        '
        Me.colPurpose.Text = "Purpose"
        Me.colPurpose.Width = 250
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(285, 420)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 30)
        Me.btnClose.TabIndex = 37
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(197, 420)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(82, 30)
        Me.btnRefresh.TabIndex = 38
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'frmESIStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 457)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lstStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmESIStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ESI Status Viewer"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstStatus As MyListView
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents colScope As ColumnHeader
    Friend WithEvents colPurpose As ColumnHeader
    Friend WithEvents colStatus As ColumnHeader
End Class
