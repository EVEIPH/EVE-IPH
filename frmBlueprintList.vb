Imports System.Data.SQLite
Imports System.Diagnostics.Eventing.Reader

Public Class frmBlueprintList
    Public Event BPSelected(bpName As String)
    Private Sub frmBlueprintList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim readerBPs As SQLiteDataReader
        Dim itemCategoryNode as TreeNode
        Dim itemGroupNode as TreeNode
        Dim marketGroupNode as TreeNode
        Dim techLevel as TreeNode

        Dim sql = "SELECT b.ITEM_CATEGORY, b.ITEM_GROUP, b.MARKET_GROUP, b.BLUEPRINT_NAME , b.TECH_LEVEL " +
                  "FROM ALL_BLUEPRINTS b " +
                  "JOIN INVENTORY_TYPES  i ON b.ITEM_ID = i.typeID " +
                  "WHERE MARKET_GROUP NOTNULL ORDER BY ITEM_CATEGORY, ITEM_GROUP, MARKET_GROUP "

        lblIntro.Text = "Expand the tree to locate a Blueprint." + Environment.NewLine + "Double-Click on it to load it into the main window." + Environment.NewLine + "This window will remain open unless you click Close."

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBPs = DBCommand.ExecuteReader
        'treBlueprintTreeView.Nodes.Clear()

        While readerBPs.Read
            'if treBlueprintTreeView.Nodes.Find(readerBPs.GetString(0), True).Count = 0 Then
            '    itemCategoryNode = treBlueprintTreeView.Nodes.Add(readerBPs.GetString(0), readerBPs.GetString(0))
            'Else 
                itemCategoryNode = treBlueprintTreeView.Nodes.Find(readerBPs.GetString(0), True)(0)
            'End If
        
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
        Dim itemCategoryNode as TreeNode
        Dim itemGroupNode as TreeNode
        Dim marketGroupNode as TreeNode

        if treBlueprintTreeView.Nodes.Find(itemCategory, True).Count = 0 Then
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

    Private Sub treBlueprintTreeView_DoubleClick(sender As Object, e As EventArgs) Handles treBlueprintTreeView.DoubleClick
        RaiseEvent BPSelected(treBlueprintTreeView.SelectedNode.Text)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close
    End Sub
End Class