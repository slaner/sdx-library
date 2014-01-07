' SlaneR's DirectX Library (SdxLib)
'
' 파일명:
'   MouseButton.vb
'
' 수정한 날짜:
'   2013/12/31
'
' 작성자:
'   SlaneR
'
' 문의:
'   dev.slaner@gmail.com
'
' 설명:
'   System.Windows.Forms.MouseButtons 열거형 상수를 정의합니다.

''' <summary>
''' 마우스 버튼을 정의합니다.
''' </summary>
Public Enum MouseButton

    None = &H0
    Left = &H1
    Right = &H2
    Middle = &H4
    XButton1 = &H8
    XButton2 = &H10

End Enum