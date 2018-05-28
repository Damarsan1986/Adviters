Public Class GestorMensaje


    Dim gestor_mensaje As New DAL.MapeadorMensaje
    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As String = ""
    Public Function escribir_mensaje(mensaje As BE.Mensaje) As Boolean
        Dim mistring As String = mensaje.idMensaje.ToString + mensaje.descripcion.ToString + mensaje.Cultura.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        mensaje.mensajeDVH = miDVH

        Dim i As Integer = gestor_mensaje.escribir_mensaje(mensaje)
        If i > 0 Then
            ActualizarSum()
            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR mensaje")
        Return False
    End Function
    Public Function escribir_mensaje(idcultura As String, idculturaNew As String) As Boolean

        Dim i As Integer = gestor_mensaje.escribir_mensaje(idcultura, idculturaNew)
        If i > 0 Then
            ActualizarDVH()
            ActualizarSum()
            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR mensaje")
        Return False
    End Function
    Public Function leer_mensaje() As List(Of BE.Mensaje)
        Return gestor_mensaje.leer_mensaje()
    End Function

    Public Function leer_mensaje(id As String, idioma As String) As BE.Mensaje
        Return gestor_mensaje.leer_mensaje(id, idioma)
    End Function

    Public Function leer_mensaje(idioma As String) As List(Of BE.Mensaje)
        Return gestor_mensaje.leer_mensaje(idioma)
    End Function

    Public Function leer_mensaje_complejo(idioma1 As String, idioma2 As String) As List(Of BE.MensajeComplejo)
        Return gestor_mensaje.leer_mensaje_complejo(idioma1, idioma2)
    End Function


    Public Function eliminar_mensaje(idmensaje As String) As Boolean
        Dim i As Integer = gestor_mensaje.eliminar_mensaje(idmensaje)
        If i > 0 Then
            ActualizarSum()
            'Se escribio correctamente
            Return True
        End If
        Return False
    End Function
    Public Function eliminar_mensaje_cultura(cultura As String) As Boolean
        Dim i As Integer = gestor_mensaje.eliminar_mensaje_cultura(cultura)
        If i > 0 Then
            ActualizarSum()
            'Se escribio correctamente
            Return True
        End If
        Return False
    End Function
    Public Sub ActualizarDVH()
        Dim ls As List(Of BE.Mensaje) = leer_mensaje()
        For Each mensaje As BE.Mensaje In ls
            Dim mistring As String = mensaje.idMensaje.ToString + mensaje.descripcion.ToString + mensaje.Cultura.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
            If Not (mensaje.mensajeDVH = miDVH) Then
                mensaje.mensajeDVH = miDVH
                escribir_mensaje(mensaje)
            End If
        Next
    End Sub
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_mensaje"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Mensaje) = leer_mensaje()
        sumaDVH = ""
        For Each registro As BE.Mensaje In ls
            sumaDVH = sumaDVH + registro.idMensaje.ToString
        Next
        Return sumaDVH.ToString + "t_mensaje"
    End Function
End Class
