' SlaneR's DirectX Library (SdxLib)
'
' File:
'   CharacterSet.vb
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
'   Defines Microsoft.DirectX.Direct3D.CharacterSet as SdxMain.CharacterSet to maximize compatibility.

''' <summary>
''' 문자셋을 정의합니다.
''' </summary>
Public Enum CharacterSet

    Ansi = &H0
    Arabic = &HB2
    Baltic = &HBA
    ChineseBig5 = &H88
    [Default] = &H1
    EastEurope = &HEE
    GB2312 = &H86
    Greek = &HA1
    Hangul = &H81
    Hebrew = &HB1
    Johab = &H82
    Mac = &H4D
    Oem = &HFF
    Russian = &HCC
    ShiftJIS = &H80
    Symbol = &H2
    Thai = &HDE
    Turkish = &HA2
    Vietnamese = &HA3

End Enum