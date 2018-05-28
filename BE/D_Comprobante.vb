Public Class D_Comprobante
    Private _idComprobante As Integer
    Public Property idComprobante() As Integer
        Get
            Return _idComprobante
        End Get
        Set(ByVal value As Integer)
            _idComprobante = value
        End Set
    End Property

    Private _idD_Comprobante As Integer
    Public Property idD_Comprobante() As Integer
        Get
            Return _idD_Comprobante
        End Get
        Set(ByVal value As Integer)
            _idD_Comprobante = value
        End Set
    End Property

    Private _idProducto As Integer
    Public Property idProducto() As Integer
        Get
            Return _idProducto
        End Get
        Set(ByVal value As Integer)
            _idProducto = value
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

    Private _pUnitario As Double
    Public Property pUnitario() As Double
        Get
            Return _pUnitario
        End Get
        Set(ByVal value As Double)
            _pUnitario = value
        End Set
    End Property

    Private _dComprobanteDVH As String
    Public Property dComprobanteDVH() As String
        Get
            Return _dComprobanteDVH
        End Get
        Set(ByVal value As String)
            _dComprobanteDVH = value
        End Set
    End Property

End Class
