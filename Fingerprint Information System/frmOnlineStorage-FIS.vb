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
    Dim blViewFile = False
    Dim TotalFileCount As Integer = 0
    Dim blRedownload As Boolean = False
    Dim UploadedFileCount As Integer = 0
    Dim SkippedFileCount As Integer = 0
    Dim DownloadedFileCount As Integer = 0
    Dim FailedFileCount As Integer = 0

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
        Try

            Me.Cursor = Cursors.WaitCursor

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
            CurrentFolderName = "My Drive"

            JsonPath = CredentialFilePath & "\FISServiceAccount.json"

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

            Me.listViewEx1.Items.Clear()
            Me.lblItemCount.Text = "Item Count: "
            ServiceCreated = False


            ShowProgressControls("", "Fetching Files from Google Drive...", eCircularProgressType.Donut)

            blUploadIsProgressing = False
            blDownloadIsProgressing = False
            blListIsLoading = False

            If blUnreadIFTFileAvailable Then
                CurrentFolderPath = "\My Drive\Internal File Transfer\" & FullDistrictName
                ParentFolderPath = "\My Drive\Internal File Transfer\"
                CurrentFolderName = FullDistrictName
                bgwListFiles.RunWorkerAsync(UserIFTFolderID)
            Else
                bgwListFiles.RunWorkerAsync("root")
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub CreateServiceAndLoadData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListFiles.DoWork
        blListIsLoading = True
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

            If TypeOf e.UserState Is Boolean Then
                frmMainInterface.cprUnreadFile.Visible = e.UserState
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
        blUnreadIFTFileAvailable = False
    End Sub

    Private Sub ListFiles(ByVal FolderID As String, ShowTrashedFiles As Boolean)
        Try

            If CurrentFolderPath.ToLower.Contains("\my drive\internal file transfer\" & FullDistrictName.ToLower) Then
                Dim tFile = New Google.Apis.Drive.v3.Data.File
                tFile.ViewedByMeTime = Now
                FISService.Files.Update(tFile, FolderID).Execute()
                bgwListFiles.ReportProgress(1, False) 'hide unread file icon in main form
            End If

            Dim List As FilesResource.ListRequest = FISService.Files.List()

            If ShowTrashedFiles Then
                List.Q = "trashed = true"
            Else
                List.Q = "trashed = false and '" & FolderID & "' in parents" ' list all files in parent folder. 
            End If


            List.PageSize = 1000 ' maximum file list
            List.Fields = "files(id, name, mimeType, size, modifiedTime, description, createdTime, viewedByMeTime)"
            List.OrderBy = "folder, name" 'sorting order

            Dim Results As FileList = List.Execute


            Dim item As ListViewItem

            If FolderID = "root" Then
                item = New ListViewItem("\My Drive")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("root")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.ImageIndex = ImageIndex.GoogleDrive 'google drive icon
                bgwListFiles.ReportProgress(1, item)
            Else
                item = New ListViewItem("\" & CurrentFolderName)
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add(FolderID)
                item.SubItems.Add("")
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

                If Result.MimeType = "application/vnd.google-apps.folder" Then ' it is a folder
                    Dim createdtime As Date = Result.CreatedTime
                    item.SubItems.Add(createdtime.ToString("dd-MM-yyyy HH:mm:ss"))

                    item.SubItems.Add("") 'size for folder
                    item.ImageIndex = ImageIndex.Folder
                    ResultIsFolder = True
                    Dim modifiedtime As Date = Result.ModifiedTime

                    If blUnreadIFTFileAvailable Or CurrentFolderPath.ToLower.Contains("\my drive\internal file transfer\" & FullDistrictName.ToLower) Then
                        If modifiedtime > dtIFTFolderViewTime And Result.Name <> "Work Done Statement" And Result.Name <> "Monthly Statements Backup" Then
                            item.ForeColor = Color.Red
                        End If
                    End If
                Else
                    Dim modifiedtime As Date = Result.ModifiedTime
                    item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                    item.ImageIndex = GetImageIndex(My.Computer.FileSystem.GetFileInfo(Result.Name).Extension)
                    ResultIsFolder = False
                    If blUnreadIFTFileAvailable Or CurrentFolderPath.ToLower.Contains("\my drive\internal file transfer\" & FullDistrictName.ToLower) Then
                        If modifiedtime > dtIFTFolderViewTime And CurrentFolderPath.ToLower <> "\my drive\internal file transfer\" & FullDistrictName.ToLower & "\work done statement" And CurrentFolderPath.ToLower <> "\my drive\internal file transfer\" & FullDistrictName.ToLower & "\monthly statements backup" Then
                            item.ForeColor = Color.Red
                        End If
                    End If
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
                If Not SuperAdmin And Result.Name.Contains(strAppName) And Result.Name.ToLower.Contains(".exe") Then
                    Description = "Admin"
                End If

                If SuperAdmin Then
                    item.SubItems.Add(Description)
                    If Not Result.ViewedByMeTime Is Nothing Then
                        Dim viewedtime As Date = Result.ViewedByMeTime
                        item.SubItems.Add(viewedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If
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
                        If Not Result.Name.StartsWith(".") And Result.Name <> "FIS Backup" Then 'if not hidden item
                            If CurrentFolderPath.Contains(FileOwner) Or CurrentFolderPath.ToLower.Contains(FullDistrictName.ToLower) Or CurrentFolderPath = "\My Drive\Internal File Transfer\*All Bureaux" Then
                                bgwListFiles.ReportProgress(2, item)
                            ElseIf item.SubItems(4).Text = FileOwner Then 'if file owner
                                bgwListFiles.ReportProgress(2, item)
                            ElseIf ResultIsFolder Then 'if folder
                                If CurrentFolderPath.ToLower.Contains("\my drive\internal file transfer\") And Not CurrentFolderPath.ToLower.Contains(FullDistrictName.ToLower) Then

                                Else
                                    bgwListFiles.ReportProgress(2, item)
                                End If

                            ElseIf Not CurrentFolderPath.StartsWith("\My Drive\FIS Backup\") And Not CurrentFolderPath.StartsWith("\My Drive\Internal File Transfer\") Then 'if not folder
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


    Private Sub ListViewEx1_DoubleClick(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick, btnViewFile.Click

        blViewFile = False
        blRedownload = False

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


        If Me.listViewEx1.SelectedItems(0).ImageIndex > 2 And (Me.listViewEx1.SelectedItems(0).SubItems(2).Text <> "" And Me.listViewEx1.SelectedItems(0).SubItems(2).Text <> "0B") Then

            Dim fname As String = Me.listViewEx1.SelectedItems(0).SubItems(0).Text

            If fname.StartsWith("FingerPrintBackup-") And CurrentFolderName <> (ShortOfficeName & "_" & ShortDistrictName) Then
                Dim f As String = strAppUserPath & "\FIS Backup\" & CurrentFolderName
                SaveFileName = f & "\" & fname
            ElseIf fname.StartsWith("FingerPrintBackup-") And CurrentFolderName = (ShortOfficeName & "_" & ShortDistrictName) Then
                Dim f As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"
                SaveFileName = f & "\" & fname
            Else
                SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & fname
            End If

            If My.Computer.FileSystem.FileExists(SaveFileName) Then
                Dim r = MessageBoxEx.Show("Selected file already exists in 'My Documents' folder. Do you want to download it again?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

                If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If r = Windows.Forms.DialogResult.No Then
                    Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                    Exit Sub
                End If
                If r = Windows.Forms.DialogResult.Yes Then
                    blRedownload = True
                    blViewFile = True
                    DownloadSelectedFile()
                    Exit Sub
                End If
            Else
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

        Try
            Dim id As String = ""
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                Dim List As FilesResource.GetRequest = FISService.Files.Get(CurrentFolderID)
                List.Fields = "parents"
                Dim Result = List.Execute
                If Result.Parents Is Nothing Then
                    id = "root"
                    CurrentFolderName = "My Drive"
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


    Private Sub RefreshFileList() Handles btnRefresh.Click, btnRefreshCM.Click
        '  blUnreadIFTFileAvailable = False
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
        CurrentFolderName = "My Drive"
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

        Dim mime = mimeType.ToLower.Replace("files/", ".")
        Dim index As Integer
        Select Case mime
            Case "application/x-msdownload"
                index = ImageIndex.Exe 'exe
            Case ".exe"
                index = ImageIndex.Exe 'exe
            Case ".msi"
                index = ImageIndex.Exe 'exe
            Case "database/mdb"
                index = ImageIndex.MSAccess 'mdb
            Case ".mdb"
                index = ImageIndex.MSAccess 'mdb
            Case ".accdb"
                index = ImageIndex.MSAccess 'mdb
            Case ".accde"
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

        If CurrentFolderPath = "\My Drive" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new folder is not allowed in 'My Drive' folder.  Use 'General Files' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\FIS Backup" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new folder is not allowed in 'FIS Backup' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\Installer File" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new folder is not allowed in 'Installer File' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderPath = "\My Drive\Internal File Transfer" And Not SuperAdmin Then
            MessageBoxEx.Show("Creation of new folder is not allowed in 'Internal File Transfer' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        '  If IsValidFileName(FolderName) = False Then
        'MessageBoxEx.Show("Folder name contains invalid characters.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        ' Exit Sub
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
            NewDirectory.Description = FileOwner

            NewDirectory = FISService.Files.Create(NewDirectory).Execute

            Dim List As FilesResource.GetRequest = FISService.Files.Get(NewDirectory.Id)
            List.Fields = "id, modifiedTime, description"
            Dim Result = List.Execute

            Dim item As ListViewItem

            item = New ListViewItem(NewDirectory.Name)
            Dim modifiedtime As Date = Result.ModifiedTime
            item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
            item.SubItems.Add("")
            item.SubItems.Add(Result.Id)
            item.SubItems.Add(Result.Description)

            item.ImageIndex = ImageIndex.Folder
            Me.listViewEx1.Items.Add(item)

            If listViewEx1.Items.Count > 0 Then
                Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
                Me.listViewEx1.SelectedItems(0).EnsureVisible()
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

        If CurrentFolderPath = "\My Drive\Internal File Transfer" And Not SuperAdmin Then
            MessageBoxEx.Show("Uploading of files is not allowed in 'Internal File Transfer' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        OpenFileDialog1.Filter = "All Files|*.*"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select Files to Upload"
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
            UploadedFileCount = 0
            SkippedFileCount = 0
            FailedFileCount = 0

            For i = 0 To TotalFileCount - 1

                If bgwUploadFile.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                Dim SelectedFile = FileList(i)
                Dim SelectedFileName As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Name

                If FileAlreadyExists(SelectedFileName) Then
                    SkippedFileCount += 1
                    bgwUploadFile.ReportProgress(i + 1, "Skipping existing file.")
                    Continue For
                End If

                dFileSize = My.Computer.FileSystem.GetFileInfo(SelectedFile).Length
                dFormatedFileSize = CalculateFileSize(dFileSize)

                Dim body As New Google.Apis.Drive.v3.Data.File()
                body.Name = SelectedFileName
                Dim extension As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Extension
                body.MimeType = "files/" & extension.Replace(".", "")
                body.Description = FileOwner

                Dim parentlist As New List(Of String)
                parentlist.Add(CurrentFolderID)
                body.Parents = parentlist

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(SelectedFile)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                uBytesUploaded = 0
                bgwUploadFile.ReportProgress(i + 1, SelectedFileName)
                bgwUploadFile.ReportProgress(i + 1, uBytesUploaded)

                Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize

                AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                UploadRequest.Fields = "id, name, mimeType, size, modifiedTime, description"
                UploadRequest.Upload()

                If uUploadStatus = UploadStatus.Completed Then
                    Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody
                    Dim item As ListViewItem = New ListViewItem(file.Name)
                    Dim modifiedtime As Date = file.ModifiedTime
                    item.SubItems.Add(modifiedtime.ToString("dd-MM-yyyy HH:mm:ss"))
                    item.SubItems.Add(CalculateFileSize(file.Size))
                    item.SubItems.Add(file.Id)
                    item.SubItems.Add(file.Description)
                    item.ImageIndex = GetImageIndex(extension)
                    UploadedFileCount += 1
                    bgwUploadFile.ReportProgress(100, item)
                End If

                If uUploadStatus = UploadStatus.Failed Then
                    FailedFileCount += 1
                End If

                Stream.Close()
            Next

            GetDriveUsageDetails()

            If CurrentFolderPath = "\My Drive\Internal File Transfer\" & FullDistrictName Then
                Dim tFile = New Google.Apis.Drive.v3.Data.File
                tFile.ViewedByMeTime = Now
                FISService.Files.Update(tFile, CurrentFolderID).Execute()
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

        Me.Cursor = Cursors.Default
        HideProgressControls()
        blUploadIsProgressing = False
        lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1

        If listViewEx1.Items.Count > 0 Then
            Me.listViewEx1.SelectedItems.Clear()
            Me.listViewEx1.Items(listViewEx1.Items.Count - 1).Selected = True
        End If

        If TotalFileCount = 1 And UploadedFileCount = 1 Then
            ShowDesktopAlert("File uploaded successfully.")
        End If

        If TotalFileCount = 1 And SkippedFileCount = 1 Then
            ShowDesktopAlert("File skipped as it already exists.")
        End If

        If TotalFileCount = 1 And FailedFileCount = 1 Then
            ShowDesktopAlert("File upload failed.")
        End If

        If TotalFileCount > 1 Then
            ShowDesktopAlert(UploadedFileCount & IIf(UploadedFileCount = 1, " file ", " files ") & "uploaded." & vbNewLine & SkippedFileCount & IIf(SkippedFileCount = 1, " file ", " files ") & "skipped." & vbNewLine & FailedFileCount & IIf(FailedFileCount = 1, " file ", " files ") & "failed.")
        End If

    End Sub


    Private Sub listViewEx1_DragDrop(sender As Object, e As DragEventArgs) Handles listViewEx1.DragDrop

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

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

            If CurrentFolderPath = "\My Drive\Internal File Transfer" And Not SuperAdmin Then
                MessageBoxEx.Show("Uploading of files is not allowed in 'Internal File Transfer' folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub btnDownloadFile_Click(sender As Object, e As EventArgs) Handles btnDownloadFile.Click, btnDownloadCM.Click
        blViewFile = False
        blRedownload = False

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

        If Me.listViewEx1.SelectedItems.Count = 1 Then

            Dim fname As String = Me.listViewEx1.SelectedItems(0).SubItems(0).Text

            If fname.StartsWith("FingerPrintBackup-") And CurrentFolderName <> (ShortOfficeName & "_" & ShortDistrictName) Then
                Dim f As String = strAppUserPath & "\FIS Backup\" & CurrentFolderName
                SaveFileName = f & "\" & fname
            ElseIf fname.StartsWith("FingerPrintBackup-") And CurrentFolderName = (ShortOfficeName & "_" & ShortDistrictName) Then
                Dim f As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"
                SaveFileName = f & "\" & fname
            Else
                SaveFileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & fname
            End If

            If My.Computer.FileSystem.FileExists(SaveFileName) Then
                Dim r = MessageBoxEx.Show("Selected file already exists in 'My Documents' folder. Do you want to download it again?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)
                If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If r = Windows.Forms.DialogResult.No Then
                    Call Shell("explorer.exe /select," & SaveFileName, AppWinStyle.NormalFocus)
                    Exit Sub
                End If
                If r = Windows.Forms.DialogResult.Yes Then
                    blRedownload = True
                End If
            End If
        End If

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        DownloadSelectedFile()
    End Sub
    Private Sub DownloadSelectedFile()

        ShowProgressControls("0", "Downloading File...", eCircularProgressType.Line)

        bgwDownloadFile.RunWorkerAsync(Me.listViewEx1.SelectedItems)
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadFile.DoWork


        Try
            blDownloadIsProgressing = True
            Dim SelectedItems As ListView.SelectedListViewItemCollection
            SelectedItems = e.Argument
            TotalFileCount = SelectedItems.Count
            DownloadedFileCount = 0
            FailedFileCount = 0
            SkippedFileCount = 0

            For i = 0 To TotalFileCount - 1

                If bgwDownloadFile.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                Dim fname As String = SelectedItems(i).SubItems(0).Text
                Dim fid As String = SelectedItems(i).SubItems(3).Text

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

                If My.Computer.FileSystem.FileExists(SaveFileName) Then
                    If blRedownload = False Then
                        SkippedFileCount += 1
                        bgwDownloadFile.ReportProgress(i + 1, "Skipping existing file.")
                        Continue For
                    Else
                        bgwDownloadFile.ReportProgress(i + 1, "Re-downloading file.")
                    End If
                End If

                Dim request = FISService.Files.Get(fid)
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
                    DownloadedFileCount += 1
                    mStream.WriteTo(fStream)
                End If

                If dDownloadStatus = DownloadStatus.Failed Then
                    FailedFileCount += 1
                End If

                fStream.Close()
                mStream.Close()
            Next


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
        Me.Cursor = Cursors.Default

        If TotalFileCount = 1 And DownloadedFileCount = 1 Then
            ShowDesktopAlert("File downloaded successfully.")
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

        If TotalFileCount = 1 And SkippedFileCount = 1 Then
            ShowDesktopAlert("File skipped as it already exists.")
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

        If TotalFileCount = 1 And FailedFileCount = 1 Then
            ShowDesktopAlert("File download failed.")
        End If

        If TotalFileCount > 1 Then
            ShowDesktopAlert(DownloadedFileCount & IIf(DownloadedFileCount = 1, " file ", " files ") & "downloaded." & vbNewLine & SkippedFileCount & IIf(SkippedFileCount = 1, " file ", " files ") & "skipped." & vbNewLine & FailedFileCount & IIf(FailedFileCount = 1, " file ", " files ") & "failed.")
            Call Shell("explorer.exe /select," & SaveFileName, AppWinStyle.NormalFocus)
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

        If Me.listViewEx1.Items.Count = 0 Then
            MessageBoxEx.Show("No files in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If selectedcount = 0 Then
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

            If SelectedItemOwner = "Admin" Or (LocalUser And SelectedItemOwner <> FileOwner And CurrentFolderName <> FullDistrictName) Then
                MessageBoxEx.Show("You are not authorized to delete the selected " & msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
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

        If selectedcount > 1 Then
            ShowPleaseWaitForm()
        End If
        Try
            For i = 0 To selectedcount - 1
                SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
                RemoveFile(listViewEx1.SelectedItems(0).SubItems(3).Text, False)
                Me.listViewEx1.SelectedItems(0).Remove()
            Next

            ClosePleaseWaitForm()
            lblItemCount.Text = "Item Count: " & Me.listViewEx1.Items.Count - 1
            GetDriveUsageDetails()

            Me.Cursor = Cursors.Default
            SelectNextItem(SelectedFileIndex)

            If blSelectedItemIsFolder Then
                msg = "Selected folder deleted from Google Drive."
            Else
                msg = "Selected file(s) deleted from Google Drive."
            End If
            ShowDesktopAlert(msg)

        Catch ex As Exception
            ClosePleaseWaitForm()
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
        If SelectedFileIndex < listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then 'selected 5 < count 10 
            Me.listViewEx1.Items(SelectedFileIndex).Selected = True 'select 5
        End If

        If SelectedFileIndex = listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then 'selected 5 = count 5 
            Me.listViewEx1.Items(SelectedFileIndex - 1).Selected = True 'select 5
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
    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click, btnRenameCM.Click

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

        If Me.listViewEx1.SelectedItems.Count > 1 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Dim ftype As String = "file"
        If blSelectedItemIsFolder Then ftype = "folder"

        If Not SuperAdmin Then
            If SelectedItemOwner = "Admin" Or (LocalUser And SelectedItemOwner <> FileOwner And CurrentFolderName <> FileOwner) Then
                MessageBoxEx.Show("You are not authorized to rename the selected " & ftype & ".", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If


        If CurrentFolderName = AdminPasswordFolderName And SuperAdmin And (SelectedItemText = "LocalAdminPass" Or SelectedItemText = "SuperAdminPass") Then
            SetAdminPassword(SelectedItemText)
            Exit Sub
        End If

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
            request.Description = FileOwner
            SelectedFileIndex = Me.listViewEx1.SelectedItems(0).Index
            Dim id As String = listViewEx1.SelectedItems(0).SubItems(3).Text
            FISService.Files.Update(request, id).Execute()

            Dim List As FilesResource.GetRequest = FISService.Files.Get(id)
            List.Fields = "id, name, modifiedTime, description"
            Dim Result = List.Execute

            Me.listViewEx1.Items(SelectedFileIndex).Text = Result.Name
            Dim modifiedtime As Date = Result.ModifiedTime
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(1).Text = modifiedtime.ToString("dd-MM-yyyy HH:mm:ss")
            Me.listViewEx1.Items(SelectedFileIndex).SubItems(4).Text = Result.Description
            ShowDesktopAlert("Selected " & ftype & " renamed successfully.")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


#Region "UPDATE FILE CONTENT"

    Private Sub btnUpdateFileContent_Click(sender As Object, e As EventArgs) Handles btnUpdateFileContent.Click, btnUpdateCM.Click

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

        If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then
            MessageBoxEx.Show("Cannot update folder. Select file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentFolderName = AdminPasswordFolderName And (SelectedFileName = "SuperAdminPass" Or SelectedFileName = "LocalAdminPass") Then
            SetAdminPassword(SelectedFileName)
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
            Dim fname As String = My.Computer.FileSystem.GetFileInfo(e.Argument).Name
            request.Name = fname
            request.MimeType = "files/" & My.Computer.FileSystem.GetFileInfo(e.Argument).Extension.Replace(".", "")
            request.Description = FileOwner
            If fname.ToLower.StartsWith(strAppName.ToLower) And fname.ToLower.EndsWith(".exe") Then
                Dim md5 As String = GetMD5(e.Argument)
                request.Description = FileOwner & ", " & md5
            End If

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(e.Argument)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            Dim UpdateRequest As FilesResource.UpdateMediaUpload = FISService.Files.Update(request, SelectedFileID, Stream, request.MimeType)
            UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize

            AddHandler UpdateRequest.ProgressChanged, AddressOf Update_ProgressChanged

            UpdateRequest.Fields = "id, name, size, modifiedTime, mimeType, description"
            UpdateRequest.Upload()

            If uUploadStatus = UploadStatus.Completed Then
                Dim file As Google.Apis.Drive.v3.Data.File = UpdateRequest.ResponseBody
                bgwUpdateFileContent.ReportProgress(100, file)
                GetDriveUsageDetails()
            End If

            Stream.Close()
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
            Dim modifiedtime As Date = file.ModifiedTime
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

        If uUploadStatus = UploadStatus.Failed Then
            MessageBoxEx.Show("File update failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Me.Cursor = Cursors.Default
    End Sub

#End Region

#Region "SHARE FILES"
    Private Sub btnShareFile_Click(sender As Object, e As EventArgs) Handles btnShareCM.Click
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

        frmInputBox.SetTitleandMessage("Share File", "Enter email id of recepient", False, "fingerprintinformationsystem@gmail.com")
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
            Dim request = FISService.Permissions.Create(userPermission, fileid)
            request.Fields = "id"
            request.Execute()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
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
            request.Description = EncryptText(NewPassword)
            FISService.Files.Update(request, SelectedFileID).Execute()
            Dim List As FilesResource.GetRequest = FISService.Files.Get(SelectedFileID)
            List.Fields = "id, name, modifiedTime, description"
            Dim Result = List.Execute

            Me.listViewEx1.Items(SelectedFileIndex).Text = Result.Name
            Dim modifiedtime As Date = Result.ModifiedTime
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

        RecoverPassword = My.Computer.Registry.GetValue(strGeneralSettingsPath, "RecoverPassword", "0")
        If RecoverPassword = "1" Then
            SuperAdminPass = "^^^px7600d"
            Dim adminprivilege As Boolean = SetAdminPrivilege()
            If adminprivilege = True Then '
                SetTitleAndSize()
                Me.listViewEx1.Items.Clear()
                Me.lblItemCount.Text = "Item Count: "
                ShowProgressControls("", "", eCircularProgressType.Donut)
                bgwListFiles.RunWorkerAsync(CurrentFolderID)
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
            Dim adminprivilege As Boolean = SetAdminPrivilege()
            If adminprivilege = True Then '
                SetTitleAndSize()
                Me.listViewEx1.Items.Clear()
                Me.lblItemCount.Text = "Item Count: "
                ShowProgressControls("", "", eCircularProgressType.Donut)
                bgwListFiles.RunWorkerAsync(CurrentFolderID)
            End If
        Else
            MessageBoxEx.Show("Unable to get user authentication.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        If LocalUser Then Header = ShortOfficeName & ", " & FullDistrictName
        Me.Text = "FIS Online File List - " & Header
        Me.TitleText = "<b>FIS Online File List - " & Header & "</b>"

        If SuperAdmin Then
            Me.listViewEx1.Columns(3).Width = 80
            Me.listViewEx1.Columns(4).Width = 80
            Me.listViewEx1.Columns(5).Width = 140
        Else
            Me.listViewEx1.Columns(3).Width = 0
            Me.listViewEx1.Columns(4).Width = 150
            Me.listViewEx1.Columns(5).Width = 0
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
        Me.btnRemoveCM.Visible = False
        Me.btnRenameCM.Visible = False
        Me.btnUpdateCM.Visible = False
        Me.btnShareCM.Visible = False
        Me.btnViewFile.Visible = False

        If Me.listViewEx1.Items.Count = 0 Or Me.listViewEx1.SelectedItems.Count = 0 Then
            Me.btnRefreshCM.Visible = True
            Me.btnNewFolderCM.Visible = True
            Me.btnUploadCM.Visible = True
        End If

        If Me.listViewEx1.SelectedItems.Count = 1 Then
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                e.Cancel = True
            End If

            If Me.listViewEx1.SelectedItems(0).ImageIndex = ImageIndex.Folder Then
                Me.btnRemoveCM.Visible = True
                Me.btnRenameCM.Visible = True
                Me.btnShareCM.Visible = SuperAdmin
            Else
                Me.btnDownloadCM.Visible = True
                Me.btnViewFile.Visible = True
                Me.btnRemoveCM.Visible = True
                Me.btnRenameCM.Visible = True
                Me.btnUpdateCM.Visible = SuperAdmin
                Me.btnShareCM.Visible = SuperAdmin
            End If
        End If

        If Me.listViewEx1.SelectedItems.Count > 1 Then
            If Me.listViewEx1.SelectedItems(0).Text.StartsWith("\") Then
                e.Cancel = True
            End If
            Me.btnRemoveCM.Visible = True
        End If
    End Sub


    Private Sub frmFISBackupList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If blDownloadIsProgressing Then
            On Error Resume Next
            If MessageBoxEx.Show("File download is in progress. Do you want to close the window?.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If bgwDownloadFile.IsBusy Then
                    bgwDownloadFile.CancelAsync()
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
End Class

