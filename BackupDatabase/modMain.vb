Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Module Sub_Main
    Dim strAppName As String = "Fingerprint Information System"
    Dim strBackupSettingsPath As String = "HKEY_CURRENT_USER\Software\BXSofts\Fingerprint Information System\BackupSettings"
    Dim strGeneralSettingsPath As String = "HKEY_CURRENT_USER\Software\BXSofts\Fingerprint Information System\General Settings"
    Dim JsonPath As String = ""
    Dim FISService As DriveService = New DriveService

    Dim UserBackupFolderName As String = ""
    Dim MasterBackupFolderID As String = ""
    Dim UserBackupFolderID As String = ""
    Dim FileOwner As String = ""
    Dim uUploadStatus As UploadStatus
    Dim AutoBackupPeriod As Integer = 15
    Dim BackupDescription As String = ""
    Dim strDatabaseFile As String = ""


    Sub Main()
        Try
            If Not InternetAvailable() Then Exit Sub

            strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", "")
            If strDatabaseFile = "" Then Exit Sub

            JsonPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\Fingerprint Information System\FISServiceAccount.json"

            If Not FileIO.FileSystem.FileExists(JsonPath) Then Exit Sub

            UserBackupFolderName = My.Computer.Registry.GetValue(strBackupSettingsPath, "FullDistrictName", "")
            FileOwner = My.Computer.Registry.GetValue(strBackupSettingsPath, "FileOwner", "")
            If UserBackupFolderName = "" Then Exit Sub
            If FileOwner = "" Then Exit Sub

            BackupDescription = My.Computer.Registry.GetValue(strBackupSettingsPath, "BackupDescription", "")

            If BackupDescription = "" Then Exit Sub

            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)

            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            UpdateOnlineDatabase()

            Dim blAutoBackup As Boolean = CBool(My.Computer.Registry.GetValue(strBackupSettingsPath, "AutoBackup", 1))
            If Not blAutoBackup Then Exit Sub

            AutoBackupPeriod = CInt(My.Computer.Registry.GetValue(strBackupSettingsPath, "AutoBackupTime", 15))
            If AutoBackupPeriod = 0 Then Exit Sub

            TakePeriodicOnlineBackup()

            System.Threading.Thread.Sleep(5000)
            TakePeriodicLocalBackup()

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

    Private Sub GetMasterBackupFolderID(FISService As DriveService)
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'FIS Backup'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                MasterBackupFolderID = ""
            Else
                MasterBackupFolderID = Results.Files(0).Id
            End If


        Catch ex As Exception

            MasterBackupFolderID = ""
        End Try
    End Sub

    Private Sub CreateUserBackupFolder(FISService As DriveService, BackupFolderName As String)
        Try

            Dim parentlist As New List(Of String)
            parentlist.Add(MasterBackupFolderID)

            Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
            NewDirectory.Name = BackupFolderName
            NewDirectory.Parents = parentlist
            NewDirectory.MimeType = "application/vnd.google-apps.folder"
            NewDirectory.Description = FileOwner '
            Dim request As FilesResource.CreateRequest = FISService.Files.Create(NewDirectory)
            NewDirectory = request.Execute()

            UserBackupFolderID = NewDirectory.Id
        Catch ex As Exception
            UserBackupFolderID = ""
        End Try

    End Sub

    Private Sub RenameUserBackupFolderName(UserBackupFolderID As String)
        Try
            Dim request As New Google.Apis.Drive.v3.Data.File
            request.Name = UserBackupFolderName ' FullDistrictName
            request.Description = FileOwner
            FISService.Files.Update(request, UserBackupFolderID).Execute()
        Catch ex As Exception

        End Try
    End Sub


