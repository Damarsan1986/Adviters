Public Class TipoCambio

    Private _idMoneda As Integer
    Public Property idMoneda() As Integer
        Get
            Return _idMoneda
        End Get
        Set(ByVal value As Integer)
            _idMoneda = value
        End Set
    End Property

    Private _fechaHoraVencimiento As DateTime
    Public Property fechaHoraVencimiento() As DateTime
        Get
            Return _fechaHoraVencimiento
        End Get
        Set(ByVal value As DateTime)
            _fechaHoraVencimiento = value
        End Set
    End Property

    Private _precioCompra As Double
    Public Property precioCompra() As Double
        Get
            Return _PrecioCompra
        End Get
        Set(ByVal value As Double)
            _PrecioCompra = value
        End Set
    End Property

    Private _precioVenta As Double
    Public Property previoVenta() As Double
        Get
            Return _precioVenta
        End Get
        Set(ByVal value As Double)
            _precioVenta = value
        End Set
    End Property

    Private _fechaHoraUltMod As DateTime
    Public Property fechaHoraUltMod() As DateTime
        Get
            Return _fechaHoraUltMod
        End Get
        Set(ByVal value As DateTime)
            _fechaHoraUltMod = value
        End Set
    End Property

    Private _usuarioUltMod As String
    Public Property usuarioUltMod() As String
        Get
            Return _usuarioUltMod
        End Get
        Set(ByVal value As String)
            _usuarioUltMod = value
        End Set
    End Property

    Private _tipoCambioDVH As String
    Public Property tipoCambioDVH() As String
        Get
            Return _tipoCambioDVH
        End Get
        Set(ByVal value As String)
            _tipoCambioDVH = value
        End Set
    End Property





End Class
