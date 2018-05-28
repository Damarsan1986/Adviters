Public Class MapeadorMoneda

    Dim da As New Accesos
    Public Function insertar_moneda(moneda As BE.Moneda) As Integer
        Dim hs As New Hashtable
        hs.Add("@idMoneda", moneda.idMoneda)
        hs.Add("@descripcionCorta", moneda.descripcionCorta)
        hs.Add("@descripcionLarga", moneda.descripcionLarga)
        hs.Add("@monedaDVH", moneda.monedaDVH)

        Dim i As Integer = da.Escribir("moneda_escribir", hs)
        Return i

    End Function

    Public Function leer_moneda() As List(Of BE.Moneda)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Moneda)
        dt = da.Leer("moneda_listar")

        For Each dr As DataRow In dt.Rows
            Dim moneda As New BE.Moneda
            moneda.idMoneda = dr("idMoneda")
            moneda.descripcionCorta = dr("descripcionCorta")
            moneda.descripcionLarga = dr("descripcionLarga")
            moneda.monedaDVH = dr("monedaDVH")

            ls.Add(moneda)
        Next
        Return ls
    End Function

    Public Function leer_moneda(idMoneda As Integer) As BE.Moneda
        Dim dt As New DataTable
        Dim hs As New Hashtable
        hs.Add("idMoneda", idMoneda)
        dt = da.Leer("moneda_leer", hs)
        Dim moneda As New BE.Moneda
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            moneda.idMoneda = dr("idMoneda")
            moneda.descripcionCorta = dr("descripcionCorta")
            moneda.descripcionLarga = dr("descripcionLarga")
            moneda.monedaDVH = dr("monedaDVH")
        End If

        Return moneda
    End Function

End Class
