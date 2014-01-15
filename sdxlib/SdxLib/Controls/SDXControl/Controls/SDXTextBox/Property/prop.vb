﻿Namespace Controls
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

        ''' <summary>
        ''' 컨트롤이 읽기 전용인지 나타내는 값을 가져오거나 설정합니다.
        ''' </summary>
        Public Property [ReadOnly] As Boolean
            Get
                Return g_ReadOnly
            End Get
            Set(ByVal value As Boolean)
                g_ReadOnly = value
            End Set
        End Property

        ''' <summary>
        ''' 텍스트의 내용을 암호 문자로 표시할 때 암호 문자로 사용할 문자를 가져오거나 설정합니다.
        ''' </summary>
        Public Property PasswordChar As Char
            Get
                Return g_PasswordChar
            End Get
            Set(ByVal value As Char)
                g_PasswordChar = value
            End Set
        End Property

    End Class
End Namespace