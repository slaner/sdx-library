' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBlock2D/prop.vb
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
'   Defines SdxBlock2D class's property.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Partial Class SdxBlock2D

    ''' <summary>
    ''' 투명도 맵을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property AlphaMap As Byte() Implements IAlphaMapSupportedGraphicsResource.AlphaMap
        Get
            Return g_AlphaMap
        End Get
    End Property



    ''' <summary>
    ''' 정찰 시작 지점을 가져오거나 설정합니다.
    ''' </summary>
    Public Property PatrolStartLocation As Vector2D
        Get
            Return g_PatrolStartLocation
        End Get
        Set(ByVal value As Vector2D)
            g_PatrolStartLocation = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 시작 X 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property PatrolStartX As Int32
        Get
            Return g_PatrolStartLocation.X
        End Get
        Set(ByVal value As Int32)
            g_PatrolStartLocation.X = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 시작 Y 좌표를 가져오거나 설정합니다.
    ''' </summary>
    Public Property PatrolStartY As Int32
        Get
            Return g_PatrolStartLocation.Y
        End Get
        Set(ByVal value As Int32)
            g_PatrolStartLocation.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 스텝을 가져오거나 설정합니다.
    ''' 계산식: (PatrolDistance / PatrolStep) * Step
    ''' </summary>
    Public Property PatrolStep As Int32
        Get
            Return g_PatrolStep
        End Get
        Set(ByVal value As Int32)
            g_PatrolStep = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 기능을 사용할 것인지에 대한 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property UsePatrol As Boolean
        Get
            Return g_UsePatrol
        End Get
        Set(ByVal value As Boolean)
            g_UsePatrol = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 거리를 가져오거나 설정합니다.
    ''' </summary>
    Public Property PatrolDistance As Vector2D
        Get
            Return g_Distance
        End Get
        Set(ByVal value As Vector2D)
            g_Distance = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 X 축 거리를 가져오거나 설정합니다.
    ''' </summary>
    Public Property PatriolDistanceX As Int32
        Get
            Return g_Distance.X
        End Get
        Set(ByVal value As Int32)
            g_Distance.X = value
        End Set
    End Property

    ''' <summary>
    ''' 정찰 Y 축 거리를 가져오거나 설정합니다.
    ''' </summary>
    Public Property PatriolDistanceY As Int32
        Get
            Return g_Distance.Y
        End Get
        Set(ByVal value As Int32)
            g_Distance.Y = value
        End Set
    End Property



    ''' <summary>
    ''' 블록을 그릴 때 혼합색으로 사용할 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Color As Drawing.Color Implements IGraphicsResource.Color
        Get
            Return g_Color
        End Get
        Set(ByVal value As Drawing.Color)
            g_Color = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 상태를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Flags As BlockStates
        Get
            Return g_State
        End Get
        Set(ByVal value As BlockStates)
            g_State = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 위치를 가져오거나 설정합니다.
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
    ''' 블록의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single Implements IGraphicsResource.Opacity
        Get
            Return g_Opacity
        End Get
        Set(ByVal value As Single)
            If value > 1.0 Then value = 1
            If value < 0 Then value = 0
            g_Opacity = value
        End Set
    End Property

    ''' <summary>
    ''' 블록의 X 좌표 위치를 가져오거나 설정합니다.
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
    ''' 블록의 Y 좌표 위치를 가져오거나 설정합니다.
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
    ''' 블록의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Width As Int32 Implements IGraphicsResource.Width
        Get
            Return g_Size.Width
        End Get
    End Property

    ''' <summary>
    ''' 블록의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Height As Int32 Implements IGraphicsResource.Height
        Get
            Return g_Size.Height
        End Get
    End Property

    ''' <summary>
    ''' 블록의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Size As Size Implements IGraphicsResource.Size
        Get
            Return g_Size
        End Get
    End Property

    ''' <summary>
    ''' 블록의 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Texture As D3.Texture Implements IGraphicsResource.Texture
        Get
            Return g_BlockTexture
        End Get
    End Property

    ''' <summary>
    ''' 블록의 위치와 크기를 저장하는 사각형 개체를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Rectangle As System.Drawing.RectangleF Implements IGraphicsResource.Rectangle
        Get
            Return New RectangleF(g_Location, g_Size)
        End Get
    End Property

End Class