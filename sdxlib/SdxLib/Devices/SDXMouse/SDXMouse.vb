Imports System.Drawing
Imports System.Runtime.InteropServices
Namespace Devices

    ''' <summary>
    ''' 마우스의 상태를 저장합니다.
    ''' </summary>
    Public NotInheritable Class SDXMouse

        <DllImport("user32")>
        Private Shared Function GetAsyncKeyState(ByVal vkey As Int32) As Int32
        End Function
        <DllImport("user32")>
        Private Shared Function GetCursorPos(ByRef p As Drawing.Point) As Int32
        End Function

        Private Delegate Sub PollingMouseStateHandler()

        ''' <summary>
        ''' 주 개체를 저장합니다.
        ''' </summary>
        Private m_Main As SDXMain

        ''' <summary>
        ''' 마우스의 스크린 위치를 저장합니다.
        ''' </summary>
        Private g_ScreenLocation As Point

        ''' <summary>
        ''' 마우스의 클라이언트 위치를 저장합니다.
        ''' </summary>
        Private g_ClientLocation As Point

        ''' <summary>
        ''' 마우스의 버튼 상태를 저장합니다.
        ''' </summary>
        Private g_ButtonFlag As Int32

        ''' <summary>
        ''' 폴링 콜백 메서드를 저장합니다.
        ''' </summary>
        Private m_PollingHandler As PollingMouseStateHandler

        Private g_Polled As Boolean

    End Class

End Namespace