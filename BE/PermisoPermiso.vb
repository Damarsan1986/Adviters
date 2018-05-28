Public Class PermisoPermiso

    Private _codigoPadre As String
    Public Property codigoPadre() As String
        Get
            Return _codigoPadre
        End Get
        Set(ByVal value As String)
            _codigoPadre = value
        End Set
    End Property

    Private _codigoHijo As String
    Public Property codigoHijo() As String
        Get
            Return _codigoHijo
        End Get
        Set(ByVal value As String)
            _codigoHijo = value
        End Set
    End Property


    Private _permisoPermisoDVH As String
    Public Property permisoPermisoDVH() As String
        Get
            Return _permisoPermisoDVH
        End Get
        Set(ByVal value As String)
            _permisoPermisoDVH = value
        End Set
    End Property
End Class

