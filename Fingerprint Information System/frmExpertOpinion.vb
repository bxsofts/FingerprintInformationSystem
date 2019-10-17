Public Class frmExpertOpinion

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        blGenerateOpinion = True
        Me.Close()
    End Sub

    Private Sub frmExpertOpinion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        blGenerateOpinion = False
        Me.txtCPD.Focus()
        Me.txtCPD.Select(Me.txtCPD.Text.Length, 0)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        blGenerateOpinion = False
        Me.Close()
    End Sub
End Class