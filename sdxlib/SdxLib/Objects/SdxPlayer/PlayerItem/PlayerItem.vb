' SlaneR's DirectX Library (SdxLib)
'
' File:
'   PlayerItem.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  0
'
' Date:
'   2013/12/06
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines PlayerItem class.

Public Class PlayerItem

    Private g_Damage As Int32
    Private g_Armor As Int32
    Private g_Health As Int32
    Private g_Stamina As Int32

    Public Sub New(ByVal d As Int32, ByVal a As Int32, ByVal h As Int32, ByVal s As Int32)
        g_Damage = d
        g_Armor = a
        g_Health = h
        g_Stamina = s
    End Sub

    Public ReadOnly Property Damage As Int32
        Get
            Return g_Damage
        End Get
    End Property
    Public ReadOnly Property Armor As Int32
        Get
            Return g_Armor
        End Get
    End Property
    Public ReadOnly Property Health As Int32
        Get
            Return g_Health
        End Get
    End Property
    Public ReadOnly Property Stamina As Int32
        Get
            Return g_Stamina
        End Get
    End Property

End Class