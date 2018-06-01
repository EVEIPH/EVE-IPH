Imports System.Web
Imports System.IO
Imports System.Net.Mail

Public Class frmError

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        End
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmError_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtError.Text = frmErrorText
        Me.Activate()
        Me.TopMost = True
    End Sub

    ' Emails my admin email the error message and other information so I don't have to ask for it all the time
    Private Sub btnSendReport_Click(sender As System.Object, e As System.EventArgs) Handles btnSendReport.Click
        'Dim mail As New MailMessage
        'Dim SMTP As New SmtpClient

        'Try
        '    ' Set the message
        '    mail.From = New MailAddress("ZifrianEVE@gmail.com", "User: " & SelectedCharacter.Name)
        '    mail.To.Add("ZifrianEVE@gmail.com")
        '    mail.Subject = "IPH Error Report (" & FormatDateTime(Now, DateFormat.ShortDate) & " " & FormatDateTime(Now, DateFormat.ShortTime) & ")"
        '    mail.Body = frmErrorText

        '    SMTP.Port = 25
        '    SMTP.EnableSsl = True
        '    'SMTP.Credentials = ""

        'Catch ex As Exception
        '    MsgBox("Unable to send error report at this time.", vbInformation, Application.ProductName)
        'End Try

    End Sub

End Class