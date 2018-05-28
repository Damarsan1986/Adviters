Imports Seguridad
Public Class GestorCultura
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As String = ""
    Dim gestor_cultura As New DAL.MapeadorCultura

    Public Function escribir_cultura(cultura As BE.Cultura) As Boolean
        Dim mistring As String = cultura.Descripcion.ToString + cultura.idCultura.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        cultura.culturaDVH = miDVH

        Dim i As Integer = gestor_cultura.escribir_cultura(cultura)
        If i > 0 Then
            ActualizarSum()

            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR CULTURA")
        Return False
    End Function

    Public Function leer_cultura() As List(Of BE.Cultura)
        Return gestor_cultura.leer_cultura()
    End Function

    Public Function leer_cultura(id As String) As BE.Cultura
        Return gestor_cultura.leer_cultura(id)
    End Function

    Public Function borrar_cultura(id As String) As Boolean
        Dim i As Integer = gestor_cultura.eliminar_cultura(id)
        If i > 0 Then
            ActualizarSum()
            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR al borrar CULTURA")
        Return False
    End Function

    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_cultura"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Cultura) = leer_cultura()
        sumaDVH = ""
        For Each registro As BE.Cultura In ls
            sumaDVH = sumaDVH + registro.idCultura.ToString
        Next
        Return sumaDVH.ToString + "t_cultura"
    End Function
End Class
