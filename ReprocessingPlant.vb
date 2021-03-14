Imports System.Data.SQLite

Public Structure RefinedMaterial

End Structure

Class ReprocessingPlant

    Dim ImplantBonus As Double
    Dim ReprocessingFacility As IndustryFacility

    Public Sub New(ByVal SentReprocessingFacility As IndustryFacility, ByVal ReprocessingImplantBonus As Double)

        ReprocessingFacility = SentReprocessingFacility
        ImplantBonus = ReprocessingImplantBonus

    End Sub

    Public Function ReprocessORE(ByVal OreID As Long, ByVal ReprocessingSkill As Integer, ByVal ReprocessingEfficiencySkill As Integer, ByVal OreProcessingSkill As Integer,
                                 ByVal TotalOre As Double, ByVal IncludeTax As Boolean, ByVal BrokerFeeData As BrokerFeeInfo, ByRef TotalYield As Double) As Materials
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
        ' Upwells - ReprocessingYield = (50+ RigModifier × (1 + SecurityModifier)) × (1 + StructureModifier)×(1+(0.03×R))×(1+(0.02×Re))×(1+(0.02×Op))×(1+Im)
        ' Station - ReprocessingYield = StationEquipment x (1 + Processing skill x 0.03) x (1 + Processing Efficiency skill x 0.02) x (1 + Ore Processing skill x 0.02) x (1 + Processing Implant)
        ' The implantModifier is 1.01, 1.02 and 1.04 for RX-801, RX-802 and RX-804 respectively.
        ' The reprocessing facility has the base yield for stations or upwells so just apply the skills
        TotalYield = ReprocessingFacility.MaterialMultiplier * (1 + (0.03 * ReprocessingSkill)) * (1 + (0.02 * ReprocessingEfficiencySkill)) * (1 + (0.02 * OreProcessingSkill)) * (1 + ImplantBonus)

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
            TempCost = RefinedMaterial.GetTotalCost - (readerRefine.GetDouble(0) * RefinedMaterial.GetQuantity * ReprocessingFacility.TaxRate)
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

End Class
