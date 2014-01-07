Partial Class SdxMissile

    ''' <summary>
    ''' 미사일의 최대 속도를 가져오거나 설정합니다.
    ''' </summary>
    Public Property MaxSpeed As Int32
        Get
            Return g_MaxSpeed
        End Get
        Set(ByVal value As Int32)
            g_MaxSpeed = value
        End Set
    End Property

    ''' <summary>
    ''' 초기 속도에서 최대 속도까지 걸리는 프레임 수를 가져오거나 설정합니다.
    ''' </summary>
    Public Property ZeroMax As Int32
        Get
            Return g_ZeroMax
        End Get
        Set(ByVal value As Int32)
            g_ZeroMax = value
            m_SpeedStep = 90 / g_ZeroMax
        End Set
    End Property

    ''' <summary>
    ''' 미사일의 피해량을 가져오거나 설정합니다.
    ''' </summary>
    Public Property Damage As Int32
        Get
            Return g_Damage
        End Get
        Set(ByVal value As Int32)
            g_Damage = value
        End Set
    End Property

End Class