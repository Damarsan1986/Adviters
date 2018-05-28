Public Class MapeadorComprobante

    Dim da As New Accesos
    Public Function insertar_Comprobante(Comp As BE.Comprobante) As Integer
        Dim hs As New Hashtable
        hs.Add("@idComprobante", Comp.idComprobante)
        hs.Add("@idCliente", Comp.idCliente)
        hs.Add("@idConsumidor", Comp.idConsumidor)
        hs.Add("@fechaHora", Comp.fechaHora)
        hs.Add("@descOperacion", Comp.descOperacion)
        hs.Add("@idOperador", Comp.idOperador)
        hs.Add("@monedaOperacion", Comp.monedaOperacion)
        hs.Add("@comprobanteDVH", Comp.comprobanteDVH)

        Dim i As Integer = da.Escribir("Comprobante_escribir", hs)
        Return i

    End Function

    Public Function actualizar_Comprobante(Comp As BE.Comprobante) As Integer
        Dim hs As New Hashtable
        hs.Add("@idComprobante", Comp.idComprobante)
        hs.Add("@comprobanteDVH", Comp.comprobanteDVH)

        Dim i As Integer = da.Escribir("Comprobante_actualizar", hs)
        Return i

    End Function

    Public Function leer_Comprobante() As List(Of BE.Comprobante)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Comprobante)
        dt = da.Leer("Comprobante_listar")

        For Each dr As DataRow In dt.Rows
            Dim Comp As New BE.Comprobante
            Comp.idComprobante = dr("idComprobante")
            Comp.idCliente = dr("idCliente")
            Comp.idConsumidor = dr("idConsumidor")
            Comp.fechaHora = dr("fechaHora")
            Comp.descOperacion = dr("descOperacion")
            Comp.idOperador = dr("idOperador")
            Comp.monedaOperacion = dr("monedaOperacion")
            Comp.comprobanteDVH = dr("comprobanteDVH")

            ls.Add(Comp)
        Next
        Return ls
    End Function

    Public Function leer_Comprobante(idComprobante As Integer) As BE.Comprobante
        Dim Comp As New BE.Comprobante
        Dim hs As New Hashtable
        hs.Add("@idComprobante", idComprobante)
        Dim dt As DataTable = da.Leer("Comprobante_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Comp.idComprobante = dr("idComprobante")
            Comp.idCliente = dr("idCliente")
            Comp.idConsumidor = dr("idConsumidor")
            Comp.fechaHora = dr("fechaHora")
            Comp.descOperacion = dr("descOperacion")
            Comp.idOperador = dr("idOperador")
            Comp.monedaOperacion = dr("monedaOperacion")
            Comp.comprobanteDVH = dr("comprobanteDVH")
        End If
        Return Comp
    End Function

    Public Function leer_Comprobante(comp As BE.Comprobante) As BE.Comprobante
        Dim hs As New Hashtable
        hs.Add("@fechaHora", comp.fechaHora)
        hs.Add("@idOperador", comp.idOperador)
        hs.Add("@idCliente", comp.idCliente)
        Dim dt As DataTable = da.Leer("Comprobante_leer_idComprobante", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            comp.idComprobante = dr("idComprobante")
            comp.idCliente = dr("idCliente")
            comp.idConsumidor = dr("idConsumidor")
            comp.fechaHora = dr("fechaHora")
            comp.descOperacion = dr("descOperacion")
            comp.idOperador = dr("idOperador")
            comp.monedaOperacion = dr("monedaOperacion")
            comp.comprobanteDVH = dr("comprobanteDVH")
        End If
        Return comp
    End Function

End Class
