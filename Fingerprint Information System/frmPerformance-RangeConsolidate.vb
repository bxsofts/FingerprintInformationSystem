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


Public Class frmPerformance_RangeConsolidate

    Dim Range As String = ""

    Dim FISService As DriveService = New DriveService
    Dim FISAccountServiceCredential As GoogleCredential

    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus

    Dim blServiceCreated As Boolean = False

    Dim SelectedDistrict As String = ""
    Dim SelectedDistrictID As String = ""
    Dim SelectedMonth As Integer
    Dim SelectedYear As String = ""
    Dim SelectedQuarter As String = ""
    Dim SelectedQuarterYear As String = ""
    Dim TotalFileCount As Integer = 0
    Dim UserFolderID As String = ""

    Private Sub frmPerformance_RangeConsolidate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor

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
                Case "Ernakulam"
                    Me.lblDistrict1.Text = "Alappuzha"
                    Me.lblDistrict2.Text = "Kottayam"
                    Me.lblDistrict3.Text = "Idukki"
                    Me.lblDistrict4.Text = "Kochi City"
                    Me.lblDistrict5.Text = "Ernakulam Rural"
                Case "Thrissur"
                    Me.lblDistrict1.Text = "Thrissur City"
                    Me.lblDistrict2.Text = "Thrissur Rural"
                    Me.lblDistrict3.Text = "Palakkad"
                    Me.lblDistrict4.Text = "Malappuram"
                    Me.lblDistrict5.Text = ""
                Case "Kannur"
                    Me.lblDistrict1.Text = "Kozhikode City"
                    Me.lblDistrict2.Text = "Kozhikode Rural"
                    Me.lblDistrict3.Text = "Wayanad"
                    Me.lblDistrict4.Text = "Kannur"
                    Me.lblDistrict5.Text = "Kasaragod"
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



End Class