Imports System.Drawing
Partial Class SdxRacingTypePlayer

    'Protected Overrides Sub ProcessAttack(ByVal Target As SdxPlayer)
    '    If Me Is Target Then Return
    '    Attack(Target)
    'End Sub
    'Public Sub MoveTo(ByVal TargetLocation As Vector2D)

    '    g_Angle = SdxHelper.GetVectorAngle(g_Location, TargetLocation)
    '    Dim aVec As Vector2D = SdxHelper.GetAngleVector(g_Angle)

    'End Sub

    'Public Sub Attack(ByVal Target As SdxPlayer)

    '    If SdxHelper.GetVectorDistance(Me.Location, Target.Location) <= 500.0F Then

    '        If Environment.TickCount >= Me.LastAttack + Me.AttackDelay Then

    '            If g_Energy >= g_EnergyCost Then

    '                ' 각도 설정
    '                Dim a As Int32 = SdxHelper.GetVectorAngle(Drawing.Point.Round(Me.RealLocation), Drawing.Point.Round(Target.RealLocation))
    '                Me.Angle = a

    '                ' 움직임 멈춤!
    '                g_CurrentPower = 0

    '                ' 마지막 공격 시간 저장
    '                Me.LastAttack = Environment.TickCount

    '                ' 발사 위치 저장
    '                Dim fireHead As Vector2D = SdxHelper.GetRectangleHead(Me.Angle, Me.Location, Me.Size)

    '                ' 선
    '                'Dim tl As New SdxTextureLine(MyBase.Main, MyBase.Main.SharedResource.LaserStripe8, fireHead, Target.RealLocation, FadeType.TrigonometricFunctionBased, FadeEffect.FadeOut, 25, False, False)
    '                'tl.Thickness = 5
    '                'tl.Color = Drawing.Color.IndianRed
    '                'MyBase.Main.Lines.Add(tl)
    '                'Dim l As New SdxLine2D(fireHead, Target.RealLocation, Drawing.Color.IndianRed, 1, FadeType.TrigonometricFunctionBased, FadeEffect.FadeOut, 35, False, False)
    '                'MyBase.Main.Lines.Add(l)
    '                ' 미사일
    '                Dim m As New SdxMissile(MyBase.Main, My.Resources.Bullet_2, fireHead, Target, Me.Damage)
    '                MyBase.Main.Effects.Add(m)

    '                g_Energy -= g_EnergyCost

    '            End If
    '        End If

    '    End If

    'End Sub

End Class