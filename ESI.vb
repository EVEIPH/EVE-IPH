Imports System.Net
Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Data.SQLite
Imports System.Collections.Specialized
Imports System.Threading
Imports Newtonsoft.Json

Public Module ESIGlobals
    Public ESICharacterSkillsScope As String = "esi-skills.read_skills" ' only required scope to use IPH
End Module

Public Class ESI
    Private Const ESIAuthorizeURL As String = "https://login.eveonline.com/oauth/authorize"
    Private Const ESITokenURL As String = "https://login.eveonline.com/oauth/token"
    Private Const ESIVerifyURL As String = "https://login.eveonline.com/oauth/verify"
    Private Const ESIPublicURL As String = "https://esi.tech.ccp.is/latest/"
    Private Const TranquilityDataSource As String = "?datasource=tranquility"

    Private Const LocalHost As String = "127.0.0.1" ' All calls will redirect to local host.
    Private Const LocalPort As String = "12500" ' Always use this port

    Private ClientID As String
    Private SecretKey As String
    Public ScopesString As String

    Public Const ESICharacterAssetScope As String = "esi-assets.read_assets"
    Public Const ESICharacterResearchAgentsScope As String = "esi-characters.read_agents_research"
    Public Const ESICharacterBlueprintsScope As String = "esi-characters.read_blueprints"
    Public Const ESICharacterStandingsScope As String = "esi-characters.read_standings"
    Public Const ESICharacterIndustryJobsScope As String = "esi-industry.read_character_jobs"
    Public Const ESICharacterSkillsScope As String = "esi-skills.read_skill"

    Public Const ESICorporationAssetScope As String = "esi-assets.read_corporation_assets"
    Public Const ESICorporationBlueprintsScope As String = "esi-corporations.read_blueprints"
    Public Const ESICorporationIndustryJobsScope As String = "esi-industry.read_corporation_jobs"
    Public Const ESICorporationMembership As String = "esi-corporations.read_corporation_membership"

    ' Cache field names and times
    Private Const IndustrySystemsField As String = "INDUSTRY_SYSTEMS_CACHED_UNTIL"
    Private Const IndustrySystemsLength As Integer = 1
    Private Const IndustryFacilitiesField As String = "INDUSTRY_FACILITIES_CACHED_UNTIL"
    Private Const IndustryFacilitiesLength As Integer = 1
    Private Const MarketPricesField As String = "MARKET_PRICES_CACHED_UNTIL"
    Private Const MarketPricesLength As Integer = 23

    ' Rate limits
    'For your requests, this means you can send an occasional burst of 400 requests all at once. 
    'If you do, you'll hit the rate limit once you try to send your 401st request unless you wait.

    'Your bucket refills at a rate of 1 per 1/150th of a second. If you send 400 requests at once, 
    'you need to wait 2.67 seconds before you can send another 400 requests (1/150 * 400), if you 
    'only wait 1.33 seconds you can send another 200, and so on. Altrnatively, you can send a constant 150 requests every 1 second. 
    Private Const ESIRatePerSecond As Integer = 150 ' max requests per second
    Private Const ESIBurstSize As Integer = 400 ' max burst of requests, which need 2.46 seconds to refill before re-bursting
    Private Const ESIMaximumConnections As Integer = 20

    Private IDToFind As Long ' for predicate
    Private RetriedCall As Boolean

    Private AuthThreadReference As Thread ' Reference in case we need to kill this
    Private AuthStreamText As String ' The return data from the web call
    Private myListner As TcpListener = New TcpListener(IPAddress.Parse(LocalHost), CInt(LocalPort)) ' Ref to the listener so we can stop it

    ' ESI implements the following scopes:

    ' Character
    ' esi-assets.read_assets.v1: Allows reading a list of assets that the character owns
    ' esi-characters.read_agents_research.v1: Allows reading a character's research status with agents
    ' esi-characters.read_blueprints.v1: Allows reading a character's blueprints
    ' esi-characters.read_standings.v1: Allows reading a character's standings
    ' esi-industry.read_character_jobs.v1: Allows reading a character's industry jobs
    ' esi-skills.read_skills.v1: Allows reading of a character's currently known skills.

    ' Corporation
    ' esi-assets.read_corporation_assets.v1: Allows reading of a character's corporation's assets, if the character has roles to do so.
    ' esi-corporations.read_blueprints.v1: Allows reading a corporation's blueprints
    ' esi-industry.read_corporation_jobs.v1: Allows reading of a character's corporation's industry jobs, if the character has roles to do so.

    ''' <summary>
    ''' Initialize the class and set the implemented scopes
    ''' </summary>
    Public Sub New()

        ' Load all the details from the authorization information file
        Dim ApplicationSettings As AppRegistrationInformationSettings = AllSettings.LoadAppRegistrationInformationSettings
        With ApplicationSettings
            ClientID = .ClientID
            SecretKey = .SecretKey
            ' The scopes as submitted to the web service must be space-delimited, but the file can store them in multiple formats, including CrLF or comma separated - Ben Abraham Fix to issue #110
            ScopesString = String.Join(" ", .Scopes.Split(New String() {" ", ",", vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries))
        End With

        AuthStreamText = ""

        RetriedCall = False

    End Sub

    Public Function GetClientID() As String
        Return ClientID
    End Function

    ''' <summary>
    ''' Opens a connection to EVE SSO server and gets an authorization token to get an access token
    ''' </summary>
    ''' <returns>Authorization Token</returns>
    Private Function GetAuthorizationToken(ByRef ErrorCode As Integer) As String
        Dim ErrorResponse As String = ""

        Try
            If ClientID <> "" And ClientID <> DummyClient Then
                Dim StartTime As Date = Now
                AuthThreadReference = New Thread(AddressOf GetAuthorizationfromWeb)
                AuthThreadReference.Start()

                ' Now loop until thread is done, 60 seconds goes by, or cancel clicked
                Do
                    If DateDiff(DateInterval.Second, StartTime, Now) > 60 Then
                        Call MsgBox("Request timed out. You must complete your login within 60 seconds.", vbInformation, Application.ProductName)
                        myListner.Stop()
                        AuthThreadReference.Abort()
                        Return Nothing
                    ElseIf CancelESISSOLogin Then
                        Call MsgBox("Request Canceled", vbInformation, Application.ProductName)
                        myListner.Stop()
                        AuthThreadReference.Abort()
                        Return Nothing
                    ElseIf Not AuthThreadReference.IsAlive Then
                        Exit Do
                    End If
                    Application.DoEvents()
                Loop

                ' Process the auth stream now
                Dim AuthTokenString As String() = AuthStreamText.Split(New Char() {" "c})
                Dim AuthToken As String = ""

                For i = 0 To AuthTokenString.Count - 1
                    If AuthTokenString(i).Contains("/?code=") Then
                        ' Strip the header and save the string
                        Dim Start As Integer = InStr(AuthTokenString(i), "=")
                        AuthToken = AuthTokenString(i).Substring(Start)
                    End If
                Next

                Return AuthToken
            End If
        Catch ex As WebException
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            ErrorResponse = GetErrorResponseBody(ex)

            If ErrorCode >= 500 And Not RetriedCall Then
                ' Try this call again after waiting a few
                Threading.Thread.Sleep(2000)
                RetriedCall = True
                Call GetAuthorizationToken(0)
            End If
            MsgBox("Web Request failed to get Authorization Token. Code: " & ErrorCode & ", " & ex.Message & " - " & ErrorResponse)
        Catch ex As Exception
            MsgBox("The request failed to get Authorization Token. " & ex.Message, vbInformation, Application.ProductName)
            ErrorCode = -1
        End Try

        Return ""
        RetriedCall = False

    End Function

    ''' <summary>
    ''' Opens the login for EVE SSO and returns the data stream from a successful log in
    ''' </summary>
    Private Sub GetAuthorizationfromWeb()
        Try
            ' Build the authorization call
            Dim URL As String = ESIAuthorizeURL & "?response_type=code" & "&redirect_uri=http://"
            URL &= LocalHost & ":" & LocalPort & "&client_id=" & ClientID & "&scope=" & ScopesString

            Process.Start(URL)

            Dim mySocket As Socket = Nothing
            Dim myStream As NetworkStream = Nothing
            Dim myReader As StreamReader = Nothing
            Dim myWriter As StreamWriter = Nothing

            myListner.Start()

            mySocket = myListner.AcceptSocket() ' Wait for response
            Debug.Print("After socket listen")
            myStream = New NetworkStream(mySocket)
            myReader = New StreamReader(myStream)
            myWriter = New StreamWriter(myStream)
            myWriter.AutoFlush = True

            Do
                AuthStreamText &= myReader.ReadLine & "|"

                If AuthStreamText.Contains("code") Then
                    Exit Do
                End If
            Loop Until myReader.EndOfStream

            myWriter.Write("Login Successful!" & vbCrLf & vbCrLf & "You can close this window.")

            myWriter.Close()
            myReader.Close()
            myStream.Close()
            mySocket.Close()
            myListner.Stop()

            Application.DoEvents()
        Catch ex As Exception
            Application.DoEvents()
        End Try

    End Sub

    ''' <summary>
    ''' Gets the Access Token data from the EVE server for authorization or refresh tokens.
    ''' </summary>
    ''' <param name="Token">An Authorization or Refresh Token</param>
    ''' <param name="Refresh">If the token is Authorization or Refresh</param>
    ''' <returns>Access Token Data object</returns>
    Private Function GetAccessToken(Token As String, Refresh As Boolean, ByRef ErrorCode As Integer) As ESITokenData

        If Token = "No Token" Or Token = "" Then
            Return Nothing
        End If

        Dim AccessTokenOutput As New ESITokenData
        Dim Success As Boolean = False
        Dim WC As New WebClient
        Dim Response As Byte()
        Dim Data As String = ""
        Dim ErrorResponse As String = ""

        Dim AuthHeader As String = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientID}:{SecretKey}"), Base64FormattingOptions.None)}"

        WC.Headers(HttpRequestHeader.Authorization) = AuthHeader
        WC.Proxy = GetProxyData()

        Dim PostParameters As New NameValueCollection
        If Not Refresh Then
            PostParameters.Add("grant_type", "authorization_code")
            PostParameters.Add("code", Token)
        Else
            PostParameters.Add("grant_type", "refresh_token")
            PostParameters.Add("refresh_token", Token)
        End If

        Try
            Response = WC.UploadValues(ESITokenURL, "POST", PostParameters)

            ' Convert byte data to string
            Data = Encoding.UTF8.GetString(Response)

            ' Parse the data to the class
            AccessTokenOutput = JsonConvert.DeserializeObject(Of ESITokenData)(Data)
            Success = True

        Catch ex As WebException
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            ErrorResponse = GetErrorResponseBody(ex)

            If ErrorCode >= 500 And Not RetriedCall Then
                ' Try this call again after waiting a few
                RetriedCall = True
                Threading.Thread.Sleep(2000)
                Call GetAccessToken(Token, Refresh, 0)
            End If

            MsgBox("Web Request failed to get Access Token. Code: " & ErrorCode & ", " & ex.Message & " - " & ErrorResponse)
        Catch ex As Exception
            MsgBox("The request failed to get Access Token. " & ex.Message, vbInformation, Application.ProductName)
            ErrorCode = -1
        End Try

        RetriedCall = False

        If Success Then
            Return AccessTokenOutput
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Queries the server for public data for the URL sent. If not found, returns nothing
    ''' </summary>
    ''' <param name="URL">Full public data URL as a string</param>
    ''' <returns>Byte Array of response or nothing if call fails</returns>
    Private Function GetPublicData(ByVal URL As String, ByRef CacheDate As Date, Optional BodyData As String = "") As String
        Dim Response As String = ""
        Dim WC As New WebClient
        Dim ErrorCode As Integer = 0
        Dim ErrorResponse As String = ""

        WC.Proxy = GetProxyData()

        Try

            If BodyData <> "" Then
                Response = Encoding.UTF8.GetString(WC.UploadData(URL, Encoding.UTF8.GetBytes(BodyData)))
            Else
                Response = WC.DownloadString(URL)
            End If

            ' Get the expiration date for the cache date
            Dim myWebHeaderCollection As WebHeaderCollection = WC.ResponseHeaders
            Dim Expires As String = myWebHeaderCollection.Item("Expires")
            Dim Pages As Integer = CInt(myWebHeaderCollection.Item("X-Pages"))

            If Not IsNothing(Expires) Then
                CacheDate = CDate(Expires.Replace("GMT", "").Substring(InStr(Expires, ",") + 1)) ' Expiration date is in GMT
            Else
                CacheDate = NoExpiry
            End If

            Return Response
        Catch ex As WebException
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            ErrorResponse = GetErrorResponseBody(ex)

            If ErrorCode >= 500 And Not RetriedCall Then
                RetriedCall = True
                ' Try this call again after waiting a few
                Threading.Thread.Sleep(2000)
                Return GetPublicData(URL, CacheDate, BodyData)
            End If
            MsgBox("Web Request failed to get Public data. Code: " & ErrorCode & ", " & ex.Message & " - " & ErrorResponse)
        Catch ex As Exception
            MsgBox("The request failed to get Public data. " & ex.Message, vbInformation, Application.ProductName)
        End Try

        RetriedCall = False

        If Response <> "" Then
            Return Response
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Queries the server for private, authorized data for data sent. Function will check the 
    ''' authorization token and update the sent variable and DB data if expired.
    ''' </summary>
    ''' <returns>Returns data response as a string</returns>
    Private Function GetPrivateAuthorizedData(ByVal URL As String, ByRef TokenData As ESITokenData,
                                              ByVal TokenExpiration As Date, ByRef CacheDate As Date,
                                              ByVal CharacterID As Long) As String
        Dim WC As New WebClient
        Dim ErrorCode As Integer = 0
        Dim ErrorResponse As String = ""
        Dim Response As String = ""

        WC.Proxy = GetProxyData()

        ' See if we update the token data first
        If TokenExpiration <= DateTime.UtcNow Then
            ' Update the token
            TokenData = GetAccessToken(TokenData.refresh_token, True, ErrorCode)
            ' Update the token data in the DB for this character
            Dim SQL As String = ""
            ' Update data - only stuff that could (reasonably) change
            SQL = "UPDATE ESI_CHARACTER_DATA SET ACCESS_TOKEN = '{0}', ACCESS_TOKEN_EXPIRE_DATE_TIME = '{1}', "
            SQL &= "TOKEN_TYPE = '{2}', REFRESH_TOKEN = '{3}' WHERE CHARACTER_ID = {4}"

            With TokenData
                SQL = String.Format(SQL, FormatDBString(.access_token),
                            Format(DateAdd(DateInterval.Second, TokenData.expires_in, DateTime.UtcNow), SQLiteDateFormat),
                            FormatDBString(.token_type), FormatDBString(.refresh_token), CharacterID)
            End With
        End If

        If ErrorCode = 0 Then
            Try
                Dim Auth_header As String = $"Bearer {TokenData.access_token}"

                WC.Headers(HttpRequestHeader.Authorization) = Auth_header
                Response = WC.DownloadString(URL)

                ' Get the expiration date for the cache date
                Dim myWebHeaderCollection As WebHeaderCollection = WC.ResponseHeaders
                Dim Expires As String = myWebHeaderCollection.Item("Expires")
                Dim Pages As Integer = CInt(myWebHeaderCollection.Item("X-Pages"))

                If Not IsNothing(Expires) Then
                    CacheDate = CDate(Expires.Replace("GMT", "").Substring(InStr(Expires, ",") + 1)) ' Expiration date is in GMT
                Else
                    CacheDate = NoExpiry
                End If

                If Not IsNothing(Pages) Then
                    If Pages > 1 Then
                        Dim TempResponse As String = ""
                        For i = 2 To Pages
                            TempResponse = WC.DownloadString(URL & "&page=" & CStr(i))
                            ' Combine with the original response - strip the end and leading brackets
                            Response = Response.Substring(0, Len(Response) - 1) & "," & TempResponse.Substring(1)
                        Next
                    End If
                End If

                Return Response
            Catch ex As WebException
                ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
                ErrorResponse = GetErrorResponseBody(ex)

                If ErrorResponse = "Character not in corporation" Then
                    ' Assume this error came from checking on NPC corp roles and just exit with nothing
                    Exit Try
                End If

                If ErrorCode >= 500 And Not RetriedCall Then
                    RetriedCall = True
                    ' Try this call again after waiting a few
                    Threading.Thread.Sleep(2000)
                    Return GetPrivateAuthorizedData(URL, TokenData, TokenExpiration, CacheDate, CharacterID)
                End If
                MsgBox("Web Request failed to get Authorized data. Code: " & ErrorCode & ", " & ex.Message & " - " & ErrorResponse)
            Catch ex As Exception
                MsgBox("The request failed to get Authorized data. " & ex.Message, vbInformation, Application.ProductName)
            End Try
        End If

        RetriedCall = False

        If Response <> "" Then
            Return Response
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Formats a SavedTokenData object to ESITokenData
    ''' </summary>
    ''' <param name="TokenData">SavedTokenData object</param>
    ''' <returns>the ESITokenData object</returns>
    Private Function FormatTokenData(ByVal TokenData As SavedTokenData) As ESITokenData
        Dim TempTokenData As New ESITokenData

        TempTokenData.access_token = TokenData.AccessToken
        TempTokenData.refresh_token = TokenData.RefreshToken
        TempTokenData.token_type = TokenData.TokenType

        Return TempTokenData

    End Function

    ''' <summary>
    ''' Formats string dates returned from the ESI server into a date object
    ''' </summary>
    ''' <param name="OriginalDate">ESI date value as a string</param>
    ''' <returns>Date object of the sent date as a string</returns>
    Public Function FormatESIDate(ByVal OriginalDate As String) As Date
        If Not IsNothing(OriginalDate) Then
            Return CDate(OriginalDate.Replace("T", " ").Replace("Z", ""))
        Else
            Return NoDate
        End If
    End Function

