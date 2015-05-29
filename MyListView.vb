

Public Class MyListView
    Public Event ProcMsg(ByVal m As Message)
    Public Const WM_VSCROLL As Integer = 277

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_VSCROLL
                RaiseEvent ProcMsg(m)
        End Select
        MyBase.WndProc(m)
    End Sub
End Class
