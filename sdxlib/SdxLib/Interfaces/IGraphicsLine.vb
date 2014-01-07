Imports System.Drawing
''' <summary>
''' 선을 구현하는 리소스를 나타냅니다.
''' </summary>
Public Interface IGraphicsLine
    Inherits IGraphicsDisposable

    Property Start As Vector2D
    Property [End] As Vector2D
    Property Color As Color
    Property Thickness As Int32

    Property FadeType As FadeType
    Property FadeEffect As FadeEffect
    Property FadeSteps As Int32
    Property Preserve As Boolean
    Property ToggleFadeEffect As Boolean

    ReadOnly Property SupportsTextureMapping As Boolean

    Sub Draw(ByVal Target As Object)

End Interface