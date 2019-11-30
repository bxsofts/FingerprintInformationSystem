Imports DevComponents.DotNetBar


Public Class frmWeeklyDiaryAuthentication

    Private Sub frmWeeklyDiaryAuthentication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponents()
        Me.txtPassword1.UseSystemPasswordChar = True
        Me.txtPassword2.UseSystemPasswordChar = True

    End Sub

    Private Sub InitializeComponents()
        Me.txtUserID.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""
        Me.btnLogin.Text = "Login"
        Me.lblPassword2.Visible = False
        Me.txtPassword2.Visible = False
        Me.lblNewUser.Visible = True
        Me.txtUserID.Focus()
    End Sub

    Private Sub lblNewUser_Click(sender As Object, e As EventArgs) Handles lblNewUser.Click
        Me.txtUserID.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""
        Me.lblPassword2.Visible = True
        Me.txtPassword2.Visible = True
        Me.lblNewUser.Visible = False
        Me.btnLogin.Text = "Save"
        txtUserID.Focus()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Me.txtUserID.Text.Trim = "" Then
            MessageBoxEx.Show("Enter PEN Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUserID.Focus()
            Exit Sub
        End If
        If Me.txtPassword1.Text = "" Then
            MessageBoxEx.Show("Enter Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword1.Focus()
            Exit Sub
        End If

        ' If btnLogin.Text = "Login" Then LoginUser()
        If btnLogin.Text = "Save" Then SaveUser()

    End Sub
    Private Sub SaveUser()
        Try
            If Me.txtPassword1.Text = "" Then
                MessageBoxEx.Show("Invalid password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword1.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            If Me.txtPassword1.Text <> Me.txtPassword2.Text Then
                MessageBoxEx.Show("Passwords do not match.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword1.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim pen As String = Me.txtUserID.Text.Trim
            Dim destfile As String = SuggestedLocation & "\WeeklyDiary\" & pen & ".mdb"

            If My.Computer.FileSystem.FileExists(destfile) Then
                MessageBoxEx.Show("User with PEN " & pen & " already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim sourcefile As String = strAppUserPath & "\Database\WeeklyDiary.mdb"

            Me.Cursor = Cursors.WaitCursor
            My.Computer.FileSystem.CopyFile(sourcefile, destfile, False) ', FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
            Application.DoEvents()
            Dim wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destfile

            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
            Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
            Me.AuthenticationTableAdapter1.Connection.Open()
            Me.AuthenticationTableAdapter1.InsertQuery(Me.txtPassword1.Text.Trim)

            Me.Cursor = Cursors.Default
            InitializeComponents()
            MessageBoxEx.Show("New user created.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

  
End Class