Imports System.Drawing

''' <summary>
''' 2차원(x, y) 좌표계를 나타냅니다.
''' </summary>
Public Structure Vector2D

    ''' <summary>
    ''' X 좌표 값을 저장합니다.
    ''' </summary>
    Private g_X As Single

    ''' <summary>
    ''' Y 좌표 값을 저장합니다.
    ''' </summary>
    Private g_Y As Single

    ''' <summary>
    ''' X, Y 좌표의 값을 이용하여 2차원 좌표계를 나타내는 Vector2D 구조체를 초기화합니다.
    ''' </summary>
    ''' <param name="x">X 좌표의 값을 입력합니다.</param>
    ''' <param name="y">Y 좌표의 값을 입력합니다.</param>
    Public Sub New(ByVal x As Single, ByVal y As Single)

        g_X = x
        g_Y = y

    End Sub

#Region "Static Member"

    ''' <summary>
    ''' 빈 Vector2D 구조체를 가져옵니다.
    ''' </summary>
    Public Shared ReadOnly Property Empty As Vector2D
        Get
            Return Nothing
        End Get
    End Property

#End Region

#Region "Operator"

    Public Shared Operator =(ByVal v1 As Vector2D, ByVal v2 As Vector2D) As Boolean

        Return v1.X = v2.X AndAlso v1.Y = v2.Y

    End Operator
    Public Shared Operator <>(ByVal v1 As Vector2D, ByVal v2 As Vector2D) As Boolean

        Return v1.X <> v2.X OrElse v1.Y <> v2.Y

    End Operator

    Public Shared Operator +(ByVal source As Vector2D, ByVal num As Int32) As Vector2D

        Return New Vector2D(source.X + num, source.Y + num)

    End Operator
    Public Shared Operator +(ByVal source As Vector2D, ByVal num As Single) As Vector2D

        Return New Vector2D(source.X + num, source.Y + num)

    End Operator
    Public Shared Operator +(ByVal source As Vector2D, ByVal p As Point) As Vector2D

        Return New Vector2D(source.X + p.X, source.Y + p.Y)

    End Operator
    Public Shared Operator +(ByVal source As Vector2D, ByVal p As Vector2D) As Vector2D

        Return New Vector2D(source.X + p.X, source.Y + p.Y)

    End Operator

    Public Shared Operator -(ByVal source As Vector2D, ByVal num As Int32) As Vector2D

        Return New Vector2D(source.X - num, source.Y - num)

    End Operator
    Public Shared Operator -(ByVal source As Vector2D, ByVal num As Single) As Vector2D

        Return New Vector2D(source.X - num, source.Y - num)

    End Operator
    Public Shared Operator -(ByVal source As Vector2D, ByVal p As Point) As Vector2D

        Return New Vector2D(source.X - p.X, source.Y - p.Y)

    End Operator
    Public Shared Operator -(ByVal source As Vector2D, ByVal p As Vector2D) As Vector2D

        Return New Vector2D(source.X - p.X, source.Y - p.Y)

    End Operator

    Public Shared Operator /(ByVal source As Vector2D, ByVal num As Int32) As Vector2D

        Return New Vector2D(source.X / num, source.Y / num)

    End Operator
    Public Shared Operator /(ByVal source As Vector2D, ByVal num As Single) As Vector2D

        Return New Vector2D(source.X / num, source.Y / num)

    End Operator
    Public Shared Operator /(ByVal source As Vector2D, ByVal p As Point) As Vector2D

        Return New Vector2D(source.X / p.X, source.Y / p.Y)

    End Operator
    Public Shared Operator /(ByVal source As Vector2D, ByVal p As Vector2D) As Vector2D

        Return New Vector2D(source.X / p.X, source.Y / p.Y)

    End Operator

    Public Shared Operator *(ByVal source As Vector2D, ByVal num As Int32) As Vector2D

        Return New Vector2D(source.X * num, source.Y * num)

    End Operator
    Public Shared Operator *(ByVal source As Vector2D, ByVal num As Single) As Vector2D

        Return New Vector2D(source.X * num, source.Y * num)

    End Operator
    Public Shared Operator *(ByVal source As Vector2D, ByVal p As Point) As Vector2D

        Return New Vector2D(source.X * p.X, source.Y * p.Y)

    End Operator
    Public Shared Operator *(ByVal source As Vector2D, ByVal p As Vector2D) As Vector2D

        Return New Vector2D(source.X * p.X, source.Y * p.Y)

    End Operator

#End Region

#Region "Type Operator"

    ''' <summary>
    ''' Point -> Vector2D
    ''' </summary>
    Public Shared Narrowing Operator CType(ByVal p As Point) As Vector2D
        Return New Vector2D(p.X, p.Y)
    End Operator

    ''' <summary>
    ''' PointF -> Vector2D
    ''' </summary>
    Public Shared Narrowing Operator CType(ByVal p As PointF) As Vector2D
        Return New Vector2D(p.X, p.Y)
    End Operator

    ''' <summary>
    ''' Vector2D -> PointF
    ''' </summary>
    Public Shared Narrowing Operator CType(ByVal v As Vector2D) As PointF
        Return New PointF(v.X, v.Y)
    End Operator

#End Region

#Region "Property"

    ''' <summary>
    ''' X 좌표의 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Single
        Get
            Return g_X
        End Get
        Set(ByVal value As Single)
            g_X = value
        End Set
    End Property

    ''' <summary>
    ''' Y 좌표의 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single
        Get
            Return g_Y
        End Get
        Set(ByVal value As Single)
            g_Y = value
        End Set
    End Property

    ''' <summary>
    ''' Vector2D 구조체의 멤버(x, y)가 비어 있는지의 여부를 나타내는 값을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return g_X = 0.0F AndAlso g_Y = 0.0F
        End Get
    End Property

#End Region

    ''' <summary>
    ''' 좌표를 문자열 형식으로 표현합니다.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return String.Format("X: {0}, Y: {1}", g_X, g_Y)
    End Function

End Structure