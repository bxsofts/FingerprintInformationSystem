Public NotInheritable Class frmPleaseWait

    Private Sub frmPleaseWait_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub


    Private Sub frmPleaseWait_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 327
        Me.Height = 95
    End Sub
End Class
