Imports System.Drawing
Imports System.Drawing.Imaging
Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Runtime.InteropServices
Imports Microsoft.DirectX

Public Class SDXHelper

    ''' <summary>
    ''' 지정한 텍스트를 추적 창에 기록합니다.
    ''' </summary>
    ''' <param name="Text">기록할 텍스트를 입력합니다.</param>
    ''' <param name="Arguments">추가로 입력될 형식 배열을 입력합니다.</param>
    Public Shared Sub SDXTrace(ByVal Text As String, ByVal ParamArray Arguments() As Object)

        Dim sf As String = String.Format(Text, Arguments)
        Debug.Print("[{0:X8}] {1}", Environment.TickCount, sf)

    End Sub

    ''' <summary>
    ''' 텍스트의 넓이를 계산합니다.
    ''' </summary>
    ''' <param name="Text">계산할 텍스트를 입력합니다.</param>
    ''' <param name="Font">계산에 사용할 폰트를 입력합니다.</param>
    Public Shared Function GetTextWidth(ByVal Font As D3.Font, ByVal DotWidth As Int32, ByVal Text As String) As Int32

        Return Font.MeasureString(Nothing, Text & ".", 0, 0).Width - DotWidth

    End Function

    ''' <summary>
    ''' 사각형 머리의 위치를 계산합니다.
    ''' </summary>
    ''' <param name="Angle">사각형의 각도를 입력합니다.</param>
    ''' <param name="Location">사각형이 있는 위치를 입력합니다.</param>
    ''' <param name="Size">사각형의 크기를 입력합니다.</param>
    Public Shared Function GetRectangleHead(ByVal Angle As Single, ByVal Location As Vector2D, ByVal Size As Size) As Vector2D

        ' 4.7123883 Radian = 270 Degree
        Dim radAngle As Double = SDXHelper.DegToRad(Angle),
            hW As Single = Size.Width / 2,
            hH As Single = Size.Height / 2

        With GetRectangleHead

            .X = hW + (Math.Sin(radAngle) * hH) + Location.X
            .Y = (1 + Math.Sin(radAngle + 4.7123883)) * hH + Location.Y

        End With

    End Function

    ''' <summary>
    ''' 사각형 머리의 위치를 계산합니다.
    ''' </summary>
    ''' <param name="Angle">사각형의 각도를 입력합니다.</param>
    ''' <param name="X">사각형이 있는 X 좌표를 입력합니다.</param>
    ''' <param name="Y">사각형이 있는 Y 좌표를 입력합니다.</param>
    ''' <param name="Width">사각형의 넓이를 입력합니다.</param>
    ''' <param name="Height">사각형의 높이를 입력합니다.</param>
    Public Shared Function GetRectangleHead(ByVal Angle As Single, ByVal X As Single, ByVal Y As Single, ByVal Width As Int32, ByVal Height As Int32) As Vector2D

        ' 4.7123883 Radian = 270 Degree
        Dim radAngle As Double = SDXHelper.DegToRad(Angle),
            hW As Single = Width / 2,
            hH As Single = Height / 2

        With GetRectangleHead

            .X = hW + (Math.Sin(radAngle) * hH) + X
            .Y = (1 + Math.Sin(radAngle + 4.7123883)) * hH + Y

        End With

    End Function

    ''' <summary>
    ''' 두 점간의 거리를 계산합니다.
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <param name="p2"></param>
    Public Shared Function GetVectorDistance(ByVal p1 As Vector2D, ByVal p2 As Vector2D) As Single

        Dim xDiff As Single = p2.X - p1.X,
            yDiff As Single = p2.Y - p1.Y

        Return Math.Sqrt((xDiff ^ 2) + (yDiff ^ 2))

    End Function

    ''' <summary>
    ''' 사각형 텍스쳐를 만듭니다.
    ''' </summary>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    Public Shared Function Rectangle(ByVal Device As D3.Device, ByVal Width As Int32, ByVal Height As Int32, ByVal BackColor As Drawing.Color, ByVal BorderColor As Drawing.Color) As D3.Texture

        Dim img As New Bitmap(Width, Height)
        Dim g As Graphics = Drawing.Graphics.FromImage(img)

        g.Clear(BackColor)
        g.DrawRectangle(New Pen(BorderColor), 0, 0, Width - 1, Height - 1)
        g.Dispose()
        Return SDXHelper.TextureFromImage(Device, img)

    End Function

    ''' <summary>
    ''' 두 점 사이의 각도를 계산합니다.
    ''' </summary>
    ''' <param name="p1">원점으로 사용할 점의 위치를 입력합니다.</param>
    ''' <param name="p2">각도를 계산하기 위해 사용할 점의 위치를 입력합니다.</param>
    Public Shared Function GetVectorAngle(ByVal p1 As Point, ByVal p2 As Point) As Single

        Dim xDiff As Int32 = p2.X - p1.X,
            yDiff As Int32 = p2.Y - p1.Y

        Dim tAng As Single = RadToDeg(Math.Atan2(yDiff, xDiff)) + 90
        If tAng < 0 Then tAng += 360
        Return tAng

    End Function

    ''' <summary>
    ''' 두 점 사이의 각도를 계산합니다.
    ''' </summary>
    ''' <param name="p1">원점으로 사용할 점의 위치를 입력합니다.</param>
    ''' <param name="p2">각도를 계산하기 위해 사용할 점의 위치를 입력합니다.</param>
    Public Shared Function GetVectorAngle(ByVal p1 As Vector2D, ByVal p2 As Vector2D) As Single

        Dim xDiff As Single = p2.X - p1.X,
            yDiff As Single = p2.Y - p1.Y

        Dim tAng As Single = RadToDeg(Math.Atan2(yDiff, xDiff)) + 90
        If tAng < 0 Then tAng += 360
        Return tAng

    End Function

    ''' <summary>
    ''' 이미지로부터 투명도 맵을 만듭니다.
    ''' </summary>
    ''' <param name="Img">투명도 맵을 만들 이미지를 입력합니다.</param>
    Public Shared Function CreateAlphaMap(ByVal Img As Image) As Byte()

        Dim Bmp As New Bitmap(Img),
            Bpp As Int32 = Image.GetPixelFormatSize(Bmp.PixelFormat)

        If Bpp < 32 Then
            Debug.Print("Only 32Bpp or Higher pixel format supported!")
            Return Nothing
        End If

        ' 작업을 위해 비트맵을 잠근다.
        Dim bmpData As BitmapData = Bmp.LockBits(New Rectangle(Point.Empty, Bmp.Size),
                                                 ImageLockMode.ReadOnly,
                                                 Bmp.PixelFormat)

        Dim pixelSize As Int32 = Math.Abs(bmpData.Stride) * Bmp.Height
        Dim pixelBytes(pixelSize - 1) As Byte,
            alphaBytes(pixelSize / 4 - 1) As Byte

        ' 비트맵 RGBA 값을 배열로 복사한다.
        Marshal.Copy(bmpData.Scan0, pixelBytes, 0, pixelSize)

        'Dim stack As Int32 = 0,
        '    row As Int32 = 0
        Dim cnt As Int32 = 0
        For i As Int32 = 3 To pixelSize - 1 Step 4
            alphaBytes(cnt) = pixelBytes(i)
            cnt += 1
            'stack += 1
            'If stack >= 32 Then
            '    stack = 0
            '    row += 1
            '    Debug.Write(vbCrLf)
            'End If
        Next

        ' 비트맵 잠금 해제
        Bmp.UnlockBits(bmpData)
        Return alphaBytes

    End Function

    ''' <summary>
    ''' 두 사각형 영역과 투명도 맵을 이용해, 픽셀 단위의 충돌 검사를 실행합니다.
    ''' </summary>
    ''' <param name="Rect1">충돌 검사를 할 첫번째 사각형 영역을 입력합니다.</param>
    ''' <param name="AlphaMap1">충돌 검사를 할 때 사용할 첫번째 사각형의 투명도 맵을 입력합니다.</param>
    ''' <param name="Rect2">충돌 검사를 할 두번째 사각형 영역을 입력합니다.</param>
    ''' <param name="AlphaMap2">충돌 검사를 할 때 사용할 두번째 사각형의 투명도 맵을 입력합니다.</param>
    Public Shared Function PixelIntersects(ByVal Rect1 As RectangleF, ByVal AlphaMap1() As Byte, ByVal Rect2 As RectangleF, ByVal AlphaMap2() As Byte) As Boolean

        Dim partialAlphaMap1 As List(Of Byte) = GetPartialAlphaMap(Rect1, AlphaMap1, Rect2),
            partialAlphaMap2 As List(Of Byte) = GetPartialAlphaMap(Rect2, AlphaMap2, Rect1)

        If partialAlphaMap1.Count <> partialAlphaMap2.Count Then
            Debug.Print("Different AlphaMap Size")
            Return False
        End If
        For i As Int32 = 0 To partialAlphaMap1.Count - 1
            If partialAlphaMap1(i) > 0 AndAlso partialAlphaMap2(i) > 0 Then Return True
        Next

        Return False

    End Function

    ''' <summary>
    ''' 두 사각형 영역으로부터 교차 영역을 만들고, 첫번째 사각형 영역의 부분 투명도 맵을 가져옵니다.
    ''' </summary>
    ''' <param name="Origin">부분 투명도 맵을 만들 사각형 영역을 입력합니다.</param>
    ''' <param name="AlphaMap">부분 투명도 맵을 만들 때 사용할 투명도 맵을 입력합니다.</param>
    ''' <param name="Target">부분 투명도 맵을 만들 때 사용할 제 2의 사각형 영역을 입력합니다.</param>
    Public Shared Function GetPartialAlphaMap(ByVal Origin As RectangleF, ByVal AlphaMap() As Byte, ByVal Target As RectangleF) As List(Of Byte)

        ' 두 사각형 영역의 교차 영역을 계산한다.
        Dim RectI As RectangleF = RectangleF.Intersect(Origin, Target)

        ' 교차 영역이 비어있다면, 함수를 종료한다.
        If RectI.IsEmpty Then Return Nothing

        ' 시작 점 및 끝 점을 저장할 변수 선언
        Dim startX As Int32,
            endX As Int32,
            startY As Int32,
            endY As Int32

        ' 원본 영역의 X 좌표값이 교차 영역의 X 값보다 크거나 같을 경우
        If Origin.X >= RectI.X Then
            startX = 0
            endX = RectI.Width
        Else ' 원본 영역의 X 좌표값이 교차 영역의 X 값보다 작은 경우
            startX = RectI.X - Origin.X
            endX = startX + RectI.Width
        End If

        ' 원본 영역의 Y 좌표값이 교차 영역의 Y 값보다 크거나 같을 경우
        If Origin.Y >= RectI.Y Then
            startY = 0
            endY = RectI.Height
        Else ' 원본 영역의 Y 좌표값이 교차 영역의 Y 값보다 작은 경우
            startY = RectI.Y - Origin.Y
            endY = startY + RectI.Height
        End If

        ' 부분 투명도 맵을 저장하기 위해 목록 변수를 선언하고,
        Dim alphaList As New List(Of Byte)

        ' 시작 점부터 끝 점까지 반복하면서, 투명도 값을 추가한다.
        For i As Int32 = startX To endX - 1
            For j As Int32 = startY To endY - 1
                alphaList.Add(AlphaMap((j * Origin.Width) + i))
            Next
        Next

        Return alphaList

    End Function

    Public Shared Function HealthBar(ByVal Size As Size, ByVal Current As Int32, ByVal Maximum As Int32, ByVal BkColor As Color, ByVal HpColor As Color) As Image

        Dim img As New Drawing.Bitmap(Size.Width, Size.Height)
        Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(img),
            HpPercentage As Single = Current / Maximum * 100

        g.Clear(BkColor)
        g.FillRectangle(New Drawing.SolidBrush(HpColor), 0, 0, CInt(Int((Size.Width / 100) * HpPercentage)), Size.Height)
        g.DrawRectangle(Pens.Black, New Drawing.Rectangle(Drawing.Point.Empty, Size - New Drawing.Size(1, 1)))
        g.Dispose()
        Return img

    End Function

    ''' <summary>
    ''' 각도 단위를 라디안으로 변환합니다.
    ''' </summary>
    ''' <param name="degree">라디안으로 바꿀 각도 단위를 입력합니다.</param>
    Public Shared Function DegToRad(ByVal degree As Double) As Double
        Return degree * (Math.PI / 180.0#)
        'Return degree * 0.01745329
    End Function

    ''' <summary>
    ''' 라디안 단위를 각도 단위로 변환합니다.
    ''' </summary>
    ''' <param name="radian">각도 단위로 바꿀 라디안 단위를 입력합니다.</param>
    Public Shared Function RadToDeg(ByVal radian As Double) As Double
        Return radian * (180 / Math.PI)
    End Function

    ''' <summary>
    ''' 이미지를 스트림으로 변환합니다.
    ''' </summary>
    ''' <param name="Img">변환할 이미지를 입력합니다.</param>
    Public Shared Function ImageToStream(ByVal Img As Image) As IO.Stream

        Dim imgStream As New IO.MemoryStream
        Img.Save(imgStream, Img.RawFormat)
        imgStream.Position = 0
        Return imgStream

    End Function

    ''' <summary>
    ''' 프레임 레이트(화면 갱신률)를 가져옵니다.
    ''' </summary>
    Public Shared ReadOnly Property FrameRate() As Int32
        Get
            Static iLastTickCount As Int32 = 0
            Static iCurrentFrameRate As Int32 = 0
            Static iStackedFrameRate As Int32 = 0
            Dim TempTickCount As Int32 = Environment.TickCount

            ' 1초 단위로 프레임레이트를 갱신한다.
            If TempTickCount - iLastTickCount >= 1000 Then

                iStackedFrameRate = iCurrentFrameRate
                iCurrentFrameRate = 0
                iLastTickCount = TempTickCount

            End If

            iCurrentFrameRate += 1

            Return iStackedFrameRate

        End Get

    End Property

    ''' <summary>
    ''' 이미지를 텍스쳐로 변환합니다.
    ''' </summary>
    ''' <param name="Device">텍스쳐를 만들 DirectX 장치를 입력합니다.</param>
    ''' <param name="Img">변환할 이미지를 입력합니다.</param>
    Public Shared Function TextureFromImage(ByVal Device As D3.Device, ByVal Img As System.Drawing.Image) As D3.Texture

        Return D3.Texture.FromBitmap(Device, Img, 0, Microsoft.DirectX.Direct3D.Pool.Managed)

    End Function

    ''' <summary>
    ''' 각도의 X, Y 좌표 값을 계산합니다.
    ''' </summary>
    ''' <param name="Angle">좌표 값을 계산할 각도를 입력합니다.</param>
    Public Shared Function GetAngleVector(ByVal Angle As Single) As Vector2D

        Return New PointF(
                            -Math.Sin(DegToRad(Angle)),
                            Math.Cos(DegToRad(Angle))
                         )

    End Function

    ''' <summary>
    ''' System.Drawing.Point 개체를 Vector2 개체로 변환합니다.
    ''' </summary>
    ''' <param name="p">변환할 System.Drawing.Point 개체를 입력합니다.</param>
    Public Shared Function PointToVector2(ByVal p As Point) As Vector2
        Return New Vector2(p.X, p.Y)
    End Function

    ''' <summary>
    ''' System.Drawing.PointF 개체를 Vector2 개체로 변환합니다.
    ''' </summary>
    ''' <param name="p">변환할 System.Drawing.PointF 개체를 입력합니다.</param>
    Public Shared Function PointFToVector2(ByVal p As Vector2D) As Vector2
        Return New Vector2(p.X, p.Y)
    End Function

End Class