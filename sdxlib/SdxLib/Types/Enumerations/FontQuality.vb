' SlaneR's DirectX Library (SdxLib)
'
' File:
'   FontQuality.vb
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
'   Defines Microsoft.DirectX.Direct3D.FontQuality as SdxMain.FontQuality to maximize compatibility.

''' <summary>
''' 폰트의 품질을 정의합니다.
''' </summary>
Public Enum FontQuality

    [Default]
    Draft
    Proof
    NonAntiAliased
    AntiAliased
    ClearType
    ClearTypeNatural

End Enum