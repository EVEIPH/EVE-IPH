
Imports System.Data.SQLite
Imports LpSolveDotNet

Public Class MineralstoOre

    Private Enum MineralID
        Tritanium = 34
        Pyerite = 35
        Mexallon = 36
        Isogen = 37
        Nocxium = 38
        Zydrine = 39
        Megacyte = 40
        Morphite = 11399
    End Enum

    Private Structure OreDetails
        Dim OreID As Integer
        Dim OreName As String
        Dim OreVolume As Double
        Dim OrePrice As Double
        Dim RefinedOreList As Materials
        Dim RefineUnits As Integer
        Dim ColumnIndex As Integer
    End Structure

    Public Sub New()
    End Sub

    Public Function GetOresfromMinerals(RefineryStation As IndustryFacility, MineralList As List(Of Material)) As List(Of Material)
        Dim OreLP As LpSolve
        Dim OreLPReturn As lpsolve_return

        Dim OldIndex As Integer
        Dim OldOreColums() As Integer
        Dim MineralRows() As Double

        Dim Index As Integer = 0
        Dim RowItemCount As Integer = 0
        Dim OreIndex As Integer = 0

        Dim SQL As String = ""
        Dim SelectionSQL As String = ""
        Dim rsOre As SQLiteDataReader

        Dim OreList As New List(Of OreDetails)
        Dim TempOre As OreDetails

        Dim Refinery As New ReprocessingPlant(RefineryStation, UserApplicationSettings.RefiningImplantValue)
        Dim RefinedAmount As Double = 0 ' The returned refine rate for the mineral 

        Call LpSolve.Init()
        OreLP = LpSolve.make_lp(0, 3)

        SQL = "SELECT ORES.ORE_ID, ORE_NAME, UNITS_TO_REFINE, ORE_VOLUME, PRICE FROM ORES, ORE_LOCATIONS, ITEM_PRICES_FACT "
        SQL &= "WHERE ORES.ORE_ID = ORE_LOCATIONS.ORE_ID AND ORES.ORE_ID = ITEM_PRICES_FACT.ITEM_ID "
        SelectionSQL = "AND COMPRESSED = 0 "
        SelectionSQL &= "AND SPACE = 'Caldari' "
        SelectionSQL &= "AND SYSTEM_SECURITY = 'High Sec' "
        SelectionSQL &= "AND BELT_TYPE = 'Ore' "
        SelectionSQL &= "AND HIGH_YIELD_ORE = 0 "

        DBCommand = New SQLiteCommand(SQL & SelectionSQL, EVEDB.DBREf)
        rsOre = DBCommand.ExecuteReader

        Index = 0

        ' Get the list of ores we will be working with for the indexes
        While rsOre.Read
            TempOre.ColumnIndex = Index
            TempOre.OreID = rsOre.GetInt32(0)
            TempOre.OreName = rsOre.GetString(1)
            TempOre.RefineUnits = rsOre.GetInt32(2)
            TempOre.OreVolume = rsOre.GetDouble(3)
            TempOre.OrePrice = rsOre.GetDouble(4)
            ' Later, if the user selects refined ore value, refine it here before we get started - TODO needs to be rounded value - double for material quantities
            TempOre.RefinedOreList = Refinery.ReprocessORE(TempOre.OreID, SelectedCharacter.Skills.GetSkillLevel(3385), SelectedCharacter.Skills.GetSkillLevel(3389),
                                                           SelectedCharacter.Skills.GetSkillLevel(TempOre.OreName & " Processing"), TempOre.RefineUnits, False, New BrokerFeeInfo, Nothing)
            Call OreList.Add(TempOre)
            Index += 1
            OreLP.set_col_name(Index, rsOre.GetString(1))
        End While

        RowItemCount = Index ' Save this for later
        ' Reset the index for the arrays
        Index -= 1

        ReDim OldOreColums(Index)
        ReDim MineralRows(Index)

        ' Set arrays for LP solve
        Dim OreColumns(Index) As Integer
        Dim PriceRow(Index) As Double
        ' Mineral arrays (will add ice mats later - maybe moon?)
        Dim TritaniumRow(Index) As Double
        Dim PyeriteRow(Index) As Double
        Dim MexallonRow(Index) As Double
        Dim IsogenRow(Index) As Double
        Dim NocxiumRow(Index) As Double
        Dim ZydrineRow(Index) As Double
        Dim MegacyteRow(Index) As Double
        Dim MorphiteRow(Index) As Double

        OreIndex = 0

        For Each Ore In OreList
            With OreLP
                ' Process each ore in the list by looping through it's refined amounts returned and adding to the appropriate row
                Index = Ore.ColumnIndex
                OreColumns(OreIndex) = Index + 1
                OreIndex += 1
                ' Add the price, later add options to what minimizing value they want to use (ore price, volume, refined price)
                PriceRow(Index) = Ore.OrePrice

                For Each Material In Ore.RefinedOreList.GetMaterialList
                    ' Select each item ID and add to that specific row when found
                    Select Case Material.GetMaterialTypeID
                        Case MineralID.Tritanium
                            TritaniumRow(Index) = Material.GetQuantity
                        Case MineralID.Pyerite
                            PyeriteRow(Index) = Material.GetQuantity
                        Case MineralID.Mexallon
                            MexallonRow(Index) = Material.GetQuantity
                        Case MineralID.Isogen
                            IsogenRow(Index) = Material.GetQuantity
                        Case MineralID.Nocxium
                            NocxiumRow(Index) = Material.GetQuantity
                        Case MineralID.Zydrine
                            ZydrineRow(Index) = Material.GetQuantity
                        Case MineralID.Megacyte
                            MegacyteRow(Index) = Material.GetQuantity
                        Case MineralID.Morphite
                            MorphiteRow(Index) = Material.GetQuantity
                    End Select
                Next
            End With
        Next

        Dim Test As Boolean = True

        With OreLP

            If Test Then
                .set_col_name(1, "Veldspar")
                .set_col_name(2, "Scordite")
                .set_col_name(3, "Plagioclase")
                .set_col_name(4, "Pyroxeres")

                ' Add the rows
                .set_add_rowmode(True) ' makes building the model faster if it is done rows by row

                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 277 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 103 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                MineralRows(OldIndex) = 121 ' Plag
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 0 ' Pyerox
                OldIndex += 1

                ' add the MineralRows to lpsolve
                If .add_constraintex(OldIndex, MineralRows, OldOreColums, lpsolve_constr_types.GE, 23005901) = False Then
                    Application.DoEvents()
                End If

                ' (Pyerite >= 2652667) ' Pyerite for Raven
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 0 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 62 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                MineralRows(OldIndex) = 0 ' Plag
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 62 ' Pyerox
                OldIndex += 1

                ' add the MineralRows to lpsolve
                If .add_constraintex(OldIndex, MineralRows, OldOreColums, lpsolve_constr_types.GE, 2652667) = False Then
                    Application.DoEvents()
                End If

                ' (Mexallon >= 664444) ' Mexallon for Raven
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 0 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 0 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                MineralRows(OldIndex) = 48 ' Plag
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 20 ' Pyerox
                OldIndex += 1

                'add the MineralRows to lpsolve
                If .add_constraintex(OldIndex, MineralRows, OldOreColums, lpsolve_constr_types.GE, 664444) = False Then
                    Application.DoEvents()
                End If

                .set_add_rowmode(False) ' rowmode should be turned off again when done building the model

                ' set the objective function - minimize price
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 16.28 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 21.73 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                MineralRows(OldIndex) = 67 ' Plag
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 42.76 ' Pyerox
                OldIndex += 1

                If .set_obj_fnex(OldIndex, MineralRows, OldOreColums) = False Then
                    Application.DoEvents()
                End If

                ' set the object direction to minimize
                .set_minim()

                ' just out of curiousity, now show the model in lp format on screen
                ' this only works if this is a console application. If not, use write_lp and a filename
                .write_lp("model.lp")

                ' I only want to see important messages on screen while solving
                .set_verbose(lpsolve_verbosity.IMPORTANT)

                ' Now let lpsolve calculate a solution
                OreLPReturn = .solve()
                If OreLPReturn = lpsolve_return.OPTIMAL Then
                    Application.DoEvents()
                Else
                    Application.DoEvents()
                End If

                ' a solution is calculated, now lets get some results

                ' objective value
                Debug.WriteLine("Objective value TEST: " & .get_objective())

                ' variable values
                .get_variables(MineralRows)
                For j = 1 To 4
                    Debug.WriteLine(.get_col_name(j) & ": " & MineralRows(j - 1))
                Next

                Return Nothing
            End If



            Dim TempRow() As Double = Nothing
            ' Loop through the mineral list sent and set each final value for need
            For Each Material In MineralList
                Select Case Material.GetMaterialTypeID
                    Case MineralID.Tritanium
                        TempRow = TritaniumRow
                    Case MineralID.Pyerite
                        TempRow = PyeriteRow
                    Case MineralID.Mexallon
                        TempRow = MexallonRow
                    Case Else
                        TempRow = Nothing
                        'Case MineralID.Isogen
                        '    TempRow = IsogenRow
                        'Case MineralID.Nocxium
                        '    TempRow = NocxiumRow
                        'Case MineralID.Zydrine
                        '    TempRow = ZydrineRow
                        'Case MineralID.Megacyte
                        '    TempRow = MegacyteRow
                        'Case MineralID.Morphite
                        '    TempRow = MorphiteRow
                End Select

                ' Add the row with the material quantity needed
                If Not IsNothing(TempRow) Then
                    If .add_constraintex(RowItemCount, TempRow, OreColumns, lpsolve_constr_types.GE, Material.GetQuantity) = False Then
                        Application.DoEvents()
                    End If
                End If
            Next

            .set_add_rowmode(False) ' rowmode should be turned off again when done building the model

            ' Now, add the price/volume row for the objective to minimize
            If .set_obj_fnex(RowItemCount, PriceRow, OreColumns) = False Then
                Application.DoEvents()
            End If

            ' set the object direction to minimize
            .set_minim()

            ' just out of curiousity, now show the model in lp format on screen
            ' this only works if this is a console application. If not, use write_lp and a filename
            .write_lp("model.lp")

            ' I only want to see important messages on screen while solving
            .set_verbose(lpsolve_verbosity.IMPORTANT)

            ' Now let lpsolve calculate a solution
            OreLPReturn = .solve()
            If OreLPReturn = lpsolve_return.OPTIMAL Then
                Application.DoEvents()
            Else
                Application.DoEvents()
            End If

            ' a solution is calculated, now lets get some results

            ' obIndexective value
            Debug.WriteLine("Objective value: " & .get_objective())
            .get_variables(MineralRows)

            ' variable values
            For Index = 1 To 4
                Debug.WriteLine(.get_col_name(Index) & ": " & MineralRows(Index - 1))
            Next

            ' we are done now
        End With

        Return Nothing

    End Function

    Public Function TestLPSolve(MineralList As List(Of Material)) As List(Of Material)
        Dim LPTest As LpSolve
        Dim LPReturn As lpsolve_return
        Dim j As Integer
        Dim colno() As Integer
        Dim row() As Double

        Call LpSolve.Init()

        LPTest = LpSolve.make_lp(0, 2)

        ReDim colno(1)
        ReDim row(1)

        With LPTest
            .set_col_name(1, "x")
            .set_col_name(2, "y")

            .set_add_rowmode(True) ' makes building the model faster if it is done rows by row

            ' construct first row (120 x + 210 y <= 15000)
            j = 0

            colno(j) = 1 ' first column
            row(j) = 120
            j = j + 1

            colno(j) = 2 ' second column
            row(j) = 210
            j = j + 1

            ' add the row to lpsolve
            If .add_constraintex(j, row, colno, lpsolve_constr_types.LE, 15000) = False Then
                Application.DoEvents()
            End If

            ' construct second row (110 x + 30 y <= 4000)
            j = 0

            colno(j) = 1 ' first column
            row(j) = 110
            j = j + 1

            colno(j) = 2 ' second column
            row(j) = 30
            j = j + 1

            ' add the row to lpsolve
            If .add_constraintex(j, row, colno, lpsolve_constr_types.LE, 4000) = False Then
                Application.DoEvents()
            End If

            ' construct third row (x + y <= 75)
            j = 0

            colno(j) = 1 ' first column
            row(j) = 1
            j = j + 1

            colno(j) = 2 ' second column
            row(j) = 1
            j = j + 1

            ' add the row to lpsolve
            If .add_constraintex(j, row, colno, lpsolve_constr_types.LE, 75) = False Then
                Application.DoEvents()
            End If

            .set_add_rowmode(False) ' rowmode should be turned off again when done building the model

            ' set the objective function (143 x + 60 y)
            j = 0

            colno(j) = 1 ' first column
            row(j) = 143
            j = j + 1

            colno(j) = 2 ' second column
            row(j) = 60
            j = j + 1

            ' set the objective in lpsolve
            If .set_obj_fnex(j, row, colno) = False Then
                Application.DoEvents()
            End If

            ' set the object direction to maximize
            .set_maxim()

            ' just out of curioucity, now show the model in lp format on screen
            ' this only works if this is a console application. If not, use write_lp and a filename
            .write_lp("model.lp")

            ' I only want to see important messages on screen while solving
            .set_verbose(lpsolve_verbosity.IMPORTANT)

            ' Now let lpsolve calculate a solution
            LPReturn = .solve()
            If LPReturn = lpsolve_return.OPTIMAL Then
                Application.DoEvents()
            Else
                Application.DoEvents()
            End If

            ' a solution is calculated, now lets get some results

            ' objective value
            Debug.WriteLine("Objective value: " & .get_objective())

            ' variable values
            .get_variables(row)
            For j = 1 To 2
                Debug.WriteLine(.get_col_name(j) & ": " & row(j - 1))
            Next

            ' we are done now


        End With


        Return Nothing

    End Function

End Class
