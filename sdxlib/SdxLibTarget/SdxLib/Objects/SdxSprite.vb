Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX
Imports DI = Microsoft.DirectX.DirectInput
Imports DS = Microsoft.DirectX.DirectSound

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Microsoft.DirectX.DirectInput
Imports Microsoft.DirectX.DirectPlay
Imports Microsoft.DirectX.DirectSound

Imports System.Drawing

Public Class SdxSprite2D
    Inherits SdxGraphicsObject

#Region " - Constructor - "

    ''' <summary>
    ''' 스프라이트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    ''' <param name="ImgFormat">텍스쳐로 사용할 이미지의 형식을 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Img As Image, ByVal ImgFormat As Imaging.ImageFormat)

        MyBase.New(Device)

        ' 이미지를 스트림에 저장한다.
        Dim imgStream As IO.Stream = IO.Stream.Null
        Img.Save(imgStream, ImgFormat)

        ' 스트림을 텍스쳐로 변환한다.
        g_Texture = D3.TextureLoader.FromStream(Me.GraphicsDevice, imgStream)

        ' 스트림을 삭제한다.
        imgStream.Dispose()

        ' 크기를 저장한다.
        g_Size = Img.Size

        ' 회전 중심 축을 설정한다.
        g_Center = New PointF(g_Size.Width / 2, g_Size.Height / 2)

    End Sub

    ''' <summary>
    ''' 스프라이트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Bmp">텍스쳐로 사용할 비트맵 이미지를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Bmp As Bitmap)

        MyBase.New(Device)

        ' 비트맵 이미지를 텍스쳐로 변환한다.
        g_Texture = Texture.FromBitmap(MyBase.GraphicsDevice, Bmp, Usage.None, Pool.Managed)

        ' 크기를 저장한다.
        g_Size = Bmp.Size

        ' 회전 중심 축을 설정한다.
        g_Center = New PointF(g_Size.Width / 2, g_Size.Height / 2)

    End Sub

#End Region
#Region " - Fields - "

    ''' <summary>
    ''' 스프라이트의 위치를 저장합니다.
    ''' </summary>
    Private g_Location As PointF

    ''' <summary>
    ''' 스프라이트의 텍스쳐를 저장합니다.
    ''' </summary>
    Private g_Texture As D3.Texture

    ''' <summary>
    ''' 스프라이트의 이동 속도를 저장합니다.
    ''' </summary>
    Private g_Speed As Single

    ''' <summary>
    ''' 스프라이트의 각도를 저장합니다.
    ''' </summary>
    Private g_Angle As Single

    ''' <summary>
    ''' 스프라이트의 크기를 저장합니다.
    ''' </summary>
    Private g_Size As Size

    ''' <summary>
    ''' 스프라이트의 회전 중심 축을 저장합니다.
    ''' </summary>
    Private g_Center As PointF

    ''' <summary>
    ''' 스프라이트의 투명도를 저장합니다.
    ''' </summary>
    Private g_Opacity As Single

    ''' <summary>
    ''' 스프라이트의 컬러 키(투명 색)을 저장합니다.
    ''' </summary>
    Private g_ColorKey As Color

#End Region



#Region " - Properties - "

    ''' <summary>
    ''' 스프라이트의 각도를 가져오거나 설정합니다. (Degree)
    ''' </summary>
    Public Property Angle As Single
        Get
            Return g_Angle
        End Get
        Set(ByVal value As Single)
            If value >= 360 Then value = 0
            If value <= 0 Then value = 360
            g_Angle = value
        End Set
    End Property

    ''' <summary>
    ''' 스프라이트의 이동 속도를 가져오거나 설정합니다.
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
    ''' 스프라이트의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As PointF
        Get
            Return g_Location
        End Get
        Set(ByVal value As PointF)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 스프라이트의 회전 중심 축을 가져오거나 설정합니다.
    ''' </summary>
    Public Property RotateOrigin As PointF
        Get
            Return g_Center
        End Get
        Set(ByVal value As PointF)
            g_Center = value
        End Set
    End Property

    ''' <summary>
    ''' 스프라이트의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single
        Get
            Return g_Opacity
        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    ''' <summary>
    ''' 스프라이트의 컬러 키(투명 색)를 가져오거나 설정합니다.
    ''' </summary>
    Public Property ColorKey As Color
        Get
            Return g_ColorKey
        End Get
        Set(ByVal value As Color)
            g_ColorKey = value
        End Set
    End Property



    ''' <summary>
    ''' 스프라이트의 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Texture As D3.Texture
        Get
            Return g_Texture
        End Get
    End Property

#End Region

#Region " - Methods - "

    ''' <summary>
    ''' 텍스쳐를 그립니다.
    ''' </summary>
    ''' <param name="Target">텍스쳐가 그려질 스프라이트 개체를 입력합니다.</param>
    Public Sub Draw(ByVal Target As D3.Sprite)

        Target.Draw2D(Me.Texture, Me.RotateOrigin, D3.Geometry.DegreeToRadian(Me.Angle), Me.Location, Me.ColorKey)

    End Sub

#End Region

    Public Class TDGSprite

        ''+-----------------------------------------+
        ''|  Class Variables                        |
        ''+-----------------------------------------+

        'Private mAttachedTextList As List(Of TDGText)
        'Private mAttachedSpriteList As List(Of TDGSprite)

        'Private mMainDevice As D3.Device

        ''Private mAsyncAutoUpdate As Boolean         ' 비동기적으로 스프라이트의 위치를 갱신할 것인지 저장한다.

        'Private mAnimateTextureList As ArrayList    ' 애니메이션 스프라이트를 저장한다.
        'Private mIsAnimatedSprite As Boolean        ' 이 스프라이트가 애니메이션 스프라이트인지 저장한다.
        'Private mAnimateInterval As Int32           ' 애니메이션 스프라이트를 업데이트할 시간(ms)을 저장한다.
        'Private mLastAnimateIndex As Int32          ' 애니메이션 스프라이트의 마지막 인덱스를 저장한다.
        'Private mSpriteAnimating As Boolean         ' 애니메이션 스프라이트가 진행 중인지를 저장한다.
        'Private mAnimateSpriteSize As Size          ' 애니메이션 스프라이트의 크기를 저장한다.
        'Private mAnimateIndexOrder As ArrayList     ' 애니메이션 스프라이트의 동작 인덱스를 저장한다.
        'Private mAnimateTimer As Threading.Timer    ' 애니메이션 스프라이트 갱신 타이머를 저장한다.
        'Private mCurrentAnimateIndex As Int32       ' 현재 애니메이션 스프라이트의 인덱스를 저장한다.
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

End Class