Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX
Imports DI = Microsoft.DirectX.DirectInput

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Microsoft.DirectX.DirectInput

Imports System.Drawing

Public Class SdxTexture2D
    Inherits SdxGraphicsObject
    Implements IGraphicsResource

#Region " - Constructor - "

    '### FromFile
    ''' <summary>
    ''' 텍스쳐 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Path">텍스쳐로 사용할 이미지가 저장된 경로를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Path As String)

        MyBase.New(Device)

        ' 파일로부터 텍스쳐를 생성한다.
        g_Texture = D3.TextureLoader.FromFile(Me.GraphicsDevice, Path)

        ' 이미지의 크기 정보를 가져온다.
        g_Size = New Size(g_Texture.GetLevelDescription(0).Width, g_Texture.GetLevelDescription(0).Height)

    End Sub

    ''' <summary>
    ''' 텍스쳐 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Path">텍스쳐로 사용할 이미지가 저장된 경로를 입력합니다.</param>
    ''' <param name="ColorKey">투명 색으로 사용할 색을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Path As String, ByVal ColorKey As Color)

        MyBase.New(Device)

        ' 파일로부터 텍스쳐를 생성한다.
        g_Texture = D3.TextureLoader.FromFile(Me.GraphicsDevice, Path, 0, 0, 0, 0, 0, Pool.Managed, 0, 0, ColorKey.ToArgb())

        ' 이미지의 크기 정보를 가져온다.
        g_Size = New Size(g_Texture.GetLevelDescription(0).Width, g_Texture.GetLevelDescription(0).Height)

    End Sub

    '### FromStream (Image)
    ''' <summary>
    ''' 텍스쳐 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Img As Image)

        MyBase.New(Device)

        ' 이미지를 스트림에 저장한다.
        Dim imgStream As New IO.MemoryStream
        Img.Save(imgStream, Img.RawFormat)
        imgStream.Position = 0

        ' 스트림을 텍스쳐로 변환한다.
        g_Texture = D3.TextureLoader.FromStream(Me.GraphicsDevice, imgStream)

        ' 스트림을 삭제한다.
        imgStream.Dispose()

        ' 크기를 저장한다.
        g_Size = Img.Size

        ' 회전 중심 축을 설정한다.
        g_Center = New Point(g_Size.Width / 2, g_Size.Height / 2)

    End Sub

    ''' <summary>
    ''' 텍스쳐 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    ''' <param name="ColorKey">투명 색으로 사용할 색을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Img As Image, ByVal ColorKey As Color)

        MyBase.New(Device)

        ' 이미지를 스트림에 저장한다.
        Dim imgStream As New IO.MemoryStream
        Img.Save(imgStream, Img.RawFormat)
        imgStream.Position = 0

        ' 스트림을 텍스쳐로 변환한다.
        g_Texture = D3.TextureLoader.FromStream(Me.GraphicsDevice, imgStream, Img.Width, Img.Height, 0, D3.Usage.None, D3.Format.Unknown, D3.Pool.Managed, D3.Filter.None, D3.Filter.None, ColorKey.ToArgb)

        ' 스트림을 삭제한다.
        imgStream.Dispose()

        ' 크기를 저장한다.
        g_Size = Img.Size

        ' 회전 중심 축을 설정한다.
        g_Center = New Point(g_Size.Width / 2, g_Size.Height / 2)

    End Sub

    '### FromStream (Bitmap)
    ''' <summary>
    ''' 텍스쳐 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Bmp">텍스쳐로 사용할 비트맵 이미지를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Bmp As Bitmap)

        MyBase.New(Device)

        ' 비트맵 이미지를 텍스쳐로 변환한다.
        g_Texture = D3.Texture.FromBitmap(Me.GraphicsDevice, Bmp, D3.Usage.None, D3.Pool.Managed)

        ' 크기를 저장한다.
        g_Size = Bmp.Size

        ' 회전 중심 축을 설정한다.
        g_Center = New Point(g_Size.Width / 2, g_Size.Height / 2)

    End Sub

    ''' <summary>
    ''' 텍스쳐 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Bmp">텍스쳐로 사용할 비트맵 이미지를 입력합니다.</param>
    ''' <param name="ColorKey">투명 색으로 사용할 색을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Bmp As Bitmap, ByVal ColorKey As Color)

        MyBase.New(Device)

        ' 비트맵 이미지를 텍스쳐로 변환한다.
        Dim bsStream As New IO.MemoryStream
        Bmp.Save(bsStream, Imaging.ImageFormat.Bmp)
        bsStream.Position = 0
        g_Texture = D3.TextureLoader.FromStream(Me.GraphicsDevice, bsStream, Bmp.Width, Bmp.Height, 0, D3.Usage.None, D3.Format.Unknown, D3.Pool.Managed, D3.Filter.None, D3.Filter.None, ColorKey.ToArgb)

        ' 사용한 스트림을 메모리에서 제거한다.
        bsStream.Dispose()

        ' 크기를 저장한다.
        g_Size = Bmp.Size

        ' 회전 중심 축을 설정한다.
        g_Center = New Point(g_Size.Width / 2, g_Size.Height / 2)

    End Sub

#End Region

#Region " - Fields - "

    ''' <summary>
    ''' 텍스쳐의 위치를 저장합니다.
    ''' </summary>
    Private g_Location As PointF = PointF.Empty

    ''' <summary>
    ''' 텍스쳐의 텍스쳐를 저장합니다.
    ''' </summary>
    Private g_Texture As D3.Texture = Nothing

    ''' <summary>
    ''' 텍스쳐의 이동 속도를 저장합니다.
    ''' </summary>
    Private g_Speed As Single = 0.0F

    ''' <summary>
    ''' 텍스쳐의 각도를 저장합니다.
    ''' </summary>
    Private g_Angle As Single = 0.0F

    ''' <summary>
    ''' 텍스쳐의 크기를 저장합니다.
    ''' </summary>
    Private g_Size As Size = Size.Empty

    ''' <summary>
    ''' 텍스쳐의 회전 중심 축을 저장합니다.
    ''' </summary>
    Private g_Center As Point = Point.Empty

    ''' <summary>
    ''' 텍스쳐의 투명도를 저장합니다.
    ''' </summary>
    Private g_Opacity As Single = 1.0F

    ''' <summary>
    ''' 텍스쳐의 컬러 키(투명 색)을 저장합니다.
    ''' </summary>
    Private g_ColorKey As Color = Color.White

#End Region

#Region " - Properties - "

    ''' <summary>
    ''' 텍스쳐의 위치와 크기를 저장하는 사각형 개체를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Rectangle As RectangleF Implements IGraphicsResource.Rectangle
        Get
            Return New RectangleF(g_Location, g_Size)
        End Get
    End Property

    ''' <summary>
    ''' 텍스쳐의 각도를 가져오거나 설정합니다. (Degree)
    ''' </summary>
    Public Property Angle As Single
        Get
            Return g_Angle
        End Get
        Set(ByVal value As Single)
            If value >= 360 Then value = value Mod 360
            If value < 0 Then value = 360 - (value Mod 360)
            g_Angle = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스쳐의 이동 속도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Speed As Single
        Get
            Return g_Speed
        End Get
        Set(ByVal value As Single)
            g_Speed = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스쳐의 회전 중심 축을 가져오거나 설정합니다.
    ''' </summary>
    Public Property RotateOrigin As Point
        Get
            Return g_Center
        End Get
        Set(ByVal value As Point)
            g_Center = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As PointF Implements IGraphicsResource.Location
        Get
            Return g_Location
        End Get
        Set(ByVal value As PointF)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single Implements IGraphicsResource.Opacity
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
    ''' 블록을 그릴 때 혼합할 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Color As Color Implements IGraphicsResource.Color
        Get
            Return g_ColorKey
        End Get
        Set(ByVal value As Color)
            g_ColorKey = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 X 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Single Implements IGraphicsResource.X
        Get
            Return g_Location.X
        End Get
        Set(ByVal value As Single)
            g_Location.X = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 Y 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single Implements IGraphicsResource.Y
        Get
            Return g_Location.Y
        End Get
        Set(ByVal value As Single)
            g_Location.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Width As Int32 Implements IGraphicsResource.Width
        Get
            Return g_Size.Width
        End Get
    End Property

    ''' <summary>
    ''' 블록의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Height As Int32 Implements IGraphicsResource.Height
        Get
            Return g_Size.Height
        End Get
    End Property

    ''' <summary>
    ''' 블록의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Size As Size Implements IGraphicsResource.Size
        Get
            Return g_Size
        End Get
    End Property

    ''' <summary>
    ''' 블록의 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Texture As D3.Texture Implements IGraphicsResource.Texture
        Get
            Return g_Texture
        End Get
    End Property

