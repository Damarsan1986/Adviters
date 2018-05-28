Imports System.Globalization
Public Class MensajeComplejo

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

    Private _descripcion2 As String
    Public Property descripcion2() As String
        Get
            Return _descripcion2
        End Get
        Set(ByVal value As String)
            _descripcion2 = value
        End Set
    End Property



End Class
