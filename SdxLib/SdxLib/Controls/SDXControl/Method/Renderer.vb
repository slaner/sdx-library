Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Namespace Controls
    Partial Class SDXControl

        ''' <summary>
        ''' 컨트롤을 그립니다.
        ''' </summary>
        ''' <param name="Target">컨트롤이 그려질 대상 스프라이트를 입력합니다.</param>
        Protected Friend Overridable Sub DrawControl(ByVal Target As D3.Sprite)

            ' 배경 이미지가 지정되지 않은 경우,
            ' 배경 색을 이용해 배경을 렌더링하고
            ' 배경 이미지가 지정된 경우
            ' 배경 색을 렌더링한 후에 배경 이미지를 렌더링한다.
            If g_BackgroundImage Is Nothing Then
                Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), g_Size, g_Location, Color.FromArgb(g_Opacity, g_BackColor))
            Else
                Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), g_Size, g_Location, Color.FromArgb(g_Opacity, g_BackColor))
                Select Case g_BackgroundImageLayout
                    Case ImageLayout.Default
                        Target.Draw2D(m_BackgroundImage, New Rectangle(Point.Empty, g_BackgroundImage.Size), g_BackgroundImage.Size, Point.Empty, Color.FromArgb(g_Opacity, Color.White))

                    Case ImageLayout.Stretch
                        Target.Draw2D(m_BackgroundImage, New Rectangle(Point.Empty, g_BackgroundImage.Size), g_Size, Point.Empty, Color.FromArgb(g_Opacity, Color.White))

                    Case ImageLayout.Center
                        Dim posX As Int32 = 0,
                            posY As Int32 = 0

                        If g_Size.Width > g_BackgroundImage.Width Then posX = g_Size.Width - g_BackgroundImage.Width / 2
                        If g_Size.Height > g_BackgroundImage.Height Then posY = g_Size.Height - g_BackgroundImage.Height / 2
                        Target.Draw2D(m_BackgroundImage, New Rectangle(Point.Empty, g_BackgroundImage.Size), g_BackgroundImage.Size, New Point(posX, posY), Color.FromArgb(g_Opacity, Color.White))

                End Select
            End If

        End Sub

        ''' <summary>
        ''' 컨트롤의 텍스트를 그립니다.
        ''' </summary>
        ''' <param name="TextTarget">컨트롤의 텍스트를 그릴 대상 스프라이트를 입력합니다.</param>
        Protected Friend Overridable Sub DrawControlText(ByVal TextTarget As D3.Sprite)

            m_Font.DrawText(Nothing, g_Text, New Rectangle(g_Location, g_Size), g_TextAlignment, Color.FromArgb(g_Opacity, g_ForeColor))

        End Sub

    End Class
End Namespace