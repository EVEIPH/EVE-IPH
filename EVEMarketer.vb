
Imports Newtonsoft.Json

' Class for querying data from EVE Markterer prices

Public Class EVEMarketer

    Implements ICloneable

    Private ErrorData As MyError

    Private Structure PriceQuery
        Dim ItemList As String
        Dim Items As List(Of Long)
        Dim PriceLocation As String
        Dim PriceLocationHeader As String
    End Structure

    Private RegionOrSystemToFind As String

    ' Predicate for searching a list of pricequery
    Private Function FindPriceQuery(ByVal ItemPrice As PriceQuery) As Boolean
        If ItemPrice.PriceLocation = RegionOrSystemToFind Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Function takes an array of strings for Regions and a TypeID list, returns an array of EVE Marketeer prices
    Public Function GetPrices(ByVal TypeIDList As List(Of PriceItem)) As List(Of EVEMarketerPrice)
        Dim PriceRecords As New List(Of EVEMarketerPrice)
        Dim TempRecord As EVEMarketerPrice
        Dim PriceOutput As New List(Of EMType)
        Dim EVEMarketerMainQuery As String = "https://api.evemarketer.com/ec/marketstat/json?"

        Dim ProcessQueryList As New List(Of PriceQuery)
        Dim FinalQueryList As New List(Of PriceQuery)
        Dim FindPQ As New PriceQuery
        Dim InsertPQ As New PriceQuery

        Dim ItemListHeader As String = "typeid="
        Dim RegionHeader As String = "&regionlimit="
        Dim SystemHeader As String = "&usesystem="
        Dim RegionSystemUsed As String = ""
        Dim PriceLocationHeaderUsed As String = ""

        ' Set up for each region/system and item combos to be queried
        For Each Item In TypeIDList
            ' Search the main query list for the region, if there add typeid to the list, else add new list 
            If Item.SystemID = "" Then
                RegionSystemUsed = CStr(Item.RegionID)
                PriceLocationHeaderUsed = RegionHeader
            Else
                RegionSystemUsed = CStr(Item.SystemID)
                PriceLocationHeaderUsed = SystemHeader
            End If

            RegionOrSystemToFind = RegionSystemUsed
            FindPQ = ProcessQueryList.Find(AddressOf FindPriceQuery)
            If FindPQ.PriceLocation = "" Then
                ' Add it
                InsertPQ.PriceLocation = RegionSystemUsed
                InsertPQ.PriceLocationHeader = PriceLocationHeaderUsed
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
            If Len(EVEMarketerMainQuery & FindPQ.ItemList & FindPQ.PriceLocationHeader & FindPQ.PriceLocation) > 1900 Or FindPQ.Items.Count >= 100 Then
                Call ProcessQueryList.Remove(FindPQ) ' remove from process list
                ' Insert the item to the final list
                Call FinalQueryList.Add(FindPQ)
            End If
        Next

        ' Add whatever is left in the process lists
        Call FinalQueryList.AddRange(ProcessQueryList)

        For Each Record In FinalQueryList
            Try
                ' Example get
                'https://api.evemarketer.com/ec/marketstat/json?typeid=34,35&usesystem=30002659

                Dim Output As String = GetJSONFile(EVEMarketerMainQuery & Record.ItemList.Substring(0, Len(Record.ItemList) - 1) & Record.PriceLocationHeader & Record.PriceLocation, "EVE Marketer Prices")
                ' Parse the out put into the object and process
                PriceOutput = JsonConvert.DeserializeObject(Of List(Of EMType))(Output)

                If Not IsNothing(PriceOutput) Then
                    For Each Price In PriceOutput
                        With Price
                            TempRecord = New EVEMarketerPrice
                            TempRecord.TypeID = .buy.forQuery.types(0)
                            TempRecord.PriceLocation = CLng(Record.PriceLocation)
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

    End Function

    ' Allow the users to access the error data returned if an error occurs for processing outside class
    Public Function GetErrorData() As MyError
        Return ErrorData
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
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
    Dim PriceLocation As Long
    Dim Errored As Boolean
End Structure

Public Class PriceItem
    Implements ICloneable

    Public TypeID As Integer
    Public Manufacture As Boolean
    Public RegionID As String
    Public SystemID As String
    Public GroupName As String
    Public PriceModifier As Double
    Public PriceType As String
    Public JitaPerimeterPrice As Boolean

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New PriceItem

        CopyOfMe.TypeID = TypeID
        CopyOfMe.Manufacture = Manufacture
        CopyOfMe.RegionID = RegionID
        CopyOfMe.SystemID = SystemID
        CopyOfMe.GroupName = GroupName
        CopyOfMe.PriceModifier = PriceModifier
        CopyOfMe.PriceType = PriceType
        CopyOfMe.JitaPerimeterPrice = JitaPerimeterPrice

        Return CopyOfMe

    End Function
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