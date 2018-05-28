Public Class BackUp
    Private _nombre As String
    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _directorio As String
    Public Property directorio() As String
        Get
            Return _directorio
        End Get
        Set(ByVal value As String)
            _directorio = value
        End Set
    End Property

    Private _fechaHora As String
    Public Property fechaHora() As String
        Get
            Return _fechaHora
        End Get
        Set(ByVal value As String)
            _fechaHora = value
        End Set
    End Property



End Class
