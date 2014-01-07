Imports System.Drawing
Partial Class SdxMissile

    Public Overrides Sub Draw(ByVal Target As Microsoft.DirectX.Direct3D.Sprite)

        If Me.Disposed Then Return

        ' 대상 플레이어가 유효하고, 삭제되지 않은 경우
        ' 위치 정보를 업데이트한다.
        If m_Target IsNot Nothing AndAlso Not m_Target.Disposed Then
            If m_LastTargetLocation <> m_Target.RealLocation Then m_LastTargetLocation = m_Target.RealLocation
        End If

        ' 각도를 계산한다.
        Dim Ang As Single = SDXHelper.GetVectorAngle(g_Location, m_LastTargetLocation)

        ' 각도 벡터를 계산한다.
        Dim Av As Vector2D = SDXHelper.GetAngleVector(Ang)

        ' 추진력을 계산한다.
        Dim spd As Single = Math.Sin(SDXHelper.DegToRad(m_CurrentStep * m_SpeedStep))

        ' X, Y 좌표에 계산한 값을 뺀다.
        g_Location.X -= Av.X * (g_MaxSpeed * spd)
        g_Location.Y -= Av.Y * (g_MaxSpeed * spd)

selectAgain:

        If Ang >= 0.0F AndAlso Ang < 90.0F Then

            ' End of Life
            If g_Location.X >= m_LastTargetLocation.X AndAlso g_Location.Y <= m_LastTargetLocation.Y Then
                If m_Target IsNot Nothing AndAlso Not m_Target.Disposed Then m_Target.Health -= g_Damage
                Dim m As New SdxExplosion(MyBase.Main, MyBase.Main.SharedResource.DefaultExplosion64, 64, 64)
                m.RenderingSize = New Size(16, 16)
                m.Location = m_LastTargetLocation - New Size(8, 8)
                MyBase.Main.Effects.Add(m)
                Me.Dispose()
                Return
            End If

        ElseIf Ang >= 90.0F AndAlso Ang < 180.0F Then

            ' End of Life
            If g_Location.X >= m_LastTargetLocation.X AndAlso g_Location.Y >= m_LastTargetLocation.Y Then
                If m_Target IsNot Nothing AndAlso Not m_Target.Disposed Then m_Target.Health -= g_Damage
                Dim m As New SdxExplosion(MyBase.Main, MyBase.Main.SharedResource.DefaultExplosion64, 64, 64)
                m.RenderingSize = New Size(16, 16)
                m.Location = m_LastTargetLocation - New Size(8, 8)
                MyBase.Main.Effects.Add(m)
                Me.Dispose()
                Return
            End If

        ElseIf Ang >= 180.0F AndAlso Ang < 270.0F Then

            ' End of Life
            If g_Location.X <= m_LastTargetLocation.X AndAlso g_Location.Y >= m_LastTargetLocation.Y Then
                If m_Target IsNot Nothing AndAlso Not m_Target.Disposed Then m_Target.Health -= g_Damage
                Dim m As New SdxExplosion(MyBase.Main, MyBase.Main.SharedResource.DefaultExplosion64, 64, 64)
                m.RenderingSize = New Size(16, 16)
                m.Location = m_LastTargetLocation - New Size(8, 8)
                MyBase.Main.Effects.Add(m)
                Me.Dispose()
                Return
            End If

        ElseIf Ang >= 270.0F AndAlso Ang < 360.0F Then

            ' End of Life
            If g_Location.X <= m_LastTargetLocation.X AndAlso g_Location.Y >= m_LastTargetLocation.Y Then
                If m_Target IsNot Nothing AndAlso Not m_Target.Disposed Then m_Target.Health -= g_Damage
                Dim m As New SdxExplosion(MyBase.Main, MyBase.Main.SharedResource.DefaultExplosion64, 64, 64)
                m.RenderingSize = New Size(16, 16)
                m.Location = m_LastTargetLocation - New Size(8, 8)
                MyBase.Main.Effects.Add(m)
                Me.Dispose()
                Return
            End If

        ElseIf Ang >= 360.0F Then

            Ang -= 360.0F
            GoTo SelectAgain

        End If

        Target.Draw2D(g_EffectTexture, New Rectangle(Point.Empty, g_Size), Nothing, m_Center, SDXHelper.DegToRad(Ang), g_Location, Drawing.Color.White)
        m_CurrentStep += 1
        If m_CurrentStep > 90 Then m_CurrentStep = 90

    End Sub

End Class