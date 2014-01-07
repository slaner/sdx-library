Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

''' <summary>
''' 공유되는 리소스를 관리하는 클래스입니다. 이 클래스는 상속될 수 없습니다.
''' </summary>
Public NotInheritable Class SdxSharedResource
    Inherits SdxObject
    Implements IDisposable

    Private g_TransparentMask As D3.Texture
    Private g_ColorMask As D3.Texture

    ''' <summary>
    ''' 공유되는 리소스를 관리하는 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    Friend Sub New(ByVal Main As SDXMain)
        MyBase.New(Main)

        g_ColorMask = D3.Texture.FromBitmap(MyBase.Main.Device, My.Resources.ColorMask, 0, 1)

    End Sub

    ''' <summary>
    ''' FOR COMPILE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DefaultExplosion64 As D3.Texture
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' 색상 마스크 텍스쳐를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property ColorMask As D3.Texture
        Get
            Return g_ColorMask
        End Get
    End Property

#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If Not IsNothing(g_ColorMask) Then g_ColorMask.Dispose()
            End If

            ' TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 Finalize()를 재정의합니다.
            ' TODO: 큰 필드를 null로 설정합니다.
        End If
        Me.disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class