Partial Class SdxRacingTypePlayer

    ''' <summary>
    ''' 방향을 틀 때 각도가 변경되는 단위를 가져오거나 설정합니다.
    ''' </summary>
    Public Property SteeringAngle As Int32
        Get
            Return g_SteeringAngle
        End Get
        Set(ByVal value As Int32)
            If value < 0 Then value = 360 - (value Mod 360)
            If value >= 360 Then value = value Mod 360
            g_SteeringAngle = value
        End Set
    End Property

    ''' <summary>
    ''' 최대 출력(전진)을 가져오거나 설정합니다.
    ''' </summary>
    Public Property MaximumTorque As Int32
        Get
            Return g_MaximumTorque
        End Get
        Set(ByVal value As Int32)
            g_MaximumTorque = value
        End Set
    End Property

    ''' <summary>
    ''' 최소 출력(후진)을 가져오거나 설정합니다.
    ''' </summary>
    Public Property MinimumTorque As Int32
        Get
            Return g_MinimumTorque
        End Get
        Set(ByVal value As Int32)
            g_MinimumTorque = value
        End Set
    End Property

    ''' <summary>
    ''' 출력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property TorquePower As Int32
        Get
            Return g_TorquePower
        End Get
        Set(ByVal value As Int32)
            g_TorquePower = value
        End Set
    End Property

    ''' <summary>
    ''' 브레이크 출력을 가져오거나 설정합니다.
    ''' </summary>
    Public Property BrakePower As Int32
        Get
            Return g_BrakePower
        End Get
        Set(ByVal value As Int32)
            g_BrakePower = value
        End Set
    End Property

    Public ReadOnly Property CurrentSpeed As Single
        Get
            Return g_CurrentPower
        End Get
    End Property

End Class