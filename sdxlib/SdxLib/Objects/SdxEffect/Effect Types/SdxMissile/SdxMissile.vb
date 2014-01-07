Imports System.Drawing
''' <summary>
''' 미사일 효과를 구현하는 효과 클래스입니다.
''' </summary>
Public Class SdxMissile
    Inherits SdxEffect

    ''' <summary>
    ''' 미사일의 목표 플레이어를 저장합니다.
    ''' </summary>
    Private m_Target As SdxPlayer

    ''' <summary>
    ''' 미사일의 마지막 목표 위치를 저장합니다.
    ''' </summary>
    Private m_LastTargetLocation As Vector2D

    ''' <summary>
    ''' 최대 속도에 도달하기까지 걸리는 프레임 수를 저장합니다.
    ''' </summary>
    Private g_ZeroMax As Int32 = 180

    ''' <summary>
    ''' 최대 속도를 저장합니다.
    ''' </summary>
    Private g_MaxSpeed As Int32 = 10

    ''' <summary>
    ''' 초기 속도를 저장합니다.
    ''' </summary>
    Private g_InitialSpeed As Int32 = 0

    ''' <summary>
    ''' 단계를 저장합니다.
    ''' </summary>
    Private m_CurrentStep As Int32

    ''' <summary>
    ''' 중앙 위치를 저장합니다.
    ''' </summary>
    Private m_Center As Point

    ''' <summary>
    ''' 프레임 당 증가하는 속도를 저장합니다.
    ''' </summary>
    Private m_SpeedStep As Single = 90 / 180

    ''' <summary>
    ''' 미사일의 피해량을 저장합니다.
    ''' </summary>
    Private g_Damage As Int32 = 10

End Class