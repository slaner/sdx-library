Imports D3 = Microsoft.DirectX.Direct3D
Public Class SdxMap
    Inherits SdxObject
    Implements IGraphicsDisposable

    Private m_LoadFromImage As Boolean
    Private g_Texture As D3.Texture

    Public Sub New(ByVal Main As SDXMain)
        MyBase.New(Main)
    End Sub



#Region "IDisposable Support"

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