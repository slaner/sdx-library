Imports System.Drawing
Namespace Controls
    Partial Class SDXTextBox

        Protected Friend Overrides Sub DrawControl(ByVal Target As Microsoft.DirectX.Direct3D.Sprite)

            ' 경계선을 그린다.
            Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), Me.Size, Me.Location, Color.Black)

            ' 컨트롤 내부를 그린다.
            MyBase.DrawControl(Target)

            If Me.Focused AndAlso m_ShowCaret Then

                ' 캐럿 그리기
                Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Drawing.Rectangle(0, 0, 1, 1), New Drawing.Size(2, MyBase.FontHeight),
                              GetCaretPos,
                              Drawing.Color.FromArgb(255 * Math.Cos(SDXHelper.DegToRad(m_CaretTick * m_CaretFadeStep)), Drawing.Color.Black))

            End If

            ' 선택 영역이 있는 경우엔
            ' 선택 영역을 렌더링한다.
            If HaveSelection() Then

                Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Drawing.Rectangle(0, 0, 1, 1), GetSelectionSize(), GetSelectionPos(), Drawing.Color.FromArgb(128, Drawing.Color.Black))

            End If

        End Sub

        Protected Friend Overrides Sub DrawControlText(ByVal TextTarget As Microsoft.DirectX.Direct3D.Sprite)

            Dim tmpText As String = m_Buffer.ToString()
            If tmpText.Length > m_iMaxDisplayableCharacters Then
                If tmpText.Length - m_ScrollLocation.X > m_iMaxDisplayableCharacters Then
                    tmpText = tmpText.Substring(m_ScrollLocation.X, m_iMaxDisplayableCharacters)
                Else
                    tmpText = tmpText.Substring(m_ScrollLocation.X)
                End If
            Else
                tmpText = tmpText.Substring(m_ScrollLocation.X)
            End If

            Dim iCount As Int32 = 1
            Do While SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, tmpText) > Me.Width
                m_ScrollLocation.X += 1
                tmpText = tmpText.Substring(iCount)
                iCount += 1
            Loop

            m_Font.DrawText(TextTarget, tmpText, New Drawing.Rectangle(Me.Location, Me.Size), Me.TextAlign, Me.ForeColor)
            m_CaretTick += 1

            If m_CaretTick >= g_CaretTick Then
                m_CaretTick = 0
                m_ShowCaret = Not m_ShowCaret
            End If

        End Sub
    End Class
End Namespace