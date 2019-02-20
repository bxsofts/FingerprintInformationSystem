
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

    Dim SelectedFileID As String = ""
    Dim SelectedFileIndex As Integer = 0

    Dim blPasswordFetched As Boolean = False

    Dim RecoverPassword As String = ""
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

        RecoverPassword = My.Computer.Registry.GetValue(strGeneralSettingsPath, "RecoverPassword", "0")
        If RecoverPassword = "1" Then
            FileOwner = "Admin"
            SuperAdmin = True
            LocalAdmin = False
            LocalUser = False
        End If

        If SuperAdmin Then
            Me.btnUpdateFileContent.Visible = True
        Else
            Me.btnUpdateFileContent.Visible = False
        End If

        SetTitleAndSize()
        Me.CenterToScreen()

        Me.lblDriveSpaceUsed.Text = "Drive Space used: "
        Me.lblItemCount.Text = "Item Count: "

        CurrentFolderPath = "\My Drive"
        ParentFolderPath = "\My Drive"

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
        Me.lblItemCount.Text = "Item Count: "
        ServiceCreated = False
        CurrentFolderName = ""

        ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)

        blUploadIsProgressing = False
        blDownloadIsProgressing = False
        blListIsLoading = False

        '  ImageList1.Images.Add(GetFileIcon(".exe"))
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
            If Not blPasswordFetched Then blPasswordFetched = GetAdminPasswords()

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
                If Me.listViewEx1.Items.Count = 1 Then Me.listViewEx1.Items(0).Font = New Font(Me.listViewEx1.Font, FontStyle.Bold)
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
        lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
        ShortenCurrentFolderPath()
    End Sub

    Private Sub ListFiles(ByVal FolderID As String, ShowTrashedFiles As Boolean)
        Try
            Dim List As FilesResource.ListRequest = FISService.Files.List()

            If ShowTrashedFiles Then
                List.Q = "trashed = true"
            Else
                List.Q = "trashed = false and '" & FolderID & "' in parents" ' list all files in parent folder. 
            End If


            List.PageSize = 1000 ' maximum file list
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

            Dim ResultIsFolder As Boolean = False
            For Each Result In Results.Files
                item = New ListViewItem(Result.Name)
                Dim modifiedtime As DateTime = Result.ModifiedTime
                item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))

                If Result.MimeType = "application/vnd.google-apps.folder" Then ' it is a folder
                    item.SubItems.Add("") 'size for folder
                    item.ImageIndex = ImageIndex.Folder
                    ResultIsFolder = True
                Else
                    item.ImageIndex = GetImageIndex(My.Computer.FileSystem.GetFileInfo(Result.Name).Extension)
                    ResultIsFolder = False
                End If

                If item.ImageIndex > 2 Then
                    If Not Result.Size Is Nothing Then
                        item.SubItems.Add(CalculateFileSize(Result.Size))
                    Else
                        item.SubItems.Add("")
                    End If
                End If

                item.SubItems.Add(Result.Id)

                Dim Description As String = Result.Description
                If SuperAdmin Then
                    item.SubItems.Add(Description)
                Else
                    If Description = "FIS Backup Folder" Then
                        item.SubItems.Add(Result.Name)
                    ElseIf IsDate(Description) Or Description = "" Then
                        item.SubItems.Add(CurrentFolderName)
                    Else
                        Dim SplitText() = Strings.Split(Description, "; ")
                        Dim u = SplitText.GetUpperBound(0)

                        If u >= 0 Then
                            item.SubItems.Add(SplitText(0)) 'uploaded by
                        End If
                    End If
                End If


                Select Case FileOwner
                    Case "Admin"
                        bgwListFiles.ReportProgress(2, item)
                    Case "Admin_" & ShortOfficeName & "_" & ShortDistrictName
                        If Not Result.Name.StartsWith("..") Then bgwListFiles.ReportProgress(2, item)
                    Case ShortOfficeName & "_" & ShortDistrictName
                        If Not Result.Name.StartsWith(".") Then 'if not hidden item
                            If CurrentFolderPath.Contains(FileOwner) Then 'if path name contains fileowner
                                bgwListFiles.ReportProgress(2, item)
                            ElseIf item.SubItems(4).Text = FileOwner Then 'if file owner
                                bgwListFiles.ReportProgress(2, item)
                            ElseIf ResultIsFolder Then 'if folder
                                bgwListFiles.ReportProgress(2, item)
                            ElseIf Not CurrentFolderPath.StartsWith("\My Drive\FIS Backup\") Then 'if not folder
                                bgwListFiles.ReportProgress(2, item)
                            End If
                        End If
                End Select

            Next

            ' bgwListFiles.ReportProgress(3, FolderID)
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
            Me.lblItemCount.Text = "Item Count: "
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
        Me.lblItemCount.Text = "Item Count: "
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

        If CurrentFolderPath = "\My Drive" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new Folder is not allowed in 'My Drive' folder.  Use 'General Files' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\FIS Backup" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new Folder is not allowed in 'FIS Backup' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\Installer File" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new Folder is not allowed in 'Installer File' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        If CurrentFolderPath = "\My Drive" And Not SuperAdmin Then
            MessageBoxEx.Show("Uploading of files is not allowed in 'My Drive' folder. Use 'General Files' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\FIS Backup" And Not SuperAdmin Then
            MessageBoxEx.Show("Uploading of files is not allowed in 'FIS Backup' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\Installer File" And Not SuperAdmin Then
            MessageBoxEx.Show("Uploading of files is not allowed in 'Installer File' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
        If uUploadStatus = UploadStatus.Completed Then
            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
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

        If Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "" Or Me.listViewEx1.SelectedItems(0).SubItems(2).Text = "0B" Then
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
        If fname.StartsWith("FingerPrintBackup-") And CurrentFolderName <> (ShortOfficeName & "_" & ShortDistrictName) Then
            Dim f As String = strAppUserPath & "\FIS Backup\" & CurrentFolderName
            If My.Computer.FileSystem.DirectoryExists(f) = False Then
                My.Computer.FileSystem.CreateDirectory(f)
            End If
            SaveFileName = f & "\" & fname
        ElseIf fname.StartsWith("FingerPrintBackup-") And CurrentFolderName = (ShortOfficeName & "_" & ShortDistrictName) Then
            Dim f As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"

            If My.Computer.FileSystem.DirectoryExists(f) = False Then
                My.Computer.FileSystem.CreateDirectory(f)
            End If
            SaveFileName = f & "\" & fname
        Else
            SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & fname
        End If
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
        Dim SelectedItemOwner As String = Me.listViewEx1.SelectedItems(0).SubItems(4).Text
        SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index

        Dim blSelectedItemIsFolder As Boolean = False
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then blSelectedItemIsFolder = True

        If SelectedItemText.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        If Not SuperAdmin Then
            Dim msg1 As String = "file."
            If blSelectedItemIsFolder Then msg1 = "folder."

            If SelectedItemOwner = "Admin" Or (LocalUser And SelectedItemOwner <> FileOwner And CurrentFolderName <> FileOwner) Then
                MessageBoxEx.Show("You are not authorized to delete the selected " & msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
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

            RemoveFile(listViewEx1.Items(SelectedFileIndex).SubItems(3).Text, False)
            Me.listViewEx1.Items(SelectedFileIndex).Remove()
            lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
            GetDriveUsageDetails()

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
                FISService.Files.Update(tFile, FileID).Execute() 'move the file to trash
            Else
                FISService.Files.Delete(FileID).Execute()
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


#Region "FILE SIZE"

    Private Sub GetDriveUsageDetails()
        Try
            Dim request = FISService.About.Get
            request.Fields = "storageQuota"
            Dim abt = request.Execute
            Me.lblDriveSpaceUsed.Text = "Drive Space used: " & CalculateFileSize(abt.StorageQuota.UsageInDrive) & "/" & CalculateFileSize(abt.StorageQuota.Limit)
        Catch ex As Exception
            Me.lblDriveSpaceUsed.Text = "Drive Space used:"
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
        Dim SelectedItemOwner As String = Me.listViewEx1.SelectedItems(0).SubItems(4).Text

        Dim blSelectedItemIsFolder As Boolean = False
        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then blSelectedItemIsFolder = True

        If SelectedItemText.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim msg1 As String = "file"
        If blSelectedItemIsFolder Then msg1 = "folder"

        If Not SuperAdmin Then
            If SelectedItemOwner = "Admin" Or (LocalUser And SelectedItemOwner <> FileOwner And CurrentFolderName <> FileOwner) Then
                MessageBoxEx.Show("You are not authorized to rename the selected " & msg1 & ".", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If


        If blSelectedItemIsFolder And SuperAdmin And (SelectedItemText = "LocalAdminPass" Or SelectedItemText = "SuperAdminPass") Then
            SetAdminPassword(SelectedItemText)
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
            request.Description = FileOwner
            SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
            Dim id As String = listViewEx1.SelectedItems(0).SubItems(3).Text
            FISService.Files.Update(request, id).Execute()

            Dim List As FilesResource.GetRequest = FISService.Files.Get(id)
            List.Fields = "id, name, modifiedTime, description"
            Dim Result = List.Execute

            Me.listViewEx1.Items(SelectedFileIndex).Text = Result.Name
            Dim modifiedtime As DateTime = Result.ModifiedTime
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(1).Text = modifiedtime.ToString("dd-MM-yyyy HH:mm:ss")
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(4).Text = Result.Description

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


#Region "UPDATE FILE CONTENT"

    Private Sub btnUpdateFileContent_Click(sender As Object, e As EventArgs) Handles btnUpdateFileContent.Click

        If Not SuperAdmin Then
            MessageBoxEx.Show("You are not authorized to update file content.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

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

        Dim SelectedFileName = Me.listViewEx1.SelectedItems(0).Text
        If SelectedFileName.StartsWith("\") Then
            MessageBoxEx.Show("No files selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder And Not SelectedFileName = "SuperAdminPass" And Not SelectedFileName = "LocalAdminPass" Then
            MessageBoxEx.Show("Cannot update folder. Select file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If SelectedFileName = "SuperAdminPass" Or SelectedFileName = "LocalAdminPass" Then
            SetAdminPassword(SelectedFileName)
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Exe File|*.exe"
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

        SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
        Dim sFileName As String = My.Computer.FileSystem.GetFileInfo(uSelectedFile).Name
        For i = 0 To Me.listViewEx1.Items.Count - 1
            If i <> SelectedFileIndex And Me.listViewEx1.Items(i).Text.ToLower = sFileName.ToLower Then
                MessageBoxEx.Show("Another file with name '" & sFileName & "' already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        ShowProgressControls("0", "Updating File...", eCircularProgressType.Line)

        SelectedFileID = Me.listViewEx1.Items(SelectedFileIndex).SubItems(3).Text
        bgwUpdateFileContent.RunWorkerAsync(uSelectedFile)

    End Sub

    Private Sub bgwUpdateFileContent_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpdateFileContent.DoWork

        Try
            blUploadIsProgressing = True

            Dim request As New Google.Apis.Drive.v3.Data.File   'FISService.Files.Get(InstallerFileID).Execute
            request.Name = My.Computer.FileSystem.GetFileInfo(e.Argument).Name
            request.MimeType = "files/" & My.Computer.FileSystem.GetFileInfo(e.Argument).Extension.Replace(".", "")
            request.Description = FileOwner

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(e.Argument)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            Dim UpdateRequest As FilesResource.UpdateMediaUpload = FISService.Files.Update(request, SelectedFileID, Stream, request.MimeType)
            UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize

            AddHandler UpdateRequest.ProgressChanged, AddressOf Update_ProgressChanged

            UpdateRequest.Fields = "id, name, size, modifiedTime, mimeType, description"
            UpdateRequest.Upload()
            Stream.Close()

            If uUploadStatus = UploadStatus.Completed Then
                Dim file As Google.Apis.Drive.v3.Data.File = UpdateRequest.ResponseBody
                bgwUpdateFileContent.ReportProgress(100, file)
                GetDriveUsageDetails()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
            blUploadIsProgressing = False
        End Try

    End Sub

    Private Sub Update_ProgressChanged(Progress As IUploadProgress)
        Control.CheckForIllegalCrossThreadCalls = False
        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
        Dim percent = CInt((uBytesUploaded / dFileSize) * 100)
        bgwUpdateFileContent.ReportProgress(percent)
    End Sub
    Private Sub bgwUpdateFileContent_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpdateFileContent.ProgressChanged

        CircularProgress1.ProgressText = e.ProgressPercentage
        lblProgressStatus.Text = CalculateFileSize(uBytesUploaded) & "/" & dFormatedFileSize

        If TypeOf e.UserState Is Google.Apis.Drive.v3.Data.File Then
            Dim file As Google.Apis.Drive.v3.Data.File = e.UserState
            Me.listViewEx1.Items(SelectedFileIndex).Text = file.Name
            Dim modifiedtime As DateTime = file.ModifiedTime
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(1).Text = modifiedtime.ToString("dd-MM-yyyy HH:mm:ss")
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(2).Text = CalculateFileSize(file.Size)
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(4).Text = file.Description
            Me.listViewEx1.Items(SelectedFileIndex).ImageIndex = GetImageIndex(file.MimeType)
        End If

    End Sub

    Private Sub bgwUpdateFileContent_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpdateFileContent.RunWorkerCompleted

        HideProgressControls()
        blUploadIsProgressing = False

        If uUploadStatus = UploadStatus.Completed Then
            MessageBoxEx.Show("File updated successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("File update failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "ADMIN PRIVILEGE & PASSWORD"
    Private Sub SetAdminPassword(SelectedPassword As String)

        frmInputBox.SetTitleandMessage("Enter New " & SelectedPassword, "Enter New " & SelectedPassword, False)
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If frmInputBox.txtInputBox.Text = "" Then Exit Sub

        Dim NewPassword As String = frmInputBox.txtInputBox.Text

        If NewPassword = "" Then
            MessageBoxEx.Show("Invalid Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
        SelectedFileID = Me.listViewEx1.SelectedItems(0).SubItems(3).Text
        Me.Cursor = Cursors.WaitCursor
        Try

            Dim request As New Google.Apis.Drive.v3.Data.File

            request.Name = SelectedPassword
            request.Description = NewPassword
            FISService.Files.Update(request, SelectedFileID).Execute()
            Dim List As FilesResource.GetRequest = FISService.Files.Get(SelectedFileID)
            List.Fields = "id, name, modifiedTime, description"
            Dim Result = List.Execute

            Me.listViewEx1.Items(SelectedFileIndex).Text = Result.Name
            Dim modifiedtime As DateTime = Result.ModifiedTime
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(1).Text = modifiedtime.ToString("dd-MM-yyyy HH:mm:ss")
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(4).Text = Result.Description
            MessageBoxEx.Show(Result.Name & " updated.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnGetAdminPrivilege_Click(sender As Object, e As EventArgs) Handles btnGetAdminPrivilege.Click
        If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
            ShowFileTransferInProgressMessage()
            Exit Sub
        End If

        If SuperAdmin Or LocalAdmin Then
            MessageBoxEx.Show("You are in Admin mode.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            Dim adminprivilege As Boolean = SetAdminPrivilege()
            If adminprivilege = True Then '
                SetTitleAndSize()
                Me.listViewEx1.Items.Clear()
                Me.lblItemCount.Text = "Item Count: "
                ShowProgressControls("", "", eCircularProgressType.Donut)
                bgwListFiles.RunWorkerAsync(CurrentFolderID)
            End If
        Else
            MessageBoxEx.Show("Connection Failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Me.Cursor = Cursors.Default
    End Sub


#End Region

    Private Sub ChangeParentFolder(sender As Object, e As EventArgs) ' Handles ButtonX1.Click
        Dim List = FISService.Files.List()
        List.Q = "name contains 'VersionFolder' and trashed = false"
        List.Fields = "files(name, id)"

        Dim Results = List.Execute
        Dim InstallerFileID As String = ""
        If Results.Files.Count > 0 Then
            InstallerFileID = Results.Files(0).Id
        End If

        Dim newparentfolderid As String = Me.listViewEx1.SelectedItems(0).SubItems(3).Text

        If InstallerFileID = "" Then Exit Sub

        Dim request = FISService.Files.Get(InstallerFileID)
        request.Fields = "parents"
        Dim file = request.Execute
        Dim previousparent = file.Parents

        Dim updateRequest = FISService.Files.Update(New Google.Apis.Drive.v3.Data.File, InstallerFileID)
        updateRequest.Fields = "id, parents"
        updateRequest.AddParents = "root"
        updateRequest.RemoveParents = previousparent(0)
        file = updateRequest.Execute
    End Sub

    Public Sub SetTitleAndSize()
        Dim Header As String = ""
        If SuperAdmin Then Header = "Super Admin"
        If LocalAdmin Then Header = "Local Admin"
        If LocalUser Then Header = FileOwner
        Me.Text = "FIS Online File List - " & Header
        Me.TitleText = "<b>FIS Online File List - " & Header & "</b>"

        If SuperAdmin Then
            Me.listViewEx1.Columns(3).Width = 100
        Else
            Me.listViewEx1.Columns(3).Width = 0
        End If

        '   Me.CircularProgress1.Location = New Point((Me.listViewEx1.Width - Me.CircularProgress1.Width) / 2, Me.CircularProgress1.Location.Y)
        '  Me.lblProgressStatus.Location = New Point((Me.listViewEx1.Width - Me.lblProgressStatus.Width) / 2, Me.lblProgressStatus.Location.Y)
        Me.CenterToScreen()
        Me.BringToFront()
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

    
End Class

