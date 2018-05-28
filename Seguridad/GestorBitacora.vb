Public Class GestorBitacora

    Dim gestor_bitacora As New DAL.MapeadorBitacora
    Dim _gestorDVV As New GestorDVV
    Dim sumaidBitacora As Integer = 0

    'Función de Escritura en bitácora
    Public Function escribir_bitacora(bitacora As BE.bitacora) As Boolean
        'Preparo los datos a insertar en la tabla
        bitacora.fechaHoraEvento = Now
        Dim mistring As String = bitacora.idUsuario + bitacora.descripcion + bitacora.fechaHoraEvento.Year.ToString + bitacora.fechaHoraEvento.Day.ToString + bitacora.fechaHoraEvento.Hour.ToString("D2") + bitacora.fechaHoraEvento.Minute.ToString("D2")
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        bitacora.dvhBitacora = miDVH

        'llamo al metodo de escritura de la DAL
        Dim i As Integer = gestor_bitacora.escribir_bitacora(bitacora)

        'Verifico el resultado de la inserción, si es mayor a 0 la cantidad de registros impactados, entonces esta OK.
        If i > 0 Then
            ActualizarSum()
            Return True             'Se escribio correctamente
        End If
        'Si hubiera un error en el insert, displayo un mensaje de error
        ''MsgBox("ERROR BITACORA")
        Return False
    End Function

    Public Function leer_bitacora() As List(Of BE.bitacora)
        Return gestor_bitacora.leer_bitacora()
    End Function

    Public Function leer_bitacora(id As String) As List(Of BE.bitacora)
        Return gestor_bitacora.leer_bitacora(id)
    End Function

    Public Function leer_bitacora_fecha(fdesde As DateTime, fHasta As DateTime) As List(Of BE.bitacora)
        Return gestor_bitacora.leer_bitacora_fecha(fdesde, fHasta)
    End Function

    Public Function leer_bitacora_descripcion(descripcion As String) As List(Of BE.bitacora)
        Return gestor_bitacora.leer_bitacora_descripcion(descripcion)
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_bitacora"
        midvv.tabla_DVV = recuperarSumBita()
        _gestorDVV.escribir_DVV(midvv)
    End Sub
    Public Function recuperarSumBita() As String
        Dim ls As List(Of BE.bitacora) = leer_bitacora()
        sumaidBitacora = 0
        For Each bita As BE.bitacora In ls
            sumaidBitacora = sumaidBitacora + bita.idBitacora
        Next
        Return sumaidBitacora.ToString + "t_bitacora"
    End Function

    Public Function actualizar_bitacora(bitacora As BE.bitacora) As Boolean
        ' solo es utilizado si el AdminMaster decide recalcular los digitos horizontales de la tabla.
        'llamo al metodo de escritura de la DAL
        Dim i As Integer = gestor_bitacora.actualizar_bitacora(bitacora)

        'Verifico el resultado de la inserción, si es mayor a 0 la cantidad de registros impactados, entonces esta OK.
        If i > 0 Then
            Dim midvv As New BE.DVV
            midvv.idtabla = "t_bitacora"
            midvv.tabla_DVV = recuperarSumBita()
            _gestorDVV.escribir_DVV(midvv)

            Return True             'Se escribio correctamente
        End If
        'Si hubiera un error en el insert, displayo un mensaje de error
        'MsgBox("ERROR BITACORA")
        Return False
    End Function

    Public Function bitacora_migrar(fDesde As DateTime) As Boolean

        'llamo al metodo de escritura de la DAL
        Dim i As Integer = gestor_bitacora.migrar_bitacora(fDesde)

        'Verifico el resultado de la inserción, si es mayor a 0 la cantidad de registros impactados, entonces esta OK.
        If i > 0 Then
            Dim midvv As New BE.DVV
            midvv.idtabla = "t_bitacora"
            midvv.tabla_DVV = recuperarSumBita()
            _gestorDVV.escribir_DVV(midvv)

            Return True             'Se escribio correctamente
        End If
        'Si hubiera un error en el insert, displayo un mensaje de error
        'MsgBox("ERROR BITACORA")
        Return False
    End Function

    Public Function bitacora_desmigrar(fDesde As DateTime, fHasta As DateTime) As Boolean

        'llamo al metodo de escritura de la DAL
        Dim i As Integer = gestor_bitacora.desmigrar_bitacora(fDesde, fHasta)


        'Verifico el resultado de la inserción, si es mayor a 0 la cantidad de registros impactados, entonces esta OK.
        If i > 0 Then
            Dim midvv As New BE.DVV
            midvv.idtabla = "t_bitacora"
            midvv.tabla_DVV = recuperarSumBita()
            _gestorDVV.escribir_DVV(midvv)

            Return True             'Se escribio correctamente
        End If
        'Si hubiera un error en el insert, displayo un mensaje de error
        'MsgBox("ERROR BITACORA")
        Return False
    End Function

End Class
