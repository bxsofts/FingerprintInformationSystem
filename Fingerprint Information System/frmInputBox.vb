Public Class frmInputBox
    Public ButtonClicked As String




    Private Sub frmInputBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Activated
        On Error Resume Next
        
        Me.txtInputBox.Focus()
        Me.txtInputBox.SelectionLength = 0
        ButtonClicked = "Cancel"
    End Sub
    Public Sub SetTitleandMessage(ByVal Title As String, ByVal Message As String, ByVal PasswordChar As Boolean, Optional ByVal DefaultValue As String = "")
        On Error Resume Next
        Me.Text = Title
        Me.TitleText = "<b>" & Title & "</b>"
        Me.lblMessage.Text = Message
        Me.txtInputBox.Text = DefaultValue
        Me.txtInputBox.UseSystemPasswordChar = PasswordChar
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        On Error Resume Next
        ButtonClicked = "OK"
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        On Error Resume Next
        ButtonClicked = "Cancel"
        Me.Close()
    End Sub
End Class