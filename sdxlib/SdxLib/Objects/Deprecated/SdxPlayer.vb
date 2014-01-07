Imports D3 = Microsoft.DirectX.Direct3D
Imports DI = Microsoft.DirectX.DirectInput
Imports System.Drawing

Public Class SdxPlayerx
    Inherits SdxGraphicsObject
    Implements IGraphicsResource

    ''' <summary>
    ''' 사용자 조작 개체의 상태를 나타내는 값들을 열거합니다.
    ''' </summary>
    Public Enum PlayerStateFlags

        ' 기본 상태
        None = &H0

        ' 이로운 효과
        GE_PowerUp = &H1            ' 공격력 증가
        GE_ArmorUp = &H2            ' 방어력 증가
        GE_SpeedUp = &H4            ' 이동속도 증가
        GE_AtkSpeedUp = &H8         ' 공격속도 증가
        GE_HealthUp = &H10          ' 최대 체력 증가
        GE_StaminaUp = &H20         ' 최대 지구력 증가

        ' 플레이어 위치 상태
        PS_Water = &H40             ' 수면
        PS_Ground = &H80            ' 지상
        PS_Air = &H100              ' 공중

        ' 해로운 효과
        BE_PowerDown = &H200        ' 공격력 감소
        BE_ArmorDown = &H400        ' 방어력 감소
        BE_SpeedDown = &H800        ' 이동속도 감소
        BE_AtkSpeedDown = &H1000    ' 공격속도 감소
        BE_HealthDown = &H2000      ' 최대 체력 감소
        BE_StaminaDown = &H4000     ' 최대 지구력 감소
        BE_Poison = &H8000          ' 초당 피해
        BE_Freeze = &H10000         ' 이동속도 감소, 행동 시 피해
        BE_Burn = &H20000           ' 초당 피해, 방어력 감소
        BE_Confuse = &H40000        ' 조작 반대
        BE_Restrict = &H80000       ' 움직임 불가, 점프 불가
        BE_Stun = &H100000          ' 행동 불가

    End Enum

#Region " - Fields - "

    ''' <summary>
    ''' 사용자 조작 개체의 상태를 저장합니다.
    ''' </summary>
    Private g_State As PlayerStateFlags

    ''' <summary>
    ''' 사용자 조작 개체의 텍스쳐를 저장합니다.
    ''' </summary>
    Private g_MainTexture As SdxTexture2D

    Private m_Blocks As List(Of SdxBlock2D)

    Private m_MoveDistance As Point

#End Region

#Region " - Properties - "

    ''' <summary>
    ''' 사용자 조작 개체가 점프를 하고 있는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property IsJumping As Boolean
        Get
            Return g_Jumping
        End Get
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 상태를 가져오거나 설정합니다.
    ''' </summary>
    Public Property State As PlayerStateFlags
        Get
            Return g_State
        End Get
        Set(ByVal value As PlayerStateFlags)
            g_State = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 각도를 가져오거나 설정합니다. (Degree)
    ''' </summary>
    Public Property Angle As Single
        Get
            Return g_MainTexture.Angle
        End Get
        Set(ByVal value As Single)
            If value >= 360 Then value = value Mod 360
            If value < 0 Then value = 360 - (value Mod 360)
            g_MainTexture.Angle = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 이동 속도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Speed As Single
        Get
            Return g_MainTexture.Speed
        End Get
        Set(ByVal value As Single)
            g_MainTexture.Speed = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 회전 중심 축을 가져오거나 설정합니다.
    ''' </summary>
    Public Property RotateOrigin As Point
        Get
            Return g_MainTexture.RotateOrigin
        End Get
        Set(ByVal value As Point)
            g_MainTexture.RotateOrigin = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As PointF Implements IGraphicsResource.Location
        Get
            Return g_MainTexture.Location
        End Get
        Set(ByVal value As PointF)
            g_MainTexture.Location = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single Implements IGraphicsResource.Opacity
        Get
            Return g_MainTexture.Opacity
        End Get
        Set(ByVal value As Single)
            If value > 1.0 Then value = 1
            If value < 0 Then value = 0
            g_MainTexture.Opacity = value
        End Set
    End Property

    ''' <summary>
    ''' 블록을 그릴 때 혼합할 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Color As Color Implements IGraphicsResource.Color
        Get
            Return g_MainTexture.Color
        End Get
        Set(ByVal value As Color)
            g_MainTexture.Color = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 X 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Int32 Implements IGraphicsResource.X
        Get
            Return g_MainTexture.X
        End Get
        '<Obsolete("이 속성을 사용하여, 값을 대입할 수 없습니다.", True)> _
        Set(ByVal value As Int32)
            g_MainTexture.X = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 Y 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Int32 Implements IGraphicsResource.Y
        Get
            Return g_MainTexture.Y
        End Get
        '<Obsolete("이 속성을 사용하여, 값을 대입할 수 없습니다.", True)> _
        Set(ByVal value As Int32)
            g_MainTexture.Y = value
        End Set
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 넓이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Width As Int32 Implements IGraphicsResource.Width
        Get
            Return g_MainTexture.Width
        End Get
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Height As Int32 Implements IGraphicsResource.Height
        Get
            Return g_MainTexture.Height
        End Get
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 크기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Size As Size Implements IGraphicsResource.Size
        Get
            Return g_MainTexture.Size
        End Get
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Texture As D3.Texture Implements IGraphicsResource.Texture
        Get
            Return g_MainTexture.Texture
        End Get
    End Property

    ''' <summary>
    ''' 사용자 조작 개체의 위치와 크기를 저장하는 사각형 개체를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Rectangle As System.Drawing.Rectangle Implements IGraphicsResource.Rectangle
        Get
            Return New Rectangle(g_MainTexture.Location, g_MainTexture.Size)
        End Get
    End Property

    ''' <summary>
    ''' 사용자 조작 개체가 지상에 있는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property InGround As Boolean
        Get
            Return g_State And PlayerStateFlags.PS_Ground
        End Get
    End Property

