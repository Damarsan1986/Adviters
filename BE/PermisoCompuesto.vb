
Public Class PermisoCompuesto
    Inherits PermisoBase

    Private _listahijos As List(Of PermisoBase) = New List(Of PermisoBase)
    Public Property listaHijos() As List(Of PermisoBase)
        Get
            Return _listahijos
        End Get
        Set(ByVal value As List(Of PermisoBase))
            _listahijos = value
        End Set
    End Property

End Class
