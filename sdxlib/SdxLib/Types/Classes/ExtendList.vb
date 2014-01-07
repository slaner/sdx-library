Imports System.ComponentModel

''' <summary>
''' 콜렉션의 추가 및 삭제에 대한 이벤트와, 첫번째 요소와 마지막 요소를 접근할 수 있도록 기능이 추가된 List(Of T) 클래스를 구현합니다.
''' </summary>
<DebuggerDisplay("First: {First}, Last: {Last}")>
Public Class ExtendList(Of T)
    Implements IList(Of T)

    Private g_InnerList As New List(Of T)

    ''' <summary>
    ''' 요소에 아이템이 추가될 때 발생합니다.
    ''' </summary>
    Public Event OnItemAdded(ByVal Item As T)

    ''' <summary>
    ''' 요소의 아이템이 제거될 때 발생합니다.
    ''' </summary>
    Public Event OnItemRemoved(ByVal Item As T)



    ''' <summary>
    ''' 목록에 있는 첫번째 요소를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property First As T
        Get
            If Me.Count = 0 Then Return Nothing
            Return Me(0)
        End Get
    End Property

    ''' <summary>
    ''' 목록에 있는 마지막 요소를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Last As T
        Get
            If Me.Count = 0 Then Return Nothing
            Return Me(Me.Count - 1)
        End Get
    End Property

    ''' <summary>
    ''' 개체를 ExtendList(Of T)의 끝 부분에 추가합니다.
    ''' </summary>
    ''' <param name="item">추가할 아이템을 입력합니다.</param>
    Public Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
        g_InnerList.Add(item)
        RaiseEvent OnItemAdded(item)
    End Sub

    ''' <summary>
    ''' ExtendList(Of T)에서 요소를 모두 제거합니다.
    ''' </summary>
    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
        g_InnerList.Clear()
    End Sub

    ''' <summary>
    ''' 요소가 ExtendList(Of T)에 있는지 여부를 확인합니다.
    ''' </summary>
    ''' <param name="item">ExtendList(Of T)에서 찾을 개체입니다.참조 형식에 대해 값은 null이 될 수 있습니다.</param>
    Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
        Return g_InnerList.Contains(item)
    End Function

    ''' <summary>
    ''' 대상 배열의 지정된 인덱스부터 시작하여 전체 ExtendList(Of T)을 호환되는 1차원 배열에 복사합니다.
    ''' </summary>
    ''' <param name="array">ExtendList(Of T)에서 복사한 요소의 대상인 일차원 System.Array입니다.System.Array의 인덱스는 0부터 시작해야 합니다.</param>
    ''' <param name="arrayIndex">array에서 복사가 시작되는 0부터 시작하는 인덱스입니다.</param>
    ''' <remarks></remarks>
    Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
        g_InnerList.CopyTo(array, arrayIndex)
    End Sub

    ''' <summary>
    ''' ExtendList(Of T)에 실제로 포함된 요소의 수를 가져옵니다.
    ''' </summary>
    Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of T).Count
        Get
            Return g_InnerList.Count
        End Get
    End Property

    ''' <summary>
    ''' ExtendList(Of T)가 읽기 전용인지 여부를 확인합니다.
    ''' </summary>
    Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' ExtendList(Of T)에서 맨 처음 나타나는 특정 개체를 제거합니다.
    ''' </summary>
    ''' <param name="item">ExtendList(Of T)에서 제거할 개체입니다.참조 형식에 대해 값은 null이 될 수 있습니다.</param>
    Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
        If g_InnerList.Remove(item) Then
            RaiseEvent OnItemRemoved(item)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' ExtendList(Of T)을 반복하는 열거자를 반환합니다.
    ''' </summary>
    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        Return g_InnerList.GetEnumerator()
    End Function

    ''' <summary>
    ''' 지정된 개체를 검색하고, 전체 ExtendList(Of T)에서 처음으로 검색한 개체의 인덱스(0부터 시작)를 반환합니다.
    ''' </summary>
    ''' <param name="item">ExtendList(Of T)에서 찾을 개체입니다.참조 형식에 대해 값은 null이 될 수 있습니다.</param>
    Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
        Return g_InnerList.IndexOf(item)
    End Function

    ''' <summary>
    ''' ExtendList(Of T)의 지정된 인덱스에 요소를 삽입합니다.
    ''' </summary>
    ''' <param name="index">item을 삽입해야 하는 0부터 시작하는 인덱스입니다.</param>
    ''' <param name="item">삽입할 개체입니다.참조 형식에 대해 값은 null이 될 수 있습니다.</param>
    Public Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
        g_InnerList.Insert(index, item)
        RaiseEvent OnItemAdded(item)
    End Sub

    ''' <summary>
    ''' 지정한 인덱스에 있는 요소를 가져오거나 설정합니다.
    ''' </summary>
    ''' <param name="index">가져오거나 설정할 요소의 0부터 시작하는 인덱스입니다.</param>
    Default Public Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
        Get
            Return g_InnerList(index)
        End Get
        Set(ByVal value As T)
            g_InnerList(index) = value
        End Set
    End Property

    ''' <summary>
    ''' ExtendList(Of T)의 지정한 인덱스에서 요소를 제거합니다.
    ''' </summary>
    ''' <param name="index">제거할 요소의 0부터 시작하는 인덱스입니다.</param>
    Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
        RaiseEvent OnItemRemoved(g_InnerList(index))
        g_InnerList.RemoveAt(index)
    End Sub

    <Browsable(False), EditorBrowsable(False)>
    Public Function UnusedFunction() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return Nothing
    End Function

End Class