#Region "Load Character Data"

    ''' <summary>
    ''' Gets verification and public data about the character returned from logging into the EVE SSO and authorizing 
    ''' access for a new character first logging in. If the character has already been loaded, then update the data.
    ''' </summary>
    ''' <returns>Returns boolean if the function was successful in setting character data.</returns>    
    Public Function SetCharacterData(Optional ByRef CharacterTokenData As SavedTokenData = Nothing, Optional IgnoreCacheDate As Boolean = False) As Boolean
        Dim TokenData As ESITokenData
        Dim CharacterData As New ESICharacterData
        Dim CharacterID As Long
        Dim ErrorCode As Integer = 0

        If Not IsNothing(CharacterTokenData) Then
            CharacterID = CharacterTokenData.CharacterID
        Else
            CharacterID = 0
        End If

        Try
            If CharacterID = 0 Then
                ' We need to get the token data from the authorization
                TokenData = GetAccessToken(GetAuthorizationToken(ErrorCode), False, ErrorCode)
                CharacterTokenData = New SavedTokenData
            Else
                ' We need to refresh the token data
                TokenData = GetAccessToken(CharacterTokenData.RefreshToken, True, ErrorCode)
                ' Update the local copy with the new information
                CharacterTokenData.AccessToken = TokenData.access_token
                CharacterTokenData.RefreshToken = TokenData.refresh_token
                CharacterTokenData.TokenType = TokenData.token_type
            End If

            If ErrorCode = 0 And Not IsNothing(TokenData) Then
                Dim CB As New CacheBox
                Dim CacheDate As Date

                ' Set the expiration date to pass
                CharacterTokenData.TokenExpiration = DateAdd(DateInterval.Second, TokenData.expires_in, DateTime.UtcNow)

                If CB.DataUpdateable(CacheDateType.PublicCharacterData, CharacterID) Or IgnoreCacheDate Then
                    ' Now with the token data, look up the character data
                    CharacterData.VerificationData = GetCharacterVerificationData(TokenData, CharacterTokenData.TokenExpiration)
                    CharacterData.PublicData = GetCharacterPublicData(CharacterData.VerificationData.CharacterID, CacheDate)

                    ' Save it in the table if not there, or update it if they selected the character again
                    Dim rsCheck As SQLiteDataReader
                    Dim SQL As String

                    SQL = "SELECT * FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID = " & CStr(CharacterData.VerificationData.CharacterID)

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsCheck = DBCommand.ExecuteReader

                    If rsCheck.HasRows Then
                        ' Update data - only stuff that could (reasonably) change
                        SQL = "UPDATE ESI_CHARACTER_DATA SET CORPORATION_ID = {0}, DESCRIPTION = '{1}', SCOPES = '{2}', ACCESS_TOKEN = '{3}',"
                        SQL &= "ACCESS_TOKEN_EXPIRE_DATE_TIME = '{4}', TOKEN_TYPE = '{5}', REFRESH_TOKEN = '{6}' "
                        SQL &= "WHERE CHARACTER_ID = {7}"

                        With CharacterData
                            SQL = String.Format(SQL, .PublicData.corporation_id,
                                        FormatDBString(FormatNullString(.PublicData.description)),
                                        FormatDBString(.VerificationData.Scopes),
                                        FormatDBString(TokenData.access_token),
                                        Format(CharacterTokenData.TokenExpiration, SQLiteDateFormat),
                                        FormatDBString(TokenData.token_type),
                                        FormatDBString(TokenData.refresh_token),
                                        .VerificationData.CharacterID)

                        End With
                    Else
                        ' Insert new data
                        SQL = "INSERT INTO ESI_CHARACTER_DATA (CHARACTER_ID, CHARACTER_NAME, CORPORATION_ID, BIRTHDAY, GENDER, RACE_ID, "
                        SQL &= "BLOODLINE_ID, ANCESTRY_ID, DESCRIPTION, ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, TOKEN_TYPE, REFRESH_TOKEN, "
                        SQL &= "SCOPES, OVERRIDE_SKILLS, IS_DEFAULT)"
                        SQL &= "VALUES ({0},'{1}',{2},'{3}','{4}',{5},{6},{7},'{8}','{9}','{10}','{11}','{12}','{13}',{14},{15})"
                        With CharacterData
                            SQL = String.Format(SQL, .VerificationData.CharacterID,
                                        FormatDBString(.VerificationData.CharacterName),
                                        .PublicData.corporation_id,
                                        Format(CDate(.PublicData.birthday.Replace("T", " ")), SQLiteDateFormat),
                                        FormatDBString(.PublicData.gender),
                                        .PublicData.race_id,
                                        .PublicData.bloodline_id,
                                        FormatNullInteger(.PublicData.ancestry_id),
                                        FormatDBString(FormatNullString(.PublicData.description)),
                                        FormatDBString(TokenData.access_token),
                                        Format(CharacterTokenData.TokenExpiration, SQLiteDateFormat),
                                        FormatDBString(TokenData.token_type),
                                        FormatDBString(TokenData.refresh_token),
                                        FormatDBString(.VerificationData.Scopes),
                                        0, 0) ' Don't set default yet or override skills
                        End With
                    End If

                    EVEDB.ExecuteNonQuerySQL(SQL)

                    ' Update public cachedate for character now that we have a record
                    Call CB.UpdateCacheDate(CacheDateType.PublicCharacterData, CacheDate, CLng(CharacterData.VerificationData.CharacterID))

                    ' While we are here, load the public information of the corporation too
                    Call SetCorporationData(CharacterData.PublicData.corporation_id, CacheDate)

                    ' Update after we update/insert the record
                    Call CB.UpdateCacheDate(CacheDateType.PublicCorporationData, CacheDate, CharacterData.PublicData.corporation_id)

                    If CharacterID = 0 Then
                        MsgBox("Character successfully added to IPH", vbInformation, Application.ProductName)
                    End If

                    Return True
                Else
                    If ErrorCode = 0 Then
                        ' Just didn't need to update yet
                        Return True
                    End If
                    If ErrorCode <> 400 Then
                        MsgBox("Unable to load the selected character to IPH", vbExclamation, Application.ProductName)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Unable to get authorization and verification data through ESI: " & ex.Message, vbInformation, Application.ProductName)
        End Try

        Return False

    End Function

    ''' <summary>
    ''' Retrieves the public data about the character ID sent
    ''' </summary>
    ''' <param name="CharacterID">CharacterID you want public data for</param>
    ''' <returns>Returns data in the ESICharacterPublicData JSON property class</returns>
    Public Function GetCharacterPublicData(ByVal CharacterID As String, ByRef DataCacheDate As Date) As ESICharacterPublicData
        Dim CharacterData As ESICharacterPublicData
        Dim PublicData As String

        PublicData = GetPublicData(ESIPublicURL & "characters/" & CStr(CharacterID) & "/" & TranquilityDataSource, DataCacheDate)

        CharacterData = JsonConvert.DeserializeObject(Of ESICharacterPublicData)(PublicData)

        Return CharacterData

    End Function

    ''' <summary>
    ''' Gets the character verification data when sent the refresh token
    ''' </summary>
    ''' <param name="TokenData"></param>
    ''' <param name="ExpirationDate"></param>
    ''' <returns>Character Verification Data object</returns>
    Public Function GetCharacterVerificationData(ByVal TokenData As ESITokenData, ByVal ExpirationDate As Date) As ESICharacterVerificationData
        Dim CacheDate As Date
        Dim WC As New WebClient
        Dim ErrorCode As Integer = 0
        Dim ErrorResponse As String = ""
        Dim Response As String = ""

        WC.Proxy = GetProxyData()

        ' See if we update the token data first
        If ExpirationDate <= DateTime.UtcNow Then
            ' Update the token
            TokenData = GetAccessToken(TokenData.refresh_token, True, ErrorCode)
        End If

        If ErrorCode = 0 Then
            Try
                Dim Auth_header As String = $"Bearer {TokenData.access_token}"

                WC.Headers(HttpRequestHeader.Authorization) = Auth_header
                Response = WC.DownloadString(ESIVerifyURL)

                ' Get the expiration date for the cache date
                Dim myWebHeaderCollection As WebHeaderCollection = WC.ResponseHeaders
                Dim Expires As String = myWebHeaderCollection.Item("Expires")
                Dim Pages As Integer = CInt(myWebHeaderCollection.Item("X-Pages"))

                If Not IsNothing(Expires) Then
                    CacheDate = CDate(Expires.Replace("GMT", "").Substring(InStr(Expires, ",") + 1)) ' Expiration date is in GMT
                Else
                    CacheDate = NoExpiry
                End If

                If Not IsNothing(Pages) Then
                    If Pages > 1 Then
                        Dim TempResponse As String = ""
                        For i = 2 To Pages
                            TempResponse = WC.DownloadString(ESIVerifyURL & "&page=" & CStr(i))
                            ' Combine with the original response - strip the end and leading brackets
                            Response = Response.Substring(0, Len(Response) - 1) & "," & TempResponse.Substring(1)
                        Next
                    End If
                End If

                Return JsonConvert.DeserializeObject(Of ESICharacterVerificationData)(Response)

            Catch ex As WebException
                ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
                ErrorResponse = GetErrorResponseBody(ex)

                If ErrorCode >= 500 And Not RetriedCall Then
                    RetriedCall = True
                    ' Try this call again after waiting a few
                    Thread.Sleep(2000)
                    Return GetCharacterVerificationData(TokenData, ExpirationDate)
                End If
                MsgBox("Web Request failed to get Authorized data. Code: " & ErrorCode & ", " & ex.Message & " - " & ErrorResponse)
            Catch ex As Exception
                MsgBox("The request failed to get Authorized data. " & ex.Message, vbInformation, Application.ProductName)
            End Try
        End If

        RetriedCall = False

        Return Nothing

    End Function

