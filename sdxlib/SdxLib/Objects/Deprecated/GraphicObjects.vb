Public Class SdxGraphicsObject

    ''' <summary>
    ''' DirectX 장치를 저장합니다.
    ''' </summary>
    Private m_Device As Microsoft.DirectX.Direct3D.Device

    ''' <summary>
    ''' 그래픽 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    Friend Sub New(ByVal Device As Microsoft.DirectX.Direct3D.Device)

        m_Device = Device

    End Sub

    ''' <summary>
    ''' DirectX 장치를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property GraphicsDevice As Microsoft.DirectX.Direct3D.Device
        Get
            Return m_Device
        End Get
    End Property

End Class