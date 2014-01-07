Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Public Class SdxTileset2D
    Inherits SdxGraphicsObject
    Implements IGraphicsResource

    Private g_Texture As D3.Texture
    Private g_Size As Drawing.Size

    Friend Sub New(ByVal Device As D3.Device, ByVal Img As Drawing.Image)

        MyBase.New(Device)

        ' 이미지를 스트림에 저장한다.
        Dim imgStream As New IO.MemoryStream
        Img.Save(imgStream, Img.RawFormat)
        imgStream.Position = 0

        ' 스트림을 텍스쳐로 변환한다.
        g_Texture = D3.TextureLoader.FromStream(Me.GraphicsDevice, imgStream)

        ' 스트림을 삭제한다.
        imgStream.Dispose()

        ' 크기를 저장한다.
        g_Size = Img.Size

    End Sub



#Region " - IGraphicsResources - "

    Private g_Disposed As Boolean = False
    Public Sub Dispose() Implements IGraphicsResource.Dispose

        If g_Disposed Then Exit Sub
        If g_Texture IsNot Nothing Then g_Texture.Dispose()
        g_Disposed = True

    End Sub
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsResource.Disposed
        Get
            Return g_Disposed
        End Get
    End Property

#End Region

    Public Property Color As System.Drawing.Color Implements IGraphicsResource.Color
        Get

        End Get
        Set(ByVal value As System.Drawing.Color)

        End Set
    End Property

    Public ReadOnly Property Height As Integer Implements IGraphicsResource.Height
        Get

        End Get
    End Property

    Public Property Location As System.Drawing.PointF Implements IGraphicsResource.Location
        Get

        End Get
        Set(ByVal value As System.Drawing.PointF)

        End Set
    End Property

    Public Property Opacity As Single Implements IGraphicsResource.Opacity
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public ReadOnly Property Rectangle As System.Drawing.RectangleF Implements IGraphicsResource.Rectangle
        Get

        End Get
    End Property

    Public ReadOnly Property Size As System.Drawing.Size Implements IGraphicsResource.Size
        Get

        End Get
    End Property

    Public ReadOnly Property Texture As Microsoft.DirectX.Direct3D.Texture Implements IGraphicsResource.Texture
        Get

        End Get
    End Property

    Public ReadOnly Property Width As Integer Implements IGraphicsResource.Width
        Get

        End Get
    End Property

    Public Property X As Single Implements IGraphicsResource.X
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public Property Y As Single Implements IGraphicsResource.Y
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public ReadOnly Property ResourceType As ResourceTypes Implements IGraphicsResource.ResourceType
        Get

        End Get
    End Property
End Class