Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

''' <summary>
''' 텍스트의 정보를 포함하고, 그리는 작업을 구현합니다.
''' </summary>
Public Class SdxText
    Inherits SdxGraphicsObject
    Implements IGraphicsResource, IGraphicsText

#Region " - Fields - "

    ''' <summary>
    ''' 폰트를 저장합니다.
    ''' </summary>
    Private m_Font As D3.Font = Nothing

    ''' <summary>
    ''' 텍스트를 저장합니다.
    ''' </summary>
    Private g_Text As String = Nothing

    ''' <summary>
    ''' 텍스트 표시 위치를 저장합니다.
    ''' </summary>
    Private g_Location As Drawing.PointF = Drawing.PointF.Empty

    ''' <summary>
    ''' 텍스트 색을 저장합니다.
    ''' </summary>
    Private g_Color As Drawing.Color = Drawing.Color.Black

    ''' <summary>
    ''' 텍스트 투명도를 저장합니다.
    ''' </summary>
    Private g_Opacity As Single = 1.0F

    ''' <summary>
    ''' 텍스트를 그릴 때 사용할 텍스쳐를 저장합니다.
    ''' </summary>
    Private g_Sprite As Sprite = Nothing

    ''' <summary>
    ''' 텍스트가 그려질 위치를 저장합니다.
    ''' </summary>
    Private g_Rectangle As Drawing.Rectangle = Drawing.Rectangle.Empty

    ''' <summary>
    ''' 텍스트를 그리는 방법을 저장합니다.
    ''' </summary>
    Private g_DrawFormat As D3.DrawTextFormat = D3.DrawTextFormat.None

#End Region

#Region " - Constructor - "

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Drawing.SystemFonts.DefaultFont)

    End Sub



    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontFam">폰트의 정보를 저장하고 있는 FontFamily 개체를 입력합니다.</param>
    ''' <param name="Size">폰트의 크기를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontFam As Drawing.FontFamily, ByVal Size As Single)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Device, New Drawing.Font(FontFam, Size))

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontFam">폰트의 정보를 저장하고 있는 FontFamily 개체를 입력합니다.</param>
    ''' <param name="Size">폰트의 크기를 입력합니다.</param>
    ''' <param name="Style">폰트의 스타일 정보를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontFam As Drawing.FontFamily, ByVal Size As Single, ByVal Style As Drawing.FontStyle)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Device, New Drawing.Font(FontFam, Size, Style))

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontFam">폰트의 정보를 저장하고 있는 FontFamily 개체를 입력합니다.</param>
    ''' <param name="Size">폰트의 크기를 입력합니다.</param>
    ''' <param name="GUnit">폰트의 단위 정보를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontFam As Drawing.FontFamily, ByVal Size As Single, ByVal GUnit As Drawing.GraphicsUnit)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Device, New Drawing.Font(FontFam, Size, GUnit))

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontFam">폰트의 정보를 저장하고 있는 FontFamily 개체를 입력합니다.</param>
    ''' <param name="Size">폰트의 크기를 입력합니다.</param>
    ''' <param name="Style">폰트의 스타일 정보를 입력합니다.</param>
    ''' <param name="GUnit">폰트의 단위 정보를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontFam As Drawing.FontFamily, ByVal Size As Single, ByVal Style As Drawing.FontStyle, ByVal GUnit As Drawing.GraphicsUnit)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Device, New Drawing.Font(FontFam, Size, Style, GUnit))

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontFam">폰트의 정보를 저장하고 있는 FontFamily 개체를 입력합니다.</param>
    ''' <param name="Size">폰트의 크기를 입력합니다.</param>
    ''' <param name="Style">폰트의 스타일 정보를 입력합니다.</param>
    ''' <param name="GUnit">폰트의 단위 정보를 입력합니다.</param>
    ''' <param name="GdiCharSet">문자 집합을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontFam As Drawing.FontFamily, ByVal Size As Single, ByVal Style As Drawing.FontStyle, ByVal GUnit As Drawing.GraphicsUnit, ByVal GdiCharSet As Byte)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Device, New Drawing.Font(FontFam, Size, Style, GUnit, GdiCharSet))

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontFam">폰트의 정보를 저장하고 있는 FontFamily 개체를 입력합니다.</param>
    ''' <param name="Size">폰트의 크기를 입력합니다.</param>
    ''' <param name="Style">폰트의 스타일 정보를 입력합니다.</param>
    ''' <param name="GUnit">폰트의 단위 정보를 입력합니다.</param>
    ''' <param name="GdiCharSet">문자 집합을 입력합니다.</param>
    ''' <param name="GdiVertical">세로로 글씨를 출력하는 폰트인지 여부를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontFam As Drawing.FontFamily, ByVal Size As Single, ByVal Style As Drawing.FontStyle, ByVal GUnit As Drawing.GraphicsUnit, ByVal GdiCharSet As Byte, ByVal GdiVertical As Boolean)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Device, New Drawing.Font(FontFam, Size, Style, GUnit, GdiCharSet, GdiVertical))

    End Sub



    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Font">텍스트를 그릴 때 사용할 폰트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Font As Drawing.Font)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Font)

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Font">텍스트를 그릴 때 사용할 폰트를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Font As Drawing.Font, ByVal Color As Drawing.Color)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Font)

        ' 폰트 색을 저장한다.
        g_Color = Color

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Font">텍스트를 그릴 때 사용할 폰트를 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Font As Drawing.Font, ByVal Text As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Font)

        ' 그릴 텍스트를 저장한다.
        g_Text = Text

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Font">텍스트를 그릴 때 사용할 폰트를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Font As Drawing.Font, ByVal Color As Drawing.Color, ByVal Text As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Font)

        ' 폰트 색을 저장한다.
        g_Color = Color

    End Sub



    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontDesc">텍스트를 그릴 때 사용할 폰트의 정보를 담고있는 FontDescription 구조체를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontDesc As D3.FontDescription)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, FontDesc)

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontDesc">텍스트를 그릴 때 사용할 폰트의 정보를 담고있는 FontDescription 구조체를 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontDesc As D3.FontDescription, ByVal Text As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, FontDesc)

        ' 그릴 텍스트를 저장한다.
        g_Text = Text

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontDesc">텍스트를 그릴 때 사용할 폰트의 정보를 담고있는 FontDescription 구조체를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontDesc As D3.FontDescription, ByVal Color As Drawing.Color)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, FontDesc)

        ' 폰트 색을 저장한다.
        g_Color = Color

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="FontDesc">텍스트를 그릴 때 사용할 폰트의 정보를 담고있는 FontDescription 구조체를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal FontDesc As D3.FontDescription, ByVal Color As Drawing.Color, ByVal Text As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, FontDesc)

        ' 폰트 색을 저장한다.
        g_Color = Color

        ' 그릴 텍스트를 저장한다.
        g_Text = Text

    End Sub



    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Width">폰트의 넓이를 입력합니다.</param>
    ''' <param name="Height">폰트의 높이를 입력합니다.</param>
    ''' <param name="Weight">폰트의 두께를 입력합니다.</param>
    ''' <param name="MipLevels">Mip 레벨을 입력합니다.</param>
    ''' <param name="Italic">기울임을 사용할 경우, 이 값을 True 로 입력합니다.</param>
    ''' <param name="CharSet">문자셋을 입력합니다.</param>
    ''' <param name="Precision">출력 방법을 입력합니다.</param>
    ''' <param name="Quality">품질을 입력합니다.</param>
    ''' <param name="Pitch">폰트 유형을 입력합니다.</param>
    ''' <param name="Name">폰트 이름을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Width As Int32, ByVal Height As Int32, ByVal Weight As FontWeight, ByVal MipLevels As Int32, ByVal Italic As Boolean, ByVal CharSet As CharacterSet, ByVal Precision As Precision, ByVal Quality As FontQuality, ByVal Pitch As PitchAndFamily, ByVal Name As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Height, Width, Weight, MipLevels, Italic, CharSet, Precision, Quality, Pitch, Name)

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Width">폰트의 넓이를 입력합니다.</param>
    ''' <param name="Height">폰트의 높이를 입력합니다.</param>
    ''' <param name="Weight">폰트의 두께를 입력합니다.</param>
    ''' <param name="MipLevels">Mip 레벨을 입력합니다.</param>
    ''' <param name="Italic">기울임을 사용할 경우, 이 값을 True 로 입력합니다.</param>
    ''' <param name="CharSet">문자셋을 입력합니다.</param>
    ''' <param name="Precision">출력 방법을 입력합니다.</param>
    ''' <param name="Quality">품질을 입력합니다.</param>
    ''' <param name="Pitch">폰트 유형을 입력합니다.</param>
    ''' <param name="Name">폰트 이름을 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Width As Int32, ByVal Height As Int32, ByVal Weight As FontWeight, ByVal MipLevels As Int32, ByVal Italic As Boolean, ByVal CharSet As CharacterSet, ByVal Precision As Precision, ByVal Quality As FontQuality, ByVal Pitch As PitchAndFamily, ByVal Name As String, ByVal Text As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Height, Width, Weight, MipLevels, Italic, CharSet, Precision, Quality, Pitch, Name)

        ' 그릴 텍스트를 저장한다.
        g_Text = Text

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Width">폰트의 넓이를 입력합니다.</param>
    ''' <param name="Height">폰트의 높이를 입력합니다.</param>
    ''' <param name="Weight">폰트의 두께를 입력합니다.</param>
    ''' <param name="MipLevels">Mip 레벨을 입력합니다.</param>
    ''' <param name="Italic">기울임을 사용할 경우, 이 값을 True 로 입력합니다.</param>
    ''' <param name="CharSet">문자셋을 입력합니다.</param>
    ''' <param name="Precision">출력 방법을 입력합니다.</param>
    ''' <param name="Quality">품질을 입력합니다.</param>
    ''' <param name="Pitch">폰트 유형을 입력합니다.</param>
    ''' <param name="Name">폰트 이름을 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Width As Int32, ByVal Height As Int32, ByVal Weight As FontWeight, ByVal MipLevels As Int32, ByVal Italic As Boolean, ByVal CharSet As CharacterSet, ByVal Precision As Precision, ByVal Quality As FontQuality, ByVal Pitch As PitchAndFamily, ByVal Name As String, ByVal Color As Drawing.Color)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Height, Width, Weight, MipLevels, Italic, CharSet, Precision, Quality, Pitch, Name)

        ' 폰트 색을 저장한다.
        g_Color = Color

    End Sub

    ''' <summary>
    ''' 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Width">폰트의 넓이를 입력합니다.</param>
    ''' <param name="Height">폰트의 높이를 입력합니다.</param>
    ''' <param name="Weight">폰트의 두께를 입력합니다.</param>
    ''' <param name="MipLevels">Mip 레벨을 입력합니다.</param>
    ''' <param name="Italic">기울임을 사용할 경우, 이 값을 True 로 입력합니다.</param>
    ''' <param name="CharSet">문자셋을 입력합니다.</param>
    ''' <param name="Precision">출력 방법을 입력합니다.</param>
    ''' <param name="Quality">품질을 입력합니다.</param>
    ''' <param name="Pitch">폰트 유형을 입력합니다.</param>
    ''' <param name="Name">폰트 이름을 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Width As Int32, ByVal Height As Int32, ByVal Weight As FontWeight, ByVal MipLevels As Int32, ByVal Italic As Boolean, ByVal CharSet As CharacterSet, ByVal Precision As Precision, ByVal Quality As FontQuality, ByVal Pitch As PitchAndFamily, ByVal Name As String, ByVal Color As Drawing.Color, ByVal Text As String)

        MyBase.New(Device)

        ' DirectX 텍스트 개체를 생성한다.
        m_Font = New D3.Font(Me.GraphicsDevice, Height, Width, Weight, MipLevels, Italic, CharSet, Precision, Quality, Pitch, Name)

        ' 폰트 색을 저장한다.
        g_Color = Color

        ' 그릴 텍스트를 저장한다.
        g_Text = Text

    End Sub

