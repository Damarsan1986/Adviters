Imports Seguridad
Public Class GestorIntegridadBLL

    Dim _gestorMovimiento As gestorMovimiento = New gestorMovimiento
    Dim _gestorMoneda As gestorMoneda = New gestorMoneda
    Dim _gestorTipoCambio As gestorTIpoCambio = New gestorTIpoCambio
    Dim _gestorbitacora As GestorBitacora = New GestorBitacora
    Dim _gestorCliente As gestorCliente = New gestorCliente
    Dim _gestorComprobante As gestorComprobante = New gestorComprobante
    Dim _gestorConsumidor As gestorConsumidor = New gestorConsumidor
    Dim _gestorD_Comprobante As gestorD_Comprobante = New gestorD_Comprobante
    Dim _gestorMovCustomer As gestorMovCustomer = New gestorMovCustomer
    Dim _GestorMovEmpresa As gestorMovEmpresa = New gestorMovEmpresa
    Dim _GestorProducto As gestorProducto = New gestorProducto
    Dim _gestorProveedor As gestorProveedor = New gestorProveedor

    Dim _gestorDVV As GestorDVV = New GestorDVV

    Dim bitacora As New BE.bitacora
    Function validar() As Boolean
        'Función validar Integridad, chequea el DVV y los DVH de las trablas Usuario y Bitácora
        If validar_DVV() And
           recalcular_t_Cliente("validar") And
           recalcular_t_Comprobante("validar") And
           recalcular_t_Consumidor("validar") And
           recalcular_t_DComprobante("validar") And
           recalcular_t_Moneda("validar") And
           recalcular_t_MovCustomer("validar") And
           recalcular_t_MovEmpresa("validar") And
           recalcular_t_Movimiento("validar") And
           recalcular_t_Producto("validar") And
           recalcular_t_Proveedor("validar") And
           recalcular_t_tipoCambio("validar") Then

            Return True
        Else
            Return False
        End If

    End Function
    Public Function validar_DVV() As Boolean
        Dim DVV_Igual As Boolean = True
        Dim DVV_cliente As Boolean = True
        Dim DVV_comprobante As Boolean = True
        Dim DVV_consumidor As Boolean = True
        Dim DVV_dComprobante As Boolean = True
        Dim DVV_moneda As Boolean = True
        Dim DVV_movimiento As Boolean = True
        Dim DVV_movCustomer As Boolean = True
        Dim DVV_movEmpresa As Boolean = True
        Dim DVV_producto As Boolean = True
        Dim DVV_proveedor As Boolean = True
        Dim DVV_tCambio As Boolean = True

        Dim miDVV As New BE.DVV

        Dim mistringTXT As String = ""
        Dim mistringMD5 As String = ""

        '*****************************   DVV Cliente   *********************************
        miDVV = _gestorDVV.leer_DVV("t_cliente")
        mistringTXT = _gestorCliente.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_cliente = False
        registrar(DVV_cliente, "Cliente")

        '*****************************   DVV Comprobante   *********************************
        miDVV = _gestorDVV.leer_DVV("t_comprobante")
        mistringTXT = _gestorComprobante.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_comprobante = False
        registrar(DVV_comprobante, "Comprobante")

        '*****************************   DVV Consumidor   *********************************
        miDVV = _gestorDVV.leer_DVV("t_consumidor")
        mistringTXT = _gestorConsumidor.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_consumidor = False
        registrar(DVV_consumidor, "Consumidor")

        '*****************************   DVV D_Comprobante   *********************************
        miDVV = _gestorDVV.leer_DVV("t_dComprobante")
        mistringTXT = _gestorD_Comprobante.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_dComprobante = False
        registrar(DVV_dComprobante, "D_Comprobante")

        '*****************************   DVV Moneda   *********************************
        miDVV = _gestorDVV.leer_DVV("t_moneda")
        mistringTXT = _gestorMoneda.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_moneda = False
        registrar(DVV_moneda, "Moneda")

        '*****************************   DVV Mov Customer   *********************************
        miDVV = _gestorDVV.leer_DVV("t_movCustomer")
        mistringTXT = _gestorMovCustomer.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_movCustomer = False
        registrar(DVV_movCustomer, "Mov Customer")

        '*****************************   DVV Mov Empresa   *********************************
        miDVV = _gestorDVV.leer_DVV("t_movEmpresa")
        mistringTXT = _GestorMovEmpresa.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_movEmpresa = False
        registrar(DVV_movEmpresa, "Mov Empresa")

        '*****************************   DVV Movimiento   *********************************
        miDVV = _gestorDVV.leer_DVV("t_movimiento")
        mistringTXT = _gestorMovimiento.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_movimiento = False
        registrar(DVV_movimiento, "Movimiento")

        '*****************************   DVV Producto   *********************************
        miDVV = _gestorDVV.leer_DVV("t_producto")
        mistringTXT = _GestorProducto.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_producto = False
        registrar(DVV_producto, "Producto")

        '*****************************   DVV Proveedor   *********************************
        miDVV = _gestorDVV.leer_DVV("t_proveedor")
        mistringTXT = _gestorProveedor.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_proveedor = False
        registrar(DVV_proveedor, "Proveedor")

        '*****************************   DVV TipoCambio   *********************************
        miDVV = _gestorDVV.leer_DVV("t_tCambio")
        mistringTXT = _gestorTipoCambio.recuperarSumDVH()
        mistringMD5 = Criptografia.ObtenerInstancia().MD5(mistringTXT)
        If Not (miDVV.tabla_DVV = mistringMD5) Then DVV_tCambio = False
        registrar(DVV_tCambio, "TipoCambio")


        If DVV_cliente And DVV_comprobante And DVV_consumidor And DVV_dComprobante And DVV_moneda And DVV_movCustomer And DVV_movEmpresa And DVV_movimiento And DVV_producto And DVV_proveedor And DVV_tCambio Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Sub registrar(resultado As Boolean, tabla As String)
        bitacora.idUsuario = "Sistema"
        If resultado Then
            bitacora.descripcion = "Integridad DVV " + tabla + " OK"
        Else
            bitacora.descripcion = "Integridad DVV " + tabla + " Comprometida"
        End If
        _gestorbitacora.escribir_bitacora(bitacora)
    End Sub
    Public Sub recalcularDVV()
        _gestorCliente.ActualizarSum()
        _gestorComprobante.ActualizarSum()
        _gestorConsumidor.ActualizarSum()
        _gestorD_Comprobante.ActualizarSum()
        _gestorMoneda.ActualizarSum()
        _gestorMovCustomer.ActualizarSum()
        _GestorMovEmpresa.ActualizarSum()
        _gestorMovimiento.ActualizarSum()
        _GestorProducto.ActualizarSum()
        _gestorProveedor.ActualizarSum()
        _gestorTipoCambio.ActualizarSum()

    End Sub
    Public Function recalcular_t_Movimiento(funcion) As Boolean
        'Validación del DVH de la tabla movimiento
        Dim ls As List(Of BE.Movimiento) = _gestorMovimiento.leer_movimiento()

        Dim t_Movimiento_Integridad As Boolean = True
        Dim t_Movimiento_IntegridadCorregida As Boolean = False

        For Each mov As BE.Movimiento In ls
            Dim mistring As String = mov.idComprobante.ToString + mov.idProducto.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.observaciones.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (mov.movimientosDVH = miDVH) Then
                Select Case funcion
                    Case "validar"
                        t_Movimiento_Integridad = False
                        bitacora.descripcion = "Error DVH Movimiento Comprometida" & " clave: " & mov.idComprobante
                    Case "corregir"
                        mov.movimientosDVH = miDVH
                        _gestorMovimiento.actualizar_movimiento(mov)
                        bitacora.descripcion = "Registro DVH Movimiento Corregido" & " clave: " & mov.idComprobante
                        t_Movimiento_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Movimiento_Integridad Then
                    bitacora.descripcion = "Integridad DVH Movimiento OK"
                Else
                    bitacora.descripcion = "Integridad DVH Movimiento Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Movimiento_Integridad
            Case "corregir"
                Return t_Movimiento_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Moneda(funcion) As Boolean
        'Validación del DVH de la tabla moneda
        Dim ls As List(Of BE.Moneda) = _gestorMoneda.leer_moneda()

        Dim t_Moneda_Integridad As Boolean = True
        Dim t_Moneda_IntegridadCorregida As Boolean = False

        For Each moneda As BE.Moneda In ls
            Dim mistring As String = moneda.idMoneda.ToString + moneda.descripcionCorta.ToString + moneda.descripcionLarga.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (moneda.monedaDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Moneda_Integridad = False
                        bitacora.descripcion = "Error DVH Moneda Comprometida" & " clave: " & moneda.idMoneda
                    Case "corregir"
                        moneda.monedaDVH = miDVH
                        _gestorMoneda.insertar_Moneda(moneda)

                        bitacora.descripcion = "Registro DVH Moneda Corregido" & " clave: " & moneda.idMoneda

                        t_Moneda_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Moneda_Integridad Then
                    bitacora.descripcion = "Integridad DVH Moneda OK"
                Else
                    bitacora.descripcion = "Integridad DVH Moneda Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Moneda_Integridad
            Case "corregir"
                Return t_Moneda_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_tipoCambio(funcion) As Boolean
        'Validación del DVH de la tabla tipoCambio
        Dim ls As List(Of BE.TipoCambio) = _gestorTipoCambio.leer_tipoCambio()

        Dim t_TipoCambio_Integridad As Boolean = True
        Dim t_TipoCambio_IntegridadCorregida As Boolean = False

        For Each tCambio As BE.TipoCambio In ls
            Dim mistring As String = tCambio.idMoneda.ToString + tCambio.precioCompra.ToString + tCambio.previoVenta.ToString + tCambio.usuarioUltMod.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (tCambio.tipoCambioDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_TipoCambio_Integridad = False
                        bitacora.descripcion = "Error DVH TipoCambio Comprometida" & " clave: " & tCambio.idMoneda
                    Case "corregir"
                        tCambio.tipoCambioDVH = miDVH
                        _gestorTipoCambio.insertar_tipoCambio(tCambio)
                        bitacora.descripcion = "Registro DVH TipoCambio Corregido" & " clave: " & tCambio.idMoneda
                        t_TipoCambio_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_TipoCambio_Integridad Then
                    bitacora.descripcion = "Integridad DVH TipoCambio OK"
                Else
                    bitacora.descripcion = "Integridad DVH TipoCambio Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_TipoCambio_Integridad
            Case "corregir"
                Return t_TipoCambio_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Cliente(funcion) As Boolean
        'Validación del DVH de la tabla Cliente
        Dim ls As List(Of BE.Cliente) = _gestorCliente.leer_cliente()

        Dim t_Cliente_Integridad As Boolean = True
        Dim t_Cliente_IntegridadCorregida As Boolean = False

        For Each cli As BE.Cliente In ls
            Dim mistring As String = cli.idCliente.ToString + cli.razonSocial.ToString + cli.domicilio.ToString + cli.Email.ToString + cli.cuit.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (cli.clienteDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Cliente_Integridad = False
                        bitacora.descripcion = "Error DVH Cliente Comprometida" & " clave: " & cli.idCliente
                    Case "corregir"
                        cli.clienteDVH = miDVH
                        _gestorCliente.insertar_cliente(cli)
                        bitacora.descripcion = "Registro DVH Cliente Corregido" & " clave: " & cli.idCliente
                        t_Cliente_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Cliente_Integridad Then
                    bitacora.descripcion = "Integridad DVH Cliente OK"
                Else
                    bitacora.descripcion = "Integridad DVH Cliente Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Cliente_Integridad
            Case "corregir"
                Return t_Cliente_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Comprobante(funcion) As Boolean
        'Validación del DVH de la tabla Comprobante
        Dim ls As List(Of BE.Comprobante) = _gestorComprobante.leer_comprobante()

        Dim t_Comprobante_Integridad As Boolean = True
        Dim t_Comprobante_IntegridadCorregida As Boolean = False

        For Each comp As BE.Comprobante In ls
            Dim mistring As String = comp.idCliente.ToString + comp.idConsumidor.ToString + comp.idOperador.ToString + comp.monedaOperacion.ToString + comp.descOperacion.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (comp.comprobanteDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Comprobante_Integridad = False
                        bitacora.descripcion = "Error DVH Comprobante Comprometida" & " clave: " & comp.idComprobante
                    Case "corregir"
                        comp.comprobanteDVH = miDVH
                        _gestorComprobante.actualizar_comprobante(comp)
                        bitacora.descripcion = "Registro DVH Comprobante Corregido" & " clave: " & comp.idComprobante
                        t_Comprobante_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next
        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Comprobante_Integridad Then
                    bitacora.descripcion = "Integridad DVH Comprobante OK"
                Else
                    bitacora.descripcion = "Integridad DVH Comprobante Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Comprobante_Integridad
            Case "corregir"
                Return t_Comprobante_IntegridadCorregida
        End Select

    End Function
    Public Function recalcular_t_Consumidor(funcion) As Boolean
        'Validación del DVH de la tabla Consumidor
        Dim ls As List(Of BE.Consumidor) = _gestorConsumidor.leer_Consumidor()

        Dim t_Consumidor_Integridad As Boolean = True
        Dim t_Consumidor_IntegridadCorregida As Boolean = False

        For Each cons As BE.Consumidor In ls
            Dim mistring As String = cons.idCliente.ToString + cons.Nombre.ToString + cons.Apellido.ToString + cons.domicilio.ToString + cons.Email.ToString + cons.dni.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (cons.consumidorDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Consumidor_Integridad = False
                        bitacora.descripcion = "Error DVH Consumidor Comprometida" & " clave: " & cons.idConsumidor
                    Case "corregir"
                        cons.consumidorDVH = miDVH
                        _gestorConsumidor.insertar_Consumidor(cons)
                        bitacora.descripcion = "Registro DVH Consumidor Corregido" & " clave: " & cons.idConsumidor
                        t_Consumidor_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Consumidor_Integridad Then
                    bitacora.descripcion = "Integridad DVH Consumidor OK"
                Else
                    bitacora.descripcion = "Integridad DVH Consumidor Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Consumidor_Integridad
            Case "corregir"
                Return t_Consumidor_IntegridadCorregida
        End Select
    End Function
    Public Function recalcular_t_DComprobante(funcion) As Boolean
        'Validación del DVH de la tabla comprobante
        Dim ls As List(Of BE.D_Comprobante) = _gestorD_Comprobante.leer_D_Comprobante()

        Dim t_DComprobante_Integridad As Boolean = True
        Dim t_DComprobante_IntegridadCorregida As Boolean = False

        For Each Comp As BE.D_Comprobante In ls
            Dim mistring As String = Comp.idComprobante.ToString + Comp.idProducto.ToString + Comp.cantidad.ToString + Comp.pUnitario.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (Comp.dComprobanteDVH = miDVH) Then
                Select Case funcion
                    Case "validar"
                        t_DComprobante_Integridad = False
                        bitacora.descripcion = "Error DVH Detalle Comprobante Comprometida" & " clave: " & Comp.idComprobante & " - " & Comp.idD_Comprobante
                    Case "corregir"
                        Comp.dComprobanteDVH = miDVH
                        _gestorD_Comprobante.actualizar_D_Comprobante(Comp)
                        bitacora.descripcion = "Registro DVH Detalle Comprobante Corregido" & " clave: " & Comp.idComprobante & " - " & Comp.idD_Comprobante
                        t_DComprobante_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_DComprobante_Integridad Then
                    bitacora.descripcion = "Integridad DVH Detalle Comprobante  OK"
                Else
                    bitacora.descripcion = "Integridad DVH Detalle Comprobante  Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_DComprobante_Integridad
            Case "corregir"
                Return t_DComprobante_IntegridadCorregida
        End Select
    End Function
    Public Function recalcular_t_MovCustomer(funcion) As Boolean
        'Validación del DVH de la tabla MovCustomer
        Dim ls As List(Of BE.MovCustomer) = _gestorMovCustomer.leer_mov_Customer()

        Dim t_MovCustomer_Integridad As Boolean = True
        Dim t_MovCustomer_IntegridadCorregida As Boolean = False

        For Each mov As BE.MovCustomer In ls
            Dim mistring As String = mov.idComprobante.ToString + mov.idCliente.ToString + mov.idCustomer.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.observaciones.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (mov.movCustomerDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_MovCustomer_Integridad = False
                        bitacora.descripcion = "Error DVH Mov Customer Comprometida" & " clave: " & mov.idCustomer
                    Case "corregir"
                        mov.movCustomerDVH = miDVH
                        _gestorMovCustomer.actualizar_mov_Customer(mov)
                        bitacora.descripcion = "Registro DVH Mov Customer Corregido" & " clave: " & mov.idCustomer
                        t_MovCustomer_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_MovCustomer_Integridad Then
                    bitacora.descripcion = "Integridad DVH Mov Customer OK"
                Else
                    bitacora.descripcion = "Integridad DVH Mov Customer Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_MovCustomer_Integridad
            Case "corregir"
                Return t_MovCustomer_IntegridadCorregida
        End Select
    End Function
    Public Function recalcular_t_MovEmpresa(funcion) As Boolean
        'Validación del DVH de la tabla MovEmpresa
        Dim ls As List(Of BE.MovEmpresa) = _GestorMovEmpresa.leer_mov_empresa()

        Dim t_MovEmpresa_Integridad As Boolean = True
        Dim t_MovEmpresa_IntegridadCorregida As Boolean = False

        For Each mov As BE.MovEmpresa In ls
            Dim mistring As String = mov.idComprobante.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.idEmpresa.ToString + mov.observaciones.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (mov.movEmpresaDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_MovEmpresa_Integridad = False
                        bitacora.descripcion = "Error DVH Mov Empresa Comprometida" & " clave: " & mov.idEmpresa
                    Case "corregir"
                        mov.movEmpresaDVH = miDVH
                        _GestorMovEmpresa.actualizar_mov_empresa(mov)
                        bitacora.descripcion = "Registro DVH Mov Empresa Corregido" & " clave: " & mov.idEmpresa
                        t_MovEmpresa_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_MovEmpresa_Integridad Then
                    bitacora.descripcion = "Integridad DVH Mov Empresa  OK"
                Else
                    bitacora.descripcion = "Integridad DVH Mov Empresa  Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_MovEmpresa_Integridad
            Case "corregir"
                Return t_MovEmpresa_IntegridadCorregida
        End Select
    End Function
    Public Function recalcular_t_Producto(funcion) As Boolean
        'Validación del DVH de la tabla Producto
        Dim ls As List(Of BE.Producto) = _GestorProducto.leer_producto()

        Dim t_Producto_Integridad As Boolean = True
        Dim t_Producto_IntegridadCorregida As Boolean = False

        For Each prod As BE.Producto In ls
            Dim mistring As String = prod.idProducto.ToString + prod.tituloProducto.ToString + prod.tipoProducto.ToString + prod.Precio.ToString + prod.marca.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (prod.productoDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Producto_Integridad = False
                        bitacora.descripcion = "Error DVH Producto Comprometida" & " clave: " & prod.idProducto
                    Case "corregir"
                        prod.productoDVH = miDVH
                        _GestorProducto.insertar_producto(prod)
                        bitacora.descripcion = "Registro DVH Producto Corregido" & " clave: " & prod.idProducto
                        t_Producto_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Producto_Integridad Then
                    bitacora.descripcion = "Integridad DVH Producto OK"
                Else
                    bitacora.descripcion = "Integridad DVH Producto Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Producto_Integridad
            Case "corregir"
                Return t_Producto_IntegridadCorregida
        End Select
    End Function
    Public Function recalcular_t_Proveedor(funcion) As Boolean
        'Validación del DVH de la tabla Proveedor
        Dim ls As List(Of BE.Proveedor) = _gestorProveedor.leer_proveedor()

        Dim t_Proveedor_Integridad As Boolean = True
        Dim t_Proveedor_IntegridadCorregida As Boolean = False

        For Each prov As BE.Proveedor In ls
            Dim mistring As String = prov.idProveedor.ToString + prov.razonSocial.ToString + prov.domicilio.ToString + prov.Email.ToString + prov.cuit.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)

            If Not (prov.proveedorDVH = miDVH) Then
                Select funcion
                    Case "validar"
                        t_Proveedor_Integridad = False
                        bitacora.descripcion = "Error DVH Proveedor Comprometida" & " clave: " & prov.idProveedor
                    Case "corregir"
                        prov.proveedorDVH = miDVH
                        _gestorProveedor.insertar_proveedor(prov)
                        bitacora.descripcion = "Registro DVH Proveedor Corregido" & " clave: " & prov.idProveedor
                        t_Proveedor_IntegridadCorregida = True
                End Select
                bitacora.idUsuario = "Sistema"
                _gestorbitacora.escribir_bitacora(bitacora)
            End If
        Next

        Select Case funcion
            Case "validar"
                bitacora.idUsuario = "Sistema"
                If t_Proveedor_Integridad Then
                    bitacora.descripcion = "Integridad DVH Proveedor OK"
                Else
                    bitacora.descripcion = "Integridad DVH Proveedor Comprometida"
                End If
                _gestorbitacora.escribir_bitacora(bitacora)

                Return t_Proveedor_Integridad
            Case "corregir"
                Return t_Proveedor_IntegridadCorregida
        End Select
    End Function

End Class
