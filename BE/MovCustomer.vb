Public Class MovCustomer
    Private _idCliente As Integer
    Public Property idCliente() As Integer
        Get
            Return _idCliente
        End Get
        Set(ByVal value As Integer)
            _idCliente = value
        End Set
    End Property

    Private _idCustomer As Integer
    Public Property idCustomer() As Integer
        Get
            Return _idCustomer
        End Get
        Set(ByVal value As Integer)
            _idCustomer = value
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

    Private _movCustomerDVH As String
    Public Property movCustomerDVH() As String
        Get
            Return _movCustomerDVH
        End Get
        Set(ByVal value As String)
            _movCustomerDVH = value
        End Set
    End Property

End Class
