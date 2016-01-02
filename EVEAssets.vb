
Imports System.Data.SQLite

Public Class EVEAssets

    Private AssetList As List(Of EVEAsset)
    Private SelectedAssetList As List(Of EVEAsset)
    Private KeyData As APIKeyData
    Private CorpID As Long

    Private CacheDate As Date

    Private ItemIDToFind As Long

    Protected LocationToFind As LocationInfo

    Private Const IgnoreFlag As String = None
    Private Const ShipHangar As String = "Ship Hangar"
    Private Const QuantitySpacer As String = " - "
    Private Const CorpDelivery As String = "Corporation Market Deliveries / Returns"

    Public Sub New(Optional ByVal Key As APIKeyData = Nothing, Optional ByVal CorporationID As Long = 0)

        If IsNothing(Key) Then
            KeyData = New APIKeyData
        Else
            KeyData = Key
        End If

        AssetList = New List(Of EVEAsset)

        ' Set for corp industry jobs
        CorpID = CorporationID

        ' Default to this until we set it
        CacheDate = NoExpiry

    End Sub

    Public Sub LoadAssets(AssetType As ScanType, ByVal RefreshAssets As Boolean)
        Dim SQL As String
        Dim readerAssets As SQLiteDataReader
        Dim readerData As SQLiteDataReader
        Dim TempAsset As New EVEAsset
        Dim Assets As New List(Of EVEAsset)
        Dim ScanID As Long

        If Not KeyData.Access Then
            ' They don't have access to the api so leave
            Exit Sub
        End If

        ' Update Assets if we have it set first
        If RefreshAssets Then
            Call UpdateAssets(AssetType)
        End If

        ' Set the ID - Corp or Personal ID number
        If AssetType = ScanType.Personal Then
            ScanID = KeyData.ID
        Else
            ScanID = CorpID
        End If

        ' Store the cache date
        ' Load the assets - corp or personal
        ' Look up the asset cache date first, if past the date, update the database
        CacheDate = GetCacheDate(AssetType)

        ' Load the assets - corp or personal
        SQL = "SELECT * FROM ASSETS WHERE ID = " & ScanID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerAssets = DBCommand.ExecuteReader

        While readerAssets.Read
            TempAsset = New EVEAsset
            TempAsset.ItemID = readerAssets.GetInt64(1)
            TempAsset.LocationID = readerAssets.GetInt64(2)
            TempAsset.TypeID = readerAssets.GetInt64(3)
            TempAsset.Quantity = readerAssets.GetInt64(4)
            TempAsset.FlagID = readerAssets.GetInt32(5)
            TempAsset.Singleton = readerAssets.GetInt32(6)
            TempAsset.RawQuantity = readerAssets.GetInt32(7)

            ' Look up the location name...first start with stations, then systems
            If TempAsset.LocationID <> 0 Then

                ' See if it's a station
                If CStr(TempAsset.LocationID).Substring(0, 1) = "6" Then

                    SQL = "SELECT STATION_NAME FROM STATIONS WHERE STATION_ID = " & CStr(TempAsset.LocationID)
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    readerData = DBCommand.ExecuteReader

                    If readerData.Read Then
                        ' Found it
                        TempAsset.LocationName = readerData.GetString(0)
                    Else
                        ' Unknown
                        TempAsset.LocationName = "Unknown Location"
                    End If
                    readerData.Close()

                ElseIf CStr(TempAsset.LocationID).Substring(0, 1) = "3" Then ' See if it's a solar system
                    SQL = "SELECT solarSystemName FROM SOLAR_SYSTEMS WHERE solarSystemID = " & CStr(TempAsset.LocationID)

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    readerData = DBCommand.ExecuteReader

                    If readerData.Read Then
                        ' Found it
                        TempAsset.LocationName = readerData.GetString(0)
                    Else
                        ' Unknown
                        TempAsset.LocationName = "Unknown Location"
                    End If

                    ' See if it has a flag assigned, if 0 then set it to 500 (my own code for space)
                    If TempAsset.FlagID = 0 Then
                        TempAsset.FlagID = -1 * SpaceFlagCode 'negative for base item
                    End If

                    readerData.Close()
                Else
                    TempAsset.LocationName = "Unknown Location"
                End If
            Else
                TempAsset.LocationName = "Unknown Location"
            End If

            ' Look up the flag text
            SQL = "SELECT FlagText FROM INVENTORY_FLAGS WHERE FlagID = " & CStr(Math.Abs(TempAsset.FlagID)) ' FlagID can be negative

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerData = DBCommand.ExecuteReader

            If readerData.Read Then
                ' Found it
                TempAsset.FlagText = readerData.GetString(0)
            Else
                TempAsset.FlagText = "Unknown"
            End If

            readerData.Close()

            ' Look up the type name
            SQL = "SELECT typeName, groupName, categoryName "
            SQL = SQL & "FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_CATEGORIES WHERE typeID = " & TempAsset.TypeID & " "
            SQL = SQL & "AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
            SQL = SQL & "AND INVENTORY_GROUPS.categoryID = INVENTORY_CATEGORIES.categoryID"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerData = DBCommand.ExecuteReader

            If readerData.Read Then
                ' Found it
                TempAsset.TypeName = readerData.GetString(0)
                TempAsset.TypeGroup = readerData.GetString(1)
                TempAsset.TypeCategory = readerData.GetString(2)
            Else
                TempAsset.TypeName = "Unknown Item"
                TempAsset.TypeGroup = "Unknown Group"
                TempAsset.TypeCategory = "Unknown Category"
            End If

            readerData.Close()

            ' Insert asset
            Assets.Add(TempAsset)

        End While

        readerAssets.Close()
        DBCommand = Nothing
        readerAssets = Nothing

        AssetList = Assets

    End Sub

    Public Sub UpdateAssets(AssetType As ScanType)
        Dim AssetAPI As New EVEAPI
        Dim Assets As New List(Of EVEAsset)
        Dim SQL As String = ""
        Dim ScanID As Long

        ' Make sure they have access
        If Not KeyData.Access Then
            Exit Sub
        End If

        ' Look up the asset cache date first, if past the date, update the database
        CacheDate = GetCacheDate(AssetType)

        ' Only update if we need to 
        If CacheDate < Date.UtcNow Then

            ' Get the assets
            Assets = AssetAPI.GetAssets(KeyData, AssetType, CacheDate)

            If Not NoAPIError(AssetAPI.GetErrorText, "Character") Then
                ' Errored, exit
                Exit Sub
            End If

            Call EVEDB.BeginSQLiteTransaction()

            ' Update the cache date
            SQL = "UPDATE API SET ASSETS_CACHED_UNTIL = '" & Format(CacheDate, SQLiteDateFormat)

            If AssetType = ScanType.Personal Then
                SQL = SQL & "' WHERE CHARACTER_ID = " & KeyData.ID
                SQL = SQL & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            Else
                SQL = SQL & "' WHERE CORPORATION_ID = " & CorpID
                SQL = SQL & " AND API_TYPE = 'Corporation'"
            End If

            Call evedb.ExecuteNonQuerySQL(SQL)

            ' Set the ID - Corp or Personal ID number
            If AssetType = ScanType.Personal Then
                ScanID = KeyData.ID
            Else
                ScanID = CorpID
            End If

            ' Clear the current assets in the database
            SQL = "DELETE FROM ASSETS WHERE ID = " & CStr(ScanID)

            Call evedb.ExecuteNonQuerySQL(SQL)

            ' Insert the records into the DB
            For i = 0 To Assets.Count - 1

                ' Insert it
                SQL = "INSERT INTO ASSETS (ID, ItemID, LocationID, TypeID, Quantity, Flag, Singleton, RawQuantity) VALUES "
                SQL = SQL & "(" & CStr(ScanID) & "," & CStr(Assets(i).ItemID) & "," & CStr(Assets(i).LocationID) & ","
                SQL = SQL & CStr(Assets(i).TypeID) & "," & CStr(Assets(i).Quantity) & "," & CStr(Assets(i).FlagID) & "," & CStr(Assets(i).Singleton) & ","
                SQL = SQL & CStr(Assets(i).RawQuantity) & ")"

                Call evedb.ExecuteNonQuerySQL(SQL)

            Next

            Call EVEDB.CommitSQLiteTransaction()

        End If

    End Sub

    Public Function GetAssetList() As List(Of EVEAsset)
        Return AssetList
    End Function

    ReadOnly Property CachedUntil() As Date
        Get
            Return CacheDate
        End Get
    End Property

    ' Gets the Tree base node for all assets - the list of checked nodes passed
    Public Function GetAssetTreeAnchorNode(SortOption As SortType, SearchItemList As List(Of Long), NodeName As String, AccountID As Long, _
                                           SavedLocations As List(Of LocationInfo), ByRef OnlyBPCs As Boolean) As TreeNode
        Dim Tree As New TreeView
        Dim AnchorNode As New TreeNode
        Dim BaseLocationNode As New TreeNode
        Dim SubNode As New TreeNode
        Dim FlagSubNode As New TreeNode
        Dim TempNode As New TreeNode
        Dim TempNodeName As String
        Dim LocationName As String

        Dim SelectedItems As Boolean
        Dim TempLocationInfo As LocationInfo

        Dim TempAsset As New EVEAsset
        Dim BaseAssets As New List(Of EVEAsset)
        Dim LocationList As New List(Of Double)

        Tree.Update()
        Tree.Nodes.Clear()

        ' If no assets, just update and leave
        If AssetList.Count = 0 Then
            AnchorNode = Tree.Nodes.Add("No " & NodeName & " Loaded")
            Tree.EndUpdate()
            Return AnchorNode
        End If

        ' Add the base node
        AnchorNode = Tree.Nodes.Add(NodeName)
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

        ' Loop through each base node and add all the items in it
        For Each TempAsset In BaseAssets

            If TempAsset.FlagText = "Space" Or TempAsset.FlagText = "Ship Offline" Then
                LocationName = TempAsset.LocationName & " (In Solar System)"
            Else
                LocationName = TempAsset.LocationName
            End If

            ' See if we have added the location
            If Not LocationList.Contains(TempAsset.LocationID) Then
                LocationName = LocationName
                ' Add the location to the list
                LocationList.Add(TempAsset.LocationID)

                BaseLocationNode = AnchorNode.Nodes.Add(LocationName)
                BaseLocationNode.Name = LocationName

                ' Also save the LocationID in the tag of the node for use searching later
                TempLocationInfo = New LocationInfo
                TempLocationInfo.AccountID = AccountID
                TempLocationInfo.LocationID = TempAsset.LocationID
                TempLocationInfo.FlagID = TempAsset.FlagID
                BaseLocationNode.Tag = TempLocationInfo

                BaseLocationNode.Checked = GetNodeCheckValue(SavedLocations, TempLocationInfo)

            End If

            ' Find the first node that is the same as the location added above - make sure we are adding to the right node
            BaseLocationNode = AnchorNode.Nodes.Find(LocationName, True)(0)

            ' Add the subnode to the tree without the name yet, wait for the search for children before marking
            If (TempAsset.TypeCategory = "Ship" And CorpID = 0 And TempAsset.FlagText <> "Ship Offline") Or TempAsset.FlagText = CorpDelivery Then

                ' Check corp deliveries first since a ship coul d be a delivery
                If TempAsset.FlagText = CorpDelivery Then
                    TempNodeName = CorpDelivery
                Else
                    TempNodeName = ShipHangar
                End If

                ' Add a new sub node if not in the tree
                If BaseLocationNode.Nodes.Find(TempNodeName, True).Count = 0 Then
                    TempNode = BaseLocationNode.Nodes.Add(TempNodeName)
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

                ' Find the ship hanger node to add to
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
            Call GetSubTreeNode(SubNode, TempAsset, SortOption, SelectedItems, AccountID, SavedLocations)

            ' Update the Node Text
            If SubNode.GetNodeCount(True) <> 0 Then
                ' Has children so don't display the quantity
                SubNode.Text = GetItemNodeText(TempAsset, True)
            Else
                SubNode.Text = GetItemNodeText(TempAsset, False)
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
    Private Sub GetSubTreeNode(ByRef BaseNode As TreeNode, ParentAsset As EVEAsset, SortOption As SortType, SelectedItems As Boolean, AccountID As Long, _
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
                    Call GetSubTreeNode(SubNode, TempAsset, SortOption, SelectedItems, AccountID, SavedLocations)

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

                Else

                    ' Add the item at this base location (ie mineral in hanger)
                    SubNode = BaseNode.Nodes.Add(GetItemNodeText(TempAsset, False))

                    ' Check for sub nodes of the found asset
                    Call GetSubTreeNode(SubNode, TempAsset, SortOption, SelectedItems, AccountID, SavedLocations)
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
            If SentAsset.RawQuantity = BPType.Original Or SentAsset.Singleton = 0 Then
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
                    If (Asset.TypeCategory = "Blueprint" And Asset.RawQuantity = BPType.Copy And OnlyBPCs) Or Not OnlyBPCs Or Asset.TypeCategory <> "Blueprint" Then
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

    Private Function GetCacheDate(AssetType As ScanType) As Date
        Dim RefreshDate As Date ' To check the update of the API.
        Dim readerAssets As SQLiteDataReader

        Dim SQL As String = ""

        ' Look up the asset cache date first, if past the date, update the database
        SQL = "SELECT ASSETS_CACHED_UNTIL FROM API "
        If AssetType = ScanType.Personal Then
            SQL = SQL & "WHERE CHARACTER_ID = " & KeyData.ID
            SQL = SQL & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
        Else
            SQL = SQL & "WHERE CORPORATION_ID = " & CorpID
            SQL = SQL & " AND API_TYPE = 'Corporation'"
        End If

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerAssets = DBCommand.ExecuteReader

        If readerAssets.Read Then
            If Not IsDBNull(readerAssets.GetValue(0)) Then
                If readerAssets.GetString(0) = "" Then
                    RefreshDate = NoDate
                Else
                    RefreshDate = CDate(readerAssets.GetString(0))
                End If
            Else
                RefreshDate = NoDate
            End If
        Else
            RefreshDate = NoDate
        End If

        readerAssets.Close()

        Return RefreshDate

    End Function

    Public Function GetAssetCount() As Long
        Return AssetList.Count
    End Function

End Class

Public Class EVEAsset
    Public ItemID As Long
    Public LocationID As Long ' Can be a station, system, or another item id
    Public LocationName As String ' Station or system name
    Public TypeID As Long
    Public TypeName As String
    Public TypeGroup As String
    Public TypeCategory As String
    Public Quantity As Long
    Public FlagID As Integer
    Public FlagText As String ' Name of flag
    Public Singleton As Integer
    Public RawQuantity As Integer ' This is the BP type

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
        Singleton = 0
        RawQuantity = 0
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
