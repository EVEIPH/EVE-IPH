<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddCharacter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddCharacter))
        Me.btnEVESSOLogin = New System.Windows.Forms.Button()
        Me.lblKeyType = New System.Windows.Forms.Label()
        Me.chkReadStructures = New System.Windows.Forms.CheckBox()
        Me.chkReadStructureMarkets = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterStandings = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterJobs = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterAgentsResearch = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterAssets = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterBlueprints = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterPlanetary = New System.Windows.Forms.CheckBox()
        Me.chkReadCorporationMembership = New System.Windows.Forms.CheckBox()
        Me.chkReadCorporationJobs = New System.Windows.Forms.CheckBox()
        Me.chkReadCorporationAssets = New System.Windows.Forms.CheckBox()
        Me.chkReadCorporationBlueprints = New System.Windows.Forms.CheckBox()
        Me.gbCorp = New System.Windows.Forms.GroupBox()
        Me.chkReadCorporationWallet = New System.Windows.Forms.CheckBox()
        Me.chkReadCorporationOrders = New System.Windows.Forms.CheckBox()
        Me.chkReadCorporationDivisions = New System.Windows.Forms.CheckBox()
        Me.gbCharacter = New System.Windows.Forms.GroupBox()
        Me.chkReadCharacterLoyalty = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterWallet = New System.Windows.Forms.CheckBox()
        Me.chkReadCharacterOrders = New System.Windows.Forms.CheckBox()
        Me.gbStructures = New System.Windows.Forms.GroupBox()
        Me.ttAPI = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.gbCorp.SuspendLayout()
        Me.gbCharacter.SuspendLayout()
        Me.gbStructures.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEVESSOLogin
        '
        Me.btnEVESSOLogin.BackgroundImage = CType(resources.GetObject("btnEVESSOLogin.BackgroundImage"), System.Drawing.Image)
        Me.btnEVESSOLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnEVESSOLogin.Location = New System.Drawing.Point(134, 241)
        Me.btnEVESSOLogin.Name = "btnEVESSOLogin"
        Me.btnEVESSOLogin.Size = New System.Drawing.Size(270, 46)
        Me.btnEVESSOLogin.TabIndex = 4
        Me.btnEVESSOLogin.UseVisualStyleBackColor = True
        '
        'lblKeyType
        '
        Me.lblKeyType.Location = New System.Drawing.Point(12, 9)
        Me.lblKeyType.Name = "lblKeyType"
        Me.lblKeyType.Size = New System.Drawing.Size(527, 32)
        Me.lblKeyType.TabIndex = 0
        Me.lblKeyType.Text = "Select the scopes you wish to authorize for this Character and log into EVE to au" &
    "thorize your character for IPH. (Note: Access to Characdter Skills is required)"
        '
        'chkReadStructures
        '
        Me.chkReadStructures.AutoSize = True
        Me.chkReadStructures.Location = New System.Drawing.Point(11, 19)
        Me.chkReadStructures.Name = "chkReadStructures"
        Me.chkReadStructures.Size = New System.Drawing.Size(124, 17)
        Me.chkReadStructures.TabIndex = 0
        Me.chkReadStructures.Tag = "esi-universe.read_structures.v1"
        Me.chkReadStructures.Text = "Structure Information"
        Me.chkReadStructures.UseVisualStyleBackColor = True
        '
        'chkReadStructureMarkets
        '
        Me.chkReadStructureMarkets.AutoSize = True
        Me.chkReadStructureMarkets.Location = New System.Drawing.Point(139, 19)
        Me.chkReadStructureMarkets.Name = "chkReadStructureMarkets"
        Me.chkReadStructureMarkets.Size = New System.Drawing.Size(110, 17)
        Me.chkReadStructureMarkets.TabIndex = 1
        Me.chkReadStructureMarkets.Tag = "esi-markets.structure_markets.v1"
        Me.chkReadStructureMarkets.Text = "Structure Markets"
        Me.chkReadStructureMarkets.UseVisualStyleBackColor = True
        '
        'chkReadCharacterStandings
        '
        Me.chkReadCharacterStandings.AutoSize = True
        Me.chkReadCharacterStandings.Location = New System.Drawing.Point(11, 20)
        Me.chkReadCharacterStandings.Name = "chkReadCharacterStandings"
        Me.chkReadCharacterStandings.Size = New System.Drawing.Size(73, 17)
        Me.chkReadCharacterStandings.TabIndex = 0
        Me.chkReadCharacterStandings.Tag = "esi-characters.read_standings.v1"
        Me.chkReadCharacterStandings.Text = "Standings"
        Me.chkReadCharacterStandings.UseVisualStyleBackColor = True
        '
        'chkReadCharacterJobs
        '
        Me.chkReadCharacterJobs.AutoSize = True
        Me.chkReadCharacterJobs.Location = New System.Drawing.Point(139, 43)
        Me.chkReadCharacterJobs.Name = "chkReadCharacterJobs"
        Me.chkReadCharacterJobs.Size = New System.Drawing.Size(88, 17)
        Me.chkReadCharacterJobs.TabIndex = 3
        Me.chkReadCharacterJobs.Tag = "esi-industry.read_character_jobs.v1"
        Me.chkReadCharacterJobs.Text = "Industry Jobs"
        Me.chkReadCharacterJobs.UseVisualStyleBackColor = True
        '
        'chkReadCharacterAgentsResearch
        '
        Me.chkReadCharacterAgentsResearch.AutoSize = True
        Me.chkReadCharacterAgentsResearch.Location = New System.Drawing.Point(11, 66)
        Me.chkReadCharacterAgentsResearch.Name = "chkReadCharacterAgentsResearch"
        Me.chkReadCharacterAgentsResearch.Size = New System.Drawing.Size(108, 17)
        Me.chkReadCharacterAgentsResearch.TabIndex = 4
        Me.chkReadCharacterAgentsResearch.Tag = "esi-characters.read_agents_research.v1"
        Me.chkReadCharacterAgentsResearch.Text = "Research Agents"
        Me.chkReadCharacterAgentsResearch.UseVisualStyleBackColor = True
        '
        'chkReadCharacterAssets
        '
        Me.chkReadCharacterAssets.AutoSize = True
        Me.chkReadCharacterAssets.Location = New System.Drawing.Point(11, 89)
        Me.chkReadCharacterAssets.Name = "chkReadCharacterAssets"
        Me.chkReadCharacterAssets.Size = New System.Drawing.Size(57, 17)
        Me.chkReadCharacterAssets.TabIndex = 6
        Me.chkReadCharacterAssets.Tag = "esi-assets.read_assets.v1"
        Me.chkReadCharacterAssets.Text = "Assets"
        Me.chkReadCharacterAssets.UseVisualStyleBackColor = True
        '
        'chkReadCharacterBlueprints
        '
        Me.chkReadCharacterBlueprints.AutoSize = True
        Me.chkReadCharacterBlueprints.Location = New System.Drawing.Point(139, 20)
        Me.chkReadCharacterBlueprints.Name = "chkReadCharacterBlueprints"
        Me.chkReadCharacterBlueprints.Size = New System.Drawing.Size(72, 17)
        Me.chkReadCharacterBlueprints.TabIndex = 1
        Me.chkReadCharacterBlueprints.Tag = "esi-characters.read_blueprints.v1"
        Me.chkReadCharacterBlueprints.Text = "Blueprints"
        Me.chkReadCharacterBlueprints.UseVisualStyleBackColor = True
        '
        'chkReadCharacterPlanetary
        '
        Me.chkReadCharacterPlanetary.AutoSize = True
        Me.chkReadCharacterPlanetary.Location = New System.Drawing.Point(11, 112)
        Me.chkReadCharacterPlanetary.Name = "chkReadCharacterPlanetary"
        Me.chkReadCharacterPlanetary.Size = New System.Drawing.Size(123, 17)
        Me.chkReadCharacterPlanetary.TabIndex = 8
        Me.chkReadCharacterPlanetary.Tag = "esi-planets.manage_planets.v1"
        Me.chkReadCharacterPlanetary.Text = "Planetary Interaction"
        Me.chkReadCharacterPlanetary.UseVisualStyleBackColor = True
        '
        'chkReadCorporationMembership
        '
        Me.chkReadCorporationMembership.AutoSize = True
        Me.chkReadCorporationMembership.Location = New System.Drawing.Point(11, 20)
        Me.chkReadCorporationMembership.Name = "chkReadCorporationMembership"
        Me.chkReadCorporationMembership.Size = New System.Drawing.Size(122, 17)
        Me.chkReadCorporationMembership.TabIndex = 0
        Me.chkReadCorporationMembership.Tag = "esi-corporations.read_corporation_membership.v1"
        Me.chkReadCorporationMembership.Text = "Membership && Roles"
        Me.chkReadCorporationMembership.UseVisualStyleBackColor = True
        '
        'chkReadCorporationJobs
        '
        Me.chkReadCorporationJobs.AutoSize = True
        Me.chkReadCorporationJobs.Location = New System.Drawing.Point(139, 43)
        Me.chkReadCorporationJobs.Name = "chkReadCorporationJobs"
        Me.chkReadCorporationJobs.Size = New System.Drawing.Size(88, 17)
        Me.chkReadCorporationJobs.TabIndex = 3
        Me.chkReadCorporationJobs.Tag = "esi-industry.read_corporation_jobs.v1"
        Me.chkReadCorporationJobs.Text = "Industry Jobs"
        Me.chkReadCorporationJobs.UseVisualStyleBackColor = True
        '
        'chkReadCorporationAssets
        '
        Me.chkReadCorporationAssets.AutoSize = True
        Me.chkReadCorporationAssets.Location = New System.Drawing.Point(11, 66)
        Me.chkReadCorporationAssets.Name = "chkReadCorporationAssets"
        Me.chkReadCorporationAssets.Size = New System.Drawing.Size(57, 17)
        Me.chkReadCorporationAssets.TabIndex = 4
        Me.chkReadCorporationAssets.Tag = "esi-assets.read_corporation_assets.v1"
        Me.chkReadCorporationAssets.Text = "Assets"
        Me.chkReadCorporationAssets.UseVisualStyleBackColor = True
        '
        'chkReadCorporationBlueprints
        '
        Me.chkReadCorporationBlueprints.AutoSize = True
        Me.chkReadCorporationBlueprints.Location = New System.Drawing.Point(139, 20)
        Me.chkReadCorporationBlueprints.Name = "chkReadCorporationBlueprints"
        Me.chkReadCorporationBlueprints.Size = New System.Drawing.Size(72, 17)
        Me.chkReadCorporationBlueprints.TabIndex = 1
        Me.chkReadCorporationBlueprints.Tag = "esi-corporations.read_blueprints.v1"
        Me.chkReadCorporationBlueprints.Text = "Blueprints"
        Me.chkReadCorporationBlueprints.UseVisualStyleBackColor = True
        '
        'gbCorp
        '
        Me.gbCorp.Controls.Add(Me.chkReadCorporationWallet)
        Me.gbCorp.Controls.Add(Me.chkReadCorporationOrders)
        Me.gbCorp.Controls.Add(Me.chkReadCorporationDivisions)
        Me.gbCorp.Controls.Add(Me.chkReadCorporationMembership)
        Me.gbCorp.Controls.Add(Me.chkReadCorporationJobs)
        Me.gbCorp.Controls.Add(Me.chkReadCorporationAssets)
        Me.gbCorp.Controls.Add(Me.chkReadCorporationBlueprints)
        Me.gbCorp.Location = New System.Drawing.Point(271, 44)
        Me.gbCorp.Name = "gbCorp"
        Me.gbCorp.Size = New System.Drawing.Size(250, 140)
        Me.gbCorp.TabIndex = 2
        Me.gbCorp.TabStop = False
        Me.gbCorp.Text = "Corporation"
        '
        'chkReadCorporationWallet
        '
        Me.chkReadCorporationWallet.AutoSize = True
        Me.chkReadCorporationWallet.Location = New System.Drawing.Point(139, 89)
        Me.chkReadCorporationWallet.Name = "chkReadCorporationWallet"
        Me.chkReadCorporationWallet.Size = New System.Drawing.Size(56, 17)
        Me.chkReadCorporationWallet.TabIndex = 6
        Me.chkReadCorporationWallet.Tag = "esi-wallet.read_corporation_wallets.v1"
        Me.chkReadCorporationWallet.Text = "Wallet"
        Me.chkReadCorporationWallet.UseVisualStyleBackColor = True
        '
        'chkReadCorporationOrders
        '
        Me.chkReadCorporationOrders.AutoSize = True
        Me.chkReadCorporationOrders.Location = New System.Drawing.Point(139, 66)
        Me.chkReadCorporationOrders.Name = "chkReadCorporationOrders"
        Me.chkReadCorporationOrders.Size = New System.Drawing.Size(93, 17)
        Me.chkReadCorporationOrders.TabIndex = 5
        Me.chkReadCorporationOrders.Tag = "esi-markets.read_corporation_orders.v1"
        Me.chkReadCorporationOrders.Text = "Market Orders"
        Me.chkReadCorporationOrders.UseVisualStyleBackColor = True
        '
        'chkReadCorporationDivisions
        '
        Me.chkReadCorporationDivisions.AutoSize = True
        Me.chkReadCorporationDivisions.Location = New System.Drawing.Point(11, 43)
        Me.chkReadCorporationDivisions.Name = "chkReadCorporationDivisions"
        Me.chkReadCorporationDivisions.Size = New System.Drawing.Size(68, 17)
        Me.chkReadCorporationDivisions.TabIndex = 2
        Me.chkReadCorporationDivisions.Tag = "esi-corporations.read_divisions.v1"
        Me.chkReadCorporationDivisions.Text = "Divisions"
        Me.chkReadCorporationDivisions.UseVisualStyleBackColor = True
        '
        'gbCharacter
        '
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterLoyalty)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterWallet)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterOrders)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterJobs)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterStandings)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterAgentsResearch)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterPlanetary)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterAssets)
        Me.gbCharacter.Controls.Add(Me.chkReadCharacterBlueprints)
        Me.gbCharacter.Location = New System.Drawing.Point(15, 44)
        Me.gbCharacter.Name = "gbCharacter"
        Me.gbCharacter.Size = New System.Drawing.Size(250, 140)
        Me.gbCharacter.TabIndex = 1
        Me.gbCharacter.TabStop = False
        Me.gbCharacter.Text = "Character"
        '
        'chkReadCharacterLoyalty
        '
        Me.chkReadCharacterLoyalty.AutoSize = True
        Me.chkReadCharacterLoyalty.Location = New System.Drawing.Point(11, 43)
        Me.chkReadCharacterLoyalty.Name = "chkReadCharacterLoyalty"
        Me.chkReadCharacterLoyalty.Size = New System.Drawing.Size(91, 17)
        Me.chkReadCharacterLoyalty.TabIndex = 2
        Me.chkReadCharacterLoyalty.Tag = "esi-characters.read_loyalty.v1"
        Me.chkReadCharacterLoyalty.Text = "Loyalty Points"
        Me.chkReadCharacterLoyalty.UseVisualStyleBackColor = True
        '
        'chkReadCharacterWallet
        '
        Me.chkReadCharacterWallet.AutoSize = True
        Me.chkReadCharacterWallet.Location = New System.Drawing.Point(139, 89)
        Me.chkReadCharacterWallet.Name = "chkReadCharacterWallet"
        Me.chkReadCharacterWallet.Size = New System.Drawing.Size(56, 17)
        Me.chkReadCharacterWallet.TabIndex = 7
        Me.chkReadCharacterWallet.Tag = "esi-wallet.read_character_wallet.v1"
        Me.chkReadCharacterWallet.Text = "Wallet"
        Me.chkReadCharacterWallet.UseVisualStyleBackColor = True
        '
        'chkReadCharacterOrders
        '
        Me.chkReadCharacterOrders.AutoSize = True
        Me.chkReadCharacterOrders.Location = New System.Drawing.Point(139, 66)
        Me.chkReadCharacterOrders.Name = "chkReadCharacterOrders"
        Me.chkReadCharacterOrders.Size = New System.Drawing.Size(93, 17)
        Me.chkReadCharacterOrders.TabIndex = 5
        Me.chkReadCharacterOrders.Tag = "esi-markets.read_character_orders.v1"
        Me.chkReadCharacterOrders.Text = "Market Orders"
        Me.chkReadCharacterOrders.UseVisualStyleBackColor = True
        '
        'gbStructures
        '
        Me.gbStructures.Controls.Add(Me.chkReadStructures)
        Me.gbStructures.Controls.Add(Me.chkReadStructureMarkets)
        Me.gbStructures.Location = New System.Drawing.Point(15, 190)
        Me.gbStructures.Name = "gbStructures"
        Me.gbStructures.Size = New System.Drawing.Size(250, 45)
        Me.gbStructures.TabIndex = 3
        Me.gbStructures.TabStop = False
        Me.gbStructures.Text = "Structures"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.AutoSize = True
        Me.btnSelectAll.Location = New System.Drawing.Point(286, 200)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(107, 28)
        Me.btnSelectAll.TabIndex = 24
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.AutoSize = True
        Me.btnDeselectAll.Location = New System.Drawing.Point(400, 200)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(107, 28)
        Me.btnDeselectAll.TabIndex = 25
        Me.btnDeselectAll.Text = "De-select All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'frmAddCharacter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(536, 296)
        Me.Controls.Add(Me.btnDeselectAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.gbStructures)
        Me.Controls.Add(Me.gbCharacter)
        Me.Controls.Add(Me.gbCorp)
        Me.Controls.Add(Me.lblKeyType)
        Me.Controls.Add(Me.btnEVESSOLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddCharacter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Character"
        Me.gbCorp.ResumeLayout(False)
        Me.gbCorp.PerformLayout()
        Me.gbCharacter.ResumeLayout(False)
        Me.gbCharacter.PerformLayout()
        Me.gbStructures.ResumeLayout(False)
        Me.gbStructures.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnEVESSOLogin As Button
    Friend WithEvents lblKeyType As Label
    Friend WithEvents chkReadStructures As CheckBox
    Friend WithEvents chkReadStructureMarkets As CheckBox
    Friend WithEvents chkReadCharacterStandings As CheckBox
    Friend WithEvents chkReadCharacterJobs As CheckBox
    Friend WithEvents chkReadCharacterAgentsResearch As CheckBox
    Friend WithEvents chkReadCharacterAssets As CheckBox
    Friend WithEvents chkReadCharacterBlueprints As CheckBox
    Friend WithEvents chkReadCharacterPlanetary As CheckBox
    Friend WithEvents chkReadCorporationMembership As CheckBox
    Friend WithEvents chkReadCorporationJobs As CheckBox
    Friend WithEvents chkReadCorporationAssets As CheckBox
    Friend WithEvents chkReadCorporationBlueprints As CheckBox
    Friend WithEvents gbCorp As GroupBox
    Friend WithEvents gbCharacter As GroupBox
    Friend WithEvents gbStructures As GroupBox
    Friend WithEvents ttAPI As ToolTip
    Friend WithEvents chkReadCorporationDivisions As CheckBox
    Friend WithEvents chkReadCorporationWallet As CheckBox
    Friend WithEvents chkReadCorporationOrders As CheckBox
    Friend WithEvents chkReadCharacterLoyalty As CheckBox
    Friend WithEvents chkReadCharacterWallet As CheckBox
    Friend WithEvents chkReadCharacterOrders As CheckBox
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents btnDeselectAll As Button
End Class
