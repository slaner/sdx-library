Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Namespace Controls

    Partial Class SDXControl

        ''' <summary>
        ''' 컨트롤이 포커스를 가지고 있는지 여부를 확인합니다.
        ''' </summary>
        Public ReadOnly Property Focused As Boolean
            Get
                Return g_HaveFocus
            End Get
        End Property

        ''' <summary>
        ''' 컨트롤의 배경 색을 가져오거나 설정합니다.
        ''' </summary>
        Public Property BackColor As Color
            Get
                Return g_BackColor
            End Get
            Set(ByVal value As Color)
                g_BackColor = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 글꼴 색을 가져오거나 설정합니다.
        ''' </summary>
        Public Property ForeColor As Color
            Get
                Return g_ForeColor
            End Get
            Set(ByVal value As Color)
                g_ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 글꼴을 가져오거나 설정합니다.
        ''' </summary>
        Public Property Font As Drawing.Font
            Get
                Return g_Font
            End Get
            Set(ByVal value As Drawing.Font)
                g_Font = value
                g_FontHeight = value.Height
                m_Font = New D3.Font(MyBase.Main.Device, g_Font)
                m_DotWidth = m_Font.MeasureString(Nothing, ".", 0, 0).Width
                RaiseEvent FontChanged()
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 표시 텍스트를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Text As String
            Get
                Return g_Text
            End Get
            Set(ByVal value As String)
                g_Text = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 투명도를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Opacity As Byte
            Get
                Return g_Opacity
            End Get
            Set(ByVal value As Byte)
                g_Opacity = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 위치를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Location As Point
            Get
                Return g_Location
            End Get
            Set(ByVal value As Point)
                g_Location = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 크기를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Size As Size
            Get
                Return g_Size
            End Get
            Set(ByVal value As Size)
                g_Size = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 사각형 영역을 가져오거나 설정합니다.
        ''' </summary>
        Public Property Bounds As Rectangle
            Get
                Return New Rectangle(g_Location, g_Size)
            End Get
            Set(ByVal value As Rectangle)
                g_Location = value.Location
                g_Size = value.Size
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 왼쪽 가장자리의 X좌표를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Left As Int32
            Get
                Return g_Location.X
            End Get
            Set(ByVal value As Int32)
                g_Location.X = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 왼쪽 가장자리의 Y좌표를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Top As Int32
            Get
                Return g_Location.Y
            End Get
            Set(ByVal value As Int32)
                g_Location.Y = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 오른쪽 가장자리의 X좌표를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Right As Int32
            Get
                Return g_Location.X + g_Size.Width
            End Get
        End Property

        ''' <summary>
        ''' 컨트롤의 오른쪽 가장자리의 Y좌표를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Bottom As Int32
            Get
                Return g_Location.Y + g_Size.Height
            End Get
        End Property

        ''' <summary>
        ''' 컨트롤의 넓이를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Width As Int32
            Get
                Return g_Size.Width
            End Get
            Set(ByVal value As Int32)
                g_Size.Width = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 높이를 가져오거나 설정합니다.
        ''' </summary>
        Public Property Height As Int32
            Get
                Return g_Size.Height
            End Get
            Set(ByVal value As Int32)
                g_Size.Height = value
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 텍스트 정렬 방식을 가져오거나 설정합니다.
        ''' </summary>
        Public Property TextAlign As TextAlignment
            Get
                Return g_TextAlignment
            End Get
            Set(ByVal value As TextAlignment)
                g_TextAlignment = value
            End Set
        End Property

        ''' <summary>
        ''' 글꼴의 높이를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property FontHeight As Single
            Get
                Return g_FontHeight
            End Get
        End Property

        ''' <summary>
        ''' 점 문자의 넓이를 가져옵니다.
        ''' </summary>
        Friend ReadOnly Property DotWidth As Int32
            Get
                Return m_DotWidth
            End Get
        End Property

        ''' <summary>
        ''' 컨트롤이 사용자 상호 작용에 응답할 수 있는지 여부를 나타내는 값을 가져오거나 설정합니다.
        ''' </summary>
        Public Property Enabled As Boolean
            Get
                Return g_Enabled
            End Get
            Set(ByVal value As Boolean)
                g_Enabled = value
            End Set
        End Property

    End Class

End Namespace