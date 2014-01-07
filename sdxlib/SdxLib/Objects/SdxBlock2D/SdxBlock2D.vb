' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBlock2D.vb
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
'   Defines SdxBlock2D class's body. (internal variables and interface implements)

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

''' <summary>
''' 2D 블록을 그래픽 대상 개체에 출력하는 작업을 구현합니다.
''' </summary>
Public Class SdxBlock2D
    Inherits SdxObject
    Implements IGraphicsResource, IAlphaMapSupportedGraphicsResource

#Region " - Fields - "

    Private g_Location As Vector2D = Vector2D.Empty
    Private g_BlockTexture As D3.Texture = Nothing
    Private g_Size As Size = Size.Empty
    Private g_Opacity As Single = 1.0F
    Private g_State As BlockStates = BlockStates.Default
    Private g_Color As Color = Drawing.Color.White
    Private g_StateOption As Int32 = 0

    ' Alpha Map
    Private g_AlphaMap() As Byte = Nothing

    ' Patrol
    Private g_PatrolStartLocation As Vector2D = Vector2D.Empty
    Private g_Distance As Vector2D = Vector2D.Empty
    Private g_UsePatrol As Boolean = False
    Private g_PatrolStep As Int32 = 0
    Private m_CurrentStep As Int32 = 0
    Private m_GoingBack As Boolean = False
    Private m_LoadFromImage As Boolean = False

#End Region

    ''' <summary>
    ''' 블록의 상태를 나타내는 값들을 열거합니다.
    ''' </summary>
    Public Enum BlockStates

        [Default] = &H8         ' 땅 블록
        HealthDamage = &H0      ' 체력 피해 블럭
        HealthRestore = &H1     ' 체력 회복 블럭
        StaminaDamage = &H2     ' 지구력 피해 블럭
        StaminaRestore = &H4    ' 지구력 회복 블럭
        Wall = &H8            ' 땅 블럭 (이 블럭과 충돌이 발생할 경우, 움직임 제한)
        Slow = &H10       ' 이동속도 감소 블럭
        Dead = &H20             ' 사망 블럭

    End Enum

#Region " - IGraphicsResources - "

#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If m_LoadFromImage Then g_BlockTexture.Dispose()
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

    ''' <summary>
    ''' 리소스의 유형을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ResourceType As ResourceTypes Implements IGraphicsResource.ResourceType
        Get
            Return ResourceTypes.Block
        End Get
    End Property

#End Region

End Class