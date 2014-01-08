Imports System.Drawing
Namespace Controls
    Partial Class SDXControl

        ' 내부 메세지 처리 함수
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

        ''' <summary>
        ''' 메세지를 처리합니다.
        ''' </summary>
        ''' <param name="m">받은 메세지를 입력합니다.</param>
        ''' <param name="ProcessAsDefault">이 메세지를 WndProc 함수로 처리할 것인지 DefWndProc 함수로 처리할 것인지에 대한 여부를 입력합니다. (True의 경우, DefWndProc 함수로 처리합니다.)</param>
        ''' <param name="Handled">이 메세지에 대해 추가적인 처리를 원치 않는 경우, 이 값을 True로 입력합니다.</param>
        Protected Overridable Sub ProcessMessage(ByRef m As Windows.Forms.Message, ByRef ProcessAsDefault As Boolean, ByRef Handled As Boolean)
        End Sub

        ''' <summary>
        ''' MouseLeave 이벤트를 발생시킵니다.
        ''' </summary>
        Private Sub OnMouseLeaveInternal()

            m_MouseEntered = False
            RaiseEvent MouseLeave()

        End Sub

        ''' <summary>
        ''' 마우스 이동에 대한 이벤트를 처리합니다.
        ''' </summary>
        Private Sub ProcessMouseMove(ByVal m As Windows.Forms.Message)

            Dim p As Point = WinAPI.WinMacro.MAKEPOINT(m.LParam)

            If Me.Bounds.Contains(p) Then

                If Not m_MouseEntered Then
                    RaiseEvent MouseEnter()
                    m_MouseEntered = True
                End If

                Dim btnFlags As Int32 = 0
                btnFlags += If(m_LMouseDown, MouseButton.Left, 0)
                btnFlags += If(m_MMouseDown, MouseButton.Middle, 0)
                btnFlags += If(m_RMouseDown, MouseButton.Right, 0)
                RaiseEvent MouseMove(btnFlags, p)

            Else

                If m_MouseEntered Then
                    If m_Holding Then
                        Dim btnFlags As Int32 = 0
                        btnFlags += If(m_LMouseDown, MouseButton.Left, 0)
                        btnFlags += If(m_MMouseDown, MouseButton.Middle, 0)
                        btnFlags += If(m_RMouseDown, MouseButton.Right, 0)
                        RaiseEvent MouseMove(btnFlags, p)
                    Else
                        RaiseEvent MouseLeave()
                        m_MouseEntered = False
                    End If
                End If

            End If

        End Sub

        ''' <summary> 
        ''' 마우스 왼쪽 버튼에 대한 이벤트를 처리합니다.
        ''' </summary>
        Private Sub ProcessMouseLeftButton(ByVal m As Windows.Forms.Message)

            Dim p As Point = WinAPI.WinMacro.MAKEPOINT(m.LParam)
            Select Case m.Msg
                Case WinAPI.WindowsMessages.WM_LBUTTONDOWN
                    If Me.Bounds.Contains(p) Then
                        If Not g_HaveFocus Then
                            g_HaveFocus = True
                            RaiseEvent GotFocus()
                        End If
                        m_LMouseDown = True
                        m_Holding = True
                        RaiseEvent MouseDown(MouseButton.Left, p)
                    End If

                Case WinAPI.WindowsMessages.WM_LBUTTONUP
                    If Me.Bounds.Contains(p) AndAlso m_LMouseDown Then
                        RaiseEvent Click()
                        RaiseEvent MouseClick(MouseButton.Left, p)
                        RaiseEvent MouseUp(MouseButton.Left, p)
                        m_LMouseDown = False
                        m_Holding = False
                    Else
                        If m_LMouseDown Then
                            RaiseEvent MouseUp(MouseButton.Left, p)
                            m_LMouseDown = False
                        End If
                    End If

                Case WinAPI.WindowsMessages.WM_LBUTTONDBLCLK
                    If Me.Bounds.Contains(p) Then
                        RaiseEvent MouseDblClick(MouseButton.Left, p)
                    End If

            End Select

        End Sub

        ''' <summary>
        ''' 마우스 중간 버튼에 대한 이벤트를 처리합니다.
        ''' </summary>
        Private Sub ProcessMouseMiddleButton(ByVal m As Windows.Forms.Message)

            Dim p As Point = WinAPI.WinMacro.MAKEPOINT(m.LParam)
            Select Case m.Msg
                Case WinAPI.WindowsMessages.WM_MBUTTONDOWN
                    If Me.Bounds.Contains(p) Then
                        If Not g_HaveFocus Then
                            g_HaveFocus = True
                            RaiseEvent GotFocus()
                        End If
                        m_MMouseDown = True
                        m_Holding = True
                        RaiseEvent MouseDown(MouseButton.Middle, p)
                    End If

                Case WinAPI.WindowsMessages.WM_MBUTTONUP
                    If Me.Bounds.Contains(p) AndAlso m_MMouseDown Then
                        RaiseEvent Click()
                        RaiseEvent MouseClick(MouseButton.Middle, p)
                        RaiseEvent MouseUp(MouseButton.Middle, p)
                        m_MMouseDown = False
                        m_Holding = False
                    Else
                        If m_MMouseDown Then
                            RaiseEvent MouseUp(MouseButton.Middle, p)
                            m_MMouseDown = False
                        End If
                    End If

                Case WinAPI.WindowsMessages.WM_MBUTTONDBLCLK
                    If Me.Bounds.Contains(p) Then
                        RaiseEvent MouseDblClick(MouseButton.Middle, p)
                    End If

            End Select

        End Sub

        ''' <summary>
        ''' 마우스 오른쪽 버튼에 대한 이벤트를 처리합니다.
        ''' </summary>
        Private Sub ProcessMouseRightButton(ByVal m As Windows.Forms.Message)

            Dim p As Point = WinAPI.WinMacro.MAKEPOINT(m.LParam)
            Select Case m.Msg
                Case WinAPI.WindowsMessages.WM_RBUTTONDOWN
                    If Me.Bounds.Contains(p) Then
                        If Not g_HaveFocus Then
                            g_HaveFocus = True
                            RaiseEvent GotFocus()
                        End If
                        m_RMouseDown = True
                        m_Holding = True
                        RaiseEvent MouseDown(MouseButton.Right, p)
                    End If

                Case WinAPI.WindowsMessages.WM_RBUTTONUP
                    If Me.Bounds.Contains(p) AndAlso m_RMouseDown Then
                        RaiseEvent Click()
                        RaiseEvent MouseClick(MouseButton.Right, p)
                        RaiseEvent MouseUp(MouseButton.Right, p)
                        m_RMouseDown = False
                        m_Holding = False
                    Else
                        If m_MouseEntered Then
                            OnMouseLeaveInternal()
                        End If

                        If m_RMouseDown Then
                            RaiseEvent MouseUp(MouseButton.Right, p)
                            m_RMouseDown = False
                        End If
                    End If

                Case WinAPI.WindowsMessages.WM_RBUTTONDBLCLK
                    If Me.Bounds.Contains(p) Then
                        RaiseEvent MouseDblClick(MouseButton.Right, p)
                    End If

            End Select

        End Sub

        ''' <summary>
        ''' 키보드에 대한 이벤트를 처리합니다.
        ''' </summary>
        Private Sub ProcessKeyboardEvents(ByVal m As Windows.Forms.Message)

            Select Case m.Msg
                Case WinAPI.WindowsMessages.WM_KEYDOWN
                    RaiseEvent KeyDown(m.WParam)

                Case WinAPI.WindowsMessages.WM_KEYUP
                    RaiseEvent KeyUp(m.WParam)

            End Select

        End Sub

    End Class
End Namespace