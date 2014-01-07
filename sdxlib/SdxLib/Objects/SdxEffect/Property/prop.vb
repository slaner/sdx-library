' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxEffect/prop.vb
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
'   Defines SdxEffect class property.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxEffect

    ''' <summary>
    ''' 효과의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As Vector2D Implements IGraphicsEffect.Location
        Get
            Return g_Location
        End Get
        Set(ByVal value As Vector2D)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 효과의 X 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Single Implements IGraphicsEffect.X
        Get
            Return g_Location.X
        End Get
        Set(ByVal value As Single)
            g_Location.X = value
        End Set
    End Property

    ''' <summary>
    ''' 효과의 Y 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single Implements IGraphicsEffect.Y
        Get
            Return g_Location.Y
        End Get
        Set(ByVal value As Single)
            g_Location.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 현재 렌더링되고 있는 파티클 번호를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property CurrentParticle As Integer Implements IGraphicsEffect.CurrentParticle
        Get
            Return g_CurrentParticle
        End Get
    End Property

    ''' <summary>
    ''' 효과 텍스쳐의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Height As Integer Implements IGraphicsEffect.Height
        Get
            Return g_Size.Width
        End Get
    End Property

    ''' <summary>
    ''' 파티클의 갯수를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ParticleCount As Integer Implements IGraphicsEffect.ParticleCount
        Get
            Return g_ParticleCount
        End Get
    End Property

    ''' <summary>
    ''' 파티클의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ParticleHeight As Integer Implements IGraphicsEffect.ParticleHeight
        Get
            Return g_ParticleSize.Height
        End Get
    End Property

    ''' <summary>
    ''' 파티클의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ParticleSize As System.Drawing.Size Implements IGraphicsEffect.ParticleSize
        Get
            Return g_ParticleSize
        End Get
    End Property

    ''' <summary>
    ''' 파티클의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ParticleWidth As Integer Implements IGraphicsEffect.ParticleWidth
        Get
            Return g_ParticleSize.Width
        End Get
    End Property

    ''' <summary>
    ''' 한 줄에 몇 개의 파티클이 있는지를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ParticlePerLine As Int32 Implements IGraphicsEffect.ParticlePerLine
        Get
            Return g_ParticlePerLine
        End Get
    End Property

    ''' <summary>
    ''' 효과 텍스쳐의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Size As System.Drawing.Size Implements IGraphicsEffect.Size
        Get
            Return g_Size
        End Get
    End Property

    ''' <summary>
    ''' 효과 텍스쳐의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Width As Integer Implements IGraphicsEffect.Width
        Get
            Return g_Size.Width
        End Get
    End Property

    ''' <summary>
    ''' 렌더링할 크기를 가져오거나 설정합니다.
    ''' </summary>
    Public Property RenderingSize As Drawing.Size Implements IGraphicsEffect.RenderingSize
        Get
            Return g_RenderingSize
        End Get
        Set(ByVal value As Drawing.Size)
            g_RenderingSize = value
        End Set
    End Property

    ''' <summary>
    ''' 렌더링할 넓이를 가져오거나 설정합니다.
    ''' </summary>
    Public Property RenderingWidth As Int32 Implements IGraphicsEffect.RenderingWidth
        Get
            Return g_RenderingSize.Width
        End Get
        Set(ByVal value As Int32)
            g_RenderingSize.Width = value
        End Set
    End Property

    ''' <summary>
    ''' 렌더링할 높이를 가져오거나 설정합니다.
    ''' </summary>
    Public Property RenderingHeight As Int32 Implements IGraphicsEffect.RenderingHeight
        Get
            Return g_RenderingSize.Height
        End Get
        Set(ByVal value As Int32)
            g_RenderingSize.Height = value
        End Set
    End Property

    ''' <summary>
    ''' 효과 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property EffectTexture As D3.Texture Implements IGraphicsEffect.EffectTexture
        Get
            Return g_EffectTexture
        End Get
    End Property

End Class