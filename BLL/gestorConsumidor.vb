Imports Seguridad
Public Class gestorConsumidor
    Dim gestor_Consumidor As New DAL.MapeadorConsumidor
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_Consumidor(cons As BE.Consumidor) As String
        Dim mistring As String = cons.idCliente.ToString + cons.Nombre.ToString + cons.Apellido.ToString + cons.domicilio.ToString + cons.Email.ToString + cons.dni.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        cons.consumidorDVH = miDVH

        Dim i As Integer = gestor_Consumidor.insertar_Consumidor(cons)
        If i > 0 Then
            ActualizarSum()
            Return "Se escribio correctamente"
        End If
        Return "No se pudo insertar"
    End Function


    Public Function leer_Consumidor() As List(Of BE.Consumidor)
        Return gestor_Consumidor.leer_Consumidor()

    End Function

    Public Function leer_Consumidor(idcliente As Integer) As List(Of BE.Consumidor)
        Return gestor_Consumidor.leer_Consumidor(idcliente)

    End Function

    Public Function leer_Consumidor(idcliente As Integer, idConsumidor As Integer) As BE.Consumidor
        Return gestor_Consumidor.leer_Consumidor(idcliente, idConsumidor)

    End Function

    Public Function leer_Consumidor_DNI(DNI As String) As BE.Consumidor
        Return gestor_Consumidor.leer_Consumidor_DNI(DNI)

    End Function
    Public Function eliminar_Consumidor(idcliente As String, Consumidor As String) As String
        Dim i As Integer = gestor_Consumidor.eliminar_Consumidor(idcliente, Consumidor)
        If i > 0 Then
            ActualizarSum()
            Return "Consumidor Eliminado"
        End If
        Return "No se pudo eliminar el Consumidor"
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_consumidor"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Consumidor) = leer_Consumidor()
        sumaDVH = 0
        For Each registro As BE.Consumidor In ls
            sumaDVH = sumaDVH + registro.idConsumidor
        Next
        Return sumaDVH.ToString + "t_consumidor"
    End Function
End Class
