' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxExplosion/ctor.vb
'
' Date:
'   2013/12/22
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxExplosion class constructor.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxExplosion

    ''' <summary>
    ''' 지정한 이미지와 개별 효과의 크기를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">효과에 사용할 이미지를 입력합니다.</param>
    ''' <param name="ParticleSize">개별 효과의 크기를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Drawing.Image, ByVal ParticleSize As Drawing.Size)

        MyBase.New(Main, Img, ParticleSize)

    End Sub

    ''' <summary>
    ''' 지정한 이미지와 개별 효과의 넓이, 높이를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">효과에 사용할 이미지를 입력합니다.</param>
    ''' <param name="ParticleWidth">개별 효과의 넓이를 입력합니다.</param>
    ''' <param name="ParticleHeight">개별 효과의 높이를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Drawing.Image, ByVal ParticleWidth As Int32, ByVal ParticleHeight As Int32)

        MyBase.New(Main, Img, New Drawing.Size(ParticleWidth, ParticleHeight))

    End Sub

    ''' <summary>
    ''' 지정한 이미지, 개별 효과의 크기 및 개별 효과의 갯수를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">효과에 사용할 이미지를 입력합니다.</param>
    ''' <param name="ParticleSize">개별 효과의 크기를 입력합니다.</param>
    ''' <param name="ParticleCount">개별 효과의 갯수를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Drawing.Image, ByVal ParticleSize As Drawing.Size, ByVal ParticleCount As Int32)

        MyBase.New(Main, Img, ParticleSize, ParticleCount)

    End Sub

    ''' <summary>
    ''' 지정한 이미지, 개별 효과의 넓이, 높이 및 개별 효과의 갯수를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">효과에 사용할 이미지를 입력합니다.</param>
    ''' <param name="ParticleWidth">개별 효과의 넓이를 입력합니다.</param>
    ''' <param name="ParticleHeight">개별 효과의 높이를 입력합니다.</param>
    ''' <param name="ParticleCount">개별 효과의 갯수를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Drawing.Image, ByVal ParticleWidth As Int32, ByVal ParticleHeight As Int32, ByVal ParticleCount As Int32)

        MyBase.New(Main, Img, New Drawing.Size(ParticleWidth, ParticleHeight), ParticleCount)

    End Sub



    ''' <summary>
    ''' 지정한 텍스쳐와 개별 효과의 크기를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">효과에 사용할 텍스쳐를 입력합니다.</param>
    ''' <param name="ParticleSize">개별 효과의 크기를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal ParticleSize As Drawing.Size)

        MyBase.New(Main, Texture, ParticleSize)

    End Sub

    ''' <summary>
    ''' 지정한 텍스쳐와 개별 효과의 넓이, 높이를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">효과에 사용할 텍스쳐를 입력합니다.</param>
    ''' <param name="ParticleWidth">개별 효과의 넓이를 입력합니다.</param>
    ''' <param name="ParticleHeight">개별 효과의 높이를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal ParticleWidth As Int32, ByVal ParticleHeight As Int32)

        MyBase.New(Main, Texture, New Drawing.Size(ParticleWidth, ParticleHeight))

    End Sub

    ''' <summary>
    ''' 지정한 이미지, 개별 효과의 크기 및 개별 효과의 갯수를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">효과에 사용할 텍스쳐를 입력합니다.</param>
    ''' <param name="ParticleSize">개별 효과의 크기를 입력합니다.</param>
    ''' <param name="ParticleCount">개별 효과의 갯수를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal ParticleSize As Drawing.Size, ByVal ParticleCount As Int32)

        MyBase.New(Main, Texture, ParticleSize, ParticleCount)

    End Sub

    ''' <summary>
    ''' 지정한 이미지, 개별 효과의 넓이, 높이 및 개별 효과의 갯수를 이용하여 효과 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">효과에 사용할 텍스쳐를 입력합니다.</param>
    ''' <param name="ParticleWidth">개별 효과의 넓이를 입력합니다.</param>
    ''' <param name="ParticleHeight">개별 효과의 높이를 입력합니다.</param>
    ''' <param name="ParticleCount">개별 효과의 갯수를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal ParticleWidth As Int32, ByVal ParticleHeight As Int32, ByVal ParticleCount As Int32)

        MyBase.New(Main, Texture, New Drawing.Size(ParticleWidth, ParticleHeight), ParticleCount)

    End Sub

End Class