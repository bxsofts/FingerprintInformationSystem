Public Class frmUpdateAlert

    Private Sub frmUpdateAlert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            blDownloadUpdate = False
            Me.RichTextBoxEx1.LoadFile(strAppUserPath & "\VersionHistory.rtf")
            Me.BringToFront()
        Catch ex As Exception
            Me.RichTextBoxEx1.Text = "New Version " & InstallerFileVersion & " Available"
        End Try
      
    End Sub

    Private Sub btnRemindLate_Click(sender As Object, e As EventArgs) Handles btnRemindLater.Click
        blDownloadUpdate = False
        Me.Close()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        blDownloadUpdate = True
        Me.Close()
    End Sub
End Class