Imports Seguridad
Public Class gestorProducto
    Dim gestor_producto As New DAL.MapeadorProducto
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_producto(prod As BE.Producto) As String
        Dim mistring As String = prod.idProducto.ToString + prod.tituloProducto.ToString + prod.tipoProducto.ToString + prod.Precio.ToString + prod.marca.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        prod.productoDVH = miDVH

        Dim i As Integer = gestor_producto.insertar_producto(prod)
        If i > 0 Then
            ActualizarSum()
            Return "Se escribio correctamente"
        End If
        Return "No se pudo insertar"
    End Function


    Public Function leer_producto() As List(Of BE.Producto)
        Return gestor_producto.leer_producto()

    End Function

    Public Function leer_producto(id As Integer) As BE.Producto
        Return gestor_producto.leer_producto(id)

    End Function

    Public Function eliminar_producto(producto As String) As String
        Dim i As Integer = gestor_producto.eliminar_producto(producto)
        If i > 0 Then
            ActualizarSum()
            Return "Producto Eliminado"
        End If
        Return "No se pudo eliminar el Producto"
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_producto"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Producto) = leer_producto()
        sumaDVH = 0
        For Each registro As BE.Producto In ls
            sumaDVH = sumaDVH + registro.idProducto
        Next
        Return sumaDVH.ToString + "t_producto"
    End Function

End Class
