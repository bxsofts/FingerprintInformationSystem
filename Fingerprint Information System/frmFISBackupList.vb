﻿
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
Public Class frmFISBackupList

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential
    Public CredentialPath As String
    Public JsonPath As String

    Dim CurrentFolderID As String = "root"
    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus
    Public uBytesUploaded As Long
    Public uUploadStatus As UploadStatus
    Dim uSelectedFile As String = ""

    Public dFileSize As Long
    Dim dFormatedFileSize As String = ""
    Public SaveFileName As String = ""
    Dim ServiceCreated As Boolean = False
    Dim CurrentFolderName As String = ""

    Dim CurrentFolderPath As String = ""
    Dim ParentFolderPath As String = ""

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
        Others = 11
    End Enum

#Region "LOAD DATA"

    Private Sub frmFISBakupList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        SetTitleAndSize()
        Me.CenterToScreen()

        Me.lblDriveSpaceUsed.Text = ""
        Me.lblItemCount.Text = ""
        CurrentFolderPath = ""
        ParentFolderPath = ""

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

        Me.listViewEx1.Items.Clear()
        ServiceCreated = False
        CurrentFolderName = ""

        CircularProgress1.ProgressText = ""
        lblProgressStatus.Text = "Fetching Files from Google Drive..."
        CircularProgress1.IsRunning = True
        CircularProgress1.ProgressColor = GetProgressColor()
        CircularProgress1.ProgressBarType = eCircularProgressType.Donut
        CircularProgress1.Show()
        lblProgressStatus.Show()

        blUploadIsProgressing = False
        blDownloadIsProgressing = False
        blListIsLoading = False

        bgwListFiles.RunWorkerAsync("root")

    End Sub


    Private Sub CreateServiceAndLoadData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListFiles.DoWork
        blListIsLoading = True
        Me.Cursor = Cursors.WaitCursor
        Try
            If ServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                ServiceCreated = True
            End If

            ListFiles(e.Argument, False)
            GetDriveUsageDetails()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            blListIsLoading = False
            ServiceCreated = False
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwListFiles_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwListFiles.ProgressChanged
        Try


            If TypeOf e.UserState Is ListViewItem Then
                listViewEx1.Items.Add(e.UserState)
            End If

            If TypeOf e.UserState Is String Then
                lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
                Me.listViewEx1.Items(0).Font = New Font(Me.listViewEx1.Font, FontStyle.Bold)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub bgwListFiles_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwListFiles.RunWorkerCompleted
        Me.Cursor = Cursors.Default
        CircularProgress1.IsRunning = False
        CircularProgress1.ProgressText = ""
        CircularProgress1.ProgressBarType = eCircularProgressType.Line
        lblProgressStatus.Text = ""
        CircularProgress1.Hide()
        lblProgressStatus.Hide()
        blListIsLoading = False
        LabelX1.Text = CurrentFolderPath
    End Sub

    Private Sub ListFiles(ByVal FolderID As String, ShowTrashedFiles As Boolean)
        Try
            Dim List As FilesResource.ListRequest = FISService.Files.List()

            Dim showuserfileonly As String = ""

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

            If FolderID = "root" Then
                item = New ListViewItem("\My Drive")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("root")
                item.SubItems.Add("")
                CurrentFolderPath = "\My Drive"
                ParentFolderPath = "\My Drive"
                item.ImageIndex = ImageIndex.GoogleDrive 'google drive icon
                bgwListFiles.ReportProgress(1, item)
            Else
                item = New ListViewItem("\" & CurrentFolderName)
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add(FolderID)
                item.SubItems.Add("")

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

                If Result.MimeType = "application/vnd.google-apps.folder" Then ' it is a folder
                    item.SubItems.Add("") 'size for folder
                    item.ImageIndex = ImageIndex.Folder
                Else
                    item.ImageIndex = GetImageIndex(Result.MimeType)
                End If

                If item.ImageIndex > 2 Then
                    item.SubItems.Add(CalculateFileSize(Result.Size))
                End If

                item.SubItems.Add(Result.Id)

                If Result.Description = "FIS Backup Folder" Then
                    item.SubItems.Add(Result.Name)
                ElseIf IsDate(Result.Description) Or Result.Description = "" Then
                    item.SubItems.Add(CurrentFolderName)
                Else
                    item.SubItems.Add(Result.Description)
                End If


                If AdminPrevilege Or CurrentFolderName = FileOwner Or CurrentFolderName = "InstallerFile" Or CurrentFolderName = "General Files" Or item.ImageIndex = ImageIndex.Folder And Result.Name <> "VersionFolder" Then
                    bgwListFiles.ReportProgress(2, item) 'report all files
                ElseIf item.SubItems(4).Text = FileOwner Then
                    bgwListFiles.ReportProgress(2, item) 'list all folders except version folder
                End If

            Next

            bgwListFiles.ReportProgress(3, FolderID)
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

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowActionInProgressMessage()
            Exit Sub
        End If



        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 Then
            If MessageBoxEx.Show("Do you want to download the selected file?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                DownloadSelectedFile()
                Exit Sub
            Else
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            ' CurrentFolderName = ""
            Dim id As String = ""
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                Dim List As FilesResource.GetRequest = FISService.Files.Get(CurrentFolderID)
                List.Fields = "parents"
                Dim Result = List.Execute
                If Result.Parents Is Nothing Then
                    id = "root"
                    CurrentFolderPath = "\My Drive"
                    ParentFolderPath = "\My Drive"
                Else
                    id = Result.Parents.First
                    List = FISService.Files.Get(id)
                    List.Fields = "name"
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
            ' lblProgressStatus.Text = "Please wait..."
            CircularProgress1.IsRunning = True
            CircularProgress1.ProgressBarType = eCircularProgressType.Donut
            CircularProgress1.Show()
            ' lblProgressStatus.Show()

            Me.listViewEx1.Items.Clear()
            bgwListFiles.RunWorkerAsync(id)
        Catch ex As Exception
            CircularProgress1.IsRunning = False
            CircularProgress1.ProgressText = ""
            CircularProgress1.ProgressBarType = eCircularProgressType.Line
            lblProgressStatus.Text = ""
            CircularProgress1.Hide()
            lblProgressStatus.Hide()
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub


    Private Sub RefreshFileList(sender As Object, e As EventArgs) Handles btnRefresh.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowActionInProgressMessage()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        CurrentFolderID = "root"
        Me.listViewEx1.Items.Clear()
        CircularProgress1.ProgressText = ""
        lblProgressStatus.Text = "Fetching Files from Google Drive..."
        CircularProgress1.IsRunning = True
        CircularProgress1.ProgressColor = GetProgressColor()
        CircularProgress1.ProgressBarType = eCircularProgressType.Donut
        CircularProgress1.Show()
        lblProgressStatus.Show()
        bgwListFiles.RunWorkerAsync("root")
    End Sub

    Private Function GetFullFolderPath(sFullFolderPath As String, sCurrentFolderName As String, blAppend As Boolean) As String
        If sCurrentFolderName = "My Drive" Then
            sFullFolderPath = "\My Drive"
        Else
            If blAppend Then sFullFolderPath = sFullFolderPath & "\" & sCurrentFolderName
            If Not blAppend Then
                sFullFolderPath = sFullFolderPath.TrimEnd("\")

                Dim l = sFullFolderPath.Length
                Dim p = sFullFolderPath.IndexOf("\")
                If p = -1 Then p = l
                sFullFolderPath = sFullFolderPath.Remove(p, l - p)
            End If

        End If
        Return sFullFolderPath.Replace("\\", "\")
    End Function

    Private Function GetImageIndex(mimeType As String) As Integer
        Dim index As Integer
        Select Case mimeType.ToLower
            Case "application/x-msdownload"
                index = ImageIndex.Exe 'exe
            Case "files/exe"
                index = ImageIndex.Exe 'exe
            Case "database/mdb"
                index = ImageIndex.MSAccess 'mdb
            Case "files/mdb"
                index = ImageIndex.MSAccess 'mdb
            Case "files/accdb"
                index = ImageIndex.MSAccess 'mdb
            Case "files/pdf"
                index = ImageIndex.PDF
            Case "files/docx"
                index = ImageIndex.Word
            Case "files/xlsx"
                index = ImageIndex.Excel
            Case "files/pptx"
                index = ImageIndex.PowerPoint
            Case "files/txt"
                index = ImageIndex.TXT
            Case "files/jpg"
                index = ImageIndex.Image
            Case "files/jpeg"
                index = ImageIndex.Image
            Case "files/png"
                index = ImageIndex.Image
            Case "files/bmp"
                index = ImageIndex.Image
            Case Else
                index = ImageIndex.Others
        End Select
        Return index
    End Function


#End Region


#Region "NEW FOLDER"

    Private Sub btnNewFolder_Click(sender As Object, e As EventArgs) Handles btnNewFolder.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowActionInProgressMessage()
            Exit Sub
        End If

        If CurrentFolderName = "" Or CurrentFolderName = "My Drive" And Not AdminPrevilege Then
            MessageBoxEx.Show("Creation of new Folder is not allowed in 'My Drive' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderName = "InstallerFile" And Not AdminPrevilege Then
            MessageBoxEx.Show("Creation of new Folder is not allowed in 'InstallerFile' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        frmInputBox.SetTitleandMessage("New Folder Name", "Enter Name of New Folder", False)
        frmInputBox.ShowDialog()
        Dim FolderName As String = frmInputBox.txtInputBox.Text
        If frmInputBox.ButtonClicked <> "OK" Or Trim(FolderName) = "" Then Exit Sub

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
            NewDirectory.Description = FileOwner

            NewDirectory = FISService.Files.Create(NewDirectory).Execute

            Dim List As FilesResource.GetRequest = FISService.Files.Get(NewDirectory.Id)
            List.Fields = "id, modifiedTime, description"
            Dim Result = List.Execute

            Dim item As ListViewItem

            item = New ListViewItem(NewDirectory.Name)
            Dim modifiedtime As DateTime = Result.ModifiedTime
            item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
            item.SubItems.Add("")
            item.SubItems.Add(Result.Id)
            item.SubItems.Add(Result.Description)

            item.ImageIndex = ImageIndex.Folder
            Me.listViewEx1.Items.Add(item)

            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
            End If

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
            ShowActionInProgressMessage()
            Exit Sub
        End If

        If CurrentFolderName = "InstallerFile" And Not AdminPrevilege Then
            MessageBoxEx.Show("Uploading of files is not allowed in 'InstallerFile' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        CircularProgress1.ProgressBarType = eCircularProgressType.Line
        CircularProgress1.Visible = True
        CircularProgress1.ProgressText = "0"
        CircularProgress1.IsRunning = True
        lblProgressStatus.Text = "Uploading File..."
        lblProgressStatus.Visible = True

        bgwUploadFile.RunWorkerAsync(uSelectedFile)

    End Sub

    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUploadFile.DoWork
        Try
            blUploadIsProgressing = True

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = My.Computer.FileSystem.GetFileInfo(e.Argument).Name
            body.MimeType = "files/" & My.Computer.FileSystem.GetFileInfo(e.Argument).Extension.Replace(".", "")
            body.Description = FileOwner

            Dim parentlist As New List(Of String)
            parentlist.Add(CurrentFolderID)
            body.Parents = parentlist

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(e.Argument)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
            UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize

            AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

            UploadRequest.Fields = "id, name, mimeType, size, modifiedTime, description"
            UploadRequest.Upload()

            If uUploadStatus = UploadStatus.Completed Then
                Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody
                Dim item As ListViewItem = New ListViewItem(file.Name)
                Dim modifiedtime As DateTime = file.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                item.SubItems.Add(CalculateFileSize(file.Size))
                item.SubItems.Add(file.Id)
                item.SubItems.Add(file.Description)
                item.ImageIndex = GetImageIndex(file.MimeType)
                bgwUploadFile.ReportProgress(100, item)
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

        CircularProgress1.Visible = False
        lblProgressStatus.Visible = False
        blUploadIsProgressing = False

        If uUploadStatus = UploadStatus.Completed Then
            MessageBoxEx.Show("File uploaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
            End If

            GetDriveUsageDetails()

        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "DOWNLOAD FILE"

    Private Sub DownloadSelectedFile() Handles btnDownloadFile.Click


        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowActionInProgressMessage()
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

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        CircularProgress1.ProgressBarType = eCircularProgressType.Line
        CircularProgress1.Visible = True
        CircularProgress1.ProgressText = 0
        CircularProgress1.IsRunning = True
        lblProgressStatus.Text = "Downloading File..."
        lblProgressStatus.Visible = True

        Dim fname As String = Me.listViewEx1.SelectedItems(0).Text
        If fname.StartsWith("FingerPrintBackup-") And CurrentFolderName <> "" Then
            fname = CurrentFolderName & "_" & fname
        End If

        SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & fname
        bgwDownloadFile.RunWorkerAsync(Me.listViewEx1.SelectedItems(0).SubItems(3).Text) ' fileid
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadFile.DoWork


        Try
            blDownloadIsProgressing = True

            Dim request = FISService.Files.Get(e.Argument)
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

        CircularProgress1.Visible = False
        lblProgressStatus.Visible = False
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
            ShowActionInProgressMessage()
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

        If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).Text = "FIS Backup" Or Me.listViewEx1.SelectedItems(0).Text = "InstallerFile" Or Me.listViewEx1.SelectedItems(0).Text = "VersionFolder" Then
            MessageBoxEx.Show("Deletion is not allowed for the selected folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder And AdminPrevilege = False And Me.listViewEx1.SelectedItems(0).SubItems(4).Text <> FileOwner And CurrentFolderName <> FileOwner Then
            MessageBoxEx.Show("You are not authorized to delete the selected folder. You can delete folders inside '" & FileOwner & "' Folder or folders created by you only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).SubItems(4).Text <> FileOwner And AdminPrevilege = False And CurrentFolderName <> FileOwner Then
            MessageBoxEx.Show("You are not authorized to delete the selected file. You can delete files inside '" & FileOwner & "' Folder or files created by you only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Dim msg As String = ""
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then
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

            RemoveFile(listViewEx1.SelectedItems(0).SubItems(3).Text, False)
            Me.listViewEx1.SelectedItems(0).Remove()
            GetDriveUsageDetails()
            frmMainInterface.ShowDesktopAlert("Selected file deleted from Google Drive.")
            Me.Cursor = Cursors.Default
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
                FISService.Files.Update(tFile, FileID).Execute() 'move the file to trash
            Else
                FISService.Files.Delete(FileID).Execute()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "FILE SIZE"

    Private Sub GetDriveUsageDetails()
        Try
            Dim request = FISService.About.Get
            request.Fields = "storageQuota"
            Dim abt = request.Execute
            Me.lblDriveSpaceUsed.Text = "Drive Space used: " & CalculateFileSize(abt.StorageQuota.UsageInDrive) & "/" & CalculateFileSize(abt.StorageQuota.Limit)
        Catch ex As Exception
            Me.lblDriveSpaceUsed.Text = ""
        End Try
    End Sub


#End Region


#Region "RENAME FILES"
    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowActionInProgressMessage()
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

        If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).Text = "FIS Backup" Or Me.listViewEx1.SelectedItems(0).Text = "InstallerFile" Or Me.listViewEx1.SelectedItems(0).Text = "VersionFolder" Then
            MessageBoxEx.Show("Renaming is not allowed for the selected folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder And AdminPrevilege = False And Me.listViewEx1.SelectedItems(0).SubItems(4).Text <> FileOwner And CurrentFolderName <> FileOwner Then
            MessageBoxEx.Show("You are not authorized to rename the selected folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder And AdminPrevilege = False And Me.listViewEx1.SelectedItems(0).SubItems(4).Text = FileOwner And CurrentFolderName = "FIS Backup" Then
            MessageBoxEx.Show("Renaming is not allowed for the selected folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).SubItems(4).Text <> FileOwner And AdminPrevilege = False And CurrentFolderName <> FileOwner Then
            MessageBoxEx.Show("You are not authorized to rename the selected file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

       
        Dim oldfilename As String = Me.listViewEx1.SelectedItems(0).Text
        Dim extension As String = ""

        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 Then
            extension = My.Computer.FileSystem.GetFileInfo(oldfilename).Extension
            oldfilename = oldfilename.Replace(extension, "")
        End If

        frmInputBox.SetTitleandMessage("Enter New Name", "Enter New Name", False, oldfilename)
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim newfilename As String = frmInputBox.txtInputBox.Text

        Try
            Dim request As New Google.Apis.Drive.v3.Data.File   'FISService.Files.Get(InstallerFileID).Execute
            request.Name = newfilename & extension
            request.Description = FileOwner
            FISService.Files.Update(request, listViewEx1.SelectedItems(0).SubItems(3).Text).Execute()
            Me.listViewEx1.SelectedItems(0).Text = request.Name
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


    Public Sub SetTitleAndSize()
        Me.Text = "FIS Online File List - " & FileOwner
        Me.TitleText = "<b>FIS Online File List - " & FileOwner & "</b>"
        If AdminPrevilege Then
            Me.listViewEx1.Columns(3).Width = 290
            Me.Width = 1150
        Else
            Me.listViewEx1.Columns(3).Width = 0
            Me.Width = 990
        End If

        Me.CircularProgress1.Location = New Point((Me.listViewEx1.Width - Me.CircularProgress1.Width) / 2, Me.CircularProgress1.Location.Y)
        Me.lblProgressStatus.Location = New Point((Me.listViewEx1.Width - Me.lblProgressStatus.Width) / 2, Me.lblProgressStatus.Location.Y)
        Me.CenterToScreen()
        Me.BringToFront()
    End Sub

   
End Class

