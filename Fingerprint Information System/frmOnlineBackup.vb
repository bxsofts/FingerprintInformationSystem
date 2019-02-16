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

    Dim blPasswordFetched As Boolean = False

    Dim sAdmin As Boolean = False
    Dim lAdmin As Boolean = False
    Dim lUser As Boolean = True

    Dim MasterBackupFolderID As String = ""
    Dim CurrentFolderName As String = ""

#Region "FORM LOAD EVENTS"

    Private Sub CreateService() Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor

        lUser = True
        sAdmin = False
        lAdmin = False

        FileOwner = ShortOfficeName & "_" & ShortDistrictName
        SetFormTitle(FileOwner)
        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"

        If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
            My.Computer.FileSystem.CreateDirectory(BackupPath)
        End If

        BackupFolder = FileOwner
        CurrentFolderName = BackupFolder
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

            blUploadIsProgressing = False
            blDownloadIsProgressing = False
            blListIsLoading = False

            LoadFilesInUserBackupFolder(False)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region


#Region "CREATE SERVICE AND LOAD DATA"

    Private Sub LoadFilesInUserBackupFolder(ShowNoFileFoundMessage As Boolean)
        Me.listViewEx1.Items.Clear()
        listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
        listViewEx1.Sort()

        ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)
        NoFileFoundMessage = ShowNoFileFoundMessage
        CurrentFolderName = FileOwner
        bgwListUserFiles.RunWorkerAsync()
    End Sub

    Private Sub CreateServiceAndLoadUserFiles(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListUserFiles.DoWork
        Try
            blListIsLoading = True

            Dim Scopes As String() = {DriveService.Scope.Drive}

            FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            MasterBackupFolderID = GetMasterBackupFolderID()
            BackupFolderID = GetUserBackupFolderID()
            Dim List = FISService.Files.List()

            If BackupFolderID = "" Then
                BackupFolderID = CreateUserBackupFolder()
            End If

            List.Q = "mimeType = 'database/mdb' and '" & BackupFolderID & "' in parents"

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

                Dim Description As String = Result.Description

                If IsDate(Description) Or Description = "" Then
                    item.SubItems.Add(CurrentFolderName)
                Else
                    Dim SplitText() = Strings.Split(Description, "; ")
                    Dim u = SplitText.GetUpperBound(0)

                    If u = 0 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add("") 'remarks
                    End If

                    If u = 2 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add("Last SOC No: " & SplitText(1) & ", DI: " & SplitText(2)) 'remarks
                    End If
                End If

                item.ImageIndex = 2
                bgwListUserFiles.ReportProgress(90, item)
            Next

            If Not blPasswordFetched Then blPasswordFetched = GetAdminPasswords()

        Catch ex As Exception
            blListIsLoading = False
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub bgwListUserFiles_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwListUserFiles.ProgressChanged

        If TypeOf e.UserState Is String Then
            lblProgressStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If

    End Sub
    Private Sub bgwListUserFiles_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwListUserFiles.RunWorkerCompleted

        DisplayInformation()
        HideProgressControls()
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

        If lUser Then
            LoadFilesInUserBackupFolder(True)
        Else
            LoadFilesInMasterBackupFolder()
        End If

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

            ' If masterfolderid = "" Then
            '  masterfolderid = CreateMasterBackupFolder()
            ' End If

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
            ShareFile(id, "fingerprintinformationsystem@gmail.com")
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

    Private Sub ShareFile(fileid As String, email As String)
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

        If CurrentFolderName <> FileOwner Then
            MessageBoxEx.Show("Cannot upload backup to '" & CurrentFolderName & "' folder", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

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

        ShowProgressControls("0", "Uploading File...", eCircularProgressType.Line)
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
            body.Description = FileOwner & "; " & LatestSOCNumber & "; " & LatestSOCDI
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
                item.SubItems.Add("Last SOC No: " & LatestSOCNumber & ", DI: " & LatestSOCDI)
                item.ImageIndex = 2
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

        HideProgressControls()
        blUploadIsProgressing = False

        DisplayInformation()

        If uUploadStatus = UploadStatus.Completed Then
            Me.lblTotalFileSize.Text = "Total Online File Size: " & CalculateFileSize(TotalFileSize)
            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(0).Selected = True
            End If
            MessageBoxEx.Show("File uploaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "DOWNLOAD FILE"

    Private Sub listViewEx1_DoubleClick(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick, btnOpenFileMSAccess.Click
        Try

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Not lUser Then
                ListViewEx1_DoubleClick_AllBackups()
                Exit Sub
            End If

            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No file selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Selected item is a folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            strBackupFile = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
            Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text

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

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
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
                DevComponents.DotNetBar.MessageBoxEx.Show("No file selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "Downloaded File" Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Selected file is a local file. Please select an online file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Cannot download folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub DownloadFileFromDrive()
        Try


            Dim fname As String = Me.listViewEx1.SelectedItems(0).Text

            If CurrentFolderName = FileOwner Then
                strBackupFile = BackupPath & "\" & fname
            Else
                Dim f As String = strAppUserPath & "\FIS Backup\" & CurrentFolderName
                If My.Computer.FileSystem.DirectoryExists(f) = False Then
                    My.Computer.FileSystem.CreateDirectory(f)
                End If
                strBackupFile = f & "\" & fname
            End If

            Dim args As DownloadArgs = New DownloadArgs
            args.ID = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            args.SelectedFileName = fname
            args.DownloadFileName = strBackupFile
            args.BackupDate = Me.listViewEx1.SelectedItems(0).SubItems(1).Text

            ShowProgressControls("0", "Downloading File...", eCircularProgressType.Line)
            Me.Cursor = Cursors.WaitCursor

            bgwDownload.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
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
        HideProgressControls()

        blDownloadIsProgressing = False

        If dDownloadStatus = DownloadStatus.Completed Then
            If DownloadOnly Then
                MessageBoxEx.Show("File downloaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call Shell("explorer.exe /select," & strBackupFile, AppWinStyle.NormalFocus)
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
                DevComponents.DotNetBar.MessageBoxEx.Show("No file selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Selected item is a folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If CurrentFolderName <> FileOwner Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The backup file you selected is uploaded by '" & CurrentFolderName & "'." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.No Then Exit Sub
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Restoring the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.Yes Then

                strBackupFile = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
                Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text

                Me.Cursor = Cursors.WaitCursor

                    If InternetAvailable() = False Then
                        MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    DownloadRestore = True
                    DownloadFileFromDrive()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            boolRestored = False
            Me.Cursor = Cursors.Default
        End Try


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
                DevComponents.DotNetBar.MessageBoxEx.Show("No file selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Not sAdmin And CurrentFolderName <> FileOwner Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Deletion is not allowed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 And Not sAdmin Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Deletion is not allowed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim msg As String = ""
            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                msg = "folder"
            Else
                msg = "file"
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to remove the selected " & msg & "?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

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
        If Me.lblSelectedFolder.Text = "FIS Backup" Then
            Me.lblCount.Text = "No. of Backup Folders: " & Me.listViewEx1.Items.Count
            Me.lblTotalFileSize.Text = ""
        Else
            Me.lblCount.Text = "No. of Backup Files: " & Me.listViewEx1.Items.Count
            Me.lblTotalFileSize.Text = "Total Online File Size: " & CalculateFileSize(TotalFileSize)
        End If
       
    End Sub


#End Region


#Region "LOAD ALL BACKUPS"

    Private Sub btnGetAdminPrivilege_Click(sender As Object, e As EventArgs) Handles btnViewAllBackupFiles.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If Not lUser Then
            LoadFilesInMasterBackupFolder()
            Exit Sub
        End If

        If Not blPasswordFetched Then
            Me.Cursor = Cursors.WaitCursor
            ShowProgressControls("", "Please Wait...", eCircularProgressType.Donut)
        End If
        bgwGetPassword.RunWorkerAsync()
    End Sub

    Private Sub bgwGetPassword_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwGetPassword.DoWork

        If Not blPasswordFetched Then blPasswordFetched = GetAdminPasswords()
        bgwGetPassword.ReportProgress(100, blPasswordFetched)
    End Sub

    Private Sub bgwGetPassword_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwGetPassword.ProgressChanged
        HideProgressControls()
        If e.UserState = True Then
            If SetAdminPrivilege() Then LoadFilesInMasterBackupFolder()
        Else
            MessageBoxEx.Show("Connection Failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadFilesInMasterBackupFolder()
        Me.listViewEx1.Items.Clear()
        listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
        listViewEx1.Sort()
        ShowProgressControls("", "", eCircularProgressType.Donut)
        SetFormTitle("FIS Backup")
        CurrentFolderName = "FIS Backup"
        bgwListAllFiles.RunWorkerAsync(MasterBackupFolderID)
    End Sub

    Private Function SetAdminPrivilege() As Boolean
        frmInputBox.SetTitleandMessage("Enter Admin Password", "Enter Admin Password", True)
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then
            Return False
        End If

        If frmInputBox.txtInputBox.Text = SuperAdminPass Then
            sAdmin = True
            lAdmin = False
            lUser = False
            Return True
        ElseIf frmInputBox.txtInputBox.Text = LocalAdminPass Then
            lAdmin = True
            sAdmin = False
            lUser = False
            Return True
        Else
            MessageBoxEx.Show("Incorrect Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            sAdmin = False
            lAdmin = False
            lUser = True
            Return False
        End If
    End Function

    Private Sub bgwListAllFiles_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListAllFiles.DoWork
        blListIsLoading = True
        Me.Cursor = Cursors.WaitCursor
        Try
            ListAllFiles(e.Argument, False)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            blListIsLoading = False
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwListAllFiles_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwListAllFiles.ProgressChanged
        Try
            If TypeOf e.UserState Is ListViewItem Then
                listViewEx1.Items.Add(e.UserState)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub bgwListAllFiles_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwListAllFiles.RunWorkerCompleted
        Me.Cursor = Cursors.Default
        DisplayInformation()
        HideProgressControls()
        If listViewEx1.Items.Count > 0 Then
            Me.listViewEx1.Items(0).Selected = True
        End If
        blListIsLoading = False
    End Sub

    Private Sub ListAllFiles(ByVal FolderID As String, ShowTrashedFiles As Boolean)
        Try
            Dim List As FilesResource.ListRequest = FISService.Files.List()

            If ShowTrashedFiles Then
                List.Q = "trashed = true"
            Else
                List.Q = "trashed = false and '" & FolderID & "' in parents" ' list all files in parent folder. 
            End If


            List.PageSize = 100 ' maximum file list
            List.Fields = "nextPageToken, files(id, name, mimeType, size, modifiedTime, description)"
            List.OrderBy = "folder, name" 'sorting order

            Dim Results As FileList = List.Execute

            Dim item As ListViewItem

            TotalFileSize = 0
            For Each Result In Results.Files
                item = New ListViewItem(Result.Name)
                Dim modifiedtime As DateTime = Result.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                item.SubItems.Add(Result.Id)
                If Result.MimeType = "application/vnd.google-apps.folder" Then ' it is a folder
                    item.SubItems.Add("") 'size for folder
                    item.ImageIndex = 3
                    item.SubItems.Add(Result.Description)
                    item.SubItems.Add("") 'remarks
                    bgwListAllFiles.ReportProgress(2, item)
                ElseIf Result.MimeType = "database/mdb" Then
                    item.ImageIndex = 2
                    item.SubItems.Add(CalculateFileSize(Result.Size))
                    TotalFileSize = TotalFileSize + Result.Size

                    Dim Description As String = Result.Description

                    Dim SplitText() = Strings.Split(Description, "; ")
                    Dim u = SplitText.GetUpperBound(0)

                    If u = 0 Then
                        If sAdmin Then
                            item.SubItems.Add(Description)
                            item.SubItems.Add("") 'remarks
                        Else
                            If IsDate(Description) Or Description = "" Then
                                item.SubItems.Add(CurrentFolderName)
                                item.SubItems.Add("") 'remarks
                            Else
                                item.SubItems.Add(SplitText(0)) 'uploaded by
                                item.SubItems.Add("") 'remarks
                            End If
                        End If

                    End If

                    If u = 2 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add("Last SOC No: " & SplitText(1) & ", DI: " & SplitText(2)) 'remarks
                    End If

                    bgwListAllFiles.ReportProgress(2, item)
                End If
            Next

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub ListViewEx1_DoubleClick_AllBackups()

        If Me.listViewEx1.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).ImageIndex = 2 Then

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The file will be downloaded and opened in Microsoft Access.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.OK Then
                Me.Cursor = Cursors.WaitCursor
                If InternetAvailable() = False Then
                    MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                DownloadOpen = True
                DownloadOnly = False
                DownloadRestore = False
                DownloadFileFromDrive()
            End If
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
            listViewEx1.Sort()
            CurrentFolderName = Me.listViewEx1.SelectedItems(0).Text
            Dim CurrentFolderID = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            SetFormTitle(CurrentFolderName)
            CircularProgress1.ProgressText = ""
            CircularProgress1.IsRunning = True
            CircularProgress1.ProgressBarType = eCircularProgressType.Donut
            CircularProgress1.Show()

            Me.listViewEx1.Items.Clear()
            bgwListAllFiles.RunWorkerAsync(CurrentFolderID)
        Catch ex As Exception
            HideProgressControls()
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

#End Region

    Private Sub btnOpenBackupLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBackupFolder.Click
        On Error Resume Next

        If CurrentFolderName = FileOwner Then
            If Me.listViewEx1.SelectedItems.Count > 0 Then
                Dim file As String = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
                If My.Computer.FileSystem.FileExists(file) Then
                    Call Shell("explorer.exe /select," & file, AppWinStyle.NormalFocus)
                    Exit Sub
                End If
            End If

            If Not My.Computer.FileSystem.DirectoryExists(BackupPath) Then
                My.Computer.FileSystem.CreateDirectory(BackupPath)
            End If
            Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)

        ElseIf CurrentFolderName = "FIS Backup" Then
            Dim f As String = strAppUserPath & "\FIS Backup"
            If My.Computer.FileSystem.DirectoryExists(f) = False Then
                My.Computer.FileSystem.CreateDirectory(f)
            End If
            If Me.listViewEx1.SelectedItems.Count > 0 Then
                If My.Computer.FileSystem.DirectoryExists(f & "\" & Me.listViewEx1.SelectedItems(0).Text) Then
                    Call Shell("explorer.exe /select," & f & "\" & Me.listViewEx1.SelectedItems(0).Text, AppWinStyle.NormalFocus)
                    Exit Sub
                End If
            End If
            Call Shell("explorer.exe " & f, AppWinStyle.NormalFocus)

        Else
            Dim f As String = strAppUserPath & "\FIS Backup\" & CurrentFolderName
            If Not FileIO.FileSystem.DirectoryExists(f) Then
                FileIO.FileSystem.CreateDirectory(f)
            End If
            If Me.listViewEx1.SelectedItems.Count > 0 Then
                Dim file As String = f & "\" & Me.listViewEx1.SelectedItems(0).Text
                If My.Computer.FileSystem.FileExists(file) Then
                    Call Shell("explorer.exe /select," & file, AppWinStyle.NormalFocus)
                    Exit Sub
                End If
            End If

            Call Shell("explorer.exe " & f, AppWinStyle.NormalFocus)

        End If





    End Sub

    Private Sub ShowProgressControls(ProgressText As String, StatusText As String, ProgressType As eCircularProgressType)
        CircularProgress1.ProgressText = ProgressText
        lblProgressStatus.Text = StatusText
        CircularProgress1.IsRunning = True
        CircularProgress1.ProgressColor = GetProgressColor()
        CircularProgress1.ProgressBarType = ProgressType
        CircularProgress1.Show()
        lblProgressStatus.Show()
    End Sub

    Private Sub HideProgressControls()
        CircularProgress1.IsRunning = False
        CircularProgress1.ProgressText = ""
        CircularProgress1.ProgressBarType = eCircularProgressType.Line
        lblProgressStatus.Text = ""
        CircularProgress1.Hide()
        lblProgressStatus.Hide()
    End Sub

    Private Sub SetFormTitle(ByVal Header As String)
        Me.Text = "Online Database Backup List - " & Header
        Me.TitleText = "<b>Online Database Backup List - " & Header & "</b>"
        Me.lblSelectedFolder.Text = Header
    End Sub
    Public Class DownloadArgs
        Public ID As String
        Public SelectedFileName As String
        Public DownloadFileName As String
        Public BackupDate As String
    End Class

  

End Class
