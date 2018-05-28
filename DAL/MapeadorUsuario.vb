<Serializable()> _
Public Class MapeadorUsuario
    Dim da As New DAL.Accesos
    Dim _mapeadorPermiso = New MapeadorPermiso()
    Public Function escribir_usuario(user As BE.Usuario) As Integer
        Dim hs As New Hashtable

        hs.Add("@idUsuario", user.idUsuario)
        hs.Add("@nombre", user.Nombre)
        hs.Add("@apellido", user.apellido)
        hs.Add("@clave", user.Clave)
        hs.Add("@email", user.Email)
        hs.Add("@IntentosFallidos", user.IntentosFallidos)
        hs.Add("@bloqueado", user.Bloqueado)
        hs.Add("@cultura", user.Cultura.ToString)
        If Not user.Perfil Is Nothing Then
            hs.Add("@perfil", user.Perfil.Nombre.ToString)
        Else
            hs.Add("@perfil", "error-revisar")
        End If
        hs.Add("@usuarioDVH", user.usuarioDVH)
        Dim i As Integer = da.Escribir("usuario_escribir", hs)
        Return i
    End Function

    Public Function leer_usuario() As List(Of BE.Usuario)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Usuario)
        dt = da.Leer("usuario_listar")
        For Each dr As DataRow In dt.Rows
            Dim user As New BE.Usuario
            user.idUsuario = dr("idUsuario")
            user.Nombre = dr("nombre")
            user.apellido = dr("apellido")
            user.Clave = dr("clave")
            user.Email = dr("email")
            user.IntentosFallidos = dr("intentosFallidos")
            user.Bloqueado = dr("bloqueado")
            user.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            Dim perfil As String = Convert.ToString(dr("perfil"))
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro.Nombre = perfil
            user.Perfil = _permisoFiltro
            user.usuarioDVH = dr("usuarioDVH")

            ls.Add(user)
        Next
        Return ls
    End Function

    Public Function leer_usuario_Empresa(idCliente As String) As List(Of BE.Usuario)
        Dim hs As New Hashtable
        hs.Add("@idCliente", idCliente)
        Dim dt As DataTable = da.Leer("usuario_listar_Empresa", hs)
        Dim ls As New List(Of BE.Usuario)
        For Each dr As DataRow In dt.Rows
            Dim user As New BE.Usuario
            user.idUsuario = dr("idUsuario")
            user.Nombre = dr("nombre")
            user.apellido = dr("apellido")
            user.Clave = dr("clave")
            user.Email = dr("email")
            user.IntentosFallidos = dr("intentosFallidos")
            user.Bloqueado = dr("bloqueado")
            user.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            Dim perfil As String = Convert.ToString(dr("perfil"))
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro.Nombre = perfil
            user.Perfil = _permisoFiltro
            user.usuarioDVH = dr("usuarioDVH")

            ls.Add(user)
        Next
        Return ls
    End Function
    Public Function leer_usuario(id As String) As BE.Usuario
        Dim user As New BE.Usuario
        Dim hs As New Hashtable
        hs.Add("@idUsuario", id)
        Dim dt As DataTable = da.Leer("usuario_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            user.idUsuario = dr("idUsuario")
            user.Nombre = dr("nombre")
            user.apellido = dr("apellido")
            user.Clave = dr("clave")
            user.Email = dr("email")
            user.IntentosFallidos = dr("intentosFallidos")
            user.Bloqueado = dr("bloqueado")
            user.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            Dim perfil As String = Convert.ToString(dr("perfil"))
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro.Nombre = perfil
            user.Perfil = _permisoFiltro
            user.usuarioDVH = dr("usuarioDVH")
        End If
        Return user
    End Function

    Public Function leer_usuarioUsaPermiso(nombrePermiso As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@nombrePermiso", nombrePermiso)
        Dim dt As DataTable = da.Leer("usuario_leerPorPermiso", hs)

        Return dt.Rows.Count

    End Function

    Public Function eliminar_usuario(user As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@idUsuario", user)

        Dim i As Integer = da.Escribir("usuario_eliminar", hs)
        Return i
    End Function

End Class

