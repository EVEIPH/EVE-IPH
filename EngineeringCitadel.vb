
' Simple class to load Citadel data
Public Class EngineeringComplex

    Public Const RiataruEC As String = "Raitaru" ' Medium
    Public Const AzbelEC As String = "Azbel" ' Large
    Public Const SotiyoEC As String = "Sotiyo" ' XL

    Public Const HighSecBonus As Double = 1
    Public Const LowSecBonus As Double = 1.9
    Public Const NullWormholeBonus As Double = 2.1

    Private SelectedEC As String

    Private MEBonus As Double
    Private TEBonus As Double
    Private UsageBonus As Double

    Public Sub New(ByVal ECName As String)

        Select Case ECName
            Case RiataruEC, AzbelEC, SotiyoEC
                SelectedEC = ECName
            Case Else
                SelectedEC = RiataruEC ' Default to smallest
        End Select

        ' Save base bonuses
        Select Case SelectedEC
            Case RiataruEC
                MEBonus = 1
                TEBonus = 15
                UsageBonus = 3
            Case AzbelEC
                MEBonus = 1
                TEBonus = 20
                UsageBonus = 4
            Case SotiyoEC
                MEBonus = 1
                TEBonus = 30
                UsageBonus = 5
        End Select

    End Sub

    Public Function GetMEBonus() As Double
        Return MEBonus
    End Function

    Public Function GetTEBonus() As Double
        Return TEBonus
    End Function

End Class

Public Enum CitadelSize
    Small = 1
    Medium = 2
    Large = 3
    XL = 4
End Enum
