' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxPlayer/Action.vb
'
' Dependencies:
'   PlayerControlSettings
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
'   Defines SdxPlayer's method (Action).

Imports System.Drawing

Partial Class SdxPlayer

    ''' <summary>
    ''' 이동 방향과 충돌하는 블록 목록을 계산합니다.
    ''' </summary>
    Protected Function GetDirectionalCollideBlocks() As ExtendList(Of SdxBlock2D)

        ' 움직임이 없는 경우, 함수 실행을 종료한다.
        If Me.MovingDistance.IsEmpty Then Return Nothing

        ' 계산하기 전 위치 저장,
        ' 충돌이 발생하는 블록 목록 저장.
        Dim orgVector As Vector2D = g_Location,
            orgCollideBox As RectangleF = Me.CollideCheckBox,
            collideBlocks As New ExtendList(Of SdxBlock2D)

        ' 충돌이 발생하는 블록들을 계산한다.
        For Each b As SdxBlock2D In MyBase.Main.Blocks

            ' 블록의 상태가 Wall(벽)이 아닐 경우, 플레이어가 지나갈 수 있으므로
            ' 충돌 검사를 하지 않는다.
            If (b.Flags And SdxBlock2D.BlockStates.Wall) = False Then Continue For

            ' X축 이동을 적용한다
            orgCollideBox.X += Me.MovingDistanceX
            If orgCollideBox.IntersectsWith(b.Rectangle) Then
                If Not collideBlocks.Contains(b) Then collideBlocks.Add(b)
            End If

            ' Y축 이동을 적용한다.
            orgCollideBox.Y += Me.MovingDistanceY
            If orgCollideBox.IntersectsWith(b.Rectangle) Then
                If Not collideBlocks.Contains(b) Then collideBlocks.Add(b)
            End If

            ' 초기값으로 돌려놓는다.
            orgCollideBox.Location -= Me.MovingDistance

        Next

        Return collideBlocks

    End Function

    ''' <summary>
    ''' 키보드 이벤트를 처리하는 함수입니다. 이 함수는 추상 함수이므로, 반드시 SdxPlayer 클래스를 구현하는 상속 클래스에서 따로 구현해야 합니다.
    ''' </summary>
    Protected Overridable Sub HandleKeyEvents()
    End Sub

    ''' <summary>
    ''' X축의 이동 방향을 업데이트합니다.
    ''' </summary>
    ''' <param name="Blocks">충돌이 일어난 블록의 목록을 입력합니다.</param>
    Protected Overridable Sub UpdateX(ByVal Blocks As ExtendList(Of SdxBlock2D))

        Me.X += Me.MovingDistanceX
        For Each b As SdxBlock2D In Blocks

            If Not Me.CollideCheckBox.IntersectsWith(b.Rectangle) Then Continue For
            Select Case Me.MovingDistanceX
                Case Is >= 0    ' (RIGHT)
                    Me.X = b.X - (Me.CollideBox.X + Me.CollideBox.Width)

                Case Else       ' (LEFT)
                    Me.X = (b.X + b.Width) - Me.CollideBox.X

            End Select

        Next

    End Sub

    ''' <summary>
    ''' Y축의 이동 방향을 업데이트합니다.
    ''' </summary>
    ''' <param name="Blocks">충돌이 일어난 블록의 목록을 입력합니다.</param>
    Protected Overridable Sub UpdateY(ByVal Blocks As ExtendList(Of SdxBlock2D))

        Me.Y += Me.MovingDistanceY
        For Each b As SdxBlock2D In Blocks

            If Not Me.CollideCheckBox.IntersectsWith(b.Rectangle) Then Continue For

            Select Case Me.MovingDistanceY
                Case Is >= 0    ' (DOWN)
                    Me.Y = b.Y - (Me.CollideBox.Y + Me.CollideBox.Height)

                Case Else       ' (UP)
                    Me.Y = (b.Y + b.Height) - Me.CollideBox.Y

            End Select

        Next

    End Sub

    ''' <summary>
    ''' 플레이어의 움직임을 업데이트하는 함수입니다.
    ''' </summary>
    Protected Overridable Sub UpdateLocation()

        ' 계산하기 전 위치 저장,
        ' 충돌이 발생하는 블록 목록 저장.
        Dim collideBlocks As ExtendList(Of SdxBlock2D) = GetDirectionalCollideBlocks()

        ' 충돌이 발생하는 블록이 하나도 없을 경우, 실행 종료
        If collideBlocks Is Nothing Then Return

        ' X 좌표, Y 좌표를 순차적으로 업데이트한다.
        UpdateX(collideBlocks)
        UpdateY(collideBlocks)

        ' 움직인 거리를 초기화한다.
        Me.MovingDistance = Vector2D.Empty

    End Sub

    ''' <summary>
    ''' 플레이어의 상태를 업데이트하는 함수입니다.
    ''' </summary>
    ''' <param name="Ks">현재의 키보드 상태를 입력합니다.</param>
    Protected Overridable Sub UpdatePlayer(ByVal Ks As Microsoft.DirectX.DirectInput.KeyboardState)

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
        UpdateLocation()

    End Sub

End Class