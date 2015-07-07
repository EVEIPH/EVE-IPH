﻿
Imports System.Data.SQLite

Public Class EVEBlueprints
    Private BlueprintList As List(Of EVEBlueprint)
    Private KeyData As APIKeyData
    Private CorpID As Long

    Private CacheDate As Date

    Public Sub New(Optional ByVal Key As APIKeyData = Nothing, Optional ByVal CorporationID As Long = 0)

        If IsNothing(Key) Then
            KeyData = New APIKeyData
        Else
            KeyData = Key
        End If

        BlueprintList = New List(Of EVEBlueprint)

        ' Set for corp Blueprints
        CorpID = CorporationID

        ' Default to this until we set it
        CacheDate = NoExpiry

    End Sub

    ' Loads all blueprints for the character from the DB
    Public Sub LoadBlueprints(BlueprintType As ScanType, UpdatefromAPI As Boolean)
        Dim SQL As String
        Dim readerBlueprints As SQLiteDataReader
        Dim TempBlueprint As EVEBlueprint
        Dim Blueprints As New List(Of EVEBlueprint)
        Dim SearchID As Long

        If Not KeyData.Access Then
            ' They don't have access to the api so leave
            Exit Sub
        End If

        ' Update Industry Blueprints first
        Call UpdateBlueprints(BlueprintType, UpdatefromAPI)

        If BlueprintType = ScanType.Personal Then
            SearchID = KeyData.ID
        Else
            SearchID = CorpID
        End If

        ' Load the blueprints
        SQL = "SELECT ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, FLAG_ID, QUANTITY, ME, TE, "
        SQL = SQL & "RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS "
        SQL = SQL & "FROM OWNED_BLUEPRINTS WHERE USER_ID = " & SearchID

        DBCommand = New SQLiteCommand(SQL, DB)
        readerBlueprints = DBCommand.ExecuteReader

        While readerBlueprints.Read

            If IsDBNull(readerBlueprints.GetValue(0)) Then
                TempBlueprint.itemID = 0
            Else
                TempBlueprint.itemID = readerBlueprints.GetInt64(0)
            End If

            TempBlueprint.locationID = readerBlueprints.GetInt64(1)
            TempBlueprint.typeID = readerBlueprints.GetInt64(2)
            TempBlueprint.typeName = readerBlueprints.GetString(3)
            TempBlueprint.flagID = readerBlueprints.GetInt32(4)
            TempBlueprint.quantity = readerBlueprints.GetInt32(5)
            TempBlueprint.timeEfficiency = readerBlueprints.GetInt32(6)
            TempBlueprint.materialEfficiency = readerBlueprints.GetInt32(7)
            TempBlueprint.runs = readerBlueprints.GetInt32(8)
            TempBlueprint.BPType = CType(readerBlueprints.GetInt32(9), BPType)
            TempBlueprint.Owned = CBool(readerBlueprints.GetInt32(10))
            TempBlueprint.Scanned = CBool(readerBlueprints.GetInt32(11))
            TempBlueprint.Favorite = CBool(readerBlueprints.GetInt32(12))
            TempBlueprint.AdditionalCosts = readerBlueprints.GetDouble(13)

            ' Insert blueprint
            Blueprints.Add(TempBlueprint)

        End While

        readerBlueprints.Close()
        DBCommand = Nothing
        readerBlueprints = Nothing

        BlueprintList = Blueprints

    End Sub

    ' Updates Blueprints from API for the character/corp and inserts them into the Database for later queries
    Private Sub UpdateBlueprints(BlueprintType As ScanType, UpdateAPI As Boolean)
        Dim readerBlueprints As SQLiteDataReader
        Dim readerCheck As SQLiteDataReader
        Dim SQL As String
        Dim RefreshDate As Date ' To check the update of the API.
        Dim API As New EVEAPI
        Dim IndyBlueprints As New List(Of EVEBlueprint)
        Dim AccountSearchID As Long
        Dim InsertBP As Boolean
        Dim IgnoreBP As Boolean
        Dim ScannedFlag As Integer

        Dim MEValue As Double
        Dim TEValue As Double

        ' See if we are doing an API update 
        If Not UpdateAPI Then
            Exit Sub
        End If

        ' Look up the industry Blueprints cache date first, if past the date, update the database
        SQL = "SELECT BLUEPRINTS_CACHED_UNTIL FROM API "
        If BlueprintType = ScanType.Personal Then
            AccountSearchID = KeyData.ID
            SQL = SQL & "WHERE CHARACTER_ID = " & AccountSearchID
            SQL = SQL & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            ScannedFlag = 1
        Else
            AccountSearchID = CorpID
            SQL = SQL & "WHERE CORPORATION_ID = " & AccountSearchID
            SQL = SQL & " AND API_TYPE = 'Corporation'"
            ScannedFlag = 2
        End If

        DBCommand = New SQLiteCommand(SQL, DB)
        readerBlueprints = DBCommand.ExecuteReader

        If readerBlueprints.Read Then
            If Not IsDBNull(readerBlueprints.GetValue(0)) Then
                If readerBlueprints.GetString(0) = "" Then
                    RefreshDate = NoDate
                Else
                    RefreshDate = CDate(readerBlueprints.GetString(0))
                End If
            Else
                RefreshDate = NoDate
            End If
        Else
            RefreshDate = NoDate
        End If

        ' See if we refresh the data 
        If RefreshDate <= DateTime.UtcNow Then

            IndyBlueprints = API.GetBlueprints(KeyData, BlueprintType, CacheDate)

            If Not NoAPIError(API.GetErrorText, "Character") Then
                ' Errored, exit
                Exit Sub
            End If

            ' Begin session
            Call BeginSQLiteTransaction()

            ' Update the cache date
            SQL = "UPDATE API SET BLUEPRINTS_CACHED_UNTIL = '" & Format(CacheDate, SQLiteDateFormat)

            If BlueprintType = ScanType.Personal Then
                SQL = SQL & "' WHERE CHARACTER_ID = " & AccountSearchID
                SQL = SQL & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            Else
                SQL = SQL & "' WHERE CORPORATION_ID = " & AccountSearchID
                SQL = SQL & " AND API_TYPE = 'Corporation'"
            End If

            Call ExecuteNonQuerySQL(SQL)

            If Not IsNothing(IndyBlueprints) Then

                ' Insert blueprint data
                For i = 0 To IndyBlueprints.Count - 1
                    ' First make sure it's not already in there
                    With IndyBlueprints(i)

                        ' For now, only include unique BPs until I get the multiple BP support done - use Max ME for the determination or Max TE if they are the same ME
                        SQL = "SELECT ME, TE, BP_TYPE, ITEM_ID, OWNED FROM OWNED_BLUEPRINTS "
                        SQL = SQL & "WHERE BLUEPRINT_ID = " & .typeID & " And USER_ID = " & AccountSearchID & " "

                        DBCommand = New SQLiteCommand(SQL, DB)
                        readerBlueprints = DBCommand.ExecuteReader
                        readerBlueprints.Read()

                        If readerBlueprints.HasRows Then

                            ' Do not overwrite anything saved by the user (owned = -1 for user owned, 0 for not owned but favorite/ignore/bptype) 
                            If readerBlueprints.GetInt32(4) = 1 Then
                                MEValue = readerBlueprints.GetInt32(0)
                                TEValue = readerBlueprints.GetInt32(1)

                                ' If greater ME, or the ME is equal and TE from API is greater, update it if it's the same type of bp
                                If MEValue < IndyBlueprints(i).materialEfficiency And readerBlueprints.GetInt32(2) = .BPType Then
                                    InsertBP = False
                                    IgnoreBP = False
                                ElseIf MEValue = IndyBlueprints(i).materialEfficiency And TEValue < IndyBlueprints(i).timeEfficiency And readerBlueprints.GetInt32(2) = .BPType Then
                                    InsertBP = False
                                    IgnoreBP = False
                                ElseIf readerBlueprints.GetInt32(2) = BPType.Copy And .BPType = BPType.Original Then ' Only update if the new BP is a BPO
                                    InsertBP = False
                                    IgnoreBP = False
                                Else
                                    ' We don't want to do anything with this bp
                                    IgnoreBP = True
                                End If
                            Else
                                ' We don't want to do anything with this bp
                                IgnoreBP = True
                                InsertBP = False
                            End If
                        Else
                            IgnoreBP = False
                            InsertBP = True
                        End If

                        If Not IgnoreBP Then

                            ' Set the correct BP_Type for the BPs they have 
                            Dim CurrentBPType As BPType = .BPType
                            ' If T2 and a copy, set to invented copy if the ME/TE match, else use what was sent
                            If CurrentBPType = BPType.Copy Then
                                SQL = "SELECT TECH_LEVEL FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID = " & CStr(.typeID)
                                DBCommand = New SQLiteCommand(SQL, DB)
                                readerCheck = DBCommand.ExecuteReader
                                If readerCheck.Read() Then
                                    Dim TempDecryptorList As New DecryptorList
                                    Dim FoundDecryptor As Decryptor = TempDecryptorList.GetDecryptor(.materialEfficiency, .timeEfficiency, .runs, readerCheck.GetInt32(0))

                                    ' If it finds a decryptor, even no decryptor, then set it to invented, else assume it's a copy from a BPO
                                    If FoundDecryptor.TypeID <> 0 Or (.materialEfficiency = BaseT2T3ME And .timeEfficiency = BaseT2T3TE) Then
                                        CurrentBPType = BPType.InventedBPC
                                    End If

                                    readerCheck.Close()
                                End If
                                readerCheck = Nothing
                            End If

                            If InsertBP Then

                                SQL = "INSERT INTO OWNED_BLUEPRINTS (USER_ID, ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, FLAG_ID, "
                                SQL = SQL & "QUANTITY, ME, TE, RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS) "
                                SQL = SQL & "VALUES (" & CStr(AccountSearchID) & "," & CStr(.itemID) & "," & CStr(.locationID) & ","
                                SQL = SQL & CStr(.typeID) & ",'" & FormatDBString(.typeName) & "',"
                                SQL = SQL & CStr(.flagID) & ",1," & CStr(.materialEfficiency) & "," & CStr(.timeEfficiency) & ","
                                SQL = SQL & .runs & "," & CStr(CurrentBPType) & ",1," & CStr(ScannedFlag) & ", 0, 0)"
                            Else
                                ' Update the BP 
                                SQL = "UPDATE OWNED_BLUEPRINTS SET "
                                SQL = SQL & "LOCATION_ID = " & CStr(.locationID) & ","
                                SQL = SQL & "FLAG_ID = " & CStr(.flagID) & ","
                                SQL = SQL & "ME = " & CStr(.materialEfficiency) & ","
                                SQL = SQL & "TE = " & CStr(.timeEfficiency) & ","
                                SQL = SQL & "RUNS = " & CStr(.runs) & ","
                                SQL = SQL & "QUANTITY = 1," ' Helps determine copies (-2), bpos (-1), or stacks of BPO's (any number), 
                                ' but we will set for 1 now and later the total of BPS with the same ME/TE

                                ' Also reset the unqiue itemid
                                SQL = SQL & "ITEM_ID = " & CStr(.itemID) & ","
                                ' Could go from a copy to orginial (with single bp loading, will change with multi)
                                SQL = SQL & "BP_TYPE = " & CStr(CurrentBPType) & ","
                                ' Mark all from API as owned
                                SQL = SQL & "OWNED = 1,"
                                SQL = SQL & "BLUEPRINT_NAME = '" & FormatDBString(.typeName) & "' " ' If it changes

                                If readerBlueprints.GetInt64(3) <> 0 Then
                                    ' Search with ITEM_ID
                                    SQL = SQL & "WHERE ITEM_ID = " & CStr(readerBlueprints.GetInt64(3)) & " AND USER_ID = " & CStr(AccountSearchID)
                                Else
                                    ' Search with the ID of the bp and the user ID - they must have saved this manually
                                    SQL = SQL & "WHERE BLUEPRINT_ID = " & .typeID & " AND USER_ID = " & CStr(AccountSearchID)
                                End If

                            End If

                            readerBlueprints.Close()
                            readerBlueprints = Nothing

                            Call ExecuteNonQuerySQL(SQL)
                        End If

                    End With
                Next

                DBCommand = Nothing

            End If

            Call CommitSQLiteTransaction()

        End If

    End Sub

    ReadOnly Property CachedUntil() As Date
        Get
            Return CacheDate
        End Get
    End Property

End Class

' For outputing lists of blueprints
Public Structure EVEBlueprint
    Dim itemID As Long
    Dim locationID As Long
    Dim typeID As Long
    Dim typeName As String
    Dim flagID As Integer
    Dim quantity As Integer
    Dim timeEfficiency As Integer
    Dim materialEfficiency As Integer
    Dim runs As Integer
    Dim BPType As BPType
    Dim Owned As Boolean
    Dim Scanned As Boolean
    Dim Favorite As Boolean
    Dim AdditionalCosts As Double
End Structure
