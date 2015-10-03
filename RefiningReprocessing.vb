Imports System.Data.SQLite

Class RefiningReprocessing

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

    Public Sub New(ByVal ReprocessingSkill As Integer, ByVal ReprocessingEfficiencySkill As Integer, _
                   ByVal ScrapMetalProcessingSkill As Integer, ByVal ReprocessingImplantBonus As Double, _
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
    Public Function ReprocessMaterial(ByVal ItemID As Long, ByVal BatchSize As Integer, ByVal NumItems As Long, _
                                      ByVal IncludeTax As Boolean, ByVal IncludeFees As Boolean, ByVal RecursiveReprocess As Boolean) As Materials
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
        SQL = SQL & "REPROCESSING.REFINED_MATERIAL_VOLUME, REPROCESSING.REFINED_MATERIAL_QUANTITY, ITEM_PRICES.PRICE "
        SQL = SQL & "FROM REPROCESSING, ITEM_PRICES "
        SQL = SQL & "WHERE REPROCESSING.ITEM_ID= " & ItemID & " "
        SQL = SQL & "AND REPROCESSING.REFINED_MATERIAL_ID = ITEM_PRICES.ITEM_ID "

        DBCommand = New SQLiteCommand(SQL, DB)
        readerBP = DBCommand.ExecuteReader

        ' Add all the materials 
        While readerBP.Read
            ' Reprocessing Rate for everything else (including unrefined Alchemy products)
            ' rate = facilityModifier * (1 + 0.02 * ScrapMetalProcessingLevel) * (1 − StationTax)
            AdjustedStationYield = StationEquipment * (1 + (0.02 * ScrapMetalProcessing))

            If AdjustedStationYield > 1 Then
                AdjustedStationYield = 1
            End If

            RefinedMatId = CLng(readerBP.GetValue(0))

            TotalBatches = CInt(Math.Round(Math.Ceiling(NumItems / BatchSize), 0))
            NewMaterialQuantity = CLng(Math.Round(CLng(readerBP.GetValue(4)) * TotalBatches * AdjustedStationYield * StationTaxes, 0))

            If RecursiveReprocess Then
                ' If this mat is a raw mat, just add it, else call this function again and get total mats from reprocessing the item
                SQL = "SELECT 'X' FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID = " & RefinedMatId

                DBCommand = New SQLiteCommand(SQL, DB)
                readerReprocess = DBCommand.ExecuteReader

                If readerReprocess.HasRows Then
                    RecursionMaterials = ReprocessMaterial(RefinedMatId, TotalBatches, NewMaterialQuantity, IncludeTax, IncludeFees, RecursiveReprocess)

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
                TempMaterial = New Material(RefinedMatId, readerBP.GetString(1), readerBP.GetString(2), _
                                                NewMaterialQuantity, readerBP.GetDouble(3), If(readerBP.IsDBNull(5), 0, readerBP.GetDouble(5)), "")
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

        If IncludeFees Then
            TempMaterials.AdjustTaxedPrice(GetSalesBrokerFee(TempMaterials.GetTotalMaterialsCost))
        End If

        Return TempMaterials

    End Function

    Public Function RefineOre(ByVal OreID As Long, ByVal OreProcessingSkill As Integer, ByVal TotalOre As Double, _
                              ByVal IncludeTax As Boolean, ByVal IncludeFees As Boolean, ByRef TotalYield As Double) As Materials
        Dim TempYield As Double
        Dim RefineBatches As Long ' Number of batches of refine units we can refine from total

        Dim SQL As String
        Dim readerRefine As SQLiteDataReader

        Dim RefinedMats As New Materials
        Dim RefinedMat As Material
        Dim NewMaterialQuantity As Long

        ' Reprocessing Rate for Ore & Ice (including Compressed)
        ' rate = facilityModifier * (1 + 0.03 * ReprocessingLevel) * (1 + 0.02 * ReprocessingEfficiencyLevel)* (1 + 0.02 * OreSpecificSkillLevel) * implantModifier * (1 − StationTax)
        ' The facilityModifier is 0.5 for most NPC stations, 0.52 for Reprocessing Arrays anchorable in highsec, 0.54 for Reprocessing Arrays anchorable in lowsec/nullsec and 0.50 to 0.60 in nullsec Outposts. 
        ' The implantModifier is 1.01, 1.02 and 1.04 for RX-801, RX-802 and RX-804 respectively.
        TempYield = CDbl(StationEquipment) * (1 + (0.03 * Reprocessing)) * (1 + (0.02 * ReprocessingEfficiency)) * (1 + (0.02 * OreProcessingSkill)) * (1 + ImplantBonus)

        ' Can't get better than 100%
        If TempYield > 1 Then
            TempYield = 1
        End If

        ' Find the units to refine
        SQL = "SELECT UNITS_TO_REFINE FROM ORES WHERE ORE_ID =" & OreID

        DBCommand = New SQLiteCommand(SQL, DB)
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

        DBCommand = New SQLiteCommand(SQL, DB)
        readerRefine = DBCommand.ExecuteReader

        While readerRefine.Read
            ' Calculate the refine amount based on yield
            NewMaterialQuantity = CLng(Math.Round(CLng(readerRefine.GetValue(4)) * RefineBatches * TempYield * StationTaxes, 0))

            ' Add the base material
            RefinedMat = New Material(readerRefine.GetInt64(0), readerRefine.GetString(1), readerRefine.GetString(2), _
                                            NewMaterialQuantity, readerRefine.GetDouble(3), If(readerRefine.IsDBNull(5), 0, readerRefine.GetDouble(5)), "")
            RefinedMats.InsertMaterial(RefinedMat)
        End While

        If IncludeTax Then
            RefinedMats.AdjustTaxedPrice(GetSalesTax(RefinedMats.GetTotalMaterialsCost))
        End If

        If IncludeFees Then
            RefinedMats.AdjustTaxedPrice(GetSalesBrokerFee(RefinedMats.GetTotalMaterialsCost))
        End If

        ' Reference
        TotalYield = TempYield * StationTaxes

        Return RefinedMats

    End Function

End Class
