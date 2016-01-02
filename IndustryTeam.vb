Imports System.Data.SQLite

Public Class IndustryTeam
    Implements ICloneable

    Public TeamName As String
    Public TeamID As Long
    Public ActivityID As Integer
    Public CreationTime As DateTime
    Public ExpiryTime As DateTime
    Public CostModifier As Double

    Public SolarSystemID As Long
    Public SolarSystemName As String

    Public SpecializationCategoryID As Integer
    Public SpecializationCategory As String

    Public IsDefault As Boolean

    ' Should only have 4 bonuses but allow for any amount
    Public Bonuses As List(Of IndustryTeamBonus)

    Public Sub New()
        TeamID = 0
        TeamName = "No Team"
        SolarSystemID = 0
        SolarSystemName = None
        SpecializationCategory = None
        SpecializationCategoryID = 0
        ActivityID = 0
        Bonuses = New List(Of IndustryTeamBonus)
        CostModifier = 0
        CreationTime = NoDate
        ExpiryTime = NoDate
        IsDefault = False
    End Sub

    ' Loads up the team sent
    Public Sub LoadTeamFromSettings(SearchTeamSettings As TeamSettings)
        Dim SQL As String = ""
        Dim rsLoader As SQLiteDataReader
        Dim FoundTeam As New IndustryTeam
        Dim FoundBonus As New IndustryTeamBonus
        Dim FoundTeamBonuses As New List(Of IndustryTeamBonus)

        ' Only look up team if we have an id
        If SearchTeamSettings.TeamID <> 0 Then
            ' Look up team info
            SQL = "SELECT TEAM_ID, TEAM_NAME, TEAM_ACTIVITY_ID, SOLAR_SYSTEM_ID, SOLAR_SYSTEM_NAME, COST_MODIFIER, CREATION_TIME, EXPIRY_TIME,"
            SQL = SQL & "INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID, INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_NAME "
            SQL = SQL & "FROM INDUSTRY_TEAMS, INDUSTRY_CATEGORY_SPECIALTIES "
            SQL = SQL & "WHERE INDUSTRY_TEAMS.SPECIALTY_CATEGORY_ID = INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID "
            SQL = SQL & "AND TEAM_ID = " & SearchTeamSettings.TeamID & " "
            SQL = SQL & "UNION "
            SQL = SQL & "SELECT TEAM_ID, TEAM_NAME, TEAM_ACTIVITY_ID, SOLAR_SYSTEM_ID, SOLAR_SYSTEM_NAME, COST_MODIFIER, CREATION_TIME, EXPIRY_TIME,"
            SQL = SQL & "INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID, INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_NAME "
            SQL = SQL & "FROM INDUSTRY_TEAMS_AUCTIONS, INDUSTRY_CATEGORY_SPECIALTIES "
            SQL = SQL & "WHERE INDUSTRY_TEAMS_AUCTIONS.SPECIALTY_CATEGORY_ID = INDUSTRY_CATEGORY_SPECIALTIES.SPECIALTY_CATEGORY_ID "
            SQL = SQL & "AND TEAM_ID = " & SearchTeamSettings.TeamID & " "

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsLoader = DBCommand.ExecuteReader
            rsLoader.Read()

            If rsLoader.HasRows Then
                With FoundTeam
                    .TeamID = rsLoader.GetInt64(0)
                    .TeamName = rsLoader.GetString(1)
                    .ActivityID = rsLoader.GetInt32(2)
                    .SolarSystemID = rsLoader.GetInt64(3)
                    .SolarSystemName = rsLoader.GetString(4)
                    .CostModifier = rsLoader.GetDouble(5)
                    .CreationTime = CDate(rsLoader.GetString(6))
                    .ExpiryTime = CDate(rsLoader.GetString(7))
                    .SpecializationCategoryID = rsLoader.GetInt32(8)
                    .SpecializationCategory = rsLoader.GetString(9)
                End With

                rsLoader.Close()
                rsLoader = Nothing
                DBCommand = Nothing

                ' If we found a team, look up the bonuses
                If SearchTeamSettings.TeamID <> 0 Then
                    ' Look up the team or store the default
                    SQL = "SELECT TEAM_ID, TEAM_NAME, BONUS_ID, BONUS_TYPE, BONUS_VALUE, SPECIALTY_GROUP_NAME, INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID "
                    SQL = SQL & "FROM INDUSTRY_TEAMS_BONUSES, INDUSTRY_GROUP_SPECIALTIES WHERE TEAM_ID = " & SearchTeamSettings.TeamID & " "
                    SQL = SQL & "AND INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID = INDUSTRY_GROUP_SPECIALTIES.SPECIALTY_GROUP_ID "
                    SQL = SQL & "GROUP BY BONUS_ID, BONUS_TYPE, BONUS_VALUE, SPECIALTY_GROUP_NAME, INDUSTRY_TEAMS_BONUSES.SPECIALTY_GROUP_ID"

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLoader = DBCommand.ExecuteReader

                    While rsLoader.Read()
                        ' Get each bonus first
                        FoundBonus = New IndustryTeamBonus
                        With FoundBonus
                            .BonusID = rsLoader.GetInt32(2)
                            .BonusType = rsLoader.GetString(3)
                            .BonusValue = rsLoader.GetDouble(4)
                            .BonusSpecialtyGroupName = rsLoader.GetString(5)
                            .BonusSpecialtyGroupID = rsLoader.GetInt32(6)
                        End With

                        ' Add to list
                        FoundTeamBonuses.Add(FoundBonus)

                    End While

                    rsLoader.Close()
                    rsLoader = Nothing
                    DBCommand = Nothing
                End If
            Else
                FoundTeam = NoTeam
            End If

            TeamID = FoundTeam.TeamID
            TeamName = FoundTeam.TeamName
            SolarSystemID = FoundTeam.SolarSystemID
            SolarSystemName = FoundTeam.SolarSystemName
            SpecializationCategory = FoundTeam.SpecializationCategory
            SpecializationCategoryID = FoundTeam.SpecializationCategoryID
            ActivityID = FoundTeam.ActivityID
            Bonuses = FoundTeamBonuses
            CostModifier = FoundTeam.CostModifier
            CreationTime = FoundTeam.CreationTime
            ExpiryTime = FoundTeam.ExpiryTime

            ' Whenever we load from settings, it's the default team
            IsDefault = True

        End If

    End Sub

    Public Sub SaveTeam(SaveTeamType As TeamType, Tab As String)
        ' If we are saving, then set the data and then save
        Dim SaveSettings As TeamSettings

        ' Save data
        SaveSettings.TeamID = TeamID
        SaveSettings.TeamTab = Tab

        Call AllSettings.SaveTeamSettings(SaveSettings, SaveTeamType)

    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim CopyOfMe = New IndustryTeam

        CopyOfMe.TeamName = TeamName
        CopyOfMe.TeamID = TeamID
        CopyOfMe.ActivityID = ActivityID
        CopyOfMe.CreationTime = CreationTime
        CopyOfMe.ExpiryTime = ExpiryTime
        CopyOfMe.ActivityID = ActivityID
        CopyOfMe.CostModifier = CostModifier
        CopyOfMe.SolarSystemID = SolarSystemID
        CopyOfMe.SolarSystemName = SolarSystemName
        CopyOfMe.SpecializationCategoryID = SpecializationCategoryID
        CopyOfMe.SpecializationCategory = SpecializationCategory
        CopyOfMe.IsDefault = IsDefault

        CopyOfMe.Bonuses = New List(Of IndustryTeamBonus)
        For i = 0 To Bonuses.Count - 1
            CopyOfMe.Bonuses.Add(Bonuses(i))
        Next

        Return CopyOfMe

    End Function

