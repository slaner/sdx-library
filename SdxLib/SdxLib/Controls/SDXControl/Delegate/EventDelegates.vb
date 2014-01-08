Namespace Controls
    Partial Class SDXControl

        ''' <summary>
        ''' 컨트롤의 마우스 입력에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        ''' <param name="Button">눌린 마우스 버튼을 가져옵니다.</param>
        ''' <param name="Location">이벤트가 발생한 마우스의 위치를 가져옵니다.</param>
        Public Delegate Sub SDXControlMouseEventHandler(ByVal Button As MouseButton, ByVal Location As Drawing.Point)

        ''' <summary>
        ''' 컨트롤의 키보드 입력에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        ''' <param name="Key">눌린 키보드 버튼을 가져옵니다.</param>
        Public Delegate Sub SDXControlKeyboardEventHandler(ByVal Key As Windows.Forms.Keys)

        ''' <summary>
        ''' 처리할 데이터가 없는 컨트롤에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        Public Delegate Sub SDXControlEmptyEventHandler()

        ''' <summary>
        ''' 컨트롤의 속성 변경에 대한 이벤트를 처리할 메서드를 나타냅니다.
        ''' </summary>
        ''' <param name="OldValue">속성이 변경되기 이전의 값을 입력합니다.</param>
        ''' <param name="NewValue">변경된 속성의 값을 입력합니다.</param>
        Public Delegate Sub SDXControlPropertyChangedHandler(ByVal OldValue As Object, ByVal NewValue As Object)

    End Class
End Namespace