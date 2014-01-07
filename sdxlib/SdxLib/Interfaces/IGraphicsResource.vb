Imports System.Drawing
Imports D3 = Microsoft.DirectX.Direct3D

''' <summary>
''' 메모리에서 해제될 수 있고, 텍스쳐와 위치 및 투명도 등의 정보를 포함하는 그래픽 리소스를 나타냅니다.
''' </summary>
Public Interface IGraphicsResource
    Inherits IGraphicsDisposable

    ''' <summary>
    ''' 리소스의 위치와 크기 정보를 저장하는 사각형 개체를 가져옵니다.
    ''' </summary>
    ReadOnly Property Rectangle As RectangleF

    ''' <summary>
    ''' 리소스의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Property Location As Vector2D

    ''' <summary>
    ''' 리소스의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Property Opacity As Single

    ''' <summary>
    ''' 블록을 그릴 때 혼합할 색을 가져오거나 설정합니다.
    ''' </summary>
    Property Color As Color

    ''' <summary>
    ''' 리소스의 X 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Property X As Single

    ''' <summary>
    ''' 리소스의 Y 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Property Y As Single

    ''' <summary>
    ''' 리소스의 넓이를 가져옵니다.
    ''' </summary>
    ReadOnly Property Width As Int32

    ''' <summary>
    ''' 리소스의 높이를 가져옵니다.
    ''' </summary>
    ReadOnly Property Height As Int32

    ''' <summary>
    ''' 리소스의 크기를 가져옵니다.
    ''' </summary>
    ReadOnly Property Size As Size

    ''' <summary>
    ''' 리소스의 텍스쳐를 가져옵니다.
    ''' </summary>
    ReadOnly Property Texture As D3.Texture

    ''' <summary>
    ''' 리소스의 유형을 가져옵니다.
    ''' </summary>
    ReadOnly Property ResourceType As ResourceTypes

End Interface