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

Module Sub_Main

    Dim strOnlineBackupSettingsPath As String = "HKEY_CURRENT_USER\Software\BXSofts\Fingerprint Information System\OnlineBackupSettings"
    Sub Main()
        Try
            Dim blUpdateOnlineDB As Boolean = CBool(My.Computer.Registry.GetValue(strOnlineBackupSettingsPath, "UpdateOnlineDB", 1))
            If blUpdateOnlineDB Then
                UpdateOnlineDatabase()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function InternetAvailable() As Boolean

        If My.Computer.Network.IsAvailable Then
            Try
                Dim IPHost As Net.IPHostEntry = Net.Dns.GetHostEntry("www.google.com")
                Return True
            Catch
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Private Function GetMasterBackupFolderID(FISService As DriveService) As String
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'FIS Backup'"
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

    Private Sub UpdateOnlineDatabase()

        Try
            If Not InternetAvailable() Then
                Exit Sub
            End If

            Dim JsonPath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\Fingerprint Information System\FISServiceAccount.json"

            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'exit 
                Exit Sub
            End If

          
            Dim UserBackupFolderName As String = My.Computer.Registry.GetValue(strOnlineBackupSettingsPath, "FullDistrictName", "")
            If UserBackupFolderName = "" Then Exit Sub

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = "Fingerprint Information System"})

            Dim List = FISService.Files.List()
            Dim MasterBackupFolderID As String = GetMasterBackupFolderID(FISService)
            If MasterBackupFolderID = "" Then Exit Sub

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserBackupFolderName & "' and '" & MasterBackupFolderID & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute
            Dim UserBackupFolderID As String = ""
            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                UserBackupFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & UserBackupFolderID & "' in parents and name = 'FingerPrintDB.mdb'"

            List.Fields = "files(id, description)"
            Results = List.Execute

            Dim RemoteSOCRecordCount As Integer = 0
            Dim description As String = ""
            Dim dtlastremotemodified As Date

            If Results.Files.Count > 0 Then
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, "; ")
                RemoteSOCRecordCount = Val(SplitText(4))
                Dim strlastremotemodified As String = SplitText(1)
                dtlastremotemodified = DateTime.ParseExact(strlastremotemodified, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            End If


            Dim strlastlocalmodified As String = My.Computer.Registry.GetValue(strOnlineBackupSettingsPath, "LastModifiedDate", "")
            If strlastlocalmodified = "" Then Exit Sub

            Dim dtlastlocalmodified As Date = DateTime.ParseExact(strlastlocalmodified, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

            Dim LocalSOCRecordCount As Integer = CInt(My.Computer.Registry.GetValue(strOnlineBackupSettingsPath, "LocalSOCRecordCount", 0))

            If LocalSOCRecordCount = 0 Then Exit Sub

            If LocalSOCRecordCount < RemoteSOCRecordCount Then Exit Sub

            If LocalSOCRecordCount = RemoteSOCRecordCount Then

                Dim ldt = dtlastlocalmodified.Date
                Dim rdt = dtlastremotemodified.Date
                Dim lhr = dtlastlocalmodified.Hour
                Dim rhr = dtlastremotemodified.Hour
                Dim lmt = dtlastlocalmodified.Minute
                Dim rmt = dtlastremotemodified.Minute
                Dim ls = dtlastlocalmodified.Second
                Dim rs = dtlastremotemodified.Second

                If ldt < rdt Then
                    Exit Sub
                End If

                If ldt = rdt Then
                    If lhr < rhr Then
                        Exit Sub
                    End If
                    If lhr = rhr Then
                        If lmt < rmt Then
                            Exit Sub
                        End If
                        If lmt = rmt Then
                            If ls <= rs Then
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            ' bgwUpdateOnlineDatabase.ReportProgress(0, True)

            Dim BackupTime As Date = Now
            Dim sBackupTime = Strings.Format(BackupTime, "dd-MM-yyyy HH:mm:ss")
            Dim BackupFileName As String = "FingerPrintDB.mdb"
            Dim BackupDescription As String = My.Computer.Registry.GetValue(strOnlineBackupSettingsPath, "BackupDescription", "")
            If BackupDescription = "" Then Exit Sub

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = BackupFileName
            body.Description = BackupDescription

            body.MimeType = "database/mdb"
            Dim strDatabaseFile As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BXSofts\Fingerprint Information System\General Settings", "DatabaseFile", "")
            If strDatabaseFile = "" Then Exit Sub

            Dim tmpFileName As String = My.Computer.FileSystem.GetTempFileName
            My.Computer.FileSystem.CopyFile(strDatabaseFile, tmpFileName, True)

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(tmpFileName)
            Dim Stream As New System.IO.MemoryStream(ByteArray)

            If Results.Files.Count = 0 Then
                Dim parentlist As New List(Of String)
                parentlist.Add(UserBackupFolderID)
                body.Parents = parentlist
                Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                '  AddHandler UploadRequest.ProgressChanged, AddressOf DBUpload_ProgressChanged
                UploadRequest.Fields = "id"
                UploadRequest.Upload()
            Else
                Dim RemoteFileID As String = Results.Files(0).Id
                Dim UpdateRequest As FilesResource.UpdateMediaUpload = FISService.Files.Update(body, RemoteFileID, Stream, body.MimeType)
                UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                '  AddHandler UpdateRequest.ProgressChanged, AddressOf DBUpdate_ProgressChanged
                UpdateRequest.Fields = "id"
                UpdateRequest.Upload()
            End If
            Stream.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module
