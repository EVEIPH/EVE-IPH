
Imports System.Data.SQLite

Public Class EVEBlueprints
    Private BlueprintList As List(Of EVEBlueprint)
    Private KeyData As SavedTokenData

    Public Sub New()

        BlueprintList = New List(Of EVEBlueprint)

    End Sub

    ' Loads all blueprints for the character from the DB
    Public Sub LoadBlueprints(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData, ByVal BlueprintType As ScanType, ByVal UpdateBPs As Boolean)
        Dim SQL As String
        Dim readerBlueprints As SQLiteDataReader
        Dim TempBlueprint As EVEBlueprint
        Dim Blueprints As New List(Of EVEBlueprint)

        ' Update Industry Blueprints first
        Call UpdateBlueprints(ID, CharacterTokenData, BlueprintType, UpdateBPs)

        ' See what ID we use for character bps
        Dim CharID As Long = 0
        If UserApplicationSettings.LoadBPsbyChar Then
            ' Use the ID sent
            CharID = SelectedCharacter.ID
        Else
            CharID = CommonLoadBPsID
        End If

        ' Load the blueprints
        SQL = "SELECT ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, FLAG_ID, QUANTITY, ME, TE, "
        SQL = SQL & "RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS "
        SQL = SQL & "FROM OWNED_BLUEPRINTS WHERE USER_ID = " & CharID

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerBlueprints = DBCommand.ExecuteReader

        While readerBlueprints.Read

            TempBlueprint.ItemID = readerBlueprints.GetInt64(0)
            TempBlueprint.LocationID = readerBlueprints.GetInt64(1)
            TempBlueprint.TypeID = readerBlueprints.GetInt64(2)
            TempBlueprint.TypeName = readerBlueprints.GetString(3)
            TempBlueprint.FlagID = readerBlueprints.GetInt32(4)
            TempBlueprint.Quantity = readerBlueprints.GetInt32(5)
            TempBlueprint.TimeEfficiency = readerBlueprints.GetInt32(6)
            TempBlueprint.MaterialEfficiency = readerBlueprints.GetInt32(7)
            TempBlueprint.Runs = readerBlueprints.GetInt32(8)
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

    ' Updates Blueprints from ESI for the character/corp and inserts them into the Database for later queries
    Private Sub UpdateBlueprints(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData,
                                 ByVal BlueprintType As ScanType, ByVal UpdateBPs As Boolean)
        Dim readerBlueprints As SQLiteDataReader
        Dim readerCheck As SQLiteDataReader
        Dim SQL As String

        Dim IndyBlueprints As New List(Of EVEBlueprint)
        Dim InsertBP As Boolean
        Dim IgnoreBP As Boolean
        Dim ScannedFlag As Integer

        Dim MEValue As Double
        Dim TEValue As Double

        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        Dim CDType As CacheDateType

        If BlueprintType = ScanType.Personal Then
            CDType = CacheDateType.PersonalBlueprints
            ScannedFlag = 1
        Else
            CDType = CacheDateType.CorporateBlueprints
            ScannedFlag = 2
        End If

        ' See what ID we save for character bps
        Dim TempID As Long = 0
        If UserApplicationSettings.LoadBPsbyChar Or BlueprintType = ScanType.Corporation Then
            ' Use the ID sent
            TempID = ID
        Else
            TempID = CommonLoadBPsID
        End If

        ' Look up the industry Blueprints cache date first      
        If CB.DataUpdateable(CDType, ID) Then
            IndyBlueprints = ESIData.GetBlueprints(ID, CharacterTokenData, BlueprintType, CacheDate)

            If Not IsNothing(IndyBlueprints) Then
                If IndyBlueprints.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' First delete all bps for this ID in the 
                    Call EVEDB.ExecuteNonQuerySQL("DELETE FROM ALL_OWNED_BLUEPRINTS WHERE OWNER_ID = " & CStr(TempID))

                    ' Insert blueprint data
                    For i = 0 To IndyBlueprints.Count - 1

                        With IndyBlueprints(i)
                            ' Load all bps in ALL_OWNED_BLUEPRINTS and only limit OWNED_BLUEPRINTS to single records
                            SQL = "INSERT INTO ALL_OWNED_BLUEPRINTS (OWNER_ID, ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, FLAG_ID, "
                            SQL = SQL & "QUANTITY, ME, TE, RUNS, BP_TYPE) "
                            SQL = SQL & "VALUES (" & CStr(TempID) & "," & CStr(.ItemID) & "," & CStr(.LocationID) & ","
                            SQL = SQL & CStr(.TypeID) & ",'" & FormatDBString(.TypeName) & "',"
                            SQL = SQL & CStr(.FlagID) & ",1," & CStr(.MaterialEfficiency) & "," & CStr(.TimeEfficiency) & ","
                            SQL = SQL & .Runs & "," & CStr(.BPType) & ")"

                            Call EVEDB.ExecuteNonQuerySQL(SQL)

                            ' Make sure it's not already in there before adding to owned
                            ' For now, only include unique BPs until I get the multiple BP support done - use Max ME for the determination or Max TE if they are the same ME
                            SQL = "SELECT ME, TE, BP_TYPE, ITEM_ID, OWNED, SCANNED FROM OWNED_BLUEPRINTS "
                            SQL = SQL & "WHERE BLUEPRINT_ID = " & .TypeID & " And USER_ID = " & CStr(TempID)

                            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                            readerBlueprints = DBCommand.ExecuteReader
                            readerBlueprints.Read()

                            If readerBlueprints.HasRows Then

                                ' Do not overwrite anything saved by the user (owned = -1 for user owned, 0 for not owned but favorite/ignore/bptype) 
                                If readerBlueprints.GetInt32(4) = 1 Then
                                    MEValue = readerBlueprints.GetInt32(0)
                                    TEValue = readerBlueprints.GetInt32(1)

                                    ' If greater ME, or the ME is equal and TE is greater, update it if it's the same type of bp
                                    If MEValue < IndyBlueprints(i).MaterialEfficiency And readerBlueprints.GetInt32(2) = .BPType Then
                                        InsertBP = False
                                        IgnoreBP = False
                                    ElseIf MEValue = IndyBlueprints(i).MaterialEfficiency And TEValue < IndyBlueprints(i).TimeEfficiency And readerBlueprints.GetInt32(2) = .BPType Then
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
                                    SQL = "SELECT TECH_LEVEL FROM ALL_BLUEPRINTS WHERE BLUEPRINT_ID = " & CStr(.TypeID)
                                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                                    readerCheck = DBCommand.ExecuteReader
                                    If readerCheck.Read() Then
                                        Dim TempDecryptorList As New DecryptorList
                                        Dim FoundDecryptor As Decryptor = TempDecryptorList.GetDecryptor(.MaterialEfficiency, .TimeEfficiency, .Runs, readerCheck.GetInt32(0))

                                        ' If it finds a decryptor, even no decryptor, then set it to invented, else assume it's a copy from a BPO
                                        If FoundDecryptor.TypeID <> 0 Or (.MaterialEfficiency = BaseT2T3ME And .TimeEfficiency = BaseT2T3TE) Then
                                            CurrentBPType = BPType.InventedBPC
                                        End If

                                        readerCheck.Close()
                                    End If

                                End If

                                If InsertBP Then
                                    SQL = "INSERT INTO OWNED_BLUEPRINTS (USER_ID, ITEM_ID, LOCATION_ID, BLUEPRINT_ID, BLUEPRINT_NAME, FLAG_ID, "
                                    SQL = SQL & "QUANTITY, ME, TE, RUNS, BP_TYPE, OWNED, SCANNED, FAVORITE, ADDITIONAL_COSTS) "
                                    SQL = SQL & "VALUES (" & CStr(TempID) & "," & CStr(.ItemID) & "," & CStr(.LocationID) & ","
                                    SQL = SQL & CStr(.TypeID) & ",'" & FormatDBString(.TypeName) & "',"
                                    SQL = SQL & CStr(.FlagID) & ",1," & CStr(.MaterialEfficiency) & "," & CStr(.TimeEfficiency) & ","
                                    SQL = SQL & .Runs & "," & CStr(CurrentBPType) & ",1," & CStr(ScannedFlag) & ", 0, 0)"
                                Else
                                    ' Update the BP 
                                    SQL = "UPDATE OWNED_BLUEPRINTS SET "
                                    SQL = SQL & "LOCATION_ID = " & CStr(.LocationID) & ","
                                    SQL = SQL & "FLAG_ID = " & CStr(.FlagID) & ","
                                    SQL = SQL & "ME = " & CStr(.MaterialEfficiency) & ","
                                    SQL = SQL & "TE = " & CStr(.TimeEfficiency) & ","
                                    SQL = SQL & "RUNS = " & CStr(.Runs) & ","
                                    SQL = SQL & "QUANTITY = 1," ' Helps determine copies (-2), bpos (-1), or stacks of BPO's (any number), 
                                    ' but we will set for 1 now and later the total of BPS with the same ME/TE

                                    ' Also reset the unqiue itemid
                                    SQL = SQL & "ITEM_ID = " & CStr(.ItemID) & ","
                                    ' Could go from a copy to orginial (with single bp loading, will change with multi)
                                    SQL = SQL & "BP_TYPE = " & CStr(CurrentBPType) & ","
                                    ' Mark all from ESI as owned
                                    SQL = SQL & "OWNED = 1,"
                                    SQL = SQL & "BLUEPRINT_NAME = '" & FormatDBString(.TypeName) & "', " ' If it changes
                                    SQL = SQL & "SCANNED = " & ScannedFlag & " "

                                    If readerBlueprints.GetInt64(3) <> 0 Then
                                        ' Search with ITEM_ID
                                        SQL = SQL & "WHERE ITEM_ID = " & CStr(readerBlueprints.GetInt64(3)) & " AND USER_ID = " & CStr(TempID)
                                    Else
                                        ' Search with the ID of the bp and the user ID - they must have saved this manually
                                        SQL = SQL & "WHERE BLUEPRINT_ID = " & .TypeID & " AND USER_ID = " & CStr(TempID)
                                    End If

                                End If

                                readerBlueprints.Close()

                                Call EVEDB.ExecuteNonQuerySQL(SQL)
                            End If

                        End With
                    Next

                    DBCommand = Nothing

                    Call EVEDB.CommitSQLiteTransaction()

                End If

                ' Update cache date now that it's all set
                Call CB.UpdateCacheDate(CDType, CacheDate, ID)

            End If
        End If

    End Sub

End Class

' For outputing lists of blueprints
Public Structure EVEBlueprint
    Dim ItemID As Long
    Dim LocationID As Long
    Dim TypeID As Long
    Dim TypeName As String
    Dim FlagID As Integer
    Dim Quantity As Integer
    Dim TimeEfficiency As Integer
    Dim MaterialEfficiency As Integer
    Dim Runs As Integer
    Dim BPType As BPType
    Dim Owned As Boolean
    Dim Scanned As Boolean
    Dim Favorite As Boolean
    Dim AdditionalCosts As Double
End Structure
