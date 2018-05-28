<Serializable()> _
Public Class GestorPermisoSimple
    Inherits GestorPermisoBase

    Dim PB As BE.PermisoBase
    Public Overrides Function EsValido(nombrePermiso As String) As Boolean
        Dim tieneUnValido As Boolean = False
        Dim usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual()
        Dim PerfilUsuario As New BE.PermisoSimple
        PerfilUsuario = usuario.Perfil

        If PerfilUsuario.Nombre = nombrePermiso Then
            tieneUnValido = True
        End If

        Return tieneUnValido
    End Function

    Public Overrides Function TieneHijos() As Boolean
        Return False
    End Function

    Public Overrides Function AgregarHijo(permiso As BE.PermisoBase) As List(Of BE.PermisoBase)
        Return Nothing
    End Function

    Public Overrides Function ObtenerHijos(nombrePermiso As String) As System.Collections.Generic.List(Of BE.PermisoBase)
        Return Nothing
    End Function

    Public Overrides Function QuitarHijo(permiso As BE.PermisoBase) As Boolean
        Return False
    End Function

    Public Overrides Sub LimpiarHijos(nombrePermiso As String)
    End Sub
End Class
