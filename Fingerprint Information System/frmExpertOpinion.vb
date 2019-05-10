Public Class frmExpertOpinion

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmExpertOpinion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtCPD.Focus()
        Me.txtCPD.Select(Me.txtCPD.Text.Length, 0)
    End Sub
End Class