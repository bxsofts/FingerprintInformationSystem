
Imports System.IO
Imports DevComponents.DotNetBar

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Public Class frmOnlineSendStatements

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential

    Public uBytesUploaded As Long
    Public uUploadStatus As UploadStatus
    Public dFileSize As Long
    Public dFormatedFileSize As String = ""

    Dim blServiceCreated As Boolean = False
    Dim blCheckBoxes As Boolean = False

    Dim SelectedDistrict As String = ""
    Dim SelectedDistrictID As String = ""
    Dim SelectedMonth As Integer
    Dim SelectedYear As String = ""
    Private Sub frmOnlineSendStatements_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Me.Cursor = Cursors.WaitCursor
            blCheckBoxes = False
            Me.CircularProgress1.Visible = False
            Me.CircularProgress1.ProgressColor = GetProgressColor()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False
            Control.CheckForIllegalCrossThreadCalls = False

            Me.cmbMonth.Items.Clear()

            For i = 0 To 11
                Me.cmbMonth.Items.Add(MonthName(i + 1))
            Next

            Dim m As Integer = DateAndTime.Month(Today)
            Dim y As Integer = DateAndTime.Year(Today)


            If m = 1 Then
                m = 12
                y = y - 1
            Else
                m = m - 1
            End If

            Me.cmbMonth.SelectedIndex = m - 1
            Me.txtYear.Value = y

            blCheckBoxes = True
            CheckForStatementFiles()

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
            blServiceCreated = False
            Me.ListViewEx1.Items.Clear()
            bgwListFiles.RunWorkerAsync("root")

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub CheckForStatementFiles() Handles cmbMonth.SelectedValueChanged, txtYear.ValueChanged
        On Error Resume Next
        If Not blCheckBoxes Then Exit Sub

        Dim StmtFolder As String = SuggestedLocation & "\SOC Statement\" & Me.txtYear.Text
        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1

        Dim StmtFileName As String = StmtFolder & "\SOC Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        chkSOC.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkSOC.Enabled = chkSOC.Checked

        StmtFolder = SuggestedLocation & "\Grave Crime Statement\" & Me.txtYear.Text
        StmtFileName = StmtFolder & "\Grave Crime Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        chkGrave.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkGrave.Enabled = chkGrave.Checked

        StmtFolder = SuggestedLocation & "\Identification Statement\" & Me.txtYear.Text
        StmtFileName = StmtFolder & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        chkID.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkID.Enabled = chkID.Checked

        StmtFolder = SuggestedLocation & "\Performance Statement"
        StmtFileName = StmtFolder & "\Monthly Performance Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        chkPerf.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkPerf.Enabled = chkPerf.Checked
    End Sub

    Private Sub LoadFolderList(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListFiles.DoWork
        Try
            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            Dim List As FilesResource.ListRequest = FISService.Files.List()
            Dim internalfolderid As String = ""

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Internal File Transfer' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                internalfolderid = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and '" & internalfolderid & "' in parents"

            List.Fields = "files(id, name)"
            List.OrderBy = "name"

            Results = List.Execute

            cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            End If

            Dim item As ListViewItem

            For Each Result In Results.Files
                If Not Result.Name.StartsWith("*") Then
                    item = New ListViewItem(Result.Name)
                    item.SubItems.Add(Result.Id)
                    bgwListFiles.ReportProgress(0, item)
                End If
            Next

        Catch ex As Exception
            blServiceCreated = False
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
    Private Sub bgwListFiles_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwListFiles.ProgressChanged
        Try
            If TypeOf e.UserState Is ListViewItem Then
                listViewEx1.Items.Add(e.UserState)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub bgwListFiles_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwListFiles.RunWorkerCompleted
        Me.Cursor = Cursors.Default
        Dim RangeDistrict As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "RangeDistrict", "")
        If RangeDistrict = "" Then Exit Sub

        For i = 0 To Me.ListViewEx1.Items.Count - 1
            If Me.ListViewEx1.Items(i).Text.ToLower = RangeDistrict.ToLower Then
                Me.ListViewEx1.Items(i).Selected = True
            End If
        Next

    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        If Me.ListViewEx1.Items.Count = 0 Then
            MessageBoxEx.Show("List of Districts is empty. Please close the form and try again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.ListViewEx1.SelectedItems.Count = 0 Then
            MessageBoxEx.Show("Please select a District.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not Me.chkSOC.Checked And Not Me.chkGrave.Checked And Not Me.chkID.Checked And Not Me.chkPerf.Checked Then
            MessageBoxEx.Show("No statements selected to upload.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim FileList(3) As String
        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1
        Dim n As Integer = 0

        Dim StmtFolder As String = SuggestedLocation & "\SOC Statement\" & Me.txtYear.Text
        Dim StmtFileName As String = StmtFolder & "\SOC Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkSOC.Checked Then
            FileList(0) = StmtFileName
            n = n + 1
        Else
            FileList(0) = ""
        End If

        If FileInUse(StmtFileName) Then
            MessageBoxEx.Show("SOC Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFolder = SuggestedLocation & "\Grave Crime Statement\" & Me.txtYear.Text
        StmtFileName = StmtFolder & "\Grave Crime Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkGrave.Checked Then
            FileList(1) = StmtFileName
            n = n + 1
        Else
            FileList(1) = ""
        End If

        If FileInUse(StmtFileName) Then
            MessageBoxEx.Show("Grave Crime Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFolder = SuggestedLocation & "\Identification Statement\" & Me.txtYear.Text
        StmtFileName = StmtFolder & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkID.Checked Then
            FileList(2) = StmtFileName
            n = n + 1
        Else
            FileList(2) = ""
        End If

        If FileInUse(StmtFileName) Then
            MessageBoxEx.Show("Identification Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFolder = SuggestedLocation & "\Performance Statement"
        StmtFileName = StmtFolder & "\Monthly Performance Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkPerf.Checked Then
            FileList(3) = StmtFileName
            n = n + 1
        Else
            FileList(3) = ""
        End If

        If FileInUse(StmtFileName) Then
            MessageBoxEx.Show("Monthly Performance Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        SelectedDistrict = Me.ListViewEx1.SelectedItems(0).Text
        SelectedDistrictID = Me.ListViewEx1.SelectedItems(0).SubItems(1).Text
        SelectedMonth = Me.cmbMonth.SelectedIndex + 1
        SelectedYear = Me.txtYear.Text

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "RangeDistrict", SelectedDistrict, Microsoft.Win32.RegistryValueKind.String)

        Me.CircularProgress1.ProgressText = "1 & n"
        Me.CircularProgress1.IsRunning = True
        Me.CircularProgress1.Show()
        FileOwner = ShortOfficeName & "_" & ShortDistrictName

        bgwUploadFile.RunWorkerAsync(FileList)
    End Sub

    Private Function CreateFolderAndGetID(FolderName As String, ParentFolderID As String)
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & FolderName & "' and '" & ParentFolderID & "' in parents"

            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                Dim parentlist As New List(Of String)
                parentlist.Add(ParentFolderID) 'parent forlder

                NewDirectory.Parents = parentlist
                NewDirectory.Name = FolderName
                NewDirectory.MimeType = "application/vnd.google-apps.folder"
                NewDirectory.Description = FileOwner
                NewDirectory = FISService.Files.Create(NewDirectory).Execute
                id = NewDirectory.Id
            Else
                id = Results.Files(0).Id
            End If

            Return id


        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUploadFile.DoWork
        Try
            Dim monthlyfolderid As String = CreateFolderAndGetID("Monthly Statements - " & FullDistrictName, SelectedDistrictID)

            Dim FileList() As String = e.Argument

            For i = 0 To 3

                Dim SelectedFile = FileList(i)
                If SelectedFile = "" Then
                    Continue For
                End If

                Dim SelectedFileName As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Name

                dFileSize = My.Computer.FileSystem.GetFileInfo(SelectedFile).Length
                dFormatedFileSize = CalculateFileSize(dFileSize)

                Dim body As New Google.Apis.Drive.v3.Data.File()
                body.Name = SelectedFileName
                Dim extension As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Extension
                body.MimeType = "files/" & extension.Replace(".", "")
                body.Description = FileOwner

                Dim parentlist As New List(Of String)
                parentlist.Add(monthlyfolderid)
                body.Parents = parentlist

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(SelectedFile)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                uBytesUploaded = 0
                bgwUploadFile.ReportProgress(i + 1, SelectedFileName)
                bgwUploadFile.ReportProgress(i + 1, uBytesUploaded)

                Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize

                AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                UploadRequest.Fields = "id"
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
End Class