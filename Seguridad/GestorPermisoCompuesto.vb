<Serializable()> _
Public Class GestorPermisoCompuesto
    Inherits GestorPermisoBase

    Dim _listaHijos As List(Of BE.PermisoBase) = New List(Of BE.PermisoBase)
    Public Overrides Function EsValido(nombrePermiso As String) As Boolean
        Dim tieneUnValido As Boolean = False
        Dim usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual()
        Dim PerfilUsuario As New BE.PermisoCompuesto
        PerfilUsuario = usuario.Perfil

        For Each p In PerfilUsuario.listaHijos
            tieneUnValido = tieneUnValido Or p.Nombre = nombrePermiso
            If Not p.esAccion Then
                Dim pHIjos As New BE.PermisoCompuesto
                pHIjos = p
                For Each pH In pHIjos.listaHijos
                    tieneUnValido = tieneUnValido Or pH.Nombre = nombrePermiso
                Next
            End If
        Next

        Return tieneUnValido
    End Function

    Public Overrides Function TieneHijos() As Boolean
        Return True
    End Function


    Public Overrides Function AgregarHijo(permiso As BE.PermisoBase) As List(Of BE.PermisoBase)
        If Not _listaHijos.Contains(permiso) Then
            _listaHijos.Add(permiso)
            Return _listaHijos
        Else
            Return Nothing
        End If
    End Function

    Public Overrides Function ObtenerHijos(nombrePermiso As String) As List(Of BE.PermisoBase)

        Return _listaHijos
    End Function

    Public Overrides Function QuitarHijo(permiso As BE.PermisoBase) As Boolean
        Return _listaHijos.Remove(permiso)
    End Function

    Public Overrides Sub LimpiarHijos(nombrePermiso As String)
        _listaHijos.Clear()
    End Sub
End Class
