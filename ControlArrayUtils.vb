'
' ControlArrayUtils.VB - Provide VB6 like support of control arrays
' 
' Author -      P Krawec, Inovision Software Solutions, Inc.
' Date   -      September 1, 2005
'
' This code is free software; you can redistribute it and/or
' modify it any way you want.
' This library is distributed in the hope that it will be useful,
' but without any waranty. 
'
' Any additions or modifications please email to: 
'   kepler77@gmail.com
'
'
' How it works:
' VB.NET does not support control arrays as VB6 did with the index property.
' There is a way to get around this with minimal coding. 
' It requires a standard naming convention of the controls.
' Then the call to one function to search the screen based on that naming 
' convention to fill an array.
' 
' Example:
' To have a control array of 5 TextBoxes, you would have to name the textboxes 
' with the following convention:
'
' myTextBox0, myTextBox1, myTextBox2,myTextBox3, myTextBox4
' 
'Then in code, you would call: 
'dim myTextBoxArray as TextBox() = _
'    ControlArrayUtils.getControlArray(FORM, "myTextBox")
'
'The function will then search through all controls directly on the form that 
'match the name and have an integer value following it.
'Returning the new control array.
'
'ToDo: 
'     Add exception handling
'     Support recursive searching for controls within other controls.
'
Public Class ControlArrayUtils

    'Converts same type of controls on a form to a control array by 
    'using the notation ControlName_1, ControlName_2, where the _ 
    'can be replaced by any separator string
    Public Shared Function getControlArray(ByVal frm As Windows.Forms.Form, ByVal frmControls As Collection, ByVal controlName As String, Optional ByVal separator As String = "") As System.Array
        Dim i As Short
        Dim startOfIndex As Integer
        Dim alist As New ArrayList
        Dim controlType As System.Type = Nothing

        Dim ctl As System.Windows.Forms.Control
        Dim strSuffix As String
        Dim maxIndex As Short = -1 'Default

        'Loop through all controls, looking for controls with the 
        'matching name pattern
        'Find the highest indexed control
        For Each ctl In frmControls
            startOfIndex = ctl.Name.ToLower.IndexOf(controlName.ToLower & separator)
            If startOfIndex = 0 Then
                strSuffix = ctl.Name.Substring(controlName.Length)
                'Check that the suffix is an integer (index of the array)
                If IsInteger(strSuffix) Then
                    If CDbl(strSuffix) > maxIndex Then maxIndex = CShort(strSuffix) 'Find the highest indexed Element
                End If
            End If
        Next ctl

        'Add to the list of controls in correct order
        If maxIndex > -1 Then
            For i = 0 To maxIndex
                Dim aControl As Control = getControlFromName(frmControls, controlName, i, separator)
                If Not (aControl Is Nothing) Then
                    'Save the object Type (uses the last control found as the Type)
                    controlType = aControl.GetType
                End If
                alist.Add(aControl)
            Next
        End If

        Return alist.ToArray(controlType)

    End Function

    Private Shared Function getControlFromName(ByRef frm As Collection, ByVal controlName As String, ByVal index As Short, ByVal separator As String) As System.Windows.Forms.Control

        controlName = controlName & separator & index
        For Each ctl As Control In frm
            If String.Compare(ctl.Name, controlName, True) = 0 Then
                Return ctl
            End If
        Next ctl

        Return Nothing  'Could not find this control by name
    End Function

    Private Shared Function IsInteger(ByVal Value As String) As Boolean

        If Value = "" Then Return False

        For Each chr As Char In Value
            If Not Char.IsDigit(chr) Then
                Return False
            End If
        Next
        Return True
    End Function

End Class

Public Class ControlsCollection

    Private Shared m_controls As Collection
    Public Sub New(ByVal myForm As Form)
        m_controls = New Collection
        'create a control walker to get 
        'all controls on the form
        Dim aControlWalker As New ControlWalker(myForm)

    End Sub
    'This property returns the collection of all controls on the form
    ReadOnly Property Controls() As Collection
        Get
            Return m_controls
        End Get
    End Property

    Private Class ControlWalker
        ' This class recursively walks through all controls 
        ' in a container, and all containers contained in 
        ' this container, visiting all controls throughout 
        ' the hierarchy
        Private mContainer As Object

        Public Sub New(ByVal Container As Control)
            Dim cControl As Control = Nothing

            If Container.HasChildren = True Then
                For Each cControl In Container.Controls
                    'add this control to the controls collection
                    m_controls.Add(cControl)
                    If cControl.HasChildren Then
                        'This control has children, create another
                        'ControlWalk go visit each of them
                        Dim cWalker As New ControlWalker(cControl)
                    End If
                Next cControl
            End If
        End Sub

    End Class
End Class
