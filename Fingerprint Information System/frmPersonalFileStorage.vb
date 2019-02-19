﻿Imports DevComponents.DotNetBar
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.IO

Imports Google
Imports Google.Apis.Auth
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store


Public Class frmPersonalFileStorage
    Dim GDService As DriveService = New DriveService
    Dim CredentialFilePath As String
    Dim JsonFile As String
    Dim TokenFile As String = ""

    Dim CurrentFolderID As String = "root"
    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus
    Public uBytesUploaded As Long
    Public uUploadStatus As UploadStatus
    Dim uSelectedFile As String = ""

    Public dFileSize As Long
    Dim dFormatedFileSize As String = ""
    Public SaveFileName As String = ""
    Dim CurrentFolderName As String = ""
    Dim CurrentFolderPath As String = ""
    Dim ParentFolderPath As String = ""

    Dim SelectedFileID As String = ""
    Dim SelectedFileIndex As Integer = 0

    Public Enum ImageIndex
        Folder = 0
        GoogleDrive = 1
        ReturnBack = 2
        MSAccess = 3
        Exe = 4
        PDF = 5
        Word = 6
        Excel = 7
        PowerPoint = 8
        TXT = 9
        Image = 10
        Zip = 11
        Others = 12
    End Enum

#Region "LOAD DATA"

    Private Sub frmFISBakupList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Me.Text = "Personal File Storage"
        Me.TitleText = "<b>Personal File Storage</b>"
        Me.CenterToScreen()
        Me.btnLogin.Image = My.Resources.Login
        ' If My.Computer.FileSystem.FileExists(TokenFile) Then My.Computer.FileSystem.DeleteFile(TokenFile)
        btnLogin.Text = "Login"
        Me.lblDriveSpaceUsed.Text = ""
        Me.lblItemCount.Text = ""

        CurrentFolderPath = "\My Drive"
        ParentFolderPath = "\My Drive"

        CredentialFilePath = strAppUserPath & "\GoogleDriveAuthentication"
        JsonFile = CredentialFilePath & "\FISOAuth2.json"

        If Not FileIO.FileSystem.FileExists(JsonFile) Then 'copy from application folder
            My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
            FileSystem.FileCopy(strAppPath & "\FISOAuth2.json", CredentialFilePath & "\FISOAuth2.json")
        End If

        Me.listViewEx1.Items.Clear()
        CurrentFolderName = ""
        blUploadIsProgressing = False
        blDownloadIsProgressing = False
        blListIsLoading = False

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub CreateOAuthService() Handles btnLogin.Click
        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        Me.listViewEx1.Items.Clear()

        If Not FileIO.FileSystem.FileExists(JsonFile) Then 'if copy failed
            MessageBoxEx.Show("Authentication File is missing. Please re-install the application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        TokenFile = CredentialFilePath & "\Google.Apis.Auth.OAuth2.Responses.TokenResponse-user" ' token file is created after authentication

        If btnLogin.Text = "Logout" Then
            Me.Cursor = Cursors.WaitCursor
            ' If My.Computer.FileSystem.FileExists(TokenFile) Then My.Computer.FileSystem.DeleteFile(TokenFile)
            GDService.Dispose()
            btnLogin.Image = My.Resources.Login
            btnLogin.Text = "Login"
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Not FileIO.FileSystem.FileExists(TokenFile) Then 'check for token file.
            '  If MessageBoxEx.Show("The application will now open your browser. Please enter your gmail id and password to authenticate.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        Try
            bgwListFiles.RunWorkerAsync("root")
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub CreateServiceAndLoadData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListFiles.DoWork
        blListIsLoading = True
        Me.Cursor = Cursors.WaitCursor
        Try
            bgwListFiles.ReportProgress(1, "Waiting for User Authentication...")
            Dim fStream As FileStream = New FileStream(JsonFile, FileMode.Open, FileAccess.Read)
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim sUserCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, "user", CancellationToken.None, New FileDataStore(CredentialFilePath, True)).Result

          
            GDService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = sUserCredential, .ApplicationName = strAppName})

            If Not My.Computer.FileSystem.FileExists(TokenFile) Then
                Exit Sub
            End If
            bgwListFiles.ReportProgress(1, "Logout")
            bgwListFiles.ReportProgress(1, "Fetching Files from Google Drive...")
            SetFormTitle()
            ListFiles(e.Argument, False)
            GetDriveUsageDetails()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            blListIsLoading = False
            bgwListFiles.ReportProgress(1, "Login")
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwListFiles_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwListFiles.ProgressChanged
        Try
            If TypeOf e.UserState Is ListViewItem Then
                listViewEx1.Items.Add(e.UserState)
                If Me.listViewEx1.Items.Count = 1 Then Me.listViewEx1.Items(0).Font = New Font(Me.listViewEx1.Font, FontStyle.Bold)
            End If

            If TypeOf e.UserState Is String Then
                If e.UserState = "Waiting for User Authentication..." Then
                    ShowProgressControls("", "Waiting for User Authentication...", eCircularProgressType.Donut)
                ElseIf e.UserState = "Fetching Files from Google Drive..." Then
                    ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)
                ElseIf e.UserState = "Logout" Then
                    btnLogin.Image = My.Resources.Logout
                    btnLogin.Text = "Logout"
                ElseIf e.UserState = "Login" Then
                    btnLogin.Image = My.Resources.Login
                    btnLogin.Text = "Login"
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub bgwListFiles_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwListFiles.RunWorkerCompleted
        Me.Cursor = Cursors.Default
        HideProgressControls()
        blListIsLoading = False
        ShortenCurrentFolderPath()
        lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
    End Sub

    Private Sub ListFiles(ByVal FolderID As String, ShowTrashedFiles As Boolean)
        Try
            Dim List As FilesResource.ListRequest = GDService.Files.List()

            If ShowTrashedFiles Then
                List.Q = "trashed = true"
            Else
                List.Q = "trashed = false and '" & FolderID & "' in parents" ' list all files in parent folder. 
            End If


            List.PageSize = 1000 ' maximum file list
            List.Fields = "nextPageToken, files(id, name, mimeType, size, modifiedTime)"
            List.OrderBy = "folder, name" 'sorting order

            Dim Results As FileList = List.Execute


            Dim item As ListViewItem

            If FolderID = "root" Then
                item = New ListViewItem("\My Drive")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("root")
                item.ImageIndex = ImageIndex.GoogleDrive 'google drive icon
                bgwListFiles.ReportProgress(1, item)
            Else
                item = New ListViewItem("\" & CurrentFolderName)
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add(FolderID)

                If CurrentFolderName = "My Drive" Then
                    item.ImageIndex = ImageIndex.GoogleDrive
                Else
                    item.ImageIndex = ImageIndex.ReturnBack 'back icon
                End If

                bgwListFiles.ReportProgress(1, item)
            End If

            For Each Result In Results.Files
                item = New ListViewItem(Result.Name)
                Dim modifiedtime As DateTime = Result.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))

                If Not Result.Size Is Nothing Then
                    item.SubItems.Add(CalculateFileSize(Result.Size))
                Else
                    item.SubItems.Add("")
                End If

                item.SubItems.Add(Result.Id)

                If Result.MimeType = "application/vnd.google-apps.folder" Then ' it is a folder
                    item.ImageIndex = ImageIndex.Folder
                Else
                    item.ImageIndex = GetImageIndex(My.Computer.FileSystem.GetFileInfo(Result.Name).Extension)
                End If

                bgwListFiles.ReportProgress(2, item)

            Next

            CurrentFolderID = FolderID
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub ListViewEx1_DoubleClick(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick


        If Me.listViewEx1.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).Text = "\My Drive" Then
            Exit Sub
        End If

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If


        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 And Me.listViewEx1.SelectedItems(0).SubItems(2).Text <> "" Then
            If MessageBoxEx.Show("Do you want to download the selected file?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                DownloadSelectedFile()
                Exit Sub
            Else
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 And Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "" Then
            MessageBoxEx.Show("Cannot download zero size file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            Dim id As String = ""
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                Dim List As FilesResource.GetRequest = GDService.Files.Get(CurrentFolderID)
                List.Fields = "parents"
                Dim Result = List.Execute
                If Result.Parents Is Nothing Then
                    id = "root"
                    CurrentFolderPath = "\My Drive"
                    ParentFolderPath = "\My Drive"
                Else
                    id = Result.Parents.First
                    List = GDService.Files.Get(id)
                    List.Fields = "name"
                    ParentFolderPath = GetFullFolderPath(ParentFolderPath, CurrentFolderName, False)
                    CurrentFolderName = List.Execute.Name
                    CurrentFolderPath = ParentFolderPath
                End If
            Else
                CurrentFolderName = Me.listViewEx1.SelectedItems(0).Text
                CurrentFolderID = Me.listViewEx1.SelectedItems(0).SubItems(3).Text
                id = CurrentFolderID
                ParentFolderPath = CurrentFolderPath
                CurrentFolderPath = GetFullFolderPath(ParentFolderPath, CurrentFolderName, True)
            End If


            CircularProgress1.ProgressText = ""
            CircularProgress1.IsRunning = True
            CircularProgress1.ProgressBarType = eCircularProgressType.Donut
            CircularProgress1.Show()

            Me.listViewEx1.Items.Clear()
            Me.lblItemCount.Text = "Item Count:"
            bgwListFiles.RunWorkerAsync(id)
        Catch ex As Exception
            HideProgressControls()
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub RefreshFileList() Handles btnRefresh.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If btnLogin.Text = "Login" Then
            MessageBoxEx.Show("Please login first.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        CurrentFolderID = "root"

        CurrentFolderPath = "\My Drive"
        ParentFolderPath = "\My Drive"

        Me.listViewEx1.Items.Clear()
        Me.lblItemCount.Text = "Item Count:"
        ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)
        bgwListFiles.RunWorkerAsync("root")
    End Sub

    Private Function GetFullFolderPath(sFullFolderPath As String, sCurrentFolderName As String, blAppend As Boolean) As String
        If sCurrentFolderName = "My Drive" Then
            sFullFolderPath = "\My Drive"
        Else
            If blAppend Then sFullFolderPath = sFullFolderPath & "\" & sCurrentFolderName
            If Not blAppend Then sFullFolderPath = sFullFolderPath.Replace("\" & sCurrentFolderName, "")
        End If
        Return sFullFolderPath.Replace("\\", "\")
    End Function

    Private Function GetImageIndex(mimeType As String) As Integer
        Dim index As Integer
        Select Case mimeType.ToLower
            Case "application/x-msdownload"
                index = ImageIndex.Exe 'exe
            Case ".exe"
                index = ImageIndex.Exe 'exe
            Case "database/mdb"
                index = ImageIndex.MSAccess 'mdb
            Case ".mdb"
                index = ImageIndex.MSAccess 'mdb
            Case ".accdb"
                index = ImageIndex.MSAccess 'mdb
            Case ".pdf"
                index = ImageIndex.PDF
            Case ".docx"
                index = ImageIndex.Word
            Case ".xlsx"
                index = ImageIndex.Excel
            Case ".pptx"
                index = ImageIndex.PowerPoint
            Case ".txt"
                index = ImageIndex.TXT
            Case ".jpg"
                index = ImageIndex.Image
            Case ".jpeg"
                index = ImageIndex.Image
            Case ".png"
                index = ImageIndex.Image
            Case ".bmp"
                index = ImageIndex.Image
            Case ".zip"
                index = ImageIndex.Zip
            Case ".rar"
                index = ImageIndex.Zip
            Case Else
                index = ImageIndex.Others
        End Select
        Return index
    End Function

    Private Sub ShortenCurrentFolderPath()
        Try
            lblCurrentFolderPath.Text = CompactString(CurrentFolderPath, lblCurrentFolderPath.Width, lblCurrentFolderPath.Font, TextFormatFlags.PathEllipsis)
            If lblCurrentFolderPath.Text.Contains("...") Then lblCurrentFolderPath.Tooltip = CurrentFolderPath
        Catch ex As Exception
            lblCurrentFolderPath.Text = CurrentFolderPath
            lblCurrentFolderPath.Tooltip = CurrentFolderPath
        End Try

    End Sub

#End Region


#Region "NEW FOLDER"

    Private Sub btnNewFolder_Click(sender As Object, e As EventArgs) Handles btnNewFolder.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If btnLogin.Text = "Login" Then
            MessageBoxEx.Show("Please login first.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        frmInputBox.SetTitleandMessage("New Folder Name", "Enter Name of New Folder", False)
        frmInputBox.ShowDialog()
        Dim FolderName As String = frmInputBox.txtInputBox.Text
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub

        If Trim(FolderName) = "" Then
            MessageBoxEx.Show("Invalid folder name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If IsValidFileName(FolderName) = False Then
            MessageBoxEx.Show("Folder name contains invalid characters.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        For i = 0 To Me.listViewEx1.Items.Count - 1
            If Me.listViewEx1.Items(i).Text.ToLower = FolderName.ToLower Then
                MessageBoxEx.Show("Folder '" & FolderName & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next

        Try

            Me.Cursor = Cursors.WaitCursor


            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If


            Dim NewDirectory = New Google.Apis.Drive.v3.Data.File


            Dim parentlist As New List(Of String)
            parentlist.Add(CurrentFolderID) 'parent forlder

            NewDirectory.Parents = parentlist
            NewDirectory.Name = FolderName
            NewDirectory.MimeType = "application/vnd.google-apps.folder"


            NewDirectory = GDService.Files.Create(NewDirectory).Execute

            Dim List As FilesResource.GetRequest = GDService.Files.Get(NewDirectory.Id)
            List.Fields = "id, modifiedTime"
            Dim Result = List.Execute

            Dim item As ListViewItem

            item = New ListViewItem(NewDirectory.Name)
            Dim modifiedtime As DateTime = Result.ModifiedTime
            item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
            item.SubItems.Add("")
            item.SubItems.Add(Result.Id)

            item.ImageIndex = ImageIndex.Folder
            Me.listViewEx1.Items.Add(item)

            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
            End If
            lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region


#Region "UPLOAD FILE"

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If btnLogin.Text = "Login" Then
            MessageBoxEx.Show("Please login first.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        OpenFileDialog1.Filter = "All Files|*.*"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select File to Upload"
        OpenFileDialog1.AutoUpgradeEnabled = True
        OpenFileDialog1.RestoreDirectory = True 'remember last directory

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
            Application.DoEvents() 'first close the selection window
            uSelectedFile = OpenFileDialog1.FileName
        Else
            Exit Sub
        End If

        For i = 0 To Me.listViewEx1.Items.Count - 1
            If Me.listViewEx1.Items(i).Text.ToLower = My.Computer.FileSystem.GetFileInfo(uSelectedFile).Name.ToLower Then
                MessageBoxEx.Show("File '" & My.Computer.FileSystem.GetFileInfo(uSelectedFile).Name & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next

        dFileSize = My.Computer.FileSystem.GetFileInfo(uSelectedFile).Length
        dFormatedFileSize = CalculateFileSize(dFileSize)

        If dFileSize >= 25 * 1048576 Then '25MB
            If MessageBoxEx.Show("File size is larger than 25MB. The upload may take time. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ShowProgressControls("0", "Uploading File...", eCircularProgressType.Line)
        System.Threading.Thread.Sleep(500)
        bgwUploadFile.RunWorkerAsync(uSelectedFile)

    End Sub

    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUploadFile.DoWork
        Try
            blUploadIsProgressing = True

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = My.Computer.FileSystem.GetFileInfo(e.Argument).Name
            Dim extension As String = My.Computer.FileSystem.GetFileInfo(e.Argument).Extension
            body.MimeType = "files/" & extension.Replace(".", "")


            Dim parentlist As New List(Of String)
            parentlist.Add(CurrentFolderID)
            body.Parents = parentlist

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(e.Argument)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            Dim UploadRequest As FilesResource.CreateMediaUpload = GDService.Files.Create(body, Stream, body.MimeType)
            UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize

            AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

            UploadRequest.Fields = "id, name, mimeType, size, modifiedTime"
            UploadRequest.Upload()

            If uUploadStatus = UploadStatus.Completed Then
                Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody
                Dim item As ListViewItem = New ListViewItem(file.Name)
                Dim modifiedtime As DateTime = file.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                item.SubItems.Add(CalculateFileSize(file.Size))
                item.SubItems.Add(file.Id)
                item.ImageIndex = GetImageIndex(extension)
                bgwUploadFile.ReportProgress(100, item)
            End If

            Stream.Close()

            If uUploadStatus = UploadStatus.Completed Then
                GetDriveUsageDetails()
            End If
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
        bgwUploadFile.ReportProgress(percent, uBytesUploaded)
    End Sub

    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUploadFile.ProgressChanged
        CircularProgress1.ProgressText = e.ProgressPercentage
        lblProgressStatus.Text = CalculateFileSize(uBytesUploaded) & "/" & dFormatedFileSize
        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If
    End Sub
    Private Sub bgwUploadFile_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUploadFile.RunWorkerCompleted

        HideProgressControls()
        blUploadIsProgressing = False

        If uUploadStatus = UploadStatus.Completed Then
            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
            End If
            MessageBoxEx.Show("File uploaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "DOWNLOAD FILE"

    Private Sub DownloadSelectedFile() Handles btnDownloadFile.Click


        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If Me.listViewEx1.Items.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then
            MessageBoxEx.Show("Cannot download Folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "" Then
            MessageBoxEx.Show("Cannot download zero size file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ShowProgressControls("0", "Downloading File...", eCircularProgressType.Line)

        Dim fname As String = Me.listViewEx1.SelectedItems(0).Text
        SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & fname

        bgwDownloadFile.RunWorkerAsync(Me.listViewEx1.SelectedItems(0).SubItems(3).Text) ' fileid
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadFile.DoWork


        Try
            blDownloadIsProgressing = True

            Dim request = GDService.Files.Get(e.Argument)
            request.Fields = "size"
            Dim file = request.Execute

            dFileSize = file.Size
            dFormatedFileSize = CalculateFileSize(dFileSize)

            Dim fStream = New System.IO.FileStream(SaveFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
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
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        Dim percent = CInt((dBytesDownloaded / dFileSize) * 100)
        bgwDownloadFile.ReportProgress(percent, dBytesDownloaded)

    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadFile.ProgressChanged

        CircularProgress1.ProgressText = e.ProgressPercentage
        lblProgressStatus.Text = CalculateFileSize(dBytesDownloaded) & "/" & dFormatedFileSize
    End Sub
    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadFile.RunWorkerCompleted

        HideProgressControls()
        blDownloadIsProgressing = False

        If dDownloadStatus = DownloadStatus.Completed Then
            MessageBoxEx.Show("File downloaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call Shell("explorer.exe /select," & SaveFileName, AppWinStyle.NormalFocus)
        End If
        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "DELETE FILE"

    Private Sub DeleteSelectedFile(sender As Object, e As EventArgs) Handles btnRemoveFile.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If Me.listViewEx1.Items.Count = 0 Then
            MessageBoxEx.Show("No files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems.Count = 0 Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim SelectedItemText As String = Me.listViewEx1.SelectedItems(0).Text
        SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index

        Dim blSelectedItemIsFolder As Boolean = False
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then blSelectedItemIsFolder = True

        If SelectedItemText.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Dim msg As String = ""


        If blSelectedItemIsFolder Then
            msg = "Do you really want to remove the selected folder?"
        Else
            msg = "Do you really want to remove the selected file?"
        End If

        Dim result As DialogResult = MessageBoxEx.Show(msg, strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If result = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Try

            RemoveFile(listViewEx1.Items(SelectedFileIndex).SubItems(3).Text, True)
            Me.listViewEx1.Items(SelectedFileIndex).Remove()
            lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1

            If blSelectedItemIsFolder Then
                msg = "Selected folder deleted from Google Drive."
            Else
                msg = "Selected file deleted from Google Drive."
            End If
            frmMainInterface.ShowDesktopAlert(msg)

            Me.Cursor = Cursors.Default

            SelectNextItem(SelectedFileIndex)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub RemoveFile(FileID As String, SendToTrash As Boolean)
        Try
            If SendToTrash Then
                Dim tFile = New Google.Apis.Drive.v3.Data.File
                tFile.Trashed = True
                GDService.Files.Update(tFile, FileID).Execute() 'move the file to trash
            Else
                GDService.Files.Delete(FileID).Execute()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
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


#Region "USER EMAIL AND FILE SIZE"

    Private Sub GetDriveUsageDetails()
        Try
            Dim request = GDService.About.Get
            request.Fields = "storageQuota"
            Dim abt = request.Execute
            Me.lblDriveSpaceUsed.Text = "Drive Space used: " & CalculateFileSize(abt.StorageQuota.UsageInDrive) & "/" & CalculateFileSize(abt.StorageQuota.Limit)
        Catch ex As Exception
            Me.lblDriveSpaceUsed.Text = ""
        End Try
    End Sub

    Private Sub SetFormTitle()
        Try
            Dim request = GDService.About.Get
            request.Fields = "user"
            Dim abt = request.Execute
            Dim email = abt.User.EmailAddress
            Me.Text = "Personal File Storage - " & email
            Me.TitleText = "<b>Personal File Storage - " & email & "</b>"
        Catch ex As Exception
            Me.Text = "Personal File Storage"
            Me.TitleText = "<b>Personal File Storage</b>"
        End Try
    End Sub


#End Region


#Region "RENAME FILES"
    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If Me.listViewEx1.Items.Count = 0 Then
            MessageBoxEx.Show("No files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems.Count = 0 Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim SelectedItemText As String = Me.listViewEx1.SelectedItems(0).Text

        Dim blSelectedItemIsFolder As Boolean = False
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then blSelectedItemIsFolder = True

        If SelectedItemText.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim msg1 As String = "file"
        If blSelectedItemIsFolder Then msg1 = "folder"


        Dim oldfilename As String = Me.listViewEx1.SelectedItems(0).Text
        Dim extension As String = ""

        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 Then
            extension = My.Computer.FileSystem.GetFileInfo(oldfilename).Extension
            If extension <> "" Then oldfilename = oldfilename.Replace(extension, "")
        End If

        frmInputBox.SetTitleandMessage("Enter New Name", "Enter New Name", False, oldfilename)
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub

        Dim newfilename As String = frmInputBox.txtInputBox.Text

        If Trim(newfilename) = "" Then
            MessageBoxEx.Show("Invalid " & msg1 & " name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If IsValidFileName(newfilename) = False Then
            MessageBoxEx.Show("File name contains invalid characters.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If newfilename = oldfilename Then
            Exit Sub
        End If

        If Not blSelectedItemIsFolder Then newfilename = newfilename & extension

        Dim SelectedItemIndex As Integer = Me.listViewEx1.SelectedItems(0).Index
        For i = 0 To Me.listViewEx1.Items.Count - 1
            If i <> SelectedItemIndex And Me.listViewEx1.Items(i).Text.ToLower = newfilename.ToLower Then
                MessageBoxEx.Show("Another " & msg1 & " with name '" & newfilename & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            Dim request As New Google.Apis.Drive.v3.Data.File   'FISService.Files.Get(InstallerFileID).Execute
            request.Name = newfilename
            SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
            Dim id As String = listViewEx1.SelectedItems(0).SubItems(3).Text
            GDService.Files.Update(request, id).Execute()

            Dim List As FilesResource.GetRequest = GDService.Files.Get(id)
            List.Fields = "id, name, modifiedTime"
            Dim Result = List.Execute

            Me.listViewEx1.Items(SelectedFileIndex).Text = Result.Name
            Dim modifiedtime As DateTime = Result.ModifiedTime
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(1).Text = modifiedtime.ToString("dd-MM-yyyy HH:mm:ss")

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region

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


End Class