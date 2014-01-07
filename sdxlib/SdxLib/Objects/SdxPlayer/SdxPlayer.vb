' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxPlayer.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   Microsoft.DirectX.DirectInput
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
'   Defines SdxPlayer class.

Imports DI = Microsoft.DirectX.DirectInput
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Public MustInherit Class SdxPlayer
    Inherits SdxObject
    Implements IGraphicsResource, IAlphaMapSupportedGraphicsResource

    ''' <summary>
    ''' 충돌 검사를 할 때 사용할 사각형 영역을 저장합니다.
    ''' </summary>
    Friend g_CollideBox As RectangleF

    ''' <summary>
    ''' 동적 뷰 로케이션 활성 여부를 저장합니다.
    ''' </summary>
    Friend g_ChaseCam As Boolean

    ''' <summary>
    ''' 플레이어의 조작 설정을 저장합니다.
    ''' </summary>
    Friend g_Pcs As PlayerControlSettings

    ''' <summary>
    ''' 키보드 상태를 저장합니다.
    ''' </summary>
    Friend g_CurrentKs As DI.KeyboardState

    ''' <summary>
    ''' 플레이어의 위치를 저장합니다.
    ''' </summary>
    Friend g_Location As Vector2D

    ''' <summary>
    ''' 이동 속도를 저장합니다.
    ''' </summary>
    Friend g_Speed As Int32

    ''' <summary>
    ''' 플레이어의 텍스쳐를 저장합니다.
    ''' </summary>
    Friend g_PlayerTexture As D3.Texture

    ''' <summary>
    ''' 이동 거리를 저장합니다.
    ''' </summary>
    Friend m_MoveDistance As Vector2D

    ''' <summary>
    ''' 플레이어의 크기를 저장합니다.
    ''' </summary>
    Friend g_Size As Size

    ''' <summary>
    ''' 플레이어의 현재 체력을 저장합니다.
    ''' </summary>
    Friend g_CurrentHealth As Int32 = 100

    ''' <summary>
    ''' 플레이어의 최대 체력을 저장합니다.
    ''' </summary>
    Friend g_MaximumHealth As Int32 = 100

    ''' <summary>
    ''' 플레이어의 투명도를 저장합니다.
    ''' </summary>
    Friend g_Opacity As Single = 1.0F

    ''' <summary>
    ''' 플레이어의 회전 각도를 저장합니다.
    ''' </summary>
    Friend g_Angle As Single = 0

    ''' <summary>
    ''' 플레이어의 활성 여부를 저장합니다.
    ''' </summary>
    Friend g_Active As Boolean = True

    ''' <summary>
    ''' 플레이어의 공격력을 저장합니다.
    ''' </summary>
    Friend g_Damage As Int32 = 5

    ''' <summary>
    ''' 플레이어의 방어력을 저장합니다.
    ''' </summary>
    Friend g_Armor As Int32

    ''' <summary>
    ''' 플레이어의 중앙 위치를 저장합니다.
    ''' </summary>
    Friend g_Center As Point

    ''' <summary>
    ''' 텍스쳐를 불러왔는지, 이미지를 불러왔는지의 여부를 저장합니다.
    ''' </summary>
    Friend m_LoadFromImage As Boolean = False

    ''' <summary>
    ''' 플레이어를 그릴 때 사용할 색을 저장합니다.
    ''' </summary>
    Friend g_Color As Drawing.Color = Drawing.Color.White

    ''' <summary>
    ''' 그림자의 위치를 저장합니다.
    ''' </summary>
    Friend g_ShadowLocation As Point = New Point(0, 32)

    ''' <summary>
    ''' 그림자를 적용할 것인지의 여부를 저장합니다.
    ''' </summary>
    Friend g_ApplyShadow As Boolean = False

    Private g_HealthRectangle As D3.Texture = Nothing
    Private g_IgnoreBlocks As Boolean = False
    Private g_AlphaMap() As Byte = Nothing
    Friend g_Drawn As Boolean = False

    ' Attacks
    Private g_BulletHead As Point
    Private g_BulletSpeed As Vector2D
    Private g_LastAttacks As Int32

    Private g_AttackDelay As Int32 = 250
    Private g_AttackRange As Int32 = 500

    Private m_PlayerBox As D3.Texture
    Private m_CollideBox As D3.Texture

#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If m_LoadFromImage Then g_PlayerTexture.Dispose()
                If m_CollideBox IsNot Nothing Then m_CollideBox.Dispose()
                If m_PlayerBox IsNot Nothing Then m_PlayerBox.Dispose()
            End If

            ' TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 Finalize()를 재정의합니다.
            ' TODO: 큰 필드를 null로 설정합니다.
        End If
        Me.disposedValue = True
    End Sub
    ''' <summary>
    ''' 개체의 사용을 종료하고, 메모리에서 해제합니다.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    ''' <summary>
    ''' 개체가 삭제되었는지 나타내는 값을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsDisposable.Disposed
        Get
            Return disposedValue
        End Get
    End Property

#End Region

End Class