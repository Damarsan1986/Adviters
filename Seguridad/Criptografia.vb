Imports System
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.Cryptography

Public Class Criptografia

    Private Shared instancia As Criptografia = New Criptografia()
    Public Shared Function ObtenerInstancia() As Criptografia
        Return instancia
    End Function


    Public Function MD5(ByRef strText As String) As String
        Dim MD5Service As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim Bytes() As Byte = MD5Service.ComputeHash(System.Text.Encoding.ASCII.GetBytes(strText))
        Dim s As String = ""
        For Each by As Byte In Bytes
            s += by.ToString("x2")
        Next
        Return s
    End Function
    Public Function CompareHashMD5(ByVal value As String, ByVal hash As String) As Boolean

        Return (hash.Equals(Me.MD5(value)))
    End Function

End Class
