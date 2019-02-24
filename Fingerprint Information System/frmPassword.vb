Imports DevComponents.DotNetBar

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Requests

Public Class frmPassword

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential
   
    Private Sub frmPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CircularProgress1.IsRunning = False
            CircularProgress1.ProgressText = ""
            CircularProgress1.ProgressBarType = eCircularProgressType.Dot
            CircularProgress1.Visible = False
            CircularProgress1.ProgressColor = GetProgressColor()

        Me.txtPassword1.UseSystemPasswordChar = True
        Me.txtPassword2.UseSystemPasswordChar = True

            InitializeComponents()

            If blAuthenticatePasswordChange Then
                Me.txtUserID.Text = oAuthUserID
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.lblNewUser.Visible = False
                Me.txtUserID.Enabled = False
                Me.txtPassword1.Focus()
                Me.btnLogin.Text = "Authenticate"
            End If

            If blChangeAndUpdatePassword Then
                Me.txtUserID.Text = oAuthUserID
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword2.Visible = True
                Me.lblPassword2.Visible = True
                Me.lblNewUser.Visible = False
                Me.txtUserID.Enabled = False
                Me.txtPassword1.Focus()
                Me.btnLogin.Text = "Save"
            End If

            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'copy from application folder
                My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
                FileSystem.FileCopy(strAppPath & "\FISServiceAccount.json", CredentialFilePath & "\FISServiceAccount.json")
            End If

            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'if copy failed
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("Authentication File is missing. Please re-install the application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            Dim Scopes As String() = {DriveService.Scope.Drive}
            FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub


#Region "NEW USER"
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

            Me.Cursor = Cursors.WaitCursor

            CircularProgress1.Visible = True
            CircularProgress1.IsRunning = True

            Dim uid As New ListViewItem(Me.txtUserID.Text.Trim)
            uid.SubItems.Add(Me.txtPassword1.Text)

            bgwSetPassword.RunWorkerAsync(uid)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwSetPassword_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSetPassword.DoWork
        Try

            Dim parentid As String = ""
            Dim List = FISService.Files.List()
            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserPasswordFolderName & "'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwSetPassword.ReportProgress(100, "")
            Else
                parentid = Results.Files(0).Id
            End If

            Dim uid As ListViewItem = e.Argument
            Dim encrypteduid As String = EncryptText(uid.Text)


            List.Q = "name = '" & encrypteduid & "' and trashed = false and '" & parentid & "' in parents" ' list files in parent folder. 
            List.Fields = "files(id, name, description)"
            List.OrderBy = "folder, name" 'sorting order

            Results = List.Execute

            cnt = Results.Files.Count
            Dim encryptedpassword As String = EncryptText(uid.SubItems(1).Text)
            If cnt = 0 Then 'create new user
                Dim NewFile = New Google.Apis.Drive.v3.Data.File
                Dim parentlist As New List(Of String)
                parentlist.Add(parentid) 'parent forlder

                NewFile.Parents = parentlist
                NewFile.Name = encrypteduid
                NewFile.MimeType = "file/.fispwd"
                NewFile.Description = encryptedpassword
                NewFile = FISService.Files.Create(NewFile).Execute
                bgwSetPassword.ReportProgress(100, True)
            Else
                If blChangeAndUpdatePassword Then
                    Dim id As String = Results.Files(0).Id
                    Dim request As New Google.Apis.Drive.v3.Data.File
                    request.Description = encryptedpassword
                    FISService.Files.Update(request, id).Execute()
                    bgwSetPassword.ReportProgress(100, "Password updated.")
                Else
                    bgwSetPassword.ReportProgress(100, "User ID already exists.")
                End If

            End If

        Catch ex As Exception
            bgwSetPassword.ReportProgress(100, ex)
        End Try
    End Sub

    Private Sub bgwSetPassword_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSetPassword.ProgressChanged

        If TypeOf e.UserState Is String Then
            MessageBoxEx.Show(e.UserState, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtUserID.Focus()
            If blChangeAndUpdatePassword Then
                blChangeAndUpdatePassword = False
                Me.Dispose()
                Me.Close()
            End If
        End If

        If TypeOf e.UserState Is Boolean Then
            If e.UserState = True Then
                MessageBoxEx.Show("New user created.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InitializeComponents()
            End If
        End If

        If TypeOf e.UserState Is Exception Then
            ShowErrorMessage(e.UserState)
        End If
    End Sub

    Private Sub bgwSetPassword_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSetPassword.RunWorkerCompleted
        CircularProgress1.Visible = False
        CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
    End Sub
#End Region


#Region "LOGIN USER"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Me.txtUserID.Text.Trim = "" Then
            MessageBoxEx.Show("Enter User ID.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUserID.Focus()
            Exit Sub
        End If
        If Me.txtPassword1.Text = "" Then
            MessageBoxEx.Show("Enter Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword1.Focus()
            Exit Sub
        End If

        If btnLogin.Text = "Login" Or btnLogin.Text = "Authenticate" Then LoginUser()
        If btnLogin.Text = "Save" Then SaveUser()
    End Sub

    Private Sub LoginUser()
        Try
            Me.Cursor = Cursors.WaitCursor

            CircularProgress1.Visible = True
            CircularProgress1.IsRunning = True

            bgwGetPassword.RunWorkerAsync(Me.txtUserID.Text.Trim)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub bgwGetPassword_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwGetPassword.DoWork

        Try

            Dim parentid As String = ""
            Dim List = FISService.Files.List()
            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserPasswordFolderName & "'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwGetPassword.ReportProgress(100, "")
                Exit Sub
            Else
                parentid = Results.Files(0).Id
            End If

            Dim encrypteduid As String = EncryptText(e.Argument)
            List.Q = "name = '" & encrypteduid & "' and trashed = false and '" & parentid & "' in parents" ' list files in parent folder. 
            List.Fields = "files(id, name, description)"
            List.OrderBy = "folder, name" 'sorting order

            Results = List.Execute

            cnt = Results.Files.Count
            If cnt = 0 Then
                bgwGetPassword.ReportProgress(100, "")
            Else
                Dim pwd = Results.Files(0).Description
                bgwGetPassword.ReportProgress(100, pwd)
            End If

        Catch ex As Exception
            bgwGetPassword.ReportProgress(100, ex)
        End Try
    End Sub

    Private Sub bgwGetPassword_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwGetPassword.ProgressChanged

        If TypeOf e.UserState Is String Then
            If e.UserState = "" Then
                MessageBoxEx.Show("User ID does not exist.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUserID.Text = ""
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtUserID.Focus()
            Else
                If e.UserState <> EncryptText(Me.txtPassword1.Text) Then
                    MessageBoxEx.Show("Incorrect password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtPassword1.Text = ""
                    Me.txtPassword1.Focus()
                    If blAuthenticatePasswordChange Then
                        blUserAuthenticated = False
                        Me.Close()
                    End If
                Else
                    oAuthUserID = Me.txtUserID.Text.Trim
                    blUserAuthenticated = True
                    Me.Close()
                End If
            End If
        End If

        If TypeOf e.UserState Is Exception Then
            ShowErrorMessage(e.UserState)
            ' MessageBoxEx.Show("Unable to get user authentication.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub bgwGetPassword_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwGetPassword.RunWorkerCompleted
        CircularProgress1.Visible = False
        CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "CHANGE PASSWORD"


#End Region

    Private Sub InitializeComponents()
        Me.txtUserID.Enabled = True
        Me.txtUserID.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""
        Me.btnLogin.Text = "Login"
        Me.lblPassword2.Visible = False
        Me.txtPassword2.Visible = False
        Me.lblNewUser.Visible = True
        blUserAuthenticated = False
        Me.txtUserID.Focus()
    End Sub



    Private Sub frmPassword_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If blChangeAndUpdatePassword Or blAuthenticatePasswordChange Then
            Me.txtPassword1.Focus()
        Else
            Me.txtUserID.Focus()
        End If

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class