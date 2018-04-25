
Imports System.Threading 'to create a mutex which will ensure that only one application is running
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering
Module modMain
    Public strAppName As String = "Fingerprint Information System"
    Public strRegistrySettingsPath As String = "HKEY_CURRENT_USER\Software\BXSofts\Fingerprint Information System"
    Public strGeneralSettingsPath As String = strRegistrySettingsPath & "\General Settings"
    Public strAppUserPath As String = FileIO.SpecialDirectories.MyDocuments & "\BXSofts\Fingerprint Information System"
    Public strAppPath As String = My.Application.Info.DirectoryPath
    Public strDatabaseFile As String = vbNullString
    Public strConString As String = vbNullString
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

    Public firstrun As String = ""
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
    Public strAnnualStatistics As String = ""
    Public strAnnualStatisticsFrom As String = ""
    Public strAnnualStatisticsTo As String = ""
    Public strAnnualStatisticsPeriod As String = ""
    Public boolShowWizard As Boolean = False
    Public dtWeeklyDiaryFrom As Date
    Public dtWeeklyDiaryTo As Date
    Public boolUseTIinLetter As Boolean = True
    Public BackupDateFormatString As String = "yyyy-MM-dd HH-mm-ss"
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


    Public Function InternetAvailable() As Boolean

        Dim objPing As New System.Net.NetworkInformation.Ping

        Try
            Return If(objPing.Send("www.google.it").Status = System.Net.NetworkInformation.IPStatus.Success, True, False)
        Catch
            Return False
        End Try

    End Function

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

    Public Function Compare(x As Object, y As Object) As Integer _
                            Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer = -1
        returnVal = [String].Compare(CType(x,  _
                        ListViewItem).SubItems(col).Text, _
                        CType(y, ListViewItem).SubItems(col).Text)

        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If
        Return returnVal
    End Function
End Class