﻿Imports System.Data.SQLite

Public Class frmBlueprintList
    Public Event BPSelected(bpName As String)
    Private Sub frmBlueprintList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblIntro.Text = "Expand the tree to locate a Blueprint." + Environment.NewLine + "Double-Click on it to load it into the main window." + Environment.NewLine + "This window will remain open unless you click Close."

        PopulateBPTree()

    End Sub

    Private Sub PopulateBPTree()
        Dim readerBPs As SQLiteDataReader
        Dim itemCategoryNode As TreeNode
        Dim itemGroupNode As TreeNode
        Dim marketGroupNode As TreeNode
        Dim techLevel As TreeNode

        DBCommand = New SQLiteCommand(BuildBPQuery(), EVEDB.DBREf)
        readerBPs = DBCommand.ExecuteReader
        'treBlueprintTreeView.Nodes.Clear()

        While readerBPs.Read
            If treBlueprintTreeView.Nodes.Find(readerBPs.GetString(0), True).Count = 0 Then
                itemCategoryNode = treBlueprintTreeView.Nodes.Add(readerBPs.GetString(0), readerBPs.GetString(0))
            Else
                itemCategoryNode = treBlueprintTreeView.Nodes.Find(readerBPs.GetString(0), True)(0)
            End If

            If itemCategoryNode.Nodes.Find(readerBPs.GetString(1), True).Count = 0 Then
                itemGroupNode = itemCategoryNode.Nodes.Add(readerBPs.GetString(1), readerBPs.GetString(1))
            Else
                itemGroupNode = itemCategoryNode.Nodes.Find(readerBPs.GetString(1), True)(0)
            End If

            If itemGroupNode.Nodes.Find(readerBPs.GetString(2), True).Count = 0 Then
                marketGroupNode = itemGroupNode.Nodes.Add(readerBPs.GetString(2), readerBPs.GetString(2))
            Else
                marketGroupNode = itemGroupNode.Nodes.Find(readerBPs.GetString(2), True)(0)
            End If

            marketGroupNode.Nodes.Add(readerBPs.GetString(3))
            'Application.DoEvents()
            'AddNode(readerBPs.GetString(0), readerBPs.GetString(1), readerBPs.GetString(2), readerBPs.GetString(3))
        End While

        readerBPs.Close()
    End Sub

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
    Private Function BuildBPQuery() As String
        Dim sql = "SELECT b.ITEM_CATEGORY, b.ITEM_GROUP, b.MARKET_GROUP, b.BLUEPRINT_NAME , b.TECH_LEVEL " +
                  "FROM ALL_BLUEPRINTS b " +
                  "JOIN INVENTORY_TYPES  i ON b.ITEM_ID = i.typeID " +
                  "{0} " +
                  "WHERE MARKET_GROUP NOTNULL " +
                  "{1} " +
                  "AND b.ITEM_TYPE IN ({2}) " +
                  "{3} " +
                  "ORDER BY ITEM_CATEGORY, ITEM_GROUP, MARKET_GROUP "

        Dim extraSql = ""
        Dim extraWhere = ""
        Dim sizeSql = ""
        Dim itemTypes = New List(Of ItemType)()
        Dim sizeLimit = New List(Of String)()

        If rbtnAmmoChargeBlueprints.Checked Then
            extraSql = "And ITEM_CATEGORY = 'Charge'"
        ElseIf rbtnBPDroneBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Drone'"
        ElseIf rbtnBPModuleBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Module' AND ITEM_GROUP NOT LIKE 'Rig%'"
        ElseIf rbtnBPShipBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Ship'"
        ElseIf rbtnBPSubsystemBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Subsystem'"
        ElseIf rbtnBPBoosterBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Implant'"
        ElseIf rbtnBPComponentBlueprints.Checked Then
            extraSql = "AND ITEM_GROUP LIKE '%Components%' AND ITEM_GROUP <> 'Station Components'"
        ElseIf rbtnBPMiscBlueprints.Checked Then
            extraSql = "AND ITEM_GROUP IN ('Tool', 'Data Interfaces', 'Cyberimplant', 'Fuel Block')"
        ElseIf rbtnBPDeployableBlueprints.Checked Then
            extraSQL = "AND ITEM_CATEGORY = 'Deployable'"
        ElseIf rbtnBPCelestialsBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY IN ('Celestial', 'Orbitals', 'Sovereignty Structures', 'Station', 'Accessories', 'Infrastructure Upgrades')"
        ElseIf rbtnBPStructureBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Starbase'"
        ElseIf rbtnBPStationPartsBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Station Components'"
        ElseIf rbtnBPRigBlueprints.Checked Then
            extraSql = "AND ITEM_CATEGORY = 'Rig Blueprint'"
        ElseIf rbtnBPOwnedBlueprints.Checked Then
            extraSql = "LEFT JOIN OWNED_BLUEPRINTS o ON b.BLUEPRINT_ID = o.BLUEPRINT_ID"
            extraWhere = "AND o.OWNED <> 0 AND o.USER_ID = " & SelectedCharacter.ID
        ElseIf rbtnBPFavoriteBlueprints.Checked Then
            extraSql = "LEFT JOIN OWNED_BLUEPRINTS o ON b.BLUEPRINT_ID = o.BLUEPRINT_ID"
            extraWhere = "AND o.OWNED <> 0 AND FAVORITE = 1 AND o.USER_ID = " & SelectedCharacter.ID
        End If

        ' Item Type Definitions - These are set by me based on existing data
        ' 1, 2, 14 are T1, T2, T3
        ' 3 is Storyline
        ' 15 is Pirate Faction
        ' 16 is Navy Faction

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

        If (sizeLimit.Count > 0) Then
            sizeSQL = String.Format("AND b.SIZE_GROUP IN ({0})", String.Join(",", sizeLimit.Select(Function(x) String.Format("'{0}'", x)).ToArray()))
        End If

        Dim returnSql = String.Format(sql, extraSql, extraWhere, String.Join(",", itemTypes.Cast(Of Integer).ToArray()), sizeSql)
        Return returnSql
    End Function
    Private Sub treBlueprintTreeView_DoubleClick(sender As Object, e As EventArgs) Handles treBlueprintTreeView.DoubleClick
        RaiseEvent BPSelected(treBlueprintTreeView.SelectedNode.Text)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        treBlueprintTreeView.Nodes.Clear()
        PopulateBPTree()
    End Sub
End Class

Enum ItemType
    Tech1 = 1
    Tech2 = 2
    Tech3 = 14
    Storyline = 3
    Pirate = 15
    Navy = 16
End Enum