#End Region

#Region " - Constructor - "

    ''' <summary>
    ''' 사용자 조작 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Texture">사용자 조작에 사용될 2D 텍스쳐를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Texture As SdxTexture2D, ByVal Blocks As List(Of SdxBlock2D))
        MyBase.New(Device)
        g_MainTexture = Texture
        'g_RectangleTexture = D3.Texture.FromBitmap(Me.GraphicsDevice, SdxTexture2DManager.Rectangle(Me.Size, Drawing.Color.Lime), 0, 1)
        m_Blocks = Blocks
    End Sub

#End Region

    Private g_RectangleTexture As D3.Texture

    '### Stats
    Private g_Health As Int32
    Private g_Mana As Int32
    Private g_Exp As UInt64
    Private g_MaxExp As UInt64


    '### Status Package
    Private g_StatusPackage As New Dictionary(Of String, Int32)

    '### Say
    Private g_CanSay As Boolean
    Private g_Saying As Boolean

    '### Move/Sprint
    Private g_CanMove As Boolean
    Private g_Moving As Boolean
    Private g_CanSprint As Boolean
    Private g_Sprinting As Boolean

    '### Attack/Guard
    Private g_CanAttack As Boolean
    Private g_Attacking As Boolean
    Private g_CanGuard As Boolean
    Private g_Guarding As Boolean

    '### Texture
    Private g_ActionTexture As New List(Of SdxTexture2D)

    Private g_SubTextures As List(Of SdxTexture2D)
    Private m_Count As Int32
    Private Const LIMITATION As Int32 = 100
    Private Const GRAVITY_SPEED As Int32 = 4

    Private m_LastLocation As Point

    ' Jump
    Private Const JUMP_POWER As Int32 = 7
    Private Const JUMP_STEP As Int32 = 5
    Private Const JUMP_COUNT As Int32 = 10000
    Private Const JUMP_LIMIT As Int32 = 180
    Private g_RemainJumpCount As Int32 = JUMP_COUNT
    Private g_Jumping As Boolean
    Private m_JumpCount As Int32
    Private m_CanJump As Boolean = True
    Private m_CurrentStep As Int32
    Private m_GoingUp As Boolean = False
    Private m_Falling As Boolean = False
    Private m_JumpStep As Int32

    Public ReadOnly Property JUMPCOUNT As Int32
        Get
            Return g_RemainJumpCount
        End Get
    End Property

