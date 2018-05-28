Public Class MapeadorConsumidor

    Dim da As New Accesos
    Public Function insertar_Consumidor(cons As BE.Consumidor) As Integer
        Dim hs As New Hashtable
        hs.Add("@Idcliente", cons.idCliente)
        hs.Add("@IdConsumidor", cons.idConsumidor)
        hs.Add("@nombre", cons.Nombre)
        hs.Add("@apellido", cons.Apellido)
        hs.Add("@dni", cons.dni)
        hs.Add("@Email", cons.Email)
        hs.Add("@domicilio", cons.domicilio)
        hs.Add("@localidad", cons.localidad)
        hs.Add("@provincia", cons.provincia)
        hs.Add("@pais", cons.pais)
        hs.Add("@CP", cons.CP)
        hs.Add("@fechaAlta", cons.fechaAlta)
        hs.Add("@consumidorDVH", cons.consumidorDVH)

        Dim i As Integer = da.Escribir("Consumidor_escribir", hs)
        Return i

    End Function

    Public Function leer_Consumidor() As List(Of BE.Consumidor)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Consumidor)
        dt = da.Leer("Consumidor_listar")

        For Each dr As DataRow In dt.Rows
            Dim cons As New BE.Consumidor
            cons.idCliente = dr("idcliente")
            cons.idConsumidor = dr("idConsumidor")
            cons.Nombre = dr("nombre")
            cons.Apellido = dr("apellido")
            cons.dni = dr("dni")
            cons.Email = dr("Email")
            cons.domicilio = dr("domicilio")
            cons.localidad = dr("localidad")
            cons.provincia = dr("provincia")
            cons.pais = dr("pais")
            cons.CP = dr("CP")
            cons.fechaAlta = dr("fechaAlta")
            cons.consumidorDVH = dr("consumidorDVH")

            ls.Add(cons)
        Next
        Return ls
    End Function

    Public Function leer_Consumidor(idcliente As Integer) As List(Of BE.Consumidor)

        Dim ls As New List(Of BE.Consumidor)
        Dim hs As New Hashtable
        hs.Add("@Idcliente", idcliente)
        Dim dt As DataTable = da.Leer("Consumidor_leer_cliente", hs)

        For Each dr As DataRow In dt.Rows
            Dim cons As New BE.Consumidor
            cons.idCliente = dr("idcliente")
            cons.idConsumidor = dr("idConsumidor")
            cons.Nombre = dr("nombre")
            cons.Apellido = dr("apellido")
            cons.dni = dr("dni")
            cons.Email = dr("Email")
            cons.domicilio = dr("domicilio")
            cons.localidad = dr("localidad")
            cons.provincia = dr("provincia")
            cons.pais = dr("pais")
            cons.CP = dr("CP")
            cons.fechaAlta = dr("fechaAlta")
            cons.consumidorDVH = dr("consumidorDVH")

            ls.Add(cons)
        Next
        Return ls
    End Function


    Public Function leer_Consumidor(idcliente As Integer, idConsumidor As Integer) As BE.Consumidor
        Dim cons As New BE.Consumidor
        Dim hs As New Hashtable
        hs.Add("@Idcliente", idcliente)
        hs.Add("@idConsumidor", idConsumidor)
        Dim dt As DataTable = da.Leer("Consumidor_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            cons.idCliente = dr("idcliente")
            cons.idConsumidor = dr("idConsumidor")
            cons.Nombre = dr("nombre")
            cons.Apellido = dr("apellido")
            cons.dni = dr("dni")
            cons.Email = dr("Email")
            cons.domicilio = dr("domicilio")
            cons.localidad = dr("localidad")
            cons.provincia = dr("provincia")
            cons.pais = dr("pais")
            cons.CP = dr("CP")
            cons.fechaAlta = dr("fechaAlta")
            cons.consumidorDVH = dr("consumidorDVH")
        End If
        Return cons
    End Function

    Public Function leer_Consumidor_DNI(DNI As String) As BE.Consumidor
        Dim cons As New BE.Consumidor
        Dim hs As New Hashtable
        hs.Add("@DNI", DNI)
        Dim dt As DataTable = da.Leer("Consumidor_DNI_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            cons.idCliente = dr("idcliente")
            cons.idConsumidor = dr("idConsumidor")
            cons.Nombre = dr("nombre")
            cons.Apellido = dr("apellido")
            cons.dni = dr("dni")
            cons.Email = dr("Email")
            cons.domicilio = dr("domicilio")
            cons.localidad = dr("localidad")
            cons.provincia = dr("provincia")
            cons.pais = dr("pais")
            cons.CP = dr("CP")
            cons.fechaAlta = dr("fechaAlta")
            cons.consumidorDVH = dr("consumidorDVH")
        End If
        Return cons
    End Function

    Public Function eliminar_Consumidor(cliente As String, cons As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@idCliente", cliente)
        hs.Add("@idConsumidor", cons)
        Dim i As Integer = da.Escribir("Consumidor_eliminar", hs)
        Return i
    End Function

End Class
