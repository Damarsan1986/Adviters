Public Class Moneda

    Private _idMoneda As Integer
    Public Property idMoneda() As Integer
        Get
            Return _idMoneda
        End Get
        Set(ByVal value As Integer)
            _idMoneda = value
        End Set
    End Property

    Private _descripcionCorta As String
    Public Property descripcionCorta() As String
        Get
            Return _descripcionCorta
        End Get
        Set(ByVal value As String)
            _descripcionCorta = value
        End Set
    End Property

    Private _descripcionLarga As String
    Public Property descripcionLarga() As String
        Get
            Return _descripcionLarga
        End Get
        Set(ByVal value As String)
            _descripcionLarga = value
        End Set
    End Property

    Private _monedaDVH As String
    Public Property monedaDVH() As String
        Get
            Return _monedaDVH
        End Get
        Set(ByVal value As String)
            _monedaDVH = value
        End Set
    End Property

End Class
