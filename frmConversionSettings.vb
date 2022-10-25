
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

            Select Case .MinimizeOn
                Case rbtnRefinePrice.Text
                    rbtnRefinePrice.Checked = True
                Case rbtnOrePrice.Text
                    rbtnOrePrice.Checked = True
                Case rbtnOreVolume.Text
                    rbtnOreVolume.Checked = True
            End Select

            Call SetTriCheck(chkConversionOre, .ConvertOre)
            Call SetTriCheck(chkConversionIce, .ConvertIce)
            Call SetTriCheck(chkConversionMoonOre, .ConvertMoonOre)
            Call SetTriCheck(chkConversionGas, .ConvertGas)

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

    Private Function CompareOptionSelected() As Boolean
        If chkConversionOre.Checked = False And chkConversionIce.Checked = False And chkConversionMoonOre.Checked = False And chkConversionGas.Checked = False Then
            Return False
        Else
            Return True
        End If
    End Function

    ' Refreshes the Ore list based on the options selected 
    Private Sub RefreshOreList()
        Dim SQL As String = ""
        Dim SQLOre As String = ""
        Dim SQLIce As String = ""
        Dim rsOres As SQLiteDataReader
        Dim OreList As New List(Of OreType)

        If Not FirstLoad Then

            ' Check to make sure they have the right stuff checked
            If Not CheckMiningEntryData() Then
                Exit Sub
            ElseIf CompareOptionSelected() Then
                Dim TempOreType As OreType

                ' First determine what type of stuff we are mining
                SQL = "SELECT CASE WHEN groupname = 'Ice' THEN CASE WHEN SUBSTR(ORE_NAME,1,10) ='Compressed' THEN SUBSTR(ORE_NAME,12) ELSE ORE_NAME END ELSE groupName END AS ORE, "
                SQL &= "CASE WHEN groupName = 'Ice' THEN 'Ice' ELSE 'Ore' END as ORE_GROUP FROM ORES, ORE_LOCATIONS, INVENTORY_GROUPS, INVENTORY_TYPES "
                SQL &= "WHERE ORES.ORE_ID = ORE_LOCATIONS.ORE_ID AND ORES.ORE_ID = INVENTORY_TYPES.typeID AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID AND "

                ' Ore Type
                If chkConversionOre.Checked Or chkConversionMoonOre.Checked Then
                    If chkConversionOre.Checked And chkConversionMoonOre.Checked Then
                        SQLOre = "((BELT_TYPE = 'Ore' OR BELT_TYPE LIKE '%MOON%') "
                    ElseIf chkConversionOre.Checked Then
                        SQLOre = "(BELT_TYPE LIKE 'Ore' "
                    Else
                        SQLOre = "(BELT_TYPE LIKE '%MOON%' "
                    End If
                    SQLOre &= "AND HIGH_YIELD_ORE IN ("
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

                If chkConversionIce.Checked Then
                    SQLIce = "(BELT_TYPE = 'Ice' AND HIGH_YIELD_ORE =-1) "
                End If

                ' Combine with OR if ore/ice checked
                If (chkConversionMoonOre.Checked Or chkConversionOre.Checked) And chkConversionIce.Checked Then
                    ' Need to combine both
                    SQL &= "(" & SQLOre & " OR " & SQLIce & ") "
                Else
                    If chkConversionIce.Checked Then
                        SQL &= SQLIce
                    ElseIf chkConversionOre.Checked Then
                        SQL &= SQLOre
                    End If
                End If

                If chkConversionOre.CheckState = CheckState.Indeterminate Or chkConversionMoonOre.CheckState = CheckState.Indeterminate Or chkConversionIce.CheckState = CheckState.Indeterminate Then
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
            If Not OresChecked And Not CompareOptionSelected() Then
                MsgBox("No ores selected", vbExclamation, Application.ProductName)
            End If
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

            If rbtnRefinePrice.Checked Then
                .MinimizeOn = rbtnRefinePrice.Text
            ElseIf rbtnOreVolume.Checked Then
                .MinimizeOn = rbtnOreVolume.Text
            ElseIf rbtnOrePrice.Checked Then
                .MinimizeOn = rbtnOrePrice.Text
            End If

            .ConvertOre = GetTriCheckValue(chkConversionOre)
            .ConvertIce = GetTriCheckValue(chkConversionIce)
            .ConvertMoonOre = GetTriCheckValue(chkConversionMoonOre)
            .ConvertGas = GetTriCheckValue(chkConversionGas)

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

        Call UpdateControls()

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
        ReDim UserConversiontoOreSettings.OverrideChecks(77)
        ReDim UserConversiontoOreSettings.IgnoreRefinedItems(34)

        For i = 0 To 77
            UserConversiontoOreSettings.OverrideChecks(i) = 1
        Next

        For i = 0 To 34
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

    Private Sub CheckOption_Changed(sender As Object, e As EventArgs) Handles chkHighSec.CheckedChanged, chkLowSec.CheckedChanged, chkUseBaseOre.CheckedChanged, chkUse5percent.CheckedChanged, chkUse10percent.CheckedChanged, chkAmarr.CheckedChanged, chkCaldari.CheckedChanged, chkGallente.CheckedChanged,
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
                                                                                lblOre29.Click, lblOre30.Click, lblOre31.Click, lblOre32.Click,
                                                                                lblOre33.Click, lblOre34.Click, lblOre35.Click, lblOre36.Click,
                                                                                lblOre37.Click, lblOre38.Click, lblOre39.Click, lblOre40.Click,
                                                                                lblOre41.Click, lblOre42.Click, lblOre43.Click, lblOre44.Click,
                                                                                lblOre45.Click, lblOre46.Click, lblOre47.Click, lblOre48.Click,
                                                                                lblOre49.Click, lblOre50.Click, lblOre51.Click, lblOre52.Click,
                                                                                lblOre53.Click, lblOre54.Click, lblOre55.Click, lblOre56.Click,
                                                                                lblOre57.Click, lblOre58.Click, lblOre59.Click, lblOre60.Click,
                                                                                lblOre61.Click, lblOre62.Click, lblOre63.Click, lblOre64.Click,
                                                                                lblOre65.Click, lblOre66.Click, lblOre67.Click, lblOre68.Click,
                                                                                lblOre69.Click, lblOre70.Click, lblOre71.Click, lblOre72.Click,
                                                                                lblOre73.Click, lblOre74.Click, lblOre75.Click, lblOre76.Click,
                                                                                lblOre77.Click, lblOre78.Click


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
                                                                                chkOre29.CheckedChanged, chkOre30.CheckedChanged, chkOre31.CheckedChanged, chkOre32.CheckedChanged,
                                                                                chkOre33.CheckedChanged, chkOre34.CheckedChanged, chkOre35.CheckedChanged, chkOre36.CheckedChanged,
                                                                                chkOre37.CheckedChanged, chkOre38.CheckedChanged, chkOre39.CheckedChanged, chkOre40.CheckedChanged,
                                                                                chkOre41.CheckedChanged, chkOre42.CheckedChanged, chkOre43.CheckedChanged, chkOre44.CheckedChanged,
                                                                                chkOre45.CheckedChanged, chkOre46.CheckedChanged, chkOre47.CheckedChanged, chkOre48.CheckedChanged,
                                                                                chkOre49.CheckedChanged, chkOre50.CheckedChanged, chkOre51.CheckedChanged, chkOre52.CheckedChanged,
                                                                                chkOre53.CheckedChanged, chkOre54.CheckedChanged, chkOre55.CheckedChanged, chkOre56.CheckedChanged,
                                                                                chkOre57.CheckedChanged, chkOre58.CheckedChanged, chkOre59.CheckedChanged, chkOre60.CheckedChanged,
                                                                                chkOre61.CheckedChanged, chkOre62.CheckedChanged, chkOre63.CheckedChanged, chkOre64.CheckedChanged,
                                                                                chkOre65.CheckedChanged, chkOre66.CheckedChanged, chkOre67.CheckedChanged, chkOre68.CheckedChanged,
                                                                                chkOre69.CheckedChanged, chkOre70.CheckedChanged, chkOre71.CheckedChanged, chkOre72.CheckedChanged,
                                                                                chkOre73.CheckedChanged, chkOre74.CheckedChanged, chkOre75.CheckedChanged, chkOre76.CheckedChanged,
                                                                                chkOre77.CheckedChanged, chkOre78.CheckedChanged
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
                                                                             lblIgnore13.Click, lblIgnore14.Click, lblIgnore15.Click, lblIgnore16.Click,
                                                                             lblIgnore17.Click, lblIgnore18.Click, lblIgnore19.Click, lblIgnore20.Click,
                                                                             lblIgnore21.Click, lblIgnore22.Click, lblIgnore23.Click, lblIgnore24.Click,
                                                                             lblIgnore25.Click, lblIgnore26.Click, lblIgnore27.Click, lblIgnore28.Click,
                                                                             lblIgnore29.Click, lblIgnore30.Click, lblIgnore31.Click, lblIgnore32.Click,
                                                                             lblIgnore33.Click, lblIgnore34.Click, lblIgnore35.Click
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
                                                                                      chkIgnore13.CheckedChanged, chkIgnore14.CheckedChanged, chkIgnore15.CheckedChanged,
                                                                                      chkIgnore17.Click, chkIgnore18.Click, chkIgnore19.Click, chkIgnore20.Click,
                                                                                      chkIgnore21.Click, chkIgnore22.Click, chkIgnore23.Click, chkIgnore24.Click,
                                                                                      chkIgnore25.Click, chkIgnore26.Click, chkIgnore27.Click, chkIgnore28.Click,
                                                                                      chkIgnore29.Click, chkIgnore30.Click, chkIgnore31.Click, chkIgnore32.Click,
                                                                                      chkIgnore33.Click, chkIgnore34.Click, chkIgnore35.Click

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

    Private Sub chkConversionGas_CheckedChanged(sender As Object, e As EventArgs) Handles chkConversionGas.CheckedChanged, chkConversionIce.CheckedChanged,
                                                                                          chkConversionMoonOre.CheckedChanged, chkConversionOre.CheckedChanged
        Dim CheckText As String = ""
        ' change the name of the check box if indeterminate is selected
        If CType(sender, CheckBox).Text.Contains("Moon Ore") Then
            CheckText = "Moon Ore"
        ElseIf CType(sender, CheckBox).Text.Contains("Gas") Then
            CheckText = "Gas"
        ElseIf CType(sender, CheckBox).Text.Contains("Ice") Then
            CheckText = "Ice"
        ElseIf CType(sender, CheckBox).Text.Contains("Ore") Then
            CheckText = "Ore"
        End If

        If CType(sender, CheckBox).CheckState = CheckState.Indeterminate Then
            CType(sender, CheckBox).Text = "Compressed " & CheckText
        Else
            CType(sender, CheckBox).Text = CheckText
        End If

        Call UpdateControls()

    End Sub

    Private Sub UpdateControls()

        Dim Ore As Boolean = chkConversionOre.Checked
        Dim Ice As Boolean = chkConversionIce.Checked
        Dim Moon As Boolean = chkConversionMoonOre.Checked
        Dim Gas As Boolean = chkConversionGas.Checked

        ' Disable if none checked
        If Not Ore And Not Ice And Not Moon And Not Gas Then
            gbSystemSecurity.Enabled = False
            gbMineOreLoc.Enabled = False
            gbOreVariants.Enabled = False
            gbWHClasses.Enabled = False
            chkWH.Enabled = False
            chkTriglavian.Enabled = False

            ' Finally, tabs
            tabPageIce.Enabled = False
            tabPageOres.Enabled = False
            tabPageGas.Enabled = False
            tabPageMoonOre.Enabled = False
            tabIgnoreMoonMats.Enabled = False

            gbIgnoreMinerals.Enabled = False
            gbIgnoreIceProducts.Enabled = False
        Else
            gbSystemSecurity.Enabled = True ' all get this

            ' Ice, no wh, system security and ore location
            If Ice Then
                tabPageIce.Enabled = True
                gbMineOreLoc.Enabled = True
                tabIgnoreList.Enabled = True
                gbIgnoreIceProducts.Enabled = True
            End If

            ' Ore, we want system security, ore variants, WH and Trig space, wh classes when checked
            If Ore Then
                tabPageOres.Enabled = True
                gbOreVariants.Enabled = True
                gbMineOreLoc.Enabled = True
                chkWH.Enabled = True
                chkTriglavian.Enabled = True
                If chkWH.Checked Then
                    gbWHClasses.Enabled = True
                Else
                    gbWHClasses.Enabled = False
                End If
                tabIgnoreList.Enabled = True
                gbIgnoreMinerals.Enabled = True
            End If

            ' Moon ore - system security, ore variants - ore location but not wh or trig and any empire is the same 'Moon'
            If Moon Then
                tabPageMoonOre.Enabled = True
                tabIgnoreList.Enabled = True
                tabIgnoreMoonMats.Enabled = True
                gbOreVariants.Enabled = True
                gbMineOreLoc.Enabled = True
            End If

            ' Gas - wh and system security(location) - no trig, no ore variants
            If Gas Then
                tabPageGas.Enabled = True
                chkWH.Enabled = True
                If chkWH.Checked Then
                    gbWHClasses.Enabled = True
                Else
                    gbWHClasses.Enabled = False
                End If
            End If
        End If

        ' Always update the current settings locally, then refresh the ore/ice checks
        Call UpdateSettingsRefresh()

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

    Private Enum ConversionType
        Ore = 0
        MoonOre = 1
        Ice = 2
        Gas = 3
    End Enum

#End Region

End Class