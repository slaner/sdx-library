Imports System.Drawing
Public Class SdxRacingTypePlayer
    Inherits SdxPlayer

    Private m_Turbo As Boolean = False
    Private g_CurrentPower As Single = 0.0F
    Private g_SteeringAngle As Int32 = 5
    Private g_MaximumTorque As Int32 = 250
    Private g_MinimumTorque As Int32 = -150
    Private g_TorquePower As Int32 = 25
    Private g_BrakePower As Int32 = 2
    Private g_Energy As Int32 = 15000
    Private g_EnergyCost As Int32 = 100

    Public Sub New(ByVal Main As SDXMain, ByVal Img As Image)
        MyBase.New(Main, Img)
        Me.ApplyShadow = True
    End Sub

End Class