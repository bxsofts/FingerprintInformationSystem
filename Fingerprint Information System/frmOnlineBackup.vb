﻿Imports System.Threading
Imports System.Threading.Tasks

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services

Imports Google.Apis.Auth
Imports Google.Apis.Download
Imports System.IO
Imports Google.Apis.Upload
Imports DevComponents.DotNetBar
Imports Google.Apis.Util.Store




Public Class frmOnlineBackup
    Private FISService As DriveService = New DriveService
    Dim FISUserCredential As UserCredential
    Dim BackupFolder As String
    Dim BackupFolderID As String
    Dim BackupPath As String = ""
    Public CredentialPath As String
    Public JsonPath As String
    Public NoFileFoundMessage As Boolean = False
    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus
    Public dFileSize As Long
    Public uBytesUploaded As Long
    Public uUploadStatus As UploadStatus
    Public ResumeUpload As Boolean = False
    Public DownloadRestore As Boolean = False
    Public DownloadOpen As Boolean = False

#Region "FORM LOAD EVENTS"


    Private Sub CreateService() Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"

        If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
            My.Computer.FileSystem.CreateDirectory(BackupPath)
        End If

        BackupFolder = "FIS_BACKUP_" & ShortOfficeName & "_" & ShortDistrictName
        BackupFolderID = ""
        Try


            CredentialPath = strAppUserPath & "\GoogleDriveAuthentication"
            JsonPath = CredentialPath & "\FIS.json"

            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'copy from application folder
                My.Computer.FileSystem.CreateDirectory(CredentialPath)
                FileSystem.FileCopy(strAppPath & "\FIS.json", CredentialPath & "\FIS.json")
            End If


            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'if copy failed
                MessageBoxEx.Show("Authentication File is missing. Please re-install the application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            If Not FileIO.FileSystem.FileExists(CredentialPath & "\Google.Apis.Auth.OAuth2.Responses.TokenResponse-user") Then
                MessageBoxEx.Show("The application will now open your browser. Please enter your gmail id and password to authenticate.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Me.listViewEx1.Items.Clear()
            listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
            listViewEx1.Sort()

            
            CircularProgress1.ProgressText = ""
            lblStatus.Text = "Please wait..."
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()
            lblStatus.Show()

            NoFileFoundMessage = False

            bgwService.RunWorkerAsync()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region


#Region "CREATE SERVICE AND LOAD DATA"

    Private Sub CreateServiceAndLoadData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwService.DoWork
        Try

            bgwService.ReportProgress(10, "Please wait...")
            System.Threading.Thread.Sleep(500)

            Dim fStream = New FileStream(JsonPath, FileMode.Open, FileAccess.Read)

            Dim Scopes As String() = {DriveService.Scope.Drive}

            bgwService.ReportProgress(20, "Connecting to Google Drive...")
            System.Threading.Thread.Sleep(500)

            FISUserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, "user", CancellationToken.None, New FileDataStore(CredentialPath, True)).Result

            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISUserCredential, .ApplicationName = strAppName})

            bgwService.ReportProgress(50, "Fetching Local Files...")
            System.Threading.Thread.Sleep(500)

            Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchAllSubDirectories, "FingerPrintBackup*.mdb")

                If foundFile Is Nothing Then
                    Exit Sub
                End If

                Dim FileName = My.Computer.FileSystem.GetName(foundFile)
                Dim FullFilePath = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & FileName

                Dim Filedate As DateTime = DateTime.ParseExact(FileName.Replace("FingerPrintBackup-", "").Replace(".mdb", ""), BackupDateFormatString, culture)

                Dim item As ListViewItem = New ListViewItem(FileName)
                item.SubItems.Add(Filedate.ToString("dd-MM-yyyy HH:mm:ss"))
                item.SubItems.Add("Downloaded File")
                item.SubItems.Add("Downloaded File")
                item.ImageIndex = 1
                bgwService.ReportProgress(60, item)
            Next

            bgwService.ReportProgress(80, "Fetching Online Files...")
            System.Threading.Thread.Sleep(500)


            BackupFolderID = GetBackupFolderID()
            Dim List = FISService.Files.List()

            If BackupFolderID = "" Then
                BackupFolderID = CreateBackupFolder()
            End If

            Dim parentlist As New List(Of String)
            parentlist.Add(BackupFolderID)

            List.Q = "mimeType = 'database/mdb' and '" & BackupFolderID & "' in parents"
            List.Fields = "nextPageToken, files(id, name, description)"

            Dim Results = List.Execute

            For Each Result In Results.Files
                Dim item As ListViewItem = New ListViewItem(Result.Name)
                item.SubItems.Add(Result.Description)
                item.SubItems.Add(Result.Id)
                item.SubItems.Add("")
                item.ImageIndex = 0
                bgwService.ReportProgress(90, item)
            Next


        Catch ex As Exception

            ShowErrorMessage(ex)


        End Try

    End Sub

    Private Sub CreateServiceBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwService.ProgressChanged

        If TypeOf e.UserState Is String Then
            lblStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If

    End Sub


    Private Sub RefreshBackupList() Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Me.listViewEx1.Items.Clear()
        Me.CircularProgress1.Show()
        Me.lblStatus.Text = "Please Wait..."
        Me.lblStatus.Show()
        Me.CircularProgress1.IsRunning = True
        NoFileFoundMessage = True
        bgwService.RunWorkerAsync()
    End Sub

