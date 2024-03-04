
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Globalization ' For culture info
Imports System.Threading

Delegate Sub UpdateStatusSafe(ByVal pgBarVisible As Boolean, ByVal lblText As String)
Delegate Sub UpdatePGBarSafe(ByVal pgBarValue As Integer)

Public Class FrmUpdaterMain

    Public Structure FileEntry
        Dim Name As String
        Dim Version As String
        Dim URL As String
        Dim MD5 As String
    End Structure

    Private Enum SettingTypes
        TypeInteger = 1
        TypeDouble = 2
        TypeString = 3
        TypeBoolean = 4
        TypeLong = 5
    End Enum

    ' Path for looking up proxy settings
    Private ReadOnly AppSettingsFilePath As String

#Region "Delegate Functions"

    Private Property HaveReprocessingFields As Boolean

    Public Sub UpdateStatus(ByVal pgBarVisible As Boolean, ByVal lblText As String)
        pgUpdate.Visible = pgBarVisible
        If lblText <> "" Then
            lblUpdateMain.Text = lblText
        End If
    End Sub

    ' Updates the value in the progressbar for a smooth progress (slows procesing a little) - total hack from this: http://stackoverflow.com/questions/977278/how-can-i-make-the-progress-bar-update-fast-enough/1214147#1214147
    Public Sub UpdateProgressBar(inValue As Integer)
        If inValue <= pgUpdate.Maximum - 1 And inValue <> 0 Then
            pgUpdate.Value = inValue
            pgUpdate.Value = pgUpdate.Value - 1
            pgUpdate.Value = inValue
        Else
            pgUpdate.Value = inValue
        End If
    End Sub

