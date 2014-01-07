Partial Class SdxMarioTypePlayer

    Protected Overrides Sub HandleKeyEvents()

        If Me.ControlSettings Is Nothing Then Return
        If Me.CurrentKeyboardState Is Nothing Then Return

        With Me.ControlSettings

            ' 좌우 방향키: 앞으로 이동, 뒤로 이동
            If Me.CurrentKeyboardState(.MoveLeft) Then Me.MovingDistanceX -= Me.Speed
            If Me.CurrentKeyboardState(.MoveRight) Then Me.MovingDistanceX += Me.Speed

            ' 브레이크(=점프):
            If .ControlSets.ContainsKey("jump") Then

                ProcessJump(Me.CurrentKeyboardState(.Jump))

            End If

            '' 공격
            'If Me.CurrentKeyboardState(.Attack1) Then

            '    For Each p In MyBase.Main.Players
            '        ProcessAttack(p)
            '    Next

            'End If

        End With

    End Sub

End Class