Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration


Public Class Accesos

    Dim cn As SqlConnection

    Private Sub Abrir()
        If cn.State <> ConnectionState.Open Then
            cn.Open()
        End If
    End Sub
    Private Sub Cerrar()
        cn.Close()
    End Sub
    Public Function Escribir(nombre As String, hs As Hashtable) As Integer
        DefinirConexion(nombre)
        Dim cm As New SqlCommand
        cm.CommandType = CommandType.StoredProcedure
        cm.CommandText = nombre

        For n = 0 To hs.Values.Count - 1
            cm.Parameters.AddWithValue(hs.Keys(n), hs.Values(n))
        Next

        cm.Connection = cn

        Abrir()

        Dim i As Integer = cm.ExecuteNonQuery()

        Cerrar()

        Return i
    End Function

    Public Function Leer(nombre As String, Optional hs As Hashtable = Nothing) As DataTable
        DefinirConexion(nombre)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter
        da.SelectCommand = New SqlCommand(nombre, cn)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.CommandText = nombre

        If hs IsNot Nothing Then
            For n = 0 To hs.Values.Count - 1
                da.SelectCommand.Parameters.AddWithValue(hs.Keys(n), hs.Values(n))
            Next
        End If

        da.SelectCommand.Connection = cn
        Abrir()
        da.Fill(dt)
        Cerrar()
        Return dt
    End Function

    Sub DefinirConexion(nombre As String)
        If nombre = "backup_restore" Then
            cn = New SqlConnection(ConfigurationManager.ConnectionStrings("StringRestore").ToString())
        Else
            cn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConString").ToString())
        End If
    End Sub

End Class