#End Region

#Region "Scopes Processing"

    Public Function GetCharacterSkills(ByVal CharacterID As Long, ByVal TokenData As SavedTokenData, ByRef SkillsCacheDate As Date) As EVESkillList
        Dim SkillData As New ESICharacterSkillsBase
        Dim ReturnData As String
        Dim ReturnSkills As New EVESkillList
        Dim TempSkill As New EVESkill

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "characters/" & CStr(CharacterID) & "/skills/" & TranquilityDataSource, TempTokenData, TokenData.TokenExpiration, SkillsCacheDate, CharacterID)

        If Not IsNothing(ReturnData) Then
            SkillData = JsonConvert.DeserializeObject(Of ESICharacterSkillsBase)(ReturnData)

            For Each entry In SkillData.skills
                TempSkill = New EVESkill
                TempSkill.TypeID = entry.skill_id
                TempSkill.Level = entry.trained_skill_level
                TempSkill.SkillPoints = entry.skillpoints_in_skill

                Call ReturnSkills.InsertSkill(TempSkill, True)
            Next

            Return ReturnSkills
        Else
            Return Nothing
        End If

    End Function

    Public Function GetCharacterStandings(ByVal CharacterID As Long, ByVal TokenData As SavedTokenData, ByRef StandingsCacheDate As Date) As EVENPCStandings
        Dim TempStandingsList As New EVENPCStandings
        Dim StandingsData As List(Of ESICharacterStandingsData)
        Dim ReturnData As String = ""
        Dim StandingType As String = ""

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "characters/" & CStr(CharacterID) & "/standings/" & TranquilityDataSource, TempTokenData, TokenData.TokenExpiration, StandingsCacheDate, CharacterID)

        If Not IsNothing(ReturnData) Then
            StandingsData = JsonConvert.DeserializeObject(Of List(Of ESICharacterStandingsData))(ReturnData)

            For Each entry In StandingsData
                Select Case entry.from_type
                    Case "agents"
                        StandingType = "Agent"
                    Case "faction"
                        StandingType = "Faction"
                    Case "npc_corp"
                        StandingType = "Corporation"
                End Select
                TempStandingsList.InsertStanding(entry.from_id, StandingType, "", entry.standing)
            Next

            Return TempStandingsList
        Else
            Return Nothing
        End If

    End Function

    Public Function GetCurrentResearchAgents(ByVal CharacterID As Long, ByVal TokenData As SavedTokenData, ByRef AgentsCacheDate As Date) As List(Of ESIResearchAgent)
        Dim ReturnData As String

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "characters/" & CStr(CharacterID) & "/agents_research/" & TranquilityDataSource, TempTokenData, TokenData.TokenExpiration, AgentsCacheDate, CharacterID)

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESIResearchAgent))(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetBlueprints(ByVal ID As Long, ByVal TokenData As SavedTokenData, ByVal ScanType As ScanType, ByRef BPCacheDate As Date) As List(Of EVEBlueprint)
        Dim ReturnedBPs As New List(Of EVEBlueprint)
        Dim TempBlueprint As EVEBlueprint
        Dim RawBPData As New List(Of ESIBlueprint)
        Dim ReturnData As String = ""

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        Dim rsLookup As SQLiteDataReader

        ' Set up query string
        If ScanType = ScanType.Personal Then
            ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "characters/" & CStr(ID) & "/blueprints/" & TranquilityDataSource,
                                                  TempTokenData, TokenData.TokenExpiration, BPCacheDate, ID)
        Else ' Corp
            ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "corporations/" & CStr(ID) & "/blueprints/" & TranquilityDataSource,
                                                  TempTokenData, TokenData.TokenExpiration, BPCacheDate, ID)
        End If

        If Not IsNothing(ReturnData) Then
            RawBPData = JsonConvert.DeserializeObject(Of List(Of ESIBlueprint))(ReturnData)

            ' Process the return data
            For Each BP In RawBPData
                TempBlueprint.ItemID = BP.item_id
                TempBlueprint.TypeID = BP.type_id
                ' Get the typeName for this bp
                DBCommand = New SQLiteCommand("SELECT typeName FROM INVENTORY_TYPES WHERE typeID = " & CStr(BP.type_id), EVEDB.DBREf)
                rsLookup = DBCommand.ExecuteReader
                If rsLookup.Read Then
                    TempBlueprint.TypeName = rsLookup.GetString(0)
                Else
                    TempBlueprint.TypeName = Unknown
                End If
                rsLookup.Close()
                TempBlueprint.LocationID = BP.location_id
                ' Get the flag id for this location
                DBCommand = New SQLiteCommand("SELECT flagID FROM INVENTORY_FLAGS WHERE flagText = '" & BP.location_flag & "'", EVEDB.DBREf)
                rsLookup = DBCommand.ExecuteReader
                If rsLookup.Read Then
                    TempBlueprint.FlagID = rsLookup.GetInt32(0)
                Else
                    TempBlueprint.FlagID = 0
                End If
                rsLookup.Close()
                TempBlueprint.Quantity = BP.quantity
                TempBlueprint.MaterialEfficiency = BP.material_efficiency
                TempBlueprint.TimeEfficiency = BP.time_efficiency
                TempBlueprint.Runs = BP.runs

                ' We determine the type of bp from quantity
                If TempBlueprint.Quantity = BPType.Original Or TempBlueprint.Quantity > 0 Then
                    ' BPO or stack of BPOs
                    TempBlueprint.BPType = BPType.Original
                ElseIf TempBlueprint.Quantity = BPType.Copy Then
                    ' BPC
                    TempBlueprint.BPType = BPType.Copy
                Else
                    ' Not sure what this is
                    TempBlueprint.BPType = 0
                End If
                TempBlueprint.Owned = False
                TempBlueprint.Scanned = True ' We just scanned it
                TempBlueprint.Favorite = False
                TempBlueprint.AdditionalCosts = 0

                ReturnedBPs.Add(TempBlueprint)
            Next

            Return ReturnedBPs
        Else
            Return Nothing
        End If

    End Function

    Public Function GetIndustryJobs(ByVal ID As Long, ByVal TokenData As SavedTokenData, ByVal JobType As ScanType, ByRef JobsCacheDate As Date) As List(Of ESIIndustryJob)
        Dim ReturnData As String = ""

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        ' Set up query string
        If JobType = ScanType.Personal Then
            ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "characters/" & CStr(ID) & "/industry/jobs/" & TranquilityDataSource & "&include_completed=true",
                                                  TempTokenData, TokenData.TokenExpiration, JobsCacheDate, ID)
        Else ' Corp
            ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "corporations/" & CStr(ID) & "/industry/jobs/" & TranquilityDataSource,
                                                  TempTokenData, TokenData.TokenExpiration, JobsCacheDate, ID)
        End If

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESIIndustryJob))(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetAssets(ByVal ID As Long, ByVal TokenData As SavedTokenData, ByVal JobType As ScanType, ByRef AssetsCacheDate As Date) As List(Of ESIAsset)
        Dim AssetList As New List(Of EVEAsset)
        Dim TempAsset As New EVEAsset
        Dim ReturnData As String = ""

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        ' Set up query string
        If JobType = ScanType.Personal Then
            ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "characters/" & CStr(ID) & "/assets/" & TranquilityDataSource,
                                                  TempTokenData, TokenData.TokenExpiration, AssetsCacheDate, ID)
        Else ' Corp
            ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "corporations/" & CStr(ID) & "/assets/" & TranquilityDataSource,
                                                  TempTokenData, TokenData.TokenExpiration, AssetsCacheDate, ID)
        End If

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESIAsset))(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetCorpRoles(ByVal CharacterID As Long, ByVal CorporationID As Long, ByVal TokenData As SavedTokenData, ByRef RolesCacheDate As Date) As List(Of ESICorporationRoles)
        Dim ReturnData As String

        Dim TempTokenData As New ESITokenData
        TempTokenData = FormatTokenData(TokenData)

        ReturnData = GetPrivateAuthorizedData(ESIPublicURL & "corporations/" & CStr(CorporationID) & "/roles/" & TranquilityDataSource, TempTokenData, TokenData.TokenExpiration, RolesCacheDate, CharacterID)

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESICorporationRoles))(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    Public Sub SetCorporationData(ByVal ID As Long, ByRef DataCacheDate As Date)
        Dim ReturnData As String = ""
        Dim SQL As String = ""
        Dim CorpData As ESICorporation = Nothing

        ' Set up query string
        ReturnData = GetPublicData(ESIPublicURL & "corporations/" & CStr(ID) & TranquilityDataSource, DataCacheDate)

        If Not IsNothing(ReturnData) Then
            CorpData = JsonConvert.DeserializeObject(Of ESICorporation)(ReturnData)

            If Not IsNothing(CorpData) Then
                Call EVEDB.BeginSQLiteTransaction()

                ' See if we insert or update
                Dim rsCheck As SQLiteDataReader
                ' Load up all the data for the corporation
                SQL = "SELECT * FROM ESI_CORPORATION_DATA WHERE CORPORATION_ID = " & ID

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                rsCheck = DBCommand.ExecuteReader

                If rsCheck.Read Then
                    ' Found a record, so update the data
                    With CorpData
                        SQL = "UPDATE ESI_CORPORATION_DATA SET "
                        SQL &= "CORPORATION_NAME = " & BuildInsertFieldString(.name) & ","
                        SQL &= "TICKER = " & BuildInsertFieldString(.ticker) & ","
                        SQL &= "MEMBER_COUNT = " & BuildInsertFieldString(.member_count) & ","
                        SQL &= "FACTION_ID = " & BuildInsertFieldString(.faction_id) & ","
                        SQL &= "ALLIANCE_ID = " & BuildInsertFieldString(.alliance_id) & ","
                        SQL &= "CEO_ID = " & BuildInsertFieldString(.ceo_id) & ","
                        SQL &= "CREATOR_ID = " & BuildInsertFieldString(.creator_id) & ","
                        SQL &= "HOME_STATION_ID = " & BuildInsertFieldString(.home_station_id) & ","
                        SQL &= "SHARES = " & BuildInsertFieldString(.shares) & ","
                        SQL &= "TAX_RATE = " & BuildInsertFieldString(.tax_rate) & ","
                        SQL &= "DESCRIPTION = " & BuildInsertFieldString(.description) & ","
                        SQL &= "DATE_FOUNDED = " & BuildInsertFieldString(.date_founded) & ","
                        SQL &= "URL = " & BuildInsertFieldString(.date_founded) & " "
                        SQL &= "WHERE CORPORATION_ID = " & CStr(ID)
                    End With
                Else
                    ' New record
                    With CorpData
                        SQL = "INSERT INTO ESI_CORPORATION_DATA VALUES ("
                        SQL &= BuildInsertFieldString(ID) & ","
                        SQL &= BuildInsertFieldString(.name) & ","
                        SQL &= BuildInsertFieldString(.ticker) & ","
                        SQL &= BuildInsertFieldString(.member_count) & ","
                        SQL &= BuildInsertFieldString(.faction_id) & ","
                        SQL &= BuildInsertFieldString(.alliance_id) & ","
                        SQL &= BuildInsertFieldString(.ceo_id) & ","
                        SQL &= BuildInsertFieldString(.creator_id) & ","
                        SQL &= BuildInsertFieldString(.home_station_id) & ","
                        SQL &= BuildInsertFieldString(.shares) & ","
                        SQL &= BuildInsertFieldString(.tax_rate) & ","
                        SQL &= BuildInsertFieldString(.description) & ","
                        SQL &= BuildInsertFieldString(.date_founded) & ","
                        SQL &= BuildInsertFieldString(.url) & ","
                        SQL &= "NULL,NULL,NULL,NULL,NULL)"
                    End With

                End If

                Call EVEDB.ExecuteNonQuerySQL(SQL)

                Call EVEDB.CommitSQLiteTransaction()

                DBCommand = Nothing

            End If
        End If

    End Sub

#End Region

#Region "Public Data"

    ' Gets the ESI file from CCP for the current Market orders (buy and sell) for the region_id and type_id sent
    ' Open transaction will open an SQL transaction here instead of the calling function
    ' Returns boolean if the history was updated or not
    Public Function UpdateMarketOrders(ByRef MHDB As DBConnection, ByVal TypeID As Long, ByVal RegionID As Long,
                                    Optional OpenTransaction As Boolean = True,
                                    Optional IgnoreCacheLookup As Boolean = False) As Boolean
        Dim MarketOrdersOutput As List(Of ESIMarketOrder)
        Dim SQL As String
        Dim rsCache As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader
        Dim CacheDate As Date = NoDate
        Dim PublicData As String = ""

        ' For looking up market order data
        Dim StationsData As New StationLocation

        If Not IgnoreCacheLookup Then
            ' First look up the cache date to see if it's time to run the update
            SQL = "SELECT CACHE_DATE FROM MARKET_ORDERS_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
            DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
            rsCache = DBCommand.ExecuteReader

            CacheDate = ProcessCacheDate(rsCache)

            rsCache.Close()
            rsCache = Nothing
            DBCommand = Nothing
        Else
            CacheDate = NoDate
        End If

        ' If it's later than now, update
        If CacheDate <= Now Then
            ' Always open here incase we update below
            If OpenTransaction Then
                Call MHDB.BeginSQLiteTransaction()
            End If

            ' Delete any records for this type and region since we have a fresh set to load
            Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_ORDERS WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))

            ' Get the data from ESI
            PublicData = GetPublicData(ESIPublicURL & "markets/" & CStr(RegionID) & "/orders/" & TranquilityDataSource & "&type_id=" & CStr(TypeID), CacheDate)

            If Not IsNothing(PublicData) Then
                MarketOrdersOutput = JsonConvert.DeserializeObject(Of List(Of ESIMarketOrder))(PublicData)

                ' Parse the data
                If MarketOrdersOutput.Count > 0 Then
                    Application.DoEvents()

                    ' Now read through all the output items that are not in the table insert them in MARKET_ORDERS
                    For i = 0 To MarketOrdersOutput.Count - 1
                        With MarketOrdersOutput(i)
                            Dim StationLocation As SystemRegion
                            Dim OrderDownloadType As String = ""

                            StationLocation = StationsData.FindStationInfo(.location_id)

                            Dim IssueDate As Date = FormatESIDate(.issued)

                            ' Insert all the new records
                            SQL = "INSERT INTO MARKET_ORDERS VALUES (" & CStr(.order_id) & "," & CStr(TypeID) & ","
                            SQL &= .location_id & "," & CStr(StationLocation.RegionID) & "," & CStr(StationLocation.SystemID) & ",'"
                            SQL &= CStr(IssueDate) & "'," & .duration & "," & CStr(CInt(.is_buy_order)) & "," & .price & "," & .volume_total & ","
                            SQL &= .min_volume & "," & .volume_remain & ",'" & .range & "')"
                            Call MHDB.ExecuteNonQuerySQL(SQL)

                        End With

                        Application.DoEvents()
                    Next

                End If
            Else
                ' Json file didn't download
                Return False
            End If

            ' Set the Cache Date for everything queried 
            Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_ORDERS_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))
            Call MHDB.ExecuteNonQuerySQL("INSERT INTO MARKET_ORDERS_UPDATE_CACHE VALUES (" & CStr(TypeID) & "," & CStr(RegionID) & "," & "'" & Format(CacheDate, SQLiteDateFormat) & "')")

            ' Done updating
            If OpenTransaction Then
                Call MHDB.CommitSQLiteTransaction()
            End If

            rsCache = Nothing
            rsCheck = Nothing
            DBCommand = Nothing

            Return True

        Else
            Return False
        End If

        Return True

    End Function

    ' Provides per day summary of market activity for 13 months for the region_id and type_id sent. (cache: 23 hours)
    ' Open transaction will open an SQL transaction here instead of the calling function
    ' Returns boolean if the history was updated or not
    Public Function UpdateMarketHistory(ByRef MHDB As DBConnection, ByVal TypeID As Long, ByVal RegionID As Long,
                                        Optional ByRef IgnoreCacheLookup As Boolean = False, Optional OpenTransaction As Boolean = False) As Boolean
        Dim MarketPricesOutput As List(Of ESIMarketHistoryItem)
        Dim SQL As String = ""
        Dim rsCache As SQLiteDataReader
        Dim rsCheck As SQLiteDataReader
        Dim MaxRecordDate As Date = NoDate
        Dim CacheDate As Date = NoDate
        Dim ESICacheDate As Date = NoDate
        Dim PublicData As String = ""
        Dim HistoryDate As Date

        Try
            If Not IgnoreCacheLookup Then
                ' First look up the cache date to see if it's time to run the update
                SQL = "SELECT CACHE_DATE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
                DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
                rsCache = DBCommand.ExecuteReader

                CacheDate = ProcessCacheDate(rsCache)

                rsCache.Close()
                rsCache = Nothing
                DBCommand = Nothing
            Else
                CacheDate = NoDate
            End If

            ' If it's later than now, update
            If CacheDate <= Now Then
                ' Always open here incase we update below
                If OpenTransaction Then
                    Call MHDB.BeginSQLiteTransaction()
                End If

                Application.DoEvents()

                ' Get the data from ESI
                PublicData = GetPublicData(ESIPublicURL & "markets/" & CStr(RegionID) & "/history/" & TranquilityDataSource & "&type_id=" & CStr(TypeID), ESICacheDate)

                MarketPricesOutput = JsonConvert.DeserializeObject(Of List(Of ESIMarketHistoryItem))(PublicData)

                ' Read in the data
                If Not IsNothing(MarketPricesOutput) Then

                    If MarketPricesOutput.Count > 0 Then
                        ' See what the last cache date we have on the records first - any records after or equal to this date we want to update
                        If CacheDate = NoDate Then ' only run this if we don't already have the max date for this typeid
                            SQL = "SELECT CACHE_DATE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
                            DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
                            rsCheck = DBCommand.ExecuteReader

                            If rsCheck.Read And Not IsDBNull(rsCheck.GetValue(0)) Then
                                ' The cache date is the date when we run the next update
                                MaxRecordDate = CDate(rsCheck.GetString(0))
                            Else
                                MaxRecordDate = NoDate
                            End If
                            rsCheck.Close()
                        Else
                            MaxRecordDate = CacheDate
                        End If

                        Application.DoEvents()
                        Dim i As Integer

                        ' Now read through all the output items that are not in the table insert them in MARKET_HISTORY
                        For i = 0 To MarketPricesOutput.Count - 1
                            With MarketPricesOutput(i)
                                HistoryDate = FormatESIDate(.history_date)
                                ' only insert the records that are larger than the max date (with no time or 0:00:00 in GMT when records are updated)
                                If HistoryDate > MaxRecordDate.Date Then
                                    SQL = "INSERT INTO MARKET_HISTORY VALUES (" & CStr(TypeID) & "," & CStr(RegionID) & ",'" & Format(HistoryDate, SQLiteDateFormat) & "',"
                                    SQL &= CStr(.lowest) & "," & CStr(.highest) & "," & CStr(.average) & "," & CStr(.order_count) & "," & CStr(.volume) & ")"
                                    Call MHDB.ExecuteNonQuerySQL(SQL)
                                End If
                            End With

                            Application.DoEvents()
                        Next
                    End If

                    ' Set the Cache Date for everything queried 
                    Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))
                    Call MHDB.ExecuteNonQuerySQL("INSERT INTO MARKET_HISTORY_UPDATE_CACHE VALUES (" & CStr(TypeID) & "," & CStr(RegionID) & "," & "'" & Format(ESICacheDate, SQLiteDateFormat) & "')")

                    ' Done updating
                    If OpenTransaction Then
                        Call MHDB.CommitSQLiteTransaction()
                    End If

                    Return True

                End If
                ' Json file didn't download
                Return False
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    ' Returns the list of trade-able types and their average market price, as shown in the inventory UI in the EVE client. 
    ' Also includes an adjusted market price which is used in industry calculations.
    Public Function UpdateAdjAvgMarketPrices(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim MarketPricesOutput As List(Of ESIMarketAdjustedPrice)
        Dim SQL As String
        Dim TempLabel As Label
        Dim TempPB As ProgressBar
        Dim PublicData As String

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Get the current list of agents updated
        If CB.DataUpdateable(CacheDateType.MarketPrices) Then

            TempLabel.Text = "Downloading Adjusted Market Price Data..."
            Application.DoEvents()

            ' Get the data from ESI
            PublicData = GetPublicData(ESIPublicURL & "markets/prices/" & TranquilityDataSource, CacheDate)

            MarketPricesOutput = JsonConvert.DeserializeObject(Of List(Of ESIMarketAdjustedPrice))(PublicData)

            ' Read in the data
            If Not IsNothing(MarketPricesOutput) Then
                If MarketPricesOutput.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    ' Clear the old records first
                    Call EVEDB.ExecuteNonQuerySQL("UPDATE ITEM_PRICES SET ADJUSTED_PRICE = 0, AVERAGE_PRICE = 0")

                    TempLabel.Text = "Saving Adjusted Market Price Data..."
                    TempPB.Minimum = 0
                    TempPB.Value = 0
                    TempPB.Maximum = MarketPricesOutput.Count - 1
                    TempPB.Visible = True
                    Application.DoEvents()

                    ' Now read through all the output items and update them in ITEM_PRICES
                    For i = 0 To MarketPricesOutput.Count - 1
                        With MarketPricesOutput(i)
                            Dim AdjustedPrice As String
                            If Not IsNothing(.adjusted_Price) Then
                                AdjustedPrice = ConvertEUDecimaltoUSDecimal(.adjusted_Price)
                            Else
                                AdjustedPrice = "0.00"
                            End If

                            Dim AveragePrice As String
                            If Not IsNothing(.average_Price) Then
                                AveragePrice = ConvertEUDecimaltoUSDecimal(.average_Price)
                            Else
                                AveragePrice = "0.00"
                            End If
                            SQL = "UPDATE ITEM_PRICES SET ADJUSTED_PRICE = " & AdjustedPrice & ", AVERAGE_PRICE = " & AveragePrice
                            SQL &= " WHERE ITEM_ID = " & CStr(.type_id)
                            Call EVEDB.ExecuteNonQuerySQL(SQL)
                        End With

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()

                    Next

                    ' All set, update cache date before leaving
                    Call CB.UpdateCacheDate(CacheDateType.MarketPrices, CacheDate)

                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()
                    Return True
                End If
            End If
            ' Data didn't download
            Return False
        End If

        Return True

    End Function

    ' This returns a list of all publicly accessible facilities, including player built outposts in nullsec.
    Public Function UpdateIndustryFacilties(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing,
                                            Optional SplashVisible As Boolean = False) As Boolean
        Dim IndustryFacilitiesOutput As List(Of ESIIndustryFacility)
        Dim FacilitiesList As New List(Of Station)
        Dim SQL As String
        Dim SQLBase As String
        Dim PublicData As String
        Dim rsLookup As SQLiteDataReader

        Dim StatusText As String = ""

        Dim SystemIndiciesUpdated As Boolean
        Dim SuccessfulDownload As Boolean

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        Dim FacilityName As String
        Dim i As Integer

        Dim TempStation As Station

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        ' Before doing anything, update the system indicies
        SuccessfulDownload = UpdateIndustrySystemsCostIndex(SystemIndiciesUpdated, UpdateLabel, PB)

        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Get the current list of agents updated
        If CB.DataUpdateable(CacheDateType.IndustryFacilities) And SuccessfulDownload Then

            StatusText = "Updating Facility Data..."
            If SplashVisible Then
                Call SetProgress(StatusText)
            Else
                TempLabel.Text = StatusText
            End If

            Application.DoEvents()
            ' Get the data from ESI
            PublicData = GetPublicData(ESIPublicURL & "industry/facilities/" & TranquilityDataSource, CacheDate)

            IndustryFacilitiesOutput = JsonConvert.DeserializeObject(Of List(Of ESIIndustryFacility))(PublicData)
            Dim StationNameLookupList As New List(Of FacilityCorpIDPair)
            Dim CorpNameLookupList As New List(Of FacilityCorpIDPair)
            Dim TempPair As FacilityCorpIDPair

            ' Read in the data
            If Not IsNothing(IndustryFacilitiesOutput) Then
                ' Save this as a list of stations for processing
                For Each Facility In IndustryFacilitiesOutput
                    Dim FacilityData As StationData

                    ' Look up static data and set if there, if not save for ESI query later
                    FacilityData = GetStationData(Facility.facility_id, Facility.owner_id)
                    If FacilityData.StationName = "" Then
                        TempPair.FacilityID = Facility.facility_id
                        TempPair.OwnerID = Facility.owner_id
                        If Not StationNameLookupList.Contains(TempPair) Then
                            StationNameLookupList.Add(TempPair)
                        End If
                    End If
                    If FacilityData.OwnedbyCorporationName = "" Then
                        TempPair.FacilityID = Facility.facility_id
                        TempPair.OwnerID = Facility.owner_id
                        If Not CorpNameLookupList.Contains(TempPair) Then
                            CorpNameLookupList.Add(TempPair)
                        End If
                    End If

                    TempStation.stationID = Facility.facility_id
                    TempStation.stationName = FacilityData.StationName
                    TempStation.stationTypeID = Facility.type_id
                    TempStation.solarSystemID = Facility.solar_system_id
                    TempStation.regionID = Facility.region_id
                    TempStation.corporationID = Facility.owner_id
                    TempStation.corporationName = FacilityData.OwnedbyCorporationName
                    TempStation.tax = Facility.tax

                    ' Add to facilities list
                    FacilitiesList.Add(TempStation)

                Next
            End If

            ' Now look up any of the unfound station names and corporation names from ESI
            Dim StationNames As List(Of NameData) = GetFacilityNameData(StationNameLookupList, NameDataType.Facility)
            Dim TempFacility As Station

            If Not IsNothing(StationNames) Then
                For Each StationName In StationNames
                    ' Look up each one and set it
                    IDToFind = StationName.IndexID
                    TempFacility = FacilitiesList.Find(AddressOf FindFacility)
                    FacilitiesList.Remove(TempFacility)
                    TempFacility.stationName = StationName.Name
                    FacilitiesList.Add(TempFacility)
                Next
            End If

            Dim CorporationNames As List(Of NameData) = GetFacilityNameData(CorpNameLookupList, NameDataType.Owner)
            If Not IsNothing(CorporationNames) Then
                For Each Corp In CorporationNames
                    ' Look up each one and set it
                    IDToFind = Corp.IndexID
                    TempFacility = FacilitiesList.Find(AddressOf FindFacility)
                    FacilitiesList.Remove(TempFacility)
                    TempFacility.corporationName = Corp.Name
                    FacilitiesList.Add(TempFacility)
                Next
            End If

            If FacilitiesList.Count > 0 Then

                Call EVEDB.BeginSQLiteTransaction()

                StatusText = "Saving Industry Facilities Data..."
                If SplashVisible Then
                    Call SetProgress(StatusText)
                Else
                    TempLabel.Text = StatusText
                End If
                TempPB.Minimum = 0
                TempPB.Value = 0
                TempPB.Maximum = FacilitiesList.Count - 1
                TempPB.Visible = True
                Application.DoEvents()

                ' Now read through all the output items and input them into the DB
                For i = 0 To FacilitiesList.Count - 1
                    With FacilitiesList(i)
                        ' See if this is an outpost or not and add the tag for type to the name
                        Select Case .stationTypeID
                            ' FACILITY_TYPE_ID	FACILITY_TYPE
                            ' 21644	Amarr Factory Outpost
                            ' 21645	Gallente Administrative Outpost
                            ' 21646	Minmatar Service Outpost
                            ' 21642	Caldari Research Outpost
                            ' 12294, 12242, 12295 conquerable stations
                            Case 21644
                                FacilityName = Format(.stationName) & " (A)"
                            Case 21645
                                FacilityName = Format(.stationName) & " (G)"
                            Case 21646
                                FacilityName = Format(.stationName) & " (M)"
                            Case 21642
                                FacilityName = Format(.stationName) & " (C)"
                            Case 12294, 12242, 12295
                                FacilityName = Format(.stationName) & " (CS)" ' conquerable 

                                ' Also, process this by adding a record to the ram_assembly_line_stations table so we can look them up later
                                SQL = "SELECT 'X' FROM RAM_ASSEMBLY_LINE_STATIONS WHERE stationID = " & CStr(.stationID)

                                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                                rsLookup = DBCommand.ExecuteReader

                                If Not rsLookup.Read Then
                                    ' Not in there, add the records for the five different assembly line types - copied data from other station type ids like this
                                    Call EVEDB.ExecuteNonQuerySQL(String.Format("INSERT INTO RAM_ASSEMBLY_LINE_STATIONS VALUES ({0},5,10,{1},{2},{3},{4})", .stationID, .stationTypeID, .corporationID, .solarSystemID, .regionID))
                                    Call EVEDB.ExecuteNonQuerySQL(String.Format("INSERT INTO RAM_ASSEMBLY_LINE_STATIONS VALUES ({0},6,50,{1},{2},{3},{4})", .stationID, .stationTypeID, .corporationID, .solarSystemID, .regionID))
                                    Call EVEDB.ExecuteNonQuerySQL(String.Format("INSERT INTO RAM_ASSEMBLY_LINE_STATIONS VALUES ({0},7,20,{1},{2},{3},{4})", .stationID, .stationTypeID, .corporationID, .solarSystemID, .regionID))
                                    Call EVEDB.ExecuteNonQuerySQL(String.Format("INSERT INTO RAM_ASSEMBLY_LINE_STATIONS VALUES ({0},8,20,{1},{2},{3},{4})", .stationID, .stationTypeID, .corporationID, .solarSystemID, .regionID))
                                    Call EVEDB.ExecuteNonQuerySQL(String.Format("INSERT INTO RAM_ASSEMBLY_LINE_STATIONS VALUES ({0},38,20,{1},{2},{3},{4})", .stationID, .stationTypeID, .corporationID, .solarSystemID, .regionID))
                                End If

                                rsLookup.Close()
                            Case Else
                                FacilityName = Format(.stationName)
                        End Select

                        ' Look up each facility and if found, update it. If not, insert - this way if the ESI is having issues, we won't delete all the station data (which doesn't change much)
                        SQL = "SELECT 'X' FROM INDUSTRY_FACILITIES WHERE FACILITY_ID = " & CStr(.stationID)

                        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                        rsLookup = DBCommand.ExecuteReader

                        If rsLookup.Read() Then
                            SQL = "UPDATE INDUSTRY_FACILITIES "
                            SQL &= "SET FACILITY_NAME = '" & FormatDBString(FacilityName) & "',"
                            SQL &= "FACILITY_TYPE_ID = " & CStr(.stationTypeID) & ","
                            SQL &= "FACILITY_TAX = " & CStr(.tax) & ","
                            SQL &= "SOLAR_SYSTEM_ID = " & CStr(.solarSystemID) & ","
                            SQL &= "REGION_ID = " & CStr(.regionID) & ","
                            SQL &= "OWNER_ID = " & CStr(.corporationID) & " "
                            SQL &= "WHERE FACILITY_ID = " & CStr(.stationID)
                            ErrorTracker = SQL
                        Else ' New record, insert
                            SQL = "INSERT INTO INDUSTRY_FACILITIES VALUES ("
                            SQL &= CStr(.stationID) & ",'"
                            SQL &= FormatDBString(FacilityName) & "',"
                            SQL &= CStr(.stationTypeID) & ","
                            SQL &= CStr(.tax) & ","
                            SQL &= CStr(.solarSystemID) & ","
                            SQL &= CStr(.regionID) & ","
                            SQL &= CStr(.corporationID) & ")"
                            ErrorTracker = SQL
                        End If

                        Call EVEDB.ExecuteNonQuerySQL(SQL)

                        rsLookup.Close()
                        DBCommand = Nothing

                    End With

                    ' For each record, update the progress bar
                    Call IncrementProgressBar(TempPB)
                    Application.DoEvents()
                Next

                ' Now that everything is inserted, update the master station table that we can query for anything
                StatusText = "Updating Stations Data..."
                If SplashVisible Then
                    Call SetProgress(StatusText)
                Else
                    TempLabel.Text = StatusText
                End If

                Application.DoEvents()

                ' Find all facilities not already in the stations table and loop through to add them
                SQL = "SELECT DISTINCT FACILITY_ID "
                SQLBase = "FROM INDUSTRY_FACILITIES WHERE FACILITY_ID NOT IN (SELECT DISTINCT FACILITY_ID FROM STATION_FACILITIES) "
                SQLBase &= "AND (FACILITY_ID IN (SELECT stationID FROM RAM_ASSEMBLY_LINE_STATIONS) " ' Stations with assembly lines
                SQLBase &= "OR FACILITY_TYPE_ID IN (21642,21644,21645,21646,12242,12294,12295)) " ' Outpost types

                Call SetProgressBar(TempPB, "SELECT COUNT(DISTINCT FACILITY_ID) " & SQLBase)
                Application.DoEvents()

                DBCommand = New SQLiteCommand(SQL & SQLBase, EVEDB.DBREf)
                rsLookup = DBCommand.ExecuteReader

                While rsLookup.Read
                    Call SetStationFacilityData(rsLookup.GetInt64(0))
                    Call IncrementProgressBar(TempPB)
                End While

                rsLookup.Close()
                DBCommand = Nothing

                '' Update Tax rates - ignore this until they actually change, NPC is set by CCP and outposts don't get sent through ESI
                'SQL = "Select DISTINCT FACILITY_ID, FACILITY_TAX FROM INDUSTRY_FACILITIES WHERE OUTPOST = 0"
                'DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                'rsLookup = DBCommand.ExecuteReader

                'While rsLookup.Read
                '    SQL = "UPDATE STATION_FACILITIES Set FACILITY_TAX = " & CStr(rsLookup.GetDouble(1)) & " WHERE FACILITY_ID = " & CStr(rsLookup.GetInt64(0))
                '    Call evedb.ExecuteNonQuerySQL(SQL)
                'End While

                'rsLookup.Close()
                'DBCommand = Nothing

                StatusText = "Refreshing Station Data..."
                If SplashVisible Then
                    Call SetProgress(StatusText)
                Else
                    TempLabel.Text = StatusText
                End If

                ' Update the outposts names, which can change and do
                SQL = "SELECT DISTINCT FACILITY_NAME, FACILITY_ID "
                SQLBase = "FROM INDUSTRY_FACILITIES WHERE FACILITY_TYPE_ID In (21642,21644,21645,21646,12242,12294,12295) " ' Outpost types

                Call SetProgressBar(TempPB, "SELECT COUNT(DISTINCT FACILITY_ID) " & SQLBase)

                DBCommand = New SQLiteCommand(SQL & SQLBase, EVEDB.DBREf)
                rsLookup = DBCommand.ExecuteReader

                While rsLookup.Read
                    SQL = "UPDATE STATION_FACILITIES SET FACILITY_NAME = '" & FormatDBString(rsLookup.GetString(0)) & "' WHERE FACILITY_ID = " & CStr(rsLookup.GetInt64(1))
                    Call EVEDB.ExecuteNonQuerySQL(SQL)
                    Call IncrementProgressBar(TempPB)
                End While

                rsLookup.Close()
                DBCommand = Nothing

                ' Clear out all the outposts from STATIONS to get the most updated data
                SQL = "DELETE FROM STATIONS WHERE STATION_TYPE_ID IN (21642,21644,21645,21646,12242,12294,12295) " ' Outpost types
                EVEDB.ExecuteNonQuerySQL(SQL)

                ' Now insert non-SDE stations (Outposts) into the stations table for easy look ups in assets
                SQL = "SELECT DISTINCT FACILITY_ID, FACILITY_NAME, FACILITY_TYPE_ID, SOLAR_SYSTEM_ID, SOLAR_SYSTEM_SECURITY, REGION_ID "
                SQLBase = "FROM STATION_FACILITIES WHERE FACILITY_ID NOT IN (SELECT STATION_ID AS FACILITY_ID FROM STATIONS) "

                Call SetProgressBar(TempPB, "SELECT COUNT(DISTINCT FACILITY_ID) " & SQLBase)

                DBCommand = New SQLiteCommand(SQL & SQLBase, EVEDB.DBREf)
                rsLookup = DBCommand.ExecuteReader

                ' Insert the new data
                While rsLookup.Read()
                    ' Get the owner of facility info from above
                    IDToFind = rsLookup.GetInt64(0)
                    TempFacility = FacilitiesList.Find(AddressOf FindFacility)
                    Dim OwnerID As Long = 0
                    If Not IsNothing(TempFacility) Then
                        OwnerID = TempFacility.corporationID
                    End If

                    SQL = "INSERT INTO STATIONS VALUES (" & CStr(rsLookup.GetInt64(0)) & ","
                    SQL &= "'" & FormatDBString(rsLookup.GetString(1)) & "',"
                    SQL &= CStr(rsLookup.GetInt64(2)) & ","
                    SQL &= CStr(rsLookup.GetInt64(3)) & ","
                    SQL &= CStr(rsLookup.GetFloat(4)) & ","
                    SQL &= CStr(rsLookup.GetInt64(5)) & ","
                    SQL &= CStr(OwnerID) & ","
                    SQL &= "0,0)" ' If we don't know the refinery data then it wasn't in the SDE, so set to zero
                    ErrorTracker = SQL
                    Call EVEDB.ExecuteNonQuerySQL(SQL)
                    Call IncrementProgressBar(TempPB)
                    Application.DoEvents()
                End While

                Call EVEDB.CommitSQLiteTransaction()

                ' All set, update cache date before leaving
                Call CB.UpdateCacheDate(CacheDateType.IndustryFacilities, CacheDate)

                ErrorTracker = ""

                StatusText = "Finalizing Data..."
                If SplashVisible Then
                    Call SetProgress(StatusText)
                Else
                    TempLabel.Text = StatusText
                End If

                ' Finally, Update the cost indicies for the solar system of the stations every time we update the system indicies (above)
                If SystemIndiciesUpdated Then
                    Call EVEDB.BeginSQLiteTransaction()
                    SQL = "SELECT DISTINCT SOLAR_SYSTEM_ID, ACTIVITY_ID, COST_INDEX FROM INDUSTRY_SYSTEMS_COST_INDICIES"
                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    rsLookup = DBCommand.ExecuteReader

                    While rsLookup.Read
                        SQL = "UPDATE STATION_FACILITIES SET COST_INDEX = " & CStr(rsLookup.GetDouble(2)) & " "
                        SQL &= " WHERE SOLAR_SYSTEM_ID = " & CStr(rsLookup.GetInt64(0)) & " AND ACTIVITY_ID = " & CStr(rsLookup.GetInt32(1))
                        Call EVEDB.ExecuteNonQuerySQL(SQL)
                    End While

                    rsLookup.Close()
                    DBCommand = Nothing

                    Call EVEDB.CommitSQLiteTransaction()
                End If

                Return True

            End If

            Return False

        End If

        Return True

    End Function

    Private Sub SetProgressBar(ByRef PB As ProgressBar, ByVal SQLCount As String)
        Dim rsCount As SQLiteDataReader
        DBCommand = New SQLiteCommand(SQLCount, EVEDB.DBREf)
        rsCount = DBCommand.ExecuteReader

        PB.Visible = False
        If rsCount.Read Then
            If rsCount.GetInt32(0) > 0 Then
                PB.Minimum = 0
                PB.Value = 0
                PB.Maximum = rsCount.GetInt32(0) - 1
                PB.Visible = True
            End If
        End If

        Application.DoEvents()

    End Sub

    ' Predicate for finding an ID in a list of facilities
    Private Function FindFacility(ByVal Facility As Station) As Boolean
        If Facility.stationID = IDToFind Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Predicate for finding an owner ID in a list of facility/corp id pairs
    Private Function FindOwner(ByVal Pair As FacilityCorpIDPair) As Boolean
        If Pair.OwnerID = IDToFind Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Predicate for finding an owner ID in a list of facility/corp id pairs
    Private Function FindFacility(ByVal Pair As FacilityCorpIDPair) As Boolean
        If Pair.FacilityID = IDToFind Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Structure FacilityCorpIDPair
        Dim FacilityID As Long
        Dim OwnerID As Long
    End Structure

    ' Sets the Station data sent for the facility ID in the STATION_FACILITIES table
    ' After all tables updated from ESI calls
    Private Sub SetStationFacilityData(ByVal FacilityID As Long)
        Dim SQL As String
        Dim rsFacility As SQLiteDataReader

        ' Set the query first
        SQL = "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL &= "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL &= "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL &= "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL &= "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL &= "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.timeMultiplier AS TIME_MULTIPLIER, "
        SQL &= "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.costMultiplier AS COST_MULTIPLIER, "
        SQL &= "INVENTORY_GROUPS.groupID AS GROUP_ID, 0 AS CATEGORY_ID, INDUSTRY_SYSTEMS_COST_INDICIES.COST_INDEX, 0 AS OUTPOST "
        SQL &= "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_ASSEMBLY_LINE_STATIONS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL &= "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP, INVENTORY_GROUPS "
        SQL &= "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL &= "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL &= "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.assemblyLineTypeID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.groupID = INVENTORY_GROUPS.groupID "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_ID = RAM_ASSEMBLY_LINE_STATIONS.stationID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL &= "AND RAM_ASSEMBLY_LINE_STATIONS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "
        SQL &= "UNION "
        SQL &= "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL &= "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL &= "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL &= "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL &= "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL &= "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.timeMultiplier AS TIME_MULTIPLIER, "
        SQL &= "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.costMultiplier AS COST_MULTIPLIER, "
        SQL &= "0 AS GROUP_ID, INVENTORY_CATEGORIES.categoryID AS CATEGORY_ID, COST_INDEX, 0 AS OUTPOST "
        SQL &= "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_ASSEMBLY_LINE_STATIONS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL &= "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY, INVENTORY_CATEGORIES "
        SQL &= "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL &= "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL &= "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.assemblyLineTypeID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.categoryID = INVENTORY_CATEGORIES.categoryID "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_ID = RAM_ASSEMBLY_LINE_STATIONS.stationID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL &= "AND RAM_ASSEMBLY_LINE_STATIONS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "
        SQL &= "UNION "
        SQL &= "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL &= "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL &= "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL &= "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL &= "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL &= "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.timeMultiplier AS TIME_MULTIPLIER, "
        SQL &= "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.costMultiplier AS COST_MULTIPLIER, "
        SQL &= "INVENTORY_GROUPS.groupID AS GROUP_ID, 0 AS CATEGORY_ID, COST_INDEX, 1 AS OUTPOST "
        SQL &= "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_INSTALLATION_TYPE_CONTENTS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL &= "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP, INVENTORY_GROUPS "
        SQL &= "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL &= "AND FACILITY_TYPE_ID IN (21642,21644,21645,21646) "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = RAM_INSTALLATION_TYPE_CONTENTS.installationTypeID "
        SQL &= "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL &= "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.assemblyLineTypeID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_GROUP.groupID = INVENTORY_GROUPS.groupID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL &= "AND RAM_INSTALLATION_TYPE_CONTENTS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "
        SQL &= "UNION "
        SQL &= "SELECT INDUSTRY_FACILITIES.FACILITY_ID, INDUSTRY_FACILITIES.FACILITY_NAME, "
        SQL &= "SOLAR_SYSTEMS.solarSystemID AS SOLAR_SYSTEM_ID, SOLAR_SYSTEMS.solarSystemName AS SOLAR_SYSTEM_NAME, "
        SQL &= "SOLAR_SYSTEMS.security AS SOLAR_SYSTEM_SECURITY, REGIONS.regionID AS REGION_ID, REGIONS.regionName AS REGION_NAME, "
        SQL &= "FACILITY_TYPE_ID, typeName AS FACILITY_TYPE, RAM_ACTIVITIES.activityID AS ACTIVITY_ID, FACILITY_TAX,"
        SQL &= "baseMaterialMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.materialMultiplier AS MATERIAL_MULTIPLIER, "
        SQL &= "baseTimeMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.timeMultiplier AS TIME_MULTIPLIER, "
        SQL &= "baseCostMultiplier * RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.costMultiplier AS COST_MULTIPLIER, "
        SQL &= "0 AS GROUP_ID, INVENTORY_CATEGORIES.categoryID AS CATEGORY_ID, COST_INDEX, 1 AS OUTPOST "
        SQL &= "FROM INDUSTRY_FACILITIES, INVENTORY_TYPES, RAM_INSTALLATION_TYPE_CONTENTS, REGIONS, SOLAR_SYSTEMS, INDUSTRY_SYSTEMS_COST_INDICIES, "
        SQL &= "RAM_ACTIVITIES, RAM_ASSEMBLY_LINE_TYPES, RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY, INVENTORY_CATEGORIES "
        SQL &= "WHERE INDUSTRY_FACILITIES.FACILITY_ID = " & CStr(FacilityID) & " "
        SQL &= "AND FACILITY_TYPE_ID IN (21642,21644,21645,21646) "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = INVENTORY_TYPES.typeID "
        SQL &= "AND INDUSTRY_FACILITIES.FACILITY_TYPE_ID = RAM_INSTALLATION_TYPE_CONTENTS.installationTypeID "
        SQL &= "AND INDUSTRY_FACILITIES.REGION_ID = REGIONS.regionID "
        SQL &= "AND INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.assemblyLineTypeID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPE_DETAIL_PER_CATEGORY.categoryID = INVENTORY_CATEGORIES.categoryID "
        SQL &= "AND RAM_ASSEMBLY_LINE_TYPES.activityID = RAM_ACTIVITIES.activityID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.SOLAR_SYSTEM_ID = INDUSTRY_FACILITIES.SOLAR_SYSTEM_ID "
        SQL &= "AND INDUSTRY_SYSTEMS_COST_INDICIES.ACTIVITY_ID = RAM_ASSEMBLY_LINE_TYPES.activityID "
        SQL &= "AND RAM_INSTALLATION_TYPE_CONTENTS.assemblyLineTypeID = RAM_ASSEMBLY_LINE_TYPES.assemblyLineTypeID "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsFacility = DBCommand.ExecuteReader

        While rsFacility.Read
            SQL = "INSERT INTO STATION_FACILITIES VALUES ("
            SQL &= CStr(rsFacility.GetInt64(0)) & ", " ' Facility ID
            SQL &= "'" & FormatDBString(rsFacility.GetString(1)) & "', " ' Facility Name
            SQL &= CStr(rsFacility.GetInt64(2)) & ", " ' Solar System ID
            SQL &= "'" & FormatDBString(rsFacility.GetString(3)) & "', " ' Solar System Name
            SQL &= CStr(rsFacility.GetDouble(4)) & ", " ' Solar System Security
            SQL &= CStr(rsFacility.GetInt64(5)) & ", " ' Region ID
            SQL &= "'" & FormatDBString(rsFacility.GetString(6)) & "', " ' Region Name
            SQL &= CStr(rsFacility.GetInt64(7)) & ", " ' Facility Type ID
            SQL &= "'" & FormatDBString(rsFacility.GetString(8)) & "', " ' Facility Type
            SQL &= CStr(rsFacility.GetInt64(9)) & ", " ' Activity ID
            SQL &= CStr(rsFacility.GetDouble(10)) & ", " ' Facility Tax
            SQL &= CStr(rsFacility.GetDouble(11)) & ", " ' Material Multiplier
            SQL &= CStr(rsFacility.GetDouble(12)) & ", " ' Time Multiplier
            SQL &= CStr(rsFacility.GetDouble(13)) & ", " ' Cost Multiplier
            SQL &= CStr(rsFacility.GetInt64(14)) & ", " ' Group ID
            SQL &= CStr(rsFacility.GetInt64(15)) & ", " ' Category ID
            SQL &= CStr(rsFacility.GetDouble(16)) & ", " ' Cost Index
            Select Case rsFacility.GetInt64(7)
                Case 12242, 12294, 12295
                    SQL &= "1)" ' Outpost for conquerable
                Case Else
                    SQL &= CStr(rsFacility.GetInt32(17)) & ")" ' Outpost 
            End Select

            Call EVEDB.ExecuteNonQuerySQL(SQL)

            Application.DoEvents()
        End While

    End Sub

    ' Lists the cost index for installing industry jobs per type of activity. This does not include wormhole space.
    ' Make private so we can only run with the update industry facilities function
    Private Function UpdateIndustrySystemsCostIndex(ByRef IndiciesUpdated As Boolean, Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
        Dim IndustrySystemsIndex As List(Of ESISystemCostIndices)
        Dim SQL As String
        Dim rsLookup As SQLiteDataReader
        Dim PublicData As String
        Dim SolarSystemID As Integer
        Dim SolarSystemName As String
        Dim ActivityID As Integer

        Dim TempLabel As Label
        Dim TempPB As ProgressBar

        If IsNothing(UpdateLabel) Then
            TempLabel = New Label
        Else
            TempLabel = UpdateLabel
        End If

        If IsNothing(PB) Then
            TempPB = New ProgressBar
        Else
            TempPB = PB
        End If

        Dim CB As New CacheBox
        Dim CacheDate As Date

        ' Get the current list of agents updated
        If CB.DataUpdateable(CacheDateType.IndustrySystems) Then
            IndiciesUpdated = True
            TempLabel.Text = "Downloading System Index Data..."
            Application.DoEvents()

            ' Get the data from ESI
            PublicData = GetPublicData(ESIPublicURL & "industry/systems/" & TranquilityDataSource, CacheDate)

            IndustrySystemsIndex = JsonConvert.DeserializeObject(Of List(Of ESISystemCostIndices))(PublicData)

            ' Read in the data
            If Not IsNothing(IndustrySystemsIndex) Then
                If IndustrySystemsIndex.Count > 0 Then
                    Call EVEDB.BeginSQLiteTransaction()

                    TempLabel.Text = "Saving System Index Data..."
                    TempPB.Minimum = 0
                    TempPB.Value = 0
                    TempPB.Maximum = IndustrySystemsIndex.Count - 1
                    TempPB.Visible = True
                    Application.DoEvents()

                    ' Now read through all the output items and input them into the DB
                    For i = 0 To IndustrySystemsIndex.Count - 1

                        SolarSystemID = IndustrySystemsIndex(i).solar_system_id
                        SolarSystemName = GetSolarSystemName(SolarSystemID)

                        For j = 0 To IndustrySystemsIndex(i).cost_indices.Count - 1
                            With IndustrySystemsIndex(i).cost_indices(j)
                                ' Update name
                                ' copying, duplicating, invention, manufacturing, none, reaction, researching_material_efficiency, researching_technology, researching_time_efficiency, reverse_engineering 
                                If .activity = "reaction" Then
                                    .activity = "Reactions"
                                ElseIf .activity.Contains("_") Then
                                    .activity = .activity.Replace("_", " ") ' replace underscores with spaces
                                End If

                                ' Format for title 
                                .activity = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(.activity)

                                ActivityID = GetActivityID(.activity)
                                ' Look up each system and if found, update it. If not, insert - this way if the ESI is having issues, we won't delete all the station data (which doesn't change much)
                                SQL = "SELECT 'X' FROM INDUSTRY_SYSTEMS_COST_INDICIES WHERE SOLAR_SYSTEM_ID = " & CStr(SolarSystemID) & " AND ACTIVITY_ID = " & CStr(ActivityID)

                                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                                rsLookup = DBCommand.ExecuteReader

                                If rsLookup.Read Then
                                    ' Update the old
                                    SQL = "UPDATE INDUSTRY_SYSTEMS_COST_INDICIES "
                                    SQL &= "SET SOLAR_SYSTEM_NAME = '" & FormatDBString(SolarSystemName) & "',"
                                    SQL &= "ACTIVITY_ID = " & CStr(ActivityID) & ","
                                    SQL &= "ACTIVITY_NAME = '" & FormatDBString(.activity) & "',"
                                    SQL &= "COST_INDEX = " & CStr(.cost_index) & " "
                                    SQL &= "WHERE SOLAR_SYSTEM_ID = " & CStr(SolarSystemID) & " AND ACTIVITY_ID = " & CStr(ActivityID)
                                Else
                                    ' Insert the new record
                                    SQL = "INSERT INTO INDUSTRY_SYSTEMS_COST_INDICIES VALUES(" & CStr(SolarSystemID) & ",'" & FormatDBString(SolarSystemName) & "',"
                                    SQL &= CStr(ActivityID) & ",'" & FormatDBString(.activity) & "'," & CStr(.cost_index) & ")"
                                End If

                                Call EVEDB.ExecuteNonQuerySQL(SQL)
                            End With
                        Next

                        ' For each record, update the progress bar
                        Call IncrementProgressBar(TempPB)
                        Application.DoEvents()
                    Next

                    TempPB.Visible = False

                    ' Rebuild indexes
                    Call EVEDB.ExecuteNonQuerySQL("REINDEX IDX_ISCI_SSID_AID")

                    ' All set, update cache date before leaving
                    Call CB.UpdateCacheDate(CacheDateType.IndustrySystems, CacheDate)

                    ' Done updating
                    Call EVEDB.CommitSQLiteTransaction()

                    Return True

                End If
            End If
            ' Json file didn't download
            Return False
        Else
            IndiciesUpdated = False
        End If

        Return True

    End Function

    ' Looks up station data in the static tables, if not there, queries from ESI
    Private Function GetStationData(StationID As Long, CorporationID As Long) As StationData
        Dim rsStation As SQLiteDataReader
        Dim SQL As String
        Dim TempData As StationData

        SQL = "SELECT STATION_NAME FROM STATIONS WHERE STATION_ID = " & CStr(StationID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsStation = DBCommand.ExecuteReader

        TempData.StationID = StationID
        TempData.OwnedbyCorporationID = CorporationID
        TempData.OwnedbyCorporationName = Unknown
        TempData.StationName = Unknown

        If rsStation.Read Then
            ' Strip off the (A), (G), (M), (C), (CS)
            Dim StationName As String = rsStation.GetString(0)
            If StationName.Contains("(A)") Then
                StationName = StationName.Substring(0, InStr(StationName, "(A)") - 2)
            ElseIf StationName.Contains("(C)") Then
                StationName = StationName.Substring(0, InStr(StationName, "(C)") - 2)
            ElseIf StationName.Contains("(G)") Then
                StationName = StationName.Substring(0, InStr(StationName, "(G)") - 2)
            ElseIf StationName.Contains("(M)") Then
                StationName = StationName.Substring(0, InStr(StationName, "(M)") - 2)
            ElseIf StationName.Contains("(CS)") Then
                StationName = StationName.Substring(0, InStr(StationName, "(CS)") - 2)
            End If

            TempData.StationName = StationName
        Else
            ' Need to look up from ESI
            TempData.StationName = ""
        End If

        rsStation.Close()

        '' Try to get the  name now for the corporation of this station
        'SQL = "SELECT ITEM_NAME FROM INVENTORY_NAMES WHERE ITEM_ID = " & CStr(CorporationID)

        'DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        'rsLookup = DBCommand.ExecuteReader

        'If rsLookup.Read Then
        '    TempData.OwnedbyCorporationName = rsLookup.GetString(0)
        'Else
        ' Need to look up from ESI
        TempData.OwnedbyCorporationName = ""
        'End If

        'rsLookup.Close()

        Return TempData

    End Function

    Private Structure StationData
        Dim StationID As Long
        Dim StationName As String
        Dim OwnedbyCorporationID As Long
        Dim OwnedbyCorporationName As String
    End Structure

#End Region

#Region "Supporting Functions"

    Private Function GetFacilityNameData(FacilityIDList As List(Of FacilityCorpIDPair), ByVal NameType As NameDataType) As List(Of NameData)
        Dim IDs As New List(Of Long)
        Dim NameList As New List(Of ESINameData)
        Dim TempNameList As List(Of ESINameData)
        Dim ReturnItem As New NameData
        Dim ReturnList As New List(Of NameData)
        Dim TempPair As FacilityCorpIDPair

        For Each Item In FacilityIDList
            If NameType = NameDataType.Owner Then
                If Not IDs.Contains(Item.OwnerID) Then
                    IDs.Add(Item.OwnerID)
                End If
            Else
                If Not IDs.Contains(Item.FacilityID) Then
                    IDs.Add(Item.FacilityID)
                End If
            End If
        Next

        If IDs.Count = 0 Then
            Return Nothing
        End If

        Dim i As Integer

        ' Only send 1000 ids at a time
        For i = 0 To CInt(IDs.Count / 1000)
            Dim Start As Integer = i * 1000
            Dim EndMark As Integer
            If Start + 1000 > IDs.Count Then
                EndMark = IDs.Count - Start
            Else
                EndMark = 1000
            End If
            Dim SendIDs As List(Of Long) = IDs.GetRange(Start, EndMark)
            TempNameList = GetNameData(SendIDs)
            ' Get names from ESI
            If Not IsNothing(TempNameList) Then
                NameList.AddRange(TempNameList)
            End If
        Next

        For Each item In NameList
            ' Find facilityid in main list to index from the ownerID
            IDToFind = item.id
            If NameType = NameDataType.Owner Then
                TempPair = FacilityIDList.Find(AddressOf FindOwner)
            Else
                TempPair = FacilityIDList.Find(AddressOf FindFacility)
            End If
            ' Set the data
            ReturnItem.IndexID = TempPair.FacilityID
            ReturnItem.ID = item.id
            ReturnItem.Name = item.name

            ReturnList.Add(ReturnItem)
        Next

        Return ReturnList

    End Function

    Private Enum NameDataType
        Owner = 0
        Facility = 1
    End Enum

    Private Structure NameData
        Dim IndexID As Long
        Dim ID As Long
        Dim Name As String
    End Structure

    Private Function GetErrorResponseBody(Ex As WebException) As String
        Dim resp As String = New StreamReader(Ex.Response.GetResponseStream()).ReadToEnd()
        Dim ErrorData As ESIError = JsonConvert.DeserializeObject(Of ESIError)(resp)

        Return ErrorData.ErrorText

    End Function

    Public Function GetFactionData() As List(Of ESIFactionData)
        Dim PublicData As String

        Try
            PublicData = GetPublicData(ESIPublicURL & "universe/factions/" & TranquilityDataSource, Nothing)

            Return JsonConvert.DeserializeObject(Of List(Of ESIFactionData))(PublicData)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ' Looks up the name for the list of IDs
    Public Function GetNameData(IDList As List(Of Long)) As List(Of ESINameData)
        Dim IDs As String = "["
        Dim PublicData As String

        For Each ID In IDList
            IDs &= CStr(ID) & ","
        Next

        IDs = IDs.Substring(0, Len(IDs) - 1) ' strip comma
        IDs &= "]"

        PublicData = GetPublicData(ESIPublicURL & "universe/names/" & TranquilityDataSource, Nothing, IDs)

        If Not IsNothing(PublicData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESINameData))(PublicData)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetRatePerSecond() As Integer
        Return ESIRatePerSecond
    End Function

    Public Function GetBurstSize() As Integer
        Return ESIBurstSize
    End Function

    Public Function GetMaximumConnections() As Integer
        Return ESIMaximumConnections
    End Function

    ' Implements limiting on the calls to ESI - Total records is the total you are going to update, call count is number already called, returns boolean if slept
    Public Function LimitESICalls(RequestStart As Date, TotalRecordCount As Integer, CallCount As Integer) As Boolean

        ' If we aren't as big as the burst size, then we don't need to limit our calls
        'If TotalRecordCount <= BurstSize Then
        '    Return False
        'End If

        ' Check to see if we hit the maximum calls per second, if so, check for time to sleep
        If CallCount >= ESIRatePerSecond Then
            ' Need to see if we are over the time limit and sleep
            ' Figure out the difference between the max time for max requests and our requests (in milliseconds)
            Dim Difference As Integer = CInt(1000 - (Now.Subtract(RequestStart).Milliseconds))
            If Difference > 0 Then
                Threading.Thread.Sleep(Difference)
            End If
            Return True
        Else
            Return False
        End If

    End Function

#End Region

End Class

#Region "JSON Classes"

Public Class ESIError
    <JsonProperty("error")> Public ErrorText As String
    <JsonProperty("sso_status")> Public sso_status As Integer
    <JsonProperty("timeout")> Public timeout As Integer
End Class

Public Class ESIMarketOrder
    <JsonProperty("order_id")> Public order_id As Long
    <JsonProperty("type_id")> Public type_id As Integer
    <JsonProperty("location_id")> Public location_id As Long
    <JsonProperty("system_id")> Public system_id As Integer
    <JsonProperty("volume_total")> Public volume_total As Integer
    <JsonProperty("volume_remain")> Public volume_remain As Integer
    <JsonProperty("min_volume")> Public min_volume As Integer
    <JsonProperty("price")> Public price As Double
    <JsonProperty("is_buy_order")> Public is_buy_order As Boolean
    <JsonProperty("duration")> Public duration As Integer
    <JsonProperty("issued")> Public issued As String ' date
    <JsonProperty("range")> Public range As String
End Class

Public Class ESIMarketAdjustedPrice
    <JsonProperty("adjusted_Price")> Public adjusted_Price As String ' String for easy processing
    <JsonProperty("average_Price")> Public average_Price As String ' String for easy processing
    <JsonProperty("type_id")> Public type_id As Integer
End Class

Public Class ESIMarketPriceItems
    <JsonProperty("date")> Public date_str As String
    <JsonProperty("volume")> Public volume As Long
    <JsonProperty("orderCount")> Public orderCount As Long
    <JsonProperty("highPrice")> Public highPrice As String ' Use string for EU processing
    <JsonProperty("avgPrice")> Public avgPrice As String ' Use string for EU processing
    <JsonProperty("lowPrice")> Public lowPrice As String ' Use string for EU processing
End Class

Public Class ESIMarketHistoryItem
    <JsonProperty("average")> Public average As Double
    <JsonProperty("date")> Public history_date As String
    <JsonProperty("highest")> Public highest As Double
    <JsonProperty("lowest")> Public lowest As Double
    <JsonProperty("order_count")> Public order_count As Long
    <JsonProperty("volume")> Public volume As Long
End Class

Public Class ESISystemCostIndices
    <JsonProperty("cost_indices")> Public cost_indices As List(Of ESIcostIndex)
    <JsonProperty("solar_system_id")> Public solar_system_id As Integer
End Class

Public Class ESIcostIndex
    <JsonProperty("activity")> Public activity As String
    <JsonProperty("cost_index")> Public cost_index As Double
End Class

Public Class ESIIndustryFacility
    <JsonProperty("facility_id")> Public facility_id As Long
    <JsonProperty("owner_id")> Public owner_id As Integer
    <JsonProperty("region_id")> Public region_id As Integer
    <JsonProperty("solar_system_id")> Public solar_system_id As Integer
    <JsonProperty("tax")> Public tax As Double
    <JsonProperty("type_id")> Public type_id As Integer
End Class

Public Class ESIStationData
    <JsonProperty("name")> Public name As String
    <JsonProperty("office_rental_cost")> Public office_rental_cost As Double
    <JsonProperty("owner")> Public owner As Integer
    <JsonProperty("position")> Public position As ESIPosition
    <JsonProperty("reprocessing_efficiency")> Public reprocessing_efficiency As Double
    <JsonProperty("reprocessing_stations_take")> Public reprocessing_stations_take As Double
    <JsonProperty("services")> Public services As List(Of String)
    <JsonProperty("station_id")> Public station_id As Integer
    <JsonProperty("system_id")> Public system_id As Integer
    <JsonProperty("type_id")> Public type_id As Integer
End Class

Public Class ESIPosition
    <JsonProperty("x")> Public x As Double
    <JsonProperty("y")> Public y As Double
    <JsonProperty("z")> Public z As Double
End Class

Public Class ESINameData
    <JsonProperty("category")> Public category As String '[ alliance, character, constellation, corporation, inventory_type, region, solar_system, station ]
    <JsonProperty("id")> Public id As Integer
    <JsonProperty("name")> Public name As String
End Class

Public Class ESIFactionData
    <JsonProperty("faction_id")> Public faction_id As Integer
    <JsonProperty("corporation_id")> Public corporation_id As Integer
    <JsonProperty("description")> Public description As String
    <JsonProperty("is_unique")> Public is_unique As Boolean
    <JsonProperty("militia_corporation_id")> Public militia_corporation_id As Integer
    <JsonProperty("name")> Public name As String
    <JsonProperty("size_factor")> Public size_factor As Double
    <JsonProperty("solar_system_id")> Public solar_system_id As Integer
    <JsonProperty("station_count")> Public station_count As Integer
    <JsonProperty("station_system_count")> Public station_system_count As Integer
End Class

#End Region

#Region "Character and Token Processing"

Public Class SavedTokenData
    Public CharacterID As Long
    Public AccessToken As String
    Public TokenType As String
    Public TokenExpiration As Date
    Public RefreshToken As String
    Public Scopes As String

    Public Sub New()
        CharacterID = 0
        AccessToken = ""
        TokenType = ""
        TokenExpiration = NoDate
        RefreshToken = ""
        Scopes = ""
    End Sub

End Class

Public Structure ESICharacterData
    Dim VerificationData As ESICharacterVerificationData
    Dim PublicData As ESICharacterPublicData
End Structure

Public Class ESITokenData
    <JsonProperty("access_token")> Public access_token As String
    <JsonProperty("token_type")> Public token_type As String
    <JsonProperty("expires_in")> Public expires_in As Integer ' in seconds
    <JsonProperty("refresh_token")> Public refresh_token As String
End Class

Public Class ESICharacterVerificationData
    <JsonProperty("CharacterID")> Public CharacterID As String
    <JsonProperty("CharacterName")> Public CharacterName As String
    <JsonProperty("ExpiresOn")> Public ExpiresOn As String
    <JsonProperty("Scopes")> Public Scopes As String
    <JsonProperty("TokenType")> Public TokenType As String
    <JsonProperty("CharacterOwnerHash")> Public CharacterOwnerHash As String
    <JsonProperty("IntellectualProperty")> Public IntellectualProperty As String
End Class

Public Class ESICharacterPublicData
    <JsonProperty("name")> Public name As String
    <JsonProperty("birthday")> Public birthday As String
    <JsonProperty("gender")> Public gender As String
    <JsonProperty("race_id")> Public race_id As Integer
    <JsonProperty("description")> Public description As String
    <JsonProperty("bloodline_id")> Public bloodline_id As Integer
    <JsonProperty("ancestry_id")> Public ancestry_id As Integer
    <JsonProperty("corporation_id")> Public corporation_id As Integer
    <JsonProperty("alliance_id")> Public alliance_id As Integer
    <JsonProperty("faction_id")> Public faction_id As Integer
    <JsonProperty("security_status")> Public security_status As Double
End Class

#End Region

#Region "Character Data Objects"

Public Class ESICharacterStandingsData
    <JsonProperty("from_id")> Public from_id As Long
    <JsonProperty("from_type")> Public from_type As String
    <JsonProperty("standing")> Public standing As Double
End Class

Public Class ESICharacterSkillsBase
    <JsonProperty("skills")> Public skills() As ESICharacterSkillsData
    <JsonProperty("total_sp")> Public total_sp As Integer
    <JsonProperty("unallocated_sp")> Public unallocated_sp As Integer
End Class

Public Class ESICharacterSkillsData
    <JsonProperty("skill_id")> Public skill_id As Integer
    <JsonProperty("skillpoints_in_skill")> Public skillpoints_in_skill As Integer
    <JsonProperty("trained_skill_level")> Public trained_skill_level As Integer
    <JsonProperty("active_skill_level")> Public active_skill_level As Integer
End Class

Public Class ESIResearchAgent
    <JsonProperty("agent_id")> Public agent_id As Long
    <JsonProperty("skill_type_id")> Public skill_type_id As Integer
    <JsonProperty("started_at")> Public started_at As String
    <JsonProperty("points_per_day")> Public points_per_day As Double
    <JsonProperty("remainder_points")> Public remainder_points As Double
End Class

Public Class ESICorporationRoles
    <JsonProperty("character_id")> Public character_id As Long
    <JsonProperty("grantable_roles")> Public grantable_roles As List(Of String)
    <JsonProperty("grantable_roles_at_base")> Public grantable_roles_at_base As List(Of String)
    <JsonProperty("grantable_roles_at_hq")> Public grantable_roles_at_hq As List(Of String)
    <JsonProperty("grantable_roles_at_other")> Public grantable_roles_at_other As List(Of String)
    <JsonProperty("roles")> Public roles As List(Of String)
    <JsonProperty("roles_at_base")> Public roles_at_base As List(Of String)
    <JsonProperty("roles_at_hq")> Public roles_at_hq As List(Of String)
    <JsonProperty("roles_at_other")> Public roles_at_other As List(Of String)
End Class

Public Class ESIBlueprint
    <JsonProperty("item_id")> Public item_id As Long
    <JsonProperty("type_id")> Public type_id As Integer
    <JsonProperty("location_id")> Public location_id As Long
    <JsonProperty("location_flag")> Public location_flag As String
    <JsonProperty("quantity")> Public quantity As Integer
    <JsonProperty("time_efficiency")> Public time_efficiency As Integer
    <JsonProperty("material_efficiency")> Public material_efficiency As Integer
    <JsonProperty("runs")> Public runs As Integer
End Class

Public Class ESIIndustryJob
    <JsonProperty("job_id")> Public job_id As Integer
    <JsonProperty("installer_id")> Public installer_id As Integer
    <JsonProperty("facility_id")> Public facility_id As Long
    <JsonProperty("station_id")> Public station_id As Long ' Add both but use location id in end
    <JsonProperty("location_id")> Public location_id As Long ' Add both but use location ID in the end
    <JsonProperty("activity_id")> Public activity_id As Integer
    <JsonProperty("blueprint_id")> Public blueprint_id As Long
    <JsonProperty("blueprint_type_id")> Public blueprint_type_id As Integer
    <JsonProperty("blueprint_location_id")> Public blueprint_location_id As Long
    <JsonProperty("output_location_id")> Public output_location_id As Long
    <JsonProperty("runs")> Public runs As Long
    <JsonProperty("cost")> Public cost As Double
    <JsonProperty("licensed_runs")> Public licensed_runs As Integer
    <JsonProperty("probability")> Public probability As Double
    <JsonProperty("product_type_id")> Public product_type_id As Integer
    <JsonProperty("status")> Public status As String ' [ active, cancelled, delivered, paused, ready, reverted ]
    <JsonProperty("duration")> Public duration As Integer
    <JsonProperty("start_date")> Public start_date As String
    <JsonProperty("end_date")> Public end_date As String
    <JsonProperty("pause_date")> Public pause_date As String
    <JsonProperty("completed_date")> Public completed_date As String
    <JsonProperty("completed_character_id")> Public completed_character_id As Integer
    <JsonProperty("successful_runs")> Public successful_runs As Integer
End Class

Public Class ESIAsset
    <JsonProperty("location_flag")> Public location_flag As String
    <JsonProperty("location_id")> Public location_id As Long
    <JsonProperty("is_singleton")> Public is_singleton As Boolean
    <JsonProperty("type_id")> Public type_id As Long
    <JsonProperty("item_id")> Public item_id As Double
    <JsonProperty("location_type")> Public location_type As String
    <JsonProperty("quantity")> Public quantity As Integer
End Class

Public Class ESICorporation
    <JsonProperty("alliance_id")> Public alliance_id As Integer
    <JsonProperty("ceo_id")> Public ceo_id As Integer
    <JsonProperty("creator_id")> Public creator_id As Integer
    <JsonProperty("date_founded")> Public date_founded As String
    <JsonProperty("description")> Public description As String
    <JsonProperty("faction_id")> Public faction_id As Integer
    <JsonProperty("home_station_id")> Public home_station_id As Integer
    <JsonProperty("member_count")> Public member_count As Integer
    <JsonProperty("name")> Public name As String
    <JsonProperty("shares")> Public shares As Long
    <JsonProperty("tax_rate")> Public tax_rate As Double
    <JsonProperty("ticker")> Public ticker As String
    <JsonProperty("url")> Public url As String
End Class

#End Region

' For outputing a list of stations
Public Structure Station
    Dim stationID As Long
    Dim stationName As String
    Dim stationTypeID As Long
    Dim solarSystemID As Long
    Dim regionID As Long
    Dim corporationID As Long
    Dim corporationName As String
    Dim tax As Double
End Structure
