
Imports System.IO
Imports DevComponents.DotNetBar

Public Class FrmLocalBackup

    Dim BackupPath As String = ""


    Private Sub frmBackupList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next

        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")
        RenameOldFormatFiles()

        Me.listViewEx1.Items.Clear()


        For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchAllSubDirectories, "FingerPrintBackup*.mdb")

            If foundFile Is Nothing Then
                Exit Sub
            End If

            Dim FileName = My.Computer.FileSystem.GetName(foundFile)
            Dim FullFilePath = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & FileName

            Dim Filedate As DateTime = DateTime.ParseExact(FileName.Replace("FingerPrintBackup-", "").Replace(".mdb", ""), BackupDateFormatString, culture)

            Dim item As ListViewItem = Me.listViewEx1.Items.Add(FileName)
            item.SubItems.Add(Filedate.ToString("dd/MM/yyyy HH:mm:ss"))
            item.SubItems.Add(FullFilePath)
            ' If FullFilePath.Contains("Online") Then
            'item.ImageIndex = 1
            ' Else
            item.ImageIndex = 0
            ' End If

        Next
        DisplayInformation()

        Me.listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
        Me.listViewEx1.Sort()
    End Sub

    Private Sub TakeBackup() Handles btnBackupDatabase.Click
        Try

            If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
                My.Computer.FileSystem.CreateDirectory(BackupPath)
            End If


            Dim BackupTime As Date = Now
            Dim d As String = Strings.Format(BackupTime, BackupDateFormatString)
            Dim sBackupTime = Strings.Format(BackupTime, "dd/MM/yyyy HH:mm:ss")
            Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"


            My.Computer.FileSystem.CopyFile(sDatabaseFile, BackupPath & "\" & BackupFileName, True) ', FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)

            Application.DoEvents()
            Dim item As ListViewItem = Me.listViewEx1.Items.Add(BackupFileName)
            item.SubItems.Add(sBackupTime)
            item.SubItems.Add(BackupPath & "\" & BackupFileName)
            item.ImageIndex = 0

            frmMainInterface.ShowAlertMessage("Database backed up successfully.")
            Application.DoEvents()
            DisplayInformation()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CopyDatabase() Handles btnCopyDatabase.Click
        Try
            If My.Computer.FileSystem.FileExists(sDatabaseFile) = False Then
                MessageBoxEx.Show("The source database file does not exist. Cannot copy database", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.FolderBrowserDialog1.ShowNewFolderButton = True
            Me.FolderBrowserDialog1.Description = "Select location to copy database"

            Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
            If (result = DialogResult.OK) Then
                Dim destination As String = Me.FolderBrowserDialog1.SelectedPath & "\Fingerprint.mdb"
                If UCase(destination) = UCase(sDatabaseFile) Then

                    MessageBoxEx.Show("The source and destination are same. Cannot copy database", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                My.Computer.FileSystem.CopyFile(sDatabaseFile, destination, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
                ' CopyFileWithProgressBar(strDatabaseFile, destination)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RestoreDatabase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreDatabase.Click
        Try
            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Restoring the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.Yes Then
                strBackupFile = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
                My.Computer.FileSystem.CopyFile(strBackupFile, sDatabaseFile, True)
                Application.DoEvents()
                boolRestored = True
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            boolRestored = False
        End Try


    End Sub
    Private Sub ImportDatabase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportDatabase.Click
        Try

            Dim reply As DialogResult

            reply = MessageBoxEx.Show("Importing the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If reply = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If


            OpenFileDialog1.Filter = "FPB Backup Files(*.fpbbk,*.mdb)|*.fpbbk;*.mdb"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select Backup File"
            OpenFileDialog1.RestoreDirectory = True
            OpenFileDialog1.InitialDirectory = BackupPath
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                strBackupFile = OpenFileDialog1.FileName
                If UCase(strBackupFile) = UCase(sDatabaseFile) Then

                    MessageBoxEx.Show("The source and destination are same. Cannot restore database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If
                If frmMainInterface.IsValidBackupFile(strBackupFile) = False Then
                    MessageBoxEx.Show("The file you selected is not a valid Database. Cannot restore database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                My.Computer.FileSystem.CopyFile(strBackupFile, sDatabaseFile, True)
                Application.DoEvents()
                boolRestored = True
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            boolRestored = False
        End Try
    End Sub


    Private Sub CloseForm(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        On Error Resume Next
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub RemoveBackupFile() Handles btnRemoveBackupFile.Click
        On Error Resume Next
        If Me.listViewEx1.Items.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Exit Sub
        End If
        If Me.listViewEx1.SelectedItems.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to remove the selected backup file?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If result = Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.DeleteFile(Me.listViewEx1.SelectedItems(0).SubItems(2).Text, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Me.listViewEx1.SelectedItems(0).Remove()
            Application.DoEvents()
            frmMainInterface.ShowAlertMessage("Selected backup file deleted to the Recycle Bin.")
        End If

        DisplayInformation()
    End Sub


    Private Sub SortByDate(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles listViewEx1.ColumnClick
        If Me.listViewEx1.Sorting = SortOrder.Ascending Then
            Me.listViewEx1.Sorting = SortOrder.Descending
        Else
            Me.listViewEx1.Sorting = SortOrder.Ascending
        End If

        Me.listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, Me.listViewEx1.Sorting)
        Me.listViewEx1.Sort()
    End Sub


    Private Sub btnOpenBackupLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBackupFolder.Click
        On Error Resume Next
        If Me.listViewEx1.SelectedItems.Count <> 0 Then
            Call Shell("explorer.exe /select," & Me.listViewEx1.SelectedItems(0).SubItems(2).Text, AppWinStyle.NormalFocus)
            Exit Sub
        End If

        If Not FileIO.FileSystem.DirectoryExists(BackupPath) Then
            FileIO.FileSystem.CreateDirectory(BackupPath)
        End If

        Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
    End Sub

    Private Sub OpenFileInMSAccess(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick, btnOpenFileMSAccess.Click
        Try
            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Shell("explorer.exe " & Me.listViewEx1.SelectedItems(0).SubItems(2).Text, AppWinStyle.MaximizedFocus)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub RenameOldFormatFiles()
        On Error Resume Next
        Dim RenameOldBackupFiles As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "RenameOldBackupFiles", "1")
        If RenameOldBackupFiles = "0" Then Exit Sub
        Dim OldFormatString As String = "dd-MM-yyyy-hh-mm-ss-tt"
        '  Dim NewFormatString As String = BackupDateFormatString
        Dim OldFileName As String
        Dim NewFileName As String

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchTopLevelOnly, "FingerPrintBackup*.fpbbk")
            If foundFile Is Nothing Then
                Exit Sub
            End If
            OldFileName = My.Computer.FileSystem.GetName(foundFile)
            If OldFileName.Contains("-AM") Or OldFileName.Contains("-PM") Then
                Dim Filedate As DateTime = DateTime.ParseExact(OldFileName.Replace("FingerPrintBackup-", "").Replace(".fpbbk", ""), OldFormatString, culture)

                NewFileName = "FingerPrintBackup-" & Filedate.ToString(BackupDateFormatString) & ".mdb"
                OldFileName = (BackupPath & "\" & OldFileName).Replace("\\", "\")
                My.Computer.FileSystem.RenameFile(OldFileName, NewFileName)
            ElseIf OldFileName.Contains(".fpbbk") Then
                NewFileName = OldFileName.Replace(".fpbbk", ".mdb")
                OldFileName = (BackupPath & "\" & OldFileName).Replace("\\", "\")
                My.Computer.FileSystem.RenameFile(OldFileName, NewFileName)

            End If

        Next
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "RenameOldBackupFiles", "0", Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Function CopyFileWithProgressBar(ByVal Source As String, ByVal Destination As String) As Integer
        Try
            Dim SourceF = New FileStream(Source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim DestinationF = New FileStream(Destination, FileMode.Create, FileAccess.ReadWrite)

            Dim len As Long = SourceF.Length - 1
            Dim buffer(1024) As Byte
            Dim byteCFead As Integer
            '  Me.ProgressBarX1.Value = 0
            While SourceF.Position < len
                byteCFead = (SourceF.Read(buffer, 0, 1024))
                DestinationF.Write(buffer, 0, byteCFead)
                '  ProgressBarX1.Value = CInt(SourceF.Position / len * 100)
                '   ProgressBarX1.Text = CInt(SourceF.Position / len * 100)

                Application.DoEvents()
            End While
            '  Me.ProgressBarX1.Value = 0
            DestinationF.Flush()
            DestinationF.Close()
            SourceF.Close()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Function

    Private Sub DisplayInformation() Handles listViewEx1.Click, listViewEx1.ItemSelectionChanged
        On Error Resume Next

        Me.lblCount.Text = "No. of Backup Files: " & Me.listViewEx1.Items.Count
        If Me.listViewEx1.SelectedItems.Count > 0 Then
            Me.lblSelectedFile.Text = Me.listViewEx1.SelectedItems(0).Text
        Else
            Me.lblSelectedFile.Text = "No file selected"
        End If

    End Sub

    
End Class



