Imports System.Data.SQLite

<CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")>
Public Class DBConnection

    Private DB As SQLiteConnection
    Private Lock As New Object

    Public Sub New(ByVal DBFileName As String)
        DB = New SQLiteConnection
        DB.ConnectionString = "Data Source=" & DBFileName & ";Version=3;"
        If DB.State = ConnectionState.Open Then DB.Close() ' Check if the DB is open and will lock on re-connection
        DB.Open()

        Call ExecuteNonQuerySQL("PRAGMA synchronous = NORMAL; PRAGMA locking_mode = NORMAL; PRAGMA cache_size = 10000; PRAGMA page_size = 4096; PRAGMA temp_store = DEFAULT; PRAGMA journal_mode = TRUNCATE; PRAGMA count_changes = OFF")
        Call ExecuteNonQuerySQL("PRAGMA auto_vacuum = FULL;") ' Keep the DB small

    End Sub

    Public Sub CloseDB()
        DB.Close()
        DB.Dispose()
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
