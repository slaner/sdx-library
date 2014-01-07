' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxMain/ctor.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Windows.Forms
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  2  .  8  .  47
'
' Date:
'   2013/12/10
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxMain class's constructor.

Imports Microsoft.DirectX.Direct3D
Imports System.Drawing
Imports System.Windows.Forms

Partial Class SDXMain

    ''' <summary>
    ''' SDXMain 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Width">그래픽 작업을 출력할 윈도우의 넓이를 입력합니다.</param>
    ''' <param name="Height">그래픽 작업을 출력할 윈도우의 높이를 입력합니다.</param>
    Public Sub New(ByVal Width As Int32, ByVal Height As Int32)

        If Not InitDX(New Size(Width, Height)) Then Throw New ArgumentException()

    End Sub

    ''' <summary>
    ''' SDXMain 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Size">그래픽 작업을 출력할 윈도우의 크기를 입력합니다.</param>
    Public Sub New(ByVal Size As Size)

        If Not InitDX(Size) Then Throw New ArgumentException()

    End Sub

    ''' <summary>
    ''' SDXMain 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Width">그래픽 작업을 출력할 윈도우의 넓이를 입력합니다.</param>
    ''' <param name="Height">그래픽 작업을 출력할 윈도우의 높이를 입력합니다.</param>
    ''' <param name="ApplicationTitle">그래픽 작업을 출력할 윈도우의 제목을 입력합니다.</param>
    ''' <param name="Fullscreen">전체 화면으로 실행할 것인지의 여부를 입력합니다.</param>
    Public Sub New(ByVal Width As Int32, ByVal Height As Int32, ByVal ApplicationTitle As String, ByVal Fullscreen As Boolean)

        If Not InitDX(New Size(Width, Height), ApplicationTitle, Fullscreen) Then Throw New ArgumentException()

    End Sub

    ''' <summary>
    ''' SDXMain 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Size">그래픽 작업을 출력할 윈도우의 크기를 입력합니다.</param>
    ''' <param name="ApplicationTitle">그래픽 작업을 출력할 윈도우의 제목을 입력합니다.</param>
    ''' <param name="Fullscreen">전체 화면으로 실행할 것인지의 여부를 입력합니다.</param>
    Public Sub New(ByVal Size As Size, ByVal ApplicationTitle As String, ByVal Fullscreen As Boolean)

        If Not InitDX(Size, ApplicationTitle, Fullscreen) Then Throw New ArgumentException()

    End Sub

    ''' <summary>
    ''' SDXMain 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Width">그래픽 작업을 출력할 윈도우의 넓이를 입력합니다.</param>
    ''' <param name="Height">그래픽 작업을 출력할 윈도우의 높이를 입력합니다.</param>
    ''' <param name="ApplicationTitle">그래픽 작업을 출력할 윈도우의 제목을 입력합니다.</param>
    ''' <param name="PresentParameter">만들어질 DirectX 장치의 옵션을 결정하는 PresentParameters 개체를 입력합니다.</param>
    Public Sub New(ByVal Width As Int32, ByVal Height As Int32, ByVal ApplicationTitle As String, ByVal PresentParameter As PresentParameters)

        If Not InitDX(New Size(Width, Height), ApplicationTitle, , PresentParameter) Then Throw New ArgumentException()

    End Sub

    ''' <summary>
    ''' SDXMain 개체를 초기화합니다.
    ''' </summary>
    ''' <param name="Size">그래픽 작업을 출력할 윈도우의 크기를 입력합니다.</param>
    ''' <param name="ApplicationTitle">그래픽 작업을 출력할 윈도우의 제목을 입력합니다.</param>
    ''' <param name="PresentParameter">만들어질 DirectX 장치의 옵션을 결정하는 PresentParameters 개체를 입력합니다.</param>
    Public Sub New(ByVal Size As Size, ByVal ApplicationTitle As String, ByVal PresentParameter As PresentParameters)

        If Not InitDX(Size, ApplicationTitle, , PresentParameter) Then Throw New ArgumentException()

    End Sub

End Class