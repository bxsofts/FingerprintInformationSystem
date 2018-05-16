<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingsWizard
    Inherits DevComponents.DotNetBar.Office2007Form


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSettingsWizard))
        Me.SettingsWizard = New DevComponents.DotNetBar.Wizard()
        Me.StartPage = New DevComponents.DotNetBar.WizardPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.wzrdPageOfficeUnit = New DevComponents.DotNetBar.WizardPage()
        Me.txtFullDistrict = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtShortDistrict = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtShortOffice = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtFullOffice = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.wzrdPageOfficers = New DevComponents.DotNetBar.WizardPage()
        Me.txtPhotographer = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.txtFPS = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.txtFPE2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtFPE3 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.txtFPE1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtTI = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.wzrdPagePeriodical = New DevComponents.DotNetBar.WizardPage()
        Me.txtWeeklyDiary = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX21 = New DevComponents.DotNetBar.LabelX()
        Me.txtVigilanceCase = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.txtGraveCrime = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.txtFPAttestation = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX18 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.txtTABill = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtRBWarrant = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtSOCDAStatement = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtIndividualPerformance = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtAttendance = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.wzrdPageDatabaseLocation = New DevComponents.DotNetBar.WizardPage()
        Me.lblDatabaseLocation = New System.Windows.Forms.LinkLabel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.wzrdPageBackup = New DevComponents.DotNetBar.WizardPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBackupInterval = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.chkAutoBackup = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblBackupLocation = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.wzrdPageFPSlipLocation = New DevComponents.DotNetBar.WizardPage()
        Me.lblFPLocation = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.wzrdPageChancePrinLocation = New DevComponents.DotNetBar.WizardPage()
        Me.lblCPLocation = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.wzrdPageFinish = New DevComponents.DotNetBar.WizardPage()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.OfficerTableTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.OfficerTableTableAdapter()
        Me.SettingsTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SettingsTableAdapter()
        Me.StyleManager1 = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.SettingsWizard.SuspendLayout()
        Me.StartPage.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.wzrdPageOfficeUnit.SuspendLayout()
        Me.wzrdPageOfficers.SuspendLayout()
        Me.wzrdPagePeriodical.SuspendLayout()
        Me.wzrdPageDatabaseLocation.SuspendLayout()
        Me.wzrdPageBackup.SuspendLayout()
        Me.wzrdPageFPSlipLocation.SuspendLayout()
        Me.wzrdPageChancePrinLocation.SuspendLayout()
        Me.wzrdPageFinish.SuspendLayout()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SettingsWizard
        '
        Me.SettingsWizard.BackButtonWidth = 86
        Me.SettingsWizard.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.SettingsWizard.BackgroundImage = CType(resources.GetObject("SettingsWizard.BackgroundImage"), System.Drawing.Image)
        Me.SettingsWizard.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007
        Me.SettingsWizard.CancelButtonText = "Cancel"
        Me.SettingsWizard.CancelButtonWidth = 86
        Me.SettingsWizard.Cursor = System.Windows.Forms.Cursors.Default
        Me.SettingsWizard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SettingsWizard.FinishButtonTabIndex = 3
        Me.SettingsWizard.FinishButtonWidth = 86
        Me.SettingsWizard.FooterHeight = 53
        '
        '
        '
        Me.SettingsWizard.FooterStyle.BackColor = System.Drawing.Color.Transparent
        Me.SettingsWizard.FooterStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.SettingsWizard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.SettingsWizard.HeaderCaptionFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SettingsWizard.HeaderDescriptionFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsWizard.HeaderDescriptionIndent = 78
        Me.SettingsWizard.HeaderDescriptionVisible = False
        Me.SettingsWizard.HeaderHeight = 90
        Me.SettingsWizard.HeaderImage = CType(resources.GetObject("SettingsWizard.HeaderImage"), System.Drawing.Image)
        Me.SettingsWizard.HeaderImageAlignment = DevComponents.DotNetBar.eWizardTitleImageAlignment.Left
        Me.SettingsWizard.HeaderImageSize = New System.Drawing.Size(64, 64)
        '
        '
        '
        Me.SettingsWizard.HeaderStyle.BackColor = System.Drawing.Color.Transparent
        Me.SettingsWizard.HeaderStyle.BackColorGradientAngle = 90
        Me.SettingsWizard.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.SettingsWizard.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.SettingsWizard.HeaderStyle.BorderBottomWidth = 1
        Me.SettingsWizard.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control
        Me.SettingsWizard.HeaderStyle.BorderLeftWidth = 1
        Me.SettingsWizard.HeaderStyle.BorderRightWidth = 1
        Me.SettingsWizard.HeaderStyle.BorderTopWidth = 1
        Me.SettingsWizard.HeaderStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.SettingsWizard.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.SettingsWizard.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.SettingsWizard.HeaderTitleIndent = 78
        Me.SettingsWizard.HelpButtonVisible = False
        Me.SettingsWizard.HelpButtonWidth = 86
        Me.SettingsWizard.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F"
        Me.SettingsWizard.Location = New System.Drawing.Point(0, 0)
        Me.SettingsWizard.Name = "SettingsWizard"
        Me.SettingsWizard.NextButtonWidth = 86
        Me.SettingsWizard.Size = New System.Drawing.Size(645, 401)
        Me.SettingsWizard.TabIndex = 0
        Me.SettingsWizard.WizardPages.AddRange(New DevComponents.DotNetBar.WizardPage() {Me.StartPage, Me.wzrdPageOfficeUnit, Me.wzrdPageOfficers, Me.wzrdPagePeriodical, Me.wzrdPageDatabaseLocation, Me.wzrdPageBackup, Me.wzrdPageFPSlipLocation, Me.wzrdPageChancePrinLocation, Me.wzrdPageFinish})
        '
        'StartPage
        '
        Me.StartPage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StartPage.BackColor = System.Drawing.Color.Transparent
        Me.StartPage.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.StartPage.Controls.Add(Me.PictureBox1)
        Me.StartPage.Controls.Add(Me.Label1)
        Me.StartPage.Controls.Add(Me.Label2)
        Me.StartPage.Controls.Add(Me.Label3)
        Me.StartPage.InteriorPage = False
        Me.StartPage.Location = New System.Drawing.Point(0, 0)
        Me.StartPage.Name = "StartPage"
        Me.StartPage.PageDescription = "Welcome"
        Me.StartPage.PageTitle = "Welcome"
        Me.StartPage.Size = New System.Drawing.Size(645, 348)
        '
        '
        '
        Me.StartPage.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.StartPage.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.StartPage.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.StartPage.TabIndex = 7
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(34, 180)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(177, 165)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.Label1.Location = New System.Drawing.Point(29, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(600, 35)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Welcome to the Fingerprint Information System Wizard"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(530, 85)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(333, 318)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(196, 27)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "To continue, click Next."
        '
        'wzrdPageOfficeUnit
        '
        Me.wzrdPageOfficeUnit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageOfficeUnit.AntiAlias = False
        Me.wzrdPageOfficeUnit.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageOfficeUnit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.wzrdPageOfficeUnit.Controls.Add(Me.txtFullDistrict)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.txtShortDistrict)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.LabelX2)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.LabelX5)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.txtShortOffice)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.txtFullOffice)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.LabelX6)
        Me.wzrdPageOfficeUnit.Controls.Add(Me.LabelX7)
        Me.wzrdPageOfficeUnit.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPageOfficeUnit.Name = "wzrdPageOfficeUnit"
        Me.wzrdPageOfficeUnit.PageDescription = "Office Name and District Settings"
        Me.wzrdPageOfficeUnit.PageTitle = "Office Name and District Settings"
        Me.wzrdPageOfficeUnit.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPageOfficeUnit.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageOfficeUnit.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageOfficeUnit.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageOfficeUnit.TabIndex = 8
        '
        'txtFullDistrict
        '
        Me.txtFullDistrict.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFullDistrict.Border.Class = "TextBoxBorder"
        Me.txtFullDistrict.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFullDistrict.DisabledBackColor = System.Drawing.Color.White
        Me.txtFullDistrict.FocusHighlightEnabled = True
        Me.txtFullDistrict.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullDistrict.ForeColor = System.Drawing.Color.Black
        Me.txtFullDistrict.Location = New System.Drawing.Point(204, 129)
        Me.txtFullDistrict.MaxLength = 255
        Me.txtFullDistrict.Name = "txtFullDistrict"
        Me.txtFullDistrict.Size = New System.Drawing.Size(309, 29)
        Me.txtFullDistrict.TabIndex = 10
        Me.txtFullDistrict.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFullDistrict.WatermarkText = "Full District Name"
        '
        'txtShortDistrict
        '
        Me.txtShortDistrict.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtShortDistrict.Border.Class = "TextBoxBorder"
        Me.txtShortDistrict.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtShortDistrict.DisabledBackColor = System.Drawing.Color.White
        Me.txtShortDistrict.FocusHighlightEnabled = True
        Me.txtShortDistrict.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortDistrict.ForeColor = System.Drawing.Color.Black
        Me.txtShortDistrict.Location = New System.Drawing.Point(204, 170)
        Me.txtShortDistrict.MaxLength = 255
        Me.txtShortDistrict.Name = "txtShortDistrict"
        Me.txtShortDistrict.Size = New System.Drawing.Size(309, 29)
        Me.txtShortDistrict.TabIndex = 11
        Me.txtShortDistrict.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtShortDistrict.WatermarkText = "Short District Name"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(83, 175)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(114, 18)
        Me.LabelX2.TabIndex = 13
        Me.LabelX2.Text = "Short District Name"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(83, 134)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(104, 18)
        Me.LabelX5.TabIndex = 12
        Me.LabelX5.Text = "Full District Name"
        '
        'txtShortOffice
        '
        Me.txtShortOffice.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtShortOffice.Border.Class = "TextBoxBorder"
        Me.txtShortOffice.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtShortOffice.DisabledBackColor = System.Drawing.Color.White
        Me.txtShortOffice.FocusHighlightEnabled = True
        Me.txtShortOffice.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortOffice.ForeColor = System.Drawing.Color.Black
        Me.txtShortOffice.Location = New System.Drawing.Point(204, 88)
        Me.txtShortOffice.MaxLength = 255
        Me.txtShortOffice.Name = "txtShortOffice"
        Me.txtShortOffice.Size = New System.Drawing.Size(309, 29)
        Me.txtShortOffice.TabIndex = 9
        Me.txtShortOffice.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtShortOffice.WatermarkText = "Short Office Name"
        '
        'txtFullOffice
        '
        Me.txtFullOffice.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFullOffice.Border.Class = "TextBoxBorder"
        Me.txtFullOffice.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFullOffice.DisabledBackColor = System.Drawing.Color.White
        Me.txtFullOffice.FocusHighlightEnabled = True
        Me.txtFullOffice.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullOffice.ForeColor = System.Drawing.Color.Black
        Me.txtFullOffice.Location = New System.Drawing.Point(204, 47)
        Me.txtFullOffice.MaxLength = 255
        Me.txtFullOffice.Name = "txtFullOffice"
        Me.txtFullOffice.Size = New System.Drawing.Size(309, 29)
        Me.txtFullOffice.TabIndex = 7
        Me.txtFullOffice.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFullOffice.WatermarkText = "Full Office Name"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(83, 93)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(107, 18)
        Me.LabelX6.TabIndex = 8
        Me.LabelX6.Text = "Short Office Name"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(83, 50)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(97, 18)
        Me.LabelX7.TabIndex = 6
        Me.LabelX7.Text = "Full Office Name"
        '
        'wzrdPageOfficers
        '
        Me.wzrdPageOfficers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageOfficers.AntiAlias = False
        Me.wzrdPageOfficers.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageOfficers.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.wzrdPageOfficers.Controls.Add(Me.txtPhotographer)
        Me.wzrdPageOfficers.Controls.Add(Me.LabelX17)
        Me.wzrdPageOfficers.Controls.Add(Me.txtFPS)
        Me.wzrdPageOfficers.Controls.Add(Me.LabelX16)
        Me.wzrdPageOfficers.Controls.Add(Me.txtFPE2)
        Me.wzrdPageOfficers.Controls.Add(Me.txtFPE3)
        Me.wzrdPageOfficers.Controls.Add(Me.LabelX12)
        Me.wzrdPageOfficers.Controls.Add(Me.LabelX13)
        Me.wzrdPageOfficers.Controls.Add(Me.txtFPE1)
        Me.wzrdPageOfficers.Controls.Add(Me.txtTI)
        Me.wzrdPageOfficers.Controls.Add(Me.LabelX14)
        Me.wzrdPageOfficers.Controls.Add(Me.LabelX15)
        Me.wzrdPageOfficers.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPageOfficers.Name = "wzrdPageOfficers"
        Me.wzrdPageOfficers.PageDescription = "Officers List"
        Me.wzrdPageOfficers.PageTitle = "Officers List"
        Me.wzrdPageOfficers.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPageOfficers.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageOfficers.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageOfficers.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageOfficers.TabIndex = 14
        '
        'txtPhotographer
        '
        Me.txtPhotographer.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPhotographer.Border.Class = "TextBoxBorder"
        Me.txtPhotographer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPhotographer.DisabledBackColor = System.Drawing.Color.White
        Me.txtPhotographer.FocusHighlightEnabled = True
        Me.txtPhotographer.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhotographer.ForeColor = System.Drawing.Color.Black
        Me.txtPhotographer.Location = New System.Drawing.Point(204, 189)
        Me.txtPhotographer.Name = "txtPhotographer"
        Me.txtPhotographer.Size = New System.Drawing.Size(309, 29)
        Me.txtPhotographer.TabIndex = 24
        Me.txtPhotographer.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtPhotographer.WatermarkText = "Photographer"
        '
        'LabelX17
        '
        Me.LabelX17.AutoSize = True
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Location = New System.Drawing.Point(83, 194)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.Size = New System.Drawing.Size(81, 18)
        Me.LabelX17.TabIndex = 25
        Me.LabelX17.Text = "Photographer"
        '
        'txtFPS
        '
        Me.txtFPS.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPS.Border.Class = "TextBoxBorder"
        Me.txtFPS.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPS.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPS.FocusHighlightEnabled = True
        Me.txtFPS.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPS.ForeColor = System.Drawing.Color.Black
        Me.txtFPS.Location = New System.Drawing.Point(204, 152)
        Me.txtFPS.Name = "txtFPS"
        Me.txtFPS.Size = New System.Drawing.Size(309, 29)
        Me.txtFPS.TabIndex = 22
        Me.txtFPS.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFPS.WatermarkText = "Fingerprint Searcher"
        '
        'LabelX16
        '
        Me.LabelX16.AutoSize = True
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Location = New System.Drawing.Point(83, 157)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.Size = New System.Drawing.Size(71, 18)
        Me.LabelX16.TabIndex = 23
        Me.LabelX16.Text = "F.P Searcher"
        '
        'txtFPE2
        '
        Me.txtFPE2.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPE2.Border.Class = "TextBoxBorder"
        Me.txtFPE2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPE2.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPE2.FocusHighlightEnabled = True
        Me.txtFPE2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPE2.ForeColor = System.Drawing.Color.Black
        Me.txtFPE2.Location = New System.Drawing.Point(204, 77)
        Me.txtFPE2.Name = "txtFPE2"
        Me.txtFPE2.Size = New System.Drawing.Size(309, 29)
        Me.txtFPE2.TabIndex = 18
        Me.txtFPE2.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFPE2.WatermarkText = "Fingerprint Expert II"
        '
        'txtFPE3
        '
        Me.txtFPE3.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPE3.Border.Class = "TextBoxBorder"
        Me.txtFPE3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPE3.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPE3.FocusHighlightEnabled = True
        Me.txtFPE3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPE3.ForeColor = System.Drawing.Color.Black
        Me.txtFPE3.Location = New System.Drawing.Point(204, 114)
        Me.txtFPE3.Name = "txtFPE3"
        Me.txtFPE3.Size = New System.Drawing.Size(309, 29)
        Me.txtFPE3.TabIndex = 19
        Me.txtFPE3.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFPE3.WatermarkText = "Fingerprint Expert III"
        '
        'LabelX12
        '
        Me.LabelX12.AutoSize = True
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Location = New System.Drawing.Point(83, 119)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.Size = New System.Drawing.Size(69, 18)
        Me.LabelX12.TabIndex = 21
        Me.LabelX12.Text = "F.P Expert 3"
        '
        'LabelX13
        '
        Me.LabelX13.AutoSize = True
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Location = New System.Drawing.Point(83, 82)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(69, 18)
        Me.LabelX13.TabIndex = 20
        Me.LabelX13.Text = "F.P Expert 2"
        '
        'txtFPE1
        '
        Me.txtFPE1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPE1.Border.Class = "TextBoxBorder"
        Me.txtFPE1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPE1.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPE1.FocusHighlightEnabled = True
        Me.txtFPE1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPE1.ForeColor = System.Drawing.Color.Black
        Me.txtFPE1.Location = New System.Drawing.Point(204, 40)
        Me.txtFPE1.Name = "txtFPE1"
        Me.txtFPE1.Size = New System.Drawing.Size(309, 29)
        Me.txtFPE1.TabIndex = 17
        Me.txtFPE1.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFPE1.WatermarkText = "Fingerprint Expert I"
        '
        'txtTI
        '
        Me.txtTI.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtTI.Border.Class = "TextBoxBorder"
        Me.txtTI.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtTI.DisabledBackColor = System.Drawing.Color.White
        Me.txtTI.FocusHighlightEnabled = True
        Me.txtTI.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTI.ForeColor = System.Drawing.Color.Black
        Me.txtTI.Location = New System.Drawing.Point(204, 4)
        Me.txtTI.Name = "txtTI"
        Me.txtTI.Size = New System.Drawing.Size(309, 29)
        Me.txtTI.TabIndex = 15
        Me.txtTI.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtTI.WatermarkText = "Tester Inspector"
        '
        'LabelX14
        '
        Me.LabelX14.AutoSize = True
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Location = New System.Drawing.Point(83, 45)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.Size = New System.Drawing.Size(69, 18)
        Me.LabelX14.TabIndex = 16
        Me.LabelX14.Text = "F.P Expert 1"
        '
        'LabelX15
        '
        Me.LabelX15.AutoSize = True
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Location = New System.Drawing.Point(83, 7)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.Size = New System.Drawing.Size(93, 18)
        Me.LabelX15.TabIndex = 14
        Me.LabelX15.Text = "Tester Inspector"
        '
        'wzrdPagePeriodical
        '
        Me.wzrdPagePeriodical.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPagePeriodical.AntiAlias = False
        Me.wzrdPagePeriodical.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPagePeriodical.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.wzrdPagePeriodical.Controls.Add(Me.txtWeeklyDiary)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX21)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtVigilanceCase)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX20)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtGraveCrime)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX19)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtFPAttestation)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX18)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX11)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtTABill)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtRBWarrant)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtSOCDAStatement)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtIndividualPerformance)
        Me.wzrdPagePeriodical.Controls.Add(Me.txtAttendance)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX10)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX3)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX4)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX8)
        Me.wzrdPagePeriodical.Controls.Add(Me.LabelX9)
        Me.wzrdPagePeriodical.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPagePeriodical.Name = "wzrdPagePeriodical"
        Me.wzrdPagePeriodical.PageDescription = "Periodical number for covering letters"
        Me.wzrdPagePeriodical.PageTitle = "Periodical number for covering letters"
        Me.wzrdPagePeriodical.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPagePeriodical.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPagePeriodical.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPagePeriodical.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPagePeriodical.TabIndex = 13
        '
        'txtWeeklyDiary
        '
        Me.txtWeeklyDiary.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtWeeklyDiary.Border.Class = "TextBoxBorder"
        Me.txtWeeklyDiary.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtWeeklyDiary.DisabledBackColor = System.Drawing.Color.White
        Me.txtWeeklyDiary.FocusHighlightEnabled = True
        Me.txtWeeklyDiary.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeeklyDiary.ForeColor = System.Drawing.Color.Black
        Me.txtWeeklyDiary.Location = New System.Drawing.Point(466, 148)
        Me.txtWeeklyDiary.MaxLength = 2
        Me.txtWeeklyDiary.Name = "txtWeeklyDiary"
        Me.txtWeeklyDiary.Size = New System.Drawing.Size(90, 29)
        Me.txtWeeklyDiary.TabIndex = 9
        '
        'LabelX21
        '
        Me.LabelX21.AutoSize = True
        '
        '
        '
        Me.LabelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX21.Location = New System.Drawing.Point(327, 152)
        Me.LabelX21.Name = "LabelX21"
        Me.LabelX21.Size = New System.Drawing.Size(77, 18)
        Me.LabelX21.TabIndex = 30
        Me.LabelX21.Text = "Weekly Diary"
        '
        'txtVigilanceCase
        '
        Me.txtVigilanceCase.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtVigilanceCase.Border.Class = "TextBoxBorder"
        Me.txtVigilanceCase.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtVigilanceCase.DisabledBackColor = System.Drawing.Color.White
        Me.txtVigilanceCase.FocusHighlightEnabled = True
        Me.txtVigilanceCase.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVigilanceCase.ForeColor = System.Drawing.Color.Black
        Me.txtVigilanceCase.Location = New System.Drawing.Point(466, 113)
        Me.txtVigilanceCase.MaxLength = 2
        Me.txtVigilanceCase.Name = "txtVigilanceCase"
        Me.txtVigilanceCase.Size = New System.Drawing.Size(90, 29)
        Me.txtVigilanceCase.TabIndex = 8
        '
        'LabelX20
        '
        Me.LabelX20.AutoSize = True
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Location = New System.Drawing.Point(327, 117)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.Size = New System.Drawing.Size(83, 18)
        Me.LabelX20.TabIndex = 28
        Me.LabelX20.Text = "Vigilance Case"
        '
        'txtGraveCrime
        '
        Me.txtGraveCrime.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtGraveCrime.Border.Class = "TextBoxBorder"
        Me.txtGraveCrime.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtGraveCrime.DisabledBackColor = System.Drawing.Color.White
        Me.txtGraveCrime.FocusHighlightEnabled = True
        Me.txtGraveCrime.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGraveCrime.ForeColor = System.Drawing.Color.Black
        Me.txtGraveCrime.Location = New System.Drawing.Point(210, 78)
        Me.txtGraveCrime.MaxLength = 2
        Me.txtGraveCrime.Name = "txtGraveCrime"
        Me.txtGraveCrime.Size = New System.Drawing.Size(90, 29)
        Me.txtGraveCrime.TabIndex = 3
        '
        'LabelX19
        '
        Me.LabelX19.AutoSize = True
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Location = New System.Drawing.Point(73, 82)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.Size = New System.Drawing.Size(72, 18)
        Me.LabelX19.TabIndex = 26
        Me.LabelX19.Text = "Grave Crime"
        '
        'txtFPAttestation
        '
        Me.txtFPAttestation.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPAttestation.Border.Class = "TextBoxBorder"
        Me.txtFPAttestation.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPAttestation.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPAttestation.FocusHighlightEnabled = True
        Me.txtFPAttestation.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPAttestation.ForeColor = System.Drawing.Color.Black
        Me.txtFPAttestation.Location = New System.Drawing.Point(210, 43)
        Me.txtFPAttestation.MaxLength = 2
        Me.txtFPAttestation.Name = "txtFPAttestation"
        Me.txtFPAttestation.Size = New System.Drawing.Size(90, 29)
        Me.txtFPAttestation.TabIndex = 2
        '
        'LabelX18
        '
        Me.LabelX18.AutoSize = True
        '
        '
        '
        Me.LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX18.Location = New System.Drawing.Point(73, 47)
        Me.LabelX18.Name = "LabelX18"
        Me.LabelX18.Size = New System.Drawing.Size(86, 18)
        Me.LabelX18.TabIndex = 24
        Me.LabelX18.Text = "F.P Attestation"
        '
        'LabelX11
        '
        Me.LabelX11.AutoSize = True
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Location = New System.Drawing.Point(327, 12)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(167, 18)
        Me.LabelX11.TabIndex = 1
        Me.LabelX11.Text = "<font color=""#ED1C24""><font color=""#ED1C24""><font color=""#BA1419"">(Only the numbe" & _
    "r is needed)</font></font></font>"
        '
        'txtTABill
        '
        Me.txtTABill.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtTABill.Border.Class = "TextBoxBorder"
        Me.txtTABill.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtTABill.DisabledBackColor = System.Drawing.Color.White
        Me.txtTABill.FocusHighlightEnabled = True
        Me.txtTABill.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTABill.ForeColor = System.Drawing.Color.Black
        Me.txtTABill.Location = New System.Drawing.Point(466, 78)
        Me.txtTABill.MaxLength = 2
        Me.txtTABill.Name = "txtTABill"
        Me.txtTABill.Size = New System.Drawing.Size(90, 29)
        Me.txtTABill.TabIndex = 7
        '
        'txtRBWarrant
        '
        Me.txtRBWarrant.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtRBWarrant.Border.Class = "TextBoxBorder"
        Me.txtRBWarrant.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtRBWarrant.DisabledBackColor = System.Drawing.Color.White
        Me.txtRBWarrant.FocusHighlightEnabled = True
        Me.txtRBWarrant.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRBWarrant.ForeColor = System.Drawing.Color.Black
        Me.txtRBWarrant.Location = New System.Drawing.Point(210, 148)
        Me.txtRBWarrant.MaxLength = 2
        Me.txtRBWarrant.Name = "txtRBWarrant"
        Me.txtRBWarrant.Size = New System.Drawing.Size(90, 29)
        Me.txtRBWarrant.TabIndex = 5
        '
        'txtSOCDAStatement
        '
        Me.txtSOCDAStatement.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtSOCDAStatement.Border.Class = "TextBoxBorder"
        Me.txtSOCDAStatement.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtSOCDAStatement.DisabledBackColor = System.Drawing.Color.White
        Me.txtSOCDAStatement.FocusHighlightEnabled = True
        Me.txtSOCDAStatement.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSOCDAStatement.ForeColor = System.Drawing.Color.Black
        Me.txtSOCDAStatement.Location = New System.Drawing.Point(466, 43)
        Me.txtSOCDAStatement.MaxLength = 2
        Me.txtSOCDAStatement.Name = "txtSOCDAStatement"
        Me.txtSOCDAStatement.Size = New System.Drawing.Size(90, 29)
        Me.txtSOCDAStatement.TabIndex = 6
        '
        'txtIndividualPerformance
        '
        Me.txtIndividualPerformance.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtIndividualPerformance.Border.Class = "TextBoxBorder"
        Me.txtIndividualPerformance.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtIndividualPerformance.DisabledBackColor = System.Drawing.Color.White
        Me.txtIndividualPerformance.FocusHighlightEnabled = True
        Me.txtIndividualPerformance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndividualPerformance.ForeColor = System.Drawing.Color.Black
        Me.txtIndividualPerformance.Location = New System.Drawing.Point(210, 113)
        Me.txtIndividualPerformance.MaxLength = 2
        Me.txtIndividualPerformance.Name = "txtIndividualPerformance"
        Me.txtIndividualPerformance.Size = New System.Drawing.Size(90, 29)
        Me.txtIndividualPerformance.TabIndex = 4
        '
        'txtAttendance
        '
        Me.txtAttendance.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtAttendance.Border.Class = "TextBoxBorder"
        Me.txtAttendance.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtAttendance.DisabledBackColor = System.Drawing.Color.White
        Me.txtAttendance.FocusHighlightEnabled = True
        Me.txtAttendance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttendance.ForeColor = System.Drawing.Color.Black
        Me.txtAttendance.Location = New System.Drawing.Point(210, 8)
        Me.txtAttendance.MaxLength = 2
        Me.txtAttendance.Name = "txtAttendance"
        Me.txtAttendance.Size = New System.Drawing.Size(90, 29)
        Me.txtAttendance.TabIndex = 1
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Location = New System.Drawing.Point(327, 82)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(39, 18)
        Me.LabelX10.TabIndex = 18
        Me.LabelX10.Text = "TA Bill"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(327, 47)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(133, 18)
        Me.LabelX3.TabIndex = 17
        Me.LabelX3.Text = "SOC and DA Statement"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(73, 152)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(99, 18)
        Me.LabelX4.TabIndex = 16
        Me.LabelX4.Text = "Rail/Bus Warrant"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Location = New System.Drawing.Point(73, 117)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(134, 18)
        Me.LabelX8.TabIndex = 15
        Me.LabelX8.Text = "Individual Performance"
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Location = New System.Drawing.Point(73, 12)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(67, 18)
        Me.LabelX9.TabIndex = 14
        Me.LabelX9.Text = "Attendance"
        '
        'wzrdPageDatabaseLocation
        '
        Me.wzrdPageDatabaseLocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageDatabaseLocation.AntiAlias = False
        Me.wzrdPageDatabaseLocation.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageDatabaseLocation.Controls.Add(Me.lblDatabaseLocation)
        Me.wzrdPageDatabaseLocation.Controls.Add(Me.Label8)
        Me.wzrdPageDatabaseLocation.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPageDatabaseLocation.Name = "wzrdPageDatabaseLocation"
        Me.wzrdPageDatabaseLocation.PageDescription = "Database Location"
        Me.wzrdPageDatabaseLocation.PageTitle = "Database Location"
        Me.wzrdPageDatabaseLocation.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPageDatabaseLocation.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageDatabaseLocation.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageDatabaseLocation.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageDatabaseLocation.TabIndex = 16
        '
        'lblDatabaseLocation
        '
        Me.lblDatabaseLocation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatabaseLocation.Location = New System.Drawing.Point(33, 60)
        Me.lblDatabaseLocation.Name = "lblDatabaseLocation"
        Me.lblDatabaseLocation.Size = New System.Drawing.Size(529, 68)
        Me.lblDatabaseLocation.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(33, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(428, 51)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Database is the file where all the records are stored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click the link below if y" & _
    "ou want to change the location of Database." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Existing Database will be copied to" & _
    " the new location."
        '
        'wzrdPageBackup
        '
        Me.wzrdPageBackup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageBackup.AntiAlias = False
        Me.wzrdPageBackup.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageBackup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.wzrdPageBackup.Controls.Add(Me.Label7)
        Me.wzrdPageBackup.Controls.Add(Me.txtBackupInterval)
        Me.wzrdPageBackup.Controls.Add(Me.chkAutoBackup)
        Me.wzrdPageBackup.Controls.Add(Me.lblBackupLocation)
        Me.wzrdPageBackup.Controls.Add(Me.Label6)
        Me.wzrdPageBackup.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPageBackup.Name = "wzrdPageBackup"
        Me.wzrdPageBackup.PageDescription = "Database Backup"
        Me.wzrdPageBackup.PageTitle = "Database Backup"
        Me.wzrdPageBackup.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPageBackup.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageBackup.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageBackup.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageBackup.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(339, 122)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 15)
        Me.Label7.TabIndex = 60
        Me.Label7.Text = "Days"
        '
        'txtBackupInterval
        '
        Me.txtBackupInterval.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtBackupInterval.Border.Class = "TextBoxBorder"
        Me.txtBackupInterval.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtBackupInterval.DisabledBackColor = System.Drawing.Color.White
        Me.txtBackupInterval.FocusHighlightEnabled = True
        Me.txtBackupInterval.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackupInterval.ForeColor = System.Drawing.Color.Black
        Me.txtBackupInterval.Location = New System.Drawing.Point(268, 116)
        Me.txtBackupInterval.MaxLength = 2
        Me.txtBackupInterval.Name = "txtBackupInterval"
        Me.txtBackupInterval.Size = New System.Drawing.Size(65, 27)
        Me.txtBackupInterval.TabIndex = 59
        '
        'chkAutoBackup
        '
        Me.chkAutoBackup.AutoSize = True
        '
        '
        '
        Me.chkAutoBackup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkAutoBackup.Location = New System.Drawing.Point(36, 119)
        Me.chkAutoBackup.Name = "chkAutoBackup"
        Me.chkAutoBackup.Size = New System.Drawing.Size(228, 18)
        Me.chkAutoBackup.TabIndex = 58
        Me.chkAutoBackup.TabStop = False
        Me.chkAutoBackup.Text = "Take Backup Automatically on every "
        '
        'lblBackupLocation
        '
        Me.lblBackupLocation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupLocation.Location = New System.Drawing.Point(33, 60)
        Me.lblBackupLocation.Name = "lblBackupLocation"
        Me.lblBackupLocation.Size = New System.Drawing.Size(529, 68)
        Me.lblBackupLocation.TabIndex = 8
        Me.lblBackupLocation.TabStop = True
        Me.lblBackupLocation.Text = " "
        '
        'Label6
        '
        Me.Label6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(33, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(481, 51)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Regular backing up of Database is recommended to avoid data loss." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click the link" & _
    " below if you want to change the Backup location for Database." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Existing Backups" & _
    " will be copied to the new location."
        '
        'wzrdPageFPSlipLocation
        '
        Me.wzrdPageFPSlipLocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageFPSlipLocation.AntiAlias = False
        Me.wzrdPageFPSlipLocation.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageFPSlipLocation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.wzrdPageFPSlipLocation.Controls.Add(Me.lblFPLocation)
        Me.wzrdPageFPSlipLocation.Controls.Add(Me.Label4)
        Me.wzrdPageFPSlipLocation.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPageFPSlipLocation.Name = "wzrdPageFPSlipLocation"
        Me.wzrdPageFPSlipLocation.PageDescription = "Scanned FP Slips Location "
        Me.wzrdPageFPSlipLocation.PageTitle = "Scanned FP Slips Location "
        Me.wzrdPageFPSlipLocation.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPageFPSlipLocation.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageFPSlipLocation.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageFPSlipLocation.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageFPSlipLocation.TabIndex = 11
        '
        'lblFPLocation
        '
        Me.lblFPLocation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFPLocation.Location = New System.Drawing.Point(33, 45)
        Me.lblFPLocation.Name = "lblFPLocation"
        Me.lblFPLocation.Size = New System.Drawing.Size(534, 68)
        Me.lblFPLocation.TabIndex = 5
        Me.lblFPLocation.TabStop = True
        Me.lblFPLocation.Text = " "
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(479, 34)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Click the link below if you want to change the location for Scanned FP Slips." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ex" & _
    "isting FP Slips if any, will not be copied to new location."
        '
        'wzrdPageChancePrinLocation
        '
        Me.wzrdPageChancePrinLocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageChancePrinLocation.AntiAlias = False
        Me.wzrdPageChancePrinLocation.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageChancePrinLocation.Controls.Add(Me.lblCPLocation)
        Me.wzrdPageChancePrinLocation.Controls.Add(Me.Label5)
        Me.wzrdPageChancePrinLocation.Location = New System.Drawing.Point(7, 102)
        Me.wzrdPageChancePrinLocation.Name = "wzrdPageChancePrinLocation"
        Me.wzrdPageChancePrinLocation.PageDescription = "Chance Print Image Location"
        Me.wzrdPageChancePrinLocation.PageTitle = "Chance Print Image Location"
        Me.wzrdPageChancePrinLocation.Size = New System.Drawing.Size(631, 234)
        '
        '
        '
        Me.wzrdPageChancePrinLocation.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageChancePrinLocation.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageChancePrinLocation.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageChancePrinLocation.TabIndex = 12
        '
        'lblCPLocation
        '
        Me.lblCPLocation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPLocation.Location = New System.Drawing.Point(33, 45)
        Me.lblCPLocation.Name = "lblCPLocation"
        Me.lblCPLocation.Size = New System.Drawing.Size(534, 55)
        Me.lblCPLocation.TabIndex = 7
        Me.lblCPLocation.TabStop = True
        Me.lblCPLocation.Text = " "
        '
        'Label5
        '
        Me.Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(33, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(511, 34)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Click the link below if you want to select a new location for saving Chance Print" & _
    "s." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Existing Chance Prints if any, will not be copied to new location."
        '
        'wzrdPageFinish
        '
        Me.wzrdPageFinish.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wzrdPageFinish.AntiAlias = False
        Me.wzrdPageFinish.BackColor = System.Drawing.Color.Transparent
        Me.wzrdPageFinish.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.wzrdPageFinish.Controls.Add(Me.LabelX1)
        Me.wzrdPageFinish.InteriorPage = False
        Me.wzrdPageFinish.Location = New System.Drawing.Point(0, 0)
        Me.wzrdPageFinish.Name = "wzrdPageFinish"
        Me.wzrdPageFinish.PageDescription = "Finish"
        Me.wzrdPageFinish.PageTitle = "Finish"
        Me.wzrdPageFinish.Size = New System.Drawing.Size(645, 348)
        '
        '
        '
        Me.wzrdPageFinish.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageFinish.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.wzrdPageFinish.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.wzrdPageFinish.TabIndex = 10
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(28, 24)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(571, 22)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "Thank You! Please press Finish to close the Wizard and to launch the application." & _
    ""
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'OfficerTableTableAdapter1
        '
        Me.OfficerTableTableAdapter1.ClearBeforeFill = True
        '
        'SettingsTableAdapter1
        '
        Me.SettingsTableAdapter1.ClearBeforeFill = True
        '
        'StyleManager1
        '
        Me.StyleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2016
        Me.StyleManager1.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(199, Byte), Integer)))
        '
        'FrmSettingsWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 401)
        Me.ControlBox = False
        Me.Controls.Add(Me.SettingsWizard)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmSettingsWizard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings Wizard"
        Me.TitleText = "<b>Settings Wizard</b>"
        Me.SettingsWizard.ResumeLayout(False)
        Me.StartPage.ResumeLayout(False)
        Me.StartPage.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.wzrdPageOfficeUnit.ResumeLayout(False)
        Me.wzrdPageOfficeUnit.PerformLayout()
        Me.wzrdPageOfficers.ResumeLayout(False)
        Me.wzrdPageOfficers.PerformLayout()
        Me.wzrdPagePeriodical.ResumeLayout(False)
        Me.wzrdPagePeriodical.PerformLayout()
        Me.wzrdPageDatabaseLocation.ResumeLayout(False)
        Me.wzrdPageDatabaseLocation.PerformLayout()
        Me.wzrdPageBackup.ResumeLayout(False)
        Me.wzrdPageBackup.PerformLayout()
        Me.wzrdPageFPSlipLocation.ResumeLayout(False)
        Me.wzrdPageFPSlipLocation.PerformLayout()
        Me.wzrdPageChancePrinLocation.ResumeLayout(False)
        Me.wzrdPageChancePrinLocation.PerformLayout()
        Me.wzrdPageFinish.ResumeLayout(False)
        Me.wzrdPageFinish.PerformLayout()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SettingsWizard As DevComponents.DotNetBar.Wizard
    Friend WithEvents StartPage As DevComponents.DotNetBar.WizardPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents wzrdPageOfficeUnit As DevComponents.DotNetBar.WizardPage
    Friend WithEvents wzrdPageFinish As DevComponents.DotNetBar.WizardPage
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtFullDistrict As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtShortDistrict As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtShortOffice As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtFullOffice As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents wzrdPageFPSlipLocation As DevComponents.DotNetBar.WizardPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblFPLocation As System.Windows.Forms.LinkLabel
    Friend WithEvents wzrdPageChancePrinLocation As DevComponents.DotNetBar.WizardPage
    Friend WithEvents lblCPLocation As System.Windows.Forms.LinkLabel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents wzrdPagePeriodical As DevComponents.DotNetBar.WizardPage
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtTABill As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtRBWarrant As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtSOCDAStatement As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtIndividualPerformance As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtAttendance As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents wzrdPageOfficers As DevComponents.DotNetBar.WizardPage
    Friend WithEvents txtFPS As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtFPE2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtFPE3 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtFPE1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtTI As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPhotographer As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtFPAttestation As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX18 As DevComponents.DotNetBar.LabelX
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents OfficerTableTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.OfficerTableTableAdapter
    Friend WithEvents SettingsTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SettingsTableAdapter
    Friend WithEvents wzrdPageBackup As DevComponents.DotNetBar.WizardPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblBackupLocation As System.Windows.Forms.LinkLabel
    Friend WithEvents chkAutoBackup As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBackupInterval As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents wzrdPageDatabaseLocation As DevComponents.DotNetBar.WizardPage
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblDatabaseLocation As System.Windows.Forms.LinkLabel
    Friend WithEvents txtGraveCrime As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtVigilanceCase As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtWeeklyDiary As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX21 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents StyleManager1 As DevComponents.DotNetBar.StyleManager
End Class
