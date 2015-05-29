
Imports System.Data.SQLite

' List of decryptors for use in Manufacturing tab
Public Class DecryptorList

    Private Decryptors() As Decryptor
    Private RaceID As Integer

    ' All decryptors are merged with pheobe
    Public Sub New()
        Dim readerDecryptor As SQLiteDataReader
        Dim SQL As String

        SQL = "SELECT typeName, groupID FROM INVENTORY_TYPES WHERE groupID = " & DecryptorGroup

        DBCommand = New SQLiteCommand(SQL, DB)
        readerDecryptor = DBCommand.ExecuteReader

        While readerDecryptor.Read
            Call LoadRacialDecryptor(readerDecryptor.GetString(0))
        End While

        readerDecryptor.Close()
        readerDecryptor = Nothing
        DBCommand = Nothing

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

    ' Loads the racial decryptor into the class array list
    Private Sub LoadRacialDecryptor(ByVal DecryptorName As String)
        Dim readerDecryptor As SQLiteDataReader
        Dim SQL As String
        Dim TempDecryptor As Decryptor

        ' Set the Decryptor first
        SQL = "SELECT INVENTORY_TYPES.typeID, attributeName, valueFloat "
        SQL = SQL & "FROM INVENTORY_TYPES, TYPE_ATTRIBUTES, ATTRIBUTE_TYPES "
        SQL = SQL & "WHERE TYPE_ATTRIBUTES.typeID = INVENTORY_TYPES.typeID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.attributeID = ATTRIBUTE_TYPES.attributeID "
        SQL = SQL & "AND INVENTORY_TYPES.typeName = '" & DecryptorName & "'"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerDecryptor = DBCommand.ExecuteReader

        If Not IsNothing(readerDecryptor) Then
            TempDecryptor.Name = DecryptorName
            While readerDecryptor.Read
                TempDecryptor.TypeID = readerDecryptor.GetInt64(0)

                Select Case readerDecryptor.GetString(1)
                    Case "inventionPropabilityMultiplier"
                        TempDecryptor.ProductionMod = readerDecryptor.GetDouble(2)
                    Case "inventionMEModifier"
                        TempDecryptor.MEMod = CInt(readerDecryptor.GetDouble(2))
                    Case "inventionTEModifier"
                        TempDecryptor.TEMod = CInt(readerDecryptor.GetDouble(2))
                    Case "inventionMaxRunModifier"
                        TempDecryptor.RunMod = CInt(readerDecryptor.GetDouble(2))
                End Select
            End While
            ' Insert the decryptor into the main list
            Call InsertDecryptor(TempDecryptor)
        End If

        readerDecryptor.Close()
        readerDecryptor = Nothing
        DBCommand = Nothing

    End Sub

    ' Inserts a decryptor into the list and auto increments the array
    Private Sub InsertDecryptor(ByVal SentDecryptor As Decryptor)
        Dim TempDecryptors() As Decryptor
        Dim i As Integer

        If IsNothing(Decryptors) Then
            ReDim Decryptors(0)
            Decryptors(0) = SentDecryptor
        Else
            ' Copy old data
            TempDecryptors = Decryptors
            ReDim Decryptors(TempDecryptors.Count)

            For i = 0 To TempDecryptors.Count - 1
                Decryptors(i).Name = TempDecryptors(i).Name
                Decryptors(i).TypeID = TempDecryptors(i).TypeID
                Decryptors(i).MEMod = TempDecryptors(i).MEMod
                Decryptors(i).TEMod = TempDecryptors(i).TEMod
                Decryptors(i).RunMod = TempDecryptors(i).RunMod
                Decryptors(i).ProductionMod = TempDecryptors(i).ProductionMod
            Next

            ' Add the sent
            Decryptors(i).Name = SentDecryptor.Name
            Decryptors(i).TypeID = SentDecryptor.TypeID
            Decryptors(i).MEMod = SentDecryptor.MEMod
            Decryptors(i).TEMod = SentDecryptor.TEMod
            Decryptors(i).RunMod = SentDecryptor.RunMod
            Decryptors(i).ProductionMod = SentDecryptor.ProductionMod

        End If

    End Sub

End Class

Public Structure Decryptor
    Dim Name As String
    Dim TypeID As Long
    Dim MEMod As Integer
    Dim TEMod As Integer
    Dim RunMod As Integer
    Dim ProductionMod As Double
End Structure

Public Module DecryptorVariables
    ' Set the dummy decryptor in case one not entered or we don't want to send one when one entered
    Public NoDecryptor As Decryptor
End Module