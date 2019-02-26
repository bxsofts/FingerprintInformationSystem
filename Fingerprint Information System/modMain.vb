
Imports System.Threading 'to create a mutex which will ensure that only one application is running
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Imports System.Security.Cryptography
Imports DevComponents.DotNetBar.Controls

Module modMain
    Public strAppName As String = "Fingerprint Information System"
    Public strRegistrySettingsPath As String = "HKEY_CURRENT_USER\Software\BXSofts\Fingerprint Information System"
    Public strGeneralSettingsPath As String = strRegistrySettingsPath & "\General Settings"
    Public strAppUserPath As String = FileIO.SpecialDirectories.MyDocuments & "\BXSofts\Fingerprint Information System"
    Public strAppPath As String = My.Application.Info.DirectoryPath
    Public sDatabaseFile As String = vbNullString
    Public sConString As String = vbNullString
    Public strBackupFile As String = ""
    Public boolRestored As Boolean = False
    Public m_BaseColorSelected As Boolean = False 'style color
    Public m_BaseColor As System.Drawing.Color
    Public m_ColorTint As System.Drawing.Color
    Public objMutex As Mutex 'mutex object
    Public FullOfficeName As String = "Single Digit Fingerprint Bureau"
    Public ShortOfficeName As String = "SDFPB"
    Public FullDistrictName As String = "Idukki"
    Public ShortDistrictName As String = "IDK"
    Public TI As String = ""
    Public FPE1 As String = ""
    Public FPE2 As String = ""
    Public FPE3 As String = ""
    Public FPS As String = ""
    Public strPhotographer As String = ""

    Public PdlAttendance As String = ""
    Public PdlIndividualPerformance As String = ""
    Public PdlRBWarrant As String = ""
    Public PdlSOCDAStatement As String = ""
    Public PdlTABill As String = ""
    Public PdlFPAttestation As String = ""
    Public PdlGraveCrime As String = ""
    Public PdlVigilanceCase As String = ""
    Public PdlWeeklyDiary As String = ""

    Public SuggestedLocation = SuggestLocation()
    Public CPImageImportLocation As String = SuggestedLocation & "\Chance Prints"
    Public FPImageImportLocation As String = SuggestedLocation & "\Scanned FP Slips"

    Public boolSettingsWizardCancelled As Boolean = False


    Public boolCurrentSOC As Boolean = False
    Public boolCurrentDA As Boolean = False
    Public boolCurrentFPA As Boolean = False
    Public boolCurrentCD As Boolean = False
    Public boolCurrentAC As Boolean = False
    Public boolCurrentID As Boolean = False
    Public boolFacingSheetWithoutGistIsVisible As Boolean = False
    Public boolFacingSheetWithGistIsVisible As Boolean = False
    Public boolCancelImport As Boolean = False
    Public boolSaveSOCReport As Boolean = False
    Public boolCancelSOCReport As Boolean = True
    Public ReportSentTo As String = ""
    Public ReportSentDate As Date = Today
    Public ReportNature As String = ""
    Public boolRSOCButtonClicked As Boolean = False

    Public dtAnnualStatisticsFrom As Date
    Public dtAnnualStatisticsTo As Date
    Public boolShowWizard As Boolean = False
    Public boolUseTIinLetter As Boolean = True
    Public BackupDateFormatString As String = "yyyy-MM-dd HH-mm-ss"
    Public culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture

    Public AdminPasswordFolderName As String = "..D4FvarcFNt/t7C/rcJltjylRSXzzthOS"
    Public UserPasswordFolderName As String = "..FAmDGDDYUnMfj45TryHY0B4ot4lMVHgcixHjrAdWfUA="
    Public SuperAdminPass As String = ""
    Public LocalAdminPass As String = ""

    Public LocalAdmin As Boolean = False
    Public SuperAdmin As Boolean = False
    Public LocalUser As Boolean = True
    Public FileOwner As String = ""

    Public blDownloadIsProgressing As Boolean
    Public blUploadIsProgressing As Boolean
    Public blListIsLoading As Boolean

    Public frmNewPleaseWaitForm As New frmPleaseWait
    Public LatestSOCNumber As String = ""
    Public LatestSOCDI As String = ""

    Public blUserAuthenticated As Boolean = False
    Public blAuthenticatePasswordChange As Boolean = False
    Public blChangeAndUpdatePassword As Boolean = False
    Public oAuthUserID As String = "user"

    Public CredentialFilePath As String = ""

    Public JsonPath As String = ""

    Public Sub CreateFolder(ByVal FolderName As String)
        If My.Computer.FileSystem.DirectoryExists(FolderName) = False Then 'if destination directory not exists
            My.Computer.FileSystem.CreateDirectory(FolderName) 'then create one!
        End If

    End Sub

    Public Function SuggestLocation()
        Dim x = System.IO.DriveInfo.GetDrives()
        Dim suggestedpath = strAppUserPath
        Dim sys = System.Environment.SystemDirectory
        Dim sysdrv = My.Computer.FileSystem.GetDriveInfo(sys).Name

        For i = 0 To x.Count - 1
            If x(i).DriveType = IO.DriveType.Fixed Then
                If x(i).Name <> sysdrv Then
                    suggestedpath = x(i).Name & strAppName
                    Exit For
                End If
            End If
        Next

        Return suggestedpath
    End Function

    Public Sub InitializeCulture()
        Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
        Dim dateTimeInfo As System.Globalization.DateTimeFormatInfo = New System.Globalization.DateTimeFormatInfo()
        dateTimeInfo.DateSeparator = "/"
        dateTimeInfo.LongDatePattern = "dd-MM-yyyy"
        dateTimeInfo.ShortDatePattern = "dd-MM-yy"
        dateTimeInfo.LongTimePattern = "hh:mm:ss tt"
        dateTimeInfo.ShortTimePattern = "hh:mm tt"

        cultureInfo.DateTimeFormat = dateTimeInfo
        'Application.CurrentCulture = cultureInfo
        Thread.CurrentThread.CurrentCulture = cultureInfo
        ' Thread.CurrentThread.CurrentUICulture = cultureInfo
    End Sub

    Public Sub ShowErrorMessage(ex As Exception)
        DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub


    Public Function InternetAvailable1() As Boolean

        Dim objUrl As New System.Uri("http://www.google.com/")
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        objWebReq.Proxy = Nothing
        Dim objResp As System.Net.WebResponse
        Try
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' objResp.Close()
            objWebReq = Nothing
            Return False
        End Try

    End Function

    Public Function InternetAvailable() As Boolean

        Dim InternetFunction As String = "0"
        If My.Computer.Registry.GetValue(strGeneralSettingsPath, "InternetFunction", Nothing) Is Nothing Then
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "InternetFunction", "0", Microsoft.Win32.RegistryValueKind.String)
        Else
            InternetFunction = My.Computer.Registry.GetValue(strGeneralSettingsPath, "InternetFunction", "0")
        End If


        If InternetFunction = "1" Then
            Return InternetAvailable1()
        End If

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
    Public Function ConvertToDate(strDate As String)

        Dim ConvertedDate As Date = Date.ParseExact(strDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Return ConvertedDate
    End Function


    Public Function IsHoliday(dt As Date) As Boolean
        IsHoliday = False
        Dim d = dt.Day
        Dim y = dt.Year

        If (dt.DayOfWeek = DayOfWeek.Sunday) Or (dt.Day > 7 And dt.Day < 15 And dt.DayOfWeek = DayOfWeek.Saturday) Or (dt = New Date(y, 1, 26)) Or (dt = New Date(y, 5, 1)) Or (dt = New Date(y, 8, 15)) Or (dt = New Date(y, 10, 2)) Or (dt = New Date(y, 12, 25)) Then
            IsHoliday = True
        End If
    End Function

    Public Function TIName() As String
        Return TI.Replace(", TI", "")
    End Function

    Public Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Function GetRandomColor1()

        Randomize()
        Dim rnd = New Random()

        Dim randomColour As String = "-16748352" 'blue

        Try
            randomColour = Color.FromArgb(&HFF000000 Or rnd.Next(&HFFFFFF + 1)).ToArgb
        Catch ex As Exception
            randomColour = "-16748352" 'blue
        End Try
        Return randomColour
    End Function
    Public Function GetRandomColor() As String
        Try

            Dim colorList(25) As String
            colorList(0) = "-4194304" 'dark red
            colorList(1) = "-65536" 'medium red
            colorList(2) = "-16384" 'orange
            colorList(3) = "-8421505" 'dark green
            colorList(4) = "-7155632" 'green
            colorList(5) = "-16732080" 'green
            colorList(6) = "-16731920" 'blue
            colorList(7) = "-16748352" 'blue
            colorList(8) = "-16768928" 'dark blue
            colorList(9) = "-9424736" 'violet
            colorList(10) = "-551354" 'orange

            colorList(11) = Color.Violet.ToArgb.ToString
            colorList(12) = Color.Indigo.ToArgb.ToString
            colorList(13) = Color.Blue.ToArgb.ToString
            colorList(14) = Color.Green.ToArgb.ToString
            colorList(15) = Color.Yellow.ToArgb.ToString
            colorList(16) = Color.Orange.ToArgb.ToString
            colorList(17) = Color.Black.ToArgb.ToString
            colorList(18) = Color.Red.ToArgb.ToString

            colorList(19) = Color.Pink.ToArgb.ToString
            colorList(20) = Color.Purple.ToArgb.ToString
            colorList(21) = Color.Magenta.ToArgb.ToString
            colorList(22) = Color.Maroon.ToArgb.ToString
            colorList(23) = Color.Honeydew.ToArgb.ToString
            colorList(24) = Color.IndianRed.ToArgb.ToString

            Randomize()
            Dim rnd = New Random()
            Dim randomColour = colorList(rnd.Next(0, colorList.Count - 1))

            Return randomColour

        Catch ex As Exception
            Return Color.Blue.ToArgb.ToString
        End Try
    End Function

    Public Function GetProgressColor() As System.Drawing.Color
        Dim BaseColor As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BaseColor", "")
        Dim cBaseColor As System.Drawing.Color

        If BaseColor = "" Or BaseColor = "0" Then
            cBaseColor = StyleManager.MetroColorGeneratorParameters.BaseColor
        Else
            cBaseColor = CType(Color.FromArgb(BaseColor), Color)
        End If
        Return cBaseColor
    End Function

    Public Function GenerateDateWithoutDay()
        On Error Resume Next

        Dim dt = Today
        Dim m As String = Month(dt)
        If m < 10 Then m = "0" & m
        Dim y As String = Year(dt)
        Dim d As String = m & "/" & y
        Return d
    End Function

    Public Function FileInUse(ByVal sFile As String) As Boolean
        Dim thisFileInUse As Boolean = False
        If System.IO.File.Exists(sFile) Then
            Try
                Using f As New IO.FileStream(sFile, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None)
                    ' thisFileInUse = False
                End Using
            Catch
                thisFileInUse = True
            End Try
        End If
        Return thisFileInUse
    End Function


    Public Function CalculateFileSize(FileSize As Long) As String
        Dim CalculatedSize As Decimal

        Dim SizeType As String = "B"

        If FileSize < 1024 Then
            CalculatedSize = FileSize

        ElseIf FileSize >= 1024 AndAlso FileSize < (1024 ^ 2) Then 'KB
            CalculatedSize = Math.Round((FileSize / 1024), 2)
            SizeType = "KB"

        ElseIf FileSize >= (1024 ^ 2) AndAlso FileSize < (1024 ^ 3) Then 'MB
            CalculatedSize = Math.Round((FileSize / (1024 ^ 2)), 2)
            SizeType = "MB"

        ElseIf FileSize >= (1024 ^ 3) AndAlso FileSize < (1024 ^ 4) Then 'GB
            CalculatedSize = Math.Round((FileSize / (1024 ^ 3)), 2)
            SizeType = "GB"

        ElseIf FileSize >= (1024 ^ 4) Then 'TB
            CalculatedSize = Math.Round((FileSize / (1024 ^ 4)), 2)
            SizeType = "TB"

        End If
        Return CalculatedSize.ToString & SizeType
    End Function

    Public Sub ShowFileTransferInProgressMessage()
        If blDownloadIsProgressing Then
            MessageBoxEx.Show("File Download is in progress. Please try later.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If blUploadIsProgressing Then
            MessageBoxEx.Show("File Upload is in progress. Please try later.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If blListIsLoading Then
            MessageBoxEx.Show("File List is loading. Please try later.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub



    Public Sub ShowPleaseWaitForm()
        Dim th As System.Threading.Thread = New Threading.Thread(AddressOf Task_A)
        th.SetApartmentState(ApartmentState.STA)
        th.Start()
    End Sub
    Public Sub Task_A()
        frmNewPleaseWaitForm = New frmPleaseWait ' Must be created on this thread!
        Application.Run(frmNewPleaseWaitForm)
    End Sub

    Public Sub ClosePleaseWaitForm()
        On Error Resume Next
        frmNewPleaseWaitForm.BeginInvoke(New Action(Sub() frmNewPleaseWaitForm.Close()))
    End Sub

    Public Function CompactString(InputString As String, ControlWidth As Integer,
   ControlFont As Drawing.Font,
   FormatFlags As Windows.Forms.TextFormatFlags) As String

        Dim Result As String = String.Copy(InputString)

        TextRenderer.MeasureText(Result, ControlFont, New Drawing.Size(ControlWidth, 0),
            FormatFlags Or TextFormatFlags.ModifyString)

        Return Result

    End Function



    Public Function IsValidFileName(ByVal FileName As String) As Boolean

        If FileName Is Nothing Then
            Return False
        End If

        For Each badChar As Char In System.IO.Path.GetInvalidFileNameChars
            If InStr(FileName, badChar) > 0 Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Function GetAdminPasswords() As Boolean
        Try

            SuperAdminPass = ""
            LocalAdminPass = ""



            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim VersionFolder As String = "Version"
            Dim VersionFolderID As String = ""


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})


            Dim parentid As String = ""
            Dim List = FISService.Files.List()
            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & AdminPasswordFolderName & "'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Return False
            Else
                parentid = Results.Files(0).Id
            End If

            List.Q = "trashed = false and '" & parentid & "' in parents" ' list all files in parent folder. 

            List.Fields = "nextPageToken, files(id, name, description)"
            List.OrderBy = "folder, name" 'sorting order

            Results = List.Execute

            cnt = Results.Files.Count
            If cnt = 0 Then Return False
            For Each Result In Results.Files
                If Result.Name = "SuperAdminPass" Then SuperAdminPass = Result.Description
                If Result.Name = "LocalAdminPass" Then LocalAdminPass = Result.Description
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetAdminPrivilege() As Boolean
        frmInputBox.SetTitleandMessage("Enter Admin Password", "Enter Admin Password", True)
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then
            Return False
        End If

        frmFISBackupList.btnUpdateFileContent.Visible = False

        Dim pass As String = EncryptText(frmInputBox.txtInputBox.Text)
        If pass = SuperAdminPass Then
            FileOwner = "Admin"
            SuperAdmin = True
            LocalAdmin = False
            LocalUser = False
            frmFISBackupList.btnUpdateFileContent.Visible = True
            Return True
        ElseIf pass = LocalAdminPass Then
            FileOwner = "Admin_" & ShortOfficeName & "_" & ShortDistrictName
            LocalAdmin = True
            SuperAdmin = False
            LocalUser = False
            Return True
        Else
            MessageBoxEx.Show("Incorrect Password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            FileOwner = ShortOfficeName & "_" & ShortDistrictName
            SuperAdmin = False
            LocalAdmin = False
            LocalUser = True
            Return False
        End If
    End Function

    Public Function EncryptText(plaintext As String) As String
        Dim WrapperPass As String = "/J2nUqpS"
        Dim Wrapper As New Simple3Des(WrapperPass)
        Dim cipherText As String = Wrapper.EncryptData(plaintext)
        Return cipherText
    End Function

    Public Sub ShowDesktopAlert(ByVal msg As String)
        On Error Resume Next
        If frmMainInterface.chkShowPopups.Checked Then
            If msg.EndsWith("!") = False And msg.EndsWith(".") = False Then
                msg = msg & "."
            End If

            DesktopAlert.PlaySound = frmMainInterface.chkPlaySound.Checked
            DesktopAlert.AutoCloseTimeOut = 3
            DesktopAlert.Show("<u><h5>" & strAppName & "</h5></u>" & msg, eAlertPosition.BottomRight)
        End If

    End Sub
End Module




Class ListViewItemComparer
    Implements IComparer
    Private col As Integer
    Private order As SortOrder

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(column As Integer, sortorder As SortOrder)
        col = column
        Me.order = sortorder
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer = -1
        returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)

        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If
        Return returnVal
    End Function


End Class


Public NotInheritable Class Simple3Des
    Private TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function

    Sub New(ByVal key As String)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub

    Public Function EncryptData(ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms,
            TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function

    Public Function DecryptData(ByVal encryptedtext As String) As String

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms,
            TripleDes.CreateDecryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function


End Class