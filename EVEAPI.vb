Imports System.Xml
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

' Class will deal with all API calls to the EVE API
Public Class EVEAPI

    ' API Path's
    Private Const APIURL As String = "https://api.eveonline.com"

    ' 09-01-2011: New API info page to get all data about accounts and access.
    Private Const AccountDetails As String = "/account/APIKeyInfo.xml.aspx"

    Private Const CharacterSheet As String = "/char/CharacterSheet.xml.aspx" ' For skill tree
    Private Const StandingsSheet As String = "/char/Standings.xml.aspx" ' For NPC Standings list
    Private Const ResearchAgents As String = "/char/Research.xml.aspx" ' For NPC Standings list

    Private Const CharacterAssets As String = "/char/AssetList.xml.aspx" ' Returns list of assets for character
    Private Const CorporationAssets As String = "/corp/AssetList.xml.aspx" ' Returns list of assets for corporation, requires corp key

    Private Const CorporationFacilities As String = "/corp/Facilities.xml.aspx" ' Returns a list of all corp facilities and outposts

    ' Blueprint API
    Private Const CharacterBlueprints As String = "/char/Blueprints.xml.aspx"
    Private Const CorporationBlueprints As String = "/corp/Blueprints.xml.aspx"

    'Added /char/IndustryJobsHistory.xml.aspx (cache: 6 hours)
    'Returns a list of running and completed jobs for your character, up to 90 days or 10000 rows.
    Private Const CharIndustryLogHistory As String = "/char/IndustryJobsHistory.xml.aspx" ' List of personal industry jobs historical

    'Added /corp/IndustryJobsHistory.xml.aspx (cache: 6 hours)
    'Returns a list of running and completed jobs for your corporation, up to 90 days or 10000 rows.
    Private Const CorpIndustryLogHistory As String = "/corp/IndustryJobsHistory.xml.aspx" ' List of industry jobs for corporation historical

    'Modified /char/IndustryJobs.xml.aspx (cache: 15 minutes)
    'Returns a list of running jobs for your character, up to 90 days or 10000 rows.
    Private Const CharIndustryLog As String = "/char/IndustryJobs.xml.aspx" ' List of personal industry jobs

    'Modified /corp/IndustryJobs.xml.aspx (cache: 15 minutes)
    'Returns a list of running jobs for your corporation, up to 90 days or 10000 rows.
    Private Const CorpIndustryLog As String = "/corp/IndustryJobs.xml.aspx" ' List of industry jobs for corporation

    Private Const EVEOutpostData As String = "/eve/ConquerableStationList.xml.aspx" ' List of conquerable stations

    Private Const EVEServerData As String = "/server/ServerStatus.xml.aspx" ' Singularity server data

    Private APIError As ErrorData

    Public Sub New()
        APIError.ErrorText = ""
        APIError.ErrorCode = 0
        APIError.CacheDate = NoDate
    End Sub

    Public Structure ErrorData
        Dim ErrorCode As Integer
        Dim ErrorText As String
        Dim CacheDate As Date
    End Structure

    Public Function GetErrorCode() As Integer
        Return APIError.ErrorCode
    End Function

    Public Function GetErrorText() As String
        Return APIError.ErrorText
    End Function

    Public Function GetErrorCacheDate() As Date
        Return APIError.CacheDate
    End Function

    Public Function GetAPIErrorData() As ErrorData
        Return APIError
    End Function

    ' Function returns an array of 3 maximum records with account information for each. If the ID is given, then will only return that character data
    Public Function GetAccountCharacters(ByVal KeyID As Long, ByVal AccountAPIKey As String, ByRef KeyType As String, Optional ByVal ID As Long = 0) As Character()
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim ReturnData() As Character = Nothing
        Dim TempData() As Character = Nothing
        Dim EVEAPIQuery As String

        Dim i As Integer = 0
        Dim j As Integer = 0

        Dim CachedUntil As Date ' Date the data is cached before update
        Dim AccessMask As Long ' Bitmask (as a long) of API access fields
        Dim APIExpirationDate As Date ' Date when this API expires

        Dim TempDate As String
        Dim TempCharID As Long

        ' Set up query string
        If ID = 0 Then
            EVEAPIQuery = APIURL & AccountDetails & "?keyID=" & CStr(KeyID) & "&vCode=" & AccountAPIKey
        Else
            EVEAPIQuery = APIURL & AccountDetails & "?keyID=" & CStr(KeyID) & "&vCode=" & AccountAPIKey & "&characterID=" & CStr(ID)
        End If

        'Get the XML Document
        m_xmld = QueryEVEAPI(EVEAPIQuery)

        ' Check data
        If IsNothing(m_xmld) Then
            ' Had an error, return nothing
            GetAccountCharacters = Nothing
            Exit Function
        End If

        ' Get the Access codes and expiration date first
        m_nodelist = m_xmld.SelectNodes("/eveapi/result/key")
        For Each m_node In m_nodelist
            With m_node.Attributes
                AccessMask = CLng(.GetNamedItem("accessMask").Value)
                ' Might be blank
                TempDate = .GetNamedItem("expires").Value
                If TempDate = "" Then
                    APIExpirationDate = NoExpiry
                Else
                    APIExpirationDate = CDate(TempDate)
                End If
                KeyType = CStr(.GetNamedItem("type").Value)
            End With
        Next

        ' Get the cache update
        m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
        ' Should only be one time
        CachedUntil = CDate(m_nodelist.Item(0).InnerText) ' Updated about every 5 minutes. Time here is in UTC

        ' Get the list of nodes for characters
        m_nodelist = m_xmld.SelectNodes("/eveapi/result/key/rowset/row")

        ' Loop through the nodes for three characters 
        ' if we are just doing the one, then it will exit with the one
        For Each m_node In m_nodelist
            If IsNothing(ReturnData) Then
                ReDim ReturnData(0)
                ReturnData(0) = New Character
            ElseIf ID = 0 Then
                ' For now copy old and redim
                TempData = ReturnData
                ReDim ReturnData(i)

                For k = 0 To i
                    ReturnData(k) = New Character
                Next

                For j = 0 To TempData.Count - 1
                    ReturnData(j).KeyID = TempData(j).KeyID
                    ReturnData(j).APIKey = TempData(j).APIKey
                    ReturnData(j).CachedUntil = TempData(j).CachedUntil
                    ReturnData(j).ID = TempData(j).ID
                    ReturnData(j).Name = TempData(j).Name
                    ReturnData(j).CharacterCorporation = TempData(j).CharacterCorporation
                    ReturnData(j).AccessMask = TempData(j).AccessMask
                    ReturnData(j).APIExpiration = TempData(j).APIExpiration
                    ReturnData(j).AssetsAccess = TempData(j).AssetsAccess
                    ReturnData(j).SkillsAccess = TempData(j).SkillsAccess
                    ReturnData(j).StandingsAccess = TempData(j).StandingsAccess
                    ReturnData(j).JobsAccess = TempData(j).JobsAccess
                Next
            End If

            With m_node.Attributes
                TempCharID = CLng(.GetNamedItem("characterID").Value)
            End With

            ' Only add if the character matches the name sent, or it is blank
            If ID = 0 Or ID = TempCharID Then
                ReturnData(i).KeyID = CLng(KeyID)
                ReturnData(i).APIKey = AccountAPIKey
                ReturnData(i).CachedUntil = CDate(CachedUntil)
                ReturnData(i).AccessMask = AccessMask
                ReturnData(i).APIExpiration = APIExpirationDate

                With m_node.Attributes
                    ReturnData(i).ID = TempCharID
                    ReturnData(i).Name = .GetNamedItem("characterName").Value
                    ReturnData(i).CharacterCorporation = New Corporation(CLng(.GetNamedItem("corporationID").Value), .GetNamedItem("corporationName").Value)
                End With

                ' Set the api access (by asset mask) for this character
                Call ReturnData(i).SetAPIAccess()

                i = i + 1
            End If

        Next

        GetAccountCharacters = ReturnData

    End Function

    ' Function will return the set of skills for the character sent
    Public Function GetCharacterSkills(ByVal APIKey As APIKeyData) As EVESkillList
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim child_node As XmlNode
        Dim m_nodeSkills As XmlNode

        Dim EVEAPIQuery As String
        Dim TempSkillTree As New EVESkillList
        Dim CachedUntil As Date
        Dim i As Integer

        If APIKey.Access Then

            EVEAPIQuery = APIURL & CharacterSheet & "?keyID=" & CStr(APIKey.KeyID) & "&vCode=" & APIKey.APIKey & "&characterID=" & CStr(APIKey.ID)

            'Create the XML Document
            m_xmld = QueryEVEAPI(EVEAPIQuery)

            ' Check data
            If IsNothing(m_xmld) Then
                ' Had an error, return nothing
                GetCharacterSkills = Nothing
                Exit Function
            End If

            ' Get the cache update
            m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
            ' Should only be one time
            CachedUntil = CDate(m_nodelist.Item(0).InnerText)

            ' Get the list of nodes for characters
            m_nodelist = m_xmld.SelectNodes("/eveapi/result")
            ' Just go to the result
            m_node = m_nodelist.Item(0)

            ' Loop through the child nodes and look for rowset = "skills"
            For Each child_node In m_node.ChildNodes
                If child_node.Name = "rowset" Then
                    ' In rowsets, check for skills
                    If child_node.Attributes.Item(0).Value = "skills" Then
                        ' Look through the child node's children for data
                        For Each m_nodeSkills In child_node.ChildNodes
                            With m_nodeSkills.Attributes
                                ' Insert the skill
                                TempSkillTree.InsertSkill(CLng(.GetNamedItem("typeID").Value), CInt(.GetNamedItem("level").Value), CLng(.GetNamedItem("skillpoints").Value), False, 0)
                            End With
                            i = i + 1
                        Next
                    End If
                End If
            Next

            GetCharacterSkills = TempSkillTree

        Else
            ' Now insert one skill that every char will have regardless
            TempSkillTree.InsertSkill(3402, 3, 8000, False, 0, "Science") ' So far just Science
            GetCharacterSkills = TempSkillTree

            APIError.ErrorCode = -1
            If APIError.ErrorText = NoStandingsLoaded Then
                APIError.ErrorText = NoSkillsStandingsLoaded
            Else
                APIError.ErrorText = NoSkillsLoaded
            End If
        End If

    End Function

    ' Function will get all NPC standings and store for character
    Public Function GetCharacterStandings(ByVal APIKey As APIKeyData) As NPCStandings
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim child_node As XmlNode
        Dim m_nodeSkills As XmlNode

        Dim EVEAPIQuery As String
        Dim TempStandingsList As New NPCStandings
        Dim i As Integer

        If APIKey.Access Then

            EVEAPIQuery = APIURL & StandingsSheet & "?keyID=" & CStr(APIKey.KeyID) & "&vCode=" & APIKey.APIKey & "&characterID=" & CStr(APIKey.ID)

            'Create the XML Document
            m_xmld = QueryEVEAPI(EVEAPIQuery)

            ' Check data
            If IsNothing(m_xmld) Then
                ' Had an error, return nothing
                GetCharacterStandings = Nothing
                Exit Function
            End If

            ' Get the list of nodes for characters
            m_nodelist = m_xmld.SelectNodes("/eveapi/result/characterNPCStandings")
            ' Just go to the result
            m_node = m_nodelist.Item(0)

            ' Loop through the child nodes and look for rowset = "skills"
            For Each child_node In m_node.ChildNodes
                If child_node.Name = "rowset" Then
                    ' In rowsets, check for types
                    If child_node.Attributes.Item(0).Value = "agents" Then
                        ' Look through the child node's children for data
                        For Each m_nodeSkills In child_node.ChildNodes
                            With m_nodeSkills.Attributes
                                ' Insert the standing
                                ' Insert the skill
                                TempStandingsList.InsertStanding(CLng(.GetNamedItem("fromID").Value), "Agent", .GetNamedItem("fromName").Value, CDbl(.GetNamedItem("standing").Value))
                            End With
                            i = i + 1
                        Next
                    End If

                    ' In rowsets, check for types
                    If child_node.Attributes.Item(0).Value = "NPCCorporations" Then
                        ' Look through the child node's children for data
                        For Each m_nodeSkills In child_node.ChildNodes
                            With m_nodeSkills.Attributes
                                ' Insert the standing
                                ' Insert the skill
                                TempStandingsList.InsertStanding(CLng(.GetNamedItem("fromID").Value), "Corporation", .GetNamedItem("fromName").Value, CDbl(.GetNamedItem("standing").Value))
                            End With
                            i = i + 1
                        Next
                    End If

                    ' In rowsets, check for types
                    If child_node.Attributes.Item(0).Value = "factions" Then
                        ' Look through the child node's children for data
                        For Each m_nodeSkills In child_node.ChildNodes
                            With m_nodeSkills.Attributes
                                ' Insert the standing
                                ' Insert the skill
                                TempStandingsList.InsertStanding(CLng(.GetNamedItem("fromID").Value), "Faction", .GetNamedItem("fromName").Value, CDbl(.GetNamedItem("standing").Value))
                            End With
                            i = i + 1
                        Next
                    End If
                End If
            Next

            GetCharacterStandings = TempStandingsList
        Else
            GetCharacterStandings = Nothing
            APIError.ErrorCode = -2
            If APIError.ErrorText = NoSkillsLoaded Then
                APIError.ErrorText = NoSkillsStandingsLoaded
            Else
                APIError.ErrorText = NoStandingsLoaded
            End If
        End If

    End Function

    ' Function will get all Industry jobs for the character and type sent (corp or personal) and return an array of the jobs
    Public Function GetIndustryJobs(ByVal SentKey As APIKeyData, ByVal ScanType As ScanType, ByRef CachedUntilDate As Date) As List(Of IndustryJob)
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim Assets() As Long = Nothing
        Dim EVEAPIQuery As String

        Dim ReturnData As New List(Of IndustryJob)
        Dim TempJob As IndustryJob
        Dim TempCharID As Long = 0
        Dim i As Long = 0
        Dim j As Long = 0

        ' Status field
        '101 = delivered, anything else is not
        If SentKey.Access Then
            ' Run each query for current and history and process
            For k = 0 To 1

                ' Set up query string 
                If ScanType = ScanType.Personal Then
                    If k = 0 Then
                        EVEAPIQuery = APIURL & CharIndustryLog & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey & "&characterID=" & CStr(SentKey.ID)
                    Else
                        EVEAPIQuery = APIURL & CharIndustryLogHistory & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey & "&characterID=" & CStr(SentKey.ID)
                    End If
                Else ' Corp (no charid required)
                    If k = 0 Then
                        EVEAPIQuery = APIURL & CorpIndustryLog & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey
                    Else
                        EVEAPIQuery = APIURL & CorpIndustryLogHistory & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey
                    End If
                End If

                'Create the XML Document
                m_xmld = QueryEVEAPI(EVEAPIQuery)

                ' Check data
                If IsNothing(m_xmld) Then
                    Return Nothing
                End If

                ' Get the cache update
                m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
                ' Should only be one time
                CachedUntilDate = CDate(m_nodelist.Item(0).InnerText)

                ' Get the list of nodes for characters
                m_nodelist = m_xmld.SelectNodes("/eveapi/result/rowset/row")

                ' Loop through the nodes for three characters 
                ' if we are just doing the one, then it will exit with the one
                For Each m_node In m_nodelist

                    With m_node.Attributes
                        TempCharID = CLng(.GetNamedItem("installerID").Value)
                    End With

                    ' Only add if the character matches the name sent
                    If TempCharID = SentKey.ID Then
                        With m_node.Attributes
                            TempJob.jobID = CLng(.GetNamedItem("jobID").Value)
                            TempJob.installerID = CLng(.GetNamedItem("installerID").Value)
                            TempJob.installerName = .GetNamedItem("installerName").Value
                            TempJob.facilityID = CLng(.GetNamedItem("facilityID").Value)
                            TempJob.solarSystemID = CLng(.GetNamedItem("solarSystemID").Value)
                            TempJob.solarSystemName = .GetNamedItem("solarSystemName").Value
                            TempJob.stationID = CLng(.GetNamedItem("stationID").Value)
                            TempJob.activityID = CInt(.GetNamedItem("activityID").Value)
                            TempJob.blueprintID = CDbl(.GetNamedItem("blueprintID").Value)
                            TempJob.blueprintTypeID = CLng(.GetNamedItem("blueprintTypeID").Value)
                            TempJob.blueprintTypeName = .GetNamedItem("blueprintTypeName").Value
                            TempJob.blueprintLocationID = CLng(.GetNamedItem("blueprintLocationID").Value)
                            TempJob.outputLocationID = CLng(.GetNamedItem("outputLocationID").Value)
                            TempJob.runs = CLng(.GetNamedItem("runs").Value)
                            TempJob.cost = CDbl(.GetNamedItem("cost").Value)
                            TempJob.teamID = CLng(.GetNamedItem("teamID").Value)
                            TempJob.licensedRuns = CLng(.GetNamedItem("licensedRuns").Value)
                            TempJob.probability = CDbl(.GetNamedItem("probability").Value)
                            TempJob.productTypeID = CLng(.GetNamedItem("productTypeID").Value)
                            TempJob.productTypeName = .GetNamedItem("productTypeName").Value
                            TempJob.status = CLng(.GetNamedItem("status").Value)
                            TempJob.timeInSeconds = CLng(.GetNamedItem("timeInSeconds").Value)
                            TempJob.successfulRuns = CLng(.GetNamedItem("successfulRuns").Value)

                            TempJob.startDate = DateTime.ParseExact(.GetNamedItem("startDate").Value, SQLiteDateFormat, LocalCulture)
                            TempJob.endDate = DateTime.ParseExact(.GetNamedItem("endDate").Value, SQLiteDateFormat, LocalCulture)
                            TempJob.pauseDate = DateTime.ParseExact(.GetNamedItem("pauseDate").Value, SQLiteDateFormat, LocalCulture)
                            TempJob.completedDate = DateTime.ParseExact(.GetNamedItem("completedDate").Value, SQLiteDateFormat, LocalCulture)

                            TempJob.completedCharacterID = CLng(.GetNamedItem("completedCharacterID").Value)

                            TempJob.JobType = ScanType
                        End With

                        ReturnData.Add(TempJob)

                    End If

                Next
            Next

            Return ReturnData
        Else
            CachedUntilDate = Nothing
            APIError.ErrorCode = -3
            APIError.ErrorText = NoIndyJobsLoaded

            Return Nothing
        End If

    End Function

    ' Function will get a list of all of a corporations facilities, including POS and Outpost - cachetime 1 hour
    Public Function GetCorpFacilities(ByVal SentKey As APIKeyData, ByRef CachedUntilDate As Date) As List(Of CorpFacility)
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim Assets() As Long = Nothing
        Dim EVEAPIQuery As String

        Dim ReturnData As New List(Of CorpFacility)
        Dim TempFacility As New CorpFacility
        Dim TempCharID As Long = 0
        Dim i As Long = 0
        Dim j As Long = 0

        If SentKey.Access Then
            ' Set up query string
            EVEAPIQuery = APIURL & CorporationFacilities & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey

            'Create the XML Document
            m_xmld = QueryEVEAPI(EVEAPIQuery)

            ' Check data
            If IsNothing(m_xmld) Then
                Return Nothing
            End If

            ' Get the cache update
            m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
            ' Should only be one time
            CachedUntilDate = CDate(m_nodelist.Item(0).InnerText)

            ' Get the list of nodes for characters
            m_nodelist = m_xmld.SelectNodes("/eveapi/result/rowset/row")

            ' Loop through the nodes for three characters 
            ' if we are just doing the one, then it will exit with the one
            For Each m_node In m_nodelist

                ' Only add if the character matches the name sent
                With m_node.Attributes
                    TempFacility.facilityID = CLng(.GetNamedItem("facilityID").Value)
                    TempFacility.typeID = CLng(.GetNamedItem("typeID").Value)
                    TempFacility.typeName = .GetNamedItem("typeName").Value
                    TempFacility.solarSystemID = CLng(.GetNamedItem("solarSystemID").Value)
                    TempFacility.solarSystemName = .GetNamedItem("solarSystemName").Value
                    TempFacility.regionID = CLng(.GetNamedItem("regionID").Value)
                    TempFacility.regionName = .GetNamedItem("regionName").Value
                    TempFacility.starbaseModifier = CDbl(.GetNamedItem("starbaseModifier").Value)
                    TempFacility.tax = CDbl(.GetNamedItem("tax").Value)
                End With

                ReturnData.Add(TempFacility)

            Next

            Return ReturnData
        Else
            CachedUntilDate = Nothing
            APIError.ErrorCode = -3
            APIError.ErrorText = NoIndyJobsLoaded

            Return Nothing
        End If

        Return Nothing

    End Function

    ' Function will get the current Research Agents for the character
    Public Function GetCurrentResearchAgents(ByVal SentKey As APIKeyData, ByRef CacheDate As Date) As List(Of CurrentResearchAgent)
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim EVEAPIQuery As String

        Dim ReturnData As New List(Of CurrentResearchAgent)
        Dim TempAgent As CurrentResearchAgent

        If SentKey.Access Then
            ' Set up query string
            EVEAPIQuery = APIURL & ResearchAgents & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey & "&characterID=" & CStr(SentKey.ID)

            'Create the XML Document
            m_xmld = QueryEVEAPI(EVEAPIQuery)

            ' Check data
            If IsNothing(m_xmld) Then
                Return Nothing
            End If

            ' Get the cache update
            m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
            ' Should only be one time
            CacheDate = CDate(m_nodelist.Item(0).InnerText)

            ' Get the list of nodes for characters
            m_nodelist = m_xmld.SelectNodes("/eveapi/result/rowset/row")

            ' Loop through the nodes for three characters 
            ' if we are just doing the one, then it will exit with the one
            For Each m_node In m_nodelist

                With m_node.Attributes
                    TempAgent.agentID = CLng(.GetNamedItem("agentID").Value)
                    TempAgent.skillTypeID = CLng(.GetNamedItem("skillTypeID").Value)
                    TempAgent.researchStartDate = DateTime.ParseExact(.GetNamedItem("researchStartDate").Value, SQLiteDateFormat, LocalCulture)
                    TempAgent.pointsPerDay = CDbl(.GetNamedItem("pointsPerDay").Value)
                    TempAgent.remainderPoints = CDbl(.GetNamedItem("remainderPoints").Value)
                End With

                ReturnData.Add(TempAgent)

            Next

            Return ReturnData
        Else
            APIError.ErrorCode = -5
            APIError.ErrorText = NoResearchLoaded

            Return Nothing
        End If
    End Function

    ' Returns an array of all Assets, return value to cache date as reference 
    Public Function GetAssets(ByVal SentKey As APIKeyData, ByVal ScanType As ScanType, ByRef CacheDate As Date) As List(Of EVEAsset)
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_Result As XmlNode
        Dim rowset_node As XmlNode
        Dim EVEAPIQuery As String

        If SentKey.Access Then
            ' Set up query string
            If ScanType = ScanType.Personal Then
                EVEAPIQuery = APIURL & CharacterAssets & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey & "&characterID=" & CStr(SentKey.ID)
            Else ' Corp
                EVEAPIQuery = APIURL & CorporationAssets & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey
            End If

            'Create the XML Document
            m_xmld = QueryEVEAPI(EVEAPIQuery)

            ' Check data
            If IsNothing(m_xmld) Then
                ' Had an error, return nothing
                Return Nothing
            End If

            ' Get the cache update
            m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
            ' Should only be one time
            CacheDate = CDate(m_nodelist.Item(0).InnerText)

            ' Get the list of nodes for characters
            m_nodelist = m_xmld.SelectNodes("/eveapi/result")
            ' Just go to the result
            m_Result = m_nodelist.Item(0)

            ' Get the base rowset
            rowset_node = m_Result.FirstChild

            ' Get all the assets from this node
            Return GetAssetList(rowset_node)
        Else
            CacheDate = Nothing
            Return Nothing
        End If

    End Function

    ' Function will recursively search all asset nodes and return  a full list of assets
    Private Function GetAssetList(ByVal rowSet As XmlNode, Optional ParentItemID As Long = 0) As List(Of EVEAsset)
        Dim TempAssets As New List(Of EVEAsset)
        Dim ReturnAssets As New List(Of EVEAsset)
        Dim InsertAsset As New EVEAsset
        Dim ColumnString As String
        Dim m_node As XmlNode
        Dim TempParentID As Long = 0

        ' Need to get the assets of the childs
        For Each m_node In rowSet.ChildNodes
            If m_node.HasChildNodes Then
                ' Asset within asset (container) - Get the item ID and save it for all children
                If m_node.Name = "row" And m_node.OuterXml.ToString.Contains("itemID") Then
                    TempParentID = CLng(m_node.Attributes.GetNamedItem("itemID").Value)
                Else
                    TempParentID = ParentItemID
                End If

                TempAssets = GetAssetList(m_node, TempParentID)

                ' Insert these into the return list
                For Each Asset In TempAssets
                    ReturnAssets.Add(Asset)
                Next
            End If

            ' Only add if it is a row
            If m_node.Name = "row" Then
                ColumnString = m_node.OuterXml.ToString
                InsertAsset = New EVEAsset

                ' Add the node asset if it is in the columns
                If ColumnString.Contains("itemID") Then
                    InsertAsset.ItemID = CLng(m_node.Attributes.GetNamedItem("itemID").Value)
                Else
                    InsertAsset.ItemID = 0
                End If

                If ColumnString.Contains("locationID") Then
                    InsertAsset.LocationID = CLng(m_node.Attributes.GetNamedItem("locationID").Value)
                Else
                    InsertAsset.LocationID = 0
                End If

                ' Special processing for offices in stations or outposts
                ' To convert locationIDs greater than or equal to 66000000 and less than 67000000 to stationIDs from staStations subtract 6000001 from the locationID. 
                ' To convert locationIDs greater than or equal to 67000000 and less than 68000000 to stationIDs from ConquerableStationList subtract 6000000 from the locationID.
                If InsertAsset.LocationID >= 66000000 And InsertAsset.LocationID < 67000000 Then
                    InsertAsset.LocationID = InsertAsset.LocationID - 6000001
                ElseIf InsertAsset.LocationID >= 67000000 And InsertAsset.LocationID < 68000000 Then
                    InsertAsset.LocationID = InsertAsset.LocationID - 6000000
                End If

                If InsertAsset.LocationID = 0 Then
                    ' Set it to the parent ItemID
                    InsertAsset.LocationID = ParentItemID
                End If

                If ColumnString.Contains("typeID") Then
                    InsertAsset.TypeID = CLng(m_node.Attributes.GetNamedItem("typeID").Value)
                Else
                    InsertAsset.TypeID = 0
                End If

                If ColumnString.Contains("quantity") Then
                    InsertAsset.Quantity = CLng(m_node.Attributes.GetNamedItem("quantity").Value)
                Else
                    InsertAsset.Quantity = 0
                End If

                If ParentItemID = 0 Then
                    ' This is a base node, so set the flag to negative to signify
                    InsertAsset.FlagID = -1 * CInt(m_node.Attributes.GetNamedItem("flag").Value)
                Else ' Regular subnode
                    If ColumnString.Contains("flag") Then
                        InsertAsset.FlagID = CInt(m_node.Attributes.GetNamedItem("flag").Value)
                    Else
                        InsertAsset.FlagID = 0
                    End If
                End If

                If ColumnString.Contains("singleton") Then
                    InsertAsset.Singleton = CInt(m_node.Attributes.GetNamedItem("singleton").Value)
                Else
                    InsertAsset.Singleton = 0
                End If

                If ColumnString.Contains("rawQuantity") Then
                    InsertAsset.RawQuantity = CInt(m_node.Attributes.GetNamedItem("rawQuantity").Value)
                Else
                    InsertAsset.RawQuantity = 0
                End If

                ' Leave these blank
                InsertAsset.LocationName = ""
                InsertAsset.FlagText = ""
                InsertAsset.TypeName = ""
                InsertAsset.TypeGroup = ""
                InsertAsset.TypeCategory = ""

                ReturnAssets.Add(InsertAsset)
            End If
        Next

        Return ReturnAssets

    End Function

    ' Function gets the list of all outposts in the game - refreshed daily
    Public Function GetOutpostList(ByRef CachedUntilDate As Date) As List(Of Station)
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim EVEAPIQuery As String

        Dim ReturnData As New List(Of Station)
        Dim TempStation As Station

        ' Set up query string
        EVEAPIQuery = APIURL & EVEOutpostData

        'Create the XML Document
        m_xmld = QueryEVEAPI(EVEAPIQuery)

        ' Check data
        If IsNothing(m_xmld) Then
            Return Nothing
        End If

        ' Update the cache update to 24 hours from this query
        'CacheDate = DateAdd(DateInterval.Day, 1, Date.UtcNow)
        ' Get the cache update
        m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
        ' Should only be one time
        CachedUntilDate = CDate(m_nodelist.Item(0).InnerText)

        ' Get the list of nodes for characters
        m_nodelist = m_xmld.SelectNodes("/eveapi/result/rowset/row")

        ' Loop through the nodes for three characters 
        ' if we are just doing the one, then it will exit with the one
        For Each m_node In m_nodelist

            With m_node.Attributes
                TempStation.stationID = CLng(.GetNamedItem("stationID").Value)
                TempStation.stationName = .GetNamedItem("stationName").Value
                TempStation.stationTypeID = CLng(.GetNamedItem("stationTypeID").Value)
                TempStation.solarSystemID = CLng(.GetNamedItem("solarSystemID").Value)
                TempStation.corporationID = CLng(.GetNamedItem("corporationID").Value)
                TempStation.corporationName = .GetNamedItem("corporationName").Value
            End With

            ReturnData.Add(TempStation)

        Next

        Return ReturnData

    End Function

    ' Function gets the current EVE server time for singularity
    Public Function GetEVEServerTime(ByRef CachedUntilDate As Date) As Date
        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList

        Dim EVEAPIQuery As String

        Dim ReturnDate As Date

        ' Set up query string
        EVEAPIQuery = APIURL & EVEServerData

        'Create the XML Document
        m_xmld = QueryEVEAPI(EVEAPIQuery)

        ' Check data
        If IsNothing(m_xmld) Then
            Return Nothing
        End If

        ' Update the cache update to 24 hours from this query
        'CacheDate = DateAdd(DateInterval.Day, 1, Date.UtcNow)
        ' Get the cache update
        m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
        ' Should only be one time
        CachedUntilDate = CDate(m_nodelist.Item(0).InnerText)

        m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
        ReturnDate = CDate(m_nodelist.Item(0).InnerText)

        Return ReturnDate

    End Function

    ' Function gets all blueprints from the API for corp or account
    Public Function GetBlueprints(ByVal SentKey As APIKeyData, ByVal ScanType As ScanType, ByRef CacheDate As Date) As List(Of EVEBlueprint)
        '<eveapi version="2">
        '    <currentTime>2014-08-21 10:56:59</currentTime>
        '    <result>
        '        <rowset name="blueprints" key="itemID" columns="itemID,locationID,typeID,typeName,flagID,quantity,timeEfficiency,materialEfficiency,runs">
        '           <row itemID="1000000012211" locationID="60014929" typeID="33876" typeName="Prophecy Blood Raiders Edition Blueprint" flagID="4" quantity="-1" timeEfficiency="0" materialEfficiency="0" runs="-1"/>
        '           <row itemID="1000000029372" locationID="60014929" typeID="11568" typeName="Avatar Blueprint" flagID="4" quantity="497" timeEfficiency="0" materialEfficiency="0" runs="-1"/>
        '           <row itemID="1000000029375" locationID="60014929" typeID="11568" typeName="Avatar Blueprint" flagID="4" quantity="-1" timeEfficiency="0" materialEfficiency="10" runs="-1"/>
        '           <row itemID="1000000029377" locationID="60014929" typeID="33876" typeName="Prophecy Blood Raiders Edition Blueprint" flagID="4" quantity="-2" timeEfficiency="0" materialEfficiency="0" runs="20000"/>
        '        </rowset>
        '    </result>
        '    <cachedUntil>2014-08-21 10:57:59</cachedUntil>
        '</eveapi>

        ' XML Variables
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim EVEAPIQuery As String
        Dim TempBlueprint As EVEBlueprint
        Dim ReturnData As New List(Of EVEBlueprint)

        If SentKey.Access Then
            ' Set up query string
            If ScanType = ScanType.Personal Then
                EVEAPIQuery = APIURL & CharacterBlueprints & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey & "&characterID=" & CStr(SentKey.ID)
            Else ' Corp
                EVEAPIQuery = APIURL & CorporationBlueprints & "?keyID=" & CStr(SentKey.KeyID) & "&vCode=" & SentKey.APIKey
            End If

            'Create the XML Document
            m_xmld = QueryEVEAPI(EVEAPIQuery)

            ' Check data
            If IsNothing(m_xmld) Then
                ' Had an error, return nothing
                Return Nothing
            End If

            ' Get the cache update
            m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
            ' Should only be one time
            CacheDate = CDate(m_nodelist.Item(0).InnerText)

            ' Get the list of nodes for characters
            m_nodelist = m_xmld.SelectNodes("/eveapi/result/rowset/row")

            ' Loop through the nodes for three characters 
            ' if we are just doing the one, then it will exit with the one
            For Each m_node In m_nodelist
                With m_node.Attributes
                    TempBlueprint.itemID = CLng(.GetNamedItem("itemID").Value)
                    TempBlueprint.locationID = CLng(.GetNamedItem("locationID").Value)
                    TempBlueprint.typeID = CLng(.GetNamedItem("typeID").Value)
                    TempBlueprint.typeName = CStr(.GetNamedItem("typeName").Value)
                    TempBlueprint.flagID = CInt(.GetNamedItem("flagID").Value)
                    TempBlueprint.quantity = CInt(.GetNamedItem("quantity").Value)
                    TempBlueprint.timeEfficiency = CInt(.GetNamedItem("timeEfficiency").Value)
                    TempBlueprint.materialEfficiency = CInt(.GetNamedItem("materialEfficiency").Value)
                    TempBlueprint.runs = CInt(.GetNamedItem("runs").Value)
                    ' We determine the type of bp from quantity
                    If TempBlueprint.quantity = -1 Or TempBlueprint.quantity > 0 Then
                        ' BPO
                        TempBlueprint.BPType = BPType.Original
                    ElseIf TempBlueprint.quantity = -2 Then
                        TempBlueprint.BPType = BPType.Copy
                    Else
                        ' Not sure what this is
                        TempBlueprint.BPType = 0
                    End If
                    TempBlueprint.Owned = False
                    If TempBlueprint.BPType = BPType.Original Then
                        TempBlueprint.OwnedType = BPOwnedType.BPO
                    Else
                        TempBlueprint.OwnedType = BPOwnedType.BPC
                    End If

                    TempBlueprint.Scanned = True ' We just scanned it
                    TempBlueprint.Favorite = False
                    TempBlueprint.AdditionalCosts = 0
                End With

                ReturnData.Add(TempBlueprint)

            Next

            Return ReturnData

        Else
            CacheDate = Nothing
            Return Nothing
        End If

    End Function

    ' Function will take the API Query and send back the XML document for processing, If there is an error with the query, the function will set the error for the API object 
    Private Function QueryEVEAPI(ByVal EVEAPIQuery As String) As XmlDocument
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        On Error GoTo ErrorHandler

        'Create the XML Document
        m_xmld = New XmlDocument
        'Load the Xml file
        Call OverrideCertificateValidation()
        m_xmld.Load(EVEAPIQuery)

        ' Get the cache update
        m_nodelist = m_xmld.SelectNodes("/eveapi/cachedUntil")
        ' Should only be one time
        APIError.CacheDate = CDate(m_nodelist.Item(0).InnerText) ' Time here is in UTC

        ' First see if the authentication went through
        m_nodelist = m_xmld.SelectNodes("/eveapi/error")
        m_node = m_nodelist.Item(0)

        If Not IsNothing(m_node) Then
            ' Authentication Failed or some other error 
            APIError.ErrorCode = CInt(m_node.Attributes.GetNamedItem("code").Value)
            APIError.ErrorText = m_nodelist.Item(0).InnerText
            Return Nothing
        Else
            ' All good
            APIError.ErrorCode = 0
            APIError.ErrorText = ""
            Return m_xmld
        End If

