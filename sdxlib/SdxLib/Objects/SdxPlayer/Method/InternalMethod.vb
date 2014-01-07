
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxPlayer

    Private Sub Initialize(ByVal Img As Image, Optional ByVal ControlSet As String = vbNullString)

        m_LoadFromImage = True
        g_AlphaMap = SDXHelper.CreateAlphaMap(Img)
        g_PlayerTexture = SDXHelper.TextureFromImage(MyBase.Main.Device, Img)
        g_Size = Img.Size
        g_Center = New Point(Img.Width / 2, Img.Height / 2)
        g_CollideBox = New RectangleF(PointF.Empty, g_Size)
        m_PlayerBox = SDXHelper.Rectangle(MyBase.Main.Device, g_Size.Width, g_Size.Height, Drawing.Color.Transparent, Drawing.Color.Red)

        If String.IsNullOrEmpty(ControlSet) Then
            g_Pcs = New PlayerControlSettings(My.Resources.DefaultControlSet, True)
        Else
            g_Pcs = New PlayerControlSettings(ControlSet)
        End If

    End Sub

    Private Sub Initialize(ByVal Texture As D3.Texture, ByVal AlphaMap() As Byte, Optional ByVal ControlSet As String = vbNullString)

        m_LoadFromImage = False
        g_AlphaMap = AlphaMap
        g_PlayerTexture = Texture
        g_Size = New Size(g_PlayerTexture.GetLevelDescription(0).Width, g_PlayerTexture.GetLevelDescription(0).Height)
        g_Center = New Point(g_Size.Width / 2, g_Size.Height / 2)
        g_CollideBox = New RectangleF(PointF.Empty, g_Size)
        m_PlayerBox = SDXHelper.Rectangle(MyBase.Main.Device, g_Size.Width, g_Size.Height, Drawing.Color.Transparent, Drawing.Color.Red)

        If String.IsNullOrEmpty(ControlSet) Then
            g_Pcs = New PlayerControlSettings(My.Resources.DefaultControlSet, True)
        Else
            g_Pcs = New PlayerControlSettings(ControlSet)
        End If

    End Sub

End Class