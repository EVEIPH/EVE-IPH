
Imports System.Data.SQLite
Imports LpSolveDotNet

Public Class ConvertToOre
    Private Refinery As ReprocessingPlant
    Private OreConversionSettings As ConversionToOreSettings

    Private Enum MineralID
        Tritanium = 34
        Pyerite = 35
        Mexallon = 36
        Isogen = 37
        Nocxium = 38
        Zydrine = 39
        Megacyte = 40
        Morphite = 11399

        HeavyWater = 16272
        HeliumIsotopes = 16274
        HydrogenIsotopes = 17889
        LiquidOzone = 16273
        NitrogenIsotopes = 17888
        OxygenIsotopes = 17887
        StrontiumClathrates = 16275

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

    Public Sub New(ByRef RefineryStation As IndustryFacility, ByVal ConversionSettings As ConversionToOreSettings)
        Refinery = New ReprocessingPlant(RefineryStation, UserApplicationSettings.RefiningImplantValue)
        OreConversionSettings = ConversionSettings
    End Sub

    ' Replaces any minerals or ice products with the best ore or ice based on settings
    Public Function GetOresfromMinerals(MineralList As Materials) As Materials
        Dim SQL As String = ""
        Dim SelectionSQL As String = ""
        Dim rsOre As SQLiteDataReader

        Dim OreLP As LpSolve
        Dim OreLPReturn As lpsolve_return

        ' Testing
        Dim OldIndex As Integer
        Dim OldOreColums() As Integer
        Dim MineralRows() As Double

        Call LpSolve.Init()
        OreLP = LpSolve.make_lp(0, 3)

        ' For inserting into final LP matrix later 
        Dim LPColumns As New Dictionary(Of Integer, OreDetails)
        Dim ColumnIndex As Integer = 0
        Dim TempOre As OreDetails
        Dim ProcessingSkill As Integer

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
                    SQL &= "AND ORE_NAME Like 'Compressed%' "
                End If

                SQL &= "GROUP BY ORES.ORE_ID, ORE_NAME, UNITS_TO_REFINE, ORE_VOLUME, PRICE, ORE_GROUP"

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsOre = DBCommand.ExecuteReader
                While rsOre.Read()
                    TempOre.ColumnIndex = ColumnIndex
                    TempOre.OreID = rsOre.GetInt32(0)
                    TempOre.OreName = rsOre.GetString(1)
                    TempOre.RefineUnits = rsOre.GetInt32(2)
                    TempOre.OreVolume = rsOre.GetDouble(3)
                    TempOre.OrePrice = rsOre.GetDouble(4)
                    If Ore.OreGroup = "Ice" Then
                        ProcessingSkill = SelectedCharacter.Skills.GetSkillLevel("Ice Processing")
                    Else
                        ProcessingSkill = SelectedCharacter.Skills.GetSkillLevel(Ore.OreName & " Processing")
                    End If
                    ' Later, if the user selects refined ore value, refine it here before we get started - TODO needs to be rounded value - use a double for material quantities - yes!
                    TempOre.RefinedOreList = Refinery.Reprocess(TempOre.OreID, SelectedCharacter.Skills.GetSkillLevel(3385), SelectedCharacter.Skills.GetSkillLevel(3389),
                                                                ProcessingSkill, TempOre.RefineUnits, False, New BrokerFeeInfo, Nothing)
                    ColumnIndex += 1
                    LPColumns.Add(ColumnIndex, TempOre)
                End While

                rsOre.Close()
            Next
        End With

        ReDim OldOreColums(3)
        ReDim MineralRows(3)

        Dim Test As Boolean = True

        With OreLP

            If Test Then
                .set_col_name(1, "Compressed Veldspar")
                .set_col_name(2, "Compressed Scordite")
                .set_col_name(3, "Compressed Plagioclase")
                .set_col_name(4, "Compressed Pyroxeres")

                ' Add the rows
                .set_add_rowmode(True) ' makes building the model faster if it is done rows by row

                ' Tritanium >= 1313168 - for 10000 AB
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 278.3 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 104.3625 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                MineralRows(OldIndex) = 121.75625 ' Plag
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 0 ' Pyrox
                OldIndex += 1

                ' add the MineralRows to lpsolve
                If .add_constraintex(OldIndex, MineralRows, OldOreColums, lpsolve_constr_types.GE, 1313168) = False Then
                    Application.DoEvents()
                End If

                ' (Pyerite >= 430794) ' Pyerite for 10000 AB
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 0 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 62.7175 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                MineralRows(OldIndex) = 0 ' Plag
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 62.6175 ' Pyrox
                OldIndex += 1

                ' add the MineralRows to lpsolve
                If .add_constraintex(OldIndex, MineralRows, OldOreColums, lpsolve_constr_types.GE, 430794) = False Then
                    Application.DoEvents()
                End If

                ' (Mexallon >= 135255) ' Mexallon for 10000 AB
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                MineralRows(OldIndex) = 0 ' Veld
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                MineralRows(OldIndex) = 0 ' Scordite
                OldIndex += 1
                OldOreColums(OldIndex) = 3 ' Plag
                MineralRows(OldIndex) = 48.7025
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                MineralRows(OldIndex) = 20.8725 ' Pyrox
                OldIndex += 1

                'add the MineralRows to lpsolve
                If .add_constraintex(OldIndex, MineralRows, OldOreColums, lpsolve_constr_types.GE, 135255) = False Then
                    Application.DoEvents()
                End If

                .set_add_rowmode(False) ' rowmode should be turned off again when done building the model

                ' set the objective function - Minimize Price
                OldIndex = 0

                OldOreColums(OldIndex) = 1
                'MineralRows(OldIndex) = 1356 ' Veld price
                MineralRows(OldIndex) = 0.15 ' Volume
                OldIndex += 1
                OldOreColums(OldIndex) = 2
                'MineralRows(OldIndex) = 2887 ' Scordite price
                MineralRows(OldIndex) = 0.19 ' Volume
                OldIndex += 1
                OldOreColums(OldIndex) = 3
                'MineralRows(OldIndex) = 4099 ' Plag price
                MineralRows(OldIndex) = 0.15 ' Volume
                OldIndex += 1
                OldOreColums(OldIndex) = 4
                'MineralRows(OldIndex) = 6989 ' Pyrox price
                MineralRows(OldIndex) = 0.16 ' Volume
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

                Return MineralList
            End If

        End With

        Return MineralList

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
