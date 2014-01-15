﻿Imports System.Drawing
Imports System.Text
Imports SDXLib.WinAPI
Imports System.Runtime.InteropServices
Imports D3 = Microsoft.DirectX.Direct3D
Namespace Controls

    ''' <summary>
    ''' 사용자가 편집할 수 있는 텍스트박스를 구현합니다.
    ''' </summary>
    Public Class SDXTextBox
        Inherits Controls.SDXControl

        ''' <summary>
        ''' 선택 영역의 정보를 저장합니다.
        ''' </summary>
        Public Structure SelectionInfo

            Public Start As Int32
            Public Length As Int32

        End Structure

        ''' <summary>
        ''' 캐럿이 표시되는 시간을 저장합니다.
        ''' </summary>
        Private g_CaretTick As Int32 = 30

        ''' <summary>
        ''' 캐럿이 사라질 때 페이드 아웃 효과를 적용할 것인지의 여부를 저장합니다.
        ''' </summary>
        Private g_UseCaretFade As Boolean = True

        ''' <summary>
        ''' 삽입 모드를 사용하고 있는지 여부를 저장합니다.
        ''' </summary>
        Private g_InsertMode As Boolean = False

        ''' <summary>
        ''' 선택 영역을 저장합니다.
        ''' </summary>
        Private g_Selection As SelectionInfo

        ''' <summary>
        ''' 읽기 전용 여부를 저장합니다.
        ''' </summary>
        Private g_ReadOnly As Boolean = False

        ''' <summary>
        ''' 암호 문자를 저장합니다.
        ''' </summary>
        Private g_PasswordChar As Char = Nothing







        Private m_ScrollPoint As Point
        Private m_CaretFadeStep As Single = 90 / 30
        Private m_Buffer As New StringBuilder()
        Private m_BufferText As String = Nothing
        Private m_PrevBuffer As StringBuilder = Nothing
        Private m_iMaxDisplayableCharacters As Int32 = 0
        Private m_iCaretPosition As Int32 = 0
        Private m_bShifted As Boolean = False
        Private m_HideCaret As Boolean = False
        Private m_CaretTick As Int32 = 0
        Private m_ShowCaret As Boolean = False
        Private m_iTextRange As Int32 = 0
        Private m_ScrollText As String = Nothing
        Private m_bComposing As Boolean = False
        Private m_bSelectLeft As Boolean = False

    End Class
End Namespace