' SlaneR's DirectX Library (SdxLib)
'
' File:
'   FontDescription.vb
'
' Dependencies:
'   -
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  0
'
' Date:
'   2013/12/09
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines Microsoft.DirectX.Direct3D.FontDescription as SdxMain.FontDescription to maximize compatibility.

''' <summary>
''' 폰트의 특성을 저장합니다.
''' </summary>
Public Class FontDescription

    Private g_CharSet As CharacterSet
    Private g_FaceName As String
    Private g_Height As Int32
    Private g_Italic As Boolean
    Private g_MipLevels As Int32
    Private g_OutputPrecision As OutputPrecision
    Private g_PitchAndFamily As PitchAndFamily
    Private g_Quality As FontQuality
    Private g_Weight As FontWeight
    Private g_Width As Int32

    ' DEFAULT CONSTRUCTOR FOR ZERO PARAMETER
    Public Sub New()
    End Sub

    ''' <summary>
    ''' 지정한 값을 이용하여 FontDescription 클래스의 새 개체를 만듭니다.
    ''' </summary>
    ''' <param name="CharSet">폰트의 문자셋을 입력합니다.</param>
    ''' <param name="FaceName">폰트의 이름을 입력합니다.</param>
    ''' <param name="Height">폰트의 높이를 입력합니다.</param>
    ''' <param name="Italic">기울임체를 사용할 것인지의 여부를 입력합니다.</param>
    ''' <param name="MipLevels">Mip-Level을 입력합니다.</param>
    ''' <param name="OutputPrecision">출력 정확도를 입력합니다.</param>
    ''' <param name="PitchAndFamily">PitchAndFamily</param>
    ''' <param name="Quality">폰트의 품질을 입력합니다.</param>
    ''' <param name="Weight">폰트의 두께를 입력합니다.</param>
    ''' <param name="Width">폰트의 넓이를 입력합니다.</param>
    Public Sub New(ByVal CharSet As CharacterSet, ByVal FaceName As String, ByVal Height As Int32, ByVal Italic As Boolean, ByVal MipLevels As Int32, ByVal OutputPrecision As OutputPrecision, ByVal PitchAndFamily As PitchAndFamily, ByVal Quality As FontQuality, ByVal Weight As FontWeight, ByVal Width As Int32)

        g_CharSet = CharSet
        g_FaceName = FaceName
        g_Height = Height
        g_Italic = Italic
        g_MipLevels = MipLevels
        g_OutputPrecision = OutputPrecision
        g_PitchAndFamily = PitchAndFamily
        g_Quality = Quality
        g_Weight = Weight
        g_Width = Width

    End Sub

    ''' <summary>
    ''' 폰트의 문자셋 정보를 가져오거나 설정합니다.
    ''' </summary>
    Public Property CharSet As CharacterSet
        Get
            Return g_CharSet
        End Get
        Set(ByVal value As CharacterSet)
            g_CharSet = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트의 이름을 가져오거나 설정합니다.
    ''' </summary>
    Public Property FaceName As String
        Get
            Return g_FaceName
        End Get
        Set(ByVal value As String)
            g_FaceName = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트의 높이를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Height As Int32
        Get
            Return g_Height
        End Get
        Set(ByVal value As Int32)
            g_Height = value
        End Set
    End Property

    ''' <summary>
    ''' 기울임체를 사용할 것인지의 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Italic As Boolean
        Get
            Return g_Italic
        End Get
        Set(ByVal value As Boolean)
            g_Italic = value
        End Set
    End Property

    ''' <summary>
    ''' Mip-Level을 가져오거나 설정합니다.
    ''' </summary>
    Public Property MipLevels As Int32
        Get
            Return g_MipLevels
        End Get
        Set(ByVal value As Int32)
            g_MipLevels = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트를 출력할 때 얼마나 정확하게 출력할 것인지를 결정하는 값을 가져오거나 설정합니다.
    ''' </summary>
    Public Property OutputPrecision As OutputPrecision
        Get
            Return g_OutputPrecision
        End Get
        Set(ByVal value As OutputPrecision)
            g_OutputPrecision = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트의 Pitch와 Family를 가져오거나 설정합니다.
    ''' </summary>
    Public Property PitchAndFamily As PitchAndFamily
        Get
            Return g_PitchAndFamily
        End Get
        Set(ByVal value As PitchAndFamily)
            g_PitchAndFamily = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트의 품질을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Quality As FontQuality
        Get
            Return g_Quality
        End Get
        Set(ByVal value As FontQuality)
            g_Quality = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트의 두께를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Weight As FontWeight
        Get
            Return g_Weight
        End Get
        Set(ByVal value As FontWeight)
            g_Weight = value
        End Set
    End Property

    ''' <summary>
    ''' 폰트의 넓이를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Width As Int32
        Get
            Return g_Width
        End Get
        Set(ByVal value As Int32)
            g_Width = value
        End Set
    End Property

    Public Shared Widening Operator CType(ByVal fd As FontDescription) As Microsoft.DirectX.Direct3D.FontDescription

        Dim d3fd As New Microsoft.DirectX.Direct3D.FontDescription

        With d3fd
            .CharSet = fd.CharSet
            .FaceName = fd.FaceName
            .Height = fd.Height
            .IsItalic = fd.Italic
            .MipLevels = fd.MipLevels
            .OutputPrecision = fd.OutputPrecision
            .PitchAndFamily = fd.PitchAndFamily
            .Quality = fd.Quality
            .Weight = fd.Weight
            .Width = fd.Width
        End With

        Return d3fd

    End Operator

End Class