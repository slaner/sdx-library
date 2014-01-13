Imports System.Drawing
Imports System.Windows.Forms
Namespace Controls
    Partial Class SDXTextBox

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
        Private Sub MoveCaretEx(ByVal Origin As Int32, ByVal Destination As Int32, Optional ByVal Length As Int32 = 0)

            ' 예외를 방지하기 위한 유효성 검사:
            If String.IsNullOrEmpty(m_BufferText) Then

                SDXHelper.SDXTrace("MoveCaretEx Error! (Empty String)")
                Return

            End If
            If Origin < 0 OrElse Origin > m_BufferText.Length OrElse Destination < 0 OrElse Destination > m_BufferText.Length OrElse Length < 0 OrElse Destination + Length > m_BufferText.Length Then

                SDXHelper.SDXTrace("MoveCaretEx Error! (Invalid Position/Length)")
                Return

            End If

            If Origin = Destination Then

                SDXHelper.SDXTrace("MoveCaretEx Warning! (Origin and Destination is equal)")
                Return

            End If

            m_iCaretPosition = Destination
            g_Selection.Start = Destination
            g_Selection.Length = Length
            If Destination > Origin Then

                If g_Selection.Start > m_ScrollPoint.X + m_iTextRange Then

                    m_ScrollPoint.X += g_Selection.Start - (m_ScrollPoint.X + m_iTextRange)
                    UpdateScrollText(True)
                    Debug.Print("SS: {0}/ CP: {1}/ SX: {2}/ TR: {3}/ ST: {4}", g_Selection.Start, m_iCaretPosition, m_ScrollPoint.X, m_iTextRange, m_ScrollText)

                End If

            Else

                If g_Selection.Start < m_ScrollPoint.X Then

                    m_ScrollPoint.X = g_Selection.Start
                    UpdateScrollText(True)

                End If

            End If

        End Sub

        ''' <summary>
        ''' 캐럿을 지정된 위치로 이동시킵니다.
        ''' </summary>
        Private Sub MoveCaret(ByVal Position As Int32)

            If Position < 0 OrElse Position > m_Buffer.Length Then
                SDXHelper.SDXTrace("MoveCaret Error! (Invalid Position)")
                Return
            End If

            m_iCaretPosition = Position

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
                MoveCaret(g_Selection.Start)
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