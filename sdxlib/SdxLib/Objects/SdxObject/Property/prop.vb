' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxObject.vb
'
' Dependencies:
'   -
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  0
'
' Date:
'   2013/12/06
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxObject class's body. (internal variables and interface implements)

Partial Class SdxObject

    ''' <summary>
    ''' 주 개체를 가져옵니다.
    ''' </summary>
    Friend ReadOnly Property Main As SDXMain
        Get
            Return g_Main
        End Get
    End Property

End Class