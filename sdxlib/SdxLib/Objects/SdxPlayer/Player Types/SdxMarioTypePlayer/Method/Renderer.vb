Imports System.Drawing
Partial Class SdxMarioTypePlayer

    Private Sub ProcessJump(ByVal JumpKeyPressed As Boolean)

        ' 이전에 점프 키가 눌린 경우:
        If m_JumpPressed Then

            ' (점프 중단) 지금은 점프 키가 눌리지 않은 경우:
            If Not JumpKeyPressed Then

                If m_JumpStep < 90 - JUMP_STEP_INCREASEMENT Then m_JumpStep = 90 - JUMP_STEP_INCREASEMENT
                m_CanJump = True

                m_JumpPressed = False
                Exit Sub

            End If

            Exit Sub

        End If

        ' 이전에 점프 키가 눌리지 않은 경우:

        ' (점프 시작) 점프 키가 눌린 경우:
        If JumpKeyPressed Then

            ' 점프를 할 수 있는 경우에 점프한다.
            If m_CanJump Then

                ' 점프를 적용하기 위한 변수 초기화
                m_JumpStep = 0
                m_CanJump = False
                g_Jumping = True

            End If

            m_JumpPressed = True

        End If

    End Sub
    Private Sub ApplyJump()

        If Not Me.IsJumping Then Exit Sub

        If m_JumpStep > 180 Then
            If Me.InGround Then
                m_CanJump = True
                g_Jumping = False
                Exit Sub
            End If
            Me.MovingDistanceY -= Math.Cos(SDXHelper.DegToRad(180)) * (JUMP_POWER + EXTRA_JUMP_CONST)
            Exit Sub
        End If

        Me.MovingDistanceY -= Math.Cos(SDXHelper.DegToRad(m_JumpStep)) * JUMP_POWER
        m_JumpStep += JUMP_STEP_INCREASEMENT

    End Sub
    Private Sub ProcessGravity()

        ' 점프 중이 아닐 경우에만 계산한다.
        If Not Me.IsJumping Then
            Debug.Print("[{0}] ProcessGravity: Gravity Applied", TimeSpan.FromMilliseconds(Environment.TickCount).ToString)
            Me.MovingDistanceY += MyBase.Main.WorldGravity
        End If

    End Sub

    Protected Overrides Sub UpdateLocation()

        ' 계산하기 전 위치 저장,
        ' 충돌이 발생하는 블록 목록 저장.
        Dim collideBlocks As ExtendList(Of SdxBlock2D) = GetDirectionalCollideBlocks()

        ' 충돌이 발생하는 블록이 하나도 없을 경우, 실행 종료
        If collideBlocks Is Nothing Then Return

        ' X 좌표, Y 좌표를 순차적으로 업데이트한다.
        MyBase.UpdateX(collideBlocks)
        UpdateY(collideBlocks)

        ' 움직인 거리를 초기화한다.
        Me.MovingDistance = Vector2D.Empty

    End Sub
    Protected Overrides Sub UpdateY(ByVal Blocks As ExtendList(Of SdxBlock2D))

        Me.Y += Me.MovingDistanceY
        For Each b As SdxBlock2D In Blocks

            If Not Me.CollideCheckBox.IntersectsWith(b.Rectangle) Then Continue For

            Select Case Me.MovingDistanceY
                Case Is >= 0    ' (DOWN)
                    Me.Y = b.Y - (Me.CollideBox.Y + Me.CollideBox.Height)

                    ' 점프 중, 충돌이 발생한 경우:
                    If Me.IsJumping Then g_Jumping = False

                Case Else       ' (UP)
                    Me.Y = (b.Y + b.Height) - Me.CollideBox.Y

                    If Me.IsJumping Then m_JumpStep = 90

            End Select

        Next

    End Sub
    Protected Overrides Sub UpdatePlayer(ByVal Ks As Microsoft.DirectX.DirectInput.KeyboardState)

        ' 개체가 삭제된 경우, 함수 호출을 종료한다.
        If Me.Disposed Then
            Return
        End If

        ' 체력이 0이하인 경우, 개체를 삭제한다.
        If Me.Health <= 0 Then
            Me.Dispose()
            Return
        End If

        ' 키보드 상태가 Nothing이 아니고, 활성 상태일때만 키를 검사한다.
        If Ks IsNot Nothing AndAlso Me.Active Then
            Me.CurrentKeyboardState = Ks
            HandleKeyEvents()
        End If

        ' 움직임을 적용한다.
        ApplyJump()
        ProcessGravity()
        UpdateLocation()

    End Sub

End Class