' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxFont/ctor.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  2
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
'   Defines SdxFont class's constructor.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Partial Class SdxFont
    
    ''' <summary>
    ''' SdxFont 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Font">텍스트를 그릴 때 사용할 폰트를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Font As Font)

        MyBase.New(Main)
        m_Font = New D3.Font(MyBase.Main.Device, Font)

    End Sub

    ''' <summary>
    ''' SdxFont 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="FontDesc">텍스트를 그릴 때 사용할 폰트의 정보를 담고있는 FontDescription 구조체를 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal FontDesc As FontDescription)

        MyBase.New(Main)
        m_Font = New D3.Font(MyBase.Main.Device, FontDesc)

    End Sub

    ''' <summary>
    ''' SdxFont 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Main">주 개체를 입력합니다.</param>
    ''' <param name="Width">폰트의 넓이를 입력합니다.</param>
    ''' <param name="Height">폰트의 높이를 입력합니다.</param>
    ''' <param name="Weight">폰트의 두께를 입력합니다.</param>
    ''' <param name="MipLevels">Mip 레벨을 입력합니다.</param>
    ''' <param name="Italic">기울임을 사용할 경우, 이 값을 True 로 입력합니다.</param>
    ''' <param name="CharSet">문자셋을 입력합니다.</param>
    ''' <param name="Precision">출력 방법을 입력합니다.</param>
    ''' <param name="Quality">품질을 입력합니다.</param>
    ''' <param name="Pitch">폰트 유형을 입력합니다.</param>
    ''' <param name="Name">폰트 이름을 입력합니다.</param>
    Public Sub New(ByVal Main As SDXMain, ByVal Width As Int32, ByVal Height As Int32, ByVal Weight As FontWeight, ByVal MipLevels As Int32, ByVal Italic As Boolean, ByVal CharSet As CharacterSet, ByVal Precision As OutputPrecision, ByVal Quality As FontQuality, ByVal Pitch As PitchAndFamily, ByVal Name As String)

        MyBase.New(Main)

        m_Font = New D3.Font(MyBase.Main.Device, Height, Width, Weight, MipLevels, Italic, CharSet, Precision, Quality, Pitch, Name)

    End Sub

End Class