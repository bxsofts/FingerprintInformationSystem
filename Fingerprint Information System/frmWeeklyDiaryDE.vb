Imports DevComponents.DotNetBar
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Imports Microsoft.Office.Interop

Public Class frmWeeklyDiaryDE
    Dim wdConString As String = ""
    Dim wdOfficerName As String = ""

    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus

    Public uBytesUploaded As Long
    Public uUploadStatus As UploadStatus
    Public dFileSize As Long

    Dim dtWeeklyDiaryFrom As Date
    Dim dtWeeklyDiaryTo As Date
    Dim TemplateFile As String

    Dim WeeklyDiaryFolder As String

    Dim blDGVChanged As Boolean
    Dim blShowUploadStatus As Boolean = True
    Dim TourStartLocation = ""

    Dim RemoteRecordCount As Integer = 0
    Dim LocalRecordCount As Integer = 0
    Dim LastRemoteRecordDate As String = "##/##/####"""

    Private Sub frmWeeklyDiaryDE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Me.Cursor = Cursors.WaitCursor
            Me.BringToFront()
            Me.CenterToScreen()
            Me.cprDataTransfer.Visible = False
            Me.cprDBAvailable.Visible = False
            Me.lblLastWeek.Visible = False

            Me.txtName.Text = ""
            Me.txtOldPassword.Text = ""
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""

            FindRemoteRecordCountAndDate()

            ShowPasswordFields(False)
            Me.btnSaveName.Visible = False
            Me.btnCancelName.Visible = False
            Me.txtOldPassword.UseSystemPasswordChar = True
            Me.txtPassword1.UseSystemPasswordChar = True
            Me.txtPassword2.UseSystemPasswordChar = True

            Me.dtFrom.MonthCalendar.DisplayMonth = Today
            Me.dtTo.MonthCalendar.DisplayMonth = Today

            Me.MonthCalendarAdv1.FirstDayOfWeek = System.DayOfWeek.Sunday
            Dim lastweekdate As Date = Date.Today.AddDays(-7) 'gets day of last week
            Dim dayOfWeek = CInt(lastweekdate.DayOfWeek)
            dtWeeklyDiaryFrom = lastweekdate.AddDays(-1 * dayOfWeek)
            dtWeeklyDiaryTo = lastweekdate.AddDays(6 - dayOfWeek)

            Me.MonthCalendarAdv1.SelectedDate = dtWeeklyDiaryFrom
            Me.MonthCalendarAdv1.DisplayMonth = dtWeeklyDiaryFrom
            MarkHolidays()
            Me.lblSelectedDate.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", TimeFormatCulture)

            Me.dgvWeeklyDiary.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)

            wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & wdDatabase
            Me.dgvOfficeDetails.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)

            ConnectToDatabase()

            CreateTourNoteColumns()
            RemoveNullFromTourNoteColumns()

            Me.WeeklyDiaryTableAdapter1.FillByDateBetween(Me.WeeklyDiaryDataSet1.WeeklyDiary, dtWeeklyDiaryFrom, dtWeeklyDiaryTo)
            Me.PersonalDetailsTableAdapter1.Fill(Me.WeeklyDiaryDataSet1.PersonalDetails)

            If Me.WeeklyDiaryDataSet1.PersonalDetails.Rows.Count = 1 Then
                wdPEN = WeeklyDiaryDataSet1.PersonalDetails(0).PEN.ToString
                Me.lblPEN.Text = wdPEN
                wdOfficerName = WeeklyDiaryDataSet1.PersonalDetails(0).OfficerName.ToString
            End If

            Me.txtName.Text = wdOfficerName
            Me.txtName.Enabled = False

            Me.Text = "Weekly Diary - " & wdOfficerName & " - " & wdPEN
            Me.TitleText = "<b>Weekly Diary - " & wdOfficerName & " - " & wdPEN & "</b>"

            Me.OfficeDetailsTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet1.OfficeDetails)
            Me.OfficeDetailsBindingSource.MoveLast()

            Me.txtUnit.Text = "SDFPB, "
            Me.btnSaveOfficeDetails.Text = "Save"

            Control.CheckForIllegalCrossThreadCalls = False

            FindLastLocalRecordCountAndDate()

            blDGVChanged = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub ConnectToDatabase()
        Try
            If Me.WeeklyDiaryTableAdapter1.Connection.State = ConnectionState.Open Then Me.WeeklyDiaryTableAdapter1.Connection.Close()
            Me.WeeklyDiaryTableAdapter1.Connection.ConnectionString = wdConString
            Me.WeeklyDiaryTableAdapter1.Connection.Open()

            If Me.PersonalDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.PersonalDetailsTableAdapter1.Connection.Close()
            Me.PersonalDetailsTableAdapter1.Connection.ConnectionString = wdConString
            Me.PersonalDetailsTableAdapter1.Connection.Open()

            If Me.OfficeDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.OfficeDetailsTableAdapter1.Connection.Close()
            Me.OfficeDetailsTableAdapter1.Connection.ConnectionString = wdConString
            Me.OfficeDetailsTableAdapter1.Connection.Open()

            If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
            Me.SocRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.SocRegisterTableAdapter1.Connection.Open()

            If Me.CommonSettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.CommonSettingsTableAdapter1.Connection.Close()
            Me.CommonSettingsTableAdapter1.Connection.ConnectionString = sConString
            Me.CommonSettingsTableAdapter1.Connection.Open()

            Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet1.CommonSettings, "TourStartLocation")
            If Me.FingerPrintDataSet1.CommonSettings.Count = 1 Then
                TourStartLocation = Me.FingerPrintDataSet1.CommonSettings(0).SettingsValue.ToString
            Else
                TourStartLocation = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TourStartingLocation", FullDistrictName)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateTourNoteColumns()
        Try
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(wdConString)
            con.Open()

            If DoesFieldExist("WeeklyDiary", "TourFrom", wdConString) = False Then
                Dim cmd = New OleDb.OleDbCommand("ALTER TABLE WeeklyDiary ADD COLUMN TourFrom VARCHAR(255) WITH COMPRESSION", con)
                cmd.ExecuteNonQuery()
            End If

            If DoesFieldExist("WeeklyDiary", "TourTo", wdConString) = False Then
                Dim cmd = New OleDb.OleDbCommand("ALTER TABLE WeeklyDiary ADD COLUMN TourTo VARCHAR(255) WITH COMPRESSION", con)
                cmd.ExecuteNonQuery()
            End If

            If DoesFieldExist("WeeklyDiary", "TourPurpose", wdConString) = False Then
                Dim cmd = New OleDb.OleDbCommand("ALTER TABLE WeeklyDiary ADD COLUMN TourPurpose VARCHAR(255) WITH COMPRESSION", con)
                cmd.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveNullFromTourNoteColumns()
        Try
            Me.WeeklyDiaryTableAdapter1.RemoveNullFromTourFrom("")
            Me.WeeklyDiaryTableAdapter1.RemoveNullFromTourTo("")
            Me.WeeklyDiaryTableAdapter1.RemoveNullFromTourPurpose("")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub MarkHolidays()
        Try
            Dim hdDatabase As String = strAppUserPath & "\WordTemplates\HolidayList.mdb"
            If My.Computer.FileSystem.FileExists(hdDatabase) = False Then
                Exit Sub
            End If

            Me.MonthCalendarAdv1.Colors.DayMarker.TextColor = Color.Red

            Dim hdConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & hdDatabase
            Dim hdTblAdptr As HolidayListDataSetTableAdapters.HolidayListTableAdapter = New HolidayListDataSetTableAdapters.HolidayListTableAdapter

            If hdTblAdptr.Connection.State = ConnectionState.Open Then hdTblAdptr.Connection.Close()
            hdTblAdptr.Connection.ConnectionString = hdConnectionString
            hdTblAdptr.Connection.Open()

            Dim hddataset As HolidayListDataSet = New HolidayListDataSet
            hdTblAdptr.Fill(hddataset.HolidayList)


            Dim hdt(hddataset.HolidayList.Count) As Date

            For i = 0 To hddataset.HolidayList.Count - 1
                hdt(i) = hddataset.HolidayList(i).DateOfHoliday
            Next

            Me.MonthCalendarAdv1.MarkedDates = hdt
            Me.MonthCalendarAdv1.UpdateMarkedDates()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub MonthCalendarAdv1_MonthChanged(sender As Object, e As EventArgs) Handles MonthCalendarAdv1.MonthChanged

        Try
            Dim dt As Date = Today
            Dim m As Integer = Me.MonthCalendarAdv1.DisplayMonth.Month
            Dim y As Integer = Me.MonthCalendarAdv1.DisplayMonth.Year


            For i = 8 To 14
                dt = New Date(y, m, i)
                If dt.Day > 7 And dt.Day < 15 And dt.DayOfWeek = DayOfWeek.Saturday Then '2nd saturday
                    Exit For
                End If
            Next
            Me.MonthCalendarAdv1.GetDay(dt).TextColor = Color.Red
        Catch ex As Exception

        End Try

    End Sub

