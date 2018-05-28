<Serializable()> _
Public Class GestorDVV

    Dim gestor_DVV As New DAL.MapeadorDVV
    Dim sumaUser As Integer = 0

    Public Function escribir_DVV(dvv As BE.DVV) As Boolean
        Dim miDVV As New BE.DVV
        miDVV.idtabla = dvv.idtabla
        miDVV.tabla_DVV = Criptografia.ObtenerInstancia().MD5(dvv.tabla_DVV)

        Dim i As Integer = gestor_DVV.escribir_DVV(miDVV)
        If i > 0 Then
            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR DVV")
        Return False
    End Function


    Public Function leer_DVV(id As String) As BE.DVV
        Return gestor_DVV.leer_DVV(id)
    End Function

End Class
