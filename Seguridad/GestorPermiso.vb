Imports System.Collections.Generic
<Serializable()> _
Public Class GestorPermiso

    Dim _gestorDVV As New GestorDVV
    Dim sumaDVH As String = ""
    Dim gestor_permiso As New DAL.MapeadorPermiso
    Public Function escribir_permiso(perm As BE.PermisoBase) As Boolean
        Try
            Dim mistring As String = perm.Nombre.ToString + perm.Descripcion.ToString
            Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
            perm.permisoDVH = miDVH

            Dim i As Integer = gestor_permiso.escribir_permiso(perm)

            If i > 0 Then
                '"Se escribio correctamente"
                If Not perm.esAccion Then
                    Dim miPermiso As BE.PermisoCompuesto = perm
                    gestor_permiso.eliminar_permiso_permiso(perm.Nombre.ToString)

                    For Each hijo As BE.PermisoBase In miPermiso.listaHijos

                        Dim mistring1 As String = perm.Nombre.ToString + hijo.Nombre.ToString
                        Dim miDVH1 As String = Criptografia.ObtenerInstancia().MD5(mistring1)

                        gestor_permiso.escribir_permiso_Hijo(perm.Nombre, hijo.Nombre, miDVH1)

                    Next

                End If
                ActualizarSum()
                ActualizarSumPP()
                Return True
            Else
                Return -1
            End If

            Return False
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function escribir_permiso_permiso(perm As BE.PermisoPermiso) As Boolean
        Dim mistring As String = perm.codigoPadre.ToString & perm.codigoHijo.ToString
        Dim miDVH As String = Criptografia.ObtenerInstancia().MD5(mistring)
        perm.permisoPermisoDVH = miDVH

        Dim i As Integer = gestor_permiso.escribir_permiso_Hijo(perm.codigoPadre, perm.codigoHijo, perm.permisoPermisoDVH)
        If i > 0 Then
            ActualizarSumPP()
            Return True             'Se escribio correctamente
        End If
        MsgBox("ERROR mensaje")
        Return False
    End Function
    Public Function leer_permiso() As List(Of BE.PermisoBase)
        Dim ListaPermiso As List(Of BE.PermisoBase) = gestor_permiso.leer_permiso()
        Dim ls As New List(Of BE.PermisoBase)
        Dim MiPermiso As New BE.PermisoCompuesto
        Dim miPermisoHijo As New BE.PermisoCompuesto
        For Each Permiso As BE.PermisoBase In ListaPermiso
            If Not Permiso.esAccion Then
                Dim _permisoFiltro As New BE.PermisoFiltro
                _permisoFiltro.Nombre = Permiso.Nombre

                Dim listaHijos As List(Of BE.PermisoBase) = gestor_permiso.ListarHijos(_permisoFiltro)
                MiPermiso = Permiso

                MiPermiso.listaHijos = New List(Of BE.PermisoBase)


                For Each hijo In listaHijos
                    If Not hijo.esAccion Then
                        _permisoFiltro.Nombre = hijo.Nombre
                        Dim listaHijosHijos As List(Of BE.PermisoBase) = gestor_permiso.ListarHijos(_permisoFiltro)

                        miPermisoHijo = hijo
                        miPermisoHijo.listaHijos = New List(Of BE.PermisoBase)
                        miPermisoHijo.listaHijos.AddRange(listaHijosHijos)

                        MiPermiso.listaHijos.Add(miPermisoHijo)
                    Else
                        MiPermiso.listaHijos.Add(hijo)
                    End If

                Next
                ls.Add(MiPermiso)
            Else
                ls.Add(Permiso)
            End If
        Next
        Return ls
    End Function

    Public Function leer_permiso_permiso() As List(Of BE.PermisoPermiso)
        Return gestor_permiso.leer_permiso_permiso()

    End Function
    Public Function leer_permiso(filtro As BE.PermisoFiltro) As List(Of BE.PermisoBase)
        Dim ListaPermiso As List(Of BE.PermisoBase) = gestor_permiso.leer_permiso(filtro)
        Dim ls As New List(Of BE.PermisoBase)
        Dim MiPermiso As New BE.PermisoCompuesto
        Dim miPermisoHijo As New BE.PermisoCompuesto
        For Each Permiso As BE.PermisoBase In ListaPermiso
            If Not Permiso.esAccion Then
                Dim _permisoFiltro As New BE.PermisoFiltro
                _permisoFiltro.Nombre = Permiso.Nombre

                Dim listaHijos As List(Of BE.PermisoBase) = gestor_permiso.ListarHijos(_permisoFiltro)
                MiPermiso = Permiso

                MiPermiso.listaHijos = New List(Of BE.PermisoBase)
                'DMS - FIX - revisar como agrega los hijos ya que antes estaba todo en la misma clase
                For Each hijo In listaHijos
                    If Not hijo.esAccion Then
                        _permisoFiltro.Nombre = hijo.Nombre
                        Dim listaHijosHijos As List(Of BE.PermisoBase) = gestor_permiso.ListarHijos(_permisoFiltro)

                        miPermisoHijo = hijo
                        miPermisoHijo.listaHijos = New List(Of BE.PermisoBase)
                        miPermisoHijo.listaHijos.AddRange(listaHijosHijos)

                        MiPermiso.listaHijos.Add(miPermisoHijo)
                    Else
                        MiPermiso.listaHijos.Add(hijo)
                    End If

                Next
                ls.Add(MiPermiso)
            Else
                ls.Add(Permiso)
            End If
        Next
        Return ls
    End Function

    Public Function leer_UnPermiso(filtro As BE.PermisoFiltro) As BE.PermisoBase

        Dim unPermiso As BE.PermisoBase = gestor_permiso.leer_UnPermiso(filtro)
        Dim MiPermiso As New BE.PermisoCompuesto
        Dim miPermisoHijo As New BE.PermisoCompuesto
        If Not unPermiso.esAccion Then
            Dim _permisoFiltro As New BE.PermisoFiltro
            _permisoFiltro.Nombre = unPermiso.Nombre

            Dim listaHijos As List(Of BE.PermisoBase) = gestor_permiso.ListarHijos(_permisoFiltro)
            MiPermiso = unPermiso

            MiPermiso.listaHijos = New List(Of BE.PermisoBase)
            'DMS - FIX - revisar como agrega los hijos ya que antes estaba todo en la misma clase
            For Each hijo In listaHijos
                If Not hijo.esAccion Then
                    _permisoFiltro.Nombre = hijo.Nombre
                    Dim listaHijosHijos As List(Of BE.PermisoBase) = gestor_permiso.ListarHijos(_permisoFiltro)

                    miPermisoHijo = hijo
                    miPermisoHijo.listaHijos = New List(Of BE.PermisoBase)
                    miPermisoHijo.listaHijos.AddRange(listaHijosHijos)

                    MiPermiso.listaHijos.Add(miPermisoHijo)
                Else
                    MiPermiso.listaHijos.Add(hijo)
                End If

            Next
            Return MiPermiso
        Else
            Return unPermiso
        End If


    End Function
    Public Function eliminar_permiso(perm As String) As Boolean
        Dim i As Integer = gestor_permiso.eliminar_permiso(perm)
        If i > 0 Then
            ActualizarSum()
            'Se escribio correctamente
            Return True
        End If
        Return False
    End Function
    Public Sub ActualizarSum()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_permiso"
        midvv.tabla_DVV = recuperarSumDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumDVH() As String
        Dim ls As List(Of BE.PermisoBase) = leer_permiso()
        sumaDVH = ""
        For Each registro As BE.PermisoBase In ls
            sumaDVH = sumaDVH + registro.Nombre.ToString
        Next
        Return sumaDVH.ToString + "t_permiso"
    End Function

    Public Sub ActualizarSumPP()
        Dim midvv As New BE.DVV
        midvv.idtabla = "t_permiso_permiso"
        midvv.tabla_DVV = recuperarSumPPDVH()
        _gestorDVV.escribir_DVV(midvv)
    End Sub

    Public Function recuperarSumPPDVH() As String
        Dim ls As List(Of BE.PermisoPermiso) = leer_permiso_permiso()
        sumaDVH = ""
        For Each registro As BE.PermisoPermiso In ls
            sumaDVH = sumaDVH + registro.codigoPadre.ToString
        Next
        Return sumaDVH.ToString + "t_permiso_permiso"
    End Function
End Class