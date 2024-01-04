Imports System.Data.SQLite

Public Class EVELoyaltyPoints

    Private LoyaltyPointsList As List(Of LoyaltyPointListing)

    Public Sub New()

        LoyaltyPointsList = New List(Of LoyaltyPointListing)

    End Sub

    Public Sub LoadLoyaltyPoints(ByVal CharacterID As Long, ByVal TokenData As SavedTokenData)
        Dim readerResearch As SQLiteDataReader
        Dim SQL As String
        Dim TempEntry As New LoyaltyPointListing

        ' Update them first
        Call UpdateLoyaltyPointListings(CharacterID, TokenData)

        ' Load the data from the DB
        SQL = "SELECT CORPORATION_ID, LOYALTY_POINTS FROM ESI_CHARACTER_LOYALTY_POINTS WHERE CHARACTER_ID = " & CStr(CharacterID) & " "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerResearch = DBCommand.ExecuteReader

        ' New list
        LoyaltyPointsList = New List(Of LoyaltyPointListing)

        While readerResearch.Read()
            TempEntry.CorporationID = readerResearch.GetInt64(0)
            TempEntry.LoyaltyPoints = readerResearch.GetInt64(1)

            ' Add the agent to the list
            LoyaltyPointsList.Add(TempEntry)

        End While

        readerResearch.Close()

    End Sub

    Private Sub UpdateLoyaltyPointListings(ByVal CharacterID As Long, ByVal CharacterTokenData As SavedTokenData)
        ' Refresh the data from the API
        Dim SQL As String = ""
        Dim CurrentPoints As List(Of ESILoyaltyPoints) = Nothing

        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Get the current list of agents updated
        If CB.DataUpdateable(CacheDateType.PersonalLoyaltyPoints, CharacterID) Then
            CurrentPoints = ESIData.GetCharacterLoyaltyPoints(CharacterID, CharacterTokenData, CacheDate)

            If Not IsNothing(CurrentPoints) Then
                If CurrentPoints.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Delete all the current records and refresh them
                    SQL = "DELETE FROM ESI_CHARACTER_LOYALTY_POINTS WHERE CHARACTER_ID = " & CStr(CharacterID)
                    Call EVEDB.ExecuteNonQuerySQL(SQL)


                    ' Insert new data
                    For i = 0 To CurrentPoints.Count - 1
                        With CurrentPoints(i)
                            SQL = "INSERT INTO ESI_CHARACTER_LOYALTY_POINTS (CHARACTER_ID, CORPORATION_ID, LOYALTY_POINTS) VALUES "
                            SQL &= "(" & CStr(CharacterID) & "," & CStr(.corporation_id) & "," & CStr(.loyalty_points) & ")"
                        End With

                        EVEDB.ExecuteNonQuerySQL(SQL)
                    Next

                    Call EVEDB.CommitSQLiteTransaction()
                End If
                ' All set, update cache date before leaving
                Call CB.UpdateCacheDate(CacheDateType.PersonalLoyaltyPoints, CacheDate, CharacterID)
            End If
        End If

    End Sub

    Public Function GetLoyaltyPoints() As List(Of LoyaltyPointListing)
        Return LoyaltyPointsList
    End Function

End Class

Public Structure LoyaltyPointListing
    Public CorporationID As Long
    Public LoyaltyPoints As Long
End Structure