#End Region

    ' Worker
    Public Worker As BackgroundWorker
    Public TestingVersion As Boolean ' For testing downloads from the server for a new update
    Public LocalXMLFileName As String

    Public UpdateFileList As New List(Of FileEntry) ' List of files that need updating, will download and rename all at the same time
    Public EVEImagesLocalFolderName As String = "" ' This is the name of the folder we are going to replace. This is stored in the text file on local comp

    Public Const XMLLatestVersionFileName As String = "LatestVersionIPH.xml"
    Public Const XMLLatestVersionTest As String = "LatestVersionIPH Test.xml"
    Public Const UpdaterFileName As String = "EVEIPH Updater.exe"

    ' File Path
    Public Const XMLUpdateFileURL = "https://raw.githubusercontent.com/EVEIPH/LatestFiles/master/LatestVersionIPH.xml"
    Public Const XMLUpdateTestFileURL = "https://github.com/EVEIPH/LatestFiles/raw/master/LatestVersionIPH_Test.xml"

    ' For tracking an error
    Public ProgramErrorLocation As String
    Public SQL As String ' Keep global so I can put in error log
    Public ThrownError As String

    Public AppDataRoamingFolder As String ' Where the dynamic files are located
    Public ROOT_FOLDER As String = "" ' Where the root folder is located, that has the IPH exe and other files we can update that are not dynamically updated in IPH
    Public Const TempUpdatePath As String = "EVE IPH Updates" ' Where Updates will be downloaded to and moved to the main directories
    Public Const DynamicAppDataPath As String = "EVE IPH"

    Public DBOLD As New SQLiteConnection
    Public DBNEW As New SQLiteConnection

    Public Const EVE_DB As String = "EVEIPH DB.sqlite"
    Public Const EVE_IMAGES_ZIP As String = "EVEIPH Images.zip"
    Public Const EVEIPH_EXE As String = "EVE Isk per Hour.exe" ' For Shelling

    Public Const DATASOURCESTRING As String = "Data source="
    Public Const NO_LOCAL_XML_FILE As String = "NO LOCAL XML FILE"
    Public Const OLD_PREFIX As String = "OLD_"

    Public LocalCulture As New CultureInfo("en-US")

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Try
            'System.Net. update to allow for TLS 1.2 to connect to github
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' This is the current folder we are working in, to download and move updates
            AppDataRoamingFolder = Path.GetDirectoryName(Application.ExecutablePath)

            ' The root folder where the exe is located and other files we want to update - this is the installation directory
            ' Get the operating folder from arguments set when shelled
            If Environment.GetCommandLineArgs.Count > 1 Then ' First argument is the filename of the executing program
                For i = 1 To Environment.GetCommandLineArgs.Count - 1
                    ROOT_FOLDER &= Environment.GetCommandLineArgs(i).ToString & " "
                Next
            End If

            ' Need to strip off any end spaces for use
            ROOT_FOLDER = Trim(ROOT_FOLDER)

            ' Set test platform
            If File.Exists("Test.txt") Then
                TestingVersion = True
            Else
                TestingVersion = False
            End If

            ' Set the version of the XML file we will use
            If TestingVersion Then
                LocalXMLFileName = XMLLatestVersionTest
            Else
                LocalXMLFileName = XMLLatestVersionFileName
            End If

            ' Create the temp updates folder
            If Directory.Exists(Path.Combine(AppDataRoamingFolder, TempUpdatePath)) Then
                ' Delete what is there and replace
                Dim ImageDir As New DirectoryInfo(Path.Combine(AppDataRoamingFolder, TempUpdatePath))
                ImageDir.Delete(True)
            End If

            ' Create the new folder
            Directory.CreateDirectory(Path.Combine(AppDataRoamingFolder, TempUpdatePath))

            ' Set the path to the settings file
            AppSettingsFilePath = Path.ChangeExtension(Path.Combine(AppDataRoamingFolder, "Settings", "ApplicationSettings"), ".xml")

            BGWorker.WorkerReportsProgress = True
            BGWorker.WorkerSupportsCancellation = True

            pgUpdate.Value = 0
            pgUpdate.Visible = False
            pgUpdate.Maximum = 100

            ProgramErrorLocation = ""
            ThrownError = ""

            Me.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ' This event handler is where the time-consuming work is done.
    Private Sub BGWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWorker.DoWork
        Worker = CType(sender, BackgroundWorker)
        Dim ProgressCounter As Integer

        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim LocalFileMD5 As String = ""
        Dim EVEImagesNewLocalFolderName As String = "" ' This is the name of the folder we are going to unzip to
        Dim EVEDBLocalFileVersion As String = "" ' Local DB version

        Dim TempFile As FileEntry
        Dim ServerFileList As New List(Of FileEntry)
        Dim LocalFileList As New List(Of FileEntry)

        Dim NewRootFolder As String

        Dim RecordCount As Integer = 0
        Dim Counter As Integer = 0
        Dim CheckFile As String ' For checking if the file downloads or not
        Dim UpdateComplete As Boolean = False

        ' XML Temp file path for server file
        Dim ServerXMLLastUpdatePath As String

        ' For DB updates
        Dim DBCommand As SQLiteCommand

        ' Delegate for updating status
        Dim UpdateStatusDelegate As UpdateStatusSafe

        '================================================
        On Error GoTo 0

        Application.UseWaitCursor = True

        ' Sets the CurrentCulture 
        Thread.CurrentThread.CurrentCulture = LocalCulture

        UpdateStatusDelegate = New UpdateStatusSafe(AddressOf UpdateStatus)
        Me.Invoke(UpdateStatusDelegate, False, "Checking for Updates...")

        ' Get the newest update file from server
        Dim URL As String
        If TestingVersion Then
            URL = XMLUpdateTestFileURL
        Else
            URL = XMLUpdateFileURL
        End If

        ServerXMLLastUpdatePath = DownloadFileFromServer(URL, Path.Combine(AppDataRoamingFolder, TempUpdatePath, LocalXMLFileName))

        If ServerXMLLastUpdatePath <> "" Then
            ' Load the server xml file to check for updates 
            m_xmld.Load(ServerXMLLastUpdatePath)

            m_nodelist = m_xmld.SelectNodes("/EVEIPH/result/rowset/row")

            ' Loop through the nodes 
            For Each m_node In m_nodelist
                ' Load all except updater
                If m_node.Attributes.GetNamedItem("Name").Value <> UpdaterFileName Then
                    TempFile.Name = m_node.Attributes.GetNamedItem("Name").Value
                    TempFile.Version = m_node.Attributes.GetNamedItem("Version").Value
                    TempFile.MD5 = m_node.Attributes.GetNamedItem("MD5").Value
                    TempFile.URL = m_node.Attributes.GetNamedItem("URL").Value
                    ' Insert the file
                    ServerFileList.Add(TempFile)
                End If
            Next
        Else
            ' Didn't download properly
            GoTo RevertToOldFileVersions
        End If

        If File.Exists(Path.Combine(AppDataRoamingFolder, LocalXMLFileName)) Then
            ' Load the local xml file to check for updates for the DB and images
            m_xmld.Load(Path.Combine(AppDataRoamingFolder, LocalXMLFileName))
            m_nodelist = m_xmld.SelectNodes("/EVEIPH/result/rowset/row")

            ' Loop through the nodes 
            For Each m_node In m_nodelist
                ' Load all except updater
                If m_node.Attributes.GetNamedItem("Name").Value <> UpdaterFileName Then
                    TempFile.Name = m_node.Attributes.GetNamedItem("Name").Value
                    TempFile.Version = m_node.Attributes.GetNamedItem("Version").Value
                    TempFile.MD5 = m_node.Attributes.GetNamedItem("MD5").Value
                    TempFile.URL = m_node.Attributes.GetNamedItem("URL").Value
                    ' Insert the file
                    LocalFileList.Add(TempFile)
                End If
            Next
        End If

        ' Done with these
        m_xmld = Nothing
        m_nodelist = Nothing
        m_node = Nothing

        Me.Invoke(UpdateStatusDelegate, False, "Downloading Updates...")
        Application.DoEvents()

        ' Now download all in the list if the server has newer versions
        RecordCount = ServerFileList.Count - 1
        For Each ServerFileRecord In ServerFileList

            If (Worker.CancellationPending = True) Then
                e.Cancel = True
                Exit Sub
            End If

            ' Get the MD5 from each filename in the list and compare to XML, if different, download the update
            If ServerFileRecord.Name = EVE_IMAGES_ZIP Or ServerFileRecord.Name = EVE_DB Then
                ' Zip file of images or the DB, so special processing
                ' Zip file is in a folder after update and DB will have a different MD5 after it is updated
                ' Need to load the local MD5 data from the Local XML since the folder doesn't have one MD5
                If LocalFileList.Count <> 0 Then
                    For j = 0 To LocalFileList.Count - 1
                        ' Find the MD5 for the EVEDB or Image Zip file
                        If ServerFileRecord.Name = LocalFileList(j).Name Then
                            ' For the zip file, save the name of the current image folder (based on xml file)
                            If ServerFileRecord.Name = EVE_IMAGES_ZIP Then
                                EVEImagesLocalFolderName = LocalFileList(j).Name.Substring(0, Len(LocalFileList(j).Name) - 4)
                            ElseIf ServerFileRecord.Name = EVE_DB Then
                                EVEDBLocalFileVersion = LocalFileList(j).Version
                            End If

                            LocalFileMD5 = LocalFileList(j).MD5
                            Exit For
                        End If
                    Next
                Else
                    LocalFileMD5 = ""
                    EVEImagesLocalFolderName = EVE_IMAGES_ZIP.Substring(0, Len(EVE_IMAGES_ZIP) - 4)
                    EVEDBLocalFileVersion = EVE_DB
                End If
            Else
                ' These are straight MD5 file checks - Images and db a special exception above
                LocalFileMD5 = MD5CalcFile(Path.Combine(GetNewRootFolder(ServerFileRecord.Name), ServerFileRecord.Name))
            End If

            ' Compare the MD5's and see if we download the new file
            If LocalFileMD5 <> ServerFileRecord.MD5 Then

                ' Need to update, download to updates folder for later update
                Me.Invoke(UpdateStatusDelegate, True, "")
                CheckFile = DownloadFileFromServer(ServerFileRecord.URL, Path.Combine(AppDataRoamingFolder, TempUpdatePath, ServerFileRecord.Name))

                If (Worker.CancellationPending = True) Then
                    e.Cancel = True
                    Exit Sub
                End If

                If CheckFile = "" Then
                    ' Some error in downloading
                    ProgramErrorLocation = "Download Failed. File Not found on Server."
                    Exit Sub
                Else
                    ' Check the file MD5 to make sure we got a good download. If not, try one more time
                    ' If they don't have a local file (which will have a blank MD5) then just go with what they got
                    If ServerFileRecord.MD5 <> NO_LOCAL_XML_FILE Then
                        ' Get the file size to check
                        Dim infoReader As System.IO.FileInfo
                        infoReader = My.Computer.FileSystem.GetFileInfo(CheckFile)
                        ' Still bad MD5 or the file is 0 bytes
                        If MD5CalcFile(CheckFile) <> ServerFileRecord.MD5 Or infoReader.Length = 0 Then
                            CheckFile = DownloadFileFromServer(ServerFileRecord.URL, Path.Combine(AppDataRoamingFolder, TempUpdatePath, ServerFileRecord.Name))

                            If (Worker.CancellationPending = True) Then
                                e.Cancel = True
                                Exit Sub
                            End If

                            If MD5CalcFile(CheckFile) <> ServerFileRecord.MD5 Or CheckFile = "" Then
                                ProgramErrorLocation = "Download Corrupted."
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                ' Record the file we are upating
                UpdateFileList.Add(ServerFileRecord)
            End If

            Me.Invoke(UpdateStatusDelegate, False, "")
        Next

        ' Leave if nothing to update
        If IsNothing(UpdateFileList) Then
            Exit Sub
        End If

        Me.Invoke(UpdateStatusDelegate, False, "Installing Updates...")
        Application.DoEvents()

        ' Try to update the old files, delete, and rename to new
        RecordCount = UpdateFileList.Count - 1
        Counter = 0
        For Each UpdateFileListRecord In UpdateFileList

            If (Worker.CancellationPending = True) Then
                e.Cancel = True
                Exit Sub
            Else
                ' Report progress.
                If RecordCount > 0 Then
                    Worker.ReportProgress((Counter / RecordCount) + 1 * 10)
                End If
            End If

            ' Now that we have the files downloaded, run special updates for DB and images (Zipped), the others are just saved already
            If UpdateFileListRecord.Name.Substring(UpdateFileListRecord.Name.Length - 7) = ".sqlite" Then

                ' Open databases, if no database file, so just exit and use downloaded one
                If File.Exists(Path.Combine(AppDataRoamingFolder, UpdateFileListRecord.Name)) Then
                    Me.Invoke(UpdateStatusDelegate, False, "Updating Database...")

                    On Error Resume Next
                    ' Delete these files before opening
                    File.Delete(Path.Combine(AppDataRoamingFolder, UpdateFileListRecord.Name) & "-shm")
                    File.Delete(Path.Combine(AppDataRoamingFolder, UpdateFileListRecord.Name) & "-wal")
                    DBOLD.ConnectionString = DATASOURCESTRING & Path.Combine(AppDataRoamingFolder, UpdateFileListRecord.Name)
                    DBOLD.Open()

                    ' Delete these files before opening
                    File.Delete(Path.Combine(AppDataRoamingFolder, TempUpdatePath, UpdateFileListRecord.Name) & "-shm")
                    File.Delete(Path.Combine(AppDataRoamingFolder, TempUpdatePath, UpdateFileListRecord.Name) & "-wal")
                    DBNEW.ConnectionString = DATASOURCESTRING & Path.Combine(AppDataRoamingFolder, TempUpdatePath, UpdateFileListRecord.Name)
                    DBNEW.Open()

                    ' Open both DBs here and update through ref
                    Call ExecuteNonQuerySQL("PRAGMA synchronous = NORMAL", DBOLD)
                    Call ExecuteNonQuerySQL("PRAGMA synchronous = NORMAL; PRAGMA auto_vacuum = FULL;", DBNEW)
                    On Error GoTo 0

                    Call UpdateAllBlueprintsTable()

                    Call UpdateESICharacterDataTable()
                    Call UpdateESICorporationDataTable()
                    Call UpdateESIPublicCacheDatesTable()
                    Call UpdateESICorporationRolesTable()

                    Call UpdateAssetsTable()
                    Call UpdateAssetLocationsTable()

                    Call UpdateCharacterSkillsTable()
                    Call UpdateCharacterStandingsTable()
                    Call UpdateCurrentResearchAgentsTable()

                    Call UpdateMarketHistoryTable()
                    Call UpdateMarketHistoryUpdateCacheTable()

                    Call UpdateMarketOrdersTable()
                    Call UpdateMarketOrdersUpdateCacheTable()
                    Call UpdateStructureMarketOrdersTable()
                    Call UpdateStructureMarketOrdersUpdateCacheTable()

                    Call UpdateStationsTable()

                    Call UpdateIndustryJobsTable()

                    Call UpdateItemPricesTable()
                    Call UpdateItemPricesCacheTable()
                    Call UpdatePriceProfilesTable()

                    Call UpdateOwnedBlueprintsTable()
                    Call UpdateFWSystemUpgradesTable()

                    ' Call UpdateIndustryFacilitiesTable()
                    Call UpdateIndustrySystemCostIndiciesTable()

                    Call UpdateSavedFacilitiesTable()
                    Call UpdateUpwellStructuresInstalledModulesTable()

                    Me.Invoke(UpdateStatusDelegate, False, "Performing Database integrity check...")
                    ProgramErrorLocation = "Error with DB integrity Check"
                    Call ExecuteNonQuerySQL("PRAGMA integrity_check", DBNEW)
                    Application.DoEvents()

                    DBOLD.Close()
                    DBNEW.Close()
                    DBNEW.Dispose()
                    DBOLD.Dispose()
                    DBNEW = Nothing
                    DBOLD = Nothing

                    ProgramErrorLocation = ""
                    SQL = ""
                End If

            ElseIf UpdateFileListRecord.Name = EVE_IMAGES_ZIP Then
                Me.Invoke(UpdateStatusDelegate, False, "Installing Image Updates...")
                ProgramErrorLocation = "Cannot copy images"

                EVEImagesNewLocalFolderName = EVE_IMAGES_ZIP.Substring(0, Len(EVE_IMAGES_ZIP) - 4) ' Save as base name
                ' Delete if exists
                File.Delete(Path.Combine(AppDataRoamingFolder, TempUpdatePath, EVEImagesNewLocalFolderName))
                ' Extract the images
                'Call ZipFile.ExtractToDirectory(Path.Combine(AppDataRoamingFolder, TempUpdatePath, EVE_IMAGES_ZIP), Path.Combine(AppDataRoamingFolder, TempUpdatePath, EVEImagesNewLocalFolderName))

                ProgramErrorLocation = ""
                SQL = ""
            End If

            Counter += 1
        Next

        ProgramErrorLocation = ""
        SQL = ""
        GC.Collect()
        ' wait a second for GC
        Thread.Sleep(1000)

        Application.DoEvents()

        ' If we screw up after this, we have to revert to anything we changed if possible
        On Error GoTo RevertToOldFileVersions

        ' Rename all files/folders with OLD and copy over new files/folders
        RecordCount = UpdateFileList.Count - 1

        For i = 0 To RecordCount
            Me.Invoke(UpdateStatusDelegate, False, "Copying Files...")
            If (Worker.CancellationPending = True) Then
                e.Cancel = True
                Exit Sub
            Else
                ' Report progress.
                If RecordCount > 0 Then
                    Worker.ReportProgress((i / RecordCount) + 1 * 10)
                End If
            End If

            NewRootFolder = GetNewRootFolder(UpdateFileList(i).Name)

            If UpdateFileList(i).Name = EVE_IMAGES_ZIP Then
                Me.Invoke(UpdateStatusDelegate, False, "Updating Images...")

                ' Delete OLD folder if it exists
                If Directory.Exists(Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesLocalFolderName)) Then
                    ProgramErrorLocation = "Error Deleting Old Images"
                    ' Delete what is there and replace
                    Directory.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesLocalFolderName), True)
                    Application.DoEvents()
                End If

                ' Rename the current folder to old
                If Directory.Exists(Path.Combine(NewRootFolder, EVEImagesLocalFolderName)) Then
                    ProgramErrorLocation = "Error Moving Old Images"
                    Directory.Move(Path.Combine(NewRootFolder, EVEImagesLocalFolderName), Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesLocalFolderName))
                    Application.DoEvents()
                End If

                ' Extract all the files into the new image folder from temp updates folder to root directory folder
                ProgramErrorLocation = "Error Moving New Images"
                Call ZipFile.ExtractToDirectory(Path.Combine(AppDataRoamingFolder, TempUpdatePath, EVE_IMAGES_ZIP), Path.Combine(NewRootFolder, EVEImagesNewLocalFolderName))
                'Directory.Move(Path.Combine(AppDataRoamingFolder, TempUpdatePath, EVEImagesNewLocalFolderName), Path.Combine(NewRootFolder, EVEImagesNewLocalFolderName))
                Application.DoEvents()

            ElseIf UpdateFileList(i).Name = EVE_DB Then
                Me.Invoke(UpdateStatusDelegate, False, "Cleaning up Database...")

                ' If an OLD file exists, delete it
                If File.Exists(Path.Combine(NewRootFolder, OLD_PREFIX & EVE_DB)) Then
                    ProgramErrorLocation = "Error Deleting Old Database"
                    File.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & EVE_DB))
                    Application.DoEvents()
                End If

                ' Rename old file if it exists to old prefix
                If File.Exists(Path.Combine(NewRootFolder, EVE_DB)) Then
                    ProgramErrorLocation = "Error Moving Old Database"
                    File.Move(Path.Combine(NewRootFolder, EVE_DB), Path.Combine(NewRootFolder, OLD_PREFIX & EVE_DB))
                    Application.DoEvents()
                End If

                ' Move new file from temp to main
                ProgramErrorLocation = "Error Moving New Database"
                File.Move(Path.Combine(AppDataRoamingFolder, TempUpdatePath, EVE_DB), Path.Combine(NewRootFolder, EVE_DB))
                Application.DoEvents()

                ' Now that the DB is moved, clean out any old sqlite-shm, sqlite-wal. or sqlite-journal files from the old DB as these could cause a malformed disk error
                If File.Exists(Path.Combine(NewRootFolder, "EVEIPH DB.sqlite-shm")) Then
                    File.Delete(Path.Combine(NewRootFolder, "EVEIPH DB.sqlite-shm"))
                End If

                If File.Exists(Path.Combine(NewRootFolder, "EVEIPH DB.sqlite-wal")) Then
                    File.Delete(Path.Combine(NewRootFolder, "EVEIPH DB.sqlite-wal"))
                End If

                If File.Exists(Path.Combine(NewRootFolder, "EVEIPH DB.sqlite-journal")) Then
                    File.Delete(Path.Combine(NewRootFolder, "EVEIPH DB.sqlite-journal"))
                End If

            Else

                Me.Invoke(UpdateStatusDelegate, False, "Updating " & UpdateFileList(i).Name & "...")

                ' If an OLD file exists, delete it
                If File.Exists(Path.Combine(NewRootFolder, OLD_PREFIX & UpdateFileList(i).Name)) Then
                    ProgramErrorLocation = "Error Deleting Old " & UpdateFileList(i).Name & "file"
                    File.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & UpdateFileList(i).Name))
                    Application.DoEvents()
                End If

                ' Rename old file if it exists to old prefix
                If File.Exists(Path.Combine(NewRootFolder, UpdateFileList(i).Name)) Then
                    ProgramErrorLocation = "Error Moving Old " & UpdateFileList(i).Name & "file"
                    File.Move(Path.Combine(NewRootFolder, UpdateFileList(i).Name), Path.Combine(NewRootFolder, OLD_PREFIX & UpdateFileList(i).Name))
                    Application.DoEvents()
                End If

                ' Move new file
                ProgramErrorLocation = "Error Moving New " & UpdateFileList(i).Name & "file"
                File.Move(Path.Combine(AppDataRoamingFolder, TempUpdatePath, UpdateFileList(i).Name), Path.Combine(NewRootFolder, UpdateFileList(i).Name))
                Application.DoEvents()

            End If
        Next

        ProgramErrorLocation = ""
        Me.Invoke(UpdateStatusDelegate, False, "Cleaning up Temp Files...")
        DBCommand = Nothing

        Exit Sub

