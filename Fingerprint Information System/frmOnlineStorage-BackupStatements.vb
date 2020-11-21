
Imports System.IO
Imports DevComponents.DotNetBar

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Upload
Imports Google.Apis.Download
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Public Class frmBackupStatements

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential

    Dim InternalFolderID As String = ""

    Public uUploadStatus As UploadStatus
    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus

    Dim SelectedMonthIndex As Integer
    Dim SelectedMonthText As String = ""
    Dim SelectedMonthYear As String = ""

    Dim SelectedQuarter As String = ""
    Dim SelectedQuarterYear As String = ""

    Dim SelectedAnnualYear As String = ""


    Dim blServiceCreated As Boolean = False
    Dim blCheckBoxes As Boolean = False
    Dim TotalFileCount As Integer = 0

    Private Sub frmOnlineBackupStatements_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Me.Cursor = Cursors.WaitCursor
            Control.CheckForIllegalCrossThreadCalls = False

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
            blCheckBoxes = False
            Me.CircularProgress1.Visible = False
            Me.CircularProgress1.ProgressColor = GetProgressColor()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False

            ShowLabels(False)

            Me.cmbMonth.Items.Clear()

            For i = 0 To 11
                Me.cmbMonth.Items.Add(MonthName(i + 1))
            Next

            Dim m As Integer = DateAndTime.Month(Today)
            Dim y As Integer = DateAndTime.Year(Today)

            Me.txtAnnualYear.Value = y - 1

            If m = 1 Then
                m = 12
                y = y - 1
            Else
                m = m - 1
            End If

            Me.cmbMonth.SelectedIndex = m - 1
            Me.txtMonthlyYear.Value = y

            m = DateAndTime.Month(Today)
            y = DateAndTime.Year(Today)

            Me.txtQuarterYear.Value = y
            Dim q As Integer = 1
            If m <= 3 Then q = 1
            If m > 3 And m < 7 Then q = 2
            If m > 6 And m < 10 Then q = 3
            If m > 9 Then q = 4
            If q = 1 Then
                Me.txtQuarter.Value = 4
                Me.txtQuarterYear.Value = y - 1
            Else
                Me.txtQuarter.Value = q - 1
                Me.txtQuarterYear.Value = y
            End If

            blCheckBoxes = True
            CheckForMonthlyStatementFiles()
            CheckForQuarterlyStatementFiles()
            CheckForAnnualStatementFiles()

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ShowLabels(Show As Boolean)
        Me.lblSOC.Text = ""
        Me.lblSOC.Visible = Show
        Me.lblGrave.Text = ""
        Me.lblGrave.Visible = Show
        Me.lblID.Text = ""
        Me.lblID.Visible = Show
        Me.lblMonthPerf.Text = ""
        Me.lblMonthPerf.Visible = Show
        Me.lblQuarterlyPerf.Text = ""
        Me.lblQuarterlyPerf.Visible = Show
        Me.lblAnnualPerf.Text = ""
        Me.lblAnnualPerf.Visible = Show
    End Sub

    Private Sub CheckForMonthlyStatementFiles() Handles cmbMonth.SelectedValueChanged, txtMonthlyYear.ValueChanged
        On Error Resume Next
        If Not blCheckBoxes Then Exit Sub
        ShowLabels(False)
        Dim StmtFolder As String = SuggestedLocation & "\SOC Statement\" & Me.txtMonthlyYear.Text
        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1

        Dim StmtFileName As String = StmtFolder & "\SOC Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        chkSOC.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkSOC.Enabled = chkSOC.Checked

        StmtFolder = SuggestedLocation & "\Grave Crime Statement\" & Me.txtMonthlyYear.Text
        StmtFileName = StmtFolder & "\Grave Crime Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        chkGrave.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkGrave.Enabled = chkGrave.Checked

        StmtFolder = SuggestedLocation & "\Identification Statement\" & Me.txtMonthlyYear.Text
        StmtFileName = StmtFolder & "\Identification Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        chkID.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkID.Enabled = chkID.Checked

        StmtFolder = SuggestedLocation & "\Performance Statement"
        StmtFileName = StmtFolder & "\Monthly Performance Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        chkMonthlyPerf.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkMonthlyPerf.Enabled = chkMonthlyPerf.Checked

    End Sub

    Private Sub CheckForQuarterlyStatementFiles() Handles txtQuarter.ValueChanged, txtQuarterYear.ValueChanged
        On Error Resume Next
        If Not blCheckBoxes Then Exit Sub
        ShowLabels(False)
        Dim StmtFolder As String = SuggestedLocation & "\Performance Statement"
        Dim StmtFileName = StmtFolder & "\Quarterly Performance Statement - " & Me.txtQuarterYear.Text & " - Q" & Me.txtQuarter.Text & ".docx"

        chkQuarterlyPerf.Enabled = My.Computer.FileSystem.FileExists(StmtFileName)
        chkQuarterlyPerf.Checked = chkQuarterlyPerf.Enabled
      
    End Sub

    Private Sub CheckForAnnualStatementFiles() Handles txtAnnualYear.ValueChanged
        On Error Resume Next
        If Not blCheckBoxes Then Exit Sub
        ShowLabels(False)
        Dim StmtFolder As String = SuggestedLocation & "\Performance Statement"
        Dim StmtFileName = StmtFolder & "\Annual Performance Statement - " & Me.txtAnnualYear.Text & ".docx"
        chkAnnualPerf.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkAnnualPerf.Enabled = chkAnnualPerf.Checked

    End Sub


