Imports DevComponents.DotNetBar


Public Class frmWeeklyDiaryAuthentication

    Private Sub frmWeeklyDiaryAuthentication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponents()
        Me.txtPassword.UseSystemPasswordChar = True
        Me.txtPassword1.UseSystemPasswordChar = True
        Me.txtPassword2.UseSystemPasswordChar = True
    End Sub

    Private Sub InitializeComponents()
        Me.Size = New Size(Me.Width, 136)

        Me.txtPEN.Text = ""
        Me.txtName.Text = ""
        Me.txtPassword.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""
        Me.btnLogin.Text = "Login"

        Me.txtPassword.Visible = True
        Me.lblPassword.Visible = True
        Me.txtName.Visible = False
        Me.lblName.Visible = False
        Me.lblPassword1.Visible = False
        Me.txtPassword1.Visible = False
        Me.lblPassword2.Visible = False
        Me.txtPassword2.Visible = False
       
        Me.lblNewUser.Visible = True
        Me.txtPEN.Focus()
    End Sub

    Private Sub lblNewUser_Click(sender As Object, e As EventArgs) Handles lblNewUser.Click
        Me.Size = New Size(Me.Width, 166)

        Me.txtPEN.Text = ""
        Me.txtPassword.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""

        Me.txtPassword.Visible = False
        Me.lblPassword.Visible = False

        Me.txtName.Visible = True
        Me.lblName.Visible = True
        Me.lblPassword1.Visible = True
        Me.txtPassword1.Visible = True
        Me.lblPassword2.Visible = True
        Me.txtPassword2.Visible = True

        Me.lblNewUser.Visible = False
        Me.btnLogin.Text = "Save"
        txtPEN.Focus()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If Me.txtPEN.Text.Trim = "" Then
            MessageBoxEx.Show("Enter PEN Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPEN.Focus()
            Exit Sub
        End If

        If btnLogin.Text = "Login" Then LoginUser()
        If btnLogin.Text = "Save" Then SaveUser()

    End Sub
    Private Sub SaveUser()
        Try
            If Me.txtName.Text = "" Then
                MessageBoxEx.Show("Enter Name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtName.Focus()
                Exit Sub
            End If
            If Me.txtPassword1.Text = "" Then
                MessageBoxEx.Show("Enter valid password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword1.Focus()
                Exit Sub
            End If
            If Me.txtPassword1.Text <> Me.txtPassword2.Text Then
                MessageBoxEx.Show("Passwords do not match.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword1.Focus()
                Exit Sub
            End If

            Dim pen As String = Me.txtPEN.Text.Trim
            Dim destfile As String = SuggestedLocation & "\WeeklyDiary\" & pen & ".mdb"

            If My.Computer.FileSystem.FileExists(destfile) Then
                MessageBoxEx.Show("User with PEN " & pen & " already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim sourcefile As String = strAppUserPath & "\Database\WeeklyDiary.mdb"

            Me.Cursor = Cursors.WaitCursor
            My.Computer.FileSystem.CopyFile(sourcefile, destfile, False) ', FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
            Application.DoEvents()

            If Not My.Computer.FileSystem.FileExists(destfile) Then
                MessageBoxEx.Show("Error creating new user. Please try again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destfile

            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
            Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
            Me.AuthenticationTableAdapter1.Connection.Open()

            If Me.PersonalDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.PersonalDetailsTableAdapter1.Connection.Close()
            Me.PersonalDetailsTableAdapter1.Connection.ConnectionString = wdConString
            Me.PersonalDetailsTableAdapter1.Connection.Open()

            Me.AuthenticationTableAdapter1.Insert(Me.txtPassword1.Text.Trim)
            Me.PersonalDetailsTableAdapter1.InsertQuery(pen, Me.txtName.Text.Trim, "")

            Me.Cursor = Cursors.Default
            InitializeComponents()
            MessageBoxEx.Show("New user created.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub LoginUser()

        If Me.txtPassword.Text = "" Then
            MessageBoxEx.Show("Enter Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Exit Sub
        End If

        Try
            Dim pen As String = Me.txtPEN.Text.Trim
            Dim destfile As String = SuggestedLocation & "\WeeklyDiary\" & pen & ".mdb"

            If Not My.Computer.FileSystem.FileExists(destfile) Then
                MessageBoxEx.Show("User with PEN " & pen & " not found.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destfile

            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
            Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
            Me.AuthenticationTableAdapter1.Connection.Open()

            Dim pwd As String = Me.AuthenticationTableAdapter1.GetPasswordQuery()

            If pwd <> Me.txtPassword.Text.Trim Then
                MessageBoxEx.Show("Incorrect Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.Default
            InitializeComponents()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub

  
End Class