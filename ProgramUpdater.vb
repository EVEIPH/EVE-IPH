Imports System.IO
Imports System.Xml

Public Enum UpdateCheckResult
    UpdateError = -1
    UpToDate = 0
    UpdateAvailable = 1
End Enum

' Class for checking for updates and storing data for comparision
Public Class ProgramUpdater

    ' XML Temp file path for server file
    Public ServerXMLLastUpdatePath As String

    ' When constructed, it will load the settings XML file into the class
    Public Sub New()

        ' Create the updates folder
        If Directory.Exists(Path.Combine(DynamicFilePath, UpdatePath)) Then
            ' Delete what is there and replace
            Dim ImageDir As New DirectoryInfo(Path.Combine(DynamicFilePath, UpdatePath))
            ImageDir.Delete(True)
        End If

        Directory.CreateDirectory(Path.Combine(DynamicFilePath, UpdatePath))

        ' Get the newest updatefile from server
        If TestingVersion Then
            ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateTestFileURL, Path.Combine(DynamicFilePath, UpdatePath, XMLLatestVersionTest))
        Else
            ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateFileURL, Path.Combine(DynamicFilePath, UpdatePath, XMLLatestVersionFileName))
        End If

    End Sub

    ' Just deletes the files and directory for updates
    Public Sub CleanUpFiles()

        On Error Resume Next

        ' Delete the updates folder (new one will be made in updater)
        Dim ImageDir As New DirectoryInfo(Path.Combine(DynamicFilePath, UpdatePath))
        ImageDir.Delete(True)

    End Sub

    Private Structure MD5FileInfo
        Dim MD5 As String
        Dim URL As String
        Dim FileName As String
    End Structure

    ' Checks the updater file to see if it needs to be updated, updates it if needed, then shells to the updater and closes this application
    Public Sub RunUpdate()
        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim UpdateFiles As New List(Of MD5FileInfo)
        Dim TempUpdateFile As New MD5FileInfo

        Dim UpdaterServerFileURL As String = ""
        Dim UpdaterServerFileMD5 As String = ""

        Dim LaunchSQLiteDLLUpdater As Boolean = False

        Dim fi As FileInfo

        On Error GoTo DownloadError

        ' Wait for a second before running - might solve the problem with incorrectly suggesting an update
        Threading.Thread.Sleep(2000)

        'Load the server XML file
        m_xmld.Load(ServerXMLLastUpdatePath)
        m_nodelist = m_xmld.SelectNodes("/EVEIPH/result/rowset/row")

        ' Loop through the nodes and find the MD5 and download URL for the updater and any other files necessary to load and run the updater program
        ' Below for the dll updates, if I push new ones, put in this fix to launch a small copy program to copy the files over since it will error if they are used by the applications
        For Each m_node In m_nodelist
            Select Case m_node.Attributes.GetNamedItem("Name").Value
                Case UpdaterFileName
                    ' Add the file to the update list 
                    TempUpdateFile.MD5 = m_node.Attributes.GetNamedItem("MD5").Value
                    TempUpdateFile.URL = m_node.Attributes.GetNamedItem("URL").Value
                    TempUpdateFile.FileName = m_node.Attributes.GetNamedItem("Name").Value
                    UpdateFiles.Add(TempUpdateFile)
                    'Case "System.Data.SQLite.dll", "SQLite.Interop.dll"
                    '    ' These require a quick copy over first before launching the updater (which presumably will use the same libraries)
                    '    ' Check MD5 hash and if different, copy
                    '    If m_node.Attributes.GetNamedItem("MD5").Value <> MD5CalcFile(Path.Combine(DynamicFilePath, m_node.Attributes.GetNamedItem("Name").Value)) Then
                    '        ' If either or doesn't match, update both because they are dependent files
                    '        LaunchSQLiteDLLUpdater = True
                    '    End If
            End Select
        Next

        ' Download the updater file if needed
        For Each UpdateFile In UpdateFiles
            If DownloadUpdatedFile(UpdateFile.MD5, UpdateFile.URL, UpdateFile.FileName) = "Download Error" Then
                GoTo DownloadError
            End If
        Next

        On Error Resume Next
        ' Don't delete the update file or folder (it will get deleted on startup of this or updater anyway
        ' Perserve the old XML file until we finish the updater - if only the updater needs to be updated, 
        ' then it will copy over the new xml file when it closes
        Dim Proc As New Process

        If LaunchSQLiteDLLUpdater Then
            ' Need to launch the DLL copy process first, then it will launch the updater - this is needed if the file is locked by the programs using it
            Proc.StartInfo.FileName = Path.Combine(DynamicFilePath, SQLiteDLLUpdater)
        Else
            ' Launch the updater process only
            Proc.StartInfo.FileName = Path.Combine(DynamicFilePath, UpdaterFileName)
        End If

        Proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Proc.StartInfo.Arguments = Path.GetDirectoryName(Application.ExecutablePath)
        Proc.Start()

        ' Close this program
        End

DownloadError:

        ' Some sort of problem, we will just update the whole thing and download the new XML file
        If Err.Description <> "" Then
            MsgBox("Unable to download updates at this time. Please try again later." & Environment.NewLine & "Error: " & Err.Description, vbCritical, Application.ProductName)
        Else
            MsgBox("Unable to download updates at this time. Please try again later.", vbCritical, Application.ProductName)
        End If

        Exit Sub

    End Sub

    Private Function DownloadUpdatedFile(ServerFileMD5 As String, ServerFileURL As String, Filename As String) As String
        Dim LocalFileMD5 As String = ""
        Dim ServerFilePath As String = ""
        Dim fi As FileInfo

        Try

            ' Get the local updater MD5, if not found, we run update anyway
            LocalFileMD5 = MD5CalcFile(Path.Combine(DynamicFilePath, Filename))

            If LocalFileMD5 <> ServerFileMD5 Then
                ' Update the file, download the new file first
                ServerFilePath = DownloadFileFromServer(ServerFileURL, Path.Combine(DynamicFilePath, UpdatePath, Filename))

                If MD5CalcFile(ServerFilePath) <> ServerFileMD5 Then
                    ' Try again
                    ServerFilePath = DownloadFileFromServer(ServerFileURL, Path.Combine(DynamicFilePath, UpdatePath, Filename))

                    If MD5CalcFile(ServerFilePath) <> ServerFileMD5 Or ServerFilePath = "" Then
                        ' Download error, just leave because we want this update to go through before running
                        Return "Download Error"
                    End If
                End If

                ' Delete the old file, rename the new
                If File.Exists(Path.Combine(DynamicFilePath, Filename)) Then
                    File.Delete(Path.Combine(DynamicFilePath, Filename))
                End If

                ' Move the downloaded file
                fi = New FileInfo(ServerFilePath)
                fi.MoveTo(Path.Combine(DynamicFilePath, Filename))
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return ""

    End Function

    ' Function just takes the download date of the current XML file and compares to one on server. If date is newer, then runs update
    Public Function IsProgramUpdatable() As UpdateCheckResult
        Dim LocalMD5 As String = ""
        Dim ServerMD5 As String = ""
        Dim XMLFile As String = ""

        Try
            If TestingVersion Then
                XMLFile = XMLLatestVersionTest
            Else
                XMLFile = XMLLatestVersionFileName
            End If

            ' Get the hash of the local XML
            LocalMD5 = MD5CalcFile(Path.Combine(DynamicFilePath, XMLFile))

            If ServerXMLLastUpdatePath <> "" Then
                ' Get the hash of the server XML
                ServerMD5 = MD5CalcFile(Path.Combine(DynamicFilePath, UpdatePath, XMLFile))
            Else
                Return UpdateCheckResult.UpdateError
            End If

            ' If the hashes are not equal, then we want to run the update
            If LocalMD5 <> ServerMD5 Then
                Return UpdateCheckResult.UpdateAvailable
            Else ' No update needed
                Return UpdateCheckResult.UpToDate
            End If

        Catch ex As Exception
            ' File didn't download, so either try again later or some other error that is unhandled
            MsgBox(ex.Message)
            WriteMsgToLog("IsProgramUpdatable" & ex.Message)
            Return UpdateCheckResult.UpdateError
        End Try
    End Function

End Class
