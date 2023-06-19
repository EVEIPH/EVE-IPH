
Imports Newtonsoft.Json

' Class for querying data from Fuzzworks prices

Public Class FuzzworksMarket

    Implements ICloneable

    Private ErrorData As MyError

    Private Structure PriceQuery
        Dim ItemList As String
        Dim Items As List(Of Long)
        Dim PriceLocation As String
        Dim RegionOrSystemHeader As String
    End Structure

    Private PriceLocationToFind As String

    ' Predicate for searching a list of pricequery
    Private Function FindPriceQuery(ByVal ItemPrice As PriceQuery) As Boolean
        If ItemPrice.PriceLocation = PriceLocationToFind Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Function takes an array of strings for Regions and a TypeID list, returns an array of prices from Fuzzworks
    Public Function GetPrices(ByVal TypeIDList As List(Of PriceItem)) As List(Of FuzzworksMarketPrice)
        Dim PriceRecords As New List(Of FuzzworksMarketPrice)
        Dim TempRecord As FuzzworksMarketPrice
        Dim PriceOutput As New Dictionary(Of Long, FuzzworksMarketType)
        Dim FuzzworksMarketMainQuery As String = "https://market.fuzzwork.co.uk/aggregates/?"

        Dim ProcessQueryList As New List(Of PriceQuery)
        Dim FinalQueryList As New List(Of PriceQuery)
        Dim FindPQ As New PriceQuery
        Dim InsertPQ As New PriceQuery

        Dim ItemListHeader As String = "types="
        Dim RegionHeader As String = "region="
        Dim SystemHeader As String = "system="
        Dim StationHeader As String = "station="
        Dim PriceLocationUsed As String = ""
        Dim PriceLocationHeaderUsed As String = ""

        ' Set up for each region/system and item combos to be queried
        For Each Item In TypeIDList
            ' Search the main query list for the region, if there add typeid to the list, else add new list 
            PriceLocationHeaderUsed = RegionHeader ' Always use region header for now
            If Item.SystemID = "" Then
                PriceLocationUsed = CStr(Item.RegionID)
            Else
                PriceLocationUsed = CStr(Item.SystemID)
            End If

            PriceLocationToFind = PriceLocationUsed
            FindPQ = ProcessQueryList.Find(AddressOf FindPriceQuery)
            If FindPQ.PriceLocation = "" Then
                ' Add it
                InsertPQ.PriceLocation = PriceLocationUsed
                InsertPQ.RegionOrSystemHeader = PriceLocationHeaderUsed
                InsertPQ.ItemList = ItemListHeader & CStr(Item.TypeID) & ","
                InsertPQ.Items = New List(Of Long)
                InsertPQ.Items.Add(Item.TypeID) ' for counting
                FindPQ = InsertPQ ' set them to the same now
                ProcessQueryList.Add(InsertPQ)
            Else
                ' Found, so update
                Call ProcessQueryList.Remove(FindPQ)
                FindPQ.ItemList &= CStr(Item.TypeID) & ","
                FindPQ.Items.Add(Item.TypeID)
                Call ProcessQueryList.Add(FindPQ)
            End If

            ' if Len(EVEMarketerMainQuery & ItemList & RegionOrSystem) > 1900 (will get a too long error at 2000) or over 100 items at a time
            If Len(FuzzworksMarketMainQuery & FindPQ.RegionOrSystemHeader & FindPQ.PriceLocation & FindPQ.ItemList) > 1900 Or FindPQ.Items.Count >= 100 Then
                Call ProcessQueryList.Remove(FindPQ) ' remove from process list
                ' Insert the item to the final list
                Call FinalQueryList.Add(FindPQ)
            End If
        Next

        ' Add whatever is left in the process lists
        Call FinalQueryList.AddRange(ProcessQueryList)

        PriceUpdateDown = False

        For Each Record In FinalQueryList
            Try
                ' Example get
                'https://market.fuzzwork.co.uk/aggregates/?region=10000002&types=34,35,36,37,38,39,40

                Dim Output As String = GetJSONFile(FuzzworksMarketMainQuery & Record.RegionOrSystemHeader & Record.PriceLocation & "&" & Record.ItemList.Substring(0, Len(Record.ItemList) - 1), "Fuzzwork Market Prices")
                ' Parse the out put into the object and process
                PriceOutput = JsonConvert.DeserializeObject(Of Dictionary(Of Long, FuzzworksMarketType))(Output)

                If PriceUpdateDown Then
                    Return PriceRecords
                End If

                If Not IsNothing(PriceOutput) Then
                    For Each Price In PriceOutput
                        With Price.Value
                            TempRecord = New FuzzworksMarketPrice
                            TempRecord.TypeID = Price.Key
                            TempRecord.PriceLocation = CLng(Record.PriceLocation)

                            TempRecord.BuyMaxPrice = .buy.max
                            TempRecord.BuyMedian = .buy.median
                            TempRecord.BuyMinPrice = .buy.min
                            TempRecord.BuyPercentile = .buy.percentile
                            TempRecord.BuyStdDev = .buy.stddev
                            TempRecord.BuyVolume = .buy.volume
                            TempRecord.BuyWeightedAveragePrice = .buy.weightedAverage

                            TempRecord.SellMaxPrice = .sell.max
                            TempRecord.SellMedian = .sell.median
                            TempRecord.SellMinPrice = .sell.min
                            TempRecord.SellPercentile = .sell.percentile
                            TempRecord.SellStdDev = .sell.stddev
                            TempRecord.SellVolume = .sell.volume
                            TempRecord.SellWeightedAveragePrice = .sell.weightedAverage
                        End With

                        ' Add the record
                        PriceRecords.Add(TempRecord)

                        Application.DoEvents()
                    Next
                End If

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
                ' that isn't in the EVE Marketer DB. If bad request (4xx error) then try and run it with 1 per batch and weed out the errors
                ' If the TypeIDBatchCount isn't 1, then re-run
                If ErrorCode = 4 Then
                    ' Message box and then exit
                    ErrorData.Description = ex.Message
                    ErrorData.Number = ErrorCode
                    Return Nothing
                ElseIf ErrorCode = 5 Then ' The server is down or something
                    ' Message box and then exit
                    ErrorData.Description = ex.Message
                    ErrorData.Number = ErrorCode
                    Return Nothing
                End If

            End Try

        Next

        Return PriceRecords

        Exit Function

    End Function

    ' Allow the users to access the error data returned if an error occurs for processing outside class
    Public Function GetErrorData() As MyError
        Return ErrorData
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class

