' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxObject/ctor.vb
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
'   Defines SdxObject's constructor.

Partial Class SdxObject

    ''' <summary>
    ''' SdxObject 클래스의 새 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    Friend Sub New(ByVal Main As SDXMain)
        g_Main = Main
    End Sub

End Class