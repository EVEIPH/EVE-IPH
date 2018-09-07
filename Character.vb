
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
    Public Function LoadDummyCharacter(IgnoreMessages As Boolean) As TriState
        Dim response As Integer
        Dim Settings As AppRegistrationInformationSettings

        If Not IgnoreMessages Then
            response = MsgBox("If you do not load a character many features will not be available to you. Do you want to continue without loading a character?", vbYesNo, Application.ProductName)
        End If

        If response = vbYes Or IgnoreMessages Then
            Dim SQL As String
            Dim rsCheck As SQLiteDataReader

            SQL = String.Format("SELECT 'X' FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = {0}", CStr(DummyCharacterID))
            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsCheck = DBCommand.ExecuteReader

            ' Double check to make sure the record doesn't already exist - user could update skills, etc for a dummy and don't want to overwrite
            If Not rsCheck.HasRows Then
                ' Now insert this data in the DB for using all the time and set to default"yyyy-MM-dd HH:mm:ss" since it doesn't exist
                ID = DummyCharacterID
                Name = "Dummy Character"
                DOB = NoDate
                Gender = Male
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

                Dim NoExpireDate As String = Format(NoExpiry, SQLiteDateFormat)

                With CharacterTokenData
                    SQL = "INSERT INTO ESI_CHARACTER_DATA VALUES ({0},'{1}',{2},'{3}','{4}',{5},{6},{7},'{8}','{9}','{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22})"
                    SQL = String.Format(SQL, ID, Name, DummyCorporationID, Format(DOB, SQLiteDateFormat), Gender, RaceID, BloodLineID, AncestryLineID, Descripton, .AccessToken, Format(.TokenExpiration, SQLiteDateFormat), .TokenType, .RefreshToken, .Scopes, 0, NoExpireDate, NoExpireDate, NoExpireDate, NoExpireDate, NoExpireDate, NoExpireDate, NoExpireDate, DefaultCharacterCode) ' Dummy is default
                End With
                Call EVEDB.ExecuteNonQuerySQL(SQL)

                ' Load dummy corp
                CharacterCorporation = New Corporation()
                CharacterCorporation.LoadDummyCorporationData

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
                SQL = "UPDATE ESI_CHARACTER_DATA SET IS_DEFAULT = {0} WHERE CHARACTER_ID = {1}"
                Call EVEDB.ExecuteNonQuerySQL(String.Format(SQL, CStr(DefaultCharacterCode), CStr(DummyCharacterID)))
                Call LoadDefaultCharacter()
            End If

            ' Finally, save the app registration information file as nothing so it doesn't try to load again
            Settings.ClientID = DummyClient
            Settings.SecretKey = ""
            Settings.Scopes = ""

            If AllSettings.SaveAppRegistrationInformationSettings(Settings) Then
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

        ' See if we have a character ID loaded
        SQL = "SELECT CHARACTER_ID FROM ESI_CHARACTER_DATA WHERE IS_DEFAULT <> 0"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCharacter = DBCommand.ExecuteReader

        If rsCharacter.Read() Then
            ' Set the base data, character ID and access token data
            ID = rsCharacter.GetInt64(0)
            CharacterTokenData.CharacterID = ID
            rsCharacter.Close()

            ' Get the latest data for this character, update token through ref
            Return LoadCharacterData(CharacterTokenData, LoadBPs, LoadAssets)

        Else ' No record in DB
            rsCharacter.Close()
            Return False
        End If

    End Function

    ' Load the latest data for the character sent or the default if no character sent from the DB - users may not want to load bps or assets 
    Public Function LoadCharacterData(ByRef TokenData As SavedTokenData, ByVal LoadBPs As Boolean, ByVal LoadAssets As Boolean,
                                      Optional IndustryJobsUpdate As Boolean = False) As Boolean
        Dim ESIData As New ESI
        Dim readerCharacter As SQLiteDataReader
        Dim SQL As String

        SQL = "SELECT CHARACTER_ID, CHARACTER_NAME, "
        SQL = SQL & "CORPORATION_ID, BIRTHDAY, GENDER, RACE_ID, BLOODLINE_ID, ANCESTRY_ID, DESCRIPTION, "
        SQL = SQL & "SCOPES, ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, TOKEN_TYPE, REFRESH_TOKEN, OVERRIDE_SKILLS, IS_DEFAULT "
        SQL = SQL & "FROM ESI_CHARACTER_DATA "
        SQL = SQL & "WHERE CHARACTER_ID = " & CStr(TokenData.CharacterID) & " "

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

                ' Refresh the character data first
                If ID <> DummyCharacterID Then
                    Dim TempESI As New ESI
                    ' Only ignore the cache date if we aren't updating industry jobs
                    Call TempESI.SetCharacterData(CharacterTokenData)
                End If

                CharacterCorporation = New Corporation()
                ' Character corporations have ID's greater than 2 million, so only run if a char corporation not npc
                If .GetInt64(2) > 2000000 Then
                    CharacterCorporation.LoadCorporationData(.GetInt64(2), ID, CharacterTokenData, LoadAssets, LoadBPs)
                End If

                UserApplicationSettings.AllowSkillOverride = CBool(.GetInt32(14))
                IsDefault = CBool(.GetInt32(15))

            End With

            readerCharacter.Close()

            ' Load the character skills
            Call Skills.LoadCharacterSkills(ID, CharacterTokenData)

            ' Load the character's industry jobs
            Jobs = New EVEIndustryJobs()
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterIndustryJobsScope) Then
                IndustryJobsAccess = True
                Call Jobs.UpdateIndustryJobs(ID, CharacterTokenData, ScanType.Personal)
            End If

            If IndustryJobsUpdate Then
                ' Only refresh skills and industry jobs for update calls from industry jobs
                Return True
            End If

            ' Load the standings for this character
            Standings = New EVENPCStandings()
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterStandingsScope) Then
                StandingsAccess = True
                Call Standings.LoadCharacterStandings(ID, CharacterTokenData)
            End If

            ' Load the character's research agents
            DatacoreAgents = New EVEResearchAgents()
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterResearchAgentsScope) Then
                ResearchAgentAccess = True
                Call DatacoreAgents.LoadResearchAgents(ID, CharacterTokenData)
            End If

            ' Load the Blueprints but don't load if they don't have selected
            Blueprints = New EVEBlueprints()
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterBlueprintsScope) Then
                BlueprintsAccess = True
                If LoadBPs Then
                    Call Blueprints.LoadBlueprints(ID, CharacterTokenData, ScanType.Personal, LoadBPs)
                End If
            End If

            ' Load in the assets unless they don't want to update
            Assets = New EVEAssets(ScanType.Personal)
            If CharacterTokenData.Scopes.Contains(ESI.ESICharacterAssetScope) Then
                AssetsAccess = True
                If LoadAssets Then
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

    Public Sub RefreshTokenData()
        ' Refresh the character token data if it's been updated
        Dim SQL As String
        Dim rsToken As SQLiteDataReader

        SQL = "SELECT ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, REFRESH_TOKEN, TOKEN_TYPE, SCOPES FROM ESI_CHARACTER_DATA "
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsToken = DBCommand.ExecuteReader

        If rsToken.Read Then
            CharacterTokenData.CharacterID = ID
            CharacterTokenData.AccessToken = rsToken.GetString(0)
            CharacterTokenData.TokenExpiration = CDate(rsToken.GetString(1))
            CharacterTokenData.TokenType = rsToken.GetString(2)
            CharacterTokenData.RefreshToken = rsToken.GetString(3)
            CharacterTokenData.Scopes = rsToken.GetString(4)
        End If

        ' Reset the corporation data to set the role flags - set reset data flag to false and don't reload jobs, bps, and assets
        CharacterCorporation.LoadCorporationData(CharacterCorporation.CorporationID, ID, CharacterTokenData, False, False, False)

    End Sub

End Class