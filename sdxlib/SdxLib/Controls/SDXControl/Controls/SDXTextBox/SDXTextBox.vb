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
        ''' 텍스트박스의 내용을 저장합니다.
        ''' </summary>
        Private m_Buffer As New StringBuilder()

        ''' <summary>
        ''' 캐럿이 표시되는 시간을 저장합니다.
        ''' </summary>
        Private g_CaretTick As Int32 = 30

        ''' <summary>
        ''' 캐럿이 사라지는 값을 저장합니다.
        ''' </summary>
        Private m_CaretFadeStep As Single = 90 / 30

        ''' <summary>
        ''' 삽입 모드를 사용하고 있는지 여부를 저장합니다.
        ''' </summary>
        Private g_InsertMode As Boolean = False

        ''' <summary>
        ''' 선택 영역을 저장합니다.
        ''' </summary>
        Private g_Selection As SelectionInfo

        ''' <summary>
        ''' 스크롤 위치를 저장합니다.
        ''' </summary>
        Private m_ScrollLocation As Point

        Private m_bGoingLeft As Boolean = False


        ''' <summary>
        ''' 컨트롤 영역 내부에 최대로 표시할 수 있는 문자의 갯수를 저장합니다.
        ''' </summary>
        Private m_iMaxDisplayableCharacters As Int32 = 0

        Private m_iCaretPosition As Int32 = 0
        Private m_iLastCaretPosition As Int32 = 0
        Private m_bComposingChar As Boolean = False

        ''' <summary>
        ''' 쉬프트키의 상태를 저장합니다.
        ''' </summary>
        Private m_bShifted As Boolean = False

        Private m_HideCaret As Boolean = False

        Private m_CaretTick As Int32 = 0
        Private m_ShowCaret As Boolean = False

        Private m_bComposition As Boolean = False

    End Class
End Namespace