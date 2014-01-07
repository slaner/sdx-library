' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxFont/method.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  3
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
'   Defines SdxFont class's methods.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

Partial Class SdxFont

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="ForeColor">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 텍스트를 입력합니다.</param>
    ''' <param name="Args">한 개 이상의 값을 포함하는 형식 개체의 배열을 입력합니다.</param>
    Public Sub DrawText(ByVal X As Int32, ByVal Y As Int32, ByVal ForeColor As Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        If String.IsNullOrEmpty(Text) Then Return
        Dim strFormat As String = String.Format(Text, Args)
        If String.IsNullOrEmpty(strFormat) Then Return
        If Me.UseTextSprite Then
            m_Font.DrawText(MyBase.Main.TextSprite, strFormat, X, Y, ForeColor)
        Else
            m_Font.DrawText(Nothing, strFormat, X, Y, ForeColor)
        End If

    End Sub

    ''' <summary>
    ''' 텍스트의 넓이와 높이를 측정합니다.
    ''' </summary>
    ''' <param name="Text">측정할 텍스트를 입력합니다.</param>
    ''' <param name="DTF">텍스트를 그릴 방법을 입력합니다.</param>
    Public Function MeasureString(ByVal Text As String, ByVal DTF As Int32) As Rectangle

        Return m_Font.MeasureString(MyBase.Main.ImageSprite, Text, DTF, 0)

    End Function

    ''' <summary>
    ''' 텍스트를 비디오 메모리에 저장합니다.
    ''' </summary>
    ''' <param name="Text">비디오 메모리에 저장할 텍스트를 입력합니다.</param>
    Public Sub PreloadText(ByVal Text As String)
        m_Font.PreloadText(Text)
    End Sub

    ''' <summary>
    ''' 문자를 비디오 메모리에 저장합니다.
    ''' </summary>
    ''' <param name="Start">비디오 메모리에 저장할 첫번째 문자의 ASCII 코드값을 입력합니다.</param>
    ''' <param name="End">비디오 메모리에 저장할 마지막 문자의 ASCII 코드값을 입력합니다.</param>
    Public Sub PreloadCharacters(ByVal Start As Int32, ByVal [End] As Int32)
        m_Font.PreloadCharacters(Start, [End])
    End Sub

    ''' <summary>
    ''' 문자 모양 정보를 비디오 메모리에 저장합니다.
    ''' </summary>
    ''' <param name="Start">비디오 메모리에 저장할 첫번째 문자의 ASCII 코드값을 입력합니다.</param>
    ''' <param name="End">비디오 메모리에 저장할 마지막 문자의 ASCII 코드값을 입력합니다.</param>
    Public Sub PreloadGlyphs(ByVal Start As Int32, ByVal [End] As Int32)
        m_Font.PreloadGlyphs(Start, [End])
    End Sub

    ''' <summary>
    ''' 문자 모양 정보를 가져옵니다.
    ''' </summary>
    ''' <param name="Glyph">가져올 문자 모양 정보의 ASCII 코드값을 입력합니다. (?)</param>
    Public Function GetGlyphData(ByVal Glyph As Int32) As D3.Texture
        Return m_Font.GetGlyphData(Glyph)
    End Function

    ''' <summary>
    ''' 문자 모양 정보를 가져옵니다.
    ''' </summary>
    ''' <param name="Glyph">가져올 문자 모양 정보의 ASCII 코드값을 입력합니다. (?)</param>
    ''' <param name="BlackBox">?</param>
    ''' <param name="CellInc">?</param>
    Public Function GetGlyphData(ByVal Glyph As Int32, ByRef BlackBox As Rectangle, ByRef CellInc As Point) As D3.Texture
        Return m_Font.GetGlyphData(Glyph, BlackBox, CellInc)
    End Function

End Class