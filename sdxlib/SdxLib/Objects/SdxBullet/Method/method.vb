' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBullet/method.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  11
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
'   Defines SdxBullet class's method.
Imports System.Drawing
Partial Class SdxBullet

    Private Sub UpdateBullet()

        ' 유효 시간이 다된 경우 삭제한다.
        If Environment.TickCount >= m_CreationTime + SDXLib.SdxBullet.LifeTime Then Me.Dispose()

        ' 사용이 종료된 개체라면 계산하지 않는다.
        If Me.Disposed Then Return

        ' 총알의 위치를 속도만큼 추가한다.
        g_Location.X += g_Speed.X
        g_Location.Y += g_Speed.Y

        ' 블록과 충돌이 있는지 확인한다.
        For Each b As SdxBlock2D In MyBase.Main.Blocks
            If Me.Rectangle.IntersectsWith(b.Rectangle) Then
                If b.Flags And SdxBlock2D.BlockStates.Wall Then
                    Dim k As Drawing.RectangleF? = Drawing.RectangleF.Intersect(Me.Rectangle, b.Rectangle)
                End If
                Me.Dispose()
                Exit Sub
            End If
        Next

        ' 플레이어와 충돌이 있는지 확인한다.
        For Each p As SdxPlayer In MyBase.Main.Players

            ' 자신의 총알은 맞지 않게
            If Me.Owner Is p Then Continue For

            If Me.Rectangle.IntersectsWith(p.Rectangle) Then

                ' 픽셀단위 충돌검사를 실행한다.
                If SDXHelper.PixelIntersects(Me.Rectangle, Me.AlphaMap, p.Rectangle, p.AlphaMap) Then

                    ' 플레이어한테 공격력만큼의 피해를 입힌다.
                    p.Health -= Me.Damage

                    ' 총알 제거
                    Me.Dispose()
                    Exit Sub

                End If

            End If
        Next

    End Sub
    Friend Sub Draw(ByVal Target As Microsoft.DirectX.Direct3D.Sprite)

        ' 총알 업데이트
        UpdateBullet()

        ' 사용이 종료된 개체라면 그리지 않는다.
        If Me.Disposed Then Return

        ' 화면 밖에 있는 경우, 그리지 않기 위해 뷰 로케이션과 플레이어의 위치를 더하고
        ' 식을 적용한다.
        Dim Loc As Vector2D = MyBase.Main.ViewLocation + g_Location
        If Loc.X - Me.Width <= 0 Or Loc.X + Me.Width >= MyBase.Main.Window.Width Then
            'If MyBase.Main.Game = GameMode.AirStriker Then Me.Dispose()
            Return
        End If
        If Loc.Y - Me.Height <= 0 Or Loc.Y + Me.Height >= MyBase.Main.Window.Height Then
            'If MyBase.Main.Game = GameMode.AirStriker Then Me.Dispose()
            Return
        End If

        Target.Draw2D(Me.Texture, Vector2D.Empty, 0, Loc, Drawing.Color.White)

    End Sub

End Class