' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxExplosion/Renderer.vb
'
' Date:
'   2013/12/21
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxExplosion class rendering method.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxExplosion

    ''' <summary>
    ''' 폭발 효과를 그립니다.
    ''' </summary>
    ''' <param name="Target">폭발 효과가 그려질 대상 스프라이트 개체를 입력합니다.</param>
    Public Overrides Sub Draw(ByVal Target As D3.Sprite)

        ' 개체가 삭제된 경우, 렌더링하지 않는다.
        If Me.Disposed Then Return

        ' 파티클을 그린다.
        Dim y As Int32 = Int(g_CurrentParticle / Me.ParticlePerLine),
            x As Int32 = g_CurrentParticle Mod Me.ParticlePerLine

        ' 효과를 그린다.
        Target.Draw2D(g_EffectTexture,
                      New Rectangle(x * g_ParticleSize.Width, y * g_ParticleSize.Height, g_ParticleSize.Width, g_ParticleSize.Height),
                      g_RenderingSize,
                      g_Location,
                      Drawing.Color.White)

        ' 첫 파티클인 경우에만 피해를 적용한다.
        If g_CurrentParticle = 0 Then

            ProcessBlast()

        End If

        ' 다음 파티클을 그리기 위해, 파티클 단계를 1 높인다.
        g_CurrentParticle += 1

        ' 현재 파티클 번호가 파티클 갯수 이상일 경우, 
        ' 모든 파티클을 그린 것이므로 종료한다!
        If g_CurrentParticle >= g_ParticleCount Then
            Me.Dispose()
            Return
        End If

    End Sub

End Class