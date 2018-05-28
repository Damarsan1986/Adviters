Public Class MapeadorTipoCambio

    Dim da As New Accesos
    Public Function insertar_tipoCambio(tCambio As BE.TipoCambio) As Integer
        Dim hs As New Hashtable
        hs.Add("@idMoneda", tCambio.idMoneda)
        hs.Add("@fechaHoraVencimiento", tCambio.fechaHoraVencimiento)
        hs.Add("@precioCompra", tCambio.precioCompra)
        hs.Add("@precioVenta", tCambio.previoVenta)
        hs.Add("@fechaHoraUltMod", tCambio.fechaHoraUltMod)
        hs.Add("@usuarioUltMod", tCambio.usuarioUltMod)
        hs.Add("@tipoCambioDVH", tCambio.tipoCambioDVH)

        Dim i As Integer = da.Escribir("tipoCambio_escribir", hs)
        Return i

    End Function
    Public Function actualizar_tipoCambio(tCambio As BE.TipoCambio) As Integer
        Dim hs As New Hashtable
        hs.Add("@idMoneda", tCambio.idMoneda)
        hs.Add("@fechaHoraVencimiento", tCambio.fechaHoraVencimiento)
        hs.Add("@precioCompra", tCambio.precioCompra)
        hs.Add("@precioVenta", tCambio.previoVenta)
        hs.Add("@fechaHoraUltMod", tCambio.fechaHoraUltMod)
        hs.Add("@usuarioUltMod", tCambio.usuarioUltMod)
        hs.Add("@tipoCambioDVH", tCambio.tipoCambioDVH)

        Dim i As Integer = da.Escribir("tipoCambio_actualizar", hs)
        Return i

    End Function

    Public Function leer_tipoCambio() As List(Of BE.TipoCambio)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.TipoCambio)
        dt = da.Leer("tipoCambio_listar")

        For Each dr As DataRow In dt.Rows
            Dim tCambio As New BE.TipoCambio
            tCambio.idMoneda = dr("idMoneda")
            tCambio.fechaHoraVencimiento = dr("fechaHoraVencimiento")
            tCambio.precioCompra = dr("precioCompra")
            tCambio.previoVenta = dr("precioVenta")
            tCambio.fechaHoraUltMod = dr("fechaHoraUltMod")
            tCambio.usuarioUltMod = dr("usuarioUltMod")
            tCambio.tipoCambioDVH = dr("tipoCambioDVH")
            ls.Add(tCambio)
        Next
        Return ls
    End Function

    Public Function leer_tipoCambio(idMoneda As Integer) As BE.TipoCambio
        Dim dt As New DataTable
        Dim hs As New Hashtable
        hs.Add("idMoneda", idMoneda)
        dt = da.Leer("tipoCambio_leer", hs)
        Dim tCambio As New BE.TipoCambio
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            tCambio.idMoneda = dr("idMoneda")
            tCambio.fechaHoraVencimiento = dr("fechaHoraVencimiento")
            tCambio.precioCompra = dr("precioCompra")
            tCambio.previoVenta = dr("precioVenta")
            tCambio.fechaHoraUltMod = dr("fechaHoraUltMod")
            tCambio.usuarioUltMod = dr("usuarioUltMod")
            tCambio.tipoCambioDVH = dr("tipoCambioDVH")
        End If

        Return tCambio
    End Function

End Class
