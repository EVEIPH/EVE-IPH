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
            ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateTestFileURL, UpdaterFilePath & XMLLatestVersionTest)
        Else
            ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateFileURL, UpdaterFilePath & XMLLatestVersionFileName)
        End If

    End Sub

    ' Just deletes the files and directory for updates
    Public Sub CleanUpFiles()

        On Error Resume Next

        ' Delete the updates folder (new one will be made in updater)
        Dim ImageDir As New DirectoryInfo(UpdaterFilePath)
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

        Dim IonicServerFileURL As String = ""
        Dim IonicServierFileMD5 As String = ""

        Dim fi As FileInfo

        On Error GoTo DownloadError

        ' Wait for a second before running - might solve the problem with incorrectly suggesting an update
        Threading.Thread.Sleep(2000)

        'Load the server XML file
        m_xmld.Load(ServerXMLLastUpdatePath)
        m_nodelist = m_xmld.SelectNodes("/EVEIPH/result/rowset/row")

        ' Loop through the nodes and find the MD5 and download URL for the updater and any other files necessary to load the updater
        For Each m_node In m_nodelist
            If m_node.Attributes.GetNamedItem("Name").Value = UpdaterFileName Then
                TempUpdateFile.MD5 = m_node.Attributes.GetNamedItem("MD5").Value
                TempUpdateFile.URL = m_node.Attributes.GetNamedItem("URL").Value
                TempUpdateFile.FileName = UpdaterFileName
                UpdateFiles.Add(TempUpdateFile)
            ElseIf m_node.Attributes.GetNamedItem("Name").Value = IonicZipFileName Then ' We need to download this in the main program because the update won't load without it
                TempUpdateFile.MD5 = m_node.Attributes.GetNamedItem("MD5").Value
                TempUpdateFile.URL = m_node.Attributes.GetNamedItem("URL").Value
                TempUpdateFile.FileName = IonicZipFileName
                UpdateFiles.Add(TempUpdateFile)
            End If
        Next

        ' Download the two files if necessary
        For Each UpdateFile In UpdateFiles
            ' Download each file needed
            If DownloadUpdatedFile(UpdateFile.MD5, UpdateFile.URL, UpdateFile.FileName) = "Download Error" Then
                GoTo DownloadError
            End If
        Next

        On Error Resume Next

        ' Don't delete the update file or folder (it will get deleted on startup of this or updater anyway
        ' Perserve the old XML file until we finish the updater - if only the updater needs to be updated, 
        ' then it will copy over the new xml file when it closes

        ' Get the directory path of this program to send to updater
        Dim ProcInfo As New ProcessStartInfo

        ProcInfo.WindowStyle = ProcessWindowStyle.Normal
        ProcInfo.FileName = UpdaterFileName
        ProcInfo.Arguments = String.Empty
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

    Private Function DownloadUpdatedFile(ServerFileMD5 As String, ServerFileURL As String, Filename As String) As String
        Dim LocalFileMD5 As String = ""
        Dim ServerFilePath As String = ""
        Dim fi As FileInfo

        ' Get the local updater MD5, if not found, we run update anyway
        LocalFileMD5 = MD5CalcFile(UserWorkingFolder & UpdaterFileName)

        If LocalFileMD5 <> ServerFileMD5 Then
            ' Update the updater file, download the new file
            ServerFilePath = DownloadFileFromServer(ServerFileURL, UpdaterFilePath & Filename)

            If MD5CalcFile(ServerFilePath) <> ServerFileMD5 Then
                ' Try again
                ServerFilePath = DownloadFileFromServer(ServerFileURL, UpdaterFilePath & Filename)

                If MD5CalcFile(ServerFilePath) <> ServerFileMD5 Or ServerFilePath = "" Then
                    ' Download error, just leave because we want this update to go through before running
                    Return "Download Error"
                End If
            End If

            ' Delete the old file, rename the new
            If File.Exists(UserWorkingFolder & Filename) Then
                File.Delete(UserWorkingFolder & Filename)
            End If

            ' Move the downloaded file
            fi = New FileInfo(ServerFilePath)
            fi.MoveTo(UserWorkingFolder & Filename)
        End If

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
