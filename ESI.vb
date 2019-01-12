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
    Private Const ESIURL As String = "https://esi.evetech.net/latest/"
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

    Public Const ESIUniverseStructuresScope As String = "esi-universe.read_structures"
    Public Const ESIStructureMarketsScope As String = "esi-markets.structure_markets"

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

    ' Keeps an array of threads if we need to abort update
    Private ThreadsArray As List(Of Thread) = New List(Of Thread)
    Private StructureCount As Integer ' for counting order updates

    Public ErrorData As ESIErrorData

    Public Structure ESIErrorData
        Dim ErrorCode As Integer
        Dim ErrorText As String
        Dim ErrorDescription As String
    End Structure

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
    '
    ' esi-universe.read_structure.v1: Allows reading of all public structures in the universe
    ' esi-markets.structure_markets.v1: Allows reading of markets for structures the character can use

    ''' <summary>
    ''' Initialize the class and set the implemented scopes
    ''' </summary>
    Public Sub New()

        ' Load all the details from the authorization information file
        Dim ApplicationSettings As AppRegistrationInformationSettings = AllSettings.LoadAppRegistrationInformationSettings
        With ApplicationSettings
            ClientID = .ClientID
            SecretKey = .SecretKey
            ' Make sure we split out all the double spaces, etc from the scopes string.
            ' Process the scopes and only leave one space between each
            ScopesString = String.Join(" ", .Scopes.Split(New String() {" ", ",", "%20", "%2520", vbCr, vbLf, vbCrLf}, StringSplitOptions.RemoveEmptyEntries))
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

                AuthStreamText = "" ' Reset before call
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

                ' Get the authtoken from the stream
                Return GetAuthToken(AuthStreamText)

            End If
        Catch ex As WebException
            ErrorData = New ESIErrorData
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            Dim TempErrorData As New ESIError
            ErrorResponse = GetErrorResponseBody(ex, TempErrorData)

            ' Save this for use in calling routine
            ErrorData.ErrorCode = ErrorCode
            ErrorData.ErrorDescription = TempErrorData.ErrorDescription
            ErrorData.ErrorText = TempErrorData.ErrorText

            If ErrorCode >= 500 And Not RetriedCall Then
                ' Try this call again after waiting a few
                Threading.Thread.Sleep(2000)
                RetriedCall = True
                Call GetAuthorizationToken(0)
            End If
            MsgBox("Web Request failed to get Authorization Token." & vbCrLf & vbCrLf & "Error Code: " & ErrorCode & vbCrLf & "Message: " & ex.Message & vbCrLf & "Description: " & ErrorResponse, vbInformation, Application.ProductName)
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
            Dim URL As String = GetURL

            Process.Start(URL)

            Dim mySocket As Socket = Nothing
            Dim myStream As NetworkStream = Nothing
            Dim myReader As StreamReader = Nothing
            Dim myWriter As StreamWriter = Nothing

            myListner.Start()

            mySocket = myListner.AcceptSocket() ' Wait for response
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
            'Throw New Exception("Test Error - Get Access Token")

            Response = WC.UploadValues(ESITokenURL, "POST", PostParameters)

            ' Convert byte data to string
            Data = Encoding.UTF8.GetString(Response)

            ' Parse the data to the class
            AccessTokenOutput = JsonConvert.DeserializeObject(Of ESITokenData)(Data)
            Success = True

        Catch ex As WebException
            ErrorData = New ESIErrorData
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            Dim TempErrorData As New ESIError
            ErrorResponse = GetErrorResponseBody(ex, TempErrorData)

            ' Save this for use in calling routine
            ErrorData.ErrorCode = ErrorCode
            ErrorData.ErrorDescription = TempErrorData.ErrorDescription
            ErrorData.ErrorText = TempErrorData.ErrorText

            If ErrorCode >= 500 And Not RetriedCall Then
                ' Try this call again after waiting a few
                RetriedCall = True
                Thread.Sleep(2000)
                Call GetAccessToken(Token, Refresh, 0)
            End If

            MsgBox("Web Request failed to get Access Token." & vbCrLf & vbCrLf & "Error Code: " & ErrorCode & vbCrLf & "Message: " & ex.Message & vbCrLf & "Description: " & ErrorResponse, vbInformation, Application.ProductName)


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
        Dim RetryCount As Integer = 0

        Dim myWebHeaderCollection As WebHeaderCollection
        Dim Expires As String = Nothing
        Dim Pages As Integer = Nothing
        Dim ESIErrorLimitReset As Integer = 0
        Dim ESIErrorLimitRemain As Integer = 0

        Try

            Do ' TODO - error limit 420s https://developers.eveonline.com/blog/article/error-limiting-imminent

                WC.Proxy = GetProxyData()

                If BodyData <> "" Then
                    Response = Encoding.UTF8.GetString(WC.UploadData(URL, Encoding.UTF8.GetBytes(BodyData)))
                Else
                    Response = WC.DownloadString(URL)
                End If

                'Throw New Exception("Test Error - Get Public Data")

                ' Get the expiration date for the cache date
                myWebHeaderCollection = WC.ResponseHeaders
                Expires = myWebHeaderCollection.Item("Expires")
                Pages = CInt(myWebHeaderCollection.Item("X-Pages"))

                ESIErrorLimitRemain = CInt(myWebHeaderCollection.Item("X-ESI-Error-Limit-Remain")) ' how many errors responses will be returned to you in the current error window
                ESIErrorLimitReset = CInt(myWebHeaderCollection.Item("X-ESI-Error-Limit-Reset")) ' indicates the number of seconds until the end of the current error window

                ' If we are at max errors, wait until the next window and try again
                If ESIErrorLimitRemain = 0 Then
                    ' Wait until the window closes
                    Thread.Sleep(ESIErrorLimitReset * 1000)
                    RetryCount += 1
                End If

            Loop While ESIErrorLimitRemain = 0 And RetryCount < 5

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
            ErrorData = New ESIErrorData
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            Dim TempErrorData As New ESIError
            ErrorResponse = GetErrorResponseBody(ex, TempErrorData)

            ' Save this for use in calling routine
            ErrorData.ErrorCode = ErrorCode
            ErrorData.ErrorDescription = TempErrorData.ErrorDescription
            ErrorData.ErrorText = TempErrorData.ErrorText

            If ErrorCode >= 500 And Not RetriedCall Then
                RetriedCall = True
                ' Try this call again after waiting a few
                Threading.Thread.Sleep(2000)
                Return GetPublicData(URL, CacheDate, BodyData)
            End If
            MsgBox("Web Request failed to get Public data." & vbCrLf & vbCrLf & "Error Code: " & ErrorCode & vbCrLf & "Message: " & ex.Message & vbCrLf & "Description: " & ErrorResponse, vbInformation, Application.ProductName)
        Catch ex As Exception
            If ex.HResult <> -2146233040 Then ' This HR result is for thread aborts. Test out for awhile to see how it works
                MsgBox("The request failed to get Public data. " & ex.Message, vbInformation, Application.ProductName)
            End If
        End Try

        RetriedCall = False

        If Not IsNothing(Response) Then
            If Response <> "" Then
                Return Response
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Queries the server for private, authorized data for data sent. Function will check the 
    ''' authorization token and update the sent variable and DB data if expired.
    ''' </summary>
    ''' <returns>Returns data response as a string</returns>
    Private Function GetPrivateAuthorizedData(ByVal URL As String, ByVal TokenData As ESITokenData,
                                              ByVal TokenExpiration As Date, ByRef CacheDate As Date,
                                              ByVal CharacterID As Long, Optional ByVal SupressErrorMsgs As Boolean = False) As String
        Dim WC As New WebClient
        Dim ErrorCode As Integer = 0
        Dim ErrorResponse As String = ""
        Dim Response As String = ""


        Try
            ' See if we update the token data first
            If TokenExpiration <= DateTime.UtcNow Then

                ' Update the token
                TokenData = GetAccessToken(TokenData.refresh_token, True, ErrorCode)

                If IsNothing(TokenData) Then
                    Return Nothing
                End If

                ' Update the token data in the DB for this character
                Dim SQL As String = ""
                ' Update data - only stuff that could (reasonably) change
                SQL = "UPDATE ESI_CHARACTER_DATA SET ACCESS_TOKEN = '{0}', ACCESS_TOKEN_EXPIRE_DATE_TIME = '{1}', "
                SQL &= "TOKEN_TYPE = '{2}', REFRESH_TOKEN = '{3}' WHERE CHARACTER_ID = {4}"

                With TokenData
                    SQL = String.Format(SQL, FormatDBString(.access_token),
                            Format(DateAdd(DateInterval.Second, .expires_in, DateTime.UtcNow), SQLiteDateFormat),
                            FormatDBString(.token_type), FormatDBString(.refresh_token), CharacterID)
                End With

                EVEDB.ExecuteNonQuerySQL(SQL)

                ' Now update the copy used in IPH so we don't re-query
                SelectedCharacter.CharacterTokenData.AccessToken = TokenData.access_token
                SelectedCharacter.CharacterTokenData.RefreshToken = TokenData.refresh_token
                SelectedCharacter.CharacterTokenData.TokenExpiration = DateAdd(DateInterval.Second, TokenData.expires_in, DateTime.UtcNow)

            End If

            If ErrorCode = 0 Then

                ' TODO - error limit 420s https://developers.eveonline.com/blog/article/error-limiting-imminent


                Dim Auth_header As String = $"Bearer {TokenData.access_token}"

                WC.Proxy = GetProxyData()
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
            End If

        Catch ex As WebException
            ErrorData = New ESIErrorData
            ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
            Dim TempErrorData As New ESIError
            ErrorResponse = GetErrorResponseBody(ex, TempErrorData)

            ' Save this for use in calling routine
            ErrorData.ErrorCode = ErrorCode
            ErrorData.ErrorDescription = TempErrorData.ErrorDescription
            ErrorData.ErrorText = TempErrorData.ErrorText

            If ErrorResponse = "Character not in corporation" Or ErrorResponse = "Character cannot grant roles" Then
                ' Assume this error came from checking on NPC corp roles or a character that doesn't have any roles and just exit with nothing
                Exit Try
            End If

            If ErrorCode >= 500 And Not RetriedCall Then
                RetriedCall = True
                ' Try this call again after waiting a few
                Threading.Thread.Sleep(2000)
                Return GetPrivateAuthorizedData(URL, TokenData, TokenExpiration, CacheDate, CharacterID, SupressErrorMsgs)
            End If
            If Not SupressErrorMsgs Then
                MsgBox("Web Request failed to get Authorized data." & vbCrLf & vbCrLf & "Error Code: " & ErrorCode & vbCrLf & "Message: " & ex.Message & vbCrLf & "Description: " & ErrorResponse, vbInformation, Application.ProductName)
            End If
        Catch ex As Exception
            If Not SupressErrorMsgs Then
                MsgBox("The request failed to get Authorized data. " & ex.Message, vbInformation, Application.ProductName)
            End If
        End Try

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
    Public Function SetCharacterData(Optional ByRef CharacterTokenData As SavedTokenData = Nothing,
                                     Optional ByVal ManualAuthToken As String = "") As Boolean
        Dim TokenData As ESITokenData
        Dim CharacterData As New ESICharacterData
        Dim CharacterID As Long
        Dim ErrorCode As Integer = 0

        Dim CB As New CacheBox
        Dim CacheDate As Date

        If Not IsNothing(CharacterTokenData) Then
            CharacterID = CharacterTokenData.CharacterID
        Else
            CharacterID = 0
        End If

        Try
            If CB.DataUpdateable(CacheDateType.PublicCharacterData, CharacterID) Then
                If CharacterID = 0 Then
                    ' We need to get the token data from the authorization
                    If ManualAuthToken <> "" Then
                        TokenData = GetAccessToken(ManualAuthToken, False, ErrorCode)
                    Else
                        TokenData = GetAccessToken(GetAuthorizationToken(ErrorCode), False, ErrorCode)
                    End If

                    CharacterTokenData = New SavedTokenData
                Else
                    ' We need to refresh the token data
                    TokenData = GetAccessToken(CharacterTokenData.RefreshToken, True, ErrorCode)
                End If

                If IsNothing(TokenData) Then
                    Return False
                End If

                If ErrorCode = 0 And Not IsNothing(TokenData) Then
                    ' Update the local copy with the new information
                    CharacterTokenData.AccessToken = TokenData.access_token
                    CharacterTokenData.RefreshToken = TokenData.refresh_token
                    CharacterTokenData.TokenType = TokenData.token_type

                    ' Set the expiration date to pass
                    CharacterTokenData.TokenExpiration = DateAdd(DateInterval.Second, TokenData.expires_in, DateTime.UtcNow)

                    ' Now with the token data, look up the character data
                    CharacterData.VerificationData = GetCharacterVerificationData(TokenData, CharacterTokenData.TokenExpiration)
                    If IsNothing(CharacterData.VerificationData) Then
                        Exit Try
                    End If
                    CharacterData.PublicData = GetCharacterPublicData(CharacterData.VerificationData.CharacterID, CacheDate)
                    If IsNothing(CharacterData.PublicData) Then
                        Exit Try
                    End If

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
                    If ErrorCode <> 400 And Not CancelESISSOLogin Then
                        MsgBox("Unable to load the selected character to IPH", vbExclamation, Application.ProductName)
                    End If
                End If
            Else
                ' Just didn't need to update yet
                Return True
            End If
        Catch ex As Exception
            MsgBox("Unable to set character data data through ESI: " & ex.Message, vbInformation, Application.ProductName)
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

        PublicData = GetPublicData(ESIURL & "characters/" & CStr(CharacterID) & "/" & TranquilityDataSource, DataCacheDate)

        If Not IsNothing(PublicData) Then
            CharacterData = JsonConvert.DeserializeObject(Of ESICharacterPublicData)(PublicData)
            Return CharacterData
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Gets the character verification data when sent the refresh token
    ''' </summary>
    ''' <param name="TokenData"></param>
    ''' <param name="TokenExpirationDate"></param>
    ''' <returns>Character Verification Data object</returns>
    Public Function GetCharacterVerificationData(ByVal TokenData As ESITokenData, ByVal TokenExpirationDate As Date) As ESICharacterVerificationData
        Dim CacheDate As Date
        Dim WC As New WebClient
        Dim ErrorCode As Integer = 0
        Dim ErrorResponse As String = ""
        Dim Response As String = ""

        WC.Proxy = GetProxyData()

        ' See if we update the token data first
        If TokenExpirationDate <= DateTime.UtcNow Then
            ' Update the token
            TokenData = GetAccessToken(TokenData.refresh_token, True, ErrorCode)
        End If

        If IsNothing(TokenData) Then
            Return Nothing
        End If

        If ErrorCode = 0 Then
            Try
                Dim Auth_header As String = $"Bearer {TokenData.access_token}"

                WC.Headers(HttpRequestHeader.Authorization) = Auth_header
                Response = WC.DownloadString(ESIVerifyURL)

                'Throw New Exception("Test Error - Get Character Verification Data")

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
                ErrorData = New ESIErrorData
                ErrorCode = CType(ex.Response, HttpWebResponse).StatusCode
                Dim TempErrorData As New ESIError
                ErrorResponse = GetErrorResponseBody(ex, TempErrorData)

                ' Save this for use in calling routine
                ErrorData.ErrorCode = ErrorCode
                ErrorData.ErrorDescription = TempErrorData.ErrorDescription
                ErrorData.ErrorText = TempErrorData.ErrorText

                If ErrorCode >= 500 And Not RetriedCall Then
                    RetriedCall = True
                    ' Try this call again after waiting a few
                    Thread.Sleep(2000)
                    Return GetCharacterVerificationData(TokenData, TokenExpirationDate)
                End If
                MsgBox("Web Request failed to get Authorized data." & vbCrLf & vbCrLf & "Error Code: " & ErrorCode & vbCrLf & "Message: " & ex.Message & vbCrLf & "Description: " & ErrorResponse, vbInformation, Application.ProductName)
            Catch ex As Exception
                MsgBox("The request failed to get Authorized data. " & ex.Message, vbInformation, Application.ProductName)
            End Try
        End If

        RetriedCall = False

        Return Nothing

    End Function

