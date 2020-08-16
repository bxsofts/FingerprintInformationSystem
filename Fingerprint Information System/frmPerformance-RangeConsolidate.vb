Imports System.IO
Imports DevComponents.DotNetBar

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Imports Microsoft.Office.Interop


Public Class frmPerformance_RangeConsolidate

    Dim Range As String = ""

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential

    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus

    Dim blServiceCreated As Boolean = False

    Dim SelectedDistrict As String = ""
    Dim SelectedDistrictID As String = ""

    Dim SelectedMonthIndex As Integer
    Dim SelectedMonthText As String = ""
    Dim SelectedMonthYear As String = ""

    Dim SelectedQuarter As String = ""
    Dim SelectedQuarterYear As String = ""

    Dim SelectedAnnualYear As String = ""

    Dim TotalDistrictCount As Integer = 5
    Dim UserFolderID As String = ""

    Dim DistrictList(4) As String
    Dim InternalFolderID As String = ""

    Dim blAllFilesDownloaded As Boolean = False

    Dim ConsolidatedPerformanceFolder As String = ""
    Dim ConsolidatedStatementsFolder As String = ""

#Region "FORM LOAD EVENTS"

    Private Sub frmPerformance_RangeConsolidate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            ConsolidatedPerformanceFolder = SuggestedLocation & "\Consolidated Performance Statement"
            My.Computer.FileSystem.CreateDirectory(ConsolidatedPerformanceFolder)

            ConsolidatedStatementsFolder = SuggestedLocation & "\Consolidated Monthly Statements"
            My.Computer.FileSystem.CreateDirectory(ConsolidatedStatementsFolder)

            ClearLabels()
            Me.CircularProgress1.Visible = False
            Me.CircularProgress1.ProgressColor = GetProgressColor()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False

            If FullDistrictName.ToLower.Contains("thiruvananthapuram") Then
                Range = "Thiruvananthapuram"
            End If

            If FullDistrictName.ToLower.Contains("kollam") Then
                Range = "Thiruvananthapuram"
            End If

            If FullDistrictName.ToLower.Contains("pathanamthitta") Then
                Range = "Thiruvananthapuram"
            End If

            If FullDistrictName.ToLower.Contains("alappuzha") Then
                Range = "Ernakulam"
            End If

            If FullDistrictName.ToLower.Contains("kottayam") Then
                Range = "Ernakulam"
            End If

            If FullDistrictName.ToLower.Contains("idukki") Then
                Range = "Ernakulam"
            End If

            If FullDistrictName.ToLower.Contains("ernakulam") Then
                Range = "Ernakulam"
            End If

            If FullDistrictName.ToLower.Contains("kochi") Then
                Range = "Ernakulam"
            End If

            If FullDistrictName.ToLower.Contains("thrissur") Then
                Range = "Thrissur"
            End If

            If FullDistrictName.ToLower.Contains("palakkad") Then
                Range = "Thrissur"
            End If

            If FullDistrictName.ToLower.Contains("malappuram") Then
                Range = "Thrissur"
            End If

            If FullDistrictName.ToLower.Contains("kozhikode") Then
                Range = "Kannur"
            End If

            If FullDistrictName.ToLower.Contains("wayanad") Then
                Range = "Kannur"
            End If

            If FullDistrictName.ToLower.Contains("kannur") Then
                Range = "Kannur"
            End If

            If FullDistrictName.ToLower.Contains("kasara") Then
                Range = "Kannur"
            End If

            Me.Text = "Consolidate Work Done - " & Range & " Range"
            Me.TitleText = "<b>Consolidate Work Done - " & Range & " Range</b>"

            Select Case Range
                Case "Thiruvananthapuram"
                    Me.lblDistrict1.Text = "Thiruvananthapuram City"
                    Me.lblDistrict2.Text = "Thiruvananthapuram Rural"
                    Me.lblDistrict3.Text = "Kollam City"
                    Me.lblDistrict4.Text = "Kollam Rural"
                    Me.lblDistrict5.Text = "Pathanamthitta"
                    TotalDistrictCount = 5
                    DistrictList(0) = "Thiruvananthapuram City"
                    DistrictList(1) = "Thiruvananthapuram Rural"
                    DistrictList(2) = "Kollam City"
                    DistrictList(3) = "Kollam Rural"
                    DistrictList(4) = "Pathanamthitta"
                Case "Ernakulam"
                    Me.lblDistrict1.Text = "Alappuzha"
                    Me.lblDistrict2.Text = "Kottayam"
                    Me.lblDistrict3.Text = "Idukki"
                    Me.lblDistrict4.Text = "Kochi City"
                    Me.lblDistrict5.Text = "Ernakulam Rural"
                    TotalDistrictCount = 5
                    DistrictList(0) = "Alappuzha"
                    DistrictList(1) = "Kottayam"
                    DistrictList(2) = "Idukki"
                    DistrictList(3) = "Kochi City"
                    DistrictList(4) = "Ernakulam Rural"
                Case "Thrissur"
                    Me.lblDistrict1.Text = "Thrissur City"
                    Me.lblDistrict2.Text = "Thrissur Rural"
                    Me.lblDistrict3.Text = "Palakkad"
                    Me.lblDistrict4.Text = "Malappuram"
                    Me.lblDistrict5.Text = ""
                    TotalDistrictCount = 4
                    DistrictList(0) = "Thrissur City"
                    DistrictList(1) = "Thrissur Rural"
                    DistrictList(2) = "Palakkad"
                    DistrictList(3) = "Malappuram"
                    DistrictList(4) = ""
                Case "Kannur"
                    Me.lblDistrict1.Text = "Kozhikode City"
                    Me.lblDistrict2.Text = "Kozhikode Rural"
                    Me.lblDistrict3.Text = "Wayanad"
                    Me.lblDistrict4.Text = "Kannur"
                    Me.lblDistrict5.Text = "Kasaragod"
                    TotalDistrictCount = 5
                    DistrictList(0) = "Kozhikode City"
                    DistrictList(1) = "Kozhikode Rural"
                    DistrictList(2) = "Wayanad"
                    DistrictList(3) = "Kannur"
                    DistrictList(4) = "Kasaragod"
                Case Else
                    Me.lblDistrict1.Text = ""
                    Me.lblDistrict2.Text = ""
                    Me.lblDistrict3.Text = ""
                    Me.lblDistrict4.Text = ""
                    Me.lblDistrict5.Text = ""

            End Select

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

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ClearLabels()
        Me.lbl1.Text = ""
        Me.lbl2.Text = ""
        Me.lbl3.Text = ""
        Me.lbl4.Text = ""
        Me.lbl5.Text = ""

        Me.lblID1.Text = ""
        Me.lblID2.Text = ""
        Me.lblID3.Text = ""
        Me.lblID4.Text = ""
        Me.lblID5.Text = ""

        Me.lblSOC1.Text = ""
        Me.lblSOC2.Text = ""
        Me.lblSOC3.Text = ""
        Me.lblSOC4.Text = ""
        Me.lblSOC5.Text = ""

        lblStmt.Text = ""

        Me.lblGrave.Text = ""
        Me.lblSOC.Text = ""
        Me.lblID.Text = ""
    End Sub


