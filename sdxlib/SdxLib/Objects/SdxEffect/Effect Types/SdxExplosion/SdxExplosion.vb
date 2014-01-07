' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxExplosion.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   SdxEffect
'   System.Drawing
'
' Date:
'   2013/12/21
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxExplosion class body.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing

''' <summary>
''' 폭발 효과를 구현하는 효과 클래스입니다.
''' </summary>
Public Class SdxExplosion
    Inherits SdxEffect

    Private Const MINIMUM_BLAST_RADIUS As Single = 100.0F

    ''' <summary>
    ''' 폭발 범위를 저장합니다.
    ''' </summary>
    Private g_Radius As Single

    ''' <summary>
    ''' 폭발 피해를 적용할 것인지에 대한 여부를 저장합니다.
    ''' </summary>
    Private g_UseBlast As Boolean = False

    ''' <summary>
    ''' 폭발 피해 범위 내에 있는 모든 플레이어에게 동일한 피해를 줄 것인지에 대한 여부를 저장합니다.
    ''' </summary>
    Private g_FixedBlastDamage As Boolean = True

    ''' <summary>
    ''' 폭발 피해량을 저장합니다.
    ''' </summary>
    Private g_Damage As Int32 = 0

End Class