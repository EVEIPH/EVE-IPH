Imports System.Data.SQLite
Imports System.IO

<CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")>
Public Class DBConnection

    Private DB As SQLiteConnection
    Private Lock As New Object

    Public Sub New(ByVal DBFilePath As String, ByVal DBName As String)
        Dim DBFileName As String = Path.Combine(DBFilePath, DBName)

        DB = New SQLiteConnection
        DB.ConnectionString = "Data Source=" & DBFileName & ";Version=3;"
        If DB.State = ConnectionState.Open Then
            DB.Close() ' Check if the DB is open and will lock on re-connection
            DB.Dispose()
            GC.Collect()
            Threading.Thread.Sleep(5000)
            DB.ConnectionString = "Data Source=" & DBFileName & ";Version=3;"
        End If

        Try
            Call OpenDB()
        Catch ex As Exception
            Call DisplayDBException(ex)
            End ' Close program
        End Try

    End Sub

    Private Sub OpenDB()
        DB.Open()
        Call ExecuteNonQuerySQL("PRAGMA auto_vacuum = FULL; PRAGMA synchronous = NORMAL; PRAGMA locking_mode = NORMAL; PRAGMA cache_size = 10000; PRAGMA page_size = 4096; PRAGMA temp_store = DEFAULT; PRAGMA journal_mode = WAL; PRAGMA count_changes = OFF")
    End Sub

    Private Sub DisplayDBException(ThrownException As Exception)
        MsgBox("IPH was unable to open the primary database and will now close." & vbCrLf & vbCrLf & "Error message: " & ThrownException.Message, vbCritical)
        Call WriteMsgToLog(ThrownException.ToString)
    End Sub

    Public Sub CloseDB()
        DB.Close()
        DB.Dispose()
        GC.Collect()
    End Sub

    Public Function DBREf() As SQLiteConnection
        Return DB
    End Function

    Public Sub ExecuteNonQuerySQL(ByVal SQL As String)
        Dim DBExecuteCmd As SQLiteCommand

        ErrorTracker = SQL
        SyncLock Lock
            DBExecuteCmd = DB.CreateCommand
            DBExecuteCmd.CommandText = SQL
            DBExecuteCmd.ExecuteNonQuery()
            DBExecuteCmd.Dispose()
            DBExecuteCmd = Nothing
        End SyncLock

        ErrorTracker = ""

    End Sub

    Public Sub BeginSQLiteTransaction()
        Call ExecuteNonQuerySQL("BEGIN;")
    End Sub

    Public Sub CommitSQLiteTransaction()
        Call ExecuteNonQuerySQL("END;")
    End Sub

    Public Sub RollbackSQLiteTransaction()
        Call ExecuteNonQuerySQL("ROLLBACK;")
    End Sub

    Public Function TransactionActive() As Boolean
        If DB.AutoCommit Then
            Return False
        Else
            Return True
        End If
    End Function

End Class
