Namespace Devices

    Partial Class SDXMouse

        Private Sub PollInternal()

            If m_Main.Window.InvokeRequired Then

                m_Main.Window.Invoke(m_PollingHandler)

            Else

                g_Polled = False

                If m_Main.Window.Focused Then

                    ' 스크린 좌표계를 가져온다.
                    Call GetCursorPos(g_ScreenLocation)

                    ' 클라이언트 좌표계로 계산한다
                    g_ClientLocation = m_Main.Window.PointToClient(g_ScreenLocation)

                    ' 왼쪽, 오른쪽, 중간 버튼
                    g_ButtonFlag = 0
                    g_ButtonFlag = If(GetAsyncKeyState(1) And -32767, MouseButton.Left, 0) Or
                                   If(GetAsyncKeyState(2) And -32767, MouseButton.Right, 0) Or
                                   If(GetAsyncKeyState(4) And -32767, MouseButton.Middle, 0)

                    g_Polled = True

                End If

            End If

        End Sub

    End Class

End Namespace