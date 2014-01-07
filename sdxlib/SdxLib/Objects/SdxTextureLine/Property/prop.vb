' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxTextureLine/prop.vb
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
'   Defines SdxTextureLine class properties.

Imports System.Drawing
Partial Class SdxTextureLine

    ''' <summary>
    ''' 선을 그릴 때 텍스쳐를 매핑하여 그리는 작업을 지원하는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property SupportsTextureMapping As Boolean Implements IGraphicsLine.SupportsTextureMapping
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' 선의 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Color As Drawing.Color Implements IGraphicsLine.Color
        Get
            Return g_Color
        End Get
        Set(ByVal value As Drawing.Color)
            g_Color = value
        End Set
    End Property

    ''' <summary>
    ''' 선의 시작 점을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Start As Vector2D Implements IGraphicsLine.Start
        Get
            Return g_Start
        End Get
        Set(ByVal value As Vector2D)
            g_Start = value
            UpdateMembers()
        End Set
    End Property

    ''' <summary>
    ''' 선의 끝 점을 가져오거나 설정합니다.
    ''' </summary>
    Public Property [End] As Vector2D Implements IGraphicsLine.End
        Get
            Return g_Ends
        End Get
        Set(ByVal value As Vector2D)
            g_Ends = value
            UpdateMembers()
        End Set
    End Property

    ''' <summary>
    ''' 페이드 효과의 계산 유형을 가져오거나 설정합니다.
    ''' </summary>
    Public Property FadeType As FadeType Implements IGraphicsLine.FadeType
        Get
            Return g_FadeType
        End Get
        Set(ByVal value As FadeType)
            g_FadeType = value
            m_CurrentStep = 0
        End Set
    End Property

    ''' <summary>
    ''' 페이드 효과의 단계 수를 가져오거나 설정합니다. (일반적으로, 60프레임에서 1초는 60입니다)
    ''' </summary>
    Public Property FadeSteps As Int32 Implements IGraphicsLine.FadeSteps
        Get
            Return g_FadeStep
        End Get
        Set(ByVal value As Int32)
            g_FadeStep = value
            m_CurrentStep = 0
            m_FadeStep = 1 / Me.FadeSteps
            m_FadeAmount = 90 / Me.FadeSteps
        End Set
    End Property

    ''' <summary>
    ''' 페이드 효과의 유형을 가져오거나 설정합니다.
    ''' </summary>
    Public Property FadeEffect As FadeEffect Implements IGraphicsLine.FadeEffect
        Get
            Return g_FadeEffect
        End Get
        Set(ByVal value As FadeEffect)
            g_FadeEffect = value
        End Set
    End Property

    ''' <summary>
    ''' 페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Preserve As Boolean Implements IGraphicsLine.Preserve
        Get
            Return g_Preserve
        End Get
        Set(ByVal value As Boolean)
            g_Preserve = value
        End Set
    End Property

    ''' <summary>
    ''' 페이드 효과가 끝난 후에 페이드 효과를 번갈아가면서 적용할 것인지의 여부를 가져오거나 설정합니다. (예: FadeIn->FadeOut->FadeIn->...)
    ''' </summary>
    Public Property ToggleFadeEffect As Boolean Implements IGraphicsLine.ToggleFadeEffect
        Get
            Return g_ToggleFadeEffect
        End Get
        Set(ByVal value As Boolean)
            g_ToggleFadeEffect = value
        End Set
    End Property

    ''' <summary>
    ''' 선의 두께를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Thickness As Int32 Implements IGraphicsLine.Thickness
        Get
            Return g_Thickness
        End Get
        Set(ByVal value As Int32)
            g_Thickness = value
        End Set
    End Property

End Class