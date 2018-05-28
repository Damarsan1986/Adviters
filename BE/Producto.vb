Public Class Producto
    Private _idProducto As Integer
    Public Property idProducto() As Integer
        Get
            Return _idProducto
        End Get
        Set(ByVal value As Integer)
            _idProducto = value
        End Set
    End Property

    Private _tituloProducto As String
    Public Property tituloProducto() As String
        Get
            Return _tituloProducto
        End Get
        Set(ByVal value As String)
            _tituloProducto = value
        End Set
    End Property

    Private _Descripcion As String
    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal value As String)
            _Descripcion = value
        End Set
    End Property

    Private _tipoProducto As String
    Public Property tipoProducto() As String
        Get
            Return _tipoProducto
        End Get
        Set(ByVal value As String)
            _tipoProducto = value
        End Set
    End Property

    Private _marca As String
    Public Property marca() As String
        Get
            Return _marca
        End Get
        Set(ByVal value As String)
            _marca = value
        End Set
    End Property

    Private _picture As String
    Public Property picture() As String
        Get
            Return _picture
        End Get
        Set(ByVal value As String)
            _picture = value
        End Set
    End Property

    Private _categoria As String
    Public Property categoria() As String
        Get
            Return _categoria
        End Get
        Set(ByVal value As String)
            _categoria = value
        End Set
    End Property


    Private _Precio As Double
    Public Property Precio() As Double
        Get
            Return _Precio
        End Get
        Set(ByVal value As Double)
            _Precio = value
        End Set
    End Property

    Private _StockMaximo As Integer
    Public Property StockMaximo() As Integer
        Get
            Return _StockMaximo
        End Get
        Set(ByVal value As Integer)
            _StockMaximo = value
        End Set
    End Property

    Private _stockMinimo As Integer
    Public Property stockMinimo() As Integer
        Get
            Return _stockMinimo
        End Get
        Set(ByVal value As Integer)
            _stockMinimo = value
        End Set
    End Property

    Private _productoDVH As String
    Public Property productoDVH() As String
        Get
            Return _productoDVH
        End Get
        Set(ByVal value As String)
            _productoDVH = value
        End Set
    End Property
End Class
