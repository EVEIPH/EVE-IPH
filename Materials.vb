
' Stores a list of materials and associated functions
Public Class Materials
    Implements ICloneable

    ' The List of Materials
    Private MaterialList As List(Of Material)

    ' Total Cost of materials in the list
    Private TotalMaterialsCost As Double
    ' Total Volume of materials in the list
    Private TotalMaterialsVolume As Double

    Private MaterialtoFind As Material

    ' Constructor
    Public Sub New()
        TotalMaterialsCost = 0
        TotalMaterialsVolume = 0

        MaterialList = New List(Of Material)
    End Sub

    ' For doing a deep copy of Materials
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim CopyOfMe = New Materials
        Dim TempMat As Material

        If Not IsNothing(MaterialList) Then
            For i = 0 To MaterialList.Count - 1
                TempMat = CType(MaterialList(i).Clone(), Material)
                CopyOfMe.InsertMaterial(TempMat)
            Next
        Else
            CopyOfMe.MaterialList = Nothing
        End If

        CopyOfMe.TotalMaterialsCost = Me.TotalMaterialsCost
        CopyOfMe.TotalMaterialsVolume = Me.TotalMaterialsVolume

        Return CopyOfMe

    End Function

    ' Resets the List
    Public Sub Clear()
        TotalMaterialsCost = 0
        TotalMaterialsVolume = 0

        MaterialList = Nothing
    End Sub

    ' Searches the list and finds then returns a material for the name part sent
    Public Function SearchListbyName(ByVal SearchText As String, Optional ExactSearch As Boolean = True) As Material

        If Not IsNothing(MaterialList) Then
            For i = 0 To MaterialList.Count - 1
                If ExactSearch Then
                    If MaterialList(i).GetMaterialName = SearchText Then
                        Return MaterialList(i)
                    End If
                Else ' Look for partial string
                    If InStr(MaterialList(i).GetMaterialName, SearchText) <> 0 Then
                        Return MaterialList(i)
                    End If
                End If
            Next
        End If

        Return Nothing

    End Function

    ' Just adds a Full list to the Class
    Public Sub InsertMaterialList(ByVal SentList As List(Of Material))
        Dim i As Integer

        If Not IsNothing(SentList) Then
            For i = 0 To (SentList.Count - 1)
                ' clone each or it inserts a reference, which will link to others like it when searched
                Call InsertMaterial(SentList(i))
            Next
        End If

    End Sub

    ' Removes a full list of materials from the list
    Public Sub RemoveMaterialList(ByVal SentList As List(Of Material))
        Dim i As Integer

        If Not IsNothing(SentList) Then
            For i = 0 To (SentList.Count - 1)
                Call RemoveMaterial(SentList(i))
            Next
        End If

    End Sub

    ' Inserts a Single material into list
    Public Sub InsertMaterial(ByVal SentMaterial As Material, Optional OverrideCost As Double = -1)
        Dim TempMat As Material
        Dim CloneMat As Material

        ' Make sure they send a valid material
        If IsNothing(SentMaterial) Then
            Exit Sub
        End If

        CloneMat = CType(SentMaterial.Clone, Material)

        ' Find the item
        MaterialtoFind = CloneMat
        TempMat = MaterialList.Find(AddressOf FindMaterial)

        If TempMat IsNot Nothing Then
            ' Remove the mat, and update the temp quantity to save later
            MaterialList.Remove(TempMat)
            TempMat.AddQuantity(CloneMat.GetQuantity)
        Else
            TempMat = CloneMat
        End If

        If OverrideCost <> -1 Then
            TempMat.SetTotalCost(OverrideCost)
        End If

        ' Add the material and update totals
        MaterialList.Add(TempMat)

        ' Update the total cost of the class
        TotalMaterialsCost = TotalMaterialsCost + CloneMat.GetTotalCost

        ' Update the total material volume for the list
        TotalMaterialsVolume = TotalMaterialsVolume + CloneMat.GetTotalVolume

    End Sub

    ' Multiplies the quantity of each material in the list by the sent multiple
    Public Sub MultiplyMaterials(ByVal SentMultiple As Integer)

        If SentMultiple <= 1 Then
            Exit Sub
        End If

        ' See if the material is in the list
        If Not IsNothing(MaterialList) Then
            ' Reset the totals
            TotalMaterialsCost = 0
            TotalMaterialsVolume = 0
            For i = 0 To MaterialList.Count - 1
                ' Loop through and multiply everything
                MaterialList(i).AddQuantity(MaterialList(i).GetQuantity * SentMultiple)
                ' Update the totals
                TotalMaterialsCost = TotalMaterialsCost + MaterialList(i).GetTotalCost
                TotalMaterialsVolume = TotalMaterialsVolume + MaterialList(i).GetTotalVolume
            Next
        End If

    End Sub

    ' Removes a Single material from the list
    Public Sub RemoveMaterial(ByVal SentMaterial As Material)
        Dim TempMat As Material

        ' Make sure they send a valid material
        If IsNothing(SentMaterial) Then
            Exit Sub
        End If

        ' Find the item and remove it from the list
        MaterialtoFind = SentMaterial
        TempMat = MaterialList.Find(AddressOf FindMaterial)

        If TempMat IsNot Nothing Then
            ' Remove from list first
            MaterialList.Remove(TempMat)
            ' If the quantity is not the same, then update the temp and re-add
            If TempMat.GetQuantity <> SentMaterial.GetQuantity Then
                ' Just update quantity (negative to remove), material function will update volume and cost
                TempMat.AddQuantity(-1 * SentMaterial.GetQuantity)
                ' Add it back
                MaterialList.Add(TempMat)
            End If
        End If

        ' Update the total cost of the class
        TotalMaterialsCost = TotalMaterialsCost - SentMaterial.GetTotalCost

        ' Update the total material volume for the list
        TotalMaterialsVolume = TotalMaterialsVolume - SentMaterial.GetTotalVolume

    End Sub

    ' Resets the value of the list to the sent value
    Public Sub ResetTotalValue(ByVal TotalValue As Double)
        TotalMaterialsCost = TotalValue
    End Sub

    ' Adds value to the total value of the list 
    Public Sub AddTotalValue(ByVal TotalValue As Double)
        TotalMaterialsCost = TotalMaterialsCost + TotalValue
    End Sub

    ' Adds volume to the total volume of the list
    Public Sub AddTotalVolume(ByVal TotalVolume As Double)
        TotalMaterialsVolume = TotalMaterialsVolume + TotalVolume
    End Sub

    ' "Adds" taxes to the total materials - i.e. takes off the taxes for selling these materials
    Public Sub AdjustTaxedPrice(ByVal TotalTax As Double)
        TotalMaterialsCost = TotalMaterialsCost - TotalTax
    End Sub

    ' Returns the list of Materials
    Public Function GetMaterialList() As List(Of Material)
        Return MaterialList
    End Function

    ' Sorts the Materials by quantity decending (Add more options later)
    Public Sub SortMaterialListByQuantity()
        If Not IsNothing(MaterialList) Then
            If MaterialList.Count - 1 > 0 Then
                ' Sort the list by quantity
                Call SortListDesc(MaterialList, 0, MaterialList.Count - 1)
            End If
        End If
    End Sub

    ' Returns the list in a clipboard format with CSV as an option - Include ME will include both the ME and the num Bps
    Public Function GetClipboardList(ByVal ExportTextFormat As String, ByVal IgnorePriceVolume As Boolean,
                                     ByVal IncludeME As Boolean, ByVal IncludeDecryptorRelic As Boolean, IncludeLinks As Boolean,
                                     Optional IncludeRunsonName As Boolean = False) As String
        Dim i As Integer
        Dim OutputString As String
        Dim MatName As String
        Dim DataInterfaces As String = ""
        Dim OutputME As String
        Dim RelicDecryptorText As String = ""
        Dim NumBps As String = ""
        Dim Location As String = ""
        Dim Separator As String = ""

        Dim BuildMaterialFieldsCSV = "ME, NumBPs, Decryptor/Relic, "
        Dim BuildMaterialFieldsSSV = "ME, NumBPs, Decryptor/Relic, "

        If Not IsNothing(MaterialList) Then

            Select Case ExportTextFormat
                Case CSVDataExport
                    Separator = ", "
                    OutputString = "Material, Quantity, "
                    If IncludeME Then
                        OutputString = OutputString & "ME, NumBPs, "
                    End If
                    If IncludeDecryptorRelic Then
                        OutputString = OutputString & "Decryptor/Relic, "
                    End If
                    OutputString = OutputString & "Cost Per Item, Total Cost, Location" & vbCrLf

                Case SSVDataExport
                    Separator = "; "
                    OutputString = "Material; Quantity; "
                    If IncludeME Then
                        OutputString = OutputString & "ME; NumBPs; "
                    End If
                    If IncludeDecryptorRelic Then
                        OutputString = OutputString & "Decryptor/Relic; "
                    End If
                    OutputString = OutputString & "Cost Per Item; Total Cost; Location" & vbCrLf
                Case MultiBuyDataExport
                    OutputString = "" ' no header
                Case Else ' Default
                    OutputString = "Material - Quantity" & vbCrLf
            End Select

            ' Loop through all materials
            For i = 0 To MaterialList.Count - 1

                If IncludeLinks And ExportTextFormat <> MultiBuyDataExport Then
                    ' Format so users can link in game
                    '<a href=showinfo:3348>Warfare Link</a> modules
                    MatName = "<a href=showinfo:" & MaterialList(i).GetMaterialTypeID & ">" & MaterialList(i).GetMaterialName & "</a>"
                ElseIf IncludeRunsonName Then
                    MatName = MaterialList(i).GetMaterialName
                Else
                    MatName = RemoveItemNameRuns(MaterialList(i).GetMaterialName)
                End If

                If MaterialList(i).GroupName.Contains("|") Then
                    ' We have a material from the shopping list, with three values in the material group
                    '.BuildType & "|" & .DecryptorRelic & "|" & .NumBPs & "|" & .Location
                    ' Parse the fields
                    Dim ItemColumns As String() = MaterialList(i).GroupName.Split(New [Char]() {"|"c})

                    If ItemColumns(1) <> None And ItemColumns(1) <> "" Then
                        RelicDecryptorText = ItemColumns(1)
                    Else
                        RelicDecryptorText = None
                    End If

                    NumBps = ItemColumns(2)
                    Location = ItemColumns(4)
                Else
                    RelicDecryptorText = None
                    NumBps = "-"
                End If

                If IncludeME Then
                    OutputME = MaterialList(i).GetItemME
                    ' If we are including an ME, then we are building something
                    ' If no numbp sent then set to 1 for now - TODO-MBPS will affect multiple bps
                    If NumBps = "-" Then
                        NumBps = "1"
                    End If
                Else
                    OutputME = "-"
                End If

                If ExportTextFormat = CSVDataExport Or ExportTextFormat = SSVDataExport Then
                    ' Format output for no commas in prices or quantity
                    OutputString = OutputString & MatName & Separator & CStr(MaterialList(i).GetQuantity) & Separator

                    If IncludeME Then
                        OutputString = OutputString & OutputME & Separator & CStr(NumBps) & Separator
                    End If

                    If IncludeDecryptorRelic Then
                        OutputString = OutputString & RelicDecryptorText & Separator
                    End If

                    OutputString = OutputString & CStr(MaterialList(i).GetCostPerItem) & Separator & CStr(MaterialList(i).GetTotalCost)

                    If Location <> "" Then
                        OutputString = OutputString & Separator & Location
                    End If

                    OutputString = OutputString & vbCrLf

                ElseIf ExportTextFormat = MultiBuyDataExport Then
                    ' Just the name and quantity for use in evepraisal etc.
                    OutputString = OutputString & MatName & " " & MaterialList(i).GetQuantity & vbCrLf
                Else
                    OutputString = OutputString & MatName

                    If IncludeME Or IncludeDecryptorRelic Then
                        OutputString = OutputString & " (" ' Adding something so start the parens

                        If OutputME <> "-" Then
                            OutputString = OutputString & "ME: " & OutputME
                            OutputString = OutputString & ", NumBPs: " & CStr(NumBps)
                        End If

                        If RelicDecryptorText <> "" And RelicDecryptorText <> None And IncludeDecryptorRelic Then
                            If RelicDecryptorText.Contains(IntactRelic) Or RelicDecryptorText.Contains(MalfunctioningRelic) Or RelicDecryptorText.Contains(WreckedRelic) Then
                                OutputString = OutputString & ", Relic: " & RelicDecryptorText
                            Else
                                ' Decryptor
                                OutputString = OutputString & ", Decryptor: " & RelicDecryptorText
                            End If
                        End If

                        OutputString = OutputString & ")"

                    End If

                    If Not MatName.Contains("Data Interface") Then
                        OutputString = OutputString & " - " & FormatNumber(MaterialList(i).GetQuantity, 0)
                    End If

                    If Location <> "" Then
                        OutputString = OutputString & vbCrLf & "Location: " & Location
                    End If

                    OutputString = OutputString & vbCrLf

                End If
