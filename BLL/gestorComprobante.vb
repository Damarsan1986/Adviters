Imports Seguridad
Public Class gestorComprobante
    Dim gestor_comprobante As New DAL.MapeadorComprobante
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_comprobante(comp As BE.Comprobante) As String
        Dim mistring As String = comp.idCliente.ToString + comp.idConsumidor.ToString + comp.idOperador.ToString + comp.monedaOperacion.ToString + comp.descOperacion.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        comp.comprobanteDVH = miDVH

        Dim i As Integer = gestor_comprobante.insertar_Comprobante(comp)
        If i > 0 Then
            'Se escribio correctamente
            ActualizarSum()
        End If
        Return i

    End Function

    Public Function actualizar_comprobante(comp As BE.Comprobante) As String
        Dim mistring As String = comp.idCliente.ToString + comp.idConsumidor.ToString + comp.idOperador.ToString + comp.monedaOperacion.ToString + comp.descOperacion.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        comp.comprobanteDVH = miDVH

        Dim i As Integer = gestor_comprobante.actualizar_Comprobante(comp)
        If i > 0 Then
            'Se escribio correctamente
            ActualizarSum()
        End If
        Return i

    End Function


    Public Function leer_comprobante() As List(Of BE.Comprobante)
        Return gestor_comprobante.leer_Comprobante()

    End Function

    Public Function leer_comprobante(idcomprobante As Integer) As BE.Comprobante
        Return gestor_comprobante.leer_Comprobante(idcomprobante)

    End Function
    Public Function leer_comprobante(idcomprobante As BE.Comprobante) As BE.Comprobante
        Return gestor_comprobante.leer_Comprobante(idcomprobante)

    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_comprobante"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Comprobante) = leer_comprobante()
        sumaDVH = 0
        For Each registro As BE.Comprobante In ls
            sumaDVH = sumaDVH + registro.idComprobante
        Next
        Return sumaDVH.ToString + "t_comprobante"
    End Function
End Class
