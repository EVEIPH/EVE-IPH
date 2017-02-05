Imports System.Data.SQLite

Public Class frmCitadelFitting

    Declare Function SendMessage Lib "User32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Const WM_NCLBUTTONDOWN As Integer = &HA1
    Const HTCAPTION As Integer = 2

    Private SlotPictureBoxList As New List(Of PictureBox)
    Private FirstLoad As Boolean

    Public CurrentCitadel As String

    Private Attributes As New EVEAttributes
    ' Stores all the stats for the selected citadel
    Private CitadelStats As New CitadelAttributes

    Public Raitaru As String = "Raitaru"
    Public Azbel As String = "Azbel"
    Public Sotiyo As String = "Sotiyo"
    Private Enum SlotSizes
        LowSlot = 11
        MediumSlot = 13
        HighSlot = 12
    End Enum

    ' For saving and updating the selected citadel
    Private Structure CitadelAttributes
        Dim CPU As Double
        Dim MaxCPU As Double
        Dim PG As Double
        Dim MaxPG As Double
        Dim Calibration As Double
        Dim MaxCalibration As Double
        Dim Capacitor As Double
        Dim MaxCapacitor As Double
        Dim CapacitorRechargeRate As Double
        Dim BaseCapRechargeRate As Double

    End Structure

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
        Dim ModuleTypeID As String = Selection.ImageKey

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
                    If RigFound(ModuleTypeID) Then
                        ' They already used this rig, so don't allow
                        Exit Sub
                    End If
                    ' Set the image info
                    Slot.Image = pbFloat.Image
                    Slot.Image.Tag = ModuleTypeID

                    ' Update the slot stats
                    Call UpdateCitadelStats()

                    ' Done updating
                    Exit For
                End If
            End If
        Next

    End Sub

    ' Sees if the rig is already used or not
    Private Function RigFound(TypeID As String) As Boolean
        Dim CurrentRigTypes As New List(Of String)

        If Not IsNothing(RigSlot1.Image) Then
            CurrentRigTypes.Add(CStr(RigSlot1.Image.Tag))
        End If
        If Not IsNothing(RigSlot2.Image) Then
            CurrentRigTypes.Add(CStr(RigSlot2.Image.Tag))
        End If
        If Not IsNothing(RigSlot3.Image) Then
            CurrentRigTypes.Add(CStr(RigSlot3.Image.Tag))
        End If

        If CurrentRigTypes.Contains(TypeID) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub InitCitadel()
        ' Load the image
        Call LoadCitadelRenderImage()
        ' Refresh the items list
        Call UpdateFittingImages()
        ' Set the slots
        Call UpdateCitadelSlots()
        ' Set the stats
        Call LoadCitadelStats()

    End Sub

    Private Sub StripFitting()

        HighSlot1.Image = Nothing
        HighSlot2.Image = Nothing
        HighSlot3.Image = Nothing
        HighSlot4.Image = Nothing
        HighSlot5.Image = Nothing
        HighSlot6.Image = Nothing
        HighSlot7.Image = Nothing
        HighSlot8.Image = Nothing

        MidSlot1.Image = Nothing
        MidSlot2.Image = Nothing
        MidSlot3.Image = Nothing
        MidSlot4.Image = Nothing
        MidSlot5.Image = Nothing
        MidSlot6.Image = Nothing
        MidSlot7.Image = Nothing
        MidSlot8.Image = Nothing

        LowSlot1.Image = Nothing
        LowSlot2.Image = Nothing
        LowSlot3.Image = Nothing
        LowSlot4.Image = Nothing
        LowSlot5.Image = Nothing
        LowSlot6.Image = Nothing
        LowSlot7.Image = Nothing
        LowSlot8.Image = Nothing

        ServiceSlot1.Image = Nothing
        ServiceSlot2.Image = Nothing
        ServiceSlot3.Image = Nothing
        ServiceSlot4.Image = Nothing
        ServiceSlot5.Image = Nothing
        ServiceSlot6.Image = Nothing

        RigSlot1.Image = Nothing
        RigSlot2.Image = Nothing
        RigSlot3.Image = Nothing

        ' init the citadel stats
        Call LoadCitadelStats()

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
        SQL &= "WHERE attributeID IN (" & ItemAttributes.hiSlots & "," & ItemAttributes.medSlots & "," & ItemAttributes.lowSlots & "," & ItemAttributes.serviceSlots & "," & ItemAttributes.rigSlots & ") "
        SQL &= "AND INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID AND typeName = '" & cmbCitadelName.Text & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsReader = DBCommand.ExecuteReader

        While rsReader.Read()
            AID = rsReader.GetInt32(0)
            If AID = ItemAttributes.hiSlots Then
                Call SetHighSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.medSlots Then
                Call SetMidSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.lowSlots Then
                Call SetLowSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.rigSlots Then
                Call SetRigSlots(CInt(rsReader.GetValue(1)))
            ElseIf AID = ItemAttributes.serviceSlots Then
                Call SetServiceSlots(CInt(rsReader.GetValue(1)))
            End If
        End While

        rsReader.Close()

    End Sub

    ' Updates the stats after a module is chosen
    Private Sub LoadCitadelStats(Optional IgnoreLabelUpdate As Boolean = False)
        Dim Stats As New List(Of AttributeRecord)
        Dim AttributesLookup As New EVEAttributes

        ' Get all the stats for the citadel 
        Stats = AttributesLookup.GetAttributes(cmbCitadelName.Text)

        ' Loop through and get the stuff we want, save it locally for update
        For Each Stat In Stats
            Select Case Stat.ID
                Case ItemAttributes.cpuOutput
                    CitadelStats.MaxCPU = Stat.Value
                    CitadelStats.CPU = 0
                Case ItemAttributes.powerOutput
                    CitadelStats.MaxPG = Stat.Value
                    CitadelStats.PG = 0
                Case ItemAttributes.upgradeCapacity ' Calibration
                    CitadelStats.MaxCalibration = Stat.Value
                    CitadelStats.Calibration = 0
                Case ItemAttributes.capacitorCapacity
                    CitadelStats.Capacitor = 0
                    CitadelStats.MaxCapacitor = Stat.Value
                Case ItemAttributes.rechargeRate
                    CitadelStats.CapacitorRechargeRate = 100
                    CitadelStats.BaseCapRechargeRate = Stat.Value
            End Select
        Next

        ' Update the stats
        If Not IgnoreLabelUpdate Then
            Call UpdateCitadelStatLabels()
        End If

    End Sub

    ' Updates the label stats of the citadel to include any items selected and installed
    Private Sub UpdateCitadelStatLabels()

        ' Update the labels
        lblCPU.Text = FormatNumber(CitadelStats.CPU) & " / " & FormatNumber(CitadelStats.MaxCPU)
        If CitadelStats.CPU > CitadelStats.MaxCPU Then
            lblCPU.ForeColor = Color.Red
        Else
            lblCPU.ForeColor = Color.Black
        End If

        lblPowerGrid.Text = FormatNumber(CitadelStats.PG) & " / " & FormatNumber(CitadelStats.MaxPG)
        If CitadelStats.PG > CitadelStats.MaxPG Then
            lblPowerGrid.ForeColor = Color.Red
        Else
            lblPowerGrid.ForeColor = Color.Black
        End If

        lblCalibration.Text = FormatNumber(CitadelStats.Calibration) & " / " & FormatNumber(CitadelStats.MaxCalibration)
        If CitadelStats.Calibration > CitadelStats.MaxCalibration Then
            lblCalibration.ForeColor = Color.Red
        Else
            lblCalibration.ForeColor = Color.Black
        End If

        lblCapacitorValues.Text = FormatNumber(CitadelStats.Capacitor) & " / " & FormatNumber(CitadelStats.MaxCapacitor)
        If CitadelStats.Capacitor > CitadelStats.MaxCapacitor Then
            lblCapacitorValues.ForeColor = Color.Red
        Else
            lblCapacitorValues.ForeColor = Color.Black
        End If

    End Sub

    Private Sub UpdateCitadelStats()
        Dim InstalledSlots As New List(Of Integer)
        Dim Attributes As New List(Of AttributeRecord)
        Dim AttribLookup As New EVEAttributes

        InstalledSlots = GetInstalledSlots()

        ' Reset the totals each time before updating
        Call LoadCitadelStats(True)

        For Each Item In InstalledSlots
            ' Look up the attributes for each slot and update the stats we want
            Attributes = AttribLookup.GetAttributes(Item)

            For Each Attribute In Attributes
                Select Case Attribute.ID
                    Case ItemAttributes.power
                        CitadelStats.PG += Attribute.Value
                    Case ItemAttributes.cpu
                        CitadelStats.CPU += Attribute.Value
                    Case ItemAttributes.capacitorNeed
                        CitadelStats.Capacitor += Attribute.Value
                    Case ItemAttributes.upgradeCost ' Calibration
                        CitadelStats.Calibration += Attribute.Value
                    Case ItemAttributes.cpuMultiplier
                        CitadelStats.MaxCPU = CitadelStats.MaxCPU * Attribute.Value
                    Case ItemAttributes.powerOutputMultiplier
                        CitadelStats.MaxPG = CitadelStats.MaxPG * Attribute.Value
                End Select
            Next
        Next

        ' Update the stats
        Call UpdateCitadelStatLabels()

    End Sub

    Private Function GetInstalledSlots() As List(Of Integer)
        Dim ReturnItems As New List(Of Integer)

        ' Go through all slots and return the typeIDs (saved in tag of image) for each installed item
        If Not IsNothing(HighSlot1.Image) Then
            ReturnItems.Add(CInt(HighSlot1.Image.Tag))
        End If
        If Not IsNothing(HighSlot2.Image) Then
            ReturnItems.Add(CInt(HighSlot2.Image.Tag))
        End If
        If Not IsNothing(HighSlot3.Image) Then
            ReturnItems.Add(CInt(HighSlot3.Image.Tag))
        End If
        If Not IsNothing(HighSlot4.Image) Then
            ReturnItems.Add(CInt(HighSlot4.Image.Tag))
        End If
        If Not IsNothing(HighSlot5.Image) Then
            ReturnItems.Add(CInt(HighSlot5.Image.Tag))
        End If
        If Not IsNothing(HighSlot6.Image) Then
            ReturnItems.Add(CInt(HighSlot6.Image.Tag))
        End If
        If Not IsNothing(HighSlot7.Image) Then
            ReturnItems.Add(CInt(HighSlot7.Image.Tag))
        End If
        If Not IsNothing(HighSlot8.Image) Then
            ReturnItems.Add(CInt(HighSlot1.Image.Tag))
        End If

        If Not IsNothing(MidSlot1.Image) Then
            ReturnItems.Add(CInt(MidSlot1.Image.Tag))
        End If
        If Not IsNothing(MidSlot2.Image) Then
            ReturnItems.Add(CInt(MidSlot2.Image.Tag))
        End If
        If Not IsNothing(MidSlot3.Image) Then
            ReturnItems.Add(CInt(MidSlot3.Image.Tag))
        End If
        If Not IsNothing(MidSlot4.Image) Then
            ReturnItems.Add(CInt(MidSlot4.Image.Tag))
        End If
        If Not IsNothing(MidSlot5.Image) Then
            ReturnItems.Add(CInt(MidSlot5.Image.Tag))
        End If
        If Not IsNothing(MidSlot6.Image) Then
            ReturnItems.Add(CInt(MidSlot6.Image.Tag))
        End If
        If Not IsNothing(MidSlot7.Image) Then
            ReturnItems.Add(CInt(MidSlot7.Image.Tag))
        End If
        If Not IsNothing(MidSlot8.Image) Then
            ReturnItems.Add(CInt(MidSlot1.Image.Tag))
        End If

        If Not IsNothing(LowSlot1.Image) Then
            ReturnItems.Add(CInt(LowSlot1.Image.Tag))
        End If
        If Not IsNothing(LowSlot2.Image) Then
            ReturnItems.Add(CInt(LowSlot2.Image.Tag))
        End If
        If Not IsNothing(LowSlot3.Image) Then
            ReturnItems.Add(CInt(LowSlot3.Image.Tag))
        End If
        If Not IsNothing(LowSlot4.Image) Then
            ReturnItems.Add(CInt(LowSlot4.Image.Tag))
        End If
        If Not IsNothing(LowSlot5.Image) Then
            ReturnItems.Add(CInt(LowSlot5.Image.Tag))
        End If
        If Not IsNothing(LowSlot6.Image) Then
            ReturnItems.Add(CInt(LowSlot6.Image.Tag))
        End If
        If Not IsNothing(LowSlot7.Image) Then
            ReturnItems.Add(CInt(LowSlot7.Image.Tag))
        End If
        If Not IsNothing(LowSlot8.Image) Then
            ReturnItems.Add(CInt(LowSlot1.Image.Tag))
        End If

        If Not IsNothing(RigSlot1.Image) Then
            ReturnItems.Add(CInt(RigSlot1.Image.Tag))
        End If
        If Not IsNothing(RigSlot2.Image) Then
            ReturnItems.Add(CInt(RigSlot2.Image.Tag))
        End If
        If Not IsNothing(RigSlot3.Image) Then
            ReturnItems.Add(CInt(RigSlot3.Image.Tag))
        End If

        If Not IsNothing(ServiceSlot1.Image) Then
            ReturnItems.Add(CInt(ServiceSlot1.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot2.Image) Then
            ReturnItems.Add(CInt(ServiceSlot2.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot3.Image) Then
            ReturnItems.Add(CInt(ServiceSlot3.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot4.Image) Then
            ReturnItems.Add(CInt(ServiceSlot4.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot5.Image) Then
            ReturnItems.Add(CInt(ServiceSlot5.Image.Tag))
        End If
        If Not IsNothing(ServiceSlot6.Image) Then
            ReturnItems.Add(CInt(ServiceSlot6.Image.Tag))
        End If

        Return ReturnItems

    End Function

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
                    If rsReader.GetString(4).Contains("Combat") Then
                        LVI.Group = ServiceModuleListView.Groups(4) ' 4 is Combat rigs
                    ElseIf rsReader.GetString(4).Contains("Reprocessing") Then
                        LVI.Group = ServiceModuleListView.Groups(5) ' 5 is Reprocessing rigs
                    ElseIf rsReader.GetString(4).Contains("Engineering") Then
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

    Private Sub MidSlot1_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot1.DoubleClick
        MidSlot1.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub MidSlot2_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot2.DoubleClick
        MidSlot2.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub MidSlot3_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot3.DoubleClick
        MidSlot3.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub MidSlot4_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot4.DoubleClick
        MidSlot4.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub MidSlot5_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot5.DoubleClick
        MidSlot5.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub MidSlot6_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot6.DoubleClick
        MidSlot6.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub MidSlot7_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot7.DoubleClick
        MidSlot7.Image = Nothing
    End Sub

    Private Sub MidSlot8_DoubleClick(sender As Object, e As EventArgs) Handles MidSlot8.DoubleClick
        MidSlot8.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot1_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot1.DoubleClick
        HighSlot1.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot2_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot2.DoubleClick
        HighSlot2.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot3_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot3.DoubleClick
        HighSlot3.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot5_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot5.DoubleClick
        HighSlot5.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot7_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot7.DoubleClick
        HighSlot7.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot4_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot4.DoubleClick
        HighSlot4.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot6_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot6.DoubleClick
        HighSlot6.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub HighSlot8_DoubleClick(sender As Object, e As EventArgs) Handles HighSlot8.DoubleClick
        HighSlot8.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub RigSlot3_DoubleClick(sender As Object, e As EventArgs) Handles RigSlot3.DoubleClick
        RigSlot3.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub RigSlot2_DoubleClick(sender As Object, e As EventArgs) Handles RigSlot2.DoubleClick
        RigSlot2.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub RigSlot1_DoubleClick(sender As Object, e As EventArgs) Handles RigSlot1.DoubleClick
        RigSlot1.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot1_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot1.DoubleClick
        LowSlot1.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot2_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot2.DoubleClick
        LowSlot2.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot3_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot3.DoubleClick
        LowSlot3.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot4_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot4.DoubleClick
        LowSlot4.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot5_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot5.DoubleClick
        LowSlot5.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot6_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot6.DoubleClick
        LowSlot6.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot7_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot7.DoubleClick
        LowSlot7.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub LowSlot8_DoubleClick(sender As Object, e As EventArgs) Handles LowSlot8.DoubleClick
        LowSlot8.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub ServiceSlot5_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot5.DoubleClick
        ServiceSlot5.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub ServiceSlot3_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot3.DoubleClick
        ServiceSlot3.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub ServiceSlot1_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot1.DoubleClick
        ServiceSlot1.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub ServiceSlot2_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot2.DoubleClick
        ServiceSlot2.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub ServiceSlot4_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot4.DoubleClick
        ServiceSlot4.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub ServiceSlot6_DoubleClick(sender As Object, e As EventArgs) Handles ServiceSlot6.DoubleClick
        ServiceSlot6.Image = Nothing
        Call UpdateCitadelStats()
    End Sub

    Private Sub btnToggleAllPriceItems_Click(sender As Object, e As EventArgs) Handles btnToggleAllPriceItems.Click
        Call StripFitting()
    End Sub

    Private Sub btnRefreshBlockData_Click(sender As Object, e As EventArgs) Handles btnRefreshBlockData.Click

    End Sub

#End Region

End Class

Public Enum CitadelImageRender
    Raitaru = 35825
    Azbel = 32826
    Sotiyo = 35827
End Enum