''' <summary>
''' 이미지를 표시하는 방법을 정의합니다.
''' </summary>
Public Enum ImageLayout

    ''' <summary>
    ''' 원래 크기대로 표시합니다.
    ''' </summary>
    [Default] = 0

    ''' <summary>
    ''' 출력되는 개체의 크기에 맞춰서 표시합니다.
    ''' </summary>
    Stretch = 1

    ''' <summary>
    ''' 출력되는 개체의 중앙에 맞춰서 표시합니다.
    ''' </summary>
    Center = 2

End Enum