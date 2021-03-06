﻿Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Media
Imports Microsoft.Win32
Imports System.Object

Imports System.Runtime.InteropServices

Imports System.Threading
Imports System.Threading.Tasks

Imports Google
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports Google.Apis.Requests

Public Class frmMainInterface

    <DllImport("user32.dll", EntryPoint:="SetForegroundWindow")> _
    Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function


#Region "VARIABLES DECLARATION "


    '-------------- edit mode ---------------------

    Dim SOCEditMode As Boolean = False
    Dim RSOCEditMode As Boolean = False
    Dim DAEditMode As Boolean = False
    Dim IDEditMode As Boolean = False
    Dim ACEditMode As Boolean = False

    Dim CDEditMode As Boolean = False
    Dim PSEditMode As Boolean = False
    Dim SSEditMode As Boolean = False



    '------- ps and officer list changed-------

    Dim PSListChanged As Boolean = False
    Dim OfficerMenuVisibleItemCount As Integer = 0

    '-------- Original Numbers --------

    Dim OriginalSOCNumber As String = vbNullString
    Dim OriginalRSOCSerialNumber As Long = 0
    Dim OriginalDANumber As String = vbNullString
    Dim OriginalIDNumber As Long = 0
    Dim OriginalACNumber As Long = 0
    Dim OriginalFPANumber As String = vbNullString
    Dim OriginalCDNumber As String = vbNullString
    Dim OriginalPSName As String = vbNullString


    '------------ Slip Image ---------------

    Dim DASlipImageFile As String = vbNullString
    Dim IDSlipImageFile As String = vbNullString
    Dim ACSlipImageFile As String = vbNullString

    Dim DASlipImageImportLocation As String
    Dim IDSlipImageImportLocation As String
    Dim ACSlipImageImportLocation As String


    WithEvents devmanager As WIA.DeviceManager

    '------------ Others---------------

    Public CurrentTab As String = "SOC"
    Dim SearchSetting As Integer = 0 'Begins with
    Dim BalloonMessage As DevComponents.DotNetBar.Balloon 'to show the alert message
    Dim SelectedRowIndex As Long = -1
    Dim SelectedColumnIndex As Integer
    Dim ColumnHeaderClicked As Boolean = False
    Dim CPIdentified As Integer = 0
    Dim CPValidated As Boolean = False
    Dim TemporarilyStopCapitalize As Boolean
    Dim ShowAllFields As Boolean = True
    Dim ShowStatusTexts As Boolean = False
    Dim blApplicationIsLoading As Boolean = True
    Dim blApplicationIsRestoring As Boolean = False
    Dim ClipBoardText As String = ""
    Dim PhotographerFieldFocussed As Boolean = False
    Dim PhotographedDateFocussed As Boolean = False

    Dim IOSelectedRow As Integer

    Dim TableEvenColor As Color
    Dim TableOddColor As Color

    Dim InstallerFileName As String = ""
    Dim InstallerFileID As String = ""
    Dim InstallerFileURL As String = "" '"https://drive.google.com/file/d/1vyGdhxjXUWjkcgTE_rTT7juiMSBA-UKc/view"

    Public blShowUpdateDownloaded As Boolean
    Public dBytesDownloaded As Long
    Public dDownloadStatus As DownloadStatus
    Public dFileSize As Long
    Public uUploadStatus As UploadStatus
    Dim dFormatedFileSize As String = ""
    Public uBytesUploaded As Long

    Dim blCheckAutoBackup As Boolean = False

    Dim blIdentificationRegisterUpdateFailed As Boolean = False
    Dim blRestartApplication As Boolean = False
    Dim blUpdateLastModificationDate As Boolean = True

    Dim RemoteModifiedDate As String = ""
    Dim RemoteModificationDetails As String = ""
    Dim InstallerMD5 As String = ""

    Dim UnreadIFTFileCount As Integer = 0
    Dim InstallerFileSize As String = ""
#End Region


#Region "FORM LOAD EVENTS"
    Private Sub LoadForm() Handles MyBase.Load

        On Error Resume Next

        If Not frmSplashScreen.Visible Then
            ShowPleaseWaitForm()
        End If
        Me.cprDBAvailable.Visible = False
        Me.cprUnreadFile.Visible = False

        ChangeCursor(Cursors.WaitCursor)

        frmSplashScreen.ShowProgressBar()

        IncrementCircularProgress(1)

        If FrmSettingsWizard.Visible Then FrmSettingsWizard.Close()
        My.Application.SplashScreen.BringToFront()
        ' SetColorTheme()
        SetTableBackColor()

        IncrementCircularProgress(1)

        strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

        sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strDatabaseFile

        If Me.SettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.SettingsTableAdapter1.Connection.Close()
        Me.SettingsTableAdapter1.Connection.ConnectionString = sConString
        Me.SettingsTableAdapter1.Connection.Open()

        If Me.CommonSettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.CommonSettingsTableAdapter1.Connection.Close()
        Me.CommonSettingsTableAdapter1.Connection.ConnectionString = sConString
        Me.CommonSettingsTableAdapter1.Connection.Open()

        LoadOfficeSettingsToMemory()

        ChangeCursor(Cursors.Default)
        LoadQuickToolBarSettings()
        ChangeCursor(Cursors.WaitCursor)

        IncrementCircularProgress(1)

        SetWindowTitle()

        DisplayTime()
        Me.txtSOCOfficer.ButtonCustom2.Image = Me.txtSOCPhotographer.ButtonCustom.Image
        AddHandler TabControl.TabStrip.MouseDown, AddressOf TabControl_MouseDown


        IncrementCircularProgress(1)

        '---------------------------------------'

        Me.RibbonControl1.SelectedRibbonTabItem = Me.tabHome
        Me.RibbonControl1.Expanded = My.Computer.Registry.GetValue(strGeneralSettingsPath, "RibbonVisible", 1)
        Me.chkHideDataEntryFieldsAtStartup.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HideFields", 0)


        ShowAllFields = Not chkHideDataEntryFieldsAtStartup.Checked

        If Me.chkHideDataEntryFieldsAtStartup.Checked Then
            ShowAllFields = False
        Else
            ShowAllFields = True
        End If

        ShowAllDataEntryFields(ShowAllFields)

        If Me.PanelSOC.Visible = False Then
            Me.SOCDatagrid.Focus()
        Else
            Me.txtSOCNumber.Focus()
        End If



        IncrementCircularProgress(1)

        blUseTIinLetter = My.Computer.Registry.GetValue(strGeneralSettingsPath, "UseTIinLetter", 0)
        Me.chkUseTIAtBottomOfLetter.Checked = blUseTIinLetter

        IncrementCircularProgress(1)
        SetDatagridFont()
        SetDatagridSortMode()
        SetAscendingSortMode()

        TemporarilyStopCapitalize = False

        SetTabColor()

        IncrementCircularProgress(1)


        '------------ Save Defualt Column Width in first run ---------------

        Dim savewidth As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "SaveDefaultWidth", "1")
        If savewidth = 1 Then
            SaveSOCDatagridColumnDefaultWidth()
            SaveRSOCDatagridColumnDefaultWidth()
            SaveDADatagridColumnDefaultWidth()
            SaveIDDatagridColumnDefaultWidth()
            SaveACDatagridColumnDefaultWidth()
            SaveFPADatagridColumnDefaultWidth()
            SaveCDDatagridColumnDefaultWidth()
            SavePSDatagridColumnDefaultWidth()
            SaveIDRDatagridColumnDefaultWidth()
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "SaveDefaultWidth", "0", Microsoft.Win32.RegistryValueKind.String)
        End If
        IncrementCircularProgress(1)

        '------------ Load Column Width ---------------
        If savewidth = 1 Then
            LoadSOCDatagridColumnDefaultWidth()
        Else
            LoadSOCDatagridColumnWidth()
        End If

        LoadRSOCDatagridColumnWidth()


        IncrementCircularProgress(1)

        LoadDADatagridColumnWidth()

        LoadIDDatagridColumnWidth()

        LoadACDatagridColumnWidth()

        LoadFPADatagridColumnWidth()

        LoadCDDatagridColumnWidth()

        LoadPSDatagridColumnWidth()
        LoadIDRDatagridColumnWidth()
        Invalidate()


        '------------ Load Column Order ---------------

        If savewidth = 1 Then
            LoadSOCDatagridColumnDefaultOrder()
        Else
            LoadSOCDatagridColumnOrder()
        End If



        IncrementCircularProgress(1)
        LoadRSOCDatagridColumnOrder()
        IncrementCircularProgress(1)
        LoadDADatagridColumnOrder()
        IncrementCircularProgress(1)
        LoadIDDatagridColumnOrder()
        IncrementCircularProgress(1)
        LoadACDatagridColumnOrder()
        IncrementCircularProgress(1)
        LoadFPADatagridColumnOrder()
        IncrementCircularProgress(1)
        LoadCDDatagridColumnOrder()
        IncrementCircularProgress(1)
        LoadPSDatagridColumnOrder()
        LoadIDRDatagridColumnOrder()
        IncrementCircularProgress(1)
        Invalidate()
        IncrementCircularProgress(1)

        FreezeNumberColumns()

        '------------ initialize fields ---------------



        Dim d As Date = Today
        Me.txtSOCYear.Value = Year(d)
        Me.txtDAYear.Value = Me.txtSOCYear.Text
        Me.txtCDYear.Value = Me.txtSOCYear.Text
        cmbDASex.SelectedIndex = 1
        cmbIDSex.SelectedIndex = 1
        cmbACSex.SelectedIndex = 1
        IncrementCircularProgress(1)

        '------------ date values ---------------

        Me.dtSOCInspection.Value = d
        Me.dtSOCReport.Value = d
        Me.dtDAEntry.Value = d
        Me.dtCDExamination.Value = d
        Me.dtRSOCReportSentOn.Value = d
        IncrementCircularProgress(1)
        LoadGeneralSettings()
        IncrementCircularProgress(1)




        '------------ device manager ---------------

        IncrementCircularProgress(1)
        devmanager = New WIA.DeviceManager
        devmanager.RegisterEvent(WIA.EventID.wiaEventDeviceConnected)
        devmanager.RegisterEvent(WIA.EventID.wiaEventDeviceDisconnected)
        IncrementCircularProgress(1)

        '------------ Connecting To Database ---------------


        Dim DBExists As Boolean = FileIO.FileSystem.FileExists(strDatabaseFile)

        If DBExists Then
            ConnectToDatabase()
            Dim CreateTable As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "CreateTable", "1")

            If CreateTable = "1" Then
                blUpdateLastModificationDate = False
                CreateLastModificationTable()
                CreateIdentificationRegisterTable()
                CreateSOCReportRegisterTable()
                CreateCommonSettingsTable()
                CreateSupportingStaffTable()
                CreateChalanRegisterTable()
                CreateRevenueCollectionTable()
                ModifyTables()
                ReCopyValuesToChalanTable()
                My.Computer.Registry.SetValue(strGeneralSettingsPath, "CreateTable", "0", Microsoft.Win32.RegistryValueKind.String)
            End If

            '------------ load last soc number ---------------

            GenerateNewSOCNumber()
            GenerateNewRSOCSerialNumber()
            GenerateNewDANumber()
            GenerateNewCDNumber()
            GenerateNewIDNumber()
            GenerateNewACNumber()

            IncrementCircularProgress(1)
            UpdateNullFields()
            IncrementCircularProgress(1)
            ShowStatusTexts = True


            '------------ load records, ps list, officer list ---------------


            If Me.chkLoadRecordsAtStartup.Checked Then
                LoadRecordsToAllTablesDependingOnCurrentYearSettings()
                Me.JoinedIDRBindingSource.MoveLast()
                IncrementCircularProgress(1)
            End If


            LoadPSListOnLoad()

            Me.cmbFileStatus.Items.Add("Closed")
            Me.cmbFileStatus.Items.Add("Identified")
            Me.cmbFileStatus.Items.Add("Otherwise Detected")

            IncrementCircularProgress(1)

            lblDesignation.Visible = False
            RemoveNullFromOfficerTable()
            InitializeOfficerTable()
            LoadOfficerToMemory()
            IncrementCircularProgress(3)
            LoadOfficerListToTable()

            '   LoadOfficeSettingsToMemory()

            LoadOfficeSettingsToTextBoxes()

            LoadNatureOfReport()
            IncrementCircularProgress(1)

            Me.chkTakeAutoBackup.Checked = My.Computer.Registry.GetValue(strBackupSettingsPath, "AutoBackup", 1)
            Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet.CommonSettings, "AutoBackupTime")

            Dim count As Integer = Me.FingerPrintDataSet.CommonSettings.Count
            If count = 1 Then
                Dim autobackuptime As String = Me.FingerPrintDataSet.CommonSettings(0).SettingsValue
                Me.txtAutoBackupPeriod.TextBox.Text = IIf(autobackuptime = "", "15", autobackuptime)
            Else
                Me.txtAutoBackupPeriod.TextBox.Text = "15"
            End If

        End If



        '------------load auto text ---------------

        If chkLoadAutoTextAtStartup.Checked And DBExists Then LoadAutoTextFromDB()

        LoadModusOperandi()

        Dim pgbarvalue = 100
        For i = pgbarvalue To 100
            IncrementCircularProgress(1)
        Next


        Dim dm As String = Today.ToString("dd/MM/yyyy", TimeFormatCulture)
        dm = Strings.Left(dm, 5)
        Dim msg = ""
        Select Case dm
            Case "01/01"
                msg = "Happy New Year!"
            Case "26/01"
                msg = "Happy Republic Day!"
            Case "15/08"
                msg = "Happy Independance Day!"
            Case "25/12"
                msg = "Happy X'mas!"
            Case Else
                msg = "Have a nice day!"
        End Select

        Me.btnSOCReport2.Icon = Me.btnSOCReport.Icon
        blApplicationIsLoading = False
        DisplayDatabaseInformation()

        '  My.Application.SplashScreen.Close()
        '   My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "ForegroundLockTimeout", 0, Microsoft.Win32.RegistryValueKind.DWord)

        ' Me.BringToFront()
        ' Me.Activate()

        ChangeCursor(Cursors.Default)
        System.Threading.Thread.Sleep(1000)
        ClosePleaseWaitForm()
        frmSplashScreen.CloseForm()

        SetForegroundWindow(MyBase.Handle)

        SetRandomDesktopAlertColor()
        ShowDesktopAlert("Welcome to " & strAppName & "!<br/>" & msg)


        btnViewReports.AutoExpandOnClick = True
        blApplicationIsLoading = False

        If blIdentificationRegisterUpdateFailed Then
            System.Threading.Thread.Sleep(3000)
            MessageBoxEx.Show("The application will now close to complete the updation and restart again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            blRestartApplication = True
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "CreateTable", "1", Microsoft.Win32.RegistryValueKind.String)
            EndApplication()
        End If

        Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet.CommonSettings, "MinimumVersion")
        Dim cnt As Integer = Me.FingerPrintDataSet.CommonSettings.Count
        Dim minimumversion As String = "162"
        If cnt = 0 Then
            Me.CommonSettingsTableAdapter1.Insert("MinimumVersion", minimumversion, "")
        Else
            Dim storedversion As String = Me.FingerPrintDataSet.CommonSettings(0).SettingsValue
            If Val(storedversion) < Val(minimumversion) Then
                Me.CommonSettingsTableAdapter1.UpdateQuery(minimumversion, "", "MinimumVersion")
            End If
        End If

        CopyCredentialFiles()

        If DBExists Then
            FileOwner = ShortOfficeName & "_" & ShortDistrictName

            bgwCleanOnlineFiles.RunWorkerAsync()

            ShowNewVersionInstalledInfo()
            CheckForUpdatesAtStartup()
            UploadVersionInfoToDrive()
            CleanupLocalBackupFiles()
            bgwCopyFolder.RunWorkerAsync()
        End If

        If DBExists = False Then
            Dim DBMissing As String = "FATAL ERROR: The database file 'Fingerprint.mdb' is missing. Please restore the database from Local or Online Backups."
            Me.pnlRegisterName.Text = DBMissing
            DisableControls()
            MessageBoxEx.Show(DBMissing, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        CheckForRecentRemoteDatabase()
    End Sub

#End Region


#Region "COLOR STYLES" 'sets Color themes for the form


    Public Sub SetColorTheme() 'set the color scheme
        On Error Resume Next

        Dim VisualStyle As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "VisualStyle", "Office2016")
        Dim Style As eStyle = CType(System.Enum.Parse(GetType(eStyle), VisualStyle), eStyle)

        Dim BaseColor As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BaseColor", "")

        If StyleManager.IsMetro(Style) Then ' if Office2016, metro, vs2012
            Me.btnRandomColor.Visible = True
            If BaseColor = "" Or BaseColor = "0" Then
                m_BaseColor = StyleManager.MetroColorGeneratorParameters.BaseColor
            Else
                m_BaseColor = CType(Color.FromArgb(BaseColor), Color)
            End If
            StyleManager.Style = Style
            StyleManager.MetroColorGeneratorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(StyleManager.MetroColorGeneratorParameters.CanvasColor, m_BaseColor)
        Else
            Me.btnRandomColor.Visible = False
            If BaseColor = "" Or BaseColor = "0" Then
                m_BaseColor = Color.Empty
            Else
                m_BaseColor = CType(Color.FromArgb(BaseColor), Color)
            End If
            StyleManager.ChangeStyle(Style, m_BaseColor)
            StyleManager.ColorTint = m_BaseColor
        End If

        Select Case VisualStyle
            Case btnOffice2016.CommandParameter
                btnOffice2016.Checked = True
            Case btnOfficeMobile2014.CommandParameter
                btnOfficeMobile2014.Checked = True
            Case btnMetro.CommandParameter
                btnMetro.Checked = True
            Case btnVS2012.CommandParameter
                btnVS2012.Checked = True
            Case btnVS2010.CommandParameter
                btnVS2010.Checked = True
            Case btnOffice2010Blue.CommandParameter
                btnOffice2010Blue.Checked = True
            Case btnOffice2010Silver.CommandParameter
                btnOffice2010Silver.Checked = True
            Case btnOffice2010Black.CommandParameter
                btnOffice2010Black.Checked = True
            Case btnOffice2007Blue.CommandParameter
                btnOffice2007Blue.Checked = True
            Case btnOffice2007Silver.CommandParameter
                btnOffice2007Silver.Checked = True
            Case btnOffice2007Black.CommandParameter
                btnOffice2007Black.Checked = True
            Case btnOffice2007VistaGlass.CommandParameter
                btnOffice2007VistaGlass.Checked = True
            Case btnWindows7Blue.CommandParameter
                btnWindows7Blue.Checked = True
            Case Else
                btnOffice2016.Checked = True
        End Select

    End Sub

    Private Sub SetColorThemeFromMenuButtons(sender As Object, e As EventArgs) Handles AppCommandTheme.Executed
        Try
            Dim Source As ICommandSource = TryCast(sender, ICommandSource)
            Dim VisualStyle As String = ""

            If TypeOf Source.CommandParameter Is String Then 'From Style Menu buttons
                VisualStyle = Source.CommandParameter.ToString()
                Dim Style As eStyle = CType(System.Enum.Parse(GetType(eStyle), Source.CommandParameter.ToString()), eStyle)

                If StyleManager.IsMetro(Style) Then 'Office2016, Metro, VS2012
                    Me.btnRandomColor.Visible = True

                    If Style = eStyle.Metro Then
                        StyleManager.MetroColorGeneratorParameters = DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters.Default
                    End If
                    StyleManager.Style = Style
                    My.Computer.Registry.SetValue(strGeneralSettingsPath, "BaseColor", StyleManager.MetroColorGeneratorParameters.BaseColor.ToArgb.ToString, Microsoft.Win32.RegistryValueKind.String)
                Else
                    Me.btnRandomColor.Visible = False
                    StyleManager.ChangeStyle(Style, Color.Empty)
                    StyleManager.ColorTint = Color.Empty
                    My.Computer.Registry.SetValue(strGeneralSettingsPath, "BaseColor", StyleManager.ColorTint.ToArgb.ToString, Microsoft.Win32.RegistryValueKind.String)
                End If
                My.Computer.Registry.SetValue(strGeneralSettingsPath, "VisualStyle", VisualStyle, Microsoft.Win32.RegistryValueKind.String)


            ElseIf TypeOf Source.CommandParameter Is Color Then 'From custom base color picker
                m_BaseColor = CType(Source.CommandParameter, Color)
                If StyleManager.IsMetro(StyleManager.Style) Then
                    StyleManager.MetroColorGeneratorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(StyleManager.MetroColorGeneratorParameters.CanvasColor, m_BaseColor)
                Else
                    StyleManager.ColorTint = m_BaseColor
                End If

                My.Computer.Registry.SetValue(strGeneralSettingsPath, "BaseColor", btnCustomBaseColor.SelectedColor.ToArgb.ToString, Microsoft.Win32.RegistryValueKind.String)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub ShowCustomBaseColorPreview(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.ColorPreviewEventArgs) Handles btnCustomBaseColor.ColorPreview
        m_BaseColor = e.Color
        If StyleManager.IsMetro(StyleManager.Style) Then
            StyleManager.MetroColorGeneratorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(StyleManager.MetroColorGeneratorParameters.CanvasColor, m_BaseColor)
        Else
            StyleManager.ColorTint = m_BaseColor
        End If
    End Sub


    Private Sub CustomBaseColorSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomBaseColor.SelectedColorChanged
        m_BaseColorSelected = True ' Indicate that color was selected for buttonStyleCustom_ExpandChange method
        btnCustomBaseColor.CommandParameter = btnCustomBaseColor.SelectedColor
        Me.btnRandomColor.Checked = False
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "UseRandomColor", 0, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub CustomBaseColorExpandChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomBaseColor.ExpandChange
        If btnCustomBaseColor.Expanded Then
            ' Remember the starting color scheme to apply if no color is selected during live-preview
            m_BaseColorSelected = False
        Else
            If m_BaseColorSelected = False Then
                SetColorTheme()
            End If
        End If

    End Sub

    Private Sub SetRandomColorButton() Handles btnRandomColor.Click
        On Error Resume Next
        btnRandomColor.Checked = Not btnRandomColor.Checked
        Dim s As Boolean = btnRandomColor.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "UseRandomColor", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub

    '-------------------- TAB COLORS  --------------------------

    Dim m_TabMouseDown As TabItem

    Private Sub TabControl_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        On Error Resume Next
        If e.Button <> MouseButtons.Right Then Exit Sub

        ' Convert to screen based coordinates for context menu
        Dim p As Point = New Point(e.X, e.Y)
        Dim pScreen As Point = TabControl.PointToScreen(p)
        Dim tab As TabItem = TabControl.TabStrip.HitTest(p.X, p.Y)
        If Not (tab Is Nothing) Then
            m_TabMouseDown = tab
            CreateTabColorContextMenu()
            Dim item As ButtonItem = CType(TabColorContextMenuBar.Items("tabColors"), ButtonItem)
            item.Popup(pScreen)

            For Each i As ButtonItem In item.SubItems
                If i.Text = tab.PredefinedColor.ToString Then
                    i.FontBold = True
                Else
                    i.FontBold = False
                End If

            Next
        Else
            CreateTabStyleContextMenu()
            Dim item As ButtonItem = CType(TabStyleContextMenuBar.Items("tabStyles"), ButtonItem)
            item.Popup(pScreen)
            For Each i As ButtonItem In item.SubItems
                If i.Text = Me.TabControl.Style.ToString Then
                    i.Checked = True
                Else
                    i.Checked = False
                End If
            Next
        End If
    End Sub

    Private Sub CreateTabColorContextMenu()

        If TabColorContextMenuBar.Items.Contains("tabColors") Then Exit Sub

        Dim item As ButtonItem = New ButtonItem("tabColors", "Tab Colors Context menu")
        item.Style = eDotNetBarStyle.Office2007
        TabColorContextMenuBar.Items.Add(item)

        ' Create one menu item for each entry in eTabItemColor
        Dim names As String() = System.Enum.GetNames(GetType(eTabItemColor))

        Dim s As String
        For Each s In names

            If Not s.StartsWith("Office") Then


                Dim menuItem As ButtonItem = New ButtonItem(s, s)
                ' Construct the image for the menu item by creating dummy tab item to get right colors from it
                Dim tabTemp As TabItem = New TabItem()
                tabTemp.PredefinedColor = CType(System.Enum.Parse(GetType(eTabItemColor), s), eTabItemColor)
                Dim bmp As Bitmap = New Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                Dim g As Graphics = Graphics.FromImage(bmp)
                Try
                    g.Clear(SystemColors.Control)
                    Dim r As Rectangle = New Rectangle(1, 1, 14, 14)
                    Dim brush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(r, tabTemp.BackColor, tabTemp.BackColor2, tabTemp.BackColorGradientAngle)
                    g.FillRectangle(brush, r)
                    brush.Dispose()
                    Dim pen As Pen = New Pen(Color.DarkGray, 1)
                    g.DrawRectangle(pen, r)
                    pen.Dispose()
                Finally
                    g.Dispose()
                End Try
                bmp.MakeTransparent(SystemColors.Control)
                menuItem.Image = bmp
                AddHandler menuItem.Click, AddressOf ChangeTabColor
                item.SubItems.Add(menuItem)
            End If
        Next
    End Sub


    Private Sub CreateTabStyleContextMenu()
        On Error Resume Next
        If TabStyleContextMenuBar.Items.Contains("tabStyles") Then Exit Sub

        Dim item As ButtonItem = New ButtonItem("tabStyles", "Tab Styles Context menu")
        item.Style = eDotNetBarStyle.Office2007
        TabStyleContextMenuBar.Items.Add(item)

        ' Create one menu item for each entry in eTabItemColor
        Dim names As String() = System.Enum.GetNames(GetType(eTabStripStyle))
        Dim s As String
        For Each s In names

            Dim menuItem As ButtonItem = New ButtonItem(s, s)
            AddHandler menuItem.Click, AddressOf ChangeTabStyle
            item.SubItems.Add(menuItem)

        Next
    End Sub

    Private Sub ChangeTabColor(ByVal sender As Object, ByVal e As EventArgs)
        On Error Resume Next
        If Not m_TabMouseDown Is Nothing Then
            m_TabMouseDown.PredefinedColor = CType((System.Enum.Parse(GetType(eTabItemColor), CType(sender, BaseItem).Text)), eTabItemColor)
        End If
        Dim tab As TabItem
        For Each tab In TabControl.Tabs
            My.Computer.Registry.SetValue(strRegistrySettingsPath & "\TabColorSettings", tab.Name, CType(tab.PredefinedColor, Integer), Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub

    Private Sub SetTabColor()
        On Error Resume Next
        Dim tab As TabItem
        For Each tab In TabControl.Tabs
            tab.PredefinedColor = My.Computer.Registry.GetValue(strRegistrySettingsPath & "\TabColorSettings", tab.Name, 0)
        Next

        TabControl.Style = My.Computer.Registry.GetValue(strRegistrySettingsPath & "\TabColorSettings", "TabStyle", 9)

    End Sub

    Private Sub ChangeTabStyle(ByVal sender As Object, ByVal e As EventArgs)
        On Error Resume Next
        Dim button As ButtonItem = CType(sender, ButtonItem)
        Dim style As eTabStripStyle = System.Enum.Parse(GetType(eTabStripStyle), CType(sender, BaseItem).Text)
        TabControl.Style = style
        My.Computer.Registry.SetValue(strRegistrySettingsPath & "\TabColorSettings", "TabStyle", CType(style, Integer), Microsoft.Win32.RegistryValueKind.String)
    End Sub


#End Region


#Region "LOAD SETTINGS"

    Private Sub LoadGeneralSettings()
        On Error Resume Next

        Dim d As Date = Today
        Me.dtSOCInspection.MonthCalendar.DisplayMonth = d
        IncrementCircularProgress(1)
        Me.dtSOCReport.MonthCalendar.DisplayMonth = d
        IncrementCircularProgress(1)
        Me.dtDAEntry.MonthCalendar.DisplayMonth = d
        Me.dtCDExamination.MonthCalendar.DisplayMonth = d
        Me.dtRSOCInspection.MonthCalendar.DisplayMonth = d
        Me.dtRSOCReportSentOn.MonthCalendar.DisplayMonth = d

        IncrementCircularProgress(1)
        CurrentTab = "SOC"
        Me.TabControl.SelectedTab = SOCTabItem
        IncrementCircularProgress(1)
        Me.txtSOCNumber.Focus()
        IncrementCircularProgress(1)

        Me.chkAutoCapitalize.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AutoCapitalize", 1)
        TemporarilyStopCapitalize = Not Me.chkAutoCapitalize.Checked
        DisplayAutoCapsStatus(Me.chkAutoCapitalize.Checked)
        Me.lblAutoCapsStatus.Visible = True

        Me.chkIgnoreAllCaps.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "IgnoreAllCaps", 1)
        IncrementCircularProgress(1)

        Me.chkShowPopups.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "ShowPopups", 1)
        IncrementCircularProgress(1)

        Me.chkPlaySound.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "PlaySound", 1)
        IncrementCircularProgress(1)

        Me.cmbAutoCompletionMode.SelectedIndex = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AutoCompleteMode", 1)

        Dim searchsetting As Integer = My.Computer.Registry.GetValue(strGeneralSettingsPath, "SearchSetting", 1)
        If searchsetting = 1 Then Me.rdoBeginsWith.Checked = True
        If searchsetting = 2 Then Me.rdoFullText.Checked = True
        If searchsetting = 3 Then Me.rdoAnyWhere.Checked = True

        Dim hcolor As Integer = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HighLightColor", 5) 'none
        Me.cmbHighlightColor.SelectedIndex = hcolor - 1
        IncrementCircularProgress(1)

        Me.chkLoadAutoTextAtStartup.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "LoadAutoTextFromDB", 1)
        IncrementCircularProgress(1)
        GetFPSlipImageImportLocation()

        IncrementCircularProgress(1)
        GetCPImageImportLocation()

        IncrementCircularProgress(1)

        IncrementCircularProgress(1)
        Me.chkLoadRecordsAtStartup.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "LoadRecordsAtStartup", 1)
        IncrementCircularProgress(1)
        Me.chkLoadCurrentYearRecordsOnly.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "LoadCurrentYearRecordsOnly", 0)
        Me.chkAppendSOCYear.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AppendSOCYear", 1)
        Me.chkAppendDAYear.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AppendDAYear", 1)

        Me.chkAppendCDYear.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AppendCDYear", 1)

        Me.chkSOCTwodigits.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TwoDigitSOCYear", 1)
        Me.chkDATwodigits.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TwoDigitDAYear", 1)
       
        Me.chkCDTwodigits.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TwoDigitCDYear", 1)


        Me.chkIDLoadOtherDetails.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "LoadDAtoID", 1)
        Me.chkACLoadOtherDetails.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "LoadDAtoAC", 1)
        Me.btnRandomColor.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "UseRandomColor", 1)

        Me.txtLoadSelectedYearRecords.Text = Year(Today)

        Me.cmbSalutation.Items.Add("Sri.")
        Me.cmbSalutation.Items.Add("Smt.")
        Me.cmbSalutation.DropDownStyle = ComboBoxStyle.DropDown
        Me.cmbSalutation.AutoCompleteMode = AutoCompleteMode.SuggestAppend

    End Sub


    Private Sub LoadOfficeSettingsToMemory()
        On Error Resume Next

        Me.SettingsTableAdapter1.Fill(Me.FingerPrintDataSet.Settings)
        Dim count = Me.FingerPrintDataSet.Settings.Count

        If count = 1 Then
            FullDistrictName = Me.FingerPrintDataSet.Settings(0).FullDistrictName
            ShortDistrictName = Me.FingerPrintDataSet.Settings(0).ShortDistrictName
            FullOfficeName = Me.FingerPrintDataSet.Settings(0).FullOfficeName
            ShortOfficeName = Me.FingerPrintDataSet.Settings(0).ShortOfficeName
            PdlAttendance = Me.FingerPrintDataSet.Settings(0).PdlAttendance.Trim
            PdlIndividualPerformance = Me.FingerPrintDataSet.Settings(0).PdlIndividualPerformance.Trim
            PdlRBWarrant = Me.FingerPrintDataSet.Settings(0).PdlRBWarrant.Trim
            PdlSOCDAStatement = Me.FingerPrintDataSet.Settings(0).PdlSOCDAStatement.Trim
            PdlTABill = Me.FingerPrintDataSet.Settings(0).PdlTABill.Trim
            PdlFPAttestation = Me.FingerPrintDataSet.Settings(0).PdlFPAttestation.Trim
            PdlGraveCrime = Me.FingerPrintDataSet.Settings(0).PdlGraveCrime.Trim
            PdlVigilanceCase = Me.FingerPrintDataSet.Settings(0).PdlVigilanceCase.Trim
            PdlWeeklyDiary = Me.FingerPrintDataSet.Settings(0).PdlWeeklyDiary.Trim

            FPImageImportLocation = Me.FingerPrintDataSet.Settings(0).FPImageImportLocation
            CPImageImportLocation = Me.FingerPrintDataSet.Settings(0).CPImageImportLocation
        End If

        Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet.CommonSettings, "PdlIdentificationStatement")
        count = Me.FingerPrintDataSet.CommonSettings.Count
        If count = 1 Then
            PdlIdentificationStatement = Me.FingerPrintDataSet.CommonSettings(0).SettingsValue
        End If

        Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet.CommonSettings, "PdlBuildingProject")
        count = Me.FingerPrintDataSet.CommonSettings.Count
        If count = 1 Then
            PdlBuildingProject = Me.FingerPrintDataSet.CommonSettings(0).SettingsValue
        End If

        Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet.CommonSettings, "PdlMonthlyStatements")
        count = Me.FingerPrintDataSet.CommonSettings.Count
        If count = 1 Then
            PdlMonthlyStatements = Me.FingerPrintDataSet.CommonSettings(0).SettingsValue
        End If

    End Sub

    Public Sub LoadOfficeSettingsToTextBoxes()
        OfficeSettingsEditMode(True)
        Me.txtFullDistrict.Text = FullDistrictName
        Me.txtShortDistrict.Text = ShortDistrictName
        Me.txtFullOffice.Text = FullOfficeName
        Me.txtShortOffice.Text = ShortOfficeName
        Me.txtAttendance.Text = PdlAttendance
        Me.txtIndividualPerformance.Text = PdlIndividualPerformance
        Me.txtRBWarrant.Text = PdlRBWarrant
        Me.txtSOCDAStatement.Text = PdlSOCDAStatement
        Me.txtTABill.Text = PdlTABill
        Me.txtFPAttestation.Text = PdlFPAttestation
        Me.txtGraveCrime.Text = PdlGraveCrime
        Me.txtVigilanceCase.Text = PdlVigilanceCase
        Me.txtWeeklyDiary.Text = PdlWeeklyDiary
        Me.txtIdentificationPdlNumber.Text = PdlIdentificationStatement
        Me.txtBuildingsProject.Text = PdlBuildingProject
        Me.txtMonthlyStatements.Text = PdlMonthlyStatements
        OfficeSettingsEditMode(False)
    End Sub

    Public Sub SetWindowTitle()
        Dim version As String = " V" & My.Application.Info.Version.ToString.Substring(0, 4)

        Me.Text = (strAppName & version & " - " & FullOfficeName & ", " & FullDistrictName).ToUpper
        Me.RibbonControl1.Text = Me.Text
        Me.RibbonControl1.TitleText = "<b>" & Me.Text & "</b>"
    End Sub
#End Region


#Region "CONNECT TO DATABASE"
    Sub ConnectToDatabase()
        On Error Resume Next


        If Me.OfficerTableAdapter.Connection.State = ConnectionState.Open Then Me.OfficerTableAdapter.Connection.Close()
        Me.OfficerTableAdapter.Connection.ConnectionString = sConString
        Me.OfficerTableAdapter.Connection.Open()

        If Me.SettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.SettingsTableAdapter1.Connection.Close()
        Me.SettingsTableAdapter1.Connection.ConnectionString = sConString
        Me.SettingsTableAdapter1.Connection.Open()

        If Me.CommonSettingsTableAdapter1.Connection.State = ConnectionState.Open Then Me.CommonSettingsTableAdapter1.Connection.Close()
        Me.CommonSettingsTableAdapter1.Connection.ConnectionString = sConString
        Me.CommonSettingsTableAdapter1.Connection.Open()

        If Me.SOCRegisterAutoTextTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterAutoTextTableAdapter.Connection.Close()
        Me.SOCRegisterAutoTextTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterAutoTextTableAdapter.Connection.Open()


        If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
        Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.IdentificationRegisterTableAdapter1.Connection.Open()

        If Me.JoinedIDRTableAdapter.Connection.State = ConnectionState.Open Then Me.JoinedIDRTableAdapter.Connection.Close()
        Me.JoinedIDRTableAdapter.Connection.ConnectionString = sConString
        Me.JoinedIDRTableAdapter.Connection.Open()

        If Me.RSOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.RSOCRegisterTableAdapter.Connection.Close()
        Me.RSOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.RSOCRegisterTableAdapter.Connection.Open()

        If Me.SocReportRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocReportRegisterTableAdapter1.Connection.Close()
        Me.SocReportRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.SocReportRegisterTableAdapter1.Connection.Open()

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

        If Me.DARegisterAutoTextTableAdapter.Connection.State = ConnectionState.Open Then Me.DARegisterAutoTextTableAdapter.Connection.Close()
        Me.DARegisterAutoTextTableAdapter.Connection.ConnectionString = sConString
        Me.DARegisterAutoTextTableAdapter.Connection.Open()


        If Me.DARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DARegisterTableAdapter.Connection.Close()
        Me.DARegisterTableAdapter.Connection.ConnectionString = sConString
        Me.DARegisterTableAdapter.Connection.Open()


        If Me.FPARegisterAutoTextTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterAutoTextTableAdapter.Connection.Close()
        Me.FPARegisterAutoTextTableAdapter.Connection.ConnectionString = sConString
        Me.FPARegisterAutoTextTableAdapter.Connection.Open()


        If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
        Me.FPARegisterTableAdapter.Connection.ConnectionString = sConString
        Me.FPARegisterTableAdapter.Connection.Open()

        If Me.ChalanTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.ChalanTableTableAdapter1.Connection.Close()
        Me.ChalanTableTableAdapter1.Connection.ConnectionString = sConString
        Me.ChalanTableTableAdapter1.Connection.Open()

        If Me.CDRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CDRegisterTableAdapter.Connection.Close()
        Me.CDRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.CDRegisterTableAdapter.Connection.Open()


        If Me.PSRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.PSRegisterTableAdapter.Connection.Close()
        Me.PSRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.PSRegisterTableAdapter.Connection.Open()


        If Me.IDRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.IDRegisterTableAdapter.Connection.Close()
        Me.IDRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.IDRegisterTableAdapter.Connection.Open()


        If Me.IDRegisterAutoTextTableAdapter.Connection.State = ConnectionState.Open Then Me.IDRegisterAutoTextTableAdapter.Connection.Close()
        Me.IDRegisterAutoTextTableAdapter.Connection.ConnectionString = sConString
        Me.IDRegisterAutoTextTableAdapter.Connection.Open()

        If Me.ACRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.ACRegisterTableAdapter.Connection.Close()
        Me.ACRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.ACRegisterTableAdapter.Connection.Open()


        If Me.ACRegisterAutoTextTableAdapter.Connection.State = ConnectionState.Open Then Me.ACRegisterAutoTextTableAdapter.Connection.Close()
        Me.ACRegisterAutoTextTableAdapter.Connection.ConnectionString = sConString
        Me.ACRegisterAutoTextTableAdapter.Connection.Open()


        If Me.ACRegisterAutoTextTableAdapter.Connection.State = ConnectionState.Open Then Me.ACRegisterAutoTextTableAdapter.Connection.Close()
        Me.ACRegisterAutoTextTableAdapter.Connection.ConnectionString = sConString
        Me.ACRegisterAutoTextTableAdapter.Connection.Open()

        If Me.JoinedIDRTableAdapter.Connection.State = ConnectionState.Open Then Me.JoinedIDRTableAdapter.Connection.Close()
        Me.JoinedIDRTableAdapter.Connection.ConnectionString = sConString
        Me.JoinedIDRTableAdapter.Connection.Open()

        If Me.LastModificationTableAdapter.Connection.State = ConnectionState.Open Then Me.LastModificationTableAdapter.Connection.Close()
        Me.LastModificationTableAdapter.Connection.ConnectionString = sConString
        Me.LastModificationTableAdapter.Connection.Open()

        If Me.CulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.CulpritsRegisterTableAdapter1.Connection.Close()
        Me.CulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.CulpritsRegisterTableAdapter1.Connection.Open()

        If Me.SSTableAdapter1.Connection.State = ConnectionState.Open Then Me.SSTableAdapter1.Connection.Close()
        Me.SSTableAdapter1.Connection.ConnectionString = sConString
        Me.SSTableAdapter1.Connection.Open()
    End Sub
#End Region


#Region "UPDATE NULL FIELDS"

    Private Sub UpdateNullFields()
        On Error Resume Next
        Dim update As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "UpdateNullFields", "1")
        If update = 1 Then
            Me.DARegisterTableAdapter.UpdateModusOperandi("")
            Me.SOCRegisterTableAdapter.RemoveNullFromFileStatus("")
            Me.SOCRegisterTableAdapter.UpdateFileStatusForClosedFiles()
            Me.FPARegisterTableAdapter.RemoveNullFromHeadOfAccount("")
            Me.PSRegisterTableAdapter.RemoveNullFromSHO("")
            Me.SOCRegisterTableAdapter.UpdateQuerySetBlankCPsIdentifiedToZero("0", "")

            RemoveNullFromIdentificationRegister()
            RemoveNullFromOfficerTable()
            RemoveNullFromCulpritRegister()

            My.Computer.Registry.SetValue(strGeneralSettingsPath, "UpdateNullFields", "0", Microsoft.Win32.RegistryValueKind.String)
        End If

    End Sub

    Private Sub RemoveNullFromOfficerTable()
        On Error Resume Next
        Me.OfficerTableAdapter.RemoveNullFromTI("")
        Me.OfficerTableAdapter.RemoveNullFromFPE1("")
        Me.OfficerTableAdapter.RemoveNullFromFPE2("")
        Me.OfficerTableAdapter.RemoveNullFromFPE3("")
        Me.OfficerTableAdapter.RemoveNullFromFPS("")
        Me.OfficerTableAdapter.RemoveNullFromPhotographer("")

    End Sub

    Private Sub btnFixDBTables_Click(sender As Object, e As EventArgs) Handles btnFixDBTables.Click
        Try
            If blDBIsInPreviewMode Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot fix tables.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "CreateTable", "1", Microsoft.Win32.RegistryValueKind.String)

            CreateLastModificationTable()
            CreateIdentificationRegisterTable()
            CreateSOCReportRegisterTable()
            CreateCommonSettingsTable()
            CreateSupportingStaffTable()
            CreateChalanRegisterTable()
            CreateRevenueCollectionTable()
            ModifyTables()

            My.Computer.Registry.SetValue(strGeneralSettingsPath, "UpdateNullFields", "1", Microsoft.Win32.RegistryValueKind.String)

            UpdateNullFields()
            ReCopyValuesToChalanTable()
            MessageBoxEx.Show("Fixed Database Tables.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


#Region "LOAD RECORDS TO TABLE"

    Private Sub LoadSOCRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor


        Me.SOCRegisterBindingSource.Sort = SOCDatagrid.Columns(2).DataPropertyName.ToString() & " ASC, " & SOCDatagrid.Columns(1).DataPropertyName.ToString() & " ASC"

        Dim oldrow As String = ""
        If Me.SOCDatagrid.SelectedRows.Count <> 0 Then
            oldrow = Me.SOCDatagrid.SelectedRows(0).Cells(0).Value
        End If


        Me.SOCRegisterTableAdapter.Fill(Me.FingerPrintDataSet.SOCRegister)
        Me.SOCRegisterBindingSource.MoveLast()

        If blApplicationIsRestoring Then
            For i = 51 To 60
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        Else
            If blApplicationIsRestoring Or blDBIsInPreviewMode Then
                Me.SOCRegisterBindingSource.MoveLast()
            Else
                Dim p = Me.SOCRegisterBindingSource.Find("SOCNumber", oldrow)
                If p >= 0 Then Me.SOCRegisterBindingSource.Position = p
            End If
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadRSOCRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor


        Me.RSOCRegisterBindingSource.Sort = RSOCDatagrid.Columns(3).DataPropertyName.ToString() & " ASC, " & RSOCDatagrid.Columns(2).DataPropertyName.ToString() ' & " ASC, " & RSOCDatagrid.Columns(7).DataPropertyName.ToString() & " ASC"
        Dim oldrow As String = ""
        If Me.RSOCDatagrid.SelectedRows.Count <> 0 Then
            oldrow = Me.RSOCDatagrid.SelectedRows(0).Cells(0).Value
        End If
        Me.RSOCRegisterTableAdapter.Fill(Me.FingerPrintDataSet.SOCReportRegister)
        Me.RSOCRegisterBindingSource.MoveLast()

        If blApplicationIsRestoring Then
            For i = 61 To 70
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadDARecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Me.DARegisterBindingSource.Sort = DADatagrid.Columns(2).DataPropertyName.ToString() & " ASC, " & DADatagrid.Columns(1).DataPropertyName.ToString() & " ASC"

        Dim oldrow As String = ""
        If Me.DADatagrid.SelectedRows.Count <> 0 Then
            oldrow = Me.DADatagrid.SelectedRows(0).Cells(0).Value
        End If
        Me.DARegisterTableAdapter.Fill(Me.FingerPrintDataSet.DARegister)
        Me.DARegisterBindingSource.MoveLast()


        If blApplicationIsRestoring Then
            For i = 71 To 80
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If


        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        Else
            If blApplicationIsRestoring Or blDBIsInPreviewMode Then
                Me.DARegisterBindingSource.MoveLast()
            Else
                Dim p = Me.DARegisterBindingSource.Find("DANumber", oldrow)
                If p >= 0 Then Me.DARegisterBindingSource.Position = p
            End If
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadIDRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Me.IDRegisterBindingSource.Sort = IDDatagrid.Columns(0).DataPropertyName.ToString() & " ASC"
        Dim oldrow As String = ""
        If Me.IDDatagrid.SelectedRows.Count <> 0 Then
            oldrow = Me.IDDatagrid.SelectedRows(0).Cells(0).Value
        End If
        Me.IDRegisterTableAdapter.Fill(Me.FingerPrintDataSet.IdentifiedSlipsRegister)
        Me.IDRegisterBindingSource.MoveLast()


        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        Else
            If blApplicationIsRestoring Or blDBIsInPreviewMode Then
                Me.IDRegisterBindingSource.MoveLast()
            Else
                Dim p = Me.IDRegisterBindingSource.Find("IDNumber", oldrow)
                If p >= 0 Then Me.IDRegisterBindingSource.Position = p
            End If
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadACRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Me.ACRegisterBindingSource.Sort = ACDatagrid.Columns(0).DataPropertyName.ToString() & " ASC"

        Dim oldrow As String = ""
        If Me.ACDatagrid.SelectedRows.Count <> 0 Then
            oldrow = Me.ACDatagrid.SelectedRows(0).Cells(0).Value
        End If
        Me.ACRegisterTableAdapter.Fill(Me.FingerPrintDataSet.ActiveCriminalsRegister)
        Me.ACRegisterBindingSource.MoveLast()

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        Else
            If blApplicationIsRestoring Or blDBIsInPreviewMode Then
                Me.ACRegisterBindingSource.MoveLast()
            Else
                Dim p = Me.ACRegisterBindingSource.Find("ACNumber", oldrow)
                If p >= 0 Then Me.ACRegisterBindingSource.Position = p
            End If
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadFPARecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Me.FPARegisterBindingSource.Sort = FPADataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & FPADataGrid.Columns(1).DataPropertyName.ToString() & " ASC"

        Dim oldrow As String = ""
        If Me.FPADataGrid.SelectedRows.Count <> 0 Then
            oldrow = Me.FPADataGrid.SelectedRows(0).Cells(0).Value
        End If
        Me.FPARegisterTableAdapter.Fill(Me.FingerPrintDataSet.FPAttestationRegister)
        Me.FPARegisterBindingSource.MoveLast()

        If blApplicationIsRestoring Then
            For i = 81 To 85
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        Else
            If blApplicationIsRestoring Or blDBIsInPreviewMode Then
                Me.FPARegisterBindingSource.MoveLast()
            Else
                Dim p = Me.FPARegisterBindingSource.Find("FPNumber", oldrow)
                If p >= 0 Then Me.FPARegisterBindingSource.Position = p
            End If
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadCDRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Me.CDRegisterBindingSource.Sort = CDDataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & CDDataGrid.Columns(1).DataPropertyName.ToString() & " ASC"
        Dim oldrow As String = ""
        If Me.CDDataGrid.SelectedRows.Count <> 0 Then
            oldrow = Me.CDDataGrid.SelectedRows(0).Cells(0).Value
        End If
        Me.CDRegisterTableAdapter.Fill(Me.FingerPrintDataSet.CDRegister)
        Me.CDRegisterBindingSource.MoveLast()

        If blApplicationIsRestoring Then
            For i = 86 To 90
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        Else
            If blApplicationIsRestoring Or blDBIsInPreviewMode Then
                Me.CDRegisterBindingSource.MoveLast()
            Else
                Dim p = Me.CDRegisterBindingSource.Find("CDNumberWithYear", oldrow)
                If p >= 0 Then Me.CDRegisterBindingSource.Position = p
            End If
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadPSRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.PSRegisterBindingSource.Sort = PSDataGrid.Columns(0).DataPropertyName.ToString() & " ASC"
        Me.PSRegisterTableAdapter.Fill(Me.FingerPrintDataSet.PoliceStationList)
        Me.PSRegisterBindingSource.MoveFirst()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadIDRRecords()
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.JoinedIDRBindingSource.Sort = JoinedIDRDataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & JoinedIDRDataGrid.Columns(19).DataPropertyName.ToString() & " ASC"

            Dim oldrow As String = ""
            If Me.JoinedIDRDataGrid.SelectedRows.Count > 0 Then
                oldrow = Me.JoinedIDRDataGrid.SelectedRows(0).Cells(0).Value
            End If

            Me.JoinedIDRTableAdapter.Fill(Me.FingerPrintDataSet.JoinedIDR)

            If blDBIsInPreviewMode Or blApplicationIsLoading Or blApplicationIsRestoring Then
                Me.JoinedIDRBindingSource.MoveLast()
            Else
                Dim p = Me.JoinedIDRBindingSource.Find("IdentificationNumber", oldrow)
                If p >= 0 Then Me.JoinedIDRBindingSource.Position = p
            End If

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadSSRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.SSTableAdapter1.Fill(Me.FingerPrintDataSet.SupportingStaff)
        Me.SSBindingSource.MoveLast()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Sub LoadRecordsToAllTablesDependingOnCurrentYearSettings() 'loads data to the datagrid
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Dim blloadcurrentyearonly As Boolean = chkLoadCurrentYearRecordsOnly.Checked

        If blRestoreDB Or blPreviewDB Then
            blloadcurrentyearonly = False
        End If

        If blloadcurrentyearonly Then
            LoadCurrentYearRecords()
        Else
            LoadSOCRecords()
            LoadRSOCRecords()
            LoadDARecords()
            LoadFPARecords()
            LoadCDRecords()
            LoadIDRRecords()
        End If

        LoadIDRecords()
        LoadSSRecords()
        If blApplicationIsRestoring Then
            For i = 91 To 95
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        LoadACRecords()
        LoadPSRecords()

        If blApplicationIsRestoring Then
            For i = 96 To 100
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(50)
            Next

            blApplicationIsRestoring = False
            frmProgressBar.Close()
            blRestoreDB = False
            ShowDesktopAlert("Database restored successfully!")
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Sub LoadRecordsToAllTablesWithMessage() Handles btnReloadRecordsToAllTables.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        LoadSOCRecords()
        LoadRSOCRecords()
        LoadDARecords()
        LoadIDRRecords()
        LoadFPARecords()
        LoadCDRecords()
        LoadIDRecords()
        LoadACRecords()
        LoadPSRecords()
        LoadSSRecords()
        LoadOfficerListToTable()
        LoadOfficerListToDropDownMenu()
        LoadOfficeSettingsToMemory()
        LoadOfficeSettingsToTextBoxes()
        ShowDesktopAlert("Records reloaded in all tables!")
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Sub LoadRecordsToTableWithAlertMessage() Handles btnReload.Click 'loads data in the datagrid

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        On Error Resume Next

        Select Case CurrentTab
            Case "SOC"
                LoadSOCRecords()
                ShowDesktopAlert("Records reloaded in SOC Register!")
            Case "RSOC"
                LoadRSOCRecords()
                ShowDesktopAlert("Records reloaded in SOC Reports Register!")
            Case "DA"
                LoadDARecords()
                ShowDesktopAlert("Records reloaded in DA Register!")
            Case "FPA"
                LoadFPARecords()
                ShowDesktopAlert("Records reloaded in FP Attestation Register!")
            Case "CD"
                LoadCDRecords()
                ShowDesktopAlert("Records reloaded in Court Duty Register!")
            Case "ID"
                LoadIDRecords()
                ShowDesktopAlert("Records reloaded in Identified Slips Register!")
            Case "AC"
                LoadACRecords()
                ShowDesktopAlert("Records reloaded in Active Criminal Slips Register!")
            Case "PS"
                LoadPSRecords()
                ShowDesktopAlert("Records reloaded in List of Police Stations!")

            Case "IO"
                LoadOfficerListToTable()
                ShowDesktopAlert("Records reloaded in Officer List!")

            Case "SS"
                LoadSSRecords()
                ShowDesktopAlert("Records reloaded in Supporting Staff List!")

            Case "OS"
                LoadOfficeSettingsToMemory()
                LoadOfficeSettingsToTextBoxes()
                ShowDesktopAlert("Records reloaded in Office Settings!")

            Case "IDR"
                Me.Cursor = Cursors.WaitCursor
                LoadIDRRecords()
                ShowDesktopAlert("Records reloaded in Identification Register!")
                Me.Cursor = Cursors.Default
        End Select

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadCurrentYearRecords()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim y = Year(Today)
        Dim d1 As Date = New Date(y, 1, 1)
        Dim d2 As Date = New Date(y, 12, 31)

        Me.SOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)
        If Me.FingerPrintDataSet.SOCRegister.Count = 0 And Today.Month = 1 Then
            Me.SOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1.AddYears(-1), d2)
        End If
        Me.SOCRegisterBindingSource.MoveLast()

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
            Application.DoEvents()
        End If


        If blApplicationIsRestoring Then
            For i = 51 To 60
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        Me.DARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.DARegister, d1, d2)

        If Me.FingerPrintDataSet.DARegister.Count = 0 And Today.Month = 1 Then
            Me.DARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.DARegister, d1.AddYears(-1), d2)
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
        End If

        If blApplicationIsRestoring Then
            For i = 61 To 70
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        Me.FPARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.FPAttestationRegister, d1, d2)

        If Me.FingerPrintDataSet.FPAttestationRegister.Count = 0 And Today.Month = 1 Then
            Me.FPARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.FPAttestationRegister, d1.AddYears(-1), d2)
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
        End If

        If blApplicationIsRestoring Then
            For i = 71 To 80
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        Me.RSOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCReportRegister, d1, d2)

        If Me.FingerPrintDataSet.SOCReportRegister.Count = 0 And Today.Month = 1 Then
            Me.RSOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCReportRegister, d1.AddYears(-1), d2)
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
        End If

        If blApplicationIsRestoring Then
            For i = 81 To 85
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        Me.JoinedIDRTableAdapter.FillByIdentifiedCases(Me.FingerPrintDataSet.JoinedIDR, d1, d2)

        If Me.FingerPrintDataSet.JoinedIDR.Count = 0 And Today.Month = 1 Then
            Me.JoinedIDRTableAdapter.FillByIdentifiedCases(Me.FingerPrintDataSet.JoinedIDR, d1.AddYears(-1), d2)
        End If

        Me.CDRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.CDRegister, d1, d2)

        If Me.FingerPrintDataSet.CDRegister.Count = 0 And Today.Month = 1 Then
            Me.CDRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.CDRegister, d1.AddYears(-1), d2)
        End If

        If blApplicationIsLoading Then
            IncrementCircularProgress(5)
        End If

        If blApplicationIsRestoring Then
            For i = 86 To 90
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
        End If

        Me.RSOCRegisterBindingSource.MoveLast()
        Me.DARegisterBindingSource.MoveLast()
        Me.FPARegisterBindingSource.MoveLast()
        Me.CDRegisterBindingSource.MoveLast()
        Me.JoinedIDRBindingSource.MoveLast()

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LoadRecordsOfTheSelectedYear(sender As Object, e As KeyEventArgs) Handles txtLoadSelectedYearRecords.KeyDown

        Try

            If e.KeyCode <> Keys.Enter Then Exit Sub
            Dim y As Integer = CInt(Me.txtLoadSelectedYearRecords.Text)

            Select Case CurrentTab
                Case "PS"
                    ShowDesktopAlert("This option is not available for the selected register!")
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    Exit Sub
            End Select



            Dim d1 As Date = New Date(y, 1, 1)
            Dim d2 As Date = New Date(y, 12, 31)

            Select Case CurrentTab
                Case "SOC"
                    Me.SOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)
                    Me.SOCRegisterBindingSource.MoveLast()

                Case "RSOC"
                    Me.RSOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCReportRegister, d1, d2)
                    Me.RSOCRegisterBindingSource.MoveLast()

                Case "DA"
                    Me.DARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.DARegister, d1, d2)
                    Me.DARegisterBindingSource.MoveLast()

                Case "CD"
                    Me.CDRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.CDRegister, d1, d2)
                    Me.CDRegisterBindingSource.MoveLast()

                Case "FPA"
                    Me.FPARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.FPAttestationRegister, d1, d2)
                    Me.FPARegisterBindingSource.MoveLast()

                Case "IDR"
                    Me.JoinedIDRTableAdapter.FillByIdentifiedCases(Me.FingerPrintDataSet.JoinedIDR, d1, d2)
                    Me.JoinedIDRBindingSource.MoveLast()

            End Select

            ShowDesktopAlert(pnlRegisterName.Text & " records of year " & y & " loaded!")
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "LOAD PS"
    Public Sub LoadPSListOnLoad()
        On Error Resume Next
        Me.PSRegisterTableAdapter.Fill(FingerPrintDataSet.PoliceStationList)
        Me.PSRegisterBindingSource.MoveFirst()
        Dim drpdwnlength As Integer
        For i As Short = 0 To Me.FingerPrintDataSet.PoliceStationList.Count - 1
            Dim ps As String = Me.FingerPrintDataSet.PoliceStationList(i).PoliceStation
            Me.cmbSOCPoliceStation.Items.Add(ps)
            Me.cmbRSOCPoliceStation.Items.Add(ps)
            Me.cmbDAPoliceStation.Items.Add(ps)
            Me.cmbCDPoliceStation.Items.Add(ps)
            Me.cmbIDPoliceStation.Items.Add(ps)
            Me.cmbACPoliceStation.Items.Add(ps)
            ChangeDropdownWidth(ps, cmbSOCPoliceStation)
        Next

        drpdwnlength = Me.cmbSOCPoliceStation.DropDownWidth
        Me.cmbRSOCPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbDAPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbCDPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbIDPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbACPoliceStation.DropDownWidth = drpdwnlength

        PSListChanged = False

    End Sub


    Public Sub LoadPSList()
        On Error Resume Next
        Me.cmbSOCPoliceStation.Items.Clear()
        Me.cmbRSOCPoliceStation.Items.Clear()
        Me.cmbSOCPoliceStation.Items.Clear()
        Me.cmbDAPoliceStation.Items.Clear()
        Me.cmbCDPoliceStation.Items.Clear()
        Me.cmbIDPoliceStation.Items.Clear()
        Me.cmbACPoliceStation.Items.Clear()

        Me.PSRegisterTableAdapter.Fill(FingerPrintDataSet.PoliceStationList)
        Me.PSRegisterBindingSource.MoveFirst()
        Dim drpdwnlength As Integer
        For i As Short = 0 To Me.FingerPrintDataSet.PoliceStationList.Count - 1
            Dim ps As String = Me.FingerPrintDataSet.PoliceStationList(i).PoliceStation
            Me.cmbSOCPoliceStation.Items.Add(ps)
            Me.cmbRSOCPoliceStation.Items.Add(ps)
            Me.cmbDAPoliceStation.Items.Add(ps)
            Me.cmbCDPoliceStation.Items.Add(ps)
            Me.cmbIDPoliceStation.Items.Add(ps)
            Me.cmbACPoliceStation.Items.Add(ps)
            ChangeDropdownWidth(ps, cmbSOCPoliceStation)
        Next
        PSListChanged = False

        drpdwnlength = Me.cmbSOCPoliceStation.DropDownWidth
        Me.cmbRSOCPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbDAPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbCDPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbIDPoliceStation.DropDownWidth = drpdwnlength
        Me.cmbACPoliceStation.DropDownWidth = drpdwnlength


    End Sub


    Public Sub ChangeDropdownWidth(ByVal Text As String, ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx)
        On Error Resume Next
        Static Dim length As Integer = cmb.DropDownWidth
        Dim g As Graphics = cmb.CreateGraphics
        Dim newlength As Integer = g.MeasureString(Text, cmb.Font).Width
        If length < newlength Then
            length = newlength
            cmb.DropDownWidth = newlength
        End If

    End Sub
#End Region


#Region "LOAD MODUS OPERANDI"

    Private Sub LoadModusOperandi()
        Try
            Me.cmbSOCModus.Items.Clear()
            For Each line As String In System.IO.File.ReadLines(strAppUserPath & "\WordTemplates\ModusOperandi-PreDefined.txt")
                If line.Trim <> "" Then
                    Me.cmbSOCModus.Items.Add(line)
                    ChangeDropdownWidth(line, cmbSOCModus)
                End If
            Next

            For Each line As String In System.IO.File.ReadLines(strAppUserPath & "\WordTemplates\ModusOperandi-UserDefined.txt")
                If line.Trim <> "" Then
                    Me.cmbSOCModus.Items.Add(line)
                    ChangeDropdownWidth(line, cmbSOCModus)
                End If
            Next

            Me.cmbSOCModus.DropDownStyle = ComboBoxStyle.DropDown
            Me.cmbSOCModus.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Dim drpdwnlength As Integer = Me.cmbSOCModus.Width
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUserDefinedModusOperandi_Click(sender As Object, e As EventArgs) Handles btnUserDefinedModusOperandi.Click
        Try
            Dim MOFile As String = strAppUserPath & "\WordTemplates\ModusOperandi-UserDefined.txt"
            If Not FileIO.FileSystem.FileExists(MOFile) Then
                System.IO.File.Create(MOFile).Dispose()
            End If

            Shell("explorer.exe " & MOFile, AppWinStyle.MaximizedFocus)

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "SAVE SETTINGS"


    Private Sub HideFieldsAtStartupStatus() Handles chkHideDataEntryFieldsAtStartup.Click
        On Error Resume Next
        Dim s As Boolean = chkHideDataEntryFieldsAtStartup.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "HideFields", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub


    Private Sub UseTIinLetter(sender As Object, e As EventArgs) Handles chkUseTIAtBottomOfLetter.Click
        On Error Resume Next
        blUseTIinLetter = chkUseTIAtBottomOfLetter.Checked
        Dim v As Integer
        If blUseTIinLetter Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "UseTIinLetter", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub



    Private Sub SearchSettings() Handles rdoAnyWhere.Click, rdoBeginsWith.Click, rdoFullText.Click
        On Error Resume Next
        Dim v As Integer

        If rdoBeginsWith.Checked Then v = 1
        If rdoFullText.Checked Then v = 2
        If rdoAnyWhere.Checked Then v = 3

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "SearchSetting", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub LoadRecordsAtStartup() Handles chkLoadRecordsAtStartup.Click
        On Error Resume Next
        Dim s As Boolean = chkLoadRecordsAtStartup.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "LoadRecordsAtStartup", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub

    Private Sub LoadCurrentYearRecordsOnlySettings() Handles chkLoadCurrentYearRecordsOnly.Click
        On Error Resume Next
        Dim s As Boolean = chkLoadCurrentYearRecordsOnly.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "LoadCurrentYearRecordsOnly", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub

    Private Sub AutoCapitalize() Handles chkAutoCapitalize.Click
        On Error Resume Next
        Dim s As Boolean = chkAutoCapitalize.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AutoCapitalize", v, Microsoft.Win32.RegistryValueKind.String)
        TemporarilyStopCapitalize = Not Me.chkAutoCapitalize.Checked
        DisplayAutoCapsStatus(s)
    End Sub


    Private Sub IgnoreAllCapsAutoCapitalize() Handles chkIgnoreAllCaps.Click
        On Error Resume Next
        Dim s As Boolean = chkIgnoreAllCaps.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "IgnoreAllCaps", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub


    Private Sub SaveAppendSOCYear() Handles chkAppendSOCYear.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkAppendSOCYear.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AppendSOCYear", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub


    

    Private Sub SaveAppendDAYear() Handles chkAppendDAYear.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkAppendDAYear.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AppendDAYear", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub

    Private Sub SaveAppendCDYear() Handles chkAppendCDYear.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkAppendCDYear.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AppendCDYear", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub


    Private Sub SaveTwoDigitSOCYear() Handles chkSOCTwodigits.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkSOCTwodigits.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TwoDigitSOCYear", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub

    Private Sub SaveTwoDigitDAYear() Handles chkDATwodigits.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkDATwodigits.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TwoDigitDAYear", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub


    Private Sub SaveTwoDigitCDYear() Handles chkCDTwodigits.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkCDTwodigits.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TwoDigitCDYear", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub


    Private Sub ReloadDataAfterSettingsWizardClose()
        Try

            System.Threading.Thread.Sleep(1000)

            strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")
            sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strDatabaseFile
            ConnectToDatabase()


            LoadPSList()

            '    LoadModusOperandi()

            InitializeOfficerTable()
            '   LoadOfficerToMemory()
            LoadOfficerListToTable()
            LoadOfficerListToDropDownMenu()


            '   LoadOfficeSettingsToMemory()
            LoadOfficeSettingsToTextBoxes()

            SetWindowTitle()
            LoadRecordsToAllTablesDependingOnCurrentYearSettings()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub SaveLoadDAtoID() Handles chkIDLoadOtherDetails.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkIDLoadOtherDetails.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "LoadDAtoID", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub


    Private Sub SaveLoadDAtoAC() Handles chkACLoadOtherDetails.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkACLoadOtherDetails.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "LoadDAtoAC", v, Microsoft.Win32.RegistryValueKind.String)


    End Sub


    Private Sub SaveHighlightColor() Handles cmbHighlightColor.SelectedIndexChanged
        On Error Resume Next
        Dim c As Integer = cmbHighlightColor.SelectedIndex + 1
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "HighLightColor", c, Microsoft.Win32.RegistryValueKind.String)
        Me.Highlighter1.FocusHighlightColor = c
        If c = 5 Then
            Me.Highlighter1.ContainerControl = Me.PanelDummy
        Else
            Me.Highlighter1.ContainerControl = Me.TabControl
        End If

    End Sub

    Private Sub BackupAutomaticallySetting() Handles chkTakeAutoBackup.Click
        On Error Resume Next
        Dim s As Boolean = chkTakeAutoBackup.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strBackupSettingsPath, "AutoBackup", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub


#End Region


#Region "QUICK TOOLBAR SETTINGS"
    Private Sub LoadQuickToolBarSettings() ' Load Quick Access Toolbar layout setting from registry
        On Error Resume Next
        Dim layout As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "QTBarLayout", "0,btnNewEntry,btnOpen,btnEdit,btnDelete")

        If layout <> "" And Not layout Is Nothing Then
            RibbonControl1.QatLayout = layout
        End If

        With RibbonControl1.QatFrequentCommands
            .Add(btnNewEntry)
            .Add(btnOpen)
            .Add(btnEdit)
            .Add(btnDelete)
            .Add(btnViewReports)
            .Add(btnReload)
            .Add(btnShowHideFields)
            .Add(btnOnlineBackup)
            .Add(btnLocalBackup)
            .Add(btnAbout)
            .Add(btnExit)
        End With


    End Sub

    Sub SaveQuicktoolbarSettings()
        On Error Resume Next
        If RibbonControl1.QatLayoutChanged Then
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "QTBarLayout", RibbonControl1.QatLayout, Microsoft.Win32.RegistryValueKind.String)
        End If
    End Sub


    Private Sub RibbonControlExpandedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RibbonControl1.ExpandedChanged
        On Error Resume Next
        If Me.RibbonControl1.Expanded = True Then
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "RibbonVisible", 1, Microsoft.Win32.RegistryValueKind.String)
        Else
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "RibbonVisible", 0, Microsoft.Win32.RegistryValueKind.String)
        End If
    End Sub

#End Region


#Region "ALERT MESSAGES SETTINGS"

    Private Sub ShowAlertMessage() Handles chkShowPopups.Click
        On Error Resume Next
        Dim s As Boolean = chkShowPopups.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "ShowPopups", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub PlaySoundOnAlertMessage() Handles chkPlaySound.Click
        On Error Resume Next
        Dim s As Boolean = chkPlaySound.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "PlaySound", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Public Sub SetRandomDesktopAlertColor()
        If Me.btnRandomColor.Checked Then
            Dim rnd = New Random()
            DesktopAlert.AlertColor = rnd.Next(0, 10)
        Else
            DesktopAlert.AlertColor = eDesktopAlertColor.Default
        End If
        Dim c As eDesktopAlertColor = CType(Color.Red.ToArgb, eDesktopAlertColor)
    End Sub

#End Region


#Region "AUTO-COMPLETION SETTINGS"


    Private Sub SetAutoCompleteMode() Handles cmbAutoCompletionMode.SelectedIndexChanged
        On Error Resume Next
        Dim mode As Integer = Me.cmbAutoCompletionMode.SelectedIndex
        Me.cmbSOCModus.AutoCompleteMode = mode
        Me.txtSOCPlace.AutoCompleteMode = mode
        Me.txtSOCSection.AutoCompleteMode = mode
        Me.cmbSOCPoliceStation.AutoCompleteMode = mode
        Me.txtSOCOfficer.AutoCompleteMode = mode
        Me.cmbRSOCPoliceStation.AutoCompleteMode = mode
        Me.cmbRSOCOfficer.AutoCompleteMode = mode
        Me.cmbSOCPhotoReceived.AutoCompleteMode = mode
        Me.txtSOCPhotographer.AutoCompleteMode = mode
        Me.cmbFileStatus.AutoCompleteMode = mode

        Me.cmbCDPoliceStation.AutoCompleteMode = mode
        Me.cmbCDOfficer.AutoCompleteMode = mode

        Me.cmbDASex.AutoCompleteMode = mode
        Me.cmbDAPoliceStation.AutoCompleteMode = mode
        Me.txtDAName.AutoCompleteMode = mode
        Me.txtDASection.AutoCompleteMode = mode
        Me.txtDAFathersName.AutoCompleteMode = mode
        Me.txtDAAliasName.AutoCompleteMode = mode
        Me.txtDAHenryNumerator.AutoCompleteMode = mode
        Me.txtDAHenryDenominator.AutoCompleteMode = mode

        Me.cmbIDSex.AutoCompleteMode = mode
        Me.cmbIDPoliceStation.AutoCompleteMode = mode
        Me.txtIDName.AutoCompleteMode = mode
        Me.txtIDSection.AutoCompleteMode = mode
        Me.txtIDFathersName.AutoCompleteMode = mode
        Me.txtIDAliasName.AutoCompleteMode = mode
        Me.txtIDHenryNumerator.AutoCompleteMode = mode
        Me.txtIDHenryDenominator.AutoCompleteMode = mode

        Me.cmbACSex.AutoCompleteMode = mode
        Me.cmbACPoliceStation.AutoCompleteMode = mode
        Me.txtACName.AutoCompleteMode = mode
        Me.txtACSection.AutoCompleteMode = mode
        Me.txtACFathersName.AutoCompleteMode = mode
        Me.txtACAliasName.AutoCompleteMode = mode
        Me.txtACModusOperandi.AutoCompleteMode = mode
        Me.txtACHenryNumerator.AutoCompleteMode = mode
        Me.txtACHenryDenominator.AutoCompleteMode = mode

        Me.cmbSHO.AutoCompleteMode = mode
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AutoCompleteMode", mode, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub LoadAutoCompleteMode()
        On Error Resume Next
        Me.cmbAutoCompletionMode.SelectedIndex = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AutoCompleteMode", 1) 'auto completion mode
    End Sub


    Private Sub LoadAutoTextFromDBStatus() Handles chkLoadAutoTextAtStartup.Click
        On Error Resume Next
        Dim s As Boolean = chkLoadAutoTextAtStartup.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "LoadAutoTextFromDB", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub LoadAutoTextFromDB()
        On Error Resume Next

        ClearAutoCompletionTexts()

        IncrementCircularProgress(1)

        '------------------------------------SOC------------------------------------


        Me.SOCRegisterAutoTextTableAdapter.FillByMO(FingerPrintDataSet.SOCRegisterAutoText)

        Dim socmodus As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.SOCRegisterAutoText.Count - 1
            socmodus.Add(FingerPrintDataSet.SOCRegisterAutoText(i).ModusOperandi)
        Next (i)
        Me.cmbSOCModus.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.cmbSOCModus.AutoCompleteCustomSource = socmodus

        Me.SOCRegisterAutoTextTableAdapter.FillBySection(FingerPrintDataSet.SOCRegisterAutoText)

        Dim soclaw As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.SOCRegisterAutoText.Count - 1
            soclaw.Add(FingerPrintDataSet.SOCRegisterAutoText(i).SectionOfLaw)
        Next (i)
        Me.txtSOCSection.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtSOCSection.AutoCompleteCustomSource = soclaw

        IncrementCircularProgress(1)

        '-----------------------------------DA------------------------------------



        Me.DARegisterAutoTextTableAdapter.FillBySection(FingerPrintDataSet.DARegisterAutoText)

        Dim dalaw As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.DARegisterAutoText.Count - 1
            dalaw.Add(FingerPrintDataSet.DARegisterAutoText(i).SectionOfLaw)
        Next (i)

        Me.txtDASection.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDASection.AutoCompleteCustomSource = dalaw

        Me.txtACSection.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACSection.AutoCompleteCustomSource = dalaw

        Me.txtIDSection.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDSection.AutoCompleteCustomSource = dalaw

        IncrementCircularProgress(1)

        Me.DARegisterAutoTextTableAdapter.FillByName(FingerPrintDataSet.DARegisterAutoText)

        Dim daname As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.DARegisterAutoText.Count - 1
            daname.Add(FingerPrintDataSet.DARegisterAutoText(i).Name)
        Next (i)
        Me.txtDAName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDAName.AutoCompleteCustomSource = daname

        Me.txtACName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACName.AutoCompleteCustomSource = daname

        Me.txtIDName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDName.AutoCompleteCustomSource = daname

        IncrementCircularProgress(1)

        Me.DARegisterAutoTextTableAdapter.FillByAlias(FingerPrintDataSet.DARegisterAutoText)

        Dim daalias As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.DARegisterAutoText.Count - 1
            daalias.Add(FingerPrintDataSet.DARegisterAutoText(i).AliasName)
        Next (i)

        IncrementCircularProgress(1)

        Me.txtDAAliasName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDAAliasName.AutoCompleteCustomSource = daalias

        Me.txtACAliasName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACAliasName.AutoCompleteCustomSource = daalias

        Me.txtIDAliasName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDAliasName.AutoCompleteCustomSource = daalias

        IncrementCircularProgress(1)

        Me.DARegisterAutoTextTableAdapter.FillByFather(FingerPrintDataSet.DARegisterAutoText)
        Dim dafather As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.DARegisterAutoText.Count - 1
            dafather.Add(FingerPrintDataSet.DARegisterAutoText(i).FathersName)
        Next (i)
        Me.txtDAFathersName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDAFathersName.AutoCompleteCustomSource = dafather

        Me.txtACFathersName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACFathersName.AutoCompleteCustomSource = dafather

        Me.txtIDFathersName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDFathersName.AutoCompleteCustomSource = dafather

        IncrementCircularProgress(1)

        Me.DARegisterAutoTextTableAdapter.FillByModus(FingerPrintDataSet.DARegisterAutoText)
        Dim damo As New AutoCompleteStringCollection

        For i As Long = 0 To FingerPrintDataSet.DARegisterAutoText.Count - 1
            damo.Add(FingerPrintDataSet.DARegisterAutoText(i).ModusOperandi)
        Next (i)

        Me.txtDAModusOperandi.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDAModusOperandi.AutoCompleteCustomSource = damo

        Me.txtACModusOperandi.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACModusOperandi.AutoCompleteCustomSource = damo

        Me.txtIDModusOperandi.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDModusOperandi.AutoCompleteCustomSource = damo

        IncrementCircularProgress(1)


        '------------------------------------ID------------------------------------


        Me.IDRegisterAutoTextTableAdapter.FillBySection(FingerPrintDataSet.IDRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.IDRegisterAutoText.Count - 1
            dalaw.Add(FingerPrintDataSet.IDRegisterAutoText(i).SectionOfLaw)
        Next (i)

        IncrementCircularProgress(1)

        Me.IDRegisterAutoTextTableAdapter.FillByName(FingerPrintDataSet.IDRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.IDRegisterAutoText.Count - 1
            daname.Add(FingerPrintDataSet.IDRegisterAutoText(i).Name)
        Next (i)

        IncrementCircularProgress(1)

        Me.IDRegisterAutoTextTableAdapter.FillByAlias(FingerPrintDataSet.IDRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.IDRegisterAutoText.Count - 1
            daalias.Add(FingerPrintDataSet.IDRegisterAutoText(i).AliasName)
        Next (i)

        IncrementCircularProgress(1)

        Me.IDRegisterAutoTextTableAdapter.FillByFather(FingerPrintDataSet.IDRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.IDRegisterAutoText.Count - 1
            dafather.Add(FingerPrintDataSet.IDRegisterAutoText(i).FathersName)
        Next (i)

        IncrementCircularProgress(1)

        Me.IDRegisterAutoTextTableAdapter.FillByModus(FingerPrintDataSet.IDRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.IDRegisterAutoText.Count - 1
            damo.Add(FingerPrintDataSet.IDRegisterAutoText(i).ModusOperandi)
        Next (i)

        IncrementCircularProgress(1)

        '------------------------------------AC------------------------------------



        Me.ACRegisterAutoTextTableAdapter.FillBySection(FingerPrintDataSet.ACRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.ACRegisterAutoText.Count - 1
            dalaw.Add(FingerPrintDataSet.ACRegisterAutoText(i).SectionOfLaw)
        Next (i)


        Me.ACRegisterAutoTextTableAdapter.FillByName(FingerPrintDataSet.ACRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.ACRegisterAutoText.Count - 1
            daname.Add(FingerPrintDataSet.ACRegisterAutoText(i).Name)
        Next (i)

        IncrementCircularProgress(1)

        Me.ACRegisterAutoTextTableAdapter.FillByAlias(FingerPrintDataSet.ACRegisterAutoText)
        For i As Long = 0 To FingerPrintDataSet.ACRegisterAutoText.Count - 1
            daalias.Add(FingerPrintDataSet.ACRegisterAutoText(i).AliasName)
        Next (i)


        Me.ACRegisterAutoTextTableAdapter.FillByFather(FingerPrintDataSet.ACRegisterAutoText)

        For i As Long = 0 To FingerPrintDataSet.ACRegisterAutoText.Count - 1
            dafather.Add(FingerPrintDataSet.ACRegisterAutoText(i).FathersName)
        Next (i)

        IncrementCircularProgress(1)

        Me.ACRegisterAutoTextTableAdapter.FillByModus(FingerPrintDataSet.ACRegisterAutoText)

        For i As Long = 0 To FingerPrintDataSet.ACRegisterAutoText.Count - 1
            damo.Add(FingerPrintDataSet.ACRegisterAutoText(i).ModusOperandi)
        Next (i)

        IncrementCircularProgress(1)

    End Sub


    Private Sub ClearAutoCompletionTexts()
        On Error Resume Next
        For Each ctrl In Me.Controls 'clear all textboxes
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then ctrl.AutoCompleteCustomSource.Clear()
        Next
    End Sub


    Private Sub AddTextsToAutoCompletionList() 'when u save and search 
        On Error Resume Next
        If CurrentTab = "SOC" Then
            '   If Trim(Me.txtSOCModus.Text) <> vbNullString Then Me.txtSOCModus.AutoCompleteCustomSource.Add(Trim(Me.txtSOCModus.Text))
            If Trim(Me.txtSOCPlace.Text) <> vbNullString Then Me.txtSOCPlace.AutoCompleteCustomSource.Add(Trim(Me.txtSOCPlace.Text))
            If Trim(Me.txtSOCSection.Text) <> vbNullString Then Me.txtSOCSection.AutoCompleteCustomSource.Add(Trim(Me.txtSOCSection.Text))
            If Trim(Me.txtSOCPhotographer.Text) <> vbNullString Then Me.txtSOCPhotographer.AutoCompleteCustomSource.Add(Trim(Me.txtSOCPhotographer.Text))
        End If


        If CurrentTab = "DA" Then
            If Trim(Me.txtDAName.Text) <> vbNullString Then Me.txtDAName.AutoCompleteCustomSource.Add(Trim(Me.txtDAName.Text))
            If Trim(Me.txtDAAliasName.Text) <> vbNullString Then Me.txtDAAliasName.AutoCompleteCustomSource.Add(Trim(Me.txtDAAliasName.Text))
            If Trim(Me.txtDASection.Text) <> vbNullString Then Me.txtDASection.AutoCompleteCustomSource.Add(Trim(Me.txtDASection.Text))
            If Trim(Me.txtDAFathersName.Text) <> vbNullString Then Me.txtDAFathersName.AutoCompleteCustomSource.Add(Trim(Me.txtDAFathersName.Text))
            If Trim(Me.txtDAModusOperandi.Text) <> vbNullString Then Me.txtDAModusOperandi.AutoCompleteCustomSource.Add(Trim(Me.txtDAModusOperandi.Text))

        End If

        If CurrentTab = "ID" Then
            If Trim(Me.txtIDName.Text) <> vbNullString Then Me.txtIDName.AutoCompleteCustomSource.Add(Trim(Me.txtIDName.Text))
            If Trim(Me.txtIDAliasName.Text) <> vbNullString Then Me.txtIDAliasName.AutoCompleteCustomSource.Add(Trim(Me.txtIDAliasName.Text))
            If Trim(Me.txtIDSection.Text) <> vbNullString Then Me.txtIDSection.AutoCompleteCustomSource.Add(Trim(Me.txtIDSection.Text))
            If Trim(Me.txtIDFathersName.Text) <> vbNullString Then Me.txtIDFathersName.AutoCompleteCustomSource.Add(Trim(Me.txtIDFathersName.Text))
            If Trim(Me.txtIDModusOperandi.Text) <> vbNullString Then Me.txtIDModusOperandi.AutoCompleteCustomSource.Add(Trim(Me.txtIDModusOperandi.Text))

        End If

        If CurrentTab = "AC" Then
            If Trim(Me.txtACName.Text) <> vbNullString Then Me.txtACName.AutoCompleteCustomSource.Add(Trim(Me.txtACName.Text))
            If Trim(Me.txtACAliasName.Text) <> vbNullString Then Me.txtACAliasName.AutoCompleteCustomSource.Add(Trim(Me.txtACAliasName.Text))
            If Trim(Me.txtACSection.Text) <> vbNullString Then Me.txtACSection.AutoCompleteCustomSource.Add(Trim(Me.txtACSection.Text))
            If Trim(Me.txtACFathersName.Text) <> vbNullString Then Me.txtACFathersName.AutoCompleteCustomSource.Add(Trim(Me.txtACFathersName.Text))
            If Trim(Me.txtACModusOperandi.Text) <> vbNullString Then Me.txtACModusOperandi.AutoCompleteCustomSource.Add(Trim(Me.txtACModusOperandi.Text))

        End If

    End Sub


    Private Sub LoadDAHenryClassAutoText() Handles txtDAHenryNumerator.GotFocus
        On Error Resume Next
        Me.DARegisterAutoTextTableAdapter.FillByClassification(FingerPrintDataSet.DARegisterAutoText, Me.txtDAName.Text, Me.txtDAFathersName.Text)
        Me.txtDAHenryDenominator.AutoCompleteCustomSource.Clear()
        Me.txtDAHenryNumerator.AutoCompleteCustomSource.Clear()
        Dim hnumerator As New AutoCompleteStringCollection
        Dim hdenominator As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.DARegisterAutoText.Count - 1
            hnumerator.Add(FingerPrintDataSet.DARegisterAutoText(i).HenryNumerator)
            hdenominator.Add(FingerPrintDataSet.DARegisterAutoText(i).HenryDenominator)
        Next
        Me.txtDAHenryNumerator.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDAHenryNumerator.AutoCompleteCustomSource = hnumerator

        Me.txtDAHenryDenominator.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtDAHenryDenominator.AutoCompleteCustomSource = hdenominator

    End Sub

    Private Sub LoadIDHenryClassAutoText() Handles txtIDHenryNumerator.GotFocus
        On Error Resume Next
        Me.IDRegisterAutoTextTableAdapter.FillByClassification(FingerPrintDataSet.IDRegisterAutoText, Me.txtIDName.Text, Me.txtIDFathersName.Text)
        Me.txtIDHenryDenominator.AutoCompleteCustomSource.Clear()
        Me.txtIDHenryNumerator.AutoCompleteCustomSource.Clear()
        Dim hnumerator As New AutoCompleteStringCollection
        Dim hdenominator As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.IDRegisterAutoText.Count - 1
            hnumerator.Add(FingerPrintDataSet.IDRegisterAutoText(i).HenryNumerator)
            hdenominator.Add(FingerPrintDataSet.IDRegisterAutoText(i).HenryDenominator)
        Next
        Me.txtIDHenryNumerator.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDHenryNumerator.AutoCompleteCustomSource = hnumerator

        Me.txtIDHenryDenominator.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtIDHenryDenominator.AutoCompleteCustomSource = hdenominator
    End Sub

    Private Sub LoadACHenryClassAutoText() Handles txtACHenryNumerator.GotFocus
        On Error Resume Next
        Me.ACRegisterAutoTextTableAdapter.FillByClassification(FingerPrintDataSet.ACRegisterAutoText, Me.txtACName.Text, Me.txtACFathersName.Text)
        Me.txtACHenryDenominator.AutoCompleteCustomSource.Clear()
        Me.txtACHenryNumerator.AutoCompleteCustomSource.Clear()
        Dim hnumerator As New AutoCompleteStringCollection
        Dim hdenominator As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet.ACRegisterAutoText.Count - 1
            hnumerator.Add(FingerPrintDataSet.ACRegisterAutoText(i).HenryNumerator)
            hdenominator.Add(FingerPrintDataSet.ACRegisterAutoText(i).HenryDenominator)
        Next
        Me.txtACHenryNumerator.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACHenryNumerator.AutoCompleteCustomSource = hnumerator

        Me.txtACHenryDenominator.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtACHenryDenominator.AutoCompleteCustomSource = hdenominator
    End Sub


#End Region


#Region "TAB SETTINGS"
    Private Sub SelectedTabChanged(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.TabStripTabChangedEventArgs) Handles TabControl.SelectedTabChanged
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.btnFacingSheetContext.Visible = False
        Me.txtLoadSelectedYearRecords.Visible = False

        Select Case e.NewTab.Name

            Case Me.SOCTabItem.Name
                Me.pnlRegisterName.Text = "Scene of Crime Register"
                CurrentTab = "SOC"
                If Me.PanelSOC.Visible = False Then
                    Me.SOCDatagrid.Focus()
                Else
                    Me.txtSOCNumber.Focus()
                End If

                If PSListChanged Then LoadPSList()
                Me.AcceptButton = btnSaveSOC
                Me.btnFacingSheetContext.Visible = True
                Me.txtLoadSelectedYearRecords.Visible = True
                Me.SOCDatagrid.Cursor = Cursors.Default


            Case Me.RSOCTabItem.Name
                Me.pnlRegisterName.Text = "Scene of Crime Reports Register"
                CurrentTab = "RSOC"
                If Me.PanelRSOC.Visible = False Then
                    Me.RSOCDatagrid.Focus()
                Else
                    Me.txtRSOCSerialNumber.Focus()
                End If

                If PSListChanged Then LoadPSList()
                Me.AcceptButton = btnSaveRSOC
                Me.RSOCDatagrid.Cursor = Cursors.Default
                Me.txtLoadSelectedYearRecords.Visible = True

            Case Me.DATabItem.Name
                Me.pnlRegisterName.Text = "Daily Arrest Slips Register"
                CurrentTab = "DA"

                If Me.PanelDA.Visible = False Then
                    Me.DADatagrid.Focus()
                Else
                    Me.txtDANumber.Focus()
                End If

                If PSListChanged Then LoadPSList()
                Me.AcceptButton = btnSaveDA
                Me.DADatagrid.Cursor = Cursors.Default
                Me.txtLoadSelectedYearRecords.Visible = True

            Case Me.IDTabItem.Name
                Me.pnlRegisterName.Text = "Identified Slips Register"
                CurrentTab = "ID"
                If Me.PanelID.Visible = False Then
                    Me.IDDatagrid.Focus()
                Else
                    Me.txtIDNumber.Focus()
                End If

                If PSListChanged Then LoadPSList()
                Me.AcceptButton = btnSaveID
                Me.IDDatagrid.Cursor = Cursors.Default


            Case Me.ACTabItem.Name
                Me.pnlRegisterName.Text = "Active Criminal Slips Register"
                CurrentTab = "AC"
                If Me.PanelAC.Visible = False Then
                    Me.ACDatagrid.Focus()
                Else
                    Me.txtACNumber.Focus()
                End If

                If PSListChanged Then LoadPSList()
                Me.AcceptButton = btnSaveAC
                Me.ACDatagrid.Cursor = Cursors.Default


            Case Me.FPATabItem.Name
                Me.pnlRegisterName.Text = "Fingerprint Attestation Register"
                CurrentTab = "FPA"
                Me.FPADataGrid.Cursor = Cursors.Default
                Me.txtLoadSelectedYearRecords.Visible = True


            Case Me.PSTabItem.Name
                Me.pnlRegisterName.Text = "List of Police Stations"
                CurrentTab = "PS"
                Me.txtPSName.Focus()
                Me.AcceptButton = btnSavePS
                If PSListChanged Then Me.PSRegisterTableAdapter.Fill(Me.FingerPrintDataSet.PoliceStationList)
                PSListChanged = False
                Me.PSDataGrid.Cursor = Cursors.Default

            Case Me.CDTabItem.Name
                Me.pnlRegisterName.Text = "Court Duty Register"
                CurrentTab = "CD"
                If Me.PanelCD.Visible = False Then
                    Me.CDDataGrid.Focus()
                Else
                    Me.txtCDNumber.Focus()
                End If

                If PSListChanged Then LoadPSList()
                Me.AcceptButton = btnSaveCD
                Me.CDDataGrid.Cursor = Cursors.Default
                Me.txtLoadSelectedYearRecords.Visible = True

            Case Me.IOTabItem.Name
                Me.pnlRegisterName.Text = "List of Officers"
                CurrentTab = "IO"
                Me.lblNumberOfRecords.Visible = False
                Me.lblCurrentYear.Visible = False
                Me.lblCurrentMonth.Visible = False
                Me.txtIOOfficerName.Focus()
                Me.IODatagrid.Cursor = Cursors.Default
                Me.AcceptButton = btnSaveIO

            Case Me.SSTabItem.Name
                Me.pnlRegisterName.Text = "List of Supporting Staff"
                CurrentTab = "SS"
                Me.lblNumberOfRecords.Visible = False
                Me.lblCurrentYear.Visible = False
                Me.lblCurrentMonth.Visible = False
                Me.txtSSName.Focus()
                Me.SSDatagrid.Cursor = Cursors.Default
                Me.AcceptButton = btnSaveSS

            Case Me.OSTabItem.Name
                Me.pnlRegisterName.Text = "Office Settings"
                CurrentTab = "OS"
                Me.lblNumberOfRecords.Visible = False
                Me.lblCurrentYear.Visible = False
                Me.lblCurrentMonth.Visible = False
                Me.txtFullOffice.Focus()
                Me.AcceptButton = btnSaveOfficeSettings

            Case Me.IDRTabItem.Name
                Me.pnlRegisterName.Text = "Identification Register"
                CurrentTab = "IDR"
                Me.JoinedIDRDataGrid.Focus()
                Me.txtLoadSelectedYearRecords.Visible = True

        End Select

        If blPreviewDB Then
            Me.pnlRegisterName.Text += " - Preview Mode"
        End If

        DisplayDatabaseInformation()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "STATUSBAR TEXTS"
    Sub DisplayDatabaseInformation() Handles SOCRegisterBindingSource.PositionChanged, RSOCRegisterBindingSource.PositionChanged, DARegisterBindingSource.PositionChanged, PSRegisterBindingSource.PositionChanged, FPARegisterBindingSource.PositionChanged, ACRegisterBindingSource.PositionChanged, IDRegisterBindingSource.PositionChanged, CDRegisterBindingSource.PositionChanged, JoinedIDRBindingSource.PositionChanged, SSBindingSource.PositionChanged
        On Error Resume Next
        If ShowStatusTexts = False Then Exit Sub
        Me.lblHasFPSlip.Visible = False
        Me.lblReportSent.Visible = False
        Dim m As String = Month(Today)
        Dim y As String = Year(Today)

        Dim d1 As Date = New Date(y, m, 1)
        Dim d As Integer = Date.DaysInMonth(y, m)
        Dim d2 As Date = New Date(y, m, d)

        Dim y1 As Date = New Date(y, 1, 1)
        Dim y2 As Date = New Date(y, 12, 31)

        Me.lblCurrentMonth.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.lblCurrentYear.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        If CurrentTab = "SOC" Then

            lblCurrentMonth.Visible = True
            lblCurrentYear.Visible = True
            Me.lblNumberOfRecords.Visible = True

            If Not blApplicationIsLoading And Not blApplicationIsRestoring And SOCDatagrid.RowCount > 0 Then
                If Me.SocReportRegisterTableAdapter1.ScalarQueryReportSent(Me.SOCDatagrid.SelectedCells(0).Value.ToString) > 0 Then
                    Me.lblReportSent.Visible = True
                Else
                    Me.lblReportSent.Visible = False
                End If
            End If


            Me.lblRegisterNameStatusBar.Text = "SOC Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.SOCRegisterBindingSource.Count


            Dim yearsoccount = Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(y1, y2)
            Dim yearcpcount = Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(y1, y2)
            Dim yearcpunfit = Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(y1, y2)
            Dim yearcpelim = Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(y1, y2)
            Dim yearcprem = Me.SOCRegisterTableAdapter.ScalarQueryCPsRemainingInDI(y1, y2)

            Me.lblCurrentYear.Text = "This Year : " & yearsoccount

            Dim monthsoccount = Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2)
            Dim monthcpcount = Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2)
            Dim monthcpunfit = Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2)
            Dim monthcpelim = Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2)
            Dim monthcprem = Me.SOCRegisterTableAdapter.ScalarQueryCPsRemainingInDI(d1, d2)
            Me.lblCurrentMonth.Text = "This Month: " & monthsoccount



            If SOCDatagrid.RowCount <> 0 Then
                Dim yr As String = Year(Me.SOCDatagrid.SelectedCells(2).Value.ToString)
                Dim soc As String = Strings.Format(Me.SOCDatagrid.SelectedCells(1).Value, "000")
                Dim Location As String = CPImageImportLocation & yr & "\SOC No. " & soc
                If FileIO.FileSystem.DirectoryExists(Location) Then
                    If FileIO.FileSystem.GetFiles(Location).Count <> 0 Then
                        Me.lblHasFPSlip.Visible = True
                    End If
                End If
            End If

        End If


        If CurrentTab = "RSOC" Then
            lblCurrentMonth.Visible = True
            lblCurrentYear.Visible = True
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "SOC Reports Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.RSOCRegisterBindingSource.Count
            Me.lblCurrentYear.Text = "This Year : " & Me.RSOCRegisterTableAdapter.ScalarQueryReportsSent(y1, y2)
            Me.lblCurrentMonth.Text = "This Month: " & Me.RSOCRegisterTableAdapter.ScalarQueryReportsSent(d1, d2)
        End If


        If CurrentTab = "DA" Then
            lblCurrentMonth.Visible = True
            lblCurrentYear.Visible = True
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "DA Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.DARegisterBindingSource.Count
            Me.lblCurrentYear.Text = "This Year : " & Me.DARegisterTableAdapter.CountDASlip(y1, y2)
            Me.lblCurrentMonth.Text = "This Month: " & Me.DARegisterTableAdapter.CountDASlip(d1, d2)
        End If

        If CurrentTab = "ID" Then
            lblCurrentMonth.Visible = False
            lblCurrentYear.Visible = False
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "Identified Slips Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.IDRegisterBindingSource.Count
        End If


        If CurrentTab = "AC" Then
            lblCurrentMonth.Visible = False
            lblCurrentYear.Visible = False
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "Active Criminals Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.ACRegisterBindingSource.Count
        End If


        If CurrentTab = "FPA" Then
            lblCurrentMonth.Visible = True
            lblCurrentYear.Visible = True
            Me.lblNumberOfRecords.Visible = True
            Me.lblCurrentMonth.Font = New Font("Rupee Foradian", 9, FontStyle.Bold)
            Me.lblCurrentYear.Font = New Font("Rupee Foradian", 9, FontStyle.Bold)

            Me.lblRegisterNameStatusBar.Text = "FP Attestation Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.FPARegisterBindingSource.Count
            Dim yearattested = Me.FPARegisterTableAdapter.AttestedPersonCount(y1, y2)
            Dim yearamount = Val(Me.FPARegisterTableAdapter.AmountRemitted(y1, y2)) & "/-"
            Dim monthattested = Me.FPARegisterTableAdapter.AttestedPersonCount(d1, d2)
            Dim monthamount = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"

            Me.lblCurrentYear.Text = "This Year : " & yearattested & ";   ` " & yearamount
            Me.lblCurrentMonth.Text = "This Month: " & monthattested & ";   ` " & monthamount
        End If

        If CurrentTab = "PS" Then
            lblCurrentMonth.Visible = False
            lblCurrentYear.Visible = False
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "List of Police Stations"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.PSRegisterBindingSource.Count
        End If

        If CurrentTab = "SS" Then
            lblCurrentMonth.Visible = False
            lblCurrentYear.Visible = False
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "List of Supporting Staff"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.SSBindingSource.Count
        End If

        If CurrentTab = "CD" Then
            lblCurrentMonth.Visible = True
            lblCurrentYear.Visible = True
            Me.lblNumberOfRecords.Visible = True
            Me.lblRegisterNameStatusBar.Text = "Court Duty Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.CDRegisterBindingSource.Count
            Me.lblCurrentYear.Text = "This Year : " & Me.CDRegisterTableAdapter.CountCD(y1, y2)
            Me.lblCurrentMonth.Text = "This Month: " & Me.CDRegisterTableAdapter.CountCD(d1, d2)
        End If

        If CurrentTab = "IO" Then
            lblCurrentMonth.Visible = False
            lblCurrentYear.Visible = False
            Me.lblNumberOfRecords.Visible = False
            Me.lblRegisterNameStatusBar.Text = "List of Officers"
        End If

        If CurrentTab = "OS" Then
            lblCurrentMonth.Visible = False
            lblCurrentYear.Visible = False
            Me.lblNumberOfRecords.Visible = False
            Me.lblRegisterNameStatusBar.Text = "Office Settings"
        End If

        If CurrentTab = "IDR" Then

            lblCurrentMonth.Visible = True
            lblCurrentYear.Visible = True
            Me.lblNumberOfRecords.Visible = True

            Me.lblRegisterNameStatusBar.Text = "Identification Register"
            Me.lblNumberOfRecords.Text = "No. of Records found: " & Me.JoinedIDRBindingSource.Count


            Dim yearIDRcount = Me.JoinedIDRTableAdapter.ScalarQuerySOCsIdentified(y1, y2)
            Me.lblCurrentYear.Text = "This Year : " & yearIDRcount

            Dim monthIDRcount = Me.JoinedIDRTableAdapter.ScalarQuerySOCsIdentified(d1, d2)
            Me.lblCurrentMonth.Text = "This Month: " & monthIDRcount

        End If

        Me.StatusBar.RecalcLayout()
    End Sub

    Private Sub DisplayTime() Handles Timer1.Tick
        On Error Resume Next
        Me.lblTime.Text = Format(Now, "dd/MM/yyyy h:mm:ss tt")

    End Sub

    Private Sub ShowTimeControlPanel() Handles lblTime.Click
        On Error Resume Next
        Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you want to open the Date and Time Control Panel?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If r = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Shell("explorer.exe " & Environment.SystemDirectory & "\timedate.cpl", AppWinStyle.NormalFocus)

    End Sub
#End Region


#Region "DATAGRID SETTINGS"

    Public Sub SetDatagridFont()
        On Error Resume Next
        Me.SOCDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.SOCDatagrid.Columns(0).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.SOCDatagrid.Columns(17).CellTemplate.Style.Font = New Font("Rupee Foradian", 9, FontStyle.Regular)
        Me.SOCDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        Me.RSOCDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.RSOCDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.RSOCDatagrid.Columns(1).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        Me.DADatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.DADatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.DADatagrid.Columns(0).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        Me.PSDataGrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.PSDataGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)


        Me.FPADataGrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.FPADataGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.FPADataGrid.Columns(0).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        Me.CDDataGrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.CDDataGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.CDDataGrid.Columns(0).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        Me.IDDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.IDDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        Me.ACDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.ACDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        Me.IODatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.IODatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.IODatagrid.Columns(0).DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.IODatagrid.Columns(6).DisplayIndex = 1

        Me.JoinedIDRDataGrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.JoinedIDRDataGrid.Columns(0).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.JoinedIDRDataGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.JoinedIDRDataGrid.Columns(22).DisplayIndex = 19

        Me.SSDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.SSDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
    End Sub

    Private Sub FreezeNumberColumns()
        On Error Resume Next
        Me.SOCDatagrid.Columns(0).Frozen = True
        Me.RSOCDatagrid.Columns(1).Frozen = True
        Me.DADatagrid.Columns(0).Frozen = True
        Me.IDDatagrid.Columns(1).Frozen = True
        Me.ACDatagrid.Columns(1).Frozen = True
        Me.FPADataGrid.Columns(0).Frozen = True
        Me.CDDataGrid.Columns(0).Frozen = True
        Me.JoinedIDRDataGrid.Columns(1).Frozen = True
    End Sub

    Private Sub ShowAllDataEntryFields(ByVal Show As Boolean)
        On Error Resume Next

        Me.PanelSOC.Visible = Show
        Application.DoEvents()
        Me.PanelRSOC.Visible = Show
        Me.PanelDA.Visible = Show
        Me.PanelID.Visible = Show
        Me.PanelAC.Visible = Show
        Me.PanelCD.Visible = Show

        ShowAllFields = Not Show

        If Show = True Then
            btnHideAllDataEntryFields.Text = "Hide All Data Entry Fields"
        Else
            btnHideAllDataEntryFields.Text = "Show All Data Entry Fields"
        End If
    End Sub

    Private Sub Datagrid_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles SOCDatagrid.CellMouseLeave, DADatagrid.CellMouseLeave, JoinedIDRDataGrid.CellMouseLeave, FPADataGrid.CellMouseLeave, CDDataGrid.CellMouseLeave, RSOCDatagrid.CellMouseLeave

        On Error Resume Next
        Select Case DirectCast(sender, Control).Name
            Case SOCDatagrid.Name
                Me.lblSOCGridInfo.Visible = False
            Case DADatagrid.Name
                Me.lblDAGridInfo.Visible = False
            Case JoinedIDRDataGrid.Name
                Me.lblIDRGridInfo.Visible = False
            Case FPADataGrid.Name
                Me.lblFPAGridInfo.Visible = False
            Case CDDataGrid.Name
                Me.lblCDGridInfo.Visible = False
            Case RSOCDatagrid.Name
                Me.lblRSOCGridInfo.Visible = False
        End Select

    End Sub


    Private Sub Datagrid_MouseMove(sender As Object, e As MouseEventArgs) Handles SOCDatagrid.MouseMove, DADatagrid.MouseMove, JoinedIDRDataGrid.MouseMove, FPADataGrid.MouseMove, CDDataGrid.MouseMove, RSOCDatagrid.MouseMove

        On Error Resume Next
        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        Select Case DirectCast(sender, Control).Name
            Case SOCDatagrid.Name
                Me.lblSOCGridInfo.Location = New Point(Me.lblSOCGridInfo.Location.X, e.Y)
            Case DADatagrid.Name
                Me.lblDAGridInfo.Location = New Point(Me.lblDAGridInfo.Location.X, e.Y)
            Case JoinedIDRDataGrid.Name
                Me.lblIDRGridInfo.Location = New Point(Me.lblIDRGridInfo.Location.X, e.Y)
            Case FPADataGrid.Name
                Me.lblFPAGridInfo.Location = New Point(Me.lblFPAGridInfo.Location.X, e.Y)
            Case CDDataGrid.Name
                Me.lblCDGridInfo.Location = New Point(Me.lblCDGridInfo.Location.X, e.Y)
            Case RSOCDatagrid.Name
                Me.lblRSOCGridInfo.Location = New Point(Me.lblRSOCGridInfo.Location.X, e.Y)
        End Select

    End Sub

    Private Sub Datagrid_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles SOCDatagrid.CellMouseEnter, DADatagrid.CellMouseEnter, JoinedIDRDataGrid.CellMouseEnter, FPADataGrid.CellMouseEnter, CDDataGrid.CellMouseEnter, RSOCDatagrid.CellMouseEnter

        On Error Resume Next
        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        Select Case DirectCast(sender, Control).Name
            Case SOCDatagrid.Name
                If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Me.lblSOCGridInfo.Visible = False

                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    Me.lblSOCGridInfo.Visible = True
                    Dim dt As Date = Me.SOCDatagrid.Rows(e.RowIndex).Cells(2).Value
                    Me.lblSOCGridInfo.Text = Strings.Format(dt, "MMM yyyy")
                End If
            Case DADatagrid.Name
                If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Me.lblDAGridInfo.Visible = False
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    Me.lblDAGridInfo.Visible = True
                    Dim dt As Date = Me.DADatagrid.Rows(e.RowIndex).Cells(2).Value
                    Me.lblDAGridInfo.Text = Strings.Format(dt, "MMM yyyy")
                End If
            Case JoinedIDRDataGrid.Name
                If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Me.lblIDRGridInfo.Visible = False
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    Me.lblIDRGridInfo.Visible = True
                    Dim dt As Date = Me.JoinedIDRDataGrid.Rows(e.RowIndex).Cells(2).Value
                    Me.lblIDRGridInfo.Text = Strings.Format(dt, "MMM yyyy")
                End If
            Case FPADataGrid.Name
                If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Me.lblFPAGridInfo.Visible = False
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    Me.lblFPAGridInfo.Visible = True
                    Dim dt As Date = Me.FPADataGrid.Rows(e.RowIndex).Cells(2).Value
                    Me.lblFPAGridInfo.Text = Strings.Format(dt, "MMM yyyy")
                End If
            Case CDDataGrid.Name
                If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Me.lblCDGridInfo.Visible = False
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    Me.lblCDGridInfo.Visible = True
                    Dim dt As Date = Me.CDDataGrid.Rows(e.RowIndex).Cells(2).Value
                    Me.lblCDGridInfo.Text = Strings.Format(dt, "MMM yyyy")
                End If
            Case RSOCDatagrid.Name
                If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Me.lblRSOCGridInfo.Visible = False
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    Me.lblRSOCGridInfo.Visible = True
                    Dim dt As Date = Me.RSOCDatagrid.Rows(e.RowIndex).Cells(8).Value
                    Me.lblRSOCGridInfo.Text = Strings.Format(dt, "MMM yyyy")
                End If
        End Select


    End Sub
    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles SOCDatagrid.CellPainting, RSOCDatagrid.CellPainting, DADatagrid.CellPainting, FPADataGrid.CellPainting, PSDataGrid.CellPainting, CDDataGrid.CellPainting, IDDatagrid.CellPainting, ACDatagrid.CellPainting, IODatagrid.CellPainting, JoinedIDRDataGrid.CellPainting, SSDatagrid.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 10, FontStyle.Bold)
        sf.LineAlignment = StringAlignment.Center
        Using b As SolidBrush = New SolidBrush(Me.ForeColor)
            If e.ColumnIndex < 0 AndAlso e.RowIndex < 0 Then
                e.Graphics.DrawString("Sl.No", f, b, e.CellBounds, sf)
                e.Handled = True
            End If

            If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
                e.Graphics.DrawString((e.RowIndex + 1).ToString, f, b, e.CellBounds, sf)
                e.Handled = True
            End If
        End Using

    End Sub

    Private Sub HandleDatagridDataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles SOCDatagrid.DataError, RSOCDatagrid.DataError, DADatagrid.DataError, IDDatagrid.DataError, ACDatagrid.DataError, FPADataGrid.DataError, ACDatagrid.DataError, PSDataGrid.DataError
        On Error Resume Next
        e.Cancel = True
        ' MessageBoxEx.Show(e.Exception.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    '-----------------------------------------Save Default Column Width-------------



    Private Sub SaveSOCDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\SOCDatagrid"
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), SOCDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveRSOCDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\RSOCDatagrid"
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), RSOCDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveDADatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\DADatagrid"
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), DADatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next

    End Sub


    Private Sub SaveFPADatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\FPADatagrid"
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), FPADataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next

    End Sub


    Private Sub SaveCDDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\CDDatagrid"
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), CDDataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next

    End Sub


    Private Sub SavePSDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\PSDatagrid"
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), PSDataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next

    End Sub


    Private Sub SaveIDDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\IDDatagrid"
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), IDDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next

    End Sub


    Private Sub SaveACDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\ACDatagrid"
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), ACDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveIDRDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\JoinedIDRDatagrid"
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "DefaultWidth" & Format(i, "00"), JoinedIDRDataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    '-----------------------------------------Load Default Column Width-------------


    Private Sub LoadSOCDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\SOCDatagrid"
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            SOCDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), SOCDatagrid.Columns(i).Width)
        Next
        SOCDatagrid.RowHeadersWidth = 50
    End Sub

    Private Sub LoadRSOCDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\RSOCDatagrid"
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            RSOCDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), RSOCDatagrid.Columns(i).Width)
        Next
        RSOCDatagrid.RowHeadersWidth = 50
    End Sub


    Private Sub LoadDADatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\DADatagrid"
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            DADatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), DADatagrid.Columns(i).Width)
        Next
        DADatagrid.RowHeadersWidth = 50
    End Sub


    Private Sub LoadFPADatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\FPADatagrid"
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            FPADataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), FPADataGrid.Columns(i).Width)
        Next
        FPADataGrid.RowHeadersWidth = 50
    End Sub


    Private Sub LoadCDDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\CDDatagrid"
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            CDDataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), CDDataGrid.Columns(i).Width)
        Next
        CDDataGrid.RowHeadersWidth = 50
    End Sub


    Private Sub LoadPSDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\PSDatagrid"
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            PSDataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), PSDataGrid.Columns(i).Width)
        Next
        PSDataGrid.RowHeadersWidth = 40
    End Sub


    Private Sub LoadIDDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\IDDatagrid"
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            IDDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), IDDatagrid.Columns(i).Width)
        Next
        IDDatagrid.RowHeadersWidth = 50
    End Sub


    Private Sub LoadACDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\ACDatagrid"
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            ACDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), ACDatagrid.Columns(i).Width)
        Next
        ACDatagrid.RowHeadersWidth = 50
    End Sub

    Private Sub LoadIDRDatagridColumnDefaultWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\JoinedIDRDatagrid"
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            JoinedIDRDataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "DefaultWidth" & Format(i, "00"), JoinedIDRDataGrid.Columns(i).Width)
        Next
        JoinedIDRDataGrid.RowHeadersWidth = 50
    End Sub


    '-----------------------------------------Load Default Column Width ALL Tables-------------

    Private Sub LoadDefaultColumnWidthsOfAllTables() Handles btnResetColumnWidthOfAllTables.Click
        On Error Resume Next
        Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("This action will reset the column widths of all tables. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        LoadSOCDatagridColumnDefaultWidth()
        LoadRSOCDatagridColumnDefaultWidth()
        LoadDADatagridColumnDefaultWidth()
        LoadIDDatagridColumnDefaultWidth()
        LoadACDatagridColumnDefaultWidth()
        LoadFPADatagridColumnDefaultWidth()
        LoadCDDatagridColumnDefaultWidth()
        LoadPSDatagridColumnDefaultWidth()
        LoadIDRDatagridColumnDefaultWidth()

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        ShowDesktopAlert("Column widths of all tables set to default widths!")
    End Sub


    Private Sub LoadDefaultColumnWidthsOfCurrentTable() Handles btnResetColumWidth.Click, btnResetColumnWidthMenu.Click
        On Error Resume Next
        Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("This action will reset the column widths of the current table. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Select Case CurrentTab
            Case "SOC"
                LoadSOCDatagridColumnDefaultWidth()
            Case "RSOC"
                LoadRSOCDatagridColumnDefaultWidth()
            Case "DA"
                LoadDADatagridColumnDefaultWidth()
            Case "ID"
                LoadIDDatagridColumnDefaultWidth()
            Case "AC"
                LoadACDatagridColumnDefaultWidth()
            Case "FPA"
                LoadFPADatagridColumnDefaultWidth()
            Case "CD"
                LoadCDDatagridColumnDefaultWidth()
            Case "PS"
                LoadPSDatagridColumnDefaultWidth()
            Case "IDR"
                LoadIDRDatagridColumnDefaultWidth()
        End Select

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        ShowDesktopAlert("Column widths of the current table set to default widths!")
    End Sub

    '-----------------------------------------Save Changed Column Width-------------



    Private Sub SaveSOCDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\SOCDatagrid"
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), SOCDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", SOCDatagrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SaveRSOCDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\RSOCDatagrid"
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), RSOCDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", RSOCDatagrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SaveDADatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\DADatagrid"
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), DADatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", DADatagrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SaveFPADatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\FPADatagrid"
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), FPADataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", FPADataGrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SaveCDDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\CDDatagrid"
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), CDDataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", CDDataGrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SavePSDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\PSDatagrid"
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), PSDataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", PSDataGrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SaveIDDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\IDDatagrid"
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), IDDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", IDDatagrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub SaveACDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\ACDatagrid"
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), ACDatagrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", ACDatagrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub SaveIDRDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\JoinedIDRDatagrid"
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Width" & Format(i, "00"), JoinedIDRDataGrid.Columns(i).Width.ToString, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(p, "RHWidth", JoinedIDRDataGrid.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)
    End Sub




    '-----------------------------------------Load Changed Column Width-------------


    Private Sub LoadSOCDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\SOCDatagrid"
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            SOCDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), SOCDatagrid.Columns(i).Width)
        Next
        SOCDatagrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)
    End Sub


    Private Sub LoadRSOCDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\RSOCDatagrid"
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            RSOCDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), RSOCDatagrid.Columns(i).Width)
        Next
        RSOCDatagrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)
    End Sub


    Private Sub LoadDADatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\DADatagrid"
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            DADatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), DADatagrid.Columns(i).Width)
        Next
        DADatagrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)

    End Sub


    Private Sub LoadFPADatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\FPADatagrid"
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            FPADataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), FPADataGrid.Columns(i).Width)
        Next
        FPADataGrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)

    End Sub


    Private Sub LoadCDDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\CDDatagrid"
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            CDDataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), CDDataGrid.Columns(i).Width)
        Next
        CDDataGrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)

    End Sub


    Private Sub LoadPSDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\PSDatagrid"
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            PSDataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), PSDataGrid.Columns(i).Width)
        Next
        PSDataGrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 40)

    End Sub


    Private Sub LoadIDDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\IDDatagrid"
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            IDDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), IDDatagrid.Columns(i).Width)
        Next
        IDDatagrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)

    End Sub


    Private Sub LoadACDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\ACDatagrid"
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            ACDatagrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), ACDatagrid.Columns(i).Width)
        Next
        ACDatagrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)

    End Sub


    Private Sub LoadIDRDatagridColumnWidth()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\JoinedIDRDatagrid"
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            JoinedIDRDataGrid.Columns(i).Width = My.Computer.Registry.GetValue(p, "Width" & Format(i, "00"), JoinedIDRDataGrid.Columns(i).Width)
        Next
        JoinedIDRDataGrid.RowHeadersWidth = My.Computer.Registry.GetValue(p, "RHWidth", 60)
    End Sub




    '----------------------------------------------Save Column Ordering -------------------------


    Private Sub SaveSOCDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\SOCDatagrid"
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), SOCDatagrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveRSOCDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\RSOCDatagrid"
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), RSOCDatagrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveDADatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\DADatagrid"
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), DADatagrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveIDDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\IDDatagrid"
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), IDDatagrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveACDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\ACDatagrid"
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), ACDatagrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveFPADatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\FPADatagrid"
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), FPADataGrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SaveCDDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\CDDatagrid"
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), CDDataGrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub SavePSDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\PSDatagrid"
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), PSDataGrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub

    Private Sub SaveIDRDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\JoinedIDRDatagrid"
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            My.Computer.Registry.SetValue(p, "Order" & Format(i, "00"), JoinedIDRDataGrid.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub



    '------------------------------------------------------Load Column Orders--------------------------

    Private Sub LoadSOCDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\SOCDatagrid"
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            SOCDatagrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), SOCDatagrid.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadRSOCDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\RSOCDatagrid"
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            RSOCDatagrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), RSOCDatagrid.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadDADatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\DADatagrid"
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            DADatagrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), DADatagrid.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadIDDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\IDDatagrid"
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            IDDatagrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), IDDatagrid.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadACDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\ACDatagrid"
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            ACDatagrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), ACDatagrid.Columns(i).DisplayIndex)
        Next
    End Sub

    Private Sub LoadFPADatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\FPADatagrid"
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            FPADataGrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), FPADataGrid.Columns(i).DisplayIndex)
        Next
    End Sub

    Private Sub LoadCDDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\CDDatagrid"
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            CDDataGrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), CDDataGrid.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadPSDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\PSDatagrid"
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            PSDataGrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), PSDataGrid.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadIDRDatagridColumnOrder()
        On Error Resume Next
        Dim p As String = strRegistrySettingsPath & "\JoinedIDRDatagrid"
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            JoinedIDRDataGrid.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(p, "Order" & Format(i, "00"), JoinedIDRDataGrid.Columns(i).DisplayIndex)
        Next
    End Sub


    '------------------------------------------------------Load Default Column Orders--------------------------


    Private Sub LoadSOCDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.SOCDatagrid.ColumnCount - 1
            SOCDatagrid.Columns(i).DisplayIndex = i
        Next
    End Sub


    Private Sub LoadRSOCDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.RSOCDatagrid.ColumnCount - 1
            RSOCDatagrid.Columns(i).DisplayIndex = i
        Next
    End Sub


    Private Sub LoadDADatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.DADatagrid.ColumnCount - 1
            DADatagrid.Columns(i).DisplayIndex = i
        Next
    End Sub


    Private Sub LoadIDDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.IDDatagrid.ColumnCount - 1
            IDDatagrid.Columns(i).DisplayIndex = i
        Next
    End Sub

    Private Sub LoadACDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.ACDatagrid.ColumnCount - 1
            ACDatagrid.Columns(i).DisplayIndex = i
        Next
    End Sub

    Private Sub LoadFPADatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.FPADataGrid.ColumnCount - 1
            FPADataGrid.Columns(i).DisplayIndex = i
        Next
    End Sub

    Private Sub LoadCDDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.CDDataGrid.ColumnCount - 1
            CDDataGrid.Columns(i).DisplayIndex = i
        Next
    End Sub

    Private Sub LoadPSDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.PSDataGrid.ColumnCount - 1
            PSDataGrid.Columns(i).DisplayIndex = i
        Next
    End Sub


    Private Sub LoadIDRDatagridColumnDefaultOrder()
        On Error Resume Next
        For i = 0 To Me.JoinedIDRDataGrid.ColumnCount - 1
            JoinedIDRDataGrid.Columns(i).DisplayIndex = i
        Next
    End Sub
    '------------------------------------------------------Load Default Column Orders All --------------------------


    Private Sub ResetDatagridColumnOrderOfAllTables() Handles btnResetColumnOrderOfAllTables.Click
        On Error Resume Next
        Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("This action will reset the column order of all tables. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        LoadSOCDatagridColumnDefaultOrder()
        LoadRSOCDatagridColumnDefaultOrder()
        LoadDADatagridColumnDefaultOrder()
        LoadIDDatagridColumnDefaultOrder()
        LoadACDatagridColumnDefaultOrder()
        LoadFPADatagridColumnDefaultOrder()
        LoadCDDatagridColumnDefaultOrder()
        LoadPSDatagridColumnDefaultOrder()
        LoadIDRDatagridColumnDefaultOrder()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        ShowDesktopAlert("Column order of all tables set to default order!")
    End Sub


    Private Sub LoadDefaultColumnOrdersOfCurrentTable() Handles btnResetColumnOrder.Click, btnResetColumnOrderMenu.Click
        On Error Resume Next
        If CurrentTab = "IO" Then
            MessageBoxEx.Show("This option is not available for the current table", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("This action will reset the column order of the current table. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Select Case CurrentTab
            Case "SOC"
                LoadSOCDatagridColumnDefaultOrder()
            Case "RSOC"
                LoadRSOCDatagridColumnDefaultOrder()
            Case "DA"
                LoadDADatagridColumnDefaultOrder()
            Case "ID"
                LoadIDDatagridColumnDefaultOrder()
            Case "AC"
                LoadACDatagridColumnDefaultOrder()
            Case "FPA"
                LoadFPADatagridColumnDefaultOrder()
            Case "CD"
                LoadCDDatagridColumnDefaultOrder()
            Case "PS"
                LoadPSDatagridColumnDefaultOrder()
            Case "IDR"
                LoadIDRDatagridColumnDefaultOrder()
            Case "IO"
                MessageBoxEx.Show("This option is not available for the current table", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
        End Select

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        ShowDesktopAlert("Column order of the current table set to default order!")
    End Sub

    Private Sub FreezeColumns() Handles btnFreezeColumn.Click
        On Error Resume Next
        If CurrentTab = "SOC" Then
            Me.SOCDatagrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If
        If CurrentTab = "RSOC" Then
            Me.RSOCDatagrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If

        If CurrentTab = "DA" Then
            Me.DADatagrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If
        If CurrentTab = "ID" Then
            Me.IDDatagrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If
        If CurrentTab = "AC" Then
            Me.ACDatagrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If
        If CurrentTab = "FPA" Then
            Me.FPADataGrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If
        If CurrentTab = "CD" Then
            Me.CDDataGrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If

        If CurrentTab = "PS" Then
            Me.PSDataGrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If

        If CurrentTab = "IDR" Then
            Me.JoinedIDRDataGrid.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumn.Checked
        End If
    End Sub


#End Region


#Region "DATAGRID BACKCOLOR"
    Private Sub SetTableBackColor()
        Try
            Dim strTableEvenColor As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TableEvenColor", Color.LightBlue.ToArgb.ToString)
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "TableEvenColor", strTableEvenColor, Microsoft.Win32.RegistryValueKind.String)

            Dim strTableOddColor As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TableOddColor", SOCDatagrid.DefaultCellStyle.BackColor.ToArgb.ToString)
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "TableOddColor", strTableOddColor, Microsoft.Win32.RegistryValueKind.String)

            TableEvenColor = CType(Color.FromArgb(strTableEvenColor), Color)
            TableOddColor = CType(Color.FromArgb(strTableOddColor), Color)
            Me.ColorPickerEvenRecords.SelectedColor = TableEvenColor
            Me.ColorPickerOddRecords.SelectedColor = TableOddColor
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SOCDatagrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles SOCDatagrid.RowPrePaint
        Try

            Dim dt As Date = SOCDatagrid.Rows(e.RowIndex).Cells(2).Value

            If dt.Month Mod 2 = 0 Then
                SOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableEvenColor
            Else
                SOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableOddColor
            End If

            Dim fs As String = SOCDatagrid.Rows(e.RowIndex).Cells(24).Value.ToString.ToLower
            If fs = "identified" Then
                SOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Red
                SOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.SelectionForeColor = Color.Red
            Else
                SOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
                SOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.SelectionForeColor = Color.Black
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub IDRDatagrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles JoinedIDRDataGrid.RowPrePaint
        Try

            Dim dt As Date = JoinedIDRDataGrid.Rows(e.RowIndex).Cells(2).Value

            If dt.Year Mod 2 = 0 Then
                JoinedIDRDataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableEvenColor
            Else
                JoinedIDRDataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableOddColor
            End If


        Catch ex As Exception
        End Try
    End Sub

    Private Sub DADatagrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DADatagrid.RowPrePaint
        Try

            Dim dt As Date = DADatagrid.Rows(e.RowIndex).Cells(2).Value
            If dt.Month Mod 2 = 0 Then
                DADatagrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableEvenColor
            Else
                DADatagrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableOddColor
            End If



        Catch ex As Exception
        End Try
    End Sub

    Private Sub FPADatagrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles FPADataGrid.RowPrePaint
        Try

            Dim dt As Date = FPADataGrid.Rows(e.RowIndex).Cells(2).Value

            If dt.Month Mod 2 = 0 Then
                FPADataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableEvenColor
            Else
                FPADataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableOddColor
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub CDDatagrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles CDDataGrid.RowPrePaint
        Try

            Dim dt As Date = CDDataGrid.Rows(e.RowIndex).Cells(2).Value
            If dt.Month Mod 2 = 0 Then
                CDDataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableEvenColor
            Else
                CDDataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableOddColor
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub rsocDatagrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles RSOCDatagrid.RowPrePaint
        Try


            Dim dt As Date = RSOCDatagrid.Rows(e.RowIndex).Cells(3).Value
            If dt.Month Mod 2 = 0 Then
                RSOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableEvenColor
            Else
                RSOCDatagrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = TableOddColor
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub EvenColorPicker_SelectedColorChanged(sender As Object, e As EventArgs) Handles ColorPickerEvenRecords.SelectedColorChanged
        TableEvenColor = ColorPickerEvenRecords.SelectedColor
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TableEvenColor", TableEvenColor.ToArgb.ToString, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub OddColorPicker_SelectedColorChanged(sender As Object, e As EventArgs) Handles ColorPickerOddRecords.SelectedColorChanged
        TableOddColor = ColorPickerOddRecords.SelectedColor
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TableOddColor", TableOddColor.ToArgb.ToString, Microsoft.Win32.RegistryValueKind.String)
    End Sub

#End Region


#Region "CONTEXT MENU SETTINGS"

    Private Sub MouseOverDatagridAction(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles SOCDatagrid.CellMouseClick, RSOCDatagrid.CellMouseClick, DADatagrid.CellMouseClick, FPADataGrid.CellMouseClick, PSDataGrid.CellMouseClick, CDDataGrid.CellMouseClick, IDDatagrid.CellMouseClick, ACDatagrid.CellMouseClick, JoinedIDRDataGrid.CellMouseClick, SSDatagrid.CellMouseClick, IODatagrid.CellMouseClick
        On Error Resume Next
        SelectedRowIndex = e.RowIndex
        SelectedColumnIndex = e.ColumnIndex
    End Sub
    Private Sub ColumnHeaderContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ColumnHeaderContextMenuBar.PopupOpen
        On Error Resume Next

        If CurrentTab = "IO" Or CurrentTab = "PS" Then
            btnResetColumnOrderMenu.Visible = False
            btnFreezeColumn.Visible = False
        Else
            btnResetColumnOrderMenu.Visible = True
            btnFreezeColumn.Visible = True

            If CurrentTab = "SOC" Then
                Me.btnFreezeColumn.Checked = Me.SOCDatagrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "RSOC" Then
                Me.btnFreezeColumn.Checked = Me.RSOCDatagrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "DA" Then
                Me.btnFreezeColumn.Checked = Me.DADatagrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "ID" Then
                Me.btnFreezeColumn.Checked = Me.IDDatagrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "AC" Then
                Me.btnFreezeColumn.Checked = Me.ACDatagrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "FPA" Then
                Me.btnFreezeColumn.Checked = Me.FPADataGrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "CD" Then
                Me.btnFreezeColumn.Checked = Me.CDDataGrid.Columns(SelectedColumnIndex).Frozen
            End If
            If CurrentTab = "IDR" Then
                Me.btnFreezeColumn.Checked = Me.JoinedIDRDataGrid.Columns(SelectedColumnIndex).Frozen
            End If
        End If

        If ColumnHeaderClicked Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
        ColumnHeaderClicked = False
    End Sub
    Private Sub DataGridContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles DataGridContextMenuBar.PopupOpen

        On Error Resume Next
        Me.btnOpenContext.Visible = True
        Me.btnFacingSheetContext.Visible = False
        Me.btnSOCReportContext.Visible = False
        Me.btnPhotoReceivedContext.Visible = False
        Me.btnViewDASlipContext.Visible = False
        Me.btnViewIDSlipContext.Visible = False
        Me.btnViewACSlipContext.Visible = False
        Me.btnLocateFPSlip.Visible = False
        Me.btnImportCP.Visible = False
        Me.btnViewCP.Visible = False
        Me.btnLocateCP.Visible = False
        Me.btnSelcetCPs.Visible = False
        Me.btnSelectFPSlipContext.Visible = False
        Me.btnImportFPSlipContext.Visible = False
        Me.btnIdentifiedTemplateContextMenu.Visible = False
        Me.btnFPAGenerateSlipFormContext.Visible = False
        Me.btnIDRShowInSoCRegister.Visible = False
        Me.btnSOCShowInIDRRegister.Visible = False
        Me.btnGenerateIdentificationCoB.Visible = False
        Me.btnGenerateIdentificationLetter.Visible = False
        Me.btnGenerateIdentificationReportDirector.Visible = False
        Me.btnGenerateExpertOpinionContext.Visible = False
        Me.btnEnterIDRDetailsContextMenu.Visible = False
        Me.btnOpenExpertOpinionFolderContext.Visible = False
        Me.btnOpenIdentificationReportFolderContext.Visible = False
        Me.btnSelectOpinionImages.Visible = False
        Me.btnGenerateExpertOpinionCLContext.Visible = False
        Me.btnGenerateIdentificationReportDPCContext.Visible = False

        If CurrentTab = "SOC" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.SOCDatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.SOCDatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.SOCDatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.SOCRegisterBindingSource.Position = SelectedRowIndex

            Me.btnFacingSheetContext.Visible = True
            Me.btnSOCReportContext.Visible = True


            Dim y = UCase(Me.SOCDatagrid.SelectedCells(19).Value.ToString) 'photo received or not.

            If Me.SOCDatagrid.SelectedCells(10).Value.ToString <> "0" Then
                Me.btnPhotoReceivedContext.Visible = True
                Me.btnImportCP.Visible = True
                Me.btnSelcetCPs.Visible = True

                Dim yr As String = Year(Me.SOCDatagrid.SelectedCells(2).Value.ToString)
                Dim soc As String = Strings.Format(Me.SOCDatagrid.SelectedCells(1).Value, "000")
                Dim Location As String = CPImageImportLocation & yr & "\SOC No. " & soc

                If FileIO.FileSystem.DirectoryExists(Location) Then
                    If FileIO.FileSystem.GetFiles(Location).Count <> 0 Then
                        Me.btnViewCP.Visible = True
                        Me.btnLocateCP.Visible = True
                    Else
                        Me.btnViewCP.Visible = False
                        Me.btnLocateCP.Visible = False
                    End If
                End If

            End If

            If y = "YES" Then
                Me.btnPhotoReceivedContext.Checked = True
            Else
                Me.btnPhotoReceivedContext.Checked = False
            End If

            Dim cpd As Integer = Val(Me.SOCDatagrid.SelectedCells(10).Value)
            Dim cpu As Integer = Val(Me.SOCDatagrid.SelectedCells(11).Value)
            Dim cpe As Integer = Val(Me.SOCDatagrid.SelectedCells(12).Value)
            Dim cpid As Integer = Val(Me.SOCDatagrid.SelectedCells(13).Value)
            Dim cpr As Integer = cpd - cpu - cpe - cpid

            If cpid > 0 Then
                Me.btnSOCShowInIDRRegister.Visible = True
                Me.btnEnterIDRDetailsContextMenu.Visible = False
            Else
                If cpr > 0 Then
                    Me.btnEnterIDRDetailsContextMenu.Visible = True
                End If
            End If


        End If



        If CurrentTab = "RSOC" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.RSOCDatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.RSOCDatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.RSOCDatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.RSOCRegisterBindingSource.Position = SelectedRowIndex
        End If


        If CurrentTab = "DA" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.DADatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.DADatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.DADatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.DARegisterBindingSource.Position = SelectedRowIndex
            Me.btnSelectFPSlipContext.Visible = True
            Me.btnImportFPSlipContext.Visible = True
            Me.btnViewDASlipContext.Visible = True
            Me.btnLocateFPSlip.Visible = True

            Dim imagefile = Me.DADatagrid.SelectedCells(15).Value.ToString
            If FileIO.FileSystem.FileExists(imagefile) = False Then
                Dim FileName As String = "DANo." & Me.DADatagrid.SelectedCells(0).Value.ToString & ".jpeg" '
                FileName = Strings.Replace(FileName, "/", "-")
                Dim yr As String = Year(Me.DADatagrid.SelectedCells(2).Value)
                If Strings.Right(DASlipImageImportLocation, 1) <> "\" Then DASlipImageImportLocation = DASlipImageImportLocation & "\"
                imagefile = DASlipImageImportLocation & yr & "\" & FileName
            End If
            Me.btnViewDASlipContext.Enabled = FileIO.FileSystem.FileExists(imagefile)
            Me.btnLocateFPSlip.Enabled = Me.btnViewDASlipContext.Enabled

        End If

        If CurrentTab = "ID" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.IDDatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.IDDatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.IDDatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.IDRegisterBindingSource.Position = SelectedRowIndex
            Me.btnSelectFPSlipContext.Visible = True
            Me.btnImportFPSlipContext.Visible = True
            Me.btnViewIDSlipContext.Visible = True
            Me.btnLocateFPSlip.Visible = True

            Dim imagefile = Me.IDDatagrid.SelectedCells(15).Value.ToString
            If FileIO.FileSystem.FileExists(imagefile) = False Then
                Dim FileName As String = "IDNo." & Format(CInt(Me.IDDatagrid.SelectedCells(0).Value), "0000") & ".jpeg"
                If Strings.Right(IDSlipImageImportLocation, 1) <> "\" Then IDSlipImageImportLocation = IDSlipImageImportLocation & "\"
                imagefile = IDSlipImageImportLocation & FileName
            End If

            Me.btnViewIDSlipContext.Enabled = FileIO.FileSystem.FileExists(imagefile)
            Me.btnLocateFPSlip.Enabled = Me.btnViewIDSlipContext.Enabled

        End If

        If CurrentTab = "AC" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.ACDatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.ACDatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.ACDatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.ACRegisterBindingSource.Position = SelectedRowIndex
            Me.btnSelectFPSlipContext.Visible = True
            Me.btnImportFPSlipContext.Visible = True
            Me.btnViewACSlipContext.Visible = True
            Me.btnLocateFPSlip.Visible = True

            Dim imagefile = Me.ACDatagrid.SelectedCells(14).Value.ToString
            If FileIO.FileSystem.FileExists(imagefile) = False Then

                Dim FileName As String = "ACNo." & Format(CInt(Me.ACDatagrid.SelectedCells(0).Value), "0000") & ".jpeg"
                If Strings.Right(ACSlipImageImportLocation, 1) <> "\" Then ACSlipImageImportLocation = ACSlipImageImportLocation & "\"
                imagefile = ACSlipImageImportLocation & FileName
            End If
            Me.btnViewACSlipContext.Enabled = FileIO.FileSystem.FileExists(imagefile)
            Me.btnLocateFPSlip.Enabled = Me.btnViewACSlipContext.Enabled

        End If

        If CurrentTab = "FPA" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.FPADataGrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.FPADataGrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.FPADataGrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.FPARegisterBindingSource.Position = SelectedRowIndex
            Me.btnFPAGenerateSlipFormContext.Visible = True
        End If


        If CurrentTab = "PS" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.PSDataGrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.PSDataGrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.PSDataGrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.PSRegisterBindingSource.Position = SelectedRowIndex

        End If

        If CurrentTab = "SS" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.SSDatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.SSDatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.SSDatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.SSBindingSource.Position = SelectedRowIndex
            Me.btnOpenContext.Visible = False
        End If

        If CurrentTab = "IO" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.IODatagrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.IODatagrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.IODatagrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.btnOpenContext.Visible = False
        End If

        If CurrentTab = "CD" Then
            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.CDDataGrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If
            Me.CDDataGrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.CDDataGrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.CDRegisterBindingSource.Position = SelectedRowIndex
        End If

        If CurrentTab = "IDR" Then

            If SelectedRowIndex < 0 Or SelectedRowIndex > Me.JoinedIDRDataGrid.Rows.Count - 1 Then
                e.Cancel = True
                Exit Sub
            End If

            Me.JoinedIDRDataGrid.Rows(SelectedRowIndex).Selected = True
            If SelectedColumnIndex <> -1 Then Me.JoinedIDRDataGrid.SelectedCells(SelectedColumnIndex).Selected = True
            Me.JoinedIDRBindingSource.Position = SelectedRowIndex
            Me.btnIDRShowInSoCRegister.Visible = True
            Me.btnIdentifiedTemplateContextMenu.Visible = True
            Me.btnGenerateIdentificationCoB.Visible = True
            Me.btnGenerateIdentificationLetter.Visible = True
            Me.btnGenerateIdentificationReportDirector.Visible = True
            Me.btnGenerateExpertOpinionContext.Visible = True
            Me.btnOpenExpertOpinionFolderContext.Visible = True
            Me.btnOpenIdentificationReportFolderContext.Visible = True
            Me.btnSelectOpinionImages.Visible = True
            Me.btnGenerateExpertOpinionCLContext.Visible = True
            Me.btnGenerateIdentificationReportDPCContext.Visible = True
        End If

        ' DisplayDatabaseInformation()
        SelectedRowIndex = -1
    End Sub

#End Region


#Region "SORT"

    Private Sub SetAscendingSortMode()
        On Error Resume Next
        Me.SOCRegisterBindingSource.Sort = SOCDatagrid.Columns(2).DataPropertyName.ToString() & " ASC, " & SOCDatagrid.Columns(1).DataPropertyName.ToString() & " ASC"
        Me.RSOCRegisterBindingSource.Sort = RSOCDatagrid.Columns(3).DataPropertyName.ToString() & " ASC, " & RSOCDatagrid.Columns(2).DataPropertyName.ToString() & " ASC"
        Me.DARegisterBindingSource.Sort = DADatagrid.Columns(2).DataPropertyName.ToString() & " ASC, " & DADatagrid.Columns(1).DataPropertyName.ToString() & " ASC"
        Me.IDRegisterBindingSource.Sort = IDDatagrid.Columns(0).DataPropertyName.ToString() & " ASC"
        Me.ACRegisterBindingSource.Sort = ACDatagrid.Columns(0).DataPropertyName.ToString() & " ASC"
        Me.FPARegisterBindingSource.Sort = FPADataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & FPADataGrid.Columns(1).DataPropertyName.ToString() & " ASC"
        Me.CDRegisterBindingSource.Sort = CDDataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & CDDataGrid.Columns(1).DataPropertyName.ToString() & " ASC"
        Me.PSRegisterBindingSource.Sort = PSDataGrid.Columns(0).DataPropertyName.ToString() & " ASC"
        '  Me.JoinedIDRBindingSource.Sort = JoinedIDRDataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & JoinedIDRDataGrid.Columns(19).DataPropertyName.ToString() & " ASC"

    End Sub


    Private Sub SetDatagridSortMode()
        On Error Resume Next
        For i = 0 To Me.SOCDatagrid.Columns.Count - 1
            Me.SOCDatagrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.RSOCDatagrid.Columns.Count - 1
            Me.RSOCDatagrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.DADatagrid.Columns.Count - 1
            Me.DADatagrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.IDDatagrid.Columns.Count - 1
            Me.IDDatagrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.ACDatagrid.Columns.Count - 1
            Me.ACDatagrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.FPADataGrid.Columns.Count - 1
            Me.FPADataGrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.CDDataGrid.Columns.Count - 1
            Me.CDDataGrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.PSDataGrid.Columns.Count - 1
            Me.PSDataGrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next
        For i = 0 To Me.JoinedIDRDataGrid.Columns.Count - 1
            Me.JoinedIDRDataGrid.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next

    End Sub

    Private Sub SortColumns(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles SOCDatagrid.ColumnHeaderMouseClick, RSOCDatagrid.ColumnHeaderMouseClick, DADatagrid.ColumnHeaderMouseClick, IDDatagrid.ColumnHeaderMouseClick, ACDatagrid.ColumnHeaderMouseClick, FPADataGrid.ColumnHeaderMouseClick, CDDataGrid.ColumnHeaderMouseClick, PSDataGrid.ColumnHeaderMouseClick, JoinedIDRDataGrid.ColumnHeaderMouseClick
        On Error Resume Next

        If e.Button <> Windows.Forms.MouseButtons.Left Then
            ColumnHeaderClicked = True
            Exit Sub
        End If


        Dim c = e.ColumnIndex
        Select Case DirectCast(sender, Control).Name

            Case SOCDatagrid.Name

                Dim oldrow As String = ""
                If Me.SOCDatagrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.SOCDatagrid.SelectedRows(0).Cells(0).Value
                End If

                If SOCDatagrid.SortOrder = SortOrder.None Or SOCDatagrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.SOCRegisterBindingSource.Sort = SOCDatagrid.Columns(2).DataPropertyName.ToString() & " ASC, " & SOCDatagrid.Columns(1).DataPropertyName.ToString() & " ASC"
                    Else
                        Me.SOCRegisterBindingSource.Sort = SOCDatagrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    End If

                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & SOCDatagrid.Columns(c).HeaderText & " in Ascending order!")
                ElseIf SOCDatagrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.SOCRegisterBindingSource.Sort = SOCDatagrid.Columns(2).DataPropertyName.ToString() & " DESC, " & SOCDatagrid.Columns(1).DataPropertyName.ToString() & " DESC"
                    Else
                        Me.SOCRegisterBindingSource.Sort = SOCDatagrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    End If
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & SOCDatagrid.Columns(c).HeaderText & " in Descending order!")
                End If

                Dim p = Me.SOCRegisterBindingSource.Find("SOCNumber", oldrow)
                If p >= 0 Then Me.SOCRegisterBindingSource.Position = p

            Case RSOCDatagrid.Name

                Dim oldrow As String = ""
                If Me.RSOCDatagrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.RSOCDatagrid.SelectedRows(0).Cells(0).Value
                End If

                If RSOCDatagrid.SortOrder = SortOrder.None Or RSOCDatagrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 1 Then
                        Me.RSOCRegisterBindingSource.Sort = RSOCDatagrid.Columns(3).DataPropertyName.ToString() & " ASC, " & RSOCDatagrid.Columns(2).DataPropertyName.ToString() & " ASC"
                    Else
                        Me.RSOCRegisterBindingSource.Sort = RSOCDatagrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    End If

                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & RSOCDatagrid.Columns(c).HeaderText & " in Ascending order!")

                ElseIf RSOCDatagrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 1 Then
                        Me.RSOCRegisterBindingSource.Sort = RSOCDatagrid.Columns(3).DataPropertyName.ToString() & " DESC, " & RSOCDatagrid.Columns(2).DataPropertyName.ToString() & " DESC"
                    Else
                        Me.RSOCRegisterBindingSource.Sort = RSOCDatagrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    End If
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & RSOCDatagrid.Columns(c).HeaderText & " in Descending order!")
                End If

                Dim p = Me.RSOCRegisterBindingSource.Find("SerialNo", oldrow)
                If p >= 0 Then Me.RSOCRegisterBindingSource.Position = p


            Case DADatagrid.Name
                Dim oldrow As String = ""
                If Me.DADatagrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.DADatagrid.SelectedRows(0).Cells(0).Value
                End If

                If DADatagrid.SortOrder = SortOrder.None Or DADatagrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.DARegisterBindingSource.Sort = DADatagrid.Columns(2).DataPropertyName.ToString() & " ASC, " & DADatagrid.Columns(1).DataPropertyName.ToString() & " ASC"
                    Else
                        Me.DARegisterBindingSource.Sort = DADatagrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    End If

                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & DADatagrid.Columns(c).HeaderText & " in Ascending order!")

                ElseIf DADatagrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.DARegisterBindingSource.Sort = DADatagrid.Columns(2).DataPropertyName.ToString() & " DESC, " & DADatagrid.Columns(1).DataPropertyName.ToString() & " DESC"
                    Else
                        Me.DARegisterBindingSource.Sort = DADatagrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    End If
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & DADatagrid.Columns(c).HeaderText & " in Descending order!")
                End If

                Dim p = Me.DARegisterBindingSource.Find("DANumber", oldrow)
                If p >= 0 Then Me.DARegisterBindingSource.Position = p



            Case IDDatagrid.Name

                Dim oldrow As String = ""
                If Me.IDDatagrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.IDDatagrid.SelectedRows(0).Cells(0).Value
                End If

                If IDDatagrid.SortOrder = SortOrder.None Or IDDatagrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.IDRegisterBindingSource.Sort = IDDatagrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & IDDatagrid.Columns(c).HeaderText & " in Ascending order!")
                ElseIf IDDatagrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.IDRegisterBindingSource.Sort = IDDatagrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & IDDatagrid.Columns(c).HeaderText & " in Descending order!")
                End If

                Dim p = Me.IDRegisterBindingSource.Find("IDNumber", oldrow)
                If p >= 0 Then Me.IDRegisterBindingSource.Position = p



            Case ACDatagrid.Name

                Dim oldrow As String = ""
                If Me.ACDatagrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.ACDatagrid.SelectedRows(0).Cells(0).Value
                End If

                If ACDatagrid.SortOrder = SortOrder.None Or ACDatagrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.ACRegisterBindingSource.Sort = ACDatagrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & ACDatagrid.Columns(c).HeaderText & " in Ascending order!")

                ElseIf ACDatagrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.ACRegisterBindingSource.Sort = ACDatagrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & ACDatagrid.Columns(c).HeaderText & " in Descending order!")
                End If

                Dim p = Me.ACRegisterBindingSource.Find("ACNumber", oldrow)
                If p >= 0 Then Me.ACRegisterBindingSource.Position = p



            Case FPADataGrid.Name

                Dim oldrow As String = ""
                If Me.FPADataGrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.FPADataGrid.SelectedRows(0).Cells(0).Value
                End If

                If FPADataGrid.SortOrder = SortOrder.None Or FPADataGrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.FPARegisterBindingSource.Sort = FPADataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & FPADataGrid.Columns(1).DataPropertyName.ToString() & " ASC"
                    Else
                        Me.FPARegisterBindingSource.Sort = FPADataGrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    End If

                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & FPADataGrid.Columns(c).HeaderText & " in Ascending order!")

                ElseIf FPADataGrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.FPARegisterBindingSource.Sort = FPADataGrid.Columns(2).DataPropertyName.ToString() & " DESC, " & FPADataGrid.Columns(1).DataPropertyName.ToString() & " DESC"
                    Else
                        Me.FPARegisterBindingSource.Sort = FPADataGrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    End If
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & FPADataGrid.Columns(c).HeaderText & " in Descending order!")

                End If

                Dim p = Me.FPARegisterBindingSource.Find("FPNumber", oldrow)
                If p >= 0 Then Me.FPARegisterBindingSource.Position = p



            Case CDDataGrid.Name

                Dim oldrow As String = ""
                If Me.CDDataGrid.SelectedRows.Count <> 0 Then
                    oldrow = Me.CDDataGrid.SelectedRows(0).Cells(0).Value
                End If

                If CDDataGrid.SortOrder = SortOrder.None Or CDDataGrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.CDRegisterBindingSource.Sort = CDDataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & CDDataGrid.Columns(1).DataPropertyName.ToString() & " ASC"
                    Else
                        Me.CDRegisterBindingSource.Sort = CDDataGrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    End If

                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & CDDataGrid.Columns(c).HeaderText & " in Ascending order!")

                ElseIf CDDataGrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.CDRegisterBindingSource.Sort = CDDataGrid.Columns(2).DataPropertyName.ToString() & " DESC, " & CDDataGrid.Columns(1).DataPropertyName.ToString() & " DESC"
                    Else
                        Me.CDRegisterBindingSource.Sort = CDDataGrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    End If
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & CDDataGrid.Columns(c).HeaderText & " in Descending order!")

                End If


                Dim p = Me.CDRegisterBindingSource.Find("CDNumberWithYear", oldrow)
                If p >= 0 Then Me.CDRegisterBindingSource.Position = p

            Case PSDataGrid.Name

                If PSDataGrid.SortOrder = SortOrder.None Or PSDataGrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.PSRegisterBindingSource.Sort = PSDataGrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & PSDataGrid.Columns(c).HeaderText & " in Ascending order!")
                ElseIf PSDataGrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.PSRegisterBindingSource.Sort = PSDataGrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & PSDataGrid.Columns(c).HeaderText & " in Descending order!")
                End If

            Case JoinedIDRDataGrid.Name

                Dim oldrow As String = ""
                If Me.JoinedIDRDataGrid.SelectedRows.Count > 0 Then
                    oldrow = Me.JoinedIDRDataGrid.SelectedRows(0).Cells(0).Value
                End If

                If JoinedIDRDataGrid.SortOrder = SortOrder.None Or JoinedIDRDataGrid.SortOrder = SortOrder.Descending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.JoinedIDRBindingSource.Sort = JoinedIDRDataGrid.Columns(2).DataPropertyName.ToString() & " ASC, " & JoinedIDRDataGrid.Columns(19).DataPropertyName.ToString() & " ASC"
                    Else
                        Me.JoinedIDRBindingSource.Sort = JoinedIDRDataGrid.Columns(c).DataPropertyName.ToString() & " ASC"
                    End If

                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & JoinedIDRDataGrid.Columns(c).HeaderText & " in Ascending order!")

                ElseIf JoinedIDRDataGrid.SortOrder = SortOrder.Ascending Then
                    Me.Cursor = Cursors.WaitCursor
                    If c = 0 Then
                        Me.JoinedIDRBindingSource.Sort = JoinedIDRDataGrid.Columns(2).DataPropertyName.ToString() & " DESC, " & JoinedIDRDataGrid.Columns(19).DataPropertyName.ToString() & " DESC"
                    Else
                        Me.JoinedIDRBindingSource.Sort = JoinedIDRDataGrid.Columns(c).DataPropertyName.ToString() & " DESC"
                    End If
                    If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                    ShowDesktopAlert("Table sorted with " & JoinedIDRDataGrid.Columns(c).HeaderText & " in Descending order!")
                End If

                Dim p = Me.JoinedIDRBindingSource.Find("IdentificationNumber", oldrow)
                If p >= 0 Then Me.JoinedIDRBindingSource.Position = p
        End Select



    End Sub


#End Region


#Region "AUTO CAPITALIZE"


    Public Function ConvertToProperCase(ByVal InputText) As String
        On Error Resume Next
        If Me.chkIgnoreAllCaps.Checked = False Then
            Return Strings.StrConv(InputText, VbStrConv.ProperCase)
            Exit Function
        End If

        Dim line() = Strings.Split(InputText, vbNewLine)
        Dim ln As String = ""
        Dim u = line.GetUpperBound(0)

        For j = 0 To u
            Dim c = line(j)
            ln = ln & JoinTexts(c)
            If j <> u Then ln = ln & vbNewLine
        Next



        Return ln
    End Function


    Private Function JoinTexts(ByVal InputText As String) As String

        On Error Resume Next

        Dim s() = Strings.Split(InputText, " ")
        Dim t As String = ""
        Dim n = s.GetUpperBound(0)
        For i = 0 To n
            Dim c = s(i)
            If AllCaps(c) Then
                t = t & c
            Else
                t = t & Strings.StrConv(c, VbStrConv.ProperCase)
            End If
            If i <> n Then t = t & " "
        Next

        Return t
    End Function

    Private Sub HandleCtrlAinMultilineTextBox(sender As Object, e As KeyEventArgs) Handles txtSOCPlace.KeyDown, txtSOCComplainant.KeyDown, txtSOCPropertyLost.KeyDown, txtSOCCPDetails.KeyDown, txtSOCGist.KeyDown, txtSOCComparisonDetails.KeyDown, txtDAAddress.KeyDown, txtDARemarks.KeyDown, txtCDDetails.KeyDown, txtCDRemarks.KeyDown, txtIDAddress.KeyDown, txtIDDetails.KeyDown, txtIDRemarks.KeyDown, txtACRemarks.KeyDown, txtACAddress.KeyDown, txtRSOCRemarks.KeyDown, txtRSOCReportSentTo.KeyDown
        Try
            Dim x As TextBox = DirectCast(sender, Control)
            If e.Control And e.KeyCode = Keys.A Then
                x.SelectAll()
                e.Handled = True
                e.SuppressKeyPress = True
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub ConvertToProperCase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSOCPlace.Validated, txtSOCComplainant.Validated, cmbDASex.Validated, txtDAAddress.Validated, txtDAName.Validated, txtDAAliasName.Validated, txtDAFathersName.Validated, cmbIDSex.Validated, txtIDAddress.Validated, txtIDName.Validated, txtIDAliasName.Validated, txtIDFathersName.Validated, txtSOCPhotographer.Validated, cmbACSex.Validated, txtACAddress.Validated, txtACName.Validated, txtACAliasName.Validated, txtACFathersName.Validated, txtSOCOfficer.Validated
        On Error Resume Next
        ' If chkAutoCapitalize.Checked = False Or TemporarilyStopCapitalize = True Then
        If TemporarilyStopCapitalize = True Then
            ' TemporarilyStopCapitalize = False
            Exit Sub
        End If
        Dim x As Control = DirectCast(sender, Control)
        Dim t As String = ConvertToProperCase(x.Text)
        t = t.Replace("S/O", "s/o")
        t = t.Replace("W/O", "w/o")
        t = t.Replace("D/O", "d/o")
        t = t.Replace("O/O", "o/o")
        t = t.Replace(" A ", " a ")
        t = t.Replace(" An ", " an ")
        t = t.Replace(" At ", " at ")
        t = t.Replace(" The ", " the ")
        t = t.Replace(". a ", ". A ")
        t = t.Replace(". an ", ". An ")
        t = t.Replace(". at ", ". At ")
        t = t.Replace(". the ", ". The ")
        t = t.Replace(" Of ", " of ")
        t = t.Replace(" Located ", " located ")
        t = t.Replace(" House ", " house ")
        t = t.Replace(" Residence ", " residence ")
        t = t.Replace(" Shop ", " shop ")
        t = t.Replace(" Fpe; ", " FPE; ")
        t = t.Replace(" Fps; ", " FPS; ")
        t = t.Replace(" Ti; ", " TI; ")
        x.Text = t
        TemporarilyStopCapitalize = False
    End Sub
    Sub TemporarilyStopCapitalization(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSOCPlace.KeyDown, txtSOCComplainant.KeyDown, cmbDASex.KeyDown, txtDAAddress.KeyDown, txtDAName.KeyDown, txtDAAliasName.KeyDown, txtDAFathersName.KeyDown, cmbIDSex.KeyDown, txtIDAddress.KeyDown, txtIDName.KeyDown, txtIDAliasName.KeyDown, txtIDFathersName.KeyDown, txtSOCPhotographer.KeyDown, cmbACSex.KeyDown, txtACAddress.KeyDown, txtACName.KeyDown, txtACAliasName.KeyDown, txtACFathersName.KeyDown, txtSOCSection.KeyDown, txtSOCPropertyLost.KeyDown, txtDASection.KeyDown, txtIDSection.KeyDown, txtACSection.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.Escape Then
            TemporarilyStopCapitalize = Not TemporarilyStopCapitalize
            DisplayAutoCapsStatus(Not TemporarilyStopCapitalize)
        End If

    End Sub


    Private Sub ChangeCase(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSOCSection.Validated, txtSOCPropertyLost.Validated, txtDASection.Validated, txtIDSection.Validated, txtACSection.Validated
        On Error Resume Next
        If TemporarilyStopCapitalize = True Then
            ' TemporarilyStopCapitalize = False
            Exit Sub
        End If
        Dim x As Control = DirectCast(sender, Control)
        Dim c As String = x.Text
        c = Strings.Replace(c, "ipc", "IPC", , , CompareMethod.Text)
        c = Strings.Replace(c, "crpc", "CrPC", , , CompareMethod.Text)
        c = Strings.Replace(c, "Rs.", "`", , , CompareMethod.Binary)
        c = Strings.Replace(c, "twrs. ", "TW `", , , CompareMethod.Text)
        c = Strings.Replace(c, "wrs. ", "W `", , , CompareMethod.Text)
        c = Strings.Replace(c, "twrs ", "TW `", , , CompareMethod.Text)
        c = Strings.Replace(c, "wrs ", "W `", , , CompareMethod.Text)
        x.Text = c

    End Sub


    Private Sub StopCapiltalize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblACAddress.Click, lblACAlias.Click, lblACFather.Click, lblACName.Click, lblDAAdress.Click, lblDAAlias.Click, lblDAFather.Click, lblIDAddress.Click, lblIDAlias.Click, lblIDFather.Click, lblIDName.Click, lblSOCPhoto.Click, lblSOCPO.Click, lblSOCComplainant.Click, lblAutoCapsStatus.Click
        On Error Resume Next
        TemporarilyStopCapitalize = Not TemporarilyStopCapitalize
        DisplayAutoCapsStatus(Not TemporarilyStopCapitalize)
    End Sub


    Private Function AllCaps(ByVal InputWord As String) As Boolean
        On Error Resume Next
        Dim aChar As Char = ""
        AllCaps = False
        For i = 1 To InputWord.Length
            aChar = Strings.Mid(InputWord, i, 1)
            If (Not IsNumeric(aChar)) Then 'And (aChar <> Space(1)) Then
                If (Asc(aChar) >= 65 And Asc(aChar) <= 90) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Next

    End Function


    Private Sub DisplayAutoCapsStatus(ByVal Status As Boolean)
        On Error Resume Next
        If Status = True Then
            Me.lblAutoCapsStatus.Text = "Auto CAPS: ON"
        Else
            Me.lblAutoCapsStatus.Text = "Auto CAPS: OFF"
        End If
    End Sub
#End Region


#Region "ALLOW NUMBER ONLY"
    Private Sub AllowOnlyNumberKeys(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAutoBackupPeriod.KeyDown ' txtLoadRecordsOfTheYear.KeyDown
        On Error Resume Next
        If e.KeyCode <> Keys.NumPad0 And e.KeyCode <> Keys.NumPad1 And e.KeyCode <> Keys.NumPad2 And e.KeyCode <> Keys.NumPad3 And e.KeyCode <> Keys.NumPad4 And e.KeyCode <> Keys.NumPad5 And e.KeyCode <> Keys.NumPad6 And e.KeyCode <> Keys.NumPad7 And e.KeyCode <> Keys.NumPad8 And e.KeyCode <> Keys.NumPad9 And e.KeyCode <> Keys.D0 And e.KeyCode <> Keys.D1 And e.KeyCode <> Keys.D2 And e.KeyCode <> Keys.D3 And e.KeyCode <> Keys.D4 And e.KeyCode <> Keys.D5 And e.KeyCode <> Keys.D6 And e.KeyCode <> Keys.D7 And e.KeyCode <> Keys.D8 And e.KeyCode <> Keys.D9 And e.KeyCode <> Keys.Back And e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Delete And e.KeyCode <> Keys.Left And e.KeyCode <> Keys.Right And e.KeyCode <> Keys.End And e.KeyCode <> Keys.Home And e.KeyCode <> Keys.Enter Then e.SuppressKeyPress = True
    End Sub

    Private Sub AllowOnlyNumberKeysAndSlash(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSOCNumber.KeyDown, txtDANumber.KeyDown, txtCDNumber.KeyDown
        On Error Resume Next
        If e.KeyCode <> Keys.NumPad0 And e.KeyCode <> Keys.NumPad1 And e.KeyCode <> Keys.NumPad2 And e.KeyCode <> Keys.NumPad3 And e.KeyCode <> Keys.NumPad4 And e.KeyCode <> Keys.NumPad5 And e.KeyCode <> Keys.NumPad6 And e.KeyCode <> Keys.NumPad7 And e.KeyCode <> Keys.NumPad8 And e.KeyCode <> Keys.NumPad9 And e.KeyCode <> Keys.D0 And e.KeyCode <> Keys.D1 And e.KeyCode <> Keys.D2 And e.KeyCode <> Keys.D3 And e.KeyCode <> Keys.D4 And e.KeyCode <> Keys.D5 And e.KeyCode <> Keys.D6 And e.KeyCode <> Keys.D7 And e.KeyCode <> Keys.D8 And e.KeyCode <> Keys.D9 And e.KeyCode <> Keys.Back And e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Delete And e.KeyCode <> Keys.Left And e.KeyCode <> Keys.Right And e.KeyCode <> Keys.Divide And e.KeyCode <> Keys.OemQuestion And e.KeyCode <> Keys.End And e.KeyCode <> Keys.Home Then e.SuppressKeyPress = True

    End Sub
    Private Sub PreventPasting(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSOCNumber.GotFocus, txtDANumber.GotFocus, txtCDNumber.GotFocus, txtAutoBackupPeriod.GotFocus, txtLoadSelectedYearRecords.GotFocus
        On Error Resume Next
        If My.Computer.Clipboard.ContainsText Then
            ClipBoardText = My.Computer.Clipboard.GetText
            My.Computer.Clipboard.Clear()
        End If
    End Sub

    Private Sub RestoreClipBoardText(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSOCNumber.LostFocus, txtDANumber.LostFocus, txtCDNumber.LostFocus, txtAutoBackupPeriod.LostFocus, txtLoadSelectedYearRecords.LostFocus
        On Error Resume Next
        If ClipBoardText <> "" Then My.Computer.Clipboard.SetText(ClipBoardText)
    End Sub
#End Region


#Region "ENABLE OR DISABLE CONTROLS"
    Private Sub DisableControls()
        On Error Resume Next
        Dim ctrl As Control
        For Each ctrl In Me.Controls
            ctrl.Enabled = False
        Next
        Me.RibbonControl1.Enabled = True
        Me.btnNewEntry.Enabled = False
        Me.btnOpen.Enabled = False
        Me.btnEdit.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnViewReports.Enabled = False
        Me.btnReload.Enabled = False
        Me.btnSearchMain.Enabled = False
        Me.btnShowHideFields.Enabled = False

        Me.RibbonTabItem1.Enabled = False
        Me.RibbonTabItem2.Enabled = False
        Me.RibbonTabItem3.Enabled = False
        Me.RibbonTabItem4.Enabled = False
        Me.RibbonTabItem5.Enabled = False
        Me.RibbonTabItem6.Enabled = False

        Me.btnLocalBackup.Enabled = True
        Me.btnOnlineBackup.Enabled = True

    End Sub

    Private Sub DisableControlsInPreviewMode()
        On Error Resume Next
        ShowAllDataEntryFields(False)
        Me.RibbonControl1.Enabled = True
        Me.btnNewEntry.Enabled = False
        Me.btnOpen.Enabled = False
        Me.btnEdit.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnShowHideFields.Enabled = False
        Me.btnSavePS.Enabled = False
        Me.btnSaveIO.Enabled = False
        Me.btnSaveSS.Enabled = False
        Me.btnSaveOfficeSettings.Enabled = False

    End Sub
    Private Sub EnableControls()
        On Error Resume Next
        Dim ctrl As Control
        For Each ctrl In Me.Controls
            ctrl.Enabled = True
        Next
        Me.RibbonControl1.Enabled = True
        Me.btnNewEntry.Enabled = True
        Me.btnOpen.Enabled = True
        Me.btnEdit.Enabled = True
        Me.btnDelete.Enabled = True
        Me.btnViewReports.Enabled = True
        Me.btnReload.Enabled = True
        Me.btnOnlineBackup.Enabled = True
        Me.btnSearchMain.Enabled = True
        Me.btnShowHideFields.Enabled = True

        Me.RibbonTabItem1.Enabled = True
        Me.RibbonTabItem2.Enabled = True
        Me.RibbonTabItem3.Enabled = True
        Me.RibbonTabItem4.Enabled = True
        Me.RibbonTabItem5.Enabled = True
        Me.RibbonTabItem6.Enabled = True

    End Sub
#End Region

    '---------------------------------------------SLIP IMAGE GENERAL SETTINGS----------------------------------

#Region "FP SLIP IMAGE GENERAL SETTINGS"

    Private Sub CheckDeviceConnectedStatus(ByVal EventID As String, ByVal DeviceID As String, ByVal ItemID As String) Handles devmanager.OnEvent
        On Error Resume Next

        Dim i As Integer
        If EventID = WIA.EventID.wiaEventDeviceConnected Then
            If Me.devmanager.DeviceInfos.Count = 1 Then
                If Me.devmanager.DeviceInfos.Item(1).Type = WIA.WiaDeviceType.CameraDeviceType Then
                    ShowDesktopAlert("Compatible Camera Connected!")
                End If

                If Me.devmanager.DeviceInfos.Item(1).Type = WIA.WiaDeviceType.ScannerDeviceType Then
                    ShowDesktopAlert("Compatible Scanner Connected!")
                End If

            ElseIf Me.devmanager.DeviceInfos.Count > 1 Then
                For i = 1 To Me.devmanager.DeviceInfos.Count
                    If Me.devmanager.DeviceInfos.Item(i).Type = WIA.WiaDeviceType.CameraDeviceType Or WIA.WiaDeviceType.ScannerDeviceType Then ShowDesktopAlert("Compatible Device Connected!")
                    Exit For
                Next
            End If

        End If
    End Sub


    Private Function ImportImageFromScannerOrCamera(ByVal SaveLocation As String, Optional ByVal FileName As String = vbNullString) As String

        Try

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        If Me.devmanager.DeviceInfos.Count = 0 Then
            MessageBoxEx.Show("No compatible Scanner or Camera Device detected. Please connect one!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return vbNullString
            Exit Function
        End If


        If My.Computer.FileSystem.FileExists(SaveLocation & FileName & ".jpeg") Then
            Dim msg As String = vbNullString
            Select Case CurrentTab
                Case "DA"
                    msg = "The DA Slip image " & FileName & ".jpeg" & " already exists in the DA Slip Image Location. Do you want to replace it?"
                Case "ID"
                    msg = "The Identified Slip image " & FileName & ".jpeg" & " already exists in the Identified Slip Image Location. Do you want to replace it?"
                Case "AC"
                    msg = "The Active Criminal Slip image " & FileName & ".jpeg" & " already exists in the Active Criminal Slip Image Location. Do you want to replace it?"
            End Select
            Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show(msg, strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If reply = Windows.Forms.DialogResult.No Then
                Return vbNullString
                Exit Function
            End If
        End If

        Dim dev As WIA.Device
        Dim dg As New WIA.CommonDialog
        Dim SelectedItems As WIA.Items
        Dim img As WIA.ImageFile
        Dim itm As WIA.Item

        dev = dg.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, False, True) 'show select device message
        SelectedItems = dg.ShowSelectItems(dev, WIA.WiaImageIntent.UnspecifiedIntent, WIA.WiaImageBias.MaximizeQuality, True, True, True) 'show the pictures in the device selected

        itm = SelectedItems(1)
        If FileName = vbNullString Then FileName = itm.Properties("Item Name").Value 'use the original name
        img = dg.ShowTransfer(itm, , True) 'transfer the picture to imgfile
        Dim saved As Boolean
        saved = SaveImage(img, SaveLocation, FileName)
        If saved = False Then
            Return vbNullString
        Else
            Return SaveLocation & FileName & ".jpeg" 'return the Photo
        End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Return vbNullString
        End Try

    End Function


    Private Function SaveImage(ByVal img As WIA.ImageFile, ByVal SaveLocation As String, ByVal FileName As String) As Boolean

        On Error Resume Next
        CreateFolder(SaveLocation)

        If Strings.Right(SaveLocation, 1) <> "\" Then SaveLocation = SaveLocation & "\"
        If Not img Is Nothing Then

            Dim tempfile As String = SaveLocation & "temp" & "." & img.FileExtension.ToLower
            If My.Computer.FileSystem.FileExists(tempfile) Then
                FileIO.FileSystem.DeleteFile(tempfile)
            End If
            img.SaveFile(tempfile)

            Dim x As Bitmap = New Bitmap(tempfile)
            x.SetResolution(Int(x.HorizontalResolution), Int(x.VerticalResolution))
            ' FileIO.FileSystem.DeleteFile(SaveLocation & FileName & ".jpeg")
            x.Save(SaveLocation & FileName & ".jpeg", ImageFormat.Jpeg)
            x.Dispose()
            FileIO.FileSystem.DeleteFile(tempfile)
            Return True
        Else
            Return False
        End If

    End Function



    Private Sub SetFPSlipImageImportLocation() Handles btnChangeFPSlipImageLocation.Click
        On Error Resume Next
        GetFPSlipImageImportLocation()
        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location to save scanned FP Slips"
        Me.FolderBrowserDialog1.SelectedPath = FPImageImportLocation
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then
            FPImageImportLocation = Me.FolderBrowserDialog1.SelectedPath
            If FPImageImportLocation.EndsWith("Scanned FP Slips") = False Then
                FPImageImportLocation = FPImageImportLocation & "\Scanned FP Slips"
            End If
            FPImageImportLocation = FPImageImportLocation.Replace("\\", "\")
            Dim id = 1
            Me.SettingsTableAdapter1.SetFPImageLocation(FPImageImportLocation, id)
            GetFPSlipImageImportLocation()
            My.Computer.FileSystem.CreateDirectory(DASlipImageImportLocation)
            My.Computer.FileSystem.CreateDirectory(IDSlipImageImportLocation)
            My.Computer.FileSystem.CreateDirectory(ACSlipImageImportLocation)
            ShowDesktopAlert("Scanned FP Slips location changed!")
        End If
    End Sub


    Private Sub GetFPSlipImageImportLocation()
        On Error Resume Next
        FPImageImportLocation = FPImageImportLocation.Replace("\\", "\")
        DASlipImageImportLocation = FPImageImportLocation & "\DA Slips\"
        IDSlipImageImportLocation = FPImageImportLocation & "\Identified Slips\"
        ACSlipImageImportLocation = FPImageImportLocation & "\Active Criminal Slips\"
    End Sub


    Private Sub ExploreFPSlipImageImportLocation() Handles btnExploreFPSlipImageLocation.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FileIO.FileSystem.DirectoryExists(FPImageImportLocation) Then
            Call Shell("explorer.exe " & FPImageImportLocation, AppWinStyle.NormalFocus)
        Else
            FileIO.FileSystem.CreateDirectory(FPImageImportLocation)
            Call Shell("explorer.exe " & FPImageImportLocation, AppWinStyle.NormalFocus)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LocateFPSlipImage() Handles btnLocateFPSlip.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim location As String = vbNullString
        Select Case CurrentTab
            Case "DA"
                location = Me.DADatagrid.SelectedCells(15).Value.ToString
                If location <> vbNullString Then
                    If FileIO.FileSystem.FileExists(location) Then
                        Call Shell("explorer.exe /select," & location, AppWinStyle.NormalFocus)
                    Else
                        MessageBoxEx.Show("The specified DA Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                Else
                    MessageBoxEx.Show("No image to locate!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Case "ID"
                location = Me.IDDatagrid.SelectedCells(15).Value.ToString
                If location <> vbNullString Then
                    If FileIO.FileSystem.FileExists(location) Then
                        Call Shell("explorer.exe /select," & location, AppWinStyle.NormalFocus)
                    Else
                        MessageBoxEx.Show("The specified Identified Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                Else
                    MessageBoxEx.Show("No image to locate!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Case "AC"
                location = Me.ACDatagrid.SelectedCells(14).Value.ToString
                If location <> vbNullString Then
                    If FileIO.FileSystem.FileExists(location) Then
                        Call Shell("explorer.exe /select," & location, AppWinStyle.NormalFocus)
                    Else
                        MessageBoxEx.Show("The specified Active Criminal Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                Else
                    MessageBoxEx.Show("No image to locate!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

        End Select

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub ViewImagesOnClickingIcon() Handles lblHasFPSlip.Click
        On Error Resume Next
        If CurrentTab = "SOC" Then
            ViewChancePrints()
        End If

        If CurrentTab = "DA" Then
            ViewDASlipImage()
        End If

        If CurrentTab = "AC" Then
            ViewACSlipImage()
        End If

        If CurrentTab = "ID" Then
            ViewIDSlipImage()
        End If


    End Sub

#End Region


#Region "IMPORT FP SLIPS FROM CONTEXT MENU"

    Private Sub ImportFPSlipFromContext() Handles btnImportFPSlipContext.Click
        On Error Resume Next

        Dim currentfile As String = ""
        If CurrentTab = "DA" Then
            currentfile = Me.DADatagrid.SelectedCells(15).Value.ToString
        End If
        If CurrentTab = "ID" Then
            currentfile = Me.IDDatagrid.SelectedCells(15).Value.ToString
        End If
        If CurrentTab = "AC" Then
            currentfile = Me.ACDatagrid.SelectedCells(14).Value.ToString
        End If




        If FileIO.FileSystem.FileExists(currentfile) Then
            Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("There is already an image file associated with the selected record. Do you want to replace it?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If


        If CurrentTab = "DA" Then
            Dim DANo As String = Me.DADatagrid.SelectedCells(0).Value.ToString()
            Dim FileName As String = "DANo." & DANo
            FileName = Strings.Replace(FileName, "/", "-")
            Dim yr As String = Year(Me.DADatagrid.SelectedCells(2).Value)

            If Strings.Right(DASlipImageImportLocation, 1) <> "\" Then DASlipImageImportLocation = DASlipImageImportLocation & "\"
            Dim SaveLocation As String = DASlipImageImportLocation & yr & "\"
            Dim ScannedImage As String = ImportImageFromScannerOrCamera(SaveLocation, FileName) 'scans the picture and returns the file name with path

            If ScannedImage <> vbNullString Then
                DASlipImageFile = ScannedImage

                Dim oldRow As FingerPrintDataSet.DARegisterRow 'add a new row to insert values
                oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(DANo)
                If oldRow IsNot Nothing Then
                    With oldRow
                        .SlipFile = DASlipImageFile
                    End With
                End If

                Me.DARegisterTableAdapter.UpdateSlipFile(DASlipImageFile, DANo)

                ShowDesktopAlert("Imported one Image")
                DASlipImageFile = ""
            End If
        End If



        If CurrentTab = "ID" Then
            Dim IDNo As String = Me.IDDatagrid.SelectedCells(0).Value
            Dim FileName As String = "IDNo." & Format(CInt(IDNo), "0000")
            If Strings.Right(IDSlipImageImportLocation, 1) <> "\" Then IDSlipImageImportLocation = IDSlipImageImportLocation & "\"
            Dim ScannedImage As String = ImportImageFromScannerOrCamera(IDSlipImageImportLocation, FileName) 'scans the picture and returns the file name with path

            If ScannedImage <> vbNullString Then
                IDSlipImageFile = ScannedImage

                Dim oldRow As FingerPrintDataSet.IdentifiedSlipsRegisterRow 'add a new row to insert values
                oldRow = Me.FingerPrintDataSet.IdentifiedSlipsRegister.FindByIDNumber(IDNo)
                If oldRow IsNot Nothing Then
                    With oldRow
                        .SlipFile = IDSlipImageFile
                    End With
                End If

                Me.IDRegisterTableAdapter.UpdateSlipFile(IDSlipImageFile, IDNo)

                ShowDesktopAlert("Imported one Image")
                IDSlipImageFile = ""
            End If
        End If



        If CurrentTab = "AC" Then
            Dim ACNo As String = Me.ACDatagrid.SelectedCells(0).Value
            Dim FileName As String = "ACNo." & Format(CInt(ACNo), "0000")
            If Strings.Right(ACSlipImageImportLocation, 1) <> "\" Then ACSlipImageImportLocation = ACSlipImageImportLocation & "\"
            Dim ScannedImage As String = ImportImageFromScannerOrCamera(ACSlipImageImportLocation, FileName) 'scans the picture and returns the file name with path

            If ScannedImage <> vbNullString Then
                ACSlipImageFile = ScannedImage

                Dim oldRow As FingerPrintDataSet.ActiveCriminalsRegisterRow 'add a new row to insert values
                oldRow = Me.FingerPrintDataSet.ActiveCriminalsRegister.FindByACNumber(ACNo)
                If oldRow IsNot Nothing Then
                    With oldRow
                        .SlipFile = ACSlipImageFile
                    End With
                End If

                Me.ACRegisterTableAdapter.UpdateSlipFile(ACSlipImageFile, ACNo)

                ShowDesktopAlert("Imported one Image")
                ACSlipImageFile = ""
            End If
        End If
        DisplayDatabaseInformation()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

    End Sub

    Private Sub SelectFPSlipFromContext() Handles btnSelectFPSlipContext.Click
        Try

            Dim currentfile As String = ""
            If CurrentTab = "DA" Then
                currentfile = Me.DADatagrid.SelectedCells(15).Value.ToString
            End If
            If CurrentTab = "ID" Then
                currentfile = Me.IDDatagrid.SelectedCells(15).Value.ToString
            End If
            If CurrentTab = "AC" Then
                currentfile = Me.ACDatagrid.SelectedCells(14).Value.ToString
            End If




            If FileIO.FileSystem.FileExists(currentfile) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("There is already an image file associated with the selected record. Do you want to replace it?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If r = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If



            If CurrentTab = "DA" Then
                OpenFileDialog1.InitialDirectory = DASlipImageImportLocation
            End If
            If CurrentTab = "ID" Then
                OpenFileDialog1.InitialDirectory = IDSlipImageImportLocation
            End If
            If CurrentTab = "AC" Then
                OpenFileDialog1.InitialDirectory = ACSlipImageImportLocation
            End If

            OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;*.tiff"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select FP Slip Image File"
            OpenFileDialog1.AutoUpgradeEnabled = True
            OpenFileDialog1.RestoreDirectory = True 'remember last directory
            Dim SelectedFile As String
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
                Application.DoEvents() 'first close the selection window
                SelectedFile = OpenFileDialog1.FileName
                Dim getInfo As System.IO.DriveInfo = My.Computer.FileSystem.GetDriveInfo(SelectedFile)

                '------------------------------------- DA -------------------------------------------

                If CurrentTab = "DA" Then
                    Dim DANo As String = Me.DADatagrid.SelectedCells(0).Value.ToString()
                    If getInfo.DriveType <> IO.DriveType.Fixed Then
                        Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The FP Slip Image File you selected is on a removable media. Do you want to copy it to the DA Slip Image Files Location?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If r = Windows.Forms.DialogResult.Yes Then
                            Dim yr As String = Year(Me.DADatagrid.SelectedCells(2).Value)
                            If Strings.Right(DASlipImageImportLocation, 1) <> "\" Then DASlipImageImportLocation = DASlipImageImportLocation & "\"
                            Dim DestinationFile As String = DASlipImageImportLocation & yr & "\" & OpenFileDialog1.SafeFileName
                            My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                            SelectedFile = DestinationFile
                        End If

                        If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    End If

                    DASlipImageFile = SelectedFile

                    Dim oldRow As FingerPrintDataSet.DARegisterRow 'add a new row to insert values
                    oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(DANo)
                    If oldRow IsNot Nothing Then
                        With oldRow
                            .SlipFile = DASlipImageFile
                        End With
                    End If

                    Me.DARegisterTableAdapter.UpdateSlipFile(DASlipImageFile, DANo)
                    DASlipImageFile = ""
                    ShowDesktopAlert("DA Slip Image file updated")
                End If


                '------------------------------------- ID -------------------------------------------


                If CurrentTab = "ID" Then
                    Dim IDNo As String = Me.IDDatagrid.SelectedCells(0).Value.ToString()
                    If getInfo.DriveType <> IO.DriveType.Fixed Then

                        Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The Identified Slip Image File you selected is on a removable media. Do you want to copy it to the Identified Slips Image Files Location?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If r = Windows.Forms.DialogResult.Yes Then
                            If Strings.Right(IDSlipImageImportLocation, 1) <> "\" Then IDSlipImageImportLocation = IDSlipImageImportLocation & "\"
                            Dim DestinationFile As String = IDSlipImageImportLocation & OpenFileDialog1.SafeFileName
                            My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                            SelectedFile = DestinationFile
                        End If

                        ' If reply=vbNo then do nothing just use the selected file

                        If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    End If

                    IDSlipImageFile = SelectedFile
                    Dim oldRow As FingerPrintDataSet.IdentifiedSlipsRegisterRow 'add a new row to insert values
                    oldRow = Me.FingerPrintDataSet.IdentifiedSlipsRegister.FindByIDNumber(IDNo)
                    If oldRow IsNot Nothing Then
                        With oldRow
                            .SlipFile = IDSlipImageFile
                        End With
                    End If

                    Me.IDRegisterTableAdapter.UpdateSlipFile(IDSlipImageFile, IDNo)
                    IDSlipImageFile = ""
                    ShowDesktopAlert("Identified Slip Image file updated")
                End If


                '------------------------------------- AC -------------------------------------------


                If CurrentTab = "AC" Then
                    Dim ACNo As String = Me.ACDatagrid.SelectedCells(0).Value.ToString()
                    If getInfo.DriveType <> IO.DriveType.Fixed Then

                        Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The Active Criminal Slip Image File you selected is on a removable media. Do you want to copy it to the Active Criminal Slips Image Files Location?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If r = Windows.Forms.DialogResult.Yes Then
                            If Strings.Right(ACSlipImageImportLocation, 1) <> "\" Then ACSlipImageImportLocation = ACSlipImageImportLocation & "\"
                            Dim DestinationFile As String = ACSlipImageImportLocation & OpenFileDialog1.SafeFileName
                            My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                            SelectedFile = DestinationFile
                        End If

                        ' If reply=vbNo then do nothing just use the selected file

                        If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    End If

                    ACSlipImageFile = SelectedFile
                    Dim oldRow As FingerPrintDataSet.ActiveCriminalsRegisterRow 'add a new row to insert values
                    oldRow = Me.FingerPrintDataSet.ActiveCriminalsRegister.FindByACNumber(ACNo)
                    If oldRow IsNot Nothing Then
                        With oldRow
                            .SlipFile = ACSlipImageFile
                        End With
                    End If

                    Me.ACRegisterTableAdapter.UpdateSlipFile(ACSlipImageFile, ACNo)
                    ACSlipImageFile = ""
                    ShowDesktopAlert("Active Criminal Slip Image file updated")
                End If

            End If
            DisplayDatabaseInformation()

        Catch ex As Exception
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

    '---------------------------------------------DEFAULT BUTTON ACTIONS---------------------------------------

#Region "HOT KEY ACTIONS"
    Private Sub SaveHotKey() Handles btnDummySave.Click
        On Error Resume Next
        If CurrentTab = "SOC" Then
            If Me.PanelSOC.Visible = False Then Exit Sub
            btnSaveSOC.Focus()
            Call SOCSaveButtonAction()
        End If
        If CurrentTab = "RSOC" Then
            If Me.PanelRSOC.Visible = False Then Exit Sub
            btnSaveRSOC.Focus()
            Call RSOCSaveButtonAction()
        End If

        If CurrentTab = "DA" Then
            If Me.PanelDA.Visible = False Then Exit Sub
            btnSaveDA.Focus()
            Call DASaveButtonAction()
        End If

        If CurrentTab = "PS" Then
            btnSavePS.Focus()
            Call PSSaveButtonAction()
        End If

        If CurrentTab = "CD" Then
            If Me.PanelCD.Visible = False Then Exit Sub
            btnSaveCD.Focus()
            Call CDSaveButtonAction()
        End If

        If CurrentTab = "ID" Then
            If Me.PanelID.Visible = False Then Exit Sub
            btnSaveID.Focus()
            Call IDSaveButtonAction()
        End If

        If CurrentTab = "AC" Then
            If Me.PanelAC.Visible = False Then Exit Sub
            btnSaveAC.Focus()
            Call ACSaveButtonAction()
        End If

        If CurrentTab = "OS" Then
            btnSaveOfficeSettings.Focus()
            Call SaveOfficeSettings()
        End If

        If CurrentTab = "IO" Then
            btnSaveIO.Focus()
            Call SaveOfficerList()
        End If
    End Sub

    Private Sub SearchHotKey() Handles btnDummySearch.Click
        On Error Resume Next
        If CurrentTab = "SOC" And PanelSOC.Visible Then
            Call SearchSOC()
        End If
        If CurrentTab = "RSOC" And PanelRSOC.Visible Then
            Call SearchRSOC()
        End If
        If CurrentTab = "DA" And PanelDA.Visible Then
            Call SearchDA()
        End If
        If CurrentTab = "CD" And PanelCD.Visible Then
            Call SearchCD()
        End If

        If CurrentTab = "ID" And PanelID.Visible Then
            Call SearchID()
        End If

        If CurrentTab = "AC" And PanelAC.Visible Then
            Call SearchAC()
        End If

    End Sub

    Private Sub ClearHotKey() Handles btnDummyClear.Click
        On Error Resume Next
        If CurrentTab = "SOC" Then
            Call ClearSOCFields()
        End If
        If CurrentTab = "RSOC" Then
            Call ClearRSOCFields()
        End If

        If CurrentTab = "DA" Then
            Call ClearDAFields()
        End If
        If CurrentTab = "CD" Then
            Call ClearCDFields()
        End If
        If CurrentTab = "ID" Then
            Call ClearIDFields()
        End If
        If CurrentTab = "AC" Then
            Call ClearACFields()
        End If
    End Sub

#End Region


#Region "NEW BUTTON ACTION"
    Private Sub NewDataMode() Handles btnNewEntry.Click
        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub
        On Error Resume Next

        If CurrentTab = "SOC" Then
            Me.PanelSOC.Visible = True
            SOCEditMode = False
            ClearSOCFields()
            GenerateNewSOCNumber()
            Me.txtSOCYear.Text = Year(Today)
            Me.dtSOCInspection.Value = Today
            Me.dtSOCReport.Value = Today
            Me.btnSaveSOC.Text = "Save"
        End If

        If CurrentTab = "RSOC" Then
            Me.PanelRSOC.Visible = True
            RSOCEditMode = False
            ClearRSOCFields()
            GenerateNewRSOCSerialNumber()
            Me.dtRSOCInspection.Text = ""
            Me.dtRSOCReportSentOn.Value = Today
            Me.btnSaveRSOC.Text = "Save"
        End If


        If CurrentTab = "DA" Then
            Me.PanelDA.Visible = True
            DAEditMode = False
            ClearDAFields()
            GenerateNewDANumber()
            Me.txtDAYear.Text = Year(Today)
            Me.dtDAEntry.Value = Today
            Me.btnSaveDA.Text = "Save"
            cmbDASex.SelectedIndex = 1
        End If

        If CurrentTab = "ID" Then
            Me.PanelID.Visible = True
            IDEditMode = False
            ClearIDFields()
            GenerateNewIDNumber()
            Me.btnSaveID.Text = "Save"
            cmbIDSex.SelectedIndex = 1
        End If

        If CurrentTab = "AC" Then
            Me.PanelAC.Visible = True
            ACEditMode = False
            ClearACFields()
            GenerateNewACNumber()
            Me.btnSaveAC.Text = "Save"
            cmbACSex.SelectedIndex = 1
        End If


        If CurrentTab = "FPA" Then
            Me.Cursor = Cursors.WaitCursor
            blFPAEditMode = False
            frmFPADE.Show()
            frmFPADE.BringToFront()
            Cursor = Cursors.Default
        End If

        If CurrentTab = "PS" Then
            PSEditMode = False
            ClearPSFields()
            Me.btnSavePS.Text = "Save"
        End If


        If CurrentTab = "CD" Then
            Me.PanelCD.Visible = True
            CDEditMode = False
            ClearCDFields()
            GenerateNewCDNumber()
            Me.txtCDYear.Text = Year(Today)
            Me.dtCDExamination.Value = Today
            Me.btnSaveCD.Text = "Save"
        End If

        If CurrentTab = "IO" Then
            ClearIOFields()
        End If

        If CurrentTab = "SS" Then
            SSEditMode = False
            ClearSSFields()
            Me.btnSaveSS.Text = "Save"
        End If

        If CurrentTab = "OS" Then
            OfficeSettingsEditMode(True)
            Me.txtFullOffice.Focus()
        End If

        If CurrentTab = "IDR" Then
            Me.Cursor = Cursors.WaitCursor
            blIDRNewDataMode = True
            blIDREditMode = False

            FrmIdentificationRegisterDE.Show()
            FrmIdentificationRegisterDE.BringToFront()
            Cursor = Cursors.Default
        End If
    End Sub

#End Region


#Region "EDIT BUTTON ACTION"
    Private Sub OpenRecordForEditing() Handles btnEdit.Click, btnEditContext.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        On Error Resume Next

        If CurrentTab = "SOC" Then

            If Me.SOCDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.SOCDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.PanelSOC.Visible = True
            SOCEditMode = True
            Me.btnSaveSOC.Text = "Update"
            ClearSOCFields()

            With Me.SOCDatagrid
                Me.txtSOCNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtSOCNumberOnly.Text = .SelectedCells(1).Value.ToString
                Me.dtSOCInspection.ValueObject = .SelectedCells(2).Value
                Me.dtSOCReport.ValueObject = .SelectedCells(3).Value
                Me.dtSOCOccurrence.Text = .SelectedCells(4).Value.ToString
                Me.cmbSOCPoliceStation.Text = .SelectedCells(5).Value.ToString
                Me.txtSOCCrimeNumber.Text = .SelectedCells(6).Value.ToString
                Me.txtSOCSection.Text = .SelectedCells(7).Value.ToString
                Me.txtSOCPlace.Text = .SelectedCells(8).Value.ToString
                Me.txtSOCOfficer.Text = .SelectedCells(9).Value.ToString.Replace(vbNewLine, "; ")
                Me.txtSOCCPsDeveloped.Text = .SelectedCells(10).Value.ToString
                Me.txtSOCCPsUnfit.Text = .SelectedCells(11).Value.ToString
                Me.txtSOCCPsEliminated.Text = .SelectedCells(12).Value.ToString

                CPIdentified = Val(.SelectedCells(13).Value)

                Me.txtSOCCPDetails.Text = .SelectedCells(14).Value.ToString
                Me.txtSOCComplainant.Text = .SelectedCells(15).Value.ToString()
                Me.cmbSOCModus.Text = .SelectedCells(16).Value.ToString
                Me.txtSOCPropertyLost.Text = .SelectedCells(17).Value.ToString
                Me.txtSOCPhotographer.Text = .SelectedCells(18).Value.ToString
                Me.cmbSOCPhotoReceived.Text = .SelectedCells(19).Value.ToString
                Me.txtSOCDateOfPhotography.Text = .SelectedCells(20).Value.ToString
                Me.txtSOCGist.Text = .SelectedCells(21).Value.ToString
                Me.txtSOCComparisonDetails.Text = .SelectedCells(22).Value.ToString
                Me.chkGraveCrime.Checked = .SelectedCells(23).Value
                Me.cmbFileStatus.Text = .SelectedCells(24).Value.ToString
            End With
            OriginalSOCNumber = Me.txtSOCNumber.Text
            Me.txtSOCNumber.Focus()
            Me.txtSOCYear.Text = Year(Me.dtSOCInspection.Value)
            TickOfficerList(Me.txtSOCOfficer.Text)
        End If




        If CurrentTab = "RSOC" Then

            If Me.RSOCDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.RSOCDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.PanelRSOC.Visible = True
            RSOCEditMode = True
            Me.btnSaveRSOC.Text = "Update"

            With Me.RSOCDatagrid
                Me.txtRSOCSerialNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtRSOCNumber.Text = .SelectedCells(1).Value.ToString
                Me.txtRSOCNumberOnly.Text = .SelectedCells(2).Value.ToString
                Me.dtRSOCInspection.ValueObject = .SelectedCells(3).Value
                Me.cmbRSOCPoliceStation.Text = .SelectedCells(4).Value.ToString
                Me.txtRSOCCrimeNumber.Text = .SelectedCells(5).Value.ToString
                Me.cmbRSOCOfficer.Text = .SelectedCells(6).Value.ToString
                Me.txtRSOCReportSentTo.Text = .SelectedCells(7).Value.ToString
                Me.dtRSOCReportSentOn.ValueObject = .SelectedCells(8).Value.ToString
                Me.cmbRSOCNatureOfReport.Text = .SelectedCells(9).Value.ToString
                Me.txtRSOCDespatchNumber.Text = .SelectedCells(10).Value.ToString
                Me.txtRSOCRemarks.Text = .SelectedCells(11).Value.ToString
            End With
            OriginalRSOCSerialNumber = Me.txtRSOCSerialNumber.Text
            Me.txtRSOCSerialNumber.Focus()

        End If





        If CurrentTab = "FPA" Then

            If Me.FPADataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.FPADataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            blFPAEditMode = True
            Me.Cursor = Cursors.WaitCursor
            frmFPADE.Show()
            frmFPADE.BringToFront()
            Me.Cursor = Cursors.Default

        End If

        If CurrentTab = "DA" Then


            If Me.DADatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DADatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.PanelDA.Visible = True
            DAEditMode = True
            ClearDAImage()
            Me.btnSaveDA.Text = "Update"
            With Me.DADatagrid
                Me.txtDANumber.Text = .SelectedCells(0).Value.ToString
                Me.txtDANumberOnly.Text = .SelectedCells(1).Value.ToString
                Me.dtDAEntry.ValueObject = .SelectedCells(2).Value
                Me.cmbDAPoliceStation.Text = .SelectedCells(3).Value.ToString
                Me.txtDACrimeNumber.Text = .SelectedCells(4).Value.ToString
                Me.txtDASection.Text = .SelectedCells(5).Value.ToString
                Me.txtDAName.Text = .SelectedCells(6).Value.ToString
                Me.txtDAAliasName.Text = .SelectedCells(7).Value.ToString()
                Me.txtDAFathersName.Text = .SelectedCells(8).Value.ToString
                Me.cmbDASex.Text = .SelectedCells(9).Value.ToString
                Me.txtDAAddress.Text = .SelectedCells(10).Value.ToString
                Me.txtDAHenryNumerator.Text = .SelectedCells(11).Value.ToString
                Me.txtDAHenryDenominator.Text = .SelectedCells(12).Value.ToString
                Me.txtDAModusOperandi.Text = .SelectedCells(13).Value.ToString
                Me.txtDARemarks.Text = .SelectedCells(14).Value.ToString
                DASlipImageFile = .SelectedCells(15).Value.ToString
            End With
            If FileIO.FileSystem.FileExists(DASlipImageFile) = True Then
                Me.picDASlip.Image = New Bitmap(DASlipImageFile)
            Else
                Me.picDASlip.Image = My.Resources.NoDAImage
            End If
            OriginalDANumber = Me.txtDANumber.Text
            Me.txtDANumber.Focus()
            Me.txtDAYear.Text = Year(Me.dtDAEntry.Value)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End If



        If CurrentTab = "ID" Then


            If Me.IDDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.IDDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.PanelID.Visible = True
            IDEditMode = True
            ClearIDImage()
            Me.btnSaveID.Text = "Update"
            With Me.IDDatagrid
                Me.txtIDNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtIDDANumber.Text = .SelectedCells(1).Value.ToString
                Me.cmbIDPoliceStation.Text = .SelectedCells(2).Value.ToString
                Me.txtIDCrimeNumber.Text = .SelectedCells(3).Value.ToString
                Me.txtIDSection.Text = .SelectedCells(4).Value.ToString
                Me.txtIDName.Text = .SelectedCells(5).Value.ToString
                Me.txtIDAliasName.Text = .SelectedCells(6).Value.ToString()
                Me.txtIDFathersName.Text = .SelectedCells(7).Value.ToString
                Me.cmbIDSex.Text = .SelectedCells(8).Value.ToString
                Me.txtIDAddress.Text = .SelectedCells(9).Value.ToString
                Me.txtIDHenryNumerator.Text = .SelectedCells(10).Value.ToString
                Me.txtIDHenryDenominator.Text = .SelectedCells(11).Value.ToString
                Me.txtIDModusOperandi.Text = .SelectedCells(12).Value.ToString
                Me.txtIDDetails.Text = .SelectedCells(13).Value.ToString
                Me.txtIDRemarks.Text = .SelectedCells(14).Value.ToString
                IDSlipImageFile = .SelectedCells(15).Value.ToString
            End With
            If FileIO.FileSystem.FileExists(IDSlipImageFile) = True Then
                Me.picIDSlip.Image = New Bitmap(IDSlipImageFile)
            Else
                Me.picIDSlip.Image = My.Resources.NoDAImage
            End If
            OriginalIDNumber = Me.txtIDNumber.Text
            Me.txtIDNumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End If





        If CurrentTab = "AC" Then


            If Me.ACDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.ACDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.PanelAC.Visible = True
            ACEditMode = True
            ClearACImage()
            Me.btnSaveAC.Text = "Update"
            With Me.ACDatagrid
                Me.txtACNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtACDANumber.Text = .SelectedCells(1).Value.ToString
                Me.cmbACPoliceStation.Text = .SelectedCells(2).Value.ToString
                Me.txtACCrimeNumber.Text = .SelectedCells(3).Value.ToString
                Me.txtACSection.Text = .SelectedCells(4).Value.ToString
                Me.txtACName.Text = .SelectedCells(5).Value.ToString
                Me.txtACAliasName.Text = .SelectedCells(6).Value.ToString()
                Me.txtACFathersName.Text = .SelectedCells(7).Value.ToString
                Me.cmbACSex.Text = .SelectedCells(8).Value.ToString
                Me.txtACAddress.Text = .SelectedCells(9).Value.ToString
                Me.txtACHenryNumerator.Text = .SelectedCells(10).Value.ToString
                Me.txtACHenryDenominator.Text = .SelectedCells(11).Value.ToString
                Me.txtACModusOperandi.Text = .SelectedCells(12).Value.ToString
                Me.txtACRemarks.Text = .SelectedCells(13).Value.ToString
                ACSlipImageFile = .SelectedCells(14).Value.ToString
            End With
            If FileIO.FileSystem.FileExists(ACSlipImageFile) = True Then
                Me.picACSlip.Image = New Bitmap(ACSlipImageFile)
            Else
                Me.picACSlip.Image = My.Resources.NoDAImage
            End If
            OriginalACNumber = Me.txtACNumber.Text
            Me.txtACNumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End If




        If CurrentTab = "CD" Then


            If Me.CDDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.CDDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.PanelCD.Visible = True
            CDEditMode = True
            Me.btnSaveCD.Text = "Update"

            With Me.CDDataGrid
                Me.txtCDNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtCDNumberOnly.Text = .SelectedCells(1).Value.ToString
                Me.dtCDExamination.ValueObject = .SelectedCells(2).Value
                Me.cmbCDOfficer.Text = .SelectedCells(3).Value.ToString()
                Me.txtCourt.Text = .SelectedCells(4).Value.ToString
                Me.txtCCNumber.Text = .SelectedCells(5).Value.ToString
                Me.cmbCDPoliceStation.Text = .SelectedCells(6).Value.ToString
                Me.txtCDCrNo.Text = .SelectedCells(7).Value.ToString
                Me.txtCDDetails.Text = .SelectedCells(8).Value.ToString()
                Me.txtCDRemarks.Text = .SelectedCells(9).Value.ToString
            End With
            OriginalCDNumber = Me.txtCDNumber.Text
            Me.txtCDYear.Text = Year(Me.dtCDExamination.Value)
            Me.txtCDNumber.Focus()
        End If


        If CurrentTab = "PS" Then


            If Me.PSDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.PSDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            PSEditMode = True
            Me.btnSavePS.Text = "Update"
            With Me.PSDataGrid
                Me.txtPSName.Text = .SelectedCells(0).Value.ToString
                Me.cmbSHO.Text = .SelectedCells(1).Value.ToString
                Me.txtPhoneNumber1.Text = .SelectedCells(2).Value.ToString
                Me.txtPhoneNumber2.Text = .SelectedCells(3).Value.ToString
                Dim d As String = .SelectedCells(4).Value.ToString
                Me.txtDistance.Text = vbNullString
                Me.txtDistance.Text = Strings.Left(d, Len(d) - 3)
            End With
            OriginalPSName = Me.txtPSName.Text
            Me.txtPSName.Focus()
            Me.txtPSName.SelectAll()
        End If

        If CurrentTab = "IO" Then
            With Me.IODatagrid.SelectedRows(0)
                IOSelectedRow = .Index
                Me.lblDesignation.Text = .Cells(0).Value.ToString
                Me.lblDesignation.Visible = True
                Me.txtIOOfficerName.Text = .Cells(1).Value.ToString
                Me.txtIOPENNo.Text = .Cells(2).Value.ToString
                Me.txtIOBAsicPay.Text = .Cells(3).Value.ToString
                Me.txtIOScaleOfPay.Text = .Cells(4).Value.ToString
                Me.txtIODARate.Text = .Cells(5).Value.ToString
                Me.cmbSalutation.Text = .Cells(6).Value.ToString
            End With
        End If

        If CurrentTab = "SS" Then
            If Me.SSDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.SSDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            SSEditMode = True
            Me.btnSaveSS.Text = "Update"
            Me.txtSSName.Text = Me.SSDatagrid.SelectedRows(0).Cells(0).Value.ToString
            Me.txtSSDesignation.Text = Me.SSDatagrid.SelectedRows(0).Cells(1).Value.ToString
            Me.txtSSPen.Text = Me.SSDatagrid.SelectedRows(0).Cells(2).Value.ToString
            Me.txtSSName.Focus()
        End If

        If CurrentTab = "OS" Then
            OfficeSettingsEditMode(True)
            Me.txtFullOffice.Focus()
        End If


        If CurrentTab = "IDR" Then
            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            blIDRNewDataMode = False
            blIDREditMode = True

            FrmIdentificationRegisterDE.Show()
            FrmIdentificationRegisterDE.BringToFront()
            Me.Cursor = Cursors.Default
        End If


    End Sub

#End Region


#Region "OPEN  BUTTON ACTION"
    Private Sub OpenRecord() Handles btnOpen.Click, btnOpenContext.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        On Error Resume Next
        If CurrentTab = "SOC" Then

            SOCEditMode = False
            Me.btnSaveSOC.Text = "Save"

            If Me.SOCDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.SOCDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.PanelSOC.Visible = True
            ClearSOCFields()
            With Me.SOCDatagrid
                Me.txtSOCNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtSOCNumberOnly.Text = .SelectedCells(1).Value.ToString
                Me.dtSOCInspection.ValueObject = .SelectedCells(2).Value
                Me.dtSOCReport.ValueObject = .SelectedCells(3).Value
                Me.dtSOCOccurrence.Text = .SelectedCells(4).Value.ToString
                Me.cmbSOCPoliceStation.Text = .SelectedCells(5).Value.ToString
                Me.txtSOCCrimeNumber.Text = .SelectedCells(6).Value.ToString
                Me.txtSOCSection.Text = .SelectedCells(7).Value.ToString
                Me.txtSOCPlace.Text = .SelectedCells(8).Value.ToString
                Me.txtSOCOfficer.Text = .SelectedCells(9).Value.ToString.Replace(vbNewLine, "; ")
                Me.txtSOCCPsDeveloped.Text = .SelectedCells(10).Value.ToString
                Me.txtSOCCPsUnfit.Text = .SelectedCells(11).Value.ToString
                Me.txtSOCCPsEliminated.Text = .SelectedCells(12).Value.ToString

                CPIdentified = Val(.SelectedCells(13).Value)

                Me.txtSOCCPDetails.Text = .SelectedCells(14).Value.ToString
                Me.txtSOCComplainant.Text = .SelectedCells(15).Value.ToString()
                Me.cmbSOCModus.Text = .SelectedCells(16).Value.ToString
                Me.txtSOCPropertyLost.Text = .SelectedCells(17).Value.ToString
                Me.txtSOCPhotographer.Text = .SelectedCells(18).Value.ToString
                Me.cmbSOCPhotoReceived.Text = .SelectedCells(19).Value.ToString
                Me.txtSOCDateOfPhotography.Text = .SelectedCells(20).Value.ToString
                Me.txtSOCGist.Text = .SelectedCells(21).Value.ToString
                Me.txtSOCComparisonDetails.Text = .SelectedCells(22).Value.ToString
                Me.chkGraveCrime.Checked = .SelectedCells(23).Value
                Me.cmbFileStatus.Text = .SelectedCells(24).Value.ToString
            End With
            OriginalSOCNumber = Me.txtSOCNumber.Text
            Me.txtSOCNumber.Focus()
            Me.txtSOCYear.Text = Year(Me.dtSOCInspection.Value)
            TickOfficerList(Me.txtSOCOfficer.Text)
        End If



        If CurrentTab = "RSOC" Then
            RSOCEditMode = False
            Me.btnSaveRSOC.Text = "Save"

            If Me.RSOCDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.RSOCDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.PanelRSOC.Visible = True


            With Me.RSOCDatagrid
                OriginalRSOCSerialNumber = .SelectedCells(0).Value.ToString
                Me.txtRSOCSerialNumber.Text = OriginalRSOCSerialNumber
                Me.txtRSOCNumber.Text = .SelectedCells(1).Value.ToString
                Me.txtRSOCNumberOnly.Text = .SelectedCells(2).Value.ToString
                Me.dtRSOCInspection.ValueObject = .SelectedCells(3).Value
                Me.cmbRSOCPoliceStation.Text = .SelectedCells(4).Value.ToString
                Me.txtRSOCCrimeNumber.Text = .SelectedCells(5).Value.ToString
                Me.cmbRSOCOfficer.Text = .SelectedCells(6).Value.ToString
                Me.txtRSOCReportSentTo.Text = .SelectedCells(7).Value.ToString
                Me.dtRSOCReportSentOn.ValueObject = .SelectedCells(8).Value.ToString
                Me.cmbRSOCNatureOfReport.Text = .SelectedCells(9).Value.ToString
                Me.txtRSOCDespatchNumber.Text = .SelectedCells(10).Value.ToString
                Me.txtRSOCRemarks.Text = .SelectedCells(11).Value.ToString
            End With

            Me.txtRSOCSerialNumber.Focus()

        End If




        If CurrentTab = "FPA" Then
            DevComponents.DotNetBar.MessageBoxEx.Show("This option is not available for FPA Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If



        If CurrentTab = "DA" Then

            DAEditMode = False
            ClearDAImage()
            Me.btnSaveDA.Text = "Save"

            If Me.DADatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DADatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.PanelDA.Visible = True
            With Me.DADatagrid
                Me.txtDANumber.Text = .SelectedCells(0).Value.ToString
                Me.txtDANumberOnly.Text = .SelectedCells(1).Value.ToString
                Me.dtDAEntry.ValueObject = .SelectedCells(2).Value
                Me.cmbDAPoliceStation.Text = .SelectedCells(3).Value.ToString
                Me.txtDACrimeNumber.Text = .SelectedCells(4).Value.ToString
                Me.txtDASection.Text = .SelectedCells(5).Value.ToString
                Me.txtDAName.Text = .SelectedCells(6).Value.ToString
                Me.txtDAAliasName.Text = .SelectedCells(7).Value.ToString()
                Me.txtDAFathersName.Text = .SelectedCells(8).Value.ToString
                Me.cmbDASex.Text = .SelectedCells(9).Value.ToString
                Me.txtDAAddress.Text = .SelectedCells(10).Value.ToString
                Me.txtDAHenryNumerator.Text = .SelectedCells(11).Value.ToString
                Me.txtDAHenryDenominator.Text = .SelectedCells(12).Value.ToString
                Me.txtDAModusOperandi.Text = .SelectedCells(13).Value.ToString
                Me.txtDARemarks.Text = .SelectedCells(14).Value.ToString
                DASlipImageFile = .SelectedCells(15).Value.ToString
            End With
            If FileIO.FileSystem.FileExists(DASlipImageFile) = True Then
                Me.picDASlip.Image = New Bitmap(DASlipImageFile)
            Else
                Me.picDASlip.Image = My.Resources.NoDAImage
            End If

            OriginalDANumber = Me.txtDANumber.Text
            Me.txtDANumber.Focus()
            Me.txtDAYear.Text = Year(Me.dtDAEntry.Value)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End If


        If CurrentTab = "ID" Then

            IDEditMode = False
            ClearIDImage()
            Me.btnSaveID.Text = "Save"

            If Me.IDDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.IDDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.PanelID.Visible = True
            With Me.IDDatagrid
                Me.txtIDNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtIDDANumber.Text = .SelectedCells(1).Value.ToString
                Me.cmbIDPoliceStation.Text = .SelectedCells(2).Value.ToString
                Me.txtIDCrimeNumber.Text = .SelectedCells(3).Value.ToString
                Me.txtIDSection.Text = .SelectedCells(4).Value.ToString
                Me.txtIDName.Text = .SelectedCells(5).Value.ToString
                Me.txtIDAliasName.Text = .SelectedCells(6).Value.ToString()
                Me.txtIDFathersName.Text = .SelectedCells(7).Value.ToString
                Me.cmbIDSex.Text = .SelectedCells(8).Value.ToString
                Me.txtIDAddress.Text = .SelectedCells(9).Value.ToString
                Me.txtIDHenryNumerator.Text = .SelectedCells(10).Value.ToString
                Me.txtIDHenryDenominator.Text = .SelectedCells(11).Value.ToString
                Me.txtIDModusOperandi.Text = .SelectedCells(12).Value.ToString
                Me.txtIDDetails.Text = .SelectedCells(13).Value.ToString
                Me.txtIDRemarks.Text = .SelectedCells(14).Value.ToString
                IDSlipImageFile = .SelectedCells(15).Value.ToString
            End With
            If FileIO.FileSystem.FileExists(IDSlipImageFile) = True Then
                Me.picIDSlip.Image = System.Drawing.Image.FromFile(IDSlipImageFile)
            Else
                Me.picIDSlip.Image = My.Resources.NoDAImage
            End If
            OriginalIDNumber = Me.txtIDNumber.Text
            Me.txtIDNumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End If


        If CurrentTab = "AC" Then

            ACEditMode = False
            ClearACImage()
            Me.btnSaveAC.Text = "Save"

            If Me.ACDatagrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.ACDatagrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.PanelAC.Visible = True
            With Me.ACDatagrid
                Me.txtACNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtACDANumber.Text = .SelectedCells(1).Value.ToString
                Me.cmbACPoliceStation.Text = .SelectedCells(2).Value.ToString
                Me.txtACCrimeNumber.Text = .SelectedCells(3).Value.ToString
                Me.txtACSection.Text = .SelectedCells(4).Value.ToString
                Me.txtACName.Text = .SelectedCells(5).Value.ToString
                Me.txtACAliasName.Text = .SelectedCells(6).Value.ToString()
                Me.txtACFathersName.Text = .SelectedCells(7).Value.ToString
                Me.cmbACSex.Text = .SelectedCells(8).Value.ToString
                Me.txtACAddress.Text = .SelectedCells(9).Value.ToString
                Me.txtACHenryNumerator.Text = .SelectedCells(10).Value.ToString
                Me.txtACHenryDenominator.Text = .SelectedCells(11).Value.ToString
                Me.txtACModusOperandi.Text = .SelectedCells(12).Value.ToString
                Me.txtACRemarks.Text = .SelectedCells(13).Value.ToString
                ACSlipImageFile = .SelectedCells(14).Value.ToString
            End With
            If FileIO.FileSystem.FileExists(ACSlipImageFile) = True Then
                Me.picACSlip.Image = New Bitmap(ACSlipImageFile)
            Else
                Me.picACSlip.Image = My.Resources.NoDAImage
            End If
            OriginalACNumber = Me.txtACNumber.Text
            Me.txtACNumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End If


        If CurrentTab = "CD" Then

            CDEditMode = False
            Me.btnSaveCD.Text = "Save"

            If Me.CDDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.CDDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.PanelCD.Visible = True
            With Me.CDDataGrid
                Me.txtCDNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtCDNumberOnly.Text = .SelectedCells(1).Value.ToString
                Me.dtCDExamination.ValueObject = .SelectedCells(2).Value
                Me.cmbCDOfficer.Text = .SelectedCells(3).Value.ToString()
                Me.txtCourt.Text = .SelectedCells(4).Value.ToString
                Me.txtCCNumber.Text = .SelectedCells(5).Value.ToString
                Me.cmbCDPoliceStation.Text = .SelectedCells(6).Value.ToString
                Me.txtCDCrNo.Text = .SelectedCells(7).Value.ToString
                Me.txtCDDetails.Text = .SelectedCells(8).Value.ToString()
                Me.txtCDRemarks.Text = .SelectedCells(9).Value.ToString
            End With
            OriginalCDNumber = Me.txtCDNumber.Text
            Me.txtCDNumber.Focus()
            Me.txtCDYear.Text = Year(Me.dtCDExamination.Value)
        End If


        If CurrentTab = "PS" Then

            PSEditMode = False
            Me.btnSavePS.Text = "Save"

            If Me.PSDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.PSDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            With Me.PSDataGrid
                Me.txtPSName.Text = .SelectedCells(0).Value.ToString
                Me.cmbSHO.Text = .SelectedCells(1).Value.ToString
                Me.txtPhoneNumber1.Text = .SelectedCells(2).Value.ToString
                Me.txtPhoneNumber2.Text = .SelectedCells(3).Value.ToString
                Dim d As String = .SelectedCells(4).Value.ToString
                Me.txtDistance.Text = vbNullString
                Me.txtDistance.Text = Strings.Left(d, Len(d) - 3)
            End With
            OriginalPSName = Trim(Me.txtPSName.Text)
            Me.txtPSName.Focus()
            Me.txtPSName.SelectAll()
        End If

        If CurrentTab = "IO" Then

            With Me.IODatagrid.SelectedRows(0)
                IOSelectedRow = .Index
                Me.lblDesignation.Text = .Cells(0).Value.ToString
                Me.lblDesignation.Visible = True
                Me.txtIOOfficerName.Text = .Cells(1).Value.ToString
                Me.txtIOPENNo.Text = .Cells(2).Value.ToString
                Me.txtIOBAsicPay.Text = .Cells(3).Value.ToString
                Me.txtIOScaleOfPay.Text = .Cells(4).Value.ToString
                Me.txtIODARate.Text = .Cells(5).Value.ToString
                Me.cmbSalutation.Text = .Cells(6).Value.ToString
            End With
        End If

        If CurrentTab = "OS" Then
            OfficeSettingsEditMode(True)
            Me.txtFullOffice.Focus()
        End If

        If CurrentTab = "IDR" Or CurrentTab = "SS" Then
            DevComponents.DotNetBar.MessageBoxEx.Show("This option is not available.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If


    End Sub

#End Region


#Region "DELETE BUTTON ACTION"


    Private Sub DeleteSelectedItem() Handles btnDelete.Click, btnDeleteContext.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        Try


            If CurrentTab = "OS" Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Deletion is not available for Office Settings", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            If Me.chkPreventDeletion.Checked Then
                MessageBoxEx.Show("Please click the down arrow to the right of the delete button and uncheck the box 'Prevent Deletion' to allow deletion of data.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ModificationDetail As String = ""

            If CurrentTab = "IO" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the officer " & Me.IODatagrid.SelectedRows(0).Cells(0).Value.ToString().ToUpper & " : " & Me.IODatagrid.SelectedRows(0).Cells(1).Value.ToString().ToUpper & "?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then
                    ModificationDetail = "Deleted the officer " & Me.IODatagrid.SelectedRows(0).Cells(1).Value.ToString()
                    IOSelectedRow = Me.IODatagrid.SelectedRows(0).Index
                    ClearIOFields()
                    UpdateOfficerList()
                    ShowDesktopAlert("Selected officer deleted")
                End If
            End If

            '###################   SOC ##############
            If CurrentTab = "SOC" Then
                If Me.SOCDatagrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.SOCDatagrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete SoC No. " & Me.SOCDatagrid.SelectedCells(0).Value.ToString() & " from SOC Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If SOCEditMode Then
                        SOCEditMode = False
                        Me.btnSaveSOC.Text = "Save"
                    End If


                    OriginalSOCNumber = Me.SOCDatagrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.SOCRegisterRow 'find old row
                    oldRow = Me.FingerPrintDataSet.SOCRegister.FindBySOCNumber(OriginalSOCNumber)
                    oldRow.Delete()

                    Me.SOCRegisterTableAdapter.DeleteSelectedRecord(OriginalSOCNumber)
                    ModificationDetail = "Deleted SOC No. " & OriginalSOCNumber

                    ShowDesktopAlert("Selected SOC record deleted!")
                    If Me.SOCDatagrid.SelectedRows.Count = 0 And Me.SOCDatagrid.RowCount <> 0 Then
                        Me.SOCDatagrid.Rows(Me.SOCDatagrid.RowCount - 1).Selected = True
                    End If
                End If
            End If


            If CurrentTab = "RSOC" Then
                If Me.RSOCDatagrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.RSOCDatagrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected record No. " & Me.RSOCDatagrid.SelectedCells(0).Value.ToString() & " from SOC Reports Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If RSOCEditMode Then
                        RSOCEditMode = False
                        Me.btnSaveRSOC.Text = "Save"
                    End If


                    OriginalRSOCSerialNumber = Me.RSOCDatagrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.SOCReportRegisterRow
                    oldRow = Me.FingerPrintDataSet.SOCReportRegister.FindBySerialNo(OriginalRSOCSerialNumber)
                    oldRow.Delete()

                    Me.RSOCRegisterTableAdapter.DeleteSelectedRecord(OriginalRSOCSerialNumber)
                    ModificationDetail = "Deleted RSOC No. " & OriginalRSOCSerialNumber

                    ShowDesktopAlert("Selected SOC Report record deleted!")
                    If Me.RSOCDatagrid.SelectedRows.Count = 0 And Me.RSOCDatagrid.RowCount <> 0 Then
                        Me.RSOCDatagrid.Rows(Me.RSOCDatagrid.RowCount - 1).Selected = True
                    End If
                End If
            End If



            If CurrentTab = "DA" Then
                If Me.DADatagrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.DADatagrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete DA No." & Me.DADatagrid.SelectedCells(0).Value.ToString() & " from DA Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If DAEditMode Then
                        DAEditMode = False
                        Me.btnSaveDA.Text = "Save"
                    End If


                    OriginalDANumber = Me.DADatagrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.DARegisterRow
                    oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(OriginalDANumber)
                    oldRow.Delete()

                    Me.DARegisterTableAdapter.DeleteSelectedRecord(OriginalDANumber)
                    ModificationDetail = "Deleted DA No. " & OriginalDANumber

                    ShowDesktopAlert("Selected DA record deleted!")
                    If Me.DADatagrid.SelectedRows.Count = 0 And Me.DADatagrid.RowCount <> 0 Then
                        Me.DADatagrid.Rows(Me.DADatagrid.RowCount - 1).Selected = True
                    End If
                End If
            End If




            If CurrentTab = "ID" Then
                If Me.IDDatagrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.IDDatagrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected record No. " & Me.IDDatagrid.SelectedCells(0).Value.ToString() & " from Identified Slips Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If IDEditMode Then
                        IDEditMode = False
                        Me.btnSaveID.Text = "Save"
                    End If


                    OriginalIDNumber = Me.IDDatagrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.IdentifiedSlipsRegisterRow
                    oldRow = Me.FingerPrintDataSet.IdentifiedSlipsRegister.FindByIDNumber(OriginalIDNumber)
                    oldRow.Delete()

                    Me.IDRegisterTableAdapter.DeleteSelectedRecord(OriginalIDNumber)
                    ModificationDetail = "Deleted Identified Slip No. " & OriginalIDNumber

                    ShowDesktopAlert("Selected Identified Slip record deleted!")
                    If Me.IDDatagrid.SelectedRows.Count = 0 And Me.IDDatagrid.RowCount <> 0 Then
                        Me.IDDatagrid.Rows(Me.IDDatagrid.RowCount - 1).Selected = True
                    End If
                End If
            End If




            If CurrentTab = "AC" Then
                If Me.ACDatagrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.ACDatagrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected record No. " & Me.ACDatagrid.SelectedCells(0).Value.ToString() & " from Active Criminal Slips Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If ACEditMode Then
                        ACEditMode = False
                        Me.btnSaveAC.Text = "Save"
                    End If


                    OriginalACNumber = Me.ACDatagrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.ActiveCriminalsRegisterRow
                    oldRow = Me.FingerPrintDataSet.ActiveCriminalsRegister.FindByACNumber(OriginalACNumber)
                    oldRow.Delete()

                    Me.ACRegisterTableAdapter.DeleteSelectedRecord(OriginalACNumber)
                    ModificationDetail = "Deleted Active Criminal Slip No. " & OriginalACNumber

                    ShowDesktopAlert("Selected Active Criminal Slip record deleted!")
                    If Me.ACDatagrid.SelectedRows.Count = 0 And Me.ACDatagrid.RowCount <> 0 Then
                        Me.ACDatagrid.Rows(Me.ACDatagrid.RowCount - 1).Selected = True
                    End If
                End If
            End If



            If CurrentTab = "CD" Then
                If Me.CDDataGrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.CDDataGrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected record No. " & Me.CDDataGrid.SelectedCells(0).Value.ToString() & " from Court Duty Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If CDEditMode Then
                        CDEditMode = False
                        Me.btnSaveCD.Text = "Save"
                    End If


                    OriginalCDNumber = Me.CDDataGrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.CDRegisterRow
                    oldRow = Me.FingerPrintDataSet.CDRegister.FindByCDNumberWithYear(OriginalCDNumber)
                    oldRow.Delete()

                    Me.CDRegisterTableAdapter.DeleteSelectedRecord(OriginalCDNumber)
                    ModificationDetail = "Deleted Court Duty No. " & OriginalCDNumber

                    ShowDesktopAlert("Selected CD record deleted!")
                    If Me.CDDataGrid.SelectedRows.Count = 0 And Me.CDDataGrid.RowCount <> 0 Then
                        Me.CDDataGrid.Rows(Me.CDDataGrid.RowCount - 1).Selected = True
                    End If
                End If
            End If


            If CurrentTab = "FPA" Then
                If Me.FPADataGrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                If Me.FPADataGrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete FPA No. " & Me.FPADataGrid.SelectedCells(0).Value.ToString() & " from FP Attestation Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If blFPAEditMode Then
                        blFPAEditMode = False
                    End If


                    OriginalFPANumber = Me.FPADataGrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.FPAttestationRegisterRow
                    oldRow = Me.FingerPrintDataSet.FPAttestationRegister.FindByFPNumber(OriginalFPANumber)
                    oldRow.Delete()

                    Me.FPARegisterTableAdapter.DeleteSelectedRecord(OriginalFPANumber)
                    Me.ChalanTableTableAdapter1.DeleteQueryFPNumber(OriginalFPANumber)
                    ModificationDetail = "Deleted FP Attestation No. " & OriginalFPANumber

                    ShowDesktopAlert("Selected FPA record deleted!")
                    If Me.FPADataGrid.SelectedRows.Count = 0 And Me.FPADataGrid.RowCount <> 0 Then
                        Me.FPADataGrid.Rows(Me.FPADataGrid.RowCount - 1).Selected = True
                    End If
                End If
            End If

            '###################   PS ##############

            If CurrentTab = "PS" Then
                If Me.PSDataGrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Me.PSDataGrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected Police Station " & Me.PSDataGrid.SelectedCells(0).Value.ToString() & "?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If PSEditMode Then
                        PSEditMode = False
                        Me.btnSavePS.Text = "Save"
                    End If


                    OriginalPSName = Me.PSDataGrid.SelectedCells(0).Value.ToString()
                    Dim oldRow As FingerPrintDataSet.PoliceStationListRow
                    oldRow = Me.FingerPrintDataSet.PoliceStationList.FindByPoliceStation(OriginalPSName)
                    oldRow.Delete()

                    Me.PSRegisterTableAdapter.DeleteSelectedRecord(OriginalPSName)
                    ModificationDetail = "Deleted Police Station " & OriginalPSName

                    ShowDesktopAlert("Selected Police Station deleted!")
                    If Me.PSDataGrid.SelectedRows.Count = 0 And Me.PSDataGrid.RowCount <> 0 Then
                        Me.PSDataGrid.Rows(Me.PSDataGrid.RowCount - 1).Selected = True
                    End If
                    PSListChanged = True
                End If
            End If

            If CurrentTab = "IDR" Then
                If Me.JoinedIDRDataGrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If FrmIdentificationRegisterDE.Visible Then
                    MessageBoxEx.Show("Identification Register Data Entry Form is open. Please close it first.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim reply As DialogResult = MessageBoxEx.Show("Do you really want to delete Identification No. " & Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString() & " from Identification Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    Dim IdentificationNumber = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString()
                    Dim SOCNo = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString()
                    Dim selectedcpsidentified As Integer = Val(Me.JoinedIDRDataGrid.SelectedCells(10).Value)
                    Dim oldcpsidentified As Integer = Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentifiedBySOCNumber(SOCNo)

                    Dim oldRow As FingerPrintDataSet.JoinedIDRRow
                    oldRow = Me.FingerPrintDataSet.JoinedIDR.FindBySlNumber(Me.JoinedIDRDataGrid.SelectedCells(21).Value.ToString())
                    oldRow.Delete()

                    Me.IdentificationRegisterTableAdapter1.DeleteSelectedRecord(IdentificationNumber)
                    Me.CulpritsRegisterTableAdapter1.DeleteQuery(IdentificationNumber)

                    Dim count = Me.IdentificationRegisterTableAdapter1.CheckSOCNumberExists(SOCNo)

                    Dim index = Me.SOCRegisterBindingSource.Find("SOCNumber", SOCNo)


                    If count = 0 Then
                        Me.SOCRegisterTableAdapter.UpdateQuerySetIdentificationDetails("", "", "0", SOCNo)
                        If index > -1 Then
                            Me.SOCRegisterBindingSource.Position = index
                            Me.SOCDatagrid.SelectedRows(0).Cells(22).Value = ""
                            Me.SOCDatagrid.SelectedRows(0).Cells(24).Value = ""
                            Me.SOCDatagrid.SelectedRows(0).Cells(13).Value = "0"
                        End If
                    End If

                    If count > 0 Then
                        Dim cpremaining = oldcpsidentified - selectedcpsidentified
                        Me.SOCRegisterTableAdapter.UpdateQuerySetIdentificationDetails("Identified", "", cpremaining.ToString, SOCNo)
                        If index > -1 Then
                            Me.SOCRegisterBindingSource.Position = index
                            Me.SOCDatagrid.SelectedRows(0).Cells(22).Value = ""
                            Me.SOCDatagrid.SelectedRows(0).Cells(24).Value = "Identified"
                            Me.SOCDatagrid.SelectedRows(0).Cells(13).Value = cpremaining.ToString
                        End If

                    End If

                    ModificationDetail = "Deleted Identification No. " & IdentificationNumber

                    ShowDesktopAlert("Selected Identification record deleted!")
                    If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 And Me.JoinedIDRDataGrid.RowCount <> 0 Then
                        Me.JoinedIDRDataGrid.Rows(Me.JoinedIDRDataGrid.RowCount - 1).Selected = True
                    End If
                End If
            End If

            If CurrentTab = "SS" Then
                If Me.SSDatagrid.RowCount = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data to remove!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Me.SSDatagrid.SelectedRows.Count = 0 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to delete the selected Staff  '" & Me.SSDatagrid.SelectedCells(0).Value.ToString() & "' ?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then
                    ModificationDetail = "Deleted the staff " & Me.SSDatagrid.SelectedCells(0).Value.ToString()
                    SSEditMode = False
                    Me.btnSavePS.Text = "Save"

                    Dim oldRow As FingerPrintDataSet.SupportingStaffRow
                    oldRow = Me.FingerPrintDataSet.SupportingStaff.Rows(Me.SSDatagrid.SelectedRows(0).Index)
                    oldRow.Delete()
                    Me.SSTableAdapter1.Update(Me.FingerPrintDataSet.SupportingStaff)
                    ShowDesktopAlert("Selected Staff deleted!")
                    If Me.SSDatagrid.SelectedRows.Count = 0 And Me.SSDatagrid.RowCount <> 0 Then
                        Me.SSDatagrid.Rows(Me.SSDatagrid.RowCount - 1).Selected = True
                    End If
                End If
            End If

            InsertOrUpdateLastModificationDate(Now, ModificationDetail)
            DisplayDatabaseInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles SOCDatagrid.UserDeletingRow, RSOCDatagrid.UserDeletingRow, PSDataGrid.UserDeletingRow, DADatagrid.UserDeletingRow, IDDatagrid.UserDeletingRow, ACDatagrid.UserDeletingRow, CDDataGrid.UserDeletingRow, FPADataGrid.UserDeletingRow, JoinedIDRDataGrid.UserDeletingRow, SSDatagrid.UserDeletingRow
        On Error Resume Next
        e.Cancel = True
        Call DeleteSelectedItem()
    End Sub

    Private Sub DeleteAllRecords() Handles btnDeleteAll.Click
        Try

            If Me.chkPreventDeletion.Checked Then
                MessageBoxEx.Show("Please uncheck the box 'Prevent Deletion' next to the Delete button to allow deletion of data.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            frmInputBox.SetTitleandMessage("Confirm Delete All Records", "Please enter the words 'I want to delete all records' to confirm deletion. This is a security measure.", False)
            frmInputBox.AcceptButton = frmInputBox.btnCancel
            frmInputBox.ShowDialog()
            If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
            If frmInputBox.txtInputBox.Text <> "I want to delete all records" Then
                MessageBoxEx.Show("The words you entered do not match the test words!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub

            End If

            Dim ModificationDetail As String = ""

            '###################   SOC  ##############
            If CurrentTab = "SOC" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from SOC Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If SOCEditMode Then
                        SOCEditMode = False
                        Me.btnSaveSOC.Text = "Save"
                    End If

                    Me.SOCRegisterTableAdapter.DeleteAllRecords()
                    Me.SOCRegisterTableAdapter.Fill(Me.FingerPrintDataSet.SOCRegister)
                    ModificationDetail = "Deleted all records from SOC Register"
                    ShowDesktopAlert("All records deleted from SOC Register!")
                End If
            End If

            If CurrentTab = "RSOC" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from SOC Reports Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If RSOCEditMode Then
                        RSOCEditMode = False
                        Me.btnSaveRSOC.Text = "Save"
                    End If

                    Me.RSOCRegisterTableAdapter.DeleteAllRecords()
                    Me.RSOCRegisterTableAdapter.Fill(Me.FingerPrintDataSet.SOCReportRegister)
                    GenerateNewRSOCSerialNumber()
                    ModificationDetail = "Deleted all records from SOC Reports Register"
                    ShowDesktopAlert("All records deleted from SOC Reports Register!")
                End If
            End If


            If CurrentTab = "FPA" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from FP Attestation Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If blFPAEditMode Then
                        blFPAEditMode = False
                    End If

                    Me.FPARegisterTableAdapter.DeleteAllRecords()
                    Me.FPARegisterTableAdapter.Fill(Me.FingerPrintDataSet.FPAttestationRegister)
                    Me.ChalanTableTableAdapter1.DeleteAllRecords()

                    ModificationDetail = "Deleted all records from FP Attestation Register"
                    ShowDesktopAlert("All records deleted from FP Attestation Register!")
                End If
            End If

            If CurrentTab = "DA" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from DA Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If DAEditMode Then
                        DAEditMode = False
                        Me.btnSaveDA.Text = "Save"
                    End If

                    Me.DARegisterTableAdapter.DeleteAllRecords()
                    Me.DARegisterTableAdapter.Fill(Me.FingerPrintDataSet.DARegister)
                    ModificationDetail = "Deleted all records from DA Register"
                    ShowDesktopAlert("All records deleted from DA Register!")
                End If
            End If


            If CurrentTab = "ID" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from Identified Slips Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then

                    If IDEditMode Then
                        IDEditMode = False
                        Me.btnSaveID.Text = "Save"
                    End If

                    Me.IDRegisterTableAdapter.DeleteAllRecords()
                    Me.IDRegisterTableAdapter.Fill(Me.FingerPrintDataSet.IdentifiedSlipsRegister)
                    ModificationDetail = "Deleted all records from Identified Slips Register"
                    ShowDesktopAlert("All records deleted from Identified Slips Register!")
                End If
            End If

            If CurrentTab = "AC" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from Active Criminal Slips Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then


                    If ACEditMode Then
                        ACEditMode = False
                        Me.btnSaveAC.Text = "Save"
                    End If

                    Me.ACRegisterTableAdapter.DeleteAllRecords()
                    Me.ACRegisterTableAdapter.Fill(Me.FingerPrintDataSet.ActiveCriminalsRegister)
                    ModificationDetail = "Deleted all records from Active Criminal Slips Register"
                    ShowDesktopAlert("All records deleted from Active Criminal Slips Register!")
                End If
            End If



            If CurrentTab = "CD" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from Court Duty Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then


                    If CDEditMode Then
                        CDEditMode = False
                        Me.btnSaveCD.Text = "Save"
                    End If

                    Me.CDRegisterTableAdapter.DeleteAllRecords()
                    Me.CDRegisterTableAdapter.Fill(Me.FingerPrintDataSet.CDRegister)
                    ModificationDetail = "Deleted all records from Court Duty Slips Register"
                    ShowDesktopAlert("All records deleted from Court Duty Register!")
                End If
            End If

            '###################   PS ##############

            If CurrentTab = "PS" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from Police Station List?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then


                    If PSEditMode Then
                        PSEditMode = False
                        Me.btnSavePS.Text = "Save"
                    End If

                    Me.PSRegisterTableAdapter.DeleteAllRecords()
                    Me.PSRegisterTableAdapter.Fill(Me.FingerPrintDataSet.PoliceStationList)
                    ModificationDetail = "Deleted all Police Stations"
                    ShowDesktopAlert("All records deleted from Police Station List!")
                    Me.cmbSOCPoliceStation.Items.Clear()
                End If
            End If

            '###################   IDR   ##############

            If CurrentTab = "IDR" Then

                If FrmIdentificationRegisterDE.Visible Then
                    MessageBoxEx.Show("Identification Register Data Entry Form is open. Please close it first.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from Identification Register?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then
                    Me.IdentificationRegisterTableAdapter1.DeleteAllRecords()
                    Me.CulpritsRegisterTableAdapter1.DeleteAllRecords()
                    Me.IdentificationRegisterTableAdapter1.Fill(Me.FingerPrintDataSet.IdentificationRegister)
                    Me.JoinedIDRTableAdapter.Fill(Me.FingerPrintDataSet.JoinedIDR)
                    ModificationDetail = "Deleted all records from Identification Register"
                    ShowDesktopAlert("All records deleted from Identification Register!")
                End If
            End If

            If CurrentTab = "SS" Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("WARNING:Delete all records. Do you really want to delete all the records from Supporting Staff List?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.Yes Then
                    PSEditMode = False
                    Me.btnSavePS.Text = "Save"
                    Me.SSTableAdapter1.DeleteAllQuery()
                    Me.SSTableAdapter1.Fill(Me.FingerPrintDataSet.SupportingStaff)
                    ModificationDetail = "Deleted all records from Supporting Staff List"
                    ShowDesktopAlert("All records deleted from Supporting Staff List!")
                End If
            End If

            InsertOrUpdateLastModificationDate(Now, ModificationDetail)
            DisplayDatabaseInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region


#Region "SEARCH BUTTON ACTION"

    Private Sub ShowAdvancedSearch(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSOCAdvancedSearch.Click, btnRSOCAdvancedSearch.Click, btnDAAdvancedSearch.Click, btnIDAdvancedSearch.Click, btnACAdvancedSearch.Click, btnCDAdvancedSearch.Click, btnSearchMain.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub


        On Error Resume Next
        If CurrentTab = "IO" Then
            MessageBoxEx.Show("Search is not available for 'Officer List'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentTab = "PS" Then
            MessageBoxEx.Show("Search is not available for 'Police Station List'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CurrentTab = "OS" Then
            MessageBoxEx.Show("Search is not available for 'Office Settings'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        FrmAdvancedSearch.Close()
        FrmAdvancedSearch.WindowState = FormWindowState.Normal
        FrmAdvancedSearch.StartPosition = FormStartPosition.CenterScreen
        FrmAdvancedSearch.Text = "Advanced Search - " & Me.pnlRegisterName.Text
        FrmAdvancedSearch.TitleText = "<b>Advanced Search - " & Me.pnlRegisterName.Text & "</b> "
        FrmAdvancedSearch.Show()
        FrmAdvancedSearch.BringToFront()


        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub
#End Region


#Region "SHOW/HIDE BUTTON ACTION"

    Private Sub HideDataEntryFields() Handles btnShowHideFields.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        On Error Resume Next

        Select Case CurrentTab
            Case "SOC"
                Me.PanelSOC.Visible = Not Me.PanelSOC.Visible
                If Me.PanelSOC.Visible = False Then
                    Me.SOCDatagrid.Focus()
                Else
                    Me.txtSOCNumber.Focus()
                End If

            Case "RSOC"
                Me.PanelRSOC.Visible = Not Me.PanelRSOC.Visible
                If Me.PanelRSOC.Visible = False Then
                    Me.RSOCDatagrid.Focus()
                Else
                    Me.txtRSOCSerialNumber.Focus()
                End If

            Case "DA"
                Me.PanelDA.Visible = Not Me.PanelDA.Visible
                If Me.PanelDA.Visible = False Then
                    Me.DADatagrid.Focus()
                Else
                    Me.txtDANumber.Focus()
                End If

            Case "ID"
                Me.PanelID.Visible = Not Me.PanelID.Visible
                If Me.PanelID.Visible = False Then
                    Me.IDDatagrid.Focus()
                Else
                    Me.txtIDNumber.Focus()
                End If

            Case "AC"
                Me.PanelAC.Visible = Not Me.PanelAC.Visible
                If Me.PanelAC.Visible = False Then
                    Me.ACDatagrid.Focus()
                Else
                    Me.txtACNumber.Focus()
                End If

            Case "CD"
                Me.PanelCD.Visible = Not Me.PanelCD.Visible
                If Me.PanelCD.Visible = False Then
                    Me.CDDataGrid.Focus()
                Else
                    Me.txtCDNumber.Focus()
                End If
            Case Else
                MessageBoxEx.Show("This option is not available.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

    End Sub


    Private Sub ShowOrHideAllFieldsOnButtonClick() Handles btnHideAllDataEntryFields.Click
        On Error Resume Next

        ShowAllDataEntryFields(ShowAllFields)
        Select Case CurrentTab
            Case "SOC"
                Me.SOCDatagrid.Focus()
            Case "RSOC"
                Me.RSOCDatagrid.Focus()
            Case "DA"
                Me.DADatagrid.Focus()
            Case "ID"
                Me.IDDatagrid.Focus()
            Case "AC"
                Me.ACDatagrid.Focus()
            Case "FPA"
                Me.FPADataGrid.Focus()
            Case "CD"
                Me.CDDataGrid.Focus()
            Case "PS"
                Me.PSDataGrid.Focus()

        End Select
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

    End Sub
#End Region

    '-------------------------------------------LOAD LAST SERIAL NUMBERS-----------------------------------------

#Region "LOAD LAST SERIAL NUMBERS"

    Private Sub IncrementSOCNumber(ByVal LastSOCNumber As String)
        On Error Resume Next
        Dim s = Strings.Split(LastSOCNumber, "/")
        Dim n As String = Val(s(0) + 1)
        Dim y As String = s(1)
        If y Is Nothing Then y = Me.txtSOCYear.Text
        Me.txtSOCNumber.Text = n.ToString & "/" & y
        Me.txtSOCNumberOnly.Text = n.ToString
    End Sub

    Private Sub GenerateNewSOCNumber()
        On Error Resume Next
        Dim y As String = Year(Today)
        Dim n As String = Val(Me.SOCRegisterTableAdapter.ScalarQueryMaxNumber(New Date(y, 1, 1), New Date(y, 12, 31))) + 1
        If Me.chkSOCTwodigits.Checked Then y = Strings.Right(y, 2)
        Me.txtSOCNumber.Text = n.ToString & "/" & y
        Me.txtSOCNumberOnly.Text = n.ToString
        Me.txtSOCNumber.Select(Me.txtSOCNumber.Text.Length, 0)
    End Sub


    Private Sub IncrementRSOCNumber(ByVal LastRSOCNumber As Long)
        On Error Resume Next
        Me.txtRSOCSerialNumber.Text = LastRSOCNumber + 1
    End Sub

    Private Sub GenerateNewRSOCSerialNumber()
        On Error Resume Next
        Me.txtRSOCSerialNumber.Text = Val(Me.RSOCRegisterTableAdapter.ScalarQueryMaxNumber()) + 1

    End Sub

    Private Sub IncrementDANumber(ByVal LastDANumber As String)
        On Error Resume Next
        Dim s = Strings.Split(LastDANumber, "/")
        Dim n As Integer = Val(s(0) + 1)
        Dim y As String = s(1)
        If y Is Nothing Then y = Me.txtDAYear.Text
        Me.txtDANumber.Text = n.ToString & "/" & y
        Me.txtDANumberOnly.Text = n.ToString
    End Sub

    Private Sub GenerateNewDANumber()
        On Error Resume Next
        Dim y As String = Year(Today)
        Dim n As Integer = Val(Me.DARegisterTableAdapter.ScalarQueryMaxNumber(New Date(y, 1, 1), New Date(y, 12, 31))) + 1
        If Me.chkDATwodigits.Checked Then y = Strings.Right(y, 2)
        Me.txtDANumber.Text = n.ToString & "/" & y
        Me.txtDANumberOnly.Text = n.ToString

    End Sub


    Private Sub IncrementIDNumber(ByVal LastIDNumber As Long)
        On Error Resume Next
        Me.txtIDNumber.Text = LastIDNumber + 1
    End Sub

    Private Sub GenerateNewIDNumber()
        On Error Resume Next
        Me.txtIDNumber.Text = Val(Me.IDRegisterTableAdapter.ScalarQueryMaxNumber()) + 1

    End Sub


    Private Sub IncrementACNumber(ByVal LastACNumber As Long)
        On Error Resume Next
        Me.txtACNumber.Text = LastACNumber + 1
    End Sub

    Private Sub GenerateNewACNumber()
        On Error Resume Next
        Me.txtACNumber.Text = Val(Me.ACRegisterTableAdapter.ScalarQueryMaxNumber()) + 1

    End Sub


    Private Sub IncrementCDNumber(ByVal LastCDNumber As String)
        On Error Resume Next
        Dim s = Strings.Split(LastCDNumber, "/")
        Dim n As Integer = Val(s(0) + 1)
        Dim y As String = s(1)
        If y Is Nothing Then y = Me.txtCDYear.Text
        Me.txtCDNumber.Text = n.ToString & "/" & y
        Me.txtCDNumberOnly.Text = n.ToString
    End Sub

    Private Sub GenerateNewCDNumber()
        On Error Resume Next
        Dim y As String = Year(Today)
        Dim n As Integer = Val(Me.CDRegisterTableAdapter.ScalarQueryMaxNumber(New Date(y, 1, 1), New Date(y, 12, 31))) + 1
        If Me.chkCDTwodigits.Checked Then y = Strings.Right(y, 2)
        Me.txtCDNumber.Text = n.ToString & "/" & y
        Me.txtCDNumberOnly.Text = n.ToString

    End Sub






#End Region


    '-------------------------------------------OFFICER SETTINGS-----------------------------------------

#Region "OFFICER SETTINGS"

    Private Sub InitializeOfficerTable()
        Dim rowcount As Integer = Me.IODatagrid.Rows.Count

        If rowcount < 6 Then
            Dim rowneeded As Integer = 6 - rowcount
            Me.IODatagrid.Rows.Add(rowneeded)
            Me.IODatagrid.Rows(0).Cells(0).Value = "Tester Inspector"
            Me.IODatagrid.Rows(1).Cells(0).Value = "FP Expert 1"
            Me.IODatagrid.Rows(2).Cells(0).Value = "FP Expert 2"
            Me.IODatagrid.Rows(3).Cells(0).Value = "FP Expert 3"
            Me.IODatagrid.Rows(4).Cells(0).Value = "FP Searcher"
            Me.IODatagrid.Rows(5).Cells(0).Value = "Photographer"
        End If

        For iocnt = 0 To 5
            For i = 1 To 6
                Me.IODatagrid.Rows(iocnt).Cells(i).Value = ""
            Next
        Next
    End Sub
    Public Sub LoadOfficerToMemory()
        On Error Resume Next
        Me.OfficerTableAdapter.Fill(Me.FingerPrintDataSet.OfficerTable)
        Dim cnt = Me.FingerPrintDataSet.OfficerTable.Count
        If cnt >= 1 Then
            TI = Me.FingerPrintDataSet.OfficerTable(0).TI & ", TI"
            FPE1 = Me.FingerPrintDataSet.OfficerTable(0).FPE1 & ", FPE"
            FPE2 = Me.FingerPrintDataSet.OfficerTable(0).FPE2 & ", FPE"
            FPE3 = Me.FingerPrintDataSet.OfficerTable(0).FPE3 & ", FPE"
            FPS = Me.FingerPrintDataSet.OfficerTable(0).FPS & ", FPS"
            strPhotographer = Me.FingerPrintDataSet.OfficerTable(0).Photographer
            Me.txtSOCPhotographer.AutoCompleteCustomSource.Add(strPhotographer)
            Me.txtSOCPhotographer.AutoCompleteCustomSource.Add("No Photographer")
            LoadOfficerListToDropDownMenu()
        End If
        If cnt >= 2 Then
            TIPen = Me.FingerPrintDataSet.OfficerTable(1).TI
        End If
    End Sub

    Public Sub LoadOfficerListToTable()

        Me.OfficerTableAdapter.Fill(Me.FingerPrintDataSet.OfficerTable)
        Dim cnt = Me.FingerPrintDataSet.OfficerTable.Count

        If cnt > 0 Then

            Me.IODatagrid.Rows(0).Cells(1).Value = Me.FingerPrintDataSet.OfficerTable(0).TI
            Me.IODatagrid.Rows(1).Cells(1).Value = Me.FingerPrintDataSet.OfficerTable(0).FPE1
            Me.IODatagrid.Rows(2).Cells(1).Value = Me.FingerPrintDataSet.OfficerTable(0).FPE2
            Me.IODatagrid.Rows(3).Cells(1).Value = Me.FingerPrintDataSet.OfficerTable(0).FPE3
            Me.IODatagrid.Rows(4).Cells(1).Value = Me.FingerPrintDataSet.OfficerTable(0).FPS
            Me.IODatagrid.Rows(5).Cells(1).Value = Me.FingerPrintDataSet.OfficerTable(0).Photographer.ToString
        End If

        If cnt > 1 Then
            Me.IODatagrid.Rows(0).Cells(2).Value = Me.FingerPrintDataSet.OfficerTable(1).TI
            Me.IODatagrid.Rows(1).Cells(2).Value = Me.FingerPrintDataSet.OfficerTable(1).FPE1
            Me.IODatagrid.Rows(2).Cells(2).Value = Me.FingerPrintDataSet.OfficerTable(1).FPE2
            Me.IODatagrid.Rows(3).Cells(2).Value = Me.FingerPrintDataSet.OfficerTable(1).FPE3
            Me.IODatagrid.Rows(4).Cells(2).Value = Me.FingerPrintDataSet.OfficerTable(1).FPS
            Me.IODatagrid.Rows(5).Cells(2).Value = Me.FingerPrintDataSet.OfficerTable(1).Photographer

        End If

        If cnt > 2 Then
            Me.IODatagrid.Rows(0).Cells(3).Value = Me.FingerPrintDataSet.OfficerTable(2).TI
            Me.IODatagrid.Rows(1).Cells(3).Value = Me.FingerPrintDataSet.OfficerTable(2).FPE1
            Me.IODatagrid.Rows(2).Cells(3).Value = Me.FingerPrintDataSet.OfficerTable(2).FPE2
            Me.IODatagrid.Rows(3).Cells(3).Value = Me.FingerPrintDataSet.OfficerTable(2).FPE3
            Me.IODatagrid.Rows(4).Cells(3).Value = Me.FingerPrintDataSet.OfficerTable(2).FPS
            Me.IODatagrid.Rows(5).Cells(3).Value = Me.FingerPrintDataSet.OfficerTable(2).Photographer

        End If

        If cnt > 3 Then
            Me.IODatagrid.Rows(0).Cells(4).Value = Me.FingerPrintDataSet.OfficerTable(3).TI
            Me.IODatagrid.Rows(1).Cells(4).Value = Me.FingerPrintDataSet.OfficerTable(3).FPE1
            Me.IODatagrid.Rows(2).Cells(4).Value = Me.FingerPrintDataSet.OfficerTable(3).FPE2
            Me.IODatagrid.Rows(3).Cells(4).Value = Me.FingerPrintDataSet.OfficerTable(3).FPE3
            Me.IODatagrid.Rows(4).Cells(4).Value = Me.FingerPrintDataSet.OfficerTable(3).FPS
            Me.IODatagrid.Rows(5).Cells(4).Value = Me.FingerPrintDataSet.OfficerTable(3).Photographer
        End If

        If cnt > 4 Then
            Me.IODatagrid.Rows(0).Cells(5).Value = Me.FingerPrintDataSet.OfficerTable(4).TI
            Me.IODatagrid.Rows(1).Cells(5).Value = Me.FingerPrintDataSet.OfficerTable(4).FPE1
            Me.IODatagrid.Rows(2).Cells(5).Value = Me.FingerPrintDataSet.OfficerTable(4).FPE2
            Me.IODatagrid.Rows(3).Cells(5).Value = Me.FingerPrintDataSet.OfficerTable(4).FPE3
            Me.IODatagrid.Rows(4).Cells(5).Value = Me.FingerPrintDataSet.OfficerTable(4).FPS
            Me.IODatagrid.Rows(5).Cells(5).Value = Me.FingerPrintDataSet.OfficerTable(4).Photographer
        End If

        If cnt > 5 Then
            Me.IODatagrid.Rows(0).Cells(6).Value = Me.FingerPrintDataSet.OfficerTable(5).TI
            Me.IODatagrid.Rows(1).Cells(6).Value = Me.FingerPrintDataSet.OfficerTable(5).FPE1
            Me.IODatagrid.Rows(2).Cells(6).Value = Me.FingerPrintDataSet.OfficerTable(5).FPE2
            Me.IODatagrid.Rows(3).Cells(6).Value = Me.FingerPrintDataSet.OfficerTable(5).FPE3
            Me.IODatagrid.Rows(4).Cells(6).Value = Me.FingerPrintDataSet.OfficerTable(5).FPS
            Me.IODatagrid.Rows(5).Cells(6).Value = Me.FingerPrintDataSet.OfficerTable(5).Photographer
        End If


    End Sub

    Public Sub LoadOfficerListToDropDownMenu()
        On Error Resume Next
        Dim x As System.Drawing.Point
        x.X = Me.txtSOCOfficer.Location.X
        x.Y = Me.txtSOCOfficer.Location.Y + Me.txtSOCOfficer.Height + 2
        Me.ItemPanel1.Location = x

        Me.cmbRSOCOfficer.Items.Clear()
        Me.cmbCDOfficer.Items.Clear()


        OfficerMenuVisibleItemCount = 0
        Dim ItemHeight As Integer = 23
        If TI <> ", TI" Then
            Me.chkTI.Visible = True
            Me.chkTI.Text = TI
            OfficerMenuVisibleItemCount = OfficerMenuVisibleItemCount + 1
            Me.cmbRSOCOfficer.Items.Add(TI)
            Me.cmbCDOfficer.Items.Add(TI)
            Me.txtSOCOfficer.AutoCompleteCustomSource.Add(TI)
            Me.cmbRSOCOfficer.AutoCompleteCustomSource.Add(TI)
            Me.cmbCDOfficer.AutoCompleteCustomSource.Add(TI)
        Else
            chkTI.Visible = False

        End If

        If FPE1 <> ", FPE" Then
            Me.chkFPE1.Visible = True
            Me.chkFPE1.Text = FPE1
            OfficerMenuVisibleItemCount = OfficerMenuVisibleItemCount + 1
            Me.cmbRSOCOfficer.Items.Add(FPE1)
            Me.cmbCDOfficer.Items.Add(FPE1)
            Me.txtSOCOfficer.AutoCompleteCustomSource.Add(FPE1)
            Me.cmbRSOCOfficer.AutoCompleteCustomSource.Add(FPE1)
            Me.cmbCDOfficer.AutoCompleteCustomSource.Add(FPE1)
        Else
            chkFPE1.Visible = False
        End If

        If FPE2 <> ", FPE" Then
            Me.chkFPE2.Visible = True
            Me.chkFPE2.Text = FPE2
            OfficerMenuVisibleItemCount = OfficerMenuVisibleItemCount + 1
            Me.cmbRSOCOfficer.Items.Add(FPE2)
            Me.cmbCDOfficer.Items.Add(FPE2)
            Me.txtSOCOfficer.AutoCompleteCustomSource.Add(FPE2)
            Me.cmbRSOCOfficer.AutoCompleteCustomSource.Add(FPE2)
            Me.cmbCDOfficer.AutoCompleteCustomSource.Add(FPE2)
        Else
            chkFPE2.Visible = False
        End If

        If FPE3 <> ", FPE" Then
            Me.chkFPE3.Visible = True
            Me.chkFPE3.Text = FPE3
            OfficerMenuVisibleItemCount = OfficerMenuVisibleItemCount + 1
            Me.cmbRSOCOfficer.Items.Add(FPE3)
            Me.cmbCDOfficer.Items.Add(FPE3)
            Me.txtSOCOfficer.AutoCompleteCustomSource.Add(FPE3)
            Me.cmbRSOCOfficer.AutoCompleteCustomSource.Add(FPE3)
            Me.cmbCDOfficer.AutoCompleteCustomSource.Add(FPE3)
        Else
            chkFPE3.Visible = False
        End If

        If FPS <> ", FPS" Then
            Me.chkFPS.Visible = True
            Me.chkFPS.Text = FPS
            OfficerMenuVisibleItemCount = OfficerMenuVisibleItemCount + 1
            Me.cmbRSOCOfficer.Items.Add(FPS)
            Me.cmbCDOfficer.Items.Add(FPS)
            Me.txtSOCOfficer.AutoCompleteCustomSource.Add(FPS)
            Me.cmbRSOCOfficer.AutoCompleteCustomSource.Add(FPS)
            Me.cmbCDOfficer.AutoCompleteCustomSource.Add(FPS)
        Else
            chkFPS.Visible = False
        End If

        Me.ItemPanel1.Height = ItemHeight * OfficerMenuVisibleItemCount + 5
        Me.ItemPanel1.Refresh()


    End Sub

    Private Sub ClearDropDownOfficerList()
        On Error Resume Next
        Me.chkTI.Checked = False
        Me.chkFPE1.Checked = False
        Me.chkFPE2.Checked = False
        Me.chkFPE3.Checked = False
        Me.chkFPS.Checked = False

    End Sub

    Private Sub TickOfficerList(ByVal OfficerList)
        On Error Resume Next
        Dim splitname() = Strings.Split(OfficerList, ";")
        Dim officername As String = "dummyname"
        Dim u = splitname.GetUpperBound(0)

        For j = 0 To u
            officername = splitname(j).ToLower.Trim
            If officername = Me.chkTI.Text.ToLower Then Me.chkTI.Checked = True
            If officername = Me.chkFPE1.Text.ToLower Then Me.chkFPE1.Checked = True
            If officername = Me.chkFPE2.Text.ToLower Then Me.chkFPE2.Checked = True
            If officername = Me.chkFPE3.Text.ToLower Then Me.chkFPE3.Checked = True
            If officername = Me.chkFPS.Text.ToLower Then Me.chkFPS.Checked = True
        Next

    End Sub



    Private Sub txtSOCOfficer_ButtonCustomClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSOCOfficer.ButtonCustomClick

        Me.ItemPanel1.Show()
        ' LoadOfficerList()
        Me.ItemPanel1.Cursor = Cursors.Default
        Me.ItemPanel1.Focus()
    End Sub

    Private Sub GenerateOfficerName() Handles chkTI.Click, chkFPE1.Click, chkFPE2.Click, chkFPE3.Click, chkFPS.Click
        'chkTI.CheckedChanged, chkFPE1.CheckedChanged, chkFPE2.CheckedChanged, chkFPE3.CheckedChanged, chkFPS.CheckedChanged
        On Error Resume Next
        Dim ti = IIf(Me.chkTI.Checked, Me.chkTI.Text + "; ", "")
        Dim fpe1 = IIf(Me.chkFPE1.Checked, Me.chkFPE1.Text + "; ", "")
        Dim fpe2 = IIf(Me.chkFPE2.Checked, Me.chkFPE2.Text + "; ", "")
        Dim fpe3 = IIf(Me.chkFPE3.Checked, Me.chkFPE3.Text + "; ", "")
        Dim fps = IIf(Me.chkFPS.Checked, Me.chkFPS.Text + "; ", "")

        Dim x = Trim(ti + fpe1 + fpe2 + fpe3 + fps)
        x = x.Trim(";")
        Me.txtSOCOfficer.Text = x

    End Sub

    Private Sub ClearIOFields()
        On Error Resume Next

        Me.txtIOOfficerName.Focus()
        Me.txtIOOfficerName.Clear()
        Me.cmbSalutation.Text = ""
        Me.txtIOPENNo.Clear()
        Me.txtIOBAsicPay.Clear()
        Me.txtIOScaleOfPay.Clear()
        Me.txtIODARate.Clear()
        Me.lblDesignation.Text = ""
        Me.lblDesignation.Visible = False
    End Sub


    Private Sub ClearSelectedIOFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIOOfficerName.ButtonCustomClick, txtIOPENNo.ButtonCustomClick, txtIOBAsicPay.ButtonCustomClick, txtIOScaleOfPay.ButtonCustomClick, txtIODARate.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString

    End Sub
    Private Sub SaveOfficerList() Handles btnSaveIO.Click
        If Me.lblDesignation.Visible = False Then
            MessageBoxEx.Show("Please press 'Edit' or 'Open' button to edit, then press 'Save'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        UpdateOfficerList()
        ShowDesktopAlert("Officer List Updated")

    End Sub

    Private Sub UpdateOfficerList()
        Try
            With Me.IODatagrid.Rows(IOSelectedRow)
                .Cells(1).Value = Me.txtIOOfficerName.Text.Trim
                .Cells(2).Value = Me.txtIOPENNo.Text.Trim
                .Cells(3).Value = Me.txtIOBAsicPay.Text.Trim
                .Cells(4).Value = Me.txtIOScaleOfPay.Text.Trim
                .Cells(5).Value = Me.txtIODARate.Text.Trim
                .Cells(6).Value = Me.cmbSalutation.Text.Trim
            End With

            ClearIOFields()
            Me.lblDesignation.Visible = False

            Me.OfficerTableAdapter.Fill(Me.FingerPrintDataSet.OfficerTable)
            Dim cnt = Me.FingerPrintDataSet.OfficerTable.Count

            If cnt = 0 Then

                Me.OfficerTableAdapter.Insert(cnt + 1, Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value)

                Me.OfficerTableAdapter.Insert(cnt + 2, Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value)

                Me.OfficerTableAdapter.Insert(cnt + 3, Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value)

                Me.OfficerTableAdapter.Insert(cnt + 4, Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value)

                Me.OfficerTableAdapter.Insert(cnt + 5, Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value)

            End If

            If cnt = 1 Then
                Dim oid = 1

                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value, oid)

                Me.OfficerTableAdapter.Insert(cnt + 1, Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value)

                Me.OfficerTableAdapter.Insert(cnt + 2, Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value)

                Me.OfficerTableAdapter.Insert(cnt + 3, Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value)

                Me.OfficerTableAdapter.Insert(cnt + 4, Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value)

            End If

            If cnt = 2 Then
                Dim oid = 1

                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value, oid)

                oid = 2
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value, oid)

                Me.OfficerTableAdapter.Insert(cnt + 1, Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value)

                Me.OfficerTableAdapter.Insert(cnt + 2, Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value)

                Me.OfficerTableAdapter.Insert(cnt + 3, Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value)

            End If

            If cnt = 3 Then
                Dim oid = 1

                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value, oid)

                oid = 2
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value, oid)

                oid = 3
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value, oid)

                Me.OfficerTableAdapter.Insert(cnt + 1, Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value)

                Me.OfficerTableAdapter.Insert(cnt + 2, Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value)
            End If

            If cnt = 4 Then
                Dim oid = 1

                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value, oid)

                oid = 2
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value, oid)

                oid = 3
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value, oid)

                oid = 4
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value, oid)

                Me.OfficerTableAdapter.Insert(cnt + 1, Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value)
            End If

            If cnt = 5 Then
                Dim oid = 1

                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value, oid)

                oid = 2
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value, oid)

                oid = 3
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value, oid)

                oid = 4
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value, oid)

                oid = 5
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value, oid)

                Me.OfficerTableAdapter.Insert(cnt + 1, Me.IODatagrid.Rows(0).Cells(6).Value, Me.IODatagrid.Rows(1).Cells(6).Value, Me.IODatagrid.Rows(2).Cells(6).Value, Me.IODatagrid.Rows(3).Cells(6).Value, Me.IODatagrid.Rows(4).Cells(6).Value, Me.IODatagrid.Rows(5).Cells(6).Value)
            End If

            If cnt = 6 Then
                Dim oid = 1

                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(1).Value, Me.IODatagrid.Rows(1).Cells(1).Value, Me.IODatagrid.Rows(2).Cells(1).Value, Me.IODatagrid.Rows(3).Cells(1).Value, Me.IODatagrid.Rows(4).Cells(1).Value, Me.IODatagrid.Rows(5).Cells(1).Value, oid)

                oid = 2
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(2).Value, Me.IODatagrid.Rows(1).Cells(2).Value, Me.IODatagrid.Rows(2).Cells(2).Value, Me.IODatagrid.Rows(3).Cells(2).Value, Me.IODatagrid.Rows(4).Cells(2).Value, Me.IODatagrid.Rows(5).Cells(2).Value, oid)

                oid = 3
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(3).Value, Me.IODatagrid.Rows(1).Cells(3).Value, Me.IODatagrid.Rows(2).Cells(3).Value, Me.IODatagrid.Rows(3).Cells(3).Value, Me.IODatagrid.Rows(4).Cells(3).Value, Me.IODatagrid.Rows(5).Cells(3).Value, oid)

                oid = 4
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(4).Value, Me.IODatagrid.Rows(1).Cells(4).Value, Me.IODatagrid.Rows(2).Cells(4).Value, Me.IODatagrid.Rows(3).Cells(4).Value, Me.IODatagrid.Rows(4).Cells(4).Value, Me.IODatagrid.Rows(5).Cells(4).Value, oid)

                oid = 5
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(5).Value, Me.IODatagrid.Rows(1).Cells(5).Value, Me.IODatagrid.Rows(2).Cells(5).Value, Me.IODatagrid.Rows(3).Cells(5).Value, Me.IODatagrid.Rows(4).Cells(5).Value, Me.IODatagrid.Rows(5).Cells(5).Value, oid)

                oid = 6
                Me.OfficerTableAdapter.UpdateQuery(Me.IODatagrid.Rows(0).Cells(6).Value, Me.IODatagrid.Rows(1).Cells(6).Value, Me.IODatagrid.Rows(2).Cells(6).Value, Me.IODatagrid.Rows(3).Cells(6).Value, Me.IODatagrid.Rows(4).Cells(6).Value, Me.IODatagrid.Rows(5).Cells(6).Value, oid)
            End If


            For i = 0 To 5
                For j = 1 To 6
                    Me.IODatagrid.Rows(i).Cells(j).Style.BackColor = Me.IODatagrid.Rows(0).Cells(0).Style.BackColor
                Next
            Next

            ConnectToDatabase()
            LoadOfficerToMemory()

            InsertOrUpdateLastModificationDate(Now, "Modified Officers List")
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "SUPPORTING STAFF"

    Private Sub ClearSSFields()
        Me.txtSSName.Text = ""
        Me.txtSSDesignation.Text = ""
        Me.txtSSPen.Text = ""
        Me.txtSSName.Focus()
    End Sub

    Private Sub ClearSelectedSSFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSName.ButtonCustomClick, txtSSDesignation.ButtonCustomClick, txtSSPen.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString
    End Sub
    Private Sub SaveSSData() Handles btnSaveSS.Click

        If Me.txtSSName.Text = "" Then
            MessageBoxEx.Show("Please enter Name of the Staff", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSSName.Focus()
            Exit Sub
        End If

        If Me.txtSSDesignation.Text = "" Then
            MessageBoxEx.Show("Please enter Designation of the Staff", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSSDesignation.Focus()
            Exit Sub
        End If

        If Me.txtSSPen.Text = "" Then
            MessageBoxEx.Show("Please enter PEN of the Staff", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSSPen.Focus()
            Exit Sub
        End If
        Dim modificationdetail As String = ""
        Try
            If Not SSEditMode Then
                Dim dgvr As FingerPrintDataSet.SupportingStaffRow = Me.FingerPrintDataSet.SupportingStaff.NewRow
                With dgvr
                    .StaffName = Me.txtSSName.Text.Trim
                    .Designation = Me.txtSSDesignation.Text.Trim
                    .PEN = Me.txtSSPen.Text.Trim
                End With
                Me.FingerPrintDataSet.SupportingStaff.AddSupportingStaffRow(dgvr)
                Me.SSTableAdapter1.Update(Me.FingerPrintDataSet.SupportingStaff)
                Me.SSTableAdapter1.Fill(Me.FingerPrintDataSet.SupportingStaff)
                Me.SSBindingSource.MoveLast()
                ShowDesktopAlert("New Supporting Staff added sucessfully!")
                modificationdetail = "New Supporting Staff added"
            Else
                Dim dgvr As FingerPrintDataSet.SupportingStaffRow = Me.FingerPrintDataSet.SupportingStaff.Rows(Me.SSDatagrid.SelectedRows(0).Index)
                With dgvr
                    .StaffName = Me.txtSSName.Text.Trim
                    .Designation = Me.txtSSDesignation.Text.Trim
                    .PEN = Me.txtSSPen.Text.Trim
                End With
                Me.SSTableAdapter1.Update(Me.FingerPrintDataSet.SupportingStaff)
                ShowDesktopAlert("Selected record updated.")
                modificationdetail = "Edited Supporting Staff details"
                SSEditMode = False
                Me.btnSaveSS.Text = "Save"
            End If

            ClearSSFields()
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, modificationdetail)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

    '-------------------------------------------SOC DATA MANIPULATION-----------------------------------------
#Region "SOC DATA ENTRY FIELDS SETTINGS"

    Private Sub InsertRupeeSymbol() Handles lblRupee.Click
        On Error Resume Next
        Me.txtSOCPropertyLost.Text = Me.txtSOCPropertyLost.Text.Substring(0, Me.txtSOCPropertyLost.SelectionStart) + "`" + Me.txtSOCPropertyLost.Text.Substring(Me.txtSOCPropertyLost.SelectionStart)
        Me.txtSOCPropertyLost.Focus()
        Me.txtSOCPropertyLost.Select(Me.txtSOCPropertyLost.Text.Length, 0)
    End Sub


    Private Sub InitializeSOCFields()
        On Error Resume Next

        Me.txtSOCNumber.Focus()
        Dim ctrl As Control
        For Each ctrl In Me.PanelSOC.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                If ctrl.Name <> txtSOCYear.Name Then ctrl.Text = vbNullString
            End If
        Next
        Me.chkGraveCrime.Checked = False
        PhotographerFieldFocussed = False
        PhotographedDateFocussed = False

        ClearDropDownOfficerList()
    End Sub

    Private Sub ClearSOCFields() Handles btnClearSOC.Click 'Clear all textboxes, comboboxes etc
        On Error Resume Next

        Me.txtSOCNumber.Focus()
        Me.dtSOCInspection.Text = vbNullString 'clear dob
        Me.dtSOCReport.Text = vbNullString
        Me.chkGraveCrime.Checked = False
        ClearDropDownOfficerList()
        Dim ctrl As Control
        For Each ctrl In Me.PanelSOC.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                If ctrl.Name <> txtSOCYear.Name Then ctrl.Text = vbNullString
            End If
        Next
        PhotographerFieldFocussed = False
        PhotographedDateFocussed = False
    End Sub

    Private Sub ClearSelectedSOCFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSOCNumber.ButtonCustomClick, dtSOCOccurrence.ButtonCustomClick, txtSOCCrimeNumber.ButtonCustomClick, txtSOCSection.ButtonCustomClick, txtSOCPlace.ButtonCustomClick, txtPSName.ButtonCustomClick, txtSOCPhotographer.ButtonCustomClick, txtSOCDateOfPhotography.ButtonCustomClick, txtSOCOfficer.ButtonCustom2Click
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString

    End Sub

    Private Sub AppendSOCYear() Handles txtSOCNumber.Leave, txtSOCCrimeNumber.Leave
        On Error Resume Next
        If Me.chkAppendSOCYear.Checked = False Then Exit Sub
        Dim y As String = Me.txtSOCYear.Text
        If y = vbNullString Then Exit Sub
        If Me.chkSOCTwodigits.Checked Then y = Strings.Right(y, 2)

        Dim n As String = Trim(Me.txtSOCNumber.Text)
        Dim c As String = Trim(Me.txtSOCCrimeNumber.Text)
        Dim l As Short = Strings.Len(n)
        If n <> vbNullString And l < 11 And y <> vbNullString Then
            If Strings.InStr(n, "/", CompareMethod.Text) = 0 Then
                Me.txtSOCNumber.Text = n & "/" & y
            End If
        End If
        l = Len(c)
        If c <> vbNullString And l < 46 And y <> vbNullString Then
            If Strings.InStr(c, "/", CompareMethod.Text) = 0 Then
                Me.txtSOCCrimeNumber.Text = c & "/" & y
            End If
        End If

    End Sub

    Private Sub CalculateDR() Handles dtSOCReport.GotFocus
        On Error Resume Next
        If Me.dtSOCInspection.IsEmpty = False Then Me.dtSOCReport.Value = Me.dtSOCInspection.Value
    End Sub

    Private Sub CalculateDO() Handles dtSOCOccurrence.GotFocus
        On Error Resume Next
        If Me.dtSOCInspection.IsEmpty = False And Me.dtSOCOccurrence.Text = "" Then Me.dtSOCOccurrence.Text = "Between " & Format(DateAdd(DateInterval.Day, -1, dtSOCInspection.Value), "dd/MM/yy") & " & " & Format(dtSOCInspection.Value, "dd/MM/yy")
        Me.dtSOCOccurrence.Select(Me.dtSOCOccurrence.Text.Length, 0)
    End Sub

    Private Sub CalculateChanceprintCount() Handles txtSOCCPsDeveloped.Validated, txtSOCCPsEliminated.Validated, txtSOCCPsUnfit.Validated
        On Error Resume Next
        If txtSOCCPsDeveloped.Text = vbNullString Then Me.txtSOCCPsDeveloped.Text = "0"
        If txtSOCCPsUnfit.Text = vbNullString Then Me.txtSOCCPsUnfit.Text = "0"
        If txtSOCCPsEliminated.Text = vbNullString Then Me.txtSOCCPsEliminated.Text = "0"

        'SetFileStatus()
    End Sub

    Private Sub SetFileStatus() Handles cmbFileStatus.GotFocus
        On Error Resume Next

        Dim cpsremaining = Me.txtSOCCPsDeveloped.Value - (Me.txtSOCCPsEliminated.Value + Me.txtSOCCPsUnfit.Value)

        If cpsremaining = 0 And Me.cmbFileStatus.Text.Trim = "" Then
            Me.cmbFileStatus.SelectedIndex = 0
        End If

        If cpsremaining > 0 And Me.cmbFileStatus.Text.ToLower <> "closed" And Me.cmbFileStatus.Text.ToLower <> "identified" And Me.cmbFileStatus.Text.ToLower <> "otherwise detected" Then
            Me.cmbFileStatus.Text = ""
        End If
        Me.cmbFileStatus.Select(Me.cmbFileStatus.Text.Length, 0)
    End Sub

    Private Sub cmbFileStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFileStatus.Validated, cmbFileStatus.TextChanged
        On Error Resume Next
        If Me.cmbFileStatus.Text.ToLower <> "identified" And Me.txtSOCComparisonDetails.Text.ToLower.StartsWith("identified as") Then
            Me.txtSOCComparisonDetails.Text = ""
            SetComparisonDetails()
        End If
    End Sub


    Private Sub ValidateChanceprintCount()
        On Error Resume Next
        If txtSOCCPsDeveloped.Text = vbNullString Then Me.txtSOCCPsDeveloped.Text = "0"
        If txtSOCCPsUnfit.Text = vbNullString Then Me.txtSOCCPsUnfit.Text = "0"
        If txtSOCCPsEliminated.Text = vbNullString Then Me.txtSOCCPsEliminated.Text = "0"

        If Me.txtSOCCPsUnfit.Value > txtSOCCPsDeveloped.Value Then
            MessageBoxEx.Show("No. of unfit CPs cannot be greater than the no. of developed CPs", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSOCCPsUnfit.Focus()
            CPValidated = False
            Exit Sub
        End If
        If Me.txtSOCCPsEliminated.Value > txtSOCCPsDeveloped.Value Then
            MessageBoxEx.Show("No. of eliminated CPs cannot be greater than the no. of developed CPs", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSOCCPsEliminated.Focus()
            CPValidated = False
            Exit Sub
        End If
        If (Me.txtSOCCPsEliminated.Value + Me.txtSOCCPsUnfit.Value) > txtSOCCPsDeveloped.Value Then
            MessageBoxEx.Show("Sum of  eliminated CPs and unfit CPs cannot be greater than the no. of developed CPs", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSOCCPsUnfit.Focus()
            CPValidated = False
            Exit Sub
        End If

        If SOCEditMode Then

            If (Me.txtSOCCPsEliminated.Value + Me.txtSOCCPsUnfit.Value + CPIdentified) > txtSOCCPsDeveloped.Value Then
                MessageBoxEx.Show("Sum of  CPs eliminated, CPs unfit and CPs identified cannot be greater than the no. of developed CPs", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtSOCCPsUnfit.Focus()
                CPValidated = False
                Exit Sub
            End If

        End If
        CPValidated = True


    End Sub

    Private Sub GenerateSOCNumberWithoutYear(ByVal SOCNumber As String)
        On Error Resume Next
        Dim s = Strings.Split(SOCNumber, "/")
        Me.txtSOCNumberOnly.Text = s(0)
    End Sub

    Private Sub ValidateSOCNumber() Handles txtSOCNumber.Validated
        On Error Resume Next
        GenerateSOCNumberWithoutYear(Me.txtSOCNumber.Text)
    End Sub

    Private Sub ValidatePhotoReceived() Handles cmbSOCPhotoReceived.Validated
        On Error Resume Next
        Dim pr As String = UCase(Trim(Me.cmbSOCPhotoReceived.Text))
        If pr <> "YES" And pr <> "NO" And pr <> vbNullString Then
            If pr = "Y" Then
                Me.cmbSOCPhotoReceived.Text = "Yes"
            ElseIf pr = "N" Then
                Me.cmbSOCPhotoReceived.Text = "No"
            Else
                DevComponents.DotNetBar.MessageBoxEx.Show("Only the values 'Yes', 'No' and blank are accepted for the field 'Photo Received or Not'. Please enter your choice or leave it blank", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.cmbSOCPhotoReceived.Focus()
            End If

        End If
    End Sub

    Private Sub SetComparisonDetails() Handles txtSOCComparisonDetails.GotFocus
        On Error Resume Next

        If Me.txtSOCComparisonDetails.Text <> vbNullString Then Exit Sub

        If Me.txtSOCCPsDeveloped.Value = 0 Then
            Me.txtSOCComparisonDetails.Text = "No action pending."
        End If

    End Sub


    Private Sub FindSOCNumber() Handles dtSOCInspection.GotFocus
        On Error Resume Next
        Dim p = Me.SOCRegisterBindingSource.Find("SOCNumber", Trim(Me.txtSOCNumber.Text))
        If p < 0 Then Exit Sub
        Me.SOCRegisterBindingSource.Position = p
    End Sub


#End Region


#Region "SOC MANDATORY FIELDS"


    Function MandatorySOCFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtSOCNumber.Text) = vbNullString Or Me.dtSOCInspection.IsEmpty Or Trim(Me.cmbSOCPoliceStation.Text) = vbNullString Or Trim(Me.txtSOCCrimeNumber.Text) = vbNullString Or Trim(Me.txtSOCSection.Text) = vbNullString Or Trim(Me.txtSOCOfficer.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub ShowMandatorySOCFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Trim(Me.txtSOCNumber.Text) = vbNullString Then
            msg = msg & " SOC Number" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.dtSOCInspection.Text) = vbNullString Then
            msg = msg & " Date of Inspection" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If
        If Trim(Me.cmbSOCPoliceStation.Text) = vbNullString Then
            msg = msg & " Police Station" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
        End If
        If Trim(Me.txtSOCCrimeNumber.Text) = vbNullString Then
            msg = msg & " Crime Number" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If
        If Trim(Me.txtSOCSection.Text) = vbNullString Then
            msg = msg & " Section of Law" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If
        If Trim(Me.txtSOCOfficer.Text) = vbNullString Then
            msg = msg & " Inspecting Officer" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 Then x = 7
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                txtSOCYear.Focus()
            Case 2
                txtSOCNumber.Focus()
            Case 3
                dtSOCInspection.Focus()
            Case 4
                cmbSOCPoliceStation.Focus()
            Case 5
                txtSOCCrimeNumber.Focus()
            Case 6
                txtSOCSection.Focus()
            Case 7
                txtSOCOfficer.Focus()
        End Select

    End Sub


#End Region


#Region "SOC SAVE BUTTON ACTION"

    Private Sub SOCSaveButtonAction() Handles btnSaveSOC.Click
        On Error Resume Next

        If MandatorySOCFieldsNotFilled() Then
            ShowMandatorySOCFieldsInfo()
            Exit Sub
        End If

        If Me.dtSOCReport.Value > Me.dtSOCInspection.Value Then
            MessageBoxEx.Show("Date of Report (" & Me.dtSOCReport.Text & ") should be on or before the Date of Inspection (" & Me.dtSOCInspection.Text & "). Please correct the error.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtSOCReport.Focus()
            Exit Sub
        End If

        If Me.txtSOCCPsDeveloped.Text = "0" And UCase(Me.cmbSOCPhotoReceived.Text) = "YES" Then
            MessageBoxEx.Show("The value of 'YES' for the field 'Photo Received or Not' is invalid as the number of prints developed is zero. Please correct the error.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.cmbSOCPhotoReceived.Focus()
            Exit Sub
        End If
        ValidateChanceprintCount()
        If CPValidated = False Then Exit Sub
        SetComparisonDetails()
        SetFileStatus()
        AddTextsToAutoCompletionList()


        If SOCEditMode Then
            UpdateSOCData()
        Else
            SaveNewSOCEntry()
        End If

    End Sub
#End Region


#Region "SOC NEW DATA ENTRY"


    Private Sub SaveNewSOCEntry()
        Try

            OriginalSOCNumber = Trim(Me.txtSOCNumber.Text)
            GenerateSOCNumberWithoutYear(OriginalSOCNumber)
            Dim sYear = Me.txtSOCNumberOnly.Text
            Dim dto = Trim(Me.dtSOCOccurrence.Text)
            Dim ps = Trim(Me.cmbSOCPoliceStation.Text)
            Dim cr = Trim(Me.txtSOCCrimeNumber.Text)
            Dim sec = Trim(Me.txtSOCSection.Text)
            Dim po = Trim(Me.txtSOCPlace.Text)
            Dim complainant = Trim(Me.txtSOCComplainant.Text)
            Dim mo = Trim(Me.cmbSOCModus.Text)
            Dim pl = Trim(Me.txtSOCPropertyLost.Text)
            Dim cpdeveloped = Me.txtSOCCPsDeveloped.Value
            Dim cpunfit = Me.txtSOCCPsUnfit.Value
            Dim cpeliminated = Me.txtSOCCPsEliminated.Value

            Dim cpdetails = Trim(Me.txtSOCCPDetails.Text)
            Dim photographer = Trim(Me.txtSOCPhotographer.Text)
            Dim photoreceived = Trim(Me.cmbSOCPhotoReceived.Text)
            Dim dateofreceptionofphoto = Trim(Me.txtSOCDateOfPhotography.Text)
            Dim officer = Trim(Me.txtSOCOfficer.Text).Replace("; ", vbNewLine)
            Dim gist = Trim(Me.txtSOCGist.Text)
            Dim comparisondetails = Trim(Me.txtSOCComparisonDetails.Text)
            Dim gravecrime As Boolean = Me.chkGraveCrime.Checked
            Dim filestatus As String = Trim(Me.cmbFileStatus.Text)
            Dim cpsidentified As String = "0"

            If filestatus.ToLower = "otherwise detected" Then
                If comparisondetails.ToLower.StartsWith("otherwise detected") = False Then
                    comparisondetails = "Otherwise detected"
                End If
            End If


            If SOCNumberExists(OriginalSOCNumber) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the SOC Number " & OriginalSOCNumber & " already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.Yes Then
                    OverWriteSOCData()
                Else
                    Me.txtSOCNumber.Focus()
                    Me.txtSOCNumber.SelectAll()
                End If

                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.SOCRegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.SOCRegister.NewSOCRegisterRow()
            With newRow
                .SOCNumber = OriginalSOCNumber
                .SOCYear = sYear
                .DateOfInspection = Me.dtSOCInspection.ValueObject

                If Me.dtSOCReport.IsEmpty = False Then .DateOfReport = dtSOCReport.Value

                .DateOfOccurrence = dto
                .PoliceStation = ps
                .CrimeNumber = cr
                .SectionOfLaw = sec
                .PlaceOfOccurrence = po
                .Complainant = complainant
                .ModusOperandi = mo
                .PropertyLost = pl
                .ChancePrintsDeveloped = cpdeveloped
                .ChancePrintsUnfit = cpunfit
                .ChancePrintsEliminated = cpeliminated

                .ChancePrintDetails = cpdetails
                .Photographer = photographer
                .PhotoReceived = photoreceived
                .DateOfReceptionOfPhoto = dateofreceptionofphoto
                .InvestigatingOfficer = officer
                .Gist = gist
                .ComparisonDetails = comparisondetails
                .GraveCrime = gravecrime
                .FileStatus = filestatus
                .CPsIdentified = "0"
            End With

            Me.FingerPrintDataSet.SOCRegister.Rows.Add(newRow) ' add the row to the table
            Me.SOCRegisterBindingSource.Position = Me.SOCRegisterBindingSource.Find("SOCNumber", OriginalSOCNumber)

            Me.SOCRegisterTableAdapter.Insert(OriginalSOCNumber, sYear, Me.dtSOCInspection.ValueObject, Me.dtSOCReport.ValueObject, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, photographer, photoreceived, dateofreceptionofphoto, officer, gist, comparisondetails, gravecrime, filestatus, cpsidentified) 'update the database


            InitializeSOCFields()
            IncrementSOCNumber(OriginalSOCNumber)
            DisplayDatabaseInformation()
            ShowDesktopAlert("New SOC Record entered successfully!")
            InsertOrUpdateLastModificationDate(Now, "Added new SOC No. " & OriginalSOCNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Function SOCNumberExists(ByVal SOCNumber As String)
        On Error Resume Next
        If Me.SOCRegisterTableAdapter.CheckSOCNumberExists(SOCNumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub btnEnterIDRDetailsContextMenu_Click(sender As Object, e As EventArgs) Handles btnEnterIDRDetailsContextMenu.Click
        Me.Cursor = Cursors.WaitCursor
        blIDRNewDataMode = True
        blIDREditMode = False
        Me.TabControl.SelectedTab = IDRTabItem
        FrmIdentificationRegisterDE.Show()
        FrmIdentificationRegisterDE.BringToFront()
        FrmIdentificationRegisterDE.txtSOCNumber.Text = Me.SOCDatagrid.SelectedRows(0).Cells(0).Value
        FrmIdentificationRegisterDE.txtSOCNumber.Focus()
        FrmIdentificationRegisterDE.txtSOCNumber_LostFocus()
        Cursor = Cursors.Default
    End Sub
#End Region


#Region "SOC EDIT RECORD"

    Private Sub UpdateSOCData()

        Try

            Dim NewSOCNumber As String = Trim(Me.txtSOCNumber.Text)
            GenerateSOCNumberWithoutYear(NewSOCNumber)
            Dim sYear = Me.txtSOCNumberOnly.Text
            Dim dti = Me.dtSOCInspection.ValueObject
            Dim dtr = Me.dtSOCReport.ValueObject
            Dim dto = Trim(Me.dtSOCOccurrence.Text)
            Dim ps = Trim(Me.cmbSOCPoliceStation.Text)
            Dim cr = Trim(Me.txtSOCCrimeNumber.Text)
            Dim sec = Trim(Me.txtSOCSection.Text)
            Dim po = Trim(Me.txtSOCPlace.Text)
            Dim complainant = Trim(Me.txtSOCComplainant.Text)
            Dim mo = Trim(Me.cmbSOCModus.Text)
            Dim pl = Trim(Me.txtSOCPropertyLost.Text)
            Dim cpdeveloped = Me.txtSOCCPsDeveloped.Value
            Dim cpunfit = Me.txtSOCCPsUnfit.Value
            Dim cpeliminated = Me.txtSOCCPsEliminated.Value

            Dim cpdetails = Trim(Me.txtSOCCPDetails.Text)
            Dim photographer = Trim(Me.txtSOCPhotographer.Text)
            Dim photoreceived = Trim(Me.cmbSOCPhotoReceived.Text)
            Dim dateofreceptionofphoto = Trim(Me.txtSOCDateOfPhotography.Text)
            Dim gist = Trim(Me.txtSOCGist.Text)
            Dim officer = Trim(Me.txtSOCOfficer.Text).Replace("; ", vbNewLine)
            Dim comparisondetails = Trim(Me.txtSOCComparisonDetails.Text)
            Dim gravecrime As Boolean = Me.chkGraveCrime.Checked
            Dim filestatus As String = Trim(Me.cmbFileStatus.Text)



            If filestatus.ToLower = "otherwise detected" Then
                If comparisondetails.ToLower.StartsWith("otherwise detected") = False Then
                    comparisondetails = "Otherwise detected"
                End If
            End If

            If LCase(NewSOCNumber) <> LCase(OriginalSOCNumber) Then
                If SOCNumberExists(NewSOCNumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the SOC Number " & NewSOCNumber & " already exists. Please enter a different SOC Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtSOCNumber.Focus()
                    Me.txtSOCNumber.SelectAll()
                    Exit Sub
                End If
            End If


            Dim oldRow As FingerPrintDataSet.SOCRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.SOCRegister.FindBySOCNumber(OriginalSOCNumber)
            If oldRow IsNot Nothing Then

                With oldRow
                    .SOCNumber = NewSOCNumber
                    .SOCYear = sYear
                    .DateOfInspection = dtSOCInspection.Value

                    If Me.dtSOCReport.IsEmpty = False Then
                        .DateOfReport = dtSOCReport.Value
                    Else
                        .DateOfReport = Nothing
                    End If

                    .DateOfOccurrence = dto
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = sec
                    .PlaceOfOccurrence = po
                    .Complainant = complainant
                    .ModusOperandi = mo
                    .PropertyLost = pl
                    .ChancePrintsDeveloped = cpdeveloped
                    .ChancePrintsUnfit = cpunfit
                    .ChancePrintsEliminated = cpeliminated

                    .ChancePrintDetails = cpdetails
                    .Photographer = photographer
                    .PhotoReceived = photoreceived
                    .DateOfReceptionOfPhoto = dateofreceptionofphoto
                    .InvestigatingOfficer = officer
                    .Gist = gist
                    .ComparisonDetails = comparisondetails
                    .GraveCrime = gravecrime
                    .FileStatus = filestatus
                End With
            End If
            Me.SOCRegisterBindingSource.Position = Me.SOCRegisterBindingSource.Find("SOCNumber", NewSOCNumber)

            Me.SOCRegisterTableAdapter.UpdateQuery(NewSOCNumber, sYear, dti, dtr, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, photographer, photoreceived, dateofreceptionofphoto, officer, gist, comparisondetails, gravecrime, filestatus, OriginalSOCNumber)

            If LCase(NewSOCNumber) <> LCase(OriginalSOCNumber) Then
                Me.RSOCRegisterTableAdapter.UpdateRSOCWithSOCEdit(NewSOCNumber, sYear, dti, ps, cr, officer, OriginalSOCNumber)
                LoadRSOCRecords()
            End If


            ShowDesktopAlert("Selected Record updated successfully!")

            InitializeSOCFields()
            GenerateNewSOCNumber()
            Me.dtSOCInspection.Value = Today
            Me.dtSOCReport.Value = Today

            Me.btnSaveSOC.Text = "Save"
            SOCEditMode = False

            InsertOrUpdateLastModificationDate(Now, "Edited SOC No. " & OriginalSOCNumber)
            DisplayDatabaseInformation()

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.SOCRegisterBindingSource.Position = Me.SOCRegisterBindingSource.Find("SOCNumber", OriginalSOCNumber)
        End Try


    End Sub
#End Region


#Region "OVERWRITE SOC DATA"

    Public Sub OverWriteSOCData()
        Try

            Dim NewSOCNumber As String = Trim(Me.txtSOCNumber.Text)
            GenerateSOCNumberWithoutYear(NewSOCNumber)
            Dim sYear = Me.txtSOCNumberOnly.Text
            Dim dti = Me.dtSOCInspection.ValueObject
            Dim dtr = Me.dtSOCReport.ValueObject
            Dim dto = Trim(Me.dtSOCOccurrence.Text)
            Dim ps = Trim(Me.cmbSOCPoliceStation.Text)
            Dim cr = Trim(Me.txtSOCCrimeNumber.Text)
            Dim sec = Trim(Me.txtSOCSection.Text)
            Dim po = Trim(Me.txtSOCPlace.Text)
            Dim complainant = Trim(Me.txtSOCComplainant.Text)
            Dim mo = Trim(Me.cmbSOCModus.Text)
            Dim pl = Trim(Me.txtSOCPropertyLost.Text)
            Dim cpdeveloped = Me.txtSOCCPsDeveloped.Value
            Dim cpunfit = Me.txtSOCCPsUnfit.Value
            Dim cpeliminated = Me.txtSOCCPsEliminated.Value

            Dim cpdetails = Trim(Me.txtSOCCPDetails.Text)
            Dim photographer = Trim(Me.txtSOCPhotographer.Text)
            Dim photoreceived = Trim(Me.cmbSOCPhotoReceived.Text)
            Dim dateofreceptionofphoto = Trim(Me.txtSOCDateOfPhotography.Text)
            Dim gist = Trim(Me.txtSOCGist.Text)
            Dim officer = Trim(Me.txtSOCOfficer.Text).Replace("; ", vbNewLine)
            Dim comparisondetails = Trim(Me.txtSOCComparisonDetails.Text)
            Dim gravecrime As Boolean = Me.chkGraveCrime.Checked
            Dim filestatus As String = Trim(Me.cmbFileStatus.Text)


            If filestatus.ToLower = "otherwise detected" Then
                If comparisondetails.ToLower.StartsWith("otherwise detected") = False Then
                    comparisondetails = "Otherwise detected"
                End If
            End If


            Dim oldRow As FingerPrintDataSet.SOCRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.SOCRegister.FindBySOCNumber(OriginalSOCNumber)
            If oldRow IsNot Nothing Then
                With oldRow
                    .SOCNumber = NewSOCNumber
                    .SOCYear = sYear
                    .DateOfInspection = dtSOCInspection.Value

                    If Me.dtSOCReport.IsEmpty = False Then
                        .DateOfReport = dtSOCReport.Value
                    Else
                        .DateOfReport = Nothing
                    End If

                    .DateOfOccurrence = dto
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = sec
                    .PlaceOfOccurrence = po
                    .Complainant = complainant
                    .ModusOperandi = mo
                    .PropertyLost = pl
                    .ChancePrintsDeveloped = cpdeveloped
                    .ChancePrintsUnfit = cpunfit
                    .ChancePrintsEliminated = cpeliminated

                    .ChancePrintDetails = cpdetails
                    .Photographer = photographer
                    .PhotoReceived = photoreceived
                    .DateOfReceptionOfPhoto = dateofreceptionofphoto
                    .InvestigatingOfficer = officer
                    .Gist = gist
                    .ComparisonDetails = comparisondetails
                    .GraveCrime = gravecrime
                    .FileStatus = filestatus
                End With
            End If
            Me.SOCRegisterBindingSource.Position = Me.SOCRegisterBindingSource.Find("SOCNumber", NewSOCNumber)

            Me.SOCRegisterTableAdapter.UpdateQuery(NewSOCNumber, sYear, dti, dtr, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, photographer, photoreceived, dateofreceptionofphoto, officer, gist, comparisondetails, gravecrime, filestatus, OriginalSOCNumber)


            ShowDesktopAlert("SOC Record overwritten!")

            InitializeSOCFields()
            ' IncrementSOCNumber(NewSOCNumber)
            GenerateNewSOCNumber()
            Me.dtSOCInspection.Value = Today
            Me.dtSOCReport.Value = Today

            Me.btnSaveSOC.Text = "Save"
            SOCEditMode = False
            InsertOrUpdateLastModificationDate(Now, "Edited SOC No. " & OriginalSOCNumber)
            DisplayDatabaseInformation()

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.SOCRegisterBindingSource.Position = Me.SOCRegisterBindingSource.Find("SOCNumber", OriginalSOCNumber)
        End Try


    End Sub
#End Region


#Region "UPDATE PHOTO RECEIVED"
    Private Sub UpdatePhotoReceived() Handles btnPhotoReceivedContext.Click
        On Error Resume Next
        Dim y As String = vbNullString
        Dim soc As String = Me.SOCDatagrid.SelectedCells(0).Value.ToString
        If Me.btnPhotoReceivedContext.Checked = True Then
            y = "Yes"
        Else
            y = "No"
        End If

        Dim oldRow As FingerPrintDataSet.SOCRegisterRow 'add a new row to insert values
        oldRow = Me.FingerPrintDataSet.SOCRegister.FindBySOCNumber(soc)
        If oldRow IsNot Nothing Then

            With oldRow
                .PhotoReceived = y
            End With
        End If

        Me.SOCRegisterTableAdapter.UpdatePhotoReceived(y, soc)
        InsertOrUpdateLastModificationDate(Now, "Photo received status set to '" & y & "' for SOC No. " & soc)
        ShowDesktopAlert("Photo received status set to '" & y & "'")
    End Sub
#End Region


#Region "SEARCH SOC RECORDS"


    Private Sub SearchTextMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBeginsWith.CheckedChanged, rdoFullText.CheckedChanged, rdoAnyWhere.CheckedChanged
        On Error Resume Next

        If rdoBeginsWith.Checked = True Then SearchSetting = 0
        If rdoFullText.Checked = True Then SearchSetting = 1
        If rdoAnyWhere.Checked = True Then SearchSetting = 2
    End Sub


    Public Sub SearchWithSOCNumber() Handles btnSOCFindByNumber.Click
        Try

            If Me.txtSOCNumber.Text = "" Then
                MessageBoxEx.Show("Please enter the SOC Number to search for.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtSOCNumber.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.SOCRegisterTableAdapter.FillByNumber(Me.FingerPrintDataSet.SOCRegister, Me.txtSOCNumber.Text)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.SOCDatagrid.RowCount < 2, Me.SOCDatagrid.RowCount & " Record", Me.SOCDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SearchWithGraveCrime() Handles btnSearchWithGraveCrime.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SOCRegisterTableAdapter.FillByGraveCrime(Me.FingerPrintDataSet.SOCRegister, Me.chkGraveCrime.Checked)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.SOCDatagrid.RowCount < 2, Me.SOCDatagrid.RowCount & " Record", Me.SOCDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub

    Public Sub SearchSOC() Handles btnSearchSOC.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim sNumber = Me.txtSOCNumber.Text

            Dim dti = Me.dtSOCInspection.ValueObject
            Dim dtr = Me.dtSOCReport.ValueObject
            Dim dto = Me.dtSOCOccurrence.Text
            Dim ps = cmbSOCPoliceStation.Text
            Dim cr = txtSOCCrimeNumber.Text
            Dim sec = txtSOCSection.Text
            Dim po = txtSOCPlace.Text
            Dim complainant = txtSOCComplainant.Text
            Dim mo = cmbSOCModus.Text
            Dim pl = txtSOCPropertyLost.Text
            Dim cpdeveloped = Me.txtSOCCPsDeveloped.Text
            Dim cpunfit = Me.txtSOCCPsUnfit.Text
            Dim cpeliminated = Me.txtSOCCPsEliminated.Text

            Dim cpdetails = txtSOCCPDetails.Text
            Dim photographer = Me.txtSOCPhotographer.Text
            Dim photoreceived = Me.cmbSOCPhotoReceived.Text
            Dim dateofreceptionofphoto = txtSOCDateOfPhotography.Text
            Dim gist = Me.txtSOCGist.Text
            Dim inspectingofficer = txtSOCOfficer.Text.Replace("; ", vbNewLine)
            Dim comparisondetails = txtSOCComparisonDetails.Text

            Dim filestatus = Me.cmbFileStatus.Text

            If SearchSetting = 0 Then
                sNumber = sNumber & "%"
                dto = dto & "%"
                ps = ps & "%"
                cr = cr & "%"
                sec = sec & "%"
                po = po & "%"
                complainant = complainant & "%"
                mo = mo & "%"
                pl = pl & "%"
                cpdeveloped = cpdeveloped & "%"
                cpunfit = cpunfit & "%"
                cpeliminated = cpeliminated & "%"

                cpdetails = cpdetails & "%"
                photographer = photographer & "%"
                photoreceived = photoreceived & "%"
                dateofreceptionofphoto = dateofreceptionofphoto & "%"
                gist = gist & "%"
                inspectingofficer = inspectingofficer & "%"
                comparisondetails = comparisondetails & "%"
                filestatus = filestatus & "%"
            End If


            If SearchSetting = 1 Then
                If sNumber = vbNullString Then sNumber = "%"
                If dto = vbNullString Then dto = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If sec = vbNullString Then sec = "%"
                If po = vbNullString Then po = "%"
                If complainant = vbNullString Then complainant = "%"
                If mo = vbNullString Then mo = "%"
                If pl = vbNullString Then pl = "%"
                If cpdeveloped = vbNullString Then cpdeveloped = "%"
                If cpunfit = vbNullString Then cpunfit = "%"
                If cpeliminated = vbNullString Then cpeliminated = "%"

                If cpdetails = vbNullString Then cpdetails = "%"
                If photographer = vbNullString Then photographer = "%"
                If photoreceived = vbNullString Then photoreceived = "%"
                If cmbSOCPhotoReceived.Text = vbNullString Then dateofreceptionofphoto = "%"
                If gist = vbNullString Then gist = "%"
                If inspectingofficer = vbNullString Then inspectingofficer = "%"
                If comparisondetails = vbNullString Then comparisondetails = "%"
                If filestatus = vbNullString Then filestatus = "%"

            End If

            If SearchSetting = 2 Then
                sNumber = "%" & sNumber & "%"
                dto = "%" & dto & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                sec = "%" & sec & "%"
                po = "%" & po & "%"
                complainant = "%" & complainant & "%"
                mo = "%" & mo & "%"
                pl = "%" & pl & "%"
                cpdeveloped = "%" & cpdeveloped & "%"
                cpunfit = "%" & cpunfit & "%"
                cpeliminated = "%" & cpeliminated & "%"

                cpdetails = "%" & cpdetails & "%"
                photographer = "%" & photographer & "%"
                photoreceived = "%" & photoreceived & "%"
                dateofreceptionofphoto = "%" & dateofreceptionofphoto & "%"
                gist = "%" & gist & "%"
                inspectingofficer = "%" & inspectingofficer & "%"
                comparisondetails = "%" & comparisondetails & "%"
                filestatus = "%" & filestatus & "%"
            End If

            If Me.dtSOCInspection.IsEmpty And Me.dtSOCReport.IsEmpty Then
                Me.SOCRegisterTableAdapter.FillWithoutDIDR(FingerPrintDataSet.SOCRegister, sNumber, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparisondetails, photographer, photoreceived, dateofreceptionofphoto, gist, filestatus)
            End If

            If Me.dtSOCInspection.IsEmpty = False And Me.dtSOCReport.IsEmpty = False Then
                Me.SOCRegisterTableAdapter.FillByDIDR(FingerPrintDataSet.SOCRegister, sNumber, dti, dtr, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparisondetails, photographer, photoreceived, dateofreceptionofphoto, gist, filestatus)
            End If

            If Me.dtSOCInspection.IsEmpty = True And Me.dtSOCReport.IsEmpty = False Then
                Me.SOCRegisterTableAdapter.FillByDR(FingerPrintDataSet.SOCRegister, sNumber, dtr, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparisondetails, photographer, photoreceived, dateofreceptionofphoto, gist, filestatus)
            End If

            If Me.dtSOCInspection.IsEmpty = False And Me.dtSOCReport.IsEmpty = True Then
                Me.SOCRegisterTableAdapter.FillByDI(FingerPrintDataSet.SOCRegister, sNumber, dti, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparisondetails, photographer, photoreceived, dateofreceptionofphoto, gist, filestatus)
            End If

            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.SOCDatagrid.RowCount < 2, Me.SOCDatagrid.RowCount & " Record", Me.SOCDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub SearchSOCInSelectedYear() Handles btnSearchSOCInYear.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim y As String = Me.txtSOCYear.Text
            If y = vbNullString Then
                MessageBoxEx.Show("Please enter the year in the year field", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtSOCYear.Focus()
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim d1 As Date = New Date(y, 1, 1)
            Dim d2 As Date = New Date(y, 12, 31)
            Dim sNumber = Me.txtSOCNumber.Text

            Dim dti = Me.dtSOCInspection.ValueObject
            Dim dtr = Me.dtSOCReport.ValueObject
            Dim dto = Me.dtSOCOccurrence.Text
            Dim ps = cmbSOCPoliceStation.Text
            Dim cr = txtSOCCrimeNumber.Text
            Dim sec = txtSOCSection.Text
            Dim po = txtSOCPlace.Text
            Dim complainant = txtSOCComplainant.Text
            Dim mo = cmbSOCModus.Text
            Dim pl = txtSOCPropertyLost.Text
            Dim cpdeveloped = Me.txtSOCCPsDeveloped.Text
            Dim cpunfit = Me.txtSOCCPsUnfit.Text
            Dim cpeliminated = Me.txtSOCCPsEliminated.Text

            Dim cpdetails = txtSOCCPDetails.Text
            Dim photographer = Me.txtSOCPhotographer.Text
            Dim photoreceived = Me.cmbSOCPhotoReceived.Text
            Dim dateofreceptionofphoto = txtSOCDateOfPhotography.Text
            Dim gist = Me.txtSOCGist.Text
            Dim inspectingofficer = txtSOCOfficer.Text.Replace("; ", vbNewLine)
            Dim comparison = txtSOCComparisonDetails.Text
            Dim filestatus = Me.cmbFileStatus.Text


            If SearchSetting = 0 Then
                sNumber = sNumber & "%"
                dto = dto & "%"
                ps = ps & "%"
                cr = cr & "%"
                sec = sec & "%"
                po = po & "%"
                complainant = complainant & "%"
                mo = mo & "%"
                pl = pl & "%"
                cpdeveloped = cpdeveloped & "%"
                cpunfit = cpunfit & "%"
                cpeliminated = cpeliminated & "%"

                cpdetails = cpdetails & "%"
                photographer = photographer & "%"
                photoreceived = photoreceived & "%"
                dateofreceptionofphoto = dateofreceptionofphoto & "%"
                gist = gist & "%"
                inspectingofficer = inspectingofficer & "%"
                comparison = comparison & "%"
                filestatus = filestatus & "%"
            End If


            If SearchSetting = 1 Then
                If sNumber = vbNullString Then sNumber = "%"
                If dto = vbNullString Then dto = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If sec = vbNullString Then sec = "%"
                If po = vbNullString Then po = "%"
                If complainant = vbNullString Then complainant = "%"
                If mo = vbNullString Then mo = "%"
                If pl = vbNullString Then pl = "%"
                If cpdeveloped = vbNullString Then cpdeveloped = "%"
                If cpunfit = vbNullString Then cpunfit = "%"
                If cpeliminated = vbNullString Then cpeliminated = "%"

                If cpdetails = vbNullString Then cpdetails = "%"
                If photographer = vbNullString Then photographer = "%"
                If photoreceived = vbNullString Then photoreceived = "%"
                If cmbSOCPhotoReceived.Text = vbNullString Then dateofreceptionofphoto = "%"
                If gist = vbNullString Then gist = "%"
                If inspectingofficer = vbNullString Then inspectingofficer = "%"
                If comparison = vbNullString Then comparison = "%"
                If filestatus = vbNullString Then filestatus = "%"
            End If

            If SearchSetting = 2 Then


                sNumber = "%" & sNumber & "%"
                dto = "%" & dto & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                sec = "%" & sec & "%"
                po = "%" & po & "%"
                complainant = "%" & complainant & "%"
                mo = "%" & mo & "%"
                pl = "%" & pl & "%"
                cpdeveloped = "%" & cpdeveloped & "%"
                cpunfit = "%" & cpunfit & "%"
                cpeliminated = "%" & cpeliminated & "%"

                cpdetails = "%" & cpdetails & "%"
                photographer = "%" & photographer & "%"
                photoreceived = "%" & photoreceived & "%"
                dateofreceptionofphoto = "%" & dateofreceptionofphoto & "%"
                gist = "%" & gist & "%"
                inspectingofficer = "%" & inspectingofficer & "%"
                comparison = "%" & comparison & "%"
                filestatus = "%" & filestatus & "%"
            End If

            If Me.dtSOCInspection.IsEmpty And Me.dtSOCReport.IsEmpty Then
                Me.SOCRegisterTableAdapter.FillWithoutDIDRSelectedYear(FingerPrintDataSet.SOCRegister, sNumber, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparison, photographer, photoreceived, dateofreceptionofphoto, gist, d1, d2, filestatus)
            End If

            If Me.dtSOCInspection.IsEmpty = False And Me.dtSOCReport.IsEmpty = False Then
                Me.SOCRegisterTableAdapter.FillByDIDRSelectedYear(FingerPrintDataSet.SOCRegister, sNumber, dti, dtr, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparison, photographer, photoreceived, dateofreceptionofphoto, gist, d1, d2, filestatus)
            End If

            If Me.dtSOCInspection.IsEmpty = True And Me.dtSOCReport.IsEmpty = False Then
                Me.SOCRegisterTableAdapter.FillByDRSelectedYear(FingerPrintDataSet.SOCRegister, sNumber, dtr, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparison, photographer, photoreceived, dateofreceptionofphoto, gist, d1, d2, filestatus)
            End If

            If Me.dtSOCInspection.IsEmpty = False And Me.dtSOCReport.IsEmpty = True Then
                Me.SOCRegisterTableAdapter.FillByDISelectedYear(FingerPrintDataSet.SOCRegister, sNumber, dti, dto, ps, cr, sec, po, complainant, mo, pl, cpdeveloped, cpunfit, cpeliminated, cpdetails, inspectingofficer, comparison, photographer, photoreceived, dateofreceptionofphoto, gist, d1, d2, filestatus)
            End If

            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.SOCDatagrid.RowCount < 2, Me.SOCDatagrid.RowCount & " Record", Me.SOCDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "CHANCE PRINT IMAGE SETTINGS"

    Private Sub SetCPImageImportLocation() Handles btnChangeCPLocation.Click
        On Error Resume Next
        Dim reply As DialogResult

        reply = MessageBoxEx.Show("Previously imported images will not be visible after changing the Chance Print Image Location." & vbNewLine & "You will need to manually copy the old images to the new location to see them again." & vbNewLine & "Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        GetCPImageImportLocation()
        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location to save imported Chance Prints"
        Me.FolderBrowserDialog1.SelectedPath = CPImageImportLocation
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then
            CPImageImportLocation = Me.FolderBrowserDialog1.SelectedPath
            If CPImageImportLocation.EndsWith("Chance Prints") = False Then
                CPImageImportLocation = CPImageImportLocation & "\Chance Prints"
            End If
            CPImageImportLocation = CPImageImportLocation.Replace("\\", "\")
            Dim id As Integer = 1
            Me.SettingsTableAdapter1.SetCPImageLocation(CPImageImportLocation, id)
            GetCPImageImportLocation()
            My.Computer.FileSystem.CreateDirectory(CPImageImportLocation)
            ShowDesktopAlert("Chance Print location changed!")
        End If
    End Sub


    Private Sub GetCPImageImportLocation()
        On Error Resume Next
        If CPImageImportLocation.EndsWith("\") = False Then
            CPImageImportLocation = CPImageImportLocation & "\"
        End If
        CPImageImportLocation = CPImageImportLocation.Replace("\\", "\")
    End Sub


    Private Sub ExploreCPImageImportLocation() Handles btnExploreCPLocation.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FileIO.FileSystem.DirectoryExists(CPImageImportLocation) Then
            Call Shell("explorer.exe " & CPImageImportLocation, AppWinStyle.NormalFocus)
        Else
            FileIO.FileSystem.CreateDirectory(CPImageImportLocation)
            Call Shell("explorer.exe " & CPImageImportLocation, AppWinStyle.NormalFocus)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub LocateChancePrints() Handles btnLocateCP.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim yr As String = Year(Me.SOCDatagrid.SelectedCells(2).Value.ToString)
        Dim soc As String = Strings.Format(Me.SOCDatagrid.SelectedCells(1).Value, "000")
        Dim Location As String = CPImageImportLocation & yr & "\SOC No. " & soc

        If FileIO.FileSystem.DirectoryExists(Location) Then
            Call Shell("explorer.exe " & Location, AppWinStyle.NormalFocus)
        Else
            MessageBoxEx.Show("No chance prints have been imported for the selected SOC Number", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub ImportChancePrints() Handles btnImportCP.Click
        Try

        
        If Me.devmanager.DeviceInfos.Count = 0 Then
            MessageBoxEx.Show("No compatible Scanner or Camera Device detected. Please connect one!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor

        Dim yr As String = Year(Me.SOCDatagrid.SelectedCells(2).Value.ToString)
        Dim soc As String = Strings.Format(Me.SOCDatagrid.SelectedCells(1).Value, "000")
        Dim Location As String = CPImageImportLocation & yr & "\SOC No. " & soc

        If FileIO.FileSystem.DirectoryExists(Location) Then
            If FileIO.FileSystem.GetFiles(Location).Count <> 0 Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A folder for the selected SOC No. already exists. Do you want to copy images to this folder?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If



        Dim FileName As String
        Dim count As Integer
        Dim saved As Boolean

        Dim dev As WIA.Device
        Dim dg As New WIA.CommonDialog
        Dim SelectedItems As WIA.Items
        Dim img As WIA.ImageFile
        Dim itm As WIA.Item

        dev = dg.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, False, True) 'show select device message
        SelectedItems = dg.ShowSelectItems(dev, WIA.WiaImageIntent.UnspecifiedIntent, WIA.WiaImageBias.MaximizeQuality, False, True, True) 'show the pictures in the device selected

        count = SelectedItems.Count

        If count = 1 Then
            itm = SelectedItems(1)
            img = dg.ShowTransfer(itm, , True) 'show default transfer progress dialog
            FileName = itm.Properties("Item Name").Value 'use the original name
            saved = SaveImage(img, Location, FileName)
            ShowDesktopAlert(count & "1 image imported sucessfully!")
        End If

        If count > 1 Then
            FrmImportTransferRate.StartPosition = FormStartPosition.CenterScreen
            FrmImportTransferRate.Show()
            FrmImportTransferRate.ProgressBarX1.Maximum = count
            boolCancelImport = False
            Dim i As Integer
            For i = 1 To count
                If boolCancelImport Then Exit For
                itm = SelectedItems(i)
                FrmImportTransferRate.ProgressBarX1.Text = "Imported image " & i & " of " & count
                FrmImportTransferRate.ProgressBarX1.Increment(1)
                Application.DoEvents()
                img = itm.Transfer()
                FileName = itm.Properties("Item Name").Value 'use the original name
                saved = SaveImage(img, Location, FileName)

            Next
            FrmImportTransferRate.Close()
            ShowDesktopAlert(i - 1 & " images imported sucessfully!")
        End If
        DisplayDatabaseInformation()

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub SelectChancePrints() Handles btnSelcetCPs.Click
        On Error GoTo errhandler
        Dim yr As String = Year(Me.SOCDatagrid.SelectedCells(2).Value.ToString)
        Dim soc As String = Strings.Format(Me.SOCDatagrid.SelectedCells(1).Value, "000")
        Dim Location As String = CPImageImportLocation & yr & "\SOC No. " & soc

        If FileIO.FileSystem.DirectoryExists(Location) Then
            If FileIO.FileSystem.GetFiles(Location).Count <> 0 Then
                Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A folder for the selected SOC No. already exists. Do you want to copy images to this folder?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If reply = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If

        OpenFileDialog1.Filter = "Chance Print Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.tif;*.gif"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select Chance Print Image Files"
        OpenFileDialog1.Multiselect = True
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If UCase(OpenFileDialog1.FileNames(0)).StartsWith(UCase(Location)) Then
                MessageBoxEx.Show("The source and destination are same. Cannot copy images.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim count = OpenFileDialog1.FileNames.Length

            If count = 1 Then
                My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileNames(0), Location & "/" & OpenFileDialog1.SafeFileNames(0), True)
                ShowDesktopAlert("1 image imported sucessfully!")
            End If

            If count > 1 Then
                FrmImportTransferRate.StartPosition = FormStartPosition.CenterScreen
                FrmImportTransferRate.Show()
                FrmImportTransferRate.ProgressBarX1.Maximum = count
                boolCancelImport = False
                Dim i As Integer
                For i = 0 To count - 1
                    If boolCancelImport Then Exit For
                    My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileNames(i), Location & "/" & OpenFileDialog1.SafeFileNames(i), True)
                    FrmImportTransferRate.ProgressBarX1.Text = "Imported image " & (i + 1) & " of " & count
                    FrmImportTransferRate.ProgressBarX1.Increment(1)
                    Application.DoEvents()
                Next
                FrmImportTransferRate.Close()
                ShowDesktopAlert(i & " images imported sucessfully!")
            End If
        End If
        DisplayDatabaseInformation()
        Exit Sub
errhandler:
        On Error Resume Next
        FrmImportTransferRate.Close()
        'MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub


    Private Sub ViewChancePrints() Handles btnViewCP.Click
        On Error Resume Next
        Dim yr As String = Year(Me.SOCDatagrid.SelectedCells(2).Value.ToString)
        Dim soc As String = Strings.Format(Me.SOCDatagrid.SelectedCells(1).Value, "000")
        Dim Location As String = CPImageImportLocation & yr & "\SOC No. " & soc
        FrmViewChancePrints.Close()
        FrmViewChancePrints.Show()
        FrmViewChancePrints.BringToFront()
        FrmViewChancePrints.lblSOCNumber.Text = "SOC No. " & Me.SOCDatagrid.SelectedCells(0).Value.ToString
        FrmViewChancePrints.LoadThumbNails(Location)

    End Sub
#End Region


#Region "SET PHOTOGRAPHER NAME"
    Private Sub LoadPhotographerName() Handles txtSOCPhotographer.GotFocus
        On Error Resume Next
        If Trim(txtSOCPhotographer.Text) <> "" Then Exit Sub
        If PhotographerFieldFocussed = False Then
            Me.txtSOCPhotographer.Text = strPhotographer
            PhotographerFieldFocussed = True
        End If

    End Sub

    Private Sub LoadPhotographedDate() Handles txtSOCDateOfPhotography.GotFocus
        On Error Resume Next
        If txtSOCCPsDeveloped.Value = 0 Or Me.txtSOCPhotographer.Text = vbNullString Or Me.txtSOCPhotographer.Text.ToLower = "no photographer" Then Exit Sub

        If PhotographedDateFocussed = False Then
            Me.txtSOCDateOfPhotography.Text = Me.dtSOCInspection.Text
            PhotographedDateFocussed = True
        End If


    End Sub
#End Region

#Region "SHOW GRAVE CRIME CASES"
    Private Sub ShowGraveCrimes() Handles btnShowGraveCrimes.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SOCRegisterTableAdapter.FillByGraveCrime(Me.FingerPrintDataSet.SOCRegister, True)
            Me.TabControl.SelectedTab = SOCTabItem
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region

    '-------------------------------------------IDENTIFICATION REGISTER-----------------------------------------


#Region "IDENTIFICATION REGISTER"

    Public Function NoOfCPsRemaining(SOCNumber As String) As String
        Return Me.SOCRegisterTableAdapter.ScalarQueryCPsRemainingInSOCNumber(SOCNumber)
    End Function


    Private Sub btnShowIdentifiedCases_Click(sender As Object, e As EventArgs) Handles btnShowIdentifiedCases.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SOCRegisterTableAdapter.FillByFileStatus(Me.FingerPrintDataSet.SOCRegister, "Identified")
            Me.TabControl.SelectedTab = SOCTabItem
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub SelectExpertOpinionImages() Handles btnSelectOpinionImages.Click
        Try
            Dim OpinionFolder As String = SuggestedLocation & "\Expert Opinion"
            System.IO.Directory.CreateDirectory(OpinionFolder)

            If Me.JoinedIDRDataGrid.RowCount = 0 Or Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                Call Shell("explorer.exe " & OpinionFolder, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            OpinionFolder = OpinionFolder & "\" & IDYear & "\ID No. " & IDNumber

            System.IO.Directory.CreateDirectory(OpinionFolder)

            OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.tif;*.gif"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select Chance Print and Specimen Image Files"
            OpenFileDialog1.Multiselect = True

            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If UCase(OpenFileDialog1.FileNames(0)).StartsWith(UCase(OpinionFolder)) Then
                    MessageBoxEx.Show("The source and destination folders are same. Cannot copy images.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim count = OpenFileDialog1.FileNames.Length

                If count = 1 Then
                    My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileNames(0), OpinionFolder & "/" & OpenFileDialog1.SafeFileNames(0), True)
                    ShowDesktopAlert("1 image imported sucessfully!")
                End If

                If count > 1 Then
                    FrmImportTransferRate.StartPosition = FormStartPosition.CenterScreen
                    FrmImportTransferRate.Show()
                    FrmImportTransferRate.ProgressBarX1.Maximum = count
                    boolCancelImport = False
                    Dim i As Integer
                    For i = 0 To count - 1
                        If boolCancelImport Then Exit For
                        My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileNames(i), OpinionFolder & "/" & OpenFileDialog1.SafeFileNames(i), True)
                        FrmImportTransferRate.ProgressBarX1.Text = "Imported image " & (i + 1) & " of " & count
                        FrmImportTransferRate.ProgressBarX1.Increment(1)
                        Application.DoEvents()
                    Next
                    FrmImportTransferRate.Close()
                    ShowDesktopAlert(i & " images imported sucessfully!")
                End If
            End If

        Catch ex As Exception
            FrmImportTransferRate.Close()
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region

    '-------------------------------------------RSOC DATA MANIPULATION-----------------------------------------

#Region "RSOC DATA ENTRY FIELDS SETTINGS"


    Private Sub LoadNatureOfReport()
        On Error Resume Next
        Me.cmbRSOCNatureOfReport.Items.Clear()
        Me.cmbRSOCNatureOfReport.Items.Add("Awaiting Photographs")
        Me.cmbRSOCNatureOfReport.Items.Add("Eliminated")
        Me.cmbRSOCNatureOfReport.Items.Add("Interim")
        Me.cmbRSOCNatureOfReport.Items.Add("Nil Print")
        Me.cmbRSOCNatureOfReport.Items.Add("No print remains")
        Me.cmbRSOCNatureOfReport.Items.Add("Preliminary")
        Me.cmbRSOCNatureOfReport.Items.Add("Unfit")
        Me.cmbRSOCNatureOfReport.Items.Add("Untraced")
    End Sub

    Private Sub InitializeRSOCFields()
        On Error Resume Next

        Me.txtRSOCNumber.Focus()
        Me.dtRSOCInspection.Text = vbNullString
        Dim ctrl As Control
        For Each ctrl In Me.PanelRSOC.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                ctrl.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearRSOCFields() Handles btnClearRSOC.Click 'Clear all textboxes, comboboxes etc
        On Error Resume Next

        Me.txtRSOCNumber.Focus() 'set focus to the first field StudentID
        Me.dtRSOCInspection.Text = vbNullString 'clear 
        Me.dtRSOCReportSentOn.Text = vbNullString
        Dim ctrl As Control
        For Each ctrl In Me.PanelRSOC.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                ctrl.Text = vbNullString
            End If
        Next

    End Sub

    Private Sub ClearSelectedRSOCFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRSOCNumber.ButtonCustomClick, txtRSOCCrimeNumber.ButtonCustomClick, txtRSOCDespatchNumber.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString

    End Sub

    Private Sub GenerateRSOCNumberWithoutYear(ByVal RSOCNumber As String)
        On Error Resume Next
        Dim s = Strings.Split(RSOCNumber, "/")
        Me.txtRSOCNumberOnly.Text = s(0)
    End Sub

    Private Sub ValidateRSOCNumber() Handles txtRSOCNumber.Validated
        On Error Resume Next
        GenerateRSOCNumberWithoutYear(Me.txtRSOCNumber.Text)
    End Sub

    Private Sub AppendRSOCYear() Handles txtRSOCNumber.Leave
        On Error Resume Next
        If Me.chkAppendSOCYear.Checked = False Then Exit Sub
        Dim y As String = Me.txtSOCYear.Text
        If y = vbNullString Then Exit Sub
        If Me.chkSOCTwodigits.Checked Then y = Strings.Right(y, 2)

        Dim n As String = Trim(Me.txtRSOCNumber.Text)
        Dim l As Short = Strings.Len(n)
        If n <> vbNullString And l < 11 And y <> vbNullString Then
            If Strings.InStr(n, "/", CompareMethod.Text) = 0 Then
                Me.txtRSOCNumber.Text = n & "/" & y
            End If
        End If


    End Sub

    Private Sub LoadDetailsToRSOCFromSOCNumber() Handles txtRSOCNumber.Validated
        On Error Resume Next
        '  If Me.chkIDLoadOtherDetails.Checked = False Then Exit Sub
        Dim n = Trim(Me.txtRSOCNumber.Text)
        If n = vbNullString Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Dim oldRow As FingerPrintDataSet.SOCRegisterRow
        oldRow = Me.FingerPrintDataSet.SOCRegister.FindBySOCNumber(n)

        If oldRow IsNot Nothing Then
            If RSOCEditMode Then
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default '
                Exit Sub
            End If


            With oldRow
                Me.dtRSOCInspection.ValueObject = .DateOfInspection
                Me.cmbRSOCPoliceStation.Text = .PoliceStation
                Me.txtRSOCCrimeNumber.Text = .CrimeNumber
                Me.cmbRSOCOfficer.Text = .InvestigatingOfficer
                Dim address As String = ""
                Me.SocReportRegisterTableAdapter1.FillBySOCNumber(Me.FingerPrintDataSet1.SOCReportRegister, n)
                If Me.FingerPrintDataSet1.SOCReportRegister.Count > 0 Then
                    address = Me.FingerPrintDataSet1.SOCReportRegister(Me.FingerPrintDataSet1.SOCReportRegister.Count - 1).ReportSentTo.ToString
                    FindRSOCNumber(n)
                Else
                    Dim ps = .PoliceStation
                    If Strings.Right(ps, 3) <> "P.S" Then
                        ps = ps & " P.S"
                    End If
                    address = "Sub Inspector of Police" & vbNewLine & ps
                End If

                Me.txtRSOCReportSentTo.Text = address
                Dim CPDeveloped As Integer = CInt(.ChancePrintsDeveloped)
                Dim CPUnfit As Integer = CInt(.ChancePrintsUnfit)
                Dim CPEliminated As Integer = CInt(.ChancePrintsEliminated)
                Dim CPIdentified As Integer = CInt(.CPsIdentified)
                Dim CPRemaining As Integer = CPDeveloped - CPUnfit - CPEliminated - CPIdentified


                If CPDeveloped = 0 Then 'nil print
                    Me.cmbRSOCNatureOfReport.Text = "Nil Print"
                End If


                If CPDeveloped > 0 And CPUnfit = CPDeveloped And CPRemaining = 0 Then 'all unfit
                    Me.cmbRSOCNatureOfReport.Text = "Unfit"
                End If

                If CPDeveloped > 0 And CPEliminated = CPDeveloped And CPRemaining = 0 Then 'all eliminated
                    Me.cmbRSOCNatureOfReport.Text = "Eliminated"
                End If

                If CPDeveloped > 0 And CPUnfit <> CPDeveloped And CPEliminated <> CPDeveloped And CPRemaining = 0 Then 'all eliminated or unfit

                    Me.cmbRSOCNatureOfReport.Text = "No print remains"
                End If

                If CPDeveloped > 0 And CPRemaining > 0 Then  'all remains
                    Dim DI As Date = CDate(Me.SOCDatagrid.SelectedCells(2).Value)
                    Dim d = Today.Subtract(DI).Days
                    If d <= 7 Then
                        Me.cmbRSOCNatureOfReport.Text = "Preliminary"
                    End If
                    If d > 7 And d <= 30 Then
                        Me.cmbRSOCNatureOfReport.Text = "Interim"

                    End If
                    If d > 30 Then
                        Me.cmbRSOCNatureOfReport.Text = "Untraced"
                    End If
                End If


            End With
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub FindRSOCNumber(ByVal socno As String) ' Handles dtSOCInspection.GotFocus
        On Error Resume Next
        Dim p = Me.RSOCRegisterBindingSource.Find("SOCNumber", Trim(socno))
        If p < 0 Then Exit Sub
        Me.RSOCRegisterBindingSource.Position = p
    End Sub


#End Region


#Region "RSOC MANDATORY FIELDS"


    Private Function MandatoryRSOCFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtRSOCNumber.Text) = vbNullString Or Me.txtRSOCSerialNumber.Text = vbNullString Or Me.dtRSOCInspection.IsEmpty Or Trim(Me.cmbRSOCPoliceStation.Text) = vbNullString Or Trim(Me.txtRSOCCrimeNumber.Text) = vbNullString Or Trim(Me.cmbRSOCOfficer.Text) = vbNullString Or Trim(Me.txtRSOCReportSentTo.Text) = vbNullString Or Me.dtRSOCReportSentOn.IsEmpty Or Trim(Me.cmbRSOCNatureOfReport.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub ShowMandatoryRSOCFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0

        If Me.txtRSOCSerialNumber.Text = vbNullString Then
            msg = msg & " Number" & vbNewLine
            x = 1
        End If

        If Trim(Me.txtRSOCNumber.Text) = vbNullString Then
            msg = msg & " SOC Number" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.dtRSOCInspection.Text) = vbNullString Then
            msg = msg & " Date of Inspection" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If
        If Trim(Me.cmbRSOCPoliceStation.Text) = vbNullString Then
            msg = msg & " Police Station" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
        End If
        If Trim(Me.txtRSOCCrimeNumber.Text) = vbNullString Then
            msg = msg & " Crime Number" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If
        If Trim(Me.cmbRSOCOfficer.Text) = vbNullString Then
            msg = msg & " Inspecting Officer" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If
        If Trim(Me.txtRSOCReportSentTo.Text) = vbNullString Then
            msg = msg & " Report Sent To" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 Then x = 7
        End If
        If Trim(Me.dtRSOCReportSentOn.Text) = vbNullString Then
            msg = msg & " Date of Sending Report" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 Then x = 8
        End If
        If Trim(Me.cmbRSOCNatureOfReport.Text) = vbNullString Then
            msg = msg & " Nature Of Report" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 And x <> 8 Then x = 9
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                txtRSOCSerialNumber.Focus()
            Case 2
                txtRSOCNumber.Focus()
            Case 3
                dtRSOCInspection.Focus()
            Case 4
                cmbRSOCPoliceStation.Focus()
            Case 5
                txtRSOCCrimeNumber.Focus()
            Case 6
                Me.cmbRSOCOfficer.Focus()

            Case 7
                Me.txtRSOCReportSentTo.Focus()
            Case 8
                Me.dtRSOCReportSentOn.Focus()
            Case 9
                Me.cmbRSOCNatureOfReport.Focus()
        End Select

    End Sub


#End Region


#Region "RSOC SAVE BUTTON ACTION"

    Private Sub RSOCSaveButtonAction() Handles btnSaveRSOC.Click
        On Error Resume Next
        If MandatoryRSOCFieldsNotFilled() Then
            ShowMandatoryRSOCFieldsInfo()
            Exit Sub
        End If
        If Me.dtRSOCReportSentOn.Value < Me.dtRSOCInspection.Value Then
            MessageBoxEx.Show("Date of Sending Report (" & Me.dtRSOCReportSentOn.Text & ") should be on or after the Date of Inspection (" & Me.dtRSOCInspection.Text & "). Please correct the error.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtRSOCReportSentOn.Focus()
            Exit Sub
        End If

        If RSOCEditMode Then
            UpdateRSOCData()
        Else
            SaveNewRSOCEntry()
        End If

    End Sub
#End Region


#Region "RSOC NEW DATA ENTRY"


    Private Sub SaveNewRSOCEntry()
        Try



            OriginalRSOCSerialNumber = Me.txtRSOCSerialNumber.Value
            GenerateRSOCNumberWithoutYear(Me.txtRSOCNumber.Text)
            Dim socno = Trim(Me.txtRSOCNumber.Text)
            Dim socnumwithoutyear = Me.txtRSOCNumberOnly.Text
            Dim ps = Trim(Me.cmbRSOCPoliceStation.Text)
            Dim cr = Trim(Me.txtRSOCCrimeNumber.Text)
            Dim officer = Trim(Me.cmbRSOCOfficer.Text)
            Dim reportto = Trim(Me.txtRSOCReportSentTo.Text)
            Dim nature = Trim(Me.cmbRSOCNatureOfReport.Text)
            Dim despatch = Trim(Me.txtRSOCDespatchNumber.Text)
            Dim remarks = Trim(Me.txtRSOCRemarks.Text)

            If RSOCSerialNumberExists(OriginalRSOCSerialNumber) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the selected Number " & OriginalRSOCSerialNumber & " already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.Yes Then
                    OverWriteRSOCData()
                Else
                    Me.txtRSOCSerialNumber.Focus()
                End If

                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.SOCReportRegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.SOCReportRegister.NewSOCReportRegisterRow()
            With newRow
                .SerialNo = OriginalRSOCSerialNumber
                .SOCNumber = socno
                .SOCNumberWithoutYear = socnumwithoutyear
                If Me.dtRSOCInspection.IsEmpty = False Then .DateOfInspection = Me.dtRSOCInspection.ValueObject
                .PoliceStation = ps
                .CrimeNumber = cr
                .InspectingOfficer = officer
                .ReportSentTo = reportto
                If Me.dtRSOCReportSentOn.IsEmpty = False Then .DateOfReportSent = Me.dtRSOCReportSentOn.ValueObject
                .NatureOfReports = nature
                .DespatchNumber = despatch
                .Remarks = remarks
            End With

            Me.FingerPrintDataSet.SOCReportRegister.Rows.Add(newRow) ' add the row to the table
            Me.RSOCRegisterBindingSource.Position = Me.RSOCRegisterBindingSource.Find("SerialNo", OriginalRSOCSerialNumber)

            Me.RSOCRegisterTableAdapter.Insert(OriginalRSOCSerialNumber, socno, socnumwithoutyear, Me.dtRSOCInspection.ValueObject, ps, cr, officer, reportto, Me.dtRSOCReportSentOn.ValueObject, nature, despatch, remarks)
            ShowDesktopAlert("New Record entered successfully!")
            InitializeRSOCFields()
            IncrementRSOCNumber(OriginalRSOCSerialNumber)

            InsertOrUpdateLastModificationDate(Now, nature & " report sent for SOC No. " & socno)
            DisplayDatabaseInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SaveSOCReportOnGeneratingReport()
        Try
            GenerateNewRSOCSerialNumber()
            OriginalRSOCSerialNumber = Me.txtRSOCSerialNumber.Value
            GenerateRSOCNumberWithoutYear(Me.txtRSOCNumber.Text)



            Dim socno = Me.SOCDatagrid.SelectedCells(0).Value
            Dim socnumwithoutyear = Me.SOCDatagrid.SelectedCells(1).Value
            Dim ps = Me.SOCDatagrid.SelectedCells(5).Value
            Dim cr = Me.SOCDatagrid.SelectedCells(6).Value
            Dim officer = Me.SOCDatagrid.SelectedCells(9).Value
            Dim reportto = ReportSentTo
            Dim nature = ReportNature
            Dim despatch = ""
            Dim remarks = ""


            Dim newRow As FingerPrintDataSet.SOCReportRegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.SOCReportRegister.NewSOCReportRegisterRow()
            With newRow
                .SerialNo = OriginalRSOCSerialNumber
                .SOCNumber = socno
                .SOCNumberWithoutYear = socnumwithoutyear
                .DateOfInspection = Me.SOCDatagrid.SelectedCells(2).Value
                .PoliceStation = ps
                .CrimeNumber = cr
                .InspectingOfficer = officer
                .ReportSentTo = reportto
                .DateOfReportSent = ReportSentDate
                .NatureOfReports = nature
                .DespatchNumber = despatch
                .Remarks = remarks
            End With

            Me.FingerPrintDataSet.SOCReportRegister.Rows.Add(newRow) ' add the row to the table
            Me.RSOCRegisterBindingSource.Position = Me.RSOCRegisterBindingSource.Find("SerialNo", OriginalRSOCSerialNumber)

            Me.RSOCRegisterTableAdapter.Insert(OriginalRSOCSerialNumber, socno, socnumwithoutyear, Me.SOCDatagrid.SelectedCells(2).Value, ps, cr, officer, reportto, ReportSentDate, nature, despatch, remarks)
            InitializeRSOCFields()
            IncrementRSOCNumber(OriginalRSOCSerialNumber)
            Me.lblReportSent.Visible = True
            Me.StatusBar.RecalcLayout()
            InsertOrUpdateLastModificationDate(Now, nature & " report sent for SOC No. " & socno)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function RSOCSerialNumberExists(ByVal sRSOCSerialNumber As Long)
        On Error Resume Next
        If Me.RSOCRegisterTableAdapter.CheckSerialNumberExists(sRSOCSerialNumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function




#End Region


#Region "RSOC EDIT RECORD"

    Private Sub UpdateRSOCData()

        Try
            Dim NewRSOCSerialNumber As Long = Me.txtRSOCSerialNumber.Value
            GenerateRSOCNumberWithoutYear(Me.txtRSOCNumber.Text)
            Dim socno = Trim(Me.txtRSOCNumber.Text)
            Dim socnumwithoutyear = Me.txtRSOCNumberOnly.Text
            Dim ps = Trim(Me.cmbRSOCPoliceStation.Text)
            Dim cr = Trim(Me.txtRSOCCrimeNumber.Text)
            Dim officer = Trim(Me.cmbRSOCOfficer.Text)
            Dim reportto = Trim(Me.txtRSOCReportSentTo.Text)
            Dim nature = Trim(Me.cmbRSOCNatureOfReport.Text)
            Dim despatch = Trim(Me.txtRSOCDespatchNumber.Text)
            Dim remarks = Trim(Me.txtRSOCRemarks.Text)

            If NewRSOCSerialNumber <> OriginalRSOCSerialNumber Then
                If RSOCSerialNumberExists(NewRSOCSerialNumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the Number " & NewRSOCSerialNumber & " already exists. Please enter a different Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtRSOCSerialNumber.Focus()
                    Exit Sub
                End If
            End If


            Dim oldRow As FingerPrintDataSet.SOCReportRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.SOCReportRegister.FindBySerialNo(OriginalRSOCSerialNumber)
            If oldRow IsNot Nothing Then

                With oldRow
                    .SerialNo = NewRSOCSerialNumber
                    .SOCNumber = socno
                    .SOCNumberWithoutYear = socnumwithoutyear
                    If Me.dtRSOCInspection.IsEmpty = False Then
                        .DateOfInspection = Me.dtRSOCInspection.ValueObject
                    Else
                        .DateOfInspection = Nothing
                    End If

                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .InspectingOfficer = officer
                    .ReportSentTo = reportto
                    If Me.dtRSOCReportSentOn.IsEmpty = False Then
                        .DateOfReportSent = Me.dtRSOCReportSentOn.ValueObject
                    Else
                        .DateOfReportSent = Nothing
                    End If

                    .NatureOfReports = nature
                    .DespatchNumber = despatch
                    .Remarks = remarks


                End With
            End If
            Me.RSOCRegisterBindingSource.Position = Me.RSOCRegisterBindingSource.Find("SerialNo", NewRSOCSerialNumber)

            Me.RSOCRegisterTableAdapter.UpdateQuery(NewRSOCSerialNumber, socno, socnumwithoutyear, Me.dtRSOCInspection.ValueObject, ps, cr, officer, reportto, Me.dtRSOCReportSentOn.ValueObject, nature, despatch, remarks, OriginalRSOCSerialNumber)

            ShowDesktopAlert("Selected Record updated successfully!")
            InitializeRSOCFields()
            GenerateNewRSOCSerialNumber()
            Me.dtRSOCInspection.Text = vbNullString
            Me.dtRSOCReportSentOn.Value = Today

            Me.btnSaveRSOC.Text = "Save"
            RSOCEditMode = False
            InsertOrUpdateLastModificationDate(Now, "Edited RSOC No. " & OriginalRSOCSerialNumber)
            DisplayDatabaseInformation()


        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.RSOCRegisterBindingSource.Position = Me.RSOCRegisterBindingSource.Find("SerialNo", OriginalRSOCSerialNumber)
        End Try


    End Sub
#End Region


#Region "OVERWRITE RSOC DATA"

    Private Sub OverWriteRSOCData()
        Try

            Dim NewRSOCSerialNumber As Long = Me.txtRSOCSerialNumber.Value
            GenerateRSOCNumberWithoutYear(Me.txtRSOCNumber.Text)
            Dim socno = Trim(Me.txtRSOCNumber.Text)
            Dim socnumwithoutyear = Me.txtRSOCNumberOnly.Text
            Dim ps = Trim(Me.cmbRSOCPoliceStation.Text)
            Dim cr = Trim(Me.txtRSOCCrimeNumber.Text)
            Dim officer = Trim(Me.cmbRSOCOfficer.Text)
            Dim reportto = Trim(Me.txtRSOCReportSentTo.Text)
            Dim nature = Trim(Me.cmbRSOCNatureOfReport.Text)
            Dim despatch = Trim(Me.txtRSOCDespatchNumber.Text)
            Dim remarks = Trim(Me.txtRSOCRemarks.Text)


            Dim oldRow As FingerPrintDataSet.SOCReportRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.SOCReportRegister.FindBySerialNo(OriginalRSOCSerialNumber)
            If oldRow IsNot Nothing Then

                With oldRow
                    .SerialNo = NewRSOCSerialNumber
                    .SOCNumber = socno
                    .SOCNumberWithoutYear = socnumwithoutyear
                    If Me.dtRSOCInspection.IsEmpty = False Then
                        .DateOfInspection = Me.dtRSOCInspection.ValueObject
                    Else
                        .DateOfInspection = Nothing
                    End If

                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .InspectingOfficer = officer
                    .ReportSentTo = reportto
                    If Me.dtRSOCReportSentOn.IsEmpty = False Then
                        .DateOfReportSent = Me.dtRSOCReportSentOn.ValueObject
                    Else
                        .DateOfReportSent = Nothing
                    End If

                    .NatureOfReports = nature
                    .DespatchNumber = despatch
                    .Remarks = remarks


                End With
            End If

            Me.RSOCRegisterBindingSource.Position = Me.RSOCRegisterBindingSource.Find("SerialNo", OriginalRSOCSerialNumber)

            Me.RSOCRegisterTableAdapter.UpdateQuery(OriginalRSOCSerialNumber, socno, socnumwithoutyear, Me.dtRSOCInspection.ValueObject, ps, cr, officer, reportto, Me.dtRSOCReportSentOn.ValueObject, nature, despatch, remarks, OriginalRSOCSerialNumber)

            ShowDesktopAlert("Selected Record over writed!")
            InitializeRSOCFields()
            GenerateNewRSOCSerialNumber()
            Me.dtRSOCInspection.Text = vbNullString
            Me.dtRSOCReportSentOn.Value = Today

            Me.btnSaveRSOC.Text = "Save"
            RSOCEditMode = False
            InsertOrUpdateLastModificationDate(Now, "Edited RSOC No. " & OriginalRSOCSerialNumber)
            DisplayDatabaseInformation()


        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.RSOCRegisterBindingSource.Position = Me.RSOCRegisterBindingSource.Find("SerialNo", OriginalRSOCSerialNumber)
        End Try

    End Sub


#End Region


#Region "SEARCH RSOC"
    Public Sub SearchWithRSOCSerialNumber() Handles btnRSOCFindByNumber.Click
        Try

            If Me.txtRSOCSerialNumber.Text = "" Then
                MessageBoxEx.Show("Please enter the Number to search for.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtRSOCSerialNumber.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.RSOCRegisterTableAdapter.FillBySerialNumber(Me.FingerPrintDataSet.SOCReportRegister, Me.txtRSOCSerialNumber.Text)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.RSOCDatagrid.RowCount < 2, Me.RSOCDatagrid.RowCount & " Record", Me.RSOCDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


    Public Sub SearchRSOC() Handles btnSearchRSOC.Click
        Try
            Me.Cursor = Cursors.WaitCursor


            Dim socno = Me.txtRSOCNumber.Text
            Dim ps = Me.cmbRSOCPoliceStation.Text
            Dim cr = Me.txtRSOCCrimeNumber.Text
            Dim officer = Me.cmbRSOCOfficer.Text
            Dim reportto = Me.txtRSOCReportSentTo.Text
            Dim nature = Me.cmbRSOCNatureOfReport.Text
            Dim despatch = Me.txtRSOCDespatchNumber.Text
            Dim remarks = Me.txtRSOCRemarks.Text

            If SearchSetting = 0 Then
                socno = socno & "%"
                ps = ps & "%"
                cr = cr & "%"
                officer = officer & "%"
                reportto = reportto & "%"
                nature = nature & "%"
                despatch = despatch & "%"
                remarks = remarks & "%"
            End If


            If SearchSetting = 1 Then
                If socno = vbNullString Then socno = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If officer = vbNullString Then officer = "%"
                If reportto = vbNullString Then reportto = "%"
                If nature = vbNullString Then nature = "%"
                If despatch = vbNullString Then despatch = "%"
                If remarks = vbNullString Then remarks = "%"
            End If

            If SearchSetting = 2 Then
                socno = "%" & socno & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                officer = "%" & officer & "%"
                reportto = "%" & reportto & "%"
                nature = "%" & nature & "%"
                despatch = "%" & despatch & "%"
                remarks = "%" & remarks & "%"
            End If

            If Me.dtRSOCInspection.IsEmpty And Me.dtRSOCReportSentOn.IsEmpty Then
                Me.RSOCRegisterTableAdapter.FillByNoDIDR(FingerPrintDataSet.SOCReportRegister, socno, ps, cr, officer, reportto, nature, despatch, remarks)
            End If

            If Me.dtRSOCInspection.IsEmpty = False And Me.dtRSOCReportSentOn.IsEmpty = False Then
                Me.RSOCRegisterTableAdapter.FillByDIDR(FingerPrintDataSet.SOCReportRegister, socno, Me.dtRSOCInspection.Value, ps, cr, officer, reportto, Me.dtRSOCReportSentOn.Value, nature, despatch, remarks)
            End If

            If Me.dtRSOCInspection.IsEmpty = True And Me.dtRSOCReportSentOn.IsEmpty = False Then
                Me.RSOCRegisterTableAdapter.FillByDR(FingerPrintDataSet.SOCReportRegister, socno, ps, cr, officer, reportto, Me.dtRSOCReportSentOn.Value, nature, despatch, remarks)
            End If

            If Me.dtRSOCInspection.IsEmpty = False And Me.dtRSOCReportSentOn.IsEmpty = True Then
                Me.RSOCRegisterTableAdapter.FillByDI(FingerPrintDataSet.SOCReportRegister, socno, Me.dtRSOCInspection.Value, ps, cr, officer, reportto, nature, despatch, remarks)
            End If

            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.RSOCDatagrid.RowCount < 2, Me.RSOCDatagrid.RowCount & " Record", Me.RSOCDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region
    '--------------------------------------------DA DATA MANIPULATION-----------------------------------------

#Region "DA DATA ENTRY FIELDS SETTINGS"


    Private Sub InitializeDAFields()
        On Error Resume Next
        Me.txtDANumber.Focus()
        ClearDAImage()
        Dim ctrl As Control
        For Each ctrl In Me.PanelDA.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                If ctrl.Name <> txtDAYear.Name And ctrl.Name <> cmbDAPoliceStation.Name And ctrl.Name <> cmbDASex.Name And ctrl.Name <> txtDASection.Name And ctrl.Name <> txtDACrimeNumber.Name Then ctrl.Text = vbNullString
                ' If InStr(Me.txtDASection.Text, "34") = 0 Then Me.txtDACrimeNumber.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearDAFields() Handles btnClearDAFields.Click 'Clear all textboxes, comboboxes etc
        On Error Resume Next

        Me.txtDANumber.Focus()
        Me.dtDAEntry.Text = vbNullString
        DASlipImageFile = vbNullString
        Me.picDASlip.ClearImage()

        Dim ctrl As Control
        For Each ctrl In Me.PanelDA.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                If ctrl.Name <> txtDAYear.Name Then ctrl.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearSelectedDAFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDANumber.ButtonCustomClick, txtDACrimeNumber.ButtonCustomClick, txtDASection.ButtonCustomClick, txtDAName.ButtonCustomClick, txtDAAliasName.ButtonCustomClick, txtDAFathersName.ButtonCustomClick, txtDAHenryNumerator.ButtonCustomClick, txtDAHenryDenominator.ButtonCustomClick, txtDAModusOperandi.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString


    End Sub

    Private Sub AppendDAYear() Handles txtDANumber.Leave, txtDACrimeNumber.Leave
        On Error Resume Next
        If Me.chkAppendDAYear.Checked = False Then Exit Sub
        Dim y As String = Me.txtDAYear.Text
        If y = vbNullString Then Exit Sub
        If Me.chkDATwodigits.Checked Then y = Strings.Right(y, 2)
        Dim n As String = Trim(Me.txtDANumber.Text)
        Dim c As String = Trim(Me.txtDACrimeNumber.Text)
        Dim l As Short = Strings.Len(n)
        If n <> vbNullString And l < 11 And y <> vbNullString Then
            If Strings.InStr(n, "/", CompareMethod.Text) = 0 Then
                Me.txtDANumber.Text = n & "/" & y
            End If
        End If
        l = Len(c)
        If c <> vbNullString And l < 46 And y <> vbNullString Then
            If Strings.InStr(c, "/", CompareMethod.Text) = 0 Then
                Me.txtDACrimeNumber.Text = c & "/" & y
            End If
        End If

    End Sub

    Private Sub ValidateDASex() Handles cmbDASex.Validated
        On Error Resume Next
        Dim sex As String = UCase(Trim(Me.cmbDASex.Text))
        If sex <> "MALE" And sex <> "FEMALE" And sex <> vbNullString Then
            If sex = "M" Then
                Me.cmbDASex.Text = "Male"
            ElseIf sex = "F" Then
                Me.cmbDASex.Text = "Female"
            Else
                DevComponents.DotNetBar.MessageBoxEx.Show("The sex you entered is invalid. Please select a sex from the list or leave it blank", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.cmbDASex.Focus()
            End If

        End If
    End Sub

    Private Sub GenerateDANumberWithoutYear(ByVal DANumber As String)
        On Error Resume Next
        Dim s = Strings.Split(DANumber, "/")
        Me.txtDANumberOnly.Text = s(0)
    End Sub

    Private Sub ValidateDANumber() Handles txtDANumber.Validated
        On Error Resume Next
        GenerateDANumberWithoutYear(Me.txtDANumber.Text)
    End Sub


    Private Sub FindDANumber() Handles dtDAEntry.GotFocus
        On Error Resume Next
        Dim p = Me.DARegisterBindingSource.Find("DANumber", Trim(Me.txtDANumber.Text))
        If p < 0 Then Exit Sub
        Me.DARegisterBindingSource.Position = p
    End Sub


    Private Sub CapitalizeDAHenryClassificationDenominator() Handles txtDAHenryDenominator.Validated
        On Error Resume Next


        Dim t As String = Me.txtDAHenryDenominator.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtDAHenryDenominator.Text = t

    End Sub

    Private Sub CapitalizeDAHenryClassificationNumerator() Handles txtDAHenryNumerator.Validated
        On Error Resume Next


        Dim t As String = Me.txtDAHenryNumerator.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtDAHenryNumerator.Text = t

    End Sub


#End Region


#Region "DA MANDATORY FIELDS"


    Function MandatoryDAFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtDANumber.Text) = vbNullString Or Me.dtDAEntry.IsEmpty Or Trim(Me.cmbDAPoliceStation.Text) = vbNullString Or Trim(Me.txtDACrimeNumber.Text) = vbNullString Or Trim(Me.txtDASection.Text) = vbNullString Or Trim(Me.txtDAName.Text) = vbNullString Or Trim(Me.txtDAHenryNumerator.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub ShowMandatoryDAFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Trim(Me.txtDANumber.Text) = vbNullString Then
            msg = msg & " DA Number" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Me.dtDAEntry.IsEmpty Then
            msg = msg & " Date of Entry" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If
        If Trim(Me.cmbDAPoliceStation.Text) = vbNullString Then
            msg = msg & " Police Station" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
        End If
        If Trim(Me.txtDACrimeNumber.Text) = vbNullString Then
            msg = msg & " Crime Number" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If
        If Trim(Me.txtDASection.Text) = vbNullString Then
            msg = msg & " Section of Law" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If
        If Trim(Me.txtDAName.Text) = vbNullString Then
            msg = msg & " Name" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 Then x = 7
        End If

        If Trim(Me.txtDAHenryNumerator.Text) = vbNullString Then
            msg = msg & " Henry Numerator" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 Then x = 8
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                txtDAYear.Focus()
            Case 2
                txtDANumber.Focus()
            Case 3
                dtDAEntry.Focus()
            Case 4
                cmbDAPoliceStation.Focus()
            Case 5
                txtDACrimeNumber.Focus()
            Case 6
                txtDASection.Focus()
            Case 7
                txtDAName.Focus()
            Case 8
                txtDAHenryNumerator.Focus()
        End Select

    End Sub


#End Region


#Region "DA SAVE BUTTON ACTION"

    Private Sub DASaveButtonAction() Handles btnSaveDA.Click
        On Error Resume Next
        CapitalizeDAHenryClassificationNumerator()
        CapitalizeDAHenryClassificationDenominator()
        AddTextsToAutoCompletionList()

        If DAEditMode Then
            UpdateDAData()
        Else
            SaveNewDAEntry()
        End If
    End Sub
#End Region


#Region "DA NEW DATA ENTRY"


    Private Sub SaveNewDAEntry()
        Try

            If MandatoryDAFieldsNotFilled() Then
                ShowMandatoryDAFieldsInfo()
                Exit Sub
            End If

            OriginalDANumber = Trim(Me.txtDANumber.Text)
            GenerateDANumberWithoutYear(OriginalDANumber)
            Dim sYear = Me.txtDANumberOnly.Text
            Dim ps = Trim(Me.cmbDAPoliceStation.Text)
            Dim cr = Trim(Me.txtDACrimeNumber.Text)
            Dim section = Trim(Me.txtDASection.Text)
            Dim name = Trim(Me.txtDAName.Text)
            Dim aliasname = Trim(Me.txtDAAliasName.Text)
            Dim father = Trim(Me.txtDAFathersName.Text)
            Dim sex = Trim(Me.cmbDASex.Text)
            Dim address = Trim(Me.txtDAAddress.Text)
            Dim hnum = Trim(Me.txtDAHenryNumerator.Text)
            Dim hden = Trim(Me.txtDAHenryDenominator.Text)
            Dim remarks = Trim(Me.txtDARemarks.Text)
            Dim modus = Trim(Me.txtDAModusOperandi.Text)

            If DANumberExists(OriginalDANumber) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the DA Number " & OriginalDANumber & " already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.Yes Then
                    OverWriteDAData()
                Else
                    Me.txtDANumber.Focus()
                    Me.txtDANumber.SelectAll()
                End If
                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.DARegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.DARegister.NewDARegisterRow()
            With newRow
                .DANumber = OriginalDANumber
                .DAYear = sYear
                .DateOfEntry = Me.dtDAEntry.ValueObject
                .PoliceStation = ps
                .CrimeNumber = cr
                .SectionOfLaw = section
                .Name = name
                .AliasName = aliasname
                .FathersName = father
                .Sex = sex
                .Address = address
                .HenryNumerator = hnum
                .HenryDenominator = hden
                .SlipFile = DASlipImageFile
                .Remarks = remarks
                .ModusOperandi = modus
            End With

            Me.FingerPrintDataSet.DARegister.Rows.Add(newRow) ' add the row to the table
            Me.DARegisterBindingSource.Position = Me.DARegisterBindingSource.Find("DANumber", OriginalDANumber)

            Me.DARegisterTableAdapter.Insert(OriginalDANumber, sYear, Me.dtDAEntry.ValueObject, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, DASlipImageFile, remarks, modus)
            ShowDesktopAlert("New DA Record entered successfully!")

            InitializeDAFields()
            IncrementDANumber(OriginalDANumber)
            InsertOrUpdateLastModificationDate(Now, "Added new DA No. " & OriginalDANumber)

            DisplayDatabaseInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function DANumberExists(ByVal DANumber As String)
        On Error Resume Next
        If Me.DARegisterTableAdapter.CheckDANumberExists(DANumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "OVERWRITE DA REGISTER"
    Private Sub OverWriteDAData()
        Try

            Dim NewDANumber As String = Trim(Me.txtDANumber.Text)
            GenerateDANumberWithoutYear(NewDANumber)
            Dim sYear = Me.txtDANumberOnly.Text
            Dim ps = Trim(Me.cmbDAPoliceStation.Text)
            Dim cr = Trim(Me.txtDACrimeNumber.Text)
            Dim section = Trim(Me.txtDASection.Text)
            Dim name = Trim(Me.txtDAName.Text)
            Dim aliasname = Trim(Me.txtDAAliasName.Text)
            Dim father = Trim(Me.txtDAFathersName.Text)
            Dim sex = Trim(Me.cmbDASex.Text)
            Dim address = Trim(Me.txtDAAddress.Text)
            Dim hnum = Trim(Me.txtDAHenryNumerator.Text)
            Dim hden = Trim(Me.txtDAHenryDenominator.Text)
            Dim remarks = Trim(Me.txtDARemarks.Text)
            Dim modus = Trim(Me.txtDAModusOperandi.Text)

            Dim oldRow As FingerPrintDataSet.DARegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(OriginalDANumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .DANumber = NewDANumber
                    .DAYear = sYear
                    .DateOfEntry = Me.dtDAEntry.ValueObject
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = section
                    .Name = name
                    .AliasName = aliasname
                    .FathersName = father
                    .Sex = sex
                    .Address = address
                    .HenryNumerator = hnum
                    .HenryDenominator = hden
                    .SlipFile = DASlipImageFile
                    .Remarks = remarks
                    .ModusOperandi = modus
                End With
            End If
            Me.DARegisterBindingSource.Position = Me.DARegisterBindingSource.Find("DANumber", NewDANumber)

            Me.DARegisterTableAdapter.UpdateQuery(NewDANumber, sYear, Me.dtDAEntry.ValueObject, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, DASlipImageFile, remarks, modus, OriginalDANumber)

            ShowDesktopAlert("DA Record over writed!")


            InitializeDAFields()
            ' IncrementDANumber(NewDANumber)
            GenerateNewDANumber()
            Me.dtDAEntry.Value = Today
            Me.btnSaveDA.Text = "Save"
            DAEditMode = False
            ClearDAImage()
            InsertOrUpdateLastModificationDate(Now, "Edited DA No. " & OriginalDANumber)
            DisplayDatabaseInformation()

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.DARegisterBindingSource.Position = Me.DARegisterBindingSource.Find("DANumber", OriginalDANumber)
        End Try

    End Sub
#End Region


#Region "DA EDIT RECORD"

    Private Sub UpdateDAData()

        Try

            If MandatoryDAFieldsNotFilled() Then
                ShowMandatoryDAFieldsInfo()
                Exit Sub
            End If

            Dim NewDANumber As String = Trim(Me.txtDANumber.Text)
            GenerateDANumberWithoutYear(NewDANumber)
            Dim sYear = Me.txtDANumberOnly.Text
            Dim ps = Trim(Me.cmbDAPoliceStation.Text)
            Dim cr = Trim(Me.txtDACrimeNumber.Text)
            Dim section = Trim(Me.txtDASection.Text)
            Dim name = Trim(Me.txtDAName.Text)
            Dim aliasname = Trim(Me.txtDAAliasName.Text)
            Dim father = Trim(Me.txtDAFathersName.Text)
            Dim sex = Trim(Me.cmbDASex.Text)
            Dim address = Trim(Me.txtDAAddress.Text)
            Dim hnum = Trim(Me.txtDAHenryNumerator.Text)
            Dim hden = Trim(Me.txtDAHenryDenominator.Text)
            Dim remarks = Trim(Me.txtDARemarks.Text)
            Dim modus = Trim(Me.txtDAModusOperandi.Text)

            If LCase(NewDANumber) <> LCase(OriginalDANumber) Then
                If DANumberExists(NewDANumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the DA Number " & NewDANumber & " already exists. Please enter a different DA Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtDANumber.Focus()
                    Me.txtDANumber.SelectAll()
                    Exit Sub
                End If
            End If


            Dim oldRow As FingerPrintDataSet.DARegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(OriginalDANumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .DANumber = NewDANumber
                    .DAYear = sYear
                    .DateOfEntry = Me.dtDAEntry.ValueObject
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = section
                    .Name = name
                    .AliasName = aliasname
                    .FathersName = father
                    .Sex = sex
                    .Address = address
                    .HenryNumerator = hnum
                    .HenryDenominator = hden
                    .SlipFile = DASlipImageFile
                    .Remarks = remarks
                    .ModusOperandi = modus
                End With
            End If
            Me.DARegisterBindingSource.Position = Me.DARegisterBindingSource.Find("DANumber", NewDANumber)

            Me.DARegisterTableAdapter.UpdateQuery(NewDANumber, sYear, Me.dtDAEntry.ValueObject, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, DASlipImageFile, remarks, modus, OriginalDANumber)

            ShowDesktopAlert("Selected DA Record updated successfully!")


            InitializeDAFields()
            ' IncrementDANumber(NewDANumber)
            GenerateNewDANumber()
            Me.dtDAEntry.Value = Today

            Me.btnSaveDA.Text = "Save"
            DAEditMode = False
            ClearDAImage()
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited DA No. " & OriginalDANumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.DARegisterBindingSource.Position = Me.DARegisterBindingSource.Find("DANumber", OriginalDANumber)
        End Try


    End Sub
#End Region


#Region "SEARCH DA RECORDS"

    Public Sub SearchWithDANumber() Handles btnDAFindByNumber.Click
        Try

            If Me.txtDANumber.Text = "" Then
                MessageBoxEx.Show("Please enter the DA Number to search for.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDANumber.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.DARegisterTableAdapter.FillByNumber(Me.FingerPrintDataSet.DARegister, Me.txtDANumber.Text)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.DADatagrid.RowCount < 2, Me.DADatagrid.RowCount & " Record", Me.DADatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


    Public Sub SearchDA() Handles btnSearchDA.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim sNumber = Me.txtDANumber.Text
            Dim ps = Trim(Me.cmbDAPoliceStation.Text)
            Dim cr = Trim(Me.txtDACrimeNumber.Text)
            Dim section = Trim(Me.txtDASection.Text)
            Dim name = Trim(Me.txtDAName.Text)
            Dim aliasname = Trim(Me.txtDAAliasName.Text)
            Dim father = Trim(Me.txtDAFathersName.Text)
            Dim sex = Trim(Me.cmbDASex.Text)
            Dim address = Trim(Me.txtDAAddress.Text)
            Dim HNUM = Trim(Me.txtDAHenryNumerator.Text)
            Dim HDEN = Trim(Me.txtDAHenryDenominator.Text)
            Dim remarks = Trim(Me.txtDARemarks.Text)
            Dim modus = Trim(Me.txtDAModusOperandi.Text)

            If HNUM = vbNullString Then
                HNUM = "HenryNumerator Like '%'"
            Else
                If HNUM.Contains("%") Or HNUM.Contains("_") Then
                    HNUM = "HenryNumerator Like '" & HNUM & "'"
                Else
                    HNUM = "instr(1, HenryNumerator, '" & HNUM & "', 0)>0 "
                End If
            End If

            If HDEN = vbNullString Then
                HDEN = "HenryDenominator Like '%'"
            Else
                If HDEN.Contains("%") Or HDEN.Contains("_") Then
                    HDEN = "HenryDenominator Like '" & HDEN & "'"
                Else
                    HDEN = "instr(1, HenryDenominator, '" & HDEN & "', 0)>0 "
                End If
            End If

            If SearchSetting = 0 Then 'begins with
                sNumber += "%"
                ps += "%"
                cr += "%"
                section += "%"
                name += "%"
                aliasname += "%"
                father += "%"
                sex += "%"
                address += "%"
                remarks += "%"
                modus += "%"
            End If


            If SearchSetting = 1 Then
                If sNumber = vbNullString Then sNumber = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If section = vbNullString Then section = "%"
                If name = vbNullString Then name = "%"
                If aliasname = vbNullString Then aliasname = "%"
                If father = vbNullString Then father = "%"
                If sex = vbNullString Then sex = "%"
                If address = vbNullString Then address = "%"
                If remarks = vbNullString Then remarks = "%"
                If modus = vbNullString Then modus = "%"
            End If

            If SearchSetting = 2 Then
                sNumber = "%" & sNumber & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                section = "%" & section & "%"
                name = "%" & name & "%"
                aliasname = "%" & aliasname & "%"
                father = "%" & father & "%"
                sex = "%" & sex & "%"
                address = "%" & address & "%"
                remarks = "%" & remarks & "%"
                modus = "%" & modus & "%"
            End If

            Dim SQLText As String = "Select * from DARegister"


            If Me.dtDAEntry.IsEmpty Then

                ' Me.DARegisterTableAdapter.FillByNoDE(FingerPrintDataSet.DARegister, sNumber, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, remarks, modus)

                SQLText = "Select * from DARegister where DANumber LIKE '" & sNumber & "' AND PoliceStation LIKE '" & ps & "' AND CrimeNumber LIKE '" & cr & "' AND SectionOfLaw LIKE '" & section & "' AND Name LIKE '" & name & "' AND AliasName LIKE '" & aliasname & "' AND FathersName LIKE '" & father & "' AND Sex LIKE '" & sex & "' AND Address LIKE '" & address & "' AND " & HNUM & " AND " & HDEN & " AND ModusOperandi LIKE '" & modus & "' AND Remarks LIKE '" & remarks & "' ORDER BY DateOfEntry, DAYear"
            End If

            If Me.dtDAEntry.IsEmpty = False Then
                '  Me.DARegisterTableAdapter.FillByDE(FingerPrintDataSet.DARegister, sNumber, dtDAEntry.ValueObject, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, remarks, modus)
                SQLText = "Select * from DARegister where DANumber LIKE '" & sNumber & "' AND PoliceStation LIKE '" & ps & "' AND CrimeNumber LIKE '" & cr & "' AND SectionOfLaw LIKE '" & section & "' AND Name LIKE '" & name & "' AND AliasName LIKE '" & aliasname & "' AND FathersName LIKE '" & father & "' AND Sex LIKE '" & sex & "' AND Address LIKE '" & address & "' AND " & HNUM & " AND " & HDEN & " AND ModusOperandi LIKE '" & modus & "' AND Remarks LIKE '" & remarks & "' AND DateOfEntry = #" & Me.dtDAEntry.Value & "# ORDER BY DateOfEntry, DAYear"
            End If
            SQLText = SQLText.Replace("%%", "%")
            SQLText = SQLText.Replace("##", "##")
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Me.FingerPrintDataSet.DARegister.Clear()
            da.Fill(Me.FingerPrintDataSet.DARegister)
            con.Close()

            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.DADatagrid.RowCount < 2, Me.DADatagrid.RowCount & " Record", Me.DADatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


    Public Sub SearchDAInSelectedYear() Handles btnSearchDAInYear.Click

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim y As String = Me.txtDAYear.Text
            If y = vbNullString Then
                MessageBoxEx.Show("Please enter the year in the year field", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtDAYear.Focus()
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim d1 As Date = New Date(y, 1, 1)
            Dim d2 As Date = New Date(y, 12, 31)

            Dim sNumber = Me.txtDANumber.Text
            Dim ps = Trim(Me.cmbDAPoliceStation.Text)
            Dim cr = Trim(Me.txtDACrimeNumber.Text)
            Dim section = Trim(Me.txtDASection.Text)
            Dim name = Trim(Me.txtDAName.Text)
            Dim aliasname = Trim(Me.txtDAAliasName.Text)
            Dim father = Trim(Me.txtDAFathersName.Text)
            Dim sex = Trim(Me.cmbDASex.Text)
            Dim address = Trim(Me.txtDAAddress.Text)
            Dim HNUM = Trim(Me.txtDAHenryNumerator.Text)
            Dim HDEN = Trim(Me.txtDAHenryDenominator.Text)
            Dim remarks = Trim(Me.txtDARemarks.Text)
            Dim modus = Trim(Me.txtDAModusOperandi.Text)

            If HNUM = vbNullString Then
                HNUM = "HenryNumerator Like '%'"
            Else
                If HNUM.Contains("%") Or HNUM.Contains("_") Then
                    HNUM = "HenryNumerator Like '" & HNUM & "'"
                Else
                    HNUM = "instr(1, HenryNumerator, '" & HNUM & "', 0)>0 "
                End If
            End If

            If HDEN = vbNullString Then
                HDEN = "HenryDenominator Like '%'"
            Else
                If HDEN.Contains("%") Or HDEN.Contains("_") Then
                    HDEN = "HenryDenominator Like '" & HDEN & "'"
                Else
                    HDEN = "instr(1, HenryDenominator, '" & HDEN & "', 0)>0 "
                End If
            End If

            If SearchSetting = 0 Then 'begins with
                sNumber += "%"
                ps += "%"
                cr += "%"
                section += "%"
                name += "%"
                aliasname += "%"
                father += "%"
                sex += "%"
                address += "%"
                remarks += "%"
                modus += "%"
            End If


            If SearchSetting = 1 Then
                If sNumber = vbNullString Then sNumber = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If section = vbNullString Then section = "%"
                If name = vbNullString Then name = "%"
                If aliasname = vbNullString Then aliasname = "%"
                If father = vbNullString Then father = "%"
                If sex = vbNullString Then sex = "%"
                If address = vbNullString Then address = "%"
                If remarks = vbNullString Then remarks = "%"
                If modus = vbNullString Then modus = "%"
            End If

            If SearchSetting = 2 Then
                sNumber = "%" & sNumber & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                section = "%" & section & "%"
                name = "%" & name & "%"
                aliasname = "%" & aliasname & "%"
                father = "%" & father & "%"
                sex = "%" & sex & "%"
                address = "%" & address & "%"
                remarks = "%" & remarks & "%"
                modus = "%" & modus & "%"
            End If

            Dim SQLText As String = "Select * from DARegister"


            If Me.dtDAEntry.IsEmpty Then

                SQLText = "Select * from DARegister where DANumber LIKE '" & sNumber & "' AND PoliceStation LIKE '" & ps & "' AND CrimeNumber LIKE '" & cr & "' AND SectionOfLaw LIKE '" & section & "' AND Name LIKE '" & name & "' AND AliasName LIKE '" & aliasname & "' AND FathersName LIKE '" & father & "' AND Sex LIKE '" & sex & "' AND Address LIKE '" & address & "' AND " & HNUM & " AND " & HDEN & " AND ModusOperandi LIKE '" & modus & "' AND Remarks LIKE '" & remarks & "' AND DateOfEntry between #" & d1 & "# AND #" & d2 & "#"
            End If

            If Me.dtDAEntry.IsEmpty = False Then

                SQLText = "Select * from DARegister where DANumber LIKE '" & sNumber & "' AND PoliceStation LIKE '" & ps & "' AND CrimeNumber LIKE '" & cr & "' AND SectionOfLaw LIKE '" & section & "' AND Name LIKE '" & name & "' AND AliasName LIKE '" & aliasname & "' AND FathersName LIKE '" & father & "' AND Sex LIKE '" & sex & "' AND Address LIKE '" & address & "' AND " & HNUM & " AND " & HDEN & " AND ModusOperandi LIKE '" & modus & "' AND Remarks LIKE '" & remarks & "' AND DateOfEntry = #" & Me.dtDAEntry.Value & "#" & " AND DateOfEntry between #" & d1 & "# AND #" & d2 & "#"
            End If

            SQLText = SQLText.Replace("%%", "%")
            SQLText = SQLText.Replace("##", "##")


            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Me.FingerPrintDataSet.DARegister.Clear()
            da.Fill(Me.FingerPrintDataSet.DARegister)
            con.Close()


            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.DADatagrid.RowCount < 2, Me.DADatagrid.RowCount & " Record", Me.DADatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "DA SLIP IMAGE FILE SETTINGS"


    Public Sub SelectDASlipImage() Handles btnDASelectFPSlip.Click, btnDASelectDisplayContext.Click 'select a photo from system
        Try

            OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select DA Slip Image File"
            OpenFileDialog1.AutoUpgradeEnabled = True
            OpenFileDialog1.RestoreDirectory = True 'remember last directory
            Dim SelectedFile As String
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
                Application.DoEvents() 'first close the selection window
                SelectedFile = OpenFileDialog1.FileName

                Dim getInfo As System.IO.DriveInfo = My.Computer.FileSystem.GetDriveInfo(SelectedFile)
                If getInfo.DriveType <> IO.DriveType.Fixed Then

                    Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The DA Slip Image File you selected is on a removable media. Do you want to copy it to the DA Slip Image Files Location?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If r = Windows.Forms.DialogResult.Yes Then
                        Dim year As String = Me.txtDAYear.Text
                        If Strings.Right(DASlipImageImportLocation, 1) <> "\" Then DASlipImageImportLocation = DASlipImageImportLocation & "\"
                        Dim DestinationFile As String = DASlipImageImportLocation & year & "\" & OpenFileDialog1.SafeFileName
                        My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                        SelectedFile = DestinationFile
                    End If

                    ' If reply=vbNo then do nothing just use the selected file

                    If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                End If

                DASlipImageFile = SelectedFile
                Me.picDASlip.Image = New Bitmap(SelectedFile) 'display the pic
                DisplayDatabaseInformation()
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ScanDASlip() Handles btnDAScanFPSlip.Click, btnDAScanDisplayContext.Click 'import photos from camera and scanner
        On Error Resume Next

        If Trim(Me.txtDANumber.Text) = vbNullString Then

            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the DA Number which is used as the scanned image's File Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtDANumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim FileName As String = "DANo." & Trim(Me.txtDANumber.Text) '

        FileName = Strings.Replace(FileName, "/", "-")
        Dim year As String = Me.txtDAYear.Text
        If Strings.Right(DASlipImageImportLocation, 1) <> "\" Then DASlipImageImportLocation = DASlipImageImportLocation & "\"
        Dim SaveLocation As String = DASlipImageImportLocation & year & "\"
        Dim ScannedImage As String = ImportImageFromScannerOrCamera(SaveLocation, FileName) 'scans the picture and returns the file name with path

        If ScannedImage <> vbNullString Then
            DASlipImageFile = ScannedImage
            Me.picDASlip.Image = New Bitmap(ScannedImage)
        End If
        DisplayDatabaseInformation()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub



    Private Sub ExploreDASlip() Handles btnDAExploreDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If DASlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(DASlipImageFile) Then
                '  Dim l As String = FileIO.FileSystem.GetParentPath(DASlipImageFile) & "\"
                ' Call Shell("explorer.exe /select," & l, AppWinStyle.NormalFocus)
                Call Shell("explorer.exe /select," & DASlipImageFile, AppWinStyle.NormalFocus)
            Else
                MessageBoxEx.Show("The specified DA Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub ClearDAImage()
        On Error Resume Next
        Me.picDASlip.ClearImage()
        DASlipImageFile = vbNullString
    End Sub

    Private Sub ClearDAImageWithMessage() Handles btnDAClearFPSlip.Click, btnDAClearDisplayContext.Click
        On Error Resume Next
        ClearDAImage()
        ShowDesktopAlert("Image cleared!")
    End Sub



    Private Sub ViewImageOnDADatagridCellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DADatagrid.CellDoubleClick
        On Error Resume Next
        If e.RowIndex < 0 Or e.RowIndex > Me.DADatagrid.Rows.Count - 1 Then
            Exit Sub
        End If
        ViewDASlipImage()
    End Sub


    Private Sub ViewDASlipImage() Handles btnViewDASlip.Click, btnViewDASlipContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If Me.DADatagrid.RowCount = 0 Then
            ShowDesktopAlert("No data in the list to show image!")
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim imagefile = Me.DADatagrid.SelectedCells(15).Value.ToString
        If FileIO.FileSystem.FileExists(imagefile) = False Then

            Dim FileName As String = "DANo." & Me.DADatagrid.SelectedCells(0).Value.ToString & ".jpeg"

            FileName = Strings.Replace(FileName, "/", "-")
            Dim yr As String = Year(Me.DADatagrid.SelectedCells(2).Value)
            If Strings.Right(DASlipImageImportLocation, 1) <> "\" Then DASlipImageImportLocation = DASlipImageImportLocation & "\"
            imagefile = DASlipImageImportLocation & yr & "\" & FileName

            If FileIO.FileSystem.FileExists(imagefile) = False Then
                MessageBoxEx.Show("No image file is associated with the selected DA Number!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If
        If FileIO.FileSystem.FileExists(imagefile) Then
            frmDASlipImageDisplayer.Show()
            frmDASlipImageDisplayer.WindowState = FormWindowState.Maximized
            frmDASlipImageDisplayer.BringToFront()



            Dim idno As String = Me.DADatagrid.CurrentRow.Cells(0).Value.ToString()
            Dim name As String = Me.DADatagrid.CurrentRow.Cells(6).Value.ToString()
            Dim aliasname As String = Me.DADatagrid.CurrentRow.Cells(7).Value.ToString()

            frmDASlipImageDisplayer.LoadDASlipPicture(imagefile, idno & "  -   " & name & IIf(aliasname <> vbNullString, " @ " & aliasname, ""))

        Else
            MessageBoxEx.Show("The specified DA Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Sub ViewImageOnDADisplayDblClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDASlip.MouseDoubleClick
        On Error Resume Next
        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        If DASlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(DASlipImageFile) Then
                frmDASlipImageDisplayer.Show()
                frmDASlipImageDisplayer.WindowState = FormWindowState.Maximized
                frmDASlipImageDisplayer.BringToFront()
                frmDASlipImageDisplayer.LoadPictureFromViewer(DASlipImageFile, Me.txtDANumber.Text)
            Else
                MessageBoxEx.Show("The specified DA Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Sub ViewDAImage() Handles btnDAViewDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If DASlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(DASlipImageFile) Then
                frmDASlipImageDisplayer.Show()
                frmDASlipImageDisplayer.WindowState = FormWindowState.Maximized
                frmDASlipImageDisplayer.BringToFront()
                frmDASlipImageDisplayer.LoadPictureFromViewer(DASlipImageFile, Me.txtDANumber.Text)
            Else
                MessageBoxEx.Show("The specified DA Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub DASlipContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles DASlipContextMenuBar.PopupOpen
        On Error Resume Next
        If DASlipImageFile = vbNullString Then
            Me.btnDAViewDisplayContext.Enabled = False
            Me.btnDAExploreDisplayContext.Enabled = False
        Else
            Me.btnDAViewDisplayContext.Enabled = True
            Me.btnDAExploreDisplayContext.Enabled = True
        End If
    End Sub

#End Region


    '---------------------------------------------ID DATA MANIPULATION-----------------------------------------
#Region "ID DATA ENTRY FIELDS SETTINGS"


    Private Sub InitializeIDFields()
        On Error Resume Next
        Me.txtIDNumber.Focus()
        IDSlipImageFile = vbNullString
        Me.picIDSlip.ClearImage()

        Dim ctrl As Control
        For Each ctrl In Me.PanelID.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                If ctrl.Name <> cmbIDPoliceStation.Name And ctrl.Name <> cmbIDSex.Name Then ctrl.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearIDFields() Handles btnClearIDFields.Click 'Clear all textboxes, comboboxes etc
        On Error Resume Next

        Me.txtIDNumber.Focus()
        IDSlipImageFile = vbNullString
        Me.picIDSlip.ClearImage()

        Dim ctrl As Control
        For Each ctrl In Me.PanelID.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                ctrl.Text = vbNullString
            End If
        Next

    End Sub

    Private Sub ClearSelectedIDFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIDNumber.ButtonCustomClick, txtIDDANumber.ButtonCustomClick, txtIDCrimeNumber.ButtonCustomClick, txtIDSection.ButtonCustomClick, txtIDName.ButtonCustomClick, txtIDAliasName.ButtonCustomClick, txtIDFathersName.ButtonCustomClick, txtIDHenryNumerator.ButtonCustomClick, txtIDHenryDenominator.ButtonCustomClick, txtIDModusOperandi.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString
    End Sub



    Private Sub ValidateIDSex() Handles cmbIDSex.Validated
        On Error Resume Next
        Dim sex As String = UCase(Trim(Me.cmbIDSex.Text))
        If sex <> "MALE" And sex <> "FEMALE" And sex <> vbNullString Then
            If sex = "M" Then
                Me.cmbIDSex.Text = "Male"
            ElseIf sex = "F" Then
                Me.cmbIDSex.Text = "Female"
            Else
                DevComponents.DotNetBar.MessageBoxEx.Show("The sex you entered is invalid. Please select a sex from the list or leave it blank", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.cmbIDSex.Focus()
            End If

        End If
    End Sub



    Private Sub LoadDetailsToIDFromDANumber() Handles txtIDDANumber.Validated
        On Error Resume Next
        If Me.chkIDLoadOtherDetails.Checked = False Then Exit Sub
        Dim n = Trim(Me.txtIDDANumber.Text)
        If n = vbNullString Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Dim oldRow As FingerPrintDataSet.DARegisterRow
        oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(n)

        If oldRow IsNot Nothing Then
            If IDEditMode Then
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default '
                Exit Sub
            End If


            With oldRow
                Me.cmbIDPoliceStation.Text = .PoliceStation
                Me.txtIDCrimeNumber.Text = .CrimeNumber
                Me.txtIDSection.Text = .SectionOfLaw
                Me.txtIDName.Text = .Name
                Me.txtIDAliasName.Text = .AliasName
                Me.txtIDFathersName.Text = .FathersName
                Me.txtIDAddress.Text = .Address
                Me.cmbIDSex.Text = .Sex
                Me.txtIDHenryDenominator.Text = .HenryDenominator
                Me.txtIDHenryNumerator.Text = .HenryNumerator
                Me.txtIDRemarks.Text = .Remarks
                Me.txtIDModusOperandi.Text = .ModusOperandi
                IDSlipImageFile = .SlipFile
                If FileIO.FileSystem.FileExists(IDSlipImageFile) = True Then
                    Me.picIDSlip.Image = New Bitmap(IDSlipImageFile)
                Else
                    ClearIDImage()
                End If
            End With
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub FindIDNumber() Handles txtIDDANumber.GotFocus
        On Error Resume Next
        Dim p = Me.IDRegisterBindingSource.Find("IDNumber", Trim(Me.txtIDNumber.Text))
        If p < 0 Then Exit Sub
        Me.IDRegisterBindingSource.Position = p
    End Sub


    Private Sub CapitalizeIDHenryClassificationDenominator() Handles txtIDHenryDenominator.Validated
        On Error Resume Next


        Dim t As String = Me.txtIDHenryDenominator.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtIDHenryDenominator.Text = t

    End Sub

    Private Sub CapitalizeIDHenryClassificationNumerator() Handles txtIDHenryNumerator.Validated
        On Error Resume Next


        Dim t As String = Me.txtIDHenryNumerator.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtIDHenryNumerator.Text = t

    End Sub
#End Region


#Region "ID MANDATORY FIELDS"


    Function MandatoryIDFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtIDNumber.Text) = vbNullString Or Trim(Me.txtIDName.Text) = vbNullString Or Trim(Me.txtIDHenryNumerator.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub ShowMandatoryIDFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Trim(Me.txtIDNumber.Text) = vbNullString Then
            msg = msg & " Number" & vbNewLine
            x = 1
        End If

        If Trim(Me.txtIDName.Text) = vbNullString Then
            msg = msg & " Name" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.txtIDHenryNumerator.Text) = vbNullString Then
            msg = msg & " Henry Numerator" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                Me.txtIDNumber.Focus()
            Case 2
                Me.txtIDName.Focus()
            Case 3
                Me.txtIDHenryNumerator.Focus()
        End Select

    End Sub


#End Region


#Region "ID SAVE BUTTON ACTION"

    Private Sub IDSaveButtonAction() Handles btnSaveID.Click
        On Error Resume Next
        CapitalizeIDHenryClassificationNumerator()
        CapitalizeIDHenryClassificationDenominator()
        AddTextsToAutoCompletionList()

        If IDEditMode Then
            UpdateIDData()
        Else
            SaveNewIDEntry()
        End If
    End Sub
#End Region


#Region "ID NEW IDTA ENTRY"


    Private Sub SaveNewIDEntry()
        Try


            If MandatoryIDFieldsNotFilled() Then
                ShowMandatoryIDFieldsInfo()
                Exit Sub
            End If

            OriginalIDNumber = Me.txtIDNumber.Value
            Dim da = Trim(Me.txtIDDANumber.Text)
            Dim ps = Trim(Me.cmbIDPoliceStation.Text)
            Dim cr = Trim(Me.txtIDCrimeNumber.Text)
            Dim section = Trim(Me.txtIDSection.Text)
            Dim name = Trim(Me.txtIDName.Text)
            Dim aliasname = Trim(Me.txtIDAliasName.Text)
            Dim father = Trim(Me.txtIDFathersName.Text)
            Dim sex = Trim(Me.cmbIDSex.Text)
            Dim address = Trim(Me.txtIDAddress.Text)
            Dim hnum = Trim(Me.txtIDHenryNumerator.Text)
            Dim hden = Trim(Me.txtIDHenryDenominator.Text)
            Dim iddetails = Trim(Me.txtIDDetails.Text)
            Dim remarks = Trim(Me.txtIDRemarks.Text)
            Dim modus = Trim(Me.txtIDModusOperandi.Text)

            If IDNumberExists(OriginalIDNumber) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the Number " & OriginalIDNumber & " already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.Yes Then
                    OverWriteIDData()
                Else
                    Me.txtIDNumber.Focus()
                End If
                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.IdentifiedSlipsRegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.IdentifiedSlipsRegister.NewIdentifiedSlipsRegisterRow()
            With newRow
                .IDNumber = OriginalIDNumber
                .DANumber = da
                .PoliceStation = ps
                .CrimeNumber = cr
                .SectionOfLaw = section
                .Name = name
                .AliasName = aliasname
                .FathersName = father
                .Sex = sex
                .Address = address
                .HenryNumerator = hnum
                .HenryDenominator = hden
                .IdentificationDetails = iddetails
                .SlipFile = IDSlipImageFile
                .Remarks = remarks
                .ModusOperandi = modus
            End With

            Me.FingerPrintDataSet.IdentifiedSlipsRegister.Rows.Add(newRow) ' add the row to the table
            Me.IDRegisterBindingSource.Position = Me.IDRegisterBindingSource.Find("IDNumber", OriginalIDNumber)


            Me.IDRegisterTableAdapter.Insert(OriginalIDNumber, da, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, iddetails, IDSlipImageFile, remarks, modus)
            ShowDesktopAlert("New Identified Slips Record entered successfully!")

            InitializeIDFields()
            IncrementIDNumber(OriginalIDNumber)
            InsertOrUpdateLastModificationDate(Now, "Added new Identified Slips Record No. " & OriginalIDNumber)

            DisplayDatabaseInformation()


        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Function IDNumberExists(ByVal IDNumber As Long)
        On Error Resume Next
        If Me.IDRegisterTableAdapter.CheckIDNumberExists(IDNumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "OVERWRITE ID REGISTER"
    Private Sub OverWriteIDData()
        Try


            Dim NewIDNumber As Long = Me.txtIDNumber.Value
            Dim da = Trim(Me.txtIDDANumber.Text)
            Dim ps = Trim(Me.cmbIDPoliceStation.Text)
            Dim cr = Trim(Me.txtIDCrimeNumber.Text)
            Dim section = Trim(Me.txtIDSection.Text)
            Dim name = Trim(Me.txtIDName.Text)
            Dim aliasname = Trim(Me.txtIDAliasName.Text)
            Dim father = Trim(Me.txtIDFathersName.Text)
            Dim sex = Trim(Me.cmbIDSex.Text)
            Dim address = Trim(Me.txtIDAddress.Text)
            Dim hnum = Trim(Me.txtIDHenryNumerator.Text)
            Dim hden = Trim(Me.txtIDHenryDenominator.Text)
            Dim iddetails = Trim(Me.txtIDDetails.Text)
            Dim remarks = Trim(Me.txtIDRemarks.Text)
            Dim modus = Trim(Me.txtIDModusOperandi.Text)


            Dim oldRow As FingerPrintDataSet.IdentifiedSlipsRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.IdentifiedSlipsRegister.FindByIDNumber(OriginalIDNumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .IDNumber = NewIDNumber
                    .DANumber = da
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = section
                    .Name = name
                    .AliasName = aliasname
                    .FathersName = father
                    .Sex = sex
                    .Address = address
                    .HenryNumerator = hnum
                    .HenryDenominator = hden
                    .IdentificationDetails = iddetails
                    .SlipFile = IDSlipImageFile
                    .Remarks = remarks
                    .ModusOperandi = modus
                End With
            End If
            Me.IDRegisterBindingSource.Position = Me.IDRegisterBindingSource.Find("IDNumber", NewIDNumber)

            Me.IDRegisterTableAdapter.UpdateQuery(NewIDNumber, da, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, iddetails, IDSlipImageFile, remarks, modus, OriginalIDNumber)


            ShowDesktopAlert("Identified Slips Record over writed!")

            InitializeIDFields()
            ' IncrementIDNumber(NewIDNumber)
            GenerateNewIDNumber()
            Me.btnSaveID.Text = "Save"
            IDEditMode = False
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Identified Slips Record No. " & OriginalIDNumber)
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.IDRegisterBindingSource.Position = Me.IDRegisterBindingSource.Find("IDNumber", OriginalIDNumber)
        End Try

    End Sub
#End Region


#Region "ID EDIT RECORD"

    Private Sub UpdateIDData()

        Try

            If MandatoryIDFieldsNotFilled() Then
                ShowMandatoryIDFieldsInfo()
                Exit Sub
            End If

            Dim NewIDNumber As Long = Me.txtIDNumber.Value
            Dim da = Trim(Me.txtIDDANumber.Text)
            Dim ps = Trim(Me.cmbIDPoliceStation.Text)
            Dim cr = Trim(Me.txtIDCrimeNumber.Text)
            Dim section = Trim(Me.txtIDSection.Text)
            Dim name = Trim(Me.txtIDName.Text)
            Dim aliasname = Trim(Me.txtIDAliasName.Text)
            Dim father = Trim(Me.txtIDFathersName.Text)
            Dim sex = Trim(Me.cmbIDSex.Text)
            Dim address = Trim(Me.txtIDAddress.Text)
            Dim hnum = Trim(Me.txtIDHenryNumerator.Text)
            Dim hden = Trim(Me.txtIDHenryDenominator.Text)
            Dim iddetails = Trim(Me.txtIDDetails.Text)
            Dim remarks = Trim(Me.txtIDRemarks.Text)
            Dim modus = Trim(Me.txtIDModusOperandi.Text)

            If NewIDNumber <> OriginalIDNumber Then
                If IDNumberExists(NewIDNumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the Number " & NewIDNumber & " already exists. Please enter a different Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtIDNumber.Focus()
                    Exit Sub
                End If
            End If


            Dim oldRow As FingerPrintDataSet.IdentifiedSlipsRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.IdentifiedSlipsRegister.FindByIDNumber(OriginalIDNumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .IDNumber = NewIDNumber
                    .DANumber = da
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = section
                    .Name = name
                    .AliasName = aliasname
                    .FathersName = father
                    .Sex = sex
                    .Address = address
                    .HenryNumerator = hnum
                    .HenryDenominator = hden
                    .IdentificationDetails = iddetails
                    .SlipFile = IDSlipImageFile
                    .Remarks = remarks
                    .ModusOperandi = modus
                End With
            End If
            Me.IDRegisterBindingSource.Position = Me.IDRegisterBindingSource.Find("IDNumber", NewIDNumber)


            Me.IDRegisterTableAdapter.UpdateQuery(NewIDNumber, da, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, iddetails, IDSlipImageFile, remarks, modus, OriginalIDNumber)

            ShowDesktopAlert("Selected Identified Slips Record updated successfully!")

            InitializeIDFields()
            ' IncrementIDNumber(NewIDNumber)
            GenerateNewIDNumber()
            Me.btnSaveID.Text = "Save"
            IDEditMode = False
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Identified Slips Record No. " & OriginalIDNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.IDRegisterBindingSource.Position = Me.IDRegisterBindingSource.Find("IDNumber", OriginalIDNumber)
        End Try



    End Sub
#End Region


#Region "SEARCH ID RECORDS"

    Public Sub SearchWithIDNumber() Handles btnIDFindByNumber.Click
        Try

            If Me.txtIDNumber.Text = "" Then
                MessageBoxEx.Show("Please enter the Number to search for.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtIDNumber.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.IDRegisterTableAdapter.FillByNumber(Me.FingerPrintDataSet.IdentifiedSlipsRegister, Me.txtIDNumber.Text)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.IDDatagrid.RowCount < 2, Me.IDDatagrid.RowCount & " Record", Me.IDDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub



    Public Sub SearchID() Handles btnSearchID.Click

        Try


            Me.Cursor = Cursors.WaitCursor

            Dim danumber = Trim(Me.txtIDDANumber.Text)
            Dim ps = Trim(Me.cmbIDPoliceStation.Text)
            Dim cr = Trim(Me.txtIDCrimeNumber.Text)
            Dim section = Trim(Me.txtIDSection.Text)
            Dim name = Trim(Me.txtIDName.Text)
            Dim aliasname = Trim(Me.txtIDAliasName.Text)
            Dim father = Trim(Me.txtIDFathersName.Text)
            Dim sex = Trim(Me.cmbIDSex.Text)
            Dim address = Trim(Me.txtIDAddress.Text)
            Dim hnum = Trim(Me.txtIDHenryNumerator.Text)
            Dim hden = Trim(Me.txtIDHenryDenominator.Text)
            Dim iddetails = Trim(Me.txtIDDetails.Text)
            Dim remarks = Trim(Me.txtIDRemarks.Text)
            Dim modus = Trim(Me.txtIDModusOperandi.Text)



            If hnum = vbNullString Then
                hnum = "HenryNumerator Like '%'"
            Else
                If hnum.Contains("%") Or hnum.Contains("_") Then
                    hnum = "HenryNumerator Like '" & hnum & "'"
                Else
                    hnum = "instr(1, HenryNumerator, '" & hnum & "', 0)>0 "
                End If
            End If

            If hden = vbNullString Then
                hden = "HenryDenominator Like '%'"
            Else
                If hden.Contains("%") Or hden.Contains("_") Then
                    hden = "HenryDenominator Like '" & hden & "'"
                Else
                    hden = "instr(1, HenryDenominator, '" & hden & "', 0)>0 "
                End If
            End If


            If SearchSetting = 0 Then 'begins with
                danumber += "%"
                ps += "%"
                cr += "%"
                section += "%"
                name += "%"
                aliasname += "%"
                father += "%"
                sex += "%"
                address += "%"
                remarks += "%"
                modus += "%"
                iddetails += "%"
            End If


            If SearchSetting = 1 Then
                If danumber = vbNullString Then danumber = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If section = vbNullString Then section = "%"
                If name = vbNullString Then name = "%"
                If aliasname = vbNullString Then aliasname = "%"
                If father = vbNullString Then father = "%"
                If sex = vbNullString Then sex = "%"
                If address = vbNullString Then address = "%"
                If ps = vbNullString Then ps = "%"
                If remarks = vbNullString Then remarks = "%"
                If modus = vbNullString Then modus = "%"
                If iddetails = vbNullString Then iddetails = "%"
            End If

            If SearchSetting = 2 Then
                danumber = "%" & danumber & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                section = "%" & section & "%"
                name = "%" & name & "%"
                aliasname = "%" & aliasname & "%"
                father = "%" & father & "%"
                sex = "%" & sex & "%"
                address = "%" & address & "%"
                remarks = "%" & remarks & "%"
                modus = "%" & modus & "%"
                iddetails = "%" & iddetails & "%"
            End If
            '
            ' Me.IDRegisterTableAdapter.FillBy(FingerPrintDataSet.IdentifiedSlipsRegister, sNumber, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, iddetails, remarks, modus)

            Dim SQLText As String = "Select * from IdentifiedSlipsRegister"




            SQLText = "Select * from IdentifiedSlipsRegister where DANumber LIKE '" & danumber & "' AND PoliceStation LIKE '" & ps & "' AND CrimeNumber LIKE '" & cr & "' AND SectionOfLaw LIKE '" & section & "' AND Name LIKE '" & name & "' AND AliasName LIKE '" & aliasname & "' AND FathersName LIKE '" & father & "' AND Sex LIKE '" & sex & "' AND Address LIKE '" & address & "' AND " & hnum & " AND " & hden & " AND ModusOperandi LIKE '" & modus & "' AND Remarks LIKE '" & remarks & "' AND IdentificationDetails LIKE '" & iddetails & "' order by IDNumber"

            SQLText = SQLText.Replace("%%", "%")

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Me.FingerPrintDataSet.IdentifiedSlipsRegister.Clear()
            da.Fill(Me.FingerPrintDataSet.IdentifiedSlipsRegister)
            con.Close()

            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.IDDatagrid.RowCount < 2, Me.IDDatagrid.RowCount & " Record", Me.IDDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


#End Region


#Region "ID SLIP IMAGE FILE SETTINGS"


    Public Sub SelectIDSlipImage() Handles btnIDSelectFPSlip.Click, btnIDSelectDisplayContext.Click 'select a photo from system
        Try

            OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select Identified Slip Image File"
            OpenFileDialog1.AutoUpgradeEnabled = True
            OpenFileDialog1.RestoreDirectory = True 'remember last directory
            Dim SelectedFile As String
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
                Application.DoEvents() 'first close the selection window
                SelectedFile = OpenFileDialog1.FileName

                Dim getInfo As System.IO.DriveInfo = My.Computer.FileSystem.GetDriveInfo(SelectedFile)
                If getInfo.DriveType <> IO.DriveType.Fixed Then

                    Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The Identified Slip Image File you selected is on a removable media. Do you want to copy it to the Identified Slips Image Files Location?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If r = Windows.Forms.DialogResult.Yes Then
                        If Strings.Right(IDSlipImageImportLocation, 1) <> "\" Then IDSlipImageImportLocation = IDSlipImageImportLocation & "\"
                        Dim DestinationFile As String = IDSlipImageImportLocation & OpenFileDialog1.SafeFileName
                        My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                        SelectedFile = DestinationFile
                    End If

                    ' If reply=vbNo then do nothing just use the selected file

                    If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                End If

                IDSlipImageFile = SelectedFile
                Me.picIDSlip.Image = New Bitmap(SelectedFile) 'display the pic
                DisplayDatabaseInformation()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ScanIDSlip() Handles btnIDScanFPSlip.Click, btnIDScanDisplayContext.Click 'import photos from camera and scanner
        On Error Resume Next

        If Me.txtIDNumber.Text = vbNullString Then

            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Number which is used as the scanned image's File Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtIDNumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim FileName As String = "IDNo." & Format(Me.txtIDNumber.Value, "0000")

        If Strings.Right(IDSlipImageImportLocation, 1) <> "\" Then IDSlipImageImportLocation = IDSlipImageImportLocation & "\"
        Dim ScannedImage As String = ImportImageFromScannerOrCamera(IDSlipImageImportLocation, FileName) 'scans the picture and returns the file name with path

        If ScannedImage <> vbNullString Then
            IDSlipImageFile = ScannedImage
            Me.picIDSlip.Image = New Bitmap(ScannedImage)
        End If
        DisplayDatabaseInformation()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub ExploreIDSlip() Handles btnIDExploreDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If IDSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(IDSlipImageFile) Then
                Call Shell("explorer.exe /select," & IDSlipImageFile, AppWinStyle.NormalFocus)
            Else
                MessageBoxEx.Show("The specified Identified Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub ClearIDImage()
        On Error Resume Next
        Me.picIDSlip.ClearImage()
        IDSlipImageFile = vbNullString
    End Sub

    Private Sub ClearIDImageWithMessage() Handles btnIDClearFPSlip.Click, btnIDClearDisplayContext.Click
        On Error Resume Next
        Me.picIDSlip.ClearImage()
        IDSlipImageFile = vbNullString
        ShowDesktopAlert("Image cleared!")
    End Sub

    Private Sub ViewImageOnIDDatagridCellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles IDDatagrid.CellDoubleClick
        On Error Resume Next
        If e.RowIndex < 0 Or e.RowIndex > Me.IDDatagrid.Rows.Count - 1 Then
            Exit Sub
        End If
        ViewIDSlipImage()
    End Sub


    Private Sub ViewIDSlipImage() Handles btnIDViewFPSlip.Click, btnViewIDSlipContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If Me.IDDatagrid.RowCount = 0 Then
            ShowDesktopAlert("No data in the list to show image!")
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim imagefile = Me.IDDatagrid.SelectedCells(15).Value.ToString
        If FileIO.FileSystem.FileExists(imagefile) = False Then

            Dim FileName As String = "IDNo." & Format(CInt(Me.IDDatagrid.SelectedCells(0).Value), "0000") & ".jpeg"
            If Strings.Right(IDSlipImageImportLocation, 1) <> "\" Then IDSlipImageImportLocation = IDSlipImageImportLocation & "\"
            imagefile = IDSlipImageImportLocation & FileName
            If FileIO.FileSystem.FileExists(imagefile) = False Then
                MessageBoxEx.Show("No image file is associated with the selected Identified Slip record!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If

        End If
        If FileIO.FileSystem.FileExists(imagefile) Then
            frmIDSlipImageDisplayer.Show()
            frmIDSlipImageDisplayer.WindowState = FormWindowState.Maximized
            frmIDSlipImageDisplayer.BringToFront()



            Dim idno As String = Me.IDDatagrid.CurrentRow.Cells(0).Value.ToString()
            Dim name As String = Me.IDDatagrid.CurrentRow.Cells(5).Value.ToString()
            Dim aliasname As String = Me.IDDatagrid.CurrentRow.Cells(6).Value.ToString()

            frmIDSlipImageDisplayer.LoadDASlipPicture(imagefile, idno & "  -   " & name & IIf(aliasname <> vbNullString, " @ " & aliasname, ""))
        Else
            MessageBoxEx.Show("The specified Identified Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Sub ViewImageOnIDDisplayDblClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picIDSlip.MouseDoubleClick
        On Error Resume Next
        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        If IDSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(IDSlipImageFile) Then
                frmIDSlipImageDisplayer.Show()
                frmIDSlipImageDisplayer.WindowState = FormWindowState.Maximized
                frmIDSlipImageDisplayer.BringToFront()
                frmIDSlipImageDisplayer.LoadPictureFromViewer(IDSlipImageFile, Me.txtIDNumber.Text)
            Else
                MessageBoxEx.Show("The specified Identified Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Sub ViewIDImage() Handles btnIDViewDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If IDSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(IDSlipImageFile) Then
                frmIDSlipImageDisplayer.Show()
                frmIDSlipImageDisplayer.WindowState = FormWindowState.Maximized
                frmIDSlipImageDisplayer.BringToFront()
                frmIDSlipImageDisplayer.LoadPictureFromViewer(IDSlipImageFile, Me.txtIDNumber.Text)
            Else
                MessageBoxEx.Show("The specified Identified Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub IDSlipContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles IDSlipContextMenuBar.PopupOpen
        On Error Resume Next
        If IDSlipImageFile = vbNullString Then
            Me.btnIDViewDisplayContext.Enabled = False
            Me.btnIDExploreDisplayContext.Enabled = False
        Else
            Me.btnIDViewDisplayContext.Enabled = True
            Me.btnIDExploreDisplayContext.Enabled = True
        End If
    End Sub

#End Region


    '---------------------------------------------AC DATA MANIPULATION-----------------------------------------
#Region "AC DATA ENTRY FIELDS SETTINGS"


    Private Sub InitializeACFields()
        On Error Resume Next
        Me.txtACNumber.Focus()
        ACSlipImageFile = vbNullString
        Me.picACSlip.ClearImage()

        Dim ctrl As Control
        For Each ctrl In Me.PanelAC.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                If ctrl.Name <> cmbACPoliceStation.Name And ctrl.Name <> cmbACSex.Name Then ctrl.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearACFields() Handles btnClearACFields.Click 'Clear all textboxes, comboboxes etc
        On Error Resume Next

        Me.txtACNumber.Focus()
        ACSlipImageFile = vbNullString
        Me.picACSlip.ClearImage()

        Dim ctrl As Control
        For Each ctrl In Me.PanelAC.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                ctrl.Text = vbNullString
            End If
        Next

    End Sub

    Private Sub ClearSelectedACFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtACNumber.ButtonCustomClick, txtACDANumber.ButtonCustomClick, txtACCrimeNumber.ButtonCustomClick, txtACSection.ButtonCustomClick, txtACName.ButtonCustomClick, txtACAliasName.ButtonCustomClick, txtACFathersName.ButtonCustomClick, txtACHenryNumerator.ButtonCustomClick, txtACHenryDenominator.ButtonCustomClick, txtACModusOperandi.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString
    End Sub



    Private Sub ValidateACSex() Handles cmbACSex.Validated
        On Error Resume Next
        Dim sex As String = UCase(Trim(Me.cmbACSex.Text))
        If sex <> "MALE" And sex <> "FEMALE" And sex <> vbNullString Then
            If sex = "M" Then
                Me.cmbACSex.Text = "Male"
            ElseIf sex = "F" Then
                Me.cmbACSex.Text = "Female"
            Else
                DevComponents.DotNetBar.MessageBoxEx.Show("The sex you entered is invalid. Please select a sex from the list or leave it blank", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.cmbACSex.Focus()
            End If

        End If
    End Sub



    Private Sub LoadDetailsToACFromDANumber() Handles txtACDANumber.Validated
        On Error Resume Next
        If Me.chkACLoadOtherDetails.Checked = False Then Exit Sub
        Dim n = Trim(Me.txtACDANumber.Text)
        If n = vbNullString Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Dim oldRow As FingerPrintDataSet.DARegisterRow
        oldRow = Me.FingerPrintDataSet.DARegister.FindByDANumber(n)

        If oldRow IsNot Nothing Then

            If ACEditMode Then
                Me.cmbACPoliceStation.Focus()
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default '
                Exit Sub
            End If


            With oldRow
                Me.cmbACPoliceStation.Text = .PoliceStation
                Me.txtACCrimeNumber.Text = .CrimeNumber
                Me.txtACSection.Text = .SectionOfLaw
                Me.txtACName.Text = .Name
                Me.txtACAliasName.Text = .AliasName
                Me.txtACFathersName.Text = .FathersName
                Me.txtACAddress.Text = .Address
                Me.cmbACSex.Text = .Sex
                Me.txtACHenryDenominator.Text = .HenryDenominator
                Me.txtACHenryNumerator.Text = .HenryNumerator
                Me.txtACRemarks.Text = .Remarks
                ACSlipImageFile = .SlipFile
                Me.txtACModusOperandi.Text = .ModusOperandi
                If FileIO.FileSystem.FileExists(ACSlipImageFile) = True Then
                    Me.picACSlip.Image = New Bitmap(ACSlipImageFile)
                Else
                    ClearACImage()
                End If
            End With
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub FindACNumber() Handles txtACDANumber.GotFocus
        On Error Resume Next
        Dim p = Me.ACRegisterBindingSource.Find("ACNumber", Trim(Me.txtACNumber.Text))
        If p < 0 Then Exit Sub
        Me.ACRegisterBindingSource.Position = p

    End Sub


    Private Sub CapitalizeACHenryClassificationDenominator() Handles txtACHenryDenominator.Validated
        On Error Resume Next


        Dim t As String = Me.txtACHenryDenominator.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtACHenryDenominator.Text = t

    End Sub

    Private Sub CapitalizeACHenryClassificationNumerator() Handles txtACHenryNumerator.Validated
        On Error Resume Next


        Dim t As String = Me.txtACHenryNumerator.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtACHenryNumerator.Text = t

    End Sub
#End Region


#Region "AC MANDATORY FIELDS"


    Function MandatoryACFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtACNumber.Text) = vbNullString Or Trim(Me.txtACName.Text) = vbNullString Or Trim(Me.txtACHenryNumerator.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub ShowMandatoryACFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Trim(Me.txtACNumber.Text) = vbNullString Then
            msg = msg & " Number" & vbNewLine
            x = 1
        End If

        If Trim(Me.txtACName.Text) = vbNullString Then
            msg = msg & " Name" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.txtACHenryNumerator.Text) = vbNullString Then
            msg = msg & " Henry Numerator" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                Me.txtACNumber.Focus()
            Case 2
                Me.txtACName.Focus()
            Case 3
                Me.txtACHenryNumerator.Focus()
        End Select

    End Sub


#End Region


#Region "AC SAVE BUTTON ACTION"

    Private Sub ACSaveButtonAction() Handles btnSaveAC.Click
        On Error Resume Next

        CapitalizeACHenryClassificationNumerator()
        CapitalizeACHenryClassificationDenominator()

        AddTextsToAutoCompletionList()

        If ACEditMode Then
            UpdateACData()
        Else
            SaveNewACEntry()
        End If
    End Sub
#End Region


#Region "AC NEW DATA ENTRY"


    Private Sub SaveNewACEntry()
        Try

            If MandatoryACFieldsNotFilled() Then
                ShowMandatoryACFieldsInfo()
                Exit Sub
            End If

            OriginalACNumber = Me.txtACNumber.Value
            Dim da = Trim(Me.txtACDANumber.Text)
            Dim ps = Trim(Me.cmbACPoliceStation.Text)
            Dim cr = Trim(Me.txtACCrimeNumber.Text)
            Dim section = Trim(Me.txtACSection.Text)
            Dim name = Trim(Me.txtACName.Text)
            Dim aliasname = Trim(Me.txtACAliasName.Text)
            Dim father = Trim(Me.txtACFathersName.Text)
            Dim sex = Trim(Me.cmbACSex.Text)
            Dim address = Trim(Me.txtACAddress.Text)
            Dim hnum = Trim(Me.txtACHenryNumerator.Text)
            Dim hden = Trim(Me.txtACHenryDenominator.Text)
            Dim modus = Trim(Me.txtACModusOperandi.Text)
            Dim remarks = Trim(Me.txtACRemarks.Text)

            If ACNumberExists(OriginalACNumber) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the Number " & OriginalACNumber & " already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.Yes Then
                    OverWriteACData()
                Else
                    Me.txtACNumber.Focus()
                End If
                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.ActiveCriminalsRegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.ActiveCriminalsRegister.NewActiveCriminalsRegisterRow()
            With newRow
                .ACNumber = OriginalACNumber
                .DANumber = da
                .PoliceStation = ps
                .CrimeNumber = cr
                .SectionOfLaw = section
                .Name = name
                .AliasName = aliasname
                .FathersName = father
                .Sex = sex
                .Address = address
                .HenryNumerator = hnum
                .HenryDenominator = hden
                .ModusOperandi = modus
                .SlipFile = ACSlipImageFile
                .Remarks = remarks
            End With

            Me.FingerPrintDataSet.ActiveCriminalsRegister.Rows.Add(newRow) ' add the row to the table
            Me.ACRegisterBindingSource.Position = Me.ACRegisterBindingSource.Find("ACNumber", OriginalACNumber)


            Me.ACRegisterTableAdapter.Insert(OriginalACNumber, da, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, modus, ACSlipImageFile, remarks)
            ShowDesktopAlert("New Active Criminal Slips Record entered successfully!")

            InitializeACFields()
            IncrementACNumber(OriginalACNumber)
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Added new Active Criminal Slip Record No. " & OriginalACNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function ACNumberExists(ByVal ACNumber As Long)
        On Error Resume Next
        If Me.ACRegisterTableAdapter.CheckACNumberExists(ACNumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "OVERWRITE AC REGISTER"
    Private Sub OverWriteACData()
        Try

            Dim NewACNumber As Long = Me.txtACNumber.Value
            Dim da = Trim(Me.txtACDANumber.Text)
            Dim ps = Trim(Me.cmbACPoliceStation.Text)
            Dim cr = Trim(Me.txtACCrimeNumber.Text)
            Dim section = Trim(Me.txtACSection.Text)
            Dim name = Trim(Me.txtACName.Text)
            Dim aliasname = Trim(Me.txtACAliasName.Text)
            Dim father = Trim(Me.txtACFathersName.Text)
            Dim sex = Trim(Me.cmbACSex.Text)
            Dim address = Trim(Me.txtACAddress.Text)
            Dim hnum = Trim(Me.txtACHenryNumerator.Text)
            Dim hden = Trim(Me.txtACHenryDenominator.Text)
            Dim modus = Trim(Me.txtACModusOperandi.Text)
            Dim remarks = Trim(Me.txtACRemarks.Text)


            Dim oldRow As FingerPrintDataSet.ActiveCriminalsRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.ActiveCriminalsRegister.FindByACNumber(OriginalACNumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .ACNumber = NewACNumber
                    .DANumber = da
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = section
                    .Name = name
                    .AliasName = aliasname
                    .FathersName = father
                    .Sex = sex
                    .Address = address
                    .HenryNumerator = hnum
                    .HenryDenominator = hden
                    .ModusOperandi = modus
                    .SlipFile = ACSlipImageFile
                    .Remarks = remarks
                End With
            End If
            Me.ACRegisterBindingSource.Position = Me.ACRegisterBindingSource.Find("ACNumber", NewACNumber)


            Me.ACRegisterTableAdapter.UpdateQuery(NewACNumber, da, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, modus, ACSlipImageFile, remarks, OriginalACNumber)


            ShowDesktopAlert("Active Criminal Slips Record over writed!")

            InitializeACFields()
            ' IncrementACNumber(NewACNumber)
            GenerateNewACNumber()
            Me.btnSaveAC.Text = "Save"
            ACEditMode = False

            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Active Criminal Slip Record No. " & OriginalACNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.ACRegisterBindingSource.Position = Me.ACRegisterBindingSource.Find("ACNumber", OriginalACNumber)
        End Try

    End Sub
#End Region


#Region "AC EDIT RECORD"

    Private Sub UpdateACData()

        Try

            If MandatoryACFieldsNotFilled() Then
                ShowMandatoryACFieldsInfo()
                Exit Sub
            End If

            Dim NewACNumber As Long = Me.txtACNumber.Value
            Dim da = Trim(Me.txtACDANumber.Text)
            Dim ps = Trim(Me.cmbACPoliceStation.Text)
            Dim cr = Trim(Me.txtACCrimeNumber.Text)
            Dim section = Trim(Me.txtACSection.Text)
            Dim name = Trim(Me.txtACName.Text)
            Dim aliasname = Trim(Me.txtACAliasName.Text)
            Dim father = Trim(Me.txtACFathersName.Text)
            Dim sex = Trim(Me.cmbACSex.Text)
            Dim address = Trim(Me.txtACAddress.Text)
            Dim hnum = Trim(Me.txtACHenryNumerator.Text)
            Dim hden = Trim(Me.txtACHenryDenominator.Text)
            Dim modus = Trim(Me.txtACModusOperandi.Text)
            Dim remarks = Trim(Me.txtACRemarks.Text)

            If NewACNumber <> OriginalACNumber Then
                If ACNumberExists(NewACNumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the Number " & NewACNumber & " already exists. Please enter a different Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtACNumber.Focus()
                    Exit Sub
                End If
            End If


            Dim oldRow As FingerPrintDataSet.ActiveCriminalsRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.ActiveCriminalsRegister.FindByACNumber(OriginalACNumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .ACNumber = NewACNumber
                    .DANumber = da
                    .PoliceStation = ps
                    .CrimeNumber = cr
                    .SectionOfLaw = section
                    .Name = name
                    .AliasName = aliasname
                    .FathersName = father
                    .Sex = sex
                    .Address = address
                    .HenryNumerator = hnum
                    .HenryDenominator = hden
                    .ModusOperandi = modus
                    .SlipFile = ACSlipImageFile
                    .Remarks = remarks
                End With
            End If
            Me.ACRegisterBindingSource.Position = Me.ACRegisterBindingSource.Find("ACNumber", NewACNumber)


            Me.ACRegisterTableAdapter.UpdateQuery(NewACNumber, da, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, modus, ACSlipImageFile, remarks, OriginalACNumber)

            ShowDesktopAlert("Selected Active Criminal Record updated successfully!")
            InitializeACFields()
            ' IncrementACNumber(NewACNumber)
            GenerateNewACNumber()
            Me.btnSaveAC.Text = "Save"
            ACEditMode = False
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Active Criminal Slip Record No. " & OriginalACNumber)

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.ACRegisterBindingSource.Position = Me.ACRegisterBindingSource.Find("ACNumber", OriginalACNumber)
        End Try
    End Sub
#End Region


#Region "SEARCH AC RECORDS"


    Public Sub SearchWithACNumber() Handles btnACFindByNumber.Click
        Try
            If Me.txtACNumber.Text = "" Then
                MessageBoxEx.Show("Please enter the Number to search for.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtACNumber.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor

            Me.ACRegisterTableAdapter.FillByNumber(Me.FingerPrintDataSet.ActiveCriminalsRegister, Me.txtACNumber.Text)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.ACDatagrid.RowCount < 2, Me.ACDatagrid.RowCount & " Record", Me.ACDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub SearchAC() Handles btnSearchAC.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim danumber = Trim(Me.txtACDANumber.Text)
            Dim ps = Trim(Me.cmbACPoliceStation.Text)
            Dim cr = Trim(Me.txtACCrimeNumber.Text)
            Dim section = Trim(Me.txtACSection.Text)
            Dim name = Trim(Me.txtACName.Text)
            Dim aliasname = Trim(Me.txtACAliasName.Text)
            Dim father = Trim(Me.txtACFathersName.Text)
            Dim sex = Trim(Me.cmbACSex.Text)
            Dim address = Trim(Me.txtACAddress.Text)
            Dim hnum = Trim(Me.txtACHenryNumerator.Text)
            Dim hden = Trim(Me.txtACHenryDenominator.Text)
            Dim modus = Trim(Me.txtACModusOperandi.Text)
            Dim remarks = Trim(Me.txtACRemarks.Text)

            If hnum = vbNullString Then
                hnum = "HenryNumerator Like '%'"
            Else
                If hnum.Contains("%") Or hnum.Contains("_") Then
                    hnum = "HenryNumerator Like '" & hnum & "'"
                Else
                    hnum = "instr(1, HenryNumerator, '" & hnum & "', 0)>0 "
                End If
            End If

            If hden = vbNullString Then
                hden = "HenryDenominator Like '%'"
            Else
                If hden.Contains("%") Or hden.Contains("_") Then
                    hden = "HenryDenominator Like '" & hden & "'"
                Else
                    hden = "instr(1, HenryDenominator, '" & hden & "', 0)>0 "
                End If
            End If


            If SearchSetting = 0 Then 'begins with
                danumber += "%"
                ps += "%"
                cr += "%"
                section += "%"
                name += "%"
                aliasname += "%"
                father += "%"
                sex += "%"
                address += "%"
                remarks += "%"
                modus += "%"
            End If


            If SearchSetting = 1 Then
                If danumber = vbNullString Then danumber = "%"
                If ps = vbNullString Then ps = "%"
                If cr = vbNullString Then cr = "%"
                If section = vbNullString Then section = "%"
                If name = vbNullString Then name = "%"
                If aliasname = vbNullString Then aliasname = "%"
                If father = vbNullString Then father = "%"
                If sex = vbNullString Then sex = "%"
                If address = vbNullString Then address = "%"
                If ps = vbNullString Then ps = "%"
                If remarks = vbNullString Then remarks = "%"
                If modus = vbNullString Then modus = "%"
            End If

            If SearchSetting = 2 Then
                danumber = "%" & danumber & "%"
                ps = "%" & ps & "%"
                cr = "%" & cr & "%"
                section = "%" & section & "%"
                name = "%" & name & "%"
                aliasname = "%" & aliasname & "%"
                father = "%" & father & "%"
                sex = "%" & sex & "%"
                address = "%" & address & "%"
                remarks = "%" & remarks & "%"
                modus = "%" & modus & "%"
            End If


            '  Me.ACRegisterTableAdapter.FillBy(FingerPrintDataSet.ActiveCriminalsRegister, danumber, ps, cr, section, name, aliasname, father, sex, address, hnum, hden, modus, remarks)
            '

            Dim SQLText As String = "Select * from ActiveCriminalsRegister"




            SQLText = "Select * from ActiveCriminalsRegister where DANumber LIKE '" & danumber & "' AND PoliceStation LIKE '" & ps & "' AND CrimeNumber LIKE '" & cr & "' AND SectionOfLaw LIKE '" & section & "' AND Name LIKE '" & name & "' AND AliasName LIKE '" & aliasname & "' AND FathersName LIKE '" & father & "' AND Sex LIKE '" & sex & "' AND Address LIKE '" & address & "' AND " & hnum & " AND " & hden & " AND ModusOperandi LIKE '" & modus & "' AND Remarks LIKE '" & remarks & "' order by ACNumber"

            SQLText = SQLText.Replace("%%", "%")

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Me.FingerPrintDataSet.ActiveCriminalsRegister.Clear()
            da.Fill(Me.FingerPrintDataSet.ActiveCriminalsRegister)
            con.Close()





            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.ACDatagrid.RowCount < 2, Me.ACDatagrid.RowCount & " Record", Me.ACDatagrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


#End Region



#Region "AC SLIP IMAGE FILE SETTINGS"


    Public Sub SelectACSlipImage() Handles btnACSelectFPSlip.Click, btnACSelectDisplayContext.Click 'select a photo from system
        Try


            OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select Active Criminal Slip Image File"
            OpenFileDialog1.AutoUpgradeEnabled = True
            OpenFileDialog1.RestoreDirectory = True 'remember last directory
            Dim SelectedFile As String
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
                Application.DoEvents() 'first close the selection window
                SelectedFile = OpenFileDialog1.FileName

                Dim getInfo As System.IO.DriveInfo = My.Computer.FileSystem.GetDriveInfo(SelectedFile)
                If getInfo.DriveType <> IO.DriveType.Fixed Then

                    Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The Active Criminal Slip Image File you selected is on a removable media. Do you want to copy it to the Active Criminal Slips Image Files Location?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If r = Windows.Forms.DialogResult.Yes Then
                        If Strings.Right(ACSlipImageImportLocation, 1) <> "\" Then ACSlipImageImportLocation = ACSlipImageImportLocation & "\"
                        Dim DestinationFile As String = ACSlipImageImportLocation & OpenFileDialog1.SafeFileName
                        My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                        SelectedFile = DestinationFile
                    End If

                    ' If reply=vbNo then do nothing just use the selected file

                    If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
                End If

                ACSlipImageFile = SelectedFile
                Me.picACSlip.Image = New Bitmap(SelectedFile) 'display the pic
                DisplayDatabaseInformation()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ScanACSlip() Handles btnACScanFPSlip.Click, btnACScanDisplayContext.Click 'import photos from camera and scanner
        On Error Resume Next

        If Me.txtACNumber.Text = vbNullString Then

            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Number which is used as the scanned image's File Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtACNumber.Focus()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim FileName As String = "ACNo." & Format(Me.txtACNumber.Value, "0000")

        If Strings.Right(ACSlipImageImportLocation, 1) <> "\" Then ACSlipImageImportLocation = ACSlipImageImportLocation & "\"
        Dim ScannedImage As String = ImportImageFromScannerOrCamera(ACSlipImageImportLocation, FileName) 'scans the picture and returns the file name with path

        If ScannedImage <> vbNullString Then
            ACSlipImageFile = ScannedImage
            Me.picACSlip.Image = New Bitmap(ScannedImage)
        End If
        DisplayDatabaseInformation()

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub ExploreACSlip() Handles btnACExploreDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If ACSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(ACSlipImageFile) Then
                Call Shell("explorer.exe /select," & ACSlipImageFile, AppWinStyle.NormalFocus)

            Else
                MessageBoxEx.Show("The specified Active Criminal Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Private Sub ClearACImage()
        On Error Resume Next
        Me.picACSlip.ClearImage()
        ACSlipImageFile = vbNullString
    End Sub

    Private Sub ClearACImageWithMessage() Handles btnACClearFPSlip.Click, btnACClearDisplayContext.Click
        On Error Resume Next
        Me.picACSlip.ClearImage()
        ACSlipImageFile = vbNullString
        ShowDesktopAlert("Image cleared!")
    End Sub


    Private Sub ViewImageOnACDatagridCellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ACDatagrid.CellDoubleClick
        On Error Resume Next
        If e.RowIndex < 0 Or e.RowIndex > Me.ACDatagrid.Rows.Count - 1 Then
            Exit Sub
        End If
        ViewACSlipImage()
    End Sub


    Private Sub ViewACSlipImage() Handles btnACViewFPSlip.Click, btnViewACSlipContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If Me.ACDatagrid.RowCount = 0 Then
            ShowDesktopAlert("No data in the list to show image!")
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim imagefile = Me.ACDatagrid.SelectedCells(14).Value.ToString
        If FileIO.FileSystem.FileExists(imagefile) = False Then

            Dim FileName As String = "ACNo." & Format(CInt(Me.ACDatagrid.SelectedCells(0).Value), "0000") & ".jpeg"
            If Strings.Right(ACSlipImageImportLocation, 1) <> "\" Then ACSlipImageImportLocation = ACSlipImageImportLocation & "\"
            imagefile = ACSlipImageImportLocation & FileName
            If FileIO.FileSystem.FileExists(imagefile) = False Then
                MessageBoxEx.Show("No image file is associated with the selected Active Criminal Slip record!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If
        If FileIO.FileSystem.FileExists(imagefile) Then
            FrmACSlipImageDisplayer.Show()
            FrmACSlipImageDisplayer.WindowState = FormWindowState.Maximized
            FrmACSlipImageDisplayer.BringToFront()

            Dim idno As String = Me.ACDatagrid.CurrentRow.Cells(0).Value.ToString()
            Dim name As String = Me.ACDatagrid.CurrentRow.Cells(5).Value.ToString()
            Dim aliasname As String = Me.ACDatagrid.CurrentRow.Cells(6).Value.ToString()

            FrmACSlipImageDisplayer.LoadACSlipPicture(imagefile, idno & "  -   " & name & IIf(aliasname <> vbNullString, " @ " & aliasname, ""))

        Else
            MessageBoxEx.Show("The specified Active Criminal Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub


    Sub ViewImageOnACDisplayDblClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picACSlip.MouseDoubleClick
        On Error Resume Next
        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        If ACSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(ACSlipImageFile) Then
                FrmACSlipImageDisplayer.Show()
                FrmACSlipImageDisplayer.WindowState = FormWindowState.Maximized
                FrmACSlipImageDisplayer.BringToFront()
                FrmACSlipImageDisplayer.LoadPictureFromViewer(ACSlipImageFile, Me.txtACNumber.Text)
            Else
                MessageBoxEx.Show("The specified Active Criminal Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Sub ViewACImage() Handles btnACViewDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If ACSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(ACSlipImageFile) Then
                FrmACSlipImageDisplayer.Show()
                FrmACSlipImageDisplayer.WindowState = FormWindowState.Maximized
                FrmACSlipImageDisplayer.BringToFront()
                FrmACSlipImageDisplayer.LoadPictureFromViewer(ACSlipImageFile, Me.txtACNumber.Text)
            Else
                MessageBoxEx.Show("The specified Identified Slip Image file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub ACSlipContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ACSlipContextMenuBar.PopupOpen
        On Error Resume Next
        If ACSlipImageFile = vbNullString Then
            Me.btnACViewDisplayContext.Enabled = False
            Me.btnACExploreDisplayContext.Enabled = False
        Else
            Me.btnACViewDisplayContext.Enabled = True
            Me.btnACExploreDisplayContext.Enabled = True
        End If
    End Sub

#End Region


    '---------------------------------------------CD DATA MANIPULATION-----------------------------------------
#Region "CD REGISTER DATAENTRY SETTINGS"
    Private Sub InitializeCDFields()
        On Error Resume Next

        Me.txtCDNumber.Focus()
        Dim ctrl As Control
        For Each ctrl In Me.PanelCD.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                If ctrl.Name <> txtCDYear.Name Then ctrl.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearCDFields() Handles btnClearCDFields.Click 'Clear all textboxes, comboboxes etc
        On Error Resume Next

        Me.txtCDNumber.Focus() 'set focus to the first field StudentID
        Me.dtCDExamination.Text = vbNullString 'clear dob
        Dim ctrl As Control
        For Each ctrl In Me.PanelCD.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                If ctrl.Name <> txtCDYear.Name Then ctrl.Text = vbNullString
            End If
        Next

    End Sub

    Private Sub ClearSelectedCDFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCNumber.ButtonCustomClick, txtCDNumber.ButtonCustomClick, txtCDCrNo.ButtonCustomClick, txtCourt.ButtonCustomClick
        On Error Resume Next

        DirectCast(sender, Control).Text = vbNullString

    End Sub

    Private Sub AppendCDYear() Handles txtCDNumber.Leave
        On Error Resume Next
        If Me.chkAppendCDYear.Checked = False Then Exit Sub
        Dim y As String = Me.txtCDYear.Text
        If y = vbNullString Then Exit Sub
        If Me.chkCDTwodigits.Checked Then y = Strings.Right(y, 2)
        Dim n As String = Trim(Me.txtCDNumber.Text)
        Dim l As Short = Strings.Len(n)
        If n <> vbNullString And l < 11 And y <> vbNullString Then
            If Strings.InStr(n, "/", CompareMethod.Text) = 0 Then
                Me.txtCDNumber.Text = n & "/" & y
            End If
        End If

    End Sub

    Private Sub GenerateCDNumberWithoutYear(ByVal CDNumber As String)
        On Error Resume Next
        Dim s = Strings.Split(CDNumber, "/")
        Me.txtCDNumberOnly.Text = s(0)
    End Sub

    Private Sub ValidateCDNumber() Handles txtCDNumber.Validated
        On Error Resume Next
        GenerateCDNumberWithoutYear(Me.txtCDNumber.Text)
    End Sub

    Private Sub FindCDNumber() Handles dtCDExamination.GotFocus
        On Error Resume Next
        Dim p = Me.CDRegisterBindingSource.Find("CDNumberWithYear", Trim(Me.txtCDNumber.Text))
        If p < 0 Then Exit Sub
        Me.CDRegisterBindingSource.Position = p
    End Sub

#End Region


#Region "CD MANDATORY FIELDS"


    Function MandatoryCDFieldsNotFilled() As Boolean
        On Error Resume Next
        If Me.dtCDExamination.IsEmpty Or Trim(Me.cmbCDPoliceStation.Text) = vbNullString Or Trim(Me.txtCDCrNo.Text) = vbNullString Or Trim(Me.txtCourt.Text) = vbNullString Or Trim(Me.txtCCNumber.Text) = vbNullString Or Trim(Me.cmbCDOfficer.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub ShowMandatorycdFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Me.dtCDExamination.IsEmpty Then
            msg = msg & " Date of Examination" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.cmbCDOfficer.Text) = vbNullString Then
            msg = msg & " Officer's Name" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3

        End If

        If Trim(Me.txtCourt.Text) = vbNullString Then
            msg = msg & " Name of the Court" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4

        End If
        If Trim(Me.txtCCNumber.Text) = vbNullString Then
            msg = msg & " CC Number" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If

        If Trim(Me.cmbCDPoliceStation.Text) = vbNullString Then
            msg = msg & " Police Station" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If
        If Trim(Me.txtCDCrNo.Text) = vbNullString Then
            msg = msg & " Crime Number" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 Then x = 7
        End If



        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                txtCDNumber.Focus()
            Case 2
                dtCDExamination.Focus()
            Case 3
                cmbCDOfficer.Focus()
            Case 4
                txtCourt.Focus()
            Case 5
                txtCCNumber.Focus()
            Case 6
                cmbCDPoliceStation.Focus()
            Case 7
                txtCDCrNo.Focus()
        End Select

    End Sub


#End Region


#Region "CD SAVE BUTTON ACTION"

    Private Sub CDSaveButtonAction() Handles btnSaveCD.Click
        On Error Resume Next

        If CDEditMode Then
            UpdateCDData()
        Else
            SaveNewCDEntry()
        End If
    End Sub
#End Region


#Region "CD NEW DATA ENTRY"


    Private Sub SaveNewCDEntry()
        Try

            If MandatoryCDFieldsNotFilled() Then
                ShowMandatorycdFieldsInfo()
                Exit Sub
            End If

            OriginalCDNumber = Trim(Me.txtCDNumber.Text)
            GenerateCDNumberWithoutYear(OriginalCDNumber)
            Dim CDN = Me.txtCDNumberOnly.Text
            Dim ps = Trim(Me.cmbCDPoliceStation.Text)
            Dim crno = Trim(Me.txtCDCrNo.Text)
            Dim officer = Trim(Me.cmbCDOfficer.Text)
            Dim court = Trim(Me.txtCourt.Text)
            Dim ccno = Trim(Me.txtCCNumber.Text)
            Dim details = Trim(Me.txtCDDetails.Text)
            Dim remarks = Trim(Me.txtCDRemarks.Text)

            If CDNumberExists(OriginalCDNumber) Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the CD Number " & OriginalCDNumber & " already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.Yes Then
                    OverWriteCDData()
                Else
                    Me.txtCDNumber.Focus()
                    Me.txtCDNumber.SelectAll()
                End If
                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.CDRegisterRow 'add a new row to insert values
            newRow = Me.FingerPrintDataSet.CDRegister.NewCDRegisterRow()
            With newRow
                .CDNumberWithYear = OriginalCDNumber
                .CDNumberWithoutYear = CDN
                .DateOfExamination = Me.dtCDExamination.ValueObject
                .NameOfOfficer = officer
                .Court = court
                .CCNumber = ccno
                .PoliceStation = ps
                .CrNumber = crno
                .Details = details
                .Remarks = remarks
            End With

            Me.FingerPrintDataSet.CDRegister.Rows.Add(newRow) ' add the row to the table
            Me.CDRegisterBindingSource.Position = Me.CDRegisterBindingSource.Find("CDNumberWithYear", OriginalCDNumber)

            Me.CDRegisterTableAdapter.Insert(OriginalCDNumber, CDN, Me.dtCDExamination.ValueObject, officer, court, ccno, ps, crno, details, remarks)
            ShowDesktopAlert("New CD Record entered successfully!")

            InitializeCDFields()
            IncrementCDNumber(OriginalCDNumber)

            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Added new Court Duty Record No. " & OriginalCDNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function CDNumberExists(ByVal CDNumber As String)
        On Error Resume Next
        If Me.CDRegisterTableAdapter.CheckCDNumberExists(CDNumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "CD EDIT RECORD"

    Private Sub UpdateCDData()

        Try


            If MandatoryCDFieldsNotFilled() Then
                ShowMandatorycdFieldsInfo()
                Exit Sub
            End If

            Dim NewCDNumber As String = Trim(Me.txtCDNumber.Text)
            GenerateCDNumberWithoutYear(NewCDNumber)
            Dim CDN = Me.txtCDNumberOnly.Text
            Dim ps = Trim(Me.cmbCDPoliceStation.Text)
            Dim crno = Trim(Me.txtCDCrNo.Text)
            Dim officer = Trim(Me.cmbCDOfficer.Text)
            Dim court = Trim(Me.txtCourt.Text)
            Dim ccno = Trim(Me.txtCCNumber.Text)
            Dim details = Trim(Me.txtCDDetails.Text)
            Dim remarks = Trim(Me.txtCDRemarks.Text)

            If LCase(NewCDNumber) <> LCase(OriginalCDNumber) Then
                If CDNumberExists(NewCDNumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the CD Number " & NewCDNumber & " already exists. Please enter a different CD Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtCDNumber.Focus()
                    Me.txtCDNumber.SelectAll()
                    Exit Sub
                End If
            End If

            Dim oldRow As FingerPrintDataSet.CDRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.CDRegister.FindByCDNumberWithYear(OriginalCDNumber)
            If oldRow IsNot Nothing Then
                With oldRow
                    .CDNumberWithYear = NewCDNumber
                    .CDNumberWithoutYear = CDN
                    .DateOfExamination = Me.dtCDExamination.ValueObject
                    .NameOfOfficer = officer
                    .Court = court
                    .CCNumber = ccno
                    .PoliceStation = ps
                    .CrNumber = crno
                    .Details = details
                    .Remarks = remarks
                End With
            End If
            Me.CDRegisterBindingSource.Position = Me.CDRegisterBindingSource.Find("CDNumberWithYear", NewCDNumber)

            Me.CDRegisterTableAdapter.UpdateQuery(NewCDNumber, CDN, Me.dtCDExamination.ValueObject, officer, court, ccno, ps, crno, details, remarks, OriginalCDNumber)

            ShowDesktopAlert("Selected CD Record updated successfully!")
            InitializeCDFields()
            ' IncrementCDNumber(NewCDNumber)
            GenerateNewCDNumber()
            Me.dtCDExamination.Value = Today
            Me.btnSaveCD.Text = "Save"
            CDEditMode = False

            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Court Duty Record No. " & OriginalCDNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.CDRegisterBindingSource.Position = Me.CDRegisterBindingSource.Find("CDNumberWithYear", OriginalDANumber)
        End Try


    End Sub
#End Region


#Region "SEARCH CD RECORDS"


    Public Sub SearchWithCDNumber() Handles btnCDFindByNumber.Click
        Try


            If Me.txtCDNumber.Text = "" Then
                MessageBoxEx.Show("Please enter the CD Number to search for.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCDNumber.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor

            Me.CDRegisterTableAdapter.FillByNumber(Me.FingerPrintDataSet.CDRegister, Me.txtCDNumber.Text)
            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.CDDataGrid.RowCount < 2, Me.CDDataGrid.RowCount & " Record", Me.CDDataGrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


    Public Sub SearchCD() Handles btnSearchCD.Click

        Try

            Me.Cursor = Cursors.WaitCursor
            Dim sNumber = Me.txtCDNumber.Text
            Dim ps = Trim(Me.cmbCDPoliceStation.Text)
            Dim crno = Trim(Me.txtCDCrNo.Text)
            Dim officer = Trim(Me.cmbCDOfficer.Text)
            Dim court = Trim(Me.txtCourt.Text)
            Dim ccno = Trim(Me.txtCCNumber.Text)
            Dim details = Trim(Me.txtCDDetails.Text)
            Dim remarks = Trim(Me.txtCDRemarks.Text)

            If SearchSetting = 1 Then
                If sNumber = vbNullString Then sNumber = "%"
                If ps = vbNullString Then ps = "%"
                If crno = vbNullString Then crno = "%"
                If officer = vbNullString Then court = "%"
                If court = vbNullString Then Name = "%"
                If ccno = vbNullString Then ccno = "%"
                If details = vbNullString Then details = "%"
                If remarks = vbNullString Then remarks = "%"
            End If

            If Me.dtCDExamination.IsEmpty Then
                Select Case SearchSetting
                    Case 0
                        Me.CDRegisterTableAdapter.FillByNoDE(FingerPrintDataSet.CDRegister, sNumber & "%", officer & "%", court & "%", ccno & "%", ps & "%", crno & "%", details & "%", remarks & "%")
                    Case 1
                        Me.CDRegisterTableAdapter.FillByNoDE(FingerPrintDataSet.CDRegister, sNumber, officer, court, ccno, ps, crno, details, remarks)
                    Case 2
                        Me.CDRegisterTableAdapter.FillByNoDE(FingerPrintDataSet.CDRegister, "%" & sNumber & "%", "%" & officer & "%", "%" & court & "%", "%" & ccno & "%", "%" & ps & "%", "%" & crno & "%", "%" & details & "%", "%" & remarks & "%")

                End Select
            End If

            If Me.dtCDExamination.IsEmpty = False Then
                Select Case SearchSetting
                    Case 0
                        Me.CDRegisterTableAdapter.FillByDE(FingerPrintDataSet.CDRegister, sNumber & "%", officer & "%", court & "%", ccno & "%", ps & "%", crno & "%", details & "%", remarks & "%", Me.dtCDExamination.ValueObject)
                    Case 1
                        Me.CDRegisterTableAdapter.FillByDE(FingerPrintDataSet.CDRegister, sNumber, officer, court, ccno, ps, crno, details, remarks, Me.dtCDExamination.ValueObject)
                    Case 2
                        Me.CDRegisterTableAdapter.FillByDE(FingerPrintDataSet.CDRegister, "%" & sNumber & "%", "%" & officer & "%", "%" & court & "%", "%" & ccno & "%", "%" & ps & "%", "%" & crno & "%", "%" & details & "%", "%" & remarks & "%", Me.dtCDExamination.ValueObject)
                End Select
            End If



            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.CDDataGrid.RowCount < 2, Me.CDDataGrid.RowCount & " Record", Me.CDDataGrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub SearchCDInSelectedYear() Handles btnSearchCDInYear.Click

        Try

            Me.Cursor = Cursors.WaitCursor
            Dim y As String = Me.txtCDYear.Text
            If y = vbNullString Then
                MessageBoxEx.Show("Please enter the year in the year field", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtCDYear.Focus()
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim d1 As Date = New Date(y, 1, 1)
            Dim d2 As Date = New Date(y, 12, 31)

            Dim sNumber = Me.txtCDNumber.Text
            Dim ps = Trim(Me.cmbCDPoliceStation.Text)
            Dim crno = Trim(Me.txtCDCrNo.Text)
            Dim officer = Trim(Me.cmbCDOfficer.Text)
            Dim court = Trim(Me.txtCourt.Text)
            Dim ccno = Trim(Me.txtCCNumber.Text)
            Dim details = Trim(Me.txtCDDetails.Text)
            Dim remarks = Trim(Me.txtCDRemarks.Text)

            If SearchSetting = 1 Then
                If sNumber = vbNullString Then sNumber = "%"
                If ps = vbNullString Then ps = "%"
                If crno = vbNullString Then crno = "%"
                If officer = vbNullString Then court = "%"
                If court = vbNullString Then Name = "%"
                If ccno = vbNullString Then ccno = "%"
                If details = vbNullString Then details = "%"
                If remarks = vbNullString Then remarks = "%"
            End If

            If Me.dtCDExamination.IsEmpty Then
                Select Case SearchSetting
                    Case 0
                        Me.CDRegisterTableAdapter.FillByNoDESelectedYear(FingerPrintDataSet.CDRegister, sNumber & "%", officer & "%", court & "%", ccno & "%", ps & "%", crno & "%", details & "%", remarks & "%", d1, d2)
                    Case 1
                        Me.CDRegisterTableAdapter.FillByNoDESelectedYear(FingerPrintDataSet.CDRegister, sNumber, officer, court, ccno, ps, crno, details, remarks, d1, d2)
                    Case 2
                        Me.CDRegisterTableAdapter.FillByNoDESelectedYear(FingerPrintDataSet.CDRegister, "%" & sNumber & "%", "%" & officer & "%", "%" & court & "%", "%" & ccno & "%", "%" & ps & "%", "%" & crno & "%", "%" & details & "%", "%" & remarks & "%", d1, d2)

                End Select
            End If

            If Me.dtCDExamination.IsEmpty = False Then
                Select Case SearchSetting
                    Case 0
                        Me.CDRegisterTableAdapter.FillByDESelectedYear(FingerPrintDataSet.CDRegister, sNumber & "%", officer & "%", court & "%", ccno & "%", ps & "%", crno & "%", details & "%", remarks & "%", Me.dtCDExamination.ValueObject, d1, d2)
                    Case 1
                        Me.CDRegisterTableAdapter.FillByDESelectedYear(FingerPrintDataSet.CDRegister, sNumber, officer, court, ccno, ps, crno, details, remarks, Me.dtCDExamination.ValueObject, d1, d2)
                    Case 2
                        Me.CDRegisterTableAdapter.FillByDESelectedYear(FingerPrintDataSet.CDRegister, "%" & sNumber & "%", "%" & officer & "%", "%" & court & "%", "%" & ccno & "%", "%" & ps & "%", "%" & crno & "%", "%" & details & "%", "%" & remarks & "%", Me.dtCDExamination.ValueObject, d1, d2)
                End Select
            End If



            DisplayDatabaseInformation()
            ShowDesktopAlert("Search finished. Found " & IIf(Me.CDDataGrid.RowCount < 2, Me.CDDataGrid.RowCount & " Record", Me.CDDataGrid.RowCount & " Records"))
            Application.DoEvents()
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "OVERWRITE CD REGISTER"
    Private Sub OverWriteCDData()
        Try

            Dim NewCDNumber As String = Trim(Me.txtCDNumber.Text)
            GenerateCDNumberWithoutYear(NewCDNumber)
            Dim CDN = Me.txtCDNumberOnly.Text
            Dim ps = Trim(Me.cmbCDPoliceStation.Text)
            Dim crno = Trim(Me.txtCDCrNo.Text)
            Dim officer = Trim(Me.cmbCDOfficer.Text)
            Dim court = Trim(Me.txtCourt.Text)
            Dim ccno = Trim(Me.txtCCNumber.Text)
            Dim details = Trim(Me.txtCDDetails.Text)
            Dim remarks = Trim(Me.txtCDRemarks.Text)

            Dim oldRow As FingerPrintDataSet.CDRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet.CDRegister.FindByCDNumberWithYear(OriginalCDNumber)
            If oldRow IsNot Nothing Then
                With oldRow
                    .CDNumberWithYear = NewCDNumber
                    .CDNumberWithoutYear = CDN
                    .DateOfExamination = Me.dtCDExamination.ValueObject
                    .NameOfOfficer = officer
                    .Court = court
                    .CCNumber = ccno
                    .PoliceStation = ps
                    .CrNumber = crno
                    .Details = details
                    .Remarks = remarks
                End With
            End If
            Me.CDRegisterBindingSource.Position = Me.CDRegisterBindingSource.Find("CDNumberWithYear", NewCDNumber)

            Me.CDRegisterTableAdapter.UpdateQuery(NewCDNumber, CDN, Me.dtCDExamination.ValueObject, officer, court, ccno, ps, crno, details, remarks, OriginalCDNumber)

            ShowDesktopAlert("CD Record over writed!")

            InitializeCDFields()
            ' IncrementCDNumber(NewCDNumber)
            GenerateNewCDNumber()
            Me.dtCDExamination.Value = Today
            Me.btnSaveCD.Text = "Save"
            CDEditMode = False
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Court Duty Record No. " & OriginalCDNumber)
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.CDRegisterBindingSource.Position = Me.CDRegisterBindingSource.Find("CDNumberWithYear", OriginalDANumber)
        End Try

    End Sub
#End Region


    '---------------------------------------------PS DATA MANIPULATION-----------------------------------------
#Region "PS DATAENTRY FIELD SETTINGS"
    Private Sub ClearPSFields()
        On Error Resume Next

        Me.txtPSName.Focus()
        Me.txtPSName.Clear()
        Me.txtPhoneNumber1.Clear()
        Me.txtPhoneNumber2.Clear()
        Me.txtDistance.Text = vbNullString
        Me.cmbSHO.Text = ""
    End Sub

    Private Sub ClearSelectedPSFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPSName.ButtonCustomClick, txtPhoneNumber1.ButtonCustomClick, txtPhoneNumber2.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString


    End Sub
#End Region


#Region "PS SAVE BUTTON ACTION"

    Private Sub PSSaveButtonAction() Handles btnSavePS.Click
        On Error Resume Next
        If PSEditMode Then
            UpdatePSData()
        Else
            SaveNewPSEntry()
        End If
    End Sub
#End Region


#Region "PS NEW DATA ENTRY"


    Private Sub SaveNewPSEntry()
       Try

        OriginalPSName = Trim(Me.txtPSName.Text)
        Dim Ph1 As String = Trim(Me.txtPhoneNumber1.Text)
        Dim Ph2 As String = Trim(Me.txtPhoneNumber2.Text)
            Dim Distance As String = Trim(Me.txtDistance.Text)
            Dim SHO As String = Trim(cmbSHO.Text).ToUpper

        If Distance <> "" Then Distance = Distance & " km"
        If OriginalPSName = vbNullString Then
            MessageBoxEx.Show("Please enter the Police Station Name!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPSName.Focus()
            Exit Sub
        End If


        If PSNameExists(OriginalPSName) Then
            Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("A record for the police station name '" & OriginalPSName & "' already exists. Do you want to over write it with the new data?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If r = Windows.Forms.DialogResult.Yes Then
                OverWritePSData()
            Else
                Me.txtPSName.Focus()
                Me.txtPSName.SelectAll()
            End If
            Exit Sub
        End If


            Dim newRow As FingerPrintDataSet.PoliceStationListRow 'add a new row to insert values

            newRow = Me.FingerPrintDataSet.PoliceStationList.NewPoliceStationListRow()

            With newRow
                .PoliceStation = OriginalPSName
                .PhoneNumber1 = Ph1
                .PhoneNumber2 = Ph2
                .DistanceKM = Distance
                .SHO = SHO
            End With

        Me.FingerPrintDataSet.PoliceStationList.Rows.Add(newRow) ' add the row to the table
        Me.PSRegisterBindingSource.Position = Me.PSRegisterBindingSource.Find("PoliceStation", OriginalPSName)


            Me.PSRegisterTableAdapter.Insert(OriginalPSName, Ph1, Ph2, Distance, SHO) 'update the database
        ShowDesktopAlert("New Police Station Name entered successfully!")


        ClearPSFields()


            PSListChanged = True
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Added new Police Station " & OriginalPSName)
       Catch ex As Exception
            ShowErrorMessage(ex)
             If Not blApplicationIsLoading  And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function PSNameExists(ByVal PSName As String)
        On Error Resume Next
        If Me.PSRegisterTableAdapter.CheckPoliceStationExists(PSName) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "PS EDIT RECORD"

    Private Sub UpdatePSData()

       Try

        If Trim(Me.txtPSName.Text) = vbNullString Then
            MessageBoxEx.Show("Please enter the Police Station Name!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPSName.Focus()
            Exit Sub
        End If

        Dim NewPSName As String = Trim(Me.txtPSName.Text)
        Dim Ph1 As String = Trim(Me.txtPhoneNumber1.Text)
        Dim Ph2 As String = Trim(Me.txtPhoneNumber2.Text)
            Dim Distance As String = Trim(Me.txtDistance.Text)
            Dim SHO As String = Trim(cmbSHO.Text)

        If Distance <> "" Then Distance = Distance & " km"
        If LCase(NewPSName) <> LCase(OriginalPSName) Then
            If PSNameExists(NewPSName) Then
                DevComponents.DotNetBar.MessageBoxEx.Show("A record for the police station name '" & NewPSName & "' already exists. Please enter a different police station name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPSName.Focus()
                Me.txtPSName.SelectAll()
                Exit Sub
            End If
        End If


        Dim oldRow As FingerPrintDataSet.PoliceStationListRow 'add a new row to insert values
        oldRow = Me.FingerPrintDataSet.PoliceStationList.FindByPoliceStation(OriginalPSName)
        If oldRow IsNot Nothing Then
            With oldRow
                .PoliceStation = NewPSName
                .PhoneNumber1 = Ph1
                .PhoneNumber2 = Ph2
                    .DistanceKM = Distance
                    .SHO = SHO
            End With
        End If
        Me.PSRegisterBindingSource.Position = Me.PSRegisterBindingSource.Find("PoliceStation", NewPSName)



            Me.PSRegisterTableAdapter.UpdateQuery(NewPSName, Distance, Ph1, Ph2, SHO, OriginalPSName) 'update the database

        If LCase(NewPSName) <> LCase(OriginalPSName) Then



            Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you want to update the Police Station Name in SOC Register, DA Register, Identified Slips Register, Active Criminals Register and Court Duty Register also?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If r = Windows.Forms.DialogResult.Yes Then
                Dim c1 As Long = Me.SOCRegisterTableAdapter.CountPS(OriginalPSName)
                Dim c2 As Long = Me.DARegisterTableAdapter.CountPS(OriginalPSName)
                Dim c3 As Long = Me.IDRegisterTableAdapter.CountPS(OriginalPSName)
                Dim c4 As Long = Me.ACRegisterTableAdapter.CountPS(OriginalPSName)
                Dim c5 As Long = Me.CDRegisterTableAdapter.CountPS(OriginalPSName)

                Me.SOCRegisterTableAdapter.UpdatePoliceStationName(NewPSName, OriginalPSName)
                Me.DARegisterTableAdapter.UpdatePoliceStationName(NewPSName, OriginalPSName)
                Me.CDRegisterTableAdapter.UpdatePoliceStationName(NewPSName, OriginalPSName)
                Me.IDRegisterTableAdapter.UpdatePoliceStationName(NewPSName, OriginalPSName)
                Me.ACRegisterTableAdapter.UpdatePoliceStationName(NewPSName, OriginalPSName)


                LoadSOCRecords()
                LoadDARecords()
                LoadCDRecords()
                LoadIDRecords()
                LoadACRecords()


                DevComponents.DotNetBar.MessageBoxEx.Show("Police Station Name updated successfully!" & vbNewLine & vbNewLine & c1 & IIf(c1 <= 1, " record ", " records ") & "updated in SOC Register." & vbNewLine & c2 & IIf(c2 <= 1, " record ", " records ") & "updated in DA Register." & vbNewLine & c3 & IIf(c3 <= 1, " record ", " records ") & "updated in Identified Slips Register." & vbNewLine & c4 & IIf(c4 <= 1, " record ", " records ") & "updated in Active Criminals Register." & vbNewLine & c5 & IIf(c5 <= 1, " record ", " records ") & "updated in CD Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ShowDesktopAlert("Police Station Name updated successfully!")
            End If
        Else
            ShowDesktopAlert("Police Station details updated successfully!")
        End If

            NewDataMode()
            PSListChanged = True
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Police Station " & OriginalPSName)

        Catch ex As Exception
            ShowErrorMessage(ex)
             If Not blApplicationIsLoading  And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.PSRegisterBindingSource.Position = Me.PSRegisterBindingSource.Find("PoliceStation", OriginalPSName)
        End Try

    End Sub
#End Region


#Region "Overwrite PS Data"
    Private Sub OverWritePSData()
       Try

        Dim NewPSName As String = Trim(Me.txtPSName.Text)
        Dim Ph1 As String = Trim(Me.txtPhoneNumber1.Text)
        Dim Ph2 As String = Trim(Me.txtPhoneNumber2.Text)
            Dim Distance As String = Trim(Me.txtDistance.Text)
            Dim SHO As String = Trim(cmbSHO.Text)
        If Distance <> "" Then Distance = Distance & " km"


        Dim oldRow As FingerPrintDataSet.PoliceStationListRow 'add a new row to insert values
        oldRow = Me.FingerPrintDataSet.PoliceStationList.FindByPoliceStation(OriginalPSName)
        If oldRow IsNot Nothing Then
            With oldRow
                .PoliceStation = NewPSName
                .PhoneNumber1 = Ph1
                .PhoneNumber2 = Ph2
                    .DistanceKM = Distance
                    .SHO = SHO
            End With
        End If
        Me.PSRegisterBindingSource.Position = Me.PSRegisterBindingSource.Find("PoliceStation", NewPSName)



            Me.PSRegisterTableAdapter.UpdateQuery(NewPSName, Distance, Ph1, Ph2, SHO, OriginalPSName) 'update the database

        ShowDesktopAlert("Police Station Name overwrited successfully!")
        NewDataMode()
        PSListChanged = True
            DisplayDatabaseInformation()
            InsertOrUpdateLastModificationDate(Now, "Edited Police Station " & OriginalPSName)
       
         Catch ex As Exception
            ShowErrorMessage(ex)
             If Not blApplicationIsLoading  And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Me.PSRegisterBindingSource.Position = Me.PSRegisterBindingSource.Find("PoliceStation", OriginalPSName)
        End Try


    End Sub
#End Region


    '-------------------------------------------OFFICE SETTINGS-----------------------------------------
#Region "SAVE OFFICE SETTINGS"

    Public Sub OfficeSettingsEditMode(allow As Boolean)
        Me.txtFullDistrict.Enabled = allow
        Me.txtShortDistrict.Enabled = allow
        Me.txtFullOffice.Enabled = allow
        Me.txtShortOffice.Enabled = allow
        Me.txtAttendance.Enabled = allow
        Me.txtIndividualPerformance.Enabled = allow
        Me.txtRBWarrant.Enabled = allow
        Me.txtSOCDAStatement.Enabled = allow
        Me.txtTABill.Enabled = allow
        Me.txtFPAttestation.Enabled = allow
        Me.txtGraveCrime.Enabled = allow
        Me.txtVigilanceCase.Enabled = allow
        Me.txtWeeklyDiary.Enabled = allow
        Me.txtIdentificationPdlNumber.Enabled = allow
        Me.txtMonthlyStatements.Enabled = allow
        Me.txtBuildingsProject.Enabled = allow
    End Sub
    Private Sub SaveOfficeSettings() Handles btnSaveOfficeSettings.Click
        Try
            If Me.txtFullDistrict.Enabled = False Then
                MessageBoxEx.Show("Please press 'Edit' or 'Open' button to edit, then press 'Save'", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            FullDistrictName = Me.txtFullDistrict.Text.Trim
            FullOfficeName = Me.txtFullOffice.Text.Trim
            ShortDistrictName = Me.txtShortDistrict.Text.Trim
            ShortOfficeName = Me.txtShortOffice.Text.Trim
            PdlAttendance = IIf(Me.txtAttendance.Text.Trim = "", "   ", Me.txtAttendance.Text.Trim)
            PdlIndividualPerformance = IIf(Me.txtIndividualPerformance.Text.Trim = "", "   ", Me.txtIndividualPerformance.Text.Trim)
            PdlRBWarrant = IIf(Me.txtRBWarrant.Text.Trim = "", "   ", Me.txtRBWarrant.Text.Trim)
            PdlSOCDAStatement = IIf(Me.txtSOCDAStatement.Text.Trim = "", "   ", Me.txtSOCDAStatement.Text.Trim)
            PdlTABill = IIf(Me.txtTABill.Text.Trim = "", "   ", Me.txtTABill.Text.Trim)
            PdlFPAttestation = IIf(Me.txtFPAttestation.Text.Trim = "", "   ", Me.txtFPAttestation.Text.Trim)
            PdlGraveCrime = IIf(Me.txtGraveCrime.Text.Trim = "", "   ", Me.txtGraveCrime.Text.Trim)
            PdlVigilanceCase = IIf(Me.txtVigilanceCase.Text.Trim = "", "   ", Me.txtVigilanceCase.Text.Trim)
            PdlWeeklyDiary = IIf(Me.txtWeeklyDiary.Text.Trim = "", "   ", Me.txtWeeklyDiary.Text.Trim)
            PdlIdentificationStatement = IIf(Me.txtIdentificationPdlNumber.Text.Trim = "", "   ", Me.txtIdentificationPdlNumber.Text.Trim)
            PdlMonthlyStatements = IIf(Me.txtMonthlyStatements.Text.Trim = "", "   ", Me.txtMonthlyStatements.Text.Trim)
            PdlBuildingProject = IIf(Me.txtBuildingsProject.Text.Trim = "", "   ", Me.txtBuildingsProject.Text.Trim)

            Me.SettingsTableAdapter1.Fill(Me.FingerPrintDataSet1.Settings)
            Dim count = Me.FingerPrintDataSet1.Settings.Count
            Dim id = 1

            If count = 0 Then
                Me.SettingsTableAdapter1.Insert(id, FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary)
            Else
                Me.SettingsTableAdapter1.UpdateQuery(FullDistrictName, ShortDistrictName, FullOfficeName, ShortOfficeName, FPImageImportLocation, CPImageImportLocation, PdlAttendance, PdlIndividualPerformance, PdlRBWarrant, PdlSOCDAStatement, PdlTABill, PdlFPAttestation, PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary, id)
            End If

            Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet1.CommonSettings, "PdlIdentificationStatement")
            If Me.FingerPrintDataSet1.CommonSettings.Count = 0 Then
                Me.CommonSettingsTableAdapter1.Insert("PdlIdentificationStatement", PdlIdentificationStatement, "")
            Else
                Me.CommonSettingsTableAdapter1.UpdateQuery(PdlIdentificationStatement, "", "PdlIdentificationStatement")
            End If

            Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet1.CommonSettings, "PdlMonthlyStatements")
            If Me.FingerPrintDataSet1.CommonSettings.Count = 0 Then
                Me.CommonSettingsTableAdapter1.Insert("PdlMonthlyStatements", PdlMonthlyStatements, "")
            Else
                Me.CommonSettingsTableAdapter1.UpdateQuery(PdlMonthlyStatements, "", "PdlMonthlyStatements")
            End If

            Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet1.CommonSettings, "PdlBuildingProject")
            If Me.FingerPrintDataSet1.CommonSettings.Count = 0 Then
                Me.CommonSettingsTableAdapter1.Insert("PdlBuildingProject", PdlBuildingProject, "")
            Else
                Me.CommonSettingsTableAdapter1.UpdateQuery(PdlBuildingProject, "", "PdlBuildingProject")
            End If

            For Each ctrl As Control In Me.GroupPanel4.Controls 'clear all textboxes
                If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                    ctrl.Text = ctrl.Text.Trim
                End If
            Next

            For Each ctrl As Control In Me.GroupPanel5.Controls 'clear all textboxes
                If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                    ctrl.Text = ctrl.Text.Trim
                End If
            Next

            SetWindowTitle()

            ShowDesktopAlert("Office Settings updated!")
            OfficeSettingsEditMode(False)
            InsertOrUpdateLastModificationDate(Now, "Edited Office Settings")
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region


#Region "DATABASE LOCATION"
    Private Sub ChangeDatabaseLocation(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeDBFolder.Click
        Try
            If blDBIsInPreviewMode Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot change Database Folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim StartFolder As String
            strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

            If ValidPath(strDatabaseFile) Then
                StartFolder = My.Computer.FileSystem.GetParentPath(strDatabaseFile)
            Else
                StartFolder = SuggestedLocation & "\Database"
            End If

            Dim OldDBFile As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\BXSofts\" & strAppName & "\Database\Fingerprint.mdb"

            If FileIO.FileSystem.FileExists(strDatabaseFile) = False Then
                strDatabaseFile = OldDBFile
            End If

            Me.FolderBrowserDialog1.ShowNewFolderButton = True
            Me.FolderBrowserDialog1.Description = "Select location for Database"
            Me.FolderBrowserDialog1.SelectedPath = StartFolder
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

                SelectedPath = SelectedPath.Replace("\\", "\")


                Dim olddbfolder = My.Computer.FileSystem.GetParentPath(strDatabaseFile)
                If olddbfolder <> SelectedPath Then
                    If FileIO.FileSystem.FileExists(SelectedPath & "\Fingerprint.mdb") Then
                        Dim reply As DialogResult = MessageBoxEx.Show("Database already exists in selected folder. Click 'Yes' to overwrite or 'No' to exit without change.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                        If reply = Windows.Forms.DialogResult.No Then
                            ShowDesktopAlert("Database Folder not changed")
                            Exit Sub
                        End If
                    End If

                    My.Computer.FileSystem.CreateDirectory(SelectedPath)
                    My.Computer.FileSystem.CopyFile(strDatabaseFile, SelectedPath & "\Fingerprint.mdb", True)
                    strDatabaseFile = SelectedPath & "\Fingerprint.mdb"
                    Application.DoEvents()
                    ShowDesktopAlert("Database Folder changed")
                    My.Computer.Registry.SetValue(strGeneralSettingsPath, "DatabaseFile", strDatabaseFile, Microsoft.Win32.RegistryValueKind.String)
                    ReloadDataAfterSettingsWizardClose()

                End If

            End If


        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try


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

#End Region

   
    '---------------------------------------------REPORTS-----------------------------------
#Region "REPORTS"

    Private Sub ShowFacingSheetOnDatagridCellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SOCDatagrid.CellDoubleClick
        On Error Resume Next
        If e.RowIndex < 0 Or e.RowIndex > Me.SOCDatagrid.Rows.Count - 1 Then
            Exit Sub
        End If
        If MessageBoxEx.Show("Generate Facing Sheet for SoC No. " & Me.SOCDatagrid.SelectedCells(0).Value & " ?", strAppName, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            ShowSOCFacingSheet()
        End If

    End Sub
    Private Sub ShowSOCFacingSheet() Handles btnSOCFacingSheet.Click, btnFacingSheet.Click, btnFacingSheetContext.Click, btnFacingSheetMenu.Click

        If (Me.SOCDatagrid.RowCount = 0) Or (Me.SOCDatagrid.SelectedRows.Count = 0) Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No record is selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim TemplateFile = strAppUserPath & "\WordTemplates\FacingSheet.docx"
        If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
            MessageBoxEx.Show("File missing. Please re-install the Application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ShowPleaseWaitForm()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)

            wdDoc.Range.NoProofing = 1
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

            Dim FileNo As String = Me.SOCDatagrid.SelectedCells(0).Value
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            wdBooks("FileNo").Range.Text = "No. " & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName
            wdBooks("Unit").Range.Text = FullDistrictName.ToUpper
            wdBooks("PS").Range.Text = Me.SOCDatagrid.SelectedCells(5).Value.ToString
            wdBooks("CrNo").Range.Text = Me.SOCDatagrid.SelectedCells(6).Value.ToString & " u/s " & Me.SOCDatagrid.SelectedCells(7).Value.ToString
            wdBooks("DI").Range.Text = Me.SOCDatagrid.SelectedCells(2).FormattedValue
            wdBooks("DR").Range.Text = Me.SOCDatagrid.SelectedCells(3).FormattedValue
            wdBooks("DO").Range.Text = Me.SOCDatagrid.SelectedCells(4).Value.ToString
            wdBooks("PO").Range.Text = Me.SOCDatagrid.SelectedCells(8).Value.ToString & vbNewLine
            wdBooks("NC").Range.Text = Me.SOCDatagrid.SelectedCells(15).Value.ToString
            wdBooks("MO").Range.Text = Me.SOCDatagrid.SelectedCells(16).Value.ToString
            wdBooks("PL").Range.Text = Me.SOCDatagrid.SelectedCells(17).Value.ToString

            Dim cpdeveloped As Integer = Me.SOCDatagrid.SelectedCells(10).Value
            Dim cpd As String = ""

            If cpdeveloped = 0 Then cpd = "Nil Print"
            If cpdeveloped = 1 Then cpd = "One Print"
            If cpdeveloped > 1 Then cpd = cpdeveloped & " Prints"
            Dim cpdetails As String = Me.SOCDatagrid.SelectedCells(14).Value.ToString

            wdBooks("CP").Range.Text = cpd & IIf(cpdetails <> "", vbNewLine & Me.SOCDatagrid.SelectedCells(14).Value.ToString, "")
            wdBooks("Photographer").Range.Text = Me.SOCDatagrid.SelectedCells(18).Value.ToString
            '  wdBooks("Remarks").Range.Text = Me.SOCDatagrid.SelectedCells(22).Value.ToString

            Dim Officer = Me.SOCDatagrid.SelectedCells(9).Value.ToString()

            Officer = Replace(Replace(Replace(Officer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector")
            Officer = Officer.Replace("; ", vbNewLine)
            wdBooks("IO").Range.Text = Officer

            ClosePleaseWaitForm()

            Dim sFileName As String = FileIO.SpecialDirectories.MyDocuments & "\FacingSheet.docx"
            If Not FileInUse(sFileName) Then wdDoc.SaveAs(sFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()


            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing


            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub ShowSOCStatement() Handles btnSOCStatement.Click, btnMonthlySOC.Click
        frmSOCStatement.ShowDialog()
    End Sub

    Private Sub ShowGraveCrimeReport() Handles btnGraveCrimeReport.Click, btnGraveCrimeStatement.Click
        On Error Resume Next
        FrmSOCGraveCrimes.ShowDialog()
        FrmSOCGraveCrimes.BringToFront()
    End Sub

    Private Sub ShowSOCReportStatement() Handles btnStatementofSOCReports.Click

        On Error Resume Next
        FrmSOCReportStatement.Show()
        FrmSOCReportStatement.WindowState = FormWindowState.Maximized
        FrmSOCReportStatement.BringToFront()
    End Sub

    Private Sub ShowConciseSOCStatement() Handles btnConciseSOCStatement.Click
        On Error Resume Next
        frmConciseSOCReport.Show()
        frmConciseSOCReport.WindowState = FormWindowState.Maximized
        frmConciseSOCReport.BringToFront()
    End Sub

    Private Sub ShowSOCStatistics() Handles btnSOCStatistics.Click
        On Error Resume Next
        frmSOCStatistics.Show()
        frmSOCStatistics.WindowState = FormWindowState.Maximized
        frmSOCStatistics.BringToFront()
    End Sub

    Private Sub ShowMonthWiseSOCStatistics() Handles btnMonthWiseSOCPerformance.Click
        On Error Resume Next
        FrmMonthWiseSOCStatistics.Show()
        FrmMonthWiseSOCStatistics.WindowState = FormWindowState.Maximized
        FrmMonthWiseSOCStatistics.BringToFront()
    End Sub

    Public Sub GenerateSOCreport() Handles btnSOCReport.Click, btnSOCReportContext.Click, btnSOCReport2.Click
        On Error Resume Next
        If (Me.SOCDatagrid.RowCount = 0) Or (Me.SOCDatagrid.SelectedRows.Count = 0) Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No record is selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub
        End If

        ReportNature = ""
        ReportSentDate = Today
        ReportSentTo = ""
        boolRSOCButtonClicked = False
        FrmSOCReportSentStatus.TitleText = "<b>Generate SOC Report</b>"
        FrmSOCReportSentStatus.Text = "Generate SOC Report"
        FrmSOCReportSentStatus.ShowDialog()
        If boolCancelSOCReport Or boolRSOCButtonClicked = False Then Exit Sub

        ReportSentTo = ReportSentTo.Replace(vbNewLine, vbNewLine & vbTab)


        ShowSOCReport()


        If boolSaveSOCReport = True Then
            ReportSentTo = ReportSentTo.Replace(vbNewLine & vbTab, vbNewLine)
            SaveSOCReportOnGeneratingReport()
        End If
    End Sub

    Sub ShowSOCReport()
        Try

            If ReportNature = "Forward Photographs" Then
                ForwardPhotograph()
                Exit Sub
            End If

            If ReportNature = "Suspect Comparison" Then
                SuspectComparison()
                Exit Sub
            End If

            If ReportNature = "Inmates Print - Request" Then
                InmateRequest()
                Exit Sub
            End If

            If ReportNature = "Inmates Print - Reminder" Then
                InmateReminder()
                Exit Sub
            End If

            ShowPleaseWaitForm()

            Me.Cursor = Cursors.WaitCursor
            Dim BodyText As String = vbNullString
            Dim InspectingOfficer As String = Me.SOCDatagrid.SelectedCells(9).Value.ToString().Replace(vbNewLine, "; ")


            Dim splitname() = Strings.Split(InspectingOfficer, "; ")
            InspectingOfficer = ""
            Dim u = splitname.GetUpperBound(0)
            For j = 0 To u
                If u = 0 Then
                    InspectingOfficer = GetSalutation(splitname(0))
                    Exit For
                End If

                If j = u - 1 Then
                    InspectingOfficer += GetSalutation(splitname(j)) + " and "
                ElseIf j = u Then
                    InspectingOfficer += GetSalutation(splitname(j))
                Else
                    InspectingOfficer += GetSalutation(splitname(j)) + ", "
                End If

            Next

            InspectingOfficer = Replace(Replace(Replace(Replace(InspectingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")

            Dim dti As String = Me.SOCDatagrid.SelectedCells(2).FormattedValue.ToString()
            Dim CPD As Integer = CInt(Me.SOCDatagrid.SelectedCells(10).Value.ToString)
            Dim CPU As Integer = CInt(Me.SOCDatagrid.SelectedCells(11).Value.ToString)
            Dim CPE As Integer = CInt(Me.SOCDatagrid.SelectedCells(12).Value.ToString)
            Dim CPR As Integer = CPD - CPU - CPE


            Dim PO As String = Trim(Me.SOCDatagrid.SelectedCells(8).Value.ToString)

            If PO <> "" Then
                PO = "(" & PO.Replace(vbNewLine, ", ") & ") "
            End If

            Dim Photographer As String = SOCDatagrid.SelectedCells(18).Value.ToString

            If Trim(Photographer) = "" Or Trim(Photographer).ToLower = "no photographer" Then
                Photographer = ""
            Else
                If CPD = 1 Then
                    Photographer = "The chance print was photographed by Sri. " & Photographer & ", Police Photographer"
                Else
                    Photographer = "The chance prints were photographed by Sri. " & Photographer & ", Police Photographer"
                End If
                Dim dtphotographed = Me.SOCDatagrid.SelectedCells(20).Value.ToString
                If dtphotographed <> vbNullString Then
                    If dti = dtphotographed Then
                        dtphotographed = " on the same day."
                    Else
                        dtphotographed = " on " & dtphotographed & "."
                    End If
                Else
                    dtphotographed = "."
                End If

                Photographer += dtphotographed
            End If

            Dim ps As String = Me.SOCDatagrid.SelectedCells(5).Value.ToString
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim cr = Me.SOCDatagrid.SelectedCells(6).Value.ToString()
            Dim us = Me.SOCDatagrid.SelectedCells(7).Value.ToString()


            Dim TextToBold As String = ""
            BodyText = vbTab & "The Scene of Crime " & PO & "in the case cited above was inspected by " & InspectingOfficer & " of this unit on " & dti

            '--------------------------NIL PRINT---------------------

            If CPD = 0 Then 'nil print
                BodyText = BodyText & "." & vbNewLine & vbTab & "It is intimated that no useful chance prints were obtained from the scene of crime. Hence no further action is pending with this office in this regard. This is for information."
                TextToBold = "no useful chance prints"
            End If

            '--------------------------ONE PRINT---------------------

            If CPD = 1 Then 'one print
                BodyText = BodyText & " and developed one chance print. " & Photographer

                If CPU = 1 Then
                    BodyText = BodyText & vbNewLine & vbTab & "It is intimated that the chance print is unfit for comparison. Hence no further action is pending with this office in this regard. This is for information."
                    TextToBold = "the chance print is unfit for comparison"
                End If

                If CPE = 1 Then
                    BodyText = BodyText & vbNewLine & vbTab & "It is intimated that the chance print is eliminated as the finger impression of the inmate. Hence no further action is pending with this office in this regard. This is for information."
                    TextToBold = "the chance print is eliminated"
                End If

                If CPR = 1 Then
                    BodyText = BodyText & vbNewLine & vbTab & "The chance print is under comparison and the result of comparison will be intimated at the earliest."

                End If

            End If

            '--------------------------MORE THAN ONE PRINT---------------------



            If CPD > 1 And CPU = CPD And CPR = 0 Then 'all unfit
                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer & vbNewLine & vbTab & "It is intimated that all the chance prints are unfit for comparison. Hence no further action is pending with this office in this regard. This is for information."
                TextToBold = "all the chance prints are unfit for comparison"
            End If

            If CPD > 1 And CPE = CPD And CPR = 0 Then 'all eliminated
                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer & vbNewLine & vbTab & "It is intimated that all the chance prints are eliminated as the finger impressions of inmates. Hence no further action is pending with this office in this regard. This is for information."
                TextToBold = "all the chance prints are eliminated"
            End If

            If CPD > 1 And CPU <> CPD And CPE <> CPD And CPR = 0 Then 'all eliminated or unfit
                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer & vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison and the remaining " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate", " chance prints are eliminated as the finger impressions of inmates") & ". Hence no further action is pending with this office in this regard. This is for information."
            End If


            If CPD > 1 And CPR = CPD Then 'all remain
                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer

                If ReportNature = "Preliminary" Then
                    BodyText = BodyText & vbNewLine & vbTab & "The chance prints are under comparison and the result of comparison will be intimated at the earliest."
                End If
            End If

            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPE <> 0 And CPU = 0 Then 'some remains

                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer & vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate", " chance prints are eliminated as the finger impressions of inmates") & ". The remaining " & ConvertNumberToWord(CPR) & IIf(CPR = 1, " chance print is ", " chance prints are ") & "under comparison and the result of comparison will be intimated at the earliest. This is for information."
            End If

            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPU <> 0 And CPE = 0 Then 'some remains

                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer & vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison. The remaining " & ConvertNumberToWord(CPR) & IIf(CPR = 1, " chance print is ", " chance prints are ") & "under comparison and the result of comparison will be intimated at the earliest. This is for information."

            End If


            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPU <> 0 And CPE <> 0 And CPU <> 0 Then 'some remains

                BodyText = BodyText & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer & vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison and " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate", " chance prints are eliminated as the finger impressions of inmates") & ". The remaining " & ConvertNumberToWord(CPR) & IIf(CPR = 1, " chance print is ", " chance prints are ") & "under comparison and the result of comparison will be intimated at the earliest. This is for information."
            End If

            If CPD = 1 Then
                Select Case ReportNature.ToLower
                    Case "awaiting photographs"
                        BodyText = vbTab & "The Scene of Crime " & PO & "in the case cited above was inspected by " & InspectingOfficer & " of this unit on " & dti & " and developed one chance print. " & Photographer
                        BodyText = BodyText & vbNewLine & vbTab & "The comparison work of the chance print will be initiated on receipt of photographs of chance print from the Police Photographer and result of comparison will be intimated to you. This is for information."

                    Case "interim"
                        BodyText = "The chance print in the above case was compared with the fingerprint slips of Active Criminals / MO Criminals recorded in this bureau and remains untraced. Further comparison is being continued in the DA Slips collection and the result will be intimated as and when search is completed. This is for information."
                    Case "untraced"
                        BodyText = "The chance print developed from the scene of crime concerned in the above case remains untraced in the available records of this bureau. This is for information."
                End Select
            Else
                Select Case ReportNature.ToLower
                    Case "awaiting photographs"
                        BodyText = vbTab & "The Scene of Crime " & PO & "in the case cited above was inspected by " & InspectingOfficer & " of this unit on " & dti & " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer
                        BodyText = BodyText & vbNewLine & vbTab & "The comparison work of chance prints will be initiated on receipt of photographs of chance prints from the Police Photographer and result of comparison will be intimated to you. This is for information."

                    Case "interim"
                        BodyText = "The chance prints in the above case were compared with the fingerprint slips of Active Criminals / MO Criminals recorded in this bureau and remain untraced. Further comparison is being continued in the DA Slips collection and the result will be intimated as and when search is completed. This is for information."
                    Case "untraced"
                        BodyText = "The chance prints developed from the scene of crime concerned in the above case remain untraced in the available records of this bureau. This is for information."
                End Select
            End If


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1
            Dim FileNo As String = Me.SOCDatagrid.SelectedCells(0).Value.ToString()

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            WordApp.Selection.TypeText("No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)


            WordApp.Selection.TypeText(vbTab & IIf(ReportSentTo.StartsWith("The "), ReportSentTo, "The " & ReportSentTo))
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)


            Select Case ReportNature.ToLower

                Case "interim"
                    WordApp.Selection.TypeText(vbTab & "Sub: Inspection of Scene of Crime - Interim report forwarding of - reg.")
                Case "untraced"
                    WordApp.Selection.TypeText(vbTab & "Sub: Inspection of Scene of Crime - Untraced report forwarding of - reg.")
                Case Else
                    WordApp.Selection.TypeText(vbTab & "Sub: Inspection of Scene of Crime - report forwarding of - reg.")
            End Select


            WordApp.Selection.TypeText(vbNewLine)


            Me.SocReportRegisterTableAdapter1.FillBySOCNumber(Me.FingerPrintDataSet1.SOCReportRegister, Me.SOCDatagrid.SelectedCells(0).Value.ToString())
            Dim dated As String = ""
            Dim count = Me.FingerPrintDataSet1.SOCReportRegister.Count
            If count > 0 Then
                For c = 0 To count - 1
                    dated = dated & Format(Me.FingerPrintDataSet1.SOCReportRegister(c).DateOfReportSent, "dd/MM/yyyy")
                    If c = count - 2 Then
                        dated = dated & " and "

                    End If
                    If c < count - 2 Then
                        dated = dated & ", "

                    End If
                Next
            End If
            If dated = "" Then dated = "...................."

            Select Case ReportNature.ToLower

                Case "interim"
                    WordApp.Selection.TypeText(vbTab & "Ref:  1. Cr.No. " & cr & " u/s " & us & " of " & ps)
                    WordApp.Selection.TypeText(vbNewLine & vbTab & "         2. This office letter of even no. dated " & dated)
                Case "untraced"
                    WordApp.Selection.TypeText(vbTab & "Ref:  1. Cr.No. " & cr & " u/s " & us & " of " & ps)
                    WordApp.Selection.TypeText(vbNewLine & vbTab & "         2. This office letter of even no. dated " & dated)
                Case Else
                    WordApp.Selection.TypeText(vbTab & "Ref: Cr.No. " & cr & " u/s " & us & " of " & ps)
            End Select

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            Select Case ReportNature.ToLower

                Case "interim"
                    WordApp.Selection.TypeText(vbTab & "Refer this office letter cited 2")
                    WordApp.Selection.Font.Superscript = 1
                    WordApp.Selection.TypeText("nd")
                    WordApp.Selection.Font.Superscript = 0
                    WordApp.Selection.TypeText(". ")
                Case "untraced"
                    WordApp.Selection.TypeText(vbTab & "Refer this office letter cited 2")
                    WordApp.Selection.Font.Superscript = 1
                    WordApp.Selection.TypeText("nd")
                    WordApp.Selection.Font.Superscript = 0
                    WordApp.Selection.TypeText(". ")
            End Select

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14
            WordApp.Selection.TypeText(BodyText.Replace("..", "."))

            If TextToBold <> "" Then
                Dim rng As Word.Range = aDoc.Range
                Dim f As Boolean = rng.Find.Execute(TextToBold)
                If f Then rng.Font.Bold = 1
            End If


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            ClosePleaseWaitForm()

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub InmateRequest()
        Try
            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\InmatesRequest.docx"

            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            ShowPleaseWaitForm()

            Dim ps As String = Me.SOCDatagrid.SelectedCells(5).Value.ToString
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim sho As String = Me.PSRegisterTableAdapter.FindSHO(ps)

            If sho Is Nothing Then
                sho = "Station House Officer"
            End If


            If sho.ToUpper = "IP" Then
                sho = "The Inspector of Police"
            Else
                sho = "The Sub Inspector of Police"
            End If


            Dim cr = Me.SOCDatagrid.SelectedCells(6).Value.ToString()

            Dim FileNo As String = Me.SOCDatagrid.SelectedCells(0).Value.ToString()

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            FileNo = FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            wdBooks("Station1").Range.Text = sho.ToUpper & ", " & ps.ToUpper
            wdBooks("Unit1").Range.Text = FullDistrictName.ToUpper
            wdBooks("SOCNo1").Range.Text = FileNo & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True)
            wdBooks("CRNo1").Range.Text = cr

            wdBooks("Station2").Range.Text = sho.ToUpper & ", " & ps.ToUpper
            wdBooks("Unit2").Range.Text = ShortDistrictName.ToUpper
            wdBooks("SOCNo2").Range.Text = FileNo & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True)
            wdBooks("CRNo2").Range.Text = cr

            ClosePleaseWaitForm()

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Visible = True
            wdApp = Nothing

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub InmateReminder()

        Try
            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\InmatesReminder.docx"

            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            ShowPleaseWaitForm()

            Dim ps As String = Me.SOCDatagrid.SelectedCells(5).Value.ToString
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim sho As String = Me.PSRegisterTableAdapter.FindSHO(ps)

            If sho Is Nothing Then
                sho = "Station House Officer"
            End If


            If sho.ToUpper = "IP" Then
                sho = "The Inspector of Police"
            Else
                sho = "The Sub Inspector of Police"
            End If


            Dim cr = Me.SOCDatagrid.SelectedCells(6).Value.ToString()
            Dim us = Me.SOCDatagrid.SelectedCells(7).Value.ToString()

            Dim FileNo As String = Me.SOCDatagrid.SelectedCells(0).Value.ToString()

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            FileNo = "No: " & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            wdBooks("SOCNo").Range.Text = FileNo
            wdBooks("Unit1").Range.Text = FullDistrictName
            wdBooks("Date").Range.Text = "Date: " & GenerateDate(True)
            wdBooks("Unit2").Range.Text = FullDistrictName
            wdBooks("SHO").Range.Text = sho
            wdBooks("Station").Range.Text = ps
            wdBooks("Ref").Range.Text = "Cr.No. " & cr & " u/s " & us & " of " & ps

            ClosePleaseWaitForm()

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Visible = True
            wdApp = Nothing

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetSalutation(OfficerName As String) As String
        Try
            Dim OfficerNameOnly As String = ""
            OfficerNameOnly = OfficerName.Replace(", FPS", "")
            OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
            OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")
            Dim n As Integer = 0
            For i = 0 To 5
                If Me.IODatagrid.Rows(i).Cells(1).Value = OfficerNameOnly Then
                    Dim salutation As String = Me.IODatagrid.Rows(i).Cells(6).Value
                    If salutation Is Nothing Then
                        salutation = ""
                    End If
                    Return IIf(salutation = "", "Sri./Smt. ", salutation & " ") & OfficerName
                End If
            Next
            Return "Sri./Smt. " & OfficerName
        Catch ex As Exception
            Return "Sri./Smt. " & OfficerName
        End Try
    End Function

    Private Sub GenerateIdentificationLetterSHO() Handles btnGenerateIdentificationLetter.Click, btnGenerateIdentificationLetter2.Click

        Try
            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records selected in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                ClosePleaseWaitForm()
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("The selected SOC Number " & SOCNumber & " does not exist. Please add details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            Dim IDRFolder As String = SuggestedLocation & "\Identification Reports"
            Dim oldfilename As String = IDRFolder & "\Identification Report - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - SHO.docx"

            IDRFolder = IDRFolder & "\" & IDYear

            System.IO.Directory.CreateDirectory(IDRFolder)

            Dim sfilename As String = IDRFolder & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - SHO.docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
                Thread.Sleep(2000)
            End If


            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ShowPleaseWaitForm()


            Dim InspectingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(7).Value.ToString().Replace(vbNewLine, "; ")

            Dim splitname() = Strings.Split(InspectingOfficer, "; ")
            InspectingOfficer = ""
            Dim u = splitname.GetUpperBound(0)

            For j = 0 To u
                If u = 0 Then
                    InspectingOfficer = GetSalutation(splitname(0))
                    Exit For
                End If

                If j = u - 1 Then
                    InspectingOfficer += GetSalutation(splitname(j)) + " and "
                ElseIf j = u Then
                    InspectingOfficer += GetSalutation(splitname(j))
                Else
                    InspectingOfficer += GetSalutation(splitname(j)) + ", "
                End If

            Next

            InspectingOfficer = Replace(Replace(Replace(Replace(InspectingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")


            Dim dtid As String = Me.JoinedIDRDataGrid.SelectedCells(2).FormattedValue.ToString
            Dim dtins As String = Me.JoinedIDRDataGrid.SelectedCells(3).FormattedValue.ToString()
            Dim CPD As Integer = Val(fds.SOCRegister(0).ChancePrintsDeveloped)
            Dim CPU As Integer = Val(fds.SOCRegister(0).ChancePrintsUnfit)
            Dim CPE As Integer = Val(fds.SOCRegister(0).ChancePrintsEliminated)
            Dim CPR As Integer = CPD - CPU - CPE

            Dim PO As String = Trim(fds.SOCRegister(0).PlaceOfOccurrence)

            If PO <> "" Then
                PO = "(" & PO.Replace(vbNewLine, ", ") & ") "
            End If

            Dim Photographer As String = fds.SOCRegister(0).Photographer

            If Trim(Photographer) = "" Or Trim(Photographer).ToLower = "no photographer" Then
                Photographer = ""
            Else
                If CPD = 1 Then
                    Photographer = "The chance print was photographed by Sri. " & Photographer & ", Police Photographer"
                Else
                    Photographer = "The chance prints were photographed by Sri. " & Photographer & ", Police Photographer"
                End If
                Dim dtphotographed = fds.SOCRegister(0).DateOfReceptionOfPhoto
                If dtphotographed <> vbNullString Then
                    If dtins = dtphotographed Then
                        dtphotographed = " on the same day."
                    Else
                        dtphotographed = " on " & dtphotographed & "."
                    End If
                Else
                    dtphotographed = "."
                End If

                Photographer += dtphotographed
            End If



            Dim sho As String = Me.PSRegisterTableAdapter.FindSHO(ps)

            If sho Is Nothing Then
                sho = "Sub Inspector of Police"
            End If


            If sho.ToUpper = "IP" Then
                sho = "The Inspector of Police" & vbNewLine & vbTab & ps
            Else
                sho = "The Sub Inspector of Police" & vbNewLine & vbTab & ps
            End If

            Dim us = fds.SOCRegister(0).SectionOfLaw

            Dim identifiedfrom As String = Me.JoinedIDRDataGrid.SelectedCells(18).Value.ToString.ToLower

            Dim IdentificationText As String = ""

            IdentificationText = vbTab & "Please refer to the above. The Scene of Crime " & PO & "in the case cited above was inspected by " & InspectingOfficer & " of this unit on " & dtins

            If CPD = 1 And CPR = 1 Then 'one print
                IdentificationText += " and developed one chance print. " & Photographer

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with the fingerprint slip of the accused received from that office, "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with the fingerprint slip of the suspect received from that office, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with Bureau records, "
                End If


            End If

            '--------------------------MORE THAN ONE PRINT---------------------



            If CPD > 1 And CPR = CPD Then 'all remain
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer
                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with the fingerprint slip of the accused received from that office, "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with the fingerprint slip of the suspect received from that office, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with Bureau records, "
                End If
            End If

            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPE <> 0 And CPU = 0 Then 'some remains
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer


                IdentificationText += vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate. ", " chance prints are eliminated as the finger impressions of inmates. ")

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the accused received from that office, "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the suspect received from that office, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with Bureau records, "
                End If
            End If

            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPU <> 0 And CPE = 0 Then 'some remains

                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer

                IdentificationText += vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison. "

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the accused received from that office, "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the suspect received from that office, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with Bureau records, "
                End If
            End If


            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPU <> 0 And CPE <> 0 And CPU <> 0 Then 'some remains
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. " & Photographer
                IdentificationText += vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate", " chance prints are eliminated as the finger impressions of inmates") & " and " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison. "

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the accused received from that office, "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the suspect received from that office, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with Bureau records, "
                End If

            End If


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If

            WordApp.Selection.NoProofing = 1

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1

            Dim FileNo As String = SOCNumber

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            FileNo = "No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName

            WordApp.Selection.TypeText(FileNo)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)


            WordApp.Selection.TypeText(vbTab & sho)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: Identification of criminal through chance prints – report - reg.")



            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Ref: 1. Cr.No. " & CrNo & " u/s " & us & " of " & ps)
            WordApp.Selection.TypeText(vbNewLine & vbTab & "        2. " & FileNo)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14

            WordApp.Selection.TypeText(IdentificationText.Replace("..", "."))

            Dim IDRNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value
            Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(fds.CulpritsRegister, IDRNumber)

            For i = 0 To fds.CulpritsRegister.Count - 1
                Dim cpid = fds.CulpritsRegister(i).CPsIdentified
                Dim previousdetails As String = fds.CulpritsRegister(i).PreviousCaseDetails

                If cpid = "1" Then

                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of the accused ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".")

                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of the suspect ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0
                    Else
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails & " ", previousdetails & ". ") & "His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".")

                    End If
                Else
                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of the accused ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".")

                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of the suspect ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0
                    Else
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails & " ", previousdetails & ". ") & "His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".")
                    End If
                End If
            Next


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "This is for information and necessary action.")


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) = False Then
                aDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            ClosePleaseWaitForm()

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateIdentificationLetterDirector() Handles btnGenerateIdentificationReportDirector.Click, btnGenerateIdentificationReportDirector2.Click

        Try
            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records selected in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                ClosePleaseWaitForm()
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("The selected SOC Number " & SOCNumber & " does not exist. Please add details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            Dim IDRFolder As String = SuggestedLocation & "\Identification Reports"
            Dim oldfilename As String = IDRFolder & "\Identification Report - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - Director.docx"

            IDRFolder = IDRFolder & "\" & IDYear

            System.IO.Directory.CreateDirectory(IDRFolder)

            Dim sfilename As String = IDRFolder & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - Director.docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
                Thread.Sleep(2000)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ShowPleaseWaitForm()

            Dim InspectingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(7).Value.ToString().Replace(vbNewLine, "; ")
            Dim IdentifyingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(8).Value.ToString().Replace(vbNewLine, "; ")

            IdentifyingOfficer = GetSalutation(IdentifyingOfficer)

            Dim splitname() = Strings.Split(InspectingOfficer, "; ")
            InspectingOfficer = ""
            Dim u = splitname.GetUpperBound(0)
            For j = 0 To u
                If u = 0 Then
                    InspectingOfficer = GetSalutation(splitname(0))
                    Exit For
                End If

                If j = u - 1 Then
                    InspectingOfficer += GetSalutation(splitname(j)) + " and "
                ElseIf j = u Then
                    InspectingOfficer += GetSalutation(splitname(j))
                Else
                    InspectingOfficer += GetSalutation(splitname(j)) + ", "
                End If

            Next

            InspectingOfficer = Replace(Replace(Replace(Replace(InspectingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")
            IdentifyingOfficer = Replace(Replace(Replace(Replace(IdentifyingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")



            Dim dtid As String = Me.JoinedIDRDataGrid.SelectedCells(2).FormattedValue.ToString
            Dim dtins As String = Me.JoinedIDRDataGrid.SelectedCells(3).FormattedValue.ToString()
            Dim CPD As Integer = Val(fds.SOCRegister(0).ChancePrintsDeveloped)
            Dim CPU As Integer = Val(fds.SOCRegister(0).ChancePrintsUnfit)
            Dim CPE As Integer = Val(fds.SOCRegister(0).ChancePrintsEliminated)
            Dim CPR As Integer = CPD - CPU - CPE


            Dim PO As String = Trim(fds.SOCRegister(0).PlaceOfOccurrence)

            If PO <> "" Then
                PO = "(" & PO.Replace(vbNewLine, ", ") & ") "
            End If


            

            Dim us = fds.SOCRegister(0).SectionOfLaw

            Dim identifiedfrom As String = Me.JoinedIDRDataGrid.SelectedCells(18).Value.ToString.ToLower
            Dim IdentificationText As String = ""

            IdentificationText = vbTab & vbTab & "Kind attention is invited to the subject and reference cited above." & vbNewLine & vbNewLine & vbTab & "The Scene of Crime " & PO & "in the case cited above was inspected by " & InspectingOfficer & " of this unit on " & dtins


            If CPD = 1 And CPR = 1 Then 'one print
                IdentificationText += " and developed one chance print. "

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with Bureau records, "
                End If


            End If

            '--------------------------MORE THAN ONE PRINT---------------------



            If CPD > 1 And CPR = CPD Then 'all remain
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. "
                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with Bureau records, "
                End If
            End If

            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPE <> 0 And CPU = 0 Then 'some remains
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. "


                IdentificationText += vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate. ", " chance prints are eliminated as the finger impressions of inmates. ")

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with Bureau records, "
                End If
            End If

            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPU <> 0 And CPE = 0 Then 'some remains

                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. "

                IdentificationText += vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison. "

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with Bureau records, "
                End If
            End If


            If CPD > 0 And CPR <> CPD And CPR <> 0 And CPU <> 0 And CPE <> 0 And CPU <> 0 Then 'some remains
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. "
                IdentificationText += vbNewLine & vbTab & "Out of the " & ConvertNumberToWord(CPD) & " chance prints developed from the scene of crime, " & ConvertNumberToWord(CPE) & IIf(CPE = 1, " chance print is eliminated as the finger impression of inmate", " chance prints are eliminated as the finger impressions of inmates") & " and " & ConvertNumberToWord(CPU) & IIf(CPU = 1, " chance print is unfit", " chance prints are unfit") & " for comparison. "

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the remaining " & IIf(CPR = 1, "chance print ", "chance prints ") & "with Bureau records, "
                End If

            End If



            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.NoProofing = 1

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1

            Dim FileNo As String = SOCNumber

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            FileNo = "No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName

            WordApp.Selection.TypeText(FileNo)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "The Director" & vbNewLine & vbTab & "Fingerprint Bureau" & vbNewLine & vbTab & "Thiruvananthapuram")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: Identification of criminal through chance prints – report - reg.")



            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Ref: 1. Cr.No. " & CrNo & " u/s " & us & " of " & ps)
            WordApp.Selection.TypeText(vbNewLine & vbTab & "        2. " & FileNo)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14

            WordApp.Selection.TypeText(IdentificationText.Replace("..", "."))

            Dim IDRNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value
            Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(fds.CulpritsRegister, IDRNumber)

            For i = 0 To fds.CulpritsRegister.Count - 1
                Dim cpid = fds.CulpritsRegister(i).CPsIdentified
                Dim previousdetails As String = fds.CulpritsRegister(i).PreviousCaseDetails



                If cpid = "1" Then

                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of the accused ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "DA Slip Number - " & fds.CulpritsRegister(i).DANumber)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of the suspect ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                    ElseIf identifiedfrom = "afis" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails, previousdetails & ". "))
                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "DA Slip Number - " & fds.CulpritsRegister(i).DANumber)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "AFIS PIN - " & fds.CulpritsRegister(i).COID)

                    Else
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails, previousdetails & ". "))
                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "DA Slip Number - " & fds.CulpritsRegister(i).DANumber)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                    End If
                Else
                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of the accused ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "DA Slip Number - " & fds.CulpritsRegister(i).DANumber)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of the suspect ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                    ElseIf identifiedfrom = "afis" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0
                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails, previousdetails & ". "))
                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "DA Slip Number - " & fds.CulpritsRegister(i).DANumber)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "AFIS PIN - " & fds.CulpritsRegister(i).COID)

                    Else
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails, previousdetails & ". "))
                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "DA Slip Number - " & fds.CulpritsRegister(i).DANumber)

                        WordApp.Selection.TypeText(vbNewLine)
                        WordApp.Selection.TypeText(vbTab & vbTab & "Henry Classification - " & fds.CulpritsRegister(i).HenryClassification)
                    End If
                End If

            Next

            Dim pl As String = fds.SOCRegister(0).PropertyLost.Replace(vbCrLf, ", ")

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & "P/L - " & pl)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & "Gist - ")


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "The identification was made by " & IdentifyingOfficer & " on " & dtid & ".")

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & "This is for favour of information.")


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) = False Then
                aDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            ClosePleaseWaitForm()

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateIdentificationLetterDPC() Handles btnGenerateIdentificationReportDPC.Click, btnGenerateIdentificationReportDPCContext.Click

        Try
            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records selected in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                ClosePleaseWaitForm()
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("The selected SOC Number " & SOCNumber & " does not exist. Please add details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            Dim IDRFolder As String = SuggestedLocation & "\Identification Reports\" & IDYear

            System.IO.Directory.CreateDirectory(IDRFolder)

            Dim sfilename As String = IDRFolder & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - DPC.docx"

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ShowPleaseWaitForm()

            Dim InspectingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(7).Value.ToString().Replace(vbNewLine, "; ")
            Dim IdentifyingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(8).Value.ToString().Replace(vbNewLine, "; ")

            IdentifyingOfficer = GetSalutation(IdentifyingOfficer)

            Dim splitname() = Strings.Split(InspectingOfficer, "; ")
            InspectingOfficer = ""
            Dim u = splitname.GetUpperBound(0)
            For j = 0 To u
                If u = 0 Then
                    InspectingOfficer = GetSalutation(splitname(0))
                    Exit For
                End If

                If j = u - 1 Then
                    InspectingOfficer += GetSalutation(splitname(j)) + " and "
                ElseIf j = u Then
                    InspectingOfficer += GetSalutation(splitname(j))
                Else
                    InspectingOfficer += GetSalutation(splitname(j)) + ", "
                End If

            Next

            InspectingOfficer = Replace(Replace(Replace(Replace(InspectingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")
            IdentifyingOfficer = Replace(Replace(Replace(Replace(IdentifyingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")



            Dim dtid As String = Me.JoinedIDRDataGrid.SelectedCells(2).FormattedValue.ToString
            Dim dtins As String = Me.JoinedIDRDataGrid.SelectedCells(3).FormattedValue.ToString()
            Dim CPD As Integer = Val(fds.SOCRegister(0).ChancePrintsDeveloped)
            Dim CPU As Integer = Val(fds.SOCRegister(0).ChancePrintsUnfit)
            Dim CPE As Integer = Val(fds.SOCRegister(0).ChancePrintsEliminated)
            Dim CPR As Integer = CPD - CPU - CPE


            Dim PO As String = Trim(fds.SOCRegister(0).PlaceOfOccurrence)

            If PO <> "" Then
                PO = "(" & PO.Replace(vbNewLine, ", ") & ") "
            End If




            Dim us = fds.SOCRegister(0).SectionOfLaw

            Dim identifiedfrom As String = Me.JoinedIDRDataGrid.SelectedCells(18).Value.ToString.ToLower
            Dim IdentificationText As String = ""

            IdentificationText = vbTab & vbTab & "Kind attention is invited to the subject and reference cited above." & vbNewLine & vbNewLine & vbTab & "The Scene of Crime " & PO & "in the case cited above was inspected by " & InspectingOfficer & " of this unit on " & dtins


            If CPD = 1 Then 'one print
                IdentificationText += " and developed one chance print. "

                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance print with Bureau records, "
                End If


            End If

            '--------------------------MORE THAN ONE PRINT---------------------



            If CPD > 1 Then
                IdentificationText += " and developed " & ConvertNumberToWord(CPD) & " chance prints. "
                If identifiedfrom = "accused" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with the fingerprint slip of the accused received from " & ps & ", "
                ElseIf identifiedfrom = "suspects" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with the fingerprint slip of the suspect received from " & ps & ", "
                ElseIf identifiedfrom = "afis" Then
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with AFIS, "
                Else
                    IdentificationText += vbNewLine & vbTab & "On comparing the chance prints with Bureau records, "
                End If
            End If



            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.NoProofing = 1

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1

            Dim FileNo As String = SOCNumber

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            FileNo = "No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName
            WordApp.Selection.TypeText(FileNo)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)

            Dim dpc As String = "The District Police Chief"
            If FullDistrictName.ToLower.Contains("city") Then
                dpc = "The Commissionar of Police"
            End If
            WordApp.Selection.TypeText(vbTab & dpc & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: Identification of criminal through chance print – report - reg.")



            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Ref: 1. Cr.No. " & CrNo & " u/s " & us & " of " & ps)
            WordApp.Selection.TypeText(vbNewLine & vbTab & "        2. " & FileNo)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14

            WordApp.Selection.TypeText(IdentificationText.Replace("..", "."))

            Dim IDRNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value
            Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(fds.CulpritsRegister, IDRNumber)

            For i = 0 To fds.CulpritsRegister.Count - 1
                Dim cpid = fds.CulpritsRegister(i).CPsIdentified
                Dim previousdetails As String = fds.CulpritsRegister(i).PreviousCaseDetails



                If cpid = "1" Then

                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of the accused ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")
                        WordApp.Selection.Font.Bold = 0


                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of the suspect ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                    Else
                        WordApp.Selection.TypeText(IIf(CPD = 1, "the", "one") & " chance print has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails, previousdetails & ". "))

                    End If
                Else
                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of the accused ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of the suspect ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                    Else
                        WordApp.Selection.TypeText(ConvertNumberToWord(Val(cpid)) & " chance prints have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of one ")
                        WordApp.Selection.Font.Bold = 1

                        WordApp.Selection.TypeText(fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".")

                        WordApp.Selection.Font.Bold = 0

                        WordApp.Selection.TypeText(" He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails, previousdetails & ". "))
                       
                    End If
                End If

            Next

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "The identification was made by " & IdentifyingOfficer & " on " & dtid & ".")

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & "This is for favour of information.")


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                '  WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) = False Then
                aDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            ClosePleaseWaitForm()

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateIdentificationCoB() Handles btnGenerateIdentificationCoB.Click, btnGenerateIdentificationCoB2.Click
        Try

            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records selected in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If



            Me.Cursor = Cursors.WaitCursor

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                ClosePleaseWaitForm()
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("The selected SOC Number " & SOCNumber & " does not exist. Please add details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            Dim IDRFolder As String = SuggestedLocation & "\Identification Reports"
            Dim oldfilename As String = IDRFolder & "\Identification Report - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - CoB.docx"

            IDRFolder = IDRFolder & "\" & IDYear

            System.IO.Directory.CreateDirectory(IDRFolder)

            Dim sfilename As String = IDRFolder & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - CoB.docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
                Thread.Sleep(2000)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ShowPleaseWaitForm()

            Dim sho As String = Me.PSRegisterTableAdapter.FindSHO(ps)

            If sho Is Nothing Then
                sho = "Sub Inspector of Police"
            End If

            If sho.ToUpper = "IP" Then
                sho = "The Inspector of Police, " & ps
            Else
                sho = "The Sub Inspector of Police, " & ps
            End If



            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.NoProofing = 1

            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("CoB/WIRELESS MESSAGE" & vbNewLine & vbNewLine)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText(("TO:" & vbTab & sho.ToUpper) & vbNewLine)
            WordApp.Selection.TypeText("INF:" & vbTab & "DIRECTOR, FPB, TVPM" & vbNewLine)
            WordApp.Selection.TypeText(("FROM:" & vbTab & "Tester Inspector, " & ShortOfficeName & ", " & ShortDistrictName).ToUpper & vbNewLine)
            WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            WordApp.Selection.Font.Bold = 1

            Dim FileNo As String = SOCNumber
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            WordApp.Selection.TypeText("No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName & vbTab & vbTab & vbTab & vbTab & vbTab & "DATE: " & Format(Now, "dd/MM/yyyy") & vbNewLine)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText((vbTab & "REFER CR.NO. " & CrNo & " U/S " & fds.SOCRegister(0).SectionOfLaw & " OF " & ps & ". ").ToUpper)

            Dim IDRNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value
            Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(fds.CulpritsRegister, IDRNumber)

            Dim identifiedfrom As String = Me.JoinedIDRDataGrid.SelectedCells(18).Value.ToString.ToLower

            For i = 0 To fds.CulpritsRegister.Count - 1
                Dim cpid = fds.CulpritsRegister(i).CPsIdentified
                Dim previousdetails As String = fds.CulpritsRegister(i).PreviousCaseDetails
                If cpid = "1" Then
                    WordApp.Selection.TypeText(("one chance print developed from the scene of crime has been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impression of ").ToUpper)

                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(("the accused " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ". His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".").ToUpper)
                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(("the suspect " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".").ToUpper)
                    ElseIf identifiedfrom = "afis" Then
                        WordApp.Selection.TypeText(("one " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & " from AFIS. PIN - " & fds.CulpritsRegister(i).COID & ".").ToUpper)
                    Else
                        WordApp.Selection.TypeText(("one " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ". He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails & " ", previousdetails & ". ") & "His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".").ToUpper)
                    End If

                Else

                    WordApp.Selection.TypeText((ConvertNumberToWord(Val(cpid)) & " chance prints developed from the scene of crime have been identified as the " & fds.CulpritsRegister(i).FingersIdentified & " finger impressions of ").ToUpper)

                    If identifiedfrom = "accused" Then
                        WordApp.Selection.TypeText(("the accused " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ". His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".").ToUpper)
                    ElseIf identifiedfrom = "suspects" Then
                        WordApp.Selection.TypeText(("the suspect " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ".").ToUpper)
                    ElseIf identifiedfrom = "afis" Then
                        WordApp.Selection.TypeText(("one " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & " from AFIS. PIN - " & fds.CulpritsRegister(i).COID & ".").ToUpper)
                    Else
                        WordApp.Selection.TypeText(("one " & fds.CulpritsRegister(i).CulpritName & ", " & fds.CulpritsRegister(i).Address.Replace(vbCrLf, ", ") & ". He is accused in " & IIf(previousdetails.EndsWith("."), previousdetails & " ", previousdetails & ". ") & "His fingerprint slip is registered in the Bureau records as Daily Arrest Slip Number " & fds.CulpritsRegister(i).DANumber & ".").ToUpper)
                    End If

                End If
            Next
            WordApp.Selection.TypeText((" Detailed report follows.").ToUpper)
            WordApp.Selection.TypeText(vbCrLf & "--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)

            WordApp.Selection.TypeText(vbNewLine)

            If My.Computer.FileSystem.FileExists(sfilename) = False Then
                aDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            ClosePleaseWaitForm()
            Me.Cursor = Cursors.Default
            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

        Catch ex As Exception
            ClosePleaseWaitForm()
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub GenerateIdentificationCL() Handles btnGenerateExpertOpinionCL.Click, btnGenerateExpertOpinionCLContext.Click

        Try
            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records selected in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor

            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                ClosePleaseWaitForm()
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("The selected SOC Number " & SOCNumber & " does not exist. Please add details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            ShowPleaseWaitForm()


            Dim IdentifyingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(8).Value.ToString().Replace(vbNewLine, "; ")

            IdentifyingOfficer = GetSalutation(IdentifyingOfficer)

            IdentifyingOfficer = Replace(Replace(Replace(Replace(IdentifyingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")


            Dim sho As String = Me.PSRegisterTableAdapter.FindSHO(ps)

            If sho Is Nothing Then
                sho = "The Station House Officer"
            End If


            If sho.ToUpper = "IP" Then
                sho = "The Inspector of Police" & vbNewLine & vbTab & ps
            Else
                sho = "The Station House Officer" & vbNewLine & vbTab & ps
            End If

            Dim us = fds.SOCRegister(0).SectionOfLaw

            


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim wdDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If

            WordApp.Selection.NoProofing = 1

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1

            Dim FileNo As String = SOCNumber

            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            WordApp.Selection.TypeText("No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)


            WordApp.Selection.TypeText(vbTab & sho)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: Identification of criminal through chance prints – Expert opinion forwarding of - reg.")



            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Ref: Cr.No. " & CrNo & " u/s " & us & " of " & ps)

            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "I am forwarding herewith the Expert Opinion in the above referred case, furnished by " & IdentifyingOfficer & " of this office, for onward transmission to the concerned court.")


            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                ' WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If


            ClosePleaseWaitForm()

            Dim sFileName As String = FileIO.SpecialDirectories.MyDocuments & "\Expert Opinion CL.docx"
            If Not FileInUse(sFileName) Then wdDoc.SaveAs(sFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            wdDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnOpenIdentificationReportFolder_Click(sender As Object, e As EventArgs) Handles btnOpenIdentificationReportFolder.Click, btnOpenIdentificationReportFolderContext.Click

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim IDRFolder As String = SuggestedLocation & "\Identification Reports"
            System.IO.Directory.CreateDirectory(IDRFolder)

            If Me.JoinedIDRDataGrid.RowCount = 0 Or Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                Call Shell("explorer.exe " & IDRFolder, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                Call Shell("explorer.exe " & IDRFolder, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            Dim oldfilename As String = IDRFolder & "\Identification Report - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - SHO.docx"

            Dim sfilename As String = IDRFolder & "\" & IDYear & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - SHO.docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                System.IO.Directory.CreateDirectory(IDRFolder & "\" & IDYear)
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Call Shell("explorer.exe /select," & sfilename, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            oldfilename = IDRFolder & "\Identification Report - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - CoB.docx"

            sfilename = IDRFolder & "\" & IDYear & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - CoB.docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                System.IO.Directory.CreateDirectory(IDRFolder & "\" & IDYear)
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Call Shell("explorer.exe /select," & sfilename, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            oldfilename = IDRFolder & "\Identification Report - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - Director.docx"

            sfilename = IDRFolder & "\" & IDYear & "\IDR No. " & IDNumber & " - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & " - Director.docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                System.IO.Directory.CreateDirectory(IDRFolder & "\" & IDYear)
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Call Shell("explorer.exe /select," & sfilename, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Call Shell("explorer.exe " & IDRFolder, AppWinStyle.NormalFocus)

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
    Private Sub btnGenerateExpertOpinion_Click(sender As Object, e As EventArgs) Handles btnGenerateExpertOpinion.Click, btnGenerateExpertOpinionContext.Click
        Try

            If Me.JoinedIDRDataGrid.RowCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No records selected in Identification Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                ClosePleaseWaitForm()
                Me.Cursor = Cursors.Default
                MessageBoxEx.Show("The selected SOC Number " & SOCNumber & " does not exist. Please add details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber

            Dim OpinionFolder As String = SuggestedLocation & "\Expert Opinion"
            Dim oldfilename As String = OpinionFolder & "\Expert Opinion - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & ".docx"

            OpinionFolder = OpinionFolder & "\" & IDYear & "\ID No. " & IDNumber

            System.IO.Directory.CreateDirectory(OpinionFolder)

            Dim sfilename As String = OpinionFolder & "\Opinion - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & ".docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
                Thread.Sleep(2000)
            End If

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If



            Dim FileNo As String = SOCNumber
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            Dim InspectingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(7).Value.ToString().Replace(vbNewLine, "; ")
            Dim IdentifyingOfficer As String = Me.JoinedIDRDataGrid.SelectedCells(8).Value

            Dim splitname() = Strings.Split(InspectingOfficer, "; ")
            InspectingOfficer = ""
            Dim u = splitname.GetUpperBound(0)

            For j = 0 To u
                If u = 0 Then
                    InspectingOfficer = GetSalutation(splitname(0))
                    Exit For
                End If

                If j = u - 1 Then
                    InspectingOfficer += GetSalutation(splitname(j)) + " and "
                ElseIf j = u Then
                    InspectingOfficer += GetSalutation(splitname(j))
                Else
                    InspectingOfficer += GetSalutation(splitname(j)) + ", "
                End If

            Next

            InspectingOfficer = Replace(Replace(Replace(Replace(InspectingOfficer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")


            Dim DTID As String = Me.JoinedIDRDataGrid.SelectedCells(2).FormattedValue.ToString
            Dim dtins As String = Me.JoinedIDRDataGrid.SelectedCells(3).FormattedValue.ToString()
            Dim CPD As Integer = Val(fds.SOCRegister(0).ChancePrintsDeveloped)
            Dim CPU As Integer = Val(fds.SOCRegister(0).ChancePrintsUnfit)
            Dim CPE As Integer = Val(fds.SOCRegister(0).ChancePrintsEliminated)
            Dim CPR As Integer = CPD - CPU - CPE

           

            Dim IDRNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value
            Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(fds.CulpritsRegister, IDRNumber)

            Dim culpritname As String = fds.CulpritsRegister(0).CulpritName
            Dim address As String = fds.CulpritsRegister(0).Address
            address = address.Replace(vbCrLf, ", ")
            address = address.Replace(",,", ",")

            Dim FingersIdentified As String = fds.CulpritsRegister(0).FingersIdentified
            Dim previouscasedetails As String = fds.CulpritsRegister(0).PreviousCaseDetails
            Dim daslipnumber As String = fds.CulpritsRegister(0).DANumber
            Dim CPI As Integer = fds.CulpritsRegister(0).CPsIdentified

            Dim CPDMarkings As String = ""


            For i = 1 To CPD
                CPDMarkings = CPDMarkings & "'" & PS.Substring(0, 1) & i & "'" & IIf(i = CPD - 1, " and ", " ")
            Next

            CPDMarkings = Trim(CPDMarkings)
            CPDMarkings = CPDMarkings.Replace(" ", ", ")
            CPDMarkings = CPDMarkings.Replace(", and, ", " and ")


            frmExpertOpinion.txtCPD.Text = CPDMarkings
            frmExpertOpinion.txtCPE.Text = IIf(CPE = 0, "Nil", CPDMarkings)
            frmExpertOpinion.txtCPU.Text = IIf(CPU = 0, "Nil", CPDMarkings)
            frmExpertOpinion.txtCPI.Text = CPDMarkings
            frmExpertOpinion.txtFingerOrder.Text = FingersIdentified
            frmExpertOpinion.txtOpinionCP.Text = CPDMarkings
            frmExpertOpinion.txtOpinionFinger.Text = FingersIdentified
            frmExpertOpinion.txtRidgeColor.Text = "white"
            frmExpertOpinion.lblCPD.Text = IIf(CPD = 1, "1 CP", CPD & " CPs")
            frmExpertOpinion.lblCPE.Text = IIf(CPE = 0, "Nil", IIf(CPE = 1, "1 CP", CPE & " CPs"))
            frmExpertOpinion.lblCPU.Text = IIf(CPU = 0, "Nil", IIf(CPU = 1, "1 CP", CPU & " CPs"))
            frmExpertOpinion.lblCPI.Text = IIf(CPI = 1, "1 CP", CPI & " CPs")

            blGenerateOpinion = False

            frmExpertOpinion.ShowDialog()
            frmExpertOpinion.BringToFront()


            If Not blGenerateOpinion Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            CPDMarkings = frmExpertOpinion.txtCPD.Text.Trim
            Dim CPUMarkings As String = frmExpertOpinion.txtCPU.Text.Trim
            Dim CPEMarkings As String = frmExpertOpinion.txtCPE.Text.Trim
            Dim CPIMarkings As String = frmExpertOpinion.txtCPI.Text.Trim
            Dim FingerOrder As String = frmExpertOpinion.txtFingerOrder.Text.Trim
            Dim OpinionCP As String = frmExpertOpinion.txtOpinionCP.Text.Trim
            Dim OpinionFinger As String = frmExpertOpinion.txtOpinionFinger.Text.Trim
            Dim RidgeColor As String = frmExpertOpinion.txtRidgeColor.Text.Trim.ToLower


            ShowPleaseWaitForm()
            Me.Cursor = Cursors.WaitCursor


            Dim PO As String = Trim(fds.SOCRegister(0).PlaceOfOccurrence)

            If PO <> "" Then
                PO = "(" & PO.Replace(vbNewLine, ", ") & ")"
                PO = PO.Replace(",, ", ", ")
            End If

            Dim Photographer As String = fds.SOCRegister(0).Photographer

            If Trim(Photographer) = "" Or Trim(Photographer).ToLower = "no photographer" Then
                Photographer = ""
            Else
                If CPD = 1 Then
                    Photographer = "The chance print was photographed by Sri. " & Photographer & ", Police Photographer"
                Else
                    Photographer = "The chance prints were photographed by Sri. " & Photographer & ", Police Photographer"
                End If
                Dim dtphotographed = fds.SOCRegister(0).DateOfReceptionOfPhoto
                If dtphotographed <> vbNullString Then
                    If dtins = dtphotographed Then
                        dtphotographed = " on the same day."
                    Else
                        dtphotographed = " on " & dtphotographed & "."
                    End If
                Else
                    dtphotographed = "."
                End If

                Photographer += dtphotographed
            End If


            Dim SHO As String = Me.PSRegisterTableAdapter.FindSHO(PS)

            If SHO Is Nothing Then
                SHO = "The Station House Officer"
            End If

            If SHO.ToUpper = "IP" Then
                SHO = "The Inspector of Police"
            Else
                SHO = "The Station House Officer"
            End If


            Dim Section = fds.SOCRegister(0).SectionOfLaw

            Dim identifiedfrom As String = Me.JoinedIDRDataGrid.SelectedCells(18).Value.ToString.ToLower

            Dim Designation As String = ""
            If IdentifyingOfficer.EndsWith(", FPE") Then
                Designation = "Fingerprint Expert"
            End If

            If IdentifyingOfficer.EndsWith(", TI") Then
                Designation = "Tester Inspector"
            End If

            IdentifyingOfficer = IdentifyingOfficer.Replace(", FPE", "")
            IdentifyingOfficer.Replace(", TI", "")




            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.NoProofing = 1

            WordApp.Selection.TypeText("No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date: " & GenerateDate(True))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("From")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & IdentifyingOfficer & vbNewLine & vbTab & Designation & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("To")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & SHO & vbNewLine & vbTab & PS)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbNewLine & vbTab)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sub: ")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText("Identification of criminal through chance prints developed from scene of crime - Expert Opinion furnishing of - regarding.")

            WordApp.Selection.TypeText(vbNewLine & vbTab)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Ref: ")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & "1. " & "Cr.No. " & CrNo & " u/s " & Section & " of " & PS)
            WordApp.Selection.TypeText(vbNewLine & vbTab & vbTab & "2. Identification report of even number dated ...... ")
            WordApp.Selection.TypeText(vbNewLine & vbNewLine & vbTab & vbTab & "Please refer to the above." & vbNewLine & vbNewLine)

            'WordApp.Selection.ParagraphFormat.Space15()

            SHO = SHO.Replace("The", "the")

            WordApp.Selection.TypeText(vbTab & "As requested by " & SHO & ", " & PS & ", I had inspected the scene of crime " & PO & " concerned in Crime No. " & CrNo & " u/s " & Section & " of " & PS & " on " & dtins & " and developed " & ConvertNumberToWord(CPD) & IIf(CPD = 1, " chance print. The chance print was marked as ", " chance prints. The chance prints were marked as ") & CPDMarkings)

            WordApp.Selection.TypeText(IIf(CPD = 1, ". The chance print was ", ". The chance prints were ") & "developed from ........ ")

            WordApp.Selection.TypeText(Photographer & vbNewLine & vbTab)

            Dim PrintComparisonDetails As String = ""
            Dim AccusedDetails As String = culpritname & ", " & address
            Dim IDDetails As String = ""

            If CPD = 1 Then
                WordApp.Selection.TypeText("The chance print was compared with the finger impressions of the inmates and found not identical.")
            End If

            If CPU = 0 And CPD > 1 Then

                If CPE = 0 Then
                    WordApp.Selection.TypeText("The chance prints were compared with the finger impressions of the inmates and found not identical.")
                End If

                If CPE = 1 Then
                    WordApp.Selection.TypeText("On detailed examination of the chance prints at the Bureau, the chance print marked as " & CPEMarkings & " was eliminated as the finger impression of inmate.")
                End If

                If CPE > 1 Then
                    WordApp.Selection.TypeText("On detailed examination of the chance prints at the Bureau, the chance prints marked as " & CPEMarkings & " were eliminated as the finger impressions of inmates.")
                End If
            End If

            If CPU > 0 And CPD > 1 Then
                WordApp.Selection.TypeText("On detailed examination of the chance prints at the Bureau, the chance " & IIf(CPU = 1, "print marked as " & CPUMarkings & " was", "prints marked as " & CPUMarkings & " were") & " found unfit for comparison due to the lack of sufficient number of ridge characteristics")
                Dim cprem As Integer = CPD - CPU
                If CPE = 0 Then
                    WordApp.Selection.TypeText(". The remaining " & ConvertNumberToWord(cprem) & IIf(cprem = 1, " chance print marked as ....... was", " chance prints marked as ....... were") & " compared with the finger impressions of the inmates and found not identical.")
                End If

                If CPE = 1 Then
                    WordApp.Selection.TypeText(" and the chance print marked as " & CPEMarkings & " was eliminated as the finger impression of inmate.")
                End If

                If CPE > 1 Then
                    WordApp.Selection.TypeText(" and the chance prints marked as " & CPEMarkings & " were eliminated as the finger impressions of inmates.")
                End If
            End If

            If CPI = 1 Then
                If identifiedfrom = "accused" Then
                    WordApp.Selection.TypeText(" On " & DTID & " the daily arrest fingerprint slip of the accused ")
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(AccusedDetails)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was received from your office. On comparison, the chance print marked as ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(CPIMarkings)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was found ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText("IDENTICAL")
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" with his ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FingersIdentified)
                    WordApp.Selection.Font.Bold = 0

                    WordApp.Selection.TypeText(" finger impression, since the impressions possess identical ridge characterics in their nature and relative positions. He is accused in " & previouscasedetails & " and his fingerprint slip is recorded in this Bureau as daily arrest slip number ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(daslipnumber)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(".")

                ElseIf identifiedfrom = "suspects" Then
                    WordApp.Selection.TypeText(" On " & DTID & " the fingerprint slip of one ")
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(AccusedDetails)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was received from your office as suspect print. On comparison, the chance print marked as ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(CPIMarkings)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was found ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText("IDENTICAL")
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" with his ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FingersIdentified)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" finger impression, since the impressions possess identical ridge characterics in their nature and relative positions.")

                Else
                    WordApp.Selection.TypeText(" On comparing the chance print with the daily arrest fingerprint slips recorded in this Bureau, the chance print marked as ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(CPIMarkings)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was found ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText("IDENTICAL")
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" with the ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FingersIdentified)
                    WordApp.Selection.Font.Bold = 0

                    WordApp.Selection.TypeText(" finger impression of one ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(AccusedDetails)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(", since the impressions possess identical ridge characterics in their nature and relative positions. He is accused in " & previouscasedetails & " and his fingerprint slip is recorded in this Bureau as daily arrest slip number ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(daslipnumber)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(".")

                End If
                WordApp.Selection.TypeText(" The details of identification were intimated to you vide reference cited second.")
            End If


            If CPI > 1 Then
                If identifiedfrom = "accused" Then
                    WordApp.Selection.TypeText(" On " & DTID & " the daily arrest fingerprint slip of the accused ")
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(AccusedDetails)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was received from your office. On comparison, the chance prints marked as ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(CPIMarkings)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" were found ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText("IDENTICAL")
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" with his ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FingerOrder)
                    WordApp.Selection.Font.Bold = 0

                    WordApp.Selection.TypeText(" finger impressions respectively, since the impressions possess identical ridge characterics in their nature and relative positions. He is accused in " & previouscasedetails & " and his fingerprint slip is recorded in this Bureau as daily arrest slip number ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(daslipnumber)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(".")

                ElseIf identifiedfrom = "suspects" Then
                    WordApp.Selection.TypeText(" On " & DTID & " the fingerprint slip of one ")
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(AccusedDetails)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" was received from your office as suspect print. On comparison, the chance prints marked as ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(CPIMarkings)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" were found ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText("IDENTICAL")
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" with his ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FingerOrder)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" finger impressions respectively, since the impressions possess identical ridge characterics in their nature and relative positions.")

                Else
                    WordApp.Selection.TypeText(" On comparing the chance prints with the daily arrest fingerprint slips recorded in this Bureau, the chance prints marked as ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(CPIMarkings)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" were found ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText("IDENTICAL")
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(" with the ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FingerOrder)
                    WordApp.Selection.Font.Bold = 0

                    WordApp.Selection.TypeText(" finger impressions respectively of one ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(AccusedDetails)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(", since the impressions possess identical ridge characterics in their nature and relative positions. He is accused in " & previouscasedetails & " and his fingerprint slip is recorded in this Bureau as daily arrest slip number ")

                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(daslipnumber)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(".")

                End If
                WordApp.Selection.TypeText(" The details of identification were intimated to you vide reference cited second.")
            End If

            WordApp.Selection.TypeText(vbNewLine & vbNewLine)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.TypeText("EXPERT OPINION")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeText(vbTab & "For furnishing the Expert Opinion in this regard, I have taken the chance print marked as ")
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(" and the specimen ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionFinger)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" finger impression in the Daily Arrest slip No. " & daslipnumber & ". The specimen " & OpinionFinger & " finger impression is marked as ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("'S’")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(" by me.")

            WordApp.Selection.TypeText(vbNewLine & vbTab & "The ridges in chance print marked as ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" are " & RidgeColor & " in colour since the chance print was developed using " & RidgeColor & " colored fingerprint developing chemical powder and the ridges in specimen impression ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("'S’")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(" are black in colour since it was taken by means of black printers ink in finger print slip. ")

            WordApp.Selection.TypeText(vbNewLine & vbTab & "In order to establish the identity, I have marked the following eight identical ridge characteristics present in their nature and relative positions in the respective enlarged photographs of ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" and ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("'S’")
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(". The photographs are duly attested by me on the rear side.")

            WordApp.Selection.TypeText(vbNewLine & vbNewLine)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.TypeText("Eight Identical Ridge Characteristics present in both the finger impressions " & OpinionCP & " and ‘S’")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Font.Underline = 0

            WordApp.Selection.TypeText("Point No. 1: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 2: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 3: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 4: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 5: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 6: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 7: is a ")
            WordApp.Selection.TypeText(vbNewLine & "Point No. 8: is a ")


            WordApp.Selection.Font.Bold = 0


            WordApp.Selection.TypeText(vbNewLine & vbNewLine & vbTab & "Since the above mentioned eight Identical Ridge Characteristics are repeatedly present in their nature and relative positions in both the chance print ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" and specimen impression ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("'S’")
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(", they are ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("IDENTICAL")
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(". That is, they are made by the same finger of the same person.  Since ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("'S’")
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" is the specimen ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionFinger)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" finger impression in the fingerprint slip of ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(culpritname)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(", and as it is identical with the chance print marked as ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" developed from the referred scene of crime, I am of the opinion that the chance print marked as ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" is made by the ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionFinger)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" finger of ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(AccusedDetails)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(".")

            WordApp.Selection.TypeText(vbNewLine & vbTab & "Attested photographic enlargements of the chance print ")

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(OpinionCP)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" and the specimen finger impression ")
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("‘S’")
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeText(" with eight points of identity marked therein are enclosed herewith.")

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If WordApp.ActiveDocument.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument) > 1 Then
                aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageFooter

                aDoc.ActiveWindow.ActivePane.Selection.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight

                aDoc.ActiveWindow.Selection.TypeText("Page ")

                Dim CurrentPage = Word.WdFieldType.wdFieldPage

                aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, CurrentPage, , )

                aDoc.ActiveWindow.Selection.TypeText(" of ")


                Dim TotalPageCount = Word.WdFieldType.wdFieldNumPages
                aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, TotalPageCount, , )

                aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument
            End If

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)



            If My.Computer.FileSystem.FileExists(sfilename) = False Then
                aDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            ClosePleaseWaitForm()

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub btnOpenExpertOpinionFolder_Click(sender As Object, e As EventArgs) Handles btnOpenExpertOpinionFolder.Click, btnOpenExpertOpinionFolderContext.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim OpinionFolder As String = SuggestedLocation & "\Expert Opinion"
            System.IO.Directory.CreateDirectory(OpinionFolder)

            If Me.JoinedIDRDataGrid.RowCount = 0 Or Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
                Call Shell("explorer.exe " & OpinionFolder, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim IDNumber As String = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString
            Dim SOCNumber As String = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString
            Dim IDDate As Date = Me.JoinedIDRDataGrid.SelectedCells(2).Value
            Dim IDYear As String = IDDate.Year.ToString

            Dim SplitText() = Strings.Split(IDNumber, "/")
            Dim IDN As Integer = CInt(SplitText(0))

            IDNumber = IDN.ToString("D3")

            Dim fds As FingerPrintDataSet = New FingerPrintDataSet
            Me.SOCRegisterTableAdapter.FillBySOCNumber(fds.SOCRegister, SOCNumber)

            If fds.SOCRegister.Count = 0 Then
                Call Shell("explorer.exe " & OpinionFolder, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim ps As String = fds.SOCRegister(0).PoliceStation
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If

            Dim CrNo = fds.SOCRegister(0).CrimeNumber


            Dim oldfilename As String = OpinionFolder & "\Expert Opinion - " & "SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & ".docx"

            OpinionFolder = OpinionFolder & "\" & IDYear & "\ID No. " & IDNumber
            System.IO.Directory.CreateDirectory(OpinionFolder)

            Dim sfilename As String = OpinionFolder & "\Opinion - SOC No. " & SOCNumber.Replace("/", "-") & " - Cr.No. " & CrNo.Replace("/", "-") & " - " & ps & ".docx"

            If My.Computer.FileSystem.FileExists(oldfilename) Then
                My.Computer.FileSystem.MoveFile(oldfilename, sfilename)
            End If


            If My.Computer.FileSystem.FileExists(sfilename) Then
                Call Shell("explorer.exe /select," & sfilename, AppWinStyle.NormalFocus)
            Else
                Call Shell("explorer.exe " & OpinionFolder, AppWinStyle.NormalFocus)
            End If

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnShowIdentifiedDocket_Click(sender As Object, e As EventArgs) Handles btnShowIdentifiedDocket.Click
        If Me.JoinedIDRDataGrid.RowCount = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.JoinedIDRDataGrid.SelectedRows.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        GenerateIdentifiedFileDocket()

    End Sub

    Private Sub GenerateIdentifiedFileDocket() Handles btnIdentifiedTemplateContextMenu.Click

        Try

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\IdentifiedTemplate.docx"

            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ShowPleaseWaitForm()
            Me.Cursor = Cursors.WaitCursor

            Dim idno As String = ""
            Dim SoCNumber As String = ""
            Dim InspectingOfficer As String = ""
            Dim IdentifyingOfficer As String = ""
            Dim sdtid As String = ""
            Dim PS As String = ""
            Dim Cr As String = ""
            Dim dtins As String = ""


            idno = Me.JoinedIDRDataGrid.SelectedCells(0).Value.ToString()
            SoCNumber = Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString()
            InspectingOfficer = Me.JoinedIDRDataGrid.SelectedCells(7).Value.ToString().Replace(vbNewLine, "; ")
            IdentifyingOfficer = Me.JoinedIDRDataGrid.SelectedCells(8).Value.ToString().Replace(vbNewLine, "; ")
            sdtid = Me.JoinedIDRDataGrid.SelectedCells(2).FormattedValue.ToString
            PS = Me.JoinedIDRDataGrid.SelectedCells(4).Value.ToString
            Cr = Me.JoinedIDRDataGrid.SelectedCells(5).Value.ToString & " u/s " & Me.JoinedIDRDataGrid.SelectedCells(6).Value.ToString
            dtins = Me.JoinedIDRDataGrid.SelectedCells(3).FormattedValue.ToString


            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            Dim FileNo As String = SoCNumber
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            Dim splitname() = Strings.Split(InspectingOfficer, "; ")
            InspectingOfficer = ""
            Dim u = splitname.GetUpperBound(0)
            For j = 0 To u
                If u = 0 Then
                    InspectingOfficer = splitname(0)
                    Exit For
                End If

                If j = u - 1 Then
                    InspectingOfficer += splitname(j) + " and "
                ElseIf j = u Then
                    InspectingOfficer += splitname(j)
                Else
                    InspectingOfficer += splitname(j) + ", "
                End If

            Next


            wdBooks("IDN").Range.Text = idno
            wdBooks("SOC").Range.Text = "No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName
            wdBooks("PS").Range.Text = PS
            wdBooks("Cr").Range.Text = Cr
            wdBooks("DIns").Range.Text = dtins
            wdBooks("Did").Range.Text = sdtid

            Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(Me.FingerPrintDataSet1.CulpritsRegister, idno)
            Dim c As Integer = Me.FingerPrintDataSet1.CulpritsRegister.Rows.Count

            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            For rc = 1 To c - 1
                wdTbl.Rows.Add()
            Next
            Dim k = 8
            For i = 0 To c - 1
                If c > 1 Then
                    wdTbl.Cell(k, 3).Range.Select()
                    wdApp.Selection.TypeText("A" & (i + 1) & ".")
                End If

                wdTbl.Cell(k, 4).Range.Select()
                wdApp.Selection.TypeText(Me.FingerPrintDataSet1.CulpritsRegister(i).CulpritName.Trim & vbCrLf)

                wdApp.Selection.Font.Bold = 0
                Dim da = Me.FingerPrintDataSet1.CulpritsRegister(i).DANumber.Trim
                Dim hc = Me.FingerPrintDataSet1.CulpritsRegister(i).HenryClassification.Trim
                Dim address = Me.FingerPrintDataSet1.CulpritsRegister(i).Address.Trim
                Dim pin = Me.FingerPrintDataSet1.CulpritsRegister(i).COID.Trim

                Dim iddetails As String = address & vbCrLf & IIf(da = "", "", vbCrLf & "DA Number: " & da) & vbCrLf & IIf(hc = "", "", "Classification: " & hc) & vbCrLf & IIf(pin = "", "", "AFIS PIN: " & pin) & vbCrLf
                wdApp.Selection.TypeText(iddetails)
                k = k + 1
            Next

            If c = 1 Then
                wdTbl.Cell(k - 1, 3).Merge(wdTbl.Cell(k - 1, 4))
            End If

            wdTbl.Borders.Enable = 0

            If InspectingOfficer = IdentifyingOfficer Then
                wdBooks("FPEIns").Range.Text = "Inspected and Identified by: " & InspectingOfficer
                wdBooks("FPEId").Range.Text = ""
            Else
                wdBooks("FPEIns").Range.Text = "Inspected by: " & InspectingOfficer
                wdBooks("FPEId").Range.Text = "Identified by: " & IdentifyingOfficer
            End If

            ClosePleaseWaitForm()

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Visible = True
            wdApp = Nothing
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub ListOfIdentifiedCases(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListOfIdentifiedCases.Click
        On Error Resume Next

        frmAnnualStatistics.StartPosition = FormStartPosition.CenterParent
        frmAnnualStatistics.Text = "List of Identified Cases"
        frmAnnualStatistics.TitleText = "<b>List of Identified Cases</b>"
        frmAnnualStatistics.ShowDialog()

    End Sub


    Private Sub ListOfMonthlyIDCases(sender As Object, e As EventArgs) Handles btnIdentificationStatement2.Click, btnIdentificationStatement.Click
        On Error Resume Next
        frmIdentificationStatement.StartPosition = FormStartPosition.CenterParent
        frmIdentificationStatement.ShowDialog()
    End Sub
    Private Sub GistOfIdentifiedCases(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGistOfIdentifiedCases.Click
        On Error Resume Next

        frmAnnualStatistics.StartPosition = FormStartPosition.CenterParent
        frmAnnualStatistics.Text = "Gist of Identified Cases"
        frmAnnualStatistics.TitleText = "<b>Gist of Identified Cases</b>"
        frmAnnualStatistics.ShowDialog()


    End Sub

    Private Sub ForwardPhotograph() Handles btnPhotographForwarding.Click, btnForwardPhoto.Click
        Try
            If (Me.SOCDatagrid.RowCount = 0) Or (Me.SOCDatagrid.SelectedRows.Count = 0) Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No record is selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub
            End If

            Dim CPD As Integer = CInt(Me.SOCDatagrid.SelectedCells(10).Value.ToString)
            Dim CPU As Integer = CInt(Me.SOCDatagrid.SelectedCells(11).Value.ToString)
            Dim CPE As Integer = CInt(Me.SOCDatagrid.SelectedCells(12).Value.ToString)
            Dim CPR As Integer = CPD - CPU - CPE


            If CPR = 0 Then
                If DevComponents.DotNetBar.MessageBoxEx.Show("No. of prints remaining for search is zero.Do you want to generate the report?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
            End If

            ShowPleaseWaitForm()

            Me.Cursor = Cursors.WaitCursor
            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.Document.PageSetup.LeftMargin = 60
            WordApp.Selection.Document.PageSetup.RightMargin = 60
            WordApp.Selection.Document.PageSetup.BottomMargin = 60
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Paragraphs.Space1()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1

            Dim FileNo As String = Me.SOCDatagrid.SelectedCells(0).Value.ToString()
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            WordApp.Selection.TypeText("No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()
            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateDate(False))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)


            WordApp.Selection.TypeText(vbTab & "The Tester Inspector" & vbNewLine & vbTab & "Single Digit Fingerprint Bureau" & vbNewLine & vbTab & "................................................")

            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: Comparison of chance prints with your Bureau records - reg:-")

            WordApp.Selection.TypeText(vbNewLine)
            Dim ps As String = Me.SOCDatagrid.SelectedCells(5).Value.ToString
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If
            WordApp.Selection.TypeText(vbTab & "Ref: Cr.No. " & Me.SOCDatagrid.SelectedCells(6).Value.ToString() & " u/s " & Me.SOCDatagrid.SelectedCells(7).Value.ToString() & " of " & ps)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14
            WordApp.Selection.TypeText(vbTab & "I am forwarding herewith the photographs of the chance prints developed from the above referred scene of crime. The same may please be compared with your Bureau records and the result intimated to this office at the earliest. Details of the case are furnished below:")

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LeftIndent = 30
            WordApp.Selection.TypeText("Date of Occurrence" & vbTab & "- " & Me.SOCDatagrid.SelectedCells(4).Value.ToString() & vbNewLine)
            Dim po = Me.SOCDatagrid.SelectedCells(8).Value.ToString()
            po = po.Replace(vbCrLf, ", ")
            WordApp.Selection.TypeText("Place of Occurrence" & vbTab & "- " & po & vbNewLine)
            WordApp.Selection.TypeText("Modus Operandi" & vbTab & "- " & Me.SOCDatagrid.SelectedCells(16).Value.ToString() & vbNewLine)
            Dim pl = Me.SOCDatagrid.SelectedCells(17).Value.ToString()
            pl = pl.Replace(vbCrLf, ", ")
            Dim oldfont = WordApp.Selection.Font.Name
            WordApp.Selection.TypeText("Property Lost")
            If pl.Contains("`") Then
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Font.Name = "Rupee Foradian"
            End If
            WordApp.Selection.TypeText(vbTab & vbTab & "- " & pl & vbNewLine)
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.TypeText("Ridges" & vbTab & vbTab & vbTab & "- " & "Black/White")
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LeftIndent = 0
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
               
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            ClosePleaseWaitForm()

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub SuspectComparison()
        Try
            If (Me.SOCDatagrid.RowCount = 0) Or (Me.SOCDatagrid.SelectedRows.Count = 0) Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No record is selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub
            End If


            ShowPleaseWaitForm()

            Dim CPD As Integer = CInt(Me.SOCDatagrid.SelectedCells(10).Value.ToString)
            Dim CPR As Integer = CInt(Me.SOCDatagrid.SelectedCells(13).Value.ToString)

            Me.Cursor = Cursors.WaitCursor
            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.Document.PageSetup.LeftMargin = 60
            WordApp.Selection.Document.PageSetup.RightMargin = 60
            WordApp.Selection.Document.PageSetup.BottomMargin = 60
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Paragraphs.Space1()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1

            Dim FileNo As String = Me.SOCDatagrid.SelectedCells(0).Value.ToString()
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)
            WordApp.Selection.TypeText("No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()
            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateDate(False))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & IIf(ReportSentTo.StartsWith("The "), ReportSentTo, "The " & ReportSentTo))

            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)


            WordApp.Selection.TypeText(vbTab & "Sub: Comparison of chance print with FP slip of suspect - report - reg.")

            WordApp.Selection.TypeText(vbNewLine)
            Dim ps As String = Me.SOCDatagrid.SelectedCells(5).Value.ToString
            If Strings.Right(ps, 3) <> "P.S" Then
                ps = ps & " P.S"
            End If
            WordApp.Selection.TypeText(vbTab & "Ref: Cr.No. " & Me.SOCDatagrid.SelectedCells(6).Value.ToString() & " u/s " & Me.SOCDatagrid.SelectedCells(7).Value.ToString() & " of " & ps)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14

            WordApp.Selection.TypeText(vbTab & "Please refer to the above. The " & IIf(CPD = 1, "chance print", "chance prints") & " developed from the above scene of crime " & IIf(CPD = 1, "was", "were") & " compared with the fingerprint slips of the following suspect(s):")

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LeftIndent = 70
            WordApp.Selection.TypeText("1. " & vbNewLine)
            WordApp.Selection.TypeText("2. " & vbNewLine)
            WordApp.Selection.TypeText("3. " & vbNewLine)
            WordApp.Selection.TypeText("4. " & vbNewLine)
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LeftIndent = 0

            WordApp.Selection.TypeText(vbTab & "It is intimated that the " & IIf(CPD = 1, "chance print is", "chance prints are") & " NOT IDENTICAL with his/their finger impressions. This is for information.")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")
            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            ClosePleaseWaitForm()
            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub SOCRegister() Handles btnSOCRegister.Click
        On Error Resume Next
        boolCurrentSOC = False
        frmSOCRegister.Show()
        frmSOCRegister.WindowState = FormWindowState.Maximized
        frmSOCRegister.BringToFront()
    End Sub

    Private Sub CurrentSOCRegister() Handles btnCurrentSOC.Click
        On Error Resume Next
        boolCurrentSOC = True
        frmSOCRegister.Show()
        frmSOCRegister.WindowState = FormWindowState.Maximized
        frmSOCRegister.BringToFront()
    End Sub

    Private Sub SOCPhotoNotReceived() Handles btnPhotoNotRecieved.Click
        On Error Resume Next
        frmPhotoNotReceived.Show()
        frmPhotoNotReceived.WindowState = FormWindowState.Maximized
        frmPhotoNotReceived.BringToFront()
    End Sub


    Private Sub ShowSOCPrintRemainingCases() Handles btnPrintRemainingCases.Click
        On Error Resume Next
        FrmSOCPrintRemainingCases.Show()
        FrmSOCPrintRemainingCases.BringToFront()
    End Sub

    Private Sub DARegister() Handles btnDARegister.Click
        On Error Resume Next
        boolCurrentDA = False
        frmDARegister.Show()
        frmDARegister.WindowState = FormWindowState.Maximized
        frmDARegister.BringToFront()
    End Sub

    Private Sub CurrentDARegister() Handles btnCurrentDA.Click
        On Error Resume Next
        boolCurrentDA = True
        frmDARegister.Show()
        frmDARegister.WindowState = FormWindowState.Maximized
        frmDARegister.BringToFront()
    End Sub


    Private Sub ShowMonthWiseDAStatistics() Handles btnMonthWiseDAStatistics.Click
        On Error Resume Next
        frmMonthWiseDAStatistics.Show()
        frmMonthWiseDAStatistics.WindowState = FormWindowState.Maximized
        frmMonthWiseDAStatistics.BringToFront()
    End Sub

    Private Sub ShowPSWiseDAStatistics() Handles btnPSWiseDAStatistics.Click
        On Error Resume Next
        FrmDAPSWiseStatistics.Show()
        FrmDAPSWiseStatistics.WindowState = FormWindowState.Maximized
        FrmDAPSWiseStatistics.BringToFront()
    End Sub


    Private Sub ShowMonthWiseFPAStatistics() Handles btnMonthWiseFPAStatistics.Click
        On Error Resume Next
        FrmMonthWiseFPAStatistics.Show()
        FrmMonthWiseFPAStatistics.WindowState = FormWindowState.Maximized
        FrmMonthWiseFPAStatistics.BringToFront()
    End Sub


    Private Sub FPARegister() Handles btnFPARegister.Click
        On Error Resume Next
        boolCurrentFPA = False
        frmFPARegister.Show()
        frmFPARegister.WindowState = FormWindowState.Maximized
        frmFPARegister.BringToFront()
    End Sub

    Private Sub CurrentFPARegister() Handles btnCurrentFPA.Click
        On Error Resume Next
        boolCurrentFPA = True
        frmFPARegister.Show()
        frmFPARegister.WindowState = FormWindowState.Maximized
        frmFPARegister.BringToFront()
    End Sub



    Private Sub ShowFPAApplicationForm() Handles btnFPAApplicationForm.Click
        Try

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\FPAApplicationForm.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ShowPleaseWaitForm()

            Me.Cursor = Cursors.WaitCursor
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            wdBooks("Year1").Range.Text = Year(Today) ' get year 
            wdBooks("District").Range.Text = ShortDistrictName
            wdBooks("Year2").Range.Text = Year(Today)
            wdBooks("Office").Range.Text = UCase(FullOfficeName & ", " & FullDistrictName)

            ClosePleaseWaitForm()
            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub FPANotice() Handles btnFPANotice.Click
        Try
            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\FPANotice.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ShowPleaseWaitForm()
            Me.Cursor = Cursors.WaitCursor
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            wdBooks("Office").Range.Text = UCase(FullOfficeName & ", " & FullDistrictName)

            ClosePleaseWaitForm()

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateFPASlip() Handles btnFPAGenerateSlipFormContext.Click, btnFPAGenerateSlipForm.Click
        GenerateFPASlipForm(False)
    End Sub

    Private Sub GenerateBlankFPAForm() Handles btnFPABlankSlipForm.Click
        GenerateFPASlipForm(True)
    End Sub

    Private Sub GenerateFPASlipForm(BlankForm As Boolean)

        Try

            If (Me.FPADataGrid.RowCount = 0) Or (Me.FPADataGrid.SelectedRows.Count = 0) Then
                DevComponents.DotNetBar.MessageBoxEx.Show("No record is selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\FPASlipForm.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ShowPleaseWaitForm()
            Me.Cursor = Cursors.WaitCursor
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            Dim FileNo As String = Me.FPADataGrid.SelectedCells(0).Value.ToString()
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/FPA/" & line(1)
            FileNo = FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName

            wdBooks("Office").Range.Text = UCase(FullOfficeName & ", " & FullDistrictName)
            If BlankForm Then
                wdBooks("Name").Range.Text = ""
                wdBooks("Address").Range.Text = ""
                wdBooks("Passport").Range.Text = ""
                wdBooks("FileNo").Range.Text = "                            "
                wdBooks("Date").Range.Text = "                "
                wdBooks("NameAddress").Range.Text = "                                        "
                wdBooks("PassportNo").Range.Text = "              "
                wdBooks("Reason").Range.Text = ""
            Else
                wdBooks("Name").Range.Text = Me.FPADataGrid.SelectedCells(3).Value.ToString.ToUpper
                wdBooks("Address").Range.Text = Me.FPADataGrid.SelectedCells(4).Value
                wdBooks("Passport").Range.Text = Me.FPADataGrid.SelectedCells(5).Value
                wdBooks("FileNo").Range.Text = FileNo
                wdBooks("Date").Range.Text = Format(Me.FPADataGrid.SelectedCells(2).Value, "dd-MMM-yyyy")
                wdBooks("NameAddress").Range.Text = Me.FPADataGrid.SelectedCells(3).Value & ", " & Me.FPADataGrid.SelectedCells(4).Value.ToString.Replace(vbNewLine, ", ")
                wdBooks("PassportNo").Range.Text = Me.FPADataGrid.SelectedCells(5).Value
                wdBooks("Reason").Range.Text = Me.FPADataGrid.SelectedCells(12).Value
            End If

            ClosePleaseWaitForm()

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CDRegister() Handles btnCDRegister.Click
        On Error Resume Next
        boolCurrentCD = False
        frmCDRegister.Show()
        frmCDRegister.WindowState = FormWindowState.Maximized
        frmCDRegister.BringToFront()
    End Sub


    Private Sub CurrentCDRegister() Handles btnCurrentCD.Click
        On Error Resume Next
        boolCurrentCD = True
        frmCDRegister.Show()
        frmCDRegister.WindowState = FormWindowState.Maximized
        frmCDRegister.BringToFront()
    End Sub

    Private Sub ShowDASlipStatementAllPS() Handles btnDAStatementAllPS.Click, btnMonthlyDA.Click

        On Error Resume Next
        frmDAStatement.ShowDialog()
    End Sub


    Private Sub ShowFPAStstistics() Handles btnFPAStatement.Click

        On Error Resume Next
        frmFPAStatement.ShowDialog()
    End Sub


    Private Sub GenerateRevenueIncomeStatement() Handles btnRevenueCollectionStatement.Click
        Me.Cursor = Cursors.WaitCursor
        frmFPAStatement.ShowDialog()
    End Sub

    Private Sub ShowMonthlyPerformance() Handles btnMonthlyPerformance.Click, btnMonthlyWorkdoneStmt.Click
        On Error Resume Next
        frmMonthlyPerformance.Show()
        frmMonthlyPerformance.WindowState = FormWindowState.Maximized
        frmMonthlyPerformance.BringToFront()
    End Sub


    Private Sub ShowQuarterlyPerformance() Handles btnQuarterlyPerformance.Click
        On Error Resume Next
        frmQuarterlyPerformance.Show()
        frmQuarterlyPerformance.WindowState = FormWindowState.Maximized
        frmQuarterlyPerformance.BringToFront()
    End Sub

    Private Sub ShowAnnualPerformance() Handles btnAnnualPerformance.Click
        On Error Resume Next
        frmAnnualPerformance.Show()
        frmAnnualPerformance.WindowState = FormWindowState.Maximized
        frmAnnualPerformance.BringToFront()
    End Sub
    Private Sub ShowIndividualPerformance() Handles btnIndividualPerformance.Click, btnMonthlyIndividual.Click

        On Error Resume Next
        FrmIndividualPerformance.ShowDialog()
    End Sub

    Private Sub ConsolidateWorkDone() Handles btnConsolidateWorkDone.Click
        On Error Resume Next
        If FullDistrictName = "" Or FullDistrictName = "FullDistrict" Then
            MessageBoxEx.Show("'Full District Name' is empty. Cannot proceed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.Cursor = Cursors.Default
        frmPerformance_RangeConsolidate.ShowDialog()
    End Sub
    Private Sub ShowPSList() Handles btnPSList.Click
        On Error Resume Next
        frmPSList.Show()
        frmPSList.WindowState = FormWindowState.Maximized
        frmPSList.BringToFront()
    End Sub

    Private Function GenerateDate(ByVal ShowDate As Boolean)
        On Error Resume Next
        Dim dt = ReportSentDate
        If ShowDate Then
            Return Format(dt, "dd/MM/yyyy")
        Else
            dt = Today
            Dim m As String = Month(dt)
            If m < 10 Then m = "0" & m
            Dim y As String = Year(dt)
            Dim d As String = m & "/" & y
            Return d
        End If

    End Function

    Private Sub ACRegister() Handles btnACRegister.Click
        On Error Resume Next
        boolCurrentAC = False
        FrmACRegister.Show()
        FrmACRegister.WindowState = FormWindowState.Maximized
        FrmACRegister.BringToFront()
    End Sub

    Private Sub CurrentACRegister() Handles btnCurrentAC.Click
        On Error Resume Next
        boolCurrentAC = True
        FrmACRegister.Show()
        FrmACRegister.WindowState = FormWindowState.Maximized
        FrmACRegister.BringToFront()
    End Sub

    Private Sub IDRegister() Handles btnIDRegister.Click
        On Error Resume Next
        boolCurrentID = False
        FrmIDRegister.Show()
        FrmIDRegister.WindowState = FormWindowState.Maximized
        FrmIDRegister.BringToFront()
    End Sub

    Private Sub CurrentIDRegister() Handles btnCurrentID.Click
        On Error Resume Next
        boolCurrentID = True
        FrmIDRegister.Show()
        FrmIDRegister.WindowState = FormWindowState.Maximized
        FrmIDRegister.BringToFront()
    End Sub


    Private Sub btnAttendanceStmt_Click(sender As Object, e As EventArgs) Handles btnAttendanceStmt1.Click, btnAttendanceStmt2.Click, btnAttendanceStmt.Click
        frmAttendanceStmt.ShowDialog()

    End Sub

#End Region


#Region "COVERING LETTERS"
    Sub CoveringLetters(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttendance.Click, btnSOCCL.Click, btnTABill.Click, btnRBWarrant.Click, btnIndividualPerformanceCL.Click, btnRBNilLetter.Click, btnVigilanceLetter.Click, btnGraveCrimeCL.Click, btnIdentificationStmtCL.Click, btnProjectLetter.Click, btnMonthlyStatementCL.Click

        Try

            ' frmPleaseWait.Show
            ShowPleaseWaitForm()

            Me.Cursor = Cursors.WaitCursor
            Dim bodytext As String = vbNullString
            Dim subject As String = vbNullString


            Dim PdlNumber As String = ""

            Dim m1 As String = Month(Today)
            Dim m2 As String = m1 - 1

            Dim y1 As String = Year(Today)
            Dim y2 As String = y1

            If m2 = 0 Then
                m2 = 12
                y2 = y1 - 1
            End If

            Dim sFileName As String = "Statement.docx"

            Select Case DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Name

                Case btnAttendance.Name
                    subject = "Abstract of Attendance Register - submitting of - reg:- "
                    bodytext = "I am submitting herewith the abstract of the Attendance Register of this office for the period from 11/" & IIf(m2.Length = 1, "0" & m2, m2) & "/" & y2 & " to 10/" & IIf(m1.Length = 1, "0" & m1, m1) & "/" & y1 & " for favour of further necessary action."
                    PdlNumber = PdlAttendance
                    sFileName = "CL - Attendance Statement.docx"
                Case btnSOCCL.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "SOC and DA Slip Statements - " & m2 & " - submitting of - reg:- "
                    bodytext = "I am submitting herewith the SOC and DA Slip statements for the month of " & m2 & " for favour of necessary action."
                    PdlNumber = PdlSOCDAStatement
                    sFileName = "CL - SoC Statement.docx"
                Case btnTABill.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "TA Bills of staff - " & m2 & " - submitting of - reg:- "
                    bodytext = "I am forwarding herewith the TA Bills of the staff of this unit for the month of " & m2 & " for favour of further necessary action."
                    PdlNumber = PdlTABill
                    sFileName = "CL - TA Bill.docx"
                Case btnRBWarrant.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Bus and Railway Warrants Statement - " & m2 & " - submitting of - reg:- "
                    bodytext = "I am submitting herewith the Bus and Railway Warrant statements for the month of " & m2 & " for favour of necessary action."
                    PdlNumber = PdlRBWarrant
                    sFileName = "CL - Rail Bus Warrant Statement.docx"
                Case btnIndividualPerformanceCL.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Individual performance statement - " & m2 & " - submitting of - reg:- "
                    bodytext = "I am submitting herewith the Individual performance statement of the staff of this unit for the month of " & m2 & " for favour of necessary action."
                    PdlNumber = PdlIndividualPerformance
                    sFileName = "CL - Individual Performance Statement.docx"
                Case btnRBNilLetter.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Bus and Railway Warrants Statement - " & m2 & " - submitting of - reg:- "
                    bodytext = "No Bus and Railway Warrants were used in the month of " & m2 & ". This is for favour of information and necessary action."
                    PdlNumber = PdlRBWarrant
                    sFileName = "CL - Rail Bus Warrant Nil Statement.docx"
                Case btnVigilanceLetter.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Vigilance case against staff – " & m2 & " - report submitting of - reg:- "
                    bodytext = "No case has been Registered or Investigated or being investigated by Local Police / CBCID / Vigilance Department against any of the staff working in this unit during the month of " & m2 & ". This is for favour of information and necessary action."
                    PdlNumber = PdlVigilanceCase
                    sFileName = "CL - Vigilance Case Statement.docx"
                Case btnGraveCrimeCL.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Grave Crime Details – " & m2 & " - submitting of - reg:- "
                    bodytext = "I am submitting herewith the Grave Crime statement for the month of " & m2 & " for favour of necessary action."
                    PdlNumber = PdlGraveCrime
                    sFileName = "CL - Grave Crime Statement.docx"
                Case btnIdentificationStmtCL.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Identification Statement – " & m2 & " - submitting of - reg:- "
                    bodytext = "I am submitting herewith the Identification statement for the month of " & m2 & " for favour of necessary action."
                    PdlNumber = PdlIdentificationStatement
                    sFileName = "CL - Identification Statement.docx"
                Case btnProjectLetter.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Details of Projects/Buildings ready for inauguration – submitting of - reg:- "
                    bodytext = "Kind attention is invited to the subject. I am submitting a NIL report in this regard. This is for favour of information and necessary action."
                    PdlNumber = PdlBuildingProject
                    sFileName = "CL - Projects Buildings.docx"
                Case btnMonthlyStatementCL.Name
                    m2 = MonthName(m2) & " " & y2
                    subject = "Monthly statements – submitting of - reg:- "
                    bodytext = "I am submitting herewith the monthly statements for the month of " & m2 & " for favour of information and necessary action."
                    PdlNumber = PdlMonthlyStatements
                    sFileName = "CL - Monthly Statements.docx"
            End Select


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Paragraphs.Space1()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.TypeText("No. " & PdlNumber & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName)
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()

            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateDate(False))
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "The Director" & vbNewLine & vbTab & "Fingerprint Bureau" & vbNewLine & vbTab & "Thiruvananthapuram")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: " & subject)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 15
            WordApp.Selection.TypeText(vbTab & bodytext)



            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & Me.IODatagrid.Rows(0).Cells(1).Value & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            ClosePleaseWaitForm()

            Dim sFullFileName As String = FileIO.SpecialDirectories.MyDocuments & "\" & sFileName
            If Not FileInUse(sFullFileName) Then aDoc.SaveAs(sFullFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default

            Exit Sub

        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try


    End Sub
    Private Sub GenerateNilReports(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBNiliAPS.Click, btnVigilanceIAPS.Click, btnProjectIAPS.Click
        Try
            Dim TemplateFile As String = ""
            Dim blmonth2 As Boolean = True

            Select Case DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Name
                Case btnRBNiliAPS.Name
                    TemplateFile = strAppUserPath & "\WordTemplates\WarrantStatement.docx"
                    blmonth2 = True
                Case btnVigilanceIAPS.Name
                    TemplateFile = strAppUserPath & "\WordTemplates\VigilanceCaseStatement.docx"
                    blmonth2 = True
                Case btnProjectIAPS.Name
                    TemplateFile = strAppUserPath & "\WordTemplates\ProjectStatement.docx"
                    blmonth2 = False
            End Select

            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ShowPleaseWaitForm()
            Me.Cursor = Cursors.WaitCursor

            Dim m1 As String = Month(Today)
            Dim m2 As String = m1 - 1

            Dim y1 As String = Year(Today)
            Dim y2 As String = y1

            If m2 = 0 Then
                m2 = 12
                y2 = y1 - 1
            End If

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdDoc.Range.NoProofing = 1

            wdBooks("unit").Range.Text = UCase(FullDistrictName)
            wdBooks("month1").Range.Text = MonthName(m2).ToUpper & " " & y2
            If blmonth2 Then wdBooks("month2").Range.Text = MonthName(m2) & " " & y2

            ClosePleaseWaitForm()

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
        End Try
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub
#End Region


#Region "TOUR NOTE"

    Private Sub GenerateTourNote() Handles btnTourNote.Click
        On Error Resume Next

        FrmTourNote.WindowState = FormWindowState.Maximized
        FrmTourNote.Show()
    End Sub
#End Region


#Region "IDR REGISTER"

    Private Sub ShowIDinSoCRegister() Handles btnIDRShowInSoCRegister.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SOCRegisterTableAdapter.FillBySOCNumber(Me.FingerPrintDataSet.SOCRegister, Me.JoinedIDRDataGrid.SelectedCells(1).Value.ToString)
            Me.TabControl.SelectedTab = SOCTabItem
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub


    Private Sub btnSOCShowInIDRRegister_Click(sender As Object, e As EventArgs) Handles btnSOCShowInIDRRegister.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.JoinedIDRTableAdapter.FillBySOCNumber(Me.FingerPrintDataSet.JoinedIDR, Me.SOCDatagrid.SelectedCells(0).Value.ToString)
            Me.TabControl.SelectedTab = IDRTabItem
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub
#End Region

    '----------------------------------------CREATE TABLE-------------------------------------------------------
#Region "CREATE TABLE"

    Public Sub CreateSOCReportRegisterTable()
        Try
            If DoesTableExist("SOCReportRegister", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE SOCReportRegister (SerialNo integer primary key, SOCNumber VARCHAR(50)WITH COMPRESSION ,  SOCNumberWithoutYear integer, DateOfInspection Date, PoliceStation VARCHAR(255) WITH COMPRESSION , CrimeNumber VARCHAR(50) WITH COMPRESSION , InspectingOfficer VARCHAR(255) WITH COMPRESSION , ReportSentTo VARCHAR(255) WITH COMPRESSION , DateOfReportSent Date, NatureOfReports VARCHAR(255) WITH COMPRESSION , DespatchNumber VARCHAR(255) WITH COMPRESSION , Remarks VARCHAR(255) WITH COMPRESSION )", con)

            cmd.ExecuteNonQuery()
            Application.DoEvents()
            InsertOrUpdateLastModificationDate(Now, "Created SOC Report Register")
        Catch ex As Exception
            'ShowErrorMessage(ex)
        End Try
    End Sub

    Public Sub CreateOfficerTable()
        Try
            If DoesTableExist("OfficerTable", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE OfficerTable (OfficerID integer primary key, TI VARCHAR(255)WITH COMPRESSION , FPE1 VARCHAR(255)WITH COMPRESSION , FPE2 VARCHAR(255)WITH COMPRESSION , FPE3 VARCHAR(255)WITH COMPRESSION , FPS VARCHAR(255)WITH COMPRESSION , Photographer VARCHAR(255)WITH COMPRESSION )", con)

            cmd.ExecuteNonQuery()
            Application.DoEvents()
            InsertOrUpdateLastModificationDate(Now, "Created Officer Table")
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Public Sub CreateSettingsTable()
        Try
            If DoesTableExist("Settings", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE Settings (SettingsID integer primary key, FullDistrictName VARCHAR(255)WITH COMPRESSION , ShortDistrictName VARCHAR(255)WITH COMPRESSION , FullOfficeName VARCHAR(255)WITH COMPRESSION , ShortOfficeName VARCHAR(255)WITH COMPRESSION , FPImageImportLocation VARCHAR(255)WITH COMPRESSION , CPImageImportLocation VARCHAR(255)WITH COMPRESSION , PdlAttendance VARCHAR(2)WITH COMPRESSION , PdlIndividualPerformance VARCHAR(2)WITH COMPRESSION , PdlRBWarrant VARCHAR(2)WITH COMPRESSION , PdlSOCDAStatement VARCHAR(2)WITH COMPRESSION , PdlTABill VARCHAR(2)WITH COMPRESSION , PdlFPAttestation VARCHAR(2)WITH COMPRESSION )", con)

            cmd.ExecuteNonQuery()
            Application.DoEvents()
            InsertOrUpdateLastModificationDate(Now, "Created Settings Table")
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Public Sub CreateLastModificationTable()
        Try
            If DoesTableExist("LastModification", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE LastModification (ID integer primary key, LastModifiedDate Date, LastModificationDetails VARCHAR(255)WITH COMPRESSION)", con)

            cmd.ExecuteNonQuery()
            Application.DoEvents()

            If DoesTableExist("LastModification", sConString) Then
                InsertOrUpdateLastModificationDate(Now, "Created Last Modification Details Table")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Public Sub CreateCommonSettingsTable()
        Try
            If DoesTableExist("CommonSettings", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE CommonSettings (SettingsName VARCHAR(255)WITH COMPRESSION , SettingsValue VARCHAR(255)WITH COMPRESSION, Remarks VARCHAR(255)WITH COMPRESSION)", con)

            cmd.ExecuteNonQuery()
            Application.DoEvents()

            InsertOrUpdateLastModificationDate(Now, "Created Common Settings Table")


        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Public Sub CreateIdentificationRegisterTable()
        Try
            If DoesTableExist("IdentificationRegister", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE IdentificationRegister (SlNumber COUNTER PRIMARY KEY, IdentificationNumber VARCHAR(10) WITH COMPRESSION , SOCNumber VARCHAR(50) WITH COMPRESSION, IdentificationDate Date, IdentifiedBy VARCHAR(255) WITH COMPRESSION, CPsIdentified VARCHAR(3) WITH COMPRESSION, NoOfCulpritsIdentified VARCHAR(3) WITH COMPRESSION,  CulpritName VARCHAR(255)  WITH COMPRESSION, Address MEMO WITH COMPRESSION, FingersIdentified VARCHAR(255) WITH COMPRESSION, HenryClassification VARCHAR(255) WITH COMPRESSION, DANumber VARCHAR(255) WITH COMPRESSION, IdentifiedFrom VARCHAR(255) WITH COMPRESSION, IdentificationDetails MEMO WITH COMPRESSION, IDRNumber Integer, PreviousCaseDetails VARCHAR(255) WITH COMPRESSION, COID VARCHAR(255) WITH COMPRESSION)", con)

            cmd.ExecuteNonQuery()

            CopyValuesToIdentificationRegisterTable()
            CreateCulpritsRegisterTable()

            RemoveNullFromIdentificationRegister()
            Thread.Sleep(1000)
            CopyValuesToCulpritsRegisterTable()
            InsertOrUpdateLastModificationDate(Now, "Created Identification Register")
        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Public Sub CopyValuesToIdentificationRegisterTable()
        Try
            If Not DoesTableExist("IdentificationRegister", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("INSERT INTO IdentificationRegister (IdentificationNumber, SOCNumber, IdentificationDate, CPsIdentified, IdentifiedBy, CulpritName, IdentificationDetails, IDRNumber) SELECT IdentificationNumber, SOCNumber, IdentificationDate, CPsIdentified, IdentifiedBy, IdentifiedAs, Remarks, VAL(IdentificationNumber) FROM SOCRegister where FileStatus = 'Identified'", con)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Public Sub CreateCulpritsRegisterTable()
        Try
            If DoesTableExist("CulpritsRegister", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE CulpritsRegister (SlNumber COUNTER PRIMARY KEY, IdentificationNumber VARCHAR(10) WITH COMPRESSION, CulpritName VARCHAR(255)  WITH COMPRESSION, Address MEMO WITH COMPRESSION, CPsIdentified VARCHAR(3) WITH COMPRESSION,  FingersIdentified VARCHAR(255) WITH COMPRESSION, HenryClassification VARCHAR(255) WITH COMPRESSION, DANumber VARCHAR(255) WITH COMPRESSION, IdentifiedFrom VARCHAR(255) WITH COMPRESSION, IdentificationDetails MEMO WITH COMPRESSION, PreviousCaseDetails VARCHAR(255) WITH COMPRESSION, COID VARCHAR(255) WITH COMPRESSION)", con)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Public Sub CreateRevenueCollectionTable()
        Try
            If DoesTableExist("RevenueCollection", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE RevenueCollection (ID COUNTER PRIMARY KEY, FPMonth NUMBER, Amount Number)", con)

            cmd.ExecuteNonQuery()
            InsertOrUpdateLastModificationDate(Now, "Created Revenue Collection Register")
        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Public Sub CopyValuesToCulpritsRegisterTable()
        Try
            If Not DoesTableExist("CulpritsRegister", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("INSERT INTO CulpritsRegister (IdentificationNumber, CulpritName, Address, CPsIdentified, FingersIdentified, HenryClassification, DANumber, IdentifiedFrom, IdentificationDetails, PreviousCaseDetails) SELECT IdentificationNumber, CulpritName, Address, CPsIdentified, FingersIdentified, HenryClassification, DANumber, IdentifiedFrom, IdentificationDetails, PreviousCaseDetails, COID FROM IdentificationRegister", con)

            cmd.ExecuteNonQuery()

            Thread.Sleep(1000)
            RemoveNullFromCulpritRegister()

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Private Sub RemoveNullFromCulpritRegister()
        Try
            If Not DoesTableExist("CulpritsRegister", sConString) Then
                Exit Sub
            End If

            If Me.CulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.CulpritsRegisterTableAdapter1.Connection.Close()
            Me.CulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.CulpritsRegisterTableAdapter1.Connection.Open()

            Me.CulpritsRegisterTableAdapter1.UpdateQueryRemoveNullFromPreviousCase("")
            Me.CulpritsRegisterTableAdapter1.RemoveNullFromCOID("")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveNullFromIdentificationRegister()
        Try

            If Not DoesTableExist("IdentificationRegister", sConString) Then
                Exit Sub
            End If

            If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
            Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.IdentificationRegisterTableAdapter1.Connection.Open()

            Me.IdentificationRegisterTableAdapter1.RemoveNullFromNoOfCulprits("")

            Me.IdentificationRegisterTableAdapter1.RemoveNullFromAddress("")

            Me.IdentificationRegisterTableAdapter1.RemoveNullFromFingersIdentified("")

            Me.IdentificationRegisterTableAdapter1.RemoveNullFromHenryClassification("")

            Me.IdentificationRegisterTableAdapter1.RemoveNullFromDANumber("")

            Me.IdentificationRegisterTableAdapter1.RemoveNullFromIdentifiedFrom("")

            Me.IdentificationRegisterTableAdapter1.UpdateQuerySetCulpritCount("1", "1")
            Me.IdentificationRegisterTableAdapter1.RemoveNullFromPreviousCase("")
            Me.IdentificationRegisterTableAdapter1.RemoveNullFromIdentificationDetails("")
            Me.IdentificationRegisterTableAdapter1.RemoveNullFromCOID("")
            blIdentificationRegisterUpdateFailed = False

            LoadIDRRecords()
        Catch ex As Exception
            blIdentificationRegisterUpdateFailed = True
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "UpdateNullFields", "1", Microsoft.Win32.RegistryValueKind.String)
        End Try
    End Sub

    Private Sub RemoveSOCRegisterIDFields()
        Try
            If Not DoesTableExist("IdentificationRegister", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister DROP COLUMN IdentificationDate, IdentifiedBy, IdentifiedAs, Remarks, IdentificationNumber", con)

            cmd.ExecuteNonQuery()
            Application.DoEvents()

            InsertOrUpdateLastModificationDate(Now, "Modified Identification Register")


        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Private Sub CreateChalanRegisterTable()
        Try
            If DoesTableExist("ChalanTable", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE ChalanTable (SerialNumber COUNTER PRIMARY KEY, FPNumber VARCHAR(50) WITH COMPRESSION, FPDate Date, ChalanNumber VARCHAR(50) WITH COMPRESSION,  ChalanDate Date, HeadOfAccount VARCHAR(255) WITH COMPRESSION, Treasury VARCHAR(255) WITH COMPRESSION, AmountRemitted VARCHAR(5) WITH COMPRESSION)", con)

            cmd.ExecuteNonQuery()
            CopyValuesToChalanTable()

        Catch ex As Exception
            ShowErrorMessage(ex)
            ' blCreateTableFailed = True
        End Try
    End Sub

    Private Sub CopyValuesToChalanTable()
        Try
            If Not DoesTableExist("ChalanTable", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("INSERT INTO ChalanTable (FPNumber, FPDate, ChalanNumber, ChalanDate, HeadOfAccount, Treasury, AmountRemitted) SELECT FPNumber, FPDate, ChalanNumber, ChalanDate, HeadOfAccount, Treasury, AmountRemitted FROM FPAttestationRegister", con)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Private Sub ReCopyValuesToChalanTable()
        Try
            If Not DoesTableExist("ChalanTable", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("INSERT INTO ChalanTable (FPNumber, FPDate, ChalanNumber, ChalanDate, HeadOfAccount, Treasury, AmountRemitted) SELECT FPNumber, FPDate, ChalanNumber, ChalanDate, HeadOfAccount, Treasury, AmountRemitted FROM FPAttestationRegister WHERE NOT EXISTS (SELECT * FROM ChalanTable WHERE (FPAttestationRegister.FPNumber=ChalanTable.FPNumber))", con)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Private Sub CreateSupportingStaffTable()
        Try
            If DoesTableExist("SupportingStaff", sConString) Then
                Exit Sub
            End If

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd = New OleDb.OleDbCommand("Create TABLE SupportingStaff (SlNo COUNTER PRIMARY KEY, StaffName VARCHAR(255) WITH COMPRESSION, Designation VARCHAR(255) WITH COMPRESSION,  PEN VARCHAR(255) WITH COMPRESSION)", con)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            '  blCreateTableFailed = True
            '   ShowErrorMessage(ex)

        End Try
    End Sub
    Public Sub ModifyTables()
        On Error Resume Next
        Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
        con.Open()


        If DoesFieldExist("SOCRegister", "GraveCrime", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN GraveCrime YesNo", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "FileStatus", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN FileStatus VARCHAR(25) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "IdentifiedBy", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN IdentifiedBy VARCHAR(255) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "IdentificationDate", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN IdentificationDate DATE", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "CPsIdentified", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN CPsIdentified VARCHAR(3) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "IdentifiedAs", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN IdentifiedAs VARCHAR(255) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "GraveCrime", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN GraveCrime YesNo", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("SOCRegister", "IdentificationNumber", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ADD COLUMN IdentificationNumber VARCHAR(10) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If


        If DoesFieldExist("FPAttestationRegister", "ChalanDate", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE FPAttestationRegister ADD COLUMN ChalanDate DATE", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("FPAttestationRegister", "HeadOfAccount", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE FPAttestationRegister ADD COLUMN HeadOfAccount VARCHAR(255) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("PoliceStationList", "SHO", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE PoliceStationList ADD COLUMN SHO VARCHAR(5) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        Dim UpdateSettingsTable As Boolean = False
        If DoesFieldExist("Settings", "PdlGraveCrime", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE Settings ADD COLUMN PdlGraveCrime VARCHAR(2) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
            UpdateSettingsTable = True
        End If

        If DoesFieldExist("Settings", "PdlVigilanceCase", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE Settings ADD COLUMN PdlVigilanceCase VARCHAR(2) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("Settings", "PdlWeeklyDiary", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE Settings ADD COLUMN PdlWeeklyDiary VARCHAR(2) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("IdentificationRegister", "COID", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE IdentificationRegister ADD COLUMN COID VARCHAR(255) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("CulpritsRegister", "COID", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE CulpritsRegister ADD COLUMN COID VARCHAR(255) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If DoesFieldExist("LastModification", "LastModificationDetails", sConString) = False Then
            Dim cmd = New OleDb.OleDbCommand("ALTER TABLE LastModification ADD COLUMN LastModificationDetails VARCHAR(255) WITH COMPRESSION", con)
            cmd.ExecuteNonQuery()
        End If

        If UpdateSettingsTable Then
            Dim id = 1
            Me.SettingsTableAdapter1.UpdateNullFields(PdlGraveCrime, PdlVigilanceCase, PdlWeeklyDiary, id)
        End If

        ' AlterSOCRemarksColumn()
        ' AlterSOCComparisoanDetailsColumn()
        con.Close()

        InsertOrUpdateLastModificationDate(Now, "Modified Various Registers")
        ' MsgBox(Err.Description)
    End Sub

    Private Sub AlterSOCRemarksColumn()
        Try
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd1 = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ALTER COLUMN Remarks MEMO", con)
            cmd1.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AlterSOCComparisoanDetailsColumn()
        Try
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd1 = New OleDb.OleDbCommand("ALTER TABLE SOCRegister ALTER COLUMN ComparisonDetails MEMO", con)
            cmd1.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Function DoesTableExist(ByVal tblName As String, ByVal cnnStr As String) As Boolean
        Try
            ' Open connection to the database
            Dim dbConn As New OleDb.OleDbConnection(cnnStr)
            dbConn.Open()


            Dim restrictions(3) As String
            restrictions(2) = tblName
            Dim dbTbl As DataTable = dbConn.GetSchema("Tables", restrictions)

            If dbTbl.Rows.Count = 0 Then
                'Table does not exist
                DoesTableExist = False
            Else
                'Table exists
                DoesTableExist = True
            End If

            dbTbl.Dispose()
            dbConn.Close()
            dbConn.Dispose()
        Catch ex As Exception
            DoesTableExist = False
        End Try

    End Function


#End Region


#Region "ANNUAL STATISTICS"
    Private Sub AnnualStatistics() Handles btnAnnualStatics.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        frmAnnualStatistics.StartPosition = FormStartPosition.CenterParent
        frmAnnualStatistics.Text = "Annual Statistics"
        frmAnnualStatistics.TitleText = "<b>Annual Statistics</b>"
        frmAnnualStatistics.ShowDialog()
        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "FORM SETTINGS"

    Private Sub frmMainInterface_StyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.StyleChanged
        On Error Resume Next
        ChangeCursor(Cursors.Default)
    End Sub

    Public Sub ChangeCursor(cr As Cursor)
        On Error Resume Next

        RibbonControl1.Cursor = cr

        Me.Cursor = cr
        Me.btnNewEntry.Cursor = cr
        Me.btnOpen.Cursor = cr
        Me.btnEdit.Cursor = cr
        Me.btnDelete.Cursor = cr
        Me.btnViewReports.Cursor = cr
        Me.btnReload.Cursor = cr
        Me.btnShowHideFields.Cursor = cr
        Me.btnOnlineBackup.Cursor = cr
        Me.btnLocalBackup.Cursor = cr
        Me.btnAbout.Cursor = cr
        Me.btnExit.Cursor = cr
        Me.RibbonBar1.Cursor = cr
        Me.RibbonBar2.Cursor = cr
        Me.RibbonBar3.Cursor = cr
        Me.RibbonBar4.Cursor = cr
        Me.RibbonBar5.Cursor = cr
        Me.RibbonBar6.Cursor = cr
        Me.RibbonBar7.Cursor = cr
        Me.RibbonBar9.Cursor = cr
        Me.RibbonBar10.Cursor = cr
        Me.RibbonBar11.Cursor = cr
        Me.RibbonBar13.Cursor = cr
        Me.RibbonControl1.Cursor = cr
        Me.RibbonPanel1.Cursor = cr
        Me.RibbonPanel2.Cursor = cr
        Me.RibbonTabItem6.Cursor = cr
        Me.tabHome.Cursor = cr
        Me.SOCDatagrid.Cursor = cr
        Me.DADatagrid.Cursor = cr
        Me.RSOCDatagrid.Cursor = cr
        Me.FPADataGrid.Cursor = cr
        Me.CDDataGrid.Cursor = cr
        Me.IDDatagrid.Cursor = cr
        Me.ACDatagrid.Cursor = cr
        Me.PSDataGrid.Cursor = cr
        Me.IODatagrid.Cursor = cr
        Me.pnlRegisterName.Cursor = cr
        Me.TabControl.Cursor = cr
        Me.TableLayoutPanel1.Cursor = cr
        Me.PanelEx1.Cursor = cr

    End Sub


    Private Sub ItemPanel1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ItemPanel1.Leave
        ItemPanel1.Hide()
    End Sub


#End Region


#Region "GENERAL SETTINGS"

    Private Sub OpenRegedit() Handles btnOpenRegedit.Click
        Try
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Applets\Regedit", "LastKey", strGeneralSettingsPath)
            Process.Start("regedit")
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub btnOpenStatementsFolder_Click(sender As Object, e As EventArgs) Handles btnOpenStatementsFolder.Click
        Try
            Call Shell("explorer.exe " & SuggestedLocation, AppWinStyle.NormalFocus)

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub
    Private Sub OpenTemplatesFolder(sender As Object, e As EventArgs) Handles btnOpenTemplatesFolder.Click
        Try
            Dim TemplatesFolder As String = strAppUserPath & "\WordTemplates"


            If FileIO.FileSystem.DirectoryExists(TemplatesFolder) Then
                Call Shell("explorer.exe " & TemplatesFolder, AppWinStyle.NormalFocus)
            Else
                MessageBoxEx.Show("The folder does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        Cursor = Cursors.WaitCursor
        frmAbout.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub IncrementCircularProgress(increment As Integer)
        frmSplashScreen.IncrementProgressBarValue(increment)
    End Sub

#End Region


    '---------------------------------------------BACKUP DATABASE MANIPULATION-----------------------------------

#Region "LOCAL AUTO BACKUP LOCATION"

    Private Sub SetBackupPath(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeBackupFolder.Click
        Try
            Dim Destination As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")
            Me.FolderBrowserDialog1.ShowNewFolderButton = True
            Me.FolderBrowserDialog1.Description = "Select Folder for Database Backup"
            Me.FolderBrowserDialog1.SelectedPath = Destination
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
                SelectedPath = SelectedPath.Replace("\\", "\")
                My.Computer.Registry.SetValue(strGeneralSettingsPath, "BackupPath", SelectedPath, Microsoft.Win32.RegistryValueKind.String)
                ShowDesktopAlert("Backup Folder changed!")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub btnOpenBackupLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBackupFolder.Click
        On Error Resume Next
        Dim BackupPath As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")

        If FileIO.FileSystem.DirectoryExists(BackupPath) Then
            Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
        Else
            FileIO.FileSystem.CreateDirectory(BackupPath)
            Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
        End If
    End Sub

#End Region


#Region "ONLINE CREDENTIALS AND FOLDER ID INTERNAL FILE TRANSFER FOLDER"

    Private Sub CopyCredentialFiles()
        Try

            CredentialFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\" & strAppName

            JsonPath = CredentialFilePath & "\FISServiceAccount.json"

            If Not FileIO.FileSystem.DirectoryExists(CredentialFilePath) Then
                My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
            End If

            Dim jsonfile As String = CredentialFilePath & "\FISServiceAccount.json"
            If Not FileIO.FileSystem.FileExists(jsonfile) Then 'copy from application folder
                FileSystem.FileCopy(strAppPath & "\FISServiceAccount.json", CredentialFilePath & "\FISServiceAccount.json")
            End If

            jsonfile = CredentialFilePath & "\FISOAuth2.json"
            If Not FileIO.FileSystem.FileExists(jsonfile) Then 'copy from application folder
                My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
                FileSystem.FileCopy(strAppPath & "\FISOAuth2.json", CredentialFilePath & "\FISOAuth2.json")
            End If

            If FileIO.FileSystem.DirectoryExists(strAppUserPath & "\GoogleDriveAuthentication") Then
                System.IO.Directory.Delete(strAppUserPath & "\GoogleDriveAuthentication", True)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function GetMasterBackupFolderID(FISService As DriveService) As String
        Try
            Dim id As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'FIS Backup' and 'root' in parents"
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


    Private Sub CreateInternalFileTransferFolderAndCheckUnreadFiles(FISService As DriveService)

        Try
            blUnreadIFTFileAvailable = False

            Dim internalfolderid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Internal File Transfer' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                internalfolderid = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & FullDistrictName & "' and '" & internalfolderid & "' in parents"

            List.Fields = "files(id, viewedByMeTime)"

            Results = List.Execute

            cnt = Results.Files.Count

            If cnt = 0 Then
                Dim parentlist As New List(Of String)
                parentlist.Add(internalfolderid)

                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                NewDirectory.Name = FullDistrictName
                NewDirectory.Parents = parentlist
                NewDirectory.MimeType = "application/vnd.google-apps.folder"
                NewDirectory.Description = FileOwner
                Dim request As FilesResource.CreateRequest = FISService.Files.Create(NewDirectory)
                NewDirectory = request.Execute()
            Else
                UserIFTFolderID = Results.Files(0).Id
                If Results.Files(0).ViewedByMeTime Is Nothing Then
                    dtIFTFolderViewTime = Now
                    Dim tFile = New Google.Apis.Drive.v3.Data.File
                    tFile.ViewedByMeTime = dtIFTFolderViewTime
                    FISService.Files.Update(tFile, UserIFTFolderID).Execute()
                    Exit Sub
                Else
                    dtIFTFolderViewTime = Results.Files(0).ViewedByMeTime
                    Dim viewedtime As String = Results.Files(0).ViewedByMeTimeRaw

                    '  List.Q = "trashed = false and mimeType != 'application/vnd.google-apps.folder' and '" & UserIFTFolderID & "' in parents and modifiedTime > '" & viewedtime & "'"
                    List.Q = "trashed = false and '" & UserIFTFolderID & "' in parents and modifiedTime > '" & viewedtime & "' and name != 'Work Done Statement' and name != 'Monthly Statements Backup'"

                    List.Fields = "files(id, modifiedTime)"
                    Results = List.Execute
                    UnreadIFTFileCount = Results.Files.Count

                    If UnreadIFTFileCount > 0 Then
                        blUnreadIFTFileAvailable = True
                    End If
                End If
            End If

        Catch ex As Exception
            ' ShowErrorMessage(ex)

        End Try
    End Sub

#End Region


#Region "LOCAL AND ONLINE MANUAL BACKUP AND RESTORE DATABASE"

    Private Sub LocalDatabaseBackup() Handles btnLocalBackup.Click

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub


        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        blRestoreDB = False
        blDBIsInPreviewMode = blPreviewDB
        blPreviewDB = False

        FrmLocalBackup.ShowDialog()

        If blRestoreDB Then
            blPreviewDB = False
            blDBIsInPreviewMode = False
            LoadRecordsAfterRestore()
            If Me.btnOpen.Enabled = False Then
                EnableControls()
                Me.pnlRegisterName.Text = "Scene of Crime Register"
            End If
        End If

        If blPreviewDB Then
            blDBIsInPreviewMode = True
            LoadRecordsAfterPreview()
        Else
            blPreviewDB = blDBIsInPreviewMode
        End If

        If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
    End Sub

    Private Sub CleanupLocalBackupFiles()
        On Error Resume Next
        Dim BackupPath = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")
        Dim x = My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchTopLevelOnly, "FingerPrintBackup*.mdb")
        Dim cnt As Integer = x.Count
        If cnt > 10 Then
            Dim ItemList As New ArrayList
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchTopLevelOnly, "FingerPrintBackup*.mdb")
                Dim FileName = My.Computer.FileSystem.GetName(foundFile)
                Dim FullFilePath = My.Computer.FileSystem.GetParentPath(foundFile) & "\" & FileName
                ItemList.Add(FullFilePath)
            Next

            ItemList.Sort()
            Dim u = cnt - 11

            For i = 0 To u
                My.Computer.FileSystem.DeleteFile(ItemList.Item(i))
            Next
        End If

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.SpecialDirectories.MyDocuments, FileIO.SearchOption.SearchTopLevelOnly, "Fingerprint Information System*.md5")
            My.Computer.FileSystem.DeleteFile(foundFile)
        Next
    End Sub

    Private Sub ShowOnlineDatabaseBackup() Handles btnOnlineBackup.Click

        If FullDistrictName = "" Or FullDistrictName = "FullDistrict" Then
            MessageBoxEx.Show("Enter'Full and Short District Names' to view online files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            boolShowWizard = True
            FrmSettingsWizard.ShowDialog()

            If FullDistrictName = "" Or FullDistrictName = "FullDistrict" Then
                MessageBoxEx.Show("'Full District Name' is empty. Cannot load files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If Not CheckUserBackupFolderExists(FullDistrictName) Then
                Me.Cursor = Cursors.Default
                Dim r = MessageBoxEx.Show("District Name '" & FullDistrictName & "' not found in Online Backup. Do you want to change District Name and try again?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If r = Windows.Forms.DialogResult.No Then
                    Exit Sub
                Else
                    boolShowWizard = True
                    FrmSettingsWizard.ShowDialog()
                End If

            End If
            Me.Cursor = Cursors.Default
        End If

        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If

        blRestoreDB = False
        blDBIsInPreviewMode = blPreviewDB
        blPreviewDB = False

        FindLastSOCNumberDICount()
        frmOnlineBackup.ShowDialog()

        If blRestoreDB Then
            blPreviewDB = False
            blDBIsInPreviewMode = False
            LoadRecordsAfterRestore()
            If Me.btnOpen.Enabled = False Then
                EnableControls()
                Me.pnlRegisterName.Text = "Scene of Crime Register"
            End If
        End If

        If blPreviewDB Then
            blDBIsInPreviewMode = True
            LoadRecordsAfterPreview()
        Else
            blPreviewDB = blDBIsInPreviewMode
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Function CheckUserBackupFolderExists(UserFolder As String) As Boolean
        Try
            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'exit 
                Return False
            End If

            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim UserFolderID As String = ""


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim parentid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'FIS Backup' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Return False
            Else
                parentid = Results.Files(0).Id
            End If

            List.Q = "name contains '" & UserFolder & "' and trashed = false and '" & parentid & "' in parents"

            List.Fields = "files(id)"

            Results = List.Execute

            If Results.Files.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Sub bgwCleanOnlineFiles_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCleanOnlineFiles.DoWork
        Try
            If InternetAvailable() = False Then
                Exit Sub
            End If

            If Not FileIO.FileSystem.FileExists(JsonPath) Then 'exit 
                Exit Sub
            End If

            If FullDistrictName = "" Then
                Exit Sub
            End If

            If FullDistrictName = "FullDistrict" Then
                Exit Sub
            End If

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim UserBackupFolderName As String = FullDistrictName
            Dim UserBackupFolderID As String = ""

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim List = FISService.Files.List()
            Dim MasterBackupFolderID As String = GetMasterBackupFolderID(FISService)

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserBackupFolderName & "' and '" & MasterBackupFolderID & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                UserBackupFolderID = Results.Files(0).Id
            End If

            List.Q = "mimeType = 'database/mdb' and '" & UserBackupFolderID & "' in parents and name contains 'FingerprintBackup'"

            List.Fields = "files(id)"
            List.OrderBy = "createdTime asc"
            Results = List.Execute

            cnt = Results.Files.Count

            If cnt < 25 Then Exit Sub

            Dim ItemList As New ArrayList

            For Each Result In Results.Files
                ItemList.Add(Result.Id) 'id
            Next

            Dim u = cnt - 25
            Dim id As String = ""
            For i = 0 To u
                id = ItemList.Item(i)
                Dim DeleteRequest = FISService.Files.Delete(id)
                DeleteRequest.Execute()
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FindLastSOCNumberDICount()
        Try
            Dim FPDS As New FingerPrintDataSet
            Dim SOCTblAdptr As New FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
            SOCTblAdptr.Connection.ConnectionString = sConString
            SOCTblAdptr.Connection.Open()

            SOCTblAdptr.FillByLatestSOCRecord(FPDS.SOCRegister)
            If FPDS.SOCRegister.Count = 1 Then
                LatestSOCNumber = FPDS.SOCRegister(0).SOCNumber
                LatestSOCDI = FPDS.SOCRegister(0).DateOfInspection.ToString("dd-MM-yyyy")
            End If
            LocalSOCRecordCount = SOCTblAdptr.ScalarQueryTotalSOCCount
        Catch ex As Exception
            LatestSOCNumber = ""
            LatestSOCDI = ""
        End Try

    End Sub

    Private Sub LoadRecordsAfterRestore()
        Try

            Me.Cursor = Cursors.WaitCursor
            blUpdateLastModificationDate = False
            Me.cprDBAvailable.Visible = False

            strDatabaseFile = My.Computer.Registry.GetValue(strGeneralSettingsPath, "DatabaseFile", SuggestedLocation & "\Database\Fingerprint.mdb")

            sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strDatabaseFile

            frmProgressBar.Show()
            frmProgressBar.SetStatusText("Restoring Database...")
            blApplicationIsRestoring = True

            ConnectToDatabase()

            For i = 1 To 5
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next

            CreateSupportingStaffTable()
            CreateChalanRegisterTable()

            CreateOfficerTable()
            CreateSupportingStaffTable()
            For i = 6 To 10
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next

            CreateSOCReportRegisterTable()
            CreateLastModificationTable()
            CreateCommonSettingsTable()
            CreateIdentificationRegisterTable()
            CreateRevenueCollectionTable()

            ReCopyValuesToChalanTable()

            For i = 11 To 15
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next


            CreateSettingsTable()


            For i = 16 To 20
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next
            ModifyTables()
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "UpdateNullFields", "1", Microsoft.Win32.RegistryValueKind.String)
            For i = 21 To 25
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next

            UpdateNullFields()

            For i = 26 To 30
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next

            RemoveNullFromOfficerTable()
            For i = 31 To 35
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next

            LoadPSList()

            ' LoadModusOperandi()

            InitializeOfficerTable()
            LoadOfficerToMemory()
            LoadOfficerListToTable()

            For i = 36 To 40
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next


            OfficeSettingsEditMode(True)
            LoadOfficeSettingsToMemory()
            SetWindowTitle()
            LoadOfficeSettingsToTextBoxes()

            For i = 41 To 50
                frmProgressBar.SetProgressText(i)
                System.Threading.Thread.Sleep(10)
            Next

            LoadRecordsToAllTablesDependingOnCurrentYearSettings()

            If Me.btnOpen.Enabled = False Then
                EnableControls()
                Me.pnlRegisterName.Text = "Scene of Crime Register"
            End If

            OfficeSettingsEditMode(False)

            Me.btnSavePS.Enabled = True
            Me.btnSaveIO.Enabled = True
            Me.btnSaveSS.Enabled = True
            Me.btnSaveOfficeSettings.Enabled = True

            If blIdentificationRegisterUpdateFailed Then
                System.Threading.Thread.Sleep(3000)
                MessageBoxEx.Show("The application will now close to complete the restore and restart again.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                blRestartApplication = True
                My.Computer.Registry.SetValue(strGeneralSettingsPath, "CreateTable", "1", Microsoft.Win32.RegistryValueKind.String)
                EndApplication()
            End If
            blUpdateLastModificationDate = True
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadRecordsAfterPreview()
        Try

            Me.Cursor = Cursors.WaitCursor
            blUpdateLastModificationDate = False
            Me.cprDBAvailable.Visible = False
            blChangePleaseWaitFormText = True
            ShowPleaseWaitForm()
            blApplicationIsRestoring = False

            sConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strDatabaseFile
            ConnectToDatabase()

            LoadPSList()
            InitializeOfficerTable()
            LoadOfficerToMemory()
            LoadOfficerListToTable()

            OfficeSettingsEditMode(True)
            LoadOfficeSettingsToMemory()
            SetWindowTitle()
            LoadOfficeSettingsToTextBoxes()

            LoadRecordsToAllTablesDependingOnCurrentYearSettings()

            OfficeSettingsEditMode(False)
            If Not Me.pnlRegisterName.Text.EndsWith("Mode") Then Me.pnlRegisterName.Text += " - Preview Mode"
            DisableControlsInPreviewMode()

            ClosePleaseWaitForm()
            blChangePleaseWaitFormText = False
            MessageBoxEx.Show("Database preview generated. Restart the application to close Preview.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Function IsValidBackupFile(ByVal DBPath As String) As Boolean
        IsValidBackupFile = False

        Try
            DBPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBPath
            If DoesTableExist("SOCRegister", DBPath) Then
                IsValidBackupFile = True
            Else
                IsValidBackupFile = False
            End If
        Catch ex As Exception
            IsValidBackupFile = False
        End Try
    End Function

#End Region


#Region "CHECK FOR RECENT DATABASE IN ONLINE"
    Private Sub CheckForRecentRemoteDatabase()
        Try
            If Not InternetAvailable() Then
                Exit Sub
            End If

            If Not FileIO.FileSystem.FileExists(JsonPath) Then
                Exit Sub
            End If

            If FullDistrictName = "" Then
                Exit Sub
            End If

            If FullDistrictName = "FullDistrict" Then
                Exit Sub
            End If

            Dim LocalSOCCount As Integer = Me.SOCRegisterTableAdapter.ScalarQueryTotalSOCCount
            bgwCheckRemoteDB.RunWorkerAsync(LocalSOCCount)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgwCheckRemoteDB_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCheckRemoteDB.DoWork
        Try
            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim UserBackupFolderName As String = FullDistrictName
            Dim UserBackupFolderID As String = ""


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim List = FISService.Files.List()
            Dim MasterBackupFolderID As String = GetMasterBackupFolderID(FISService)

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '" & UserBackupFolderName & "' and '" & MasterBackupFolderID & "' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwCheckRemoteDB.ReportProgress(0, False)
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
            RemoteModificationDetails = ""

            cnt = Results.Files.Count
            If cnt > 0 Then
                description = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, "; ")
                RemoteSOCRecordCount = Val(SplitText(4))
                RemoteModifiedDate = SplitText(1)
                dtlastremotemodified = DateTime.ParseExact(RemoteModifiedDate, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

                Dim u = SplitText.GetUpperBound(0)

                If u = 5 Then
                    RemoteModificationDetails = SplitText(5)
                End If

                If RemoteModificationDetails = "NIL" Then
                    RemoteModificationDetails = ""
                End If
            Else
                bgwCheckRemoteDB.ReportProgress(0, False)
                Exit Sub
            End If


            Dim dtlastlocalmodified As Date = GetLastModificationDate()

            Dim LocalSOCRecordCount As Integer = e.Argument

            If RemoteSOCRecordCount > LocalSOCRecordCount Then
                bgwCheckRemoteDB.ReportProgress(0, True)
            End If

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
                    bgwCheckRemoteDB.ReportProgress(0, True)
                End If

                If ldt = rdt Then
                    If lhr < rhr Then
                        bgwCheckRemoteDB.ReportProgress(0, True)
                    End If
                    If lhr = rhr Then
                        If lmt < rmt Then
                            bgwCheckRemoteDB.ReportProgress(0, True)
                        End If
                        If lmt = rmt Then
                            If ls < rs Then
                                bgwCheckRemoteDB.ReportProgress(0, True)
                            End If
                            If ls = rs Then
                                bgwCheckRemoteDB.ReportProgress(0, False)
                            End If
                        End If
                    End If
                End If

                If ldt > rdt Then
                    bgwCheckRemoteDB.ReportProgress(0, False)
                End If

            End If

        Catch ex As Exception
            bgwCheckRemoteDB.ReportProgress(0, False)
        End Try
    End Sub

    Private Sub bgwCheckRemoteDB_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwCheckRemoteDB.ProgressChanged
        If TypeOf e.UserState Is Boolean Then
            Me.cprDBAvailable.Visible = e.UserState
            If e.UserState = True Then
                Thread.Sleep(5000)
                If blDBIsInPreviewMode Then
                    Exit Sub
                End If

                Dim message As String = ""

                If RemoteModificationDetails = "" Then
                    message = "A more recent database is available in online backup." & vbNewLine & vbNewLine & vbTab & "Last Modification Date: " & RemoteModifiedDate & vbNewLine & vbNewLine & "Press OK to view and restore this database."
                Else
                    message = "A recently modified database is available in online backup." & vbNewLine & vbNewLine & vbTab & "Last Modification Details: " & RemoteModificationDetails & vbNewLine & vbTab & "Last Modification Date: " & RemoteModifiedDate & vbNewLine & vbNewLine & "Press OK to view and restore this database."
                End If
                Dim r = MessageBoxEx.Show(message, strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

                If r = Windows.Forms.DialogResult.OK Then
                    ShowOnlineDatabaseBackup()
                End If
            End If
        Else
            Me.cprDBAvailable.Visible = False
        End If
    End Sub

#End Region


#Region "OPEN DATABASE"

    Private Sub OpenDBLocation() Handles btnOpenDBFolder.Click
        On Error Resume Next
        If blDBIsInPreviewMode Then
            MessageBoxEx.Show("Database is in Preview Mode. Cannot open Database Folder.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If FileIO.FileSystem.FileExists(strDatabaseFile) Then
            Call Shell("explorer.exe /select," & strDatabaseFile, AppWinStyle.NormalFocus)
        Else
            MessageBoxEx.Show("The database file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub


    Private Sub OpenDBInAccess() Handles btnOpenDBInMSAccess.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FileIO.FileSystem.FileExists(strDatabaseFile) Then
            Shell("explorer.exe " & strDatabaseFile, AppWinStyle.MaximizedFocus)
        Else
            MessageBoxEx.Show("The database file does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Cursor = Cursors.Default
    End Sub


#End Region


#Region "DOWNLOAD INSTALLER AND HOLIDAY LIST"

    Private Sub btnDownloadInstallerInBrowser_Click(sender As Object, e As EventArgs) Handles btnDownloadInstallerInBrowser.Click
        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("No internet connection detected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        ' CheckForUpdates()
        Me.Cursor = Cursors.Default
        ' Process.Start(InstallerFileURL) ' google drive File
        Process.Start("https://drive.google.com/file/d/1vyGdhxjXUWjkcgTE_rTT7juiMSBA-UKc/view")
    End Sub


    Private Sub btnDownloadInstaller_Click(sender As Object, e As EventArgs) Handles btnDownloadInstallerFile.Click
        Me.Cursor = Cursors.WaitCursor

        If Me.pgrDownloadInstaller.Visible And Me.pgrDownloadInstaller.Text.StartsWith("Uploading Backup") Then
            MessageBoxEx.Show("Another File Upload is in progress. Please try later.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.pgrDownloadInstaller.Visible And Me.pgrDownloadInstaller.Text.StartsWith("Downloading Installer") Then
            MessageBoxEx.Show("Another File Download is in progress. Please try later.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If InternetAvailable() = False Then
            MessageBoxEx.Show("No internet connection detected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim message As String = ""
        If InstallerFileSize = "" Then
            message = "Download Installer?"
        Else
            message = "Installer File size is " & InstallerFileSize & ". Download Installer?"
        End If
        Dim r = MessageBoxEx.Show(message, strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If r <> Windows.Forms.DialogResult.OK Then Exit Sub

        blShowUpdateDownloaded = True
        DownloadInstaller()
    End Sub

    Private Sub DownloadInstaller()

        If Me.rbrDownloadInstaller.Visible Then
            MessageBoxEx.Show("Another download is in progress. Please try later.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        pgrDownloadInstaller.Visible = True
        pgrDownloadInstaller.Value = 0
        pgrDownloadInstaller.Text = "Downloading Installer 0%"
        Me.StatusBar.RecalcLayout()

        cpgrDownloadInstaller.ProgressColor = GetProgressColor()
        cpgrDownloadInstaller.ProgressText = "0"
        rbrDownloadInstaller.Visible = True
        cpgrDownloadInstaller.IsRunning = True
        rbrDownloadInstaller.Text = "Downloading"

        bgwDownloadInstaller.RunWorkerAsync()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bgwDownloadInstaller_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownloadInstaller.DoWork
        Try

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})


            Dim parentid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Installer File' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwDownloadInstaller.ReportProgress(100, "Installer File folder not found")
                Exit Sub
            Else
                parentid = Results.Files(0).Id
            End If

            List.Q = "name contains 'Fingerprint Information System' and name contains '.exe' and trashed = false and '" & parentid & "' in parents"
            List.Fields = "files(name, id, webViewLink, description)"

            Results = List.Execute

            If Results.Files.Count > 0 Then
                InstallerFileName = Results.Files(0).Name
                Dim InstallerFilePath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & InstallerFileName
                InstallerFileID = Results.Files(0).Id

                Dim description As String = Results.Files(0).Description
                Dim SplitText() = Strings.Split(description, ", ")
                Dim u = SplitText.GetUpperBound(0)

                If u > 0 Then
                    Dim md5 As String = SplitText(1)
                    '  Dim md5filename As String = System.IO.Path.GetFileNameWithoutExtension(InstallerFilePath) & ".md5"
                    ' Dim md5file As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & md5filename, False)
                    ' md5file.WriteLine(md5)
                    ' md5file.Close()
                    My.Computer.Registry.SetValue(strGeneralSettingsPath, "MD5", md5, Microsoft.Win32.RegistryValueKind.String)
                End If

                Dim request = FISService.Files.Get(InstallerFileID)
                request.Fields = "size"
                Dim file = request.Execute

                dFileSize = file.Size
                dFormatedFileSize = CalculateFileSize(dFileSize)

                Dim fStream = New System.IO.FileStream(InstallerFilePath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)
                Dim mStream = New System.IO.MemoryStream

                Dim m = request.MediaDownloader
                m.ChunkSize = 256 * 1024

                AddHandler m.ProgressChanged, AddressOf Download_ProgressChanged

                request.DownloadWithStatus(mStream)

                If dDownloadStatus = DownloadStatus.Completed Then
                    mStream.WriteTo(fStream)
                End If

                fStream.Close()
                mStream.Close()
            Else
                bgwDownloadInstaller.ReportProgress(100, "File not found")

            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Download_ProgressChanged(Progress As IDownloadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        dBytesDownloaded = Progress.BytesDownloaded
        dDownloadStatus = Progress.Status
        Dim percent = CInt((dBytesDownloaded / dFileSize) * 100)
        bgwDownloadInstaller.ReportProgress(percent)
    End Sub

    Private Sub bgwDownloadInstaller_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDownloadInstaller.ProgressChanged
        cpgrDownloadInstaller.ProgressText = e.ProgressPercentage
        pgrDownloadInstaller.Value = e.ProgressPercentage
        pgrDownloadInstaller.Text = "Downloading Installer " & e.ProgressPercentage & "% " & CalculateFileSize(dBytesDownloaded) & "/" & dFormatedFileSize

        If e.UserState = "File not found" Then
            blShowUpdateDownloaded = False
            MessageBoxEx.Show("Installer File not found. Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        If e.UserState = "Installer File folder not found" Then
            blShowUpdateDownloaded = False
            MessageBoxEx.Show("Installer File folder not found. Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub bgwDownloadInstaller_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloadInstaller.RunWorkerCompleted

        Try
            rbrDownloadInstaller.Visible = False
            pgrDownloadInstaller.Visible = False

            If dDownloadStatus = DownloadStatus.Completed And blShowUpdateDownloaded Then
                MessageBoxEx.Show(InstallerFileName.Replace(".exe", "") & " Installer File downloaded successfully.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call Shell("explorer.exe /select," & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & InstallerFileName, AppWinStyle.NormalFocus)
            End If

            If dDownloadStatus = DownloadStatus.Failed And blShowUpdateDownloaded Then
                MessageBoxEx.Show("Installer File Download failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If dDownloadStatus = DownloadStatus.Failed Then
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & InstallerFileName)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DownloadHolidayList()
        Try

            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})


            Dim parentid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..HolidayList' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Exit Sub
            Else
                parentid = Results.Files(0).Id
            End If

            List.Q = "name = 'HolidayList.mdb' and trashed = false and '" & parentid & "' in parents"
            List.Fields = "files(name, id, modifiedTime)"

            Results = List.Execute

            If Results.Files.Count > 0 Then
                Dim hdFileName = Results.Files(0).Name
                Dim hdFileID = Results.Files(0).Id
                Dim modifiedtime As Date = Results.Files(0).ModifiedTime
                Dim hdDatabase As String = strAppUserPath & "\WordTemplates\HolidayList.mdb"
                Dim createdtime As Date
                If My.Computer.FileSystem.FileExists(hdDatabase) Then
                    createdtime = My.Computer.FileSystem.GetFileInfo(hdDatabase).LastWriteTime
                    If createdtime > modifiedtime Then
                        Exit Sub
                    End If
                End If

                Dim request = FISService.Files.Get(hdFileID)

                Dim tempfile As String = My.Computer.FileSystem.GetTempFileName & ".mdb"
                Dim fStream = New System.IO.FileStream(tempfile, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)

                Dim mStream = New System.IO.MemoryStream

                Dim m = request.MediaDownloader
                m.ChunkSize = 256 * 1024

                request.DownloadWithStatus(mStream)
                mStream.WriteTo(fStream)
                fStream.Close()
                mStream.Close()
                My.Computer.FileSystem.CopyFile(tempfile, hdDatabase, True)

            End If

        Catch ex As Exception
            ' ShowErrorMessage(ex)
        End Try
    End Sub
#End Region


#Region "CHECK FOR APP UPDATE AND NEW FILES"

    Private Sub ShowNewVersionInstalledInfo()
        Try

            Dim ShowNewVersionInfo As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "ShowNewVersionInfo", "0")

            If ShowNewVersionInfo = "1" And My.Computer.FileSystem.FileExists(strAppUserPath & "\NewVersionFeatures.rtf") Then
                Thread.Sleep(2000)
                My.Computer.Registry.SetValue(strGeneralSettingsPath, "ShowNewVersionInfo", "0", Microsoft.Win32.RegistryValueKind.String)
                blNewVersionFound = False
                frmNewVersionFeatures.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CheckForUpdatesAtStartup()
        Try
            bgwUpdateChecker.RunWorkerAsync()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CheckForUpdatesManually() Handles btnCheckUpdate.Click

        Me.Cursor = Cursors.WaitCursor
        ShowPleaseWaitForm()
        If InternetAvailable() = False Then
            ClosePleaseWaitForm()
            MessageBoxEx.Show("No internet connection detected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If CheckForUpdates() Then
            ClosePleaseWaitForm()
            Application.DoEvents()

            Dim d = MessageBoxEx.Show("A new version 'V" & RemoteInstallerVersion & "' is available. Do you want to download?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            If d = Windows.Forms.DialogResult.Yes Then
                ShowDesktopAlert("Download will continue in the background. You will be notified when finished.")
                blShowUpdateDownloaded = True
                DownloadInstaller()
            End If
        Else
            ClosePleaseWaitForm()
            blShowUpdateDownloaded = False
            MessageBoxEx.Show("You are using the latest version.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function CheckForUpdates() As Boolean

        If Not FileIO.FileSystem.FileExists(JsonPath) Then 'exit 
            Return False
        End If

        Try
            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}
            Dim VersionFolder As String = "Version"
            Dim VersionFolderID As String = ""


            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            If FullDistrictName <> "" And FullDistrictName <> "FullDistrict" Then
                CreateInternalFileTransferFolderAndCheckUnreadFiles(FISService) 'CHECK FOR NEW FILES
            End If


            DownloadHolidayList()

            Dim parentid As String = ""
            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'Installer File' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                Return False
            Else
                parentid = Results.Files(0).Id
            End If

            List.Q = "name contains 'Fingerprint Information System' and name contains '.exe' and trashed = false and '" & parentid & "' in parents"

            List.Fields = "files(name, id, webViewLink, size)"

            Results = List.Execute

            If Results.Files.Count > 0 Then
                RemoteInstallerVersion = Results.Files(0).Name
                InstallerFileID = Results.Files(0).Id
                InstallerFileURL = Results.Files(0).WebViewLink
                InstallerFileSize = CalculateFileSize(Results.Files(0).Size)
                RemoteInstallerVersion = RemoteInstallerVersion.Substring(RemoteInstallerVersion.Length - 8).Remove(4)
                Dim LocalVersion As String = My.Application.Info.Version.ToString.Substring(0, 4)

                If RemoteInstallerVersion > LocalVersion Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            '  ShowErrorMessage(ex)
            Return False
        End Try
    End Function


    Private Sub bgwUpdateChecker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpdateChecker.DoWork
        If InternetAvailable() = False Then
            Exit Sub
        End If
        Dim updateavailable As Boolean = CheckForUpdates()
        bgwUpdateChecker.ReportProgress(100, updateavailable)
    End Sub

    Private Sub bgwUpdateChecker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpdateChecker.ProgressChanged
        If e.ProgressPercentage = 100 And e.UserState = True Then 'auto update avaialable
            DownloadInstaller()
        End If

        If blUnreadIFTFileAvailable Then
            Me.cprUnreadFile.Visible = True
            Dim message As String = ""

            If UnreadIFTFileCount = 1 Then
                message = "You have 1 unread file in online 'Internal File Transfer' Folder. Press 'OK' to view the file."
            End If

            If UnreadIFTFileCount > 1 Then
                message = "You have " & UnreadIFTFileCount & " unread files in online 'Internal File Transfer' Folder. Press 'OK' to view the files."
            End If

            Dim r = MessageBoxEx.Show(message, strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            If r = Windows.Forms.DialogResult.OK Then
                ShowFISFileList()
            Else
                blUnreadIFTFileAvailable = False
            End If
        End If
    End Sub


#End Region


#Region "VERSION FILE"

    Private Sub UploadVersionInfoToDrive()
        If FullDistrictName = "" Then
            Exit Sub
        End If

        If FullDistrictName = "FullDistrict" Then
            Exit Sub
        End If

        bgwVersionUploader.RunWorkerAsync()
    End Sub

    Private Sub bgwVersionUploader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwVersionUploader.DoWork
        Try
            If InternetAvailable() = False Then
                Exit Sub
            End If


            Dim FISService As DriveService = New DriveService
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim FISAccountServiceCredential As GoogleCredential = GoogleCredential.FromFile(JsonPath).CreateScoped(Scopes)
            FISService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = FISAccountServiceCredential, .ApplicationName = strAppName})

            Dim List = FISService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '..VersionFolder' and 'root' in parents"
            List.Fields = "files(id)"

            Dim Results = List.Execute
            Dim VersionFolderID As String = ""
            Dim cnt = Results.Files.Count

            If cnt > 0 Then
                VersionFolderID = Results.Files(0).Id
            Else
                Exit Sub
            End If

            List.Q = "mimeType = 'files/.fisver' and name = '" & FullDistrictName & "' and trashed = false and '" & VersionFolderID & "' in parents"
            List.Fields = "files(id, name, description)"

            Results = List.Execute

            Dim VersionFileID As String = ""
            Dim LocalVersion As String = My.Application.Info.Version.ToString.Substring(0, 4)
            Dim VersionFile = New Google.Apis.Drive.v3.Data.File
            VersionFile.Description = LocalVersion

            cnt = Results.Files.Count

            If cnt > 0 Then 'update version
                VersionFileID = Results.Files(0).Id
                Dim RemoteVersion As String = Results.Files(0).Description
                If RemoteVersion <> LocalVersion Then
                    VersionFile = FISService.Files.Update(VersionFile, VersionFileID).Execute
                End If

            Else ' create version
                Dim parentlist As New List(Of String)
                parentlist.Add(VersionFolderID) 'parent forlder

                VersionFile.Parents = parentlist
                VersionFile.Name = FullDistrictName
                VersionFile.MimeType = "files/.fisver"
                VersionFile = FISService.Files.Create(VersionFile).Execute
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region


#Region "FIS ONLINE FILE LIST"
    Private Sub ShowFISFileList() Handles btnBasicOnlineFileTransfer.Click
        On Error Resume Next

        If FullDistrictName = "" Or FullDistrictName = "FullDistrict" Then
            MessageBoxEx.Show("'Full District Name' is empty. Cannot load files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If blDBIsInPreviewMode Then
            MessageBoxEx.Show("Database is in Preview Mode. Cannot open.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If

        FileOwner = ShortOfficeName & "_" & ShortDistrictName
        LocalAdmin = False
        SuperAdmin = False
        LocalUser = True
        frmFISBackupList.SetTitleAndSize()
        frmFISBackupList.btnUpdateFileContent.Visible = False
        Me.Cursor = Cursors.Default
        frmFISBackupList.Show()
    End Sub


    Private Sub btnPersonalOnlineFiles_Click(sender As Object, e As EventArgs) Handles btnPersonalOnlineFiles.Click
        On Error Resume Next
        If frmPersonalFileStorage.Visible Then
            frmPersonalFileStorage.Activate()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        If InternetAvailable() = False Then
            MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.Cursor = Cursors.Default
        frmPassword.ShowDialog()
        If blUserAuthenticated Then
            blUserAuthenticated = False
            frmPersonalFileStorage.Show()
        End If


    End Sub
#End Region


#Region "DATABASE LAST MODIFICATION DATE"

    Public Sub InsertOrUpdateLastModificationDate(ModificationDate As Date, ModificationDetails As String)
        Try
            If blUpdateLastModificationDate = False Then Exit Sub
            If blDBIsInPreviewMode Then Exit Sub

            Dim ID As Integer = 1
            Me.LastModificationTableAdapter.FillByID(FingerPrintDataSet.LastModification, ID)
            If FingerPrintDataSet.LastModification.Count = 0 Then
                Me.LastModificationTableAdapter.Insert(ID, ModificationDate, ModificationDetails.Trim)
            Else
                Me.LastModificationTableAdapter.UpdateQuery(ID, ModificationDate, ModificationDetails.Trim, ID)
            End If
            Me.cprDBAvailable.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetLastModificationDate() As Date
        Try
            Dim ID As Integer = 1
            Me.LastModificationTableAdapter.FillByID(FingerPrintDataSet.LastModification, ID)
            Dim lastdate As Date = Now
            If FingerPrintDataSet.LastModification.Count = 1 Then
                lastdate = FingerPrintDataSet.LastModification(0).LastModifiedDate
            End If
            Return lastdate
        Catch ex As Exception
            Return Now
        End Try

    End Function

    Public Function GetLastModificationDetails() As String
        Try
            Dim ID As Integer = 1
            Me.LastModificationTableAdapter.FillByID(FingerPrintDataSet.LastModification, ID)
            Dim modification As String = "NIL"
            If FingerPrintDataSet.LastModification.Count = 1 Then
                modification = FingerPrintDataSet.LastModification(0).LastModificationDetails
                If modification Is Nothing Then modification = "NIL"
            End If
            Return modification
        Catch ex As Exception
            Return "NIL"
        End Try

    End Function
#End Region


#Region "BACKUP RANGE STATEMENTS"

    Private Sub BackupStatements() Handles btnBackupStatements.Click
        Try
            If FullDistrictName = "" Or FullDistrictName = "FullDistrict" Then
                MessageBoxEx.Show("'Full District Name' is empty. Cannot backup files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If blDBIsInPreviewMode Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot backup files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not blApplicationIsLoading And Not blApplicationIsRestoring Then Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Me.Cursor = Cursors.Default
            frmBackupStatements.ShowDialog()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region

    '---------------------------------------------END APPLICATION-----------------------------------

#Region "END APPLICATION"

    Private Sub frmMainInterface_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        EndApplication()

    End Sub

    Private Sub EndApplication() Handles btnExit.Click ', MyBase.FormClosed


        If blApplicationIsLoading Or blApplicationIsRestoring Then Exit Sub

        On Error Resume Next

        If blRestartApplication Then
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "RestartApp", "1", Microsoft.Win32.RegistryValueKind.String)
        Else
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "RestartApp", "0", Microsoft.Win32.RegistryValueKind.String)
        End If

        SaveSOCDatagridColumnWidth()
        SaveRSOCDatagridColumnWidth()
        SaveDADatagridColumnWidth()
        SaveIDDatagridColumnWidth()
        SaveFPADatagridColumnWidth()
        SaveCDDatagridColumnWidth()
        SaveACDatagridColumnWidth()
        SavePSDatagridColumnWidth()
        SaveIDRDatagridColumnWidth()

        SaveSOCDatagridColumnOrder()
        SaveRSOCDatagridColumnOrder()
        SaveDADatagridColumnOrder()
        SaveIDDatagridColumnOrder()
        SaveFPADatagridColumnOrder()
        SaveCDDatagridColumnOrder()
        SaveACDatagridColumnOrder()
        SavePSDatagridColumnOrder()
        SaveIDRDatagridColumnOrder()

        If Not blDBIsInPreviewMode Then
            Dim AutoBackupTime As String = Me.txtAutoBackupPeriod.TextBox.Text.Trim
            My.Computer.Registry.SetValue(strBackupSettingsPath, "AutoBackupTime", AutoBackupTime, Microsoft.Win32.RegistryValueKind.String)

            Me.CommonSettingsTableAdapter1.FillBySettingsName(Me.FingerPrintDataSet.CommonSettings, "AutoBackupTime")
            Dim count As Integer = Me.FingerPrintDataSet.CommonSettings.Count
            If count = 1 Then
                Me.CommonSettingsTableAdapter1.UpdateQuery(AutoBackupTime, "", "AutoBackupTime")
            Else
                Me.CommonSettingsTableAdapter1.Insert("AutoBackupTime", AutoBackupTime, "")
            End If

            My.Computer.Registry.SetValue(strBackupSettingsPath, "FullDistrictName", FullDistrictName, Microsoft.Win32.RegistryValueKind.String)

            Dim LastModifiedDate As String = GetLastModificationDate().ToString("dd-MM-yyyy HH:mm:ss")

            My.Computer.Registry.SetValue(strBackupSettingsPath, "LastModifiedDate", LastModifiedDate, Microsoft.Win32.RegistryValueKind.String)

            Dim LastModificationDetail As String = GetLastModificationDetails()

            My.Computer.Registry.SetValue(strBackupSettingsPath, "LastModificationDetail", LastModificationDetail, Microsoft.Win32.RegistryValueKind.String)

            FindLastSOCNumberDICount()

            My.Computer.Registry.SetValue(strBackupSettingsPath, "LocalSOCRecordCount", LocalSOCRecordCount, Microsoft.Win32.RegistryValueKind.String)

            Dim FileOwner As String = ShortOfficeName & "_" & ShortDistrictName
            My.Computer.Registry.SetValue(strBackupSettingsPath, "FileOwner", FileOwner, Microsoft.Win32.RegistryValueKind.String)

            Dim BackupDescription As String = FileOwner & "_AutoBackup" & "; " & LastModifiedDate & "; " & LatestSOCNumber & "; " & LatestSOCDI & "; " & LocalSOCRecordCount & "; " & LastModificationDetail

            My.Computer.Registry.SetValue(strBackupSettingsPath, "BackupDescription", BackupDescription, Microsoft.Win32.RegistryValueKind.String)
        End If


        SaveQuicktoolbarSettings()
        objMutex.Close()
        objMutex = Nothing

        If Not blDBIsInPreviewMode And Not blRestartApplication Then
            Process.Start(strAppPath & "\BackupDatabase.exe")
        End If


        Me.Dispose()
        End
    End Sub
#End Region


#Region "COMPACT DATABASE"

    Private Sub btnCompactDatabase_Click(sender As Object, e As EventArgs) Handles btnCompactDatabase.Click
        Try
            If blDBIsInPreviewMode Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot compact.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim olddbsize As String = CalculateFileSize(My.Computer.FileSystem.GetFileInfo(strDatabaseFile).Length)
            Dim r = MessageBoxEx.Show("Current Database file size is " & olddbsize & ". Do you want to compact the Database?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If r = Windows.Forms.DialogResult.No Then Exit Sub

            Me.Cursor = Cursors.WaitCursor
            Dim db As New Microsoft.Office.Interop.Access.Dao.DBEngine

            Dim BackupPath As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BackupPath", SuggestedLocation & "\Backups")

            Dim BackupTime As Date = Now
            Dim d As String = Strings.Format(BackupTime, BackupDateFormatString)
            Dim sBackupTime = Strings.Format(BackupTime, "dd-MM-yyyy HH:mm:ss")
            Dim BackupFileName As String = "FingerPrintBackup-" & d & ".mdb"
            Dim BackupFile As String = BackupPath & "\" & BackupFileName
            My.Computer.FileSystem.CopyFile(strDatabaseFile, BackupFile, True)

            If My.Computer.FileSystem.FileExists(BackupFile) Then
                Dim tmpfile As String = My.Computer.FileSystem.GetTempFileName & ".mdb"
                db.CompactDatabase(BackupFile, tmpfile)
                If My.Computer.FileSystem.FileExists(tmpfile) Then
                    My.Computer.FileSystem.CopyFile(tmpfile, strDatabaseFile, True)
                    Dim newdbsize As String = CalculateFileSize(My.Computer.FileSystem.GetFileInfo(strDatabaseFile).Length)
                    MessageBoxEx.Show("Database file compressed successfully." & vbNewLine & "File size before compression - " & olddbsize & vbNewLine & "File size after compression - " & newdbsize & vbNewLine & "A backup file has been created before compression." & vbNewLine & vbNewLine & "The application will now restart automatically.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    blRestartApplication = True
                    EndApplication()
                Else
                    MessageBoxEx.Show("Database compression failed.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "MOVE STATEMENT FOLDERS TO NEW LOCATION"

    Private Sub bgwCopyFolder_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCopyFolder.DoWork
        Try
            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Annual Statistics", SuggestedLocation & "\Annual Statistics")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Attendance Statement", SuggestedLocation & "\Attendance Statement")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Expert Opinion", SuggestedLocation & "\Expert Opinion")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Grave Crime Statement", SuggestedLocation & "\Grave Crime Statement")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Identification Reports", SuggestedLocation & "\Identification Reports")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Identification Statement", SuggestedLocation & "\Identification Statement")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\Performance Statement", SuggestedLocation & "\Performance Statement")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\SOC Statement", SuggestedLocation & "\SOC Statement")
            Thread.Sleep(2000)

            CopyFolder(FileIO.SpecialDirectories.MyDocuments & "\TA Bills", SuggestedLocation & "\TA Bills")
            Thread.Sleep(2000)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CopyFolder(SourceFolder As String, DestinationFolder As String)
        Try
            If My.Computer.FileSystem.DirectoryExists(SourceFolder) Then
                My.Computer.FileSystem.CreateDirectory(DestinationFolder)
                My.Computer.FileSystem.CopyDirectory(SourceFolder, DestinationFolder, False)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub btnWeeklyDiaryDB_Click(sender As Object, e As EventArgs) Handles btnWeeklyDiaryDB.Click
        Try
            blPENVerified = False
            Me.Cursor = Cursors.WaitCursor
            frmWeeklyDiaryAuthentication.ShowDialog()
            If blPENVerified Then
                Me.Cursor = Cursors.WaitCursor
                frmWeeklyDiaryDE.Show()
                frmWeeklyDiaryDE.BringToFront()
            Else
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub btnOpenErrorLog_Click(sender As Object, e As EventArgs) Handles btnOpenErrorLog.Click
        Try
            Dim errfile As String = strAppUserPath & "\ErrorLog.txt"
            If My.Computer.FileSystem.FileExists(errfile) Then
                Me.Cursor = Cursors.WaitCursor
                Shell("explorer.exe " & errfile, AppWinStyle.NormalFocus)
            Else
                MessageBoxEx.Show("Error Log File not found")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub btnCopyDBtoUSB_Click(sender As Object, e As EventArgs) Handles btnCopyDBtoUSB.Click
        Try
            If blDBIsInPreviewMode Then
                MessageBoxEx.Show("Database is in Preview Mode. Cannot copy.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim driveletter As String = ""
            For Each d As System.IO.DriveInfo In My.Computer.FileSystem.Drives
                If d.DriveType = IO.DriveType.Removable Then
                    driveletter = d.Name
                    Exit For
                End If
            Next

            If driveletter <> "" Then
                Dim destfile As String = driveletter & "\Fingerprint.mdb"
                Dim blshowcopied As Boolean = False

                If Not My.Computer.FileSystem.FileExists(destfile) Then
                    blshowcopied = True
                End If

                If My.Computer.FileSystem.DirectoryExists(driveletter) Then
                    My.Computer.FileSystem.CopyFile(strDatabaseFile, driveletter & "\Fingerprint.mdb", FileIO.UIOption.AllDialogs, FileIO.UICancelOption.DoNothing)

                    If My.Computer.FileSystem.FileExists(destfile) And blshowcopied Then
                        ShowDesktopAlert("Database copied to External drive.")
                    End If
                Else
                    MessageBoxEx.Show("Error copying file.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBoxEx.Show("No USB drive found.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


End Class
