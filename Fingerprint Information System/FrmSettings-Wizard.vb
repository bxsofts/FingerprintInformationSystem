Imports DevComponents.DotNetBar
Public Class FrmSettingsWizard

    Dim BackupPath = ""
    Dim firstrun As Boolean

    Private Sub FormLoad() Handles MyBase.Load
        Try

            Dim ItemList As New ArrayList
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.SpecialDirectories.MyDocuments, FileIO.SearchOption.SearchTopLevelOnly, "Fingerprint Information System V*.exe")
                Dim FileName = My.Computer.FileSystem.GetName(foundFile)
                ItemList.Add(FileName)
            Next

            ItemList.Sort()
            Dim cnt As Integer = ItemList.Count

            If cnt > 0 Then
                Dim NewInstallerName As String = ItemList.Item(cnt - 1)

                If NewInstallerName <> "" Then
                    Dim LocalVersion As String = My.Application.Info.Version.ToString.Substring(0, 4)
                    Dim NewInstallerVersion As String = NewInstallerName.Substring(NewInstallerName.Length - 8).Remove(4)

                    If NewInstallerVersion > LocalVersion Then
                        Dim InstallerFile As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & NewInstallerName
                        If My.Computer.FileSystem.FileExists(InstallerFile) Then
                            Dim exemd5 As String = GetMD5(InstallerFile)
                            Dim remotemd5 As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "MD5", "")
                            If remotemd5 = exemd5 Then
                                    System.Threading.Thread.Sleep(2000)
                                    frmSplashScreen.CloseForm()
                                    Application.DoEvents()
                                    System.Threading.Thread.Sleep(1000)
                                    frmNewVersion.lblMessage.Text = "A new version " & NewInstallerVersion & " is available. Press OK to install."
                                    frmNewVersion.ShowDialog()
                                    Process.Start(InstallerFile)
                                    End
                                End If
                            End If
                        End If
                    End If
                End If



            firstrun = CBool(My.Computer.Registry.GetValue(strGeneralSettingsPath, "FirstRun", 1))
            If frmMainInterface.Visible = False Then
                SetColorTheme()
            End If

            frmMainInterface.SetColorTheme()
            ShowDotNetWarning()

            If (firstrun = False And boolShowWizard = False) Then
                frmMainInterface.Show()
                Me.Close()
                Exit Sub
            End If

            strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

            If Not FileIO.FileSystem.FileExists(strDatabaseFile) Then
                If Not ValidPath(strDatabaseFile) Then
                    strDatabaseFile = SuggestedLocation & "\Database\Fingerprint.mdb"
                End If

                My.Computer.FileSystem.CopyFile(strAppUserPath & "\Database\Fingerprint.mdb", strDatabaseFile, False)
                Application.DoEvents()
            End If

            My.Computer.Registry.SetValue(strGeneralSettingsPath, "DatabaseFile", strDatabaseFile, Microsoft.Win32.RegistryValueKind.String)

            BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")

            If BackupPath = strAppUserPath & "\Backups" Then
                BackupPath = SuggestedLocation & "\Backups"
            End If



            sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strDatabaseFile

            If Me.SettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.SettingsTableAdapter1.Connection.Close()
            Me.SettingsTableAdapter1.Connection.ConnectionString = sConString
            Me.SettingsTableAdapter1.Connection.Open()



            If frmMainInterface.DoesTableExist("Settings", sConString) Then

                Me.SettingsTableAdapter1.Fill(Me.FingerPrintDataSet1.Settings)
                Dim count = Me.FingerPrintDataSet1.Settings.Count

                If count = 1 Then
                    Me.txtFullDistrict.Text = Me.FingerPrintDataSet1.Settings(0).FullDistrictName
                    Me.txtShortDistrict.Text = Me.FingerPrintDataSet1.Settings(0).ShortDistrictName
                    If Me.txtFullDistrict.Text = "FullDistrict" Then Me.txtFullDistrict.Text = ""
                    If Me.txtShortDistrict.Text = "ShortDistrict" Then Me.txtShortDistrict.Text = ""
                    Me.txtFullOffice.Text = Me.FingerPrintDataSet1.Settings(0).FullOfficeName
                    Me.txtShortOffice.Text = Me.FingerPrintDataSet1.Settings(0).ShortOfficeName

                    PdlAttendance = Me.FingerPrintDataSet1.Settings(0).PdlAttendance.Trim
                    PdlIndividualPerformance = Me.FingerPrintDataSet1.Settings(0).PdlIndividualPerformance.Trim
                    PdlRBWarrant = Me.FingerPrintDataSet1.Settings(0).PdlRBWarrant.Trim
                    PdlSOCDAStatement = Me.FingerPrintDataSet1.Settings(0).PdlSOCDAStatement.Trim
                    PdlTABill = Me.FingerPrintDataSet1.Settings(0).PdlTABill.Trim
                    PdlFPAttestation = Me.FingerPrintDataSet1.Settings(0).PdlFPAttestation.Trim
                    PdlGraveCrime = Me.FingerPrintDataSet1.Settings(0).PdlGraveCrime.Trim
                    PdlVigilanceCase = Me.FingerPrintDataSet1.Settings(0).PdlVigilanceCase.Trim
                    PdlWeeklyDiary = Me.FingerPrintDataSet1.Settings(0).PdlWeeklyDiary.Trim

                    FPImageImportLocation = Me.FingerPrintDataSet1.Settings(0).FPImageImportLocation
                    CPImageImportLocation = Me.FingerPrintDataSet1.Settings(0).CPImageImportLocation

                    If FPImageImportLocation = "Location" Then
                        FPImageImportLocation = SuggestedLocation & "\Scanned FP Slips"
                    End If

                    If CPImageImportLocation = "Location" Then
                        CPImageImportLocation = SuggestedLocation & "\Chance Prints"
                    End If

                Else
                    Me.txtFullDistrict.Text = FullDistrictName
                    Me.txtShortDistrict.Text = ShortDistrictName
                    Me.txtFullOffice.Text = FullOfficeName
                    Me.txtShortOffice.Text = ShortOfficeName
                End If

            End If

            If Me.txtFullOffice.Text = "" Then Me.txtFullOffice.Text = "Single Digit Fingerprint Bureau"
            If Me.txtShortOffice.Text = "" Then Me.txtShortOffice.Text = "SDFPB"

            Me.ActiveControl = Me.txtFullDistrict
            Me.txtFullDistrict.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex)
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

    Private Sub SaveSettings() Handles btnSave.Click
        On Error Resume Next

        If Strings.Trim(Me.txtFullOffice.Text) = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Full Office Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtFullOffice.Focus()
            Exit Sub
        End If

        If Strings.Trim(Me.txtShortOffice.Text) = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Short Office Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtShortOffice.Focus()
            Exit Sub
        End If

        If Strings.Trim(Me.txtFullDistrict.Text) = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Full District Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtFullDistrict.Focus()
            Exit Sub
        End If

        If Strings.Trim(Me.txtShortDistrict.Text) = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter Short District Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtShortDistrict.Focus()
            Exit Sub
        End If

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "FirstRun", "0", Microsoft.Win32.RegistryValueKind.String)

        sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strDatabaseFile


        If Me.SettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.SettingsTableAdapter1.Connection.Close()
        Me.SettingsTableAdapter1.Connection.ConnectionString = sConString
        Me.SettingsTableAdapter1.Connection.Open()


        FullDistrictName = Me.txtFullDistrict.Text
        FullOfficeName = Me.txtFullOffice.Text
        ShortDistrictName = Me.txtShortDistrict.Text
        ShortOfficeName = Me.txtShortOffice.Text


        Me.SettingsTableAdapter1.Fill(Me.FingerPrintDataSet1.Settings)
        Dim count = Me.FingerPrintDataSet1.Settings.Count
        Dim id = 1

        If count = 0 Then
            Me.SettingsTableAdapter1.Insert(id, FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary)
        Else
            '   Me.SettingsTableAdapter1.UpdateOfficeSettings(FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, id)

            Me.SettingsTableAdapter1.UpdateQuery(FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary, id)
        End If

        If Me.OfficerTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.OfficerTableTableAdapter1.Connection.Close()
        Me.OfficerTableTableAdapter1.Connection.ConnectionString = sConString
        Me.OfficerTableTableAdapter1.Connection.Open()

        Me.OfficerTableTableAdapter1.Fill(Me.FingerPrintDataSet1.OfficerTable)
        Dim cnt = Me.FingerPrintDataSet1.OfficerTable.Count
        If cnt = 0 Then
            Me.OfficerTableTableAdapter1.Insert(cnt + 1, "", "", "", "", "", "")
        Else
            Dim TI = IIf(Me.FingerPrintDataSet1.OfficerTable(0).TI = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).TI)
            Dim FPE1 = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPE1 = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPE1)
            Dim FPE2 = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPE2 = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPE2)
            Dim FPE3 = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPE3 = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPE3)
            Dim FPS = IIf(Me.FingerPrintDataSet1.OfficerTable(0).FPS = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).FPS)
            Dim Photographer = IIf(Me.FingerPrintDataSet1.OfficerTable(0).Photographer = "Name", "", Me.FingerPrintDataSet1.OfficerTable(0).Photographer)
            Dim oid = 1
            Me.OfficerTableTableAdapter1.UpdateQuery(TI, FPE1, FPE2, FPE3, FPS, Photographer, oid)
        End If

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "BackupPath", BackupPath, Microsoft.Win32.RegistryValueKind.String)

        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation)
        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation & "\DA Slips\")
        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation & "\Identified Slips\")
        My.Computer.FileSystem.CreateDirectory(FPImageImportLocation & "\Active Criminal Slips\")
        My.Computer.FileSystem.CreateDirectory(CPImageImportLocation)
        My.Computer.FileSystem.CreateDirectory(BackupPath)

        frmMainInterface.Show()
        Me.Hide()
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

    Private Function ValidPath(ByVal Path As String) As Boolean
        Try
            If My.Computer.FileSystem.FileExists(Path) Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class