Imports System.Globalization

Public Class Usuario

    Private _idUsuario As String
    Public Property idUsuario() As String
        Get
            Return _idUsuario
        End Get
        Set(ByVal value As String)
            _idUsuario = value
        End Set
    End Property

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _apellido As String
    Public Property apellido() As String
        Get
            Return _apellido
        End Get
        Set(ByVal value As String)
            _apellido = value
        End Set
    End Property

    Private _clave As String
    Public Property Clave() As String
        Get
            Return _clave
        End Get
        Set(ByVal value As String)
            _clave = value
        End Set
    End Property

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property


    Private _intentosFallidos As Byte
    Public Property IntentosFallidos() As Byte
        Get
            Return _intentosFallidos
        End Get
        Set(ByVal value As Byte)
            _intentosFallidos = value
        End Set
    End Property

    Private _bloqueado As Boolean
    Public Property Bloqueado() As Boolean
        Get
            Return _bloqueado
        End Get
        Set(ByVal value As Boolean)
            _bloqueado = value
        End Set
    End Property

    Private _cultura As CultureInfo
    Public Property Cultura() As CultureInfo
        Get
            Return _cultura
        End Get
        Set(ByVal value As CultureInfo)
            _cultura = value
        End Set
    End Property

    Private _perfil As PermisoBase
    Public Property Perfil() As PermisoBase
        Get
            Return _perfil
        End Get
        Set(ByVal value As PermisoBase)
            _perfil = value
        End Set
    End Property

    Private _usuarioDVH As String
    Public Property usuarioDVH() As String
        Get
            Return _usuarioDVH
        End Get
        Set(ByVal value As String)
            _usuarioDVH = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Me.idUsuario
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If Not obj Is Nothing AndAlso TypeOf obj Is Usuario Then
            Return Me.idUsuario.Equals(CType(obj, Usuario).idUsuario)
        Else
            Return False
        End If
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

End Class
