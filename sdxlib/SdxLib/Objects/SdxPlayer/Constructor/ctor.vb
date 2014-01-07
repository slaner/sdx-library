' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxPlayer/ctor.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  25
'
' Date:
'   2013/12/10
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxPlayer's constructor.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Partial Class SdxPlayer

    ''' <summary>
    ''' 지정한 이미지를 이용해 플레이어 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image)

        MyBase.New(Main)
        Initialize(Img)

    End Sub

    ''' <summary>
    ''' 지정한 이미지와 플레이어 조작 설정 파일을 이용해 플레이어 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">텍스쳐로 사용할 이미지를 입력합니다.</param>
    ''' <param name="ControlSetPath">플레이어 조작 설정 파일이 저장된 위치를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image, ByVal ControlSetPath As String)

        MyBase.New(Main)
        Initialize(Img, ControlSetPath)

    End Sub

    ''' <summary>
    ''' 지정한 텍스쳐와 투명도 맵을 이용해 플레이어 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="AlphaMap">투명도 맵을 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal AlphaMap() As Byte)

        MyBase.New(Main)
        Initialize(Texture, AlphaMap)

    End Sub

    ''' <summary>
    ''' 지정한 텍스쳐와 투명도 맵, 그리고 플레이어 조작 설정 파일을 이용해 플레이어 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Texture">텍스쳐를 입력합니다.</param>
    ''' <param name="AlphaMap">투명도 맵을 입력합니다.</param>
    ''' <param name="ControlSetPath">플레이어 조작 설정 파일이 저장된 위치를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Texture As D3.Texture, ByVal AlphaMap() As Byte, ByVal ControlSetPath As String)

        MyBase.New(Main)
        Initialize(Texture, AlphaMap, ControlSetPath)

    End Sub

End Class