#End Region

#Region " - Properties - "

    ''' <summary>
    ''' 텍스트의 X 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Single Implements IGraphicsText.X
        Get
            Return g_Location.X
        End Get
        Set(ByVal value As Single)
            g_Location.X = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 Y 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single Implements IGraphicsText.Y
        Get
            Return g_Location.Y
        End Get
        Set(ByVal value As Single)
            g_Location.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single Implements IGraphicsText.Opacity
        Get
            Return g_Opacity
        End Get
        Set(ByVal value As Single)
            If value > 1.0 Then value = 1
            If value < 0 Then value = 0
            g_Opacity = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As Drawing.PointF Implements IGraphicsText.Location
        Get
            Return g_Location
        End Get
        Set(ByVal value As Drawing.PointF)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Text As String Implements IGraphicsText.Text
        Get
            Return g_Text
        End Get
        Set(ByVal value As String)
            g_Text = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property ForeColor As Drawing.Color Implements IGraphicsText.ForeColor
        Get
            Return g_Color
        End Get
        Set(ByVal value As Drawing.Color)
            g_Color = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트를 그릴 때 사용할 스프라이트를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Sprite As D3.Sprite
        Get
            Return g_Sprite
        End Get
        Set(ByVal value As D3.Sprite)
            g_Sprite = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트가 그려질 사각형을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Rectangle As Drawing.Rectangle
        Get
            Return g_Rectangle
        End Get
        Set(ByVal value As Drawing.Rectangle)
            g_Rectangle = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트가 그려지는 방법을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Format As D3.DrawTextFormat
        Get
            Return g_DrawFormat
        End Get
        Set(ByVal value As D3.DrawTextFormat)
            g_DrawFormat = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트 개체가 비트맵 폰트를 지원하는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property SupportsBitmapFont As Boolean Implements IGraphicsText.SupportsBitmapFont
        Get
            Return False
        End Get
    End Property

