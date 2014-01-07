' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxMain.vb      [CORE]
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   Microsoft.DirectX.DirectInput
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
'   Defines SdxMain class.

Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX
Imports DI = Microsoft.DirectX.DirectInput

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Public Class SDXMain
    Implements IDisposable

#Region " - Events - "

#Region "   - Inputs - "

    ''' <summary>
    ''' 키보드 상태 정보를 가져왔을 때 발생하는 이벤트입니다.
    ''' </summary>
    Public Event OnObtainKeyboardState(ByVal KeyboardState As DI.KeyboardState)

    ''' <summary>
    ''' 키보드의 키가 눌렸을 때 발생하는 이벤트입니다.
    ''' </summary>
    Public Event OnKeyPressed(ByVal Keys() As DI.Key)

#End Region

#Region "   - Sprites - "

    ''' <summary>
    ''' 메인 텍스쳐가 시작될 때 발생합니다.
    ''' </summary>
    Public Event OnSpriteBegin(ByVal Target As D3.Sprite)

    ''' <summary>
    ''' 메인 텍스쳐가 종료되기 전에 발생합니다.
    ''' </summary>
    Public Event PreSpriteEnd(ByVal Target As D3.Sprite)

    ''' <summary>
    ''' 메인 텍스쳐가 종료될 때 발생합니다.
    ''' </summary>
    Public Event OnSpriteEnd()

#End Region

#Region "   - Scenes - "

    ''' <summary>
    ''' 씬이 시작될 때 발생하는 이벤트입니다.
    ''' </summary>
    Public Event OnBeginScene()

    ''' <summary>
    ''' 씬이 종료될 때 발생하는 이벤트입니다.
    ''' </summary>
    Public Event OnEndScene()

#End Region

#Region "   - Frames - "

    ''' <summary>
    ''' 프레임이 그려지기 전에 발생하는 이벤트입니다.
    ''' </summary>
    Public Event PreDrawFrame()

    ''' <summary>
    ''' 프레임이 그려진 후에 발생하는 이벤트입니다.
    ''' </summary>
    Public Event PostDrawFrame()

    ''' <summary>
    ''' 프레임이 그려질 때 발생하는 이벤트입니다.
    ''' PreDrawFrame 이벤트보단 나중에 발생하고, PostDrawFrame 이벤트보단 먼저 발생합니다.
    ''' </summary>
    ''' <param name="FrameRate">현재 프레임 갱신률을 나타냅니다.</param>
    Public Event OnDrawFrame(ByVal FrameRate As Int32)

