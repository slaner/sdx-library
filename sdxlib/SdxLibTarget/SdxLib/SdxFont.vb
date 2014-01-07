Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Public Class SdxFont
    Inherits SdxGraphicsObject

    ''' <summary>
    ''' 폰트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    Public Sub New(ByVal Device As D3.Device)

        MyBase.New(Device)

    End Sub

End Class