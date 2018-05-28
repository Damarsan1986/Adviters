Public Class Menu
    Private _menuId As Integer
    Public Property menuId() As Integer
        Get
            Return _menuId
        End Get
        Set(ByVal value As Integer)
            _menuId = value
        End Set
    End Property

    Private _parentMenuId As Integer
    Public Property parentMenuId() As Integer
        Get
            Return _parentMenuId
        End Get
        Set(ByVal value As Integer)
            _parentMenuId = value
        End Set
    End Property

    Private _titulo As String
    Public Property titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property


    Private _descripcion As String
    Public Property descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Private _url As String
    Public Property url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Private _menuDVH As String
    Public Property menuDVH() As String
        Get
            Return _menuDVH
        End Get
        Set(ByVal value As String)
            _menuDVH = value
        End Set
    End Property
    
End Class
