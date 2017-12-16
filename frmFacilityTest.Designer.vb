<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFacilityTest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFacilityTest))
        Me.tabFacility = New System.Windows.Forms.TabPage()
        Me.BPFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabBPInventionEquip = New System.Windows.Forms.TabControl()
        Me.tabCalcFacilities = New System.Windows.Forms.TabControl()
        Me.tabCalcFacilityBase = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityComponents = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityCopy = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityT2Invention = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityT3Invention = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilitySupers = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityCapitals = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityT3Ships = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilitySubsystems = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityBoosters = New System.Windows.Forms.TabPage()
        Me.tabCalcFacilityNoPOS = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.tabFacility.SuspendLayout()
        Me.tabBPInventionEquip.SuspendLayout()
        Me.tabCalcFacilities.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabFacility
        '
        Me.tabFacility.Controls.Add(Me.BPFacility)
        Me.tabFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabFacility.Location = New System.Drawing.Point(4, 4)
        Me.tabFacility.Margin = New System.Windows.Forms.Padding(0)
        Me.tabFacility.Name = "tabFacility"
        Me.tabFacility.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFacility.Size = New System.Drawing.Size(281, 142)
        Me.tabFacility.TabIndex = 1
        Me.tabFacility.Text = "Facility"
        Me.tabFacility.UseVisualStyleBackColor = True
        '
        'BPFacility
        '
        Me.BPFacility.Location = New System.Drawing.Point(0, 0)
        Me.BPFacility.Name = "BPFacility"
        Me.BPFacility.Size = New System.Drawing.Size(281, 142)
        Me.BPFacility.TabIndex = 0
        '
        'tabBPInventionEquip
        '
        Me.tabBPInventionEquip.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.tabBPInventionEquip.Controls.Add(Me.tabFacility)
        Me.tabBPInventionEquip.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tabBPInventionEquip.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.tabBPInventionEquip.ItemSize = New System.Drawing.Size(49, 20)
        Me.tabBPInventionEquip.Location = New System.Drawing.Point(12, 12)
        Me.tabBPInventionEquip.Multiline = True
        Me.tabBPInventionEquip.Name = "tabBPInventionEquip"
        Me.tabBPInventionEquip.Padding = New System.Drawing.Point(0, 0)
        Me.tabBPInventionEquip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tabBPInventionEquip.SelectedIndex = 0
        Me.tabBPInventionEquip.Size = New System.Drawing.Size(309, 150)
        Me.tabBPInventionEquip.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tabBPInventionEquip.TabIndex = 17
        '
        'tabCalcFacilities
        '
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityBase)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityComponents)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityCopy)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityT2Invention)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityT3Invention)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilitySupers)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityCapitals)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityT3Ships)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilitySubsystems)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityBoosters)
        Me.tabCalcFacilities.Controls.Add(Me.tabCalcFacilityNoPOS)
        Me.tabCalcFacilities.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tabCalcFacilities.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.tabCalcFacilities.HotTrack = True
        Me.tabCalcFacilities.ItemSize = New System.Drawing.Size(49, 20)
        Me.tabCalcFacilities.Location = New System.Drawing.Point(11, 228)
        Me.tabCalcFacilities.Multiline = True
        Me.tabCalcFacilities.Name = "tabCalcFacilities"
        Me.tabCalcFacilities.Padding = New System.Drawing.Point(0, 0)
        Me.tabCalcFacilities.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tabCalcFacilities.SelectedIndex = 0
        Me.tabCalcFacilities.Size = New System.Drawing.Size(310, 176)
        Me.tabCalcFacilities.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tabCalcFacilities.TabIndex = 18
        '
        'tabCalcFacilityBase
        '
        Me.tabCalcFacilityBase.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityBase.Margin = New System.Windows.Forms.Padding(0)
        Me.tabCalcFacilityBase.Name = "tabCalcFacilityBase"
        Me.tabCalcFacilityBase.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCalcFacilityBase.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityBase.TabIndex = 1
        Me.tabCalcFacilityBase.Text = "Base"
        Me.tabCalcFacilityBase.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityComponents
        '
        Me.tabCalcFacilityComponents.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityComponents.Name = "tabCalcFacilityComponents"
        Me.tabCalcFacilityComponents.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityComponents.TabIndex = 10
        Me.tabCalcFacilityComponents.Text = "Components"
        Me.tabCalcFacilityComponents.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityCopy
        '
        Me.tabCalcFacilityCopy.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityCopy.Name = "tabCalcFacilityCopy"
        Me.tabCalcFacilityCopy.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityCopy.TabIndex = 3
        Me.tabCalcFacilityCopy.Text = "Copy"
        Me.tabCalcFacilityCopy.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityT2Invention
        '
        Me.tabCalcFacilityT2Invention.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT2Invention.Name = "tabCalcFacilityT2Invention"
        Me.tabCalcFacilityT2Invention.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT2Invention.TabIndex = 2
        Me.tabCalcFacilityT2Invention.Text = "T2 Inv"
        Me.tabCalcFacilityT2Invention.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityT3Invention
        '
        Me.tabCalcFacilityT3Invention.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT3Invention.Name = "tabCalcFacilityT3Invention"
        Me.tabCalcFacilityT3Invention.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT3Invention.TabIndex = 11
        Me.tabCalcFacilityT3Invention.Text = "T3 Inv"
        Me.tabCalcFacilityT3Invention.UseVisualStyleBackColor = True
        '
        'tabCalcFacilitySupers
        '
        Me.tabCalcFacilitySupers.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilitySupers.Name = "tabCalcFacilitySupers"
        Me.tabCalcFacilitySupers.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilitySupers.TabIndex = 6
        Me.tabCalcFacilitySupers.Text = "Supers"
        Me.tabCalcFacilitySupers.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityCapitals
        '
        Me.tabCalcFacilityCapitals.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityCapitals.Name = "tabCalcFacilityCapitals"
        Me.tabCalcFacilityCapitals.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityCapitals.TabIndex = 5
        Me.tabCalcFacilityCapitals.Text = "Capitals"
        Me.tabCalcFacilityCapitals.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityT3Ships
        '
        Me.tabCalcFacilityT3Ships.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT3Ships.Name = "tabCalcFacilityT3Ships"
        Me.tabCalcFacilityT3Ships.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT3Ships.TabIndex = 9
        Me.tabCalcFacilityT3Ships.Text = "T3 Ships"
        Me.tabCalcFacilityT3Ships.UseVisualStyleBackColor = True
        '
        'tabCalcFacilitySubsystems
        '
        Me.tabCalcFacilitySubsystems.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilitySubsystems.Name = "tabCalcFacilitySubsystems"
        Me.tabCalcFacilitySubsystems.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilitySubsystems.TabIndex = 8
        Me.tabCalcFacilitySubsystems.Text = "Subsystems"
        Me.tabCalcFacilitySubsystems.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityBoosters
        '
        Me.tabCalcFacilityBoosters.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityBoosters.Name = "tabCalcFacilityBoosters"
        Me.tabCalcFacilityBoosters.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityBoosters.TabIndex = 7
        Me.tabCalcFacilityBoosters.Text = "Boosters"
        Me.tabCalcFacilityBoosters.UseVisualStyleBackColor = True
        '
        'tabCalcFacilityNoPOS
        '
        Me.tabCalcFacilityNoPOS.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityNoPOS.Name = "tabCalcFacilityNoPOS"
        Me.tabCalcFacilityNoPOS.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityNoPOS.TabIndex = 4
        Me.tabCalcFacilityNoPOS.Text = "No POS"
        Me.tabCalcFacilityNoPOS.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(62, 186)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 21)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Test BP 1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(134, 186)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(66, 21)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "Test BP 2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(206, 186)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(66, 21)
        Me.Button3.TabIndex = 21
        Me.Button3.Text = "Test BP 3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'frmFacilityTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 416)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.tabCalcFacilities)
        Me.Controls.Add(Me.tabBPInventionEquip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFacilityTest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFacilityTest"
        Me.tabFacility.ResumeLayout(False)
        Me.tabBPInventionEquip.ResumeLayout(False)
        Me.tabCalcFacilities.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabFacility As TabPage
    Friend WithEvents tabBPInventionEquip As TabControl
    Friend WithEvents tabCalcFacilities As TabControl
    Friend WithEvents tabCalcFacilityBase As TabPage
    Friend WithEvents tabCalcFacilityComponents As TabPage
    Friend WithEvents tabCalcFacilityCopy As TabPage
    Friend WithEvents tabCalcFacilityT2Invention As TabPage
    Friend WithEvents tabCalcFacilityT3Invention As TabPage
    Friend WithEvents tabCalcFacilitySupers As TabPage
    Friend WithEvents tabCalcFacilityCapitals As TabPage
    Friend WithEvents tabCalcFacilityT3Ships As TabPage
    Friend WithEvents tabCalcFacilitySubsystems As TabPage
    Friend WithEvents tabCalcFacilityBoosters As TabPage
    Friend WithEvents tabCalcFacilityNoPOS As TabPage
    Friend WithEvents BPFacility As ManufacturingFacility
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
