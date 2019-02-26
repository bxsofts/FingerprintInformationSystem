Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop

Imports System.IO

Imports Google
Imports Google.Apis.Auth
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports System.Threading


Public Class frmWeeklyDiary
    Dim dtWeeklyDiaryFrom As Date
    Dim dtWeeklyDiaryTo As Date
    Dim TemplateFile As String
    Dim sFileName As String

    Dim GDService As DriveService = New DriveService
    Dim JsonFile As String
    Dim TokenFile As String = ""

    Dim uBytesUploaded As Long
    Dim uUploadStatus As UploadStatus

    Dim dFileSize As Long

    Dim blUploadAuthenticated As Boolean = False

    Dim WeeklyDiaryFile As String

#Region "GENERATE WEEKLY DIARY"

    Private Sub frmWeeklyDiary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.CircularProgress1.ProgressColor = GetProgressColor()
            Me.CircularProgress1.Hide()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False

            cprgBackup.ProgressText = ""
            cprgBackup.ProgressColor = GetProgressColor()
            cprgBackup.IsRunning = False
            cprgBackup.Hide()

            blUploadAuthenticated = False

            Me.MonthCalendarAdv1.FirstDayOfWeek = System.DayOfWeek.Sunday
            Me.MonthCalendarAdv1.DisplayMonth = Today
            Dim lastweekdate As Date = Date.Today.AddDays(-7) 'gets day of last week
            Dim dayOfWeek = CInt(lastweekdate.DayOfWeek)
            dtWeeklyDiaryFrom = lastweekdate.AddDays(-1 * dayOfWeek)
            dtWeeklyDiaryTo = lastweekdate.AddDays(6 - dayOfWeek)

            Me.MonthCalendarAdv1.SelectedDate = dtWeeklyDiaryFrom
            Me.lblSelectedDate.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", culture)


            If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
            Me.SocRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.SocRegisterTableAdapter1.Connection.Open()
            Control.CheckForIllegalCrossThreadCalls = False

            RenameAndMoveOldFiles()

        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
        End Try


    End Sub


    Private Sub GenerateWeeklyDiary(sender As Object, e As EventArgs) Handles btnGenerateWeeklyDiary.Click
        Try
            If Me.MonthCalendarAdv1.SelectedDate.DayOfWeek <> DayOfWeek.Sunday Then
                If DevComponents.DotNetBar.MessageBoxEx.Show("Selected date is not Sunday. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            dtWeeklyDiaryFrom = Me.MonthCalendarAdv1.SelectedDate
            dtWeeklyDiaryTo = dtWeeklyDiaryFrom.AddDays(6)

            Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Weekly Diary\" & TI.Replace(",", "") & "\" & Year(dtWeeklyDiaryFrom).ToString
            System.IO.Directory.CreateDirectory(SaveFolder)
            sFileName = SaveFolder & "\Weekly Diary - " & dtWeeklyDiaryFrom.ToString("yyyy-MM-dd") & ".docx"
            If My.Computer.FileSystem.FileExists(sFileName) Then
                Shell("explorer.exe " & sFileName, AppWinStyle.MaximizedFocus)
                Exit Sub
            End If

            TemplateFile = strAppUserPath & "\WordTemplates\WeeklyDiary.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            CircularProgress1.ProgressText = "0"
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()


            Me.bgwWeeklyDiary.RunWorkerAsync("WeeklyDiary")

        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default

        End Try

    End Sub

    Private Sub bgwWeeklyDiary_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwWeeklyDiary.DoWork
        Try

            If e.Argument = "WeeklyDiary" Then
                For delay = 1 To 5
                    bgwWeeklyDiary.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)

                For delay = 6 To 10
                    bgwWeeklyDiary.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                wdDoc.Range.NoProofing = 1
                Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

                wdBooks("district1").Range.Text = FullDistrictName.ToUpper
                wdBooks("name").Range.Text = TI.Replace(", TI", ", Tester Inspector")
                wdBooks("office").Range.Text = ShortOfficeName
                wdBooks("district2").Range.Text = FullDistrictName
                wdBooks("fromdt").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", culture)
                wdBooks("todt").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy", culture)


                If boolUseTIinLetter Then
                    wdBooks("tiname").Range.Text = TIName()
                    wdBooks("ti").Range.Text = "Tester Inspector"
                    wdBooks("sdfpb").Range.Text = FullOfficeName
                    wdBooks("district3").Range.Text = FullDistrictName
                Else
                    wdBooks("tiname").Range.Text = ""
                    wdBooks("ti").Range.Text = ""
                    wdBooks("sdfpb").Range.Text = ""
                    wdBooks("district3").Range.Text = ""
                End If

                For delay = 11 To 19
                    bgwWeeklyDiary.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim cnt As Integer = Me.FingerPrintDataSet1.SOCRegister.Count

                Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

                For i = 2 To 8
                    With wdTbl
                        .Cell(i, 1).Range.Text = dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dd/MM/yyyy", culture) & vbNewLine & dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dddd")

                        Me.SocRegisterTableAdapter1.FillByInspectingOfficer(Me.FingerPrintDataSet1.SOCRegister, "%" & TI & "%", dtWeeklyDiaryFrom.AddDays(i - 2))
                        cnt = Me.FingerPrintDataSet1.SOCRegister.Count

                        If cnt = 0 Then


                            Dim dt = dtWeeklyDiaryFrom.AddDays(i - 2)
                            Dim msg = ""

                            If IsHoliday(dt) Then
                                msg = "Availed Holiday"
                            Else
                                msg = "Attended office duty"
                            End If

                            .Cell(i, 2).Range.Text = msg
                        End If

                        If cnt = 1 Then
                            .Cell(i, 2).Range.Text = "Inspected SOC in Cr.No. " & Me.FingerPrintDataSet1.SOCRegister(0).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(0).PoliceStation & " P.S"
                        End If

                        If cnt > 1 Then
                            Dim details As String = ""
                            For j = 0 To cnt - 1
                                If j <> cnt - 1 Then
                                    details = details & "Cr.No " & Me.FingerPrintDataSet1.SOCRegister(j).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(j).PoliceStation & " P.S; "
                                Else
                                    details = details.Remove(details.Length - 2)
                                    details = details & " and Cr.No " & Me.FingerPrintDataSet1.SOCRegister(j).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(j).PoliceStation & " P.S"
                                End If

                            Next
                            .Cell(i, 2).Range.Text = "Inspected SOC in " & details
                        End If

                    End With

                    bgwWeeklyDiary.ReportProgress(i * 10)
                    System.Threading.Thread.Sleep(10)

                Next
                For i = 81 To 99
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                bgwWeeklyDiary.ReportProgress(100)
                System.Threading.Thread.Sleep(10)

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                If My.Computer.FileSystem.FileExists(sFileName) = False Then
                    wdDoc.SaveAs(sFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                ReleaseObject(wdTbl)
                ReleaseObject(wdBooks)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

            If e.Argument = "CL" Then

                For i = 1 To 10
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
                wdDoc.Range.NoProofing = 1
                Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

                For i = 11 To 20
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                wdBooks("FileNo").Range.Text = "No. " & PdlWeeklyDiary & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName
                wdBooks("OfficeName1").Range.Text = FullOfficeName
                wdBooks("District1").Range.Text = FullDistrictName
                wdBooks("Date1").Range.Text = Today.ToString("dd/MM/yyyy", culture)

                wdBooks("Name1").Range.Text = TIName()
                wdBooks("OfficeName2").Range.Text = FullOfficeName
                wdBooks("District2").Range.Text = FullDistrictName

                wdBooks("DateFrom").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", culture)
                wdBooks("DateTo").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy", culture)

                For i = 21 To 30
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                If boolUseTIinLetter Then
                    wdBooks("Name2").Range.Text = TIName()
                    wdBooks("Designation").Range.Text = "Tester Inspector"
                    wdBooks("OfficeName3").Range.Text = FullOfficeName
                    wdBooks("District3").Range.Text = FullDistrictName
                Else
                    wdBooks("Name2").Range.Text = ""
                    wdBooks("Designation").Range.Text = ""
                    wdBooks("OfficeName3").Range.Text = ""
                    wdBooks("District3").Range.Text = ""
                End If

                For i = 31 To 99
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                bgwWeeklyDiary.ReportProgress(100)
                System.Threading.Thread.Sleep(10)

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                ReleaseObject(wdBooks)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub bgwWeeklyDiary_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwWeeklyDiary.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwWeeklyDiary_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwWeeklyDiary.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default

        If e.Error IsNot Nothing Then
            DevComponents.DotNetBar.MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub GenerateWeeklyDiaryCL() Handles btnCoveringLetter.Click
        Try
            TemplateFile = strAppUserPath & "\WordTemplates\WeeklyDiaryCL.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor

            CircularProgress1.ProgressText = "0"
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()

            Me.bgwWeeklyDiary.RunWorkerAsync("CL")
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub btnOpenWeeklyDiaryFolder_Click(sender As Object, e As EventArgs) Handles btnOpenWeeklyDiaryFolder.Click
        Try
            Dim WeeklyDiaryFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Weekly Diary\" & TI.Replace(",", "")
            Dim sfilename = WeeklyDiaryFolder & "\Weekly Diary - " & Me.MonthCalendarAdv1.SelectedDate.ToString("yyyy-MM-dd") & ".docx"

            If FileIO.FileSystem.FileExists(sfilename) Then
                Call Shell("explorer.exe /select," & sfilename, AppWinStyle.NormalFocus)
                Exit Sub
            End If

            If Not FileIO.FileSystem.DirectoryExists(WeeklyDiaryFolder) Then
                FileIO.FileSystem.CreateDirectory(WeeklyDiaryFolder)
            End If

            Call Shell("explorer.exe " & WeeklyDiaryFolder, AppWinStyle.NormalFocus)
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
        End Try
    End Sub


    Private Sub MonthCalendarAdv1_ItemClick(sender As Object, e As EventArgs) Handles MonthCalendarAdv1.ItemClick
        Me.lblSelectedDate.Text = Me.MonthCalendarAdv1.SelectedDate.ToString("dd/MM/yyyy", culture)
    End Sub


    Private Sub RenameAndMoveOldFiles()
        Try

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(FileIO.SpecialDirectories.MyDocuments & "\Weekly Diary\" & TI.Replace(",", ""), FileIO.SearchOption.SearchTopLevelOnly, "*.docx")

                If foundFile Is Nothing Then
                    Exit Sub
                End If

                Dim OldFileName As String
                Dim NewFileName As String

                OldFileName = My.Computer.FileSystem.GetName(foundFile)

                Dim SplitText() = Strings.Split(OldFileName, " - ")
                Dim u = SplitText.GetUpperBound(0)


                If u = 1 Then
                    Dim y As String = SplitText(1)
                    y = y.Substring(0, 4)

                    Dim SaveFolder As String = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & y
                    My.Computer.FileSystem.CreateDirectory(SaveFolder)

                    NewFileName = SaveFolder & "\" & OldFileName

                    My.Computer.FileSystem.MoveFile(foundFile, NewFileName)

                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
#End Region


#Region "BACKUP TO GOOGLE DRIVE"
    Private Sub btnUploadToGoogleDrive_Click(sender As Object, e As EventArgs) Handles btnUploadToGoogleDrive.Click

        Try

            Dim WeeklyDiaryFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Weekly Diary\" & TI.Replace(",", "") & "\" & (Me.MonthCalendarAdv1.SelectedDate.Year).ToString
            WeeklyDiaryFile = WeeklyDiaryFolder & "\Weekly Diary - " & Me.MonthCalendarAdv1.SelectedDate.ToString("yyyy-MM-dd") & ".docx"

            If My.Computer.FileSystem.FileExists(WeeklyDiaryFile) = False Then
                MessageBoxEx.Show("Weekly Diary File not found.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Me.Cursor = Cursors.Default

            If Not blUploadAuthenticated Then
                frmPassword.ShowDialog()
                If Not blUserAuthenticated Then Exit Sub
            End If

            blUploadAuthenticated = True


            JsonFile = CredentialFilePath & "\FISOAuth2.json"

            If Not FileIO.FileSystem.FileExists(JsonFile) Then 'copy from application folder
                My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
                FileSystem.FileCopy(strAppPath & "\FISOAuth2.json", CredentialFilePath & "\FISOAuth2.json")
            End If

            TokenFile = CredentialFilePath & "\Google.Apis.Auth.OAuth2.Responses.TokenResponse-" & oAuthUserID ' token file is created after authentication

            If Not FileIO.FileSystem.FileExists(TokenFile) Then 'check for token file.
                If MessageBoxEx.Show("The application will now open your browser. Please enter your Google ID and password to authenticate.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.WaitCursor

            cprgBackup.Visible = True
            cprgBackup.IsRunning = True
            lblBackup.Visible = True

            Dim year As String = Me.MonthCalendarAdv1.SelectedDate.Year.ToString
            bgwUploadFile.RunWorkerAsync(year)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUploadFile.DoWork
        Try
            bgwUploadFile.ReportProgress(0, "Creating Google Drive Service...")
            Dim fStream As FileStream = New FileStream(JsonFile, FileMode.Open, FileAccess.Read)
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim sUserCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, oAuthUserID, CancellationToken.None, New FileDataStore(strAppName)).Result

            GDService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = sUserCredential, .ApplicationName = strAppName})

            If GDService.ApplicationName <> strAppName Then
                bgwUploadFile.ReportProgress(0, "Google Drive Service failed.")
                Exit Sub
            End If

            Dim masterfolderid As String = ""
            Dim List = GDService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Weekly Diary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwUploadFile.ReportProgress(0, "Creating Weekly Diary folder...")
                Threading.Thread.Sleep(100)
                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                NewDirectory.Name = "Weekly Diary"
                NewDirectory.MimeType = "application/vnd.google-apps.folder"
                Dim request As FilesResource.CreateRequest = GDService.Files.Create(NewDirectory)
                NewDirectory = request.Execute()
                masterfolderid = NewDirectory.Id
            Else
                masterfolderid = Results.Files(0).Id
            End If

            Dim parentlist As New List(Of String)
            parentlist.Add(masterfolderid)

            Dim SubFolderName As String = e.Argument
            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & SubFolderName & "' and '" & masterfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute

            cnt = Results.Files.Count

            Dim subfolderid As String = ""
            If cnt = 0 Then
                bgwUploadFile.ReportProgress(0, "Creating Weekly Diary sub folder...")
                Threading.Thread.Sleep(100)
                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                NewDirectory.Name = SubFolderName
                NewDirectory.MimeType = "application/vnd.google-apps.folder"
                NewDirectory.Parents = parentlist
                Dim request As FilesResource.CreateRequest = GDService.Files.Create(NewDirectory)
                NewDirectory = request.Execute()
                subfolderid = NewDirectory.Id
            Else
                subfolderid = Results.Files(0).Id
            End If



            Dim fName As String = My.Computer.FileSystem.GetFileInfo(WeeklyDiaryFile).Name
            dFileSize = My.Computer.FileSystem.GetFileInfo(WeeklyDiaryFile).Length

            Dim parentlist1 As New List(Of String)
            parentlist1.Add(subfolderid)

            List = GDService.Files.List()
            List.Q = "name = '" & fName & "' and trashed = false and '" & subfolderid & "' in parents" ' list files in parent folder. 
            List.Fields = "files(id)"

            Results = List.Execute
            cnt = Results.Files.Count
            If cnt = 0 Then 'if file not exitsts then create new file

                bgwUploadFile.ReportProgress(0, "Uploading Weekly Diary...")
                Threading.Thread.Sleep(100)

                Dim body As New Google.Apis.Drive.v3.Data.File()
                body.Parents = parentlist1
                body.Name = My.Computer.FileSystem.GetFileInfo(WeeklyDiaryFile).Name
                body.MimeType = "files/docx"

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(WeeklyDiaryFile)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                Dim UploadRequest As FilesResource.CreateMediaUpload = GDService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                UploadRequest.Upload()
                Stream.Close()

                If uUploadStatus = UploadStatus.Completed Then
                    bgwUploadFile.ReportProgress(100, "Weekly Diary uploaded.")
                End If
            Else 'file exists. Update contetnt

                bgwUploadFile.ReportProgress(0, "Updating Weekly Diary...")
                Threading.Thread.Sleep(100)

                Dim id As String = Results.Files(0).Id
                Dim body As New Google.Apis.Drive.v3.Data.File
                body.Name = fName
                body.MimeType = "files/docx"

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(WeeklyDiaryFile)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                Dim UpdateRequest As FilesResource.UpdateMediaUpload = GDService.Files.Update(body, id, Stream, body.MimeType)
                UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UpdateRequest.ProgressChanged, AddressOf Upload_ProgressChanged
                UpdateRequest.Upload()
                Stream.Close()

                If uUploadStatus = UploadStatus.Completed Then
                    bgwUploadFile.ReportProgress(100, "Weekly Diary updated.")
                End If
            End If


        Catch ex As Exception
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
        cprgBackup.ProgressText = e.ProgressPercentage

        If TypeOf e.UserState Is String Then
            If e.ProgressPercentage = 100 Then
                ShowDesktopAlert(e.UserState)
            End If
            lblBackup.Text = e.UserState
        End If
    End Sub
    Private Sub bgwUploadFile_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUploadFile.RunWorkerCompleted

        cprgBackup.Visible = False
        cprgBackup.IsRunning = False

        Me.Cursor = Cursors.Default
        Me.lblBackup.Text = "Backup Weekly Diary to Google Drive"
    End Sub

#End Region


End Class