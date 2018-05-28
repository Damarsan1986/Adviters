Imports Seguridad
Public Class gestorTIpoCambio
    Dim gestor_tipoCambio As New DAL.MapeadorTipoCambio
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_tipoCambio(tCambio As BE.TipoCambio) As String
        Dim mistring As String = tCambio.idMoneda.ToString + tCambio.precioCompra.ToString + tCambio.previoVenta.ToString + tCambio.usuarioUltMod.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        tCambio.tipoCambioDVH = miDVH

        Dim tCambioActual As BE.TipoCambio = leer_tipoCambio(tCambio.idMoneda)

        If tCambioActual.tipoCambioDVH = tCambio.tipoCambioDVH Then
            tCambioActual.usuarioUltMod = tCambio.usuarioUltMod
            actualizar_tipoCambio(tCambioActual)
        End If
        tCambio.fechaHoraUltMod = Now
        tCambio.fechaHoraVencimiento = "9999-12-31 23:59:59"
        Dim i As Integer = gestor_tipoCambio.insertar_tipoCambio(tCambio)
        If i > 0 Then
            ActualizarSum()
            Return "Se escribio correctamente"
        End If
        Return "No se pudo insertar"

    End Function


    Public Function leer_tipoCambio() As List(Of BE.TipoCambio)
        Return gestor_tipoCambio.leer_tipoCambio()

    End Function

    Public Function actualizar_tipoCambio(tCambio As BE.TipoCambio) As String
        tCambio.fechaHoraUltMod = Now

        Dim i As Integer = gestor_tipoCambio.actualizar_tipoCambio(tCambio)
        If i > 0 Then
            ActualizarSum()
            Return i
        End If
    End Function
    Public Function leer_tipoCambio(idmoneda As Integer) As BE.TipoCambio
        Return gestor_tipoCambio.leer_tipoCambio(idmoneda)
    End Function

    Public Function calcular_precio(monto As Double, idMoneda As Integer) As Double
        Dim tCambio As BE.TipoCambio = leer_tipoCambio(idMoneda)
        Dim precio As Double = monto * tCambio.previoVenta
        Return precio
    End Function

    Public Function calcular_puntos(monto As Double, idMoneda As Integer) As Double
        Dim tCambio As BE.TipoCambio = leer_tipoCambio(idMoneda)
        Dim precio As Double = monto / tCambio.precioCompra
        Return precio
    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_tCambio"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.TipoCambio) = leer_tipoCambio()
        sumaDVH = 0
        For Each registro As BE.TipoCambio In ls
            sumaDVH = sumaDVH + registro.idMoneda
        Next
        Return sumaDVH.ToString + "t_tCambio"
    End Function
End Class
