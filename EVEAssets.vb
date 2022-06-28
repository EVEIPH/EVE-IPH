
Imports System.Data.SQLite

Public Class EVEAssets

    Private AssetList As List(Of EVEAsset)
    Private SelectedAssetList As List(Of EVEAsset)
    Private AssetType As ScanType
    Private CacheDate As Date
    Private ItemIDToFind As Long

    Protected LocationToFind As LocationInfo

    Private LocationNames As List(Of LocationName)
    Private AddedNodes As List(Of String)
    Private UnknownLocationCounter As Integer
    Private LocationIDToFind As Long

    Private Const IgnoreFlag As String = None
    Private Const ShipHangar As String = "Ship Hangar"
    Private Const UnknownLocation As String = "Unknown Location"
    Private Const QuantitySpacer As String = " - "
    Private Const CorpDelivery As String = "Corporation Market Deliveries / Returns"

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
        Dim TempAsset As New EVEAsset
        Dim Assets As New List(Of EVEAsset)

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
        SQL &= "CASE WHEN BP_TYPE Is NULL THEN 0 ELSE BP_TYPE END AS BPType FROM ASSETS "
        SQL &= "LEFT JOIN ALL_OWNED_BLUEPRINTS ON ID = OWNER_ID And ItemID = ITEM_ID "
        SQL &= "WHERE ID = " & ID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerAssets = DBCommand.ExecuteReader

        While readerAssets.Read
            TempAsset = New EVEAsset
            TempAsset.ItemID = readerAssets.GetInt64(1)
            TempAsset.LocationID = readerAssets.GetInt64(2)
            TempAsset.TypeID = readerAssets.GetInt64(3)
            TempAsset.Quantity = readerAssets.GetInt64(4)
            TempAsset.FlagID = readerAssets.GetInt32(5)
            TempAsset.IsSingleton = CBool(readerAssets.GetInt32(6))
            TempAsset.BPType = CType(readerAssets.GetInt32(7), BPType)

            ' Get the location name, update flagID (ref) and set flag text (ref)
            TempAsset.LocationName = GetAssetLocationAndFlagInfo(TempAsset.LocationID, TempAsset.FlagID, TempAsset.FlagText, CharacterTokenData)

            ' Look up the type name
            SQL = "SELECT typeName, groupName, categoryName "
            SQL &= "FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_CATEGORIES WHERE typeID = " & TempAsset.TypeID & " "
            SQL &= "And INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
            SQL &= "And INVENTORY_GROUPS.categoryID = INVENTORY_CATEGORIES.categoryID"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerData = DBCommand.ExecuteReader

            Try
                If readerData.Read Then
                    ' Found it
                    If Not IsDBNull(readerData.GetValue(0)) Then
                        TempAsset.TypeName = readerData.GetString(0)
                    Else
                        TempAsset.TypeName = "Unknown Item"
                    End If

                    If Not IsDBNull(readerData.GetValue(1)) Then
                        TempAsset.TypeGroup = readerData.GetString(1)
                    Else
                        TempAsset.TypeGroup = "Unknown Group"
                    End If

                    If Not IsDBNull(readerData.GetValue(2)) Then
                        TempAsset.TypeCategory = readerData.GetString(2)
                    Else
                        TempAsset.TypeCategory = "Unknown Category"
                    End If
                Else
                    TempAsset.TypeName = "Unknown Item"
                    TempAsset.TypeGroup = "Unknown Group"
                    TempAsset.TypeCategory = "Unknown Category"
                End If
            Catch ex As Exception

                TempAsset.TypeName = "Unknown Item"
                TempAsset.TypeGroup = "Unknown Group"
                TempAsset.TypeCategory = "Unknown Category"

            End Try

            readerData.Close()

            ' Insert asset
            Assets.Add(TempAsset)

        End While

        readerAssets.Close()

        AssetList = Assets

    End Sub

    Public Sub UpdateAssets(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData,
                            ByVal AssetType As ScanType, ByVal UpdateAssets As Boolean)
        Dim Assets As New List(Of ESIAsset)
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

                        ' Insert it
                        If Assets(i).location_id <> ID Then ' Don't add assets that are on the character
                            SQL = "INSERT INTO ASSETS (ID, ItemID, LocationID, TypeID, Quantity, Flag, IsSingleton) VALUES "
                            SQL &= "(" & CStr(ID) & "," & CStr(Assets(i).item_id) & "," & CStr(Assets(i).location_id) & ","
                            SQL &= CStr(Assets(i).type_id) & "," & CStr(Assets(i).quantity) & "," & CStr(FlagID) & ","
                            SQL &= CInt(Assets(i).is_singleton) & ")"

                            Call EVEDB.ExecuteNonQuerySQL(SQL)

                        End If
                    Next

                    ' Finally, update all the asset flags to negative values if they are base nodes
                    SQL = String.Format("UPDATE ASSETS SET Flag = CASE WHEN Flag > 0 THEN (Flag * -1) ELSE -2 END WHERE ID = {0} AND LocationID NOT IN (SELECT ItemID FROM ASSETS WHERE ID = {0})", ID)
                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                    Call EVEDB.CommitSQLiteTransaction()

                    DBCommand = Nothing
                End If

                ' Update cache date since it's all set now
                Call CB.UpdateCacheDate(CDType, CacheDate, ID)
            End If
        End If

    End Sub

    Public Function GetAssetList() As List(Of EVEAsset)
        Return AssetList
    End Function

    Private Function GetAssetLocationAndFlagInfo(ByVal LocationID As Long, ByRef FlagID As Integer, ByRef FlagText As String, ByVal CharacterTokenData As SavedTokenData) As String
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
                    LocationName = GetAssetLocationAndFlagInfo(readerData.GetInt64(0), FlagID, FlagText, CharacterTokenData)
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
        SQL = "SELECT FlagText FROM INVENTORY_FLAGS WHERE FlagID = " & CStr(Math.Abs(FlagID)) ' FlagID can be negative

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerData = DBCommand.ExecuteReader

        If readerData.Read Then
            ' Found it
            FlagText = CStr(readerData.GetValue(0))
        Else
            FlagText = "Unknown"
        End If

        readerData.Close()

        If FlagText = "Space" Or FlagText = "Ship Offline" Then
            LocationName = LocationName & " (In Solar System)"
        End If

        Return LocationName

    End Function

    ' Gets the Tree base node for all assets - the list of checked nodes passed
    Public Function GetAssetTreeAnchorNode(SortOption As SortType, SearchItemList As List(Of Long), NodeName As String, AccountID As Long,
                                           SavedLocations As List(Of LocationInfo), ByRef OnlyBPCs As Boolean) As TreeNode
        Dim Tree As New TreeView
        Dim AnchorNode As New TreeNode
        Dim BaseLocationNode As New TreeNode
        Dim SubNode As New TreeNode
        Dim FlagSubNode As New TreeNode
        Dim TempNode As New TreeNode
        Dim TempNodeName As String

        Dim SelectedItems As Boolean
        Dim TempLocationInfo As LocationInfo

        Dim TempAsset As New EVEAsset
        Dim BaseAssets As New List(Of EVEAsset)
        Dim LocationList As New List(Of String)

        Tree.Update()
        Tree.Nodes.Clear()

        ' If no assets, just update and leave
        If AssetList.Count = 0 Then
            AnchorNode = Tree.Nodes.Add("No " & NodeName & " Loaded")
            Tree.EndUpdate()
            Return AnchorNode
        End If
        AddedNodes = New List(Of String)
        ' Add the base node
        AnchorNode = Tree.Nodes.Add(NodeName)
        AddedNodes.Add(NodeName)
        TempLocationInfo = New LocationInfo
        TempLocationInfo.AccountID = AccountID
        TempLocationInfo.LocationID = -1
        TempLocationInfo.FlagID = 0
        AnchorNode.Tag = TempLocationInfo ' Base node

        Tree.CheckBoxes = True

        ' Only build this if we are looking for Build mat types
        If SearchItemList.Count <> 0 Then
            SelectedItems = True

            ' Build the asset list based only on these mats - Need to take the list of mats we have, then search all
            ' assets to find those item ID's that we want only
            ' Search built asset list for only base assets
            SelectedAssetList = New List(Of EVEAsset)
            SelectedAssetList = BuildMaterialAssetList(SearchItemList, OnlyBPCs)
            ' Get the base assets that have items we want somewhere down the tree
            BaseAssets = SelectedAssetList.FindAll(AddressOf FindBaseAsset)

        Else ' Get all items
            SelectedItems = False
            ' Get just base assets
            BaseAssets = AssetList.FindAll(AddressOf FindBaseAsset)
        End If

        ' Loop through each node and add all the items in it
        For Each TempAsset In BaseAssets

            ' If we know the location and the node is a base node, then process
            If TempAsset.LocationName <> UnknownLocation And TempAsset.FlagID <= 0 Then

                ' See if we have added the location
                If Not LocationList.Contains(TempAsset.LocationName) Then

                    ' Add the base location to the list
                    LocationList.Add(TempAsset.LocationName)
                    BaseLocationNode = AnchorNode.Nodes.Add(TempAsset.LocationName)
                    AddedNodes.Add(TempAsset.LocationName)
                    BaseLocationNode.Name = TempAsset.LocationName

                    ' Also save the LocationID in the tag of the node for use searching later
                    TempLocationInfo = New LocationInfo
                    TempLocationInfo.AccountID = AccountID
                    TempLocationInfo.LocationID = TempAsset.LocationID
                    TempLocationInfo.FlagID = TempAsset.FlagID
                    BaseLocationNode.Tag = TempLocationInfo

                    BaseLocationNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)

                End If

                ' Find the first node that is the same as the location added above - make sure we are adding to the right node
                BaseLocationNode = AnchorNode.Nodes.Find(TempAsset.LocationName, True)(0)

                ' Add the subnode to the tree without the name yet, wait for the search for children before marking
                If (TempAsset.TypeCategory = "Ship" And AssetType = ScanType.Corporation And TempAsset.FlagText <> "Ship Offline") Or TempAsset.FlagText = CorpDelivery Then

                    ' Check corp deliveries first since a ship could be a delivery
                    If TempAsset.FlagText = CorpDelivery Then
                        TempNodeName = CorpDelivery
                    Else
                        TempNodeName = ShipHangar
                    End If

                    ' Add a new sub node if not in the tree
                    If BaseLocationNode.Nodes.Find(TempNodeName, True).Count = 0 Then
                        TempNode = BaseLocationNode.Nodes.Add(TempNodeName)
                        AddedNodes.Add(TempNodeName)
                        TempNode.Name = TempNodeName
                        ' Since I add a ship hanger (for personal assets) or a corp delivery hanger (corp assets) need to set the location id's in the tree
                        ' to compensate. So store the negative of the location. Ie, it'll be a station for these so store the negative of the station ID
                        TempLocationInfo = New LocationInfo
                        TempLocationInfo.AccountID = AccountID
                        TempLocationInfo.LocationID = -1 * TempAsset.LocationID
                        TempLocationInfo.FlagID = TempAsset.FlagID
                        TempNode.Tag = TempLocationInfo
                        TempNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)
                    End If

                    ' Find the corp delivery or ship hanger node to add to
                    TempNode = BaseLocationNode.Nodes.Find(TempNodeName, True)(0)
                    SubNode = TempNode.Nodes.Add("")

                Else
                    SubNode = BaseLocationNode.Nodes.Add("")
                End If

                ' The location of the subnode is in the asset we are looking at
                TempLocationInfo = New LocationInfo
                TempLocationInfo.AccountID = AccountID
                TempLocationInfo.LocationID = TempAsset.ItemID
                TempLocationInfo.FlagID = TempAsset.FlagID
                SubNode.Tag = TempLocationInfo
                SubNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)
                SubNode.ImageIndex = Math.Abs(TempAsset.FlagID)

                ' See if the node has children and add
                Call SetSubTreeNode(SubNode, TempAsset, SortOption, SelectedItems, AccountID, SavedLocations)

                ' Update the Node Text
                If SubNode.GetNodeCount(True) <> 0 Then
                    ' Has children so don't display the quantity
                    SubNode.Text = GetItemNodeText(TempAsset, True)
                Else
                    SubNode.Text = GetItemNodeText(TempAsset, False)
                End If
                AddedNodes.Add(SubNode.Text)
            End If
        Next

        ' Finally sort the tree
        If SortOption = SortType.Name Then
            Tree.TreeViewNodeSorter = New NodeNameSorter
        ElseIf SortOption = SortType.Quantity Then
            Tree.TreeViewNodeSorter = New NodeQuantitySorter
        Else
            Tree.TreeViewNodeSorter = New NodeNameSorter
        End If

        Tree.EndUpdate()

        Return AnchorNode

    End Function

    ' Gets subnodes of the Parent ID
    Private Sub SetSubTreeNode(ByRef BaseNode As TreeNode, ParentAsset As EVEAsset, SortOption As SortType, SelectedItems As Boolean, AccountID As Long,
                               SavedLocations As List(Of LocationInfo))
        Dim CategoryNode As New TreeNode
        Dim SubNode As New TreeNode

        Dim TempNode As New TreeNode
        Dim CategoryFlagName As String
        Dim FlagIDList As New List(Of Long)

        Dim TempAsset As New EVEAsset
        Dim FoundAssets As New List(Of EVEAsset)
        Dim TempLocationInfo As LocationInfo

        ' Search for each item sent, may not be in list
        ItemIDToFind = ParentAsset.ItemID
        If Not SelectedItems Then
            FoundAssets = AssetList.FindAll(AddressOf FindAsset)
        Else ' just selected assets
            FoundAssets = SelectedAssetList.FindAll(AddressOf FindAsset)
        End If

        If FoundAssets.Count <> 0 Then

            ' Loop through all assets and add the nodes
            For Each TempAsset In FoundAssets

                ' Get flag name first - this is the category we are adding
                If TempAsset.FlagText = "Hangar" Then
                    ' First corp hanger flag is called hanger, not Access Group, so change it
                    CategoryFlagName = "Corp Hangar 1"
                Else
                    CategoryFlagName = TempAsset.FlagText
                End If

                If CategoryFlagName <> IgnoreFlag Then
                    ' If we haven't added it yet
                    If Not FlagIDList.Contains(TempAsset.FlagID) Then
                        ' Add the flag to the list
                        FlagIDList.Add(TempAsset.FlagID)

                        ' Add the flag as a base node
                        CategoryNode = BaseNode.Nodes.Add(CategoryFlagName)
                        AddedNodes.Add(CategoryFlagName)
                        CategoryNode.Name = CategoryFlagName
                        ' Save the location this node is part
                        TempLocationInfo = New LocationInfo
                        TempLocationInfo.AccountID = AccountID
                        TempLocationInfo.LocationID = ParentAsset.ItemID
                        TempLocationInfo.FlagID = TempAsset.FlagID
                        CategoryNode.Tag = TempLocationInfo
                        CategoryNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)
                    End If

                    ' Find the first node that is the same as the location added above - make sure we are adding to the right node
                    CategoryNode = BaseNode.Nodes.Find(CategoryFlagName, True)(0)

                    ' Add the subnode without text
                    SubNode = CategoryNode.Nodes.Add("")

                    ' Check for sub nodes of the found asset
                    Call SetSubTreeNode(SubNode, TempAsset, SortOption, SelectedItems, AccountID, SavedLocations)

                    ' Add the item at this base location (ie mineral in hanger)
                    If CategoryFlagName.Contains("power slot") Then
                        ' If the parent node is a slot, then the item will be the only one in it - leave off quantity
                        SubNode.Text = GetItemNodeText(TempAsset, True)
                    ElseIf SubNode.Nodes.Count <> 0 Then
                        ' This has items under it so just show the name
                        SubNode.Text = GetItemNodeText(TempAsset, True)
                    Else
                        ' Base items
                        SubNode.Text = GetItemNodeText(TempAsset, False)
                    End If
                    AddedNodes.Add(SubNode.Text)
                Else

                    ' Add the item at this base location (ie mineral in hanger)
                    SubNode = BaseNode.Nodes.Add(GetItemNodeText(TempAsset, False))
                    AddedNodes.Add(GetItemNodeText(TempAsset, False))
                    ' Check for sub nodes of the found asset
                    Call SetSubTreeNode(SubNode, TempAsset, SortOption, SelectedItems, AccountID, SavedLocations)
                End If

                ' Location of the subnode is under the category node
                TempLocationInfo = New LocationInfo
                TempLocationInfo.AccountID = AccountID
                TempLocationInfo.LocationID = TempAsset.ItemID
                TempLocationInfo.FlagID = TempAsset.FlagID
                SubNode.Tag = TempLocationInfo
                SubNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)
            Next
        End If

    End Sub

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

        ' Add the quantity if it's not a parent node with children
        If Not ParentNode Then
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
                    If (Asset.TypeCategory = "Blueprint" And Asset.BPType = BPType.Copy And OnlyBPCs) Or Not OnlyBPCs Or Asset.TypeCategory <> "Blueprint" Then
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

    Private Class NodeNameSorter
        Implements IComparer

        ' Compare the length of the strings, or the strings 
        ' themselves, if they are the same length. 
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim tx As TreeNode = CType(x, TreeNode)
            Dim ty As TreeNode = CType(y, TreeNode)
            Dim Qx As Long
            Dim Qy As Long

            ' Get the quantity for the nodes
            Qx = GetNodeQuantity(tx)
            Qy = GetNodeQuantity(ty)

            If tx.Text.Contains("R.Db") Or ty.Text.Contains("R.Db") Then
                Application.DoEvents()
            End If

            If Qx = 0 And Qy = 0 Then
                ' Sort by name
                Return String.Compare(tx.Text, ty.Text)
            ElseIf Qx = 0 Or Qy = 0 Then
                ' Sort assending, since a zero quantity is a base item and should go to the top
                Return Qx.CompareTo(Qy)
            Else
                Return String.Compare(tx.Text, ty.Text)
            End If

        End Function

    End Class

    Private Class NodeQuantitySorter
        Implements IComparer

        ' Compare the length of the strings, or the strings 
        ' themselves, if they are the same length. 
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim tx As TreeNode = CType(x, TreeNode)
            Dim ty As TreeNode = CType(y, TreeNode)
            Dim Qx As Long
            Dim Qy As Long

            ' Get the quantity for the nodes
            Qx = GetNodeQuantity(tx)
            Qy = GetNodeQuantity(ty)

            If Qx = 0 And Qy = 0 Then
                ' Sort by name!
                Return String.Compare(tx.Text, ty.Text)
            ElseIf Qx = 0 Or Qy = 0 Then
                ' Sort assending, since a zero quanity is a base item and should go to the top
                Return Qx.CompareTo(Qy)
            Else
                ' Sort descending
                Return Qy.CompareTo(Qx)
            End If

        End Function

    End Class

    Private Shared Function GetNodeQuantity(ByRef SentTreeNode As TreeNode) As Long
        Dim NodeQuantity As Long = 0

        ' Get the quantity from the first string
        If InStr(SentTreeNode.Text, QuantitySpacer) <> 0 Then
            ' We can get the quantity - only find the last spacer at the end since the name can have dashes too
            If IsNumeric(SentTreeNode.Text.Substring(SentTreeNode.Text.LastIndexOf(QuantitySpacer) + Len(QuantitySpacer) - 1)) Then
                NodeQuantity = CLng(SentTreeNode.Text.Substring(SentTreeNode.Text.LastIndexOf(QuantitySpacer) + Len(QuantitySpacer) - 1))
            End If
        End If

        Return NodeQuantity

    End Function

    ' For sorting assets by name
    Public Class AssetNameComparer

        Implements System.Collections.Generic.IComparer(Of EVEAsset)

        Public Function Compare(ByVal p1 As EVEAsset, ByVal p2 As EVEAsset) As Integer Implements IComparer(Of EVEAsset).Compare
            ' Sort by name alphabetically
            Return p1.TypeName.CompareTo(p2.TypeName)
        End Function

    End Class

    ' For sorting assets by quantity
    Public Class AssetQuantityComparer

        Implements System.Collections.Generic.IComparer(Of EVEAsset)

        Public Function Compare(ByVal p1 As EVEAsset, ByVal p2 As EVEAsset) As Integer Implements IComparer(Of EVEAsset).Compare
            ' Sort by quantity decending
            Return p2.Quantity.CompareTo(p1.Quantity)
        End Function

    End Class

    ' For sorting assets by item group
    Public Class AssetGroupComparer

        Implements System.Collections.Generic.IComparer(Of EVEAsset)

        Public Function Compare(ByVal p1 As EVEAsset, ByVal p2 As EVEAsset) As Integer Implements IComparer(Of EVEAsset).Compare
            ' Sort by group name alphabetically
            Return p1.TypeGroup.CompareTo(p2.TypeGroup)
        End Function

    End Class

    ' For sorting assets by flag id
    Public Class AssetFlagComparer

        Implements System.Collections.Generic.IComparer(Of EVEAsset)

        Public Function Compare(ByVal p1 As EVEAsset, ByVal p2 As EVEAsset) As Integer Implements IComparer(Of EVEAsset).Compare
            ' Sort by flagid
            Return p1.FlagID.CompareTo(p2.FlagID)
        End Function

    End Class

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

    ReadOnly Property CachedUntil() As Date
        Get
            Return CacheDate
        End Get
    End Property

End Class

Public Class EVEAsset
    Public LocationID As Long ' Can be a station, system, or another item id
    Public LocationName As String ' Station or system name
    Public ItemID As Long
    Public TypeID As Long
    Public TypeName As String
    Public TypeGroup As String
    Public TypeCategory As String
    Public Quantity As Long
    Public FlagID As Integer
    Public FlagText As String ' Name of flag
    Public IsSingleton As Boolean ' True is unpacked (not stackable), False is packed (stackable)
    Public BPType As BPType ' if it's a blueprint, then will look up the data for the character for all blueprints and set

    Public Sub New()
        ItemID = 0
        LocationID = 0
        LocationName = ""
        TypeID = 0
        TypeName = ""
        TypeGroup = ""
        Quantity = 0
        FlagID = 0
        FlagText = ""
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
