' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBlock2D/ctor.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  18
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
'   Defines SdxBlock2D class's constructor.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Partial Class SdxBlock2D

    ''' <summary>
    ''' 지정한 이미지를 이용해 2D 블록 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image)

        MyBase.New(Main)

        ' 이미지의 투명도 맵을 만든다.
        g_AlphaMap = SdxHelper.CreateAlphaMap(Img)

        ' 이미지 -> 텍스쳐 변환
        g_BlockTexture = SdxHelper.TextureFromImage(MyBase.Main.Device, Img)

        ' 이미지의 크기를 저장한다.
        g_Size = Img.Size

    End Sub

    Public Sub New(ByVal Main As SDXMain, ByVal Img As D3.Texture)

        MyBase.New(Main)

        ' 이미지 -> 텍스쳐 변환
        g_BlockTexture = Img

        ' 이미지의 크기를 저장한다.
        g_Size = New Size(Img.GetLevelDescription(0).Width, Img.GetLevelDescription(0).Height)

    End Sub

End Class