Public Class MapeadorD_Comprobante

    Dim da As New Accesos
    Public Function insertar_D_Comprobante(DCom As BE.D_Comprobante) As Integer
        Dim hs As New Hashtable
        hs.Add("@idComprobante", DCom.idComprobante)
        hs.Add("@idD_Comprobante", DCom.idD_Comprobante)
        hs.Add("@idProducto", DCom.idProducto)
        hs.Add("@cantidad", DCom.Cantidad)
        hs.Add("@pUnitario", DCom.pUnitario)
        hs.Add("@dComprobanteDVH", DCom.dComprobanteDVH)

        Dim i As Integer = da.Escribir("D_Comprobante_escribir", hs)
        Return i

    End Function
    Public Function actualizar_D_Comprobante(DCom As BE.D_Comprobante) As Integer
        Dim hs As New Hashtable
        hs.Add("@idComprobante", DCom.idComprobante)
        hs.Add("@idD_Comprobante", DCom.idD_Comprobante)
        hs.Add("@dComprobanteDVH", DCom.dComprobanteDVH)

        Dim i As Integer = da.Escribir("D_Comprobante_actualizar", hs)
        Return i

    End Function
    Public Function leer_D_Comprobante() As List(Of BE.D_Comprobante)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.D_Comprobante)
        dt = da.Leer("D_Comprobante_listar")

        For Each dr As DataRow In dt.Rows
            Dim DCom As New BE.D_Comprobante
            DCom.idComprobante = dr("idComprobante")
            DCom.idD_Comprobante = dr("idD_Comprobante")
            DCom.idProducto = dr("idProducto")
            DCom.cantidad = dr("Cantidad")
            DCom.pUnitario = dr("pUnitario")
            DCom.dComprobanteDVH = dr("dComprobanteDVH")

            ls.Add(DCom)
        Next
        Return ls
    End Function

    Public Function leer_D_Comprobante(idComprobante As Integer, idD_Comprobante As Integer) As BE.D_Comprobante
        Dim DCom As New BE.D_Comprobante
        Dim hs As New Hashtable
        hs.Add("@idComprobante", idComprobante)
        hs.Add("@idD_Comprobante", idD_Comprobante)
        Dim dt As DataTable = da.Leer("D_Comprobante_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            DCom.idComprobante = dr("idComprobante")
            DCom.idD_Comprobante = dr("idD_Comprobante")
            DCom.idProducto = dr("idProducto")
            DCom.cantidad = dr("Cantidad")
            DCom.pUnitario = dr("pUnitario")
            DCom.dComprobanteDVH = dr("dComprobanteDVH")
        End If
        Return DCom
    End Function
    Public Function leer_D_Comprobante(idComprobante As Integer) As List(Of BE.D_Comprobante)
        Dim hs As New Hashtable
        Dim ls As New List(Of BE.D_Comprobante)
        hs.Add("@idComprobante", idComprobante)
        Dim dt As DataTable = da.Leer("D_Comprobante_leerPorComp", hs)
        For Each dr As DataRow In dt.Rows
            Dim DCom As New BE.D_Comprobante
            DCom.idComprobante = dr("idComprobante")
            DCom.idD_Comprobante = dr("idD_Comprobante")
            DCom.idProducto = dr("idProducto")
            DCom.cantidad = dr("Cantidad")
            DCom.pUnitario = dr("pUnitario")
            DCom.dComprobanteDVH = dr("dComprobanteDVH")

            ls.Add(DCom)
        Next
        Return ls
    End Function

End Class
