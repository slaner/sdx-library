' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBlock2D/Renderer.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  18
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
'   Defines SdxBlock2D's method (Rendering).

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxBlock2D

    Friend Sub Draw(ByVal Target As D3.Sprite)

        ' 블록 업데이트
        UpdateBlock()

        ' 사용이 종료된 개체라면, 그리지 않는다.
        If Me.Disposed Then Return

        ' 화면 밖에 있는 경우, 그리지 않기 위해 뷰 로케이션과 블록의 위치를 더하고
        ' 식을 적용한다.
        Dim Loc As Vector2D = MyBase.Main.ViewLocation + g_Location
        If Loc.X + Me.Width <= 0 Or Loc.X >= MyBase.Main.WindowSize.Width + Me.Width Then Return
        If Loc.Y + Me.Height <= 0 Or Loc.Y >= MyBase.Main.WindowSize.Height + Me.Height Then Return

        Target.Draw2D(Me.Texture, Vector2D.Empty, 0, Loc, Me.Color)

    End Sub

End Class