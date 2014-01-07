Imports D3 = Microsoft.DirectX.Direct3D
Imports DI = Microsoft.DirectX.DirectInput
Imports System.Drawing

Public Class SdxMenu
    Inherits SdxObject

    Private g_Default As Image
    Private g_Hover As Image
    Private g_Pressed As Image
    Private g_Text As String
    Private g_Location As Point
    Private g_Size As Size

    Private g_Textures(2) As D3.Texture

    Public Sub New(ByVal Main As SdxMain, ByVal DefaultImage As Image, ByVal HoverImage As Image, ByVal PressedImage As Image, ByVal Text As String)
        MyBase.New(Main)

        g_Default = DefaultImage
        g_Hover = HoverImage
        g_Pressed = PressedImage
        g_Textures(0) = SdxHelper.TextureFromImage(MyBase.Main.Device, DefaultImage)
        g_Textures(1) = SdxHelper.TextureFromImage(MyBase.Main.Device, HoverImage)
        g_Textures(2) = SdxHelper.TextureFromImage(MyBase.Main.Device, PressedImage)
        g_Location = New Point(0, 0)
        g_Size = New Size(200, 30)
        g_Text = Text

    End Sub

    Friend Sub Draw(ByVal Ms As SdxMain.MouseState, ByVal Target As D3.Sprite)

        'If Ms.X > 200 Then
        '    Target.Draw2D(SdxHelper.Rectangle(MyBase.Main.Device, 200, 30), Nothing, 0, New Point(220, 220), Color.Red)
        'Else
        '    Target.Draw2D(SdxHelper.Rectangle(MyBase.Main.Device, 200, 30, Color.Gray.ToArgb), Nothing, 0, New Point(220, 220), Color.White)
        'End If

    End Sub

End Class