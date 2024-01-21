Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Security.Cryptography

Public Class frmSQLiteDLLMain

    Private Structure MD5FileInfo
        Dim MD5 As String
        Dim URL As String
        Dim FileName As String
    End Structure

    Const UpdaterFileName As String = "EVEIPH Updater.exe"
    Const SQLiteDBFileName As String = "EVEIPH DB.sqlite"
    Const DynamicAppDataPath As String = "EVE IPH"
    Const UpdatePath As String = "EVE IPH Updates"
    Const XMLUpdateFileURL = "https://raw.githubusercontent.com/EVEIPH/LatestFiles/master/LatestVersionIPH.xml"
    Const XMLLatestVersionFileName As String = "LatestVersionIPH.xml"

    Private DynamicFilePath As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim ServerXMLLastUpdatePath As String
        Dim ROOT_FOLDER As String = ""
        Dim m_xmld As New XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim UpdateFiles As New List(Of MD5FileInfo)
        Dim TempUpdateFile As New MD5FileInfo

        ' The root folder where the exe is located and other files we want to update - this is the installation directory
        ' Get the operating folder from arguments set when shelled
        If Environment.GetCommandLineArgs.Count > 1 Then ' First argument is the filename of the executing program
            For i = 1 To Environment.GetCommandLineArgs.Count - 1
                ROOT_FOLDER &= Environment.GetCommandLineArgs(i).ToString & " "
            Next
        End If

        ' Find out if we are running all in the current folder or with updates and the DB in the appdata folder
        If File.Exists(SQLiteDBFileName) Then
            ' Single folder that we are in, so set the path variables to it for updates
            DynamicFilePath = Path.GetDirectoryName(Application.ExecutablePath)
        Else
            ' They ran the installer (or we assume they did) and all the files are updated in the appdata/roaming folder
            ' Set where files will be updated
            DynamicFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DynamicAppDataPath)
        End If

        ' We know we need to update the two DLLs, so download the Latest update xml file and parse the data from that to update
        ServerXMLLastUpdatePath = DownloadFileFromServer(XMLUpdateFileURL, Path.Combine(DynamicFilePath, UpdatePath, XMLLatestVersionFileName))
        'Load the server XML file
        m_xmld.Load(ServerXMLLastUpdatePath)
        m_nodelist = m_xmld.SelectNodes("/EVEIPH/result/rowset/row")

        ' Copy the two SQLite DLLs
        For Each m_node In m_nodelist
            Select Case m_node.Attributes.GetNamedItem("Name").Value
                Case "System.Data.SQLite.dll", "SQLite.Interop.dll"
                    ' Add the file to the update list 
                    TempUpdateFile.MD5 = m_node.Attributes.GetNamedItem("MD5").Value
                    TempUpdateFile.URL = m_node.Attributes.GetNamedItem("URL").Value
                    TempUpdateFile.FileName = m_node.Attributes.GetNamedItem("Name").Value
                    UpdateFiles.Add(TempUpdateFile)
            End Select
        Next

        ' Download the files
        For Each UpdateFile In UpdateFiles
            If DownloadUpdatedFile(UpdateFile.MD5, UpdateFile.URL, UpdateFile.FileName) = "Download Error" Then
                Call MsgBox("Failed to update SQLite DLLs", vbExclamation)

            End If
        Next

        ' Launch the updater
        Dim Proc As New Process
        Proc.StartInfo.FileName = Path.Combine(DynamicFilePath, UpdaterFileName)
        Proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Proc.StartInfo.Arguments = Path.GetDirectoryName(Trim(ROOT_FOLDER))
        Proc.Start()

        End

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

    ' Downloads the sent file from server and saves it to the root directory as the sent file name
    Public Function DownloadFileFromServer(ByVal DownloadURL As String, ByVal FileName As String) As String
        ' Creating the request And getting the response
        Dim Response As HttpWebResponse
        Dim Request As HttpWebRequest

        ' For reading in chunks of data
        Dim readBytes(4095) As Byte
        ' Create directory if it doesn't exist already
        If Not Directory.Exists(Path.GetDirectoryName(FileName)) Then
            Directory.CreateDirectory(Path.GetDirectoryName(FileName))
        End If
        Dim writeStream As New FileStream(FileName, FileMode.Create)
        Dim bytesread As Integer

        Try 'Checks if the file exist
            Request = DirectCast(HttpWebRequest.Create(DownloadURL), HttpWebRequest)
            Request.Credentials = CredentialCache.DefaultCredentials ' Added 9/27 to attempt to fix error: (407) Proxy Authentication Required.
            Request.Timeout = 50000
            Response = CType(Request.GetResponse, HttpWebResponse)
        Catch ex As Exception
            ' Show error and exit
            'Close the streams
            writeStream.Close()
            MsgBox("An error occurred while downloading update file: " & ex.Message, vbCritical, Application.ProductName)
            Return ""
        End Try

        ' Loop through and get the file in chunks, save out
        Do
            bytesread = Response.GetResponseStream.Read(readBytes, 0, 4096)

            ' No more bytes to read
            If bytesread = 0 Then Exit Do

            writeStream.Write(readBytes, 0, bytesread)
        Loop

        'Close the streams
        Response.GetResponseStream.Close()
        writeStream.Close()

        ' Finally, check if the file is xml or text and adjust the lf to crlf (git saves as unix or lf only)
        If FileName.Contains(".txt") Then 'Or FileName.Contains(".xml") Then
            Dim FileText As String = File.ReadAllText(FileName)
            FileText = FileText.Replace(Chr(10), vbCrLf)
            ' Write the file back out if it's been updated
            File.WriteAllText(FileName, FileText)
        End If

        Return FileName

    End Function

    ' MD5 Hash - specify the path to a file and this routine will calculate your hash
    Public Function MD5CalcFile(ByVal filepath As String) As String

        ' Open file (as read-only) - If it's not there, return ""
        If File.Exists(filepath) Then
            Using reader As New FileStream(filepath, FileMode.Open, FileAccess.Read)
                Using md5 As New MD5CryptoServiceProvider

                    ' hash contents of this stream
                    Dim hash() As Byte = md5.ComputeHash(reader)
                    Dim sb As New Text.StringBuilder(hash.Length * 2)

                    For i As Integer = 0 To hash.Length - 1
                        sb.Append(hash(i).ToString("X2"))
                    Next

                    Return sb.ToString().ToLower

                End Using
            End Using
        End If

        ' Something went wrong
        Return ""

    End Function

    ' SHA Hash
    Public Function HashSHA(InputString As String) As String
        Try
            Dim sha512 As SHA512 = SHA512Managed.Create()
            Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(InputString)
            Dim hash As Byte() = sha512.ComputeHash(bytes)
            Dim stringBuilder As New Text.StringBuilder()

            For i As Integer = 0 To hash.Length - 1
                stringBuilder.Append(hash(i).ToString("X2"))
            Next

            Return stringBuilder.ToString()
        Catch ex As Exception
            Return ""
        End Try

    End Function

End Class