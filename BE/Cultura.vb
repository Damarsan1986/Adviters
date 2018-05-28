Imports System.Globalization
Public Class Cultura
    Private _idCultura As CultureInfo
    Public Property idCultura() As CultureInfo
        Get
            Return _idCultura
        End Get
        Set(ByVal value As CultureInfo)
            _idCultura = value
        End Set
    End Property

    Private _descripcion As String
    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Private _culturaDVH As String
    Public Property culturaDVH() As String
        Get
            Return _culturaDVH
        End Get
        Set(ByVal value As String)
            _culturaDVH = value
        End Set
    End Property
End Class
