
Public Class SeguridadException
    Inherits Exception

    Private _codigoError As Integer
    Public Property CodigError() As Integer
        Get
            Return _codigoError
        End Get
        Set(ByVal value As Integer)
            _codigoError = value
        End Set
    End Property

    Public Sub New(ByVal codigoError As Integer)
        Me.CodigError = codigoError
    End Sub
    Public Sub New(ByVal codigoError As Integer, ByVal message As String)
        MyBase.New(message)
        Me.CodigError = codigoError
    End Sub
    Public Sub New(ByVal codigoError As Integer, ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
        Me.CodigError = codigoError
    End Sub

End Class
