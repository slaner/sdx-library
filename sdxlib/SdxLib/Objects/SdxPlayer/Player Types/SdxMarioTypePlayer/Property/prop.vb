Partial Class SdxMarioTypePlayer

    ''' <summary>
    ''' 플레이어가 지상에 있는지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property InGround As Boolean
        Get
            Return g_InGround
        End Get
    End Property

    ''' <summary>
    ''' 플레이어가 점프를 하고 있는 중인지의 여부를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property IsJumping As Boolean
        Get
            Return g_Jumping
        End Get
    End Property

End Class