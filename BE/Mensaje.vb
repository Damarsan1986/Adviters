Imports System.Globalization
Public Class Mensaje

    Private _idMensaje As String
    Public Property idMensaje() As String
        Get
            Return _idMensaje
        End Get
        Set(ByVal value As String)
            _idMensaje = value
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

    Private _descripcion As String
    Public Property descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal value As String)
            _Descripcion = value
        End Set
    End Property

    Private _mensajeDVH As String
    Public Property mensajeDVH() As String
        Get
            Return _mensajeDVH
        End Get
        Set(ByVal value As String)
            _mensajeDVH = value
        End Set
    End Property


End Class