Public Structure FuzzworksMarketPrice
    Dim TypeID As Long
    Dim PriceLocation As Long
    Dim BuyVolume As Double
    Dim BuyWeightedAveragePrice As Double
    Dim BuyMaxPrice As Double
    Dim BuyMinPrice As Double
    Dim BuyStdDev As Double
    Dim BuyMedian As Double
    Dim BuyPercentile As Double
    Dim SellVolume As Double
    Dim SellWeightedAveragePrice As Double
    Dim SellMaxPrice As Double
    Dim SellMinPrice As Double
    Dim SellStdDev As Double
    Dim SellMedian As Double
    Dim SellPercentile As Double
End Structure

Public Class FuzzworksMarketTypeStat
    <JsonProperty("weightedAverage")> Public weightedAverage As Double
    <JsonProperty("max")> Public max As Double
    <JsonProperty("min")> Public min As Double
    <JsonProperty("stddev")> Public stddev As Double
    <JsonProperty("median")> Public median As Double
    <JsonProperty("volume")> Public volume As Double
    <JsonProperty("orderCount")> Public orderCount As Double
    <JsonProperty("percentile")> Public percentile As Double
End Class

Public Class FuzzworksMarketType
    <JsonProperty("buy")> Public buy As FuzzworksMarketTypeStat
    <JsonProperty("sell")> Public sell As FuzzworksMarketTypeStat
End Class