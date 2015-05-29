
Imports System.Xml

Public Class EVEMarketeerAPI

    ' API Path
    Private Const MainURL As String = "http://www.evemarketeer.com/api/averages"
    Private Const QueryInterval As Integer = 25
    '   Parameters 
    '   region_id	: the region for which this data applies
    '   days		: the amount of days over which you would like averages 
    '			    (default: 30, possible: 30, 60, 90, 180, 360)
    '   type_id		: the type id(s) for which this data applies (default: 
    '			    csv, possible: csv, xml, json)
    '   Example:    (25 items per call)
    '   http://www.evemarketeer.com/api/averages/10000043/90/34_35_36/xml

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
    Public Function GetPriceAverages(SentTypeIDs As List(Of Long), RegionID As Long, Days As Integer) As List(Of MarketeerItemPriceAverage)
        Dim ReturnList As New List(Of MarketeerItemPriceAverage)
        Dim TempItem As MarketeerItemPriceAverage
        Dim i As Integer

        ' XML variables
        Dim m_xmld As XmlDocument
        ' For the main Type ID nodes
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim sub_nodelist As XmlNodeList
        Dim sub_node As XmlNode

        Dim ParameterString As String = ""
        Dim TypeList As String = ""
        Dim TotalItems As Integer
        Dim ItemCount As Integer = 0 ' max of 25
        Dim AverageCount As Integer = 0

        ' Only 30, 60, 90, 180, 360 days possible
        Select Case Days
            Case 30, 60, 90, 180, 360
                ' Good to go
            Case Else
                APIError.ErrorText = "Wrong date range: Must be 30,60,90,180,360"
                APIError.ErrorCode = -1
                Return ReturnList
        End Select

        If SentTypeIDs.Count = 0 Then
            APIError.ErrorText = "No type ID's sent"
            APIError.ErrorCode = -2
            Return ReturnList
        Else
            TotalItems = SentTypeIDs.Count
        End If

        ParameterString = "/" & CStr(RegionID) & "/" & CStr(Days) & "/"

        For i = 0 To TotalItems - 1

            ' Count each for 100 check
            ItemCount = ItemCount + 1

            ' Add the typenumber
            TypeList = TypeList & CStr(SentTypeIDs(i)) & "_"

            ' See if we are at 100 or at the end, if so - query EC and insert records
            If ItemCount = QueryInterval Or (i = TotalItems - 1) Then
                ' Query EVE Marketeer
                ' Create the XML Document
                m_xmld = New XmlDocument

                Try

                    Dim request = System.Net.HttpWebRequest.Create(MainURL & ParameterString & TypeList.Substring(0, Len(TypeList) - 1) & "/xml")
                    request.Timeout = 500
                    request.Proxy = Nothing
                    request.Method = "GET"

                    Dim response = request.GetResponse()

                    m_xmld.Load(response.GetResponseStream())

                    ' Load the Xml file
                    m_xmld.Load(MainURL & ParameterString & TypeList.Substring(0, Len(TypeList) - 1) & "/xml")

                    ' Set the result node
                    m_nodelist = m_xmld.SelectNodes("/result")

                    ' Loop through each row
                    For Each m_node In m_nodelist
                        ' Set the child nodes for each row to get the prices (/result/row)
                        sub_nodelist = m_node.ChildNodes

                        ' Loop through 3 nodes and get values that are returned
                        For Each sub_node In sub_nodelist
                            TempItem = New MarketeerItemPriceAverage
                            TempItem.typeID = CLng(FormatNumberData(sub_node.ChildNodes.Item(0).InnerText))
                            TempItem.days = CInt(FormatNumberData(sub_node.ChildNodes.Item(1).InnerText))
                            TempItem.regionID = CLng(FormatNumberData(sub_node.ChildNodes.Item(2).InnerText))
                            TempItem.average = FormatNumberData(sub_node.ChildNodes.Item(3).InnerText)
                            TempItem.maximum = FormatNumberData(sub_node.ChildNodes.Item(4).InnerText)
                            TempItem.minimum = FormatNumberData(sub_node.ChildNodes.Item(5).InnerText)
                            TempItem.average_price = FormatNumberData(sub_node.ChildNodes.Item(6).InnerText)
                            TempItem.maximum_price = FormatNumberData(sub_node.ChildNodes.Item(7).InnerText)
                            TempItem.minimum_price = FormatNumberData(sub_node.ChildNodes.Item(8).InnerText)
                            TempItem.est_spct = FormatNumberData(sub_node.ChildNodes.Item(9).InnerText)
                            TempItem.est_bpct = FormatNumberData(sub_node.ChildNodes.Item(10).InnerText)
                            TempItem.last_updated = Now
                            ' Insert into list
                            ReturnList.Add(TempItem)
                        Next

                        Application.DoEvents()
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

    ' Formats the data to a number if it comes in as "null"
    Private Function FormatNumberData(InData As String) As Object
        If InData = "null" Then
            Return "null"
        Else
            Return InData
        End If
    End Function

End Class

Public Structure MarketeerItemPriceAverage
    Dim typeID As Long  ' the type id for which this data applies
    Dim days As Integer     ' the amount of days over which this data is the average
    Dim regionID As Long  ' the region for which this data applies
    Dim average As Object   ' average transactions a day over this time
    Dim maximum As Object    ' maximum transactions a day over this time
    Dim minimum As Object    ' minimum transactions a day over this time
    Dim average_price As Object ' average price over this time
    Dim maximum_price As Object  ' maximum price over this time
    Dim minimum_price As Object  ' minimum price over this time
    Dim est_spct As Object       ' estimated percentage of transactions that took place on sell orders
    Dim est_bpct As Object       ' estimated percentage of transactions that took place  on buy orders
    Dim last_updated As Date    ' Date this price was queried
End Structure