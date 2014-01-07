' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxMain/InternalMethod.vb
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
'   Defines SdxMain class's method (Internal).

Imports D3 = Microsoft.DirectX.Direct3D
Imports DI = Microsoft.DirectX.DirectInput
Imports System.Drawing
Imports System.Windows.Forms

Partial Class SDXMain

    Private Function InitDX(ByVal ClientSize As Size, Optional ByVal ApplicationTitle As String = "SDXApplication", Optional ByVal FullScreen As Boolean = False, Optional ByVal PresentParameter As D3.PresentParameters = Nothing) As Boolean

        ' Create a Form
        Debug.Write("Creating/Initializing Window...")
        Try

            m_Window = New SDXWindow
            m_Window.ClientSize = ClientSize
            m_Window.Text = ApplicationTitle
            m_Window.StartPosition = FormStartPosition.CenterScreen
            m_Window.Visible = True
            m_Window.Select()

        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")

        ' DirectX Initializer
        Debug.Write("Creating DirectX Device...")
        Dim pPresent As D3.PresentParameters = DefaultPresentParameters(m_Window, FullScreen)
        Try
            m_Device = New D3.Device(0, D3.DeviceType.Hardware, m_Window.Handle, D3.CreateFlags.HardwareVertexProcessing, IIf(PresentParameter Is Nothing, pPresent, PresentParameter))
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating DirectX:Keyboard and Mouse Device...")
        Try
            m_Keyboard = New DI.Device(DI.SystemGuid.Keyboard)
            m_Keyboard.SetDataFormat(DI.DeviceDataFormat.Keyboard)
            m_Keyboard.SetCooperativeLevel(m_Window, DI.CooperativeLevelFlags.Foreground Or DI.CooperativeLevelFlags.NonExclusive)
            m_Mouse = New DI.Device(DI.SystemGuid.Mouse)
            m_Mouse.SetDataFormat(DI.DeviceDataFormat.Mouse)
            m_Mouse.SetCooperativeLevel(m_Window, Microsoft.DirectX.DirectInput.CooperativeLevelFlags.Foreground Or Microsoft.DirectX.DirectInput.CooperativeLevelFlags.NonExclusive)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating EventHandler for Device Events...")
        Try
            m_OnDeviceLost = New EventHandler(AddressOf OnDeviceLost)
            m_OnDeviceReset = New EventHandler(AddressOf OnDeviceReset)
            m_OnDeviceResizing = New System.ComponentModel.CancelEventHandler(AddressOf OnDeviceResizing)
            AddHandler m_Device.DeviceLost, m_OnDeviceLost
            AddHandler m_Device.DeviceReset, m_OnDeviceReset
            AddHandler m_Device.DeviceResizing, m_OnDeviceResizing
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating Image Sprite...")
        Try
            ' 이미지 텍스쳐를 그릴 텍스쳐 개체를 초기화한다.
            m_ImageSprite = New D3.Sprite(m_Device)
            m_Line = New D3.Line(m_Device)
            m_LineSprite = New D3.Sprite(m_Device)
            m_ControlTextRenderingSprite = New D3.Sprite(m_Device)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating Text Sprite...")
        Try
            ' 텍스트를 그릴 스프라이트 개체를 초기화한다.
            m_TextSprite = New D3.Sprite(m_Device)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating Background Sprite...")
        Try
            m_BGISprite = New D3.Sprite(m_Device)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating List...")
        Try
            g_TextList = New ExtendList(Of IGraphicsText)
            g_BlockList = New ExtendList(Of SdxBlock2D)
            g_ControlPlayers = New ExtendList(Of SdxPlayer)
            g_Bullets = New ExtendList(Of SdxBullet)
            g_Lines = New ExtendList(Of IGraphicsLine)
            'g_Menus = New ExtendList(Of SdxMenu)
            g_Effects = New ExtendList(Of SdxEffect)
            g_Controls = New ExtendList(Of Controls.SDXControl)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating EventHandler for FormClosing Event...")
        Try
            m_FormClosingEventProc = New FormClosingEventHandler(AddressOf Dispose)
            AddHandler m_Window.FormClosing, m_FormClosingEventProc
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Creating Texture from Resource (SR)...")
        Try
            g_SharedResource = New SdxSharedResource(Me)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Write("Initializing Variables...")
        Try
            m_BackColor = System.Drawing.Color.White
            g_WindowSize = m_Window.ClientSize
            g_MouseState = New Devices.SDXMouse(Me)
            'g_TextureManager = New SdxTextureManager(m_Device)
            'g_TextManager = New SdxTextManager(m_Device, m_ImageSprite)
        Catch ex As Exception
            Debug.Print("Failure")
            Return False
        End Try
        Debug.Print("Done")


        Debug.Print("DirectX Initialize Successfully!")
        Return True

        RaiseEvent Initialize()

    End Function
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
    Private Function DefaultPresentParameters(ByVal Window As Form, Optional ByVal FullScreen As Boolean = False) As D3.PresentParameters

        Dim pPresent As New D3.PresentParameters()

        With pPresent

            ' 백버퍼 갯수
            .BackBufferCount = 1
            .AutoDepthStencilFormat = D3.DepthFormat.D16
            .EnableAutoDepthStencil = True
            .DeviceWindowHandle = Window.Handle
            .SwapEffect = D3.SwapEffect.Discard
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

End Class