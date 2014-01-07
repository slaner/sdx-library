Imports System.Drawing
Public Class SdxLine2D
    Inherits SdxObject
    Implements IGraphicsLine

    ''' <summary>
    ''' 선의 시작 점을 저장합니다.
    ''' </summary>
    Private g_Start As Vector2D = Vector2D.Empty

    ''' <summary>
    ''' 선의 끝 점을 저장합니다.
    ''' </summary>
    Private g_Ends As Vector2D = Vector2D.Empty

    ''' <summary>
    ''' 선의 색을 저장합니다.
    ''' </summary>
    Private g_Color As Drawing.Color = Drawing.Color.White

    ''' <summary>
    ''' 선의 투명도를 저장합니다.
    ''' </summary>
    Private g_Opacity As Single = 1.0F

    ''' <summary>
    ''' 페이드 효과를 사용할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_FadeType As FadeType = SDXLib.FadeType.None

    ''' <summary>
    ''' 페이드 효과의 유형을 저장합니다.
    ''' </summary>
    Private g_FadeEffect As FadeEffect = FadeEffect.FadeOut

    ''' <summary>
    ''' 페이드 효과의 단계 수를 저장합니다.
    ''' </summary>
    Private g_FadeStep As Int32 = 60

    ''' <summary>
    ''' 페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_Preserve As Boolean = True

    ''' <summary>
    ''' 페이드 효과가 끝난 후에 개체를 보존할 경우, 페이드 효과를 번갈아가면서 적용할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_ToggleFadeEffect As Boolean = False

    ''' <summary>
    ''' 선의 두께를 저장합니다.
    ''' </summary>
    Private g_Thickness As Int32 = 1

    ''' <summary>
    ''' 안티 앨리어싱을 사용할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_AntiAlias As Boolean = False

    Private m_CurrentStep As Int32 = 0                  ' 페이드 효과의 단계를 저장합니다.
    Private m_FadeStep As Single = 0.0F                 ' 페이드 효과의 단계별 값을 저장합니다. (프레임 기반)
    Private m_FadeAmount As Single = 0.0F               ' 페이드 효과의 단계별 값을 저장합니다. (삼각함수 기반)



#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                ' do nothing
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