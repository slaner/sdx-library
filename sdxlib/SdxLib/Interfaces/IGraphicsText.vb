Imports System.Drawing
''' <summary>
''' 렌더링 가능한 텍스트를 나타냅니다.
''' </summary>
Public Interface IGraphicsText
    Inherits IDisposable

    ''' <summary>
    ''' 비트맵 폰트를 지원하는지의 여부를 가져옵니다.
    ''' </summary>
    ReadOnly Property SupportsBitmapFont As Boolean

    ''' <summary>
    ''' 텍스트의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Property Opacity As Single

    ''' <summary>
    ''' 텍스트의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Property Location As Vector2D

    ''' <summary>
    ''' 텍스트의 X 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Property X As Single

    ''' <summary>
    ''' 텍스트의 Y 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Property Y As Single

    ''' <summary>
    ''' 텍스트를 가져오거나 설정합니다.
    ''' </summary>
    Property Text As String

    ''' <summary>
    ''' 텍스트의 색을 가져오거나 설정합니다.
    ''' </summary>
    Property ForeColor As Drawing.Color

    ''' <summary>
    ''' 텍스트를 그리는 작업을 구현합니다.
    ''' </summary>
    Sub Draw()

End Interface