Imports System.Data.SQLite

Class Reprocessing

    Dim Reprocessing As Integer
    Dim ReprocessingEfficiency As Integer
    Dim ScrapMetalProcessing As Integer
    Dim ImplantBonus As Double
    Dim StationEquipment As Double
    Dim StationTax As Double
    Dim StationStanding As Double
    Dim Accounting As Integer

    Dim StationTaxes As Double

    ' Notes:
    ' Reprocessing Rate for Ore & Ice (including Compressed)
    ' rate = facilityModifier * (1 + 0.03 * ReprocessingLevel) * (1 + 0.02 * ReprocessingEfficiencyLevel)* (1 + 0.02 * OreSpecificSkillLevel) * implantModifier * (1 − StationTax)
    ' The facilityModifier is 0.5 for most NPC stations, 0.52 for Reprocessing Arrays anchorable in highsec, 0.54 for Reprocessing Arrays anchorable in lowsec/nullsec and 0.50 to 0.60 in nullsec Outposts. 
    ' The implantModifier is 1.01, 1.02 and 1.04 for RX-801, RX-802 and RX-804 respectively.

    ' Reprocessing Rate for everything else (including unrefined Alchemy products)
    ' rate = facilityModifier * (1 + 0.02 * ScrapMetalProcessingLevel) * (1 − StationTax)

    ' The facilityModifier is 0.5 for most NPC stations as well as for all nullsec outposts. There are no anchorable arrays for this activity.
    ' The reprocessing output is obtained by multiplying the reprocessing rate with the base material amounts and then rounding down (POS) or rounding to next integer (Station).

    ' NPC Station Tax for Reprocessing 
    ' StationTax = 5.0% − (0.75% * YourCorporationStanding) - You need 5%/0.75% = 6.67 Standing to pay no Station Tax.

    Public Sub New(ByVal ReprocessingSkill As Integer, ByVal ReprocessingEfficiencySkill As Integer,
                   ByVal ScrapMetalProcessingSkill As Integer, ByVal ReprocessingImplantBonus As Double,
                   ByVal UserStationEquipment As Double, ByVal UserStationTaxRate As Double, ByVal UserStationStanding As Double)

        ' Save all the variables
        Reprocessing = ReprocessingSkill
        ReprocessingEfficiency = ReprocessingEfficiencySkill
        ScrapMetalProcessing = ScrapMetalProcessingSkill

        ImplantBonus = ReprocessingImplantBonus

        StationEquipment = UserStationEquipment
        StationTax = UserStationTaxRate
        StationStanding = UserStationStanding

        ' NPC Station Tax for Reprocessing 
        ' StationTax = 5.0% − (0.75% * YourCorporationStanding) - You need 5%/0.75% = 6.67 Standing to pay no Station Tax.
        StationTaxes = StationTax - (0.0075 * StationStanding)

        If StationTaxes < 0 Then
            StationTaxes = 0
        End If

        ' Make sure it's multipliable
        StationTaxes = 1 - StationTaxes

    End Sub

    ' Returns a material list that would result from reprocessing the Product size of the item 
    Public Function ReprocessMaterial(ByVal ItemID As Long, ByVal BatchSize As Integer, ByVal NumItems As Long,
                                      ByVal IncludeTax As Boolean, ByVal BrokerFeeData As BrokerFeeInfo, ByVal RecursiveReprocess As Boolean) As Materials
        Dim SQL As String
        Dim readerBP As SQLiteDataReader
        Dim readerReprocess As SQLiteDataReader

        Dim TempMaterial As Material
        Dim TempMaterials As New Materials
        Dim RecursionMaterials As New Materials
        Dim i As Integer
        Dim RefinedMatId As Long
        Dim AdjustedStationYield As Double
        Dim TotalBatches As Integer
        Dim NewMaterialQuantity As Long

        ' Open up the Items Material view with this BP's Product ID (T1 Component)
        SQL = "SELECT REPROCESSING.REFINED_MATERIAL_ID, REPROCESSING.REFINED_MATERIAL, REPROCESSING.REFINED_MATERIAL_GROUP, "
        SQL = SQL & "REPROCESSING.REFINED_MATERIAL_VOLUME, REPROCESSING.REFINED_MATERIAL_QUANTITY, ITEM_PRICES.PRICE, ITEM_PRICES.ADJUSTED_PRICE "
        SQL = SQL & "FROM REPROCESSING, ITEM_PRICES "
        SQL = SQL & "WHERE REPROCESSING.ITEM_ID= " & ItemID & " "
        SQL = SQL & "AND REPROCESSING.REFINED_MATERIAL_ID = ITEM_PRICES.ITEM_ID "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBP = DBCommand.ExecuteReader

        ' Add all the materials 
        While readerBP.Read
            ' Reprocessing Rate for everything else (including unrefined Alchemy products)
            ' rate = facilityModifier * (1 + 0.02 * ScrapMetalProcessingLevel)
            AdjustedStationYield = StationEquipment * (1 + (0.02 * ScrapMetalProcessing))

            If AdjustedStationYield > 1 Then
                AdjustedStationYield = 1
            End If

            RefinedMatId = CLng(readerBP.GetValue(0))

            TotalBatches = CInt(Math.Round(Math.Ceiling(NumItems / BatchSize), 0))
            NewMaterialQuantity = CLng(Math.Round(CLng(readerBP.GetValue(4)) * TotalBatches * AdjustedStationYield, 0))

            If RecursiveReprocess Then
                ' If this mat is a raw mat, just add it, else call this function again and get total mats from reprocessing the item
                SQL = "SELECT 'X' FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID = " & RefinedMatId

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerReprocess = DBCommand.ExecuteReader

                If readerReprocess.HasRows Then
                    RecursionMaterials = ReprocessMaterial(RefinedMatId, TotalBatches, NewMaterialQuantity, IncludeTax, BrokerFeeData, RecursiveReprocess)

                    ' If there are recursion mats returned, add items to new list
                    If Not IsNothing(RecursionMaterials.GetMaterialList) Then
                        ' Insert each returned material from the recursion into this list
                        For i = 0 To RecursionMaterials.GetMaterialList.Count - 1
                            TempMaterials.InsertMaterial(RecursionMaterials.GetMaterialList(i))
                        Next
                    End If
                End If

                readerReprocess.Close()
                readerReprocess = Nothing

            End If

            If RecursionMaterials.GetMaterialList.Count = 0 Then
                ' Add the base material if not inserted as recusive mat
                TempMaterial = New Material(RefinedMatId, readerBP.GetString(1), readerBP.GetString(2),
                                                NewMaterialQuantity, readerBP.GetDouble(3), If(readerBP.IsDBNull(5), 0, readerBP.GetDouble(5)), "", "")
                TempMaterials.InsertMaterial(TempMaterial)
            End If

        End While

        readerBP.Close()
        readerBP = Nothing
        DBCommand = Nothing

        ' Apply taxes and brokers fees to this total if set
        If IncludeTax Then
            TempMaterials.AdjustTaxedPrice(GetSalesTax(TempMaterials.GetTotalMaterialsCost))
        End If

        ' Broker fee
        TempMaterials.AdjustTaxedPrice(GetSalesBrokerFee(TempMaterials.GetTotalMaterialsCost, BrokerFeeData))

        Return TempMaterials

    End Function

    Public Function ReprocessORE(ByVal OreID As Long, ByVal OreProcessingSkill As Integer, ByVal TotalOre As Double,
                              ByVal IncludeTax As Boolean, ByVal BrokerFeeData As BrokerFeeInfo, ByRef TotalYield As Double) As Materials
        Dim RefineBatches As Long ' Number of batches of refine units we can refine from total

        Dim SQL As String
        Dim readerRefine As SQLiteDataReader

        Dim RefinedMats As New Materials
        Dim RefinedMat As Material
        Dim NewMaterialQuantity As Long

        Dim TempCost As Double = 0
        Dim AdjustedCost As Double = 0
        Dim ModStationTaxRate As Double = 0

        ' Reprocessing Rate for Ore & Ice (including Compressed)
        ' rate = facilityModifier * (1 + 0.03 * ReprocessingLevel) * (1 + 0.02 * ReprocessingEfficiencyLevel)* (1 + 0.02 * OreSpecificSkillLevel) * implantModifier
        ' The implantModifier is 1.01, 1.02 and 1.04 for RX-801, RX-802 and RX-804 respectively.
        TotalYield = CDbl(StationEquipment) * (1 + (0.03 * Reprocessing)) * (1 + (0.02 * ReprocessingEfficiency)) * (1 + (0.02 * OreProcessingSkill)) * (1 + ImplantBonus)

        ' Can't get better than 100%
        If TotalYield > 1 Then
            TotalYield = 1
        End If

        ' Find the units to refine
        SQL = "SELECT UNITS_TO_REFINE FROM ORES WHERE ORE_ID =" & OreID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerRefine = DBCommand.ExecuteReader

        If readerRefine.Read Then
            RefineBatches = CLng(Math.Floor(TotalOre / CLng(readerRefine.GetValue(0))))
        Else
            Return Nothing
        End If

        readerRefine.Close()

        ' Get the Mats that will come from refining 1 batch
        SQL = "SELECT REPROCESSING.REFINED_MATERIAL_ID, REPROCESSING.REFINED_MATERIAL, REPROCESSING.REFINED_MATERIAL_GROUP, "
        SQL = SQL & "REPROCESSING.REFINED_MATERIAL_VOLUME, REPROCESSING.REFINED_MATERIAL_QUANTITY, ITEM_PRICES.PRICE "
        SQL = SQL & "FROM REPROCESSING, ITEM_PRICES "
        SQL = SQL & "WHERE REPROCESSING.ITEM_ID= " & OreID & " "
        SQL = SQL & "AND REPROCESSING.REFINED_MATERIAL_ID = ITEM_PRICES.ITEM_ID "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerRefine = DBCommand.ExecuteReader

        While readerRefine.Read
            ' Calculate the refine amount based on yield
            NewMaterialQuantity = CLng(Math.Round(CLng(readerRefine.GetValue(4)) * RefineBatches * TotalYield, 0))
            ' Add the base material
            RefinedMat = New Material(readerRefine.GetInt64(0), readerRefine.GetString(1), readerRefine.GetString(2),
                                      NewMaterialQuantity, readerRefine.GetDouble(3), If(readerRefine.IsDBNull(5), 0, readerRefine.GetDouble(5)), "", "")
            RefinedMats.InsertMaterial(RefinedMat)
        End While

        ' Subtract the station's refine tax
        For Each RefinedMaterial In RefinedMats.GetMaterialList
            SQL = "SELECT ADJUSTED_PRICE FROM ITEM_PRICES WHERE ITEM_ID = " & RefinedMaterial.GetMaterialTypeID
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerRefine = DBCommand.ExecuteReader
            readerRefine.Read()
            ' Adjust the station tax on the material (by reference) - get the total adjusted price times tax rate minus total cost (save total)
            ModStationTaxRate = StationTax - (0.0075 * StationStanding)
            TempCost = RefinedMaterial.GetTotalCost - (readerRefine.GetDouble(0) * RefinedMaterial.GetQuantity * ModStationTaxRate)
            RefinedMaterial.SetTotalCost(TempCost)
            AdjustedCost += TempCost
        Next

        ' Update the total cost for the list
        Call RefinedMats.ResetTotalValue(AdjustedCost)

        ' Finally adjust the taxes
        If IncludeTax Then
            RefinedMats.AdjustTaxedPrice(GetSalesTax(RefinedMats.GetTotalMaterialsCost))
        End If

        ' Broker fee data
        RefinedMats.AdjustTaxedPrice(GetSalesBrokerFee(RefinedMats.GetTotalMaterialsCost, BrokerFeeData))

        Return RefinedMats

    End Function

    Public Enum OreType
        BaseOre = 0
        FivePercentOre = 1
        TenPercentOre = 2
    End Enum

End Class
