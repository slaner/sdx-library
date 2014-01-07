Imports System.Runtime.InteropServices

Partial Class SdxMain

    <DllImport("user32")>
    Private Shared Function GetAsyncKeyState(ByVal vkey As Int32) As Int32
    End Function
    <DllImport("user32")>
    Private Shared Function GetCursorPos(ByRef p As Drawing.Point) As Int32
    End Function
    <DllImport("user32")>
    Private Shared Function GetActiveWindow() As IntPtr
    End Function


    ''' <summary>
    ''' 마우스의 위치 정보를 저장합니다.
    ''' </summary>
    Public Class MouseState

        Private Delegate Sub UpdateMouseState(ByVal T As SdxMain)
        Private cbUms As UpdateMouseState = New UpdateMouseState(AddressOf ums)
        Private Sub ums(ByVal T As SdxMain)

            If T.Window.InvokeRequired Then
                T.Window.Invoke(cbUms, T)
            Else
                GetCursorPos(p)
                g_Left = Math.Abs(GetAsyncKeyState(1) And -32767)
                g_Right = Math.Abs(GetAsyncKeyState(2) And -32767)
                g_Middle = Math.Abs(GetAsyncKeyState(4) And -32767)
            End If

        End Sub

        Private p As Drawing.Point
        Private g_Left, g_Right, g_Middle As Int32

        ''' <summary>
        ''' 마우스의 정보를 저장하는 MouseState 개체를 초기화합니다.
        ''' </summary>
        ''' <param name="Main">주 개체를 입력합니다.</param>
        Public Sub New(ByVal Main As SdxMain)

            ums(Main)

        End Sub

        ''' <summary>
        ''' 마우스의 X좌표를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property X As Int32
            Get
                Return p.X
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 Y좌표를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Y As Int32
            Get
                Return p.Y
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 위치를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Location As Drawing.Point
            Get
                Return p
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 왼쪽 버튼이 눌렸는지의 여부를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property LeftButton As Int32
            Get
                Return g_Left
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 오른쪽 버튼이 눌렸는지의 여부를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property RightButton As Int32
            Get
                Return g_Right
            End Get
        End Property

        ''' <summary>
        ''' 마우스의 가운데 버튼이 눌렸는지의 여부를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property MiddleButton As Int32
            Get
                Return g_Middle
            End Get
        End Property

    End Class

End Class