ErrorHandler:
        ' A non-api error
        APIError.ErrorCode = Err.Number
        APIError.ErrorText = Err.Description
    End Function

    ' For https
    Public Shared Sub OverrideCertificateValidation()
        ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf RemoteCertValidate)
    End Sub

    ' For https
    Private Shared Function RemoteCertValidate(ByVal sender As Object, ByVal cert As X509Certificate, ByVal chain As X509Chain, ByVal [error] As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

End Class

' Special Variables
Public Enum AccessMaskBitLocs
    ' ACCESS_MASK Codes - this is a bitmask (max = 134217727)
    AccountBalance = 1
    AssetList = 2
    CalendarEventAttendees = 3
    CharacterSheet = 4
    ContactList = 5
    ContactNotifications = 6
    FacWarStats = 7
    IndustryJobs = 8
    KillLog = 9
    MailBodies = 10
    MailingLists = 11
    MailMessages = 12
    MarketOrders = 13
    Medals = 14
    Notifications = 15
    NotificationTexts = 16
    Research = 17
    SkillInTraining = 18
    SkillQueue = 19
    Standings = 20
    UpcomingCalendarEvents = 21
    WalletJournal = 22
    WalletTransactions = 23
    CharacterInfoPublic = 24
    CharacterInfoPrivate = 25
    AccountStatus = 26
    Contracts = 27
End Enum

Public Structure CurrentResearchAgent
    Dim agentID As Long
    Dim skillTypeID As Long
    Dim researchStartDate As Date ' Date in "yyyy-MM-dd hh:mm:ss" format
    Dim pointsPerDay As Double
    Dim remainderPoints As Double
End Structure

Public Class APIKeyData
    Public KeyID As Long
    Public APIKey As String
    Public Access As Boolean ' whether the data has access to query what we are running
    Public ID As Long ' Optional for corp keys

    Public Sub New()
        KeyID = 0
        APIKey = ""
        ID = 0
        Access = False
    End Sub

End Class

' For outputing a list of stations
Public Structure Station
    Dim stationID As Long
    Dim stationName As String
    Dim stationTypeID As Long
    Dim solarSystemID As Long
    Dim corporationID As Long
    Dim corporationName As String
End Structure