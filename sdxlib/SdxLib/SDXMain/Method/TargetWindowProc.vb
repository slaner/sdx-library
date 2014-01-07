Partial Class SDXMain

    Private Function TargetWndProc(ByVal Handle As IntPtr, ByVal Msg As Int32, ByVal WParam As IntPtr, ByVal LParam As IntPtr) As Int32

        Dim m As Windows.Forms.Message = Windows.Forms.Message.Create(Handle, Msg, WParam, LParam),
            ProcessAsDef As Boolean = False,
            Handled As Boolean = False

        RaiseEvent TargetFormWndProc(m, ProcessAsDef, Handled)
        If Not Handled Then
            If ProcessAsDef Then
                Return WinAPI.DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam)
            Else
                Return WinAPI.CallWindowProc(m_PrevProc, m.HWnd, m.Msg, m.WParam, m.LParam)
            End If
        Else
            Return 0
        End If

    End Function

End Class