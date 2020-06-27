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
    Dim UserBackupFolderName As String
    Dim UserBackupFolderID As String
    Dim BackupPath As String = ""
    Public CredentialFilePath As String
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
    Public DownloadPreview As Boolean = False

    Dim FileOwner As String = ""
    Dim TotalFileSize As Long = 0

    Dim blPasswordFetched As Boolean = False

    Dim sAdmin As Boolean = False
    Dim lAdmin As Boolean = False
    Dim lUser As Boolean = True

    Dim MasterBackupFolderID As String = ""
    Dim CurrentFolderName As String = ""
    Dim LastModifiedDate As String = ""
    Dim LastModificationDetail As String = ""
    Dim blShowFileCount As Boolean



#Region "FORM LOAD EVENTS"

    Private Sub CreateService() Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor

        lUser = True
        sAdmin = False
        lAdmin = False

        FileOwner = ShortOfficeName & "_" & ShortDistrictName
        SetFormTitle(ShortOfficeName & ", " & FullDistrictName)

        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"

        If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
            My.Computer.FileSystem.CreateDirectory(BackupPath)
        End If

        UserBackupFolderName = FullDistrictName
        CurrentFolderName = UserBackupFolderName
        UserBackupFolderID = ""
        Me.lblTotalFileSize.Text = ""

        Try

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

            blUploadIsProgressing = False
            blDownloadIsProgressing = False
            blListIsLoading = False

            LastModifiedDate = frmMainInterface.GetLastModificationDate.ToString("dd-MM-yyyy HH:mm:ss")
            LastModificationDetail = frmMainInterface.GetLastModificationDetails()

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
        Me.lblTotalFileSize.Text = ""
        Me.lblItemCount.Text = ""
        listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
        listViewEx1.Sort()

        If UserBackupFolderName = "" Then
            MessageBoxEx.Show("'Full District Name' is empty. Cannot load files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)
        NoFileFoundMessage = ShowNoFileFoundMessage
        CurrentFolderName = UserBackupFolderName
        bgwListUserFiles.RunWorkerAsync()
    End Sub

    Private Sub CreateServiceAndLoadUserFiles(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListUserFiles.DoWork
        Try
            blListIsLoading = True

            Dim Scopes As String() = {DriveService.Scope.Drive}

            FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            MasterBackupFolderID = GetMasterBackupFolderID()
            UserBackupFolderID = GetUserBackupFolderID()
            Dim List = FISService.Files.List()

            If UserBackupFolderID = "" Then
                UserBackupFolderID = CreateUserBackupFolder()
            End If

            List.Q = "mimeType = 'database/mdb' and '" & UserBackupFolderID & "' in parents"
            List.PageSize = 1000
            List.Fields = "files(id, name, modifiedTime, size, description)"

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
                    item.SubItems.Add(FileOwner)
                Else
                    Dim SplitText() = Strings.Split(Description, "; ")
                    Dim u = SplitText.GetUpperBound(0)

                    If u = 0 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add("") 'last modified date
                        item.SubItems.Add("") 'Last SOC No.
                        item.SubItems.Add("") 'Last SOC DI
                        item.SubItems.Add("") 'Total Records
                    End If

                    If u = 3 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add(SplitText(1)) 'last modified date
                        item.SubItems.Add(SplitText(2)) ' Last SOC No.
                        item.SubItems.Add(SplitText(3)) ' Last SOC DI
                        item.SubItems.Add("") 'Total Records
                    End If

                    If u = 4 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add(SplitText(1)) 'last modified date
                        item.SubItems.Add(SplitText(2)) ' Last SOC No.
                        item.SubItems.Add(SplitText(3)) ' Last SOC DI
                        item.SubItems.Add(SplitText(4)) 'Total Records
                    End If

                    If u = 5 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add(SplitText(1)) 'last modified date
                        item.SubItems.Add(SplitText(2)) ' Last SOC No.
                        item.SubItems.Add(SplitText(3)) ' Last SOC DI
                        item.SubItems.Add(SplitText(4)) 'Total Records
                        Dim RemoteModificationDetails = SplitText(5) ' Last Modiifcation Details

                        If RemoteModificationDetails = "NIL" Then
                            RemoteModificationDetails = ""
                        End If
                        item.SubItems.Add(RemoteModificationDetails)
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
            ShowDesktopAlert("No online Backup Files were found.")
            NoFileFoundMessage = False
        End If

        If e.Error IsNot Nothing Then
            MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.lblTotalFileSize.Text = "Total Online File Size: " & CalculateFileSize(TotalFileSize)
    End Sub

    Private Sub RefreshBackupList() Handles btnRefresh.Click, btnRefreshCM.Click

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
            LoadFilesInMasterBackupFolder(False)
        End If

    End Sub

#End Region


#Region "GOOGLE DRIVE ID AND FOLDER MANIPULATION"

    Private Function GetUserBackupFolderID() As String
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserBackupFolderName & "' and '" & MasterBackupFolderID & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & FileOwner & "' and '" & MasterBackupFolderID & "' in parents"
                List.Fields = "files(id)"
                Results = List.Execute
                cnt = Results.Files.Count

                If cnt = 0 Then
                    id = ""
                Else
                    id = Results.Files(0).Id
                    RenameUserBackupFolderName(id)
                End If
            Else
                id = Results.Files(0).Id
            End If

            Return id
        Catch ex As Exception
            ' ShowErrorMessage(ex)
            Return ""
        End Try
    End Function

    Private Function CreateUserBackupFolder() As String

        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserBackupFolderName & "' and '" & MasterBackupFolderID & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count

            If cnt = 0 Then
                Dim masterfolderid As String = GetMasterBackupFolderID()
                Dim parentlist As New List(Of String)
                parentlist.Add(masterfolderid)

                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                NewDirectory.Name = UserBackupFolderName
                NewDirectory.Parents = parentlist
                NewDirectory.MimeType = "application/vnd.google-apps.folder"
                NewDirectory.Description = FileOwner
                Dim request As FilesResource.CreateRequest = FISService.Files.Create(NewDirectory)
                NewDirectory = request.Execute()
                id = NewDirectory.Id
            Else
                id = Results.Files(0).Id
            End If

            Return id

        Catch ex As Exception
            ' ShowErrorMessage(ex)
            Return ""
        End Try

    End Function

    Private Function CreateMasterBackupFolder() As String
        Try
            Dim id As String = ""
            Dim NewDirectory = New Google.Apis.Drive.v3.Data.File

            NewDirectory.Name = "FIS Backup"
            NewDirectory.Description = "Admin"
            NewDirectory.MimeType = "application/vnd.google-apps.folder"

            Dim request As FilesResource.CreateRequest = FISService.Files.Create(NewDirectory)

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

    Private Sub RenameUserBackupFolderName(UserBackupFolderID As String)
        Try
            If blDBIsInPreviewMode Then
                Exit Sub
            End If

            Dim request As New Google.Apis.Drive.v3.Data.File
            request.Name = FullDistrictName
            request.Description = FileOwner
            FISService.Files.Update(request, UserBackupFolderID).Execute()
        Catch ex As Exception

        End Try
    End Sub

#End Region


#Region "BACKUP DATABASE"

    Private Sub UploadBackup() Handles btnBackupDatabase.Click, btnUploadCM.Click

        If UserBackupFolderName = "" Then
            MessageBoxEx.Show("'Full District Name' is empty. Cannot load files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If CurrentFolderName <> UserBackupFolderName Then
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

        If blDBIsInPreviewMode Then
            MessageBoxEx.Show("Database is in Preview Mode. Cannot Upload.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            If UserBackupFolderID = "" Then
                UserBackupFolderID = CreateUserBackupFolder()
            End If

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = BackupFileName
            body.Description = FileOwner & "; " & LastModifiedDate & "; " & LatestSOCNumber & "; " & LatestSOCDI & "; " & LocalSOCRecordCount & "; " & LastModificationDetail
            body.MimeType = "database/mdb"

            Dim parentlist As New List(Of String)
            parentlist.Add(UserBackupFolderID)
            body.Parents = parentlist


            Dim tmpFileName As String = My.Computer.FileSystem.GetTempFileName

            My.Computer.FileSystem.CopyFile(strDatabaseFile, tmpFileName, True)

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
                item.SubItems.Add(LastModifiedDate)
                item.SubItems.Add(LatestSOCNumber)
                item.SubItems.Add(LatestSOCDI)
                item.SubItems.Add(LocalSOCRecordCount)
                item.SubItems.Add(LastModificationDetail)
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
                If listViewEx1.SelectedItems.Count > 0 Then listViewEx1.SelectedItems.Clear()
                If listViewEx1.Items(0).Text.ToLower = "fingerprintdb.mdb" Then
                    Me.listViewEx1.Items(1).Selected = True
                Else
                    Me.listViewEx1.Items(0).Selected = True
                End If
            End If
            ShowDesktopAlert("File uploaded successfully.")
        End If
        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "DOWNLOAD FILE"

    Private Sub listViewEx1_DoubleClick(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick, btnOpenFileMSAccess.Click, btnOpenCM.Click
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

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub DownloadSelectedFile() Handles btnDownloadDatabase.Click, btnDownloadCM.Click
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

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            If CurrentFolderName = UserBackupFolderName Then
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
                blPreviewDB = False
                If My.Computer.FileSystem.FileExists(strBackupFile) Then
                    strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")
                    My.Computer.FileSystem.CopyFile(strBackupFile, strDatabaseFile, True)
                    blRestoreDB = True
                    Me.Close()
                Else
                    MessageBoxEx.Show("Cannot restore. Backup file is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Me.Cursor = Cursors.Default
            End If

            If DownloadPreview Then
                Me.Cursor = Cursors.WaitCursor
                DownloadPreview = False
                blRestoreDB = False
                If My.Computer.FileSystem.FileExists(strBackupFile) Then
                    strDatabaseFile = strBackupFile
                    blPreviewDB = True
                    Me.Close()
                Else
                    MessageBoxEx.Show("Cannot preview. Backup file is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        DownloadPreview = False

        DisplayInformation()
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "RESTORE DATABASE"

    Private Sub RestoreSelectedFile() Handles btnRestoreDatabase.Click, btnRestoreCM.Click
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

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Selected item is a folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If CurrentFolderName <> UserBackupFolderName Then
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
            blRestoreDB = False
            Me.Cursor = Cursors.Default
        End Try


    End Sub

#End Region


#Region "PREVIEW DATABASE"

    Private Sub PreviewDatabase() Handles btnPreview.Click, btnPreviewCM.Click
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

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Selected item is a folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            DownloadPreview = True

            DownloadFileFromDrive()

        Catch ex As Exception
            ShowErrorMessage(ex)
            ' blPreviewMode = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub
#End Region


#Region "REMOVE FILE"
    Private Sub RemoveBackupFileFromDrive() Handles btnRemoveBackupFile.Click, btnRemoveCM.Click
        Try

            Dim selectedcount As Integer = Me.listViewEx1.SelectedItems.Count

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If selectedcount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Not sAdmin And CurrentFolderName <> UserBackupFolderName Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Deletion is not allowed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 And Not sAdmin Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Deletion is not allowed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ftype As String = ""
            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                ftype = "folder"
            Else
                If selectedcount = 1 Then
                    ftype = "file"
                Else
                    ftype = "files"
                End If
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to remove the selected " & ftype & "?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor

            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If selectedcount > 1 Then
                ShowPleaseWaitForm()
            End If

            Dim SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
            Dim i = 0
            For i = 0 To selectedcount - 1
                ' Dim SelectedFileName As String = Me.listViewEx1.SelectedItems(0).Text
                '  Dim SelectedFile = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
                Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text

                SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index

                Dim request = FISService.Files.Get(id)
                request.Fields = "size"
                Dim file = request.Execute

                Dim DeleteRequest = FISService.Files.Delete(id)
                DeleteRequest.Execute()
                If Not file.Size Is Nothing Then
                    TotalFileSize -= file.Size
                End If

                Me.listViewEx1.SelectedItems(0).Remove()
                Application.DoEvents()
            Next
            SelectNextItem(SelectedFileIndex)
            ClosePleaseWaitForm()
            ShowDesktopAlert("Selected " & ftype & " deleted from Google Drive.")
            Me.Cursor = Cursors.Default
            DisplayInformation()
            ' GetDriveStorageDetails()
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub SelectNextItem(SelectedFileIndex)
        On Error Resume Next
        If SelectedFileIndex < listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then 'selected 5 < count 10 
            Me.listViewEx1.Items(SelectedFileIndex).Selected = True 'select 5
        End If

        If SelectedFileIndex = listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then 'selected 5 = count 5 
            Me.listViewEx1.Items(SelectedFileIndex - 1).Selected = True 'select 5
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

    Private Sub DisplayInformation()
        On Error Resume Next
        If Me.lblSelectedFolder.Text = "FIS Backup" Then
            Me.lblItemCount.Text = "No. of Backup Folders: " & Me.listViewEx1.Items.Count
            Me.lblTotalFileSize.Text = ""
        Else
            Me.lblItemCount.Text = "No. of Backup Files: " & Me.listViewEx1.Items.Count
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
            LoadFilesInMasterBackupFolder(True)
            Exit Sub
        End If

        Dim RecoverPassword As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "RecoverPassword", "0")
        If RecoverPassword = "1" Then
            SuperAdminPass = "^^^px7600d"
            Dim adminprivilege As Boolean = SetAdminPrivilege()
            If adminprivilege = True Then '
                LoadFilesInMasterBackupFolder(True)
            End If
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
            If SetAdminPrivilege() Then LoadFilesInMasterBackupFolder(True)
        Else
            MessageBoxEx.Show("Unable to get user authentication.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadFilesInMasterBackupFolder(ShowFileCount As Boolean)
        Me.listViewEx1.Items.Clear()
        Me.lblTotalFileSize.Text = ""
        Me.lblItemCount.Text = ""
        listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
        listViewEx1.Sort()
        ShowProgressControls("", "", eCircularProgressType.Donut)
        SetFormTitle("FIS Backup")
        CurrentFolderName = "FIS Backup"
        blShowFileCount = ShowFileCount
        bgwListAllFiles.RunWorkerAsync(MasterBackupFolderID)
    End Sub

    Private Function SetAdminPrivilege() As Boolean
        frmInputBox.SetTitleandMessage("Enter Admin Password", "Enter Admin Password", True)
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then
            Return False
        End If

        Dim pass As String = EncryptText(frmInputBox.txtInputBox.Text)
        If pass = SuperAdminPass Then
            sAdmin = True
            lAdmin = False
            lUser = False
            Return True
        ElseIf pass = LocalAdminPass Then
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
                If blShowFileCount Then
                    Me.CircularProgress1.ProgressText = e.ProgressPercentage & "%"
                End If
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
        blShowFileCount = False
    End Sub

    Private Sub ListAllFiles(ByVal FolderID As String, ShowTrashedFiles As Boolean)
        Try
            Dim List As FilesResource.ListRequest = FISService.Files.List()

            If ShowTrashedFiles Then
                List.Q = "trashed = true"
            Else
                List.Q = "trashed = false and '" & FolderID & "' in parents" ' list all files in parent folder. 
            End If


            List.PageSize = 1000 ' maximum file list
            List.Fields = "files(id, name, mimeType, size, modifiedTime, description)"
            List.OrderBy = "folder, name" 'sorting order

            Dim Results As FileList = List.Execute

            Dim item As ListViewItem

            TotalFileSize = 0
            Dim id As String = ""
            Dim filecount As Integer = 0
            Dim i As Integer = 0
            Dim foldercount As Integer = Results.Files.Count

            For Each Result In Results.Files
                item = New ListViewItem(Result.Name)
                Dim modifiedtime As DateTime = Result.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                id = Result.Id
                item.SubItems.Add(id)
                If Result.MimeType = "application/vnd.google-apps.folder" Then ' it is a folder
                    item.SubItems.Add("") 'size for folder
                    item.ImageIndex = 3
                    item.SubItems.Add(Result.Description)
                    If blShowFileCount Then
                        item.SubItems.Add("")
                        item.SubItems.Add("")
                        item.SubItems.Add("")
                        List.Q = "trashed = false and '" & id & "' in parents"
                        Results = List.Execute
                        filecount = Results.Files.Count
                        item.SubItems.Add(filecount) 'remarks
                        i = i + 1
                    End If

                    bgwListAllFiles.ReportProgress(i / foldercount * 100, item)
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
                            item.SubItems.Add("") 'last modified date
                            item.SubItems.Add("") 'Last SOC No.
                            item.SubItems.Add("") 'Last SOC DI
                            item.SubItems.Add("") 'Total Records
                        Else
                            If IsDate(Description) Or Description = "" Then
                                item.SubItems.Add(CurrentFolderName)
                                item.SubItems.Add("") 'last modified date
                                item.SubItems.Add("") 'Last SOC No.
                                item.SubItems.Add("") 'Last SOC DI
                                item.SubItems.Add("") 'Total Records
                            Else
                                item.SubItems.Add(SplitText(0)) 'uploaded by
                                item.SubItems.Add("") 'last modified date
                                item.SubItems.Add("") 'Last SOC No.
                                item.SubItems.Add("") 'Last SOC DI
                                item.SubItems.Add("") 'Total Records
                            End If
                        End If

                    End If


                    If u = 3 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add(SplitText(1)) 'last modified date
                        item.SubItems.Add(SplitText(2)) ' Last SOC No.
                        item.SubItems.Add(SplitText(3)) ' Last SOC DI
                        item.SubItems.Add("") 'Total Records
                    End If

                    If u = 4 Then
                        item.SubItems.Add(SplitText(0)) 'uploaded by
                        item.SubItems.Add(SplitText(1)) 'last modified date
                        item.SubItems.Add(SplitText(2)) ' Last SOC No.
                        item.SubItems.Add(SplitText(3)) ' Last SOC DI
                        item.SubItems.Add(SplitText(4)) 'Total Records
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
            Me.lblTotalFileSize.Text = ""
            Me.lblItemCount.Text = ""
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

        If CurrentFolderName = UserBackupFolderName Then
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


    Private Sub ContextMenuBar1_PopupOpen(sender As Object, e As PopupOpenEventArgs) Handles ContextMenuBar1.PopupOpen
        On Error Resume Next
        Me.btnRefreshCM.Visible = False
        Me.btnUploadCM.Visible = False
        Me.btnDownloadCM.Visible = False
        Me.btnRemoveCM.Visible = False
        Me.btnRestoreCM.Visible = False
        Me.btnOpenCM.Visible = False

        If Me.listViewEx1.Items.Count = 0 Or Me.listViewEx1.SelectedItems.Count = 0 Then
            Me.btnRefreshCM.Visible = True
            Me.btnUploadCM.Visible = True
        End If

        If Me.listViewEx1.SelectedItems.Count = 1 Then
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                e.Cancel = True
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = 3 Then
                Me.btnRemoveCM.Visible = sAdmin
            Else
                Me.btnDownloadCM.Visible = True
                Me.btnRemoveCM.Visible = True
                Me.btnRestoreCM.Visible = True
                Me.btnOpenCM.Visible = True
            End If
        End If

        If Me.listViewEx1.SelectedItems.Count > 1 Then
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                e.Cancel = True
            End If
            Me.btnRemoveCM.Visible = True
        End If
    End Sub

End Class
