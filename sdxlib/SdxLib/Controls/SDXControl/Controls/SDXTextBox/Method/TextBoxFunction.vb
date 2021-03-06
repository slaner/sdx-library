﻿Imports System.Drawing
Imports System.Windows.Forms
Namespace Controls
    Partial Class SDXTextBox

        Private Function RemoveSafeMethod(ByVal sb As System.Text.StringBuilder, ByVal Index As Int32, ByVal Length As Int32) As System.Text.StringBuilder

            If Length > sb.Length Then Return sb
            If Index >= sb.Length Then Return sb
            If Index + Length > sb.Length Then Return sb
            Return sb.Remove(Index, Length)

        End Function

        Private Sub ScrollTextBox()

            Dim CPos As Point = GetCaretPos()

            ' 캐럿의 X 좌표 + 스크롤 X 좌표의 값이 컨트롤의 넓이보다 클 경우,
            If CPos.X + m_ScrollLocation.X > Me.Width Then
                m_ScrollLocation.X -= (CPos.X - Me.Width)
            End If

        End Sub

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

            ' 선택 문자열이 있는지 확인한다.
            If HaveSelection() Then

                ' 선택 문자열을 지운다.
                m_Buffer = DeleteSelectionText()

            End If

            ' 삽입 모드인 경우, 오른쪽에 있는 문자를 지운다
            If g_InsertMode Then
                If g_Selection.Start < m_Buffer.Length Then m_Buffer = m_Buffer.Remove(g_Selection.Start, 1)
            End If

            Dim tSS As Int32 = g_Selection.Start
            g_Selection.Start += 1

            ' 내용이 변경됬으므로, TextChanged 이벤트를 발생시킨다.
            m_Buffer.Insert(tSS, [Char])
            RaiseEvent TextChanged()

            m_iCaretPosition += 1
            'ScrollTextBox()
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
            Return RemoveSafeMethod(m_Buffer, tSS, tSL)

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

            Return m_Buffer.ToString().Substring(g_Selection.Start, g_Selection.Length)

        End Function

        ''' <summary>
        ''' + 선택 영역을 설정합니다.
        ''' </summary>
        Private Sub SetSelection(ByVal Start As Int32, ByVal Length As Int32)

            If Start < 0 Or
               Start + Length > m_Buffer.Length Then
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

            If HaveSelection() Then
                m_Buffer = DeleteSelectionText()
            End If

            Dim cbText As String = Clipboard.GetText()
            m_Buffer = m_Buffer.Insert(g_Selection.Start, cbText)
            SetSelection(g_Selection.Start + cbText.Length, 0)
            m_iCaretPosition = g_Selection.Start

        End Sub

        ''' <summary>
        ''' + 선택 문자열을 클립보드에 복사합니다.
        ''' </summary>
        Private Function CopySelectionText() As Boolean

            If g_Selection.Length = 0 Then Return False

            Clipboard.Clear()
            Clipboard.SetText(m_Buffer.ToString().Substring(g_Selection.Start, g_Selection.Length))
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
            CPos.X += SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_Buffer.ToString().Substring(0, m_iCaretPosition))

            Return CPos

        End Function

        ''' <summary>
        ''' + 선택 영역의 위치를 가져옵니다.
        ''' </summary>
        Private Function GetSelectionPos() As Point

            If m_Buffer.Length = 0 Then Return New Point(Me.Left, Me.Top)
            Dim CPos As New Point(Me.Left, Me.Top)

            CPos.X += SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_Buffer.ToString().Substring(0, g_Selection.Start))

            Return CPos

        End Function

        ''' <summary>
        ''' + 선택 영역의 크기를 가져옵니다.
        ''' </summary>
        Private Function GetSelectionSize() As Size

            If g_Selection.Length = 0 Then Return Size.Empty
            Return New Size(
                            SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, m_Buffer.ToString().Substring(g_Selection.Start, g_Selection.Length)),
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