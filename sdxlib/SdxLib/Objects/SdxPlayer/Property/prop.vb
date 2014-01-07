' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxPlayer/prop.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
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
'   Defines SdxPlayer class's properties.

Imports DI = Microsoft.DirectX.DirectInput
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxPlayer

    ''' <summary>
    ''' 플레이어의 방어력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Armor As Int32
        Get
            Return g_Armor
        End Get
        Set(ByVal value As Int32)
            g_Armor = value
        End Set
    End Property

    ''' <summary>
    ''' 공격의 유효 사정거리를 가져오거나 설정합니다.
    ''' </summary>
    Public Property AttackRange As Int32
        Get
            Return g_AttackRange
        End Get
        Set(ByVal value As Int32)
            g_AttackRange = value
        End Set
    End Property

    ''' <summary>
    ''' 공격 대기시간을 가져오거나 설정합니다.
    ''' </summary>
    Public Property AttackDelay As Int32
        Get
            Return g_AttackDelay
        End Get
        Set(ByVal value As Int32)
            g_AttackDelay = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 공격력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Damage As Int32
        Get
            Return g_Damage ' + (Other Methods)
        End Get
        Set(ByVal value As Int32)
            g_Damage = value
        End Set
    End Property

#Region "Air Attacks"

    ''' <summary>
    ''' 공격할 때, 총알이 나가는 시작 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property BulletHead As Drawing.Point
        Get
            Return g_BulletHead
        End Get
        Set(ByVal value As Drawing.Point)
            g_BulletHead = value
        End Set
    End Property

    ''' <summary>
    ''' 총알의 속도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property BulletSpeed As Vector2D
        Get
            Return g_BulletSpeed
        End Get
        Set(ByVal value As Vector2D)
            g_BulletSpeed = value
        End Set
    End Property


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
    ''' 플레이어가 그려졌는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property IsDrawn As Boolean
        Get
            Return g_Drawn
        End Get
    End Property

    ''' <summary>
    ''' 블록과의 충돌을 무시할 것인지의 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property IgnoreBlocks As Boolean
        Get
            Return g_IgnoreBlocks
        End Get
        Set(ByVal value As Boolean)
            g_IgnoreBlocks = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 현재 체력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Health As Int32
        Get
            Return g_CurrentHealth
        End Get
        Set(ByVal value As Int32)
            g_CurrentHealth = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 최대 체력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property MaximumHealth As Int32
        Get
            Return g_MaximumHealth
        End Get
        Set(ByVal value As Int32)
            g_MaximumHealth = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어 조작 설정을 저장하고 있는 PlayerControlSettings 개체를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ControlSettings As PlayerControlSettings
        Get
            Return g_Pcs
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Overridable Property Location As Vector2D Implements IGraphicsResource.Location
        Get
            Return g_Location
        End Get
        Set(ByVal value As Vector2D)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 X 좌표를 가져오거나 설정합니다.
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
    ''' 플레이어의 Y 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single Implements IGraphicsResource.Y
        Get
            Return g_Location.Y
        End Get
        Set(ByVal value As Single)
            g_Location.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 이동 속도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Speed As Int32
        Get
            Return g_Speed
        End Get
        Set(ByVal value As Int32)
            g_Speed = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Size As Drawing.Size Implements IGraphicsResource.Size
        Get
            Return g_Size
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Width As Int32 Implements IGraphicsResource.Width
        Get
            Return g_Size.Width
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Height As Int32 Implements IGraphicsResource.Height
        Get
            Return g_Size.Height
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 위치와 크기 정보를 저장하고 있는 사각형을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Rectangle As Drawing.RectangleF Implements IGraphicsResource.Rectangle
        Get
            Return New Drawing.RectangleF(g_Location, g_Size)
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Texture As D3.Texture Implements IGraphicsResource.Texture
        Get
            Return g_PlayerTexture
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single Implements IGraphicsResource.Opacity
        Get
            Return g_Opacity
        End Get
        Set(ByVal value As Single)
            g_Opacity = value
        End Set
    End Property

    ''' <summary>
    ''' 리소스의 유형을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ResourceType As ResourceTypes Implements IGraphicsResource.ResourceType
        Get
            Return ResourceTypes.Player
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 각도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Angle As Single
        Get
            Return g_Angle
        End Get
        Set(ByVal value As Single)
            If value >= 360.0F Then value = 0
            If value < 0.0F Then value = 360.0F - 0.000001F
            g_Angle = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 조작을 사용할 것인지의 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Active As Boolean
        Get
            Return g_Active
        End Get
        Set(ByVal value As Boolean)
            g_Active = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 중심 위치를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Center As Drawing.Point
        Get
            Return g_Center
        End Get
    End Property

    ''' <summary>
    ''' 플레이어의 현재 위치와 중심 위치가 더해진 위치를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property RealLocation As Vector2D
        Get
            Return g_Location + g_Center
        End Get
    End Property

    ''' <summary>
    ''' 플레이어를 그릴 때 사용할 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Color As System.Drawing.Color Implements IGraphicsResource.Color
        Get
            Return g_Color
        End Get
        Set(ByVal value As System.Drawing.Color)
            g_Color = value
        End Set
    End Property

    ''' <summary>
    ''' 그림자의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property ShadowLocation As Drawing.Point
        Get
            Return g_ShadowLocation
        End Get
        Set(ByVal value As Drawing.Point)
            g_ShadowLocation = value
        End Set
    End Property

    ''' <summary>
    ''' 그림자를 그릴 것인지의 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property ApplyShadow As Boolean
        Get
            Return g_ApplyShadow
        End Get
        Set(ByVal value As Boolean)
            g_ApplyShadow = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 위치에 따라 뷰 로케이션을 설정할 것인지를 결정하는 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property ChaseCamera As Boolean
        Get
            Return g_ChaseCam
        End Get
        Set(ByVal value As Boolean)
            g_ChaseCam = value
        End Set
    End Property

    ''' <summary>
    ''' 이동 후 충돌 검사를 할 때 사용할 사각형 영역을 가져오거나 설정합니다.
    ''' </summary>
    Public Property CollideBox As RectangleF
        Get
            Return g_CollideBox
        End Get
        Set(ByVal value As RectangleF)
            g_CollideBox = value
            If m_CollideBox IsNot Nothing Then
                m_CollideBox.Dispose()
            End If
            m_CollideBox = SDXHelper.Rectangle(MyBase.Main.Device, g_CollideBox.Width, g_CollideBox.Height, Drawing.Color.Transparent, Drawing.Color.Blue)
        End Set
    End Property

    Public ReadOnly Property CollideCheckBox As RectangleF
        Get
            Return New RectangleF(g_Location + g_CollideBox.Location, g_CollideBox.Size)
        End Get
    End Property

#Region "Access: Protected"

    ''' <summary>
    ''' 마지막으로 공격한 시간을 가져오거나 설정합니다.
    ''' </summary>
    Protected Property LastAttack As Int32
        Get
            Return g_LastAttacks
        End Get
        Set(ByVal value As Int32)
            g_LastAttacks = value
        End Set
    End Property

    ''' <summary>
    ''' 움직인 거리를 가져오거나 설정합니다.
    ''' </summary>
    Protected Property MovingDistance As Vector2D
        Get
            Return m_MoveDistance
        End Get
        Set(ByVal value As Vector2D)
            m_MoveDistance = value
        End Set
    End Property

    ''' <summary>
    ''' 움직인 X 축 거리를 가져오거나 설정합니다.
    ''' </summary>
    Protected Property MovingDistanceX As Single
        Get
            Return m_MoveDistance.X
        End Get
        Set(ByVal value As Single)
            m_MoveDistance.X = value
        End Set
    End Property

    ''' <summary>
    ''' 움직인 Y 축 거리를 가져오거나 설정합니다.
    ''' </summary>
    Protected Property MovingDistanceY As Single
        Get
            Return m_MoveDistance.Y
        End Get
        Set(ByVal value As Single)
            m_MoveDistance.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 플레이어의 키보드 상태를 가져오거나 설정합니다.
    ''' </summary>
    Protected Property CurrentKeyboardState As DI.KeyboardState
        Get
            Return g_CurrentKs
        End Get
        Set(ByVal value As DI.KeyboardState)
            g_CurrentKs = value
        End Set
    End Property

#End Region

#Region "Access: Friend"

    Public Property DebugRectangle As Boolean

#End Region

End Class