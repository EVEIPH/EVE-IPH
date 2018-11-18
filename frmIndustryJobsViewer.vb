
Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Threading

Public Class frmIndustryJobsViewer

    Private ColumnPositions(NumIndustryJobColumns) As String ' For saving the column order
    Private FirstLoad As Boolean
    Private Updating As Boolean
    Private AddingColumns As Boolean
    Private MovedColumn As Integer
    Private CurrentDateTime As Date
    Private UserIDToFind As Long
    Private myTimer As Timer

    Private LoadedCharacters As New List(Of IndyCharacter) ' The list of characters to show in the industry jobs list

    Private PauseScreenUpdate As Boolean

    Private JobListColumnClicked As Integer
    Private JobListColumnSortOrder As SortOrder
    Private AcctListColumnClicked As Integer
    Private AcctListColumnSortOrder As SortOrder

    Private Structure IndyCharacter
        Dim Token As SavedTokenData
        Dim Name As String
        Dim Corporation As String
        Dim IndustryLines As Integer
        Dim ResearchLines As Integer
        Dim ReactionLines As Integer
        Dim IndustryJobs As Integer
        Dim ResearchJobs As Integer
        Dim ReactionJobs As Integer
    End Structure

    Private Structure ColumnWidth
        Dim Name As String
        Dim Width As Integer
    End Structure

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FirstLoad = True
        Updating = False

        If UserApplicationSettings.ShowToolTips Then
            ttMain.SetToolTip(btnSaveSettings, "Saves Column order and Sort Column")
        End If

        CurrentDateTime = DateTime.UtcNow

        PauseScreenUpdate = False

        Dim myCallback As New TimerCallback(AddressOf UpdateTimes)
        myTimer = New Timer(myCallback, lstIndustryJobs, 1000, 1000)

        ' Width 725, 21 for scrollbar, 25 for check (679)
        lstCharacters.Columns.Add("", -2, HorizontalAlignment.Left)
        lstCharacters.Columns.Add("Character Name", 200, HorizontalAlignment.Left)
        lstCharacters.Columns.Add("Character Corporation", 230, HorizontalAlignment.Left)
        lstCharacters.Columns.Add("Industry Jobs", 83, HorizontalAlignment.Center)
        lstCharacters.Columns.Add("Research Jobs", 83, HorizontalAlignment.Center)
        lstCharacters.Columns.Add("Reaction Jobs", 83, HorizontalAlignment.Center)
        lstCharacters.Columns.Add("CharID", 0, HorizontalAlignment.Left) ' Hidden

        chkAutoUpdate.Checked = UserIndustryJobsColumnSettings.AutoUpdateJobs
        JobListColumnClicked = UserIndustryJobsColumnSettings.OrderByColumn
        If UserIndustryJobsColumnSettings.OrderType = "Ascending" Then
            JobListColumnSortOrder = SortOrder.Ascending
        Else
            JobListColumnSortOrder = SortOrder.Descending
        End If

        FirstLoad = False

    End Sub

    Private Sub frmIndustryJobsViewer_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim fAccessError As New frmAPIError

        FirstLoad = True

        ' See if they can load the jobs at all
        If Not SelectedCharacter.IndustryJobsAccess Then
            fAccessError.ErrorText = "Industry Jobs cannot be loaded for this character." &
                Environment.NewLine & Environment.NewLine & "Please ensure to include ESI access to industry jobs."
            fAccessError.Text = "ESI: No Industry Jobs Loaded"
            fAccessError.ErrorLink = ""
            fAccessError.ShowDialog()

            gbInventionJobs.Enabled = False
            Exit Sub
        Else
            gbInventionJobs.Enabled = True

            If SelectedCharacter.CharacterCorporation.JobsAccess Then
                rbtnBothJobs.Enabled = True
                rbtnCorpJobs.Enabled = True
            Else
                rbtnBothJobs.Enabled = False
                rbtnCorpJobs.Enabled = False
            End If
        End If

        If UserIndustryJobsColumnSettings.ViewJobType = rbtnPersonalJobs.Text Then
            rbtnPersonalJobs.Checked = True
        ElseIf UserIndustryJobsColumnSettings.ViewJobType = rbtnCorpJobs.Text And rbtnCorpJobs.Enabled Then
            rbtnCorpJobs.Checked = True
        ElseIf UserIndustryJobsColumnSettings.ViewJobType = rbtnBothJobs.Text And rbtnBothJobs.Enabled Then
            rbtnBothJobs.Checked = True
        Else
            rbtnPersonalJobs.Checked = True
        End If

        If UserIndustryJobsColumnSettings.JobTimes = rbtnCurrentJobs.Text Then
            rbtnCurrentJobs.Checked = True
        Else
            rbtnJobHistory.Checked = True
        End If

        ' Only update the API data if they selected it
        Call UpdateJobs(chkAutoUpdate.Checked)

        If FirstIndustryJobsViewerLoad Then
            ' Don't run this if they reopen again, make them manually update
            FirstIndustryJobsViewerLoad = False
        End If

        FirstLoad = False

    End Sub

    ' Updates all the jobs
    Private Sub UpdateJobs(ByVal RefreshAPI As Boolean)
        Updating = True

        ' Just refresh the char list and it will update from ESI
        Call RefreshCharacterList(RefreshAPI)
        Call RefreshGrid()

        If RefreshAPI Then
            MsgBox("Industry Jobs updated.", vbInformation, Application.ProductName)
        End If

        Updating = False

    End Sub

    ' Refreshes main grid with the industry jobs
    Private Sub RefreshGrid()
        Dim SQL As String
        Dim CHAR_ID_SQL As String = ""
        Dim rsJobs As SQLiteDataReader
        Dim lstJobRow As ListViewItem
        Dim JobState As String
        Dim JobStateColor As Color

        Dim StartDate As Date
        Dim EndDate As Date

        Dim Status As String = ""

        If rbtnCurrentJobs.Checked Then
            Me.Text = "Current Industry Jobs"
        Else
            Me.Text = "Historical Industry Jobs"
        End If

        Application.UseWaitCursor = True
        Me.Cursor = Cursors.WaitCursor
        gbInventionJobs.Enabled = False
        Application.DoEvents()

        ' If they don't select a character, then just clear and exit
        If lstCharacters.CheckedItems.Count = 0 Then
            lstIndustryJobs.Items.Clear()
            Exit Sub
        End If

        ' Find out what characters we are querying for
        If UserIndustryJobsColumnSettings.SelectedCharacterIDs = "" Then
            ' Just load the selected character since the data is already refreshed
            CHAR_ID_SQL = CHAR_ID_SQL & "AND installerID = " & SelectedCharacter.ID & " "
        Else
            ' Format this for multiple character ids that were saved
            CHAR_ID_SQL = CHAR_ID_SQL & "AND installerID IN ("
            For j = 0 To LoadedCharacters.Count - 1
                If UserIndustryJobsColumnSettings.SelectedCharacterIDs.Contains(CStr(LoadedCharacters(j).Token.CharacterID)) Then
                    CHAR_ID_SQL = CHAR_ID_SQL & CStr(LoadedCharacters(j).Token.CharacterID) & ","
                End If
            Next
            CHAR_ID_SQL = CHAR_ID_SQL.Substring(0, Len(CHAR_ID_SQL) - 1) & ")"
        End If

        ' Load up the charcters from the table (instead of objects) for speed and ease of loading
        SQL = "SELECT CHARACTER_NAME AS Installer, activityName AS Activity, ABS.BLUEPRINT_NAME as Blueprint, "
        SQL = SQL & "ABS.ITEM_NAME AS 'Output Item', status, startDate, endDate, ABS.ITEM_GROUP AS 'Output Item Type', "
        SQL = SQL & " CASE WHEN SOLAR_SYSTEMS.solarSystemName IS NOT NULL THEN SOLAR_SYSTEMS.solarSystemName ELSE 'Unknown System' END AS 'Install System', "
        SQL = SQL & "CASE WHEN REGIONS.regionName Is Not NULL THEN REGIONS.regionName ELSE 'Unknown Region' END AS 'Install Region',”
        SQL = SQL & "licensedRuns, runs, successfulRuns, "
        SQL = SQL & "CASE WHEN S1.STATION_NAME IS NOT NULL THEN S1.STATION_NAME ELSE "
        SQL = SQL & "(CASE WHEN C1.STATION_NAME IS NOT NULL THEN C1.STATION_NAME || ' Container' ELSE "
        SQL = SQL & "(CASE WHEN IT2.typeName IS NOT NULL THEN IT2.typeName ELSE "
        SQL = SQL & "(CASE WHEN S3.STATION_NAME IS NOT NULL THEN S3.STATION_NAME ELSE 'Unknown' END) END) END) END AS 'Blueprint Location', "
        SQL = SQL & "CASE WHEN S2.STATION_NAME IS NOT NULL THEN S2.STATION_NAME ELSE "
        SQL = SQL & "(CASE WHEN C2.STATION_NAME IS NOT NULL THEN C2.STATION_NAME || ' Container' ELSE "
        SQL = SQL & "(CASE WHEN IT3.typeName IS NOT NULL THEN IT3.typeName ELSE "
        SQL = SQL & "(CASE WHEN S3.STATION_NAME IS NOT NULL THEN S3.STATION_NAME ELSE 'Unknown' END) END) END) END AS 'Output Location', "
        SQL = SQL & "CASE WHEN jobType =1 THEN 'Corporation' ELSE 'Personal' END AS 'Job Type' "
        SQL = SQL & "FROM INDUSTRY_JOBS, RAM_ACTIVITIES, ESI_CHARACTER_DATA, ALL_BLUEPRINTS AS ABS "
        SQL = SQL & "LEFT JOIN STATIONS ON STATIONS.STATION_ID = INDUSTRY_JOBS.locationID “
        SQL = SQL & "LEFT JOIN SOLAR_SYSTEMS ON STATIONS.SOLAR_SYSTEM_ID = SOLAR_SYSTEMS.solarSystemID “
        SQL = SQL & "LEFT JOIN REGIONS ON SOLAR_SYSTEMS.regionID = REGIONS.regionID “

        ' Stations
        SQL = SQL & "LEFT OUTER JOIN (SELECT STATION_ID, STATION_NAME FROM STATIONS) AS S1 ON S1.STATION_ID = INDUSTRY_JOBS.blueprintLocationID "
        SQL = SQL & "LEFT OUTER JOIN (SELECT STATION_ID, STATION_NAME FROM STATIONS) AS S2 ON S2.STATION_ID = INDUSTRY_JOBS.outputLocationID "
        SQL = SQL & "LEFT OUTER JOIN (SELECT STATION_ID, STATION_NAME FROM STATIONS) AS S3 ON S3.STATION_ID = INDUSTRY_JOBS.locationID "
        ' Containers in stations
        SQL = SQL & "LEFT OUTER JOIN (SELECT STATION_ID, STATION_NAME, A1.ItemID FROM STATIONS LEFT OUTER JOIN (SELECT LocationID, ItemID FROM ASSETS WHERE ID = " & SelectedCharacter.ID & ") "
        SQL = SQL & "AS A1 ON A1.LocationID = STATION_ID) AS C1 ON C1.ItemID = INDUSTRY_JOBS.blueprintLocationID "
        SQL = SQL & "LEFT OUTER JOIN (SELECT STATION_ID, STATION_NAME, A2.ItemID FROM STATIONS LEFT OUTER JOIN (SELECT LocationID, ItemID FROM ASSETS WHERE ID = " & SelectedCharacter.ID & ") "
        SQL = SQL & "AS A2 ON A2.LocationID = STATION_ID) AS C2 ON C2.ItemID = INDUSTRY_JOBS.blueprintLocationID "
        ' POS modules
        SQL = SQL & "LEFT OUTER JOIN (SELECT typeID, typeName FROM INVENTORY_TYPES) AS IT2 ON IT2.typeID = INDUSTRY_JOBS.blueprintLocationID "
        SQL = SQL & "LEFT OUTER JOIN (SELECT typeID, typeName FROM INVENTORY_TYPES) AS IT3 ON IT3.typeID = INDUSTRY_JOBS.outputLocationID "

        SQL = SQL & "WHERE INDUSTRY_JOBS.activityID = RAM_ACTIVITIES.activityID  "
        SQL = SQL & "AND INDUSTRY_JOBS.blueprintTypeID = ABS.BLUEPRINT_ID "
        SQL = SQL & "AND ESI_CHARACTER_DATA.CHARACTER_ID = INDUSTRY_JOBS.installerID "

        If rbtnCurrentJobs.Checked Then
            ' Only check status for current jobs
            SQL = SQL & "AND status = 'active' "
        End If

        ' Add the charids
        SQL = SQL & CHAR_ID_SQL

        ' For both just ignore the selections
        If rbtnCorpJobs.Checked Then
            SQL = SQL & "AND JobType = " & CStr(ScanType.Corporation) & " "
        ElseIf rbtnPersonalJobs.Checked Then
            SQL = SQL & CHAR_ID_SQL & "AND JobType = " & CStr(ScanType.Personal) & " "
        End If

        ' Add sorting options here
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsJobs = DBCommand.ExecuteReader

        lstIndustryJobs.BeginUpdate()
        Call RefreshColumns()

        While rsJobs.Read
            Application.DoEvents()

            Status = rsJobs.GetString(4)
            StartDate = CDate(rsJobs.GetString(5))
            EndDate = CDate(rsJobs.GetString(6))

            'active, cancelled, delivered, paused, ready, reverted
            Select Case Status
                Case "active"
                    ' Job State Flag
                    If EndDate <= CurrentDateTime Then
                        ' Job is done
                        JobState = "Complete"
                        JobStateColor = Color.Green
                    ElseIf StartDate <= CurrentDateTime Then
                        JobState = "In Progress"
                        JobStateColor = Color.DarkOrange
                    Else
                        JobState = "Pending"
                        JobStateColor = Color.Orange
                    End If
                Case "delivered"
                    ' This has been completed
                    JobState = "Completed"
                    JobStateColor = Color.DarkGray
                Case "cancelled"
                    JobState = "Cancelled"
                    JobStateColor = Color.Red
                Case "paused"
                    JobState = "Paused"
                    JobStateColor = Color.Gray
                Case "ready"
                    JobState = "Ready"
                    JobStateColor = Color.Blue
                Case "reverted"
                    JobState = "Reverted"
                    JobStateColor = Color.Purple
                Case Else
                    JobState = "Unknown"
                    JobStateColor = Color.DarkRed
            End Select

            lstIndustryJobs.ListViewItemSorter = Nothing
            ' Always add the end time to column 0 for sorting 
            lstJobRow = New ListViewItem(rsJobs.GetString(6))
            lstJobRow.UseItemStyleForSubItems = False

            With UserIndustryJobsColumnSettings
                For i = 1 To ColumnPositions.Count - 1
                    Select Case ColumnPositions(i)
                        Case ProgramSettings.JobStateColumn
                            lstJobRow.SubItems.Add(JobState) ' Job State
                            lstJobRow.SubItems(Array.IndexOf(ColumnPositions, "Job State")).ForeColor = JobStateColor
                        Case ProgramSettings.InstallerNameColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(0))
                        Case ProgramSettings.TimetoCompleteColumn
                            If JobState <> "Complete" And JobState <> "Completed" Then
                                lstJobRow.SubItems.Add(GetTimeToComplete(EndDate, CurrentDateTime))
                            Else
                                lstJobRow.SubItems.Add("")
                            End If
                        Case ProgramSettings.ActivityColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(1))
                        Case ProgramSettings.StatusColumn
                            If rsJobs.GetString(4) = "active" Then
                                lstJobRow.SubItems.Add("Delivered")
                            Else
                                If JobState = "Completed" Then
                                    lstJobRow.SubItems.Add("Ready for Delivery")
                                Else
                                    lstJobRow.SubItems.Add("In Progress")
                                End If
                            End If
                        Case ProgramSettings.StartTimeColumn
                            lstJobRow.SubItems.Add(CStr(StartDate))
                        Case ProgramSettings.EndTimeColumn
                            lstJobRow.SubItems.Add(CStr(EndDate))
                        Case ProgramSettings.CompletionTimeColumn
                            If JobState = "Completed" Then
                                lstJobRow.SubItems.Add(rsJobs.GetString(4))
                            Else
                                lstJobRow.SubItems.Add("")
                            End If
                        Case ProgramSettings.BlueprintColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(2))
                        Case ProgramSettings.OutputItemColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(3))
                        Case ProgramSettings.OutputItemTypeColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(7))
                        Case ProgramSettings.InstallSolarSystemColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(8))
                        Case ProgramSettings.InstallRegionColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(9))
                        Case ProgramSettings.LicensedRunsColumn
                            lstJobRow.SubItems.Add(CStr(rsJobs.GetInt32(10)))
                        Case ProgramSettings.RunsColumn
                            lstJobRow.SubItems.Add(CStr(rsJobs.GetInt32(11)))
                        Case ProgramSettings.SuccessfulRunsColumn
                            lstJobRow.SubItems.Add(CStr(rsJobs.GetInt32(12)))
                        Case ProgramSettings.BlueprintLocationColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(13))
                        Case ProgramSettings.OutputLocationColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(14))
                        Case ProgramSettings.JobTypeColumn
                            lstJobRow.SubItems.Add(rsJobs.GetString(15))
                    End Select
                Next
            End With

            Call lstIndustryJobs.Items.Add(lstJobRow)

        End While

        lstIndustryJobs.EndUpdate()

        ' Force last sort order to switch to ascending and sort by the user column
        Call ListViewColumnSorter(UserIndustryJobsColumnSettings.OrderByColumn, lstIndustryJobs, JobListColumnClicked, JobListColumnSortOrder, True)

        Application.UseWaitCursor = False
        gbInventionJobs.Enabled = True
        Me.Cursor = Cursors.Default
        Application.DoEvents()

    End Sub

    ' Refreshes the user grid with all users in the DB
    Private Sub RefreshCharacterList(ByVal UpdateAPIData As Boolean)
        Dim lstCharacterRow As ListViewItem
        Dim SQL As String
        Dim rsJobs As SQLiteDataReader

        Application.UseWaitCursor = True
        'Me.Cursor = Cursors.WaitCursor
        gbInventionJobs.Enabled = False
        Application.DoEvents()

        ' Update the data for each character first if selected
        If UpdateAPIData Then
            Call UpdateLoadedCharacterData()
        End If

        ' Now jobs and skills should show correctly
        SQL = "SELECT CHARACTER_NAME, CORPORATION_NAME, CHARACTER_ID, "
        SQL = SQL & "ACCESS_TOKEN, ACCESS_TOKEN_EXPIRE_DATE_TIME, REFRESH_TOKEN, TOKEN_TYPE, SCOPES, "
        SQL = SQL & "CASE WHEN RESEARCH_JOBS IS NULL THEN 0 ELSE RESEARCH_JOBS END AS RESEARCH_JOBS, "
        SQL = SQL & "CASE WHEN RESEARCH_LINES IS NULL THEN 1 ELSE RESEARCH_LINES END AS RESEARCH_LINES, "
        SQL = SQL & "CASE WHEN JOB_COUNT IS NULL THEN 0 ELSE JOB_COUNT END AS JOB_COUNT, "
        SQL = SQL & "CASE WHEN INDUSTRY_LINES IS NULL THEN 1 ELSE INDUSTRY_LINES END AS INDUSTRY_LINES, "
        SQL = SQL & "CASE WHEN REACTION_JOBS IS NULL THEN 0 ELSE REACTION_JOBS END AS REACTION_JOBS, "
        SQL = SQL & "CASE WHEN REACTION_LINES IS NULL THEN 1 ELSE REACTION_LINES END AS REACTION_LINES "
        SQL = SQL & "FROM ESI_CHARACTER_DATA AS ECD, ESI_CORPORATION_DATA AS ECPD "
        SQL = SQL & "LEFT JOIN (SELECT SUM(SKILL_LEVEL) + 1 AS RESEARCH_LINES, CHARACTER_ID AS CHAR_ID FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID IN (3406,24624) GROUP BY CHARACTER_ID) AS I ON I.CHAR_ID = ECD.CHARACTER_ID "
        SQL = SQL & "LEFT JOIN (SELECT installerID, COUNT(*) AS RESEARCH_JOBS FROM INDUSTRY_JOBS WHERE STATUS = 'active' AND activityID NOT IN (1,11) GROUP BY installerID) AS J ON J.installerID = CHARACTER_ID "
        SQL = SQL & "LEFT JOIN (SELECT SUM(SKILL_LEVEL) + 1 AS INDUSTRY_LINES, CHARACTER_ID AS CHAR_ID FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID IN (3387,24625) GROUP BY CHARACTER_ID) AS K ON K.CHAR_ID = ECD.CHARACTER_ID "
        SQL = SQL & "LEFT JOIN (SELECT installerID, COUNT(*) AS JOB_COUNT FROM INDUSTRY_JOBS WHERE STATUS = 'active' AND activityID = 1 GROUP BY installerID) AS L ON L.installerID = CHARACTER_ID "
        SQL = SQL & "LEFT JOIN (SELECT SUM(SKILL_LEVEL) + 1 AS REACTION_LINES, CHARACTER_ID AS CHAR_ID FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID IN (45748,45749) GROUP BY CHARACTER_ID) AS M ON M.CHAR_ID = ECD.CHARACTER_ID "
        SQL = SQL & "LEFT JOIN (SELECT installerID, COUNT(*) AS REACTION_JOBS FROM INDUSTRY_JOBS WHERE STATUS = 'active' AND activityID = 11 GROUP BY installerID) AS N ON N.installerID = CHARACTER_ID "
        SQL &= "WHERE ECD.CORPORATION_ID = ECPD.CORPORATION_ID AND ECD.CHARACTER_ID <> " & CStr(DummyCharacterID)

        ' Get all the characters and store them regardless so we only need to do one look up
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsJobs = DBCommand.ExecuteReader

        LoadedCharacters = New List(Of IndyCharacter)

        While rsJobs.Read
            ' Load up the data for this character id in the list and check it
            Dim TempToken As New SavedTokenData
            TempToken.CharacterID = rsJobs.GetInt32(2)
            TempToken.AccessToken = rsJobs.GetString(3)
            TempToken.TokenExpiration = CDate(rsJobs.GetString(4))
            TempToken.RefreshToken = rsJobs.GetString(5)
            TempToken.TokenType = rsJobs.GetString(6)
            TempToken.Scopes = rsJobs.GetString(7)

            Dim TempCharacter As IndyCharacter

            TempCharacter.Token = TempToken
            TempCharacter.Name = rsJobs.GetString(0)
            TempCharacter.Corporation = rsJobs.GetString(1)

            ' Industry Runs and lines
            TempCharacter.ResearchJobs = rsJobs.GetInt32(8)
            TempCharacter.ResearchLines = rsJobs.GetInt32(9)
            TempCharacter.IndustryJobs = rsJobs.GetInt32(10)
            TempCharacter.IndustryLines = rsJobs.GetInt32(11)
            TempCharacter.ReactionJobs = rsJobs.GetInt32(12)
            TempCharacter.ReactionLines = rsJobs.GetInt32(13)

            ' Add this to the list
            If Not LoadedCharacters.Contains(TempCharacter) Then
                LoadedCharacters.Add(TempCharacter)
            End If

            Application.DoEvents()

        End While

        lstCharacters.BeginUpdate()
        lstCharacters.Items.Clear()

        For i = 0 To LoadedCharacters.Count - 1
            Application.DoEvents()
            With LoadedCharacters(i)
                lstCharacterRow = New ListViewItem("") ' Check
                lstCharacterRow.SubItems.Add(.Name) ' Name
                lstCharacterRow.SubItems.Add(.Corporation)

                ' Add the jobs as part of lines i.e 4/10 = 4 jobs of 10 lines
                lstCharacterRow.SubItems.Add(CStr(.IndustryJobs) & "/" & CStr(.IndustryLines))
                lstCharacterRow.SubItems.Add(CStr(.ResearchJobs) & "/" & CStr(.ResearchLines))
                lstCharacterRow.SubItems.Add(CStr(.ReactionJobs) & "/" & CStr(.ReactionLines))

                ' Add the hidden character ID
                Dim CharacterID As String = CStr(.Token.CharacterID)
                lstCharacterRow.SubItems.Add(CharacterID)

                If UserIndustryJobsColumnSettings.SelectedCharacterIDs.Contains(CharacterID) Then
                    ' In the list so check it
                    lstCharacterRow.Checked = True

                ElseIf UserIndustryJobsColumnSettings.SelectedCharacterIDs = "" And .Name = SelectedCharacter.Name Then
                    ' Empty list of selected chars and this is the same as the one we pulled
                    lstCharacterRow.Checked = True
                End If
            End With

            Call lstCharacters.Items.Add(lstCharacterRow)

        Next

        lstCharacters.EndUpdate()

        gbInventionJobs.Enabled = True
        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

    ' Updates the industry jobs and skills for the characters in the list
    Private Sub UpdateLoadedCharacterData()
        Dim f1 As New frmStatus
        Dim SQL As String
        Dim rsCharacters As SQLiteDataReader

        Dim TempCharacter As New Character
        Dim TokenData As New SavedTokenData

        f1.lblStatus.Text = "Updating Character data..."
        f1.Show()
        Application.UseWaitCursor = True
        Application.DoEvents()

        ' Pause updates if we are using the update jobs button
        PauseScreenUpdate = True

        SQL = "SELECT CHARACTER_ID FROM ESI_CHARACTER_DATA WHERE CHARACTER_ID <> " & CStr(DummyCharacterID)
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsCharacters = DBCommand.ExecuteReader

        ' Get all the character ids loaded and refresh them on skills and jobs
        While rsCharacters.Read
            TempCharacter = New Character
            TokenData.CharacterID = CInt(rsCharacters.GetInt32(0))
            ' Loading a character will update the jobs and skills
            TempCharacter.LoadCharacterData(TokenData, False, False, True)
            Application.DoEvents()
        End While

        PauseScreenUpdate = False

        f1.Dispose()
        f1 = Nothing
        Me.Select()
        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

    Private Function GetCharIDs() As String
        Dim CharIDs As String = ""

        For i = 0 To lstCharacters.CheckedItems.Count - 1
            CharIDs = CharIDs & CStr(lstCharacters.CheckedItems(i).SubItems(5).Text) & ","
        Next

        ' Strip the last comma
        If CharIDs <> "" Then
            CharIDs = CharIDs.Substring(0, Len(CharIDs) - 1)
        End If

        Return CharIDs

    End Function

    Private Delegate Sub ListDelegate(ByVal myJobList As ListView)

    Private Sub UpdateTimes(ByVal sentJobList As Object)
        Dim TimeToComplete As String
        Dim EndDate As Date

        If PauseScreenUpdate Then
            Exit Sub
        End If

        Dim myJobList As New ListView
        myJobList = DirectCast(sentJobList, ListView)

        If myJobList.InvokeRequired Then
            myJobList.Invoke(New ListDelegate(AddressOf UpdateTimes), myJobList)
            Exit Sub
        End If

        Try
            ' On each tick just update the time column manually
            With UserIndustryJobsColumnSettings
                If .TimeToComplete <> 0 Then ' only if the time to complete column is visible
                    CurrentDateTime = DateAdd(DateInterval.Second, 1, CurrentDateTime)
                    Application.DoEvents()

                    For i = 0 To myJobList.Items.Count - 1
                        ' Only update records with a time
                        If myJobList.Items(i).SubItems(.JobState).Text <> "Complete" And myJobList.Items(i).SubItems(.JobState).Text <> "Completed" Then

                            EndDate = CDate(myJobList.Items(i).SubItems(0).Text)
                            TimeToComplete = GetTimeToComplete(EndDate, CurrentDateTime)

                            ' If the time comes back negative, then switch it to blank and reset the job state to 'Complete'
                            If TimeToComplete = "" Then
                                TimeToComplete = "0"
                            End If

                            If TimeToComplete.Substring(0, 1) = "-" Or TimeToComplete = "0" Then
                                myJobList.Items(i).SubItems(.TimeToComplete).Text = ""
                                myJobList.Items(i).SubItems(.JobState).Text = "Complete"
                                myJobList.Items(i).SubItems(.JobState).ForeColor = Color.Green
                            Else
                                myJobList.Items(i).SubItems(.TimeToComplete).Text = TimeToComplete
                            End If

                            myJobList.Update()
                            Application.DoEvents()
                        End If
                    Next
                End If
            End With
        Catch ex As Exception
            ' Don't do anything, just let it update next go around
        End Try

        Application.DoEvents()

    End Sub

    Private Function GetTimeToComplete(EndJobDate As Date, CompareDate As Date) As String
        Dim SecondsDiff As Long

        SecondsDiff = DateDiff(DateInterval.Second, CompareDate, EndJobDate)

        Return FormatTimeToComplete(SecondsDiff)

    End Function

