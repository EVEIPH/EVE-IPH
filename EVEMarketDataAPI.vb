
Imports System.Xml

Public Class EVEMarketDataAPI
    ' API Path
    Private Const MainURL As String = "http://api.eve-marketdata.com/api/item_history2.xml?char_name=EVEIPH"
    Private Const QueryInterval As Integer = 25

    ' API Methods - Item Price History
    ' This is the exact same data that you see in the Market History tab in the game

    ' Parameters:
    ' char_name - required Your character name, so that I can contact you in case something goes wrong
    ' type_ids - Which typeID's to check
    ' marketgroup_ids - Which marketgroup_id's to check
    ' region_ids - Which regionIDs's to check
    ' days - How many days back to show data for (< 30 days is MUCH faster) defaults to 30 days
    ' callback - The JSONP callback function (optional, only needed for JSONP)

    ' At least one from type_ids, marketgroup_ids, or region_ids is required.
    ' a max of 10,000 rows will be returned

    ' Return Values:
    ' typeID - eve_inv_types.typeID
    ' regionID - eve_map_regions.regionIDs
    ' date - date this is for, yyyy-mm-dd
    ' lowPrice - low price (from eve)
    ' highPrice - high price (from eve)
    ' avgPprice - average price (from eve)
    ' volume - # of units sold
    ' orders - # of orders

    Private APIError As ErrorData

    Public Sub New()
        APIError.ErrorText = ""
        APIError.ErrorCode = 0
    End Sub

    Private Structure ErrorData
        Dim ErrorCode As Integer
        Dim ErrorText As String
    End Structure

    Public Function GetErrorCode() As Integer
        Return APIError.ErrorCode
    End Function

    Public Function GetErrorText() As String
        Return APIError.ErrorText
    End Function

    ' Takes the type ID's sent and queries all the averages, returns an array of item price averages 
    Public Function GetPriceAverages(SentTypeIDs As List(Of Long), RegionID As Long, Days As Integer) As List(Of EVEMarketDataPriceAverage)
        Dim ReturnList As New List(Of EVEMarketDataPriceAverage)
        Dim TempItem As EVEMarketDataPriceAverage
        Dim i As Integer

        ' XML variables
        Dim m_xmld As XmlDocument
        ' For the main Type ID nodes
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim TempDateString As String = ""
        Dim UpdatedDate As Date

        Dim ParameterString As String = ""
        Dim TypeList As String = ""
        Dim TotalItems As Integer
        Dim ItemCount As Integer = 0 ' max of 25
        Dim AverageCount As Integer = 0

        ' Only allow 7 to 90 days for prices (limit of 90 days * 100 items each query will keep under the 10k row limit for API)
        If Days < 7 Or Days > 90 Then
            APIError.ErrorText = "Date range must be greater than 7 and less than 90 days"
            APIError.ErrorCode = -1
            Return ReturnList
        End If

        If SentTypeIDs.Count = 0 Then
            APIError.ErrorText = "No type ID's sent"
            APIError.ErrorCode = -2
            Return ReturnList
        Else
            TotalItems = SentTypeIDs.Count
        End If

        ' Parameters:
        ' type_ids - Which typeID's to check, separated by commas
        ' region_ids - Which regionIDs's to check
        ' days - How many days back to show data for (< 30 days is MUCH faster) defaults to 30 days

        ' Example:
        ' http://api.eve-marketdata.com/api/item_history2.xml?char_name=demo&type_ids=34,456,35,38,42&region_ids=10000002&days=5
        ParameterString = "&region_ids=" & CStr(RegionID) & "&days=" & CStr(Days)

        For i = 0 To TotalItems - 1

            ' Count each for 100 check
            ItemCount = ItemCount + 1

            ' Add the typenumber
            TypeList = TypeList & CStr(SentTypeIDs(i)) & ","

            ' See if we are at 100 or at the end, if so - query and insert records
            If ItemCount = QueryInterval Or (i = TotalItems - 1) Then
                ' Query EVE Market Data

                ' Strip the last typelist comma and add header
                TypeList = "&type_ids=" & TypeList.Substring(0, Len(TypeList) - 1)
                UpdatedDate = Now

                ' Create the XML Document
                m_xmld = New XmlDocument

                Try

                    ' Load the Xml file
                    m_xmld.Load(MainURL & TypeList & ParameterString)

                    ' Set the rowset node
                    m_nodelist = m_xmld.SelectNodes("/emd/result/rowset/row")

                    ' after result is the rowset
                    ' <rowset name="history" key="typeID,regionID,date" columns="typeID,regionID,date,lowPrice,highPrice,avgPrice,volume,orders"></rowset>

                    ' Loop through each row and save data
                    For Each m_node In m_nodelist
                        TempItem = New EVEMarketDataPriceAverage
                        TempItem.typeID = CLng(m_node.Attributes(0).InnerText)
                        TempItem.regionID = CLng(m_node.Attributes(1).InnerText)
                        TempItem.pricedate = CDate(m_node.Attributes(2).InnerText) 'yyyy-mm-dd
                        TempItem.lowPrice = CDbl(m_node.Attributes(3).InnerText)
                        TempItem.highPrice = CDbl(m_node.Attributes(4).InnerText)
                        TempItem.avgPrice = CDbl(m_node.Attributes(5).InnerText)
                        TempItem.volume = CLng(m_node.Attributes(6).InnerText)
                        TempItem.orders = CLng(m_node.Attributes(7).InnerText)
                        TempItem.UpdateRan = UpdatedDate

                        ' Insert into list
                        ReturnList.Add(TempItem)

                        Application.DoEvents()
                        'Next
                    Next

                Catch ex As Exception

                    ' If we had an error, just save the err number and continue to try the rest of the items if possible
                    APIError.ErrorCode = Err.Number
                    APIError.ErrorText = Err.Description
                End Try

                ' Reset item count
                ItemCount = 0
                ' Reset typeid's to query
                TypeList = ""

            End If

        Next

        Return ReturnList

    End Function

End Class

Public Structure EVEMarketDataPriceAverage

    Dim typeID As Long  ' the type id for which this data applies
    Dim regionID As Long  ' the region for which this data applies
    Dim pricedate As Date ' Date of the data
    Dim lowPrice As Double ' lowest price from EVE
    Dim highPrice As Double ' highest price from EVE
    Dim avgPrice As Double ' average price from EVE
    Dim volume As Long ' # of units sold per day (rowset line)
    Dim orders As Long ' # of orders on that day (rowset line)
    Dim UpdateRan As Date ' Date the update record was queried

End Structure
