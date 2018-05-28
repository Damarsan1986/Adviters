Public Class MapeadorBitacora
    Dim da As New DAL.Accesos

    Public Function escribir_bitacora(bitacora As BE.bitacora) As Integer

        Dim hs As New Hashtable
        hs.Add("@idBitacora", bitacora.idBitacora)
        hs.Add("@idUsuario", bitacora.idUsuario)
        hs.Add("@descripcion", bitacora.descripcion)
        hs.Add("@fechaHoraEvento", bitacora.fechaHoraEvento)
        hs.Add("@dvhBitacora", bitacora.dvhBitacora)

        Dim i As Integer = da.Escribir("bitacora_escribir", hs)
        Return i
    End Function

    Public Function leer_bitacora() As List(Of BE.bitacora)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.bitacora)
        dt = da.Leer("bitacora_listar")
        For Each dr As DataRow In dt.Rows
            Dim bitacora As New BE.bitacora
            bitacora.idBitacora = dr("idBitacora")
            bitacora.idUsuario = dr("idUsuario")
            bitacora.descripcion = dr("Descripcion")
            bitacora.fechaHoraEvento = dr("fechaHoraEvento")
            bitacora.dvhBitacora = dr("dvhBitacora")
            ls.Add(bitacora)
        Next
        Return ls
    End Function

    Public Function leer_bitacora(id As String) As List(Of BE.bitacora)
        Dim hs As New Hashtable
        Dim ls As New List(Of BE.bitacora)
        hs.Add("@idUsuario", id)
        Dim dt As DataTable = da.Leer("bitacora_leer", hs)
        For Each dr As DataRow In dt.Rows
            Dim bitacora As New BE.bitacora
            bitacora.idBitacora = dr("idBitacora")
            bitacora.idUsuario = dr("idUsuario")
            bitacora.descripcion = dr("Descripcion")
            bitacora.fechaHoraEvento = dr("fechaHoraEvento")
            bitacora.dvhBitacora = dr("dvhBitacora")
            ls.Add(bitacora)
        Next
        Return ls
    End Function

    Public Function leer_bitacora_fecha(fdesde As DateTime, fhasta As DateTime) As List(Of BE.bitacora)
        Dim hs As New Hashtable
        Dim ls As New List(Of BE.bitacora)
        hs.Add("@fechaDesde", fdesde)
        hs.Add("@fechaHasta", fhasta)
        Dim dt As DataTable = da.Leer("bitacora_leer_fecha", hs)
        For Each dr As DataRow In dt.Rows
            Dim bitacora As New BE.bitacora
            bitacora.idBitacora = dr("idBitacora")
            bitacora.idUsuario = dr("idUsuario")
            bitacora.descripcion = dr("Descripcion")
            bitacora.fechaHoraEvento = dr("fechaHoraEvento")
            bitacora.dvhBitacora = dr("dvhBitacora")
            ls.Add(bitacora)
        Next
        Return ls
    End Function

    Public Function leer_bitacora_descripcion(descripcion As String) As List(Of BE.bitacora)
        Dim hs As New Hashtable
        Dim ls As New List(Of BE.bitacora)
        hs.Add("@descripcion", descripcion)
        Dim dt As DataTable = da.Leer("bitacora_leer_descripcion", hs)
        For Each dr As DataRow In dt.Rows
            Dim bitacora As New BE.bitacora
            bitacora.idBitacora = dr("idBitacora")
            bitacora.idUsuario = dr("idUsuario")
            bitacora.descripcion = dr("Descripcion")
            bitacora.fechaHoraEvento = dr("fechaHoraEvento")
            bitacora.dvhBitacora = dr("dvhBitacora")
            ls.Add(bitacora)
        Next
        Return ls
    End Function

    Public Function actualizar_bitacora(bitacora As BE.bitacora) As Integer

        Dim hs As New Hashtable
        hs.Add("@idBitacora", bitacora.idBitacora)
        hs.Add("@dvhBitacora", bitacora.dvhBitacora)

        Dim i As Integer = da.Escribir("bitacora_actualizar", hs)
        Return i
    End Function

    Public Function migrar_bitacora(fDesde As DateTime) As Integer

        Dim hs As New Hashtable
        hs.Add("@FechaDesde", fDesde)

        Dim i As Integer = da.Escribir("bitacora_migrar", hs)
        Return i
    End Function

    Public Function desmigrar_bitacora(fDesde As DateTime, fHasta As DateTime) As Integer

        Dim hs As New Hashtable
        hs.Add("@FechaDesde", fDesde)
        hs.Add("@FechaHasta", fHasta)

        Dim i As Integer = da.Escribir("bitacora_desmigrar", hs)
        Return i
    End Function
End Class

