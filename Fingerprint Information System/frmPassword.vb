﻿Imports DevComponents.DotNetBar

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Requests

Public Class frmPassword

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential
    Public CredentialPath As String
    Public JsonPath As String


    Private Sub frmPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtPassword1.UseSystemPasswordChar = True
        Me.txtPassword2.UseSystemPasswordChar = True
        InitializeComponents()

        CredentialPath = strAppUserPath & "\GoogleDriveAuthentication"
        JsonPath = CredentialPath & "\FISServiceAccount.json"

        If Not FileIO.FileSystem.FileExists(JsonPath) Then 'copy from application folder
            My.Computer.FileSystem.CreateDirectory(CredentialPath)
            FileSystem.FileCopy(strAppPath & "\FISServiceAccount.json", CredentialPath & "\FISServiceAccount.json")
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
            If cnt = 0 Then
                Dim encryptedpassword As String = EncryptText(uid.SubItems(1).Text)
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
                Dim pwd = Results.Files(0).Description
                bgwSetPassword.ReportProgress(100, pwd)
            End If

        Catch ex As Exception
            bgwSetPassword.ReportProgress(100, ex)
        End Try
    End Sub

    Private Sub bgwSetPassword_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSetPassword.ProgressChanged

        If TypeOf e.UserState Is String Then
            If e.UserState <> "" Then
                MessageBoxEx.Show("User ID already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtUserID.Focus()
            End If
        End If

        If TypeOf e.UserState Is Boolean Then
            If e.UserState = True Then
                frmMainInterface.ShowDesktopAlert("New user created.")
                InitializeComponents()
            End If
        End If

        If TypeOf e.UserState Is Exception Then
            ShowErrorMessage(e.UserState)
        End If
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
        If btnLogin.Text = "Login" Then LoginUser()
        If btnLogin.Text = "Save" Then SaveUser()
    End Sub

    Private Sub LoginUser()
        Try
            Me.Cursor = Cursors.WaitCursor
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
                Else
                    blUserAuthenticated = True
                    Me.Close()
                End If
            End If
        End If

        If TypeOf e.UserState Is Exception Then
            ShowErrorMessage(e.UserState)
            ' MessageBoxEx.Show("Unable to get user authentication.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Me.Cursor = Cursors.Default
    End Sub


#End Region

    Private Sub InitializeComponents()
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
        Me.txtUserID.Focus()
    End Sub

   
End Class