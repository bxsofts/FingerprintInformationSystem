Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO

Imports DevComponents.DotNetBar

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests



Public Class frmOnlineBackup
    Private FISService As DriveService = New DriveService
    '  Dim FISUserCredential As UserCredential
    Dim FISAccountServiceCredential As GoogleCredential
    Dim BackupFolder As String
    Dim BackupFolderID As String
    Dim BackupPath As String = ""
    Public CredentialPath As String
    Public JsonPath As String
    Public NoFileFoundMessage As Boolean = False
    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus
    Public dFileSize As Long
    Dim dFormatedFileSize As String = ""
    Public uBytesUploaded As Long
    Public uUploadStatus As UploadStatus
    Public DownloadRestore As Boolean = False
    Public DownloadOpen As Boolean = False
    Public DownloadOnly As Boolean = False

    Dim FileOwner As String = ""
    Dim TotalFileSize As Long = 0

#Region "FORM LOAD EVENTS"

    Private Sub CreateService() Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        FileOwner = ShortOfficeName & "_" & ShortDistrictName
        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"

        If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
            My.Computer.FileSystem.CreateDirectory(BackupPath)
        End If

        BackupFolder = FileOwner

        BackupFolderID = ""
        Me.lblTotalFileSize.Text = ""
        Try

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

            ' For oauth2 auhentication only
            '  If Not FileIO.FileSystem.FileExists(CredentialPath & "\Google.Apis.Auth.OAuth2.Responses.TokenResponse-user") Then
            'MessageBoxEx.Show("The application will now open your browser. Please enter your gmail id and password to authenticate.", strAppName, 'MessageBoxButtons.OK, MessageBoxIcon.Information)
            '  End If 


            blUploadIsProgressing = False
            blDownloadIsProgressing = False
            blListIsLoading = False


            FetchFilesFromDrive(False)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region


#Region "CREATE SERVICE AND LOAD DATA"

    Private Sub FetchFilesFromDrive(ShowNoFileFoundMessage As Boolean)
        Me.listViewEx1.Items.Clear()
        listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
        listViewEx1.Sort()


        CircularProgress1.ProgressText = ""
        lblProgressStatus.Text = "Fetching Files from Google Drive..."
        CircularProgress1.IsRunning = True
        CircularProgress1.ProgressColor = GetProgressColor()
        CircularProgress1.ProgressBarType = eCircularProgressType.Donut
        CircularProgress1.Show()
        lblProgressStatus.Show()

        NoFileFoundMessage = ShowNoFileFoundMessage

        bgwService.RunWorkerAsync()
    End Sub

    Private Sub CreateServiceAndLoadData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwService.DoWork
        Try
            blListIsLoading = True

            Dim Scopes As String() = {DriveService.Scope.Drive}

            '  Dim fStream = New FileStream(JsonPath, FileMode.Open, FileAccess.Read) ' use fro oauth2 authentication
            '    FISUserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, "user", CancellationToken.None, New FileDataStore(CredentialPath, True)).Result

            '  FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISUserCredential, .ApplicationName = strAppName})

            FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchTopLevelOnly, "FingerPrintBackup*.mdb")

                Dim FileName = My.Computer.FileSystem.GetName(foundFile)

                Dim FullFilePath = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & FileName
                Dim filesize = CalculateFileSize(My.Computer.FileSystem.GetFileInfo(FullFilePath).Length)
                Dim Filedate As DateTime = DateTime.ParseExact(FileName.Replace("FingerPrintBackup-", "").Replace(".mdb", ""), BackupDateFormatString, culture)

                Dim item As ListViewItem = New ListViewItem(FileName) 'name
                item.SubItems.Add(Filedate.ToString("dd-MM-yyyy HH:mm:ss")) 'backuptime
                item.SubItems.Add("Downloaded File") 'fileid
                item.SubItems.Add(filesize) 'size
                item.SubItems.Add("Downloaded File") 'remarks
                item.ImageIndex = 1
                bgwService.ReportProgress(50, item)
            Next

            BackupFolderID = GetUserBackupFolderID()
            Dim List = FISService.Files.List()

            If BackupFolderID = "" Then
                BackupFolderID = CreateUserBackupFolder()
            End If

            '  Dim parentlist As New List(Of String)
            '  parentlist.Add(BackupFolderID)

            List.Q = "mimeType = 'database/mdb' and '" & BackupFolderID & "' in parents"
            ' List.Q = "mimeType = 'database/mdb'" ' list all files
            List.Fields = "nextPageToken, files(id, name, modifiedTime, size, description)"

            Dim Results = List.Execute
            TotalFileSize = 0

            For Each Result In Results.Files
                Dim item As ListViewItem = New ListViewItem(Result.Name) 'name
                Dim modifiedtime As DateTime = Result.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss")) 'backup time
                item.SubItems.Add(Result.Id) 'id
                TotalFileSize = TotalFileSize + Result.Size
                item.SubItems.Add(CalculateFileSize(Result.Size)) 'size
                item.SubItems.Add(Result.Description)

                item.ImageIndex = 0
                bgwService.ReportProgress(90, item)
            Next

        Catch ex As Exception
            blListIsLoading = False
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub CreateServiceBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwService.ProgressChanged

        If TypeOf e.UserState Is String Then
            lblProgressStatus.Text = e.UserState
        End If

        '  Me.CircularProgress1.ProgressText = e.ProgressPercentage

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If

    End Sub
    Private Sub bgServiceBackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwService.RunWorkerCompleted

        DisplayInformation()
        CircularProgress1.IsRunning = False
        CircularProgress1.ProgressText = ""
        lblProgressStatus.Text = ""
        CircularProgress1.Hide()
        lblProgressStatus.Hide()
        blListIsLoading = False

        Me.Cursor = Cursors.Default
        If listViewEx1.Items.Count > 0 Then
            Me.listViewEx1.Items(0).Selected = True
        End If
        If NoFileFoundMessage And listViewEx1.Items.Count = 0 Then
            frmMainInterface.ShowDesktopAlert("No online Backup Files were found.")
            NoFileFoundMessage = False
        End If

        If e.Error IsNot Nothing Then
            MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Me.lblTotalFileSize.Text = "Total Online File Size: " & CalculateFileSize(TotalFileSize)
    End Sub

    Private Sub RefreshBackupList() Handles btnRefresh.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        FetchFilesFromDrive(True)
    End Sub

