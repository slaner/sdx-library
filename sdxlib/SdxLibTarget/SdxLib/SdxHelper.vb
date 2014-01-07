Imports System.Drawing
Imports System.Drawing.Imaging

Friend Class SdxHelper

    ''' <summary>
    ''' 이미지를 스트림으로 변환합니다.
    ''' </summary>
    ''' <param name="Img">변환할 이미지를 입력합니다.</param>
    ''' <param name="ImgFormat">변환할 이미지의 형식을 입력합니다.</param>
    Public Shared Function ImageToStream(ByVal Img As Image, ByVal ImgFormat As ImageFormat) As IO.Stream

        Dim imgStream As IO.Stream = IO.Stream.Null
        Img.Save(imgStream, ImgFormat)
        Return imgStream

    End Function

End Class