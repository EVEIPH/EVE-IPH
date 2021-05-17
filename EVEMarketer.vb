
Imports Newtonsoft.Json

' Class for querying data from EVE Markterer prices

Public Class EVEMarketer
    Private ErrorData As MyError

    Private Structure PriceQuery
        Dim ItemList As String
        Dim RegionOrSystem As String
    End Structure

    ' Function takes an array of strings for Regions and a TypeID list, returns an array of EVE Marketeer prices
    Public Function GetPrices(ByVal TypeIDList As List(Of Long), RegionID As Integer, Optional SystemID As Integer = 0, Optional TypeIDBatchCount As Integer = 100) As List(Of EVEMarketerPrice)
        Dim PriceRecords As New List(Of EVEMarketerPrice)
        Dim TempRecord As EVEMarketerPrice
        Dim PriceOutput As New List(Of EMType)
        Dim EVEMarketerMainQuery As String = "https://api.evemarketer.com/ec/marketstat/json?"

        Dim QueryTypeIDList As New List(Of PriceQuery)
        Dim TempPQ As New PriceQuery
        Dim ItemCount As Integer = 0 ' For breaking it into sets of 100 for query

        Dim EVECentralItem As String = ""
        Dim ItemListHeader As String = "typeid="
        Dim RegionHeader As String = "&regionlimit="
        Dim SystemHeader As String = "&usesystem="
        Dim RegionOrSystem As String = ""
        Dim RegionOrSystemUsed As Integer = 0

        If SystemID = 0 Then
            RegionOrSystem = RegionHeader & CStr(RegionID)
            RegionOrSystemUsed = RegionID
        Else
            RegionOrSystem = SystemHeader & CStr(SystemID)
            RegionOrSystemUsed = SystemID
        End If

        TempPQ.RegionOrSystem = RegionOrSystem

        ' Query each set of 100 prices at time. So build query of 100 items or if Len(EVEMarketerMainQuery & ItemList & RegionOrSystem) > 1900 (will get a too long error at 2000)
        For Each ID In TypeIDList
            ItemCount += 1
            TempPQ.ItemList &= CStr(ID) & ","

            If ItemCount = TypeIDBatchCount Or ItemCount = TypeIDList.Count Or
                Len(EVEMarketerMainQuery & TempPQ.ItemList & RegionOrSystem) > 1900 Then
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
                'https://api.evemarketer.com/ec/marketstat/json?typeid=34,35&usesystem=30002659

                Dim Output As String = GetJSONFile(EVEMarketerMainQuery & Record.ItemList & Record.RegionOrSystem, "EVE Marketer Prices")
                ' Parse the out put into the object and process
                PriceOutput = JsonConvert.DeserializeObject(Of List(Of EMType))(Output)

                If Not IsNothing(PriceOutput) Then
                    For Each Price In PriceOutput
                        With Price
                            TempRecord.TypeID = .buy.forQuery.types(0)
                            TempRecord.RegionOrSystem = CStr(RegionOrSystemUsed)
                            TempRecord.BuyAvgPrice = .buy.avg
                            TempRecord.BuyMaxPrice = .buy.max
                            TempRecord.BuyMedian = .buy.median
                            TempRecord.BuyMinPrice = .buy.min
                            TempRecord.BuyPercentile = .buy.fivePercent
                            TempRecord.BuyStdDev = .buy.stdDev
                            TempRecord.BuyVolume = .buy.volume
                            TempRecord.BuyWeightedAveragePrice = .buy.wavg
                            TempRecord.BuyVariance = .buy.variance

                            TempRecord.SellAvgPrice = .sell.avg
                            TempRecord.SellMaxPrice = .sell.max
                            TempRecord.SellMedian = .sell.median
                            TempRecord.SellMinPrice = .sell.min
                            TempRecord.SellPercentile = .sell.fivePercent
                            TempRecord.SellStdDev = .sell.stdDev
                            TempRecord.SellVolume = .sell.volume
                            TempRecord.SellWeightedAveragePrice = .sell.wavg
                            TempRecord.SellVariance = .sell.variance
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

Public Structure EVEMarketerPrice
    Dim TypeID As Long
    Dim BuyVolume As Double
    Dim BuyWeightedAveragePrice As Double
    Dim BuyAvgPrice As Double
    Dim BuyMaxPrice As Double
    Dim BuyMinPrice As Double
    Dim BuyStdDev As Double
    Dim BuyMedian As Double
    Dim BuyPercentile As Double
    Dim BuyVariance As Double
    Dim SellVolume As Double
    Dim SellWeightedAveragePrice As Double
    Dim SellAvgPrice As Double
    Dim SellMaxPrice As Double
    Dim SellMinPrice As Double
    Dim SellStdDev As Double
    Dim SellMedian As Double
    Dim SellPercentile As Double
    Dim SellVariance As Double
    Dim RegionOrSystem As String
    Dim Errored As Boolean
End Structure

Public Class PriceItem
    Public TypeID As Long
    Public Manufacture As Boolean
    Public RegionID As String
    Public SystemID As String
    Public StructureID As String
    Public GroupName As String
    Public PriceModifier As Double
    Public PriceType As String
    Public JitaPerimeterPrice As Boolean
End Class

Public Class EMforQuery
    <JsonProperty("bid")> Public bid As String
    <JsonProperty("types")> Public types As List(Of Integer)
    <JsonProperty("regions")> Public regions As List(Of Integer)
    <JsonProperty("systems")> Public systems As List(Of Integer)
    <JsonProperty("hours")> Public hours As Integer
    <JsonProperty("minq")> Public minq As Integer
End Class

Public Class EMTypeStat
    <JsonProperty("forQuery")> Public forQuery As EMforQuery
    <JsonProperty("volume")> Public volume As Long
    <JsonProperty("wavg")> Public wavg As Double
    <JsonProperty("avg")> Public avg As Double
    <JsonProperty("min")> Public min As Double
    <JsonProperty("max")> Public max As Double
    <JsonProperty("variance")> Public variance As Double
    <JsonProperty("stdDev")> Public stdDev As Double
    <JsonProperty("median")> Public median As Double
    <JsonProperty("fivePercent")> Public fivePercent As Double
    <JsonProperty("highToLow")> Public highToLow As Boolean
    <JsonProperty("generated")> Public generated As Long
End Class

Public Class EMType
    <JsonProperty("buy")> Public buy As EMTypeStat
    <JsonProperty("sell")> Public sell As EMTypeStat
End Class