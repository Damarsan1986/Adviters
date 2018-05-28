Imports Seguridad
Public Class gestorCliente
    Dim gestor_cliente As New DAL.MapeadorCliente
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_cliente(cli As BE.Cliente) As String
        Dim mistring As String = cli.idCliente.ToString + cli.razonSocial.ToString + cli.domicilio.ToString + cli.Email.ToString + cli.cuit.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        cli.clienteDVH = miDVH

        Dim i As Integer = gestor_cliente.insertar_cliente(cli)
        If i > 0 Then
            'Se escribio correctamente

            ActualizarSum()
            Return "Se escribio correctamente"
        End If
        Return "No se pudo insertar"
    End Function


    Public Function leer_cliente() As List(Of BE.Cliente)
        Return gestor_cliente.leer_cliente()

    End Function

    Public Function leer_cliente(id As String) As BE.Cliente
        Return gestor_cliente.leer_cliente(id)

    End Function

    Public Function leer_cliente_CUIT(CUIT As String) As BE.Cliente
        Return gestor_cliente.leer_cliente_CUIT(CUIT)

    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_cliente"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Cliente) = leer_cliente()
        sumaDVH = 0
        For Each registro As BE.Cliente In ls
            sumaDVH = sumaDVH + registro.idCliente
        Next
        Return sumaDVH.ToString + "t_cliente"
    End Function

    Public Function eliminar_cliente(cliente As String) As String
        Dim i As Integer = gestor_cliente.eliminar_cliente(cliente)
        If i > 0 Then
            'Se escribio correctamente
            ActualizarSum()
            Return "cliente Eliminado"

        End If
        Return "No se pudo eliminar el cliente"
    End Function
End Class
