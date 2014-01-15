Imports SDXLib

Module EntryPoint

    WithEvents SDX As SDXMain
    WithEvents txtChat As Controls.SDXTextBox
    WithEvents btnExit As Controls.SDXControl
    Private TextSurface As SdxFont
    Private l As SdxLine2D
    Private a As Single = 0.0F

    Public Sub Main()

        ' Create SDX Object
        SDX = New SDXMain(1024, 768, "SDXApplication", False)
        SDX.BackColor = Color.White                         ' Black Background Color
        SDX.WorldGravity = 4                                ' World Gravity
        SDX.UseCustomStep = False                           ' Don't use user defined step

        btnExit = New Controls.SDXControl(SDX)
        With btnExit
            .Text = "종료하려면 이 버튼을 클릭하세요"
            .FontDescription = New FontDescription(1, "돋움", 28, True, 0, 0, 0, FontQuality.ClearType, FontWeight.Medium, 15)
            .Size = New Size(512, btnExit.FontHeight * 2)
            .Location = New Point(256, 0)
            .ForeColor = Color.Black
            .BackColor = Color.LimeGreen
            .TextAlign = TextAlignment.VerticalCenter Or TextAlignment.Center
        End With

        txtChat = New Controls.SDXTextBox(SDX)
        With txtChat
            .Location = New Point(0, 256)
            .ForeColor = Color.Black
            .FontDescription = New FontDescription(1, "돋움", 42, False, 0, 0, 0, FontQuality.ClearType, FontWeight.Medium, 21)
            .Size = New Size(512, txtChat.FontHeight)
            .TextAlign = TextAlignment.VerticalCenter Or TextAlignment.Left
            .BackColor = Color.LightGray
            .ReadOnly = False
        End With

        SDX.Components.Add(btnExit)
        SDX.Components.Add(txtChat)

        ' Add grids
        For i As Int32 = 0 To (SDX.Window.ClientSize.Width / 32) - 1
            SDX.Lines.Add(New SdxLine2D(SDX, New Vector2D(i * 32, 0), New Vector2D(i * 32, SDX.Window.ClientSize.Height), Color.Gray, 1))
        Next
        For i As Int32 = 0 To (SDX.Window.ClientSize.Height / 32) - 1
            SDX.Lines.Add(New SdxLine2D(SDX, New Vector2D(0, i * 32), New Vector2D(SDX.Window.ClientSize.Width, i * 32), Color.Gray, 1))
        Next

        l = New SdxLine2D(SDX, New Vector2D(128, 128), New Vector2D(0, 0))
        l.Thickness = 2
        l.Color = Color.Black
        SDX.Lines.Add(l)

        TextSurface = New SdxFont(SDX, New FontDescription(CharacterSet.Default, "나눔고딕코딩", 20, False, 0, OutputPrecision.Default, PitchAndFamily.DefaultPitch, FontQuality.ClearTypeNatural, FontWeight.Black, 8))
        TextSurface.UseTextSprite = True
        SDX.Run()

    End Sub

    Private Sub SDX_OnDrawFrame(ByVal FrameRate As Integer) Handles SDX.OnDrawFrame

        l.End = SDXHelper.GetDistanceVector(l.Start, a, 128)
        a += 1
        If a >= 360 Then a = 0

    End Sub

    Private Sub SDX_OnTextSpriteBegin() Handles SDX.OnTextSpriteBegin

        TextSurface.DrawText(2, 0, Color.Black, "Fps       : " & SDX.FrameRate & vbCrLf & _
                                                "Blocks    : " & SDX.Blocks.Count & vbCrLf & _
                                                "TextLength: " & Len(txtChat.Text) & vbCrLf & _
                                                "Text      : " & txtChat.Text)

    End Sub

    Private Sub btnExit_Click() Handles btnExit.Click
        SDX.Dispose()
    End Sub
    Private Sub btnExit_MouseEnter() Handles btnExit.MouseEnter
        btnExit.BackColor = Color.Lime
    End Sub
    Private Sub btnExit_MouseLeave() Handles btnExit.MouseLeave
        btnExit.BackColor = Color.LimeGreen
    End Sub

End Module