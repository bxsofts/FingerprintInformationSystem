Public Class frmNewVersionFeatures

    Private Sub frmUpdateAlert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.RichTextBoxEx1.LoadFile(strAppUserPath & "\NewVersionFeatures.rtf")
            Me.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class