#Region " - Actions - "

    Public Sub Jump()

        ' 공중에 떠있거나, 수면이거나, 속박 상태인 경우,
        ' 점프를 하지 않는다.
        If g_State And (PlayerStateFlags.BE_Restrict Or PlayerStateFlags.PS_Water) Then Return

        ' 점프 카운트가 남아있지 않으면, 점프를 하지 않는다.
        If g_RemainJumpCount <= 0 Then Return

        If m_CanJump Then

            ' 점프중이라는 것을 알린다.
            g_Jumping = True
            m_JumpCount = 0
            g_RemainJumpCount -= 1

        End If

    End Sub

#End Region

#Region " - Private Methods - "

    Private Function TouchedDeadBlock(ByVal b As SdxBlock2D) As Boolean
        If b.Flags And SdxBlock2D.BlockStateFlags.Dead Then
            g_MainTexture.Location = Point.Empty
            Return True
        End If
        Return False
    End Function
    Private Function ProcessJump(Optional ByVal Jpower As Int32 = JUMP_POWER) As Boolean

        '' 떨어지고 있는 경우,
        'If m_Falling Then

        '    Dim RStep As Int32 = Math.Abs(m_CurrentStep - 90) + 90
        '    If RStep > JUMP_LIMIT Then
        '        If InGround Then
        '            Debug.Print("Falling Ends")
        '            g_Jumping = False
        '            m_Falling = False
        '            Return True
        '        Else
        '            Debug.Print("Keep Down")
        '            m_MoveDistance.Y -= Math.Round(Math.Cos(D3.Geometry.DegreeToRadian(180)) * Jpower)
        '            Return False
        '        End If
        '    End If
        '    m_MoveDistance.Y -= Math.Round(Math.Cos(D3.Geometry.DegreeToRadian(RStep)) * Jpower)
        '    m_CurrentStep += 5
        '    Return False

        'End If

        ' 사용자 조작 개체가 점프중인 경우,
        If IsJumping Then

            ' 180도에 도달했다면, 점프가 끝난 것이다!
            If m_JumpCount > JUMP_LIMIT Then

                ' 하지만, 180 을 넘어서도 땅에 닿지 않았을 경우:
                If Not InGround Then
                    m_MoveDistance.Y -= Math.Round(Math.Cos(D3.Geometry.DegreeToRadian(180)) * Jpower)
                    Return False
                Else
                    g_Jumping = False
                    m_CanJump = True
                    Return True
                End If

            End If

            m_MoveDistance.Y -= Math.Round(Math.Cos(D3.Geometry.DegreeToRadian(m_JumpCount)) * Jpower)
            m_JumpCount += JUMP_STEP
            Return False

        End If

        Return True

    End Function

#End Region

#Region " - Public Methods - "

    Public Sub MoveTo(ByVal XCoordinate As Boolean, Optional ByVal Distance As Int32 = GRAVITY_SPEED)

        If XCoordinate Then
            m_MoveDistance.X += Distance
        Else
            m_MoveDistance.Y += Distance
        End If

    End Sub

    Public Sub Draw(ByVal Target As D3.Sprite)

        ' 변수 선언:
        ' 1. 연산을 하기 전의 텍스쳐 위치를 저장
        ' 2. 충돌이 발생한 개체를 저장
        ' 3. 밑으로 내려가고 있는지, 올라가고 있는지의 여부를 저장
        Dim pPrev As Point = g_MainTexture.Location,
            gTouched As IGraphicsResource = Nothing,
            bGoingDown = False

        ' 점프가 끝난 상태라면, True 를 반환하므로
        ' 중력을 적용한다.
        If ProcessJump() Then m_MoveDistance.Y += GRAVITY_SPEED


        ' 이동 방향이 있을 경우에만 검사한다.
        If Not m_MoveDistance.IsEmpty Then

            ' X 좌표 움직임을 적용한다.
            g_MainTexture.X += m_MoveDistance.X

            ' 블록과의 충돌이 발생한 경우, 이전 위치로 되돌린다.
            If CheckCollisions(gTouched) Then

                If m_MoveDistance.X > 0 Then
                    ' 오른쪽으로 이동한 경우,
                    pPrev.X = gTouched.X - g_MainTexture.Width
                Else
                    ' 왼쪽으로 이동한 경우,
                    pPrev.X = gTouched.X + g_MainTexture.Width
                End If
                g_MainTexture.X = pPrev.X

                ' 데드 블록이랑 충돌이 일어났는지 확인한다.
                If TouchedDeadBlock(gTouched) Then GoTo DrawRoutine

            End If

            ' Y 좌표 움직임을 적용한다.
            g_MainTexture.Y += m_MoveDistance.Y

            ' 블록과의 충돌이 발생한 경우,
            If CheckCollisions(gTouched) Then

                If m_MoveDistance.Y > 0 Then
                    ' 밑으로 이동한 경우,
                    pPrev.Y = gTouched.Y - g_MainTexture.Height
                    bGoingDown = True

                    ' 점프 중, 충돌이 발생한 경우:
                    If IsJumping Then
                        m_CanJump = True
                        g_Jumping = False
                    End If
                Else
                    ' 위로 이동한 경우,
                    pPrev.Y = gTouched.Y + g_MainTexture.Height
                    bGoingDown = False
                    ' 점프중에 부딪힌 경우:
                    If IsJumping Then
                        m_JumpCount = Math.Abs(m_JumpCount - 90) + 90
                    End If
                End If
                g_MainTexture.Y = pPrev.Y

                ' 데드 블록이랑 충돌이 일어났는지 확인한다.
                If TouchedDeadBlock(gTouched) Then GoTo DrawRoutine

            End If

            ' 이동한 거리를 0으로 초기화한다.
            m_MoveDistance = Point.Empty

        End If

