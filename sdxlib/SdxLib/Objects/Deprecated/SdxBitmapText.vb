Imports D3 = Microsoft.DirectX.Direct3D
Imports DX = Microsoft.DirectX

Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Imports System.Xml
Imports System.Reflection
Imports System.Windows.Forms

''' <summary>
''' 비트맵을 이용하여 텍스트를 그리는 작업을 구현합니다.
''' </summary>
Public Class SdxBitmapText
    Inherits SdxGraphicsObject
    Implements IGraphicsResource, IGraphicsText

#Region " - Structure & Enum - "

    Private Enum GlyphFlags
        None = 0
        ForceWhite = 1
        ' force the drawing color for this glyph to be white.
    End Enum
    Private Structure GlyphInfo
        Public nBitmapID As UShort
        Public pxLocX As UInt16
        Public pxLocY As UInt16
        Public pxWidth As UInt16
        Public pxHeight As UInt16
        Public pxAdvanceWidth As UInt16
        Public pxLeftSideBearing As SByte
        Public nFlags As GlyphFlags
    End Structure
    Private Structure BitmapInfo
        Public strFilename As String
        Public nX As Integer, nY As Integer
    End Structure

#End Region

#Region " - Fields - "

    Private g_Color As Drawing.Color = Drawing.Color.White
    Private g_Location As Drawing.PointF = Drawing.PointF.Empty
    Private g_Text As String = vbNullString
    Private g_Kerning As Boolean = False
    Private g_BaseLine As Int32 = 0
    Private g_LineHeight As Int32 = 0
    Private g_TextAlign As Windows.Forms.HorizontalAlignment = Windows.Forms.HorizontalAlignment.Left
    Private g_Opacity As Single = 1.0F

    Private g_File As String = vbNullString
    Private g_Name As String = vbNullString
    Private g_Path As String = vbNullString

    Private m_BitmapInfo As New Dictionary(Of Int32, BitmapInfo)
    Private m_BitmapTexture As New Dictionary(Of Int32, D3.Texture)
    Private m_UnicodeGlyph As New Dictionary(Of Char, GlyphInfo)
    Private m_KernInfo As New Dictionary(Of Char, Dictionary(Of Char, SByte))
    Private m_SurfaceSprite As D3.Sprite

#End Region

#Region " - Properties - "

    ''' <summary>
    ''' 텍스트의 색을 가져오거나 설정합니다.
    ''' </summary>
    Public Property ForeColor As Drawing.Color Implements IGraphicsText.ForeColor
        Get
            Return g_Color
        End Get
        Set(ByVal value As Drawing.Color)
            g_Color = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Location As Drawing.PointF Implements IGraphicsText.Location
        Get
            Return g_Location
        End Get
        Set(ByVal value As Drawing.PointF)
            g_Location = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Text As String Implements IGraphicsText.Text
        Get
            Return g_Text
        End Get
        Set(ByVal value As String)
            g_Text = value
        End Set
    End Property

    ''' <summary>
    ''' 커닝을 활성화할 것인지에 대한 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Kerning As Boolean
        Get
            Return g_Kerning
        End Get
        Set(ByVal value As Boolean)
            g_Kerning = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트를 정렬하는 방법을 가져오거나 설정합니다.
    ''' </summary>
    Public Property TextAlign As Windows.Forms.HorizontalAlignment
        Get
            Return g_TextAlign
        End Get
        Set(ByVal value As Windows.Forms.HorizontalAlignment)
            g_TextAlign = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 투명도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Opacity As Single Implements IGraphicsText.Opacity
        Get
            Return g_Opacity
        End Get
        Set(ByVal value As Single)
            If value > 1.0 Then value = 1
            If value < 0 Then value = 0
            g_Opacity = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트 개체가 비트맵 폰트를 지원하는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property SupportsBitmapFont As Boolean Implements IGraphicsText.SupportsBitmapFont
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' 텍스트의 X 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property X As Single Implements IGraphicsText.X
        Get
            Return g_Location.X
        End Get
        Set(ByVal value As Single)
            g_Location.X = value
        End Set
    End Property

    ''' <summary>
    ''' 텍스트의 Y 좌표 위치를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Y As Single Implements IGraphicsText.Y
        Get
            Return g_Location.Y
        End Get
        Set(ByVal value As Single)
            g_Location.Y = value
        End Set
    End Property



    ''' <summary>
    ''' 기본 줄의 간격을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property BaseLine As Int32
        Get
            Return g_BaseLine
        End Get
    End Property

    ''' <summary>
    ''' 줄의 높이를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property LineHeight As Int32
        Get
            Return g_LineHeight
        End Get
    End Property

    ''' <summary>
    ''' 폰트의 이름을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Name() As String
        Get
            Return g_Name
        End Get
    End Property

    ''' <summary>
    ''' 폰트의 파일 이름을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property FileName() As String
        Get
            Return g_File
        End Get
    End Property

#End Region

#Region " - Private Methods - "

    Private Sub LoadXml(ByVal XFontFile As String)

        Dim XmlDoc As New XmlDocument()

        ' 디스크에 파일이 있는 경우
        If IO.File.Exists(XFontFile) Then

            ' 파일의 경로를 가져온다.
            g_Path = Replace$(IO.Path.GetDirectoryName(XFontFile) & "\", "\\", "\")
            g_File = XFontFile

            ' XML 문서를 불러온다.
            XmlDoc.Load(XFontFile)

            ' 디스크에 파일이 없는 경우
        Else

            Dim EmbedPath As String = Nothing,
             EmbedName As String = Nothing

            ' 경로를 변환한다.
            Call ConvertFilePath2EmbeddedPath(XFontFile, EmbedPath, EmbedName)

            ' 리소스 스트림을 가져온다.
            Dim s As IO.Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmbedPath & EmbedName)

            ' 리소스 스트림을 가져오지 못한 경우
            If s Is Nothing Then
                Throw New IO.InvalidDataException(String.Format("'{0}' - 지정된 이름을 가진 폰트를 찾지 못했습니다.", XFontFile))
            End If

            g_Path = EmbedPath
            g_File = EmbedName

            XmlDoc.Load(s)

        End If

        g_Name = Nothing
        LoadFontXML(XmlDoc.ChildNodes)

        If String.IsNullOrEmpty(g_Name) Then g_Name = IO.Path.GetFileNameWithoutExtension(XFontFile)

        For Each k As KeyValuePair(Of Int32, BitmapInfo) In m_BitmapInfo
            m_BitmapTexture(k.Key) = D3.TextureLoader.FromFile(Me.GraphicsDevice, g_Path & k.Value.strFilename)
        Next

    End Sub
    Private Sub ConvertFilePath2EmbeddedPath(ByVal strFilePath As String, ByRef strEmbeddedPath As String, ByRef strEmbeddedName As String)
        Dim a As Assembly = Assembly.GetExecutingAssembly()
        ' calc the resource path to this font (strip off <fontname>.xml)
        strEmbeddedPath = a.GetName().Name & "."

        ' strip directory nams from the filepath and add them to the embedded path
        Dim aPath As String() = strFilePath.Split(New Char() {"/"c, "\"c})
        For i As Integer = 0 To aPath.Length - 2
            strEmbeddedPath += aPath(i) & "."
        Next
        strEmbeddedName = aPath(aPath.Length - 1)
    End Sub
    Private Sub CountCharWidth(ByVal pxMaxWidth As Integer, ByVal str As String, ByRef nChars As Integer, ByRef pxWidth As Integer)

        Dim nLastWordBreak As Integer = 0
        Dim pxLastWordBreakWidth As Integer = 0
        Dim pxLastWidth As Integer = 0
        Dim cLast As Char = ControlChars.NullChar

        nChars = 0
        pxWidth = 0

        For Each c As Char In str
            ' if this is a newline, then return. the width is set correctly
            If c = ControlChars.Lf Then
                nChars += 1
                Return
            End If

            Dim gInfo As GlyphInfo
            ' 문자 모양 정보가 없는 경우, 물음표로 처리한다.
            If Not m_UnicodeGlyph.ContainsKey(c) Then
                gInfo = m_UnicodeGlyph("?"c)
            Else
                gInfo = m_UnicodeGlyph(c)
            End If

            ' 커닝이 활성화된 경우, 커닝 값을 적용한다.
            If Me.Kerning Then
                pxWidth += CalculateKern(cLast, c)
                cLast = c
            End If

            ' update the string width and char count
            pxLastWidth = pxWidth
            pxWidth += gInfo.pxAdvanceWidth
            nChars += 1

            ' record the end of the previous word if this is a whitespace char
            If [Char].IsWhiteSpace(c) Then
                nLastWordBreak = nChars
                ' include space in char count
                ' don't include space in width
                pxLastWordBreakWidth = pxLastWidth
            End If

            ' if we've exceeded the max, then return the chars up to the last complete word
            If pxWidth > pxMaxWidth Then
                pxWidth = pxLastWordBreakWidth
                If pxWidth = 0 Then
                    ' fallback to last char if we haven't seen a complete word
                    pxWidth = pxLastWidth
                    nChars -= 1
                Else
                    nChars = nLastWordBreak
                End If
                Return
            End If
        Next
    End Sub
    Private Sub DrawTextInternal(ByVal Target As D3.Sprite, ByVal Location As Drawing.PointF, ByVal Color As Drawing.Color, ByVal Text As String)

        If String.IsNullOrEmpty(Text) Then Return

        Dim Origin As Drawing.PointF = Drawing.PointF.Empty,
            LastChar As Char = ControlChars.NullChar

        ' 문자열에 있는 각 문자마다 반복한다.
        For Each c As Char In Text

            Dim gInfo As GlyphInfo
            ' 문자 모양 정보가 없는 경우, 물음표로 처리한다.
            If Not m_UnicodeGlyph.ContainsKey(c) Then
                gInfo = m_UnicodeGlyph("?"c)
            Else
                gInfo = m_UnicodeGlyph(c)
            End If

            ' 커닝이 활성화된 경우, 커닝 값을 적용한다.
            If Me.Kerning Then
                Location.X += CalculateKern(LastChar, c)
                LastChar = c
            End If

            ' 문자를 그린다.
            Location.X += gInfo.pxLeftSideBearing
            If gInfo.pxWidth <> 0 AndAlso gInfo.pxHeight <> 0 Then
                Dim Source As New Drawing.Rectangle(gInfo.pxLocX, gInfo.pxLocY, gInfo.pxWidth, gInfo.pxHeight),
                    TextColor As Drawing.Color = IIf(gInfo.nFlags And GlyphFlags.ForceWhite, Drawing.Color.White, Color)

                TextColor = Drawing.Color.FromArgb(Me.Opacity * 255, TextColor.R, TextColor.G, TextColor.B)
                Target.Draw2D(m_BitmapTexture(gInfo.nBitmapID), Source, Drawing.SizeF.Empty, Location, TextColor)

            End If

            Location.X += gInfo.pxAdvanceWidth - gInfo.pxLeftSideBearing

        Next

    End Sub
    Private Function CalculateKern(ByVal chLeft As Char, ByVal chRight As Char) As Integer
        If m_KernInfo.ContainsKey(chLeft) Then
            Dim kern2 As Dictionary(Of Char, SByte) = m_KernInfo(chLeft)
            If kern2.ContainsKey(chRight) Then
                Return kern2(chRight)
            End If
        End If
        Return 0
    End Function

#End Region

#Region " - Constructor - "

    ''' <summary>
    ''' 비트맵 텍스트 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Device">주 DirectX 장치를 입력합니다.</param>
    ''' <param name="Sprite">텍스트를 그릴 대상 스프라이트를 입력합니다.</param>
    ''' <param name="XFontFile">비트맵 폰트에 대한 정보를 저장하고 있는 XML 파일의 경로를 입력합니다.</param>
    Friend Sub New(ByVal Device As D3.Device, ByVal Sprite As D3.Sprite, ByVal XFontFile As String)

        MyBase.New(Device)
        LoadXml(XFontFile)
        m_SurfaceSprite = Sprite

    End Sub

#End Region

#Region " - Public Methods - "

    ''' <summary>
    ''' 주어진 문자열의 넓이를 계산합니다.
    ''' </summary>
    ''' <param name="Text">계산할 문자열을 입력합니다.</param>
    ''' <param name="args">형식으로 사용할 값들을 입력합니다.</param>
    Public Function MeasureString(ByVal Text As String, ByVal ParamArray args As Object()) As Integer
        Dim str As String = String.Format(Text, args)
        Dim pxWidth As Integer = 0
        Dim cLast As Char = ControlChars.NullChar

        For Each c As Char In str

            Dim gInfo As GlyphInfo
            ' 문자 모양 정보가 없는 경우, 물음표로 처리한다.
            If Not m_UnicodeGlyph.ContainsKey(c) Then
                gInfo = m_UnicodeGlyph("?"c)
            Else
                gInfo = m_UnicodeGlyph(c)
            End If

            ' 커닝이 활성화된 경우, 커닝 값을 적용한다.
            If Me.Kerning Then
                pxWidth += CalculateKern(cLast, c)
                cLast = c
            End If

            ' update the string width
            pxWidth += gInfo.pxAdvanceWidth
        Next

        Return pxWidth
    End Function

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    Public Overloads Sub DrawText() Implements IGraphicsText.Draw

        DrawTextInternal(m_SurfaceSprite, Me.Location, Me.ForeColor, Me.Text)

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawText(ByVal Text As String, ByVal ParamArray Args() As Object)

        DrawTextInternal(m_SurfaceSprite, Me.Location, Me.ForeColor, String.Format(Text, Args))

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawText(ByVal Color As Drawing.Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        DrawTextInternal(m_SurfaceSprite, Me.Location, Color, String.Format(Text, Args))

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawText(ByVal Location As Drawing.PointF, ByVal Text As String, ByVal ParamArray Args() As Object)

        DrawTextInternal(m_SurfaceSprite, Location, Me.ForeColor, String.Format(Text, Args))

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawText(ByVal X As Single, ByVal Y As Single, ByVal Text As String, ByVal ParamArray Args() As Object)

        DrawTextInternal(m_SurfaceSprite, New Drawing.PointF(X, Y), Me.ForeColor, String.Format(Text, Args))

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawText(ByVal Location As Drawing.PointF, ByVal Color As Drawing.Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        DrawTextInternal(m_SurfaceSprite, Location, Color, String.Format(Text, Args))

    End Sub

    ''' <summary>
    ''' 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawText(ByVal X As Single, ByVal Y As Single, ByVal Color As Drawing.Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        DrawTextInternal(m_SurfaceSprite, New Drawing.PointF(X, Y), Color, String.Format(Text, Args))

    End Sub



    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Size">텍스트가 그려질 사각형의 크기를 입력합니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Size As Drawing.SizeF)

        Dim Text As String = Me.Text,
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Me.Location,
            TextRectangle As New Drawing.RectangleF(Me.Location, Size)

        While Text.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, Text, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Text.Substring(0, ValidChar))
            Text = Text.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Width">텍스트가 그려질 사각형의 넓이를 입력합니다.</param>
    ''' <param name="Height">텍스트가 그려질 사각형의 높이를 입력합니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Width As Single, ByVal Height As Single)

        Dim Text As String = Me.Text,
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Me.Location,
            Size As Drawing.SizeF = New Drawing.SizeF(Width, Height),
            TextRectangle As New Drawing.RectangleF(Me.Location, Size)

        While Text.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, Text, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Text.Substring(0, ValidChar))
            Text = Text.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Size">텍스트가 그려질 사각형의 크기를 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Size As Drawing.SizeF, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Me.Location,
            TextRectangle As New Drawing.RectangleF(Me.Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Width">텍스트가 그려질 사각형의 넓이를 입력합니다.</param>
    ''' <param name="Height">텍스트가 그려질 사각형의 높이를 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Width As Single, ByVal Height As Single, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Me.Location,
            Size As Drawing.SizeF = New Drawing.SizeF(Width, Height),
            TextRectangle As New Drawing.RectangleF(Me.Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Size">텍스트가 그려질 사각형의 크기를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Size As Drawing.SizeF, ByVal Color As Drawing.Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Me.Location,
            TextRectangle As New Drawing.RectangleF(Me.Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Color, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Size">텍스트가 그려질 사각형의 크기를 입력합니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Location As Drawing.PointF, ByVal Size As Drawing.SizeF)

        Dim Text As String = Me.Text,
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Location,
            TextRectangle As New Drawing.RectangleF(Location, Size)

        While Text.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, Text, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Text.Substring(0, ValidChar))
            Text = Text.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Width">텍스트가 그려질 사각형의 넓이를 입력합니다.</param>
    ''' <param name="Height">텍스트가 그려질 사각형의 높이를 입력합니다.</param>
    Public Overloads Sub DrawBoxText(ByVal X As Single, ByVal Y As Single, ByVal Width As Single, ByVal Height As Single)

        Dim Text As String = Me.Text,
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = New Drawing.PointF(X, Y),
            Size As Drawing.SizeF = New Drawing.SizeF(Width, Height),
            TextRectangle As New Drawing.RectangleF(Location, Size)

        While Text.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, Text, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Text.Substring(0, ValidChar))
            Text = Text.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Size">텍스트가 그려질 사각형의 크기를 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Location As Drawing.PointF, ByVal Size As Drawing.SizeF, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Location,
            TextRectangle As New Drawing.RectangleF(Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Width">텍스트가 그려질 사각형의 넓이를 입력합니다.</param>
    ''' <param name="Height">텍스트가 그려질 사각형의 높이를 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal X As Single, ByVal Y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = New Drawing.PointF(X, Y),
            Size As Drawing.SizeF = New Drawing.SizeF(Width, Height),
            TextRectangle As New Drawing.RectangleF(Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="Location">텍스트가 그려질 위치를 입력합니다.</param>
    ''' <param name="Size">텍스트가 그려질 사각형의 크기를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal Location As Drawing.PointF, ByVal Size As Drawing.SizeF, ByVal Color As Drawing.Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = Location,
            TextRectangle As New Drawing.RectangleF(Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Color, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

    ''' <summary>
    ''' 사각형 내에 텍스트를 그립니다.
    ''' </summary>
    ''' <param name="X">텍스트가 그려질 X 좌표를 입력합니다.</param>
    ''' <param name="Y">텍스트가 그려질 Y 좌표를 입력합니다.</param>
    ''' <param name="Width">텍스트가 그려질 사각형의 넓이를 입력합니다.</param>
    ''' <param name="Height">텍스트가 그려질 사각형의 높이를 입력합니다.</param>
    ''' <param name="Color">텍스트를 그릴 색을 입력합니다.</param>
    ''' <param name="Text">그릴 문자열을 입력합니다.</param>
    ''' <param name="Args">서식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
    Public Overloads Sub DrawBoxText(ByVal X As Single, ByVal Y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Color As Drawing.Color, ByVal Text As String, ByVal ParamArray Args() As Object)

        Dim FormatText As String = String.Format(Text, Args),
            ValidChar As Int32,
            ValidWidth As Int32,
            TextLocation As Drawing.PointF = New Drawing.PointF(X, Y),
            Size As Drawing.SizeF = New Drawing.SizeF(Width, Height),
            TextRectangle As New Drawing.RectangleF(Location, Size)

        While FormatText.Length

            ' 문자열의 높이가 사각형의 높이보다 클 경우, 그리지 않는다.
            If TextLocation.Y + Me.LineHeight > TextRectangle.Bottom Then Return

            ' 문자열의 넓이를 계산한다.
            Call CountCharWidth(TextLocation.X + Size.Width, FormatText, ValidChar, ValidWidth)

            ' 정렬 방법에 따라 계산한다.

            Select Case Me.TextAlign
                Case Windows.Forms.HorizontalAlignment.Left
                    TextLocation.X = TextRectangle.Left
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Center
                    TextLocation.X = TextRectangle.Left + ((TextRectangle.Width - ValidWidth) / 2)
                    Exit Select
                Case Windows.Forms.HorizontalAlignment.Right
                    TextLocation.X = TextRectangle.Left + (TextRectangle.Width - ValidWidth)
                    Exit Select
            End Select

            DrawText(TextLocation, Color, FormatText.Substring(0, ValidChar))
            FormatText = FormatText.Substring(ValidChar)
            TextLocation.Y += Me.LineHeight

        End While

    End Sub

#End Region

#Region " - XmlFont Loader - "

    Private Sub LoadFontXML(ByVal xnl As XmlNodeList)
        For Each xn As XmlNode In xnl
            If xn.Name = "font" Then
                g_Name = GetXMLAttribute(xn, "name")
                g_BaseLine = Int32.Parse(GetXMLAttribute(xn, "base"))
                g_LineHeight = Int32.Parse(GetXMLAttribute(xn, "height"))

                LoadFontXML_font(xn.ChildNodes)
            End If
        Next
    End Sub
    Private Sub LoadFontXML_font(ByVal xnl As XmlNodeList)
        For Each xn As XmlNode In xnl
            If xn.Name = "bitmaps" Then
                LoadFontXML_bitmaps(xn.ChildNodes)
            End If
            If xn.Name = "glyphs" Then
                LoadFontXML_glyphs(xn.ChildNodes)
            End If
            If xn.Name = "kernpairs" Then
                LoadFontXML_kernpairs(xn.ChildNodes)
            End If
        Next
    End Sub
    Private Sub LoadFontXML_bitmaps(ByVal xnl As XmlNodeList)
        For Each xn As XmlNode In xnl
            If xn.Name = "bitmap" Then
                Dim strID As String = GetXMLAttribute(xn, "id")
                Dim strFilename As String = GetXMLAttribute(xn, "name")
                Dim strSize As String = GetXMLAttribute(xn, "size")
                Dim aSize As String() = strSize.Split("x"c)

                Dim bminfo As BitmapInfo
                bminfo.strFilename = strFilename
                bminfo.nX = Int32.Parse(aSize(0))
                bminfo.nY = Int32.Parse(aSize(1))

                m_BitmapInfo(Int32.Parse(strID)) = bminfo
            End If
        Next
    End Sub
    Private Sub LoadFontXML_glyphs(ByVal xnl As XmlNodeList)
        For Each xn As XmlNode In xnl
            If xn.Name = "glyph" Then
                Dim strChar As String = GetXMLAttribute(xn, "ch")
                Dim strBitmapID As String = GetXMLAttribute(xn, "bm")
                Dim strLoc As String = GetXMLAttribute(xn, "loc")
                Dim strSize As String = GetXMLAttribute(xn, "size")
                Dim strAW As String = GetXMLAttribute(xn, "aw")
                Dim strLSB As String = GetXMLAttribute(xn, "lsb")
                Dim strForceWhite As String = GetXMLAttribute(xn, "forcewhite")

                If strLoc = "" Then
                    strLoc = GetXMLAttribute(xn, "origin")
                End If
                ' obsolete - use loc instead
                Dim aLoc As String() = strLoc.Split(","c)
                Dim aSize As String() = strSize.Split("x"c)

                Dim ginfo As New GlyphInfo()
                ginfo.nBitmapID = UInt16.Parse(strBitmapID)
                ginfo.pxLocX = UInt16.Parse(aLoc(0))
                ginfo.pxLocY = UInt16.Parse(aLoc(1))
                ginfo.pxWidth = UInt16.Parse(aSize(0))
                ginfo.pxHeight = UInt16.Parse(aSize(1))
                ginfo.pxAdvanceWidth = UInt16.Parse(strAW)
                ginfo.pxLeftSideBearing = [SByte].Parse(strLSB)
                ginfo.nFlags = 0
                ginfo.nFlags = ginfo.nFlags Or (If(strForceWhite = "true", GlyphFlags.ForceWhite, GlyphFlags.None))

                m_UnicodeGlyph(strChar(0)) = ginfo
            End If
        Next
    End Sub
    Private Sub LoadFontXML_kernpairs(ByVal xnl As XmlNodeList)
        For Each xn As XmlNode In xnl
            If xn.Name = "kernpair" Then
                Dim strLeft As String = GetXMLAttribute(xn, "left")
                Dim strRight As String = GetXMLAttribute(xn, "right")
                Dim strAdjust As String = GetXMLAttribute(xn, "adjust")

                Dim chLeft As Char = strLeft(0)
                Dim chRight As Char = strRight(0)

                ' create a kern dict for the left char if needed
                If Not m_KernInfo.ContainsKey(chLeft) Then
                    m_KernInfo(chLeft) = New Dictionary(Of Char, SByte)()
                End If

                ' add the right char to the left char's kern dict
                Dim kern2 As Dictionary(Of Char, SByte) = m_KernInfo(chLeft)
                kern2(chRight) = [SByte].Parse(strAdjust)
            End If
        Next
    End Sub
    Private Function GetXMLAttribute(ByVal n As XmlNode, ByVal strAttr As String) As String
        Dim attr As XmlAttribute = TryCast(n.Attributes.GetNamedItem(strAttr), XmlAttribute)
        If attr IsNot Nothing Then
            Return attr.Value
        End If
        Return ""
    End Function

#End Region

#Region " - IGraphicsResource - "

    Private g_Disposed As Boolean = False

    ''' <summary>
    ''' 리소스의 사용을 종료하고, 메모리에서 해제합니다.
    ''' </summary>
    Public Sub Dispose() Implements IGraphicsResource.Dispose

        If g_Disposed Then Exit Sub
        For Each k As Int32 In m_BitmapTexture.Keys
            m_BitmapTexture(k).Dispose()
        Next
        g_Disposed = True

    End Sub

    ''' <summary>
    ''' 리소스의 사용이 종료되고, 메모리에서 해제되었는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsResource.Disposed
        Get
            Return g_Disposed
        End Get
    End Property

#End Region

#Region " - Obsoleted part of IGraphicsResource - "

    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty1 As System.Drawing.Color Implements IGraphicsResource.Color
        Get
            Return Nothing
        End Get
        Set(ByVal value As System.Drawing.Color)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty2 As Integer Implements IGraphicsResource.Height
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty3 As System.Drawing.PointF Implements IGraphicsResource.Location
        Get
            Return Nothing
        End Get
        Set(ByVal value As System.Drawing.PointF)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty4 As Single Implements IGraphicsResource.Opacity
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty5 As System.Drawing.RectangleF Implements IGraphicsResource.Rectangle
        Get

        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty6 As System.Drawing.Size Implements IGraphicsResource.Size
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty7 As Microsoft.DirectX.Direct3D.Texture Implements IGraphicsResource.Texture
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public ReadOnly Property ObsoletedProperty8 As Integer Implements IGraphicsResource.Width
        Get
            Return Nothing
        End Get
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty9 As Single Implements IGraphicsResource.X
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property ObsoletedProperty10 As Single Implements IGraphicsResource.Y
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property

#End Region

    Public ReadOnly Property ResourceType As ResourceTypes Implements IGraphicsResource.ResourceType
        Get

        End Get
    End Property

End Class