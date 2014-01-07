
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Public Class SdxUnit
    Implements IGraphicsDisposable

    ''' <summary>
    ''' 유닛의 종류를 정의합니다.
    ''' </summary>
    Public Enum UnitType

        ''' <summary>
        ''' 기본: 지상 유닛
        ''' </summary>
        [Default] = 1

        ''' <summary>
        ''' 지상 유닛
        ''' </summary>
        Ground = 1

        ''' <summary>
        ''' 공중 유닛
        ''' </summary>
        Air = 2

        ''' <summary>
        ''' 건물
        ''' </summary>
        Building = 4

        ''' <summary>
        ''' 벙커
        ''' </summary>
        Bunker = 8

    End Enum

    ''' <summary>
    ''' 유닛의 위치를 저장합니다.
    ''' </summary>
    Private g_Location As Vector2D = Vector2D.Empty

    ''' <summary>
    ''' 유닛의 텍스쳐를 저장합니다.
    ''' </summary>
    Private g_Texture As D3.Texture = Nothing

    ''' <summary>
    ''' 유닛의 종류를 저장합니다.
    ''' </summary>
    Private g_UnitType As UnitType = UnitType.Default

    ''' <summary>
    ''' 유닛의 기본 이동속도를 저장합니다.
    ''' </summary>
    Private g_Speed As Single = 0.0F

    Private g_Equipments As UnitEquipments




    Private m_LoadFromImage As Boolean = False


    Public Sub New(ByVal Main As SDXMain, ByVal UnitImage As Image)

        m_LoadFromImage = True
        g_Texture = D3.Texture.FromBitmap(Main.Device, UnitImage, 0, 1)

    End Sub

    Public ReadOnly Property CurrentHealth As Int32
        Get
            Return 0
        End Get
    End Property
    Public ReadOnly Property Health As Int32
        Get
            Return g_Equipments.Weapon.HealthBonus
        End Get
    End Property

#Region "IGraphicsDisposable"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If m_LoadFromImage Then g_Texture.Dispose()
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