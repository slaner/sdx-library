Imports SDXLib.WinAPI
Imports System.Text
Imports System.Windows.Forms

Namespace Controls
    Partial Class SDXTextBox

        ''' <summary>
        ''' + 백스페이스 키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub ProcessBackspace()

            ' 읽기 전용일 경우, 지우기 작업을 하지 않는다.
            If Me.ReadOnly Then Return

            ' 캐럿의 위치를 확인하고,
            ' 0보다 클 경우, 왼쪽에 문자가 있는 것이므로
            ' 문자를 지우고, 캐럿 위치를 1 감소시킨다.
            If m_Buffer.Length > 0 Then

                ' 선택 영역이 있는 경우,
                If HaveSelection() Then

                    ' 선택 영역을 지운다.
                    m_Buffer = DeleteSelectionText()

                    ' 버퍼 텍스트를 업데이트하고,
                    UpdateBufferText()

                    ' 캐럿을 표시되도록 한다.
                    EnsureCaretVisible()

                Else

                    ' 왼쪽에 지울 수 있는 문자가 있는 경우,
                    If g_Selection.Start > 0 Then

                        ' 캐럿을 이동시키고,
                        MoveCaretEx(g_Selection.Start, g_Selection.Start - 1)

                        ' 문자 하나를 지운다.
                        m_Buffer = m_Buffer.Remove(g_Selection.Start, 1)

                        ' 버퍼 텍스트를 업데이트하고,
                        UpdateBufferText()

                        ' 캐럿을 표시되도록 한다.
                        EnsureCaretVisible()

                    End If

                End If

            End If
            UpdateScrollText()

        End Sub

        ''' <summary>
        ''' + 왼쪽 화살표 방향키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub LeftArrowAction()

            MoveCaret(m_iCaretPosition - 1)

            ' 선택 모드일 경우 (쉬프트키가 눌린 상태)
            If m_bShifted Then

                ' 왼쪽으로 이동하고 있는 경우
                If m_bGoingLeft Then

                    ' 선택 영역이 1 이상부터 시작하고, 선택 영역의 시작 + 길이가 버퍼의 길이 이하인 경우
                    If g_Selection.Start > 0 AndAlso g_Selection.Start + g_Selection.Length <= m_Buffer.Length Then

                        ' 선택 영역을 1 감소시키고, 길이를 1 증가시킨다.
                        SetSelection(g_Selection.Start - 1, g_Selection.Length + 1)

                    End If

                Else

                    ' 선택 영역의 길이가 0일 경우, (위의 m_bGoingLeft 값이 True일 때와 동일하게 처리함)
                    If g_Selection.Length = 0 Then

                        ' 왼쪽으로 이동하므로, m_bGoingLeft 변수의 값을 True로 한다.
                        m_bGoingLeft = True

                        ' 선택 영역이 1 이상부터 시작하고, 선택 영역의 시작 + 길이가 버퍼의 길이 이하인 경우
                        If g_Selection.Start > 0 AndAlso g_Selection.Start + g_Selection.Length <= m_Buffer.Length Then

                            ' 선택 영역을 1 감소시키고, 길이를 1 증가시킨다.
                            SetSelection(g_Selection.Start - 1, g_Selection.Length + 1)

                        End If
                        Return

                    End If

                    ' 선택 영역의 길이가 0이 아닐 경우, 길이를 1 감소시킨다.
                    g_Selection.Length -= 1

                End If

            Else

                ' 선택 영역을 초기화한다.
                SetSelection(m_iCaretPosition, 0)
                g_Selection.Length = 0
                m_bGoingLeft = False

            End If

        End Sub

        ''' <summary>
        ''' + 오른쪽 화살표 방향키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub RightArrowAction()

            MoveCaret(m_iCaretPosition + 1)

            ' 선택 모드일 경우 (쉬프트키가 눌린 상태)
            If m_bShifted Then

                ' 왼쪽으로 이동하고 있는 경우
                If m_bGoingLeft Then

                    ' 선택 영역이 1 이상부터 시작하고, 선택 영역의 시작 + 길이가 버퍼의 길이 이하인 경우
                    If g_Selection.Start >= 0 AndAlso g_Selection.Start + g_Selection.Length <= m_Buffer.Length Then

                        ' 선택 영역을 1 증가시키고, 길이를 1 감소시킨다.
                        SetSelection(g_Selection.Start + 1, g_Selection.Length - 1)

                    End If

                    ' 선택 영역의 길이가 0일 경우,
                    If g_Selection.Length = 0 Then

                        ' 왼쪽으로 이동하지 않으므로, m_bGoingLeft 변수의 값을 False로 하고,
                        ' 함수의 실행을 종료한다.
                        m_bGoingLeft = False
                        Return

                    End If

                Else

                    ' 선택 영역의 길이를 1 증가시킨다.
                    SetSelection(g_Selection.Start, g_Selection.Length + 1)

                End If

            Else
                
                ' 선택 영역을 초기화한다.
                SetSelection(m_iCaretPosition, 0)
                g_Selection.Length = 0
                m_bGoingLeft = False

            End If

        End Sub

        ''' <summary>
        ''' + Home 키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub HomeAction()

            MoveCaret(0)
            m_ScrollPoint.X = 0
            If m_bShifted Then
                If g_Selection.Start = 0 Then
                    m_bGoingLeft = False
                Else
                    m_bGoingLeft = True
                End If
                SetSelection(0, g_Selection.Start)
            Else
                m_bGoingLeft = False
                SetSelection(0, 0)
            End If

        End Sub

        ''' <summary>
        ''' + End 키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub EndAction()

            MoveCaret(m_Buffer.Length)

            If m_bShifted Then
                If m_bGoingLeft Then
                    SetSelection(g_Selection.Start + g_Selection.Length, m_Buffer.Length - (g_Selection.Start + g_Selection.Length))
                    m_bGoingLeft = False
                Else
                    SetSelection(g_Selection.Start, m_Buffer.Length - g_Selection.Start)
                End If
            Else
                m_bGoingLeft = False
                SetSelection(m_Buffer.Length, 0)
            End If

        End Sub

        ''' <summary>
        ''' + Delete 키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub DeleteAction()

            ' 읽기 전용일 경우, 지우기 작업을 하지 않는다.
            If Me.ReadOnly Then Return

            m_bGoingLeft = False
            If g_Selection.Start < m_Buffer.Length Then
                m_Buffer = m_Buffer.Remove(g_Selection.Start, 1)
            End If

        End Sub

        '' CACHING?

        ''' <summary>
        ''' 버퍼 텍스트를 업데이트합니다.
        ''' </summary>
        Private Sub UpdateBufferText()

            ' 이전 버퍼와 현재 버퍼의 내용이 다를 경우,
            If Not Object.ReferenceEquals(m_Buffer, m_PrevBuffer) Then

                ' 버퍼의 값을 문자열로 변경한다.
                m_BufferText = m_Buffer.ToString()

                ' 이전 버퍼에 현재 버퍼를 대입한다.
                m_PrevBuffer = New StringBuilder(m_Buffer.ToString(), m_Buffer.Capacity)

            End If

        End Sub



        ''' <summary> 
        ''' WM_CHAR 메세지를 처리합니다.
        ''' </summary>
        Private Sub ProcessWmCharMsg(ByVal m As System.Windows.Forms.Message)
            Select Case m.WParam
                Case VirtualKeys.VK_BACK
                    ProcessBackspace()

                Case 1                              ' Ctrl A (전체 선택)
                    SelectAllText()

                Case 3                              ' Ctrl C (복사)
                    CopySelectionText()

                Case 22                             ' Ctrl V (붙여넣기)
                    PasteClipboardText()

                Case 24                             ' Ctrl X (잘라내기)
                    If CopySelectionText() Then
                        m_Buffer = DeleteSelectionText()
                    End If

                    ' 문자로 취급하지 말아야 할 값들 (제어 문자)
                Case 2                              ' Ctrl B
                Case 4                              ' Ctrl D
                Case 6                              ' Ctrl F
                Case 7                              ' Ctrl G
                Case 5                              ' Ctrl E
                Case 9                              ' Ctrl I
                Case 10                             ' Ctrl J
                Case 11                             ' Ctrl K
                Case 12                             ' Ctrl L
                Case 14                             ' Ctrl N
                Case 15                             ' Ctrl O
                Case 16                             ' Ctrl P
                Case 17                             ' Ctrl Q
                Case 18                             ' Ctrl R
                Case 19                             ' Ctrl S
                Case 20                             ' Ctrl T
                Case 21                             ' Ctrl U
                Case 23                             ' Ctrl W
                Case 25                             ' Ctrl Y
                Case 26                             ' Ctrl Z
                Case 27                             ' Ctrl [
                Case 28                             ' Ctrl \
                Case 29                             ' Ctrl ]

                Case Else
                    ApplyInput(ChrW(m.WParam))

            End Select
        End Sub

        ''' <summary>
        ''' WM_KEYDOWN 메세지를 처리합니다.
        ''' </summary>
        Private Sub ProcessWmKeyDownMsg(ByVal m As Windows.Forms.Message)
            Select Case m.WParam
                Case VirtualKeys.VK_LEFT
                    MoveCaretEx(m_iCaretPosition, m_iCaretPosition - 1)

                Case VirtualKeys.VK_RIGHT
                    MoveCaretEx(m_iCaretPosition, m_iCaretPosition + 1)

                Case VirtualKeys.VK_HOME
                    MoveCaretEx(m_iCaretPosition, 0)

                Case VirtualKeys.VK_END
                    MoveCaretEx(0, m_BufferText.Length)

                Case VirtualKeys.VK_DELETE
                    DeleteAction()

                Case VirtualKeys.VK_SHIFT
                    m_bShifted = True

            End Select

        End Sub

        ''' <summary>
        ''' WM_KEYUP 메세지를 처리합니다.
        ''' </summary>
        Private Sub ProcessWmKeyUpMsg(ByVal m As Windows.Forms.Message)
            Select Case m.WParam
                Case VirtualKeys.VK_SHIFT
                    m_bShifted = False

            End Select

        End Sub

        ''' <summary>
        ''' WM_IME_CHAR 메세지를 처리합니다.
        ''' </summary>
        Private Sub ProcessImeChar(ByVal m As Windows.Forms.Message)

            If m_bComposition Then
                m_Buffer = RemoveSafe(m_Buffer, m_iCaretPosition, 1)
            End If

            ApplyInput(ChrW(m.WParam))
            m_bComposition = False

        End Sub

        Protected Overrides Sub ProcessMessage(ByRef m As System.Windows.Forms.Message, ByRef ProcessAsDefault As Boolean, ByRef Handled As Boolean)

            If Not Me.Focused Then
                ProcessAsDefault = True
                Return
            End If

            Select Case m.Msg
                Case WindowsMessages.WM_INPUTLANGCHANGE
                    SDXHelper.SDXTrace("WM_INPUTLANGCHANGE")

                Case WindowsMessages.WM_IME_SETCONTEXT
                    SDXHelper.SDXTrace("WM_IME_SETCONTEXT")

                Case WindowsMessages.WM_CHAR
                    SDXHelper.SDXTrace("WM_CHAR")
                    ProcessWmCharMsg(m)
                    Handled = True

                Case WindowsMessages.WM_INPUTLANGCHANGE
                    SDXHelper.SDXTrace("WM_INPUTLANGCHANGE")

                Case WindowsMessages.WM_KEYDOWN
                    SDXHelper.SDXTrace("WM_KEYDOWN")
                    ProcessWmKeyDownMsg(m)
                    Handled = True

                Case WindowsMessages.WM_KEYUP
                    SDXHelper.SDXTrace("WM_KEYUP")
                    ProcessWmKeyUpMsg(m)
                    Handled = True

                Case WindowsMessages.WM_IME_SETCONTEXT
                    SDXHelper.SDXTrace("WM_IME_SETCONTEXT")
                    Handled = True

                Case WindowsMessages.WM_IME_ENDCOMPOSITION
                    SDXHelper.SDXTrace("WM_IME_ENDCOMPOSITION")
                    m_HideCaret = False
                    Handled = True

                Case WindowsMessages.WM_IME_CHAR
                    SDXHelper.SDXTrace("WM_IME_CHAR")
                    ProcessImeChar(m)
                    Handled = True

                Case WindowsMessages.WM_IME_COMPOSITION
                    SDXHelper.SDXTrace("WM_IME_COMPOSITION")

                    If m.LParam.ToInt32 And ImeFlags.GCS_RESULTSTR Then

                        If HaveSelection() Then
                            Debug.Print("GCS_RESULTSTR and HaveSelection(): True")
                            m_Buffer = DeleteSelectionText()
                        End If

                    End If


                    If m.LParam.ToInt32 And ImeFlags.GCS_COMPSTR Then

                        Dim hImc As IntPtr = ImmGetContext(MyBase.Main.Window.Handle)
                        Dim cpsLen As Int32 = ImmGetCompositionString(hImc, ImeFlags.GCS_COMPSTR, Nothing, 0)
                        Dim strBytes(cpsLen) As Byte
                        ImmGetCompositionString(hImc, ImeFlags.GCS_COMPSTR, strBytes, cpsLen)

                        ' 문자를 조합중이면,
                        If m_bComposition AndAlso m_bComposingChar Then

                            ' 조합된 문자를 지운다.
                            m_Buffer = RemoveSafe(m_Buffer, m_iCaretPosition, 1)
                            UpdateBufferText()
                            m_bComposingChar = False

                        End If

                        If cpsLen = 0 Then
                            m_bComposition = False
                            m_bComposingChar = False
                        Else
                            m_bComposition = True
                            m_bComposingChar = True
                        End If

                        m_Buffer.Insert(m_iCaretPosition, Trim(Encoding.Unicode.GetString(strBytes, 0, cpsLen)))
                        UpdateBufferText()
                        ImmReleaseContext(MyBase.Main.Window.Handle, hImc)
                        m_HideCaret = True
                        Handled = True

                    Else
                        ProcessAsDefault = False
                    End If

                    UpdateScrollText()

            End Select

        End Sub

    End Class
End Namespace