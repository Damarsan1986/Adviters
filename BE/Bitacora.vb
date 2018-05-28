Public Class bitacora

    Private _idBitacora As Integer
    Public Property idBitacora() As Integer
        Get
            Return _idBitacora
        End Get
        Set(ByVal value As Integer)
            _idBitacora = value
        End Set
    End Property

    Private _idUsuario As String
    Public Property idUsuario() As String
        Get
            Return _idUsuario
        End Get
        Set(ByVal value As String)
            _idUsuario = value
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

    Private _fechaHoraEvento As DateTime
    Public Property fechaHoraEvento() As DateTime
        Get
            Return _fechaHoraEvento
        End Get
        Set(ByVal value As DateTime)
            _fechaHoraEvento = value
        End Set
    End Property

    Private _dvhBitacora As String
    Public Property dvhBitacora() As String
        Get
            Return _dvhBitacora
        End Get
        Set(ByVal value As String)
            _dvhBitacora = value
        End Set
    End Property

End Class
