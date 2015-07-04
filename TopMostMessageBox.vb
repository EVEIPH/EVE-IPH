Public NotInheritable Class TopMostMessageBox

    Public Shared Function Show(ByVal message As String) As DialogResult
        Return Show(message, String.Empty, MessageBoxButtons.OK, Nothing)
    End Function

    Public Shared Function Show(ByVal message As String, ByVal title As String) As DialogResult
        Return Show(message, title, MessageBoxButtons.OK, Nothing)
    End Function

    Public Shared Function Show(ByVal message As String, ByVal title As String, ByVal buttons As MessageBoxButtons, ByVal ProgramIcon As Icon) As DialogResult
        ' Create a host form that is a TopMost window which will be the 
        ' parent of the MessageBox.

        Dim topmostForm As New Form()
        ' We do not want anyone to see this window so position it off the 

        ' visible screen and make it as small as possible
        topmostForm.Size = New System.Drawing.Size(1, 1)
        topmostForm.StartPosition = FormStartPosition.Manual
        If Not IsNothing(ProgramIcon) Then
            topmostForm.Icon = ProgramIcon
        End If

        Dim rect As System.Drawing.Rectangle = SystemInformation.VirtualScreen

        topmostForm.Location = New System.Drawing.Point(rect.Bottom + 10, rect.Right + 10)
        topmostForm.Show()

        ' Make this form the active form and make it TopMost
        topmostForm.Focus()
        topmostForm.BringToFront()
        topmostForm.TopMost = True
        ' Finally show the MessageBox with the form just created as its owner

        Dim result As DialogResult = MessageBox.Show(topmostForm, message, title, buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        topmostForm.Dispose()
        ' clean it up all the way

        Return result
    End Function
End Class
