<Serializable()> _
Public MustInherit Class GestorPermisoBase
    Dim PB As BE.PermisoBase

    Public MustOverride Function EsValido(nombrePermiso As String) As Boolean

    Public MustOverride Function TieneHijos() As Boolean

    Public MustOverride Sub LimpiarHijos(nombrePermiso As String)

    Public MustOverride Function AgregarHijo(permiso As BE.PermisoBase) As List(Of BE.PermisoBase)

    Public MustOverride Function QuitarHijo(permiso As BE.PermisoBase) As Boolean

    Public MustOverride Function ObtenerHijos(nombrePermiso As String) As List(Of BE.PermisoBase)

    Public Overrides Function ToString() As String
        Return PB.Nombre
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If Not obj Is Nothing Then
            If TypeOf obj Is BE.PermisoBase Then
                ' comparacion contra un objeto PermisoBase
                Return PB.Nombre.Equals(CType(obj, BE.PermisoBase).Nombre)
            ElseIf TypeOf obj Is String Then
                ' comparacion contra un String
                Return PB.Nombre.Equals(obj)
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

End Class

