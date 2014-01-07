Imports SdxLib

Imports System.Runtime.InteropServices

Public Class Form1

    Private WithEvents Sdx As SDXMain
    Private TextSurface As SdxFont
    Private p As SdxRacingTypePlayer
    Private m As SdxMarioTypePlayer
    Private f As Boolean = False
    WithEvents fx As SdxLib.Components.SdxComponents
    WithEvents fx2 As SdxLib.Components.SdxComponents
    Private tb As SDXTextBox
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.D1
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 0 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D2
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 1 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D3
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 2 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D4
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 3 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D5
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 4 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D6
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 5 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D7
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 6 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D8
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 7 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D9
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 8 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next
            Case Windows.Forms.Keys.D0
                For i As Int32 = 0 To Sdx.Players.Count - 1
                    If i = 9 Then
                        Sdx.Players(i).Active = True
                    Else
                        Sdx.Players(i).Active = False
                    End If
                Next

            Case Windows.Forms.Keys.Escape
                Sdx.Dispose()
                Form1_Load(Nothing, Nothing)

        End Select

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ClientSize = New Size(1024, 768)
        Me.Show()
        Randomize()

        ' Sdx Initialize
        'Sdx = New SDXMain(Me, False)                ' Windowed
        Sdx.BackColor = Color.White                 ' Black Background Color
        Sdx.WorldGravity = 4                        ' World Gravity
        Sdx.UseCustomStep = False                   ' Don't use user defined step

        fx = New SdxLib.Components.SdxComponents(Sdx)
        fx2 = New SdxLib.Components.SdxComponents(Sdx)
        tb = New SdxLib.SDXTextBox(Sdx)
        fx.BackColor = Color.Lime
        fx2.BackColor = Color.FromArgb(128, Color.Black)
        fx.Size = New Size(256, 32)
        fx2.Location = New Point(0, 736)
        fx2.Size = New Size(0, 32)
        fx.Location = New Point(0, 736)
        fx.Font = SystemFonts.DefaultFont
        fx2.Font = SystemFonts.DefaultFont
        fx.ForeColor = Color.Black
        tb.BackColor = Color.Gray
        tb.TextAlign = TextAlignment.Left
        tb.Size = New Size(512, 30)
        tb.Location = New Point(200, 200)
        tb.ForeColor = Color.Black
        tb.Font = SystemFonts.DefaultFont
        Sdx.Components.Add(fx2)
        Sdx.Components.Add(fx)
        Sdx.Components.Add(tb)

        ' Add grids
        For i As Int32 = 0 To (Me.ClientSize.Width / 32) - 1
            Sdx.Lines.Add(New SdxLine2D(Sdx, New Vector2D(i * 32, 0), New Vector2D(i * 32, Me.ClientSize.Height), Color.Gray, 1))
        Next
        For i As Int32 = 0 To (Me.ClientSize.Height / 32) - 1
            Sdx.Lines.Add(New SdxLine2D(Sdx, New Vector2D(0, i * 32), New Vector2D(Me.ClientSize.Width, i * 32), Color.Gray, 1))
        Next

        ' Setup blocks
        Dim fid As Int32 = FreeFile(),
            cl As Int32 = 0

        FileOpen(fid, "C:\runway60.map", OpenMode.Input)
        Do Until EOF(fid)
            Dim ls As String = LineInput(fid)
            For i As Int32 = 0 To ls.Length - 1
                If ls(i) = "2"c Then
                    Dim b As New SdxBlock2D(Sdx, Sdx.SharedResource.IronBlock)
                    b.Flags = SdxBlock2D.BlockStates.Dead
                    Sdx.Blocks.Add(b)
                ElseIf ls(i) = "1"c Then
                    Sdx.Blocks.Add(New SdxBlock2D(Sdx, Sdx.SharedResource.WoodBlock))
                ElseIf ls(i) = "0"c Then
                    Dim b As New SdxBlock2D(Sdx, Sdx.SharedResource.RoadBlock)
                    b.Flags = 0
                    Sdx.Blocks.Add(b)
                Else
                    Continue For
                End If
                Sdx.Blocks.Last.Location = New Point(i * 32, cl * 32)
            Next
            cl += 1
        Loop
        FileClose(fid)

        If Not f Then

            p = New SdxRacingTypePlayer(Sdx, My.Resources.Player)
            p.IgnoreBlocks = False
            p.Speed = 4
            p.Location = New Vector2D(256, 128)
            p.ApplyShadow = False
            p.Active = True
            p.ChaseCamera = True
            Sdx.Players.Add(p)
            f = True

        Else

            m = New SdxMarioTypePlayer(Sdx, My.Resources.Player)
            m.IgnoreBlocks = False
            m.Speed = 4
            m.Location = New Vector2D(256, 128)
            m.CollideBox = New RectangleF(8, 0, 16, 32)
            m.ApplyShadow = False
            m.Active = True
            m.ChaseCamera = True
            m.DebugRectangle = True
            Sdx.Players.Add(m)

        End If


        ' Font Initialize
        TextSurface = New SdxFont(Sdx, New SDXMain.FontDescription(CharacterSet.Default, "나눔고딕코딩", 20, False, 0, OutputPrecision.Default, PitchAndFamily.DefaultPitch, FontQuality.ClearTypeNatural, FontWeight.Black, 8))
        TextSurface.UseTextSprite = True
        Sdx.Run()

    End Sub

    Private Sub Sdx_OnDrawFrame(ByVal FrameRate As Integer) Handles Sdx.OnDrawFrame

        Dim fstep As Single = 255 / 12500
        Select Case p.CurrentSpeed
            Case 0 To 12500
                fx.BackColor = Color.FromArgb(fstep * p.CurrentSpeed, 255, 0)
            Case 12500 To 25000
                fx.BackColor = Color.FromArgb(255, 255 - (fstep * (p.CurrentSpeed - 12500)), 0)

        End Select
        fx.Text = "Current Speed: " & FormatNumber(p.CurrentSpeed, 1) & " pxl/s"
        fx2.Width = (256 / 25000) * p.CurrentSpeed

    End Sub
    Private Sub Sdx_OnTextSpriteBegin() Handles Sdx.OnTextSpriteBegin

        TextSurface.DrawText(2, 0, Color.Black, "Fps       : " & Sdx.FrameRate & vbCrLf & _
                                                "Blocks    : " & Sdx.Blocks.Count & vbCrLf & _
                                                "View Information" & vbCrLf & _
                                                "  Location: " & Sdx.ViewLocation.ToString & vbCrLf & _
                                                "Player Information" & vbCrLf & _
                                                "  Location: " & If(Not f, m.Location.ToString, p.Location.ToString) & vbCrLf & _
                                                "  Angle   : " & If(Not f, m.Angle, p.Angle) & vbCrLf & _
                                                "  Speed   : " & If(Not f, "0", p.CurrentSpeed) & " pxl/s")

    End Sub

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        Debug.Print(e.Location.ToString)
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim ne As New SdxExplosion(Sdx, Sdx.SharedResource.DefaultExplosion64, 64, 64)
            ne.RenderingSize = New Size(128, 128)
            ne.Damage = 350
            ne.Radius = 350
            ne.EnableBlast = True
            ne.FixedBlastDamage = False
            ne.Location = e.Location - New Size(64, 64)
            Sdx.Effects.Add(ne)

        End If

    End Sub

    Private Sub fx2_OnMouseMove(ByVal Button As SdxLib.MouseButton, ByVal Location As System.Drawing.Point) Handles fx.OnMouseMove
        Debug.Print("Button: {0} / Location: {1}", Button, Location)
    End Sub
End Class