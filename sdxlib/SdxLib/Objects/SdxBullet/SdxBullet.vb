' SlaneR's DirectX Library (SdxLib)
'
' File:
'   SdxBullet.vb
'
' Dependencies:
'   Microsoft.DirectX.Direct3D
'   System.Drawing
'
' Version: 
'   Maj | Min | Bld | Rev
'    1  .  0  .  0  .  11
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
'   Defines SdxBullet class's body. (internal variables and interface implements)

Imports Microsoft.DirectX.Direct3D
Imports System.Drawing

Public Class SdxBullet
    Inherits SdxObject
    Implements IGraphicsResource, IAlphaMapSupportedGraphicsResource

    Private m_CreationTime As Int32
    Private g_Location As Vector2D
    Private g_Size As Size
    Private g_Speed As Vector2D
    Private g_BulletTexture As Texture
    Private g_Damage As Int32
    Private g_AlphaMap() As Byte
    Private g_Owner As SdxPlayer

    Private m_LoadFromImage As Boolean = False


#Region " - IGraphicsResources - "

#Region "IDisposable Support"

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                If m_LoadFromImage Then g_BulletTexture.Dispose()
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

    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property Color As System.Drawing.Color Implements IGraphicsResource.Color
        Get

        End Get
        Set(ByVal value As System.Drawing.Color)

        End Set
    End Property
    <DebuggerBrowsable(False), System.ComponentModel.Browsable(False), System.ComponentModel.EditorBrowsable(False), Obsolete("이 속성은 사용되지 않습니다.", True)> _
    Public Property Opacity As Single Implements IGraphicsResource.Opacity
        Get
            Return Nothing
        End Get
        Set(ByVal value As Single)

        End Set
    End Property

#End Region

End Class