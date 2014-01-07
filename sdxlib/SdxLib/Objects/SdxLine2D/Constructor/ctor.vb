Imports System.Drawing
Partial Class SdxLine2D

    ''' <summary>
    ''' 시작 점과 끝 점을 이용해 SdxLine2D 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="End">끝 점의 위치를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Start As Vector2D, ByVal [End] As Vector2D)

        MyBase.New(Main)
        g_Start = Start
        g_Ends = [End]

    End Sub

    ''' <summary>
    ''' 시작 점과 끝 점 및 색을 이용해 SdxLine2D 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="End">끝 점의 위치를 입력합니다.</param>
    ''' <param name="Color">선의 색을 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Start As Vector2D, ByVal [End] As Vector2D, ByVal Color As Drawing.Color)

        MyBase.New(Main)
        g_Start = Start
        g_Ends = [End]
        g_Color = Color

    End Sub

    ''' <summary>
    ''' 시작 점과 끝 점, 색 및 두께를 이용해 SdxLine2D 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="End">끝 점의 위치를 입력합니다.</param>
    ''' <param name="Color">선의 색을 입력합니다.</param>
    ''' <param name="Thickness">선의 두께를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Start As Vector2D, ByVal [End] As Vector2D, ByVal Color As Drawing.Color, ByVal Thickness As Int32)

        MyBase.New(Main)
        g_Start = Start
        g_Ends = [End]
        g_Color = Color
        g_Thickness = Thickness

    End Sub

    ''' <summary>
    ''' 시작 점과 끝 점, 색, 두께 및 페이드 효과 정보를 이용해 SdxLine2D 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="End">끝 점의 위치를 입력합니다.</param>
    ''' <param name="Color">선의 색을 입력합니다.</param>
    ''' <param name="Thickness">선의 두께를 입력합니다.</param>
    ''' <param name="FadeType">페이드 효과를 적용하는 방법을 입력합니다.</param>
    ''' <param name="FadeEffect">페이드 효과의 유형을 입력합니다.</param>
    ''' <param name="FadeStep">페이드 효과의 길이를 입력합니다. (일반적으로, 60프레임인 경우에 1초는 60입니다)</param>
    ''' <param name="Preserve">페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 입력합니다.</param>
    ''' <param name="ToggleFadeEffect">페이드 효과를 번갈아가며 적용할 것인지의 여부를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Start As Vector2D, ByVal [End] As Vector2D, ByVal Color As Drawing.Color, ByVal Thickness As Int32, ByVal FadeType As FadeType, ByVal FadeEffect As FadeEffect, ByVal FadeStep As Int32, ByVal Preserve As Boolean, ByVal ToggleFadeEffect As Boolean)

        MyBase.New(Main)
        g_Start = Start
        g_Ends = [End]
        g_Color = Color
        g_Thickness = Thickness
        Me.FadeType = FadeType
        Me.FadeEffect = FadeEffect
        Me.FadeSteps = FadeStep
        Me.Preserve = Preserve
        Me.ToggleFadeEffect = ToggleFadeEffect

    End Sub

End Class