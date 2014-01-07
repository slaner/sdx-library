' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxEffect.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
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
'   Defines SdxEffect class body.

' NOTE:
'   기본 클래스로만 사용할 수 있는 클래스의 멤버들은,
'   파생 클래스에서 멤버 접근을 가능하게 하기 위해 Friend 로 선언한다.
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

''' <summary>
''' 효과를 나타내는 클래스입니다.
''' </summary>
Public MustInherit Class SdxEffect
    Inherits SdxObject
    Implements IGraphicsEffect

    ''' <summary>
    ''' 전체 텍스쳐의 크기를 저장합니다.
    ''' </summary>
    Friend g_Size As Size

    ''' <summary>
    ''' 효과의 위치를 저장합니다.
    ''' </summary>
    Friend g_Location As Vector2D

    ''' <summary>
    ''' 개별 효과의 크기를 저장합니다.
    ''' </summary>
    Friend g_ParticleSize As Size

    ''' <summary>
    ''' 개별 효과의 갯수를 저장합니다.
    ''' </summary>
    Friend g_ParticleCount As Int32

    ''' <summary>
    ''' 효과의 텍스쳐를 저장합니다.
    ''' </summary>
    Friend g_EffectTexture As D3.Texture

    ''' <summary>
    ''' 렌더링할 크기를 저장합니다.
    ''' </summary>
    Friend g_RenderingSize As Size

    ''' <summary>
    ''' 렌더링될 파티클의 번호를 저장합니다.
    ''' </summary>
    Friend g_CurrentParticle As Int32

    ''' <summary>
    ''' 줄 당 파티클의 갯수를 저장합니다.
    ''' </summary>
    Friend g_ParticlePerLine As Int32

    ''' <summary>
    ''' 이미지로부터 텍스쳐를 생성하는지의 여부를 저장합니다.
    ''' </summary>
    Friend m_LoadFromImage As Boolean = False



#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If m_LoadFromImage Then g_EffectTexture.Dispose()
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