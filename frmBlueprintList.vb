Imports System.Data.SQLite

Public Class frmBlueprintList
    Public Event BPSelected(bpName As String)
    Private Sub frmBlueprintList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblIntro.Text = "Expand the tree to locate a Blueprint." + Environment.NewLine + "Double-Click on it to load it into the main window." + Environment.NewLine + "This window will remain open unless you click Close."

        SetTopNodes()

    End Sub

    ' I'd like to try and find some way of merging PopulateNode and SetTopNodes, but I don't think there's a simple way
    Private Sub SetTopNodes()
        treBlueprintTreeView.Nodes.Clear()
        Using con = New SQLiteConnection(EVEDB.DBREf.ConnectionString)
            Dim com = con.CreateCommand()
            com.CommandText = BuildBPQuery("ITEM_CATEGORY", "", "")

            con.Open()
            Using reader = com.ExecuteReader()
                While reader.Read
                    Dim readCategory = reader("ITEM_CATEGORY").ToString
                    Dim newNode = New TreeNode(readCategory)
                    newNode.Tag = "ITEM_CATEGORY"
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
        Dim filterLevel As String = CStr(thisNode.Tag)
        Dim displayLevel = GetDisplayLevel(filterLevel)

        Using con = New SQLiteConnection(EVEDB.DBREf.ConnectionString)
            Dim com = con.CreateCommand()
            com.CommandText = BuildBPQuery(displayLevel, filterLevel, thisNode.Text)

            con.Open()
            Using reader = com.ExecuteReader()
                While reader.Read
                    Dim newNode = New TreeNode(reader(displayLevel).ToString)
                    newNode.Tag = displayLevel
                    thisNode.Nodes.Add(newNode)
                    If displayLevel <> "BLUEPRINT_NAME" Then
                        newNode.Nodes.Add(New TreeNode) 'dummy node to show the + mark
                    End If
                End While
            End Using
        End Using
    End Sub

    'Private Sub PopulateBPTree()
    '    Dim readerBPs As SQLiteDataReader
    '    Dim itemCategoryNode As TreeNode
    '    Dim itemGroupNode As TreeNode
    '    Dim marketGroupNode As TreeNode

    '    DBCommand = New SQLiteCommand(BuildBPQuery(), EVEDB.DBREf)
    '    readerBPs = DBCommand.ExecuteReader
    '    'treBlueprintTreeView.Nodes.Clear()

    '    While readerBPs.Read
    '        If treBlueprintTreeView.Nodes.Find(readerBPs.GetString(0), True).Count = 0 Then
    '            itemCategoryNode = treBlueprintTreeView.Nodes.Add(readerBPs.GetString(0), readerBPs.GetString(0))
    '        Else
    '            itemCategoryNode = treBlueprintTreeView.Nodes.Find(readerBPs.GetString(0), True)(0)
    '        End If

    '        If itemCategoryNode.Nodes.Find(readerBPs.GetString(1), True).Count = 0 Then
    '            itemGroupNode = itemCategoryNode.Nodes.Add(readerBPs.GetString(1), readerBPs.GetString(1))
    '        Else
    '            itemGroupNode = itemCategoryNode.Nodes.Find(readerBPs.GetString(1), True)(0)
    '        End If

    '        If itemGroupNode.Nodes.Find(readerBPs.GetString(2), True).Count = 0 Then
    '            marketGroupNode = itemGroupNode.Nodes.Add(readerBPs.GetString(2), readerBPs.GetString(2))
    '        Else
    '            marketGroupNode = itemGroupNode.Nodes.Find(readerBPs.GetString(2), True)(0)
    '        End If

    '        marketGroupNode.Nodes.Add(readerBPs.GetString(3))
    '        'Application.DoEvents()
    '        'AddNode(readerBPs.GetString(0), readerBPs.GetString(1), readerBPs.GetString(2), readerBPs.GetString(3))
    '    End While

    '    readerBPs.Close()
    'End Sub

    Private Sub AddNode(itemCategory As String, itemGroup As String, marketGroup As String, bpName As String)
        Dim itemCategoryNode As TreeNode
        Dim itemGroupNode As TreeNode
        Dim marketGroupNode As TreeNode

        If treBlueprintTreeView.Nodes.Find(itemCategory, True).Count = 0 Then
            itemCategoryNode = treBlueprintTreeView.Nodes.Add(itemCategory, itemCategory)
        Else
            itemCategoryNode = treBlueprintTreeView.Nodes.Find(itemCategory, True)(0)
        End If

        If itemCategoryNode.Nodes.Find(itemGroup, True).Count = 0 Then
            itemGroupNode = itemCategoryNode.Nodes.Add(itemGroup, itemGroup)
        Else
            itemGroupNode = itemCategoryNode.Nodes.Find(itemGroup, True)(0)
        End If

        If itemGroupNode.Nodes.Find(marketGroup, True).Count = 0 Then
            marketGroupNode = itemGroupNode.Nodes.Add(marketGroup, marketGroup)
        Else
            marketGroupNode = itemGroupNode.Nodes.Find(marketGroup, True)(0)
        End If

        marketGroupNode.Nodes.Add(bpName)

    End Sub

    Private Function BuildBPQuery(displayLevel As String, filterColumnName As String, filterColumnValue As String) As String

        Dim levelFilter = ""
        If filterColumnName <> "" Then
            levelFilter = $"And {filterColumnName} = '{filterColumnValue}'"
        End If

        Dim query =
$"SELECT DISTINCT b.{displayLevel}
FROM ALL_BLUEPRINTS b
JOIN INVENTORY_TYPES i ON b.ITEM_ID = i.typeID {GetExtraJoinFilter()}
{GetOwnedJoin()}
WHERE MARKET_GROUP IS NOT NULL
{GetSizeGroupFilter()}
{GetItemTypesFilter()}
{levelFilter}
ORDER BY {displayLevel}
"

        Return query
    End Function

    Private Function GetExtraJoinFilter() As String
        If rbtnAmmoChargeBlueprints.Checked Then
            Return "And ITEM_CATEGORY = 'Charge'"
        ElseIf rbtnBPDroneBlueprints.Checked Then
            Return "AND ITEM_CATEGORY = 'Drone'"
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
        Dim baseJoin = $"LEFT JOIN OWNED_BLUEPRINTS o ON b.BLUEPRINT_ID = o.BLUEPRINT_ID "
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
