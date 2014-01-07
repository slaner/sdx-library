Imports System.Drawing
Partial Class SdxRacingTypePlayer

    Private Sub ProcessMoves()

        If g_CurrentPower <> 0 Then

            If g_CurrentPower > 0 Then
                g_CurrentPower -= Me.BrakePower / 3
                If g_CurrentPower < 0 Then g_CurrentPower = 0
            ElseIf g_CurrentPower < 0 Then
                g_CurrentPower += Me.BrakePower / 3
                If g_CurrentPower > 0 Then g_CurrentPower = 0
            End If

            Dim angVector As Vector2D = SDXHelper.GetAngleVector(Me.Angle) * -(g_CurrentPower / 25),
                orgVector As Vector2D = g_Location,
                orgRectangle As RectangleF,
                collideBlocks As New ExtendList(Of SdxBlock2D)

            orgRectangle.Location = g_Location
            orgRectangle.Size = g_Size

            For Each b As SdxBlock2D In MyBase.Main.Blocks

                If b.Flags And SdxBlock2D.BlockStates.Wall Then

                    ' X 축 진행 후 충돌이 있는지 검사한다.
                    orgRectangle.X += angVector.X

                    ' 충돌 검사
                    If orgRectangle.IntersectsWith(b.Rectangle) Then
                        ' 충돌이 발생한 경우, 선언한 목록에 추가한다.
                        If Not collideBlocks.Contains(b) Then collideBlocks.Add(b)
                    End If


                    ' Y 축 진행 후 충돌이 있는지 검사한다.
                    orgRectangle.Y += angVector.Y

                    ' 충돌 검사
                    If orgRectangle.IntersectsWith(b.Rectangle) Then
                        ' 충돌이 발생한 경우, 선언한 목록에 추가한다.
                        If Not collideBlocks.Contains(b) Then collideBlocks.Add(b)
                    End If


                    ' X, Y 축 값을 원래대로 돌려놓는다.
                    orgRectangle.Location = g_Location

                End If

            Next

            Dim t As SdxBlock2D = Nothing

            ' X 축 진행.
            Me.X += angVector.X
            If hasCollision(collideBlocks, t) Then
                Select Case angVector.X
                    Case Is >= 0
                        Me.X = t.X - Me.Width

                    Case Else
                        Me.X = t.X + Me.Width

                End Select
            End If

            ' Y 축 진행
            Me.Y += angVector.Y
            If hasCollision(collideBlocks, t) Then
                Select Case angVector.Y
                    Case Is >= 0
                        Me.Y = t.Y - Me.Height

                    Case Else
                        Me.Y = t.Y + t.Height

                End Select
            End If

        End If

    End Sub
    Private Function hasCollision(ByVal list As ExtendList(Of SdxBlock2D), ByRef touched As SdxBlock2D) As Boolean

        For Each b In list

            If Me.Rectangle.IntersectsWith(b.Rectangle) Then
                touched = b
                Return True
            End If

        Next
        Return False

    End Function
    Protected Overrides Sub HandleKeyEvents()

        If Me.ControlSettings Is Nothing Then Return
        If Me.CurrentKeyboardState Is Nothing Then Return

        With Me.ControlSettings

            ' 부스터(=달리기):
            m_Turbo = False
            If Me.CurrentKeyboardState(.Sprint) Then
                m_Turbo = True
            End If

            ' 좌우 방향키를 누르면, 각도를 바꾼다.
            If Me.CurrentKeyboardState(.MoveLeft) Then Me.Angle -= Me.SteeringAngle
            If Me.CurrentKeyboardState(.MoveRight) Then Me.Angle += Me.SteeringAngle

            ' 상하 방향키
            ' 전진:
            If Me.CurrentKeyboardState(.MoveUp) Then

                ' 터보 사용중인 경우엔, 속도 증가치가 7.5배, 최고 속도 제한을 100배로 높임
                If m_Turbo Then
                    g_CurrentPower += (Me.TorquePower / 10) * 7.5
                    If g_CurrentPower >= Me.MaximumTorque * 100 Then g_CurrentPower = Me.MaximumTorque * 100
                Else
                    g_CurrentPower += Me.TorquePower / 10
                    If g_CurrentPower >= Me.MaximumTorque Then g_CurrentPower = Me.MaximumTorque
                End If

            End If

            ' 후진:
            If Me.CurrentKeyboardState(.MoveDown) Then

                g_CurrentPower -= Me.TorquePower / 10
                If g_CurrentPower <= Me.MinimumTorque Then g_CurrentPower = Me.MinimumTorque

            End If

            ' 브레이크(=점프):
            If Me.CurrentKeyboardState(.Jump) Then

                If g_CurrentPower > 0 Then
                    g_CurrentPower -= Me.BrakePower
                    If g_CurrentPower < 0 Then g_CurrentPower = 0
                ElseIf g_CurrentPower < 0 Then
                    g_CurrentPower += Me.BrakePower
                    If g_CurrentPower > 0 Then g_CurrentPower = 0
                End If

            End If

            '' 공격
            'If Me.CurrentKeyboardState(.Attack1) Then

            '    For Each p In MyBase.Main.Players
            '        ProcessAttack(p)
            '    Next

            'End If

        End With

    End Sub
    Protected Overrides Sub UpdatePlayer(ByVal Ks As Microsoft.DirectX.DirectInput.KeyboardState)

        ' 개체가 삭제된 경우, 함수 호출을 종료한다.
        If Me.Disposed Then
            Return
        End If

        '' 체력이 0이하인 경우, 개체를 삭제한다.
        'If Me.Health <= 0 Then
        '    Dim explodeEffect As New SdxExplosion(MyBase.Main, MyBase.Main.SharedResource.DefaultExplosion64, 64, 64)
        '    Dim biggerSize As Int32 = IIf(Me.Width > Me.Height, Me.Width, Me.Height)
        '    explodeEffect.Location = Me.Location + Me.Center
        '    explodeEffect.Location -= New Point(32, 32)
        '    explodeEffect.RenderingSize = New Size(64, 64)
        '    MyBase.Main.Effects.Add(explodeEffect)
        '    Me.Dispose()
        '    Return
        'End If

        ' 키보드 상태가 Nothing이 아니고, 활성 상태일때만 키를 검사한다.
        If Ks IsNot Nothing AndAlso Me.Active Then
            Me.CurrentKeyboardState = Ks
            HandleKeyEvents()
        End If

        ' 움직임을 적용한다.
        ProcessMoves()

    End Sub

    Friend Overrides Sub Draw(ByVal Ks As Microsoft.DirectX.DirectInput.KeyboardState, ByVal Target As Microsoft.DirectX.Direct3D.Sprite)

        g_Drawn = False

        ' 플레이어 업데이트
        UpdatePlayer(Ks)

        ' 개체가 삭제된 경우, 함수 호출을 종료한다.
        If Me.Disposed Then
            Return
        End If

        If g_ChaseCam Then
            MyBase.Main.ViewLocation = New Vector2D((MyBase.Main.Window.ClientSize.Width / 2) - (Me.RealLocation.X - Me.MovingDistanceX),
                                                    (MyBase.Main.Window.ClientSize.Height / 2) - (Me.RealLocation.Y - Me.MovingDistanceY))
        End If

        ' 화면 밖에 있는 경우, 그리지 않기 위해 뷰 로케이션과 플레이어의 위치를 더하고
        ' 식을 적용한다.
        Dim Loc As Vector2D = MyBase.Main.ViewLocation + g_Location
        If Loc.X + Me.Width <= 0 Or Loc.X + Me.Width >= MyBase.Main.Window.ClientSize.Width Then Exit Sub
        If Loc.Y + Me.Height <= 0 Or Loc.Y + Me.Height >= MyBase.Main.Window.ClientSize.Height Then Exit Sub

        ' 그림자 그리기가 활성화된 경우, 그림자를 먼저 그린다.
        If Me.ApplyShadow Then
            Target.Draw2D(Me.Texture, Drawing.Rectangle.Empty, Me.Size, Me.Center, SDXHelper.DegToRad(Me.Angle), Loc + Me.Center + Me.ShadowLocation, Drawing.Color.FromArgb(96, Drawing.Color.DarkGray))
        End If

        ' 투명도가 적용된 경우, 투명도를 적용한다.
        Dim c As Drawing.Color = Me.Color
        If Me.Opacity < 1 Then
            c = Drawing.Color.FromArgb(255 * Me.Opacity, Me.Color)
        End If
        Target.Draw2D(Me.Texture, Drawing.Rectangle.Empty, Me.Size, Me.Center, SDXHelper.DegToRad(Me.Angle), Loc + Me.Center, c)

        '' 체력바를 그림ㅋ
        'Dim barStep As Single = Me.Height / Me.MaximumHealth,
        '    Xofs As Int32 = -(Me.Height - Me.Width) / 2,
        '    energyStep As Single = Me.Height / 15000

        'Target.Draw2D(MyBase.Main.SharedResource.GreenBar, New Rectangle(0, 0, 1, 5), New SizeF(Me.Height, 3), Loc + New Point(Xofs, Me.Height + 2), Drawing.Color.MediumSlateBlue)
        'Target.Draw2D(MyBase.Main.SharedResource.GreenBar, New Rectangle(0, 0, 1, 5), New SizeF(Me.g_Energy * energyStep, 3), Loc + New Point(Xofs, Me.Height + 2), Drawing.Color.LightSkyBlue)
        'Target.Draw2D(MyBase.Main.SharedResource.RedBar, New Rectangle(0, 0, 1, 5), New SizeF(Me.Height, 3), Loc + New Point(Xofs, Me.Height + 6), Drawing.Color.White)
        'Target.Draw2D(MyBase.Main.SharedResource.GreenBar, New Rectangle(0, 0, 1, 5), New SizeF(Me.Health * barStep, 3), Loc + New Point(Xofs, Me.Height + 6), Drawing.Color.White)

        g_Drawn = True

    End Sub

End Class