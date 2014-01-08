Namespace Controls
    Partial Class SDXControl

        ''' <summary>
        ''' 컨트롤 내부로 마우스 포인터가 들어오면 발생합니다.
        ''' </summary>
        Public Event MouseEnter As SDXControlEmptyEventHandler

        ''' <summary>
        ''' 마우스 포인터가 컨트롤 밖으로 나가면 발생합니다.
        ''' </summary>
        Public Event MouseLeave As SDXControlEmptyEventHandler

        ''' <summary>
        ''' 마우스 버튼이 눌리면 발생합니다.
        ''' </summary>
        Public Event MouseDown As SDXControlMouseEventHandler

        ''' <summary>
        ''' 마우스 버튼이 때어지면 발생합니다.
        ''' </summary>
        Public Event MouseUp As SDXControlMouseEventHandler

        ''' <summary>
        ''' 컨트롤 내부를 마우스로 클릭하면 발생합니다.
        ''' </summary>
        Public Event MouseClick As SDXControlMouseEventHandler

        ''' <summary>
        ''' 컨트롤 내부를 더블 클릭을 하면 발생합니다.
        ''' </summary>
        Public Event MouseDblClick As SDXControlMouseEventHandler

        ''' <summary>
        ''' 컨트롤 내부에서 마우스를 움직이면 발생합니다.
        ''' </summary>
        Public Event MouseMove As SDXControlMouseEventHandler

        ''' <summary>
        ''' 컨트롤에 포커스가 있을 때 키가 눌리면 발생합니다.
        ''' </summary>
        Public Event KeyPress As SDXControlKeyboardEventHandler

        ''' <summary>
        ''' 컨트롤에 포커스가 있을 때 키가 눌려지면 발생합니다.
        ''' </summary>
        Public Event KeyDown As SDXControlKeyboardEventHandler

        ''' <summary>
        ''' 컨트롤에 포커스가 있을 때 눌렸던 키가 때어지면 발생합니다.
        ''' </summary>
        Public Event KeyUp As SDXControlKeyboardEventHandler

        ''' <summary>
        ''' 컨트롤이 포커스를 받으면 발생합니다.
        ''' </summary>
        Public Event GotFocus As SDXControlEmptyEventHandler

        ''' <summary>
        ''' 컨트롤이 포커스를 잃으면 발생합니다.
        ''' </summary>
        Public Event LostFocus As SDXControlEmptyEventHandler

        ''' <summary>
        ''' 컨트롤 목록에 새로 컨트롤이 추가되면 발생합니다.
        ''' </summary>
        Public Event ControlAdded As SDXControlEmptyEventHandler

        ''' <summary>
        ''' 컨트롤 목록에서 컨트롤이 삭제되면 발생합니다.
        ''' </summary>
        Public Event ControlRemoved As SDXControlEmptyEventHandler

        ''' <summary>
        ''' 컨트롤을 클릭하면 발생합니다.
        ''' </summary>
        Public Event Click As SDXControlEmptyEventHandler

    End Class
End Namespace