#End Region


#Region "GOOGLE DRIVE ID AND FOLDER MANIPULATION"

    Private Function GetBackupFolderID()
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

    Private Function CreateBackupFolder()
        Try
            Dim id As String = ""
            Dim body As New Google.Apis.Drive.v3.Data.File()

            Dim NewDirectory = New Google.Apis.Drive.v3.Data.File

            body.Name = BackupFolder
            body.Description = "FIS Backup Folder"
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

#End Region


#Region "BACKUP DATABSE"


    Private Sub UploadBackup() Handles btnBackupDatabase.Click

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If


        Me.CircularProgress1.Show()
        Me.lblStatus.Text = "Please Wait..."
        Me.lblStatus.Show()
        Me.CircularProgress1.IsRunning = True

        bgwUpload.RunWorkerAsync()


    End Sub


    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpload.DoWork

        Try

            bgwUpload.ReportProgress(10, "Connecting to Google Drive...")
            System.Threading.Thread.Sleep(500)

            Dim BackupTime As Date = Now
            Dim d As String = Strings.Format(BackupTime, BackupDateFormatString)
            Dim sBackupTime = Strings.Format(BackupTime, "dd-MM-yyyy HH:mm:ss")
            Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"

            If BackupFolderID = "" Then
                BackupFolderID = CreateBackupFolder()
            End If

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = BackupFileName
            body.Description = sBackupTime
            body.MimeType = "database/mdb"

            Dim parentlist As New List(Of String)
            parentlist.Add(BackupFolderID)
            body.Parents = parentlist


            Dim tmpFileName As String = My.Computer.FileSystem.GetTempFileName

            My.Computer.FileSystem.CopyFile(strDatabaseFile, tmpFileName, True)

            bgwUpload.ReportProgress(20, "Copying Database...")
            System.Threading.Thread.Sleep(500)

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(tmpFileName)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            dFileSize = FileLen(tmpFileName)
            bgwUpload.ReportProgress(50, "Uploading...")
            System.Threading.Thread.Sleep(500)

            Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
            UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
            AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

            UploadRequest.Fields = "id"
            UploadRequest.Upload()


            If uUploadStatus = UploadStatus.Completed Then

                bgwUpload.ReportProgress(97, "Uploaded.")
                System.Threading.Thread.Sleep(500)

                Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody

                If file Is Nothing Then
                    bgwUpload.ReportProgress(98, "Upload failed.")
                    System.Threading.Thread.Sleep(500)
                Else
                    Dim item As ListViewItem = New ListViewItem(BackupFileName)
                    item.SubItems.Add(sBackupTime)
                    item.SubItems.Add(file.Id)
                    item.SubItems.Add("")
                    item.ImageIndex = 0
                    bgwUpload.ReportProgress(99, item)
                End If
            End If

            If uUploadStatus = UploadStatus.Failed Then
                bgwUpload.ReportProgress(98, "Upload interrupted.")
            End If

            Stream.Close()


        Catch ex As Exception

            ShowErrorMessage(ex)

        End Try
    End Sub

    Private Sub Upload_ProgressChanged(Progress As IUploadProgress)

        Control.CheckForIllegalCrossThreadCalls = False

        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
        CircularProgress1.ProgressText = CInt(uBytesUploaded / dFileSize * 100)
        System.Threading.Thread.Sleep(100)
    End Sub

    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged


        If TypeOf e.UserState Is String Then
            lblStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If

        If e.ProgressPercentage = 98 Then
            MessageBoxEx.Show("Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Question)
        End If

        If e.ProgressPercentage = 97 Then
            lblStatus.Text = "Uploaded"
            frmMainInterface.ShowAlertMessage("Database uploaded successfully")
        End If

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

            CircularProgress1.ProgressText = ""
            lblStatus.Text = "Connecting to Google Drive..."
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()
            lblStatus.Show()
            Me.Cursor = Cursors.WaitCursor

            bgwDownload.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub DownloadSelectedFile() Handles btnDownloadDatabase.Click
        Try

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


            Dim args As DownloadArgs = New DownloadArgs

            args.ID = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            args.SelectedFileName = Me.listViewEx1.SelectedItems(0).Text
            args.DownloadFileName = BackupPath & "\" & args.SelectedFileName
            args.BackupDate = Me.listViewEx1.SelectedItems(0).SubItems(1).Text

            CircularProgress1.ProgressText = ""
            lblStatus.Text = "Connecting to Google Drive..."
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()
            lblStatus.Show()
            Me.Cursor = Cursors.WaitCursor

            bgwDownload.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownload.DoWork
        Try
            Dim args As DownloadArgs = e.Argument
            bgwDownload.ReportProgress(10, "Connecting to Google Drive...")
            System.Threading.Thread.Sleep(500)

            Dim request = FISService.Files.Get(args.ID)
            request.Fields = "size"
            Dim file = request.Execute

            dFileSize = file.Size

            Dim fStream = New System.IO.FileStream(args.DownloadFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
            Dim mStream = New System.IO.MemoryStream

            bgwDownload.ReportProgress(20, "Downloading...")
            System.Threading.Thread.Sleep(100)

            Dim m = request.MediaDownloader
            m.ChunkSize = 256 * 1024

            AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

            request.DownloadWithStatus(mStream)

            If dDownloadStatus = DownloadStatus.Completed Then
                mStream.WriteTo(fStream)

                Dim item As ListViewItem = New ListViewItem(args.SelectedFileName)
                item.SubItems.Add(args.BackupDate)
                item.SubItems.Add("Downloaded File")
                item.SubItems.Add("Downloaded File")
                item.ImageIndex = 1
                bgwDownload.ReportProgress(97, item)
                System.Threading.Thread.Sleep(100)

                If DownloadOpen Then
                    bgwDownload.ReportProgress(98, "Opening in MS Access...")
                    System.Threading.Thread.Sleep(1000)
                End If

                If DownloadRestore Then
                    bgwDownload.ReportProgress(99, "Restoring...")
                    System.Threading.Thread.Sleep(1000)
                End If

            End If

            fStream.Close()
            mStream.Close()


        Catch ex As Exception
            ShowErrorMessage(ex)
            bgwDownload.ReportProgress(100)
        End Try
    End Sub


    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)
        Control.CheckForIllegalCrossThreadCalls = False

        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        CircularProgress1.ProgressText = CInt(dBytesDownloaded / dFileSize * 100)
        System.Threading.Thread.Sleep(100)
    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownload.ProgressChanged

        If TypeOf e.UserState Is String Then
            lblStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If


        If e.ProgressPercentage = 97 Then
            lblStatus.Text = "Downloaded"
            frmMainInterface.ShowAlertMessage("File downloaded successfully")
        End If

    End Sub



#End Region

#Region "RESTORE DATABASE"

    Private Sub RestoreSelectedFile() Handles btnRestoreDatabase.Click
        Try

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
                        My.Computer.FileSystem.CopyFile(strBackupFile, strDatabaseFile, True)
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


#Region "OPEN FOLDER IN MS ACCESS"
    Private Sub OpenFileInMSAccess(sender As Object, e As EventArgs) Handles btnOpenFileMSAccess.Click, listViewEx1.DoubleClick
        Try
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
                Me.Cursor = Cursors.WaitCursor
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

        If FileIO.FileSystem.DirectoryExists(BackupPath) Then
            Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
        End If
    End Sub

#End Region


#Region "REMOVE FILE"
    Private Sub RemoveBackupFileFromDrive() Handles btnRemoveBackupFile.Click
        Try
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

            If id = "Downloaded File" Then 'delete local file
                My.Computer.FileSystem.DeleteFile(SelectedFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                Me.listViewEx1.SelectedItems(0).Remove()
                Application.DoEvents()
                frmMainInterface.ShowAlertMessage("Selected backup file deleted to the Recycle Bin.")
            Else 'remove online file

                If InternetAvailable() = False Then
                    MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Dim DeleteRequest = FISService.Files.Delete(id)
                DeleteRequest.Execute()
                Me.listViewEx1.SelectedItems(0).Remove()
                Application.DoEvents()
                frmMainInterface.ShowAlertMessage("Selected backup file deleted from Google Drive.")
            End If

            Me.Cursor = Cursors.Default

            DisplayInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

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


    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwService.RunWorkerCompleted, bgwUpload.RunWorkerCompleted, bgwDownload.RunWorkerCompleted

        DisplayInformation()
        CircularProgress1.IsRunning = False
        CircularProgress1.ProgressText = ""
        lblStatus.Text = ""
        CircularProgress1.Hide()
        lblStatus.Hide()
        Me.Cursor = Cursors.Default

        If NoFileFoundMessage And listViewEx1.Items.Count = 0 Then
            frmMainInterface.ShowAlertMessage("No online Backup Files were found.")
            NoFileFoundMessage = False
        End If

        If e.Error IsNot Nothing Then
            MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        If DownloadOpen Then
            Me.Cursor = Cursors.WaitCursor
            DownloadOpen = False
            lblStatus.Text = "Opening in MS Access..."
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
                My.Computer.FileSystem.CopyFile(strBackupFile, strDatabaseFile, True)
                lblStatus.Text = "Restoring..."
                boolRestored = True
                Me.Close()
                Exit Sub
            Else
                MessageBoxEx.Show("Cannot restore. Backup file is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

End Class

Public Class DownloadArgs
    Public ID As String
    Public SelectedFileName As String
    Public DownloadFileName As String
    Public BackupDate As String
End Class