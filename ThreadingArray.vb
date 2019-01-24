Imports System.Threading

Public Class ThreadingArray
    ' Keeps an array of threads if we need to abort update
    Private ThreadsArray As List(Of Thread)

    Public Sub New()
        ThreadsArray = New List(Of Thread)
    End Sub

    ' Adds a thread to the array
    Public Sub AddThread(T As Thread)
        ThreadsArray.Add(T)
    End Sub

    ' Kills all the threads in the class
    Public Sub StopAllThreads()
        ' Kill all the threads
        For i = 0 To ThreadsArray.Count - 1
            If ThreadsArray(i).IsAlive Then
                ThreadsArray(i).Abort()
            End If
        Next
    End Sub

    ' Returns true if all threads are complete, else false
    Public Function Complete() As Boolean

        For Each T In ThreadsArray
            If T.ThreadState = ThreadState.Running Or T.ThreadState = ThreadState.WaitSleepJoin Then
                ' Check if the call to stop threads is flagged
                If CancelThreading Then
                    Return True
                End If
                ' Still working on at least 1 thread, so exit
                Return False
            End If
        Next

        ' Must all be complete
        Return True

    End Function

End Class
