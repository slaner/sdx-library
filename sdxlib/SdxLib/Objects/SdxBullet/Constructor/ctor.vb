' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBullet/ctor.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  13
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
'   Defines SdxBullet class's constructor.
Imports System.Drawing
Partial Class SdxBullet

    Public Sub New(ByVal Main As SDXMain, ByVal Img As Drawing.Image, ByVal Location As Drawing.Point, ByVal Speed As Vector2D, ByVal Damage As Int32)

        MyBase.New(Main)

        ' 총알의 위치를 저장한다.
        g_Location = Location

        ' 총알의 속도를 저장한다.
        g_Speed = Speed

        ' 총알의 공격력을 저장한다.
        g_Damage = Damage

        ' 이미지의 투명도 맵을 만든다.
        g_AlphaMap = SdxHelper.CreateAlphaMap(Img)

        ' 이미지 -> 텍스쳐 변환
        g_BulletTexture = SDXHelper.TextureFromImage(MyBase.Main.Device, Img)

        ' 이미지의 크기를 저장한다.
        g_Size = Img.Size

        ' LifeTime
        m_CreationTime = Environment.TickCount

    End Sub

End Class