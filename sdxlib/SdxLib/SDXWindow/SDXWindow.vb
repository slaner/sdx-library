Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Public Class SDXWindow
    Inherits Form

    Friend Event OnWndProc(ByRef m As Message, ByRef ProcessAsDefault As Boolean, ByRef Handled As Boolean)
    Protected Overrides Sub WndProc(ByRef m As Message)

        Dim ProcessAsDefault As Boolean = False,
            Handled As Boolean = False
        RaiseEvent OnWndProc(m, ProcessAsDefault, Handled)

        If Not Handled Then

            If ProcessAsDefault Then
                MyBase.WndProc(m)
            Else
                MyBase.DefWndProc(m)
            End If

        End If
        
    End Sub
    Friend Sub HandleMsg(ByRef m As Message)
        MyBase.WndProc(m)
    End Sub
    Friend Sub DefHandleMsg(ByRef m As Message)
        MyBase.DefWndProc(m)
    End Sub

End Class