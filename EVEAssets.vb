
Imports System.Data.SQLite

Public Class EVEAssets

    Private AssetList As List(Of EVEAsset)
    Private SelectedAssetList As List(Of EVEAsset)
    Private AssetType As ScanType
    Private CacheDate As Date
    Private ItemIDToFind As Long
    Private ChildNode As TreeEntry
    Protected LocationToFind As LocationInfo

    Private LocationNames As List(Of LocationName)
    Private Const UnknownLocation As String = "Unknown Location"
    Private UnknownLocationCounter As Integer
    Private LocationIDToFind As Long
    Private Const QuantitySpacer As String = " - "

    Private ParentNodeID As Long

    Public Class LocationName
        Public ID As Long
        Public Name As String
    End Class

    Public Sub New(Optional ByVal InitalAssetType As ScanType = ScanType.Personal)

        AssetType = InitalAssetType
        AssetList = New List(Of EVEAsset)
        CacheDate = NoDate
    End Sub

    Public Sub LoadAssets(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData, ByVal RefreshAssets As Boolean)
        Dim SQL As String
        Dim readerAssets As SQLiteDataReader
        Dim readerData As SQLiteDataReader
        Dim Asset As New EVEAsset
        Dim Assets As New List(Of EVEAsset)
        Dim FoundLocationID As Long = 0

        ' Update asset data first
        Call UpdateAssets(ID, CharacterTokenData, AssetType, RefreshAssets)

        ' Set the cache date, since we may not update the assets
        If AssetType = ScanType.Personal Then
            SQL = "SELECT ASSETS_CACHE_DATE FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = " & CStr(ID)
        Else
            SQL = "SELECT ASSETS_CACHE_DATE FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & CStr(ID)
        End If
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerData = DBCommand.ExecuteReader

        If readerData.Read Then
            If IsDBNull(readerData.GetValue(0)) Then
                CacheDate = NoDate
            Else
                CacheDate = CDate(readerData.GetString(0))
            End If
        Else
            CacheDate = NoDate
        End If

        LocationNames = New List(Of LocationName)
        UnknownLocationCounter = 0

        ' Load the assets - corp or personal
        SQL = "SELECT ID, ItemID, LocationID, TypeID, Assets.Quantity, Flag, IsSingleton, "
        SQL &= "CASE WHEN BP_TYPE Is NULL THEN 0 ELSE BP_TYPE END AS BPType, ItemName FROM ASSETS "
        SQL &= "LEFT JOIN ALL_OWNED_BLUEPRINTS ON ID = OWNER_ID And ItemID = ITEM_ID "
        SQL &= "WHERE ID = " & ID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerAssets = DBCommand.ExecuteReader

        While readerAssets.Read
            Asset = New EVEAsset
            Asset.ItemID = readerAssets.GetInt64(1)
            Asset.LocationID = readerAssets.GetInt64(2)
            Asset.TypeID = readerAssets.GetInt64(3)
            Asset.Quantity = readerAssets.GetInt64(4)
            Asset.FlagID = readerAssets.GetInt32(5)
            Asset.IsSingleton = CBool(readerAssets.GetInt32(6))
            Asset.BPType = CType(readerAssets.GetInt32(7), BPType)

            ' If there is nothing in ItemName, then just set to empty string
            If IsDBNull(readerAssets.GetValue(8)) Then
                Asset.ItemName = ""
            Else
                Asset.ItemName = readerAssets.GetString(8)
            End If

            ' Get the location name, update flagID (ref), set flag text (ref), container (ref), Flagsort (ref)
            Asset.LocationName = GetAssetLocationAndFlagInfo(Asset.LocationID, Asset.FlagID, Asset.FlagText, Asset.Container, Asset.FlagSort, CharacterTokenData)

            ' Look up the type name
            SQL = "SELECT typeName, groupName, categoryName FROM ITEM_LOOKUP WHERE typeID = " & Asset.TypeID & " "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerData = DBCommand.ExecuteReader

            Try
                If readerData.Read Then
                    ' Found it
                    If Not IsDBNull(readerData.GetValue(0)) Then
                        Asset.TypeName = readerData.GetString(0)
                    Else
                        Asset.TypeName = "Unknown Item"
                    End If

                    If Not IsDBNull(readerData.GetValue(1)) Then
                        Asset.TypeGroup = readerData.GetString(1)
                    Else
                        Asset.TypeGroup = "Unknown Group"
                    End If

                    If Not IsDBNull(readerData.GetValue(2)) Then
                        Asset.TypeCategory = readerData.GetString(2)
                    Else
                        Asset.TypeCategory = "Unknown Category"
                    End If
                Else
                    Asset.TypeName = "Unknown Item"
                    Asset.TypeGroup = "Unknown Group"
                    Asset.TypeCategory = "Unknown Category"
                End If
            Catch ex As Exception

                Asset.TypeName = "Unknown Item"
                Asset.TypeGroup = "Unknown Group"
                Asset.TypeCategory = "Unknown Category"

            End Try

            ' Add the location name to the item name if it's set
            If Asset.ItemName <> "" Then
                Asset.TypeName = Asset.ItemName & " (" & Asset.TypeName & ")"
            End If

            readerData.Close()

            ' Insert asset
            Assets.Add(Asset)

        End While

        readerAssets.Close()

        AssetList = Assets

    End Sub

    Public Sub UpdateAssets(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData,
                            ByVal AssetType As ScanType, ByVal UpdateAssets As Boolean)
        Dim Assets As New List(Of ESIAsset)
        Dim AssetIDs As New List(Of Double) ' For getting names
        Dim SQL As String = ""

        Dim ESIData As New ESI
        Dim CB As New CacheBox

        Dim CDType As CacheDateType

        If AssetType = ScanType.Personal Then
            CDType = CacheDateType.PersonalAssets
        Else
            CDType = CacheDateType.CorporateAssets
        End If

        ' Look up the assets cache date first      
        If CB.DataUpdateable(CDType, ID) Then
            Assets = ESIData.GetAssets(ID, CharacterTokenData, AssetType, CacheDate)

            ' Insert the records into the DB
            If Not IsNothing(Assets) Then
                If Assets.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Clear the current assets in the database
                    SQL = "DELETE FROM ASSETS WHERE ID = " & CStr(ID)
                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    Dim FlagID As Integer = 0
                    Dim rsLookup As SQLiteDataReader

                    For i = 0 To Assets.Count - 1
                        ' Get the flagID for this location
                        DBCommand = New SQLiteCommand("SELECT flagID FROM INVENTORY_FLAGS WHERE flagName = '" & Assets(i).location_flag & "'", EVEDB.DBREf)
                        rsLookup = DBCommand.ExecuteReader
                        If rsLookup.Read Then
                            FlagID = rsLookup.GetInt32(0)
                        Else
                            FlagID = 0
                        End If
                        rsLookup.Close()

                        ' Get the category of the item - if it's a ship and flag = 4, then it's in a ships hangar, so switch to 90 (Ships Hangar)
                        DBCommand = New SQLiteCommand("SELECT categoryID FROM ITEM_LOOKUP WHERE typeID = " & Assets(i).type_id, EVEDB.DBREf)
                        rsLookup = DBCommand.ExecuteReader
                        If rsLookup.Read Then
                            If rsLookup.GetInt32(0) = 6 And Assets(i).location_flag = "Hangar" Then
                                FlagID = 90
                            End If
                        End If
                        rsLookup.Close()

                        ' Insert it
                        If Assets(i).location_id <> ID Then ' Don't add assets that are on the character
                            SQL = "INSERT INTO ASSETS (ID, ItemID, LocationID, TypeID, Quantity, Flag, IsSingleton) VALUES "
                            SQL &= "(" & CStr(ID) & "," & CStr(Assets(i).item_id) & "," & CStr(Assets(i).location_id) & ","
                            SQL &= CStr(Assets(i).type_id) & "," & CStr(Assets(i).quantity) & "," & CStr(FlagID) & ","
                            SQL &= CInt(Assets(i).is_singleton) & ")"

                            Call EVEDB.ExecuteNonQuerySQL(SQL)

                        End If

                        ' Save the ID for looking up the names
                        Call AssetIDs.Add(Assets(i).item_id)

                    Next

                    ' Update all the asset flags to negative values if they are base nodes - meaning they are not contained in any other node/asset value
                    SQL = String.Format("UPDATE ASSETS SET Flag = CASE WHEN Flag > 0 THEN (Flag * -1) ELSE Flag END WHERE ID = {0} AND LocationID NOT IN (SELECT ItemID FROM ASSETS WHERE ID = {0})", ID)
                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    ' For personal assets, look up any Base Asset for the location ID where it's an ItemID and search if it's in space or not
                    If AssetType = ScanType.Personal Then



                    End If

                    ' Special processing for corporation data
                    If AssetType = ScanType.Corporation Then

                        ' Get the corp division names first
                        If CB.DataUpdateable(CacheDateType.CorporateDivisions, ID) Then
                            Dim DivisionNames As ESICorporationDivisions = ESIData.GetDivisionNames(ID, CharacterTokenData, CacheDate, False)

                            ' Clear data first then add to the table
                            Call EVEDB.ExecuteNonQuerySQL("DELETE FROM ESI_CORPORATION_DIVISIONS")

                            For Each item In DivisionNames.hangar
                                Call EVEDB.ExecuteNonQuerySQL("INSERT INTO ESI_CORPORATION_DIVISIONS VALUES (" & CStr(ID) & ",'HANGAR'," & CStr(item.division) & ",'" & FormatDBString(item.name) & "')")
                            Next
                            For Each item In DivisionNames.wallet
                                Call EVEDB.ExecuteNonQuerySQL("INSERT INTO ESI_CORPORATION_DIVISIONS VALUES (" & CStr(ID) & ",'WALLET'," & CStr(item.division) & ",'" & FormatDBString(item.name) & "')")
                            Next
                            ' Update cache date too
                            Call CB.UpdateCacheDate(CacheDateType.CorporateDivisions, CacheDate, ID)
                        End If

                        ' If we are looking up corporation asset names, we need to limit it to just those that will come up in the call
                        ' since it's not the same as personal asset where you can query all IDs
                        ' Only send itemids that singleton is true, the locationid is an item (i.e., container not station), and the item is in the list of location ids
                        AssetIDs = New List(Of Double)
                        SQL = "SELECT ItemID FROM ASSETS WHERE ID = {0} "
                        SQL &= "AND ItemID IN (SELECT LocationID FROM ASSETS WHERE ID = {0}) "
                        SQL &= "AND LocationID > 90000000 AND IsSingleton= -1"
                        DBCommand = New SQLiteCommand(String.Format(SQL, CStr(ID)), EVEDB.DBREf)
                        rsLookup = DBCommand.ExecuteReader
                        While rsLookup.Read
                            AssetIDs.Add(rsLookup.GetDouble(0))
                        End While
                        rsLookup.Close()
                    End If

                    ' Finally, get all the names and update the Assets table - use the same cache date for assets
                    Dim AssetItemNames As New List(Of ESICharacterAssetName)
                    AssetItemNames = ESIData.GetAssetNames(AssetIDs, ID, CharacterTokenData, AssetType, CacheDate)

                    ' Update the names in the asset table for each itemID
                    If Not IsNothing(AssetItemNames) Then
                        For Each item In AssetItemNames
                            If item.name <> None Then
                                Call EVEDB.ExecuteNonQuerySQL("UPDATE ASSETS SET ItemName='" & FormatDBString(item.name) & "' WHERE ItemID=" & CStr(item.item_id))
                            End If
                        Next
                    End If

                    'Update cache date since it's all set now
                    Call CB.UpdateCacheDate(CDType, CacheDate, ID)

                    Call EVEDB.CommitSQLiteTransaction()
                    DBCommand = Nothing
                End If
            End If
        End If

    End Sub

    Public Function GetAssetList() As List(Of EVEAsset)
        Return AssetList
    End Function

    Private Function GetAssetLocationAndFlagInfo(ByVal LocationID As Long, ByRef FlagID As Integer, ByRef FlagText As String,
                                                 ByRef Container As Boolean, ByRef FlagSortNumber As Integer, ByVal CharacterTokenData As SavedTokenData) As String
        Dim SQL As String
        Dim readerData As SQLiteDataReader
        Dim LocationName As String = ""

        ' Look up the location name, first start with stations, then systems
        If LocationID <> 0 Then

            ' See if it's a station
            If LocationID >= MinStationID And LocationID < MaxStationID Then

                SQL = "SELECT STATION_NAME FROM STATIONS WHERE STATION_ID = " & CStr(LocationID)
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerData = DBCommand.ExecuteReader

                If readerData.Read Then
                    ' Found it
                    LocationName = readerData.GetString(0)
                Else
                    ' Unknown
                    LocationName = UnknownLocation
                End If
                readerData.Close()

            ElseIf LocationID >= 30000000 And LocationID < 40000000 Then ' See if it's a solar system
                SQL = "SELECT solarSystemName FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(LocationID)

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerData = DBCommand.ExecuteReader

                If readerData.Read Then
                    ' Found it
                    LocationName = readerData.GetString(0)
                Else
                    ' Unknown
                    LocationName = UnknownLocation
                End If

                ' See if it has a flag assigned, if 0 then set it to 500 (my own code for space)
                If FlagID = 0 Then
                    FlagID = -1 * SpaceFlagCode 'negative for base item
                End If

                readerData.Close()
            Else
                ' See if it's connected to another record, which will have a name look up
                SQL = "SELECT locationID FROM ASSETS WHERE itemID = " & CStr(LocationID)

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerData = DBCommand.ExecuteReader
                readerData.Read()

                If readerData.HasRows Then
                    ' Call this function again to get the location name
                    LocationName = GetAssetLocationAndFlagInfo(readerData.GetInt64(0), FlagID, FlagText, Container, FlagSortNumber, CharacterTokenData)
                    readerData.Close()
                Else
                    ' See if it's a upwell structure that they have access to
                    Dim FoundLocation As LocationName
                    ' See if we looked it up first before downloading
                    LocationIDToFind = LocationID
                    FoundLocation = LocationNames.Find(AddressOf FindLocation)

                    If FoundLocation IsNot Nothing Then
                        LocationName = FoundLocation.Name
                    Else
                        ' Get the location name for the location if we don't have it yet
                        Dim SP As New StructureProcessor
                        Dim LocationData As StructureProcessor.StructureStationInformation = SP.GetStationInformation(LocationID, CharacterTokenData, True)

                        If LocationData.Name <> "" Then
                            LocationName = LocationData.Name
                        End If

                        If LocationName = "" Then
                            UnknownLocationCounter += 1
                            ' Not found, so add a counter to it to deliniate unknown locations
                            LocationName = UnknownLocation & " " & CStr(UnknownLocationCounter)
                        End If

                        ' Insert location into our list
                        Dim CN As New LocationName
                        CN.ID = LocationID
                        CN.Name = LocationName
                        LocationNames.Add(CN)

                    End If
                End If
            End If
        Else
            LocationName = UnknownLocation
        End If

        ' Look up the flag text
        SQL = "SELECT FlagText, container, sort_order FROM INVENTORY_FLAGS WHERE FlagID = " & CStr(Math.Abs(FlagID)) ' FlagID can be negative

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerData = DBCommand.ExecuteReader

        If readerData.Read Then
            ' Found it
            FlagText = CStr(readerData.GetValue(0))
            Container = CBool(readerData.GetValue(1))
            FlagSortNumber = CInt(readerData.GetValue(2))
        Else
            FlagText = "Unknown"
            Container = False
            FlagSortNumber = -1
        End If

        readerData.Close()

        If FlagText = "Space" Or FlagText = "Ship Offline" Then
            LocationName = LocationName & " (In Solar System)"
        End If

        Return LocationName

    End Function

    Private Function SetLocationInfo(ByVal LocationID As Long, ByVal AccountID As Long, ByVal FlagID As Integer) As LocationInfo
        Dim TempLocationInfo As LocationInfo = New LocationInfo

        TempLocationInfo.LocationID = LocationID
        TempLocationInfo.AccountID = AccountID

        If FlagID = -90 Then
            TempLocationInfo.FlagID = -4
        Else

            TempLocationInfo.FlagID = FlagID
        End If

        Return TempLocationInfo

    End Function

    Private Structure TreeEntry
        Dim Node As TreeNode
        Dim FlagID As Integer
        Dim FlagSort As Integer
        Dim Container As Boolean
        Dim DisplayText As String
        Dim Name As String
        Dim Tag As Object
        Dim IsSingleton As Boolean
        Dim Quantity As Long ' only store if IsSingleton is false
    End Structure

    ' Gets the Tree base node for all assets - the list of checked nodes passed *** NEW
    Public Function GetAssetTreeReturnNode(SortOption As SortType, SearchItemList As List(Of Long), NodeName As String, AccountID As Long,
                                           SavedLocations As List(Of LocationInfo), ByRef OnlyBPCs As Boolean) As TreeNode
        Dim Tree As New TreeView
        Dim ReturnNode As TreeNode
        ' For building asset list tree views
        Dim AssetTreeViewNodes As New List(Of TreeEntry)
        Dim TempNode As TreeNode
        Dim TempTreeEntry As TreeEntry
        Dim FoundTreeEntry As TreeEntry
        Dim ContainerLocationID As Long

        Dim TempLocationInfo As LocationInfo
        Dim InContainer As Boolean
        Dim BaseNodeAdded As Boolean
        Dim UnknownStructureAdded As Boolean
        Dim Asset As EVEAsset
        Dim BaseAssets As New List(Of EVEAsset)

        Tree.SuspendLayout()
        Tree.Update()
        Tree.Nodes.Clear()

        ' If no assets, just update and leave
        If AssetList.Count = 0 Then
            ReturnNode = Tree.Nodes.Add(NodeName & " - No Assets Loaded")
            Tree.EndUpdate()
            Return ReturnNode
        End If

        ' Add the base node to the return variable
        ReturnNode = Tree.Nodes.Add(NodeName)
        'TempLocationInfo = SetLocationInfo(AccountID, -1, 0)
        ReturnNode.Name = CStr(AccountID)
        ReturnNode.Tag = -1 ' Base node

        Tree.CheckBoxes = True

        ' Only build this if we are looking for Build mat types
        If SearchItemList.Count <> 0 Then
            ' Build the asset list based only on these mats - Need to take the list of mats we have, then search all
            ' assets to find those item ID's that we want only
            BaseAssets = BuildMaterialAssetList(SearchItemList, OnlyBPCs)
        Else
            ' Get all items
            BaseAssets = AssetList
        End If

        UnknownStructureAdded = False

        ' Loop through each node and add all the items in it
        For Each Asset In BaseAssets
            InContainer = False
            BaseNodeAdded = False
            ' All nodes will have 
            ' - .Text = Display name
            ' - .Name = ItemID
            ' - .Tag = ParentNodeID and use -1 for base node
            ' When storing, it will use the item ID for the item, and location ID for the others

            ' If the node is a base node (flag less than 0), then add this first
            If Asset.FlagID <= 0 Then
                ' Add base node (locationID)
                TempNode = New TreeNode
                TempNode.Name = CStr(Asset.LocationID)
                TempNode.Text = Asset.LocationName
                TempNode.Tag = -1 ' Base node so no parents

                TempLocationInfo = SetLocationInfo(AccountID, Asset.ItemID, Asset.FlagID)
                TempNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)

                ' For ship with just fittings or any flags that we don't have a base node location id for - add unknown structure, and set a common flag 
                ' to set the treenode pair lookup - then use below for adding any flags and item
                If Asset.LocationName = "Unknown Structure" Then
                    UnknownStructureAdded = True
                    TempNode.Name = CStr(Asset.LocationID) ' Make a dummy node with this as main ID to look up later
                End If

                BaseNodeAdded = True

                TempTreeEntry = New TreeEntry
                TempTreeEntry.Node = TempNode
                TempTreeEntry.DisplayText = TempNode.Text
                TempTreeEntry.Name = TempNode.Name
                TempTreeEntry.Tag = TempNode.Tag
                TempTreeEntry.FlagID = Asset.FlagID
                TempTreeEntry.FlagSort = -1
                TempTreeEntry.Container = True
                TempTreeEntry.IsSingleton = True
                TempTreeEntry.Quantity = 0

                ' For base nodes, the lookup will be the locationid
                Call AddAssetTreeNode(AssetTreeViewNodes, TempTreeEntry)
            End If

            ' Add a subnode to the list if the flag indicates it can have things within the location (e.g., bays and holds)

            ' Since I add a ship hanger (for personal assets) or a corp delivery hanger (corp assets) need to set the location id's in the tree
            ' to compensate. So store the negative of the location. Ie, it'll be a station for these so store the negative of the station ID
            If Asset.Container And Asset.LocationName <> "Unknown Structure" Then
                If BaseNodeAdded Then
                    ContainerLocationID = -1 * Asset.LocationID ' Store negative locationID so it is unique for Parent ID to search 
                Else
                    ' For just items in containers, (not item/shiphangar/station) container location is the negative of the id
                    ContainerLocationID = -1 * Asset.ItemID
                End If

                TempNode = New TreeNode
                TempNode.Name = CStr(ContainerLocationID)
                TempNode.Text = Asset.FlagText
                TempNode.Tag = Asset.LocationID ' Parent is base node above so use it's ID or location ID if we didn't add a base, same number

                TempLocationInfo = SetLocationInfo(AccountID, Asset.ItemID, Asset.FlagID)
                TempNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)

                TempTreeEntry = New TreeEntry
                TempTreeEntry.Node = TempNode
                TempTreeEntry.DisplayText = TempNode.Text
                TempTreeEntry.Name = TempNode.Name
                TempTreeEntry.Tag = TempNode.Tag
                TempTreeEntry.FlagID = Asset.FlagID
                TempTreeEntry.FlagSort = Asset.FlagSort
                TempTreeEntry.Container = True
                TempTreeEntry.IsSingleton = True
                TempTreeEntry.Quantity = 0

                ' See if this container is already in the list with this FlagID and Location, and if so, update the tag to match that ItemID
                ChildNode = TempTreeEntry
                FoundTreeEntry = AssetTreeViewNodes.Find(AddressOf FindContainerEntry)

                If Not IsNothing(FoundTreeEntry.Node) Then
                    ' If it's in the list, just save the container location id for the item below
                    ContainerLocationID = CLng(FoundTreeEntry.Name)
                Else
                    ' Location node for the hanger is still in station location, just negative
                    Call AddAssetTreeNode(AssetTreeViewNodes, TempTreeEntry)
                End If

                InContainer = True
            End If

            ' The location of the subnode is in the asset we are looking at
            TempNode = New TreeNode
            TempNode.Name = CStr(Asset.ItemID)
            TempNode.Text = GetItemNodeText(Asset, False)
            If InContainer Then
                ' The parent is the ContainerLocationID set above
                TempNode.Tag = ContainerLocationID
            Else
                TempNode.Tag = Asset.LocationID ' Just where it's located (can, office, or main hanger)
            End If
            TempLocationInfo = SetLocationInfo(AccountID, Asset.ItemID, Asset.FlagID)
            TempNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)

            TempTreeEntry = New TreeEntry
            TempTreeEntry.Node = TempNode
            TempTreeEntry.DisplayText = TempNode.Text
            TempTreeEntry.Name = TempNode.Name
            TempTreeEntry.Tag = TempNode.Tag
            TempTreeEntry.FlagID = Asset.FlagID
            TempTreeEntry.FlagSort = Asset.FlagSort
            TempTreeEntry.IsSingleton = Asset.IsSingleton
            If Not Asset.IsSingleton Then
                TempTreeEntry.Quantity = Asset.Quantity
            Else
                TempTreeEntry.Quantity = 0
            End If

            ' Special case to mark containers for sorting, if the group contains 'Container' and the category is Celestial, then mark the container flag
            If Asset.TypeGroup.Contains("Container") And Asset.TypeCategory = "Celestial" Then
                TempTreeEntry.Container = True
            Else
                TempTreeEntry.Container = False
            End If

            ' Now store the item with ItemID
            Call AddAssetTreeNode(AssetTreeViewNodes, TempTreeEntry)
        Next

        ' Now sort the list
        If SortOption = SortType.Name Then
            AssetTreeViewNodes.Sort(New TreeEntryNameSorter)
        Else
            AssetTreeViewNodes.Sort(New TreeEntryQuantitySorter)
        End If

        ' Populate tree
        For Each Item In AssetTreeViewNodes
            If CLng(Item.Node.Tag) <> -1 Then
                ' Find Parent and add node to it
                Dim ParentNode As New TreeNode
                ChildNode = Item ' Find based on Parent ID
                ParentNode = AssetTreeViewNodes.Find(AddressOf FindParentNode).Node ' At some point add a check here so it doesn't error if not found
                ParentNode.Nodes.Add(Item.Node)
            Else
                ReturnNode.Nodes.Add(Item.Node)
            End If
        Next

        Tree.EndUpdate()
        Tree.ResumeLayout()

        Return ReturnNode

    End Function

    ' Adds a tree node to the Tree list sent
    Private Sub AddAssetTreeNode(ByRef NodeList As List(Of TreeEntry), ByVal TreeItem As TreeEntry)
        Dim FoundNode As TreeEntry = Nothing
        ChildNode = TreeItem
        FoundNode = NodeList.Find(AddressOf FindAssetNode)
        If IsNothing(FoundNode.Node) Then
            Call NodeList.Add(TreeItem)
        End If
    End Sub

    ' Predicate for finding the asset with the set itemid
    Private Function FindAssetNode(ByVal SearchItem As TreeEntry) As Boolean

        If SearchItem.Node.Name = CStr(ChildNode.Node.Name) Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Predicate for finding the container with the same info
    Private Function FindContainerEntry(ByVal SearchItem As TreeEntry) As Boolean

        ' Find the same container name and FlagID
        If SearchItem.Node.Text = CStr(ChildNode.Node.Text) And CStr(SearchItem.Tag) = CStr(ChildNode.Tag) Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Predicate for finding the parent node Tree Entry
    Private Function FindParentNode(ByVal SearchItem As TreeEntry) As Boolean

        If SearchItem.FlagID = ChildNode.FlagID And SearchItem.Node.Name = CStr(ChildNode.Node.Tag) Then
            Return True
        Else
            ' Flags aren't the same, so just look for the tag matching the name
            If SearchItem.Node.Name = CStr(ChildNode.Node.Tag) Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    ' Searches the set of settings for the location pair passed to see if it is in the location or not
    Private Function GetNodeCheckValue(ByRef SavedLocations As List(Of LocationInfo), ByVal SearchLocationInfo As LocationInfo) As Boolean
        Dim FindLocation As New LocationInfo

        ' See if it's one we check
        LocationToFind = SearchLocationInfo
        FindLocation = SavedLocations.Find(AddressOf FindLocationID)

        If FindLocation IsNot Nothing Then
            ' We found it, so check the current item
            Return True
        Else
            Return False
        End If

    End Function

    ' Searches the item and if a blueprint, formats the name else returns the name
    Private Function GetItemNodeText(SentAsset As EVEAsset, ParentNode As Boolean) As String
        Dim ItemName As String = ""

        If SentAsset.TypeCategory = "Blueprint" Then
            ' Add onto the name what type of BP it is -1 is original, -2 is copy - if the bp is packaged, singleton = 0 and is a bpo
            If SentAsset.BPType = BPType.Original Then
                ItemName = SentAsset.TypeName & " (Original)"
            Else
                ItemName = SentAsset.TypeName & " (Copy)"
            End If
        Else
            ItemName = SentAsset.TypeName
        End If

        ' Add the quantity if it's not a parent node with children or it's not a singleton (unpackaged)
        If Not ParentNode And SentAsset.IsSingleton = False Then
            Return ItemName & QuantitySpacer & FormatNumber(SentAsset.Quantity, 0)
        Else
            Return ItemName
        End If

    End Function

    ' Predicate for finding the asset with the set itemid
    Private Function FindAsset(ByVal SearchItem As EVEAsset) As Boolean

        If SearchItem.LocationID = ItemIDToFind Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Predicate for finding the asset with the set itemid
    Private Function FindAssetbyItemID(ByVal SearchItem As EVEAsset) As Boolean

        If SearchItem.ItemID = ItemIDToFind Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Predicate for finding the location in the list of location pairs 
    Private Function FindLocationID(ByVal SearchItem As LocationInfo) As Boolean

        If LocationToFind.AccountID = SearchItem.AccountID And LocationToFind.LocationID = SearchItem.LocationID And LocationToFind.FlagID = SearchItem.FlagID Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Predicate for finding an assite by typeID
    Private Function FindAssetbyTypeID(ByVal SearchItem As EVEAsset) As Boolean

        If SearchItem.TypeID = ItemIDToFind Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Predicate for finding the asset with the set itemid
    Private Function FindBaseAsset(ByVal SearchItem As EVEAsset) As Boolean

        ' Everything negative is going to be a base node to search
        If SearchItem.FlagID < 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Builds an asset list of only those assets that are in our list or linked to those in the list
    Private Function BuildMaterialAssetList(SentList As List(Of Long), ByRef OnlyBPCs As Boolean) As List(Of EVEAsset)
        Dim BuildMatAssetList As New List(Of EVEAsset)
        Dim FoundAssets As New List(Of EVEAsset)
        Dim FindAssets As New List(Of EVEAsset)
        Dim LocationAssets As New List(Of EVEAsset)
        Dim TypeID As Long

        ' Search all the assets for each itemID in the list
        For Each TypeID In SentList
            ' Find it in Assets
            ItemIDToFind = TypeID
            FindAssets = AssetList.FindAll(AddressOf FindAssetbyTypeID)

            ' Add them all to the base list
            For Each Asset In FindAssets
                If Not FoundAssets.Contains(Asset) Then
                    ' If it's a blueprint, see if we are only looking at BPCs
                    If ((Asset.TypeCategory = "Blueprint" And Asset.BPType = BPType.Copy And OnlyBPCs) Or Not OnlyBPCs Or Asset.TypeCategory <> "Blueprint") Then
                        FoundAssets.Add(Asset)
                    End If
                End If
            Next
        Next

        ' Now we should have a base list of assets, we need to figure out where they all are
        For Each Asset In FoundAssets
            ' Get the tree for this asset - including the asset
            LocationAssets = FindAssetTree(Asset)

            ' Add the entire tree of asset nodes
            For Each LocAsset In LocationAssets
                If Not BuildMatAssetList.Contains(LocAsset) Then
                    BuildMatAssetList.Add(LocAsset)
                End If
            Next
        Next

        Return BuildMatAssetList

    End Function

    ' Searches the tree for an asset and returns the list of assets found 
    Public Function FindAssetTree(SearchAsset As EVEAsset) As List(Of EVEAsset)
        Dim TreeAssets As New List(Of EVEAsset)
        Dim FindAssets As New List(Of EVEAsset)
        Dim RecursiveAssets As New List(Of EVEAsset)

        ' Look up asset by location, then based on the location found, look up the tree for a connected asset
        ' Assets are connected: LocationID -> ItemID. If no records found for where Asset.LocationID = DB.ItemID, then we are at top
        ItemIDToFind = SearchAsset.LocationID
        FindAssets = AssetList.FindAll(AddressOf FindAssetbyItemID)

        If FindAssets.Count <> 0 Then
            ' Need to run this function on every asset found
            For Each Asset In FindAssets
                RecursiveAssets = FindAssetTree(Asset)
                ' Add them to tree
                If RecursiveAssets.Count <> 0 Then
                    For Each RecursiveAsset In RecursiveAssets
                        If Not TreeAssets.Contains(RecursiveAsset) Then
                            TreeAssets.Add(RecursiveAsset)
                        End If
                    Next
                End If
            Next
        End If

        ' Make sure the asset sent is in the tree too
        If Not TreeAssets.Contains(SearchAsset) Then
            TreeAssets.Add(SearchAsset)
        End If

        ' We are done, return the list
        Return TreeAssets

    End Function

    ' Returns count of assets in this object
    Public Function GetAssetCount() As Long
        Return AssetList.Count
    End Function

    ' Predicate for finding an local location name
    Private Function FindLocation(ByVal Item As LocationName) As Boolean
        If Item.ID = LocationIDToFind Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Gets the user's saved location IDs from the table
    Public Function GetAssetLocationIDs(Location As AssetWindow, ID As Long, CharacterCorporation As Corporation) As List(Of LocationInfo)
        Dim TempLocation As LocationInfo
        Dim ReturnLocations As New List(Of LocationInfo)
        Dim SQL As String
        Dim readerLocations As SQLiteDataReader

        ' Since a lot of locations will bog down the settings loading, load locations from a table
        SQL = "SELECT ID, LocationID, FlagID FROM ASSET_LOCATIONS WHERE EnumAssetType = " & CStr(Location)
        SQL &= " AND ID IN (" & CStr(ID) & "," & CStr(CharacterCorporation.CorporationID) & ")"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerLocations = DBCommand.ExecuteReader

        While readerLocations.Read
            TempLocation = New LocationInfo
            TempLocation.AccountID = readerLocations.GetInt64(0)
            TempLocation.LocationID = readerLocations.GetInt64(1)
            TempLocation.FlagID = readerLocations.GetInt32(2)

            ReturnLocations.Add(TempLocation)
        End While

        readerLocations.Close()

        Return ReturnLocations

    End Function

    ' Gets the Cache date of these assets for updating
    ReadOnly Property CachedUntil() As Date
        Get
            Return CacheDate
        End Get
    End Property

    Private Class TreeEntryNameSorter
        Implements IComparer(Of TreeEntry)

        ' Compare the length of the strings, or the strings themselves, if they are the same length. 
        Public Function Compare(ByVal x As TreeEntry, ByVal y As TreeEntry) As Integer Implements IComparer(Of TreeEntry).Compare

            ' If container and has children then will go to the top but always use flag sort for order
            If x.Container And y.Container And x.IsSingleton And y.IsSingleton Then
                ' Do sort order, else compare the name if flagsort equal
                If x.FlagSort < y.FlagSort Then
                    ' y comes after x 
                    Return -1
                ElseIf x.FlagSort > y.FlagSort Then
                    ' x comes after y
                    Return 1
                Else
                    Return String.Compare(x.DisplayText, y.DisplayText)
                End If
            ElseIf (x.Container And x.IsSingleton) Or (y.Container And y.IsSingleton) Then ' One is a container so prioritize that
                If x.Container Then ' y comes after x container
                    Return -1
                Else ' y is a container and x comes after it
                    Return 1
                End If
            ElseIf (Not x.Container And x.IsSingleton) And (Not y.Container And y.IsSingleton) Then
                ' Unpackaged items not containers, just compare names
                Return String.Compare(x.DisplayText, y.DisplayText)
            ElseIf x.IsSingleton Or y.IsSingleton Then
                ' Singleton items go above stacked items
                If x.IsSingleton Then ' y comes after x unpackaged item
                    Return -1
                Else ' y is unpackaged and x comes after it
                    Return 1
                End If
            Else
                Return String.Compare(x.DisplayText, y.DisplayText)
            End If

        End Function

    End Class

    Private Class TreeEntryQuantitySorter
        Implements IComparer(Of TreeEntry)

        ' Compare the length of the strings, or the strings themselves, if they are the same length. 
        Public Function Compare(ByVal x As TreeEntry, ByVal y As TreeEntry) As Integer Implements IComparer(Of TreeEntry).Compare

            ' If container and has children then will go to the top but always use flag sort for order
            If x.Container And y.Container And x.IsSingleton And y.IsSingleton Then
                ' Do sort order, else compare the name if flagsort equal
                If x.FlagSort < y.FlagSort Then
                    ' y comes after x 
                    Return -1
                ElseIf x.FlagSort > y.FlagSort Then
                    ' x comes after y
                    Return 1
                Else
                    Return String.Compare(x.DisplayText, y.DisplayText)
                End If
            ElseIf (x.Container And x.IsSingleton) Or (y.Container And y.IsSingleton) Then ' One is a container so prioritize that
                If x.Container Then ' y comes after x container
                    Return -1
                Else ' y is a container and x comes after it
                    Return 1
                End If
            ElseIf (Not x.Container And x.IsSingleton) And (Not y.Container And y.IsSingleton) Then
                ' Unpackaged items not containers, just compare names
                Return String.Compare(x.DisplayText, y.DisplayText)
            ElseIf x.IsSingleton Or y.IsSingleton Then
                ' Singleton items go above stacked items
                If x.IsSingleton Then ' y comes after x unpackaged item
                    Return -1
                Else ' y is unpackaged and x comes after it
                    Return 1
                End If
            Else
                Return y.Quantity.CompareTo(x.Quantity)
            End If

        End Function

    End Class

