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

    Public Function GetFacilility() As IndustryFacility
        Return ReprocessingFacility
    End Function

    Public Function Reprocess(ByVal ItemID As Long, ByVal ReprocessingSkill As Integer, ByVal ReprocessingEfficiencySkill As Integer, ByVal ProcessingSkill As Integer,
                              ByVal TotalQuantity As Double, ByVal IncludeTax As Boolean, ByVal BrokerFeeData As BrokerFeeInfo, ByRef TotalYield As Double, ByRef ReprocessingFees As Double,
                              Optional MintoOreFormat As Boolean = False, Optional ByRef RefinedMineralsList As List(Of String) = Nothing) As Materials
        Dim RefineBatches As Long ' Number of batches of refine units we can refine from total

        Dim SQL As String
        Dim readerRefine As SQLiteDataReader

        Dim RefinedMats As New Materials
        Dim RefinedMat As Material
        Dim NewMaterialQuantity As Long
        Dim DoubleNewMaterialQuantity As Double

        Dim TempCost As Double = 0
        Dim AdjustedCost As Double = 0
        Dim ModStationTaxRate As Double = 0
        Dim ScrapReprocessing As Boolean

        ' Find the units to refine for ore
        SQL = "SELECT UNITS_TO_REFINE FROM ORES WHERE ORE_ID =" & ItemID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerRefine = DBCommand.ExecuteReader

        If readerRefine.Read Then
            ' Process into batches
            RefineBatches = CLng(Math.Floor(TotalQuantity / CLng(readerRefine.GetValue(0))))
            If RefineBatches = 0 Then
                ' Can't reprocess if there arne't enough units to refine
                Return RefinedMats
            End If
            ScrapReprocessing = False
        Else
            ' Not an ore or ice, so must be scrapmetal processing
            SQL = "SELECT UNITS_TO_REPROCESS FROM REPROCESSING WHERE ITEM_ID =" & ItemID
            readerRefine.Close()
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerRefine = DBCommand.ExecuteReader

            If readerRefine.Read Then
                RefineBatches = CLng(Math.Floor(TotalQuantity / CLng(readerRefine.GetValue(0))))
            Else
                RefineBatches = CLng(TotalQuantity)
            End If
            ScrapReprocessing = True
        End If

        readerRefine.Close()

        ' Reprocessing Rate for Ore & Ice (including Compressed)
        ' Upwells - ReprocessingYield = (50+ RigModifier × (1 + SecurityModifier)) × (1 + StructureModifier)×(1+(0.03×R))×(1+(0.02×Re))×(1+(0.02×Op))×(1+Im)
        ' Station - ReprocessingYield = StationEquipment x (1 + Processing skill x 0.03) x (1 + Processing Efficiency skill x 0.02) x (1 + Ore Processing skill x 0.02) x (1 + Processing Implant)
        ' The implantModifier is 1.01, 1.02 and 1.04 for RX-801, RX-802 and RX-804 respectively.

        If ScrapReprocessing Then
            ' Base Station Equipment x (1 + Scrapmetal Processing x 0.02) - scrapmetal processing is only modifier that applies
            ' **** GET THE VALUES FROM ATTTRIBUTES - change to take an override processing skill or look it up if null based on the item
            TotalYield = ReprocessingFacility.BaseME * (1 + (0.02 * ProcessingSkill))
        Else
            TotalYield = ReprocessingFacility.MaterialMultiplier * (1 + (0.03 * ReprocessingSkill)) * (1 + (0.02 * ReprocessingEfficiencySkill)) * (1 + (0.02 * ProcessingSkill)) * (1 + ImplantBonus)
        End If

        ' Can't get better than 100%
        If TotalYield > 1 Then
            TotalYield = 1
        End If

        ' Add the base material
        If MintoOreFormat Then

            ' Get all the materials that could come from refining ores, even if zero, and add them to the list
            SQL = "SELECT typeID, typeName, groupName, volume, CASE WHEN REFINED_MATERIAL_QUANTITY IS NULL THEN 0 ELSE REFINED_MATERIAL_QUANTITY END AS QUANTITY, PRICE "
            SQL &= "FROM INVENTORY_TYPES, INVENTORY_GROUPS, ITEM_PRICES LEFT JOIN REPROCESSING ON REPROCESSING.REFINED_MATERIAL_ID = INVENTORY_TYPES.typeID "
            SQL &= "AND REPROCESSING.ITEM_ID = " & ItemID & " WHERE INVENTORY_TYPES.groupID IN (18,423) AND INVENTORY_TYPES.typeID = ITEM_PRICES.ITEM_ID "
            SQL &= "AND typeID NOT IN (27029,48927) AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID ORDER BY typeID"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerRefine = DBCommand.ExecuteReader

            While readerRefine.Read
                DoubleNewMaterialQuantity = CLng(readerRefine.GetValue(4)) * RefineBatches * TotalYield

                RefinedMat = New Material(readerRefine.GetInt64(0), readerRefine.GetString(1), readerRefine.GetString(2),
                    DoubleNewMaterialQuantity, readerRefine.GetDouble(3), If(readerRefine.IsDBNull(5), 0, readerRefine.GetDouble(5)), "", "")

                RefinedMats.InsertMaterial(RefinedMat)

                ' If this has a quantity, then add the mineral name to the list for use
                If DoubleNewMaterialQuantity <> 0 Then
                    RefinedMineralsList.Add(readerRefine.GetString(1))
                End If

            End While

        Else
            ' Get the Mats that will come from refining 1 batch
            SQL = "SELECT REPROCESSING.REFINED_MATERIAL_ID, REPROCESSING.REFINED_MATERIAL, REPROCESSING.REFINED_MATERIAL_GROUP, "
            SQL &= "REPROCESSING.REFINED_MATERIAL_VOLUME, REPROCESSING.REFINED_MATERIAL_QUANTITY, ITEM_PRICES.PRICE "
            SQL &= "FROM REPROCESSING, ITEM_PRICES "
            SQL &= "WHERE REPROCESSING.ITEM_ID= " & ItemID & " "
            SQL &= "AND REPROCESSING.REFINED_MATERIAL_ID = ITEM_PRICES.ITEM_ID "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            readerRefine = DBCommand.ExecuteReader

            While readerRefine.Read
                ' Calculate the refine amount based on yield
                If ScrapReprocessing Then
                    NewMaterialQuantity = CLng(Math.Floor(CLng(readerRefine.GetValue(4)) * RefineBatches * TotalYield))
                Else
                    NewMaterialQuantity = CLng(Math.Floor(CLng(readerRefine.GetValue(4)) * RefineBatches * TotalYield))
                End If

                RefinedMat = New Material(readerRefine.GetInt64(0), readerRefine.GetString(1), readerRefine.GetString(2),
                        NewMaterialQuantity, readerRefine.GetDouble(3), If(readerRefine.IsDBNull(5), 0, readerRefine.GetDouble(5)), "", "")

                RefinedMats.InsertMaterial(RefinedMat)

            End While

        End If

        readerRefine.Close()

        Dim RefinedMatQuantity As Double
        Dim SingleReprocessingFee As Double
        ReprocessingFees = 0

        If ReprocessingFacility.IncludeActivityUsage Then
            ' Subtract the station's refine tax - or usage
            For Each RefinedMaterial In RefinedMats.GetMaterialList
                SQL = "SELECT ADJUSTED_PRICE FROM ITEM_PRICES WHERE ITEM_ID = " & RefinedMaterial.GetMaterialTypeID
                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerRefine = DBCommand.ExecuteReader
                readerRefine.Read()
                If MintoOreFormat Then
                    RefinedMatQuantity = RefinedMaterial.GetDQuantity
                Else
                    RefinedMatQuantity = RefinedMaterial.GetQuantity
                End If
                If RefinedMatQuantity > 0 Then
                    SingleReprocessingFee = (readerRefine.GetDouble(0) * RefinedMatQuantity * ReprocessingFacility.TaxRate)
                    ReprocessingFees += SingleReprocessingFee
                    ' Adjust the station tax on the material - get the total adjusted price times tax rate minus total cost (save total)
                    TempCost = RefinedMaterial.GetTotalCost - SingleReprocessingFee
                    RefinedMaterial.SetTotalCost(TempCost)
                    AdjustedCost += TempCost
                End If
                readerRefine.Close()
            Next

            ' Update the total cost for the list
            Call RefinedMats.ResetTotalValue(AdjustedCost)
        End If

        ' Finally adjust the taxes
        If IncludeTax Then
            RefinedMats.AdjustTaxedPrice(GetSalesTax(RefinedMats.GetTotalMaterialsCost))
        End If

        ' Broker fee data
        RefinedMats.AdjustTaxedPrice(GetSalesBrokerFee(RefinedMats.GetTotalMaterialsCost, BrokerFeeData))

        Return RefinedMats

    End Function

End Class
