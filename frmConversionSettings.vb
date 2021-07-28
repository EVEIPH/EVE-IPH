
Imports System.Data.SQLite

Public Class frmConversiontoOreSettings
    Private FirstFormLoad As Boolean
    Private m_ControlsCollection As ControlsCollection
    Private OreCheckBoxes() As CheckBox
    Private OreLabels() As Label

    Public Sub New()
        FirstFormLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Settings
        With UserConversiontoOreSettings
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

            chkUseBaseOre.Checked = .OreVariant0
            chkUse5percent.Checked = .OreVariant5
            chkUse10percent.Checked = .OreVariant10

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

            gbWHClasses.Enabled = chkWH.Checked
            'cmbNullAnomLevel.Enabled = chkNullSec.Checked
        End With

        m_ControlsCollection = New ControlsCollection(Me)
        ' Get Region check boxes (note index starts at 1)
        OreCheckBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "chkOre"), CheckBox())
        OreLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblOre"), Label())

        Call RefreshOreList()
        FirstFormLoad = False

    End Sub

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

    ' Refreshes the Ore list based on the options selected 
    Private Sub RefreshOreList()
        If FirstLoad Then
            Exit Sub
        End If

        Dim SQL As String = ""
        Dim rsOres As SQLiteDataReader

        ' Check to make sure they have the right stuff checked
        ' - need a variant
        ' - need a space
        ' - if wh, need a class

        ' First determine what type of stuff we are mining
        SQL = "SELECT CASE WHEN groupname = 'Ice' THEN CASE WHEN SUBSTR(ORE_NAME,1,10) ='Compressed' THEN SUBSTR(ORE_NAME,12) ELSE ORE_NAME END ELSE groupName END AS ORE_GROUP "
        SQL &= "FROM ORES, ORE_LOCATIONS, INVENTORY_GROUPS, INVENTORY_TYPES "
        SQL &= "WHERE ORES.ORE_ID = ORE_LOCATIONS.ORE_ID AND ORES.ORE_ID = INVENTORY_TYPES.typeID AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "

        ' Ore Type
        If rbtnConversionOre.Checked Or rbtnConversionOreIce.Checked Then
            SQL &= "AND (BELT_TYPE = 'Ore' AND HIGH_YIELD_ORE IN ("
            ' Add the variant flag with this
            If chkUseBaseOre.Checked Then
                SQL &= "0,"
            End If
            If chkUse5percent.Checked Then
                SQL &= "1,"
            End If
            If chkUse10percent.Checked Then
                SQL &= "2,"
            End If
            SQL = SQL.Substring(0, Len(SQL) - 1) & ")) "
        End If

        If rbtnConversionIce.Checked Or rbtnConversionOreIce.Checked Then
            SQL &= "AND (BELT_TYPE = 'Ice' AND HIGH_YIELD_ORE =-1) "
        End If

        If (chkCompressedIce.Checked And chkCompressedIce.Enabled) Or (chkCompressedOre.Checked And chkCompressedOre.Enabled) Then
            SQL &= "AND COMPRESSED = 1 "
        Else
            SQL &= "AND COMPRESSED = 0 "
        End If

        SQL &= "AND SYSTEM_SECURITY IN ("
        If chkHighSec.Checked Then
            SQL &= "'High Sec',"
        End If
        If chkLowSec.Checked Then
            SQL &= "'Low Sec',"
        End If
        If chkNullSec.Checked Then
            SQL &= "'Null Sec',"
        End If

        ' If WH checked, then add the classes
        If chkWH.Checked = True And chkWH.Enabled Then
            If chkC1.Checked And chkC1.Enabled Then
                SQL &= "'C1',"
            End If
            If chkC2.Checked And chkC2.Enabled Then
                SQL &= "'C2',"
            End If
            If chkC3.Checked And chkC3.Enabled Then
                SQL &= "'C3',"
            End If
            If chkC4.Checked And chkC4.Enabled Then
                SQL &= "'C4',"
            End If
            If chkC5.Checked And chkC5.Enabled Then
                SQL &= "'C5',"
            End If
            If chkC6.Checked And chkC6.Enabled Then
                SQL &= "'C6',"
            End If
        End If
        SQL = SQL.Substring(0, Len(SQL) - 1) & ") "

        ' Now determine what space we are looking at
        SQL &= "AND SPACE IN ("

        If chkAmarr.Checked Then
            SQL &= "'Amarr',"
        End If
        If chkCaldari.Checked Then
            SQL &= "'Caldari',"
        End If
        If chkGallente.Checked Then
            SQL &= "'Gallente',"
        End If
        If chkMinmatar.Checked Then
            SQL &= "'Minmatar',"
        End If
        If chkTriglavian.Checked Then
            SQL &= "'Triglavian',"
        End If
        If chkWH.Checked Then
            SQL &= "'WH',"
        End If
        SQL = SQL.Substring(0, Len(SQL) - 1) & ") "

        SQL &= "GROUP BY ORE_GROUP"

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsOres = DBCommand.ExecuteReader

        Dim OreList As New List(Of String)

        While rsOres.Read
            OreList.Add(rsOres.GetString(0))
        End While

        ' Update all the ore checks
        Call UpdateOreChecks(OreList)

        ' If they have nothing checked, let them know


    End Sub

    Private Sub UpdateOreChecks(SelectedOres As List(Of String))

        ' Disable tab before updates
        tabPageOres.Enabled = False
        tabPageIce.Enabled = False

        ' Clear all checks
        For i = 1 To OreCheckBoxes.Count - 1
            OreCheckBoxes(i).Checked = False
        Next

        For i = 1 To OreLabels.Count - 1
            If SelectedOres.Contains(OreLabels(i).Text) Then
                ' Set the checks for selected ores first
                OreCheckBoxes(i).Checked = True
            End If
        Next

        tabPageOres.Enabled = True
        tabPageIce.Enabled = True

    End Sub

    Private Sub UpdateSettings(Optional SaveChanges As Boolean = False)
        Dim TempSettings As ConversionToOreSettings = Nothing
        Dim Settings As New ProgramSettings

        With TempSettings
            If rbtnConversionNone.Checked Then
                .ConversionType = rbtnConversionNone.Text
            ElseIf rbtnConversionOre.Checked Then
                .ConversionType = rbtnConversionOre.Text
            ElseIf rbtnConversionIce.Checked Then
                .ConversionType = rbtnConversionIce.Text
            ElseIf rbtnConversionOreIce.Checked Then
                .ConversionType = rbtnConversionOreIce.Text
            End If

            If rbtnRefinePrice.Checked Then
                .MinimizeOn = rbtnRefinePrice.Text
            ElseIf rbtnOreVolume.Checked Then
                .MinimizeOn = rbtnOreVolume.Text
            ElseIf rbtnOrePrice.Checked Then
                .MinimizeOn = rbtnOrePrice.Text
            End If

            .CompressedIce = chkCompressedIce.Checked
            .CompressedOre = chkCompressedOre.Checked

            .HighSec = chkHighSec.Checked
            .LowSec = chkLowSec.Checked
            .NullSec = chkNullSec.Checked
            .IndyLevel = cmbNullAnomLevel.Text

            .Amarr = chkAmarr.Checked
            .Caldari = chkCaldari.Checked
            .Gallente = chkGallente.Checked
            .Minmatar = chkMinmatar.Checked
            .Wormhole = chkWH.Checked
            .Triglavian = chkTriglavian.Checked

            .C1 = chkC1.Checked
            .C2 = chkC2.Checked
            .C3 = chkC3.Checked
            .C4 = chkC4.Checked
            .C5 = chkC5.Checked
            .C6 = chkC6.Checked

            .OreVariant0 = chkUseBaseOre.Checked
            .OreVariant5 = chkUse5percent.Checked
            .OreVariant10 = chkUse10percent.Checked

            ' Override Checks is already set each time one is checked
            .OverrideChecks = UserConversiontoOreSettings.OverrideChecks

        End With

        ' Save the data to the local variable
        UserConversiontoOreSettings = TempSettings

        If SaveChanges Then
            ' Save the data in the XML file
            Call Settings.SaveConversionToOreSettings(TempSettings)
            MsgBox("Settings Saved", vbInformation, Application.ProductName)
        End If


    End Sub