#End Region

#Region " - Methods - "

    ''' <summary>
    ''' 지정된 개체와 충돌이 일어나는지를 검사합니다.
    ''' </summary>
    ''' <param name="Object">충돌 검사를 할 대상을 입력합니다.</param>
    Public Function HasCollision(ByVal [Object] As IGraphicsResource) As Boolean

        Return Me.Rectangle.IntersectsWith([Object].Rectangle)

    End Function

    ''' <summary>
    ''' 지정된 개체들과 충돌이 일어나는지를 검사합니다.
    ''' </summary>
    ''' <param name="Objects">충돌 검사를 할 한개 이상의 개체를 입력합니다.</param>
    Public Function HasCollisions(ByVal Objects() As IGraphicsResource, Optional ByRef TouchedObject As IGraphicsResource = Nothing) As Boolean
        For Each o In Objects
            If Me.HasCollision(o) Then
                TouchedObject = o
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' 텍스쳐를 그립니다.
    ''' </summary>
    ''' <param name="Target">텍스쳐가 그려질 텍스쳐 개체를 입력합니다.</param>
    Public Sub Draw(ByVal Target As D3.Sprite)

        g_ColorKey = Color.FromArgb(g_Opacity * 255, g_ColorKey.R, g_ColorKey.G, g_ColorKey.B)
        Target.Draw2D(Me.Texture, Me.RotateOrigin, D3.Geometry.DegreeToRadian(Me.Angle), Me.Location, Me.Color)

    End Sub

