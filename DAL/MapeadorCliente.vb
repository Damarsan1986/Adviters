Public Class MapeadorCliente

    Dim da As New Accesos
    Public Function insertar_cliente(cli As BE.Cliente) As Integer
        Dim hs As New Hashtable
        hs.Add("@Idcliente", cli.idcliente)
        hs.Add("@razonSocial", cli.razonSocial)
        hs.Add("@cuit", cli.cuit)
        hs.Add("@email", cli.Email)
        hs.Add("@domicilio", cli.domicilio)
        hs.Add("@localidad", cli.localidad)
        hs.Add("@provincia", cli.provincia)
        hs.Add("@pais", cli.pais)
        hs.Add("@CP", cli.CP)
        hs.Add("@SFI", cli.SFI)
        hs.Add("@fechaAlta", cli.fechaAlta)
        hs.Add("@clienteDVH", cli.clienteDVH)

        Dim i As Integer = da.Escribir("cliente_escribir", hs)
        Return i

    End Function

    Public Function leer_cliente() As List(Of BE.Cliente)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Cliente)
        dt = da.Leer("cliente_listar")

        For Each dr As DataRow In dt.Rows
            Dim cli As New BE.Cliente
            cli.idcliente = dr("idcliente")
            cli.razonSocial = dr("razonSocial")
            cli.cuit = dr("cuit")
            cli.Email = dr("email")
            cli.domicilio = dr("domicilio")
            cli.localidad = dr("localidad")
            cli.provincia = dr("provincia")
            cli.pais = dr("pais")
            cli.CP = dr("CP")
            cli.SFI = dr("SFI")
            cli.fechaAlta = dr("fechaAlta")
            cli.clienteDVH = dr("clienteDVH")
            ls.Add(cli)
        Next
        Return ls
    End Function

    Public Function leer_cliente(id As String) As BE.Cliente
        Dim cli As New BE.Cliente
        Dim hs As New Hashtable
        hs.Add("@Idcliente", id)
        Dim dt As DataTable = da.Leer("cliente_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            cli.idcliente = dr("idcliente")
            cli.razonSocial = dr("razonSocial")
            cli.cuit = dr("cuit")
            cli.Email = dr("email")
            cli.domicilio = dr("domicilio")
            cli.localidad = dr("localidad")
            cli.provincia = dr("provincia")
            cli.pais = dr("pais")
            cli.CP = dr("CP")
            cli.SFI = dr("SFI")
            cli.fechaAlta = dr("fechaAlta")
            cli.clienteDVH = dr("clienteDVH")
        End If
        Return cli
    End Function

    Public Function leer_cliente_CUIT(CUIT As String) As BE.Cliente
        Dim cli As New BE.Cliente
        Dim hs As New Hashtable
        hs.Add("@CUIT", CUIT)
        Dim dt As DataTable = da.Leer("cliente_CUIT_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            cli.idCliente = dr("idcliente")
            cli.razonSocial = dr("razonSocial")
            cli.cuit = dr("cuit")
            cli.Email = dr("email")
            cli.domicilio = dr("domicilio")
            cli.localidad = dr("localidad")
            cli.provincia = dr("provincia")
            cli.pais = dr("pais")
            cli.CP = dr("CP")
            cli.SFI = dr("SFI")
            cli.fechaAlta = dr("fechaAlta")
            cli.clienteDVH = dr("clienteDVH")
        End If
        Return cli
    End Function

    Public Function eliminar_cliente(cliente As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@idcliente", cliente)
        Dim i As Integer = da.Escribir("cliente_eliminar", hs)
        Return i
    End Function

End Class