#Region "Click events"
    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Call UpdateSettings(True)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    'Private Sub chkNullSec_CheckedChanged(sender As Object, e As EventArgs) Handles chkNullSec.CheckedChanged
    '    cmbNullAnomLevel.Enabled = chkNullSec.Checked
    'End Sub

    Private Sub chkWH_CheckedChanged(sender As Object, e As EventArgs) Handles chkWH.CheckedChanged
        gbWHClasses.Enabled = chkWH.Checked
    End Sub

    Private Sub Option_Changed(sender As Object, e As EventArgs) Handles chkCompressedIce.CheckedChanged, chkCompressedOre.CheckedChanged, rbtnRefinePrice.CheckedChanged, rbtnOreVolume.CheckedChanged,
            rbtnOrePrice.CheckedChanged, chkHighSec.CheckedChanged, chkLowSec.CheckedChanged, cmbNullAnomLevel.SelectedIndexChanged, chkUseBaseOre.CheckedChanged,
            chkUse5percent.CheckedChanged, chkUse10percent.CheckedChanged, chkAmarr.CheckedChanged, chkCaldari.CheckedChanged, chkGallente.CheckedChanged, chkMinmatar.CheckedChanged,
            chkWH.CheckedChanged, chkTriglavian.CheckedChanged, chkC1.CheckedChanged, chkC2.CheckedChanged, chkC3.CheckedChanged, chkC4.CheckedChanged, chkC5.CheckedChanged, chkC6.CheckedChanged

        ' Always update the current settings locally, then refresh the ore/ice checks
        If Not FirstFormLoad Then
            Call UpdateSettings()
            Call RefreshOreList()
        End If
    End Sub



    Private Sub OreIce_CheckedChanged(sender As Object, e As EventArgs) Handles chkOre1.CheckedChanged, chkOre2.CheckedChanged, chkOre3.CheckedChanged, chkOre4.CheckedChanged,
                                                                                chkOre5.CheckedChanged, chkOre6.CheckedChanged, chkOre7.CheckedChanged, chkOre8.CheckedChanged,
                                                                                chkOre9.CheckedChanged, chkOre10.CheckedChanged, chkOre11.CheckedChanged, chkOre12.CheckedChanged,
                                                                                chkOre13.CheckedChanged, chkOre14.CheckedChanged, chkOre15.CheckedChanged, chkOre16.CheckedChanged,
                                                                                chkOre17.CheckedChanged, chkOre18.CheckedChanged, chkOre19.CheckedChanged, chkOre20.CheckedChanged,
                                                                                chkOre21.CheckedChanged, chkOre22.CheckedChanged, chkOre23.CheckedChanged, chkOre24.CheckedChanged,
                                                                                chkOre25.CheckedChanged, chkOre26.CheckedChanged, chkOre27.CheckedChanged, chkOre28.CheckedChanged,
                                                                                chkOre29.CheckedChanged

        ' Get the check number then update the override list
        Dim SelecteCheckbox As CheckBox = CType(sender, CheckBox)

        ' They manually updated the check so change it here
        UserConversiontoOreSettings.OverrideChecks(CInt(SelecteCheckbox.Name.ToString.Substring(6)) - 1) = CInt(SelecteCheckbox.Checked) * -1

    End Sub

    Private Sub rbtnConversionOreIce_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnConversionOreIce.CheckedChanged
        If rbtnConversionOreIce.Checked Then
            Call EnableObjects(True, True)
        End If
    End Sub

    Private Sub rbtnConversionIce_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnConversionIce.CheckedChanged
        If rbtnConversionIce.Checked Then
            Call EnableObjects(False, True)
        End If
    End Sub

    Private Sub rbtnConversionOre_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnConversionOre.CheckedChanged
        If rbtnConversionOre.Checked Then
            Call EnableObjects(True, False)
        End If
    End Sub

    Private Sub rbtnConversionNone_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnConversionNone.CheckedChanged
        If rbtnConversionNone.Checked Then
            Call EnableObjects(False, False)
        End If
    End Sub

    Private Sub EnableObjects(Ore As Boolean, Ice As Boolean)

        ' Disable first
        chkCompressedIce.Enabled = False
        chkCompressedOre.Enabled = False
        gbSystemSecurity.Enabled = False
        gbMineOreLoc.Enabled = False
        gbOreVariants.Enabled = False
        'cmbNullAnomLevel.Enabled = False

        gbWHClasses.Enabled = False

        chkWH.Enabled = False
        chkTriglavian.Enabled = False

        If Ore Then
            gbSystemSecurity.Enabled = True
            gbOreVariants.Enabled = True
            chkCompressedOre.Enabled = True
            'cmbNullAnomLevel.Enabled = True
            chkWH.Enabled = True
            chkTriglavian.Enabled = True

            If chkWH.Checked Then
                gbWHClasses.Enabled = True
            End If
            gbMineOreLoc.Enabled = True
        End If

        If Ice Then
            chkCompressedIce.Enabled = True
            gbSystemSecurity.Enabled = True
            gbMineOreLoc.Enabled = True
        End If

        ' Always update the current settings locally, then refresh the ore/ice checks
        If Not FirstFormLoad Then
            Call UpdateSettings()
            Call RefreshOreList()
        End If

        ' Finally, tabs
        tabPageIce.Enabled = Ice
        tabPageOres.Enabled = Ore

    End Sub

    Private Sub chkNullSec_CheckedChanged(sender As Object, e As EventArgs) Handles chkNullSec.CheckedChanged
        If chkNullSec.Checked Then
            chkWH.Enabled = True
            chkTriglavian.Enabled = True
        Else
            chkWH.Enabled = False
            chkTriglavian.Enabled = False
        End If

        If chkWH.Checked Then
            gbWHClasses.Enabled = True
        Else
            gbWHClasses.Enabled = False
        End If

    End Sub

#End Region

End Class