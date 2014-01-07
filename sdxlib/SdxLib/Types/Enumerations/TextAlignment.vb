' SlaneR's DirectX Library (SdxLib)
'
' 파일명:
'   TextAlignment.vb
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
'   문자열을 정렬하는 방법을 나열한 열거형 상수를 정의합니다.

''' <summary>
''' 문자열을 정렬하는 방법을 정의합니다.
''' </summary>
Public Enum TextAlignment

    Bottom = 8
    Center = 1
    ExpandTabs = &H40
    Left = 0
    NoClip = &H100
    None = 0
    Right = 2
    RightToLeftReading = &H20000
    SingleLine = &H20
    Top = 0
    VerticalCenter = 4
    WordBreak = &H10

End Enum