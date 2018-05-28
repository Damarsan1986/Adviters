
Public Class MapeadorMensaje
    Dim da As New DAL.Accesos

    Public Function escribir_mensaje(mensaje As BE.Mensaje) As Integer

        Dim hs As New Hashtable
        hs.Add("@idMensaje", mensaje.idMensaje)
        hs.Add("@cultura", mensaje.Cultura.ToString)
        hs.Add("@descripcion", mensaje.descripcion)
        hs.Add("@mensajeDVH", mensaje.mensajeDVH)

        Dim i As Integer = da.Escribir("mensaje_escribir", hs)
        Return i
    End Function

    Public Function escribir_mensaje(idCultura As String, idCulturaNew As String) As Integer

        Dim hs As New Hashtable
        hs.Add("@culturaNew", idCulturaNew)
        hs.Add("@cultura", idCultura)

        Dim i As Integer = da.Escribir("mensaje_escribir_masivo", hs)
        Return i
    End Function

    Public Function leer_mensaje() As List(Of BE.Mensaje)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Mensaje)
        dt = da.Leer("mensaje_listar")
        For Each dr As DataRow In dt.Rows
            Dim mensaje As New BE.Mensaje
            mensaje.idMensaje = dr("idMensaje")
            mensaje.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            mensaje.descripcion = dr("Descripcion")
            mensaje.mensajeDVH = dr("mensajeDVH")

            ls.Add(mensaje)
        Next
        Return ls
    End Function

    Public Function leer_mensaje(idioma As String) As List(Of BE.Mensaje)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Mensaje)
        Dim hs As New Hashtable
        hs.Add("@cultura", idioma)
        dt = da.Leer("mensaje_listar_cultura", hs)
        For Each dr As DataRow In dt.Rows
            Dim mensaje As New BE.Mensaje
            mensaje.idMensaje = dr("idMensaje")
            mensaje.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            mensaje.descripcion = dr("Descripcion")
            mensaje.mensajeDVH = dr("mensajeDVH")

            ls.Add(mensaje)
        Next
        Return ls
    End Function

    Public Function leer_mensaje_complejo(idioma1 As String, idioma2 As String) As List(Of BE.MensajeComplejo)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.MensajeComplejo)
        Dim hs As New Hashtable
        hs.Add("@idioma1", idioma1)
        hs.Add("@idioma2", idioma2)
        dt = da.Leer("mensaje_complejo_listar", hs)
        For Each dr As DataRow In dt.Rows
            Dim mensaje As New BE.MensajeComplejo
            mensaje.idMensaje = dr("idMensaje")
            mensaje.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            mensaje.descripcion = dr("Descripcion")
            mensaje.descripcion2 = dr("DescripcionEdicion")


            ls.Add(mensaje)
        Next
        Return ls
    End Function
    Public Function leer_mensaje(id As String, cultura As String) As BE.Mensaje
        Dim MensajeTraducido As New BE.Mensaje
        Dim hs As New Hashtable
        Dim ls As New List(Of BE.Mensaje)
        hs.Add("@idMensaje", id)
        hs.Add("@cultura", cultura)
        Dim dt As DataTable = da.Leer("mensaje_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            MensajeTraducido.idMensaje = dr("idMensaje")
            MensajeTraducido.Cultura = New System.Globalization.CultureInfo(Convert.ToString(dr("cultura")))
            MensajeTraducido.descripcion = dr("Descripcion")
            MensajeTraducido.mensajeDVH = dr("mensajeDVH")
        End If
        Return MensajeTraducido
    End Function

    Public Function eliminar_mensaje(idmensaje As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@idMensaje", idmensaje)

        Dim i As Integer = da.Escribir("mensaje_eliminar", hs)
        Return i
    End Function

    Public Function eliminar_mensaje_cultura(cultura As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@cultura", cultura)

        Dim i As Integer = da.Escribir("mensaje_eliminar_cultura", hs)
        Return i
    End Function
End Class
