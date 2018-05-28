Public Class MapeadorBackup
    Dim da As New DAL.Accesos
    Public Function escribir_backup(bkup As BE.BackUp) As Integer
        Dim hs As New Hashtable
        hs.Add("@directorio", bkup.directorio)
        hs.Add("@nombre", bkup.nombre)
        hs.Add("@fechaHora", bkup.fechaHora)

        Dim i As Integer = da.Escribir("backup_escribir", hs)
        Return i

    End Function

    Public Function escribir_restore(bkup As BE.BackUp) As Integer
        Dim hs As New Hashtable
        hs.Add("@directorio", bkup.directorio)
        hs.Add("@nombre", bkup.nombre)
        hs.Add("@fechaHora", bkup.fechaHora)

        Dim i As Integer = da.Escribir("backup_restore", hs)
        Return i

    End Function

End Class