#Region "BACKUP FILES"
    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click

        If Not Me.chkSOC.Checked And Not Me.chkGrave.Checked And Not Me.chkID.Checked And Not Me.chkMonthlyPerf.Checked And Not Me.chkQuarterlyPerf.Checked And Not Me.chkAnnualPerf.Checked Then
            MessageBoxEx.Show("No statements selected to upload.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim FileList(5) As String
        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1

        TotalFileCount = 0
        Dim StmtFolder As String = SuggestedLocation & "\SOC Statement\" & Me.txtMonthlyYear.Text
        Dim StmtFileName As String = StmtFolder & "\SOC Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkSOC.Checked Then
            FileList(0) = StmtFileName
            TotalFileCount = TotalFileCount + 1
        Else
            FileList(0) = ""
        End If

        If FileInUse(StmtFileName) And chkSOC.Checked Then
            MessageBoxEx.Show("SOC Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFolder = SuggestedLocation & "\Grave Crime Statement\" & Me.txtMonthlyYear.Text
        StmtFileName = StmtFolder & "\Grave Crime Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkGrave.Checked Then
            FileList(1) = StmtFileName
            TotalFileCount = TotalFileCount + 1
        Else
            FileList(1) = ""
        End If

        If FileInUse(StmtFileName) And chkGrave.Checked Then
            MessageBoxEx.Show("Grave Crime Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFolder = SuggestedLocation & "\Identification Statement\" & Me.txtMonthlyYear.Text
        StmtFileName = StmtFolder & "\Identification Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkID.Checked Then
            FileList(2) = StmtFileName
            TotalFileCount = TotalFileCount + 1
        Else
            FileList(2) = ""
        End If

        If FileInUse(StmtFileName) And chkID.Checked Then
            MessageBoxEx.Show("Identification Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFolder = SuggestedLocation & "\Performance Statement"
        StmtFileName = StmtFolder & "\Monthly Performance Statement - " & Me.txtMonthlyYear.Text & " - " & m.ToString("D2") & ".docx"
        If chkMonthlyPerf.Checked Then
            FileList(3) = StmtFileName
            TotalFileCount = TotalFileCount + 1
        Else
            FileList(3) = ""
        End If

        If FileInUse(StmtFileName) And chkMonthlyPerf.Checked Then
            MessageBoxEx.Show("Monthly Performance Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFileName = StmtFolder & "\Quarterly Performance Statement - " & Me.txtQuarterYear.Text & " - Q" & Me.txtQuarter.Text & ".docx"
        If chkQuarterlyPerf.Checked Then
            FileList(4) = StmtFileName
            TotalFileCount = TotalFileCount + 1
        Else
            FileList(4) = ""
        End If

        If FileInUse(StmtFileName) And chkQuarterlyPerf.Checked Then
            MessageBoxEx.Show("Quarterly Performance Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        StmtFileName = StmtFolder & "\Annual Performance Statement - " & Me.txtAnnualYear.Text & ".docx"
        If chkAnnualPerf.Checked Then
            FileList(5) = StmtFileName
            TotalFileCount = TotalFileCount + 1
        Else
            FileList(5) = ""
        End If

        If FileInUse(StmtFileName) And chkAnnualPerf.Checked Then
            MessageBoxEx.Show("Annual Performance Statement File is open in MS Word. Please close it.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.CircularProgress1.ProgressText = "1/" & TotalFileCount
        Me.CircularProgress1.IsRunning = True
        Me.CircularProgress1.Show()
        ShowLabels(True)

        FileOwner = ShortOfficeName & "_" & ShortDistrictName

        bgwUploadFile.RunWorkerAsync(FileList)
    End Sub

    Private Function CreateFolderAndGetID(NewFolderName As String, ParentFolderID As String)
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & NewFolderName & "' and '" & ParentFolderID & "' in parents"

            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                Dim parentlist As New List(Of String)
                parentlist.Add(ParentFolderID) 'parent forlder

                NewDirectory.Parents = parentlist
                NewDirectory.Name = NewFolderName
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

            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            Dim internalfolderid As String = ""

            Dim List = FISService.Files.List()
            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Internal File Transfer' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                internalfolderid = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & FullDistrictName & "' and '" & internalfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute

            Dim DistrictFolderID As String = ""

            cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                DistrictFolderID = Results.Files(0).Id
            End If


            Dim workdonefolderid As String = CreateFolderAndGetID("Work Done Statement", DistrictFolderID)
            Dim monthlyfolderid As String = CreateFolderAndGetID("Monthly Statements Backup", DistrictFolderID)

            If workdonefolderid = "" Or monthlyfolderid = "" Then Exit Sub


            Dim FileList() As String = e.Argument
            Dim currentfilenumber As Integer = 0
            For i = 0 To 5

                Dim SelectedFile = FileList(i)
                If SelectedFile = "" Then
                    Continue For
                End If
                currentfilenumber = currentfilenumber + 1
                bgwUploadFile.ReportProgress(currentfilenumber, currentfilenumber)

                Dim SelectedFileName As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Name

                Dim stmt As String = SelectedFileName.ToLower.Substring(0, 3)
                Dim FolderID As String = ""

                If stmt = "mon" Or stmt = "qua" Or stmt = "ann" Then
                    FolderID = workdonefolderid
                Else
                    FolderID = monthlyfolderid
                End If



                List.Q = "name = '" & SelectedFileName & "' and '" & FolderID & "' in parents"
                List.Fields = "files(id)"

                Results = List.Execute

                cnt = Results.Files.Count

                If cnt > 0 Then
                    bgwUploadFile.ReportProgress(100, stmt & " Already Uploaded")
                Else
                    If My.Computer.FileSystem.FileExists(SelectedFile) Then
                        bgwUploadFile.ReportProgress(100, stmt & " Uploading")
                        Dim body As New Google.Apis.Drive.v3.Data.File()
                        body.Name = SelectedFileName
                        Dim extension As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Extension
                        body.MimeType = "files/" & extension.Replace(".", "")
                        body.Description = FileOwner

                        Dim parentlist As New List(Of String)
                        parentlist.Add(FolderID)
                        body.Parents = parentlist

                        Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(SelectedFile)
                        Dim Stream As New System.IO.MemoryStream(ByteArray)

                        Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                        AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                        UploadRequest.Upload()
                        Stream.Close()

                        If uUploadStatus = UploadStatus.Completed Then
                            bgwUploadFile.ReportProgress(100, stmt & " Uploaded")
                        End If
                        If uUploadStatus = UploadStatus.Failed Then
                            bgwUploadFile.ReportProgress(100, stmt & " Failed")
                        End If
                    Else
                        bgwUploadFile.ReportProgress(100, stmt & " File not found")
                    End If

                   
                End If
            Next

        Catch ex As Exception
            blUploadIsProgressing = False
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Upload_ProgressChanged(Progress As IUploadProgress)
        On Error Resume Next
        Control.CheckForIllegalCrossThreadCalls = False
        uUploadStatus = Progress.Status
    End Sub

    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUploadFile.ProgressChanged
        If TypeOf e.UserState Is Integer Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalFileCount
        End If

        If TypeOf e.UserState Is String Then
            Dim stmt As String = e.UserState.ToString.Substring(0, 3)
            Dim lblText As String = e.UserState.ToString.Substring(4)
            Dim clr As Color = Color.Black

            If lblText = "Uploading" Then
                clr = Color.Blue
            End If

            If lblText = "Uploaded" Then
                clr = Color.Green
            End If

            If lblText = "Failed" Then
                clr = Color.Red
            End If

            If lblText = "File not found" Then
                clr = Color.Red
            End If

            If lblText = "Already Uploaded" Then
                clr = Color.Brown
            End If


            Select Case stmt
                Case "soc"
                    lblSOC.Text = lblText
                    lblSOC.ForeColor = clr
                Case "ide"
                    lblID.Text = lblText
                    lblID.ForeColor = clr
                Case "gra"
                    lblGrave.Text = lblText
                    lblGrave.ForeColor = clr
                Case "mon"
                    lblMonthPerf.Text = lblText
                    lblMonthPerf.ForeColor = clr
                Case "qua"
                    lblQuarterlyPerf.Text = lblText
                    lblQuarterlyPerf.ForeColor = clr
                Case "ann"
                    lblAnnualPerf.Text = lblText
                    lblAnnualPerf.ForeColor = clr
            End Select
        End If

    End Sub
    Private Sub bgwUploadFile_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUploadFile.RunWorkerCompleted

        Me.Cursor = Cursors.Default
        Me.CircularProgress1.IsRunning = False
        Me.CircularProgress1.Text = ""
        Me.CircularProgress1.Hide()

    End Sub

#End Region


#Region "DOWNLOAD FILES"
    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            SelectedMonthIndex = Me.cmbMonth.SelectedIndex + 1
            SelectedMonthText = Me.cmbMonth.Text
            SelectedMonthYear = Me.txtMonthlyYear.Text

            SelectedQuarter = Me.txtQuarter.Text
            SelectedQuarterYear = Me.txtQuarterYear.Text

            SelectedAnnualYear = Me.txtAnnualYear.Text

            Me.CircularProgress1.ProgressText = "1/6"
            Me.CircularProgress1.IsRunning = True
            Me.CircularProgress1.Show()
            ShowLabels(True)
            bgwDownloadStatements.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgwDownloadStatements_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadStatements.DoWork
        Try
            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            Dim internalfolderid As String = ""

            Dim List = FISService.Files.List()
            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Internal File Transfer' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwDownloadStatements.ReportProgress(0, "all No stmt found")
                Exit Sub
            Else
                internalfolderid = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & FullDistrictName & "' and '" & internalfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute

            Dim DistrictFolderID As String = ""

            cnt = Results.Files.Count
            If cnt = 0 Then
                bgwDownloadStatements.ReportProgress(0, "all No stmt found")
                Exit Sub
            Else
                DistrictFolderID = Results.Files(0).Id
            End If


            Dim workdonefolderid As String = CreateFolderAndGetID("Work Done Statement", DistrictFolderID)
            Dim monthlyfolderid As String = CreateFolderAndGetID("Monthly Statements Backup", DistrictFolderID)

            If workdonefolderid = "" Or monthlyfolderid = "" Then
                bgwDownloadStatements.ReportProgress(0, "all No stmt found")
                Exit Sub
            End If

            Dim RemoteFileName As String = "SOC Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
            Dim DownloadFolder As String = SuggestedLocation & "\SOC Statement\" & SelectedMonthYear
            DownloadFile(RemoteFileName, DownloadFolder, "soc", monthlyfolderid, 1)

            RemoteFileName = "Grave Crime Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
            DownloadFolder = SuggestedLocation & "\Grave Crime Statement\" & SelectedMonthYear
            DownloadFile(RemoteFileName, DownloadFolder, "gra", monthlyfolderid, 2)

            RemoteFileName = "Identification Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
            DownloadFolder = SuggestedLocation & "\Identification Statement\" & SelectedMonthYear
            DownloadFile(RemoteFileName, DownloadFolder, "ide", monthlyfolderid, 3)

            RemoteFileName = "Monthly Performance Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
            DownloadFolder = SuggestedLocation & "\Performance Statement"
            DownloadFile(RemoteFileName, DownloadFolder, "mon", workdonefolderid, 4)

            RemoteFileName = "Quarterly Performance Statement - " & SelectedQuarterYear & " - Q" & SelectedQuarter & ".docx"
            DownloadFolder = SuggestedLocation & "\Performance Statement"
            DownloadFile(RemoteFileName, DownloadFolder, "qua", workdonefolderid, 5)

            RemoteFileName = "Annual Performance Statement - " & SelectedAnnualYear & ".docx"
            DownloadFolder = SuggestedLocation & "\Performance Statement"
            DownloadFile(RemoteFileName, DownloadFolder, "ann", workdonefolderid, 6)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DownloadFile(RemoteFileName As String, DownloadFolder As String, stmt As String, DistrictFolderID As String, filenumber As Integer)
        Try
            Dim FullDownloadPath As String = DownloadFolder & "\" & RemoteFileName
            My.Computer.FileSystem.CreateDirectory(DownloadFolder)
            If My.Computer.FileSystem.FileExists(FullDownloadPath) Then
                bgwDownloadStatements.ReportProgress(filenumber, stmt & " Already Exists")
                Exit Sub
            End If

            Dim List = FISService.Files.List()
            Dim Results As Google.Apis.Drive.v3.Data.FileList
            Dim cnt As Integer = 0

            List.Q = "name = '" & RemoteFileName & "' and '" & DistrictFolderID & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute
            cnt = Results.Files.Count

            If cnt = 0 Then
                bgwDownloadStatements.ReportProgress(filenumber, stmt & " No stmt found")
            Else
                bgwDownloadStatements.ReportProgress(filenumber, stmt & " Downloading")
                Dim fileid As String = Results.Files(0).Id
                Dim request = FISService.Files.Get(fileid)
                Dim file = request.Execute

                Dim fStream = New System.IO.FileStream(FullDownloadPath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                Dim mStream = New System.IO.MemoryStream

                Dim m = request.MediaDownloader

                AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

                request.DownloadWithStatus(mStream)

                If dDownloadStatus = DownloadStatus.Completed Then
                    bgwDownloadStatements.ReportProgress(filenumber, stmt & " Downloaded")
                    mStream.WriteTo(fStream)
                End If

                If dDownloadStatus = DownloadStatus.Failed Then
                    bgwDownloadStatements.ReportProgress(filenumber, stmt & " Failed")
                    mStream.WriteTo(fStream)
                End If

                fStream.Close()
                mStream.Close()
            End If

        Catch ex As Exception
            bgwDownloadStatements.ReportProgress(filenumber, stmt & " Failed")
        End Try
    End Sub

    Private Sub bgwDownloadStatements_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadStatements.ProgressChanged

        CircularProgress1.ProgressText = e.ProgressPercentage & "/6"

        If TypeOf e.UserState Is String Then
            Dim stmt As String = e.UserState.ToString.Substring(0, 3)
            Dim lblText As String = e.UserState.ToString.Substring(4)
            Dim clr As Color = Color.Black

            If lblText = "Already Exists" Then
                clr = Color.Blue
            End If

            If lblText = "Downloading" Then
                clr = Color.Blue
            End If

            If lblText = "Downloaded" Then
                clr = Color.Green
            End If

            If lblText = "Failed" Then
                clr = Color.Red
            End If

            If lblText = "No stmt found" Then
                clr = Color.Brown
            End If


            Select Case stmt
                Case "soc"
                    lblSOC.Text = lblText
                    lblSOC.ForeColor = clr
                Case "ide"
                    lblID.Text = lblText
                    lblID.ForeColor = clr
                Case "gra"
                    lblGrave.Text = lblText
                    lblGrave.ForeColor = clr
                Case "mon"
                    lblMonthPerf.Text = lblText
                    lblMonthPerf.ForeColor = clr
                Case "qua"
                    lblQuarterlyPerf.Text = lblText
                    lblQuarterlyPerf.ForeColor = clr
                Case "ann"
                    lblAnnualPerf.Text = lblText
                    lblAnnualPerf.ForeColor = clr
            End Select
        End If
    End Sub

    Private Sub bgwDownloadStatements_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadStatements.RunWorkerCompleted
        Me.CircularProgress1.IsRunning = False
        Me.CircularProgress1.Text = ""
        Me.CircularProgress1.Hide()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)
        On Error Resume Next
        Control.CheckForIllegalCrossThreadCalls = False
        dDownloadStatus = Progress.Status
    End Sub
#End Region


End Class