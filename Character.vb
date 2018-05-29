
Imports System.Data.SQLite

Public Class Character
    Public ID As Long ' PK
    Public Name As String
    Public DOB As Date
    Public Gender As String
    Public RaceID As Integer
    Public BloodLineID As Integer
    Public AncestryLineID As Integer
    Public Descripton As String

    ' For ESI access, etc.
    Public CharacterTokenData As SavedTokenData

    Public OverrideSkills As Boolean
    Public IsDefault As Boolean

    ' Skill Tree - Required Scope
    Public Skills As EVESkillList
    ' Standings
    Public StandingsAccess As Boolean
    Public Standings As EVENPCStandings ' Base Standings
    ' Industry jobs
    Public IndustryJobsAccess As Boolean
    Public Jobs As EVEIndustryJobs
    ' Research agents
    Public ResearchAgentAccess As Boolean
    Public DatacoreAgents As EVEResearchAgents
    ' Assets
    Public AssetsAccess As Boolean
    Public Assets As EVEAssets
    ' Blueprints
    Public BlueprintsAccess As Boolean
    Public Blueprints As EVEBlueprints

    ' All corporation data stored here (assets, jobs, etc)
    Public CharacterCorporation As Corporation

    Public Sub New()
        ID = 0
        Name = ""
        DOB = NoDate
        RaceID = 0
        BloodLineID = 0
        AncestryLineID = 0
        Descripton = ""

        CharacterTokenData = New SavedTokenData

        ' To store the scope access they give us when registering
        StandingsAccess = False
        AssetsAccess = False
        IndustryJobsAccess = False
        ResearchAgentAccess = False
        BlueprintsAccess = False

        Skills = New EVESkillList
        Standings = New EVENPCStandings
        Jobs = New EVEIndustryJobs
        DatacoreAgents = New EVEResearchAgents
        Assets = New EVEAssets

        ' Corporation Data for this character
        CharacterCorporation = New Corporation

    End Sub

    ' Saves the dummy character for the program
    Public Function LoadDummyCharacter() As TriState
        Dim response As Integer
        Dim Settings As AppRegistrationInformationSettings

        response = MsgBox("If you do not load a character many features will not be available to you. Do you want to continue without loading a character?", vbYesNo, Application.ProductName)

        If response = vbYes Then
            Dim SQL As String
            Dim rsCheck As SQLiteDataReader

            DBCommand = New SQLiteCommand("SELECT 'X' FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = 0", EVEDB.DBREf)
            rsCheck = DBCommand.ExecuteReader

            ' Double check to make sure the record doesn't already exist - user could update skills, etc for a dummy and don't want to overwrite
            If Not rsCheck.HasRows Then
                ' Now insert this data in the DB for using all the time and set to default"yyyy-MM-dd HH:mm:ss" since it doesn't exist
                ID = DummyCharacterID
                Name = "Dummy Character"
                DOB = NoDate
                RaceID = 1
                BloodLineID = 8
                AncestryLineID = 9
                Descripton = None

                CharacterTokenData.CharacterID = ID
                CharacterTokenData.AccessToken = "No Token"
                CharacterTokenData.TokenExpiration = NoExpiry
                CharacterTokenData.TokenType = None
                CharacterTokenData.RefreshToken = "No Token"
                CharacterTokenData.Scopes = "No Scopes"

                ' Default corp
                CharacterCorporation = New Corporation()

                With CharacterTokenData
                    SQL = "INSERT INTO ESI_CHARACTER_DATA VALUES ({0},'{1}',{2},'{3}','{4}',{5},{6},{7},'{8}','{9}','{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22})"
                    SQL = String.Format(SQL, 0, None, 0, NoExpiry, "M", 1, 8, 9, None, .AccessToken, .TokenExpiration, .TokenType, .RefreshToken, .Scopes, 0, NoExpiry, NoExpiry, NoExpiry, NoExpiry, NoExpiry, NoExpiry, NoExpiry, 1) ' Dummy is default
                End With
                Call EVEDB.ExecuteNonQuerySQL(SQL)

                ' Load the dummy skills
                Skills.LoadDummySkills()

                ' No standings
                Standings = New EVENPCStandings

                ' No agents
                DatacoreAgents = New EVEResearchAgents

                ' No Assets
                Assets = New EVEAssets

                ' No Jobs
                Jobs = New EVEIndustryJobs

            Else ' There is a dummy already in the DB, so just set it to default and load like a normal char
                SQL = "UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = {0} WHERE CHARACTER_ID = 0"
                Call EVEDB.ExecuteNonQuerySQL(String.Format(SQL, DefaultDummyCharacterCode))

                Call LoadDefaultCharacter()

            End If

            ' Finally, save the app registration information file as nothing so it doesn't try to load again
            Settings.ClientID = DummyClient
            Settings.SecretKey = ""
            Settings.Port = 0
            Settings.Scopes = ""

            If AllSettings.SaveAppRegistrationInformationSettings(Settings) Then
                DummyAccountLoaded = True
                Return TriState.True ' both options load a dummy character
            Else
                Return TriState.False
            End If
        Else
            ' Go back and do nothing
            Return TriState.UseDefault
        End If

    End Function

    ' Sets the default character for the program if no character ID sent, else it returns the character ID. If we can't find it in DB, then return false
    Public Function LoadDefaultCharacter(Optional LoadBPs As Boolean = False, Optional LoadAssets As Boolean = False) As Boolean
        Dim rsCharacter As SQLiteDataReader
        Dim SQL As String
        Dim ESIConnection As New ESI

        ' Make sure the application is registered. If not, exit and run the process to save registration info
        If Not ESIConnection.AppRegistered Then
            Return False
        End If

        ' See if we have a character ID loaded
        SQL = "SELECT CHARACTER_ID, ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, TOKEN_TYPE, REFRESH_TOKEN, SCOPES "
        SQL &= "FROM ESI_CHARACTER_DATA WHERE IS_DEFAULT <> 0"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCharacter = DBCommand.ExecuteReader

        If rsCharacter.Read() Then

            ' Check for dummy account first and set the global flag
            Dim numChars As Long

            SQL = "SELECT COUNT(*) FROM ESI_CHARACTER_DATA WHERE IS_DEFAULT = {0}"

            DBCommand = New SQLiteCommand(String.Format(SQL, DefaultDummyCharacterCode), EVEDB.DBREf)
            numChars = CLng(DBCommand.ExecuteScalar())

            If numChars = 0 Then
                DummyAccountLoaded = False
            Else
                DummyAccountLoaded = True
            End If

            ' Set the base data, character ID and access token data
            ID = rsCharacter.GetInt64(0)
            CharacterTokenData.CharacterID = ID
            CharacterTokenData.AccessToken = rsCharacter.GetString(1)
            CharacterTokenData.TokenExpiration = CDate(rsCharacter.GetString(2))
            CharacterTokenData.TokenType = rsCharacter.GetString(3)
            CharacterTokenData.RefreshToken = rsCharacter.GetString(4)
            CharacterTokenData.Scopes = rsCharacter.GetString(5)
            rsCharacter.Close()

            ' Get the latest data for this character
            Return LoadCharacterData(ID, CharacterTokenData, LoadBPs, LoadAssets)

        Else ' No record in DB
            rsCharacter.Close()
            Return False
        End If

    End Function

    ' Load the latest data for the character sent or the default if no character sent from the DB - users may not want to load bps or assets 
    Public Function LoadCharacterData(ByVal CharacterID As Long, ByRef TokenData As SavedTokenData, ByVal LoadBPs As Boolean, ByVal LoadAssets As Boolean) As Boolean
        Dim ESIData As New ESI
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String

        ' Update the character data first
        If Not ESIData.SetCharacterData(CharacterID, TokenData) And Not DummyAccountLoaded Then
            ' They probably don't have the authorization set (deleted character from thirdparty access page)
            ' https://community.eveonline.com/support/third-party-applications/
            ' For now, delete the record from ESI and have them reload
            EVEDB.ExecuteNonQuerySQL("DELETE FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = " & CStr(CharacterID) & " ")
            Return False
        End If

        SQL = "SELECT CHARACTER_ID, CHARACTER_NAME, "
        SQL = SQL & "CORPORATION_ID, BIRTHDAY, GENDER, RACE_ID, BLOODLINE_ID, ANCESTRY_ID, DESCRIPTION, "
        SQL = SQL & "SCOPES, ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, TOKEN_TYPE, REFRESH_TOKEN, OVERRIDE_SKILLS, IS_DEFAULT "
        SQL = SQL & "FROM ESI_CHARACTER_DATA "
        SQL = SQL & "WHERE CHARACTER_ID = " & CStr(CharacterID) & " "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerCharacter = DBCommand.ExecuteReader

        If readerCharacter.Read Then
            ' Query the character data and store
            With readerCharacter
                ID = .GetInt64(0)
                Name = .GetString(1)
                DOB = CDate(.GetString(3))
                Gender = .GetString(4)
                RaceID = .GetInt32(5)
                BloodLineID = .GetInt32(6)
                AncestryLineID = .GetInt32(7)
                Descripton = .GetString(8)

                ' For ESI access, etc.
                CharacterTokenData.CharacterID = ID
                CharacterTokenData.AccessToken = .GetString(10)
                CharacterTokenData.TokenExpiration = CDate(.GetString(11))
                CharacterTokenData.TokenType = .GetString(12)
                CharacterTokenData.RefreshToken = .GetString(13)
                CharacterTokenData.Scopes = .GetString(9)

                UserApplicationSettings.AllowSkillOverride = CBool(.GetInt32(14))
                IsDefault = CBool(.GetInt32(15))

                CharacterCorporation = New Corporation()
                CharacterCorporation.LoadCorporationData(.GetInt64(2), ID, CharacterTokenData, LoadAssets, LoadBPs)

            End With

            readerCharacter.Close()

            ' Load the character skills
            Call Skills.LoadCharacterSkills(ID, CharacterTokenData)

            ' Load the standings for this character
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterStandingsScope) Then
                StandingsAccess = True
                Standings = New EVENPCStandings()
                Call Standings.LoadCharacterStandings(ID, CharacterTokenData)
            End If

            ' Load the character's research agents
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterResearchAgentsScope) Then
                ResearchAgentAccess = True
                DatacoreAgents = New EVEResearchAgents()
                Call DatacoreAgents.LoadResearchAgents(ID, CharacterTokenData)
            End If

            ' Load the character's industry jobs
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterIndustryJobsScope) Then
                IndustryJobsAccess = True
                Jobs = New EVEIndustryJobs()
                Call Jobs.UpdateIndustryJobs(ID, CharacterTokenData, ScanType.Personal)
            End If

            ' Load the Blueprints but don't load if they don't have selected
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterBlueprintsScope) Then
                BlueprintsAccess = True
                If LoadBPs Then
                    Blueprints = New EVEBlueprints()
                    Call Blueprints.LoadBlueprints(ID, CharacterTokenData, ScanType.Personal, LoadBPs)
                End If
            End If

            ' Load in the assets unless they don't want to update
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterAssetScope) Then
                AssetsAccess = True
                If LoadAssets Then
                    Assets = New EVEAssets(ScanType.Personal)
                    Call Assets.LoadAssets(ID, CharacterTokenData, LoadAssets)
                End If
            End If

            readerCharacter.Close()

            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetIndustryJobs() As EVEIndustryJobs
        Return Jobs
    End Function

    Public Function GetResearchAgents() As EVEResearchAgents
        Return DatacoreAgents
    End Function

    Public Function GetAssets() As EVEAssets
        Return Assets
    End Function

    Public Function GetBlueprints() As EVEBlueprints
        Return Blueprints
    End Function

End Class