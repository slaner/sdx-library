Namespace Controls
    Partial Class SDXControl

        ''' <summary>
        ''' 마우스 메세지를 처리할 것인지 여부를 가져오거나 설정합니다.
        ''' </summary>
        Friend Property DoNotProcessMouseMessages As Boolean
            Get
                Return g_DoNotProcessMouseMessages
            End Get
            Set(ByVal value As Boolean)
                g_DoNotProcessMouseMessages = value
            End Set
        End Property

        ''' <summary>
        ''' 키보드 메세지를 처리할 것인지 여부를 가져오거나 설정합니다.
        ''' </summary>
        Friend Property DoNotProcessKeyboardMessages As Boolean
            Get
                Return g_DoNotProcessKeyboardMessages
            End Get
            Set(ByVal value As Boolean)
                g_DoNotProcessKeyboardMessages = value
            End Set
        End Property

    End Class
End Namespace