End Class

' Bonus structure for industry teams
Public Structure IndustryTeamBonus
    Dim BonusID As Integer
    Dim BonusType As String
    Dim BonusValue As Double
    Dim BonusSpecialtyGroupID As Integer
    Dim BonusSpecialtyGroupName As String
End Structure

Public Enum TeamType
    Manufacturing = 1
    ComponentManufacturing = 2
    Copy = 5
    Invention = 8
End Enum

Public Module TeamVariables

    ' For passing no team data
    Public NoTeam As New IndustryTeam

    ' BP Selected Team
    Public SelectedBPManufacturingTeam As New IndustryTeam
    Public SelectedBPComponentManufacturingTeam As New IndustryTeam
    Public SelectedBPCopyTeam As New IndustryTeam
    Public SelectedBPInventionTeam As New IndustryTeam

    ' To save whatever the BP defaults loaded are
    Public DefaultBPManufacturingTeam As IndustryTeam
    Public DefaultBPComponentManufacturingTeam As IndustryTeam
    Public DefaultBPCopyTeam As IndustryTeam
    Public DefaultBPInventionTeam As IndustryTeam

    ' Calc Selected Team
    Public SelectedCalcManufacturingTeam As New IndustryTeam
    Public SelectedCalcComponentManufacturingTeam As New IndustryTeam
    Public SelectedCalcCopyTeam As New IndustryTeam
    Public SelectedCalcInventionTeam As New IndustryTeam

    ' To save whatever the Calc defaults loaded are
    Public DefaultCalcManufacturingTeam As IndustryTeam
    Public DefaultCalcComponentManufacturingTeam As IndustryTeam
    Public DefaultCalcCopyTeam As IndustryTeam
    Public DefaultCalcInventionTeam As IndustryTeam

End Module

