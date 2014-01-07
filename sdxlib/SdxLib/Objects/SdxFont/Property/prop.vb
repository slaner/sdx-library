' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxFont/prop.vb
'
' Dependencies:
'   -
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  6
'
' Date:
'   2013/12/10
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxFont class's property.

Partial Class SdxFont

    ''' <summary>
    ''' 텍스트 그리기 전용 스프라이트에 글씨를 출력할 것인지, 백 버퍼에 그대로 출력할 것인지의 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property UseTextSprite As Boolean
        Get
            Return g_UseTextSprite
        End Get
        Set(ByVal value As Boolean)
            g_UseTextSprite = value
        End Set
    End Property

End Class