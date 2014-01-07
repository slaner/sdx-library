' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxMissile/ctor.vb
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
'   Defines SdxMissile class constructor.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxMissile

    ''' <summary>
    ''' 지정된 이미지와 대상 플레이어를 이용해 미사일 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">사용할 이미지를 입력합니다.</param>
    ''' <param name="Target">대상 플레이어를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image, ByVal FireBase As Vector2D, ByVal Target As SdxPlayer)

        MyBase.New(Main, Img, Img.Size, 1)
        g_Location = FireBase
        m_Target = Target
        m_Center.X = Img.Width / 2
        m_Center.Y = Img.Height / 2

    End Sub

    ''' <summary>
    ''' 지정된 이미지와 대상 플레이어 및 피해량을 이용해 미사일 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Img">사용할 이미지를 입력합니다.</param>
    ''' <param name="Target">대상 플레이어를 입력합니다.</param>
    ''' <param name="Damage">피해량을 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image, ByVal FireBase As Vector2D, ByVal Target As SdxPlayer, ByVal Damage As Int32)

        MyBase.New(Main, Img, Img.Size, 1)
        g_Location = FireBase
        m_Target = Target
        m_Center.X = Img.Width / 2
        m_Center.Y = Img.Height / 2
        g_Damage = Damage

    End Sub

End Class