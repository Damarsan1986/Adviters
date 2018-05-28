Imports Seguridad
Public Class gestorProveedor
    Dim gestor_proveedor As New DAL.MapeadorProveedor
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0

    Public Function insertar_proveedor(prov As BE.Proveedor) As String
        Dim mistring As String = prov.idProveedor.ToString + prov.razonSocial.ToString + prov.domicilio.ToString + prov.Email.ToString + prov.cuit.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        prov.proveedorDVH = miDVH

        Dim i As Integer = gestor_proveedor.insertar_proveedor(prov)
        If i > 0 Then
            ActualizarSum()
            Return "Se escribio correctamente"
        End If
        Return "No se pudo insertar"
    End Function


    Public Function leer_proveedor() As List(Of BE.Proveedor)
        Return gestor_proveedor.leer_proveedor()

    End Function

    Public Function leer_proveedor(id As Integer) As BE.Proveedor
        Return gestor_proveedor.leer_proveedor(id)

    End Function

    Public Function eliminar_proveedor(proveedor As String) As String
        Dim i As Integer = gestor_proveedor.eliminar_proveedor(proveedor)
        If i > 0 Then
            ActualizarSum()
            Return "proveedor Eliminado"
        End If
        Return "No se pudo eliminar el proveedor"
    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_proveedor"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Proveedor) = leer_proveedor()
        sumaDVH = 0
        For Each registro As BE.Proveedor In ls
            sumaDVH = sumaDVH + registro.idProveedor
        Next
        Return sumaDVH.ToString + "t_proveedor"
    End Function
End Class
