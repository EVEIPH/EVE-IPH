
Imports System.Data.SQLite

Public Class frmConversiontoOreSettings

    Public Sub New(FormSettings As ConversionToOreSettings)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Settings
        With FormSettings
            Select Case .ConversionType
                Case rbtnConversionNone.Text
                    rbtnConversionNone.Checked = True
                Case rbtnConversionOre.Text
                    rbtnConversionOre.Checked = True
                Case rbtnConversionIce.Text
                    rbtnConversionIce.Checked = True
                Case rbtnConversionOreIce.Text
                    rbtnConversionOreIce.Checked = True
            End Select

            chkCompressedOre.Checked = .CompressedOre
            chkCompressedIce.Checked = .CompressedIce

            Select Case .MinimizeOn
                Case rbtnRefinePrice.Text
                    rbtnRefinePrice.Checked = True
                Case rbtnOrePrice.Text
                    rbtnOrePrice.Checked = True
                Case rbtnOreVolume.Text
                    rbtnOreVolume.Checked = True
            End Select

            chkHighSec.Checked = .HighSec
            chkLowSec.Checked = .LowSec
            chkNullSec.Checked = .NullSec

            cmbNullAnomLevel.Text = .IndyLevel

            chkIncludeBaseOre.Checked = .OreVariant0
            chkInclude5percent.Checked = .OreVariant5
            chkInclude10percent.Checked = .OreVariant10

            chkAmarr.Checked = .Amarr
            chkCaldari.Checked = .Caldari
            chkGallente.Checked = .Gallente
            chkMinmatar.Checked = .Minmatar

            chkC1.Checked = .C1
            chkC2.Checked = .C2
            chkC3.Checked = .C3
            chkC4.Checked = .C4
            chkC5.Checked = .C5
            chkC6.Checked = .C6

        End With

        Call RefreshOreList()

    End Sub

    ' Refreshes the Ore list based on the options selected 
    Private Sub RefreshOreList()
        Dim SQL As String = ""
        Dim rsOres As SQLiteDataReader

        SQL = "Select scope, status, purpose FROM ESI_STATUS_ITEMS, ESI_ENDPOINT_ROUTE_TO_SCOPE WHERE route = endpoint_route"
        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsOres = DBCommand.ExecuteReader

        While rsOres.Read
            ' Check each one based on name

        End While

        ' Finaly update with any overrides
        Call CheckOverrides()

    End Sub

    ' If the user selected to override any ores different than selection (e.g. highsec ores but they want Talassonite) then set here
    Private Sub CheckOverrides()

    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click

    End Sub


End Class