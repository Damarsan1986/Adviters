Public Class Proveedor
    Private _idProveedor As Integer
    Public Property idProveedor() As Integer
        Get
            Return _idProveedor
        End Get
        Set(ByVal value As Integer)
            _idProveedor = value
        End Set
    End Property

    Private _razonSocial As String
    Public Property razonSocial() As String
        Get
            Return _razonSocial
        End Get
        Set(ByVal value As String)
            _razonSocial = value
        End Set
    End Property

    Private _cuit As Long
    Public Property cuit() As Long
        Get
            Return _cuit
        End Get
        Set(ByVal value As Long)
            _cuit = value
        End Set
    End Property

    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Private _domicilio As String
    Public Property domicilio() As String
        Get
            Return _domicilio
        End Get
        Set(ByVal value As String)
            _domicilio = value
        End Set
    End Property

    Private _localidad As String
    Public Property localidad() As String
        Get
            Return _localidad
        End Get
        Set(ByVal value As String)
            _localidad = value
        End Set
    End Property

    Private _provincia As String
    Public Property provincia() As String
        Get
            Return _provincia
        End Get
        Set(ByVal value As String)
            _provincia = value
        End Set
    End Property

    Private _pais As String
    Public Property pais() As String
        Get
            Return _pais
        End Get
        Set(ByVal value As String)
            _pais = value
        End Set
    End Property

    Private _CP As String
    Public Property CP() As String
        Get
            Return _CP
        End Get
        Set(ByVal value As String)
            _CP = value
        End Set
    End Property

    Private _SFI As Integer
    Public Property SFI() As Integer
        Get
            Return _SFI
        End Get
        Set(ByVal value As Integer)
            _SFI = value
        End Set
    End Property

    Private _fechaAlta As Date
    Public Property fechaAlta() As Date
        Get
            Return _fechaAlta
        End Get
        Set(ByVal value As Date)
            _fechaAlta = value
        End Set
    End Property

    Private _proveedorDVH As String
    Public Property proveedorDVH() As String
        Get
            Return _proveedorDVH
        End Get
        Set(ByVal value As String)
            _proveedorDVH = value
        End Set
    End Property









End Class
