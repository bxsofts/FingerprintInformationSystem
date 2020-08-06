
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
    Dim uSelectedFile As String = ""
    Dim blServiceCreated As Boolean = False
    Dim blCheckBoxes As Boolean = False

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

        StmtFileName = StmtFolder & "\Grave Crime Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        chkGrave.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkGrave.Enabled = chkGrave.Checked

        StmtFileName = StmtFolder & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
        chkID.Checked = My.Computer.FileSystem.FileExists(StmtFileName)
        chkID.Enabled = chkID.Checked

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

        Dim SelectedItemText As String = Me.ListViewEx1.SelectedItems(0).Text
        Dim SelectedItemID As String = Me.ListViewEx1.SelectedItems(0).SubItems(1).Text
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "RangeDistrict", SelectedItemText, Microsoft.Win32.RegistryValueKind.String)
    End Sub

End Class