<Serializable()> _
Public Class GestorUsuario

    Dim gestor_usuario As New DAL.MapeadorUsuario
    Dim _gestorPermiso As New GestorPermiso
    Dim sumauser As Integer = 0
    Dim _gestorDVV As New GestorDVV

    'Calculo del DVH al escribir en la tabla de Usuarios
    Public Function escribir_usuario(user As BE.Usuario) As Boolean
        Dim claveEncriptada As String
        If String.IsNullOrEmpty(user.Clave) Then
            Dim usuario As BE.Usuario = leer_usuario(user.idUsuario)
            claveEncriptada = usuario.Clave
        Else
            claveEncriptada = Criptografia.ObtenerInstancia().MD5(user.Clave)
        End If
        user.Clave = claveEncriptada
        Dim mistring As String = user.idUsuario.ToString + user.Clave.ToString + user.Perfil.Nombre.ToString + user.Email.ToString + user.IntentosFallidos.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        user.usuarioDVH = miDVH
        Dim i As Integer = gestor_usuario.escribir_usuario(user)
        If i > 0 Then
            'Se escribio correctamente
            ActualizarSum()
            Return True
        End If
        Return False
    End Function

    Public Function leer_usuario() As List(Of BE.Usuario)
        Dim ListaUsuario As List(Of BE.Usuario) = gestor_usuario.leer_usuario()
        Dim ls As New List(Of BE.Usuario)
        For Each user As BE.Usuario In ListaUsuario
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro = user.Perfil
            user.Perfil = _gestorPermiso.leer_UnPermiso(_permisoFiltro)
            ls.Add(user)
        Next
        Return ls

    End Function

    Public Function leer_usuario_Empresa(idCliente As String) As List(Of BE.Usuario)
        Dim ListaUsuario As List(Of BE.Usuario) = gestor_usuario.leer_usuario_Empresa(idCliente)
        Dim ls As New List(Of BE.Usuario)
        For Each user As BE.Usuario In ListaUsuario
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro = user.Perfil
            user.Perfil = _gestorPermiso.leer_UnPermiso(_permisoFiltro)
            ls.Add(user)
        Next
        Return ls

    End Function

    Public Function leer_usuario(id As String) As BE.Usuario
        Dim user As BE.Usuario = gestor_usuario.leer_usuario(id)
        If user.idUsuario IsNot Nothing Then
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro = user.Perfil
            user.Perfil = _gestorPermiso.leer_UnPermiso(_permisoFiltro)
        End If
        Return user

    End Function

    Public Function leer_ususarioUsaPermiso(nombrePermiso As String) As Integer
        Return gestor_usuario.leer_usuarioUsaPermiso(nombrePermiso)
    End Function
    Public Function eliminar_usuario(user As String) As Boolean
        Dim i As Integer = gestor_usuario.eliminar_usuario(user)
        If i > 0 Then
            'Se escribio correctamente
            ActualizarSum()
            Return True
        End If
        Return False
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_usuario"
        midvv.tabla_DVV = recuperarSumUsuario()
        _gestorDVV.escribir_DVV(midvv)
    End Sub
    Public Function recuperarSumUsuario() As String
        Dim ls As List(Of BE.Usuario) = leer_usuario()
        sumauser = 0
        For Each user As BE.Usuario In ls
            sumauser = sumauser + 1
        Next
        Return sumaUser.ToString + "t_usuario"
    End Function
End Class
