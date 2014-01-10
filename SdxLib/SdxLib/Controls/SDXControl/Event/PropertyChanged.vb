Namespace Controls
    Partial Class SDXControl

        ''' <summary>
        ''' 글꼴이 변경될 때 발생합니다.
        ''' </summary>
        Public Event FontChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 텍스트가 변경될 때 발생합니다.
        ''' </summary>
        Public Event TextChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 위치가 변경될 때 발생합니다.
        ''' </summary>
        Public Event LocationChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 크기가 변경될 때 발생합니다.
        ''' </summary>
        Public Event SizeChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 배경색이 변경될 때 발생합니다.
        ''' </summary>
        Public Event BackColorChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 글꼴색이 변경될 때 발생합니다.
        ''' </summary>
        Public Event ForeColorChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 투명도가 변경될 때 발생합니다.
        ''' </summary>
        Public Event OpacityChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 활성 여부가 변경될 때 발생합니다.
        ''' </summary>
        Public Event EnabledChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 텍스트 정렬 방식이 변경될 때 발생합니다.
        ''' </summary>
        Public Event TextAlignmentChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' 배경 이미지가 변경될 때 발생합니다.
        ''' </summary>
        Public Event BackgroundImageChanged As SDXControlPropertyChangedHandler

        ''' <summary>
        ''' FontDescription 구조체의 값이 변경될 때 발생합니다.
        ''' </summary>
        Public Event FontDescriptionChanged As SDXControlPropertyChangedHandler

    End Class
End Namespace