#End Region


#Region "GOOGLE DRIVE ID AND FOLDER MANIPULATION"

    Private Function GetUserBackupFolderID() As String
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & BackupFolder & "'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                id = ""
            Else
                id = Results.Files(0).Id
            End If

            Return id
        Catch ex As Exception
            ' ShowErrorMessage(ex)
            Return ""
        End Try
    End Function

    Private Function CreateUserBackupFolder()
        Try
            Dim id As String = ""
            Dim body As New Google.Apis.Drive.v3.Data.File()
            Dim NewDirectory = New Google.Apis.Drive.v3.Data.File

            Dim parentlist As New List(Of String)
            Dim masterfolderid As String = GetMasterBackupFolderID()

            If masterfolderid = "" Then
                masterfolderid = CreateMasterBackupFolder()
            End If

            parentlist.Add(masterfolderid)

            body.Parents = parentlist
            body.Name = BackupFolder
            body.Description = ShortOfficeName & "-" & ShortDistrictName
            body.MimeType = "application/vnd.google-apps.folder"

            Dim request As FilesResource.CreateRequest = FISService.Files.Create(body)

            NewDirectory = request.Execute()
            id = NewDirectory.Id
            Return id
        Catch ex As Exception
            ' ShowErrorMessage(ex)
            Return ""
        End Try

    End Function

    Private Function CreateMasterBackupFolder() As String
        Try
            Dim id As String = ""
            Dim body As New Google.Apis.Drive.v3.Data.File()

            Dim NewDirectory = New Google.Apis.Drive.v3.Data.File

            body.Name = "FIS Backup"
            body.Description = "Admin"
            body.MimeType = "application/vnd.google-apps.folder"

            Dim request As FilesResource.CreateRequest = FISService.Files.Create(body)

            NewDirectory = request.Execute()
            id = NewDirectory.Id
            CreateSharing(id, "fingerprintinformationsystem@gmail.com")
            Return id
        Catch ex As Exception
            ' ShowErrorMessage(ex)
            Return ""
        End Try

    End Function

    Private Function GetMasterBackupFolderID() As String
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'FIS Backup'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                id = ""
            Else
                id = Results.Files(0).Id
            End If

            Return id
        Catch ex As Exception
            ' ShowErrorMessage(ex)
            Return ""
        End Try
    End Function

    Private Sub CreateSharing(fileid As String, email As String)
        Dim userPermission As Permission = New Permission
        userPermission.Type = "user"
        userPermission.Role = "writer"
        userPermission.EmailAddress = email
        Dim request = FISService.Permissions.Create(userPermission, fileid)
        request.Fields = "id"
        request.Execute()
    End Sub

