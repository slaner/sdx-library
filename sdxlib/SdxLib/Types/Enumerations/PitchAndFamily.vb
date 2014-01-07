' SlaneR's DirectX Library (SdxLib)
'
' File:
'   PitchAndFamily.vb
'
' Dependencies:
'   -
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  0
'
' Date:
'   2013/12/09
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines Microsoft.DirectX.Direct3D.PitchAndFamily as SdxMain.PitchAndFamily to maximize compatibility.

''' <summary>
''' 폰트 패밀리를 정의합니다.
''' </summary>
Public Enum PitchAndFamily

    DefaultPitch = 0
    FamilyDecorative = 80
    FamilyDoNotCare = 0
    FamilyModern = &H30
    FamilyRoman = &H10
    FamilyScript = &H40
    FamilySwiss = &H20
    FixedPitch = 1
    MonoFont = 8
    VariablePitch = 2

End Enum