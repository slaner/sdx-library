Partial Class SdxBlock2D

    Private Sub UpdateBlock()

        If Not g_UsePatrol Then Return
        If Me.Disposed Then Return

        ' STEP
        If m_CurrentStep = 0 Then
            g_PatrolStartLocation = Me.Location
        End If

        Dim pX As Single = g_Distance.X / g_PatrolStep,
            pY As Single = g_Distance.Y / g_PatrolStep

        If Not m_GoingBack Then
            Me.Location += New Drawing.Point(pX, pY)
        Else
            Me.Location -= New Drawing.Point(pX, pY)
        End If

        m_CurrentStep += 1
        If m_CurrentStep > g_PatrolStep Then
            If m_GoingBack Then
                m_GoingBack = False
            Else
                m_GoingBack = True
            End If
            m_CurrentStep = 0
        End If

    End Sub

End Class