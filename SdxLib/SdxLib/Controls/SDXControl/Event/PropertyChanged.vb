﻿Namespace Controls
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

    End Class
End Namespace