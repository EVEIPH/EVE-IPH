<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBlueprintManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBlueprintManagement))
        Me.gbBPFilter = New System.Windows.Forms.GroupBox()
        Me.gbBlueprintTech = New System.Windows.Forms.GroupBox()
        Me.chkBPPirateFaction = New System.Windows.Forms.CheckBox()
        Me.chkBPStoryline = New System.Windows.Forms.CheckBox()
        Me.chkBPNavyFaction = New System.Windows.Forms.CheckBox()
        Me.chkBPT3 = New System.Windows.Forms.CheckBox()
        Me.chkBPT2 = New System.Windows.Forms.CheckBox()
        Me.chkBPT1 = New System.Windows.Forms.CheckBox()
        Me.gbBackup = New System.Windows.Forms.GroupBox()
        Me.btnLoadBPs = New System.Windows.Forms.Button()
        Me.btnBackupBPs = New System.Windows.Forms.Button()
        Me.gbRace = New System.Windows.Forms.GroupBox()
        Me.chkRacePirate = New System.Windows.Forms.CheckBox()
        Me.chkRaceOther = New System.Windows.Forms.CheckBox()
        Me.chkRaceGallente = New System.Windows.Forms.CheckBox()
        Me.chkRaceCaldari = New System.Windows.Forms.CheckBox()
        Me.chkRaceAmarr = New System.Windows.Forms.CheckBox()
        Me.chkRaceMinmatar = New System.Windows.Forms.CheckBox()
        Me.gbSize = New System.Windows.Forms.GroupBox()
        Me.chkBPXL = New System.Windows.Forms.CheckBox()
        Me.chkBPLarge = New System.Windows.Forms.CheckBox()
        Me.chkBPMedium = New System.Windows.Forms.CheckBox()
        Me.chkBPSmall = New System.Windows.Forms.CheckBox()
        Me.gbBPCopyOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnOnlyInventedBPCs = New System.Windows.Forms.RadioButton()
        Me.rbtnOnlyBPO = New System.Windows.Forms.RadioButton()
        Me.rbtnOnlyCopies = New System.Windows.Forms.RadioButton()
        Me.rbtnShowAllBPtypes = New System.Windows.Forms.RadioButton()
        Me.gbBPTextSearch = New System.Windows.Forms.GroupBox()
        Me.btnResetSearch = New System.Windows.Forms.Button()
        Me.txtBPSearch = New System.Windows.Forms.TextBox()
        Me.btnBPSearch = New System.Windows.Forms.Button()
        Me.grpScanAssets = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnScanCorpBPs = New System.Windows.Forms.Button()
        Me.btnResetAll = New System.Windows.Forms.Button()
        Me.btnScanPersonalBPs = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.gbItemTypeFilter = New System.Windows.Forms.GroupBox()
        Me.cmbBPTypeFilter = New System.Windows.Forms.ComboBox()
        Me.lblBPCombo = New System.Windows.Forms.Label()
        Me.gbBlueprintType = New System.Windows.Forms.GroupBox()
        Me.rbtnReactionBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnStructureBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnCelestialsBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnStructureModulesBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnAmmoChargeBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnDeployableBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnAllBPTypes = New System.Windows.Forms.RadioButton()
        Me.rbtnRigBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnBoosterBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnSubsystemBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnModuleBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnMiscBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnDroneBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnComponentBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnShipBlueprints = New System.Windows.Forms.RadioButton()
        Me.rbtnStructureRigsBlueprints = New System.Windows.Forms.RadioButton()
        Me.gbBPSelect = New System.Windows.Forms.GroupBox()
        Me.chkNotIgnored = New System.Windows.Forms.CheckBox()
        Me.chkNotOwned = New System.Windows.Forms.CheckBox()
        Me.lblScanCorpColor = New System.Windows.Forms.Label()
        Me.rbtnIgnored = New System.Windows.Forms.RadioButton()
        Me.lblScanPersonalColor = New System.Windows.Forms.Label()
        Me.rbtnScannedCorpBPs = New System.Windows.Forms.RadioButton()
        Me.rbtnScannedPersonalBPs = New System.Windows.Forms.RadioButton()
        Me.rbtnFavorites = New System.Windows.Forms.RadioButton()
        Me.rbtnAllBPs = New System.Windows.Forms.RadioButton()
        Me.rbtnOwned = New System.Windows.Forms.RadioButton()
        Me.gbUpdateOptions = New System.Windows.Forms.GroupBox()
        Me.rbtnRemoveAllSettings = New System.Windows.Forms.RadioButton()
        Me.chkMarkasIgnored = New System.Windows.Forms.CheckBox()
        Me.chkMarkasFavorite = New System.Windows.Forms.CheckBox()
        Me.chkEnableMETE = New System.Windows.Forms.CheckBox()
        Me.rbtnMarkasUnowned = New System.Windows.Forms.RadioButton()
        Me.btnUpdateSelected = New System.Windows.Forms.Button()
        Me.txtBPTE = New System.Windows.Forms.TextBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.rbtnMarkasOwned = New System.Windows.Forms.RadioButton()
        Me.lblBPTE = New System.Windows.Forms.Label()
        Me.txtBPME = New System.Windows.Forms.TextBox()
        Me.lblBPME = New System.Windows.Forms.Label()
        Me.txtBPEdit = New System.Windows.Forms.TextBox()
        Me.ttBPManage = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.cmbEdit = New System.Windows.Forms.ComboBox()
        Me.lstBPs = New EVE_Isk_per_Hour.MyListView()
        Me.gbBPFilter.SuspendLayout()
        Me.gbBlueprintTech.SuspendLayout()
        Me.gbBackup.SuspendLayout()
        Me.gbRace.SuspendLayout()
        Me.gbSize.SuspendLayout()
        Me.gbBPCopyOptions.SuspendLayout()
        Me.gbBPTextSearch.SuspendLayout()
        Me.grpScanAssets.SuspendLayout()
        Me.gbItemTypeFilter.SuspendLayout()
        Me.gbBlueprintType.SuspendLayout()
        Me.gbBPSelect.SuspendLayout()
        Me.gbUpdateOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbBPFilter
        '
        Me.gbBPFilter.Controls.Add(Me.gbBlueprintTech)
        Me.gbBPFilter.Controls.Add(Me.gbBackup)
        Me.gbBPFilter.Controls.Add(Me.gbRace)
        Me.gbBPFilter.Controls.Add(Me.gbSize)
        Me.gbBPFilter.Controls.Add(Me.gbBPCopyOptions)
        Me.gbBPFilter.Controls.Add(Me.gbBPTextSearch)
        Me.gbBPFilter.Controls.Add(Me.grpScanAssets)
        Me.gbBPFilter.Controls.Add(Me.gbItemTypeFilter)
        Me.gbBPFilter.Controls.Add(Me.gbBlueprintType)
        Me.gbBPFilter.Controls.Add(Me.gbBPSelect)
        Me.gbBPFilter.Controls.Add(Me.gbUpdateOptions)
        Me.gbBPFilter.Location = New System.Drawing.Point(6, 370)
        Me.gbBPFilter.Name = "gbBPFilter"
        Me.gbBPFilter.Size = New System.Drawing.Size(1105, 240)
        Me.gbBPFilter.TabIndex = 0
        Me.gbBPFilter.TabStop = False
        Me.gbBPFilter.Text = "Blueprint Filters"
        '
        'gbBlueprintTech
        '
        Me.gbBlueprintTech.Controls.Add(Me.chkBPPirateFaction)
        Me.gbBlueprintTech.Controls.Add(Me.chkBPStoryline)
        Me.gbBlueprintTech.Controls.Add(Me.chkBPNavyFaction)
        Me.gbBlueprintTech.Controls.Add(Me.chkBPT3)
        Me.gbBlueprintTech.Controls.Add(Me.chkBPT2)
        Me.gbBlueprintTech.Controls.Add(Me.chkBPT1)
        Me.gbBlueprintTech.Location = New System.Drawing.Point(333, 13)
        Me.gbBlueprintTech.Name = "gbBlueprintTech"
        Me.gbBlueprintTech.Size = New System.Drawing.Size(101, 122)
        Me.gbBlueprintTech.TabIndex = 2
        Me.gbBlueprintTech.TabStop = False
        Me.gbBlueprintTech.Text = "Tech"
        '
        'chkBPPirateFaction
        '
        Me.chkBPPirateFaction.AutoSize = True
        Me.chkBPPirateFaction.Location = New System.Drawing.Point(9, 100)
        Me.chkBPPirateFaction.Name = "chkBPPirateFaction"
        Me.chkBPPirateFaction.Size = New System.Drawing.Size(91, 17)
        Me.chkBPPirateFaction.TabIndex = 22
        Me.chkBPPirateFaction.Text = "Pirate Faction"
        Me.chkBPPirateFaction.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBPPirateFaction.UseVisualStyleBackColor = True
        '
        'chkBPStoryline
        '
        Me.chkBPStoryline.AutoSize = True
        Me.chkBPStoryline.Location = New System.Drawing.Point(9, 66)
        Me.chkBPStoryline.Name = "chkBPStoryline"
        Me.chkBPStoryline.Size = New System.Drawing.Size(66, 17)
        Me.chkBPStoryline.TabIndex = 20
        Me.chkBPStoryline.Text = "Storyline"
        Me.chkBPStoryline.UseVisualStyleBackColor = True
        '
        'chkBPNavyFaction
        '
        Me.chkBPNavyFaction.AutoSize = True
        Me.chkBPNavyFaction.Location = New System.Drawing.Point(9, 83)
        Me.chkBPNavyFaction.Name = "chkBPNavyFaction"
        Me.chkBPNavyFaction.Size = New System.Drawing.Size(89, 17)
        Me.chkBPNavyFaction.TabIndex = 21
        Me.chkBPNavyFaction.Text = "Navy Faction"
        Me.chkBPNavyFaction.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBPNavyFaction.UseVisualStyleBackColor = True
        '
        'chkBPT3
        '
        Me.chkBPT3.AutoSize = True
        Me.chkBPT3.Location = New System.Drawing.Point(9, 49)
        Me.chkBPT3.Name = "chkBPT3"
        Me.chkBPT3.Size = New System.Drawing.Size(60, 17)
        Me.chkBPT3.TabIndex = 19
        Me.chkBPT3.Text = "Tech 3"
        Me.chkBPT3.UseVisualStyleBackColor = True
        '
        'chkBPT2
        '
        Me.chkBPT2.AutoSize = True
        Me.chkBPT2.Location = New System.Drawing.Point(9, 32)
        Me.chkBPT2.Name = "chkBPT2"
        Me.chkBPT2.Size = New System.Drawing.Size(60, 17)
        Me.chkBPT2.TabIndex = 18
        Me.chkBPT2.Text = "Tech 2"
        Me.chkBPT2.UseVisualStyleBackColor = True
        '
        'chkBPT1
        '
        Me.chkBPT1.AutoSize = True
        Me.chkBPT1.Location = New System.Drawing.Point(9, 15)
        Me.chkBPT1.Name = "chkBPT1"
        Me.chkBPT1.Size = New System.Drawing.Size(60, 17)
        Me.chkBPT1.TabIndex = 17
        Me.chkBPT1.Text = "Tech 1"
        Me.chkBPT1.UseVisualStyleBackColor = True
        '
        'gbBackup
        '
        Me.gbBackup.Controls.Add(Me.btnLoadBPs)
        Me.gbBackup.Controls.Add(Me.btnBackupBPs)
        Me.gbBackup.Location = New System.Drawing.Point(804, 10)
        Me.gbBackup.Name = "gbBackup"
        Me.gbBackup.Size = New System.Drawing.Size(132, 75)
        Me.gbBackup.TabIndex = 44
        Me.gbBackup.TabStop = False
        '
        'btnLoadBPs
        '
        Me.btnLoadBPs.Location = New System.Drawing.Point(6, 41)
        Me.btnLoadBPs.Name = "btnLoadBPs"
        Me.btnLoadBPs.Size = New System.Drawing.Size(120, 29)
        Me.btnLoadBPs.TabIndex = 42
        Me.btnLoadBPs.Text = "Load BPs from File"
        Me.btnLoadBPs.UseVisualStyleBackColor = True
        '
        'btnBackupBPs
        '
        Me.btnBackupBPs.Location = New System.Drawing.Point(6, 11)
        Me.btnBackupBPs.Name = "btnBackupBPs"
        Me.btnBackupBPs.Size = New System.Drawing.Size(120, 29)
        Me.btnBackupBPs.TabIndex = 41
        Me.btnBackupBPs.Text = "Backup BPs to File"
        Me.btnBackupBPs.UseVisualStyleBackColor = True
        '
        'gbRace
        '
        Me.gbRace.Controls.Add(Me.chkRacePirate)
        Me.gbRace.Controls.Add(Me.chkRaceOther)
        Me.gbRace.Controls.Add(Me.chkRaceGallente)
        Me.gbRace.Controls.Add(Me.chkRaceCaldari)
        Me.gbRace.Controls.Add(Me.chkRaceAmarr)
        Me.gbRace.Controls.Add(Me.chkRaceMinmatar)
        Me.gbRace.Location = New System.Drawing.Point(439, 13)
        Me.gbRace.Name = "gbRace"
        Me.gbRace.Size = New System.Drawing.Size(76, 122)
        Me.gbRace.TabIndex = 3
        Me.gbRace.TabStop = False
        Me.gbRace.Text = "BP Race"
        '
        'chkRacePirate
        '
        Me.chkRacePirate.AutoSize = True
        Me.chkRacePirate.Location = New System.Drawing.Point(6, 83)
        Me.chkRacePirate.Name = "chkRacePirate"
        Me.chkRacePirate.Size = New System.Drawing.Size(53, 17)
        Me.chkRacePirate.TabIndex = 27
        Me.chkRacePirate.Text = "Pirate"
        Me.chkRacePirate.UseVisualStyleBackColor = True
        '
        'chkRaceOther
        '
        Me.chkRaceOther.AutoSize = True
        Me.chkRaceOther.Location = New System.Drawing.Point(6, 100)
        Me.chkRaceOther.Name = "chkRaceOther"
        Me.chkRaceOther.Size = New System.Drawing.Size(52, 17)
        Me.chkRaceOther.TabIndex = 28
        Me.chkRaceOther.Text = "Other"
        Me.chkRaceOther.UseVisualStyleBackColor = True
        '
        'chkRaceGallente
        '
        Me.chkRaceGallente.AutoSize = True
        Me.chkRaceGallente.Location = New System.Drawing.Point(6, 49)
        Me.chkRaceGallente.Name = "chkRaceGallente"
        Me.chkRaceGallente.Size = New System.Drawing.Size(65, 17)
        Me.chkRaceGallente.TabIndex = 25
        Me.chkRaceGallente.Text = "Gallente"
        Me.chkRaceGallente.UseVisualStyleBackColor = True
        '
        'chkRaceCaldari
        '
        Me.chkRaceCaldari.AutoSize = True
        Me.chkRaceCaldari.Location = New System.Drawing.Point(6, 32)
        Me.chkRaceCaldari.Name = "chkRaceCaldari"
        Me.chkRaceCaldari.Size = New System.Drawing.Size(58, 17)
        Me.chkRaceCaldari.TabIndex = 24
        Me.chkRaceCaldari.Text = "Caldari"
        Me.chkRaceCaldari.UseVisualStyleBackColor = True
        '
        'chkRaceAmarr
        '
        Me.chkRaceAmarr.AutoSize = True
        Me.chkRaceAmarr.Location = New System.Drawing.Point(6, 15)
        Me.chkRaceAmarr.Name = "chkRaceAmarr"
        Me.chkRaceAmarr.Size = New System.Drawing.Size(53, 17)
        Me.chkRaceAmarr.TabIndex = 23
        Me.chkRaceAmarr.Text = "Amarr"
        Me.chkRaceAmarr.UseVisualStyleBackColor = True
        '
        'chkRaceMinmatar
        '
        Me.chkRaceMinmatar.AutoSize = True
        Me.chkRaceMinmatar.Location = New System.Drawing.Point(6, 66)
        Me.chkRaceMinmatar.Name = "chkRaceMinmatar"
        Me.chkRaceMinmatar.Size = New System.Drawing.Size(69, 17)
        Me.chkRaceMinmatar.TabIndex = 26
        Me.chkRaceMinmatar.Text = "Minmatar"
        Me.chkRaceMinmatar.UseVisualStyleBackColor = True
        '
        'gbSize
        '
        Me.gbSize.Controls.Add(Me.chkBPXL)
        Me.gbSize.Controls.Add(Me.chkBPLarge)
        Me.gbSize.Controls.Add(Me.chkBPMedium)
        Me.gbSize.Controls.Add(Me.chkBPSmall)
        Me.gbSize.Location = New System.Drawing.Point(521, 89)
        Me.gbSize.Name = "gbSize"
        Me.gbSize.Size = New System.Drawing.Size(159, 45)
        Me.gbSize.TabIndex = 43
        Me.gbSize.TabStop = False
        Me.gbSize.Text = "Size"
        '
        'chkBPXL
        '
        Me.chkBPXL.AutoSize = True
        Me.chkBPXL.Location = New System.Drawing.Point(119, 17)
        Me.chkBPXL.Name = "chkBPXL"
        Me.chkBPXL.Size = New System.Drawing.Size(39, 17)
        Me.chkBPXL.TabIndex = 4
        Me.chkBPXL.Text = "XL"
        Me.chkBPXL.UseVisualStyleBackColor = True
        '
        'chkBPLarge
        '
        Me.chkBPLarge.AutoSize = True
        Me.chkBPLarge.Location = New System.Drawing.Point(83, 17)
        Me.chkBPLarge.Name = "chkBPLarge"
        Me.chkBPLarge.Size = New System.Drawing.Size(32, 17)
        Me.chkBPLarge.TabIndex = 3
        Me.chkBPLarge.Text = "L"
        Me.chkBPLarge.UseVisualStyleBackColor = True
        '
        'chkBPMedium
        '
        Me.chkBPMedium.AutoSize = True
        Me.chkBPMedium.Location = New System.Drawing.Point(44, 17)
        Me.chkBPMedium.Name = "chkBPMedium"
        Me.chkBPMedium.Size = New System.Drawing.Size(35, 17)
        Me.chkBPMedium.TabIndex = 2
        Me.chkBPMedium.Text = "M"
        Me.chkBPMedium.UseVisualStyleBackColor = True
        '
        'chkBPSmall
        '
        Me.chkBPSmall.AutoSize = True
        Me.chkBPSmall.Location = New System.Drawing.Point(7, 17)
        Me.chkBPSmall.Name = "chkBPSmall"
        Me.chkBPSmall.Size = New System.Drawing.Size(33, 17)
        Me.chkBPSmall.TabIndex = 1
        Me.chkBPSmall.Text = "S"
        Me.chkBPSmall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkBPSmall.UseVisualStyleBackColor = True
        '
        'gbBPCopyOptions
        '
        Me.gbBPCopyOptions.Controls.Add(Me.rbtnOnlyInventedBPCs)
        Me.gbBPCopyOptions.Controls.Add(Me.rbtnOnlyBPO)
        Me.gbBPCopyOptions.Controls.Add(Me.rbtnOnlyCopies)
        Me.gbBPCopyOptions.Controls.Add(Me.rbtnShowAllBPtypes)
        Me.gbBPCopyOptions.Location = New System.Drawing.Point(333, 136)
        Me.gbBPCopyOptions.Name = "gbBPCopyOptions"
        Me.gbBPCopyOptions.Size = New System.Drawing.Size(347, 42)
        Me.gbBPCopyOptions.TabIndex = 6
        Me.gbBPCopyOptions.TabStop = False
        Me.gbBPCopyOptions.Text = "Blueprint Type:"
        '
        'rbtnOnlyInventedBPCs
        '
        Me.rbtnOnlyInventedBPCs.AutoSize = True
        Me.rbtnOnlyInventedBPCs.Location = New System.Drawing.Point(229, 17)
        Me.rbtnOnlyInventedBPCs.Name = "rbtnOnlyInventedBPCs"
        Me.rbtnOnlyInventedBPCs.Size = New System.Drawing.Size(96, 17)
        Me.rbtnOnlyInventedBPCs.TabIndex = 37
        Me.rbtnOnlyInventedBPCs.TabStop = True
        Me.rbtnOnlyInventedBPCs.Text = "Invented BPCs"
        Me.rbtnOnlyInventedBPCs.UseVisualStyleBackColor = True
        '
        'rbtnOnlyBPO
        '
        Me.rbtnOnlyBPO.AutoSize = True
        Me.rbtnOnlyBPO.Location = New System.Drawing.Point(70, 17)
        Me.rbtnOnlyBPO.Name = "rbtnOnlyBPO"
        Me.rbtnOnlyBPO.Size = New System.Drawing.Size(52, 17)
        Me.rbtnOnlyBPO.TabIndex = 34
        Me.rbtnOnlyBPO.TabStop = True
        Me.rbtnOnlyBPO.Text = "BPOs"
        Me.rbtnOnlyBPO.UseVisualStyleBackColor = True
        '
        'rbtnOnlyCopies
        '
        Me.rbtnOnlyCopies.AutoSize = True
        Me.rbtnOnlyCopies.Location = New System.Drawing.Point(147, 17)
        Me.rbtnOnlyCopies.Name = "rbtnOnlyCopies"
        Me.rbtnOnlyCopies.Size = New System.Drawing.Size(57, 17)
        Me.rbtnOnlyCopies.TabIndex = 35
        Me.rbtnOnlyCopies.TabStop = True
        Me.rbtnOnlyCopies.Text = "Copies"
        Me.rbtnOnlyCopies.UseVisualStyleBackColor = True
        '
        'rbtnShowAllBPtypes
        '
        Me.rbtnShowAllBPtypes.AutoSize = True
        Me.rbtnShowAllBPtypes.Location = New System.Drawing.Point(9, 17)
        Me.rbtnShowAllBPtypes.Name = "rbtnShowAllBPtypes"
        Me.rbtnShowAllBPtypes.Size = New System.Drawing.Size(36, 17)
        Me.rbtnShowAllBPtypes.TabIndex = 36
        Me.rbtnShowAllBPtypes.TabStop = True
        Me.rbtnShowAllBPtypes.Text = "All"
        Me.rbtnShowAllBPtypes.UseVisualStyleBackColor = True
        '
        'gbBPTextSearch
        '
        Me.gbBPTextSearch.Controls.Add(Me.btnResetSearch)
        Me.gbBPTextSearch.Controls.Add(Me.txtBPSearch)
        Me.gbBPTextSearch.Controls.Add(Me.btnBPSearch)
        Me.gbBPTextSearch.Location = New System.Drawing.Point(521, 13)
        Me.gbBPTextSearch.Name = "gbBPTextSearch"
        Me.gbBPTextSearch.Size = New System.Drawing.Size(277, 75)
        Me.gbBPTextSearch.TabIndex = 5
        Me.gbBPTextSearch.TabStop = False
        Me.gbBPTextSearch.Text = "BP Search:"
        '
        'btnResetSearch
        '
        Me.btnResetSearch.Location = New System.Drawing.Point(138, 40)
        Me.btnResetSearch.Name = "btnResetSearch"
        Me.btnResetSearch.Size = New System.Drawing.Size(106, 29)
        Me.btnResetSearch.TabIndex = 33
        Me.btnResetSearch.Text = "Reset Search"
        Me.btnResetSearch.UseVisualStyleBackColor = True
        '
        'txtBPSearch
        '
        Me.txtBPSearch.Location = New System.Drawing.Point(7, 17)
        Me.txtBPSearch.Name = "txtBPSearch"
        Me.txtBPSearch.Size = New System.Drawing.Size(263, 20)
        Me.txtBPSearch.TabIndex = 30
        '
        'btnBPSearch
        '
        Me.btnBPSearch.Location = New System.Drawing.Point(32, 41)
        Me.btnBPSearch.Name = "btnBPSearch"
        Me.btnBPSearch.Size = New System.Drawing.Size(100, 28)
        Me.btnBPSearch.TabIndex = 32
        Me.btnBPSearch.Text = "Search"
        Me.btnBPSearch.UseVisualStyleBackColor = True
        '
        'grpScanAssets
        '
        Me.grpScanAssets.Controls.Add(Me.btnClose)
        Me.grpScanAssets.Controls.Add(Me.btnReset)
        Me.grpScanAssets.Controls.Add(Me.btnScanCorpBPs)
        Me.grpScanAssets.Controls.Add(Me.btnResetAll)
        Me.grpScanAssets.Controls.Add(Me.btnScanPersonalBPs)
        Me.grpScanAssets.Controls.Add(Me.btnRefresh)
        Me.grpScanAssets.Location = New System.Drawing.Point(942, 10)
        Me.grpScanAssets.Name = "grpScanAssets"
        Me.grpScanAssets.Size = New System.Drawing.Size(155, 220)
        Me.grpScanAssets.TabIndex = 8
        Me.grpScanAssets.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(6, 46)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(143, 34)
        Me.btnClose.TabIndex = 51
        Me.btnClose.Text = "Close Form"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(6, 80)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(143, 34)
        Me.btnReset.TabIndex = 50
        Me.btnReset.Text = "Reset Form"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnScanCorpBPs
        '
        Me.btnScanCorpBPs.Location = New System.Drawing.Point(6, 148)
        Me.btnScanCorpBPs.Name = "btnScanCorpBPs"
        Me.btnScanCorpBPs.Size = New System.Drawing.Size(143, 34)
        Me.btnScanCorpBPs.TabIndex = 38
        Me.btnScanCorpBPs.Text = "Scan Corp BPs"
        Me.btnScanCorpBPs.UseVisualStyleBackColor = True
        '
        'btnResetAll
        '
        Me.btnResetAll.Location = New System.Drawing.Point(6, 182)
        Me.btnResetAll.Name = "btnResetAll"
        Me.btnResetAll.Size = New System.Drawing.Size(143, 34)
        Me.btnResetAll.TabIndex = 40
        Me.btnResetAll.Text = "Reset All Stored BP Data"
        Me.btnResetAll.UseVisualStyleBackColor = True
        '
        'btnScanPersonalBPs
        '
        Me.btnScanPersonalBPs.Location = New System.Drawing.Point(6, 114)
        Me.btnScanPersonalBPs.Name = "btnScanPersonalBPs"
        Me.btnScanPersonalBPs.Size = New System.Drawing.Size(143, 34)
        Me.btnScanPersonalBPs.TabIndex = 37
        Me.btnScanPersonalBPs.Text = "Scan Personal BPs"
        Me.btnScanPersonalBPs.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(6, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(143, 34)
        Me.btnRefresh.TabIndex = 39
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'gbItemTypeFilter
        '
        Me.gbItemTypeFilter.Controls.Add(Me.cmbBPTypeFilter)
        Me.gbItemTypeFilter.Controls.Add(Me.lblBPCombo)
        Me.gbItemTypeFilter.Location = New System.Drawing.Point(333, 175)
        Me.gbItemTypeFilter.Name = "gbItemTypeFilter"
        Me.gbItemTypeFilter.Size = New System.Drawing.Size(347, 58)
        Me.gbItemTypeFilter.TabIndex = 4
        Me.gbItemTypeFilter.TabStop = False
        '
        'cmbBPTypeFilter
        '
        Me.cmbBPTypeFilter.FormattingEnabled = True
        Me.cmbBPTypeFilter.Location = New System.Drawing.Point(7, 29)
        Me.cmbBPTypeFilter.Name = "cmbBPTypeFilter"
        Me.cmbBPTypeFilter.Size = New System.Drawing.Size(332, 21)
        Me.cmbBPTypeFilter.TabIndex = 29
        Me.cmbBPTypeFilter.Text = "Select Type"
        '
        'lblBPCombo
        '
        Me.lblBPCombo.AutoSize = True
        Me.lblBPCombo.Location = New System.Drawing.Point(6, 13)
        Me.lblBPCombo.Name = "lblBPCombo"
        Me.lblBPCombo.Size = New System.Drawing.Size(59, 13)
        Me.lblBPCombo.TabIndex = 3
        Me.lblBPCombo.Text = "Type Filter:"
        '
        'gbBlueprintType
        '
        Me.gbBlueprintType.Controls.Add(Me.rbtnReactionBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnStructureBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnCelestialsBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnStructureModulesBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnAmmoChargeBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnDeployableBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnAllBPTypes)
        Me.gbBlueprintType.Controls.Add(Me.rbtnRigBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnBoosterBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnSubsystemBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnModuleBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnMiscBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnDroneBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnComponentBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnShipBlueprints)
        Me.gbBlueprintType.Controls.Add(Me.rbtnStructureRigsBlueprints)
        Me.gbBlueprintType.Location = New System.Drawing.Point(6, 107)
        Me.gbBlueprintType.Name = "gbBlueprintType"
        Me.gbBlueprintType.Size = New System.Drawing.Size(321, 126)
        Me.gbBlueprintType.TabIndex = 1
        Me.gbBlueprintType.TabStop = False
        Me.gbBlueprintType.Text = "Item Type:"
        '
        'rbtnReactionBlueprints
        '
        Me.rbtnReactionBlueprints.AutoSize = True
        Me.rbtnReactionBlueprints.Location = New System.Drawing.Point(116, 70)
        Me.rbtnReactionBlueprints.Name = "rbtnReactionBlueprints"
        Me.rbtnReactionBlueprints.Size = New System.Drawing.Size(73, 17)
        Me.rbtnReactionBlueprints.TabIndex = 67
        Me.rbtnReactionBlueprints.TabStop = True
        Me.rbtnReactionBlueprints.Text = "Reactions"
        Me.rbtnReactionBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnStructureBlueprints
        '
        Me.rbtnStructureBlueprints.AutoSize = True
        Me.rbtnStructureBlueprints.Location = New System.Drawing.Point(6, 87)
        Me.rbtnStructureBlueprints.Name = "rbtnStructureBlueprints"
        Me.rbtnStructureBlueprints.Size = New System.Drawing.Size(73, 17)
        Me.rbtnStructureBlueprints.TabIndex = 14
        Me.rbtnStructureBlueprints.TabStop = True
        Me.rbtnStructureBlueprints.Text = "Structures"
        Me.rbtnStructureBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnCelestialsBlueprints
        '
        Me.rbtnCelestialsBlueprints.AutoSize = True
        Me.rbtnCelestialsBlueprints.Location = New System.Drawing.Point(227, 70)
        Me.rbtnCelestialsBlueprints.Name = "rbtnCelestialsBlueprints"
        Me.rbtnCelestialsBlueprints.Size = New System.Drawing.Size(64, 17)
        Me.rbtnCelestialsBlueprints.TabIndex = 19
        Me.rbtnCelestialsBlueprints.TabStop = True
        Me.rbtnCelestialsBlueprints.Text = "Celestial"
        Me.rbtnCelestialsBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnStructureModulesBlueprints
        '
        Me.rbtnStructureModulesBlueprints.AutoSize = True
        Me.rbtnStructureModulesBlueprints.Location = New System.Drawing.Point(116, 87)
        Me.rbtnStructureModulesBlueprints.Name = "rbtnStructureModulesBlueprints"
        Me.rbtnStructureModulesBlueprints.Size = New System.Drawing.Size(111, 17)
        Me.rbtnStructureModulesBlueprints.TabIndex = 66
        Me.rbtnStructureModulesBlueprints.TabStop = True
        Me.rbtnStructureModulesBlueprints.Text = "Structure Modules"
        Me.rbtnStructureModulesBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnAmmoChargeBlueprints
        '
        Me.rbtnAmmoChargeBlueprints.AutoSize = True
        Me.rbtnAmmoChargeBlueprints.Location = New System.Drawing.Point(6, 36)
        Me.rbtnAmmoChargeBlueprints.Name = "rbtnAmmoChargeBlueprints"
        Me.rbtnAmmoChargeBlueprints.Size = New System.Drawing.Size(98, 17)
        Me.rbtnAmmoChargeBlueprints.TabIndex = 11
        Me.rbtnAmmoChargeBlueprints.TabStop = True
        Me.rbtnAmmoChargeBlueprints.Text = "Ammo/Charges"
        Me.rbtnAmmoChargeBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnDeployableBlueprints
        '
        Me.rbtnDeployableBlueprints.AutoSize = True
        Me.rbtnDeployableBlueprints.Location = New System.Drawing.Point(116, 53)
        Me.rbtnDeployableBlueprints.Name = "rbtnDeployableBlueprints"
        Me.rbtnDeployableBlueprints.Size = New System.Drawing.Size(78, 17)
        Me.rbtnDeployableBlueprints.TabIndex = 17
        Me.rbtnDeployableBlueprints.TabStop = True
        Me.rbtnDeployableBlueprints.Text = "Deployable"
        Me.rbtnDeployableBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnAllBPTypes
        '
        Me.rbtnAllBPTypes.AutoSize = True
        Me.rbtnAllBPTypes.Location = New System.Drawing.Point(6, 19)
        Me.rbtnAllBPTypes.Name = "rbtnAllBPTypes"
        Me.rbtnAllBPTypes.Size = New System.Drawing.Size(36, 17)
        Me.rbtnAllBPTypes.TabIndex = 6
        Me.rbtnAllBPTypes.TabStop = True
        Me.rbtnAllBPTypes.Text = "All"
        Me.rbtnAllBPTypes.UseVisualStyleBackColor = True
        '
        'rbtnRigBlueprints
        '
        Me.rbtnRigBlueprints.AutoSize = True
        Me.rbtnRigBlueprints.Location = New System.Drawing.Point(227, 36)
        Me.rbtnRigBlueprints.Name = "rbtnRigBlueprints"
        Me.rbtnRigBlueprints.Size = New System.Drawing.Size(46, 17)
        Me.rbtnRigBlueprints.TabIndex = 10
        Me.rbtnRigBlueprints.TabStop = True
        Me.rbtnRigBlueprints.Text = "Rigs"
        Me.rbtnRigBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnBoosterBlueprints
        '
        Me.rbtnBoosterBlueprints.AutoSize = True
        Me.rbtnBoosterBlueprints.Location = New System.Drawing.Point(6, 70)
        Me.rbtnBoosterBlueprints.Name = "rbtnBoosterBlueprints"
        Me.rbtnBoosterBlueprints.Size = New System.Drawing.Size(66, 17)
        Me.rbtnBoosterBlueprints.TabIndex = 15
        Me.rbtnBoosterBlueprints.TabStop = True
        Me.rbtnBoosterBlueprints.Text = "Boosters"
        Me.rbtnBoosterBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnSubsystemBlueprints
        '
        Me.rbtnSubsystemBlueprints.AutoSize = True
        Me.rbtnSubsystemBlueprints.Location = New System.Drawing.Point(6, 53)
        Me.rbtnSubsystemBlueprints.Name = "rbtnSubsystemBlueprints"
        Me.rbtnSubsystemBlueprints.Size = New System.Drawing.Size(81, 17)
        Me.rbtnSubsystemBlueprints.TabIndex = 13
        Me.rbtnSubsystemBlueprints.TabStop = True
        Me.rbtnSubsystemBlueprints.Text = "Subsystems"
        Me.rbtnSubsystemBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnModuleBlueprints
        '
        Me.rbtnModuleBlueprints.AutoSize = True
        Me.rbtnModuleBlueprints.Location = New System.Drawing.Point(227, 19)
        Me.rbtnModuleBlueprints.Name = "rbtnModuleBlueprints"
        Me.rbtnModuleBlueprints.Size = New System.Drawing.Size(65, 17)
        Me.rbtnModuleBlueprints.TabIndex = 8
        Me.rbtnModuleBlueprints.TabStop = True
        Me.rbtnModuleBlueprints.Text = "Modules"
        Me.rbtnModuleBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnMiscBlueprints
        '
        Me.rbtnMiscBlueprints.AutoSize = True
        Me.rbtnMiscBlueprints.Location = New System.Drawing.Point(6, 104)
        Me.rbtnMiscBlueprints.Name = "rbtnMiscBlueprints"
        Me.rbtnMiscBlueprints.Size = New System.Drawing.Size(50, 17)
        Me.rbtnMiscBlueprints.TabIndex = 16
        Me.rbtnMiscBlueprints.TabStop = True
        Me.rbtnMiscBlueprints.Text = "Misc."
        Me.rbtnMiscBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnDroneBlueprints
        '
        Me.rbtnDroneBlueprints.AutoSize = True
        Me.rbtnDroneBlueprints.Location = New System.Drawing.Point(116, 36)
        Me.rbtnDroneBlueprints.Name = "rbtnDroneBlueprints"
        Me.rbtnDroneBlueprints.Size = New System.Drawing.Size(59, 17)
        Me.rbtnDroneBlueprints.TabIndex = 9
        Me.rbtnDroneBlueprints.TabStop = True
        Me.rbtnDroneBlueprints.Text = "Drones"
        Me.rbtnDroneBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnComponentBlueprints
        '
        Me.rbtnComponentBlueprints.AutoSize = True
        Me.rbtnComponentBlueprints.Location = New System.Drawing.Point(227, 53)
        Me.rbtnComponentBlueprints.Name = "rbtnComponentBlueprints"
        Me.rbtnComponentBlueprints.Size = New System.Drawing.Size(84, 17)
        Me.rbtnComponentBlueprints.TabIndex = 12
        Me.rbtnComponentBlueprints.TabStop = True
        Me.rbtnComponentBlueprints.Text = "Components"
        Me.rbtnComponentBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnShipBlueprints
        '
        Me.rbtnShipBlueprints.AutoSize = True
        Me.rbtnShipBlueprints.Location = New System.Drawing.Point(116, 19)
        Me.rbtnShipBlueprints.Name = "rbtnShipBlueprints"
        Me.rbtnShipBlueprints.Size = New System.Drawing.Size(51, 17)
        Me.rbtnShipBlueprints.TabIndex = 7
        Me.rbtnShipBlueprints.TabStop = True
        Me.rbtnShipBlueprints.Text = "Ships"
        Me.rbtnShipBlueprints.UseVisualStyleBackColor = True
        '
        'rbtnStructureRigsBlueprints
        '
        Me.rbtnStructureRigsBlueprints.AutoSize = True
        Me.rbtnStructureRigsBlueprints.Location = New System.Drawing.Point(227, 87)
        Me.rbtnStructureRigsBlueprints.Name = "rbtnStructureRigsBlueprints"
        Me.rbtnStructureRigsBlueprints.Size = New System.Drawing.Size(92, 17)
        Me.rbtnStructureRigsBlueprints.TabIndex = 18
        Me.rbtnStructureRigsBlueprints.TabStop = True
        Me.rbtnStructureRigsBlueprints.Text = "Structure Rigs"
        Me.rbtnStructureRigsBlueprints.UseVisualStyleBackColor = True
        '
        'gbBPSelect
        '
        Me.gbBPSelect.Controls.Add(Me.chkNotIgnored)
        Me.gbBPSelect.Controls.Add(Me.chkNotOwned)
        Me.gbBPSelect.Controls.Add(Me.lblScanCorpColor)
        Me.gbBPSelect.Controls.Add(Me.rbtnIgnored)
        Me.gbBPSelect.Controls.Add(Me.lblScanPersonalColor)
        Me.gbBPSelect.Controls.Add(Me.rbtnScannedCorpBPs)
        Me.gbBPSelect.Controls.Add(Me.rbtnScannedPersonalBPs)
        Me.gbBPSelect.Controls.Add(Me.rbtnFavorites)
        Me.gbBPSelect.Controls.Add(Me.rbtnAllBPs)
        Me.gbBPSelect.Controls.Add(Me.rbtnOwned)
        Me.gbBPSelect.Location = New System.Drawing.Point(6, 13)
        Me.gbBPSelect.Name = "gbBPSelect"
        Me.gbBPSelect.Size = New System.Drawing.Size(321, 93)
        Me.gbBPSelect.TabIndex = 0
        Me.gbBPSelect.TabStop = False
        '
        'chkNotIgnored
        '
        Me.chkNotIgnored.AutoSize = True
        Me.chkNotIgnored.Location = New System.Drawing.Point(230, 30)
        Me.chkNotIgnored.Name = "chkNotIgnored"
        Me.chkNotIgnored.Size = New System.Drawing.Size(82, 17)
        Me.chkNotIgnored.TabIndex = 63
        Me.chkNotIgnored.Text = "Not Ignored"
        Me.chkNotIgnored.UseVisualStyleBackColor = True
        '
        'chkNotOwned
        '
        Me.chkNotOwned.AutoSize = True
        Me.chkNotOwned.Location = New System.Drawing.Point(230, 13)
        Me.chkNotOwned.Name = "chkNotOwned"
        Me.chkNotOwned.Size = New System.Drawing.Size(80, 17)
        Me.chkNotOwned.TabIndex = 18
        Me.chkNotOwned.Text = "Not Owned"
        Me.chkNotOwned.UseVisualStyleBackColor = True
        '
        'lblScanCorpColor
        '
        Me.lblScanCorpColor.BackColor = System.Drawing.Color.LightGreen
        Me.lblScanCorpColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblScanCorpColor.Location = New System.Drawing.Point(143, 65)
        Me.lblScanCorpColor.Name = "lblScanCorpColor"
        Me.lblScanCorpColor.Size = New System.Drawing.Size(46, 12)
        Me.lblScanCorpColor.TabIndex = 6
        Me.lblScanCorpColor.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'rbtnIgnored
        '
        Me.rbtnIgnored.AutoSize = True
        Me.rbtnIgnored.Location = New System.Drawing.Point(143, 28)
        Me.rbtnIgnored.Name = "rbtnIgnored"
        Me.rbtnIgnored.Size = New System.Drawing.Size(61, 17)
        Me.rbtnIgnored.TabIndex = 62
        Me.rbtnIgnored.TabStop = True
        Me.rbtnIgnored.Text = "Ignored"
        Me.rbtnIgnored.UseVisualStyleBackColor = True
        '
        'lblScanPersonalColor
        '
        Me.lblScanPersonalColor.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.lblScanPersonalColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblScanPersonalColor.Location = New System.Drawing.Point(143, 48)
        Me.lblScanPersonalColor.Name = "lblScanPersonalColor"
        Me.lblScanPersonalColor.Size = New System.Drawing.Size(46, 12)
        Me.lblScanPersonalColor.TabIndex = 5
        '
        'rbtnScannedCorpBPs
        '
        Me.rbtnScannedCorpBPs.AutoSize = True
        Me.rbtnScannedCorpBPs.Location = New System.Drawing.Point(6, 63)
        Me.rbtnScannedCorpBPs.Name = "rbtnScannedCorpBPs"
        Me.rbtnScannedCorpBPs.Size = New System.Drawing.Size(115, 17)
        Me.rbtnScannedCorpBPs.TabIndex = 4
        Me.rbtnScannedCorpBPs.TabStop = True
        Me.rbtnScannedCorpBPs.Text = "Scanned Corp BPs"
        Me.rbtnScannedCorpBPs.UseVisualStyleBackColor = True
        '
        'rbtnScannedPersonalBPs
        '
        Me.rbtnScannedPersonalBPs.AutoSize = True
        Me.rbtnScannedPersonalBPs.Location = New System.Drawing.Point(6, 46)
        Me.rbtnScannedPersonalBPs.Name = "rbtnScannedPersonalBPs"
        Me.rbtnScannedPersonalBPs.Size = New System.Drawing.Size(134, 17)
        Me.rbtnScannedPersonalBPs.TabIndex = 3
        Me.rbtnScannedPersonalBPs.TabStop = True
        Me.rbtnScannedPersonalBPs.Text = "Scanned Personal BPs"
        Me.rbtnScannedPersonalBPs.UseVisualStyleBackColor = True
        '
        'rbtnFavorites
        '
        Me.rbtnFavorites.AutoSize = True
        Me.rbtnFavorites.Location = New System.Drawing.Point(143, 11)
        Me.rbtnFavorites.Name = "rbtnFavorites"
        Me.rbtnFavorites.Size = New System.Drawing.Size(68, 17)
        Me.rbtnFavorites.TabIndex = 2
        Me.rbtnFavorites.TabStop = True
        Me.rbtnFavorites.Text = "Favorites"
        Me.rbtnFavorites.UseVisualStyleBackColor = True
        '
        'rbtnAllBPs
        '
        Me.rbtnAllBPs.AutoSize = True
        Me.rbtnAllBPs.Location = New System.Drawing.Point(6, 12)
        Me.rbtnAllBPs.Name = "rbtnAllBPs"
        Me.rbtnAllBPs.Size = New System.Drawing.Size(85, 17)
        Me.rbtnAllBPs.TabIndex = 1
        Me.rbtnAllBPs.TabStop = True
        Me.rbtnAllBPs.Text = "All Blueprints"
        Me.rbtnAllBPs.UseVisualStyleBackColor = True
        '
        'rbtnOwned
        '
        Me.rbtnOwned.AutoSize = True
        Me.rbtnOwned.ForeColor = System.Drawing.Color.Blue
        Me.rbtnOwned.Location = New System.Drawing.Point(6, 29)
        Me.rbtnOwned.Name = "rbtnOwned"
        Me.rbtnOwned.Size = New System.Drawing.Size(81, 17)
        Me.rbtnOwned.TabIndex = 5
        Me.rbtnOwned.TabStop = True
        Me.rbtnOwned.Text = "Owned BPs"
        Me.rbtnOwned.UseVisualStyleBackColor = True
        '
        'gbUpdateOptions
        '
        Me.gbUpdateOptions.Controls.Add(Me.rbtnRemoveAllSettings)
        Me.gbUpdateOptions.Controls.Add(Me.chkMarkasIgnored)
        Me.gbUpdateOptions.Controls.Add(Me.chkMarkasFavorite)
        Me.gbUpdateOptions.Controls.Add(Me.chkEnableMETE)
        Me.gbUpdateOptions.Controls.Add(Me.rbtnMarkasUnowned)
        Me.gbUpdateOptions.Controls.Add(Me.btnUpdateSelected)
        Me.gbUpdateOptions.Controls.Add(Me.txtBPTE)
        Me.gbUpdateOptions.Controls.Add(Me.btnSelectAll)
        Me.gbUpdateOptions.Controls.Add(Me.rbtnMarkasOwned)
        Me.gbUpdateOptions.Controls.Add(Me.lblBPTE)
        Me.gbUpdateOptions.Controls.Add(Me.txtBPME)
        Me.gbUpdateOptions.Controls.Add(Me.lblBPME)
        Me.gbUpdateOptions.Location = New System.Drawing.Point(686, 89)
        Me.gbUpdateOptions.Name = "gbUpdateOptions"
        Me.gbUpdateOptions.Size = New System.Drawing.Size(250, 141)
        Me.gbUpdateOptions.TabIndex = 7
        Me.gbUpdateOptions.TabStop = False
        Me.gbUpdateOptions.Text = "Update Options"
        '
        'rbtnRemoveAllSettings
        '
        Me.rbtnRemoveAllSettings.AutoSize = True
        Me.rbtnRemoveAllSettings.Location = New System.Drawing.Point(10, 55)
        Me.rbtnRemoveAllSettings.Name = "rbtnRemoveAllSettings"
        Me.rbtnRemoveAllSettings.Size = New System.Drawing.Size(120, 17)
        Me.rbtnRemoveAllSettings.TabIndex = 54
        Me.rbtnRemoveAllSettings.TabStop = True
        Me.rbtnRemoveAllSettings.Text = "Remove All Settings"
        Me.rbtnRemoveAllSettings.UseVisualStyleBackColor = True
        '
        'chkMarkasIgnored
        '
        Me.chkMarkasIgnored.AutoSize = True
        Me.chkMarkasIgnored.Location = New System.Drawing.Point(135, 38)
        Me.chkMarkasIgnored.Name = "chkMarkasIgnored"
        Me.chkMarkasIgnored.Size = New System.Drawing.Size(104, 17)
        Me.chkMarkasIgnored.TabIndex = 53
        Me.chkMarkasIgnored.Text = "Mark As Ignored"
        Me.chkMarkasIgnored.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMarkasIgnored.UseVisualStyleBackColor = True
        '
        'chkMarkasFavorite
        '
        Me.chkMarkasFavorite.AutoSize = True
        Me.chkMarkasFavorite.Location = New System.Drawing.Point(135, 21)
        Me.chkMarkasFavorite.Name = "chkMarkasFavorite"
        Me.chkMarkasFavorite.Size = New System.Drawing.Size(106, 17)
        Me.chkMarkasFavorite.TabIndex = 51
        Me.chkMarkasFavorite.Text = "Mark As Favorite"
        Me.chkMarkasFavorite.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMarkasFavorite.UseVisualStyleBackColor = True
        '
        'chkEnableMETE
        '
        Me.chkEnableMETE.AutoSize = True
        Me.chkEnableMETE.Location = New System.Drawing.Point(135, 79)
        Me.chkEnableMETE.Name = "chkEnableMETE"
        Me.chkEnableMETE.Size = New System.Drawing.Size(103, 17)
        Me.chkEnableMETE.TabIndex = 52
        Me.chkEnableMETE.Text = "Enable ME / TE"
        Me.chkEnableMETE.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkEnableMETE.UseVisualStyleBackColor = True
        '
        'rbtnMarkasUnowned
        '
        Me.rbtnMarkasUnowned.AutoSize = True
        Me.rbtnMarkasUnowned.Location = New System.Drawing.Point(10, 37)
        Me.rbtnMarkasUnowned.Name = "rbtnMarkasUnowned"
        Me.rbtnMarkasUnowned.Size = New System.Drawing.Size(112, 17)
        Me.rbtnMarkasUnowned.TabIndex = 46
        Me.rbtnMarkasUnowned.TabStop = True
        Me.rbtnMarkasUnowned.Text = "Mark as Unowned"
        Me.rbtnMarkasUnowned.UseVisualStyleBackColor = True
        '
        'btnUpdateSelected
        '
        Me.btnUpdateSelected.Location = New System.Drawing.Point(129, 103)
        Me.btnUpdateSelected.Name = "btnUpdateSelected"
        Me.btnUpdateSelected.Size = New System.Drawing.Size(112, 33)
        Me.btnUpdateSelected.TabIndex = 50
        Me.btnUpdateSelected.Text = "Update Selected"
        Me.btnUpdateSelected.UseVisualStyleBackColor = True
        '
        'txtBPTE
        '
        Me.txtBPTE.Location = New System.Drawing.Point(91, 77)
        Me.txtBPTE.MaxLength = 2
        Me.txtBPTE.Name = "txtBPTE"
        Me.txtBPTE.Size = New System.Drawing.Size(31, 20)
        Me.txtBPTE.TabIndex = 44
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(9, 103)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(113, 33)
        Me.btnSelectAll.TabIndex = 49
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'rbtnMarkasOwned
        '
        Me.rbtnMarkasOwned.AutoSize = True
        Me.rbtnMarkasOwned.Location = New System.Drawing.Point(10, 20)
        Me.rbtnMarkasOwned.Name = "rbtnMarkasOwned"
        Me.rbtnMarkasOwned.Size = New System.Drawing.Size(100, 17)
        Me.rbtnMarkasOwned.TabIndex = 45
        Me.rbtnMarkasOwned.TabStop = True
        Me.rbtnMarkasOwned.Text = "Mark as Owned"
        Me.rbtnMarkasOwned.UseVisualStyleBackColor = True
        '
        'lblBPTE
        '
        Me.lblBPTE.AutoSize = True
        Me.lblBPTE.Location = New System.Drawing.Point(67, 80)
        Me.lblBPTE.Name = "lblBPTE"
        Me.lblBPTE.Size = New System.Drawing.Size(24, 13)
        Me.lblBPTE.TabIndex = 15
        Me.lblBPTE.Text = "TE:"
        '
        'txtBPME
        '
        Me.txtBPME.Location = New System.Drawing.Point(32, 77)
        Me.txtBPME.MaxLength = 2
        Me.txtBPME.Name = "txtBPME"
        Me.txtBPME.Size = New System.Drawing.Size(31, 20)
        Me.txtBPME.TabIndex = 43
        '
        'lblBPME
        '
        Me.lblBPME.Location = New System.Drawing.Point(6, 80)
        Me.lblBPME.Name = "lblBPME"
        Me.lblBPME.Size = New System.Drawing.Size(26, 13)
        Me.lblBPME.TabIndex = 12
        Me.lblBPME.Text = "ME:"
        '
        'txtBPEdit
        '
        Me.txtBPEdit.Location = New System.Drawing.Point(802, 325)
        Me.txtBPEdit.Name = "txtBPEdit"
        Me.txtBPEdit.Size = New System.Drawing.Size(48, 20)
        Me.txtBPEdit.TabIndex = 58
        Me.txtBPEdit.TabStop = False
        Me.txtBPEdit.Visible = False
        '
        'ttBPManage
        '
        Me.ttBPManage.IsBalloon = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "Filename"
        '
        'cmbEdit
        '
        Me.cmbEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEdit.FormattingEnabled = True
        Me.cmbEdit.ItemHeight = 13
        Me.cmbEdit.Items.AddRange(New Object() {"Yes", "No"})
        Me.cmbEdit.Location = New System.Drawing.Point(802, 347)
        Me.cmbEdit.Name = "cmbEdit"
        Me.cmbEdit.Size = New System.Drawing.Size(48, 21)
        Me.cmbEdit.TabIndex = 61
        Me.cmbEdit.TabStop = False
        '
        'lstBPs
        '
        Me.lstBPs.CheckBoxes = True
        Me.lstBPs.FullRowSelect = True
        Me.lstBPs.GridLines = True
        Me.lstBPs.HideSelection = False
        Me.lstBPs.Location = New System.Drawing.Point(6, 12)
        Me.lstBPs.MultiSelect = False
        Me.lstBPs.Name = "lstBPs"
        Me.lstBPs.Size = New System.Drawing.Size(1105, 350)
        Me.lstBPs.TabIndex = 60
        Me.lstBPs.TabStop = False
        Me.lstBPs.UseCompatibleStateImageBehavior = False
        Me.lstBPs.View = System.Windows.Forms.View.Details
        '
        'frmBlueprintManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1115, 614)
        Me.Controls.Add(Me.gbBPFilter)
        Me.Controls.Add(Me.cmbEdit)
        Me.Controls.Add(Me.txtBPEdit)
        Me.Controls.Add(Me.lstBPs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmBlueprintManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Blueprint Management"
        Me.gbBPFilter.ResumeLayout(False)
        Me.gbBlueprintTech.ResumeLayout(False)
        Me.gbBlueprintTech.PerformLayout()
        Me.gbBackup.ResumeLayout(False)
        Me.gbRace.ResumeLayout(False)
        Me.gbRace.PerformLayout()
        Me.gbSize.ResumeLayout(False)
        Me.gbSize.PerformLayout()
        Me.gbBPCopyOptions.ResumeLayout(False)
        Me.gbBPCopyOptions.PerformLayout()
        Me.gbBPTextSearch.ResumeLayout(False)
        Me.gbBPTextSearch.PerformLayout()
        Me.grpScanAssets.ResumeLayout(False)
        Me.gbItemTypeFilter.ResumeLayout(False)
        Me.gbItemTypeFilter.PerformLayout()
        Me.gbBlueprintType.ResumeLayout(False)
        Me.gbBlueprintType.PerformLayout()
        Me.gbBPSelect.ResumeLayout(False)
        Me.gbBPSelect.PerformLayout()
        Me.gbUpdateOptions.ResumeLayout(False)
        Me.gbUpdateOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbBPFilter As System.Windows.Forms.GroupBox
    Friend WithEvents gbBPTextSearch As System.Windows.Forms.GroupBox
    Friend WithEvents txtBPSearch As System.Windows.Forms.TextBox
    Friend WithEvents gbBPSelect As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnAllBPs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnOwned As System.Windows.Forms.RadioButton
    Friend WithEvents gbBlueprintType As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnRigBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBoosterBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnStructureBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSubsystemBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnModuleBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnMiscBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAmmoChargeBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDroneBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnComponentBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnShipBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents gbUpdateOptions As System.Windows.Forms.GroupBox
    Friend WithEvents gbItemTypeFilter As System.Windows.Forms.GroupBox
    Friend WithEvents cmbBPTypeFilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblBPCombo As System.Windows.Forms.Label
    Friend WithEvents rbtnAllBPTypes As System.Windows.Forms.RadioButton
    Friend WithEvents btnBPSearch As System.Windows.Forms.Button
    Friend WithEvents gbBlueprintTech As System.Windows.Forms.GroupBox
    Friend WithEvents chkBPPirateFaction As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPStoryline As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPNavyFaction As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPT3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPT2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPT1 As System.Windows.Forms.CheckBox
    Friend WithEvents rbtnMarkasOwned As System.Windows.Forms.RadioButton
    Friend WithEvents txtBPTE As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdateSelected As System.Windows.Forms.Button
    Friend WithEvents lblBPTE As System.Windows.Forms.Label
    Friend WithEvents txtBPME As System.Windows.Forms.TextBox
    Friend WithEvents lblBPME As System.Windows.Forms.Label
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents rbtnMarkasUnowned As System.Windows.Forms.RadioButton
    Friend WithEvents grpScanAssets As System.Windows.Forms.GroupBox
    Friend WithEvents btnScanPersonalBPs As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents ttBPManage As System.Windows.Forms.ToolTip
    Friend WithEvents btnResetSearch As System.Windows.Forms.Button
    Friend WithEvents rbtnScannedPersonalBPs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnScannedCorpBPs As System.Windows.Forms.RadioButton
    Friend WithEvents btnScanCorpBPs As System.Windows.Forms.Button
    Friend WithEvents lblScanCorpColor As System.Windows.Forms.Label
    Friend WithEvents lblScanPersonalColor As System.Windows.Forms.Label
    Friend WithEvents btnResetAll As System.Windows.Forms.Button
    Friend WithEvents gbBPCopyOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnOnlyBPO As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnOnlyCopies As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnShowAllBPtypes As System.Windows.Forms.RadioButton
    Friend WithEvents gbRace As System.Windows.Forms.GroupBox
    Friend WithEvents chkRaceMinmatar As System.Windows.Forms.CheckBox
    Friend WithEvents chkRacePirate As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceOther As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceGallente As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceCaldari As System.Windows.Forms.CheckBox
    Friend WithEvents chkRaceAmarr As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoadBPs As System.Windows.Forms.Button
    Friend WithEvents btnBackupBPs As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtBPEdit As System.Windows.Forms.TextBox
    Friend WithEvents rbtnFavorites As System.Windows.Forms.RadioButton
    Friend WithEvents chkMarkasFavorite As System.Windows.Forms.CheckBox
    Friend WithEvents lstBPs As EVE_Isk_per_Hour.MyListView
    Friend WithEvents cmbEdit As System.Windows.Forms.ComboBox
    Friend WithEvents chkNotOwned As System.Windows.Forms.CheckBox
    Friend WithEvents gbSize As System.Windows.Forms.GroupBox
    Friend WithEvents chkBPXL As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPLarge As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPMedium As System.Windows.Forms.CheckBox
    Friend WithEvents chkBPSmall As System.Windows.Forms.CheckBox
    Friend WithEvents rbtnCelestialsBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnStructureRigsBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDeployableBlueprints As System.Windows.Forms.RadioButton
    Friend WithEvents chkEnableMETE As System.Windows.Forms.CheckBox
    Friend WithEvents gbBackup As System.Windows.Forms.GroupBox
    Friend WithEvents chkMarkasIgnored As System.Windows.Forms.CheckBox
    Friend WithEvents rbtnIgnored As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnOnlyInventedBPCs As System.Windows.Forms.RadioButton
    Friend WithEvents chkNotIgnored As System.Windows.Forms.CheckBox
    Friend WithEvents rbtnRemoveAllSettings As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnStructureModulesBlueprints As RadioButton
    Friend WithEvents rbtnReactionBlueprints As RadioButton
End Class
