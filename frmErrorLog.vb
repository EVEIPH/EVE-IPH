Imports System.IO

Public Class frmErrorLog

    Private LogFilePath As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LogFilePath = Path.Combine(DynamicFilePath, "EVEIPH.log")

    End Sub

    Private Sub OKButton_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub frmPatchNotes_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        Me.Refresh()
        Application.DoEvents()

        If File.Exists(LogFilePath) Then
            Application.UseWaitCursor = True

            ' Load the text
            txtLog.Text = My.Computer.FileSystem.ReadAllText(LogFilePath)

            Application.UseWaitCursor = False
            Application.DoEvents()
        Else
            MsgBox("Unable to locate an EVEIPH.log file", vbInformation, Application.ProductName)
        End If

    End Sub

    Private Sub btnClearLog_Click(sender As Object, e As EventArgs) Handles btnClearLog.Click
        ' Just delete the file and clear the screen
        File.Delete(LogFilePath)
        txtLog.Text = ""
        MsgBox("The EVEIPH Error Log has been reset.", vbInformation, Application.ProductName)

    End Sub

    Private Sub btnCopyLog_Click(sender As Object, e As EventArgs) Handles btnCopyLog.Click
        CopyTextToClipboard(txtLog.Text)
    End Sub

End Class