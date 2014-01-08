Imports SDXLib

Module EntryPoint

    WithEvents SDX As SDXMain
    WithEvents SDXTB As Controls.SDXTextBox
    Private SDXP As SdxRacingTypePlayer
    Private TextSurface As SdxFont

    Public Sub Main()

        ' Create SDX Object
        SDX = New SDXMain(1024, 768, "SDXApplication", False)
        SDX.BackColor = Color.White                         ' Black Background Color
        SDX.WorldGravity = 4                                ' World Gravity
        SDX.UseCustomStep = False                           ' Don't use user defined step


        SDXTB = New Controls.SDXTextBox(SDX)
        With SDXTB
            .Location = New Point(256, 700)
            .ForeColor = Color.Black
            .Size = New Size(512, 0)
            .Font = New Font("Orbit-B BT", 20.25, FontStyle.Bold, GraphicsUnit.World)
            .Size = New Size(512, SDXTB.FontHeight)
            .TextAlign = TextAlignment.VerticalCenter Or TextAlignment.Left
            .BackColor = Color.LightGray
        End With

        SDX.Components.Add(SDXTB)

        ' Add grids
        For i As Int32 = 0 To (SDX.Window.ClientSize.Width / 32) - 1
            SDX.Lines.Add(New SdxLine2D(SDX, New Vector2D(i * 32, 0), New Vector2D(i * 32, SDX.Window.ClientSize.Height), Color.Gray, 1))
        Next
        For i As Int32 = 0 To (SDX.Window.ClientSize.Height / 32) - 1
            SDX.Lines.Add(New SdxLine2D(SDX, New Vector2D(0, i * 32), New Vector2D(SDX.Window.ClientSize.Width, i * 32), Color.Gray, 1))
        Next

        ' Setup blocks
        Dim fid As Int32 = FreeFile(),
            cl As Int32 = 0

        FileOpen(fid, "C:\rect.map", OpenMode.Input)
        Do Until EOF(fid)
            Dim ls As String = LineInput(fid)
            For i As Int32 = 0 To ls.Length - 1
                If ls(i) = "1"c Then
                    SDX.Blocks.Add(New SdxBlock2D(SDX, My.Resources.WoodBlock))
                ElseIf ls(i) = "0"c Then
                    Dim b As New SdxBlock2D(SDX, My.Resources.Road)
                    b.Flags = 0
                    SDX.Blocks.Add(b)
                Else
                    Continue For
                End If
                SDX.Blocks.Last.Location = New Point(i * 32, cl * 32)
            Next
            cl += 1
        Loop
        FileClose(fid)

        SDXP = New SdxRacingTypePlayer(SDX, My.Resources.Player)
        With SDXP
            .IgnoreBlocks = True
            .Speed = 5
            .Location = New Vector2D(128, 128)
            .ApplyShadow = False
            .Active = True
            .ChaseCamera = True
        End With

        SDX.Players.Add(SDXP)

        TextSurface = New SdxFont(SDX, New FontDescription(CharacterSet.Default, "나눔고딕코딩", 20, False, 0, OutputPrecision.Default, PitchAndFamily.DefaultPitch, FontQuality.ClearTypeNatural, FontWeight.Black, 8))
        TextSurface.UseTextSprite = True
        SDX.Run()

    End Sub

    Private Sub SDX_OnTextSpriteBegin() Handles SDX.OnTextSpriteBegin

        TextSurface.DrawText(2, 0, Color.Black, "Fps       : " & SDX.FrameRate & vbCrLf & _
                                                "Blocks    : " & SDX.Blocks.Count & vbCrLf & _
                                                "View Information" & vbCrLf & _
                                                "  Location: " & SDX.ViewLocation.ToString & vbCrLf & _
                                                "Player Information" & vbCrLf & _
                                                "  Location: " & SDXP.Location.ToString & vbCrLf & _
                                                "  Angle   : " & SDXP.Angle & vbCrLf & _
                                                "  Speed   : " & SDXP.CurrentSpeed & " pxl/s")


    End Sub

    Private Sub SDXTB_KeyDown(ByVal Key As System.Windows.Forms.Keys) Handles SDXTB.KeyDown

        Debug.Print("SDXTextBox::KeyDown(Key: {0})", Key.ToString)

    End Sub

    Private Sub SDXTB_MouseClick(ByVal Button As SDXLib.MouseButton, ByVal Location As System.Drawing.Point) Handles SDXTB.MouseClick

        Debug.Print("SDXTextBox::MouseClick(Button: {0}, Location: {1})", Button.ToString, Location.ToString)

    End Sub

    Private Sub SDXTB_MouseDown(ByVal Button As SDXLib.MouseButton, ByVal Location As System.Drawing.Point) Handles SDXTB.MouseDown

        Debug.Print("SDXTextBox::MouseDown(Button: {0}, Location: {1})", Button.ToString, Location.ToString)

    End Sub
    Private Sub SDXTB_MouseEnter() Handles SDXTB.MouseEnter

        Debug.Print("SDXTextBox::MouseEnter")

    End Sub
    Private Sub SDXTB_MouseLeave() Handles SDXTB.MouseLeave

        Debug.Print("SDXTextBox::MouseLeave")

    End Sub
    Private Sub SDXTB_MouseUp(ByVal Button As SDXLib.MouseButton, ByVal Location As System.Drawing.Point) Handles SDXTB.MouseUp

        Debug.Print("SDXTextBox::MouseUp(Button: {0}, Location: {1})", Button.ToString, Location.ToString)

    End Sub

End Module