
Public Class MyDomainUpDown

    Inherits System.Windows.Forms.DomainUpDown

    Public Event ProcMsg(ByVal m As Message)
    Public Const WM_VSCROLL As Integer = 277

    Public Sub New()

        ' Remove the up down arrow
        Controls(0).Visible = False

        Call InitializeComponent()

    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.Clear(SystemColors.Window) ' clear the color
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
