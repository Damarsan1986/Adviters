Public Class MapeadorMenu

    Dim da As New Accesos
    Public Function escribir_menu(menu As BE.Menu) As Integer
        Dim hs As New Hashtable
        hs.Add("@menuId", menu.menuId)
        hs.Add("@parentMenuId", menu.parentMenuId)
        hs.Add("@titulo", menu.titulo)
        hs.Add("@descripcion", menu.descripcion)
        hs.Add("@url", menu.url)
        hs.Add("@menuDVH", menu.menuDVH)

        Dim i As Integer = da.Escribir("menu_Escribir", hs)
        Return i

    End Function

    Public Function leer_menu() As List(Of BE.Menu)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Menu)
        dt = da.Leer("menu_Listar")

        For Each dr As DataRow In dt.Rows
            Dim menu As New BE.Menu
            menu.menuId = dr("menuId")
            menu.parentMenuId = dr("parentMenuId")
            menu.titulo = dr("titulo")
            menu.descripcion = dr("descripcion")
            menu.url = dr("url")
            menu.menuDVH = dr("menuDVH")

            ls.Add(menu)
        Next
        Return ls
    End Function

    Public Function leer_parentMenu(parent As Integer) As List(Of BE.Menu)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.Menu)
        Dim hs As New Hashtable
        hs.Add("@parentMenuId", parent)
        dt = da.Leer("menu_ListarParent", hs)

        For Each dr As DataRow In dt.Rows
            Dim menu As New BE.Menu
            menu.menuId = dr("menuId")
            menu.parentMenuId = dr("parentMenuId")
            menu.titulo = dr("titulo")
            menu.descripcion = dr("descripcion")
            menu.url = dr("url")
            menu.menuDVH = dr("menuDVH")

            ls.Add(menu)
        Next
        Return ls
    End Function

    Public Function leer_menu(UnMenu As Integer) As BE.Menu
        Dim menu As New BE.Menu
        Dim hs As New Hashtable
        hs.Add("@menuId", UnMenu)
        Dim dt As DataTable = da.Leer("menu_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            menu.menuId = dr("menuId")
            menu.parentMenuId = dr("parentMenuId")
            menu.titulo = dr("titulo")
            menu.descripcion = dr("descripcion")
            menu.url = dr("url")
            menu.menuDVH = dr("menuDVH")

        End If
        Return menu
    End Function

    Public Function eliminar_menu(UnMenu As BE.Menu) As Integer
        Dim hs As New Hashtable
        hs.Add("@menuId", UnMenu)
        Dim i As Integer = da.Escribir("menu_eliminar", hs)
        Return i
    End Function

End Class
