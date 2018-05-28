Public Class Movimiento
    Private _idProducto As Integer
    Public Property idProducto() As Integer
        Get
            Return _idProducto
        End Get
        Set(ByVal value As Integer)
            _idProducto = value
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

    Private _movimientosDVH As String
    Public Property movimientosDVH() As String
        Get
            Return _movimientosDVH
        End Get
        Set(ByVal value As String)
            _movimientosDVH = value
        End Set
    End Property

End Class
