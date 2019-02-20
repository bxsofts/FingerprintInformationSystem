Public Class frmPassword

    Private Sub frmPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtPassword1.UseSystemPasswordChar = True
        Me.txtPassword2.UseSystemPasswordChar = True
    End Sub
End Class