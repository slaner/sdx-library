' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxEffect/InternalMethod.vb
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
'   Defines SdxExplosion class internal method.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxEffect

    Private Sub Initialize(ByVal Texture As D3.Texture, ByVal ParticleSize As Size)

        g_EffectTexture = Texture
        g_Size = New Size(Texture.GetLevelDescription(0).Width, Texture.GetLevelDescription(0).Height)

        g_ParticleSize = ParticleSize

        g_ParticleCount = (g_Size.Width / g_ParticleSize.Width) * (g_Size.Height / g_ParticleSize.Height)
        g_ParticlePerLine = g_Size.Width / g_ParticleSize.Width

    End Sub
    Private Sub Initialize(ByVal Texture As D3.Texture, ByVal ParticleSize As Size, ByVal ParticleCount As Int32)

        g_EffectTexture = Texture
        g_Size = New Size(Texture.GetLevelDescription(0).Width, Texture.GetLevelDescription(0).Height)

        g_ParticleSize = ParticleSize

        g_ParticleCount = ParticleCount
        g_ParticlePerLine = g_Size.Width / g_ParticleSize.Width

    End Sub
    Private Sub Initialize(ByVal Img As Image, ByVal ParticleSize As Size)

        m_LoadFromImage = True

        g_EffectTexture = D3.Texture.FromBitmap(MyBase.Main.Device, Img, 0, 1)
        g_Size = Img.Size

        g_ParticleSize = ParticleSize

        g_ParticleCount = (g_Size.Width / g_ParticleSize.Width) * (g_Size.Height / g_ParticleSize.Height)
        g_ParticlePerLine = g_Size.Width / g_ParticleSize.Width

    End Sub
    Private Sub Initialize(ByVal Img As Image, ByVal ParticleSize As Size, ByVal ParticleCount As Int32)

        m_LoadFromImage = True

        g_EffectTexture = D3.Texture.FromBitmap(MyBase.Main.Device, Img, 0, 1)
        g_Size = Img.Size

        g_ParticleSize = ParticleSize

        g_ParticleCount = ParticleCount
        g_ParticlePerLine = g_Size.Width / g_ParticleSize.Width

    End Sub

End Class