#End Region

    ''' <summary>
    ''' 백버퍼를 배경색으로 칠했을 때 발생하는 이벤트입니다.
    ''' </summary>
    Public Event OnPaintBackground()


    ''' <summary>
    ''' 백버퍼에 그려진 그래픽이 화면으로 출력되기 바로 전 단계에 발생하는 이벤트입니다.
    ''' </summary>
    Public Event PrePresent()


    ''' <summary>
    ''' 개체가 성공적으로 초기화된 경우에 발생하는 이벤트입니다.
    ''' </summary>
    Public Event Initialize()

#End Region

#Region " - Fields - "

    ''' <summary>
    ''' 게임의 진행 여부를 저장합니다.
    ''' </summary>
    Private g_GameRunning As Boolean

    ''' <summary>
    ''' 윈도우 창의 클라이언트 영역을 저장합니다.
    ''' </summary>
    Private g_WindowSize As Drawing.Size

    ''' <summary>
    ''' 컨트롤 목록을 저장합니다.
    ''' </summary>
    Private g_Controls As ExtendList(Of Controls.SDXControl)

    ''' <summary>
    ''' 컨트롤의 텍스트를 렌더링할 때 사용할 스프라이트 개체를 저장합니다.
    ''' </summary>
    Private m_ControlTextRenderingSprite As D3.Sprite

    ''' <summary>
    ''' 마우스 상태를 저장합니다.
    ''' </summary>
    Private g_MouseState As Devices.SDXMouse

    ''' <summary>
    ''' DirectX의 주 출력 윈도우를 저장합니다.
    ''' </summary>
    Private m_Window As SDXWindow

    ''' <summary>
    ''' 뷰 로케이션(월드)을 저장합니다.
    ''' </summary>
    Private g_ViewLocation As Vector2D

    ''' <summary>
    ''' 월드의 중력을 저장합니다.
    ''' </summary>
    Private g_WorldGravity As Int32



    '### Shared Resource (SR)
    Private g_SharedResource As SdxSharedResource

    '### Lists
    Private g_ControlPlayers As ExtendList(Of SdxPlayer)
    Private g_Bullets As ExtendList(Of SdxBullet)

    '### Input
    ' : 키보드의 상태를 얻어올 때 사용됨
    Private m_Keyboard As DI.Device
    Private m_Mouse As DI.Device

    '### Frame
    ' : 화면 갱신률을 얻어올 때 사용됨
    Private g_FrameRate As Int32
    Private g_CustomStep As Boolean
    Private g_StepUpdateTime As TimeSpan

    '### Window/Event
    ' : DirectX 의 주 출력 윈도우를 저장,
    '   윈도우가 종료될 때 발생하는 이벤트 저장
    Private m_FormClosingEventProc As FormClosingEventHandler

    '### Sprite
    'Private g_TextureManager As SdxTextureManager

    ''### Font/Text
    'Private g_TextManager As SdxTextManager

    '### Rendering
    ' : 그리기 전 백버퍼를 무슨 색으로 칠할지 저장함
    Private m_BackColor As System.Drawing.Color

    '### Event Callback
    Private m_OnDeviceReset As EventHandler
    Private m_OnDeviceLost As EventHandler
    Private m_OnDeviceResizing As System.ComponentModel.CancelEventHandler

    '### Device
    Private m_Device As D3.Device

    Private m_ImageSprite As D3.Sprite
    Private m_TextSprite As D3.Sprite

    ''' <summary>
    ''' 선을 그릴 때 사용합니다.
    ''' </summary>
    Private m_Line As D3.Line

    ''' <summary>
    ''' 텍스쳐를 매핑한 선을 그릴 때 사용합니다.
    ''' </summary>
    Private m_LineSprite As D3.Sprite

    Private m_BGISprite As D3.Sprite
    Private m_BgiTexture As D3.Texture
    Private g_BackgroundImage As Drawing.Image
    Private g_BGILayout As ImageLayout = ImageLayout.Default

    Private g_GameMode As GameMode = GameMode.Default

    '### Object List
    Private g_TextList As ExtendList(Of IGraphicsText)
    Private g_BlockList As ExtendList(Of SdxBlock2D)
    Private g_Lines As ExtendList(Of IGraphicsLine)
    'Private g_Menus As ExtendList(Of SdxMenu)

    Private g_DrawingFrame As Boolean

    Private g_Effects As ExtendList(Of SdxEffect)

#End Region

#Region " - Public Methods - "



    ''' <summary>
    ''' DirectX 개체의 사용을 종료하고 메모리에 할당된 모든 개체를 삭제합니다.
    ''' </summary>
    Public Sub Dispose() Implements System.IDisposable.Dispose

        g_GameRunning = False

        ' 이벤트 핸들러를 제거한다.
        RemoveHandler m_Window.FormClosing, m_FormClosingEventProc
        RemoveHandler m_Device.DeviceReset, m_OnDeviceReset
        RemoveHandler m_Device.DeviceLost, m_OnDeviceLost
        RemoveHandler m_Device.DeviceResizing, m_OnDeviceResizing

        '' 프레임 그리기가 끝날때가지 대기한다.
        'Do : If Not g_DrawingFrame Then Exit Do
        'Loop

        ' 개체 삭제:
        For Each o In g_BlockList : o.Dispose() : Next      ' 블록 삭제
        For Each o In g_Bullets : o.Dispose() : Next        ' 총알 삭제
        For Each o In g_Lines : o.Dispose() : Next          ' 선 삭제
        For Each o In g_ControlPlayers : o.Dispose() : Next ' 플레이어 삭제
        For Each o In g_TextList : o.Dispose() : Next       ' 텍스트 삭제
        For Each o In g_Effects : o.Dispose() : Next        ' 효과 삭제

        ' 스프라이트 제거:
        m_BGISprite.Dispose()
        m_TextSprite.Dispose()
        m_LineSprite.Dispose()
        m_ImageSprite.Dispose()

        ' 공유 리소스 제거:
        g_SharedResource.Dispose()

        ' 주 DirectX 장치를 삭제한다.
        m_Device.Dispose()

    End Sub

#End Region

#Region " - Private Methods - "

    Private Sub OnDeviceReset(ByVal o As Object, ByVal e As EventArgs)

        '
        Debug.Print("OnDeviceReset")

    End Sub
    Private Sub OnDeviceLost(ByVal o As Object, ByVal e As EventArgs)

        '
        Debug.Print("OnDeviceLost")

    End Sub
    Private Sub OnDeviceResizing(ByVal o As Object, ByVal e As System.ComponentModel.CancelEventArgs)

        '
        Debug.Print("OnDeviceResizing")

    End Sub

#End Region

End Class