Imports System.Data.SQLite

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


    Private Sub frmBlueprintList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblIntro.Text = "Expand the tree to locate a Blueprint." + Environment.NewLine + "Double-Click on it to load it into the main window." + Environment.NewLine + "This window will remain open unless you click Close."

        SetTopNodes()

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
            com.CommandText = BuildBPQuery(displayLevel, filterLevel.FilterField, filterLevel.FilterValue)

            con.Open()
            Using reader = com.ExecuteReader()
                While reader.Read
                    Dim newNode = New TreeNode(reader(displayLevel).ToString)
                    newNode.Tag = New NodeTag(displayLevel, CInt(reader("FilterID")))
                    thisNode.Nodes.Add(newNode)
                    If displayLevel <> "BLUEPRINT_NAME" Then
                        newNode.Nodes.Add(New TreeNode) 'dummy node to show the + mark
                    End If
                End While
            End Using
        End Using
    End Sub

    Private Function BuildBPQuery(displayLevel As String, filterColumnName As String, filterColumnValue As Integer?) As String

        Dim levelFilter = ""
        If filterColumnName <> "" And filterColumnValue.HasValue Then
            levelFilter = $"And {filterColumnName}_ID = {filterColumnValue}"
        End If


        Dim query =
$"SELECT b.{displayLevel}, {If(displayLevel = "BLUEPRINT_NAME", "0", $"{displayLevel}_ID")} AS FilterID
FROM ALL_BLUEPRINTS b
JOIN INVENTORY_TYPES i ON b.ITEM_ID = i.typeID {GetExtraJoinFilter()}
{GetOwnedJoin()}
WHERE MARKET_GROUP IS NOT NULL
{GetSizeGroupFilter()}
{GetItemTypesFilter()}
{levelFilter}
GROUP BY b.{displayLevel}, FilterID
ORDER BY b.{displayLevel}
"

        Return query
    End Function

    Private Function GetExtraJoinFilter() As String
        If rbtnAmmoChargeBlueprints.Checked Then
            Return "And ITEM_CATEGORY = 'Charge'"
        ElseIf rbtnBPDroneBlueprints.Checked Then
            Return "AND ITEM_CATEGORY IN ('Drone', 'Fighter')"
        ElseIf rbtnBPModuleBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Module' AND ITEM_GROUP NOT LIKE 'Rig%'"
        ElseIf rbtnBPShipBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Ship'"
        ElseIf rbtnBPSubsystemBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Subsystem'"
        ElseIf rbtnBPBoosterBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Implant'"
        ElseIf rbtnBPComponentBlueprints.Checked Then
            Return "AND ITEM_GROUP LIKE '%Components%' AND ITEM_GROUP <> 'Station Components'"
        ElseIf rbtnBPMiscBlueprints.Checked Then
            Return "AND ITEM_GROUP IN ('Tool', 'Data Interfaces', 'Cyberimplant', 'Fuel Block')"
        ElseIf rbtnBPDeployableBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Deployable'"
        ElseIf rbtnBPCelestialsBlueprints.Checked Then
            Return "AND ITEM_CATEGORY IN ('Celestial', 'Orbitals', 'Sovereignty Structures', 'Station', 'Accessories', 'Infrastructure Upgrades')"
        ElseIf rbtnBPStructureBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Starbase'"
        ElseIf rbtnBPStationPartsBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Station Components'"
        ElseIf rbtnBPRigBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Rig Blueprint'"
        Else
            Return ""
        End If
    End Function

    Private Function GetOwnedJoin() As String
        Dim ownedJoin = ""
        Dim baseJoin = $"JOIN OWNED_BLUEPRINTS o ON b.BLUEPRINT_ID = o.BLUEPRINT_ID "
        Dim ownedFilter = $" AND o.OWNED <> 0 AND o.USER_ID = {SelectedCharacter.ID}"

        If rbtnBPOwnedBlueprints.Checked Then
            ownedJoin = $"{baseJoin} {ownedFilter}"
        ElseIf rbtnBPFavoriteBlueprints.Checked Then
            ownedJoin = $"{baseJoin} {ownedFilter} FAVORITE = 1"
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

    Private Sub treBlueprintTreeView_DoubleClick(sender As Object, e As EventArgs) Handles treBlueprintTreeView.DoubleClick
        If (treBlueprintTreeView.SelectedNode Is Nothing) Then
            Return
        End If
        RaiseEvent BPSelected(treBlueprintTreeView.SelectedNode.Text)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        SetTopNodes()
    End Sub

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
