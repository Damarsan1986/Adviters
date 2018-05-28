Imports Seguridad
<Serializable()> _
Public Class SesionActualWindows
    Inherits SesionActualBase

    Private Shared ReadOnly _instancia As SesionActualBase = New SesionActualWindows()
    Dim _gestorMensaje As GestorMensaje = New GestorMensaje
    Public Shared Function SesionActual() As SesionActualBase
        Return _instancia
    End Function
    Private Sub New()
    End Sub

    Private _usuario As BE.Usuario
    Protected Overrides Sub EstablecerUsuarioActual(usuario As BE.Usuario)
        Me._usuario = usuario
    End Sub

    Public Overrides Function ObtenerUsuarioActual() As BE.Usuario
        Return Me._usuario
    End Function

    Public Overrides Sub EstablecerCulturaActual(usuario As BE.Usuario)
        Me._usuario.Cultura = usuario.Cultura
    End Sub

    Public Overrides Function ObtenerCulturaActual() As String
        Return Me._usuario.Cultura.ToString
    End Function
End Class
