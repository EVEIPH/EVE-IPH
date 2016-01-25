Imports System.Collections

Public Class TimePicker

    Dim _99SingleDigitList As New Queue(Of String)
    Dim _99TwoDigitList As New Queue(Of String)
    Dim _23List As New Queue(Of String)
    Dim _59List As New Queue(Of String)

    Public ResetHours As Boolean
    Public ResetMinutes As Boolean
    Public ResetSeconds As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Build the two types of number lists
        For i = 0 To 99
            _99SingleDigitList.Enqueue(CStr(i))
            _99TwoDigitList.Enqueue(Format(i, "00"))
        Next

        For i = 0 To 23
            _23List.Enqueue(Format(i, "00"))
        Next

        For i = 0 To 59
            _59List.Enqueue(Format(i, "00"))
        Next

        ' Add 99 days for each - maybe later I'll get fancy and limit the numbers based on 24:59:59
        Days.Items.Clear()
        Days.Items.AddRange(_99SingleDigitList)

        Hours.Items.Clear()
        Hours.Items.AddRange(_99TwoDigitList)

        Minutes.Items.Clear()
        Minutes.Items.AddRange(_99TwoDigitList)

        Seconds.Items.Clear()
        Seconds.Items.AddRange(_99TwoDigitList)

        ResetHours = True
        ResetMinutes = True
        ResetSeconds = True

    End Sub

    ' Returns the time entered in seconds
    Public Function GetTime() As Long
        Return ((CInt(Days.Text) * 86400) + (CInt(Hours.Text) * 3600) + (CInt(Minutes.Text) * 60) + CInt(Seconds.Text))
    End Function

    Private Sub Days_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles Days.KeyDown
        If (e.KeyValue <> Keys.Delete And e.KeyValue <> Keys.Back And e.KeyValue <> Keys.Left And e.KeyValue <> Keys.Right) And Len(Days.Text) >= 2 Then
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub Hours_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles Hours.KeyDown
        If (e.KeyValue <> Keys.Delete And e.KeyValue <> Keys.Back And e.KeyValue <> Keys.Left And e.KeyValue <> Keys.Right) And Len(Hours.Text) >= 2 Then
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub Minutes_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles Minutes.KeyDown
        If (e.KeyValue <> Keys.Delete And e.KeyValue <> Keys.Back And e.KeyValue <> Keys.Left And e.KeyValue <> Keys.Right) And Len(Minutes.Text) >= 2 Then
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub Seconds_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles Seconds.KeyDown
        If (e.KeyValue <> Keys.Delete And e.KeyValue <> Keys.Back And e.KeyValue <> Keys.Left And e.KeyValue <> Keys.Right) And Len(Seconds.Text) >= 2 Then
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    'Private Sub Days_SelectedItemChanged(sender As Object, e As System.EventArgs) Handles Days.SelectedItemChanged
    '    Dim Oldvalue As String

    '    If Days.Text = "0" Then
    '        ' reset the hours to allow to go to 99 - save the old value and reset
    '        Oldvalue = Hours.Text
    '        Hours.Items.Clear()
    '        Hours.Items.AddRange(_99TwoDigitList)
    '        Hours.Text = Oldvalue
    '        ResetHours = True
    '    Else
    '        ' Need to load hours back up - just one time
    '        If ResetHours Then
    '            If Val(Hours.Text) < 24 Then
    '                Oldvalue = Hours.Text
    '            Else
    '                Oldvalue = "23"
    '            End If
    '            Hours.Items.Clear()
    '            Hours.Items.AddRange(_23List)
    '            Hours.Text = Oldvalue
    '            ResetHours = False
    '        End If
    '    End If
    'End Sub

    'Private Sub Hours_SelectedItemChanged(sender As System.Object, e As System.EventArgs) Handles Hours.SelectedItemChanged
    '    Dim Oldvalue As String

    '    If Hours.Text = "00" Then
    '        ' reset the hours to allow to go to 99 - save the old value and reset
    '        Oldvalue = Minutes.Text
    '        Minutes.Items.Clear()
    '        Minutes.Items.AddRange(_99TwoDigitList)
    '        Minutes.Text = Oldvalue
    '        ResetMinutes = True
    '    Else
    '        ' Need to load hours back up - just one time
    '        If ResetHours Then
    '            If Val(Minutes.Text) < 59 Then
    '                Oldvalue = Minutes.Text
    '            Else
    '                Oldvalue = "59"
    '            End If
    '            Minutes.Items.Clear()
    '            Minutes.Items.AddRange(_59List)
    '            Minutes.Text = Oldvalue
    '            ResetMinutes = False
    '        End If
    '    End If
    'End Sub

End Class
