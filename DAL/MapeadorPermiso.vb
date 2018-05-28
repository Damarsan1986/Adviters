Imports System.Collections.Generic
Imports DAL
<Serializable()> _
Public Class MapeadorPermiso

    Dim da As New DAL.Accesos
    Public Function escribir_permiso(perm As BE.PermisoBase) As Integer
        Dim hs As New Hashtable
        hs.Add("@nombre", perm.Nombre)
        hs.Add("@descripcion", perm.Descripcion)
        hs.Add("@esAccion", perm.esAccion)
        hs.Add("@permisoDVH", perm.permisoDVH)
        Dim i As Integer = da.Escribir("permiso_escribir", hs)

        Return i
    End Function

    Public Function escribir_permiso_Hijo(padre As String, hijo As String, dvh As String) As Integer

        Dim hs3 As New Hashtable
        hs3.Add("@padre", padre.ToString)
        hs3.Add("@hijo", hijo.ToString)
        hs3.Add("@permisoPermisoDVH", dvh.ToString)
        Dim i As Integer = da.Escribir("permiso_permiso_escribir", hs3)

        Return i
    End Function

    Public Function eliminar_permiso_permiso(padre As String) As Integer

        Dim hs3 As New Hashtable
        hs3.Add("@nombre", padre.ToString)
        Dim i As Integer = da.Escribir("permiso_permiso_eliminar", hs3)

        Return i
    End Function

    Public Function eliminar_permiso_Hijo(padre As String, hijo As String) As Integer

        Dim hs3 As New Hashtable
        hs3.Add("@padre", padre.ToString)
        hs3.Add("@hijo", hijo.ToString)
        Dim i As Integer = da.Escribir("permiso_hijo_eliminar", hs3)

        Return i
    End Function
    Public Function leer_permiso() As List(Of BE.PermisoBase)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.PermisoBase)
        Dim perm As BE.PermisoBase
        dt = da.Leer("permiso_listar")

        For Each dr As DataRow In dt.Rows
            If dr("esAccion") Then
                perm = New BE.PermisoSimple
            Else
                perm = New BE.PermisoCompuesto
            End If
            perm.Nombre = dr("codigo")
            perm.Descripcion = dr("descripcion")
            perm.esAccion = dr("esAccion")
            perm.permisoDVH = dr("permisoDVH")

            ls.Add(perm)
        Next
        Return ls
    End Function
    Public Function leer_permiso(filtro As BE.PermisoFiltro) As List(Of BE.PermisoBase)
        Dim perm As BE.PermisoBase
        Dim nombrePermiso As String = filtro.Nombre
        Dim hs As New Hashtable
        hs.Add("@nombre", nombrePermiso)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.PermisoBase)
        dt = da.Leer("permiso_leer", hs)

        For Each dr As DataRow In dt.Rows
            If dr("esAccion") Then
                perm = New BE.PermisoSimple
            Else
                perm = New BE.PermisoCompuesto
            End If
            perm.esAccion = dr("esAccion")
            perm.Nombre = dr("codigo")
            perm.Descripcion = dr("descripcion")
            perm.permisoDVH = dr("permisoDVH")

            ls.Add(perm)
        Next
        Return ls

    End Function

 
    Public Function leer_UnPermiso(filtro As BE.PermisoFiltro) As BE.PermisoBase
        Dim perm As BE.PermisoBase
        Dim hs As New Hashtable
        Dim nombrePermiso As String = filtro.Nombre
        hs.Add("@nombre", nombrePermiso)
        Dim dt As DataTable = da.Leer("permiso_leer", hs)
        If dt.Rows.Count <> 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("esAccion") Then
                perm = New BE.PermisoSimple
            Else
                perm = New BE.PermisoCompuesto
            End If
            perm.Descripcion = dr("descripcion")
            perm.esAccion = dr("esAccion")
            perm.Nombre = dr("codigo")
            perm.permisoDVH = dr("permisoDVH")

        End If
        Return perm

    End Function

    Public Function ListarHijos(filtro As BE.PermisoFiltro) As List(Of BE.PermisoBase)
        Dim perm As BE.PermisoBase
        Dim ls As List(Of BE.PermisoBase) = New List(Of BE.PermisoBase)
        Dim hs As New Hashtable
        Dim nombrePermiso As String = filtro.Nombre
        hs.Add("@nombre", nombrePermiso)
        Dim dt As New DataTable
        dt = da.Leer("permiso_permiso_leer", hs)
        For Each dr As DataRow In dt.Rows
            If dr("esAccion") Then
                perm = New BE.PermisoSimple
            Else
                perm = New BE.PermisoCompuesto
            End If
            perm.esAccion = dr("esAccion")
            perm.Nombre = dr("codigo")
            perm.Descripcion = dr("descripcion")
            perm.permisoDVH = dr("permisoDVH")

            ls.Add(perm)
        Next
        Return ls

    End Function
    Public Function ConsultarUno(filtro As BE.PermisoFiltro) As BE.PermisoBase
        Dim lista As List(Of BE.PermisoBase) = Me.leer_permiso(filtro)
        If lista.Count > 0 Then
            Return lista.Item(0)
        Else
            Return Nothing
        End If
    End Function
    Public Function eliminar_permiso(perm As String) As Integer
        Dim hs As New Hashtable
        hs.Add("@nombre", perm)

        Dim i As Integer = da.Escribir("permiso_eliminar", hs)
        Return i
    End Function
    Public Function leer_permiso_permiso() As List(Of BE.PermisoPermiso)
        Dim dt As New DataTable
        Dim ls As New List(Of BE.PermisoPermiso)
        Dim perm As BE.PermisoPermiso
        dt = da.Leer("permiso_permiso_listar")

        For Each dr As DataRow In dt.Rows
            perm = New BE.PermisoPermiso
            perm.codigoPadre = dr("codigoPadre")
            perm.codigoHijo = dr("codigoHijo")
            perm.permisoPermisoDVH = dr("permisoPermisoDVH")

            ls.Add(perm)
        Next
        Return ls
    End Function
End Class