#Region "Click events"

    ' Clears the list and rebuilds it with columns they selected
    Private Sub RefreshColumns()

        Call LoadIndustryJobColumnPositions()

        Call lstIndustryJobs.Clear()
        AddingColumns = True

        ' Add an empty Column
        lstIndustryJobs.Columns.Add("")

        ' Now load all the columns in order of the settings
        For i = 1 To ColumnPositions.Count - 1
            If ColumnPositions(i) <> "" Then
                lstIndustryJobs.Columns.Add(ColumnPositions(i), GetColumnWidth(ColumnPositions(i)), GetColumnAlignment(ColumnPositions(i)))
            End If
        Next

        ' Empty Column not visible
        lstIndustryJobs.Columns(0).Width = 0

        AddingColumns = False

    End Sub

    ' Takes the column settings and saves the order to an array
    Private Sub LoadIndustryJobColumnPositions()

        For i = 0 To ColumnPositions.Count - 1
            ColumnPositions(i) = ""
        Next

        With UserIndustryJobsColumnSettings
            ColumnPositions(.JobState) = ProgramSettings.JobStateColumn
            ColumnPositions(.InstallerName) = ProgramSettings.InstallerNameColumn
            ColumnPositions(.TimeToComplete) = ProgramSettings.TimetoCompleteColumn
            ColumnPositions(.Activity) = ProgramSettings.ActivityColumn
            ColumnPositions(.Status) = ProgramSettings.StatusColumn
            ColumnPositions(.StartTime) = ProgramSettings.StartTimeColumn
            ColumnPositions(.EndTime) = ProgramSettings.EndTimeColumn
            ColumnPositions(.CompletionTime) = ProgramSettings.CompletionTimeColumn
            ColumnPositions(.Blueprint) = ProgramSettings.BlueprintColumn
            ColumnPositions(.OutputItem) = ProgramSettings.OutputItemColumn
            ColumnPositions(.OutputItemType) = ProgramSettings.OutputItemTypeColumn
            ColumnPositions(.InstallSystem) = ProgramSettings.InstallSolarSystemColumn
            ColumnPositions(.InstallRegion) = ProgramSettings.InstallRegionColumn
            ColumnPositions(.LicensedRuns) = ProgramSettings.LicensedRunsColumn
            ColumnPositions(.Runs) = ProgramSettings.RunsColumn
            ColumnPositions(.SuccessfulRuns) = ProgramSettings.SuccessfulRunsColumn
            ColumnPositions(.BlueprintLocation) = ProgramSettings.BlueprintLocationColumn
            ColumnPositions(.OutputLocation) = ProgramSettings.OutputLocationColumn
            ColumnPositions(.JobType) = ProgramSettings.JobTypeColumn
        End With

        ' Reset the first one with nothing since the first column is empty
        ColumnPositions(0) = ""

    End Sub

    ' Returns the column Width for the sent column name
    Private Function GetColumnWidth(ColumnName As String) As Integer

        With UserIndustryJobsColumnSettings
            Select Case ColumnName
                Case ProgramSettings.JobStateColumn
                    Return .JobStateWidth
                Case ProgramSettings.InstallerNameColumn
                    Return .InstallerNameWidth
                Case ProgramSettings.TimetoCompleteColumn
                    Return .TimeToCompleteWidth
                Case ProgramSettings.ActivityColumn
                    Return .ActivityWidth
                Case ProgramSettings.StatusColumn
                    Return .StatusWidth
                Case ProgramSettings.StartTimeColumn
                    Return .StartTimeWidth
                Case ProgramSettings.EndTimeColumn
                    Return .EndTimeWidth
                Case ProgramSettings.CompletionTimeColumn
                    Return .CompletionTimeWidth
                Case ProgramSettings.BlueprintColumn
                    Return .BlueprintWidth
                Case ProgramSettings.OutputItemColumn
                    Return .OutputItemWidth
                Case ProgramSettings.OutputItemTypeColumn
                    Return .OutputItemTypeWidth
                Case ProgramSettings.InstallSolarSystemColumn
                    Return .InstallSystemWidth
                Case ProgramSettings.InstallRegionColumn
                    Return .InstallRegionWidth
                Case ProgramSettings.LicensedRunsColumn
                    Return .LicensedRunsWidth
                Case ProgramSettings.RunsColumn
                    Return .RunsWidth
                Case ProgramSettings.SuccessfulRunsColumn
                    Return .SuccessfulRunsWidth
                Case ProgramSettings.BlueprintLocationColumn
                    Return .BlueprintLocationWidth
                Case ProgramSettings.OutputLocationColumn
                    Return .OutputLocationWidth
                Case ProgramSettings.JobTypeColumn
                    Return .JobTypeWidth
                Case Else
                    Return 0
            End Select
        End With

    End Function

    ' Returns the allignment for the column name sent
    Private Function GetColumnAlignment(ColumnName As String) As System.Windows.Forms.HorizontalAlignment

        Select Case ColumnName
            Case ProgramSettings.JobStateColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.InstallerNameColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.TimetoCompleteColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.ActivityColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.StartTimeColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.EndTimeColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.CompletionTimeColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.BlueprintColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.OutputItemColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.OutputItemTypeColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.InstallSolarSystemColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.InstallRegionColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.LicensedRunsColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.RunsColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.SuccessfulRunsColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.BlueprintLocationColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.OutputLocationColumn
                Return HorizontalAlignment.Left
            Case ProgramSettings.JobTypeColumn
                Return HorizontalAlignment.Left
            Case Else
                Return 0
        End Select

    End Function

    ' Updates the column order when changed
    Private Sub lstIndustryJobs_ColumnReordered(sender As Object, e As System.Windows.Forms.ColumnReorderedEventArgs) Handles lstIndustryJobs.ColumnReordered
        Dim TempArray(NumIndustryJobColumns) As String
        Dim Minus1 As Boolean = False

        e.Cancel = True ' Cancel the event so we can manually update the grid columns

        ' First index is ""
        TempArray(0) = ""

        If e.OldDisplayIndex > e.NewDisplayIndex Then
            ' For all indices larger than the new index, need to move it to the next array
            For i = 1 To e.NewDisplayIndex - 1
                TempArray(i) = ColumnPositions(i)
            Next

            ' Insert the new column
            TempArray(e.NewDisplayIndex) = ColumnPositions(e.OldDisplayIndex)

            ' Move all the rest of the items up one
            For i = e.NewDisplayIndex + 1 To TempArray.Count - 1
                If i < e.OldDisplayIndex + 1 Then
                    TempArray(i) = ColumnPositions(i - 1)
                Else
                    TempArray(i) = ColumnPositions(i)
                End If
            Next
        Else
            ' For all indices larger than the new index, need to move it to the next array
            For i = 1 To e.OldDisplayIndex - 1
                TempArray(i) = ColumnPositions(i)
            Next

            ' Insert the new column
            TempArray(e.NewDisplayIndex) = ColumnPositions(e.OldDisplayIndex)

            ' Back fill the array between the column we moved and the new location
            For i = e.OldDisplayIndex To e.NewDisplayIndex - 1
                TempArray(i) = ColumnPositions(i + 1)
            Next

            ' Replace all the items left
            For i = e.NewDisplayIndex + 1 To TempArray.Count - 1
                TempArray(i) = ColumnPositions(i)
            Next

        End If

        ColumnPositions = TempArray

        ' Savel the columns based on the current order
        With UserIndustryJobsColumnSettings
            For i = 1 To ColumnPositions.Count - 1
                Select Case ColumnPositions(i)
                    Case ProgramSettings.JobStateColumn
                        .JobState = i
                    Case ProgramSettings.InstallerNameColumn
                        .InstallerName = i
                    Case ProgramSettings.TimetoCompleteColumn
                        .TimeToComplete = i
                    Case ProgramSettings.ActivityColumn
                        .Activity = i
                    Case ProgramSettings.StatusColumn
                        .Status = i
                    Case ProgramSettings.StartTimeColumn
                        .StartTime = i
                    Case ProgramSettings.EndTimeColumn
                        .EndTime = i
                    Case ProgramSettings.CompletionTimeColumn
                        .CompletionTime = i
                    Case ProgramSettings.BlueprintColumn
                        .Blueprint = i
                    Case ProgramSettings.OutputItemColumn
                        .OutputItem = i
                    Case ProgramSettings.OutputItemTypeColumn
                        .OutputItemType = i
                    Case ProgramSettings.InstallSolarSystemColumn
                        .InstallSystem = i
                    Case ProgramSettings.InstallRegionColumn
                        .InstallRegion = i
                    Case ProgramSettings.LicensedRunsColumn
                        .LicensedRuns = i
                    Case ProgramSettings.RunsColumn
                        .Runs = i
                    Case ProgramSettings.SuccessfulRunsColumn
                        .SuccessfulRuns = i
                    Case ProgramSettings.BlueprintLocationColumn
                        .BlueprintLocation = i
                    Case ProgramSettings.OutputLocationColumn
                        .OutputLocation = i
                    Case ProgramSettings.JobTypeColumn
                        .JobType = i
                End Select
            Next
        End With

        ' Now Refresh the grid
        Call RefreshGrid()

    End Sub

    ' Updates the column sizes when changed
    Private Sub lstIndustryJobs_ColumnWidthChanged(sender As Object, e As System.Windows.Forms.ColumnWidthChangedEventArgs) Handles lstIndustryJobs.ColumnWidthChanged
        Dim NewWidth As Integer = lstIndustryJobs.Columns(e.ColumnIndex).Width

        If Not AddingColumns Then
            With UserIndustryJobsColumnSettings
                Select Case ColumnPositions(e.ColumnIndex)
                    Case ProgramSettings.JobStateColumn
                        .JobStateWidth = NewWidth
                    Case ProgramSettings.InstallerNameColumn
                        .InstallerNameWidth = NewWidth
                    Case ProgramSettings.TimetoCompleteColumn
                        .TimeToCompleteWidth = NewWidth
                    Case ProgramSettings.ActivityColumn
                        .ActivityWidth = NewWidth
                    Case ProgramSettings.StatusColumn
                        .StatusWidth = NewWidth
                    Case ProgramSettings.StartTimeColumn
                        .StartTimeWidth = NewWidth
                    Case ProgramSettings.EndTimeColumn
                        .EndTimeWidth = NewWidth
                    Case ProgramSettings.CompletionTimeColumn
                        .CompletionTimeWidth = NewWidth
                    Case ProgramSettings.BlueprintColumn
                        .BlueprintWidth = NewWidth
                    Case ProgramSettings.OutputItemColumn
                        .OutputItemWidth = NewWidth
                    Case ProgramSettings.OutputItemTypeColumn
                        .OutputItemTypeWidth = NewWidth
                    Case ProgramSettings.InstallSolarSystemColumn
                        .InstallSystemWidth = NewWidth
                    Case ProgramSettings.InstallRegionColumn
                        .InstallRegionWidth = NewWidth
                    Case ProgramSettings.LicensedRunsColumn
                        .LicensedRunsWidth = NewWidth
                    Case ProgramSettings.RunsColumn
                        .RunsWidth = NewWidth
                    Case ProgramSettings.SuccessfulRunsColumn
                        .SuccessfulRunsWidth = NewWidth
                    Case ProgramSettings.BlueprintLocationColumn
                        .BlueprintLocationWidth = NewWidth
                    Case ProgramSettings.OutputLocationColumn
                        .OutputLocationWidth = NewWidth
                    Case ProgramSettings.JobTypeColumn
                        .JobTypeWidth = NewWidth
                End Select
            End With
        End If

    End Sub

    ' Determines if we display the sent colum
    Private Function ShowColumn(ColumnName As String) As Boolean
        If Array.IndexOf(ColumnPositions, ColumnName) <> -1 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Save the column order, the column size and the sort order
    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click

        If rbtnPersonalJobs.Checked Then
            UserIndustryJobsColumnSettings.ViewJobType = rbtnPersonalJobs.Text
        ElseIf rbtnCorpJobs.Checked Then
            UserIndustryJobsColumnSettings.ViewJobType = rbtnCorpJobs.Text
        ElseIf rbtnBothJobs.Checked Then
            UserIndustryJobsColumnSettings.ViewJobType = rbtnBothJobs.Text
        End If

        If rbtnCurrentJobs.Checked Then
            UserIndustryJobsColumnSettings.JobTimes = rbtnCurrentJobs.Text
        Else
            UserIndustryJobsColumnSettings.JobTimes = rbtnJobHistory.Text
        End If

        UserIndustryJobsColumnSettings.SelectedCharacterIDs = GetCharIDs()
        UserIndustryJobsColumnSettings.AutoUpdateJobs = chkAutoUpdate.Checked

        AllSettings.SaveIndustryJobsColumnSettings(UserIndustryJobsColumnSettings)

        MsgBox("Settings saved", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnSelectColumns_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectColumns.Click
        Dim f1 As New frmSelectIndustryJobColumns
        f1.ShowDialog()

        ' And refresh the Grid
        Call RefreshGrid()

    End Sub

    Private Sub btnUpdateJobs_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateJobs.Click

        ' Always update the data when pushed
        Call UpdateJobs(True)

    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        'Timer1.Enabled = False
        Me.Hide()
    End Sub

    Private Sub lstIndustryJobs_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstIndustryJobs.ColumnClick
        Call ListViewColumnSorter(e.Column, lstIndustryJobs, JobListColumnClicked, JobListColumnSortOrder)
        UserIndustryJobsColumnSettings.OrderByColumn = JobListColumnClicked
        If JobListColumnSortOrder = SortOrder.Ascending Then
            UserIndustryJobsColumnSettings.OrderType = "Ascending"
        Else
            UserIndustryJobsColumnSettings.OrderType = "Decending"
        End If
    End Sub

    Private Sub lstCharacters_ColumnClick(sender As System.Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstCharacters.ColumnClick
        Call ListViewColumnSorter(e.Column, CType(lstCharacters, ListView), AcctListColumnClicked, AcctListColumnSortOrder)
    End Sub

    Private Sub btnRefreshList_Click(sender As System.Object, e As System.EventArgs) Handles btnRefreshList.Click
        Call RefreshGrid()
    End Sub

    Private Sub rbtnPersonalJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnPersonalJobs.CheckedChanged
        If Not FirstLoad Then
            Call RefreshGrid()
        End If
    End Sub

    Private Sub rbtnCorpJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCorpJobs.CheckedChanged
        If Not FirstLoad Then
            Call RefreshGrid()
        End If
    End Sub

    Private Sub rbtnBothJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnBothJobs.CheckedChanged
        If Not FirstLoad Then
            Call RefreshGrid()
        End If
    End Sub

    Private Sub rbtnCurrentJobs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnCurrentJobs.CheckedChanged
        If Not FirstLoad And rbtnCurrentJobs.Checked Then
            Call RefreshGrid()
        End If
    End Sub

    Private Sub rbtnJobHistory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtnJobHistory.CheckedChanged
        If Not FirstLoad And rbtnJobHistory.Checked Then
            Call RefreshGrid()
        End If
    End Sub

    Private Sub chkAutoUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoUpdate.CheckedChanged
        UserIndustryJobsColumnSettings.AutoUpdateJobs = chkAutoUpdate.Checked
    End Sub

    Private Sub lstCharacters_ItemChecked(sender As System.Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstCharacters.ItemChecked
        ' Just set up the list of char ids
        If Not FirstLoad And Not Updating Then
            UserIndustryJobsColumnSettings.SelectedCharacterIDs = GetCharIDs()
            ' Now refresh the grid
            RefreshGrid()
        End If

    End Sub

    Private Sub frmIndustryJobsViewer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        PauseScreenUpdate = True
        myTimer.Dispose()
    End Sub

    Protected Overrides Sub Finalize()
        PauseScreenUpdate = True
        myTimer.Dispose()
        MyBase.Finalize()
    End Sub

#End Region

End Class
