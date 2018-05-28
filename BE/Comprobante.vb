Public Class Comprobante
    Private _idComprobante As Integer
    Public Property idComprobante() As Integer
        Get
            Return _idComprobante
        End Get
        Set(ByVal value As Integer)
            _idComprobante = value
        End Set
    End Property

    Private _idCliente As Integer
    Public Property idCliente() As Integer
        Get
            Return _idCliente
        End Get
        Set(ByVal value As Integer)
            _idCliente = value
        End Set
    End Property

    Private _idConsumidor As Integer
    Public Property idConsumidor() As Integer
        Get
            Return _idConsumidor
        End Get
        Set(ByVal value As Integer)
            _idConsumidor = value
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

    Private _descOperacion As String
    Public Property descOperacion() As String
        Get
            Return _descOperacion
        End Get
        Set(ByVal value As String)
            _descOperacion = value
        End Set
    End Property

    Private _idOperador As String
    Public Property idOperador() As String
        Get
            Return _idOperador
        End Get
        Set(ByVal value As String)
            _idOperador = value
        End Set
    End Property

    Private _monedaOperacion As Integer
    Public Property monedaOperacion() As Integer
        Get
            Return _monedaOperacion
        End Get
        Set(ByVal value As Integer)
            _monedaOperacion = value
        End Set
    End Property

    Private _comprobanteDVH As String
    Public Property comprobanteDVH() As String
        Get
            Return _comprobanteDVH
        End Get
        Set(ByVal value As String)
            _comprobanteDVH = value
        End Set
    End Property

End Class
