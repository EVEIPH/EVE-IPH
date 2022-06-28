
Imports System.Data.SQLite
Imports LpSolveDotNet

Public Class ConvertToOre
    Private Refinery As ReprocessingPlant
    Private OreConversionSettings As ConversionToOreSettings

    Private Structure OreDetails
        Dim ID As Integer
        Dim Name As String
        Dim BaseName As String ' Instead of Compressed Dense Veldspar, just Veldspar
        Dim Group As String
        Dim RefineRate As Double
        Dim Volume As Double
        Dim Price As Double
        Dim MinimizeOnValue As Double
        Dim RefinedOreList As Materials
        Dim RefineUnits As Integer
    End Structure

    Public Sub New(ByRef RefineryStation As IndustryFacility, ByVal ConversionSettings As ConversionToOreSettings)
        Refinery = New ReprocessingPlant(RefineryStation, UserApplicationSettings.RefiningImplantValue)
        OreConversionSettings = ConversionSettings
    End Sub

    ' Replaces any minerals or ice products with the best ore or ice based on settings
    Public Function GetOresfromMinerals(ByVal BuildMaterialList As Materials, ByRef ExcessMaterials As Materials, ByRef ReprocessingUsage As Double) As Materials
        Dim SQL As String = ""
        Dim SelectionSQL As String = ""
        Dim rsOre As SQLiteDataReader

        Dim OreLP As LpSolve
        Dim OreLPReturn As lpsolve_return
        Dim OreColumns() As Integer
        Dim MineralRows() As Double
        Dim Outputs() As Double
        Dim OutputQuantity As Long
        Dim TempMaterial As Material

        ' For inserting into final LP matrix later 
        Dim OreData As New Dictionary(Of Integer, OreDetails)
        Dim OreColumnIndex As Integer = 0
        Dim ColumnIndex As Integer = 0
        Dim TempOre As OreDetails
        Dim ProcessingSkill As Integer
        Dim RefinedItemsList As New List(Of String) ' For setting up the rows for LP Solve and making sure no rows are empty
        Dim ReturnRefinedItemsList As List(Of String)

        ' Conversion Settings has the ores we want to use, so just get the refine values
        With OreConversionSettings
            For Each Ore In .SelectedOres
                SQL = "SELECT ORES.ORE_ID, ORE_NAME, UNITS_TO_REFINE, ORE_VOLUME, PRICE, "
                SQL &= "CASE WHEN ITEM_GROUP = 'Ice' THEN CASE WHEN SUBSTR(ORE_NAME,1,10) ='Compressed' THEN SUBSTR(ORE_NAME,12) ELSE ORE_NAME END ELSE ITEM_GROUP END AS ORE_GROUP "
                SQL &= "FROM ORES, ORE_LOCATIONS, ITEM_PRICES WHERE ORES.ORE_ID = ITEM_PRICES.ITEM_ID "
                SQL &= "AND ORE_GROUP = '" & Ore.OreName & "' "
                ' Check Variants for ore only
                If Ore.OreGroup = "Ore" Then
                    SQL &= "AND HIGH_YIELD_ORE IN ("
                    If .OreVariant0 Then
                        SQL &= "0,"
                    End If
                    If .OreVariant5 Then
                        SQL &= "1,"
                    End If
                    If .OreVariant10 Then
                        SQL &= "2,"
                    End If
                    ' Strip the last comma
                    SQL = SQL.Substring(0, Len(SQL) - 1) & ") "
                End If

                If (Ore.OreGroup = "Ice" And .CompressedIce) Or (Ore.OreGroup = "Ore" And .CompressedOre) Then
                    SQL &= "AND ORE_NAME LIKE 'Compressed%' "
                Else
                    SQL &= "AND ORE_NAME NOT LIKE 'Compressed%' "
                End If

                SQL &= "GROUP BY ORES.ORE_ID, ORE_NAME, UNITS_TO_REFINE, ORE_VOLUME, PRICE, ORE_GROUP"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsOre = DBCommand.ExecuteReader

                While rsOre.Read()
                    TempOre.ID = rsOre.GetInt32(0)
                    TempOre.Name = rsOre.GetString(1)
                    TempOre.RefineUnits = rsOre.GetInt32(2)
                    TempOre.Volume = rsOre.GetDouble(3)
                    TempOre.Price = rsOre.GetDouble(4)
                    TempOre.Group = Ore.OreGroup
                    TempOre.BaseName = Ore.OreName
                    ProcessingSkill = SelectedCharacter.Skills.GetSkillLevel(GetOreProcessingSkillName(Ore.OreName))
                    ' Set the refining rate based on ice or ore
                    If Ore.OreGroup = "Ice" Then
                        TempOre.RefineRate = Refinery.GetFacilility.IceFacilityRefineRate
                    Else
                        TempOre.RefineRate = Refinery.GetFacilility.OreFacilityRefineRate
                    End If

                    Refinery.GetFacilility.MaterialMultiplier = TempOre.RefineRate

                    ReturnRefinedItemsList = New List(Of String)

                    ' Refine but use a double for material quantities to get partial refining value to be more exact
                    TempOre.RefinedOreList = Refinery.Reprocess(TempOre.ID, SelectedCharacter.Skills.GetSkillLevel(3385), SelectedCharacter.Skills.GetSkillLevel(3389),
                                                                ProcessingSkill, TempOre.RefineUnits, False, New BrokerFeeInfo, Nothing, Nothing, True, ReturnRefinedItemsList)

                    ' For Ice, if the isotopes don't exist in the returned list for what we need in the material list, don't add it
                    If Ore.OreGroup = "Ice" And Not IsNothing(BuildMaterialList.SearchListbyName("Isotopes")) Then
                        For Each Item In ReturnRefinedItemsList
                            If Item.Contains("Isotopes") Then
                                ' Check it
                                If IsNothing(BuildMaterialList.SearchListbyName(Item, True)) Then
                                    GoTo NextOre
                                End If
                            End If
                        Next
                    End If

                    ' Save the value for use in minimize
                    Select Case OreConversionSettings.MinimizeOn
                        Case "Refine Price"
                            TempOre.MinimizeOnValue = TempOre.RefinedOreList.GetTotalMaterialsCost
                        Case "Ore/Ice Price"
                            TempOre.MinimizeOnValue = rsOre.GetDouble(4)
                        Case "Ore/Ice Volume"
                            TempOre.MinimizeOnValue = rsOre.GetDouble(3)
                    End Select

                    ' Add any refined minerals to the list not already added
                    For Each Item In ReturnRefinedItemsList
                        If Not RefinedItemsList.Contains(Item) And Not .IgnoreItems.Contains(Item) Then
                            RefinedItemsList.Add(Item)
                        End If
                    Next

                    OreColumnIndex += 1
                    OreData.Add(OreColumnIndex, TempOre)
                End While
