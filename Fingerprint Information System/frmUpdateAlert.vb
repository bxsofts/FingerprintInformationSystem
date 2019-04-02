Public Class frmUpdateAlert

    Private Sub frmUpdateAlert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBoxEx1.LoadFile(strAppUserPath & "\VersionHistory.rtf")
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub
End Class