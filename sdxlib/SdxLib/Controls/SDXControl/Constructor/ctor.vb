'
'
'
'
'
'
'
'
'
'
'
'
'
'
'
'
'
'
'
'
Namespace Controls

    Partial Class SDXControl

        Public Sub New(ByVal Main As SDXMain)
            MyBase.New(Main)
            Me.Font = Drawing.SystemFonts.DefaultFont
            AddHandler Main.Window.OnWndProc, AddressOf ProcessMessageInternal
        End Sub

    End Class

End Namespace