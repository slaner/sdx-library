Imports System.Drawing
Namespace Controls
    Partial Class SDXTextBox

        Friend Overrides Sub DrawControl(ByVal Target As Microsoft.DirectX.Direct3D.Sprite)

            Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), Me.Size + New Size(6, 6), Me.Location - New Size(3, 3), Color.Black)
            Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), Me.Size + New Size(4, 4), Me.Location - New Size(2, 2), Color.White)


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

        Friend Overrides Sub DrawControlText(ByVal TextTarget As Microsoft.DirectX.Direct3D.Sprite)

            Me.Text = m_Buffer.ToString()
            m_Font.DrawText(TextTarget, Me.Text, New Drawing.Rectangle(Me.Location, Me.Size), Me.TextAlign, Me.ForeColor)
            m_CaretTick += 1

            If m_CaretTick >= g_CaretTick Then
                m_CaretTick = 0
                m_ShowCaret = Not m_ShowCaret
            End If

        End Sub
    End Class
End Namespace