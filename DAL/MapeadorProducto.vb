Public Class MapeadorProducto

    Dim da As New Accesos
    Public Function insertar_producto(prod As BE.Producto) As Integer
        Dim hs As New Hashtable
        hs.Add("@Idproducto", prod.idProducto)
        hs.Add("@tituloProducto", prod.tituloProducto)
        hs.Add("@descripcion", prod.Descripcion)
        hs.Add("@tipoProducto", prod.tipoProducto)
        hs.Add("@marca", prod.marca)
        hs.Add("@picture", prod.picture)
        hs.Add("@categoria", prod.categoria)
        hs.Add("@precio", prod.Precio)
        hs.Add("@stockMaximo", prod.StockMaximo)
        hs.Add("@stockMinimo", prod.stockMinimo)
        hs.Add("@productoDVH", prod.productoDVH)

        Dim i As Integer = da.Escribir("producto_escribir", hs)
        Return i

    End Function

    Public Function leer_producto() As List(Of BE.Producto)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Producto)
        dt = da.Leer("producto_listar")

        For Each dr As DataRow In dt.Rows
            Dim prod As New BE.Producto
            prod.idProducto = dr("idProducto")
            prod.tituloProducto = dr("tituloProducto")
            prod.Descripcion = dr("descripcion")
            prod.tipoProducto = dr("tipoProducto")
            prod.marca = dr("marca")
            prod.picture = dr("picture")
            prod.categoria = dr("categoria")
            prod.Precio = dr("precio")
            prod.StockMaximo = dr("StockMaximo")
            prod.stockMinimo = dr("stockMinimo")
            prod.productoDVH = dr("productoDVH")

            ls.Add(prod)
        Next
        Return ls
    End Function

    Public Function leer_producto(id As String) As BE.Producto
        Dim prod As New BE.Producto
        Dim hs As New Hashtable
        hs.Add("@Idproducto", id)
        Dim dt As DataTable = da.Leer("producto_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            prod.idProducto = dr("idProducto")
            prod.tituloProducto = dr("tituloProducto")
            prod.Descripcion = dr("descripcion")
            prod.tipoProducto = dr("tipoProducto")
            prod.marca = dr("marca")
            prod.picture = dr("picture")
            prod.categoria = dr("categoria")
            prod.Precio = dr("precio")
            prod.StockMaximo = dr("StockMaximo")
            prod.stockMinimo = dr("stockMinimo")
            prod.productoDVH = dr("productoDVH")
        End If
        Return prod
    End Function

    Public Function eliminar_producto(producto As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@idProducto", producto)
        Dim i As Integer = da.Escribir("producto_eliminar", hs)
        Return i
    End Function

End Class
