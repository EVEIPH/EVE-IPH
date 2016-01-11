
Public Class MyListView
    Inherits System.Windows.Forms.ListView

    Public Event ProcMsg(ByVal m As Message)
    Public Const WM_VSCROLL As Integer = 277

    Public Sub New()

        Call InitializeComponent()

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_VSCROLL
                RaiseEvent ProcMsg(m)
        End Select
        MyBase.WndProc(m)
        Me.DoubleBuffered = True
    End Sub

End Class
