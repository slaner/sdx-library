' SlaneR's DirectX Library (SdxLib)
'
' File:
'   OutputPrecision.vb
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
'   Defines Microsoft.DirectX.Direct3D.OutputPrecision as SdxMain.OutputPrecision to maximize compatibility.

''' <summary>
''' 원하는 대로 출력할 수 있도록 도와주는 값을 정의합니다.
''' </summary>
Public Enum OutputPrecision

    [Default]
    [String]
    Character
    Stroke
    Tt
    Device
    Raster
    TtOnly
    Outline
    ScreenOutline
    PsOnly

End Enum