#End Region

#Region "Auth Processing"

    Public Function GetCharacterSkills(ByVal CharacterID As Long, ByVal TokenData As SavedTokenData, ByRef SkillsCacheDate As Date) As EVESkillList
        Dim SkillData As New ESICharacterSkillsBase
        Dim ReturnData As String
        Dim ReturnSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)
        Dim TempSkill As New EVESkill

        ReturnData = GetPrivateAuthorizedData(ESIURL & "characters/" & CStr(CharacterID) & "/skills/" & TranquilityDataSource,
                                              FormatTokenData(TokenData), TokenData.TokenExpiration, SkillsCacheDate, CharacterID)

        If Not IsNothing(ReturnData) Then
            SkillData = JsonConvert.DeserializeObject(Of ESICharacterSkillsBase)(ReturnData)

            For Each entry In SkillData.skills
                TempSkill = New EVESkill
                TempSkill.TypeID = entry.skill_id
                TempSkill.TrainedLevel = entry.trained_skill_level
                TempSkill.ActiveLevel = entry.active_skill_level
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

        ReturnData = GetPrivateAuthorizedData(ESIURL & "characters/" & CStr(CharacterID) & "/standings/" & TranquilityDataSource,
                                              FormatTokenData(TokenData), TokenData.TokenExpiration, StandingsCacheDate, CharacterID)

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

        ReturnData = GetPrivateAuthorizedData(ESIURL & "characters/" & CStr(CharacterID) & "/agents_research/" & TranquilityDataSource,
                                              FormatTokenData(TokenData), TokenData.TokenExpiration, AgentsCacheDate, CharacterID)

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
        Dim rsLookup As SQLiteDataReader

        ' Set up query string
        If ScanType = ScanType.Personal Then
            ReturnData = GetPrivateAuthorizedData(ESIURL & "characters/" & CStr(ID) & "/blueprints/" & TranquilityDataSource,
                                                  FormatTokenData(TokenData), TokenData.TokenExpiration, BPCacheDate, ID)
        Else ' Corp
            ReturnData = GetPrivateAuthorizedData(ESIURL & "corporations/" & CStr(ID) & "/blueprints/" & TranquilityDataSource,
                                                  FormatTokenData(TokenData), TokenData.TokenExpiration, BPCacheDate, ID)
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
                DBCommand = New SQLiteCommand("SELECT flagID FROM INVENTORY_FLAGS WHERE flagName = '" & BP.location_flag & "'", EVEDB.DBREf)
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

        ' Set up query string
        If JobType = ScanType.Personal Then
            ReturnData = GetPrivateAuthorizedData(ESIURL & "characters/" & CStr(ID) & "/industry/jobs/" & TranquilityDataSource & "&include_completed=true",
                                                  FormatTokenData(TokenData), TokenData.TokenExpiration, JobsCacheDate, ID)
        Else ' Corp
            ReturnData = GetPrivateAuthorizedData(ESIURL & "corporations/" & CStr(ID) & "/industry/jobs/" & TranquilityDataSource,
                                                  FormatTokenData(TokenData), TokenData.TokenExpiration, JobsCacheDate, ID)
        End If

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESIIndustryJob))(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetAssets(ByVal ID As Long, ByVal TokenData As SavedTokenData, ByVal JobType As ScanType, ByRef AssetsCacheDate As Date) As List(Of ESIAsset)
        Dim ReturnData As String = ""

        ' Set up query string
        If JobType = ScanType.Personal Then
            ReturnData = GetPrivateAuthorizedData(ESIURL & "characters/" & CStr(ID) & "/assets/" & TranquilityDataSource,
                                                  FormatTokenData(TokenData), TokenData.TokenExpiration, AssetsCacheDate, ID)
        Else ' Corp
            ReturnData = GetPrivateAuthorizedData(ESIURL & "corporations/" & CStr(ID) & "/assets/" & TranquilityDataSource,
                                                  FormatTokenData(TokenData), TokenData.TokenExpiration, AssetsCacheDate, ID)
        End If

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of List(Of ESIAsset))(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetCorporationRoles(ByVal CharacterID As Long, ByVal CorporationID As Long, ByVal TokenData As SavedTokenData, ByRef RolesCacheDate As Date) As List(Of ESICorporationRoles)
        Dim ReturnData As String

        ReturnData = GetPrivateAuthorizedData(ESIURL & "corporations/" & CStr(CorporationID) & "/roles/" & TranquilityDataSource, FormatTokenData(TokenData), TokenData.TokenExpiration, RolesCacheDate, CharacterID)

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
        ReturnData = GetPublicData(ESIURL & "corporations/" & CStr(ID) & TranquilityDataSource, DataCacheDate)

        If Not IsNothing(ReturnData) Then
            CorpData = JsonConvert.DeserializeObject(Of ESICorporation)(ReturnData)

            If Not IsNothing(CorpData) Then

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

                DBCommand = Nothing

            End If
        End If

    End Sub

    Public Function GetStructureData(ByVal ID As Long, ByVal TokenData As SavedTokenData, ByRef StructureCacheDate As Date) As ESIUniverseStructure
        Dim ReturnData As String = ""

        ' Set up query string - suppress error messages since this will probably have the most issues
        ReturnData = GetPrivateAuthorizedData(ESIURL & "universe/structures/" & CStr(ID) & "/" & TranquilityDataSource,
                                              FormatTokenData(TokenData), TokenData.TokenExpiration, StructureCacheDate, TokenData.CharacterID, True)

        If Not IsNothing(ReturnData) Then
            Return JsonConvert.DeserializeObject(Of ESIUniverseStructure)(ReturnData)
        Else
            Return Nothing
        End If

    End Function

    ' Updates the db with orders from structures for the items and region sent
    Public Function UpdateStructureMarketOrders(QueryRegionID As String, ByVal Tokendata As SavedTokenData, ByRef refPG As ToolStripProgressBar) As Boolean
        Dim SQL As String
        Dim rsCheck As SQLiteDataReader
        Dim CacheDate As Date = NoDate
        Dim PublicData As String = ""

        Dim StructureIDs As New List(Of Long)

        ' First, get all the structures in that region
        SQL = "SELECT STATION_ID FROM STATIONS WHERE REGION_ID = " & CStr(QueryRegionID) & " "
        SQL &= "AND STATION_TYPE_ID IN "
        SQL &= "(SELECT TYPEID FROM INVENTORY_TYPES AS IT, INVENTORY_GROUPS AS IG, INVENTORY_CATEGORIES AS IC "
        SQL &= "WHERE IT.groupID = IG.groupID AND IG.categoryID = IC.categoryID AND IG.categoryID = 65 AND IT.published <> 0) "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCheck = DBCommand.ExecuteReader

        While rsCheck.Read
            StructureIDs.Add(rsCheck.GetInt64(0))
        End While

        rsCheck.Close()

        If StructureIDs.Count > 0 Then
            ' For processing
            Dim ThreadPairs As New List(Of StructureDataQueryInfo)
            Dim Pair As New StructureDataQueryInfo
            Dim PairMarker As Integer = 0

            ' Load all the query data
            For Each SID In StructureIDs
                Pair.StructureID = SID
                Pair.TokenData = Tokendata
                Pair.SupressMessages = True
                Pair.ProgressBarRef = Nothing
                ThreadPairs.Add(Pair)
            Next

            EVEDB.BeginSQLiteTransaction()

            ThreadsArray = New List(Of Thread)

            Application.DoEvents()

            ' Call this manually if it's just one item to update
            If ThreadPairs.Count = 1 Then
                ' Set the refPG so it's updated inside the single run of the structure orders
                Dim TempPair As New StructureDataQueryInfo
                TempPair = ThreadPairs(0)
                TempPair.ProgressBarRef = refPG
                ThreadPairs = New List(Of StructureDataQueryInfo)
                ThreadPairs.Add(TempPair)
                Call LoadStructureMarketOrders(ThreadPairs(0))
            Else
                ' Reset the value of the progress bar for counting structures
                If Not IsNothing(refPG) Then
                    refPG.Visible = True
                    refPG.Value = 0
                    StructureCount = 0
                    refPG.Maximum = StructureIDs.Count
                End If

                ' Call each thread for the pairs
                For i = 0 To ThreadPairs.Count - 1
                    Dim UPHThread As New Thread(AddressOf LoadStructureMarketOrders)
                    UPHThread.Start(ThreadPairs(i))
                    ' Save the thread if we need to kill it
                    ThreadsArray.Add(UPHThread)
                Next

                Dim Stillworking As Boolean = True
                Dim PrevCount As Integer = 0
                Dim StartTime As DateTime = Now

                Do Until Not Stillworking
                    ' Now loop until all the threads are done
                    For Each T In ThreadsArray
                        If T.ThreadState = ThreadState.Running Then
                            ' Still working on at least 1 thread, so exit
                            Stillworking = True
                            Exit For
                        Else
                            Stillworking = False
                        End If
                    Next
                    ' Update the progress bar with current count every time we check (only if we finished at least one run)
                    If StructureCount > PrevCount Then
                        Call IncrementToolStripProgressBar(StructureCount, refPG)
                    End If
                    PrevCount = StructureCount
                    Application.DoEvents()

                    ' Check if we need to leave - cancel pressed or 2 minutes passed
                    If CancelUpdatePrices Or (StartTime <> NoDate And DateDiff(DateInterval.Second, StartTime, Now) >= 120) Then
                        Call KillThreads()
                        If CancelUpdatePrices Then
                            Return True ' They wanted this so don't error
                        Else
                            Return False
                        End If
                    End If
                Loop

                ' Make sure all threads are not running
                Call KillThreads()

            End If

            EVEDB.CommitSQLiteTransaction()
        End If

        Return True

    End Function

    Public Structure StructureDataQueryInfo
        Dim StructureID As Long
        Dim TokenData As SavedTokenData
        Dim ProgressBarRef As ToolStripProgressBar
        Dim SupressMessages As Boolean
    End Structure

    ' Updates the class referenced toolbar 
    Private Sub IncrementToolStripProgressBar(inValue As Integer, ByRef PG As ToolStripProgressBar)

        If IsNothing(PG) Then
            Exit Sub
        End If

        ' Updates the value in the progressbar for a smooth progress (slows procesing a little)
        If inValue <= PG.Maximum - 1 And inValue <> 0 Then
            PG.Value = inValue
            PG.Value = inValue - 1
            PG.Value = inValue
        Else
            PG.Value = inValue
        End If

    End Sub

    ' Loads the sent structureIDs market orders - for use with threading call
    Public Function LoadStructureMarketOrders(SetQueryInfo As Object) As Boolean
        Dim MarketOrdersOutput As List(Of ESIMarketOrder)
        Dim SQL As String
        Dim rsCache As SQLiteDataReader
        Dim CacheDate As Date = NoDate
        Dim PriceData As String = ""

        Dim QueryInfo As StructureDataQueryInfo = CType(SetQueryInfo, StructureDataQueryInfo)

        ' First look up the cache date to see if it's time to run the update for the structure
        SQL = "SELECT CACHE_DATE FROM STRUCTURE_MARKET_ORDERS_UPDATE_CACHE WHERE STRUCTURE_ID = " & CStr(QueryInfo.StructureID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCache = DBCommand.ExecuteReader

        CacheDate = ProcessCacheDate(rsCache)

        rsCache.Close()
        rsCache = Nothing
        DBCommand = Nothing

        ' For each structure, update the total record count for the progressbar on frmMain regardless of whether we do anything with it
        StructureCount += 1

        ' If it's later than now, update
        If CacheDate <= Date.UtcNow Then
            With QueryInfo
                ' Delete any records for this structure since we have a fresh set to load
                Call EVEDB.ExecuteNonQuerySQL("DELETE FROM STRUCTURE_MARKET_ORDERS WHERE LOCATION_ID = " & CStr(.StructureID))

                ' Get the data from ESI 
                PriceData = GetPrivateAuthorizedData(ESIURL & "markets/structures/" & CStr(.StructureID) & "/" & TranquilityDataSource,
                                                FormatTokenData(.TokenData), .TokenData.TokenExpiration, CacheDate, .TokenData.CharacterID, QueryInfo.SupressMessages)

                If Not IsNothing(PriceData) Then
                    MarketOrdersOutput = JsonConvert.DeserializeObject(Of List(Of ESIMarketOrder))(PriceData)

                    ' Parse the data
                    If MarketOrdersOutput.Count > 0 Then
                        Application.DoEvents()

                        Dim StructureLocation As SystemRegion

                        ' Get the structure information first
                        StructureLocation = GetStationStructureLocation(.StructureID, QueryInfo.TokenData, StationStructureIDType._Structure)

                        If Not IsNothing(QueryInfo.ProgressBarRef) Then
                            QueryInfo.ProgressBarRef.Value = 0
                            QueryInfo.ProgressBarRef.Maximum = MarketOrdersOutput.Count - 1
                            QueryInfo.ProgressBarRef.Visible = True
                        End If

                        ' Now read through all the output items that are not in the table insert them in MARKET_ORDERS
                        For i = 0 To MarketOrdersOutput.Count - 1
                            With MarketOrdersOutput(i)

                                Dim OrderDownloadType As String = ""

                                Dim IssueDate As Date = FormatESIDate(.issued)

                                ' Insert all the new records
                                SQL = "INSERT INTO STRUCTURE_MARKET_ORDERS VALUES (" & CStr(.order_id) & "," & CStr(.type_id) & ","
                                SQL &= .location_id & "," & CStr(StructureLocation.RegionID) & "," & CStr(StructureLocation.SystemID) & ",'"
                                SQL &= CStr(IssueDate) & "'," & .duration & "," & CStr(CInt(.is_buy_order)) & "," & .price & "," & .volume_total & ","
                                SQL &= .min_volume & "," & .volume_remain & ",'" & .range & "')"
                                Call EVEDB.ExecuteNonQuerySQL(SQL)

                            End With
                            If Not IsNothing(QueryInfo.ProgressBarRef) Then
                                QueryInfo.ProgressBarRef.Value = i
                            End If
                            Application.DoEvents()
                        Next

                    End If
                Else
                    Return False
                End If

                ' Set the Cache Date for everything queried 
                Call EVEDB.ExecuteNonQuerySQL("DELETE FROM STRUCTURE_MARKET_ORDERS_UPDATE_CACHE WHERE STRUCTURE_ID = " & CStr(.StructureID))
                Call EVEDB.ExecuteNonQuerySQL("INSERT INTO STRUCTURE_MARKET_ORDERS_UPDATE_CACHE VALUES (" & CStr(.StructureID) & ",'" & Format(CacheDate, SQLiteDateFormat) & "')")

                rsCache = Nothing
                DBCommand = Nothing

                Return True

            End With
        Else
            Return False
        End If

        Return True

    End Function

    ' Just tries to download market prices - if it gets prices, then returns true else false
    Public Function CheckStructureMarketData(StructureID As Long, TokenData As SavedTokenData, SupressErrors As Boolean) As Boolean
        Dim PriceData As String = ""

        PriceData = GetPrivateAuthorizedData(ESIURL & "markets/structures/" & CStr(StructureID) & "/" & TranquilityDataSource,
                                                     FormatTokenData(TokenData), TokenData.TokenExpiration, Nothing, SelectedCharacter.ID, SupressErrors)

        If Not IsNothing(PriceData) Then
            Return True
        Else
            Return False
        End If

    End Function

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
        If CacheDate <= Date.UtcNow Then
            ' Always open here incase we update below
            If OpenTransaction Then
                Call MHDB.BeginSQLiteTransaction()
            End If

            ' Delete any records for this type and region since we have a fresh set to load
            Call MHDB.ExecuteNonQuerySQL("DELETE FROM MARKET_ORDERS WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID))

            ' Get the data from ESI 
            PublicData = GetPublicData(ESIURL & "markets/" & CStr(RegionID) & "/orders/" & TranquilityDataSource & "&type_id=" & CStr(TypeID), CacheDate)

            If Not IsNothing(PublicData) Then
                MarketOrdersOutput = JsonConvert.DeserializeObject(Of List(Of ESIMarketOrder))(PublicData)

                For Each item In MarketOrdersOutput
                    If Not LocationIDs.Contains(item.location_id) Then
                        LocationIDs.Add(item.location_id)
                    End If
                Next

                ' Parse the data
                If MarketOrdersOutput.Count > 0 Then
                    Application.DoEvents()

                    ' Now read through all the output items that are not in the table insert them in MARKET_ORDERS
                    For i = 0 To MarketOrdersOutput.Count - 1
                        With MarketOrdersOutput(i)
                            Dim StationLocation As SystemRegion
                            Dim OrderDownloadType As String = ""

                            ' We can have stations or structures in market data, so look up based on ID
                            If .location_id < 67000000 Then
                                StationLocation = GetStationStructureLocation(.location_id, Nothing, StationStructureIDType._Station)
                            Else
                                StationLocation = GetStationStructureLocation(.location_id, SelectedCharacter.CharacterTokenData, StationStructureIDType._Structure)
                            End If

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
                PublicData = GetPublicData(ESIURL & "markets/" & CStr(RegionID) & "/history/" & TranquilityDataSource & "&type_id=" & CStr(TypeID), ESICacheDate)

                MarketPricesOutput = JsonConvert.DeserializeObject(Of List(Of ESIMarketHistoryItem))(PublicData)

                ' Read in the data
                If Not IsNothing(MarketPricesOutput) Then

                    If MarketPricesOutput.Count > 0 Then
                        ' See what the last cache date we have on the records first - any records after or equal to this date we want to update
                        If CacheDate = NoDate Then ' only run this if we don't already have the max date for this typeid
                            SQL = "SELECT CACHE_DATE FROM MARKET_HISTORY_UPDATE_CACHE WHERE TYPE_ID = " & CStr(TypeID) & " AND REGION_ID = " & CStr(RegionID)
                            DBCommand = New SQLiteCommand(SQL, MHDB.DBREf)
                            rsCheck = DBCommand.ExecuteReader
                            If rsCheck.Read() Then
                                If IsDBNull(rsCheck.GetValue(0)) Then
                                    MaxRecordDate = NoDate
                                Else
                                    ' The cache date is the date when we run the next update
                                    MaxRecordDate = CDate(rsCheck.GetString(0))
                                End If
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
            PublicData = GetPublicData(ESIURL & "markets/prices/" & TranquilityDataSource, CacheDate)

            If Not IsNothing(PublicData) Then
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
            End If
            ' Data didn't download
            Return False
        End If

        Return True

    End Function

    ' Updates the cost index for installing industry jobs per type of activity. This does not include wormhole space.
    Public Function UpdateIndustrySystemsCostIndex(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean
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
            TempLabel.Text = "Downloading System Index Data..."
            Application.DoEvents()

            ' Get the data from ESI
            PublicData = GetPublicData(ESIURL & "industry/systems/" & TranquilityDataSource, CacheDate)

            If Not IsNothing(PublicData) Then
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
            End If
            ' Json file didn't download or other error
            Return False
        End If

        Return True

    End Function

    ' Downloads all public structure ID's that have markets and then refreshes the data on them in the Stations Table.
    Public Function UpdatePublicStructureMarketsData(Optional ByRef UpdateLabel As Label = Nothing, Optional ByRef PB As ProgressBar = Nothing) As Boolean

        Try
            Dim TempLabel As Label
            Dim TempPB As ProgressBar
            Dim PublicData As String
            Dim PublicStructureIDs As New List(Of Long)
            Dim TempList As New List(Of Long)

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

            ' Update public structures only if dummy not selected, since we need a token to update the structure data
            If CB.DataUpdateable(CacheDateType.PublicStructures) And SelectedCharacter.ID <> DummyCharacterID Then

                TempLabel.Text = "Downloading Public Structure Data..."
                Application.DoEvents()

                ' Get all the public structure IDs from ESI
                Dim startdate As Date = Now
                PublicData = GetPublicData(ESIURL & "universe/structures/" & TranquilityDataSource & "&filter=market", CacheDate)

                If Not IsNothing(PublicData) Then
                    PublicStructureIDs = JsonConvert.DeserializeObject(Of List(Of Long))(PublicData)

                    If Not IsNothing(PublicStructureIDs) Then
                        If PublicStructureIDs.Count > 0 Then

                            Call EVEDB.BeginSQLiteTransaction()

                            ' Delete all structures from the table first, so that we have a fresh list. Industry jobs and Asset updates will refresh their structures if needed
                            Call ResetPublicStructureData()

                            ' Set the labels and progress bar if needed
                            TempLabel.Text = "Saving Public Structure Data..."
                            TempPB.Visible = False ' don't show this one
                            Application.DoEvents()

                            ThreadsArray = New List(Of Thread)

                            ' Now read through all the output items and update them in STATIONS - using a thread per look up
                            ' Call each thread for the pairs
                            For Each ID In PublicStructureIDs
                                Dim Params As New StructureUpdateData
                                Params.ID = ID
                                Params.TokenData = SelectedCharacter.CharacterTokenData

                                Dim SDThread As New Thread(AddressOf UpdateSingleStructureData)
                                SDThread.Start(Params)
                                ' Save the thread if we need to kill it
                                ThreadsArray.Add(SDThread)
                            Next

                            Dim stillworking As Boolean = True

                            Do Until Not stillworking
                                ' Now loop until all the threads are done
                                For Each T In ThreadsArray
                                    If T.ThreadState = ThreadState.Running Then
                                        ' Still working on at least 1 thread, so exit
                                        stillworking = True
                                        Exit For
                                    Else
                                        stillworking = False
                                    End If
                                Next
                            Loop

                            ' Make sure all threads are not running
                            Call KillThreads()

                            ' All set, update cache date before leaving
                            Call CB.UpdateCacheDate(CacheDateType.PublicStructures, CacheDate)

                            ' Done updating
                            Call EVEDB.CommitSQLiteTransaction()

                            Return True

                        End If
                    End If
                End If

                ' Data didn't download
                Return False
            End If

            Return True

        Catch ex As Exception
            MsgBox("Failed to update public structure data: " & ex.Message, vbInformation, Application.ProductName)

            Return False

        End Try

    End Function

    Private Sub UpdateSingleStructureData(Parameters As Object)
        Dim Param As StructureUpdateData

        Param = CType(Parameters, StructureUpdateData)

        Dim TempIDList As New List(Of Long)
        TempIDList.Add(Param.ID)

        Call UpdateStructureData(TempIDList, Param.TokenData)

    End Sub

    Private Structure StructureUpdateData
        Dim ID As Long
        Dim TokenData As SavedTokenData
    End Structure

#End Region

#Region "Supporting Functions"

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

    Private Function GetErrorResponseBody(ByVal Ex As WebException, ByRef RefErrorData As ESIError) As String
        Try
            Dim resp As String = New StreamReader(Ex.Response.GetResponseStream()).ReadToEnd()
            Dim ErrorData As ESIError = JsonConvert.DeserializeObject(Of ESIError)(resp)

            If Not IsNothing(ErrorData) Then
                ' save the data for reference
                RefErrorData = ErrorData
                If IsNothing(ErrorData.ErrorDescription) Then
                    ErrorData.ErrorDescription = ""
                End If
                Return ErrorData.ErrorText & " - " & ErrorData.ErrorDescription
            Else
                RefErrorData.ErrorText = Ex.Message
                RefErrorData.ErrorDescription = ""
                Return Ex.Message
            End If
        Catch tryex As Exception
            Return "Unknown error"
        End Try

    End Function

    Public Function GetFactionData() As List(Of ESIFactionData)
        Dim PublicData As String

        Try
            PublicData = GetPublicData(ESIURL & "universe/factions/" & TranquilityDataSource, Nothing)

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

        PublicData = GetPublicData(ESIURL & "universe/names/" & TranquilityDataSource, Nothing, IDs)

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

    ' Returns the URL from all the data for this registration
    Public Function GetURL() As String
        Return ESIAuthorizeURL & "?response_type=code" & "&redirect_uri=http://" & LocalHost & ":" & LocalPort & "&client_id=" & ClientID & "&scope=" & ScopesString
    End Function

    Public Function GetAuthToken(ByVal StreamText As String) As String
        Dim AuthToken As String = ""

        ' Process the auth stream now
        Dim AuthTokenString As String() = StreamText.Split(New Char() {" "c})

        For i = 0 To AuthTokenString.Count - 1
            If AuthTokenString(i).Contains("/?code=") Then
                ' Strip the header and save the string
                Dim Start As Integer = InStr(AuthTokenString(i), "=")
                AuthToken = AuthTokenString(i).Substring(Start)
                Exit For
            End If
        Next

        Return AuthToken
    End Function

    ' Aborts all the threads in threads array
    Private Sub KillThreads()
        ' Kill all the threads
        For i = 0 To ThreadsArray.Count - 1
            If ThreadsArray(i).IsAlive Then
                ThreadsArray(i).Abort()
            End If
        Next
    End Sub

#End Region

End Class

#Region "JSON Classes"

Public Class ESIError
    <JsonProperty("error")> Public ErrorText As String
    <JsonProperty("error_description")> Public ErrorDescription As String
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

Public Class ESIUniverseStructure
    <JsonProperty("name")> Public name As String
    <JsonProperty("owner_id")> Public owner_id As Integer
    <JsonProperty("position")> Public position As ESIPosition
    <JsonProperty("solar_system_id")> Public solar_system_id As Integer
    <JsonProperty("type_id")> Public type_id As Integer
End Class

Public Class ESIPostion
    <JsonProperty("x")> Public x As Double
    <JsonProperty("y")> Public y As Double
    <JsonProperty("z")> Public z As Double
End Class

#End Region