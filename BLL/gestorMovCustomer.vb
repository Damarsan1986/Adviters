Imports Seguridad
Public Class gestorMovCustomer
    Dim gestor_mov_Customer As New DAL.MapeadorMovCustomer
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0

    Public Function insertar_mov_Customer(mov As BE.MovCustomer) As String
        Dim mistring As String = mov.idComprobante.ToString + mov.idCliente.ToString + mov.idCustomer.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.observaciones.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mov.movCustomerDVH = miDVH

        Dim i As Integer = gestor_mov_Customer.insertar_mov_Customer(mov)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function

    Public Function actualizar_mov_Customer(mov As BE.MovCustomer) As String
        Dim mistring As String = mov.idComprobante.ToString + mov.idCliente.ToString + mov.idCustomer.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.observaciones.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mov.movCustomerDVH = miDVH

        Dim i As Integer = gestor_mov_Customer.actualizar_mov_Customer(mov)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function

    Public Function leer_mov_Customer() As List(Of BE.MovCustomer)
        Return gestor_mov_Customer.leer_mov_Customer()

    End Function

    Public Function leer_mov_Customer(idCliente As String) As List(Of BE.MovCustomer)
        Return gestor_mov_Customer.leer_mov_Customer(idCliente)

    End Function

    Public Function leer_mov_Customer(idCliente As String, idCustomer As String) As List(Of BE.MovCustomer)
        Return gestor_mov_Customer.leer_mov_Customer(idCliente, idCustomer)

    End Function
    Public Function calcular_stock_Customer(idcliente As Integer, idcustomer As Integer) As Integer
        Return gestor_mov_Customer.leer_mov_Customer_Stock(idcliente, idcustomer)
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_movCustomer"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.MovCustomer) = leer_mov_Customer()
        sumaDVH = 0
        For Each registro As BE.MovCustomer In ls
            sumaDVH = sumaDVH + registro.idComprobante
        Next
        Return sumaDVH.ToString + "t_movCustomer"
    End Function
End Class
