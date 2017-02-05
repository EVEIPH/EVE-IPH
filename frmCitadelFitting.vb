Imports System.Data.SQLite

Public Class frmCitadelFitting

    Declare Function SendMessage Lib "User32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Const WM_NCLBUTTONDOWN As Integer = &HA1
    Const HTCAPTION As Integer = 2

    Private SlotPictureBoxList As New List(Of PictureBox)
    Private FirstLoad As Boolean

    Public CurrentCitadel As String

    Public Raitaru As String = "Raitaru"
    Public Azbel As String = "Azbel"
    Public Sotiyo As String = "Sotiyo"
    Private Enum SlotSizes
        LowSlot = 11
        MediumSlot = 13
        HighSlot = 12
    End Enum

    Private Enum StructureAttributes
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FirstLoad = True

        'Temp stuff
        CurrentCitadel = Azbel
        EVEDB = New DBConnection(SQLiteDBFileName)

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

        ' Set the combo text
        cmbCitadelName.Text = CurrentCitadel

        ' Add all the images to the image list
        Call LoadFittingImages()

        ' Load the citadel
        Call InitCitadel()

        FirstLoad = False

    End Sub

    Private Sub ServiceModuleListView_MouseDown(sender As Object, e As MouseEventArgs) Handles ServiceModuleListView.MouseDown
        ' Make sure we select the image
        Dim Selection As ListViewItem = ServiceModuleListView.GetItemAt(e.X, e.Y)

        If Not IsNothing(Selection) Then
            pbFloat.Image = FittingImages.Images(Selection.ImageKey)
            pbFloat.Tag = Selection.Group.Tag
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
                Dim FloatSlot As String = CStr(pbFloat.Tag)
                If FloatSlot.Contains(Slot.Name.Substring(0, Len(Slot.Name) - 1)) Then
                    ' Only drop if over the right slot
                    Slot.Image = pbFloat.Image
                    Exit For
                End If
            End If
        Next

    End Sub

    Private Sub InitCitadel()
        ' Load the image
        Call LoadCitadelRenderImage()
        ' Refresh the items list
        Call UpdateFittingImages()
        ' Set the slots
        Call UpdateCitadelSlots()

    End Sub

    ' Load the image into the background
    Private Sub LoadCitadelRenderImage()

        Select Case cmbCitadelName.Text
            Case Azbel
                StructurePicture.Image = My.Resources.AzbelRender
            Case Raitaru
                StructurePicture.Image = My.Resources.RaitaruRender
            Case Sotiyo
                StructurePicture.Image = My.Resources.SotiyoRender
        End Select

        StructurePicture.Refresh()
        Application.DoEvents()

    End Sub

    ' Clear and Set the slots to match the citadel we are using
    Private Sub UpdateCitadelSlots()
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand
        Dim AID As Integer

        ' Query all the stats for the selected Citadel and process slots
        SQL = "SELECT attributeID, COALESCE(valueint, valuefloat) AS Value "
        SQL &= "FROM TYPE_ATTRIBUTES, INVENTORY_TYPES "
        SQL &= "WHERE attributeID IN (" & StructureAttributes.hiSlots & "," & StructureAttributes.medSlots & "," & StructureAttributes.lowSlots & "," & StructureAttributes.serviceSlots & "," & StructureAttributes.rigSlots & ") "
        SQL &= "AND INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID AND typeName = '" & cmbCitadelName.Text & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        While rsReader.Read()
            AID = rsReader.GetInt32(0)
            If AID = StructureAttributes.hiSlots Then
                Call SetHighSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = StructureAttributes.medSlots Then
                Call SetMidSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = StructureAttributes.lowSlots Then
                Call SetLowSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = StructureAttributes.rigSlots Then
                Call SetRigSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = StructureAttributes.serviceSlots Then
                Call SetServiceSlots(CInt(rsReader.GetValue(1)))
            End If
        End While

        rsReader.Close()

    End Sub

    ' Loads the images for fittings in the image lists
    Private Sub LoadFittingImages()
        Dim SQL As String = ""
        Dim rsReader As SQLiteDataReader
        Dim DBCommand As SQLiteCommand

        SQL = "SELECT typeID, typeName FROM INVENTORY_TYPES, INVENTORY_GROUPS "
        SQL &= "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID AND categoryID = 66 "
        SQL &= "AND INVENTORY_TYPES.published <> 0"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        Dim myImage As Image
        Dim typeID As String
        Dim typeName As String

        While rsReader.Read()
            ' Add to the image list, and put in view with names
            typeID = CStr(rsReader.GetInt32(0))
            typeName = rsReader.GetString(1)
            myImage = Image.FromFile(BPImageFilePath & typeID & "_64.png")

            Call FittingImages.Images.Add(typeID, myImage)

        End While

        rsReader.Close()

    End Sub

    ' Updates all the fitting images based on the check boxes in the list view
    Private Sub UpdateFittingImages()

        If Not FirstLoad Then

            ' Clear current images
            ServiceModuleListView.Items.Clear()

            Dim SQL As String = ""
            Dim RigString As String = ""
            Dim SlotString As String = ""
            Dim ServicesString As String = ""
            Dim SQLList As New List(Of String)
            Dim rsReader As SQLiteDataReader
            Dim DBCommand As SQLiteCommand

            SQL = "SELECT INVENTORY_TYPES.typeID, INVENTORY_GROUPS.groupID, typeName, CASE WHEN effectID IS NULL THEN -1 ELSE effectID END AS EffID, groupName "
            SQL &= "FROM INVENTORY_GROUPS, INVENTORY_TYPES "
            SQL &= "LEFT JOIN TYPE_EFFECTS ON INVENTORY_TYPES.typeID = TYPE_EFFECTS.typeID AND effectID IN (12,13,11) "
            SQL &= "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID AND categoryID = 66 "
            SQL &= "AND INVENTORY_TYPES.published <> 0 "

            If chkItemViewTypeServices.Checked Then
                ServicesString &= "(INVENTORY_TYPES.groupID IN (1321, 1322, 1415, 1717) "
                If cmbCitadelName.Text = Azbel Then
                    ' Azbel can't use cap or super capital arrays
                    ServicesString &= "AND INVENTORY_TYPES.typeID NOT IN (35881,35877))"
                Else
                    ServicesString &= ")"
                End If
                ' Add the sql
                Call SQLList.Add(ServicesString)
            End If

            ' Process high, medium, and low slots together
            If chkItemViewTypeHigh.Checked Then
                SlotString &= CStr(SlotSizes.HighSlot) & ","
            End If

            If chkItemViewTypeMedium.Checked Then
                SlotString &= CStr(SlotSizes.MediumSlot) & ","
            End If

            If chkItemViewTypeLow.Checked Then
                SlotString &= CStr(SlotSizes.LowSlot) & ","
            End If

            If SlotString <> "" Then
                SlotString = SlotString.Substring(0, Len(SlotString) - 1)
                SlotString = "(EffID IN (" & SlotString & "))"
                ' Add the sql
                Call SQLList.Add(SlotString)
            End If

            If chkRigTypeViewCombat.Checked Then
                Call SQLList.Add("(groupName Like 'Structure Combat Rig " & GetRigSize() & "%')")
            End If

            If chkRigTypeViewEngineering.Checked Then
                Call SQLList.Add("(groupName LIKE 'Structure Engineering Rig " & GetRigSize() & "%')")
            End If

            If chkRigTypeViewReprocessing.Checked Then
                Call SQLList.Add("(groupName LIKE 'Structure Resource Rig " & GetRigSize() & "%')")
            End If

            ' Set the SQL
            If SQLList.Count > 0 Then
                SQL &= "AND ("
                For Each entry In SQLList
                    SQL &= "(" & entry & ") OR "
                Next
                ' Strip last OR
                SQL = SQL.Substring(0, Len(SQL) - 4)
                SQL &= ")"
            Else
                Exit Sub
            End If

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsReader = DBCommand.ExecuteReader

            While rsReader.Read()
                Dim GID As Integer = rsReader.GetInt32(1)
                Dim EID As Integer = rsReader.GetInt32(3)
                Dim LVI As New ListViewItem

                If GID = 1321 Or GID = 1322 Or GID = 1415 Or GID = 1717 Then
                    LVI.Group = ServiceModuleListView.Groups(0) ' 0 is services
                ElseIf EID = SlotSizes.HighSlot Then
                    LVI.Group = ServiceModuleListView.Groups(1) ' 1 is high
                ElseIf EID = SlotSizes.MediumSlot Then
                    LVI.Group = ServiceModuleListView.Groups(2) ' 2 is medium
                ElseIf EID = SlotSizes.LowSlot Then
                    LVI.Group = ServiceModuleListView.Groups(3) ' 3 is low
                Else
                    ' Rigs
                    If rsReader.GetString(4).Contains("Combat Rig") Then
                        LVI.Group = ServiceModuleListView.Groups(4) ' 4 is Combat rigs
                    ElseIf rsReader.GetString(4).Contains("Reprocessing Rig") Then
                        LVI.Group = ServiceModuleListView.Groups(5) ' 5 is Reprocessing rigs
                    ElseIf rsReader.GetString(4).Contains("Engineering Rig") Then
                        LVI.Group = ServiceModuleListView.Groups(6) ' 6 is Engineering rigs
                    End If
                End If

                ' add the image
                LVI.ImageKey = CStr(rsReader.GetInt32(0))
                LVI.Text = rsReader.GetString(2)
                ServiceModuleListView.Items.Add(LVI)

            End While

        End If

    End Sub

    Private Function GetRigSize() As String
        Select Case cmbCitadelName.Text
            Case Azbel ' Medium
                Return "M"
            Case Raitaru ' Large
                Return "L"
            Case Sotiyo ' Extra large
                Return "XL"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub SetHighSlots(Slots As Integer)

        ' Init slots
        HighSlot1.Visible = False
        HighSlot2.Visible = False
        HighSlot3.Visible = False
        HighSlot4.Visible = False
        HighSlot5.Visible = False
        HighSlot6.Visible = False
        HighSlot7.Visible = False
        HighSlot8.Visible = False

        HighSlot1.Image = Nothing
        HighSlot2.Image = Nothing
        HighSlot3.Image = Nothing
        HighSlot4.Image = Nothing
        HighSlot5.Image = Nothing
        HighSlot6.Image = Nothing
        HighSlot7.Image = Nothing
        HighSlot8.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    HighSlot1.Visible = True
                Case 2
                    HighSlot2.Visible = True
                Case 3
                    HighSlot3.Visible = True
                Case 4
                    HighSlot4.Visible = True
                Case 5
                    HighSlot5.Visible = True
                Case 6
                    HighSlot6.Visible = True
                Case 7
                    HighSlot7.Visible = True
                Case 8
                    HighSlot8.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetMidSlots(Slots As Integer)

        ' Init slots
        MidSlot1.Visible = False
        MidSlot2.Visible = False
        MidSlot3.Visible = False
        MidSlot4.Visible = False
        MidSlot5.Visible = False
        MidSlot6.Visible = False
        MidSlot7.Visible = False
        MidSlot8.Visible = False

        MidSlot1.Image = Nothing
        MidSlot2.Image = Nothing
        MidSlot3.Image = Nothing
        MidSlot4.Image = Nothing
        MidSlot5.Image = Nothing
        MidSlot6.Image = Nothing
        MidSlot7.Image = Nothing
        MidSlot8.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    MidSlot1.Visible = True
                Case 2
                    MidSlot2.Visible = True
                Case 3
                    MidSlot3.Visible = True
                Case 4
                    MidSlot4.Visible = True
                Case 5
                    MidSlot5.Visible = True
                Case 6
                    MidSlot6.Visible = True
                Case 7
                    MidSlot7.Visible = True
                Case 8
                    MidSlot8.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetLowSlots(Slots As Integer)

        ' Init slots
        LowSlot1.Visible = False
        LowSlot2.Visible = False
        LowSlot3.Visible = False
        LowSlot4.Visible = False
        LowSlot5.Visible = False
        LowSlot6.Visible = False
        LowSlot7.Visible = False
        LowSlot8.Visible = False

        LowSlot1.Image = Nothing
        LowSlot2.Image = Nothing
        LowSlot3.Image = Nothing
        LowSlot4.Image = Nothing
        LowSlot5.Image = Nothing
        LowSlot6.Image = Nothing
        LowSlot7.Image = Nothing
        LowSlot8.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    LowSlot1.Visible = True
                Case 2
                    LowSlot2.Visible = True
                Case 3
                    LowSlot3.Visible = True
                Case 4
                    LowSlot4.Visible = True
                Case 5
                    LowSlot5.Visible = True
                Case 6
                    LowSlot6.Visible = True
                Case 7
                    LowSlot7.Visible = True
                Case 8
                    LowSlot8.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetRigSlots(Slots As Integer)

        ' Init slots
        RigSlot1.Visible = False
        RigSlot2.Visible = False
        RigSlot3.Visible = False

        RigSlot1.Image = Nothing
        RigSlot2.Image = Nothing
        RigSlot3.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    RigSlot1.Visible = True
                Case 2
                    RigSlot2.Visible = True
                Case 3
                    RigSlot3.Visible = True
            End Select
        Next
    End Sub

    Private Sub SetServiceSlots(Slots As Integer)

        ' Init slots
        ServiceSlot1.Visible = False
        ServiceSlot2.Visible = False
        ServiceSlot3.Visible = False
        ServiceSlot4.Visible = False
        ServiceSlot5.Visible = False
        ServiceSlot6.Visible = False

        ServiceSlot1.Image = Nothing
        ServiceSlot2.Image = Nothing
        ServiceSlot3.Image = Nothing
        ServiceSlot4.Image = Nothing
        ServiceSlot5.Image = Nothing
        ServiceSlot6.Image = Nothing

        For i = 1 To Slots
            Select Case i
                Case 1
                    ServiceSlot1.Visible = True
                Case 2
                    ServiceSlot2.Visible = True
                Case 3
                    ServiceSlot3.Visible = True
                Case 4
                    ServiceSlot4.Visible = True
                Case 5
                    ServiceSlot5.Visible = True
                Case 6
                    ServiceSlot6.Visible = True
            End Select
        Next
    End Sub

#Region "Click Events"
    Private Sub cmbCitadelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCitadelName.SelectedIndexChanged
        Call InitCitadel()
    End Sub

    Private Sub chkItemViewTypeAll_CheckedChanged(sender As Object, e As EventArgs)
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeHigh_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeHigh.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeLow_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeLow.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeMedium_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeMedium.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkItemViewTypeServices_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemViewTypeServices.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewCombat_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewCombat.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewEngineering_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewEngineering.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub chkRigTypeViewReprocessing_CheckedChanged(sender As Object, e As EventArgs) Handles chkRigTypeViewReprocessing.CheckedChanged
        Call UpdateFittingImages()
    End Sub

    Private Sub cmbCitadelName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCitadelName.KeyPress
        e.Handled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub btnSaveUpdatePrices_Click(sender As Object, e As EventArgs) Handles btnSaveUpdatePrices.Click

    End Sub

    Private Sub btnToggleAllPriceItems_Click(sender As Object, e As EventArgs) Handles btnToggleAllPriceItems.Click

    End Sub

#End Region

End Class

Public Enum CitadelImageRender
    Raitaru = 35825
    Azbel = 32826
    Sotiyo = 35827
End Enum