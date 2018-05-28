Imports Seguridad
Public Class gestorMovEmpresa
    Dim gestor_mov_empresa As New DAL.MapeadorMovEmpresa
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Public Function insertar_mov_empresa(mov As BE.MovEmpresa) As String
        Dim mistring As String = mov.idComprobante.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.idEmpresa.ToString + mov.observaciones.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mov.movEmpresaDVH = miDVH

        Dim i As Integer = gestor_mov_empresa.insertar_mov_empresa(mov)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function

    Public Function actualizar_mov_empresa(mov As BE.MovEmpresa) As String
        Dim mistring As String = mov.idComprobante.ToString + mov.accion.ToString + mov.cantidad.ToString + mov.idEmpresa.ToString + mov.observaciones.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mov.movEmpresaDVH = miDVH

        Dim i As Integer = gestor_mov_empresa.actualizar_mov_empresa(mov)
        If i > 0 Then
            ActualizarSum()
        End If
        Return i
    End Function


    Public Function leer_mov_empresa() As List(Of BE.MovEmpresa)
        Return gestor_mov_empresa.leer_mov_empresa()

    End Function
    Public Function calcular_stock_empresa(id As Integer) As Integer
        Return gestor_mov_empresa.leer_mov_empresa_Stock(id)
    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_movEmpresa"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.MovEmpresa) = leer_mov_empresa()
        sumaDVH = 0
        For Each registro As BE.MovEmpresa In ls
            sumaDVH = sumaDVH + registro.idComprobante
        Next
        Return sumaDVH.ToString + "t_movEmpresa"
    End Function
End Class
