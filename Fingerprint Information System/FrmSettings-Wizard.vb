Imports DevComponents.DotNetBar
Public Class FrmSettingsWizard
    Dim OldDBFile = ""
    Dim BackupPath = ""
    Dim firstrun As Boolean

    Private Sub FormLoad() Handles MyBase.Load
        Try
            firstrun = CBool(My.Computer.Registry.GetValue(strGeneralSettingsPath, "FirstRun", 1))
            If frmMainInterface.Visible = False Then
                SetColorTheme()
            End If

            frmMainInterface.SetColorTheme()
            ShowDotNetWarning()
            boolSettingsWizardCancelled = False




            If (firstrun = False And boolShowWizard = False) Then
                frmMainInterface.Show()
                Me.Close()
                Exit Sub
            End If

            sDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

            If ValidPath(sDatabaseFile) Then
                Me.lblDatabaseLocation.Text = My.Computer.FileSystem.GetParentPath(sDatabaseFile)
            Else
                Me.lblDatabaseLocation.Text = SuggestedLocation & "\Database"
            End If

            OldDBFile = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\BXSofts\" & strAppName & "\Database\Fingerprint.mdb"

            If FileIO.FileSystem.FileExists(sDatabaseFile) = False Then
                sDatabaseFile = OldDBFile
            End If

            BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")

            If BackupPath = strAppUserPath & "\Backups" Then
                BackupPath = SuggestedLocation & "\Backups"
            End If
            Me.lblBackupLocation.Text = BackupPath

            Me.chkAutoBackup.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AutoBackup", 1)
            Dim autobackuptime As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AutoBackupTime", 30)
            If autobackuptime = "TextBoxItem1" Then
                autobackuptime = "30"
            End If


            Me.txtBackupInterval.Text = IIf(autobackuptime = "", "30", autobackuptime)

            sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & sDatabaseFile

            If Me.OfficerTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.OfficerTableTableAdapter1.Connection.Close()
            Me.OfficerTableTableAdapter1.Connection.ConnectionString = sConString
            Me.OfficerTableTableAdapter1.Connection.Open()

            If Me.SettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.SettingsTableAdapter1.Connection.Close()
            Me.SettingsTableAdapter1.Connection.ConnectionString = sConString
            Me.SettingsTableAdapter1.Connection.Open()

            PdlGraveCrime = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlGraveCrime", "12"))
            PdlVigilanceCase = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlVigilanceCase", "6"))
            PdlWeeklyDiary = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlWeeklyDiary", ""))

            If frmMainInterface.DoesTableExist("Settings", sConString) = False Then 'older version of database
                frmMainInterface.CreateSettingsTable()
                Application.DoEvents()
                Me.txtFullDistrict.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "FullDistrictName", "Idukki")
                Me.txtShortDistrict.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "ShortDistrictName", "IDK")
                Me.txtFullOffice.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "FullOfficeName", "Single Digit Fingerprint Bureau")
                Me.txtShortOffice.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "ShortOfficeName", "SDFPB")
                Me.lblFPLocation.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "FPImageImportLocation", SuggestedLocation & "\Scanned FP Slips")
                Me.lblCPLocation.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "CPImageImportLocation", SuggestedLocation & "\Chance Prints")

                Me.txtAttendance.Text = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlAttendance", "1"))
                Me.txtIndividualPerformance.Text = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlIndividual", "2"))
                Me.txtRBWarrant.Text = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlRBWarrant", "3"))
                Me.txtSOCDAStatement.Text = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlSOCDAStatement", "4"))
                Me.txtTABill.Text = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlTABill", "5"))
                Me.txtFPAttestation.Text = Trim(My.Computer.Registry.GetValue(strGeneralSettingsPath, "PdlFPAttestation", "6"))
                Me.txtGraveCrime.Text = PdlGraveCrime
                Me.txtVigilanceCase.Text = PdlVigilanceCase
                Me.txtWeeklyDiary.Text = PdlWeeklyDiary
            Else

                If frmMainInterface.DoesFieldExist("Settings", "PdlGraveCrime", sConString) = False Then 'create new fields
                    frmMainInterface.ModifyTables()
                    Me.txtGraveCrime.Text = PdlGraveCrime
                    Me.txtVigilanceCase.Text = PdlVigilanceCase
                    Me.txtWeeklyDiary.Text = PdlWeeklyDiary

                    Dim id = 1
                    Me.SettingsTableAdapter1.UpdateNullFields(PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary, id)
                End If


                Me.SettingsTableAdapter1.Fill(Me.FingerPrintDataSet1.Settings)
                Dim count = Me.FingerPrintDataSet1.Settings.Count

                If count = 1 Then
                    Me.txtFullDistrict.Text = Me.FingerPrintDataSet1.Settings(0).FullDistrictName
                    Me.txtShortDistrict.Text = Me.FingerPrintDataSet1.Settings(0).ShortDistrictName
                    Me.txtFullOffice.Text = Me.FingerPrintDataSet1.Settings(0).FullOfficeName
                    Me.txtShortOffice.Text = Me.FingerPrintDataSet1.Settings(0).ShortOfficeName
                    CPImageImportLocation = IIf(Me.FingerPrintDataSet1.Settings(0).CPImageImportLocation = "Location", SuggestedLocation & "\Chance Prints", Me.FingerPrintDataSet1.Settings(0).CPImageImportLocation)
                    FPImageImportLocation = IIf(Me.FingerPrintDataSet1.Settings(0).FPImageImportLocation = "Location", SuggestedLocation & "\Scanned FP Slips", Me.FingerPrintDataSet1.Settings(0).FPImageImportLocation)
                    Me.lblFPLocation.Text = FPImageImportLocation
                    Me.lblCPLocation.Text = CPImageImportLocation
                    Me.txtAttendance.Text = Me.FingerPrintDataSet1.Settings(0).PdlAttendance.Trim
                    Me.txtIndividualPerformance.Text = Me.FingerPrintDataSet1.Settings(0).PdlIndividualPerformance.Trim
                    Me.txtRBWarrant.Text = Me.FingerPrintDataSet1.Settings(0).PdlRBWarrant.Trim
                    Me.txtSOCDAStatement.Text = Me.FingerPrintDataSet1.Settings(0).PdlSOCDAStatement.Trim
                    Me.txtTABill.Text = Me.FingerPrintDataSet1.Settings(0).PdlTABill.Trim
                    Me.txtFPAttestation.Text = Me.FingerPrintDataSet1.Settings(0).PdlFPAttestation.Trim
                    Me.txtGraveCrime.Text = Me.FingerPrintDataSet1.Settings(0).PdlGraveCrime.Trim
                    Me.txtVigilanceCase.Text = Me.FingerPrintDataSet1.Settings(0).PdlVigilanceCase.Trim
                    Me.txtWeeklyDiary.Text = Me.FingerPrintDataSet1.Settings(0).PdlWeeklyDiary.Trim

                Else

                    Me.txtFullDistrict.Text = FullDistrictName
                    Me.txtShortDistrict.Text = ShortDistrictName
                    Me.txtFullOffice.Text = FullOfficeName
                    Me.txtShortOffice.Text = ShortOfficeName
                    Me.lblFPLocation.Text = FPImageImportLocation
                    Me.lblCPLocation.Text = CPImageImportLocation
                    Me.txtAttendance.Text = PdlAttendance.Trim
                    Me.txtIndividualPerformance.Text = PdlIndividualPerformance.Trim
                    Me.txtRBWarrant.Text = PdlRBWarrant.Trim
                    Me.txtSOCDAStatement.Text = PdlSOCDAStatement.Trim
                    Me.txtTABill.Text = PdlTABill.Trim
                    Me.txtFPAttestation.Text = PdlFPAttestation.Trim
                    Me.txtGraveCrime.Text = PdlGraveCrime.Trim
                    Me.txtVigilanceCase.Text = PdlVigilanceCase.Trim
                    Me.txtWeeklyDiary.Text = PdlWeeklyDiary.Trim
                End If

            End If

            If frmMainInterface.DoesTableExist("OfficerTable", sConString) = False Then
                frmMainInterface.CreateOfficerTable()
                Application.DoEvents()
            End If

            Me.OfficerTableTableAdapter1.Fill(Me.FingerPrintDataSet1.OfficerTable)
            Dim cnt = Me.FingerPrintDataSet1.OfficerTable.Count
            If cnt >= 1 Then
                Me.txtTI.Text = IIf(Me.FingerPrintDataSet1.OfficerTable(0).TI = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).TI)
                Me.txtFPE1.Text = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPE1 = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPE1)
                Me.txtFPE2.Text = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPE2 = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPE2)
                Me.txtFPE3.Text = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPE3 = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPE3)
                Me.txtFPS.Text = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPS = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPS)
                Me.txtPhotographer.Text = IIf(Me.FingerPrintDataSet1.OfficerTable(0).Photographer = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).Photographer)
            End If


            Me.SettingsWizard.SelectedPageIndex = 0
            Exit Sub

        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Public Sub SetColorTheme() 'set the color scheme
        On Error Resume Next


        Dim VisualStyle As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "VisualStyle", "Office2016")
        Dim Style As eStyle = CType(System.Enum.Parse(GetType(eStyle), VisualStyle), eStyle)

        Dim BaseColor As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BaseColor", "")

        If StyleManager.IsMetro(Style) Then ' if Office2016, metro, vs2012
            Dim UseRandomColor As String = CBool(My.Computer.Registry.GetValue(strGeneralSettingsPath, "UseRandomColor", 1))

            If UseRandomColor And firstrun = False Then
                BaseColor = GetRandomColor()
                My.Computer.Registry.SetValue(strGeneralSettingsPath, "BaseColor", BaseColor, Microsoft.Win32.RegistryValueKind.String)
            End If

            If BaseColor = "" Or BaseColor = "0" Then
                m_BaseColor = StyleManager.MetroColorGeneratorParameters.BaseColor
            Else
                m_BaseColor = CType(Color.FromArgb(BaseColor), Color)
            End If
            ' BaseColor = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BaseColor", StyleManager.MetroColorGeneratorParameters.BaseColor)
            StyleManager.Style = Style
            StyleManager.MetroColorGeneratorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(StyleManager.MetroColorGeneratorParameters.CanvasColor, m_BaseColor)
        Else
            If BaseColor = "" Or BaseColor = "0" Then
                m_BaseColor = Color.Empty
            Else
                m_BaseColor = CType(Color.FromArgb(BaseColor), Color)
            End If
            StyleManager.ChangeStyle(Style, m_BaseColor)
            StyleManager.ColorTint = m_BaseColor
        End If
    End Sub
    Private Sub Wizard1_CancelButtonClick(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SettingsWizard.CancelButtonClick
        On Error Resume Next
        boolSettingsWizardCancelled = True
        If firstrun = "1" Then
            If DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to close the Wizard? You will need to run this wizard again if you want to use the " & strAppName, strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                Me.Close()

            End If
        Else
            Me.Close()

        End If
    End Sub



    Private Sub Wizard1_FinishButtonClick(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SettingsWizard.FinishButtonClick
        On Error Resume Next


        Dim newdbfolder = Me.lblDatabaseLocation.Text
        Dim olddbfolder = My.Computer.FileSystem.GetParentPath(sDatabaseFile)
        If olddbfolder <> newdbfolder Then
            My.Computer.FileSystem.CreateDirectory(newdbfolder)
            My.Computer.FileSystem.CopyFile(sDatabaseFile, newdbfolder & "\Fingerprint.mdb", False)
            sDatabaseFile = newdbfolder & "\Fingerprint.mdb"
            Application.DoEvents()
        End If
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "DatabaseFile", sDatabaseFile, Microsoft.Win32.RegistryValueKind.String)

       
        Dim newbackupfolder = Me.lblBackupLocation.Text
        My.Computer.FileSystem.CreateDirectory(newbackupfolder)

        If (BackupPath <> newbackupfolder) Then
            My.Computer.FileSystem.CopyDirectory(BackupPath, newbackupfolder)
            BackupPath = newbackupfolder
        End If

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "BackupPath", BackupPath, Microsoft.Win32.RegistryValueKind.String)

        Dim s As Boolean = chkAutoBackup.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AutoBackup", v, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AutoBackupTime", Me.txtBackupInterval.Text, Microsoft.Win32.RegistryValueKind.String)

        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation)
        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation & "\DA Slips\")
        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation & "\Identified Slips\")
        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation & "\Active Criminal Slips\")
        My.Computer.FileSystem.CreateDirectory(CPImageImportLocation)

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "FirstRun", "0", Microsoft.Win32.RegistryValueKind.String)

        Application.DoEvents()

        sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & sDatabaseFile

        If Me.OfficerTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.OfficerTableTableAdapter1.Connection.Close()
        Me.OfficerTableTableAdapter1.Connection.ConnectionString = sConString
        Me.OfficerTableTableAdapter1.Connection.Open()

        If Me.SettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.SettingsTableAdapter1.Connection.Close()
        Me.SettingsTableAdapter1.Connection.ConnectionString = sConString
        Me.SettingsTableAdapter1.Connection.Open()


        Me.OfficerTableTableAdapter1.Fill(Me.FingerPrintDataSet1.OfficerTable)
        Dim cnt = Me.FingerPrintDataSet1.OfficerTable.Count
        If cnt = 0 Then
            Me.OfficerTableTableAdapter1.Insert(cnt + 1, Me.txtTI.Text, Me.txtFPE1.Text, Me.txtFPE2.Text, Me.txtFPE3.Text, Me.txtFPS.Text, Me.txtPhotographer.Text)
        Else
            Dim oid = 1

            Me.OfficerTableTableAdapter1.UpdateQuery(Me.txtTI.Text, Me.txtFPE1.Text, Me.txtFPE2.Text, Me.txtFPE3.Text, Me.txtFPS.Text, Me.txtPhotographer.Text, oid)
        End If

        Application.DoEvents()
        TI = Me.txtTI.Text & ", TI"
        FPE1 = Me.txtFPE1.Text & ", FPE"
        FPE2 = Me.txtFPE2.Text & ", FPE"
        FPE3 = Me.txtFPE3.Text & ", FPE"
        FPS = Me.txtFPS.Text & ", FPS"
        strPhotographer = Me.txtPhotographer.Text

        frmMainInterface.txtSOCPhotographer.AutoCompleteCustomSource.Clear()
        frmMainInterface.txtSOCPhotographer.AutoCompleteCustomSource.Add(strPhotographer)
        frmMainInterface.txtSOCPhotographer.AutoCompleteCustomSource.Add("No Photographer")

        FullDistrictName = Me.txtFullDistrict.Text
        FullOfficeName = Me.txtFullOffice.Text
        ShortDistrictName = Me.txtShortDistrict.Text
        ShortOfficeName = Me.txtShortOffice.Text

        PdlAttendance = IIf(Me.txtAttendance.Text = "", "   ", Me.txtAttendance.Text)
        PdlIndividualPerformance = IIf(Me.txtIndividualPerformance.Text = "", "   ", Me.txtIndividualPerformance.Text)
        PdlRBWarrant = IIf(Me.txtRBWarrant.Text = "", "   ", Me.txtRBWarrant.Text)
        PdlSOCDAStatement = IIf(Me.txtSOCDAStatement.Text = "", "   ", Me.txtSOCDAStatement.Text)
        PdlTABill = IIf(Me.txtTABill.Text = "", "   ", Me.txtTABill.Text)
        PdlFPAttestation = IIf(Me.txtFPAttestation.Text = "", "   ", Me.txtFPAttestation.Text)
        PdlGraveCrime = IIf(Me.txtGraveCrime.Text = "", "   ", Me.txtGraveCrime.Text)
        PdlVigilanceCase = IIf(Me.txtVigilanceCase.Text = "", "   ", Me.txtVigilanceCase.Text)
        PdlWeeklyDiary = IIf(Me.txtWeeklyDiary.Text = "", "   ", Me.txtWeeklyDiary.Text)

        CPImageImportLocation = Me.lblCPLocation.Text
        FPImageImportLocation = Me.lblFPLocation.Text

        Me.SettingsTableAdapter1.Fill(Me.FingerPrintDataSet1.Settings)
        Dim count = Me.FingerPrintDataSet1.Settings.Count
        Dim id = 1

        If count = 0 Then
            Me.SettingsTableAdapter1.Insert(id, FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary)
        Else
            Me.SettingsTableAdapter1.UpdateQuery(FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary, id)
        End If

        boolSettingsWizardCancelled = False

        frmMainInterface.Show()
        Me.Hide()

    End Sub

    Private Sub AfterUnitPageDisplayed(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.WizardPageChangeEventArgs) Handles wzrdPageOfficeUnit.AfterPageDisplayed
        On Error Resume Next
        ' If e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then
        Me.txtFullOffice.Focus()
        '  End If
    End Sub


    Private Sub AfterPeriodicalPageDisplayed(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.WizardPageChangeEventArgs) Handles wzrdPagePeriodical.AfterPageDisplayed
        On Error Resume Next
        '  If e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then
        Me.txtAttendance.Focus()
        '  End If
    End Sub

    Private Sub OfficerSettingsPage_AfterPageDisplayed(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.WizardPageChangeEventArgs) Handles wzrdPageOfficers.AfterPageDisplayed
        On Error Resume Next
        ' If e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then
        Me.txtTI.Focus()
        ' End If
    End Sub

    Private Sub Wizard1_WizardPageChanging(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.WizardCancelPageChangeEventArgs) Handles SettingsWizard.WizardPageChanging
        On Error Resume Next

        If e.OldPage Is StartPage AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then
            e.NewPage = wzrdPageOfficeUnit
        End If

        If e.OldPage Is wzrdPageOfficeUnit AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then

            If Strings.Trim(Me.txtFullOffice.Text) = vbNullString Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Full Office Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True
                Me.txtFullOffice.Focus()
                Exit Sub
            End If

            If Strings.Trim(Me.txtShortOffice.Text) = vbNullString Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Short Office Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True
                Me.txtShortOffice.Focus()
                Exit Sub
            End If

            If Strings.Trim(Me.txtFullDistrict.Text) = vbNullString Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Full District Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True
                Me.txtFullDistrict.Focus()
                Exit Sub
            End If

            If Strings.Trim(Me.txtShortDistrict.Text) = vbNullString Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Short District Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True
                Me.txtShortDistrict.Focus()
                Exit Sub
            End If
        End If
    End Sub


    Private Sub SetFPImportLocation() Handles lblFPLocation.Click
        On Error Resume Next

        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location to save scanned FP Slips"
        Me.FolderBrowserDialog1.SelectedPath = FPImageImportLocation
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then

            FPImageImportLocation = Me.FolderBrowserDialog1.SelectedPath
            If FPImageImportLocation.EndsWith("\" & strAppName & "\Scanned FP Slips") = False Then
                If FPImageImportLocation.EndsWith("\" & strAppName) Then
                    FPImageImportLocation = FPImageImportLocation & "\Scanned FP Slips"
                Else
                    FPImageImportLocation = FPImageImportLocation & "\" & strAppName & "\Scanned FP Slips"
                End If
            End If
            FPImageImportLocation = FPImageImportLocation.Replace("\\", "\")
            Me.lblFPLocation.Text = FPImageImportLocation
        End If
    End Sub

    Private Sub SetCPImportLocation() Handles lblCPLocation.Click
        On Error Resume Next

        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location to save imported Chance Prints"
        Me.FolderBrowserDialog1.SelectedPath = CPImageImportLocation
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then

            CPImageImportLocation = Me.FolderBrowserDialog1.SelectedPath
            If CPImageImportLocation.EndsWith("\" & strAppName & "\Chance Prints") = False Then
                If CPImageImportLocation.EndsWith("\" & strAppName) Then
                    CPImageImportLocation = CPImageImportLocation & "\Chance Prints"
                Else
                    CPImageImportLocation = CPImageImportLocation & "\" & strAppName & "\Chance Printse"
                End If
            End If
            CPImageImportLocation = CPImageImportLocation.Replace("\\", "\")
            'My.Computer.FileSystem.CreateDirectory(CPImageImportLocation)
            Me.lblCPLocation.Text = CPImageImportLocation
        End If
    End Sub


    Private Sub ShowDotNetWarning()
        Dim dotnetversion As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", "Release", "")
        If dotnetversion = "" Then
            Dim ShowDotNetVersion As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "ShowDotNetVersion", 1)

            If ShowDotNetVersion = 1 Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Microsoft .NET V4.5 or above is not installed. The application will not work correctly until it is installed." & vbNewLine & "Do you want to hide this message in the future?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                If r = Windows.Forms.DialogResult.Yes Then My.Computer.Registry.SetValue(strGeneralSettingsPath, "ShowDotNetVersion", 0, Microsoft.Win32.RegistryValueKind.String)
                If r = Windows.Forms.DialogResult.No Then My.Computer.Registry.SetValue(strGeneralSettingsPath, "ShowDotNetVersion", 1, Microsoft.Win32.RegistryValueKind.String)
            End If

        End If
    End Sub




    Private Sub SetDatabseLocation(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDatabaseLocation.Click
        On Error Resume Next

        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location for Database"
        Me.FolderBrowserDialog1.SelectedPath = Me.lblDatabaseLocation.Text
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then

            Dim SelectedPath = Me.FolderBrowserDialog1.SelectedPath
            If SelectedPath.EndsWith("\" & strAppName & "\Database") = False Then
                If SelectedPath.EndsWith("\" & strAppName) Then
                    SelectedPath = SelectedPath & "\Database"
                Else
                    SelectedPath = SelectedPath & "\" & strAppName & "\Database"
                End If
            End If

            Me.lblDatabaseLocation.Text = SelectedPath.Replace("\\", "\")
        End If
    End Sub

    Private Sub SetBackupLocation(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBackupLocation.Click
        On Error Resume Next

        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location for Database Backup"
        Me.FolderBrowserDialog1.SelectedPath = Me.lblBackupLocation.Text
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then

            Dim SelectedPath = Me.FolderBrowserDialog1.SelectedPath
            If SelectedPath.EndsWith("\" & strAppName & "\Backups") = False Then
                If SelectedPath.EndsWith("\" & strAppName) Then
                    SelectedPath = SelectedPath & "\Backups"
                Else
                    SelectedPath = SelectedPath & "\" & strAppName & "\Backups"
                End If
            End If
            Me.lblBackupLocation.Text = SelectedPath.Replace("\\", "\")
        End If
    End Sub

    Private Function ValidPath(ByVal Path As String) As Boolean
        Try
            If My.Computer.FileSystem.FileExists(Path) Then
                ValidPath = True
            End If
        Catch ex As Exception
            ValidPath = False
        End Try

    End Function

End Class