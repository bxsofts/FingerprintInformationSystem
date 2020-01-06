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

            strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

            OldDBFile = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\BXSofts\" & strAppName & "\Database\Fingerprint.mdb"

            If FileIO.FileSystem.FileExists(strDatabaseFile) = False Then
                strDatabaseFile = OldDBFile
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
                    Me.txtFullOffice.Text = Me.FingerPrintDataSet1.Settings(0).FullOfficeName
                    Me.txtShortOffice.Text = Me.FingerPrintDataSet1.Settings(0).ShortOfficeName
                Else
                    Me.txtFullDistrict.Text = FullDistrictName
                    Me.txtShortDistrict.Text = ShortDistrictName
                    Me.txtFullOffice.Text = FullOfficeName
                    Me.txtShortOffice.Text = ShortOfficeName
                End If

            End If

            If Me.txtFullOffice.Text = "" Then Me.txtFullOffice.Text = "Single Digit Fingerprint Bureau"
            If Me.txtShortOffice.Text = "" Then Me.txtShortOffice.Text = "SDFPB"
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
    Private Sub Wizard1_CancelButtonClick(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
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
            Me.SettingsTableAdapter1.UpdateQuery(FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary, id)
        End If

        boolSettingsWizardCancelled = False

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


End Class