#End Region

#Region " - IGraphicsResources - "

    Private g_Disposed As Boolean = False

    ''' <summary>
    ''' 리소스의 사용을 종료하고, 메모리에서 해제합니다.
    ''' </summary>
    Public Sub Dispose() Implements IGraphicsResource.Dispose

        If g_Disposed Then Exit Sub
        If g_Texture IsNot Nothing Then g_Texture.Dispose()
        g_Disposed = True

    End Sub

    ''' <summary>
    ''' 리소스의 사용이 종료되고, 메모리에서 해제되었는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsResource.Disposed
        Get
            Return g_Disposed
        End Get
    End Property

#End Region

    Public Class TDGSprite

        ''+-----------------------------------------+
        ''|  Class Variables                        |
        ''+-----------------------------------------+

        'Private mAttachedTextList As List(Of TDGText)
        'Private mAttachedSpriteList As List(Of TDGSprite)

        'Private mMainDevice As D3.Device

        ''Private mAsyncAutoUpdate As Boolean         ' 비동기적으로 텍스쳐의 위치를 갱신할 것인지 저장한다.

        'Private mAnimateTextureList As ArrayList    ' 애니메이션 텍스쳐를 저장한다.
        'Private mIsAnimatedSprite As Boolean        ' 이 텍스쳐가 애니메이션 텍스쳐인지 저장한다.
        'Private mAnimateInterval As Int32           ' 애니메이션 텍스쳐를 업데이트할 시간(ms)을 저장한다.
        'Private mLastAnimateIndex As Int32          ' 애니메이션 텍스쳐의 마지막 인덱스를 저장한다.
        'Private mSpriteAnimating As Boolean         ' 애니메이션 텍스쳐가 진행 중인지를 저장한다.
        'Private mAnimateSpriteSize As Size          ' 애니메이션 텍스쳐의 크기를 저장한다.
        'Private mAnimateIndexOrder As ArrayList     ' 애니메이션 텍스쳐의 동작 인덱스를 저장한다.
        'Private mAnimateTimer As Threading.Timer    ' 애니메이션 텍스쳐 갱신 타이머를 저장한다.
        'Private mCurrentAnimateIndex As Int32       ' 현재 애니메이션 텍스쳐의 인덱스를 저장한다.
        'Private mAnimationStopSignal As Int32       ' -

        'Private Sub AnimateSprite(ByVal State As Object)

        '    If mAnimationStopSignal = 1 Then mAnimateTimer.Dispose() : Exit Sub

        '    If mCurrentAnimateIndex > mAnimateIndexOrder.Count - 1 Then mCurrentAnimateIndex = 0
        '    mCurrentAnimateIndex += 1

        'End Sub


        ''+-----------------------------------------+
        ''|  Public Methods                         |
        ''+-----------------------------------------+
        'Public Sub MakeAnimationSprite(ByVal SceneWidth As Int32, ByVal SceneHeight As Int32,
        '                                ByVal StartIndex As Int32, ByVal EndIndex As Int32, ByVal AnimateInterval As Int32)

        '    Dim StepW As Int32 = Int(mSize.Width / SceneWidth),
        '        StepH As Int32 = Int(mSize.Height / SceneHeight),
        '        Indices As Int32 = 0

        '    mAnimateTextureList = New ArrayList()
        '    mAnimateIndexOrder = New ArrayList()

        '    If EndIndex = 0 Then EndIndex = StepW * StepH

        '    For i As Int32 = StartIndex To EndIndex

        '        Dim IndexW As Int32 = i Mod StepW,
        '            IndexH As Int32 = Int(i / StepW)

        '        mAnimateTextureList.Add(LoadTextureFromImage(mMainDevice, GetPartialImage(mImage, New Point(IndexW * SceneWidth, IndexH * SceneHeight), New Size(SceneWidth, SceneHeight)), mColorKey))
        '        mAnimateIndexOrder.Add(Indices)
        '        Indices += 1

        '    Next

        '    mIsAnimatedSprite = True
        '    mAnimateInterval = AnimateInterval
        '    mCenter.X = SceneWidth / 2
        '    mCenter.Y = SceneHeight / 2

        'End Sub
        'Public Sub MakeNormalSprite()

        '    For Each ListItem In mAnimateTextureList
        '        ListItem.Dispose()
        '    Next

        '    mAnimateTextureList = Nothing
        '    mAnimateIndexOrder = Nothing
        '    mCurrentAnimateIndex = 0
        '    mLastAnimateIndex = 0
        '    mIsAnimatedSprite = False
        '    mSpriteAnimating = False
        '    mAnimateTimer.Dispose()

        'End Sub

        'Public Sub StartAnimate()

        '    If Not mIsAnimatedSprite Then Exit Sub
        '    mSpriteAnimating = True
        '    mAnimateTimer = New Threading.Timer(AddressOf AnimateSprite, Nothing, mAnimateInterval, mAnimateInterval)
        '    mAnimationStopSignal = 0

        'End Sub
        'Public Sub StopAnimate()
        '    mAnimationStopSignal = 1
        'End Sub
        'Public Sub Say(ByVal Text As String)
        '    Dim SayText As New TDGText(Text)
        '    SayText.SetLifeTime(4000)

        '    SayText.Location = New Vector2D(0, -20)
        '    mAttachedTextList.Add(SayText)
        'End Sub

    End Class

    Public ReadOnly Property ResourceType As SdxMain.ResourceTypes Implements IGraphicsResource.ResourceType
        Get

        End Get
    End Property
End Class