
Public Class PermisoFiltro
    Inherits PermisoSimple

    Private _filtrarPorSimpleCompuesto As Boolean
    Public Property FiltrarPorSimpleCompuesto() As Boolean
        Get
            Return _filtrarPorSimpleCompuesto
        End Get
        Set(ByVal value As Boolean)
            _filtrarPorSimpleCompuesto = value
        End Set
    End Property

    Private _soloSimples As Boolean
    Public Property SoloSimples() As Boolean
        Get
            Return _soloSimples
        End Get
        Set(ByVal value As Boolean)
            _soloSimples = value
        End Set
    End Property


End Class
