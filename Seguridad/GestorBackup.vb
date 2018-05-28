
Public Class GestorBackup

    Dim gestor_backup As New DAL.MapeadorBackup
    Public Function escribir_backup(bkup As BE.BackUp) As Boolean
        Dim i As Integer = gestor_backup.escribir_backup(bkup)
        If i <> 0 Then
            'Se escribio correctamente
            Return True
        End If
        Return False
    End Function

    Public Function escribir_restore(bkup As BE.BackUp) As Boolean
        Dim i As Integer = gestor_backup.escribir_restore(bkup)
        If i <> 0 Then
            'Se escribio correctamente
            Return True
        End If
        Return False
    End Function

End Class
