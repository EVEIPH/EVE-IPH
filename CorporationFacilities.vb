Imports System.Data.SQLite

Public Class CorporationFacilities

    Private FacilityList As List(Of CorpFacility)
    Private KeyData As APIKeyData
    Private CorpID As Long

    Private CacheDate As Date

    Public Sub New(ByVal Key As APIKeyData, ByVal CorporationID As Long)

        If IsNothing(Key) Then
            KeyData = New APIKeyData
        Else
            KeyData = Key
        End If

        FacilityList = New List(Of CorpFacility)
        CorpID = CorporationID

        ' Default to this until we set it
        CacheDate = NoExpiry

    End Sub

    ' Loads all the corp facilities for the character from the DB
    Public Sub LoadCorpFacilities(UpdatefromAPI As Boolean)
        Dim SQL As String
        Dim readerFacility As SQLiteDataReader
        Dim TempFacility As CorpFacility
        Dim Facilities As New List(Of CorpFacility)

        If Not KeyData.Access Then
            ' They don't have access to the api so leave
            Exit Sub
        End If

        ' Update Industry jobs first
        Call UpdateCorpFacilities(UpdatefromAPI)

        ' Load the facilities
        SQL = "SELECT * FROM CORPORATION_FACILITIES WHERE corporationID=" & CorpID

        DBCommand = New SQLiteCommand(SQL, DB)
        readerFacility = DBCommand.ExecuteReader

        While readerFacility.Read

            With TempFacility
                .facilityID = readerFacility.GetDouble(0)
                .typeID = readerFacility.GetInt64(2)
                .typeName = readerFacility.GetString(3)
                .solarSystemID = readerFacility.GetInt64(4)
                .solarSystemName = readerFacility.GetString(5)
                .regionID = readerFacility.GetInt64(6)
                .regionName = readerFacility.GetString(7)
                .starbaseModifier = readerFacility.GetDouble(8)
                .tax = readerFacility.GetDouble(9)
            End With

            ' Insert standing
            Facilities.Add(TempFacility)

        End While

        readerFacility.Close()
        DBCommand = Nothing
        readerFacility = Nothing

        FacilityList = Facilities

    End Sub

    ' Updates the corporation facilities from API for the corp and inserts them into the Database for later queries
    Private Sub UpdateCorpFacilities(UpdateAPI As Boolean)
        Dim readerJobs As SQLiteDataReader
        Dim SQL As String
        Dim RefreshDate As Date ' To check the update of the API.
        Dim API As New EVEAPI
        Dim Facilities As New List(Of CorpFacility)

        ' See if we are doing an API update 
        If Not UpdateAPI Then
            Exit Sub
        End If

        ' Look up the facilities cache date first, if past the date, update the database
        SQL = "SELECT FACILITIES_CACHED_UNTIL FROM API "
        SQL = SQL & "WHERE CORPORATION_ID = " & CorpID
        SQL = SQL & " AND API_TYPE = 'Corporation'"

        DBCommand = New SQLiteCommand(SQL, DB)
        readerJobs = DBCommand.ExecuteReader

        If readerJobs.Read Then
            If Not IsDBNull(readerJobs.GetValue(0)) Then
                If readerJobs.GetString(0) = "" Then
                    RefreshDate = NoDate
                Else
                    RefreshDate = CDate(readerJobs.GetString(0))
                End If
            Else
                RefreshDate = NoDate
            End If
        Else
            RefreshDate = NoDate
        End If

        ' See if we refresh the data 
        If RefreshDate <= DateTime.UtcNow Then

            Facilities = API.GetCorpFacilities(KeyData, CacheDate)

            If Not NoAPIError(API.GetErrorText, "Corporation") Then
                ' Errored, exit
                Exit Sub
            End If

            ' Begin session
            Call BeginSQLiteTransaction()

            ' Update the cache date
            SQL = "UPDATE API SET FACILITIES_CACHED_UNTIL = '" & Format(CacheDate, SQLiteDateFormat)
            SQL = SQL & "' WHERE CORPORATION_ID = " & CorpID
            SQL = SQL & " AND API_TYPE = 'Corporation'"

            Call ExecuteNonQuerySQL(SQL)

            ' Clear out all facilities for a fresh set each time
            SQL = "DELETE FROM CORPORATION_FACILITIES WHERE corporationID = " & CorpID
            Call ExecuteNonQuerySQL(SQL)

            If Not IsNothing(Facilities) Then

                ' Insert industry data
                For i = 0 To Facilities.Count - 1

                    With Facilities(i)

                        ' Insert it
                        SQL = "INSERT INTO CORPORATION_FACILITIES (facilityID, corporationID, typeID, typeName, solarSystemID, solarSystemName, "
                        SQL = SQL & "regionID, regionName, starbaseModifier, tax) "
                        SQL = SQL & "VALUES (" & .facilityID & "," & CorpID & "," & .typeID & ",'" & .typeName & "',"
                        SQL = SQL & .solarSystemID & ",'" & .solarSystemName & "'," & .regionID & ",'" & .regionName & "',"
                        SQL = SQL & .starbaseModifier & "," & .tax & ")"

                        Call ExecuteNonQuerySQL(SQL)

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

Public Structure CorpFacility
    Dim facilityID As Double
    Dim typeID As Long
    Dim typeName As String
    Dim solarSystemID As Long
    Dim solarSystemName As String
    Dim regionID As Long
    Dim regionName As String
    Dim starbaseModifier As Double
    Dim tax As Double
End Structure