Imports System.Collections

Public Class TimePicker

    Dim _99SingleDigitList As New Queue(Of String)
    Dim _99TwoDigitList As New Queue(Of String)
    Dim _23List As New Queue(Of String)
    Dim _59List As New Queue(Of String)

    Public ResetHours As Boolean
    Public ResetMinutes As Boolean
    Public ResetSeconds As Boolean
    Public Event TimeChange(ByVal sender As Object, ByVal e As System.EventArgs)

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

        ' Default to 1 hour
        Days.Text = "0"
        Hours.Text = "01"
        Minutes.Text = "00"
        Seconds.Text = "00"

        ResetHours = True
        ResetMinutes = True
        ResetSeconds = True

    End Sub

    Public Overrides Property Text() As String
        Get
            Dim D As String = Trim(Days.Text)
            Dim H As String = Trim(Hours.Text)
            Dim M As String = Trim(Minutes.Text)
            Dim S As String = Trim(Seconds.Text)
            If D = "" Then
                D = "0"
            End If
            If H = "" Then
                H = "00"
            End If
            If M = "" Then
                M = "00"
            End If
            If S = "" Then
                S = "00"
            End If
            Return (D & " Days " & H & ":" & M & ":" & S)
        End Get

        Set(value As String)
            Try
                ' Add the time from string - X Days
                Dim strArr() As String
                Dim count As Integer

                If value = "" Then
                    ' Default to 1 hour
                    Days.Text = "0"
                    Hours.Text = "01"
                    Minutes.Text = "00"
                    Seconds.Text = "00"
                Else
                    ' Make sure the sent string has no extra spaces that create a blank array entry
                    value = Trim(value)
                    ' Strip off the days portion
                    Dim SentDays As String = value.Substring(0, InStr(UCase(value), "DAY") - 2)
                    Days.Text = SentDays

                    Dim Time As String = value.Substring(InStr(UCase(value), "DAY") + 4)

                    ' Break up the time sections
                    strArr = Time.Split(New Char() {":"c})

                    For count = 0 To strArr.Count - 1
                        Select Case count
                            Case 0
                                Hours.Text = Trim(strArr(count))
                            Case 1
                                Minutes.Text = Trim(strArr(count))
                            Case 2
                                Seconds.Text = Trim(strArr(count))
                        End Select
                    Next
                End If
            Catch ex As Exception
                ' Default to 1 hour and exit
                Days.Text = "0"
                Hours.Text = "01"
                Minutes.Text = "00"
                Seconds.Text = "00"
            End Try

        End Set

    End Property

    Private Sub Days_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Days.SelectedItemChanged

        RaiseEvent TimeChange(sender, e)

    End Sub

    Private Sub Hours_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Hours.SelectedItemChanged

        RaiseEvent TimeChange(sender, e)

    End Sub

    Private Sub Minutes_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Minutes.SelectedItemChanged

        RaiseEvent TimeChange(sender, e)

    End Sub

    Private Sub Seconds_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Seconds.SelectedItemChanged

        RaiseEvent TimeChange(sender, e)

    End Sub

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
