Imports System.IO

Public Class frmPatchNotes

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub OKButton_Click(sender As System.Object, e As System.EventArgs) Handles OKButton.Click
        Me.Hide()
    End Sub

    Private Sub frmPatchNotes_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim FilePath As String
        Dim FileText As String = ""

        Me.Refresh()

        ' Download the patch notes from the server
        FilePath = DownloadFileFromServer(PatchNotesURL, Path.Combine(DynamicFilePath, "Patch Notes.txt"))

        If FilePath = "" Then
            Exit Sub
        End If

        ' Display in Text box
        FileText = File.ReadAllText(FilePath)

        ' Strip off the beginning text 
        txtPatchNotes.Text = FileText.Substring(InStr(FileText, "Version") - 1)

        Application.UseWaitCursor = False
        Application.DoEvents()

    End Sub

End Class