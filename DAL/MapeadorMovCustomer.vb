Public Class MapeadorMovCustomer

    Dim da As New Accesos
    Public Function insertar_mov_Customer(mov As BE.MovCustomer) As Integer
        Dim hs As New Hashtable
        hs.Add("@IdCliente", mov.idCliente)
        hs.Add("@IdCustomer", mov.idCustomer)
        hs.Add("@idComprobante", mov.idComprobante)
        hs.Add("@fechaHora", mov.fechaHora)
        hs.Add("@cantidad", mov.cantidad)
        hs.Add("@accion", mov.accion)
        hs.Add("@observaciones", mov.observaciones)
        hs.Add("@movCustomerDVH", mov.movCustomerDVH)

        Dim i As Integer = da.Escribir("mov_Customer_escribir", hs)
        Return i

    End Function

    Public Function actualizar_mov_Customer(mov As BE.MovCustomer) As Integer
        Dim hs As New Hashtable
        hs.Add("@IdCliente", mov.idCliente)
        hs.Add("@IdCustomer", mov.idCustomer)
        hs.Add("@idComprobante", mov.idComprobante)
        hs.Add("@fechaHora", mov.fechaHora)
        hs.Add("@movCustomerDVH", mov.movCustomerDVH)

        Dim i As Integer = da.Escribir("mov_customer_actualizar", hs)
        Return i

    End Function

    Public Function leer_mov_Customer() As List(Of BE.MovCustomer)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.MovCustomer)
        dt = da.Leer("mov_Customer_listar")

        For Each dr As DataRow In dt.Rows
            Dim mov As New BE.MovCustomer
            mov.idCliente = dr("idCliente")
            mov.idCustomer = dr("idCustomer")
            mov.idComprobante = dr("idComprobante")
            mov.fechaHora = dr("fechaHora")
            mov.cantidad = dr("cantidad")
            mov.accion = dr("accion")
            mov.observaciones = dr("observaciones")
            mov.movCustomerDVH = dr("movCustomerDVH")

            ls.Add(mov)
        Next
        Return ls
    End Function

    Public Function leer_mov_Customer(idCliente As String) As List(Of BE.MovCustomer)

        Dim hs As New Hashtable
        hs.Add("@idCliente", idCliente)
        Dim dt As DataTable = da.Leer("mov_Customer_Listar_Cliente", hs)
        Dim ls As New List(Of BE.MovCustomer)
        
        For Each dr As DataRow In dt.Rows
            Dim mov As New BE.MovCustomer
            mov.idCliente = dr("idCliente")
            mov.idCustomer = dr("idCustomer")
            mov.idComprobante = dr("idComprobante")
            mov.fechaHora = dr("fechaHora")
            mov.cantidad = dr("cantidad")
            mov.accion = dr("accion")
            mov.observaciones = dr("observaciones")
            mov.movCustomerDVH = dr("movCustomerDVH")

            ls.Add(mov)
        Next
        Return ls
    End Function

    Public Function leer_mov_Customer(idCliente As String, idCustomer As String) As List(Of BE.MovCustomer)

        Dim hs As New Hashtable
        hs.Add("@idCliente", idCliente)
        hs.Add("@idCustomer", idCustomer)
        Dim dt As DataTable = da.Leer("mov_Customer_Listar_customer", hs)
        Dim ls As New List(Of BE.MovCustomer)

        For Each dr As DataRow In dt.Rows
            Dim mov As New BE.MovCustomer
            mov.idCliente = dr("idCliente")
            mov.idCustomer = dr("idCustomer")
            mov.idComprobante = dr("idComprobante")
            mov.fechaHora = dr("fechaHora")
            mov.cantidad = dr("cantidad")
            mov.accion = dr("accion")
            mov.observaciones = dr("observaciones")
            mov.movCustomerDVH = dr("movCustomerDVH")

            ls.Add(mov)
        Next
        Return ls
    End Function

    Public Function leer_mov_Customer_Stock(idcliente As Integer, idcustomer As Integer) As Integer
        Dim mov As New BE.MovCustomer
        Dim hs As New Hashtable
        hs.Add("@idcliente", idcliente)
        hs.Add("@IdCustomer", idcustomer)
        Dim dt As DataTable = da.Leer("mov_Customer_leer_stock", hs)
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
