Namespace Devices

    Partial Class SDXMouse

        ''' <summary>
        ''' 마우스 정보를 가져오는 개체를 초기화합니다.
        ''' </summary>
        ''' <param name="Main">주 개체를 입력합니다.</param>
        Public Sub New(ByVal Main As SDXMain)

            m_Main = Main
            m_PollingHandler = New PollingMouseStateHandler(AddressOf PollInternal)

        End Sub

    End Class

End Namespace