NextOre:
                rsOre.Close()
            Next
        End With

        If OreData.Count = 0 Then
            ' If no ores match what we need, then just return the main list
            Return BuildMaterialList
        End If

        ' Initialize model
        Call LpSolve.Init()
        OreLP = LpSolve.make_lp(0, OreData.Count - 1)

        OreColumnIndex = 1

        With OreLP
            ' Start with adding the column names
            For OreColumnIndex = 1 To OreData.Count
                .set_col_name(OreColumnIndex, OreData(OreColumnIndex).Name)
            Next

            ' Add the rows
            .set_add_rowmode(True) ' makes building the model faster if it is done rows by row

            ' Number of rows is based on any items in the refined items list
            ReDim OreColumns(OreData.Count - 1)
            ReDim MineralRows(OreData.Count - 1)
            ReDim Outputs(OreData.Count - 1)

            ' Loop through each refined item and search each ore data refined list for the value and add it to the row
            For Each Item In RefinedItemsList
                ColumnIndex = 0 ' Reset the row column index each new item
                For OreColumnIndex = 1 To OreData.Count
                    OreColumns(ColumnIndex) = ColumnIndex + 1
                    MineralRows(ColumnIndex) = OreData(OreColumnIndex).RefinedOreList.SearchListbyName(Item).GetDQuantity ' Search for the value and add
                    ColumnIndex += 1
                Next

                TempMaterial = BuildMaterialList.SearchListbyName(Item)

                ' Add the rows to lpsolve after looking up the value needed
                If Not IsNothing(TempMaterial) Then
                    .add_constraintex(ColumnIndex, MineralRows, OreColumns, lpsolve_constr_types.GE, TempMaterial.GetQuantity)
                End If
            Next

            .set_add_rowmode(False) ' rowmode should be turned off again when done building the model

            ' Now set the objective function to minimize on
            ColumnIndex = 0 ' Reset the row column index each new item
            For OreColumnIndex = 1 To OreData.Count
                OreColumns(ColumnIndex) = ColumnIndex + 1
                MineralRows(ColumnIndex) = OreData(OreColumnIndex).MinimizeOnValue ' Search for the value and add
                ColumnIndex += 1
            Next

            .set_obj_fnex(ColumnIndex, MineralRows, OreColumns) ' Add the minimize row to lpsolve 
            .set_minim() ' Set the object direction to minimize

            ' Let lpsolve calculate a solution
            OreLPReturn = .solve()
            .get_variables(Outputs) ' Get the output values for each column name

        End With

        ' Process for returning the final new list with ores
        Dim MatLookup As Material
        Dim RefinedMaterials As New Materials
        Dim ReturnRefineryFee As Double = 0

        ' Now, for each refined item we converted, remove it from the material list sent
        For Each Item In RefinedItemsList
            ' Save the original amounts
            MatLookup = BuildMaterialList.SearchListbyName(Item)
            Call ExcessMaterials.InsertMaterial(MatLookup)
            BuildMaterialList.RemoveMaterial(MatLookup)
        Next

        ' Add all the items we converted in LP Solve
        For CI = 1 To OreData.Count
            With OreData(CI)
                OutputQuantity = CLng(Math.Ceiling(Outputs(CI - 1))) * .RefineUnits
                If OutputQuantity <> 0 Then
                    BuildMaterialList.InsertMaterial(New Material(.ID, .Name, .Group, OutputQuantity, .Volume, .Price, "", ""))

                    ' Refine this to calculate excess minerals
                    If .Group = "Ice" Then
                        ProcessingSkill = SelectedCharacter.Skills.GetSkillLevel("Ice Processing")
                    Else
                        ProcessingSkill = SelectedCharacter.Skills.GetSkillLevel(.BaseName & " Processing")
                    End If

                    ' Set the correct refining rate
                    Refinery.GetFacilility.MaterialMultiplier = .RefineRate

                    ' Insert the refined materials for totals later
                    RefinedMaterials.InsertMaterialList(Refinery.Reprocess(.ID, SelectedCharacter.Skills.GetSkillLevel(3385), SelectedCharacter.Skills.GetSkillLevel(3389),
                                                         ProcessingSkill, OutputQuantity, False, New BrokerFeeInfo, Nothing, ReturnRefineryFee).GetMaterialList)
                    ' Get the total cost to refine
                    ReprocessingUsage += ReturnRefineryFee

                End If
            End With
        Next

        ' Finaly adjust the excess materials
        ' Refined materials from this ore we solved for should be greater than or equal to what is needed, so just subtract all minerals from the totals needed
        For Each mat In ExcessMaterials.GetMaterialList
            MatLookup = RefinedMaterials.SearchListbyName(mat.GetMaterialName)
            If Not IsNothing(MatLookup) Then
                ' Adjust the quantity in the excess material list
                mat.SetQuantity(MatLookup.GetQuantity - mat.GetQuantity)
            End If
        Next

        Return BuildMaterialList

    End Function

End Class
