Public Class frmSelectIndustryJobColumns

    Dim MaxColumnNumber As Integer
    Dim SelectedIndex As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MaxColumnNumber = 1
        SelectedIndex = 0

    End Sub

    Private Sub SetMaxColumnNumber(InNumber As Integer)
        If InNumber > MaxColumnNumber Then
            MaxColumnNumber = InNumber
        End If
    End Sub

    ' Load all the current checks
    Private Sub frmSelectIndustryJobColumns_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Call ShowList()
    End Sub

    Private Sub ShowList()
        With UserIndustryJobsColumnSettings
            If .JobState <> 0 Then
                chkLstBoxColumns.SetItemChecked(0, True)
                SetMaxColumnNumber(.JobState)
            Else
                chkLstBoxColumns.SetItemChecked(0, False)
            End If

            If .InstallerName <> 0 Then
                chkLstBoxColumns.SetItemChecked(1, True)
                SetMaxColumnNumber(.InstallerName)
            Else
                chkLstBoxColumns.SetItemChecked(1, False)
            End If

            If .TimeToComplete <> 0 Then
                chkLstBoxColumns.SetItemChecked(2, True)
                SetMaxColumnNumber(.TimeToComplete)
            Else
                chkLstBoxColumns.SetItemChecked(2, False)
            End If

            If .Activity <> 0 Then
                chkLstBoxColumns.SetItemChecked(3, True)
                SetMaxColumnNumber(.Activity)
            Else
                chkLstBoxColumns.SetItemChecked(3, False)
            End If

            If .Status <> 0 Then
                chkLstBoxColumns.SetItemChecked(4, True)
                SetMaxColumnNumber(.Status)
            Else
                chkLstBoxColumns.SetItemChecked(4, False)
            End If

            If .StartTime <> 0 Then
                chkLstBoxColumns.SetItemChecked(5, True)
                SetMaxColumnNumber(.StartTime)
            Else
                chkLstBoxColumns.SetItemChecked(5, False)
            End If

            If .EndTime <> 0 Then
                chkLstBoxColumns.SetItemChecked(6, True)
                SetMaxColumnNumber(.EndTime)
            Else
                chkLstBoxColumns.SetItemChecked(6, False)
            End If

            If .CompletionTime <> 0 Then
                chkLstBoxColumns.SetItemChecked(7, True)
                SetMaxColumnNumber(.CompletionTime)
            Else
                chkLstBoxColumns.SetItemChecked(7, False)
            End If

            If .Blueprint <> 0 Then
                chkLstBoxColumns.SetItemChecked(8, True)
                SetMaxColumnNumber(.Blueprint)
            Else
                chkLstBoxColumns.SetItemChecked(8, False)
            End If

            If .OutputItem <> 0 Then
                chkLstBoxColumns.SetItemChecked(9, True)
                SetMaxColumnNumber(.OutputItem)
            Else
                chkLstBoxColumns.SetItemChecked(9, False)
            End If

            If .OutputItemType <> 0 Then
                chkLstBoxColumns.SetItemChecked(10, True)
                SetMaxColumnNumber(.OutputItemType)
            Else
                chkLstBoxColumns.SetItemChecked(10, False)
            End If

            If .InstallSystem <> 0 Then
                chkLstBoxColumns.SetItemChecked(11, True)
                SetMaxColumnNumber(.InstallSystem)
            Else
                chkLstBoxColumns.SetItemChecked(11, False)
            End If

            If .InstallRegion <> 0 Then
                chkLstBoxColumns.SetItemChecked(12, True)
                SetMaxColumnNumber(.InstallRegion)
            Else
                chkLstBoxColumns.SetItemChecked(12, False)
            End If

            If .LicensedRuns <> 0 Then
                chkLstBoxColumns.SetItemChecked(13, True)
                SetMaxColumnNumber(.LicensedRuns)
            Else
                chkLstBoxColumns.SetItemChecked(13, False)
            End If

            If .Runs <> 0 Then
                chkLstBoxColumns.SetItemChecked(14, True)
                SetMaxColumnNumber(.Runs)
            Else
                chkLstBoxColumns.SetItemChecked(14, False)
            End If

            If .SuccessfulRuns <> 0 Then
                chkLstBoxColumns.SetItemChecked(15, True)
                SetMaxColumnNumber(.SuccessfulRuns)
            Else
                chkLstBoxColumns.SetItemChecked(15, False)
            End If

            If .BlueprintLocation <> 0 Then
                chkLstBoxColumns.SetItemChecked(16, True)
                SetMaxColumnNumber(.BlueprintLocation)
            Else
                chkLstBoxColumns.SetItemChecked(16, False)
            End If

            If .OutputLocation <> 0 Then
                chkLstBoxColumns.SetItemChecked(17, True)
                SetMaxColumnNumber(.OutputLocation)
            Else
                chkLstBoxColumns.SetItemChecked(17, False)
            End If

            If .JobType <> 0 Then
                chkLstBoxColumns.SetItemChecked(18, True)
                SetMaxColumnNumber(.JobType)
            Else
                chkLstBoxColumns.SetItemChecked(18, False)
            End If

            If .LocalCompletionDateTime <> 0 Then
                chkLstBoxColumns.SetItemChecked(19, True)
                SetMaxColumnNumber(.LocalCompletionDateTime)
            Else
                chkLstBoxColumns.SetItemChecked(19, False)
            End If

            chkLstBoxColumns.Update()

        End With
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click
        If chkLstBoxColumns.CheckedItems.Count = 0 Then
            MsgBox("You must select at least one Column", vbExclamation, Application.ProductName)
            Exit Sub
        End If

        ' Save the local settings and the user settings
        Call SaveLocalColumnSettings()

        ' Save the data in the XML file
        Call AllSettings.SaveIndustryJobsColumnSettings(UserIndustryJobsColumnSettings)

        MsgBox("Columns Saved", vbInformation, Application.ProductName)

        Me.Hide()

    End Sub

    ' Save the items as viewed or not and order them from the last column
    Public Sub SaveLocalColumnSettings()
        Dim chkstate As CheckState
        Dim ColumnPositions(20) As String
        Dim TempColumns(20) As String
        Dim ColumnCount As Integer = 0
        Dim i As Integer = 1
        Dim j As Integer = 1

        With UserIndustryJobsColumnSettings

            chkstate = chkLstBoxColumns.GetItemCheckState(0)
            ' Change to max column order + 1 if checked and not already set
            If .JobState = 0 And chkstate = CheckState.Checked Then
                .JobState = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .JobState = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(1)
            ' Change to max column order + 1 if checked and not already set
            If .InstallerName = 0 And chkstate = CheckState.Checked Then
                .InstallerName = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .InstallerName = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(2)
            ' Change to max column order + 1 if checked and not already set
            If .TimeToComplete = 0 And chkstate = CheckState.Checked Then
                .TimeToComplete = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .TimeToComplete = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(3)
            ' Change to max column order + 1 if checked and not already set
            If .Activity = 0 And chkstate = CheckState.Checked Then
                .Activity = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .Activity = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(4)
            ' Change to max column order + 1 if checked and not already set
            If .Status = 0 And chkstate = CheckState.Checked Then
                .Status = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .Status = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(5)
            ' Change to max column order + 1 if checked and not already set
            If .StartTime = 0 And chkstate = CheckState.Checked Then
                .StartTime = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .StartTime = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(6)
            ' Change to max column order + 1 if checked and not already set
            If .EndTime = 0 And chkstate = CheckState.Checked Then
                .EndTime = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .EndTime = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(7)
            ' Change to max column order + 1 if checked and not already set
            If .CompletionTime = 0 And chkstate = CheckState.Checked Then
                .CompletionTime = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .CompletionTime = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(8)
            ' Change to max column order + 1 if checked and not already set
            If .Blueprint = 0 And chkstate = CheckState.Checked Then
                .Blueprint = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .Blueprint = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(9)
            ' Change to max column order + 1 if checked and not already set
            If .OutputItem = 0 And chkstate = CheckState.Checked Then
                .OutputItem = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .OutputItem = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(10)
            ' Change to max column order + 1 if checked and not already set
            If .OutputItemType = 0 And chkstate = CheckState.Checked Then
                .OutputItemType = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .OutputItemType = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(11)
            ' Change to max column order + 1 if checked and not already set
            If .InstallSystem = 0 And chkstate = CheckState.Checked Then
                .InstallSystem = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .InstallSystem = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(12)
            ' Change to max column order + 1 if checked and not already set
            If .InstallRegion = 0 And chkstate = CheckState.Checked Then
                .InstallRegion = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .InstallRegion = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(13)
            ' Change to max column order + 1 if checked and not already set
            If .LicensedRuns = 0 And chkstate = CheckState.Checked Then
                .LicensedRuns = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .LicensedRuns = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(14)
            ' Change to max column order + 1 if checked and not already set
            If .Runs = 0 And chkstate = CheckState.Checked Then
                .Runs = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .Runs = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(15)
            ' Change to max column order + 1 if checked and not already set
            If .SuccessfulRuns = 0 And chkstate = CheckState.Checked Then
                .SuccessfulRuns = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .SuccessfulRuns = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(16)
            ' Change to max column order + 1 if checked and not already set
            If .BlueprintLocation = 0 And chkstate = CheckState.Checked Then
                .BlueprintLocation = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .BlueprintLocation = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(17)
            ' Change to max column order + 1 if checked and not already set
            If .OutputLocation = 0 And chkstate = CheckState.Checked Then
                .OutputLocation = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .OutputLocation = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(18)
            ' Change to max column order + 1 if checked and not already set
            If .JobType = 0 And chkstate = CheckState.Checked Then
                .JobType = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .JobType = 0
            End If

            chkstate = chkLstBoxColumns.GetItemCheckState(19)
            ' Change to max column order + 1 if checked and not already set
            If .LocalCompletionDateTime = 0 And chkstate = CheckState.Checked Then
                .LocalCompletionDateTime = MaxColumnNumber + 1
                MaxColumnNumber += 1
            ElseIf chkstate = CheckState.Unchecked Then
                .LocalCompletionDateTime = 0
            End If

            ' Now in case something was removed, we want to update the indicies
            For i = 0 To ColumnPositions.Count - 1
                ColumnPositions(i) = ""
            Next

            With UserIndustryJobsColumnSettings
                ColumnPositions(.JobState) = ProgramSettings.JobStateColumn
                ColumnPositions(.InstallerName) = ProgramSettings.InstallerNameColumn
                ColumnPositions(.TimeToComplete) = ProgramSettings.TimetoCompleteColumn
                ColumnPositions(.Activity) = ProgramSettings.ActivityColumn
                ColumnPositions(.Status) = ProgramSettings.StatusColumn
                ColumnPositions(.StartTime) = ProgramSettings.StartTimeColumn
                ColumnPositions(.EndTime) = ProgramSettings.EndTimeColumn
                ColumnPositions(.CompletionTime) = ProgramSettings.CompletionTimeColumn
                ColumnPositions(.Blueprint) = ProgramSettings.BlueprintColumn
                ColumnPositions(.OutputItem) = ProgramSettings.OutputItemColumn
                ColumnPositions(.OutputItemType) = ProgramSettings.OutputItemTypeColumn
                ColumnPositions(.InstallSystem) = ProgramSettings.InstallSolarSystemColumn
                ColumnPositions(.InstallRegion) = ProgramSettings.InstallRegionColumn
                ColumnPositions(.LicensedRuns) = ProgramSettings.LicensedRunsColumn
                ColumnPositions(.Runs) = ProgramSettings.RunsColumn
                ColumnPositions(.SuccessfulRuns) = ProgramSettings.SuccessfulRunsColumn
                ColumnPositions(.BlueprintLocation) = ProgramSettings.BlueprintLocationColumn
                ColumnPositions(.OutputLocation) = ProgramSettings.OutputLocationColumn
                ColumnPositions(.JobType) = ProgramSettings.JobTypeColumn
                ColumnPositions(.LocalCompletionDateTime) = ProgramSettings.LocalCompletionDateTimeColumn
            End With

            ' Reset the first one with nothing since the first column is empty
            ColumnPositions(0) = ""

            ' Now get the total number of columns in the list we want to see
            For i = 1 To ColumnPositions.Count - 1
                If ColumnPositions(i) <> "" Then
                    ColumnCount += 1
                End If
            Next

            ' Init temp
            For i = 0 To TempColumns.Count - 1
                TempColumns(i) = ""
            Next

            ' Now loop through the columns and update the positions
            For i = 1 To ColumnPositions.Count - 1
                If ColumnPositions(i) <> "" Then
                    TempColumns(j) = ColumnPositions(i)
                    j += 1
                Else
                    If i = UserIndustryJobsColumnSettings.OrderByColumn Then
                        ' They removed the column they sorted, so default to the first column since you must have 1
                        UserIndustryJobsColumnSettings.OrderByColumn = 1
                    End If
                End If
            Next

            ColumnPositions = TempColumns

            ' Finally save the columns based on the current order
            With UserIndustryJobsColumnSettings
                For i = 1 To ColumnPositions.Count - 1
                    Select Case ColumnPositions(i)
                        Case ProgramSettings.JobStateColumn
                            .JobState = i
                        Case ProgramSettings.InstallerNameColumn
                            .InstallerName = i
                        Case ProgramSettings.TimetoCompleteColumn
                            .TimeToComplete = i
                        Case ProgramSettings.ActivityColumn
                            .Activity = i
                        Case ProgramSettings.StartTimeColumn
                            .StartTime = i
                        Case ProgramSettings.EndTimeColumn
                            .EndTime = i
                        Case ProgramSettings.CompletionTimeColumn
                            .CompletionTime = i
                        Case ProgramSettings.BlueprintColumn
                            .Blueprint = i
                        Case ProgramSettings.OutputItemColumn
                            .OutputItem = i
                        Case ProgramSettings.OutputItemTypeColumn
                            .OutputItemType = i
                        Case ProgramSettings.InstallSolarSystemColumn
                            .InstallSystem = i
                        Case ProgramSettings.InstallRegionColumn
                            .InstallRegion = i
                        Case ProgramSettings.LicensedRunsColumn
                            .LicensedRuns = i
                        Case ProgramSettings.RunsColumn
                            .Runs = i
                        Case ProgramSettings.SuccessfulRunsColumn
                            .SuccessfulRuns = i
                        Case ProgramSettings.BlueprintLocationColumn
                            .BlueprintLocation = i
                        Case ProgramSettings.OutputLocationColumn
                            .OutputLocation = i
                        Case ProgramSettings.JobTypeColumn
                            .JobType = i
                        Case ProgramSettings.LocalCompletionDateTimeColumn
                            .LocalCompletionDateTime = i
                    End Select
                Next
            End With
        End With

    End Sub

    Private Sub chkLstBoxColumns_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles chkLstBoxColumns.SelectedIndexChanged

        If SelectedIndex <> chkLstBoxColumns.SelectedIndex Then
            SelectedIndex = chkLstBoxColumns.SelectedIndex

            If chkLstBoxColumns.GetItemChecked(chkLstBoxColumns.SelectedIndex) Then
                ' Uncheckit
                chkLstBoxColumns.SetItemChecked(chkLstBoxColumns.SelectedIndex, False)
            Else
                ' Checkit
                chkLstBoxColumns.SetItemChecked(chkLstBoxColumns.SelectedIndex, True)
            End If

        End If

    End Sub

    Private Sub btnDefaults_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaults.Click
        UserIndustryJobsColumnSettings = AllSettings.SetDefaultIndustryJobsColumnSettings()
        Call ShowList()
        chkLstBoxColumns.Update()
    End Sub

End Class