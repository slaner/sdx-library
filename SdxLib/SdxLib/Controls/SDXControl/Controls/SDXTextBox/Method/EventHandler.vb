Namespace Controls
    Partial Class SDXTextBox

        Private Sub MybaseFontChanged() Handles MyBase.FontChanged

            Dim iCount As Int32 = 1
            Do

                If SDXHelper.GetTextWidth(m_Font, MyBase.DotWidth, StrDup(iCount, "i"c)) > Me.Width Then
                    m_iMaxDisplayableCharacters = iCount - 1
                    Return
                End If
                iCount += 1

            Loop

        End Sub

    End Class
End Namespace