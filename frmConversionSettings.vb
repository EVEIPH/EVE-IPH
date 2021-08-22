
Imports System.Data.SQLite

Public Class frmConversiontoOreSettings
    Private FirstFormLoad As Boolean
    Private Reset As Boolean
    Private OresChecked As Boolean
    Private OretoFind As String
    Public SelectedLocation As ProgramLocation

    Private m_ControlsCollection As ControlsCollection
    Private OreCheckBoxes() As CheckBox
    Private OreLabels() As Label
    Private IgnoreChecks() As CheckBox
    Private IgnoreLabels() As Label

    Public Sub New()

        FirstFormLoad = True
        Reset = False

        ' This call is required by the designer.
        InitializeComponent()

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

        End With

        m_ControlsCollection = New ControlsCollection(Me)
        ' Get Region check boxes (note index starts at 1)
        OreCheckBoxes = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "chkOre"), CheckBox())
        OreLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblOre"), Label())
        IgnoreChecks = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "chkIgnore"), CheckBox())
        IgnoreLabels = DirectCast(ControlArrayUtils.getControlArray(Me, Me.MyControls, "lblIgnore"), Label())

        Call RefreshOreList()

        FirstFormLoad = False

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public ReadOnly Property MyControls() As Collection
        Get
            Return m_ControlsCollection.Controls
        End Get
    End Property

    ' Refreshes the Ore list based on the options selected 
    Private Sub RefreshOreList()
        Dim SQL As String = ""
        Dim SQLOre As String = ""
        Dim SQLIce As String = ""
        Dim rsOres As SQLiteDataReader
        Dim OreList As New List(Of OreType)

        ' Check to make sure they have the right stuff checked
        If Not CheckMiningEntryData() Then
            Exit Sub
        End If

        If rbtnConversionNone.Checked = False Then
            Dim TempOreType As OreType

            ' First determine what type of stuff we are mining
            SQL = "SELECT CASE WHEN groupname = 'Ice' THEN CASE WHEN SUBSTR(ORE_NAME,1,10) ='Compressed' THEN SUBSTR(ORE_NAME,12) ELSE ORE_NAME END ELSE groupName END AS ORE, "
            SQL &= "CASE WHEN groupName = 'Ice' THEN 'Ice' ELSE 'Ore' END as ORE_GROUP FROM ORES, ORE_LOCATIONS, INVENTORY_GROUPS, INVENTORY_TYPES "
            SQL &= "WHERE ORES.ORE_ID = ORE_LOCATIONS.ORE_ID AND ORES.ORE_ID = INVENTORY_TYPES.typeID AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID AND "

            ' Ore Type
            If rbtnConversionOre.Checked Or rbtnConversionOreIce.Checked Then
                SQLOre = "(BELT_TYPE = 'Ore' AND HIGH_YIELD_ORE IN ("
                ' Add the variant flag with this
                If chkUseBaseOre.Checked Then
                    SQLOre &= "0,"
                End If
                If chkUse5percent.Checked Then
                    SQLOre &= "1,"
                End If
                If chkUse10percent.Checked Then
                    SQLOre &= "2,"
                End If
                SQLOre = SQLOre.Substring(0, Len(SQLOre) - 1) & ")) "
            End If

            If rbtnConversionIce.Checked Or rbtnConversionOreIce.Checked Then
                SQLIce = "(BELT_TYPE = 'Ice' AND HIGH_YIELD_ORE =-1) "
            End If

            ' Combine with OR if ore/ice checked
            If rbtnConversionOreIce.Checked Then
                ' Need to combine both
                SQL &= "(" & SQLOre & " OR " & SQLIce & ") "
            Else
                If rbtnConversionIce.Checked Then
                    SQL &= SQLIce
                Else
                    SQL &= SQLOre
                End If
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

            SQL &= "GROUP BY ORE, ORE_GROUP"

            DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
            rsOres = DBCommand.ExecuteReader

            While rsOres.Read
                TempOreType.OreName = rsOres.GetString(0)
                TempOreType.OreGroup = rsOres.GetString(1)
                OreList.Add(TempOreType)
            End While
        End If

        ' Update all the ore checks and adjust the orelist depending on overrides
        Call UpdateOreChecks(OreList)

        ' If they have nothing checked, let them know
        If Not OresChecked And Not rbtnConversionNone.Checked Then
            MsgBox("No ores selected", vbExclamation, Application.ProductName)
        End If

    End Sub

    ' Checks all the data entered
    Private Function CheckMiningEntryData() As Boolean

        ' Check the location
        If chkHighSec.Checked = False And chkLowSec.Checked = False And chkNullSec.Checked = False Then
            ' Can't query any ore
            MsgBox("You must select an System Security", vbExclamation, Application.ProductName)
            Return False
        End If

        ' Check the Space types
        If chkAmarr.Checked = False And chkCaldari.Checked = False And chkGallente.Checked = False And chkMinmatar.Checked = False And chkWH.Checked = False And chkTriglavian.Checked = False Then
            ' Can't query any ore
            MsgBox("You must select an Ore Location", vbExclamation, Application.ProductName)
            Return False
        End If

        If chkWH.Checked = True And chkWH.Enabled = True And (chkC1.Checked = False And chkC2.Checked = False And chkC3.Checked = False And chkC4.Checked = False And chkC5.Checked = False And chkC6.Checked = False) Then
            ' Can't query any ore
            MsgBox("You must select a Wormhole Class", vbExclamation, Application.ProductName)
            Return False
        End If

        ' If ore or ore/ice is checked, then need to have a variant selected
        If gbOreVariants.Enabled = True And chkUseBaseOre.Checked = False And chkUse5percent.Checked = False And chkUse10percent.Checked = False Then
            MsgBox("You must select a Ore Variant", vbExclamation, Application.ProductName)
            Return False
        End If

        Return True

    End Function

    Private Sub UpdateOreChecks(ByRef SentOres As List(Of OreType))
        Dim FoundItem As OreType

        FirstFormLoad = True ' pause check updates
        OresChecked = False
        UserConversiontoOreSettings.SelectedOres = New List(Of OreType) ' Reset and add each ore to the list when selected
        UserConversiontoOreSettings.IgnoreItems = New List(Of String) ' Reset this too

        ' Clear all checks
        For i = 1 To OreCheckBoxes.Count - 1
            OreCheckBoxes(i).Checked = False
        Next

        With UserConversiontoOreSettings
            If SentOres.Count <> 0 Then
                For i = 1 To OreLabels.Count - 1
                    OretoFind = OreLabels(i).Text
                    FoundItem.OreName = ""
                    FoundItem = SentOres.Find(AddressOf FindOre)
                    If FoundItem.OreName <> "" Then
                        ' Set the checks for selected ores first
                        OreCheckBoxes(i).Checked = True
                        .SelectedOres.Add(FoundItem)
                        OresChecked = True
                    End If
                Next

                ' Now do overrides - if it's checked from the selected options, reset the override value
                For i = 0 To .OverrideChecks.Count - 1
                    If .OverrideChecks(i) <> AllSettings.DefaultOverrideValue Then
                        If CInt(OreCheckBoxes(i + 1).Checked) <> .OverrideChecks(i) Then
                            ' The override value is different than what is checked through query, so set it to the override value
                            OreCheckBoxes(i + 1).Checked = CBool(.OverrideChecks(i))
                        Else
                            ' Its the same as the checked value from query, so reset the override check to default
                            .OverrideChecks(i) = AllSettings.DefaultOverrideValue
                        End If
                        ' Save ore info
                        FoundItem.OreName = OreLabels(i + 1).Text
                        If i + 1 >= 20 Then
                            FoundItem.OreGroup = "Ice"
                        Else
                            FoundItem.OreGroup = "Ore"
                        End If
                        ' If checked add it to the list, else remove it
                        If OreCheckBoxes(i + 1).Checked Then
                            .SelectedOres.Add(FoundItem)
                            OresChecked = True
                        Else
                            .SelectedOres.Remove(FoundItem)
                        End If
                    End If
                Next

                ' Update ignore checks too
                For i = 0 To .IgnoreRefinedItems.Count - 1
                    IgnoreChecks(i + 1).Checked = CBool(.IgnoreRefinedItems(i))
                    If IgnoreChecks(i + 1).Checked Then
                        .IgnoreItems.Add(IgnoreLabels(i + 1).Text)
                    End If
                Next

            End If
        End With

        FirstFormLoad = False

    End Sub

    ' Predicate for finding the ore in the selected list
    Public Function FindOre(ByVal Ore As OreType) As Boolean
        If OretoFind = Ore.OreName Then
            Return True
        Else
            Return False
        End If
    End Function

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
            .SelectedOres = UserConversiontoOreSettings.SelectedOres
            .IgnoreRefinedItems = UserConversiontoOreSettings.IgnoreRefinedItems
            .IgnoreItems = UserConversiontoOreSettings.IgnoreItems
        End With

        ' Save the data to the local variable
        UserConversiontoOreSettings = TempSettings

        If SaveChanges Then
            ' Save the data in the XML file
            Call Settings.SaveConversionToOreSettings(TempSettings)
            MsgBox("Settings Saved", vbInformation, Application.ProductName)
        End If

    End Sub

    Private Sub UpdateSettingsRefresh()
        ' Always update the current settings locally, then refresh the ore/ice checks
        If Not FirstFormLoad And Not FirstLoad Then
            Call UpdateSettings()
            Call RefreshOreList()
            ' Load the bp on bp tab
            If Not IsNothing(SelectedBlueprint) And SelectedLocation = ProgramLocation.BlueprintTab Then
                With SelectedBlueprint
                    Call frmMain.UpdateBPGrids(.GetTypeID, .GetTechLevel, False, .GetItemGroupID, .GetItemCategoryID, SentFromLocation.BlueprintTab)
                End With
            End If
        End If
    End Sub

