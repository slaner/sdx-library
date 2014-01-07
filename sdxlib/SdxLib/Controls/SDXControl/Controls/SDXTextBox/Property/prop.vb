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

        ''' <summary>
        ''' 선택 영역의 시작 부분을 가져오거나 설정합니다.
        ''' </summary>
        Public Property SelectionStart As Int32
            Get
                Return g_Selection.Start
            End Get
            Set(ByVal value As Int32)
                g_Selection.Start = value
            End Set
        End Property

        ''' <summary>
        ''' 선택 영역의 길이를 가져오거나 설정합니다.
        ''' </summary>
        Public Property SelectionLength As Int32
            Get
                Return g_Selection.Length
            End Get
            Set(ByVal value As Int32)
                If value > m_Buffer.Length Then Return
                If g_Selection.Start + value > m_Buffer.Length Then Return
                g_Selection.Length = value
            End Set
        End Property



    End Class
End Namespace