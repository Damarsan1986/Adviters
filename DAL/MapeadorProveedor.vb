Public Class MapeadorProveedor

    Dim da As New Accesos
    Public Function insertar_proveedor(prov As BE.Proveedor) As Integer
        Dim hs As New Hashtable
        hs.Add("@Idproveedor", prov.idproveedor)
        hs.Add("@razonSocial", prov.razonSocial)
        hs.Add("@cuit", prov.cuit)
        hs.Add("@email", prov.Email)
        hs.Add("@domicilio", prov.domicilio)
        hs.Add("@localidad", prov.localidad)
        hs.Add("@provincia", prov.provincia)
        hs.Add("@pais", prov.pais)
        hs.Add("@CP", prov.CP)
        hs.Add("@SFI", prov.SFI)
        hs.Add("@fechaAlta", prov.fechaAlta)
        hs.Add("@proveedorDVH", prov.proveedorDVH)

        Dim i As Integer = da.Escribir("proveedor_escribir", hs)
        Return i

    End Function

    Public Function leer_proveedor() As List(Of BE.Proveedor)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Proveedor)
        dt = da.Leer("proveedor_listar")

        For Each dr As DataRow In dt.Rows
            Dim prov As New BE.Proveedor
            prov.idproveedor = dr("idproveedor")
            prov.razonSocial = dr("razonSocial")
            prov.cuit = dr("cuit")
            prov.Email = dr("email")
            prov.domicilio = dr("domicilio")
            prov.localidad = dr("localidad")
            prov.provincia = dr("provincia")
            prov.pais = dr("pais")
            prov.CP = dr("CP")
            prov.SFI = dr("SFI")
            prov.fechaAlta = dr("fechaAlta")
            prov.proveedorDVH = dr("proveedorDVH")

            ls.Add(prov)
        Next
        Return ls
    End Function

    Public Function leer_proveedor(id As String) As BE.Proveedor
        Dim prov As New BE.Proveedor
        Dim hs As New Hashtable
        hs.Add("@Idproveedor", id)
        Dim dt As DataTable = da.Leer("proveedor_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            prov.idProveedor = dr("idproveedor")
            prov.razonSocial = dr("razonSocial")
            prov.cuit = dr("cuit")
            prov.Email = dr("email")
            prov.domicilio = dr("domicilio")
            prov.localidad = dr("localidad")
            prov.provincia = dr("provincia")
            prov.pais = dr("pais")
            prov.CP = dr("CP")
            prov.SFI = dr("SFI")
            prov.fechaAlta = dr("fechaAlta")
            prov.proveedorDVH = dr("proveedorDVH")
        End If
        Return prov
    End Function

    Public Function eliminar_proveedor(proveedor As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@idproveedor", proveedor)
        Dim i As Integer = da.Escribir("proveedor_eliminar", hs)
        Return i
    End Function

End Class
