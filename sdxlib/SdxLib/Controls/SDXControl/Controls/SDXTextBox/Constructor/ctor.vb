Namespace Controls
    Partial Class SDXTextBox

        ''' <summary>
        ''' 텍스트박스 개체를 초기화합니다.
        ''' </summary>
        ''' <param name="Main">주 개체를 입력합니다.</param>
        Public Sub New(ByVal Main As SDXMain)
            MyBase.New(Main)
            MyBase.DoNotProcessKeyboardMessages = True
        End Sub

    End Class
End Namespace