End Class

Public Class EVEAsset
    Public LocationID As Long ' Can be a station, system, or another item id
    Public LocationName As String ' Station or system name
    Public ItemID As Long
    Public ItemName As String ' This is the in-game user name given to the item/location - e.g. can name
    Public TypeID As Long
    Public TypeName As String
    Public TypeGroup As String
    Public TypeCategory As String
    Public Quantity As Long
    Public FlagID As Integer
    Public FlagSort As Integer ' How we sort this flag in relation to others
    Public FlagText As String ' Name of flag
    Public Container As Boolean ' if the item is in a container based on the flag
    Public IsSingleton As Boolean ' True is unpacked (not stackable), False is packed (stackable)
    Public BPType As BPType ' if it's a blueprint, then will look up the data for the character for all blueprints and set

    Public Sub New()
        ItemID = 0
        ItemName = ""
        LocationID = 0
        LocationName = ""
        TypeID = 0
        TypeName = ""
        TypeGroup = ""
        Quantity = 0
        FlagID = 0
        FlagSort = -1
        FlagText = ""
        Container = False
        IsSingleton = True
        BPType = 0
    End Sub

End Class

Public Enum SortType
    Name = 1
    Quantity = 2
End Enum

Public Enum AssetTypes
    All = 1
    Selected = 2
End Enum