#Region "LAST RECORD COUNT"


    Private Sub FindLastLocalRecordCountAndDate()
        Try
            Me.lblLastWeek.Visible = False
            LocalRecordCount = Me.WeeklyDiaryTableAdapter1.ScalarQueryCount
            If LocalRecordCount > 0 Then
                Dim lastdate As Date = Me.WeeklyDiaryTableAdapter1.ScalarQueryLastDate
                lastdate = lastdate.AddDays(-6)
                Me.lblLastWeek.Text = "Last generated week: " & lastdate.ToString("dd/MM/yyyy", TimeFormatCulture)
                Me.lblLastWeek.Visible = True
                Me.lblLocalCount.Text = "No. of Local Records: " & LocalRecordCount
                Me.lblLocalLastDate.Text = "Last Local Diary Date: " & lastdate.ToString("dd/MM/yyyy", TimeFormatCulture)
            Else
                Me.lblLocalCount.Text = "No. of Local Records: 0"
                Me.lblLocalLastDate.Text = "Last Local Diary Date: ##/##/####"
            End If
        Catch ex As Exception
            Me.lblLastWeek.Text = "Last generated week "
            Me.lblLastWeek.Visible = False
        End Try
    End Sub

    Private Sub FindRemoteRecordCountAndDate()
        Try
            If InternetAvailable() Then bgwRemoteCount.RunWorkerAsync()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgwRemoteCount_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwRemoteCount.DoWork
        Try
            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim wdFolderID As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwRemoteCount.ReportProgress(2, "0")
            Else
                wdFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & wdFolderID & "' in parents and name = '" & wdPEN & ".mdb'"
            List.Fields = "files(id, description)"
            Results = List.Execute

            Dim remotecount As Integer = 0
            Dim description As String = ""
            Dim lastdate As String = ""
            cnt = Results.Files.Count

            If Results.Files.Count = 0 Then
                bgwRemoteCount.ReportProgress(2, remotecount)
            Else
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, " - ")
                Dim u = SplitText.GetUpperBound(0)

                If u = 0 Then
                    remotecount = Val(SplitText(0))
                    lastdate = "##/##/####"
                End If

                If u > 0 Then
                    remotecount = Val(SplitText(1))
                    lastdate = SplitText(2)
                End If
                bgwRemoteCount.ReportProgress(3, remotecount)
                bgwRemoteCount.ReportProgress(4, lastdate)
            End If
        Catch ex As Exception
            bgwRemoteCount.ReportProgress(2, 0)
        End Try

    End Sub

    Private Sub bgwRemoteCount_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwRemoteCount.ProgressChanged
        If e.ProgressPercentage = 2 Then
            Me.lblRemoteCount.Text = "No. of Remote Records: 0"
            Me.lblRemoteLastDate.Text = "Last Remote Diary Date: ##/##/####"
        End If

        If e.ProgressPercentage = 3 Then
            RemoteRecordCount = e.UserState
            Me.lblRemoteCount.Text = "No. of Remote Records: " & RemoteRecordCount
        End If

        If e.ProgressPercentage = 4 Then
            LastRemoteRecordDate = e.UserState
            Me.lblRemoteLastDate.Text = "Last Remote Diary Date: " & LastRemoteRecordDate
        End If
    End Sub

    Private Sub bgwRemoteCount_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwRemoteCount.RunWorkerCompleted
        Try
            LocalRecordCount = Me.WeeklyDiaryTableAdapter1.ScalarQueryCount
            If RemoteRecordCount > LocalRecordCount Then
                Me.cprDBAvailable.Visible = True
                Dim r = MessageBoxEx.Show("Weekly Diary upto " & LastRemoteRecordDate & " is available in online backup. Press OK to import this database.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If r = Windows.Forms.DialogResult.OK Then
                    cprDataTransfer.IsRunning = True
                    cprDataTransfer.ProgressColor = GetProgressColor()
                    cprDataTransfer.ProgressText = "0"
                    cprDataTransfer.Visible = True
                    Me.RibbonBar1.RecalcLayout()
                    Me.bgwDownload.RunWorkerAsync()
                End If
            Else
                Me.cprDBAvailable.Visible = False
            End If
        Catch ex As Exception

        End Try
        
    End Sub

#End Region


#Region "CHANGE PASSWORD"

    Private Sub ShowPasswordFields(Show As Boolean)
        Me.txtOldPassword.Visible = Show
        Me.lblOldPassword.Visible = Show
        Me.txtPassword1.Visible = Show
        Me.txtPassword2.Visible = Show
        Me.lblPassword1.Visible = Show
        Me.lblPassword2.Visible = Show
        Me.btnSavePassword.Visible = Show
        Me.btnCancelPassword.Visible = Show
    End Sub

    Private Sub lblChangePassword_Click(sender As Object, e As EventArgs) Handles lblChangePassword.Click
        Me.txtOldPassword.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""

        ShowPasswordFields(True)
        Me.txtOldPassword.Focus()
    End Sub

    Private Sub btnSavePassword_Click(sender As Object, e As EventArgs) Handles btnSavePassword.Click

        If Me.txtOldPassword.Text.Trim = "" Then
            MessageBoxEx.Show("Enter current password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.txtOldPassword.Focus()
            Exit Sub
        End If

        If Me.txtPassword1.Text.Trim = "" Then
            MessageBoxEx.Show("Enter new password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.txtPassword1.Focus()
            Exit Sub
        End If

        If Me.txtPassword1.Text.Trim <> Me.txtPassword2.Text.Trim Then
            MessageBoxEx.Show("Passwords do not match.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.txtPassword1.Focus()
            Exit Sub
        End If

        Try


            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
            Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
            Me.AuthenticationTableAdapter1.Connection.Open()

            Dim pwd As String = Me.AuthenticationTableAdapter1.GetPasswordQuery()

            If pwd <> Me.txtOldPassword.Text.Trim Then
                MessageBoxEx.Show("Current Password is incorrect.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtOldPassword.Focus()
                Exit Sub
            End If

            Me.AuthenticationTableAdapter1.UpdateQuery(Me.txtPassword1.Text.Trim, Me.txtOldPassword.Text.Trim)
            MessageBoxEx.Show("Password updated.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ShowPasswordFields(False)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try


    End Sub

    Private Sub btnCancelPassword_Click(sender As Object, e As EventArgs) Handles btnCancelPassword.Click
        ShowPasswordFields(False)
    End Sub

#End Region


#Region "CHANGE NAME"
    Private Sub lblChangeName_Click(sender As Object, e As EventArgs) Handles lblChangeName.Click
        Me.btnSaveName.Visible = True
        Me.btnCancelName.Visible = True
        Me.txtName.Enabled = True

    End Sub

    Private Sub btnSaveName_Click(sender As Object, e As EventArgs) Handles btnSaveName.Click
        Dim newname = Me.txtName.Text.Trim

        If newname = "" Then
            MessageBoxEx.Show("Enter Name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtName.Focus()
            Exit Sub
        End If


        Try
            Me.PersonalDetailsTableAdapter1.UpdateOfficerName(newname, Me.lblPEN.Text)
            wdOfficerName = newname

            Me.btnSaveName.Visible = False
            Me.btnCancelName.Visible = False
            Me.txtName.Enabled = False

            Me.Text = "Weekly Diary - " & wdOfficerName & " - " & wdPEN
            Me.TitleText = "<b>Weekly Diary - " & wdOfficerName & " - " & wdPEN & "</b>"

            MessageBoxEx.Show("Name updated.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnCancelName_Click(sender As Object, e As EventArgs) Handles btnCancelName.Click
        Me.btnSaveName.Visible = False
        Me.btnCancelName.Visible = False
        Me.txtName.Enabled = False
    End Sub

#End Region


#Region "NEW DATA"
    Private Sub btnNewEntry_Click(sender As Object, e As EventArgs) Handles btnNewEntry.Click

        If Me.SuperTabControl1.SelectedTab Is tabOD Then
            InitializeODFields()
        End If

        If Me.SuperTabControl1.SelectedTab Is tabWD Then
            MessageBoxEx.Show("Select Weekly Diary start date and press 'Generate' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

#End Region


#Region "EDIT DATA"
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If Me.SuperTabControl1.SelectedTab Is tabOD Then
                If Me.dgvOfficeDetails.RowCount = 0 Then
                    MessageBoxEx.Show("No records in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Me.dgvOfficeDetails.SelectedRows.Count = 0 Then
                    MessageBoxEx.Show("No records selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Me.txtUnit.Text = Me.dgvOfficeDetails.SelectedRows(0).Cells(0).Value
                Me.dtFrom.ValueObject = Me.dgvOfficeDetails.SelectedRows(0).Cells(1).Value
                Me.dtTo.ValueObject = Me.dgvOfficeDetails.SelectedRows(0).Cells(2).Value
                Me.txtDesignation.Text = Me.dgvOfficeDetails.SelectedRows(0).Cells(3).Value
                Me.txtODRemarks.Text = Me.dgvOfficeDetails.SelectedRows(0).Cells(4).Value
                Me.txtUnit.Focus()
                Me.btnSaveOfficeDetails.Text = "Update"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

        If Me.SuperTabControl1.SelectedTab Is tabWD Then
            If Me.dgvWeeklyDiary.RowCount = 0 Then
                MessageBoxEx.Show("No records in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.dgvWeeklyDiary.SelectedRows.Count = 0 Then
                MessageBoxEx.Show("No records selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            MessageBoxEx.Show("To edit data, double click required cell, modify the content and save the changes.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
#End Region


#Region "SAVE OFFICE DETAILS"

    Private Sub InitializeODFields()
        Me.txtUnit.Text = "SDFPB, "
        Me.dtFrom.Text = vbNullString
        Me.dtTo.Text = vbNullString
        Me.txtDesignation.Text = ""
        Me.txtODRemarks.Text = ""
        Me.btnSaveOfficeDetails.Text = "Save"
        Me.txtUnit.Focus()
    End Sub
    Private Sub btnSaveOfficeDetails_Click(sender As Object, e As EventArgs) Handles btnSaveOfficeDetails.Click

        If Me.txtUnit.Text.Trim = "" Then
            MessageBoxEx.Show("Please enter 'Unit'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtUnit.Focus()
            Exit Sub
        End If

        If Me.dtFrom.Text.Trim = "" Then
            MessageBoxEx.Show("Please enter 'From Date'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Exit Sub
        End If

        If Me.txtDesignation.Text.Trim = "" Then
            MessageBoxEx.Show("Please enter 'Designation'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtDesignation.Focus()
            Exit Sub
        End If

        Try
            If Me.btnSaveOfficeDetails.Text = "Save" Then
                Dim dgvr As WeeklyDiaryDataSet.OfficeDetailsRow = Me.WeeklyDiaryDataSet1.OfficeDetails.NewRow
                With dgvr
                    .Unit = Me.txtUnit.Text.Trim
                    .FromDate = Me.dtFrom.ValueObject
                    .ToDate = Me.dtTo.ValueObject
                    .Designation = Me.txtDesignation.Text.Trim
                    .Remarks = Me.txtODRemarks.Text.Trim
                End With
                Me.WeeklyDiaryDataSet1.OfficeDetails.AddOfficeDetailsRow(dgvr)
                Me.OfficeDetailsTableAdapter1.InsertQuery(Me.txtUnit.Text.Trim, Me.dtFrom.ValueObject, Me.dtTo.ValueObject, Me.txtDesignation.Text.Trim, Me.txtODRemarks.Text.Trim)

                '  Me.OfficeDetailsTableAdapter1.Update(Me.WeeklyDiaryDataSet1.OfficeDetails)
                Me.OfficeDetailsBindingSource.MoveLast()

                InitializeODFields()
                ShowDesktopAlert("New Record entered.")
            End If

            If Me.btnSaveOfficeDetails.Text = "Update" Then
                Dim dgvr As WeeklyDiaryDataSet.OfficeDetailsRow = Me.WeeklyDiaryDataSet1.OfficeDetails.Rows(Me.dgvOfficeDetails.SelectedRows(0).Index)
                With dgvr
                    .Unit = Me.txtUnit.Text.Trim
                    .FromDate = Me.dtFrom.ValueObject
                    .ToDate = Me.dtTo.ValueObject
                    .Designation = Me.txtDesignation.Text.Trim
                    .Remarks = Me.txtODRemarks.Text.Trim
                End With
                '   Me.OfficeDetailsTableAdapter1.Update(Me.WeeklyDiaryDataSet1.OfficeDetails)
                Me.OfficeDetailsTableAdapter1.UpdateQuery(Me.txtUnit.Text.Trim, Me.dtFrom.ValueObject, Me.dtTo.ValueObject, Me.txtDesignation.Text.Trim, Me.txtODRemarks.Text.Trim, Me.dgvOfficeDetails.SelectedRows(0).Cells(5).Value)

                InitializeODFields()
                ShowDesktopAlert("Selected Record updated.")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


#End Region


#Region "DELETE DATA"

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try


            If Me.SuperTabControl1.SelectedTab Is tabOD Then
                If Me.dgvOfficeDetails.RowCount = 0 Then
                    MessageBoxEx.Show("No records in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Me.dgvOfficeDetails.SelectedRows.Count = 0 Then
                    MessageBoxEx.Show("No records selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected record?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then
                    Dim dgvr As WeeklyDiaryDataSet.OfficeDetailsRow = Me.WeeklyDiaryDataSet1.OfficeDetails.Rows(Me.dgvOfficeDetails.SelectedRows(0).Index)
                    Me.OfficeDetailsTableAdapter1.DeleteQuery(Me.dgvOfficeDetails.SelectedRows(0).Cells(5).Value)
                    dgvr.Delete()
                    If Me.dgvOfficeDetails.SelectedRows.Count = 0 And Me.dgvOfficeDetails.RowCount <> 0 Then
                        Me.dgvOfficeDetails.Rows(Me.dgvOfficeDetails.RowCount - 1).Selected = True
                    End If

                    ShowDesktopAlert("Selected record deleted.")
                End If
            End If

            If Me.SuperTabControl1.SelectedTab Is tabWD Then

                If Me.dgvWeeklyDiary.RowCount = 0 Then
                    MessageBoxEx.Show("No records in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the weekly diary for the selected week starting from " & Me.lblSelectedDate.Text & " ?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then
                    blDGVChanged = False
                    LoadSelectedWeekDiary()

                    If Me.dgvWeeklyDiary.RowCount = 0 Then
                        MessageBoxEx.Show("No records found for the selected week.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    For i = 0 To 6
                        dgvWeeklyDiary.Rows.RemoveAt(0)
                    Next
                    Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
                    LoadSelectedWeekDiary()
                    FindLastLocalRecordCountAndDate()
                    ShowDesktopAlert("Weekly Diary for the selected week deleted.")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "BACKUP"

    Private Sub btnOnlineBackup_Click(sender As Object, e As EventArgs) Handles btnOnlineBackup.Click
        Try

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                ShowFileTransferInProgressMessage()
                Exit Sub
            End If

            If Not InternetAvailable() Then
                MessageBoxEx.Show("Cannot connect to server. Please check your Internet connection.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim r = MessageBoxEx.Show("Remote database will be replaced with local database. Do you want to take backup?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If r <> Windows.Forms.DialogResult.Yes Then Exit Sub

            Me.Cursor = Cursors.WaitCursor


            Dim LocalRecordCount As Integer = Me.WeeklyDiaryTableAdapter1.ScalarQueryCount()

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim wdFolderID As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                wdFolderID = ""
            Else
                wdFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & wdFolderID & "' in parents and name = '" & wdPEN & ".mdb'"
            List.Fields = "files(id, description)"
            Results = List.Execute

            Dim remotecount As Integer = 0
            Dim description As String = ""

            If Results.Files.Count > 0 Then
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, " - ")
                Dim u = SplitText.GetUpperBound(0)

                If u = 0 Then
                    remotecount = Val(SplitText(0))
                End If

                If u > 0 Then
                    remotecount = Val(SplitText(1))
                End If

            End If


            If remotecount > LocalRecordCount Then
                MessageBoxEx.Show("Remote database file has more records (" & remotecount & ") than local database (" & LocalRecordCount & "). Cannot upload database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim WDDS = New WeeklyDiaryDataSet

            Me.WeeklyDiaryTableAdapter1.FillByLastDate(WDDS.WeeklyDiary)
            Dim lastdate As String = ""
            If WDDS.WeeklyDiary.Count > 0 Then
                Dim dtlast As Date = WDDS.WeeklyDiary(0).DiaryDate
                dtlast = dtlast.AddDays(-6)
                lastdate = dtlast.ToString("dd/MM/yyyy", TimeFormatCulture)
            End If

            Dim fDescription As String = wdOfficerName & " - " & LocalRecordCount
            If lastdate <> "" Then
                fDescription = wdOfficerName & " - " & LocalRecordCount & " - " & lastdate
            End If

            cprDataTransfer.IsRunning = True
            cprDataTransfer.ProgressColor = GetProgressColor()
            cprDataTransfer.Visible = True
            cprDataTransfer.ProgressText = "0"
            Me.RibbonBar1.RecalcLayout()
            blShowUploadStatus = True
            Me.bgwUpload.RunWorkerAsync(fDescription)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub bgwUpload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpload.DoWork
        Try
            blUploadIsProgressing = True
            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim wdFolderID As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                wdFolderID = ""
            Else
                wdFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & wdFolderID & "' in parents and name = '" & wdPEN & ".mdb'"
            List.Fields = "files(id)"
            Results = List.Execute

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = My.Computer.FileSystem.GetFileInfo(wdDatabase).Name
            body.MimeType = "database/mdb"
            body.Description = e.Argument

            Dim tmpFileName As String = My.Computer.FileSystem.GetTempFileName
            My.Computer.FileSystem.CopyFile(wdDatabase, tmpFileName, True)

            dFileSize = FileLen(tmpFileName)

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(tmpFileName)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            If Results.Files.Count = 0 Then
                Dim parentlist As New List(Of String)
                parentlist.Add(wdFolderID)
                body.Parents = parentlist
                Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize

                AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                UploadRequest.Fields = "id, name, mimeType, size, description"
                UploadRequest.Upload()
            Else
                Dim RemoteFileID As String = Results.Files(0).Id
                Dim UpdateRequest As FilesResource.UpdateMediaUpload = FISService.Files.Update(body, RemoteFileID, Stream, body.MimeType)
                UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize

                AddHandler UpdateRequest.ProgressChanged, AddressOf Update_ProgressChanged

                UpdateRequest.Fields = "id, name, mimeType, description"
                UpdateRequest.Upload()
            End If

            If uUploadStatus = UploadStatus.Completed Then
                bgwUpload.ReportProgress(100, e.Argument)
            End If

            Stream.Close()

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Upload_ProgressChanged(Progress As IUploadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
        Dim percent = CInt((uBytesUploaded / dFileSize) * 100)
        bgwUpload.ReportProgress(percent, uBytesUploaded)
    End Sub

    Private Sub Update_ProgressChanged(Progress As IUploadProgress)
        Control.CheckForIllegalCrossThreadCalls = False
        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
        Dim percent = CInt((uBytesUploaded / dFileSize) * 100)
        bgwUpload.ReportProgress(percent)
    End Sub
    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        cprDataTransfer.ProgressText = e.ProgressPercentage
        If e.ProgressPercentage = 100 And TypeOf e.UserState Is String Then
            Dim description = e.UserState.ToString
            Dim SplitText() = Strings.Split(description, " - ")
            Dim u = SplitText.GetUpperBound(0)
            If u > 0 Then
                Me.lblRemoteCount.Text = "No. of Remote Records: " & Val(SplitText(1))
                Me.lblRemoteLastDate.Text = "Last Remote Diary Date: " & SplitText(2)
            End If
        End If
    End Sub

    Private Sub bgwUploadFile_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted

        cprDataTransfer.Visible = False
        blUploadIsProgressing = False

        If uUploadStatus = UploadStatus.Completed And blShowUploadStatus Then
            MessageBoxEx.Show("File uploaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If uUploadStatus = UploadStatus.Failed And blShowUploadStatus Then
            MessageBoxEx.Show("File upload failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Me.Cursor = Cursors.Default
    End Sub
    Private Sub TakeAutoBackup() Handles Me.FormClosed

        Try
            Dim localcount As Integer = Me.WeeklyDiaryTableAdapter1.ScalarQueryCount()

            Dim WDDS = New WeeklyDiaryDataSet

            Me.WeeklyDiaryTableAdapter1.FillByLastDate(WDDS.WeeklyDiary)
            Dim lastdate As String = ""
            If WDDS.WeeklyDiary.Count > 0 Then
                Dim dtlast As Date = WDDS.WeeklyDiary(0).DiaryDate
                dtlast = dtlast.AddDays(-6)
                lastdate = dtlast.ToString("dd/MM/yyyy", TimeFormatCulture)
            End If

            Dim fDescription As String = wdOfficerName & " - " & localcount
            If lastdate <> "" Then
                fDescription = wdOfficerName & " - " & localcount & " - " & lastdate
            End If

            If Me.WeeklyDiaryTableAdapter1.Connection.State = ConnectionState.Open Then Me.WeeklyDiaryTableAdapter1.Connection.Close()

            If Me.PersonalDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.PersonalDetailsTableAdapter1.Connection.Close()

            If Me.OfficeDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.OfficeDetailsTableAdapter1.Connection.Close()
            ' GC.Collect()
            bgwAutoUpload.RunWorkerAsync(fDescription)

        Catch ex As Exception
            ' ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwAutoUpload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAutoUpload.DoWork
        Try

            If blDownloadIsProgressing Or blUploadIsProgressing Or blListIsLoading Then
                Exit Sub
            End If

            If Not InternetAvailable() Then
                Exit Sub
            End If

            Dim fDescription As String = e.Argument
            Dim SplitfDescription() = Strings.Split(fDescription, " - ")
            Dim localcount As Integer = SplitfDescription(1)

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim wdFolderID As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                wdFolderID = ""
            Else
                wdFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & wdFolderID & "' in parents and name = '" & wdPEN & ".mdb'"
            List.Fields = "files(id, description)"
            Results = List.Execute

            Dim remotecount As Integer = 0
            Dim description As String = ""

            If Results.Files.Count > 0 Then
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, " - ")
                Dim u = SplitText.GetUpperBound(0)

                If u = 0 Then
                    remotecount = Val(SplitText(0))
                End If

                If u > 0 Then
                    remotecount = Val(SplitText(1))
                End If

            End If


            If remotecount >= localcount Then
                Exit Sub
            End If

            blShowUploadStatus = False
            Me.bgwUpload.RunWorkerAsync(fDescription)

        Catch ex As Exception
            '  ShowErrorMessage(ex)
        End Try
    End Sub


#End Region


#Region "RESTORE"

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Try

            If Not InternetAvailable() Then
                MessageBoxEx.Show("Cannot connect to server. Please check your Internet connection.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim r = MessageBoxEx.Show("This will overwrite the existing database. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If r <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Dim localcount As Integer = Me.WeeklyDiaryTableAdapter1.ScalarQueryCount()

            Me.Cursor = Cursors.WaitCursor

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim wdFolderID As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                wdFolderID = ""
            Else
                wdFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & wdFolderID & "' in parents and name = '" & wdPEN & ".mdb'"
            List.Fields = "files(id, description)"
            Results = List.Execute

            Dim remotecount As Integer = 0
            Dim description As String = ""
            If Results.Files.Count > 0 Then
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, " - ")
                Dim u = SplitText.GetUpperBound(0)

                If u = 0 Then
                    remotecount = Val(SplitText(0))
                End If

                If u > 0 Then
                    remotecount = Val(SplitText(1))
                End If
            End If

            If remotecount < localcount Then
                MessageBoxEx.Show("Local database has more records (" & localcount & ") than remote database (" & remotecount & "). Cannot replace database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            cprDataTransfer.IsRunning = True
            cprDataTransfer.ProgressColor = GetProgressColor()
            cprDataTransfer.ProgressText = "0"
            cprDataTransfer.Visible = True
            Me.RibbonBar1.RecalcLayout()
            Me.bgwDownload.RunWorkerAsync()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownload.DoWork
        Try
            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim wdFileID As String = ""

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})


            Dim parentid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..WeeklyDiary'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwDownload.ReportProgress(100, "Weekly Diary folder not found")
                Exit Sub
            Else
                parentid = Results.Files(0).Id
            End If


            List.Q = "name = '" & wdPEN & ".mdb' and trashed = false and '" & parentid & "' in parents"
            List.Fields = "files(name, id)"

            Results = List.Execute

            If Results.Files.Count > 0 Then
                wdFileID = Results.Files(0).Id
                Dim request = FISService.Files.Get(wdFileID)
                request.Fields = "size"
                Dim file = request.Execute

                dFileSize = file.Size

                Dim tempfile As String = My.Computer.FileSystem.GetTempFileName & ".mdb"

                Dim fStream = New System.IO.FileStream(tempfile, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
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

                My.Computer.FileSystem.CopyFile(tempfile, SuggestedLocation & "\Weekly Diary\" & wdPEN & ".mdb", True)

            Else
                bgwDownload.ReportProgress(100, "File not found")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        Dim percent = CInt((dBytesDownloaded / dFileSize) * 100)
        bgwDownload.ReportProgress(percent)
    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownload.ProgressChanged
        cprDataTransfer.ProgressText = e.ProgressPercentage


        If e.UserState = "Weekly Diary folder not found" Then
            MessageBoxEx.Show("Weekly Diary folder not found in remote server. Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        If e.UserState = "File not found" Then
            MessageBoxEx.Show("Weekly Diary file not found in remote server.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownload.RunWorkerCompleted
        On Error Resume Next
        Me.Cursor = Cursors.Default

        cprDataTransfer.Visible = False

        If dDownloadStatus = DownloadStatus.Completed Then
            Me.cprDBAvailable.Visible = False
            ConnectToDatabase()
            CreateTourNoteColumns()
            RemoveNullFromTourNoteColumns()

            Me.WeeklyDiaryTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet1.WeeklyDiary)
            Me.WeeklyDiaryBindingSource.MoveLast()

            Me.OfficeDetailsTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet1.OfficeDetails)
            Me.OfficeDetailsBindingSource.MoveLast()

            Me.txtName.Enabled = True
            Me.PersonalDetailsTableAdapter1.Fill(Me.WeeklyDiaryDataSet1.PersonalDetails)

            If Me.WeeklyDiaryDataSet1.PersonalDetails.Rows.Count = 1 Then
                wdOfficerName = WeeklyDiaryDataSet1.PersonalDetails(0).OfficerName.ToString
            End If

            Me.txtName.Text = wdOfficerName
            Me.txtName.Enabled = False

            Me.Text = "Weekly Diary - " & wdOfficerName & " - " & wdPEN
            Me.TitleText = "<b>Weekly Diary - " & wdOfficerName & " - " & wdPEN & "</b>"
            FindLastLocalRecordCountAndDate()
            MessageBoxEx.Show("Weekly Diary file restored successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If dDownloadStatus = DownloadStatus.Failed Then
            MessageBoxEx.Show("Weekly Diary file download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
#End Region


#Region "GENERATE WEEKLY DIARY"

    Private Sub LoadSelectedWeekDiary() Handles MonthCalendarAdv1.ItemClick
        Try

            If blDGVChanged Then
                Dim reply As DialogResult = MessageBoxEx.Show("There are unsaved changes in Weekly Diary table data. Do you want to save the changes?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If reply = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If reply = Windows.Forms.DialogResult.Yes Then
                    Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
                End If
            End If

            blDGVChanged = False

            Me.Cursor = Cursors.WaitCursor

            dtWeeklyDiaryFrom = Me.MonthCalendarAdv1.SelectedDate.AddDays(-Me.MonthCalendarAdv1.SelectedDate.DayOfWeek)
            dtWeeklyDiaryTo = dtWeeklyDiaryFrom.AddDays(6)

            Me.lblSelectedDate.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", TimeFormatCulture)

            Me.WeeklyDiaryTableAdapter1.FillByDateBetween(Me.WeeklyDiaryDataSet1.WeeklyDiary, dtWeeklyDiaryFrom, dtWeeklyDiaryTo)
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub btnGenerateWD_Click(sender As Object, e As EventArgs) Handles btnGenerateWD.Click
        Try
            If blDGVChanged Then
                Dim reply As DialogResult = MessageBoxEx.Show("There are unsaved changes in Weekly Diary table data. Do you want to save the changes?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If reply = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If reply = Windows.Forms.DialogResult.Yes Then
                    Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
                End If
            End If

            blDGVChanged = False

            dtWeeklyDiaryFrom = Me.MonthCalendarAdv1.SelectedDate.AddDays(-Me.MonthCalendarAdv1.SelectedDate.DayOfWeek)
            dtWeeklyDiaryTo = dtWeeklyDiaryFrom.AddDays(6)

            If dtWeeklyDiaryFrom > Today Then
                MessageBoxEx.Show("The week you selected has not started. Cannot generate data.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If dtWeeklyDiaryTo > Today Then
                MessageBoxEx.Show("The week you selected has not completed. Cannot generate data.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor

            Me.WeeklyDiaryTableAdapter1.FillByDateBetween(Me.WeeklyDiaryDataSet1.WeeklyDiary, dtWeeklyDiaryFrom, dtWeeklyDiaryTo)

            If Me.WeeklyDiaryDataSet1.WeeklyDiary.Count > 0 Then
                ShowDesktopAlert("Weekly Diary loaded from Database.")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            For i = 1 To 7
                Dim dtWD = dtWeeklyDiaryFrom.AddDays(i - 1)
                Me.SocRegisterTableAdapter1.FillByInspectingOfficer(Me.FingerPrintDataSet1.SOCRegister, "%" & wdOfficerName & "%", dtWD)

                Dim cnt As Integer = Me.FingerPrintDataSet1.SOCRegister.Count
                Dim WorkDone As String = ""
                Dim TourTo As String = ""
                Dim TourPurpose As String = ""
                Dim TourFrom As String = ""

                Dim officer As String = ""
                Dim inspected As String = ""
                Dim ps As String = ""
                Dim crno As String = ""
                Dim po As String = ""

                If cnt = 0 Then
                    If IsHoliday(dtWD) Then
                        WorkDone = "Availed Holiday" & IIf(strHoliday <> "", " (" & strHoliday.Trim & ")", "")
                    Else
                        WorkDone = "Attended office duty"
                    End If
                End If

                If cnt = 1 Then
                    officer = Me.FingerPrintDataSet1.SOCRegister(0).InvestigatingOfficer
                    If officer.Contains(vbCrLf) Then
                        inspected = "Supervised the inspection of SOC in Cr.No. "
                    Else
                        inspected = "Inspected SOC in Cr.No. "
                    End If
                    crno = Me.FingerPrintDataSet1.SOCRegister(0).CrimeNumber
                    ps = Me.FingerPrintDataSet1.SOCRegister(0).PoliceStation
                    WorkDone = inspected & crno & " of " & ps & " P.S"
                    TourFrom = TourStartLocation
                    TourTo = Me.FingerPrintDataSet1.SOCRegister(0).PlaceOfOccurrence
                    TourPurpose = "SOC inspection in Cr.No. " & crno & " of " & ps & " P.S"
                End If

                If cnt > 1 Then

                    officer = Me.FingerPrintDataSet1.SOCRegister(0).InvestigatingOfficer
                    If officer.Contains(vbCrLf) Then
                        inspected = "Supervised the inspection of SOC in "
                    Else
                        inspected = "Inspected SOC in "
                    End If

                    Dim details As String = ""

                    For j = 0 To cnt - 1
                        If j <> cnt - 1 Then
                            details = details & "Cr.No " & Me.FingerPrintDataSet1.SOCRegister(j).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(j).PoliceStation & " P.S; "
                        Else
                            details = details.Remove(details.Length - 2)
                            details = details & " and Cr.No " & Me.FingerPrintDataSet1.SOCRegister(j).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(j).PoliceStation & " P.S"
                        End If

                    Next
                    WorkDone = inspected & details
                    TourFrom = TourStartLocation
                    TourTo = Me.FingerPrintDataSet1.SOCRegister(0).PlaceOfOccurrence
                    TourPurpose = "SOC inspection in " & details
                End If

                Dim dgvr As WeeklyDiaryDataSet.WeeklyDiaryRow = Me.WeeklyDiaryDataSet1.WeeklyDiary.NewRow
                With dgvr
                    .DiaryDate = dtWD
                    .WorkDone = WorkDone
                    .Remarks = ""
                    .TourFrom = TourFrom
                    .TourTo = TourTo
                    .TourPurpose = TourPurpose
                End With
                Me.WeeklyDiaryDataSet1.WeeklyDiary.AddWeeklyDiaryRow(dgvr)
            Next
            Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
            Me.WeeklyDiaryTableAdapter1.FillByDateBetween(Me.WeeklyDiaryDataSet1.WeeklyDiary, dtWeeklyDiaryFrom, dtWeeklyDiaryTo)
            Me.WeeklyDiaryBindingSource.MoveFirst()
            blDGVChanged = False
            FindLastLocalRecordCountAndDate()
            ShowDesktopAlert("Weekly Diary generated.")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnSaveWD_Click(sender As Object, e As EventArgs) Handles btnSaveWD.Click
        Try
            Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
            blDGVChanged = False
            ShowDesktopAlert("Weekly Diary Records saved.")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub dgvWeeklyDiary_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvWeeklyDiary.CellFormatting
        On Error Resume Next
        If Not blDGVChanged Then
            e.CellStyle.BackColor = Color.White
            e.CellStyle.ForeColor = Color.Black
            e.CellStyle.SelectionForeColor = Color.Black
        End If
    End Sub

    Private Sub dgvWeeklyDiary_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvWeeklyDiary.CellValueChanged
        On Error Resume Next
        blDGVChanged = True
        Me.dgvWeeklyDiary.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Yellow
        Me.dgvWeeklyDiary.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
        Me.dgvWeeklyDiary.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.SelectionForeColor = Color.Red
    End Sub

    Private Sub frmWeeklyDiaryDE_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            If blDGVChanged Then
                Dim reply As DialogResult = MessageBoxEx.Show("There are unsaved changes in Weekly Diary table data. Do you want to save the changes?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If reply = Windows.Forms.DialogResult.Cancel Then e.Cancel = True
                If reply = Windows.Forms.DialogResult.Yes Then
                    Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
                    blDGVChanged = False
                End If
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub btnPrintWD_Click(sender As Object, e As EventArgs) Handles btnPrintWD.Click
        Try
            If Me.dgvWeeklyDiary.RowCount > 7 Then
                LoadSelectedWeekDiary()
            End If

            If Me.dgvWeeklyDiary.RowCount = 0 Then
                MessageBoxEx.Show("Weekly Diary not yet generated for the selected week.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If blDGVChanged Then
                Dim reply As DialogResult = MessageBoxEx.Show("There are unsaved changes in Weekly Diary table data. Do you want to save the changes before printing?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If reply = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If reply = Windows.Forms.DialogResult.Yes Then
                    Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
                End If
            End If

            blDGVChanged = False

            TemplateFile = strAppUserPath & "\WordTemplates\WeeklyDiary.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            ShowPleaseWaitForm()

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)

            wdDoc.Range.NoProofing = 1

            Dim designation As String = "Tester Inspector"
            Dim unit As String = ShortOfficeName & ", " & FullDistrictName

            Dim rowcount As Integer = Me.dgvOfficeDetails.Rows.Count

            If rowcount > 0 Then
                If Me.dgvOfficeDetails.Rows(rowcount - 1).Cells(3).Value Is DBNull.Value Then
                    designation = "Tester Inspector"
                Else
                    designation = Me.dgvOfficeDetails.Rows(rowcount - 1).Cells(3).Value
                End If

                If Me.dgvOfficeDetails.Rows(rowcount - 1).Cells(0).Value Is DBNull.Value Then
                    unit = "SDFPB, " & FullDistrictName
                Else
                    unit = Me.dgvOfficeDetails.Rows(rowcount - 1).Cells(0).Value
                End If
            End If

            dtWeeklyDiaryFrom = Me.MonthCalendarAdv1.SelectedDate.AddDays(-Me.MonthCalendarAdv1.SelectedDate.DayOfWeek)
            dtWeeklyDiaryTo = Me.dgvWeeklyDiary.Rows(6).Cells(1).Value

            Dim header As String = "Weekly Diary of " & wdOfficerName & ", " & designation & ", " & unit & " for the period from " & dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", TimeFormatCulture) & " to " & dtWeeklyDiaryTo.ToString("dd/MM/yyyy", TimeFormatCulture)

            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

            wdBooks("header").Range.Text = header

            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            For i = 2 To 8
                Dim dt As Date = Me.dgvWeeklyDiary.Rows(i - 2).Cells(1).Value
                wdTbl.Cell(i, 1).Range.Text = dt.ToString("dd/MM/yyyy", TimeFormatCulture) & vbNewLine & Format(dt, "dddd")
                wdTbl.Cell(i, 2).Range.Text = Me.dgvWeeklyDiary.Rows(i - 2).Cells(2).Value
                wdTbl.Cell(i, 3).Range.Text = Me.dgvWeeklyDiary.Rows(i - 2).Cells(3).Value
            Next

            ClosePleaseWaitForm()
            Me.Cursor = Cursors.Default

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            '  If My.Computer.FileSystem.FileExists(sFileName) = False Then
            '    wdDoc.SaveAs(sFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            ' End If

            ReleaseObject(wdTbl)
            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnCL_Click(sender As Object, e As EventArgs) Handles btnCL.Click
        Try
            TemplateFile = strAppUserPath & "\WordTemplates\WeeklyDiaryCL.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            ShowPleaseWaitForm()

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            wdDoc.Range.NoProofing = 1
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

            dtWeeklyDiaryFrom = Me.MonthCalendarAdv1.SelectedDate.AddDays(-Me.MonthCalendarAdv1.SelectedDate.DayOfWeek)
            dtWeeklyDiaryTo = dtWeeklyDiaryFrom.AddDays(6)

            wdBooks("FileNo").Range.Text = "No. " & PdlWeeklyDiary & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName
            wdBooks("OfficeName1").Range.Text = FullOfficeName
            wdBooks("District1").Range.Text = FullDistrictName
            wdBooks("Date1").Range.Text = Today.ToString("dd/MM/yyyy", TimeFormatCulture)

            wdBooks("Name1").Range.Text = wdOfficerName
            wdBooks("OfficeName2").Range.Text = FullOfficeName
            wdBooks("District2").Range.Text = FullDistrictName

            If Me.dgvWeeklyDiary.Rows.Count = 7 Then
                dtWeeklyDiaryFrom = Me.dgvWeeklyDiary.Rows(0).Cells(1).Value
                dtWeeklyDiaryTo = Me.dgvWeeklyDiary.Rows(6).Cells(1).Value
            End If


            wdBooks("DateFrom").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", TimeFormatCulture)
            wdBooks("DateTo").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy", TimeFormatCulture)


            If blUseTIinLetter Then
                wdBooks("Name2").Range.Text = wdOfficerName
                wdBooks("Designation").Range.Text = "Tester Inspector"
                wdBooks("OfficeName3").Range.Text = FullOfficeName
                wdBooks("District3").Range.Text = FullDistrictName
            Else
                wdBooks("Name2").Range.Text = ""
                wdBooks("Designation").Range.Text = ""
                wdBooks("OfficeName3").Range.Text = ""
                wdBooks("District3").Range.Text = ""
            End If

            ClosePleaseWaitForm()
            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


    Private Sub SuperTabControl1_SelectedTabChanged(sender As Object, e As SuperTabStripSelectedTabChangedEventArgs) Handles SuperTabControl1.SelectedTabChanged
        If SuperTabControl1.SelectedTabIndex = 1 Then
            Me.txtUnit.Focus()
        End If
    End Sub

    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvOfficeDetails.CellPainting, dgvWeeklyDiary.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 10, FontStyle.Bold)
        sf.LineAlignment = StringAlignment.Center
        Using b As SolidBrush = New SolidBrush(Me.ForeColor)
            If e.ColumnIndex < 0 AndAlso e.RowIndex < 0 Then
                e.Graphics.DrawString("Sl.No", f, b, e.CellBounds, sf)
                e.Handled = True
            End If

            If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
                e.Graphics.DrawString((e.RowIndex + 1).ToString, f, b, e.CellBounds, sf)
                e.Handled = True
            End If
        End Using

    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        Try
            If blDGVChanged Then
                Dim reply As DialogResult = MessageBoxEx.Show("There are unsaved changes in Weekly Diary table data. Do you want to save the changes?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If reply = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If reply = Windows.Forms.DialogResult.Yes Then
                    Me.WeeklyDiaryTableAdapter1.Update(Me.WeeklyDiaryDataSet1)
                End If
            End If

            blDGVChanged = False

            Me.Cursor = Cursors.WaitCursor
            If Me.SuperTabControl1.SelectedTab Is tabOD Then
                Me.OfficeDetailsTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet1.OfficeDetails)
                Me.OfficeDetailsBindingSource.MoveLast()
                ShowDesktopAlert("Data reloaded in Office Details table.")
            End If

            If Me.SuperTabControl1.SelectedTab Is tabWD Then
                Me.WeeklyDiaryTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet1.WeeklyDiary)
                Me.WeeklyDiaryBindingSource.MoveLast()
                ShowDesktopAlert("Data reloaded in Weekly Diary table.")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

        Me.Cursor = Cursors.Default
    End Sub


    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            WeeklyDiaryFolder = My.Computer.FileSystem.GetFileInfo(wdDatabase).DirectoryName

            If FileIO.FileSystem.FileExists(wdDatabase) Then
                Call Shell("explorer.exe /select," & wdDatabase, AppWinStyle.NormalFocus)
                Exit Sub
            End If

            If Not FileIO.FileSystem.DirectoryExists(WeeklyDiaryFolder) Then
                FileIO.FileSystem.CreateDirectory(WeeklyDiaryFolder)
            End If

            Call Shell("explorer.exe " & WeeklyDiaryFolder, AppWinStyle.NormalFocus)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnOpenDatabase_Click(sender As Object, e As EventArgs) Handles btnOpenDatabase.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FileIO.FileSystem.FileExists(wdDatabase) Then
            Shell("explorer.exe " & wdDatabase, AppWinStyle.MaximizedFocus)
        Else
            MessageBoxEx.Show("The database file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnPrintAll_Click(sender As Object, e As EventArgs) Handles btnPrintAll.Click
        Me.Cursor = Cursors.WaitCursor
        frmWeeklyDiaryView.Show()
        Me.Cursor = Cursors.Default
    End Sub


End Class
