
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

    Dim blServiceCreated As Boolean = False
    Dim blCheckBoxes As Boolean = False

    Dim SelectedDistrict As String = ""
    Dim SelectedDistrictID As String = ""
    Dim SelectedMonth As Integer
    Dim SelectedYear As String = ""
    Dim SelectedQuarter As String = ""
    Dim SelectedQuarterYear As String = ""
    Dim TotalFileCount As Integer = 0
    Dim UserFolderID As String = ""
    Private Sub frmOnlineSendStatements_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            Me.ListViewEx1.Items.Clear()
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


            If m = 1 Then
                m = 12
                y = y - 1
            Else
                m = m - 1
            End If

            Me.cmbMonth.SelectedIndex = m - 1
            Me.txtYear.Value = y

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
            bgwListFiles.RunWorkerAsync("root")
            

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
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
    End Sub

    Private Sub CheckForMonthlyStatementFiles() Handles cmbMonth.SelectedValueChanged, txtYear.ValueChanged
        On Error Resume Next
        If Not blCheckBoxes Then Exit Sub
        ShowLabels(False)
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

        Dim m = DateAndTime.Month(Today)
        If (m = 1 Or m = 4 Or m = 7 Or m = 10) And chkQuarterlyPerf.Enabled Then
            chkQuarterlyPerf.Checked = True
        Else
            chkQuarterlyPerf.Checked = False
        End If
    End Sub


