Public Class MapeadorMovEmpresa

    Dim da As New Accesos
    Public Function insertar_mov_empresa(mov As BE.MovEmpresa) As Integer
        Dim hs As New Hashtable
        hs.Add("@IdEmpresa", mov.idEmpresa)
        hs.Add("@idComprobante", mov.idComprobante)
        hs.Add("@fechaHora", mov.fechaHora)
        hs.Add("@cantidad", mov.cantidad)
        hs.Add("@accion", mov.accion)
        hs.Add("@observaciones", mov.observaciones)
        hs.Add("@movEmpresaDVH", mov.movEmpresaDVH)

        Dim i As Integer = da.Escribir("mov_empresa_escribir", hs)
        Return i

    End Function

    Public Function actualizar_mov_empresa(mov As BE.MovEmpresa) As Integer
        Dim hs As New Hashtable
        hs.Add("@IdEmpresa", mov.idEmpresa)
        hs.Add("@idComprobante", mov.idComprobante)
        hs.Add("@fechaHora", mov.fechaHora)
        hs.Add("@movEmpresaDVH", mov.movEmpresaDVH)

        Dim i As Integer = da.Escribir("mov_empresa_actualizar", hs)
        Return i

    End Function

    Public Function leer_mov_empresa() As List(Of BE.MovEmpresa)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.MovEmpresa)
        dt = da.Leer("mov_empresa_listar")

        For Each dr As DataRow In dt.Rows
            Dim mov As New BE.MovEmpresa
            mov.idEmpresa = dr("idEmpresa")
            mov.idComprobante = dr("idComprobante")
            mov.fechaHora = dr("fechaHora")
            mov.cantidad = dr("cantidad")
            mov.accion = dr("accion")
            mov.observaciones = dr("observaciones")
            mov.movEmpresaDVH = dr("movEmpresaDVH")

            ls.Add(mov)
        Next
        Return ls
    End Function

    Public Function leer_mov_empresa_Stock(id As String) As Integer
        Dim mov As New BE.MovEmpresa
        Dim hs As New Hashtable
        hs.Add("@IdEmpresa", id)
        Dim dt As DataTable = da.Leer("mov_empresa_leer_stock", hs)
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