#End Region


#Region "DOWNLOAD STATEMENTS"
    Private Sub btnDownloadStatements_Click(sender As Object, e As EventArgs) Handles btnDownloadStatements.Click
        Me.Cursor = Cursors.WaitCursor

        SelectedMonthIndex = Me.cmbMonth.SelectedIndex + 1
        SelectedMonthText = Me.cmbMonth.Text
        SelectedMonthYear = Me.txtMonthlyYear.Text

        ClearLabels()

        Me.lblGrave.Text = "Grave"
        Me.lblSOC.Text = "SOC"
        Me.lblID.Text = "Identification"

        Me.lblStmt.Text = "Download Statements for " & SelectedMonthText & " - " & SelectedMonthYear

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.CircularProgress1.ProgressText = "1/" & TotalDistrictCount
        Me.CircularProgress1.IsRunning = True
        Me.CircularProgress1.Show()

        bgwDownloadStatements.RunWorkerAsync(DistrictList)
    End Sub


    Private Sub bgwDownloadStatements_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadStatements.DoWork
        Try
            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            If InternalFolderID = "" Then
                InternalFolderID = GetFolderID("Internal File Transfer", "root")
            End If

            Dim currentdistrictnumber As Integer = 0

            Dim List = FISService.Files.List()
            Dim Results As Google.Apis.Drive.v3.Data.FileList
            Dim cnt As Integer = 0

            For i = 0 To 4

                SelectedDistrict = DistrictList(i)
                Dim blUserDistrict As Boolean = FullDistrictName.ToLower.StartsWith(SelectedDistrict.ToLower)
                If SelectedDistrict = "" Then
                    Continue For
                End If

                currentdistrictnumber = currentdistrictnumber + 1
                bgwDownloadStatements.ReportProgress(currentdistrictnumber, currentdistrictnumber)

                Dim DownloadFileName As String = ""
                Dim DownloadFolder As String = ""

                If blUserDistrict Then
                    DownloadFileName = "Grave Crime Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
                    DownloadFolder = SuggestedLocation & "\Grave Crime Statement\" & SelectedMonthYear
                Else
                    DownloadFileName = SelectedDistrict & "-" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-" & "Grave.docx"
                    DownloadFolder = ConsolidatedStatementsFolder
                End If











                If blUserDistrict Then
                    DownloadFileName = "SOC Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
                    DownloadFolder = SuggestedLocation & "\SOC Statement\" & SelectedMonthYear
                Else
                    DownloadFileName = SelectedDistrict & "-" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-" & "SOC.docx"
                    DownloadFolder = ConsolidatedStatementsFolder
                End If

                
                If blUserDistrict Then
                    DownloadFileName = "Identification Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx"
                    DownloadFolder = SuggestedLocation & "\Identification Statement\" & SelectedMonthYear
                Else
                    DownloadFileName = SelectedDistrict & "-" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-" & "Identification.docx"
                    DownloadFolder = ConsolidatedStatementsFolder
                End If




                Dim districtfolderid As String = GetFolderID(SelectedDistrict, InternalFolderID)
                If districtfolderid = "" Then
                    bgwDownloadStatements.ReportProgress(currentdistrictnumber, "all No stmt found")
                    Continue For
                End If

                Dim statementfolderid As String = GetFolderID("Monthly Statements Backup", districtfolderid)
                If statementfolderid = "" Then
                    bgwDownloadStatements.ReportProgress(currentdistrictnumber, "all No stmt found")
                    Continue For
                End If

                List.Q = "name = 'Grave Crime Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx' and '" & statementfolderid & "' in parents"
                List.Fields = "files(id)"

                Results = List.Execute
                cnt = Results.Files.Count

                If cnt = 0 Then
                    bgwDownloadStatements.ReportProgress(currentdistrictnumber, "gra No stmt found")
                Else

                    



                    DownloadFile(Results.Files(j).Id, DownloadFolder & "\" & DownloadFileName)

                End If
            Next


        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub DownloadFile(FileID As String, FullDownloadPath As String, currentdistrictnumber As Integer, stmt As String)
        Try

            If My.Computer.FileSystem.FileExists(FullDownloadPath) Then
                If FullDistrictName.ToLower.StartsWith(SelectedDistrict.ToLower) Then
                    bgwDownloadStatements.ReportProgress(currentdistrictnumber, stmt & " Already Exists")
                Else
                    bgwDownloadStatements.ReportProgress(currentdistrictnumber, stmt & " Already Downloaded")
                End If
            End If

            bgwDownloadStatements.ReportProgress(currentdistrictnumber, stmt & " Downloading")

            Dim request = FISService.Files.Get(FileID)
            Dim file = request.Execute

            Dim fStream = New System.IO.FileStream(FullDownloadPath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
            Dim mStream = New System.IO.MemoryStream

            Dim m = request.MediaDownloader

            AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

            request.DownloadWithStatus(mStream)

            If dDownloadStatus = DownloadStatus.Completed Then
                bgwDownloadStatements.ReportProgress(currentdistrictnumber, stmt & " Downloaded")
                mStream.WriteTo(fStream)
            End If

            If dDownloadStatus = DownloadStatus.Failed Then
                bgwDownloadStatements.ReportProgress(currentdistrictnumber, stmt & " Failed")
                mStream.WriteTo(fStream)
            End If

            fStream.Close()
            mStream.Close()
        Catch ex As Exception
            bgwDownloadStatements.ReportProgress(currentdistrictnumber, stmt & " Failed")
        End Try
    End Sub

    Private Sub bgwDownloadStatements_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadStatements.ProgressChanged

        If TypeOf e.UserState Is Integer Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalDistrictCount
        End If

        If TypeOf e.UserState Is String Then
            Dim districtnumber As Integer = e.ProgressPercentage
            Dim Status As String = e.UserState.ToString
            Dim stmt As String = Status.Substring(0, 3)
            Dim lblText As String = Status.Substring(4)

            Dim clr As Color = Color.Black

            If lblText.StartsWith("No stmt") Then
                clr = Color.Red
            End If

            If lblText.StartsWith("Already") Then
                clr = Color.Brown
            End If

            If lblText.StartsWith("Downloading") Then
                clr = Color.Blue
            End If

            If lblText.StartsWith("Downloaded") Then
                clr = Color.Green
            End If

            If lblText = "Failed" Then
                clr = Color.Red
            End If

            Select Case districtnumber
                Case 1
                    Select Case stmt
                        Case "all"
                            Me.lbl1.Text = lblText
                            Me.lbl1.ForeColor = clr

                            Me.lblID1.Text = lblText
                            Me.lblID1.ForeColor = clr

                            Me.lblSOC1.Text = lblText
                            Me.lblSOC1.ForeColor = clr

                        Case "soc"
                            lblSOC1.Text = lblText
                            lblSOC1.ForeColor = clr
                        Case "ide"
                            lblID1.Text = lblText
                            lblID1.ForeColor = clr
                        Case "gra"
                            Me.lbl1.Text = lblText
                            Me.lbl1.ForeColor = clr
                    End Select
                Case 2
                    Select Case stmt
                        Case "all"
                            Me.lbl2.Text = lblText
                            Me.lbl2.ForeColor = clr

                            Me.lblID2.Text = lblText
                            Me.lblID2.ForeColor = clr

                            Me.lblSOC2.Text = lblText
                            Me.lblSOC2.ForeColor = clr

                        Case "soc"
                            lblSOC2.Text = lblText
                            lblSOC2.ForeColor = clr
                        Case "ide"
                            lblID2.Text = lblText
                            lblID2.ForeColor = clr
                        Case "gra"
                            Me.lbl2.Text = lblText
                            Me.lbl2.ForeColor = clr
                    End Select
                Case 3
                    Select Case stmt
                        Case "all"
                            Me.lbl3.Text = lblText
                            Me.lbl3.ForeColor = clr

                            Me.lblID3.Text = lblText
                            Me.lblID3.ForeColor = clr

                            Me.lblSOC3.Text = lblText
                            Me.lblSOC3.ForeColor = clr

                        Case "soc"
                            lblSOC3.Text = lblText
                            lblSOC3.ForeColor = clr
                        Case "ide"
                            lblID3.Text = lblText
                            lblID3.ForeColor = clr
                        Case "gra"
                            Me.lbl3.Text = lblText
                            Me.lbl3.ForeColor = clr
                    End Select
                Case 4
                    Select Case stmt
                        Case "all"
                            Me.lbl4.Text = lblText
                            Me.lbl4.ForeColor = clr

                            Me.lblID4.Text = lblText
                            Me.lblID4.ForeColor = clr

                            Me.lblSOC4.Text = lblText
                            Me.lblSOC4.ForeColor = clr

                        Case "soc"
                            lblSOC4.Text = lblText
                            lblSOC4.ForeColor = clr
                        Case "ide"
                            lblID4.Text = lblText
                            lblID4.ForeColor = clr
                        Case "gra"
                            Me.lbl4.Text = lblText
                            Me.lbl4.ForeColor = clr
                    End Select
                Case 5
                    Select Case stmt
                        Case "all"
                            Me.lbl5.Text = lblText
                            Me.lbl5.ForeColor = clr

                            Me.lblID5.Text = lblText
                            Me.lblID5.ForeColor = clr

                            Me.lblSOC5.Text = lblText
                            Me.lblSOC5.ForeColor = clr

                        Case "soc"
                            lblSOC5.Text = lblText
                            lblSOC5.ForeColor = clr
                        Case "ide"
                            lblID5.Text = lblText
                            lblID5.ForeColor = clr
                        Case "gra"
                            Me.lbl5.Text = lblText
                            Me.lbl5.ForeColor = clr
                    End Select
            End Select
        End If
    End Sub

    Private Sub bgwDownloadStatements_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadStatements.RunWorkerCompleted
        Me.CircularProgress1.IsRunning = False
        Me.CircularProgress1.Text = ""
        Me.CircularProgress1.Hide()
        Call Shell("explorer.exe " & ConsolidatedStatementsFolder, AppWinStyle.NormalFocus)
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "MONTHLY PERF CONSOLIDATED"

    Private Sub btnConsolidateMonth_Click(sender As Object, e As EventArgs) Handles btnConsolidateMonth.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        SelectedMonthIndex = Me.cmbMonth.SelectedIndex + 1
        SelectedMonthText = Me.cmbMonth.Text
        SelectedMonthYear = Me.txtMonthlyYear.Text

        ClearLabels()

        Me.lblStmt.Text = "Consolidated Statement - " & SelectedMonthText & " - " & SelectedMonthYear

        Dim ConsolidatedFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-Consolidated Work Done.docx"

        If My.Computer.FileSystem.FileExists(ConsolidatedFileName) Then
            Shell("explorer.exe " & ConsolidatedFileName, AppWinStyle.MaximizedFocus)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.CircularProgress1.ProgressText = "1/" & TotalDistrictCount
        Me.CircularProgress1.IsRunning = True
        Me.CircularProgress1.Show()

        blAllFilesDownloaded = False
        bgwDownloadMonthFiles.RunWorkerAsync(DistrictList)

    End Sub

    Private Function GetFolderID(FolderName As String, ParentFolderID As String)
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name contains '" & FolderName & "' and '" & ParentFolderID & "' in parents"

            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 1 Then
                id = Results.Files(0).Id
            End If

            Return id


        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub bgwDownloadMonthFiles_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadMonthFiles.DoWork
        Try
            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            If InternalFolderID = "" Then
                InternalFolderID = GetFolderID("Internal File Transfer", "root")
            End If

            Dim currentfilenumber As Integer = 0

            Dim List = FISService.Files.List()
            Dim Results As Google.Apis.Drive.v3.Data.FileList
            Dim cnt As Integer = 0

            Dim DownloadedFileCount As Integer = 0


            For i = 0 To 4

                Dim SelectedDistrict = DistrictList(i)
                If SelectedDistrict = "" Then
                    Continue For
                End If

                currentfilenumber = currentfilenumber + 1
                bgwDownloadMonthFiles.ReportProgress(currentfilenumber, currentfilenumber)

                Dim DownloadFileName As String = SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-" & SelectedDistrict & ".docx"


                If My.Computer.FileSystem.FileExists(ConsolidatedPerformanceFolder & "\" & DownloadFileName) Then
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "Already downloaded")
                    DownloadedFileCount += 1
                    Continue For
                End If

                Dim districtfolderid As String = GetFolderID(SelectedDistrict, InternalFolderID)
                If districtfolderid = "" Then
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "No stmt found")
                    Continue For
                End If

                Dim workfolderid As String = GetFolderID("Work Done Statement", districtfolderid)
                If workfolderid = "" Then
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "No stmt found")
                    Continue For
                End If

                Dim fileid As String = ""

                List.Q = "name = 'Monthly Performance Statement - " & SelectedMonthYear & " - " & SelectedMonthIndex.ToString("D2") & ".docx' and '" & workfolderid & "' in parents"
                List.Fields = "files(id)"

                Results = List.Execute
                cnt = Results.Files.Count

                If cnt = 0 Then
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "No stmt found")
                Else

                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "Downloading")
                    fileid = Results.Files(0).Id

                    Dim request = FISService.Files.Get(fileid)
                    request.Fields = "size"
                    Dim file = request.Execute

                    Dim fStream = New System.IO.FileStream(ConsolidatedPerformanceFolder & "\" & DownloadFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                    Dim mStream = New System.IO.MemoryStream

                    Dim m = request.MediaDownloader

                    AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

                    request.DownloadWithStatus(mStream)

                    If dDownloadStatus = DownloadStatus.Completed Then
                        mStream.WriteTo(fStream)
                        bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "Downloaded")
                        DownloadedFileCount += 1
                    End If

                    If dDownloadStatus = DownloadStatus.Failed Then
                        bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "Failed")
                        mStream.WriteTo(fStream)
                    End If

                    fStream.Close()
                    mStream.Close()

                End If
            Next

            If Not DownloadedFileCount = TotalDistrictCount Then
                blAllFilesDownloaded = False
            Else
                blAllFilesDownloaded = True

                Dim TemplateFile As String = strAppUserPath & "\WordTemplates\ConsolidatedPerformance.docx"

                Dim wdApp As Word.Application = New Word.Application
                Dim wdDocs As Word.Documents = wdApp.Documents
                Dim wdDocConsol As Word.Document = wdDocs.Add(TemplateFile)
                Dim wdDocConsolTbl As Word.Table = wdDocConsol.Range.Tables.Item(1)
                Dim wdBooks As Word.Bookmarks = wdDocConsol.Bookmarks

                wdDocConsol.Range.NoProofing = 1

                wdBooks("Range").Range.Text = Range.ToUpper & " RANGE"
                wdBooks("Period").Range.Text = SelectedMonthText.ToUpper & " " & SelectedMonthYear

                currentfilenumber = 0

                For i = 0 To 4

                    Dim SelectedDistrict = DistrictList(i)
                    wdDocConsolTbl.Cell(1, i + 3).Range.Text = SelectedDistrict

                    If SelectedDistrict = "" Then
                        Continue For
                    End If

                    currentfilenumber = currentfilenumber + 1
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, currentfilenumber)
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "Attaching")

                    Dim DistFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-" & SelectedDistrict & ".docx"
                    Dim wdDocDist As Word.Document = wdDocs.Add(DistFileName)
                    Dim wdDocDistTbl As Word.Table = wdDocDist.Range.Tables.Item(1)
                    Dim rc As Integer = wdDocDistTbl.Rows.Count

                    For j = 4 To rc
                        wdDocConsolTbl.Cell(j - 2, i + 3).Range.Text = wdDocDistTbl.Cell(j, 4).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                    wdDocDist.Close()
                    ReleaseObject(wdDocDistTbl)
                    ReleaseObject(wdDocDist)
                    bgwDownloadMonthFiles.ReportProgress(currentfilenumber, "Attached")
                    Threading.Thread.Sleep(250)
                Next

                Dim rcnt As Integer = wdDocConsolTbl.Rows.Count

                For i = 2 To rcnt
                    Dim total As Integer = 0

                    For j = 3 To TotalDistrictCount + 2
                        If i = rcnt Then
                            Dim t = wdDocConsolTbl.Cell(i, j).Range.Text.ToLower
                            t = t.Replace("rs.", "")
                            t = t.Replace("`", "")
                            t = t.Replace("/-", "")
                            total = total + Val(t)
                        Else
                            total = total + Val(wdDocConsolTbl.Cell(i, j).Range.Text)
                        End If
                    Next

                    If i = rcnt Then
                        wdDocConsolTbl.Cell(i, 8).Range.Text = "Rs." & total & "/-"
                    Else
                        wdDocConsolTbl.Cell(i, 8).Range.Text = total
                    End If

                Next


                Dim ConsolidatedFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-Consolidated Work Done.docx"

                If My.Computer.FileSystem.FileExists(ConsolidatedFileName) = False Then
                    wdDocConsol.SaveAs(ConsolidatedFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocConsol.Activate()

                ReleaseObject(wdDocConsolTbl)
                ReleaseObject(wdDocConsol)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)
        On Error Resume Next
        Control.CheckForIllegalCrossThreadCalls = False
        dDownloadStatus = Progress.Status
    End Sub

