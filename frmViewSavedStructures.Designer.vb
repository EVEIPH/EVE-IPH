<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewSavedStructures
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewSavedStructures))
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lstStructures = New EVE_Isk_per_Hour.MyListView()
        Me.check = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chStructureName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSystem = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chRegion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(345, 338)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 101
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(239, 338)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(100, 25)
        Me.btnRemove.TabIndex = 102
        Me.btnRemove.Text = "Remove Selected"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lstStructures
        '
        Me.lstStructures.CheckBoxes = True
        Me.lstStructures.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.check, Me.chID, Me.chStructureName, Me.chSystem, Me.chRegion, Me.chStatus})
        Me.lstStructures.FullRowSelect = True
        Me.lstStructures.GridLines = True
        Me.lstStructures.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstStructures.HideSelection = False
        Me.lstStructures.Location = New System.Drawing.Point(12, 12)
        Me.lstStructures.MultiSelect = False
        Me.lstStructures.Name = "lstStructures"
        Me.lstStructures.Size = New System.Drawing.Size(660, 318)
        Me.lstStructures.TabIndex = 1
        Me.lstStructures.UseCompatibleStateImageBehavior = False
        Me.lstStructures.View = System.Windows.Forms.View.Details
        '
        'check
        '
        Me.check.Text = ""
        Me.check.Width = 13
        '
        'chID
        '
        Me.chID.Text = "ID"
        '
        'chStructureName
        '
        Me.chStructureName.Text = "Structure Name"
        Me.chStructureName.Width = 177
        '
        'chSystem
        '
        Me.chSystem.Text = "System"
        '
        'chRegion
        '
        Me.chRegion.Text = "Region"
        '
        'chStatus
        '
        Me.chStatus.Text = "Status"
        Me.chStatus.Width = 115
        '
        'frmViewSavedStructures
        '
        Me.ClientSize = New System.Drawing.Size(684, 375)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lstStructures)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewSavedStructures"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Saved Structures"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstStructures As MyListView
    Friend WithEvents btnExit As Button
    Friend WithEvents chID As ColumnHeader
    Friend WithEvents chStructureName As ColumnHeader
    Friend WithEvents chSystem As ColumnHeader
    Friend WithEvents chRegion As ColumnHeader
    Friend WithEvents btnRemove As Button
    Friend WithEvents chStatus As ColumnHeader
    Friend WithEvents check As ColumnHeader
End Class
