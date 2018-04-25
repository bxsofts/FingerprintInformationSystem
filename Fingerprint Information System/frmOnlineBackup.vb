Imports System.Threading
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


    Private Sub CreateService() Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups") & "\Online Downloads"

        If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
            My.Computer.FileSystem.CreateDirectory(BackupPath)
        End If

        BackupFolder = "FIS_BACKUP_" & ShortOfficeName & "_" & ShortDistrictName
        BackupFolderID = ""
        Try


            Dim CredentialPath As String = strAppUserPath & "\GoogleDriveAuthentication"
            Dim JsonPath As String = CredentialPath & "\FIS.json"

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

            Dim fStream = New FileStream(JsonPath, FileMode.Open, FileAccess.Read)

            Dim Scopes As String() = {DriveService.Scope.Drive}

            FISUserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, "user", CancellationToken.None, New FileDataStore(CredentialPath, True)).Result

            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISUserCredential, .ApplicationName = strAppName})

            LoadOnlineBackupListWithMessage(False)
            LoadDownloadedBackupFiles()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub LoadDownloadedBackupFiles()
        Try
            Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchAllSubDirectories, "FingerPrintBackup*.mdb")

                If foundFile Is Nothing Then
                    Exit Sub
                End If

                Dim FileName = My.Computer.FileSystem.GetName(foundFile)
                Dim FullFilePath = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & FileName

                Dim Filedate As DateTime = DateTime.ParseExact(FileName.Replace("FingerPrintBackup-", "").Replace(".mdb", ""), BackupDateFormatString, culture)

                Dim item As ListViewItem = Me.listViewEx1.Items.Add(FileName)
                item.SubItems.Add(Filedate.ToString("dd/MM/yyyy HH:mm:ss"))
                item.SubItems.Add("Downloaded File")
                item.ImageIndex = 1

            Next
            DisplayInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LoadOnlineBackupListWithMessage(ByVal ShowMessage As Boolean)
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.listViewEx1.Items.Clear()
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
            Me.listViewEx1.Items.Clear()

            For Each Result In Results.Files
                Dim item As ListViewItem = Me.listViewEx1.Items.Add(Result.Name)
                item.SubItems.Add(Result.Description)
                item.SubItems.Add(Result.Id)
                item.ImageIndex = 0
            Next

            DisplayInformation()

            Me.listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
            Me.listViewEx1.Sort()

            If Me.listViewEx1.Items.Count = 0 And ShowMessage Then
                MessageBoxEx.Show("No online Backup Files were found.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default

        End Try

    End Sub

    Private Sub RefreshBackupList() Handles btnRefresh.Click
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If


        LoadOnlineBackupListWithMessage(True)
        LoadDownloadedBackupFiles()
    End Sub
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


    Private Sub UploadBackup() Handles btnBackupDatabase.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Application.DoEvents()
            Dim BackupTime As Date = Now
            Dim d As String = Strings.Format(BackupTime, BackupDateFormatString)
            Dim sBackupTime = Strings.Format(BackupTime, "dd/MM/yyyy HH:mm:ss")
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

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(tmpFileName)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
            UploadRequest.Upload()
            UploadRequest.Fields = "id"


            Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody

            If file Is Nothing Then
                MessageBoxEx.Show("Upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            Else
                MessageBoxEx.Show("Uploaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Stream.Close()

            Dim item As ListViewItem = Me.listViewEx1.Items.Add(BackupFileName)
            item.SubItems.Add(sBackupTime)
            item.SubItems.Add(file.Id)
            item.ImageIndex = 0
            DisplayInformation()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default

        End Try
    End Sub


    Private Sub DownloadFileFromDrive()
        Try
            Dim id As String = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            Dim SelectedFileName As String = Me.listViewEx1.SelectedItems(0).Text
            Dim DownloadFileName As String = BackupPath & "\" & SelectedFileName
            Dim BackupDate As String = Me.listViewEx1.SelectedItems(0).SubItems(1).Text

            Dim request = FISService.Files.Get(id)
            Dim FileStream = New System.IO.FileStream(DownloadFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)

            request.DownloadWithStatus(FileStream)

            FileStream.Close()

            If My.Computer.FileSystem.FileExists(DownloadFileName) Then
                Dim item As ListViewItem = Me.listViewEx1.Items.Add(SelectedFileName)
                item.SubItems.Add(BackupDate)
                item.SubItems.Add("Downloaded File")
                item.ImageIndex = 1
            End If
            DisplayInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


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

                    DownloadFileFromDrive()
                End If

                If My.Computer.FileSystem.FileExists(strBackupFile) Then
                    My.Computer.FileSystem.CopyFile(strBackupFile, strDatabaseFile, True)
                    Application.DoEvents()
                    boolRestored = True

                    Me.Close()
                Else
                    MessageBoxEx.Show("Cannot restore. Backup file is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                Me.Cursor = Cursors.Default
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            boolRestored = False
            Me.Cursor = Cursors.Default
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
            Dim DownloadFileName = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text

            DownloadFileFromDrive()

            If My.Computer.FileSystem.FileExists(DownloadFileName) Then
                MessageBoxEx.Show("Downloaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            DisplayInformation()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

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


            Dim DownloadFileName = BackupPath & "\" & Me.listViewEx1.SelectedItems(0).Text
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
                DownloadFileFromDrive()
            End If

            If My.Computer.FileSystem.FileExists(DownloadFileName) Then
                Shell("explorer.exe " & DownloadFileName, AppWinStyle.MaximizedFocus)
            Else
                MessageBoxEx.Show("Cannot open file. File is missing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            DisplayInformation()
            Me.Cursor = Cursors.Default

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
                MessageBoxEx.Show("Selected backup file deleted to the Recycle Bin.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                MessageBoxEx.Show("Selected backup file deleted from Google Drive.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Me.Cursor = Cursors.Default

            DisplayInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub SortByDate(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles listViewEx1.ColumnClick
        If Me.listViewEx1.Sorting = SortOrder.Ascending Then
            Me.listViewEx1.Sorting = SortOrder.Descending
        Else
            Me.listViewEx1.Sorting = SortOrder.Ascending
        End If

        Me.listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, Me.listViewEx1.Sorting)
        Me.listViewEx1.Sort()
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


End Class