#End Region

#Region " - Methods - "

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    Public Sub Draw() Implements IGraphicsText.Draw

        If String.IsNullOrEmpty(Me.Text) Then Return

        If Me.Format And D3.DrawTextFormat.None Then
            m_Font.DrawText(Me.Sprite, Me.Text, New Drawing.Point(Me.Location.X, Me.Location.Y), Me.ForeColor)
        Else
            m_Font.DrawText(Me.Sprite, Me.Text, Me.Rectangle, Me.Format, Me.ForeColor)
        End If

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Sub Draw(ByVal Text As String, ByVal ParamArray Args() As Object)

        If String.IsNullOrEmpty(Text) Then Return
        Dim FormatText As String = String.Format(Text, Args)
        If String.IsNullOrEmpty(FormatText) Then Return

        If Me.Format And D3.DrawTextFormat.None Then
            m_Font.DrawText(Me.Sprite, FormatText, New Drawing.Point(Me.Location.X, Me.Location.Y), Me.ForeColor)
        Else
            m_Font.DrawText(Me.Sprite, FormatText, Me.Rectangle, Me.Format, Me.ForeColor)
        End If

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Sub Draw(ByVal Location As Drawing.Point, ByVal Text As String, ByVal ParamArray Args() As Object)

        If String.IsNullOrEmpty(Text) Then Return
        Dim FormatText As String = String.Format(Text, Args)
        If String.IsNullOrEmpty(FormatText) Then Return
        m_Font.DrawText(Me.Sprite, FormatText, Location, Me.ForeColor)

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Sub Draw(ByVal X As Int32, ByVal Y As Int32, ByVal Text As String, ByVal ParamArray Args() As Object)

        If String.IsNullOrEmpty(Text) Then Return
        Dim FormatText As String = String.Format(Text, Args)
        If String.IsNullOrEmpty(FormatText) Then Return
        m_Font.DrawText(Me.Sprite, FormatText, X, Y, Me.ForeColor)

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Sub Draw(ByVal Color As Drawing.Color, ByVal Location As Drawing.Point, ByVal Text As String, ByVal ParamArray Args() As Object)

        If String.IsNullOrEmpty(Text) Then Return
        Dim FormatText As String = String.Format(Text, Args)
        If String.IsNullOrEmpty(FormatText) Then Return
        m_Font.DrawText(Me.Sprite, FormatText, Location, Color)

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Sub Draw(ByVal Color As Drawing.Color, ByVal X As Int32, ByVal Y As Int32, ByVal Text As String, ByVal ParamArray Args() As Object)

        If String.IsNullOrEmpty(Text) Then Return
        Dim FormatText As String = String.Format(Text, Args)
        If String.IsNullOrEmpty(FormatText) Then Return
        m_Font.DrawText(Me.Sprite, Text, X, Y, Color)

    End Sub