RevertToOldFileVersions:

        ' Output error first
        Call WriteMsgToLog(Err.Description)

        On Error Resume Next

        ' If we get here, try to delete everything we downloaded and rename any files saved as "Old" to original names
        ProgramErrorLocation &= " - Reverted to Old file versions"
        ' Save the error
        ThrownError = Err.Description

        ' Delete the updates folder
        If Directory.Exists(Path.Combine(AppDataRoamingFolder, TempUpdatePath)) Then
            ' Delete what is there and replace
            Directory.Delete(Path.Combine(AppDataRoamingFolder, TempUpdatePath), True)
            Application.DoEvents()
        End If

        ' Rename all files/folders 
        If Not IsNothing(UpdateFileList) Then
            For Each RenameFileRecord In UpdateFileList

                NewRootFolder = GetNewRootFolder(RenameFileRecord.Name)

                If RenameFileRecord.Name = EVE_IMAGES_ZIP Then
                    ' Delete the new folder if the old one renamed
                    If Directory.Exists(Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesNewLocalFolderName)) Then
                        ' Delete it
                        Directory.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesNewLocalFolderName), True)
                        Application.DoEvents()
                    End If

                    ' Rename the old zip folder
                    Directory.Move(Path.Combine(NewRootFolder, EVEImagesLocalFolderName), Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesLocalFolderName))
                    Application.DoEvents()

                ElseIf RenameFileRecord.Name = EVE_DB Then

                    ' If an OLD file exists, delete new, rename old
                    If File.Exists(Path.Combine(NewRootFolder, OLD_PREFIX & EVE_DB)) Then
                        File.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & EVE_DB))
                        Application.DoEvents()
                        File.Move(Path.Combine(AppDataRoamingFolder, OLD_PREFIX & EVE_DB), Path.Combine(AppDataRoamingFolder, EVE_DB))
                        Application.DoEvents()
                    End If

                Else
                    ' Only rename if old version exists
                    If File.Exists(Path.Combine(NewRootFolder, OLD_PREFIX & RenameFileRecord.Name)) Then
                        ' Delete the new file
                        File.Delete(Path.Combine(NewRootFolder, RenameFileRecord.Name))
                        Application.DoEvents()
                        ' Rename old file back 
                        File.Move(Path.Combine(NewRootFolder, OLD_PREFIX & RenameFileRecord.Name), Path.Combine(NewRootFolder, RenameFileRecord.Name))
                        Application.DoEvents()
                    End If
                End If

            Next
        End If

        Exit Sub

    End Sub

    ' This event handler updates the progress.
    Private Sub BGWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles BGWorker.ProgressChanged

        Dim safedelegate As New UpdatePGBarSafe(AddressOf UpdateProgressBar)
        Me.Invoke(safedelegate, e.ProgressPercentage) 'Invoke the TreadsafeDelegate

    End Sub

    Private Function GetNewRootFolder(ByVal FileName As String) As String
        ' need to split where the files are stored - update path (roaming) or the same folder - exe is in root path for instance
        Select Case FileName
            Case UpdaterFileName, EVE_DB, LocalXMLFileName
                Return AppDataRoamingFolder ' current folder we are working in (roaming or the root depending on install location)
            Case Else
                ' Main folder for the manifest, dlls, and main exe
                Return ROOT_FOLDER
        End Select
    End Function

    Private Sub CleanUpOLDFiles()
        Dim ImageDir As DirectoryInfo
        Dim NewRootFolder As String

        On Error Resume Next

        For i = 0 To UpdateFileList.Count - 1
            NewRootFolder = GetNewRootFolder(UpdateFileList(i).Name)

            If UpdateFileList(i).Name = EVE_IMAGES_ZIP Then
                ' Downloaded old folder
                Directory.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & EVEImagesLocalFolderName), True)
            ElseIf UpdateFileList(i).Name = EVE_DB Then
                ' Delete old file
                File.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & EVE_DB))
            Else
                ' Delete old file
                File.Delete(Path.Combine(NewRootFolder, OLD_PREFIX & UpdateFileList(i).Name))
            End If
        Next

    End Sub

    ' Shows message box with message sent
    Private Sub ShowNotifyBox(LabelText As String)
        Dim f1 As New frmNotify
        f1.lblNotify.Text = LabelText
        f1.ShowDialog()
    End Sub

    ' This event handler deals with the results of the background operation.
    Private Sub BGWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles BGWorker.RunWorkerCompleted
        Dim ErrorText As String = ""

        Try

            ' Allow the messagebox to pop up over the form now
            Me.TopMost = False

            If Not IsNothing(e.Error) Then
                ErrorText = e.Error.ToString
            End If

            ' Clean up all OLD files and folders that might be left around
            Call CleanUpOLDFiles()

            Application.UseWaitCursor = False

            If e.Cancelled = True Then
                lblUpdateMain.Text = "Update Canceled"
                Call ShowNotifyBox("Update Canceled")
            ElseIf (e.Error IsNot Nothing) Or (ProgramErrorLocation <> "") Then

                lblUpdateMain.Text = "Update Failed."

                ' Write sql and error to log
                If SQL <> "" Then
                    ErrorText = ErrorText & " SQL: " & SQL
                End If

                Call WriteMsgToLog(ErrorText)

                Dim MainMessage As String = "There was an error in the update. Program not updated."

                If ThrownError <> "" And ProgramErrorLocation <> "" Then
                    MsgBox(MainMessage & vbCrLf & ProgramErrorLocation & vbCrLf & "Error: " & ThrownError, vbCritical, Application.ProductName)
                ElseIf ProgramErrorLocation <> "" Then
                    MsgBox(MainMessage & vbCrLf & ProgramErrorLocation, vbCritical, Application.ProductName)
                ElseIf ThrownError <> "" Then
                    MsgBox(MainMessage & vbCrLf & "Error: " & ThrownError, vbCritical, Application.ProductName)
                Else
                    Call ShowNotifyBox(MainMessage)
                End If

            Else
                Me.Hide()
                lblUpdateMain.Text = "Update Complete."
                ' We have completed the update
                ' Copy over the old XML file and delete the old
                If File.Exists(Path.Combine(AppDataRoamingFolder, LocalXMLFileName)) Then
                    File.Delete(Path.Combine(AppDataRoamingFolder, LocalXMLFileName))
                End If

                File.Move(Path.Combine(AppDataRoamingFolder, TempUpdatePath, LocalXMLFileName), Path.Combine(AppDataRoamingFolder, LocalXMLFileName))

                ' Finally delete the temp updates folder
                Directory.Delete(Path.Combine(AppDataRoamingFolder, TempUpdatePath), True)

                ' Wait for a second before running - might solve the problem with incorrectly suggesting an update
                Thread.Sleep(1000)

                Call ShowNotifyBox("Update Complete!")
            End If

            ' Open new program
            Dim Proc As New Process
            Proc.StartInfo.FileName = Path.Combine(ROOT_FOLDER, EVEIPH_EXE)
            Proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Proc.Start()

        Catch ex As Exception
            MsgBox("Run Worker Completed error: " & ex.ToString)
        End Try

        ' Done, hide form and close
        Me.Hide()
        End

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If BGWorker.WorkerSupportsCancellation = True Then
            ' Cancel the asynchronous operation.
            BGWorker.CancelAsync()
        End If
    End Sub

    ' When the form is shown, run the updates
    Private Sub FrmUpdaterMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Me.Refresh()
        Application.DoEvents()
        Application.UseWaitCursor = True

        If BGWorker.IsBusy <> True Then
            ' Start the asynchronous operation.
            BGWorker.RunWorkerAsync()
        End If

        Me.Refresh()
        Application.DoEvents()
        Application.UseWaitCursor = False

    End Sub

    ' Downloads the sent file from server and saves it to the root directory as the sent file name
    Public Function DownloadFileFromServer(ByVal DownloadURL As String, ByVal FileName As String) As String
        'Creating the request and getting the response
        Dim Response As HttpWebResponse
        Dim Request As HttpWebRequest

        ' File sizes for progress bar
        Dim FileSize As Double

        ' For reading in chunks of data
        Dim readBytes(4095) As Byte
        ' Create directory if it doesn't exist already
        If Not Directory.Exists(Path.GetDirectoryName(FileName)) Then
            Directory.CreateDirectory(Path.GetDirectoryName(FileName))
        End If
        Dim writeStream As New FileStream(FileName, FileMode.Create)
        Dim bytesread As Integer

        'Replacement for Stream.Position (webResponse stream doesn't support seek)
        Dim nRead As Long

        Worker.ReportProgress(0)

        Try 'Checks if the file exist
            Request = DirectCast(HttpWebRequest.Create(DownloadURL), HttpWebRequest)
            Request.Proxy = GetProxyData()
            Request.Credentials = CredentialCache.DefaultCredentials ' Added 9/27 to attempt to fix error: (407) Proxy Authentication Required.
            Request.Timeout = 50000
            Response = CType(Request.GetResponse, HttpWebResponse)
        Catch ex As Exception
            ' Set as empty and return
            writeStream.Close()
            Return ""
        End Try

        ' Get size
        FileSize = Response.ContentLength()

        ' Loop through and get the file in chunks, save out
        Do
            Application.DoEvents()

            If Worker.CancellationPending Then 'If user abort download
                Exit Do
            End If

            bytesread = Response.GetResponseStream.Read(readBytes, 0, 4096)

            ' No more bytes to read
            If bytesread = 0 Then Exit Do

            nRead += bytesread
            ' Update progress 
            Worker.ReportProgress((nRead * 100) / FileSize)

            writeStream.Write(readBytes, 0, bytesread)
        Loop

        'Close the streams
        Response.GetResponseStream.Close()
        writeStream.Close()

        Return FileName

    End Function

    ' MD5 Hash - specify the path to a file and this routine will calculate your hash
    Public Function MD5CalcFile(ByVal filepath As String) As String

        ' open file (as read-only)
        If File.Exists(filepath) Then
            Using reader As New System.IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read)
                Using md5 As New System.Security.Cryptography.MD5CryptoServiceProvider

                    ' hash contents of this stream
                    Dim hash() As Byte = md5.ComputeHash(reader)

                    ' return formatted hash
                    Return ByteArrayToString(hash)

                End Using
            End Using
        End If

        ' Something went wrong
        Return ""

    End Function

    ' MD5 Hash - utility function to convert a byte array into a hex string
    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String

        Dim sb As New System.Text.StringBuilder(arrInput.Length * 2)

        For i As Integer = 0 To arrInput.Length - 1
            sb.Append(arrInput(i).ToString("X2"))
        Next

        Return sb.ToString().ToLower

    End Function

    ' Formats the value sent to what we want to insert into the table field
    Private Function BuildInsertFieldString(ByVal inValue As Object) As String
        Dim CheckNullValue As Object
        Dim OutputString As String

        ' See if it is null first
        CheckNullValue = CheckNull(inValue)

        If CStr(CheckNullValue) <> "null" Then
            ' Not null, so format
            If inValue.GetType.Name = "DateTime" Then
                OutputString = "'" & Format(inValue, "yyyy-MM-dd HH:mm:ss") & "'"
            ElseIf inValue.GetType.Name <> "String" Then
                ' Just a value, so no quotes needed
                OutputString = CStr(inValue)
            Else
                ' String, so check for appostrophes and add quotes
                OutputString = "'" & FormatDBString(CStr(inValue)) & "'"
            End If
        Else
            OutputString = "NULL"
        End If

        Return OutputString

    End Function

    Public Function CheckNull(ByVal inVariable As Object) As Object
        If IsNothing(inVariable) Then
            Return "null"
        ElseIf DBNull.Value.Equals(inVariable) Then
            Return "null"
        Else
            Return inVariable
        End If
    End Function

    Public Sub ExecuteNonQuerySQL(ByVal SQL As String, ByRef db As SQLiteConnection)
        Dim DBExecuteCmd As SQLiteCommand

        DBExecuteCmd = New SQLiteCommand(SQL, db)
        DBExecuteCmd.ExecuteNonQuery()

        DBExecuteCmd.Dispose()
    End Sub

    Public Sub BeginSQLiteTransaction(ByRef DB As SQLiteConnection)
        Call ExecuteNonQuerySQL("BEGIN;", DB)
    End Sub

    Public Sub CommitSQLiteTransaction(ByRef DB As SQLiteConnection)
        Call ExecuteNonQuerySQL("END;", DB)
    End Sub

    Public Function FormatDBString(ByVal inStrVar As String) As String
        ' Anything with quote mark in name it won't correctly load - need to replace with double quotes
        If InStr(inStrVar, "'") <> 0 Then
            inStrVar = Replace(inStrVar, "'", "''")
        End If
        Return inStrVar
    End Function

    ' Writes a sent message to a log file
    Public Sub WriteMsgToLog(ByVal ErrorMsg As String)
        Dim FilePath As String = Path.Combine(AppDataRoamingFolder, "EVEIPH.log")
        Dim AllText() As String

        ' Only write to log if there is an error to write
        If Trim(ErrorMsg) <> "" Then
            If Not IO.File.Exists(FilePath) Then
                Dim sw As IO.StreamWriter = IO.File.CreateText(FilePath)
                sw.Close()
            End If

            ' This is an easier way to get all of the strings in the file.
            AllText = IO.File.ReadAllLines(FilePath)
            ' This will append the string to the end of the file.
            My.Computer.FileSystem.WriteAllText(FilePath, CStr(Now) & " - " & ErrorMsg & vbCrLf, True)
        End If

    End Sub

    Public Function GetProxyData() As WebProxy
        Dim ReturnProxy As WebProxy
        Dim ProxyAddress As String = ""
        Dim ProxyPort As Integer = 0

        If File.Exists(AppSettingsFilePath) Then
            ProxyAddress = GetSettingValue(SettingTypes.TypeString, "ApplicationSettings", "ProxyAddress", "")
            ProxyPort = GetSettingValue(SettingTypes.TypeInteger, "ApplicationSettings", "ProxyPort", 0)
        End If

        If ProxyAddress <> "" Then
            If ProxyPort <> 0 Then
                ReturnProxy = New WebProxy(ProxyAddress, ProxyPort)
            Else
                ReturnProxy = New WebProxy(ProxyAddress)
            End If

            ReturnProxy.Credentials = CredentialCache.DefaultCredentials

            Return ReturnProxy
        Else
            Return Nothing
        End If

    End Function

    ' Gets a value from a referenced XML file by searching for it
    Private Function GetSettingValue(ObjectType As SettingTypes, RootElement As String, ElementString As String, DefaultValue As Object) As Object
        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim FilePath As String = AppSettingsFilePath
        Dim TempValue As String

        'Load the Xml file
        m_xmld.Load(FilePath)

        'Get the settings
        m_nodelist = m_xmld.SelectNodes("/" & RootElement & "/" & ElementString)

        If Not IsNothing(m_nodelist.Item(0)) Then
            ' Should only be one
            TempValue = m_nodelist.Item(0).InnerText

            ' If blank, then return default
            If TempValue = "" Then
                Return DefaultValue
            End If

            If TempValue = "False" Or TempValue = "True" Then
                ' Change to type boolean
                ObjectType = SettingTypes.TypeBoolean
            End If

            ' Found it, return the cast
            Select Case ObjectType
                Case SettingTypes.TypeBoolean
                    Return CBool(TempValue)
                Case SettingTypes.TypeDouble
                    Return CDbl(TempValue)
                Case SettingTypes.TypeInteger
                    Return CInt(TempValue)
                Case SettingTypes.TypeString
                    Return CStr(TempValue)
                Case SettingTypes.TypeLong
                    Return CLng(TempValue)
            End Select

        Else
            ' Doesn't exist, use default
            Return DefaultValue
        End If

        Return Nothing

    End Function

