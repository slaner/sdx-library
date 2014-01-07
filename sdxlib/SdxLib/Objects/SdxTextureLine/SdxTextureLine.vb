' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxTextureLine.vb
'
' Dependencies:
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  12
'
' Date:
'   2013/12/17
'
' Author:
'   SlaneR
'
' Contact:
'   dev.slaner@gmail.com
'
' Description:
'   Defines SdxTextureLine class.

Imports D3 = Microsoft.DirectX.Direct3D
Imports System.Drawing
''' <summary>
''' 텍스쳐를 이용하여 선을 그리는 작업을 구현합니다.
''' </summary>
Public Class SdxTextureLine
    Inherits SdxObject
    Implements IGraphicsLine

    ''' <summary>
    ''' 선의 시작 점을 저장합니다.
    ''' </summary>
    Private g_Start As Vector2D = Vector2D.Empty

    ''' <summary>
    ''' 선의 끝 점을 저장합니다.
    ''' </summary>
    Private g_Ends As Vector2D = Vector2D.Empty

    ''' <summary>
    ''' 선의 색을 저장합니다.
    ''' </summary>
    Private g_Color As Drawing.Color = Drawing.Color.White

    ''' <summary>
    ''' 선의 투명도를 저장합니다.
    ''' </summary>
    Private g_Opacity As Single = 1.0F

    ''' <summary>
    ''' 페이드 효과를 사용할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_FadeType As FadeType = SDXLib.FadeType.None

    ''' <summary>
    ''' 페이드 효과의 유형을 저장합니다.
    ''' </summary>
    Private g_FadeEffect As FadeEffect = FadeEffect.FadeOut

    ''' <summary>
    ''' 페이드 효과의 단계 수를 저장합니다.
    ''' </summary>
    Private g_FadeStep As Int32 = 60

    ''' <summary>
    ''' 페이드 효과가 끝난 후에 개체를 보존할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_Preserve As Boolean = True

    ''' <summary>
    ''' 페이드 효과가 끝난 후에 개체를 보존할 경우, 페이드 효과를 번갈아가면서 적용할 것인지의 여부를 저장합니다.
    ''' </summary>
    Private g_ToggleFadeEffect As Boolean = False

    ''' <summary>
    ''' 선의 두께를 저장합니다.
    ''' </summary>
    Private g_Thickness As Int32 = 1

    Private m_Dist As Int32 = 0                         ' 시작 점과 끝 점간의 거리를 저장합니다.
    Private m_Angle As Single = 0                       ' 시작 점과 끝 점간의 각도를 저장합니다.

    Private m_LineSize As Size = Nothing                ' 선의 크기를 저장합니다.
    Private m_RotateOrigin As Point = Nothing           ' 선의 회전 중심 축을 저장합니다.
    Private m_LineTexture As D3.Texture = Nothing       ' 선의 텍스쳐를 저장합니다.

    Private m_CurrentStep As Int32 = 0                  ' 페이드 효과의 단계를 저장합니다.
    Private m_FadeStep As Single = 0.0F                 ' 페이드 효과의 단계별 값을 저장합니다. (프레임 기반)
    Private m_FadeAmount As Single = 0.0F               ' 페이드 효과의 단계별 값을 저장합니다. (삼각함수 기반)

    Private m_LoadFromImage As Boolean = False          ' 이미지로부터 텍스쳐를 만들었는지의 여부를 저장합니다.

#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If m_LoadFromImage Then m_LineTexture.Dispose()
            End If

            ' TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 Finalize()를 재정의합니다.
            ' TODO: 큰 필드를 null로 설정합니다.
        End If
        Me.disposedValue = True
    End Sub
    ''' <summary>
    ''' 개체의 사용을 종료하고, 메모리에서 해제합니다.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    ''' <summary>
    ''' 개체가 삭제되었는지 나타내는 값을 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Disposed As Boolean Implements IGraphicsDisposable.Disposed
        Get
            Return disposedValue
        End Get
    End Property

#End Region

#Region "Internal Methods"

    Private Sub UpdateMembers()

        m_Dist = SDXHelper.GetVectorDistance(g_Start, g_Ends)
        m_Angle = SDXHelper.GetVectorAngle(g_Start, g_Ends)

    End Sub
    Private Overloads Sub Initialize(ByVal Img As Image)

        m_LineTexture = SDXHelper.TextureFromImage(Main.Device, Img)
        m_LineSize = Img.Size
        m_RotateOrigin = Point.Round(New PointF(m_LineSize.Width / 2, m_LineSize.Height / 2))

    End Sub
    Private Overloads Sub Initialize(ByVal Tex As D3.Texture)

        m_LineTexture = Tex
        m_LineSize = New Size(Tex.GetLevelDescription(0).Width, Tex.GetLevelDescription(0).Height)
        m_RotateOrigin = Point.Round(New PointF(m_LineSize.Width / 2, m_LineSize.Height / 2))

    End Sub

#End Region

End Class