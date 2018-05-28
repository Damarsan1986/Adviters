Public Class MapeadorDVV
    Dim da As New DAL.Accesos

    Public Function escribir_DVV(dvv As BE.DVV) As Integer

        Dim hs As New Hashtable
        hs.Add("@idtabla", dvv.idtabla)
        hs.Add("@tabla_DVV", dvv.tabla_DVV)

        Dim i As Integer = da.Escribir("dvv_escribir", hs)
        Return i
    End Function

    Public Function leer_DVV(idtabla As String) As BE.DVV
        Dim miDVV As New BE.DVV
        miDVV.idtabla = idtabla
        Dim hs As New Hashtable
        hs.Add("@idtabla", idtabla)
        Dim dt As DataTable = da.Leer("dvv_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            miDVV.tabla_DVV = dr("tabla_DVV")
        End If
        Return miDVV

    End Function
 
End Class

