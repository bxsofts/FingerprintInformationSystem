Imports DevComponents.DotNetBar
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Net
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
    Dim SelectedFolderName As String = ""

    Dim TotalFileCount As Integer = 0
    Dim ShowStatusText As Boolean

    Dim blShowSharedWithMe As Boolean = False
    Dim blViewFile As Boolean = False

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

    Private Sub frmPersonalFileStorage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If blDownloadIsProgressing Then
            On Error Resume Next
            If MessageBoxEx.Show("File download is in progress. Do you want to close the window?.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If bgwDownloadFile.IsBusy Then
                    bgwDownloadFile.CancelAsync()
                End If
                If bgwDownloadFolder.IsBusy Then
                    bgwDownloadFolder.CancelAsync()
                End If
            End If
        End If

        If blUploadIsProgressing Then
            If MessageBoxEx.Show("File upload is in progress. Do you want to close the window?.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If bgwUploadFile.IsBusy Then
                    bgwUploadFile.CancelAsync()
                End If
            End If
        End If

    End Sub

    Private Sub frmFISBakupList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Me.Text = "Personal File Storage - " & oAuthUserID
        Me.TitleText = "<b>Personal File Storage - " & oAuthUserID & "</b>"
        Me.CenterToScreen()

        SideNav1.SelectedItem = btnMyFiles

        Me.PictureBox1.Hide()

        ' If My.Computer.FileSystem.FileExists(TokenFile) Then My.Computer.FileSystem.DeleteFile(TokenFile)
        btnLogin.Text = "Google Login"
        Me.lblDriveSpaceUsed.Text = ""
        Me.lblItemCount.Text = ""

        CurrentFolderPath = "\My Drive"
        ParentFolderPath = "\My Drive"
        CurrentFolderName = "My Drive"

        JsonFile = CredentialFilePath & "\FISOAuth2.json"

        If Not FileIO.FileSystem.FileExists(JsonFile) Then 'copy from application folder
            My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
            FileSystem.FileCopy(strAppPath & "\FISOAuth2.json", CredentialFilePath & "\FISOAuth2.json")
        End If

        blShowSharedWithMe = False

        Me.CircularProgress1.Parent = Me.listViewEx1
        Me.lblProgressStatus.Parent = Me.listViewEx1
        Me.lblProgressStatus.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        Me.listViewEx1.Items.Clear()
        blUploadIsProgressing = False
        blDownloadIsProgressing = False
        blListIsLoading = False
        CreateOAuthService()
        Me.Cursor = Cursors.Default


    End Sub

    Private Sub CreateOAuthService() Handles btnLogin.Click
        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

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



        If btnLogin.Text = "Google Logout" Then

            If MessageBoxEx.Show("Logout is not necessary. If you logout, you will need to enter the Google credentials again for Login. Do you want to Logout?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If My.Computer.FileSystem.FileExists(TokenFile) Then My.Computer.FileSystem.DeleteFile(TokenFile)
            GDService.Dispose()
            btnLogin.Text = "Google Login"
            Me.listViewEx1.Items.Clear()
            PictureBox1.Image = Nothing
            PictureBox1.Hide()
            Me.Cursor = Cursors.Default
            ShowDesktopAlert("Logged out successfully")
            Exit Sub
        End If

        Me.listViewEx1.Items.Clear()
        TokenFile = CredentialFilePath & "\Google.Apis.Auth.OAuth2.Responses.TokenResponse-" & oAuthUserID ' token file is created after authentication


        If Not FileIO.FileSystem.FileExists(TokenFile) Then 'check for token file.
            If MessageBoxEx.Show("The application will now open your browser. Please enter your Google ID and password to authenticate.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

        End If

        Me.Cursor = Cursors.WaitCursor
        ShowStatusText = True
        Try
            bgwListFiles.RunWorkerAsync("root")
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub CreateServiceAndLoadData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListFiles.DoWork
        blListIsLoading = True
        Try
            bgwListFiles.ReportProgress(1, "Waiting for User Authentication...")
            Dim fStream As FileStream = New FileStream(JsonFile, FileMode.Open, FileAccess.Read)
            Dim Scopes As String() = {DriveService.Scope.Drive}

            '  Dim sUserCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, UserName, CancellationToken.None, New FileDataStore(CredentialFilePath, True)).Result

            Dim sUserCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, oAuthUserID, CancellationToken.None, New FileDataStore(strAppName)).Result

            GDService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = sUserCredential, .ApplicationName = strAppName})

            If GDService.ApplicationName <> strAppName Then
                bgwListFiles.ReportProgress(1, "Authentication failed.")
                Exit Sub
            End If

            bgwListFiles.ReportProgress(1, "Logout")
            If ShowStatusText Then
                bgwListFiles.ReportProgress(1, "Fetching Files from Google Drive...")
            Else
                bgwListFiles.ReportProgress(1, "")
            End If

            GetUserInfoAndSetFormTitle()
            ListFiles(e.Argument, False)
            GetDriveUsageDetails()

        Catch ex As Exception
            blListIsLoading = False
            bgwListFiles.ReportProgress(1, "Login")
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
                ElseIf e.UserState = "Authentication failed." Then
                    MessageBoxEx.Show("Authentication failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf e.UserState = "Fetching Files from Google Drive..." Then
                    ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)
                ElseIf e.UserState = "Logout" Then
                    btnLogin.Text = "Google Logout"
                ElseIf e.UserState = "Login" Then
                    btnLogin.Text = "Google Login"
                ElseIf e.UserState = "" Then
                    ShowProgressControls("", "", eCircularProgressType.Donut)
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
                If FolderID = "root" Then
                    If blShowSharedWithMe Then
                        List.Q = "trashed = false and sharedWithMe = true"
                    Else
                        List.Q = "trashed = false and 'root' in parents" ' list all files in root
                    End If
                Else
                    List.Q = "trashed = false and '" & FolderID & "' in parents" ' list all files in parent folder. 
                End If

            End If


            List.PageSize = 1000 ' maximum file list
            List.Fields = "nextPageToken, files(id, name, mimeType, size, modifiedTime, owners)"
            List.OrderBy = "folder, name" 'sorting order

            Dim Results As FileList = List.Execute

            Dim item As ListViewItem

            If FolderID = "root" Then
                item = New ListViewItem("\My Drive")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("root")
                item.SubItems.Add("")
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

                If Not Result.Size Is Nothing Then
                    item.SubItems.Add(CalculateFileSize(Result.Size))
                Else
                    item.SubItems.Add("")
                End If

                item.SubItems.Add(Result.Id)

                If blShowSharedWithMe Then
                    Dim owner = Result.Owners(0)
                    item.SubItems.Add(owner.DisplayName)
                Else
                    item.SubItems.Add("")
                End If


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


    Private Sub ListViewEx1_DoubleClick(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick, btnView.Click


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


        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 And Me.listViewEx1.SelectedItems(0).SubItems(2).Text <> "" And Me.listViewEx1.SelectedItems(0).SubItems(2).Text <> "0B" Then
            If MessageBoxEx.Show("Do you want to download and view the selected file?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                blViewFile = True
                DownloadSelectedFile()
                Exit Sub
            Else
                blViewFile = False
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 And (Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "" Or Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "0B") Then
            MessageBoxEx.Show("Cannot download zero size file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ShowStatusText = False
        Try
            Dim id As String = ""
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                Dim List As FilesResource.GetRequest = GDService.Files.Get(CurrentFolderID)
                List.Fields = "parents"
                Dim Result = List.Execute
                If Result.Parents Is Nothing Then
                    id = "root"
                    CurrentFolderName = "My Drive"
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


    Private Sub RefreshFileList() Handles btnRefresh.Click, btnRefreshCM.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If btnLogin.Text = "Google Login" Then
            MessageBoxEx.Show("Please login using Google ID. Press 'Google Login' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        CurrentFolderID = "root"

        CurrentFolderName = "My Drive"
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

    Private Sub btnNewFolder_Click(sender As Object, e As EventArgs) Handles btnNewFolder.Click, btnNewFolderCM.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If blShowSharedWithMe Then
            MessageBoxEx.Show("New folder creation is not allowed in 'Shared with Me' folder", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If btnLogin.Text = "Google Login" Then
            MessageBoxEx.Show("Please login using Google ID. Press 'Google Login' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        frmInputBox.SetTitleandMessage("New Folder Name", "Enter Name of New Folder", False, "")
        frmInputBox.ShowDialog()
        Dim FolderName As String = frmInputBox.txtInputBox.Text
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub

        If Trim(FolderName) = "" Then
            MessageBoxEx.Show("Invalid folder name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        '   If IsValidFileName(FolderName) = False Then
        'MessageBoxEx.Show("Folder name contains invalid characters.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '  Exit Sub
        '  End If

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
            ShowDesktopAlert("New Folder created successfully.")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region


#Region "UPLOAD FILE"

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click, btnUploadCM.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If blShowSharedWithMe Then
            MessageBoxEx.Show("File upload is not allowed in 'Shared with Me' folder", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If btnLogin.Text = "Google Login" Then
            MessageBoxEx.Show("Please login using Google ID. Press 'Google Login' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        OpenFileDialog1.Filter = "All Files|*.*"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select File to Upload"
        OpenFileDialog1.AutoUpgradeEnabled = True
        OpenFileDialog1.RestoreDirectory = True 'remember last directory

       Dim FileList() As String

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
            Application.DoEvents() 'first close the selection window
            FileList = OpenFileDialog1.FileNames
        Else
            Exit Sub
        End If

        If FileList.Length = 1 Then
            For i = 0 To Me.listViewEx1.Items.Count - 1
                If FileAlreadyExists(FileList(0)) Then
                    MessageBoxEx.Show("File '" & My.Computer.FileSystem.GetFileInfo(FileList(0)).Name & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Next
        End If
       

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ShowProgressControls("0", "Uploading Files...", eCircularProgressType.Line)
        System.Threading.Thread.Sleep(200)
        bgwUploadFile.RunWorkerAsync(FileList)

    End Sub

    Private Function FileAlreadyExists(sFileName As String) As Boolean
        For i = 0 To Me.listViewEx1.Items.Count - 1
            If Me.listViewEx1.Items(i).Text.ToLower = My.Computer.FileSystem.GetFileInfo(sFileName).Name.ToLower Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUploadFile.DoWork
        Try
            blUploadIsProgressing = True

            Dim FileList() As String = e.Argument
            TotalFileCount = FileList.Count

            For i = 0 To TotalFileCount - 1

                Dim SelectedFile As String = FileList(i)
                Dim SelectedFileName As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Name

                If FileAlreadyExists(SelectedFileName) Then
                    bgwUploadFile.ReportProgress(i + 1, "Skipping existing file.")
                    Continue For
                End If

                dFileSize = My.Computer.FileSystem.GetFileInfo(SelectedFile).Length
                dFormatedFileSize = CalculateFileSize(dFileSize)

                Dim body As New Google.Apis.Drive.v3.Data.File()
                body.Name = SelectedFileName
                Dim extension As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Extension
                body.MimeType = "files/" & extension.Replace(".", "")


                Dim parentlist As New List(Of String)
                parentlist.Add(CurrentFolderID)
                body.Parents = parentlist

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(SelectedFile)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                uBytesUploaded = 0
                bgwUploadFile.ReportProgress(i + 1, SelectedFileName)
                bgwUploadFile.ReportProgress(i + 1, uBytesUploaded)

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
            Next
            

            If uUploadStatus = UploadStatus.Completed Then
                GetDriveUsageDetails()
            End If
        Catch ex As Exception
            blUploadIsProgressing = False
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

        If TypeOf e.UserState Is String Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalFileCount
            lblProgressStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is Long Then
            lblProgressStatus.Text = CalculateFileSize(uBytesUploaded) & "/" & dFormatedFileSize
        End If

        If TypeOf e.UserState Is ListViewItem Then
            listViewEx1.Items.Add(e.UserState)
        End If
    End Sub
    Private Sub bgwUploadFile_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUploadFile.RunWorkerCompleted

        HideProgressControls()
        blUploadIsProgressing = False

        If uUploadStatus = UploadStatus.Completed Then
            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.SelectedItems.Clear()
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
            End If
            If TotalFileCount = 1 Then ShowDesktopAlert("File uploaded successfully.")
            If TotalFileCount > 1 Then ShowDesktopAlert("Files uploaded successfully.")
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub listViewEx1_DragDrop(sender As Object, e As DragEventArgs) Handles listViewEx1.DragDrop

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If blShowSharedWithMe Then
                MessageBoxEx.Show("File upload is not allowed in 'Shared with Me' folder", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If btnLogin.Text = "Google Login" Then
                MessageBoxEx.Show("Please login using Google ID. Press 'Google Login' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim filePaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

             For i = 0 To filePaths.Length - 1
                If My.Computer.FileSystem.DirectoryExists(filePaths(i)) Then
                    MessageBoxEx.Show("Folder drop is not allowed. Drop file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Next

            If filePaths.Length = 1 Then
                For i = 0 To Me.listViewEx1.Items.Count - 1
                    If FileAlreadyExists(filePaths(0)) Then
                        MessageBoxEx.Show("File '" & My.Computer.FileSystem.GetFileInfo(filePaths(0)).Name & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Next
            End If

            Me.Cursor = Cursors.WaitCursor

            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ShowProgressControls("0", "Uploading File...", eCircularProgressType.Line)
            System.Threading.Thread.Sleep(200)
            bgwUploadFile.RunWorkerAsync(filePaths)

        End If
    End Sub

    Private Sub listViewEx1_DragEnter(sender As Object, e As DragEventArgs) Handles listViewEx1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
#End Region


#Region "DOWNLOAD FILE"

    Private Sub DownloadSelectedFile() Handles btnDownloadFile.Click, btnDownloadCM.Click

        blViewFile = False

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
            Dim r = MessageBoxEx.Show("Do you want to download files in the selected folder?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
            ShowProgressControls("0", "Downloading Folder...", eCircularProgressType.Line)
            SelectedFolderName = Me.listViewEx1.SelectedItems(0).Text
            bgwDownloadFolder.RunWorkerAsync(Me.listViewEx1.SelectedItems(0).SubItems(3).Text) ' folderid
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "" Or Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "0B" Then
            MessageBoxEx.Show("Cannot download zero size file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems.Count = 1 Then
            SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Me.listViewEx1.SelectedItems(0).SubItems(0).Text

            If My.Computer.FileSystem.FileExists(SaveFileName) Then
                Dim r = MessageBoxEx.Show("Selected file already exists in 'My Documents' folder. Do you want to download it again?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)
                If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If r = Windows.Forms.DialogResult.No Then
                    Call Shell("explorer.exe /select," & SaveFileName, AppWinStyle.NormalFocus)
                    Exit Sub
                End If
            End If
        End If

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        ShowProgressControls("0", "Downloading File...", eCircularProgressType.Line)

        bgwDownloadFile.RunWorkerAsync(Me.listViewEx1.SelectedItems)
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadFile.DoWork


        Try
            blDownloadIsProgressing = True

            Dim SelectedItems As ListView.SelectedListViewItemCollection
            SelectedItems = e.Argument
            TotalFileCount = SelectedItems.Count

            For i = 0 To TotalFileCount - 1
                Dim fname As String = SelectedItems(i).SubItems(0).Text
                Dim fid As String = SelectedItems(i).SubItems(3).Text

                SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & fname

                If My.Computer.FileSystem.FileExists(SaveFileName) Then
                    bgwDownloadFile.ReportProgress(i + 1, "Skipping existing file.")
                End If

                Dim request = GDService.Files.Get(fid)
                request.Fields = "size"
                Dim file = request.Execute

                dFileSize = file.Size
                dFormatedFileSize = CalculateFileSize(dFileSize)
                dBytesDownloaded = 0
                bgwDownloadFile.ReportProgress(i + 1, fname)
                bgwDownloadFile.ReportProgress(i + 1, dBytesDownloaded)

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
            Next

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
        bgwDownloadFile.ReportProgress(percent, dBytesDownloaded)

    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadFile.ProgressChanged

        If TypeOf e.UserState Is String Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalFileCount
            lblProgressStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is Long Then
            lblProgressStatus.Text = CalculateFileSize(dBytesDownloaded) & "/" & dFormatedFileSize
        End If

    End Sub
    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadFile.RunWorkerCompleted

        HideProgressControls()
        blDownloadIsProgressing = False

        If dDownloadStatus = DownloadStatus.Completed Then
            If TotalFileCount = 1 Then ShowDesktopAlert("File downloaded successfully.")
            If TotalFileCount > 1 Then ShowDesktopAlert("Files downloaded successfully.")

            If blViewFile Then
                If My.Computer.FileSystem.FileExists(SaveFileName) Then
                    Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                Else
                    MessageBoxEx.Show("Cannot open file. File is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                Call Shell("explorer.exe /select," & SaveFileName, AppWinStyle.NormalFocus)
            End If

        End If
        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.Cursor = Cursors.Default
    End Sub



#End Region


#Region "DOWNLOAD FOLDER"
    Private Sub bgwDownloadFolder_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadFolder.DoWork
        Try

            blDownloadIsProgressing = True

            Dim folderid As String = e.Argument

            Dim List As FilesResource.ListRequest = GDService.Files.List()

            List.Q = "trashed = false and mimeType != 'application/vnd.google-apps.folder' and'" & folderid & "' in parents" ' list all files in parent folder. 


            List.PageSize = 1000 ' maximum file list
            List.Fields = "files(id, name, mimeType, size)"
            List.OrderBy = "name" 'sorting order

            Dim Results As FileList = List.Execute

            TotalFileCount = Results.Files.Count

            If TotalFileCount = 0 Then
                Exit Sub
            End If

            Dim SaveFolder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & SelectedFolderName
            My.Computer.FileSystem.CreateDirectory(SaveFolder)

            For i = 0 To TotalFileCount - 1

                If bgwDownloadFolder.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                Dim Result = Results.Files(i)

                dFileSize = Result.Size
                Dim dFileID As String = Result.Id
                Dim dFileName As String = Result.Name

                SaveFileName = SaveFolder & "\" & dFileName

                If My.Computer.FileSystem.FileExists(SaveFileName) Then
                    Dim fsize As Long = My.Computer.FileSystem.GetFileInfo(SaveFileName).Length
                    If fsize = dFileSize Then
                        bgwDownloadFolder.ReportProgress(i + 1, "Skipping existing file.")
                        Continue For
                    Else
                        bgwDownloadFolder.ReportProgress(i + 1, "Re-downloading file.")
                    End If
                End If

                If dFileSize > 0 Then

                    bgwDownloadFolder.ReportProgress(i + 1, dFileName)

                    dFormatedFileSize = CalculateFileSize(dFileSize)
                    dBytesDownloaded = 0
                    bgwDownloadFolder.ReportProgress(i + 1, dBytesDownloaded)

                    Dim fStream = New System.IO.FileStream(SaveFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                    Dim mStream = New System.IO.MemoryStream

                    Dim request = GDService.Files.Get(dFileID)

                    Dim m = request.MediaDownloader
                    m.ChunkSize = 256 * 1024

                    AddHandler m.ProgressChanged, AddressOf FolderDownload_ProgressChanged
                    request.DownloadWithStatus(mStream)

                    If dDownloadStatus = DownloadStatus.Completed Then
                        mStream.WriteTo(fStream)
                    End If

                    fStream.Close()
                    mStream.Close()
                End If
            Next

        Catch ex As Exception
            blDownloadIsProgressing = False
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub FolderDownload_ProgressChanged(Progress As IDownloadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        Dim percent = CInt((dBytesDownloaded / dFileSize) * 100)
        bgwDownloadFolder.ReportProgress(percent, dBytesDownloaded)

    End Sub

    Private Sub bgwDownloadFolder_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadFolder.ProgressChanged

        If TypeOf e.UserState Is String Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalFileCount
            lblProgressStatus.Text = e.UserState
        End If

        If TypeOf e.UserState Is Long Then
            lblProgressStatus.Text = CalculateFileSize(dBytesDownloaded) & "/" & dFormatedFileSize
        End If

    End Sub

    Private Sub bgwDownloadFolder_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadFolder.RunWorkerCompleted

        Me.Cursor = Cursors.Default
        HideProgressControls()
        blDownloadIsProgressing = False

        If TotalFileCount = 0 Then
            MessageBoxEx.Show("No files in the folder")
            Exit Sub
        End If

        Dim folder = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & SelectedFolderName
        If My.Computer.FileSystem.DirectoryExists(folder) Then
            Call Shell("explorer.exe " & folder, AppWinStyle.NormalFocus)
        End If

    End Sub
#End Region


#Region "DELETE FILE"

    Private Sub DeleteSelectedFile(sender As Object, e As EventArgs) Handles btnRemoveFile.Click, btnRemoveCM.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        Dim selectedcount As Integer = Me.listViewEx1.SelectedItems.Count

        If blShowSharedWithMe Then
            MessageBoxEx.Show("Deletion is not allowed in 'Shared with Me' folder", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.Items.Count = 0 Then
            MessageBoxEx.Show("No files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If selectedcount = 0 Then
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
            If selectedcount = 1 Then
                msg = "Do you really want to remove the selected file?"
            Else
                msg = "Do you really want to remove the selected files?"
            End If
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

            If selectedcount > 1 Then
                ShowPleaseWaitForm()
            End If

            For i = 0 To selectedcount - 1
                SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
                RemoveFile(listViewEx1.SelectedItems(0).SubItems(3).Text, False)
                Me.listViewEx1.SelectedItems(0).Remove()
            Next

            ClosePleaseWaitForm()

            lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1

            If blSelectedItemIsFolder Then
                msg = "Selected folder deleted from Google Drive."
            Else
                msg = "Selected file deleted from Google Drive."
            End If
            ShowDesktopAlert(msg)

            Me.Cursor = Cursors.Default

            SelectNextItem(SelectedFileIndex)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Function RemoveFile(FileID As String, SendToTrash As Boolean) As Boolean
        Try
            If SendToTrash Then
                Dim tFile = New Google.Apis.Drive.v3.Data.File
                tFile.Trashed = True
                GDService.Files.Update(tFile, FileID).Execute() 'move the file to trash
            Else
                GDService.Files.Delete(FileID).Execute()
            End If
            Return True
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
            Return False
        End Try
    End Function

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

    Private Sub GetUserInfoAndSetFormTitle()
        Try
            Dim request = GDService.About.Get
            request.Fields = "user"
            Dim abt = request.Execute
            Dim email = abt.User.EmailAddress
            Me.Text = "Personal File Storage - " & oAuthUserID & " - " & email
            Me.TitleText = "<b>Personal File Storage - " & oAuthUserID & " - " & email & "</b>"
            SetUserImage(abt.User.PhotoLink)
        Catch ex As Exception
            Me.Text = "Personal File Storage - " & oAuthUserID
            Me.TitleText = "<b>Personal File Storage - " & oAuthUserID & "</b>"
        End Try
    End Sub

    Private Sub SetUserImage(imagelink As String)
        Try
            Dim wClient As WebClient = New WebClient
            Dim wImage As Bitmap = Bitmap.FromStream(New MemoryStream(wClient.DownloadData(imagelink)))
            PictureBox1.Image = wImage
            PictureBox1.Show()
        Catch ex As Exception
            PictureBox1.Hide()
        End Try
    End Sub
#End Region


#Region "RENAME FILES"
    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click, btnRenameCM.Click

        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If blShowSharedWithMe Then
            MessageBoxEx.Show("Rename is not allowed in 'Shared with Me' folder", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        If Me.listViewEx1.SelectedItems.Count > 1 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim SelectedItemText As String = Me.listViewEx1.SelectedItems(0).Text

        Dim blSelectedItemIsFolder As Boolean = False
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then blSelectedItemIsFolder = True

        If SelectedItemText.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim ftype As String = "file"
        If blSelectedItemIsFolder Then ftype = "folder"


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
            MessageBoxEx.Show("Invalid " & ftype & " name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If IsValidFileName(newfilename) = False And Not blSelectedItemIsFolder Then
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
                MessageBoxEx.Show("Another " & ftype & " with name '" & newfilename & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            ShowDesktopAlert("Selected " & ftype & " renamed successfully.")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


#Region "SHARE FILES"
    Private Sub btnShareFile_Click(sender As Object, e As EventArgs) Handles btnShareFile.Click, btnShareCM.Click
        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If blShowSharedWithMe Then
            MessageBoxEx.Show("Files in 'Shared with Me' folder cannot be shared.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        If Me.listViewEx1.SelectedItems.Count > 1 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim SelectedItemText As String = Me.listViewEx1.SelectedItems(0).Text

        Dim blSelectedItemIsFolder As Boolean = False
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then blSelectedItemIsFolder = True

        If SelectedItemText.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim ftype As String = "file"
        If blSelectedItemIsFolder Then ftype = "folder"

        frmInputBox.SetTitleandMessage("Share File", "Enter email id of recepient", False)
        frmInputBox.ShowDialog()
        Dim email As String = frmInputBox.txtInputBox.Text
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If Not ValidEmail(email) Then
            MessageBoxEx.Show("Invalid email id.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor

        If (ShareFile(listViewEx1.SelectedItems(0).SubItems(3).Text, email)) Then
            ShowDesktopAlert("Selected " & ftype & " shared successfully.")
        Else
            MessageBoxEx.Show("Sharing failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Function ValidEmail(email As String) As Boolean
        Try
            Dim a As New System.Net.Mail.MailAddress(email)
        Catch
            Return False
        End Try
        Return True
    End Function

    Private Function ShareFile(fileid As String, email As String)
        Try
            Dim userPermission As Permission = New Permission
            userPermission.Type = "user"
            userPermission.Role = "reader"
            userPermission.EmailAddress = email
            Dim request = GDService.Permissions.Create(userPermission, fileid)
            request.Fields = "id"
            request.Execute()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region


#Region "CHANGE PASSWORD"

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        If Not MessageBoxEx.Show("Enter your current local password to authenticate.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then Exit Sub

        blAuthenticatePasswordChange = True
        frmPassword.ShowDialog()

        If blUserAuthenticated Then
            If MessageBoxEx.Show("Enter new password and confirm.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                blChangeAndUpdatePassword = True
                frmPassword.ShowDialog()
            End If
        End If

        blChangeAndUpdatePassword = False
        blAuthenticatePasswordChange = False
    End Sub

#End Region

    Private Sub ShowProgressControls(ProgressText As String, StatusText As String, ProgressType As eCircularProgressType)
        CircularProgress1.ProgressText = ProgressText
        lblProgressStatus.Text = StatusText
        CircularProgress1.IsRunning = True
        CircularProgress1.ProgressColor = GetProgressColor()
        CircularProgress1.ProgressBarType = ProgressType
        CircularProgress1.Show()
        If StatusText = "" Then
            lblProgressStatus.Hide()
        Else
            lblProgressStatus.Show()
        End If

    End Sub

    Private Sub HideProgressControls()
        CircularProgress1.IsRunning = False
        CircularProgress1.ProgressText = ""
        CircularProgress1.ProgressBarType = eCircularProgressType.Line
        lblProgressStatus.Text = ""
        CircularProgress1.Hide()
        lblProgressStatus.Hide()
    End Sub


    Private Sub btnSharedWithMe_Click(sender As Object, e As EventArgs) Handles btnSharedWithMe.Click
        Me.listViewEx1.Columns(4).Text = "Shared By"
        Me.listViewEx1.Parent = Me.SideNavPanel2
        blShowSharedWithMe = True
        RefreshFileList()
    End Sub

    Private Sub btnMyFiles_Click(sender As Object, e As EventArgs) Handles btnMyFiles.Click
        Me.listViewEx1.Columns(4).Text = ""
        Me.listViewEx1.Parent = Me.SideNavPanel1
        blShowSharedWithMe = False
        RefreshFileList()
    End Sub

    Private Sub listViewEx1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listViewEx1.SelectedIndexChanged
        On Error Resume Next
        Me.listViewEx1.SelectedItems(0).EnsureVisible()
    End Sub


    Private Sub ContextMenuBar1_PopupOpen(sender As Object, e As PopupOpenEventArgs) Handles ContextMenuBar1.PopupOpen
        On Error Resume Next

        Me.btnRefreshCM.Visible = False
        Me.btnNewFolderCM.Visible = False
        Me.btnUploadCM.Visible = False
        Me.btnDownloadCM.Visible = False
        Me.btnView.Visible = False
        Me.btnRemoveCM.Visible = False
        Me.btnRenameCM.Visible = False
        Me.btnShareCM.Visible = False


        If Me.listViewEx1.Items.Count = 0 Or Me.listViewEx1.SelectedItems.Count = 0 Then
            Me.btnRefreshCM.Visible = True
            Me.btnNewFolderCM.Visible = True
            Me.btnUploadCM.Visible = True
            Me.btnRemoveCM.Visible = False
        End If

        If Me.listViewEx1.SelectedItems.Count = 1 Then

            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                e.Cancel = True
            End If

            Me.btnRenameCM.Visible = True
            Me.btnShareCM.Visible = True
            Me.btnRemoveCM.Visible = True
            If Not Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then
                Me.btnDownloadCM.Visible = True
                Me.btnView.Visible = True
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