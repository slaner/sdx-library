''' <summary>
''' 페이드 효과를 정의합니다.
''' </summary>
Public Enum FadeType

    ''' <summary>
    ''' 페이드 효과를 사용하지 않습니다.
    ''' </summary>
    None = 0

    ''' <summary>
    ''' 프레임 기반 페이드 효과를 사용합니다.
    ''' </summary>
    FrameBased = 1

    ''' <summary>
    ''' 삼각함수 기반 페이드 효과를 사용합니다.
    ''' </summary>
    TrigonometricFunctionBased = 2

End Enum