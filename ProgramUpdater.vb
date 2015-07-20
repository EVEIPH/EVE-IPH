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
        If Directory.Exists(UpdaterFilePath) Then
            ' Delete what is there and replace
            Dim ImageDir As New DirectoryInfo(UpdaterFilePath)
            ImageDir.Delete(True)
        End If

        Directory.CreateDirectory(UpdaterFilePath)

        ' Get the newest updatefile from server
        If TestingVersion Then
            ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateTestServerURL, UpdaterFilePath & XMLLatestVersionTest)
        Else
            ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateServerURL, UpdaterFilePath & XMLLatestVersionFileName)
        End If

    End Sub

    ' Just deletes the files and directory for updates
    Public Sub CleanUpFiles()

        On Error Resume Next

        ' Delete the updates folder (new one will be made in updater)
        Dim ImageDir As New DirectoryInfo(UpdaterFilePath)
        ImageDir.Delete(True)

    End Sub

    ' Checks the updater file to see if it needs to be updated, updates it if needed, then shells to the updater and closes this application
    Public Sub RunUpdate()
        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim UpdaterServerFileURL As String = ""
        Dim UpdaterServerFileMD5 As String = ""
        Dim UpdaterLocalFileMD5 As String = ""
        Dim UpdaterServerFilePath As String = ""

        Dim fi As FileInfo

        On Error GoTo DownloadError

        ' Wait for a second before running - might solve the problem with incorrectly suggesting an update
        Threading.Thread.Sleep(2000)

        'Load the server XML file
        m_xmld.Load(ServerXMLLastUpdatePath)
        m_nodelist = m_xmld.SelectNodes("/EVEIPH/result/rowset/row")

        ' Loop through the nodes and find the MD5 and download URL for the updater
        For Each m_node In m_nodelist
            If m_node.Attributes.GetNamedItem("Name").Value = UpdaterFileName Then
                UpdaterServerFileMD5 = m_node.Attributes.GetNamedItem("MD5").Value
                UpdaterServerFileURL = m_node.Attributes.GetNamedItem("URL").Value
                Exit For
            End If
        Next

        ' Get the local updater MD5, if not found, we run update anyway
        UpdaterLocalFileMD5 = MD5CalcFile(UserWorkingFolder & UpdaterFileName)

        If UpdaterLocalFileMD5 <> UpdaterServerFileMD5 Then
            ' Update the updater file, download the new file
            UpdaterServerFilePath = DownloadFileFromServer(UpdaterServerFileURL, UpdaterFilePath & UpdaterFileName)

            If MD5CalcFile(UpdaterServerFilePath) <> UpdaterServerFileMD5 Then
                UpdaterServerFilePath = DownloadFileFromServer(UpdaterServerFileURL, UpdaterFilePath & UpdaterFileName)

                If MD5CalcFile(UpdaterServerFilePath) <> UpdaterServerFileMD5 Or UpdaterServerFilePath = "" Then
                    ' Download error, just leave because we want this update to go through before running
                    GoTo DownloadError
                End If
            End If

            ' Delete the old updater file, rename the new
            If File.Exists(UserWorkingFolder & UpdaterFileName) Then
                File.Delete(UserWorkingFolder & UpdaterFileName)
            End If

            ' Move the downloaded file
            fi = New FileInfo(UpdaterServerFilePath)
            fi.MoveTo(UserWorkingFolder & UpdaterFileName)
        End If

        On Error Resume Next

        ' Don't delete the update file or folder (it will get deleted on startup of this or updater anyway
        ' Perserve the old XML file until we finish the updater - if only the updater needs to be updated, 
        ' then it will copy over the new xml file when it closes

        ' Get the directory path of this program to send to updater
        Dim ProcInfo As New ProcessStartInfo

        ProcInfo.WindowStyle = ProcessWindowStyle.Normal
        ProcInfo.FileName = UpdaterFileName
        ProcInfo.Arguments = String.Empty
        'ProcInfo.UseShellExecute = True

        'ProcInfo.Arguments = "|" & EVEIPHEXEPath ' Add pipe to read the path in the updater

        '' Shell the updater and send an argument to it on as the shell back to path
        'Call Shell(UpdaterFileName)
        Process.Start(ProcInfo)

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
            LocalMD5 = MD5CalcFile(UserWorkingFolder & XMLFile)

            If ServerXMLLastUpdatePath <> "" Then
                ' Get the hash of the server XML
                ServerMD5 = MD5CalcFile(UpdaterFilePath & XMLFile)
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
            Return UpdateCheckResult.UpdateError
        End Try
    End Function

End Class
