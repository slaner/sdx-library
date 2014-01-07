Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX
Imports DI = Microsoft.DirectX.DirectInput
Imports DS = Microsoft.DirectX.DirectSound

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Imports System.Windows.Forms

Public Class SdxMain

#Region " - Apis - "

#End Region

#Region " - Fields - "

    Private m_Device As D3.Device
    Private m_SpriteList As List(Of SdxSprite2D)
    Private m_Sprite As D3.Sprite

    '### Threading
    Private m_RenderThread As System.Threading.Thread
    Private m_StopThread As Boolean

#End Region

#Region " - Constructor - "

    ''' <summary>
    ''' DirectX 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Window">그래픽이 출력될 대상 윈도우를 입력합니다.</param>
    Public Sub New(ByVal Window As Form)

        Debug.Write("Creating Device...")

        ' DirectX 장치를 초기화한다.
        Dim pPresent As PresentParameters = createDefaultPresentParameters(Window)
        m_Device = New D3.Device(0, D3.DeviceType.Hardware, Window.Handle, D3.CreateFlags.HardwareVertexProcessing, pPresent)

        Debug.Print("Done")
        Debug.Write("Creating Sprite...")

        ' 텍스쳐를 그릴 스프라이트 개체를 초기화한다.
        m_Sprite = New D3.Sprite(m_Device)

        Debug.Print("Done")
        Debug.Write("Creating Thread...")

        ' 쓰레드를 생성한다.
        m_RenderThread = New System.Threading.Thread(AddressOf RenderProc)

        Debug.Print("Done")
        Debug.Print("Success")

    End Sub

    ''' <summary>
    ''' DirectX 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Window">그래픽이 출력될 대상 윈도우를 입력합니다.</param>
    ''' <param name="FullScreen">전체 화면으로 표시하려는 경우, 이 값을 True 로 입력합니다.</param>
    Public Sub New(ByVal Window As Form, ByVal FullScreen As Boolean)

        ' DirectX 장치를 초기화한다.
        m_Device = New D3.Device(0, D3.DeviceType.Hardware, Window.Handle, D3.CreateFlags.HardwareVertexProcessing, createDefaultPresentParameters(Window, FullScreen))

        ' 텍스쳐를 그릴 스프라이트 개체를 초기화한다.
        m_Sprite = New D3.Sprite(m_Device)

        ' 쓰레드를 생성한다.
        m_RenderThread = New System.Threading.Thread(AddressOf RenderProc)

    End Sub

    ''' <summary>
    ''' DirectX 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Window">그래픽이 출력될 대상 윈도우를 입력합니다.</param>
    ''' <param name="PresentParameter">만들어질 DirectX 장치의 옵션을 결정하는 PresentParameters 개체를 입력합니다.</param>
    Public Sub New(ByVal Window As Form, ByVal PresentParameter As PresentParameters)

        ' DirectX 장치를 초기화한다.
        m_Device = New D3.Device(0, D3.DeviceType.Hardware, Window.Handle, D3.CreateFlags.HardwareVertexProcessing, PresentParameter)

        ' 텍스쳐를 그릴 스프라이트 개체를 초기화한다.
        m_Sprite = New D3.Sprite(m_Device)

        ' 쓰레드를 생성한다.
        m_RenderThread = New System.Threading.Thread(AddressOf RenderProc)

    End Sub

#End Region

#Region " - Properties - "

    Public ReadOnly Property Sprites As List(Of SdxSprite2D)
        Get
            Return m_SpriteList
        End Get
    End Property

#End Region

