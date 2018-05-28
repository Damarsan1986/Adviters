Imports Seguridad
Public Class gestorD_Comprobante
    Dim gestor_D_Comprobante As New DAL.MapeadorD_Comprobante
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_D_Comprobante(comp As BE.D_Comprobante) As String
        Dim mistring As String = comp.idComprobante.ToString + comp.idProducto.ToString + comp.cantidad.ToString + comp.pUnitario.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        comp.dComprobanteDVH = miDVH

        Dim i As Integer = gestor_D_Comprobante.insertar_D_Comprobante(comp)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function

    Public Function actualizar_D_Comprobante(comp As BE.D_Comprobante) As String
        Dim mistring As String = comp.idComprobante.ToString + comp.idProducto.ToString + comp.cantidad.ToString + comp.pUnitario.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        comp.dComprobanteDVH = miDVH

        Dim i As Integer = gestor_D_Comprobante.actualizar_D_Comprobante(comp)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function


    Public Function leer_D_Comprobante() As List(Of BE.D_Comprobante)
        Return gestor_D_Comprobante.leer_D_Comprobante()

    End Function

    Public Function leer_D_Comprobante(idComprobante As Integer) As List(Of BE.D_Comprobante)
        Return gestor_D_Comprobante.leer_D_Comprobante(idComprobante)

    End Function

    Public Function leer_D_Comprobante(idComprobante As Integer, idD_Comprobante As Integer) As BE.D_Comprobante
        Return gestor_D_Comprobante.leer_D_Comprobante(idComprobante, idD_Comprobante)

    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_dComprobante"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.D_Comprobante) = leer_D_Comprobante()
        sumaDVH = 0
        For Each registro As BE.D_Comprobante In ls
            sumaDVH = sumaDVH + registro.idD_Comprobante
        Next
        Return sumaDVH.ToString + "t_dComprobante"
    End Function
End Class
