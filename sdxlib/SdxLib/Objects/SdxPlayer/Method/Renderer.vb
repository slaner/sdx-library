' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxPlayer/Renderer.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   Microsoft.DirectX.DirectInput
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  25
'
' Date:
'   2013/12/10
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxPlayer's method (Rendering).

Imports DI = Microsoft.DirectX.DirectInput
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxPlayer

    Friend Overridable Sub Draw(ByVal Ks As DI.KeyboardState, ByVal Target As D3.Sprite)

        g_Drawn = False

        ' 플레이어 업데이트
        UpdatePlayer(Ks)

        ' 사용이 종료된 개체라면 그리지 않는다.
        If Me.Disposed Then Return

        If g_ChaseCam Then
            MyBase.Main.ViewLocation = New Vector2D((MyBase.Main.Window.ClientSize.Width / 2) - (Me.RealLocation.X - Me.MovingDistanceX),
                                                    (MyBase.Main.Window.ClientSize.Height / 2) - (Me.RealLocation.Y - Me.MovingDistanceY))
        End If

        ' 화면 밖에 있는 경우, 그리지 않기 위해 뷰 로케이션과 플레이어의 위치를 더하고
        ' 식을 적용한다.
        Dim Loc As Vector2D = MyBase.Main.ViewLocation + g_Location
        If Loc.X + Me.Width <= 0 Or Loc.X + Me.Width >= MyBase.Main.Window.Width Then Exit Sub
        If Loc.Y + Me.Height <= 0 Or Loc.Y + Me.Height >= MyBase.Main.Window.Height Then Exit Sub

        Dim c As Drawing.Color = Drawing.Color.FromArgb(g_Opacity * 255, 255, 255, 255)
        Target.Draw2D(Me.Texture, Drawing.Rectangle.Empty, g_Size, Loc, c)

        If DebugRectangle Then
            Target.Draw2D(m_PlayerBox, PointF.Empty, 0, Loc, Drawing.Color.White)
            Target.Draw2D(m_CollideBox, PointF.Empty, 0, Loc + g_CollideBox.Location, Drawing.Color.White)
        End If
        g_Drawn = True

    End Sub

End Class