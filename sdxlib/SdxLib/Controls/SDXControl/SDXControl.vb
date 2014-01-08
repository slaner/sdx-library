Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Namespace Controls

    ''' <summary>
    ''' 그래픽 컴포넌트 요소를 구현합니다.
    ''' </summary>
    Public Class SDXControl
        Inherits SdxObject

        ''' <summary>
        ''' 요소가 나타내는 제목을 저장합니다.
        ''' </summary>
        Private g_Text As String

        ''' <summary>
        ''' 요소의 위치를 저장합니다.
        ''' </summary>
        Private g_Location As Point

        ''' <summary>
        ''' 요소의 배경 색을 저장합니다.
        ''' </summary>
        Private g_BackColor As Color = Color.White

        ''' <summary>
        ''' 요소의 크기를 저장합니다.
        ''' </summary>
        Private g_Size As Size

        ''' <summary>
        ''' 요소의 투명도를 저장합니다.
        ''' </summary>
        Private g_Opacity As Byte

        ''' <summary>
        ''' 요소의 문자열 색을 저장합니다.
        ''' </summary>
        Private g_ForeColor As Color = Color.Black

        ''' <summary>
        ''' 요소의 글꼴을 저장합니다.
        ''' </summary>
        Private g_Font As Drawing.Font

        ''' <summary>
        ''' 요소의 텍스트 정렬 방식을 저장합니다.
        ''' </summary>
        Private g_TextAlignment As TextAlignment = TextAlignment.VerticalCenter Or TextAlignment.Center

        ''' <summary>
        ''' 글꼴의 높이를 저장합니다.
        ''' </summary>
        Private g_FontHeight As Single

        ''' <summary>
        ''' [Internal Access] 점 문자의 넓이를 저장합니다.
        ''' </summary>
        Private m_DotWidth As Int32

        ''' <summary>
        ''' 포커스가 있는지의 여부를 저장합니다.
        ''' </summary>
        Private g_HaveFocus As Boolean = False

        ''' <summary>
        ''' 컨트롤이 활성화되었는지의 여부를 저장합니다.
        ''' </summary>
        Private g_Enabled As Boolean = True

        ''' <summary>
        ''' 컨트롤의 배경 이미지를 저장합니다.
        ''' </summary>
        Private g_BackgroundImage As Image = Nothing



        ''' <summary>
        ''' 마우스 메세지를 처리할 것인지의 여부를 저장합니다.
        ''' </summary>
        Private g_DoNotProcessMouseMessages As Boolean = False

        ''' <summary>
        ''' 키보드 메세지를 처리할 것인지의 여부를 저장합니다.
        ''' </summary>
        Private g_DoNotProcessKeyboardMessages As Boolean = False

        ''' <summary>
        ''' 마우스 왼쪽 버튼이 눌렸는지 여부를 저장합니다.
        ''' </summary>
        Private m_LMouseDown As Boolean = False

        ''' <summary>
        ''' 마우스 중간 버튼이 눌렸는지 여부를 저장합니다.
        ''' </summary>
        Private m_MMouseDown As Boolean = False

        ''' <summary>
        ''' 마우스 오른쪽 버튼이 눌렸는지 여부를 저장합니다.
        ''' </summary>
        Private m_RMouseDown As Boolean = False

        ''' <summary>
        ''' 마우스를 캡쳐중인지 여부를 저장합니다.
        ''' </summary>
        Private m_Holding As Boolean = False

        ''' <summary>
        ''' 컨트롤 내부에 마우스가 들어왔는지 여부를 저장합니다.
        ''' </summary>
        Private m_MouseEntered As Boolean = False

        ''' <summary>
        ''' DirectX Font 개체를 저장합니다.
        ''' </summary>
        Friend m_Font As D3.Font



    End Class

End Namespace