#End Region


#Region "QUARTERLY PERF CONSOLIDATED"

    Private Sub btnConsolidateQuarter_Click(sender As Object, e As EventArgs) Handles btnConsolidateQuarter.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        SelectedQuarter = Me.txtQuarter.Text
        SelectedQuarterYear = Me.txtQuarterYear.Text

        ClearLabels()
        Me.lblStmt.Text = "Consolidated Statement - Quarter " & SelectedQuarter & " - " & SelectedQuarterYear

        Dim ConsolidatedFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedQuarterYear & "-Q" & SelectedQuarter & "-Consolidated Work Done.docx"

        If My.Computer.FileSystem.FileExists(ConsolidatedFileName) Then
            Shell("explorer.exe " & ConsolidatedFileName, AppWinStyle.MaximizedFocus)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.CircularProgress1.ProgressText = "1/" & TotalDistrictCount
        Me.CircularProgress1.IsRunning = True
        Me.CircularProgress1.Show()


        blAllFilesDownloaded = False
        bgwDownloadQuarterFiles.RunWorkerAsync()

    End Sub

    Private Sub bgwDownloadQuarterFiles_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadQuarterFiles.DoWork
        Try
            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            If InternalFolderID = "" Then
                InternalFolderID = GetFolderID("Internal File Transfer", "root")
            End If

            Dim currentfilenumber As Integer = 0

            Dim List = FISService.Files.List()
            Dim Results As Google.Apis.Drive.v3.Data.FileList
            Dim cnt As Integer = 0

            Dim DownloadedFileCount As Integer = 0

            For i = 0 To 4

                Dim SelectedDistrict = DistrictList(i)
                If SelectedDistrict = "" Then
                    Continue For
                End If

                currentfilenumber = currentfilenumber + 1

                Dim DownloadFileName As String = SelectedQuarterYear & "-Q" & SelectedQuarter & "-" & SelectedDistrict & ".docx"

                bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, currentfilenumber)


                If My.Computer.FileSystem.FileExists(ConsolidatedPerformanceFolder & "\" & DownloadFileName) Then
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "Already downloaded")
                    DownloadedFileCount += 1
                    Continue For
                End If

                Dim districtfolderid As String = GetFolderID(SelectedDistrict, InternalFolderID)
                If districtfolderid = "" Then
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "No stmt found")
                    Continue For
                End If

                Dim workfolderid As String = GetFolderID("Work Done Statement", districtfolderid)
                If workfolderid = "" Then
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "No stmt found")
                    Continue For
                End If

                Dim fileid As String = ""

                List.Q = "name = 'Quarterly Performance Statement - " & SelectedQuarterYear & " - Q" & SelectedQuarter & ".docx' and '" & workfolderid & "' in parents"
                List.Fields = "files(id)"

                Results = List.Execute
                cnt = Results.Files.Count

                If cnt = 0 Then
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "No stmt found")
                Else

                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "Downloading")
                    fileid = Results.Files(0).Id

                    Dim request = FISService.Files.Get(fileid)
                    request.Fields = "size"
                    Dim file = request.Execute

                    Dim fStream = New System.IO.FileStream(ConsolidatedPerformanceFolder & "\" & DownloadFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                    Dim mStream = New System.IO.MemoryStream

                    Dim m = request.MediaDownloader

                    AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

                    request.DownloadWithStatus(mStream)

                    If dDownloadStatus = DownloadStatus.Completed Then
                        mStream.WriteTo(fStream)
                        bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "Downloaded")
                        DownloadedFileCount += 1
                    End If

                    If dDownloadStatus = DownloadStatus.Failed Then
                        bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "Failed")
                        mStream.WriteTo(fStream)
                    End If

                    fStream.Close()
                    mStream.Close()

                End If
            Next

            If Not DownloadedFileCount = TotalDistrictCount Then
                blAllFilesDownloaded = False
            Else
                blAllFilesDownloaded = True

                Dim TemplateFile As String = strAppUserPath & "\WordTemplates\ConsolidatedPerformance.docx"

                Dim wdApp As Word.Application = New Word.Application
                Dim wdDocs As Word.Documents = wdApp.Documents
                Dim wdDocConsol As Word.Document = wdDocs.Add(TemplateFile)
                Dim wdDocConsolTbl As Word.Table = wdDocConsol.Range.Tables.Item(1)
                Dim wdBooks As Word.Bookmarks = wdDocConsol.Bookmarks

                wdDocConsol.Range.NoProofing = 1

                wdBooks("Range").Range.Text = Range.ToUpper & " RANGE"
                wdBooks("Period").Range.Text = "QUARTER " & SelectedQuarter & " OF " & SelectedQuarterYear

                currentfilenumber = 0

                For i = 0 To 4

                    Dim SelectedDistrict = DistrictList(i)
                    wdDocConsolTbl.Cell(1, i + 3).Range.Text = SelectedDistrict

                    If SelectedDistrict = "" Then
                        Continue For
                    End If

                    currentfilenumber = currentfilenumber + 1
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, currentfilenumber)
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "Attaching")

                    Dim DistFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedQuarterYear & "-Q" & SelectedQuarter & "-" & SelectedDistrict & ".docx"
                    Dim wdDocDist As Word.Document = wdDocs.Add(DistFileName)
                    Dim wdDocDistTbl As Word.Table = wdDocDist.Range.Tables.Item(1)
                    Dim rc As Integer = wdDocDistTbl.Rows.Count

                    For j = 4 To rc
                        wdDocConsolTbl.Cell(j - 2, i + 3).Range.Text = wdDocDistTbl.Cell(j, 7).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                    wdDocDist.Close()
                    ReleaseObject(wdDocDistTbl)
                    ReleaseObject(wdDocDist)
                    bgwDownloadQuarterFiles.ReportProgress(currentfilenumber, "Attached")
                    Threading.Thread.Sleep(250)
                Next

                Dim rcnt As Integer = wdDocConsolTbl.Rows.Count

                For i = 2 To rcnt
                    Dim total As Integer = 0

                    For j = 3 To TotalDistrictCount + 2
                        If i = rcnt Then
                            Dim t = wdDocConsolTbl.Cell(i, j).Range.Text.ToLower
                            t = t.Replace("rs.", "")
                            t = t.Replace("`", "")
                            t = t.Replace("/-", "")
                            total = total + Val(t)
                        Else
                            total = total + Val(wdDocConsolTbl.Cell(i, j).Range.Text)
                        End If
                    Next

                    If i = rcnt Then
                        wdDocConsolTbl.Cell(i, 8).Range.Text = "Rs." & total & "/-"
                    Else
                        wdDocConsolTbl.Cell(i, 8).Range.Text = total
                    End If

                Next


                Dim ConsolidatedFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedQuarterYear & "-Q" & SelectedQuarter & "-Consolidated Work Done.docx"

                If My.Computer.FileSystem.FileExists(ConsolidatedFileName) = False Then
                    wdDocConsol.SaveAs(ConsolidatedFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocConsol.Activate()

                ReleaseObject(wdDocConsolTbl)
                ReleaseObject(wdDocConsol)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwDownloadQuarterFiles_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadQuarterFiles.ProgressChanged, bgwDownloadMonthFiles.ProgressChanged, bgwDownloadAnnualFiles.ProgressChanged

        If TypeOf e.UserState Is Integer Then
            CircularProgress1.ProgressText = e.ProgressPercentage & "/" & TotalDistrictCount
        End If

        If TypeOf e.UserState Is String Then
            Dim districtnumber As Integer = e.ProgressPercentage
            Dim status As String = e.UserState.ToString
            Dim clr As Color = Color.Black

            If status = "No stmt found" Then
                clr = Color.Red
            End If

            If status = "Already downloaded" Then
                clr = Color.Brown
            End If

            If status = "Downloading" Then
                clr = Color.Blue
            End If

            If status = "Downloaded" Then
                clr = Color.Green
            End If

            If status = "Failed" Then
                clr = Color.Red
            End If

            If status = "Attaching" Then
                clr = Color.Blue
            End If

            If status = "Attached" Then
                clr = Color.Green
            End If

            Select Case districtnumber
                Case 1
                    Me.lbl1.Text = status
                    Me.lbl1.ForeColor = clr
                Case 2
                    Me.lbl2.Text = status
                    Me.lbl2.ForeColor = clr
                Case 3
                    Me.lbl3.Text = status
                    Me.lbl3.ForeColor = clr
                Case 4
                    Me.lbl4.Text = status
                    Me.lbl4.ForeColor = clr
                Case 5
                    Me.lbl5.Text = status
                    Me.lbl5.ForeColor = clr
            End Select
        End If
    End Sub

    Private Sub bgwDownloadQuarterFiles_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadQuarterFiles.RunWorkerCompleted, bgwDownloadMonthFiles.RunWorkerCompleted, bgwDownloadAnnualFiles.RunWorkerCompleted

        Me.Cursor = Cursors.Default
        Me.CircularProgress1.IsRunning = False
        Me.CircularProgress1.Text = ""
        Me.CircularProgress1.Hide()

        If Not blAllFilesDownloaded Then
            MessageBoxEx.Show("Cannot generate consolidated statement as all files are not downloaded.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

#End Region


#Region "ANNUAL PERF CONSOLIDATED"

    Private Sub btnConsolidateAnnual_Click(sender As Object, e As EventArgs) Handles btnConsolidateAnnual.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        SelectedAnnualYear = Me.txtAnnualYear.Text

        ClearLabels()

        Me.lblStmt.Text = "Consolidated Statement - Year " & SelectedAnnualYear

        Dim ConsolidatedFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedAnnualYear & "-Consolidated Work Done.docx"

        If My.Computer.FileSystem.FileExists(ConsolidatedFileName) Then
            Shell("explorer.exe " & ConsolidatedFileName, AppWinStyle.MaximizedFocus)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.CircularProgress1.ProgressText = "1/" & TotalDistrictCount
        Me.CircularProgress1.IsRunning = True
        Me.CircularProgress1.Show()

        blAllFilesDownloaded = False
        bgwDownloadAnnualFiles.RunWorkerAsync(DistrictList)

    End Sub

    Private Sub bgwDownloadAnnualFiles_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadAnnualFiles.DoWork
        Try
            If blServiceCreated = False Then
                Dim Scopes As String() = {DriveService.Scope.Drive}
                FISAccountServiceCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
                FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})
                blServiceCreated = True
            End If

            If InternalFolderID = "" Then
                InternalFolderID = GetFolderID("Internal File Transfer", "root")
            End If

            Dim currentfilenumber As Integer = 0

            Dim List = FISService.Files.List()
            Dim Results As Google.Apis.Drive.v3.Data.FileList
            Dim cnt As Integer = 0

            Dim DownloadedFileCount As Integer = 0


            For i = 0 To 4

                Dim SelectedDistrict = DistrictList(i)
                If SelectedDistrict = "" Then
                    Continue For
                End If

                currentfilenumber = currentfilenumber + 1
                bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, currentfilenumber)

                Dim DownloadFileName As String = SelectedAnnualYear & "-" & SelectedDistrict & ".docx" '2020-Kozhikode Rural.docx

                If My.Computer.FileSystem.FileExists(ConsolidatedPerformanceFolder & "\" & DownloadFileName) Then
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "Already downloaded")
                    DownloadedFileCount += 1
                    Continue For
                End If

                Dim districtfolderid As String = GetFolderID(SelectedDistrict, InternalFolderID)
                If districtfolderid = "" Then
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "No stmt found")
                    Continue For
                End If

                Dim workfolderid As String = GetFolderID("Work Done Statement", districtfolderid)
                If workfolderid = "" Then
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "No stmt found")
                    Continue For
                End If

                Dim fileid As String = ""

                List.Q = "name = 'Annual Performance Statement - " & SelectedAnnualYear & ".docx' and '" & workfolderid & "' in parents"
                List.Fields = "files(id)"

                Results = List.Execute
                cnt = Results.Files.Count

                If cnt = 0 Then
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "No stmt found")
                Else

                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "Downloading")
                    fileid = Results.Files(0).Id

                    Dim request = FISService.Files.Get(fileid)
                    request.Fields = "size"
                    Dim file = request.Execute

                    Dim fStream = New System.IO.FileStream(ConsolidatedPerformanceFolder & "\" & DownloadFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                    Dim mStream = New System.IO.MemoryStream

                    Dim m = request.MediaDownloader

                    AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

                    request.DownloadWithStatus(mStream)

                    If dDownloadStatus = DownloadStatus.Completed Then
                        mStream.WriteTo(fStream)
                        bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "Downloaded")
                        DownloadedFileCount += 1
                    End If

                    If dDownloadStatus = DownloadStatus.Failed Then
                        bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "Failed")
                        mStream.WriteTo(fStream)
                    End If

                    fStream.Close()
                    mStream.Close()

                End If
            Next

            If Not DownloadedFileCount = TotalDistrictCount Then
                blAllFilesDownloaded = False
            Else
                blAllFilesDownloaded = True

                Dim TemplateFile As String = strAppUserPath & "\WordTemplates\ConsolidatedPerformance.docx"

                Dim wdApp As Word.Application = New Word.Application
                Dim wdDocs As Word.Documents = wdApp.Documents
                Dim wdDocConsol As Word.Document = wdDocs.Add(TemplateFile)
                Dim wdDocConsolTbl As Word.Table = wdDocConsol.Range.Tables.Item(1)
                Dim wdBooks As Word.Bookmarks = wdDocConsol.Bookmarks

                wdDocConsol.Range.NoProofing = 1

                wdBooks("Range").Range.Text = Range.ToUpper & " RANGE"
                wdBooks("Period").Range.Text = "YEAR " & SelectedAnnualYear

                currentfilenumber = 0

                For i = 0 To 4

                    Dim SelectedDistrict = DistrictList(i)
                    wdDocConsolTbl.Cell(1, i + 3).Range.Text = SelectedDistrict

                    If SelectedDistrict = "" Then
                        Continue For
                    End If

                    currentfilenumber = currentfilenumber + 1
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, currentfilenumber)
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "Attaching")

                    Dim DistFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedAnnualYear & "-" & SelectedDistrict & ".docx"
                    Dim wdDocDist As Word.Document = wdDocs.Add(DistFileName)
                    Dim wdDocDistTbl As Word.Table = wdDocDist.Range.Tables.Item(1)
                    Dim rc As Integer = wdDocDistTbl.Rows.Count

                    For j = 2 To rc
                        wdDocConsolTbl.Cell(j, i + 3).Range.Text = wdDocDistTbl.Cell(j, 7).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                    wdDocDist.Close()
                    ReleaseObject(wdDocDistTbl)
                    ReleaseObject(wdDocDist)
                    bgwDownloadAnnualFiles.ReportProgress(currentfilenumber, "Attached")
                    Threading.Thread.Sleep(250)
                Next

                Dim rcnt As Integer = wdDocConsolTbl.Rows.Count

                For i = 2 To rcnt
                    Dim total As Integer = 0

                    For j = 3 To TotalDistrictCount + 2
                        If i = rcnt Then
                            Dim t = wdDocConsolTbl.Cell(i, j).Range.Text.ToLower
                            t = t.Replace("rs.", "")
                            t = t.Replace("`", "")
                            t = t.Replace("/-", "")
                            total = total + Val(t)
                        Else
                            total = total + Val(wdDocConsolTbl.Cell(i, j).Range.Text)
                        End If
                    Next

                    If i = rcnt Then
                        wdDocConsolTbl.Cell(i, 8).Range.Text = "Rs." & total & "/-"
                    Else
                        wdDocConsolTbl.Cell(i, 8).Range.Text = total
                    End If

                Next


                Dim ConsolidatedFileName As String = ConsolidatedPerformanceFolder & "\" & SelectedAnnualYear & "-Consolidated Work Done.docx"

                If My.Computer.FileSystem.FileExists(ConsolidatedFileName) = False Then
                    wdDocConsol.SaveAs(ConsolidatedFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocConsol.Activate()

                ReleaseObject(wdDocConsolTbl)
                ReleaseObject(wdDocConsol)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim ConsolidatedFileName As String = ""

            If Me.lblStmt.Text.StartsWith("Consolidated Statement - Quarter") Then
                SelectedQuarter = Me.txtQuarter.Text
                SelectedQuarterYear = Me.txtQuarterYear.Text
                ConsolidatedFileName = ConsolidatedPerformanceFolder & "\" & SelectedQuarterYear & "-Q" & SelectedQuarter & "-Consolidated Work Done.docx"

            ElseIf Me.lblStmt.Text.StartsWith("Consolidated Statement - Year") Then
                SelectedAnnualYear = Me.txtAnnualYear.Text
                ConsolidatedFileName = ConsolidatedPerformanceFolder & "\" & SelectedAnnualYear & "-Consolidated Work Done.docx"

            ElseIf Me.lblStmt.Text.StartsWith("Consolidated Statement - ") Or Me.lblStmt.Text = "" Then
                SelectedMonthIndex = Me.cmbMonth.SelectedIndex + 1
                SelectedMonthYear = Me.txtMonthlyYear.Text
                ConsolidatedFileName = ConsolidatedPerformanceFolder & "\" & SelectedMonthYear & "-" & SelectedMonthIndex.ToString("D2") & "-Consolidated Work Done.docx"
            ElseIf Me.lblStmt.Text.StartsWith("Download") Then
                Call Shell("explorer.exe " & ConsolidatedStatementsFolder, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If My.Computer.FileSystem.FileExists(ConsolidatedFileName) Then
                Shell("explorer.exe /select," & ConsolidatedFileName, AppWinStyle.NormalFocus)
            Else
                Call Shell("explorer.exe " & ConsolidatedPerformanceFolder, AppWinStyle.NormalFocus)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub


End Class