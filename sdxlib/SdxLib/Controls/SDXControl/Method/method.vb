Imports D3 = Microsoft.DirectX.Direct3D
Namespace Controls
    Partial Class SDXControl

        Public Sub SetFontDescription(ByVal fd As D3.FontDescription)

            m_Font = New D3.Font(MyBase.Main.Device, fd)
            g_FontHeight = fd.Height

        End Sub

    End Class
End Namespace