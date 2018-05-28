Imports Seguridad
Public Class gestorMoneda
    Dim gestor_Moneda As New DAL.MapeadorMoneda
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_Moneda(moneda As BE.Moneda) As String
        Dim mistring As String = moneda.idMoneda.ToString + moneda.descripcionCorta.ToString + moneda.descripcionLarga.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        moneda.monedaDVH = miDVH

        Dim i As Integer = gestor_Moneda.insertar_moneda(moneda)
        If i > 0 Then
            ActualizarSum()
            Return "Se escribio correctamente"
        End If
        Return "No se pudo insertar"

    End Function


    Public Function leer_moneda() As List(Of BE.Moneda)
        Return gestor_Moneda.leer_moneda()

    End Function

    Public Function leer_moneda(idmoneda As Integer) As BE.Moneda
        Return gestor_Moneda.leer_moneda(idmoneda)
    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_moneda"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Moneda) = leer_moneda()
        sumaDVH = 0
        For Each registro As BE.Moneda In ls
            sumaDVH = sumaDVH + registro.idMoneda
        Next
        Return sumaDVH.ToString + "t_moneda"
    End Function
End Class
