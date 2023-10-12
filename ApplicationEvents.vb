Namespace My

    ' The following events are available for MyApplication:
    '
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException

            If ErrorTracker <> "" Then
                Call WriteMsgToLog(e.Exception.ToString & vbCrLf & "Error Tracking: " & ErrorTracker)
            Else
                Call WriteMsgToLog(e.Exception.ToString)
            End If

            Dim f2 = New frmError
            frmErrorText = "An Unhandled Exception has occured and EVE Isk per Hour will now close."
            frmErrorText &= Environment.NewLine & "This error occured in EVE IPH Version: " & My.Application.Info.Version.ToString
            frmErrorText &= Environment.NewLine & Environment.NewLine & "Please fill out the following information so I can reproduce the bug"
            frmErrorText &= Environment.NewLine & Environment.NewLine & "What is your Operating System? "
            frmErrorText &= Environment.NewLine & "What tab or screen did the error occur? "
            frmErrorText &= Environment.NewLine & "What are the steps to reproduce the Error? "
            frmErrorText &= Environment.NewLine & "Web link to a screenshot of your error: "
            frmErrorText &= Environment.NewLine & "In addition to a screenshot, copy the data below and send to developer."
            frmErrorText &= Environment.NewLine & Environment.NewLine & "Source: " & e.Exception.Source
            frmErrorText &= Environment.NewLine & "Message: " & e.Exception.Message & vbCrLf
            frmErrorText &= "Raw Error Text: " & e.Exception.ToString & vbCrLf
            If ErrorTracker <> "" Then
                frmErrorText &= "Error Tracking: " & ErrorTracker
            End If

            f2.ShowDialog()

        End Sub
    End Class

End Namespace