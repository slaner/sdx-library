' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxTextureLine/ctor.vb
'
' Dependencies:
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  12
'
' Date:
'   2013/12/17
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxTextureLine class constructor.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxTextureLine

    ''' <summary>
    ''' 지정된 이미지를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image)

        MyBase.New(Main)
        Initialize(Img)
        m_LoadFromImage = True

    End Sub

    ''' <summary>
    ''' 지정된 텍스쳐를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture)

        MyBase.New(Main)
        Initialize(Texture)

    End Sub

    ''' <summary>
    ''' 지정된 텍스쳐와 크기를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="Size">텍스쳐의 크기를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal Size As Size)

        MyBase.New(Main)
        m_LineTexture = Texture
        m_LineSize = Size
        m_RotateOrigin = Point.Round(New PointF(m_LineSize.Width / 2, m_LineSize.Height / 2))

    End Sub



    ''' <summary>
    ''' 지정된 이미지와 시작 점 및 끝 점 위치를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="Ends">끝 점의 위치를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image, ByVal Start As Vector2D, ByVal Ends As Vector2D)

        MyBase.New(Main)
        Initialize(Img)
        Me.Start = Start
        Me.End = Ends
        m_LoadFromImage = True

    End Sub

    ''' <summary>
    ''' 지정된 텍스쳐와 시작 점 및 끝 점 위치를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="Ends">끝 점의 위치를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal Start As Vector2D, ByVal Ends As Vector2D)

        MyBase.New(Main)
        Initialize(Texture)
        Me.Start = Start
        Me.End = Ends

    End Sub

    ''' <summary>
    ''' 지정된 텍스쳐, 크기와 시작 점 및 끝 점 위치를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="Size">텍스쳐의 크기를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="Ends">끝 점의 위치를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal Size As Size, ByVal Start As Vector2D, ByVal Ends As Vector2D)

        MyBase.New(Main)
        m_LineTexture = Texture
        m_LineSize = Size
        m_RotateOrigin = Point.Round(New PointF(m_LineSize.Width / 2, m_LineSize.Height / 2))
        Me.Start = Start
        Me.End = Ends

    End Sub



    ''' <summary>
    ''' 지정된 이미지, 시작 점 및 끝 점 위치와 페이드 효과 정보를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="Ends">끝 점의 위치를 입력합니다.</param>
    ''' <param name="FadeType">페이드 효과를 적용하는 방법을 입력합니다.</param>
    ''' <param name="FadeEffect">페이드 효과의 유형을 입력합니다.</param>
    ''' <param name="FadeStep">페이드 효과의 길이를 입력합니다. (일반적으로, 60프레임인 경우에 1초는 60입니다)</param>
    ''' <param name="Preserve">페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 입력합니다.</param>
    ''' <param name="ToggleFadeEffect">페이드 효과를 번갈아가며 적용할 것인지의 여부를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image, ByVal Start As Vector2D, ByVal Ends As Vector2D, ByVal FadeType As FadeType, ByVal FadeEffect As FadeEffect, ByVal FadeStep As Int32, ByVal Preserve As Boolean, ByVal ToggleFadeEffect As Boolean)

        MyBase.New(Main)
        Initialize(Img)
        Me.Start = Start
        Me.End = Ends
        Me.FadeType = FadeType
        Me.FadeEffect = FadeEffect
        Me.FadeSteps = FadeStep
        Me.Preserve = Preserve
        Me.ToggleFadeEffect = ToggleFadeEffect
        m_LoadFromImage = True

    End Sub

    ''' <summary>
    ''' 지정된 텍스쳐, 시작 점 및 끝 점 위치와 페이드 효과 정보를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="Ends">끝 점의 위치를 입력합니다.</param>
    ''' <param name="FadeType">페이드 효과를 적용하는 방법을 입력합니다.</param>
    ''' <param name="FadeEffect">페이드 효과의 유형을 입력합니다.</param>
    ''' <param name="FadeStep">페이드 효과의 길이를 입력합니다. (일반적으로, 60프레임인 경우에 1초는 60입니다)</param>
    ''' <param name="Preserve">페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 입력합니다.</param>
    ''' <param name="ToggleFadeEffect">페이드 효과를 번갈아가며 적용할 것인지의 여부를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal Start As Vector2D, ByVal Ends As Vector2D, ByVal FadeType As FadeType, ByVal FadeEffect As FadeEffect, ByVal FadeStep As Int32, ByVal Preserve As Boolean, ByVal ToggleFadeEffect As Boolean)

        MyBase.New(Main)
        Initialize(Texture)
        Me.Start = Start
        Me.End = Ends
        Me.FadeType = FadeType
        Me.FadeEffect = FadeEffect
        Me.FadeSteps = FadeStep
        Me.Preserve = Preserve
        Me.ToggleFadeEffect = ToggleFadeEffect

    End Sub

    ''' <summary>
    ''' 지정된 텍스쳐, 크기, 시작 점 및 끝 점 위치와 페이드 효과 정보를 이용해 SdxTextureLine 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="Size">텍스쳐의 크기를 입력합니다.</param>
    ''' <param name="Start">시작 점의 위치를 입력합니다.</param>
    ''' <param name="Ends">끝 점의 위치를 입력합니다.</param>
    ''' <param name="FadeType">페이드 효과를 적용하는 방법을 입력합니다.</param>
    ''' <param name="FadeEffect">페이드 효과의 유형을 입력합니다.</param>
    ''' <param name="FadeStep">페이드 효과의 길이를 입력합니다. (일반적으로, 60프레임인 경우에 1초는 60입니다)</param>
    ''' <param name="Preserve">페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 입력합니다.</param>
    ''' <param name="ToggleFadeEffect">페이드 효과를 번갈아가며 적용할 것인지의 여부를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal Size As Size, ByVal Start As Vector2D, ByVal Ends As Vector2D, ByVal FadeType As FadeType, ByVal FadeEffect As FadeEffect, ByVal FadeStep As Int32, ByVal Preserve As Boolean, ByVal ToggleFadeEffect As Boolean)

        MyBase.New(Main)
        m_LineTexture = Texture
        m_LineSize = Size
        m_RotateOrigin = Point.Round(New PointF(m_LineSize.Width / 2, m_LineSize.Height / 2))
        Me.Start = Start
        Me.End = Ends
        Me.FadeType = FadeType
        Me.FadeEffect = FadeEffect
        Me.FadeSteps = FadeStep
        Me.Preserve = Preserve
        Me.ToggleFadeEffect = ToggleFadeEffect

    End Sub

End Class