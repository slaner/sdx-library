Imports System.Drawing
Namespace Devices

    Partial Class SDXMouse

        ''' <summary>
        ''' 마우스 상태가 업데이트 되었는지의 여부를 나타내는 값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Polled As Boolean
            Get
                Return g_Polled
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 스크린 좌표값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ScreenLocation As Point
            Get
                Return g_ScreenLocation
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 X축 스크린 좌표값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ScreenX As Int32
            Get
                Return g_ScreenLocation.X
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 Y축 스크린 좌표값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ScreenY As Int32
            Get
                Return g_ScreenLocation.Y
            End Get
        End Property



        ''' <summary>
        ''' 마우스의 클라이언트 좌표값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ClientLocation As Point
            Get
                Return g_ClientLocation
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 X축 클라이언트 좌표값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ClientX As Int32
            Get
                Return g_ClientLocation.X
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 Y축 클라이언트 좌표값을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ClientY As Int32
            Get
                Return g_ClientLocation.Y
            End Get
        End Property

        ''' <summary>
        ''' 마우스 버튼의 상태를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ButtonFlags As Int32
            Get
                Return g_ButtonFlag
            End Get
        End Property

    End Class

End Namespace