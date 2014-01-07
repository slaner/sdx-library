Namespace Controls
    Partial Class SDXTextBox

        ''' <summary>
        ''' 캐럿이 사라지는 시간을 가져오거나 설정합니다.
        ''' </summary>
        Public Property CaretTick As Int32
            Get
                Return g_CaretTick
            End Get
            Set(ByVal value As Int32)
                g_CaretTick = value
                m_CaretFadeStep = 90 / value
            End Set
        End Property

        Public Property SelectionStart As Int32
            Get
                Return g_SelectionStart
            End Get
            Set(ByVal value As Int32)
                g_SelectionStart = value
            End Set
        End Property

        Public ReadOnly Property SelectionLength As Int32
            Get
                Return g_SelectionLength
            End Get
        End Property



    End Class
End Namespace