#Region "UPDATE ONLINE DATABASE"

    Private Sub UpdateOnlineDatabase()

        Try

            Dim List = FISService.Files.List()

            GetMasterBackupFolderID(FISService)

            If MasterBackupFolderID = "" Then Exit Sub

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserBackupFolderName & "' and '" & MasterBackupFolderID & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute
            Dim cnt = Results.Files.Count

            If cnt = 0 Then
                List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & FileOwner & "' and '" & MasterBackupFolderID & "' in parents"
                List.Fields = "files(id)"
                Results = List.Execute
                cnt = Results.Files.Count

                If cnt = 0 Then
                    CreateUserBackupFolder(FISService, UserBackupFolderName)
                Else
                    UserBackupFolderID = Results.Files(0).Id
                    RenameUserBackupFolderName(UserBackupFolderID)
                End If

            Else
                UserBackupFolderID = Results.Files(0).Id
            End If

            If UserBackupFolderID = "" Then Exit Sub

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


            Dim strlastlocalmodified As String = My.Computer.Registry.GetValue(strBackupSettingsPath, "LastModifiedDate", "")
            If strlastlocalmodified = "" Then Exit Sub

            Dim dtlastlocalmodified As Date = DateTime.ParseExact(strlastlocalmodified, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

            Dim LocalSOCRecordCount As Integer = CInt(My.Computer.Registry.GetValue(strBackupSettingsPath, "LocalSOCRecordCount", 0))

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

            System.Threading.Thread.Sleep(5000)

            Dim BackupFileName As String = "FingerPrintDB.mdb"

            Dim body As New Google.Apis.Drive.v3.Data.File()
            body.Name = BackupFileName
            body.Description = BackupDescription

            body.MimeType = "database/mdb"

            Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(strDatabaseFile)
            Dim Stream As New System.IO.MemoryStream(ByteArray)
            Dim BackupTime As Date
            Dim file As Google.Apis.Drive.v3.Data.File

            If Results.Files.Count = 0 Then
                Dim parentlist As New List(Of String)
                parentlist.Add(UserBackupFolderID)
                body.Parents = parentlist
                Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UploadRequest.ProgressChanged, AddressOf DBUpload_ProgressChanged
                UploadRequest.Fields = "id, modifiedTime"
                UploadRequest.Upload()
                file = UploadRequest.ResponseBody
                BackupTime = file.ModifiedTime
            Else
                Dim RemoteFileID As String = Results.Files(0).Id
                Dim UpdateRequest As FilesResource.UpdateMediaUpload = FISService.Files.Update(body, RemoteFileID, Stream, body.MimeType)
                UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UpdateRequest.ProgressChanged, AddressOf DBUpdate_ProgressChanged
                UpdateRequest.Fields = "id, modifiedTime"
                UpdateRequest.Upload()
                file = UpdateRequest.ResponseBody
                BackupTime = file.ModifiedTime
            End If
            Stream.Close()

            Dim sBackupTime As String = BackupTime.ToString("dd-MM-yyyy HH:mm:ss")

            If uUploadStatus = UploadStatus.Completed Then
                My.Computer.Registry.SetValue(strBackupSettingsPath, "LastOnlineUpdate", sBackupTime & "; Success", Microsoft.Win32.RegistryValueKind.String)
            End If

            If uUploadStatus = UploadStatus.Failed Then
                My.Computer.Registry.SetValue(strBackupSettingsPath, "LastOnlineUpdate", sBackupTime & "; Failed", Microsoft.Win32.RegistryValueKind.String)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DBUpload_ProgressChanged(Progress As IUploadProgress)
        On Error Resume Next
        uUploadStatus = Progress.Status
    End Sub

    Private Sub DBUpdate_ProgressChanged(Progress As IUploadProgress)
        On Error Resume Next
        uUploadStatus = Progress.Status
    End Sub

#End Region


#Region "PERIODIC ONLINE DATABASE BACKUP"

    Private Sub TakePeriodicOnlineBackup()

        Try
            If MasterBackupFolderID = "" Then Exit Sub
            If UserBackupFolderID = "" Then Exit Sub

            Dim List = FISService.Files.List()
            List.Q = "mimeType = 'database/mdb' and '" & UserBackupFolderID & "' in parents and name contains 'FingerprintBackup'"

            List.Fields = "files(id, modifiedTime)"
            List.OrderBy = "createdTime desc"
            Dim Results = List.Execute

            Dim blTakeBackup As Boolean = False

            If Results.Files.Count = 0 Then blTakeBackup = True

            Dim lastbackupdate As Date = Now
            If Results.Files.Count > 0 Then
                lastbackupdate = Results.Files(0).ModifiedTime
            End If


            Dim dt As Date = lastbackupdate.AddDays(AutoBackupPeriod)
            Dim BackupTime As Date = Now

            If Now.Date >= dt.Date Or blTakeBackup Then

                System.Threading.Thread.Sleep(5000)

                Dim d As String = Strings.Format(BackupTime, "yyyy-MM-dd HH-mm-ss")
                Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"

                Dim body As New Google.Apis.Drive.v3.Data.File()
                body.Name = BackupFileName
                body.Description = BackupDescription
                body.MimeType = "database/mdb"

                Dim parentlist As New List(Of String)
                parentlist.Add(UserBackupFolderID)
                body.Parents = parentlist

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(strDatabaseFile)
                Dim Stream As New System.IO.MemoryStream(ByteArray)
                Dim UploadRequest As FilesResource.CreateMediaUpload = FISService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                UploadRequest.Fields = "id, modifiedTime"
                UploadRequest.Upload()
                Dim file As Google.Apis.Drive.v3.Data.File = UploadRequest.ResponseBody
                BackupTime = file.ModifiedTime
                Stream.Close()
            Else
                Exit Sub
            End If

            Dim sBackupTime As String = BackupTime.ToString("dd-MM-yyyy HH:mm:ss")

            If uUploadStatus = UploadStatus.Completed Then
                My.Computer.Registry.SetValue(strBackupSettingsPath, "LastPeriodicOnlineBackup", sBackupTime & "; Success", Microsoft.Win32.RegistryValueKind.String)
            End If

            If uUploadStatus = UploadStatus.Failed Then
                My.Computer.Registry.SetValue(strBackupSettingsPath, "LastPeriodicOnlineBackup", sBackupTime & "; Failed", Microsoft.Win32.RegistryValueKind.String)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Upload_ProgressChanged(Progress As IUploadProgress)
        On Error Resume Next
        uUploadStatus = Progress.Status
    End Sub

#End Region


#Region "PERIODIC LOCAL DATABASE BACKUP"
    Private Sub TakePeriodicLocalBackup()
        Try
            Dim blTakeBackup As Boolean = False

            Dim lastbackupdate As Date = Now

            Dim strlastbackupdate As String = My.Computer.Registry.GetValue(strBackupSettingsPath, "LastPeriodicLocalBackup", "")

            If strlastbackupdate = "" Then
                blTakeBackup = True
            Else
                lastbackupdate = DateTime.ParseExact(strlastbackupdate, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            End If

            Dim dt As Date = lastbackupdate.AddDays(AutoBackupPeriod)

            If Now >= dt Or blTakeBackup Then

                Dim Destination As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", "")

                If Destination = "" Then Exit Sub

                If My.Computer.FileSystem.DirectoryExists(Destination) = False Then
                    My.Computer.FileSystem.CreateDirectory(Destination)
                End If

                If Strings.Right(Destination, 1) <> "\" Then Destination = Destination & "\"
                Dim backuptime As Date = Now
                Dim d As String = Strings.Format(backuptime, "yyyy-MM-dd HH-mm-ss")
                Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"

                Destination = Destination & BackupFileName
                My.Computer.FileSystem.CopyFile(strDatabaseFile, Destination, True)
                My.Computer.Registry.SetValue(strBackupSettingsPath, "LastPeriodicLocalBackup", backuptime.ToString("dd-MM-yyyy HH:mm:ss"), Microsoft.Win32.RegistryValueKind.String)

            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region


End Module
