' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxMain/Event.vb
'
' Dependencies:
'   -
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  2  .  8  .  47
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
'   Defines SdxMain class's event.

Partial Class SDXMain

    ''' <summary>
    ''' 텍스트 그리기 전용 스프라이트가 시작된 후에 발생합니다.
    ''' </summary>
    Public Event OnTextSpriteBegin()

    ''' <summary>
    ''' 텍스트 그리기 전용 스프라이트가 종료된 후에 발생합니다.
    ''' </summary>
    Public Event OnTextSpriteEnd()



    ''' <summary>
    ''' 이미지(텍스쳐) 그리기 전용 스프라이트가 시작된 후에 발생합니다.
    ''' </summary>
    Public Event OnImageSpriteBegin(ByVal Target As Object)

    ''' <summary>
    ''' 이미지(텍스쳐) 그리기 전용 스프라이트가 종료된 후에 발생합니다.
    ''' </summary>
    Public Event OnImageSpriteEnd()



    ''' <summary>
    ''' 배경이 지정된 색으로 칠해진 후에 발생합니다.
    ''' </summary>
    Public Event OnBackgroundPainted()



    ''' <summary>
    ''' 백 버퍼의 내용을 화면으로 출력하기 전에 발생합니다.
    ''' </summary>
    Public Event BeforePresent()

End Class