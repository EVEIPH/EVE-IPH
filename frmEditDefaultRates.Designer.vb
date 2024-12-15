<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditDefaultRates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditDefaultRates))
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblBaseSalesTax = New System.Windows.Forms.Label()
        Me.txtBaseSalesTax = New System.Windows.Forms.TextBox()
        Me.txtBaseBrokerFee = New System.Windows.Forms.TextBox()
        Me.lblBaseBrokerFee = New System.Windows.Forms.Label()
        Me.txtSCCBrokerFeeSurcharge = New System.Windows.Forms.TextBox()
        Me.lblSCCBrokerFeeSurcharge = New System.Windows.Forms.Label()
        Me.txtSCCIndustryFeeSurcharge = New System.Windows.Forms.TextBox()
        Me.lblSCCIndustryFeeSurcharge = New System.Windows.Forms.Label()
        Me.gbIndustryandTaxRates = New System.Windows.Forms.GroupBox()
        Me.txtAlphaAccountTaxRate = New System.Windows.Forms.TextBox()
        Me.lblAlphaAccountTaxRate = New System.Windows.Forms.Label()
        Me.gbStructureRates = New System.Windows.Forms.GroupBox()
        Me.txtDefaultStationTaxRate = New System.Windows.Forms.TextBox()
        Me.lblDefaultStationTaxRate = New System.Windows.Forms.Label()
        Me.txtDefaultStructureTaxRate = New System.Windows.Forms.TextBox()
        Me.lblDefaultStructureTaxRate = New System.Windows.Forms.Label()
        Me.gbIndustryandTaxRates.SuspendLayout()
        Me.gbStructureRates.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(119, 246)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(101, 30)
        Me.btnReset.TabIndex = 33
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(14, 246)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 30)
        Me.btnSave.TabIndex = 32
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(67, 282)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 30)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblBaseSalesTax
        '
        Me.lblBaseSalesTax.Location = New System.Drawing.Point(7, 20)
        Me.lblBaseSalesTax.Name = "lblBaseSalesTax"
        Me.lblBaseSalesTax.Size = New System.Drawing.Size(144, 13)
        Me.lblBaseSalesTax.TabIndex = 35
        Me.lblBaseSalesTax.Text = "Base Sales Tax:"
        Me.lblBaseSalesTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBaseSalesTax
        '
        Me.txtBaseSalesTax.Location = New System.Drawing.Point(157, 17)
        Me.txtBaseSalesTax.Name = "txtBaseSalesTax"
        Me.txtBaseSalesTax.Size = New System.Drawing.Size(51, 20)
        Me.txtBaseSalesTax.TabIndex = 36
        '
        'txtBaseBrokerFee
        '
        Me.txtBaseBrokerFee.Location = New System.Drawing.Point(157, 43)
        Me.txtBaseBrokerFee.Name = "txtBaseBrokerFee"
        Me.txtBaseBrokerFee.Size = New System.Drawing.Size(51, 20)
        Me.txtBaseBrokerFee.TabIndex = 38
        '
        'lblBaseBrokerFee
        '
        Me.lblBaseBrokerFee.Location = New System.Drawing.Point(7, 46)
        Me.lblBaseBrokerFee.Name = "lblBaseBrokerFee"
        Me.lblBaseBrokerFee.Size = New System.Drawing.Size(144, 13)
        Me.lblBaseBrokerFee.TabIndex = 37
        Me.lblBaseBrokerFee.Text = "Base Broker Fee:"
        Me.lblBaseBrokerFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSCCBrokerFeeSurcharge
        '
        Me.txtSCCBrokerFeeSurcharge.Location = New System.Drawing.Point(157, 69)
        Me.txtSCCBrokerFeeSurcharge.Name = "txtSCCBrokerFeeSurcharge"
        Me.txtSCCBrokerFeeSurcharge.Size = New System.Drawing.Size(51, 20)
        Me.txtSCCBrokerFeeSurcharge.TabIndex = 40
        '
        'lblSCCBrokerFeeSurcharge
        '
        Me.lblSCCBrokerFeeSurcharge.Location = New System.Drawing.Point(7, 72)
        Me.lblSCCBrokerFeeSurcharge.Name = "lblSCCBrokerFeeSurcharge"
        Me.lblSCCBrokerFeeSurcharge.Size = New System.Drawing.Size(144, 13)
        Me.lblSCCBrokerFeeSurcharge.TabIndex = 39
        Me.lblSCCBrokerFeeSurcharge.Text = "SCC Broker Fee Surcharge:"
        Me.lblSCCBrokerFeeSurcharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSCCIndustryFeeSurcharge
        '
        Me.txtSCCIndustryFeeSurcharge.Location = New System.Drawing.Point(157, 95)
        Me.txtSCCIndustryFeeSurcharge.Name = "txtSCCIndustryFeeSurcharge"
        Me.txtSCCIndustryFeeSurcharge.Size = New System.Drawing.Size(51, 20)
        Me.txtSCCIndustryFeeSurcharge.TabIndex = 42
        '
        'lblSCCIndustryFeeSurcharge
        '
        Me.lblSCCIndustryFeeSurcharge.Location = New System.Drawing.Point(7, 98)
        Me.lblSCCIndustryFeeSurcharge.Name = "lblSCCIndustryFeeSurcharge"
        Me.lblSCCIndustryFeeSurcharge.Size = New System.Drawing.Size(144, 13)
        Me.lblSCCIndustryFeeSurcharge.TabIndex = 41
        Me.lblSCCIndustryFeeSurcharge.Text = "SCC Industry Fee Surcharge:"
        Me.lblSCCIndustryFeeSurcharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbIndustryandTaxRates
        '
        Me.gbIndustryandTaxRates.Controls.Add(Me.txtAlphaAccountTaxRate)
        Me.gbIndustryandTaxRates.Controls.Add(Me.lblAlphaAccountTaxRate)
        Me.gbIndustryandTaxRates.Controls.Add(Me.lblBaseSalesTax)
        Me.gbIndustryandTaxRates.Controls.Add(Me.txtSCCIndustryFeeSurcharge)
        Me.gbIndustryandTaxRates.Controls.Add(Me.txtBaseSalesTax)
        Me.gbIndustryandTaxRates.Controls.Add(Me.lblSCCIndustryFeeSurcharge)
        Me.gbIndustryandTaxRates.Controls.Add(Me.lblBaseBrokerFee)
        Me.gbIndustryandTaxRates.Controls.Add(Me.txtSCCBrokerFeeSurcharge)
        Me.gbIndustryandTaxRates.Controls.Add(Me.txtBaseBrokerFee)
        Me.gbIndustryandTaxRates.Controls.Add(Me.lblSCCBrokerFeeSurcharge)
        Me.gbIndustryandTaxRates.Location = New System.Drawing.Point(10, 6)
        Me.gbIndustryandTaxRates.Name = "gbIndustryandTaxRates"
        Me.gbIndustryandTaxRates.Size = New System.Drawing.Size(214, 151)
        Me.gbIndustryandTaxRates.TabIndex = 43
        Me.gbIndustryandTaxRates.TabStop = False
        Me.gbIndustryandTaxRates.Text = "Industry && Tax Rates"
        '
        'txtAlphaAccountTaxRate
        '
        Me.txtAlphaAccountTaxRate.Location = New System.Drawing.Point(157, 121)
        Me.txtAlphaAccountTaxRate.Name = "txtAlphaAccountTaxRate"
        Me.txtAlphaAccountTaxRate.Size = New System.Drawing.Size(51, 20)
        Me.txtAlphaAccountTaxRate.TabIndex = 44
        '
        'lblAlphaAccountTaxRate
        '
        Me.lblAlphaAccountTaxRate.Location = New System.Drawing.Point(7, 124)
        Me.lblAlphaAccountTaxRate.Name = "lblAlphaAccountTaxRate"
        Me.lblAlphaAccountTaxRate.Size = New System.Drawing.Size(144, 13)
        Me.lblAlphaAccountTaxRate.TabIndex = 43
        Me.lblAlphaAccountTaxRate.Text = "Alpha Account Tax Rate:"
        Me.lblAlphaAccountTaxRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbStructureRates
        '
        Me.gbStructureRates.Controls.Add(Me.txtDefaultStationTaxRate)
        Me.gbStructureRates.Controls.Add(Me.lblDefaultStationTaxRate)
        Me.gbStructureRates.Controls.Add(Me.txtDefaultStructureTaxRate)
        Me.gbStructureRates.Controls.Add(Me.lblDefaultStructureTaxRate)
        Me.gbStructureRates.Location = New System.Drawing.Point(10, 163)
        Me.gbStructureRates.Name = "gbStructureRates"
        Me.gbStructureRates.Size = New System.Drawing.Size(214, 79)
        Me.gbStructureRates.TabIndex = 44
        Me.gbStructureRates.TabStop = False
        Me.gbStructureRates.Text = "Structure Default Rates:"
        '
        'txtDefaultStationTaxRate
        '
        Me.txtDefaultStationTaxRate.Location = New System.Drawing.Point(157, 45)
        Me.txtDefaultStationTaxRate.Name = "txtDefaultStationTaxRate"
        Me.txtDefaultStationTaxRate.Size = New System.Drawing.Size(51, 20)
        Me.txtDefaultStationTaxRate.TabIndex = 44
        '
        'lblDefaultStationTaxRate
        '
        Me.lblDefaultStationTaxRate.Location = New System.Drawing.Point(7, 48)
        Me.lblDefaultStationTaxRate.Name = "lblDefaultStationTaxRate"
        Me.lblDefaultStationTaxRate.Size = New System.Drawing.Size(144, 13)
        Me.lblDefaultStationTaxRate.TabIndex = 43
        Me.lblDefaultStationTaxRate.Text = "Default Station Tax Rate:"
        Me.lblDefaultStationTaxRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDefaultStructureTaxRate
        '
        Me.txtDefaultStructureTaxRate.Location = New System.Drawing.Point(157, 19)
        Me.txtDefaultStructureTaxRate.Name = "txtDefaultStructureTaxRate"
        Me.txtDefaultStructureTaxRate.Size = New System.Drawing.Size(51, 20)
        Me.txtDefaultStructureTaxRate.TabIndex = 42
        '
        'lblDefaultStructureTaxRate
        '
        Me.lblDefaultStructureTaxRate.Location = New System.Drawing.Point(7, 22)
        Me.lblDefaultStructureTaxRate.Name = "lblDefaultStructureTaxRate"
        Me.lblDefaultStructureTaxRate.Size = New System.Drawing.Size(144, 13)
        Me.lblDefaultStructureTaxRate.TabIndex = 41
        Me.lblDefaultStructureTaxRate.Text = "Default Structure Tax Rate:"
        Me.lblDefaultStructureTaxRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmEditDefaultRates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 319)
        Me.Controls.Add(Me.gbStructureRates)
        Me.Controls.Add(Me.gbIndustryandTaxRates)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditDefaultRates"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Default Rates"
        Me.gbIndustryandTaxRates.ResumeLayout(False)
        Me.gbIndustryandTaxRates.PerformLayout()
        Me.gbStructureRates.ResumeLayout(False)
        Me.gbStructureRates.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnReset As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents lblBaseSalesTax As Label
    Friend WithEvents txtBaseSalesTax As TextBox
    Friend WithEvents txtBaseBrokerFee As TextBox
    Friend WithEvents lblBaseBrokerFee As Label
    Friend WithEvents txtSCCBrokerFeeSurcharge As TextBox
    Friend WithEvents lblSCCBrokerFeeSurcharge As Label
    Friend WithEvents txtSCCIndustryFeeSurcharge As TextBox
    Friend WithEvents lblSCCIndustryFeeSurcharge As Label
    Friend WithEvents gbIndustryandTaxRates As GroupBox
    Friend WithEvents txtAlphaAccountTaxRate As TextBox
    Friend WithEvents lblAlphaAccountTaxRate As Label
    Friend WithEvents gbStructureRates As GroupBox
    Friend WithEvents txtDefaultStationTaxRate As TextBox
    Friend WithEvents lblDefaultStationTaxRate As Label
    Friend WithEvents txtDefaultStructureTaxRate As TextBox
    Friend WithEvents lblDefaultStructureTaxRate As Label
End Class
