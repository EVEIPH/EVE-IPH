<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShoppingList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShoppingList))
        Me.lblTotalCost = New System.Windows.Forms.Label()
        Me.lblTC = New System.Windows.Forms.Label()
        Me.lblTV = New System.Windows.Forms.Label()
        Me.lblTotalVolume = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.DeleteBuildStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteBuildItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteItemStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblTotalProfit1 = New System.Windows.Forms.Label()
        Me.lblTotalProfit = New System.Windows.Forms.Label()
        Me.lblAvgIPH = New System.Windows.Forms.Label()
        Me.lblAvgIPH1 = New System.Windows.Forms.Label()
        Me.lblTotalBuiltVolume = New System.Windows.Forms.Label()
        Me.lblTotalBuiltVolume1 = New System.Windows.Forms.Label()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnUpdateListwithAssets = New System.Windows.Forms.Button()
        Me.chkUpdateAssetsWhenUsed = New System.Windows.Forms.CheckBox()
        Me.DeleteMaterialStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteMaterial = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSaveListToFile = New System.Windows.Forms.Button()
        Me.btnLoadListFromFile = New System.Windows.Forms.Button()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.lblFeeRate = New System.Windows.Forms.Label()
        Me.chkBuyorBuyOrder = New System.Windows.Forms.CheckBox()
        Me.txtBrokerFeeRate = New System.Windows.Forms.TextBox()
        Me.chkAlwaysOnTop = New System.Windows.Forms.CheckBox()
        Me.lblUsage = New System.Windows.Forms.Label()
        Me.chkUsage = New System.Windows.Forms.CheckBox()
        Me.lblFees = New System.Windows.Forms.Label()
        Me.gbExportOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnExportMulitBuy = New System.Windows.Forms.RadioButton()
        Me.rbtnExportSSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportCSV = New System.Windows.Forms.RadioButton()
        Me.rbtnExportDefault = New System.Windows.Forms.RadioButton()
        Me.txtAddlCosts = New System.Windows.Forms.TextBox()
        Me.chkFees = New System.Windows.Forms.CheckBox()
        Me.lblAddlCosts = New System.Windows.Forms.Label()
        Me.chkRebuildItemsfromList = New System.Windows.Forms.CheckBox()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.lblItemstoBuy = New System.Windows.Forms.Label()
        Me.lblItemstoBuild = New System.Windows.Forms.Label()
        Me.lblComponentstoBuild = New System.Windows.Forms.Label()
        Me.btnCopyPasteAssets = New System.Windows.Forms.Button()
        Me.btnShowAssets = New System.Windows.Forms.Button()
        Me.gbUpdateList = New System.Windows.Forms.GroupBox()
        Me.lblTotalInventionCost = New System.Windows.Forms.Label()
        Me.lblTotalCopyCost = New System.Windows.Forms.Label()
        Me.lblTIC = New System.Windows.Forms.Label()
        Me.lblTCC = New System.Windows.Forms.Label()
        Me.lblTotalItemsInList = New System.Windows.Forms.Label()
        Me.lstBuy = New EVE_Isk_per_Hour.MyListView()
        Me.lstItems = New EVE_Isk_per_Hour.MyListView()
        Me.lstBuild = New EVE_Isk_per_Hour.MyListView()
        Me.DeleteBuildStrip.SuspendLayout()
        Me.DeleteItemStrip.SuspendLayout()
        Me.DeleteMaterialStrip.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.gbExportOptions.SuspendLayout()
        Me.gbUpdateList.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTotalCost
        '
        Me.lblTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalCost.Location = New System.Drawing.Point(925, 547)
        Me.lblTotalCost.Name = "lblTotalCost"
        Me.lblTotalCost.Size = New System.Drawing.Size(163, 16)
        Me.lblTotalCost.TabIndex = 23
        Me.lblTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTC
        '
        Me.lblTC.AutoSize = True
        Me.lblTC.Location = New System.Drawing.Point(866, 549)
        Me.lblTC.Name = "lblTC"
        Me.lblTC.Size = New System.Drawing.Size(58, 13)
        Me.lblTC.TabIndex = 22
        Me.lblTC.Text = "Total Cost:"
        Me.lblTC.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTV
        '
        Me.lblTV.AutoSize = True
        Me.lblTV.Location = New System.Drawing.Point(852, 606)
        Me.lblTV.Name = "lblTV"
        Me.lblTV.Size = New System.Drawing.Size(72, 13)
        Me.lblTV.TabIndex = 28
        Me.lblTV.Text = "Total Volume:"
        Me.lblTV.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalVolume
        '
        Me.lblTotalVolume.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalVolume.Location = New System.Drawing.Point(925, 604)
        Me.lblTotalVolume.Name = "lblTotalVolume"
        Me.lblTotalVolume.Size = New System.Drawing.Size(163, 16)
        Me.lblTotalVolume.TabIndex = 29
        Me.lblTotalVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(107, 645)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(94, 32)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Exit"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(7, 645)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(94, 32)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear List"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(7, 543)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(94, 32)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.Text = "Copy List"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'DeleteBuildStrip
        '
        Me.DeleteBuildStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteBuildItem})
        Me.DeleteBuildStrip.Name = "ContextMenuStrip1"
        Me.DeleteBuildStrip.Size = New System.Drawing.Size(165, 26)
        '
        'DeleteBuildItem
        '
        Me.DeleteBuildItem.Name = "DeleteBuildItem"
        Me.DeleteBuildItem.Size = New System.Drawing.Size(164, 22)
        Me.DeleteBuildItem.Text = "Delete Build Item"
        '
        'DeleteItemStrip
        '
        Me.DeleteItemStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteItem})
        Me.DeleteItemStrip.Name = "ContextMenuStrip1"
        Me.DeleteItemStrip.Size = New System.Drawing.Size(135, 26)
        '
        'DeleteItem
        '
        Me.DeleteItem.Name = "DeleteItem"
        Me.DeleteItem.Size = New System.Drawing.Size(134, 22)
        Me.DeleteItem.Text = "Delete Item"
        '
        'lblTotalProfit1
        '
        Me.lblTotalProfit1.AutoSize = True
        Me.lblTotalProfit1.Location = New System.Drawing.Point(824, 663)
        Me.lblTotalProfit1.Name = "lblTotalProfit1"
        Me.lblTotalProfit1.Size = New System.Drawing.Size(100, 13)
        Me.lblTotalProfit1.TabIndex = 34
        Me.lblTotalProfit1.Text = "Approx. Total Profit:"
        Me.lblTotalProfit1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalProfit
        '
        Me.lblTotalProfit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalProfit.Location = New System.Drawing.Point(925, 661)
        Me.lblTotalProfit.Name = "lblTotalProfit"
        Me.lblTotalProfit.Size = New System.Drawing.Size(163, 16)
        Me.lblTotalProfit.TabIndex = 35
        Me.lblTotalProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAvgIPH
        '
        Me.lblAvgIPH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvgIPH.Location = New System.Drawing.Point(925, 642)
        Me.lblAvgIPH.Name = "lblAvgIPH"
        Me.lblAvgIPH.Size = New System.Drawing.Size(163, 16)
        Me.lblAvgIPH.TabIndex = 33
        Me.lblAvgIPH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAvgIPH1
        '
        Me.lblAvgIPH1.AutoSize = True
        Me.lblAvgIPH1.Location = New System.Drawing.Point(832, 644)
        Me.lblAvgIPH1.Name = "lblAvgIPH1"
        Me.lblAvgIPH1.Size = New System.Drawing.Size(92, 13)
        Me.lblAvgIPH1.TabIndex = 32
        Me.lblAvgIPH1.Text = "Approx. Avg. IPH:"
        Me.lblAvgIPH1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalBuiltVolume
        '
        Me.lblTotalBuiltVolume.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalBuiltVolume.Location = New System.Drawing.Point(925, 623)
        Me.lblTotalBuiltVolume.Name = "lblTotalBuiltVolume"
        Me.lblTotalBuiltVolume.Size = New System.Drawing.Size(163, 16)
        Me.lblTotalBuiltVolume.TabIndex = 31
        Me.lblTotalBuiltVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBuiltVolume1
        '
        Me.lblTotalBuiltVolume1.AutoSize = True
        Me.lblTotalBuiltVolume1.Location = New System.Drawing.Point(795, 625)
        Me.lblTotalBuiltVolume1.Name = "lblTotalBuiltVolume1"
        Me.lblTotalBuiltVolume1.Size = New System.Drawing.Size(129, 13)
        Me.lblTotalBuiltVolume1.TabIndex = 30
        Me.lblTotalBuiltVolume1.Text = "Total Built Item(s) Volume:"
        Me.lblTotalBuiltVolume1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ttMain
        '
        Me.ttMain.IsBalloon = True
        '
        'btnUpdateListwithAssets
        '
        Me.btnUpdateListwithAssets.Location = New System.Drawing.Point(8, 15)
        Me.btnUpdateListwithAssets.Name = "btnUpdateListwithAssets"
        Me.btnUpdateListwithAssets.Size = New System.Drawing.Size(116, 48)
        Me.btnUpdateListwithAssets.TabIndex = 19
        Me.btnUpdateListwithAssets.Text = "Update with Selected Assets"
        Me.btnUpdateListwithAssets.UseVisualStyleBackColor = True
        '
        'chkUpdateAssetsWhenUsed
        '
        Me.chkUpdateAssetsWhenUsed.AutoSize = True
        Me.chkUpdateAssetsWhenUsed.Checked = True
        Me.chkUpdateAssetsWhenUsed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUpdateAssetsWhenUsed.Location = New System.Drawing.Point(6, 99)
        Me.chkUpdateAssetsWhenUsed.Name = "chkUpdateAssetsWhenUsed"
        Me.chkUpdateAssetsWhenUsed.Size = New System.Drawing.Size(155, 17)
        Me.chkUpdateAssetsWhenUsed.TabIndex = 10
        Me.chkUpdateAssetsWhenUsed.Text = "Update Assets When Used"
        Me.chkUpdateAssetsWhenUsed.UseVisualStyleBackColor = True
        '
        'DeleteMaterialStrip
        '
        Me.DeleteMaterialStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteMaterial})
        Me.DeleteMaterialStrip.Name = "ContextMenuStrip1"
        Me.DeleteMaterialStrip.Size = New System.Drawing.Size(154, 26)
        '
        'DeleteMaterial
        '
        Me.DeleteMaterial.Name = "DeleteMaterial"
        Me.DeleteMaterial.Size = New System.Drawing.Size(153, 22)
        Me.DeleteMaterial.Text = "Delete Material"
        '
        'btnSaveListToFile
        '
        Me.btnSaveListToFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveListToFile.Location = New System.Drawing.Point(7, 577)
        Me.btnSaveListToFile.Name = "btnSaveListToFile"
        Me.btnSaveListToFile.Size = New System.Drawing.Size(94, 32)
        Me.btnSaveListToFile.TabIndex = 2
        Me.btnSaveListToFile.Text = "Save List"
        Me.btnSaveListToFile.UseVisualStyleBackColor = True
        '
        'btnLoadListFromFile
        '
        Me.btnLoadListFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnLoadListFromFile.Location = New System.Drawing.Point(7, 611)
        Me.btnLoadListFromFile.Name = "btnLoadListFromFile"
        Me.btnLoadListFromFile.Size = New System.Drawing.Size(94, 32)
        Me.btnLoadListFromFile.TabIndex = 4
        Me.btnLoadListFromFile.Text = "Load List"
        Me.btnLoadListFromFile.UseVisualStyleBackColor = True
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.lblFeeRate)
        Me.gbOptions.Controls.Add(Me.chkBuyorBuyOrder)
        Me.gbOptions.Controls.Add(Me.txtBrokerFeeRate)
        Me.gbOptions.Controls.Add(Me.chkAlwaysOnTop)
        Me.gbOptions.Controls.Add(Me.lblUsage)
        Me.gbOptions.Controls.Add(Me.chkUsage)
        Me.gbOptions.Controls.Add(Me.lblFees)
        Me.gbOptions.Controls.Add(Me.gbExportOptions)
        Me.gbOptions.Controls.Add(Me.txtAddlCosts)
        Me.gbOptions.Controls.Add(Me.chkFees)
        Me.gbOptions.Controls.Add(Me.chkUpdateAssetsWhenUsed)
        Me.gbOptions.Controls.Add(Me.lblAddlCosts)
        Me.gbOptions.Location = New System.Drawing.Point(214, 538)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(321, 141)
        Me.gbOptions.TabIndex = 8
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options:"
        '
        'lblFeeRate
        '
        Me.lblFeeRate.AutoSize = True
        Me.lblFeeRate.Location = New System.Drawing.Point(189, 17)
        Me.lblFeeRate.Name = "lblFeeRate"
        Me.lblFeeRate.Size = New System.Drawing.Size(54, 13)
        Me.lblFeeRate.TabIndex = 80
        Me.lblFeeRate.Text = "Fee Rate:"
        Me.lblFeeRate.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkBuyorBuyOrder
        '
        Me.chkBuyorBuyOrder.AutoSize = True
        Me.chkBuyorBuyOrder.Checked = True
        Me.chkBuyorBuyOrder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBuyorBuyOrder.Location = New System.Drawing.Point(6, 35)
        Me.chkBuyorBuyOrder.Name = "chkBuyorBuyOrder"
        Me.chkBuyorBuyOrder.Size = New System.Drawing.Size(185, 17)
        Me.chkBuyorBuyOrder.TabIndex = 13
        Me.chkBuyorBuyOrder.Text = "Calculate Buy Order / Buy Market"
        Me.chkBuyorBuyOrder.ThreeState = True
        Me.chkBuyorBuyOrder.UseVisualStyleBackColor = True
        '
        'txtBrokerFeeRate
        '
        Me.txtBrokerFeeRate.Location = New System.Drawing.Point(249, 13)
        Me.txtBrokerFeeRate.Name = "txtBrokerFeeRate"
        Me.txtBrokerFeeRate.Size = New System.Drawing.Size(48, 20)
        Me.txtBrokerFeeRate.TabIndex = 79
        Me.txtBrokerFeeRate.TabStop = False
        Me.txtBrokerFeeRate.Visible = False
        '
        'chkAlwaysOnTop
        '
        Me.chkAlwaysOnTop.AutoSize = True
        Me.chkAlwaysOnTop.Location = New System.Drawing.Point(6, 117)
        Me.chkAlwaysOnTop.Name = "chkAlwaysOnTop"
        Me.chkAlwaysOnTop.Size = New System.Drawing.Size(96, 17)
        Me.chkAlwaysOnTop.TabIndex = 9
        Me.chkAlwaysOnTop.Text = "Always on Top"
        Me.chkAlwaysOnTop.UseVisualStyleBackColor = True
        '
        'lblUsage
        '
        Me.lblUsage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUsage.Location = New System.Drawing.Point(63, 55)
        Me.lblUsage.Name = "lblUsage"
        Me.lblUsage.Size = New System.Drawing.Size(112, 17)
        Me.lblUsage.TabIndex = 15
        Me.lblUsage.Text = "0.00"
        Me.lblUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkUsage
        '
        Me.chkUsage.AutoSize = True
        Me.chkUsage.Checked = True
        Me.chkUsage.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUsage.Location = New System.Drawing.Point(6, 55)
        Me.chkUsage.Name = "chkUsage"
        Me.chkUsage.Size = New System.Drawing.Size(60, 17)
        Me.chkUsage.TabIndex = 14
        Me.chkUsage.Text = "Usage:"
        Me.chkUsage.UseVisualStyleBackColor = True
        '
        'lblFees
        '
        Me.lblFees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFees.Location = New System.Drawing.Point(62, 15)
        Me.lblFees.Name = "lblFees"
        Me.lblFees.Size = New System.Drawing.Size(113, 17)
        Me.lblFees.TabIndex = 12
        Me.lblFees.Text = "0.00"
        Me.lblFees.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbExportOptions
        '
        Me.gbExportOptions.Controls.Add(Me.rbtnExportMulitBuy)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportSSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportCSV)
        Me.gbExportOptions.Controls.Add(Me.rbtnExportDefault)
        Me.gbExportOptions.Location = New System.Drawing.Point(192, 50)
        Me.gbExportOptions.Name = "gbExportOptions"
        Me.gbExportOptions.Size = New System.Drawing.Size(123, 86)
        Me.gbExportOptions.TabIndex = 74
        Me.gbExportOptions.TabStop = False
        Me.gbExportOptions.Text = "Export Data in:"
        '
        'rbtnExportMulitBuy
        '
        Me.rbtnExportMulitBuy.AutoSize = True
        Me.rbtnExportMulitBuy.Location = New System.Drawing.Point(8, 15)
        Me.rbtnExportMulitBuy.Name = "rbtnExportMulitBuy"
        Me.rbtnExportMulitBuy.Size = New System.Drawing.Size(114, 17)
        Me.rbtnExportMulitBuy.TabIndex = 3
        Me.rbtnExportMulitBuy.TabStop = True
        Me.rbtnExportMulitBuy.Text = "Multi-Buy (Buy List)"
        Me.rbtnExportMulitBuy.UseVisualStyleBackColor = True
        '
        'rbtnExportSSV
        '
        Me.rbtnExportSSV.AutoSize = True
        Me.rbtnExportSSV.Location = New System.Drawing.Point(8, 66)
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
        Me.rbtnExportCSV.Location = New System.Drawing.Point(8, 49)
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
        Me.rbtnExportDefault.Location = New System.Drawing.Point(8, 32)
        Me.rbtnExportDefault.Name = "rbtnExportDefault"
        Me.rbtnExportDefault.Size = New System.Drawing.Size(59, 17)
        Me.rbtnExportDefault.TabIndex = 0
        Me.rbtnExportDefault.TabStop = True
        Me.rbtnExportDefault.Text = "Default"
        Me.rbtnExportDefault.UseVisualStyleBackColor = True
        '
        'txtAddlCosts
        '
        Me.txtAddlCosts.Location = New System.Drawing.Point(63, 75)
        Me.txtAddlCosts.MaxLength = 15
        Me.txtAddlCosts.Name = "txtAddlCosts"
        Me.txtAddlCosts.Size = New System.Drawing.Size(112, 20)
        Me.txtAddlCosts.TabIndex = 17
        Me.txtAddlCosts.Text = "0.00"
        Me.txtAddlCosts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkFees
        '
        Me.chkFees.AutoSize = True
        Me.chkFees.Checked = True
        Me.chkFees.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFees.Location = New System.Drawing.Point(6, 15)
        Me.chkFees.Name = "chkFees"
        Me.chkFees.Size = New System.Drawing.Size(52, 17)
        Me.chkFees.TabIndex = 11
        Me.chkFees.Text = "Fees:"
        Me.chkFees.ThreeState = True
        Me.chkFees.UseVisualStyleBackColor = True
        '
        'lblAddlCosts
        '
        Me.lblAddlCosts.AutoSize = True
        Me.lblAddlCosts.Location = New System.Drawing.Point(4, 79)
        Me.lblAddlCosts.Name = "lblAddlCosts"
        Me.lblAddlCosts.Size = New System.Drawing.Size(62, 13)
        Me.lblAddlCosts.TabIndex = 16
        Me.lblAddlCosts.Text = "Add'l Costs:"
        '
        'chkRebuildItemsfromList
        '
        Me.chkRebuildItemsfromList.AutoSize = True
        Me.chkRebuildItemsfromList.Location = New System.Drawing.Point(546, 641)
        Me.chkRebuildItemsfromList.Name = "chkRebuildItemsfromList"
        Me.chkRebuildItemsfromList.Size = New System.Drawing.Size(179, 17)
        Me.chkRebuildItemsfromList.TabIndex = 79
        Me.chkRebuildItemsfromList.Text = "Rebuild Items when Loading List"
        Me.chkRebuildItemsfromList.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveSettings.Location = New System.Drawing.Point(107, 611)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(94, 32)
        Me.btnSaveSettings.TabIndex = 5
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'lblItemstoBuy
        '
        Me.lblItemstoBuy.Location = New System.Drawing.Point(7, 4)
        Me.lblItemstoBuy.Name = "lblItemstoBuy"
        Me.lblItemstoBuy.Size = New System.Drawing.Size(1081, 13)
        Me.lblItemstoBuy.TabIndex = 71
        Me.lblItemstoBuy.Text = "Items to Buy"
        Me.lblItemstoBuy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblItemstoBuild
        '
        Me.lblItemstoBuild.Location = New System.Drawing.Point(6, 322)
        Me.lblItemstoBuild.Name = "lblItemstoBuild"
        Me.lblItemstoBuild.Size = New System.Drawing.Size(708, 14)
        Me.lblItemstoBuild.TabIndex = 72
        Me.lblItemstoBuild.Text = "Items to Build"
        Me.lblItemstoBuild.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblComponentstoBuild
        '
        Me.lblComponentstoBuild.Location = New System.Drawing.Point(720, 323)
        Me.lblComponentstoBuild.Name = "lblComponentstoBuild"
        Me.lblComponentstoBuild.Size = New System.Drawing.Size(368, 14)
        Me.lblComponentstoBuild.TabIndex = 73
        Me.lblComponentstoBuild.Text = "Components to Build"
        Me.lblComponentstoBuild.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCopyPasteAssets
        '
        Me.btnCopyPasteAssets.Location = New System.Drawing.Point(8, 65)
        Me.btnCopyPasteAssets.Name = "btnCopyPasteAssets"
        Me.btnCopyPasteAssets.Size = New System.Drawing.Size(168, 28)
        Me.btnCopyPasteAssets.TabIndex = 21
        Me.btnCopyPasteAssets.Text = "Copy and Paste Assets"
        Me.btnCopyPasteAssets.UseVisualStyleBackColor = True
        '
        'btnShowAssets
        '
        Me.btnShowAssets.Image = CType(resources.GetObject("btnShowAssets.Image"), System.Drawing.Image)
        Me.btnShowAssets.Location = New System.Drawing.Point(128, 15)
        Me.btnShowAssets.Name = "btnShowAssets"
        Me.btnShowAssets.Size = New System.Drawing.Size(48, 48)
        Me.btnShowAssets.TabIndex = 20
        Me.btnShowAssets.UseVisualStyleBackColor = True
        '
        'gbUpdateList
        '
        Me.gbUpdateList.Controls.Add(Me.btnCopyPasteAssets)
        Me.gbUpdateList.Controls.Add(Me.btnUpdateListwithAssets)
        Me.gbUpdateList.Controls.Add(Me.btnShowAssets)
        Me.gbUpdateList.Location = New System.Drawing.Point(541, 538)
        Me.gbUpdateList.Name = "gbUpdateList"
        Me.gbUpdateList.Size = New System.Drawing.Size(184, 99)
        Me.gbUpdateList.TabIndex = 18
        Me.gbUpdateList.TabStop = False
        Me.gbUpdateList.Text = "Update List Options:"
        '
        'lblTotalInventionCost
        '
        Me.lblTotalInventionCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalInventionCost.Location = New System.Drawing.Point(925, 566)
        Me.lblTotalInventionCost.Name = "lblTotalInventionCost"
        Me.lblTotalInventionCost.Size = New System.Drawing.Size(163, 16)
        Me.lblTotalInventionCost.TabIndex = 25
        Me.lblTotalInventionCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalCopyCost
        '
        Me.lblTotalCopyCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalCopyCost.Location = New System.Drawing.Point(925, 585)
        Me.lblTotalCopyCost.Name = "lblTotalCopyCost"
        Me.lblTotalCopyCost.Size = New System.Drawing.Size(163, 16)
        Me.lblTotalCopyCost.TabIndex = 27
        Me.lblTotalCopyCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTIC
        '
        Me.lblTIC.AutoSize = True
        Me.lblTIC.Location = New System.Drawing.Point(819, 568)
        Me.lblTIC.Name = "lblTIC"
        Me.lblTIC.Size = New System.Drawing.Size(105, 13)
        Me.lblTIC.TabIndex = 75
        Me.lblTIC.Text = "Total Invention Cost:"
        Me.lblTIC.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTCC
        '
        Me.lblTCC.AutoSize = True
        Me.lblTCC.Location = New System.Drawing.Point(839, 587)
        Me.lblTCC.Name = "lblTCC"
        Me.lblTCC.Size = New System.Drawing.Size(85, 13)
        Me.lblTCC.TabIndex = 76
        Me.lblTCC.Text = "Total Copy Cost:"
        Me.lblTCC.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalItemsInList
        '
        Me.lblTotalItemsInList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalItemsInList.Location = New System.Drawing.Point(107, 543)
        Me.lblTotalItemsInList.Name = "lblTotalItemsInList"
        Me.lblTotalItemsInList.Size = New System.Drawing.Size(94, 32)
        Me.lblTotalItemsInList.TabIndex = 77
        Me.lblTotalItemsInList.Text = "9,999 Items to Build"
        Me.lblTotalItemsInList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstBuy
        '
        Me.lstBuy.ContextMenuStrip = Me.DeleteMaterialStrip
        Me.lstBuy.FullRowSelect = True
        Me.lstBuy.GridLines = True
        Me.lstBuy.HideSelection = False
        Me.lstBuy.Location = New System.Drawing.Point(7, 20)
        Me.lstBuy.Name = "lstBuy"
        Me.lstBuy.Size = New System.Drawing.Size(1081, 300)
        Me.lstBuy.TabIndex = 37
        Me.lstBuy.TabStop = False
        Me.lstBuy.UseCompatibleStateImageBehavior = False
        Me.lstBuy.View = System.Windows.Forms.View.Details
        '
        'lstItems
        '
        Me.lstItems.ContextMenuStrip = Me.DeleteItemStrip
        Me.lstItems.FullRowSelect = True
        Me.lstItems.HideSelection = False
        Me.lstItems.Location = New System.Drawing.Point(6, 339)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(711, 198)
        Me.lstItems.TabIndex = 38
        Me.lstItems.TabStop = False
        Me.lstItems.UseCompatibleStateImageBehavior = False
        Me.lstItems.View = System.Windows.Forms.View.Details
        '
        'lstBuild
        '
        Me.lstBuild.ContextMenuStrip = Me.DeleteBuildStrip
        Me.lstBuild.FullRowSelect = True
        Me.lstBuild.HideSelection = False
        Me.lstBuild.Location = New System.Drawing.Point(720, 339)
        Me.lstBuild.Name = "lstBuild"
        Me.lstBuild.Size = New System.Drawing.Size(368, 198)
        Me.lstBuild.TabIndex = 39
        Me.lstBuild.TabStop = False
        Me.lstBuild.Tag = "20"
        Me.lstBuild.UseCompatibleStateImageBehavior = False
        Me.lstBuild.View = System.Windows.Forms.View.Details
        '
        'frmShoppingList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1094, 682)
        Me.Controls.Add(Me.chkRebuildItemsfromList)
        Me.Controls.Add(Me.lblTotalItemsInList)
        Me.Controls.Add(Me.lblTCC)
        Me.Controls.Add(Me.lblTIC)
        Me.Controls.Add(Me.lblTotalCopyCost)
        Me.Controls.Add(Me.lblTotalInventionCost)
        Me.Controls.Add(Me.gbUpdateList)
        Me.Controls.Add(Me.lblItemstoBuild)
        Me.Controls.Add(Me.lblItemstoBuy)
        Me.Controls.Add(Me.lstBuy)
        Me.Controls.Add(Me.lblTV)
        Me.Controls.Add(Me.lblTotalCost)
        Me.Controls.Add(Me.gbOptions)
        Me.Controls.Add(Me.lblTotalVolume)
        Me.Controls.Add(Me.lblTotalBuiltVolume)
        Me.Controls.Add(Me.lblComponentstoBuild)
        Me.Controls.Add(Me.lblTotalBuiltVolume1)
        Me.Controls.Add(Me.lblAvgIPH)
        Me.Controls.Add(Me.lstItems)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblTC)
        Me.Controls.Add(Me.lblTotalProfit)
        Me.Controls.Add(Me.lblTotalProfit1)
        Me.Controls.Add(Me.lblAvgIPH1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnLoadListFromFile)
        Me.Controls.Add(Me.btnSaveListToFile)
        Me.Controls.Add(Me.lstBuild)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmShoppingList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Shopping List"
        Me.DeleteBuildStrip.ResumeLayout(False)
        Me.DeleteItemStrip.ResumeLayout(False)
        Me.DeleteMaterialStrip.ResumeLayout(False)
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.gbExportOptions.ResumeLayout(False)
        Me.gbExportOptions.PerformLayout()
        Me.gbUpdateList.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents lblTotalCost As System.Windows.Forms.Label
    Friend WithEvents lblTC As System.Windows.Forms.Label
    Friend WithEvents lblTV As System.Windows.Forms.Label
    Friend WithEvents lblTotalVolume As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents lblTotalProfit1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalProfit As System.Windows.Forms.Label
    Friend WithEvents lblAvgIPH As System.Windows.Forms.Label
    Friend WithEvents lblAvgIPH1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalBuiltVolume As System.Windows.Forms.Label
    Friend WithEvents lblTotalBuiltVolume1 As System.Windows.Forms.Label
    Friend WithEvents btnShowAssets As System.Windows.Forms.Button
    Friend WithEvents ttMain As System.Windows.Forms.ToolTip
    Friend WithEvents btnUpdateListwithAssets As System.Windows.Forms.Button
    Friend WithEvents chkUpdateAssetsWhenUsed As System.Windows.Forms.CheckBox
    Friend WithEvents DeleteItemStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteMaterialStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteMaterial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSaveListToFile As System.Windows.Forms.Button
    Friend WithEvents btnLoadListFromFile As System.Windows.Forms.Button
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkFees As System.Windows.Forms.CheckBox
    Friend WithEvents lblFees As System.Windows.Forms.Label
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents DeleteBuildStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteBuildItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtAddlCosts As System.Windows.Forms.TextBox
    Friend WithEvents lstBuy As EVE_Isk_per_Hour.MyListView
    Friend WithEvents lstItems As EVE_Isk_per_Hour.MyListView
    Friend WithEvents lstBuild As EVE_Isk_per_Hour.MyListView
    Friend WithEvents lblItemstoBuy As System.Windows.Forms.Label
    Friend WithEvents lblItemstoBuild As System.Windows.Forms.Label
    Friend WithEvents lblComponentstoBuild As System.Windows.Forms.Label
    Friend WithEvents lblAddlCosts As System.Windows.Forms.Label
    Friend WithEvents lblUsage As System.Windows.Forms.Label
    Friend WithEvents chkUsage As System.Windows.Forms.CheckBox
    Friend WithEvents chkAlwaysOnTop As System.Windows.Forms.CheckBox
    Friend WithEvents btnCopyPasteAssets As System.Windows.Forms.Button
    Friend WithEvents chkBuyorBuyOrder As System.Windows.Forms.CheckBox
    Friend WithEvents gbUpdateList As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalInventionCost As System.Windows.Forms.Label
    Friend WithEvents lblTotalCopyCost As System.Windows.Forms.Label
    Friend WithEvents gbExportOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnExportSSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportCSV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnExportDefault As System.Windows.Forms.RadioButton
    Friend WithEvents lblTIC As System.Windows.Forms.Label
    Friend WithEvents lblTCC As System.Windows.Forms.Label
    Friend WithEvents lblTotalItemsInList As System.Windows.Forms.Label
    Friend WithEvents chkRebuildItemsfromList As CheckBox
    Friend WithEvents rbtnExportMulitBuy As RadioButton
    Friend WithEvents txtBrokerFeeRate As TextBox
    Friend WithEvents lblFeeRate As Label
End Class
