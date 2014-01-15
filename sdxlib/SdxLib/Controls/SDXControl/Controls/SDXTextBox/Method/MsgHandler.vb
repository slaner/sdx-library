Imports SDXLib.WinAPI
Imports System.Text
Imports System.Windows.Forms

Namespace Controls
    Partial Class SDXTextBox

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
        ''' WM_IME_COMPOSITION 메세지를 처리합니다.
        ''' </summary>
        Private Function ProcessImeComposition(ByVal m As Windows.Forms.Message) As Boolean

            ' 조합중인 문자를 완성한 경우:
            If m.LParam.ToInt32 And ImeFlags.GCS_RESULTSTR Then

                ' 선택 영역이 있는 경우:
                If HaveSelection() Then

                    ' 선택 영역을 지운다.
                    m_Buffer = DeleteSelectionText()

                End If

            End If

            ' 조합 문자:
            If m.LParam.ToInt32 And ImeFlags.GCS_COMPSTR Then

                ' IME 컨텍스트 포인터를 가져온다.
                Dim hImc As IntPtr = ImmGetContext(MyBase.Main.Window.Handle)

                ' 조합중인 문자의 길이를 가져온다.
                Dim cpsLen As Int32 = ImmGetCompositionString(hImc, ImeFlags.GCS_COMPSTR, Nothing, 0)

                ' 조합중인 문자를 가져오기 위해 바이트배열을 선언한다.
                Dim strBytes(cpsLen - 1) As Byte

                ' 조합중인 문자를 가져온다.
                ImmGetCompositionString(hImc, ImeFlags.GCS_COMPSTR, strBytes, cpsLen)

                ' 문자를 조합중인 경우
                If m_bComposing Then

                    ' 이전에 조합중이던 문자를 지운다.
                    m_Buffer = RemoveSafe(m_Buffer, m_iCaretPosition, 1)

                    ' 버퍼 텍스트를 업데이트한다.
                    UpdateBufferText()

                End If

                ' 조합중인 문자의 길이가 0일 경우 (조합 취소)
                If cpsLen = 0 Then
                    m_bComposing = False
                Else
                    m_bComposing = True
                End If

                ' 조합중인 문자를 추가한다.
                m_Buffer.Insert(m_iCaretPosition, Trim(Encoding.Unicode.GetString(strBytes, 0, cpsLen)))

                ' 버퍼 텍스트를 업데이트한다.
                UpdateBufferText()

                ' 사용한 IME 컨텍스트를 해제한다.
                ImmReleaseContext(MyBase.Main.Window.Handle, hImc)

                ' 문자를 조합중일땐 캐럿을 보이지 않게 해야 하므로,
                ' 캐럿을 숨긴다.
                m_HideCaret = True

                ' 스크롤 텍스트를 업데이트한다.
                UpdateScrollText()

                ' COMPSTR 플래그가 설정된 메세지에 대해서만 메세지를 처리한다.
                Return True

            End If

            Return False

        End Function

        ''' <summary>
        ''' WM_KEYDOWN 메세지를 처리합니다.
        ''' </summary>
        Private Sub ProcessWmKeyDownMsg(ByVal m As Windows.Forms.Message)
            Select Case m.WParam
                Case VirtualKeys.VK_LEFT
                    MoveCaretEx(m_iCaretPosition, m_iCaretPosition - 1, True, m_bShifted)

                Case VirtualKeys.VK_RIGHT
                    MoveCaretEx(m_iCaretPosition, m_iCaretPosition + 1, , m_bShifted)

                Case VirtualKeys.VK_HOME
                    MoveCaretEx(m_iCaretPosition, 0, True, m_bShifted)

                Case VirtualKeys.VK_END
                    MoveCaretEx(m_iCaretPosition, m_BufferText.Length, False, m_bShifted)

                Case VirtualKeys.VK_DELETE
                    ProcessDelete()

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

            ' 문자를 조합중인 경우,
            If m_bComposing Then

                ' 마지막 문자(조합중인 문자)를 지운다.
                m_Buffer = RemoveSafe(m_Buffer, m_iCaretPosition, 1)

            End If

            ' 문자를 추가한다.
            ApplyInput(ChrW(m.WParam))
            m_bComposing = False

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

                    ' 읽기 전용이 아닐 경우에만 메세지를 처리한다.
                    If Not Me.ReadOnly Then
                        ProcessWmCharMsg(m)
                        Handled = True
                    End If

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

                    ' 읽기 전용이 아닐 경우에만 메세지를 처리한다.
                    If Not Me.ReadOnly Then
                        ProcessImeChar(m)
                        Handled = True
                    End If

                Case WindowsMessages.WM_IME_COMPOSITION
                    SDXHelper.SDXTrace("WM_IME_COMPOSITION")

                    ' 읽기 전용이 아닐 경우에만 메세지를 처리한다.
                    If Not Me.ReadOnly Then
                        If ProcessImeComposition(m) Then
                            Handled = True
                        Else
                            ProcessAsDefault = False
                        End If
                    End If

            End Select

        End Sub

    End Class
End Namespace