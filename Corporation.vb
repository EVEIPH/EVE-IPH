
Imports System.Data.SQLite

' Corporation class will handle all processing for corporation data. 

Public Class Corporation

    Private KeyID As Long
    Private APIKey As String
    Private Name As String
    Private ID As Long ' Corp ID
    Private CorpMemberID As Long ' The corp member id we are associating with this corp

    Private KeyData As APIKeyData

    Private Jobs As EVEIndustryJobs
    ' Private Facilities As CorporationFacilities ' Maybe use later
    Private Assets As EVEAssets
    Private Blueprints As EVEBlueprints

    Private AccessMask As Long

    Private AssetsAccess As Boolean
    Private IndustryJobsAccess As Boolean

    Public Sub New(Optional ByVal CorpID As Long = 0, Optional ByVal CorpName As String = "No Corp", Optional ByVal MemberID As Long = 0)

        ID = CorpID
        Name = CorpName
        CorpMemberID = MemberID

        KeyID = 0
        APIKey = ""
        AccessMask = 0
        AssetsAccess = False
        IndustryJobsAccess = False

        KeyData = New APIKeyData

    End Sub

    ' Load the API and key from the DB if a corp key exists. 
    Public Sub LoadCorpAPIData(RefreshAssets As Boolean, RefreshBlueprints As Boolean)
        Dim rsAPI As SQLiteDataReader
        Dim SQL As String
        Dim RefreshDate As Date
        Dim UpdatedNewData As Boolean

        SQL = "SELECT KEY_ID, API_KEY, CACHED_UNTIL, API_TYPE FROM API WHERE API_TYPE = '" & CorporationAPITypeName & "' "
        SQL = SQL & "AND CORPORATION_ID =" & ID & " AND CORPORATION_NAME ='" & FormatDBString(Name) & "'"

        DBCommand = New SQLiteCommand(SQL, DB)
        rsAPI = DBCommand.ExecuteReader

        ' If the key exists, then update it first
        If rsAPI.Read Then
            ' RefreshDate is in UTC time
            RefreshDate = CDate(rsAPI.GetString(2))

            ' See if we want to refresh the corp data
            If RefreshDate < DateTime.UtcNow Then
                UpdatedNewData = UpdateAccountAPIData(rsAPI.GetInt64(0), rsAPI.GetString(1), rsAPI.GetString(3))
            Else
                ' Data wasn't updated, so don't run API updates on other API data either
                UpdatedNewData = False
            End If
        End If

        rsAPI.Close()

        SQL = "SELECT KEY_ID, API_KEY, ACCESS_MASK "
        SQL = SQL & "FROM API WHERE API_TYPE = '" & CorporationAPITypeName & "' "
        SQL = SQL & "AND CORPORATION_ID =" & ID & " AND CORPORATION_NAME ='" & FormatDBString(Name) & "'"

        DBCommand = New SQLiteCommand(SQL, DB)
        rsAPI = DBCommand.ExecuteReader

        If rsAPI.Read Then
            KeyID = rsAPI.GetInt64(0)
            APIKey = rsAPI.GetString(1)
            AccessMask = rsAPI.GetInt64(2)
        End If

        ' Now that we have the API, set the access variables
        Call SetAPIAccess()

        KeyData.ID = CorpMemberID
        KeyData.KeyID = KeyID
        KeyData.APIKey = APIKey

        ' Load industry jobs here 
        KeyData.Access = IndustryJobsAccess
        Jobs = New EVEIndustryJobs(KeyData, ID)
        Call Jobs.LoadIndustryJobs(ScanType.Corporation, UpdatedNewData)

        '' Load corp facilities
        'KeyData.Access = IndustryJobsAccess ' Temp
        'Facilities = New CorporationFacilities(KeyData, ID)
        'Call Facilities.LoadCorpFacilities(UpdatedNewData)

        ' Wait on loading assets until they look at them due to long refresh time
        KeyData.Access = AssetsAccess
        Assets = New EVEAssets(KeyData, ID)
        Call Assets.LoadAssets(ScanType.Corporation, RefreshAssets)

        ' Load the Blueprints but don't update due to long api cache times
        KeyData.Access = AssetsAccess
        Blueprints = New EVEBlueprints(KeyData, ID)
        Call Blueprints.LoadBlueprints(ScanType.Corporation, RefreshBlueprints)

        rsAPI.Close()

    End Sub

    ' Sets the Access variables for the corp key
    Private Sub SetAPIAccess()
        Dim BitString As String
        Dim BitLen As Integer

        ' Access mask is a bitmask 
        BitString = GetBits(AccessMask)

        BitLen = Len(BitString)

        ' Just do a bool cast on the bits for any API access stuff we want
        If BitLen >= AccessMaskBitLocs.AssetList Then
            AssetsAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.AssetList, 1))
        Else
            AssetsAccess = False
        End If

        If BitLen >= AccessMaskBitLocs.IndustryJobs Then
            IndustryJobsAccess = CBool(BitString.Substring(BitLen - AccessMaskBitLocs.IndustryJobs, 1))
        Else
            IndustryJobsAccess = False
        End If

    End Sub

    Public Function GetIndustryJobs() As EVEIndustryJobs
        Return Jobs
    End Function

    Public Function GetAssets() As EVEAssets
        Return Assets
    End Function

    Public Function GetBlueprints() As EVEBlueprints
        Return Blueprints
    End Function

    ReadOnly Property JobsAccess() As Boolean
        Get
            Return IndustryJobsAccess
        End Get
    End Property

    ReadOnly Property AssetAccess() As Boolean
        Get
            Return AssetsAccess
        End Get
    End Property

    ReadOnly Property CorporationID() As Long
        Get
            Return ID
        End Get
    End Property

    ReadOnly Property CorporationName() As String
        Get
            Return Name
        End Get
    End Property

End Class
