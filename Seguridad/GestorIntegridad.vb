Public Class GestorIntegridad

    Dim _gestorusuario As GestorUsuario = New GestorUsuario
    Dim _gestorbitacora As GestorBitacora = New GestorBitacora
    Dim _gestorMenu As GestorMenu = New GestorMenu
    Dim _gestorMensaje As GestorMensaje = New GestorMensaje
    Dim _gestorCultura As GestorCultura = New GestorCultura
    Dim _gestorPermiso As GestorPermiso = New GestorPermiso

    Dim _gestorDVV As GestorDVV = New GestorDVV
 
    Dim bitacora As New BE.bitacora
    Function validar() As Boolean
        'Función validar Integridad, chequea el DVV y los DVH de las trablas Usuario y Bitácora
        If validar_DVV() And
           recalcular_t_Usuario("validar") And
           recalcular_t_Bitacora("validar") And
           recalcular_t_Cultura("validar") And
           recalcular_t_Mensaje("validar") And
           recalcular_t_Menu("validar") And
           recalcular_t_PermisoPermiso("validar") And
           recalcular_t_Permisos("validar") Then

            Return True
        Else
            Return False
        End If

    End Function
    Public Function validar_DVV() As Boolean
        'Validación del DVV calculado a partir de los DVH de las tablas usuario y bitácora
        Dim DVV_Usuario As Boolean = True
        Dim DVV_Bitacora As Boolean = True
        Dim DVV_Cultura As Boolean = True
        Dim DVV_Mensaje As Boolean = True
        Dim DVV_Menu As Boolean = True
        Dim DVV_PermisoPermiso As Boolean = True
        Dim DVV_Permiso As Boolean = True
        Dim miDVV As New BE.DVV

        Dim mistringTXT As String = ""
        Dim mistringMD5 As String = ""

        '*****************************   DVV Bitacora   *********************************
        miDVV = _gestorDVV.leer_DVV("t_bitacora")
        mistringTXT = _gestorbitacora.recuperarSumBita()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_Bitacora = False
        registrar(DVV_Bitacora, "Bitacora")

        '*****************************   DVV Usuario   *********************************
        miDVV = _gestorDVV.leer_DVV("t_usuario")
        mistringTXT = _gestorusuario.recuperarSumUsuario()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_Usuario = False
        registrar(DVV_Usuario, "Usuario")

        '*****************************   DVV Cultura   *********************************
        miDVV = _gestorDVV.leer_DVV("t_cultura")
        mistringTXT = _gestorCultura.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_Cultura = False
        registrar(DVV_Cultura, "Cultura")

        '*****************************   DVV Mensaje   *********************************
        miDVV = _gestorDVV.leer_DVV("t_mensaje")
        mistringTXT = _gestorMensaje.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_Mensaje = False
        registrar(DVV_Mensaje, "Mensaje")

        '*****************************   DVV Menu   *********************************
        miDVV = _gestorDVV.leer_DVV("t_menu")
        mistringTXT = _gestorMenu.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_Menu = False
        registrar(DVV_Menu, "Menu")

        '**************************   DVV Permiso Permiso ******************************
        miDVV = _gestorDVV.leer_DVV("t_permiso_permiso")
        mistringTXT = _gestorPermiso.recuperarSumPPDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_PermisoPermiso = False
        registrar(DVV_PermisoPermiso, "Permiso_Permiso")

        '*****************************   DVV Permiso   *********************************
        miDVV = _gestorDVV.leer_DVV("t_permiso")
        mistringTXT = _gestorPermiso.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_Permiso = False
        registrar(DVV_Permiso, "Permiso")

        If DVV_Bitacora And DVV_Usuario And DVV_Cultura And DVV_Mensaje And DVV_Menu And DVV_Permiso And DVV_PermisoPermiso Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Sub recalcularDVV()
        _gestorPermiso.ActualizarSum()
        _gestorPermiso.ActualizarSumPP()
        _gestorCultura.ActualizarSum()
        _gestorMensaje.ActualizarSum()
        _gestorMenu.ActualizarSum()
        _gestorbitacora.ActualizarSum()
        _gestorusuario.ActualizarSum()
    End Sub

    Public Sub registrar(resultado As Boolean, tabla As String)
        bitacora.idUsuario = "Sistema"
        If resultado Then
            bitacora.descripcion = "Integridad DVV " + tabla + " OK"
        Else
            bitacora.descripcion = "Integridad DVV " + tabla + " Comprometida"
        End If
        _gestorbitacora.escribir_bitacora(bitacora)
    End Sub
    Public Function recalcular_t_Bitacora(funcion) As Boolean

        Dim ls As List(Of BE.bitacora) = _gestorbitacora.leer_bitacora()

        Dim t_Bitacora_IntegridadCorregida As Boolean = False
        Dim t_Bitacora_Integridad As Boolean = True

        For Each bita As BE.bitacora In ls
            Dim mistring As String = bita.idUsuario + bita.descripcion + bita.fechaHoraEvento.Year.ToString + bita.fechaHoraEvento.Day.ToString + bita.fechaHoraEvento.Hour.ToString("D2") + bita.fechaHoraEvento.Minute.ToString("D2")
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
            If Not (bita.dvhBitacora = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Bitacora_Integridad = False
                        bitacora.descripcion = "Error DVH Bitacora Comprometida" & " clave: " & bita.idBitacora
                    Case "corregir"
                        bita.dvhBitacora = miDVH
                        _gestorbitacora.actualizar_bitacora(bita)
                        bitacora.descripcion = "Registro DVH Bitacora Corregido" & " clave: " & bita.idBitacora
                        t_Bitacora_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Bitacora_Integridad Then
                    bitacora.descripcion = "Integridad DVH Bitacora OK"
                Else
                    bitacora.descripcion = "Integridad DVH Bitacora Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Bitacora_Integridad
            Case "corregir"
                Return t_Bitacora_IntegridadCorregida
        End Select



    End Function
    Public Function recalcular_t_Usuario(funcion) As Boolean
        'Validación del DVH de la tabla usuario
        Dim ls As List(Of BE.Usuario) = _gestorusuario.leer_usuario()

        Dim t_Usuario_Integridad As Boolean = True
        Dim t_Usuario_IntegridadCorregida As Boolean = False

        For Each user As BE.Usuario In ls
            Dim mistring As String = user.idUsuario.ToString + user.Clave.ToString + user.Perfil.Nombre.ToString + user.Email.ToString + user.IntentosFallidos.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (user.usuarioDVH = miDVH) Then
                Select Case funcion
                    Case "validar"
                        t_Usuario_Integridad = False
                        bitacora.descripcion = "Error DVH Usuario Comprometida" & " clave: " & user.idUsuario
                    Case "corregir"
                        user.usuarioDVH = miDVH
                        _gestorusuario.escribir_usuario(user)
                        bitacora.descripcion = "Registro DVH Usuario Corregido" & " clave: " & user.idUsuario
                        t_Usuario_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select funcion
            Case "validar"

                bitacora.idUsuario = "Sistema"
                If t_Usuario_Integridad Then
                    bitacora.descripcion = "Integridad DVH Usuario OK"
                Else
                    bitacora.descripcion = "Integridad DVH Usuario Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Usuario_Integridad

            Case "corregir"
                Return t_Usuario_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Menu(funcion) As Boolean
        'Validación del DVH de la tabla usuario
        Dim ls As List(Of BE.Menu) = _gestorMenu.leer_menu()
        Dim ls_Integridad As New List(Of BE.Menu)

        Dim t_Menu_Integridad As Boolean = True
        Dim t_Menu_IntegridadCorregida As Boolean = False

        For Each menu As BE.Menu In ls

            Dim mistring As String = menu.menuId.ToString + menu.parentMenuId.ToString + menu.descripcion.ToString + menu.titulo.ToString + menu.url.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (menu.menuDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Menu_Integridad = False
                        bitacora.descripcion = "Error DVH Menu Comprometida" & " clave: " & menu.menuId
                    Case "corregir"
                        menu.menuDVH = miDVH
                        _gestorMenu.escribir_menu(menu)
                        bitacora.descripcion = "Registro DVH Menu Corregido" & " clave: " & menu.menuId
                        t_Menu_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"

                bitacora.idUsuario = "Sistema"
                If t_Menu_Integridad Then
                    bitacora.descripcion = "Integridad DVH Menu OK"
                Else
                    bitacora.descripcion = "Integridad DVH Menu Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Menu_Integridad

            Case "corregir"
                Return t_Menu_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Mensaje(funcion) As Boolean
        'Validación del DVH de la tabla usuario
        Dim ls As List(Of BE.Mensaje) = _gestorMensaje.leer_mensaje()

        Dim t_Mensaje_Integridad As Boolean = True
        Dim t_Mensaje_IntegridadCorregida As Boolean = False

        For Each mensaje As BE.Mensaje In ls
            Dim mistring As String = mensaje.idMensaje.ToString + mensaje.descripcion.ToString + mensaje.Cultura.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (mensaje.mensajeDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Mensaje_Integridad = False
                        bitacora.descripcion = "Error DVH Mensaje Comprometida" & " clave: " & mensaje.idMensaje
                    Case "corregir"
                        mensaje.mensajeDVH = miDVH
                        _gestorMensaje.escribir_mensaje(mensaje)
                        bitacora.descripcion = "Registro DVH Mensaje Corregido" & " clave: " & mensaje.idMensaje
                        t_Mensaje_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"

                bitacora.idUsuario = "Sistema"
                If t_Mensaje_Integridad Then
                    bitacora.descripcion = "Integridad DVH Mensaje OK"
                Else
                    bitacora.descripcion = "Integridad DVH Mensaje Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Mensaje_Integridad

            Case "corregir"
                Return t_Mensaje_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Cultura(funcion) As Boolean
        'Validación del DVH de la tabla usuario
        Dim ls As List(Of BE.Cultura) = _gestorCultura.leer_cultura()
        Dim ls_Integridad As New List(Of BE.Cultura)

        Dim t_Cultura_Integridad As Boolean = True
        Dim t_Cultura_IntegridadCorregida As Boolean = False

        For Each cultura As BE.Cultura In ls
            Dim mistring As String = cultura.Descripcion.ToString + cultura.idCultura.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (cultura.culturaDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Cultura_Integridad = False
                        bitacora.descripcion = "Error DVH Cultura Comprometida" & " clave: " & cultura.idCultura.ToString
                    Case "corregir"
                        cultura.culturaDVH = miDVH
                        _gestorCultura.escribir_cultura(cultura)
                        bitacora.descripcion = "Registro DVH Cultura Corregido" & " clave: " & cultura.idCultura.ToString
                        t_Cultura_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"

                bitacora.idUsuario = "Sistema"
                If t_Cultura_Integridad Then
                    bitacora.descripcion = "Integridad DVH Cultura OK"
                Else
                    bitacora.descripcion = "Integridad DVH Cultura Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Cultura_Integridad

            Case "corregir"
                Return t_Cultura_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Permisos(funcion) As Boolean
        'Validación del DVH de la tabla usuario
        Dim ls As List(Of BE.PermisoBase) = _gestorPermiso.leer_permiso()
        Dim ls_Integridad As New List(Of BE.PermisoBase)

        Dim t_Permiso_Integridad As Boolean = True
        Dim t_Permiso_IntegridadCorregida As Boolean = False

        For Each perm As BE.PermisoBase In ls
            Dim mistring As String = perm.Nombre.ToString + perm.Descripcion.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (perm.permisoDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Permiso_Integridad = False
                        bitacora.descripcion = "Error DVH Permiso Comprometida" & " clave: " & perm.Nombre.ToString
                    Case "corregir"
                        perm.permisoDVH = miDVH
                        _gestorPermiso.escribir_permiso(perm)
                        bitacora.descripcion = "Registro DVH Permiso Corregido" & " clave: " & perm.Nombre.ToString
                        t_Permiso_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"

                bitacora.idUsuario = "Sistema"
                If t_Permiso_Integridad Then
                    bitacora.descripcion = "Integridad DVH Permiso OK"
                Else
                    bitacora.descripcion = "Integridad DVH Permiso Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Permiso_Integridad

            Case "corregir"
                Return t_Permiso_IntegridadCorregida
        End Select

    End Function


    Public Function recalcular_t_PermisoPermiso(funcion) As Boolean
        'Validación del DVH de la tabla PermisoPermiso
        Dim ls As List(Of BE.PermisoPermiso) = _gestorPermiso.leer_permiso_permiso()
        Dim ls_Integridad As New List(Of BE.PermisoPermiso)

        Dim t_Permiso_Integridad As Boolean = True
        Dim t_Permiso_IntegridadCorregida As Boolean = False

        For Each perm As BE.PermisoPermiso In ls
            Dim mistring As String = perm.codigoPadre.ToString + perm.codigoHijo.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (perm.permisoPermisoDVH = miDVH) Then
                Select Case funcion
                    Case "validar"
                        t_Permiso_Integridad = False
                        bitacora.descripcion = "Error DVH Permiso_Permiso Comprometida" & " clave: " & perm.codigoPadre.ToString & perm.codigoHijo.ToString
                    Case "corregir"
                        perm.permisoPermisoDVH = miDVH
                        _gestorPermiso.escribir_permiso_permiso(perm)
                        bitacora.descripcion = "Registro DVH Permiso_Permiso Corregido" & " clave: " & perm.codigoPadre.ToString & perm.codigoHijo.ToString
                        t_Permiso_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"

                bitacora.idUsuario = "Sistema"
                If t_Permiso_Integridad Then
                    bitacora.descripcion = "Integridad DVH Permiso_Permiso OK"
                Else
                    bitacora.descripcion = "Integridad DVH Permiso_Permiso Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Permiso_Integridad

            Case "corregir"
                Return t_Permiso_IntegridadCorregida
        End Select

    End Function

End Class
