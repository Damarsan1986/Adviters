Public Class DVV

    Private _idtabla As String
    Public Property idtabla() As String
        Get
            Return _idtabla
        End Get
        Set(ByVal value As String)
            _idtabla = value
        End Set
    End Property

    Private _tabla_DVV As String
    Public Property tabla_DVV() As String
        Get
            Return _tabla_DVV
        End Get
        Set(ByVal value As String)
            _tabla_DVV = value
        End Set
    End Property

End Class