#Region "Database Table Updates"

    Private Sub UpdateESICharacterDataTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewAPIFields As Boolean

        ProgramErrorLocation = "Cannot copy ESI_CHARACTER_DATA"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM ESI_CHARACTER_DATA"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have it
            SQL = "SELECT * FROM ESI_CHARACTER_DATA"
        Else
            ' They don't have the table, so exit because this is a required table and will come with nothing in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "INSERT INTO ESI_CHARACTER_DATA VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(14)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(15)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(16)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(17)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(18)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(19)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(20)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(21)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(22)) & ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateESICorporationDataTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String = ""
        Dim HaveNewFields As Boolean = True

        ProgramErrorLocation = "Cannot copy ESI_CORPORATION_DATA"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT CORP_ROLES_CACHE_DATE FROM ESI_CORPORATION_DATA"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If IsNothing(readerUpdate) Then
            ' They don't have the new fields
            HaveNewFields = False
        End If

        SQL = "SELECT * FROM ESI_CORPORATION_DATA"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            If Not HaveNewFields Then
                SQL = "INSERT INTO ESI_CORPORATION_DATA VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(0) & "," ' FactionID
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                SQL &= BuildInsertFieldString(0) & "," ' Home_Station_id
                SQL &= BuildInsertFieldString(0) & "," ' Shares
                SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(14)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(15)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(16)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(17)) & ","
                SQL &= "NULL)" ' Corp roles cache date
            Else
                SQL = "INSERT INTO ESI_CORPORATION_DATA VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(14)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(15)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(16)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(17)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(18)) & ")"
            End If

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateESICorporationRolesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewAPIFields As Boolean

        ProgramErrorLocation = "Cannot copy ESI_CORPORATION_ROLES"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM ESI_CORPORATION_ROLES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have it
            SQL = "SELECT * FROM ESI_CORPORATION_ROLES"
        Else
            ' They don't have the table, so exit because this is a required table and will come with nothing in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "INSERT INTO ESI_CORPORATION_ROLES VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateESIPublicCacheDatesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewField1 As Boolean = True
        Dim HaveNewField2 As Boolean = True

        ProgramErrorLocation = "Cannot copy ESI_PUBLIC_CACHE_DATES"

        ' See if they have the new fields
        On Error Resume Next
        readerUpdate = Nothing
        SQL = "SELECT PUBLIC_ESI_STATUS_CACHED_UNTIL FROM ESI_PUBLIC_CACHE_DATES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        If IsNothing(readerUpdate) Then
            ' They don't have the new field
            HaveNewField1 = False
        End If
        readerUpdate.Close()
        readerUpdate = Nothing

        SQL = "SELECT PUBLIC_CONTRACTS_CACHED_UNTIL FROM ESI_PUBLIC_CACHE_DATES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        If IsNothing(readerUpdate) Then
            ' They don't have the new field
            HaveNewField2 = False
        End If
        readerUpdate.Close()

        On Error GoTo 0
        SQL = "SELECT * FROM ESI_PUBLIC_CACHE_DATES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "INSERT INTO ESI_PUBLIC_CACHE_DATES VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            If HaveNewField1 Then
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            Else
                SQL &= "NULL,"
            End If
            If HaveNewField2 Then
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ")"
            Else
                SQL &= "NULL)"
            End If

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateSavedFacilitiesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewFields As Boolean = True

        ProgramErrorLocation = "Cannot copy SAVED_FACILITIES"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM SAVED_FACILITIES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have the table
            readerUpdate.Close()
            readerUpdate = Nothing

            ' See if they have the new fields
            On Error Resume Next
            SQL = "SELECT CONVERT_TO_ORE FROM SAVED_FACILITIES"
            DBCommand = New SQLiteCommand(SQL, DBOLD)
            readerUpdate = DBCommand.ExecuteReader
            On Error GoTo 0

            If IsNothing(readerUpdate) Then
                ' They don't have the new field
                HaveNewFields = False
            Else
                readerUpdate.Close()
            End If
        Else
            ' They don't have the table, so exit because this is a required table and will come with defaults in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand("SELECT * FROM SAVED_FACILITIES", DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "INSERT INTO SAVED_FACILITIES VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(14)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(15)) & ","
            If HaveNewFields Then
                SQL &= BuildInsertFieldString(readerUpdate.Item(16)) & ")"
            Else
                SQL &= "0)"
            End If

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateUpwellStructuresInstalledModulesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewAPIFields As Boolean

        ProgramErrorLocation = "Cannot copy UPWELL_STRUCTURES_INSTALLED_MODULES"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM UPWELL_STRUCTURES_INSTALLED_MODULES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have it
            SQL = "SELECT * FROM UPWELL_STRUCTURES_INSTALLED_MODULES"
        Else
            ' They don't have the table, so exit because this is a required table and will come with nothing in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "INSERT INTO UPWELL_STRUCTURES_INSTALLED_MODULES VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateAllBlueprintsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewAPIFields As Boolean

        ProgramErrorLocation = "Cannot copy ALL_BLUEPRINTS Data"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM ALL_BLUEPRINTS_FACT"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have it
            SQL = "SELECT BLUEPRINT_ID, IGNORE, FAVORITE FROM ALL_BLUEPRINTS_FACT"
        Else
            ' They don't have the table, so exit because this is a required table and will come with nothing in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "UPDATE ALL_BLUEPRINTS_FACT SET IGNORE =" & BuildInsertFieldString(readerUpdate.Item(1)) & ", "
            SQL &= "FAVORITE = " & BuildInsertFieldString(readerUpdate.Item(2)) & " "
            SQL &= "WHERE BLUEPRINT_ID = " & BuildInsertFieldString(readerUpdate.Item(0))

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateAssetsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewFields As Boolean = True

        ' ASSETS
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Assets"

        ' See if they have the new fields
        On Error Resume Next
        SQL = "SELECT ItemName FROM ASSETS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If IsNothing(readerUpdate) Then
            ' They don't have the new field
            HaveNewFields = False
        End If

        SQL = "SELECT * FROM ASSETS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then
            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO ASSETS VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                If HaveNewFields Then
                    SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ")"
                Else
                    SQL &= "NULL)"
                End If

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateAssetLocationsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' ASSET_LOCATIONS
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Asset Locations"
        SQL = "SELECT * FROM ASSET_LOCATIONS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then
            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO ASSET_LOCATIONS (EnumAssetType, ID, LocationID, FlagID) VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateCharacterSkillsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewFields = True

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT ACTIVE_SKILL_LEVEL FROM ESI_PUBLIC_CACHE_DATES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If IsNothing(readerUpdate) Then
            ' They don't have the new field
            HaveNewFields = False
        End If

        ' CHARACTER_SKILLS
        ProgramErrorLocation = "Cannot copy Character Skills"
        SQL = "SELECT * FROM CHARACTER_SKILLS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)

        While readerUpdate.Read
            SQL = "INSERT INTO CHARACTER_SKILLS VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & "," ' Now trained level
            If HaveNewFields Then
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(7))
            Else
                SQL &= "0," ' New active field, so just set to zero to start and let it refresh
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6))
            End If

            SQL &= ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)
        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateCharacterStandingsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' CHARACTER_STANDINGS
        ProgramErrorLocation = "Cannot copy Character Standings"
        SQL = "SELECT * FROM CHARACTER_STANDINGS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)

        While readerUpdate.Read
            SQL = "INSERT INTO CHARACTER_STANDINGS VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4))
            SQL &= ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)
        readerUpdate.Close()

    End Sub

    Public Sub UpdateCurrentResearchAgentsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' CURRENT_RESEARCH_AGENTS
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy CURRENT_RESEARCH_AGENTS Data table"
        SQL = "SELECT * FROM CURRENT_RESEARCH_AGENTS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then
            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO CURRENT_RESEARCH_AGENTS VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

    End Sub

    Private Sub UpdateMarketHistoryTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' MARKET_HISTORY
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Market History"
        SQL = "SELECT * FROM MARKET_HISTORY"

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then

            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO MARKET_HISTORY VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(7))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateMarketHistoryUpdateCacheTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' MARKET_HISTORY_UPDATE_CACHE
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Market History Cache"
        SQL = "SELECT * FROM MARKET_HISTORY_UPDATE_CACHE"

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then

            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO MARKET_HISTORY_UPDATE_CACHE VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateMarketOrdersTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' MARKET_ORDERS
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Market Orders"
        SQL = "SELECT * FROM MARKET_ORDERS"

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then

            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO MARKET_ORDERS VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateMarketOrdersUpdateCacheTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' MARKET_ORDERS_UPDATE_CACHE
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Market Orders"
        SQL = "SELECT * FROM MARKET_ORDERS_UPDATE_CACHE"

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then

            Call BeginSQLiteTransaction(DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO MARKET_ORDERS_UPDATE_CACHE VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateStructureMarketOrdersTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewAPIFields As Boolean

        ProgramErrorLocation = "Cannot copy STRUCTURE_MARKET_ORDERS"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM STRUCTURE_MARKET_ORDERS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have it
            SQL = "SELECT * FROM STRUCTURE_MARKET_ORDERS"
        Else
            ' They don't have the table, so exit because this is a required table and will come with nothing in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read

            SQL = "INSERT INTO STRUCTURE_MARKET_ORDERS VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateStructureMarketOrdersUpdateCacheTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ProgramErrorLocation = "Cannot copy STRUCTURE_MARKET_ORDERS_UPDATE_CACHE"

        ' See if they have the table
        On Error Resume Next
        SQL = "SELECT 'X' FROM STRUCTURE_MARKET_ORDERS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have it
            SQL = "SELECT * FROM STRUCTURE_MARKET_ORDERS_UPDATE_CACHE"
        Else
            ' They don't have the table, so exit because this is a required table and will come with nothing in it to start
            Exit Sub
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)

        While readerUpdate.Read
            SQL = "INSERT INTO STRUCTURE_MARKET_ORDERS_UPDATE_CACHE VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1))
            SQL &= ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateStationsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewAPIFields As Boolean

        ProgramErrorLocation = "Cannot copy STATIONS"

        ' See if they have the new field with data
        On Error Resume Next
        SQL = "SELECT * FROM STATIONS WHERE MANUAL_ENTRY <> 0"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If IsNothing(readerUpdate) Then
            ' They don't have the field, so exit because they will add data to it for next update
            Exit Sub
        End If

        Call BeginSQLiteTransaction(DBNEW)

        While readerUpdate.Read
            ' Insert all the manually entered stations/structures
            SQL = "INSERT INTO STATIONS VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ")"

            Call ExecuteNonQuerySQL(SQL, DBNEW)

        End While

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdatePriceProfilesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' PRICE_PROFILES
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Price Profiles"
        ' Get all settings so it gets all
        SQL = "SELECT * FROM PRICE_PROFILES GROUP BY ID, GROUP_NAME, PRICE_TYPE, REGION_NAME, SOLAR_SYSTEM_NAME, PRICE_MODIFIER, RAW_MATERIAL"

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then

            Call BeginSQLiteTransaction(DBNEW)

            ' Since they have the table, load what they have and delete what is there in the new db
            Call ExecuteNonQuerySQL("DELETE FROM PRICE_PROFILES", DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO PRICE_PROFILES VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateIndustryJobsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String
        Dim HaveNewIndustryJobsTable As Boolean

        ' INDUSTRY_JOBS
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy Industry Jobs"
        SQL = "SELECT * FROM INDUSTRY_JOBS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then

            ' See if they have the new or old format for industry jobs
            On Error Resume Next
            SQL = "SELECT successfulRuns FROM INDUSTRY_JOBS"
            DBCommand = New SQLiteCommand(SQL, DBOLD)
            readerCheck = DBCommand.ExecuteReader
            ' If it didn't error, they have the field for the old table
            If Err.Number = 0 Then
                HaveNewIndustryJobsTable = True
                readerCheck.Close()
            Else
                HaveNewIndustryJobsTable = False
            End If
            On Error GoTo 0

            readerCheck = Nothing

            Call BeginSQLiteTransaction(DBNEW)

            ' If they have the new table, copy if the old then just leave it blank since the api will update
            If HaveNewIndustryJobsTable Then
                ' Copy the current data
                While readerUpdate.Read
                    SQL = "INSERT INTO INDUSTRY_JOBS VALUES ("
                    SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
                    SQL &= BuildInsertFieldString(CStr(readerUpdate.Item(14))) & "," ' Hack to force this to convert to a string from int
                    SQL &= BuildInsertFieldString(readerUpdate.Item(15)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(16)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(17)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(18)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(19)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(20)) & ","
                    SQL &= BuildInsertFieldString(readerUpdate.Item(21)) & ","
                    SQL &= BuildInsertFieldString(CInt(readerUpdate.Item(22)))
                    SQL &= ")"

                    DBCommand = New SQLiteCommand(SQL, DBNEW)
                    DBCommand.ExecuteNonQuery()
                    DBCommand = Nothing
                End While
            End If

            Call CommitSQLiteTransaction(DBNEW)
            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateItemPricesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' ITEM_PRICES
        ProgramErrorLocation = "Cannot copy Item Prices"

        ' See if they have the new fields
        On Error Resume Next
        SQL = "SELECT PRICE_SOURCE FROM ITEM_PRICES_FACT"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have the new fields
            SQL = "SELECT ITEM_ID, PRICE, ADJUSTED_PRICE, AVERAGE_PRICE, RegionORSystem, PRICE_SOURCE FROM ITEM_PRICES_FACT"
        Else
            SQL = "SELECT ITEM_ID, PRICE, ADJUSTED_PRICE, AVERAGE_PRICE, -1, -1 FROM ITEM_PRICES_FACT"
        End If

        On Error GoTo 0

        Call BeginSQLiteTransaction(DBNEW)

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        While readerUpdate.Read
            SQL = "UPDATE ITEM_PRICES_FACT "
            SQL &= "SET PRICE = " & BuildInsertFieldString(readerUpdate.GetDouble(1)) & ", "
            SQL &= "ADJUSTED_PRICE = " & BuildInsertFieldString(readerUpdate.GetDouble(2)) & ", "
            SQL &= "AVERAGE_PRICE = " & BuildInsertFieldString(readerUpdate.GetDouble(3)) & ", "
            SQL &= "RegionORSystem = " & BuildInsertFieldString(readerUpdate.GetDouble(4)) & ", "
            SQL &= "PRICE_SOURCE = " & BuildInsertFieldString(readerUpdate.GetDouble(5)) & " "
            SQL &= "WHERE ITEM_ID = " & BuildInsertFieldString(readerUpdate.GetValue(0))

            DBCommand = New SQLiteCommand(SQL, DBNEW)
            DBCommand.ExecuteNonQuery()
            DBCommand = Nothing
        End While

        Call CommitSQLiteTransaction(DBNEW)
        readerUpdate.Close()
        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateItemPricesCacheTable()
        Dim DBCommand As SQLiteCommand
        Dim readerCheck As SQLiteDataReader
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' ITEM_PRICES_CACHE
        ' See if they have the percentile values first
        ProgramErrorLocation = "Cannot copy Item Price Cache"

        On Error GoTo 0

        readerCheck = Nothing

        ' See if they have the new fields
        On Error Resume Next
        SQL = "SELECT PRICE_SOURCE FROM ITEM_PRICES_CACHE"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        If Not IsNothing(readerUpdate) Then
            ' They have the new fields
            SQL = "SELECT * FROM ITEM_PRICES_CACHE"
        Else
            SQL = "SELECT typeID,buyVolume,buyweightedAvg,buyAvg,buyMax,buyMin,buyStdDev,buyMedian,buyPercentile,buyVariance,"
            SQL &= "sellVolume,sellweightedAvg,sellAvg,sellMax,sellMin,sellStdDev,sellMedian,sellPercentile,sellVariance,RegionORSystem,UpdateDate, -1 FROM ITEM_PRICES_CACHE"
        End If

        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        Call BeginSQLiteTransaction(DBNEW)

        While readerUpdate.Read
            SQL = "INSERT INTO ITEM_PRICES_CACHE VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(7)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(8)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(14)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(15)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(16)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(17)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(18)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(19)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(20)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(21)) & ")"

            DBCommand = New SQLiteCommand(SQL, DBNEW)
            DBCommand.ExecuteNonQuery()

        End While

        readerUpdate.Close()

        Call CommitSQLiteTransaction(DBNEW)

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateOwnedBlueprintsTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' OWNED_BLUEPRINTS
        ProgramErrorLocation = "Cannot copy Owned Blueprints"
        SQL = "SELECT * FROM OWNED_BLUEPRINTS"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader

        ' Copy all data over
        Call BeginSQLiteTransaction(DBNEW)
        While readerUpdate.Read
            SQL = "INSERT INTO OWNED_BLUEPRINTS VALUES ("
            SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(6)) & ","
            SQL &= BuildInsertFieldString(CInt(readerUpdate.Item(7))) & ","
            SQL &= BuildInsertFieldString(CInt(readerUpdate.Item(8))) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(9)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(10)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(11)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(12)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(13)) & ","
            SQL &= BuildInsertFieldString(readerUpdate.Item(14))
            SQL &= ")"

            DBCommand = New SQLiteCommand(SQL, DBNEW)
            DBCommand.ExecuteNonQuery()
        End While

        Call CommitSQLiteTransaction(DBNEW)
        readerUpdate.Close()

    End Sub

    Private Sub UpdateFWSystemUpgradesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' FW_SYSTEM_UPGRADES
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy FW_SYSTEM_UPGRADES Data table"
        SQL = "SELECT * FROM FW_SYSTEM_UPGRADES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then
            Call BeginSQLiteTransaction(DBNEW)

            ' Copy the table data
            SQL = "SELECT * FROM FW_SYSTEM_UPGRADES"
            DBCommand = New SQLiteCommand(SQL, DBOLD)
            readerUpdate = DBCommand.ExecuteReader

            While readerUpdate.Read
                SQL = "INSERT INTO FW_SYSTEM_UPGRADES VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateIndustryFacilitiesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' INDUSTRY_FACILITIES
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy INDUSTRY_FACILITIES Data table"
        SQL = "SELECT * FROM INDUSTRY_FACILITIES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then
            Call BeginSQLiteTransaction(DBNEW)

            ' Delete all the station records first, then reload
            SQL = "DELETE FROM INDUSTRY_FACILITIES"
            Call ExecuteNonQuerySQL(SQL, DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO INDUSTRY_FACILITIES VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(5)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(6))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

    Private Sub UpdateIndustrySystemCostIndiciesTable()
        Dim DBCommand As SQLiteCommand
        Dim readerUpdate As SQLiteDataReader
        Dim SQL As String

        ' INDUSTRY_SYSTEMS_COST_INDICIES
        On Error Resume Next
        ProgramErrorLocation = "Cannot copy INDUSTRY_SYSTEMS_COST_INDICIES Data table"
        SQL = "SELECT * FROM INDUSTRY_SYSTEMS_COST_INDICIES"
        DBCommand = New SQLiteCommand(SQL, DBOLD)
        readerUpdate = DBCommand.ExecuteReader
        On Error GoTo 0

        ' They might not have this table yet.
        If Not IsNothing(readerUpdate) Then
            Call BeginSQLiteTransaction(DBNEW)

            ' Delete all the records first, then reload (if any)
            SQL = "DELETE FROM INDUSTRY_SYSTEMS_COST_INDICIES"
            Call ExecuteNonQuerySQL(SQL, DBNEW)

            While readerUpdate.Read
                SQL = "INSERT INTO INDUSTRY_SYSTEMS_COST_INDICIES VALUES ("
                SQL &= BuildInsertFieldString(readerUpdate.Item(0)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(1)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(2)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(3)) & ","
                SQL &= BuildInsertFieldString(readerUpdate.Item(4))
                SQL &= ")"

                Call ExecuteNonQuerySQL(SQL, DBNEW)

            End While

            Call CommitSQLiteTransaction(DBNEW)

            readerUpdate.Close()
        End If

        readerUpdate = Nothing
        DBCommand = Nothing

    End Sub

#End Region

End Class