Public Class MapeadorCultura
    Dim da As New DAL.Accesos

    Public Function escribir_cultura(cultura As BE.Cultura) As Integer

        Dim hs As New Hashtable
        hs.Add("@idcultura", cultura.idCultura.ToString)
        hs.Add("@descripcion", cultura.Descripcion)
        hs.Add("@culturaDVH", cultura.culturaDVH)

        Dim i As Integer = da.Escribir("cultura_escribir", hs)
        Return i
    End Function
    Public Function leer_cultura() As List(Of BE.Cultura)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Cultura)
        dt = da.Leer("cultura_listar")
        Dim cultura As BE.Cultura
        For Each dr As DataRow In dt.Rows
            cultura = New BE.Cultura
            Try
                cultura.idCultura = New System.Globalization.CultureInfo(Convert.ToString(dr("idCultura")))
                cultura.Descripcion = dr("Descripcion")
            Catch ex As Exception
                cultura.idCultura = New System.Globalization.CultureInfo("es-AR")
                cultura.Descripcion = "Español Argentina"
            End Try

            cultura.culturaDVH = dr("culturaDVH")
            ls.Add(cultura)
        Next
        Return ls
    End Function

    Public Function leer_cultura(id As String) As BE.Cultura
        Dim hs As New Hashtable
        Dim cult As New BE.Cultura
        hs.Add("@idCultura", id)
        Dim dt As DataTable = da.Leer("cultura_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Try
                cult.idCultura = New System.Globalization.CultureInfo(Convert.ToString(dr("idCultura")))
                cult.Descripcion = dr("Descripcion")
            Catch ex As Exception
                cult.idCultura = New System.Globalization.CultureInfo("es-AR")
                cult.Descripcion = "Español Argentina"
            End Try
            cult.culturaDVH = dr("culturaDVH")
        End If
        Return cult
    End Function

    Public Function eliminar_cultura(cultura As String) As Integer

        Dim hs As New Hashtable
        hs.Add("@idcultura", cultura.ToString)

        Dim i As Integer = da.Escribir("cultura_eliminar", hs)
        Return i
    End Function


End Class

