
Public Class Traductor
    Public Shared Function TraducirControles(controles As Object, idioma As String)

        If Not String.IsNullOrWhiteSpace(idioma) Then
            For Each c In controles

                Dim tipo As String = c.GetType.ToString
                Select Case tipo
                    Case "System.Web.UI.WebControls.Label",
                         "System.Web.UI.WebControls.Button",
                         "System.Web.UI.WebControls.LinkButton"
                        c.Text = Traducir(c.ID, idioma)

                    Case "System.Web.UI.WebControls.ContentPlaceHolder"
                        For Each subC In c.controls
                            Dim tipoSubC As String = subC.GetType.ToString
                            Select Case tipoSubC
                                Case "System.Web.UI.WebControls.Label",
                                     "System.Web.UI.WebControls.Button",
                                     "System.Web.UI.WebControls.LinkButton"
                                    subC.Text = Traducir(subC.ID, idioma)
                                Case "System.Web.UI.WebControls.PlaceHolder"
                                    For Each subSubC In subC.controls
                                        Dim tipoSubSubC As String = subSubC.GetType.ToString
                                        Select Case tipoSubSubC
                                            Case "System.Web.UI.WebControls.Label",
                                                 "System.Web.UI.WebControls.Button",
                                                 "System.Web.UI.WebControls.LinkButton"
                                                subSubC.Text = Traducir(subSubC.ID, idioma)
                                        End Select
                                    Next

                            End Select
                        Next

                    Case "System.Web.UI.WebControls.Menu"
                        For Each item In c.items
                            If item.value = "menuCultura" Then
                                item.Text = idioma
                            Else
                                If Not item.value = "menuUsuario" Then
                                    item.Text = Traducir(item.value, idioma)
                                End If
                            End If
                            For Each subItem In item.childitems
                                Dim str As String = subItem.value.ToString
                                Dim str2 As String = str.Substring(1)
                                If Not str2 = "menuCultura" Then
                                    subItem.Text = Traducir(subItem.value, idioma)
                                End If
                            Next
                        Next
                End Select

            Next
        End If

    End Function

    Public Shared Function Mensaje(nombre As String, idioma As String) As String
        Dim trad As BE.Mensaje = New BE.Mensaje
        Dim _gestorMensaje As GestorMensaje = New GestorMensaje
        trad = _gestorMensaje.leer_mensaje(nombre, idioma)
        If Not String.IsNullOrWhiteSpace(trad.descripcion) AndAlso Not Nothing Then
            Return trad.descripcion
        Else
            Return nombre
        End If
    End Function

    Public Shared Function Traducir(nombre As String, idioma As String)
        Dim trad As BE.Mensaje = New BE.Mensaje
        Dim _gestorMensaje As GestorMensaje = New GestorMensaje
        trad = _gestorMensaje.leer_mensaje(nombre, idioma)
        If Not String.IsNullOrWhiteSpace(trad.descripcion) AndAlso Not Nothing Then
            Return trad.descripcion
        Else
            'DMS - display de objetos no traducidos
            'MsgBox("no se pudo traducir --> " + nombre)
            Return trad.descripcion
        End If
    End Function
    Public Shared Function TraducirMensage(ByVal codigo As Integer, _
                                           ByVal msgDefecto As String) As String
        Select Case codigo
            Case 101
                Return "Error al agregar el usuario."
            Case 102
                Return "Error al modificar el usuario."
            Case 103
                Return "Error al eliminar el usuario."
            Case 104
                Return "Error al listar los usuarios."
            Case 201
                Return "Error al agregar el permiso."
            Case 202
                Return "Error al modificar el permiso."
            Case 203
                Return "Error al eliminar el permiso."
            Case 204
                Return "Error al listar los permisos."

            Case 401
                Return "Error al agregar el cliente."
            Case 402
                Return "Error al modificar el cliente."
            Case 403
                Return "Error al eliminar el cliente."
            Case 404
                Return "Error al listar los clientes."
            Case 407
                Return "El cliente es inválido."

            Case 991
                Return "Se agregó exitosamente."
            Case 992
                Return "Se modificó exitosamente."
            Case 993
                Return "Se eliminó exitosamente."
            Case 994
                Return "Error al agregar el objeto."
            Case 995
                Return "Error al modificar el objeto."
            Case 996
                Return "Error al eliminar el objeto."
            Case 999
                Return "Error objeto no encontrado."
            Case Else
                If Not String.IsNullOrWhiteSpace(msgDefecto) Then
                    Return msgDefecto
                Else
                    Return String.Format("codigo no encontrado {0}", codigo)
                End If
        End Select
    End Function


End Class