#End Region

#Region " - IGraphicsResources - "

    Private g_Disposed As Boolean = False
    Public Sub Dispose() Implements IGraphicsResource.Dispose

        If g_Disposed Then Exit Sub
        If g_Sprite IsNot Nothing Then g_Sprite.Dispose()
        If m_Font IsNot Nothing Then m_Font.Dispose()
        g_Disposed = True

    End Sub
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsResource.Disposed
        Get
            Return g_Disposed
        End Get
    End Property

#End Region

#Region " - Obsoleted part of IGraphicsResource - "

    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty1 As System.Drawing.Color Implements IGraphicsResource.Color
        Get
            Return Nothing
        End Get
        Set(ByVal value As System.Drawing.Color)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty2 As Integer Implements IGraphicsResource.Height
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty3 As System.Drawing.PointF Implements IGraphicsResource.Location
        Get
            Return Nothing
        End Get
        Set(ByVal value As System.Drawing.PointF)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty4 As Single Implements IGraphicsResource.Opacity
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty5 As System.Drawing.RectangleF Implements IGraphicsResource.Rectangle
        Get

        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty6 As System.Drawing.Size Implements IGraphicsResource.Size
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty7 As Microsoft.DirectX.Direct3D.Texture Implements IGraphicsResource.Texture
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty8 As Integer Implements IGraphicsResource.Width
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty9 As Single Implements IGraphicsResource.X
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty10 As Single Implements IGraphicsResource.Y
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property

#End Region

    Public ReadOnly Property ResourceType As ResourceTypes Implements IGraphicsResource.ResourceType
        Get

        End Get
    End Property

End Class