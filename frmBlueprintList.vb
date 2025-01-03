﻿Imports System.Data.SQLite

Public Class frmBlueprintList

    Public Event BPSelected(bpName As String)

    Private Class NodeTag
        Public Property FilterField As String
        Public Property FilterValue As Integer?
        Public Sub New(field As String, value As Integer?)
            FilterField = field
            FilterValue = value
        End Sub
    End Class

    Private FirstFormLoad As Boolean

    Public Sub New()

        FirstFormLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblIntro.Text = "Expand the tree to locate a Blueprint." + Environment.NewLine + "Double-Click on it to load it into the main window." + Environment.NewLine + "This window will remain open unless you click Close."

        ' Load settings, which will fire handler to set top nodes for the saved options
        Call InitForm()

        ' Load the tree
        Call SetTopNodes()

        FirstFormLoad = False

    End Sub

    ' Loads settings on the form
    Private Sub InitForm()

        With UserBPViewerSettings
            ' Load saved settings
            Select Case .BlueprintTypeSelection
                Case rbtnBPAll.Text
                    rbtnBPAll.Checked = True
                Case rbtnBPOwnedBlueprints.Text
                    rbtnBPOwnedBlueprints.Checked = True
                Case rbtnBPFavoriteBlueprints.Text
                    rbtnBPFavoriteBlueprints.Checked = True
                Case rbtnBPShipBlueprints.Text
                    rbtnBPShipBlueprints.Checked = True
                Case rbtnBPDroneBlueprints.Text
                    rbtnBPDroneBlueprints.Checked = True
                Case rbtnBPAmmoChargeBlueprints.Text
                    rbtnBPAmmoChargeBlueprints.Checked = True
                Case rbtnBPModuleBlueprints.Text
                    rbtnBPModuleBlueprints.Checked = True
                Case rbtnBPComponentBlueprints.Text
                    rbtnBPComponentBlueprints.Checked = True
                Case rbtnBPStructureBlueprints.Text
                    rbtnBPStructureBlueprints.Checked = True
                Case rbtnBPSubsystemBlueprints.Text
                    rbtnBPSubsystemBlueprints.Checked = True
                Case rbtnBPRigBlueprints.Text
                    rbtnBPRigBlueprints.Checked = True
                Case rbtnBPBoosterBlueprints.Text
                    rbtnBPBoosterBlueprints.Checked = True
                Case rbtnBPMiscBlueprints.Text
                    rbtnBPMiscBlueprints.Checked = True
                Case rbtnBPDeployableBlueprints.Text
                    rbtnBPDeployableBlueprints.Checked = True
                Case rbtnBPCelestialsBlueprints.Text
                    rbtnBPCelestialsBlueprints.Checked = True
                Case rbtnBPStructureRigsBlueprints.Text
                    rbtnBPStructureRigsBlueprints.Checked = True
                Case rbtnBPReactionBlueprints.Text
                    rbtnBPReactionBlueprints.Checked = True
            End Select

            chkBPNPCBPOs.Checked = .BPNPCBPOsCheck

            chkBPTech1.Checked = .Tech1Check
            chkBPTech2.Checked = .Tech2Check
            chkBPTech3.Checked = .Tech3Check
            chkBPNavy.Checked = .TechFactionCheck
            chkBPStory.Checked = .TechStorylineCheck
            chkBPPirate.Checked = .TechPirateCheck

            ' chkBPIncludeIgnoredBPs.Checked = .IncludeIgnoredBPs

            chkBPSmall.Checked = .SmallCheck
            chkBPMedium.Checked = .MediumCheck
            chkBPLarge.Checked = .LargeCheck
            chkBPXLarge.Checked = .XLCheck

        End With

    End Sub

    ' Saves settings for form to XML
    Private Sub btnReactionsSaveSettings_Click(sender As Object, e As EventArgs) Handles btnReactionsSaveSettings.Click
        Dim TempSettings As BPViewerSettings = Nothing
        Dim Settings As New ProgramSettings

        With TempSettings
            If rbtnBPAll.Checked Then
                .BlueprintTypeSelection = rbtnBPAll.Text
            ElseIf rbtnBPOwnedBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPOwnedBlueprints.Text
            ElseIf rbtnBPFavoriteBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPFavoriteBlueprints.Text
            ElseIf rbtnBPShipBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPShipBlueprints.Text
            ElseIf rbtnBPDroneBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPDroneBlueprints.Text
            ElseIf rbtnBPAmmoChargeBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPAmmoChargeBlueprints.Text
            ElseIf rbtnBPModuleBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPModuleBlueprints.Text
            ElseIf rbtnBPComponentBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPComponentBlueprints.Text
            ElseIf rbtnBPStructureBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPStructureBlueprints.Text
            ElseIf rbtnBPSubsystemBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPSubsystemBlueprints.Text
            ElseIf rbtnBPRigBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPRigBlueprints.Text
            ElseIf rbtnBPBoosterBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPBoosterBlueprints.Text
            ElseIf rbtnBPMiscBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPMiscBlueprints.Text
            ElseIf rbtnBPCelestialsBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPCelestialsBlueprints.Text
            ElseIf rbtnBPDeployableBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPDeployableBlueprints.Text
            ElseIf rbtnBPStructureRigsBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPStructureRigsBlueprints.Text
            ElseIf rbtnBPReactionBlueprints.Checked Then
                .BlueprintTypeSelection = rbtnBPReactionBlueprints.Text
            End If

            .BPNPCBPOsCheck = chkBPNPCBPOs.Checked

            .Tech1Check = chkBPTech1.Checked
            .Tech2Check = chkBPTech2.Checked
            .Tech3Check = chkBPTech3.Checked
            .TechStorylineCheck = chkBPStory.Checked
            .TechFactionCheck = chkBPNavy.Checked
            .TechPirateCheck = chkBPPirate.Checked

            '.IncludeIgnoredBPs = chkBPIncludeIgnoredBPs.Checked

            .SmallCheck = chkBPSmall.Checked
            .MediumCheck = chkBPMedium.Checked
            .LargeCheck = chkBPLarge.Checked
            .XLCheck = chkBPXLarge.Checked
        End With

        Call Settings.SaveBPViewerSettings(TempSettings)

        UserBPViewerSettings = TempSettings

        MsgBox("Settings Saved", vbInformation, Application.ProductName)

    End Sub

    ' I'd like to try and find some way of merging PopulateNode and SetTopNodes, but I don't think there's a simple way
    Private Sub SetTopNodes()
        treBlueprintTreeView.Nodes.Clear()
        Using con = New SQLiteConnection(EVEDB.DBREf.ConnectionString)
            Dim com = con.CreateCommand()
            com.CommandText = BuildBPQuery("ITEM_CATEGORY", "", Nothing)

            con.Open()
            Using reader = com.ExecuteReader()
                While reader.Read
                    Dim readCategory = reader("ITEM_CATEGORY").ToString
                    Dim newNode = New TreeNode(readCategory)
                    newNode.Tag = New NodeTag("ITEM_CATEGORY", CInt(reader("FilterID")))
                    treBlueprintTreeView.Nodes.Add(newNode)
                    newNode.Nodes.Add(New TreeNode) 'dummy node to show the + mark
                End While
            End Using
        End Using

        ' If there is only one node, expand it to show the subnodes for usability
        If treBlueprintTreeView.Nodes.Count = 1 Then
            treBlueprintTreeView.Nodes(0).Expand()
        End If
    End Sub

    Private Sub treBlueprintTreeView_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles treBlueprintTreeView.BeforeExpand
        PopulateNode(e.Node)
    End Sub

    Private Function GetDisplayLevel(parentLevel As String) As String
        Select Case parentLevel
            Case "ITEM_CATEGORY"
                Return "ITEM_GROUP"
            Case "ITEM_GROUP"
                Return "MARKET_GROUP"
            Case "MARKET_GROUP"
                Return "BLUEPRINT_NAME"
            Case Else
                Throw New ArgumentOutOfRangeException($"Value of {NameOf(parentLevel)} is invalid: {parentLevel}")
        End Select
    End Function

    ' I'd like to try and find some way of merging PopulateNode and SetTopNodes, but I don't think there's a simple way
    Private Sub PopulateNode(thisNode As TreeNode)
        thisNode.Nodes.Clear()
        Dim filterLevel = CType(thisNode.Tag, NodeTag)
        Dim displayLevel = GetDisplayLevel(filterLevel.FilterField)

        Using con = New SQLiteConnection(EVEDB.DBREf.ConnectionString)
            Dim com = con.CreateCommand()
            Dim ItemGroupID As Integer = 0
            If thisNode.Name <> "" Then
                ItemGroupID = CInt(thisNode.Name)
            End If
            com.CommandText = BuildBPQuery(displayLevel, filterLevel.FilterField, filterLevel.FilterValue, ItemGroupID)

            con.Open()
            Using reader = com.ExecuteReader()
                While reader.Read
                    Dim newNode = New TreeNode(reader(displayLevel).ToString)
                    newNode.Tag = New NodeTag(displayLevel, CInt(reader("FilterID")))
                    newNode.Name = reader("ITEM_GROUP_ID").ToString ' Store the group id
                    thisNode.Nodes.Add(newNode)
                    If displayLevel <> "BLUEPRINT_NAME" Then
                        newNode.Nodes.Add(New TreeNode) 'dummy node to show the + mark
                    End If
                End While
            End Using
            con.Close()
        End Using
    End Sub

    Private Function BuildBPQuery(displayLevel As String, filterColumnName As String, filterColumnValue As Integer?, Optional ItemGroupID As Integer = 0) As String

        Dim levelFilter As String = ""
        If filterColumnName <> "" And filterColumnValue.HasValue Then
            levelFilter = $"AND {filterColumnName}_ID = {filterColumnValue}"
        End If

        ' Ignore flag
        Dim IgnoreFilter As String = ""
        'If chkBPIncludeIgnoredBPs.Checked = False Then
        '    IgnoreFilter = " AND IGNORE = 0 "
        'End If

        ' Text search
        Dim TextFilter As String = ""
        If Trim(txtBPItemFilter.Text) <> "" Then
            TextFilter = " AND BLUEPRINT_NAME LIKE '%" & FormatDBString(Trim(txtBPItemFilter.Text)) & "%' "
        End If

        Dim NPCBPOFilter As String = ""
        If chkBPNPCBPOs.Checked Then
            NPCBPOFilter = " AND i2.marketGroupID IS NOT NULL AND b.ITEM_TYPE <> 2 "
        End If

        Dim ItemGroupFilter As String = ""
        If ItemGroupID <> 0 Then
            ItemGroupFilter = "AND ITEM_GROUP_ID = " & CStr(ItemGroupID) & " "
        End If

        Dim query =
            $"SELECT ITEM_GROUP_ID, b.{displayLevel}, {If(displayLevel = "BLUEPRINT_NAME", "0", $"{displayLevel}_ID")} AS FilterID
            FROM ALL_BLUEPRINTS b
            JOIN INVENTORY_TYPES i ON b.ITEM_ID = i.typeID {GetExtraJoinFilter()}
            JOIN INVENTORY_TYPES i2 ON b.BLUEPRINT_ID = i2.typeID
            {GetOwnedJoin()}
            WHERE MARKET_GROUP IS NOT NULL
            {ItemGroupFilter}
            {GetSizeGroupFilter()}
            {GetItemTypesFilter()}
            {levelFilter}
            {IgnoreFilter}
            {TextFilter}
            {NPCBPOFilter}
            GROUP BY b.{displayLevel}, FilterID"

        Return query

    End Function

    Private Function GetExtraJoinFilter() As String
        Dim ReturnString As String = ""
        ReturnString = GetBlueprintSQLWhereQuery(rbtnBPAmmoChargeBlueprints.Checked, rbtnBPDroneBlueprints.Checked, rbtnBPModuleBlueprints.Checked, rbtnBPShipBlueprints.Checked,
                                        rbtnBPSubsystemBlueprints.Checked, rbtnBPBoosterBlueprints.Checked, rbtnBPComponentBlueprints.Checked, rbtnBPMiscBlueprints.Checked,
                                        rbtnBPDeployableBlueprints.Checked, rbtnBPCelestialsBlueprints.Checked, rbtnBPStructureBlueprints.Checked, rbtnBPStructureRigsBlueprints.Checked,
                                        rbtnBPStructureModuleBlueprints.Checked, rbtnBPReactionBlueprints.Checked, rbtnBPRigBlueprints.Checked)
        If ReturnString <> "" Then
            ReturnString = "AND " & ReturnString
        End If

        Return ReturnString
    End Function

    Private Function GetOwnedJoin() As String
        Dim ownedJoin = ""
        Dim baseJoin = $"JOIN OWNED_BLUEPRINTS o ON b.BLUEPRINT_ID = o.BLUEPRINT_ID "
        Dim ownedFilter = ""
        ' See what ID we use for character bps
        Dim TempID As Long = 0
        If UserApplicationSettings.LoadBPsbyChar Then
            ' Use the ID sent
            TempID = SelectedCharacter.ID
        Else
            TempID = CommonLoadBPsID
        End If

        ownedFilter = $" AND o.OWNED <> 0 AND o.USER_ID = {TempID}"

        If rbtnBPOwnedBlueprints.Checked Then
            ownedJoin = $"{baseJoin} {ownedFilter}"
        ElseIf rbtnBPFavoriteBlueprints.Checked Then
            ownedJoin = $"{baseJoin} {ownedFilter} AND o.FAVORITE = 1"
        End If

        Return ownedJoin
    End Function

    Private Function GetSizeGroupFilter() As String
        Dim sizeGroupFilter = ""
        Dim sizeLimit = New List(Of String)()

        If chkBPSmall.Checked Then
            sizeLimit.Add("S")
        End If

        If chkBPMedium.Checked Then
            sizeLimit.Add("M")
        End If

        If chkBPLarge.Checked Then
            sizeLimit.Add("L")
        End If

        If chkBPXLarge.Checked Then
            sizeLimit.Add("XL")
        End If

        If sizeLimit.Count > 0 Then
            Dim sizeGroupString = sizeLimit.Select(Function(x) $"'{x}'").Aggregate(Function(prev, this) $"{prev}, {this}")
            sizeGroupFilter = $"AND b.SIZE_GROUP IN ({sizeGroupString})"
        End If
        Return sizeGroupFilter
    End Function

    Private Function GetItemTypesFilter() As String

        Dim itemTypes = New List(Of ItemType)
        Dim itemTypesFilter = "''"

        If chkBPTech1.Checked Then
            itemTypes.Add(ItemType.Tech1)
        End If

        If chkBPTech2.Checked Then
            itemTypes.Add(ItemType.Tech2)
        End If

        If chkBPTech3.Checked Then
            itemTypes.Add(ItemType.Tech3)
        End If

        If chkBPStory.Checked Then
            itemTypes.Add(ItemType.Storyline)
        End If

        If chkBPPirate.Checked Then
            itemTypes.Add(ItemType.Pirate)
        End If

        If chkBPNavy.Checked Then
            itemTypes.Add(ItemType.Navy)
        End If

        If itemTypes.Count > 0 Then

            Dim itemTypesString = itemTypes.Select(Function(it) CInt(it).ToString).Aggregate(Function(prev, this) $"{prev}, {this}")
            itemTypesFilter = $"AND b.ITEM_TYPE IN ({itemTypesString})"
        End If

        Return itemTypesFilter
    End Function

    ' Changed to use after select to allow single click loading
    'Private Sub treBlueprintTreeView_DoubleClick(sender As Object, e As EventArgs) Handles treBlueprintTreeView.DoubleClick
    '    'If (treBlueprintTreeView.SelectedNode Is Nothing) Then
    '    '    Return
    '    'End If
    '    '' Only load if the final node (bp) in the tree
    '    'If treBlueprintTreeView.SelectedNode.Nodes.Count = 0 Then
    '    '    RaiseEvent BPSelected(treBlueprintTreeView.SelectedNode.Text)
    '    'End If
    'End Sub

    ' After select allows loading the bp selected after the event fires
    Private Sub treBlueprintTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treBlueprintTreeView.AfterSelect
        If (treBlueprintTreeView.SelectedNode Is Nothing) Then
            Return
        End If
        ' Only load if the final node (bp) in the tree
        If treBlueprintTreeView.SelectedNode.Nodes.Count = 0 Then
            RaiseEvent BPSelected(treBlueprintTreeView.SelectedNode.Text)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ResetSelectors(ByVal T1 As Boolean, ByVal T2 As Boolean, ByVal T3 As Boolean, ByVal Storyline As Boolean, ByVal NavyFaction As Boolean, ByVal PirateFaction As Boolean)
        If Not FirstFormLoad Then
            chkBPTech1.Enabled = T1
            chkBPTech2.Enabled = T2
            chkBPTech3.Enabled = T3
            chkBPNavy.Enabled = NavyFaction
            chkBPPirate.Enabled = PirateFaction
            chkBPStory.Enabled = Storyline

            ' Make sure we have something checked
            Call EnsureBPTechCheck()
            ' Load the New data
            Call SetTopNodes()
        End If
    End Sub

    ' Makes sure we have a tech checked for blueprints
    Private Sub EnsureBPTechCheck()
        If chkBPTech1.Enabled And chkBPTech1.Checked Then
            Exit Sub
        ElseIf chkBPTech2.Enabled And chkBPTech2.Checked Then
            Exit Sub
        ElseIf chkBPTech3.Enabled And chkBPTech3.Checked Then
            Exit Sub
        ElseIf chkBPNavy.Enabled And chkBPNavy.Checked Then
            Exit Sub
        ElseIf chkBPPirate.Enabled And chkBPPirate.Checked Then
            Exit Sub
        ElseIf chkBPStory.Enabled And chkBPStory.Checked Then
            Exit Sub
        End If

        ' If here, then none are checked that are enabled, find the first one enabled and check it
        If chkBPTech1.Enabled Then
            chkBPTech1.Checked = True
            Exit Sub
        ElseIf chkBPTech2.Enabled Then
            chkBPTech2.Checked = True
            Exit Sub
        ElseIf chkBPTech3.Enabled Then
            chkBPTech3.Checked = True
            Exit Sub
        ElseIf chkBPNavy.Enabled Then
            chkBPNavy.Checked = True
            Exit Sub
        ElseIf chkBPPirate.Enabled Then
            chkBPPirate.Checked = True
            Exit Sub
        ElseIf chkBPStory.Enabled Then
            chkBPStory.Checked = True
            Exit Sub
        End If

    End Sub

