
Imports System.Data.SQLite

Public Class StationLocation

    Private StationLocations As List(Of SystemRegion)
    Private StationIDtoFind As Long

    ' Basically load up all station location data for easy look up
    Public Sub New()
        Dim SQL As String
        Dim rsStations As SQLiteDataReader
        Dim TempInfo As SystemRegion

        ' Get a few fields first - Get the region and system id from the location of the station
        SQL = "SELECT STATION_ID, SOLAR_SYSTEM_ID, REGION_ID FROM STATIONS"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsStations = DBCommand.ExecuteReader

        StationLocations = New List(Of SystemRegion)

        While rsStations.Read()
            TempInfo.StationID = rsStations.GetInt64(0)
            TempInfo.RegionID = rsStations.GetInt64(1)
            TempInfo.SystemID = rsStations.GetInt64(2)

            StationLocations.Add(TempInfo)

        End While

        rsStations.Close()
        DBCommand = Nothing

    End Sub

    Public Function FindStationInfo(LocationID As Long) As SystemRegion
        StationIDtoFind = LocationID
        Return StationLocations.Find(AddressOf FindStation)
    End Function

    ' Predicate for finding an item in the list of stations
    Private Function FindStation(ByVal Record As SystemRegion) As Boolean
        If Record.StationID = StationIDtoFind Then
            Return True
        Else
            Return False
        End If
    End Function

End Class

Public Structure SystemRegion
    Dim StationID As Long
    Dim RegionID As Long
    Dim SystemID As Long
End Structure