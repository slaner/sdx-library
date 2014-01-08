Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Namespace Controls

    ''' <summary>
    ''' 그래픽 컴포넌트 요소를 구현합니다.
    ''' </summary>
    Public Class SDXControl
        Inherits SdxObject

        ''' <summary>
        ''' 컨트롤의 마우스 입력에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        ''' <param name="Button">눌린 마우스 버튼을 가져옵니다.</param>
        ''' <param name="Location">이벤트가 발생한 마우스의 위치를 가져옵니다.</param>
        Public Delegate Sub SDXControlMouseEventHandler(ByVal Button As MouseButton, ByVal Location As Point)

        ''' <summary>
        ''' 컨트롤의 키보드 입력에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        ''' <param name="Key">눌린 키보드 버튼을 가져옵니다.</param>
        Public Delegate Sub SDXControlKeyboardEventHandler(ByVal Key As Windows.Forms.Keys)

        ''' <summary>
        ''' 처리할 데이터가 없는 컨트롤에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        Public Delegate Sub SDXControlEmptyEventHandler()

        ''' <summary>
        ''' 컨트롤의 속성 변경에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        ''' <param name="OldValue">속성이 변경되기 이전의 값을 입력합니다.</param>
        ''' <param name="NewValue">변경된 속성의 값을 입력합니다.</param>
        Public Delegate Sub SDXControlPropertyChangedHandler(ByVal OldValue As Object, ByVal NewValue As Object)


        ''' <summary>
        ''' 요소가 나타내는 제목을 저장합니다.
        ''' </summary>
        Private g_Text As String

        ''' <summary>
        ''' 요소의 위치를 저장합니다.
        ''' </summary>
        Private g_Location As Point

        ''' <summary>
        ''' 요소의 배경 색을 저장합니다.
        ''' </summary>
        Private g_BackColor As Color = Color.White

        ''' <summary>
        ''' 요소의 크기를 저장합니다.
        ''' </summary>
        Private g_Size As Size

        ''' <summary>
        ''' 요소의 투명도를 저장합니다.
        ''' </summary>
        Private g_Opacity As Byte

        ''' <summary>
        ''' 요소의 문자열 색을 저장합니다.
        ''' </summary>
        Private g_ForeColor As Color = Color.Black

        ''' <summary>
        ''' 요소의 글꼴을 저장합니다.
        ''' </summary>
        Private g_Font As Drawing.Font

        ''' <summary>
        ''' 요소의 텍스트 정렬 방식을 저장합니다.
        ''' </summary>
        Private g_TextAlignment As TextAlignment = TextAlignment.VerticalCenter Or TextAlignment.Center

        ''' <summary>
        ''' 글꼴의 높이를 저장합니다.
        ''' </summary>
        Private g_FontHeight As Single

        ''' <summary>
        ''' [Internal Access] 점 문자의 넓이를 저장합니다.
        ''' </summary>
        Private m_DotWidth As Int32

        ''' <summary>
        ''' 포커스가 있는지의 여부를 저장합니다.
        ''' </summary>
        Private g_HaveFocus As Boolean = False

        ''' <summary>
        ''' 컨트롤이 활성화되었는지의 여부를 저장합니다.
        ''' </summary>
        Private g_Enabled As Boolean = True

        ''' <summary>
        ''' 컨트롤의 배경 이미지를 저장합니다.
        ''' </summary>
        Private g_BackgroundImage As Image = Nothing



        ''' <summary>
        ''' 마우스 메세지를 처리할 것인지의 여부를 저장합니다.
        ''' </summary>
        Private g_DoNotProcessMouseMessages As Boolean = False

        ''' <summary>
        ''' 키보드 메세지를 처리할 것인지의 여부를 저장합니다.
        ''' </summary>
        Private g_DoNotProcessKeyboardMessages As Boolean = False

        ''' <summary>
        ''' 마우스 왼쪽 버튼이 눌렸는지 여부를 저장합니다.
        ''' </summary>
        Private m_LMouseDown As Boolean = False

        ''' <summary>
        ''' 마우스 중간 버튼이 눌렸는지 여부를 저장합니다.
        ''' </summary>
        Private m_MMouseDown As Boolean = False

        ''' <summary>
        ''' 마우스 오른쪽 버튼이 눌렸는지 여부를 저장합니다.
        ''' </summary>
        Private m_RMouseDown As Boolean = False

        ''' <summary>
        ''' 마우스를 캡쳐중인지 여부를 저장합니다.
        ''' </summary>
        Private m_Holding As Boolean = False

        ''' <summary>
        ''' 컨트롤 내부에 마우스가 들어왔는지 여부를 저장합니다.
        ''' </summary>
        Private m_MouseEntered As Boolean = False

        ''' <summary>
        ''' DirectX Font 개체를 저장합니다.
        ''' </summary>
        Friend m_Font As D3.Font



        Friend Overridable Sub DrawControl(ByVal Target As D3.Sprite)

            Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), g_Size, g_Location, Color.Black)
            Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), g_Size - New Size(2, 2), g_Location + New Size(1, 1), Color.White)

        End Sub
        Friend Overridable Sub DrawControlText(ByVal TextTarget As D3.Sprite)

            m_Font.DrawText(Nothing, g_Text, New Rectangle(g_Location, g_Size), g_TextAlignment, g_ForeColor)

        End Sub

        Private Sub ProcessMessageInternal(ByRef m As Windows.Forms.Message, ByRef ProcessAsDefault As Boolean, ByRef Handled As Boolean)

            ' 비활성화 컨트롤에 대해선, 메세지를 통지받지 않는다.
            If Not Me.Enabled Then Return

            Select Case m.Msg
                Case WinAPI.WindowsMessages.WM_MOUSEMOVE
                    If g_DoNotProcessMouseMessages Then GoTo PassToHandler
                    ProcessMouseMove(m)
                    Handled = True
                    Return

                Case WinAPI.WindowsMessages.WM_LBUTTONDOWN, WinAPI.WindowsMessages.WM_LBUTTONUP, WinAPI.WindowsMessages.WM_LBUTTONDBLCLK
                    If g_DoNotProcessMouseMessages Then GoTo PassToHandler
                    ProcessMouseLeftButton(m)
                    Handled = True
                    Return

                Case WinAPI.WindowsMessages.WM_MBUTTONDOWN, WinAPI.WindowsMessages.WM_MBUTTONUP, WinAPI.WindowsMessages.WM_MBUTTONDBLCLK
                    If g_DoNotProcessMouseMessages Then GoTo PassToHandler
                    ProcessMouseMiddleButton(m)
                    Handled = True
                    Return

                Case WinAPI.WindowsMessages.WM_RBUTTONDOWN, WinAPI.WindowsMessages.WM_RBUTTONUP, WinAPI.WindowsMessages.WM_RBUTTONDBLCLK
                    If g_DoNotProcessMouseMessages Then GoTo PassToHandler
                    ProcessMouseRightButton(m)
                    Handled = True
                    Return

                Case WinAPI.WindowsMessages.WM_KEYDOWN, WinAPI.WindowsMessages.WM_KEYUP
                    If g_DoNotProcessKeyboardMessages Then GoTo PassToHandler
                    If Not g_HaveFocus Then Return
                    ProcessKeyboardEvents(m)
                    Handled = True
                    Return

            End Select

PassToHandler:
            ProcessMessage(m, ProcessAsDefault, Handled)

        End Sub
        Protected Overridable Sub ProcessMessage(ByRef m As Windows.Forms.Message, ByRef ProcessAsDefault As Boolean, ByRef Handled As Boolean)
        End Sub

    End Class

End Namespace