#Region "Click events"

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Call UpdateSettings(True)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ' Clear the ignore and overrides, then reload the form
        ReDim UserConversiontoOreSettings.OverrideChecks(28)
        ReDim UserConversiontoOreSettings.IgnoreRefinedItems(14)

        For i = 0 To 28
            UserConversiontoOreSettings.OverrideChecks(i) = 1
        Next

        For i = 0 To 14
            UserConversiontoOreSettings.IgnoreRefinedItems(i) = 0
        Next

        Reset = True
        For i = 1 To IgnoreChecks.Count - 1
            IgnoreChecks(i).Checked = False
        Next
        Reset = False
        Call RefreshOreList()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub chkWH_CheckedChanged(sender As Object, e As EventArgs) Handles chkWH.CheckedChanged
        gbWHClasses.Enabled = chkWH.Checked
        Call UpdateSettingsRefresh()
    End Sub

    Private Sub RadioOption_Changed(sender As Object, e As EventArgs) Handles rbtnRefinePrice.CheckedChanged, rbtnOreVolume.CheckedChanged, rbtnOrePrice.CheckedChanged

        ' Always update the current settings locally, then refresh the ore/ice checks
        If CType(sender, RadioButton).Checked Then
            Call UpdateSettingsRefresh()
        End If

    End Sub

    Private Sub CheckOption_Changed(sender As Object, e As EventArgs) Handles chkCompressedIce.CheckedChanged, chkCompressedOre.CheckedChanged, chkHighSec.CheckedChanged, chkLowSec.CheckedChanged, chkUseBaseOre.CheckedChanged, chkUse5percent.CheckedChanged, chkUse10percent.CheckedChanged, chkAmarr.CheckedChanged, chkCaldari.CheckedChanged, chkGallente.CheckedChanged,
            chkMinmatar.CheckedChanged, chkWH.CheckedChanged, chkTriglavian.CheckedChanged, chkC1.CheckedChanged, chkC2.CheckedChanged, chkC3.CheckedChanged, chkC4.CheckedChanged, chkC5.CheckedChanged, chkC6.CheckedChanged

        ' Always update the current settings locally, then refresh the ore/ice checks
        Call UpdateSettingsRefresh()

    End Sub

    Private Sub OreLabels_Click(sender As Object, e As EventArgs) Handles lblOre1.Click, lblOre2.Click, lblOre3.Click, lblOre4.Click,
                                                                                lblOre5.Click, lblOre6.Click, lblOre7.Click, lblOre8.Click,
                                                                                lblOre9.Click, lblOre10.Click, lblOre11.Click, lblOre12.Click,
                                                                                lblOre13.Click, lblOre14.Click, lblOre15.Click, lblOre16.Click,
                                                                                lblOre17.Click, lblOre18.Click, lblOre19.Click, lblOre20.Click,
                                                                                lblOre21.Click, lblOre22.Click, lblOre23.Click, lblOre24.Click,
                                                                                lblOre25.Click, lblOre26.Click, lblOre27.Click, lblOre28.Click,
                                                                                lblOre29.Click

        ' Find the index and toggle the check
        If Not FirstFormLoad Then
            Dim Index As Integer = CInt(CType(sender, Label).Name.ToString.Substring(6))
            'Check the index
            OreCheckBoxes(Index).Checked = Not OreCheckBoxes(Index).Checked
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
        If Not FirstFormLoad Then
            ' Get the check number then update the override list
            Dim SelectedCheckbox As CheckBox = CType(sender, CheckBox)

            ' They manually updated the check so change it here
            UserConversiontoOreSettings.OverrideChecks(CInt(SelectedCheckbox.Name.ToString.Substring(6)) - 1) = CInt(SelectedCheckbox.Checked)
            Call UpdateSettingsRefresh()
        End If
    End Sub

    Private Sub IgnoreLabels_Click(sender As Object, e As EventArgs) Handles lblIgnore1.Click, lblIgnore2.Click, lblIgnore3.Click, lblIgnore4.Click,
                                                                             lblIgnore5.Click, lblIgnore6.Click, lblIgnore7.Click, lblIgnore8.Click,
                                                                             lblIgnore9.Click, lblIgnore10.Click, lblIgnore11.Click, lblIgnore12.Click,
                                                                             lblIgnore13.Click, lblIgnore14.Click, lblIgnore15.Click

        ' Find the index and toggle the check
        If Not FirstFormLoad Then
            Dim Index As Integer = CInt(CType(sender, Label).Name.ToString.Substring(9))
            'Check the index
            IgnoreChecks(Index).Checked = Not IgnoreChecks(Index).Checked
        End If
    End Sub

    Private Sub IgnoreChecks_CheckedChanged(sender As Object, e As EventArgs) Handles chkIgnore1.CheckedChanged, chkIgnore2.CheckedChanged, chkIgnore3.CheckedChanged,
                                                                                      chkIgnore4.CheckedChanged, chkIgnore5.CheckedChanged, chkIgnore6.CheckedChanged,
                                                                                      chkIgnore7.CheckedChanged, chkIgnore8.CheckedChanged, chkIgnore9.CheckedChanged,
                                                                                      chkIgnore10.CheckedChanged, chkIgnore11.CheckedChanged, chkIgnore12.CheckedChanged,
                                                                                      chkIgnore13.CheckedChanged, chkIgnore14.CheckedChanged, chkIgnore15.CheckedChanged
        If Not FirstFormLoad Then
            ' Get the check number then update the override list
            Dim SelectedCheck As CheckBox = CType(sender, CheckBox)
            Dim Index As Integer = CInt(SelectedCheck.Name.ToString.Substring(9)) - 1

            If Not Reset Then
                ' They manually updated an ignore check
                UserConversiontoOreSettings.IgnoreRefinedItems(CInt(SelectedCheck.Name.ToString.Substring(9)) - 1) = CInt(SelectedCheck.Checked)
                Call UpdateSettingsRefresh()
            End If
        End If
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
        Call UpdateSettingsRefresh()

        gbIgnoreMinerals.Enabled = Ore
        gbIgnoreIceProducts.Enabled = Ice

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

        Call UpdateSettingsRefresh()

    End Sub

#End Region

End Class