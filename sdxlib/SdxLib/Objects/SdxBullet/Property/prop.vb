' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBullet/prop.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  11
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
'   Defines SdxBullet class's property.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Partial Class SdxBullet

#Region "Static Property"

    ''' <summary>
    ''' 총알의 생명 주기(ms)를 가져오거나 설정합니다.
    ''' </summary>
    Public Shared Property LifeTime As Int32 = 20000

#End Region

    ''' <summary>
    ''' 투명도 맵을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property AlphaMap As Byte() Implements IAlphaMapSupportedGraphicsResource.AlphaMap
        Get
            Return g_AlphaMap
        End Get
    End Property



    ''' <summary>
    ''' 총알의 위치와 크기 정보를 저장하고 있는 사각형을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Rectangle As System.Drawing.RectangleF Implements IGraphicsResource.Rectangle
        Get
            Return New RectangleF(g_Location, g_Size)
        End Get
    End Property

    ''' <summary>
    ''' 리소스의 유형을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ResourceType As ResourceTypes Implements IGraphicsResource.ResourceType
        Get
            Return ResourceTypes.Texture
        End Get
    End Property

    ''' <summary>
    ''' 총알의 텍스쳐를 가져옵니다.
    ''' </summary>
    Friend ReadOnly Property Texture As D3.Texture Implements IGraphicsResource.Texture
        Get
            Return g_BulletTexture
        End Get
    End Property



    ''' <summary>
    ''' 총알의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Size As System.Drawing.Size Implements IGraphicsResource.Size
        Get
            Return g_Size
        End Get
    End Property

    ''' <summary>
    ''' 총알의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Width As Integer Implements IGraphicsResource.Width
        Get
            Return g_Size.Width
        End Get
    End Property

    ''' <summary>
    ''' 총알의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Height As Integer Implements IGraphicsResource.Height
        Get
            Return g_Size.Width
        End Get
    End Property



    ''' <summary>
    ''' 총알의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As Vector2D Implements IGraphicsResource.Location
        Get
            Return g_Location
        End Get
        Set(ByVal value As Vector2D)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 총알의 X 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Single Implements IGraphicsResource.X
        Get
            Return g_Location.X
        End Get
        Set(ByVal value As Single)
            g_Location.X = value
        End Set
    End Property

    ''' <summary>
    ''' 총알의 Y 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single Implements IGraphicsResource.Y
        Get
            Return g_Location.Y
        End Get
        Set(ByVal value As Single)
            g_Location.Y = value
        End Set
    End Property


    Public Property Owner As SdxPlayer
        Get
            Return g_Owner
        End Get
        Set(ByVal value As SdxPlayer)
            g_Owner = value
        End Set
    End Property

    ''' <summary>
    ''' 총알의 공격력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Damage As Int32
        Get
            Return g_Damage
        End Get
        Set(ByVal value As Int32)
            g_Damage = value
        End Set
    End Property

    Public ReadOnly Property Speed As Vector2D
        Get
            Return g_Speed
        End Get
    End Property

End Class