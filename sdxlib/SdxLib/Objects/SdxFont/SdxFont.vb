' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxFont.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  0
'
' Date:
'   2013/12/06
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxFont class's body. (internal variables and interface implements)

Imports D3 = Microsoft.DirectX.Direct3D

''' <summary>
''' DirectX 그리기 전용 폰트를 저장하고, 그래픽 대상 개체에 출력하는 작업을 구현합니다.
''' </summary>
Public Class SdxFont
    Inherits SdxObject
    Implements IGraphicsDisposable

    Private m_Font As D3.Font = Nothing
    Private g_UseTextSprite As Boolean = True

#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                m_Font.Dispose()
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