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
        Me.MainFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabBPInventionEquip = New System.Windows.Forms.TabControl()
        Me.capitals = New System.Windows.Forms.TabControl()
        Me.tabCalcFacilityBase = New System.Windows.Forms.TabPage()
        Me.BPFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityComponents = New System.Windows.Forms.TabPage()
        Me.compFacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityCopy = New System.Windows.Forms.TabPage()
        Me.copyfacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityT2Invention = New System.Windows.Forms.TabPage()
        Me.t2inventionfacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityT3Invention = New System.Windows.Forms.TabPage()
        Me.t3inventionfacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilitySupers = New System.Windows.Forms.TabPage()
        Me.superfacility = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityCapitals = New System.Windows.Forms.TabPage()
        Me.capitalsf = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityT3Ships = New System.Windows.Forms.TabPage()
        Me.t3ships = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilitySubsystems = New System.Windows.Forms.TabPage()
        Me.subsystems = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityBoosters = New System.Windows.Forms.TabPage()
        Me.boosters = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.tabCalcFacilityNoPOS = New System.Windows.Forms.TabPage()
        Me.nopos = New EVE_Isk_per_Hour.ManufacturingFacility()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.tabFacility.SuspendLayout()
        Me.tabBPInventionEquip.SuspendLayout()
        Me.capitals.SuspendLayout()
        Me.tabCalcFacilityBase.SuspendLayout()
        Me.tabCalcFacilityComponents.SuspendLayout()
        Me.tabCalcFacilityCopy.SuspendLayout()
        Me.tabCalcFacilityT2Invention.SuspendLayout()
        Me.tabCalcFacilityT3Invention.SuspendLayout()
        Me.tabCalcFacilitySupers.SuspendLayout()
        Me.tabCalcFacilityCapitals.SuspendLayout()
        Me.tabCalcFacilityT3Ships.SuspendLayout()
        Me.tabCalcFacilitySubsystems.SuspendLayout()
        Me.tabCalcFacilityBoosters.SuspendLayout()
        Me.tabCalcFacilityNoPOS.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabFacility
        '
        Me.tabFacility.Controls.Add(Me.MainFacility)
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
        'MainFacility
        '
        Me.MainFacility.Location = New System.Drawing.Point(0, 0)
        Me.MainFacility.Name = "MainFacility"
        Me.MainFacility.Size = New System.Drawing.Size(281, 142)
        Me.MainFacility.TabIndex = 1
        '
        'tabBPInventionEquip
        '
        Me.tabBPInventionEquip.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.tabBPInventionEquip.Controls.Add(Me.tabFacility)
        Me.tabBPInventionEquip.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tabBPInventionEquip.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.tabBPInventionEquip.ItemSize = New System.Drawing.Size(49, 20)
        Me.tabBPInventionEquip.Location = New System.Drawing.Point(6, 227)
        Me.tabBPInventionEquip.Multiline = True
        Me.tabBPInventionEquip.Name = "tabBPInventionEquip"
        Me.tabBPInventionEquip.Padding = New System.Drawing.Point(0, 0)
        Me.tabBPInventionEquip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tabBPInventionEquip.SelectedIndex = 0
        Me.tabBPInventionEquip.Size = New System.Drawing.Size(309, 150)
        Me.tabBPInventionEquip.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tabBPInventionEquip.TabIndex = 17
        '
        'capitals
        '
        Me.capitals.Controls.Add(Me.tabCalcFacilityBase)
        Me.capitals.Controls.Add(Me.tabCalcFacilityComponents)
        Me.capitals.Controls.Add(Me.tabCalcFacilityCopy)
        Me.capitals.Controls.Add(Me.tabCalcFacilityT2Invention)
        Me.capitals.Controls.Add(Me.tabCalcFacilityT3Invention)
        Me.capitals.Controls.Add(Me.tabCalcFacilitySupers)
        Me.capitals.Controls.Add(Me.tabCalcFacilityCapitals)
        Me.capitals.Controls.Add(Me.tabCalcFacilityT3Ships)
        Me.capitals.Controls.Add(Me.tabCalcFacilitySubsystems)
        Me.capitals.Controls.Add(Me.tabCalcFacilityBoosters)
        Me.capitals.Controls.Add(Me.tabCalcFacilityNoPOS)
        Me.capitals.DataBindings.Add(New System.Windows.Forms.Binding("Font", Global.EVE_Isk_per_Hour.My.MySettings.Default, "MyDefault", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.capitals.Font = Global.EVE_Isk_per_Hour.My.MySettings.Default.MyDefault
        Me.capitals.HotTrack = True
        Me.capitals.ItemSize = New System.Drawing.Size(49, 20)
        Me.capitals.Location = New System.Drawing.Point(2, 4)
        Me.capitals.Multiline = True
        Me.capitals.Name = "capitals"
        Me.capitals.Padding = New System.Drawing.Point(0, 0)
        Me.capitals.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.capitals.SelectedIndex = 0
        Me.capitals.Size = New System.Drawing.Size(310, 176)
        Me.capitals.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.capitals.TabIndex = 18
        '
        'tabCalcFacilityBase
        '
        Me.tabCalcFacilityBase.Controls.Add(Me.BPFacility)
        Me.tabCalcFacilityBase.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityBase.Margin = New System.Windows.Forms.Padding(0)
        Me.tabCalcFacilityBase.Name = "tabCalcFacilityBase"
        Me.tabCalcFacilityBase.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCalcFacilityBase.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityBase.TabIndex = 1
        Me.tabCalcFacilityBase.Text = "Base"
        Me.tabCalcFacilityBase.UseVisualStyleBackColor = True
        '
        'BPFacility
        '
        Me.BPFacility.Location = New System.Drawing.Point(0, 0)
        Me.BPFacility.Name = "BPFacility"
        Me.BPFacility.Size = New System.Drawing.Size(302, 128)
        Me.BPFacility.TabIndex = 0
        '
        'tabCalcFacilityComponents
        '
        Me.tabCalcFacilityComponents.Controls.Add(Me.compFacility)
        Me.tabCalcFacilityComponents.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityComponents.Name = "tabCalcFacilityComponents"
        Me.tabCalcFacilityComponents.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityComponents.TabIndex = 10
        Me.tabCalcFacilityComponents.Text = "Components"
        Me.tabCalcFacilityComponents.UseVisualStyleBackColor = True
        '
        'compFacility
        '
        Me.compFacility.Location = New System.Drawing.Point(0, 0)
        Me.compFacility.Name = "compFacility"
        Me.compFacility.Size = New System.Drawing.Size(302, 128)
        Me.compFacility.TabIndex = 1
        '
        'tabCalcFacilityCopy
        '
        Me.tabCalcFacilityCopy.Controls.Add(Me.copyfacility)
        Me.tabCalcFacilityCopy.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityCopy.Name = "tabCalcFacilityCopy"
        Me.tabCalcFacilityCopy.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityCopy.TabIndex = 3
        Me.tabCalcFacilityCopy.Text = "Copy"
        Me.tabCalcFacilityCopy.UseVisualStyleBackColor = True
        '
        'copyfacility
        '
        Me.copyfacility.Location = New System.Drawing.Point(0, 0)
        Me.copyfacility.Name = "copyfacility"
        Me.copyfacility.Size = New System.Drawing.Size(302, 128)
        Me.copyfacility.TabIndex = 1
        '
        'tabCalcFacilityT2Invention
        '
        Me.tabCalcFacilityT2Invention.Controls.Add(Me.t2inventionfacility)
        Me.tabCalcFacilityT2Invention.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT2Invention.Name = "tabCalcFacilityT2Invention"
        Me.tabCalcFacilityT2Invention.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT2Invention.TabIndex = 2
        Me.tabCalcFacilityT2Invention.Text = "T2 Inv"
        Me.tabCalcFacilityT2Invention.UseVisualStyleBackColor = True
        '
        't2inventionfacility
        '
        Me.t2inventionfacility.Location = New System.Drawing.Point(0, 0)
        Me.t2inventionfacility.Name = "t2inventionfacility"
        Me.t2inventionfacility.Size = New System.Drawing.Size(302, 128)
        Me.t2inventionfacility.TabIndex = 1
        '
        'tabCalcFacilityT3Invention
        '
        Me.tabCalcFacilityT3Invention.Controls.Add(Me.t3inventionfacility)
        Me.tabCalcFacilityT3Invention.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT3Invention.Name = "tabCalcFacilityT3Invention"
        Me.tabCalcFacilityT3Invention.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT3Invention.TabIndex = 11
        Me.tabCalcFacilityT3Invention.Text = "T3 Inv"
        Me.tabCalcFacilityT3Invention.UseVisualStyleBackColor = True
        '
        't3inventionfacility
        '
        Me.t3inventionfacility.Location = New System.Drawing.Point(0, 0)
        Me.t3inventionfacility.Name = "t3inventionfacility"
        Me.t3inventionfacility.Size = New System.Drawing.Size(302, 128)
        Me.t3inventionfacility.TabIndex = 1
        '
        'tabCalcFacilitySupers
        '
        Me.tabCalcFacilitySupers.Controls.Add(Me.superfacility)
        Me.tabCalcFacilitySupers.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilitySupers.Name = "tabCalcFacilitySupers"
        Me.tabCalcFacilitySupers.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilitySupers.TabIndex = 6
        Me.tabCalcFacilitySupers.Text = "Supers"
        Me.tabCalcFacilitySupers.UseVisualStyleBackColor = True
        '
        'superfacility
        '
        Me.superfacility.Location = New System.Drawing.Point(0, 0)
        Me.superfacility.Name = "superfacility"
        Me.superfacility.Size = New System.Drawing.Size(302, 128)
        Me.superfacility.TabIndex = 2
        '
        'tabCalcFacilityCapitals
        '
        Me.tabCalcFacilityCapitals.Controls.Add(Me.capitalsf)
        Me.tabCalcFacilityCapitals.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityCapitals.Name = "tabCalcFacilityCapitals"
        Me.tabCalcFacilityCapitals.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityCapitals.TabIndex = 5
        Me.tabCalcFacilityCapitals.Text = "Capitals"
        Me.tabCalcFacilityCapitals.UseVisualStyleBackColor = True
        '
        'capitalsf
        '
        Me.capitalsf.Location = New System.Drawing.Point(0, 0)
        Me.capitalsf.Name = "capitalsf"
        Me.capitalsf.Size = New System.Drawing.Size(302, 128)
        Me.capitalsf.TabIndex = 1
        '
        'tabCalcFacilityT3Ships
        '
        Me.tabCalcFacilityT3Ships.Controls.Add(Me.t3ships)
        Me.tabCalcFacilityT3Ships.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityT3Ships.Name = "tabCalcFacilityT3Ships"
        Me.tabCalcFacilityT3Ships.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityT3Ships.TabIndex = 9
        Me.tabCalcFacilityT3Ships.Text = "T3 Ships"
        Me.tabCalcFacilityT3Ships.UseVisualStyleBackColor = True
        '
        't3ships
        '
        Me.t3ships.Location = New System.Drawing.Point(0, 0)
        Me.t3ships.Name = "t3ships"
        Me.t3ships.Size = New System.Drawing.Size(302, 128)
        Me.t3ships.TabIndex = 1
        '
        'tabCalcFacilitySubsystems
        '
        Me.tabCalcFacilitySubsystems.Controls.Add(Me.subsystems)
        Me.tabCalcFacilitySubsystems.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilitySubsystems.Name = "tabCalcFacilitySubsystems"
        Me.tabCalcFacilitySubsystems.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilitySubsystems.TabIndex = 8
        Me.tabCalcFacilitySubsystems.Text = "Subsystems"
        Me.tabCalcFacilitySubsystems.UseVisualStyleBackColor = True
        '
        'subsystems
        '
        Me.subsystems.Location = New System.Drawing.Point(0, 0)
        Me.subsystems.Name = "subsystems"
        Me.subsystems.Size = New System.Drawing.Size(302, 128)
        Me.subsystems.TabIndex = 1
        '
        'tabCalcFacilityBoosters
        '
        Me.tabCalcFacilityBoosters.Controls.Add(Me.boosters)
        Me.tabCalcFacilityBoosters.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityBoosters.Name = "tabCalcFacilityBoosters"
        Me.tabCalcFacilityBoosters.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityBoosters.TabIndex = 7
        Me.tabCalcFacilityBoosters.Text = "Boosters"
        Me.tabCalcFacilityBoosters.UseVisualStyleBackColor = True
        '
        'boosters
        '
        Me.boosters.Location = New System.Drawing.Point(0, 0)
        Me.boosters.Name = "boosters"
        Me.boosters.Size = New System.Drawing.Size(302, 128)
        Me.boosters.TabIndex = 1
        '
        'tabCalcFacilityNoPOS
        '
        Me.tabCalcFacilityNoPOS.Controls.Add(Me.nopos)
        Me.tabCalcFacilityNoPOS.Location = New System.Drawing.Point(4, 44)
        Me.tabCalcFacilityNoPOS.Name = "tabCalcFacilityNoPOS"
        Me.tabCalcFacilityNoPOS.Size = New System.Drawing.Size(302, 128)
        Me.tabCalcFacilityNoPOS.TabIndex = 4
        Me.tabCalcFacilityNoPOS.Text = "No POS"
        Me.tabCalcFacilityNoPOS.UseVisualStyleBackColor = True
        '
        'nopos
        '
        Me.nopos.Location = New System.Drawing.Point(0, 0)
        Me.nopos.Name = "nopos"
        Me.nopos.Size = New System.Drawing.Size(302, 128)
        Me.nopos.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(54, 186)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 21)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Test BP 1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(126, 186)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(66, 21)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "Test BP 2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(198, 186)
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
        Me.ClientSize = New System.Drawing.Size(317, 416)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.capitals)
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
        Me.capitals.ResumeLayout(False)
        Me.tabCalcFacilityBase.ResumeLayout(False)
        Me.tabCalcFacilityComponents.ResumeLayout(False)
        Me.tabCalcFacilityCopy.ResumeLayout(False)
        Me.tabCalcFacilityT2Invention.ResumeLayout(False)
        Me.tabCalcFacilityT3Invention.ResumeLayout(False)
        Me.tabCalcFacilitySupers.ResumeLayout(False)
        Me.tabCalcFacilityCapitals.ResumeLayout(False)
        Me.tabCalcFacilityT3Ships.ResumeLayout(False)
        Me.tabCalcFacilitySubsystems.ResumeLayout(False)
        Me.tabCalcFacilityBoosters.ResumeLayout(False)
        Me.tabCalcFacilityNoPOS.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabFacility As TabPage
    Friend WithEvents tabBPInventionEquip As TabControl
    Friend WithEvents capitals As TabControl
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
    Friend WithEvents MainFacility As ManufacturingFacility
    Friend WithEvents compFacility As ManufacturingFacility
    Friend WithEvents copyfacility As ManufacturingFacility
    Friend WithEvents t2inventionfacility As ManufacturingFacility
    Friend WithEvents t3inventionfacility As ManufacturingFacility
    Friend WithEvents superfacility As ManufacturingFacility
    Friend WithEvents capitalsf As ManufacturingFacility
    Friend WithEvents t3ships As ManufacturingFacility
    Friend WithEvents subsystems As ManufacturingFacility
    Friend WithEvents boosters As ManufacturingFacility
    Friend WithEvents nopos As ManufacturingFacility
End Class
