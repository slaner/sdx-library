Partial Class SdxPacManTypePlayer

    Protected Overrides Sub HandleKeyEvents()

        If Me.ControlSettings Is Nothing Then Return
        If Me.CurrentKeyboardState Is Nothing Then Return
        If Not Me.Active Then Return

        With Me.ControlSettings

            ' 상하좌우 방향키를 방향별로 이동한다.
            If .ControlSets.ContainsKey("move_left") AndAlso Me.CurrentKeyboardState(.MoveLeft) Then Me.MovingDistanceX -= Me.Speed
            If .ControlSets.ContainsKey("move_right") AndAlso Me.CurrentKeyboardState(.MoveRight) Then Me.MovingDistanceX += Me.Speed
            If .ControlSets.ContainsKey("move_down") AndAlso Me.CurrentKeyboardState(.MoveDown) Then Me.MovingDistanceY += Me.Speed
            If .ControlSets.ContainsKey("move_up") AndAlso Me.CurrentKeyboardState(.MoveUp) Then Me.MovingDistanceY -= Me.Speed

        End With

    End Sub
    Protected Overrides Sub UpdatePlayer(ByVal Ks As Microsoft.DirectX.DirectInput.KeyboardState)

        If Me.Health <= 0 Then Me.Dispose()
        If Me.Disposed Then Return
        Me.CurrentKeyboardState = Ks
        HandleKeyEvents()
        UpdateLocation()

    End Sub

End Class