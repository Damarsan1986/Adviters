Imports Seguridad
Public Class GestorMenu

    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As Integer = 0
    Dim mapeador_Menu As New DAL.MapeadorMenu
    Public Function escribir_menu(menu As BE.Menu) As Boolean
        Dim mistring As String = menu.menuId.ToString + menu.parentMenuId.ToString + menu.descripcion.ToString + menu.titulo.ToString + menu.url.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        menu.menuDVH = miDVH

        Dim i As Integer = mapeador_Menu.escribir_menu(menu)
        If i > 0 Then
            ActualizarSum()
            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR menu")
        Return False
    End Function

    Public Function leer_menu() As List(Of BE.Menu)
        Return mapeador_Menu.leer_menu()
    End Function

    Public Function leer_menu(unMenu As Integer) As BE.Menu
        Return mapeador_Menu.leer_menu(unMenu)
    End Function

    Public Function leer_parentMenu(parent As Integer) As List(Of BE.Menu)
        Return mapeador_Menu.leer_parentMenu(parent)
    End Function

    Public Function eliminar_menu(unMenu As BE.Menu) As Boolean
        Dim i As Integer = mapeador_Menu.eliminar_menu(unMenu)
        If i > 0 Then
            ActualizarSum()
            'Se escribio correctamente
            Return True
        End If
        Return False
    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_menu"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.Menu) = leer_menu()
        sumaDVH = 0
        For Each registro As BE.Menu In ls
            sumaDVH = sumaDVH + registro.menuId
        Next
        Return sumaDVH.ToString + "t_menu"
    End Function
   End Class
