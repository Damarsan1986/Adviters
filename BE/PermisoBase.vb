Public MustInherit Class PermisoBase

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _descripcion As String
    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Private _esAccion As Boolean
    Public Property esAccion() As Boolean
        Get
            Return _esAccion
        End Get
        Set(ByVal value As Boolean)
            _esAccion = value
        End Set
    End Property

    Private _permisoDVH As String
    Public Property permisoDVH() As String
        Get
            Return _permisoDVH
        End Get
        Set(ByVal value As String)
            _permisoDVH = value
        End Set
    End Property
End Class

