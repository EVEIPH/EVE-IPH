

Public Class ESI
    Private Const BaseESIURL As String = "https://login.eveonline.com/"
    Private Const LocalHost As String = "127.0.0.1" ' All calls will redirect to local host. Users can change the port number

    Private ImplementedScopes As List(Of String)

    ''' <summary>
    ''' Initialize the class and set the implemented scopes
    ''' </summary>
    Public Sub New()

        ' Load up all the scopes that we have currently implemented
        ImplementedScopes = New List(Of String)
        ' ESI-assets.read_assets.v1: Allows reading a list of assets that the character owns
        ' ESI-characters.read_agents_research.v1: Allows reading a character's research status with agents
        ' ESI-characters.read_blueprints.v1: Allows reading a character's blueprints
        ' ESI-characters.read_standings.v1: Allows reading a character's standings
        ' ESI-industry.read_character_jobs.v1: Allows reading a character's industry jobs
        ' ESI-skills.read_skills.v1: Allows reading of a character's currently known skills.

        ' Character
        ImplementedScopes.Add("esi-skills.read_skills.v1")
        ImplementedScopes.Add("esi-assets.read_assets.v1")
        ImplementedScopes.Add("esi-characters.read_standings.v1")
        ImplementedScopes.Add("esi-characters.read_agents_research.v1")
        ImplementedScopes.Add("esi-industry.read_character_jobs.v1")
        ImplementedScopes.Add("esi-characters.read_blueprints.v1")

        ' ESI-assets.read_corporation_assets.v1: Allows reading of a character's corporation's assets, if the character has roles to do so.
        ' ESI-corporations.read_blueprints.v1: Allows reading a corporation's blueprints
        ' ESI-industry.read_corporation_jobs.v1: Allows reading of a character's corporation's industry jobs, if the character has roles to do so.

        ' Corporation
        ImplementedScopes.Add("esi-assets.read_corporation_assets.v1")
        ImplementedScopes.Add("esi-corporations.read_blueprints.v1")
        ImplementedScopes.Add("esi-industry.read_corporation_jobs.v1")


    End Sub


End Class

