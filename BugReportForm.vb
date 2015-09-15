Imports System.Management

Public Class BugReportForm

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(e As UnhandledExceptionEventArgs)
        Me.New()
        Dim ex As Exception = CType(e.ExceptionObject, Exception)
        StackTraceBox.Text = "Source: " & ex.Source & Environment.NewLine & "Message: " & ex.Message _
            & Environment.NewLine & "Stack Trace: " & ex.StackTrace
    End Sub

    Private Sub BugReportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OSBox.Text = GetOSName()
    End Sub

    Private Function GetOSName() As String
        Return (From x In New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType(Of ManagementObject)() _
                    Select x.GetPropertyValue("Caption")).FirstOrDefault().ToString

    End Function

    Public Shared Sub OpenBugReportForm(e As UnhandledExceptionEventArgs)
        Dim brf As New BugReportForm(e)
        Dim activeForm = Form.ActiveForm
        If activeForm IsNot Nothing Then
            brf.TabScreenBox.Text = activeForm.Text
        End If
        brf.ShowDialog()
    End Sub

    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click
        Dim doubleNewline = Environment.NewLine & Environment.NewLine
        Dim errorDetails = "Operating System: " & Me.OSBox.Text & _
            doubleNewline & "Screen/Tab: " & Me.TabScreenBox.Text & _
            doubleNewline & "Repro Steps: " & Me.ReproStepsBox.Text & _
            doubleNewline & "Link to Screenshot: " & Me.ScreenLinkBox.Text & _
            doubleNewline & "Message and Stack Trace: " & Environment.NewLine & Me.ScreenLinkBox.Text

        Clipboard.SetText(errorDetails)
    End Sub
End Class