' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxExplosion/prop.vb
'
' Date:
'   2013/12/25
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxExplosion class property.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
Partial Class SdxExplosion

    ''' <summary>
    ''' 폭발 피해 범위를 가져오거나 설정합니다.
    ''' </summary>
    Public Property Radius As Single
        Get
            Return g_Radius
        End Get
        Set(ByVal value As Single)
            g_Radius = value
        End Set
    End Property

    ''' <summary>
    ''' 폭발 피해를 적용할 것인지에 대한 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property EnableBlast As Boolean
        Get
            Return g_UseBlast
        End Get
        Set(ByVal value As Boolean)
            g_UseBlast = value
        End Set
    End Property

    ''' <summary>
    ''' 폭발 범위 내에 있는 모든 플레이어에게 동일한 피해를 줄 것인지에 대한 여부를 가져오거나 설정합니다.
    ''' </summary>
    Public Property FixedBlastDamage As Boolean
        Get
            Return g_FixedBlastDamage
        End Get
        Set(ByVal value As Boolean)
            g_FixedBlastDamage = value
        End Set
    End Property

    ''' <summary>
    ''' 폭발 피해량을 가져오거나 설정합니다.
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