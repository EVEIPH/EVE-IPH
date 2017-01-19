Imports System.Data.SQLite

Public Class frmCitadelFitting

    Declare Function SendMessage Lib "User32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Const WM_NCLBUTTONDOWN As Integer = &HA1
    Const HTCAPTION As Integer = 2

    Private SlotPictureBoxList As New List(Of PictureBox)

    Public MainRenderImage As CitadelImageRender

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Put all the slot images into an array
        With SlotPictureBoxList
            .Add(HighSlot1)
            .Add(HighSlot2)
            .Add(HighSlot3)
            .Add(HighSlot4)
            .Add(HighSlot5)
            .Add(HighSlot6)
            .Add(HighSlot7)
            .Add(HighSlot8)

            .Add(MidSlot1)
            .Add(MidSlot2)
            .Add(MidSlot3)
            .Add(MidSlot4)
            .Add(MidSlot5)
            .Add(MidSlot6)
            .Add(MidSlot7)
            .Add(MidSlot8)

            .Add(LowSlot1)
            .Add(LowSlot2)
            .Add(LowSlot3)
            .Add(LowSlot4)
            .Add(LowSlot5)
            .Add(LowSlot6)
            .Add(LowSlot7)
            .Add(LowSlot8)

            .Add(ServiceSlot1)
            .Add(ServiceSlot2)
            .Add(ServiceSlot3)
            .Add(ServiceSlot4)
            .Add(ServiceSlot5)
            .Add(ServiceSlot6)

            .Add(RigSlot1)
            .Add(RigSlot2)
            .Add(RigSlot3)
        End With

        ' Load the image
        Call LoadCitadelRenderImage(MainRenderImage)

    End Sub

    Private Sub ServiceModuleListView_MouseDown(sender As Object, e As MouseEventArgs) Handles ServiceModuleListView.MouseDown
        ' Make sure we select the image
        Dim Selection As ListViewItem = ServiceModuleListView.GetItemAt(e.X, e.Y)

        If Not IsNothing(Selection) Then
            pbFloat.Image = ImageList1.Images(Selection.ImageIndex)
        Else
            pbFloat.Image = Nothing
        End If

        If Not IsNothing(pbFloat.Image) Then
            pbFloat.Visible = True
            pbFloat.Location = New Point(e.X + ServiceModuleListView.Left, e.Y + ServiceModuleListView.Top)
            ' Now select the image and connect it to the mouse cursor
            SendMessage(pbFloat.Handle.ToInt32, WM_NCLBUTTONDOWN, HTCAPTION, 0&)
        Else
            pbFloat.Visible = False
        End If

        pbFloat.Visible = False

        Dim SlotLocation As Point
        Dim WHAdjust As Integer = 64
        Dim MP As Point = PointToClient(MousePosition)

        ' Loop through all the picture boxes and update the one they clicked over
        For Each Slot In SlotPictureBoxList

            SlotLocation = Slot.Location
            SlotLocation.X += tabCitadel.Left
            SlotLocation.Y += tabCitadel.Top

            ' See if they dropped the image on a fitting slot and change the selected item
            If MP.X > SlotLocation.X And MP.X < SlotLocation.X + WHAdjust And
                MP.Y > SlotLocation.Y And MP.Y < SlotLocation.Y + WHAdjust Then
                Slot.Image = pbFloat.Image
                Call AddFitting(Slot.Image)
                Exit For
            End If
        Next

    End Sub

    Private Sub AddFitting(FittingImage As Image)

        Call UpdateFitting()
    End Sub

    Private Sub UpdateFitting()

    End Sub

    ' Load the image into the background
    Private Sub LoadCitadelRenderImage(ImageRenderID As CitadelImageRender)
        Select Case ImageRenderID
            Case CitadelImageRender.Azbel
                StructurePicture.Image = My.Resources.AzbelRender
            Case CitadelImageRender.Raitaru
                StructurePicture.Image = My.Resources.RaitaruRender
            Case CitadelImageRender.Sotiyo
                StructurePicture.Image = My.Resources.SotiyoRender
        End Select

        StructurePicture.Refresh()
        Call InitializeCitadelSlots(ImageRenderID)
        Application.DoEvents()

    End Sub

    Private Sub cmbCitadelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCitadelName.SelectedIndexChanged
        Select Case cmbCitadelName.Text
            Case "Azbel"
                Call LoadCitadelRenderImage(CitadelImageRender.Azbel)
            Case "Raitaru"
                Call LoadCitadelRenderImage(CitadelImageRender.Raitaru)
            Case "Sotiyo"
                Call LoadCitadelRenderImage(CitadelImageRender.Sotiyo)
            Case Else
                Call LoadCitadelRenderImage(CitadelImageRender.Azbel)
        End Select
    End Sub

    ' Clear and Set the slots to match the citadel we are using
    Private Sub InitializeCitadelSlots(CitadelID As CitadelImageRender)
        Dim SQL As String = ""
        Dim rsReader As SQLite.SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        ' Query all the stats for the selected Citadel and process slots
        'Select Case type_attributes.attributeID, attributename, displayname, coalesce(valueint, valuefloat) 
        'From type_attributes, attribute_types
        'Where typeid = 35825 And type_attributes.attributeID = attribute_types.attributeid

        SQL = "SELECT COUNT(*) FROM ALL_BLUEPRINTS"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        While rsReader.Read()

        End While

        rsReader.Close()
    End Sub

