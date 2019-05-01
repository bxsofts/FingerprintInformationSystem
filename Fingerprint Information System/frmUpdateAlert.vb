Public Class frmUpdateAlert

    Private Sub frmUpdateAlert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            blDownloadUpdate = False
            Me.btnDownloadUpdate.Visible = False
            Me.btnRemindLater.Visible = False

            If blNewVersionFound Then
                Me.RichTextBoxEx1.LoadFile(strAppUserPath & "\NewVersionAvailable.rtf")
                Me.btnDownloadUpdate.Visible = True
                Me.btnRemindLater.Text = "Remind Me Later"
                Me.btnRemindLater.Visible = True
            Else
                Me.Text = "New Version Features"
                Me.TitleText = "<b>New Version Features</b>"
                Me.RichTextBoxEx1.LoadFile(strAppUserPath & "\NewVersionFeatures.rtf")
                Me.btnRemindLater.Text = "OK"
                Me.btnRemindLater.Visible = True
            End If

            Me.BringToFront()
        Catch ex As Exception
            Me.RichTextBoxEx1.Text = "New Version " & InstallerFileVersion & " Available"
        End Try
      
    End Sub

    Private Sub btnRemindLater_Click(sender As Object, e As EventArgs) Handles btnRemindLater.Click
        blDownloadUpdate = False
        Me.Close()
    End Sub

    Private Sub btnDownloadUpdate_Click(sender As Object, e As EventArgs) Handles btnDownloadUpdate.Click
        blDownloadUpdate = True
        Me.Close()
    End Sub
End Class