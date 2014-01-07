Partial Class SdxLine2D

    Private Function CalculateAlpha(ByRef ref As Single) As Boolean

        ' 현재의 페이드 단계가 최대치를 넘어선 경우:
        If m_CurrentStep > g_FadeStep Then

            ' 페이드 효과가 끝나도 개체를 보존하는 경우:
            If g_Preserve Then

                ' 페이드 효과를 토글하여 적용하는 경우:
                If g_ToggleFadeEffect Then Me.FadeEffect = If(Me.FadeEffect, SDXLib.FadeEffect.FadeOut, SDXLib.FadeEffect.FadeIn)

                ' 페이드 단계를 0으로 초기화한다.
                m_CurrentStep = 0

            Else
                ' 보존하지 않는 경우:
                Me.Dispose()
                Return False

            End If

        End If

        Select Case g_FadeEffect
            Case SDXLib.FadeEffect.FadeIn
                ' 페이드 인 (0 -> 255)
                Select Case g_FadeType
                    Case SDXLib.FadeType.FrameBased
                        ref = m_FadeStep * m_CurrentStep

                    Case SDXLib.FadeType.TrigonometricFunctionBased
                        ref = Math.Sin(SDXHelper.DegToRad(m_FadeAmount * m_CurrentStep))

                    Case Else
                        Return False

                End Select
                Return True

            Case SDXLib.FadeEffect.FadeOut
                ' 페이드 아웃 (255 -> 0)
                Select Case g_FadeType
                    Case SDXLib.FadeType.FrameBased
                        ref = 1.0F - (m_FadeStep * m_CurrentStep)

                    Case SDXLib.FadeType.TrigonometricFunctionBased
                        ref = Math.Cos(SDXHelper.DegToRad(m_FadeAmount * m_CurrentStep))
                        'ref = Math.Sin(SdxHelper.DegToRad(90 + (m_FadeAmount * m_CurrentStep)))

                    Case Else
                        Return False

                End Select
                Return True

            Case Else
                Return False

        End Select

    End Function

    ''' <summary>
    ''' 선을 그립니다.
    ''' </summary>
    ''' <param name="Target">선을 그릴 대상 Line 개체를 입력합니다.</param>
    Public Sub Draw(ByVal Target As Object) Implements IGraphicsLine.Draw

        If Me.Disposed Then Return

        Dim TargetAlpha As Single = 1.0F

        If g_FadeType Then

            If Not CalculateAlpha(TargetAlpha) Then Return
            m_CurrentStep += 1

        End If

        If Target.AntiAlias <> g_AntiAlias Then Target.AntiAlias = g_AntiAlias
        Target.Width = Me.Thickness
        Target.Begin()
        Target.Draw({SDXHelper.PointFToVector2(Me.Start + MyBase.Main.ViewLocation), SDXHelper.PointFToVector2(Me.End + MyBase.Main.ViewLocation)}, Drawing.Color.FromArgb(TargetAlpha * 255, g_Color))
        Target.End()

    End Sub

End Class