#End Region


#Region "BACKUP DATABASE"


    Private Sub UploadBackup() Handles btnBackupDatabase.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        CircularProgress1.ProgressBarType = eCircularProgressType.Line
        CircularProgress1.ProgressText = "0"
        Me.CircularProgress1.Show()
        lblProgressStatus.Text = "Uploading File..."
        Me.lblProgressStatus.Show()
        Me.CircularProgress1.IsRunning = True

        bgwUpload.RunWorkerAsync()


    End Sub


    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpload.DoWork

        Try
            blUploadIsProgressing = True

            Dim BackupTime As Date = Now
            Dim d As String = Strings.Format(BackupTime, BackupDateFormatString)
            Dim sBackupTime = Strings.Format(BackupTime, "dd-MM-yyyy HH:mm:ss")
            Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"

            If BackupFolderID = "" Then
                BackupFolderID = CreateUserBackupFolder()
            End If

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = BackupFileName
            body.Description = FileOwner
            body.MimeType = "database/mdb"

            Dim parentlist As New List(Of String)
            parentlist.Add(BackupFolderID)
            body.Parents = parentlist


            Dim tmpFileName As String = My.Computer.FileSystem.GetTempFileName

            My.Computer.FileSystem.CopyFile(sDatabaseFile, tmpFileName, True)

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(tmpFileName)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            dFileSize = FileLen(tmpFileName)
            dFormatedFileSize = CalculateFileSize(dFileSize)

            Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
            UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
            AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

            UploadRequest.Fields = "id, name, size, modifiedTime"
            UploadRequest.Upload()


            If uUploadStatus = UploadStatus.Completed Then
                Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody
                Dim item As ListViewItem = New ListViewItem(BackupFileName)
                Dim modifiedtime As DateTime = file.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                item.SubItems.Add(file.Id)
                item.SubItems.Add(CalculateFileSize(file.Size))
                item.SubItems.Add(FileOwner)
                item.ImageIndex = 0
                bgwUpload.ReportProgress(100, item)
                TotalFileSize += file.Size
            End If

            Stream.Close()

        Catch ex As Exception
            blUploadIsProgressing = False
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Upload_ProgressChanged(Progress As IUploadProgress)

        Control.CheckForIllegalCrossThreadCalls = False

        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
        Dim percent = CInt((uBytesUploaded / dFileSize) * 100)
        bgwUpload.ReportProgress(percent, uBytesUploaded)
    End Sub

    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged

        CircularProgress1.ProgressText = e.ProgressPercentage
        lblProgressStatus.Text = CalculateFileSize(uBytesUploaded) & "/" & dFormatedFileSize
        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If

    End Sub


    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        CircularProgress1.Visible = False
        lblProgressStatus.Visible = False
        blUploadIsProgressing = False

        DisplayInformation()

        If uUploadStatus = UploadStatus.Completed Then
            Me.lblTotalFileSize.Text = "Total Online File Size: " & CalculateFileSize(TotalFileSize)
            MessageBoxEx.Show("File uploaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(0).Selected = True
            End If
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "DOWNLOAD FILE"

    Private Sub DownloadFileFromDrive()
        Try
            Dim args As DownloadArgs = New DownloadArgs

            args.ID = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            args.SelectedFileName = Me.listViewEx1.SelectedItems(0).Text
            args.DownloadFileName = BackupPath & "\" & args.SelectedFileName
            args.BackupDate = Me.listViewEx1.SelectedItems(0).SubItems(1).Text

            CircularProgress1.ProgressText = "0"
            lblProgressStatus.Text = "Downloading File..."
            CircularProgress1.IsRunning = True
            CircularProgress1.ProgressBarType = eCircularProgressType.Line
            CircularProgress1.Show()
            lblProgressStatus.Show()
            Me.Cursor = Cursors.WaitCursor

            bgwDownload.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub DownloadSelectedFile() Handles btnDownloadDatabase.Click
        Try

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "Downloaded File" Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Selected file is a local file. Please select an online file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor
            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            DownloadRestore = False
            DownloadOpen = False
            DownloadOnly = True

            DownloadFileFromDrive()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownload.DoWork
        Try
            blDownloadIsProgressing = True
            Dim args As DownloadArgs = e.Argument
            Dim request = FISService.Files.Get(args.ID)
            request.Fields = "size"
            Dim file = request.Execute

            dFileSize = file.Size
            dFormatedFileSize = CalculateFileSize(dFileSize)

            Dim fStream = New System.IO.FileStream(args.DownloadFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
            Dim mStream = New System.IO.MemoryStream

            Dim m = request.MediaDownloader
            m.ChunkSize = 256 * 1024

            AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged
            request.DownloadWithStatus(mStream)

            If dDownloadStatus = DownloadStatus.Completed Then
                mStream.WriteTo(fStream)

                Dim item As ListViewItem = New ListViewItem(args.SelectedFileName)
                item.SubItems.Add(args.BackupDate)
                item.SubItems.Add("Downloaded File")
                item.SubItems.Add(dFormatedFileSize)
                item.SubItems.Add("Downloaded File")
                item.ImageIndex = 1

                bgwDownload.ReportProgress(100, item)
            End If

            fStream.Close()
            mStream.Close()


        Catch ex As Exception
            blDownloadIsProgressing = False
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)
        Control.CheckForIllegalCrossThreadCalls = False

        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        Dim percent = CInt((dBytesDownloaded / dFileSize) * 100)
        bgwDownload.ReportProgress(percent, dBytesDownloaded)

    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownload.ProgressChanged
        If TypeOf e.UserState Is Long Then
            CircularProgress1.ProgressText = e.ProgressPercentage
            lblProgressStatus.Text = CalculateFileSize(dBytesDownloaded) & "/" & dFormatedFileSize
        End If

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If


    End Sub

    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownload.RunWorkerCompleted

        Me.Cursor = Cursors.Default
        CircularProgress1.Visible = False
        lblProgressStatus.Visible = False
        blDownloadIsProgressing = False

        If dDownloadStatus = DownloadStatus.Completed Then
            If DownloadOnly Then
                MessageBoxEx.Show("File downloaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If DownloadOpen Then
                Me.Cursor = Cursors.WaitCursor
                If My.Computer.FileSystem.FileExists(strBackupFile) Then
                    Shell("explorer.exe " & strBackupFile, AppWinStyle.MaximizedFocus)
                Else
                    MessageBoxEx.Show("Cannot open file. File is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Me.Cursor = Cursors.Default
            End If



            If DownloadRestore Then
                Me.Cursor = Cursors.WaitCursor
                DownloadRestore = False

                If My.Computer.FileSystem.FileExists(strBackupFile) Then
                    My.Computer.FileSystem.CopyFile(strBackupFile, sDatabaseFile, True)
                    boolRestored = True
                    Me.Close()
                    Exit Sub
                Else
                    MessageBoxEx.Show("Cannot restore. Backup file is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Me.Cursor = Cursors.Default
            End If
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        DownloadOnly = False
        DownloadRestore = False
        DownloadOpen = False
        DisplayInformation()

    End Sub

#End Region


#Region "RESTORE DATABASE"

    Private Sub RestoreSelectedFile() Handles btnRestoreDatabase.Click
        Try
            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Restoring the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.Yes Then

                strBackupFile = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
                Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text

                If id <> "Downloaded File" Then 'download and use

                    result = DevComponents.DotNetBar.MessageBoxEx.Show("The file will be downloaded and restored.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)

                    If result = Windows.Forms.DialogResult.Cancel Then
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    Me.Cursor = Cursors.WaitCursor

                    If InternetAvailable() = False Then
                        MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    DownloadRestore = True
                    DownloadFileFromDrive()
                Else
                    DownloadRestore = False
                    Me.Cursor = Cursors.WaitCursor
                    If My.Computer.FileSystem.FileExists(strBackupFile) Then
                        My.Computer.FileSystem.CopyFile(strBackupFile, sDatabaseFile, True)
                        boolRestored = True
                        Me.Close()
                        Exit Sub
                    Else
                        MessageBoxEx.Show("Cannot restore. Backup file is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Me.Cursor = Cursors.Default
                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            boolRestored = False
            Me.Cursor = Cursors.Default
        End Try


    End Sub

#End Region


#Region "OPEN FILE IN MS ACCESS"
    Private Sub OpenFileInMSAccess(sender As Object, e As EventArgs) Handles btnOpenFileMSAccess.Click, listViewEx1.DoubleClick
        Try

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            strBackupFile = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
            Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text

            If id <> "Downloaded File" Then 'download and use

                Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The file will be downloaded and opened in Microsoft Access.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)

                If result = Windows.Forms.DialogResult.Cancel Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Me.Cursor = Cursors.WaitCursor
                If InternetAvailable() = False Then
                    MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                DownloadOpen = True
                DownloadFileFromDrive()
            Else
                DownloadOpen = False
                Me.Cursor = Cursors.WaitCursor
                If My.Computer.FileSystem.FileExists(strBackupFile) Then
                    Shell("explorer.exe " & strBackupFile, AppWinStyle.MaximizedFocus)
                Else
                    MessageBoxEx.Show("Cannot open file. File is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Me.Cursor = Cursors.Default
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub btnOpenBackupLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBackupFolder.Click
        On Error Resume Next

        If Me.listViewEx1.SelectedItems.Count > 0 Then
            If Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "Downloaded File" Then
                Call Shell("explorer.exe /select," & BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text, AppWinStyle.NormalFocus)
                Exit Sub
            End If
        End If

        If Not FileIO.FileSystem.DirectoryExists(BackupPath) Then
            FileIO.FileSystem.CreateDirectory(BackupPath)
        End If

        Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
    End Sub

#End Region


#Region "REMOVE FILE"
    Private Sub RemoveBackupFileFromDrive() Handles btnRemoveBackupFile.Click
        Try

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to remove the selected backup file?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim SelectedFileName As String = Me.listViewEx1.SelectedItems(0).Text
            Dim SelectedFile = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
            Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            Dim SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index

            If id = "Downloaded File" Then 'delete local file
                My.Computer.FileSystem.DeleteFile(SelectedFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                Me.listViewEx1.SelectedItems(0).Remove()
                Application.DoEvents()
                frmMainInterface.ShowDesktopAlert("Selected backup file deleted to the Recycle Bin.")
            Else 'remove online file

                If InternetAvailable() = False Then
                    MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Dim request = FISService.Files.Get(id)
                request.Fields = "size"
                Dim file = request.Execute

                Dim DeleteRequest = FISService.Files.Delete(id)
                DeleteRequest.Execute()
                TotalFileSize -= file.Size
                Me.lblTotalFileSize.Text = "Total Online File Size: " & CalculateFileSize(TotalFileSize)
                Me.listViewEx1.SelectedItems(0).Remove()
                Application.DoEvents()
                frmMainInterface.ShowDesktopAlert("Selected backup file deleted from Google Drive.")
            End If

            Me.Cursor = Cursors.Default

            SelectNextItem(SelectedFileIndex)

            DisplayInformation()
            ' GetDriveStorageDetails()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub SelectNextItem(SelectedFileIndex)
        On Error Resume Next
        If SelectedFileIndex > listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then
            Me.listViewEx1.Items(SelectedFileIndex - 1).Selected = True
        End If

        If SelectedFileIndex <= listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then
            Me.listViewEx1.Items(SelectedFileIndex).Selected = True
        End If
    End Sub

#End Region


#Region "SORT LIST"

    Private Sub SortByDate(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles listViewEx1.ColumnClick
        Try
            If Me.listViewEx1.Sorting = SortOrder.Ascending Then
                Me.listViewEx1.Sorting = SortOrder.Descending
            Else
                Me.listViewEx1.Sorting = SortOrder.Ascending
            End If

            Me.listViewEx1.ListViewItemSorter = New ListViewItemComparer(e.Column, Me.listViewEx1.Sorting)
            Me.listViewEx1.Sort()
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub DisplayInformation() Handles listViewEx1.Click, listViewEx1.ItemSelectionChanged
        On Error Resume Next

        Me.lblCount.Text = "No. of Backup Files: " & Me.listViewEx1.Items.Count
        If Me.listViewEx1.SelectedItems.Count > 0 Then
            Me.lblSelectedFile.Text = Me.listViewEx1.SelectedItems(0).Text
        Else
            Me.lblSelectedFile.Text = "No file selected"
        End If

    End Sub


#End Region


    Public Class DownloadArgs
        Public ID As String
        Public SelectedFileName As String
        Public DownloadFileName As String
        Public BackupDate As String
    End Class


End Class
