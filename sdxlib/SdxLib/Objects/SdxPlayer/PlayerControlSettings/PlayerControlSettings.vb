' SlaneR's DirectX Library (SdxLib)
'
' File:
'   PlayerControlSetings.vb
'
' Dependencies:
'   System.Xml
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
'   Defines SdxObject class.

Imports System.Xml

''' <summary>
''' 플레이어의 조작 설정을 저장합니다.
''' </summary>
Public Class PlayerControlSettings

    Private Shared m_ActionLists() As String = {
        "move_left", "move_right", "move_up", "move_down",
        "attack1", "attack2", "attack3", "attack4",
        "skill1", "skill2", "skill3", "skill4",
        "jump", "sprint", "fly", "menu", "exit", "pause"}

    Private g_SettingFile As String
    Private g_Author As String
    Private g_Name As String
    Private g_ControlSets As Dictionary(Of String, Int32)

#Region "Constructor"

    ''' <summary>
    ''' 지정한 Xml 문서 혹은 Xml 문자열로부터 PlayerControlSettings 클래스의 새 개체를 만듭니다.
    ''' </summary>
    ''' <param name="SettingFile"></param>
    ''' <exception cref="System.ArgumentException">Xml 문서의 확장자가 플레이어 조작 설정(*.pcs) 이 아닌 경우</exception>
    ''' <exception cref="System.IO.FileNotFoundException">Xml 문서가 없는 경우</exception>
    Public Sub New(ByVal SettingFile As String, Optional ByVal IsXmlText As Boolean = False)

        g_ControlSets = New Dictionary(Of String, Int32)
        Dim XmlDoc As New XmlDocument()

        If IsXmlText Then
            ' XML 문자열

            XmlDoc.LoadXml(SettingFile)
            LoadSetting(XmlDoc.ChildNodes)

        Else
            ' 파일

            ' 확장자 검사:
            If IO.Path.GetExtension(SettingFile).ToLower <> "pcs" Then
                Throw New ArgumentException("확장자가 잘못되었습니다.")
            End If

            ' 파일 존재 유무 검사:
            If Not IO.File.Exists(SettingFile) Then
                Throw New IO.FileNotFoundException("Xml 문서가 존재하지 않습니다.")
            End If

            ' Xml 개체 선언 및 불러오기
            XmlDoc.Load(SettingFile)
            LoadSetting(XmlDoc.ChildNodes)

        End If
    End Sub

#End Region

#Region "XML Load"

    Private Sub LoadSetting(ByVal xnl As XmlNodeList)

        For Each xn As XmlNode In xnl

            If xn.Name = "controlsettings" Then

                g_Name = GetXmlAttribute(xn, "name")
                g_Author = GetXmlAttribute(xn, "author")

                LoadControls(xn.ChildNodes)

            End If

        Next

    End Sub
    Private Sub LoadControls(ByVal xnl As XmlNodeList)

        For Each xn As XmlNode In xnl

            If xn.Name = "controlset" Then

                Dim strAction As String = GetXmlAttribute(xn, "action"),
                    iKeys As Keys? = [Enum].Parse(GetType(Keys), GetXmlAttribute(xn, "key"), True)

                If Not IsValidAction(strAction) Then Continue For
                If iKeys Is Nothing Then Continue For
                If g_ControlSets.ContainsKey(strAction) Then Continue For
                If g_ControlSets.ContainsValue(iKeys) Then Continue For

                g_ControlSets.Add(strAction, iKeys)

            End If

        Next

    End Sub

    Private Function IsValidAction(ByVal a As String) As Boolean

        For Each act As String In m_ActionLists

            If a = act Then Return True

        Next

        Return False

    End Function
    Private Function GetXmlAttribute(ByVal Node As XmlNode, ByVal AttributeName As String) As String

        Dim XmlAttr As XmlAttribute = Node.Attributes.GetNamedItem(AttributeName)
        If XmlAttr IsNot Nothing Then Return XmlAttr.Value
        Return String.Empty

    End Function

#End Region

#Region "Movement"

    ''' <summary>
    ''' 왼쪽으로 이동할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property MoveLeft As Keys
        Get
            Return g_ControlSets("move_left")
        End Get
    End Property

    ''' <summary>
    ''' 오른쪽으로 이동할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property MoveRight As Keys
        Get
            Return g_ControlSets("move_right")
        End Get
    End Property

    ''' <summary>
    ''' 위로 이동할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property MoveUp As Keys
        Get
            Return g_ControlSets("move_up")
        End Get
    End Property

    ''' <summary>
    ''' 아래로 이동할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property MoveDown As Keys
        Get
            Return g_ControlSets("move_down")
        End Get
    End Property

#End Region

#Region "Game Control"

    ''' <summary>
    ''' 메뉴로 갈 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Menu As Keys
        Get
            Return g_ControlSets("menu")
        End Get
    End Property

    ''' <summary>
    ''' 종료할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property [Exit] As Keys
        Get
            Return g_ControlSets("exit")
        End Get
    End Property

    ''' <summary>
    ''' 일시적으로 게임을 중단할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Pause As Keys
        Get
            Return g_ControlSets("pause")
        End Get
    End Property

#End Region

#Region "Action"

    ''' <summary>
    ''' 점프할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Jump As Keys
        Get
            Return g_ControlSets("jump")
        End Get
    End Property

    ''' <summary>
    ''' 달리기를 할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Sprint As Keys
        Get
            Return g_ControlSets("sprint")
        End Get
    End Property

    ''' <summary>
    ''' 날 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Fly As Keys
        Get
            Return g_ControlSets("fly")
        End Get
    End Property

    ''' <summary>
    ''' 1번 공격할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Attack1 As Keys
        Get
            Return g_ControlSets("attack1")
        End Get
    End Property

    ''' <summary>
    ''' 2번 공격할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Attack2 As Keys
        Get
            Return g_ControlSets("attack2")
        End Get
    End Property

    ''' <summary>
    ''' 3번 공격할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Attack3 As Keys
        Get
            Return g_ControlSets("attack3")
        End Get
    End Property

    ''' <summary>
    ''' 4번 공격할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Attack4 As Keys
        Get
            Return g_ControlSets("attack4")
        End Get
    End Property

    ''' <summary>
    ''' 1번 특수기능을 사용할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Skill1 As Keys
        Get
            Return g_ControlSets("skill1")
        End Get
    End Property

    ''' <summary>
    ''' 2번 특수기능을 사용할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Skill2 As Keys
        Get
            Return g_ControlSets("skill2")
        End Get
    End Property

    ''' <summary>
    ''' 3번 특수기능을 사용할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Skill3 As Keys
        Get
            Return g_ControlSets("skill3")
        End Get
    End Property

    ''' <summary>
    ''' 4번 특수기능을 사용할 때 사용되는 키를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Skill4 As Keys
        Get
            Return g_ControlSets("skill4")
        End Get
    End Property

#End Region

#Region "Dictionary"

    Public ReadOnly Property ControlSets As Dictionary(Of String, Int32)
        Get
            Return g_ControlSets
        End Get
    End Property

#End Region

End Class