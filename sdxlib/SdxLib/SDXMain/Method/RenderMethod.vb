Imports System.Runtime.InteropServices
Imports SDXLib.WinAPI
Partial Class SDXMain

    ''' <summary>
    ''' 렌더링을 시작합니다.
    ''' </summary>
    Public Sub Run()

        If Not g_GameRunning Then

            g_GameRunning = True
            Dim incomeMsg As WinAPI.MSG
            Do While g_GameRunning

                If WinAPI.PeekMessage(incomeMsg, 0, 0, 0, WinAPI.PM_REMOVE) Then

                    If incomeMsg.Msg = WindowsMessages.WM_QUIT Then
                        g_GameRunning = False
                    End If
                    TranslateMessage(incomeMsg)
                    DispatchMessage(incomeMsg)

                Else

                PresentBackBuffer()

                End If

            Loop

        End If


    End Sub

    ''' <summary>
    ''' 한 프레임을 그립니다.
    ''' </summary>
    Public Sub RunOnce()

        PresentBackBuffer()

    End Sub

    ''' <summary>
    ''' 렌더링을 중단합니다.
    ''' </summary>
    Public Sub [Stop]()

        g_GameRunning = False

    End Sub

End Class