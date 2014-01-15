Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text
Namespace Controls
    Partial Class SDXTextBox

        ''' <summary>
        ''' DELETE 키에 대한 작업을 구현합니다.
        ''' </summary>
        Private Sub ProcessDelete()

            ' 읽기 전용일 경우, 지우기 작업을 하지 않는다.
            If Me.ReadOnly Then Return

            Dim hasChanges As Boolean = False

            ' 선택 문자열이 있는 경우:
            If HaveSelection() Then
                m_Buffer = DeleteSelectionText()
                hasChanges = True
            Else

                ' 선택 문자열은 없고, 캐럿의 위치가 문자열의 길이보다 작을 경우
                If m_iCaretPosition < m_Buffer.Length Then
                    m_Buffer = RemoveSafe(m_Buffer, m_iCaretPosition, 1)
                    hasChanges = True
                End If

            End If

            ' 변경 사항이 있는 경우에만 버퍼/스크롤 텍스트를 업데이트한다.
            If hasChanges Then
                UpdateBufferText()
                UpdateScrollText()
            End If

        End Sub

        ''' <summary>
        ''' 문자의 위치를 가져옵니다.
        ''' </summary>
        Private Function GetCharPos(ByVal Pos As Int32) As Point

            If Pos < 0 Or Pos > m_Buffer.Length Then

                SDXHelper.SDXTrace("GetCharPos Error! (Invalid Position)")
                Return Point.Empty

            End If

        End Function

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

                ' 컨트롤의 텍스트를 업데이트한다.
                Me.Text = m_BufferText

            End If

        End Sub

        ''' <summary>
        ''' 안전한 문자열 제거 함수입니다.
        ''' </summary>
        Private Function RemoveSafe(ByVal sb As System.Text.StringBuilder, ByVal Index As Int32, ByVal Length As Int32) As System.Text.StringBuilder

            If Length > sb.Length Then Return sb
            If Index >= sb.Length Then Return sb
            If Index + Length > sb.Length Then Return sb
            Return sb.Remove(Index, Length)

        End Function

        ''' <summary>
        ''' 스크롤 텍스트를 업데이트합니다.
        ''' </summary>
        Private Sub UpdateScrollText(Optional ByVal RemoveFromRear As Boolean = False)

            ' 텍스트 대입
            m_ScrollText = m_BufferText

            ' A = 표시 가능한 최대 문자의 길이
            ' 텍스트박스 내용의 길이가 A보다 큰 경우,
            If m_ScrollText.Length > m_iMaxDisplayableCharacters Then

                '표시 텍스트의 길이에서 X 좌표의 스크롤 위치를 뺀 값이 A보다 큰 경우,
                If m_ScrollText.Length - m_ScrollPoint.X > m_iMaxDisplayableCharacters Then

                    ' X 좌표 스크롤 위치에서 A만큼 부분 문자열을 잘라낸다.
                    m_ScrollText = m_ScrollText.Substring(m_ScrollPoint.X, m_iMaxDisplayableCharacters)

                    ' 그 외의 경우,
                Else

                    ' 굳이 A만큼 잘라낼 필요가 없으므로, 
                    ' X 좌표 스크롤 위치부터 끝까지 부분 문자열을 잘라낸다.
                    m_ScrollText = m_ScrollText.Substring(m_ScrollPoint.X)

                End If

                ' 그 외의 경우,
            Else

                ' X 좌표 스크롤 위치부터 끝까지 부분 문자열을 잘라낸다.
                m_ScrollText = m_ScrollText.Substring(m_ScrollPoint.X)

            End If

            ' 잘린 텍스트의 크기가 컨트롤의 크기보다 클 때만 자르기를 한다.
            Do While SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_ScrollText) > Me.Width

                If RemoveFromRear Then
                    m_ScrollText = m_ScrollText.Substring(0, m_ScrollText.Length - 1)
                Else
                    m_ScrollText = m_ScrollText.Substring(1)
                End If

            Loop

            m_iTextRange = m_ScrollText.Length

        End Sub

        ''' <summary>
        ''' 캐럿을 이동시킵니다.
        ''' </summary>
        Private Sub MoveCaretEx(ByVal Origin As Int32, ByVal Destination As Int32, Optional ByVal GoingLeft As Boolean = False, Optional ByVal Shifted As Boolean = False)

            ' 예외를 방지하기 위한 유효성 검사:
            If String.IsNullOrEmpty(m_BufferText) Then

                SDXHelper.SDXTrace("MoveCaretEx Error! (Empty String)")
                Return

            End If
            If Origin < 0 OrElse Origin > m_BufferText.Length OrElse Destination < 0 OrElse Destination > m_BufferText.Length Then

                SDXHelper.SDXTrace("MoveCaretEx Error! (Invalid Position)")
                Return

            End If
            If Origin = Destination Then

                SDXHelper.SDXTrace("MoveCaretEx Warning! (Origin and Destination is equal)")
                Return

            End If

            ' 쉬프트키가 눌린 경우:
            If Shifted Then

                Debug.Print("SHIFT: {0} / LEFT: {1} / LEFTSELECT: {2}", Shifted, GoingLeft, m_bSelectLeft)

                If GoingLeft Then

                    If m_bSelectLeft Then

                        ' 선택 영역을 1 감소시키고, 길이를 1 증가시킨다.
                        SetSelection(g_Selection.Start - 1, g_Selection.Length + 1)
                        m_iCaretPosition = g_Selection.Start

                    Else

                        ' 선택 영역의 길이가 0일 경우, (위의 m_bGoingLeft 값이 True일 때와 동일하게 처리함)
                        If g_Selection.Length = 0 Then

                            ' 왼쪽으로 이동하므로, m_bGoingLeft 변수의 값을 True로 한다.
                            m_bSelectLeft = True

                            ' 선택 영역을 1 감소시키고, 길이를 1 증가시킨다.
                            SetSelection(g_Selection.Start - 1, g_Selection.Length + 1)
                            m_iCaretPosition = g_Selection.Start

                        Else

                            ' 선택 영역의 길이가 0이 아닐 경우, 길이를 1 감소시킨다.
                            g_Selection.Length -= 1
                            m_iCaretPosition = g_Selection.Start + g_Selection.Length

                        End If

                    End If

                Else

                    ' 왼쪽으로 이동하고 있는 경우
                    If m_bSelectLeft Then

                        ' 선택 영역을 1 증가시키고, 길이를 1 감소시킨다.
                        SetSelection(g_Selection.Start + 1, g_Selection.Length - 1)

                        ' 선택 영역의 길이가 0일 경우,
                        If g_Selection.Length = 0 Then

                            ' 왼쪽으로 이동하지 않으므로, m_bGoingLeft 변수의 값을 False로 하고,
                            ' 함수의 실행을 종료한다.
                            m_bSelectLeft = False

                        End If
                        m_iCaretPosition = g_Selection.Start

                    Else

                        ' 선택 영역의 길이를 1 증가시킨다.
                        SetSelection(g_Selection.Start, g_Selection.Length + 1)
                        m_iCaretPosition = g_Selection.Start + g_Selection.Length

                    End If

                End If

            Else

                m_iCaretPosition = Destination
                g_Selection.Start = Destination
                g_Selection.Length = 0
                m_bSelectLeft = False

            End If

            If Destination > Origin Then

                If m_iCaretPosition > m_ScrollPoint.X + m_iTextRange Then

                    m_ScrollPoint.X += m_iCaretPosition - (m_ScrollPoint.X + m_iTextRange)
                    UpdateScrollText(True)

                End If

            Else

                If m_iCaretPosition < m_ScrollPoint.X Then

                    m_ScrollPoint.X = m_iCaretPosition
                    UpdateScrollText(True)

                End If

            End If

            EnsureCaretVisible()

        End Sub

        ''' <summary>
        ''' + 문자를 추가합니다.
        ''' </summary>
        Private Sub ApplyInput(ByVal [Char] As Char)

            ' 읽기 전용일 경우, 작업을 하지 않는다.
            If Me.ReadOnly Then Return

            ' 선택 문자열이 있는지 확인한다.
            If HaveSelection() Then

                ' 선택 문자열을 지운다.
                m_Buffer = DeleteSelectionText()
                UpdateBufferText()

            End If

            ' 삽입 모드인 경우, 오른쪽에 있는 문자를 지운다
            If g_InsertMode Then
                If g_Selection.Start < m_Buffer.Length Then m_Buffer = m_Buffer.Remove(g_Selection.Start, 1)
            End If

            ' 문자를 삽입한다.
            m_Buffer.Insert(g_Selection.Start, [Char])
            g_Selection.Start += 1
            m_iCaretPosition += 1

            ' 버퍼 텍스트/스크롤 텍스트를 업데이트한다.
            UpdateBufferText()
            UpdateScrollText()

            ' 캐럿 이동
            MoveCaretEx(g_Selection.Start - 1, g_Selection.Start)

            ' 뭔지 알죠? 캐럿을 보이게 합시다.
            EnsureCaretVisible()

        End Sub

        ''' <summary> 
        ''' + 선택 문자열을 제거한 문자열을 가져옵니다.
        ''' </summary>
        Private Function DeleteSelectionText() As System.Text.StringBuilder

            If g_Selection.Length = 0 Then Return m_Buffer
            If g_Selection.Start >= m_Buffer.Length Then Return m_Buffer
            If g_Selection.Start + g_Selection.Length > m_Buffer.Length Then Return m_Buffer
            Dim tSL As Int32 = g_Selection.Length,
                tSS As Int32 = g_Selection.Start

            If g_Selection.Start < m_iCaretPosition Then

                MoveCaretEx(m_iCaretPosition, g_Selection.Start)
                g_Selection.Start = g_Selection.Length - (g_Selection.Length - g_Selection.Start)

            End If
            g_Selection.Length = 0
            Return RemoveSafe(m_Buffer, tSS, tSL)

        End Function

        ''' <summary>
        ''' + 선택 문자열이 있는지 확인합니다.
        ''' </summary>
        Private Function HaveSelection() As Boolean

            If g_Selection.Length = 0 Then Return False
            If g_Selection.Start + g_Selection.Length > m_Buffer.Length Then Return False
            Return True

        End Function

        ''' <summary>
        ''' + 선택 문자열을 가져옵니다.
        ''' </summary>
        Private Function GetSelectionText() As String

            If g_Selection.Length = 0 Then Return Nothing
            If g_Selection.Start >= m_Buffer.Length Then Return Nothing
            If g_Selection.Start + g_Selection.Length >= m_Buffer.Length Then Return Nothing

            Return m_BufferText.Substring(g_Selection.Start, g_Selection.Length)

        End Function

        ''' <summary>
        ''' + 선택 영역을 설정합니다.
        ''' </summary>
        Private Sub SetSelection(ByVal Start As Int32, ByVal Length As Int32)

            If Start < 0 Or
               Start + Length > m_Buffer.Length Or
               Length < 0 Then
                SDXHelper.SDXTrace("SetSelection Error! (Invalid Length)")
                Return
            End If

            g_Selection.Start = Start
            g_Selection.Length = Length

        End Sub

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

            ' 스크롤 텍스트를 업데이트한다.
            UpdateScrollText()

        End Sub

        ''' <summary>
        ''' + 클립보드의 텍스트를 붙여넣기 합니다.
        ''' </summary>
        Private Sub PasteClipboardText()

            ' 읽기 전용일 경우, 작업을 하지 않는다.
            If Me.ReadOnly Then Return

            If HaveSelection() Then
                m_Buffer = DeleteSelectionText()
                UpdateBufferText()
            End If

            Dim cbText As String = Clipboard.GetText()
            m_Buffer = m_Buffer.Insert(g_Selection.Start, cbText)
            UpdateBufferText()
            SetSelection(g_Selection.Start + cbText.Length, 0)
            m_iCaretPosition = g_Selection.Start

        End Sub

        ''' <summary>
        ''' + 선택 문자열을 클립보드에 복사합니다.
        ''' </summary>
        Private Function CopySelectionText() As Boolean

            If g_Selection.Length = 0 Then Return False

            Clipboard.Clear()
            Clipboard.SetText(m_BufferText.Substring(g_Selection.Start, g_Selection.Length))
            Return True

        End Function

        ''' <summary>
        ''' + 텍스트를 모두 선택합니다. 
        ''' </summary>
        Private Sub SelectAllText()

            SetSelection(0, m_Buffer.Length)
            m_iCaretPosition = m_Buffer.Length

        End Sub

        ''' <summary>
        ''' + 캐럿의 위치를 가져옵니다.
        ''' </summary>
        Private Function GetCaretPos() As Point

            If m_Buffer.Length = 0 Then Return New Point(Me.Left, Me.Top)
            Dim CPos As New Point(Me.Left, Me.Top)

            If m_ScrollPoint.X > 0 Then CPos.X -= SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_BufferText.Substring(0, m_ScrollPoint.X))
            CPos.X += SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_BufferText.Substring(0, m_iCaretPosition))

            Return CPos

        End Function

        ''' <summary>
        ''' + 선택 영역의 위치를 가져옵니다.
        ''' </summary>
        Private Function GetSelectionPos() As Point

            If m_Buffer.Length = 0 Then Return New Point(Me.Left, Me.Top)
            Dim CPos As New Point(Me.Left, Me.Top)

            If m_ScrollPoint.X > 0 Then CPos.X -= SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_BufferText.Substring(0, m_ScrollPoint.X))
            CPos.X += SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_BufferText.Substring(0, g_Selection.Start))

            Return CPos

        End Function

        ''' <summary>
        ''' + 선택 영역의 크기를 가져옵니다.
        ''' </summary>
        Private Function GetSelectionSize() As Size

            If g_Selection.Length = 0 Then Return Size.Empty
            Return New Size(
                            SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_BufferText.Substring(g_Selection.Start, g_Selection.Length)),
                            MyBase.FontHeight)

        End Function

        ''' <summary>
        ''' + 캐럿을 표시하게 합니다.
        ''' </summary>
        Private Sub EnsureCaretVisible()

            m_CaretTick = 0
            m_ShowCaret = True

        End Sub

    End Class
End Namespace