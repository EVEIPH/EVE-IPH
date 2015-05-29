
Imports System.Xml ' For API Calls  

' Class for querying data from EVE Central
' Can only query Eve Central 100 records at a time 

Public Class EVECentralAPI

    Private EVECentralURL As String = "http://api.eve-central.com/api/marketstat?"
    Private ErrorData As MyError

    ' Function takes an array of strings for Regions and a TypeID list, returns an array of EVE Central prices
    Public Function GetPrices(ByVal Regions As List(Of String), ByVal System As String, ByVal TypeIDList As List(Of Long), Optional TypeIDBatchCount As Integer = 100) As List(Of EVECentralPrice)
        Dim PriceRecords As New List(Of EVECentralPrice)
        Dim TempRecord As EVECentralPrice

        ' XML variables
        Dim m_xmld As XmlDocument
        ' For the main Type ID nodes
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim sub_nodelist As Xml.XmlNodeList
        Dim sub_node As Xml.XmlNode

        Dim i As Integer = 0
        Dim ItemCount As Integer = 0 ' For breaking it into sets of 100 for query

        Dim EVECentralItemList As String = "" ' String of items in eve central format to build the query string
        Dim EVECentralRegionHeader As String = "regionlimit="
        Dim EVECentralSystemHeader As String = "usesystem="
        Dim EVECentralRegionSystem As String = "" ' String of regions in eve central format to query

        Dim DBRegionList As String = "" ' The string of numbers for the list in the DB (region, region, region)

        ' Example Query
        'http://api.eve-central.com/api/marketstat?typeid=34&typeid=35&regionlimit=10000002&regionlimit=10000003
        'http://api.eve-central.com/api/marketstat?typeid=34&usesystem=30000142

        ' First Build the Region List
        If System = "" Then
            For i = 0 To Regions.Count - 1
                EVECentralRegionSystem = EVECentralRegionSystem & Regions(i) & "&" & EVECentralRegionHeader
                DBRegionList = DBRegionList & Regions(i) & ","
            Next

            ' Strip off the last &
            EVECentralRegionSystem = EVECentralRegionHeader & Left(EVECentralRegionSystem, EVECentralRegionSystem.Length - Len("&" & EVECentralRegionHeader))
            ' Strip off last comma
            DBRegionList = Left(DBRegionList, DBRegionList.Length - 1)

        Else
            EVECentralRegionSystem = EVECentralSystemHeader & System
            DBRegionList = System
        End If

        ' Loop through the TypeID's and query prices. For each set of 100, query the data and insert it
        For i = 0 To TypeIDList.Count - 1

            ' Count each for batch size check
            ItemCount = ItemCount + 1

            Select Case TypeIDList(i)
                ' Ignore the outposts and outpost upgrades - the upgrades are on the market but he still errors them
                Case 28076, 28078, 28079, 28077, 28080, 28081, 28083, 28084, 28082, 28085, 28086, 28088, 28089, 28087, _
                    28090, 28091, 28092, 28094, 28095, 28093, 28096, 28097, 28099, 28100, 28098, 28101, 28102, 28104, _
                    28105, 28103, 28106, 28108, 28109, 28107, 28110, 28111, 28113, 28114, 28112, 28115, 28116, 28118, _
                    28119, 28117, 28120, 28121, 28123, 28124, 28122, 28125, 28126, 28128, 28129, 28127, 28130, 28131, _
                    28133, 28134, 28132, 28135, 21644, 21642, 21645, 21646

                    ' Build the list for web query
                    EVECentralItemList = EVECentralItemList

                Case Else

                    ' Build the list for web query
                    EVECentralItemList = EVECentralItemList & "typeid=" & TypeIDList(i) & "&"

            End Select

            ' See if we are at BatchCount or at the end, if so - query EC and insert records
            ' Also, if the request is larger than 2000 characters (play it safe at 1900), we need to run it or we'll get a 414 (too long) url error
            If ItemCount = TypeIDBatchCount Or (i = TypeIDList.Count - 1) Or Len(EVECentralURL & EVECentralItemList & EVECentralRegionSystem) > 1900 Then
                ' Query EVE Central
                ' Create the XML Document
                m_xmld = New XmlDocument

                ' Load the Xml file
                Try
                    m_xmld.Load(EVECentralURL & EVECentralItemList & EVECentralRegionSystem)

                    ' Set the TypeID node
                    m_nodelist = m_xmld.SelectNodes("/evec_api/marketstat/type")

                    ' Loop through each attribute
                    For Each m_node In m_nodelist
                        ' Get the typeid
                        TempRecord.TypeID = CInt(m_node.Attributes.GetNamedItem("id").Value)
                        TempRecord.RegionList = DBRegionList
                        TempRecord.Errored = False

                        ' Set the child nodes for each type id to get the prices
                        sub_nodelist = m_node.ChildNodes

                        ' Loop through 3 nodes and get values
                        For Each sub_node In sub_nodelist
                            If sub_node.Name = "all" Then
                                TempRecord.AllVolume = CDbl(sub_node.ChildNodes.Item(0).InnerText)
                                TempRecord.AllAvgPrice = CDbl(sub_node.ChildNodes.Item(1).InnerText)
                                TempRecord.AllMaxPrice = CDbl(sub_node.ChildNodes.Item(2).InnerText)
                                TempRecord.AllMinPrice = CDbl(sub_node.ChildNodes.Item(3).InnerText)
                                TempRecord.AllStdDev = CDbl(sub_node.ChildNodes.Item(4).InnerText)
                                TempRecord.AllMedian = CDbl(sub_node.ChildNodes.Item(5).InnerText)
                                TempRecord.AllPercentile = CDbl(sub_node.ChildNodes.Item(6).InnerText)
                            ElseIf sub_node.Name = "buy" Then
                                TempRecord.BuyVolume = CDbl(sub_node.ChildNodes.Item(0).InnerText)
                                TempRecord.BuyAvgPrice = CDbl(sub_node.ChildNodes.Item(1).InnerText)
                                TempRecord.BuyMaxPrice = CDbl(sub_node.ChildNodes.Item(2).InnerText)
                                TempRecord.BuyMinPrice = CDbl(sub_node.ChildNodes.Item(3).InnerText)
                                TempRecord.BuyStdDev = CDbl(sub_node.ChildNodes.Item(4).InnerText)
                                TempRecord.BuyMedian = CDbl(sub_node.ChildNodes.Item(5).InnerText)
                                TempRecord.BuyPercentile = CDbl(sub_node.ChildNodes.Item(6).InnerText)
                            ElseIf sub_node.Name = "sell" Then
                                TempRecord.SellVolume = CDbl(sub_node.ChildNodes.Item(0).InnerText)
                                TempRecord.SellAvgPrice = CDbl(sub_node.ChildNodes.Item(1).InnerText)
                                TempRecord.SellMaxPrice = CDbl(sub_node.ChildNodes.Item(2).InnerText)
                                TempRecord.SellMinPrice = CDbl(sub_node.ChildNodes.Item(3).InnerText)
                                TempRecord.SellStdDev = CDbl(sub_node.ChildNodes.Item(4).InnerText)
                                TempRecord.SellMedian = CDbl(sub_node.ChildNodes.Item(5).InnerText)
                                TempRecord.SellPercentile = CDbl(sub_node.ChildNodes.Item(6).InnerText)
                            End If
                        Next

                        ' Add the record
                        PriceRecords.Add(TempRecord)

                        Application.DoEvents()
                    Next

                Catch ex As Exception
                    ' Determine if it's a 4xx error (my error) or 5xx (server error)
                    Dim ErrMsg As String = ex.Message
                    ' Get the first digit
                    Dim ErrorCode As Integer

                    If InStr(ErrMsg, "(") <> 0 Then
                        ' It has an error code
                        If IsNumeric(ErrMsg.Substring(InStr(ErrMsg, "("), 1)) Then
                            ErrorCode = CInt(ErrMsg.Substring(InStr(ErrMsg, "("), 1))
                        Else
                            ' No clue what it is
                            ' Message box and then exit
                            ErrorData.Description = ErrMsg & vbCrLf & " In: " & ex.Source & vbCrLf & " With: " & ex.Data.ToString & vbCrLf
                            ErrorData.Number = ErrorCode
                            Return Nothing
                        End If
                    Else
                        ' No clue what it is
                        ' Message box and then exit
                        ErrorData.Description = ErrMsg & vbCrLf & " In: " & ex.Source & vbCrLf & " With: " & ex.Data.ToString & vbCrLf
                        ErrorData.Number = ErrorCode
                        Return Nothing
                    End If

                    ' If we error, that means one of the item list has errored. Probably a bad request for an item
                    ' that isn't in the EVE Central DB. If bad request (4xx error) then try and run it with 1 per batch and weed out the errors
                    ' If the TypeIDBatchCount isn't 1, then re-run
                    If ErrorCode = 4 Then
                        If TypeIDBatchCount <> 1 Then
                            Dim PartialTypeIDList As List(Of Long) = FormatPriceTypeIDList(EVECentralItemList)
                            ' Add all the records from this set that didn't error
                            PriceRecords.AddRange(GetPrices(Regions, System, PartialTypeIDList, 1))
                        Else
                            ' This has errored before and a single price, so set the price data to 0s and save it
                            TempRecord.TypeID = TypeIDList(i)
                            TempRecord.RegionList = DBRegionList
                            TempRecord.Errored = True

                            TempRecord.AllVolume = 0.0
                            TempRecord.AllAvgPrice = 0.0
                            TempRecord.AllMaxPrice = 0.0
                            TempRecord.AllMinPrice = 0.0
                            TempRecord.AllStdDev = 0.0
                            TempRecord.AllMedian = 0.0
                            TempRecord.AllPercentile = 0.0

                            TempRecord.BuyVolume = 0.0
                            TempRecord.BuyAvgPrice = 0.0
                            TempRecord.BuyMaxPrice = 0.0
                            TempRecord.BuyMinPrice = 0.0
                            TempRecord.BuyStdDev = 0.0
                            TempRecord.BuyMedian = 0.0
                            TempRecord.BuyPercentile = 0.0

                            TempRecord.SellVolume = 0.0
                            TempRecord.SellAvgPrice = 0.0
                            TempRecord.SellMaxPrice = 0.0
                            TempRecord.SellMinPrice = 0.0
                            TempRecord.SellStdDev = 0.0
                            TempRecord.SellMedian = 0.0
                            TempRecord.SellPercentile = 0.0

                            ' Add the record
                            PriceRecords.Add(TempRecord)

                            Application.DoEvents()

                        End If

                    ElseIf ErrorCode = 5 Then ' The server is down or something
                        ' Message box and then exit
                        ErrorData.Description = ex.Message
                        ErrorData.Number = ErrorCode
                        Return Nothing
                    End If

                End Try

                ' Reset item count
                ItemCount = 0
                ' Reset typeid's to query
                EVECentralItemList = ""

            End If
        Next

        Return PriceRecords

        Exit Function

    End Function

    ' Formats the partial typeid list from a string to array of longs
    Private Function FormatPriceTypeIDList(ByVal TypeIDList As String) As List(Of Long)
        Dim TypeIDListArray As String()
        Dim ReturnTypeIDList As New List(Of Long)
        Dim TempString As String

        If TypeIDList <> "" Then
            ' Separate by the &
            TypeIDListArray = TypeIDList.Split(New Char() {"&"c}, StringSplitOptions.RemoveEmptyEntries)
        Else
            ' Return empty list
            Return ReturnTypeIDList
        End If

        ' Convert to longs
        ' Find the end of the number and save - formatted as "typeid=" & TypeIDList(i) & "&"
        For i = 0 To TypeIDListArray.Count - 1
            TempString = TypeIDListArray(i).Replace("typeid=", "")
            If IsNumeric(TempString) Then
                ReturnTypeIDList.Add(CLng(TempString))
            End If
        Next

        Return ReturnTypeIDList

    End Function

    ' Allow the users to access the error data returned if an error occurs for processing outside class
    Public Function GetErrorData() As MyError
        Return ErrorData
    End Function

End Class

Public Structure EVECentralPrice
    Dim TypeID As Long
    Dim AllVolume As Double
    Dim AllAvgPrice As Double
    Dim AllMaxPrice As Double
    Dim AllMinPrice As Double
    Dim AllStdDev As Double
    Dim AllMedian As Double
    Dim AllPercentile As Double
    Dim BuyVolume As Double
    Dim BuyAvgPrice As Double
    Dim BuyMaxPrice As Double
    Dim BuyMinPrice As Double
    Dim BuyStdDev As Double
    Dim BuyMedian As Double
    Dim BuyPercentile As Double
    Dim SellVolume As Double
    Dim SellAvgPrice As Double
    Dim SellMaxPrice As Double
    Dim SellMinPrice As Double
    Dim SellStdDev As Double
    Dim SellMedian As Double
    Dim SellPercentile As Double
    Dim RegionList As String
    Dim Errored As Boolean
End Structure