SkipFormat:
            Next

            If ExportTextFormat <> MultiBuyDataExport Then
                ' Add total volume and cost to end
                If Not IgnorePriceVolume Then

                    OutputString = OutputString & DataInterfaces

                    If ExportTextFormat = CSVDataExport Or ExportTextFormat = SSVDataExport Then
                        Separator = Trim(Separator) ' Remove space
                        OutputString = OutputString & vbCrLf & "Total Volume of Materials:" & Separator & CStr(TotalMaterialsVolume) & Separator & "m3"
                        OutputString = OutputString & vbCrLf & "Total Cost of Materials:" & Separator & CStr(TotalMaterialsCost) & Separator & "ISK"
                    Else
                        OutputString = OutputString & vbCrLf & "Total Volume of Materials: " & FormatNumber(TotalMaterialsVolume, 2) & " m3"
                        OutputString = OutputString & vbCrLf & "Total Cost of Materials: " & FormatNumber(TotalMaterialsCost, 2) & " ISK"
                    End If
                End If

                ' Finally, if the export type is ssv, replace periods with commas
                If ExportTextFormat = SSVDataExport Then
                    OutputString = ConvertUStoEUDecimal(OutputString)
                End If
            End If

            GetClipboardList = OutputString
        Else
            GetClipboardList = "No items in List" & vbCrLf
        End If

    End Function

    ' Returns the total cost of the material list
    Public Function GetTotalMaterialsCost() As Double
        Return TotalMaterialsCost
    End Function

    ' Returns the total volume of the matierals in the list
    Public Function GetTotalVolume() As Double
        Return TotalMaterialsVolume
    End Function

    ' Sorts the material list by quantity
    Private Sub SortListDesc(ByVal MatList As List(Of Material), ByVal First As Integer, ByVal Last As Integer)
        Dim LowIndex As Integer
        Dim HighIndex As Integer
        Dim MidValue As Long

        ' Quicksort
        LowIndex = First
        HighIndex = Last
        MidValue = MatList((First + Last) \ 2).GetQuantity

        Do
            While MatList(LowIndex).GetQuantity > MidValue
                LowIndex = LowIndex + 1
            End While

            While MatList(HighIndex).GetQuantity < MidValue
                HighIndex = HighIndex - 1
            End While

            If LowIndex <= HighIndex Then
                Swap(LowIndex, HighIndex)
                LowIndex = LowIndex + 1
                HighIndex = HighIndex - 1
            End If
        Loop While LowIndex <= HighIndex

        If First < HighIndex Then
            SortListDesc(MatList, First, HighIndex)
        End If

        If LowIndex < Last Then
            SortListDesc(MatList, LowIndex, Last)
        End If

    End Sub

    ' This swaps the list values
    Private Sub Swap(ByRef IndexA As Integer, ByRef IndexB As Integer)
        Dim Temp As Material

        Temp = MaterialList(IndexA)
        MaterialList(IndexA) = MaterialList(IndexB)
        MaterialList(IndexB) = Temp

    End Sub

    ' Returns boolean comparison of two material lists
    Public Function MaterialListsEqual(ByVal List1 As Materials, ByVal List2 As Materials) As Boolean
        Dim i, j As Integer
        Dim Matlist1, Matlist2 As List(Of Material)
        Dim ItemFound As Boolean

        Matlist1 = List1.GetMaterialList
        Matlist2 = List2.GetMaterialList

        For i = 0 To Matlist1.Count - 1
            For j = 0 To Matlist2.Count - 1
                ' Looking for the item first, if not found then not equal
                ItemFound = False
                If Matlist1(i).GetMaterialName = Matlist2(j).GetMaterialName Then
                    ItemFound = True
                    If Matlist1(i).GetMaterialTypeID <> Matlist2(j).GetMaterialTypeID Then
                        Return False
                    End If
                    If Matlist1(i).GroupName <> Matlist2(j).GroupName Then
                        Return False
                    End If
                    If Matlist1(i).GetQuantity <> Matlist2(j).GetQuantity Then
                        Return False
                    End If
                    If Matlist1(i).GetVolume <> Matlist2(j).GetVolume Then
                        Return False
                    End If
                    If Matlist1(i).GetCostPerItem <> Matlist2(j).GetCostPerItem Then
                        Return False
                    End If
                    If Matlist1(i).GetItemME <> Matlist2(j).GetItemME Then
                        Return False
                    End If
                    If Matlist1(i).GetBuildItem <> Matlist2(j).GetBuildItem Then
                        Return False
                    End If
                    If Matlist1(i).GetTotalVolume <> Matlist2(j).GetTotalVolume Then
                        Return False
                    End If
                    If Matlist1(i).GetTotalCost <> Matlist2(j).GetTotalCost Then
                        Return False
                    End If
                    If Matlist1(i).GetItemType <> Matlist2(j).GetItemType Then
                        Return False
                    End If
                End If

                If ItemFound Then
                    ' Exit the loop if we found it
                    Exit For
                End If
            Next

            If Not ItemFound Then
                Return False
            End If

        Next

        Return True

    End Function

    ' Predicate for finding an item in the profit list
    Private Function FindMaterial(ByVal Mat As Material) As Boolean
        If Mat.GetMaterialName = MaterialtoFind.GetMaterialName And _
            Mat.GroupName = MaterialtoFind.GroupName And _
            Mat.GetItemME = MaterialtoFind.GetItemME Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
