Public Class frmAddStructureIDs

    Private UpdatingStructureIDText As Boolean
    Private Const InvalidName As String = "Invalid Name"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Set the structure instructions for importing prices from structures
        lblStructurePriceInstructions.Text = "Some structures may not be 'public' and IPH cannot download prices from them. However, if you have access to a market (e.g Nullsec Trade Hub), follow these steps to import prices for specific structures:"
        lblStructurePriceInstructions.Text &= vbCrLf & vbCrLf
        lblStructurePriceInstructions.Text &= "1. Make a link of the market hub in the EVE Chat window. You can drag the station name from assets or type the name and select auto-link and then search for 'Station' and hit enter to put it in chat. Note, if you do not have access search will not work."
        lblStructurePriceInstructions.Text &= vbCrLf
        lblStructurePriceInstructions.Text &= "2. Right click the link and select Copy after entering the chat link."
        lblStructurePriceInstructions.Text &= vbCrLf
        lblStructurePriceInstructions.Text &= "3. Paste the text into the structure ID textbox - it will format the number for you."
        lblStructurePriceInstructions.Text &= vbCrLf
        lblStructurePriceInstructions.Text &= "4. Click check for Market. If there is no market available or you do not have access, the Structure ID will not be added."

        UpdatingStructureIDText = False

        For i = 1 To 10
            Call UpdateControls(i, False)
        Next

    End Sub

    ' Will run an ESI query to see if the index they gave has market data
    Private Sub CheckIDforMarketData(ByVal Index As Integer)
        Dim ESIData As New ESI
        Dim StructureTextbox As TextBox = GetTextBox(Index)
        Dim StructureLabel As Label = GetLabel(Index)

        If GetTextBox(Index).Text <> InvalidName Then
            Application.UseWaitCursor = True
            Application.DoEvents()

            If StructureTextbox.Text <> "" Then
                If ESIData.CheckStructureMarketData(CLng(StructureTextbox.Text), SelectedCharacter.CharacterTokenData, True) Then
                    StructureLabel.ForeColor = Color.Green
                    StructureLabel.Text = "OK"
                Else
                    StructureLabel.ForeColor = Color.Red
                    StructureLabel.Text = "Market Access Denied"
                End If
            Else
                MsgBox("You must enter an ID", vbInformation, Application.ProductName)
                StructureTextbox.Focus()
            End If

            Application.UseWaitCursor = False
            Application.DoEvents()
        Else
            MsgBox("Structure number is invalid", vbInformation, Application.ProductName)
            StructureTextbox.SelectAll()
            StructureTextbox.Focus()
        End If
    End Sub

    ' Save all the structure IDs they entered, if they didn't check it, then run the check of the data and don't add if it comes back false
    Private Sub btnSaveStuctureIDs_Click(sender As Object, e As EventArgs) Handles btnSaveStuctureIDs.Click
        Dim ESIData As New ESI
        Dim CheckedIDs As New List(Of Integer)
        Dim StructureTextBox As TextBox
        Dim AddedCount As Integer = 0
        Dim StructureIDList As New List(Of Long)

        Application.UseWaitCursor = True
        Application.DoEvents()

        CheckedIDs = GetCheckedIDs()

        For Each index In CheckedIDs
            StructureTextBox = GetTextBox(index)

            ' Check the structure for ability to add data
            If ESIData.CheckStructureMarketData(CLng(StructureTextBox.Text), SelectedCharacter.CharacterTokenData, True) Then
                ' They can add it
                StructureIDList.Add(CLng(StructureTextBox.Text))
                AddedCount += 1
            End If
        Next

        ' Add the data
        Dim SP As New StructureProcessor
        For Each StructureID In StructureIDList
            Call SP.UpdateStructureData(StructureID, SelectedCharacter.CharacterTokenData, True, False, True)
        Next

        ' Refresh the view saved screen if open
        If frmViewStructures.Visible Then
            Call frmViewStructures.LoadStructureGrid()
        End If

        If AddedCount = 0 Then
            MsgBox("Could not add selected items. Please check information and try again.", vbInformation, Application.ProductName)
        Else
            If AddedCount = CheckedIDs.Count Then
                MsgBox("Selected Structures added.", vbInformation, Application.ProductName)
            Else
                MsgBox("Added " & CStr(AddedCount) & " out of " & CheckedIDs.Count & " selected. Please double check information and try again.", vbInformation, Application.ProductName)
            End If
        End If

        Application.UseWaitCursor = False
        Application.DoEvents()


    End Sub

    Private Function GetCheckedIDs() As List(Of Integer)
        Dim TempList As New List(Of Integer)

        If CheckBox1.Checked Then TempList.Add(1)
        If CheckBox2.Checked Then TempList.Add(2)
        If CheckBox3.Checked Then TempList.Add(3)
        If CheckBox4.Checked Then TempList.Add(4)
        If CheckBox5.Checked Then TempList.Add(5)
        If CheckBox6.Checked Then TempList.Add(6)
        If CheckBox7.Checked Then TempList.Add(7)
        If CheckBox8.Checked Then TempList.Add(8)
        If CheckBox9.Checked Then TempList.Add(9)
        If CheckBox10.Checked Then TempList.Add(10)

        Return TempList

    End Function

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged, TextBox8.TextChanged, TextBox7.TextChanged, TextBox6.TextChanged, TextBox5.TextChanged, TextBox4.TextChanged, TextBox3.TextChanged, TextBox2.TextChanged, TextBox10.TextChanged, TextBox1.TextChanged
        ' Format the text to just show the number, if it doesn't contain the correct text, show 'Invalid Name'
        Dim FormattedText As String = ""
        Dim textID As TextBox = CType(sender, TextBox)

        If Not UpdatingStructureIDText And textID.Text <> InvalidName Then
            Try
                If textID.Text.Contains("//") Then
                    ' Find the ID after it - [0054:36] Zifrian > <url=showinfo:35835//1027907881953>Tamo</url>
                    Dim IDStart As Integer = textID.Text.IndexOf("//") + 2
                    Dim IDEnd As Integer = textID.Text.IndexOf(">", IDStart)
                    FormattedText = textID.Text.Substring(IDStart, IDEnd - IDStart)
                ElseIf IsNumeric(textID.Text) Then
                    FormattedText = Trim(textID.Text)
                Else
                    ' Not formatted correctly
                    FormattedText = InvalidName
                End If
            Catch
                FormattedText = InvalidName
            End Try
            UpdatingStructureIDText = True
            textID.Text = FormattedText
            textID.SelectAll()
            UpdatingStructureIDText = False
        End If

    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox9.KeyDown, TextBox8.KeyDown, TextBox7.KeyDown, TextBox6.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox10.KeyDown, TextBox1.KeyDown
        If e.KeyCode = Keys.Delete Then
            UpdatingStructureIDText = True
        Else
            UpdatingStructureIDText = False
        End If
    End Sub

    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress, TextBox8.KeyPress, TextBox7.KeyPress, TextBox6.KeyPress, TextBox5.KeyPress, TextBox4.KeyPress, TextBox3.KeyPress, TextBox2.KeyPress, TextBox10.KeyPress, TextBox1.KeyPress
        If e.KeyChar = ControlChars.Back Then
            UpdatingStructureIDText = True
        Else
            UpdatingStructureIDText = False
        End If
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox9.CheckedChanged, CheckBox8.CheckedChanged, CheckBox7.CheckedChanged, CheckBox6.CheckedChanged, CheckBox5.CheckedChanged, CheckBox4.CheckedChanged, CheckBox3.CheckedChanged, CheckBox2.CheckedChanged, CheckBox10.CheckedChanged, CheckBox1.CheckedChanged
        Dim tempcheck As CheckBox = CType(sender, CheckBox)
        Call UpdateControls(GetControlIndex(tempcheck.Name), tempcheck.Checked)
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button10.Click, Button1.Click
        Dim tempButton As Button = CType(sender, Button)
        Call CheckIDforMarketData(GetControlIndex(tempButton.Name))
    End Sub

    Public Function GetControlIndex(Name As String) As Integer
        Dim i As Integer = CInt(Name.Substring(Len(Name) - 1))
        If i = 0 Then
            i = CInt(Name.Substring(Len(Name) - 2))
        End If
        Return i
    End Function

    Private Sub UpdateControls(index As Integer, value As Boolean)
        Select Case index
            Case 1
                TextBox1.Enabled = value
                Button1.Enabled = value
                Label1.Text = ""
            Case 2
                TextBox2.Enabled = value
                Button2.Enabled = value
                Label2.Text = ""
            Case 3
                TextBox3.Enabled = value
                Button3.Enabled = value
                Label3.Text = ""
            Case 4
                TextBox4.Enabled = value
                Button4.Enabled = value
                Label4.Text = ""
            Case 5
                TextBox5.Enabled = value
                Button5.Enabled = value
                Label5.Text = ""
            Case 6
                TextBox6.Enabled = value
                Button6.Enabled = value
                Label6.Text = ""
            Case 7
                TextBox7.Enabled = value
                Button7.Enabled = value
                Label7.Text = ""
            Case 8
                TextBox8.Enabled = value
                Button8.Enabled = value
                Label8.Text = ""
            Case 9
                TextBox9.Enabled = value
                Button9.Enabled = value
                Label9.Text = ""
            Case 10
                TextBox10.Enabled = value
                Button10.Enabled = value
                Label10.Text = ""
        End Select
    End Sub

    Private Function GetTextBox(Index As Integer) As TextBox
        Select Case Index
            Case 1
                Return TextBox1
            Case 2
                Return TextBox2
            Case 3
                Return TextBox3
            Case 4
                Return TextBox4
            Case 5
                Return TextBox5
            Case 6
                Return TextBox6
            Case 7
                Return TextBox7
            Case 8
                Return TextBox8
            Case 9
                Return TextBox9
            Case 10
                Return TextBox10
        End Select

        Return Nothing

    End Function

    Private Function GetLabel(Index As Integer) As Label
        Select Case Index
            Case 1
                Return Label1
            Case 2
                Return Label2
            Case 3
                Return Label3
            Case 4
                Return Label4
            Case 5
                Return Label5
            Case 6
                Return Label6
            Case 7
                Return Label7
            Case 8
                Return Label8
            Case 9
                Return Label9
            Case 10
                Return Label10
        End Select

        Return Nothing
    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Hide()
    End Sub

    Private Sub btnViewSavedStructures_Click(sender As Object, e As EventArgs) Handles btnViewSavedStructures.Click
        If frmViewStructures.Visible = False Then
            frmViewStructures = New frmViewSavedStructures
            frmViewStructures.Show()
        End If
    End Sub

End Class