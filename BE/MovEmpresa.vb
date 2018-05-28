Public Class MovEmpresa
    Private _idEmpresa As Integer
    Public Property idEmpresa() As Integer
        Get
            Return _idEmpresa
        End Get
        Set(ByVal value As Integer)
            _idEmpresa = value
        End Set
    End Property

    Private _idComprobante As Integer
    Public Property idComprobante() As Integer
        Get
            Return _idComprobante
        End Get
        Set(ByVal value As Integer)
            _idComprobante = value
        End Set
    End Property

    Private _fechaHora As DateTime
    Public Property fechaHora() As DateTime
        Get
            Return _fechaHora
        End Get
        Set(ByVal value As DateTime)
            _fechaHora = value
        End Set
    End Property

    Private _cantidad As Integer
    Public Property cantidad() As Integer
        Get
            Return _cantidad
        End Get
        Set(ByVal value As Integer)
            _cantidad = value
        End Set
    End Property

    Private _accion As String
    Public Property accion() As String
        Get
            Return _accion
        End Get
        Set(ByVal value As String)
            _accion = value
        End Set
    End Property

    Private _observaciones As String
    Public Property observaciones() As String
        Get
            Return _observaciones
        End Get
        Set(ByVal value As String)
            _observaciones = value
        End Set
    End Property

    Private _movEmpresaDVH As String
    Public Property movEmpresaDVH() As String
        Get
            Return _movEmpresaDVH
        End Get
        Set(ByVal value As String)
            _movEmpresaDVH = value
        End Set
    End Property

End Class
