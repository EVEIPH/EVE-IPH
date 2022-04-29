
Imports Newtonsoft.Json

' Class for querying data from EVE Markterer prices

Public Class FuzzworksMarket

    Private ErrorData As MyError

    Private Structure PriceQuery
        Dim ItemList As String
        Dim RegionOrSystem As String
    End Structure

    ' Function takes an array of strings for Regions and a TypeID list, returns an array of EVE Marketeer prices
    Public Function GetPrices(ByVal TypeIDList As List(Of Long), RegionID As Integer, Optional SystemID As Integer = 0, Optional TypeIDBatchCount As Integer = 100) As List(Of FuzzworksMarketPrice)
        Dim PriceRecords As New List(Of FuzzworksMarketPrice)
        Dim TempRecord As FuzzworksMarketPrice
        Dim PriceOutput As New Dictionary(Of Long, FuzzworksMarketType)
        Dim FuzzworksMarketMainQuery As String = "https://market.fuzzwork.co.uk/aggregates/?"

        Dim QueryTypeIDList As New List(Of PriceQuery)
        Dim TempPQ As New PriceQuery
        Dim ItemCount As Integer = 0 ' For breaking it into sets of 100 for query

        Dim ItemListHeader As String = "types="
        Dim RegionHeader As String = "region="
        Dim SystemHeader As String = "system="
        Dim StationHeader As String = "station="
        Dim RegionOrSystem As String = ""
        Dim PriceLocationUsed As Integer = 0

        Dim TempSystemID As Integer = 0

        If SystemID = 0 Then
            RegionOrSystem = RegionHeader & CStr(RegionID)
            PriceLocationUsed = RegionID
        Else
            ' Use station ID's for the hub systems except Perimeter
            Select Case SystemID
                Case 30002187 ' Amarr
                    TempSystemID = 60008494
                    RegionOrSystem = StationHeader & CStr(TempSystemID)
                Case 30002659 ' Dodixie
                    TempSystemID = 60011866
                    RegionOrSystem = StationHeader & CStr(TempSystemID)
                Case 30002053 ' Hek
                    TempSystemID = 60005686
                    RegionOrSystem = StationHeader & CStr(TempSystemID)
                Case 30002510 ' Rens
                    TempSystemID = 60004588
                    RegionOrSystem = StationHeader & CStr(TempSystemID)
                Case 30000142 ' Jita
                    TempSystemID = 60003760
                    RegionOrSystem = StationHeader & CStr(TempSystemID)
                Case Else
                    RegionOrSystem = SystemHeader & CStr(SystemID) ' Perimeter - no others should come in as I disabled the system combo
            End Select
            PriceLocationUsed = SystemID
        End If

        TempPQ.RegionOrSystem = RegionOrSystem

        ' Query each set of 100 prices at time. So build query of 100 items or if Len(FuzzworksMarketMainQuery & RegionOrSystem & "&" & TempPQ.ItemList) > 1900 (will get a too long error at 2000)
        For Each ID In TypeIDList
            ItemCount += 1
            TempPQ.ItemList &= CStr(ID) & ","

            If ItemCount = TypeIDBatchCount Or ItemCount = TypeIDList.Count Or
                Len(FuzzworksMarketMainQuery & RegionOrSystem & "&" & TempPQ.ItemList) > 1900 Then
                ' Insert the item
                ' Add the header to the item list
                TempPQ.ItemList = ItemListHeader & TempPQ.ItemList
                ' Strip comma
                TempPQ.ItemList = TempPQ.ItemList.Substring(0, Len(TempPQ.ItemList) - 1)
                QueryTypeIDList.Add(TempPQ)
                TempPQ.ItemList = ""

                ItemCount = 0
            End If
        Next

        For Each Record In QueryTypeIDList
            Try
                ' Example get
                'https://market.fuzzwork.co.uk/aggregates/?region=10000002&types=34,35,36,37,38,39,40

                Dim Output As String = GetJSONFile(FuzzworksMarketMainQuery & Record.RegionOrSystem & "&" & Record.ItemList, "Fuzzwork Market Prices")
                ' Parse the out put into the object and process
                PriceOutput = JsonConvert.DeserializeObject(Of Dictionary(Of Long, FuzzworksMarketType))(Output)

                If Not IsNothing(PriceOutput) Then
                    For Each Price In PriceOutput
                        With Price.Value
                            TempRecord.TypeID = Price.Key
                            TempRecord.PriceLocation = CStr(PriceLocationUsed)

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

End Class

Public Structure FuzzworksMarketPrice
    Dim TypeID As Long
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
    Dim PriceLocation As String
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