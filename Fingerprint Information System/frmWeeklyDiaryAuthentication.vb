Imports DevComponents.DotNetBar

Imports System.Threading
Imports System.Threading.Tasks

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Public Class frmWeeklyDiaryAuthentication

    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus
    Public dFileSize As Long

    Private Sub frmWeeklyDiaryAuthentication_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()

            If Me.PersonalDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.PersonalDetailsTableAdapter1.Connection.Close()
            GC.Collect()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub frmWeeklyDiaryAuthentication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CircularProgress1.Visible = False
        InitializeComponents()
        Me.txtPassword.UseSystemPasswordChar = True
        Me.txtPassword1.UseSystemPasswordChar = True
        Me.txtPassword2.UseSystemPasswordChar = True
    End Sub

    Private Sub InitializeComponents()

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
        Me.lblDownloadDatabase.Visible = True
        Me.ActiveControl = Me.txtPEN
    End Sub

    Private Sub lblNewUser_Click(sender As Object, e As EventArgs) Handles lblNewUser.Click

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
        Me.lblDownloadDatabase.Visible = False

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
            If Me.txtName.Text.Trim = "" Then
                MessageBoxEx.Show("Enter Name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtName.Focus()
                Exit Sub
            End If

            If Me.txtPassword1.Text.Trim = "" Then
                MessageBoxEx.Show("Enter valid password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword1.Focus()
                Exit Sub
            End If

            If Me.txtPassword1.Text.Trim <> Me.txtPassword2.Text.Trim Then
                MessageBoxEx.Show("Passwords do not match.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword1.Text = ""
                Me.txtPassword2.Text = ""
                Me.txtPassword1.Focus()
                Exit Sub
            End If

            Dim pen As String = Me.txtPEN.Text.Trim
            Dim destfile As String = SuggestedLocation & "\Weekly Diary\" & pen & ".mdb"

            If My.Computer.FileSystem.FileExists(destfile) Then
                MessageBoxEx.Show("User with PEN " & pen & " already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim sourcefile As String = strAppUserPath & "\Database\WeeklyDiary.mdb"

            Me.Cursor = Cursors.WaitCursor
            My.Computer.FileSystem.CopyFile(sourcefile, destfile, False)
            Application.DoEvents()

            If Not My.Computer.FileSystem.FileExists(destfile) Then
                MessageBoxEx.Show("Error creating new user and database. Please try again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Dim dgvr As WeeklyDiaryDataSet.PersonalDetailsRow = Me.WeeklyDiaryDataSet1.PersonalDetails.NewPersonalDetailsRow

            With dgvr
                .PEN = pen
                .OfficerName = Me.txtName.Text.Trim
                .Remarks = ""
            End With

            WeeklyDiaryDataSet1.PersonalDetails.Rows.Add(dgvr)
            PersonalDetailsTableAdapter1.Update(WeeklyDiaryDataSet1.PersonalDetails)
            ' Me.PersonalDetailsTableAdapter1.InsertQuery(pen, Me.txtName.Text.Trim, "")

            Me.Cursor = Cursors.Default
            InitializeComponents()
            MessageBoxEx.Show("New user and database created.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub LoginUser()

        If Me.txtPassword.Text.Trim = "" Then
            MessageBoxEx.Show("Enter Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Exit Sub
        End If

        Try
            Dim pen As String = Me.txtPEN.Text.Trim
            Dim wdFile As String = SuggestedLocation & "\Weekly Diary\" & pen & ".mdb"

            If Not My.Computer.FileSystem.FileExists(wdFile) Then
                MessageBoxEx.Show("User with PEN " & pen & " not found. Please create a new database or download existing database and login again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & wdFile

            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
            Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
            Me.AuthenticationTableAdapter1.Connection.Open()

            If Me.PersonalDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.PersonalDetailsTableAdapter1.Connection.Close()
            Me.PersonalDetailsTableAdapter1.Connection.ConnectionString = wdConString
            Me.PersonalDetailsTableAdapter1.Connection.Open()

            Dim pwd As String = Me.AuthenticationTableAdapter1.GetPasswordQuery()

            If pwd <> Me.txtPassword.Text.Trim Then
                MessageBoxEx.Show("Incorrect Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.Default
            blPENVerified = True
            wdDatabase = wdFile
            '  wdPEN = pen
            Me.Close()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub


    Private Sub lblDownloadDatabase_Click(sender As Object, e As EventArgs) Handles lblDownloadDatabase.Click
        Try
            Dim Pen As String = Me.txtPEN.Text.Trim

            If Not InternetAvailable() Then
                MessageBoxEx.Show("Cannot connect to server. Please check your Internet connection.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPEN.Focus()
                Exit Sub
            End If


            If Pen = "" Then
                MessageBoxEx.Show("Enter PEN Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPEN.Focus()
                Exit Sub
            End If


            Dim wdFile As String = SuggestedLocation & "\Weekly Diary\" & Pen & ".mdb"
            Dim wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & wdFile

            Dim localcount As Integer = 0

            If My.Computer.FileSystem.FileExists(wdFile) Then
                Dim r = MessageBoxEx.Show("Database for PEN " & Pen & " already exists. Do you want to overwrite it?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If r <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                Else

                    If Me.txtPassword.Text.Trim = "" Then
                        MessageBoxEx.Show("Enter Password to authenticate overwriting of database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtPassword.Focus()
                        Exit Sub
                    End If



                    If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
                    Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
                    Me.AuthenticationTableAdapter1.Connection.Open()

                    Dim pwd As String = Me.AuthenticationTableAdapter1.GetPasswordQuery()

                    If pwd <> Me.txtPassword.Text.Trim Then
                        MessageBoxEx.Show("Incorrect Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    If Me.WeeklyDiaryTableAdapter1.Connection.State = ConnectionState.Open Then Me.WeeklyDiaryTableAdapter1.Connection.Close()
                    Me.WeeklyDiaryTableAdapter1.Connection.ConnectionString = wdConString
                    Me.WeeklyDiaryTableAdapter1.Connection.Open()

                    localcount = Me.WeeklyDiaryTableAdapter1.ScalarQueryCount()
                End If
            End If

            Me.Cursor = Cursors.WaitCursor

           

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim wdFolderID As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                wdFolderID = ""
            Else
                wdFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & wdFolderID & "' in parents and name = '" & Me.txtPEN.Text.Trim & ".mdb'"
            List.Fields = "files(id, description)"
            Results = List.Execute

            cnt = Results.Files.Count

            If cnt = 0 Then
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("Weekly Diary file for the selected PEN not found in remote server.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim remotecount As Integer = 0

            Dim description As String = ""

            If cnt > 0 Then
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, " - ")
                Dim u = SplitText.GetUpperBound(0)

                If u = 0 Then
                    remotecount = Val(SplitText(0))
                End If

                If u > 0 Then
                    remotecount = Val(SplitText(1))
                End If
            End If

            If remotecount < localcount Then
                MessageBoxEx.Show("Local database has more records (" & localcount & ") than remote database (" & remotecount & "). Cannot replace database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            CircularProgress1.IsRunning = True
            CircularProgress1.ProgressColor = GetProgressColor()
            CircularProgress1.ProgressText = "0"
            CircularProgress1.Show()
            Me.bgwDownload.RunWorkerAsync(Me.txtPEN.Text.Trim & ".mdb")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownload.DoWork
        Try
            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim wdFileName As String = e.Argument.ToString
            Dim wdFileID As String = ""


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})


            Dim parentid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwDownload.ReportProgress(100, "Weekly Diary folder not found")
                Exit Sub
            Else
                parentid = Results.Files(0).Id
            End If


            List.Q = "name = '" & wdFileName & "' and trashed = false and '" & parentid & "' in parents"
            List.Fields = "files(name, id)"

            Results = List.Execute

            If Results.Files.Count > 0 Then
                wdFileID = Results.Files(0).Id
                Dim request = FISService.Files.Get(wdFileID)
                request.Fields = "size"
                Dim file = request.Execute

                dFileSize = file.Size

                Dim tempfile As String = My.Computer.FileSystem.GetTempFileName & ".mdb"

                Dim fStream = New System.IO.FileStream(tempfile, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                Dim mStream = New System.IO.MemoryStream

                Dim m = request.MediaDownloader
                m.ChunkSize = 256 * 1024

                AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

                request.DownloadWithStatus(mStream)

                If dDownloadStatus = DownloadStatus.Completed Then
                    mStream.WriteTo(fStream)
                End If

                fStream.Close()
                mStream.Close()

                My.Computer.FileSystem.CopyFile(tempfile, SuggestedLocation & "\Weekly Diary\" & wdFileName, True)
            Else
                bgwDownload.ReportProgress(100, "File not found")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        Dim percent = CInt((dBytesDownloaded / dFileSize) * 100)
        bgwDownload.ReportProgress(percent)
    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownload.ProgressChanged
        CircularProgress1.ProgressText = e.ProgressPercentage
       

        If e.UserState = "Weekly Diary folder not found" Then
            MessageBoxEx.Show("Weekly Diary folder not found in remote server. Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        If e.UserState = "File not found" Then
            MessageBoxEx.Show("Weekly Diary file for the selected PEN not found in remote server.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownload.RunWorkerCompleted

        Me.Cursor = Cursors.Default

        CircularProgress1.Visible = False

        If dDownloadStatus = DownloadStatus.Completed Then
            MessageBoxEx.Show("Weekly Diary file downloaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("Weekly Diary file download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

End Class