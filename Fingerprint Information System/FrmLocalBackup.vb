
Imports System.IO
Imports DevComponents.DotNetBar

Public Class FrmLocalBackup

    Dim BackupPath As String = ""
    Dim TotalFileSize As Long = 0

    Private Sub frmBackupList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next

        BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")

        Me.listViewEx1.Items.Clear()
        TotalFileSize = 0

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchAllSubDirectories, "FingerPrintBackup*.mdb")

            If foundFile Is Nothing Then
                Exit Sub
            End If
            Dim FileName = My.Computer.FileSystem.GetName(foundFile)
            Dim FullFilePath = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & FileName

            Dim Filedate As DateTime = DateTime.ParseExact(FileName.Replace("FingerPrintBackup-", "").Replace(".mdb", ""), BackupDateFormatString, TimeFormatCulture)

            Dim item As ListViewItem = Me.listViewEx1.Items.Add(FileName)
            item.SubItems.Add(Filedate.ToString("dd-MM-yyyy HH:mm:ss"))
            item.SubItems.Add(FullFilePath)
            Dim fsize = My.Computer.FileSystem.GetFileInfo(FullFilePath).Length
            TotalFileSize += fsize
            item.SubItems.Add(CalculateFileSize(fsize))
            ' If FullFilePath.Contains("Online") Then
            'item.ImageIndex = 1
            ' Else
            item.ImageIndex = 0
            ' End If

        Next
        DisplayInformation()
        Me.lblTotalFileSize.Text = "Total File Size: " & CalculateFileSize(TotalFileSize)

        Me.listViewEx1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)
        Me.listViewEx1.Sort()
        If Me.listViewEx1.Items.Count > 0 Then Me.listViewEx1.Items(0).Selected = True
    End Sub

    Private Sub TakeBackup() Handles btnBackupDatabase.Click
        Try
            If frmMainInterface.pnlRegisterName.Text.EndsWith(" Mode") Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot take Backup.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If My.Computer.FileSystem.DirectoryExists(BackupPath) = False Then
                My.Computer.FileSystem.CreateDirectory(BackupPath)
            End If


            Dim BackupTime As Date = Now
            Dim d As String = Strings.Format(BackupTime, BackupDateFormatString)
            Dim sBackupTime = Strings.Format(BackupTime, "dd-MM-yyyy HH:mm:ss")
            Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"
            Dim FullFilePath As String = BackupPath & "\" & BackupFileName

            My.Computer.FileSystem.CopyFile(strDatabaseFile, FullFilePath, True) ', FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)

            Application.DoEvents()

            Dim item As ListViewItem = Me.listViewEx1.Items.Add(BackupFileName)
            item.SubItems.Add(sBackupTime)
            item.SubItems.Add(FullFilePath)
            Dim fsize As Long = My.Computer.FileSystem.GetFileInfo(FullFilePath).Length
            TotalFileSize += fsize
            item.SubItems.Add(CalculateFileSize(fsize))
            item.ImageIndex = 0


            ShowDesktopAlert("Database backed up successfully.")
            Application.DoEvents()
            DisplayInformation()
            Me.lblTotalFileSize.Text = "Total File Size: " & CalculateFileSize(TotalFileSize)
            If listViewEx1.Items.Count > 0 Then
                listViewEx1.SelectedItems.Clear()
                Me.listViewEx1.Items(0).Selected = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CopyDatabase() Handles btnCopyDatabase.Click
        Try
            If frmMainInterface.pnlRegisterName.Text.EndsWith(" Mode") Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot copy Database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If My.Computer.FileSystem.FileExists(strDatabaseFile) = False Then
                MessageBoxEx.Show("The source database file does not exist. Cannot copy database", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.FolderBrowserDialog1.ShowNewFolderButton = True
            Me.FolderBrowserDialog1.Description = "Select location to copy database"

            Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
            If (result = DialogResult.OK) Then
                Dim destination As String = Me.FolderBrowserDialog1.SelectedPath & "\Fingerprint.mdb"
                If UCase(destination) = UCase(strDatabaseFile) Then

                    MessageBoxEx.Show("The source and destination are same. Cannot copy database", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                My.Computer.FileSystem.CopyFile(strDatabaseFile, destination, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
                ' CopyFileWithProgressBar(strDatabaseFile, destination)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RestoreDatabase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreDatabase.Click, btnRestoreCM.Click
        Try
            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Restoring the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result = Windows.Forms.DialogResult.Yes Then
                strBackupFile = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
                strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")
                My.Computer.FileSystem.CopyFile(strBackupFile, strDatabaseFile, True)
                Application.DoEvents()
                blRestore = True
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            blRestore = False
        End Try
    End Sub
    Private Sub ImportDatabase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportDatabase.Click
        Try
            If frmMainInterface.pnlRegisterName.Text.EndsWith(" Mode") Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot Import Database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

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
                If UCase(strBackupFile) = UCase(strDatabaseFile) Then

                    MessageBoxEx.Show("The source and destination are same. Cannot restore database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If
                If frmMainInterface.IsValidBackupFile(strBackupFile) = False Then
                    MessageBoxEx.Show("The file you selected is not a valid Database. Cannot restore database.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

                My.Computer.FileSystem.CopyFile(strBackupFile, strDatabaseFile, True)
                Application.DoEvents()
                blRestore = True
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            blRestore = False
        End Try
    End Sub


    Private Sub CloseForm(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        On Error Resume Next
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub RemoveBackupFile() Handles btnRemoveBackupFile.Click, btnRemoveCM.Click
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

        If result = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If result = Windows.Forms.DialogResult.Yes Then
            Dim selectedfileindex = Me.listViewEx1.SelectedItems(0).Index
            For i = 0 To Me.listViewEx1.SelectedItems.Count - 1
                Dim fsize As Long = My.Computer.FileSystem.GetFileInfo(Me.listViewEx1.SelectedItems(0).SubItems(2).Text).Length
                TotalFileSize -= fsize
                My.Computer.FileSystem.DeleteFile(Me.listViewEx1.SelectedItems(0).SubItems(2).Text, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                Me.listViewEx1.SelectedItems(0).Remove()
                Application.DoEvents()
            Next

            ShowDesktopAlert("Selected backup files deleted to the Recycle Bin.")
            SelectNextItem(selectedfileindex)
            DisplayInformation()
            Me.lblTotalFileSize.Text = "Total File Size: " & CalculateFileSize(TotalFileSize)
        End If

    End Sub


    Private Sub SelectNextItem(SelectedFileIndex)
        On Error Resume Next
       
        If SelectedFileIndex < listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then 'selected 5 < count 10 
            Me.listViewEx1.Items(SelectedFileIndex).Selected = True 'select 5
        End If

        If SelectedFileIndex = listViewEx1.Items.Count And listViewEx1.Items.Count > 0 Then 'selected 5 = count 5 
            Me.listViewEx1.Items(SelectedFileIndex - 1).Selected = True 'select 5
        End If
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


    Private Sub btnOpenBackupLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBackupFolder.Click, btnOpenFolderCM.Click
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

    Private Sub OpenFileInMSAccess(sender As Object, e As EventArgs) Handles listViewEx1.DoubleClick, btnOpenFileMSAccess.Click, btnOpenFileCM.Click
        Try
            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Shell("explorer.exe " & Me.listViewEx1.SelectedItems(0).SubItems(2).Text, AppWinStyle.MaximizedFocus)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

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

    Private Sub DisplayInformation() 'Handles listViewEx1.Click, listViewEx1.ItemSelectionChanged
        On Error Resume Next
        Me.lblCount.Text = "No. of Backup Files: " & Me.listViewEx1.Items.Count
    End Sub

    Private Sub ContextMenuBar1_PopupOpen(sender As Object, e As PopupOpenEventArgs) Handles ContextMenuBar1.PopupOpen
        On Error Resume Next
        Me.btnRestoreCM.Visible = False
        Me.btnRemoveCM.Visible = False
        Me.btnOpenFileCM.Visible = False
        Me.btnOpenFolderCM.Visible = False

        If Me.listViewEx1.Items.Count = 0 Or Me.listViewEx1.SelectedItems.Count = 0 Then
            e.Cancel = True
        End If

        If Me.listViewEx1.SelectedItems.Count = 1 Then
            Me.btnRestoreCM.Visible = True
            Me.btnRemoveCM.Visible = True
            Me.btnOpenFileCM.Visible = True
            Me.btnOpenFolderCM.Visible = True
        End If

        If Me.listViewEx1.SelectedItems.Count > 1 Then
            Me.btnRemoveCM.Visible = True
        End If
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click, btnPreviewCM.Click
        Try
            If Me.listViewEx1.Items.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.listViewEx1.SelectedItems.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.listViewEx1.SelectedItems.Count > 1 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Select single file only.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            strDatabaseFile = Me.listViewEx1.SelectedItems(0).SubItems(2).Text
            blRestore = False
            blPreviewMode = True

                Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            blRestore = False
        End Try
    End Sub
End Class