#Region " - Public Methods - "

    ''' <summary>
    ''' 그리기를 시작합니다.
    ''' </summary>
    Public Sub Run()

        m_RenderThread.Start()

    End Sub

    ''' <summary>
    ''' 한 프레임을 그립니다.
    ''' 만약 Run 메서드를 사용하여 그리기를 진행중인 경우,
    ''' RunOnce 메서드에 의한 그리기가 끝나는 즉시 Run 메서드에 의해 시작된 그리기는 중단됩니다.
    ''' </summary>
    Public Sub RunOnce()

        Draw()
        m_StopThread = True

    End Sub

    ''' <summary>
    ''' 프레임 그리기를 중단합니다.
    ''' </summary>
    Public Sub [Stop]()

        m_StopThread = True
        m_RenderThread.Abort()

    End Sub



    ''' <summary>
    ''' 비트맵으로부터 2D 스프라이트를 만듭니다.
    ''' </summary>
    ''' <param name="Bmp">2D 스프라이트를 만들 비트맵을 입력합니다.</param>
    Public Function CreateSdxSprite2DFromBitmap(ByVal Bmp As System.Drawing.Bitmap) As SdxSprite2D

        Return New SdxSprite2D(m_Device, Bmp)

    End Function

    ''' <summary>
    ''' 이미지로부터 2D 스프라이트를 만듭니다.
    ''' </summary>
    ''' <param name="Img">2D 스프라이트를 만들 이미지를 입력합니다.</param>
    ''' <param name="ImgFormat">2D 스프라이트를 만들 이미지의 형식을 입력합니다.</param>
    Public Function CreateSdxSprite2DFromImage(ByVal Img As System.Drawing.Image, ByVal ImgFormat As System.Drawing.Imaging.ImageFormat) As SdxSprite2D

        Return New SdxSprite2D(m_Device, Img, ImgFormat)

    End Function

#End Region

#Region " - Private Methods - "

    ''' <summary>
    ''' 그리기 작업을 하는 쓰레드 호출 함수입니다.
    ''' </summary>
    Private Sub RenderProc()

        Do

            If m_StopThread Then Exit Do
            Draw()

        Loop
        m_StopThread = False

    End Sub

    ''' <summary>
    ''' 프레임을 그립니다.
    ''' </summary>
    Private Sub Draw()

        m_Sprite.Begin(SpriteFlags.AlphaBlend)

        For Each s2d As SdxSprite2D In m_SpriteList
            s2d.Draw(m_Sprite)
        Next

        m_Sprite.End()

    End Sub

    ''' <summary>
    ''' 주어진 개체가 윈도우가 맞는지 아닌지 확인합니다.
    ''' </summary>
    ''' <param name="Object">확인할 개체를 입력합니다.</param>
    <Obsolete("이 메서드는 사용되지 않습니다.", True)> _
    Private Function IsWinForm(ByVal [Object] As Object) As Boolean

        ' 개체가 Nothing 인지 아닌지 확인한다
        If IsNothing([Object]) Then Return False

        ' 개체의 필드/메서드 중 Handle 이 있는지 없는지 확인한다.
        Try
            Debug.Assert([Object].Handle)
        Catch ex1 As MissingFieldException
            Return False
        Catch ex2 As MissingMethodException
            Return False
        Catch ex3 As MissingMemberException
            Return False
        Catch ex As Exception
            Debug.Print("Another Exception at CheckIsWindow")
            Return False
        End Try

        'If IsWindow([Object].Handle) Then Return True
        Return False

    End Function

    ''' <summary>
    ''' 기본 설정의 PresentParameters 개체를 만듭니다.
    ''' </summary>
    ''' <param name="Window">PresentParameters 개체를 만드는데 필요한 정보를 가져올 때 사용할 윈도우를 입력합니다.</param>
    ''' <param name="FullScreen">전체 화면으로 표시하려는 경우, 이 값을 True 로 입력합니다.</param>
    Private Function createDefaultPresentParameters(ByVal Window As Form, Optional ByVal FullScreen As Boolean = False) As D3.PresentParameters

        Dim pPresent As New D3.PresentParameters()

        With pPresent

            ' 백버퍼 갯수
            .BackBufferCount = 1
            .AutoDepthStencilFormat = D3.DepthFormat.D16
            .EnableAutoDepthStencil = True
            .DeviceWindowHandle = Window.Handle
            .SwapEffect = D3.SwapEffect.Flip
            .Windowed = True
            .PresentationInterval = D3.PresentInterval.Default

            If FullScreen Then

                .Windowed = False
                .BackBufferWidth = Window.Width
                .BackBufferHeight = Window.Height
                .BackBufferFormat = D3.Format.A8R8G8B8
                .FullScreenRefreshRateInHz = D3.Manager.Adapters(0).CurrentDisplayMode.RefreshRate

            End If

        End With

        Return pPresent

    End Function

#End Region

End Class