''' <summary>
''' 유닛이 장착할 수 있는 장비를 구현합니다.
''' </summary>
Public Class UnitEquipments

    ''' <summary>
    ''' 장비의 기본 속성을 구현하는 클래스입니다.
    ''' </summary>
    Public MustInherit Class EquipmentBase

        ''' <summary>
        ''' 추가 체력을 저장합니다.
        ''' </summary>
        Private g_HealthBonus As Int32 = 0

        ''' <summary>
        ''' 추가 방어력을 저장합니다.
        ''' </summary>
        Private g_ArmorBonus As Int32 = 0

        ''' <summary>
        ''' 추가 공격력을 저장합니다.
        ''' </summary>
        Private g_DamageBonus As Int32 = 0

        ''' <summary>
        ''' 추가 이동속도를 저장합니다.
        ''' </summary>
        Private g_SpeedBonus As Single = 0.0F

        ''' <summary>
        ''' 추가 에너지를 저장합니다.
        ''' </summary>
        Private g_EnergyBonus As Int32 = 0

        ''' <summary>
        ''' 추가 에너지 재생량을 저장합니다.
        ''' </summary>
        Private g_EnergyRegenBonus As Int32 = 0

        ''' <summary>
        ''' 장비의 무게를 저장합니다.
        ''' </summary>
        Private g_Weight As Int32 = 0

        ''' <summary>
        ''' 장비의 이름을 저장합니다.
        ''' </summary>
        Private g_Name As String

        Friend Sub New(ByVal HB As Int32, ByVal AB As Int32, ByVal DB As Int32, ByVal SB As Single, ByVal EB As Int32, ByVal ERB As Int32, ByVal W As Int32, ByVal N As String)

            g_HealthBonus = HB
            g_ArmorBonus = AB
            g_DamageBonus = DB
            g_SpeedBonus = SB
            g_EnergyBonus = EB
            g_EnergyRegenBonus = ERB
            g_Weight = W
            g_Name = N

        End Sub

        ''' <summary>
        ''' 추가 체력을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property HealthBonus As Int32
            Get
                Return g_HealthBonus
            End Get
        End Property

        ''' <summary>
        ''' 추가 방어력을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property ArmorBonus As Int32
            Get
                Return g_ArmorBonus
            End Get
        End Property

        ''' <summary>
        ''' 추가 공격력을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property DamageBonus As Int32
            Get
                Return g_DamageBonus
            End Get
        End Property

        ''' <summary>
        ''' 추가 이동속도를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property SpeedBonus As Single
            Get
                Return g_SpeedBonus
            End Get
        End Property

        ''' <summary>
        ''' 추가 에너지를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property EnergyBonus As Int32
            Get
                Return g_EnergyBonus
            End Get
        End Property

        ''' <summary>
        ''' 추가 에너지 재생량을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property EnergyRegenBonus As Int32
            Get
                Return g_EnergyRegenBonus
            End Get
        End Property

        ''' <summary>
        ''' 장비의 이름을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Name As String
            Get
                Return g_Name
            End Get
        End Property

        ''' <summary>
        ''' 장비의 무게를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Weight As Int32
            Get
                Return g_Weight
            End Get
        End Property

    End Class

    ''' <summary>
    ''' 무기 장비를 구현합니다.
    ''' </summary>
    Public Class WeaponBase
        Inherits EquipmentBase

        ''' <summary>
        ''' 공격 시간을 저장합니다.
        ''' </summary>
        Private g_Delay As Int32

        ''' <summary>
        ''' 공격 사거리를 저장합니다.
        ''' </summary>
        Private g_Range As Int32

        ''' <summary>
        ''' 공격 에너지를 저장합니다.
        ''' </summary>
        Private g_EnergyCost As Int32

        Friend Sub New(ByVal HB As Int32, ByVal AB As Int32, ByVal DB As Int32, ByVal SB As Single, ByVal EB As Int32, ByVal ERB As Int32, ByVal W As Int32, ByVal N As String, ByVal D As Int32, ByVal R As Int32, ByVal EC As Int32)

            MyBase.New(HB, AB, DB, SB, EB, ERB, W, N)
            g_Delay = D
            g_Range = R
            g_EnergyCost = EC

        End Sub

        ''' <summary>
        ''' 공격 시간을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Delay As Int32
            Get
                Return g_Delay
            End Get
        End Property

        ''' <summary>
        ''' 공격 사거리를 가져옵니다.
        ''' </summary>
        Public ReadOnly Property Range As Int32
            Get
                Return g_Range
            End Get
        End Property

        ''' <summary>
        ''' 공격 에너지 소모량을 가져옵니다.
        ''' </summary>
        Public ReadOnly Property EnergyCost As Int32
            Get
                Return g_EnergyCost
            End Get
        End Property

    End Class

    ''' <summary>
    ''' 엔진 장비를 구현합니다.
    ''' </summary>
    Public Class EngineBase
        Inherits EquipmentBase

        Friend Sub New(ByVal HB As Int32, ByVal AB As Int32, ByVal DB As Int32, ByVal SB As Single, ByVal EB As Int32, ByVal ERB As Int32, ByVal W As Int32, ByVal N As String)

            MyBase.New(HB, AB, DB, SB, EB, ERB, W, N)

        End Sub

    End Class

    ''' <summary>
    ''' 에너지 저장 장비를 구현합니다.
    ''' </summary>
    Public Class StorageBase
        Inherits EquipmentBase

        Friend Sub New(ByVal HB As Int32, ByVal AB As Int32, ByVal DB As Int32, ByVal SB As Single, ByVal EB As Int32, ByVal ERB As Int32, ByVal W As Int32, ByVal N As String, ByVal D As Int32, ByVal R As Int32)

            MyBase.New(HB, AB, DB, SB, EB, ERB, W, N)

        End Sub

    End Class

    ''' <summary>
    ''' 방어 장비를 구현합니다.
    ''' </summary>
    Public Class ArmorBase
        Inherits EquipmentBase

        Friend Sub New(ByVal HB As Int32, ByVal AB As Int32, ByVal DB As Int32, ByVal SB As Single, ByVal EB As Int32, ByVal ERB As Int32, ByVal W As Int32, ByVal N As String, ByVal D As Int32, ByVal R As Int32)

            MyBase.New(HB, AB, DB, SB, EB, ERB, W, N)

        End Sub

    End Class

    Private g_Weapon As WeaponBase
    Private g_Armor As ArmorBase
    Private g_Storage As StorageBase
    Private g_Engine As EngineBase

    Friend Sub New()

    End Sub

    ''' <summary>
    ''' 모든 장비의 추가 체력을 계산합니다.
    ''' </summary>
    Public Function GetHealth() As Int32

        Dim H As Int32 = 0

        ' 추가 체력
        ' 엔진, 방어구
        If g_Armor IsNot Nothing Then H = g_Armor.HealthBonus
        If g_Engine IsNot Nothing Then H += g_Engine.HealthBonus

        Return H

    End Function

    ''' <summary>
    ''' 모든 장비의 추가 방어력을 계산합니다.
    ''' </summary>
    Public Function GetArmor() As Int32

        Dim A As Int32 = 0

        ' 추가 방어력
        ' 방어구
        If g_Armor IsNot Nothing Then A = g_Armor.ArmorBonus

        Return A

    End Function

    Public Sub EquipItem(ByVal Part As Int32, ByVal Equipment As EquipmentBase)

        Select Case Part
            Case 0
                g_Weapon = Equipment

            Case 1
                g_Armor = Equipment

            Case 2
                g_Storage = Equipment

            Case 3
                g_Engine = Equipment
        End Select

    End Sub



    ''' <summary>
    ''' 장착중인 무기를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Weapon As WeaponBase
        Get
            Return g_Weapon
        End Get
    End Property

    ''' <summary>
    ''' 장착중인 방어구를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Armor As ArmorBase
        Get
            Return g_Armor
        End Get
    End Property

    ''' <summary>
    ''' 장착중인 에너지 저장 장비를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Storage As StorageBase
        Get
            Return g_Storage
        End Get
    End Property

    ''' <summary>
    ''' 장착중인 엔진을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Engine As EngineBase
        Get
            Return g_Engine
        End Get
    End Property

End Class