End Class

Public Enum CitadelImageRender
    Raitaru = 35825
    Azbel = 32826
    Sotiyo = 35827
End Enum

Public Enum StructureAttributes
    hp = 9
    powerOutput = 11
    lowSlots = 12
    medSlots = 13
    hiSlots = 14
    cpuOutput = 48
    rechargeRate = 55
    maxTargetRange = 76
    launcherSlotsLeft = 101
    kineticDamageResonance = 109
    thermalDamageResonance = 110
    explosiveDamageResonance = 111
    emDamageResonance = 113
    uniformity = 136
    maxLockedTargets = 192
    scanRadarStrength = 208
    scanLadarStrength = 209
    scanMagnetometricStrength = 210
    scanGravimetricStrength = 211
    shieldCapacity = 263
    armorHP = 265
    armorEmDamageResonance = 267
    armorExplosiveDamageResonance = 268
    armorKineticDamageResonance = 269
    armorThermalDamageResonance = 270
    shieldEmDamageResonance = 271
    shieldExplosiveDamageResonance = 272
    shieldKineticDamageResonance = 273
    shieldThermalDamageResonance = 274
    droneCapacity = 283
    shieldRechargeRate = 479
    capacitorCapacity = 482
    shieldUniformity = 484
    armorUniformity = 524
    signatureRadius = 552
    scanResolution = 564
    upgradeCapacity = 1132
    rigSlots = 1137
    upgradeSlotsLeft = 1154
    heatCapacityHi = 1178
    heatCapacityMed = 1199
    heatCapacityLow = 1200
    heatAttenuationHi = 1259
    heatAttenuationMed = 1261
    heatAttenuationLow = 1262
    droneBandwidth = 1271
    rigSize = 1547
    shieldDamageLimit = 2034
    armorDamageLimit = 2035
    structureDamageLimit = 2036
    energyWarfareResistance = 2045
    serviceSlots = 2056
    vulnerabilityRequired = 2111
    sensorDampenerResistance = 2112
    weaponDisruptionResistance = 2113
    targetPainterResistance = 2114
    stasisWebifierResistance = 2115
    remoteRepairImpedance = 2116
    remoteAssistanceImpedance = 2135
    fighterAbilityAntiCapitalMissileResistance = 2244
    ECMResistance = 2253
    tetheringRange = 2268
    structureServiceRoleBonus = 2339
    strEngMatBonus = 2600
    strEngCostBonus = 2601
    strEngTimeBonus = 2602
End Enum