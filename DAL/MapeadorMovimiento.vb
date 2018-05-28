Public Class MapeadorMovimiento

    Dim da As New Accesos
    Public Function insertar_movimientos(mov As BE.Movimiento) As Integer
        Dim hs As New Hashtable
        hs.Add("@Idproducto", mov.idProducto)
        hs.Add("@idComprobante", mov.idComprobante)
        hs.Add("@fechaHora", mov.fechaHora)
        hs.Add("@cantidad", mov.cantidad)
        hs.Add("@accion", mov.accion)
        hs.Add("@observaciones", mov.observaciones)
        hs.Add("@movimientosDVH", mov.movimientosDVH)

        Dim i As Integer = da.Escribir("movimiento_escribir", hs)
        Return i

    End Function

    Public Function actualizar_movimientos(mov As BE.Movimiento) As Integer
        Dim hs As New Hashtable
        hs.Add("@Idproducto", mov.idProducto)
        hs.Add("@idComprobante", mov.idComprobante)
        hs.Add("@fechaHora", mov.fechaHora)
        hs.Add("@movimientosDVH", mov.movimientosDVH)

        Dim i As Integer = da.Escribir("movimiento_actualizar", hs)
        Return i

    End Function

    Public Function leer_movimiento() As List(Of BE.Movimiento)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Movimiento)
        dt = da.Leer("movimiento_listar")

        For Each dr As DataRow In dt.Rows
            Dim mov As New BE.Movimiento
            mov.idProducto = dr("idProducto")
            mov.idComprobante = dr("idComprobante")
            mov.fechaHora = dr("fechaHora")
            mov.cantidad = dr("cantidad")
            mov.accion = dr("accion")
            mov.observaciones = dr("observaciones")
            mov.movimientosDVH = dr("movimientosDVH")

            ls.Add(mov)
        Next
        Return ls
    End Function

    Public Function leer_movimiento_Stock(id As String) As Integer
        Dim mov As New BE.Movimiento
        Dim hs As New Hashtable
        hs.Add("@Idproducto", id)
        Dim dt As DataTable = da.Leer("movimiento_leer_stock", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If IsDBNull(dr(0)) Then
                Return 0
            Else
                Return dr(0)
            End If
        Else
            Return 0

        End If
    End Function

End Class
