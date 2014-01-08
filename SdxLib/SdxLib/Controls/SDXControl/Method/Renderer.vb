Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Namespace Controls
    Partial Class SDXControl

        ''' <summary>
        ''' 컨트롤을 그립니다.
        ''' </summary>
        ''' <param name="Target">컨트롤이 그려질 대상 스프라이트를 입력합니다.</param>
        Protected Friend Overridable Sub DrawControl(ByVal Target As D3.Sprite)

            Target.Draw2D(MyBase.Main.SharedResource.ColorMask, New Rectangle(0, 0, 1, 1), g_Size, g_Location, Color.White)

        End Sub

        ''' <summary>
        ''' 컨트롤의 텍스트를 그립니다.
        ''' </summary>
        ''' <param name="TextTarget">컨트롤의 텍스트를 그릴 대상 스프라이트를 입력합니다.</param>
        Protected Friend Overridable Sub DrawControlText(ByVal TextTarget As D3.Sprite)

            m_Font.DrawText(Nothing, g_Text, New Rectangle(g_Location, g_Size), g_TextAlignment, g_ForeColor)

        End Sub

    End Class
End Namespace