#Region "Click Events"

    Private Sub rbtnAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPAll.CheckedChanged
        Call ResetSelectors(True, True, True, True, True, True)
    End Sub

    Private Sub rbBPOwned_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPOwnedBlueprints.CheckedChanged
        Call ResetSelectors(True, True, True, True, True, True)
    End Sub

    Private Sub chkBPIncludeIgnoredBPs_CheckedChanged(sender As System.Object, e As System.EventArgs)
        Call ResetSelectors(True, True, True, True, True, True)
    End Sub

    Private Sub rbtnShipBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPShipBlueprints.CheckedChanged
        Call ResetSelectors(True, True, True, False, True, True)
    End Sub

    Private Sub rbtnModuleBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPModuleBlueprints.CheckedChanged
        Call ResetSelectors(True, True, False, True, True, False)
    End Sub

    Private Sub rbtnDroneBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPDroneBlueprints.CheckedChanged
        Call ResetSelectors(True, True, False, False, False, True)
    End Sub

    Private Sub rbtnComponentBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPComponentBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, False)
    End Sub

    Private Sub rbtnSubsystemBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPSubsystemBlueprints.CheckedChanged
        Call ResetSelectors(False, False, True, False, False, False)
    End Sub

    Private Sub rbtnToolBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPMiscBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, False)
    End Sub

    Private Sub rbtnAmmoChargeBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPAmmoChargeBlueprints.CheckedChanged
        Call ResetSelectors(True, True, False, False, False, False)
    End Sub

    Private Sub rbtnRigBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPRigBlueprints.CheckedChanged
        Call ResetSelectors(True, True, False, False, False, False)
    End Sub

    Private Sub rbtnStructureBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPStructureBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, True)
    End Sub

    Private Sub rbtnBoosterBlueprints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBPBoosterBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, False)
    End Sub

    Private Sub rbtnBPDeployableBlueprints_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBPDeployableBlueprints.CheckedChanged
        Call ResetSelectors(True, True, False, False, False, False)
    End Sub

    Private Sub rbtnBPStationPartsBlueprints_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBPStructureRigsBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, False)
    End Sub

    Private Sub rbtnBPStationModulesBlueprints_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnBPStructureModuleBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, False)
    End Sub

    Private Sub rbtnBPCelestialBlueprints_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBPCelestialsBlueprints.CheckedChanged
        Call ResetSelectors(True, False, False, False, False, False)
    End Sub

    Private Sub rbtnBPFavoriteBlueprints_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBPFavoriteBlueprints.CheckedChanged
        Call ResetSelectors(True, True, True, True, True, True)
    End Sub

    Private Sub chkbpTech1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPTech1.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkbpTech2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPTech2.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkbpTech3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPTech3.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPNavy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPNavy.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPPirate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPPirate.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPStory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBPStory.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPSmall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPSmall.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPMedium_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPMedium.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPLarge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPLarge.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub chkBPXLarge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBPXLarge.CheckedChanged
        If Not FirstFormLoad Then
            Call SetTopNodes()
        End If
    End Sub

    Private Sub btnClearItemFilter_Click(sender As Object, e As EventArgs) Handles btnClearItemFilter.Click
        txtBPItemFilter.Text = ""
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call SetTopNodes()
    End Sub

    Private Sub chkBPNPCBPOs_CheckedChanged(sender As Object, e As EventArgs) Handles chkBPNPCBPOs.CheckedChanged
        Call SetTopNodes()
    End Sub

#End Region

End Class


''' <summary>
''' Item Type Definitions - These are set by Cwittofur based on existing data
''' </summary>
Enum ItemType
    Tech1 = 1
    Tech2 = 2
    Tech3 = 14
    Storyline = 3
    Pirate = 15
    Navy = 16
End Enum
