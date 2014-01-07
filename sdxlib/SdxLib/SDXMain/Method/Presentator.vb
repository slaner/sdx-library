Imports DI = Microsoft.DirectX.DirectInput
Imports Microsoft.DirectX.Direct3D

Partial Class SDXMain

    Private Sub PresentBackBuffer()

        ' 게임이 실행 중이 아니라면,
        ' 그리기 함수를 실행하지 않음
        If Not g_GameRunning Then Return

        ' 키보드 상태를 가져온다.
        Dim GlobalKs As DI.KeyboardState = GetKeybdState()

        ' 마우스 상태를 업데이트 한다.
        g_MouseState.Poll()

        g_DrawingFrame = True

        ' 백버퍼에 그래픽 작업을 하기 위해 씬을 시작하고,
        ' OnBeginScene 이벤트를 발생시킨다.
        m_Device.BeginScene()
        RaiseEvent OnBeginScene()

        ' 백버퍼를 배경색으로 칠한다.
        m_Device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, m_BackColor, 0, 0)

        ' 배경 이미지:
        ProcessBGI()

        ' OnPaintBackground 이벤트를 발생시킨다.
        RaiseEvent OnBackgroundPainted()

        ' OnImageSpriteBegin 이벤트를 발생시킨다.
        m_ImageSprite.Begin(SpriteFlags.AlphaBlend)
        RaiseEvent OnImageSpriteBegin(m_ImageSprite)


        ' 비트맵 텍스트를 출력한다.
        For Each gt As IGraphicsText In g_TextList
            If gt.SupportsBitmapFont Then gt.Draw()
        Next

        ' 2D 블록 그리기
        For i As Int32 = g_BlockList.Count - 1 To 0 Step -1
            If g_BlockList(i).Disposed Then
                g_BlockList.RemoveAt(i)
            Else
                g_BlockList(i).Draw(m_ImageSprite)
            End If
        Next

        ' 플레이어 그리기
        For i As Int32 = g_ControlPlayers.Count - 1 To 0 Step -1
            If g_ControlPlayers(i).Disposed Then
                g_ControlPlayers.RemoveAt(i)
            Else
                g_ControlPlayers(i).Draw(GlobalKs, m_ImageSprite)
            End If
        Next

        ' 효과 그리기
        For i As Int32 = g_Effects.Count - 1 To 0 Step -1
            If g_Effects(i).Disposed Then
                g_Effects.RemoveAt(i)
            Else
                g_Effects(i).Draw(m_ImageSprite)
            End If
        Next

        ' 총알 그리기
        For i As Int32 = g_Bullets.Count - 1 To 0 Step -1
            If g_Bullets(i).Disposed Then
                g_Bullets.RemoveAt(i)
            Else
                g_Bullets(i).Draw(m_ImageSprite)
            End If
        Next

        ' 메뉴 그리기
        For i As Int32 = g_Controls.Count - 1 To 0 Step -1
            g_Controls(i).DrawControl(m_ImageSprite)
        Next

        ' 선을 그린다.
        ProcessLine()

        ' 텍스쳐 그리기가 끝났으므로, 텍스쳐를 종료하고,
        ' OnImageSpriteEnd 이벤트를 발생시킨다.
        m_ImageSprite.End()
        RaiseEvent OnImageSpriteEnd()

        m_ControlTextRenderingSprite.Begin(SpriteFlags.AlphaBlend)
        For i As Int32 = g_Controls.Count - 1 To 0 Step -1
            g_Controls(i).DrawControlText(m_ControlTextRenderingSprite)
        Next
        m_ControlTextRenderingSprite.End()


        ProcessText()


        RaiseEvent OnEndScene()
        m_Device.EndScene()

        ' 백 버퍼의 내용을 화면으로 출력하기 전에
        ' BeforePresent 이벤트를 발생시킨 뒤, Present 메서드를 호출한다.
        RaiseEvent BeforePresent()
        m_Device.Present()

        ' Present 메서드 호출 후에 프레임을 그렸다는 것을 알리기 위해,
        ' OnDrawFrame 이벤트를 발생시킨다.
        g_FrameRate = SDXHelper.FrameRate
        RaiseEvent OnDrawFrame(Me.FrameRate())


        g_DrawingFrame = False

    End Sub



    Private Function GetKeybdState() As DI.KeyboardState

        Dim kbdState As DI.KeyboardState = Nothing
        Try
            m_Keyboard.Acquire()
            kbdState = m_Keyboard.GetCurrentKeyboardState()
        Catch ex As Exception
        End Try

        If kbdState IsNot Nothing Then
            Return kbdState
        End If
        Return Nothing

    End Function
    Private Sub ProcessBGI()

        If Me.BackgroundImage IsNot Nothing Then

            m_BGISprite.Begin(SpriteFlags.None)
            If g_BGILayout = ImageLayout.Default Then
                m_BGISprite.Draw2D(m_BgiTexture, Vector2D.Empty, 0, Vector2D.Empty, Drawing.Color.White)
            Else
                m_BGISprite.Draw2D(m_BgiTexture, New Drawing.Rectangle(Drawing.Point.Empty, Drawing.Size.Empty), m_Window.Size, Vector2D.Empty, Drawing.Color.White)
            End If
            m_BGISprite.End()

        End If

    End Sub
    Private Sub ProcessText()

        ' 텍스트 스프라이트를 시작한다.
        m_TextSprite.Begin(SpriteFlags.AlphaBlend)
        RaiseEvent OnTextSpriteBegin()

        ' 텍스트 스프라이트를 종료한다.
        m_TextSprite.End()
        RaiseEvent OnTextSpriteEnd()

    End Sub
    Private Sub ProcessLine()

        For i As Int32 = g_Lines.Count - 1 To 0 Step -1

            ' 개체가 삭제된 경우, 목록에서 지운다.
            If g_Lines(i).Disposed Then
                g_Lines.RemoveAt(i)
                Continue For
            End If

            If g_Lines(i).SupportsTextureMapping Then
                ' 텍스쳐 매핑을 지원하는 경우, 선 스프라이트에 그린다.
                g_Lines(i).Draw(m_ImageSprite)
            Else
                ' 텍스쳐 매핑을 지원하지 않는 경우, Line 개체에 그린다.
                g_Lines(i).Draw(m_Line)
            End If
        Next

    End Sub

End Class