#Region "LOAD DISTRICT LIST"

    Private Sub LoadDistrictList(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwListFiles.DoWork
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
                    If Result.Name.ToLower = FullDistrictName.ToLower Then
                        UserFolderID = Result.Id
                    End If
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

        If Me.ListViewEx1.SelectedItems.Count = 1 Then
            SelectedDistrict = Me.ListViewEx1.SelectedItems(0).Text
            SelectedDistrictID = Me.ListViewEx1.SelectedItems(0).SubItems(1).Text
            SelectedMonth = Me.cmbMonth.SelectedIndex + 1
            SelectedYear = Me.txtYear.Text
            SelectedQuarter = Me.txtQuarter.Text
            SelectedQuarterYear = Me.txtQuarterYear.Text
            ShowLabels(True)
            bgwCheckSentStatus.RunWorkerAsync()
        End If

    End Sub

#End Region


#Region "CHECK FILE SENT STATUS"

    Private Sub bgwCheckSentStatus_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCheckSentStatus.DoWork
        Try
            Dim monthlyfolderid As String = CreateFolderAndGetID("Statements - " & FullDistrictName, SelectedDistrictID)
            If monthlyfolderid = "" Then Exit Sub

            Dim List As FilesResource.ListRequest = FISService.Files.List()
            Dim NewFileName As String = ""

            NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "SOC.docx"
            List.Q = "name = '" & NewFileName & "' and '" & monthlyfolderid & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute
            Dim cnt = Results.Files.Count

            If cnt > 0 Then
                bgwCheckSentStatus.ReportProgress(100, "soc Already Sent")
            End If

            NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "Grave.docx"
            List.Q = "name = '" & NewFileName & "' and '" & monthlyfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute
            cnt = Results.Files.Count

            If cnt > 0 Then
                bgwCheckSentStatus.ReportProgress(100, "gra Already Sent")
            End If

            NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "Identification.docx"
            List.Q = "name = '" & NewFileName & "' and '" & monthlyfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute
            cnt = Results.Files.Count

            If cnt > 0 Then
                bgwCheckSentStatus.ReportProgress(100, "ide Already Sent")
            End If

            NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "WorkDone.docx"
            List.Q = "name = '" & NewFileName & "' and '" & monthlyfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute
            cnt = Results.Files.Count

            If cnt > 0 Then
                bgwCheckSentStatus.ReportProgress(100, "mon Already Sent")
            End If

            NewFileName = ShortDistrictName.ToUpper & "-" & SelectedQuarterYear & "-Q" & SelectedQuarter & "-" & "WorkDone.docx"
            List.Q = "name = '" & NewFileName & "' and '" & monthlyfolderid & "' in parents"
            List.Fields = "files(id)"

            Results = List.Execute
            cnt = Results.Files.Count

            If cnt > 0 Then
                bgwCheckSentStatus.ReportProgress(100, "qua Already Sent")
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub bgwCheckSentStatus_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwCheckSentStatus.ProgressChanged
        If TypeOf e.UserState Is String Then
            Dim stmt As String = e.UserState.ToString.Substring(0, 3)
            Dim lblText As String = "Already Sent"
            Dim clr As Color = Color.Brown
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
            End Select
        End If
    End Sub



#End Region


#Region "SEND FILES"
    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        If Me.ListViewEx1.Items.Count = 0 Then
            MessageBoxEx.Show("List of Districts is empty. Please close the form and try again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.ListViewEx1.SelectedItems.Count = 0 Then
            MessageBoxEx.Show("Please select a District.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not Me.chkSOC.Checked And Not Me.chkGrave.Checked And Not Me.chkID.Checked And Not Me.chkMonthlyPerf.Checked And Not Me.chkQuarterlyPerf.Checked Then
            MessageBoxEx.Show("No statements selected to upload.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim FileList(4) As String
        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1

        TotalFileCount = 0
        Dim StmtFolder As String = SuggestedLocation & "\SOC Statement\" & Me.txtYear.Text
        Dim StmtFileName As String = StmtFolder & "\SOC Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
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

        StmtFolder = SuggestedLocation & "\Grave Crime Statement\" & Me.txtYear.Text
        StmtFileName = StmtFolder & "\Grave Crime Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
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

        StmtFolder = SuggestedLocation & "\Identification Statement\" & Me.txtYear.Text
        StmtFileName = StmtFolder & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
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
        StmtFileName = StmtFolder & "\Monthly Performance Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
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
        SelectedQuarter = Me.txtQuarter.Text
        SelectedQuarterYear = Me.txtQuarterYear.Text

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "RangeDistrict", SelectedDistrict, Microsoft.Win32.RegistryValueKind.String)

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

            Dim monthlyfolderid As String = CreateFolderAndGetID("Statements - " & FullDistrictName, SelectedDistrictID)
            Dim monthlyworkdonefolderid As String = CreateFolderAndGetID("Work Done Statement - Monthly" & FullDistrictName, UserFolderID)
            Dim quarterlyworkdonefolderid As String = CreateFolderAndGetID("Work Done Statement - Quarterly" & FullDistrictName, UserFolderID)

            If monthlyfolderid = "" Then Exit Sub

            Dim FileList() As String = e.Argument
            Dim currentfilenumber As Integer = 0
            For i = 0 To 4

                Dim SelectedFile = FileList(i)
                If SelectedFile = "" Then
                    Continue For
                End If
                currentfilenumber = currentfilenumber + 1
                bgwUploadFile.ReportProgress(currentfilenumber, currentfilenumber)
                Dim SelectedFileName As String = My.Computer.FileSystem.GetFileInfo(SelectedFile).Name
                Dim NewFileName As String = ""

                Dim stmt As String = SelectedFileName.ToLower.Substring(0, 3)

                Select Case stmt
                    Case "soc"
                        NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "SOC.docx"
                    Case "gra"
                        NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "Grave.docx"
                    Case "ide"
                        NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "Identification.docx"
                    Case "mon"
                        NewFileName = ShortDistrictName.ToUpper & "-" & SelectedYear & "-" & SelectedMonth.ToString("D2") & "-" & "WorkDone.docx"
                    Case "qua"
                        NewFileName = ShortDistrictName.ToUpper & "-" & SelectedQuarterYear & "-Q" & SelectedQuarter & "-" & "WorkDone.docx"
                    Case Else
                        Continue For
                End Select

                Dim List = FISService.Files.List()

                List.Q = "name = '" & NewFileName & "' and '" & monthlyfolderid & "' in parents"
                List.Fields = "files(id)"

                Dim Results = List.Execute
                Dim cnt = Results.Files.Count

                If cnt > 0 Then
                    '  VersionFolderID = Results.Files(0).Id
                    bgwUploadFile.ReportProgress(100, stmt & " Already Sent")
                Else
                    Dim body As New Google.Apis.Drive.v3.Data.File()
                    body.Name = NewFileName
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

                    Stream.Close()
                    If uUploadStatus = UploadStatus.Completed Then
                        bgwUploadFile.ReportProgress(100, stmt & " Uploaded")
                        Dim tFile = New Google.Apis.Drive.v3.Data.File
                        tFile.ModifiedTime = Now
                        FISService.Files.Update(tFile, monthlyfolderid).Execute()
                    End If
                    If uUploadStatus = UploadStatus.Failed Then
                        bgwUploadFile.ReportProgress(100, stmt & " Failed")
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

        Control.CheckForIllegalCrossThreadCalls = False
        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
    End Sub

    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUploadFile.ProgressChanged
        If TypeOf e.UserState Is Integer Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalFileCount
        End If

        If TypeOf e.UserState Is String Then
            Dim stmt As String = e.UserState.ToString.Substring(0, 3)
            Dim lblText As String = e.UserState.ToString.Substring(4)
            Dim clr As Color

            If lblText = "Uploaded" Then
                clr = Color.Green
            End If

            If lblText = "Failed" Then
                clr = Color.Red
            End If

            If lblText = "Already Sent" Then
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

End Class