DrawRoutine:

        Target.Draw2D(Me.Texture, Me.RotateOrigin, Me.Angle, Me.Location + Me.RotateOrigin, Me.Color)
        Target.Draw2D(g_RectangleTexture, PointF.Empty, 0, Me.Location, Drawing.Color.White)

        If m_LastLocation.Y = g_MainTexture.Y Then

            If Not g_Jumping Then
                ' 점프 중이 아닌 경우:
                If bGoingDown Then
                    ' 밑으로 내려가고 있는 경우:
                    g_State = (g_State And (Not PlayerStateFlags.PS_Air)) Or PlayerStateFlags.PS_Ground
                    g_RemainJumpCount = JUMP_COUNT
                    m_CanJump = True
                End If
            Else
                ' 점프 중인 경우, 점프 카운트를 계산해서 지상인지 공중인지를 처리한다.

            End If
        Else
            If g_State And PlayerStateFlags.PS_Ground Then
                g_State = (g_State And (Not PlayerStateFlags.PS_Ground)) Or PlayerStateFlags.PS_Air
            End If
        End If

        m_LastLocation = g_MainTexture.Location

    End Sub

    Private Function CheckCollisions(ByRef TouchedObject As IGraphicsResource) As Boolean
        Return HasCollisions(m_Blocks.ToArray, TouchedObject)
    End Function

    Public Function HasCollision(ByVal [object] As IGraphicsResource) As Boolean
        Return g_MainTexture.HasCollision([object])
    End Function

    Public Function HasCollisions(ByVal [objects]() As IGraphicsResource, Optional ByRef TouchedObject As IGraphicsResource = Nothing) As Boolean
        Return g_MainTexture.HasCollisions(objects, TouchedObject)
    End Function

#End Region

    Public ReadOnly Property Status(ByVal Name As String) As Int32
        Get
            If Not g_StatusPackage.ContainsKey(Name) Then Return -1
            Return g_StatusPackage(Name)
        End Get
    End Property

#Region " - IGraphicsResource - "

    Private g_Disposed As Boolean = False

    ''' <summary>
    ''' 리소스의 사용을 종료하고, 메모리에서 해제합니다.
    ''' </summary>
    Public Sub Dispose() Implements IGraphicsResource.Dispose

        If g_Disposed Then Exit Sub
        If g_MainTexture IsNot Nothing Then g_MainTexture.Dispose()
        g_Disposed = True

    End Sub

    ''' <summary>
    ''' 리소스의 사용이 종료되고, 메모리에서 해제되었는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsResource.Disposed
        Get
            Return g_Disposed
        End Get
    End Property

#End Region

    Public ReadOnly Property ResourceType As SdxMain.ResourceTypes Implements IGraphicsResource.ResourceType
        Get

        End Get
    End Property
End Class