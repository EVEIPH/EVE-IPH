
Imports System.Data.SQLite

' List of decryptors for use in Manufacturing tab
Public Class DecryptorList

    Private Decryptors As New List(Of Decryptor)
    Private RaceID As Integer
    Private DecryptortoFind As New Decryptor

    ' All decryptors are merged with pheobe
    Public Sub New()
        Dim readerDecryptor As SQLiteDataReader
        Dim SQL As String

        SQL = "SELECT typeName, groupID FROM INVENTORY_TYPES WHERE groupID = 1304" ' Only one Decryptor Group with Pheobe

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerDecryptor = DBCommand.ExecuteReader

        While readerDecryptor.Read
            Call LoadRacialDecryptor(readerDecryptor.GetString(0))
        End While

        readerDecryptor.Close()

    End Sub

    ' Function will return a decryptor of group and multiplier that is in the list, if not returns no decryptor
    Public Function GetDecryptor(ByVal ProbabilityMult As Double) As Decryptor
        Dim i As Integer

        For i = 0 To Decryptors.Count - 1
            If Decryptors(i).ProductionMod = ProbabilityMult Then
                Return (Decryptors(i))
            End If
        Next

        Return NoDecryptor

    End Function

    ' Function returns the full decryptor for the name sent
    Public Function GetDecryptor(ByVal DecryptorName As String) As Decryptor
        Dim i As Integer

        For i = 0 To Decryptors.Count - 1
            If Decryptors(i).Name = DecryptorName Then
                Return (Decryptors(i))
            End If
        Next

        Return NoDecryptor

    End Function

    ' Returns the list of decryptors
    Public Function GetDecryptorList() As List(Of Decryptor)
        Return Decryptors
    End Function

    ' Function returns the decryptor for the ME/TE/Runs values sent
    Public Function GetDecryptor(ByVal BPME As Integer, ByVal BPTE As Integer, ByVal BPRuns As Integer, ByVal TechLevel As Integer, _
                                 Optional ProbabilityModifier As Double = -1) As Decryptor

        Dim RunsModifier As Integer
        Dim MEModifier As Integer = BPME - BaseT2T3ME
        Dim TEModifier As Integer = BPTE - BaseT2T3TE

        If MEModifier = -2 And TEModifier = 2 Then
            ' We used the decryptor with max run modifier of 9 (hardcode to get around the decryptor with 9 extra runs for 1 run bpcs, which then makes 10 and is the same as the base for modules)
            RunsModifier = 9
        Else
            If TechLevel = 2 Then
                ' Either ships or modules
                If BPRuns >= 10 Then
                    RunsModifier = BPRuns - 10
                Else
                    RunsModifier = BPRuns - 1
                End If
            ElseIf TechLevel = 3 Then
                If BPRuns >= 3 Then
                    ' Wrecked
                    RunsModifier = BPRuns - 3
                ElseIf BPRuns >= 10 Then
                    ' Malfunctioning
                    RunsModifier = BPRuns - 10
                ElseIf BPRuns >= 20 Then
                    ' Intact
                    RunsModifier = BPRuns - 20
                End If
            End If
        End If

        For i = 0 To Decryptors.Count - 1
            With Decryptors(i)
                If .MEMod = MEModifier And .TEMod = TEModifier And .RunMod = RunsModifier And CBool(IIf(ProbabilityModifier <> -1, .ProductionMod = ProbabilityModifier, True)) Then
                    Return (Decryptors(i))
                End If
            End With
        Next

        ' If still not found, look for just ME and TE
        For i = 0 To Decryptors.Count - 1
            With Decryptors(i)
                If .MEMod = MEModifier And .TEMod = TEModifier Then
                    Return (Decryptors(i))
                End If
            End With
        Next

        Return NoDecryptor

    End Function

    ' Loads the racial decryptor into the class array list
    Private Sub LoadRacialDecryptor(ByVal DecryptorName As String)
        Dim readerDecryptor As SQLiteDataReader
        Dim SQL As String
        Dim TempDecryptor As New Decryptor

        ' Set the Decryptor first
        SQL = "SELECT INVENTORY_TYPES.typeID, attributeID, value "
        SQL &= "FROM INVENTORY_TYPES, TYPE_ATTRIBUTES "
        SQL &= "WHERE TYPE_ATTRIBUTES.typeID = INVENTORY_TYPES.typeID "
        SQL &= "AND INVENTORY_TYPES.typeName = '" & DecryptorName & "'"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerDecryptor = DBCommand.ExecuteReader

        If Not IsNothing(readerDecryptor) Then
            TempDecryptor.Name = DecryptorName
            While readerDecryptor.Read
                TempDecryptor.TypeID = readerDecryptor.GetInt64(0)

                Select Case readerDecryptor.GetInt32(1)
                    Case 1112
                        TempDecryptor.ProductionMod = readerDecryptor.GetDouble(2)
                    Case 1113
                        TempDecryptor.MEMod = CInt(readerDecryptor.GetDouble(2))
                    Case 1114
                        TempDecryptor.TEMod = CInt(readerDecryptor.GetDouble(2))
                    Case 1124
                        TempDecryptor.RunMod = CInt(readerDecryptor.GetDouble(2))
                End Select
            End While

            ' Insert the decryptor into the main list
            Dim FoundDecryptor As Decryptor
            DecryptortoFind = TempDecryptor
            FoundDecryptor = Decryptors.Find(AddressOf FindDecryptor)

            If FoundDecryptor Is Nothing Then
                Decryptors.Add(TempDecryptor)
            End If

        End If

        readerDecryptor.Close()

    End Sub

    ' Predicate for finding an item in the profit list
    Private Function FindDecryptor(ByVal Mat As Decryptor) As Boolean
        If Mat.Name = DecryptortoFind.Name And Mat.TypeID = DecryptortoFind.TypeID And Mat.MEMod = DecryptortoFind.MEMod And _
            Mat.RunMod = DecryptortoFind.RunMod And Mat.ProductionMod = DecryptortoFind.ProductionMod Then
            Return True
        Else
            Return False
        End If
    End Function

End Class

Public Class Decryptor
    Public Name As String
    Public TypeID As Long
    Public MEMod As Integer
    Public TEMod As Integer
    Public RunMod As Integer
    Public ProductionMod As Double

    Public Sub New()
        Name = "None"
        TypeID = 0
        MEMod = 0
        TEMod = 0
        RunMod = 0
        ProductionMod = 1.0
    End Sub
End Class

Public Module DecryptorVariables
    ' Set the dummy decryptor in case one not entered or we don't want to send one when one entered
    Public NoDecryptor As New Decryptor
End Module