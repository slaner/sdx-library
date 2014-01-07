Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

''' <summary>
''' 효과 리소스를 나타냅니다.
''' </summary>
Public Interface IGraphicsEffect
    Inherits IGraphicsDisposable

    Property Location As Vector2D
    Property X As Single
    Property Y As Single

    ReadOnly Property CurrentParticle As Int32
    ReadOnly Property ParticleCount As Int32

    ReadOnly Property ParticleSize As Drawing.Size
    ReadOnly Property ParticleWidth As Int32
    ReadOnly Property ParticleHeight As Int32
    ReadOnly Property ParticlePerLine As Int32

    Property RenderingSize As Drawing.Size
    Property RenderingWidth As Int32
    Property RenderingHeight As Int32

    ReadOnly Property Size As Drawing.Size
    ReadOnly Property Width As Int32
    ReadOnly Property Height As Int32

    ReadOnly Property EffectTexture As D3.Texture

    Sub Draw(ByVal Target As D3.Sprite)

End Interface