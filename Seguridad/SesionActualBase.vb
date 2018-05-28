<Serializable()> _
Public MustInherit Class SesionActualBase

    Dim _gestorUsuarios As GestorUsuario = New GestorUsuario()
    Dim _gestorbitacora As GestorBitacora = New GestorBitacora
    Dim _gestorCultura As GestorCultura = New GestorCultura()
    Dim _gestorPermiso As GestorPermiso = New GestorPermiso()
    Dim bitacora As New BE.bitacora
    Public Function Iniciar(ByVal usuario As BE.Usuario) As ResultadoAutenticacion

        Dim valido As ResultadoAutenticacion = ResultadoAutenticacion.UsuarioInvalido

        If Not usuario Is Nothing Then
            'buscar el usuario por su nombre
            Dim usuarioEncontrado As BE.Usuario = _gestorUsuarios.leer_usuario(usuario.idUsuario)
            If Not usuarioEncontrado.idUsuario Is Nothing Then
                'verificar que el usuario no este bloqueado
                If Not usuarioEncontrado.Bloqueado Then
                    'encriptar la clave ingresada por el usuario
                    Dim clavesinEncriptar As String = usuario.Clave
                    Dim claveEncriptada As String = Criptografia.ObtenerInstancia().MD5(usuario.Clave)
                    'verificar que las contraseñas encriptadas coinciden
                    If usuarioEncontrado.Clave.Equals(claveEncriptada) Then
                        valido = ResultadoAutenticacion.UsuarioValido

                        'cargar permisos
                        'blanquear intentos incorrectos y bloqueos
                        usuarioEncontrado.IntentosFallidos = 0
                        usuarioEncontrado.Bloqueado = False

                        'establecer el usuario logueado actualmente en sesion
                        Me.EstablecerUsuarioActual(usuarioEncontrado)

                        bitacora.idUsuario = usuarioEncontrado.idUsuario
                        bitacora.descripcion = "Login al Sistema"
                        _gestorbitacora.escribir_bitacora(bitacora)

                    Else
                        'contraseñas invalidas, incrementar el contado de intentos fallidos
                        usuarioEncontrado.IntentosFallidos += 1

                        bitacora.idUsuario = usuarioEncontrado.idUsuario
                        bitacora.descripcion = "Contraseña Inválida"
                        _gestorbitacora.escribir_bitacora(bitacora)

                        'verificar si los intentos fallidos es mayor o igual a 3
                        If usuarioEncontrado.IntentosFallidos >= 3 Then
                            usuarioEncontrado.Bloqueado = True
                            valido = ResultadoAutenticacion.UsuarioBloqueado

                            bitacora.idUsuario = usuarioEncontrado.idUsuario
                            bitacora.descripcion = "Usuario Bloqueado 3er intento"
                            _gestorbitacora.escribir_bitacora(bitacora)
                        End If
                    End If
                    'guardar los cambios del usuario
                    usuarioEncontrado.Clave = clavesinEncriptar
                    _gestorUsuarios.escribir_usuario(usuarioEncontrado)
                Else
                    valido = ResultadoAutenticacion.UsuarioBloqueado
                    bitacora.idUsuario = usuarioEncontrado.idUsuario
                    bitacora.descripcion = "Intento de Acceso Usuario Bloqueado"
                    _gestorbitacora.escribir_bitacora(bitacora)
                End If
            Else
                If (usuario.idUsuario = "adminMaster" And usuario.Clave = "Init753951!") Then
                    valido = ResultadoAutenticacion.UsuarioContingencia
                    usuario.Cultura = _gestorCultura.leer_cultura("es-AR").idCultura
                    Dim permiso As BE.PermisoFiltro = New BE.PermisoFiltro()
                    permiso.Nombre = "ADM MASTER"
                    usuario.Perfil = _gestorPermiso.leer_UnPermiso(permiso)
                    Me.EstablecerUsuarioActual(usuario)
                    bitacora.idUsuario = usuario.idUsuario
                    bitacora.descripcion = "Ingreso por Contingencia"
                    _gestorbitacora.escribir_bitacora(bitacora)

                Else
                    bitacora.idUsuario = usuario.idUsuario
                    bitacora.descripcion = "Intento de Acceso Usuario Inválido"
                    _gestorbitacora.escribir_bitacora(bitacora)
                End If
            End If
        End If
        Return valido
    End Function

    Public Function IniciarContingencia(ByVal usuario As BE.Usuario) As ResultadoAutenticacion

        Dim valido As ResultadoAutenticacion = ResultadoAutenticacion.UsuarioInvalido

        If Not usuario Is Nothing Then
            'establecer el usuario logueado actualmente en sesion
            Me.EstablecerUsuarioActual(usuario)
            Me.EstablecerCulturaActual(usuario)

            bitacora.idUsuario = usuario.idUsuario
            bitacora.descripcion = "Acceso por Contingencia"
            _gestorbitacora.escribir_bitacora(bitacora)
        End If

        Return valido
    End Function

    Public Sub Cerrar()

        bitacora.idUsuario = SesionActualWindows.SesionActual.ObtenerUsuarioActual.idUsuario.ToString
        bitacora.descripcion = "Cierre de Sesión"
        _gestorbitacora.escribir_bitacora(bitacora)
        Me.EstablecerUsuarioActual(Nothing)
    End Sub

    Public MustOverride Function ObtenerUsuarioActual() As BE.Usuario
    Public MustOverride Function ObtenerCulturaActual() As String
    Protected MustOverride Sub EstablecerUsuarioActual(usuario As BE.Usuario)
    Public MustOverride Sub EstablecerCulturaActual(usuario As BE.Usuario)
    Public Function TienePermisoPara(nombrePermiso As String) As Boolean
        Dim tienePermiso As Boolean = False
        Dim usuario = ObtenerUsuarioActual()
        If Not usuario Is Nothing Then
            If Not usuario.Perfil Is Nothing Then

                'tienePermiso = usuario.Perfil.EsValido()
                'DMS - FIX - revisar

                If usuario.Perfil.esAccion Then
                    Dim _GestorPermiso As New GestorPermisoSimple
                    tienePermiso = _GestorPermiso.EsValido(nombrePermiso)
                Else
                    Dim _GestorPermiso As New GestorPermisoCompuesto
                    tienePermiso = _GestorPermiso.EsValido(nombrePermiso)
                End If

            End If

        End If

            Return tienePermiso
    End Function

End Class
