
Public Class frmAssetsViewer

    Public Sub New(ByVal AssetType As AssetWindow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Init control and go from there
        Call mainAssetViewer.InitializeControl(AssetType, SelectedCharacter)

    End Sub

    Private Sub frmAssetsViewerr_Load(Sender As Object, e As EventArgs) Handles Me.Load
        Me.Show()
        Application.DoEvents()
        Call mainAssetViewer.RefreshTree()
    End Sub
End Class