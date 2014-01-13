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
                If value <> g_BackColor Then
                    Dim previousValue As Color = g_BackColor
                    g_BackColor = value
                    RaiseEvent BackColorChanged(previousValue, value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 배경 이미지를 가져오거나 설정합니다.
        ''' </summary>
        Public Property BackgroundImage As Image
            Get
                Return g_BackgroundImage
            End Get
            Set(ByVal value As Image)
                If Not value.Equals(g_BackgroundImage) Then
                    Dim previousValue As Image = value
                    g_BackgroundImage = value
                    m_BackgroundImage = D3.Texture.FromBitmap(MyBase.Main.Device, g_BackgroundImage, 0, 1)
                    RaiseEvent BackgroundImageChanged(previousValue, value)
                End If
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
                If value <> g_ForeColor Then
                    Dim previousValue As Color = g_ForeColor
                    g_ForeColor = value
                    RaiseEvent ForeColorChanged(previousValue, value)
                End If
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
                If g_Font Is Nothing Then GoTo updateImmediately
                If Not g_Font.Equals(value) Then
updateImmediately:
                    Dim previousValue As Font = g_Font
                    g_Font = value
                    g_FontHeight = value.Height
                    If Not IsNothing(m_Font) Then m_Font.Dispose()
                    m_Font = New D3.Font(MyBase.Main.Device, g_Font)
                    m_DotWidth = m_Font.MeasureString(Nothing, ".", 0, 0).Width
                    RaiseEvent FontChanged(previousValue, value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' 컨트롤의 글꼴을 설명하는 FontDescription 구조체를 가져오거나 설정합니다.
        ''' </summary>
        Public Property FontDescription As FontDescription
            Get
                Return g_FontDescription
            End Get
            Set(ByVal value As FontDescription)
                If value <> g_FontDescription Then
                    Dim previousValue As FontDescription = g_FontDescription
                    g_FontDescription = value
                    g_FontHeight = value.Height
                    If Not IsNothing(m_Font) Then m_Font.Dispose()
                    m_Font = New D3.Font(MyBase.Main.Device, g_FontDescription)
                    m_DotWidth = m_Font.MeasureString(Nothing, ".", 0, 0).Width
                    RaiseEvent FontDescriptionChanged(previousValue, value)
                End If
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
                If value <> g_Text Then
                    Dim previousValue As String = g_Text
                    g_Text = value
                    RaiseEvent TextChanged(previousValue, value)
                End If
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
                If value <> g_Opacity Then
                    Dim previousValue As Byte = g_Opacity
                    g_Opacity = value
                    RaiseEvent OpacityChanged(previousValue, value)
                End If
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
                If value <> g_Location Then
                    Dim previousValue As Point = g_Location
                    g_Location = value
                    RaiseEvent LocationChanged(previousValue, value)
                End If
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
                If value <> g_Size Then
                    Dim previousValue As Point = g_Size
                    g_Size = value
                    RaiseEvent SizeChanged(previousValue, value)
                End If
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
                If value <> Me.Bounds Then
                    g_Location = value.Location
                    g_Size = value.Size
                End If
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
                If value <> g_Location.X Then
                    Dim previousValue As Point = g_Location
                    g_Location.X = value
                    RaiseEvent LocationChanged(previousValue, g_Location)
                End If
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
                If value <> g_Location.Y Then
                    Dim previousValue As Point = g_Location
                    g_Location.Y = value
                    RaiseEvent LocationChanged(previousValue, g_Location)
                End If
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
                If value <> g_Size.Width Then
                    Dim previousValue As Point = g_Location
                    g_Size.Width = value
                    RaiseEvent SizeChanged(previousValue, g_Size)
                End If
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
                If value <> g_Size.Height Then
                    Dim previousValue As Point = g_Location
                    g_Size.Height = value
                    RaiseEvent SizeChanged(previousValue, g_Size)
                End If
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
                If value <> g_TextAlignment Then
                    Dim previousValue As TextAlignment = g_TextAlignment
                    g_TextAlignment = value
                    RaiseEvent TextAlignmentChanged(previousValue, value)
                End If
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
                If value <> g_Enabled Then
                    Dim previousValue As Boolean = g_Enabled
                    g_Enabled = value
                    RaiseEvent EnabledChanged(previousValue, value)
                End If
            End Set
        End Property

    End Class

End Namespace