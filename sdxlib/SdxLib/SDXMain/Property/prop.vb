' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxMain/prop.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Windows.Forms
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  2  .  8  .  47
'
' Date:
'   2013/12/10
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxMain class's property.

Imports System.Windows.Forms
Imports D3 = Microsoft.DirectX.Direct3D

Partial Class SDXMain

#Region "R/W Property"

    ''' <summary>
    ''' 게임 상의 중력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property WorldGravity As Int32
        Get
            Return g_WorldGravity
        End Get
        Set(ByVal value As Int32)
            g_WorldGravity = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 정의 업데이트 시간을 가져오거나 설정합니다.
    ''' </summary>
    Public Property UpdateTime As TimeSpan
        Get
            Return g_StepUpdateTime
        End Get
        Set(ByVal value As TimeSpan)
            g_StepUpdateTime = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 정의 업데이트 시간을 사용할 것인지 나타내는 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property UseCustomStep As Boolean
        Get
            Return g_CustomStep
        End Get
        Set(ByVal value As Boolean)
            g_CustomStep = value
        End Set
    End Property

    ''' <summary>
    ''' 그리기 작업에서 화면을 초기화할 때 사용할 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property BackColor As System.Drawing.Color
        Get
            Return m_BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            m_BackColor = value
        End Set
    End Property

    ''' <summary>
    ''' 뷰 로케이션을 가져오거나 설정합니다.
    ''' </summary>
    Public Property ViewLocation As Vector2D
        Get
            Return g_ViewLocation
        End Get
        Set(ByVal value As Vector2D)
            g_ViewLocation = value
        End Set
    End Property

    ''' <summary>
    ''' 뷰 로케이션의 X 좌표 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property ViewX As Single
        Get
            Return g_ViewLocation.X
        End Get
        Set(ByVal value As Single)
            g_ViewLocation.X = value
        End Set
    End Property

    ''' <summary>
    ''' 뷰 로케이션의 Y 좌표 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property ViewY As Single
        Get
            Return g_ViewLocation.Y
        End Get
        Set(ByVal value As Single)
            g_ViewLocation.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 배경 이미지를 가져오거나 설정합니다.
    ''' </summary>
    Public Property BackgroundImage As Drawing.Image
        Get
            Return g_BackgroundImage
        End Get
        Set(ByVal value As Drawing.Image)
            m_BgiTexture = SDXHelper.TextureFromImage(m_Device, value)
            g_BackgroundImage = value
        End Set
    End Property

    ''' <summary>
    ''' 배경 이미지를 표시하는 방법을 가져오거나 설정합니다.
    ''' </summary>
    Public Property BackgroundImageLayout As ImageLayout
        Get
            Return g_BGILayout
        End Get
        Set(ByVal value As ImageLayout)
            g_BGILayout = value
        End Set
    End Property

#End Region

#Region "ReadOnly Property"

    ''' <summary>
    ''' 게임이 진행 중인지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property IsRunning As Boolean
        Get
            Return g_GameRunning
        End Get
    End Property

    ''' <summary>
    ''' 마우스 상태를 가져옵니다. (마우스 상태는 매 프레임마다 갱신됩니다)
    ''' </summary>
    Public ReadOnly Property MouseState As Devices.SDXMouse
        Get
            Return g_MouseState
        End Get
    End Property

    ''' <summary>
    ''' 초당 프레임 수를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property FrameRate As Int32
        Get
            Return g_FrameRate
        End Get
    End Property

    ''' <summary>
    ''' DirectX 개체의 주 출력 창을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Window As SDXWindow
        Get
            Return m_Window
        End Get
    End Property

    ''' <summary>
    ''' 프레임을 그리고 있는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property DrawingFrame As Boolean
        Get
            Return g_DrawingFrame
        End Get
    End Property

    ''' <summary>
    ''' DirectX 개체의 주 출력 창의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property WindowSize As Drawing.Size
        Get
            Return g_WindowSize
        End Get
    End Property

#End Region

#Region "List Property"

    ''' <summary>
    ''' 플레이어 목록을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Players As ExtendList(Of SdxPlayer)
        Get
            Return g_ControlPlayers
        End Get
    End Property

    ''' <summary>
    ''' 총알 목록을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Bullets As ExtendList(Of SdxBullet)
        Get
            Return g_Bullets
        End Get
    End Property

    ''' <summary>
    ''' 선 목록을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Lines As ExtendList(Of IGraphicsLine)
        Get
            Return g_Lines
        End Get
    End Property

    ''' <summary>
    ''' 효과 목록을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Effects As ExtendList(Of SdxEffect)
        Get
            Return g_Effects
        End Get
    End Property

    Public ReadOnly Property Components As ExtendList(Of Controls.SDXControl)
        Get
            Return g_Controls
        End Get
    End Property

#End Region

#Region "* Internal Access"

    ''' <summary>
    ''' DirectX 주 개체를 가져옵니다.
    ''' </summary>
    Friend ReadOnly Property Device As D3.Device
        Get
            Return m_Device
        End Get
    End Property

    ''' <summary>
    ''' 주 그리기 대상 스프라이트 개체를 가져옵니다.
    ''' </summary>
    Friend ReadOnly Property ImageSprite As D3.Sprite
        Get
            Return m_ImageSprite
        End Get
    End Property

    ''' <summary>
    ''' 텍스트 그리기 대상 스프라이트 개체를 가져옵니다.
    ''' </summary>
    Friend ReadOnly Property TextSprite As D3.Sprite
        Get
            Return m_TextSprite
        End Get
    End Property

    ''' <summary>
    ''' 공유 리소스를 관리하는 SdxSharedResource 개체를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property SharedResource As SdxSharedResource
        Get
            Return g_SharedResource
        End Get
    End Property

#End Region

    ''' <summary>
    ''' 그려질 2D 블록 목록을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Blocks As ExtendList(Of SdxBlock2D)
        Get
            Return g_BlockList
        End Get
    End Property

    ''' <summary>
    ''' 그려질 텍스트 목록을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Texts As ExtendList(Of IGraphicsText)
        Get
            Return g_TextList
        End Get
    End Property



End Class