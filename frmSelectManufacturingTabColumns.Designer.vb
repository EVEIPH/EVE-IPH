﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectManufacturingTabColumns
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectManufacturingTabColumns))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.lblInstruction = New System.Windows.Forms.Label()
        Me.lblTip = New System.Windows.Forms.Label()
        Me.chkLstBoxColumns = New System.Windows.Forms.CheckedListBox()
        Me.btnDefaults = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(160, 535)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(97, 25)
        Me.btnCancel.TabIndex = 67
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(41, 535)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(97, 25)
        Me.btnSaveSettings.TabIndex = 69
        Me.btnSaveSettings.Text = "Save"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'lblInstruction
        '
        Me.lblInstruction.Location = New System.Drawing.Point(12, 9)
        Me.lblInstruction.Name = "lblInstruction"
        Me.lblInstruction.Size = New System.Drawing.Size(273, 19)
        Me.lblInstruction.TabIndex = 70
        Me.lblInstruction.Text = "Select the Columns you want to view:"
        '
        'lblTip
        '
        Me.lblTip.Location = New System.Drawing.Point(18, 473)
        Me.lblTip.Name = "lblTip"
        Me.lblTip.Size = New System.Drawing.Size(260, 30)
        Me.lblTip.TabIndex = 71
        Me.lblTip.Text = "Change the order of the columns by dragging them to desired location on the Manuf" & _
    "acturing Tab list."
        Me.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkLstBoxColumns
        '
        Me.chkLstBoxColumns.FormattingEnabled = True
        Me.chkLstBoxColumns.Items.AddRange(New Object() {"Item Category", "Item Group", "Item Name", "Owned", "Tech", "BPME", "BPTE", "Inputs", "Compared", "Total Runs", "Single Invented BPC Runs", "Production Lines", "Laboratory Lines", "Total Invention Cost", "Total Copy Cost", "Taxes", "Broker Fees", "BP Production Time", "Total Production Time", "Copy Time", "Invention Time", "Item Market Price", "Profit", "Profit Percentage", "Isk per Hour", "SVR", "SVR * IPH", "Total Cost", "Base Job Cost", "Num BPs", "Invention Chance", "BP Type", "Race", "Volume per Item", "Total Volume", "Manufacturing Job Fee", "Manufacturing Facility Name", "Manufacturing Facility System", "Manufacturing Facility Region", "Manufacturing Facility System Index", "Manufacturing Facility Tax", "Manufacturing Facility ME Bonus", "Manufacturing Facility TE Bonus", "Manufacturing Facility Usage", "Component Facility Name", "Component Facility System", "Component Facility Region", "Component Facility System Index", "Component Facility Tax", "Component Facility ME Bonus", "Component Facility TE Bonus", "Component Facility Usage", "Copying Facility Name", "Copying Facility System", "Copying Facility Region", "Copying Facility System Index", "Copying Facility Tax", "Copying Facility ME Bonus", "Copying Facility TE Bonus", "Copying Facility Usage", "Invention Facility Name", "Invention Facility System", "Invention Facility Region", "Invention Facility SystemIndex", "Invention Facility Tax", "Invention Facility ME Bonus", "Invention Facility TE Bonus", "Invention Facility Usage"})
        Me.chkLstBoxColumns.Location = New System.Drawing.Point(15, 31)
        Me.chkLstBoxColumns.Name = "chkLstBoxColumns"
        Me.chkLstBoxColumns.Size = New System.Drawing.Size(273, 439)
        Me.chkLstBoxColumns.TabIndex = 72
        '
        'btnDefaults
        '
        Me.btnDefaults.Location = New System.Drawing.Point(101, 504)
        Me.btnDefaults.Name = "btnDefaults"
        Me.btnDefaults.Size = New System.Drawing.Size(97, 25)
        Me.btnDefaults.TabIndex = 73
        Me.btnDefaults.Text = "Reset to Default"
        Me.btnDefaults.UseVisualStyleBackColor = True
        '
        'frmSelectManufacturingTabColumns
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(297, 572)
        Me.Controls.Add(Me.btnDefaults)
        Me.Controls.Add(Me.chkLstBoxColumns)
        Me.Controls.Add(Me.lblTip)
        Me.Controls.Add(Me.lblInstruction)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSelectManufacturingTabColumns"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Columns"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents lblInstruction As System.Windows.Forms.Label
    Friend WithEvents lblTip As System.Windows.Forms.Label
    Friend WithEvents chkLstBoxColumns As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnDefaults As System.Windows.Forms.Button
End Class
