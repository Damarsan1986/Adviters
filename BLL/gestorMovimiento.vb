Imports Seguridad
Public Class gestorMovimiento
    Dim gestor_movimiento As New DAL.MapeadorMovimiento
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_movimiento(mov As BE.Movimiento) As String
        Dim mistring As String = mov.idComprobante.ToString + mov.idProducto.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.observaciones.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mov.movimientosDVH = miDVH

        Dim i As Integer = gestor_movimiento.insertar_movimientos(mov)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function

    Public Function actualizar_movimiento(mov As BE.Movimiento) As String
        Dim mistring As String = mov.idComprobante.ToString + mov.idProducto.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.observaciones.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mov.movimientosDVH = miDVH

        Dim i As Integer = gestor_movimiento.actualizar_movimientos(mov)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function
    Public Function leer_movimiento() As List(Of BE.Movimiento)
        Return gestor_movimiento.leer_movimiento()

    End Function
    Public Function calcular_stock(id As Integer) As Integer
        Return gestor_movimiento.leer_movimiento_Stock(id)
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_movimiento"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Movimiento) = leer_movimiento()
        sumaDVH = 0
        For Each registro As BE.Movimiento In ls
            sumaDVH = sumaDVH + registro.idComprobante
        Next
        Return sumaDVH.ToString + "t_movimiento"
    End Function
End Class
