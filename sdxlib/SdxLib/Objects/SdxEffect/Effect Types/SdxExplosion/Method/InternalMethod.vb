Imports System.Drawing
Partial Class SdxExplosion

    Private Sub ProcessBlast()

        ' 폭발 피해를 활성화한 경우에만 계산한다.
        If g_UseBlast Then

            ' 플레이어를 모두 순회한다.
            For Each p As SdxPlayer In MyBase.Main.Players

                ' 거리를 계산한다.
                Dim dist As Single = Math.Abs(SDXHelper.GetVectorDistance(g_Location + New Size(g_RenderingSize.Width / 2, g_RenderingSize.Height / 2), p.RealLocation))

                ' 범위 안에 플레이어가 있을 경우에만
                ' 피해량을 계산한다.
                If dist <= g_Radius Then

                    ' 범위 내에 있는 모든 플레이어에게 동일한 피해를 줄 경우:
                    If g_FixedBlastDamage Then

                        p.Health -= g_Damage

                    Else ' 범위 별로 동적인 피해를 줄 경우:

                        Dim distStep As Single = g_Radius / 3
                        Select Case dist
                            Case 0 To distStep ' 가장 근접, 100% 피해 적용
                                p.Health -= g_Damage

                            Case distStep To distStep * 2 ' 중간 근접, 66% 피해 적용
                                p.Health -= g_Damage / 1.5

                            Case distStep * 2 To g_Radius ' 테두리, 33% 피해 적용
                                p.Health -= g_Damage / 3

                        End Select

                    End If

                End If

            Next

        End If

    End Sub

End Class